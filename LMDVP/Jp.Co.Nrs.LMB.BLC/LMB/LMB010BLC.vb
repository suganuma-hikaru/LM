' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB010    : 入荷データ検索
'  作  成  者       :  [金ヘスル]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMB010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMB010DAC = New LMB010DAC()

    '2017/09/25 修正 李↓
    ''20151106 tsunehira add
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = MessageManager.MessageLanguage
    '2017/09/25 修正 李↑

#End Region

#Region "Const"

    '2015.10.20 tusnehira add
    '英語化対応
    Private Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Private Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 初期出荷マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()

        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 初期出荷マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

    'START YANAI メモ②No.28
    ''' <summary>
    ''' EDIチェック対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListEdiData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectListEdiData1", ds)
        ds = MyBase.CallDAC(Me._Dac, "SelectListEdiData2", ds)
        ds = MyBase.CallDAC(Me._Dac, "SelectListInkaSData", ds)
        Return ds

    End Function
    'END YANAI メモ②No.28

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 初期出荷マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistShokiShukkaM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistShokiShukkaM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 初期出荷マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaShokiShukkaM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectShokiShukkaM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '0件の場合、論理排他メッセージを設定する
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 初期出荷マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertShokiShukkaM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertShokiShukkaM", ds)

    End Function

    ''' <summary>
    ''' 初期出荷マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateShokiShukkaM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateShokiShukkaM", ds)

    End Function

#End Region

#Region "新規登録"

    'START YANAI メモ②No.28
    ''' <summary>
    ''' EDI入荷データ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertDataEDI(ByVal ds As DataSet) As DataSet

        'EDI入荷管理番号採番
        Dim ediCtlNo As String = Me.GetEdiCtlNo(ds)
        Dim value As String() = New String() {ediCtlNo}
        ds.Tables("LMB010IN_EDI").Rows(0).Item("EDI_CTL_NO") = value(0)

        'ＥＤＩデュポンＨＥＤ
        Dim rtnResult As Boolean = Me.InsertHInkaEdiHedDpnData(ds)

        'ＥＤＩデュポンＤＴＬ
        rtnResult = rtnResult AndAlso Me.InsertHInkaEdiDtlDpnData(ds)

        'H_INKAEDI_L
        rtnResult = rtnResult AndAlso Me.InsertHInkaEdiLData(ds)

        'H_INKAEDI_M
        rtnResult = rtnResult AndAlso Me.InsertHInkaEdiMData(ds)

        If rtnResult = False Then
            '英語化対応
            '20151021 tsunehira add
            MyBase.SetMessageStore("00", "E643")
            'MyBase.SetMessageStore("00", "E305", New String() {"EDI入荷データ", String.Empty})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' EDI_CTL_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>OutkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetEdiCtlNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_INKA_NO_L, Me, ds.Tables("LMB010IN_EDI").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' ＥＤＩデュポンＨＥＤ新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertHInkaEdiHedDpnData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertHInkaEdiHedDpnData")

    End Function

    ''' <summary>
    ''' ＥＤＩデュポンＤＴＬ新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertHInkaEdiDtlDpnData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertHInkaEdiDtlDpnData")

    End Function

    ''' <summary>
    ''' H_INKAEDI_L新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertHInkaEdiLData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertHInkaEdiLData")

    End Function

    ''' <summary>
    ''' H_INKAEDI_M新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertHInkaEdiMData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertHInkaEdiMData")

    End Function

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._Dac, actionId, ds)

    End Function
    'EMD YANAI メモ②No.28

#End Region

#Region "作業一括作成処理"
    'START YANAI 20120121 作業一括処理対応
    ''' <summary>
    ''' 作業一括作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSagyo(ByVal ds As DataSet) As DataSet

        Dim rtnFlg As Boolean = True
        'Dim dr As DataRow = Nothing

        'メッセージのクリア
        MyBase.SetMessage(Nothing)

        '作成対象荷主チェック
        rtnFlg = Me.custDetailsCheck(ds)
        If rtnFlg = False Then
            Return ds
        End If

        '作成用の作業レコードの取得
        ds = MyBase.CallDAC(Me._Dac, "SelectMakeSagyo", ds)

        Dim max As Integer = ds.Tables("LMB010OUT_SAGYO").Rows.Count - 1
        'Dim max2 As Integer = 4
        Dim inkaNoL As String = String.Empty
        Dim inkaNoM As String = String.Empty
        'Dim sagyoCd As String = String.Empty
        Dim sagyoNb As Decimal = 0
        Dim rowNo As Integer = -1
        For i As Integer = 0 To max

            If (String.IsNullOrEmpty(inkaNoL) = False AndAlso _
                ((inkaNoL).Equals(ds.Tables("LMB010OUT_SAGYO").Rows(i).Item("INKA_NO_L").ToString) = False OrElse _
                (inkaNoM).Equals(ds.Tables("LMB010OUT_SAGYO").Rows(i).Item("INKA_NO_M").ToString) = False)) Then
                '出荷管理番号が変わった場合は作業作成処理を行う

                '保存処理
                Call Me.InsertSagyoSub(ds, sagyoNb, rowNo)

                rowNo = -1
                sagyoNb = 0
                inkaNoL = String.Empty
                inkaNoM = String.Empty
            End If

            '同じ出荷管理番号のデータの今回請求数のまとめ処理
            sagyoNb = Convert.ToDecimal( _
                                        sagyoNb + _
                                        Convert.ToDecimal(ds.Tables("LMB010OUT_SAGYO").Rows(i).Item("SAGYO_NB").ToString) _
                                      )
            If rowNo = -1 Then
                rowNo = i
                inkaNoL = ds.Tables("LMB010OUT_SAGYO").Rows(i).Item("INKA_NO_L").ToString
                inkaNoM = ds.Tables("LMB010OUT_SAGYO").Rows(i).Item("INKA_NO_M").ToString
            End If

        Next

        '最後の保存対象レコードが必ずあるので、ここで保存処理を行う
        If 0 <= max Then
            '保存処理
            Call Me.InsertSagyoSub(ds, sagyoNb, rowNo)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 作業一括作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub InsertSagyoSub(ByVal ds As DataSet, ByVal sagyoNb As Decimal, ByVal rowNo As Integer)

        Dim dr As DataRow = Nothing
        Dim sagyoCd As String = String.Empty
        Dim max2 As Integer = 4

        For j As Integer = 0 To max2
            Select Case j
                Case 0
                    sagyoCd = ds.Tables("LMB010OUT_SAGYO").Rows(rowNo).Item("SAGYO_CD1").ToString
                Case 1
                    sagyoCd = ds.Tables("LMB010OUT_SAGYO").Rows(rowNo).Item("SAGYO_CD2").ToString
                Case 2
                    sagyoCd = ds.Tables("LMB010OUT_SAGYO").Rows(rowNo).Item("SAGYO_CD3").ToString
                Case 3
                    sagyoCd = ds.Tables("LMB010OUT_SAGYO").Rows(rowNo).Item("SAGYO_CD4").ToString
                Case 4
                    sagyoCd = ds.Tables("LMB010OUT_SAGYO").Rows(rowNo).Item("SAGYO_CD5").ToString
            End Select

            If String.IsNullOrEmpty(sagyoCd) = False Then
                ds.Tables("LMB010IN_SAGYOMST").Clear()
                ds.Tables("LMB010OUT_SAGYOMST").Clear()

                dr = ds.Tables("LMB010IN_SAGYOMST").NewRow()
                With dr

                    .Item("NRS_BR_CD") = ds.Tables("LMB010OUT_SAGYO").Rows(rowNo).Item("NRS_BR_CD").ToString
                    .Item("SAGYO_CD") = sagyoCd

                End With
                ds.Tables("LMB010IN_SAGYOMST").Rows.Add(dr)

                '作業マスタの取得
                ds = MyBase.CallDAC(Me._Dac, "SelectSagyoMst", ds)
                If 0 < ds.Tables("LMB010OUT_SAGYOMST").Rows.Count Then
                    '作業マスタを元に作業レコードを再設定し、作業レコードの更新を作成を行う
                    ds = Me.InsertSagyoRec(ds, sagyoCd, Convert.ToString(sagyoNb), rowNo)
                End If

            End If

        Next

    End Sub

    ''' <summary>
    ''' 作成対象荷主チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function custDetailsCheck(ByVal ds As DataSet) As Boolean

        '2017/09/25 修正 李↓

        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        '荷主明細マスタの取得
        ds = MyBase.CallDAC(Me._Dac, "SelectCustDetails", ds)

        If ds.Tables("LMB010OUT_CHECK").Rows.Count = 0 Then
            '荷主明細マスタにデータがないのでエラー
            MyBase.SetMessage("E209") 'BLF判定用のダミー

            MyBase.SetMessageStore("00", "E209", New String() {String.Concat(
                                   lgm.Selector({"荷主=", "Custmer=", "하주=", "中国語荷主="}),
                                   ds.Tables("LMB010IN_SAGYO").Rows(0).Item("CUST_CD_L").ToString)}, ds.Tables("LMB010IN_SAGYO").Rows(0).Item("ROW_NO").ToString)

            '20151029 tsunehira add End

            Return False
        End If


        '作業レコードの取得
        ds = MyBase.CallDAC(Me._Dac, "SelectSagyoData", ds)

        If ds.Tables("LMB010OUT_CHECK").Rows.Count <> 0 Then

            '20151020 tsunehira add
                    '荷主明細マスタにデータがないのでエラー
            MyBase.SetMessage("E432") 'BLF判定用のダミー

            MyBase.SetMessageStore("00", "E432", New String() {String.Concat(
                                   lgm.Selector({"荷主=", "Custmer=", "하주=", "中国語荷主="}),
                                   ds.Tables("LMB010IN_SAGYO").Rows(0).Item("CUST_CD_L").ToString)}, ds.Tables("LMB010IN_SAGYO").Rows(0).Item("ROW_NO").ToString)


            '20151029 tsunehira add End
        End If

        '2017/09/25 修正 李↑

        Return True

    End Function

    ''' <summary>
    ''' 作業レコードの作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertSagyoRec(ByVal ds As DataSet, ByVal sagyoCd As String, ByVal sagyoNb As String, ByVal rowNo As Integer) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        '値のクリア
        inTbl = setDs.Tables("LMB010OUT_SAGYO")
        inTbl.Clear()
        inTbl.ImportRow(ds.Tables("LMB010OUT_SAGYO").Rows(rowNo))

        '作業レコード番号採番
        Dim sagyoRecNo As String = Me.GetSagyoRecNo(ds)
        Dim value As String() = New String() {sagyoRecNo}
        inTbl.Rows(0).Item("SAGYO_REC_NO") = value(0)

        '作業名
        inTbl.Rows(0).Item("SAGYO_CD1") = sagyoCd

        '作業名
        Dim sagyoNm As String = ds.Tables("LMB010OUT_SAGYOMST").Rows(0).Item("SAGYO_NM").ToString
        inTbl.Rows(0).Item("SAGYO_NM") = sagyoNm

        '請求単位
        Dim invTani As String = ds.Tables("LMB010OUT_SAGYOMST").Rows(0).Item("INV_TANI").ToString
        inTbl.Rows(0).Item("INV_TANI") = invTani

        '今回請求数
        If ("01").Equals(ds.Tables("LMB010OUT_SAGYOMST").Rows(0).Item("KOSU_BAI").ToString) = True Then
            inTbl.Rows(0).Item("SAGYO_NB") = "1"
        Else
            inTbl.Rows(0).Item("SAGYO_NB") = sagyoNb
        End If

        '単価
        Dim sagyoUp As String = ds.Tables("LMB010OUT_SAGYOMST").Rows(0).Item("SAGYO_UP").ToString
        inTbl.Rows(0).Item("SAGYO_UP") = sagyoUp

        '作業金額
        Dim sagyoNb2 As String = inTbl.Rows(0).Item("SAGYO_NB").ToString
        Dim sagyoGk As String = Convert.ToString(Math.Round((Convert.ToDecimal(sagyoUp) * Convert.ToDecimal(sagyoNb2)), MidpointRounding.AwayFromZero))
        inTbl.Rows(0).Item("SAGYO_GK") = sagyoGk

        '課税区分
        inTbl.Rows(0).Item("TAX_KB") = ds.Tables("LMB010OUT_SAGYOMST").Rows(0).Item("TAX_KB").ToString


        '作業レコードの作成
        rtnDs = MyBase.CallDAC(Me._Dac, "InsertSagyoRec", setDs)

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSagyo(ByVal ds As DataSet) As DataSet

        '作業レコードの物理削除
        ds = MyBase.CallDAC(Me._Dac, "DeleteSagyo", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 作業明細番号を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>OutkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetSagyoRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, ds.Tables("LMB010IN_SAGYO").Rows(0).Item("NRS_BR_CD").ToString())

    End Function
    'END YANAI 20120121 作業一括処理対応
#End Region

#Region "出荷データ作成処理"
    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）
    ''' <summary>
    ''' 出荷データ作成処理（メイン）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMakeOutkaData(ByVal ds As DataSet) As DataSet

        '2017/09/25 修正 李↓

        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        'UTI追加修正 yamanaka 2012.12.22 Start
        Dim rtnDs As DataSet = Nothing

        '出荷データ作成用データの検索(カウント)
        If ds.Tables("LMB010IN_OUTKA").Rows(0).Item("JIKKO_FLG").ToString().Equals("1") = True Then
            ds = MyBase.CallDAC(Me._Dac, "SelectUtiOutkaDataCount", ds)
        Else
            ds = MyBase.CallDAC(Me._Dac, "SelectOutkaDataCount", ds)
        End If

        'Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SelectOutkaDataCount", ds)
        'UTI追加修正 yamanaka 2012.12.22 End

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count = 0 Then

            'UTI追加修正 yamanaka 2012.12.22 Start
            If ds.Tables("LMB010IN_OUTKA").Rows(0).Item("JIKKO_FLG").ToString().Equals("1") = True _
            AndAlso ds.Tables("LMB010IN_OUTKA").Rows(0).Item("OLD_INKA_STATE_KB").ToString().Equals("50") = False Then
                '20151020 tsunehira add
                MyBase.SetMessageStore("00", "E619", , ds.Tables("LMB010IN_OUTKA").Rows(0).Item("ROW_NO").ToString)
                MyBase.SetMessage("E619")

                'MyBase.SetMessageStore("00", "E530", New String() {"出荷済", "出荷データ作成"}, ds.Tables("LMB010IN_OUTKA").Rows(0).Item("ROW_NO").ToString)
                'MyBase.SetMessage("E530", New String() {"出荷済", "出荷データ作成"})
                Return ds
            End If
            'UTI追加修正 yamanaka 2012.12.22 End

            '引当可能な出荷データがない場合
            MyBase.SetMessageStore("00", "E486", New String() {String.Empty}, ds.Tables("LMB010IN_OUTKA").Rows(0).Item("ROW_NO").ToString)
            MyBase.SetMessage("E486")
            Return ds
        End If

        'UTI修正 yamanaka 2012.12.07 Start
        '出荷データ作成用データの検索
        If ds.Tables("LMB010IN_OUTKA").Rows(0).Item("JIKKO_FLG").ToString().Equals("1") = True Then
            ds = MyBase.CallDAC(Me._Dac, "SelectUtiOutkaData", ds)
        Else
            ds = MyBase.CallDAC(Me._Dac, "SelectOutkaData", ds)
        End If

        'ds = MyBase.CallDAC(Me._Dac, "SelectOutkaData", ds)
        'UTI修正 yamanaka 2012.12.07 End

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ作成処理（メイン）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function MakeOutkaData(ByVal ds As DataSet) As DataSet

        '出荷データ作成(東レUTI専用)対応 yamanaka 2012.11.28 Start
        '出荷・運送のデータセットを設定
        If ds.Tables("LMB010IN_OUTKA").Rows(0).Item("JIKKO_FLG").ToString().Equals("1") = True Then
            ds = Me.SetUtiOutkaData(ds)
        Else
            ds = Me.SetOutkaData(ds)
        End If
        'ds = Me.SetOutkaData(ds)
        '出荷データ作成(東レUTI専用)対応 yamanaka 2012.11.28 End

        '出荷データ(大)の更新
        Dim rtnResult As Boolean = Me.InsertOutkaL(ds)
        If rtnResult = False Then
            '英語化対応
            '20151021 tsunehira add
            MyBase.SetMessageStore("00", "E646")
            MyBase.SetMessage("E646")
            'MyBase.SetMessageStore("00", "E305", New String() {"出荷データ(大)", String.Empty})
            'MyBase.SetMessage("E305")
            Return ds
        End If

        '出荷データ(中)の更新
        rtnResult = Me.InsertOutkaM(ds)
        If rtnResult = False Then
            '英語化対応
            '20151021 tsunehira add
            MyBase.SetMessageStore("00", "E647")
            MyBase.SetMessage("E647")
            'MyBase.SetMessageStore("00", "E305", New String() {"出荷データ(中)", String.Empty})
            'MyBase.SetMessage("E305")
            Return ds
        End If

        '運送データ(大)の更新
        rtnResult = Me.InsertUnsoL(ds)
        If rtnResult = False Then
            '英語化対応
            '20151021 tsunehira add
            MyBase.SetMessageStore("00", "E644")
            MyBase.SetMessage("E644")
            'MyBase.SetMessageStore("00", "E305", New String() {"運送データ(大)", String.Empty})
            'MyBase.SetMessage("E305")
            Return ds
        End If

        '運送データ(中)の更新
        rtnResult = Me.InsertUnsoM(ds)
        If rtnResult = False Then
            '英語化対応
            '20151021 tsunehira add
            MyBase.SetMessageStore("00", "E645")
            MyBase.SetMessage("E645")
            'MyBase.SetMessageStore("00", "E305", New String() {"運送データ(中)", String.Empty})
            'MyBase.SetMessage("E305")
            Return ds
        End If

        '2014.05.16 追加START
        If ds.Tables("LMB010IN_OUTKA_PICK_WK").Rows.Count > 0 Then
            '出荷ピックWKの更新
            Me.CallDAC(Me._Dac, "InsertCOutkaPickWk", ds)
            If rtnResult = False Then
                '英語化対応
                '20151021 tsunehira add
                MyBase.SetMessageStore("00", "E648")
                MyBase.SetMessage("E648")
                'MyBase.SetMessageStore("00", "E305", New String() {"出荷ピックWK", String.Empty})
                'MyBase.SetMessage("E305")
                Return ds
            End If
        End If
        '2014.05.16 追加END

        Return ds

    End Function

    'UTI追加修正 yamanaka 2012.12.21 Start
    ''' <summary>
    ''' 出荷データ作成処理（更新）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpMakeOutkaData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpMakeOutkaData", ds)

    End Function
    'UTI追加修正 yamanaka 2012.12.21 End

    ''' <summary>
    ''' 出荷・運送のデータセットを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetOutkaData(ByVal ds As DataSet) As DataSet

        ds.Tables("LMB010IN_OUTKAL").Clear()
        ds.Tables("LMB010IN_OUTKAM").Clear()
        ds.Tables("LMB010IN_UNSOL").Clear()
        ds.Tables("LMB010IN_UNSOM").Clear()
        Dim outkaLdr As DataRow = Nothing
        Dim outkaMdr As DataRow = Nothing
        Dim unsoLdr As DataRow = Nothing
        Dim unsoMdr As DataRow = Nothing
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim serialNo As String = String.Empty
        Dim goodsCdNrs As String = String.Empty
        Dim rsvNo As String = String.Empty
        Dim lotNo As String = String.Empty
        Dim irime As String = String.Empty

        '2014.05.16 追加START
        ds.Tables("LMB010IN_OUTKA_PICK_WK").Clear()
        Dim outkaPickWkdr As DataRow = Nothing
        '2014.05.16 追加END

        Dim outkaNoL As String = String.Empty
        Dim outkaNoM As Decimal = 0
        Dim unsoNoL As String = String.Empty
        Dim outkaPkgNb As Decimal = 0
        Dim allocCanNb As Decimal = 0
        Dim allocCanQt As Decimal = 0
        Dim pkgNb As Decimal = 0
        Dim outkaMpkgNb As Decimal = 0
        Dim amari As Decimal = 0
        Dim outkaLflg As Boolean = True
        Dim outkaMflg As Boolean = True

        '2014.05.16 修正START
        Dim outkaDr() As DataRow = ds.Tables("LMB010OUT_OUTKA").Select(Nothing, "NRS_BR_CD, CUST_CD_L, CUST_CD_M, GOODS_CD_NRS, SERIAL_NO, RSV_NO, LOT_NO, IRIME")
        'Dim outkaDr() As DataRow = ds.Tables("LMB010OUT_OUTKA").Select(Nothing, "NRS_BR_CD, CUST_CD_L, CUST_CD_M, SERIAL_NO, GOODS_CD_NRS, RSV_NO, LOT_NO, IRIME")
        '2014.05.16 修正END
        Dim max As Integer = outkaDr.Length - 1

        ' シリアルNo分割可否結果保持用
        Dim separateCheckList As New Dictionary(Of String, Boolean)

        For i As Integer = 0 To max

            outkaLflg = False
            outkaMflg = False

            'WIT対応 昭和電工出荷データ作成処理対応 kasama Start
            ' シリアルNoで出荷(大)を分割するかどうかを判定
            Dim checkKey As String = outkaDr(i).Item("NRS_BR_CD").ToString + outkaDr(i).Item("CUST_CD_L").ToString
            Dim canSeparate As Boolean

            ' シリアルNo分割可否結果の取得
            If separateCheckList.TryGetValue(checkKey, canSeparate) = False Then
                ' 結果が存在しない場合のみ荷主明細問い合わせ
                canSeparate = Me.CanSeparateBySerialNo(outkaDr(i).Item("NRS_BR_CD").ToString, outkaDr(i).Item("CUST_CD_L").ToString)
                separateCheckList.Add(checkKey, canSeparate)
            End If
            'WIT対応 昭和電工出荷データ作成処理対応 kasama End

            ' 分割する場合のみシリアルNoを比較
            If (custCdL).Equals(outkaDr(i).Item("CUST_CD_L").ToString) = False OrElse _
                (custCdM).Equals(outkaDr(i).Item("CUST_CD_M").ToString) = False OrElse _
                (canSeparate AndAlso (serialNo).Equals(outkaDr(i).Item("SERIAL_NO").ToString) = False) OrElse _
                outkaNoM = 999 Then
                '出荷(大)・運送(大)の切り替え判定
                outkaLflg = True
                outkaMflg = True
            End If

            If (goodsCdNrs).Equals(outkaDr(i).Item("GOODS_CD_NRS").ToString) = False OrElse _
                (rsvNo).Equals(outkaDr(i).Item("RSV_NO").ToString) = False OrElse _
                (lotNo).Equals(outkaDr(i).Item("LOT_NO").ToString) = False OrElse _
                (irime).Equals(outkaDr(i).Item("IRIME").ToString) = False Then
                '出荷(中)・運送(中)の切り替え判定
                outkaMflg = True
            End If

            If outkaMflg = True Then
                '出荷(中)・運送(中)の切り替え
                If i > 0 Then
                    '■出荷(中)
                    outkaMdr.Item("OUTKA_TTL_NB") = allocCanNb
                    outkaMdr.Item("OUTKA_TTL_QT") = allocCanQt
                    outkaMdr.Item("BACKLOG_NB") = allocCanNb
                    outkaMdr.Item("BACKLOG_QT") = allocCanQt
                    outkaMpkgNb = Math.Floor(CalcData(allocCanNb, pkgNb)) '割った時の整数部分を求める
                    amari = CalcDataMod(allocCanNb, pkgNb) '割った時の余り部分を求める
                    If 0 < amari Then
                        outkaMpkgNb = outkaMpkgNb + 1
                    End If
                    outkaMdr.Item("OUTKA_M_PKG_NB") = outkaMpkgNb
                    outkaPkgNb = outkaPkgNb + outkaMpkgNb
                    ds.Tables("LMB010IN_OUTKAM").Rows.Add(outkaMdr) 'データセットに設定(出荷(中))

                    '■運送(中)
                    unsoMdr.Item("UNSO_TTL_NB") = allocCanNb
                    unsoMdr.Item("UNSO_TTL_QT") = allocCanQt
                    ds.Tables("LMB010IN_UNSOM").Rows.Add(unsoMdr) 'データセットに設定(運送(中))
                End If

                outkaMdr = ds.Tables("LMB010IN_OUTKAM").NewRow()
                unsoMdr = ds.Tables("LMB010IN_UNSOM").NewRow()
                outkaNoM = outkaNoM + 1 '出荷管理番号(中)の採番(運送(中)の管理番号も兼ねている)
                '初期化
                allocCanNb = 0
                allocCanQt = 0
                pkgNb = Convert.ToDecimal(outkaDr(i).Item("PKG_NB").ToString)
                '切り替え判定値の保持
                goodsCdNrs = outkaDr(i).Item("GOODS_CD_NRS").ToString
                rsvNo = outkaDr(i).Item("RSV_NO").ToString
                lotNo = outkaDr(i).Item("LOT_NO").ToString
                irime = outkaDr(i).Item("IRIME").ToString
            End If

            If outkaLflg = True Then
                '出荷(大)・運送(大)の切り替え
                If i > 0 Then
                    '■出荷(大)
                    outkaLdr.Item("OUTKA_PKG_NB") = outkaPkgNb
                    ds.Tables("LMB010IN_OUTKAL").Rows.Add(outkaLdr) 'データセットに設定(出荷(大))

                    '■運送(大)
                    unsoLdr.Item("UNSO_PKG_NB") = outkaPkgNb
                    ds.Tables("LMB010IN_UNSOL").Rows.Add(unsoLdr) 'データセットに設定(運送(大))
                End If

                outkaLdr = ds.Tables("LMB010IN_OUTKAL").NewRow()
                unsoLdr = ds.Tables("LMB010IN_UNSOL").NewRow()
                outkaNoL = Me.GetOutkaNoL(ds) '出荷管理番号(大)の採番
                unsoNoL = Me.GetUnsoNoL(ds) '運送番号(大)の採番
                '初期化
                outkaNoM = 1
                outkaPkgNb = 0
                '切り替え判定値の保持
                custCdL = outkaDr(i).Item("CUST_CD_L").ToString
                custCdM = outkaDr(i).Item("CUST_CD_M").ToString
                serialNo = outkaDr(i).Item("SERIAL_NO").ToString
            End If

            '■出荷(大)
            If String.IsNullOrEmpty(outkaLdr.Item("NRS_BR_CD").ToString) = True Then
                outkaLdr.Item("NRS_BR_CD") = outkaDr(i).Item("NRS_BR_CD").ToString
                outkaLdr.Item("OUTKA_NO_L") = outkaNoL
                outkaLdr.Item("FURI_NO") = String.Empty
                outkaLdr.Item("OUTKA_KB") = "10"
                outkaLdr.Item("SYUBETU_KB") = "10"
                outkaLdr.Item("OUTKA_STATE_KB") = "10"
                outkaLdr.Item("OUTKAHOKOKU_YN") = String.Empty
                outkaLdr.Item("PICK_KB") = "01"
                outkaLdr.Item("DENP_NO") = String.Empty
                outkaLdr.Item("ARR_KANRYO_INFO") = String.Empty
                outkaLdr.Item("WH_CD") = outkaDr(i).Item("WH_CD").ToString
                outkaLdr.Item("OUTKA_PLAN_DATE") = outkaDr(i).Item("OUTKA_DATE_INIT").ToString
                outkaLdr.Item("OUTKO_DATE") = outkaDr(i).Item("OUTKA_DATE_INIT").ToString
                outkaLdr.Item("ARR_PLAN_DATE") = outkaDr(i).Item("OUTKA_DATE_INIT").ToString
                outkaLdr.Item("ARR_PLAN_TIME") = String.Empty
                outkaLdr.Item("HOKOKU_DATE") = String.Empty
                outkaLdr.Item("TOUKI_HOKAN_YN") = "01"
                outkaLdr.Item("END_DATE") = String.Empty
                outkaLdr.Item("CUST_CD_L") = outkaDr(i).Item("CUST_CD_L").ToString
                outkaLdr.Item("CUST_CD_M") = outkaDr(i).Item("CUST_CD_M").ToString
                outkaLdr.Item("SHIP_CD_L") = String.Empty
                outkaLdr.Item("SHIP_CD_M") = String.Empty
                outkaLdr.Item("DEST_CD") = String.Empty
                outkaLdr.Item("DEST_AD_3") = String.Empty
                outkaLdr.Item("DEST_TEL") = String.Empty
                outkaLdr.Item("NHS_REMARK") = String.Empty
                outkaLdr.Item("SP_NHS_KB") = String.Empty
                outkaLdr.Item("COA_YN") = String.Empty
                outkaLdr.Item("CUST_ORD_NO") = String.Empty
                outkaLdr.Item("BUYER_ORD_NO") = String.Empty
                outkaLdr.Item("REMARK") = String.Empty
                outkaLdr.Item("DENP_YN") = "00"
                outkaLdr.Item("PC_KB") = "01"
                outkaLdr.Item("NIYAKU_YN") = "01"
                outkaLdr.Item("DEST_KB") = "01"
                outkaLdr.Item("DEST_NM") = String.Empty
                outkaLdr.Item("DEST_AD_1") = String.Empty
                outkaLdr.Item("DEST_AD_2") = String.Empty
                outkaLdr.Item("ALL_PRINT_FLAG") = "00"
                outkaLdr.Item("NIHUDA_FLAG") = "00"
                outkaLdr.Item("NHS_FLAG") = "00"
                outkaLdr.Item("DENP_FLAG") = "00"
                outkaLdr.Item("COA_FLAG") = "00"
                outkaLdr.Item("HOKOKU_FLAG") = "00"
                outkaLdr.Item("MATOME_PICK_FLAG") = String.Empty
                outkaLdr.Item("LAST_PRINT_DATE") = String.Empty
                outkaLdr.Item("LAST_PRINT_TIME") = String.Empty
                outkaLdr.Item("SASZ_USER") = String.Empty
                outkaLdr.Item("OUTKO_USER") = String.Empty
                outkaLdr.Item("KEN_USER") = String.Empty
                outkaLdr.Item("OUTKA_USER") = String.Empty
                outkaLdr.Item("HOU_USER") = String.Empty
                outkaLdr.Item("ORDER_TYPE") = String.Empty
            End If

            '■出荷(中)
            If String.IsNullOrEmpty(outkaMdr.Item("NRS_BR_CD").ToString) = True Then
                outkaMdr.Item("NRS_BR_CD") = outkaDr(i).Item("NRS_BR_CD").ToString
                outkaMdr.Item("OUTKA_NO_L") = outkaNoL
                outkaMdr.Item("OUTKA_NO_M") = Me.MaeCoverData(Convert.ToString(outkaNoM), "0", 3)
                outkaMdr.Item("EDI_SET_NO") = String.Empty
                outkaMdr.Item("COA_YN") = "00"
                outkaMdr.Item("CUST_ORD_NO_DTL") = String.Empty
                outkaMdr.Item("BUYER_ORD_NO_DTL") = String.Empty
                outkaMdr.Item("GOODS_CD_NRS") = outkaDr(i).Item("GOODS_CD_NRS").ToString
                outkaMdr.Item("RSV_NO") = outkaDr(i).Item("RSV_NO").ToString
                outkaMdr.Item("LOT_NO") = outkaDr(i).Item("LOT_NO").ToString
                '2014.05.16 修正START
                If canSeparate = False Then
                    outkaMdr.Item("SERIAL_NO") = String.Empty
                Else
                    outkaMdr.Item("SERIAL_NO") = outkaDr(i).Item("SERIAL_NO").ToString
                End If
                'outkaMdr.Item("SERIAL_NO") = outkaDr(i).Item("SERIAL_NO").ToString
                '2014.05.16 修正END
                outkaMdr.Item("ALCTD_KB") = "01"
                outkaMdr.Item("OUTKA_PKG_NB") = outkaDr(i).Item("PKG_NB").ToString
                outkaMdr.Item("OUTKA_HASU") = "0"
                outkaMdr.Item("OUTKA_QT") = "0"
                outkaMdr.Item("ALCTD_NB") = "0"
                outkaMdr.Item("ALCTD_QT") = "0"
                outkaMdr.Item("UNSO_ONDO_KB") = "90"
                outkaMdr.Item("IRIME") = outkaDr(i).Item("IRIME").ToString
                outkaMdr.Item("IRIME_UT") = outkaDr(i).Item("IRIME_UT").ToString
                outkaMdr.Item("REMARK") = String.Empty
                outkaMdr.Item("SIZE_KB") = String.Empty
                outkaMdr.Item("ZAIKO_KB") = String.Empty
                outkaMdr.Item("SOURCE_CD") = String.Empty
                outkaMdr.Item("YELLOW_CARD") = String.Empty
                outkaMdr.Item("GOODS_CD_NRS_FROM") = String.Empty
                outkaMdr.Item("PRINT_SORT") = "99"
            End If
            allocCanNb = allocCanNb + Convert.ToDecimal(outkaDr(i).Item("ALLOC_CAN_NB").ToString)
            allocCanQt = allocCanQt + Convert.ToDecimal(outkaDr(i).Item("ALLOC_CAN_QT").ToString)

            '■運送(大)
            If String.IsNullOrEmpty(unsoLdr.Item("NRS_BR_CD").ToString) = True Then
                unsoLdr.Item("NRS_BR_CD") = outkaDr(i).Item("NRS_BR_CD").ToString
                unsoLdr.Item("UNSO_NO_L") = unsoNoL
                unsoLdr.Item("YUSO_BR_CD") = outkaDr(i).Item("NRS_BR_CD").ToString
                unsoLdr.Item("INOUTKA_NO_L") = outkaNoL
                unsoLdr.Item("TRIP_NO") = String.Empty
                unsoLdr.Item("UNSO_CD") = String.Empty
                unsoLdr.Item("UNSO_BR_CD") = String.Empty
                unsoLdr.Item("BIN_KB") = String.Empty
                unsoLdr.Item("JIYU_KB") = String.Empty
                unsoLdr.Item("DENP_NO") = String.Empty
                unsoLdr.Item("OUTKA_PLAN_DATE") = outkaDr(i).Item("OUTKA_DATE_INIT").ToString
                unsoLdr.Item("OUTKA_PLAN_TIME") = String.Empty
                unsoLdr.Item("ARR_PLAN_DATE") = outkaDr(i).Item("OUTKA_DATE_INIT").ToString
                unsoLdr.Item("ARR_PLAN_TIME") = String.Empty
                unsoLdr.Item("ARR_ACT_TIME") = String.Empty
                unsoLdr.Item("CUST_CD_L") = outkaDr(i).Item("CUST_CD_L").ToString
                unsoLdr.Item("CUST_CD_M") = outkaDr(i).Item("CUST_CD_M").ToString
                unsoLdr.Item("CUST_REF_NO") = String.Empty
                unsoLdr.Item("SHIP_CD") = String.Empty
                unsoLdr.Item("ORIG_CD") = String.Empty
                unsoLdr.Item("DEST_CD") = String.Empty
                unsoLdr.Item("NB_UT") = String.Empty
                unsoLdr.Item("UNSO_WT") = "0"
                unsoLdr.Item("UNSO_ONDO_KB") = "90"
                unsoLdr.Item("PC_KB") = "01"
                unsoLdr.Item("TARIFF_BUNRUI_KB") = String.Empty
                unsoLdr.Item("VCLE_KB") = String.Empty
                unsoLdr.Item("MOTO_DATA_KB") = "20"
                unsoLdr.Item("TAX_KB") = String.Empty
                unsoLdr.Item("REMARK") = String.Empty
                unsoLdr.Item("SEIQ_TARIFF_CD") = String.Empty
                unsoLdr.Item("SEIQ_ETARIFF_CD") = String.Empty
                unsoLdr.Item("AD_3") = String.Empty
                unsoLdr.Item("UNSO_TEHAI_KB") = "90"
                unsoLdr.Item("BUY_CHU_NO") = String.Empty
                unsoLdr.Item("AREA_CD") = String.Empty
                unsoLdr.Item("TYUKEI_HAISO_FLG") = "00"
                unsoLdr.Item("SYUKA_TYUKEI_CD") = String.Empty
                unsoLdr.Item("HAIKA_TYUKEI_CD") = String.Empty
                unsoLdr.Item("TRIP_NO_SYUKA") = String.Empty
                unsoLdr.Item("TRIP_NO_TYUKEI") = String.Empty
                unsoLdr.Item("TRIP_NO_HAIKA") = String.Empty
            End If

            '■運送(中)
            If String.IsNullOrEmpty(unsoMdr.Item("NRS_BR_CD").ToString) = True Then
                unsoMdr.Item("NRS_BR_CD") = outkaDr(i).Item("NRS_BR_CD").ToString
                unsoMdr.Item("UNSO_NO_L") = unsoNoL
                unsoMdr.Item("UNSO_NO_M") = Me.MaeCoverData(Convert.ToString(outkaNoM), "0", 3)
                unsoMdr.Item("GOODS_CD_NRS") = outkaDr(i).Item("GOODS_CD_NRS").ToString
                unsoMdr.Item("GOODS_NM") = outkaDr(i).Item("GOODS_NM").ToString
                unsoMdr.Item("NB_UT") = outkaDr(i).Item("NB_UT").ToString
                unsoMdr.Item("QT_UT") = outkaDr(i).Item("IRIME_UT").ToString
                unsoMdr.Item("HASU") = "0"
                unsoMdr.Item("ZAI_REC_NO") = String.Empty
                unsoMdr.Item("UNSO_ONDO_KB") = "90"
                unsoMdr.Item("IRIME") = outkaDr(i).Item("IRIME").ToString
                unsoMdr.Item("IRIME_UT") = outkaDr(i).Item("IRIME_UT").ToString
                unsoMdr.Item("BETU_WT") = outkaDr(i).Item("STD_WT_KGS").ToString
                unsoMdr.Item("SIZE_KB") = String.Empty
                If String.IsNullOrEmpty(outkaDr(i).Item("ZBUKA_CD").ToString) = False Then
                    unsoMdr.Item("ZBUKA_CD") = Mid(outkaDr(i).Item("SEARCH_KEY_1").ToString, 1, 7)
                Else
                    unsoMdr.Item("ZBUKA_CD") = String.Empty
                End If
                unsoMdr.Item("ABUKA_CD") = String.Empty
                unsoMdr.Item("PKG_NB") = outkaDr(i).Item("PKG_NB").ToString
                unsoMdr.Item("LOT_NO") = outkaDr(i).Item("LOT_NO").ToString
                unsoMdr.Item("REMARK") = String.Empty
            End If

            '2014.05.16 追加START
            '■出荷ピックWK
            If canSeparate = False Then
                outkaPickWkdr = ds.Tables("LMB010IN_OUTKA_PICK_WK").NewRow()
                outkaPickWkdr.Item("NRS_BR_CD") = outkaDr(i).Item("NRS_BR_CD").ToString
                outkaPickWkdr.Item("OUTKA_NO_L") = outkaNoL
                outkaPickWkdr.Item("OUTKA_NO_M") = Me.MaeCoverData(Convert.ToString(outkaNoM), "0", 3)
                outkaPickWkdr.Item("SERIAL_NO") = outkaDr(i).Item("SERIAL_NO").ToString
                outkaPickWkdr.Item("HIKI_STATE_KBN") = "00"
                ds.Tables("LMB010IN_OUTKA_PICK_WK").Rows.Add(outkaPickWkdr) 'データセットに設定(出荷ピックWK)
            End If
            '2014.05.16 追加END

        Next

        '■出荷(中)の最終行設定
        outkaMdr.Item("OUTKA_TTL_NB") = allocCanNb
        outkaMdr.Item("OUTKA_TTL_QT") = allocCanQt
        outkaMdr.Item("BACKLOG_NB") = allocCanNb
        outkaMdr.Item("BACKLOG_QT") = allocCanQt
        outkaMpkgNb = Math.Floor(CalcData(allocCanNb, pkgNb)) '割った時の整数部分を求める
        amari = CalcDataMod(allocCanNb, pkgNb) '割った時の余り部分を求める
        If 0 < amari Then
            outkaMpkgNb = outkaMpkgNb + 1
        End If
        outkaMdr.Item("OUTKA_M_PKG_NB") = outkaMpkgNb
        outkaPkgNb = outkaPkgNb + outkaMpkgNb
        ds.Tables("LMB010IN_OUTKAM").Rows.Add(outkaMdr) 'データセットに設定(出荷(中))

        '■運送(中)の最終行設定
        unsoMdr.Item("UNSO_TTL_NB") = allocCanNb
        unsoMdr.Item("UNSO_TTL_QT") = allocCanQt
        ds.Tables("LMB010IN_UNSOM").Rows.Add(unsoMdr) 'データセットに設定(運送(中))

        '■出荷(大)の最終行設定
        outkaLdr.Item("OUTKA_PKG_NB") = outkaPkgNb
        ds.Tables("LMB010IN_OUTKAL").Rows.Add(outkaLdr) 'データセットに設定(出荷(大))

        '■運送(大)の最終行設定
        unsoLdr.Item("UNSO_PKG_NB") = outkaPkgNb
        ds.Tables("LMB010IN_UNSOL").Rows.Add(unsoLdr) 'データセットに設定(運送(大))

        Return ds

    End Function

    'WIT対応 昭和電工出荷データ作成処理対応 kasama Start

    ''' <summary>
    ''' シリアルNoで分割するかどうかを取得します。
    ''' </summary>
    ''' <param name="nrsBrCd"></param>
    ''' <param name="custCdL"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CanSeparateBySerialNo(ByVal nrsBrCd As String, ByVal custCdL As String) As Boolean

        Dim setDs As New DSL.LMB010DS
        Dim custDetailTbl As DataTable = setDs.Tables("LMB010IN_CUST_DETAIL")
        Dim custDetailRow As DataRow = custDetailTbl.NewRow

        custDetailRow("NRS_BR_CD") = nrsBrCd
        custDetailRow("CUST_CD") = custCdL
        custDetailRow("SUB_KB") = "65"

        custDetailTbl.Rows.Add(custDetailRow)

        '荷主明細マスタの取得
        setDs = CType(MyBase.CallDAC(Me._Dac, "SelectCustDetail", setDs), DSL.LMB010DS)

        '荷主明細マスタが存在する場合は分割しない
        If setDs.Tables("LMB010OUT_CUST_DETAIL").Rows.Count <> 0 _
            AndAlso LMConst.FLG.ON.Equals(setDs.Tables("LMB010OUT_CUST_DETAIL").Rows(0)("SET_NAIYO")) = True Then

            Return False
        End If

        '通常は分割する
        Return True

    End Function

    'WIT対応 昭和電工出荷データ作成処理対応 kasama Start

    '出荷データ作成(東レUTI専用)対応 yamanaka 2012.11.28 Start
    ''' <summary>
    ''' 出荷・運送のデータセットを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetUtiOutkaData(ByVal ds As DataSet) As DataSet

        ds.Tables("LMB010IN_OUTKAL").Clear()
        ds.Tables("LMB010IN_OUTKAM").Clear()
        ds.Tables("LMB010IN_UNSOL").Clear()
        ds.Tables("LMB010IN_UNSOM").Clear()
        Dim outkaLdr As DataRow = Nothing
        Dim outkaMdr As DataRow = Nothing
        Dim unsoLdr As DataRow = Nothing
        Dim unsoMdr As DataRow = Nothing
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim serialNo As String = String.Empty
        Dim goodsCdNrs As String = String.Empty
        Dim rsvNo As String = String.Empty
        Dim lotNo As String = String.Empty
        Dim irime As String = String.Empty
        Dim destCd As String = String.Empty

        Dim outkaNoL As String = String.Empty
        Dim outkaNoM As Decimal = 0
        Dim unsoNoL As String = String.Empty
        Dim outkaPkgNb As Decimal = 0
        Dim allocCanNb As Decimal = 0
        Dim allocCanQt As Decimal = 0
        Dim pkgNb As Decimal = 0
        Dim outkaMpkgNb As Decimal = 0
        Dim amari As Decimal = 0
        Dim outkaLflg As Boolean = True
        Dim outkaMflg As Boolean = True

        '要望番号:1704 yamanaka 2012.12.19 Start
        Dim outkaDr() As DataRow = ds.Tables("LMB010OUT_OUTKA").Select(Nothing, "NRS_BR_CD, CUST_CD_L, CUST_CD_M, ORIG_CD, PKG_UT, SERIAL_NO")
        'Dim outkaDr() As DataRow = ds.Tables("LMB010OUT_OUTKA").Select(Nothing, "NRS_BR_CD, CUST_CD_L, CUST_CD_M, ORIG_CD, GOODS_CD_NRS")
        '要望番号:1704 yamanaka 2012.12.19 End
        Dim max As Integer = outkaDr.Length - 1

        For i As Integer = 0 To max

            outkaLflg = False
            outkaMflg = False

            If (custCdL).Equals(outkaDr(i).Item("CUST_CD_L").ToString) = False OrElse _
                (custCdM).Equals(outkaDr(i).Item("CUST_CD_M").ToString) = False OrElse _
                (destCd).Equals(outkaDr(i).Item("ORIG_CD").ToString) = False OrElse _
                outkaNoM = 999 Then
                '出荷(大)・運送(大)の切り替え判定
                outkaLflg = True
                outkaMflg = True
            End If

            If (goodsCdNrs).Equals(outkaDr(i).Item("GOODS_CD_NRS").ToString) = False OrElse _
                (rsvNo).Equals(outkaDr(i).Item("RSV_NO").ToString) = False OrElse _
                (lotNo).Equals(outkaDr(i).Item("LOT_NO").ToString) = False OrElse _
                (irime).Equals(outkaDr(i).Item("IRIME").ToString) = False OrElse _
                (serialNo).Equals(outkaDr(i).Item("SERIAL_NO").ToString) = False Then
                '出荷(中)・運送(中)の切り替え判定
                outkaMflg = True
            End If

            If outkaMflg = True Then
                '出荷(中)・運送(中)の切り替え
                If i > 0 Then
                    '■出荷(中)
                    outkaMdr.Item("OUTKA_TTL_NB") = allocCanNb
                    outkaMdr.Item("OUTKA_TTL_QT") = allocCanQt
                    outkaMdr.Item("BACKLOG_NB") = allocCanNb
                    outkaMdr.Item("BACKLOG_QT") = allocCanQt
                    outkaMpkgNb = Math.Floor(CalcData(allocCanNb, pkgNb)) '割った時の整数部分を求める
                    amari = CalcDataMod(allocCanNb, pkgNb) '割った時の余り部分を求める
                    If 0 < amari Then
                        outkaMpkgNb = outkaMpkgNb + 1
                    End If
                    '要望番号2018 追加START
                    outkaMdr.Item("OUTKA_HASU") = amari
                    '要望番号2018 追加END
                    outkaMdr.Item("OUTKA_M_PKG_NB") = outkaMpkgNb
                    outkaPkgNb = outkaPkgNb + outkaMpkgNb
                    ds.Tables("LMB010IN_OUTKAM").Rows.Add(outkaMdr) 'データセットに設定(出荷(中))

                    '■運送(中)
                    unsoMdr.Item("UNSO_TTL_NB") = allocCanNb
                    unsoMdr.Item("UNSO_TTL_QT") = allocCanQt
                    '要望番号2018 追加START
                    unsoMdr.Item("HASU") = amari
                    '要望番号2018 追加END
                    ds.Tables("LMB010IN_UNSOM").Rows.Add(unsoMdr) 'データセットに設定(運送(中))
                End If

                outkaMdr = ds.Tables("LMB010IN_OUTKAM").NewRow()
                unsoMdr = ds.Tables("LMB010IN_UNSOM").NewRow()
                outkaNoM = outkaNoM + 1 '出荷管理番号(中)の採番(運送(中)の管理番号も兼ねている)
                '初期化
                allocCanNb = 0
                allocCanQt = 0
                pkgNb = Convert.ToDecimal(outkaDr(i).Item("PKG_NB").ToString)
                '切り替え判定値の保持
                goodsCdNrs = outkaDr(i).Item("GOODS_CD_NRS").ToString
                rsvNo = outkaDr(i).Item("RSV_NO").ToString
                lotNo = outkaDr(i).Item("LOT_NO").ToString
                irime = outkaDr(i).Item("IRIME").ToString
                serialNo = outkaDr(i).Item("SERIAL_NO").ToString        'ADD 2018/06/19 依頼番号 : 001979  
            End If

            If outkaLflg = True Then
                '出荷(大)・運送(大)の切り替え
                If i > 0 Then
                    '■出荷(大)
                    outkaLdr.Item("OUTKA_PKG_NB") = outkaPkgNb
                    ds.Tables("LMB010IN_OUTKAL").Rows.Add(outkaLdr) 'データセットに設定(出荷(大))

                    '■運送(大)
                    unsoLdr.Item("UNSO_PKG_NB") = outkaPkgNb
                    ds.Tables("LMB010IN_UNSOL").Rows.Add(unsoLdr) 'データセットに設定(運送(大))
                End If

                outkaLdr = ds.Tables("LMB010IN_OUTKAL").NewRow()
                unsoLdr = ds.Tables("LMB010IN_UNSOL").NewRow()
                outkaNoL = Me.GetOutkaNoL(ds) '出荷管理番号(大)の採番
                unsoNoL = Me.GetUnsoNoL(ds) '運送番号(大)の採番
                '初期化
                outkaNoM = 1
                outkaPkgNb = 0
                '切り替え判定値の保持
                custCdL = outkaDr(i).Item("CUST_CD_L").ToString
                custCdM = outkaDr(i).Item("CUST_CD_M").ToString
                serialNo = outkaDr(i).Item("SERIAL_NO").ToString
                destCd = outkaDr(i).Item("ORIG_CD").ToString
            End If

            '■出荷(大)
            If String.IsNullOrEmpty(outkaLdr.Item("NRS_BR_CD").ToString) = True Then
                outkaLdr.Item("NRS_BR_CD") = outkaDr(i).Item("NRS_BR_CD").ToString
                outkaLdr.Item("OUTKA_NO_L") = outkaNoL
                outkaLdr.Item("FURI_NO") = String.Empty
                outkaLdr.Item("OUTKA_KB") = "10"
                outkaLdr.Item("SYUBETU_KB") = "10"
                outkaLdr.Item("OUTKA_STATE_KB") = "10"
                outkaLdr.Item("OUTKAHOKOKU_YN") = String.Empty
                outkaLdr.Item("PICK_KB") = "01"
                outkaLdr.Item("DENP_NO") = String.Empty
                outkaLdr.Item("ARR_KANRYO_INFO") = String.Empty
                outkaLdr.Item("WH_CD") = outkaDr(i).Item("WH_CD").ToString
                outkaLdr.Item("OUTKA_PLAN_DATE") = outkaDr(i).Item("OUTKA_DATE_INIT").ToString
                outkaLdr.Item("OUTKO_DATE") = outkaDr(i).Item("OUTKA_DATE_INIT").ToString
                outkaLdr.Item("ARR_PLAN_DATE") = outkaDr(i).Item("OUTKA_DATE_INIT").ToString
                outkaLdr.Item("ARR_PLAN_TIME") = String.Empty
                outkaLdr.Item("HOKOKU_DATE") = String.Empty
                outkaLdr.Item("TOUKI_HOKAN_YN") = "01"
                outkaLdr.Item("END_DATE") = String.Empty
                outkaLdr.Item("CUST_CD_L") = outkaDr(i).Item("CUST_CD_L").ToString
                outkaLdr.Item("CUST_CD_M") = outkaDr(i).Item("CUST_CD_M").ToString
                outkaLdr.Item("SHIP_CD_L") = outkaDr(i).Item("ORIG_CD").ToString
                outkaLdr.Item("SHIP_CD_M") = String.Empty
                outkaLdr.Item("DEST_CD") = outkaDr(i).Item("DEST_CD").ToString
                outkaLdr.Item("DEST_AD_3") = outkaDr(i).Item("AD_3").ToString
                outkaLdr.Item("DEST_TEL") = String.Empty
                outkaLdr.Item("NHS_REMARK") = String.Empty
                outkaLdr.Item("SP_NHS_KB") = String.Empty
                outkaLdr.Item("COA_YN") = String.Empty
                outkaLdr.Item("CUST_ORD_NO") = String.Empty
                outkaLdr.Item("BUYER_ORD_NO") = String.Empty
                outkaLdr.Item("REMARK") = String.Empty
                outkaLdr.Item("DENP_YN") = "00"
                outkaLdr.Item("PC_KB") = "01"
                outkaLdr.Item("NIYAKU_YN") = "01"
                outkaLdr.Item("DEST_KB") = "01"
                outkaLdr.Item("DEST_NM") = outkaDr(i).Item("DEST_NM").ToString
                outkaLdr.Item("DEST_AD_1") = outkaDr(i).Item("AD_1").ToString
                outkaLdr.Item("DEST_AD_2") = outkaDr(i).Item("AD_2").ToString
                outkaLdr.Item("ALL_PRINT_FLAG") = "00"
                outkaLdr.Item("NIHUDA_FLAG") = "00"
                outkaLdr.Item("NHS_FLAG") = "00"
                outkaLdr.Item("DENP_FLAG") = "00"
                outkaLdr.Item("COA_FLAG") = "00"
                outkaLdr.Item("HOKOKU_FLAG") = "00"
                outkaLdr.Item("MATOME_PICK_FLAG") = String.Empty
                outkaLdr.Item("LAST_PRINT_DATE") = String.Empty
                outkaLdr.Item("LAST_PRINT_TIME") = String.Empty
                outkaLdr.Item("SASZ_USER") = String.Empty
                outkaLdr.Item("OUTKO_USER") = String.Empty
                outkaLdr.Item("KEN_USER") = String.Empty
                outkaLdr.Item("OUTKA_USER") = String.Empty
                outkaLdr.Item("HOU_USER") = String.Empty
                outkaLdr.Item("ORDER_TYPE") = String.Empty
            End If

            '■出荷(中)
            If String.IsNullOrEmpty(outkaMdr.Item("NRS_BR_CD").ToString) = True Then
                outkaMdr.Item("NRS_BR_CD") = outkaDr(i).Item("NRS_BR_CD").ToString
                outkaMdr.Item("OUTKA_NO_L") = outkaNoL
                outkaMdr.Item("OUTKA_NO_M") = Me.MaeCoverData(Convert.ToString(outkaNoM), "0", 3)
                outkaMdr.Item("EDI_SET_NO") = String.Empty
                outkaMdr.Item("COA_YN") = "00"
                outkaMdr.Item("CUST_ORD_NO_DTL") = String.Empty
                outkaMdr.Item("BUYER_ORD_NO_DTL") = String.Empty
                outkaMdr.Item("GOODS_CD_NRS") = outkaDr(i).Item("GOODS_CD_NRS").ToString
                outkaMdr.Item("RSV_NO") = outkaDr(i).Item("RSV_NO").ToString
                outkaMdr.Item("LOT_NO") = outkaDr(i).Item("LOT_NO").ToString
                outkaMdr.Item("SERIAL_NO") = outkaDr(i).Item("SERIAL_NO").ToString
                outkaMdr.Item("ALCTD_KB") = "01"
                outkaMdr.Item("OUTKA_PKG_NB") = outkaDr(i).Item("PKG_NB").ToString
                outkaMdr.Item("OUTKA_HASU") = "0"
                outkaMdr.Item("OUTKA_QT") = "0"
                outkaMdr.Item("ALCTD_NB") = "0"
                outkaMdr.Item("ALCTD_QT") = "0"
                outkaMdr.Item("UNSO_ONDO_KB") = "90"
                outkaMdr.Item("IRIME") = outkaDr(i).Item("IRIME").ToString
                outkaMdr.Item("IRIME_UT") = outkaDr(i).Item("IRIME_UT").ToString
                outkaMdr.Item("REMARK") = String.Empty
                outkaMdr.Item("SIZE_KB") = String.Empty
                outkaMdr.Item("ZAIKO_KB") = String.Empty
                outkaMdr.Item("SOURCE_CD") = String.Empty
                outkaMdr.Item("YELLOW_CARD") = String.Empty
                outkaMdr.Item("GOODS_CD_NRS_FROM") = String.Empty
                outkaMdr.Item("PRINT_SORT") = "99"
            End If
            allocCanNb = allocCanNb + Convert.ToDecimal(outkaDr(i).Item("ALLOC_CAN_NB").ToString)
            allocCanQt = allocCanQt + Convert.ToDecimal(outkaDr(i).Item("ALLOC_CAN_QT").ToString)

            '■運送(大)
            If String.IsNullOrEmpty(unsoLdr.Item("NRS_BR_CD").ToString) = True Then
                unsoLdr.Item("NRS_BR_CD") = outkaDr(i).Item("NRS_BR_CD").ToString
                unsoLdr.Item("UNSO_NO_L") = unsoNoL
                unsoLdr.Item("YUSO_BR_CD") = outkaDr(i).Item("NRS_BR_CD").ToString
                unsoLdr.Item("INOUTKA_NO_L") = outkaNoL
                unsoLdr.Item("TRIP_NO") = String.Empty
                unsoLdr.Item("UNSO_CD") = outkaDr(i).Item("SP_UNSO_CD").ToString
                unsoLdr.Item("UNSO_BR_CD") = outkaDr(i).Item("SP_UNSO_BR_CD").ToString
                unsoLdr.Item("BIN_KB") = String.Empty
                unsoLdr.Item("JIYU_KB") = String.Empty
                unsoLdr.Item("DENP_NO") = String.Empty
                unsoLdr.Item("OUTKA_PLAN_DATE") = outkaDr(i).Item("OUTKA_DATE_INIT").ToString
                unsoLdr.Item("OUTKA_PLAN_TIME") = String.Empty
                unsoLdr.Item("ARR_PLAN_DATE") = outkaDr(i).Item("OUTKA_DATE_INIT").ToString
                unsoLdr.Item("ARR_PLAN_TIME") = String.Empty
                unsoLdr.Item("ARR_ACT_TIME") = String.Empty
                unsoLdr.Item("CUST_CD_L") = outkaDr(i).Item("CUST_CD_L").ToString
                unsoLdr.Item("CUST_CD_M") = outkaDr(i).Item("CUST_CD_M").ToString
                unsoLdr.Item("CUST_REF_NO") = String.Empty
                unsoLdr.Item("SHIP_CD") = String.Empty
                unsoLdr.Item("ORIG_CD") = String.Empty
                unsoLdr.Item("DEST_CD") = String.Empty
                unsoLdr.Item("NB_UT") = String.Empty
                unsoLdr.Item("UNSO_WT") = "0"
                unsoLdr.Item("UNSO_ONDO_KB") = "90"
                unsoLdr.Item("PC_KB") = "01"
                unsoLdr.Item("TARIFF_BUNRUI_KB") = String.Empty
                unsoLdr.Item("VCLE_KB") = String.Empty
                unsoLdr.Item("MOTO_DATA_KB") = "20"
                unsoLdr.Item("TAX_KB") = String.Empty
                unsoLdr.Item("REMARK") = String.Empty
                unsoLdr.Item("SEIQ_TARIFF_CD") = String.Empty
                unsoLdr.Item("SEIQ_ETARIFF_CD") = String.Empty
                unsoLdr.Item("AD_3") = String.Empty
                unsoLdr.Item("UNSO_TEHAI_KB") = "90"
                unsoLdr.Item("BUY_CHU_NO") = String.Empty
                unsoLdr.Item("AREA_CD") = String.Empty
                unsoLdr.Item("TYUKEI_HAISO_FLG") = "00"
                unsoLdr.Item("SYUKA_TYUKEI_CD") = String.Empty
                unsoLdr.Item("HAIKA_TYUKEI_CD") = String.Empty
                unsoLdr.Item("TRIP_NO_SYUKA") = String.Empty
                unsoLdr.Item("TRIP_NO_TYUKEI") = String.Empty
                unsoLdr.Item("TRIP_NO_HAIKA") = String.Empty
            End If

            '■運送(中)
            If String.IsNullOrEmpty(unsoMdr.Item("NRS_BR_CD").ToString) = True Then
                unsoMdr.Item("NRS_BR_CD") = outkaDr(i).Item("NRS_BR_CD").ToString
                unsoMdr.Item("UNSO_NO_L") = unsoNoL
                unsoMdr.Item("UNSO_NO_M") = Me.MaeCoverData(Convert.ToString(outkaNoM), "0", 3)
                unsoMdr.Item("GOODS_CD_NRS") = outkaDr(i).Item("GOODS_CD_NRS").ToString
                unsoMdr.Item("GOODS_NM") = outkaDr(i).Item("GOODS_NM").ToString
                unsoMdr.Item("NB_UT") = outkaDr(i).Item("NB_UT").ToString
                unsoMdr.Item("QT_UT") = outkaDr(i).Item("IRIME_UT").ToString
                unsoMdr.Item("HASU") = "0"
                unsoMdr.Item("ZAI_REC_NO") = String.Empty
                unsoMdr.Item("UNSO_ONDO_KB") = "90"
                unsoMdr.Item("IRIME") = outkaDr(i).Item("IRIME").ToString
                unsoMdr.Item("IRIME_UT") = outkaDr(i).Item("IRIME_UT").ToString
                unsoMdr.Item("BETU_WT") = outkaDr(i).Item("STD_WT_KGS").ToString
                unsoMdr.Item("SIZE_KB") = String.Empty
                If String.IsNullOrEmpty(outkaDr(i).Item("ZBUKA_CD").ToString) = False Then
                    unsoMdr.Item("ZBUKA_CD") = Mid(outkaDr(i).Item("SEARCH_KEY_1").ToString, 1, 7)
                Else
                    unsoMdr.Item("ZBUKA_CD") = String.Empty
                End If
                unsoMdr.Item("ABUKA_CD") = String.Empty
                unsoMdr.Item("PKG_NB") = outkaDr(i).Item("PKG_NB").ToString
                unsoMdr.Item("LOT_NO") = outkaDr(i).Item("LOT_NO").ToString
                unsoMdr.Item("REMARK") = String.Empty
            End If

        Next

        '■出荷(中)の最終行設定
        outkaMdr.Item("OUTKA_TTL_NB") = allocCanNb
        outkaMdr.Item("OUTKA_TTL_QT") = allocCanQt
        outkaMdr.Item("BACKLOG_NB") = allocCanNb
        outkaMdr.Item("BACKLOG_QT") = allocCanQt
        outkaMpkgNb = Math.Floor(CalcData(allocCanNb, pkgNb)) '割った時の整数部分を求める
        amari = CalcDataMod(allocCanNb, pkgNb) '割った時の余り部分を求める
        If 0 < amari Then
            outkaMpkgNb = outkaMpkgNb + 1
        End If
        '要望番号2018 追加START
        outkaMdr.Item("OUTKA_HASU") = amari
        '要望番号2018 追加END
        outkaMdr.Item("OUTKA_M_PKG_NB") = outkaMpkgNb
        outkaPkgNb = outkaPkgNb + outkaMpkgNb
        ds.Tables("LMB010IN_OUTKAM").Rows.Add(outkaMdr) 'データセットに設定(出荷(中))

        '■運送(中)の最終行設定
        unsoMdr.Item("UNSO_TTL_NB") = allocCanNb
        unsoMdr.Item("UNSO_TTL_QT") = allocCanQt
        '要望番号2018 追加START
        unsoMdr.Item("HASU") = amari
        '要望番号2018 追加END
        ds.Tables("LMB010IN_UNSOM").Rows.Add(unsoMdr) 'データセットに設定(運送(中))

        '■出荷(大)の最終行設定
        outkaLdr.Item("OUTKA_PKG_NB") = outkaPkgNb
        ds.Tables("LMB010IN_OUTKAL").Rows.Add(outkaLdr) 'データセットに設定(出荷(大))

        '■運送(大)の最終行設定
        unsoLdr.Item("UNSO_PKG_NB") = outkaPkgNb
        ds.Tables("LMB010IN_UNSOL").Rows.Add(unsoLdr) 'データセットに設定(運送(大))

        Return ds

    End Function
    '出荷データ作成(東レUTI専用)対応 yamanaka 2012.11.28 End

    ''' <summary>
    ''' 出荷データ(大)の更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertOutkaL(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertOutkaL")

    End Function

    ''' <summary>
    ''' 出荷データ(中)の更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertOutkaM(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertOutkaM")

    End Function

    ''' <summary>
    ''' 運送データ(大)の更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertUnsoL(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertUnsoL")

    End Function

    ''' <summary>
    ''' 出荷データ(中)の更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertUnsoM(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertUnsoM")

    End Function

    ''' <summary>
    ''' OUTKA_NO_Lを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>OutkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoL(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.OUTKA_NO_L, Me, ds.Tables("LMB010OUT_OUTKA").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' UNSO_NO_Lを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>OutkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetUnsoNoL(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, ds.Tables("LMB010OUT_OUTKA").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' ゼロ割回避処理
    ''' </summary>
    ''' <param name="value1">分子の値</param>
    ''' <param name="value2">分母の値</param>
    ''' <returns>分母が0の場合、0を返却</returns>
    ''' <remarks></remarks>
    Private Function CalcData(ByVal value1 As Decimal, ByVal value2 As Decimal) As Decimal

        If value2 = 0 Then
            Return 0
        End If

        Return value1 / value2

    End Function

    ''' <summary>
    ''' ゼロ割回避処理(あまり値を返却)
    ''' </summary>
    ''' <param name="value1">分子の値</param>
    ''' <param name="value2">分母の値</param>
    ''' <returns>分母が0の場合、0を返却</returns>
    ''' <remarks></remarks>
    Private Function CalcDataMod(ByVal value1 As Decimal, ByVal value2 As Decimal) As Decimal

        If value2 = 0 Then
            Return 0
        End If

        Return value1 Mod value2

    End Function

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function
    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）
#End Region

#Region "入荷報告取り消し処理"
    'START 要望番号1784 s.kobayashi 
    ''' <summary>
    ''' 入荷報告取り消し処理（チェック）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function IsCreatedOutkaData(ByVal ds As DataSet) As DataSet

        '2017/09/25 修正 李↓

        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        '入荷報告取り消し処理の検索(カウント)
        ds = MyBase.CallDAC(Me._Dac, "SelectUtiOutkaDataCountForCancel", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count <> 0 Then

            '20151020 tsunehira add
            MyBase.SetMessageStore("00", "E621", , ds.Tables("LMB010IN_INKA_L").Rows(0).Item("ROW_NO").ToString)
            'MyBase.SetMessageStore("00", "E532", New String() {"削除", "報告済取消"}, ds.Tables("LMB010IN_INKA_L").Rows(0).Item("ROW_NO").ToString)

            MyBase.SetMessage("E486")
            Return ds
        End If

        Return ds

    End Function

    'START 要望番号1784 s.kobayashi 
    ''' <summary>
    ''' 入荷報告取り消し処理（更新）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaHokokuCancel(ByVal ds As DataSet) As DataSet

        '要望番号:1892 yamanaka 2013.03.25 Start
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables("LMB010IN_INKA_L")

        For i As Integer = 0 To ds.Tables("LMB010IN_INKA_L").Rows.Count - 1

            '値のクリア
            setDt.Clear()

            '条件の設定
            setDt.ImportRow(ds.Tables("LMB010IN_INKA_L").Rows(i))

            setDs = MyBase.CallDAC(Me._Dac, "UpdateInkaHokokuCancel", setDs)

        Next

        Return ds

        'Return MyBase.CallDAC(Me._Dac, "UpdateInkaHokokuCancel", ds)
        '要望番号:1892 yamanaka 2013.03.25 End

    End Function

    'End 要望番号1784 s.kobayashi 
#End Region

#Region "入荷データ一括取込処理"

    'WIT対応 入荷データ一括取込対応 kasama Start
    ''' <summary>
    ''' 入荷データ一括取込処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InkaIkkatuTorikomi(ByVal ds As DataSet) As DataSet

        '2017/09/25 修正 李↓

        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        'メッセージのクリア
        MyBase.SetMessage(Nothing)

        '入力チェック
        If Me.CheckInkaIkkatuTorikomi(ds) = False Then
            Return ds
        End If

        'エラーチェック完了後にUpdateしたいめコメントアウト＆Functionの最下部に移動 2014/07.14 kikuchi
        '入荷(大)のステータス更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateInkaIkkatuTorikomi", ds)

        'INKA_WKを元に、取込用元データの取得
        ds = MyBase.CallDAC(Me._Dac, "SelectTorikomiSrcData", ds)

        '追加開始 --- 2014.07.14 kikuchi
        'エラーチェック
        If ds.Tables("LMB010OUT_TORIKOMI_SRC").Rows.Count = 0 Then

            MyBase.SetMessage("E616")

            MyBase.SetMessageStore("00", "E616", , _
                                ds.Tables("LMB010IN_INKA_L").Rows(0).Item("ROW_NO").ToString(), _
                                lgm.Selector({"入荷管理番号=", "Stock control number=", "입하관리번호=", "中国語="}),
                                ds.Tables("LMB010IN_INKA_L").Rows(0).Item("INKA_NO_L").ToString())

            Return ds

        End If
        '追加終了 --- 

        '取込用元データから、入荷(小)・在庫データ作成
        For Each torikomiSrcRow As DataRow In ds.Tables("LMB010OUT_TORIKOMI_SRC").Rows

            '検品対象の入荷(中)が存在しない場合はエラー
            If String.IsNullOrEmpty(torikomiSrcRow("NRS_BR_CD").ToString) = True Then

                MyBase.SetMessage("E616")
                MyBase.SetMessageStore("00", "E616", , _
                                    ds.Tables("LMB010IN_INKA_L").Rows(0).Item("ROW_NO").ToString(), _
                                    lgm.Selector({"入荷管理番号=", "Stock control number=", "입하관리번호=", "中国語="}),
                                    String.Concat(torikomiSrcRow("INKA_NO_L").ToString, "-", torikomiSrcRow("INKA_NO_M").ToString))

                Return ds
            End If

            '検品数とEDI予定数が異なる場合はエラー
            If Convert.ToInt32(torikomiSrcRow("EDI_NB")) <> Convert.ToInt32(torikomiSrcRow("SUM_INKA_NB")) Then

                MyBase.SetMessage("E616")
                MyBase.SetMessageStore("00", "E616", , _
                                    ds.Tables("LMB010IN_INKA_L").Rows(0).Item("ROW_NO").ToString(), _
                                    lgm.Selector({"入荷管理番号=", "Stock control number=", "입하관리번호=", "中国語="}),
                                    String.Concat(torikomiSrcRow("INKA_NO_L").ToString, "-", torikomiSrcRow("INKA_NO_M").ToString))

                Return ds
            End If

            '商品が存在しない場合はエラー
            If String.IsNullOrEmpty(torikomiSrcRow("GOODS_CD_NRS").ToString) = True Then

                

                '20151020 tsunehira add

                Dim params() As String = New String() {torikomiSrcRow("INKA_NO_L").ToString, _
                                                       torikomiSrcRow("INKA_NO_M").ToString}

                MyBase.SetMessage("E623", params)
                MyBase.SetMessageStore("00", "E623", params)

                'Dim params() As String = New String() {"取込対象", _
                '                                       "取込", _
                '                                       torikomiSrcRow("INKA_NO_L").ToString, _
                '                                       torikomiSrcRow("INKA_NO_M").ToString}
                'MyBase.SetMessage("E603", params)
                'MyBase.SetMessageStore("00", "E603", params)
                    

                Return ds
            End If

            '2017/09/25 修正 李↑

            'INKA_NO_Sを採番し、torikomiSrcRowへ設定
            Dim inkaNoS As String = Me.GetInkaNoS(torikomiSrcRow("NRS_BR_CD").ToString, _
                                                  torikomiSrcRow("INKA_NO_L").ToString, _
                                                  torikomiSrcRow("INKA_NO_M").ToString)

            torikomiSrcRow("INKA_NO_S") = inkaNoS

            'セット用DS作成
            Dim setDs As DataSet = Me.MakeTorikomiSetDs(torikomiSrcRow)

            '入荷(小)作成
            Call Me.CallDAC(Me._Dac, "InsertInkaS", setDs)

            '在庫作成
            Call Me.CallDAC(Me._Dac, "InsertZaiTrs", setDs)

            'INKA_WK更新
            Call Me.CallDAC(Me._Dac, "UpdateInkaWk", setDs)
        Next

        '入荷(大)のステータス更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateInkaIkkatuTorikomi", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 取込用元データ行から、SetDsを作成します。
    ''' </summary>
    ''' <param name="torikomiSrcRow"></param>
    ''' <remarks></remarks>
    Private Function MakeTorikomiSetDs(ByVal torikomiSrcRow As DataRow) As DataSet

        'SetDs作成
        Dim setDs As DataSet = torikomiSrcRow.Table.DataSet.Copy
        Dim setTorikomiSrcDt As DataTable = setDs.Tables(torikomiSrcRow.Table.TableName)
        setTorikomiSrcDt.Clear()
        setTorikomiSrcDt.ImportRow(torikomiSrcRow)

        Dim setInkaSDt As DataTable = setDs.Tables("LMB010IN_INKA_S")
        Dim setZaiTrsDt As DataTable = setDs.Tables("LMB010IN_ZAI_TRS")

        setInkaSDt.Clear()
        setZaiTrsDt.Clear()

        '在庫番号を採番
        Dim zaiRecNo As String = Me.GetZaiRecNo(torikomiSrcRow("NRS_BR_CD").ToString)

        '入荷S情報作成
        Dim inkaRow As DataRow = setInkaSDt.NewRow
        inkaRow("NRS_BR_CD") = torikomiSrcRow("NRS_BR_CD").ToString()
        inkaRow("INKA_NO_L") = torikomiSrcRow("INKA_NO_L").ToString()
        inkaRow("INKA_NO_M") = torikomiSrcRow("INKA_NO_M").ToString()
        inkaRow("INKA_NO_S") = torikomiSrcRow("INKA_NO_S").ToString()

        inkaRow("ZAI_REC_NO") = zaiRecNo
        '2014.06.06 FFEM対応 追加START
        If (torikomiSrcRow("SET_NAIYO").ToString()).Equals("01") = True Then
            inkaRow("LOT_NO") = torikomiSrcRow("LOT_NO").ToString()
        Else
            inkaRow("LOT_NO") = String.Empty
        End If
        '2014.06.06 FFEM対応 追加END
        inkaRow("LOCA") = torikomiSrcRow("LOCA").ToString()
        inkaRow("TOU_NO") = torikomiSrcRow("TOU_NO").ToString()
        inkaRow("SITU_NO") = torikomiSrcRow("SITU_NO").ToString()
        inkaRow("ZONE_CD") = torikomiSrcRow("ZONE_CD").ToString()

        Dim nyukozumi As Integer = CInt(torikomiSrcRow("NYUKOZUMI_NB").ToString())
        Dim pkgNb As Integer = CInt(torikomiSrcRow("PKG_NB").ToString())

        inkaRow("KONSU") = CStr(nyukozumi \ pkgNb)
        inkaRow("HASU") = CStr(nyukozumi Mod pkgNb)
        inkaRow("IRIME") = torikomiSrcRow("STD_IRIME_NB").ToString()
        inkaRow("BETU_WT") = torikomiSrcRow("STD_WT_KGS").ToString()
        '2014.06.06 FFEM対応 追加START
        If (torikomiSrcRow("SET_NAIYO").ToString()).Equals("01") = True Then
            'If torikomiSrcRow("GOODS_KANRI_NO").ToString().Length = 6 Then
            '    inkaRow("SERIAL_NO") = String.Empty
            'ElseIf torikomiSrcRow("GOODS_KANRI_NO").ToString().Length = 8 Then
            '    inkaRow("SERIAL_NO") = torikomiSrcRow("GOODS_KANRI_NO").ToString()
            'Else
            '    inkaRow("SERIAL_NO") = String.Empty
            'End If
            inkaRow("SERIAL_NO") = String.Empty
        Else
            inkaRow("SERIAL_NO") = torikomiSrcRow("GOODS_KANRI_NO").ToString()
        End If
        '2014.06.06 FFEM対応 追加END
        inkaRow("GOODS_COND_KB_1") = String.Empty
        inkaRow("GOODS_COND_KB_2") = String.Empty
        inkaRow("GOODS_COND_KB_3") = String.Empty
        inkaRow("GOODS_CRT_DATE") = String.Empty
        inkaRow("LT_DATE") = String.Empty
        inkaRow("SPD_KB") = "01"
        inkaRow("OFB_KB") = "01"
        inkaRow("DEST_CD") = String.Empty
        '2014.06.06 FFEM対応 追加START
        'If (torikomiSrcRow("SET_NAIYO").ToString()).Equals("01") = True Then
        '    If torikomiSrcRow("GOODS_KANRI_NO").ToString().Length = 6 Then
        '        inkaRow("REMARK") = torikomiSrcRow("GOODS_KANRI_NO").ToString()
        '    Else
        '        inkaRow("REMARK") = String.Empty
        '    End If
        'Else
        '    inkaRow("REMARK") = String.Empty
        'End If
        inkaRow("REMARK") = String.Empty
        '2014.06.06 FFEM対応 追加END
        inkaRow("ALLOC_PRIORITY") = "10"
        inkaRow("REMARK_OUT") = String.Empty

        setInkaSDt.Rows.Add(inkaRow)

        '在庫情報作成
        Dim zaiRow As DataRow = setZaiTrsDt.NewRow
        zaiRow("NRS_BR_CD") = torikomiSrcRow("NRS_BR_CD").ToString()
        zaiRow("ZAI_REC_NO") = zaiRecNo
        zaiRow("WH_CD") = torikomiSrcRow("WH_CD").ToString
        zaiRow("TOU_NO") = torikomiSrcRow("TOU_NO").ToString()
        zaiRow("SITU_NO") = torikomiSrcRow("SITU_NO").ToString()
        zaiRow("ZONE_CD") = torikomiSrcRow("ZONE_CD").ToString()
        zaiRow("LOCA") = torikomiSrcRow("LOCA").ToString()
        '2014.06.06 FFEM対応 追加START
        If (torikomiSrcRow("SET_NAIYO").ToString()).Equals("01") = True Then
            zaiRow("LOT_NO") = torikomiSrcRow("LOT_NO").ToString()
        Else
            zaiRow("LOT_NO") = String.Empty
        End If
        '2014.06.06 FFEM対応 追加END
        zaiRow("CUST_CD_L") = torikomiSrcRow("CUST_CD_L").ToString()
        zaiRow("CUST_CD_M") = torikomiSrcRow("CUST_CD_M").ToString()
        zaiRow("GOODS_KANRI_NO") = torikomiSrcRow("GOODS_KANRI_NO").ToString()
        zaiRow("GOODS_CD_NRS") = torikomiSrcRow("GOODS_CD_NRS").ToString()
        zaiRow("INKA_NO_L") = torikomiSrcRow("INKA_NO_L").ToString()
        zaiRow("INKA_NO_M") = torikomiSrcRow("INKA_NO_M").ToString()
        zaiRow("INKA_NO_S") = torikomiSrcRow("INKA_NO_S").ToString()
        zaiRow("ALLOC_PRIORITY") = "10"
        zaiRow("RSV_NO") = String.Empty
        '2014.06.06 FFEM対応 追加START
        If (torikomiSrcRow("SET_NAIYO").ToString()).Equals("01") = True Then
            'If torikomiSrcRow("GOODS_KANRI_NO").ToString().Length = 6 Then
            '    zaiRow("SERIAL_NO") = String.Empty
            'ElseIf torikomiSrcRow("GOODS_KANRI_NO").ToString().Length = 8 Then
            '    zaiRow("SERIAL_NO") = torikomiSrcRow("GOODS_KANRI_NO").ToString()
            'Else
            '    zaiRow("SERIAL_NO") = String.Empty
            'End If
            zaiRow("SERIAL_NO") = String.Empty
        Else
            zaiRow("SERIAL_NO") = torikomiSrcRow("GOODS_KANRI_NO").ToString()
        End If
        '2014.06.06 FFEM対応 追加END
        zaiRow("HOKAN_YN") = torikomiSrcRow("HOKAN_YN").ToString()
        zaiRow("TAX_KB") = torikomiSrcRow("TAX_KB").ToString()
        zaiRow("GOODS_COND_KB_1") = String.Empty
        zaiRow("GOODS_COND_KB_2") = String.Empty
        zaiRow("GOODS_COND_KB_3") = String.Empty
        zaiRow("OFB_KB") = "01"
        zaiRow("SPD_KB") = "01"
        zaiRow("REMARK_OUT") = String.Empty
        zaiRow("PORA_ZAI_NB") = torikomiSrcRow("INKA_NB").ToString()
        zaiRow("ALCTD_NB") = "0"
        zaiRow("ALLOC_CAN_NB") = torikomiSrcRow("INKA_NB").ToString()
        zaiRow("IRIME") = torikomiSrcRow("STD_IRIME_NB").ToString()

        Dim irime As Double = 0
        Dim inkaNb As Integer = 0
        Double.TryParse(torikomiSrcRow("STD_IRIME_NB").ToString(), irime)
        Integer.TryParse(torikomiSrcRow("INKA_NB").ToString(), inkaNb)

        zaiRow("PORA_ZAI_QT") = CStr(inkaNb * irime)
        zaiRow("ALCTD_QT") = "0"
        zaiRow("ALLOC_CAN_QT") = CStr(inkaNb * irime)
        zaiRow("INKO_DATE") = MyBase.GetSystemDate
        zaiRow("INKO_PLAN_DATE") = MyBase.GetSystemDate
        zaiRow("ZERO_FLAG") = String.Empty
        zaiRow("LT_DATE") = String.Empty
        zaiRow("GOODS_CRT_DATE") = String.Empty
        zaiRow("DEST_CD_P") = String.Empty
        '2014.06.06 FFEM対応 追加START
        'If (torikomiSrcRow("SET_NAIYO").ToString()).Equals("01") = True Then
        '    If torikomiSrcRow("GOODS_KANRI_NO").ToString().Length = 6 Then
        '        zaiRow("REMARK") = torikomiSrcRow("GOODS_KANRI_NO").ToString()
        '    Else
        '        zaiRow("REMARK") = String.Empty
        '    End If
        'Else
        '    zaiRow("REMARK") = String.Empty
        'End If
        zaiRow("REMARK") = String.Empty
        '2014.06.06 FFEM対応 追加END
        zaiRow("SMPL_FLAG") = "00"

        setZaiTrsDt.Rows.Add(zaiRow)

        Return setDs

    End Function

    ''' <summary>
    ''' ZAI_REC_NOを取得
    ''' </summary>
    ''' <param name="nrsBrCd"></param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetZaiRecNo(ByVal nrsBrCd As String) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, nrsBrCd)

    End Function

    ''' <summary>
    ''' INKA_NO_Sを取得
    ''' </summary>
    ''' <param name="nrsBrCd"></param>
    ''' <param name="inkaNoL"></param>
    ''' <param name="inkaNoM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetInkaNoS(ByVal nrsBrCd As String, ByVal inkaNoL As String, inkaNoM As String) As String

        Dim setDs As New DSL.LMB010DS
        Dim inTbl As DataTable = setDs.Tables("LMB010IN_INKA_S")
        Dim inRow As DataRow = inTbl.NewRow

        inRow("NRS_BR_CD") = nrsBrCd
        inRow("INKA_NO_L") = inkaNoL
        inRow("INKA_NO_M") = inkaNoM

        inTbl.Rows.Add(inRow)

        Call Me.CallDAC(Me._Dac, "SelectMaxInkaNoS", setDs)

        Dim outRow As DataRow = setDs.Tables("LMB010OUT_INKA_NO_S").Rows(0)
        Dim maxInkaNoS As Integer = CInt(outRow("MAX_INKA_NO_S").ToString)

        Return String.Format("{0:000}", maxInkaNoS + 1)

    End Function

    ''' <summary>
    ''' 一括取込入力チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckInkaIkkatuTorikomi(ByVal ds As DataSet) As Boolean

        '2017/09/25 修正 李↓

        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        Dim inRow As DataRow = ds.Tables("LMB010IN_INKA_L").Rows(0)

        Dim setDs As DataSet = ds.Copy
        Dim custDetailTbl As DataTable = setDs.Tables("LMB010IN_CUST_DETAIL")
        Dim custDetailRow As DataRow = custDetailTbl.NewRow

        custDetailRow("NRS_BR_CD") = inRow("NRS_BR_CD").ToString
        custDetailRow("CUST_CD") = inRow("CUST_CD_L").ToString
        custDetailRow("SUB_KB") = "64"

        custDetailTbl.Rows.Add(custDetailRow)

        '荷主明細マスタの取得
        setDs = MyBase.CallDAC(Me._Dac, "SelectCustDetail", setDs)

        If setDs.Tables("LMB010OUT_CUST_DETAIL").Rows.Count = 0 _
            OrElse LMConst.FLG.ON.Equals(setDs.Tables("LMB010OUT_CUST_DETAIL").Rows(0)("SET_NAIYO")) = False Then
            '荷主明細マスタにデータがないのでエラー

            MyBase.SetMessage("E209")

            MyBase.SetMessageStore("00", "E209", New String() {String.Concat(
                                   lgm.Selector({"日本語=", "English=", "한국어=", "中国語="}),
                                   inRow("CUST_CD_L").ToString)}, inRow("ROW_NO").ToString)

            Return False
        End If

        '2017/09/25 修正 李↑

        '排他検索[入荷(大)]
        If Me.ServerChkJudge(ds, "SelectInkaLCountBySysDateTime") = False Then
            MyBase.SetMessage("E011")
            MyBase.SetMessageStore("00", "E011", , inRow.Item("ROW_NO").ToString())
            Return False
        End If

        '入荷WK　検品数チェック
        ds = MyBase.CallDAC(Me._Dac, "SelectBInkaWkKenpinChk", ds)
        If MyBase.GetResultCount > 0 Then
            '英語化対応
            '20151021 tsunehira add
            MyBase.SetMessage("E642")
            MyBase.SetMessageStore("00", "E642", , inRow("ROW_NO").ToString)

            'MyBase.SetMessage("E011")
            'MyBase.SetMessageStore("00", "E260", New String() {"未検品データ"}, inRow("ROW_NO").ToString)

            'エラーがあるかを判定
            Return Not MyBase.IsMessageExist()

        End If

        Return True

    End Function
    'WIT対応 入荷データ一括取込対応 kasama End

#End Region

    '2014.04.17 CALT連携対応 ri追加 --ST--
#Region "入荷予定データ作成処理"
    Private Function InkaYoteiInsert(ByVal ds As DataSet) As DataSet

        '--外部定義変数
        '***カウンター
        'LMB010IN_INKA_PLAN_SENDテーブルのRowカウンタ
        Dim iIPlanSendCnt As Integer = 0
        Dim Cnt As Integer = 0

        '****フラグ系
        '抽出処理成否フラグ
        Dim bSCForSelData As Boolean = True
        '作成処理成否フラグ
        Dim bSCForInsData As Boolean = True
        'キャンセル処理フラグ
        Dim bSCForCanData As Boolean = False

        '***データセット系
        'IN用データセット
        Dim dsI As DataSet = ds.Copy()
        Dim dtIPlanSend As DataTable = dsI.Tables("LMB010IN_INKA_PLAN_SEND")
        '作業データセット
        Dim dsTmp As DataSet = ds.Clone()
        Dim drTmp As DataRow = Nothing
        '==========================================================

        'INデータ分だけループ(2014.04.17の組込時点ではループは一回のみ)
        iIPlanSendCnt = dtIPlanSend.Rows.Count
        For i As Integer = 0 To iIPlanSendCnt - 1

            Dim drIPlanSend As DataRow = Nothing
            drIPlanSend = dtIPlanSend.Rows(i)

            '--入力チェック

            '==========================================================

            '--DACアクセス(SELECT) --報告用データ抽出兼排他チェック--
            Dim dsO As DataSet = dsI.Copy
            dsO = MyBase.CallDAC(Me._Dac, "SelectInkaLMS", dsO)

            If MyBase.GetResultCount() < 1 Then
                'Dim param() As String = {"入荷データが更新されている", "入荷予定データ作成"}
                'MyBase.SetMessage("E454", param)
                'MyBase.SetMessageStore("00", "E454", param, drIPlanSend.Item("ROW_NO").ToString())

                '20151029 tsunehira add Start
                '英語化対応
                MyBase.SetMessage("E788")
                MyBase.SetMessageStore("00", "E788", New String() {drIPlanSend.Item("ROW_NO").ToString()})
                '2015.10.29 tusnehira add End
                Return ds
            End If

            '入荷S作成チェック
            If MyBase.GetResultCount() > 0 Then

                Dim iRowCnt As Integer = 0
                Dim dtRow As DataRow = Nothing
                iRowCnt = dsO.Tables("LMB010OUT_INKA_PLAN_SEND").Rows.Count

                For j As Integer = 0 To iRowCnt - 1
                    dtRow = dsO.Tables("LMB010OUT_INKA_PLAN_SEND").Rows(j)

                    If String.IsNullOrEmpty(dtRow.Item("INKA_NO_S").ToString()) Then
                        'Dim param() As String = {"入荷小がないデータ"}
                        'MyBase.SetMessage("E237", param)
                        'MyBase.SetMessageStore("00", "E237", param, drIPlanSend.Item("ROW_NO").ToString())

                        '20151029 tsunehira add Start
                        '英語化対応
                        MyBase.SetMessage("E789")
                        MyBase.SetMessageStore("00", "E789", New String() {drIPlanSend.Item("ROW_NO").ToString()})
                        '2015.10.29 tusnehira add End
                    End If
                Next

                If IsMessageExist() = True Then
                    Return ds
                End If

            End If

            '==========================================================

            '--キャンセルデータの抽出
            Dim dsC As DataSet = dsI.Copy
            dsC = MyBase.CallDAC(Me._Dac, "SelectSendCancel", dsC)

            '--DACアクセス(INSERT) --キャンセル報告データ作成--
            If MyBase.GetResultCount() > 0 Then

                '--パラメータの編集(必要があれば当Function内に記述
                Call Me.EditData(dsC)

                Cnt = 0
                Cnt = dsC.Tables("LMB010OUT_INKA_PLAN_SEND").Rows.Count
                For j As Integer = 0 To Cnt - 1
                    dsTmp.Tables("LMB010OUT_INKA_PLAN_SEND").Clear()

                    drTmp = dsC.Tables("LMB010OUT_INKA_PLAN_SEND").Rows(j)

                    dsTmp.Tables("LMB010OUT_INKA_PLAN_SEND").ImportRow(drTmp)

                    MyBase.CallDAC(Me._Dac, "InsertSendInkaData", dsTmp)
                Next
            End If

            '==========================================================

            '--パラメータの編集(必要があれば当Function内に記述)
            Call Me.EditData(dsO)

            '--DACアクセス(INSERT) --報告データ作成--
            Cnt = 0
            Cnt = dsO.Tables("LMB010OUT_INKA_PLAN_SEND").Rows.Count
            For j As Integer = 0 To Cnt - 1
                dsTmp.Tables("LMB010OUT_INKA_PLAN_SEND").Clear()

                drTmp = dsO.Tables("LMB010OUT_INKA_PLAN_SEND").Rows(j)

                dsTmp.Tables("LMB010OUT_INKA_PLAN_SEND").ImportRow(drTmp)

                MyBase.CallDAC(Me._Dac, "InsertSendInkaData", dsTmp)
            Next

            '==========================================================

            '--DACアクセス(UPDATE) '--入荷進捗ステータス引上げ--
            'MyBase.CallDAC(Me._Dac, "UpdateInkaLState", dsI)

            '==========================================================

        Next

        Return ds

    End Function

    Private Sub EditData(ByRef ds As DataSet)
        Dim dsEdit As DataSet = ds.Copy()
        Dim dtEditSend As DataTable = dsEdit.Tables("LMB010OUT_INKA_PLAN_SEND")

        Dim iSeq As Integer = 0


        For Each editRow As DataRow In dtEditSend.Rows
            iSeq = Convert.ToInt32(editRow.Item("SEND_SEQ"))
            editRow.Item("SEND_SEQ") = Right(String.Concat("000", iSeq), 3)
            '=================
        Next

        ds.Clear()

        ds = dsEdit
    End Sub

#End Region
    '2014.04.17 CALT連携対応 ri追加 --ST--

#Region "TSMC入荷EDI受信データテーブル 存在チェック"

    ''' <summary>
    ''' TSMC入荷EDI受信データテーブル 存在チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function CheckInkaediDtlTsmcExists(ByVal ds As DataSet) As Boolean

        ds.Tables("LMB010_TBL_EXISTS").Clear()
        Dim drTblExists As DataRow = ds.Tables("LMB010_TBL_EXISTS").NewRow()
        drTblExists.Item("NRS_BR_CD") = ds.Tables("LMB010IN_PRINT_RFID").Rows(0).Item("NRS_BR_CD")
        drTblExists.Item("TBL_NM") = "H_INKAEDI_DTL_TSMC"
        ds.Tables("LMB010_TBL_EXISTS").Rows.Add(drTblExists)
        ds = Me.GetTrnTblExits(ds)

        Dim drExists As DataRow()
        drExists = ds.Tables("LMB010_TBL_EXISTS").Select("TBL_NM = 'H_INKAEDI_DTL_TSMC'")
        If drExists.Count > 0 AndAlso drExists(0).Item("TBL_EXISTS").ToString() = "1" Then
            Return True
        Else
            Return False
        End If

    End Function

#End Region ' "TSMC入荷EDI受信データテーブル 存在チェック"

#Region "特定の荷主固有のテーブルが存在するか否かの判定"

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function GetTrnTblExits(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "GetTrnTblExits", ds)

    End Function

#End Region ' "特定の荷主固有のテーブルが存在するか否かの判定"

#Region "RFIDラベルデータ取得処理"

    Private Function SelectRfidLavelData(ByVal ds As DataSet) As DataSet

        If Not CheckInkaediDtlTsmcExists(ds) Then
            ' TSMC入荷EDI受信データテーブルの存在しない環境は、RFIDラベルデータ取得処理を行う前に対象データなし判定とする。
            MyBase.SetMessageStore("00", "E501", New String() {String.Empty}, ds.Tables("LMB010IN_PRINT_RFID").Rows(0).Item("ROW_NO").ToString(), "入荷管理番号(大)", ds.Tables("LMB010IN_PRINT_RFID").Rows(0).Item("INKA_NO_L").ToString())
            Return ds
        End If

        ds = MyBase.CallDAC(Me._Dac, "SelectRfidLavelData", ds)

        If MyBase.GetResultCount() = 0 Then
            ' 対象データなし
            MyBase.SetMessageStore("00", "E501", New String() {String.Empty}, ds.Tables("LMB010IN_PRINT_RFID").Rows(0).Item("ROW_NO").ToString(), "入荷管理番号(大)", ds.Tables("LMB010IN_PRINT_RFID").Rows(0).Item("INKA_NO_L").ToString())
        End If

        Return ds

    End Function
#End Region ' "RFIDラベルデータ取得処理"

#Region "更新処理"

    'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    ''' <summary>
    ''' 入荷(大)の更新処理を行う
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaLPrint(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateInkaLPrint", ds)

    End Function
    'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

#End Region

#End Region

End Class
