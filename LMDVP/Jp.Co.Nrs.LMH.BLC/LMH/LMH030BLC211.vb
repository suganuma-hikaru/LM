' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH              : EDI
'  プログラムID     :  LMH030           : EDI出荷検索
'  EDI荷主ID　　　　:  211　　　        : 協立化学(国内[輸出以外]用)
'  作  成  者       :  Inoue            : LMH030BLC210を継承
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.DSL.LMH030DS

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC211
    Inherits LMH030BLC210

#Region "Field"

    ''' <summary>
    ''' 削除区分設定値
    ''' (ToDo:適切な定義場所に移動)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class DEL_KB_VALUES

        ''' <summary>
        ''' 正常(非削除)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NORMAL As String = LMConst.FLG.OFF

        ''' <summary>
        ''' 削除
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DELETE As String = LMConst.FLG.ON

        ''' <summary>
        ''' キャンセル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CANCEL As String = "2"

        ''' <summary>
        ''' 保留
        ''' </summary>
        ''' <remarks></remarks>
        Public Const RESERVATION As String = "3"

    End Class

    ''' <summary>
    ''' 受信テーブル書込みフラグ設定値
    ''' (ToDo:適切な定義場所に移動)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class RCV_TBL_INS_FLG_VALUES
        ''' <summary>
        ''' 書込み対象外 
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NOT_WRITE As String = "0"

        ''' <summary>
        ''' 書込み対象＋過去DATA整合性CHK有
        ''' </summary>
        ''' <remarks></remarks>
        Public Const WRITE_AND_DATA_CHECK As String = "1"

        ''' <summary>
        ''' 過去DATA整合性CHK無
        ''' </summary>
        ''' <remarks></remarks>
        Public Const WRITE_ONLY As String = "2"

    End Class


    ''' <summary>
    ''' 取込ファイルの文字コードを設定
    ''' </summary>
    ''' <remarks></remarks>
    Protected ReadOnly _FileEncoding As System.Text.Encoding = _
        System.Text.Encoding.GetEncoding("Shift_JIS")


    ''' <summary>
    ''' EDI取消状態の利用有無を設定
    ''' </summary>
    ''' <remarks></remarks>
    Protected _CancelStatusEnabled As Boolean = False

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        MyBase._Dac = New LMH030DAC211()

    End Sub

#End Region

#Region "Method(出荷登録)"

#Region "出荷登録処理"
    ''' <summary>
    ''' 出荷登録処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function OutkaToroku(ByVal ds As DataSet) As DataSet


        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        'EDI出荷(大)の値取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        If ds.Tables("LMH030_OUTKAEDI_L").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(大)の初期値設定
        ds = Me.SetEdiLShoki(ds)

        '届先自動追加チェック
        Dim sFlag17 As String = ds.Tables("LMH030INOUT").Rows(0).Item("FLAG_17").ToString()
        If (sFlag17.Equals(LMConst.FLG.OFF)) AndAlso (ds.Tables("LMH030_M_DEST").Rows.Count = 0) Then
            'エラーメッセージ
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(中)の値取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

        If ds.Tables("LMH030_OUTKAEDI_M").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(大)の初期値設定後の関連チェック
        If Me.EdiLKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        'EDI出荷(大)の初期値設定後のDB存在チェック
        If Me.EdiLDbExistsCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        '届先コードの初期値設定
        ds = Me.SetDestCd(ds)

        'EDI出荷(中)の初期値設定後のマスタ存在チェック
        If Me.EdiMMasterExistsCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        'EDI出荷(中)の初期値設定後の関連チェック
        If Me.EdiMKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        ' まとめ処理のワーニング表示可否フラグ(0,1,9)
        Dim autoMatomeF As String = ds.Tables("LMH030INOUT").Rows(0)("AUTO_MATOME_FLG").ToString()
        Dim matomeNo As String = String.Empty
        Dim matomeFlg As Boolean = False
        Dim UnsoMatomeFlg As Boolean = False

        '自動まとめフラグ = "0" or "1"の場合、まとめ処理
        If autoMatomeF.Equals("0") OrElse autoMatomeF.Equals("1") Then

            'まとめ先取得
            ds = MyBase.CallDAC(Me._DacCom, "SelectMatomeTarget", ds)

            If MyBase.GetResultCount = 0 Then
                'まとめ先が無い場合、通常登録
                matomeFlg = False

            ElseIf MyBase.GetResultCount > 1 Then
                choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.STD_WID_L001, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    'まとめ対象だったデータを出したい場合はコメントをはずす
                    'Dim matomeTargetNo As String = Me.matomesakiOutkaNo(ds)
                    msgArray(1) = "出荷管理番号(大)"
                    msgArray(2) = "出荷"
                    msgArray(3) = "注意)進捗区分が同一の場合は、管理番号が若い方にまとまります。"
                    msgArray(4) = String.Empty
                    matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                    ds = Me._Blc.SetComWarningL("W199", LMH030BLC.STD_WID_L001, ds, msgArray, matomeNo, String.Empty)
                    Return ds

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    '自動まとめ処理を行う
                    matomeFlg = True
                End If

            ElseIf autoMatomeF.Equals("0") = True Then

                choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.STD_WID_L002, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    msgArray(1) = "出荷管理番号(大)"
                    msgArray(2) = String.Empty
                    msgArray(3) = String.Empty
                    msgArray(4) = String.Empty
                    matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                    ds = Me._Blc.SetComWarningL("W168", LMH030BLC.STD_WID_L002, ds, msgArray, matomeNo, String.Empty)
                    Return ds

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    '自動まとめ処理を行う
                    matomeFlg = True

                ElseIf choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return ds

                End If

            ElseIf autoMatomeF.Equals("1") = True Then
                Dim dtMatome As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")

                Dim matomeStatus As String = dtMatome.Rows(0).Item("OUTKA_STATE_KB").ToString()

                If matomeStatus.Equals("10") = False Then

                    choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.STD_WID_L003, 0)

                    '進捗区分が予定入力より先になっているのでワーニングを出力
                    If String.IsNullOrEmpty(choiceKb) = True Then
                        msgArray(1) = "出荷管理番号(大)"
                        msgArray(2) = "出荷"
                        msgArray(3) = String.Empty
                        msgArray(4) = String.Empty
                        matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                        ds = Me._Blc.SetComWarningL("W198", LMH030BLC.STD_WID_L003, ds, msgArray, matomeNo, String.Empty)
                        Return ds

                    ElseIf choiceKb.Equals("01") = True Then
                        'ワーニングで"はい"を選択時
                        '自動まとめ処理を行う
                        matomeFlg = True

                    End If

                Else
                    'まとめ処理を行う
                    matomeFlg = True
                End If

            End If
        End If
        '追加箇所 20110824 end

        '出荷管理番号(大)の採番
        ds = Me.GetOutkaNoL(ds, matomeFlg)

        ''出荷管理番号(中)の採番
        ds = Me.GetOutkaNoM(ds, matomeFlg)

        '出荷(大)データセット設定
        ds = Me.SetDatasetOutkaL(ds, matomeFlg)

        '出荷(中)データセット設定
        ds = Me.SetDatasetOutkaM(ds, matomeFlg)

        If ds.Tables("LMH030_C_OUTKA_M").Rows.Count = 0 Then
            Return ds
        End If

        '作業レコードデータセット設定
        ds = Me.SetDatasetSagyo(ds)

        '運送(大,中)データセット設定
        ds = Me.SetDatasetUnsoL(ds, matomeFlg)
        ds = Me.SetDatasetUnsoM(ds)

        '運送(大)の運送重量をデータセットに再設定
        ds = Me.SetdatasetUnsoJyuryo(ds, matomeFlg)

        'タブレット項目の初期値設定
        ds = MyBase.CallBLC(Me._Blc, "SetDatasetOutnkaLTabletData", ds)

        '出荷登録(通常処理)
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        If matomeFlg = False Then
            '出荷(大)の新規登録
            ds = MyBase.CallDAC(Me._Dac, "InsertOutkaLData", ds)
        Else
            '出荷(大)のまとめ更新
            ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
        End If

        '出荷(中)の新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertOutkaMData", ds)

#If True Then   'ADD 2019/08/02 006202   【LMS】商品に出荷時作業加工区分が登録されている商品のEDI出荷登録できない荷主有り調査、改修依頼
        '作業の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_E_SAGYO").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertSagyoData", ds)
        End If

#End If

        If sFlag17.Equals(LMConst.FLG.ON) = True Then

            '届先マスタの自動追加
            If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
                   AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
                ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "1"
                ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)
                ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "0"
            End If

            '届先マスタの自動更新
            If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
               AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_UPDATE_FLG").Equals("1") = True Then
                ds.Tables("LMH030_M_DEST").Rows(0).Item("UPDATE_TARGET_FLG") = "1"
                ds = MyBase.CallDAC(Me._Dac, "UpdateMDestData", ds)
                If MyBase.GetResultCount = 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return ds
                End If
                ds.Tables("LMH030_M_DEST").Rows(0).Item("UPDATE_TARGET_FLG") = "0"
            End If

        End If

        '運送(大)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_L").Rows.Count <> 0 Then
            If matomeFlg = False Then
                ds = MyBase.CallDAC(Me._Dac, "InsertUnsoLData", ds)
            Else
                ds = MyBase.CallDAC(Me._Dac, "UpdateMatomesakiUnsoLData", ds)
            End If
        End If

        '運送(中)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_M").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoMData", ds)
        End If

        If matomeFlg = True Then
            'まとめ先EDI出荷(大)の更新(まとめ先EDIデータにまとめ番号を設定)
            ds = MyBase.CallDAC(Me._Dac, "UpdateMatomesakiEdiLData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
        End If

        Return ds

    End Function
#End Region




#Region "EDI出荷(中)のDAC側でのチェック + 初期値設定"
    ''' <summary>
    ''' EDI出荷(中)のDAC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function EdiMMasterExistsCheck(ByVal ds As DataSet _
                                                     , ByVal rowNo As String _
                                                     , ByVal ediCtlNo As String) As Boolean

        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim max As Integer = dtM.Rows.Count - 1
        Dim unsoData As String = String.Empty
        Dim custGoodsCd As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")
        Dim setDtM As DataTable = setDs.Tables("LMH030_OUTKAEDI_M")
        Dim dtGooDs As DataTable = setDs.Tables("LMH030_M_GOODS")

        Dim flgWarning As Boolean = False

        For i As Integer = 0 To max

            custGoodsCd = dtM.Rows(i)("CUST_GOODS_CD").ToString()

            If String.IsNullOrEmpty(custGoodsCd) = False Then

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDtL.ImportRow(dtL.Rows(0))
                setDtM.ImportRow(dtM.Rows(i))

                choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.NKS_WID_M001, 0)

                If choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                End If

                '商品マスタ検索（NRS商品コード or 荷主商品コード）
                setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsOutka", setDs))

                If MyBase.GetResultCount = 0 Then
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                ElseIf GetResultCount() > 1 Then

                    '入目 + 荷主商品コードで再検索
                    setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsIrimeOutka", setDs))

                    If MyBase.GetResultCount = 1 Then
                    Else
                        '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                        msgArray(1) = String.Empty
                        msgArray(2) = String.Empty
                        msgArray(3) = String.Empty
                        msgArray(4) = String.Empty

                        '2012.03.19 修正START
                        ds = Me._Blc.SetComWarningM("W162", LMH030BLC.NKS_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)
                        '2012.03.19 修正END

                        flgWarning = True 'ワーニングフラグをたてて処理続行

                        Continue For
                    End If

                End If

                'EDI出荷(中)の初期値設定処理
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                    'エラー/ワーニング設定
                    Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                    If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                        '整合性チェックでエラーがあった場合は処理終了
                        Return False
                    Else
                        '整合性チェックでワーニングがあった場合は、flgWarning=True
                        flgWarning = True
                    End If

                End If

                '運送重量取得用項目をデータセット(EDI出荷(中))に格納
                If Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i, rowNo, ediCtlNo) = False Then
                    Return False
                End If

            Else
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"商品コード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        Next

        '----------------------------------------------------------------------------------------------------------
        'ワーニングがある場合はマスタから商品が選択できていない為、処理をつづけるとデータによってはアベンドする。
        'そのため中データのループが終わり、ワーニングがある（flgWarning=True）場合は処理を終了させる
        '-----------------------------------------------------------------------------------------------------------
        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If

        Return True

    End Function

#End Region


#Region "データセット設定"

#Region "データセット設定(出荷L)"
    ''' <summary>
    ''' データセット設定(出荷L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function SetDatasetOutkaL(ByVal ds As DataSet _
                                                , ByVal matomeFlg As Boolean) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim outkaDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").NewRow()
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim matomesakiDt As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")

        If matomeFlg = False Then
            '通常登録処理
            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_KB") = ediDr("OUTKA_KB")
            outkaDr("SYUBETU_KB") = ediDr("SYUBETU_KB")
            outkaDr("OUTKA_STATE_KB") = ediDr("OUTKA_STATE_KB")
            outkaDr("OUTKAHOKOKU_YN") = FormatZero(ediDr("OUTKAHOKOKU_YN").ToString(), 2)
            outkaDr("PICK_KB") = ediDr("PICK_KB")
            outkaDr("DENP_NO") = String.Empty
            outkaDr("ARR_KANRYO_INFO") = String.Empty
            outkaDr("WH_CD") = ediDr("WH_CD")
            outkaDr("OUTKA_PLAN_DATE") = ediDr("OUTKA_PLAN_DATE")
            outkaDr("OUTKO_DATE") = ediDr("OUTKO_DATE")
            outkaDr("ARR_PLAN_DATE") = ediDr("ARR_PLAN_DATE")
            outkaDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
            outkaDr("HOKOKU_DATE") = ediDr("HOKOKU_DATE")
            outkaDr("TOUKI_HOKAN_YN") = FormatZero(ediDr("TOUKI_HOKAN_YN").ToString(), 2)
            outkaDr("END_DATE") = String.Empty
            outkaDr("CUST_CD_L") = ediDr("CUST_CD_L")
            outkaDr("CUST_CD_M") = ediDr("CUST_CD_M")
            outkaDr("SHIP_CD_L") = ediDr("SHIP_CD_L")
            outkaDr("SHIP_CD_M") = String.Empty


            outkaDr("CUST_ORD_NO") = ediDr("CUST_ORD_NO")
            outkaDr("BUYER_ORD_NO") = ediDr("BUYER_ORD_NO")


            If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                Dim destMDr As DataRow = ds.Tables("LMH030_M_DEST").Rows(0)
                outkaDr("DEST_CD") = destMDr("DEST_CD")
                outkaDr("DEST_AD_3") = destMDr("AD_3")
                outkaDr("DEST_TEL") = destMDr("TEL")
                outkaDr("DEST_KB") = "00"
                outkaDr("DEST_NM") = destMDr("DEST_NM")
                outkaDr("DEST_AD_1") = destMDr("AD_1")
                outkaDr("DEST_AD_2") = destMDr("AD_2")

            Else
                outkaDr("DEST_CD") = ediDr("DEST_CD")
                outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")
                outkaDr("DEST_TEL") = ediDr("DEST_TEL")
                outkaDr("DEST_KB") = "02"
                outkaDr("DEST_NM") = ediDr("DEST_NM")
                outkaDr("DEST_AD_1") = ediDr("DEST_AD_1")
                outkaDr("DEST_AD_2") = ediDr("DEST_AD_2")
            End If

            outkaDr("NHS_REMARK") = String.Empty
            outkaDr("SP_NHS_KB") = ediDr("SP_NHS_KB")
            outkaDr("COA_YN") = FormatZero(ediDr("COA_YN").ToString(), 2)
            outkaDr("REMARK") = ediDr("REMARK")
            outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt)
            outkaDr("DENP_YN") = FormatZero(ediDr("DENP_YN").ToString(), 2)
            outkaDr("PC_KB") = ediDr("PC_KB")
            outkaDr("UNCHIN_YN") = FormatZero(ediDr("UNCHIN_YN").ToString(), 2)
            outkaDr("NIYAKU_YN") = FormatZero(ediDr("NIYAKU_YN").ToString(), 2)
            outkaDr("ALL_PRINT_FLAG") = "00"
            outkaDr("NIHUDA_FLAG") = "00"
            outkaDr("NHS_FLAG") = "00"
            outkaDr("DENP_FLAG") = "00"
            outkaDr("COA_FLAG") = "00"
            outkaDr("HOKOKU_FLAG") = "00"
            outkaDr("MATOME_PICK_FLAG") = "00"
            outkaDr("LAST_PRINT_DATE") = String.Empty
            outkaDr("LAST_PRINT_TIME") = String.Empty
            outkaDr("SASZ_USER") = String.Empty
            outkaDr("OUTKO_USER") = String.Empty
            outkaDr("KEN_USER") = String.Empty
            outkaDr("OUTKA_USER") = String.Empty
            outkaDr("HOU_USER") = String.Empty
            outkaDr("ORDER_TYPE") = String.Empty
            outkaDr("SYS_DEL_FLG") = "0"
            'outkaDr("DEST_KB") = "02"
            'outkaDr("DEST_NM") = ediDr("DEST_NM")
            'outkaDr("DEST_AD_1") = ediDr("DEST_AD_1")
            'outkaDr("DEST_AD_2") = ediDr("DEST_AD_2")
        Else
            'まとめ登録処理
            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt) + Convert.ToDouble(matomesakiDt.Rows(0)("OUTKA_PKG_NB"))
            outkaDr("SYS_UPD_DATE") = matomesakiDt.Rows(0)("SYS_UPD_DATE")
            outkaDr("SYS_UPD_TIME") = matomesakiDt.Rows(0)("SYS_UPD_TIME")

            '先方区切りバイト数(区分マスタ)
            Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0).Item("EVENT_SHUBETSU").ToString()
            ds.Tables("LMH030_JUDGE").Clear()
            Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").NewRow()
            Dim delimiterByte As Integer = 30
            drJudge("EVENT_SHUBETSU") = eventShubetsu
            drJudge("KBN_GROUP_CD") = "D027"
            drJudge("KBN_CD") = String.Concat(ediDr("NRS_BR_CD"), ediDr("CUST_CD_L"), ediDr("CUST_CD_M"))
            ds.Tables("LMH030_JUDGE").Rows.Add(drJudge)

            ds = MyBase.CallDAC(Me._Dac, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 1 Then
                delimiterByte = Convert.ToInt32(ds.Tables("LMH030_Z_KBN").Rows(0).Item("NISUGATA"))
            End If

            '2014/04/08 (黎) まとめ処理時のオーダー番号まとめ --ST--
            If String.IsNullOrEmpty(matomesakiDt.Rows(0)("CUST_ORD_NO").ToString()) = False Then
                '2015.05.15 テルモ要望対応　修正START
                'outkaDr("CUST_ORD_NO") = Me._Blc.LeftB(String.Concat(matomesakiDt.Rows(0)("CUST_ORD_NO"), ",", ediDr("CUST_ORD_NO")), 30)
                outkaDr("CUST_ORD_NO") = Me._Blc.LeftB(String.Concat(matomesakiDt.Rows(0)("CUST_ORD_NO"), ",", Right(ediDr("CUST_ORD_NO").ToString(), delimiterByte)), 30)
                '2015.05.15 テルモ要望対応　修正END
            ElseIf String.IsNullOrEmpty(ediDr("CUST_ORD_NO").ToString()) = False Then
                outkaDr("CUST_ORD_NO") = Me._Blc.LeftB(ediDr("CUST_ORD_NO").ToString(), 30)
            End If
            '2014/04/08 (黎) まとめ処理時のオーダー番号まとめ --ED--

        End If
        'データセットに設定
        ds.Tables("LMH030_C_OUTKA_L").Rows.Add(outkaDr)

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷M)"
    ''' <summary>
    ''' データセット設定(出荷M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function SetDatasetOutkaM(ByVal ds As DataSet, ByVal matomeflg As Boolean) As DataSet
        '要望番号:1712 umano 2013.01.11 START
        'Protected Overloads Function SetDatasetOutkaM(ByVal ds As DataSet) As DataSet
        '要望番号:1712 umano 2013.01.11 END

        Dim ediDr As DataRow
        Dim outkaDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim remark As String = String.Empty
        Dim SetNo As String = String.Empty
        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim max As Integer = dt.Rows.Count - 1
        Dim calcPkgModNb As Long = 0
        Dim calcPkgQuoNb As Double = 0

        '要望番号:1712 umano 2013.01.11 START
        Dim matomesakiDt As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")
        '要望番号:1712 umano 2013.01.11 END


        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            outkaDr = ds.Tables("LMH030_C_OUTKA_M").NewRow()

            If String.IsNullOrEmpty(dt.Rows(i)("PKG_NB").ToString()) = False _
            AndAlso String.IsNullOrEmpty(dt.Rows(i)("OUTKA_TTL_NB").ToString()) = False _
            AndAlso (dt.Rows(i)("PKG_NB").ToString()).Equals("0") = False _
            AndAlso (dt.Rows(i)("OUTKA_TTL_NB").ToString()).Equals("0") = False Then

                calcPkgModNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) / Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Math.Floor(calcPkgQuoNb)
            End If

            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediLDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_NO_M") = ediDr("OUTKA_CTL_NO_CHU")
            If ediDr("SET_KB").ToString = "2" Then
                outkaDr("EDI_SET_NO") = ediDr("FREE_C10")
            Else
                outkaDr("EDI_SET_NO") = String.Empty
            End If
            outkaDr("COA_YN") = FormatZero(ediDr("COA_YN").ToString(), 2)
            '要望番号:1712 umano 2013.01.11 START
            'EDI出荷(大)のオーダ番号とEDI出荷(中)のオーダ番号が同一である場合、出荷(中)のオーダ番号を空で登録する。
            'ただしまとめた場合はまとめ先のEDI出荷(大)を参照する
            'outkaDr("CUST_ORD_NO_DTL") = ediDr("CUST_ORD_NO_DTL")
            Dim sCustOrdNo As String = vbNullString
            If matomeflg Then   'まとめの場合
                sCustOrdNo = matomesakiDt.Rows(0)("CUST_ORD_NO").ToString   'まとめ先のEDI出荷(大)を参照
            Else                'まとめでない場合
                '2015.06.29 協立化学　修正START
                'sCustOrdNo = ediLDr("CUST_ORD_NO").ToString                 '自分のEDI出荷(大)を参照
                sCustOrdNo = String.Empty
                '2015.06.29 協立化学　修正END
            End If

            If ediDr("CUST_ORD_NO_DTL").ToString = sCustOrdNo Then
                outkaDr("CUST_ORD_NO_DTL") = vbNullString
            Else
                outkaDr("CUST_ORD_NO_DTL") = ediDr("CUST_ORD_NO_DTL")
            End If
            '要望番号:1712 umano 2013.01.11 END

            '要望番号:1712 umano 2013.01.11 START
            'EDI出荷(大)の注文番号とEDI出荷(中)の注文番号が同一である場合、出荷(中)の注文番号を空で登録する。
            'outkaDr("BUYER_ORD_NO_DTL") = ediDr("BUYER_ORD_NO_DTL")
            'ただしまとめた場合はまとめ先のEDI出荷(大)を参照する
            Dim sBuyerOrdNo As String = vbNullString
            If matomeflg Then   'まとめの場合
                sBuyerOrdNo = matomesakiDt.Rows(0)("BUYER_ORD_NO").ToString 'まとめ先のEDI出荷(大)を参照
            Else                'まとめでない場合
                '2015.06.29 協立化学　修正START
                'sBuyerOrdNo = ediLDr("BUYER_ORD_NO").ToString               '自分のEDI出荷(大)を参照
                sBuyerOrdNo = String.Empty
                '2015.06.29 協立化学　修正END
            End If

            If ediDr("BUYER_ORD_NO_DTL").ToString = sBuyerOrdNo Then
                outkaDr("BUYER_ORD_NO_DTL") = vbNullString
            Else
                outkaDr("BUYER_ORD_NO_DTL") = ediDr("BUYER_ORD_NO_DTL")
            End If
            '要望番号:1712  umano 2013.01.11 END
            outkaDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            outkaDr("RSV_NO") = ediDr("RSV_NO")
            outkaDr("LOT_NO") = ediDr("LOT_NO")
            outkaDr("SERIAL_NO") = ediDr("SERIAL_NO")
            outkaDr("ALCTD_KB") = ediDr("ALCTD_KB")
            outkaDr("OUTKA_PKG_NB") = ediDr("PKG_NB")

            outkaDr("OUTKA_QT") = Convert.ToInt32(ediDr("OUTKA_HASU")) * Convert.ToDecimal(ediDr("IRIME"))
            outkaDr("OUTKA_TTL_QT") = Convert.ToInt32(ediDr("OUTKA_HASU")) * Convert.ToDecimal(ediDr("IRIME"))
            outkaDr("BACKLOG_NB") = Convert.ToInt32(ediDr("OUTKA_HASU"))
            outkaDr("BACKLOG_QT") = Convert.ToInt32(ediDr("OUTKA_HASU")) * Convert.ToDecimal(ediDr("IRIME"))
            outkaDr("OUTKA_TTL_NB") = Convert.ToInt32(ediDr("OUTKA_HASU"))
            outkaDr("OUTKA_HASU") = Convert.ToInt32(ediDr("OUTKA_HASU"))
            outkaDr("CUST_ORD_NO_DTL") = ediDr("CUST_ORD_NO_DTL")

            outkaDr("ALCTD_NB") = 0
            outkaDr("ALCTD_QT") = 0
            outkaDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
            outkaDr("IRIME") = ediDr("IRIME")
            outkaDr("IRIME_UT") = ediDr("IRIME_UT")

            If Convert.ToInt64(dt.Rows(i)("PKG_NB")) = 0 Then
                outkaDr("OUTKA_M_PKG_NB") = 0
            Else
                If 0 = calcPkgModNb Then
                    outkaDr("OUTKA_M_PKG_NB") = calcPkgQuoNb
                Else
                    outkaDr("OUTKA_M_PKG_NB") = calcPkgQuoNb + 1
                End If
            End If

            If Convert.ToInt64(outkaDr("OUTKA_M_PKG_NB")) > 999 Then
                outkaDr("OUTKA_M_PKG_NB") = 1
            End If

            outkaDr("REMARK") = ediDr("REMARK")
            outkaDr("SIZE_KB") = String.Empty
            outkaDr("ZAIKO_KB") = String.Empty
            outkaDr("SOURCE_CD") = String.Empty
            outkaDr("YELLOW_CARD") = String.Empty
            outkaDr("PRINT_SORT") = "99"
            outkaDr("SYS_DEL_FLG") = "0"

            'データセットに設定
            ds.Tables("LMH030_C_OUTKA_M").Rows.Add(outkaDr)

        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(運送L)"
    ''' <summary>
    ''' データセット設定(運送L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function SetDatasetUnsoL(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim unsoDr As DataRow = ds.Tables("LMH030_UNSO_L").NewRow()
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim outkaLDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility

        If matomeFlg = False Then
            '通常登録
            unsoDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            unsoDr("UNSO_NO_L") = num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, nrsBrCd)
            unsoDr("YUSO_BR_CD") = ediDr("NRS_BR_CD")
            unsoDr("WH_CD") = ediDr("WH_CD")
            unsoDr("INOUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            unsoDr("TRIP_NO") = String.Empty
            unsoDr("UNSO_CD") = ediDr("UNSO_CD")
            unsoDr("UNSO_BR_CD") = ediDr("UNSO_BR_CD")
            unsoDr("BIN_KB") = ediDr("BIN_KB")
            unsoDr("JIYU_KB") = String.Empty
            unsoDr("DENP_NO") = String.Empty
            unsoDr("OUTKA_PLAN_DATE") = ediDr("OUTKA_PLAN_DATE")
            unsoDr("OUTKA_PLAN_TIME") = String.Empty
            unsoDr("ARR_PLAN_DATE") = ediDr("ARR_PLAN_DATE")
            unsoDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
            unsoDr("ARR_ACT_TIME") = String.Empty
            unsoDr("CUST_CD_L") = ediDr("CUST_CD_L")
            unsoDr("CUST_CD_M") = ediDr("CUST_CD_M")
            unsoDr("CUST_REF_NO") = ediDr("CUST_ORD_NO")
            unsoDr("SHIP_CD") = ediDr("SHIP_CD_L")
            unsoDr("DEST_CD") = ediDr("DEST_CD")
            unsoDr("UNSO_PKG_NB") = outkaLDr("OUTKA_PKG_NB")
            'unsoDr("NB_UT") = ediDr("NB_UT") '運送Mで取得の為ここではコメント
            unsoDr("UNSO_WT") = 0             '運送Mの集計値
            unsoDr("UNSO_ONDO_KB") = ediMDr("UNSO_ONDO_KB")
            '要望番号1476:(出荷登録時、元着払いが〔着払い〕になる(要望番号1457関連)) 2012/09/28 本明 Start
            'unsoDr("PC_KB") = ediDr("PICK_KB")
            unsoDr("PC_KB") = ediDr("PC_KB")
            '要望番号1476:(出荷登録時、元着払いが〔着払い〕になる(要望番号1457関連)) 2012/09/28 本明 End
            unsoDr("TARIFF_BUNRUI_KB") = ediDr("UNSO_TEHAI_KB")
            unsoDr("VCLE_KB") = ediDr("SYARYO_KB")
            unsoDr("MOTO_DATA_KB") = "20"
            unsoDr("TAX_KB") = "01" '課税区分は"01"(課税)固定とする
            unsoDr("REMARK") = ediDr("UNSO_ATT")
            unsoDr("SEIQ_TARIFF_CD") = ediDr("UNCHIN_TARIFF_CD")
            unsoDr("SEIQ_ETARIFF_CD") = ediDr("EXTC_TARIFF_CD")
            unsoDr("AD_3") = outkaLDr("DEST_AD_3")
            unsoDr("UNSO_TEHAI_KB") = ediDr("UNSO_MOTO_KB")
            unsoDr("BUY_CHU_NO") = ediDr("BUYER_ORD_NO")
            unsoDr("AREA_CD") = String.Empty
            unsoDr("TYUKEI_HAISO_FLG") = "00"   '中継配送フラグ"00:無し"を設定
            unsoDr("SYUKA_TYUKEI_CD") = String.Empty
            unsoDr("HAIKA_TYUKEI_CD") = String.Empty
            unsoDr("TRIP_NO_SYUKA") = String.Empty
            unsoDr("TRIP_NO_TYUKEI") = String.Empty
            unsoDr("TRIP_NO_HAIKA") = String.Empty

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString()) = False AndAlso _
               String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString()) = False Then

                '運送会社マスタの取得(支払請求タリフコード,支払割増タリフコード)
                ds = MyBase.CallDAC(Me._DacCom, "SelectListDataShiharaiTariff", ds)
                Dim unsocoMDr As DataRow = ds.Tables("LMH030_SHIHARAI_TARIFF").Rows(0)

                If MyBase.GetResultCount > 0 Then
                    unsoDr("SHIHARAI_TARIFF_CD") = unsocoMDr("UNCHIN_TARIFF_CD")
                    unsoDr("SHIHARAI_ETARIFF_CD") = unsocoMDr("EXTC_TARIFF_CD")
                End If

            End If
            'END UMANO 要望番号1302 支払運賃に伴う修正。
        Else
            'まとめ処理
            Dim matomeDr As DataRow = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)
            unsoDr("NRS_BR_CD") = matomeDr("NRS_BR_CD")
            unsoDr("UNSO_NO_L") = matomeDr("UNSO_NO_L")
            unsoDr("SYS_UPD_DATE") = matomeDr("SYS_UNSO_UPD_DATE")
            unsoDr("SYS_UPD_TIME") = matomeDr("SYS_UNSO_UPD_TIME")

            '運送梱包個数の計算
            Dim unsoPkgNb As Long = 0
            Dim matomesakiUnsoPkgNb As Long = Convert.ToInt64(matomeDr("UNSO_PKG_NB"))
            Dim matomesakiOutkaPkgNb As Long = Convert.ToInt64(matomeDr("OUTKA_PKG_NB"))

            unsoDr("UNSO_PKG_NB") = Convert.ToInt64(outkaLDr("OUTKA_PKG_NB")) + matomesakiUnsoPkgNb - matomesakiOutkaPkgNb

        End If
        'データセットに設定
        ds.Tables("LMH030_UNSO_L").Rows.Add(unsoDr)

        Return ds

    End Function

#End Region

#Region "データセット設定(運送M)"
    ''' <summary>
    ''' データセット設定(運送M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function SetDatasetUnsoM(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim unsoMDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)

        Dim stdWtKgs As Decimal = 0
        Dim irime As Decimal = 0
        Dim stdIrimeNb As Decimal = 0
        Dim nisugata As Decimal = 0
        Dim outkaTtlNb As Decimal = 0

        Dim calcPkgModNb As Long = 0

        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)

            calcPkgModNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt64(dt.Rows(i)("PKG_NB"))


            unsoMDr = ds.Tables("LMH030_UNSO_M").NewRow()

            unsoMDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            unsoMDr("UNSO_NO_L") = unsoLDr("UNSO_NO_L")
            unsoMDr("UNSO_NO_M") = ediDr("OUTKA_CTL_NO_CHU")
            unsoMDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            unsoMDr("GOODS_NM") = ediDr("GOODS_NM")


            unsoMDr("UNSO_TTL_QT") = (Convert.ToInt32(ediDr("OUTKA_HASU")) - calcPkgModNb) * Convert.ToDecimal(ediDr("IRIME"))
            unsoMDr("UNSO_TTL_NB") = Convert.ToInt32(ediDr("OUTKA_HASU")) - calcPkgModNb
            unsoMDr("HASU") = Convert.ToInt32(ediDr("OUTKA_HASU")) - calcPkgModNb

#If True Then 'コメント解除
            unsoMDr("UNSO_TTL_NB") = ediDr("OUTKA_PKG_NB")
            unsoMDr("UNSO_TTL_QT") = ediDr("OUTKA_TTL_QT")
            unsoMDr("HASU") = ediDr("OUTKA_HASU")
#End If
            unsoMDr("NB_UT") = ediDr("KB_UT")
            unsoMDr("QT_UT") = ediDr("QT_UT")
            unsoMDr("ZAI_REC_NO") = String.Empty
            unsoMDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
            unsoMDr("IRIME") = ediDr("IRIME")
            unsoMDr("IRIME_UT") = ediDr("IRIME_UT")

            stdWtKgs = Convert.ToDecimal(ediDr("STD_WT_KGS"))
            irime = Convert.ToDecimal(ediDr("IRIME"))
            stdIrimeNb = Convert.ToDecimal(ediDr("STD_IRIME_NB"))

            If String.IsNullOrEmpty(ediDr("NISUGATA").ToString()) = False Then
                nisugata = Convert.ToDecimal(ediDr("NISUGATA"))
            End If
            outkaTtlNb = Convert.ToDecimal(ediDr("OUTKA_TTL_NB"))

            If ediDr("TARE_YN").Equals("01") = False Then
                unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb)

            Else
                unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb + nisugata)

            End If

            unsoMDr("SIZE_KB") = String.Empty
            unsoMDr("ZBUKA_CD") = String.Empty
            unsoMDr("ABUKA_CD") = String.Empty
            unsoMDr("PKG_NB") = ediDr("PKG_NB")
            unsoMDr("LOT_NO") = ediDr("LOT_NO")
            unsoMDr("REMARK") = ediDr("REMARK")

            'データセットに設定
            ds.Tables("LMH030_UNSO_M").Rows.Add(unsoMDr)
        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷包装個数)"
    Protected Overloads Function SumPkgNb(ByVal dt As DataTable) As Double

        Dim max As Integer = dt.Rows.Count - 1
        Dim sumNb As Double = 0
        Dim calcPkgModNb As Long = 0
        Dim calcPkgQuoNb As Double = 0

        For i As Integer = 0 To max

            If (String.IsNullOrEmpty(dt.Rows(i)("PKG_NB").ToString()) = False AndAlso _
                String.IsNullOrEmpty(dt.Rows(i)("OUTKA_TTL_NB").ToString()) = False AndAlso _
                (dt.Rows(i)("PKG_NB").ToString()).Equals("0") = False AndAlso _
                (dt.Rows(i)("OUTKA_TTL_NB").ToString()).Equals("0") = False
                ) Then

                calcPkgModNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) / Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Math.Floor(calcPkgQuoNb)

            End If

            ' 加算
            sumNb = sumNb + calcPkgQuoNb

            ' 残り入れる箱を用意
            If calcPkgModNb > 0 Then
                sumNb = sumNb + 1
            End If

        Next

        Return sumNb

    End Function
#End Region

#Region "EDI出荷(中)の初期値設定(出荷登録処理)"
    ''' <summary>
    ''' EDI出荷(中)の初期値設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function EdiMDefaultSet(ByVal ds As DataSet, ByVal setDs As DataSet, _
                                                ByVal count As Integer, ByVal unsodata As String, _
                                                ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(count)
        Dim mGoodsDr As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)

        ''-------------------------------------------------------------------------------------
        ''●チェック
        ''-------------------------------------------------------------------------------------

        Dim flgWarning As Boolean = False
        Dim compareWarningFlg As Boolean = False
        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        '商品名(マスタの値で入替は行わない。必ずEDIで送られてくる値を使用)
        'ediMDr("GOODS_NM") = ediMDr("FREE_C04")    '<<商品が見つからないなら素直にFREE_C04を見ること
        'ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1") '<<商品マスタの値で登録(LMSに合わせる) '2015.08.10[Ri] 最上部のコメント通り、塗り替えない！

        '入目単位
        If String.IsNullOrEmpty(ediMDr("IRIME_UT").ToString()) = True Then
            ediMDr("IRIME_UT") = mGoodsDr("STD_IRIME_UT")
        Else
            If unsodata.Equals("01") = False AndAlso ediMDr("IRIME_UT").Equals(mGoodsDr("STD_IRIME_UT")) = False Then
                '運送データ以外でEDI(中)と商品マスタで入目単位が異なる場合、エラー
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"入目単位", "商品マスタ", "入目単位"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '分析表区分
        If String.IsNullOrEmpty(ediMDr("COA_YN").ToString()) = True Then
            ediMDr("COA_YN") = Left(mGoodsDr("COA_YN").ToString, 1)
        End If

        '荷主注文番号(明細単位)
        If String.IsNullOrEmpty(ediMDr("CUST_ORD_NO_DTL").ToString()) = True Then
            ediMDr("CUST_ORD_NO_DTL") = ediLDr("CUST_ORD_NO")
        End If

        '買主注文番号(明細単位)
        If String.IsNullOrEmpty(ediMDr("BUYER_ORD_NO_DTL").ToString()) = True Then
            ediMDr("BUYER_ORD_NO_DTL") = ediLDr("BUYER_ORD_NO")
        End If

        '商品KEY
        If unsodata.Equals("01") = False Then
            ediMDr("NRS_GOODS_CD") = mGoodsDr("GOODS_CD_NRS")
        End If

        '引当単位区分
        If String.IsNullOrEmpty(ediMDr("ALCTD_KB").ToString()) = True Then
            If String.IsNullOrEmpty(mGoodsDr("ALCTD_KB").ToString()) = False Then

                ediMDr("ALCTD_KB") = mGoodsDr("ALCTD_KB")
            Else
                ediMDr("ALCTD_KB") = "01"
            End If
        End If

        '個数単位
        ediMDr("KB_UT") = mGoodsDr("NB_UT")

        '数量単位
        ediMDr("QT_UT") = mGoodsDr("STD_IRIME_UT")

        '包装個数
        ediMDr("PKG_NB") = mGoodsDr("PKG_NB")

        '包装単位
        ediMDr("PKG_UT") = mGoodsDr("PKG_UT")

        '温度区分
        ediMDr("ONDO_KB") = mGoodsDr("ONDO_KB")

        '運送温度区分
        If String.IsNullOrEmpty(ediMDr("UNSO_ONDO_KB").ToString()) = True Then

            If (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) < (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) _
            AndAlso ((ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4)) < (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) _
            OrElse (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) < (ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4))) Then
                ediMDr("UNSO_ONDO_KB") = "90"
            Else
                ediMDr("UNSO_ONDO_KB") = mGoodsDr("UNSO_ONDO_KB")
            End If

            If (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) < (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) _
            AndAlso ((ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4)) < (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) _
            OrElse (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) < (ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4))) Then
                ediMDr("UNSO_ONDO_KB") = "90"
            Else
                ediMDr("UNSO_ONDO_KB") = mGoodsDr("UNSO_ONDO_KB")
            End If
        Else
            '運送温度区分(区分マスタ)
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U006
            drJudge("KBN_CD") = ediMDr("UNSO_ONDO_KB")
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送温度区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        End If

        '入目
        If Convert.ToDecimal(ediMDr("IRIME")) = 0 _
        AndAlso Convert.ToDecimal(mGoodsDr("STD_IRIME_NB")) <> 0 Then
            ediMDr("IRIME") = mGoodsDr("STD_IRIME_NB")
        End If

        '出荷包装個数
        '出荷端数
        '※　セミEDIで取り込んだ数値をそのまま使用
        Dim pkgNb As Double = Convert.ToDouble(ediMDr("PKG_NB"))
        Dim outkaPkgNb As Double = Convert.ToDouble(ediMDr("OUTKA_PKG_NB"))
        Dim outkaHasu As Double = Convert.ToDouble(ediMDr("OUTKA_HASU"))
        Dim alctdKb As String = ediMDr("ALCTD_KB").ToString
        Dim irime As Double = Convert.ToDouble(ediMDr("IRIME"))
        Dim outkaTtlQt As Double = Convert.ToDouble(ediMDr("OUTKA_TTL_QT"))

        '数値01(FREE_N01)
        ediMDr("FREE_N01") = mGoodsDr("PLT_PER_PKG_UT")

        '出荷数量
        '出荷総数量
        If outkaTtlQt <= 0 Then
            ediMDr("OUTKA_QT") = Convert.ToInt32(ediMDr("OUTKA_TTL_NB")) * irime
            ediMDr("OUTKA_TTL_QT") = Convert.ToInt32(ediMDr("OUTKA_TTL_NB")) * irime
        End If

        '個別重量(KGS)
        If unsodata.Equals("01") = False Then
            ediMDr("BETU_WT") = mGoodsDr("STD_WT_KGS")
        End If

        '出荷時加工作業区分1-5
        ediMDr("OUTKA_KAKO_SAGYO_KB_1") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_1")
        ediMDr("OUTKA_KAKO_SAGYO_KB_2") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_2")
        ediMDr("OUTKA_KAKO_SAGYO_KB_3") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_3")
        ediMDr("OUTKA_KAKO_SAGYO_KB_4") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_4")
        ediMDr("OUTKA_KAKO_SAGYO_KB_5") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_5")

        'ワーニングが存在する場合はここでの判定はFalseで返す
        If compareWarningFlg = True Then
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

#End Region

#Region "Method(セミEDI)"
#Region "データセット設定"
#Region "セミEDI時　データセット設定(EDI出荷(大))"
    ''' <summary>
    ''' データセット設定(EDI出荷(大)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function SetSemiOutkaEdiL(ByVal setDs As DataSet _
                                                , ByVal rowNo As Integer _
                                                , ByRef sEdiCtlNo As String _
                                                , ByRef iEdiCtlNoChu As Integer _
                                                , ByVal iFindOutkaEdiFlg As Boolean _
                                                , ByVal iCntDiffFlg As Boolean _
                                                ) As DataSet

        Dim drOutkaEdiL As DataRow = setDs.Tables("LMH030_OUTKAEDI_L").NewRow()
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(rowNo)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        Dim dtDestMst As DataTable = setDs.Tables("LMH030_M_DEST")
        Dim drDestMst As DataRow = Nothing

        If dtDestMst.Rows.Count > 0 Then
            drDestMst = dtDestMst.Rows(0)
        End If

        Dim sTempColNo As String = String.Empty
        Dim sKey As String = String.Empty

        '対象行のキャンセル情報取得
        Dim delKbColumnName As String = String.Empty
        Dim isCancelData As Boolean = (isNullColKey(drSemiEdiInfo, "L_DEL_KB_NO", delKbColumnName) AndAlso _
                                       (drRcvEdiDtl.Item(delKbColumnName).ToString().Trim().Length > 0))

        ' 削除区分設定
        If iFindOutkaEdiFlg = True Then
            '出荷登録済みの場合

            ' 削除
            drOutkaEdiL("DEL_KB") = DEL_KB_VALUES.DELETE
            drOutkaEdiL("SYS_DEL_FLG") = LMConst.FLG.ON

#If False Then ' 協立(国内)は設定を問わず、キャンセルを取り込まない仕様とする
            If isCancelData = True AndAlso _CancelStatusEnabled = True Then

                drOutkaEdiL.Item("DEL_KB") = DEL_KB_VALUES.CANCEL
                drOutkaEdiL.Item("SYS_DEL_FLG") = LMConst.FLG.ON
            ElseIf iCntDiffFlg = True Then
#Else
            If iCntDiffFlg = True Then
#End If
                ' 登録済みのデータと異なる場合は、保留で登録する
                drOutkaEdiL("DEL_KB") = DEL_KB_VALUES.RESERVATION
                drOutkaEdiL("SYS_DEL_FLG") = LMConst.FLG.OFF

            End If

        Else

            If isCancelData = True Then
#If False Then ' 協立(国内)は設定を問わず、キャンセルを取り込まない仕様とする
                If (_CancelStatusEnabled = True) Then
                    drOutkaEdiL.Item("DEL_KB") = DEL_KB_VALUES.DELETE
                    drOutkaEdiL.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                Else
                    ' 取り込んだファイルのセルのデータが削除フラグとして設定される。
                    drOutkaEdiL.Item("DEL_KB") = drRcvEdiDtl.Item(delKbColumnName).ToString().Trim()
                    drOutkaEdiL.Item("SYS_DEL_FLG") = drRcvEdiDtl.Item(delKbColumnName).ToString().Trim()
                End If
#Else
                drOutkaEdiL.Item("DEL_KB") = DEL_KB_VALUES.DELETE
                drOutkaEdiL.Item("SYS_DEL_FLG") = LMConst.FLG.ON
#End If
            Else
                drOutkaEdiL.Item("DEL_KB") = DEL_KB_VALUES.NORMAL
                drOutkaEdiL.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
            End If

        End If

        '営業所
        drOutkaEdiL("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")

        'EDI管理番号
        drOutkaEdiL("EDI_CTL_NO") = sEdiCtlNo

        '出荷管理番号
        drOutkaEdiL("OUTKA_CTL_NO") = String.Empty

        '出荷区分
        drOutkaEdiL("OUTKA_KB") = "10"

        '種別区分
        drOutkaEdiL("SYUBETU_KB") = "10"

        '内外区分
        drOutkaEdiL("NAIGAI_KB") = String.Empty

        '作業進捗区分
        drOutkaEdiL("OUTKA_STATE_KB") = "10"

        '作業報告有無
        drOutkaEdiL("OUTKAHOKOKU_YN") = String.Empty

        'ピッキンング区分
        drOutkaEdiL("PICK_KB") = String.Empty

        '営業所名
        drOutkaEdiL("NRS_BR_NM") = String.Empty

        '倉庫コード
        drOutkaEdiL("WH_CD") = drSemiEdiInfo.Item("WH_CD").ToString()

        '倉庫名
        drOutkaEdiL("WH_NM") = String.Empty

        '納入予定日
        sKey = "L_ARR_PLAN_DATE_NO"
        Dim arrplandate As String = String.Empty
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            arrplandate = drRcvEdiDtl(sTempColNo).ToString()
            If drRcvEdiDtl(sTempColNo).ToString().Trim().IndexOf("/") > 0 AndAlso _
               drRcvEdiDtl(sTempColNo).ToString().Trim().Length <= 10 Then
                Dim sDate As String = Convert.ToDateTime(drRcvEdiDtl(sTempColNo).ToString()).ToString("yyyy/MM/dd")
                Dim temStr As String() = sDate.Trim().Split("/"c)
                'Dim temStr As String() = drRcvEdiDtl(sTempColNo).ToString().Trim().Split("/"c)
                drOutkaEdiL("ARR_PLAN_DATE") = temStr(0).PadLeft(4, "0"c) & temStr(1).PadLeft(2, "0"c) & temStr(2).PadLeft(2, "0"c)
            Else
                drOutkaEdiL("ARR_PLAN_DATE") = drRcvEdiDtl(sTempColNo).ToString().Trim().Replace("/", "")
                If drOutkaEdiL("ARR_PLAN_DATE").ToString.Length > 8 Then drOutkaEdiL("ARR_PLAN_DATE") = Left(drOutkaEdiL("ARR_PLAN_DATE").ToString, 8)
            End If
        End If

        '出荷日
        sKey = "L_OUTKA_PLAN_DATE_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            If drRcvEdiDtl(sTempColNo).ToString().Trim().IndexOf("/") > 0 AndAlso _
               drRcvEdiDtl(sTempColNo).ToString().Trim().Length <= 10 Then
                Dim sDate As String = Convert.ToDateTime(drRcvEdiDtl(sTempColNo).ToString()).ToString("yyyy/MM/dd")
                Dim temStr As String() = sDate.Trim().Split("/"c)
                'Dim temStr As String() = drRcvEdiDtl(sTempColNo).ToString().Trim().Split("/"c)
                drOutkaEdiL("OUTKA_PLAN_DATE") = temStr(0).PadLeft(4, "0"c) & temStr(1).PadLeft(2, "0"c) & temStr(2).PadLeft(2, "0"c)
            Else
                drOutkaEdiL("OUTKA_PLAN_DATE") = drRcvEdiDtl(sTempColNo).ToString().Trim().Replace("/", "")
                If drOutkaEdiL("OUTKA_PLAN_DATE").ToString.Length > 8 Then drOutkaEdiL("OUTKA_PLAN_DATE") = Left(drOutkaEdiL("OUTKA_PLAN_DATE").ToString, 8)
            End If
        ElseIf Weekday(Convert.ToDateTime(arrplandate)) = 2 Then
            drOutkaEdiL("OUTKA_PLAN_DATE") = Left(Convert.ToString(Me.GetBussinessDayKrt(drOutkaEdiL("ARR_PLAN_DATE").ToString(), 0, setDs)).Replace("/", String.Empty), 8)
        Else
            drOutkaEdiL("OUTKA_PLAN_DATE") = Left(Convert.ToString(Me.GetBussinessDayKrt(drOutkaEdiL("ARR_PLAN_DATE").ToString(), -1, setDs)).Replace("/", String.Empty), 8)
        End If

        '出庫日
        sKey = "L_OUTKA_PLAN_DATE_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            If drRcvEdiDtl(sTempColNo).ToString().Trim().IndexOf("/") > 0 AndAlso _
               drRcvEdiDtl(sTempColNo).ToString().Trim().Length <= 10 Then
                Dim sDate As String = Convert.ToDateTime(drRcvEdiDtl(sTempColNo).ToString()).ToString("yyyy/MM/dd")
                Dim temStr As String() = sDate.Trim().Split("/"c)
                'Dim temStr As String() = drRcvEdiDtl(sTempColNo).ToString().Trim().Split("/"c)
                drOutkaEdiL("OUTKO_DATE") = temStr(0).PadLeft(4, "0"c) & temStr(1).PadLeft(2, "0"c) & temStr(2).PadLeft(2, "0"c)
            Else
                drOutkaEdiL("OUTKO_DATE") = drRcvEdiDtl(sTempColNo).ToString().Trim().Replace("/", "")
                If drOutkaEdiL("OUTKO_DATE").ToString.Length > 8 Then drOutkaEdiL("OUTKO_DATE") = Left(drOutkaEdiL("OUTKO_DATE").ToString, 8)
            End If
        ElseIf Weekday(Convert.ToDateTime(arrplandate)) = 2 Then
            drOutkaEdiL("OUTKO_DATE") = Left(Convert.ToString(Me.GetBussinessDayKrt(drOutkaEdiL("ARR_PLAN_DATE").ToString(), 0, setDs)).Replace("/", String.Empty), 8)
        Else
            drOutkaEdiL("OUTKO_DATE") = Left(Convert.ToString(Me.GetBussinessDayKrt(drOutkaEdiL("ARR_PLAN_DATE").ToString(), -1, setDs)).Replace("/", String.Empty), 8)
        End If

        '納入予定時刻
        If Weekday(Convert.ToDateTime(arrplandate)) = 7 Then
            drOutkaEdiL("ARR_PLAN_TIME") = "09"
        Else
            drOutkaEdiL("ARR_PLAN_TIME") = "02"
        End If

        drOutkaEdiL("ARR_PLAN_TIME") = String.Empty

        '出荷報告日
        drOutkaEdiL("HOKOKU_DATE") = String.Empty

        '冬季保管量負担有無
        drOutkaEdiL("TOUKI_HOKAN_YN") = "1"

        '荷主L
        drOutkaEdiL("CUST_CD_L") = drSemiEdiInfo.Item("CUST_CD_L").ToString()

        '荷主M
        drOutkaEdiL("CUST_CD_M") = drSemiEdiInfo.Item("CUST_CD_M").ToString()

        '荷主L名称
        drOutkaEdiL("CUST_NM_L") = String.Empty

        '荷主M名称
        drOutkaEdiL("CUST_NM_M") = String.Empty

        '荷送りL
        drOutkaEdiL("SHIP_CD_L") = String.Empty

        '荷送りM
        drOutkaEdiL("SHIP_CD_M") = String.Empty

        '荷送りL名称
        drOutkaEdiL("SHIP_NM_L") = String.Empty

        '荷送りM名称
        drOutkaEdiL("SHIP_NM_M") = String.Empty

        '売上先コード
        sKey = "L_SHIP_CD_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("SHIP_CD_L") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        'EDI届先
        sKey = "L_DEST_CD_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("EDI_DEST_CD") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        '届先
        sKey = "L_DEST_CD_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("DEST_CD") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        '届先名称
        sKey = "L_DEST_NM_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            If ("COLUMN_MST").Equals(sTempColNo) Then
                If setDs.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                    drOutkaEdiL("DEST_NM") = setDs.Tables("LMH030_M_DEST").Rows(0).Item("DEST_NM").ToString
                End If
            Else
                drOutkaEdiL("DEST_NM") = drRcvEdiDtl(sTempColNo).ToString().Trim()

            End If


        End If

        '届先郵便番号
        sKey = "L_DEST_ZIP_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            If ("COLUMN_MST").Equals(sTempColNo) Then
                If setDs.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                    drOutkaEdiL("DEST_ZIP") = setDs.Tables("LMH030_M_DEST").Rows(0).Item("ZIP").ToString
                End If
            Else
                drOutkaEdiL("DEST_ZIP") = drRcvEdiDtl(sTempColNo).ToString().Trim()
                If InStr(drOutkaEdiL.Item("DEST_ZIP").ToString(), Space(1)) > 0 Then
                    drOutkaEdiL.Item("DEST_ZIP") = _Blc.LeftB(drOutkaEdiL.Item("DEST_ZIP").ToString(), InStr(drOutkaEdiL.Item("DEST_ZIP").ToString(), Space(1))).Replace("〒", String.Empty)

                Else
                    drOutkaEdiL.Item("DEST_ZIP") = _Blc.LeftB(drOutkaEdiL.Item("DEST_ZIP").ToString(), 10).Replace("〒", String.Empty)

                End If

            End If

        End If

        '届先住所1
        sKey = "L_DEST_AD_1_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            If ("COLUMN_MST").Equals(sTempColNo) Then
                If setDs.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                    drOutkaEdiL("DEST_AD_1") = setDs.Tables("LMH030_M_DEST").Rows(0).Item("AD_1").ToString
                End If
            Else
                drOutkaEdiL("DEST_AD_1") = drRcvEdiDtl(sTempColNo).ToString().Trim()
                If InStr(drOutkaEdiL.Item("DEST_AD_1").ToString(), Space(1)) > 0 Then
                    If InStr(drOutkaEdiL.Item("DEST_AD_1").ToString(), "　") = 0 Then
                        drOutkaEdiL.Item("DEST_AD_1") = _Blc.LeftB(drOutkaEdiL.Item("DEST_AD_1").ToString().Substring(InStr(drOutkaEdiL.Item("DEST_AD_1").ToString(), Space(1))), 40)
                    Else
                        drOutkaEdiL.Item("DEST_AD_1") = _Blc.LeftB(drOutkaEdiL.Item("DEST_AD_1").ToString().Substring(InStr(drOutkaEdiL.Item("DEST_AD_1").ToString(), Space(1)), InStr(drOutkaEdiL.Item("DEST_AD_1").ToString(), "　") - InStr(drOutkaEdiL.Item("DEST_AD_1").ToString(), Space(1))), 40)
                    End If
                Else
                    drOutkaEdiL.Item("DEST_AD_1") = String.Empty
                End If
            End If
        End If

        '届先住所2
        sKey = "L_DEST_AD_2_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            If ("COLUMN_MST").Equals(sTempColNo) Then
                If setDs.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                    drOutkaEdiL("DEST_AD_2") = setDs.Tables("LMH030_M_DEST").Rows(0).Item("AD_2").ToString
                End If
            Else
                drOutkaEdiL("DEST_AD_2") = drRcvEdiDtl(sTempColNo).ToString().Trim()
                If InStr(drOutkaEdiL.Item("DEST_AD_2").ToString(), "　") > 0 Then
                    drOutkaEdiL.Item("DEST_AD_2") = _Blc.LeftB(drOutkaEdiL.Item("DEST_AD_2").ToString().Substring(InStr(drOutkaEdiL("DEST_AD_2").ToString(), "　")), 40)
                Else
                    drOutkaEdiL.Item("DEST_AD_2") = String.Empty
                End If
            End If
        End If

        '届先住所3
        sKey = "L_DEST_AD_3_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then

            If ("COLUMN_MST").Equals(sTempColNo) Then
                If setDs.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                    drOutkaEdiL("DEST_AD_3") = setDs.Tables("LMH030_M_DEST").Rows(0).Item("AD_3").ToString
                End If
            Else
                drOutkaEdiL("DEST_AD_3") = drRcvEdiDtl(sTempColNo).ToString().Trim()
            End If

        End If

        '届先住所4
        drOutkaEdiL("DEST_AD_4") = String.Empty

        '届先住所5
        drOutkaEdiL("DEST_AD_5") = String.Empty

        '届先電話番号
        sKey = "L_DEST_TEL_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("DEST_TEL") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        '届先FAX
        drOutkaEdiL("DEST_FAX") = String.Empty

        '届先TEL
        drOutkaEdiL("DEST_MAIL") = String.Empty

        '届先JIS
        sKey = "L_DEST_JIS_CD_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("DEST_JIS_CD") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        '指定納品書区分
        If Not drDestMst Is Nothing Then
            drOutkaEdiL("SP_NHS_KB") = drDestMst.Item("SP_NHS_KB").ToString().Trim()
        Else
            drOutkaEdiL("SP_NHS_KB") = String.Empty
        End If

        '分析表添付区分
        If Not drDestMst Is Nothing Then
            drOutkaEdiL("COA_YN") = Right(drDestMst.Item("COA_YN").ToString().Trim(), 1)
        Else
            drOutkaEdiL("COA_YN") = String.Empty
        End If

        '荷主オーダー番号
        sKey = "L_DEST_CUST_ORD_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("CUST_ORD_NO") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        '買主オーダー番号
        'drOutkaEdiL("BUYER_ORD_NO") = String.Empty
        sKey = "L_BUYER_ORD_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("BUYER_ORD_NO") = _Blc.LeftB(drRcvEdiDtl(sTempColNo).ToString().Trim(), 30)
        End If

        '運祖元区分
        drOutkaEdiL("UNSO_MOTO_KB") = "90"

        '運送手配区分
        drOutkaEdiL("UNSO_TEHAI_KB") = String.Empty

        '車両区分
        drOutkaEdiL("SYARYO_KB") = String.Empty

        '便区分
        drOutkaEdiL("BIN_KB") = String.Empty

        '運送コード
        drOutkaEdiL("UNSO_CD") = String.Empty

        '運送名称
        drOutkaEdiL("UNSO_NM") = String.Empty

        '運送支店コード
        drOutkaEdiL("UNSO_BR_CD") = String.Empty

        '運送支店名称
        drOutkaEdiL("UNSO_BR_NM") = String.Empty

        '運タリコ
        drOutkaEdiL("UNCHIN_TARIFF_CD") = String.Empty

        '増タリコ
        drOutkaEdiL("EXTC_TARIFF_CD") = String.Empty

        '注意事項
        sKey = "L_REMARK_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            If ("COLUMN_MST").Equals(sTempColNo) Then
                If setDs.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                    drOutkaEdiL("REMARK") = setDs.Tables("LMH030_M_DEST").Rows(0).Item("REMARK").ToString
                End If
            ElseIf sTempColNo.Trim().Split(","c).Length > 0 Then
                Dim tempNo As String() = sTempColNo.Trim().Split(","c)
                For i As Integer = 0 To sTempColNo.Trim().Split(","c).Length - 1
                    If InStr(tempNo(i), "COLUMN_") = 0 Then
                        tempNo(i) = String.Concat("COLUMN_", tempNo(i))
                    End If
                    drOutkaEdiL("REMARK") = _Blc.LeftB(String.Concat(drOutkaEdiL("REMARK").ToString(), Space(1), drRcvEdiDtl(tempNo(i)).ToString().Trim()), 100)
                Next
            Else
                drOutkaEdiL("REMARK") = drRcvEdiDtl(sTempColNo).ToString().Trim()
            End If
        End If

        '配送時注意
        drOutkaEdiL("UNSO_ATT") = String.Empty

        '送り状作成有無
        drOutkaEdiL("DENP_YN") = String.Empty

        '元着区分
        drOutkaEdiL("PC_KB") = String.Empty

        '運賃請求有無
        drOutkaEdiL("UNCHIN_YN") = String.Empty

        '荷役請求有無
        drOutkaEdiL("NIYAKU_YN") = String.Empty

        '出荷データ書込み
        drOutkaEdiL("OUT_FLAG") = "0"

        '赤黒
        drOutkaEdiL("AKAKURO_KB") = "0"
        sKey = "L_DEL_KB_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            If drSemiEdiInfo.Item("EDI_TORITERM_FLG").ToString().Equals("9") = True Then
                If String.IsNullOrEmpty(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) = False Then
                    drOutkaEdiL("AKAKURO_KB") = LMConst.FLG.ON
                End If
            End If
        End If

        '実績
        drOutkaEdiL("JISSEKI_FLAG") = "0"

        '実績者
        drOutkaEdiL("JISSEKI_USER") = String.Empty

        '実績日
        drOutkaEdiL("JISSEKI_DATE") = String.Empty

        '実績時刻
        drOutkaEdiL("JISSEKI_TIME") = String.Empty

        '数値1
        sKey = "L_FREE_N01_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("FREE_N01") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        '数値2
        sKey = "L_FREE_N02_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("FREE_N02") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        '数値3
        sKey = "L_FREE_N03_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("FREE_N03") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        '
        drOutkaEdiL("FREE_N04") = 0

        '
        drOutkaEdiL("FREE_N05") = 0

        '
        drOutkaEdiL("FREE_N06") = 0

        '
        drOutkaEdiL("FREE_N07") = 0

        '
        drOutkaEdiL("FREE_N08") = 0

        '
        drOutkaEdiL("FREE_N09") = 0

        '
        drOutkaEdiL("FREE_N10") = 0

        '文字列1
        sKey = "L_FREE_C01_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("FREE_C01") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        '文字列2
        sKey = "L_FREE_C02_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("FREE_C02") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        '文字列3
        Dim endlgt As Integer = 0
        sKey = "L_FREE_C03_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            '2015.09.08 協立化学　要望対応START
            'drOutkaEdiL("FREE_C03") = drRcvEdiDtl(sTempColNo).ToString().Trim()
            endlgt = InStr(drRcvEdiDtl(sTempColNo).ToString().Trim(), "【")
            'drOutkaEdiL("FREE_C03") = Replace(Replace(drRcvEdiDtl(sTempColNo).ToString().Trim(), "】", "　"), "【", "　")
            drOutkaEdiL("FREE_C03") = Replace(drRcvEdiDtl(sTempColNo).ToString().Trim().Substring(0, endlgt), "【", "　")
            '2015.09.08 協立化学　要望対応END
        End If

        '
        drOutkaEdiL("FREE_C04") = String.Empty

        '
        drOutkaEdiL("FREE_C05") = String.Empty

        '
        drOutkaEdiL("FREE_C06") = String.Empty

        '
        drOutkaEdiL("FREE_C07") = String.Empty

        '
        drOutkaEdiL("FREE_C08") = String.Empty

        '
        drOutkaEdiL("FREE_C09") = String.Empty

        '
        drOutkaEdiL("FREE_C10") = String.Empty

        '
        drOutkaEdiL("FREE_C11") = String.Empty

        '
        drOutkaEdiL("FREE_C12") = String.Empty

        '
        drOutkaEdiL("FREE_C13") = String.Empty

        '
        drOutkaEdiL("FREE_C14") = String.Empty

        '
        drOutkaEdiL("FREE_C15") = String.Empty

        '
        drOutkaEdiL("FREE_C16") = String.Empty

        '
        drOutkaEdiL("FREE_C17") = String.Empty

        '
        drOutkaEdiL("FREE_C18") = String.Empty

        '
        drOutkaEdiL("FREE_C19") = String.Empty

        '
        drOutkaEdiL("FREE_C20") = String.Empty

        '
        drOutkaEdiL("FREE_C21") = String.Empty

        '
        drOutkaEdiL("FREE_C22") = String.Empty

        '
        drOutkaEdiL("FREE_C23") = String.Empty

        '文字列24
        sKey = "L_FREE_C24_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("FREE_C24") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        '文字列25
        sKey = "L_FREE_C25_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("FREE_C25") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        '文字列26
        sKey = "L_FREE_C26_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("FREE_C26") = drRcvEdiDtl(sTempColNo).ToString().Trim()
        End If

        'ファイル名
        drOutkaEdiL("FREE_C27") = drRcvEdiDtl("FILE_NAME_RCV").ToString().Trim()

        '
        drOutkaEdiL("FREE_C28") = String.Empty

        '
        drOutkaEdiL("FREE_C29") = String.Empty

        '
        drOutkaEdiL("FREE_C30") = String.Empty

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_L").Rows.Add(drOutkaEdiL)
        Return setDs

    End Function

#End Region

#Region "セミEDI時　データセット設定(EDI出荷(中))"
    ''' <summary>
    ''' データセット設定(EDI出荷(中)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function SetSemiOutkaEdiM(ByVal setDs As DataSet _
                                    , ByVal rowNo As Integer _
                                    , ByRef sEdiCtlNo As String _
                                    , ByRef iEdiCtlNoChu As Integer _
                                    , ByVal iFindOutkaEdiFlg As Boolean _
                                    , ByVal iCntDiffFlg As Boolean _
                                     ) As DataSet

        Dim drOutkaEdiM As DataRow = setDs.Tables("LMH030_OUTKAEDI_M").NewRow()
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(rowNo)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        Dim dtMGoods As DataTable = setDs.Tables("LMH030_M_GOODS")
        Dim drMgoods As DataRow = Nothing

        'TODO;複数存在した場合は(一番最初)
        If dtMGoods.Rows.Count > 0 Then
            drMgoods = dtMGoods.Rows(0)
        End If

        Dim sTempColNo As String = String.Empty
        Dim sKey As String = String.Empty


        '対象行のキャンセル情報取得
        Dim delKbColumnName As String = String.Empty
        Dim isCancelData As Boolean = (isNullColKey(drSemiEdiInfo, "M_DEL_KB_NO", delKbColumnName) AndAlso _
                                       (drRcvEdiDtl.Item(delKbColumnName).ToString().Trim().Length > 0))

#If True Then
        If iFindOutkaEdiFlg = True Then
            '出荷登録済みの場合

            ' 削除
            drOutkaEdiM("DEL_KB") = DEL_KB_VALUES.DELETE
            drOutkaEdiM("SYS_DEL_FLG") = LMConst.FLG.ON


            If isCancelData = True AndAlso _CancelStatusEnabled = True Then

                'ToDo: 登録済みのデータにキャンセルが来た場合の業務フローは?

                drOutkaEdiM.Item("DEL_KB") = DEL_KB_VALUES.CANCEL
                drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.ON
            ElseIf iCntDiffFlg = True Then

                ' 登録済みのデータと異なる場合は、保留で登録する
                drOutkaEdiM("DEL_KB") = DEL_KB_VALUES.RESERVATION
                drOutkaEdiM("SYS_DEL_FLG") = LMConst.FLG.OFF

            End If

        Else

            If isCancelData = True Then

#If False Then
                If (_IsEdiToriTermEquals9 = True) Then
                    drOutkaEdiM.Item("DEL_KB") = DEL_KB_VALUES.DELETE
                    drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                Else

                    ' 取り込んだファイルのセルのデータが削除フラグとして設定される。
                    drOutkaEdiM.Item("DEL_KB") = drRcvEdiDtl.Item(delKbColumnName).ToString().Trim()
                    drOutkaEdiM.Item("SYS_DEL_FLG") = drRcvEdiDtl.Item(delKbColumnName).ToString().Trim()
                End If
#Else
                drOutkaEdiM.Item("DEL_KB") = DEL_KB_VALUES.DELETE
                drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.ON
#End If
            Else
                drOutkaEdiM.Item("DEL_KB") = DEL_KB_VALUES.NORMAL
                drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
            End If

        End If

#Else

        '削除区分
        If iFindOutkaEdiFlg = True Then

            drOutkaEdiM.Item("DEL_KB") = DEL_KB_VALUES.RESERVATION
            drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

            sKey = "M_DEL_KB_NO"
            If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
                If drSemiEdiInfo.Item("EDI_TORITERM_FLG").ToString().Equals("9") = True Then
                    If String.IsNullOrEmpty(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) = False Then
                        drOutkaEdiM.Item("DEL_KB") = DEL_KB_VALUES.CANCEL
                        drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                    ElseIf iCntDiffFlg = True Then
                        drOutkaEdiM.Item("DEL_KB") = DEL_KB_VALUES.RESERVATION
                        drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                    ElseIf itargetUpdFlg = True Then
                        drOutkaEdiM.Item("DEL_KB") = DEL_KB_VALUES.DELETE
                        drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                    End If
                End If

            ElseIf iCntDiffFlg = True Then
                drOutkaEdiM.Item("DEL_KB") = DEL_KB_VALUES.RESERVATION
                drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
            ElseIf itargetUpdFlg = True Then
                drOutkaEdiM.Item("DEL_KB") = LMConst.FLG.ON
                drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.ON

            End If
        Else
            sKey = "M_DEL_KB_NO"
            If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
                If drSemiEdiInfo.Item("EDI_TORITERM_FLG").ToString().Equals("9") = True Then
                    If String.IsNullOrEmpty(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) = False Then
                        drOutkaEdiM.Item("DEL_KB") = DEL_KB_VALUES.DELETE
                        drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                    Else
                        drOutkaEdiM.Item("DEL_KB") = DEL_KB_VALUES.NORMAL
                        drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                    End If
                Else
                    If String.IsNullOrEmpty(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) = False Then
                        drOutkaEdiM.Item("DEL_KB") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
                        drOutkaEdiM.Item("SYS_DEL_FLG") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
                    Else
                        drOutkaEdiM.Item("DEL_KB") = DEL_KB_VALUES.NORMAL
                        drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                    End If
                End If

            Else
                drOutkaEdiM.Item("DEL_KB") = DEL_KB_VALUES.NORMAL
                drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

            End If
        End If
#End If
        '営業所区分
        drOutkaEdiM.Item("NRS_BR_CD") = drSemiEdiInfo.Item("NRS_BR_CD").ToString()

        'EDI管理番号L
        drOutkaEdiM.Item("EDI_CTL_NO") = sEdiCtlNo

        'EDI管理番号M
        drOutkaEdiM.Item("EDI_CTL_NO_CHU") = iEdiCtlNoChu.ToString("000")

        '出荷管理番号L
        drOutkaEdiM.Item("OUTKA_CTL_NO") = String.Empty

        '出荷管理番号M
        drOutkaEdiM.Item("OUTKA_CTL_NO_CHU") = String.Empty

        '分析表区分
        drOutkaEdiM.Item("COA_YN") = String.Empty

        '荷主注文番号
        sKey = "M_CUST_ORD_NO_DTL_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM.Item("CUST_ORD_NO_DTL") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        '買主注文番号
        drOutkaEdiM.Item("BUYER_ORD_NO_DTL") = String.Empty

        '商品コード
        sKey = "M_CUST_GOODS_CD_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM.Item("CUST_GOODS_CD") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()

            '商品コードの前nケタをカット
            Dim drCustMst As DataRow = Nothing
            Dim cutCount As Integer = 0
            If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='96'").Count > 0 Then
                drCustMst = setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='96'")(0)
                cutCount = CType(drCustMst.Item("SET_NAIYO").ToString(), Integer)
                drOutkaEdiM.Item("CUST_GOODS_CD") = drOutkaEdiM.Item("CUST_GOODS_CD").ToString.Substring(cutCount)
            Else
                drOutkaEdiM.Item("CUST_GOODS_CD") = drOutkaEdiM.Item("CUST_GOODS_CD").ToString()
            End If
        End If

        '商品キー
        If dtMGoods.Rows.Count > 0 Then
            drOutkaEdiM("NRS_GOODS_CD") = dtMGoods.Rows(0).Item("GOODS_CD_NRS").ToString
        Else
            drOutkaEdiM.Item("NRS_GOODS_CD") = String.Empty
        End If

        '商品名
        sKey = "M_GOODS_NM_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM.Item("GOODS_NM") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        '予約番号
        drOutkaEdiM.Item("RSV_NO") = String.Empty

        'ロット№
        sKey = "M_LOT_NO_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM("LOT_NO") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        'シリアル№
        sKey = "M_SERIAL_NO_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM.Item("SERIAL_NO") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        '入目
        sKey = "M_IRIME_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            If Convert.ToInt32(drRcvEdiDtl.Item(sTempColNo).ToString().Replace(".", String.Empty)) > 0 Then
                drOutkaEdiM("IRIME") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
            End If
        Else
            If drMgoods Is Nothing OrElse String.IsNullOrEmpty(drMgoods.Item("STD_IRIME_NB").ToString()) = True Then
                drOutkaEdiM("IRIME") = 0
            Else
                drOutkaEdiM("IRIME") = drMgoods.Item("STD_IRIME_NB").ToString()
            End If
        End If

        '引当単位区分
        drOutkaEdiM.Item("ALCTD_KB") = drSemiEdiInfo.Item("M_DEF_ALCTD_KB").ToString().ToString().Trim()

        '引当計上区分によって取得項目の切り替え
        If drOutkaEdiM.Item("ALCTD_KB").Equals("01") Then
            sKey = "M_OUTKA_TTL_NB_NO"
        ElseIf drOutkaEdiM.Item("ALCTD_KB").Equals("02") Then
            sKey = "M_OUTKA_TTL_QT_NO"
        Else
            sKey = "M_OUTKA_TTL_NB_NO"
        End If

        '出荷包装個数
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then

            drOutkaEdiM("OUTKA_PKG_NB") = 0
            drOutkaEdiM.Item("OUTKA_HASU") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()


        End If

        '出荷数量 << INFOテーブルにない
        'sKey = "M_OUTKA_TTL_NB_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then

            If drOutkaEdiM.Item("ALCTD_KB").Equals("01") Then
                '①×マスタ標準入目
                If drMgoods Is Nothing OrElse String.IsNullOrEmpty(drMgoods.Item("STD_IRIME_NB").ToString()) = True Then
                    drOutkaEdiM("OUTKA_QT") = 0
                Else
                    drOutkaEdiM("OUTKA_QT") = Convert.ToDecimal(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) * Convert.ToDecimal(drMgoods.Item("STD_IRIME_NB"))
                End If
            Else
                '②そのまま
                drOutkaEdiM("OUTKA_QT") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
            End If

        End If

        '出荷総個数
        'sKey = "M_OUTKA_TTL_NB_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then

            If drOutkaEdiM.Item("ALCTD_KB").Equals("01") Then
                '①そのまま
                drOutkaEdiM("OUTKA_TTL_NB") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
            Else
                '②マスタ標準入目でDIVIDE
                If drMgoods Is Nothing OrElse String.IsNullOrEmpty(drMgoods.Item("STD_IRIME_NB").ToString()) = True Then
                    drOutkaEdiM("OUTKA_TTL_NB") = 0
                Else
                    drOutkaEdiM("OUTKA_TTL_NB") = Convert.ToDecimal(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) / Convert.ToDecimal(drMgoods.Item("STD_IRIME_NB"))
                End If
            End If

        End If

        '出荷総数量
        'sKey = "M_OUTKA_TTL_NB_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then

            If drOutkaEdiM.Item("ALCTD_KB").Equals("02") Then
                '①そのまま
                drOutkaEdiM("OUTKA_TTL_QT") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
            Else
                '②×マスタ標準入目
                If drMgoods Is Nothing OrElse String.IsNullOrEmpty(drMgoods.Item("STD_IRIME_NB").ToString()) = True Then
                    drOutkaEdiM("OUTKA_TTL_QT") = 0
                Else
                    sKey = "M_OUTKA_TTL_NB_NO"
                    If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
                        drOutkaEdiM("OUTKA_TTL_QT") = Convert.ToDecimal(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) * Convert.ToDecimal(drMgoods.Item("STD_IRIME_NB"))
                    End If

                End If
            End If

        End If

        '個数単位
        drOutkaEdiM.Item("KB_UT") = String.Empty

        '数量単位
        drOutkaEdiM.Item("QT_UT") = String.Empty

        '包装個数
        drOutkaEdiM.Item("PKG_NB") = String.Empty

        '包装単位
        drOutkaEdiM.Item("PKG_UT") = String.Empty

        '温度区分
        drOutkaEdiM.Item("ONDO_KB") = String.Empty

        '運送時温度区分
        drOutkaEdiM.Item("UNSO_ONDO_KB") = String.Empty

        '入目単位
        'drOutkaEdiM.Item("IRIME_UT") = String.Empty
        sKey = "M_IRIME_UT_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM.Item("IRIME_UT") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        '個別重量
        drOutkaEdiM.Item("BETU_WT") = String.Empty

        '注意事項
        sKey = "M_REMARK_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM("REMARK") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        '出荷データ書込対象区分
        drOutkaEdiM.Item("OUT_KB") = "0"

        '赤黒区分
        drOutkaEdiM.Item("AKAKURO_KB") = "0"
        sKey = "M_DEL_KB_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            If drSemiEdiInfo.Item("EDI_TORITERM_FLG").ToString().Equals("9") = True Then
                If String.IsNullOrEmpty(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) = False Then
                    drOutkaEdiM("AKAKURO_KB") = LMConst.FLG.ON
                End If
            End If
        End If


        '実績書込フラグ
        drOutkaEdiM.Item("JISSEKI_FLAG") = "0"

        '実績書込者
        drOutkaEdiM.Item("JISSEKI_USER") = String.Empty

        '実績書込日付
        drOutkaEdiM.Item("JISSEKI_DATE") = String.Empty

        '実績書込時刻
        drOutkaEdiM.Item("JISSEKI_TIME") = String.Empty

        'セット品区
        drOutkaEdiM.Item("SET_KB") = String.Empty

        '数値1
        sKey = "M_FREE_N01_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM("FREE_N01") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        '数値2
        sKey = "M_FREE_N02_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM("FREE_N02") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        '数値3
        sKey = "M_FREE_N03_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM("FREE_N03") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        drOutkaEdiM("FREE_N04") = 0
        drOutkaEdiM("FREE_N05") = 0
        drOutkaEdiM("FREE_N06") = 0
        drOutkaEdiM("FREE_N07") = 0
        drOutkaEdiM("FREE_N08") = 0
        drOutkaEdiM("FREE_N09") = 0
        drOutkaEdiM("FREE_N10") = 0

        '文字列1
        sKey = "M_FREE_C01_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM("FREE_C01") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        '文字列2
        sKey = "M_FREE_C02_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM("FREE_C02") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        '文字列3
        sKey = "M_FREE_C03_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM("FREE_C03") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        drOutkaEdiM("FREE_C04") = String.Empty
        drOutkaEdiM("FREE_C05") = String.Empty
        drOutkaEdiM("FREE_C06") = String.Empty
        drOutkaEdiM("FREE_C07") = String.Empty
        drOutkaEdiM("FREE_C08") = String.Empty
        drOutkaEdiM("FREE_C09") = String.Empty
        drOutkaEdiM("FREE_C10") = String.Empty
        drOutkaEdiM("FREE_C11") = String.Empty
        drOutkaEdiM("FREE_C12") = String.Empty
        drOutkaEdiM("FREE_C13") = String.Empty
        drOutkaEdiM("FREE_C14") = String.Empty
        drOutkaEdiM("FREE_C15") = String.Empty
        drOutkaEdiM("FREE_C16") = String.Empty
        drOutkaEdiM("FREE_C17") = String.Empty
        drOutkaEdiM("FREE_C18") = String.Empty
        drOutkaEdiM("FREE_C19") = String.Empty
        drOutkaEdiM("FREE_C20") = String.Empty
        drOutkaEdiM("FREE_C21") = String.Empty
        drOutkaEdiM("FREE_C22") = String.Empty
        drOutkaEdiM("FREE_C23") = String.Empty

        '文字列24
        sKey = "M_FREE_C24_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM("FREE_C24") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        '文字列25
        sKey = "M_FREE_C25_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM("FREE_C25") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        '文字列26
        sKey = "M_FREE_C26_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiM("FREE_C26") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        '行番号
        drOutkaEdiM("FREE_C27") = drRcvEdiDtl("REC_NO").ToString()

        drOutkaEdiM("FREE_C28") = String.Empty
        drOutkaEdiM("FREE_C29") = String.Empty
        drOutkaEdiM("FREE_C30") = String.Empty

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_M").Rows.Add(drOutkaEdiM)

        Return setDs

    End Function

#End Region


#Region "セミEDI時 データセット設定(キャンセル対象データ抽出)"
    ''' <summary>
    ''' キャンセルデータの抽出用INデータ設定
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <param name="RowNo"></param>
    ''' <remarks></remarks>
    Protected Overloads Sub SetDatasetSelectCancelData(ByRef setDs As DataSet, ByVal RowNo As Integer)

        Dim drSemiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        Dim drItems As DataRow = setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(RowNo)

        Dim setInOutDt As DataTable = setDs.Tables("LMH030INOUT")
        Dim setInOutDr As DataRow = setInOutDt.NewRow

        Dim tempColNo As String = String.Empty

        '中身のクリア
        setInOutDt.Clear()

        '営業所
        setInOutDr.Item("NRS_BR_CD") = drSemiInfo.Item("NRS_BR_CD")
        '荷主L
        setInOutDr.Item("CUST_CD_L") = drSemiInfo.Item("CUST_CD_L")
        '荷主M
        setInOutDr.Item("CUST_CD_M") = drSemiInfo.Item("CUST_CD_M")

        ' CUST_ORD_NO
        If isNullColKey(drSemiInfo, "L_DEST_CUST_ORD_NO", tempColNo) Then
            setInOutDr("CUST_ORD_NO") = drItems.Item(tempColNo).ToString().Trim()
        End If

        'Add処理
        setInOutDt.Rows.Add(setInOutDr)

    End Sub

#End Region

#End Region

#Region "画面取込(セミEDI)チェック処理"
    ''' <summary>
    ''' 画面取込(セミEDI)チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function SemiEdiTorikomiChk(ByVal ds As DataSet) As DataSet

        Dim dtSemiInfo As DataTable = ds.Tables("LMH030_SEMIEDI_INFO")
        Dim dtSemiHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")
        Dim dtSemiDtl As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")

        Dim dtMcustD As DataTable = ds.Tables("LMH030_M_CUST_DETAILS")

        Dim dr As DataRow
        Dim hedDr As DataRow = dtSemiHed.Rows(0)
        Dim drSemiInfo As DataRow = dtSemiInfo.Rows(0)

        Dim max As Integer = dtSemiDtl.Rows.Count - 1
        Dim hedmax As Integer = dtSemiHed.Rows.Count - 1

        Dim ediCustIndex As String = drSemiInfo.Item("EDI_CUST_INDEX").ToString()

        Dim iRowCnt As Integer = 0

        Dim drSelect As DataRow() = Nothing

        '------------------------------------------------------------------------------------------
        ' ソート()
        '------------------------------------------------------------------------------------------
        Dim arrSortKey As ArrayList = New ArrayList
        Dim sTempColNo As String = String.Empty
        Dim tmpDt As DataTable = dtSemiDtl.Clone()
        Dim tmpDr() As DataRow = Nothing
        Dim sSortKeyVal As String = String.Empty
        Dim iCnt As Integer = 0

        If isNullColKey(drSemiInfo, "DEVIDE_NO_1", sTempColNo) <> False Then
            arrSortKey.Add(sTempColNo)
        End If
        If isNullColKey(drSemiInfo, "DEVIDE_NO_2", sTempColNo) <> False Then
            arrSortKey.Add(sTempColNo)
        End If
        If isNullColKey(drSemiInfo, "DEVIDE_NO_3", sTempColNo) <> False Then
            arrSortKey.Add(sTempColNo)
        End If

        For Each sKey As String In arrSortKey
            If iCnt = 0 Then
                sSortKeyVal = sSortKeyVal + String.Concat(sKey, " ASC")
            Else
                sSortKeyVal = sSortKeyVal + String.Concat(" ,", sKey, " ASC")
            End If
            iCnt += 1
        Next

        '仮受入Rowに格納
        tmpDr = dtSemiDtl.Select(String.Empty, sSortKeyVal)

        'dtSemiDtlに再セット
        For Each row As DataRow In tmpDr
            tmpDt.ImportRow(row)
        Next

        '元の取込明細へ返却
        dtSemiDtl.Clear()

        dtSemiDtl.Merge(tmpDt)

        '------------------------------------------------------------------------------------------
        ' エラーチェック
        '------------------------------------------------------------------------------------------
        For i As Integer = 0 To hedmax

            'ファイル内の明細取得
            drSelect = dtSemiDtl.Select(String.Concat("FILE_NAME_RCV ='", dtSemiHed.Rows(i).Item("FILE_NAME_RCV").ToString(), "'"))

            '明細件数カウント(件数ZEROはエラー)
            If drSelect.Count < 1 Then
                dtSemiHed.Rows(i).Item("ERR_FLG") = "1"
            End If

            'エラーフラグ判定
            If dtSemiHed.Rows(i).Item("ERR_FLG").ToString.Equals("1") Then
                '最初からエラーフラグが立っている場合（明細件数０件の場合）
                Dim sFileNm As String = dtSemiHed.Rows(i).Item("FILE_NAME_RCV").ToString()
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E460", , , LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)

            Else

                For j As Integer = iRowCnt To max

                    dr = dtSemiDtl.Rows(j)

                    If (dr.Item("FILE_NAME_RCV").ToString().Trim()).Equals(dtSemiHed.Rows(i).Item("FILE_NAME_RCV").ToString().Trim()) = True Then
                        'ヘッダと明細のファイル名称が等しい場合

                        '入力チェック(数値,日付チェック)
                        If Me.TorikomiValChk(dr, dtSemiInfo, dtMcustD) = False Then

                            '異常の場合
                            '詳細のエラーフラグに"1"をセットする
                            dr.Item("ERR_FLG") = "1"

                            'ヘッダのエラーフラグに"1"をセットする
                            dtSemiHed.Rows(i).Item("ERR_FLG") = "1"
                        Else
                            '正常の場合は処理無し（未処理（:9）の状態を保持するため）
                        End If
                    Else
                        'ヘッダと明細のファイル名称が等しくない場合
                        '現在行を保持してループを抜ける()
                        iRowCnt = j
                        Exit For
                    End If

                Next

            End If
        Next

        Return ds

    End Function

#End Region

#Region "画面取込(セミEDI)データセット＋更新処理"

    ''' <summary>
    ''' 画面取込(セミEDI)データセット＋更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function SemiEdiTorikomi(ByVal ds As DataSet) As DataSet

        ' セミEDI情報(ROW)
        Dim semiEdiInfo As DataRow = _
            ds.Tables("LMH030_SEMIEDI_INFO").Rows(0)

        ' 取込HED(dtSetHed)
        Dim torikomiHedTable As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")

        ' 取込DTL(dtSetDtl)
        Dim torikomiDtlTable As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")

        ' 処理件数(dtSetRet)
        Dim torikomiRetTable As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_RET")

        ' キー項目
        Dim sKey As String = String.Empty

        ' カラム格納番地
        Dim sTempColNo As String = String.Empty

        ' 登録フラグ(True:EDI出荷に登録する、  False:EDI出荷に登録しない)
        Dim isRegisterEdi As Boolean = True

        ' キー項目（現在行）
        Dim sNewKey As String = String.Empty

        ' 番号格納用アレイリスト
        Dim arrKey As ArrayList = New ArrayList

        ' EDI管理番号
        Dim sEdiCtlNo As String = String.Empty

        ' EDI管理番号（中）
        Dim iEdiCtlNoChu As Integer = 0

        ' 書込件数（受信DTL）
        Dim iRcvDtlInsCnt As Integer = 0

        ' 書込件数（出荷EDI(大)）
        Dim iOutHedInsCnt As Integer = 0

        ' 書込件数（出荷EDI(中)）
        Dim iOutDtlInsCnt As Integer = 0

        ' 取消件数（受信DTL[キャンセルデータ]）
        Dim iRcvDtlCanCnt As Integer = 0

        ' 取消件数（出荷EDI(大)）
        Dim iOutHedCanCnt As Integer = 0

        ' 取消件数（出荷EDI(中)）
        Dim iOutDtlCanCnt As Integer = 0

        ' 更新件数（出荷EDI(大)）
        Dim iOutHedUpdCnt As Integer = 0

        ' エラー無しフラグ（True：エラー無し、False：エラー有り）
        Dim bNoErr As Boolean = True

        '---------------------------------------------------------------------------
        ' プライベート変数[倉庫コード/荷主L/荷主M]の設定
        '---------------------------------------------------------------------------
        _WhCd = semiEdiInfo.Item("WH_CD").ToString
        _CustCdL = semiEdiInfo.Item("CUST_CD_L").ToString
        _CustCdM = semiEdiInfo.Item("CUST_CD_M").ToString

        ' 受信テーブル書込指定
        Dim rcvInsFlg As String = semiEdiInfo.Item("RCV_TBL_INS_FLG").ToString()

        ' キャンセル状態の利用有無
        _CancelStatusEnabled _
             = semiEdiInfo.Item("EDI_TORITERM_FLG").ToString().Equals("9")

        ' 販売伝票番号のカラム名
        Dim destCustOrdNoLColumnName As String = String.Empty
        If (isNullColKey(semiEdiInfo, "L_DEST_CUST_ORD_NO", destCustOrdNoLColumnName) <> True) Then
            bNoErr = False
        End If

        ' 販売伝票明細番号のカラム名
        Dim destCustOrdNoMColumnName As String = String.Empty
        If (isNullColKey(semiEdiInfo, "M_CUST_ORD_NO_DTL_NO", destCustOrdNoMColumnName) <> True) Then
            bNoErr = False
        End If

        ' 納入期日
        Dim arrPlanDateColumnName As String = String.Empty
        If (isNullColKey(semiEdiInfo, "L_ARR_PLAN_DATE_NO", arrPlanDateColumnName) <> True) Then
            bNoErr = False
        End If

        ' 届先CDのカラム名
        Dim destCdNoLColumnName As String = String.Empty
        If (isNullColKey(semiEdiInfo, "L_DEST_CD_NO", destCdNoLColumnName) <> True) Then
            bNoErr = False
        End If

        '  ロット番号のカラム名
        Dim lotNoColumnName As String = String.Empty
        If (isNullColKey(semiEdiInfo, "M_LOT_NO_NO", lotNoColumnName) <> True) Then
            bNoErr = False
        End If


        ' 拒否理由のカラム名
        Dim delKbColumnName As String = String.Empty
        If (isNullColKey(semiEdiInfo, "L_DEL_KB_NO", delKbColumnName) <> True) Then
            bNoErr = False
        End If



        If (bNoErr = False) Then

            'エラー有り
            torikomiHedTable.Rows(0).Item("ERR_FLG") = LMConst.FLG.ON

            ' [%1]に失敗しました。システム管理者に連絡してください。
            Dim replaceMessages As String() = {"EDI取込のための設定情報の読み込み"}
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "S001", replaceMessages)

            Return ds

        End If


        ' EDI取消処理許可
        Dim isEdiTorikesiFlgOn As Boolean = _
            (semiEdiInfo.Item("EDI_TORIKESI_FLG").ToString().Equals(LMConst.FLG.ON))


        ' EDIオーダー番号とEDI管理番号Mのペア(取込状態確認用)
        Dim ediOrderAndCtrlNumbers As Dictionary(Of String, String) = _
            New Dictionary(Of String, String)()

        ' EDI管理番号LとEDI管理番号Mのペア(Mのインクリメント用)
        Dim ediCtrlNoMCounters As Dictionary(Of String, Integer) = _
            New Dictionary(Of String, Integer)()

        ' 以前のデータとの差異のあるEDIオーダー番号を管理する
        Dim changedDataEdiOrderNumbers As List(Of String) = New List(Of String)()

        ' EDI取込のデータをDBへInsert,Updateするデータを格納するデータセット
        Dim ediEntryDs As DataSet = Nothing


        ' 取込行毎の処理
        For fileRowIdx As Integer = 0 To torikomiDtlTable.Rows.Count - 1

            '
            iOutHedUpdCnt = 0

            '---------------------------------------------------------------------------
            ' キー項目設定
            '---------------------------------------------------------------------------
            Dim drSetDtl As DataRow = torikomiDtlTable.Rows(fileRowIdx)

            'リスト初期化
            arrKey.Clear()

            'キーの初期化
            sNewKey = String.Empty

            '2015.06.29 検証一覧№8 追加START
            _DevideLen1 = 0
            _DevideLen2 = 0
            '2015.06.29 検証一覧№8 追加END

            'キー設定['分割番号1 + 分割番号2 + 分割番号3]
            sKey = "DEVIDE_NO_1"
            If isNullColKey(semiEdiInfo, sKey, sTempColNo) <> False Then
                '2015.06.29 検証一覧№8 追加START
                _DevideLen1 = _FileEncoding.GetByteCount(drSetDtl.Item(sTempColNo).ToString().Trim())
                '2015.06.29 検証一覧№8 追加END
                arrKey.Add(drSetDtl.Item(sTempColNo).ToString().Trim())
            End If
            sKey = "DEVIDE_NO_2"
            If isNullColKey(semiEdiInfo, sKey, sTempColNo) <> False Then
                '2015.06.29 検証一覧№8 追加START
                _DevideLen2 = _FileEncoding.GetByteCount(drSetDtl.Item(sTempColNo).ToString().Trim())
                '2015.06.29 検証一覧№8 追加END
                arrKey.Add(Left(drSetDtl.Item(sTempColNo).ToString().Trim(), _DevideLen2 - 1))
            End If
            sKey = "DEVIDE_NO_3"
            If isNullColKey(semiEdiInfo, sKey, sTempColNo) <> False Then
                arrKey.Add(drSetDtl.Item(sTempColNo).ToString().Trim())
            End If

            For Each keys As Object In arrKey
                sNewKey += Convert.ToString(keys)
            Next

            '---------------------------------------------------------------------------
            ' EDI取消処理(SYSDEL_FLG ='1',DEl_KB ='1')
            ' 出荷取込保留処理(SYSDEL_FLG ='1',DEl_KB ='3')
            '---------------------------------------------------------------------------
            Dim setDelDs As DataSet = ds.Copy()

            ' EDIオーダー番号
            Dim ediOrderNo As String = sNewKey

            ' 出荷登録済の状態を格納する
            Dim isCompletedShippingReg As Boolean = False

            ' 削除候補のデータを格納する
            Dim candidateCancelDataRows As IEnumerable(Of DataRow) = {}

            ' キャンセル指定文字列
            Dim cancelColumnValue As String = _
                drSetDtl.Item(delKbColumnName).ToString().Trim()

            ' 拒否理由の設定値有無を格納する(ToDo:"削除"の文字列固定にするべきか確認)
            Dim isDeleteRow As Boolean = (cancelColumnValue.Trim().Length > 0)

            '　EDI登録有無フラグ
            isRegisterEdi = True

            '　INデータセット設定
            SetDatasetSelectCancelData(setDelDs, fileRowIdx)

            ' 同一販売伝票番号をもつキャンセル候補のデータを取得
            Dim selectCandidateCancelData As DataSet = _
                MyBase.CallDAC(Me._Dac, "SelectCancelData", setDelDs)

            ' キャンセル候補となる同一販売伝票番号のレコード数を設定
            Dim selectSameOrderNoDataCount As Integer = MyBase.GetResultCount

            ' 有効な同一販売伝票番号のデータ有無を確認
            If selectSameOrderNoDataCount > 0 Then

                ' 同一販売伝票のデータ
                candidateCancelDataRows = setDelDs.Tables("LMH030OUT").Select()

                '出荷登録済確認(キャンセル候補内に一件でも出荷管理番号が設定されている行が存在する。)
                isCompletedShippingReg = _
                    (candidateCancelDataRows.Where(Function(row) row.Item("OUTKA_CTL_NO").ToString().Length > 0).Count > 0)

                If (isCompletedShippingReg = True) Then

                    ' 出荷登録済みデータを除去
                    candidateCancelDataRows = candidateCancelDataRows.
                        Where(Function(row) row.Item("OUTKA_CTL_NO").ToString().Trim().Length = 0)

                End If

                ' 同一ファイル内登録データの妥当性チェック
                Dim delEdiCtrlNo As String = ""
                If (ediOrderAndCtrlNumbers.ContainsKey(ediOrderNo)) Then

                    ' 販売伝票番号からEDI管理番号取得
                    delEdiCtrlNo = ediOrderAndCtrlNumbers(ediOrderNo)

                    ' 同一ファイル内の処理で設定されたEDI管理番号のデータを抽出
                    Dim qry As IEnumerable(Of DataRow) _
                        = candidateCancelDataRows _
                          .Where(Function(row) row("EDI_CTL_NO").Equals(delEdiCtrlNo))

                    If (isDeleteRow = False) Then '削除行はチェック不要
                        If (qry.Count > 0) Then
                            '登録中の行データは、子データと判断

                            For Each friendData As DataRow In qry

                                ' 出荷日
                                Dim compareArrPlanDate As DateTime = Nothing
                                If (DateTime.TryParse(drSetDtl.Item(arrPlanDateColumnName).ToString(), compareArrPlanDate) = False OrElse _
                                    compareArrPlanDate.ToString("yyyyMMdd").Equals(friendData("ARR_PLAN_DATE").ToString()) = False) Then

                                    ' エラー設定
                                    bNoErr = False

                                    ' 出荷日が異なるデータが存在する。E260 [%1]が存在するため、処理できません。
                                    Dim replaceMessages As String() = {"同一の販売伝票内に異なる指定納入期日のデータ"}
                                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E260", replaceMessages, (fileRowIdx + 1).ToString(), LMH030BLC.EXCEL_COLTITLE, friendData("ARR_PLAN_DATE").ToString())
                                End If


                                ' 同一販売伝票内で届先が一致しているか確認
                                If (drSetDtl.Item(destCdNoLColumnName).Equals(friendData("DEST_CD").ToString()) = False) Then
                                    ' エラー設定
                                    bNoErr = False

                                    ' 届先が異なるデータが存在する。E260 [%1]が存在するため、処理できません。
                                    Dim replaceMessages As String() = {"同一の販売伝票内に異なる出荷先のデータ"}
                                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E260", replaceMessages, (fileRowIdx + 1).ToString(), LMH030BLC.EXCEL_COLTITLE, friendData("DEST_CD").ToString())
                                End If

                                ' 出荷日,届先のどちらかでエラーになった時点で以降は確認しない
                                If (bNoErr = False) Then Exit For
                            Next
                        End If
                    End If


                    If (bNoErr = False) Then
                        Continue For '次の行へ
                    End If
                End If ' ediOrderAndCtrlNumbers.ContainsKey



                ' ---------------------------  旧データ削除 START  --------------------------- 

                ' 本取込処理ないで実行されたEDI管理番号を削除対象が除外する
                Dim exceptDeleteEdiCtrlNo As String = ""
                If (ediOrderAndCtrlNumbers.ContainsKey(ediOrderNo)) Then
                    exceptDeleteEdiCtrlNo = ediOrderAndCtrlNumbers(ediOrderNo)
                End If

                'ここより前でcandidateCancelDataRowsから出荷登録されたデータは除去されている
                Dim deleteEdiCtrlNumbers As IEnumerable(Of String) _
                    = candidateCancelDataRows.Where(Function(row) row.Item("EDI_CTL_NO").ToString() <> exceptDeleteEdiCtrlNo) _
                                             .Select(Function(row) row.Item("EDI_CTL_NO").ToString()).Distinct()

                'Dim delDsByEdiCtrlNo As DataSet = setDelDs.Copy()
                Dim delDsByEdiCtrlNo As DataSet = setDelDs.Clone()
                delDsByEdiCtrlNo.Tables("LMH030_SEMIEDI_INFO").ImportRow(semiEdiInfo)

                ' 出荷登録されていない同一販売伝票番号の本処理と異なるEDI管理番号をもつデータを削除
                For Each deleteEdiCtrlNumber As String In deleteEdiCtrlNumbers

                    'INデータ再設定
                    delDsByEdiCtrlNo.Tables("LMH030INOUT").Clear()

                    Dim drSetDel As DataRow = delDsByEdiCtrlNo.Tables("LMH030INOUT").NewRow()
                    drSetDel.Item("NRS_BR_CD") = semiEdiInfo.Item("NRS_BR_CD")
                    drSetDel.Item("EDI_CTL_NO") = deleteEdiCtrlNumber

                    delDsByEdiCtrlNo.Tables("LMH030INOUT").Rows.Add(drSetDel)

                    'EDI取消(L)
                    MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiL", delDsByEdiCtrlNo)

                    'EDI出荷(大)の削除行カウント
                    If MyBase.GetResultCount > 0 Then
                        iOutHedCanCnt += MyBase.GetResultCount
                    End If

                    'EDI取消(M)
                    MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiM", delDsByEdiCtrlNo)

                    'EDI出荷(中)の削除行カウント
                    If MyBase.GetResultCount > 0 Then
                        iOutDtlCanCnt += MyBase.GetResultCount
                    End If

                    If (rcvInsFlg.Equals(RCV_TBL_INS_FLG_VALUES.WRITE_AND_DATA_CHECK) = True OrElse _
                        rcvInsFlg.Equals(RCV_TBL_INS_FLG_VALUES.WRITE_ONLY) = True) Then

                        If String.IsNullOrEmpty(semiEdiInfo.Item("RCV_NM_DTL").ToString()) = False Then

                            '受信テーブル(DTL)の削除処理
                            MyBase.CallDAC(Me._Dac, "UpdateDelRcvDtl", delDsByEdiCtrlNo)
                        End If

                    End If

                    delDsByEdiCtrlNo.Clear()
                Next

                ' ---------------------------  旧データ削除 END --------------------------- 


                ' --------------------------- 出荷登録済みのデータ変更確認 START --------------------------- 
                If (isCompletedShippingReg = True) Then
                    'チェック ①<レコード有 / OUTKA_CTL_NO有> = 出荷が既にできているため(後続データ保留扱いでINSERT)

                    If (rcvInsFlg.Equals(RCV_TBL_INS_FLG_VALUES.WRITE_AND_DATA_CHECK) = True OrElse _
                        rcvInsFlg.Equals(RCV_TBL_INS_FLG_VALUES.WRITE_ONLY) = True) Then

                        If String.IsNullOrEmpty(semiEdiInfo.Item("RCV_NM_DTL").ToString()) = False Then

                            setDelDs.Tables("LMH030_EDI_TORIKOMI_DTL").Clear()
                            setDelDs.Tables("LMH030_EDI_TORIKOMI_DTL").ImportRow(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(fileRowIdx))

                            '受信テーブル同一データチェック処理
                            setDelDs = MyBase.CallDAC(Me._Dac, "SelectCompareData", setDelDs)

                            If MyBase.GetResultCount > 0 Then
                                ' 同一データがある場合は、削除

                                If rcvInsFlg.Equals(RCV_TBL_INS_FLG_VALUES.WRITE_AND_DATA_CHECK) = True Then

                                    isRegisterEdi = False

                                ElseIf rcvInsFlg.Equals(RCV_TBL_INS_FLG_VALUES.WRITE_ONLY) = True Then
                                    ' 協立化学はこちら

                                End If
                            Else
                                ' 同一データがない場合は、保留

                                'データの変更あり
                                If (changedDataEdiOrderNumbers.Contains(ediOrderNo) = False) Then
                                    changedDataEdiOrderNumbers.Add(ediOrderNo)
                                End If


                            End If
                        Else
                            If (changedDataEdiOrderNumbers.Contains(ediOrderNo) = False) Then
                                changedDataEdiOrderNumbers.Add(ediOrderNo)
                            End If

                        End If
                    End If
                End If
                ' ---------------------------  出荷登録済みのデータ変更確認 END ---------------------------  


            End If ' selectSameOrderNoDataCount > 0

            '---------------------------------------------------------------------------
            ' EDI管理番号(大,中)の採番
            '---------------------------------------------------------------------------
            If (ediOrderAndCtrlNumbers.ContainsKey(ediOrderNo) = True) Then
                'EDIオーダー番号からEDI管理番号を取得
                sEdiCtlNo = ediOrderAndCtrlNumbers(ediOrderNo)

                'EDI管理番号からEDI管理詳細番号を取得
                iEdiCtlNoChu = ediCtrlNoMCounters(sEdiCtlNo)
            Else
                iEdiCtlNoChu = 0
            End If

            '---------------------------------------------------------------------------
            ' EDI管理番号(大,中)の設定
            '---------------------------------------------------------------------------
            ds = Me.GetEdiCtlNo(ds, ediOrderAndCtrlNumbers.ContainsKey(ediOrderNo), sEdiCtlNo, iEdiCtlNoChu)

            '---------------------------------------------------------------------------
            ' 出荷EDI LM のINデータ設定
            '---------------------------------------------------------------------------
            '別インスタンス(明細用)
            ediEntryDs = ds.Copy()


            ' 届先M自動追加・商品Mｴﾗｰ判断フラグ
            Dim sFlg17 As String = semiEdiInfo.Item("FLAG_17").ToString()

            If semiEdiInfo.Item("DTL_DATACHECK_FLG").ToString().Equals(LMConst.FLG.ON) = True OrElse _
               ediOrderAndCtrlNumbers.ContainsKey(ediOrderNo) = False Then

                '届先マスタ読込用INデータ作成
                Me.SetInDataSelectDestMst(ediEntryDs, fileRowIdx)

                '届先マスタ読込
                ediEntryDs = MyBase.CallDAC(Me._Dac, "SelectMstDest", ediEntryDs)

                '抽出件数判定
                If MyBase.GetResultCount = 0 Then
                    If sFlg17.Equals("0") Then
                        'エラー返却
                        Dim sDenNo As String = String.Empty
                        Dim sDestCd As String = String.Empty

                        '届先CD取得
                        sDestCd = drSetDtl.Item(destCdNoLColumnName).ToString().Trim()

                        '伝票管理番号取得
                        sDenNo = drSetDtl.Item(destCustOrdNoLColumnName).ToString().Trim()

                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {String.Concat("届先コード:", sDestCd), "届先マスタ", String.Concat(" 伝票管理番号:", sDenNo)})
                        bNoErr = False
                        Continue For
                    Else
                        ' 協立化学の設定はこちら
                        ' 自動登録(INSERT)
                        ' 出荷登録時に行うので処理不要
                    End If

                End If

            End If 'bSameKeyFlg = False


            If (isRegisterEdi = True) Then
                '取込DTL⇒EDI出荷(大)へのデータ設定
                ediEntryDs = Me.SetSemiOutkaEdiL(ediEntryDs, fileRowIdx, sEdiCtlNo, iEdiCtlNoChu, isCompletedShippingReg, changedDataEdiOrderNumbers.Contains(ediOrderNo))

            End If


            ' 商品の存在確認
            If (Me.IsGoodsExists(ediEntryDs, fileRowIdx, semiEdiInfo, drSetDtl, sFlg17) = False) Then
                bNoErr = False
                Continue For
            End If

            '取込DTL⇒EDI出荷(中)へのデータ設定
            ediEntryDs = Me.SetSemiOutkaEdiM(ediEntryDs, fileRowIdx, sEdiCtlNo, iEdiCtlNoChu, isCompletedShippingReg, changedDataEdiOrderNumbers.Contains(ediOrderNo))

            '受信テーブル書込みフラグが"1"または"2"の場合は受信テーブル新規追加処理を行う
            If (rcvInsFlg.Equals(RCV_TBL_INS_FLG_VALUES.WRITE_AND_DATA_CHECK) = True OrElse _
                rcvInsFlg.Equals(RCV_TBL_INS_FLG_VALUES.WRITE_ONLY) = True) Then

                If String.IsNullOrEmpty(semiEdiInfo.Item("RCV_NM_DTL").ToString()) = False Then
                    '取込DTL⇒受信テーブル(DTL)への書き込み処理
                    Dim setDsRcv As DataSet = ds.Copy
                    setDsRcv.Clear()
                    setDsRcv.Tables("LMH030_SEMIEDI_INFO").ImportRow(ediEntryDs.Tables("LMH030_SEMIEDI_INFO").Rows(0))
                    setDsRcv.Tables("LMH030_EDI_TORIKOMI_DTL").ImportRow(ediEntryDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(fileRowIdx))
                    setDsRcv.Tables("LMH030_OUTKAEDI_M").ImportRow(ediEntryDs.Tables("LMH030_OUTKAEDI_M").Rows(0))

                    If (isCompletedShippingReg = True) Then
                        ' 一旦、削除で取込を登録。差異がある場合は後で復活
                        setDsRcv.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                    Else
                        setDsRcv.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                    End If

                    setDsRcv = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiRcvDtl", setDsRcv)

                End If

            End If


            If (isDeleteRow = False) Then ' 拒否理由に削除が設定されたデータは登録しない

                '---------------------------------------------------------------------------
                ' EDI出荷データの追加処理を行う
                '---------------------------------------------------------------------------
                If (isRegisterEdi = True AndAlso _
                    ediOrderAndCtrlNumbers.ContainsKey(ediOrderNo) = False) Then

                    'EDI出荷(大)の新規追加
                    ediEntryDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiL", ediEntryDs)
                    iOutHedInsCnt = iOutHedInsCnt + 1

                    '同一ファイル内で作成されたEDI管理番号を格納
                    ediOrderAndCtrlNumbers.Add(ediOrderNo, sEdiCtlNo)

                    'EDI管理番号の子番号を格納
                    ediCtrlNoMCounters.Add(sEdiCtlNo, 0)

                End If

                '　EDI出荷(中)の新規追加
                If isRegisterEdi = True Then
                    ediEntryDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiM", ediEntryDs)
                    ediCtrlNoMCounters(sEdiCtlNo) += 1
                    iOutDtlInsCnt = iOutDtlInsCnt + 1
                End If
            End If



            ' ------------------------ 関連行更新 START ----------------------------

            ' 同一データのEDI出荷(大)更新処理
            If isCompletedShippingReg = True AndAlso _
               changedDataEdiOrderNumbers.Contains(ediOrderNo) = True Then

                If (ediEntryDs Is Nothing) Then
                    ' 拒否理由による削除指定行用にデータを設定

                    ediEntryDs = ds.Copy()

                    If (ediEntryDs.Tables("LMH030_OUTKAEDI_L").Rows.Count = 0) Then
                        ediEntryDs.Tables("LMH030_OUTKAEDI_L").Rows.Add(ediEntryDs.Tables("LMH030_OUTKAEDI_L").NewRow)
                    End If

                    ediEntryDs.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD") = semiEdiInfo("NRS_BR_CD")
                    ediEntryDs.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo

                End If


                ' EDI_CTL_NOをキーとして、DEL_KBNを保留(3)にする
                ediEntryDs = MyBase.CallDAC(Me._Dac, "UpdateEditOutkaEdiL", ediEntryDs)
                iOutHedUpdCnt = MyBase.GetResultCount

                If iOutHedUpdCnt > 0 Then
                    ' 同一データで変更データがある場合はEDI出荷(中)復活(保留)処理
                    ediEntryDs = MyBase.CallDAC(Me._Dac, "UpdateEditOutkaEdiM", ediEntryDs)
                End If

                ' EDI取込データを削除から正常へ変更
                ediEntryDs = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiDtl", ediEntryDs)

            End If

            ' -------------------------- 関連行更新 END ------------------------------
        Next

        If bNoErr Then
            'エラー無し
            torikomiHedTable.Rows(0).Item("ERR_FLG") = LMConst.FLG.OFF
        Else
            'エラー有り
            torikomiHedTable.Rows(0).Item("ERR_FLG") = LMConst.FLG.ON
        End If


        '処理件数
        torikomiRetTable.Rows(0).Item("RCV_DTL_INS_CNT") = iRcvDtlInsCnt.ToString()
        torikomiRetTable.Rows(0).Item("OUT_HED_INS_CNT") = iOutHedInsCnt.ToString()
        torikomiRetTable.Rows(0).Item("OUT_DTL_INS_CNT") = iOutDtlInsCnt.ToString()
        torikomiRetTable.Rows(0).Item("RCV_DTL_CAN_CNT") = iRcvDtlCanCnt.ToString()
        torikomiRetTable.Rows(0).Item("OUT_HED_CAN_CNT") = iOutHedCanCnt.ToString()
        torikomiRetTable.Rows(0).Item("OUT_DTL_CAN_CNT") = iOutDtlCanCnt.ToString()

        Return ds

    End Function



    ''' <summary>
    ''' 品目の存在確認
    ''' </summary>
    ''' <param name="ediEntryDs">EDI登録データセット</param>
    ''' <param name="fileRowIdx">処理行のIndex</param>
    ''' <param name="semiEdiInfo">セミＥＤＩ情報設定</param>
    ''' <param name="drSetDtl"></param>
    ''' <param name="sFlg17">届先M自動追加・商品Mｴﾗｰ判断フラグ</param>
    ''' <returns>確認結果(true:存在する, false:存在しない)</returns>
    ''' <remarks></remarks>
    Function IsGoodsExists(ByRef ediEntryDs As DataSet _
                         , ByVal fileRowIdx As Integer _
                         , ByRef semiEdiInfo As DataRow _
                         , ByRef drSetDtl As DataRow _
                         , ByVal sFlg17 As String) As Boolean

        '商品マスタ読込用INデータ作成
        Me.SetInDataSelectGoodsMst(ediEntryDs, fileRowIdx)

        '商品マスタ読込処理(商品コードがマスタに存在しない場合はエラー)
        ediEntryDs = MyBase.CallDAC(Me._Dac, "SelectMstGoods", ediEntryDs)
        If MyBase.GetResultCount = 0 AndAlso (sFlg17.Equals("0") OrElse sFlg17.Equals("1")) Then
            '商品マスタに荷主商品コードが存在しない場合はエラー
            Dim sDenNo As String = String.Empty
            Dim sDenMsg As String = String.Empty
            Dim sGoodsCd As String = String.Empty
            Dim sColTemp As String = String.Empty

            sColTemp = semiEdiInfo.Item("M_CUST_GOODS_CD_NO").ToString()
            sColTemp = String.Concat("COLUMN_", sColTemp)
            sGoodsCd = drSetDtl.Item(sColTemp).ToString().Trim()

            sColTemp = semiEdiInfo.Item("L_DEST_CUST_ORD_NO").ToString()
            If String.IsNullOrEmpty(sColTemp) = True Then
                sDenNo = Convert.ToString(fileRowIdx)
                sDenMsg = "行番号:"
            Else
                sColTemp = String.Concat("COLUMN_", sColTemp)
                sDenMsg = "伝票管理番号:"
                sDenNo = drSetDtl.Item(sColTemp).ToString().Trim()
            End If

            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {String.Concat("商品コード:", sGoodsCd), "商品マスタ", String.Concat(sDenMsg, sDenNo)})

            Return False
        End If

        Return True

    End Function





#End Region
#End Region

End Class
