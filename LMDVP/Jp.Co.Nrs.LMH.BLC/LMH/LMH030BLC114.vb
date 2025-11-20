' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH              : EDI
'  プログラムID     :  LMH030           : EDI出荷検索
'  EDI荷主ID　　　　:  114　　　        : アクタス
'  作  成  者       :  Umano 
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC114
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"
    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC114 = New LMH030DAC114()

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

#Region "CONST"


#End Region

#Region "Method(出荷登録)"

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

        '直送先情報チェック
        '直送先で直送先電話番号が存在しなく、届け先情報が０件の場合はエラー
        If ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("FREE_C04").ToString().Equals("02") = True AndAlso _
           String.IsNullOrEmpty(ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("FREE_C26").ToString()) = True AndAlso _
           ds.Tables("LMH030_M_DEST").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E375", New String() {"直送先情報不足", "届先マスタ取得"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        '届け先情報が複数件取得できる場合はエラー
        If ds.Tables("LMH030_M_DEST").Rows.Count > 1 Then
            'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {"EDI届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If


        '届先自動追加チェック
        Dim sFlag17 As String = ds.Tables("LMH030INOUT").Rows(0).Item("FLAG_17").ToString()
        If (sFlag17.Equals(LMConst.FLG.OFF) OrElse sFlag17.Equals("2")) AndAlso (ds.Tables("LMH030_M_DEST").Rows.Count = 0) Then
            'エラーメッセージ
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
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
                    ds = Me._Blc.SetComWarningL("W199", LMH030BLC.ATS_WID_L001, ds, msgArray, matomeNo, String.Empty)
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
                    ds = Me._Blc.SetComWarningL("W168", LMH030BLC.ATS_WID_L002, ds, msgArray, matomeNo, String.Empty)
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
                        ds = Me._Blc.SetComWarningL("W198", LMH030BLC.ATS_WID_L003, ds, msgArray, matomeNo, String.Empty)
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

        '紐付け処理の場合は、別Funcでデータセット設定+更新処理(紐づけは現状行わない:2014/04/04 黎) --ST--
        'Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()
        'If eventShubetsu.Equals("3") Then
        '    '紐付処理をして終了
        '    ds = Me.Himoduke(ds)
        '    Return ds
        'End If
        '紐付け処理の場合は、別Funcでデータセット設定+更新処理(紐づけは現状行わない:2014/04/04 黎) --ED--

        '出荷(大)データセット設定
        ds = Me.SetDatasetOutkaL(ds, matomeFlg)

        '出荷(中)データセット設定
        ds = Me.SetDatasetOutkaM(ds, matomeFlg)

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

#Region "EDI出荷(大)の初期値設定(出荷登録処理)"
    ''' <summary>
    ''' EDI_Lの初期設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>各マスタ値を取得しEDI_Lの初期設定をする</remarks>
    Private Function SetEdiLShoki(ByVal ds As DataSet) As DataSet

        ''荷主M取得
        'ds = MyBase.CallDAC(Me._DacCom, "SelectMcustOutkaToroku", ds)

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        'Dim mCustDr As DataRow = ds.Tables("LMH030_M_CUST").Rows(0)
        Dim mDestDr As DataRow = Nothing
        Dim mDestFlgYN As Boolean = False      '届先マスタ有無フラグ


        'FREE_C02(経由コード)を元に運送会社設定M(M_GENERIC_UNSOCO)の値を取得
        'ds = MyBase.CallDAC(Me._Dac, "SelectDataMGenericUnsoco", ds)
        ds = MyBase.CallDAC(Me._Dac, "SelectDataMInfoStateAts", ds)

        'FREE_C04(届け先使用区分)を設定する
        '"01"→届け先を使用、"02"→直送先を使用
        '直送先の値が入っている場合→(FREE_C03(直送先名),FREE_C24(直送先住所))
        'If String.IsNullOrEmpty(ediDr("FREE_C03").ToString().Trim()) = False OrElse _
        '    String.IsNullOrEmpty(ediDr("FREE_C24").ToString().Trim()) = False Then
        '    If ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows.Count > 0 Then
        '        If ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(0).Item("DEFAULT_UNSO_FLG").ToString().Equals("1") = True Then
        '            ediDr("FREE_C04") = "02"
        '        Else
        '            ediDr("FREE_C04") = "01"
        '        End If

        '    End If
        'Else
        '    ediDr("FREE_C04") = "01"

        'End If

        '出荷報告有無
        ediDr("OUTKAHOKOKU_YN") = "0"

        '送り状作成有無(初期値設定)
        If String.IsNullOrEmpty(ediDr("DENP_YN").ToString().Trim()) = True Then
            ediDr("DENP_YN") = "0"
        End If

        If ds.Tables("LMH030_M_INFOSTATE_ATS").Rows.Count <> 0 Then
            ediDr("FREE_C04") = ds.Tables("LMH030_M_INFOSTATE_ATS").Rows(0).Item("DELIVERY_KBN").ToString()
        ElseIf ds.Tables("LMH030_M_INFOSTATE_ATS").Rows.Count = 0 Then
            Return ds
        End If


        '届け先情報の設定
        Select Case ediDr("FREE_C04").ToString()

            Case "01"  '届け先の場合は設定値のまま

                '届先CDより届先情報を設定
                If String.IsNullOrEmpty(ds.Tables("LMH030_M_INFOSTATE_ATS").Rows(0).Item("DELIVERY_CD").ToString()) = False Then
                    ediDr("DEST_CD") = ds.Tables("LMH030_M_INFOSTATE_ATS").Rows(0).Item("DELIVERY_CD").ToString()
                ElseIf String.IsNullOrEmpty(ediDr("FREE_C01").ToString()) = False Then
                    ediDr("DEST_CD") = String.Concat(ediDr("DEST_CD").ToString(), "-", ediDr("FREE_C01").ToString())
                End If
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdest", ds)
                If ds.Tables("LMH030_M_DEST").Rows.Count = 0 Then
                    Return ds
                Else
                    mDestDr = ds.Tables("LMH030_M_DEST").Rows(0)
                    ediDr("DEST_NM") = mDestDr("DEST_NM").ToString()
                    ediDr("DEST_ZIP") = mDestDr("ZIP").ToString()
                    ediDr("DEST_AD_1") = mDestDr("AD_1").ToString()
                    ediDr("DEST_AD_2") = mDestDr("AD_2").ToString()
                    'ediDr("DEST_AD_3") = mDestDr("AD_3").ToString()
                    ediDr("DEST_TEL") = mDestDr("TEL").ToString()
                    ediDr("DEST_JIS_CD") = mDestDr("JIS").ToString()
                End If

            Case "02"  '直送先情報を設定する

                Dim choiceKb As String = String.Empty
                Dim msgArray(5) As String

                '直送先情報より届先情報を設定
                '直送先電話番号が入っていない場合はエラー
                If String.IsNullOrEmpty(ediDr("FREE_C26").ToString()) = False Then
                    ds = MyBase.CallDAC(Me._Dac, "SelectDataMDestTel", ds)
                End If
                If ds.Tables("LMH030_M_DEST").Rows.Count = 0 Then
                    '直送先名称(FREE_C03)が空の場合は、現状の届け先コードより届け先マスタを取得する
                    If String.IsNullOrEmpty(ediDr("FREE_C03").ToString()) = True Then

                        If String.IsNullOrEmpty(ediDr("FREE_C01").ToString()) = False Then
                            ediDr("DEST_CD") = String.Concat(ediDr("DEST_CD").ToString(), "-", ediDr("FREE_C01").ToString())
                        End If

                        ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdest", ds)

                        If ds.Tables("LMH030_M_DEST").Rows.Count = 0 Then
                            Return ds
                        Else
                            mDestDr = ds.Tables("LMH030_M_DEST").Rows(0)
                            ediDr("DEST_NM") = mDestDr("DEST_NM").ToString()
                            ediDr("DEST_ZIP") = mDestDr("ZIP").ToString()
                            ediDr("DEST_AD_1") = mDestDr("AD_1").ToString()
                            ediDr("DEST_AD_2") = mDestDr("AD_2").ToString()
                            ediDr("DEST_TEL") = mDestDr("TEL").ToString()
                            ediDr("DEST_JIS_CD") = mDestDr("JIS").ToString()
                            ediDr("FREE_C04") = "01" '届け先情報に変更
                        End If

                    Else
                        Return ds
                    End If
                ElseIf ds.Tables("LMH030_M_DEST").Rows.Count = 1 Then
                    mDestDr = ds.Tables("LMH030_M_DEST").Rows(0)
                    ediDr("DEST_CD") = mDestDr("DEST_CD").ToString()
                    ediDr("DEST_NM") = mDestDr("DEST_NM").ToString()
                    ediDr("DEST_ZIP") = mDestDr("ZIP").ToString()
                    ediDr("DEST_AD_1") = mDestDr("AD_1").ToString()
                    ediDr("DEST_AD_2") = mDestDr("AD_2").ToString()
                    'ediDr("DEST_AD_3") = mDestDr("AD_3").ToString()
                    ediDr("DEST_TEL") = mDestDr("TEL").ToString()
                    ediDr("DEST_JIS_CD") = mDestDr("JIS").ToString()
                Else

                    '■届先コードをセット■
                    choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.ATS_WID_L010, 0)

                    '■届先マスタを取得■
                    If String.IsNullOrEmpty(choiceKb) = False Then

                        ds.Tables("LMH030_M_DEST").Clear()
                        ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdest", ds)

                        If ds.Tables("LMH030_M_DEST").Rows.Count = 1 Then
                            mDestDr = ds.Tables("LMH030_M_DEST").Rows(0)
                            ediDr("DEST_NM") = mDestDr("DEST_NM").ToString()
                            ediDr("DEST_ZIP") = mDestDr("ZIP").ToString()
                            ediDr("DEST_AD_1") = mDestDr("AD_1").ToString()
                            ediDr("DEST_AD_2") = mDestDr("AD_2").ToString()
                            ediDr("DEST_TEL") = mDestDr("TEL").ToString()
                            ediDr("DEST_JIS_CD") = mDestDr("JIS").ToString()
                        End If

                    Else
                        '届先POPを表示
                        '届先が確定できない場合はワーニング
                        ds = Me._Blc.SetComWarningL("W208", LMH030BLC.ATS_WID_L010, ds, msgArray, _
                                                    String.Concat("直送先名称:", ediDr("FREE_C03").ToString(), Space(1), "直送先電話番号:", ediDr("FREE_C26").ToString()), String.Empty)
                        Return ds
                    End If

                End If

            Case Else

        End Select

        '最終納品先名表示フラグが"1"の場合は、FREE_C03(直送先名称)を設定
        If ("1").Equals(ds.Tables("LMH030_M_INFOSTATE_ATS").Rows(0).Item("LAST_DELIVERY_FLG").ToString()) = True Then
            ediDr("DEST_AD_3") = ediDr("FREE_C03").ToString()
        Else
            ediDr("DEST_AD_3") = mDestDr("AD_3").ToString()
        End If

        '届先M取得
        'If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = False OrElse _
        '    String.IsNullOrEmpty(ediDr("EDI_DEST_CD").ToString().Trim()) = False Then
        '    ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdest", ds)
        'End If

        If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
            mDestDr = ds.Tables("LMH030_M_DEST").Rows(0)
            mDestFlgYN = True
        End If

        'ピッキングリスト区分
        If String.IsNullOrEmpty(ediDr("PICK_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("PICK_KB") = mDestDr("PICK_KB").ToString().Trim()
                If String.IsNullOrEmpty(ediDr("PICK_KB").ToString().Trim()) = True Then
                    ediDr("PICK_KB") = "01"
                End If
            Else
                ediDr("PICK_KB") = "01"
            End If
        End If

        '出庫日
        '出荷予定日
        '納入予定日

        'If String.IsNullOrEmpty(ediDr("OUTKO_DATE").ToString().Trim()) = True Then
        '    ediDr("OUTKO_DATE") = ediDr("OUTKA_PLAN_DATE")
        'End If

        Dim dOutkaDate As DateTime = Convert.ToDateTime(Me._Blc.GetSlashEditDate(ediDr("OUTKA_PLAN_DATE").ToString()))
        Dim dArrDate As DateTime = Convert.ToDateTime(Me._Blc.GetSlashEditDate(ediDr("ARR_PLAN_DATE").ToString()))
        Dim outkaAddtionday As Integer = Convert.ToInt32(ds.Tables("LMH030_M_INFOSTATE_ATS").Rows(0).Item("OUTKA_ADDTION_DATE"))
        Dim arrAddtionday As Integer = Convert.ToInt32(ds.Tables("LMH030_M_INFOSTATE_ATS").Rows(0).Item("ARR_ADDTION_DATE"))

        Dim outkaPlandate As DateTime = dOutkaDate.AddDays(outkaAddtionday)
        Dim arrPlandate As DateTime = dArrDate.AddDays(arrAddtionday)

        ediDr("OUTKO_DATE") = outkaPlandate.ToShortDateString.Replace("/", String.Empty)
        ediDr("OUTKA_PLAN_DATE") = outkaPlandate.ToShortDateString.Replace("/", String.Empty)
        ediDr("ARR_PLAN_DATE") = arrPlandate.ToShortDateString.Replace("/", String.Empty)

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
            Else
                ediDr("COA_YN") = "0"
            End If
        End If

        ''運送手配区分
        'Dim dBussinessDate As DateTime = Convert.ToDateTime(Me._Blc.GetSlashEditDate(ediDr("OUTKA_PLAN_DATE").ToString()))

        ''火曜日以外の場合は路線(西濃)
        'If Weekday(dBussinessDate) <> 3 Then
        '    Dim max As Integer = ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows.Count - 1
        '    If ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(max).Item("DEFAULT_UNSO_FLG").ToString().Equals("1") = True Then
        '        ediDr("UNSO_MOTO_KB") = ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(max).Item("UNSO_TEHAI_KB").ToString()
        '        ediDr("BIN_KB") = ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(max).Item("BIN_KB").ToString()
        '        ediDr("UNSO_CD") = ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(max).Item("UNSO_CD").ToString().Trim()
        '        ediDr("UNSO_BR_CD") = ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(max).Item("UNSO_BR_CD").ToString().Trim()
        '    Else
        '        ediDr("UNSO_MOTO_KB") = "90"
        '        ediDr("BIN_KB") = String.Empty
        '        ediDr("UNSO_CD") = String.Empty
        '        ediDr("UNSO_BR_CD") = String.Empty
        '    End If

        'Else
        '    '火曜日の場合は、

        '    Select Case ediDr("FREE_C04").ToString()

        '        Case "01"
        '            If ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(0).Item("DEFAULT_UNSO_FLG").ToString().Equals("1") = True Then
        '                ediDr("UNSO_MOTO_KB") = ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(0).Item("UNSO_TEHAI_KB").ToString()
        '                ediDr("BIN_KB") = ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(0).Item("BIN_KB").ToString()
        '                ediDr("UNSO_CD") = ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(0).Item("UNSO_CD").ToString().Trim()
        '                ediDr("UNSO_BR_CD") = ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(0).Item("UNSO_BR_CD").ToString().Trim()
        '            Else
        '                ediDr("UNSO_MOTO_KB") = "90"
        '                ediDr("BIN_KB") = String.Empty
        '                ediDr("UNSO_CD") = String.Empty
        '                ediDr("UNSO_BR_CD") = String.Empty
        '            End If

        '        Case "02"
        '            If ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(0).Item("DEFAULT_UNSO_FLG").ToString().Equals("0") = True Then
        '                ediDr("UNSO_MOTO_KB") = ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(0).Item("UNSO_TEHAI_KB").ToString()
        '                ediDr("BIN_KB") = ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(0).Item("BIN_KB").ToString()
        '                ediDr("UNSO_CD") = ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(0).Item("UNSO_CD").ToString().Trim()
        '                ediDr("UNSO_BR_CD") = ds.Tables("LMH030_M_GENERIC_UNSOCO").Rows(0).Item("UNSO_BR_CD").ToString().Trim()
        '            Else
        '                ediDr("UNSO_MOTO_KB") = "90"
        '                ediDr("BIN_KB") = String.Empty
        '                ediDr("UNSO_CD") = String.Empty
        '                ediDr("UNSO_BR_CD") = String.Empty
        '            End If

        '    End Select


        'End If

        ediDr("UNSO_MOTO_KB") = ds.Tables("LMH030_M_INFOSTATE_ATS").Rows(0).Item("UNSO_TEHAI_KB").ToString()
        ediDr("BIN_KB") = ds.Tables("LMH030_M_INFOSTATE_ATS").Rows(0).Item("BIN_KB").ToString()
        ediDr("UNSO_CD") = ds.Tables("LMH030_M_INFOSTATE_ATS").Rows(0).Item("UNSO_CD").ToString().Trim()
        ediDr("UNSO_BR_CD") = ds.Tables("LMH030_M_INFOSTATE_ATS").Rows(0).Item("UNSO_BR_CD").ToString().Trim()


        'ediDr("UNSO_MOTO_KB") = mCustDr("UNSO_TEHAI_KB").ToString().Trim()
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
                    'ediDr("UNSO_CD") = mCustDr("SP_UNSO_CD").ToString().Trim()
                    'ediDr("UNSO_BR_CD") = mCustDr("SP_UNSO_BR_CD").ToString().Trim()
                End If
            Else
                'ediDr("UNSO_CD") = mCustDr("SP_UNSO_CD").ToString().Trim()
                'ediDr("UNSO_BR_CD") = mCustDr("SP_UNSO_BR_CD").ToString().Trim()
            End If

        End If

        '運賃・割増タリフを荷主より取得する(※タリフは荷主より取得する)
        ediDr("FREE_C29") = "00"

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
                    End If
                End If

            End If

        End If

        '送り状作成有無
        'If String.IsNullOrEmpty(ediDr("DENP_YN").ToString().Trim()) = True Then
        If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = False AndAlso _
            String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString().Trim()) = False Then
            '運送会社荷主別送り状マスタの存在チェック
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataUnsoCustRpt", ds)
            If MyBase.GetResultCount = 0 Then
                ediDr("DENP_YN") = "0"
            Else
                ediDr("DENP_YN") = "1"
            End If
        Else
            ediDr("DENP_YN") = "0"
        End If

        'End If

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

#End Region

#Region "EDI出荷(中)の初期値設定(出荷登録処理)"
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
#If True Then   'ADD 2019/12/24 009299【LMS】フィルメEDI出荷登録_数量10倍誤出荷理由調査

        If ("1").Equals(ediMDr.Item("GOODSM_FUKAKUTEI_FLG").ToString) = True Then
            '商品M不確定チェック

            'ediMDr("OUTKA_HASU") = ediMDr.Item("OUTKA_TTL_NB")

            '---数量 2020/02/22
            ediMDr("OUTKA_TTL_QT") = Convert.ToInt32(ediMDr.Item("OUTKA_TTL_NB")) * Convert.ToDecimal(mGoodsDr.Item("STD_IRIME_NB"))

            'OUTKA_PKG_NB計算し設定
            ediMDr("OUTKA_PKG_NB") = Convert.ToInt32(ediMDr.Item("OUTKA_TTL_NB")) \ Convert.ToInt32(mGoodsDr.Item("PKG_NB"))
            ' ''端数も再設定
            ediMDr("OUTKA_HASU") = Convert.ToInt32(ediMDr.Item("OUTKA_TTL_NB")) Mod Convert.ToInt32(mGoodsDr.Item("PKG_NB"))

        End If
#End If
            ''-------------------------------------------------------------------------------------
            ''●チェック
            ''-------------------------------------------------------------------------------------

            Dim flgWarning As Boolean = False
            Dim compareWarningFlg As Boolean = False
            Dim msgArray(5) As String
            Dim choiceKb As String = String.Empty
            Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

            '商品名
            ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1")

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

            ''荷主注文番号(明細単位)
            'If String.IsNullOrEmpty(ediMDr("CUST_ORD_NO_DTL").ToString()) = True Then
            '    ediMDr("CUST_ORD_NO_DTL") = ediLDr("CUST_ORD_NO")
            'End If

            ''買主注文番号(明細単位)
            'If String.IsNullOrEmpty(ediMDr("BUYER_ORD_NO_DTL").ToString()) = True Then
            '    ediMDr("BUYER_ORD_NO_DTL") = ediLDr("BUYER_ORD_NO")
            'End If

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
            Dim pkgNb As Double = Convert.ToDouble(ediMDr("PKG_NB"))
            Dim outkaPkgNb As Double = Convert.ToDouble(ediMDr("OUTKA_PKG_NB"))
            Dim outkaHasu As Double = Convert.ToDouble(ediMDr("OUTKA_HASU"))
            Dim alctdKb As String = ediMDr("ALCTD_KB").ToString
            Dim irime As Double = Convert.ToDouble(ediMDr("IRIME"))
            Dim outkaTtlQt As Double = Convert.ToDouble(ediMDr("OUTKA_TTL_QT"))

            Select Case alctdKb

                Case "01"
#If False Then  'UPD 2020/01/22
                                'If 1 < pkgNb Then

                '    ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
                '    ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb
                'Else
                '    ediMDr("OUTKA_PKG_NB") = pkgNb * outkaPkgNb + outkaHasu
                '    ediMDr("OUTKA_HASU") = 0
                'End If

                'ediMDr("OUTKA_TTL_QT") = (pkgNb * outkaPkgNb + outkaHasu) * irime

                ediMDr("OUTKA_PKG_NB") = 0
                ediMDr("OUTKA_HASU") = outkaPkgNb
                ediMDr("OUTKA_TTL_QT") = outkaPkgNb * irime

#Else
                    If 1 < pkgNb Then

                        ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
                        ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb
                    Else
                        ediMDr("OUTKA_PKG_NB") = pkgNb * outkaPkgNb + outkaHasu
                        ediMDr("OUTKA_HASU") = 0
                    End If

                    ediMDr("OUTKA_TTL_QT") = (pkgNb * outkaPkgNb + outkaHasu) * irime
                    'SHINODA 2014/11/13 ナフコセミEDI対応 START
                    ediMDr("OUTKA_TTL_NB") = pkgNb * outkaPkgNb + outkaHasu

#End If



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
            ediMDr("BETU_WT") = mGoodsDr("STD_WT_KGS")

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

        If ds.Tables("LMH030_M_DEST").Rows.Count = 1 Then

            '-------------------------------------------------------------------------------------
            '●荷主共通チェック
            '-------------------------------------------------------------------------------------
            'オーダー番号重複チェック
            If String.IsNullOrEmpty(drEdiL.Item("CUST_ORD_NO").ToString) = False Then
                Dim actionNm As String = String.Empty

                Select Case drIn("ORDER_CHECK_FLG").ToString()
                    Case "1"
                        actionNm = "SelectOrderCheckData"
                    Case "2"
                        actionNm = "SelectOrderCheckDataInSum"

                End Select

                If String.IsNullOrEmpty(actionNm) = False Then
                    Call MyBase.CallDAC(Me._DacCom, actionNm, ds)
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

            ''倉庫コード(倉庫マスタ)
            'ds = MyBase.CallDAC(Me._DacCom, "SelectDataSoko", ds)

            'If MyBase.GetResultCount = 0 Then
            '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"倉庫コード", "倉庫マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return False
            'End If

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

            ''荷主コード(荷主マスタ)
            'ds = MyBase.CallDAC(Me._DacCom, "SelectDataMcust", ds)

            'If MyBase.GetResultCount = 0 Then
            '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"荷主コード", "荷主マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return False
            'End If

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

        End If
        '-------------------------------------------------------------------------------------
        '●荷主固有チェック(標準化荷主用)
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
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"届先(EDI)コードが空", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Dim mDestCount As Integer = ds.Tables("LMH030_M_DEST").Rows.Count

        If mDestCount = 1 Then
            '1件に特定できた場合、マスタ値とEDI出荷(大)の整合性チェック
            'セミEDI時点での届先Ｍ情報と出荷登録時の届先Ｍ情報がズレがないかのチェック
            If Me.DestCompareCheck(ds, rowNo, ediCtlNo) = False Then

                Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                    '整合性チェックでエラーがあった場合は処理終了
                    Return False
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
#If False Then  'UPD 2020/01/17 010139【LMS】フィルメ誤出荷類似_EDI取込で標準機能使用_出荷登録で個別荷主機能使用
                'choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.ATS_WID_M001, 0)

                'If choiceKb.Equals("03") = True Then
                '    'ワーニングで"キャンセル"を選択時
                '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'End If
#Else
                choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.ATS_WID_M001, 0)

                If choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                End If
#End If

                '商品マスタ検索（NRS商品コード or 荷主商品コード）
                ' ''setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsOutka", setDs))

                ' ''If MyBase.GetResultCount = 0 Then

                If String.IsNullOrEmpty(setDtM.Rows(0)("NRS_GOODS_CD").ToString()) = True Then
                    '荷主商品コードで再検索
                    setDs = (MyBase.CallDAC(Me._Dac, "SelectDataMgoodsCust", setDs))

                Else
                    '商品マスタ検索（NRS商品コード or 荷主商品コード
                    setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsOutka", setDs))

                End If

                If MyBase.GetResultCount = 1 Then
                ElseIf MyBase.GetResultCount > 1 Then
#If False Then  'UPD 2020/01/17 010139【LMS】フィルメ誤出荷類似_EDI取込で標準機能使用_出荷登録で個別荷主機能使用
                                                ''再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                        'msgArray(1) = String.Empty
                        'msgArray(2) = String.Empty
                        'msgArray(3) = String.Empty
                        'msgArray(4) = String.Empty

                        ''2012.03.19 修正START
                        'ds = Me._Blc.SetComWarningM("W162", LMH030BLC.ATS_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)
                        ''2012.03.19 修正END

                        'flgWarning = True 'ワーニングフラグをたてて処理続行

                        'Continue For

#Else
                    '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                    msgArray(1) = String.Empty
                    msgArray(2) = String.Empty
                    msgArray(3) = String.Empty
                    msgArray(4) = String.Empty

                    '2012.03.19 修正START
                    ds = Me._Blc.SetComWarningM("W162", LMH030BLC.ATS_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)
                    '2012.03.19 修正END

                    flgWarning = True 'ワーニングフラグをたてて処理続行

                    Continue For

#End If
                Else
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If

                ' ''            ElseIf GetResultCount() > 1 Then
                ' ''#If False Then  'UPD 2020/01/17 010139【LMS】フィルメ誤出荷類似_EDI取込で標準機能使用_出荷登録で個別荷主機能使用
                ' ''                    If MyBase.GetResultCount = 1 Then
                ' ''                    Else
                ' ''                        ''再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                ' ''                        'msgArray(1) = String.Empty
                ' ''                        'msgArray(2) = String.Empty
                ' ''                        'msgArray(3) = String.Empty
                ' ''                        'msgArray(4) = String.Empty

                ' ''                        ''2012.03.19 修正START
                ' ''                        'ds = Me._Blc.SetComWarningM("W162", LMH030BLC.ATS_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)
                ' ''                        ''2012.03.19 修正END

                ' ''                        'flgWarning = True 'ワーニングフラグをたてて処理続行

                ' ''                        'Continue For
                ' ''                    End If

                ' ''#Else
                ' ''                '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                ' ''                msgArray(1) = String.Empty
                ' ''                msgArray(2) = String.Empty
                ' ''                msgArray(3) = String.Empty
                ' ''                msgArray(4) = String.Empty

                ' ''                '2012.03.19 修正START
                ' ''                ds = Me._Blc.SetComWarningM("W162", LMH030BLC.ATS_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)
                ' ''                '2012.03.19 修正END

                ' ''                flgWarning = True 'ワーニングフラグをたてて処理続行

                ' ''                Continue For

                ' ''#End If

                ' ''            End If

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
        Dim mZipJis As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediZip As String = String.Empty
        If String.IsNullOrEmpty(dtEdi.Rows(0)("DEST_ZIP").ToString()) = False Then
            dtEdi.Rows(0)("DEST_ZIP") = Replace(dtEdi.Rows(0)("DEST_ZIP").ToString(), "-", String.Empty)
            ediZip = dtEdi.Rows(0)("DEST_ZIP").ToString()
        End If

        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        Dim compareWarningFlg As Boolean = False

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
                'drEdiL("DEST_JIS_CD") = mZipJis
                warningString = "郵便番号マスタ"
            End If
        End If

        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.STD_WID_L004, 0)

        If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = workDestString
            msgArray(2) = "届先マスタ"
            msgArray(3) = "EDIデータ"
            If String.IsNullOrEmpty(mZipJis) = False Then
                msgArray(4) = String.Empty
            Else
                msgArray(4) = "※郵便番号からJISコードが特定できないため、出荷画面で運賃がゼロになる可能性があります。"
            End If


            ds = Me._Blc.SetComWarningL("W186", LMH030BLC.STD_WID_L004, ds, msgArray, workDestCd, String.Empty)

            compareWarningFlg = True

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
            drMD("FAX") = drEdiL("DEST_FAX").ToString()
            drMD("JIS") = mZipJis
            'EDIデータにも値をセットする
            drEdiL("DEST_JIS_CD") = mZipJis
            drMD("PICK_KB") = "01"
            drMD("BIN_KB") = "01"
            drMD("LARGE_CAR_YN") = "01"
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

#Region "届先マスタチェック"
    ''' <summary>
    ''' マスタ値とEDI出荷(大)の整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DestCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")
        Dim mSysDelF As String = dtMdest.Rows(0).Item("SYS_DEL_FLG").ToString()

        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtMZip As DataTable = ds.Tables("LMH030_M_ZIP")

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
        Dim ediDestNm As String = String.Concat(dtEdi.Rows(0)("FREE_C03").ToString(), dtEdi.Rows(0)("FREE_C25").ToString())
        Dim ediZip As String = dtEdi.Rows(0)("DEST_ZIP").ToString()
        Dim ediTel As String = dtEdi.Rows(0)("FREE_C26").ToString()
        Dim ediDestAd1 As String = dtEdi.Rows(0)("FREE_C24").ToString()
        'Dim ediDestAd2 As String = dtEdi.Rows(0)("DEST_AD_2").ToString()
        'Dim ediDestAd3 As String = dtEdi.Rows(0)("DEST_AD_3").ToString()
        'Dim ediDestAdAll As String = String.Concat(ediDestAd1, ediDestAd2, ediDestAd3)
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        Dim compareWarningFlg As Boolean = False

        '削除フラグ(届先マスタ)
        If mSysDelF.Equals("1") = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E331", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        mDestNm = Me.SpaceCutChk(mDestNm)
        ediDestNm = Me.SpaceCutChk(ediDestNm)

        '届先コードを使用する場合は整合性チェックは行わない
        If dtEdi.Rows(0).Item("FREE_C04").ToString().Equals("02") = False Then
            Return True
        End If

        '以後は直送先の場合のみにチェックを行う

        '届先名称(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediDestNm) = True Then
            'チェックなし
        ElseIf mDestNm.Equals(ediDestNm) = True Then
            'チェックなし
        Else
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.ATS_WID_L005, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "届先名称"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "届先名称"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty
                'ds = Me._Blc.SetComWarningL("W166", LMH030BLC.ATS_WID_L005, ds, msgArray, ediDestNm, mDestNm)
                ds = Me._Blc.SetComWarningL("W234", LMH030BLC.ATS_WID_L005, ds, msgArray, ediDestNm, mDestNm)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                ''ワーニングで"はい"を選択時
                'dtMdest.Rows(0).Item("DEST_NM") = String.Concat(dtEdi.Rows(0)("FREE_C03").ToString(), dtEdi.Rows(0)("FREE_C25").ToString())
                ''マスタ更新対象フラグ
                'dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            End If

        End If

        '届先住所(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediDestAd1) = True Then
            'チェックなし
        Else

            mAdAll = SpaceCutChk(mAdAll)
            ediDestAd1 = SpaceCutChk(ediDestAd1)
            If mAdAll.Equals(ediDestAd1) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.ATS_WID_L006, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先住所"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "住所"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    'ds = Me._Blc.SetComWarningL("W166", LMH030BLC.ATS_WID_L006, ds, msgArray, ediDestAd1, mAdAll)
                    ds = Me._Blc.SetComWarningL("W234", LMH030BLC.ATS_WID_L006, ds, msgArray, ediDestAd1, mAdAll)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    ''ワーニングで"はい"を選択時
                    'dtMdest.Rows(0).Item("AD_1") = dtEdi.Rows(0)("FREE_C24").ToString()
                    'dtMdest.Rows(0).Item("AD_2") = String.Empty
                    'dtMdest.Rows(0).Item("AD_3") = String.Empty
                    ''マスタ更新対象フラグ
                    'dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If
            End If

        End If

        ''届先郵便番号(マスタ値が完全一致でなければワーニング)
        'If String.IsNullOrEmpty(ediZip) = True Then
        '    'チェックなし
        'Else
        '    If mZip.Equals(Replace(ediZip, "-", String.Empty)) = False Then

        '        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.ATS_WID_L007, 0)

        '        If String.IsNullOrEmpty(choiceKb) = True Then

        '            msgArray(1) = "届先郵便番号"
        '            msgArray(2) = "届先マスタ"
        '            msgArray(3) = "郵便番号"
        '            msgArray(4) = "EDIデータ"
        '            msgArray(5) = String.Empty
        '            ds = Me._Blc.SetComWarningL("W166", LMH030BLC.ATS_WID_L007, ds, msgArray, ediZip, mZip)

        '            compareWarningFlg = True

        '        ElseIf choiceKb.Equals("01") = True Then
        '            'ワーニングで"はい"を選択時
        '            dtMdest.Rows(0).Item("ZIP") = dtEdi.Rows(0)("DEST_ZIP").ToString()
        '            'マスタ更新対象フラグ
        '            dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

        '        End If

        '    End If

        'End If


        '届先電話番号(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediTel) = True Then
            'チェックなし
        Else
            If (Replace(Replace(mTel, "-", String.Empty), Space(1), String.Empty)).Equals(Replace(Replace(ediTel, "-", String.Empty), Space(1), String.Empty)) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.ATS_WID_L008, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先電話番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "電話番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    'ds = Me._Blc.SetComWarningL("W166", LMH030BLC.ATS_WID_L008, ds, msgArray, ediTel, mTel)
                    ds = Me._Blc.SetComWarningL("W234", LMH030BLC.ATS_WID_L008, ds, msgArray, ediTel, mTel)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    ''ワーニングで"はい"を選択時
                    'dtMdest.Rows(0).Item("TEL") = dtEdi.Rows(0)("FREE_C26").ToString()
                    ''マスタ更新対象フラグ
                    'dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If

        '届先JISコード(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediDestJisCd) = True Then
            'チェックなしだが届先MのDEST_JISをセット
            dtEdi.Rows(0)("DEST_JIS_CD") = mJis
        Else
            If mJis.Equals(ediDestJisCd) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.ATS_WID_L009, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先JISコード"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "JISコード"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    'ds = Me._Blc.SetComWarningL("W166", LMH030BLC.ATS_WID_L009, ds, msgArray, ediDestJisCd, mJis)
                    ds = Me._Blc.SetComWarningL("W234", LMH030BLC.ATS_WID_L009, ds, msgArray, ediDestJisCd, mJis)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    ''ワーニングで"はい"を選択時
                    'dtMdest.Rows(0).Item("JIS") = dtEdi.Rows(0)("DEST_JIS_CD").ToString()
                    ''マスタ更新対象フラグ
                    'dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

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
        Dim mstFlg As String = String.Empty

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return choiceKb
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) Then

                'ワーニング画面の処理区分値を反映
                choiceKb = dr.Item("CHOICE_KB").ToString()

                mstFlg = warningId.Substring(7, 1)

                Select Case mstFlg
                    Case "2"
                        'ワーニング処理設定の値を反映
                        setDt.Rows(0).Item("DEST_CD") = dr.Item("MST_VALUE")
                    Case Else

                End Select

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
                'outkaDr("DEST_AD_3") = destMDr("AD_3")
                outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")
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
        Else
            'まとめ登録処理
            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt) + Convert.ToDouble(matomesakiDt.Rows(0)("OUTKA_PKG_NB"))
            outkaDr("SYS_UPD_DATE") = matomesakiDt.Rows(0)("SYS_UPD_DATE")
            outkaDr("SYS_UPD_TIME") = matomesakiDt.Rows(0)("SYS_UPD_TIME")

            '2014/04/08 (黎) まとめ処理時のオーダー番号まとめ --ST--
            outkaDr("CUST_ORD_NO") = Me._Blc.LeftB(String.Concat(matomesakiDt.Rows(0)("CUST_ORD_NO"), ",", ediDr("CUST_ORD_NO")), 30)
            '2014/04/08 (黎) まとめ処理時のオーダー番号まとめ --ED--

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
        '要望番号:1712 umano 2013.01.11 START
        'Private Function SetDatasetOutkaM(ByVal ds As DataSet) As DataSet
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
                sCustOrdNo = ediLDr("CUST_ORD_NO").ToString                 '自分のEDI出荷(大)を参照
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
                sBuyerOrdNo = ediLDr("BUYER_ORD_NO").ToString               '自分のEDI出荷(大)を参照
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

#Region "紐付け処理[使用していません]"
    ''' <summary>
    ''' 紐付け処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Himoduke(ByVal ds As DataSet) As DataSet

        '紐付けフラグの設定
        ds = Me.SetHimodukeFlg(ds)

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

#If True Then   '千葉アクタス　000(標準)よりコピー　ADD 2020/01/20 

#Region "Method(セミEDI)"
#Region "データセット設定"
#Region "セミEDI時　データセット設定(EDI出荷(大))"
    ''' <summary>
    ''' データセット設定(EDI出荷(大)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiOutkaEdiL(ByVal setDs As DataSet _
                                    , ByVal rowNo As Integer _
                                    , ByRef sEdiCtlNo As String _
                                    , ByRef iEdiCtlNoChu As Integer _
                                    , ByVal iFindOutkaEdiFlg As Integer _
                                    , ByVal itargetUpdFlg As Integer _
                                    , ByVal iCntDiffFlg As Integer _
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

        '削除区分
        If iFindOutkaEdiFlg = 1 Then
            drOutkaEdiL("DEL_KB") = "3"
            drOutkaEdiL("SYS_DEL_FLG") = "0"
            sKey = "L_DEL_KB_NO"
            If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
                If drSemiEdiInfo.Item("EDI_TORITERM_FLG").ToString().Equals("9") = True Then
                    If String.IsNullOrEmpty(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) = False Then
                        drOutkaEdiL.Item("DEL_KB") = "2"
                        drOutkaEdiL.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                    ElseIf iCntDiffFlg = 1 Then
                        drOutkaEdiL("DEL_KB") = "3"
                        drOutkaEdiL("SYS_DEL_FLG") = "0"
                    ElseIf itargetUpdFlg = 1 Then
                        drOutkaEdiL.Item("DEL_KB") = LMConst.FLG.ON
                        drOutkaEdiL.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                    End If
                End If

            ElseIf iCntDiffFlg = 1 Then
                drOutkaEdiL("DEL_KB") = "3"
                drOutkaEdiL("SYS_DEL_FLG") = "0"
            ElseIf itargetUpdFlg = 1 Then
                drOutkaEdiL.Item("DEL_KB") = LMConst.FLG.ON
                drOutkaEdiL.Item("SYS_DEL_FLG") = LMConst.FLG.ON

            End If

        Else
            sKey = "L_DEL_KB_NO"
            If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
                If drSemiEdiInfo.Item("EDI_TORITERM_FLG").ToString().Equals("9") = True Then
                    If String.IsNullOrEmpty(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) = False Then
                        drOutkaEdiL.Item("DEL_KB") = LMConst.FLG.ON
                        drOutkaEdiL.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                    Else
                        drOutkaEdiL.Item("DEL_KB") = LMConst.FLG.OFF
                        drOutkaEdiL.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                    End If
                Else
                    '2015.05.13 古川エージェンシー　修正START
                    If String.IsNullOrEmpty(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) = False Then
                        drOutkaEdiL.Item("DEL_KB") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
                        drOutkaEdiL.Item("SYS_DEL_FLG") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
                    Else
                        drOutkaEdiL.Item("DEL_KB") = LMConst.FLG.OFF
                        drOutkaEdiL.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                    End If
                    '2015.05.13 古川エージェンシー　修正END
                End If

            Else
                drOutkaEdiL.Item("DEL_KB") = LMConst.FLG.OFF
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
                '追加開始 --- 2015.03.18 
                If drOutkaEdiL("OUTKA_PLAN_DATE").ToString.Length > 8 Then drOutkaEdiL("OUTKA_PLAN_DATE") = Left(drOutkaEdiL("OUTKA_PLAN_DATE").ToString, 8)
                '追加終了 --- 2015.03.18 
            End If
        Else
            drOutkaEdiL("OUTKA_PLAN_DATE") = MyBase.GetSystemDate()
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
                '追加開始 --- 2015.03.18 
                If drOutkaEdiL("OUTKO_DATE").ToString.Length > 8 Then drOutkaEdiL("OUTKO_DATE") = Left(drOutkaEdiL("OUTKO_DATE").ToString, 8)
                '追加終了 --- 2015.03.18 
            End If
        Else
            drOutkaEdiL("OUTKO_DATE") = MyBase.GetSystemDate()
        End If

        '納入予定日
        sKey = "L_ARR_PLAN_DATE_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            If drRcvEdiDtl(sTempColNo).ToString().Trim().IndexOf("/") > 0 AndAlso _
               drRcvEdiDtl(sTempColNo).ToString().Trim().Length <= 10 Then
                Dim sDate As String = Convert.ToDateTime(drRcvEdiDtl(sTempColNo).ToString()).ToString("yyyy/MM/dd")
                Dim temStr As String() = sDate.Trim().Split("/"c)
                'Dim temStr As String() = drRcvEdiDtl(sTempColNo).ToString().Trim().Split("/"c)
                drOutkaEdiL("ARR_PLAN_DATE") = temStr(0).PadLeft(4, "0"c) & temStr(1).PadLeft(2, "0"c) & temStr(2).PadLeft(2, "0"c)
            Else
                drOutkaEdiL("ARR_PLAN_DATE") = drRcvEdiDtl(sTempColNo).ToString().Trim().Replace("/", "")
                '追加開始 --- 2015.03.18 
                If drOutkaEdiL("ARR_PLAN_DATE").ToString.Length > 8 Then drOutkaEdiL("ARR_PLAN_DATE") = Left(drOutkaEdiL("ARR_PLAN_DATE").ToString, 8)
                '追加終了 --- 2015.03.18 
            End If
        End If

        '2015.06.05 古川エージェンシー　修正対応START
        '納入予定時刻
        If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count > 0 Then
            drOutkaEdiL("ARR_PLAN_TIME") = "02"
        Else
            drOutkaEdiL("ARR_PLAN_TIME") = String.Empty
        End If
        '2015.06.05 古川エージェンシー　修正対応END

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
                '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
            ElseIf setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='98'").Count > 0 Then
                drOutkaEdiL("DEST_ZIP") = drRcvEdiDtl(sTempColNo).ToString().Trim()
                If InStr(drOutkaEdiL.Item("DEST_ZIP").ToString(), Space(1)) > 0 Then
                    drOutkaEdiL.Item("DEST_ZIP") = _Blc.LeftB(drOutkaEdiL.Item("DEST_ZIP").ToString(), InStr(drOutkaEdiL.Item("DEST_ZIP").ToString(), Space(1))).Replace("〒", String.Empty)

                    '2015.05.25 千葉・Ｍ・Ｒ・Ｃデュポン対応 追加START
                Else
                    drOutkaEdiL.Item("DEST_ZIP") = _Blc.LeftB(drOutkaEdiL.Item("DEST_ZIP").ToString(), 10).Replace("〒", String.Empty)
                    '2015.05.25 千葉・Ｍ・Ｒ・Ｃデュポン対応 追加END

                End If
                '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 END
            Else
                drOutkaEdiL("DEST_ZIP") = drRcvEdiDtl(sTempColNo).ToString().Trim()
            End If

        End If

        '届先住所1
        sKey = "L_DEST_AD_1_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            If ("COLUMN_MST").Equals(sTempColNo) Then
                If setDs.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                    drOutkaEdiL("DEST_AD_1") = setDs.Tables("LMH030_M_DEST").Rows(0).Item("AD_1").ToString
                End If
                '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
            ElseIf setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='98'").Count > 0 Then
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
                '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 END

            Else
                drOutkaEdiL("DEST_AD_1") = drRcvEdiDtl(sTempColNo).ToString().Trim()
            End If
        End If

        '届先住所2
        sKey = "L_DEST_AD_2_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            If ("COLUMN_MST").Equals(sTempColNo) Then
                If setDs.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                    drOutkaEdiL("DEST_AD_2") = setDs.Tables("LMH030_M_DEST").Rows(0).Item("AD_2").ToString
                End If
                '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
            ElseIf setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='98'").Count > 0 Then
                drOutkaEdiL("DEST_AD_2") = drRcvEdiDtl(sTempColNo).ToString().Trim()
                If InStr(drOutkaEdiL.Item("DEST_AD_2").ToString(), "　") > 0 Then
                    drOutkaEdiL.Item("DEST_AD_2") = _Blc.LeftB(drOutkaEdiL.Item("DEST_AD_2").ToString().Substring(InStr(drOutkaEdiL("DEST_AD_2").ToString(), "　")), 40)
                Else
                    drOutkaEdiL.Item("DEST_AD_2") = String.Empty
                End If
                '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 END
            Else
                drOutkaEdiL("DEST_AD_2") = drRcvEdiDtl(sTempColNo).ToString().Trim()
            End If
        End If

        '届先住所3
        sKey = "L_DEST_AD_3_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then

            If ("COLUMN_MST").Equals(sTempColNo) Then
                If setDs.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                    drOutkaEdiL("DEST_AD_3") = setDs.Tables("LMH030_M_DEST").Rows(0).Item("AD_3").ToString
                End If
                '2015.03.23 横浜・古川エージェンシー対応 START
            ElseIf setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count = 0 Then
                drOutkaEdiL("DEST_AD_3") = drRcvEdiDtl(sTempColNo).ToString().Trim()
            ElseIf drOutkaEdiL("DEST_AD_1").ToString().Equals(drRcvEdiDtl(sTempColNo).ToString().Trim()) = False Then
                drOutkaEdiL("DEST_AD_3") = drRcvEdiDtl(sTempColNo).ToString().Trim()
            End If
            '2015.03.23 横浜・古川エージェンシー対応 END

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

            '20170802古川エージェンシー変更対応 コメントアウト START
            ''2015.03.23 横浜・古川エージェンシー対応 START
            'If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count > 0 Then
            '    '2015.05.26 横浜・古川エージェンシー対応 修正START
            '    Dim strBuyerOrdNo As String = drRcvEdiDtl(sTempColNo).ToString()
            '    Dim lenB As Integer = System.Text.Encoding.GetEncoding(932).GetByteCount(strBuyerOrdNo)
            '    '2015.06.08 横浜・古川エージェンシー対応 修正START
            '    If InStr(strBuyerOrdNo, Space(1)) = 0 AndAlso lenB < 6 Then
            '        drOutkaEdiL.Item("BUYER_ORD_NO") = String.Empty
            '    ElseIf InStr(strBuyerOrdNo, Space(1)) = 0 AndAlso lenB = 6 Then
            '        drOutkaEdiL.Item("BUYER_ORD_NO") = String.Empty
            '    Else
            '        drOutkaEdiL.Item("BUYER_ORD_NO") = RTrim(_Blc.LeftB(_Blc.LeftB(strBuyerOrdNo, lenB - 6), 30))
            '    End If
            '    '2015.06.08 横浜・古川エージェンシー対応 修正END
            'Else
            drOutkaEdiL("BUYER_ORD_NO") = _Blc.LeftB(drRcvEdiDtl(sTempColNo).ToString().Trim(), 30)
            'End If
            ''2015.03.23 横浜・古川エージェンシー対応 START
            '20170802古川エージェンシー変更対応 コメントアウト END

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
            '2015.05.13 MRCデュポン対応 修正START
            If ("COLUMN_MST").Equals(sTempColNo) Then
                If setDs.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                    drOutkaEdiL("REMARK") = setDs.Tables("LMH030_M_DEST").Rows(0).Item("REMARK").ToString
                End If
            Else
                drOutkaEdiL("REMARK") = drRcvEdiDtl(sTempColNo).ToString().Trim()
            End If
            '2015.05.13 MRCデュポン対応 修正END
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
            '2015.03.23 横浜・古川エージェンシー対応 START
            If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count > 0 Then
                drOutkaEdiL.Item("FREE_C02") = Right(drOutkaEdiL.Item("FREE_C02").ToString(), 6)
            End If
            '2015.03.23 横浜・古川エージェンシー対応 END
        End If

        '文字列3
        sKey = "L_FREE_C03_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            drOutkaEdiL("FREE_C03") = drRcvEdiDtl(sTempColNo).ToString().Trim()
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
    Private Function SetSemiOutkaEdiM(ByVal setDs As DataSet _
                                    , ByVal rowNo As Integer _
                                    , ByRef sEdiCtlNo As String _
                                    , ByRef iEdiCtlNoChu As Integer _
                                    , ByVal iFindOutkaEdiFlg As Integer _
                                    , ByVal itargetUpdFlg As Integer _
                                    , ByVal iCntDiffFlg As Integer _
                                     ) As DataSet

        Dim drOutkaEdiM As DataRow = setDs.Tables("LMH030_OUTKAEDI_M").NewRow()
        Dim drRcvEdiDtl As DataRow = setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(rowNo)
        Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        Dim dtMGoods As DataTable = setDs.Tables("LMH030_M_GOODS")
        Dim drMgoods As DataRow = Nothing

#If False Then   'ADD 2019/12/12 【LMS】フィルメEDI出荷登録_数量10倍誤出荷理由調査

                'TODO;複数存在した場合は(一番最初)
        If dtMGoods.Rows.Count > 0 Then
            drMgoods = dtMGoods.Rows(0)
        End If

#Else
        If dtMGoods.Rows.Count > 1 Then
            '商品複数ありフラグセット
            drOutkaEdiM.Item("GOODSM_FUKAKUTEI_FLG") = "1"
        Else
            drOutkaEdiM.Item("GOODSM_FUKAKUTEI_FLG") = "0"
        End If

        'TODO;複数存在した場合は(一番最初)
        If dtMGoods.Rows.Count > 0 Then
            drMgoods = dtMGoods.Rows(0)
        End If


#End If

        Dim sTempColNo As String = String.Empty
        Dim sKey As String = String.Empty

        '削除区分
        If iFindOutkaEdiFlg = 1 Then
            drOutkaEdiM.Item("DEL_KB") = "3"
            drOutkaEdiM.Item("SYS_DEL_FLG") = "0"
            sKey = "M_DEL_KB_NO"
            If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
                If drSemiEdiInfo.Item("EDI_TORITERM_FLG").ToString().Equals("9") = True Then
                    If String.IsNullOrEmpty(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) = False Then
                        drOutkaEdiM.Item("DEL_KB") = "2"
                        drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                    ElseIf iCntDiffFlg = 1 Then
                        drOutkaEdiM.Item("DEL_KB") = "3"
                        drOutkaEdiM.Item("SYS_DEL_FLG") = "0"
                    ElseIf itargetUpdFlg = 1 Then
                        drOutkaEdiM.Item("DEL_KB") = LMConst.FLG.ON
                        drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                    End If
                End If

            ElseIf iCntDiffFlg = 1 Then
                drOutkaEdiM.Item("DEL_KB") = "3"
                drOutkaEdiM.Item("SYS_DEL_FLG") = "0"
            ElseIf itargetUpdFlg = 1 Then
                drOutkaEdiM.Item("DEL_KB") = LMConst.FLG.ON
                drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.ON

            End If
        Else
            sKey = "M_DEL_KB_NO"
            If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
                If drSemiEdiInfo.Item("EDI_TORITERM_FLG").ToString().Equals("9") = True Then
                    If String.IsNullOrEmpty(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) = False Then
                        drOutkaEdiM.Item("DEL_KB") = LMConst.FLG.ON
                        drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                    Else
                        drOutkaEdiM.Item("DEL_KB") = LMConst.FLG.OFF
                        drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                    End If
                Else
                    '2015.05.13 古川エージェンシー　修正START
                    If String.IsNullOrEmpty(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) = False Then
                        drOutkaEdiM.Item("DEL_KB") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
                        drOutkaEdiM.Item("SYS_DEL_FLG") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
                    Else
                        drOutkaEdiM.Item("DEL_KB") = LMConst.FLG.OFF
                        drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                    End If
                    '2015.05.13 古川エージェンシー　修正END
                End If

            Else
                drOutkaEdiM.Item("DEL_KB") = LMConst.FLG.OFF
                drOutkaEdiM.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

            End If
        End If

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
        If dtMGoods.Rows.Count = 1 _
            AndAlso String.IsNullOrEmpty(drMgoods.Item("GOODS_CD_NRS").ToString) = False Then
            drOutkaEdiM("CUST_GOODS_CD") = drMgoods.Item("GOODS_CD_CUST").ToString
        Else
            sKey = "M_CUST_GOODS_CD_NO"
            If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
                drOutkaEdiM.Item("CUST_GOODS_CD") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()

                '追加開始 --- 2015.03.17
                '商品コードの前nケタをカット
                Dim drCustMst As DataRow = Nothing
                Dim cutCount As Integer = 0
                If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='96'").Count > 0 Then
                    drCustMst = setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='96'")(0)
                    cutCount = CType(drCustMst.Item("SET_NAIYO").ToString(), Integer)
                    drOutkaEdiM.Item("CUST_GOODS_CD") = drOutkaEdiM.Item("CUST_GOODS_CD").ToString.Substring(cutCount)

                    '2015.06.25 横浜・古川エージェンシー対応 START
                ElseIf setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count > 0 Then
                    Dim strGoodsCdCust As String = drOutkaEdiM.Item("CUST_GOODS_CD").ToString()
                    Dim lenB As Integer = System.Text.Encoding.GetEncoding(932).GetByteCount(strGoodsCdCust)
                    If lenB > 20 Then
                        drOutkaEdiM.Item("CUST_GOODS_CD") = _Blc.LeftB(drOutkaEdiM.Item("CUST_GOODS_CD").ToString(), 20)
                    End If
                    '2015.06.25 横浜・古川エージェンシー対応 END

                    '2015.04.24 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
                ElseIf setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='98'").Count > 0 Then

                    '2015.05.21 千葉・Ｍ・Ｒ・Ｃデュポン対応 修正START　前6byteと"-"(ハイフン)以降の文字列を取得するように変更
                    'drOutkaEdiM.Item("CUST_GOODS_CD") = String.Concat(_Blc.LeftB(drOutkaEdiM.Item("CUST_GOODS_CD").ToString(), 6), Space(1), Right(drOutkaEdiM.Item("CUST_GOODS_CD").ToString(), 3))
                    drOutkaEdiM.Item("CUST_GOODS_CD") = String.Concat(_Blc.LeftB(drOutkaEdiM.Item("CUST_GOODS_CD").ToString(), 6), Space(1), _
                                                                      Right(drOutkaEdiM.Item("CUST_GOODS_CD").ToString(), _
                                                                      Len(drOutkaEdiM.Item("CUST_GOODS_CD").ToString()) - InStr(drOutkaEdiM.Item("CUST_GOODS_CD").ToString(), "-") + 1))
                    '2015.04.24 千葉・Ｍ・Ｒ・Ｃデュポン対応 END
                    '2015.05.21 千葉・Ｍ・Ｒ・Ｃデュポン対応 修正END


                End If
                '2015.03.23 横浜・古川エージェンシー対応 END
                '追加終了 --- 2015.03.17
            End If

        End If

        '商品キー
        '2015.05.13 MRCデュポン対応 修正START
        If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='98'").Count > 0 Then
            If dtMGoods.Rows.Count > 0 Then
                drOutkaEdiM("NRS_GOODS_CD") = dtMGoods.Rows(0).Item("GOODS_CD_NRS").ToString
            End If
        Else
            drOutkaEdiM.Item("NRS_GOODS_CD") = String.Empty
        End If
        '2015.05.13 MRCデュポン対応 修正START

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
        '2015.03.23 横浜・古川エージェンシー対応 START
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
            Dim preIrime As String = String.Empty
            preIrime = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
            If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count > 0 Then
                Dim conSnum As String = String.Empty
                For i As Integer = 0 To preIrime.Length - 1
                    '2015.06.05 横浜・古川エージェンシー対応 修正START
                    If IsNumeric(preIrime.Substring(i, 1)) = True OrElse preIrime.Substring(i, 1).Equals(".") = True Then
                        conSnum = String.Concat(conSnum, preIrime.Substring(i, 1))
                    End If
                Next
                If Convert.ToDecimal(conSnum) > 0 Then
                    '2015.06.05 横浜・古川エージェンシー対応 修正END
                    drOutkaEdiM("IRIME") = conSnum
                End If

            ElseIf Convert.ToInt32(drRcvEdiDtl.Item(sTempColNo)) > 0 Then
                drOutkaEdiM("IRIME") = preIrime
            End If
            '2015.03.23 横浜・古川エージェンシー対応 END
        Else
            If drMgoods Is Nothing OrElse String.IsNullOrEmpty(drMgoods.Item("STD_IRIME_NB").ToString()) = True _
                 OrElse dtMGoods.Rows.Count > 1 Then
                drOutkaEdiM("IRIME") = 0
            Else
                drOutkaEdiM("IRIME") = drMgoods.Item("STD_IRIME_NB").ToString()
            End If
        End If

        '引当単位区分
        '2015.03.23 横浜・古川エージェンシー対応 START
        If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count > 0 Then
            sKey = "M_IRIME_UT_NO"
            If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then
                '先方荷姿(区分マスタ)
                Dim drJudge As DataRow = setDs.Tables("LMH030_JUDGE").NewRow()
                drJudge("KBN_GROUP_CD") = "D026"
                drJudge("KBN_CD") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim().ToLower()
                setDs.Tables("LMH030_JUDGE").Rows.Add(drJudge)

                setDs = MyBase.CallDAC(Me._Dac, "SelectDataZkbn", setDs)

                If MyBase.GetResultCount = 1 Then
                    drOutkaEdiM.Item("ALCTD_KB") = setDs.Tables("LMH030_Z_KBN").Rows(0).Item("NISUGATA").ToString()
                Else
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E454", New String() {"引当単位が特定できない", "処理", String.Concat("単位", drRcvEdiDtl.Item(sTempColNo).ToString().Trim())}, Convert.ToString(rowNo), LMH030BLC.EXCEL_COLTITLE, sEdiCtlNo)
                    Return setDs
                End If
            End If

        Else
            drOutkaEdiM.Item("ALCTD_KB") = drSemiEdiInfo.Item("M_DEF_ALCTD_KB").ToString().ToString().Trim()
        End If
        '2015.03.23 横浜・古川エージェンシー対応 END


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

            '荷主明細マスタ（SUB_KB=87の取得:受信個数が、端数の場合：千葉アクタス）
            Call MyBase.CallDAC(Me._Dac, "SelectMstCustD87", setDs)
            If MyBase.GetResultCount > 0 Then
                drOutkaEdiM("OUTKA_PKG_NB") = 0
                drOutkaEdiM.Item("OUTKA_HASU") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
            Else

                If drOutkaEdiM.Item("ALCTD_KB").Equals("01") Then
                    '①梱数計算FLGがONならば計算
                    '追加開始 --- 2015.04.06 (梱数計算必要な荷主が存在するため)
                    If drSemiEdiInfo.Item("DTL_OUTKAPKGNB_CALC_FLG").ToString().Equals(LMConst.FLG.ON) Then
                        '2018/10/12 要望管理002468対応 START
                        If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='9M'").Count > 0 AndAlso _
                           Convert.ToDecimal(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) - System.Math.Floor(Convert.ToDecimal(drRcvEdiDtl.Item(sTempColNo).ToString().Trim())) <> 0 _
                          OrElse (dtMGoods.Rows.Count > 1) Then
                            drOutkaEdiM("OUTKA_PKG_NB") = 0
                        Else
                            '2018/10/12 要望管理002468対応 END
                            drOutkaEdiM("OUTKA_PKG_NB") = Convert.ToInt32(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) \ Convert.ToInt32(drMgoods.Item("PKG_NB"))
                        End If
                    Else
                        drOutkaEdiM("OUTKA_PKG_NB") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
                    End If
                    '追加終了 --- 2015.04.06
                Else
                    '②マスタ標準入目でDIVIDE
                    If drMgoods Is Nothing OrElse String.IsNullOrEmpty(drMgoods.Item("STD_IRIME_NB").ToString()) = True _
                        OrElse dtMGoods.Rows.Count > 1 Then
                        drOutkaEdiM("OUTKA_PKG_NB") = 0
                        '2015.03.23 横浜・古川エージェンシー対応 START
                        If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count > 0 AndAlso Convert.ToDecimal(drOutkaEdiM("IRIME")) > 0 Then
                            drOutkaEdiM("OUTKA_PKG_NB") = Convert.ToDecimal(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) / Convert.ToDecimal(drOutkaEdiM("IRIME"))
                        End If
                        '2015.03.23 横浜・古川エージェンシー対応 END
                    Else
                        drOutkaEdiM("OUTKA_PKG_NB") = Convert.ToDecimal(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) / Convert.ToDecimal(drMgoods.Item("STD_IRIME_NB"))
                    End If
                End If
                '出荷端数
                '追加開始 --- 2015.04.09 (端数計算必要な荷主が存在するため)
                If drSemiEdiInfo.Item("DTL_OUTKAPKGNB_CALC_FLG").ToString().Equals(LMConst.FLG.ON) Then
                    '2018/10/12 要望管理002468対応 START
                    If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='9M'").Count > 0 AndAlso _
                       Convert.ToDecimal(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) - System.Math.Floor(Convert.ToDecimal(drRcvEdiDtl.Item(sTempColNo).ToString().Trim())) <> 0 _
                       OrElse (dtMGoods.Rows.Count > 1) Then
                        drOutkaEdiM.Item("OUTKA_HASU") = 0
                    Else
                        '2018/10/12 要望管理002468対応 END
                        drOutkaEdiM.Item("OUTKA_HASU") = Convert.ToInt32(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) Mod Convert.ToInt32(drMgoods.Item("PKG_NB"))
                    End If
                Else
                    drOutkaEdiM.Item("OUTKA_HASU") = 0
                End If
                '追加終了 --- 2015.04.09
            End If


        End If

        '出荷数量 << INFOテーブルにない
        'sKey = "M_OUTKA_TTL_NB_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then

            If drOutkaEdiM.Item("ALCTD_KB").Equals("01") Then
                '①×マスタ標準入目
                If drMgoods Is Nothing OrElse String.IsNullOrEmpty(drMgoods.Item("STD_IRIME_NB").ToString()) = True _
                    OrElse dtMGoods.Rows.Count > 1 Then
                    drOutkaEdiM("OUTKA_QT") = 0
                    '2015.03.23 横浜・古川エージェンシー対応 START
                    If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count > 0 AndAlso Convert.ToDecimal(drOutkaEdiM("IRIME")) > 0 Then
                        drOutkaEdiM("OUTKA_QT") = Convert.ToDecimal(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) * Convert.ToDecimal(drOutkaEdiM("IRIME"))
                    End If
                    '2015.03.23 横浜・古川エージェンシー対応 END
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
                '2018/10/12 要望管理002468対応 START
                If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='9M'").Count > 0 AndAlso _
                   Convert.ToDecimal(drOutkaEdiM("OUTKA_QT")) / Convert.ToDecimal(drOutkaEdiM("IRIME")) - System.Math.Floor(Convert.ToDecimal(drOutkaEdiM("OUTKA_QT")) / Convert.ToDecimal(drOutkaEdiM("IRIME"))) <> 0  Then
                    drOutkaEdiM("OUTKA_TTL_NB") = 0

                Else
                    '2018/10/12 要望管理002468対応 END

                    '①そのまま
                    drOutkaEdiM("OUTKA_TTL_NB") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
                End If
            Else
                '②マスタ標準入目でDIVIDE
                If drMgoods Is Nothing OrElse String.IsNullOrEmpty(drMgoods.Item("STD_IRIME_NB").ToString()) = True _
                    OrElse (dtMGoods.Rows.Count > 1) Then
                    drOutkaEdiM("OUTKA_TTL_NB") = 0
                    '2015.03.23 横浜・古川エージェンシー対応 START
                    If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count > 0 AndAlso Convert.ToDecimal(drOutkaEdiM("IRIME")) > 0 Then
                        drOutkaEdiM("OUTKA_TTL_NB") = Convert.ToDecimal(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) / Convert.ToDecimal(drOutkaEdiM("IRIME"))
                    End If
                    '2015.03.23 横浜・古川エージェンシー対応 END
                Else
                    drOutkaEdiM("OUTKA_TTL_NB") = Convert.ToDecimal(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) / Convert.ToDecimal(drMgoods.Item("STD_IRIME_NB"))
                End If
            End If

        End If

        '出荷総数量
        'sKey = "M_OUTKA_TTL_NB_NO"
        If isNullColKey(drSemiEdiInfo, sKey, sTempColNo) <> False Then

            'If drOutkaEdiM.Item("ALCTD_KB").Equals("01") Then
            If drOutkaEdiM.Item("ALCTD_KB").Equals("02") Then
                '①そのまま
                drOutkaEdiM("OUTKA_TTL_QT") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
            Else
                '②×マスタ標準入目
                If drMgoods Is Nothing OrElse String.IsNullOrEmpty(drMgoods.Item("STD_IRIME_NB").ToString()) = True _
                    OrElse (dtMGoods.Rows.Count > 1) Then
                    drOutkaEdiM("OUTKA_TTL_QT") = 0
                    '2015.03.23 横浜・古川エージェンシー対応 START
                    If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count > 0 AndAlso Convert.ToDecimal(drOutkaEdiM("IRIME")) > 0 Then
                        drOutkaEdiM("OUTKA_TTL_QT") = Convert.ToDecimal(drRcvEdiDtl.Item(sTempColNo).ToString().Trim()) * Convert.ToDecimal(drOutkaEdiM("IRIME"))
                    End If
                    '2015.03.23 横浜・古川エージェンシー対応 END
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
        '2015.06.08 横浜・古川エージェンシー対応 START
        If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count > 0 AndAlso _
           drMgoods Is Nothing = False AndAlso dtMGoods.Rows.Count = 1 Then

            drOutkaEdiM.Item("PKG_NB") = drMgoods.Item("PKG_NB").ToString()

        Else
            drOutkaEdiM.Item("PKG_NB") = String.Empty
        End If
        '2015.06.08 横浜・古川エージェンシー対応 END

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

            '2015.05.14 横浜・古川エージェンシー対応 修正START
            If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count > 0 Then
                drOutkaEdiM.Item("IRIME_UT") = String.Empty
            Else
                drOutkaEdiM.Item("IRIME_UT") = drRcvEdiDtl.Item(sTempColNo).ToString().Trim()
            End If
            '2015.05.14 横浜・古川エージェンシー対応 修正END

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

#If False Then  'UPD 2020/02/04　010140
        drOutkaEdiM("FREE_C04") = String.Empty

#Else
        drOutkaEdiM("FREE_C04") = drRcvEdiDtl("COLUMN_101").ToString          '変換前商品CD

#End If
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

        'ADD 2016/07/19 横浜　フィルメ対応 Start
        If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='0L'").Count > 0 Then
            Dim goodsNM2 As String = CStr(IIf(Len(drOutkaEdiM("FREE_C03").ToString) = 0, "", " " & drOutkaEdiM("FREE_C03").ToString))
            Dim goodsNM3 As String = CStr(IIf(Len(drOutkaEdiM("FREE_C24").ToString) = 0, "", " " & drOutkaEdiM("FREE_C24").ToString))
            Dim goodsNM As String = String.Concat(drOutkaEdiM("FREE_C02").ToString.Trim, goodsNM2, goodsNM3)
            '新名称を半角変換後FREE_C04に設定する
            drOutkaEdiM("FREE_C04") = StrConv(goodsNM, VbStrConv.Narrow)    '全角　⇒　半角対応
        End If
        'ADD 2016/07/19 横浜　フィルメ対応 eND

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_M").Rows.Add(drOutkaEdiM)

        Return setDs

    End Function

#End Region

#Region "セミEDI時  データセット設定(EDI管理番号(大・中))"
    ''' <summary>
    ''' データセット設定(EDI管理番号(大・中)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdiCtlNo(ByVal ds As DataSet _
                               , ByVal bSameKeyFlg As Boolean _
                               , ByRef sEdiCtlNo As String _
                               , ByRef iEdiCtlNoChu As Integer
                                ) As DataSet

        Dim sNrsBrCd As String = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("NRS_BR_CD").ToString()

        '前行とキーが異なる場合
        If bSameKeyFlg = False Then
            iEdiCtlNoChu = 0        '０クリア    
        End If

        'EDI管理番号(中)をカウントアップ
        iEdiCtlNoChu = Convert.ToInt32((iEdiCtlNoChu + 1).ToString("000"))

        If bSameKeyFlg = False Then
            '前行とキーが異なる場合　
            'EDI管理番号(大)を新規採番してEDI管理番号(中)を"001"採番
            Dim num As New NumberMasterUtility
            sEdiCtlNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_OUTKA_NO_L, Me, sNrsBrCd)
        End If

        Return ds

    End Function

#End Region

#Region "セミEDI時  データセット設定(商品マスタ)"
    ''' <summary>
    ''' 読み込み・存在用INデータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInDataSelectGoodsMst(ByRef setDs As DataSet, ByVal rowCnt As Integer)

        Dim setDt As DataTable = setDs.Tables("LMH030_M_GOODS")
        Dim setDr As DataRow = setDt.NewRow
        Dim drSemiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        Dim drEdiDtl As DataRow = setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(rowCnt)
        Dim sTempColNo As String = String.Empty
        Dim sKey As String = String.Empty

        'INデータ作成前にテーブル内一掃
        setDt.Clear()

        'INデータ設定
        setDr.Item("NRS_BR_CD") = drSemiInfo.Item("NRS_BR_CD").ToString()
        setDr.Item("CUST_CD_L") = drSemiInfo.Item("CUST_CD_L").ToString()
        setDr.Item("CUST_CD_M") = drSemiInfo.Item("CUST_CD_M").ToString()

        'カラム指定INデータ
        '荷主商品コード
        sKey = "M_CUST_GOODS_CD_NO"
        If isNullColKey(drSemiInfo, sKey, sTempColNo) <> False Then
            setDr.Item("GOODS_CD_CUST") = drEdiDtl.Item(sTempColNo).ToString().Trim()

            '追加開始 --- 2015.03.17
            '商品コードの前nケタをカット
            Dim drCustMst As DataRow = Nothing
            Dim cutCount As Integer = 0
            If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='96'").Count > 0 Then
                drCustMst = setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='96'")(0)
                cutCount = CType(drCustMst.Item("SET_NAIYO").ToString(), Integer)
                setDr.Item("GOODS_CD_CUST") = setDr.Item("GOODS_CD_CUST").ToString.Substring(cutCount)

                '2015.06.25 横浜・古川エージェンシー対応 START
            ElseIf setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='97'").Count > 0 Then
                Dim strGoodsCdCust As String = setDr.Item("GOODS_CD_CUST").ToString()
                Dim lenB As Integer = System.Text.Encoding.GetEncoding(932).GetByteCount(strGoodsCdCust)
                If lenB > 20 Then
                    setDr.Item("GOODS_CD_CUST") = _Blc.LeftB(setDr.Item("GOODS_CD_CUST").ToString(), 20)
                End If
                '2015.06.25 横浜・古川エージェンシー対応 END

            ElseIf setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='98'").Count > 0 Then
                '2015.05.21 千葉・Ｍ・Ｒ・Ｃデュポン対応 修正START　前6byteと"-"(ハイフン)以降の文字列を取得するように変更
                'setDr.Item("GOODS_CD_CUST") = String.Concat(_Blc.LeftB(setDr.Item("GOODS_CD_CUST").ToString(), 6), Space(1), Right(setDr.Item("GOODS_CD_CUST").ToString(), 3))
                setDr.Item("GOODS_CD_CUST") = String.Concat(_Blc.LeftB(setDr.Item("GOODS_CD_CUST").ToString(), 6), Space(1), _
                                                                  Right(setDr.Item("GOODS_CD_CUST").ToString(), _
                                                                  Len(setDr.Item("GOODS_CD_CUST").ToString()) - InStr(setDr.Item("GOODS_CD_CUST").ToString(), "-") + 1))

                '2015.05.21 千葉・Ｍ・Ｒ・Ｃデュポン対応 修正END

                '2016/07/20 横浜フィルメ対応 START
            ElseIf setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='0L'").Count > 0 Then
#If False Then ' フィルメニッヒ セミEDI対応  20160912 changed inoue
                Dim dblIrime As Double = CDbl(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(rowCnt).Item("COLUMN_14").ToString)
                setDr.Item("STD_IRIME_NB") = dblIrime
#Else
                setDr.Item("STD_IRIME_NB") = Me.CalcIrime(drEdiDtl, drSemiInfo)
#End If
                '2016/07/20 横浜フィルメ対応 END
            End If
            '追加終了 --- 2015.03.17

        End If

        'Add処理
        setDt.Rows.Add(setDr)

    End Sub

#End Region

#Region "セミEDI時  データセット設定(届先マスタ)"
    ''' <summary>
    ''' 届先マスタ読み込み・存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInDataSelectDestMst(ByRef setDs As DataSet, ByVal rowCnt As Integer)

        Dim setDt As DataTable = setDs.Tables("LMH030_M_DEST")
        Dim setDr As DataRow = setDt.NewRow
        Dim drSemiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        Dim drEdiDtl As DataRow = setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(rowCnt)
        Dim sTempColNo As String = String.Empty
        Dim sKey As String = String.Empty

        'INデータ作成前にテーブル内一掃
        setDt.Clear()

        'INデータ設定
        setDr.Item("NRS_BR_CD") = drSemiInfo.Item("NRS_BR_CD").ToString()
        setDr.Item("CUST_CD_L") = drSemiInfo.Item("CUST_CD_L").ToString()

        'カラム指定INデータ
        '荷主商品コード
        sKey = "L_DEST_CD_NO"
        If isNullColKey(drSemiInfo, sKey, sTempColNo) <> False Then
            setDr.Item("DEST_CD") = drEdiDtl.Item(sTempColNo).ToString().Trim()
        End If

        'Add処理
        setDt.Rows.Add(setDr)

    End Sub

#End Region

#Region "セミEDI時 データセット設定(キャンセル対象データ抽出)"
    ''' <summary>
    ''' キャンセルデータの抽出用INデータ設定
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <param name="RowNo"></param>
    ''' <remarks></remarks>
    Private Sub SetDatasetSelectCancelData(ByRef setDs As DataSet, ByVal RowNo As Integer)

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
        'CUST_ORD_NO
        If isNullColKey(drSemiInfo, "L_DEST_CUST_ORD_NO", tempColNo) Then
            setInOutDr.Item("CUST_ORD_NO") = drItems.Item(tempColNo).ToString().Trim()

        End If

        'Add処理
        setInOutDt.Rows.Add(setInOutDr)

    End Sub

#End Region

#Region "画面取込(セミEDI)データセット＋更新処理"
    ''' <summary>
    ''' 画面取込(セミEDI)データセット＋更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomi(ByVal ds As DataSet) As DataSet

        Dim dtSemiInfo As DataTable = ds.Tables("LMH030_SEMIEDI_INFO")       'セミEDI情報(TBL)
        Dim drSemiInfo As DataRow = dtSemiInfo.Rows(0) 'セミEDI情報(ROW)

        Dim dtSetHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")     '取込HED
        Dim dtSetDtl As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")     '取込DTL
        Dim dtSetRet As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_RET")     '処理件数

        Dim dtRcvHed As DataTable = Nothing                                  'EDI受信HED             (荷主により可変)Nothing有
        Dim dtRcvDtl As DataTable = Nothing                                  'EDI受信DTL             (荷主により可変)Nothing有
        Dim dtRcvEx As DataTable = Nothing                                   'EDI受信特殊            (荷主により可変)Nothing有
        Dim dtRcvDtlCancel As DataTable = Nothing                            'EDI受信キャンセルデータ(荷主により可変)Nothing有

        Dim sKey As String = String.Empty                                    'キー項目
        Dim sTempColNo As String = String.Empty                              'カラム格納番地

        Dim iSetDtlMax As Integer = dtSetDtl.Rows.Count - 1

        Dim sWhcd As String = String.Empty                                   '倉庫コード 
        Dim sCustCdL As String = String.Empty                                '荷主コード大
        Dim sCustCdM As String = String.Empty                                '荷主コード中
        Dim sNrsGoodsCd As String = String.Empty                             '日陸商品コード
        Dim sNrsGoodsNm As String = String.Empty                             '日陸商品名
        Dim sIrime As String = String.Empty                                  '入目

        Dim iAkakuroVal As Integer = 0                                       '赤黒値    (0:黒、1:赤)         

        Dim iSkipFlg As Integer = 0                                          'スキップフラグ     (0:EDI出荷に登録する、  1:EDI出荷に登録しない)
        Dim iDeleteFlg As Integer = 0                                        '取消フラグ         (0:EDI出荷を削除しない、1:EDI出荷を削除する)
        Dim iFindOutkaEdiFlg As Integer = 0                                  '削除対象EDI出荷データ存在フラグ (0:存在しない、1:存在する)

        Dim sNowKey As String = String.Empty                                 'キー項目（Temp用）
        Dim sOldKey As String = String.Empty                                 'キー項目（前行）
        Dim sNewKey As String = String.Empty                                 'キー項目（現在行）
        Dim bSameKeyFlg As Boolean = False                                   '前行とキーが同じ場合True、異なる場合False

        Dim arrKey As ArrayList = New ArrayList                              '番号格納用アレイリスト

        Dim sEdiCtlNo As String = String.Empty                               'EDI管理番号
        Dim iEdiCtlNoChu As Integer = 0                                      'EDI管理番号（中）

        Dim iRcvDtlInsCnt As Integer = 0                                     '書込件数（受信DTL）
        Dim iOutHedInsCnt As Integer = 0                                     '書込件数（出荷EDI(大)）
        Dim iOutDtlInsCnt As Integer = 0                                     '書込件数（出荷EDI(中)）
        Dim iRcvDtlCanCnt As Integer = 0                                     '取消件数（受信DTL[キャンセルデータ]）
        Dim iOutHedCanCnt As Integer = 0                                     '取消件数（出荷EDI(大)）
        Dim iOutDtlCanCnt As Integer = 0                                     '取消件数（出荷EDI(中)）

        Dim iOutHedUpdCnt As Integer = 0                                     '更新件数（出荷EDI(大)）

        Dim bNoErr As Boolean = True                                         'エラー無しフラグ（True：エラー無し、False：エラー有り）

        '---------------------------------------------------------------------------
        ' プライベート変数[倉庫コード/荷主L/荷主M]の設定
        '---------------------------------------------------------------------------
        _WhCd = drSemiInfo.Item("WH_CD").ToString
        _CustCdL = drSemiInfo.Item("CUST_CD_L").ToString
        _CustCdM = drSemiInfo.Item("CUST_CD_M").ToString

        Dim rcvInsFlg As String = String.Empty
        rcvInsFlg = drSemiInfo.Item("RCV_TBL_INS_FLG").ToString()

        '別インスタンス
        Dim setDsWK As DataSet = ds.Copy()
        Dim setDtlDtWk As DataTable = setDsWK.Tables("LMH030_EDI_TORIKOMI_DTL")

        For i As Integer = 0 To iSetDtlMax

            iDeleteFlg = 0
            iOutHedUpdCnt = 0

            Dim itargetUpdFlg As Integer = 0
            Dim iCntDiffFlg As Integer = 0

            '---------------------------------------------------------------------------
            ' キー項目設定
            '---------------------------------------------------------------------------
            Dim drSetDtl As DataRow = dtSetDtl.Rows(i)

            'リスト初期化
            arrKey.Clear()

            'キーの初期化
            sNewKey = String.Empty

#If True Then   'ADD 2020/01/31 010140
            setDsWK.Tables("LMH030_EDI_TORIKOMI_DTL").Clear()
            setDsWK.Tables("LMH030_EDI_TORIKOMI_DTL").ImportRow(dtSetDtl.Rows(i))

            setDsWK = MyBase.CallDAC(Me._Dac, "CheckGoods", setDsWK)
            If setDsWK.Tables("LMH030_HENKAN_GOODS").Rows.Count > 0 Then
                dtSetDtl.Rows(i).Item("COLUMN_101") = dtSetDtl.Rows(i).Item("COLUMN_7").ToString.Trim
                dtSetDtl.Rows(i).Item("COLUMN_102") = setDsWK.Tables("LMH030_HENKAN_GOODS").Rows(0).Item("HENKAN_GOODS_CD").ToString.Trim
                dtSetDtl.Rows(i).Item("COLUMN_7") = setDsWK.Tables("LMH030_HENKAN_GOODS").Rows(0).Item("HENKAN_GOODS_CD").ToString.Trim
            End If

#End If

            'キー設定['分割番号1 + 分割番号2 + 分割番号3]
            sKey = "DEVIDE_NO_1"
            If isNullColKey(drSemiInfo, sKey, sTempColNo) <> False Then
                '2015.04.21 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
                If ds.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='98'").Count > 0 Then
                    Dim chkTempColNo As String = String.Empty
                    Dim chksKey As String = "M_CUST_GOODS_CD_NO"
                    If isNullColKey(drSemiInfo, chksKey, chkTempColNo) <> False Then

                        If InStr(drSetDtl.Item(chkTempColNo).ToString().Trim(), "-") > 0 AndAlso _
                           drSetDtl.Item(chkTempColNo).ToString().Trim().Substring(InStr(drSetDtl.Item(chkTempColNo).ToString().Trim(), "-") - 2, 1).Equals("P") = True Then
                            drSetDtl.Item(sTempColNo) = String.Concat(drSetDtl.Item(sTempColNo).ToString().Trim(), "P")
                        End If
                    End If
                End If
                '2015.04.21 千葉・Ｍ・Ｒ・Ｃデュポン対応 END
                arrKey.Add(drSetDtl.Item(sTempColNo).ToString().Trim())
            End If
            sKey = "DEVIDE_NO_2"
            If isNullColKey(drSemiInfo, sKey, sTempColNo) <> False Then
                arrKey.Add(drSetDtl.Item(sTempColNo).ToString().Trim())
            End If
            sKey = "DEVIDE_NO_3"
            If isNullColKey(drSemiInfo, sKey, sTempColNo) <> False Then
                arrKey.Add(drSetDtl.Item(sTempColNo).ToString().Trim())
            End If

            For Each keys As Object In arrKey
                sNewKey += Convert.ToString(keys)
            Next

            If i = 0 Then
                '1番目は必ずbSameKeyFlgはFalse
                bSameKeyFlg = False
            Else
                '2番目以降はキーを比較
                If sNewKey.Equals(sOldKey) = True Then

                    '明細データチェックフラグが"1"の場合は明細単位で全行チェックする
                    If drSemiInfo.Item("DTL_DATACHECK_FLG").ToString().Equals(LMConst.FLG.ON) = True Then
                        bSameKeyFlg = False
                    Else
                        'キーが同一の場合
                        bSameKeyFlg = True
                    End If
                Else
                    'キーが異なる場合
                    bSameKeyFlg = False
                End If
            End If

            '---------------------------------------------------------------------------
            ' EDI取消処理(SYSDEL_FLG ='1',DEl_KB ='1')
            ' 出荷取込保留処理(SYSDEL_FLG ='1',DEl_KB ='3')
            '---------------------------------------------------------------------------

            'EDI取消処理許可フラグが"1"のみ処理
            If bSameKeyFlg = False AndAlso drSemiInfo.Item("EDI_TORIKESI_FLG").ToString().Equals("1") = True Then

                Dim setDelDs As DataSet = ds.Copy()

                '保留フラグの初期化
                iFindOutkaEdiFlg = 0

                '処理のスキップフラグ
                iSkipFlg = 0

                'INデータセット設定
                SetDatasetSelectCancelData(setDelDs, i)
                Dim tempColNo As String = String.Empty

                If setDelDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("DTL_DATACHECK_FLG").ToString().Equals(LMConst.FLG.ON) = True Then

                    '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
                    'CUST_ORD_NO + CUST_ORD_NO_DTL_NO
                    If isNullColKey(drSemiInfo, "L_DEST_CUST_ORD_NO", tempColNo) = True AndAlso isNullColKey(drSemiInfo, "M_CUST_ORD_NO_DTL_NO", tempColNo) = True Then
                        setDelDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("DTL_DATACHECK_FLG") = "3"
                    ElseIf isNullColKey(drSemiInfo, "M_CUST_ORD_NO_DTL_NO", tempColNo) = True Then

                        'CUST_ORD_NO_DTL_NO
                        If isNullColKey(drSemiInfo, "M_CUST_ORD_NO_DTL_NO", tempColNo) Then
                            setDelDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("DTL_DATACHECK_FLG") = "4"
                            setDelDs.Tables("LMH030INOUT").Rows(0).Item("CUST_ORD_NO") = setDelDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i).Item(tempColNo).ToString().Trim()
                        End If

                        '2015.04.28 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
                        If ds.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='98'").Count > 0 Then
                            If isNullColKey(drSemiInfo, "M_FREE_C25_NO", tempColNo) <> False Then
                                setDelDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("DTL_DATACHECK_FLG") = "5"
                                setDelDs.Tables("LMH030INOUT").Rows(0).Item("CUST_ORD_NO") = setDelDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i).Item(tempColNo).ToString().Trim()
                            End If
                        End If
                        '2015.04.28 千葉・Ｍ・Ｒ・Ｃデュポン対応 END
                    End If
                    '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 END

                    MyBase.CallDAC(Me._Dac, "SelectCancelData", setDelDs)

                    '2015.04.28 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
                    If ds.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='98'").Count = 0 Then
                        setDelDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("DTL_DATACHECK_FLG") = LMConst.FLG.ON
                    End If
                    '2015.04.28 千葉・Ｍ・Ｒ・Ｃデュポン対応 END

                    'CUST_ORD_NO
                    If isNullColKey(drSemiInfo, "L_DEST_CUST_ORD_NO", tempColNo) Then

                        'If MyBase.GetResultCount <> 0 AndAlso MyBase.GetResultCount <> ds.Tables("LMH030_EDI_TORIKOMI_DTL").Select(String.Concat("COLUMN_1 = '", setDelDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i).Item(tempColNo).ToString().Trim(), "'")).Length Then
                        If MyBase.GetResultCount <> 0 AndAlso MyBase.GetResultCount <> ds.Tables("LMH030_EDI_TORIKOMI_DTL").Select(String.Concat(tempColNo, "= '", setDelDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i).Item(tempColNo).ToString().Trim(), "'")).Length Then
                            iFindOutkaEdiFlg = 1
                            itargetUpdFlg = 1
                            iCntDiffFlg = 1
                        End If

                        '2015.04.22 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
                        'CUST_ORD_NO_DTL
                    ElseIf isNullColKey(drSemiInfo, "M_CUST_ORD_NO_DTL_NO", tempColNo) Then

                        '2015.04.28 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
                        If ds.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='98'").Count > 0 Then
                            If isNullColKey(drSemiInfo, "M_FREE_C25_NO", tempColNo) <> False Then

                            End If
                        End If
                        '2015.04.28 千葉・Ｍ・Ｒ・Ｃデュポン対応 END

                        'If MyBase.GetResultCount <> 0 AndAlso MyBase.GetResultCount <> ds.Tables("LMH030_EDI_TORIKOMI_DTL").Select(String.Concat("COLUMN_3 = '", setDelDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i).Item(tempColNo).ToString().Trim(), "'")).Length Then
                        If MyBase.GetResultCount <> 0 AndAlso MyBase.GetResultCount <> ds.Tables("LMH030_EDI_TORIKOMI_DTL").Select(String.Concat(tempColNo, "= '", setDelDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i).Item(tempColNo).ToString().Trim(), "'")).Length Then
                            iFindOutkaEdiFlg = 1
                            itargetUpdFlg = 1
                            iCntDiffFlg = 1
                        End If
                        '2015.04.22 千葉・Ｍ・Ｒ・Ｃデュポン対応 END
                    End If

                End If

                If setDelDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("DTL_DATACHECK_FLG").ToString().Equals(LMConst.FLG.ON) = True Then
                    'CUST_ORD_NO_DTL_NO
                    If isNullColKey(drSemiInfo, "M_CUST_ORD_NO_DTL_NO", tempColNo) = True AndAlso setDelDs.Tables("LMH030INOUT").Rows(0).Item("CUST_ORD_NO").ToString().Equals(setDelDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i).Item(tempColNo).ToString().Trim()) = False Then
                        setDelDs.Tables("LMH030INOUT").Rows(0).Item("CUST_ORD_NO") = String.Concat(setDelDs.Tables("LMH030INOUT").Rows(0).Item("CUST_ORD_NO"), setDelDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i).Item(tempColNo).ToString().Trim())
                    End If
                End If

                'DACアクセス
                Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SelectCancelData", setDelDs)
                If MyBase.GetResultCount > 0 Then
                    If String.IsNullOrEmpty(rtnDs.Tables("LMH030OUT").Rows(0).Item("OUTKA_CTL_NO").ToString()) Then
                        'チェック ②<レコード有 / OUTKA_CTL_NO無> = 出荷はまだできてないため(前回データを取消UPDATE、後続データを新規データ扱いでINSERT)

                        'INデータ再設定
                        setDelDs.Tables("LMH030INOUT").Clear()
                        Dim drSetDel As DataRow = setDelDs.Tables("LMH030INOUT").NewRow()
                        drSetDel.Item("NRS_BR_CD") = drSemiInfo.Item("NRS_BR_CD")
                        drSetDel.Item("EDI_CTL_NO") = rtnDs.Tables("LMH030OUT").Rows(0).Item("EDI_CTL_NO")

                        setDelDs.Tables("LMH030INOUT").Rows.Add(drSetDel)

                        MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiL", setDelDs)
                        'EDI出荷(大)の削除行カウント
                        If MyBase.GetResultCount > 0 Then
                            iOutHedCanCnt += MyBase.GetResultCount
                        End If

                        MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiM", setDelDs)
                        'EDI出荷(中)の削除行カウント
                        If MyBase.GetResultCount > 0 Then
                            iOutDtlCanCnt += MyBase.GetResultCount
                        End If

                        If rcvInsFlg.Equals("1") = True OrElse rcvInsFlg.Equals("2") = True Then
                            If String.IsNullOrEmpty(drSemiInfo.Item("RCV_NM_DTL").ToString()) = False Then
                                '受信テーブル(DTL)の削除処理
                                MyBase.CallDAC(Me._Dac, "UpdateDelRcvDtl", setDelDs)
                            End If

                        End If

                    Else
                        'チェック ①<レコード有 / OUTKA_CTL_NO有> = 出荷が既にできているため(後続データ保留扱いでINSERT)
                        iFindOutkaEdiFlg = 1
                        If rcvInsFlg.Equals("1") = True OrElse rcvInsFlg.Equals("2") = True Then
                            If String.IsNullOrEmpty(drSemiInfo.Item("RCV_NM_DTL").ToString()) = False Then
                                setDelDs.Tables("LMH030_EDI_TORIKOMI_DTL").Clear()
                                setDelDs.Tables("LMH030_EDI_TORIKOMI_DTL").ImportRow(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i))
                                '受信テーブル同一データチェック処理
                                rtnDs = MyBase.CallDAC(Me._Dac, "SelectCompareData", setDelDs)
                                If MyBase.GetResultCount > 0 Then
                                    If rcvInsFlg.Equals("1") = True Then
                                        iSkipFlg = 1
                                    ElseIf rcvInsFlg.Equals("2") = True Then
                                        iSkipFlg = 0
                                        itargetUpdFlg = 1
                                    End If

                                End If
                            End If

                        End If

                    End If
                Else
                    'チェック ③<レコード無 /                     = 処理なし                (問題なし。[正常データINSERT])
                End If

                '2番目以降はキーを比較
                If sNewKey.Equals(sOldKey) = True Then
                    'キーが同一の場合
                    bSameKeyFlg = True
                End If

            End If

            '---------------------------------------------------------------------------
            ' EDI管理番号(大,中)の採番
            '---------------------------------------------------------------------------
            ds = Me.GetEdiCtlNo(ds, bSameKeyFlg, sEdiCtlNo, iEdiCtlNoChu)

            '---------------------------------------------------------------------------
            ' 出荷EDI LM のINデータ設定
            '---------------------------------------------------------------------------
            '別インスタンス(明細用)
            Dim setDs As DataSet = ds.Copy()

            '自動追加判定
            Dim sFlg17 As String = String.Empty

            sFlg17 = drSemiInfo.Item("FLAG_17").ToString()

            If bSameKeyFlg = False Then

                '届先マスタ読込用INデータ作成
                Me.SetInDataSelectDestMst(setDs, i)

                '届先マスタ読込
                setDs = MyBase.CallDAC(Me._Dac, "SelectMstDest", setDs)

                '抽出件数判定
                If MyBase.GetResultCount = 0 Then
                    If sFlg17.Equals("0") Then
                        'エラー返却
                        Dim sDenNo As String = String.Empty
                        Dim sDestCd As String = String.Empty

                        sKey = "L_DEST_CD_NO"
                        If isNullColKey(drSemiInfo, sKey, sTempColNo) <> False Then
                            sDestCd = drSetDtl.Item(sTempColNo).ToString().Trim()
                        End If


                        sKey = "L_DEST_CUST_ORD_NO"
                        If isNullColKey(drSemiInfo, sKey, sTempColNo) <> False Then
                            sDenNo = drSetDtl.Item(sTempColNo).ToString().Trim()
                        End If

                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {String.Concat("届先コード:", sDestCd), "届先マスタ", String.Concat(" 伝票管理番号:", sDenNo)})
                        bNoErr = False
                        Continue For
                    Else
                        '自動登録(INSERT)
                        '出荷登録時に行うので処理なし

                    End If
                End If

            End If

            If (bSameKeyFlg = False AndAlso iSkipFlg = 0) OrElse (bSameKeyFlg = True AndAlso (iCntDiffFlg = 1 OrElse itargetUpdFlg = 1)) Then

                '取込DTL⇒EDI出荷(大)へのデータ設定
                'setDs = Me.SetSemiOutkaEdiL(setDs, i, sEdiCtlNo, iEdiCtlNoChu, iFindOutkaEdiFlg)
                setDs = Me.SetSemiOutkaEdiL(setDs, i, sEdiCtlNo, iEdiCtlNoChu, iFindOutkaEdiFlg, itargetUpdFlg, iCntDiffFlg)

            End If

            '商品マスタ読込用INデータ作成
            SetInDataSelectGoodsMst(setDs, i)

            '商品マスタ読込処理(商品コードがマスタに存在しない場合はエラー)
            setDs = MyBase.CallDAC(Me._Dac, "SelectMstGoods", setDs)
            If MyBase.GetResultCount = 0 AndAlso (sFlg17.Equals("0") OrElse sFlg17.Equals("1")) Then
                '商品マスタに荷主商品コードが存在しない場合はエラー

                Dim sDenNo As String = String.Empty

                Dim sDenMsg As String = String.Empty

                Dim sGoodsCd As String = String.Empty
                Dim sColTemp As String = String.Empty

                sColTemp = drSemiInfo.Item("M_CUST_GOODS_CD_NO").ToString()
                sColTemp = String.Concat("COLUMN_", sColTemp)
                sGoodsCd = drSetDtl.Item(sColTemp).ToString().Trim()

                'ADD 2016/07/20 Start
                Dim sIrimeMSG As String = String.Empty

                If setDs.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='0L'").Count > 0 Then
#If False Then ' フィルメニッヒ セミEDI対応  20160912 changed inoue
                    Dim dblIrime As Double = CDbl(drSetDtl.Item("COLUMN_14").ToString().Trim())
                    sIrimeMSG = String.Concat(" 入り目: ", dblIrime)
#Else
                    sIrimeMSG = String.Concat(" 入目: ", Me.CalcIrime(drSetDtl, drSemiInfo))

#End If
                End If

                'ADD 2016/07/20 End

                sColTemp = drSemiInfo.Item("L_DEST_CUST_ORD_NO").ToString()
                If String.IsNullOrEmpty(sColTemp) = True Then
                    sDenNo = Convert.ToString(i)
                    sDenMsg = "行番号:"
                Else
                    sColTemp = String.Concat("COLUMN_", sColTemp)
                    sDenMsg = "伝票管理番号:"
                    sDenNo = drSetDtl.Item(sColTemp).ToString().Trim()
                End If

                'UPD 2016/07/20 
                'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {String.Concat("商品コード:", sGoodsCd), "商品マスタ", String.Concat(sDenMsg, sDenNo)}
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {String.Concat("商品コード:", sGoodsCd, sIrimeMSG), "商品マスタ", String.Concat(sDenMsg, sDenNo)})
                bNoErr = False
                Continue For
            End If

            '取込DTL⇒EDI出荷(中)へのデータ設定
            'setDs = Me.SetSemiOutkaEdiM(setDs, i, sEdiCtlNo, iEdiCtlNoChu, iFindOutkaEdiFlg)
            setDs = Me.SetSemiOutkaEdiM(setDs, i, sEdiCtlNo, iEdiCtlNoChu, iFindOutkaEdiFlg, itargetUpdFlg, iCntDiffFlg)

            '受信テーブル書込みフラグが"1"または"2"の場合は受信テーブル新規追加処理を行う
            If rcvInsFlg.Equals("1") = True OrElse rcvInsFlg.Equals("2") = True Then
                If String.IsNullOrEmpty(drSemiInfo.Item("RCV_NM_DTL").ToString()) = False Then
                    '取込DTL⇒受信テーブル(DTL)への書き込み処理
                    Dim setDsRcv As DataSet = ds.Copy
                    setDsRcv.Clear()
                    setDsRcv.Tables("LMH030_SEMIEDI_INFO").ImportRow(setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0))
                    setDsRcv.Tables("LMH030_EDI_TORIKOMI_DTL").ImportRow(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i))
                    setDsRcv.Tables("LMH030_OUTKAEDI_M").ImportRow(setDs.Tables("LMH030_OUTKAEDI_M").Rows(0))
                    'setDsRcv.Tables("LMH030_OUTKAEDI_L").ImportRow(setDs.Tables("LMH030_OUTKAEDI_L").Rows(0))
                    setDsRcv = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiRcvDtl", setDsRcv)
                End If

            End If

            '---------------------------------------------------------------------------
            ' EDI出荷データの追加処理を行う
            '---------------------------------------------------------------------------
            '前行と差異がある場合、スキップフラグが= "0"(スキップなし)の場合は、EDI出荷(大)を新規追加
            If bSameKeyFlg = False AndAlso iSkipFlg = 0 Then
                'EDI出荷(大)の新規追加
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiL", setDs)
                iOutHedInsCnt = iOutHedInsCnt + 1

            End If

            'スキップフラグが= "0"(スキップなし)の場合はEDI出荷(中)の新規追加
            'EDI出荷(中)の新規追加
            If iSkipFlg = 0 Then
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiM", setDs)
                iOutDtlInsCnt = iOutDtlInsCnt + 1
            End If

            '同一データのEDI出荷(大)更新処理
            If itargetUpdFlg = 1 Then
                setDs = MyBase.CallDAC(Me._Dac, "UpdateEditOutkaEdiL", setDs)
                iOutHedUpdCnt = MyBase.GetResultCount
                If iOutHedUpdCnt > 0 Then
                    '同一データで変更データがある場合はEDI出荷(中)復活(保留)処理
                    setDs = MyBase.CallDAC(Me._Dac, "UpdateEditOutkaEdiM", setDs)
                End If
            End If

            'OldキーにNewキーをセット
            sOldKey = sNewKey
        Next

        If bNoErr Then
            'エラー無し
            dtSetHed.Rows(0).Item("ERR_FLG") = "0"
        Else
            'エラー有り
            dtSetHed.Rows(0).Item("ERR_FLG") = "1"
        End If

        '処理件数
        dtSetRet.Rows(0).Item("RCV_DTL_INS_CNT") = iRcvDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_HED_INS_CNT") = iOutHedInsCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_DTL_INS_CNT") = iOutDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_CAN_CNT") = iRcvDtlCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_HED_CAN_CNT") = iOutHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_DTL_CAN_CNT") = iOutDtlCanCnt.ToString()

        Return ds

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
                             , ByRef sIrime As String _
                              ) As Integer

        Dim dtMstGoods As DataTable = ds.Tables("LMH030_M_GOODS")
        Dim iMstGoodsCnt As Integer = dtMstGoods.Rows.Count


        Select Case iMstGoodsCnt

            Case 0      '商品マスタ取得０件
                '荷主は日興産業とする
                'エラー

                sCustCdL = _CustCdL
                sCustCdM = _CustCdM
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
                        '荷主は日興産業とする
                        sCustCdL = _CustCdL
                        sCustCdM = _CustCdM
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

#Region "カラム項目の値・日付チェック"
    ''' <summary>
    ''' 値・日付チェック
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TorikomiValChk(ByVal dr As DataRow, ByVal columnDt As DataTable, ByVal dtMcustD As DataTable) As Boolean
        Dim sFileNm As String = dr.Item("FILE_NAME_RCV").ToString()
        Dim sRecNo As String = dr.Item("REC_NO").ToString()
        Dim bRet As Boolean = True

        Dim sNum As String = String.Empty
        Dim dNum As Double = 0
        Dim sDate As String = String.Empty
        Dim sMsg As String = String.Empty
        Dim validVal As String = String.Empty
        Dim validLen As Integer = 0

        Dim sTempColNo As String = String.Empty
        Dim sKey As String = String.Empty
        Dim columnDr As DataRow = columnDt.Rows(0)

        '出荷予定日
        '- 列番号取得 -
        sKey = "L_OUTKA_PLAN_DATE_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("出荷予定日(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            sDate = dr.Item(sTempColNo).ToString().Trim()

            'If Not (sDate.Length <= 10 AndAlso sDate.IndexOf("/"c) > 0) Then
            If sDate.Length <= 10 AndAlso sDate.IndexOf("/"c) > 0 Then

                If sDate.IndexOf("/"c) > 0 Then
                    sDate = Convert.ToDateTime(sDate).ToString("yyyy/MM/dd")
                    sDate = Left(Replace(sDate, "/", String.Empty), 8)
                    dr.Item(sTempColNo) = sDate
                End If

                'バイトチェック
                If sDate.Length <> 8 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                Else

                    '日付チェック
                    sDate = Me._Blc.GetSlashEditDate(sDate).Trim()    'スラッシュ付日付に編集
                    If IsDate(sDate) = True Then
                    Else
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat(sMsg, sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                End If

            End If

        End If

        '納入予定日
        '- 列番号取得 -
        sKey = "L_ARR_PLAN_DATE_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("納入予定日(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            sDate = dr.Item(sTempColNo).ToString().Trim()

            If sDate.Equals(String.Empty) = False Then
                'If Not (sDate.Length <= 10 AndAlso sDate.IndexOf("/"c) > 0) Then
                If sDate.Length <= 10 AndAlso sDate.IndexOf("/"c) > 0 Then

                    If sDate.IndexOf("/"c) > 0 Then
                        sDate = Convert.ToDateTime(sDate).ToString("yyyy/MM/dd")
                        sDate = Left(Replace(sDate, "/", String.Empty), 8)
                        dr.Item(sTempColNo) = sDate
                    End If

                    'バイトチェック
                    If sDate.Length <> 8 Then
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    Else

                        '日付チェック
                        sDate = Me._Blc.GetSlashEditDate(sDate).Trim()    'スラッシュ付日付に編集
                        If IsDate(sDate) = True Then
                        Else
                            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat(sMsg, sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                            bRet = False
                        End If
                    End If
                End If
            End If
        End If

        '届先コード
        '- 列番号取得 -
        sKey = "L_DEST_CD_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("届先コード(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            '必須チェック
            If String.IsNullOrEmpty(validVal) Then

                '2015.06.08 横浜・古川エージェンシー対応 START
                If dtMcustD.Select("SUB_KB='97'").Count > 0 Then
                Else
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
                '2015.06.08 横浜・古川エージェンシー対応 END
            Else

                'バイト数チェック
                validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

                If validLen > 15 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If

        '届先名称
        '- 列番号取得 -
        sKey = "L_DEST_NM_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("届先名称(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            If sTempColNo <> "COLUMN_MST" Then

                validVal = dr.Item(sTempColNo).ToString().Trim()
                '必須チェック
                If columnDr.Item("FLAG_17").ToString().Equals(LMConst.FLG.ON) AndAlso String.IsNullOrEmpty(validVal) Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                Else

                    'バイト数チェック
                    validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

                    If validLen > 80 Then
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                End If
            End If

        End If

        '届先郵便番号
        '- 列番号取得 -
        sKey = "L_DEST_ZIP_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("届先郵便番号(カラム番号：", columnDr.Item(sKey), ")[")

            If sTempColNo <> "COLUMN_MST" Then


                '- ﾁｪｯｸ実行 -
                validVal = dr.Item(sTempColNo).ToString().Trim()

                '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
                If dtMcustD.Select("SUB_KB='98'").Count > 0 Then
                    If InStr(validVal, Space(1)) > 0 Then
                        validVal = _Blc.LeftB(validVal, InStr(validVal, Space(1))).Replace("〒", String.Empty)

                        '2015.05.21 千葉・Ｍ・Ｒ・Ｃデュポン対応 追加START
                    Else
                        validVal = Left(validVal, 9)
                        '2015.05.21 千葉・Ｍ・Ｒ・Ｃデュポン対応 追加END
                    End If
                End If
                '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 END

                'バイト数チェック
                validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

                If validLen > 10 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If

        '届先住所1
        '- 列番号取得 -
        sKey = "L_DEST_AD_1_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("届先住所1(カラム番号：", columnDr.Item(sKey), ")[")
            If sTempColNo <> "COLUMN_MST" Then


                '- ﾁｪｯｸ実行 -
                validVal = dr.Item(sTempColNo).ToString().Trim()

                '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
                If dtMcustD.Select("SUB_KB='98'").Count > 0 Then
                    If InStr(validVal, Space(1)) > 0 Then
                        If InStr(validVal, "　") = 0 Then
                            validVal = validVal.Substring(InStr(validVal, Space(1)))
                        Else
                            validVal = validVal.Substring(InStr(validVal, Space(1)), InStr(validVal, "　") - InStr(validVal, Space(1)))
                        End If
                    Else
                        validVal = String.Empty
                    End If
                End If
                '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 END

                'バイト数チェック
                validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

                If validLen > 40 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If



        '届先住所2
        '- 列番号取得 -
        sKey = "L_DEST_AD_2_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("届先住所2(カラム番号：", columnDr.Item(sKey), ")[")
            If sTempColNo <> "COLUMN_MST" Then


                '- ﾁｪｯｸ実行 -
                validVal = dr.Item(sTempColNo).ToString().Trim()

                '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
                If dtMcustD.Select("SUB_KB='98'").Count > 0 Then
                    If InStr(validVal, "　") > 0 Then
                        validVal = validVal.Substring(InStr(validVal, "　"))
                    Else
                        validVal = String.Empty
                    End If
                End If
                '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 END

                'バイト数チェック
                validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

                If validLen > 40 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If

        '届先住所3
        '- 列番号取得 -
        sKey = "L_DEST_AD_3_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("届先住所3(カラム番号：", columnDr.Item(sKey), ")[")
            If sTempColNo <> "COLUMN_MST" Then


                '- ﾁｪｯｸ実行 -
                validVal = dr.Item(sTempColNo).ToString().Trim()
                'バイト数チェック
                validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

                If validLen > 40 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If

        '届先電話番号
        '- 列番号取得 -
        sKey = "L_DEST_TEL_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("届先住所3(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 20 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '届先JISコード
        '- 列番号取得 -
        sKey = "L_DEST_JIS_CD_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("届先JISコード(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 7 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '売上先コード
        '- 列番号取得 -
        sKey = "L_SHIP_CD_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("売上先コード(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            '必須チェック
            If String.IsNullOrEmpty(validVal) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            Else

                'バイト数チェック
                validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

                If validLen > 15 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If

        '荷主注文番号
        '- 列番号取得 -
        sKey = "L_DEST_CUST_ORD_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("荷主注文番号(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 30 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '買主注文番号
        '- 列番号取得 -
        sKey = "L_BUYER_ORD_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("買主注文番号(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 30 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '配送時注意事項
        '- 列番号取得 -
        sKey = "L_REMARK_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("配送時注意事項(カラム番号：", columnDr.Item(sKey), ")[")

            '2015.05.25 千葉・MRCデュポン対応 修正START
            If sTempColNo <> "COLUMN_MST" Then

                '- ﾁｪｯｸ実行 -
                validVal = dr.Item(sTempColNo).ToString().Trim()
                'バイト数チェック
                validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

                If validLen > 100 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If

            End If
            '2015.05.25 千葉・MRCデュポン対応 修正START

        End If

        '自由設定文字列1
        '- 列番号取得 -
        sKey = "L_FREE_C01_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("自由設定文字列1(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 100 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '自由設定文字列2
        '- 列番号取得 -
        sKey = "L_FREE_C02_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("自由設定文字列2(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 100 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '自由設定文字列3
        '- 列番号取得 -
        sKey = "L_FREE_C03_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("自由設定文字列3(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 100 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '自由設定文字列24
        '- 列番号取得 -
        sKey = "L_FREE_C24_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("自由設定文字24(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 200 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '自由設定文字列25
        '- 列番号取得 -
        sKey = "L_FREE_C25_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("自由設定文字25(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 200 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '自由設定文字列26
        '- 列番号取得 -
        sKey = "L_FREE_C26_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("自由設定文字26(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 200 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If
        '数値1
        '- 列番号取得 -
        sKey = "L_FREE_N01_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("数値1(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 
            sNum = dr.Item(sTempColNo).ToString().Trim()
            If String.IsNullOrEmpty(sNum) = True Then
                '空の場合はゼロをセット
                dr.Item(sTempColNo) = 0
            Else
                If IsNumeric(sNum) Then
                    '数値の場合
                    dNum = Convert.ToDouble(sNum)
                    dNum = System.Math.Abs(dNum)
                    If dNum > 999999999.999 Then
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, dNum.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                Else
                    '数値でない場合
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If

        '数値2
        '- 列番号取得 -
        sKey = "L_FREE_N02_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("数値2(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 
            sNum = dr.Item(sTempColNo).ToString().Trim()
            If String.IsNullOrEmpty(sNum) = True Then
                '空の場合はゼロをセット
                dr.Item(sTempColNo) = 0
            Else
                If IsNumeric(sNum) Then
                    '数値の場合
                    dNum = Convert.ToDouble(sNum)
                    dNum = System.Math.Abs(dNum)
                    If dNum > 999999999.999 Then
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, dNum.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                Else
                    '数値でない場合
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If

        '数値3
        '- 列番号取得 -
        sKey = "L_FREE_N03_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("数値3(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 
            sNum = dr.Item(sTempColNo).ToString().Trim()
            If String.IsNullOrEmpty(sNum) = True Then
                '空の場合はゼロをセット
                dr.Item(sTempColNo) = 0
            Else
                If IsNumeric(sNum) Then
                    '数値の場合
                    dNum = Convert.ToDouble(sNum)
                    dNum = System.Math.Abs(dNum)
                    If dNum > 999999999.999 Then
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, dNum.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                Else
                    '数値でない場合
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If

        '=======以下よりMレベル制御======

        '荷主注文番号(明細)
        '- 列番号取得 -
        sKey = "M_CUST_ORD_NO_DTL_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("荷主注文番号(明細)(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 30 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '商品コード
        '- 列番号取得 -
        sKey = "M_CUST_GOODS_CD_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("商品コード(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            '必須チェック
            If String.IsNullOrEmpty(validVal) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            Else

                'バイト数チェック
                validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

                '2015.06.25 横浜・古川エージェンシー対応 START
                If dtMcustD.Select("SUB_KB='97'").Count > 0 Then
                    If validLen > 20 Then
                        validVal = _Blc.LeftB(validVal, 20)
                        validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)
                    End If
                End If
                '2015.06.25 横浜・古川エージェンシー対応 END

                If validLen > 80 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If

        '商品名
        '- 列番号取得 -
        sKey = "M_GOODS_NM_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("商品名(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 60 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If
        'ロット№
        '- 列番号取得 -
        sKey = "M_LOT_NO_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("ロット№(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 40 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        'シリアル№
        '- 列番号取得 -
        sKey = "M_SERIAL_NO_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("シリアル№(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 40 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '- 列番号取得 -
        If columnDr.Item("M_DEF_ALCTD_KB").ToString().Equals("01") Then

            '出荷総個数
            '- 列番号取得 -
            sKey = "M_OUTKA_TTL_NB_NO"
            If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

                '- エラーメッセージ設定 -
                sMsg = String.Concat("出荷総個数(カラム番号：", columnDr.Item(sKey), ")[")
                '- ﾁｪｯｸ実行 
                sNum = dr.Item(sTempColNo).ToString().Trim()
                If String.IsNullOrEmpty(sNum) = True Then
                    '空の場合はゼロをセット
                    dr.Item(sTempColNo) = 0
                Else
                    If IsNumeric(sNum) Then
                        '数値の場合
                        dNum = Convert.ToDouble(sNum)
                        If dNum > 9999999999 Then
                            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, dNum.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                            bRet = False
                        End If
                    Else
                        '数値でない場合
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                End If

            End If

        ElseIf dr.Item(sTempColNo).ToString().Equals("02") Then

            '出荷総数量
            '- 列番号取得 -
            sKey = "M_OUTKA_TTL_QT_NO"
            If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

                '- エラーメッセージ設定 -
                sMsg = String.Concat("出荷総数量(カラム番号：", columnDr.Item(sKey), ")[")
                '- ﾁｪｯｸ実行 
                sNum = dr.Item(sTempColNo).ToString().Trim()
                If String.IsNullOrEmpty(sNum) = True Then
                    '空の場合はゼロをセット
                    dr.Item(sTempColNo) = 0
                Else
                    If IsNumeric(sNum) Then
                        '数値の場合
                        dNum = Convert.ToDouble(sNum)
                        If dNum > 999999999.999 Then
                            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, dNum.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                            bRet = False
                        End If
                    Else
                        '数値でない場合
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                End If

            End If

        End If

        '入目
        '- 列番号取得 -
        sKey = "M_IRIME_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("入目(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 
            sNum = dr.Item(sTempColNo).ToString().Trim()
            If String.IsNullOrEmpty(sNum) = True Then
                '空の場合はゼロをセット
                dr.Item(sTempColNo) = 0
            Else
                '2015.03.23 横浜・古川エージェンシー対応 START
                If dtMcustD.Select("SUB_KB='97'").Count = 0 AndAlso IsNumeric(sNum) Then
                    '数値の場合
                    dNum = Convert.ToDouble(sNum)
                    If dNum > 999999999.999 Then
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, dNum.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                ElseIf dtMcustD.Select("SUB_KB='97'").Count > 0 Then
                    Dim conSnum As String = String.Empty
                    For i As Integer = 0 To sNum.Length - 1
                        If IsNumeric(sNum.Substring(i, 1)) Then
                            conSnum = String.Concat(conSnum, sNum.Substring(i, 1))
                        End If
                    Next
                    If IsNumeric(conSnum) Then
                        '数値の場合
                        dNum = Convert.ToDouble(conSnum)
                        If dNum > 999999999.999 Then
                            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, dNum.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                            bRet = False
                        End If
                    Else
                        '数値でない場合
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                    '2015.03.23 横浜・古川エージェンシー対応 END
                Else
                    '数値でない場合
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If


        '自由設定文字列1
        '- 列番号取得 -
        sKey = "M_FREE_C01_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("自由設定文字列1(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 100 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '自由設定文字列2
        '- 列番号取得 -
        sKey = "M_FREE_C02_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("自由設定文字列2(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 100 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '自由設定文字列3
        '- 列番号取得 -
        sKey = "M_FREE_C03_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("自由設定文字列3(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 100 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '自由設定文字列24
        '- 列番号取得 -
        sKey = "M_FREE_C24_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("自由設定文字24(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 200 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '自由設定文字列25
        '- 列番号取得 -
        sKey = "M_FREE_C25_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("自由設定文字25(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 200 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '自由設定文字列26
        '- 列番号取得 -
        sKey = "M_FREE_C26_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("自由設定文字26(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 -
            validVal = dr.Item(sTempColNo).ToString().Trim()
            'バイト数チェック
            validLen = System.Text.Encoding.GetEncoding(932).GetByteCount(validVal)

            If validLen > 200 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, validVal, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If

        End If

        '数値1
        '- 列番号取得 -
        sKey = "M_FREE_N01_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("数値1(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 
            sNum = dr.Item(sTempColNo).ToString().Trim()
            If String.IsNullOrEmpty(sNum) = True Then
                '空の場合はゼロをセット
                dr.Item(sTempColNo) = 0
            Else
                If IsNumeric(sNum) Then
                    '数値の場合
                    dNum = Convert.ToDouble(sNum)
                    dNum = System.Math.Abs(dNum)
                    If dNum > 999999999.999 Then
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, dNum.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                Else
                    '数値でない場合
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If

        '数値2
        '- 列番号取得 -
        sKey = "M_FREE_N02_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("数値2(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 
            sNum = dr.Item(sTempColNo).ToString().Trim()
            If String.IsNullOrEmpty(sNum) = True Then
                '空の場合はゼロをセット
                dr.Item(sTempColNo) = 0
            Else
                If IsNumeric(sNum) Then
                    '数値の場合
                    dNum = Convert.ToDouble(sNum)
                    dNum = System.Math.Abs(dNum)
                    If dNum > 999999999.999 Then
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, dNum.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                Else
                    '数値でない場合
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If

        '数値3
        '- 列番号取得 -
        sKey = "M_FREE_N03_NO"
        If isNullColKey(columnDr, sKey, sTempColNo) <> False Then

            '- エラーメッセージ設定 -
            sMsg = String.Concat("数値3(カラム番号：", columnDr.Item(sKey), ")[")
            '- ﾁｪｯｸ実行 
            sNum = dr.Item(sTempColNo).ToString().Trim()
            If String.IsNullOrEmpty(sNum) = True Then
                '空の場合はゼロをセット
                dr.Item(sTempColNo) = 0
            Else
                If IsNumeric(sNum) Then
                    '数値の場合
                    dNum = Convert.ToDouble(sNum)
                    dNum = System.Math.Abs(dNum)
                    If dNum > 999999999.999 Then
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, dNum.ToString(), "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                Else
                    '数値でない場合
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If

        End If

        '戻り値設定
        Return bRet

    End Function

#End Region


#Region "ユーティリティ"

#If False Then  'DEL 2020/01/20 000(標準)コピー後


    '列キーのNull判定
    ''' <summary>
    ''' 列キーが空であるかどうかの判定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function isNullColKey(ByVal columnDr As DataRow, ByVal getColKey As String, ByRef setColKey As String) As Boolean

        Dim sTempColNo As String = String.Empty

        sTempColNo = columnDr.Item(getColKey).ToString()
        '空ならFalse返却
        If String.IsNullOrEmpty(sTempColNo) Then
            Return False
        End If

        setColKey = String.Concat("COLUMN_", sTempColNo)

        Return True
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
#End If

#End Region

#Region "荷主明細マスタ取得"

    ''' <summary>
    ''' 荷主明細マスタ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectMstCustAll(ByVal ds As DataSet) As DataSet
        '---------------------------------------------------------------------------
        ' 該当する荷主明細を全取得
        '---------------------------------------------------------------------------
        ds = MyBase.CallDAC(Me._Dac, "SelectMstCustAll", ds)

        Return ds

    End Function

#End Region



    ''' <summary>
    ''' 入目算出
    ''' </summary>
    ''' <param name="detailRow">EDI詳細行データ</param>
    ''' <param name="semiInfoRow">セミEDI情報設定行データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CalcIrime(ByVal detailRow As DataRow, ByVal semiInfoRow As DataRow) As Double

        Dim irime As Double = 0.0
        Dim ttlNb As Double = 0
        Dim ttlQt As Double = 0
        Dim colName As String = ""

        If (isNullColKey(semiInfoRow, "M_OUTKA_TTL_NB_NO", colName) AndAlso _
            Double.TryParse(TryCast(detailRow.Item(colName), String), ttlNb)) Then

            If (isNullColKey(semiInfoRow, "M_OUTKA_TTL_QT_NO", colName) AndAlso _
                Double.TryParse(TryCast(detailRow.Item(colName), String), ttlQt)) Then

                If (ttlNb > 0) Then
                    irime = Math.Round(ttlQt / ttlNb, 3, MidpointRounding.AwayFromZero)
                End If

            End If
        End If

        Return irime

    End Function


#If True Then 'フィルメニッヒ セミEDI対応  20160926 added inoue

    ' ToDo: フィルメニッヒのセミEDIを共通から別クラスへ分離後にフィルメ固有処理を削除する

    ''' <summary>
    ''' フィルメニッヒ判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsFirmeType(ByVal ds As DataSet) As Boolean

        Dim inputData As DataSet = ds.Clone()

        inputData.Tables("LMH030_OUTKAEDI_L").ImportRow(ds.Tables("LMH030_OUTKAEDI_L").Rows(0))

        MyBase.CallDAC(Me._Dac, "SelectMstCustD0L", inputData)

        If (MyBase.GetResultCount > 0) Then
            Return True
        End If

        Return False

    End Function

#End If


#If True Then 'ADD 2019/02/27 依頼番号 : 004291   【LMS】千葉テルモ_EDIテスト+EDI出荷改修

    ' ToDo: 千葉テルモEDIを共通から固有処理を追加する

    ''' <summary>
    ''' 千葉テルモ判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsTrmType(ByVal ds As DataSet) As Boolean

        Dim inputData As DataSet = ds.Clone()

        inputData.Tables("LMH030_OUTKAEDI_L").ImportRow(ds.Tables("LMH030_OUTKAEDI_L").Rows(0))

        MyBase.CallDAC(Me._Dac, "SelectMstCustD9X", inputData)

        If (MyBase.GetResultCount > 0) Then
            Return True
        End If

        Return False

    End Function

#End If


#End Region

#End If
#End Region

#Region "ユーティリティ"

    '列キーのNull判定
    ''' <summary>
    ''' 列キーが空であるかどうかの判定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function isNullColKey(ByVal columnDr As DataRow, ByVal getColKey As String, ByRef setColKey As String) As Boolean

        Dim sTempColNo As String = String.Empty

        sTempColNo = columnDr.Item(getColKey).ToString()

        '空ならFalse返却
        If String.IsNullOrEmpty(sTempColNo) Then
            Return False
        End If

        setColKey = String.Concat("COLUMN_", sTempColNo)

        Return True
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

End Class

