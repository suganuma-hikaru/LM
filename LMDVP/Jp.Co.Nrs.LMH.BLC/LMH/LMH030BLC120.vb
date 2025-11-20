' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID　　　　:  120　　　 : ITW
'  作  成  者       :  honmyo
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC120
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC120 = New LMH030DAC120()

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

#Region "ITW用CONST"

    ''' <summary>
    ''' ITW
    ''' </summary>
    ''' <remarks></remarks>
    Public Const WH_CD_ITW As String = "101"                '倉庫コード
    Public Const CUST_CD_L_ITW As String = "00555"          '荷主コード（大）
    Public Const CUST_CD_M_ITW As String = "00"             '荷主コード（中）

    '要望番号:1209(出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 Start
    'Public Const NRS_GOODS_CD_UNSO As String = "B0000999999999999999"           '商品コード（運送）
    Public Const NRS_GOODS_CD_UNSO As String = ""                               '商品コード（運送）空を送る
    '要望番号:1209(出荷EDI→運送登録仕様見直し②商品マスタの存在チェックは外す) 2012/06/28 本明 End
    Public Const IRIME_UNSO As String = "0"                 '入目値（運送）

    ''' <summary>
    ''' その他
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DEF_CTL_NO As String = "B00000000"             '管理番号初期値
    Public Const DEF_UNSO_NO_L As String = "01-B00000000"       '運送番号初期値
    Public Const DEF_UNSO_NO_M As String = "01-B00000000000"    '運送番号初期値

#End Region

#Region "Method"

    '#Region "運送登録処理"
    '    ''' <summary>
    '    ''' 運送登録処理
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function OutkaToroku(ByVal ds As DataSet) As DataSet

    '        Dim msgArray(5) As String
    '        Dim choiceKb As String = String.Empty

    '        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
    '        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
    '        Dim ediCustIdx As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CUST_INDEX").ToString()
    '        '2012.03.25 大阪対応START
    '        Dim unsoFlg As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CUST_UNSOFLG").ToString()
    '        '2012.03.25 大阪対応END

    '        'EDI出荷(大)の値取得
    '        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

    '        If ds.Tables("LMH030_OUTKAEDI_L").Rows.Count = 0 Then
    '            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
    '            Return ds
    '        End If

    '        'EDI出荷(大)の初期値設定
    '        ds = Me.SetEdiLShoki(ds)

    '        'EDI出荷(中)の値取得
    '        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

    '        If ds.Tables("LMH030_OUTKAEDI_M").Rows.Count = 0 Then
    '            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
    '            Return ds
    '        End If

    '        'EDI出荷(大)の初期値設定後の関連チェック
    '        If Me.EdiLKanrenCheck(ds, rowNo, ediCtlNo) = False Then
    '            Return ds
    '        End If

    '        'EDI出荷(大)の初期値設定後のDB存在チェック
    '        If Me.EdiLDbExistsCheck(ds, rowNo, ediCtlNo) = False Then
    '            Return ds
    '        End If

    '        '届先コードの初期値設定
    '        ds = Me.SetDestCd(ds)

    '        'EDI出荷(中)の初期値設定後のマスタ存在チェック
    '        If Me.EdiMMasterExistsCheck(ds, rowNo, ediCtlNo) = False Then
    '            Return ds
    '        End If

    '        'EDI出荷(中)の初期値設定後の関連チェック
    '        If Me.EdiMKanrenCheck(ds, rowNo, ediCtlNo) = False Then
    '            Return ds
    '        End If

    '        '出荷管理番号(大)の採番
    '        ds = Me.GetOutkaNoL(ds)

    '        ''出荷管理番号(中)の採番
    '        ds = Me.GetOutkaNoM(ds)


    '        '注意)運送登録なので、出荷は作成しないがデータセット設定は行う
    '        '理由)後続の運送設定の際に、出荷(大)で設定された運送個数を使用したい為
    '        '出荷(大)データセット設定処理
    '        ds = Me.SetDatasetOutkaL(ds)

    '        'EDI受信テーブル(DTL)データセット設定
    '        ds = Me.SetDatasetEdiRcvDtl(ds)

    '        '運送(大,中)データセット設定
    '        ds = Me.SetDatasetUnsoL(ds)
    '        ds = Me.SetDatasetUnsoM(ds)

    '        '運送(大)の運送重量をデータセットに再設定
    '        ds = Me.SetdatasetUnsoJyuryo(ds)

    '        '運送登録(通常処理)
    '        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
    '        If MyBase.GetResultCount = 0 Then
    '            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
    '            Return ds
    '        End If

    '        'EDI出荷(中)の更新
    '        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

    '        'EDI受信(DTL)の更新
    '        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

    '        '届先マスタの自動追加
    '        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
    '               AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
    '            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "1"
    '            ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)
    '            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "0"
    '        End If

    '        '届先マスタの自動更新
    '        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
    '           AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_UPDATE_FLG").Equals("1") = True Then
    '            ds.Tables("LMH030_M_DEST").Rows(0).Item("UPDATE_TARGET_FLG") = "1"
    '            ds = MyBase.CallDAC(Me._Dac, "UpdateMDestData", ds)
    '            If MyBase.GetResultCount = 0 Then
    '                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
    '                Return ds
    '            End If
    '            ds.Tables("LMH030_M_DEST").Rows(0).Item("UPDATE_TARGET_FLG") = "0"
    '        End If

    '        '運送(大)の新規登録(データセットに設定されている場合のみ)
    '        If ds.Tables("LMH030_UNSO_L").Rows.Count <> 0 Then
    '            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoLData", ds)
    '        End If

    '        '運送(中)の新規登録(データセットに設定されている場合のみ)
    '        If ds.Tables("LMH030_UNSO_M").Rows.Count <> 0 Then
    '            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoMData", ds)
    '        End If

    '        Return ds

    '    End Function
    '#End Region

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

        'Dim autoMatomeF As String = ds.Tables("LMH030INOUT").Rows(0)("AUTO_MATOME_FLG").ToString()
        'Dim matomeNo As String = String.Empty
        'Dim matomeFlg As Boolean = False
        'Dim UnsoMatomeFlg As Boolean = False

        '出荷管理番号(大)の採番
        ds = Me.GetOutkaNoL(ds)

        ''出荷管理番号(中)の採番
        ds = Me.GetOutkaNoM(ds)

        '紐付け処理の場合は、別Funcでデータセット設定+更新処理
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()
        If eventShubetsu.Equals("3") Then
            '紐付処理をして終了
            ds = Me.Himoduke(ds)
            Return ds

        End If

        '出荷(大)データセット設定処理
        ds = Me.SetDatasetOutkaL(ds)

        '出荷(中)データセット設定
        ds = Me.SetDatasetOutkaM(ds)

        'EDI受信テーブル(DTL)データセット設定
        ds = Me.SetDatasetEdiRcvDtl(ds)

#If True Then   'ADD 2019/08/02 006202   【LMS】商品に出荷時作業加工区分が登録されている商品のEDI出荷登録できない荷主有り調査、改修依頼
        '作業レコードデータセット設定
        ds = Me.SetDatasetSagyo(ds)
#End If
        '運送(大,中)データセット設定
        ds = Me.SetDatasetUnsoL(ds)
        ds = Me.SetDatasetUnsoM(ds)

        '運送(大)の運送重量をデータセットに再設定
        ds = Me.SetdatasetUnsoJyuryo(ds)

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

        'If matomeFlg = False Then
        '出荷(大)の新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertOutkaLData", ds)
        'Else
        '出荷(大)のまとめ更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
        'End If

        '出荷(中)の新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertOutkaMData", ds)

#If True Then   'ADD 2019/08/02 006202   【LMS】商品に出荷時作業加工区分が登録されている商品のEDI出荷登録できない荷主有り調査、改修依頼
        '作業の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_E_SAGYO").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertSagyoData", ds)
        End If

#End If
        '届先マスタの自動追加
        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
               AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "1"
            ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "0"
        End If

        '届先マスタの更新 2012.03.20 追加START
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
        '届先マスタの更新 2012.03.20 追加END

        ''作業の新規登録(データセットに設定されている場合のみ)
        'If ds.Tables("LMH030_E_SAGYO").Rows.Count <> 0 Then
        '    ds = MyBase.CallDAC(Me._Dac, "InsertSagyoData", ds)
        'End If

        '運送(大)の新規登録(データセットに設定されている場合のみ)
        'If ds.Tables("LMH030_UNSO_L").Rows.Count <> 0 Then
        'If matomeFlg = False Then
        ds = MyBase.CallDAC(Me._Dac, "InsertUnsoLData", ds)
        'Else
        'ds = MyBase.CallDAC(Me._Dac, "UpdateMatomesakiUnsoLData", ds)
        'End If
        'End If

        '運送(中)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_M").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoMData", ds)
        End If

        'If matomeFlg = True Then
        '    'まとめ先EDI出荷(大)の更新(まとめ先EDIデータにまとめ番号を設定)
        '    ds = MyBase.CallDAC(Me._Dac, "UpdateMatomesakiEdiLData", ds)
        '    If MyBase.GetResultCount = 0 Then
        '        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '        Return ds
        '    End If
        'End If

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
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = False OrElse
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
                ediDr("COA_YN") = "0"  'SetInsMDestFromDestの値と一致させる事！（荷主により値が異なるため）
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
        If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = True AndAlso
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
                If String.IsNullOrEmpty(mDestDr("DELI_ATT").ToString().Trim()) = False AndAlso
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
            If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = False AndAlso
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
        If (ediDr("UNSO_MOTO_KB").ToString()).Equals("10") = True OrElse
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

    Private Function EdiMDefaultSet(ByVal ds As DataSet, ByVal setDs As DataSet,
                                    ByVal count As Integer, ByVal unsodata As String,
                                    ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(count)
        Dim mGoodsDr As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)
        Dim compareWarningFlg As Boolean = False

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)

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
        'DEL Start 2018.10.22 要望管理002632 オーダー番号Mが空の場合でもオーダー番号Lで置換しないよう修正
        'If String.IsNullOrEmpty(ediMDr("CUST_ORD_NO_DTL").ToString()) = True Then
        '    ediMDr("CUST_ORD_NO_DTL") = ediLDr("CUST_ORD_NO")
        'End If
        'DEL End   2018.10.22 要望管理002632 オーダー番号Mが空の場合でもオーダー番号Lで置換しないよう修正

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

        '2012.03.20 JC大秦化工(注意事項)
        '関塗工(運送データ)の場合以下の４項目は値の入れ替えはしない
        '↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        '個数単位
        '数量単位
        '包装個数
        '包装単位
        '↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

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

#If True Then 'ADD 2018/12/26 マイナスは対象外とする
        Dim dTTL_NB As Double = Convert.ToDouble(ediMDr("OUTKA_TTL_NB").ToString)
        If dTTL_NB < 0 Then
            'マイナスの場合はエラーとする
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E185", New String() {"個数"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

#End If
        '出荷包装個数
        '出荷端数
#If False Then
        Dim pkgNb As Double = Convert.ToDouble(ediMDr("PKG_NB"))
#Else
        'mGoodsDr
        Dim pkgNb As Double = Convert.ToDouble(mGoodsDr("PKG_NB"))

        '今回商品選択時（出荷登録で）
        If ("0").Equals(ediMDr("PKG_NB").ToString) Then
            ediMDr("PKG_NB") = Convert.ToDouble(mGoodsDr("PKG_NB"))         '入数
            ediMDr("BETU_WT") = Convert.ToDouble(mGoodsDr("STD_WT_KGS"))    '重量
            ediMDr("ONDO_KB") = Convert.ToDouble(mGoodsDr("ONDO_KB"))       '保管温度

        End If


#End If
        Dim outkaPkgNb As Double = Convert.ToDouble(ediMDr("OUTKA_PKG_NB"))
        Dim outkaHasu As Double = Convert.ToDouble(ediMDr("OUTKA_HASU"))
        Dim alctdKb As String = ediMDr("ALCTD_KB").ToString
        Dim irime As Double = Convert.ToDouble(ediMDr("IRIME"))
        Dim outkaTtlQt As Double = Convert.ToDouble(ediMDr("OUTKA_TTL_QT"))

        Select Case alctdKb

            Case "01"
                If 1 < pkgNb Then

                    '2012.06.18 修正START
                    ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
                    ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb
                    ediMDr("OUTKA_TTL_QT") = (pkgNb * outkaPkgNb + outkaHasu) * irime
                    '2012.06.18 修正END

                Else

                    '2012.06.18 修正START
                    '運送EDIの場合、商品マスタに存在する汎用商品は入数１なので、PKG_NBは計算に含めない
                    ediMDr("OUTKA_PKG_NB") = outkaPkgNb + outkaHasu
                    ediMDr("OUTKA_HASU") = 0
                    ediMDr("OUTKA_TTL_QT") = (outkaPkgNb + outkaHasu) * irime
                    '2012.06.18 修正END

                End If

                'If 1 < pkgNb Then
                '    ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
                '    ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb
                'Else
                '    ediMDr("OUTKA_PKG_NB") = pkgNb * outkaPkgNb + outkaHasu
                '    ediMDr("OUTKA_HASU") = 0
                'End If

                'ediMDr("OUTKA_TTL_QT") = (pkgNb * outkaPkgNb + outkaHasu) * irime

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

        '要望番号:1209(出荷EDI→運送登録仕様見直し②重量がマイナスの場合はエラーとする) 2012/06/28 本明 Start
        Dim dBetuWt As Double = Convert.ToDouble(ediMDr("BETU_WT").ToString)
        If dBetuWt < 0 Then
            '重量がマイナスの場合はエラーとする
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E185", New String() {"個別重量"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If
        '要望番号:1209(出荷EDI→運送登録仕様見直し②重量がマイナスの場合はエラーとする) 2012/06/28 本明 End

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
        '(関塗工は現状ワーニング設定なし)
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

#If True Then   'ADD 2018/10/02 依頼番号 : 002437   【LMS】千葉ITW_セミEDIの追加改修
        '保管荷役料計算最終日以前のデータは出荷登録させない。 LMH030_M_CUST HOKAN_NIYAKU_CALCULATION
        Dim sHOKAN_NIYAKU_CALCULATION As String = ds.Tables("LMH030_M_CUST").Rows(0).Item("HOKAN_NIYAKU_CALCULATION").ToString.Trim

        If String.IsNullOrEmpty(sHOKAN_NIYAKU_CALCULATION) = False Then
            If drEdiL("OUTKA_PLAN_DATE").ToString <= sHOKAN_NIYAKU_CALCULATION Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E977", New String() {drEdiL("CUST_ORD_NO").ToString}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False

            End If
        End If
#End If

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
        '●荷主固有チェック(関塗工専用)
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
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "運送登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '入目 + 出荷総数量
        If Me._Blc.IrimeSosuryoLargeSmallCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"入目と出荷総数量"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '赤黒区分
        If Me._Blc.AkakuroKbCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "運送登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
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

            If String.IsNullOrEmpty(dtL.Rows(0).Item("FREE_C30").ToString()) = False Then
                unsoData = dtL.Rows(0).Item("FREE_C30").ToString().Substring(0, 2)
            End If

            If String.IsNullOrEmpty(custGoodsCd) = False Then

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDtL.ImportRow(dtL.Rows(0))
                setDtM.ImportRow(dtM.Rows(i))

                choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.KTK_WID_M001, 0)

                '商品マスタ検索（NRS商品コード or 荷主商品コード）
#If False Then  'UPD 2018/12/14 依頼番号 : 003818   【LMS】ITWセミEDI_エクセル取込時 
                setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsOutka", setDs))

#Else
                setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsOutkaLike", setDs))

#End If

                '基本的に関塗工の場合は、汎用商品コードの商品キーが入ってくる為１件になる。
                If MyBase.GetResultCount = 0 Then

                    '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 Start
                    '商品マスタが存在しない場合はダミーで商品データセットを作成する
                    'DEL 2018/12/13setDs = Me._Blc.SetDummyGoodsM(ds, setDs, i)
                    '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 End

                    '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 Start（以下コメント化）
                    ''要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
#If True Then   'UPD 2018/12/13  コメント解除やめ
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"商品コード", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End
                    Return False
#End If
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

                        ds = Me._Blc.SetComWarningM("W162", LMH030BLC.KTK_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)

                        flgWarning = True 'ワーニングフラグをたてて処理続行

                        Continue For
                    End If

#If False Then   'ADD 2018/12/25 Goods情報設定(出荷登録で複数商品から選択時)
                ElseIf GetResultCount() = 1 Then
                    If (String.Empty).Equals(setDtM.Rows(i).Item("IRIME_UT")) Then
                        setDtM.Rows(i).Item("NRS_GOODS_CD") = setDs.Tables("LMH030_M_GOODS").Rows(0).Item("GOODS_CD_NRS")
                        setDtM.Rows(i).Item("GOODS_NM") = setDs.Tables("LMH030_M_GOODS").Rows(0).Item("GOODS_NM_1")
                        setDtM.Rows(i).Item("IRIME") = setDs.Tables("LMH030_M_GOODS")(0).Item("STD_IRIME_NB")
                        setDtM.Rows(i).Item("ALCTD_KB") = setDs.Tables("LMH030_M_GOODS").Rows(0).Item("ALCTD_KB")
                        setDtM.Rows(i).Item("PKG_NB") = setDs.Tables("LMH030_M_GOODS")(0).Item("PKG_NB")
                        setDtM.Rows(i).Item("IRIME_UT") = setDs.Tables("LMH030_M_GOODS").Rows(0).Item("STD_IRIME_UT")
                    End If
#End If
                End If

                'EDI出荷(中)の初期値設定処理
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                    '関塗工は現段階ではワーニングはないが、共通のロジックを組み込む為入れておく
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

#Region "届先マスタチェック(関塗工)"
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
        Dim ediADD As String = dtEdi.Rows(0)("DEST_AD_1").ToString()
        Dim ediFreeC21 As String = dtEdi.Rows(0)("FREE_C21").ToString()
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        Dim compareWarningFlg As Boolean = False

        '削除フラグ(届先マスタ)
        If mSysDelF.Equals("1") = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E331", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '現LMSでチェックコメントアウトの為コメント化
        mDestNm = Me.SpaceCutChk(mDestNm)
        ediDestNm = Me.SpaceCutChk(ediDestNm)

        '届先名称(マスタ値が完全一致でなければワーニング)
        If mDestNm.Equals(ediDestNm) = True Then
            'チェックなし
        Else
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L008, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "届先名称"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "届先名称"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W166", LMH030BLC.KTK_WID_L008, ds, msgArray, ediDestNm, mDestNm)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("DEST_NM") = dtEdi.Rows(0)("DEST_NM").ToString()
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            End If

        End If

        '現LMSでチェックコメントアウトの為コメント化
        'FREE_C21:届先住所(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediFreeC21) = True Then
            'チェックなし
        Else

            mAdAll = SpaceCutChk(mAdAll)
            ediFreeC21 = SpaceCutChk(ediFreeC21)
            If mAdAll.Equals(ediFreeC21) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L009, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先住所"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "住所"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.KTK_WID_L009, ds, msgArray, ediFreeC21, mAdAll)

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

        '届先郵便番号(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediZip) = True Then
            'チェックなし
        Else
            If mZip.Equals(Replace(ediZip, "-", String.Empty)) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L001, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先郵便番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "郵便番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.KTK_WID_L001, ds, msgArray, ediZip, mZip)

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

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L002, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先電話番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "電話番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.KTK_WID_L002, ds, msgArray, ediTel, mTel)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("TEL") = dtEdi.Rows(0)("DEST_TEL").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If

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

            '2012.03.01 大阪対応START
            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し運送登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "郵便番号マスタ"
            End If
            '2012.03.01 大阪対応END
        End If

        'Else
        '取得できなかった場合は、再度住所を元にJISマスタよりJISコードを取得する
        If String.IsNullOrEmpty(mZipJis) = True Then

            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromAdd", ds)

            'If MyBase.GetResultCount = 0 Then
            '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"届先住所１＋届先住所２＋届先住所３", "JISマスタ", "県＋市"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return False
            'End If

            'mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
            'warningString = "JISマスタ"

            '2012.03.01 大阪対応START
            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し運送登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "JISマスタ"
            End If
            '2012.03.01 大阪対応END

        End If
        '2012.03.28 要望番号948 修正END


        If String.IsNullOrEmpty(mZipJis) = True AndAlso String.IsNullOrEmpty(ediDestJisCd) = False Then
            'JISマスタのJISコードが空or郵便番号マスタのJISコードが空の場合かつ、EDIデストコードに値がある場合、更新ワーニング
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L003, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = "JISコード"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = "EDIデータ"
                msgArray(5) = "※郵便番号・住所からJISコードが特定できないため、出荷画面で運賃がゼロになる可能性があります。"
                ds = Me._Blc.SetComWarningL("W197", LMH030BLC.KTK_WID_L003, ds, msgArray, ediDestJisCd, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("JIS") = ediDestJisCd
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            End If


        ElseIf String.IsNullOrEmpty(mZipJis) = True AndAlso String.IsNullOrEmpty(ediDestJisCd) = True Then
            'JISマスタのJISコードが空or郵便番号マスタのJISコードが空の場合かつ、EDIデストコードが空の場合、処理続行確認
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L004, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                ds = Me._Blc.SetComWarningL("W188", LMH030BLC.KTK_WID_L004, ds, msgArray, ediDestJisCd, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時

            End If

        ElseIf String.IsNullOrEmpty(ediDestJisCd) = False AndAlso ediDestJisCd.Equals(mJis) = False Then
            'EDIのJISコードが空でなくEDIのJISコードと届先マスタのJISコードに差異がある場合
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L005, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = "JISコード"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W187", LMH030BLC.KTK_WID_L005, ds, msgArray, ediDestJisCd, mJis)

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
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L006, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = String.Concat(warningString, "から取得したJISコード")
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = warningString
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W187", LMH030BLC.KTK_WID_L006, ds, msgArray, mZipJis, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("JIS") = mZipJis
                dtEdi.Rows(0)("DEST_JIS_CD") = mZipJis '追加箇所 20110222
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

        '関塗工共通入替え項目
        '届先JISコード
        If String.IsNullOrEmpty(ediDestJisCd) = True Then
            dtEdi.Rows(0)("DEST_JIS_CD") = mZipJis
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

            '2012.03.01 大阪対応START
            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し運送登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "郵便番号マスタ"
            End If
            '2012.03.01 大阪対応END
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

            '2012.03.01 大阪対応START
            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し運送登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "JISマスタ"
            End If
            '2012.03.01 大阪対応END

        End If
        '2012.03.28 要望番号948 修正END

        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.KTK_WID_L007, 0)

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


            ds = Me._Blc.SetComWarningL("W186", LMH030BLC.KTK_WID_L007, ds, msgArray, workDestCd, String.Empty) '追加箇所 20110222

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
            drMD("ZIP") = Replace(drEdiL("DEST_ZIP").ToString(), "-", String.Empty)
            drMD("AD_1") = drEdiL("DEST_AD_1").ToString()
            drMD("AD_2") = drEdiL("DEST_AD_2").ToString()
            drMD("AD_3") = drEdiL("DEST_AD_3").ToString()
            drMD("COA_YN") = "00"
            drMD("TEL") = drEdiL("DEST_TEL").ToString()
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
    Private Function SetDestWarningChoiceKb(ByVal setDt As DataTable, ByVal ds As DataSet,
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
    Private Function SetGoodsWarningChoiceKb(ByRef setDt As DataTable, ByVal ds As DataSet,
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

    '#Region "データセット設定(運送番号)"

    '    ''' <summary>
    '    ''' データセット設定(運送番号)
    '    ''' </summary>
    '    ''' <param name="ds"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function GetOutkaNoL(ByVal ds As DataSet) As DataSet

    '        Dim dr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
    '        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
    '        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

    '        Dim max As Integer = dt.Rows.Count - 1

    '        '運送登録の場合
    '        Dim num As New NumberMasterUtility
    '        dr("FREE_C30") = String.Concat("01-", num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, nrsBrCd))

    '        Return ds

    '    End Function

    '#End Region
#Region "データセット設定(出荷管理番号L)"

    ''' <summary>
    ''' データセット設定(出荷管理番号L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoL(ByVal ds As DataSet) As DataSet

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

            'ElseIf matomeFlg = False Then
        Else
            '通常出荷登録処理の場合
            Dim num As New NumberMasterUtility
            outkaKanriNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.OUTKA_NO_L, Me, nrsBrCd)

            dr("OUTKA_CTL_NO") = outkaKanriNo

            For i As Integer = 0 To max
                dt.Rows(i)("OUTKA_CTL_NO") = outkaKanriNo
            Next

            'Else
            '    'まとめ処理の場合
            '    outkaKanriNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("OUTKA_CTL_NO").ToString()
            '    dr("OUTKA_CTL_NO") = outkaKanriNo
            '    dr("FREE_C30") = String.Concat("04-", ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("EDI_CTL_NO").ToString())

            '    For i As Integer = 0 To max
            '        dt.Rows(i)("OUTKA_CTL_NO") = outkaKanriNo
            '    Next

        End If

        Return ds

    End Function

#End Region

#End Region

    '#Region "データセット設定(出荷管理番号M)"
    '    ''' <summary>
    '    ''' 出荷管理番号(中)取得
    '    ''' </summary>
    '    ''' <param name="ds"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function GetOutkaNoM(ByVal ds As DataSet) As DataSet

    '        Dim dtEdiM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
    '        Dim nrsBrCd As String = dtEdiM.Rows(0).ToString
    '        Dim max As Integer = dtEdiM.Rows.Count - 1

    '        Dim unsoNoL As String = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("FREE_C30").ToString()
    '        Dim unsoNoM As String = String.Empty

    '        '運送登録の場合
    '        For i As Integer = 0 To max
    '            unsoNoM = (i + 1).ToString("000")
    '            dtEdiM.Rows(i)("FREE_C30") = String.Concat(unsoNoL, unsoNoM)
    '        Next

    '        Return ds

    '    End Function

    '#End Region
#Region "データセット設定(出荷管理番号M)"
    ''' <summary>
    ''' 出荷管理番号(中)取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoM(ByVal ds As DataSet) As DataSet

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

            'ElseIf matomeFlg = False Then
        Else

            '通常出荷登録処理の場合
            For i As Integer = 0 To max
                outkaKanriNo = (i + 1).ToString("000")
                dtEdiM.Rows(i)("OUTKA_CTL_NO_CHU") = outkaKanriNo
            Next

            'Else
            '    'まとめ処理の場合、まとめ先DataSetから取得
            '    '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo Start
            '    'Dim maxOutkaKanriNo As Integer = Convert.ToInt32(ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("OUTKA_CTL_NO_CHU"))
            '    Dim maxOutkaKanriNo As Integer = Me._DacCom.GetMaxOUTKA_NO_CHU(ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("NRS_BR_CD").ToString, ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("OUTKA_CTL_NO").ToString)
            '    '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo End
            '    For i As Integer = 0 To max
            '        outkaKanriNo = (maxOutkaKanriNo + i + 1).ToString("000")
            '        dtEdiM.Rows(i)("OUTKA_CTL_NO_CHU") = outkaKanriNo
            '    Next

        End If

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
#End Region

    '#Region "データセット設定(出荷L)"
    '    ''' <summary>
    '    ''' データセット設定(出荷L)
    '    ''' </summary>
    '    ''' <param name="ds"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function SetDatasetOutkaL(ByVal ds As DataSet) As DataSet

    '        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
    '        Dim outkaDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").NewRow()
    '        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
    '        Dim matomesakiDt As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")

    '        '通常登録処理
    '        outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
    '        outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
    '        outkaDr("OUTKA_KB") = ediDr("OUTKA_KB")
    '        outkaDr("SYUBETU_KB") = ediDr("SYUBETU_KB")
    '        outkaDr("OUTKA_STATE_KB") = ediDr("OUTKA_STATE_KB")
    '        outkaDr("OUTKAHOKOKU_YN") = Me._Blc.FormatZero(ediDr("OUTKAHOKOKU_YN").ToString(), 2)
    '        outkaDr("PICK_KB") = ediDr("PICK_KB")
    '        outkaDr("DENP_NO") = String.Empty
    '        outkaDr("ARR_KANRYO_INFO") = String.Empty
    '        outkaDr("WH_CD") = ediDr("WH_CD")
    '        outkaDr("OUTKA_PLAN_DATE") = ediDr("OUTKA_PLAN_DATE")
    '        outkaDr("OUTKO_DATE") = ediDr("OUTKO_DATE")
    '        outkaDr("ARR_PLAN_DATE") = ediDr("ARR_PLAN_DATE")
    '        outkaDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
    '        outkaDr("HOKOKU_DATE") = ediDr("HOKOKU_DATE")
    '        outkaDr("TOUKI_HOKAN_YN") = Me._Blc.FormatZero(ediDr("TOUKI_HOKAN_YN").ToString(), 2)
    '        outkaDr("END_DATE") = String.Empty
    '        outkaDr("CUST_CD_L") = ediDr("CUST_CD_L")
    '        outkaDr("CUST_CD_M") = ediDr("CUST_CD_M")
    '        outkaDr("SHIP_CD_L") = ediDr("SHIP_CD_L")
    '        outkaDr("SHIP_CD_M") = String.Empty

    '        If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
    '            Dim destMDr As DataRow = ds.Tables("LMH030_M_DEST").Rows(0)
    '            outkaDr("DEST_CD") = destMDr("DEST_CD")
    '            outkaDr("DEST_AD_3") = destMDr("AD_3")
    '            outkaDr("DEST_TEL") = destMDr("TEL")
    '        Else
    '            outkaDr("DEST_CD") = ediDr("DEST_CD")
    '            outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")
    '            outkaDr("DEST_TEL") = ediDr("DEST_TEL")
    '        End If

    '        outkaDr("NHS_REMARK") = String.Empty
    '        outkaDr("SP_NHS_KB") = ediDr("SP_NHS_KB")
    '        outkaDr("COA_YN") = Me._Blc.FormatZero(ediDr("COA_YN").ToString(), 2)
    '        outkaDr("CUST_ORD_NO") = ediDr("CUST_ORD_NO")
    '        outkaDr("BUYER_ORD_NO") = ediDr("BUYER_ORD_NO")
    '        outkaDr("REMARK") = ediDr("REMARK")
    '        outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt)
    '        outkaDr("DENP_YN") = Me._Blc.FormatZero(ediDr("DENP_YN").ToString(), 2)
    '        outkaDr("PC_KB") = ediDr("PC_KB")
    '        outkaDr("UNCHIN_YN") = Me._Blc.FormatZero(ediDr("UNCHIN_YN").ToString(), 2)
    '        outkaDr("NIYAKU_YN") = Me._Blc.FormatZero(ediDr("NIYAKU_YN").ToString(), 2)
    '        outkaDr("ALL_PRINT_FLAG") = "00"
    '        outkaDr("NIHUDA_FLAG") = "00"
    '        outkaDr("NHS_FLAG") = "00"
    '        outkaDr("DENP_FLAG") = "00"
    '        outkaDr("COA_FLAG") = "00"
    '        outkaDr("HOKOKU_FLAG") = "00"
    '        outkaDr("MATOME_PICK_FLAG") = "00"
    '        outkaDr("LAST_PRINT_DATE") = String.Empty
    '        outkaDr("LAST_PRINT_TIME") = String.Empty
    '        outkaDr("SASZ_USER") = String.Empty
    '        outkaDr("OUTKO_USER") = String.Empty
    '        outkaDr("KEN_USER") = String.Empty
    '        outkaDr("OUTKA_USER") = String.Empty
    '        outkaDr("HOU_USER") = String.Empty
    '        outkaDr("ORDER_TYPE") = String.Empty
    '        outkaDr("SYS_DEL_FLG") = "0"
    '        outkaDr("DEST_KB") = "02"
    '        outkaDr("DEST_NM") = ediDr("DEST_NM")
    '        outkaDr("DEST_AD_1") = ediDr("DEST_AD_1")
    '        outkaDr("DEST_AD_2") = ediDr("DEST_AD_2")
    '        'データセットに設定
    '        ds.Tables("LMH030_C_OUTKA_L").Rows.Add(outkaDr)

    '        Return ds

    '    End Function

    '#End Region

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
        Dim matomesakiDt As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")

        'If matomeFlg = False Then
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
        outkaDr("DEST_KB") = "02"
        outkaDr("DEST_NM") = ediDr("DEST_NM")
        outkaDr("DEST_AD_1") = ediDr("DEST_AD_1")
        outkaDr("DEST_AD_2") = ediDr("DEST_AD_2")
        'Else
        ''まとめ登録処理
        'outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
        'outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
        'outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt) + Convert.ToDouble(matomesakiDt.Rows(0)("OUTKA_PKG_NB"))
        'outkaDr("SYS_UPD_DATE") = matomesakiDt.Rows(0)("SYS_UPD_DATE")
        'outkaDr("SYS_UPD_TIME") = matomesakiDt.Rows(0)("SYS_UPD_TIME")

        ' ''現場要望(要望番号922 2012.03.29 追加START(まとめ処理：荷主注文番号連結)

        'Dim strCustOrder As String = "CUST_ORD_NO"
        'Dim byteCnt As Integer = 30

        ''現場要望(要望番号851) 荷主注文番号もまとめの場合は足し込む
        ''②今からまとめるEDI出荷(大)の荷主注文番号とまとめられる出荷(大)の荷主注文番号のチェック

        ''②-1 EDI出荷(大)の荷主注文番号が空の場合は、まとめられる出荷(大)の荷主注文番号をそのまま使用
        'If String.IsNullOrEmpty(ediDr(strCustOrder).ToString()) = True Then
        '    outkaDr(strCustOrder) = matomesakiDt.Rows(0)(strCustOrder)

        '    '②-2 出荷(大)の荷主注文番号が空の場合は、今からまとめるEDI出荷(大)の荷主注文番号を付与
        'ElseIf String.IsNullOrEmpty(matomesakiDt.Rows(0)(strCustOrder).ToString()) = True Then
        '    outkaDr(strCustOrder) = ediDr(strCustOrder)

        '    '②-3 出荷(大)の荷主注文番号が空でない場合で、今からまとめるEDI出荷(大)の荷主注文番号が同じ場合は、
        '    '出荷(大)の荷主注文番号をそのまま使用
        'ElseIf String.IsNullOrEmpty(matomesakiDt.Rows(0)(strCustOrder).ToString()) = False AndAlso _
        '       (matomesakiDt.Rows(0)(strCustOrder).ToString()).Equals(ediDr(strCustOrder).ToString().Trim()) = True Then
        '    outkaDr(strCustOrder) = matomesakiDt.Rows(0)(strCustOrder)

        '    '②-4 出荷(大)の荷主注文番号にまとめるEDI出荷(大)の荷主注文番号が含まれていない場合は、
        '    '出荷(大)の荷主注文番号に、まとめるEDI出荷(大)の荷主注文番号を付与
        'ElseIf InStr(matomesakiDt.Rows(0)(strCustOrder).ToString().Trim(), ediDr(strCustOrder).ToString().Trim()) = 0 Then
        '    outkaDr(strCustOrder) = Me._Blc.LeftB(String.Concat(matomesakiDt.Rows(0)(strCustOrder).ToString().Trim(), Space(1), ediDr(strCustOrder).ToString().Trim()), byteCnt)

        '    '②-5 出荷(大)の荷主注文番号にまとめるEDI出荷(大)の荷主注文番号が含まれている場合は、
        '    '出荷(大)の荷主注文番号をそのまま使用
        'ElseIf InStr(matomesakiDt.Rows(0)(strCustOrder).ToString().Trim(), ediDr(strCustOrder).ToString().Trim()) > 0 Then
        '    outkaDr(strCustOrder) = matomesakiDt.Rows(0)(strCustOrder)
        'End If

        ' ''要望番号922 2012.03.29 追加END

        'End If
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

            'sumNb = sumNb + Convert.ToDouble(dt.Rows(i)("OUTKA_PKG_NB"))
            sumNb = sumNb + calcPkgQuoNb

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
            outkaDr("COA_YN") = Me._Blc.FormatZero(ediDr("COA_YN").ToString(), 2)
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

            outkaDr("REMARK") = String.Empty
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

            'データセットに設定
            ds.Tables("LMH030_EDI_RCV_DTL").Rows.Add(rcvDr)

        Next

        Return ds

    End Function

#End Region

    'ADD 2019/08/02 006202   【LMS】商品に出荷時作業加工区分が登録されている商品のEDI出荷登録できない荷主有り調査、改修依頼
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

                    '通常登録時はEDI(中)よりセット
                    outkaNoLM = String.Concat(ediDrM("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))

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

                    '通常登録時はEDI(中)よりセット
                    outkaNoLM = String.Concat(ediDrM("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))

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

    '#Region "データセット設定(運送L)"
    '    ''' <summary>
    '    ''' データセット設定(運送L)
    '    ''' </summary>
    '    ''' <param name="ds"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function SetDatasetUnsoL(ByVal ds As DataSet) As DataSet

    '        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
    '        Dim unsoDr As DataRow = ds.Tables("LMH030_UNSO_L").NewRow()
    '        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
    '        Dim outkaLDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").Rows(0)
    '        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
    '        Dim ediMCntDr As DataRow

    '        Dim num As New NumberMasterUtility

    '        '通常登録
    '        unsoDr("NRS_BR_CD") = ediDr("NRS_BR_CD")

    '        '関塗工の場合は運送登録なので、前の処理でFREE_C30で取得した運送番号を使用
    '        unsoDr("UNSO_NO_L") = ediDr("FREE_C30").ToString().Substring(3, 9)

    '        unsoDr("YUSO_BR_CD") = ediDr("NRS_BR_CD")
    '        unsoDr("WH_CD") = ediDr("WH_CD")
    '        '関塗工の場合は運送登録なので、出荷管理番号は空
    '        unsoDr("INOUTKA_NO_L") = String.Empty
    '        unsoDr("TRIP_NO") = String.Empty
    '        unsoDr("UNSO_CD") = ediDr("UNSO_CD")
    '        unsoDr("UNSO_BR_CD") = ediDr("UNSO_BR_CD")
    '        unsoDr("BIN_KB") = ediDr("BIN_KB")
    '        unsoDr("JIYU_KB") = String.Empty

    '        '関塗工の場合は荷主注文番号をセット
    '        unsoDr("DENP_NO") = ediDr("CUST_ORD_NO")
    '        unsoDr("OUTKA_PLAN_DATE") = ediDr("OUTKA_PLAN_DATE")
    '        unsoDr("OUTKA_PLAN_TIME") = String.Empty
    '        unsoDr("ARR_PLAN_DATE") = ediDr("ARR_PLAN_DATE")
    '        unsoDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
    '        unsoDr("ARR_ACT_TIME") = String.Empty
    '        unsoDr("CUST_CD_L") = ediDr("CUST_CD_L")
    '        unsoDr("CUST_CD_M") = ediDr("CUST_CD_M")

    '        Dim max As Integer = ds.Tables("LMH030_OUTKAEDI_M").Rows.Count - 1

    '        For i As Integer = 0 To max
    '            ediMCntDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
    '            If InStr(unsoDr("CUST_REF_NO").ToString().Trim(), ediMCntDr("CUST_ORD_NO_DTL").ToString().Trim()) = 0 Then
    '                unsoDr("CUST_REF_NO") = Me._Blc.LeftB(Trim(String.Concat(unsoDr("CUST_REF_NO").ToString(), Space(2), ediMCntDr("CUST_ORD_NO_DTL").ToString())), 30)
    '            End If
    '        Next

    '        unsoDr("SHIP_CD") = ediDr("SHIP_CD_L")
    '        unsoDr("DEST_CD") = ediDr("DEST_CD")
    '        unsoDr("UNSO_PKG_NB") = outkaLDr("OUTKA_PKG_NB")
    '        'unsoDr("NB_UT") = ediDr("NB_UT") '運送Mで取得の為ここではコメント
    '        unsoDr("UNSO_WT") = 0             '運送Mの集計値
    '        unsoDr("UNSO_ONDO_KB") = ediMDr("UNSO_ONDO_KB")
    '        '要望番号1476:(出荷登録時、元着払いが〔着払い〕になる(要望番号1457関連)) 2012/09/28 本明 Start
    '        'unsoDr("PC_KB") = ediDr("PICK_KB")
    '        unsoDr("PC_KB") = ediDr("PC_KB")
    '        '要望番号1476:(出荷登録時、元着払いが〔着払い〕になる(要望番号1457関連)) 2012/09/28 本明 End
    '        unsoDr("TARIFF_BUNRUI_KB") = ediDr("UNSO_TEHAI_KB")
    '        unsoDr("VCLE_KB") = ediDr("SYARYO_KB")

    '        'ディック共同配送の場合は運送登録なので、元データ区分が変わる
    '        unsoDr("MOTO_DATA_KB") = "40"

    '        unsoDr("TAX_KB") = "01" '課税区分は"01"(課税)固定とする
    '        unsoDr("REMARK") = Me._Blc.LeftB(Trim(String.Concat(ediDr("REMARK").ToString(), Space(2), ediDr("UNSO_ATT").ToString())), 100)
    '        unsoDr("SEIQ_TARIFF_CD") = ediDr("UNCHIN_TARIFF_CD")
    '        unsoDr("SEIQ_ETARIFF_CD") = ediDr("EXTC_TARIFF_CD")
    '        unsoDr("AD_3") = outkaLDr("DEST_AD_3")

    '        unsoDr("UNSO_TEHAI_KB") = ediDr("UNSO_MOTO_KB")
    '        unsoDr("BUY_CHU_NO") = ediDr("BUYER_ORD_NO")
    '        unsoDr("AREA_CD") = String.Empty
    '        '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 Start
    '        'unsoDr("TYUKEI_HAISO_FLG") = String.Empty
    '        unsoDr("TYUKEI_HAISO_FLG") = "00"   '中継配送フラグ"00:無し"を設定
    '        '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 End
    '        unsoDr("SYUKA_TYUKEI_CD") = String.Empty
    '        unsoDr("HAIKA_TYUKEI_CD") = String.Empty
    '        unsoDr("TRIP_NO_SYUKA") = String.Empty
    '        unsoDr("TRIP_NO_TYUKEI") = String.Empty
    '        unsoDr("TRIP_NO_HAIKA") = String.Empty


    '        'START UMANO 要望番号1302 支払運賃に伴う修正。
    '        If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString()) = False AndAlso _
    '           String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString()) = False Then

    '            '運送会社マスタの取得(支払請求タリフコード,支払割増タリフコード)
    '            ds = MyBase.CallDAC(Me._DacCom, "SelectListDataShiharaiTariff", ds)
    '            Dim unsocoMDr As DataRow = ds.Tables("LMH030_SHIHARAI_TARIFF").Rows(0)

    '            If MyBase.GetResultCount > 0 Then
    '                unsoDr("SHIHARAI_TARIFF_CD") = unsocoMDr("UNCHIN_TARIFF_CD")
    '                unsoDr("SHIHARAI_ETARIFF_CD") = unsocoMDr("EXTC_TARIFF_CD")
    '            End If

    '        End If
    '        'END UMANO 要望番号1302 支払運賃に伴う修正。


    '        'データセットに設定
    '        ds.Tables("LMH030_UNSO_L").Rows.Add(unsoDr)

    '        Return ds

    '    End Function

    '#End Region

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
        Dim num As New NumberMasterUtility

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
        unsoDr("UNSO_WT") = 0             '運送Mの集計値
        unsoDr("UNSO_ONDO_KB") = ediMDr("UNSO_ONDO_KB")
        unsoDr("PC_KB") = ediDr("PC_KB")
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
        If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString()) = False AndAlso
           String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString()) = False Then

            '運送会社マスタの取得(支払請求タリフコード,支払割増タリフコード)
            ds = MyBase.CallDAC(Me._DacCom, "SelectListDataShiharaiTariff", ds)
            Dim unsocoMDr As DataRow = ds.Tables("LMH030_SHIHARAI_TARIFF").Rows(0)

            If MyBase.GetResultCount > 0 Then
                unsoDr("SHIHARAI_TARIFF_CD") = unsocoMDr("UNCHIN_TARIFF_CD")
                unsoDr("SHIHARAI_ETARIFF_CD") = unsocoMDr("EXTC_TARIFF_CD")
            End If

        End If

        'データセットに設定
        ds.Tables("LMH030_UNSO_L").Rows.Add(unsoDr)

        Return ds

    End Function

#End Region


    '#Region "データセット設定(運送M)"
    '    ''' <summary>
    '    ''' データセット設定(運送M)
    '    ''' </summary>
    '    ''' <param name="ds"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function SetDatasetUnsoM(ByVal ds As DataSet) As DataSet

    '        Dim ediDr As DataRow
    '        Dim unsoMDr As DataRow
    '        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
    '        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)

    '        Dim stdWtKgs As Decimal = 0
    '        Dim irime As Decimal = 0
    '        Dim stdIrimeNb As Decimal = 0
    '        Dim nisugata As Decimal = 0
    '        Dim outkaTtlNb As Decimal = 0

    '        Dim max As Integer = dt.Rows.Count - 1

    '        For i As Integer = 0 To max

    '            ediDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
    '            unsoMDr = ds.Tables("LMH030_UNSO_M").NewRow()

    '            unsoMDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
    '            unsoMDr("UNSO_NO_L") = unsoLDr("UNSO_NO_L")

    '            '関塗工の場合は運送登録なので、出荷管理番号(中)が存在しないので採番する
    '            '運送登録処理の場合
    '            unsoMDr("UNSO_NO_M") = (i + 1).ToString("000")
    '            unsoMDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
    '            unsoMDr("GOODS_NM") = ediDr("GOODS_NM")
    '            unsoMDr("UNSO_TTL_NB") = ediDr("OUTKA_PKG_NB")
    '            unsoMDr("NB_UT") = ediDr("KB_UT")
    '            unsoMDr("UNSO_TTL_QT") = ediDr("OUTKA_TTL_QT")
    '            unsoMDr("QT_UT") = ediDr("QT_UT")
    '            unsoMDr("HASU") = ediDr("OUTKA_HASU")
    '            unsoMDr("ZAI_REC_NO") = String.Empty

    '            '関塗工の場合は運送登録なので、運送温度区分が"90"の場合は入替
    '            If (ediDr("UNSO_ONDO_KB").ToString()).Equals("90") = True Then
    '                unsoMDr("UNSO_ONDO_KB") = ediDr("ONDO_KB")
    '            Else
    '                unsoMDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
    '            End If

    '            unsoMDr("IRIME") = ediDr("IRIME")
    '            unsoMDr("IRIME_UT") = ediDr("IRIME_UT")

    '            stdWtKgs = Convert.ToDecimal(ediDr("STD_WT_KGS"))
    '            irime = Convert.ToDecimal(ediDr("IRIME"))
    '            stdIrimeNb = Convert.ToDecimal(ediDr("STD_IRIME_NB"))

    '            If String.IsNullOrEmpty(ediDr("NISUGATA").ToString()) = False Then
    '                nisugata = Convert.ToDecimal(ediDr("NISUGATA"))
    '            End If
    '            outkaTtlNb = Convert.ToDecimal(ediDr("OUTKA_TTL_NB"))

    '            '2012.06.19 関塗工　新旧不具合対応
    '            'If ediDr("TARE_YN").Equals("01") = False Then
    '            '    unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb)

    '            'Else
    '            '    unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb + nisugata)

    '            'End If

    '            '運送EDI(関塗工)の場合、個別重量はEDI出荷(中)の個別重量をセット
    '            unsoMDr("BETU_WT") = ediDr("BETU_WT")
    '            '2012.06.19 関塗工　新旧不具合対応

    '            unsoMDr("SIZE_KB") = String.Empty
    '            unsoMDr("ZBUKA_CD") = String.Empty
    '            unsoMDr("ABUKA_CD") = String.Empty
    '            unsoMDr("PKG_NB") = ediDr("PKG_NB")
    '            unsoMDr("LOT_NO") = ediDr("LOT_NO")
    '            unsoMDr("REMARK") = ediDr("REMARK")

    '            'データセットに設定
    '            ds.Tables("LMH030_UNSO_M").Rows.Add(unsoMDr)
    '        Next

    '        Return ds

    '    End Function

    '#End Region

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
    ''' 運送重量再計算処理(運送EDI荷主：関塗工の場合は入数は計算式に含まない)
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
            '2012.06.13 修正START 関塗工の場合　取込時に入数が 0セットされるので、入数は計算式に含まない 
            'NB = Convert.ToDecimal(unsoMDr("UNSO_TTL_NB")) * Convert.ToDecimal(unsoMDr("PKG_NB")) + Convert.ToDecimal(unsoMDr("HASU"))
            NB = Convert.ToDecimal(unsoMDr("UNSO_TTL_NB")) + Convert.ToDecimal(unsoMDr("HASU"))
            '2012.06.13 修正END

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
    Private Function SetDatasetEdiMUnsoJyuryo(ByVal ds As DataSet, ByVal setDs As DataSet, ByVal count As Integer,
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

        If String.IsNullOrEmpty(ediMDr("PKG_UT").ToString()) = False Then

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

        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH030_H_OUTKAEDI_DTL_ITW").NewRow()
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
        drEdiRcvDtl("CUST_CD_L") = CUST_CD_L_ITW                                                        '荷主コード（大）
        drEdiRcvDtl("CUST_CD_M") = CUST_CD_M_ITW                                                        '荷主コード（中）
        drEdiRcvDtl("PRTFLG") = "0"                                                                     'プリントフラグ

        drEdiRcvDtl("ORDER_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_1").ToString().Trim(), 30)             '受注No
        drEdiRcvDtl("DEST_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_2").ToString().Trim(), 15)              'お届先コード
        drEdiRcvDtl("DEST_NM1") = Me._Blc.LeftB(drSetDtl("COLUMN_3").ToString().Trim(), 80)             'お届先名称1
        drEdiRcvDtl("DEST_NM2") = Me._Blc.LeftB(drSetDtl("COLUMN_4").ToString().Trim(), 60)             'お届先名称2
        drEdiRcvDtl("DEST_ZIP") = Me._Blc.LeftB(drSetDtl("COLUMN_5").ToString().Trim(), 10)             'お届先郵便番号
        drEdiRcvDtl("DEST_AD_1") = Me._Blc.LeftB(drSetDtl("COLUMN_6").ToString().Trim(), 40)            'お届先住所1
        drEdiRcvDtl("DEST_AD_2") = Me._Blc.LeftB(drSetDtl("COLUMN_7").ToString().Trim(), 40)            'お届先住所2
        drEdiRcvDtl("DEST_TEL") = Me._Blc.LeftB(drSetDtl("COLUMN_8").ToString().Trim(), 20)             'お客様電話番号
        drEdiRcvDtl("DEST_FAX") = Me._Blc.LeftB(drSetDtl("COLUMN_9").ToString().Trim(), 15)             'お客様FAX番号
        drEdiRcvDtl("CUST_ORD_NO_DTL") = Me._Blc.LeftB(drSetDtl("COLUMN_10").ToString().Trim(), 30)     '得意先注文No
        drEdiRcvDtl("SALES_PERSON") = Me._Blc.LeftB(drSetDtl("COLUMN_11").ToString().Trim(), 80)        '営業担当者
        drEdiRcvDtl("SLIP_INPUT_PERSON") = Me._Blc.LeftB(drSetDtl("COLUMN_12").ToString().Trim(), 80)   '伝票入力者
        drEdiRcvDtl("ORDER_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_13").ToString().Trim(), 8)           '受注日
        drEdiRcvDtl("OUTKA_PLAN_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_14").ToString().Trim(), 8)      '出荷指示日
        drEdiRcvDtl("ARR_PLAN_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_15").ToString().Trim(), 8)        '受注納期
        drEdiRcvDtl("ARR_PLAN_TIME") = Me._Blc.LeftB(drSetDtl("COLUMN_16").ToString().Trim(), 5)        '受注納期時刻
        drEdiRcvDtl("CUST_GOODS_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_17").ToString().Trim(), 20)       '商品コード
        drEdiRcvDtl("GOODS_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_18").ToString().Trim(), 60)            '商品名
        drEdiRcvDtl("GOODS_CAPACITY") = Me._Blc.LeftB(drSetDtl("COLUMN_19").ToString().Trim(), 20)      '商品容量
        drEdiRcvDtl("JAN_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_20").ToString().Trim(), 13)              'JANコード
        drEdiRcvDtl("LOT_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_21").ToString().Trim(), 40)              'ロットNo
        drEdiRcvDtl("OUTKA_TTL_NB") = Me._Blc.LeftB(drSetDtl("COLUMN_22").ToString().Replace(",", "").Trim(), 10)        '出荷指示数(カンマ区切りのためカンマ削除)
        drEdiRcvDtl("TRAN_UNIT") = Me._Blc.LeftB(drSetDtl("COLUMN_23").ToString().Trim(), 10)           '取引単位
        drEdiRcvDtl("CUST_ORD_NO_DTL2") = Me._Blc.LeftB(drSetDtl("COLUMN_24").ToString().Trim(), 4)     '得意先注文番号
#If True Then       'UPD 2018/09/28 依頼番号 : 002437   【LMS】千葉ITW_セミEDIの追加改修(PS吉房)◎担当；阿達→玉野・大極Team◎*T4-差込09* 
        '３．出荷時注意事項に、CSVファイルJ・X・Y列を半角スペース区切りで連結して登録。
        drEdiRcvDtl("REMARK") = Me._Blc.LeftB(String.Concat(drSetDtl("COLUMN_10").ToString(), " ", drSetDtl("COLUMN_24").ToString().Trim(), " ", drSetDtl("COLUMN_25").ToString().Trim()), 100)             '行備考

#Else
        drEdiRcvDtl("REMARK") = Me._Blc.LeftB(drSetDtl("COLUMN_25").ToString().Trim(), 100)             '行備考

#End If
        drEdiRcvDtl("JISSEKI_SHORI_FLG") = "1"                                                          '実績処理フラグ

        'データセットに設定
        'ds.Tables("LMH030_OUTKAEDI_HED_KTK").Rows.Add(drEdiRcvHed)
        ds.Tables("LMH030_H_OUTKAEDI_DTL_ITW").Rows.Add(drEdiRcvDtl)

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
                                     , ByVal mGoodsCnt As Integer
                                    ) As DataSet


        Dim drOutkaEdiM As DataRow = setDs.Tables("LMH030_OUTKAEDI_M").NewRow()
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_H_OUTKAEDI_DTL_ITW").Rows(0)
        'Dim drRcvEdiHed As DataRow = setDs.Tables("LMH030_OUTKAEDI_HED_KTK").Rows(0)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        Dim mGoodsDt As DataTable = setDs.Tables("LMH030_M_GOODS")

        drOutkaEdiM("DEL_KB") = "0"
        drOutkaEdiM("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drOutkaEdiM("EDI_CTL_NO") = drRcvEdiDtl("EDI_CTL_NO")
        drOutkaEdiM("EDI_CTL_NO_CHU") = drRcvEdiDtl("EDI_CTL_NO_CHU")
        drOutkaEdiM("OUTKA_CTL_NO") = String.Empty
        drOutkaEdiM("OUTKA_CTL_NO_CHU") = String.Empty
        drOutkaEdiM("COA_YN") = String.Empty
        '2018.10.22 要望管理002632 drRcvEdiDtl("ORDER_NO")の値を設定しないよう修正
        drOutkaEdiM("CUST_ORD_NO_DTL") = String.Empty
        drOutkaEdiM("BUYER_ORD_NO_DTL") = drRcvEdiDtl("CUST_ORD_NO_DTL")
        drOutkaEdiM("CUST_GOODS_CD") = drRcvEdiDtl("CUST_GOODS_CD")
        '2018/03/01 セミEDI_千葉ITW_新規登録 商品マスタ未確定時対応 annen upd start
        Select Case mGoodsCnt
            Case 0
                drOutkaEdiM("NRS_GOODS_CD") = String.Empty
                drOutkaEdiM("GOODS_NM") = String.Empty
                drOutkaEdiM("IRIME") = 0
                drOutkaEdiM("ALCTD_KB") = String.Empty
                drOutkaEdiM("PKG_NB") = 0
                drOutkaEdiM("IRIME_UT") = String.Empty
            Case 1
                '2018/03/26 セミEDI_千葉ITW_新規登録 商品マスタ未確定時対応 annen upd start
                'drOutkaEdiM("NRS_GOODS_CD") = String.Empty
                'drOutkaEdiM("GOODS_NM") = String.Empty
                drOutkaEdiM("NRS_GOODS_CD") = mGoodsDt.Rows(0).Item("GOODS_CD_NRS")
                drOutkaEdiM("GOODS_NM") = mGoodsDt.Rows(0).Item("GOODS_NM_1")
                '2018/03/26 セミEDI_千葉ITW_新規登録 商品マスタ未確定時対応 annen upd end
                drOutkaEdiM("IRIME") = mGoodsDt.Rows(0).Item("STD_IRIME_NB")
                drOutkaEdiM("ALCTD_KB") = mGoodsDt.Rows(0).Item("ALCTD_KB")
                drOutkaEdiM("PKG_NB") = 0
                drOutkaEdiM("IRIME_UT") = mGoodsDt.Rows(0).Item("STD_IRIME_UT")
            Case Else
                drOutkaEdiM("NRS_GOODS_CD") = String.Empty
                drOutkaEdiM("GOODS_NM") = String.Empty
                '入目の単一確認
                '商品マスタの該当レコードが、全て同じ入目であればその値を設定する。
                'そうでなければ入目に0を設定する。
                For j As Integer = 1 To mGoodsCnt - 1
                    If (mGoodsDt.Rows(j).Item("STD_IRIME_NB").ToString()).Equals _
                       (mGoodsDt.Rows(j - 1).Item("STD_IRIME_NB").ToString()) = False Then
                        drOutkaEdiM("IRIME") = 0
                        Exit For
                    Else
                        drOutkaEdiM("IRIME") = mGoodsDt.Rows(j).Item("STD_IRIME_NB")
                    End If
                Next
                drOutkaEdiM("ALCTD_KB") = String.Empty
                drOutkaEdiM("PKG_NB") = 0
                drOutkaEdiM("IRIME_UT") = String.Empty
        End Select
        'drOutkaEdiM("NRS_GOODS_CD") = mGoodsDt.Rows(0).Item("GOODS_CD_NRS")
        'drOutkaEdiM("GOODS_NM") = mGoodsDt.Rows(0).Item("GOODS_NM_1")
        'drOutkaEdiM("IRIME") = mGoodsDt.Rows(0).Item("STD_IRIME_NB")
        '2018/03/01 セミEDI_千葉ITW_新規登録 商品マスタ未確定時対応 annen upd end

        'If mGoodsCnt = 0 Then
        '    '商品マスタ取得０件
        '    drOutkaEdiM("NRS_GOODS_CD") = String.Empty
        '    drOutkaEdiM("GOODS_NM") = String.Empty
        '    drOutkaEdiM("IRIME") = 0
        'ElseIf mGoodsCnt = 1 Then
        '    '商品マスタ取得１件
        '    drOutkaEdiM("NRS_GOODS_CD") = mGoodsDt.Rows(0).Item("GOODS_CD_NRS")
        '    drOutkaEdiM("GOODS_NM") = mGoodsDt.Rows(0).Item("GOODS_NM_1")
        '    drOutkaEdiM("IRIME") = mGoodsDt.Rows(0).Item("STD_IRIME_NB")
        'ElseIf mGoodsCnt > 1 Then
        '    '商品マスタ取得１件以上
        '    drOutkaEdiM("NRS_GOODS_CD") = String.Empty
        '    drOutkaEdiM("GOODS_NM") = String.Empty

        '    '入目の単一確認
        '    For j As Integer = 1 To mGoodsCnt   'Rows(1)から開始
        '        If (mGoodsDt.Rows(j).Item("STD_IRIME_NB").ToString()).Equals _
        '           (mGoodsDt.Rows(j - 1).Item("STD_IRIME_NB").ToString()) = False Then
        '            drOutkaEdiM("IRIME") = 0
        '            Exit For
        '        Else
        '            drOutkaEdiM("IRIME") = mGoodsDt.Rows(j).Item("STD_IRIME_NB")
        '        End If
        '    Next
        'End If

        drOutkaEdiM("RSV_NO") = String.Empty
        drOutkaEdiM("LOT_NO") = drRcvEdiDtl("LOT_NO")
        drOutkaEdiM("SERIAL_NO") = String.Empty
        '2018/03/01 セミEDI_千葉ITW_新規登録 商品マスタ未確定時対応 annen del start
        'drOutkaEdiM("ALCTD_KB") = mGoodsDt.Rows(0).Item("ALCTD_KB")
        '2018/03/01 セミEDI_千葉ITW_新規登録 商品マスタ未確定時対応 annen del start

        drOutkaEdiM("OUTKA_PKG_NB") = 0             '出荷包装個数

        Dim iSuryou As Integer = Convert.ToInt32(drRcvEdiDtl("OUTKA_TTL_NB").ToString)
        drOutkaEdiM("OUTKA_HASU") = iSuryou         '出荷端数
        drOutkaEdiM("OUTKA_QT") = 0                 '出荷数量
        drOutkaEdiM("OUTKA_TTL_NB") = iSuryou       '出荷総個数
        drOutkaEdiM("OUTKA_TTL_QT") = 0             '出荷送数量

        ''荷姿から入目、入目単位を求める
        'Dim sNISUGATA As String = drRcvEdiDtl("NISUGATA").ToString
        ''Dim sIrime As String = String.Empty
        'Dim sIrimeUt As String = String.Empty

        'If sNISUGATA = "" Then    '指定なし
        '    sIrime = "0"
        '    sIrimeUt = ""
        'ElseIf IsNumeric(sNISUGATA) = True Then  '数値のみ
        '    sIrime = sNISUGATA
        '    sIrimeUt = "NO"
        'Else
        '    sIrime = Left(sNISUGATA, Len(sNISUGATA) - 1)
        '    sIrimeUt = Right(sNISUGATA, 1)
        '    If IsNumeric(sIrime) = True Then '単位は１桁
        '    Else
        '        sIrime = Left(sNISUGATA, Len(sNISUGATA) - 2)
        '        sIrimeUt = Right(sNISUGATA, 2)
        '        If IsNumeric(sIrime) = True Then '単位は２桁
        '        Else  '数値なし
        '            sIrime = "0"
        '            sIrimeUt = sNISUGATA
        '        End If
        '    End If
        'End If

        'Dim dTtlQt As Double = 0
        'dTtlQt = Convert.ToDouble(sIrime) * iSuryou
        'drOutkaEdiM("OUTKA_TTL_QT") = dTtlQt    '出荷総数量

        ''数量単位
        'Select Case drRcvEdiDtl("TAN_I").ToString
        '    Case "個"
        '        drOutkaEdiM("KB_UT") = "KE"
        '    Case "缶"
        '        drOutkaEdiM("KB_UT") = "CC"
        '    Case "函"
        '        drOutkaEdiM("KB_UT") = "BX"
        '    Case "枚"
        '        drOutkaEdiM("KB_UT") = "SH"
        '    Case "DM"
        '        drOutkaEdiM("KB_UT") = "DR"
        '    Case Else
        '        drOutkaEdiM("KB_UT") = String.Empty
        'End Select

        drOutkaEdiM("KB_UT") = String.Empty
        drOutkaEdiM("QT_UT") = String.Empty
        '2018/03/01 セミEDI_千葉ITW_新規登録 商品マスタ未確定時対応 annen del start
        'drOutkaEdiM("PKG_NB") = mGoodsDt.Rows(0).Item("PKG_NB")
        '2018/03/01 セミEDI_千葉ITW_新規登録 商品マスタ未確定時対応 annen del end
        drOutkaEdiM("PKG_UT") = drOutkaEdiM("KB_UT")    '個数単位と同じ
        drOutkaEdiM("ONDO_KB") = String.Empty
        drOutkaEdiM("UNSO_ONDO_KB") = String.Empty

        'drOutkaEdiM("IRIME") = sIrime                   '求めた入目

        ''入目単位 
        'Select Case sIrimeUt
        '    Case "K", "KG"
        '        drOutkaEdiM("IRIME_UT") = "KG"
        '    Case "G"
        '        drOutkaEdiM("IRIME_UT") = "G"
        '    Case "L"
        '        drOutkaEdiM("IRIME_UT") = "L"
        '    Case "ML"
        '        drOutkaEdiM("IRIME_UT") = "ML"
        '    Case ""
        '        drOutkaEdiM("IRIME_UT") = ""
        '    Case Else
        '        drOutkaEdiM("IRIME_UT") = "NO"
        'End Select
        '2018/03/01 セミEDI_千葉ITW_新規登録 商品マスタ未確定時対応 annen del start
        'drOutkaEdiM("IRIME_UT") = mGoodsDt.Rows(0).Item("STD_IRIME_UT")
        '2018/03/01 セミEDI_千葉ITW_新規登録 商品マスタ未確定時対応 annen del end

        '個別重量
        drOutkaEdiM("BETU_WT") = 0

        '注意事項 
        drOutkaEdiM("REMARK") = drRcvEdiDtl("REMARK")


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

        drOutkaEdiM("FREE_C01") = drRcvEdiDtl("GOODS_CAPACITY")
        drOutkaEdiM("FREE_C02") = drRcvEdiDtl("JAN_CD")
        drOutkaEdiM("FREE_C03") = drRcvEdiDtl("TRAN_UNIT")
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
    Private Function SetSemiOutkaEdiL(ByVal setDs As DataSet _
                                    , ByVal sWhCd As String _
                                    , ByVal sCustCdL As String _
                                    , ByVal sCustCdM As String _
                                    , ByVal sNrsGoodsCd As String _
                                    , ByVal sNrsGoodsNm As String _
                                    , ByVal sIrime As String
                                    ) As DataSet

        Dim drOutkaEdiL As DataRow = setDs.Tables("LMH030_OUTKAEDI_L").NewRow()
        'Dim drRcvEdiHed As DataRow = setDs.Tables("LMH030_OUTKAEDI_HED_KTK").Rows(0)
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_H_OUTKAEDI_DTL_ITW").Rows(0)
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
        drOutkaEdiL("WH_CD") = WH_CD_ITW
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

        '納入予定時刻
#If True Then       'UPD 2018/09/28 依頼番号 : 002437   【LMS】千葉ITW_セミEDIの追加改修(PS吉房)◎担当；阿達→玉野・大極Team◎*T4-差込09* 
        drOutkaEdiL("ARR_PLAN_TIME") = "00"
#Else
        Dim arrPlanTime As String = drRcvEdiDtl("ARR_PLAN_TIME").ToString
        If String.IsNullOrEmpty(arrPlanTime) Then
            drOutkaEdiL("ARR_PLAN_TIME") = "00"
        Else
            Select Case Strings.Right(arrPlanTime, 1)
                Case "0"
                    '受注納期時刻の最後の1文字が0なら必着
                    drOutkaEdiL("ARR_PLAN_TIME") = "05"
                Case "1"
                    '受注納期時刻の最後の1文字が1なら午前中指定
                    drOutkaEdiL("ARR_PLAN_TIME") = "06"
                Case "2"
                    '受注納期時刻の最後の1文字が2なら午後指定
                    drOutkaEdiL("ARR_PLAN_TIME") = "07"
            End Select
        End If

#End If

        drOutkaEdiL("HOKOKU_DATE") = String.Empty

        '当期保管料負担有無
        drOutkaEdiL("TOUKI_HOKAN_YN") = String.Empty
        drOutkaEdiL("CUST_CD_L") = drRcvEdiDtl("CUST_CD_L")
        drOutkaEdiL("CUST_CD_M") = drRcvEdiDtl("CUST_CD_M")
        drOutkaEdiL("CUST_NM_L") = String.Empty
        drOutkaEdiL("CUST_NM_M") = String.Empty
        drOutkaEdiL("SHIP_CD_L") = String.Empty
        drOutkaEdiL("SHIP_CD_M") = String.Empty
        drOutkaEdiL("SHIP_NM_L") = String.Empty
        drOutkaEdiL("SHIP_NM_M") = String.Empty
        'rep 2019/11/13 要望管理009103 start
        'drOutkaEdiL("EDI_DEST_CD") = drRcvEdiDtl("DEST_CD")
        drOutkaEdiL("EDI_DEST_CD") = drRcvEdiDtl("DEST_TEL")
        'rep 2019/11/13 要望管理009103 end
        drOutkaEdiL("DEST_CD") = drOutkaEdiL("EDI_DEST_CD")
        drOutkaEdiL("DEST_NM") = drRcvEdiDtl("DEST_NM1")
        drOutkaEdiL("DEST_ZIP") = drRcvEdiDtl("DEST_ZIP").ToString.Replace("-", "")
        drOutkaEdiL("DEST_AD_1") = drRcvEdiDtl("DEST_AD_1")
        drOutkaEdiL("DEST_AD_2") = drRcvEdiDtl("DEST_AD_2")
        drOutkaEdiL("DEST_AD_3") = drRcvEdiDtl("DEST_NM2")
        drOutkaEdiL("DEST_AD_4") = String.Empty
        drOutkaEdiL("DEST_AD_5") = String.Empty
        drOutkaEdiL("DEST_TEL") = drRcvEdiDtl("DEST_TEL")
        drOutkaEdiL("DEST_FAX") = drRcvEdiDtl("DEST_FAX")
        drOutkaEdiL("DEST_MAIL") = String.Empty
        drOutkaEdiL("DEST_JIS_CD") = String.Empty
        drOutkaEdiL("SP_NHS_KB") = String.Empty
        drOutkaEdiL("COA_YN") = String.Empty
        drOutkaEdiL("CUST_ORD_NO") = drRcvEdiDtl("ORDER_NO")
        drOutkaEdiL("BUYER_ORD_NO") = drRcvEdiDtl("CUST_ORD_NO_DTL")

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

        ''注意事項

        drOutkaEdiL("REMARK") = drRcvEdiDtl("REMARK")
        drOutkaEdiL("UNSO_ATT") = String.Empty
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

        drOutkaEdiL("FREE_C01") = drRcvEdiDtl("SALES_PERSON")
        drOutkaEdiL("FREE_C02") = drRcvEdiDtl("SLIP_INPUT_PERSON")
        drOutkaEdiL("FREE_C03") = drRcvEdiDtl("ORDER_DATE")
        'rep 2019/11/13 要望管理009103 start
        'drOutkaEdiL("FREE_C04") = String.Empty
        drOutkaEdiL("FREE_C04") = drRcvEdiDtl("DEST_CD")
        'rep 2019/11/13 要望管理009103 end
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
        drOutkaEdiL("FREE_C30") = String.Empty

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_L").Rows.Add(drOutkaEdiL)
        Return setDs


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
                             , ByRef sCustCdL As String _
                             , ByRef sCustCdM As String _
                             , ByRef sNrsGoodsCd As String _
                             , ByRef sNrsGoodsNm As String _
                             , ByRef sIrime As String
                              ) As Integer

        Dim dtMstGoods As DataTable = ds.Tables("LMH030_M_GOODS")
        Dim iMstGoodsCnt As Integer = dtMstGoods.Rows.Count


        Select Case iMstGoodsCnt

            Case 0      '商品マスタ取得０件
                '荷主は関塗工とする
                sCustCdL = CUST_CD_L_ITW
                sCustCdM = CUST_CD_M_ITW
                sNrsGoodsCd = String.Empty
                sNrsGoodsNm = String.Empty
                sIrime = "0"

            Case 1      '商品マスタ取得１件
                '荷主は商品マスタから取得する
                sCustCdL = dtMstGoods.Rows(0).Item("CUST_CD_L").ToString
                sCustCdM = dtMstGoods.Rows(0).Item("CUST_CD_M").ToString
                sNrsGoodsCd = dtMstGoods.Rows(0).Item("GOODS_CD_NRS").ToString
                sNrsGoodsNm = dtMstGoods.Rows(0).Item("GOODS_NM_1").ToString
                sIrime = dtMstGoods.Rows(0).Item("STD_IRIME_NB").ToString

            Case Else   '商品マスタ取得２件以上

                '荷主の単一確認
                For i As Integer = 1 To iMstGoodsCnt - 1  'Rows(1)から開始'■要望番号:1612（セミEDI 荷主商品コード重複チェックでアベンド) 2012/12/14 本明修正　（iMstGoodsCnt→iMstGoodsCnt-1に修正）
                    If (dtMstGoods.Rows(i).Item("CUST_CD_L").ToString()).Equals(dtMstGoods.Rows(i - 1).Item("CUST_CD_L").ToString()) _
                    AndAlso (dtMstGoods.Rows(i).Item("CUST_CD_M").ToString()).Equals(dtMstGoods.Rows(i - 1).Item("CUST_CD_M").ToString()) Then
                        '等しい場合はセットする
                        sCustCdL = dtMstGoods.Rows(i).Item("CUST_CD_L").ToString
                        sCustCdM = dtMstGoods.Rows(i).Item("CUST_CD_M").ToString
                    Else
                        '等しくない場合は既定値をセットして抜ける
                        '荷主は関塗工とする
                        sCustCdL = CUST_CD_L_ITW
                        sCustCdM = CUST_CD_M_ITW
                        Exit For
                    End If
                Next

                sNrsGoodsCd = String.Empty
                sNrsGoodsNm = String.Empty

                '入目の単一確認
                For i As Integer = 1 To iMstGoodsCnt - 1  'Rows(1)から開始'■要望番号:1612（セミEDI 荷主商品コード重複チェックでアベンド) 2012/12/14 本明修正　（iMstGoodsCnt→iMstGoodsCnt-1に修正）
                    If (dtMstGoods.Rows(i).Item("STD_IRIME_NB").ToString()).Equals _
                       (dtMstGoods.Rows(i - 1).Item("STD_IRIME_NB").ToString()) Then
                        '等しい場合はセットする
                        sIrime = dtMstGoods.Rows(i).Item("STD_IRIME_NB").ToString
                    Else
                        '等しくない場合は既定値をセットして抜ける
                        sIrime = "0"
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
                                , ByRef sEdiCtlNo As String, ByRef iEdiCtlNoChu As Integer) As DataSet

        'Dim dtRcvEdiHed As DataTable = ds.Tables("LMH030_OUTKAEDI_HED_KTK")
        'Dim drRcvEdiHed As DataRow = ds.Tables("LMH030_OUTKAEDI_HED_KTK").Rows(0)
        Dim dtRcvEdiDtl As DataTable = ds.Tables("LMH030_H_OUTKAEDI_DTL_ITW")
        Dim drRcvEdiDtl As DataRow = ds.Tables("LMH030_H_OUTKAEDI_DTL_ITW").Rows(0)
        Dim sNrsBrCd As String = ds.Tables("LMH030_H_OUTKAEDI_DTL_ITW").Rows(0).Item("NRS_BR_CD").ToString()


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
            'dtRcvEdiHed.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo              'HEDにセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo              'DTLにセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO_CHU") = iEdiCtlNoChu.ToString("000")   'EDI_CHUにセット
            dtRcvEdiDtl.Rows(0).Item("GYO") = iEdiCtlNoChu.ToString("00")               '行数にもEDI_CHUと同じ値をセット
        Else
            'dtRcvEdiHed.Rows(0).Item("EDI_CTL_NO") = DEF_CTL_NO             'HEDに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO") = DEF_CTL_NO             'DTLに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("EDI_CTL_NO_CHU") = "000"              'EDI_CHUに固定値をセット
            dtRcvEdiDtl.Rows(0).Item("GYO") = iEdiCtlNoChu.ToString("00")   '行数にはカウントアップした値を入れる
        End If

        ''削除EDI管理番号にも設定する(削除フラグが１の場合のみ)
        'If iDeleteFlg = 1 Then
        '    Dim dtRcvHedDel As DataTable = ds.Tables("LMH030_HED_KTK_CANCELOUT")
        '    Dim drRcvHedDel As DataRow = ds.Tables("LMH030_DTL_KTK_CANCELOUT").Rows(0)

        '    Dim dtRcvDtlDel As DataTable = ds.Tables("LMH030_DTL_KTK_CANCELOUT")
        '    Dim drRcvDtlDel As DataRow = ds.Tables("LMH030_DTL_KTK_CANCELOUT").Rows(0)
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

        '受注No
        Dim targetStr As String = dr.Item("COLUMN_1").ToString
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("受注No(カラム1番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 30, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("受注No(カラム1番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'お届先コード
        targetStr = dr.Item("COLUMN_2").ToString
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("お届先コード(カラム2番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 15, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("お届先コード(カラム2番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'お届先名称１
        targetStr = dr.Item("COLUMN_3").ToString
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("お届先名称１(カラム3番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '文字列長チェック
        ElseIf targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("お届先名称１(カラム3番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'お届先名称２
        targetStr = dr.Item("COLUMN_4").ToString
        '文字列長チェック
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("お届先名称２(カラム4番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'お届先郵便番号
        targetStr = dr.Item("COLUMN_5").ToString
        '文字列長チェック
        If IsHalfSize(targetStr, 10, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("お届先郵便番号(カラム5番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'お届先住所１
        targetStr = dr.Item("COLUMN_6").ToString
        '文字列長チェック
        If targetStr.Length > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("お届先住所１(カラム6番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'お届先住所２
        targetStr = dr.Item("COLUMN_7").ToString
        '文字列長チェック
        If targetStr.Length > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("お届先住所２(カラム7番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'お届先電話番号
        targetStr = dr.Item("COLUMN_8").ToString
        '文字列長チェック
        If IsHalfSize(targetStr, 20, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("お届先電話番号(カラム8番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'お届先FAX番号
        targetStr = dr.Item("COLUMN_9").ToString
        '文字列長チェック
        If IsHalfSize(targetStr, 15, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("お届先FAX番号(カラム9番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '得意先注文No
#If False Then          'UPD 2018/10/15 依頼番号 : 002597   【LMS】ITWセミEDI_差戻_CSVデータ大文字でも取り込めるように変更
        targetStr = dr.Item("COLUMN_10").ToString
        '文字列長チェック
        If IsHalfSize(targetStr, 30, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("得意先注文No(カラム10番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If
#Else
        targetStr = dr.Item("COLUMN_10").ToString
        '文字列長チェック
        If targetStr.Length > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("得意先注文No(カラム10番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If
#End If

        '営業担当者
        targetStr = dr.Item("COLUMN_11").ToString
        '文字列長チェック
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("営業担当者(カラム11番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '伝票入力者
        targetStr = dr.Item("COLUMN_12").ToString
        '文字列長チェック
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("伝票入力者(カラム12番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '受注日（カラム13番目）
        Dim sDate As String = dr.Item("COLUMN_13").ToString()
        '年月日チェック
        If IsConvertDate(sDate) Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("受注日(カラム13番目)[", sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '出荷指示日（カラム14番目）
        sDate = dr.Item("COLUMN_14").ToString()
        '年月日チェック
        If IsConvertDate(sDate) Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("出荷指示日(カラム14番目)[", sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '受付納期（カラム15番目）
#If True Then       'UPD 2018/10/02 依頼番号 : 002437   【LMS】千葉ITW_セミEDIの追加改修
        If String.IsNullOrEmpty(dr.Item("COLUMN_15").ToString()) = False Then
            sDate = dr.Item("COLUMN_15").ToString()
            '年月日チェック
            If IsConvertDate(sDate) Then
            Else
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("受注納期(カラム15番目)[", sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If
#Else
        sDate = dr.Item("COLUMN_15").ToString()
        '年月日チェック
        If IsConvertDate(sDate) Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("受付納期(カラム15番目)[", sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

#End If

        '受注納期時刻（カラム16番目）
        Dim val As String = dr.Item("COLUMN_16").ToString()
#If True Then       'UPD 2018/10/02 依頼番号 : 002437   【LMS】千葉ITW_セミEDIの追加改修
        If String.Empty.Equals(val) = False Then
            '半角チェック
            If IsHalfSize(val).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E036", New String() {String.Concat("受注納期時刻(カラム16番目)[", val, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
                '桁数チェック
            ElseIf val.Length <> 4 AndAlso val.Length <> 5 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("受注納期時刻(カラム16番目)[", val, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            Else
                '頭に0を付けて右から5桁取得することで、頭の月日部分をMDDからMMDDに揃える
                Dim val2 As String = Strings.Right(String.Concat("0", val), 5)
                'MMDDの値を取得する
                sDate = Strings.Left(val2, 4)
                '頭4文字が月日として使用できなければエラー
                If IsConvertDate2(sDate).Equals(False) Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("受注納期時刻(カラム16番目)[", val, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                Else
                    '最後尾1文字を取得
                    sDate = Strings.Right(val2, 1)
                    '最後尾1文字が"0"、"1"、"2"でなければエラー
                    If Not (sDate.Equals("0") OrElse sDate.Equals("1") OrElse sDate.Equals("2")) Then
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("受注納期時刻(カラム16番目)[", val, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                End If
            End If

        End If
#Else


#End If

        '商品コード
        targetStr = dr.Item("COLUMN_17").ToString
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("商品コード(カラム17番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 20, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("商品コード(カラム17番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '商品名
        targetStr = dr.Item("COLUMN_18").ToString
        '文字列長チェック
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("商品名(カラム18番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '商品容量
        targetStr = dr.Item("COLUMN_19").ToString
        '文字列長チェック
        If targetStr.Length > 20 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("商品容量(カラム19番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'JANコード
        targetStr = dr.Item("COLUMN_20").ToString
        '文字列長チェック
        If targetStr.Length > 13 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("JANコード(カラム20番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'ロットNo
        targetStr = dr.Item("COLUMN_21").ToString
        '文字列長チェック
        If targetStr.Length > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("ロットNo(カラム21番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If


        '出荷指示数
        targetStr = dr.Item("COLUMN_22").ToString()
        Dim d As Double
        '数値チェック
        If Double.TryParse(targetStr.Replace(",", ""), d) Then
            '桁数チェック
            If IsHalfSize(targetStr.Replace(",", ""), 10, False).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("出荷指示数(カラム22番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat("出荷指示数(カラム22番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '取引単位
        targetStr = dr.Item("COLUMN_23").ToString
        '文字列長チェック
        If targetStr.Length > 10 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("取引単位(カラム23番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If


        '得意先注文番号
#If False Then          'UPD 2018/10/15 依頼番号 : 002597   【LMS】ITWセミEDI_差戻_CSVデータ大文字でも取り込めるように変更
        targetStr = dr.Item("COLUMN_24").ToString
        '文字列長チェック
        If IsHalfSize(targetStr, 30, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("得意先注文番号(カラム24番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

#Else
        targetStr = dr.Item("COLUMN_24").ToString
        '文字列長チェック
        If targetStr.Length > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("得意先注文番号(カラム24番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If
#End If

        '行備考
#If False Then          'UPD 2018/10/15 依頼番号 : 002597   【LMS】ITWセミEDI_差戻_CSVデータ大文字でも取り込めるように変更
        targetStr = dr.Item("COLUMN_25").ToString
        '文字列長チェック
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("行備考(カラム25番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

#Else
        targetStr = dr.Item("COLUMN_25").ToString
        '文字列長チェック
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("行備考(カラム25番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If
#End If

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

        Dim dtRcvDtl As DataTable = ds.Tables("LMH030_H_OUTKAEDI_DTL_ITW")         'EDI受信Dtl

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


        For i As Integer = 0 To iSetDtlMax

            '---------------------------------------------------------------------------
            ' セミEDI取込(共通)⇒受信HED/DTLへのデータセット
            '---------------------------------------------------------------------------
            ds.Tables("LMH030_H_OUTKAEDI_DTL_ITW").Clear() '受信DTLをクリア
            ds = Me.SetSemiOutkaEdiRcv(ds, i)

            'Dim drEdiRcvHed As DataRow = ds.Tables("LMH030_OUTKAEDI_HED_KTK").Rows(0)
            Dim drEdiRcvDtl As DataRow = ds.Tables("LMH030_H_OUTKAEDI_DTL_ITW").Rows(0)


            '---------------------------------------------------------------------------
            ' 倉庫/仕入先コードから荷主コード、商品コードを設定
            '---------------------------------------------------------------------------
            sWhcd = WH_CD_ITW
            sCustCdL = CUST_CD_L_ITW
            sCustCdM = CUST_CD_M_ITW
            sNrsGoodsCd = NRS_GOODS_CD_UNSO
            sNrsGoodsNm = String.Empty
            sIrime = IRIME_UNSO

            ''Select Case drEdiRcvHed.Item("SOUKO_CD").ToString
            ''    Case SOUKO_CD_TAITAI     '運送指示データの場合
            ''        '大泰化工扱いとする
            ''        sWhcd = WH_CD_TAITAI
            ''        sCustCdL = CUST_CD_L_TAITAI
            ''        sCustCdM = CUST_CD_M_TAITAI
            ''        sNrsGoodsCd = NRS_GOODS_CD_UNSO
            ''        sNrsGoodsNm = String.Empty
            ''        sIrime = "0"

            ''    Case Else

            ''        '関塗工またはアイカ工業
            ''        sWhcd = WH_CD_KTK

            ''        '商品マスタ情報を取得する
            ''        ds = MyBase.CallDAC(Me._Dac, "SelectMstGoods", ds)

            ''        '取得した商品マスタから荷主コード等を決定する
            ''        Call Me.GetCustCd(ds, sCustCdL, sCustCdM, sNrsGoodsCd, sNrsGoodsNm, sIrime)
            ''End Select

            ' ''決定した荷主コードをdrEdiRcvHed、drEdiRcvDtlにセットする
            ''drEdiRcvHed.Item("CUST_CD_L") = sCustCdL
            ''drEdiRcvHed.Item("CUST_CD_M") = sCustCdM
            ''drEdiRcvDtl.Item("CUST_CD_L") = sCustCdL
            ''drEdiRcvDtl.Item("CUST_CD_M") = sCustCdM

            '---------------------------------------------------------------------------
            ' 取消区分名を元に赤黒フラグ、スキップフラグを設定
            '---------------------------------------------------------------------------
            ''Select Case drEdiRcvHed.Item("TORIKESHI_KB_NM").ToString

            ''    Case "売上済"
            ''        If Convert.ToInt32(drEdiRcvDtl.Item("QT")) >= 0 Then
            ''            iAkakuroFlg = 0
            ''        Else
            ''            iAkakuroFlg = 1
            ''        End If

            ''        iSkipFlg = 1

            ''    Case "取消伝票"
            ''        If Convert.ToInt32(drEdiRcvDtl.Item("QT")) >= 0 Then
            ''            iAkakuroFlg = 0
            ''        Else
            ''            iAkakuroFlg = 1
            ''        End If

            ''        iSkipFlg = 0

            ''    Case Else
            ''        iAkakuroFlg = 0
            ''        iSkipFlg = 0
            ''End Select

            ' ''決定した赤黒フラグをdrEdiRcvHedにセットする
            ''drEdiRcvHed.Item("CANCEL_FLG") = iAkakuroFlg.ToString

            '---------------------------------------------------------------------------
            ' 発行区分名を元に発行フラグを設定
            '---------------------------------------------------------------------------
            ''Select Case drEdiRcvHed.Item("HAKKO_KB_NM").ToString
            ''    Case "再発行"
            ''        iHakkoFlg = 1
            ''    Case Else
            ''        iHakkoFlg = 0
            ''End Select

            '---------------------------------------------------------------------------
            ' キー項目設定
            '---------------------------------------------------------------------------
            sNewKey = drEdiRcvDtl.Item("ORDER_NO").ToString

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
            ' スキップフラグが０かつ前行同一フラグがfalseの場合
            ' 受注伝票番号を元に取消データの確認処理を行う
            '---------------------------------------------------------------------------
            ''If iSkipFlg = 0 AndAlso bSameKeyFlg = False Then

            ''    '取消フラグ,キャンセルフラグを0にする(初期値)
            ''    iDeleteFlg = 0
            ''    iCancelFlg = 0

            ''    '受信DTL取消データ取得処理
            ''    ds.Tables("LMH030_HED_KTK_CANCELOUT").Clear()    '取得用DSをクリア
            ''    ds = MyBase.CallDAC(Me._Dac, "SelectOutkaEdiRcvCancel", ds)

            ''    'データ取得できた場合
            ''    If MyBase.GetResultCount > 0 Then
            ''        '※直近のレコードで判断（SQL内でDESCされているので１件目のレコード）
            ''        Dim drRcvHedCancel As DataRow = ds.Tables("LMH030_HED_KTK_CANCELOUT").Rows(0)
            ''        '取得したデータのキャンセルフラグを設定
            ''        Dim sRcvHedCancelFlg As String = drRcvHedCancel.Item("CANCEL_FLG").ToString
            ''        '発行フラグ、取得データのキャンセルフラグ、赤黒フラグを元に継続処理判断
            ''        Dim sKeyFlg As String = String.Concat(iHakkoFlg.ToString, sRcvHedCancelFlg, iAkakuroFlg.ToString)
            ''        Select Case sKeyFlg
            ''            Case "000", "011"   '重複受信エラー
            ''                If Left(sNewKey, 32) = Left(sOldKey, 32) Then
            ''                    'メッセージ出力　出荷受信データ伝票番号重複（アイカ工業分混在）
            ''                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E468", New String() {"（アイカ工業分混在）"}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, "")
            ''                Else
            ''                    'メッセージ出力　出荷受信データ伝票番号重複
            ''                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E468", New String() {""}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, "")
            ''                End If
            ''                'エラーフラグ設定してからループを出る
            ''                bNoErr = False
            ''                Exit For

            ''            Case "100", "111"    '再発行かつ既に同内容処理済み
            ''                'ファイル名が同一の場合
            ''                If drRcvHedCancel.Item("FILE_NAME").ToString.Equals(drEdiRcvHed.Item("FILE_NAME").ToString) Then
            ''                    'メッセージ出力　出荷受信データ伝票番号重複
            ''                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E468", New String() {""}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, "")
            ''                    'エラーフラグ設定してからループを出る
            ''                    bNoErr = False
            ''                    Exit For
            ''                Else
            ''                    'スキップフラグを１にして処理続行
            ''                    '※受信DTL,HEDは更新、EDI出荷L,Mは更新しない
            ''                    iSkipFlg = 1
            ''                End If

            ''            Case "001", "101"    '赤データのため削除処理 
            ''                iDeleteFlg = 1  '取消フラグを1にする

            ''                '取得したデータの出荷管理番号が(DEF_EDI_CTL_NO)(出荷未登録の場合)　
            ''                If Right((drRcvHedCancel.Item("OUTKA_CTL_NO").ToString()), 8).Equals("00000000") = True Then
            ''                    'EDI出荷（大・中）を削除するためキャンセルフラグを"1"にする
            ''                    iCancelFlg = 1
            ''                End If
            ''        End Select
            ''    End If
            ''End If

            '---------------------------------------------------------------------------
            ' EDI管理番号(大,中)の採番(キャンセルフラグが０の場合も関数内で判断
            '---------------------------------------------------------------------------
            ds = Me.GetEdiCtlNo(ds, iDeleteFlg, iCancelFlg, iSkipFlg, bSameKeyFlg, sEdiCtlNo, iEdiCtlNoChu)

            '---------------------------------------------------------------------------
            ' 取消フラグが"1"の場合、受信データの取消処理を行う
            '---------------------------------------------------------------------------
            ''If iDeleteFlg = 1 Then

            ''    '削除EDI管理番号に設定する
            ''    Dim drRcvHedCancel As DataRow = ds.Tables("LMH030_HED_KTK_CANCELOUT").Rows(0)
            ''    If String.IsNullOrEmpty(sEdiCtlNo) Then
            ''        drRcvHedCancel.Item("DELETE_EDI_NO") = DEF_CTL_NO
            ''        '※DELETE_EDI_NO_CHU項目が存在しないのでDELETE_USER項目にDELETE_EDI_NO_CHUをセットする
            ''        drRcvHedCancel.Item("DELETE_USER") = "000"
            ''    Else
            ''        drRcvHedCancel.Item("DELETE_EDI_NO") = sEdiCtlNo
            ''        '※DELETE_EDI_NO_CHU項目が存在しないのでDELETE_USER項目にDELETE_EDI_NO_CHUをセットする
            ''        drRcvHedCancel.Item("DELETE_USER") = iEdiCtlNoChu.ToString("000")
            ''    End If

            ''    'EDI受信(DTL)の削除(論理削除)
            ''    ds = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaRcvDtl", ds)
            ''    iRcvDtlCanCnt = iRcvDtlCanCnt + 1

            ''    'EDI受信(HED)の削除(論理削除)
            ''    ds = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaRcvHed", ds)
            ''    iRcvHedCanCnt = iRcvHedCanCnt + 1

            ''    '---------------------------------------------------------------------------
            ''    ' キャンセルフラグが"1"の場合、EDI出荷データの削除更新を行う
            ''    '---------------------------------------------------------------------------
            ''    If iCancelFlg = 1 Then

            ''        'EDI出荷(大)の削除(論理削除)
            ''        ds = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiL", ds)
            ''        iOutHedCanCnt = iOutHedCanCnt + 1

            ''        'EDI出荷(中)の削除(論理削除)
            ''        ds = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiM", ds)
            ''        iOutDtlCanCnt = iOutDtlCanCnt + 1

            ''    End If

            ''End If


            '別インスタンス
            Dim setDs As DataSet = ds.Copy()

            'Dim setHedDt As DataTable = setDs.Tables("LMH030_OUTKAEDI_HED_KTK")
            Dim setDtlDt As DataTable = setDs.Tables("LMH030_H_OUTKAEDI_DTL_ITW")

            'setHedDt.Clear()
            setDtlDt.Clear()

            'setHedDt.ImportRow(dtRcvHed.Rows(0))
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

            setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiRcvDtl", setDs)
            iRcvDtlInsCnt = iRcvDtlInsCnt + 1

            ''If bSameKeyFlg = False Then
            ''    ' EDI受信データ(HED)の新規追加

            ''    'iSkipFlgを削除区分の値として使用する
            ''    If iSkipFlg = 0 Then
            ''        setHedDt.Rows(0).Item("DEL_KB") = "0"
            ''    Else
            ''        setHedDt.Rows(0).Item("DEL_KB") = "1"
            ''    End If

            ''    setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiRcvHed", setDs)
            ''    iRcvHedInsCnt = iRcvHedInsCnt + 1
            ''End If

            '---------------------------------------------------------------------------
            ' キャンセルフラグが0かつスキップフラグが0の場合、EDI出荷データの追加処理を行う
            '---------------------------------------------------------------------------
            If iCancelFlg = 0 AndAlso iSkipFlg = 0 Then

                '商品マスタ情報の取得
                setDs = MyBase.CallDAC(Me._Dac, "SelectMstGoods", setDs)
                Dim mGoodsCnt As Integer = MyBase.GetResultCount
#If False Then 'UPD 2018/12/13
               '2018/03/01 セミEDI_千葉ITW_新規登録 商品マスタ未確定時対応 annen del start
                If mGoodsCnt <= 0 Then
                    'メッセージ出力　商品マスタ情報が取得できない
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E609", New String() {"受注番号:" & setDs.Tables("LMH030_H_OUTKAEDI_DTL_ITW").Rows(0).Item("ORDER_NO").ToString}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, "")
                    'エラーフラグ設定してからループを出る
                    bNoErr = False
                    Exit For
                End If

                ''If mGoodsCnt >= 2 Then
                ''    'メッセージ出力　商品マスタ情報が複数取得できてしまう
                ''    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E904", New String() {"受注番号:" & setDs.Tables("LMH030_H_OUTKAEDI_DTL_ITW").Rows(0).Item("ORDER_NO").ToString}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, "")
                ''    'エラーフラグ設定してからループを出る
                ''    bNoErr = False
                ''    Exit For
                ''End If
                '2018/03/01 セミEDI_千葉ITW_新規登録 商品マスタ未確定時対応 annen del start
#End If
                '受信DTL⇒EDI出荷(中)へのデータセット(上記で取得した商品情報も含む)
                setDs = Me.SetSemiOutkaEdiM(setDs, mGoodsCnt)

                'EDI出荷(中)の新規追加
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiM", setDs)
                iOutDtlInsCnt = iOutDtlInsCnt + 1

                '前行と差異がある場合は、EDI出荷(大)を新規追加
                If bSameKeyFlg = False Then
                    '受信DTL⇒EDI出荷(大)へのデータセット
                    setDs = Me.SetSemiOutkaEdiL(setDs, sWhcd, sCustCdL, sCustCdM, sNrsGoodsCd, sNrsGoodsNm, sIrime)
#If False Then      'DEL 2018/09/28 依頼番号 : 002437   【LMS】千葉ITW_セミEDIの追加改修(PS吉房)◎担当；阿達→玉野・大極Team◎*T4-差込09* 
                                        'EDI出荷（大）に、すでに同じ受注番号の情報が登録されていないか確認する
                    setDs = MyBase.CallDAC(Me._Dac, "SelectOutkaEdiLCount", setDs)
                    If setDs.Tables("DATA_COUNT").Rows(0).Item("DATA_COUNT").ToString <> "0" Then
                        'メッセージ出力　既に同じ受注番号が登録されている
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E269", New String() {"受注番号:" & setDs.Tables("LMH030_H_OUTKAEDI_DTL_ITW").Rows(0).Item("ORDER_NO").ToString}, "", LMH030BLC.EXCEL_COLTITLE_SEMIEDI, "")
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
