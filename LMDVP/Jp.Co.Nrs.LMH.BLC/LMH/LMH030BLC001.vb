' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID　　　　:  001　　　 : センコー
'  作  成  者       :  umano
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC001
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC001 = New LMH030DAC001()

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
    ''' 汎用届先コードの自動追加フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _genericInsFlg As Integer = 0

#End Region

#Region "Const"

#Region "(センコー)CONST"

    ''' <summary>
    ''' センコー
    ''' </summary>
    ''' <remarks></remarks>
    Public Const WH_CD_DIC As String = "001"             '倉庫コード（センコー千葉工場）


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

        ''要望番号1282 追加START
        ''荷主明細マスタの取得
        'ds = MyBase.CallDAC(Me._DacCom, "SelectListDataMcustDetails2", ds)

        ''汎用届先自動追加フラグの取得
        'Dim genericInsFlg As Integer = 0
        'If ds.Tables("LMH030_M_CUST_DETAILS").Rows.Count > 0 Then
        '    genericInsFlg = Me.GenericDestCheck(ds)
        '    'genericInsFlg = Me._Blc.GenericDestCheck(ds)
        'End If

        ''要望番号1282 追加END

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

        ''要望番号1282 修正START 2012.07.19
        'EDI出荷(大)の初期値設定後のDB存在チェック
        If Me.EdiLDbExistsCheck(ds, rowNo, ediCtlNo, Me._genericInsFlg) = False Then
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

        '出荷管理番号(大)の採番
        'センコーの場合は運送登録なので、出荷は作成しない
        ds = Me.GetOutkaNoL(ds)

        ''出荷管理番号(中)の採番
        'センコーの場合は運送登録なので、出荷は作成しない
        ds = Me.GetOutkaNoM(ds)

        '注意)センコーの場合は運送登録なので、出荷は作成しないがデータセット設定は行う
        '理由)後続の運送設定の際に、出荷(大)で設定された運送個数を使用したい為
        '出荷(大)データセット設定処理
        ds = Me.SetDatasetOutkaL(ds)

        'EDI受信テーブル(DTL)データセット設定
        ds = Me.SetDatasetEdiRcvDtl(ds)

        '運送(大,中)データセット設定
        ds = Me.SetDatasetUnsoL(ds)
        ds = Me.SetDatasetUnsoM(ds)

        'センコーの場合、受信TBLの運送重量をセットする為、再設定しない
        ''運送(大)の運送重量をデータセットに再設定
        ds = Me.SetdatasetUnsoJyuryo(ds)

        '運送登録(通常処理)
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

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
        '届先M取得
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = False OrElse _
            String.IsNullOrEmpty(ediDr("EDI_DEST_CD").ToString().Trim()) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdest", ds)
        End If

        'If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = False OrElse _
        '    String.IsNullOrEmpty(ediDr("EDI_DEST_CD").ToString().Trim()) = False Then

        '    'センコー特殊対応START
        '    If (ediDr("DEST_CD").ToString().Trim()).Equals(ediDr("DEST_TEL").ToString().Trim()) = True Then
        '        '①届先電話番号 = 届先コードの場合(一見コード)
        '        '届先マスタの存在チェックを電話番号で抽出する(複数件存在する可能性有)
        '        ds = MyBase.CallDAC(Me._Dac, "SelectDataMdestTel", ds)
        '        Me._genericInsFlg = 1
        '    Else
        '        '②届先電話番号 <> 届先コードの場合
        '        '届先マスタの存在チェックを届先コードで抽出(通常パターン)
        '        ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdest", ds)
        '    End If
        '    'センコー特殊対応END

        'End If

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
                ediDr("COA_YN") = "2"  'SetInsMDestFromDestの値と一致させる事！（荷主により値が異なるため）
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
        '運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        '割増タリフコード(割増運賃タリフマスタ)
        'DACで値セットを行う
        '2012.02.25 大阪対応 START
        '(三井化学：EDIの時点で値が入っててもタリフMに存在しないケースがある為の対応)
        '①荷主明細マスタの存在チェック(荷主明細マスタに存在していれば入替えOK)
        '荷主明細マスタの取得
        ds = MyBase.CallDAC(Me._DacCom, "SelectListDataMcustDetails", ds)

        '(2012.09.07)要望番号:1425 UMANO START
        '運賃タリフセットマスタ(荷主・運送会社)の取得(運賃・割増タリフ)
        ds = MyBase.CallDAC(Me._DacCom, "SetUnsocoTariffData", ds)
        '(2012.09.07)要望番号:1425 UMANO END

        '(2012.09.07)要望番号:1425 UMANO START
        If MyBase.GetResultCount <> 1 AndAlso _
           MyBase.GetResultCount <> 3 Then
            'タリフセットマスタの取得(運賃タリフ)
            ds = MyBase.CallDAC(Me._DacCom, "SetTariffData", ds)
        End If
        '2012.02.25 大阪対応 END
        '2012.03.05 大阪対応 START
        If MyBase.GetResultCount <> 2 AndAlso _
           MyBase.GetResultCount <> 3 Then
            'タリフセットマスタの取得(割増タリフ)
            ds = MyBase.CallDAC(Me._DacCom, "SetExtcTariffData", ds)
            '2012.03.05 大阪対応 END
        End If
        '(2012.09.07)要望番号:1425 UMANO END

        ''タリフ分類区分
        ''運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        ''割増タリフコード(割増運賃タリフマスタ)
        ''DACで値セットを行う
        ''(三井化学：EDIの時点で値が入っててもタリフMに存在しないケースがある為の対応)
        ''①荷主明細マスタの存在チェック(荷主明細マスタに存在していれば入替えOK)
        ''荷主明細マスタの取得
        'ds = MyBase.CallDAC(Me._DacCom, "SelectListDataMcustDetails", ds)
        ''タリフセットマスタの取得(運賃タリフ)
        'ds = MyBase.CallDAC(Me._DacCom, "SetTariffData", ds)

        ''タリフセットマスタの取得(割増タリフ)
        'ds = MyBase.CallDAC(Me._DacCom, "SetExtcTariffData", ds)


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

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        '↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        'センコー専用チェック

        '①商品名
        Dim mGOODSNm As String = mGoodsDr("GOODS_NM_1").ToString()
        Dim ediGOODSNm As String = ediMDr("GOODS_NM").ToString()

        'スペース除去チェックの追加
        mGOODSNm = Me.SpaceCutChk(mGOODSNm)
        ediGOODSNm = Me.SpaceCutChk(ediGOODSNm)

        If mGOODSNm.Equals(ediGOODSNm) = True Then
            'チェックなし
        Else
            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.SNK_WID_M002, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "商品名"
                msgArray(2) = "商品マスタ"
                msgArray(3) = "商品名"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty

                ds = Me._Blc.SetComWarningM("W159", LMH030BLC.SNK_WID_M002, ds, setDs, msgArray, _
                                            ediMDr("GOODS_NM").ToString(), mGoodsDr("GOODS_NM_1").ToString())

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                '処理区分が"はい"の場合、REMARKを入れ替えて処理続行
                If String.IsNullOrEmpty(ediMDr("REMARK").ToString().Trim) Then
                    ediMDr("REMARK") = mGoodsDr("OUTKA_ATT").ToString
                Else
                    ediMDr("REMARK") = Me._Blc.LeftB(String.Concat(ediMDr("REMARK"), Space(2), mGoodsDr("OUTKA_ATT")), 100)
                End If

                '全角変換したままの場合、桁あふれを起こす事がある
                ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1").ToString()

            End If

        End If

        '②個数単位
        '個数単位が特定できていない場合は、強制的に商品マスタの値を設定
        If String.IsNullOrEmpty(ediMDr("KB_UT").ToString()) = True Then
            ediMDr("KB_UT") = mGoodsDr("NB_UT")
        End If
        '受信時にセットした個数単位と商品マスタの個数単位が異なる場合はワーニング
        If (ediMDr("KB_UT").ToString()).Equals(mGoodsDr("NB_UT").ToString()) = False Then

            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.SNK_WID_M003, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "個数単位"
                msgArray(2) = "商品マスタ"
                msgArray(3) = "個数単位"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty

                ds = Me._Blc.SetComWarningM("W159", LMH030BLC.SNK_WID_M003, ds, setDs, msgArray, _
                                            ediMDr("KB_UT").ToString(), mGoodsDr("NB_UT").ToString())

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                '処理区分が"はい"の場合処理続行
                ediMDr("KB_UT") = mGoodsDr("NB_UT")

            End If

        End If

        '③包装単位
        '包装単位が特定できていない場合は、強制的に商品マスタの値を設定
        If String.IsNullOrEmpty(ediMDr("PKG_UT").ToString()) = True Then
            ediMDr("PKG_UT") = mGoodsDr("PKG_UT")
        End If
        '受信時にセットした個数単位と商品マスタの個数単位が異なる場合はワーニング
        If (ediMDr("PKG_UT").ToString()).Equals(mGoodsDr("PKG_UT").ToString()) = False Then

            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.SNK_WID_M004, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "包装単位"
                msgArray(2) = "商品マスタ"
                msgArray(3) = "包装単位"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty

                ds = Me._Blc.SetComWarningM("W159", LMH030BLC.SNK_WID_M004, ds, setDs, msgArray, _
                                            ediMDr("PKG_UT").ToString(), mGoodsDr("PKG_UT").ToString())

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                '処理区分が"はい"の場合処理続行
                ediMDr("PKG_UT") = mGoodsDr("PKG_UT")

            End If

        End If

        '↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
        'センコー専用チェック

        '分析表区分
        If String.IsNullOrEmpty(ediMDr("COA_YN").ToString()) = True Then

            If Len(mGoodsDr("COA_YN").ToString()) = 2 Then
                ediMDr("COA_YN") = (mGoodsDr("COA_YN").ToString()).Substring(1, 1)
            Else
                ediMDr("COA_YN") = String.Empty
            End If

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


        '引当単位区分
        If String.IsNullOrEmpty(ediMDr("ALCTD_KB").ToString()) = True Then
            If String.IsNullOrEmpty(mGoodsDr("ALCTD_KB").ToString()) = False Then

                ediMDr("ALCTD_KB") = mGoodsDr("ALCTD_KB")
            Else
                ediMDr("ALCTD_KB") = "01"
            End If
        End If

        '数量単位
        ediMDr("QT_UT") = mGoodsDr("STD_IRIME_UT")

        '包装個数
        ediMDr("PKG_NB") = mGoodsDr("PKG_NB")

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

        '③入目
        '入目が特定できていない場合は、強制的に商品マスタの値を設定
        If Convert.ToDecimal(ediMDr("IRIME")) = 0 Then
            ediMDr("IRIME") = mGoodsDr("STD_IRIME_NB")
        End If
        '受信時にセットした入目と商品マスタの入目が異なる場合はエラー
        If Convert.ToDecimal(ediMDr("IRIME")) <> Convert.ToDecimal(mGoodsDr("STD_IRIME_NB")) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"入目", "商品マスタ", "標準入目"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'このレコードのワーニングをクリア
            ds.Tables("WARNING_DTL").Rows.Clear()
            Return False
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

        '出荷包装個数
        If outkaPkgNb = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E161", New String() {}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Select Case alctdKb

            Case "01"
                If 1 < pkgNb Then
                    ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
                    ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb
                Else
                    ediMDr("OUTKA_PKG_NB") = outkaPkgNb + outkaHasu
                    ediMDr("OUTKA_HASU") = 0
                    ediMDr("OUTKA_TTL_QT") = (outkaPkgNb + outkaHasu) * irime
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


        '④個別重量(KGS)
        ''-------------------------------------------------------------------------------------
        ''●荷主固有チェック(センコー専用)個別重量
        ''-------------------------------------------------------------------------------------

        Dim dBetuWt As Double = Convert.ToDouble(ediMDr("BETU_WT").ToString)
        Dim sumWt As Double = Convert.ToDouble(ediMDr("OUTKA_TTL_QT").ToString)
        Dim sumNb As Double = Convert.ToDouble(ediMDr("OUTKA_TTL_NB").ToString)
        Dim betuAmari As Integer = Convert.ToInt32(sumWt Mod sumNb)
        If betuAmari <> 0 Then
            '重量が割り切れなかった場合はワーニングとする

            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.SNK_WID_M005, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "個別重量"
                msgArray(2) = String.Empty
                msgArray(3) = String.Empty
                msgArray(4) = String.Empty
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningM("W210", LMH030BLC.SNK_WID_M005, ds, setDs, msgArray, ediMDr("BETU_WT").ToString, String.Empty)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時

            End If

            '受信時にセットした個別重量と商品マスタの個別重量が異なる場合はワーニング
        ElseIf dBetuWt <> Convert.ToDouble(mGoodsDr("STD_WT_KGS").ToString()) Then

            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.SNK_WID_M006, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "個別重量"
                msgArray(2) = "商品マスタ"
                msgArray(3) = "個別重量"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty

                ds = Me._Blc.SetComWarningM("W159", LMH030BLC.SNK_WID_M006, ds, setDs, msgArray, _
                                            ediMDr("BETU_WT").ToString(), mGoodsDr("STD_WT_KGS").ToString())
                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                '処理区分が"はい"の場合処理続行
                ediMDr("BETU_WT") = mGoodsDr("STD_WT_KGS")

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

        'ワーニングが存在する場合はここでの判定はFalseで返す
        'センコーでワーニングを使用
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
                'センコー特殊処理START
                'センコー特殊のオーダー番号重複チェック(運送荷主の為、運送(大)で抽出しEDI出荷(大)のFREE_C01が同値の場合)
                Call MyBase.CallDAC(Me._Dac, "SelectOrderCheckDataUnsoL", ds)
                If MyBase.GetResultCount > 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"運送データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If
                'センコー特殊処理END
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
        '●荷主固有チェック(センコー専用)
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
            'DEST_CDとEDI_DEST_CDが両方空の場合、エラーとする。
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"届先(EDI)コードが空", "運送登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
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
            'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {"EDI届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'センコー特殊処理START
            'センコーの場合、汎用届先コードは届先電話番号で抽出の為
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {"届先電話番号", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'センコー特殊処理END
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

                choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.SNK_WID_M001, 0)

                '商品マスタ検索（NRS商品コード or 荷主商品コード）
                setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsOutka", setDs))

                '基本的にセンコーの場合は、センコーの汎用商品コードの商品キーが入ってくる為１件になる。
                If MyBase.GetResultCount = 0 Then

                    ''要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    ''要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End

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

                        ds = Me._Blc.SetComWarningM("W162", LMH030BLC.SNK_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)

                        flgWarning = True 'ワーニングフラグをたてて処理続行

                        Continue For
                    End If

                End If

                'EDI出荷(中)の初期値設定処理
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                    'センコーは現段階ではワーニングはないが、共通のロジックを組み込む為入れておく
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
        Dim mAdAll As String = String.Concat(mAd1, mAd2, mAd3)
        Dim mZipJis As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediDestCd As String = dtEdi.Rows(0)("DEST_CD").ToString()
        Dim ediDestNm As String = dtEdi.Rows(0)("DEST_NM").ToString()
        Dim ediAd1 As String = dtEdi.Rows(0).Item("DEST_AD_1").ToString()
        Dim ediAd2 As String = dtEdi.Rows(0).Item("DEST_AD_2").ToString()
        Dim ediAd3 As String = dtEdi.Rows(0).Item("DEST_AD_3").ToString()
        Dim ediTel As String = dtEdi.Rows(0)("DEST_TEL").ToString()
        Dim ediAdAll As String = String.Concat(ediAd1, ediAd2, ediAd3)
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        Dim compareWarningFlg As Boolean = False

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
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SNK_WID_L001, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "届先名称"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "届先名称"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W166", LMH030BLC.SNK_WID_L001, ds, msgArray, ediDestNm, mDestNm)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("DEST_NM") = dtEdi.Rows(0)("DEST_NM").ToString()
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            End If

        End If

        '届先住所(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediAdAll) = True Then
            'チェックなし
        Else

            mAdAll = SpaceCutChk(mAdAll)
            ediAdAll = SpaceCutChk(ediAdAll)
            If mAdAll.Equals(ediAdAll) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SNK_WID_L002, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先住所"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "住所"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.SNK_WID_L002, ds, msgArray, ediAdAll, mAdAll)

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

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SNK_WID_L003, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先電話番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "電話番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.SNK_WID_L003, ds, msgArray, ediTel, mTel)

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

        If mJis.Equals(ediDestJisCd) = False Then

            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SNK_WID_L004, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = "届先JISコード"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W166", LMH030BLC.SNK_WID_L004, ds, msgArray, ediDestJisCd, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("JIS") = dtEdi.Rows(0)("DEST_JIS_CD").ToString()
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            End If

        End If

        'ワーニングが存在する場合はここでの判定はFalseで返す
        If compareWarningFlg = True Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "届先マスタ自動追加(センコー専用)"

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

        Dim choiceKb As String = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.SNK_WID_L005, 0)

        ''要望番号1282 修正START 2012.07.19(汎用届先コードの自動追加の場合はワーニングを出力しない)
        If String.IsNullOrEmpty(choiceKb) = True AndAlso genericInsFlg = 0 Then
            'If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = workDestString
            msgArray(2) = "届先マスタ"
            msgArray(3) = "EDIデータ"
            msgArray(4) = String.Empty

            ds = Me._Blc.SetComWarningL("W182", LMH030BLC.SNK_WID_L005, ds, msgArray, workDestCd, String.Empty)

            compareWarningFlg = True

            ''要望番号1282 修正START 2012.07.19(汎用届先コードの場合はワーニングなしで自動追加)
        ElseIf choiceKb.Equals("01") = True OrElse genericInsFlg = 1 Then
            'ElseIf choiceKb.Equals("01") = True Then

            'ワーニングで"はい"を選択時
            Dim drMD As DataRow = dtMD.NewRow()
            drMD("NRS_BR_CD") = drEdiL("NRS_BR_CD").ToString()
            drMD("CUST_CD_L") = drEdiL("CUST_CD_L").ToString()
            drMD("DEST_CD") = workDestCd
            If genericInsFlg = 0 Then
                drMD("EDI_CD") = workDestCd
            ElseIf genericInsFlg = 1 Then
                drMD("EDI_CD") = Replace(Replace(Replace(workDestCd, "-", String.Empty), "(", String.Empty), ")", String.Empty)
            End If
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

        Dim num As New NumberMasterUtility

        '通常登録
        unsoDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
        'センコーの場合は運送登録なので、前の処理でFREE_C30で取得した運送番号を使用
        unsoDr("UNSO_NO_L") = ediDr("FREE_C30").ToString().Substring(3, 9)
        unsoDr("YUSO_BR_CD") = ediDr("NRS_BR_CD")
        unsoDr("WH_CD") = ediDr("WH_CD")
        'センコーの場合は運送登録なので、出荷管理番号は空
        unsoDr("INOUTKA_NO_L") = String.Empty
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
        'センコー特殊処理START
        'センコーの場合、荷主参照番号にオーダー番号を設定
        unsoDr("CUST_REF_NO") = ediDr("CUST_ORD_NO")
        'センコー特殊処理END
        unsoDr("SHIP_CD") = ediDr("SHIP_CD_L")
        unsoDr("ORIG_CD") = ediDr("FREE_C22")
        unsoDr("DEST_CD") = ediDr("DEST_CD")
        unsoDr("UNSO_PKG_NB") = outkaLDr("OUTKA_PKG_NB")
        'unsoDr("NB_UT") = ediDr("NB_UT") '運送Mで取得の為ここではコメント
        'センコー特殊処理START
        'センコーの場合、運送重量は受信TBLより設定した値を使用する
        unsoDr("UNSO_WT") = 0             '運送Mの集計値
        'unsoDr("UNSO_WT") = ediDr("FREE_N04")
        'センコー特殊処理END
        unsoDr("UNSO_ONDO_KB") = ediMDr("UNSO_ONDO_KB")
        unsoDr("PC_KB") = ediDr("PC_KB")
        unsoDr("TARIFF_BUNRUI_KB") = ediDr("UNSO_TEHAI_KB")
        unsoDr("VCLE_KB") = ediDr("SYARYO_KB")
        'センコーの場合は運送登録なので元データ区分は運送
        unsoDr("MOTO_DATA_KB") = "40"
        unsoDr("TAX_KB") = "01" '課税区分は"01"(課税)固定とする
        unsoDr("REMARK") = ediDr("UNSO_ATT")
        unsoDr("SEIQ_TARIFF_CD") = ediDr("UNCHIN_TARIFF_CD")
        unsoDr("SEIQ_ETARIFF_CD") = ediDr("EXTC_TARIFF_CD")
        unsoDr("AD_3") = outkaLDr("DEST_AD_3")

        unsoDr("UNSO_TEHAI_KB") = ediDr("UNSO_MOTO_KB")
        unsoDr("BUY_CHU_NO") = ediDr("BUYER_ORD_NO")
        unsoDr("TYUKEI_HAISO_FLG") = "00"   '中継配送フラグ"00:無し"を設定
        unsoDr("SYUKA_TYUKEI_CD") = String.Empty
        unsoDr("HAIKA_TYUKEI_CD") = String.Empty
        unsoDr("TRIP_NO_SYUKA") = String.Empty
        unsoDr("TRIP_NO_TYUKEI") = String.Empty
        unsoDr("TRIP_NO_HAIKA") = String.Empty

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
        '2012.11.13 センコー特殊(幹線配達区分)　追加START
        unsoDr("MAIN_DELI_KB") = ediDr("FREE_C20")
        '2012.11.13 センコー特殊(幹線配達区分)　追加END

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
        Dim drL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
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

            'センコーの場合は運送登録なので、出荷管理番号(中)が存在しないので採番する
            '運送登録処理の場合
            unsoMDr("UNSO_NO_M") = (i + 1).ToString("000")
            unsoMDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            unsoMDr("GOODS_NM") = ediDr("GOODS_NM")
            unsoMDr("UNSO_TTL_NB") = ediDr("OUTKA_PKG_NB")
            unsoMDr("NB_UT") = ediDr("KB_UT")
            unsoMDr("UNSO_TTL_QT") = ediDr("OUTKA_TTL_QT")
            unsoMDr("QT_UT") = ediDr("QT_UT")
            unsoMDr("HASU") = ediDr("OUTKA_HASU")
            unsoMDr("ZAI_REC_NO") = String.Empty

            'センコーの場合は運送登録なので、運送温度区分が"90"の場合は入替
            If (ediDr("UNSO_ONDO_KB").ToString()).Equals("90") = True Then
                unsoMDr("UNSO_ONDO_KB") = ediDr("ONDO_KB")
            Else
                unsoMDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
            End If
            unsoMDr("IRIME") = ediDr("IRIME")
            unsoMDr("IRIME_UT") = ediDr("IRIME_UT")

            stdWtKgs = Convert.ToDecimal(ediDr("STD_WT_KGS"))
            irime = Convert.ToDecimal(ediDr("IRIME"))
            stdIrimeNb = Convert.ToDecimal(ediDr("STD_IRIME_NB"))

            If String.IsNullOrEmpty(ediDr("NISUGATA").ToString()) = False Then
                nisugata = Convert.ToDecimal(ediDr("NISUGATA"))
            End If
            outkaTtlNb = Convert.ToDecimal(ediDr("OUTKA_TTL_NB"))

            '個別重量は運送側で計算せずに、EDIデータを使用する
            'If ediDr("TARE_YN").Equals("01") = False Then
            '    unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb)

            'Else
            '    unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb + nisugata)

            'End If

            unsoMDr("BETU_WT") = ediDr("BETU_WT")

            unsoMDr("SIZE_KB") = String.Empty
            'unsoMDr("ZBUKA_CD") = ediDr("FREE_C02")
            'unsoMDr("ABUKA_CD") = ediDr("FREE_C01")
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
        'Dim drJudge As DataRow

        '標準重量
        ediMDr("STD_WT_KGS") = mGoodsDr("STD_WT_KGS")
        '標準入目
        ediMDr("STD_IRIME_NB") = mGoodsDr("STD_IRIME_NB")

        '風袋加算フラグ
        ediMDr("TARE_YN") = mGoodsDr("TARE_YN")

        'If String.IsNullOrEmpty(ediMDr("PKG_UT").ToString) = False Then
        '    '荷姿(区分マスタ)
        '    drJudge = ds.Tables("LMH030_JUDGE").Rows(0)
        '    drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N001
        '    drJudge("KBN_CD") = ediMDr("PKG_UT")

        '    ds = MyBase.CallDAC(Me._DacCom, "SelectDataPkgUtZkbn", ds)

        '    If MyBase.GetResultCount = 0 Then
        '        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"包装単位", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '        Return False
        '    End If

        '    Dim zkbnDr As DataRow = ds.Tables("LMH030_Z_KBN").Rows(0)
        '    '風袋重量
        '    ediMDr("NISUGATA") = zkbnDr("NISUGATA")
        'End If

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

#Region "実績作成処理"

    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSakusei(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        Dim updFlg As String = ds.Tables("LMH030INOUT").Rows(0).Item("OUTKA_L_UPD_FLG").ToString()

        ''★★★2011.09.14 追加START
        'Dim msgArray(5) As String
        'Dim choiceKb As String = String.Empty

        ''AGC出荷指示ID(OUTKA_SIJI_ID)重複チェック(センコー送信TBL)

        'ds = MyBase.CallDAC(Me._Dac, "SelectSenkoOutkaSijiId", ds)
        'If MyBase.GetResultCount <> 0 Then

        '    choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030INOUT"), ds, LMH030BLC.NCGO_WID_L007, 0)

        '    If String.IsNullOrEmpty(choiceKb) = True Then

        '        msgArray(1) = "同一オーダー番号での変更"
        '        msgArray(2) = "出荷"
        '        msgArray(3) = String.Empty
        '        msgArray(4) = String.Empty
        '        ds = Me._Blc.SetComWarningL("W181", LMH030BLC.NCGO_WID_L007, ds, msgArray, String.Empty, String.Empty)
        '        Return ds
        '    ElseIf choiceKb.Equals("01") = True Then
        '        'ワーニングで"はい"を選択時

        '    ElseIf choiceKb.Equals("03") = True Then
        '        'ワーニングで"キャンセル"を選択時
        '        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)                        '!!!TO-DO 後にEXCEL出力に変更!!!
        '        Return ds
        '    End If

        'End If

        'センコーEDI実績の値設定(EDI受信TBLより)
        ds = MyBase.CallDAC(Me._Dac, "SelectSnkEdiSend", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"EDI受信テーブル", "該当レコード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'センコーEDI実績データの更新
        ds = MyBase.CallDAC(Me._Dac, "InsertSnkEdiSendData", ds)

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        Return ds

    End Function

#End Region

#End Region

End Class

