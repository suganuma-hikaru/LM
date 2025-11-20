' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID　　　　:  502　　　 : 住化カラー（岩槻⇒群馬・千葉・千葉(南袖)）
'  作  成  者       :  honmyo
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC502
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC502 = New LMH030DAC502()

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _DacInka As LMH010DAC502 = New LMH010DAC502()

    ''' <summary>
    ''' 使用するDACクラスの生成(共通DAC)
    ''' </summary>
    ''' <remarks></remarks>
    Private _DacCom As LMH030DAC = New LMH030DAC()

    ''' <summary>
    ''' 使用するBLC共通クラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH030BLC = New LMH030BLC()

#End Region

#Region "住化カラー用CONST(セミEDI)"

    ''' <summary>
    ''' 住化カラー(群馬)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NRS_CD_SMK_GNM As String = "30"           '営業所コード 
    Public Const WH_CD_SMK_GNM As String = "301"            '倉庫コード
    Public Const CUST_CD_L_GNM As String = "00952"          '荷主コード（大）
    Public Const CUST_CD_M_GNM As String = "00"             '荷主コード（中）

    ''' <summary>
    ''' 住化カラー(千葉)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NRS_CD_SMK_CBA As String = "10"            '営業所コード 
    Public Const WH_CD_SMK_CBA As String = "101"            '倉庫コード
    Public Const CUST_CD_L_CBA As String = "00002"          '荷主コード（大）
    Public Const CUST_CD_M_CBA As String = "00"             '荷主コード（中）

    ''' <summary>
    ''' 住化カラー(千葉:南袖)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NRS_CD_SMK_MSD As String = "10"            '営業所コード 
    Public Const WH_CD_SMK_MSD As String = "101"            '倉庫コード
    Public Const CUST_CD_L_MSD As String = "00404"          '荷主コード（大）
    Public Const CUST_CD_M_MSD As String = "00"             '荷主コード（中）

    ''' <summary>
    ''' 受払い場所(ファイル項目(6項目目))
    ''' </summary>
    ''' <remarks></remarks>
    Public Const UKEHARAI_GNM As String = "503"            '受払場所(群馬)
    Public Const UKEHARAI_CBA As String = "502"            '受払場所(千葉)

    ''' <summary>
    ''' その他
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DEF_CTL_NO As String = "C00000000"             '管理番号初期値
    Public Const DEF_UNSO_NO_L As String = "01-C00000000"       '運送番号初期値
    Public Const DEF_UNSO_NO_M As String = "01-C00000000000"    '運送番号初期値

#End Region

#Region "Method"

#Region "出荷登録処理"
    ''' <summary>
    ''' 出荷登録処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OutkaToroku(ByVal ds As DataSet) As DataSet

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

        Dim autoMatomeF As String = ds.Tables("LMH030INOUT").Rows(0)("AUTO_MATOME_FLG").ToString()
        Dim matomeNo As String = String.Empty
        Dim matomeFlg As Boolean = False
        Dim UnsoMatomeFlg As Boolean = False


        '住化カラーはまとめ対象荷主(共通のまとめSQLを使用)
        '自動まとめフラグ = "0" or "1"の場合、まとめ処理
        If autoMatomeF.Equals("0") OrElse autoMatomeF.Equals("1") Then

            'まとめ先取得
            ds = MyBase.CallDAC(Me._DacCom, "SelectMatomeTarget", ds)

            If MyBase.GetResultCount = 0 Then
                'まとめ先が無い場合、通常登録
                matomeFlg = False

            ElseIf MyBase.GetResultCount > 1 Then
                choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.SMK_WID_L013, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    'まとめ対象だったデータを出したい場合はコメントをはずす
                    'Dim matomeTargetNo As String = Me.matomesakiOutkaNo(ds)
                    msgArray(1) = "出荷管理番号(大)"
                    msgArray(2) = "出荷"
                    msgArray(3) = "注意)進捗区分が同一の場合は、管理番号が若い方にまとまります。"
                    msgArray(4) = String.Empty
                    matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                    ds = Me._Blc.SetComWarningL("W199", LMH030BLC.SMK_WID_L013, ds, msgArray, matomeNo, String.Empty)
                    Return ds

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    '自動まとめ処理を行う
                    matomeFlg = True
                End If
                ''まとめ先が複数件の場合、エラー
                'matomeNo = Me.matomesakiOutkaNo(ds)
                'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E427", New String() {matomeNo}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'Return ds
            ElseIf autoMatomeF.Equals("0") = True Then

                choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.SMK_WID_L005, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    msgArray(1) = "出荷管理番号(大)"
                    msgArray(2) = String.Empty
                    msgArray(3) = String.Empty
                    msgArray(4) = String.Empty
                    matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                    ds = Me._Blc.SetComWarningL("W168", LMH030BLC.SMK_WID_L005, ds, msgArray, matomeNo, String.Empty)
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

                    choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.SMK_WID_L014, 0)

                    '進捗区分が予定入力より先になっているのでワーニングを出力
                    If String.IsNullOrEmpty(choiceKb) = True Then
                        msgArray(1) = "出荷管理番号(大)"
                        msgArray(2) = "出荷"
                        msgArray(3) = String.Empty
                        msgArray(4) = String.Empty
                        matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                        ds = Me._Blc.SetComWarningL("W198", LMH030BLC.SMK_WID_L014, ds, msgArray, matomeNo, String.Empty)
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

        '出荷管理番号(大)の採番
        ds = Me.GetOutkaNoL(ds, matomeFlg)

        ''出荷管理番号(中)の採番
        ds = Me.GetOutkaNoM(ds, matomeFlg)

        '紐付け処理の場合は、別Funcでデータセット設定+更新処理
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()
        If eventShubetsu.Equals("3") Then
            '紐付処理をして終了
            ds = Me.Himoduke(ds)
            Return ds

        End If

        '出荷(大)データセット設定処理
        ds = Me.SetDatasetOutkaL(ds, matomeFlg)

        '出荷(中)データセット設定
        '要望番号:1767(千葉 住化カラー納品書明細行のオーダー番号間違い) 2013/01/11 Start
        'ds = Me.SetDatasetOutkaM(ds)
        ds = Me.SetDatasetOutkaM(ds, matomeFlg)
        '要望番号:1767(千葉 住化カラー納品書明細行のオーダー番号間違い) 2013/01/11 End

        'EDI受信テーブル(HED)データセット設定
        ds = Me.SetDatasetEdiRcvHed(ds)

        'EDI受信テーブル(DTL)データセット設定
        ds = Me.SetDatasetEdiRcvDtl(ds)

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

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        If matomeFlg = False Then
            '出荷(大)の新規登録
            ds = MyBase.CallDAC(Me._Dac, "InsertOutkaLData", ds)
        Else
            '出荷(大)のまとめ更新
            ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
        End If

        '出荷(中)の新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertOutkaMData", ds)

        '届先マスタの自動追加
        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
               AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "1"
            ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "0"
        End If

        '届先マスタの更新
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

        '作業の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_E_SAGYO").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertSagyoData", ds)
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

#Region "実績作成処理"
    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSakusei(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        '受信HEDの更新
        '更新すべき項目は存在しないが、Upd日付時刻等をDTLと一致させるため
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        '出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)

        Return ds

    End Function

#End Region

    '要望番号:1848（千葉 住化カラー まとめデータの実績作成でアベンド）対応　 2013/02/13 本明Start
#Region "実績作成時同一まとめレコード取得処理"
    ''' <summary>
    ''' 実績作成時同一まとめレコード取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectMatome(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMatome", ds)

        Return ds

    End Function

#End Region
    '要望番号:1848（千葉 住化カラー まとめデータの実績作成でアベンド）対応　 2013/02/13 本明End



#Region "EDI_Lの初期値設定(出荷登録処理)"
    ''' <summary>
    ''' EDI_Lの初期設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>各マスタ値を取得しEDI_Lの初期設定をする</remarks>
    Private Function SetEdiLShoki(ByVal ds As DataSet) As DataSet

        '荷主M取得
        ds = MyBase.CallDAC(Me._DacCom, "SelectMcustOutkaToroku", ds)

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim mCustDr As DataRow = ds.Tables("LMH030_M_CUST").Rows(0)
        Dim mDestDr As DataRow = Nothing
        Dim mDestFlgYN As Boolean = False      '届先マスタ有無フラグ

        '届先M取得
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = False OrElse _
            String.IsNullOrEmpty(ediDr("EDI_DEST_CD").ToString().Trim()) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdest", ds)
        End If

        If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
            mDestDr = ds.Tables("LMH030_M_DEST").Rows(0)
            mDestFlgYN = True
        End If

        '出荷区分
        If String.IsNullOrEmpty(ediDr("OUTKA_KB").ToString().Trim()) = True Then
            ediDr("OUTKA_KB") = "10"
        End If

        '出荷種別区分
        If String.IsNullOrEmpty(ediDr("SYUBETU_KB").ToString().Trim()) = True Then
            ediDr("SYUBETU_KB") = "10"
        End If

        '出荷先国内・輸出
        If String.IsNullOrEmpty(ediDr("NAIGAI_KB").ToString().Trim()) = True Then
            ediDr("NAIGAI_KB") = "01"
        End If

        '作業進捗区分
        If String.IsNullOrEmpty(ediDr("OUTKA_STATE_KB").ToString().Trim()) = True Then
            ediDr("OUTKA_STATE_KB") = "10"
        End If

        '出荷報告有無
        If String.IsNullOrEmpty(ediDr("OUTKAHOKOKU_YN").ToString().Trim()) = True Then
            If String.IsNullOrEmpty(mCustDr("OUTKA_RPT_YN").ToString().Trim()) = False Then
                ediDr("OUTKAHOKOKU_YN") = Right(mCustDr("OUTKA_RPT_YN").ToString().Trim(), 1)
            Else
                ediDr("OUTKAHOKOKU_YN") = "0"
            End If
        End If

        'ピッキングリスト区分
        If String.IsNullOrEmpty(ediDr("PICK_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("PICK_KB") = mDestDr("PICK_KB").ToString().Trim()
            Else
                ediDr("PICK_KB") = "01"
            End If
        End If

        '出庫日
        If String.IsNullOrEmpty(ediDr("OUTKO_DATE").ToString().Trim()) = True Then
            ediDr("OUTKO_DATE") = ediDr("OUTKA_PLAN_DATE")
        End If

        '当期保管料負担有無
        If String.IsNullOrEmpty(ediDr("TOUKI_HOKAN_YN").ToString().Trim()) = True Then
            ediDr("TOUKI_HOKAN_YN") = "1"
        End If

        '荷主名(大)
        If String.IsNullOrEmpty(ediDr("CUST_NM_L").ToString().Trim()) = True Then
            ediDr("CUST_NM_L") = ds.Tables("LMH030_M_CUST").Rows(0).Item("CUST_NM_L").ToString()
        End If

        '荷主名(中)
        If String.IsNullOrEmpty(ediDr("CUST_NM_M").ToString().Trim()) = True Then
            ediDr("CUST_NM_M") = ds.Tables("LMH030_M_CUST").Rows(0).Item("CUST_NM_M").ToString()
        End If

        '荷送人名(大)
        If String.IsNullOrEmpty(ediDr("SHIP_CD_L").ToString().Trim()) = True Then
            ediDr("SHIP_NM_L") = ""
        Else
            'DACで値セットを行う
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdestShip", ds)
            If ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows.Count <> 0 Then
                ediDr("SHIP_NM_L") = ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("DEST_NM").ToString().Trim()
            End If
        End If

        '指定納品書区分
        If String.IsNullOrEmpty(ediDr("SP_NHS_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("SP_NHS_KB") = mDestDr("SP_NHS_KB").ToString().Trim()
            End If
        End If

        '分析票添付区分
        If String.IsNullOrEmpty(ediDr("COA_YN").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("COA_YN") = mDestDr("COA_YN").ToString().Trim().Substring(1, 1)
                '要望番号:483((出荷登録時)EDI出荷(大,中)の更新不具合の"COA_YN") 2012/06/21 本明 Start
            Else
                '届先マスタに存在しない場合、自動追加の値と同値をセットする
                ediDr("COA_YN") = "0"  'SetInsMDestFromDestの値と一致させる事！（荷主により値が異なるため）
                '要望番号:483((出荷登録時)EDI出荷(大,中)の更新不具合の"COA_YN") 2012/06/21 本明 End
            End If
        End If

        '運送手配区分
        If String.IsNullOrEmpty(ediDr("UNSO_MOTO_KB").ToString().Trim()) = True Then
            ediDr("UNSO_MOTO_KB") = mCustDr("UNSO_TEHAI_KB").ToString().Trim()
        End If

        '便区分
        If String.IsNullOrEmpty(ediDr("BIN_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("BIN_KB").ToString().Trim()) = False Then
                    ediDr("BIN_KB") = mDestDr("BIN_KB")
                Else
                    ediDr("BIN_KB") = "01"
                End If
            Else
                ediDr("BIN_KB") = "01"
            End If
        End If

        '運送会社コード
        '運送会社支店コード
        '空の場合は届先マスタの値を設定、届先Mが空の場合は荷主マスタの値を設定
        If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = True AndAlso _
           String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString().Trim()) = True Then

            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("SP_UNSO_CD").ToString().Trim()) = False Then
                    ediDr("UNSO_CD") = mDestDr("SP_UNSO_CD").ToString().Trim()
                    ediDr("UNSO_BR_CD") = mDestDr("SP_UNSO_BR_CD").ToString().Trim()
                Else
                    ediDr("UNSO_CD") = mCustDr("SP_UNSO_CD").ToString().Trim()
                    ediDr("UNSO_BR_CD") = mCustDr("SP_UNSO_BR_CD").ToString().Trim()
                End If
            Else
                ediDr("UNSO_CD") = mCustDr("SP_UNSO_CD").ToString().Trim()
                ediDr("UNSO_BR_CD") = mCustDr("SP_UNSO_BR_CD").ToString().Trim()
            End If

        End If

        'タリフ分類区分
        '運賃タリフコード
        '割増タリフコード
        ''DACで値セットを行う
        'ds = MyBase.CallDAC(Me._DacCom, "SetTariffData", ds)

        'タリフ分類区分
        '運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        '割増タリフコード(割増運賃タリフマスタ)
        'DACで値セットを行う
        '(三井化学：EDIの時点で値が入っててもタリフMに存在しないケースがある為の対応)
        '①荷主明細マスタの存在チェック(荷主明細マスタに存在していれば入替えOK)
        '荷主明細マスタの取得
        ds = MyBase.CallDAC(Me._DacCom, "SelectListDataMcustDetails", ds)
        'タリフセットマスタの取得(運賃タリフ)
        ds = MyBase.CallDAC(Me._DacCom, "SetTariffData", ds)

        'タリフセットマスタの取得(割増タリフ)
        ds = MyBase.CallDAC(Me._DacCom, "SetExtcTariffData", ds)


        '配送時注意事項
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = True Then
        Else
            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("DELI_ATT").ToString().Trim()) = False Then

                    If String.IsNullOrEmpty(ediDr("UNSO_ATT").ToString().Trim()) = True Then

                        ediDr("UNSO_ATT") = mDestDr("DELI_ATT").ToString().Trim()
                    ElseIf InStr(ediDr("UNSO_ATT").ToString().Trim(), mDestDr("DELI_ATT").ToString().Trim()) > 0 Then
                    Else
                        ediDr("UNSO_ATT") = Me._Blc.LeftB(String.Concat(ediDr("UNSO_ATT").ToString() & Strings.Space(2), mDestDr("DELI_ATT").ToString().Trim()), 100)
                    End If
                End If

            End If

        End If

        '送り状作成有無
        If String.IsNullOrEmpty(ediDr("DENP_YN").ToString().Trim()) = True Then
            If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = False AndAlso _
                String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString().Trim()) = False Then
                '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start
                ''運送会社M取得
                'ds = MyBase.CallDAC(Me._DacCom, "SelectDataUnsoco", ds)
                '運送会社荷主別送り状マスタの存在チェック
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataUnsoCustRpt", ds)
                '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start
                If MyBase.GetResultCount = 0 Then
                    ediDr("DENP_YN") = "0"
                Else
                    ediDr("DENP_YN") = "1"
                End If
            Else
                ediDr("DENP_YN") = "0"
            End If

        End If

        '元着払区分
        If String.IsNullOrEmpty(ediDr("PC_KB").ToString().Trim()) = True Then
            ediDr("PC_KB") = "01"
        End If

        '運賃請求有無
        If (ediDr("UNSO_MOTO_KB").ToString()).Equals("10") = True OrElse _
           (ediDr("UNSO_MOTO_KB").ToString()).Equals("40") = True Then
            ediDr("UNCHIN_YN") = "1"
        Else
            ediDr("UNCHIN_YN") = "0"
        End If

        '荷役料有無
        If String.IsNullOrEmpty(ediDr("NIYAKU_YN").ToString().Trim()) = True Then
            ediDr("NIYAKU_YN") = "1"
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 届先コード設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>EDIデータの届先コードが空の場合、届先マスタの値を設定する
    ''' この設定はDB存在チェック後に行う</remarks>
    Private Function SetDestCd(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        '届先コード
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = True Then
            If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                ediDr("DEST_CD") = ds.Tables("LMH030_M_DEST").Rows(0)("DEST_CD").ToString().Trim() '2012.04.09 要望番号943 修正
            End If
        End If

        Return ds

    End Function

#End Region

#Region "EDI出荷(中)の初期値設定"

    ''' <summary>
    ''' EDI出荷(中)の初期値設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function EdiMDefaultSet(ByVal ds As DataSet, ByVal setDs As DataSet, _
                                    ByVal count As Integer, ByVal unsodata As String, _
                                    ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(count)
        Dim mGoodsDr As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)



        ''-------------------------------------------------------------------------------------
        ''●荷主固有チェック
        ''-------------------------------------------------------------------------------------

        Dim flgWarning As Boolean = False
        Dim compareWarningFlg As Boolean = False
        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        '商品名
        Dim mGoodsNm As String = Replace(mGoodsDr("GOODS_NM_1").ToString(), "ｰ", "-").Trim()
        Dim ediGoodsNm As String = Replace(ediMDr("GOODS_NM").ToString(), "ｰ", "-").Trim()
        Dim mDestGoodsNm As String = String.Empty
        If String.IsNullOrEmpty(ediMDr("DEST_GOODS_NM").ToString()) = False Then
            mDestGoodsNm = Replace(ediMDr("DEST_GOODS_NM").ToString(), "ｰ", "-").Trim()
        End If

        If mGoodsNm.Equals(ediGoodsNm) = True Then
            'チェックなし
            ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1").ToString()

        ElseIf String.IsNullOrEmpty(mGoodsNm) = False AndAlso String.IsNullOrEmpty(mDestGoodsNm) = False AndAlso _
               mDestGoodsNm.Equals(ediGoodsNm) = False Then
            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.SMK_WID_M001, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "商品名"
                msgArray(2) = "届先商品マスタ"
                msgArray(3) = "商品名"
                msgArray(4) = "注意) 受信した品名は「出荷時注意事項」に記載します。"
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningM("W194", LMH030BLC.SMK_WID_M001, ds, setDs, msgArray, _
            ediMDr("DEST_GOODS_NM").ToString(), mGoodsDr("GOODS_NM_1").ToString())
                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                If String.IsNullOrEmpty(ediMDr("REMARK").ToString()) = True Then
                    ediMDr("REMARK") = String.Concat("受信品名＝", ediMDr("GOODS_NM"))
                Else
                    ediMDr("REMARK") = _Blc.LeftB(String.Concat(ediMDr("REMARK"), Space(2), "受信品名＝", ediMDr("GOODS_NM")), 100)
                End If
                '全角変換したままの場合、桁あふれを起こす事がある
                ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1").ToString()

            End If

        ElseIf mGoodsNm.Equals(ediGoodsNm) = False Then

            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.UKIMA_WID_M002, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "商品名"
                msgArray(2) = "商品マスタ"
                msgArray(3) = "商品名"
                msgArray(4) = String.Empty
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningM("W194", LMH030BLC.UKIMA_WID_M002, ds, setDs, msgArray, _
                            ediMDr("GOODS_NM").ToString(), mGoodsDr("GOODS_NM_1").ToString())
                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                '全角変換したままの場合、桁あふれを起こす事がある
                ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1").ToString()

            End If

        End If


        'チェック不要
        '入目単位
        ''Dim mIRIMENm As String = mGoodsDr("STD_IRIME_UT").ToString()
        ''Dim ediIRIMENm As String = ediMDr("IRIME_UT").ToString()

        ''If String.IsNullOrEmpty(ediIRIMENm) = True Then
        ''    'チェックなし
        ''Else
        ''    If mIRIMENm.Equals(ediIRIMENm) = True Then
        ''        'チェックなし
        ''    Else
        ''        choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.SMK_WID_M003, count)

        ''        If String.IsNullOrEmpty(choiceKb) = True Then
        ''            msgArray(1) = "入目単位"
        ''            msgArray(2) = "商品マスタ"
        ''            msgArray(3) = "入目単位"
        ''            msgArray(4) = "EDIデータ"
        ''            msgArray(5) = String.Empty

        ''            ds = Me._Blc.SetComWarningM("W159", LMH030BLC.SMK_WID_M003, ds, setDs, msgArray, ediIRIMENm, mIRIMENm)

        ''            compareWarningFlg = True

        ''        ElseIf choiceKb.Equals("01") = True Then
        ''            'ワーニングで"はい"を選択時
        ''            ediMDr("IRIME_UT") = mIRIMENm

        ''        End If
        ''    End If
        ''End If

        '入目単位
        If String.IsNullOrEmpty(ediMDr("IRIME_UT").ToString()) = True Then
            ediMDr("IRIME_UT") = mGoodsDr("STD_IRIME_UT")
        Else
            If unsodata.Equals("01") = False AndAlso ediMDr("IRIME_UT").Equals(mGoodsDr("STD_IRIME_UT")) = False Then
                '運送データ以外でEDI(中)と商品マスタで入目単位が異なる場合、エラー(サクラ以外)
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"入目単位", "商品マスタ", "入目単位"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If



        'サーチキー
        ediMDr("FREE_C21") = mGoodsDr("SEARCH_KEY_1").ToString()
        
        '分析表区分
        If String.IsNullOrEmpty(ediMDr("COA_YN").ToString()) = True Then
            ediMDr("COA_YN") = (mGoodsDr("COA_YN").ToString()).Substring(1, 1)
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


        If Convert.ToDecimal(ediMDr("IRIME")) = 0 _
        AndAlso Convert.ToDecimal(mGoodsDr("STD_IRIME_NB")) <> 0 Then
            ediMDr("IRIME") = mGoodsDr("STD_IRIME_NB")
        End If

        '出荷包装個数
        '出荷端数
        Dim pkgNb As Double = Convert.ToDouble(ediMDr("PKG_NB"))
        Dim outkaPkgNb As Double = Convert.ToDouble(ediMDr("OUTKA_PKG_NB"))
        Dim outkaHasu As Double = Convert.ToDouble(ediMDr("OUTKA_HASU"))
        Dim alctdKb As String = ediMDr("ALCTD_KB").ToString
        Dim irime As Double = Convert.ToDouble(ediMDr("IRIME"))
        Dim outkaTtlQt As Double = Convert.ToDouble(ediMDr("OUTKA_TTL_QT"))

        Select Case alctdKb

            Case "01"
                If 1 < pkgNb Then

                    ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
                    ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb
                Else
                    ediMDr("OUTKA_PKG_NB") = pkgNb * outkaPkgNb + outkaHasu
                    ediMDr("OUTKA_HASU") = 0
                End If

                ediMDr("OUTKA_TTL_QT") = (pkgNb * outkaPkgNb + outkaHasu) * irime

            Case "02"
                ediMDr("OUTKA_PKG_NB") = 0
                If outkaTtlQt Mod irime = 0 Then
                    ediMDr("OUTKA_HASU") = outkaTtlQt / irime
                Else
                    ediMDr("OUTKA_HASU") = Math.Floor(outkaTtlQt / irime) + 1
                End If

                ediMDr("OUTKA_TTL_NB") = ediMDr("OUTKA_HASU")

            Case "03"
                ediMDr("OUTKA_PKG_NB") = 0
                ediMDr("OUTKA_HASU") = 0
                ediMDr("OUTKA_TTL_NB") = 0

            Case Else

        End Select

        '出荷数量
        ediMDr("OUTKA_QT") = ediMDr("OUTKA_TTL_QT")

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
        '(住化カラーはワーニング設定有)
        If compareWarningFlg = True Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "入力チェック(出荷登録処理)"

#Region "EDI出荷(大)のBLC側でのチェック"

    ''' <summary>
    ''' EDI出荷(大)のBLC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiLKanrenCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        '-------------------------------------------------------------------------------------
        '●荷主共通チェック
        '-------------------------------------------------------------------------------------

        '出荷管理番号
        If Me._Blc.OutkaCtlNoCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E349", New String() {"EDIデータ", "出荷管理番号"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷報告有無
        If Me._Blc.OutkaHokokuYnCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"出荷報告有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷予定日
        If Me._Blc.OutkaPlanDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出庫日
        If Me._Blc.OutkoDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷予定日+出庫日
        If Me._Blc.OutkaPlanLargeSmallCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E166", New String() {"出荷予定日", "出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '納入予定日
        If Me._Blc.arrPlanDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"納入予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷報告日
        If Me._Blc.HokokuDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷報告日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '荷主コード(大)
        If Me._Blc.CustCdLCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(大)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '荷主コード(中)
        If Me._Blc.CustCdMCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(中)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '送り状作成有無
        If Me._Blc.DenpYnCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"送り状作成有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Return True

    End Function

#End Region

#Region "EDI出荷(大)のDAC側でのチェック"

    ''' <summary>
    ''' EDI出荷(大)のDAC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiLDbExistsCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim drIn As DataRow = ds.Tables("LMH030INOUT").Rows(0)

        '-------------------------------------------------------------------------------------
        '●荷主共通チェック
        '-------------------------------------------------------------------------------------
        ''オーダー番号重複チェック
        If String.IsNullOrEmpty(drEdiL.Item("CUST_ORD_NO").ToString) = False Then

            If drIn("ORDER_CHECK_FLG").Equals("1") = True Then
                Call MyBase.CallDAC(Me._DacCom, "SelectOrderCheckData", ds)
                If MyBase.GetResultCount > 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"出荷データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If

            End If

        End If

        '出荷区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S014
        drJudge("KBN_CD") = drEdiL("OUTKA_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷種別区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S020
        drJudge("KBN_CD") = drEdiL("SYUBETU_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷種別区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '作業進捗区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S010
        drJudge("KBN_CD") = drEdiL("OUTKA_STATE_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"作業進捗区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        'ピッキングリスト区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_P001
        drJudge("KBN_CD") = drEdiL("PICK_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"ピッキングリスト区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '倉庫コード(倉庫マスタ)
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataSoko", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"倉庫コード", "倉庫マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '納入予定時刻(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N010
        drJudge("KBN_CD") = drEdiL("ARR_PLAN_TIME")

        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"納入予定時刻", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '荷主コード(荷主マスタ)
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataMcust", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"荷主コード", "荷主マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送元区分(区分マスタ) 注)値は運送手配区分を使用
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U005
        drJudge("KBN_CD") = drEdiL("UNSO_MOTO_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送手配区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送手配区分(区分マスタ) 注)値はタリフ分類区分を使用
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_T015
        drJudge("KBN_CD") = drEdiL("UNSO_TEHAI_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"タリフ分類区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '車輌区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S012
        drJudge("KBN_CD") = drEdiL("SYARYO_KB")
        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else

            ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"車輌区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        End If

        '便区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U001
        drJudge("KBN_CD") = drEdiL("BIN_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"便区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送会社コード
        If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = False OrElse String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = False Then

            If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = True Then
                drEdiL("UNSO_CD") = String.Empty
            End If

            If String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = True Then
                drEdiL("UNSO_BR_CD") = String.Empty
            End If

            Call MyBase.CallDAC(Me._DacCom, "SelectDataUnsoco", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送会社コード", "運送会社マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        Dim unchinTariffCd As String = String.Empty
        unchinTariffCd = drEdiL("UNCHIN_TARIFF_CD").ToString()
        Dim unsoTehaiKb As String = String.Empty
        unsoTehaiKb = drEdiL("UNSO_TEHAI_KB").ToString()

        If String.IsNullOrEmpty(unchinTariffCd) = True Then

        Else

            If unsoTehaiKb.Equals("40") = True Then
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataMyokoTariffHd", ds)
            Else
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataMunchinTariff", ds)
            End If

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運賃タリフコード", "運賃タリフマスタまたは横持ちヘッダー"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        End If

        '割増運賃タリフコード(割増運賃タリフマスタ)
        Dim extcTariffCd As String = String.Empty
        extcTariffCd = drEdiL("EXTC_TARIFF_CD").ToString()
        If String.IsNullOrEmpty(extcTariffCd) = True Then

        Else
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMextcUnchin", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"割増運賃タリフコード", "割増運賃タリフマスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '元着払い区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_M001
        drJudge("KBN_CD") = drEdiL("PC_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"元着払い区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '-------------------------------------------------------------------------------------
        '●荷主固有チェック
        '-------------------------------------------------------------------------------------
        Dim flgWarning As Boolean = False

        '届先マスタ存在チェック
        Dim destCd As String = drEdiL("DEST_CD").ToString()         '届先コード
        Dim ediDestCd As String = drEdiL("EDI_DEST_CD").ToString()  'EDI届先コード
        Dim workDestCd As String = String.Empty                     '検索する届先コード格納変数
        Dim workDestString As String = String.Empty                 '"届先コード"or"EDI届先コード"
        Dim dtMS As DataTable = ds.Tables("LMH030_M_DEST_SHIP_CD_L")


        'DEST_CDが空の場合、EDI_DEST_CDを使う
        If String.IsNullOrEmpty(destCd) = False Then
            workDestCd = destCd
            workDestString = "届先コード"
        ElseIf String.IsNullOrEmpty(ediDestCd) = False Then
            workDestCd = ediDestCd
            workDestString = "EDI届先コード"
        Else
        End If

        Dim mDestCount As Integer = ds.Tables("LMH030_M_DEST").Rows.Count

        If mDestCount = 1 Then
            '1件に特定できた場合、マスタ値とEDI出荷(大)の整合性チェックとZIPコードのマスタ存在チェック
            If Me.DestCompareCheck(ds, rowNo, ediCtlNo) = False Then

                Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                    '整合性チェックでエラーがあった場合は処理終了
                    Return False
                Else
                    '整合性チェックでワーニングがあった場合は、flgWarning=True
                    flgWarning = True
                End If
            End If

        ElseIf mDestCount = 0 Then
            '0件の場合、ZIPコードのマスタ存在チェックを行い、届先マスタの更新をする
            'JISマスタに存在しない場合、エラー
            'JISマスタに存在するが、JISが空の場合、ワーニング
            If Me.ZipCompareCheck(ds, rowNo, ediCtlNo, workDestCd, workDestString) = False Then
                Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                    'チェックでエラーがあった場合は処理終了
                    Return False
                Else
                    'ワーニング⇒マスタ追加(ワーニング設定した場合はflgWarning=True)
                    flgWarning = True
                End If
            End If

        Else
            '複数件の場合、エラー
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {"EDI届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If


        Return True

    End Function

#End Region

#Region "EDI出荷(中)のBLC側でのチェック"

    ''' <summary>
    ''' EDI出荷(中)のBLC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiMKanrenCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        '引当単位区分
        If Me._Blc.AlctdKbCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"引当単位区分"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '温度区分 + 便区分
        If Me._Blc.OndoBinKbCheck(dtL, dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E352", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷端数
        If Me._Blc.OutkaHasuCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '入目 + 出荷総数量
        If Me._Blc.IrimeSosuryoLargeSmallCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"入目と出荷総数量"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '赤黒区分
        If Me._Blc.AkakuroKbCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Return True

    End Function

#End Region

#Region "EDI出荷(中)のDAC側でのチェック + 初期値設定"

    ''' <summary>
    ''' EDI出荷(中)のDAC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiMMasterExistsCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

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

                choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.SMK_WID_M001, 0)

                If choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                End If

                '商品マスタ検索（NRS商品コード or 荷主商品コード）
                setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsOutka", setDs))

                If MyBase.GetResultCount = 0 Then
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"商品コード", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End
                    Return False
                ElseIf GetResultCount() > 1 Then

                    '入目 + 荷主商品コードで再検索
                    setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsIrimeOutka", setDs))

                    If MyBase.GetResultCount = 1 Then
                    Else
                        '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                        '注意!!! セットメッセージは消してよいのか判断がつかないので調査する
                        'MyBase.SetMessage("W162")
                        msgArray(1) = String.Empty
                        msgArray(2) = String.Empty
                        msgArray(3) = String.Empty
                        msgArray(4) = String.Empty

                        ds = Me._Blc.SetComWarningM("W162", LMH030BLC.SMK_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)

                        flgWarning = True 'ワーニングフラグをたてて処理続行

                        Continue For
                    End If

                End If

                'EDI出荷(中)の初期値設定処理
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                    '住化カラーは現段階ではワーニングが存在するが、エラーはなし。共通のロジックを組み込む為入れておく
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

#Region "届先マスタチェック"
    ''' <summary>
    ''' マスタ値とEDI出荷(大)の整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DestCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtMZip As DataTable = ds.Tables("LMH030_M_ZIP")

        Dim mSysDelF As String = dtMdest.Rows(0).Item("SYS_DEL_FLG").ToString()
        Dim mDestNm As String = dtMdest.Rows(0).Item("DEST_NM").ToString()
        Dim mAd1 As String = dtMdest.Rows(0).Item("AD_1").ToString()
        Dim mAd2 As String = dtMdest.Rows(0).Item("AD_2").ToString()
        Dim mAd3 As String = dtMdest.Rows(0).Item("AD_3").ToString()
        Dim mZip As String = dtMdest.Rows(0).Item("ZIP").ToString()
        Dim mTel As String = dtMdest.Rows(0).Item("TEL").ToString()
        Dim mJis As String = dtMdest.Rows(0).Item("JIS").ToString()
        Dim mAdAll As String = String.Concat(mAd1, mAd2, mAd3)
        Dim mZipJis As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediDestCd As String = dtEdi.Rows(0)("DEST_CD").ToString()
        Dim ediDestNm As String = dtEdi.Rows(0)("DEST_NM").ToString()
        Dim ediZip As String = dtEdi.Rows(0)("DEST_ZIP").ToString()
        Dim ediTel As String = dtEdi.Rows(0)("DEST_TEL").ToString()
        Dim ediFreeC21 As String = dtEdi.Rows(0)("FREE_C21").ToString()
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        Dim compareWarningFlg As Boolean = False

        '削除フラグ(届先マスタ)
        If mSysDelF.Equals("1") = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E331", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        mDestNm = _Blc.SpaceCutChk(mDestNm)
        ediDestNm = _Blc.SpaceCutChk(ediDestNm)

        '※名称マスタチェック不要
        ' ''届先名称(マスタ値が完全一致でなければワーニング)
        ''If mDestNm.Equals(ediDestNm) = True Then
        ''    'チェックなし
        ''Else
        ''    choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SMK_WID_L006, 0)

        ''    If String.IsNullOrEmpty(choiceKb) = True Then
        ''        msgArray(1) = "届先名称"
        ''        msgArray(2) = "届先マスタ"
        ''        msgArray(3) = "届先名称"
        ''        msgArray(4) = "EDIデータ"
        ''        msgArray(5) = String.Empty

        ''        ds = Me._Blc.SetComWarningL("W166", LMH030BLC.SMK_WID_L006, ds, msgArray, _
        ''                                    dtEdi.Rows(0)("DEST_NM").ToString(), dtMdest.Rows(0).Item("DEST_NM").ToString())

        ''        compareWarningFlg = True

        ''    ElseIf choiceKb.Equals("01") = True Then
        ''        'ワーニングで"はい"を選択時
        ''        dtMdest.Rows(0).Item("DEST_NM") = dtEdi.Rows(0)("DEST_NM").ToString()
        ''        'マスタ更新対象フラグ
        ''        dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

        ''    End If

        ''End If

        '※届先住所マスタチェック不要
        ' ''FREE_C21:届先住所(マスタ値が完全一致でなければワーニング)
        ''If String.IsNullOrEmpty(ediFreeC21) = True Then
        ''    'チェックなし
        ''Else

        ''    mAdAll = SpaceCutChk(mAdAll)
        ''    ediFreeC21 = SpaceCutChk(ediFreeC21)
        ''    If mAdAll.Equals(ediFreeC21) = False Then

        ''        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SMK_WID_L007, 0)

        ''        If String.IsNullOrEmpty(choiceKb) = True Then

        ''            msgArray(1) = "届先住所"
        ''            msgArray(2) = "届先マスタ"
        ''            msgArray(3) = "住所"
        ''            msgArray(4) = "EDIデータ"
        ''            msgArray(5) = String.Empty
        ''            ds = Me._Blc.SetComWarningL("W166", LMH030BLC.SMK_WID_L007, ds, msgArray, ediFreeC21, mAdAll)

        ''            compareWarningFlg = True

        ''        ElseIf choiceKb.Equals("01") = True Then
        ''            'ワーニングで"はい"を選択時
        ''            dtMdest.Rows(0).Item("AD_1") = dtEdi.Rows(0)("DEST_AD_1").ToString()
        ''            dtMdest.Rows(0).Item("AD_2") = dtEdi.Rows(0)("DEST_AD_2").ToString()
        ''            dtMdest.Rows(0).Item("AD_3") = dtEdi.Rows(0)("DEST_AD_3").ToString()
        ''            'マスタ更新対象フラグ
        ''            dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

        ''        End If
        ''    End If

        ''End If


        '届先郵便番号(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediZip) = True Then
            'チェックなし
        Else
            If mZip.Equals(Replace(ediZip, "-", String.Empty)) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SMK_WID_L008, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先郵便番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "郵便番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.SMK_WID_L008, ds, msgArray, ediZip, mZip)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("ZIP") = dtEdi.Rows(0)("DEST_ZIP").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If

        '届先電話番号(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediTel) = True Then
            'チェックなし
        Else
            If mTel.Equals(ediTel) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SMK_WID_L008, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先電話番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "電話番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.SMK_WID_L008, ds, msgArray, ediTel, mTel)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("TEL") = dtEdi.Rows(0)("DEST_TEL").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If

        '郵便番号を元に、郵便番号マスタよりJISコードを取得する。
        'JISマスタ存在チェック
        Dim warningString As String = String.Empty

        If String.IsNullOrEmpty(ediZip) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromZip", ds)

            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "郵便番号マスタ"
            End If

        End If

        'Else
        '取得できなかった場合は、再度住所を元にJISマスタよりJISコードを取得する
        If String.IsNullOrEmpty(mZipJis) = True Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromAdd", ds)

            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "JISマスタ"
            End If
        End If


        If String.IsNullOrEmpty(mZipJis) = True AndAlso String.IsNullOrEmpty(ediDestJisCd) = False Then
            'JISマスタのJISコードが空or郵便番号マスタのJISコードが空の場合かつ、EDIデストコードに値がある場合、更新ワーニング
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SMK_WID_L009, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = "JISコード"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = "EDIデータ"
                msgArray(5) = "※郵便番号・住所からJISコードが特定できないため、出荷画面で運賃がゼロになる可能性があります。"
                ds = Me._Blc.SetComWarningL("W197", LMH030BLC.SMK_WID_L009, ds, msgArray, ediDestJisCd, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("JIS") = ediDestJisCd
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            End If


        ElseIf String.IsNullOrEmpty(mZipJis) = True AndAlso String.IsNullOrEmpty(ediDestJisCd) = True Then
            'JISマスタのJISコードが空or郵便番号マスタのJISコードが空の場合かつ、EDIデストコードが空の場合、処理続行確認
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SMK_WID_L010, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                ds = Me._Blc.SetComWarningL("W188", LMH030BLC.SMK_WID_L010, ds, msgArray, ediDestJisCd, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時

            End If

        ElseIf String.IsNullOrEmpty(ediDestJisCd) = False AndAlso ediDestJisCd.Equals(mJis) = False Then
            'EDIのJISコードが空でなくEDIのJISコードと届先マスタのJISコードに差異がある場合
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SMK_WID_L011, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = "JISコード"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W187", LMH030BLC.SMK_WID_L011, ds, msgArray, ediDestJisCd, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("JIS") = ediDestJisCd
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            ElseIf choiceKb.Equals("02") = True Then
                'ワーニングで"いいえ"を選択時
            End If

        ElseIf String.IsNullOrEmpty(ediDestJisCd) = True AndAlso mZipJis.Equals(mJis) = False Then
            'EDIのJISコードが空でJISマスタ(郵便番号マスタ)のJISコードと届先マスタのJISコードに差異がある場合
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SMK_WID_L011, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = String.Concat(warningString, "から取得したJISコード")
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = warningString
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W187", LMH030BLC.SMK_WID_L011, ds, msgArray, mZipJis, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("JIS") = mZipJis
                dtEdi.Rows(0)("DEST_JIS_CD") = mZipJis
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            ElseIf choiceKb.Equals("02") = True Then
                'ワーニングで"いいえ"を選択時
            End If

        End If

        'ワーニングが存在する場合はここでの判定はFalseで返す
        If compareWarningFlg = True Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "届先マスタ追加時チェック"
    ''' <summary>
    ''' 届先マスタ追加時チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ZipCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String, ByVal workDestCd As String, ByVal workDestString As String) As Boolean

        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtMJis As DataTable = ds.Tables("LMH030_M_JIS")
        Dim drEdiL As DataRow = dtEdi.Rows(0)

        'Dim mZip As String = dtMdest.Rows(0).Item("ZIP").ToString()
        'Dim mTel As String = dtMdest.Rows(0).Item("TEL").ToString()
        'Dim mJis As String = dtMdest.Rows(0).Item("JIS").ToString()
        Dim mZipJis As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediZip As String = dtEdi.Rows(0)("DEST_ZIP").ToString()
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        Dim compareWarningFlg As Boolean = False

        '2012.03.28 要望番号948 修正START
        '郵便番号を元に、郵便番号マスタよりJISコードを取得する。
        'JISマスタ存在チェック
        Dim warningString As String = String.Empty

        If String.IsNullOrEmpty(ediZip) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromZip", ds)

            'If MyBase.GetResultCount = 0 Then
            '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"届先JISコード", "JISマスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return False
            'End If

            'mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
            'warningString = "郵便番号マスタ"

            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "郵便番号マスタ"
            End If

        End If

        '取得できなかった場合は、再度住所を元にJISマスタよりJISコードを取得する
        If String.IsNullOrEmpty(mZipJis) = True Then
            'Else
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromAdd", ds)

            'If MyBase.GetResultCount = 0 Then
            '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"届先住所１＋届先住所２＋届先住所３", "JISマスタ", "県＋市"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return False
            'End If

            'mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
            'warningString = "JISマスタ"

            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "JISマスタ"
            End If

        End If
        '2012.03.28 要望番号948 修正END

        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SMK_WID_L012, 0)

        If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = workDestString
            msgArray(2) = "届先マスタ"
            msgArray(3) = "EDIデータ"
            If String.IsNullOrEmpty(mZipJis) = False Then
                msgArray(4) = String.Empty
            Else
                msgArray(4) = "※郵便番号・住所からJISコードが特定できないため、出荷画面で運賃がゼロになる可能性があります。"
            End If

            ds = Me._Blc.SetComWarningL("W186", LMH030BLC.SMK_WID_L012, ds, msgArray, workDestCd, String.Empty) '追加箇所 20110222

            compareWarningFlg = True
            'Return True

        ElseIf choiceKb.Equals("01") = True Then
            'ワーニングで"はい"を選択時
            Dim drMD As DataRow = dtMdest.NewRow()
            drMD("NRS_BR_CD") = drEdiL("NRS_BR_CD").ToString()
            drMD("CUST_CD_L") = drEdiL("CUST_CD_L").ToString()
            drMD("DEST_CD") = workDestCd
            drMD("EDI_CD") = workDestCd
            If String.IsNullOrEmpty(drEdiL("DEST_NM").ToString()) = False Then
                drMD("DEST_NM") = drEdiL("DEST_NM").ToString()
            End If
            drMD("ZIP") = drEdiL("DEST_ZIP").ToString()
            drMD("AD_1") = drEdiL("DEST_AD_1").ToString()
            drMD("AD_2") = drEdiL("DEST_AD_2").ToString()
            drMD("AD_3") = drEdiL("DEST_AD_3").ToString()
            drMD("COA_YN") = "00"
            drMD("TEL") = drEdiL("DEST_TEL").ToString()

            'If String.IsNullOrEmpty(drEdiL("DEST_ZIP").ToString) = False _
            '   AndAlso String.IsNullOrEmpty(drEdiL("DEST_JIS_CD").ToString) = True Then
            '    drMD("JIS") = dtMJis.Rows(0).Item("JIS_CD").ToString()
            'Else
            '    drMD("JIS") = drEdiL("DEST_JIS_CD").ToString()
            'End If
            drMD("JIS") = mZipJis
            drMD("PICK_KB") = "01"
            drMD("BIN_KB") = "01"
            'マスタ自動追加対象フラグ
            drMD("MST_INSERT_FLG") = "1"
            dtMdest.Rows.Add(drMD)

        End If

        'ワーニングが存在する場合はここでの判定はFalseで返す
        If compareWarningFlg = True Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "ワーニング処理(EDI(大)届先)選択区分の取得"

    ''' <summary>
    ''' 選択区分の取得
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDestWarningChoiceKb(ByVal setDt As DataTable, ByVal ds As DataSet, _
                                           ByVal warningId As String, ByVal count As Integer) As String

        Dim dtWarning As DataTable = ds.Tables("WARNING_SHORI")
        Dim ediCtlNoL As String = setDt.Rows(count)("EDI_CTL_NO").ToString()
        Dim max As Integer = dtWarning.Rows.Count - 1
        Dim dr As DataRow
        Dim choiceKb As String = String.Empty

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return choiceKb
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) Then
                'ワーニング処理設定の値を反映
                choiceKb = dr.Item("CHOICE_KB").ToString()
                Return choiceKb

            End If

        Next

        Return choiceKb
    End Function

#End Region

#Region "ワーニング処理(EDI(中)商品)選択区分の取得"

    ''' <summary>
    ''' 選択区分の取得
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetGoodsWarningChoiceKb(ByRef setDt As DataTable, ByVal ds As DataSet, _
                                           ByVal warningId As String, ByVal count As Integer) As String

        Dim dtWarning As DataTable = ds.Tables("WARNING_SHORI")
        Dim ediCtlNoL As String = setDt.Rows(0)("EDI_CTL_NO").ToString()
        Dim ediCtlNoM As String = setDt.Rows(count)("EDI_CTL_NO_CHU").ToString()
        Dim max As Integer = dtWarning.Rows.Count - 1
        Dim dr As DataRow
        Dim choiceKb As String = String.Empty
        Dim mstFlg As String = String.Empty

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return choiceKb
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) _
                                                                AndAlso ediCtlNoM.Equals(dr("EDI_CTL_NO_M")) Then
                'ワーニング画面の処理区分値を反映
                choiceKb = dr.Item("CHOICE_KB").ToString()

                mstFlg = warningId.Substring(7, 1)

                Select Case mstFlg
                    Case "1"
                        'ワーニング処理設定の値を反映
                        setDt.Rows(0).Item("NRS_GOODS_CD") = dr.Item("MST_VALUE")
                    Case Else

                End Select

                Return choiceKb

            End If

        Next

        Return choiceKb
    End Function

#End Region

#End Region

#Region "データセット設定"

#Region "データセット設定(出荷管理番号L)"

    ''' <summary>
    ''' データセット設定(出荷管理番号L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="matomeFlg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoL(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim outkaKanriNo As String = String.Empty
        Dim dr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim outkaKanriNoPrm As String = ds.Tables("LMH030INOUT").Rows(0).Item("OUTKA_CTL_NO").ToString()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

        Dim max As Integer = dt.Rows.Count - 1

        If eventShubetsu.Equals("3") = True Then

            '紐付け処理の場合
            dr("OUTKA_CTL_NO") = outkaKanriNoPrm
            For i As Integer = 0 To max
                dt.Rows(i)("OUTKA_CTL_NO") = outkaKanriNoPrm
            Next

        ElseIf matomeFlg = False Then

            '通常出荷登録処理の場合
            Dim num As New NumberMasterUtility
            outkaKanriNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.OUTKA_NO_L, Me, nrsBrCd)

            dr("OUTKA_CTL_NO") = outkaKanriNo

            For i As Integer = 0 To max
                dt.Rows(i)("OUTKA_CTL_NO") = outkaKanriNo
            Next

        Else
            'まとめ処理の場合
            outkaKanriNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("OUTKA_CTL_NO").ToString()
            dr("OUTKA_CTL_NO") = outkaKanriNo
            dr("FREE_C30") = String.Concat("04-", ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("EDI_CTL_NO").ToString())

            For i As Integer = 0 To max
                dt.Rows(i)("OUTKA_CTL_NO") = outkaKanriNo
            Next

        End If

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷管理番号M)"
    ''' <summary>
    ''' 出荷管理番号(中)取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoM(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim outkaKanriNo As String = String.Empty
        Dim dtEdiM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtHimoduke As DataTable = ds.Tables("LMH030_HIMODUKE")
        Dim nrsBrCd As String = dtEdiM.Rows(0).ToString
        Dim max As Integer = dtEdiM.Rows.Count - 1
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

        If eventShubetsu.Equals("3") = True Then
            '紐付け処理の場合
            For i As Integer = 0 To max
                outkaKanriNo = dtHimoduke.Rows(i)("HIMODUKE_NO").ToString()
                dtEdiM.Rows(i)("OUTKA_CTL_NO_CHU") = outkaKanriNo
            Next

        ElseIf matomeFlg = False Then
            '通常出荷登録処理の場合
            For i As Integer = 0 To max
                outkaKanriNo = (i + 1).ToString("000")
                dtEdiM.Rows(i)("OUTKA_CTL_NO_CHU") = outkaKanriNo
            Next

        Else
            'まとめ処理の場合、まとめ先DataSetから取得
            Dim maxOutkaKanriNo As Integer = Me._DacCom.GetMaxOUTKA_NO_CHU(ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("NRS_BR_CD").ToString, ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("OUTKA_CTL_NO").ToString)
            For i As Integer = 0 To max
                outkaKanriNo = (maxOutkaKanriNo + i + 1).ToString("000")
                dtEdiM.Rows(i)("OUTKA_CTL_NO_CHU") = outkaKanriNo
            Next

        End If

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷L)"
    ''' <summary>
    ''' データセット設定(出荷L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetOutkaL(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

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
            outkaDr("OUTKAHOKOKU_YN") = _Blc.FormatZero(ediDr("OUTKAHOKOKU_YN").ToString(), 2)
            outkaDr("PICK_KB") = ediDr("PICK_KB")
            outkaDr("DENP_NO") = String.Empty
            outkaDr("ARR_KANRYO_INFO") = String.Empty
            outkaDr("WH_CD") = ediDr("WH_CD")
            outkaDr("OUTKA_PLAN_DATE") = ediDr("OUTKA_PLAN_DATE")
            outkaDr("OUTKO_DATE") = ediDr("OUTKO_DATE")
            outkaDr("ARR_PLAN_DATE") = ediDr("ARR_PLAN_DATE")
            outkaDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
            outkaDr("HOKOKU_DATE") = ediDr("HOKOKU_DATE")
            outkaDr("TOUKI_HOKAN_YN") = _Blc.FormatZero(ediDr("TOUKI_HOKAN_YN").ToString(), 2)
            outkaDr("END_DATE") = String.Empty
            outkaDr("CUST_CD_L") = ediDr("CUST_CD_L")
            outkaDr("CUST_CD_M") = ediDr("CUST_CD_M")
            outkaDr("SHIP_CD_L") = ediDr("SHIP_CD_L")
            outkaDr("SHIP_CD_M") = String.Empty

            If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                Dim destMDr As DataRow = ds.Tables("LMH030_M_DEST").Rows(0)
                outkaDr("DEST_CD") = destMDr("DEST_CD")
                outkaDr("DEST_AD_3") = destMDr("AD_3")
                outkaDr("DEST_TEL") = destMDr("TEL")
            Else
                outkaDr("DEST_CD") = ediDr("DEST_CD")
                outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")
                outkaDr("DEST_TEL") = ediDr("DEST_TEL")
            End If

            outkaDr("NHS_REMARK") = String.Empty
            outkaDr("SP_NHS_KB") = ediDr("SP_NHS_KB")
            outkaDr("COA_YN") = _Blc.FormatZero(ediDr("COA_YN").ToString(), 2)
            outkaDr("CUST_ORD_NO") = ediDr("CUST_ORD_NO")
            outkaDr("BUYER_ORD_NO") = ediDr("BUYER_ORD_NO")
            outkaDr("REMARK") = ediDr("REMARK")
            outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt)
            outkaDr("DENP_YN") = _Blc.FormatZero(ediDr("DENP_YN").ToString(), 2)
            outkaDr("PC_KB") = ediDr("PC_KB")
            outkaDr("UNCHIN_YN") = _Blc.FormatZero(ediDr("UNCHIN_YN").ToString(), 2)
            outkaDr("NIYAKU_YN") = _Blc.FormatZero(ediDr("NIYAKU_YN").ToString(), 2)
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
            outkaDr("DEST_KB") = "02"
            outkaDr("DEST_NM") = ediDr("DEST_NM")
            outkaDr("DEST_AD_1") = ediDr("DEST_AD_1")
            outkaDr("DEST_AD_2") = ediDr("DEST_AD_2")
        Else
            'まとめ登録処理
            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt) + Convert.ToDouble(matomesakiDt.Rows(0)("OUTKA_PKG_NB"))
            outkaDr("SYS_UPD_DATE") = matomesakiDt.Rows(0)("SYS_UPD_DATE")
            outkaDr("SYS_UPD_TIME") = matomesakiDt.Rows(0)("SYS_UPD_TIME")
        End If
        'データセットに設定
        ds.Tables("LMH030_C_OUTKA_L").Rows.Add(outkaDr)

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷包装個数)"
    Private Function SumPkgNb(ByVal dt As DataTable) As Double

        Dim max As Integer = dt.Rows.Count - 1
        Dim sumNb As Double = 0
        Dim calcPkgModNb As Long = 0
        Dim calcPkgQuoNb As Double = 0

        For i As Integer = 0 To max

            If String.IsNullOrEmpty(dt.Rows(i)("PKG_NB").ToString()) = False _
            AndAlso String.IsNullOrEmpty(dt.Rows(i)("OUTKA_TTL_NB").ToString()) = False _
            AndAlso (dt.Rows(i)("PKG_NB").ToString()).Equals("0") = False _
            AndAlso (dt.Rows(i)("OUTKA_TTL_NB").ToString()).Equals("0") = False Then

                calcPkgModNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) / Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Math.Floor(calcPkgQuoNb)

            End If

            sumNb = sumNb + Convert.ToDouble(dt.Rows(i)("OUTKA_PKG_NB"))

            If 0 = calcPkgModNb Then
            Else
                sumNb = sumNb + 1
            End If

        Next

        Return sumNb

    End Function
#End Region

#Region "データセット設定(出荷M)"
    ''' <summary>
    ''' データセット設定(出荷M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetOutkaM(ByVal ds As DataSet, ByVal matomeflg As Boolean) As DataSet
        '要望番号:1767(千葉 住化カラー納品書明細行のオーダー番号間違い) 2013/01/11 Start
        'Private Function SetDatasetOutkaM(ByVal ds As DataSet) As DataSet
        '要望番号:1767(千葉 住化カラー納品書明細行のオーダー番号間違い) 2013/01/11 End
        Dim ediDr As DataRow
        Dim outkaDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim remark As String = String.Empty
        Dim SetNo As String = String.Empty
        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim max As Integer = dt.Rows.Count - 1
        Dim calcPkgModNb As Long = 0
        Dim calcPkgQuoNb As Double = 0

        '要望番号:1767(千葉 住化カラー納品書明細行のオーダー番号間違い) 2013/01/11 Start
        Dim matomesakiDt As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")
        '要望番号:1767(千葉 住化カラー納品書明細行のオーダー番号間違い) 2013/01/11 End


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
            outkaDr("COA_YN") = _Blc.FormatZero(ediDr("COA_YN").ToString(), 2)

            '要望番号:1767(千葉 住化カラー納品書明細行のオーダー番号間違い) 2013/01/11 Start
            'EDI出荷(大)のオーダ番号とEDI出荷(中)のオーダ番号が同一である場合、出荷(中)のオーダ番号を空で登録する。
            'ただしまとめた場合はまとめ先のEDI出荷(大)を参照する
            'outkaDr("CUST_ORD_NO_DTL") = ediDr("CUST_ORD_NO_DTL")
            Dim sCustOrdNo As String = vbNullString
            If matomeflg Then   'まとめの場合
                sCustOrdNo = matomesakiDt.Rows(0)("CUST_ORD_NO").ToString   'まとめ先のEDI出荷(大)を参照
            Else                'まとめでない場合
                sCustOrdNo = ediLDr("CUST_ORD_NO").ToString                 '自分のEDI出荷(大)を参照
            End If

            If ediDr("CUST_ORD_NO_DTL").ToString = sCustOrdNo Then
                outkaDr("CUST_ORD_NO_DTL") = vbNullString
            Else
                outkaDr("CUST_ORD_NO_DTL") = ediDr("CUST_ORD_NO_DTL")
            End If
            '要望番号:1767(千葉 住化カラー納品書明細行のオーダー番号間違い) 2013/01/11 End


            '要望番号:1767(千葉 住化カラー納品書明細行のオーダー番号間違い) 2013/01/11 Start
            'EDI出荷(大)の注文番号とEDI出荷(中)の注文番号が同一である場合、出荷(中)の注文番号を空で登録する。
            'ただしまとめた場合はまとめ先のEDI出荷(大)を参照する
            'outkaDr("BUYER_ORD_NO_DTL") = ediDr("BUYER_ORD_NO_DTL")
            Dim sBuyerOrdNo As String = vbNullString
            If matomeflg Then   'まとめの場合
                sBuyerOrdNo = matomesakiDt.Rows(0)("BUYER_ORD_NO").ToString 'まとめ先のEDI出荷(大)を参照
            Else                'まとめでない場合
                sBuyerOrdNo = ediLDr("BUYER_ORD_NO").ToString               '自分のEDI出荷(大)を参照
            End If

            If ediDr("BUYER_ORD_NO_DTL").ToString = sBuyerOrdNo Then
                outkaDr("BUYER_ORD_NO_DTL") = vbNullString
            Else
                outkaDr("BUYER_ORD_NO_DTL") = ediDr("BUYER_ORD_NO_DTL")
            End If
            '要望番号:1767(千葉 住化カラー納品書明細行のオーダー番号間違い) 2013/01/11 End


            outkaDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            outkaDr("RSV_NO") = ediDr("RSV_NO")
            outkaDr("LOT_NO") = ediDr("LOT_NO")
            outkaDr("SERIAL_NO") = ediDr("SERIAL_NO")
            outkaDr("ALCTD_KB") = ediDr("ALCTD_KB")
            outkaDr("OUTKA_PKG_NB") = ediDr("PKG_NB")
            outkaDr("OUTKA_HASU") = ediDr("OUTKA_HASU")
            outkaDr("OUTKA_QT") = ediDr("OUTKA_QT")
            outkaDr("OUTKA_TTL_NB") = ediDr("OUTKA_TTL_NB")
            outkaDr("OUTKA_TTL_QT") = ediDr("OUTKA_TTL_QT")
            outkaDr("ALCTD_NB") = 0
            outkaDr("ALCTD_QT") = 0
            outkaDr("BACKLOG_NB") = ediDr("OUTKA_TTL_NB")
            outkaDr("BACKLOG_QT") = ediDr("OUTKA_TTL_QT")
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

#Region "データセット設定(EDI受信DTL)"
    ''' <summary>
    ''' データセット設定(EDI受信テーブル(DTL))
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiRcvDtl(ByVal ds As DataSet) As DataSet

        Dim rcvDr As DataRow
        Dim outkaedimDr As DataRow
        Dim outkaedilDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim max As Integer = dt.Rows.Count - 1
        Dim outkaCtlNoChu As String = String.Empty

        For i As Integer = 0 To max

            outkaedimDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            rcvDr = ds.Tables("LMH030_EDI_RCV_DTL").NewRow()

            rcvDr("NRS_BR_CD") = outkaedilDr("NRS_BR_CD")
            rcvDr("EDI_CTL_NO") = outkaedilDr("EDI_CTL_NO")
            rcvDr("EDI_CTL_NO_CHU") = outkaedimDr("EDI_CTL_NO_CHU")
            rcvDr("OUTKA_CTL_NO") = outkaedimDr("OUTKA_CTL_NO")
            rcvDr("OUTKA_CTL_NO_CHU") = outkaedimDr("OUTKA_CTL_NO_CHU").ToString()
            rcvDr("SYS_DEL_FLG") = "0"

            '住化カラー専用機能
            rcvDr("SEARCH_KEY_1") = outkaedimDr("FREE_C21") 'SEARCH_KEY_1の設定

            'データセットに設定
            ds.Tables("LMH030_EDI_RCV_DTL").Rows.Add(rcvDr)

        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(EDI受信HED)"
    ''' <summary>
    ''' データセット設定(EDI受信テーブル(HED))
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiRcvHed(ByVal ds As DataSet) As DataSet

        Dim rcvDr As DataRow = ds.Tables("LMH030_EDI_RCV_HED").NewRow()
        Dim inDr As DataRow = ds.Tables("LMH030INOUT").Rows(0)
        Dim outkaediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        rcvDr("NRS_BR_CD") = outkaediDr("NRS_BR_CD")
        rcvDr("EDI_CTL_NO") = outkaediDr("EDI_CTL_NO")
        rcvDr("CUST_CD_L") = outkaediDr("CUST_CD_L")
        rcvDr("CUST_CD_M") = outkaediDr("CUST_CD_M")
        rcvDr("SYS_UPD_DATE") = inDr("RCV_SYS_UPD_DATE")
        rcvDr("SYS_UPD_TIME") = inDr("RCV_SYS_UPD_TIME")
        rcvDr("SYS_DEL_FLG") = "0"
        rcvDr("OUTKA_CTL_NO") = outkaediDr("OUTKA_CTL_NO")

        'データセットに設定
        ds.Tables("LMH030_EDI_RCV_HED").Rows.Add(rcvDr)

        Return ds

    End Function

#End Region

#Region "データセット設定(作業)"
    ''' <summary>
    ''' データセット設定(作業)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetSagyo(ByVal ds As DataSet) As DataSet

        Dim ediDrM As DataRow
        Dim sagyoDr As DataRow
        Dim ediDrL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim max As Integer = ds.Tables("LMH030_OUTKAEDI_M").Rows.Count - 1
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim sagyoCD As String = String.Empty
        Dim outkaNoLM As String = String.Empty
        Dim num As New NumberMasterUtility

        For i As Integer = 0 To max

            ediDrM = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)

            For j As Integer = 1 To 5

                sagyoCD = ediDrM("OUTKA_KAKO_SAGYO_KB_" & j).ToString()

                If String.IsNullOrEmpty(sagyoCD) = False Then

                    outkaNoLM = String.Concat(ediDrM("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))

                    sagyoDr = ds.Tables("LMH030_E_SAGYO").NewRow()

                    'sagyoDr("SAGYO_COMP") = "0"
                    'sagyoDr("SKYU_CHK") = "0"
                    sagyoDr("SAGYO_COMP") = "00"
                    sagyoDr("SKYU_CHK") = "00"
                    sagyoDr("SAGYO_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, nrsBrCd)
                    sagyoDr("SAGYO_SIJI_NO") = String.Empty
                    sagyoDr("INOUTKA_NO_LM") = outkaNoLM
                    sagyoDr("NRS_BR_CD") = ediDrL("NRS_BR_CD")
                    sagyoDr("WH_CD") = ediDrL("WH_CD")
                    sagyoDr("IOZS_KB") = "21"
                    sagyoDr("SAGYO_CD") = sagyoCD
                    sagyoDr("CUST_CD_L") = ediDrL("CUST_CD_L")
                    sagyoDr("CUST_CD_M") = ediDrL("CUST_CD_M")
                    sagyoDr("DEST_CD") = ediDrL("DEST_CD")
                    sagyoDr("DEST_NM") = ediDrL("DEST_NM")
                    sagyoDr("GOODS_CD_NRS") = ediDrM("NRS_GOODS_CD")
                    sagyoDr("GOODS_NM_NRS") = ediDrM("GOODS_NM")
                    sagyoDr("LOT_NO") = ediDrM("LOT_NO")
                    sagyoDr("SAGYO_NB") = 0
                    sagyoDr("SAGYO_GK") = 0
                    sagyoDr("REMARK_SKYU") = ediDrM("REMARK")
                    sagyoDr("SAGYO_COMP_CD") = String.Empty
                    sagyoDr("SAGYO_COMP_DATE") = String.Empty
                    sagyoDr("DEST_SAGYO_FLG") = "00"
                    sagyoDr("SYS_DEL_FLG") = "0"

                    'データセットに設定
                    ds.Tables("LMH030_E_SAGYO").Rows.Add(sagyoDr)
                End If
            Next

            For k As Integer = 1 To 2

                sagyoCD = ediDrM("SAGYO_KB_" & k).ToString()

                If String.IsNullOrEmpty(sagyoCD) = False Then

                    outkaNoLM = String.Concat(ediDrM("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))

                    sagyoDr = ds.Tables("LMH030_E_SAGYO").NewRow()

                    'sagyoDr("SAGYO_COMP") = "0"
                    'sagyoDr("SKYU_CHK") = "0"
                    sagyoDr("SAGYO_COMP") = "00"
                    sagyoDr("SKYU_CHK") = "00"
                    sagyoDr("SAGYO_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, nrsBrCd)
                    sagyoDr("SAGYO_SIJI_NO") = String.Empty
                    sagyoDr("INOUTKA_NO_LM") = outkaNoLM
                    sagyoDr("NRS_BR_CD") = ediDrL("NRS_BR_CD")
                    sagyoDr("WH_CD") = ediDrL("WH_CD")
                    sagyoDr("IOZS_KB") = "21"
                    sagyoDr("SAGYO_CD") = sagyoCD
                    sagyoDr("CUST_CD_L") = ediDrL("CUST_CD_L")
                    sagyoDr("CUST_CD_M") = ediDrL("CUST_CD_M")
                    sagyoDr("DEST_CD") = ediDrL("DEST_CD")
                    sagyoDr("DEST_NM") = ediDrL("DEST_NM")
                    sagyoDr("GOODS_CD_NRS") = ediDrM("NRS_GOODS_CD")
                    sagyoDr("GOODS_NM_NRS") = ediDrM("GOODS_NM")
                    sagyoDr("LOT_NO") = ediDrM("LOT_NO")
                    sagyoDr("SAGYO_NB") = 0
                    sagyoDr("SAGYO_GK") = 0
                    sagyoDr("REMARK_SKYU") = ediDrM("REMARK")
                    sagyoDr("SAGYO_COMP_CD") = String.Empty
                    sagyoDr("SAGYO_COMP_DATE") = String.Empty
                    sagyoDr("DEST_SAGYO_FLG") = "01"
                    sagyoDr("SYS_DEL_FLG") = "0"

                    'データセットに設定
                    ds.Tables("LMH030_E_SAGYO").Rows.Add(sagyoDr)
                End If
            Next
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
    Private Function SetDatasetUnsoL(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

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
	        '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 Start
	        'unsoDr("TYUKEI_HAISO_FLG") = String.Empty
	        unsoDr("TYUKEI_HAISO_FLG") = "00"   '中継配送フラグ"00:無し"を設定
            '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 End
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
    Private Function SetDatasetUnsoM(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim unsoMDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)

        Dim stdWtKgs As Decimal = 0
        Dim irime As Decimal = 0
        Dim stdIrimeNb As Decimal = 0
        Dim nisugata As Decimal = 0
        Dim outkaTtlNb As Decimal = 0

        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            unsoMDr = ds.Tables("LMH030_UNSO_M").NewRow()

            unsoMDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            unsoMDr("UNSO_NO_L") = unsoLDr("UNSO_NO_L")
            unsoMDr("UNSO_NO_M") = ediDr("OUTKA_CTL_NO_CHU")
            unsoMDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            unsoMDr("GOODS_NM") = ediDr("GOODS_NM")
            unsoMDr("UNSO_TTL_NB") = ediDr("OUTKA_PKG_NB")
            unsoMDr("NB_UT") = ediDr("KB_UT")
            unsoMDr("UNSO_TTL_QT") = ediDr("OUTKA_TTL_QT")
            unsoMDr("QT_UT") = ediDr("QT_UT")
            unsoMDr("HASU") = ediDr("OUTKA_HASU")
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

#Region "データセット設定(運送L：運送重量)"
    ''' <summary>
    ''' データセット設定(運送L：運送重量)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetdatasetUnsoJyuryo(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim unsoJyuryo As Decimal = 0
        Dim matomeUnsoJyuryo As Decimal = 0

        'まとめ(運送Mデータの運送重量合算)
        If matomeFlg = True Then

            'まとめ先の中データ取得
            ds = MyBase.CallDAC(Me._DacCom, "SelectUnsoMatomeTarget", ds)
            If MyBase.GetResultCount = 0 Then
                unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
                unsoLDr("UNSO_WT") = Math.Ceiling(unsoJyuryo)
                unsoLDr("NB_UT") = ediMDr("KB_UT")
                Return ds

            Else
                matomeUnsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_MATOME_UNSO_M")
                unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
                unsoLDr("UNSO_WT") = Math.Ceiling(matomeUnsoJyuryo + unsoJyuryo)

                Return ds

            End If

        Else
            unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
            unsoLDr("UNSO_WT") = Math.Ceiling(unsoJyuryo)
            unsoLDr("NB_UT") = ediMDr("KB_UT")

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 運送重量再計算処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCalcJyuryo(ByVal ds As DataSet, ByVal tblNm As String) As Decimal

        Dim unsoMDr As DataRow
        Dim unsoJyuryo As Decimal = 0
        Dim NB As Decimal = 0
        Dim max As Integer = ds.Tables(tblNm).Rows.Count - 1

        For i As Integer = 0 To max

            unsoMDr = ds.Tables(tblNm).Rows(i)

            '運送M個数の算出（梱数 * 入数 + 端数）
            NB = Convert.ToDecimal(unsoMDr("UNSO_TTL_NB")) * Convert.ToDecimal(unsoMDr("PKG_NB")) + Convert.ToDecimal(unsoMDr("HASU"))

            unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) * NB + unsoJyuryo

        Next

        Return unsoJyuryo

    End Function
#End Region

#Region "データセット設定(EDI出荷M：運送重量必要項目)"
    ''' <summary>
    ''' データセット設定(EDI出荷M：運送重量必要項目)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiMUnsoJyuryo(ByVal ds As DataSet, ByVal setDs As DataSet, ByVal count As Integer, _
                                              ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(count)
        Dim mGoodsDr As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)
        Dim drJudge As DataRow

        '標準重量
        ediMDr("STD_WT_KGS") = mGoodsDr("STD_WT_KGS")
        '標準入目
        ediMDr("STD_IRIME_NB") = mGoodsDr("STD_IRIME_NB")
        '風袋加算フラグ
        ediMDr("TARE_YN") = mGoodsDr("TARE_YN")

        '荷姿(区分マスタ)
        drJudge = ds.Tables("LMH030_JUDGE").Rows(0)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N001
        drJudge("KBN_CD") = ediMDr("PKG_UT")

        ds = MyBase.CallDAC(Me._DacCom, "SelectDataPkgUtZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"包装単位", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Dim zkbnDr As DataRow = ds.Tables("LMH030_Z_KBN").Rows(0)
        '風袋重量
        ediMDr("NISUGATA") = zkbnDr("NISUGATA")

        Return True

    End Function

#End Region

#End Region

#Region "まとめ先複数件の時出荷管理番号取得"

    ''' <summary>
    ''' まとめ先出荷管理番号の取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function matomesakiOutkaNo(ByVal ds As DataSet) As String

        Dim max As Integer = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows.Count - 1
        Dim concatOutkaNo As String = String.Empty
        Dim matomeOutkaNo As String = String.Empty

        For i As Integer = 0 To max

            'まとめ先出荷管理番号の取得
            matomeOutkaNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(i)("OUTKA_CTL_NO").ToString
            If i = 0 Then
                concatOutkaNo = matomeOutkaNo
            ElseIf i > 0 Then
                concatOutkaNo = String.Concat(concatOutkaNo, ",", matomeOutkaNo)
            End If

        Next

        Return concatOutkaNo

    End Function


#End Region

#Region "出荷登録処理(運賃作成)"

    ''' <summary>
    ''' 出荷登録処理(運賃作成)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnchinSakusei(ByVal ds As DataSet) As DataSet

        '運賃の新規作成
        ds = MyBase.CallDAC(Me._Dac, "InsertUnchinData", ds)

        Return ds

    End Function

#End Region

#Region "紐付け処理"
    ''' <summary>
    ''' 紐付け処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Himoduke(ByVal ds As DataSet) As DataSet

        '紐付けフラグの設定
        ds = Me.SetHimodukeFlg(ds)

        '受信DTLデータセット
        ds = Me.SetDatasetEdiRcvDtl(ds)

        '受信HEDデータセット
        ds = Me.SetDatasetEdiRcvHed(ds)

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        Return ds
    End Function

    ''' <summary>
    ''' 紐付けフラグの設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetHimodukeFlg(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        dr.Item("MATCHING_FLAG") = "01"

        Return ds

    End Function

#End Region

#End Region

#Region "Method(セミEDI)"

#Region "データセット設定"

#Region "セミEDI時　データセット設定(EDI受信HED・DTL)"

    ''' <summary>
    ''' データセット設定(EDI受信HED・DTL)：セミEDI
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiOutkaEdiRcv(ByVal ds As DataSet, ByVal i As Integer) As DataSet

        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0)

        Dim drEdiRcvHed As DataRow = ds.Tables("LMH030_INOUTKAEDI_HED_SMK").NewRow()
        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH030_INOUTKAEDI_DTL_SMK").NewRow()
        Dim drSetDtl As DataRow = ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i)

        'EDI受信HED設定
        drEdiRcvHed("CRT_DATE") = MyBase.GetSystemDate()                                            'データ受信日
        drEdiRcvHed("FILE_NAME") = drSetDtl("FILE_NAME_OPE")                                        '受信ファイル名
        drEdiRcvHed("REC_NO") = Convert.ToInt32(drSetDtl("REC_NO")).ToString("00000")               '受信ファイル行数

        Select Case Me._Blc.LeftB(drSetDtl("COLUMN_6").ToString().Trim(), 3)

            Case LMH030BLC502.UKEHARAI_GNM
                drEdiRcvHed("NRS_BR_CD") = LMH030BLC502.NRS_CD_SMK_GNM                              '営業所コード(群馬)
                drEdiRcvHed("CUST_CD_L") = LMH030BLC502.CUST_CD_L_GNM                               '荷主コード（大）(群馬)
                drEdiRcvHed("CUST_CD_M") = LMH030BLC502.CUST_CD_M_GNM                               '荷主コード（中）(群馬)
                drEdiRcvHed("WH_CD") = LMH030BLC502.WH_CD_SMK_GNM                                   '倉庫コード(群馬)

            Case LMH030BLC502.UKEHARAI_CBA
                drEdiRcvHed("NRS_BR_CD") = LMH030BLC502.NRS_CD_SMK_CBA                              '営業所コード(千葉)
                drEdiRcvHed("CUST_CD_L") = LMH030BLC502.CUST_CD_L_CBA                               '荷主コード（大）(千葉)
                drEdiRcvHed("CUST_CD_M") = LMH030BLC502.CUST_CD_M_CBA                               '荷主コード（中）(千葉)
                drEdiRcvHed("WH_CD") = LMH030BLC502.WH_CD_SMK_CBA                                   '倉庫コード(千葉)

            Case Else

        End Select

        Select Case Me._Blc.LeftB(drSetDtl("COLUMN_8").ToString().Trim(), 1)

            '入荷データ
            Case "0", "2", "3", "4"
                drEdiRcvHed("INOUT_KB") = "1"                              '入出荷区分(入荷)
                Select Case Me._Blc.LeftB(drSetDtl("COLUMN_6").ToString().Trim(), 3)

                    Case LMH030BLC502.UKEHARAI_CBA
                        drEdiRcvHed("NRS_BR_CD") = LMH030BLC502.NRS_CD_SMK_MSD                              '営業所コード(千葉:南袖)
                        drEdiRcvHed("CUST_CD_L") = LMH030BLC502.CUST_CD_L_MSD                               '荷主コード（大）(千葉:南袖)
                        drEdiRcvHed("CUST_CD_M") = LMH030BLC502.CUST_CD_M_MSD                               '荷主コード（中）(千葉:南袖)
                        drEdiRcvHed("WH_CD") = LMH030BLC502.WH_CD_SMK_MSD                                   '倉庫コード(千葉：南袖)
                        drEdiRcvHed("INOUT_KB") = "0"                                                       '入出荷区分(出荷に切替)

                    Case Else

                End Select

                Select Case Me._Blc.LeftB(drSetDtl("COLUMN_8").ToString().Trim(), 3)

                    Case "310"
                        '入荷登録対象データ
                        drEdiRcvHed("INOUT_ENTRY_KB") = "1"                 '入出荷登録対象区分
                        drEdiRcvDtl("INOUT_ENTRY_KB") = "1"                 '入出荷登録対象区分
                    Case Else
                        '入荷登録対象外データ
                        drEdiRcvHed("INOUT_ENTRY_KB") = "0"                 '入出荷登録対象区分
                        drEdiRcvDtl("INOUT_ENTRY_KB") = "0"                 '入出荷登録対象区分

                End Select

            Case Else

                drEdiRcvHed("INOUT_KB") = "0"                              '入出荷区分(出荷)
                Select Case Me._Blc.LeftB(drSetDtl("COLUMN_8").ToString().Trim(), 3)

                    Case "500" To "599", "810"
                        '出荷登録対象データ
                        drEdiRcvHed("INOUT_ENTRY_KB") = "1"                 '入出荷登録対象区分
                        drEdiRcvDtl("INOUT_ENTRY_KB") = "1"                 '入出荷登録対象区分
                    Case Else
                        '出荷登録対象外データ
                        drEdiRcvHed("INOUT_ENTRY_KB") = "0"                 '入出荷登録対象区分
                        drEdiRcvDtl("INOUT_ENTRY_KB") = "0"                 '入出荷登録対象区分
                End Select

        End Select

        If drEdiRcvHed("INOUT_ENTRY_KB").ToString().Equals("1") = True Then
            drEdiRcvHed("DEL_KB") = "0"                                                                 '削除区分
        Else
            drEdiRcvHed("DEL_KB") = "3"                                                                 '削除区分
        End If

        drEdiRcvHed("EDI_CTL_NO") = String.Empty                                                                'ＥＤＩ管理番号
        drEdiRcvHed("INKA_CTL_NO_L") = DEF_CTL_NO                                                               '入荷管理番号
        drEdiRcvHed("OUTKA_CTL_NO") = DEF_CTL_NO                                                                '出荷管理番号
        drEdiRcvHed("PRTFLG") = "0"                                                                             'プリントフラグ
        drEdiRcvHed("CANCEL_FLG") = String.Empty                                                                'キャンセルフラグ
        drEdiRcvHed("NRS_NO") = "000000"                                                                        '日陸連番

        drEdiRcvHed("COMPANY_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_1").ToString().Trim(), 2)                    '会社コード
        drEdiRcvHed("RB_KB") = Me._Blc.LeftB(drSetDtl("COLUMN_2").ToString().Trim(), 1)                         '赤黒区分
        drEdiRcvHed("DENP_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_3").ToString().Trim(), 7)                       '伝票番号
        drEdiRcvHed("DENP_NO_EDA") = Me._Blc.LeftB(Mid(drSetDtl("COLUMN_3").ToString().Trim(), 8, 1), 1)        '伝票番号枝番
        drEdiRcvHed("SHORI_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_4").ToString().Trim(), 8)                    '処理日
        drEdiRcvHed("OUTKA_PLAN_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_5").ToString().Trim(), 8)               '出荷日
        drEdiRcvHed("UKE_CHUKEI") = Me._Blc.LeftB(drSetDtl("COLUMN_6").ToString().Trim(), 3)                    '受払場所（中継地）
        drEdiRcvHed("UKE_AITE") = Me._Blc.LeftB(drSetDtl("COLUMN_7").ToString().Trim(), 3)                      '受払場所（相手）
        drEdiRcvHed("UKE_SHUBETSU") = Me._Blc.LeftB(drSetDtl("COLUMN_8").ToString().Trim(), 3)                  '受払種別
        drEdiRcvHed("ARR_PLAN_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_20").ToString().Trim(), 8)                '納期
        drEdiRcvHed("DEST_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_25").ToString().Trim(), 6)                      '荷受主コード
        drEdiRcvHed("DEST_ZIP") = Me._Blc.LeftB(drSetDtl("COLUMN_26").ToString().Trim(), 8)                     '荷受主郵便番号
        drEdiRcvHed("DEST_TEL") = Me._Blc.LeftB(drSetDtl("COLUMN_27").ToString().Trim(), 18)                    '荷受主電話番号
        drEdiRcvHed("DEST_MSG") = Me._Blc.LeftB(drSetDtl("COLUMN_28").ToString().Trim(), 40)                    '荷受主宛メッセージ
        drEdiRcvHed("DEST_NM") = Me._Blc.LeftB(Replace(Replace(drSetDtl("COLUMN_36").ToString().Trim(), "　　", " "), " ", "　"), 80)                     '荷受主取引先名
        drEdiRcvHed("DEST_AD_1") = Me._Blc.LeftB(drSetDtl("COLUMN_37").ToString().Trim(), 30)                   '荷受主住所１
        drEdiRcvHed("DEST_AD_2") = Me._Blc.LeftB(drSetDtl("COLUMN_38").ToString().Trim(), 30)                   '荷受主住所２
        drEdiRcvHed("DEST_NM_KANA") = Me._Blc.LeftB(drSetDtl("COLUMN_40").ToString().Trim(), 25)                '荷受主取引先名カナ
        drEdiRcvHed("DEST_AD_KANA1") = Me._Blc.LeftB(drSetDtl("COLUMN_41").ToString().Trim(), 20)               '荷受主住所１カナ
        drEdiRcvHed("DEST_AD_KANA2") = Me._Blc.LeftB(drSetDtl("COLUMN_42").ToString().Trim(), 20)               '荷受主住所２カナ
        If drEdiRcvHed("DEST_NM").ToString().Length > 30 Then
            drEdiRcvHed("DEST_NM_EDI") = String.Concat(Me._Blc.LeftB(drEdiRcvHed("DEST_NM").ToString().Trim(), 58), "*")                             '荷受主名（ＥＤＩ）
        Else
            drEdiRcvHed("DEST_NM_EDI") = drEdiRcvHed("DEST_NM").ToString()
        End If

        'EDI受信DTL設定
        If drEdiRcvDtl("INOUT_ENTRY_KB").ToString().Equals("1") = True Then
            drEdiRcvDtl("DEL_KB") = "0"                                                                 '削除区分
        Else
            drEdiRcvDtl("DEL_KB") = "3"                                                                 '削除区分
        End If
        drEdiRcvDtl("CRT_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl("FILE_NAME") = drSetDtl("FILE_NAME_OPE")
        drEdiRcvDtl("REC_NO") = Convert.ToInt32(drSetDtl("REC_NO")).ToString("00000")
        drEdiRcvDtl("GYO") = String.Empty
        drEdiRcvDtl("NRS_BR_CD") = drEdiRcvHed("NRS_BR_CD")
        drEdiRcvDtl("EDI_CTL_NO") = String.Empty
        drEdiRcvDtl("EDI_CTL_NO_CHU") = String.Empty
        drEdiRcvDtl("OUTKA_CTL_NO") = DEF_CTL_NO
        drEdiRcvDtl("OUTKA_CTL_NO_CHU") = "000"
        drEdiRcvDtl("INKA_CTL_NO_L") = DEF_CTL_NO
        drEdiRcvDtl("INKA_CTL_NO_M") = "000"
        drEdiRcvDtl("CUST_CD_L") = drEdiRcvHed("CUST_CD_L")                                                        '荷主コード（大）
        drEdiRcvDtl("CUST_CD_M") = drEdiRcvHed("CUST_CD_M")                                                        '荷主コード（中）
        drEdiRcvDtl("NRS_NO") = drEdiRcvHed("NRS_NO")                                                              '日陸連番
        drEdiRcvDtl("NRS_GYO_NO") = "000"                                                                          '日陸連番行番号
        drEdiRcvDtl("INOUT_KB") = drEdiRcvHed("INOUT_KB")

        drEdiRcvDtl("RB_KB") = Me._Blc.LeftB(drSetDtl("COLUMN_2").ToString().Trim(), 1)                            '赤黒区分
        drEdiRcvDtl("DENP_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_3").ToString().Trim(), 7)                          '伝票番号
        drEdiRcvDtl("DENP_NO_EDA") = Me._Blc.LeftB(Mid(drSetDtl("COLUMN_3").ToString().Trim(), 8, 1), 1)           '伝票番号枝番
        drEdiRcvDtl("DENP_NO_REN") = Me._Blc.LeftB(Mid(drSetDtl("COLUMN_3").ToString().Trim(), 9, 2), 2)           '伝票番号連番
        drEdiRcvDtl("DENP_NO_GYO") = Me._Blc.LeftB(Mid(drSetDtl("COLUMN_3").ToString().Trim(), 11, 1).Trim(), 1)   '伝票番号行
        drEdiRcvDtl("GOODS_CLASS") = Me._Blc.LeftB(drSetDtl("COLUMN_9").ToString().Trim(), 4)                      '品種
        drEdiRcvDtl("GOODS_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_10").ToString().Trim(), 6)                        '商品コード
        drEdiRcvDtl("GOODS_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_11").ToString().Trim(), 26)                       '商品名
        drEdiRcvDtl("NISUGATA") = Me._Blc.LeftB(drSetDtl("COLUMN_12").ToString().Trim(), 2)                        '荷姿
        drEdiRcvDtl("IRIME") = Me._Blc.LeftB(drSetDtl("COLUMN_13").ToString().Trim(), 9)                           '容量
        If String.IsNullOrEmpty(drEdiRcvDtl("IRIME").ToString()) = True Then
            drEdiRcvDtl("IRIME") = 0
        Else
            '少数点を含めて送ってくるようになったので÷1000はしない
            'drEdiRcvDtl("IRIME") = Convert.ToDouble(drEdiRcvDtl("IRIME"))
            drEdiRcvDtl("IRIME") = Convert.ToDouble(drEdiRcvDtl("IRIME"))
        End If
        drEdiRcvDtl("TANA_KB") = Me._Blc.LeftB(drSetDtl("COLUMN_14").ToString().Trim(), 2)                         '棚卸区分
        drEdiRcvDtl("ZAIKO_KB") = Me._Blc.LeftB(drSetDtl("COLUMN_15").ToString().Trim(), 1)                        '在庫区分
        drEdiRcvDtl("LOT_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_16").ToString().Trim(), 15)                         'ロット番号
        drEdiRcvDtl("SEISAN_KOJO") = Me._Blc.LeftB(drSetDtl("COLUMN_17").ToString().Trim(), 3)                     '生産工場
        drEdiRcvDtl("KOSU") = Me._Blc.LeftB(drSetDtl("COLUMN_18").ToString().Trim(), 8)                            '個数
        If String.IsNullOrEmpty(drEdiRcvDtl("KOSU").ToString()) = True Then
            drEdiRcvDtl("KOSU") = 0
        Else
            drEdiRcvDtl("KOSU") = Convert.ToInt64(drEdiRcvDtl("KOSU"))
        End If

        drEdiRcvDtl("SURYO") = Me._Blc.LeftB(drSetDtl("COLUMN_19").ToString().Trim(), 13)                          '受払数量
        If String.IsNullOrEmpty(drEdiRcvDtl("SURYO").ToString()) = True Then
            drEdiRcvDtl("SURYO") = 0
        Else
            '少数点を含めて送ってくるようになったので÷1000はしない
            'drEdiRcvDtl("SURYO") = Convert.ToDouble(drEdiRcvDtl("SURYO")) / 1000
            drEdiRcvDtl("SURYO") = Convert.ToDouble(drEdiRcvDtl("SURYO"))
        End If
        drEdiRcvDtl("YOUKI_JOKEN") = Me._Blc.LeftB(drSetDtl("COLUMN_21").ToString().Trim(), 2)                     '容器条件
        drEdiRcvDtl("NIWATASHI_JOKEN") = Me._Blc.LeftB(drSetDtl("COLUMN_22").ToString().Trim(), 2)                 '荷渡条件
        drEdiRcvDtl("SHIKEN_HYO") = Me._Blc.LeftB(drSetDtl("COLUMN_23").ToString().Trim(), 1)                      '試験表有無
        drEdiRcvDtl("SHITEI_DENPYO") = Me._Blc.LeftB(drSetDtl("COLUMN_24").ToString().Trim(), 1)                   '指定帳表
        drEdiRcvDtl("BUYER_ORD_NO_DTL") = Me._Blc.LeftB(drSetDtl("COLUMN_29").ToString().Trim(), 20)               '相手先オーダー番号
        drEdiRcvDtl("BUYER_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_30").ToString().Trim(), 6)                        '注文主コード
        drEdiRcvDtl("BUYER_TEL") = Me._Blc.LeftB(drSetDtl("COLUMN_31").ToString().Trim(), 18)                      '注文主電話番号
        drEdiRcvDtl("KOJO_MSG") = Me._Blc.LeftB(drSetDtl("COLUMN_32").ToString().Trim(), 40)                       '工場宛てメッセージ
        drEdiRcvDtl("HANBAI_KA") = Me._Blc.LeftB(drSetDtl("COLUMN_33").ToString().Trim(), 5)                       '販売担当課
        drEdiRcvDtl("HANBAI_TANTO") = Me._Blc.LeftB(drSetDtl("COLUMN_34").ToString().Trim(), 4)                    '販売担当者
        drEdiRcvDtl("ORDER_KB") = Me._Blc.LeftB(drSetDtl("COLUMN_35").ToString().Trim(), 1)                        'オーダー区分
        drEdiRcvDtl("BUYER_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_39").ToString().Trim(), 80)                       '注文主取引先名
        drEdiRcvDtl("SAKUSEI_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_43").ToString().Trim(), 8)                    '作成日
        drEdiRcvDtl("SAKUSEI_TIME") = Me._Blc.LeftB(drSetDtl("COLUMN_44").ToString().Trim(), 8)                    '作成時間
        drEdiRcvDtl("RACK_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_45").ToString().Trim(), 3)                         'ラック番号
        drEdiRcvDtl("CUST_ORD_NO_DTL") = Me._Blc.LeftB(drSetDtl("COLUMN_46").ToString().Trim(), 8)                 '荷主注文番号
        drEdiRcvDtl("RIYUU") = Me._Blc.LeftB(drSetDtl("COLUMN_47").ToString().Trim(), 2)                           '理由
        drEdiRcvDtl("DATA_KB") = Me._Blc.LeftB(drSetDtl("COLUMN_48").ToString().Trim(), 5)                         'データ区分
        drEdiRcvDtl("JISSEKI_SHORI_FLG") = drEdiRcvHed("INOUT_ENTRY_KB")                                           '実績処理フラグ

        'データセットに設定
        ds.Tables("LMH030_INOUTKAEDI_HED_SMK").Rows.Add(drEdiRcvHed)
        ds.Tables("LMH030_INOUTKAEDI_DTL_SMK").Rows.Add(drEdiRcvDtl)

        Return ds

    End Function

#End Region

#Region "セミEDI時　データセット設定(EDI出荷(中))"

    ''' <summary>
    ''' データセット設定(EDI出荷(中)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiOutkaEdiM(ByVal setDs As DataSet _
                                    , ByVal sSerchKey As String _
                                    , ByVal sNrsGoodsCd As String _
                                    , ByVal sNrsGoodsNm As String _
                                    , ByVal sIrime As String _
                                    ) As DataSet

        Dim drOutkaEdiM As DataRow = setDs.Tables("LMH030_OUTKAEDI_M").NewRow()
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_INOUTKAEDI_DTL_SMK").Rows(0)
        Dim drRcvEdiHed As DataRow = setDs.Tables("LMH030_INOUTKAEDI_HED_SMK").Rows(0)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)

        drOutkaEdiM("DEL_KB") = drRcvEdiDtl("DEL_KB")
        drOutkaEdiM("NRS_BR_CD") = drRcvEdiHed("NRS_BR_CD")
        drOutkaEdiM("EDI_CTL_NO") = drRcvEdiHed("EDI_CTL_NO")
        drOutkaEdiM("EDI_CTL_NO_CHU") = drRcvEdiDtl("EDI_CTL_NO_CHU")
        drOutkaEdiM("OUTKA_CTL_NO") = String.Empty
        drOutkaEdiM("OUTKA_CTL_NO_CHU") = String.Empty
        If drRcvEdiDtl("SHIKEN_HYO").Equals("0") = True Then
            drOutkaEdiM("COA_YN") = "0"
        Else
            drOutkaEdiM("COA_YN") = "1"
        End If

        drOutkaEdiM("CUST_ORD_NO_DTL") = drRcvEdiDtl("CUST_ORD_NO_DTL").ToString().Trim()
        drOutkaEdiM("BUYER_ORD_NO_DTL") = drRcvEdiDtl("BUYER_ORD_NO_DTL").ToString().Trim()
        drOutkaEdiM("CUST_GOODS_CD") = drRcvEdiDtl("GOODS_CD")
        drOutkaEdiM("NRS_GOODS_CD") = sNrsGoodsCd
        '商品名は受信した商品名を使用
        'drOutkaEdiM("GOODS_NM") = sNrsGoodsNm
        drOutkaEdiM("GOODS_NM") = drRcvEdiDtl("GOODS_NM")
        drOutkaEdiM("RSV_NO") = String.Empty
        drOutkaEdiM("LOT_NO") = drRcvEdiDtl("LOT_NO").ToString().Trim()
        drOutkaEdiM("SERIAL_NO") = String.Empty
        drOutkaEdiM("ALCTD_KB") = "01"
        drOutkaEdiM("OUTKA_PKG_NB") = 0
        '出荷端数
        '出荷数量
        '出荷総個数
        '出荷総数量
        drOutkaEdiM("OUTKA_HASU") = Convert.ToDouble(drRcvEdiDtl("KOSU").ToString)
        drOutkaEdiM("OUTKA_QT") = 0
        drOutkaEdiM("OUTKA_TTL_NB") = Convert.ToDouble(drRcvEdiDtl("KOSU").ToString)
        drOutkaEdiM("OUTKA_TTL_QT") = 0
        drOutkaEdiM("KB_UT") = String.Empty
        drOutkaEdiM("QT_UT") = String.Empty
        drOutkaEdiM("PKG_NB") = 0
        drOutkaEdiM("PKG_UT") = String.Empty
        drOutkaEdiM("ONDO_KB") = String.Empty
        drOutkaEdiM("UNSO_ONDO_KB") = String.Empty
        Dim dIrime As Double = Convert.ToDouble(sIrime)
        If dIrime = 0 Then
            drOutkaEdiM("IRIME") = sIrime
        Else
            drOutkaEdiM("IRIME") = Convert.ToDouble(drRcvEdiDtl("IRIME"))
        End If

        drOutkaEdiM("IRIME_UT") = String.Empty
        drOutkaEdiM("BETU_WT") = 0
        drOutkaEdiM("REMARK") = String.Empty
        drOutkaEdiM("OUT_KB") = "0"
        If drRcvEdiDtl("RB_KB").ToString().Equals("2") = True Then
            drOutkaEdiM("AKAKURO_KB") = "1"
        Else
            drOutkaEdiM("AKAKURO_KB") = "0"
        End If
        'drOutkaEdiM("AKAKURO_KB") = drRcvEdiHed("CANCEL_FLG")
        drOutkaEdiM("JISSEKI_FLAG") = "0"
        drOutkaEdiM("JISSEKI_USER") = String.Empty
        drOutkaEdiM("JISSEKI_DATE") = String.Empty
        drOutkaEdiM("JISSEKI_TIME") = String.Empty
        drOutkaEdiM("SET_KB") = String.Empty
        drOutkaEdiM("FREE_N01") = drRcvEdiDtl("SURYO")
        drOutkaEdiM("FREE_N02") = 0
        drOutkaEdiM("FREE_N03") = 0
        drOutkaEdiM("FREE_N04") = 0
        drOutkaEdiM("FREE_N05") = 0
        drOutkaEdiM("FREE_N06") = 0
        drOutkaEdiM("FREE_N07") = 0
        drOutkaEdiM("FREE_N08") = 0
        drOutkaEdiM("FREE_N09") = 0
        drOutkaEdiM("FREE_N10") = 0
        drOutkaEdiM("FREE_C01") = drRcvEdiDtl("EDI_CTL_NO").ToString()
        drOutkaEdiM("FREE_C02") = drRcvEdiDtl("EDI_CTL_NO_CHU").ToString()
        drOutkaEdiM("FREE_C03") = String.Concat(drRcvEdiDtl("DENP_NO").ToString(), drRcvEdiDtl("DENP_NO_EDA").ToString())
        drOutkaEdiM("FREE_C04") = String.Concat(drRcvEdiDtl("DENP_NO_REN").ToString(), drRcvEdiDtl("DENP_NO_GYO").ToString())
        drOutkaEdiM("FREE_C05") = drRcvEdiDtl("GOODS_CLASS").ToString()
        drOutkaEdiM("FREE_C06") = drRcvEdiDtl("NISUGATA").ToString()
        drOutkaEdiM("FREE_C07") = drRcvEdiDtl("TANA_KB").ToString()
        drOutkaEdiM("FREE_C08") = drRcvEdiDtl("ZAIKO_KB").ToString()
        drOutkaEdiM("FREE_C09") = drRcvEdiDtl("SEISAN_KOJO").ToString()
        drOutkaEdiM("FREE_C10") = drRcvEdiDtl("YOUKI_JOKEN").ToString()
        drOutkaEdiM("FREE_C11") = drRcvEdiDtl("NIWATASHI_JOKEN").ToString()
        drOutkaEdiM("FREE_C12") = drRcvEdiDtl("SHIKEN_HYO").ToString()
        drOutkaEdiM("FREE_C13") = drRcvEdiDtl("SHITEI_DENPYO").ToString()
        drOutkaEdiM("FREE_C14") = drRcvEdiDtl("BUYER_CD").ToString()
        drOutkaEdiM("FREE_C15") = drRcvEdiDtl("BUYER_TEL").ToString()
        drOutkaEdiM("FREE_C16") = drRcvEdiDtl("KOJO_MSG").ToString()
        drOutkaEdiM("FREE_C17") = drRcvEdiDtl("HANBAI_KA").ToString()
        drOutkaEdiM("FREE_C18") = drRcvEdiDtl("HANBAI_TANTO").ToString()
        drOutkaEdiM("FREE_C19") = drRcvEdiDtl("ORDER_KB").ToString()
        drOutkaEdiM("FREE_C20") = drRcvEdiDtl("BUYER_NM").ToString()
        drOutkaEdiM("FREE_C21") = drRcvEdiDtl("SEARCH_KEY_1").ToString()
        drOutkaEdiM("FREE_C22") = String.Empty
        drOutkaEdiM("FREE_C23") = String.Empty
        drOutkaEdiM("FREE_C24") = String.Empty
        drOutkaEdiM("FREE_C25") = String.Empty
        drOutkaEdiM("FREE_C26") = String.Empty
        drOutkaEdiM("FREE_C27") = String.Empty
        drOutkaEdiM("FREE_C28") = String.Empty
        drOutkaEdiM("FREE_C29") = String.Empty
        drOutkaEdiM("FREE_C30") = String.Empty

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_M").Rows.Add(drOutkaEdiM)

        Return setDs

    End Function

#End Region

#Region "セミEDI時　データセット設定(EDI出荷(大))"

    ''' <summary>
    ''' データセット設定(EDI出荷(大)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiOutkaEdiL(ByVal setDs As DataSet) As DataSet

        Dim drOutkaEdiL As DataRow = setDs.Tables("LMH030_OUTKAEDI_L").NewRow()
        Dim drRcvEdiHed As DataRow = setDs.Tables("LMH030_INOUTKAEDI_HED_SMK").Rows(0)
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_INOUTKAEDI_DTL_SMK").Rows(0)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        '荷主Index
        Dim ediCustIndex As String = drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString()

        drOutkaEdiL("DEL_KB") = drRcvEdiHed("DEL_KB")
        drOutkaEdiL("NRS_BR_CD") = drRcvEdiHed("NRS_BR_CD")
        drOutkaEdiL("EDI_CTL_NO") = drRcvEdiHed("EDI_CTL_NO")
        drOutkaEdiL("OUTKA_CTL_NO") = String.Empty
        drOutkaEdiL("OUTKA_KB") = "10"
        drOutkaEdiL("SYUBETU_KB") = "10"
        drOutkaEdiL("NAIGAI_KB") = String.Empty
        drOutkaEdiL("OUTKA_STATE_KB") = "10"
        drOutkaEdiL("OUTKAHOKOKU_YN") = String.Empty
        drOutkaEdiL("PICK_KB") = "01"
        drOutkaEdiL("NRS_BR_NM") = String.Empty
        drOutkaEdiL("WH_CD") = drRcvEdiHed("WH_CD")
        drOutkaEdiL("WH_NM") = String.Empty
        '出荷予定日
        drOutkaEdiL("OUTKA_PLAN_DATE") = drRcvEdiHed("OUTKA_PLAN_DATE")
        '出庫日
        drOutkaEdiL("OUTKO_DATE") = drRcvEdiHed("OUTKA_PLAN_DATE")
        '納入予定日
        drOutkaEdiL("ARR_PLAN_DATE") = drRcvEdiHed("ARR_PLAN_DATE")
        '納入予定時刻
        Dim sRemark As String = drRcvEdiDtl("KOJO_MSG").ToString
        drOutkaEdiL("ARR_PLAN_TIME") = String.Empty
        If InStr(sRemark, "着") > 0 Then
            If InStr(sRemark, "AM") > 0 OrElse _
               InStr(sRemark, "ＡＭ") > 0 OrElse _
               InStr(sRemark, "午前") > 0 Then
                '午前必着：02をセット
                drOutkaEdiL("ARR_PLAN_TIME") = "02"
            End If
        End If

        drOutkaEdiL("HOKOKU_DATE") = String.Empty

        '当期保管料負担有無
        drOutkaEdiL("TOUKI_HOKAN_YN") = String.Empty
        drOutkaEdiL("CUST_CD_L") = drRcvEdiHed("CUST_CD_L")
        drOutkaEdiL("CUST_CD_M") = drRcvEdiHed("CUST_CD_M")
        drOutkaEdiL("CUST_NM_L") = String.Empty
        drOutkaEdiL("CUST_NM_M") = String.Empty
        drOutkaEdiL("SHIP_CD_L") = String.Empty
        drOutkaEdiL("SHIP_CD_M") = String.Empty
        drOutkaEdiL("SHIP_NM_L") = String.Empty
        drOutkaEdiL("SHIP_NM_M") = String.Empty
        drOutkaEdiL("EDI_DEST_CD") = drRcvEdiHed("DEST_CD").ToString().Trim()
        drOutkaEdiL("DEST_CD") = drRcvEdiHed("DEST_CD").ToString().Trim()
        drOutkaEdiL("DEST_NM") = drRcvEdiHed("DEST_NM_EDI").ToString().Trim()
        drOutkaEdiL("DEST_ZIP") = drRcvEdiHed("DEST_ZIP").ToString().Trim()
        drOutkaEdiL("DEST_AD_1") = drRcvEdiHed("DEST_AD_1").ToString().Trim()
        drOutkaEdiL("DEST_AD_2") = drRcvEdiHed("DEST_AD_2").ToString().Trim()
        drOutkaEdiL("DEST_AD_3") = String.Empty
        drOutkaEdiL("DEST_AD_4") = String.Empty
        drOutkaEdiL("DEST_AD_5") = String.Empty
        drOutkaEdiL("DEST_TEL") = drRcvEdiHed("DEST_TEL").ToString().Trim()
        drOutkaEdiL("DEST_FAX") = String.Empty
        drOutkaEdiL("DEST_MAIL") = String.Empty
        drOutkaEdiL("DEST_JIS_CD") = String.Empty
        drOutkaEdiL("SP_NHS_KB") = String.Empty
        drOutkaEdiL("COA_YN") = String.Empty
        drOutkaEdiL("CUST_ORD_NO") = String.Concat(drRcvEdiHed("DENP_NO").ToString().Trim(), "-", drRcvEdiHed("DENP_NO_EDA").ToString().Trim())
        drOutkaEdiL("BUYER_ORD_NO") = drRcvEdiDtl("BUYER_ORD_NO_DTL").ToString().Trim()
        If drRcvEdiHed("CUST_CD_L").ToString().Equals(LMH030BLC502.CUST_CD_L_MSD) = True Then
            drOutkaEdiL("UNSO_MOTO_KB") = "90"
            drOutkaEdiL("UNSO_TEHAI_KB") = "30"
            drOutkaEdiL("BIN_KB") = String.Empty
            drOutkaEdiL("UNSO_ATT") = String.Empty
            drOutkaEdiL("UNCHIN_YN") = "0"
        Else
            drOutkaEdiL("UNSO_MOTO_KB") = "10"
            drOutkaEdiL("UNSO_TEHAI_KB") = "10"
            drOutkaEdiL("BIN_KB") = "01"
            drOutkaEdiL("UNSO_ATT") = drRcvEdiDtl("KOJO_MSG").ToString().Trim()
            drOutkaEdiL("UNCHIN_YN") = "1"
        End If

        drOutkaEdiL("SYARYO_KB") = String.Empty
        drOutkaEdiL("UNSO_CD") = String.Empty
        drOutkaEdiL("UNSO_NM") = String.Empty
        drOutkaEdiL("UNSO_BR_CD") = String.Empty
        drOutkaEdiL("UNSO_BR_NM") = String.Empty
        drOutkaEdiL("UNCHIN_TARIFF_CD") = String.Empty
        drOutkaEdiL("EXTC_TARIFF_CD") = String.Empty

        ''注意事項
        drOutkaEdiL("REMARK") = drRcvEdiHed("DEST_MSG").ToString().Trim()
        drOutkaEdiL("DENP_YN") = "1"
        drOutkaEdiL("PC_KB") = String.Empty
        drOutkaEdiL("NIYAKU_YN") = String.Empty

        If drRcvEdiHed("INOUT_ENTRY_KB").ToString().Equals("1") = True Then
            drOutkaEdiL("OUT_FLAG") = "0"
            drOutkaEdiL("JISSEKI_FLAG") = "0"
        Else
            drOutkaEdiL("OUT_FLAG") = "9"
            drOutkaEdiL("JISSEKI_FLAG") = "9"
        End If

        If drRcvEdiHed("RB_KB").ToString().Equals("2") = True Then
            drOutkaEdiL("AKAKURO_KB") = "1"
        Else
            drOutkaEdiL("AKAKURO_KB") = "0"
        End If
        'drOutkaEdiL("AKAKURO_KB") = drRcvEdiHed("CANCEL_FLG")
        drOutkaEdiL("JISSEKI_USER") = String.Empty
        drOutkaEdiL("JISSEKI_DATE") = String.Empty
        drOutkaEdiL("JISSEKI_TIME") = String.Empty
        drOutkaEdiL("FREE_N01") = 0
        drOutkaEdiL("FREE_N02") = 0
        drOutkaEdiL("FREE_N03") = 0
        drOutkaEdiL("FREE_N04") = 0
        drOutkaEdiL("FREE_N05") = 0
        drOutkaEdiL("FREE_N06") = 0
        drOutkaEdiL("FREE_N07") = 0
        drOutkaEdiL("FREE_N08") = 0
        drOutkaEdiL("FREE_N09") = 0
        drOutkaEdiL("FREE_N10") = 0

        drOutkaEdiL("FREE_C01") = drRcvEdiHed("EDI_CTL_NO").ToString().Trim()
        drOutkaEdiL("FREE_C02") = drRcvEdiHed("DENP_NO").ToString().Trim()
        drOutkaEdiL("FREE_C03") = drRcvEdiHed("UKE_CHUKEI").ToString().Trim()
        drOutkaEdiL("FREE_C04") = drRcvEdiHed("UKE_AITE").ToString().Trim()
        drOutkaEdiL("FREE_C05") = drRcvEdiHed("UKE_SHUBETSU").ToString().Trim()
        drOutkaEdiL("FREE_C06") = drRcvEdiHed("DEST_NM_KANA").ToString().Trim()
        drOutkaEdiL("FREE_C07") = drRcvEdiHed("DEST_AD_KANA1").ToString().Trim()
        drOutkaEdiL("FREE_C08") = drRcvEdiHed("DEST_AD_KANA2").ToString().Trim()
        drOutkaEdiL("FREE_C09") = String.Empty
        drOutkaEdiL("FREE_C10") = String.Empty
        drOutkaEdiL("FREE_C11") = String.Empty
        drOutkaEdiL("FREE_C12") = String.Empty
        drOutkaEdiL("FREE_C13") = String.Empty
        drOutkaEdiL("FREE_C14") = String.Empty
        drOutkaEdiL("FREE_C15") = String.Empty
        drOutkaEdiL("FREE_C16") = String.Empty
        drOutkaEdiL("FREE_C17") = String.Empty
        drOutkaEdiL("FREE_C18") = String.Empty
        drOutkaEdiL("FREE_C19") = String.Empty
        drOutkaEdiL("FREE_C20") = String.Concat(drRcvEdiHed("CUST_CD_L").ToString(), drRcvEdiHed("CUST_CD_M").ToString())
        drOutkaEdiL("FREE_C21") = String.Concat(drRcvEdiHed("DEST_AD_1").ToString, drRcvEdiHed("DEST_AD_2").ToString)
        drOutkaEdiL("FREE_C22") = drRcvEdiHed("DEST_NM").ToString().Trim()
        drOutkaEdiL("FREE_C23") = String.Empty
        drOutkaEdiL("FREE_C24") = String.Empty
        drOutkaEdiL("FREE_C25") = String.Empty
        drOutkaEdiL("FREE_C26") = String.Empty
        drOutkaEdiL("FREE_C27") = String.Empty
        drOutkaEdiL("FREE_C28") = String.Empty
        drOutkaEdiL("FREE_C29") = "00"
        drOutkaEdiL("FREE_C30") = "00"

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_L").Rows.Add(drOutkaEdiL)
        Return setDs

    End Function

#End Region

#Region "セミEDI時　データセット設定(EDI入荷(中))"

    ''' <summary>
    ''' データセット設定(EDI入荷(中)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiInkaEdiM(ByVal setDs As DataSet _
                                    , ByVal setDsLMH010 As DataSet _
                                    , ByVal sSerchKey As String _
                                    , ByVal sNrsGoodsCd As String _
                                    , ByVal sNrsGoodsNm As String _
                                    , ByVal sIrime As String _
                                    ) As DataSet

        Dim drInkaEdiM As DataRow = setDsLMH010.Tables("LMH010_INKAEDI_M").NewRow()
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_INOUTKAEDI_DTL_SMK").Rows(0)
        Dim drRcvEdiHed As DataRow = setDs.Tables("LMH030_INOUTKAEDI_HED_SMK").Rows(0)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)

        drInkaEdiM("DEL_KB") = drRcvEdiDtl("DEL_KB")
        drInkaEdiM("NRS_BR_CD") = drRcvEdiHed("NRS_BR_CD")
        drInkaEdiM("EDI_CTL_NO") = drRcvEdiHed("EDI_CTL_NO")
        drInkaEdiM("EDI_CTL_NO_CHU") = drRcvEdiDtl("EDI_CTL_NO_CHU")
        drInkaEdiM("INKA_CTL_NO_L") = String.Empty
        drInkaEdiM("INKA_CTL_NO_M") = String.Empty
        drInkaEdiM("NRS_GOODS_CD") = sNrsGoodsCd
        drInkaEdiM("CUST_GOODS_CD") = drRcvEdiDtl("GOODS_CD")
        '商品名は受信した商品名を使用
        'drInkaEdiM("GOODS_NM") = sNrsGoodsNm
        drInkaEdiM("GOODS_NM") = drRcvEdiDtl("GOODS_NM")
        drInkaEdiM("NB") = Convert.ToDouble(drRcvEdiDtl("KOSU").ToString)
        drInkaEdiM("NB_UT") = String.Empty
        drInkaEdiM("PKG_NB") = 1
        drInkaEdiM("PKG_UT") = String.Empty
        drInkaEdiM("INKA_PKG_NB") = 0
        drInkaEdiM("HASU") = Convert.ToDouble(drRcvEdiDtl("KOSU").ToString)
        drInkaEdiM("STD_IRIME") = sIrime
        drInkaEdiM("STD_IRIME_UT") = String.Empty
        drInkaEdiM("BETU_WT") = Convert.ToDouble(drRcvEdiDtl("IRIME"))
        drInkaEdiM("CBM") = 0
        drInkaEdiM("ONDO_KB") = "01"
        drInkaEdiM("OUTKA_FROM_ORD_NO") = drRcvEdiDtl("CUST_ORD_NO_DTL").ToString().Trim()
        drInkaEdiM("BUYER_ORD_NO") = drRcvEdiDtl("BUYER_ORD_NO_DTL").ToString().Trim()
        drInkaEdiM("REMARK") = drRcvEdiDtl("KOJO_MSG").ToString().Trim()
        drInkaEdiM("LOT_NO") = drRcvEdiDtl("LOT_NO").ToString().Trim()
        drInkaEdiM("SERIAL_NO") = String.Empty
        Dim dIrime As Double = Convert.ToDouble(sIrime)
        If dIrime = 0 Then
            drInkaEdiM("IRIME") = sIrime
        Else
            drInkaEdiM("IRIME") = Convert.ToDouble(drRcvEdiDtl("IRIME"))
        End If
        drInkaEdiM("IRIME_UT") = "KG"
        drInkaEdiM("OUT_KB") = "0"
        If drRcvEdiDtl("RB_KB").ToString().Equals("2") = True Then
            drInkaEdiM("AKAKURO_KB") = "1"
        Else
            drInkaEdiM("AKAKURO_KB") = "0"
        End If
        'drOutkaEdiM("AKAKURO_KB") = drRcvEdiHed("CANCEL_FLG")
        drInkaEdiM("JISSEKI_FLAG") = "0"
        drInkaEdiM("JISSEKI_USER") = String.Empty
        drInkaEdiM("JISSEKI_DATE") = String.Empty
        drInkaEdiM("JISSEKI_TIME") = String.Empty
        drInkaEdiM("FREE_N01") = drRcvEdiDtl("SURYO")
        drInkaEdiM("FREE_N02") = 0
        drInkaEdiM("FREE_N03") = 0
        drInkaEdiM("FREE_N04") = 0
        drInkaEdiM("FREE_N05") = 0
        drInkaEdiM("FREE_N06") = 0
        drInkaEdiM("FREE_N07") = 0
        drInkaEdiM("FREE_N08") = 0
        drInkaEdiM("FREE_N09") = 0
        drInkaEdiM("FREE_N10") = 0
        drInkaEdiM("FREE_C01") = drRcvEdiDtl("EDI_CTL_NO").ToString()
        drInkaEdiM("FREE_C02") = drRcvEdiDtl("EDI_CTL_NO_CHU").ToString()
        drInkaEdiM("FREE_C03") = String.Concat(drRcvEdiDtl("DENP_NO").ToString(), drRcvEdiDtl("DENP_NO_EDA").ToString())
        drInkaEdiM("FREE_C04") = String.Concat(drRcvEdiDtl("DENP_NO_REN").ToString(), drRcvEdiDtl("DENP_NO_GYO").ToString())
        drInkaEdiM("FREE_C05") = drRcvEdiDtl("GOODS_CLASS").ToString()
        drInkaEdiM("FREE_C06") = drRcvEdiDtl("NISUGATA").ToString()
        drInkaEdiM("FREE_C07") = drRcvEdiDtl("TANA_KB").ToString()
        drInkaEdiM("FREE_C08") = drRcvEdiDtl("ZAIKO_KB").ToString()
        drInkaEdiM("FREE_C09") = drRcvEdiDtl("SEISAN_KOJO").ToString()
        drInkaEdiM("FREE_C10") = drRcvEdiDtl("YOUKI_JOKEN").ToString()
        drInkaEdiM("FREE_C11") = drRcvEdiDtl("NIWATASHI_JOKEN").ToString()
        drInkaEdiM("FREE_C12") = drRcvEdiDtl("SHIKEN_HYO").ToString()
        drInkaEdiM("FREE_C13") = drRcvEdiDtl("SHITEI_DENPYO").ToString()
        drInkaEdiM("FREE_C14") = drRcvEdiDtl("BUYER_CD").ToString()
        drInkaEdiM("FREE_C15") = drRcvEdiDtl("BUYER_TEL").ToString()
        drInkaEdiM("FREE_C16") = drRcvEdiDtl("HANBAI_KA").ToString()
        drInkaEdiM("FREE_C17") = drRcvEdiDtl("HANBAI_TANTO").ToString()
        drInkaEdiM("FREE_C18") = drRcvEdiDtl("ORDER_KB").ToString()
        drInkaEdiM("FREE_C19") = drRcvEdiDtl("BUYER_NM").ToString()
        drInkaEdiM("FREE_C20") = drRcvEdiDtl("SEARCH_KEY_1").ToString()
        drInkaEdiM("FREE_C21") = String.Empty
        drInkaEdiM("FREE_C22") = String.Empty
        drInkaEdiM("FREE_C23") = String.Empty
        drInkaEdiM("FREE_C24") = String.Empty
        drInkaEdiM("FREE_C25") = String.Empty
        drInkaEdiM("FREE_C26") = String.Empty
        drInkaEdiM("FREE_C27") = String.Empty
        drInkaEdiM("FREE_C28") = String.Empty
        drInkaEdiM("FREE_C29") = String.Empty
        drInkaEdiM("FREE_C30") = String.Empty

        If drInkaEdiM("DEL_KB").ToString().Equals("3") = True Then
            drInkaEdiM("SYS_DEL_FLG") = "0"
        Else
            drInkaEdiM("SYS_DEL_FLG") = drRcvEdiDtl("DEL_KB")
        End If

        'データセットに設定
        setDsLMH010.Tables("LMH010_INKAEDI_M").Rows.Add(drInkaEdiM)

        Return setDsLMH010

    End Function

#End Region

#Region "セミEDI時　データセット設定(EDI入荷(大))"

    ''' <summary>
    ''' データセット設定(EDI入荷(大)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiInkaEdiL(ByVal setDs As DataSet, ByVal setDsLMH010 As DataSet) As DataSet

        Dim drInkaEdiL As DataRow = setDsLMH010.Tables("LMH010_INKAEDI_L").NewRow()
        Dim drRcvEdiHed As DataRow = setDs.Tables("LMH030_INOUTKAEDI_HED_SMK").Rows(0)
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_INOUTKAEDI_DTL_SMK").Rows(0)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        '荷主Index
        Dim ediCustIndex As String = drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString()

        drInkaEdiL("DEL_KB") = drRcvEdiHed("DEL_KB")
        'drInkaEdiL("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drInkaEdiL("NRS_BR_CD") = drRcvEdiHed("NRS_BR_CD")
        drInkaEdiL("EDI_CTL_NO") = drRcvEdiHed("EDI_CTL_NO")
        drInkaEdiL("INKA_CTL_NO_L") = String.Empty
        drInkaEdiL("INKA_TP") = "10"
        drInkaEdiL("INKA_KB") = "10"
        drInkaEdiL("INKA_STATE_KB") = "10"
        '入荷(予定)日
        drInkaEdiL("INKA_DATE") = drRcvEdiHed("OUTKA_PLAN_DATE")
        drInkaEdiL("INKA_TIME") = "0900"
        drInkaEdiL("NRS_WH_CD") = drRcvEdiHed("WH_CD")
        drInkaEdiL("CUST_CD_L") = drRcvEdiHed("CUST_CD_L")
        drInkaEdiL("CUST_CD_M") = drRcvEdiHed("CUST_CD_M")
        drInkaEdiL("CUST_NM_L") = String.Empty
        drInkaEdiL("CUST_NM_M") = String.Empty
        '入荷予定数量
        drInkaEdiL("INKA_PLAN_QT") = 0
        drInkaEdiL("INKA_PLAN_QT_UT") = String.Empty
        '入荷総個数
        drInkaEdiL("INKA_TTL_NB") = 0
        drInkaEdiL("NAIGAI_KB") = "01"
        drInkaEdiL("BUYER_ORD_NO") = drRcvEdiDtl("BUYER_ORD_NO_DTL").ToString().Trim()
        drInkaEdiL("OUTKA_FROM_ORD_NO") = String.Concat(drRcvEdiHed("DENP_NO").ToString().Trim(), "-", drRcvEdiHed("DENP_NO_EDA").ToString().Trim())
        drInkaEdiL("SEIQTO_CD") = String.Empty
        drInkaEdiL("TOUKI_HOKAN_YN") = "1"
        drInkaEdiL("HOKAN_YN") = "1"
        drInkaEdiL("HOKAN_FREE_KIKAN") = 0
        drInkaEdiL("HOKAN_STR_DATE") = drRcvEdiHed("OUTKA_PLAN_DATE")
        drInkaEdiL("NIYAKU_YN") = "1"
        drInkaEdiL("TAX_KB") = "01"
        ''注意事項
        drInkaEdiL("REMARK") = drRcvEdiHed("DEST_MSG").ToString().Trim()
        drInkaEdiL("NYUBAN_L") = String.Empty
        drInkaEdiL("UNCHIN_TP") = "00"
        drInkaEdiL("UNCHIN_KB") = String.Empty
        drInkaEdiL("OUTKA_MOTO") = String.Empty
        drInkaEdiL("SYARYO_KB") = String.Empty
        drInkaEdiL("UNSO_ONDO_KB") = String.Empty
        drInkaEdiL("UNSO_CD") = String.Empty
        drInkaEdiL("UNSO_BR_CD") = String.Empty
        drInkaEdiL("UNCHIN") = 0
        drInkaEdiL("YOKO_TARIFF_CD") = String.Empty

        If drRcvEdiHed("INOUT_ENTRY_KB").ToString().Equals("1") = True Then
            drInkaEdiL("OUT_FLAG") = "0"
            drInkaEdiL("JISSEKI_FLAG") = "0"
        Else
            drInkaEdiL("OUT_FLAG") = "9"
            drInkaEdiL("JISSEKI_FLAG") = "9"
        End If

        If drRcvEdiHed("RB_KB").ToString().Equals("2") = True Then
            drInkaEdiL("AKAKURO_KB") = "1"
        Else
            drInkaEdiL("AKAKURO_KB") = "0"
        End If
        'drOutkaEdiL("AKAKURO_KB") = drRcvEdiHed("CANCEL_FLG")
        drInkaEdiL("JISSEKI_USER") = String.Empty
        drInkaEdiL("JISSEKI_DATE") = String.Empty
        drInkaEdiL("JISSEKI_TIME") = String.Empty
        drInkaEdiL("FREE_N01") = 0
        drInkaEdiL("FREE_N02") = 0
        drInkaEdiL("FREE_N03") = 0
        drInkaEdiL("FREE_N04") = 0
        drInkaEdiL("FREE_N05") = 0
        drInkaEdiL("FREE_N06") = 0
        drInkaEdiL("FREE_N07") = 0
        drInkaEdiL("FREE_N08") = 0
        drInkaEdiL("FREE_N09") = 0
        drInkaEdiL("FREE_N10") = 0

        drInkaEdiL("FREE_C01") = drRcvEdiHed("EDI_CTL_NO").ToString().Trim()
        drInkaEdiL("FREE_C02") = drRcvEdiHed("DENP_NO").ToString().Trim()
        drInkaEdiL("FREE_C03") = drRcvEdiHed("UKE_CHUKEI").ToString().Trim()
        drInkaEdiL("FREE_C04") = drRcvEdiHed("UKE_AITE").ToString().Trim()
        drInkaEdiL("FREE_C05") = drRcvEdiHed("UKE_SHUBETSU").ToString().Trim()
        drInkaEdiL("FREE_C06") = drRcvEdiHed("ARR_PLAN_DATE").ToString().Trim()
        drInkaEdiL("FREE_C07") = drRcvEdiHed("DEST_CD").ToString().Trim()
        drInkaEdiL("FREE_C08") = drRcvEdiHed("DEST_ZIP").ToString().Trim()
        drInkaEdiL("FREE_C09") = drRcvEdiHed("DEST_TEL").ToString().Trim()
        drInkaEdiL("FREE_C10") = drRcvEdiHed("DEST_AD_1").ToString().Trim()
        drInkaEdiL("FREE_C11") = drRcvEdiHed("DEST_AD_2").ToString().Trim()
        drInkaEdiL("FREE_C12") = drRcvEdiHed("DEST_NM_KANA").ToString().Trim()
        drInkaEdiL("FREE_C13") = drRcvEdiHed("DEST_AD_KANA1").ToString().Trim()
        drInkaEdiL("FREE_C14") = drRcvEdiHed("DEST_AD_KANA2").ToString().Trim()
        drInkaEdiL("FREE_C15") = String.Empty
        drInkaEdiL("FREE_C16") = String.Empty
        drInkaEdiL("FREE_C17") = String.Empty
        drInkaEdiL("FREE_C18") = String.Empty
        drInkaEdiL("FREE_C19") = String.Empty
        drInkaEdiL("FREE_C20") = String.Concat(drRcvEdiHed("CUST_CD_L").ToString(), drRcvEdiHed("CUST_CD_M").ToString())
        drInkaEdiL("FREE_C21") = String.Concat(drRcvEdiHed("DEST_AD_1").ToString, drRcvEdiHed("DEST_AD_2").ToString)
        drInkaEdiL("FREE_C22") = drRcvEdiHed("DEST_NM").ToString().Trim()
        drInkaEdiL("FREE_C23") = String.Empty
        drInkaEdiL("FREE_C24") = String.Empty
        drInkaEdiL("FREE_C25") = String.Empty
        drInkaEdiL("FREE_C26") = String.Empty
        drInkaEdiL("FREE_C27") = String.Empty
        drInkaEdiL("FREE_C28") = String.Empty
        drInkaEdiL("FREE_C29") = String.Empty
        drInkaEdiL("FREE_C30") = String.Empty

        If drInkaEdiL("DEL_KB").ToString().Equals("3") = True Then
            drInkaEdiL("SYS_DEL_FLG") = "0"
        Else
            drInkaEdiL("SYS_DEL_FLG") = drRcvEdiHed("DEL_KB")
        End If

        'データセットに設定
        setDsLMH010.Tables("LMH010_INKAEDI_L").Rows.Add(drInkaEdiL)

        Return setDsLMH010

    End Function

#End Region

#End Region

#Region "セミEDI時　商品マスタからCustCd等を取得する"

    ''' <summary>
    ''' 商品マスタからCustCd等を取得する
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCustCd(ByVal ds As DataSet _
                             , ByRef sSerchKey As String _
                             , ByRef sNrsGoodsCd As String _
                             , ByRef sNrsGoodsNm As String _
                             , ByRef sIrime As String _
                              ) As Integer

        Dim dtMstGoods As DataTable = ds.Tables("LMH030_M_GOODS")
        Dim iMstGoodsCnt As Integer = dtMstGoods.Rows.Count

        Select Case iMstGoodsCnt

            Case 0      '商品マスタ取得０件
                sSerchKey = String.Empty
                sNrsGoodsCd = String.Empty
                sNrsGoodsNm = String.Empty
                sIrime = "0"

            Case 1      '商品マスタ取得１件
                '荷主は商品マスタから取得する
                sSerchKey = dtMstGoods.Rows(0).Item("SEARCH_KEY_1").ToString
                sNrsGoodsCd = dtMstGoods.Rows(0).Item("GOODS_CD_NRS").ToString
                sNrsGoodsNm = dtMstGoods.Rows(0).Item("GOODS_NM_1").ToString
                sIrime = dtMstGoods.Rows(0).Item("STD_IRIME_NB").ToString

            Case Else   '商品マスタ取得２件以上

                sNrsGoodsCd = String.Empty
                sNrsGoodsNm = String.Empty

                '入目の単一確認
                For i As Integer = 1 To iMstGoodsCnt - 1  'Rows(1)から開始'■要望番号:1612（セミEDI 荷主商品コード重複チェックでアベンド) 2012/12/14 本明修正　（iMstGoodsCnt→iMstGoodsCnt-1に修正）
                    If (dtMstGoods.Rows(i).Item("STD_IRIME_NB").ToString()).Equals _
                       (dtMstGoods.Rows(i - 1).Item("STD_IRIME_NB").ToString()) Then
                        '等しい場合はセットする
                        sIrime = dtMstGoods.Rows(i).Item("STD_IRIME_NB").ToString
                    Else

                        If Convert.ToDecimal(ds.Tables("LMH030_INOUTKAEDI_DTL_SMK").Rows(0).Item("IRIME")) <> 0 Then
                            sIrime = ds.Tables("LMH030_INOUTKAEDI_DTL_SMK").Rows(0).Item("IRIME").ToString()
                        Else
                            '等しくない場合は既定値をセットして抜ける
                            sIrime = "0"
                        End If

                        Exit For
                    End If
                Next

        End Select

    End Function

#Region "セミEDI データセット設定(EDI管理番号(大・中))"

    ''' <summary>
    ''' データセット設定(EDI管理番号(大・中)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdiCtlNo(ByVal ds As DataSet _
                               , ByVal iDeleteFlg As Integer, ByVal iCancelFlg As Integer, ByVal iSkipFlg As Integer, ByVal bSameKeyFlg As Boolean _
                                , ByVal iakakuroFlg As Integer, ByRef sEdiCtlNo As String, ByRef iEdiCtlNoChu As Integer) As DataSet
        '(2013.02.25)要望番号1898 修正START
        'Private Function GetEdiCtlNo(ByVal ds As DataSet _
        '                           , ByVal iDeleteFlg As Integer, ByVal iCancelFlg As Integer, ByVal iSkipFlg As Integer, ByVal bSameKeyFlg As Boolean _
        '                            , ByRef sEdiCtlNo As String, ByRef iEdiCtlNoChu As Integer) As DataSet
        '(2013.02.25)要望番号1898 修正END

        Dim dtRcvEdiHed As DataTable = ds.Tables("LMH030_INOUTKAEDI_HED_SMK")
        Dim drRcvEdiHed As DataRow = ds.Tables("LMH030_INOUTKAEDI_HED_SMK").Rows(0)
        Dim dtRcvEdiDtl As DataTable = ds.Tables("LMH030_INOUTKAEDI_DTL_SMK")
        Dim drRcvEdiDtl As DataRow = ds.Tables("LMH030_INOUTKAEDI_DTL_SMK").Rows(0)
        Dim sNrsBrCd As String = ds.Tables("LMH030_INOUTKAEDI_DTL_SMK").Rows(0).Item("NRS_BR_CD").ToString()


        '前行とキーが異なる場合　
        If bSameKeyFlg = False Then
            iEdiCtlNoChu = 0    '０クリア    
        End If

        'EDI管理番号(中)をカウントアップ
        iEdiCtlNoChu = iEdiCtlNoChu + 1

        '(2013.02.25)要望番号1898 修正START
        If (iCancelFlg = 0 AndAlso iSkipFlg = 0 AndAlso iakakuroFlg = 0) OrElse _
           (iCancelFlg = 1 AndAlso iSkipFlg = 0 AndAlso iakakuroFlg = 0) Then
            'If iCancelFlg = 0 AndAlso iSkipFlg = 0 Then
            '(2013.02.25)要望番号1898 修正END
            'キャンセルフラグが０ かつ スキップフラグが０の場合　
            If bSameKeyFlg = False Then
                Dim num As New NumberMasterUtility

                '前行とキーが異なる場合　
                If drRcvEdiHed.Item("INOUT_KB").ToString().Equals("0") = True Then
                    'EDI管理番号(大)を新規採番してEDI管理番号(中)を"001"採番
                    sEdiCtlNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_OUTKA_NO_L, Me, sNrsBrCd)
                ElseIf drRcvEdiHed.Item("INOUT_KB").ToString().Equals("1") = True Then
                    'EDI管理番号(大)を新規採番してEDI管理番号(中)を"001"採番
                    sEdiCtlNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_INKA_NO_L, Me, sNrsBrCd)
                End If

            End If

            '登録用EDI管理番号
            dtRcvEdiHed.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo              'HEDにセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo              'DTLにセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO_CHU") = iEdiCtlNoChu.ToString("000")   'EDI_CHUにセット
            dtRcvEdiDtl.Rows(0).Item("GYO") = iEdiCtlNoChu.ToString("00")               '行数にもEDI_CHUと同じ値をセット
            dtRcvEdiDtl.Rows(0).Item("RPT_PAGE") = (((iEdiCtlNoChu - 1) / 8) + 1).ToString("000")   '印刷時ページ番号
            dtRcvEdiDtl.Rows(0).Item("RPT_LINE") = (((iEdiCtlNoChu - 1) Mod 8) + 1).ToString        '印刷時行番号
        Else
            dtRcvEdiHed.Rows(0).Item("EDI_CTL_NO") = DEF_CTL_NO             'HEDに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO") = DEF_CTL_NO             'DTLに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO_CHU") = "000"              'EDI_CHUに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("GYO") = iEdiCtlNoChu.ToString("00")   '行数にはカウントアップした値を入れる
            dtRcvEdiDtl.Rows(0).Item("RPT_PAGE") = (((iEdiCtlNoChu - 1) / 8) + 1).ToString("000")   '印刷時ページ番号
            dtRcvEdiDtl.Rows(0).Item("RPT_LINE") = (((iEdiCtlNoChu - 1) Mod 8) + 1).ToString        '印刷時行番号
        End If
        ''削除EDI管理番号にも設定する(削除フラグが１の場合のみ)
        'If iDeleteFlg = 1 Then
        '    Dim dtRcvHedDel As DataTable = ds.Tables("LMH030_HED_SMK_CANCELOUT")
        '    Dim drRcvHedDel As DataRow = ds.Tables("LMH030_DTL_SMK_CANCELOUT").Rows(0)

        '    Dim dtRcvDtlDel As DataTable = ds.Tables("LMH030_DTL_SMK_CANCELOUT")
        '    Dim drRcvDtlDel As DataRow = ds.Tables("LMH030_DTL_SMK_CANCELOUT").Rows(0)
        '    dtRcvHedDel.Rows(0).Item("DELETE_EDI_NO") = sEdiCtlNo
        '    dtRcvDtlDel.Rows(0).Item("DELETE_EDI_NO") = sEdiCtlNo
        '    dtRcvDtlDel.Rows(0).Item("DELETE_EDI_NO_CHU") = iEdiCtlNoChu.ToString("000")
        'End If

        Return ds

    End Function

#End Region
#End Region

#Region "画面取込(セミEDI)チェック処理"
    ''' <summary>
    ''' 画面取込(セミEDI)チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomiChk(ByVal ds As DataSet) As DataSet

        Dim dtSemiInfo As DataTable = ds.Tables("LMH030_SEMIEDI_INFO")
        Dim dtSemiHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")
        Dim dtSemiDtl As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")

        Dim dr As DataRow
        Dim hedDr As DataRow = dtSemiHed.Rows(0)

        Dim max As Integer = dtSemiDtl.Rows.Count - 1
        Dim hedmax As Integer = dtSemiHed.Rows.Count - 1

        Dim ediCustIndex As String = dtSemiInfo.Rows(0).Item("EDI_CUST_INDEX").ToString()

        Dim smkNrsNrCd As String = String.Empty

        Dim iRowCnt As Integer = 0

        '------------------------------------------------------------------------------------------
        ' 対象データのみ抜き出す
        ' ※ＪＴ固有機能
        '------------------------------------------------------------------------------------------
        For i As Integer = 0 To hedmax

            If dtSemiHed.Rows(i).Item("ERR_FLG").ToString.Equals("1") Then
                '最初からエラーフラグが立っている場合（明細件数０件の場合）

            Else

                Select Case Convert.ToInt32(ediCustIndex)

                    Case LMH030BLC.EdiCustIndex.Smk00952_00
                        smkNrsNrCd = LMH030BLC502.UKEHARAI_GNM

                    Case LMH030BLC.EdiCustIndex.SmkChb00002_00
                        smkNrsNrCd = LMH030BLC502.UKEHARAI_CBA

                    Case LMH030BLC.EdiCustIndex.SmkChb00404_00
                        smkNrsNrCd = LMH030BLC502.UKEHARAI_CBA
                    Case Else


                End Select

                'ファイル内の受払場所(COLUMN_6,COLUMN_1)で抽出しセットする
                Dim drSelect As DataRow() = dtSemiDtl.Select(" COLUMN_6 =  '" & smkNrsNrCd & "' AND " & _
                                                        " COLUMN_1  =  '17'")

                If drSelect.Count = 0 Then
                    '抜き出したデータRowが０件の場合
                    dtSemiHed.Rows(i).Item("ERR_FLG") = "1" '０件エラーフラグを立てる

                Else

                    'SelectしたデータをdtSemiDtlに再セットする
                    Dim dtSelect As DataTable           'Select後の DataTable を用意
                    dtSelect = dtSemiDtl.Clone          'Select前テーブルの情報をクローン化
                    For Each row As DataRow In drSelect
                        dtSelect.ImportRow(row)         'SelectしたデータRowをクローンにセットする
                    Next

                    'dtSemiDtlに再セット（以降の処理はdtSemiDtlで処理されるため）
                    dtSemiDtl.Clear()
                    For k As Integer = 0 To dtSelect.Rows.Count - 1
                        dtSemiDtl.ImportRow(dtSelect.Rows(k))
                    Next

                End If

            End If
        Next

        max = dtSemiDtl.Rows.Count - 1  'dtSemiDtlに再セット後の最大件数を再取得


        For i As Integer = 0 To hedmax

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
                        If Me.TorikomiValChk(dr, ediCustIndex) = False Then

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

#Region "カラム項目の値・日付チェック"

    ''' <summary>
    ''' 値・日付チェック
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="ediCustIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function TorikomiValChk(ByVal dr As DataRow, ByVal ediCustIndex As String) As Boolean
        Dim sFileNm As String = dr.Item("FILE_NAME_RCV").ToString()
        Dim sRecNo As String = dr.Item("REC_NO").ToString()
        Dim bRet As Boolean = True

        '処理日(カラム4番目)
        Dim sSyoriDate As String = dr.Item("COLUMN_4").ToString()
        Dim sSyoriDateWithSlash As String = Me._Blc.GetSlashEditDate(sSyoriDate) 'スラッシュ付加

        If IsDate(sSyoriDateWithSlash) = True Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("処理日(カラム4番目)[", sSyoriDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '出荷日(カラム5番目)
        Dim sOutkaDate As String = dr.Item("COLUMN_5").ToString()
        Dim sOutkaDateWithSlash As String = Me._Blc.GetSlashEditDate(sOutkaDate) 'スラッシュ付加

        If IsDate(sOutkaDateWithSlash) = True Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("出荷日(カラム5番目)[", sOutkaDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '容量(カラム13番目)
        Dim sIrime As String = dr.Item("COLUMN_13").ToString().Trim()
        If String.IsNullOrEmpty(sIrime) = True Then
            '空の場合はゼロをセット
            dr.Item("COLUMN_13") = 0
        Else
            If IsNumeric(sIrime) Then
                '数値の場合
                Dim dIrime As Double = Convert.ToDouble(sIrime)
                If dIrime > 999999999.999 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat("容量(カラム13番目)[", dIrime.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            Else
                '数値でない場合
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat("容量(カラム13番目)[", sIrime, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            End If
        End If

        '個数(カラム18番目)
        Dim sKosu As String = dr.Item("COLUMN_18").ToString().Trim()

        If String.IsNullOrEmpty(sKosu) = True Then
            '空の場合はゼロをセット
            dr.Item("COLUMN_18") = 0
        Else
            If IsNumeric(sKosu) = True Then
                '数値の場合
                Dim iKosu As Integer = Convert.ToInt32(sKosu)

                If iKosu > 99999999 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat("個数(カラム18番目)[", iKosu.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            Else
                '数値でない場合
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat("個数(カラム18番目)[", sKosu, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '数量(カラム19番目)
        Dim sQT As String = dr.Item("COLUMN_19").ToString().Trim()
        If String.IsNullOrEmpty(sQT) = True Then
            '空の場合はゼロをセット
            dr.Item("COLUMN_19") = 0
        Else
            If IsNumeric(sQT) Then
                '数値の場合
                Dim dQT As Double = Convert.ToDouble(sQT)
                If dQT > 999999999999.999 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat("数量(カラム19番目)[", dQT.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            Else
                '数値でない場合
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat("数量(カラム19番目)[", sQT, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            End If
        End If

        '荷着日(カラム20番目)
        Dim sNicyakuDate As String = dr.Item("COLUMN_20").ToString()
        Dim sNicyakuDateWithSlash As String = Me._Blc.GetSlashEditDate(sNicyakuDate) 'スラッシュ付加

        If IsDate(sNicyakuDateWithSlash) = True OrElse sNicyakuDate.Equals("00000000") = True Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("荷着日(カラム20番目)[", sNicyakuDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '作成日(カラム43番目)
        Dim sCreDate As String = dr.Item("COLUMN_43").ToString()
        Dim sCreDateWithSlash As String = Me._Blc.GetSlashEditDate(sCreDate) 'スラッシュ付加

        If IsDate(sCreDateWithSlash) = True Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("作成日(カラム43番目)[", sCreDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '戻り値設定
        Return bRet

    End Function

#End Region

#Region "画面取込(セミEDI)データセット＋更新処理"

    ''' <summary>
    ''' 画面取込(セミEDI)データセット＋更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomi(ByVal ds As DataSet) As DataSet

        Dim dtSetHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")        '取込Hed
        Dim dtSetDtl As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")        '取込Dtl
        Dim dtSetRet As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_RET")        '処理件数

        Dim dtRcvHed As DataTable = ds.Tables("LMH030_INOUTKAEDI_HED_SMK")         'EDI受信Hed
        Dim dtRcvDtl As DataTable = ds.Tables("LMH030_INOUTKAEDI_DTL_SMK")         'EDI受信Dtl
        Dim dtRcvHedCancel As DataTable = ds.Tables("LMH030_HED_SMK_CANCELOUT")  'EDI受信Hed

        Dim iCancelCnt As Integer = 0
        Dim iGoodsCnt As Integer = 0

        Dim iSetDtlMax As Integer = dtSetDtl.Rows.Count - 1

        Dim sWhcd As String = String.Empty          '倉庫コード     
        Dim sNrsGoodsCd As String = String.Empty    '日陸商品コード （※商品コードから取得する）
        Dim sNrsGoodsNm As String = String.Empty    '日陸商品名     （※商品コードから取得する）
        Dim sSerchKey As String = String.Empty      '日陸商品名     （※商品コードから取得する）
        Dim sIrime As String = String.Empty         '入目           （※商品コードから取得する）

        Dim iDeleteFlg As Integer = 0               '取消フラグ         (現行LMに合わせた)
        Dim iCancelFlg As Integer = 0               'キャンセルフラグ   (現行LMに合わせた)
        Dim iAkakuroFlg As Integer = 0              '赤黒フラグ         (現行LMに合わせた)
        Dim iSkipFlg As Integer = 0                 'スキップフラグ     (現行LMに合わせた)
        Dim iHakkoFlg As Integer = 0                '発行フラグ         (現行LMに合わせた)

        Dim sNowKey As String = String.Empty        'キー項目（Temp用）
        Dim sOldKey As String = String.Empty        'キー項目（前行）
        Dim sNewKey As String = String.Empty        'キー項目（現在行）
        Dim bSameKeyFlg As Boolean = False          '前行とキーが同じ場合True、異なる場合False

        Dim sEdiCtlNo As String = String.Empty      'EDI管理番号
        Dim iEdiCtlNoChu As Integer = 0             'EDI管理番号（中）

        Dim iRcvHedInsCnt As Integer = 0            '書込件数（受信HED）
        Dim iRcvDtlInsCnt As Integer = 0            '書込件数（受信DTL）
        Dim iOutHedInsCnt As Integer = 0            '書込件数（出荷EDI(大)）
        Dim iOutDtlInsCnt As Integer = 0            '書込件数（出荷EDI(中)）
        Dim iInHedInsCnt As Integer = 0             '書込件数（入荷EDI(大)）
        Dim iInDtlInsCnt As Integer = 0             '書込件数（入荷EDI(中)）
        Dim iRcvHedCanCnt As Integer = 0            '取消件数（受信HED）
        Dim iRcvDtlCanCnt As Integer = 0            '取消件数（受信Dtl）
        Dim iOutHedCanCnt As Integer = 0            '取消件数（出荷EDI(大)）
        Dim iOutDtlCanCnt As Integer = 0            '取消件数（出荷EDI(中)）
        Dim iInHedCanCnt As Integer = 0             '取消件数（入荷EDI(大)）
        Dim iInDtlCanCnt As Integer = 0             '取消件数（入荷EDI(中)）

        Dim bNoErr As Boolean = True                'エラー無しフラグ（True：エラー無し、False：エラー有り）

        For i As Integer = 0 To iSetDtlMax

            '---------------------------------------------------------------------------
            ' セミEDI取込(共通)⇒受信HED/DTLへのデータセット
            '---------------------------------------------------------------------------
            ds.Tables("LMH030_INOUTKAEDI_HED_SMK").Clear() '受信HEDをクリア
            ds.Tables("LMH030_INOUTKAEDI_DTL_SMK").Clear() '受信DTLをクリア
            ds = Me.SetSemiOutkaEdiRcv(ds, i)

            Dim drEdiRcvHed As DataRow = ds.Tables("LMH030_INOUTKAEDI_HED_SMK").Rows(0)
            Dim drEdiRcvDtl As DataRow = ds.Tables("LMH030_INOUTKAEDI_DTL_SMK").Rows(0)

            '---------------------------------------------------------------------------
            ' 商品コードを設定
            '---------------------------------------------------------------------------
            '商品マスタ情報を取得する
            '(2013.02.19)要望番号1875 追加START
            ds.Tables("LMH030_M_GOODS").Clear() '商品MのDATATABLEをクリア
            '(2013.02.19)要望番号1875 追加END
            ds = MyBase.CallDAC(Me._Dac, "SelectMstGoods", ds)

            '取得した商品マスタから商品情報を設定する
            Call Me.GetCustCd(ds, sSerchKey, sNrsGoodsCd, sNrsGoodsNm, sIrime)

            '---------------------------------------------------------------------------
            ' キー項目設定
            '---------------------------------------------------------------------------
            sNewKey = String.Concat(drEdiRcvHed.Item("UKE_SHUBETSU").ToString _
                                      , drEdiRcvHed.Item("RB_KB").ToString _
                                      , drEdiRcvHed.Item("DENP_NO").ToString _
                                      , drEdiRcvHed.Item("DENP_NO_EDA").ToString _
                                      , drEdiRcvHed.Item("INOUT_KB").ToString())
            If i = 0 Then
                '1番目は必ずbSameKeyFlgはFalse
                bSameKeyFlg = False

            Else
                '2番目以降はキーを比較
                If sNewKey.Equals(sOldKey) = True Then
                    'キーが同一の場合
                    bSameKeyFlg = True
                Else
                    'キーが異なる場合
                    bSameKeyFlg = False

                    'キーセットは最後にiSkipFlgの値で判断する
                    'sOldKey = sNewKey   'OldキーにNewキーをセット
                End If
            End If

            '---------------------------------------------------------------------------
            ' 赤黒区分を元に赤黒フラグ、スキップフラグを設定
            '---------------------------------------------------------------------------
            Select Case drEdiRcvDtl.Item("RB_KB").ToString

                Case "0"    '新規
                    iAkakuroFlg = 0     '黒データ
                    iSkipFlg = 0        'EDI出荷登録する
                    iDeleteFlg = 0      'EDI出荷削除しない

                    'Case "1"    '変更
                    '    iAkakuroVal = 0     '黒データ
                    '    iSkipFlg = 0        'EDI出荷登録する
                    '    iDeleteFlg = 1      'EDI出荷削除する

                Case "2"    'キャンセル
                    iAkakuroFlg = 1     '赤データ
                    '(2013.02.25)要望番号1898 修正START
                    'iSkipFlg = 1        'EDI出荷登録しない
                    iSkipFlg = 0        'EDI出荷登録しない
                    '(2013.02.25)要望番号1898 修正END
                    iDeleteFlg = 1      'EDI出荷削除する
            End Select

            '決定した赤黒フラグをdrEdiRcvHedにセットする
            drEdiRcvHed.Item("CANCEL_FLG") = iAkakuroFlg.ToString

            '---------------------------------------------------------------------------
            ' スキップフラグが０かつ前行同一フラグがfalseの場合
            ' 受注伝票番号を元に取消データの確認処理を行う
            '---------------------------------------------------------------------------
            If iSkipFlg = 0 AndAlso bSameKeyFlg = False Then

                '取消フラグ,キャンセルフラグを0にする(初期値)
                iDeleteFlg = 0
                iCancelFlg = 0

                '受信DTL取消データ取得処理
                ds.Tables("LMH030_HED_SMK_CANCELOUT").Clear()    '取得用DSをクリア
                ds = MyBase.CallDAC(Me._Dac, "SelectInOutkaEdiRcvCancel", ds)

                'データ取得できた場合
                If MyBase.GetResultCount > 0 Then
                    '※直近のレコードで判断（SQL内でDESCされているので１件目のレコード）
                    Dim drRcvHedCancel As DataRow = ds.Tables("LMH030_HED_SMK_CANCELOUT").Rows(0)
                    ''取得したデータのキャンセルフラグを設定
                    'Dim sRcvHedCancelFlg As String = drRcvHedCancel.Item("CANCEL_FLG").ToString
                    ''発行フラグ、取得データのキャンセルフラグ、赤黒フラグを元に継続処理判断
                    'Dim sKeyFlg As String = String.Concat(iHakkoFlg.ToString, sRcvHedCancelFlg, iAkakuroFlg.ToString)
                    'Select Case sKeyFlg
                    '    Case "000", "011"   '重複受信エラー
                    '        If Left(sNewKey, 32) = Left(sOldKey, 32) Then
                    '            'メッセージ出力　出荷受信データ伝票番号重複（アイカ工業分混在）
                    '            'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E468", New String() {"（アイカ工業分混在）"}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    '            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E468", New String() {"（アイカ工業分混在）"}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, "")
                    '        Else
                    '            'メッセージ出力　出荷受信データ伝票番号重複
                    '            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E468", New String() {""}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, "")
                    '        End If
                    '        'エラーフラグ設定してからループを出る
                    '        bNoErr = False
                    '        Exit For

                    '    Case "100", "111"    '再発行かつ既に同内容処理済み
                    '        'ファイル名が同一の場合
                    '        If drRcvHedCancel.Item("FILE_NAME").ToString.Equals(drEdiRcvHed.Item("FILE_NAME").ToString) Then
                    '            'メッセージ出力　出荷受信データ伝票番号重複
                    '            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E468", New String() {""}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, "")
                    '            'エラーフラグ設定してからループを出る
                    '            bNoErr = False
                    '            Exit For
                    '        Else
                    '            'スキップフラグを１にして処理続行
                    '            '※受信DTL,HEDは更新、EDI出荷L,Mは更新しない
                    '            iSkipFlg = 1
                    '        End If

                    'Case "001", "101"    '赤データのため削除処理 

                    '取得したデータの出荷管理番号が(DEF_EDI_CTL_NO)(出荷未登録の場合)　
                    If (drRcvHedCancel.Item("INOUT_KB").ToString().Equals("0") = True AndAlso _
                    Right((drRcvHedCancel.Item("OUTKA_CTL_NO").ToString()), 8).Equals("00000000") = True) OrElse _
                    (drRcvHedCancel.Item("INOUT_KB").ToString().Equals("1") = True AndAlso _
                    Right((drRcvHedCancel.Item("INKA_CTL_NO_L").ToString()), 8).Equals("00000000") = True) Then
                        'EDI出荷（大・中）を削除するためキャンセルフラグを"1"にする
                        iCancelFlg = 1
                        iDeleteFlg = 1  '取消フラグを1にする

                        '(2013.02.25)要望番号1898 修正START
                        '赤黒フラグが赤の場合
                    ElseIf iAkakuroFlg = 1 Then

                        iDeleteFlg = 1      '取消フラグを1にする
                        iSkipFlg = 1        'EDI出荷登録しない
                        '(2013.02.25)要望番号1898 修正END
                    End If
                    'End Select
                End If
            End If

            '---------------------------------------------------------------------------
            ' EDI管理番号(大,中)の採番(キャンセルフラグが０の場合も関数内で判断
            '---------------------------------------------------------------------------
            '(2013.02.25)要望番号1898 修正START
            'ds = Me.GetEdiCtlNo(ds, iDeleteFlg, iCancelFlg, iSkipFlg, bSameKeyFlg, sEdiCtlNo, iEdiCtlNoChu)
            ds = Me.GetEdiCtlNo(ds, iDeleteFlg, iCancelFlg, iSkipFlg, bSameKeyFlg, iAkakuroFlg, sEdiCtlNo, iEdiCtlNoChu)
            '(2013.02.25)要望番号1898 修正END
            '---------------------------------------------------------------------------
            ' 取消フラグが"1"の場合、受信データの取消処理を行う
            '---------------------------------------------------------------------------
            If iDeleteFlg = 1 Then

                '削除EDI管理番号に設定する
                Dim drRcvHedCancel As DataRow = ds.Tables("LMH030_HED_SMK_CANCELOUT").Rows(0)
                If String.IsNullOrEmpty(sEdiCtlNo) Then
                    drRcvHedCancel.Item("DELETE_EDI_NO") = DEF_CTL_NO
                    '※DELETE_EDI_NO_CHU項目が存在しないのでDELETE_USER項目にDELETE_EDI_NO_CHUをセットする
                    drRcvHedCancel.Item("DELETE_USER") = "000"
                Else
                    drRcvHedCancel.Item("DELETE_EDI_NO") = sEdiCtlNo
                    '※DELETE_EDI_NO_CHU項目が存在しないのでDELETE_USER項目にDELETE_EDI_NO_CHUをセットする
                    drRcvHedCancel.Item("DELETE_USER") = iEdiCtlNoChu.ToString("000")
                End If

                If drRcvHedCancel.Item("INOUT_KB").ToString().Equals("0") = True Then

                    'EDI受信(DTL)の削除(論理削除)：EDI出荷データ
                    ds = MyBase.CallDAC(Me._Dac, "UpdateDelInOutkaRcvDtl", ds)
                    iRcvDtlCanCnt = iRcvDtlCanCnt + 1

                    'EDI受信(HED)の削除(論理削除)：EDI出荷データ
                    ds = MyBase.CallDAC(Me._Dac, "UpdateDelInOutkaRcvHed", ds)
                    iRcvHedCanCnt = iRcvHedCanCnt + 1

                ElseIf drRcvHedCancel.Item("INOUT_KB").ToString().Equals("1") = True Then

                    'EDI受信(DTL)の削除(論理削除)：EDI入荷データ
                    ds = MyBase.CallDAC(Me._DacInka, "UpdateDelInOutkaRcvDtl", ds)
                    iRcvDtlCanCnt = iRcvDtlCanCnt + 1

                    'EDI受信(HED)の削除(論理削除)：EDI入荷データ
                    ds = MyBase.CallDAC(Me._DacInka, "UpdateDelInOutkaRcvHed", ds)
                    iRcvHedCanCnt = iRcvHedCanCnt + 1

                End If

                '---------------------------------------------------------------------------
                ' キャンセルフラグが"1"の場合、
                ' INOUT_KB = '0'の場合、EDI出荷データの削除更新を行う
                ' INOUT_KB = '1'の場合、EDI入荷データの削除更新を行う
                '---------------------------------------------------------------------------
                If iCancelFlg = 1 Then

                    If drRcvHedCancel.Item("INOUT_KB").ToString().Equals("0") = True Then

                        'EDI出荷(大)の削除(論理削除)
                        ds = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiL", ds)
                        iOutHedCanCnt = iOutHedCanCnt + 1

                        'EDI出荷(中)の削除(論理削除)
                        ds = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiM", ds)
                        iOutDtlCanCnt = iOutDtlCanCnt + 1

                    ElseIf drRcvHedCancel.Item("INOUT_KB").ToString().Equals("1") = True Then

                        'EDI入荷(大)の削除(論理削除)
                        ds = MyBase.CallDAC(Me._DacInka, "UpdateDelInkaEdiL", ds)
                        iOutHedCanCnt = iOutHedCanCnt + 1

                        'EDI入荷(中)の削除(論理削除)
                        ds = MyBase.CallDAC(Me._DacInka, "UpdateDelInkaEdiM", ds)
                        iOutDtlCanCnt = iOutDtlCanCnt + 1

                    End If

                End If

            End If

            '別インスタンス
            Dim setDs As DataSet = ds.Copy()

            Dim setHedDt As DataTable = setDs.Tables("LMH030_INOUTKAEDI_HED_SMK")
            Dim setDtlDt As DataTable = setDs.Tables("LMH030_INOUTKAEDI_DTL_SMK")

            setHedDt.Clear()
            setDtlDt.Clear()

            setHedDt.ImportRow(dtRcvHed.Rows(0))
            setDtlDt.ImportRow(dtRcvDtl.Rows(0))

            '---------------------------------------------------------------------------
            ' EDI受信データの新規追加
            '---------------------------------------------------------------------------
            ' EDI受信データ(DTL)の新規追加
            '削除区分が"3"(保留)の場合は区分値を入れ替えない
            If setDtlDt.Rows(0).Item("DEL_KB").Equals("3") = False Then

                'iSkipFlgを削除区分の値として使用する
                '(2013.02.25)要望番号1898 修正START
                If iSkipFlg = 1 OrElse iAkakuroFlg = 1 Then
                    'If iSkipFlg = 1 Then
                    '(2013.02.25)要望番号1898 修正END
                    '    setDtlDt.Rows(0).Item("DEL_KB") = "0"
                    'Else
                    setDtlDt.Rows(0).Item("DEL_KB") = "1"
                End If

            End If

            setDs = MyBase.CallDAC(Me._Dac, "InsertInOutkaEdiRcvDtl", setDs)
            iRcvDtlInsCnt = iRcvDtlInsCnt + 1

            If bSameKeyFlg = False Then
                ' EDI受信データ(HED)の新規追加

                '削除区分が"3"(保留)の場合は区分値を入れ替えない
                If setHedDt.Rows(0).Item("DEL_KB").Equals("3") = False Then

                    'iSkipFlgを削除区分の値として使用する
                    '(2013.02.25)要望番号1898 修正START
                    If iSkipFlg = 0 AndAlso iAkakuroFlg = 0 Then
                        'If iSkipFlg = 0 Then
                        setHedDt.Rows(0).Item("DEL_KB") = "0"
                        '(2013.02.25)要望番号1898 修正END
                    Else
                        setHedDt.Rows(0).Item("DEL_KB") = "1"
                    End If

                End If

                setDs = MyBase.CallDAC(Me._Dac, "InsertInOutkaEdiRcvHed", setDs)
                iRcvHedInsCnt = iRcvHedInsCnt + 1
            End If

            '---------------------------------------------------------------------------
            ' キャンセルフラグが0かつスキップフラグが0の場合、
            ' INOUT_KB = '0'の場合、EDI出荷データの追加処理を行う
            ' INOUT_KB = '1'の場合、EDI入荷データの追加処理を行う
            '---------------------------------------------------------------------------

            Dim setDsLMH010 As DataSet = New LMH010DS()

            '(2013.02.25)要望番号1898 修正START
            If (iCancelFlg = 0 AndAlso iSkipFlg = 0 AndAlso iAkakuroFlg = 0) OrElse _
               (iCancelFlg = 1 AndAlso iSkipFlg = 0 AndAlso iAkakuroFlg = 0) Then
                'If iCancelFlg = 0 AndAlso iSkipFlg = 0 Then
                '(2013.02.25)要望番号1898 修正END

                If setDtlDt.Rows(0).Item("INOUT_KB").ToString().Equals("0") = True Then

                    '受信DTL⇒EDI出荷(中)へのデータセット(上記で取得した商品情報も含む)
                    setDs = Me.SetSemiOutkaEdiM(setDs, sSerchKey, sNrsGoodsCd, sNrsGoodsNm, sIrime)

                    'EDI出荷(中)の新規追加
                    setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiM", setDs)
                    iOutDtlInsCnt = iOutDtlInsCnt + 1

                ElseIf setDtlDt.Rows(0).Item("INOUT_KB").ToString().Equals("1") = True Then

                    '受信DTL⇒EDI入荷(中)へのデータセット(上記で取得した商品情報も含む)
                    setDsLMH010 = Me.SetSemiInkaEdiM(setDs, setDsLMH010, sSerchKey, sNrsGoodsCd, sNrsGoodsNm, sIrime)

                    'EDI入荷(中)の新規追加
                    setDsLMH010 = MyBase.CallDAC(Me._DacInka, "InsertInkaEdiM", setDsLMH010)
                    iInDtlInsCnt = iInDtlInsCnt + 1

                End If

                '前行と差異がある場合は、EDI出荷(大)を新規追加
                If bSameKeyFlg = False Then

                    If setHedDt.Rows(0).Item("INOUT_KB").ToString().Equals("0") = True Then
                        '受信DTL⇒EDI出荷(大)へのデータセット
                        setDs = Me.SetSemiOutkaEdiL(setDs)

                        'EDI出荷(大)の新規追加
                        setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiL", setDs)
                        iOutHedInsCnt = iOutHedInsCnt + 1

                    ElseIf setHedDt.Rows(0).Item("INOUT_KB").ToString().Equals("1") = True Then
                        '受信DTL⇒EDI入荷(大)へのデータセット
                        setDsLMH010 = Me.SetSemiInkaEdiL(setDs, setDsLMH010)

                        'EDI入荷(大)の新規追加
                        setDsLMH010 = MyBase.CallDAC(Me._DacInka, "InsertInkaEdiL", setDsLMH010)
                        iInHedInsCnt = iInHedInsCnt + 1

                    End If

                End If

            End If

            'キーを入れ替えるのはiSkipFlgの値で判断する
            '※iSkipFlg = 1の場合、sOldKeyは前行の値である必要があるため
            '(2013.02.25)要望番号1898 修正START
            If iSkipFlg = 0 OrElse i = 0 Then
                'If iSkipFlg = 0 Then
                '(2013.02.25)要望番号1898 修正END
                sOldKey = sNewKey   'OldキーにNewキーをセット

            End If

        Next

        If bNoErr Then
            'エラー無し
            dtSetHed.Rows(0).Item("ERR_FLG") = "0"
        Else
            'エラー有り
            dtSetHed.Rows(0).Item("ERR_FLG") = "1"
        End If

        '処理件数
        dtSetRet.Rows(0).Item("RCV_HED_INS_CNT") = iRcvHedInsCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_INS_CNT") = iRcvDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_HED_INS_CNT") = iOutHedInsCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_DTL_INS_CNT") = iOutDtlInsCnt.ToString()
        'dtSetRet.Rows(0).Item("IN_HED_INS_CNT") = iInHedInsCnt.ToString()
        'dtSetRet.Rows(0).Item("IN_DTL_INS_CNT") = iInDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_HED_CAN_CNT") = iRcvHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_CAN_CNT") = iRcvDtlCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_HED_CAN_CNT") = iOutHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_DTL_CAN_CNT") = iOutDtlCanCnt.ToString()
        'dtSetRet.Rows(0).Item("IN_HED_CAN_CNT") = iInHedCanCnt.ToString()
        'dtSetRet.Rows(0).Item("IN_DTL_CAN_CNT") = iInDtlCanCnt.ToString()

        Return ds

    End Function

#End Region

#Region "土・日・休日チェック"
    ''' <summary>
    ''' 土・日・休日チェック(営業日の抽出)
    ''' </summary>
    ''' <param name="sWorkDay"></param>
    ''' <param name="max"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBussinessDay(ByVal sWorkDay As String, ByVal max As Integer, ByVal setDs As DataSet) As DateTime

        '2012.03.23
        Dim holDr As DataRow
        Dim sWorkDayDate As DateTime = Convert.ToDateTime(Me._Blc.GetSlashEditDate(sWorkDay))

        '土・日は営業日として認めない
        For i As Integer = 1 To max

            Do
                sWorkDayDate = sWorkDayDate.AddDays(1)

                If Weekday(sWorkDayDate) = 1 OrElse Weekday(sWorkDayDate) = 7 Then
                Else
                    setDs.Tables("LMH030_M_HOL").Clear()

                    '休日マスタ参照
                    holDr = setDs.Tables("LMH030_M_HOL").NewRow()
                    holDr("HOL") = Format(sWorkDayDate, "yyyyMMdd")
                    'データセットに設定
                    setDs.Tables("LMH030_M_HOL").Rows.Add(holDr)

                    '休日マスタの値を取得
                    '休日マスタに存在する場合は、翌日で抽出(抽出できなくなるまで回す)
                    setDs = MyBase.CallDAC(Me._DacCom, "SelectMHolList", setDs)

                    If MyBase.GetResultCount = 0 Then '休日マスタに存在しない
                        'sWorkDayが求める日
                        Exit Do
                    End If

                End If
            Loop
        Next

        Return sWorkDayDate

    End Function

#End Region

#End Region

End Class
