' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID　　　　:  125　　　 : 千葉東レ
'  作  成  者       :  kido
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC132
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC132 = New LMH030DAC132()

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

    ''' <summary>
    ''' 倉庫/荷主L/荷主M
    ''' </summary>
    ''' <remarks></remarks>
    Private _WhCd As String = String.Empty            '倉庫コード
    Private _CustCdL As String = String.Empty         '荷主コード（大）
    Private _CustCdM As String = String.Empty         '荷主コード（中）

#End Region

#Region "Const"

#If True Then       'ADD 2018/11/14 依頼番号 : 002600   【LMS】東レ_運送セミEDI_個数マイナスデータ対応
    'BN欄 指図状態　　
    'A ＝ 新規
    'M ＝ 修正前
    'S = 修正後
    'C＝キャンセル

    ''' <summary>
    ''' 指図状態 新規
    ''' </summary>
    ''' <remarks></remarks>
    Public Const sizuJyotai_A As String = "A"

    ''' <summary>
    ''' 指図状態 修正前
    ''' </summary>
    ''' <remarks></remarks>
    Public Const sizuJyotai_M As String = "M"

    ''' <summary>
    ''' 指図状態 修正後
    ''' </summary>
    ''' <remarks></remarks>
    Public Const sizuJyotai_S As String = "S"

    ''' <summary>
    ''' 指図状態 キャンセル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const sizuJyotai_C As String = "C"

#End If


#Region "汎用商品コード(ディック共同配送)CONST"

    '要望番号:1209(出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 Start
    'Public Const NRS_GOODS_CD_UNSO As String = "B0000999999999999999"           '商品コード（運送）
    Public Const NRS_GOODS_CD_UNSO As String = ""                               '商品コード（運送）空を送る
    '要望番号:1209(出荷EDI→運送登録仕様見直し②商品マスタの存在チェックは外す) 2012/06/28 本明 End
    Public Const IRIME_UNSO As String = "0"                 '入目値（運送）

    ''' <summary>
    ''' その他
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DEF_CTL_NO As String = "C00000000"             '管理番号初期値
    Public Const DEF_UNSO_NO_L As String = "01-C00000000"       '運送番号初期値
    Public Const DEF_UNSO_NO_M As String = "01-C00000000000"    '運送番号初期値

#End Region

#End Region

#Region "Method"

#Region "運送登録処理"
    ''' <summary>
    ''' 運送登録処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OutkaToroku(ByVal ds As DataSet) As DataSet

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        Dim ediCustIdx As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CUST_INDEX").ToString()

        'EDI出荷(大)の値取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        If ds.Tables("LMH030_OUTKAEDI_L").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        '要望番号1282 追加START
        '荷主明細マスタの取得
        ds = MyBase.CallDAC(Me._DacCom, "SelectListDataMcustDetails2", ds)

        '汎用届先自動追加フラグの取得
        Dim genericInsFlg As Integer = 0
        If ds.Tables("LMH030_M_CUST_DETAILS").Rows.Count > 0 Then
            genericInsFlg = Me.GenericDestCheck(ds)
        End If

        '要望番号1282 追加END

        'EDI出荷(大)の初期値設定
        ds = Me.SetEdiLShoki(ds)

        '2012.06.05 ディック共同配送用 修正START
        '登録前の事前チェック（届先JISコード）
        ds = MyBase.CallDAC(Me._Dac, "EntryBeforeCheck", ds)

        'データが存在しない場合はエラー
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E457", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        '2012.06.05 ディック共同配送用 修正END

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

        ''要望番号1282 修正START 2012.07.19
        'EDI出荷(大)の初期値設定後のDB存在チェック
        If Me.EdiLDbExistsCheck(ds, rowNo, ediCtlNo, genericInsFlg) = False Then
            'If Me.EdiLDbExistsCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If
        ''要望番号1282 修正END 2012.07.19

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

        '2012.06.05 ディック共同配送用 修正START
        '出荷管理番号(大)の採番
        'ディック共同配送の場合は運送登録なので、出荷は作成しない
        ds = Me.GetOutkaNoL(ds)
        '2012.06.05 ディック共同配送用 修正End

        '2012.06.05 ディック共同配送用 修正START
        ''出荷管理番号(中)の採番
        'ディック共同配送の場合は運送登録なので、出荷は作成しない
        ds = Me.GetOutkaNoM(ds)
        '2012.06.05 ディック共同配送用 修正End

        '2012.06.05 ディック共同配送用 修正START
        '注意)ディック共同配送の場合は運送登録なので、出荷は作成しないがデータセット設定は行う
        '理由)後続の運送設定の際に、出荷(大)で設定された運送個数を使用したい為
        '出荷(大)データセット設定処理
        ds = Me.SetDatasetOutkaL(ds)

        ''要望番号1281 DIC春日部 タイムアウト不具合対応　コメントSTART
        ''EDI受信テーブル(HED)データセット設定
        'ds = Me.SetDatasetEdiRcvHed(ds)

        ''EDI受信テーブル(DTL)データセット設定
        'ds = Me.SetDatasetEdiRcvDtl(ds)
        ''要望番号1281 DIC春日部 タイムアウト不具合対応　コメントEND

        '運送(大,中)データセット設定
        ds = Me.SetDatasetUnsoL(ds)
        ds = Me.SetDatasetUnsoM(ds)

        '運送(大)の運送重量をデータセットに再設定
        ds = Me.SetdatasetUnsoJyuryo(ds)

        '運送登録(通常処理)
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        ' ''要望番号1281 DIC春日部 タイムアウト不具合対応　コメントSTART
        ''EDI受信(HED)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

        ''EDI受信(DTL)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)
        ' ''要望番号1281 DIC春日部 タイムアウト不具合対応　コメントEND

        '2012.06.05 ディック共同配送用

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
        '届先マスタの自動更新

        '2012.06.05 ディック共同配送用

        '運送(大)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_L").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoLData", ds)
        End If

        '運送(中)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_M").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoMData", ds)
        End If

        Return ds

    End Function
#End Region

#Region "EDI_Lの初期値設定(運送登録処理)"
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
        Else
            'DACで値セットを行う
            '2012.02.25 大阪対応 START
            'ds = MyBase.CallDAC(Me._DacCom, "SetShipNmL", ds)
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdestShip", ds)
            If ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows.Count <> 0 Then
                ediDr("SHIP_NM_L") = ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("DEST_NM").ToString().Trim()
            End If
            '2012.02.25 大阪対応 END
        End If

        '指定納品書区分
        If String.IsNullOrEmpty(ediDr("SP_NHS_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("SP_NHS_KB") = mDestDr("SP_NHS_KB").ToString().Trim()
            End If
        End If

        '追加箇所 20120222
        '分析票添付区分
        If String.IsNullOrEmpty(ediDr("COA_YN").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("COA_YN") = mDestDr("COA_YN").ToString().Trim().Substring(1, 1)

                '要望番号:483((出荷登録時)EDI出荷(大,中)の更新不具合の"COA_YN") 2012/06/21 本明 Start
            Else
                '届先マスタに存在しない場合、自動追加の値と同値をセットする
                ediDr("COA_YN") = "2"  'SetInsMDestFromDestの値と一致させる事！（荷主により値が異なるため）
                '要望番号:483((出荷登録時)EDI出荷(大,中)の更新不具合の"COA_YN") 2012/06/21 本明 End
            End If
        End If
        '追加箇所 20120222

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
                If String.IsNullOrEmpty(mDestDr("DELI_ATT").ToString().Trim()) = False AndAlso _
                   String.IsNullOrEmpty(ediDr("UNSO_ATT").ToString().Trim()) = True Then

                    ediDr("UNSO_ATT") = mDestDr("DELI_ATT").ToString().Trim()
                Else
                    If InStr(ediDr("UNSO_ATT").ToString().Trim(), mDestDr("DELI_ATT").ToString().Trim()) > 0 Then
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
                ''運送会社マスタの存在チェック
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

        '追加箇所 20120222
        '運賃請求有無
        If (ediDr("UNSO_MOTO_KB").ToString()).Equals("10") = True OrElse _
           (ediDr("UNSO_MOTO_KB").ToString()).Equals("40") = True Then
            ediDr("UNCHIN_YN") = "1"
        Else
            ediDr("UNCHIN_YN") = "0"
        End If
        '追加箇所 20120222

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
                '2012.04.04 要望番号943 修正START
                ediDr("DEST_CD") = ds.Tables("LMH030_M_DEST").Rows(0)("DEST_CD").ToString().Trim()
                '2012.04.04 要望番号943 修正END
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
        Dim compareWarningFlg As Boolean = False

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)
        'ディック春日部追加箇所 20120611 terakawa Start
        Dim custIndex As Integer = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CUST_INDEX"))
        'ディック春日部追加箇所 20120611 terakawa End

        '要望番号1246 2012.07.06 umano 追加START
        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        '要望番号1246 2012.07.06 umano 追加END


        '分析表区分
        If String.IsNullOrEmpty(ediMDr("COA_YN").ToString()) = True Then

            '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 Start
            'ediMDr("COA_YN") = (mGoodsDr("COA_YN").ToString()).Substring(1, 1)
            'mGoodsDr("COA_YN")が２文字の場合のみSubstring(1, 1)を行う（SubstringでAbendするため）
            If Len(mGoodsDr("COA_YN").ToString()) = 2 Then
                ediMDr("COA_YN") = (mGoodsDr("COA_YN").ToString()).Substring(1, 1)
            Else
                ediMDr("COA_YN") = String.Empty
            End If
            '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し②商品マスタの存在チェックは外す) 2012/06/28 本明 End

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
        ediMDr("NRS_GOODS_CD") = mGoodsDr("GOODS_CD_NRS")

        '商品名
        If unsodata.Equals("01") = False Then
            ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1")
        End If

        '引当単位区分
        If String.IsNullOrEmpty(ediMDr("ALCTD_KB").ToString()) = True Then
            If String.IsNullOrEmpty(mGoodsDr("ALCTD_KB").ToString()) = False Then

                ediMDr("ALCTD_KB") = mGoodsDr("ALCTD_KB")
            Else
                ediMDr("ALCTD_KB") = "01"
            End If
        End If

        'ディック春日部追加箇所 terakawa 20120611 Start
        '2012.06.05 ディック共同配送
        Select Case custIndex
            Case LMH030BLC.EdiCustIndex.DicItk10005_00
                'ディック共同配送の場合、通常の出荷データと同じで以下の４項目は商品マスタの値に入れ替え
                '↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
                '個数単位
                ediMDr("KB_UT") = mGoodsDr("NB_UT")

                '数量単位
                ediMDr("QT_UT") = mGoodsDr("STD_IRIME_UT")

                '包装個数
                ediMDr("PKG_NB") = mGoodsDr("PKG_NB")

                '包装単位
                ediMDr("PKG_UT") = mGoodsDr("PKG_UT")

                '↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
            Case Else
                'ディック共同配達以外の場合は、商品マスタの値に入れ替えない
        End Select
        'ディック春日部追加箇所 terakawa 20120611 End

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

        '入目単位
        If String.IsNullOrEmpty(ediMDr("IRIME_UT").ToString()) = True Then
            ediMDr("IRIME_UT") = mGoodsDr("STD_IRIME_UT")
        Else
            If unsodata.Equals("01") = False AndAlso ediMDr("IRIME_UT").Equals(mGoodsDr("STD_IRIME_UT")) = False Then
                '運送データ以外でEDI(中)と商品マスタで入目単位が異なる場合はエラー
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"入目単位", "商品マスタ", "入目単位"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
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
                'ディック春日部暫定対応 terakawa 2012.06.18 Start
                If 1 < pkgNb Then
                    ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
                    ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb
                    'ediMDr("OUTKA_TTL_QT") = (pkgNb * outkaPkgNb + outkaHasu) * irime 入力ファイルの数量を適用するためコメント
                Else
                    ediMDr("OUTKA_PKG_NB") = outkaPkgNb + outkaHasu
                    ediMDr("OUTKA_HASU") = 0
                    'ediMDr("OUTKA_TTL_QT") = (outkaPkgNb + outkaHasu) * irime 入力ファイルの数量を適用するためコメント
                End If

                'If 1 < pkgNb Then
                '    ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
                '    ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb
                'Else
                '    ediMDr("OUTKA_PKG_NB") = pkgNb * outkaPkgNb + outkaHasu
                '    ediMDr("OUTKA_HASU") = 0
                'End If

                'ediMDr("OUTKA_TTL_QT") = (pkgNb * outkaPkgNb + outkaHasu) * irime
                'ディック春日部暫定対応 terakawa 2012.06.18 End

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

        '要望番号:1243(【春日部要望】赤データの表示) 2012/07/05 本明 Start（マイナスエラーをコメント化）
        ''要望番号:1209(出荷EDI→運送登録仕様見直し②重量がマイナスの場合はエラーとする) 2012/06/28 本明 Start
        'Dim dBetuWt As Double = Convert.ToDouble(ediMDr("BETU_WT").ToString)
        'If dBetuWt < 0 Then
        '    '重量がマイナスの場合はエラーとする
        '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E185", New String() {"個別重量"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '    Return False
        'End If
        ''要望番号:1209(出荷EDI→運送登録仕様見直し②重量がマイナスの場合はエラーとする) 2012/06/28 本明 End
        '要望番号:1243(【春日部要望】赤データの表示) 2012/07/05 本明 End（マイナスエラーをコメント化）

        '要望番号1246 2012.07.06 umano 追加START
        ''-------------------------------------------------------------------------------------
        ''●荷主固有チェック(DIC春日部専用)個別重量
        ''-------------------------------------------------------------------------------------

        Dim dBetuWt As Double = Convert.ToDouble(ediMDr("BETU_WT").ToString)
        If dBetuWt < 0 Then
            '重量がマイナスの場合はワーニングとする

            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.DICUNSO_WID_M002, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "個別重量"
                msgArray(2) = String.Empty
                msgArray(3) = String.Empty
                msgArray(4) = String.Empty
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningM("W210", LMH030BLC.DICUNSO_WID_M002, ds, setDs, msgArray, ediMDr("BETU_WT").ToString, String.Empty)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時

            End If

        End If
        '要望番号1246 2012.07.06 umano 追加END

        '現LMSと異なりこの時点で重量計算を行う為
        If unsodata.Equals("01") = False Then
            ediMDr("BETU_WT") = mGoodsDr("STD_WT_KGS")
        End If

        '出荷時加工作業区分1-5
        ediMDr("OUTKA_KAKO_SAGYO_KB_1") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_1")
        ediMDr("OUTKA_KAKO_SAGYO_KB_2") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_2")
        ediMDr("OUTKA_KAKO_SAGYO_KB_3") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_3")
        ediMDr("OUTKA_KAKO_SAGYO_KB_4") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_4")
        ediMDr("OUTKA_KAKO_SAGYO_KB_5") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_5")

        '2013.01.18ディック千葉輸送対応START

        Dim mDicDestDr As DataRow = Nothing

        If LMH030BLC.EdiCustIndex.TORAY00951_00 = custIndex Then  '荷主コード(10010)と同じ動き

            'EDI出荷(中)レベルに持っている届先CDでORIG情報(発地住所・発地JISCD)を取得
            ds = MyBase.CallDAC(Me._Dac, "SelectDicYusoOrigData", ds)

            If MyBase.GetResultCount = 0 Then
                ds = MyBase.CallDAC(Me._Dac, "SelectDicYusoOrigEditData", ds)
                If MyBase.GetResultCount = 0 Then
                Else
                    mDicDestDr = ds.Tables("LMH030_M_DEST_ORIG").Rows(0)
                    ediMDr("FREE_C11") = mDicDestDr("AD_1")
                    ediMDr("FREE_C12") = mDicDestDr("JIS")
                End If
            Else
                mDicDestDr = ds.Tables("LMH030_M_DEST_ORIG").Rows(0)
                ediMDr("FREE_C11") = mDicDestDr("AD_1")
                ediMDr("FREE_C12") = mDicDestDr("JIS")
            End If

        Else
            'DIC千葉輸送以外は取得しない
        End If
        '2013.01.18ディック千葉輸送対応END

        'ワーニングが存在する場合はここでの判定はFalseで返す
        'DIC春日部でワーニングを使用
        If compareWarningFlg = True Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "入力チェック(運送登録処理)"

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
    Private Function EdiLDbExistsCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String, _
                                       ByVal genericInsFlg As Integer) As Boolean
        '要望番号1282 修正END 2012.07.19

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim drIn As DataRow = ds.Tables("LMH030INOUT").Rows(0)

        '-------------------------------------------------------------------------------------
        '●荷主共通チェック
        '-------------------------------------------------------------------------------------
        ''オーダー番号重複チェック
        If String.IsNullOrEmpty(drEdiL.Item("CUST_ORD_NO").ToString) = False Then

            If drIn("ORDER_CHECK_FLG").Equals("1") = True Then
#If False Then  'upd 2018/09/12 依頼番号 : 002436   【LMS】千葉東レ_セミEDI運送バグ
                Call MyBase.CallDAC(Me._DacCom, "SelectOrderCheckData", ds)
#Else
                '運送Lでのチェック
                Call MyBase.CallDAC(Me._Dac, "SelectOrderCheckUnsoData", ds)
#End If
                If MyBase.GetResultCount > 0 Then
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"出荷データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"運送データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
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
        '●荷主固有チェック(ディック共同配送専用)
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
            '2012.06.29 届先コードが空の場合はエラー Start
            'DEST_CDとEDI_DEST_CDが両方空の場合、エラーとする。
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"届先(EDI)コードが空", "運送登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
            '2012.06.29 届先コードが空の場合はエラー End
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
            '0件の場合、届先マスタの自動追加をする
            ''要望番号1282 修正START 2012.07.19
            If SetInsMDestFromDest(ds, workDestCd, workDestString, rowNo, ediCtlNo, genericInsFlg) = False Then
                'If SetInsMDestFromDest(ds, workDestCd, workDestString, rowNo, ediCtlNo) = False Then
                ''要望番号1282 修正END 2012.07.19
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
        'ディック春日部追加箇所 20120611 terakawa Start
        Dim custIndex As Integer = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CUST_INDEX"))
        'ディック春日部追加箇所 20120611 terakawa End

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
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "運送登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '入目 + 出荷総数量
        If Me._Blc.IrimeSosuryoLargeSmallCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"入目と出荷総数量"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '赤黒区分
        'ディック春日部追加箇所 20120611 terakawa Start
        '（春日部）DIC春日部
        '（春日部）DIC春日部顔料
        Select Case custIndex
            Case LMH030BLC.EdiCustIndex.DicKkb10001_00 _
                , LMH030BLC.EdiCustIndex.DicKkb10001_03
                '上記荷主は赤黒区分チェックを行わない
            Case Else
                If Me._Blc.AkakuroKbCheck(dtM) = False Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "運送登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If
        End Select

        Return True
        'ディック春日部追加箇所 20120611 terakawa End
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
            If String.IsNullOrEmpty(dtL.Rows(0).Item("FREE_C30").ToString()) = False Then
                unsoData = dtL.Rows(0).Item("FREE_C30").ToString().Substring(0, 2)
            End If

            If String.IsNullOrEmpty(custGoodsCd) = False Then

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDtL.ImportRow(dtL.Rows(0))
                setDtM.ImportRow(dtM.Rows(i))

                choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.DICUNSO_WID_M001, 0)

                '商品マスタ検索（NRS商品コード or 荷主商品コード）
                setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsOutka", setDs))

                '基本的にディック共同配送の場合は、共同配送の汎用商品コードの商品キーが入ってくる為１件になる。
                If MyBase.GetResultCount = 0 Then


                    '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 Start
                    '商品マスタが存在しない場合はダミーで商品データセットを作成する
                    setDs = Me._Blc.SetDummyGoodsM(ds, setDs, i)
                    '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 End

                    '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 Start（以下コメント化）
                    ''要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                    ''MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"商品コード", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    ''要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End

                    'Return False
                    '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 End（以上コメント化）

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

                        ds = Me._Blc.SetComWarningM("W162", LMH030BLC.DICUNSO_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)

                        flgWarning = True 'ワーニングフラグをたてて処理続行

                        Continue For
                    End If

                End If

                'EDI出荷(中)の初期値設定処理
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                    'ディック共同配送は現段階ではワーニングはないが、共通のロジックを組み込む為入れておく
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

        Dim mSysDelF As String = dtMdest.Rows(0).Item("SYS_DEL_FLG").ToString()
        Dim mDestNm As String = dtMdest.Rows(0).Item("DEST_NM").ToString()
        Dim mAd1 As String = dtMdest.Rows(0).Item("AD_1").ToString()
        Dim mAd2 As String = dtMdest.Rows(0).Item("AD_2").ToString()
        Dim mAd3 As String = dtMdest.Rows(0).Item("AD_3").ToString()
        Dim mTel As String = dtMdest.Rows(0).Item("TEL").ToString()
        Dim mJis As String = dtMdest.Rows(0).Item("JIS").ToString()
        Dim mSpNhsKb As String = dtMdest.Rows(0).Item("SP_NHS_KB").ToString()
        Dim mCoaYn As String = dtMdest.Rows(0).Item("COA_YN").ToString()
        Dim mAdAll As String = String.Concat(mAd1, mAd2, mAd3)
        Dim mZipJis As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediDestCd As String = dtEdi.Rows(0)("DEST_CD").ToString()
        Dim ediDestNm As String = dtEdi.Rows(0)("DEST_NM").ToString()
        Dim ediTel As String = dtEdi.Rows(0)("DEST_TEL").ToString()
        Dim ediFreeC21 As String = dtEdi.Rows(0)("FREE_C21").ToString()
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()
        Dim ediSpNhsKb As String = dtEdi.Rows(0)("SP_NHS_KB").ToString()
        Dim ediCoaYn As String = dtEdi.Rows(0)("COA_YN").ToString()

        Dim compareWarningFlg As Boolean = False

        '2013.01.18ディック千葉輸送対応START
        Dim custIndex As Integer = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CUST_INDEX"))
        '2013.01.18ディック千葉輸送対応END


        '削除フラグ(届先マスタ)
        If mSysDelF.Equals("1") = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E331", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        mDestNm = Me.SpaceCutChk(mDestNm)
        ediDestNm = Me.SpaceCutChk(ediDestNm)

        '届先名称(マスタ値が完全一致でなければワーニング)
        If mDestNm.Equals(ediDestNm) = True Then
            'チェックなし
        Else
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DICUNSO_WID_L001, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "届先名称"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "届先名称"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W166", LMH030BLC.DICUNSO_WID_L001, ds, msgArray, ediDestNm, mDestNm)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("DEST_NM") = dtEdi.Rows(0)("DEST_NM").ToString()
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            End If

        End If

        'FREE_C21:届先住所(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediFreeC21) = True Then
            'チェックなし
        Else

            mAdAll = SpaceCutChk(mAdAll)
            ediFreeC21 = SpaceCutChk(ediFreeC21)
            If mAdAll.Equals(ediFreeC21) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DICUNSO_WID_L002, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先住所"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "住所"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.DICUNSO_WID_L002, ds, msgArray, ediFreeC21, mAdAll)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("AD_1") = dtEdi.Rows(0)("DEST_AD_1").ToString()
                    dtMdest.Rows(0).Item("AD_2") = dtEdi.Rows(0)("DEST_AD_2").ToString()
                    dtMdest.Rows(0).Item("AD_3") = dtEdi.Rows(0)("DEST_AD_3").ToString()
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

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DICUNSO_WID_L003, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先電話番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "電話番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.DICUNSO_WID_L003, ds, msgArray, ediTel, mTel)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("TEL") = dtEdi.Rows(0)("DEST_TEL").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If

        '届先JISコード(マスタ値が完全一致でなければワーニング)
        '地区コードがXXXXの場合はチェックしない
        If String.IsNullOrEmpty(ediDestJisCd) = True OrElse ediDestJisCd.Equals("XXXX") = True Then
            'チェックなし
        Else
            If mJis.Equals(ediDestJisCd) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DICUNSO_WID_L004, 0)

                '2013.01.18ディック千葉輸送対応START
                If LMH030BLC.EdiCustIndex.TORAY00951_00 = custIndex Then  '荷主コード(10010)と同じ動き

                    If String.IsNullOrEmpty(choiceKb) = True Then

                        msgArray(1) = "EDIデータ"
                        msgArray(2) = "届先JISコード"
                        msgArray(3) = "運送登録"
                        msgArray(4) = "注)届先マスタのJISコードと不整合があっても届先マスタの自動更新は行いません。"
                        msgArray(5) = String.Empty
                        ds = Me._Blc.SetComWarningL("W229", LMH030BLC.DICUNSO_WID_L004, ds, msgArray, ediDestJisCd, mJis)

                        compareWarningFlg = True

                    End If
                    '2013.01.18ディック千葉輸送対応END

                Else

                    If String.IsNullOrEmpty(choiceKb) = True Then

                        msgArray(1) = "届先JISコード"
                        msgArray(2) = "届先マスタ"
                        msgArray(3) = "JISコード"
                        msgArray(4) = "EDIデータ"
                        msgArray(5) = String.Empty
                        ds = Me._Blc.SetComWarningL("W166", LMH030BLC.DICUNSO_WID_L004, ds, msgArray, ediDestJisCd, mJis)

                        compareWarningFlg = True

                    ElseIf choiceKb.Equals("01") = True Then
                        'ワーニングで"はい"を選択時
                        dtMdest.Rows(0).Item("JIS") = dtEdi.Rows(0)("DEST_JIS_CD").ToString()
                        'マスタ更新対象フラグ
                        dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                    End If


                End If

            End If

        End If

        '指定納品書区分(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediSpNhsKb) = True Then
            'チェックなし
        Else
            If mSpNhsKb.Equals(ediSpNhsKb) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DICUNSO_WID_L005, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "指定納品書区分"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "指定納品書区分"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.DICUNSO_WID_L005, ds, msgArray, ediSpNhsKb, mSpNhsKb)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("SP_NHS_KB") = dtEdi.Rows(0)("SP_NHS_KB").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If

        '分析票区分(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediCoaYn) = True Then
            'チェックなし
        Else
            If mCoaYn.Equals(FormatZero(ediCoaYn, 2)) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DICUNSO_WID_L006, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "分析表添付区分"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "分析表添付区分"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.DICUNSO_WID_L006, ds, msgArray, ediCoaYn, mCoaYn)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("COA_YN") = FormatZero(dtEdi.Rows(0)("COA_YN").ToString(), 2)
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If

        'ワーニングが存在する場合はここでの判定はFalseで返す
        If compareWarningFlg = True Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "届先マスタ自動追加(ディック共同配送専用)"

    ''' <summary>
    ''' 届先コード(EDI届先コード)から届先マスタInsertデータを作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' ワーニング設定をする
    ''' ワーニング画面の戻り値がある場合、届先コード(EDI届先コード)から届先マスタInsertデータを作成する
    ''' </remarks>
    '''     ''要望番号1282 修正START 2012.07.19
    Private Function SetInsMDestFromDest(ByVal ds As DataSet, ByVal workDestCd As String, ByVal workDestString As String _
                                         , ByVal rowNo As String, ByVal ediCtlNo As String, ByVal genericInsFlg As Integer) As Boolean
        ''要望番号1282 修正END 2012.07.19

        Dim dtMD As DataTable = ds.Tables("LMH030_M_DEST")
        Dim msgArray(5) As String
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim compareWarningFlg As Boolean = False

        Dim choiceKb As String = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.DICUNSO_WID_L007, 0)

        ''要望番号1282 修正START 2012.07.19(汎用届先コードの自動追加の場合はワーニングを出力しない)
        If String.IsNullOrEmpty(choiceKb) = True AndAlso genericInsFlg = 0 Then
            'If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = workDestString
            msgArray(2) = "届先マスタ"
            msgArray(3) = "EDIデータ"
            msgArray(4) = String.Empty

            ds = Me._Blc.SetComWarningL("W182", LMH030BLC.DICUNSO_WID_L007, ds, msgArray, workDestCd, String.Empty)

            compareWarningFlg = True

            ''要望番号1282 修正START 2012.07.19(汎用届先コードの場合はワーニングなしで自動追加)
        ElseIf choiceKb.Equals("01") = True OrElse genericInsFlg = 1 Then
            'ElseIf choiceKb.Equals("01") = True Then

            'ワーニングで"はい"を選択時
            Dim drMD As DataRow = dtMD.NewRow()
            drMD("NRS_BR_CD") = drEdiL("NRS_BR_CD").ToString()
            drMD("CUST_CD_L") = drEdiL("CUST_CD_L").ToString()
            drMD("DEST_CD") = workDestCd
            ''要望番号1282 追加START 2012.07.19
            If genericInsFlg = 0 Then
                drMD("EDI_CD") = workDestCd
            ElseIf genericInsFlg = 1 Then
                drMD("EDI_CD") = drEdiL("EDI_DEST_CD").ToString()
            End If
            ''要望番号1282 追加END 2012.07.19
            drMD("DEST_NM") = drEdiL("DEST_NM").ToString()
            drMD("COA_YN") = "02"
            drMD("TEL") = drEdiL("DEST_TEL").ToString()
            drMD("JIS") = drEdiL("DEST_JIS_CD").ToString()
            drMD("AD_1") = drEdiL("DEST_AD_1").ToString()
            drMD("AD_2") = drEdiL("DEST_AD_2").ToString()
            drMD("AD_3") = drEdiL("DEST_AD_3").ToString()
            drMD("PICK_KB") = "01"
            drMD("BIN_KB") = "01"
            drMD("LARGE_CAR_YN") = "01"
            'マスタ自動追加対象フラグ
            drMD("MST_INSERT_FLG") = "1"
            dtMD.Rows.Add(drMD)

        End If

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

#Region "CUST_ORD_NO連結処理"

    ''' <summary>
    ''' CUST_ORD_NO連結処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CustOrdNoConect(ByVal ds As DataSet) As DataSet

        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim max As Integer = dtM.Rows.Count - 1

        For i As Integer = 0 To max
            '出荷(中)の中番が"001"の場合はEDI出荷(大)のCUST_ORD_NOにEDI出荷(中)のCUST_ORD_NO_DTLをセット
            If dtM.Rows(i).Item("EDI_CTL_NO_CHU").ToString() = "001" Then
                dtL.Rows(0).Item("CUST_ORD_NO") = dtM.Rows(i).Item("CUST_ORD_NO_DTL").ToString().Trim()
            Else
                '出荷(中)の中番が"001"以外の場合は、EDI出荷(大)のCUST_ORD_NOにEDI出荷(中)のCUST_ORD_NO_DTLが含まれていない場合のみ連結
                If InStr(dtL.Rows(0).Item("CUST_ORD_NO").ToString().Trim(), dtM.Rows(i).Item("CUST_ORD_NO_DTL").ToString().Trim()) = 0 Then
                    dtL.Rows(0).Item("CUST_ORD_NO") = String.Concat(dtL.Rows(0).Item("CUST_ORD_NO").ToString().Trim(), _
                                                Space(1), dtM.Rows(i).Item("CUST_ORD_NO_DTL").ToString().Trim())
                End If
            End If
        Next

        Return ds

    End Function


#End Region

#End Region

#Region "データセット設定"

    ''要望番号1282 追加START 2012.07.19
#Region "EDI出荷(大)届先コード設定"

    ''' <summary>
    ''' DIC汎用届先コードの採番
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function GenericDestCheck(ByVal ds As DataSet) As Integer

        Dim dtCd As DataTable = ds.Tables("LMH030_M_CUST_DETAILS")
        Dim dtEdiL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim max As Integer = dtCd.Rows.Count - 1
        Dim genericCd As String = String.Empty
        Dim whereSql As String = String.Empty
        Dim destCd As String = dtEdiL.Rows(0).Item("DEST_CD").ToString()
        Dim ediDestCd As String = dtEdiL.Rows(0).Item("EDI_DEST_CD").ToString()
        Dim comDestCd As String = String.Empty
        Dim genericlgt As Integer = 0
        Dim genericInsFlg As Integer = LMH030BLC.FLG_OFF
        Dim dicGeneralNo As Integer = 0
        Dim num As New NumberMasterUtility

        If String.IsNullOrEmpty(destCd) = True Then
            comDestCd = ediDestCd
        Else
            comDestCd = destCd
        End If

        For i As Integer = 0 To max

            genericCd = dtCd.Rows(i).Item("SET_NAIYO").ToString()
            whereSql = dtCd.Rows(i).Item("SET_NAIYO_2").ToString()
            genericlgt = genericCd.Length

            Select Case whereSql

                Case "0"

                    If genericCd.Equals(comDestCd) = True Then

                        dicGeneralNo = Convert.ToInt32(num.GetAutoCode(NumberMasterUtility.NumberKbn.GENERAL_NO, Me, String.Empty))

                        dtEdiL.Rows(0).Item("DEST_CD") = String.Concat(genericCd, _
                                                                       Me.FormatZero(Convert.ToString(dicGeneralNo), LMH030BLC.DEST_CD_LENGTH - genericlgt))
                        genericInsFlg = LMH030BLC.FLG_ON
                        Return genericInsFlg
                    End If

                Case "1"

                    If InStr(comDestCd, genericCd) > 0 Then

                        dicGeneralNo = Convert.ToInt32(num.GetAutoCode(NumberMasterUtility.NumberKbn.GENERAL_NO, Me, String.Empty))

                        dtEdiL.Rows(0).Item("DEST_CD") = String.Concat(genericCd, _
                                                                       Me.FormatZero(Convert.ToString(dicGeneralNo), LMH030BLC.DEST_CD_LENGTH - genericlgt))
                        genericInsFlg = LMH030BLC.FLG_ON
                        Return genericInsFlg
                    End If

            End Select

        Next

        Return genericInsFlg

    End Function

#End Region
    ''要望番号1282 追加START 2012.07.19

#Region "データセット設定(運送番号)"

    ''' <summary>
    ''' データセット設定(運送番号)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoL(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        Dim max As Integer = dt.Rows.Count - 1

        '運送登録の場合
        Dim num As New NumberMasterUtility
        dr("FREE_C30") = String.Concat("01-", num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, nrsBrCd))
        '2012.03.25 大阪対応END

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
    Private Function GetOutkaNoM(ByVal ds As DataSet) As DataSet

        Dim dtEdiM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim nrsBrCd As String = dtEdiM.Rows(0).ToString
        Dim max As Integer = dtEdiM.Rows.Count - 1

        Dim unsoNoL As String = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("FREE_C30").ToString()
        Dim unsoNoM As String = String.Empty

        '運送登録の場合
        For i As Integer = 0 To max
            unsoNoM = (i + 1).ToString("000")
            dtEdiM.Rows(i)("FREE_C30") = String.Concat(unsoNoL, unsoNoM)
        Next

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
    Private Function SetDatasetOutkaL(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim outkaDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").NewRow()
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

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

            '2012.03.20 修正START
            'DEST_AD_3には、備考項目が入っている為、届先マスタより取得しない
            '↑で合っているかどうかはペンディングとする
            '2012.04.04 再修正START マスタの値をセットする
            '現LMS同様に、マスタのAD_3をセットし、出荷編集画面で出荷予定表と届先が差違がないかを比較する
            'outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")
            outkaDr("DEST_AD_3") = destMDr("AD_3")
            '2012.04.04 再修正END
            '2012.03.20 修正END
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
        outkaDr("DEST_KB") = "02"
        outkaDr("DEST_NM") = ediDr("DEST_NM")
        outkaDr("DEST_AD_1") = ediDr("DEST_AD_1")
        outkaDr("DEST_AD_2") = ediDr("DEST_AD_2")
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

#Region "データセット設定(運送L)"
    ''' <summary>
    ''' データセット設定(運送L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetUnsoL(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim unsoDr As DataRow = ds.Tables("LMH030_UNSO_L").NewRow()
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim outkaLDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        'Dim ediMCntDr As DataRow
        'ディック春日部追加箇所 20120611 terakawa Start
        Dim custIndex As Integer = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CUST_INDEX"))
        'ディック春日部追加箇所 20120611 terakawa End

        Dim num As New NumberMasterUtility

        '通常登録
        unsoDr("NRS_BR_CD") = ediDr("NRS_BR_CD")

        '2012.06.05 ディック共同配送用 START
        'ディック共同配送の場合は運送登録なので、前の処理でFREE_C30で取得した運送番号を使用
        unsoDr("UNSO_NO_L") = ediDr("FREE_C30").ToString().Substring(3, 9)
        '2012.06.05 ディック共同配送用 END

        unsoDr("YUSO_BR_CD") = ediDr("NRS_BR_CD")
        unsoDr("WH_CD") = ediDr("WH_CD")
        '2012.06.05 ディック共同配送用 START
        'ディック共同配送の場合は運送登録なので、出荷管理番号は空
        unsoDr("INOUTKA_NO_L") = String.Empty
        '2012.06.05 ディック共同配送用 END
        unsoDr("TRIP_NO") = String.Empty
        unsoDr("UNSO_CD") = ediDr("UNSO_CD")
        unsoDr("UNSO_BR_CD") = ediDr("UNSO_BR_CD")
        unsoDr("BIN_KB") = ediDr("BIN_KB")
        unsoDr("JIYU_KB") = String.Empty

        '2012.06.05 ディック共同配送用 START
        'ディック共同配送の場合は荷主注文番号をセット
        unsoDr("DENP_NO") = ediDr("CUST_ORD_NO")
        '2012.06.05 ディック共同配送用 END
        unsoDr("OUTKA_PLAN_DATE") = ediDr("OUTKA_PLAN_DATE")
        unsoDr("OUTKA_PLAN_TIME") = String.Empty
        unsoDr("ARR_PLAN_DATE") = ediDr("ARR_PLAN_DATE")
        unsoDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
        unsoDr("ARR_ACT_TIME") = String.Empty
        unsoDr("CUST_CD_L") = ediDr("CUST_CD_L")
        unsoDr("CUST_CD_M") = ediDr("CUST_CD_M")

        unsoDr("CUST_REF_NO") = ediDr("CUST_ORD_NO")
        unsoDr("SHIP_CD") = ediDr("SHIP_CD_L")
        unsoDr("ORIG_CD") = ediDr("FREE_C01")
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

        '2012.06.05 ディック共同配送用 START
        'ディック共同配送の場合は運送登録なので、元データ区分が変わる
        unsoDr("MOTO_DATA_KB") = "40"
        '2012.06.05 ディック共同配送用 END

        unsoDr("TAX_KB") = "01" '課税区分は"01"(課税)固定とする
        '2012.06.05 ディック共同配送用 START
        unsoDr("REMARK") = Me._Blc.LeftB(Trim(String.Concat(ediDr("REMARK").ToString(), Space(2), ediDr("UNSO_ATT").ToString())), 100)
        '2012.06.05 ディック共同配送用 END
        unsoDr("SEIQ_TARIFF_CD") = ediDr("UNCHIN_TARIFF_CD")
        unsoDr("SEIQ_ETARIFF_CD") = ediDr("EXTC_TARIFF_CD")

        '2012.06.05 ディック共同配送用 START
        'DEST_AD_3には、備考項目が入っている為、届先マスタ(出荷(大))より取得しない
        unsoDr("AD_3") = ediDr("DEST_AD_3")
        'unsoDr("AD_3") = outkaLDr("DEST_AD_3")
        '2012.06.05 ディック共同配送用 END

        unsoDr("UNSO_TEHAI_KB") = ediDr("UNSO_MOTO_KB")
        'unsoDr("BUY_CHU_NO") = ediDr("FREE_C02")
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

        '2012.07.02 今まで通り 倉庫Mの届先倉庫コードをSQLで取得するSTART
        ''2012.06.18 追加START(運賃計算対応)
        ''届先マスタに各ディック荷主(大・中)コードで届先コードを登録する事
        'unsoDr("ORIG_CD") = String.Concat(ediDr("CUST_CD_L").ToString().Trim(), ediDr("CUST_CD_M").ToString().Trim())
        ''2012.06.18 追加END
        '2012.07.02 今まで通り 倉庫Mの届先倉庫コードをSQLで取得するEND


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
        'ディック春日部対応 terakawa 2012.06.15 Start
        Dim drL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        'ディック春日部対応 terakawa 2012.06.15 End
        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)

        '2013.10.11 要望番号2113 追加START
        Dim custIndex As Integer = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CUST_INDEX"))
        '2013.10.11 要望番号2113 追加END

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

            '2012.06.05 ディック共同配送用 START
            'ディック共同配送の場合は運送登録なので、出荷管理番号(中)が存在しないので採番する
            '運送登録処理の場合
            unsoMDr("UNSO_NO_M") = (i + 1).ToString("000")
            '2012.06.05 ディック共同配送用 END            
            unsoMDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            unsoMDr("GOODS_NM") = ediDr("GOODS_NM")
            unsoMDr("UNSO_TTL_NB") = ediDr("OUTKA_PKG_NB")
            unsoMDr("NB_UT") = ediDr("KB_UT")
            unsoMDr("UNSO_TTL_QT") = ediDr("OUTKA_TTL_QT")
            unsoMDr("QT_UT") = ediDr("QT_UT")
            unsoMDr("HASU") = ediDr("OUTKA_HASU")
            unsoMDr("ZAI_REC_NO") = String.Empty

            '2012.06.05 ディック共同配送用 START
            'ディック共同配送の場合は運送登録なので、運送温度区分が"90"の場合は入替
            If (ediDr("UNSO_ONDO_KB").ToString()).Equals("90") = True Then
                unsoMDr("UNSO_ONDO_KB") = ediDr("ONDO_KB")
            Else
                unsoMDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
            End If
            '2012.06.05 ディック共同配送用 END

            'unsoMDr("IRIME") = ediDr("IRIME") 　入力ファイルの数量を適用するためコメント
            unsoMDr("IRIME_UT") = ediDr("IRIME_UT")

            stdWtKgs = Convert.ToDecimal(ediDr("STD_WT_KGS"))
            irime = Convert.ToDecimal(ediDr("IRIME"))
            stdIrimeNb = Convert.ToDecimal(ediDr("STD_IRIME_NB"))

            If String.IsNullOrEmpty(ediDr("NISUGATA").ToString()) = False Then
                nisugata = Convert.ToDecimal(ediDr("NISUGATA"))
            End If
            outkaTtlNb = Convert.ToDecimal(ediDr("OUTKA_TTL_NB"))

            'ディック春日部暫定対応 terakawa 2012.06.15 Start
            'If ediDr("TARE_YN").Equals("01") = False Then
            '    unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb)

            'Else
            '    unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb + nisugata)

            'End If

            'ディック春日部の場合、個別重量はEDI出荷(中)の個別重量をセット
            '要望番号:1243(【春日部要望】赤データの表示) 2012/07/05 本明 Start
            'BETU_WTにマイナス値の場合があり得るので絶対値をセット
            'unsoMDr("BETU_WT") = ediDr("BETU_WT")
            unsoMDr("BETU_WT") = System.Math.Abs(Convert.ToDouble(ediDr("BETU_WT").ToString))
            '要望番号:1243(【春日部要望】赤データの表示) 2012/07/05 本明 End

            'ディック春日部暫定対応 terakawa 2012.06.15 End

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
    Private Function SetdatasetUnsoJyuryo(ByVal ds As DataSet) As DataSet

        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim unsoJyuryo As Decimal = 0
        Dim matomeUnsoJyuryo As Decimal = 0

        'まとめ(運送Mデータの運送重量合算)

        unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
        unsoLDr("UNSO_WT") = Math.Ceiling(unsoJyuryo)
        unsoLDr("NB_UT") = ediMDr("KB_UT")
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
            '2012.06.13 修正START ディック春日部の場合　取込時に入数が 0セットされるので、入数は計算式に含まない 
            'NB = Convert.ToDecimal(unsoMDr("UNSO_TTL_NB")) * Convert.ToDecimal(unsoMDr("PKG_NB")) + Convert.ToDecimal(unsoMDr("HASU"))
            'NB = Convert.ToDecimal(unsoMDr("UNSO_TTL_NB")) + Convert.ToDecimal(unsoMDr("HASU")) 　入力ファイルの数量を適用するためコメント
            '2012.06.13 修正END

            'unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) * NB + unsoJyuryo   入力ファイルの数量を適用するためコメント
            unsoJyuryo = Convert.ToDecimal(unsoMDr("UNSO_TTL_QT")) + unsoJyuryo

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

        'ディック春日部対応 terakawa 20120614 Start
        If String.IsNullOrEmpty(ediMDr("PKG_UT").ToString) = False Then
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
        End If
        'ディック春日部対応 terakawa 20120614 End

        Return True

    End Function

#End Region

#End Region

#Region "運送登録処理(運賃作成)"

    ''' <summary>
    ''' 運送登録処理(運賃作成)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnchinSakusei(ByVal ds As DataSet) As DataSet

        '運賃の新規作成
        ds = MyBase.CallDAC(Me._Dac, "InsertUnchinData", ds)

        Return ds

    End Function

#End Region

#End Region

#Region "Method"

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

        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH030_H_OUTKAEDI_DTL_TOR").NewRow()
        Dim drSetDtl As DataRow = ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i)

        'EDI受信DTL設定
        drEdiRcvDtl("DEL_KB") = "0"
        drEdiRcvDtl("CRT_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl("FILE_NAME") = drSetDtl("FILE_NAME_OPE")
        drEdiRcvDtl("REC_NO") = Convert.ToInt32(drSetDtl("REC_NO")).ToString("00000")
        drEdiRcvDtl("GYO") = String.Empty
        drEdiRcvDtl("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")                                           '後でセット 
        drEdiRcvDtl("EDI_CTL_NO") = String.Empty                                                        '後でセット                
        drEdiRcvDtl("EDI_CTL_NO_CHU") = String.Empty                                                    '後でセット
        drEdiRcvDtl("OUTKA_CTL_NO") = DEF_CTL_NO
        drEdiRcvDtl("OUTKA_CTL_NO_CHU") = "000"
        drEdiRcvDtl("CUST_CD_L") = drSemiEdiInfo("CUST_CD_L")                                           '荷主コード（大）
        drEdiRcvDtl("CUST_CD_M") = drSemiEdiInfo("CUST_CD_M")                                           '荷主コード（中）
        drEdiRcvDtl("PRTFLG") = "0"                                                                     'プリントフラグ

        drEdiRcvDtl("OUTKA_PLAN_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_2").ToString().Trim(), 8)       '出荷日
        drEdiRcvDtl("ARR_PLAN_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_3").ToString().Trim(), 8)         '着荷日
        drEdiRcvDtl("OUTKA_FROM_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_10").ToString().Trim(), 100)      '出荷元
        drEdiRcvDtl("OUTKA_FROM_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_11").ToString().Trim(), 100)      '出荷元翻訳
        drEdiRcvDtl("DEST_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_12").ToString().Trim(), 15)             '出荷先
        drEdiRcvDtl("DEST_NM1") = Me._Blc.LeftB(drSetDtl("COLUMN_13").ToString().Trim(), 80)            '出荷先翻訳
        drEdiRcvDtl("DEST_ZIP") = Me._Blc.LeftB(drSetDtl("COLUMN_16").ToString().Trim(), 10)            '出荷先郵便番号
        drEdiRcvDtl("DEST_AD_1") = Me._Blc.LeftB(drSetDtl("COLUMN_17").ToString().Trim(), 40)           '出荷先住所
        drEdiRcvDtl("DEST_TEL") = Me._Blc.LeftB(drSetDtl("COLUMN_18").ToString().Trim(), 20)            '出荷先電話番号
        drEdiRcvDtl("CUST_GOODS_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_30").ToString().Trim(), 20)       '管理品名（商品コード）
        drEdiRcvDtl("GOODS_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_31").ToString().Trim(), 60)            '管理品名翻訳（商品名）
        drEdiRcvDtl("GOODS_KIND") = Me._Blc.LeftB(drSetDtl("COLUMN_34").ToString().Trim(), 100)         '品種1+2
        drEdiRcvDtl("OUTKA_QT") = Me._Blc.LeftB(drSetDtl("COLUMN_35").ToString().Trim(), 12)            '指図数量
        drEdiRcvDtl("IRIME_UT") = Me._Blc.LeftB(drSetDtl("COLUMN_37").ToString().Trim(), 2)             '単位翻訳
        drEdiRcvDtl("OUTKA_PKG_NB") = Me._Blc.LeftB(drSetDtl("COLUMN_38").ToString().Trim(), 10)        '個数
        drEdiRcvDtl("BUYER_ORD_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_40").ToString().Trim(), 30)        '契約No
        drEdiRcvDtl("ORDER_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_41").ToString().Trim(), 30)            '指図No
        drEdiRcvDtl("UNSO_ATT") = Me._Blc.LeftB(drSetDtl("COLUMN_52").ToString().Trim(), 100)           '連絡備考1翻訳
        drEdiRcvDtl("REMARK") = Me._Blc.LeftB(drSetDtl("COLUMN_57").ToString().Trim(), 100)             '連絡任意
        drEdiRcvDtl("SASIZU_JYOTAI") = Me._Blc.LeftB(drSetDtl("COLUMN_66").ToString().Trim(), 2)        '指図状態     'ADD 2018/11/13 依頼番号 : 002600   【LMS】東レ_運送セミEDI_個数マイナスデータ対応

        'データセットに設定
        ds.Tables("LMH030_H_OUTKAEDI_DTL_TOR").Rows.Add(drEdiRcvDtl)

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
    Private Function SetSemiOutkaEdiM(ByVal setDs As DataSet, Optional OutkaM_OutCnt As Integer = 0) As DataSet

        Dim drOutkaEdiM As DataRow = setDs.Tables("LMH030_OUTKAEDI_M").NewRow()
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_H_OUTKAEDI_DTL_TOR").Rows(0)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        Dim mGoodsDt As DataTable = setDs.Tables("LMH030_M_GOODS")

        drOutkaEdiM("DEL_KB") = "0"
        drOutkaEdiM("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drOutkaEdiM("EDI_CTL_NO") = drRcvEdiDtl("EDI_CTL_NO")
        'drOutkaEdiM("EDI_CTL_NO_CHU") = drRcvEdiDtl("EDI_CTL_NO_CHU")
        'drOutkaEdiM("EDI_CTL_NO_CHU") = FormatZero(OutkaM_OutCnt.ToString, 3)
        drOutkaEdiM("EDI_CTL_NO_CHU") = OutkaM_OutCnt.ToString("000")


        drOutkaEdiM("OUTKA_CTL_NO") = String.Empty
        drOutkaEdiM("OUTKA_CTL_NO_CHU") = String.Empty
        drOutkaEdiM("COA_YN") = String.Empty
        drOutkaEdiM("CUST_ORD_NO_DTL") = drRcvEdiDtl("ORDER_NO")
        drOutkaEdiM("BUYER_ORD_NO_DTL") = drRcvEdiDtl("BUYER_ORD_NO")
        drOutkaEdiM("CUST_GOODS_CD") = drRcvEdiDtl("CUST_GOODS_CD")
        drOutkaEdiM("NRS_GOODS_CD") = String.Empty
        drOutkaEdiM("GOODS_NM") = drRcvEdiDtl("GOODS_NM")

        drOutkaEdiM("RSV_NO") = String.Empty
        drOutkaEdiM("LOT_NO") = String.Empty
        drOutkaEdiM("SERIAL_NO") = String.Empty
        drOutkaEdiM("ALCTD_KB") = String.Empty

        drOutkaEdiM("OUTKA_PKG_NB") = drRcvEdiDtl("OUTKA_PKG_NB")     '出荷包装個数
        drOutkaEdiM("OUTKA_HASU") = 0
        drOutkaEdiM("OUTKA_QT") = drRcvEdiDtl("OUTKA_QT")             '出荷数量
        drOutkaEdiM("OUTKA_TTL_NB") = drRcvEdiDtl("OUTKA_PKG_NB")     '出荷総個数
        drOutkaEdiM("OUTKA_TTL_QT") = drRcvEdiDtl("OUTKA_QT")         '出荷総数量

        drOutkaEdiM("KB_UT") = String.Empty
        drOutkaEdiM("QT_UT") = String.Empty
        drOutkaEdiM("PKG_NB") = 0
        drOutkaEdiM("PKG_UT") = String.Empty
        drOutkaEdiM("ONDO_KB") = String.Empty
        drOutkaEdiM("UNSO_ONDO_KB") = String.Empty

        drOutkaEdiM("IRIME") = 0
        drOutkaEdiM("IRIME_UT") = drRcvEdiDtl("IRIME_UT")             '入目単位
        drOutkaEdiM("BETU_WT") = String.Empty
        drOutkaEdiM("REMARK") = String.Empty
        drOutkaEdiM("OUT_KB") = "0"
        drOutkaEdiM("AKAKURO_KB") = "0"
        drOutkaEdiM("JISSEKI_FLAG") = "0"
        drOutkaEdiM("JISSEKI_USER") = String.Empty
        drOutkaEdiM("JISSEKI_DATE") = String.Empty
        drOutkaEdiM("JISSEKI_TIME") = String.Empty
        drOutkaEdiM("SET_KB") = String.Empty
        drOutkaEdiM("FREE_N01") = 0
        drOutkaEdiM("FREE_N02") = 0
        drOutkaEdiM("FREE_N03") = 0
        drOutkaEdiM("FREE_N04") = 0
        drOutkaEdiM("FREE_N05") = 0
        drOutkaEdiM("FREE_N06") = 0
        drOutkaEdiM("FREE_N07") = 0
        drOutkaEdiM("FREE_N08") = 0
        drOutkaEdiM("FREE_N09") = 0
        drOutkaEdiM("FREE_N10") = 0
        drOutkaEdiM("FREE_C01") = drRcvEdiDtl("GOODS_KIND")           '品種1+2
        drOutkaEdiM("FREE_C02") = drRcvEdiDtl("SASIZU_JYOTAI")          '指図状態     'ADD 2018/11/13 依頼番号 : 002600   【LMS】東レ_運送セミEDI_個数マイナスデータ対応
        drOutkaEdiM("FREE_C03") = String.Empty
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
        drOutkaEdiM("FREE_C24") = String.Empty
        drOutkaEdiM("FREE_C25") = String.Empty
        drOutkaEdiM("FREE_C26") = String.Empty
        drOutkaEdiM("FREE_C27") = String.Empty
        drOutkaEdiM("FREE_C28") = String.Empty
        drOutkaEdiM("FREE_C29") = String.Empty
        drOutkaEdiM("FREE_C30") = DEF_UNSO_NO_M

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
                                    , ByVal sNrsGoodsCd As String _
                                    , ByVal sNrsGoodsNm As String _
                                    , ByVal sIrime As String _
                                    ) As DataSet

        Dim drOutkaEdiL As DataRow = setDs.Tables("LMH030_OUTKAEDI_L").NewRow()
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_H_OUTKAEDI_DTL_TOR").Rows(0)
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
        drOutkaEdiL("OUTKAHOKOKU_YN") = "0"
        drOutkaEdiL("PICK_KB") = "01"
        drOutkaEdiL("NRS_BR_NM") = String.Empty
        drOutkaEdiL("WH_CD") = drSemiEdiInfo("WH_CD")
        drOutkaEdiL("WH_NM") = String.Empty

        '出荷予定日
        If String.IsNullOrEmpty(drRcvEdiDtl("OUTKA_PLAN_DATE").ToString) Then
            drOutkaEdiL("OUTKA_PLAN_DATE") = MyBase.GetSystemDate() 'サーバー日付
        Else
            drOutkaEdiL("OUTKA_PLAN_DATE") = drRcvEdiDtl("OUTKA_PLAN_DATE")
        End If

        '出庫日(出荷予定日と同日)
        drOutkaEdiL("OUTKO_DATE") = drOutkaEdiL("OUTKA_PLAN_DATE")

        '納入予定日
        If String.IsNullOrEmpty(drRcvEdiDtl("ARR_PLAN_DATE").ToString) Then
            drOutkaEdiL("ARR_PLAN_DATE") = MyBase.GetSystemDate() 'サーバー日付
        Else
            drOutkaEdiL("ARR_PLAN_DATE") = drRcvEdiDtl("ARR_PLAN_DATE")
        End If

        drOutkaEdiL("ARR_PLAN_TIME") = String.Empty
        drOutkaEdiL("HOKOKU_DATE") = String.Empty
        drOutkaEdiL("TOUKI_HOKAN_YN") = String.Empty
        drOutkaEdiL("CUST_CD_L") = drRcvEdiDtl("CUST_CD_L")
        drOutkaEdiL("CUST_CD_M") = drRcvEdiDtl("CUST_CD_M")
        drOutkaEdiL("CUST_NM_L") = String.Empty
        drOutkaEdiL("CUST_NM_M") = String.Empty
        drOutkaEdiL("SHIP_CD_L") = String.Empty
        drOutkaEdiL("SHIP_CD_M") = String.Empty
        drOutkaEdiL("SHIP_NM_L") = String.Empty
        drOutkaEdiL("SHIP_NM_M") = String.Empty
        drOutkaEdiL("EDI_DEST_CD") = drRcvEdiDtl("DEST_CD")
        drOutkaEdiL("DEST_CD") = drOutkaEdiL("EDI_DEST_CD")
        drOutkaEdiL("DEST_NM") = drRcvEdiDtl("DEST_NM1")
        drOutkaEdiL("DEST_ZIP") = drRcvEdiDtl("DEST_ZIP").ToString.Replace("-", "")
        drOutkaEdiL("DEST_AD_1") = drRcvEdiDtl("DEST_AD_1")
        drOutkaEdiL("DEST_AD_2") = String.Empty
        drOutkaEdiL("DEST_AD_3") = String.Empty
        drOutkaEdiL("DEST_AD_4") = String.Empty
        drOutkaEdiL("DEST_AD_5") = String.Empty
        drOutkaEdiL("DEST_TEL") = drRcvEdiDtl("DEST_TEL")
        drOutkaEdiL("DEST_FAX") = String.Empty
        drOutkaEdiL("DEST_MAIL") = String.Empty
        drOutkaEdiL("DEST_JIS_CD") = String.Empty
        drOutkaEdiL("SP_NHS_KB") = String.Empty
        drOutkaEdiL("COA_YN") = String.Empty
        drOutkaEdiL("CUST_ORD_NO") = drRcvEdiDtl("ORDER_NO")
        drOutkaEdiL("BUYER_ORD_NO") = drRcvEdiDtl("BUYER_ORD_NO")

        drOutkaEdiL("UNSO_MOTO_KB") = String.Empty
        drOutkaEdiL("UNSO_TEHAI_KB") = String.Empty
        drOutkaEdiL("SYARYO_KB") = String.Empty
        drOutkaEdiL("BIN_KB") = String.Empty
        drOutkaEdiL("UNSO_CD") = String.Empty
        drOutkaEdiL("UNSO_NM") = String.Empty
        drOutkaEdiL("UNSO_BR_CD") = String.Empty
        drOutkaEdiL("UNSO_BR_NM") = String.Empty
        drOutkaEdiL("UNCHIN_TARIFF_CD") = String.Empty
        drOutkaEdiL("EXTC_TARIFF_CD") = String.Empty

        drOutkaEdiL("REMARK") = drRcvEdiDtl("REMARK")
        drOutkaEdiL("UNSO_ATT") = drRcvEdiDtl("UNSO_ATT")
        drOutkaEdiL("DENP_YN") = "1"
        drOutkaEdiL("PC_KB") = String.Empty
        drOutkaEdiL("UNCHIN_YN") = "1"
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

        drOutkaEdiL("FREE_C01") = drRcvEdiDtl("OUTKA_FROM_CD")
        drOutkaEdiL("FREE_C02") = drRcvEdiDtl("OUTKA_FROM_NM")
        drOutkaEdiL("FREE_C03") = String.Empty
        drOutkaEdiL("FREE_C04") = String.Empty
        drOutkaEdiL("FREE_C05") = String.Empty
        drOutkaEdiL("FREE_C06") = String.Empty
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
        drOutkaEdiL("FREE_C20") = String.Empty
        drOutkaEdiL("FREE_C21") = String.Empty
        drOutkaEdiL("FREE_C22") = String.Empty
        drOutkaEdiL("FREE_C23") = String.Empty
        drOutkaEdiL("FREE_C24") = String.Empty
        drOutkaEdiL("FREE_C25") = String.Empty
        drOutkaEdiL("FREE_C26") = String.Empty
        drOutkaEdiL("FREE_C27") = String.Empty
        drOutkaEdiL("FREE_C28") = String.Empty
        drOutkaEdiL("FREE_C29") = String.Empty
        drOutkaEdiL("FREE_C30") = DEF_UNSO_NO_L

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_L").Rows.Add(drOutkaEdiL)
        Return setDs


    End Function

#End Region

#End Region

#Region "セミEDI データセット設定(EDI管理番号(大・中))"

    ''' <summary>
    ''' データセット設定(EDI管理番号(大・中)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdiCtlNo(ByVal ds As DataSet _
                               , ByVal iDeleteFlg As Integer, ByVal iCancelFlg As Integer, ByVal iSkipFlg As Integer, ByVal bSameKeyFlg As Boolean _
                                , ByRef sEdiCtlNo As String, ByRef iEdiCtlNoChu As Integer) As DataSet

        Dim dtRcvEdiDtl As DataTable = ds.Tables("LMH030_H_OUTKAEDI_DTL_TOR")
        Dim drRcvEdiDtl As DataRow = ds.Tables("LMH030_H_OUTKAEDI_DTL_TOR").Rows(0)
        Dim sNrsBrCd As String = ds.Tables("LMH030_H_OUTKAEDI_DTL_TOR").Rows(0).Item("NRS_BR_CD").ToString()


        '前行とキーが異なる場合　
        If bSameKeyFlg = False Then
            iEdiCtlNoChu = 0    '０クリア    
        End If

        'EDI管理番号(中)をカウントアップ
        iEdiCtlNoChu = iEdiCtlNoChu + 1

        If iCancelFlg = 0 AndAlso iSkipFlg = 0 Then
            'キャンセルフラグが０ かつ スキップフラグが０の場合　
            If bSameKeyFlg = False Then
                '前行とキーが異なる場合　
                'EDI管理番号(大)を新規採番してEDI管理番号(中)を"001"採番
                Dim num As New NumberMasterUtility
                sEdiCtlNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_OUTKA_NO_L, Me, sNrsBrCd)
            End If

            '登録用EDI管理番号
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo              'DTLにセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO_CHU") = iEdiCtlNoChu.ToString("000")   'EDI_CHUにセット
            dtRcvEdiDtl.Rows(0).Item("GYO") = iEdiCtlNoChu.ToString("00")               '行数にもEDI_CHUと同じ値をセット
        Else
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO") = DEF_CTL_NO             'DTLに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO_CHU") = "000"              'EDI_CHUに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("GYO") = iEdiCtlNoChu.ToString("00")   '行数にはカウントアップした値を入れる
        End If

        Return ds

    End Function

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

                        '先頭の"="を取る
                        Me.EqualCutEdit(dr)

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
        Dim sDate As String = String.Empty
        Dim targetStr As String = String.Empty
        Dim dNum As Double = 0
        Dim bRet As Boolean = True

        '出荷日
        sDate = dr.Item("COLUMN_2").ToString().Trim()
        '年月日チェック
        If IsConvertDate(sDate) Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("出荷日(カラム2番目)[", sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '着荷日
        sDate = dr.Item("COLUMN_3").ToString().Trim()
        '年月日チェック
        If IsConvertDate(sDate) Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("着荷日(カラム3番目)[", sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '出荷元
        targetStr = dr.Item("COLUMN_10").ToString.Trim()
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("出荷元(カラム10番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 100, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("出荷元(カラム10番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '出荷元翻訳
        targetStr = dr.Item("COLUMN_11").ToString.Trim()
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("出荷元翻訳(カラム11番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '文字列長チェック
        ElseIf targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("出荷元翻訳(カラム11番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '出荷先
        targetStr = dr.Item("COLUMN_12").ToString.Trim()
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("出荷先(カラム12番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 15, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("出荷先(カラム12番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '出荷先翻訳
        targetStr = dr.Item("COLUMN_13").ToString.Trim()
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("出荷先翻訳(カラム13番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '文字列長チェック
        ElseIf targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("出荷先翻訳(カラム13番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '出荷先郵便番号
        targetStr = dr.Item("COLUMN_16").ToString.Trim()
        '文字列長チェック
        If IsHalfSize(targetStr, 10, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("出荷先郵便番号(カラム16番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '出荷先住所
        targetStr = dr.Item("COLUMN_17").ToString.Trim()
        '文字列長チェック
        If targetStr.Length > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("出荷先住所(カラム17番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '出荷先電話番号
        targetStr = dr.Item("COLUMN_18").ToString.Trim()
        '文字列長チェック
        If IsHalfSize(targetStr, 20, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("出荷先電話番号(カラム18番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '管理品名（商品コード）
        targetStr = dr.Item("COLUMN_30").ToString.Trim()
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("管理品名(カラム30番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 20, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("管理品名(カラム30番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '管理品名翻訳（商品名）
        targetStr = dr.Item("COLUMN_31").ToString.Trim()
        '文字列長チェック
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("管理品名翻訳(カラム31番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '品種1+2
        targetStr = dr.Item("COLUMN_34").ToString.Trim()
        '文字列長チェック
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("品種1+2(カラム34番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '指図数量
        targetStr = dr.Item("COLUMN_35").ToString().Trim()
        If targetStr.Length > 0 Then
            If IsConvertDbl(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat("指図数量(カラム35番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            Else
                dNum = Convert.ToDouble(targetStr)
                dNum = System.Math.Abs(dNum)
                If dNum > 999999999.999 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("指図数量(カラム35番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If
        End If

        '単位翻訳
        targetStr = dr.Item("COLUMN_37").ToString.Trim()
        '文字列長チェック
        If targetStr.Length > 2 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("単位翻訳(カラム37番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '個数
        targetStr = dr.Item("COLUMN_38").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 10 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("個数(カラム38番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertDbl(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat("個数(カラム38番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '契約No
        targetStr = dr.Item("COLUMN_40").ToString.Trim()
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("契約No(カラム40番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 30, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("契約No(カラム40番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '指図No
        targetStr = dr.Item("COLUMN_41").ToString.Trim()
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("指図No(カラム41番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 30, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("指図No(カラム41番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '連絡備考1翻訳
        targetStr = dr.Item("COLUMN_52").ToString.Trim()
        '文字列長チェック
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("連絡備考1翻訳(カラム52番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '連絡任意
        targetStr = dr.Item("COLUMN_57").ToString.Trim()
        '文字列長チェック
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("連絡任意(カラム57番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '戻り値設定
        Return bRet

    End Function

#End Region

    ''' <summary>
    ''' 年月日チェック
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>True=年月日として扱える False=年月日として扱えない</returns>
    ''' <remarks>引数にyyyyMMdd形式の文字列を設定し、それが年月日として扱えるかの判別を行う</remarks>
    Private Function IsConvertDate(ByVal targetString As String) As Boolean
        Dim d As DateTime
        Return DateTime.TryParseExact(targetString, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None, d)
    End Function

    ''' <summary>
    ''' 月日チェック
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>True=月日として扱える False=月日として扱えない</returns>
    ''' <remarks>引数にMMdd形式の文字列を設定し、それが月日として扱えるかの判別を行う</remarks>
    Private Function IsConvertDate2(ByVal targetString As String) As Boolean
        Dim d As DateTime
        Return DateTime.TryParseExact(targetString, "MMdd", System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None, d)
    End Function


    ''' <summary>
    ''' 文字列長（バイト）を求める
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>対象文字列のバイト数</returns>
    ''' <remarks></remarks>
    Private Function LenB(ByVal targetString As String) As Integer
        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(targetString)
    End Function

    ''' <summary>
    ''' 文字列が全て半角であるかをチェックする
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>True = 全て半角 False = 全角混在</returns>
    ''' <remarks></remarks>
    Private Overloads Function IsHalfSize(ByVal targetString As String) As Boolean
        Static Encode_JIS As Text.Encoding = Text.Encoding.GetEncoding("Shift_JIS")
        Dim Str_Count As Integer = targetString.Length
        Dim ByteCount As Integer = Encode_JIS.GetByteCount(targetString)
        Return Str_Count.Equals(ByteCount)
    End Function


    ''' <summary>
    ''' 文字列が全て半角であるかをチェックし、文字列長をチェックする
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <param name="length">比較文字列長</param>
    ''' <returns>True=条件を満たしている False=条件を満たしていない</returns>
    ''' <remarks>文字列長はイコール比較を行う</remarks>
    Private Overloads Function IsHalfSize(ByVal targetString As String, ByVal length As Integer) As Boolean
        IsHalfSize(targetString, length, True)
    End Function

    ''' <summary>
    ''' 文字列が全て半角であるかをチェックし、文字列長をチェックする
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <param name="length">比較文字列長</param>
    ''' <param name="EqualOrMax">True=イコール比較 False=最大値比較 </param>
    ''' <returns>True=条件を満たしている False=条件を満たしていない</returns>
    ''' <remarks></remarks>
    Private Overloads Function IsHalfSize(ByVal targetString As String, ByVal length As Integer, ByVal EqualOrMax As Boolean) As Boolean
        If IsHalfSize(targetString).Equals(False) Then
            Return False
        ElseIf EqualOrMax.Equals(True) Then
            If targetString.Length.Equals(length).Equals(False) Then
                Return False
            End If
        Else
            If targetString.Length > length Then
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' 文字列が数値（Double型）に変換出来るかチェックする
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>True=変換できる　False=変換できない</returns>
    ''' <remarks></remarks>
    Private Function IsConvertDbl(ByVal targetString As String) As Boolean
        Dim d As Double
        Return Double.TryParse(targetString, d)
    End Function

#Region "カラム項目の値の先頭=を取る"

    ''' <summary>
    ''' データ編集
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Public Sub EqualCutEdit(ByVal dr As DataRow)

        '出荷日
        dr.Item("COLUMN_2") = EqualCut(dr.Item("COLUMN_2").ToString().Trim())

        '着荷日
        dr.Item("COLUMN_3") = EqualCut(dr.Item("COLUMN_3").ToString().Trim())

        '出荷元
        dr.Item("COLUMN_10") = EqualCut(dr.Item("COLUMN_10").ToString().Trim())

        '出荷元翻訳
        dr.Item("COLUMN_11") = EqualCut(dr.Item("COLUMN_11").ToString().Trim())

        '出荷先
        dr.Item("COLUMN_12") = EqualCut(dr.Item("COLUMN_12").ToString().Trim())

        '出荷先翻訳
        dr.Item("COLUMN_13") = EqualCut(dr.Item("COLUMN_13").ToString().Trim())

        '出荷先郵便番号
        dr.Item("COLUMN_16") = EqualCut(dr.Item("COLUMN_16").ToString().Trim())

        '出荷先住所
        dr.Item("COLUMN_17") = EqualCut(dr.Item("COLUMN_17").ToString().Trim())

        '出荷先電話番号
        dr.Item("COLUMN_18") = EqualCut(dr.Item("COLUMN_18").ToString().Trim())

        '管理品名（商品コード）
        dr.Item("COLUMN_30") = EqualCut(dr.Item("COLUMN_30").ToString().Trim())

        '管理品名翻訳（商品名）
        dr.Item("COLUMN_31") = EqualCut(dr.Item("COLUMN_31").ToString().Trim())

        '品種1+2
        dr.Item("COLUMN_34") = EqualCut(dr.Item("COLUMN_34").ToString().Trim())

        '指図数量
        dr.Item("COLUMN_35") = EqualCut(dr.Item("COLUMN_35").ToString().Trim())

        '単位翻訳
        dr.Item("COLUMN_37") = EqualCut(dr.Item("COLUMN_37").ToString().Trim())

        '個数
        dr.Item("COLUMN_38") = EqualCut(dr.Item("COLUMN_38").ToString().Trim())

        '契約No
        dr.Item("COLUMN_40") = EqualCut(dr.Item("COLUMN_40").ToString().Trim())

        '指図No
        dr.Item("COLUMN_41") = EqualCut(dr.Item("COLUMN_41").ToString().Trim())

        '連絡備考1翻訳
        dr.Item("COLUMN_52") = EqualCut(dr.Item("COLUMN_52").ToString().Trim())

        '連絡任意
        dr.Item("COLUMN_57") = EqualCut(dr.Item("COLUMN_57").ToString().Trim())

    End Sub

#End Region

#Region "先頭=（equal）を取る(EqualCut)"
    ''' <summary>先頭" ="を取って返す。</summary>
    ''' <param name="str">対象の文字列</param>
    ''' <returns>切捨てられた文字列</returns>
    ''' <remarks>最初の１バイトが"="以外はそのまま返す</remarks>
    Public Function EqualCut(ByVal str As String) As String

        If Mid(str, 1, 1) <> "=" Then
            Return str
        End If

        str = Mid(str, 2)

        'ダブルクォーテーションを除外した文字列をセットする
        If Left(str, 1).Equals(Chr(34)) And Right(str, 1).Equals(Chr(34)) Then
            str = Trim(Mid(str, 2, Len(str) - 2))
        End If

        Return str

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

        Dim dtSemiInfo As DataTable = ds.Tables("LMH030_SEMIEDI_INFO")          'セミEDI情報(TBL)
        Dim drSemiInfo As DataRow = dtSemiInfo.Rows(0)                          'セミEDI情報(ROW)
        Dim dtSetHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")        '取込Hed
        Dim dtSetDtl As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")        '取込Dtl
        Dim dtSetRet As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_RET")        '処理件数

        Dim dtRcvDtl As DataTable = ds.Tables("LMH030_H_OUTKAEDI_DTL_TOR")         'EDI受信Dtl

        Dim iCancelCnt As Integer = 0
        Dim iGoodsCnt As Integer = 0

        Dim iSetDtlMax As Integer = dtSetDtl.Rows.Count - 1

        Dim sWhcd As String = String.Empty          '倉庫コード     
        Dim sCustCdL As String = String.Empty       '荷主コード大   （※商品コードから取得する）
        Dim sCustCdM As String = String.Empty       '荷主コード中   （※商品コードから取得する）
        Dim sNrsGoodsCd As String = String.Empty    '日陸商品コード （※商品コードから取得する）
        Dim sNrsGoodsNm As String = String.Empty    '日陸商品名     （※商品コードから取得する）
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

#If True Then       'ADD 2018/11/14 依頼番号 : 002600   【LMS】東レ_運送セミEDI_個数マイナスデータ対応
        Dim sOldOrderNoKey As String = String.Empty        'キー項目（前行）
        Dim sNewOrderNoKey As String = String.Empty        'キー項目（現在行）
        Dim bSameKeOrderNoyFlg As Boolean = False          '前行とキーが同じ場合True、異なる場合False

#End If

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

        '---------------------------------------------------------------------------
        ' 倉庫/仕入先コードから荷主コード、商品コードを設定
        '---------------------------------------------------------------------------
        _WhCd = drSemiInfo.Item("WH_CD").ToString
        _CustCdL = drSemiInfo.Item("CUST_CD_L").ToString
        _CustCdM = drSemiInfo.Item("CUST_CD_M").ToString
        sNrsGoodsCd = NRS_GOODS_CD_UNSO       '未使用
        sNrsGoodsNm = String.Empty            '未使用
        sIrime = IRIME_UNSO                   '未使用

        Dim sOutkaM_OutCnt As Integer = 0

        For i As Integer = 0 To iSetDtlMax

            '---------------------------------------------------------------------------
            ' セミEDI取込(共通)⇒受信HED/DTLへのデータセット
            '---------------------------------------------------------------------------
            ds.Tables("LMH030_H_OUTKAEDI_DTL_TOR").Clear() '受信DTLをクリア
            ds = Me.SetSemiOutkaEdiRcv(ds, i)

            Dim drEdiRcvDtl As DataRow = ds.Tables("LMH030_H_OUTKAEDI_DTL_TOR").Rows(0)

            '---------------------------------------------------------------------------
            ' キー項目設定
            '---------------------------------------------------------------------------
            sNewKey = String.Concat(drEdiRcvDtl.Item("OUTKA_PLAN_DATE").ToString, "-", _
                                    drEdiRcvDtl.Item("ARR_PLAN_DATE").ToString, "-", _
                                    drEdiRcvDtl.Item("OUTKA_FROM_CD").ToString, "-", _
                                    drEdiRcvDtl.Item("DEST_CD").ToString)

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

                    sOutkaM_OutCnt = 0      'ADD 2018/11/14 依頼番号 : 002600
                End If
            End If

#If True Then   'ADD 2018/11/14 依頼番号 : 002600   【LMS】東レ_運送セミEDI_個数マイナスデータ対応
            '明細は次明細と比較で処理する

            sNewOrderNoKey = String.Concat(drEdiRcvDtl.Item("OUTKA_PLAN_DATE").ToString, "-", _
                        drEdiRcvDtl.Item("ARR_PLAN_DATE").ToString, "-", _
                        drEdiRcvDtl.Item("OUTKA_FROM_CD").ToString, "-", _
                        drEdiRcvDtl.Item("DEST_CD").ToString, "-", _
                        drEdiRcvDtl.Item("ORDER_NO").ToString)

            If i < iSetDtlMax Then

                sOldOrderNoKey = String.Concat(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i + 1).Item("COLUMN_2").ToString, "-", _
                ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i + 1).Item("COLUMN_3").ToString, "-", _
                ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i + 1).Item("COLUMN_10").ToString, "-", _
                ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i + 1).Item("COLUMN_12").ToString, "-", _
                ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i + 1).Item("COLUMN_41").ToString)

            Else
                sOldOrderNoKey = String.Empty

            End If

            'キーを比較
            If sNewOrderNoKey.Equals(sOldOrderNoKey) = True Then
                'キーが同一の場合
                bSameKeOrderNoyFlg = True
            Else
                'キーが異なる場合
                bSameKeOrderNoyFlg = False

            End If

#End If

            '---------------------------------------------------------------------------
            ' EDI管理番号(大,中)の採番(キャンセルフラグが０の場合も関数内で判断
            '---------------------------------------------------------------------------
            ds = Me.GetEdiCtlNo(ds, iDeleteFlg, iCancelFlg, iSkipFlg, bSameKeyFlg, sEdiCtlNo, iEdiCtlNoChu)

            '別インスタンス
            Dim setDs As DataSet = ds.Copy()

            Dim setDtlDt As DataTable = setDs.Tables("LMH030_H_OUTKAEDI_DTL_TOR")

            setDtlDt.Clear()

            setDtlDt.ImportRow(dtRcvDtl.Rows(0))

            '---------------------------------------------------------------------------
            ' EDI受信データの新規追加
            '---------------------------------------------------------------------------
            ' EDI受信データ(DTL)の新規追加

            'iSkipFlgを削除区分の値として使用する
            If iSkipFlg = 0 Then
                setDtlDt.Rows(0).Item("DEL_KB") = "0"
            Else
                setDtlDt.Rows(0).Item("DEL_KB") = "1"
            End If

            '---------------------------------------------------------------------------
            ' キャンセルフラグが0かつスキップフラグが0の場合、EDI出荷データの追加処理を行う
            '---------------------------------------------------------------------------
            If iCancelFlg = 0 AndAlso iSkipFlg = 0 Then

#If True Then   'ADD 2018/11/14 依頼番号 : 002600   【LMS】東レ_運送セミEDI_個数マイナスデータ対応
                If bSameKeOrderNoyFlg = False _
                    Or (i).Equals(iSetDtlMax) = True Then

                    Dim sasizuFLG As String = setDs.Tables("LMH030_H_OUTKAEDI_DTL_TOR").Rows(0).Item("SASIZU_JYOTAI").ToString()
                    'Dim sasizuFLG As String = setDs.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("FREE_C02").ToString

                    Select Case sasizuFLG
                        Case sizuJyotai_A, sizuJyotai_S

                            sOutkaM_OutCnt = sOutkaM_OutCnt + 1

                            '受信DTL⇒EDI出荷(中)へのデータセット(中番にため再設定)
                            setDs = Me.SetSemiOutkaEdiM(setDs, sOutkaM_OutCnt)

                            'EDI出荷(中)の新規追加
                            setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiM", setDs)
                            iOutDtlInsCnt = iOutDtlInsCnt + 1

                        Case Else
                            Continue For          'ADD 2019/01/30 依頼番号 : 004466   【LMS】東レ運送セミEDI_登録時システムエラー

                    End Select

                    'ADD 2019/01/30 依頼番号 : 004466   【LMS】東レ運送セミEDI_登録時システムエラー

                Else                    'ADD 2019/01/30 依頼番号 : 004466   【LMS】東レ運送セミEDI_登録時システムエラー
                    Continue For        'ADD 2019/01/30 依頼番号 : 004466   【LMS】東レ運送セミEDI_登録時システムエラー

                End If

#Else
                '受信DTL⇒EDI出荷(中)へのデータセット
                setDs = Me.SetSemiOutkaEdiM(setDs)

                'EDI出荷(中)の新規追加
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiM", setDs)
                iOutDtlInsCnt = iOutDtlInsCnt + 1

#End If

                '前行と差異がある場合は、EDI出荷(大)を新規追加
#If False Then  'UPD 2018/11/14 依頼番号 : 002600   【LMS】東レ_運送セミEDI_個数マイナスデータ対応
                If bSameKeyFlg = False Then
#Else
                If sOutkaM_OutCnt = 1 Then
#End If

                    '受信DTL⇒EDI出荷(大)へのデータセット
                    setDs = Me.SetSemiOutkaEdiL(setDs, _WhCd, _CustCdL, _CustCdM, sNrsGoodsCd, sNrsGoodsNm, sIrime)
#If False Then  'DEL 2018/09/12　頼番号 : 002436   【LMS】千葉東レ_セミEDI運送バグ
                    'EDI出荷（大）に、すでに同じ受注番号の情報が登録されていないか確認する
                    setDs = MyBase.CallDAC(Me._Dac, "SelectOutkaEdiLCount", setDs)
                    If setDs.Tables("DATA_COUNT").Rows(0).Item("DATA_COUNT").ToString <> "0" Then
                        'メッセージ出力　既に同じ受注番号が登録されている
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E269", New String() {"受注番号:" & setDs.Tables("LMH030_H_OUTKAEDI_DTL_TOR").Rows(0).Item("ORDER_NO").ToString}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, "")
                        'エラーフラグ設定してからループを出る
                        bNoErr = False
                        Exit For
                    End If
#End If

                    'EDI出荷(大)の新規追加
                    setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiL", setDs)

                    iOutHedInsCnt = iOutHedInsCnt + 1

                End If

                'キーを入れ替えるのはiSkipFlgの値で判断する
                '※iSkipFlg = 1の場合、sOldKeyは前行の値である必要があるため 
                If iSkipFlg = 0 Then
                    sOldKey = sNewKey   'OldキーにNewキーをセット
                End If
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
        dtSetRet.Rows(0).Item("RCV_HED_CAN_CNT") = iRcvHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_CAN_CNT") = iRcvDtlCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_HED_CAN_CNT") = iOutHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_DTL_CAN_CNT") = iOutDtlCanCnt.ToString()

        Return ds

    End Function

#End Region

#End Region

End Class
