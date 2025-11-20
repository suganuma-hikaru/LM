' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG050BLF : 請求処理 請求書作成
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMG050BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG050BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMG050BLC = New LMG050BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print530 As LMG530BLC = New LMG530BLC()


    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print520 As LMG520BLC = New LMG520BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''初期処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function SelectContCurrCd(ByVal ds As DataSet) As DataSet

        '契約通貨コード取得
        ds = MyBase.CallBLC(Me._Blc, "SelectContCurrCd", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 請求鑑ヘッダ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectData", ds)

    End Function

    ''' <summary>
    ''' セット料金の単価マスタが登録された荷主の主請求先(であるか否か) の検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectSeiqtoCustSetPrice(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectSeiqtoCustSetPrice", ds)

    End Function

    ''' <summary>
    ''' TSMC請求明細よりの取込データの取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectTorikomiDataTsmc(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectTorikomiDataTsmc", ds)

    End Function

#End Region

#Region "チェック処理"

    ''' <summary>
    ''' 請求鑑ヘッダ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HaitaChk(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "HaitaChk", ds)

    End Function

    ''' <summary>
    ''' 新規データチェック処理(請求鑑ヘッダチェック)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkNewData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "ChkNewData", ds)

    End Function

    '2014.08.21 追加START 多通貨対応
    ''' <summary>
    ''' 請求通貨＋契約通貨　EX_RATE取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function setCurrList(ByVal ds As DataSet) As DataSet

        '契約通貨コード取得
        ds = MyBase.CallBLC(Me._Blc, "SelectContCurrCd", ds)

        ds = MyBase.CallBLC(Me._Blc, "RepCurrChk", ds)

        If ds.Tables("LMG050_CURRINFO").Rows.Count > 1 Then
            MyBase.SetMessage("E206", New String() {"請求建値", "契約建値"})
            Return ds
        End If

        Return ds

    End Function
    '2014.08.21 追加END 多通貨対応

    ''' <summary>
    ''' 最低保証額チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqtoData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectSeiqtoData", ds)

    End Function

#End Region

#If True Then   ''ADD 2018/08/21 依頼番号 : 002136 
#Region "復活処理"

    ''' <summary>
    ''' 復活処理(請求鑑ヘッダ、請求鑑詳復活)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function FukkatsuData(ByVal ds As DataSet) As DataSet

        '請求先マスタ存在チェックを行う
        ds = MyBase.CallBLC(Me._Blc, "ExistSeiqtoMChk", ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "FukkatsuData", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region
#End If

#Region "削除処理"

    ''' <summary>
    ''' 削除処理(請求鑑ヘッダ、請求鑑詳細論理削除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        '請求先マスタ存在チェックを行う
        ds = MyBase.CallBLC(Me._Blc, "ExistSeiqtoMChk", ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If
#If False Then      'DEL 2018/08/09 依頼番号 : 002136  
        '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add start
        '元請求番号存在チェックを行う
        ds = MyBase.CallBLC(Me._Blc, "ExistMotoSeiqNoChk", ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If
        '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add end
#End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "DeleteData", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#Region "ステージアップ処理"

    ''' <summary>
    ''' 請求開始日取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetInvFrom(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "GetInvFrom", ds)

    End Function

    ''' <summary>
    ''' 確定処理(請求鑑ヘッダ更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpKakuteiHed(ByVal ds As DataSet) As DataSet

        '請求先マスタ存在チェックを行う
        ds = MyBase.CallBLC(Me._Blc, "ExistSeiqtoMChk", ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpKakuteiHed", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' ステージアップ処理(請求鑑ヘッダ更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpStageKagamiHed(ByVal ds As DataSet) As DataSet

        '請求先マスタ存在チェックを行う
        ds = MyBase.CallBLC(Me._Blc, "ExistSeiqtoMChk", ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpStageKagamiHed", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

#Region "初期化処理(新黒データ新規登録)"

    ''' <summary>
    ''' 初期化処理(請求鑑ヘッダ、請求鑑詳細新規登録)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsCopyKuro(ByVal ds As DataSet) As DataSet

        '請求先マスタ存在チェックを行う
        ds = MyBase.CallBLC(Me._Blc, "ExistSeiqtoMChk", ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの論理削除
            ds = MyBase.CallBLC(Me._Blc, "DeleteData", ds)

            '排他チェックでNGの場合、処理終了
            If MyBase.IsMessageExist = True Then
                Return ds
            End If

            '新規登録内容を設定
            Call Me.InsertSetKuroData(ds)

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "InsertData", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録内容を設定する
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub InsertSetKuroData(ByVal ds As DataSet)

        Dim dtHed As DataTable = ds.Tables("LMG050HED")
        Dim dtDtl As DataTable = ds.Tables("LMG050DTL")

        '請求書番号を新規採番する
        Dim brCd As String = dtHed.Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility
        Dim skyuNo As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.SKYU_NO, Me, brCd)

        '新規登録内容を設定する
        With dtHed.Rows(0)
            .Item("SKYU_NO") = skyuNo
            .Item("STATE_KB") = "00"

            If String.IsNullOrEmpty(.Item("SKYU_NO_RELATED").ToString()) Then
                If .Item("UNCHIN_KB").ToString.Equals("00") Then
                    .Item("UNCHIN_IMP_FROM_DATE") = String.Empty
                End If
                If .Item("SAGYO_KB").ToString.Equals("00") Then
                    .Item("SAGYO_IMP_FROM_DATE") = String.Empty
                End If
                If .Item("YOKOMOCHI_KB").ToString.Equals("00") Then
                    .Item("YOKOMOCHI_IMP_FROM_DATE") = String.Empty
                End If
            End If

        End With

        Dim max As Integer = dtDtl.Rows.Count - 1
        For i As Integer = 0 To max
            With dtDtl.Rows(i)
                .Item("SKYU_NO") = skyuNo
            End With
        Next

    End Sub

#End Region

#Region "未来データ取得"

    ''' <summary>
    ''' 請求鑑ヘッダ検索処
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectNextData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectNextData", ds)

    End Function

#End Region

#Region "経理戻し処理(赤黒データ新規登録)"

    ''' <summary>
    ''' 経理戻し処理(請求鑑ヘッダ、請求鑑詳細新規登録)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertAkaKuro(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'システム共通項目の更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateSysData", ds)

            '排他チェックでNGの場合、処理終了
            If MyBase.IsMessageExist = True Then
                Return ds
            End If

            '新規登録内容を設定(赤データ)
            Call Me.InsertSetAkaData(ds)

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "InsertData", ds)

            '新規登録内容を設定(新黒データ)
            Call Me.InsertSetSinKuroData(ds)

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "InsertData", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録内容を設定する(赤データ))
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub InsertSetAkaData(ByVal ds As DataSet)

        Dim dtHed As DataTable = ds.Tables("LMG050HED")
        Dim dtDtl As DataTable = ds.Tables("LMG050DTL")

        '請求書番号を新規採番する
        Dim brCd As String = dtHed.Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility
        Dim skyuNo As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.SKYU_NO, Me, brCd)
        Dim motoSkyuNo As String = dtHed.Rows(0).Item("SKYU_NO").ToString()

        '新規登録内容を設定する
        With dtHed.Rows(0)
            .Item("SKYU_NO") = skyuNo
            .Item("STATE_KB") = "02"   '印刷済み
            .Item("NEBIKI_GK1") = (Convert.ToDecimal(.Item("NEBIKI_GK1")) * -1).ToString
            .Item("TAX_GK1") = (Convert.ToDecimal(.Item("TAX_GK1")) * -1).ToString
            .Item("TAX_HASU_GK1") = (Convert.ToDecimal(.Item("TAX_HASU_GK1")) * -1).ToString
            .Item("NEBIKI_GK2") = (Convert.ToDecimal(.Item("NEBIKI_GK2")) * -1).ToString
            .Item("SKYU_NO_RELATED") = motoSkyuNo
            .Item("RB_FLG") = "01"     '赤
        End With
        Dim max As Integer = dtDtl.Rows.Count - 1
        For i As Integer = 0 To max
            With dtDtl.Rows(i)
                .Item("SKYU_NO") = skyuNo
                .Item("KEISAN_TLGK") = (Convert.ToDecimal(.Item("KEISAN_TLGK")) * -1).ToString
                '★ ADD START 2011/09/06 SUGA
                .Item("NEBIKI_RTGK") = (Convert.ToDecimal(.Item("NEBIKI_RTGK")) * -1).ToString
                '★ ADD E N D 2011/09/06 SUGA
                .Item("NEBIKI_GK") = (Convert.ToDecimal(.Item("NEBIKI_GK")) * -1).ToString
            End With

        Next

    End Sub

    ''' <summary>
    ''' 新規登録内容を設定する(新黒データ))
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub InsertSetSinKuroData(ByVal ds As DataSet)

        Dim dtHed As DataTable = ds.Tables("LMG050HED")
        Dim dtDtl As DataTable = ds.Tables("LMG050DTL")

        '請求書番号を新規採番する
        Dim brCd As String = dtHed.Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility
        Dim skyuNo As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.SKYU_NO, Me, brCd)
        Dim motoSkyuNo As String = dtHed.Rows(0).Item("SKYU_NO").ToString()

        '新規登録内容を設定する
        With dtHed.Rows(0)
            .Item("SKYU_NO") = skyuNo
            .Item("STATE_KB") = "00"   '未確定
            .Item("NEBIKI_GK1") = (Convert.ToDecimal(.Item("NEBIKI_GK1")) * -1).ToString
            .Item("TAX_GK1") = (Convert.ToDecimal(.Item("TAX_GK1")) * -1).ToString
            .Item("TAX_HASU_GK1") = (Convert.ToDecimal(.Item("TAX_HASU_GK1")) * -1).ToString
            .Item("NEBIKI_GK2") = (Convert.ToDecimal(.Item("NEBIKI_GK2")) * -1).ToString
            .Item("SKYU_NO_RELATED") = motoSkyuNo
            .Item("RB_FLG") = "00"     '黒
            If .Item("CRT_FLG").Equals(LMConst.FLG.ON) Then
                .Item("CRT_KB") = "01"  '手書き請求書
            End If
        End With
        Dim max As Integer = dtDtl.Rows.Count - 1
        For i As Integer = 0 To max
            With dtDtl.Rows(i)
                .Item("SKYU_NO") = skyuNo
                .Item("KEISAN_TLGK") = (Convert.ToDecimal(.Item("KEISAN_TLGK")) * -1).ToString
                '★ ADD START 2011/09/06 SUGA
                .Item("NEBIKI_RTGK") = (Convert.ToDecimal(.Item("NEBIKI_RTGK")) * -1).ToString
                '★ ADD E N D 2011/09/06 SUGA
                .Item("NEBIKI_GK") = (Convert.ToDecimal(.Item("NEBIKI_GK")) * -1).ToString
            End With

        Next

    End Sub

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 保存処理(請求鑑ヘッダ、請求鑑詳細新規登録/更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SaveData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '請求書番号の取得
            If Me.SetSkyuNo(ds) = True Then

                '新規データチェックを行う
                ds = MyBase.CallBLC(Me._Blc, "ChkNewData", ds)

                'チェックでNGの場合、処理終了
                If MyBase.IsMessageExist = True Then
                    Return ds
                End If

                '新規登録処理を行う
                ds = MyBase.CallBLC(Me._Blc, "InsertData", ds)

            Else

                '請求マスタ存在チェックを行う
                ds = MyBase.CallBLC(Me._Blc, "ExistSeiqtoMChk", ds)

                'チェックでNGの場合、処理終了
                If MyBase.IsMessageExist = True Then
                    Return ds
                End If

                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateSaveData", ds)

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録処理の場合、請求書番号を採番する
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>TRUE:新規登録、FALSE:更新登録</returns>
    ''' <remarks></remarks>
    Private Function SetSkyuNo(ByVal ds As DataSet) As Boolean

        Dim dtHed As DataTable = ds.Tables("LMG050HED")
        Dim dtDtl As DataTable = ds.Tables("LMG050DTL")

        If String.IsNullOrEmpty(dtHed.Rows(0).Item("SKYU_NO").ToString()) = False Then
            Return False
        End If

        '請求書番号を新規採番する
        Dim brCd As String = dtHed.Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility
        Dim skyuNo As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.SKYU_NO, Me, brCd)

        '新規登録内容を設定する
        dtHed.Rows(0).Item("SKYU_NO") = skyuNo
        Dim max As Integer = dtDtl.Rows.Count - 1
        For i As Integer = 0 To max
            dtDtl.Rows(i).Item("SKYU_NO") = skyuNo
        Next

        Return True

    End Function

#End Region

#Region "印刷"

    ''' <summary>
    ''' 印刷処理(請求明細チェックリスト)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintCheck(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._Print530, "DoPrint", ds)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return ds

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理(請求明細)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintSeiqto(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        If ds.Tables("LMG520IN").Rows(0)("PRINT_SYUBETSU").ToString().Equals("03") = True Then
            '鏡印刷
            ds.Merge(New RdPrevInfoDS)
            '一括印刷
            ds = Me.PrintIkkatsu(ds)
        Else
            '鏡印刷
            ds.Merge(New RdPrevInfoDS)

            ds = MyBase.CallBLC(Me._Print520, "DoPrint", ds)

        End If
        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return ds

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 一括印刷
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintIkkatsu(ByVal ds As DataSet) As DataSet

        Dim tempDs As DataSet = Nothing

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        '初期値の退避
        Dim iniDs As DataSet = ds.Copy
        Dim iniDt As DataTable = iniDs.Tables("LMG520IN")
        Dim hokanDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone
        Dim unchinDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone
        Dim sagyoDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone
        ds.Merge(New RdPrevInfoDS)
        Dim msgExistsFlg As Boolean = False

        '正・副・控え・経理で４回くるくる
        For i As Integer = 0 To 3
            If Not (i = 0 AndAlso "1".Equals(iniDt.Rows(0)("SEI"))) _
              AndAlso Not (i = 1 AndAlso "1".Equals(iniDt.Rows(0)("HUKU"))) _
              AndAlso Not (i = 2 AndAlso "1".Equals(iniDt.Rows(0)("HIKAE"))) _
              AndAlso Not (i = 3 AndAlso "1".Equals(iniDt.Rows(0)("KEIRIHIKAE"))) Then
                Continue For
            End If
            '初期化
            ds.Tables("LMG520IN").Rows(0)("SEI") = "0"
            ds.Tables("LMG520IN").Rows(0)("HUKU") = "0"
            ds.Tables("LMG520IN").Rows(0)("HIKAE") = "0"
            ds.Tables("LMG520IN").Rows(0)("KEIRIHIKAE") = "0"

            Select Case i
                Case 0
                    ds.Tables("LMG520IN").Rows(0)("SEI") = "1"
                Case 1
                    ds.Tables("LMG520IN").Rows(0)("HUKU") = "1"
                Case 2
                    ds.Tables("LMG520IN").Rows(0)("HIKAE") = "1"
                Case 3
                    ds.Tables("LMG520IN").Rows(0)("KEIRIHIKAE") = "1"
            End Select
            '初期化
            ds.Tables("LMG520SET").Clear()
            ds.Tables("LMG520OUT").Clear()
            ds.Tables("M_RPT").Clear()
            ds.Tables(LMConst.RD).Clear()
            tempDs = MyBase.CallBLC(Me._Print520, "DoPrint", ds)
            rdPrevDt.Merge(tempDs.Tables(LMConst.RD))
            tempDs = Nothing
            'メッセージの初期化（後続処理に影響がでるため）
            msgExistsFlg = Me.IsMessageExisted(msgExistsFlg)
            MyBase.SetMessage(Nothing)

            'ADD 2017/05/31 一括印刷時で全自動の場合は、経理控えのときは明細を印刷しない(1987)
            Dim printFlg As String = LMConst.FLG.ON

            If i.Equals(3) = True Then
                Dim maxRow As Integer = ds.Tables("LMG520OUT").Rows.Count - 1
                If maxRow >= -1 Then
                    Dim outRow As DataRow = ds.Tables("LMG520OUT").Rows(maxRow)
                    Dim KAGAMI_CRT_NM As String = outRow.Item("KAGAMI_CRT_NM").ToString.Trim

                    If ("自動請求書").Equals(KAGAMI_CRT_NM.ToString.Trim) = True Then
                        '経理控えで全自動請求書は印字対象外
                        printFlg = LMConst.FLG.OFF
                    End If
                End If

            End If

            '2019/11/22 要望管理006013 add (経理控えなら 保管料荷役料請求明細書,運賃請求明細,作業料明細書 は出力しない)
            If i.Equals(3) = True Then
                printFlg = LMConst.FLG.OFF
            End If

            If printFlg.Equals(LMConst.FLG.ON) Then

                If "01".Equals(ds.Tables("LMG520IN").Rows(0)("PRINT_LMG500_FLG")) Then
                    If hokanDt.Rows.Count = 0 Then
                        tempDs = Me.PrintHokanNiyakuMeisai(ds.Tables("LMG520IN"))
                        If Not tempDs.Tables(LMConst.RD) Is Nothing Then
                            rdPrevDt.Merge(tempDs.Tables(LMConst.RD))
                            hokanDt.Merge(tempDs.Tables(LMConst.RD))
                        End If
                        'メッセージの初期化（後続処理に影響がでるため）
                        msgExistsFlg = Me.IsMessageExisted(msgExistsFlg)
                        MyBase.SetMessage(Nothing)
                    Else
                        rdPrevDt.Merge(hokanDt)
                    End If
                End If
                tempDs = Nothing
                If "01".Equals(ds.Tables("LMG520IN").Rows(0)("PRINT_LMF510_FLG")) Then
                    If unchinDt.Rows.Count = 0 Then
                        tempDs = Me.PrintUnchinMeisai(ds.Tables("LMG520IN"))
                        If Not tempDs.Tables(LMConst.RD) Is Nothing Then
                            rdPrevDt.Merge(tempDs.Tables(LMConst.RD))
                            unchinDt.Merge(tempDs.Tables(LMConst.RD))
                        End If
                        'メッセージの初期化（後続処理に影響がでるため）
                        msgExistsFlg = Me.IsMessageExisted(msgExistsFlg)
                        MyBase.SetMessage(Nothing)
                    Else
                        rdPrevDt.Merge(unchinDt)
                    End If
                End If
                tempDs = Nothing
                If "01".Equals(ds.Tables("LMG520IN").Rows(0)("PRINT_LME500_FLG")) Then
                    If sagyoDt.Rows.Count = 0 Then
                        tempDs = Me.PrintSagyoMeisai(ds.Tables("LMG520IN"))
                        If Not tempDs.Tables(LMConst.RD) Is Nothing Then
                            rdPrevDt.Merge(tempDs.Tables(LMConst.RD))
                            sagyoDt.Merge(tempDs.Tables(LMConst.RD))
                        End If
                        'メッセージの初期化（後続処理に影響がでるため）
                        msgExistsFlg = Me.IsMessageExisted(msgExistsFlg)
                        MyBase.SetMessage(Nothing)
                    Else
                        rdPrevDt.Merge(sagyoDt)
                    End If
                End If

            End If

        Next

        ds.Tables(LMConst.RD).Clear()
        ds.Tables(LMConst.RD).Merge(rdPrevDt)

        'エラーが存在した場合、メッセージを詰めなおす
        'メッセージの初期化（後続処理に影響がでるため）
        msgExistsFlg = Me.IsMessageExisted(msgExistsFlg)
        MyBase.SetMessage(Nothing)
        If msgExistsFlg = True Then
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 一度でもエラーが発生していたら、エラーを返す
    ''' </summary>
    ''' <param name="msgExistsFlg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsMessageExisted(ByVal msgExistsFlg As Boolean) As Boolean

        If msgExistsFlg = True Then Return msgExistsFlg

        If Me.IsMessageExist = True Then
            msgExistsFlg = True
        End If

        Return msgExistsFlg

    End Function

    ''' <summary>
    ''' 保管料荷役料明細書印刷
    ''' </summary>
    ''' <param name="inDt"></param>
    ''' <remarks></remarks>
    Private Function PrintHokanNiyakuMeisai(ByVal inDt As DataTable) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone
        Dim strJobList As String = String.Empty

        '2015.04.13 M_takahashi Add
        '一括印刷速度アップ修正
        'JOB_NOを取得する。
        Dim ds_job As DataSet = New LMG500DS
        Dim dt_job As DataTable = ds_job.Tables("LMG500IN")
        Dim dr_job As DataRow = dt_job.NewRow
        dr_job("SEKY_FLG") = "00" '本番
        dr_job("NRS_BR_CD") = inDt.Rows(0)("NRS_BR_CD")
        dr_job("SEIQTO_CD") = inDt.Rows(0)("SEIQTO_CD")
        dr_job("INV_DATE_TO") = inDt.Rows(0)("SEIQ_DATE_TO")

        dt_job.Rows.Add(dr_job)
        ds_job.Merge(New RdPrevInfoDS)

        ds_job = MyBase.CallBLC(New LMG500BLC(), "SelectJobNo", ds_job)

        For Each objJOB As DataRow In ds_job.Tables("LMG500OUT").Rows
            If strJobList.Equals(String.Empty) = True Then
                strJobList = objJOB("JOB_NO").ToString
            Else
                strJobList &= "," & objJOB("JOB_NO").ToString
            End If
        Next

        Dim ds As DataSet = New LMG500DS
        Dim dt As DataTable = ds.Tables("LMG500IN")
        Dim dr As DataRow = dt.NewRow
        dr("SEKY_FLG") = "00" '本番
        dr("NRS_BR_CD") = inDt.Rows(0)("NRS_BR_CD")
        dr("SEIQTO_CD") = inDt.Rows(0)("SEIQTO_CD")
        dr("INV_DATE_TO") = inDt.Rows(0)("SEIQ_DATE_TO")
        If strJobList.Equals(String.Empty) = False Then
            dr("JOB_NO") = strJobList
        End If
        dr("PREVIEW_FLG") = "1" 'プレビュー表示
        dt.Rows.Add(dr)

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(New LMG500BLC(), "DoPrint", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 運賃明細書印刷
    ''' </summary>
    ''' <param name="inDt"></param>
    ''' <remarks></remarks>
    Private Function PrintUnchinMeisai(ByVal inDt As DataTable) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        Dim ds As DataSet = New LMF510DS
        Dim dt As DataTable = ds.Tables("LMF510IN")
        Dim dr As DataRow = dt.NewRow

        ''データセットに格納
        dr("NRS_BR_CD") = inDt.Rows(0)("NRS_BR_CD")
        dr("SEIQ_CD") = inDt.Rows(0)("SEIQTO_CD")
        dr("F_DATE") = inDt.Rows(0)("SEIQ_DATE_FROM")
        dr("T_DATE") = inDt.Rows(0)("SEIQ_DATE_TO")
        dr("UNTIN_CALCULATION_KB") = inDt.Rows(0)("UNTIN_CALCULATION_KB")
        dt.Rows.Add(dr)

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(New LMF510BLC(), "DoPrint", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 作業料明細書印刷
    ''' </summary>
    ''' <param name="inDt"></param>
    ''' <remarks></remarks>
    Private Function PrintSagyoMeisai(ByVal inDt As DataTable) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        Dim ds As DataSet = New LME500DS
        Dim dt As DataTable = ds.Tables("LME500IN")
        Dim dr As DataRow = dt.NewRow

        ''データセットに格納
        dr.Item("NRS_BR_CD") = inDt.Rows(0)("NRS_BR_CD")
        dr.Item("SEIQTO_CD") = inDt.Rows(0)("SEIQTO_CD")
        dr.Item("F_DATE") = inDt.Rows(0)("SEIQ_DATE_FROM")
        dr.Item("T_DATE") = inDt.Rows(0)("SEIQ_DATE_TO")
        dr.Item("PRT_SHUBETU") = "01"
        dt.Rows.Add(dr)

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(New LME500BLC(), "DoPrint", ds)

        Return ds

    End Function

#End Region

#Region "SAP出力処理"

    ''' <summary>
    ''' SAP出力処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SapOut(ByVal ds As DataSet) As DataSet

        ' トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ' SAP出力処理（BLC）
            ds = MyBase.CallBLC(Me._Blc, "SapOut", ds)

            ' エラーありの場合、処理終了
            If MyBase.IsMessageExist = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

#Region "SAP取消処理"

    ''' <summary>
    ''' SAP取消処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SapCancel(ByVal ds As DataSet) As DataSet

        ' トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ' SAP取消処理（BLC）
            ds = MyBase.CallBLC(Me._Blc, "SapCancel", ds)

            ' エラーありの場合、処理終了
            If MyBase.IsMessageExist = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

#Region "ComboBox"

    ''' <summary>
    ''' 製品セグメント取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Function SelectComboSeihin(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectComboSeihin", ds)

    End Function

    ''' <summary>
    ''' 地域セグメント取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Function SelectComboChiiki(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectComboChiiki", ds)

    End Function

    ''' <summary>
    ''' セグメント初期値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>行追加時の初期値として利用</remarks>
    Private Function SelectDefSeg(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectDefSeg", ds)

    End Function

#End Region

#End Region

End Class