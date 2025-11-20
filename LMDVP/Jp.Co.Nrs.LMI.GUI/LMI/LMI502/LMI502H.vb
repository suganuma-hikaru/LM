' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI    : データ管理サブ
'  プログラムID     :  LMI502 : デュポン在庫
'  作  成  者       :  
' ==========================================================================
Imports System.IO
Imports System.Text
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI502ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI502H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconH As LMIControlH

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'Hnadler共通クラスの設定
        Me._LMIconH = New LMIControlH(MyBase.GetPGID())

        Dim ds As DataSet = Me._LMIconH.SetInData(prm.ParamDataSet, New LMI502DS())

        'ファイル作成処理
        Call Me.CreatePrintData(ds)

        'インスタンスの開放
        Call LMFormNavigate.Revoke(Me)

    End Sub

    ''' <summary>
    ''' ファイル作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub CreatePrintData(ByVal ds As DataSet)

        'サーバアクセス
        Dim rtnDs As DataSet = MyBase.CallWSA(String.Concat(LMI500C.CLASS_NM, LMIControlC.BLF), LMI502C.ACTION_ID_SELECT_DATA, ds)

        'エラーの場合、終了
        If MyBase.IsMessageExist() = True Then
            Exit Sub
        End If

        Dim inDr As DataRow = ds.Tables(LMI020C.TABLE_NM_IN).Rows(0)
        Dim plantCd As String = inDr.Item("PLANT_CD").ToString()
        Dim nrsBrCd As String = inDr.Item("NRS_BR_CD").ToString()
        Dim custCdL As String = inDr.Item("CUST_CD_L").ToString()
        Dim value As StringBuilder = Me.SetTextFileData(rtnDs, plantCd, nrsBrCd, custCdL)

        'Dim fileNm As String = Me._LMIconH.GetFileNmAndAccess(String.Concat(plantCd, "_", inDr.Item("REPORT_DATE").ToString(), ".txt"))
        '保存先のファイルのパス
        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'E021'"))
        Dim filePath As String = String.Concat(kbnDr(0).Item("KBN_NM1").ToString, "\", MyBase.GetPGID(), "\", inDr.Item("CUST_CD_S").ToString, "\")
        Dim fileNm As String = String.Concat(filePath, plantCd, "_", inDr.Item("REPORT_DATE").ToString(), ".txt")
        System.IO.Directory.CreateDirectory(filePath)

        'ファイルが存在する場合、削除
        Dim file As FileInfo = New FileInfo(fileNm)
        If file.Exists = True Then
            file.Delete()
        End If

        'Shift-Jisでファイルを作成
        Dim sw As StreamWriter = New StreamWriter(fileNm, True, System.Text.Encoding.GetEncoding("Shift_Jis"))
        sw.Write(value.ToString())

        'ファイルを閉じる
        sw.Close()

        '作成ファイルの出力
        Me._LMIconH.OutFileOpen(New String() {fileNm})

    End Sub

    ''' <summary>
    ''' テキストファイルに出力する内容を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="plantCd">プラントコード</param>
    ''' <param name="nrsBrCd">営業所コード</param>
    ''' <param name="custCdL">荷主コード(大)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTextFileData(ByVal ds As DataSet, ByVal plantCd As String, ByVal nrsBrCd As String, ByVal custCdL As String) As StringBuilder

        SetTextFileData = Nothing
        Dim dt As DataTable = ds.Tables(LMI502C.TABLE_NM_LMI502SET)
        Dim max As Integer = dt.Rows.Count - 1

        'WQ49の場合
        If LMI502C.PLANTCD_SPECIAL.Equals(plantCd) = True Then

            SetTextFileData = Me.SetTextFileSpecialData(dt)

        Else

            If nrsBrCd = "10" AndAlso custCdL = LMI502C.CUST_CD_L_CELANESE Then

                SetTextFileData = Me.SetTextFileNomalData2(dt)

            Else

                SetTextFileData = Me.SetTextFileNomalData(dt)

            End If

        End If

        Return SetTextFileData

    End Function

    ''' <summary>
    ''' テキストファイルの出力内容を設定(WQ49)
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTextFileSpecialData(ByVal dt As DataTable) As StringBuilder

        Dim max As Integer = dt.Rows.Count - 1
        Dim value As StringBuilder = New StringBuilder()

        If -1 < max Then

            '値設定
            value = Me.SetTextFileSpecialData(dt.Rows(0), value)

            For i As Integer = 1 To max

                '2行以降は改行
                value.Append(vbNewLine)

                '値設定
                value = Me.SetTextFileSpecialData(dt.Rows(i), value)

            Next

        End If

        Return value

    End Function

    ''' <summary>
    ''' テキストファイルの出力内容を設定(WQ49)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="value">設定先変数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTextFileSpecialData(ByVal dr As DataRow, ByVal value As StringBuilder) As StringBuilder

        'PLANT(4)
        value.Append(Me.SetSpaceCoverData(dr.Item("PLANT").ToString(), 4))

        'ロケーション(4)
        value.Append(Me.SetSpaceCoverData(dr.Item("LOCATION").ToString(), 4))

        '商品コード(18)
        value.Append(Me.SetSpaceCoverData(dr.Item("GOODS_CD_CUST").ToString(), 18))

        'LOT(10)
        value.Append(Me.SetSpaceCoverData(dr.Item("LOT").ToString(), 10))

        '個数(12)
        value.Append(Me.SetSpaceCoverData(dr.Item("NB").ToString(), 12))

        '個数単位
        value.Append(LMI502C.NB_UT_EA)

        'ブロック数量
        value.Append(Space(13))

        Return value

    End Function

    ''' <summary>
    ''' テキストファイルの出力内容を設定(WQ49以外)
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTextFileNomalData(ByVal dt As DataTable) As StringBuilder

        Dim max As Integer = dt.Rows.Count - 1
        Dim value As StringBuilder = New StringBuilder()

        If -1 < max Then

            '値設定
            value = Me.SetTextFileNomalData(dt.Rows(0), value)

            For i As Integer = 1 To max

                '2行以降は改行
                value.Append(vbNewLine)

                '値設定
                value = Me.SetTextFileNomalData(dt.Rows(i), value)

            Next

        End If

        Return value

    End Function

    ''' <summary>
    ''' テキストファイルの出力内容を設定(WQ49以外)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="value">設定先変数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTextFileNomalData(ByVal dr As DataRow, ByVal value As StringBuilder) As StringBuilder

        'PLANT(4)
        value.Append(Me.SetSpaceCoverData(dr.Item("PLANT").ToString(), 4))

        'ロケーション(4)
        value.Append(Me.SetSpaceCoverData(dr.Item("LOCATION").ToString(), 4))

        '商品コード(15)
        value.Append(Me.SetSpaceCoverData(dr.Item("GOODS_CD_CUST").ToString(), 15))

        'LOT(20)
        value.Append(Me.SetSpaceCoverData(dr.Item("LOT").ToString(), 20))

        '個数(12)
        value.Append(Me.SetSpaceCoverData(dr.Item("QT").ToString(), 12))

        Return value

    End Function

    ''' <summary>
    ''' テキストファイルの出力内容を設定(千葉・セラニーズ 専用)
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTextFileNomalData2(ByVal dt As DataTable) As StringBuilder

        Dim max As Integer = dt.Rows.Count - 1
        Dim value As StringBuilder = New StringBuilder()

        If -1 < max Then

            '値設定
            value = Me.SetTextFileNomalData2(dt.Rows(0), value)

            For i As Integer = 1 To max

                '2行以降は改行
                value.Append(vbNewLine)

                '値設定
                value = Me.SetTextFileNomalData2(dt.Rows(i), value)

            Next

        End If

        Return value

    End Function

    ''' <summary>
    ''' テキストファイルの出力内容を設定(千葉・セラニーズ 専用)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="value">設定先変数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTextFileNomalData2(ByVal dr As DataRow, ByVal value As StringBuilder) As StringBuilder

        'PLANT(4)
        value.Append(Me.SetSpaceCoverData(dr.Item("PLANT").ToString(), 4))

        'ロケーション(4)
        value.Append(Me.SetSpaceCoverData(dr.Item("LOCATION").ToString(), 4))

        '商品コード(15)
        value.Append(Me.SetSpaceCoverData2(dr.Item("GOODS_CD_CUST").ToString(), 15))

        'LOT(20)
        value.Append(Me.SetSpaceCoverData(dr.Item("LOT").ToString(), 20))

        '個数(12)
        value.Append(Me.SetSpaceCoverData(dr.Item("QT").ToString(), 12))

        Return value

    End Function

    ''' <summary>
    ''' スペース埋め処理
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="ketasu">設定桁数</param>
    ''' <returns>スペースつき設定値</returns>
    ''' <remarks></remarks>
    Private Function SetSpaceCoverData(ByVal value As String, ByVal ketasu As Integer) As String

        SetSpaceCoverData = String.Concat(value, Space(ketasu))

        Return SetSpaceCoverData.Substring(0, ketasu)

    End Function

    ''' <summary>
    ''' スペース埋め処理(千葉・セラニーズの商品コード専用)
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="ketasu">設定桁数</param>
    ''' <returns>スペースつき設定値</returns>
    ''' <remarks></remarks>
    Private Function SetSpaceCoverData2(ByVal value As String, ByVal ketasu As Integer) As String

        SetSpaceCoverData2 = String.Concat(value, Space(ketasu))

        Dim cutStrTrimEnd As String = SetSpaceCoverData2.Substring(ketasu).TrimEnd()
        Dim cutStrTrimEndLen As Integer = cutStrTrimEnd.Length()
        If cutStrTrimEndLen = 0 Then
            ' 値の先頭から指定桁数より右に文字がない場合、通常のスペース埋め処理と同じ結果を返す。
            Return SetSpaceCoverData2.Substring(0, ketasu)
        End If

        ' 値の先頭から指定桁数より右に文字がある場合、
        ' 値にその文字数分の前ゼロがあれば、指定桁数を切り出す先頭を、前出の文字数分進めた位置からとする。
        If SetSpaceCoverData2.Substring(0, ketasu).Substring(0, cutStrTrimEndLen) = (New String("0"c, cutStrTrimEndLen)) _
            AndAlso SetSpaceCoverData2.Length() >= cutStrTrimEndLen + ketasu Then
            Return SetSpaceCoverData2.Substring(cutStrTrimEndLen, ketasu)
        Else
            Return SetSpaceCoverData2.Substring(0, ketasu)
        End If

    End Function

#End Region

#End Region

End Class
