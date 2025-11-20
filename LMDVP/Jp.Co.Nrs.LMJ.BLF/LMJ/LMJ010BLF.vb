' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ       : システム管理
'  プログラムID     :  LMJ010DAC : 請求在庫・実在庫差異分リスト作成
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMJ010BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMJ010BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMJ010BLC = New LMJ010BLC()

#End Region

#Region "Const"

    ''' <summary>
    ''' LMJ010INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMJ010IN"

    ''' <summary>
    ''' LMJ010OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMJ010OUT"

    ''' <summary>
    ''' LMJ800OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_800 As String = "LMJ800OUT"

    ''' <summary>
    ''' ZAIK_ZANテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_ZAIK_ZAN As String = "ZAIK_ZAN"

    ''' <summary>
    ''' SYS_DATETIMEテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SYS_DATETIME As String = "SYS_DATETIME"

    ''' <summary>
    ''' ERRテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_ERR As String = "ERR"

    ''' <summary>
    ''' 締め荷主取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_SHIME_CUST As String = "SelectShimeCust"

    ''' <summary>
    ''' 請求在庫の荷主取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_SEIQ_CUST As String = "SelectSeiqZaikoData"

    ''' <summary>
    ''' バッチ呼び出しアクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_CREATE_DATA As String = "SelectListData"

    ''' <summary>
    ''' 処理内容 = 指定された荷主のみチェックする
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHORI_SONOTA As String = "99"

    ''' <summary>
    ''' 請求在庫存在エラー
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ERR_SEIQ_DATA As String = "01"

    ''' <summary>
    ''' 月末在庫存在エラー
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ERR_GETSU_DATA As String = "02"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' コンボ用のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGetuData(ByVal ds As DataSet) As DataSet
        ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
        Return Me.SetSysDateTime(ds)
    End Function

    ''' <summary>
    ''' 帳票出力データの作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCreateData(ByVal ds As DataSet) As DataSet

        Dim closeKb As String = ds.Tables(LMJ010BLF.TABLE_NM_IN).Rows(0).Item("CLOSE_KB").ToString()

        '締め荷主を取得
        ds = Me.BlcAccess(ds, LMJ010BLF.ACTION_ID_SELECT_SHIME_CUST)

        '指定荷主の場合
        If LMJ010BLF.SHORI_SONOTA.Equals(closeKb) = True Then
            ds = Me.SelectCustData(ds)
        Else

            ds = Me.SelectAllCustData(ds)

            'エラーメッセージは帳票に出力するためここでは削除
            ds.Tables(LMJ010BLF.TABLE_NM_ERR).Clear()

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 指定荷主処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        'エラー判定(締め荷主チェック)
        If Me.JugErrData(ds) = False Then
            Return ds
        End If

        '件数チェック
        ds = Me.ChkSelectCountData(ds)

        'エラー判定
        If Me.JugErrData(ds) = False Then
            Return ds
        End If

        'バッチ呼び出し
        Return Me.SetFileData(ds, ds, ds.Tables(LMJ010BLF.TABLE_NM_IN).Rows(0), New LMJ800BLC())

    End Function

    ''' <summary>
    ''' 全荷主に対しての処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectAllCustData(ByVal ds As DataSet) As DataSet

        'エラー判定(締め荷主チェック)
        If Me.JugErrData(ds) = False Then
            Return ds
        End If

        Dim dt As DataTable = ds.Tables(LMJ010BLF.TABLE_NM_IN)
        Dim selectDs As DataSet = ds.Copy
        Dim selectDt As DataTable = selectDs.Tables(LMJ010BLF.TABLE_NM_IN)
        Dim selectDr As DataRow = Nothing
        Dim errDt As DataTable = selectDs.Tables(LMJ010BLF.TABLE_NM_ERR)
        Dim max As Integer = dt.Rows.Count - 1
        Dim blc As LMJ800BLC = New LMJ800BLC()
        For i As Integer = 0 To max

            '変数の初期化
            selectDt.Clear()
            errDt.Clear()

            '検索するレコードの設定
            selectDt.ImportRow(dt.Rows(i))

            '件数チェック
            selectDr = selectDt.Rows(0)
            selectDs = Me.ChkSelectCountData(selectDs)
            If Me.JugErrData(selectDs) = False Then
                ds = Me.SetErrData(ds, selectDs, selectDr)
                Continue For
            End If

            'バッチ呼び出し
            ds = Me.SetFileData(ds, selectDs, selectDr, blc)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 件数チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkSelectCountData(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' バッチ処理の反映
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="selectDs">DataSet</param>
    ''' <param name="selectDr">DataRow</param>
    ''' <param name="blc">LMJ800BLC</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetFileData(ByVal ds As DataSet, ByVal selectDs As DataSet, ByVal selectDr As DataRow, ByVal blc As LMJ800BLC) As DataSet

        'バッチ処理
        selectDs = MyBase.CallBLC(blc, LMJ010BLF.ACTION_ID_SELECT_CREATE_DATA, selectDs)

        '戻り値の設定
        Dim dt As DataTable = ds.Tables(LMJ010BLF.TABLE_NM_OUT)
        Dim dr As DataRow = Nothing

        '戻り値の反映
        Dim rtnDt As DataTable = selectDs.Tables(LMJ010BLF.TABLE_NM_800)
        Dim rtnDr As DataRow = Nothing
        Dim max As Integer = rtnDt.Rows.Count - 1
        Dim unMatchFlg As Boolean = True

        '請求在庫の荷主を取得
        selectDs = Me.BlcAccess(selectDs, LMJ010BLF.ACTION_ID_SELECT_SEIQ_CUST)
        Dim seiqCustDt As DataTable = ds.Tables(LMJ010BLF.TABLE_NM_ZAIK_ZAN)

        For i As Integer = 0 To max

            '初期設定
            dr = Me.SetInitDataRow(selectDr, dt.NewRow())

            rtnDr = rtnDt.Rows(i)
            With rtnDr

                'アンマッチチェック
                unMatchFlg = Me.ChkSuryoData(rtnDr)

                dr.Item("GOODS_CD_NRS") = .Item("GOODS_CD_NRS").ToString()
                dr.Item("GOODS_CD_CUST") = .Item("GOODS_CD_CUST").ToString()
                dr.Item("CUST_CD_DTL") = Me.GetCustCd(rtnDr)
                dr.Item("GOODS_NM") = .Item("GOODS_NM").ToString()
                dr.Item("LOT_NO") = .Item("LOT_NO").ToString()
                dr.Item("SERIAL_NO") = .Item("SERIAL_NO").ToString()
                dr.Item("HIKAKU_ZAI_NB") = Me.GetSeiqZaikoKosu(rtnDr, 0 < seiqCustDt.Select(String.Concat("CUST_CD_S = '", .Item("CUST_CD_S").ToString(), "' AND CUST_CD_SS = '", .Item("CUST_CD_SS").ToString(), "' ")).Length)
                dr.Item("HIKAKU_ZAI_QT") = Me.GetSeiqZaikoSuryo(rtnDr, unMatchFlg)
                dr.Item("ZAI_NB") = .Item("PORA_ZAI_NB").ToString()
                dr.Item("ZAI_QT") = Me.GetJitsuZaikoSuryo(rtnDr, unMatchFlg)
                dr.Item("UNMATCH") = Me.GetUnMatchData(unMatchFlg)

            End With

            '行追加
            dt.Rows.Add(dr)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 荷主コードを連結して返却
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>文字</returns>
    ''' <remarks></remarks>
    Private Function GetCustCd(ByVal dr As DataRow) As String

        With dr

            GetCustCd = Me.EditConcatData(.Item("CUST_CD_L").ToString(), .Item("CUST_CD_M").ToString())
            GetCustCd = Me.EditConcatData(GetCustCd, .Item("CUST_CD_S").ToString())
            GetCustCd = Me.EditConcatData(GetCustCd, .Item("CUST_CD_SS").ToString())

            Return GetCustCd

        End With

    End Function

    ''' <summary>
    ''' 請求在庫個数の設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="cntFlg">請求在庫存在フラグ　True:請求在庫有　False:請求在庫無</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeiqZaikoKosu(ByVal dr As DataRow, ByVal cntFlg As Boolean) As String

        With dr

            '請求在庫有
            Dim exNb As Decimal = Convert.ToDecimal(Me.FormatNumValue(.Item("EXTRA_ZAI_NB").ToString()))
            If cntFlg = True Then

                'HIKAKU_ZAI_NB = 0 且つ EXTRA_ZAI_NB = 0
                If 0 = Convert.ToDecimal(Me.FormatNumValue(.Item("HIKAKU_ZAI_NB").ToString())) _
                    AndAlso 0 = exNb _
                    Then
                    Return "データなし"
                End If

                'EXTRA_ZAI_NB = 1
                If 1 = exNb Then
                    Return "0"
                End If

            Else

                'HIKAKU_ZAI_NB = 0 且つ EXTRA_ZAI_NB = 0
                If 0 = Convert.ToDecimal(Me.FormatNumValue(.Item("HIKAKU_ZAI_NB").ToString())) _
                    AndAlso 0 = exNb _
                    Then
                    Return "請求未完了"
                End If

                'EXTRA_ZAI_NB = 1
                If 1 = exNb Then
                    Return "データなし"
                End If

            End If

            Return .Item("EXTRA_ZAI_NB").ToString()

        End With

    End Function

    ''' <summary>
    ''' 請求在庫数量の設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="flg">アンマッチ判定フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeiqZaikoSuryo(ByVal dr As DataRow, ByVal flg As Boolean) As String
        'アンマッチの場合
        If flg = False Then
            Return dr.Item("HIKAKU_ZAI_QT").ToString()
        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' 実在個数量の設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="flg">アンマッチ判定フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetJitsuZaikoSuryo(ByVal dr As DataRow, ByVal flg As Boolean) As String

        'アンマッチの場合
        If flg = False Then
            Return dr.Item("PORA_ZAI_QT").ToString()
        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' 数量不一致の設定
    ''' </summary>
    ''' <param name="flg">アンマッチ判定フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUnMatchData(ByVal flg As Boolean) As String

        'アンマッチの場合
        If flg = False Then
            Return "*"
        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' アンマッチチェック
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>True:正常　False:アンマッチ</returns>
    ''' <remarks></remarks>
    Private Function ChkSuryoData(ByVal dr As DataRow) As Boolean

        With dr

            'EXTRA_ZAI_NB = PORA_ZAI_NB 且つ HIKAKU_ZAI_QT <> PORA_ZAI_QT
            If Convert.ToDecimal(Me.FormatNumValue(.Item("HIKAKU_ZAI_NB").ToString())) = Convert.ToDecimal(Me.FormatNumValue(.Item("PORA_ZAI_NB").ToString())) _
                AndAlso Convert.ToDecimal(Me.FormatNumValue(.Item("HIKAKU_ZAI_QT").ToString())) <> Convert.ToDecimal(Me.FormatNumValue(.Item("PORA_ZAI_QT").ToString())) _
                Then
                Return False
            End If

            Return True

        End With

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc, actionId, ds)

    End Function

    ''' <summary>
    ''' システム日時を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSysDateTime(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMJ010BLF.TABLE_NM_SYS_DATETIME)
        dt.Clear()
        Dim dr As DataRow = dt.NewRow()
        dr.Item("SYS_DATE") = MyBase.GetSystemDate()
        dr.Item("SYS_TIME") = MyBase.GetSystemTime()
        dt.Rows.Add(dr)
        Return ds

    End Function

    ''' <summary>
    ''' エラー判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function JugErrData(ByVal ds As DataSet) As Boolean

        'エラー設定されている場合、False
        If 0 < ds.Tables(LMJ010BLF.TABLE_NM_ERR).Rows.Count Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 存在チェックエラー設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="selectDs">DataSet</param>
    ''' <param name="selectDr">締め荷主情報のDataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetErrData(ByVal ds As DataSet, ByVal selectDs As DataSet, ByVal selectDr As DataRow) As DataSet

        Dim errDt As DataTable = selectDs.Tables(LMJ010BLF.TABLE_NM_ERR)
        Dim dt As DataTable = ds.Tables(LMJ010BLF.TABLE_NM_OUT)
        Dim dr As DataRow = dt.NewRow()

        '値の初期設定
        dr = Me.SetInitDataRow(selectDr, dr)

        '請求在庫存在エラーの場合
        If LMJ010BLF.ERR_SEIQ_DATA.Equals(errDt.Rows(errDt.Rows.Count - 1).Item("CHK").ToString()) = True Then

            dr.Item("GOODS_NM") = "請求在庫データが存在しません。"

        Else

            dr.Item("GOODS_NM") = "月末在庫履歴データが存在しません。"

        End If

        '行追加
        dt.Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 戻り値の初期設定
    ''' </summary>
    ''' <param name="setDr">締め荷主情報のDataRow</param>
    ''' <param name="dr">設定先DataRow</param>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Private Function SetInitDataRow(ByVal setDr As DataRow, ByVal dr As DataRow) As DataRow

        With setDr

            dr.Item("CUST_NM") = Me.EditConcatData(.Item("CUST_NM_L").ToString(), .Item("CUST_NM_M").ToString(), "　")
            dr.Item("CUST_CD") = Me.EditConcatData(.Item("CUST_CD_L").ToString(), .Item("CUST_CD_M").ToString())
            dr.Item("OUTPUT_DATE") = .Item("SEIKYU_DATE").ToString()
            dr.Item("GOODS_CD_NRS") = String.Empty
            dr.Item("GOODS_CD_CUST") = String.Empty
            dr.Item("CUST_CD_DTL") = String.Empty
            dr.Item("GOODS_NM") = String.Empty
            dr.Item("LOT_NO") = String.Empty
            dr.Item("SERIAL_NO") = String.Empty
            dr.Item("HIKAKU_ZAI_NB") = 0
            dr.Item("HIKAKU_ZAI_QT") = 0
            dr.Item("ZAI_NB") = 0
            dr.Item("ZAI_QT") = String.Empty
            dr.Item("UNMATCH") = String.Empty

        End With

        Return dr

    End Function

    ''' <summary>
    ''' エラー行のマージ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="rtnDs">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ErrDataMerge(ByVal ds As DataSet, ByVal rtnDs As DataSet) As DataSet

        Dim errDt As DataTable = ds.Tables(LMJ010BLF.TABLE_NM_ERR)
        Dim rtnDt As DataTable = rtnDs.Tables(LMJ010BLF.TABLE_NM_ERR)
        Dim max As Integer = rtnDt.Rows.Count - 1
        For i As Integer = 0 To max
            errDt.ImportRow(rtnDt.Rows(i))
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 2つの値を連結して設定
    ''' </summary>
    ''' <param name="value1">値1</param>
    ''' <param name="value2">値2</param>
    ''' <returns>編集後の値</returns>
    ''' <remarks></remarks>
    Private Function EditConcatData(ByVal value1 As String, ByVal value2 As String, Optional ByVal str As String = "-") As String

        EditConcatData = value1
        If String.IsNullOrEmpty(EditConcatData) = True Then

            EditConcatData = value2

        Else

            If String.IsNullOrEmpty(value2) = False Then

                EditConcatData = String.Concat(EditConcatData, str, value2)

            End If

        End If

        Return EditConcatData

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Private Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

#End Region

#End Region

End Class
