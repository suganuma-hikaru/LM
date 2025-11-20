' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID　　　　:  110　　　 : ロンザ(千葉) コピー元102（ビックケミー） 
'  作  成  者       :  honmyo
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC110
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Structure"
    '要望番号:1861（ロンザ　EDI取込　子セット品が複数行の場合、エラーとなる）対応　 2013/02/15 本明Start
    Structure GoodsSet
        Dim KO_CD As String
        Dim SET_KOSU As Integer
    End Structure
    '要望番号:1861（ロンザ　EDI取込　子セット品が複数行の場合、エラーとなる）対応　 2013/02/15 本明Start
#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC110 = New LMH030DAC110()

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

#Region "ロンザ用CONST"

    ''' <summary>
    ''' ロンザ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const WH_CD_LNZ As String = "101"                '倉庫コード
    Public Const CUST_CD_L_LNZ As String = "00182"          '荷主コード（大）
    Public Const CUST_CD_M_LNZ As String = "00"             '荷主コード（中）

    Public Const NRS_GOODS_CD_UNSO As String = "C0000999999999999999"           '商品コード（運送）
    Public Const IRIME_UNSO As String = "0"                 '入目値（運送）

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

        ' ''オーダー重複チェック【ロンザ専用】
        ''If Me.CustOrderNoCheck(ds, rowNo, ediCtlNo) = False Then
        ''    Return ds
        ''End If
        'ディック（大阪）対応 20120315 End

        Dim autoMatomeF As String = ds.Tables("LMH030INOUT").Rows(0)("AUTO_MATOME_FLG").ToString()
        Dim matomeNo As String = String.Empty
        Dim matomeFlg As Boolean = False
        Dim UnsoMatomeFlg As Boolean = False


        '追加箇所 20110824 start
        'ロンザはまとめ対象荷主(まとめFLAG = "7"を使用)
        '基本、自動まとめフラグは"1"なのでここには入らない
        '自動まとめフラグ = "0" or "1"の場合、まとめ処理
        If autoMatomeF.Equals("0") OrElse autoMatomeF.Equals("1") Then

            'まとめ先取得
            ds = MyBase.CallDAC(Me._DacCom, "SelectMatomeTarget", ds)

            If MyBase.GetResultCount = 0 Then
                'まとめ先が無い場合、通常登録
                matomeFlg = False

            ElseIf MyBase.GetResultCount > 1 Then
                choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.LNZ_WID_L001, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    'まとめ対象だったデータを出したい場合はコメントをはずす
                    'Dim matomeTargetNo As String = Me.matomesakiOutkaNo(ds)
                    msgArray(1) = "出荷管理番号(大)"
                    msgArray(2) = "出荷"
                    msgArray(3) = "注意)進捗区分が同一の場合は、管理番号が若い方にまとまります。"
                    msgArray(4) = String.Empty
                    matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                    ds = Me._Blc.SetComWarningL("W199", LMH030BLC.LNZ_WID_L001, ds, msgArray, matomeNo, String.Empty)
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

                choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.LNZ_WID_L002, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    msgArray(1) = "出荷管理番号(大)"
                    msgArray(2) = String.Empty
                    msgArray(3) = String.Empty
                    msgArray(4) = String.Empty
                    matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                    ds = Me._Blc.SetComWarningL("W168", LMH030BLC.LNZ_WID_L002, ds, msgArray, matomeNo, String.Empty)
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

                    choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.LNZ_WID_L003, 0)

                    '進捗区分が予定入力より先になっているのでワーニングを出力
                    If String.IsNullOrEmpty(choiceKb) = True Then
                        msgArray(1) = "出荷管理番号(大)"
                        msgArray(2) = "出荷"
                        msgArray(3) = String.Empty
                        msgArray(4) = String.Empty
                        matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                        ds = Me._Blc.SetComWarningL("W198", LMH030BLC.LNZ_WID_L003, ds, msgArray, matomeNo, String.Empty)
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
        ds = Me.SetDatasetOutkaM(ds)

        'EDI受信テーブル(DTL)データセット設定
        ds = Me.SetDatasetEdiRcvDtl(ds)

        '要望番号1902 ロンザ専用作業付与START
        '作業レコードデータセット設定
        'ds = Me.SetDatasetSagyo(ds)
        If Me.SetDatasetSagyoLonza(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        '要望番号1902 ロンザ専用作業付与END

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

        'ロンザの場合は自動追加・更新はしない
        'もし、仕様変更があればここのコメントを外し、届先整合性チェックのロジックを記載する
        ''届先マスタの自動追加
        'If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
        '       AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
        '    ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "1"
        '    ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)
        '    ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "0"
        'End If

        ''届先マスタの更新 20110821追加 start
        'If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
        '   AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_UPDATE_FLG").Equals("1") = True Then
        '    ds.Tables("LMH030_M_DEST").Rows(0).Item("UPDATE_TARGET_FLG") = "1"
        '    ds = MyBase.CallDAC(Me._Dac, "UpdateMDestData", ds)
        '    If MyBase.GetResultCount = 0 Then
        '        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '        Return ds
        '    End If
        '    ds.Tables("LMH030_M_DEST").Rows(0).Item("UPDATE_TARGET_FLG") = "0"
        'End If
        ''届先マスタの更新 20110821追加 end

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

        '不具合暫定対応 START
        '荷主名(大)
        If String.IsNullOrEmpty(ediDr("CUST_NM_L").ToString().Trim()) = True Then
            ediDr("CUST_NM_L") = ds.Tables("LMH030_M_CUST").Rows(0).Item("CUST_NM_L").ToString()
        End If

        '荷主名(中)
        If String.IsNullOrEmpty(ediDr("CUST_NM_M").ToString().Trim()) = True Then
            ediDr("CUST_NM_M") = ds.Tables("LMH030_M_CUST").Rows(0).Item("CUST_NM_M").ToString()
        End If
        '不具合暫定対応 END

        '荷送人名(大)
        If String.IsNullOrEmpty(ediDr("SHIP_CD_L").ToString().Trim()) = True Then
            ediDr("SHIP_NM_L") = ""

            '★★ロンザ特殊対応 START★★
            '得意先名が既に入っている場合はマスタの値に上書かない
        ElseIf String.IsNullOrEmpty(ediDr("SHIP_NM_L").ToString().Trim()) = True Then
            '★★ロンザ特殊対応 END★★

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
        'DACで値セットを行う
        'ds = MyBase.CallDAC(Me._DacCom, "SetTariffData", ds)

        'タリフ分類区分
        '運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        '割増タリフコード(割増運賃タリフマスタ)
        'DACで値セットを行う
        '2012.03.06 大阪対応 START
        '(三井化学：EDIの時点で値が入っててもタリフMに存在しないケースがある為の対応)
        '①荷主明細マスタの存在チェック(荷主明細マスタに存在していれば入替えOK)
        '荷主明細マスタの取得
        ds = MyBase.CallDAC(Me._DacCom, "SelectListDataMcustDetails", ds)
        'タリフセットマスタの取得(運賃タリフ)
        ds = MyBase.CallDAC(Me._DacCom, "SetTariffData", ds)
        '2012.03.06 大阪対応 END

        '2012.03.06 大阪対応 START
        'タリフセットマスタの取得(割増タリフ)
        ds = MyBase.CallDAC(Me._DacCom, "SetExtcTariffData", ds)
        '2012.03.06 大阪対応 END


        '配送時注意事項
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = True Then
        Else
            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("DELI_ATT").ToString().Trim()) = False Then

                    If String.IsNullOrEmpty(ediDr("UNSO_ATT").ToString().Trim()) = True Then

                        ediDr("UNSO_ATT") = mDestDr("DELI_ATT").ToString().Trim()
                    ElseIf InStr(ediDr("UNSO_ATT").ToString().Trim(), mDestDr("DELI_ATT").ToString().Trim()) > 0 Then
                    Else
                        '2012.03.02 大阪対応START
                        ediDr("UNSO_ATT") = Me._Blc.LeftB(String.Concat(ediDr("UNSO_ATT").ToString() & Strings.Space(2), mDestDr("DELI_ATT").ToString().Trim()), 100)
                        '2012.03.02 大阪対応END
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

        '追加箇所 20110822
        '運賃請求有無
        If (ediDr("UNSO_MOTO_KB").ToString()).Equals("10") = True OrElse _
           (ediDr("UNSO_MOTO_KB").ToString()).Equals("40") = True Then
            ediDr("UNCHIN_YN") = "1"
        Else
            ediDr("UNCHIN_YN") = "0"
        End If
        '追加箇所 20110822

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
        ''●荷主固有チェック(ロンザ専用)
        ''-------------------------------------------------------------------------------------

        Dim flgWarning As Boolean = False
        Dim compareWarningFlg As Boolean = False
        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        ' ''①商品名
        ''Dim mGoodsNm As String = Replace(Replace(mGoodsDr("GOODS_NM_1").ToString(), "ｰ", "-"), Space(1), "").Trim()
        ''Dim ediGoodsNm As String = Replace(Replace(ediMDr("GOODS_NM").ToString(), "ｰ", "-"), Space(1), "").Trim()

        ''If mGoodsNm.Equals(ediGoodsNm) = True Then
        ''    'チェックなし
        ''    ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1").ToString()

        ''ElseIf mGoodsNm.Equals(ediGoodsNm) = False Then

        ''    choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.LNZ_WID_M002, count)

        ''    If String.IsNullOrEmpty(choiceKb) = True Then
        ''        msgArray(1) = "商品名"
        ''        msgArray(2) = "商品マスタ"
        ''        msgArray(3) = "商品名"
        ''        msgArray(4) = String.Empty
        ''        msgArray(5) = String.Empty
        ''        ds = Me._Blc.SetComWarningM("W194", LMH030BLC.LNZ_WID_M002, ds, setDs, msgArray, _
        ''                    ediMDr("GOODS_NM").ToString(), mGoodsDr("GOODS_NM_1").ToString())

        ''        compareWarningFlg = True

        ''    ElseIf choiceKb.Equals("01") = True Then
        ''        'ワーニングで"はい"を選択時
        ''        ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1").ToString()

        ''    End If

        ''End If

        '②入目
        Dim ediIrime As String = ediMDr("IRIME").ToString()
        Dim mIrime As String = mGoodsDr("STD_IRIME_NB").ToString()

        If (ediIrime).Equals(mIrime) = False Then

            'セミEDI時は入目は全て０で登録される。EDI出荷編集で修正された時のみ、不整合でワーニング。
            If Convert.ToDecimal(ediIrime) = 0 Then

                ediMDr("IRIME") = mIrime

            Else

                choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.LNZ_WID_M003, count)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    msgArray(1) = "入目"
                    msgArray(2) = "商品マスタ"
                    msgArray(3) = "入目"
                    msgArray(4) = "注意) 出荷処理にて修正して下さい。"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningM("W194", LMH030BLC.LNZ_WID_M003, ds, setDs, msgArray, ediIrime, mIrime)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    ediMDr("IRIME") = mIrime

                End If

            End If

        End If

        '③入目単位
        Dim mIRIMENm As String = mGoodsDr("STD_IRIME_UT").ToString()
        Dim ediIRIMENm As String = ediMDr("IRIME_UT").ToString()

        If String.IsNullOrEmpty(ediIRIMENm) = True Then
            'チェックなし
        Else
            If mIRIMENm.Equals(ediIRIMENm) = True Then
                'チェックなし
            Else
                choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.LNZ_WID_M004, count)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    msgArray(1) = "入目単位"
                    msgArray(2) = "商品マスタ"
                    msgArray(3) = "入目単位"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningM("W159", LMH030BLC.LNZ_WID_M004, ds, setDs, msgArray, ediIRIMENm, mIRIMENm)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    ediMDr("IRIME_UT") = mIRIMENm

                End If
            End If
        End If

        '④備考
        ediMDr("REMARK") = String.Concat(mGoodsDr("OUTKA_ATT").ToString(), ediMDr("REMARK").ToString())

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

        '商品マスタの宅急便サイズ区分をFREE_C14にセットする
        ediMDr("FREE_C14") = mGoodsDr("SIZE_KB")

        '2013.03.25 要望番号1902 ロンザ専用作業付与START
        '商品マスタの毒劇区分をFREE_C15にセットする
        ediMDr("FREE_C15") = mGoodsDr("DOKU_KB")
        '2013.03.25 要望番号1902 ロンザ専用作業付与END

        '2012.03.01 大阪対応START
        'ワーニングが存在する場合はここでの判定はFalseで返す
        '(ロンザはワーニング設定有)
        If compareWarningFlg = True Then
            Return False
        End If
        '2012.03.01 大阪対応END

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

        Dim flgWarning As Boolean = False

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        '-------------------------------------------------------------------------------------
        '●荷主共通チェック
        '-------------------------------------------------------------------------------------
        'オーダー番号重複チェック
        If String.IsNullOrEmpty(drEdiL.Item("CUST_ORD_NO").ToString) = False Then
            If drIn("ORDER_CHECK_FLG").Equals("1") = True Then

                'ロンザの場合は同一タイミングの出荷のみオーダー重複を許す
                'Call MyBase.CallDAC(Me._DacCom, "SelectOrderCheckData", ds)
                Call MyBase.CallDAC(Me._Dac, "SelectOrderCheckDataLNZ", ds)
                If MyBase.GetResultCount > 0 Then
                    '同一オーダーがある場合はワーニング

                    choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.LNZ_WID_L014, 0)

                    If String.IsNullOrEmpty(choiceKb) = True Then
                        msgArray(1) = String.Empty
                        msgArray(2) = String.Empty
                        msgArray(3) = String.Empty
                        msgArray(4) = String.Empty
                        ds = Me._Blc.SetComWarningL("W230", LMH030BLC.LNZ_WID_L014, ds, msgArray, String.Empty, String.Empty)

                        flgWarning = True

                    ElseIf choiceKb.Equals("01") = True Then
                        'ワーニングで"はい"を選択時

                    End If
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"出荷データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'Return False
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
        '●荷主固有チェック(ロンザ専用)
        '-------------------------------------------------------------------------------------

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
            'DEST_CDとEDI_DEST_CDが両方空の場合、エラーとする。
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"届先(EDI)コードが空", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Dim mDestCount As Integer = ds.Tables("LMH030_M_DEST").Rows.Count

        If mDestCount = 1 Then
            '1件に特定できた場合、マスタ値とEDI出荷(大)の整合性チェック
            'ロンザの場合、不整合の場合はワーニング(マスタの値に入替える)
            'セミEDI時点での届先Ｍ情報と出荷登録時の届先Ｍ情報がズレがないかのチェック
            If Me.DestCompareCheck(ds, rowNo, ediCtlNo) = False Then

                Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                    '整合性チェックでエラーがあった場合は処理終了
                    Return False
                End If
            End If

        ElseIf mDestCount = 0 Then
            'ロンザの場合、0件の場合はエラー
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False

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

                choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.LNZ_WID_M001, 0)

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

                        '2012.03.19 修正START
                        ds = Me._Blc.SetComWarningM("W162", LMH030BLC.LNZ_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)
                        '2012.03.19 修正END

                        flgWarning = True 'ワーニングフラグをたてて処理続行

                        Continue For
                    End If

                End If

                'EDI出荷(中)の初期値設定処理
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                    'ロンザは現段階ではワーニングが存在するが、エラーはなし。共通のロジックを組み込む為入れておく
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

    '#Region "オーダーNO重複チェック(明細単位)"

    '    ''' <summary>
    '    ''' オーダーNO重複チェック
    '    ''' </summary>
    '    ''' <param name="ds"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function CustOrderNoCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

    '        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
    '        Dim dtEdiM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
    '        Dim drIn As DataRow = ds.Tables("LMH030INOUT").Rows(0)
    '        Dim edimCount As Integer = dtEdiM.Rows.Count - 1

    '        Dim setDs As DataSet = ds.Copy
    '        Dim setDtEdiL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")
    '        Dim setDtEdiM As DataTable = setDs.Tables("LMH030_OUTKAEDI_M")

    '        For i As Integer = 0 To edimCount

    '            '値のクリア
    '            setDs.Clear()

    '            setDtEdiL.ImportRow(drEdiL)
    '            setDtEdiM.ImportRow(dtEdiM.Rows(i))

    '            'オーダー番号重複チェック処理(明細単位)
    '            If String.IsNullOrEmpty(drEdiL.Item("CUST_ORD_NO").ToString()) = False Then
    '                If drIn("ORDER_CHECK_FLG").Equals("1") = True Then
    '                    Call MyBase.CallDAC(Me._Dac, "SelectOrderCheckDataLNZ", setDs)
    '                    If MyBase.GetResultCount > 0 Then
    '                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"出荷データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
    '                        Return False
    '                    End If

    '                End If
    '            End If
    '        Next

    '        Return True

    '    End Function


    '#End Region

#Region "届先マスタチェック(ロンザ専用)"
    ''' <summary>
    ''' マスタ値とEDI出荷(大)の整合性チェック（削除フラグのみ）
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
        Dim ediAd1 As String = dtEdi.Rows(0)("DEST_AD_1").ToString()
        Dim ediAd2 As String = dtEdi.Rows(0)("DEST_AD_2").ToString()
        Dim ediAd3 As String = dtEdi.Rows(0)("DEST_AD_3").ToString()
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()
        Dim ediAdAll As String = String.Concat(ediAd1, ediAd2, ediAd3)

        Dim compareWarningFlg As Boolean = False

        '削除フラグ(届先マスタ)
        If mSysDelF.Equals("1") = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E331", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '!!!ロンザは届先マスタの整合性チェックは行わない。!!!

        'mDestNm = Me.SpaceCutChk(mDestNm)
        'ediDestNm = Me.SpaceCutChk(ediDestNm)

        ''届先名称(マスタ値が完全一致でなければワーニング)
        'If mDestNm.Equals(ediDestNm) = True Then
        '    'チェックなし
        'Else
        '    choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.LNZ_WID_L003, 0)

        '    If String.IsNullOrEmpty(choiceKb) = True Then
        '        msgArray(1) = "届先名称"
        '        msgArray(2) = "届先マスタ"
        '        msgArray(3) = "届先名称"
        '        msgArray(4) = "EDIデータ"
        '        msgArray(5) = String.Empty

        '        ds = Me._Blc.SetComWarningL("W159", LMH030BLC.LNZ_WID_L003, ds, msgArray, _
        '                                    dtEdi.Rows(0)("DEST_NM").ToString(), dtMdest.Rows(0).Item("DEST_NM").ToString())

        '        compareWarningFlg = True

        '    ElseIf choiceKb.Equals("01") = True Then
        '        'ワーニングで"はい"を選択時
        '        dtEdi.Rows(0)("DEST_NM") = dtMdest.Rows(0).Item("DEST_NM").ToString()

        '    End If

        'End If

        ''届先住所(マスタ値が完全一致でなければワーニング)
        'If String.IsNullOrEmpty(ediAdAll) = True Then
        '    'チェックなし
        'Else

        '    mAdAll = SpaceCutChk(mAdAll)
        '    ediAdAll = SpaceCutChk(ediAdAll)
        '    If mAdAll.Equals(ediDestNm) = False Then

        '        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.LNZ_WID_L004, 0)

        '        If String.IsNullOrEmpty(choiceKb) = True Then

        '            msgArray(1) = "届先住所"
        '            msgArray(2) = "届先マスタ"
        '            msgArray(3) = "住所"
        '            msgArray(4) = "EDIデータ"
        '            msgArray(5) = String.Empty
        '            ds = Me._Blc.SetComWarningL("W159", LMH030BLC.LNZ_WID_L004, ds, msgArray, ediAdAll, mAdAll)

        '            compareWarningFlg = True

        '        ElseIf choiceKb.Equals("01") = True Then
        '            'ワーニングで"はい"を選択時
        '            dtEdi.Rows(0)("DEST_AD_1") = dtMdest.Rows(0).Item("AD_1").ToString()
        '            dtEdi.Rows(0)("DEST_AD_2") = dtMdest.Rows(0).Item("AD_2").ToString()
        '            dtEdi.Rows(0)("DEST_AD_3") = dtMdest.Rows(0).Item("AD_3").ToString()

        '        End If
        '    End If

        'End If

        ''届先電話番号(マスタ値が完全一致でなければワーニング)
        'If String.IsNullOrEmpty(ediTel) = True Then
        '    'チェックなし
        'Else
        '    If mTel.Equals(ediTel) = False Then

        '        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.LNZ_WID_L005, 0)

        '        If String.IsNullOrEmpty(choiceKb) = True Then

        '            msgArray(1) = "届先電話番号"
        '            msgArray(2) = "届先マスタ"
        '            msgArray(3) = "電話番号"
        '            msgArray(4) = "EDIデータ"
        '            msgArray(5) = String.Empty
        '            ds = Me._Blc.SetComWarningL("W159", LMH030BLC.LNZ_WID_L005, ds, msgArray, ediTel, mTel)

        '            compareWarningFlg = True

        '        ElseIf choiceKb.Equals("01") = True Then
        '            'ワーニングで"はい"を選択時
        '            dtEdi.Rows(0)("DEST_ZIP") = dtMdest.Rows(0).Item("ZIP").ToString()

        '        End If

        '    End If

        'End If

        ''届先郵便番号(マスタ値が完全一致でなければワーニング)
        'If String.IsNullOrEmpty(ediZip) = True Then
        '    'チェックなし
        'Else
        '    If mZip.Equals(ediZip) = False Then

        '        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.LNZ_WID_L006, 0)

        '        If String.IsNullOrEmpty(choiceKb) = True Then

        '            msgArray(1) = "届先郵便番号"
        '            msgArray(2) = "届先マスタ"
        '            msgArray(3) = "郵便番号"
        '            msgArray(4) = "EDIデータ"
        '            msgArray(5) = String.Empty
        '            ds = Me._Blc.SetComWarningL("W159", LMH030BLC.LNZ_WID_L006, ds, msgArray, ediZip, mZip)

        '            compareWarningFlg = True

        '        ElseIf choiceKb.Equals("01") = True Then
        '            'ワーニングで"はい"を選択時
        '            dtEdi.Rows(0)("DEST_TEL") = dtMdest.Rows(0).Item("TEL").ToString()

        '        End If

        '    End If

        'End If

        ''届先JISコード(マスタ値が完全一致でなければワーニング)
        'If String.IsNullOrEmpty(ediDestJisCd) = True Then
        '    'チェックなし
        'Else
        '    If mJis.Equals(ediDestJisCd) = False Then

        '        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.LNZ_WID_L007, 0)

        '        If String.IsNullOrEmpty(choiceKb) = True Then

        '            msgArray(1) = "届先JISコード"
        '            msgArray(2) = "届先マスタ"
        '            msgArray(3) = "JISコード"
        '            msgArray(4) = "EDIデータ"
        '            msgArray(5) = String.Empty
        '            ds = Me._Blc.SetComWarningL("W159", LMH030BLC.LNZ_WID_L007, ds, msgArray, ediDestJisCd, mJis)

        '            compareWarningFlg = True

        '        ElseIf choiceKb.Equals("01") = True Then
        '            'ワーニングで"はい"を選択時
        '            dtEdi.Rows(0)("DEST_JIS_CD") = dtMdest.Rows(0).Item("JIS").ToString()

        '        End If

        '    End If

        'End If

        'ワーニングが存在する場合はここでの判定はFalseで返す
        If compareWarningFlg = True Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "SPACE除去 + 文字変換"
    ''' <summary>
    ''' SPACE除去 + 文字変換
    ''' </summary>
    ''' <param name="chkFld"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SpaceCutChk(ByVal chkFld As String) As String

        chkFld = Replace(Trim(chkFld), Space(1), String.Empty)
        chkFld = Replace(chkFld, "　", String.Empty)
        chkFld = StrConv(chkFld, VbStrConv.Wide)

        Return chkFld

    End Function

#End Region

#Region "Null変換"
    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertString(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = String.Empty
        End If

        Return value

    End Function

    ''' <summary>
    ''' Null変換（数値）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertZero(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = 0
        End If

        Return value

    End Function
#End Region

#Region "左埋処理"
    ''' <summary>
    ''' 0埋処理
    ''' </summary>
    ''' <param name="val">対象文字列</param>
    ''' <param name="keta">0埋後の桁数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormatZero(ByVal val As String, ByVal keta As Integer) As String

        val = val.PadLeft(keta, "0"c)

        Return val

    End Function

    ''' <summary>
    ''' スペース埋処理
    ''' </summary>
    ''' <param name="val"></param>
    ''' <param name="keta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormatSpace(ByVal val As String, ByVal keta As Integer) As String

        val = val.PadLeft(keta)

        Return val

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
            '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo Start
            'Dim maxOutkaKanriNo As Integer = Convert.ToInt32(ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("OUTKA_CTL_NO_CHU"))
            Dim maxOutkaKanriNo As Integer = Me._DacCom.GetMaxOUTKA_NO_CHU(ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("NRS_BR_CD").ToString, ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("OUTKA_CTL_NO").ToString)
            '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo End
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
            outkaDr("COA_YN") = FormatZero(ediDr("COA_YN").ToString(), 2)
            outkaDr("CUST_ORD_NO") = ediDr("CUST_ORD_NO")
            outkaDr("BUYER_ORD_NO") = ediDr("BUYER_ORD_NO")
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
            outkaDr("DEST_KB") = "00"   '届先区分は届先を使用　
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
    Private Function SetDatasetOutkaM(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim outkaDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim remark As String = String.Empty
        Dim SetNo As String = String.Empty
        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim max As Integer = dt.Rows.Count - 1
        Dim calcPkgModNb As Long = 0
        Dim calcPkgQuoNb As Double = 0


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
            outkaDr("CUST_ORD_NO_DTL") = ediDr("CUST_ORD_NO_DTL")
            outkaDr("BUYER_ORD_NO_DTL") = ediDr("BUYER_ORD_NO_DTL")
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

            'ロンザの場合はEDI出荷(中)FREE_C14をセットする
            outkaDr("SIZE_KB") = ediDr("FREE_C14")

            'REMARK情報はセットしない。使用する時はEDIのFREE_C01を取得する
            outkaDr("ZAIKO_KB") = String.Empty
            'outkaDr("ZAIKO_KB") = Me._Blc.LeftB(ediDr("FREE_C01").ToString, 10)     'DBが10Byteなのでとりあえず10Byte   

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

            'データセットに設定
            ds.Tables("LMH030_EDI_RCV_DTL").Rows.Add(rcvDr)

        Next

        Return ds

    End Function

#End Region

    '#Region "データセット設定(作業)"
    '    ''' <summary>
    '    ''' データセット設定(作業)
    '    ''' </summary>
    '    ''' <param name="ds"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function SetDatasetSagyo(ByVal ds As DataSet) As DataSet

    '        Dim ediDrM As DataRow
    '        Dim sagyoDr As DataRow
    '        Dim ediDrL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
    '        Dim max As Integer = ds.Tables("LMH030_OUTKAEDI_M").Rows.Count - 1
    '        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
    '        Dim sagyoCD As String = String.Empty
    '        Dim outkaNoLM As String = String.Empty
    '        Dim num As New NumberMasterUtility

    '        For i As Integer = 0 To max

    '            ediDrM = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)

    '            For j As Integer = 1 To 5

    '                sagyoCD = ediDrM("OUTKA_KAKO_SAGYO_KB_" & j).ToString()

    '                If String.IsNullOrEmpty(sagyoCD) = False Then

    '                    outkaNoLM = String.Concat(ediDrM("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))

    '                    sagyoDr = ds.Tables("LMH030_E_SAGYO").NewRow()

    '                    sagyoDr("SAGYO_COMP") = "00"
    '                    sagyoDr("SKYU_CHK") = "00"
    '                    sagyoDr("SAGYO_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, nrsBrCd)
    '                    sagyoDr("SAGYO_SIJI_NO") = String.Empty
    '                    sagyoDr("INOUTKA_NO_LM") = outkaNoLM
    '                    sagyoDr("NRS_BR_CD") = ediDrL("NRS_BR_CD")
    '                    sagyoDr("WH_CD") = ediDrL("WH_CD")
    '                    sagyoDr("IOZS_KB") = "21"
    '                    sagyoDr("SAGYO_CD") = sagyoCD
    '                    sagyoDr("CUST_CD_L") = ediDrL("CUST_CD_L")
    '                    sagyoDr("CUST_CD_M") = ediDrL("CUST_CD_M")
    '                    sagyoDr("DEST_CD") = ediDrL("DEST_CD")
    '                    sagyoDr("DEST_NM") = ediDrL("DEST_NM")
    '                    sagyoDr("GOODS_CD_NRS") = ediDrM("NRS_GOODS_CD")
    '                    sagyoDr("GOODS_NM_NRS") = ediDrM("GOODS_NM")
    '                    sagyoDr("LOT_NO") = ediDrM("LOT_NO")
    '                    sagyoDr("SAGYO_NB") = 0
    '                    sagyoDr("SAGYO_GK") = 0
    '                    sagyoDr("REMARK_SKYU") = ediDrM("REMARK")
    '                    sagyoDr("SAGYO_COMP_CD") = String.Empty
    '                    sagyoDr("SAGYO_COMP_DATE") = String.Empty
    '                    sagyoDr("DEST_SAGYO_FLG") = "00"
    '                    sagyoDr("SYS_DEL_FLG") = "0"

    '                    'データセットに設定
    '                    ds.Tables("LMH030_E_SAGYO").Rows.Add(sagyoDr)
    '                End If
    '            Next

    '            For k As Integer = 1 To 2

    '                sagyoCD = ediDrM("SAGYO_KB_" & k).ToString()

    '                If String.IsNullOrEmpty(sagyoCD) = False Then

    '                    outkaNoLM = String.Concat(ediDrM("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))

    '                    sagyoDr = ds.Tables("LMH030_E_SAGYO").NewRow()

    '                    sagyoDr("SAGYO_COMP") = "00"
    '                    sagyoDr("SKYU_CHK") = "00"
    '                    sagyoDr("SAGYO_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, nrsBrCd)
    '                    sagyoDr("SAGYO_SIJI_NO") = String.Empty
    '                    sagyoDr("INOUTKA_NO_LM") = outkaNoLM
    '                    sagyoDr("NRS_BR_CD") = ediDrL("NRS_BR_CD")
    '                    sagyoDr("WH_CD") = ediDrL("WH_CD")
    '                    sagyoDr("IOZS_KB") = "21"
    '                    sagyoDr("SAGYO_CD") = sagyoCD
    '                    sagyoDr("CUST_CD_L") = ediDrL("CUST_CD_L")
    '                    sagyoDr("CUST_CD_M") = ediDrL("CUST_CD_M")
    '                    sagyoDr("DEST_CD") = ediDrL("DEST_CD")
    '                    sagyoDr("DEST_NM") = ediDrL("DEST_NM")
    '                    sagyoDr("GOODS_CD_NRS") = ediDrM("NRS_GOODS_CD")
    '                    sagyoDr("GOODS_NM_NRS") = ediDrM("GOODS_NM")
    '                    sagyoDr("LOT_NO") = ediDrM("LOT_NO")
    '                    sagyoDr("SAGYO_NB") = 0
    '                    sagyoDr("SAGYO_GK") = 0
    '                    sagyoDr("REMARK_SKYU") = ediDrM("REMARK")
    '                    sagyoDr("SAGYO_COMP_CD") = String.Empty
    '                    sagyoDr("SAGYO_COMP_DATE") = String.Empty
    '                    sagyoDr("DEST_SAGYO_FLG") = "01"
    '                    sagyoDr("SYS_DEL_FLG") = "0"

    '                    'データセットに設定
    '                    ds.Tables("LMH030_E_SAGYO").Rows.Add(sagyoDr)
    '                End If
    '            Next
    '        Next

    '        Return ds

    '    End Function

    '#End Region

    '2013.03.25 要望番号1902 ロンザ専用作業付与START
#Region "データセット設定(作業):ロンザ専用"
    ''' <summary>
    ''' データセット設定(作業)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetSagyoLonza(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean
        'Private Function SetDatasetSagyoLonza(ByVal ds As DataSet) As DataSet

        Dim ediDrM As DataRow
        Dim sagyoDr As DataRow
        Dim ediDrL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim max As Integer = ds.Tables("LMH030_OUTKAEDI_M").Rows.Count - 1
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim sagyoCD As String = String.Empty
        Dim outkaNoLM As String = String.Empty
        Dim num As New NumberMasterUtility

        Dim doKuFlg As Integer = 0
        Dim destNM As String = String.Empty

        For i As Integer = 0 To max

            ediDrM = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)

            If String.IsNullOrEmpty(ediDrM.Item("FREE_C15").ToString()) = False AndAlso _
               ediDrM.Item("FREE_C15").ToString().Equals("01") = False Then
                doKuFlg = 1
                Exit For
            End If

        Next

        '①商品に対する作業料

        ds = MyBase.CallDAC(Me._Dac, "SelectMgoodsSagyoCd", ds)

        If MyBase.GetResultCount = 0 Then
            '商品明細マスタに作業項目コードが登録されていない場合はエラー
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E454", New String() {"商品明細マスタの作業項目コードが未設定", "出荷登録", _
                                                                                 String.Concat("商品名: ", ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("GOODS_NM").ToString())}, _
                                                                                 rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        sagyoCD = ds.Tables("LMH030_M_GOODS_DETAILS").Rows(0).Item("SET_NAIYO").ToString()

        If String.IsNullOrEmpty(sagyoCD) = False Then

            '作業マスタの取得
            '2013.04.5 要望番号2002 作業マスタ取得START
            ds = Me.SetSagyoCd(sagyoCD, ds, True)
            ds = MyBase.CallDAC(Me._Dac, "SelectMSagyo", ds)
            If MyBase.GetResultCount = 0 Then
                '作業マスタに作業項目コードが登録されていない場合はエラー
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E492", New String() {"作業項目マスタ", "作業項目コード", _
                                                                                     String.Concat("作業項目コード: ", sagyoCD)}, _
                                                                                    rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
            '2013.04.5 要望番号2002 作業マスタ取得End
            '届先マスタの取得
            '2013.05.1 要望番号2037 届先マスタ取得START
            ds = MyBase.CallDAC(Me._Dac, "SelectDataMdestOutkaToroku", ds)
            If MyBase.GetResultCount = 0 Then
                destNM = ediDrL("DEST_NM").ToString()
            Else
                destNM = ds.Tables("LMH030_M_DEST").Rows(0).Item("DEST_NM").ToString()
            End If
            '2013.05.1 要望番号2037 届先マスタ取得End

            outkaNoLM = String.Concat(ediDrL("OUTKA_CTL_NO"), ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("OUTKA_CTL_NO_CHU"))

            sagyoDr = ds.Tables("LMH030_E_SAGYO").NewRow()

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
            '2013.04.5 要望番号2002 作業マスタ取得Start
            'sagyoDr("DEST_NM") = ediDrL("DEST_NM")
            sagyoDr("DEST_NM") = destNM
            '2013.04.5 要望番号2002 作業マスタ取得End
            sagyoDr("GOODS_CD_NRS") = ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("NRS_GOODS_CD")
            sagyoDr("GOODS_NM_NRS") = ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("GOODS_NM")
            sagyoDr("LOT_NO") = ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("LOT_NO")
            sagyoDr("REMARK_SKYU") = ediDrL("CUST_ORD_NO")
            sagyoDr("SAGYO_COMP_CD") = String.Empty
            sagyoDr("SAGYO_COMP_DATE") = String.Empty
            sagyoDr("DEST_SAGYO_FLG") = "00"
            sagyoDr("SYS_DEL_FLG") = "0"

            sagyoDr("SAGYO_NB") = Me.GetSagyoNB(ds)
            sagyoDr("SAGYO_GK") = Me.GetSagyoGk(sagyoDr, ds)

            'データセットに設定
            ds.Tables("LMH030_E_SAGYO").Rows.Add(sagyoDr)
        End If

        '②毒劇に対する作業料

        If doKuFlg = 1 Then

            '2013.03.29ロンザ　タカラ対応 追加START
            ds = MyBase.CallDAC(Me._Dac, "SelectMdestDetailsNotSubject", ds)

            If MyBase.GetResultCount > 0 Then
                '届先明細マスタにロンザ毒劇品作業項目対象外フラグのレコードが存在する場合、毒劇の作業料を付与しない
                Return True
            End If
            '2013.03.29ロンザ　タカラ対応 追加END

            outkaNoLM = String.Concat(ediDrL("OUTKA_CTL_NO"), ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("OUTKA_CTL_NO_CHU"))

            sagyoCD = Me.GetLonzaDokuSagyoCd(ds, rowNo, ediCtlNo)

            '作業マスタの取得
            '2013.04.5 要望番号2002 作業マスタ取得START
            ds = Me.SetSagyoCd(sagyoCD, ds, True)
            ds = MyBase.CallDAC(Me._Dac, "SelectMSagyo", ds)
            If MyBase.GetResultCount = 0 Then
                '作業マスタに作業項目コードが登録されていない場合はエラー
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E492", New String() {"作業項目マスタ", "作業項目コード", _
                                                                                     String.Concat("作業項目コード: ", sagyoCD)}, _
                                                                                    rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
            '2013.04.5 要望番号2002 作業マスタ取得End
            '届先マスタの取得
            '2013.05.1 要望番号2037 届先マスタ取得START
            ds = MyBase.CallDAC(Me._Dac, "SelectDataMdestOutkaToroku", ds)
            If MyBase.GetResultCount = 0 Then
                destNM = ediDrL("DEST_NM").ToString()
            Else
                destNM = ds.Tables("LMH030_M_DEST").Rows(0).Item("DEST_NM").ToString()
            End If
            '2013.05.1 要望番号2037 届先マスタ取得End

            sagyoDr = ds.Tables("LMH030_E_SAGYO").NewRow()

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
            '2013.04.5 要望番号2002 作業マスタ取得START
            'sagyoDr("DEST_NM") = ediDrL("DEST_NM")
            sagyoDr("DEST_NM") = destNM
            '2013.04.5 要望番号2002 作業マスタ取得End
            sagyoDr("GOODS_CD_NRS") = ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("NRS_GOODS_CD")
            sagyoDr("GOODS_NM_NRS") = ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("GOODS_NM")
            sagyoDr("LOT_NO") = ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("LOT_NO")
            sagyoDr("REMARK_SKYU") = ediDrL("CUST_ORD_NO")
            sagyoDr("SAGYO_COMP_CD") = String.Empty
            sagyoDr("SAGYO_COMP_DATE") = String.Empty
            sagyoDr("DEST_SAGYO_FLG") = "00"
            sagyoDr("SYS_DEL_FLG") = "0"

            sagyoDr("SAGYO_NB") = Me.GetSagyoNB(ds)
            sagyoDr("SAGYO_GK") = Me.GetSagyoGk(sagyoDr, ds)

            'データセットに設定
            ds.Tables("LMH030_E_SAGYO").Rows.Add(sagyoDr)
        End If

        Return True
        'Return ds

    End Function

    ''' <summary>
    ''' 作用項目マスタ取得を行う作業コードをデータセットに設定
    ''' </summary>
    ''' <param name="sagyoCd"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSagyoCd(ByVal sagyoCd As String, ByVal ds As DataSet, Optional ByVal initFlg As Boolean = True) As DataSet

        If initFlg = True Then
            '初期化
            ds.Tables("LMH030_M_SAGYO_IN").Clear()
            ds.Tables("LMH030_M_SAGYO").Clear()
        End If

        Dim dr As DataRow = ds.Tables("LMH030_M_SAGYO_IN").NewRow
        dr("NRS_BR_CD") = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD")
        dr("SAGYO_CD") = sagyoCd

        ds.Tables("LMH030_M_SAGYO_IN").Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 作業個数の設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSagyoNB(ByVal ds As DataSet) As Decimal

        Dim mSagyo As DataRow = ds.Tables("LMH030_M_SAGYO").Rows(0)
        Dim sagyoNB As Integer = 0
        '
        If ("01").Equals(mSagyo.Item("KOSU_BAI").ToString) = True Then
            sagyoNB = 1
        ElseIf ("02").Equals(mSagyo.Item("KOSU_BAI").ToString) = True OrElse ("03").Equals(mSagyo.Item("KOSU_BAI").ToString) = True Then
            '総個数を設定
            If IsNumeric(ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("OUTKA_TTL_NB").ToString()) = True Then
                sagyoNB = Convert.ToInt32(ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("OUTKA_TTL_NB").ToString())
            End If
        End If
        If sagyoNB = 0 Then
            '０の場合、１を設定
            sagyoNB = 1
        End If

        Return sagyoNB

    End Function

    ''' <summary>
    ''' 作業金額取得
    ''' </summary>
    ''' <param name="sagyoDr"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSagyoGk(ByVal sagyoDr As DataRow, ByVal ds As DataSet) As Integer

        Dim mSagyoDr As DataRow = ds.Tables("LMH030_M_SAGYO").Rows(0)
        Dim sagyoGK As Integer = 0
        sagyoGK = Convert.ToInt32(Math.Round(Convert.ToDecimal(sagyoDr.Item("SAGYO_NB").ToString) * Convert.ToDecimal(mSagyoDr.Item("SAGYO_UP").ToString), MidpointRounding.AwayFromZero))

        Return sagyoGK

    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetLonzaDokuSagyoCd(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As String

        ds = MyBase.CallDAC(Me._Dac, "SelectDataZkbn", ds)

        If ds.Tables("LMH030_Z_KBN").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"(毒劇物対象の)作業項目コード", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return String.Empty
        End If

        Dim zkbnDr As DataRow = ds.Tables("LMH030_Z_KBN").Rows(0)

        Return zkbnDr("NISUGATA").ToString() '"C0879"

    End Function

#End Region
    '2013.03.25 要望番号1902 ロンザ専用作業付与END

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

            '要望番号2002 20130403 修正START
            'unsoMDr("SIZE_KB") = String.Empty
            unsoMDr("SIZE_KB") = ediDr("FREE_C14")
            '要望番号2002 20130322 修正END
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

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

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

        'Dim drEdiRcvHed As DataRow = ds.Tables("LMH030_OUTKAEDI_HED_LNZ").NewRow()
        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH030_OUTKAEDI_DTL_LNZ").NewRow()
        Dim drSetDtl As DataRow = ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i)

        'EDI受信HED設定
        'drEdiRcvHed("DEL_KB") = "0"                                                                 '削除区分
        'drEdiRcvHed("CRT_DATE") = MyBase.GetSystemDate()                                            'データ受信日
        'drEdiRcvHed("FILE_NAME") = drSetDtl("FILE_NAME_OPE")                                        '受信ファイル名
        'drEdiRcvHed("REC_NO") = Convert.ToInt32(drSetDtl("REC_NO")).ToString("00000")               '受信ファイル行数
        'drEdiRcvHed("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")                                       '営業所コード
        'drEdiRcvHed("EDI_CTL_NO") = String.Empty                                                    'ＥＤＩ管理番号
        'drEdiRcvHed("OUTKA_CTL_NO") = DEF_CTL_NO                                                    '出荷管理番号
        'drEdiRcvHed("CUST_CD_L") = String.Empty                                                     '荷主コード（大）
        'drEdiRcvHed("CUST_CD_M") = String.Empty                                                     '荷主コード（中）
        'drEdiRcvHed("PRTFLG") = "0"                                                                 'プリントフラグ
        'drEdiRcvHed("CANCEL_FLG") = String.Empty                                                    'キャンセルフラグ

        'drEdiRcvHed("HAKKO_KB_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_1").ToString().Trim(), 6)       '発行区分名
        'drEdiRcvHed("OUT_KB_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_2").ToString().Trim(), 8)         '出力区分名
        'drEdiRcvHed("JIGYO_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_3").ToString().Trim(), 3)          '事業所コード
        'drEdiRcvHed("JIGYO_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_4").ToString().Trim(), 40)         '事業所名
        'drEdiRcvHed("SOUKO_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_5").ToString().Trim(), 9)          '倉庫／仕入先コード
        'drEdiRcvHed("SOUKO_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_6").ToString().Trim(), 40)         '倉庫名／仕入先名
        'drEdiRcvHed("OUTKA_BI") = Me._Blc.LeftB(drSetDtl("COLUMN_7").ToString().Trim(), 8)          '出荷日
        'drEdiRcvHed("NONYU_ZIP") = Me._Blc.LeftB(drSetDtl("COLUMN_8").ToString().Trim(), 8)         '納入先郵便番号
        'drEdiRcvHed("NONYU_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_9").ToString().Trim(), 9)          '納入先コード
        'drEdiRcvHed("NONYU_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_10").ToString().Trim(), 40)        '納入先名
        'drEdiRcvHed("NONYU_AD_1") = Me._Blc.LeftB(drSetDtl("COLUMN_11").ToString().Trim(), 40)      '納入先住所１
        'drEdiRcvHed("NONYU_AD_2") = Me._Blc.LeftB(drSetDtl("COLUMN_12").ToString().Trim(), 40)      '納入先住所２
        'drEdiRcvHed("NONYU_TEL") = Me._Blc.LeftB(drSetDtl("COLUMN_13").ToString().Trim(), 20)       '納入先電話番号
        'drEdiRcvHed("TORIKESHI_KB_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_14").ToString().Trim(), 8)  '取消区分名
        'drEdiRcvHed("DENP_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_15").ToString().Trim(), 10)         '受注伝票番号
        'drEdiRcvHed("JISSEKI_SHORI_FLG") = "1"                                                      '実績処理フラグ

        'EDI受信DTL設定
        drEdiRcvDtl("DEL_KB") = "0"
        drEdiRcvDtl("CRT_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl("FILE_NAME") = drSetDtl("FILE_NAME_OPE")
        drEdiRcvDtl("REC_NO") = Convert.ToInt32(drSetDtl("REC_NO")).ToString("00000")
        'drEdiRcvDtl("GYO") = String.Empty
        drEdiRcvDtl("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drEdiRcvDtl("EDI_CTL_NO") = DEF_CTL_NO              '親以外は後でUpdateされる                
        drEdiRcvDtl("EDI_CTL_NO_CHU") = "000"               '親以外は後でUpdateされる 
        drEdiRcvDtl("OUTKA_CTL_NO") = DEF_CTL_NO
        drEdiRcvDtl("OUTKA_CTL_NO_CHU") = "000"
        'drEdiRcvDtl("CUST_CD_L") = CUST_CD_L_LNZ                                                   '荷主コード（大）
        'drEdiRcvDtl("CUST_CD_M") = CUST_CD_M_LNZ                                                   '荷主コード（中）
        drEdiRcvDtl("PRTFLG") = "0"                                                                 'プリントフラグ
        'drEdiRcvDtl("CANCEL_FLG") = "0"                                                            'キャンセルフラグ
        drEdiRcvDtl("JISSEKI_SHORI_FLG") = "1"                                                      '実績処理フラグ

        '荷主固有データ
        drEdiRcvDtl("DELIVERY_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_1").ToString().Trim(), 50)      'デリバリーＮｏ．
        drEdiRcvDtl("S_ORDER_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_4").ToString().Trim(), 50)       'S.Order No.
        drEdiRcvDtl("CUST_PO") = Me._Blc.LeftB(drSetDtl("COLUMN_3").ToString().Trim(), 80)          'お客様ＰＯ
        drEdiRcvDtl("MAKER_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_5").ToString().Trim(), 15)         'メーカーコード
        drEdiRcvDtl("ITEM_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_2").ToString().Trim(), 15)          '品目コード
        drEdiRcvDtl("PRODUCT_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_6").ToString().Trim(), 60)       'プロダクトネーム
        drEdiRcvDtl("QTY") = Me._Blc.LeftB(drSetDtl("COLUMN_10").ToString().Trim(), 8)              'ＱＴＹ
        drEdiRcvDtl("LOT_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_9").ToString().Trim(), 50)           'ロットＮｏ．
        drEdiRcvDtl("OUTKA_PLAN_DATE") = Me.GetSystemDate                                           '出荷日
        drEdiRcvDtl("ARR_DATE") = Format(Convert.ToDateTime(drSetDtl("COLUMN_12")), "yyyyMMdd")     'お届け日
        drEdiRcvDtl("SAP_DEST_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_7").ToString().Trim(), 50)      'SAP　納入先ｺｰﾄﾞ
        drEdiRcvDtl("DEST_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_8").ToString().Trim(), 80)          '納入先名
        drEdiRcvDtl("REIZO") = Me._Blc.LeftB(drSetDtl("COLUMN_11").ToString().Trim(), 100)          '冷蔵
        drEdiRcvDtl("REMARK") = Me._Blc.LeftB(drSetDtl("COLUMN_13").ToString().Trim(), 80)          '備考

        drEdiRcvDtl("URIAGESAKI_CD") = String.Empty         '売上先コード
        drEdiRcvDtl("URIAGESAKI_NM") = String.Empty         '売上先名称
        drEdiRcvDtl("UNSO_ONDO_KB") = String.Empty          '運送温度区分
        drEdiRcvDtl("MATOME_REIZO") = String.Empty          'まとめ用冷蔵
        drEdiRcvDtl("SET_GROUP_NO") = String.Empty          'セットグループNo.
        drEdiRcvDtl("SET_FLG") = String.Empty               'セットフラグ
        drEdiRcvDtl("SET_ONDO_KB") = String.Empty           '温度区分
        drEdiRcvDtl("SET_OYA_CD") = String.Empty            'セット親メーカーコード
        drEdiRcvDtl("GOODS_DOKU_KB") = String.Empty         '劇毒区分
        drEdiRcvDtl("GOODS_ONDO_KB") = String.Empty         '温度管理区分
        drEdiRcvDtl("GOODS_ONDO_MX") = String.Empty         '温度上限
        drEdiRcvDtl("MATOME_ONDO") = String.Empty           'まとめ用温度
        drEdiRcvDtl("EDI_DATA_FLG") = "1"                   'EDIデータフラグ

        'データセットに設定
        'ds.Tables("LMH030_OUTKAEDI_HED_LNZ").Rows.Add(drEdiRcvHed)
        ds.Tables("LMH030_OUTKAEDI_DTL_LNZ").Rows.Add(drEdiRcvDtl)

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
                                    , ByVal sWhcd As String _
                                    , ByVal sCustCdL As String _
                                    , ByVal sCustCdM As String _
                                    , ByVal sNrsGoodsCd As String _
                                    , ByVal sNrsGoodsNm As String _
                                    , ByVal sIrime As String _
                                    , ByVal sGsEdiCtlNo As String _
                                    , ByVal iGroupCnt As Integer _
                                    , ByVal iRecCnt As Integer _
                                    , ByVal iPageCnt As Integer _
                                    , Optional ByVal bPrtFlg As Boolean = False) As DataSet

        Dim drOutkaEdiM As DataRow = setDs.Tables("LMH030_OUTKAEDI_M").NewRow()
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_OUTKAEDI_DTL_LNZ").Rows(0)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        'Dim drGoods As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)

        drOutkaEdiM("DEL_KB") = "0"
        drOutkaEdiM("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drOutkaEdiM("EDI_CTL_NO") = drRcvEdiDtl("EDI_CTL_NO")
        drOutkaEdiM("EDI_CTL_NO_CHU") = drRcvEdiDtl("EDI_CTL_NO_CHU")
        drOutkaEdiM("OUTKA_CTL_NO") = String.Empty
        drOutkaEdiM("OUTKA_CTL_NO_CHU") = String.Empty
        drOutkaEdiM("COA_YN") = String.Empty
        drOutkaEdiM("CUST_ORD_NO_DTL") = Me._Blc.LeftB(drRcvEdiDtl("DELIVERY_NO").ToString, 30)
        drOutkaEdiM("BUYER_ORD_NO_DTL") = Me._Blc.LeftB(drRcvEdiDtl("CUST_PO").ToString, 30)
        drOutkaEdiM("CUST_GOODS_CD") = drRcvEdiDtl("MAKER_CD")
        drOutkaEdiM("NRS_GOODS_CD") = String.Empty
        drOutkaEdiM("GOODS_NM") = drRcvEdiDtl("PRODUCT_NM")

        drOutkaEdiM("RSV_NO") = String.Empty
        drOutkaEdiM("LOT_NO") = Me._Blc.LeftB(drRcvEdiDtl("LOT_NO").ToString, 40)
        drOutkaEdiM("SERIAL_NO") = String.Empty
        drOutkaEdiM("ALCTD_KB") = "01"                              '"01"：個数引当

        drOutkaEdiM("OUTKA_PKG_NB") = 0                             '出荷包装個数   0:固定

        drOutkaEdiM("OUTKA_HASU") = drRcvEdiDtl("QTY")              '出荷端数 
        drOutkaEdiM("OUTKA_QT") = 0                                 '出荷数量
        drOutkaEdiM("OUTKA_TTL_NB") = drRcvEdiDtl("QTY")            '出荷総個数
        drOutkaEdiM("OUTKA_TTL_QT") = 0                             '出荷総数量

        drOutkaEdiM("KB_UT") = String.Empty                         '数量単位
        drOutkaEdiM("QT_UT") = String.Empty
        drOutkaEdiM("PKG_NB") = 0
        drOutkaEdiM("PKG_UT") = String.Empty
        drOutkaEdiM("ONDO_KB") = String.Empty
        drOutkaEdiM("UNSO_ONDO_KB") = String.Empty
        drOutkaEdiM("IRIME") = 0                                    '入目
        drOutkaEdiM("IRIME_UT") = String.Empty
        drOutkaEdiM("BETU_WT") = 0                                  '個別重量

        drOutkaEdiM("REMARK") = IIf(drRcvEdiDtl("SET_OYA_CD").ToString.Trim <> "", "(セット品：" & drRcvEdiDtl("SET_OYA_CD").ToString.Trim & ")", "")                        '注意事項

        drOutkaEdiM("OUT_KB") = drRcvEdiDtl("SET_FLG")
        drOutkaEdiM("AKAKURO_KB") = "0"
        drOutkaEdiM("JISSEKI_FLAG") = "0"
        drOutkaEdiM("JISSEKI_USER") = String.Empty
        drOutkaEdiM("JISSEKI_DATE") = String.Empty
        drOutkaEdiM("JISSEKI_TIME") = String.Empty
        drOutkaEdiM("SET_KB") = drRcvEdiDtl("SET_FLG")                             '"0"：対象

        drOutkaEdiM("FREE_N01") = drRcvEdiDtl("QTY")
        drOutkaEdiM("FREE_N02") = 0
        drOutkaEdiM("FREE_N03") = 0
        drOutkaEdiM("FREE_N04") = 0
        drOutkaEdiM("FREE_N05") = 0
        drOutkaEdiM("FREE_N06") = 0
        drOutkaEdiM("FREE_N07") = 0
        drOutkaEdiM("FREE_N08") = 0
        drOutkaEdiM("FREE_N09") = 0
        drOutkaEdiM("FREE_N10") = 0

        drOutkaEdiM("FREE_C01") = drRcvEdiDtl("REMARK")
        drOutkaEdiM("FREE_C02") = drRcvEdiDtl("ITEM_CD")
        drOutkaEdiM("FREE_C03") = drRcvEdiDtl("PRODUCT_NM")
        drOutkaEdiM("FREE_C04") = drRcvEdiDtl("SAP_DEST_CD")
        drOutkaEdiM("FREE_C05") = drRcvEdiDtl("DEST_NM")
        drOutkaEdiM("FREE_C06") = drRcvEdiDtl("REIZO")
        drOutkaEdiM("FREE_C07") = drRcvEdiDtl("S_ORDER_NO")
        drOutkaEdiM("FREE_C08") = drRcvEdiDtl("CUST_PO")
        drOutkaEdiM("FREE_C09") = drRcvEdiDtl("SET_OYA_CD")
        drOutkaEdiM("FREE_C10") = drRcvEdiDtl("URIAGESAKI_CD")
        drOutkaEdiM("FREE_C11") = drRcvEdiDtl("URIAGESAKI_NM")
        drOutkaEdiM("FREE_C12") = drRcvEdiDtl("FILE_NAME")
        drOutkaEdiM("FREE_C13") = drRcvEdiDtl("REC_NO")
        drOutkaEdiM("FREE_C14") = String.Empty
        drOutkaEdiM("FREE_C15") = String.Empty
        drOutkaEdiM("FREE_C16") = String.Empty
        drOutkaEdiM("FREE_C17") = String.Empty
        drOutkaEdiM("FREE_C18") = String.Empty
        drOutkaEdiM("FREE_C19") = String.Empty
        drOutkaEdiM("FREE_C20") = String.Empty      '下部も参照の事
        drOutkaEdiM("FREE_C21") = iGroupCnt         'グループカウント（印刷用）
        drOutkaEdiM("FREE_C22") = iRecCnt           'レコードカウント（印刷用）
        drOutkaEdiM("FREE_C23") = String.Empty      '下部も参照の事
        drOutkaEdiM("FREE_C24") = String.Empty
        drOutkaEdiM("FREE_C25") = iPageCnt          'ページカウント（印刷用）
        drOutkaEdiM("FREE_C26") = String.Empty      '下部も参照の事
        drOutkaEdiM("FREE_C27") = String.Empty      '下部も参照の事
        drOutkaEdiM("FREE_C28") = String.Empty
        drOutkaEdiM("FREE_C29") = String.Empty
        drOutkaEdiM("FREE_C30") = String.Empty

        '印刷時の特殊設定
        If bPrtFlg Then
            drOutkaEdiM("FREE_C20") = sGsEdiCtlNo
            drOutkaEdiM("FREE_C23") = drRcvEdiDtl("REC_NO")
            drOutkaEdiM("FREE_C26") = sCustCdL
            drOutkaEdiM("FREE_C27") = sCustCdM
        End If

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
    Private Function SetSemiOutkaEdiL(ByVal setDs As DataSet _
                                    , ByVal sWhCd As String _
                                    , ByVal sCustCdL As String _
                                    , ByVal sCustCdM As String _
                                    , ByVal sGsEdiCtlNo As String _
                                    , Optional ByVal bPrtFlg As Boolean = False) As DataSet

        Dim drOutkaEdiL As DataRow = setDs.Tables("LMH030_OUTKAEDI_L").NewRow()
        'Dim drRcvEdiHed As DataRow = setDs.Tables("LMH030_OUTKAEDI_HED_LNZ").Rows(0)
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_OUTKAEDI_DTL_LNZ").Rows(0)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)



        '荷主Index
        Dim ediCustIndex As String = drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString()

        drOutkaEdiL("DEL_KB") = "0"
        drOutkaEdiL("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drOutkaEdiL("EDI_CTL_NO") = drRcvEdiDtl("EDI_CTL_NO")
        drOutkaEdiL("OUTKA_CTL_NO") = String.Empty
        drOutkaEdiL("OUTKA_KB") = "10"
        drOutkaEdiL("SYUBETU_KB") = "10"
        drOutkaEdiL("NAIGAI_KB") = String.Empty
        drOutkaEdiL("OUTKA_STATE_KB") = "10"
        drOutkaEdiL("OUTKAHOKOKU_YN") = String.Empty
        drOutkaEdiL("PICK_KB") = "01"
        drOutkaEdiL("NRS_BR_NM") = String.Empty
        drOutkaEdiL("WH_CD") = sWhCd
        drOutkaEdiL("WH_NM") = String.Empty
        drOutkaEdiL("OUTKA_PLAN_DATE") = drRcvEdiDtl("OUTKA_PLAN_DATE").ToString    '出荷予定日
        drOutkaEdiL("OUTKO_DATE") = drRcvEdiDtl("OUTKA_PLAN_DATE").ToString         '出庫日

        '納入予定日
        drOutkaEdiL("ARR_PLAN_DATE") = drRcvEdiDtl("ARR_DATE").ToString           '納入予定日

        drOutkaEdiL("ARR_PLAN_TIME") = String.Empty     '納入予定時刻
        drOutkaEdiL("HOKOKU_DATE") = String.Empty       '出荷報告日
        drOutkaEdiL("TOUKI_HOKAN_YN") = String.Empty    '当期保管料負担有無

        drOutkaEdiL("CUST_CD_L") = sCustCdL
        drOutkaEdiL("CUST_CD_M") = sCustCdM
        drOutkaEdiL("CUST_NM_L") = String.Empty
        drOutkaEdiL("CUST_NM_M") = String.Empty

        drOutkaEdiL("SHIP_CD_L") = String.Empty
        drOutkaEdiL("SHIP_CD_M") = String.Empty
        drOutkaEdiL("SHIP_NM_L") = String.Empty
        drOutkaEdiL("SHIP_NM_M") = String.Empty

        drOutkaEdiL("EDI_DEST_CD") = Me._Blc.LeftB(drRcvEdiDtl("SAP_DEST_CD").ToString, 30)
        drOutkaEdiL("DEST_CD") = Me._Blc.LeftB(drRcvEdiDtl("SAP_DEST_CD").ToString, 30)
        drOutkaEdiL("DEST_NM") = Me._Blc.LeftB(drRcvEdiDtl("DEST_NM").ToString().Trim(), 60)

        drOutkaEdiL("DEST_ZIP") = String.Empty
        drOutkaEdiL("DEST_AD_1") = String.Empty
        drOutkaEdiL("DEST_AD_2") = String.Empty
        drOutkaEdiL("DEST_AD_3") = String.Empty
        drOutkaEdiL("DEST_AD_4") = String.Empty
        drOutkaEdiL("DEST_AD_5") = String.Empty
        drOutkaEdiL("DEST_TEL") = String.Empty
        drOutkaEdiL("DEST_FAX") = String.Empty
        drOutkaEdiL("DEST_MAIL") = String.Empty
        drOutkaEdiL("DEST_JIS_CD") = String.Empty
        drOutkaEdiL("SP_NHS_KB") = String.Empty
        drOutkaEdiL("COA_YN") = String.Empty

        drOutkaEdiL("CUST_ORD_NO") = Me._Blc.LeftB(drRcvEdiDtl("DELIVERY_NO").ToString().Trim(), 30)
        drOutkaEdiL("BUYER_ORD_NO") = String.Empty          '買主注文番号（全体）

        drOutkaEdiL("UNSO_MOTO_KB") = String.Empty                      '10：日陸手配
        drOutkaEdiL("UNSO_TEHAI_KB") = String.Empty
        drOutkaEdiL("SYARYO_KB") = String.Empty
        drOutkaEdiL("BIN_KB") = String.Empty
        drOutkaEdiL("UNSO_CD") = String.Empty
        drOutkaEdiL("UNSO_NM") = String.Empty
        drOutkaEdiL("UNSO_BR_CD") = String.Empty
        drOutkaEdiL("UNSO_BR_NM") = String.Empty
        drOutkaEdiL("UNCHIN_TARIFF_CD") = String.Empty
        drOutkaEdiL("EXTC_TARIFF_CD") = String.Empty

        ''注意事項
        drOutkaEdiL("REMARK") = String.Empty
        drOutkaEdiL("UNSO_ATT") = String.Empty
        drOutkaEdiL("DENP_YN") = "1"                                    '1：有
        drOutkaEdiL("PC_KB") = String.Empty
        drOutkaEdiL("UNCHIN_YN") = "1"                                  '1：有
        drOutkaEdiL("NIYAKU_YN") = String.Empty
        drOutkaEdiL("OUT_FLAG") = "0"
        drOutkaEdiL("AKAKURO_KB") = "0"
        drOutkaEdiL("JISSEKI_FLAG") = "0"
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

        drOutkaEdiL("FREE_C01") = drRcvEdiDtl("FILE_NAME")
        drOutkaEdiL("FREE_C02") = drRcvEdiDtl("SAP_DEST_CD")
        drOutkaEdiL("FREE_C03") = drRcvEdiDtl("URIAGESAKI_CD")
        drOutkaEdiL("FREE_C04") = drRcvEdiDtl("URIAGESAKI_NM")
        drOutkaEdiL("FREE_C05") = String.Empty

        If bPrtFlg Then
            drOutkaEdiL("FREE_C06") = sGsEdiCtlNo
        Else
            drOutkaEdiL("FREE_C06") = String.Empty
        End If

        drOutkaEdiL("FREE_C07") = String.Empty
        drOutkaEdiL("FREE_C08") = String.Empty
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
        drOutkaEdiL("FREE_C20") = CUST_CD_L_LNZ & CUST_CD_M_LNZ
        drOutkaEdiL("FREE_C21") = drRcvEdiDtl("EDI_CTL_NO")
        drOutkaEdiL("FREE_C22") = String.Empty
        drOutkaEdiL("FREE_C23") = String.Empty
        drOutkaEdiL("FREE_C24") = String.Empty
        drOutkaEdiL("FREE_C25") = String.Empty
        drOutkaEdiL("FREE_C26") = String.Empty
        drOutkaEdiL("FREE_C27") = String.Empty
        drOutkaEdiL("FREE_C28") = String.Empty
        drOutkaEdiL("FREE_C29") = String.Empty
        drOutkaEdiL("FREE_C30") = String.Empty

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_L").Rows.Add(drOutkaEdiL)
        Return setDs


    End Function

#End Region

#End Region

#Region "セミEDI時　商品マスタから取得する"

    ''' <summary>
    ''' 商品マスタからCustCd等を取得する
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMstGoods(ByVal ds As DataSet _
                             , ByRef sUNSO_ONDO_KB As String _
                             , ByRef sDOKU_KB As String _
                             , ByRef sONDO_KB As String _
                             , ByRef iONDO_MX As Integer _
                             , ByRef iMATOME_ONDO As Integer _
                              ) As Integer

        Dim dtMstGoods As DataTable = ds.Tables("LMH030_M_GOODS")
        Dim iMstGoodsCnt As Integer = dtMstGoods.Rows.Count

        '既定値設定
        sUNSO_ONDO_KB = String.Empty
        sDOKU_KB = String.Empty
        sONDO_KB = String.Empty
        iONDO_MX = 0
        iMATOME_ONDO = 0

        Select Case iMstGoodsCnt
            Case 0      '商品マスタ取得０件
                '既定値を返す

            Case Else   '商品マスタ取得(１件目で判定)
                sUNSO_ONDO_KB = dtMstGoods.Rows(0).Item("UNSO_ONDO_KB").ToString
                sDOKU_KB = dtMstGoods.Rows(0).Item("DOKU_KB").ToString
                sONDO_KB = dtMstGoods.Rows(0).Item("ONDO_KB").ToString
                iONDO_MX = CInt(dtMstGoods.Rows(0).Item("ONDO_MX").ToString)

                'まとめ温度の設定
                Select Case sONDO_KB
                    Case "01"
                        iMATOME_ONDO = 0
                    Case "02"
                        If iONDO_MX < -20 Then
                            iMATOME_ONDO = -80

                            '要望番号:1871（ロンザ　EDI取込　温度区分でまとまらないパターンがある）対応　 2013/02/18 本明Start
                            'ElseIf (4 <= iMATOME_ONDO And iMATOME_ONDO <= 8) Then
                        ElseIf (4 <= iONDO_MX And iONDO_MX <= 8) Then
                            '要望番号:1871（ロンザ　EDI取込　温度区分でまとまらないパターンがある）対応　 2013/02/18 本明End
                            iMATOME_ONDO = 4
                        Else
                            iMATOME_ONDO = iONDO_MX
                        End If
                End Select
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
                               , ByVal iDeleteFlg As Integer, ByVal iSkipFlg As Integer, ByVal bSameKeyFlg As Boolean _
                                , ByRef sEdiCtlNo As String, ByRef iEdiCtlNoChu As Integer) As DataSet

        Dim dtRcvEdiHed As DataTable = ds.Tables("LMH030_OUTKAEDI_HED_LNZ")
        Dim dtRcvEdiDtl As DataTable = ds.Tables("LMH030_OUTKAEDI_DTL_LNZ")
        Dim drRcvEdiDtl As DataRow = ds.Tables("LMH030_OUTKAEDI_DTL_LNZ").Rows(0)
        Dim sNrsBrCd As String = ds.Tables("LMH030_OUTKAEDI_DTL_LNZ").Rows(0).Item("NRS_BR_CD").ToString()

        '前行とキーが異なる場合　
        If bSameKeyFlg = False Then
            iEdiCtlNoChu = 0    '０クリア    
        End If

        'EDI管理番号(中)をカウントアップ
        iEdiCtlNoChu = iEdiCtlNoChu + 1

        If iSkipFlg = 0 Then
            'キャンセルフラグが０ かつ スキップフラグが０の場合　
            If bSameKeyFlg = False Then
                '前行とキーが異なる場合　
                'EDI管理番号(大)を新規採番してEDI管理番号(中)を"001"採番
                Dim num As New NumberMasterUtility
                sEdiCtlNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_OUTKA_NO_L, Me, sNrsBrCd)
            End If

            '登録用EDI管理番号
            'dtRcvEdiHed.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo              'HEDにセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo              'DTLにセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO_CHU") = iEdiCtlNoChu.ToString("000")   'EDI_CHUにセット
            'dtRcvEdiDtl.Rows(0).Item("GYO") = iEdiCtlNoChu.ToString("000")              '行数にもEDI_CHUと同じ値をセット
        Else
            'dtRcvEdiHed.Rows(0).Item("EDI_CTL_NO") = DEF_CTL_NO             'HEDに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO") = DEF_CTL_NO             'DTLに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO_CHU") = "000"              'EDI_CHUに固定値をセット
            'dtRcvEdiDtl.Rows(0).Item("GYO") = iEdiCtlNoChu.ToString("000")  '行数にはカウントアップした値を入れる
        End If

        ''削除EDI管理番号にも設定する(削除フラグが１の場合のみ)
        'If iDeleteFlg = 1 Then
        '    Dim dtRcvHedDel As DataTable = ds.Tables("LMH030_DTL_LNZ_CANCELOUT")
        '    Dim drRcvHedDel As DataRow = ds.Tables("LMH030_DTL_LNZ_CANCELOUT").Rows(0)

        '    Dim dtRcvDtlDel As DataTable = ds.Tables("LMH030_DTL_LNZ_CANCELOUT")
        '    Dim drRcvDtlDel As DataRow = ds.Tables("LMH030_DTL_LNZ_CANCELOUT").Rows(0)
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

        Dim iRowCnt As Integer = 0


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

        Dim sNum As String = String.Empty
        Dim dNum As Double = 0
        Dim sDate As String = String.Empty
        Dim sMsg As String = String.Empty

        '要望番号1982（ロンザ　セミEDI時のチェック追加）対応 2013/04/02 本明 Start
        sMsg = "届け先コード(カラム7番目)"
        If dr.Item("COLUMN_7").ToString() = vbNullString Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {sMsg}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If
        '要望番号1982（ロンザ　セミEDI時のチェック追加）対応 2013/04/02 本明 End

        sMsg = "お届け日(カラム12番目)["
        sDate = dr.Item("COLUMN_12").ToString()
        'sDate = Me._Blc.GetSlashEditDate(sDate)    'スラッシュ付日付に編集
        If IsDate(sDate) = True Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat(sMsg, sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "数量(カラム10番目)["
        sNum = dr.Item("COLUMN_10").ToString().Trim()
        If String.IsNullOrEmpty(sNum) = True Then
            '空の場合はゼロをセット
            dr.Item("COLUMN_10") = 0
        Else
            If IsNumeric(sNum) Then
                '数値の場合
                dNum = Convert.ToDouble(sNum)
                If dNum > 99999999 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, dNum.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            Else
                '数値でない場合
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            End If
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

        'Dim dtRcvHed As DataTable = ds.Tables("LMH030_OUTKAEDI_HED_LNZ")         'EDI受信Hed
        Dim dtRcvDtl As DataTable = ds.Tables("LMH030_OUTKAEDI_DTL_LNZ")         'EDI受信Dtl
        'Dim dtRcvDtlCancel As DataTable = ds.Tables("LMH030_DTL_LNZ_CANCELOUT")  'EDI受信Hed

        Dim drEdiRcvDtl As DataRow = Nothing


        Dim iCancelCnt As Integer = 0
        Dim iGoodsCnt As Integer = 0

        Dim iSetDtlMax As Integer = dtSetDtl.Rows.Count - 1

        Dim sWhcd As String = String.Empty          '倉庫コード     
        Dim sCustCdL As String = String.Empty       '荷主コード大   
        Dim sCustCdM As String = String.Empty       '荷主コード中   
        Dim sNrsGoodsCd As String = String.Empty    '日陸商品コード 
        Dim sNrsGoodsNm As String = String.Empty    '日陸商品名     
        Dim sIrime As String = String.Empty         '入目           

        Dim iAkakuroVal As Integer = 0              '赤黒値    (0:黒、1:赤)         

        Dim iSkipFlg As Integer = 0                 'スキップフラグ     (0:EDI出荷に登録する、  1:EDI出荷に登録しない)
        Dim iDeleteFlg As Integer = 0               '取消フラグ         (0:EDI出荷を削除しない、1:EDI出荷を削除する)

        Dim iFindRcvEdiFlg As Integer = 0           '削除対象EDI受信データ存在フラグ (0:存在しない、1:存在する)
        Dim iFindOutkaEdiFlg As Integer = 0         '削除対象EDI出荷データ存在フラグ (0:存在しない、1:存在する)

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
        Dim iRcvHedCanCnt As Integer = 0            '取消件数（受信HED）
        Dim iRcvDtlCanCnt As Integer = 0            '取消件数（受信Dtl）
        Dim iOutHedCanCnt As Integer = 0            '取消件数（出荷EDI(大)）
        Dim iOutDtlCanCnt As Integer = 0            '取消件数（出荷EDI(中)）


        Dim bNoErr As Boolean = True                'エラー無しフラグ（True：エラー無し、False：エラー有り）

        Dim iSetCnt As Integer = 0                      'セット明細数
        Dim iSetGroupNo As Integer = 0                  'セットグループ№カウント用

        Dim sUNSO_ONDO_KB As String = String.Empty      '運送温度区分
        Dim sMATOME_REIZO As String = String.Empty      'まとめ用冷蔵
        Dim sSET_GROUP_NO As String = String.Empty      'セットグループ№（セット品ごとに番号をセットする（セット以外"ZZZ"））
        Dim sSET_FLG As String = String.Empty           'セットフラグ（0:子、1:セット品親）
        Dim sSET_ONDO_KB As String = String.Empty       'セット温度区分
        Dim sSET_OYA_CD As String = String.Empty        'セット親コード
        Dim sGOODS_DOKU_KB As String = String.Empty     '劇毒区分       （商品マスタより（セット品親は未設定））
        Dim sGOODS_ONDO_KB As String = String.Empty     '温度管理区分   （商品マスタより（セット品親は未設定））
        Dim iGOODS_ONDO_MX As Integer = 0               '温度上限       （商品マスタより（セット品親は未設定））
        Dim iMATOME_ONDO As Integer = 0                 '上記温度上限をまとめ用に変換後の温度


        Dim sGsEdiCtlNo As String = String.Empty

        Dim iGroupCnt As Integer = 0
        Dim iRecCnt As Integer = 0
        Dim iPageCnt As Integer = 0

        Dim sFileNm As String = String.Empty
        Dim dOyaQty As Double = 0   'セット親の個数（子のチェックで使用）

        '要望番号:1861（ロンザ　EDI取込　子セット品が複数行の場合、エラーとなる）対応　 2013/02/15 本明Start
        Dim dGoodsSet() As GoodsSet
        '要望番号:1861（ロンザ　EDI取込　子セット品が複数行の場合、エラーとなる）対応　 2013/02/15 本明End

        '---------------------------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------------------------
        '  　初期値設定
        '---------------------------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------------------------
        sWhcd = WH_CD_LNZ           '倉庫コードは既定値使用
        sCustCdL = CUST_CD_L_LNZ    '荷主コードは規定値使用
        sCustCdM = CUST_CD_M_LNZ    '荷主コードは規定値使用

        iAkakuroVal = 0             '黒データ
        iSkipFlg = 0                'EDI出荷登録する
        iDeleteFlg = 0              'EDI出荷削除しない

        '---------------------------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------------------------
        '  ①受信EDIデータに登録する
        '  　受信データをもとにH_OUTKAEDI_DTL_LNZに登録する
        '---------------------------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------------------------
        For i As Integer = 0 To iSetDtlMax

            '---------------------------------------------------------------------------
            ' セミEDI取込(共通)⇒EDI受信データセット
            '---------------------------------------------------------------------------
            ds.Tables("LMH030_OUTKAEDI_DTL_LNZ").Clear() '受信DTLをクリア
            ds = Me.SetSemiOutkaEdiRcv(ds, i)
            drEdiRcvDtl = ds.Tables("LMH030_OUTKAEDI_DTL_LNZ").Rows(0)

            '---------------------------------------------------------------------------
            ' [セット明細数] の値により処理を分岐　※iSetCnt>0:セット商品の展開中
            '---------------------------------------------------------------------------
            If iSetCnt = 0 Then

                sFileNm = drEdiRcvDtl.Item("FILE_NAME").ToString()

                '---------------------------------------------------------------------------
                ' ロンザセット商品マスタを読込み、親コードであるかを判断
                '---------------------------------------------------------------------------
                ds = MyBase.CallDAC(Me._Dac, "SelectMstSetGoodsLnz", ds)
                If MyBase.GetResultCount > 0 Then
                    'セット商品の親コードの場合

                    iSetCnt = MyBase.GetResultCount             'セット点数

                    '要望番号:1861（ロンザ　EDI取込　子セット品が複数行の場合、エラーとなる）対応　 2013/02/15 本明Start
                    ReDim dGoodsSet(iSetCnt - 1)

                    'セット商品の内容を配列にセットする
                    Dim iQty As Integer = Convert.ToInt32(drEdiRcvDtl.Item("QTY").ToString)

                    For j As Integer = 0 To iSetCnt - 1
                        dGoodsSet(j).KO_CD = ds.Tables("LMH030_SET_GOODS_LNZ").Rows(j).Item("KO_CD").ToString.Trim
                        Dim iKosu As Integer = Convert.ToInt32(ds.Tables("LMH030_SET_GOODS_LNZ").Rows(j).Item("SET_KOSU").ToString)
                        dGoodsSet(j).SET_KOSU = iQty * iKosu  '親セット数×子のセット数
                    Next
                    '要望番号:1861（ロンザ　EDI取込　子セット品が複数行の場合、エラーとなる）対応　 2013/02/15 本明End

                    sUNSO_ONDO_KB = ds.Tables("LMH030_SET_GOODS_LNZ").Rows(0).Item("UNSO_ONDO_KB").ToString '運送温度区分
                    sMATOME_REIZO = drEdiRcvDtl.Item("REIZO").ToString     'まとめ用冷蔵
                    iSetGroupNo = iSetGroupNo + 1
                    sSET_GROUP_NO = iSetGroupNo.ToString("000") 'セットグループ№
                    sSET_FLG = "1"                              'セットフラグ
                    sSET_ONDO_KB = sUNSO_ONDO_KB                'セット温度区分
                    sSET_OYA_CD = drEdiRcvDtl.Item("MAKER_CD").ToString        'セット親コード
                    sGOODS_DOKU_KB = String.Empty               '劇毒区分       （商品マスタより（セット品親は未設定））
                    sGOODS_ONDO_KB = String.Empty               '温度管理区分   （商品マスタより（セット品親は未設定））
                    iGOODS_ONDO_MX = 0                          '温度上限       （商品マスタより（セット品親は未設定））
                    iMATOME_ONDO = 0                            '上記温度上限をまとめ用に変換後の温度

                    dOyaQty = Convert.ToDouble(drEdiRcvDtl.Item("QTY").ToString)    'セット親の個数（子のチェックで使用）
                Else
                    'セット商品の親コードでない場合（単品商品）

                    sMATOME_REIZO = String.Empty                'まとめ用冷蔵

                    sSET_GROUP_NO = "ZZZ"                       'セットグループ№⇒"ZZZ"を設定
                    sSET_FLG = "0"                              'セットフラグ
                    sSET_OYA_CD = String.Empty                  'セット親コード

                    '商品マスタを読込
                    ds = MyBase.CallDAC(Me._Dac, "SelectMstGoods", ds)
                    '取得した商品マスタから温度区分等を決定する
                    Call Me.GetMstGoods(ds, sUNSO_ONDO_KB, sGOODS_DOKU_KB, sGOODS_ONDO_KB, iGOODS_ONDO_MX, iMATOME_ONDO)

                    sSET_ONDO_KB = sUNSO_ONDO_KB                'セット温度区分

                End If

            Else
                'セット商品の子コードの場合（LMH030_SET_GOODS_LNZの展開中）

                'LMH030_SET_GOODS_LNZ内に、親コード、子コードのセットが存在するか
                Dim dt As DataTable = ds.Tables("LMH030_SET_GOODS_LNZ")
                Dim outDr() As DataRow = Nothing
                outDr = dt.Select(String.Concat("OYA_CD = '", sSET_OYA_CD, "' AND ", _
                                                "KO_CD = '", drEdiRcvDtl.Item("MAKER_CD").ToString, "'"))
                If outDr.Count = 0 Then
                    'エラー！！
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E516", New String() {String.Concat("")}, Convert.ToString(i), LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bNoErr = False
                    Exit For
                End If

                '要望番号:1861（ロンザ　EDI取込　子セット品が複数行の場合、エラーとなる）対応　 2013/02/15 本明Start

                'LMH030_SET_GOODS_LNZ内のセット個数×親の数量は子の数量に等しいか
                Dim sKo_cd As String = drEdiRcvDtl.Item("MAKER_CD").ToString.Trim           '子コード
                Dim iQty As Integer = Convert.ToInt32(drEdiRcvDtl.Item("QTY").ToString)     '子数量

                For k As Integer = LBound(dGoodsSet) To UBound(dGoodsSet)

                    If dGoodsSet(k).KO_CD = sKo_cd Then '子コードが等しい場合
                        dGoodsSet(k).SET_KOSU = dGoodsSet(k).SET_KOSU - iQty    '数量を０になるまで引き算する（子商品が入れ子になる場合ありの為）
                        Select Case dGoodsSet(k).SET_KOSU
                            Case Is > 0

                            Case 0
                                'セット明細数の値をカウントダウン
                                iSetCnt = iSetCnt - 1

                            Case Is < 0
                                'エラー！！
                                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E517", New String() {String.Concat("")}, Convert.ToString(i), LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                                bNoErr = False
                        End Select
                        Exit For
                    End If
                Next

                If bNoErr = False Then
                    Exit For
                End If
                '要望番号:1861（ロンザ　EDI取込　子セット品が複数行の場合、エラーとなる）対応　 2013/02/15 本明End

                'sUNSO_ONDO_KB          '運送温度区分       ⇒親と同じ
                'sMATOME_REIZO          'まとめ用冷蔵       ⇒親と同じ
                'sSET_GROUP_NO          'セットグループ№   ⇒親と同じ
                sSET_FLG = "0"          'セットフラグ       ⇒親：1、子：0
                'sSET_ONDO_KB           'セット温度区分     ⇒親と同じ
                'sSET_OYA_CD            'セット親コード     ⇒親と同じ

                '商品マスタを読込
                ds = MyBase.CallDAC(Me._Dac, "SelectMstGoods", ds)
                '取得した商品マスタから温度区分等を決定する
                Dim sDummyUNSO_ONDO_KB As String = String.Empty
                Call Me.GetMstGoods(ds, sDummyUNSO_ONDO_KB, sGOODS_DOKU_KB, sGOODS_ONDO_KB, iGOODS_ONDO_MX, iMATOME_ONDO)

            End If

            '---------------------------------------------------------------------------
            ' EDI受信データの新規追加
            '---------------------------------------------------------------------------
            '別インスタンス
            Dim setDs As DataSet = ds.Copy()
            Dim setDtlDt As DataTable = setDs.Tables("LMH030_OUTKAEDI_DTL_LNZ")
            setDtlDt.Clear()
            setDtlDt.ImportRow(dtRcvDtl.Rows(0))

            '各種設定
            setDtlDt.Rows(0).Item("DEL_KB") = iSkipFlg.ToString         'iSkipFlgを削除区分の値として使用する

            setDtlDt.Rows(0).Item("UNSO_ONDO_KB") = sUNSO_ONDO_KB       '運送温度区分
            setDtlDt.Rows(0).Item("MATOME_REIZO") = sMATOME_REIZO       'まとめ用冷蔵

            setDtlDt.Rows(0).Item("SET_GROUP_NO") = sSET_GROUP_NO       'セットグループ№
            setDtlDt.Rows(0).Item("SET_FLG") = sSET_FLG                 'セットフラグ
            setDtlDt.Rows(0).Item("SET_ONDO_KB") = sSET_ONDO_KB         'セット温度区分

            'セットの子の場合のみ親コードを設定する（セット親には設定しない）
            If sSET_FLG = "1" Then
                'セット親の場合
                setDtlDt.Rows(0).Item("SET_OYA_CD") = String.Empty      'セット親コード
            Else
                'セット親以外の場合
                setDtlDt.Rows(0).Item("SET_OYA_CD") = sSET_OYA_CD       'セット親コード
            End If

            setDtlDt.Rows(0).Item("GOODS_DOKU_KB") = sGOODS_DOKU_KB     '劇毒区分       （商品マスタより（セット品親は未設定））
            setDtlDt.Rows(0).Item("GOODS_ONDO_KB") = sGOODS_ONDO_KB     '温度管理区分   （商品マスタより（セット品親は未設定））
            setDtlDt.Rows(0).Item("GOODS_ONDO_MX") = iGOODS_ONDO_MX     '温度上限       （商品マスタより（セット品親は未設定））
            setDtlDt.Rows(0).Item("MATOME_ONDO") = iMATOME_ONDO         '上記温度上限をまとめ用に変換後の温度

            ' EDI受信データ(DTL)の新規追加
            setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiRcvDtl", setDs)
            iRcvDtlInsCnt = iRcvDtlInsCnt + 1

        Next

        'エラー無しの場合のみチェック
        If bNoErr Then
            'セット品が全て展開済であるかをチェックする
            If iSetCnt > 0 Then
                'エラー！！
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E515", New String() {String.Concat("")}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bNoErr = False
            End If
        End If

        If bNoErr Then
            'エラー無し
            dtSetHed.Rows(0).Item("ERR_FLG") = "0"
        Else
            'エラー有り
            dtSetHed.Rows(0).Item("ERR_FLG") = "1"
            Return ds
        End If


        '---------------------------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------------------------
        '  ②EDI出荷データに登録する
        '  　H_OUTKAEDI_DTL_LNZをSAP_DEST_CD、OUTKA_PLAN_DATE、ARR_DATE、GOODS_DOKU_KB、MATOME_ONDOでソートして
        '  　H_OUTKAEDI_L, Mに登録する
        '　※ただしH_OUTKAEDI_DTL_LNZのセット親を除く。
        '　　したがってH_OUTKAEDI_DTL_LNZの件数≧H_OUTKAEDI_Mの件数となる（セット親の件数分、少なくなる）
        '---------------------------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------------------------
        iGroupCnt = 0
        iPageCnt = 0
        iRecCnt = 0

        '---------------------------------------------------------------------------
        ' 受信EDIデータをまとめ順にSELECTする
        '---------------------------------------------------------------------------
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiDtlForOutokaEdi", ds)

        '---------------------------------------------------------------------------
        ' EDI出荷データに登録する
        '---------------------------------------------------------------------------
        iSetDtlMax = ds.Tables("LMH030_OUTKAEDI_DTL_LNZ").Rows.Count - 1

        For i As Integer = 0 To iSetDtlMax

            '---------------------------------------------------------------------------
            ' EDI受信テーブル⇒EDI受信データセット
            '---------------------------------------------------------------------------
            '別インスタンス
            Dim setDs As DataSet = ds.Copy()
            Dim setDtlDt As DataTable = setDs.Tables("LMH030_OUTKAEDI_DTL_LNZ")
            setDtlDt.Clear()
            drEdiRcvDtl = ds.Tables("LMH030_OUTKAEDI_DTL_LNZ").Rows(i)
            setDtlDt.ImportRow(drEdiRcvDtl)

            '---------------------------------------------------------------------------
            ' キー項目設定
            '---------------------------------------------------------------------------
            sNewKey = String.Concat(setDtlDt.Rows(0).Item("SAP_DEST_CD").ToString, "-", _
                                    setDtlDt.Rows(0).Item("OUTKA_PLAN_DATE").ToString, "-", _
                                    setDtlDt.Rows(0).Item("ARR_DATE").ToString, "-", _
                                    setDtlDt.Rows(0).Item("GOODS_DOKU_KB").ToString, "-", _
                                    setDtlDt.Rows(0).Item("MATOME_ONDO").ToString)

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
                End If
            End If

            '---------------------------------------------------------------------------
            ' EDI管理番号(大,中)の採番(キャンセルフラグが０の場合も関数内で判断
            '---------------------------------------------------------------------------
            ds = Me.GetEdiCtlNo(ds, iDeleteFlg, iSkipFlg, bSameKeyFlg, sEdiCtlNo, iEdiCtlNoChu)

            'GroupCnt、PageCnt、RecCntのカウント
            If bSameKeyFlg = False Then
                iGroupCnt = iGroupCnt + 1
                iPageCnt = iPageCnt + 1
                iRecCnt = 0
            End If

            '---------------------------------------------------------------------------
            ' EDI受信データのEDI管理番号の更新処理を行う
            '---------------------------------------------------------------------------
            setDtlDt.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo
            setDtlDt.Rows(0).Item("EDI_CTL_NO＿CHU") = iEdiCtlNoChu.ToString("000")

            'EDI受信(DTL)の更新
            setDs = MyBase.CallDAC(Me._Dac, "UpdateOutkaRcvDtl", setDs)

            '---------------------------------------------------------------------------
            ' EDI出荷データの追加処理を行う
            '---------------------------------------------------------------------------
            '1ページ明細6行印字（クリレポ　サブレポートリンク対応）
            iRecCnt = iRecCnt + 1
            If iRecCnt > 6 Then
                iGroupCnt = iGroupCnt + 1
                iRecCnt = 1
            End If

            '受信DTL⇒EDI出荷(中)へのデータセット(上記で取得した商品情報も含む)
            setDs = Me.SetSemiOutkaEdiM(setDs, sWhcd, sCustCdL, sCustCdM, sNrsGoodsCd, sNrsGoodsNm, sIrime, _
                                        "", iGroupCnt, iRecCnt, iPageCnt, False)

            'EDI出荷(中)の新規追加
            setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiM", setDs)
            iOutDtlInsCnt = iOutDtlInsCnt + 1

            '前行と差異がある場合は、EDI出荷(大)を新規追加
            If bSameKeyFlg = False Then

                '受信DTL⇒EDI出荷(大)へのデータセット
                setDs = Me.SetSemiOutkaEdiL(setDs, sWhcd, sCustCdL, sCustCdM, sGsEdiCtlNo, False)

                'EDI出荷(大)の新規追加
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiL", setDs)
                iOutHedInsCnt = iOutHedInsCnt + 1


            End If

            'キーを入れ替えるのはiSkipFlgの値で判断する
            '※iSkipFlg = 1の場合、sOldKeyは前行の値である必要があるため 
            If iSkipFlg = 0 Then
                sOldKey = sNewKey   'OldキーにNewキーをセット
            End If

        Next

        If bNoErr Then
            'エラー無し
            dtSetHed.Rows(0).Item("ERR_FLG") = "0"
        Else
            'エラー有り
            dtSetHed.Rows(0).Item("ERR_FLG") = "1"
            Return ds
        End If


        '---------------------------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------------------------
        '  ③EDI出荷データに登録する
        '  　H_OUTKAEDI_DTL_LNZをDELIVERY_NO、SAP_DEST_CDでソートして
        '  　H_OUTKAEDI_L_PRT_LNZ,H_OUTKAEDI_M_PRT_LNZに登録する
        '　※ただしH_OUTKAEDI_DTL_LNZのセット親を含む。
        '　　したがってH_OUTKAEDI_DTL_LNZの件数＝H_OUTKAEDI_M_PRT_LNZの件数となる
        '---------------------------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------------------------
        iGroupCnt = 0
        iPageCnt = 0
        iRecCnt = 0

        '---------------------------------------------------------------------------
        ' 受信EDIデータをまとめ順にSELECTする
        '---------------------------------------------------------------------------
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiDtlForOutokaEdiPrt", ds)

        '---------------------------------------------------------------------------
        ' EDI出荷データに登録する
        '---------------------------------------------------------------------------
        iSetDtlMax = ds.Tables("LMH030_OUTKAEDI_DTL_LNZ").Rows.Count - 1

        For i As Integer = 0 To iSetDtlMax

            '---------------------------------------------------------------------------
            ' EDI受信テーブル⇒EDI受信データセット
            '---------------------------------------------------------------------------
            '別インスタンス
            Dim setDs As DataSet = ds.Copy()
            Dim setDtlDt As DataTable = setDs.Tables("LMH030_OUTKAEDI_DTL_LNZ")
            setDtlDt.Clear()
            drEdiRcvDtl = ds.Tables("LMH030_OUTKAEDI_DTL_LNZ").Rows(i)
            setDtlDt.ImportRow(drEdiRcvDtl)

            '---------------------------------------------------------------------------
            ' キー項目設定
            '---------------------------------------------------------------------------
            sNewKey = String.Concat(setDtlDt.Rows(0).Item("DELIVERY_NO").ToString, "-", _
                                    setDtlDt.Rows(0).Item("SAP_DEST_CD").ToString)

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
                End If
            End If

            '---------------------------------------------------------------------------
            ' EDI管理番号(大,中)の採番(キャンセルフラグが０の場合も関数内で判断
            '---------------------------------------------------------------------------
            ds = Me.GetEdiCtlNo(ds, iDeleteFlg, iSkipFlg, bSameKeyFlg, sEdiCtlNo, iEdiCtlNoChu)

            'GroupCnt、PageCnt、RecCntのカウント
            If bSameKeyFlg = False Then
                iGroupCnt = iGroupCnt + 1
                iPageCnt = iPageCnt + 1
                iRecCnt = 0
                '---------------------------------------------------------------------------
                ' ロンザ出荷ＥＤＩデータのデリバリーNo. + SAP納入先ｺｰﾄﾞで最小のEDI_CTL_NOを取得する
                '---------------------------------------------------------------------------
                sGsEdiCtlNo = Me._Dac.SelectEdiDtlForOutokaEdiPrt_EdiCtlNo(setDs)
            End If

            '---------------------------------------------------------------------------
            ' EDI受信データのEDI管理番号の設定（更新処理は行わない）
            '---------------------------------------------------------------------------
            setDtlDt.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo
            setDtlDt.Rows(0).Item("EDI_CTL_NO＿CHU") = iEdiCtlNoChu.ToString("000")

            ' ''EDI受信(DTL)の更新
            ''setDs = MyBase.CallDAC(Me._Dac, "UpdateOutkaRcvDtl", setDs)

            '---------------------------------------------------------------------------
            ' EDI出荷データの追加処理を行う
            '---------------------------------------------------------------------------
            '1ページ明細6行印字（クリレポ　サブレポートリンク対応）
            iRecCnt = iRecCnt + 1
            If iRecCnt > 6 Then
                iGroupCnt = iGroupCnt + 1
                iRecCnt = 1
            End If

            '受信DTL⇒EDI出荷(中)へのデータセット(上記で取得した商品情報も含む)
            setDs = Me.SetSemiOutkaEdiM(setDs, sWhcd, sCustCdL, sCustCdM, sNrsGoodsCd, sNrsGoodsNm, sIrime, _
                                        sGsEdiCtlNo, iGroupCnt, iRecCnt, iPageCnt, True)

            'EDI出荷(中)の新規追加
            setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiM_PRT_LNZ", setDs)

            '前行と差異がある場合は、EDI出荷(大)を新規追加
            If bSameKeyFlg = False Then

                '受信DTL⇒EDI出荷(大)へのデータセット
                setDs = Me.SetSemiOutkaEdiL(setDs, sWhcd, sCustCdL, sCustCdM, sGsEdiCtlNo, True)

                'EDI出荷(大)の新規追加
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiL_PRT_LNZ", setDs)

            End If

            'キーを入れ替えるのはiSkipFlgの値で判断する
            '※iSkipFlg = 1の場合、sOldKeyは前行の値である必要があるため 
            If iSkipFlg = 0 Then
                sOldKey = sNewKey   'OldキーにNewキーをセット
            End If

        Next

        If bNoErr Then
            'エラー無し
            dtSetHed.Rows(0).Item("ERR_FLG") = "0"
        Else
            'エラー有り
            dtSetHed.Rows(0).Item("ERR_FLG") = "1"
            Return ds
        End If


        '処理件数
        dtSetRet.Rows(0).Item("RCV_HED_INS_CNT") = iRcvHedInsCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_INS_CNT") = iRcvDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_HED_INS_CNT") = iOutHedInsCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_DTL_INS_CNT") = iOutDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_HED_CAN_CNT") = iRcvHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_CAN_CNT") = iRcvDtlCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_HED_CAN_CNT") = iOutHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_DTL_CAN_CNT") = iOutDtlCanCnt.ToString()

        Return ds

    End Function

#End Region

#Region "営業日取得"
    ''' <summary>
    ''' 営業日取得
    ''' </summary>
    ''' <param name="sStartDay"></param>
    ''' <param name="iBussinessDays"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBussinessDay(ByVal sStartDay As String, ByVal iBussinessDays As Integer, ByVal setDs As DataSet) As DateTime
        'sStartDate     ：基準日（YYYYMMDD形式）
        'iBussinessDays ：基準日からの営業日数（前々営業日の場合は-2、前営業日の場合は-1、翌営業日の場合は+1、翌々営業日の場合は+2）
        '戻り値         ：求めた営業日（YYYY/MM/DD形式）

        Dim drHOL As DataRow

        'スラッシュを付加して日付型に変更
        Dim dBussinessDate As DateTime = Convert.ToDateTime(Me._Blc.GetSlashEditDate(sStartDay))

        For i As Integer = 1 To System.Math.Abs(iBussinessDays)  'マイナス値に対応するため絶対値指定

            '基準日からの営業日数分、Doループを繰り返す
            Do
                '日付加算
                If iBussinessDays > 0 Then
                    dBussinessDate = dBussinessDate.AddDays(1)      '翌営業日
                Else
                    dBussinessDate = dBussinessDate.AddDays(-1)     '前営業日
                End If

                If Weekday(dBussinessDate) = 1 OrElse Weekday(dBussinessDate) = 7 Then
                Else
                    '土日でない場合
                    setDs.Tables("LMH030_M_HOL").Clear()

                    '休日マスタ参照
                    drHOL = setDs.Tables("LMH030_M_HOL").NewRow()
                    drHOL("HOL") = Format(dBussinessDate, "yyyyMMdd")
                    'データセットに設定
                    setDs.Tables("LMH030_M_HOL").Rows.Add(drHOL)

                    '休日マスタの値を取得
                    setDs = MyBase.CallDAC(Me._DacCom, "SelectMHolList", setDs)

                    If MyBase.GetResultCount = 0 Then
                        '休日マスタに存在しない場合、dBussinessDateが求める日
                        Exit Do
                    End If

                End If
            Loop
        Next

        Return dBussinessDate

    End Function

#End Region

#End Region

End Class
