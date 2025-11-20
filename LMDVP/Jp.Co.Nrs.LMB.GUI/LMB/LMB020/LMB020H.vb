' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB020H : 入荷データ編集
'  作  成  者       :  [iwamoto]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports System.IO
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility ' 2017/09/25 追加 李

''' <summary>
''' LMB020ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMB020H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMB020V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMB020G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconV As LMBControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconH As LMBControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconG As LMBControlG

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet

    '2017/09/25 修正 李↓
    '    ''' <summary>
    '    ''' 選択した言語を格納するフィールド
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '#If False Then '_LangFlgが初期化される前にアクセスしてされる問題の仮対応 20151109 INOUE
    '    Private _LangFlg As String
    '#Else
    '    Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    '#End If
    '2017/09/25 修正 李↑

    Private _Prm As LMFormData

    '2011/08/25 岸 まとめ検証結果(画面共通)№3対応
    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB020F

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _DsCmpr As DataSet

    ''' <summary>
    ''' 保管・荷役料最終計算日 検索結果
    ''' </summary>
    Private _DtHokanNiyakuCalculation As DataTable = Nothing

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        Me._Prm = prm

        'フォームの作成
        Dim frm As LMB020F = New LMB020F(Me)

        'Validate共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMBconV = New LMBControlV(Me, sForm)

        'Hnadler共通クラスの設定
        Me._LMBconH = New LMBControlH(sForm, MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMBconG = New LMBControlG(sForm)

        'Gamenクラスの設定
        Me._G = New LMB020G(Me, frm, Me._LMBconG, Me._LMBconH)

        'Validateクラスの設定
        Me._V = New LMB020V(Me, frm, Me._LMBconV, Me._LMBconG, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbEigyo, frm.cmbSoko)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '20110916　スプレッドEnterイベント実験
        'Enter押下イベント設定
        Call Me._LMBconG.SetEnterEvent(frm)


        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMB020C.ActionType.INIT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        '初期設定
        If Me.SetForm(frm, prmDs, prm.RecStatus, LMControlC.LMB020C_TABLE_NM_IN) = False Then
            MyBase.ShowMessage(frm, "G045")
            Exit Sub
        End If

        'メッセージの表示
        Call Me.ShowGMessage(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMB020C.ActionType.INIT)

        'フォームの表示
        frm.Show()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMB020C.ActionType.INIT)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub



    ''' <summary>
    ''' ロード処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="prmDs">データセット</param>
    ''' <param name="recStatus">レコードステータス</param>
    ''' <param name="tblNm">DataTable名</param>
    ''' <returns>
    ''' True ：検索成功
    ''' false：検索失敗
    ''' </returns>
    ''' <remarks></remarks>
    Private Function SetForm(ByVal frm As LMB020F, ByVal prmDs As DataSet, ByVal recStatus As String, ByVal tblNm As String) As Boolean

        Dim rtnResult As Boolean = False
        Dim mode As String = String.Empty
        Dim status As String = String.Empty
        Dim inkaNoM As String = String.Empty
        If Not prmDs.Tables(LMControlC.LMB020C_TABLE_NM_IN) Is Nothing Then
            inkaNoM = prmDs.Tables(LMControlC.LMB020C_TABLE_NM_IN).Rows(0).Item("INKA_NO_M").ToString()
        End If

        'ステータス判定
        Select Case recStatus

            Case RecordStatus.NEW_REC

                '新規処理時の初期値設定
                Me._Ds = Me.SetNewData(New LMB020DS(), prmDs, tblNm)

                rtnResult = True
                mode = DispMode.EDIT
                status = RecordStatus.NEW_REC

            Case RecordStatus.NOMAL_REC

                '初期検索処理
                'DBリードオンリー設定 ADD 2021/11/01
                Me._Ds = Me.ServerAccess(prmDs, "SelectInitData", "1")

                '検索成功
                If Me._Ds Is Nothing = False _
                    AndAlso 0 < Me._Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows.Count Then
                    rtnResult = True
                    mode = DispMode.VIEW
                    status = RecordStatus.NOMAL_REC
                End If

        End Select

        If rtnResult = True Then

            'モード・ステータスの設定
            Call Me._G.SetModeAndStatus(mode, status)

            '初期表示時の値設定
            Call Me._G.SetInitValue(Me._Ds)

            Dim rootPGID As String = MyBase.RootPGID()
            MyBase.RootPGID() = MyBase.GetPGID()
            If (LMB020C.PGID_LMD040).Equals(rootPGID) = True Then
                Dim rowIndex As Integer = Me.GetInkaNoM(frm, prmDs.Tables(LMControlC.LMB020C_TABLE_NM_IN).Rows(0).Item("INKA_NO_M").ToString)
                If rowIndex <> -1 Then
                    Call Me.SetInkaMInforData(frm, rowIndex)

                    '計算処理
                    rtnResult = Me.AllCalculation(frm)
                End If
            Else
                '入荷(中)の詳細情報表示
                If String.IsNullOrEmpty(inkaNoM) = True AndAlso
                    0 < Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows.Count Then
                    inkaNoM = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows(0).Item("INKA_NO_M").ToString
                End If
                Call Me.SetInitInkaMData(Me._Ds, inkaNoM)

                If String.IsNullOrEmpty(inkaNoM) = False Then
                    '入荷(小)情報表示
                    Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.INIT, inkaNoM)
                End If

                '計算処理
                rtnResult = Me.AllCalculation(frm)

            End If

            '新規
            If status.EndsWith(RecordStatus.NEW_REC) = True Then

                '###### 運送関連初期表示設定 ######
                '要望番号:1724 terakawa 2013.01.16 Start
                '運送関連項目初期化
                Me._G.ClearUnsoControl()
                '要望番号:1724 terakawa 2013.01.16 End

                '運送タリフセット取得
                Me._G.SetUnsoTariffSet()

                '運送会社取得（荷主マスタ→届先マスタ→運送会社マスタ 経由で取得）
                Call Me._G.GetUnsoCompany()

                '運送課税区分取得＆輸送営業所設定
                Call Me.SetUnchinUmuInitCd(frm)

                'タブレット項目の設定
                Call Me._G.SetWHTabletControl()
                '###### ここまで ######

            End If

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey(LMB020C.ActionType.INIT)

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus(LMB020C.ActionType.INIT, Me._Ds)

            'ファンクションキーの制御(F2)
            Call Me._G.SetEditBtnEnabled(Me.IsEnabledEditBtn(frm))

            'LMF800DSを設定
            Me._Ds = Me.SetUnchinCalcDataSet(Me._Ds)

            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
            'LMF810DSを設定
            Me._Ds = Me.SetShiharaiCalcDataSet(Me._Ds)
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End
            '比較用DSにコピー
            Me._DsCmpr = Me._Ds.Copy
        End If

        Return rtnResult

    End Function

    ''' <summary>
    ''' New処理時の値設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="prmDs">INパラDataSet</param>
    ''' <param name="tblNm">DataTable名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetNewData(ByVal ds As DataSet, ByVal prmDs As DataSet, ByVal tblNm As String) As DataSet

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '入荷(大)の初期値設定
        Dim inkaLDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_L)
        Dim inkaLDr As DataRow = inkaLDt.NewRow()

        '空文字の設定
        Dim max As Integer = inkaLDt.Columns.Count - 1
        For i As Integer = 0 To max
            inkaLDr.Item(i) = String.Empty
        Next

        '削除 2015.05.20 営業所またぎ処理のため営業所コード取得箇所を変更
        'inkaLDr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()

        Dim prmDr As DataRow = prmDs.Tables(tblNm).Rows(0)
        Dim custLCd As String = prmDr.Item("CUST_CD_L").ToString()
        Dim custMCd As String = prmDr.Item("CUST_CD_M").ToString()
        Dim nrsBrCd As String = prmDr.Item("NRS_BR_CD").ToString() '追加 2015.05.20
        inkaLDr.Item("CUST_CD_L") = custLCd
        inkaLDr.Item("CUST_CD_M") = custMCd
        inkaLDr.Item("NRS_BR_CD") = nrsBrCd '追加 2015.05.20


        Dim custNm As String = String.Empty
        Dim soko As String = String.Empty
        Dim tax As String = String.Empty
        Dim free As String = String.Empty
        Dim unchinTp As String = String.Empty

        If LMControlC.LMB020C_TABLE_NM_IN.Equals(tblNm) = True Then

            '検索からの新規処理の場合、キャッシュから名称を取得
            Dim custDrs As DataRow() = Me._LMBconV.SelectCustListDataRow(custLCd, custMCd)
            If 0 < custDrs.Length Then
                custNm = String.Concat(custDrs(0).Item("CUST_NM_L").ToString(), custDrs(0).Item("CUST_NM_M").ToString())
                soko = custDrs(0).Item("DEFAULT_SOKO_CD").ToString()
                If String.IsNullOrEmpty(soko) = True Then
                    soko = LM.Base.LMUserInfoManager.GetWhCd().ToString()
                End If
                tax = custDrs(0).Item("TAX_KB").ToString()
                free = custDrs(0).Item("HOKAN_FREE_KIKAN").ToString()
                unchinTp = custDrs(0).Item("UNSO_TEHAI_KB").ToString()
            End If

        Else
            'F1からの新規処理の場合、Popの戻り値を設定
            custNm = String.Concat(prmDr.Item("CUST_NM_L").ToString(), prmDr.Item("CUST_NM_M").ToString())
            soko = prmDr.Item("DEFAULT_SOKO_CD").ToString()
            If String.IsNullOrEmpty(soko) = True Then
                soko = LM.Base.LMUserInfoManager.GetWhCd().ToString()
            End If
            tax = prmDr.Item("TAX_KB").ToString()
            free = prmDr.Item("HOKAN_FREE_KIKAN").ToString()
            unchinTp = prmDr.Item("UNSO_TEHAI_KB").ToString()
        End If
        inkaLDr.Item("INKA_KB") = LMB020C.INKAKBN_NOMAL

        Dim inkaKbn As String = String.Empty
        Dim kbnDrs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_N006, "' AND KBN_CD = '", LMB020C.INKAKBN_NOMAL, "'"))
        If 0 < kbnDrs.Length Then

            '2017/09/25 修正 李↓
            inkaKbn = kbnDrs(0).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString()
            '2017/09/25 修正 李↑

        End If

        inkaLDr.Item("INKA_KB_NM") = inkaKbn
        inkaLDr.Item("WH_CD") = soko
        inkaLDr.Item("CUST_NM") = custNm
        inkaLDr.Item("INKA_TP") = LMB020C.SHUBETU_NOMAL
        inkaLDr.Item("INKA_DATE") = MyBase.GetSystemDateTime(0)
        inkaLDr.Item("TAX_KB") = tax
        inkaLDr.Item("TOUKI_HOKAN_YN") = LMB020C.UMU_ARI
        inkaLDr.Item("HOKAN_YN") = LMB020C.UMU_ARI
        inkaLDr.Item("NIYAKU_YN") = LMB020C.UMU_ARI
        inkaLDr.Item("HOKAN_FREE_KIKAN") = free

        '数値系にはゼロを設定
        inkaLDr.Item("INKA_PLAN_QT") = 0
        inkaLDr.Item("INKA_TTL_NB") = 0

#If True Then       'ADD 2018/10/31 依頼番号 : 002192   【LMS】荷主ごと_入庫日・出荷日の初期値設定(千葉角田)◎

        Dim tmpdate As Date = Date.Parse(Format(Convert.ToInt32(inkaLDr.Item("INKA_DATE")), "0000/00/00"))

        Dim custSetFLG As String = LMConst.FLG.OFF
        Dim custDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                                                                                  "CUST_CD_L = '", custLCd, "' AND ", _
                                                                                                  "CUST_CD_M = '", custMCd, "'"))

        If 0 < custDr.Length Then
            custSetFLG = LMConst.FLG.ON

            Select Case custDr(0).Item("INIT_OUTKA_PLAN_DATE_KB").ToString
                Case LMB020C.INKA_DATE_INIT_01
                    inkaLDr.Item("INKA_DATE") = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                Case LMB020C.INKA_DATE_INIT_02

                Case LMB020C.INKA_DATE_INIT_03
                    inkaLDr.Item("INKA_DATE") = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

                Case LMB020C.INKA_DATE_INIT_04
                    '前営業日
                    inkaLDr.Item("INKA_DATE") = Format(GetBussinessDay(inkaLDr.Item("INKA_DATE").ToString(), -1), "yyyyMMdd")

                Case LMB020C.INKA_DATE_INIT_05
                    '翌営業日
                    inkaLDr.Item("INKA_DATE") = Format(GetBussinessDay(inkaLDr.Item("INKA_DATE").ToString(), +1), "yyyyMMdd")
                Case Else

                    custSetFLG = LMConst.FLG.OFF
            End Select
        End If

        If (custSetFLG).Equals(LMConst.FLG.OFF) = True Then
            '荷主より設定できないとき
            '現行　ユーザーマスタ設定より
            Dim mUser As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LM.Base.LMUserInfoManager.GetUserID().ToString(), "'"))
            If 0 < mUser.Length Then
                tmpdate = Date.Parse(Format(Convert.ToInt32(inkaLDr.Item("INKA_DATE")), "0000/00/00"))
                Select Case mUser(0).Item("INKA_DATE_INIT").ToString
                    Case LMB020C.INKA_DATE_INIT_01
                        inkaLDr.Item("INKA_DATE") = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                    Case LMB020C.INKA_DATE_INIT_02

                    Case LMB020C.INKA_DATE_INIT_03
                        inkaLDr.Item("INKA_DATE") = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

                    Case LMB020C.INKA_DATE_INIT_04
                        '前営業日
                        inkaLDr.Item("INKA_DATE") = Format(GetBussinessDay(inkaLDr.Item("INKA_DATE").ToString(), -1), "yyyyMMdd")

                    Case LMB020C.INKA_DATE_INIT_05
                        '翌営業日
                        inkaLDr.Item("INKA_DATE") = Format(GetBussinessDay(inkaLDr.Item("INKA_DATE").ToString(), +1), "yyyyMMdd")

                End Select
            End If
        End If
#Else
        Dim mUser As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LM.Base.LMUserInfoManager.GetUserID().ToString(), "'"))
        If 0 < mUser.Length Then
            Dim tmpdate As Date = Date.Parse(Format(Convert.ToInt32(inkaLDr.Item("INKA_DATE")), "0000/00/00"))
            Select Case mUser(0).Item("INKA_DATE_INIT").ToString
                Case LMB020C.INKA_DATE_INIT_01
                    inkaLDr.Item("INKA_DATE") = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                Case LMB020C.INKA_DATE_INIT_02

                Case LMB020C.INKA_DATE_INIT_03
                    inkaLDr.Item("INKA_DATE") = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

                Case LMB020C.INKA_DATE_INIT_04
                    '前営業日
                    inkaLDr.Item("INKA_DATE") = Format(GetBussinessDay(inkaLDr.Item("INKA_DATE").ToString(), -1), "yyyyMMdd")

                Case LMB020C.INKA_DATE_INIT_05
                    '翌営業日
                    inkaLDr.Item("INKA_DATE") = Format(GetBussinessDay(inkaLDr.Item("INKA_DATE").ToString(), +1), "yyyyMMdd")

            End Select
        End If
#End If



        If String.IsNullOrEmpty(unchinTp) = True Then
            unchinTp = LMB020C.TEHAI_MITEI
        End If

        inkaLDr.Item("UNCHIN_TP") = unchinTp

        '行追加
        inkaLDt.Rows.Add(inkaLDr)

        'シーケンスの初期値設定
        ds = Me.AddMaxNo(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 新規でMaxRecを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function AddMaxNo(ByVal ds As DataSet) As DataSet

        'シーケンスの初期値設定
        Dim maxSeqDt As DataTable = ds.Tables("LMB020_MAX_NO")
        Dim maxSeqDr As DataRow = maxSeqDt.NewRow()
        Dim max As Integer = maxSeqDt.Columns.Count - 1
        For i As Integer = 0 To max
            maxSeqDr.Item(i) = LMB020C.MAEZERO
        Next
        maxSeqDt.Rows.Add(maxSeqDr)

        Return ds

    End Function

    ''' <summary>
    ''' 入荷(中)の詳細情報表示
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inkaNoM">入荷中番</param>
    ''' <remarks></remarks>
    Private Sub SetInitInkaMData(ByVal ds As DataSet, ByVal inkaNoM As String)

        'INパラに入荷中番がない場合、スルー
        If String.IsNullOrEmpty(inkaNoM) = True Then
            Exit Sub
        End If

        '入荷(中)の詳細情報表示
        Call Me._G.SetInkaMInforData(ds, -1, inkaNoM)

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 新規モードに切替
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShiftInsertStatus(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.INIT))

        '荷主マスタPop表示
        'START YANAI 要望番号481
        'rtnResult = rtnResult AndAlso Me.NewModeShowCustPop(frm)
        rtnResult = rtnResult AndAlso Me.NewModeShowCustPop(frm, LMB020C.ActionType.INIT)
        'END YANAI 要望番号481

        '処理終了アクション
        Call Me.EndAction(frm)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMB020C.ActionType.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMB020C.ActionType.NEWMODE, Me._Ds)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMB020C.ActionType.NEWMODE)

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShiftEditMode(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.EDIT))

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsEditChk(Me._Ds)

        'エラーの場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '排他チェック
        Dim rtnDs As DataSet = Me.ServerAccess(Me._Ds, "EditChk")

        'エラーがある場合、終了
        If MyBase.IsMessageExist() = True Then

            'メッセージ設定
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call Me.EndAction(frm)

            Exit Sub

        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMB020C.ActionType.EDIT)

        '特殊モードを通常に設定
        frm.lblEdit.TextValue = String.Empty

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMB020C.ActionType.EDIT, Me._Ds)

        'スプレッドの行をクリア
        frm.sprDetail.CrearSpread()

        Dim inkaNoM As String = frm.lblKanriNoM.TextValue
        If String.IsNullOrEmpty(inkaNoM) = False Then

            '入荷(小)情報表示
            Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.DOUBLECLICK, inkaNoM)
      
        End If

        'フォーカスの設定
        Call Me._G.SetFoucus(LMB020C.ActionType.EDIT)

    End Sub

    ''' <summary>
    ''' 複写処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShiftCopyMode(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.COPY)) = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '要望：2210 振替チェック
        Dim rtnResult As Boolean = Me._V.IsFurikae(Me._Ds)
        If rtnResult = False Then
            '振替データは複写しない
            Exit Sub
        End If

        'データセットの設定
        Me._Ds = Me.SetCopyData(Me._Ds)

        '保管・荷役料最終計算日 検索結果 初期化
        Me._DtHokanNiyakuCalculation = Nothing

        '処理終了アクション
        Call Me.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.COPY_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMB020C.ActionType.COPY)

        '値のクリア
        Call Me._G.ClearControl()

        '値の設定
        Call Me._G.SetInitValue(Me._Ds)

        'タブレット項目の設定
        Call Me._G.SetWHTabletControl()

        'スプレッドの行をクリア
        frm.sprDetail.CrearSpread()

        '1行目の中情報を表示
        Call Me.SetInkaMInforData(frm, 0)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMB020C.ActionType.COPY, Me._Ds)

        'ファンクションキーの制御(F2)
        Call Me._G.SetEditBtnEnabled(Me.IsEnabledEditBtn(frm))

        'フォーカスの設定
        Call Me._G.SetFoucus(LMB020C.ActionType.EDIT)

    End Sub

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteAction(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.DELETE))
        
        'データチェック
        rtnResult = rtnResult AndAlso Me._V.IsDeleteChk(Me._Ds)

        '確認メッセージ表示
        rtnResult = rtnResult AndAlso Me._LMBconH.SetMessageC001(frm, frm.FunctionKey.F4ButtonName.Replace("　", String.Empty))

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '排他チェック
        Dim rtnDs As DataSet = Me.ServerAccess(Me._Ds, "DeleteAction")

        'エラーの場合、終了
        If MyBase.IsMessageExist() = True Then

            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call Me.EndAction(frm)

            Exit Sub

        End If

        '画面を閉じる
        frm.Close()

    End Sub
    ''' <summary>
    ''' 取込(CSV,入荷検品WK)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub TorikomiAction(ByVal frm As LMB020F)

        'パラメータクラス生成(20130215)
        Dim prm As LMFormData = New LMFormData()

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.INIT))

        'データチェック
        rtnResult = rtnResult AndAlso Me._V.IsSpecialEditChk(Me._Ds, LMB020C.ActionType.CSVINPUT)

        '入荷(中)向け開始
        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInkaMAddChk(LMB020C.ActionType.INIT_M)

        '入荷(中)の追加処理
        Dim flgMdata As String = String.Empty

        '2014.07.29 Ri [ｱｸﾞﾘﾏｰﾄ対応] add -ST-
        '入荷データの集約フラグ
        Dim flgMMerge As String = String.Empty
        '2014.07.29 Ri [ｱｸﾞﾘﾏｰﾄ対応] add -ED-

        '2013.07.16 追加START
        '荷主明細マスタを参照し、チェック有無を確認する
        Dim nrsbrCd As String = frm.cmbEigyo.SelectedValue.ToString() '20160728 営業所コード追加
        Dim custCd As String = frm.txtCustCdL.TextValue()
        Dim custDetailDrs As DataRow() = Me._LMBconV.SelectCustDetailsListDataRow(nrsbrCd, custCd)
        Dim goodsLotCheckYn As String = String.Empty
        Dim chkFlg As Boolean = False
        Dim serverChkFlg As Boolean = False
        If 0 < custDetailDrs.Length Then

            For i As Integer = 0 To custDetailDrs.Length - 1
                If custDetailDrs(i).Item("SUB_KB").ToString().Equals("59") = True Then
                    flgMdata = custDetailDrs(i).Item("SET_NAIYO").ToString()

                    '2014.07.29 Ri [ｱｸﾞﾘﾏｰﾄ対応] add -ST-
                    flgMMerge = custDetailDrs(i).Item("SET_NAIYO_2").ToString()
                    '2014.07.29 Ri [ｱｸﾞﾘﾏｰﾄ対応] add -ED-


                    Exit For
                End If
            Next

#If False Then ' 新照合対応
            'If String.IsNullOrEmpty(flgMdata) = True Then
            '    flgMdata = "0"
            'End If
            '20160728 tsunehira add
            'SUB_KB=59（取込フラグ）がなかった時の処理
            If String.IsNullOrEmpty(flgMdata) = True Then
                MyBase.ShowMessage(frm, "E208", New String() {frm.FunctionKey.F6ButtonName})
                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Sub
            End If
#Else
            'M_CUST_DETAILSが0件のとき通らないのでIFの外に移動
            'If String.IsNullOrEmpty(flgMdata) = True Then
            '    flgMdata = LMB020C.M_DATA_FLG.NRS_SEQ_QR_NO
            'End If
#End If
        End If
        '2013.07.16 追加END

        If String.IsNullOrEmpty(flgMdata) = True Then
            flgMdata = LMB020C.M_DATA_FLG.NRS_SEQ_QR_NO
        End If

        'flgMdata = "1" '("1":CSV取り込み　0:入荷M明細　2:検品WK取込)
        If (LMB020C.M_DATA_FLG.NRS_SEQ_QR_NO.Equals(flgMdata)) Then

            ' 入荷連番QR取込
            If (rtnResult = False OrElse Me.LoadInkaQrAction(frm) = False) Then

                If (Me.IsMessageStoreExist) Then
                    MyBase.MessageStoreDownload(True)

                    If (MyBase.IsErrorMessageExist = False) Then
                        MyBase.SetMessage("E235")
                    End If
                End If

                If (Me.IsMessageExist) Then
                    MyBase.ShowMessage(frm)
                End If
            End If

        Else
            rtnResult = rtnResult AndAlso Me.AddInkaMDataAction(frm, flgMdata, flgMMerge)
        End If


        '入荷(中)向け終了

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '2015年4月よりCSV取り込みを使用していないためコメントアウト
        '取込処理
        '2013.07.16 修正START
        'If flgMdata.Equals("2") = True Then
        '    'Call Me.TorikomiKenpinData(frm)
        'Else
        '    Call Me.TorikomiData(frm)
        'End If
        '2013.07.16 修正END

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub


    ''' <summary>
    '''日陸連番QR番号を区切文字付の書式に変換する。
    ''' </summary>
    ''' <param name="nrsQrNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertNrsSeqQrNoWithSpliter(ByVal nrsQrNo As String) As String

        Const ONE_GROUP_LENGTH As Integer = 5
        Const SPLITER As String = "-"

        If (String.IsNullOrWhiteSpace(nrsQrNo)) Then
            Return ""
        End If

        Dim withSpliterText As New System.Text.StringBuilder()
        For i As Integer = 0 To nrsQrNo.Length - 1 Step ONE_GROUP_LENGTH

            If (nrsQrNo.Length < i) Then
                Exit For
            End If

            withSpliterText.Append(Left(nrsQrNo.Substring(i), ONE_GROUP_LENGTH))
            withSpliterText.Append(SPLITER)
        Next

        Return If(SPLITER.Equals(withSpliterText.ToString().LastOrDefault) _
                               , withSpliterText.ToString(0, withSpliterText.Length - 1) _
                               , withSpliterText.ToString())


    End Function


    ''' <summary>
    ''' 日陸連番QRによる検品結果をメッセージストアに設定する。
    ''' </summary>
    ''' <param name="table"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetMessageStoreInkaQr(ByVal table As DataTable) As Boolean


        ' タイトル行
        Me.SetMessageStore(LMB020C.GUIDANCE_KBN, "G088")

        ' 検品データ
        For Each r As DataRow In table.Rows


            Dim nrsSeqQrNo As String _
                = Me.ConvertNrsSeqQrNoWithSpliter(TryCast(r.Item("NRS_SEQ_QR_NO"), String))

            Me.SetMessageStore(LMB020C.GUIDANCE_KBN, "G089" _
                             , New String() _
                             {TryCast(r.Item("INKA_NO_M"), String) _
                             , TryCast(r.Item("INKA_NO_S"), String) _
                             , nrsSeqQrNo _
                             , TryCast(r.Item("GOODS_CD_CUST"), String) _
                             , TryCast(r.Item("GOODS_NM_1"), String) _
                             , TryCast(r.Item("LOT_NO"), String) _
                             , TryCast(r.Item("KENPIN_NB"), String) _
                             , TryCast(r.Item("IRIME"), String) _
                             , TryCast(r.Item("LT_DATE"), String) _
                             , TryCast(r.Item("SERIAL_NO"), String) _
                             , TryCast(r.Item("GOODS_CRT_DATE"), String) _
                             })
        Next

    End Function


    ''' <summary>
    ''' 入荷実績(日陸連番QR)取込
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadInkaQrAction(ByVal frm As LMB020F) As Boolean


        If (Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.EDIT) = False) Then
            Return False
        End If

        Dim isCurrectData As Boolean = True

        If (_V.IsPossibleLoadInkaQrStart = False) Then
            Return False
        End If

        Using wkData As New LMB020DS
            wkData.Merge(_Ds)

            Dim isLoaded As Boolean = False

            ' 入荷連番QRから取込 C20626433
            If (wkData.LMB020_INKA_SEQ_QR.Count > 0 AndAlso _
                wkData.LMB020_INKA_SEQ_QR _
                    .Where(Function(r) LMConst.FLG.ON.Equals(r.IS_LOADING)).Count = 0) Then

                If (wkData.LMB020_INKA_SEQ_QR _
                    .Where(Function(r) String.IsNullOrEmpty(r.LOAD_DATE) = False).Count > 0) Then

                    If (MyBase.ShowMessage(frm, "W268") <> MsgBoxResult.Ok) Then

                        Me.SetMessage("G090")
                        Return False
                    End If

                End If

                If (_V.IsMatchInspecedGoods(wkData) = False) Then

                    ' 検品結果をExcelへ出力する。
                    Me.SetMessageStoreInkaQr(wkData.LMB020_INKA_SEQ_QR)
                    Me.SetMessage("E938")

                    Return False
                End If


                'Dim inkaSRowCount As Integer = 0
                'Dim ttlNumbers As New Dictionary(Of String, Integer)
                'Dim changedRows As New List(Of LMB020DS.LMB020_INKA_SEQ_QRRow)


                Dim existsWarning As Boolean = False

                ' 検品データが存在しない入荷Sの行情報を格納
                Dim noInspectedRows As New List(Of LMB020DS.LMB020_INKA_SRow)

                ' 取込データ反映後の新しい入荷Sの行情報を格納
                Dim newTable As New LMB020DS.LMB020_INKA_SDataTable

                ' 入荷M
                For Each inkaMRow As LMB020DS.LMB020_INKA_MRow In wkData.LMB020_INKA_M


                    If (LMB020C.HIKIATE_ARI.Equals(inkaMRow.HIKIATE)) Then

                        ' 引当中の商品が存在するため取込できません。
                        MyBase.SetMessage("E937")
                        Return False
                    End If


                    Dim kenpinCountM As Long = wkData.LMB020_INKA_SEQ_QR _
                            .Where(Function(q) inkaMRow.INKA_NO_L.Equals(q.INKA_NO_L) AndAlso _
                                               inkaMRow.INKA_NO_M.Equals(q.INKA_NO_M) AndAlso _
                                               LMConst.FLG.OFF.Equals(inkaMRow.SYS_DEL_FLG)) _
                            .Sum(Function(q) Convert.ToInt64(q.KENPIN_NB))

                    If (kenpinCountM <> Convert.ToInt64(inkaMRow.SUM_KOSU)) Then

                        Me.SetMessageStoreInkaQr(wkData.LMB020_INKA_SEQ_QR)
                        Me.SetMessage("E938")

                        Return False
                    End If


                    ' 入荷S

                    For Each inkaSRow As LMB020DS.LMB020_INKA_SRow In wkData.LMB020_INKA_S _
                        .Where(Function(s) s.INKA_NO_L.Equals(inkaMRow.INKA_NO_L) AndAlso _
                                           s.INKA_NO_M.Equals(inkaMRow.INKA_NO_M))

                        ' 検品データ(入荷QR)
                        Dim qrRows As IEnumerable(Of LMB020DS.LMB020_INKA_SEQ_QRRow) _
                            = wkData.LMB020_INKA_SEQ_QR _
                            .Where(Function(q) inkaSRow.INKA_NO_L.Equals(q.INKA_NO_L) AndAlso _
                                               inkaSRow.INKA_NO_M.Equals(q.INKA_NO_M) AndAlso _
                                               inkaSRow.INKA_NO_S.Equals(q.INKA_NO_S))

                        If (qrRows.Count = 0) Then
                            ' 未検品の商品(検品データが存在しない)
                            noInspectedRows.Add(inkaSRow)
                            existsWarning = True

                            Continue For

                        End If

                        Dim kenpinCount As Long = qrRows.Sum(Function(q) Convert.ToInt64(q.KENPIN_NB))
                        If (kenpinCount <> Convert.ToInt64(inkaSRow.KOSU_S)) Then
                            ' S毎の入荷数が異なる。=> ワーニング
                            existsWarning = True
                        End If

                        Dim isFirst As Boolean = True


                        ' 検品データ(入荷QR)
                        For Each qrRow As LMB020DS.LMB020_INKA_SEQ_QRRow In qrRows

                            If (inkaMRow.GOODS_CD_NRS.Equals(qrRow.GOODS_CD_NRS) = False) Then
                                ' 検品時と商品が異なる。
                                Me.SetMessageStoreInkaQr(wkData.LMB020_INKA_SEQ_QR)
                                Me.SetMessage("E938")
                                Return False

                            End If

                            If (inkaSRow.IRIME.Equals(qrRow.IRIME) = False OrElse _
                                inkaSRow.LOT_NO.Equals(qrRow.LOT_NO) = False OrElse _
                                inkaSRow.SERIAL_NO.Equals(qrRow.SERIAL_NO) = False OrElse _
                                inkaSRow.GOODS_CRT_DATE.Equals(qrRow.GOODS_CRT_DATE) = False OrElse _
                                inkaSRow.LT_DATE.Equals(qrRow.LT_DATE) = False) Then

                                ' 検品時と商品が異なる。
                                Me.SetMessageStoreInkaQr(wkData.LMB020_INKA_SEQ_QR)
                                Me.SetMessage("E938")

                                Return False

                            End If


                            Dim newRow As LMB020DS.LMB020_INKA_SRow = newTable.NewLMB020_INKA_SRow

                            ' 行コピー
                            newRow.ItemArray = inkaSRow.ItemArray()

                            If (String.IsNullOrWhiteSpace(qrRow.TOU_NO) = False) Then
                                newRow.TOU_NO = qrRow.TOU_NO
                            End If

                            If (String.IsNullOrWhiteSpace(qrRow.SITU_NO) = False) Then
                                newRow.SITU_NO = qrRow.SITU_NO
                            End If

                            If (String.IsNullOrWhiteSpace(qrRow.ZONE_CD) = False) Then
                                newRow.ZONE_CD = qrRow.ZONE_CD
                            End If

                            If (String.IsNullOrWhiteSpace(qrRow.LOCA) = False) Then
                                newRow.LOCA = qrRow.LOCA
                            End If

                            newRow.KOSU_S = qrRow.KENPIN_NB
                            newRow.SURYO_S = (Convert.ToDecimal(qrRow.IRIME) * Convert.ToDecimal(qrRow.KENPIN_NB)).ToString()


                            Dim remainder As Long = 0
                            newRow.KONSU = Math.DivRem(Convert.ToInt64(qrRow.KENPIN_NB) _
                                                     , Convert.ToInt64(inkaMRow.PKG_NB), remainder).ToString()

                            newRow.HASU = remainder.ToString()
                            newRow.BETU_WT = Convert.ToString( _
                                                            Me._G.ToRound( _
                                                                          Convert.ToDecimal(newRow.IRIME) * _
                                                                          Convert.ToDecimal(inkaMRow.STD_WT_KGS) / _
                                                                          Convert.ToDecimal(inkaMRow.STD_IRIME_NB) _
                                                                          , LMB020C.JURYO_ROUND_POS _
                                                                         ) _
                                                         )
                            newRow.JURYO_S = (Convert.ToDecimal(qrRow.KENPIN_NB) _
                                            * Convert.ToDecimal(inkaMRow.STD_WT_KGS)).ToString()

                            If (LMConst.FLG.ON.Equals(qrRow.EXISTS_REMARK)) Then
                                newRow.EXISTS_REMARK = LMConst.FLG.ON
                            End If

                            Dim whereString As String = String.Concat("TOU_NO ='", newRow.TOU_NO, "'", _
                                                                      "AND SITU_NO ='", newRow.SITU_NO, "'", _
                                                                      "AND ZONE_CD ='", newRow.ZONE_CD, "'", _
                                                                      "AND LOCA ='", newRow.LOCA, "'", _
                                                                      "AND EXISTS_REMARK ='", newRow.EXISTS_REMARK, "'", _
                                                                      "AND IRIME ='", newRow.IRIME, "'", _
                                                                      "AND LOT_NO ='", newRow.LOT_NO, "'", _
                                                                      "AND SERIAL_NO ='", newRow.SERIAL_NO, "'", _
                                                                      "AND GOODS_CRT_DATE ='", newRow.GOODS_CRT_DATE, "'", _
                                                                      "AND LT_DATE ='", newRow.LT_DATE, "'")

                            Dim addedRow As DataRow() = newTable.Select(whereString)

                            If addedRow.Length > 0 Then
                                addedRow(0).Item("KOSU_S") = (Convert.ToDecimal(addedRow(0).Item("KOSU_S")) + Convert.ToDecimal(newRow.KOSU_S)).ToString()
                                addedRow(0).Item("SURYO_S") = (Convert.ToDecimal(qrRow.IRIME) * Convert.ToDecimal(addedRow(0).Item("KOSU_S"))).ToString()


                                addedRow(0).Item("KONSU") = Math.DivRem(Convert.ToInt64(addedRow(0).Item("KOSU_S")) _
                                                                        , Convert.ToInt64(inkaMRow.PKG_NB), remainder).ToString()

                                addedRow(0).Item("HASU") = remainder.ToString()

                                addedRow(0).Item("JURYO_S") = (Convert.ToDecimal(addedRow(0).Item("KOSU_S")) _
                                                            * Convert.ToDecimal(inkaMRow.STD_WT_KGS)).ToString()
                                qrRow.IS_LOADING = LMConst.FLG.ON
                            Else
                                If Not isFirst Then
                                    newRow.INKA_NO_S = Me.SetInkaSmaxSeq(inkaSRow.INKA_NO_M)
                                    newRow.ZAI_REC_NO = ""
                                    newRow.ZAI_REC_CNT = ""
                                    newRow.UP_KBN = LMConst.FLG.OFF
                                End If
                                qrRow.IS_LOADING = LMConst.FLG.ON
                                newTable.AddLMB020_INKA_SRow(newRow)
                                isFirst = False
                            End If

                        Next
                    Next
                Next

                '検品数と取込数チェック
                Dim kenpinCountS As Long = wkData.LMB020_INKA_SEQ_QR.Sum(Function(q) Convert.ToInt64(q.KENPIN_NB))
                Dim newCount As Long = newTable.Sum(Function(q) Convert.ToInt64(q.KOSU_S))
                If kenpinCountS <> newCount Then
                    Me.SetMessageStoreInkaQr(wkData.LMB020_INKA_SEQ_QR)
                    Me.SetMessage("E938")
                End If

                ' 強制確認
                Dim isForce As Boolean = False
                If (isCurrectData AndAlso existsWarning) Then

                    Me.SetMessage("W269")

                    If (Me.ShowMessage(frm) = MsgBoxResult.Ok) Then
                        isForce = True
                    Else

                        Me.SetMessage("G090")

                        ' キャンセル
                        Return False
                    End If
                End If

                If (isCurrectData) Then
                    If (isForce) Then
                        ' 検品データが存在しない行に削除フラグを設定
                        For Each deleteRow As LMB020DS.LMB020_INKA_SRow In noInspectedRows
                            If (LMConst.FLG.ON.Equals(deleteRow.UP_KBN)) Then
                                deleteRow.SYS_DEL_FLG = LMConst.FLG.ON
                                newTable.ImportRow(deleteRow)

                            End If
                        Next
                    End If

                    _Ds.Tables(LMB020C.TABLE_NM_INKA_S).Clear()
                    _Ds.Tables(LMB020C.TABLE_NM_INKA_S).Merge(newTable)

                    _Ds.Tables(LMB020C.TABLE_NM_INKA_SEQ_QR).Clear()
                    _Ds.Tables(LMB020C.TABLE_NM_INKA_SEQ_QR).Merge(wkData.LMB020_INKA_SEQ_QR)

                    frm.cmbWhWkStatus.SelectedValue = LMB020C.WH_KENPIN_WK_STATUS_INKA.INSPECTED
                    frm.cmbWhWkStatus.Refresh()

                    Me.SetInkaMInforData(frm, 0)
                    For Each inkaMRow As LMB020DS.LMB020_INKA_MRow In wkData.LMB020_INKA_M

                        For Each inkaSRow As LMB020DS.LMB020_INKA_SRow In newTable _
                            .Where(Function(s) s.INKA_NO_L.Equals(inkaMRow.INKA_NO_L) AndAlso _
                                               s.INKA_NO_M.Equals(inkaMRow.INKA_NO_M))

                            ' 在庫再設定
                            Me.SetZaikoData(_Ds, inkaMRow, inkaSRow)


                            If (LMConst.FLG.ON.Equals(inkaSRow.SYS_DEL_FLG)) Then

                                Me.DeleteTabelData(_Ds.Tables(LMB020C.TABLE_NM_ZAI) _
                                                 , String.Format("INKA_NO_M = '{0}' AND INKA_NO_S = '{1}'" _
                                                                , inkaSRow.INKA_NO_M _
                                                                , inkaSRow.INKA_NO_S))
                            End If
                        Next
                    Next
                Else

                    If (IsMessageExist() = False) Then
                        Me.SetMessage("E938")
                    End If

                    Return False
                End If

            Else
                ' 取込対象のデータが存在しません。
                MyBase.SetMessage("E656")

                Return False
            End If

        End Using

        Return True

    End Function


    ''' <summary>
    ''' 取込(検品取込)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub KenpinTorikomiAction(ByVal frm As LMB020F)

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        '追加開始 2014.01.13 韓国CALT対応

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.INIT))

        'データチェック
        rtnResult = rtnResult AndAlso Me._V.IsSpecialEditChk(Me._Ds, LMB020C.ActionType.CSVINPUT)

        '入荷(中)向け開始
        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInkaMAddChk(LMB020C.ActionType.INIT_M)

        '入荷(中)の追加処理
        Dim flgMdata As String = String.Empty

        '入荷データの集約フラグ
        Dim flgMMerge As String = String.Empty

        '区分マスタを参照し、処理続行可能か確認する
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'Dim nrsbrCd As String = LMUserInfoManager.GetNrsBrCd()
        Dim nrsbrCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim whCd As String = frm.cmbSoko.SelectedValue.ToString()
        Dim kbnDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "C024", "' AND KBN_NM1 = '", nrsbrCd, "' AND KBN_NM2 = '", whCd, "'"))

        '入荷データの集約フラグ
        '2017/09/25 修正 李↓
        rtnResult = rtnResult AndAlso Me._V.IsKenpinTorikomiActionChk(kbnDr.Count, lgm.Selector({"選択中の倉庫は", "W/H in selection", "선택중인 창고는 ", "中国語"}))
        '2017/09/25 修正 李↑

        '入荷中追加
        rtnResult = rtnResult AndAlso Me.AddKenpinInkaMDataAction(frm, flgMdata, flgMMerge)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        '追加終了 2014.01.13 韓国CALT対応

    End Sub


    ''' <summary>
    ''' 運送修正処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShiftUnsoEditMode(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.UNSOEDIT))

        'データチェック
        rtnResult = rtnResult AndAlso Me._V.IsSpecialEditChk(Me._Ds, LMB020C.ActionType.UNSOEDIT)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '排他チェック
        Dim rtnDs As DataSet = Me.ServerAccess(Me._Ds, "UnsoEditChk")

        'エラーの場合、処理終了
        If MyBase.IsMessageExist() = True Then

            'メッセージ設定
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call Me.EndAction(frm)

            Exit Sub

        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMB020C.ActionType.UNSOEDIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMB020C.ActionType.UNSOEDIT, Me._Ds)

        'ファンクションキーの制御(F2)
        Call Me._G.SetEditBtnEnabled(Me.IsEnabledEditBtn(frm))

        '特殊モードに設定
        frm.lblEdit.TextValue = LMB020C.ActionType.UNSOEDIT.ToString()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMB020C.ActionType.UNSOEDIT)

    End Sub

    ''' <summary>
    ''' 起算日修正処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShiftKisanbiEditMode(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.DATEEDIT))

        'データチェック
        rtnResult = rtnResult AndAlso Me._V.IsSpecialEditChk(Me._Ds, LMB020C.ActionType.DATEEDIT)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '排他チェック
        Dim rtnDs As DataSet = Me.ServerAccess(Me._Ds, "DateEditChk")

        'エラーの場合、処理終了
        If MyBase.IsMessageExist() = True Then

            'メッセージ設定
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call Me.EndAction(frm)

            Exit Sub

        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMB020C.ActionType.DATEEDIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMB020C.ActionType.DATEEDIT, Me._Ds)

        'ファンクションキーの制御(F2)
        Call Me._G.SetEditBtnEnabled(Me.IsEnabledEditBtn(frm))

        '特殊モードに設定
        frm.lblEdit.TextValue = LMB020C.ActionType.DATEEDIT.ToString()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMB020C.ActionType.DATEEDIT)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMB020F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.MASTEROPEN))

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMB020C.ActionType.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        '2011/08/25 岸 まとめ検証結果(画面共通)№3対応
        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        '項目チェック
        Me.ShowPopupControl(frm, objNm, LMB020C.ActionType.MASTEROPEN)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Dim eventFlg As Boolean = e.KeyCode = Keys.Enter

        '参照の場合、Tab移動して終了
        If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then
            Call Me.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        'Enterキー判定
        Dim rtnResult As Boolean = eventFlg
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '権限チェック
        rtnResult = rtnResult AndAlso Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.ENTER))

        '計算処理
        rtnResult = rtnResult AndAlso Me.SetKosuData(frm, objNm)


        '20110916　スプレッドEnterイベント実験
        If frm.sprDetail.Name.Equals(objNm) Then Exit Sub


        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMB020C.ActionType.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me.NextFocusedControl(frm, objNm, eventFlg)
            Exit Sub
        End If

        '2011/08/25 岸 まとめ検証結果(画面共通)№3対応
        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        '項目チェック
        Me.ShowPopupControl(frm, objNm, LMB020C.ActionType.ENTER)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me.NextFocusedControl(frm, objNm, eventFlg)

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SaveInkaItemData(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.SAVE))

        '計算処理
        rtnResult = rtnResult AndAlso Me.AllCalculation(frm)
        
        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck()

        '値設定
        rtnResult = rtnResult AndAlso Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.SAVE)

        'タブレット項目設定値更新
        Me.SetWhhSijiStatus(frm)

        '入荷(小)のデータチェック
        rtnResult = rtnResult AndAlso Me.IsInkaSZenRecChk(frm, Me._Ds)

        '要望番号:1350 terakawa 2012.08.27 Start
        '同一置き場（同一商品・ロット）チェック
        rtnResult = rtnResult AndAlso Me.GoodsLotCheck(frm, Me._Ds)
        '要望番号:1350 terakawa 2012.08.27 End

        '棟 + 室 + ZONE（置き場情報）温度管理チェック
        rtnResult = rtnResult AndAlso Me.IsOndoCheck(frm, Me._Ds)

        '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
        '申請外の商品保管ルール情報取得
        Me._Ds = Me.ServerAccess(Me._Ds, "getTouSituExp")
        '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

        '依頼番号:013987 棟室マスタ、ZONEマスタチェック処理改修
        ''棟 + 室 危険物倉庫、一般倉庫チェック
        'rtnResult = rtnResult AndAlso Me.IsSokoCheck(frm, Me._Ds)
        '新規入荷チェック
        rtnResult = rtnResult AndAlso Me.IsTouSituZoneCheck(frm, Me._Ds)

        ' 温度管理アラートチェック
        rtnResult = rtnResult AndAlso Me.IsOndoKanriAlertCheck(frm, Me._Ds)

        ' 庫内作業データチェック
        rtnResult = rtnResult AndAlso Me.IsInkaQrCheck(frm, Me._Ds)

        '確認メッセージ表示
        rtnResult = rtnResult AndAlso Me.SaveConfirmMessage(frm, actionType)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Return False

        End If

        '--------要望番号1904 （中）（小）の個数違いによるワーニングを出す
        Dim max As Integer = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows.Count - 1
        For i As Integer = 0 To max

            Dim inkanoM As String = String.Empty
            inkanoM = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows(i).Item("INKA_NO_M").ToString()

            If LMConst.FLG.ON.Equals(Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows(i).Item("SYS_DEL_FLG").ToString()) = False Then

                Call Me._G.SetInkaMInforData(Me._Ds, -1, inkanoM)

                Dim chkResult As Boolean = Me.SetKosuChkMessage(frm)

                If chkResult = False Then
                    '処理終了アクション
                    Call Me.EndAction(frm)

                    '入荷(中)の詳細情報クリア
                    Call Me._G.ClearInkaMControl()
                    frm.sprDetail.CrearSpread()
                    Call Me._G.SetInkaMInforData(Me._Ds, -1, inkanoM)

                    '入荷(小)情報表示
                    Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.SAVE, frm.lblKanriNoM.TextValue)

                    Exit Function
                End If

            End If

        Next

        '--------要望番号1904 （中）（小）の個数違いによるワーニングを出す

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing
        Me._Ds.Merge(New RdPrevInfoDS)
        Me._Ds.Tables(LMConst.RD).Clear()

        If RecordStatus.NOMAL_REC.Equals(frm.lblSituation.RecordStatus) = True Then

            rtnDs = Me.ServerAccess(Me._Ds, Me.GetUpdateAction(frm))

        Else

            rtnDs = Me.ServerAccess(Me._Ds, "InsertSaveAction")

        End If

        'エラーがある場合、終了
        If MyBase.IsErrorMessageExist() = True Then

            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション 
            Call Me.EndAction(frm)
            Return False

        End If

        'プレビューの生成
        Dim prevFrm As RDViewer = Nothing

        '印刷ミスの場合、ガイダンスを表示
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
        Else
            'プレビュー判定 
            Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)
            If prevDt.Rows.Count > 0 Then

                'インスタンス生成
                prevFrm = New RDViewer()

                'データ設定
                prevFrm.DataSource = prevDt

                'プレビュー処理の開始
                prevFrm.Run()

            End If

        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        'INKA_TORI_FLGリセット対象をクリア
        rtnDs.Tables(LMB020C.TABLE_NM_KENPIN_WK_TORI_RESET).Clear()    'ADD 2019/12/02 006350

        '更新結果の反映
        Me._Ds = rtnDs
        Me._DsCmpr = _Ds.Copy

        Dim dr As DataRow = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)
        dr.Item("INKA_STATE_KB_NM") = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_N004, "' AND KBN_CD = '", dr.Item("INKA_STATE_KB").ToString(), "' "))(0).Item("KBN_NM1").ToString()

        '保管・荷役料最終計算日 検索結果 初期化
        Me._DtHokanNiyakuCalculation = Nothing

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMB020C.ActionType.SAVE)

        '値のクリア
        Call Me._G.ClearControl()

        '値の設定
        Call Me._G.SetInitValue(Me._Ds)

        '入荷(小)の情報をクリア
        frm.sprDetail.CrearSpread()

        Call Me.SetInkaMInforData(frm, 0)


        '処理終了メッセージの表示
        '20151020 tsunehira add
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty), String.Concat("[", frm.lblTitleInkaNoL.Text, " = ", frm.lblKanriNoL.TextValue, "]")})

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMB020C.ActionType.SAVE, Me._Ds)

        'ファンクションキーの制御(F2)
        Call Me._G.SetEditBtnEnabled(Me.IsEnabledEditBtn(frm))

        'フォーカスの設定
        Call Me._G.SetFoucus(LMB020C.ActionType.SAVE)

        If prevFrm Is Nothing = False Then

            'プレビューフォームの表示
            prevFrm.Show()

        End If

        Me._Prm.ReturnFlg = True

        Return True

    End Function

#If True Then    'ADD 2022/01/26 026543 【LMS】運送保険料システム化_実装_運送保険申込書対応_入荷機能新規作成

    ''' <summary>
    ''' エラー帳票出力処理
    ''' </summary>
    ''' <returns>出力する場合:False　出力しない場合:True</returns>
    ''' <remarks></remarks>
    Private Function ShowStorePrintData(ByVal frm As LMB020F) As Boolean

        If MyBase.IsMessageStoreExist() = True Then

            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")

            Return False

        End If

        Return True

    End Function
#End If
    ''' <summary>
    ''' Spreadダブルクリック検索処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMB020F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Dim rowNo As Integer = e.Row

        '行がない場合、スルー
        If rowNo < 0 Then
            Exit Sub
        End If

        'スプレッドヘッダー選択の場合、スルー
        If e.ColumnHeader = True Then
            Exit Sub
        End If

        '参照モードの場合、スルー
        If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then

            '入荷(中)の詳細情報表示
            Call Me.SetInkaMInforData(frm, rowNo)

            Exit Sub
        End If

        '処理開始アクション
        Call Me.StartAction(frm)

        '計算処理(オーバーフローチェック用)
        Dim rtnResult As Boolean = Me.AllCalculation(frm)

        '入荷(中)追加処理前チェックを行う。
        rtnResult = rtnResult AndAlso Me._V.IsInkaMAddChk(LMB020C.ActionType.DOUBLECLICK)

        '入力中の値を設定
        rtnResult = rtnResult AndAlso Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.DOUBLECLICK)

        'エラーがある場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '処理終了アクション(ロック制御により解除)
        Call Me.EndAction(frm)

        '入荷(中)の詳細情報表示
        Call Me.SetInkaMInforData(frm, rowNo)

        '計算処理(選択したデータの計算)
        rtnResult = Me.AllCalculation(frm)

        'エラーがある場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'ロック制御
        Call Me._G.SetControlsStatus(LMB020C.ActionType.DOUBLECLICK, Me._Ds)

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub RowLeaveCellSelection(ByVal frm As LMB020F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        Dim rowNo As Integer = e.NewRow

        '行がない場合、スルー
        If rowNo < 0 Then
            Exit Sub
        End If

        'START YANAI 要望番号646
        '同じ行の場合、スルー
        'If e.Row = rowNo Then
        '    Exit Sub
        'End If
        'END YANAI 要望番号646

        '参照モードの場合、表示
        If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then

            '入荷(中)の詳細情報表示
            Call Me.SetInkaMInforData(frm, rowNo)

        End If

        'START YANAI 要望番号646
        '処理開始アクション
        Call Me.StartAction(frm)

        '計算処理(オーバーフローチェック用)
        Dim rtnResult As Boolean = Me.AllCalculation(frm)

        '入荷(中)追加処理前チェックを行う。
        rtnResult = rtnResult AndAlso Me._V.IsInkaMAddChk(LMB020C.ActionType.DOUBLECLICK)

        '入荷(中)追加処理前チェック(リマーク品)
        rtnResult = rtnResult AndAlso Me._V.IsSpreadRemakSet(Me._Ds)

        '入力中の値を設定
        rtnResult = rtnResult AndAlso Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.DOUBLECLICK)

        ''--------要望番号1904 （中）（小）の個数違いによるワーニングを出す
        'Dim chkResult As Boolean = Me.SetKosuChkMessage(frm)

        'If chkResult = False Then
        '    '処理終了アクション
        '    Call Me.EndAction(frm)
        '    Exit Sub
        'End If

        ''--------要望番号1904 （中）（小）の個数違いによるワーニングを出す

        'エラーがある場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '処理終了アクション(ロック制御により解除)
        Call Me.EndAction(frm)

        '入荷(中)の詳細情報表示
        Call Me.SetInkaMInforData(frm, rowNo)

        '計算処理(選択したデータの計算)
        rtnResult = Me.AllCalculation(frm)

        ''--------要望番号1904 （中）（小）の個数違いによるワーニングを出す
        'Dim chkResult As Boolean = Me.SetKosuChkMessage(frm)

        'If chkResult = False Then
        '    '処理終了アクション
        '    Call Me.EndAction(frm)
        '    Exit Sub
        'End If

        ''--------要望番号1904 （中）（小）の個数違いによるワーニングを出す

        'エラーがある場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'ロック制御
        Call Me._G.SetControlsStatus(LMB020C.ActionType.DOUBLECLICK, Me._Ds)
        'END YANAI 要望番号646

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub PrintAction(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.PRINT))

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsPrintChk(Me._Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0))

        'エラーがある場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '印刷処理
        '値設定
        rtnResult = rtnResult AndAlso Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.PRINT)

        '要望番号:1523 terakawa 2012.10.22 Start
        '入荷S存在チェック(チェックリスト印刷のみ)
        If (LMB020C.PRINT_CHECKLIST).Equals(frm.cmbPrint.SelectedValue) AndAlso Me._V.IsInkaSCheck(Me._Ds) = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If
        '要望番号:1523 terakawa 2012.10.22 End

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing
        Me._Ds.Merge(New RdPrevInfoDS) '20110811追加
        Me._Ds.Tables(LMConst.RD).Clear()
        rtnDs = Me.ServerAccess(Me._Ds, "PrintAction")

        'エラーの場合、処理終了
        If MyBase.IsMessageExist() = True Then

            'メッセージ設定
            MyBase.ShowMessage(frm)

        Else
            '処理終了メッセージの表示
            '20151020 tsunehira add
            MyBase.ShowMessage(frm, "G062")
            'MyBase.ShowMessage(frm, "G002", New String() {"印刷処理", String.Empty})

            'エラー帳票出力の判定(EXCEL起動)
            Call Me.ShowStorePrintData(frm)  'ADD 2022/01/26 026543 【LMS】運送保険料システム化_実装_運送保険申込書対応_入荷機能新規作成

            '判定
            Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)
            If prevDt.Rows.Count > 0 Then

                'プレビューの生成
                Dim prevFrm As RDViewer = New RDViewer()

                'データ設定
                prevFrm.DataSource = prevDt

                'プレビュー処理の開始
                prevFrm.Run()

                'プレビューフォームの表示
                prevFrm.Show()

            End If

        End If

        '更新結果の反映（更新日付格納）
        Me._Ds = rtnDs

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 印刷処理 GHSラベル印刷
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub PrintGHSAction(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.PRINT))

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsPrintChk(Me._Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0))

        'エラーがある場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '印刷処理
        '値設定
        rtnResult = rtnResult AndAlso Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.PRINT)

        'LABE_TYPEを退避
        Dim getLABE_TYPE As String = String.Empty
        If frm.cmbPrint.SelectedValue.Equals("06") = True AndAlso _
             frm.cmbLabelTYpe.Visible = True Then
            getLABE_TYPE = CStr(Convert.ToInt32(frm.cmbLabelTYpe.SelectedValue.ToString.Trim))
        Else
            getLABE_TYPE = String.Empty
        End If

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing
        rtnDs = Me.ServerAccess(Me._Ds, "PrintGHSAction")

        'エラーの場合、処理終了
        If MyBase.IsMessageExist() = True Then

            'メッセージ設定
            MyBase.ShowMessage(frm)

        Else
            Dim prm As LMFormData = New LMFormData()

            'パラメータ設定
            prm.ReturnFlg = False

            'GHSラベルCSV出力処理呼出 
            Dim dr As DataRow = rtnDs.Tables("LMB800IN").NewRow()
            dr.Item("LABEL_TYPE") = getLABE_TYPE.ToString
            rtnDs.Tables("LMB800IN").Rows.Add(dr)


            prm.ParamDataSet = rtnDs
            LMFormNavigate.NextFormNavigate(Me, "LMB800", prm)

            '未取得データ取得（PDF名、PDF番号）
            Dim outDr As DataRow() = rtnDs.Tables("LMB800OUT").Select("PDF_NO = '' OR PDF_NM = '' OR FOLDER = ''", "INKA_NO")
            Dim max As Integer = outDr.Length - 1

            If max > -1 Then
                Dim strMsg As String = String.Empty

                For i As Integer = 0 To max

                    strMsg = String.Concat(Mid(outDr(i).Item("INKA_NO").ToString.Trim, 1, 9), "-", outDr(i).Item("GOODS_CD_CUST").ToString.Trim)

                    MyBase.SetMessageStore("00", "E223", New String() {"商品マスタにPDFファイル名またはラベル種類の設定がないため印刷"}, "0", "入荷番号-商品コード", strMsg)

                Next

            End If

            'メッセージコードの判定
            If MyBase.IsMessageStoreExist = True Then
                MyBase.ShowMessage(frm, "E235")
                'EXCEL起動()
                MyBase.MessageStoreDownload()

            Else
                '処理終了メッセージの表示
                MyBase.ShowMessage(frm, "G002", New String() {frm.cmbPrint.SelectedText, String.Empty})

            End If
        End If

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 入荷(中)追加処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub AddInkaMData(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.INIT_M))

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInkaMAddChk(LMB020C.ActionType.INIT_M)

        '入荷(中)の追加処理
        Dim flgMdata As String = String.Empty

        '2014.07.29 Ri Add [ｱｸﾞﾘﾏｰﾄ対応] -ST-
        Dim flgMMerge As String = String.Empty
        '2014.07.29 Ri Add [ｱｸﾞﾘﾏｰﾄ対応] -ED-

        flgMdata = "0" '("1":CSV取り込み　0:入荷M明細 　2:入荷検品WK取込)

        '2014.07.29 Ri Add [ｱｸﾞﾘﾏｰﾄ対応] -ST-
        flgMMerge = "0"
        '2014.07.29 Ri Add [ｱｸﾞﾘﾏｰﾄ対応] -ED-

        rtnResult = rtnResult AndAlso Me.AddInkaMDataAction(frm, flgMdata, flgMMerge)

        '処理終了アクション(ロック制御により解除)
        Call Me.EndAction(frm)

        '入荷(小)の追加処理
        rtnResult = rtnResult AndAlso Me.AddInkaSData(frm, frm.lblKanriNoM.TextValue)

        'ロック制御
        Call Me._G.SetControlsStatus(LMB020C.ActionType.INIT_M, Me._Ds)

        If rtnResult = True Then

            '検索条件クリア
            frm.txtSerchGoodsCd.TextValue = String.Empty
            frm.txtSerchGoodsNm.TextValue = String.Empty

            'スクロールバーを一番下に設定
            Call Me.SetEndScrollGoods(frm)
            Call Me.SetEndScrollDetail(frm)

        End If

    End Sub

    '--------要望番号1904 （中）（小）の個数違いによるワーニングを出す
    ''' <summary>
    ''' （中）（小）の個数違いによるワーニング表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetKosuChkMessage(ByVal frm As LMB020F) As Boolean

        Dim edikosu As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(frm.numEdiKosu.TextValue))
        Dim edisuryo As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(frm.numEdiSuryo.TextValue))
        Dim zero As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue("0"))
        Dim inkanoM As String = frm.lblKanriNoM.TextValue

        '(中)のEDI個数、EDI数量が０以外の場合、(小)の合計個数・合計数量と比較する
        If zero.Equals(edikosu) = False And zero.Equals(edisuryo) = False Then

            Dim sumcnt As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(frm.numSumCnt.TextValue))
            Dim suryo As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(frm.numSuryo.TextValue))

            '(中)のEDI個数・EDI数量が、(小)の合計個数・合計数量と一致しない場合、ワーニング表示
            If edikosu.Equals(sumcnt) = False Or edisuryo.Equals(suryo) = False Then

                MyBase.SetMessage("W231", New String() {frm.lblKanriNoM.TextValue})

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '処理続行
                    Return True

                Else    '「いいえ」を選択
                    '処理を中断して終了
                    Return False
                End If

                '(中)のEDI個数・EDI数量が、(小)の合計個数・合計数量と一致する場合、処理続行
            Else
                Return True
            End If
        Else
            '(中)のEDI個数、EDI数量が０の場合、処理続行
            Return True
        End If

    End Function
    '--------要望番号1904 （中）（小）の個数違いによるワーニングを出す

    ''' <summary>
    ''' 入荷(小)追加処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub AddInkaSData(ByVal frm As LMB020F, Optional ByVal copyFlg As Boolean = False)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.INIT_S))

        Dim arr As ArrayList = Me._V.IsSprSSelectChk()
        Dim cnt As Integer = arr.Count

        '行複写の追加チェック（未選択、単一行選択チェック）
        If copyFlg = True Then
            rtnResult = rtnResult AndAlso Me._LMBconV.IsSelectChk(cnt)
            rtnResult = rtnResult AndAlso Me._LMBconV.IsSelectOneChk(cnt)

            'START YANAI 要望番号557
            rtnResult = rtnResult AndAlso Me._V.IsInkaNoSOverChk()
            'END YANAI 要望番号557

        End If

        '入荷中番の必須チェック
        rtnResult = rtnResult AndAlso Me._V.IsInkaMNoHissuChk()

        '処理終了アクション(ロック制御により解除)
        Call Me.EndAction(frm)

        '入荷(小)の追加処理
        If copyFlg = True Then
            'START YANAI 要望番号557
            'rtnResult = rtnResult AndAlso Me.AddInkaSData(frm, frm.lblKanriNoM.TextValue, Convert.ToInt32(arr(0)))
            Dim max As Integer = Convert.ToInt32(frm.numRowCopyScnt.TextValue) - 1
            For i As Integer = 0 To max
                rtnResult = rtnResult AndAlso Me.AddInkaSData(frm, frm.lblKanriNoM.TextValue, Convert.ToInt32(arr(0)), i, max)
            Next
            'END YANAI 要望番号557
        Else
            rtnResult = rtnResult AndAlso Me.AddInkaSData(frm, frm.lblKanriNoM.TextValue)
        End If

        If rtnResult = True Then

            'スクロールバーを一番下に設定
            Call Me.SetEndScrollDetail(frm)

        End If

    End Sub

    ''' <summary>
    ''' 分析票追加処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub AddCoaMst(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.COA))

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMBconH.GetCheckList(frm.sprDetail.ActiveSheet, LMB020G.sprDetailDef.DEF.ColNo)
        End If

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._LMBconV.IsSelectChk(arr.Count)
        '単一行選択チェック
        rtnResult = rtnResult AndAlso Me._LMBconV.IsSelectOneChk(arr.Count)

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'パラメータのインスタンス生成
        Dim prm As LMFormData = New LMFormData()

        prm.ParamDataSet = Me.SetDataSetLMM020InData(frm, arr)

        '分析票管理マスタメンテ画面に遷移
        LMFormNavigate.NextFormNavigate(Me, "LMM020", prm)

        '処理終了アクション(ロック制御により解除)
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 分析票管理マスタメンテ画面のパラメータ設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMM020InData(ByVal frm As LMB020F, ByVal arr As ArrayList) As DataSet

        Dim ds As DataSet = New LMM020DS()
        Dim dt As DataTable = ds.Tables(LMControlC.LMM020C_TABLE_NM_IN)
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        For i As Integer = 0 To max

            dr = dt.NewRow()
            With dr
                .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                .Item("GOODS_CD_CUST") = frm.lblGoodsCd.TextValue
                .Item("GOODS_NM_1") = frm.lblGoodsNm.TextValue
                .Item("GOODS_CD_NRS") = frm.lblGoodsCdNrs.TextValue
                .Item("LOT_NO") = Me._LMBconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMB020G.sprDetailDef.LOT_NO.ColNo))
                .Item("INKA_DATE") = frm.imdNyukaDate.TextValue 'ADD 2018/11/14 要望番号001939
            End With
            dt.Rows.Add(dr)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' イエローカード追加処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub AddYCardMst(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.YCARD))

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMBconH.GetCheckList(frm.sprDetail.ActiveSheet, LMB020G.sprDetailDef.DEF.ColNo)
        End If

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._LMBconV.IsSelectChk(arr.Count)
        '単一行選択チェック
        rtnResult = rtnResult AndAlso Me._LMBconV.IsSelectOneChk(arr.Count)

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'パラメータのインスタンス生成
        Dim prm As LMFormData = New LMFormData()

        prm.ParamDataSet = Me.SetDataSetLMM530InData(frm, arr)

        'イエローカード管理マスタメンテ画面に遷移
        LMFormNavigate.NextFormNavigate(Me, "LMM530", prm)

        '処理終了アクション(ロック制御により解除)
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' イエローカード管理マスタメンテ画面のパラメータ設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMM530InData(ByVal frm As LMB020F, ByVal arr As ArrayList) As DataSet

        Dim ds As DataSet = New LMM530DS()
        Dim dt As DataTable = ds.Tables("LMM530IN")
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        For i As Integer = 0 To max

            dr = dt.NewRow()
            With dr
                .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                .Item("GOODS_CD_CUST") = frm.lblGoodsCd.TextValue
                .Item("GOODS_NM_1") = frm.lblGoodsNm.TextValue
                .Item("GOODS_CD_NRS") = frm.lblGoodsCdNrs.TextValue
                .Item("LOT_NO") = Me._LMBconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMB020G.sprDetailDef.LOT_NO.ColNo))
            End With
            dt.Rows.Add(dr)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 画面の値によるロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetLockControl()

        'ロック制御
        Call Me._G.SetLockControl(LMB020C.ActionType.TEHAI_CHANGED, Me._G.LockTripControl(Me._Ds))

    End Sub

    ''' <summary>
    ''' 運送課税区分の初期値設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetUnchinUmuInitCd(ByVal frm As LMB020F)

        With frm

            '参照の場合、スルー
            If DispMode.VIEW.Equals(.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            Dim tehaiKbn As String = .cmbUnchinUmu.SelectedValue.ToString()

            '値がない場合、スルー
            If String.IsNullOrEmpty(tehaiKbn) = True OrElse _
                ("10").Equals(tehaiKbn) = False Then
                Exit Sub
            End If

            'START YANAI 要望番号602
            'Dim drs As DataRow() = Me._LMBconV.SelectCustListDataRow( _
            '                                                         .txtCustCdL.TextValue _
            '                                                         , .txtCustCdM.TextValue _
            '                                                        )

            ''取得できた場合、初期値設定
            'If 0 < drs.Length Then
            '    '運送課税区分の初期値設定
            '    .cmbTax.SelectedValue = drs(0).Item("TAX_KB").ToString
            'End If
            '運送課税区分の初期値設定
            .cmbTax.SelectedValue = LMB020C.TAX_KB01
            'END YANAI 要望番号602


            '要望番号1357:(輸送営業所に初期値設定し、必須チェックを入れる) 2012/08/22 本明 Start
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.cmbYusoBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            .cmbYusoBrCd.SelectedValue = frm.cmbEigyo.SelectedValue.ToString()
            '要望番号1357:(輸送営業所に初期値設定し、必須チェックを入れる) 2012/08/22 本明 End

            'ADD Start 2018/10/25 要望番号001820
            Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
            Dim custLCd As String = frm.txtCustCdL.TextValue
            Dim custMCd As String = frm.txtCustCdM.TextValue
            Dim inkaOrigCd As String

            '荷主マスタの取得
            Dim custDrs As DataRow() = Me._LMBconV.SelectCustListDataRow(custLCd, custMCd, "00", "00")
            If 0 < custDrs.Length Then
                inkaOrigCd = custDrs(0).Item("INKA_ORIG_CD").ToString()

                If (Not String.IsNullOrEmpty(inkaOrigCd)) Then
                    '届先マスタの取得
                    Dim destDrs As DataRow() = Me._LMBconV.SelectDestListDataRow(brCd, custLCd, inkaOrigCd)
                    If 0 < destDrs.Length Then
                        'マスタの値を設定
                        .txtShukkaMotoCD.TextValue = destDrs(0).Item("DEST_CD").ToString()
                        .lblShukkaMotoNM.TextValue = destDrs(0).Item("DEST_NM").ToString()
                    End If
                End If
            End If
            'ADD End   2018/10/25 要望番号001820

        End With

    End Sub

    ''' <summary>
    ''' タリフコードの初期値設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetTariffInitCd(ByVal frm As LMB020F)

        With frm

            '参照の場合、スルー
            If DispMode.VIEW.Equals(.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            Dim tariffKbn As String = .cmbUnchinKbn.SelectedValue.ToString()

            '値がない場合、スルー
            If String.IsNullOrEmpty(tariffKbn) = True Then
                Exit Sub
            End If

            'ロック制御
            Call Me._G.SetLockControl(LMB020C.ActionType.TARIFF_CHANGED, Me._G.LockTripControl(Me._Ds))

            'START YANAI 要望番号1386 タリフ分類区分を変更した際に誤ったタリフを設定する
            'Dim drs As DataRow() = Me._LMBconV.SelectTariffSetListDataRow( _
            '                                                              .cmbEigyo.SelectedValue.ToString() _
            '                                                            , .txtCustCdL.TextValue _
            '                                                            , .txtCustCdM.TextValue _
            '                                                            , String.Empty _
            '                                                            , tariffKbn _
            '                                                            )
            Dim setKb As String = "02"
            Dim drs As DataRow() = Me._LMBconV.SelectTariffSetListDataRow( _
                                                                          .cmbEigyo.SelectedValue.ToString() _
                                                                        , .txtCustCdL.TextValue _
                                                                        , .txtCustCdM.TextValue _
                                                                        , String.Empty _
                                                                        , tariffKbn _
                                                                        , String.Empty _
                                                                        , String.Empty _
                                                                        , String.Empty _
                                                                        , String.Empty _
                                                                        , setKb _
                                                                        )
            'END YANAI 要望番号1386 タリフ分類区分を変更した際に誤ったタリフを設定する

            Dim tariffCd As String = String.Empty
            Dim tariffNm As String = String.Empty

            '取得できた場合、初期値設定
            If 0 < drs.Length Then

                Select Case tariffKbn

                    Case LMB020C.TARIFF_KONSAI

                        tariffCd = drs(0).Item("UNCHIN_TARIFF_CD1").ToString()

                    Case LMB020C.TARIFF_KURUMA

                        tariffCd = drs(0).Item("UNCHIN_TARIFF_CD2").ToString()

                    Case LMB020C.TARIFF_YOKO

                        tariffCd = drs(0).Item("YOKO_TARIFF_CD").ToString()

                End Select

                If LMB020C.TARIFF_YOKO.Equals(tariffKbn) = True Then
                    Dim drsT As DataRow() = Me._LMBconV.SelectYokoTariffListDataRow( _
                                                                                    .cmbEigyo.SelectedValue.ToString() _
                                                                                  , tariffCd)
                    If 0 < drsT.Length Then
                        tariffNm = drsT(0).Item("YOKO_REM").ToString()
                    End If
                Else
                    Dim drsT As DataRow() = Me._LMBconV.SelectUnchinTariffListDataRow(tariffCd)
                    If 0 < drsT.Length Then
                        tariffNm = drsT(0).Item("UNCHIN_TARIFF_REM").ToString()
                    End If

                End If

            End If

            'タリフコードの初期値設定
            .txtUnsoTariffCD.TextValue = tariffCd
            .lblUnsoTariffNM.TextValue = tariffNm


        End With

    End Sub

    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
    ''' <summary>
    ''' 支払タリフコードの初期値設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetShiharaiTariffInitCd(ByVal frm As LMB020F)

        With frm

            '参照の場合、スルー
            If DispMode.VIEW.Equals(.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            Dim tariffKbn As String = .cmbUnchinKbn.SelectedValue.ToString()

            '値がない場合、スルー
            If String.IsNullOrEmpty(tariffKbn) = True Then
                Exit Sub
            End If

            'ロック制御
            Call Me._G.SetLockControl(LMB020C.ActionType.TARIFF_CHANGED, Me._G.LockTripControl(Me._Ds))

            ''Dim drs As DataRow() = Me._LMBconV.SelectShiharaiTariffSetListDataRow( _
            ''                                                              .cmbEigyo.SelectedValue.ToString() _
            ''                                                            , .txtCustCdL.TextValue _
            ''                                                            , .txtCustCdM.TextValue _
            ''                                                            , String.Empty _
            ''                                                            , tariffKbn _
            ''                                                            )

            Dim tariffCd As String = String.Empty
            Dim tariffNm As String = String.Empty

            ' ''取得できた場合、初期値設定
            ''If 0 < drs.Length Then

            ''    Select Case tariffKbn

            ''        Case LMB020C.TARIFF_KONSAI

            ''            tariffCd = drs(0).Item("UNCHIN_TARIFF_CD1").ToString()

            ''        Case LMB020C.TARIFF_KURUMA

            ''            tariffCd = drs(0).Item("UNCHIN_TARIFF_CD2").ToString()

            ''        Case LMB020C.TARIFF_YOKO

            ''            tariffCd = drs(0).Item("YOKO_TARIFF_CD").ToString()

            ''    End Select

            ''    If LMB020C.TARIFF_YOKO.Equals(tariffKbn) = True Then
            ''        Dim drsT As DataRow() = Me._LMBconV.SelectYokoTariffListDataRow( _
            ''                                                                        .cmbEigyo.SelectedValue.ToString() _
            ''                                                                      , tariffCd)
            ''        If 0 < drsT.Length Then
            ''            tariffNm = drsT(0).Item("YOKO_REM").ToString()
            ''        End If
            ''    Else
            ''        Dim drsT As DataRow() = Me._LMBconV.SelectUnchinTariffListDataRow(tariffCd)
            ''        If 0 < drsT.Length Then
            ''            tariffNm = drsT(0).Item("UNCHIN_TARIFF_REM").ToString()
            ''        End If

            ''    End If

            ''End If

            'タリフコードの初期値設定
            ''.txtShiharaiTariffCD.TextValue = tariffCd
            ''.lblShiharaiTariffNM.TextValue = tariffNm


        End With

    End Sub
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End


    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub IkkatsuHenkoAction(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.HENKO))

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then

            arr = Me._LMBconH.GetCheckList(frm.sprGoodsDef.ActiveSheet, LMB020G.sprGoodsDef.DEF.ColNo)

        End If

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._LMBconV.IsSelectChk(arr.Count)

        '入力チェック(行追加と同様のチェックを行う)
        rtnResult = rtnResult AndAlso Me._V.IsInkaMAddChk(LMB020C.ActionType.HENKO, arr)

        '入力中のデータを設定
        rtnResult = rtnResult AndAlso Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.HENKO)

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'クリア前に入荷中番を保持
        Dim inkaNoM As String = frm.lblKanriNoM.TextValue

        '画面の入荷(中)の情報をクリア
        Call Me._G.ClearInkaMControl()

        '画面の入荷(小)の情報をクリア
        frm.sprDetail.CrearSpread()

        '一括変更処理
        Me._Ds = Me.SetIkkatsuHenkoData(frm, arr)

        '保持してある値を反映
        frm.lblKanriNoM.TextValue = inkaNoM

        '入荷(中)情報表示
        Call Me._G.SetInkaMData(Me._Ds)

        '元のレコードの詳細情報表示
        Call Me._G.ClearInkaMControl()
        Call Me._G.SetInkaMInforData(Me._Ds, -1, inkaNoM)

        '処理終了アクション(ロック制御により解除)
        Call Me.EndAction(frm)

        '入荷(小)情報表示
        Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.EDIT, inkaNoM)

    End Sub

    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ChangeToshitsuZone(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.HENKO))

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then

            arr = Me._LMBconH.GetCheckList(frm.sprGoodsDef.ActiveSheet, LMB020G.sprGoodsDef.DEF.ColNo)

        End If

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._LMBconV.IsSelectChk(arr.Count)

        '入力チェック(行追加と同様のチェックを行う)
        'rtnResult = rtnResult AndAlso Me._V.IsInkaMAddChk(LMB020C.ActionType.DEL_S)
        rtnResult = rtnResult AndAlso Me._V.IsAllChangeInputChk()

        '入力中のデータを設定
        rtnResult = rtnResult AndAlso Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.HENKO)

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'クリア前に入荷中番を保持
        Dim inkaNoM As String = frm.lblKanriNoM.TextValue

        '画面の入荷(中)の情報をクリア
        Call Me._G.ClearInkaMControl()

        '画面の入荷(小)の情報をクリア
        frm.sprDetail.CrearSpread()

        '一括変更処理
        Me._Ds = Me.SetToSituZoneData(frm, arr)

        '保持してある値を反映
        frm.lblKanriNoM.TextValue = inkaNoM

        '入荷(中)情報表示
        Call Me._G.SetInkaMData(Me._Ds)

        '元のレコードの詳細情報表示
        Call Me._G.ClearInkaMControl()
        Call Me._G.SetInkaMInforData(Me._Ds, -1, inkaNoM)

        '処理終了アクション(ロック制御により解除)
        Call Me.EndAction(frm)

        '入荷(小)情報表示
        Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.EDIT, inkaNoM)

    End Sub

    'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
    ''' <summary>
    ''' 運送会社変更処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub LeaveUnsoCdAction(ByVal frm As LMB020F)

        '運送会社コードOLD設定
        Me._G.SetUnsoCdOld(frm)

        '運賃タリフセットからタリフコードを設定
        Call Me._G.GetUnchinTariffSet(frm, False)

    End Sub
    'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseForm(ByVal frm As LMB020F, ByVal e As FormClosingEventArgs)

        '編集モード以外なら処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = False OrElse AllFKeyOffChk(frm) Then
            Exit Sub
        End If

        'メッセージの表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes '「はい」押下時

                '保存処理
                If Me.SaveInkaItemData(frm, LMB020C.ActionType.CLOSE) = False Then
                    e.Cancel = True
                End If

            Case MsgBoxResult.Cancel '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

#Region "営業日取得"
    '要望番号2690 前営業日・翌営業日対応
    ''' <summary>
    ''' 営業日取得
    ''' </summary>
    ''' <param name="sStartDay"></param>
    ''' <param name="iBussinessDays"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBussinessDay(ByVal sStartDay As String, ByVal iBussinessDays As Integer) As DateTime
        'sStartDate     ：基準日（YYYYMMDD形式）
        'iBussinessDays ：基準日からの営業日数（前々営業日の場合は-2、前営業日の場合は-1、翌営業日の場合は+1、翌々営業日の場合は+2）
        '戻り値         ：求めた営業日（YYYY/MM/DD形式）

        'スラッシュを付加して日付型に変更
        Dim dBussinessDate As DateTime = Convert.ToDateTime((Convert.ToInt32(sStartDay)).ToString("0000/00/00"))

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

                    '該当する日付が休日マスタに存在するか？
                    Dim sBussinessDate As String = Format(dBussinessDate, "yyyyMMdd")
                    Dim holDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.HOL).Select(" SYS_DEL_FLG = '0' AND HOL = '" & sBussinessDate & "'")
                    If holDr.Count = 0 Then
                        '休日マスタに存在しない場合、dBussinessDateが求める日
                        Exit Do
                    End If

                End If
            Loop
        Next

        Return dBussinessDate

    End Function
#End Region

#Region "実行押下処理"
    ''' <summary>
    ''' 実行押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub JikkouAction(ByRef frm As LMB020F)

        Dim prm As LMFormData = New LMFormData()

        '処理開始アクション
        Call Me.StartAction(frm)

        '単項目チェック
        If Me._V.IsJikkouSingleCheck(Me._Ds) = False Then
            Call Me.EndAction(frm)
            Exit Sub
        End If

        If ("01").Equals(frm.cmbJikkou.SelectedValue) = True Then
            '文書管理を呼ぶ
            Me.ShowBunshoKanri(frm, prm, frm.lblKanriNoL.TextValue, "47")

        ElseIf ("02").Equals(frm.cmbJikkou.SelectedValue) = True Then
            '現場作業指示取り消し
            Me.WHSagyoSiji(frm, LMB810C.PROC_TYPE.CANCEL)

        ElseIf ("03").Equals(frm.cmbJikkou.SelectedValue) = True Then
            '現場作業指示
            Me.WHSagyoSiji(frm, LMB810C.PROC_TYPE.INSTRUCT)

        End If

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

#End Region

#Region "実行押下処理"
    ''' <summary>
    ''' 実行押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub AddImgAction(ByRef frm As LMB020F)

        Dim prm As LMFormData = New LMFormData()

        '処理開始アクション
        Call Me.StartAction(frm)

        '単項目チェック
        If Me._V.IsAddImgSingleCheck() = False Then
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '文書管理を呼ぶ
        Dim keyNo As String = String.Empty
        Dim inkaNoS As String = String.Empty
        For i As Integer = 0 To frm.sprDetail.ActiveSheet.Rows.Count - 1
            If LMConst.FLG.ON.Equals(Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMB020G.sprDetailDef.DEF.ColNo))) Then
                inkaNoS = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMB020G.sprDetailDef.KANRI_NO_S.ColNo))
            End If
        Next

        keyNo = String.Concat(frm.lblKanriNoL.TextValue, frm.lblKanriNoM.TextValue, inkaNoS)

        Me.ShowBunshoKanri(frm, prm, keyNo, "44")

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

#End Region

    'ADD 2022/11/07 倉庫写真アプリ対応 START
#Region "写真選択押下処理"
    ''' <summary>
    ''' 写真選択押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub PhotoSelAction(ByRef frm As LMB020F)

        Dim prm As LMFormData = New LMFormData()

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.PHOTOSEL))

        '選択チェック
        rtnResult = rtnResult AndAlso Me._V.IsAddImgSingleCheck()

        'エラーがある場合、スルー
        If rtnResult = False Then
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '写真選択を呼ぶ
        Dim inkaNoS As String = String.Empty
        Dim rowNo As Integer = 0
        For i As Integer = 0 To frm.sprDetail.ActiveSheet.Rows.Count - 1
            If LMConst.FLG.ON.Equals(Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMB020G.sprDetailDef.DEF.ColNo))) Then
                inkaNoS = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMB020G.sprDetailDef.KANRI_NO_S.ColNo))
                rowNo = i
            End If
        Next

        Me.ShowPhotoSel(frm, prm, rowNo, frm.lblKanriNoL.TextValue, frm.lblKanriNoM.TextValue, inkaNoS)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

#End Region
    'ADD 2022/11/07 倉庫写真アプリ対応 END

#Region "タブレット取込"
    ''' <summary>
    ''' タブレット取込
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub TabletTorikomiAction(ByVal frm As LMB020F)
        'パラメータクラス生成(20130215)
        Dim prm As LMFormData = New LMFormData()

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.INIT))

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If


        'タブレットデータ取得
        Dim ds As DataSet = GetTabletData(frm)

        If MyBase.IsMessageExist = True Then
            MyBase.ShowMessage(frm)
            Call Me.EndAction(frm)
            Exit Sub
        End If

        Using wkData As New LMB020DS
            wkData.Merge(_Ds)
            wkData.Tables(LMB020C.TABLE_NM_TAB_DTL).Clear()
            wkData.Tables(LMB020C.TABLE_NM_TAB_DTL).Merge(ds.Tables(LMB020C.TABLE_NM_TAB_DTL))

            Dim isLoaded As Boolean = False

            ' タブレット明細から取り込む
            If (wkData.LMB020_TAB_DTL.Count > 0) Then

                Dim existsWarning As Boolean = False

                ' 検品データが存在しない入荷Sの行情報を格納
                Dim noInspectedRows As New List(Of LMB020DS.LMB020_INKA_SRow)

                ' 取込データ反映後の新しい入荷Sの行情報を格納
                Dim newTable As New LMB020DS.LMB020_INKA_SDataTable

                ' 入荷Mチェック
                For Each inkaMRow As LMB020DS.LMB020_INKA_MRow In wkData.LMB020_INKA_M

                    If (LMB020C.HIKIATE_ARI.Equals(inkaMRow.HIKIATE)) Then

                        ' 引当中の商品が存在するため取込できません。
                        MyBase.ShowMessage(frm, "E937")
                        Call Me.EndAction(frm)
                        Exit Sub
                    End If

                Next

                '入荷S更新
                For Each tabRow As LMB020DS.LMB020_TAB_DTLRow In wkData.LMB020_TAB_DTL

                    Dim inkaSRow As IEnumerable(Of LMB020DS.LMB020_INKA_SRow) _
                                = wkData.LMB020_INKA_S _
                                .Where(Function(q) tabRow.INKA_NO_L.Equals(q.INKA_NO_L) AndAlso _
                                                   tabRow.INKA_NO_M.Equals(q.INKA_NO_M) AndAlso _
                                                   tabRow.INKA_NO_S.Equals(q.INKA_NO_S))

                    If inkaSRow.Count > 0 Then
                        '既存レコードを更新
                        If LMConst.FLG.ON.Equals(tabRow.SYS_DEL_FLG) = True Then
                            inkaSRow(0).SYS_DEL_FLG = LMConst.FLG.ON

                        Else

                            If (String.IsNullOrWhiteSpace(tabRow.LOT_NO) = False) Then
                                inkaSRow(0).LOT_NO = tabRow.LOT_NO
                            End If

                            If (String.IsNullOrWhiteSpace(tabRow.TOU_NO) = False) Then
                                inkaSRow(0).TOU_NO = tabRow.TOU_NO
                            End If

                            If (String.IsNullOrWhiteSpace(tabRow.SITU_NO) = False) Then
                                inkaSRow(0).SITU_NO = tabRow.SITU_NO
                            End If

                            If (String.IsNullOrWhiteSpace(tabRow.ZONE_CD) = False) Then
                                inkaSRow(0).ZONE_CD = tabRow.ZONE_CD
                            End If

                            If (String.IsNullOrWhiteSpace(tabRow.LOCA) = False) Then
                                inkaSRow(0).LOCA = tabRow.LOCA
                            End If

                            If (String.IsNullOrWhiteSpace(tabRow.SERIAL_NO) = False) Then
                                inkaSRow(0).SERIAL_NO = tabRow.SERIAL_NO
                            End If

                            If (String.IsNullOrWhiteSpace(tabRow.IRIME) = False) Then
                                inkaSRow(0).IRIME = tabRow.IRIME
                            End If

                            inkaSRow(0).REMARK = tabRow.REMARK

                            inkaSRow(0).GOODS_COND_KB_1 = tabRow.GOODS_COND_KB_1
                            inkaSRow(0).GOODS_COND_KB_2 = tabRow.GOODS_COND_KB_2
                            inkaSRow(0).GOODS_COND_KB_3 = tabRow.GOODS_COND_KB_3

                            Dim kosu As Decimal = (Convert.ToDecimal(tabRow.KONSU) * Convert.ToDecimal(tabRow.PKG_NB)) + Convert.ToDecimal(tabRow.HASU)
                            inkaSRow(0).KOSU_S = kosu.ToString
                            inkaSRow(0).SURYO_S = (Convert.ToDecimal(tabRow.IRIME) * kosu).ToString()

                            Dim remainder As Long = 0
                            inkaSRow(0).KONSU = tabRow.KONSU
                            inkaSRow(0).HASU = tabRow.HASU
                            inkaSRow(0).BETU_WT = tabRow.BETU_WT
                            inkaSRow(0).JURYO_S = (kosu * Convert.ToDecimal(tabRow.STD_WT_KGS)).ToString()
                            inkaSRow(0).IMG_YN = tabRow.IMG_YN
                            inkaSRow(0).BUG_YN = tabRow.BUG_FLG

                        End If

                    Else
                        If LMConst.FLG.ON.Equals(tabRow.SYS_DEL_FLG) Then
                            'LMSにデータなし、タブレットにデータあり、タブレットが削除のときはデータ登録しない
                        Else
                            '新規レコードを追加
                            Dim newRow As LMB020DS.LMB020_INKA_SRow = newTable.NewLMB020_INKA_SRow

                            newRow.NRS_BR_CD = tabRow.NRS_BR_CD
                            newRow.INKA_NO_L = tabRow.INKA_NO_L
                            newRow.INKA_NO_M = tabRow.INKA_NO_M
                            newRow.INKA_NO_S = tabRow.INKA_NO_S
                            newRow.LOT_NO = tabRow.LOT_NO
                            newRow.LOCA = tabRow.LOCA
                            newRow.TOU_NO = tabRow.TOU_NO
                            newRow.SITU_NO = tabRow.SITU_NO
                            newRow.ZONE_CD = tabRow.ZONE_CD
                            newRow.KONSU = tabRow.KONSU
                            newRow.HASU = tabRow.HASU
                            newRow.IRIME = tabRow.IRIME
                            newRow.BETU_WT = tabRow.BETU_WT
                            newRow.SERIAL_NO = tabRow.SERIAL_NO
                            newRow.GOODS_COND_KB_1 = tabRow.GOODS_COND_KB_1
                            newRow.GOODS_COND_KB_2 = tabRow.GOODS_COND_KB_2
                            newRow.GOODS_COND_KB_3 = tabRow.GOODS_COND_KB_3
                            newRow.GOODS_CRT_DATE = tabRow.GOODS_CRT_DATE
                            newRow.LT_DATE = tabRow.LT_DATE
                            newRow.SPD_KB = tabRow.SPD_KB
                            newRow.OFB_KB = tabRow.OFB_KB
                            newRow.DEST_CD = tabRow.DEST_CD
                            newRow.REMARK = tabRow.REMARK
                            newRow.ALLOC_PRIORITY = tabRow.ALLOC_PRIORITY
                            newRow.REMARK_OUT = tabRow.REMARK_OUT
                            newRow.SYS_DEL_FLG = LMConst.FLG.OFF
                            newRow.STD_IRIME_UT = tabRow.IRIME_UT

                            Dim kosu As Decimal = (Convert.ToDecimal(tabRow.KONSU) * Convert.ToDecimal(tabRow.PKG_NB)) + Convert.ToDecimal(tabRow.HASU)
                            newRow.KOSU_S = kosu.ToString
                            newRow.SURYO_S = (Convert.ToDecimal(tabRow.IRIME) * kosu).ToString()
                            newRow.JURYO_S = (kosu * Convert.ToDecimal(tabRow.STD_WT_KGS)).ToString()
                            newRow.DEST_NM = tabRow.DEST_NM
                            newRow.UP_KBN = LMConst.FLG.OFF
                            newRow.BUG_YN = tabRow.BUG_FLG
                            newRow.STD_IRIME_NM = tabRow.STD_IRIME_NM
                            newRow.STD_WT_KGS = tabRow.STD_WT_KGS
                            newRow.IMG_YN = tabRow.IMG_YN

                            newRow.SUM_KONSU_S = ""
                            newRow.ZAI_REC_NO = ""
                            newRow.ZAI_REC_CNT = ""
                            newRow.LOT_CTL_KB = ""
                            newRow.LT_DATE_CTL_KB = ""
                            newRow.CRT_DATE_CTL_KB = ""
                            newRow.TORIKOMI_GOODS_KANRI_NO = ""
                            newRow.EXISTS_REMARK = ""

                            newTable.AddLMB020_INKA_SRow(newRow)

                            Dim inkaSMaxRow As IEnumerable(Of LMB020DS.LMB020_MAX_NORow) _
                                = wkData.LMB020_MAX_NO _
                                .Where(Function(q) tabRow.INKA_NO_M.Equals(q.INKA_NO_M))

                            If inkaSMaxRow.Count > 0 Then
                                inkaSMaxRow(0).MAX_INKA_NO_S = tabRow.INKA_NO_S
                            End If

                        End If

                    End If
                Next
                wkData.Tables(LMB020C.TABLE_NM_INKA_S).Merge(newTable)

                ' 検品データが存在しない行に削除フラグを設定
                For Each deleteRow As LMB020DS.LMB020_INKA_SRow In noInspectedRows
                    If (LMConst.FLG.ON.Equals(deleteRow.UP_KBN)) Then
                        deleteRow.SYS_DEL_FLG = LMConst.FLG.ON
                        newTable.ImportRow(deleteRow)

                    End If
                Next
            End If

            _Ds.Tables(LMB020C.TABLE_NM_INKA_S).Clear()
            _Ds.Tables(LMB020C.TABLE_NM_INKA_S).Merge(wkData.Tables(LMB020C.TABLE_NM_INKA_S))

            _DsCmpr.Tables(LMB020C.TABLE_NM_INKA_S).Clear()
            _DsCmpr.Tables(LMB020C.TABLE_NM_INKA_S).Merge(wkData.Tables(LMB020C.TABLE_NM_INKA_S))

            _Ds.Tables(LMB020C.TABLE_NM_MAX_NO).Clear()
            _Ds.Tables(LMB020C.TABLE_NM_MAX_NO).Merge(wkData.Tables(LMB020C.TABLE_NM_MAX_NO))

            _DsCmpr.Tables(LMB020C.TABLE_NM_MAX_NO).Clear()
            _DsCmpr.Tables(LMB020C.TABLE_NM_MAX_NO).Merge(wkData.Tables(LMB020C.TABLE_NM_MAX_NO))

            'ロケ設定済のとき完了に変更
            If LMB020C.WH_TAB_SAGYO_04.Equals(frm.cmbWHSagyoStatus.SelectedValue) Then
                frm.cmbWHSagyoStatus.SelectedValue = LMB020C.WH_TAB_SAGYO_05
                frm.cmbWHSagyoStatus.Refresh()
            End If

            Me.SetInkaMInforData(frm, 0)
            For Each inkaMRow As LMB020DS.LMB020_INKA_MRow In wkData.LMB020_INKA_M

                For Each inkaSRow As LMB020DS.LMB020_INKA_SRow In wkData.LMB020_INKA_S _
                    .Where(Function(s) s.INKA_NO_L.Equals(inkaMRow.INKA_NO_L) AndAlso _
                                       s.INKA_NO_M.Equals(inkaMRow.INKA_NO_M))

                    ' 在庫再設定
                    Me.SetZaikoData(_Ds, inkaMRow, inkaSRow)

                    If (LMConst.FLG.ON.Equals(inkaSRow.SYS_DEL_FLG)) Then

                        Me.DeleteTabelData(_Ds.Tables(LMB020C.TABLE_NM_ZAI) _
                                         , String.Format("INKA_NO_M = '{0}' AND INKA_NO_S = '{1}'" _
                                                        , inkaSRow.INKA_NO_M _
                                                        , inkaSRow.INKA_NO_S))
                    End If
                Next
            Next

            '取込完了時、フラグをONに変更する
            frm.txtWhWorkImpFlg.TextValue = LMConst.FLG.ON
            '取込完了後再指示不要にチェックを付与
            frm.chkNoSiji.Checked = True
        End Using

        MyBase.ShowMessage(frm, "G099")

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' タブレットデータ取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function GetTabletData(ByVal frm As LMB020F) As DataSet

        Dim prmDs As DataSet = Me._Ds.Clone
        Dim dr As DataRow = prmDs.Tables("LMB020IN").NewRow

        dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr.Item("INKA_NO_L") = frm.lblKanriNoL.TextValue
        prmDs.Tables("LMB020IN").Rows.Add(dr)
        prmDs = Me.ServerAccess(prmDs, "WHSagyoTorikomi")

        Return prmDs

    End Function

#End Region

#Region "現場作業指示"
    ''' <summary>
    ''' 現場作業指示
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub WHSagyoSiji(ByVal frm As LMB020F, ByVal procType As String)

        'パラメータのインスタンス生成
        Dim ds As DataSet = New LMB810DS()
        Dim dr As DataRow = Nothing

        Dim tabStatus As String = String.Empty
        Select Case procType
            Case LMB810C.PROC_TYPE.INSTRUCT
                tabStatus = LMB810C.WH_TAB_SIJI_STATUS.INSTRUCTED
            Case LMB810C.PROC_TYPE.CANCEL
                tabStatus = LMB810C.WH_TAB_SIJI_STATUS.NOT_INSTRUCTED
            Case LMB810C.PROC_TYPE.DELETE
                tabStatus = LMB810C.WH_TAB_SIJI_STATUS.NOT_INSTRUCTED
        End Select

        'IN情報を設定
        dr = ds.Tables("LMB810IN").NewRow()
        With dr

            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
            .Item("INKA_NO_L") = frm.lblKanriNoL.TextValue
            .Item("INKA_STATE_KB") = _Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("INKA_STATE_KB").ToString
            .Item("WH_TAB_WORK_STATUS_KB") = frm.cmbWHSagyoStatus.SelectedValue
            .Item("ROW_NO") = String.Empty
            .Item("SYS_UPD_DATE") = _Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("SYS_UPD_DATE").ToString
            .Item("SYS_UPD_TIME") = _Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("SYS_UPD_TIME").ToString
            .Item("WH_TAB_STATUS_KB") = tabStatus
            .Item("PROC_TYPE") = procType

        End With
        ds.Tables("LMB810IN").Rows.Add(dr)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'パラメータ設定
        prm.ReturnFlg = False

        prm.ParamDataSet = ds
        LMFormNavigate.NextFormNavigate(Me, "LMB810", prm)

        '処理結果反映
        If prm.ReturnFlg = True Then
            If prm.ParamDataSet.Tables("LMB810OUT_UPD_RESULTS").Rows.Count > 0 Then
                _Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("WH_TAB_SAGYO_SIJI_STATUS") = prm.ParamDataSet.Tables("LMB810OUT_UPD_RESULTS").Rows(0).Item("WH_TAB_STATUS").ToString
                _Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("SYS_UPD_DATE") = prm.ParamDataSet.Tables("LMB810OUT_UPD_RESULTS").Rows(0).Item("SYS_UPD_DATE").ToString
                _Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("SYS_UPD_TIME") = prm.ParamDataSet.Tables("LMB810OUT_UPD_RESULTS").Rows(0).Item("SYS_UPD_TIME").ToString

                frm.cmbWHSagyoSijiStatus.SelectedValue = _Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("WH_TAB_SAGYO_SIJI_STATUS").ToString

            End If
        End If

        'エラーはストレージ形式になるかも(件数が膨大ならいちいち止めるのはよくない？)
        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm)
        Else
            MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})
        End If

    End Sub

#End Region

    'Add Start 2019/10/09 要望管理007373
    ''' <summary>
    ''' 出荷止め処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub StopAlloc(ByVal frm As LMB020F)

        'スプレッドの値をデータセットに反映
        Me.SetInkaSData(frm, Me._Ds)

        '引当済でない入荷(中)
        Dim whereInkaNoM As String = ""
        For Each row As DataRow In _Ds.Tables(LMB020C.TABLE_NM_INKA_M).Select("HIKIATE <> '" & LMB020C.HIKIATE_ARI & "' AND SYS_DEL_FLG = '0'")
            whereInkaNoM &= ",'" & row.Item("INKA_NO_M").ToString & "'"
        Next

        If whereInkaNoM = "" Then
            ShowMessage(frm, "E320", {"すべて引当済", "保留品区分を変更"})
            '出荷止のチェック状態を戻す
            frm.chkStopAlloc.Checked = Not frm.chkStopAlloc.Checked
            Return
        End If

        whereInkaNoM = whereInkaNoM.Substring(1)

        '現在の保留品区分の確認
        Dim checkValue As String
        Dim setValue As String
        If frm.chkStopAlloc.Checked Then
            checkValue = LMB020C.SPD_KB.ShipOK
            setValue = LMB020C.SPD_KB.Unpermitted
        Else
            checkValue = LMB020C.SPD_KB.Unpermitted
            setValue = LMB020C.SPD_KB.ShipOK
        End If

        If _Ds.Tables(LMB020C.TABLE_NM_INKA_S).Select("INKA_NO_M IN(" & whereInkaNoM & ") AND SPD_KB <> '" & checkValue & "' AND SYS_DEL_FLG = '0'").Length > 0 Then
            '保留品区分の名称取得
            Dim cachedZKbn As DataTable = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN)
            Dim checkValueName As String = cachedZKbn.Select("KBN_GROUP_CD = 'H003' AND KBN_CD = '" & checkValue & "'")(0).Item("KBN_NM1").ToString
            Dim setValueName As String = cachedZKbn.Select("KBN_GROUP_CD = 'H003' AND KBN_CD = '" & setValue & "'")(0).Item("KBN_NM1").ToString

            '確認メッセージ
            If MyBase.ShowMessage(frm, "C001", {"保留品区分が「" & checkValueName & "」でないものがあります。すべて「" & setValueName & "」に変更"}) <> MsgBoxResult.Ok Then
                '出荷止のチェック状態を戻す
                frm.chkStopAlloc.Checked = Not frm.chkStopAlloc.Checked
                Return
            End If
        End If

        '保留品区分の設定
        For Each row As DataRow In _Ds.Tables(LMB020C.TABLE_NM_INKA_S).Select("INKA_NO_M IN(" & whereInkaNoM & ") AND SYS_DEL_FLG = '0'")
            row.Item("SPD_KB") = setValue
        Next

        '画面表示
        Dim inkaNoM As String = frm.lblKanriNoM.TextValue
        If String.IsNullOrEmpty(inkaNoM) = False Then
            '入荷(小)情報表示
            Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.DOUBLECLICK, inkaNoM)
        End If

    End Sub
    'Add End   2019/10/09 要望管理007373

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

    Private Function AllFKeyOffChk(ByVal frm As LMB020F) As Boolean

        Dim result As Boolean = True

        With frm.FunctionKey
            result = result AndAlso .F1ButtonEnabled.Equals(False)
            result = result AndAlso .F2ButtonEnabled.Equals(False)
            result = result AndAlso .F3ButtonEnabled.Equals(False)
            result = result AndAlso .F4ButtonEnabled.Equals(False)
            result = result AndAlso .F5ButtonEnabled.Equals(False)
            result = result AndAlso .F6ButtonEnabled.Equals(False)
            result = result AndAlso .F7ButtonEnabled.Equals(False)
            result = result AndAlso .F8ButtonEnabled.Equals(False)
            result = result AndAlso .F9ButtonEnabled.Equals(False)
            result = result AndAlso .F10ButtonEnabled.Equals(False)
            result = result AndAlso .F11ButtonEnabled.Equals(False)
            result = result AndAlso .F12ButtonEnabled.Equals(False)
        End With

        Return result
    End Function

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMB020F, ByVal objNm As String, ByVal actionType As LMB020C.ActionType) As Boolean

        With frm

            'スプレッドの場合、後でロック
            Dim sprNm As String = .sprDetail.Name
            If sprNm.Equals(objNm) = False Then

                '処理開始アクション
                Call Me.StartAction(frm)

            End If

            Select Case objNm

                Case sprNm

                    Return Me.ShowPopupSpread(frm, objNm, actionType)

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    'START YANAI 要望番号481
                    'Call Me.SetReturnCustPop(frm)
                    Call Me.SetReturnCustPop(frm, actionType)
                    'END YANAI 要望番号481

                Case .txtUnsoCd.Name, .txtTrnBrCD.Name

                    'START YANAI 要望番号481
                    'Call Me.SetReturnUnsocoPop(frm)
                    Call Me.SetReturnUnsocoPop(frm, actionType)
                    'END YANAI 要望番号481

                Case .txtShukkaMotoCD.Name

                    'START YANAI 要望番号481
                    'Call Me.SetReturnShukkamotoPop(frm, objNm)
                    Call Me.SetReturnShukkamotoPop(frm, objNm, actionType)
                    'END YANAI 要望番号481

                Case .txtUnsoTariffCD.Name

                    'START YANAI 要望番号481
                    'Call Me.SetReturnTariffPop(frm)
                    Call Me.SetReturnTariffPop(frm, actionType)
                    'END YANAI 要望番号481

                    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
                Case .txtShiharaiTariffCD.Name

                    Call Me.SetReturnShiharaiTariffPop(frm, actionType)
                    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End


                Case .txtSerchGoodsCd.Name, .txtSerchGoodsNm.Name

                    'Return Me.AddInkaMDataAction(frm)
                    'START YANAI 要望番号481
                    'Call Me.SetReturnGoodsPop(frm)
                    Call Me.SetReturnGoodsPop(frm, actionType)
                    'END YANAI 要望番号481

                Case .txtNyukaComment.Name, .txtGoodsComment.Name

                    'START YANAI 要望番号481
                    'Call Me.SetReturnRemarkPop(frm, objNm)
                    Call Me.SetReturnRemarkPop(frm, objNm, actionType)
                    'END YANAI 要望番号481

                Case .txtTouNo.Name, .txtSituNo.Name, .txtZoneCd.Name
                    'ヘッダの棟室マスタ参照時
                    '棟室マスタ照会画面をPOP呼出&戻り値設定
                    Call Me.SetTouSituPop(frm, actionType)

                Case Else

                    '作業コードの場合
                    Select Case objNm.Substring(0, objNm.Length - 2)

                        Case LMB020C.SAGYO_CD

                            Call Me.SetReturnSagyoPop(frm, objNm, objNm.Substring(objNm.Length - 2, 2), actionType)

                    End Select

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupSpread(ByVal frm As LMB020F, ByVal objNm As String, ByVal actionType As LMB020C.ActionType) As Boolean

        Dim spr As Win.Spread.LMSpread = frm.sprDetail

        With spr.ActiveSheet

            If 0 < .Rows.Count Then

                Dim cell As FarPoint.Win.Spread.Cell = .ActiveCell
                Dim colNo As Integer = cell.Column.Index
                Dim rowNo As Integer = cell.Row.Index

                Select Case colNo

                    Case LMB020G.sprDetailDef.TOU_NO.ColNo _
                       , LMB020G.sprDetailDef.SHITSU_NO.ColNo _
                       , LMB020G.sprDetailDef.ZONE_CD.ColNo

                        'ロック項目はスルー
                        If Me._V.IsFocusSprChk(spr, cell) = False Then
                            Return False
                        End If

                        '処理開始アクション
                        Call Me.StartAction(frm)

                        '禁止文字チェック
                        If Me._V.IsPopupInputCheck(spr, rowNo, LMB020G.sprDetailDef.TOU_NO.ColNo, LMB020G.sprDetailDef.TOU_NO.ColName) = False Then
                            Return False
                        End If

                        '禁止文字チェック
                        If Me._V.IsPopupInputCheck(spr, rowNo, LMB020G.sprDetailDef.SHITSU_NO.ColNo, LMB020G.sprDetailDef.TOU_NO.ColName) = False Then
                            Return False
                        End If

                        '禁止文字チェック
                        If Me._V.IsPopupInputCheck(spr, rowNo, LMB020G.sprDetailDef.ZONE_CD.ColNo, LMB020G.sprDetailDef.ZONE_CD.ColName) = False Then
                            Return False
                        End If

                        'Enter処理は値がない場合、スルー
                        Select Case actionType

                            Case LMB020C.ActionType.ENTER

                                If String.IsNullOrEmpty(Me._LMBconV.GetCellValue(spr.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.TOU_NO.ColNo))) = True _
                                    AndAlso String.IsNullOrEmpty(Me._LMBconV.GetCellValue(spr.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.SHITSU_NO.ColNo))) = True _
                                    AndAlso String.IsNullOrEmpty(Me._LMBconV.GetCellValue(spr.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.ZONE_CD.ColNo))) = True _
                                    Then
                                    Return False
                                End If

                        End Select

                        'START YANAI 要望番号481
                        'Dim toShitsuPop As LMFormData = Me.ShowToshitsuZonePopup(frm, rowNo)
                        Dim toShitsuPop As LMFormData = Me.ShowToshitsuZonePopup(frm, rowNo, actionType)
                        'END YANAI 要望番号481
                        If toShitsuPop.ReturnFlg = True Then
                            Dim toShitsuDr As DataRow = toShitsuPop.ParamDataSet.Tables(LMZ120C.TABLE_NM_OUT).Rows(0)
                            spr.SetCellValue(rowNo, LMB020G.sprDetailDef.TOU_NO.ColNo, toShitsuDr.Item("TOU_NO").ToString())
                            spr.SetCellValue(rowNo, LMB020G.sprDetailDef.SHITSU_NO.ColNo, toShitsuDr.Item("SITU_NO").ToString())
                            spr.SetCellValue(rowNo, LMB020G.sprDetailDef.ZONE_CD.ColNo, toShitsuDr.Item("ZONE_CD").ToString())
                        End If

                    Case LMB020G.sprDetailDef.DEST_CD.ColNo

                        'ロック項目はスルー
                        If Me._V.IsFocusSprChk(spr, cell) = False Then
                            Return False
                        End If

                        '処理開始アクション
                        Call Me.StartAction(frm)

                        '禁止文字チェック
                        If Me._V.IsPopupInputCheck(spr, rowNo, colNo, LMB020G.sprDetailDef.DEST_CD.ColName) = False Then
                            Return False
                        End If

                        'Enter処理は値がない場合、スルー
                        Select Case actionType

                            Case LMB020C.ActionType.ENTER

                                If String.IsNullOrEmpty(Me._LMBconV.GetCellValue(spr.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.DEST_CD.ColNo))) = True Then
                                    Return False
                                End If

                        End Select

                        'START YANAI 要望番号481
                        'Dim destPop As LMFormData = Me.ShowDestPopup(frm, objNm, rowNo)
                        Dim destPop As LMFormData = Me.ShowDestPopup(frm, objNm, rowNo, actionType)
                        'END YANAI 要望番号481
                        If destPop.ReturnFlg = True Then
                            Dim destDr As DataRow = destPop.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
                            spr.SetCellValue(rowNo, colNo, destDr.Item("DEST_CD").ToString())
                            spr.SetCellValue(rowNo, LMB020G.sprDetailDef.DEST_NM.ColNo, destDr.Item("DEST_NM").ToString())
                        End If

                    Case Else

                        Select Case actionType
                            Case LMB020C.ActionType.ENTER
                                'フォーカス移動処理
                                Call Me.NextFocusedControl(frm, True)
                                Return False
                        End Select

                        Return Me._LMBconV.SetFocusErrMessage()

                End Select

            End If

        End With

        Return True

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 荷主コードの初期値設定
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <returns>True:選択有 False:選択無</returns>
    '''' <remarks></remarks>
    'Private Function NewModeShowCustPop(ByVal frm As LMB020F) As Boolean
    ''' <summary>
    ''' 荷主コードの初期値設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function NewModeShowCustPop(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As Boolean
        'END YANAI 要望番号481

        '新規の場合
        Me._PopupSkipFlg = False
        'START YANAI 要望番号481
        'Dim prm As LMFormData = Me.ShowCustPopup(frm)
        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)
        'END YANAI 要望番号481
        If prm.ReturnFlg = False Then

            Me._LMBconV.SetErrMessage("E193")
            Return False

        End If

        '保持している情報をクリア
        Me._Ds.Clear()

        '保管・荷役料最終計算日 検索結果 初期化
        Me._DtHokanNiyakuCalculation = Nothing

        '初期値設定
        Return Me.SetForm(frm, prm.ParamDataSet, RecordStatus.NEW_REC, LMZ260C.TABLE_NM_OUT)

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 商品マスタ戻り値設定
    '''' </summary>
    '''' <param name="frm"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function SetReturnGoodsPop(ByVal frm As LMB020F) As Boolean
    ''' <summary>
    ''' 商品マスタ戻り値設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetReturnGoodsPop(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As Boolean
        'END YANAI 要望番号481

        'START YANAI 要望番号481
        'Dim prm As LMFormData = Me.ShowGoodsPopup(frm)

        Dim prm As LMFormData = Me.ShowGoodsPopup(frm, actionType)
        'END YANAI 要望番号481

        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)

            With frm
                .txtSerchGoodsCd.TextValue = dr.Item("GOODS_CD_CUST").ToString()
                .txtSerchGoodsNm.TextValue = dr.Item("GOODS_NM_1").ToString()
            End With

        End If

        Return True

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 商品マスタ参照POP起動
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <remarks></remarks>
    'Private Function ShowGoodsPopup(ByVal frm As LMB020F) As LMFormData
    ''' <summary>
    ''' 商品マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowGoodsPopup(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As LMFormData
        'END YANAI 要望番号481

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ020DS()
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            'START YANAI 要望番号481
            '.Item("GOODS_CD_CUST") = frm.txtSerchGoodsCd.TextValue
            '.Item("GOODS_NM_1") = frm.txtSerchGoodsNm.TextValue
            If (LMB020C.ActionType.ENTER).Equals(actionType) = True OrElse _
                (LMB020C.ActionType.INIT_M).Equals(actionType) = True Then
                .Item("GOODS_CD_CUST") = frm.txtSerchGoodsCd.TextValue
                .Item("GOODS_NM_1") = frm.txtSerchGoodsNm.TextValue
            End If
            'END YANAI 要望番号481
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            '.Item("HW_ICCHI_FLG") = "0"
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        '2011/08/25 岸 まとめ検証結果(画面共通)№3対応
        'Pop起動
        Return Me._LMBconH.FormShow(ds, "LMZ020", "", Me._PopupSkipFlg)
        'Return Me.PopFormShow(prm, "LMZ020")

    End Function

    '2013.07.16 追加START
    ''' <summary>
    ''' 入荷検品選択POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowGoodsKenpinPopup(ByVal frm As LMB020F) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMB040DS()
        Dim dt As DataTable = ds.Tables(LMB040C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        'Pop起動
        Return Me._LMBconH.FormShow(ds, "LMB040", "", Me._PopupSkipFlg)

    End Function
    '2013.07.16 追加END

    '追加開始 2015.01.13 韓国CALT対応
    ''' <summary>
    ''' 入荷検品選択POP起動2
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowKenpinGoodsKenpinPopup(ByVal frm As LMB020F) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMB050DS()
        Dim dt As DataTable = ds.Tables(LMB050C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            .Item("WH_CD") = frm.cmbSoko.SelectedValue
            .Item("INKA_DATE") = frm.imdNyukaDate.TextValue
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        'Pop起動
        Return Me._LMBconH.FormShow(ds, "LMB050", "", Me._PopupSkipFlg)

    End Function
    '追加開始 2015.01.13 韓国CALT対応

    'START YANAI 要望番号481
    '''' <summary>
    '''' 商品マスタ参照POP起動(CSV取り込用)
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <remarks></remarks>
    'Private Function ShowGoodsPopup_CSV(ByVal frm As LMB020F) As LMFormData
    ''' <summary>
    ''' 商品マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowGoodsPopup_CSV(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As LMFormData
        'END YANAI 要望番号481

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ020DS()
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            'START YANAI 要望番号481
            '.Item("GOODS_CD_CUST") = frm.txtSerchGoodsCd.TextValue
            '.Item("GOODS_NM_1") = frm.txtSerchGoodsNm.TextValue
            'If (LMB020C.ActionType.ENTER).Equals(actionType) = True OrElse _
            '   (LMB020C.ActionType.INIT_M).Equals(actionType) = True Then
            .Item("GOODS_CD_CUST") = frm.txtSerchGoodsCd.TextValue
            .Item("IRIME") = frm.txtIrime.TextValue

            '.Item("GOODS_NM_1") = frm.txtSerchGoodsNm.TextValue
            'End If
            'END YANAI 要望番号481
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            '.Item("HW_ICCHI_FLG") = "1"
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        '2011/08/25 岸 まとめ検証結果(画面共通)№3対応
        'Pop起動
        Return Me._LMBconH.FormShow(ds, "LMZ020", "", Me._PopupSkipFlg) '回転前なのでコメントアウト
        'Return Me.PopFormShow(prm, "LMZ020")

        ' ''ここから回転

        ''Dim dtOut As DataTable = ds.Tables(LMZ020C.TABLE_NM_OUT)
        ''Dim max As Integer = dt.Rows.Count - 1
        ''Dim outCount As Integer = 0

        ''For i As Integer = 0 To max

        ''    '完全一致を想定して検索へ
        ''    Me._LMBconH.FormShow(ds, "LMZ020", "", Me._PopupSkipFlg)

        ''    'OUT件数確認
        ''    'ZERO件の場合

        ''    outCount = dtOut.Rows.Count

        ''    Select Case outCount
        ''        Case 0
        ''            dr.Item("HW_ICCHI_FLG") = "0"

        ''            '部分一致を想定して検索へ
        ''            Me._LMBconH.FormShow(ds, "LMZ020", "", Me._PopupSkipFlg)

        ''            '結果一件以上の場合
        ''            If dtOut.Rows.Count > 1 Then

        ''                'EXCEL起動 
        ''                MyBase.MessageStoreDownload(True)
        ''                MyBase.ShowMessage(frm, "E033")
        ''                'Return False
        ''            End If
        ''        Case 1
        ''        Case Is > 1
        ''            'EXCEL起動 
        ''            MyBase.MessageStoreDownload(True)
        ''            MyBase.ShowMessage(frm, "E033")
        ''            'Return False

        ''    End Select
        ''Next
    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' タリフPopの戻り値を設定
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <returns>True:選択有 False:選択無</returns>
    '''' <remarks></remarks>
    'Private Function SetReturnTariffPop(ByVal frm As LMB020F) As Boolean
    ''' <summary>
    ''' タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnTariffPop(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As Boolean
        'END YANAI 要望番号481

        Dim prm As LMFormData = Nothing
        Dim tblNm As String = String.Empty
        Dim code As String = String.Empty
        Dim remark As String = String.Empty

        With frm

            If LMB020C.TARIFF_YOKO.Equals(.cmbUnchinKbn.SelectedValue.ToString()) = True Then

                '横持ちタリフPop
                'START YANAI 要望番号481
                'prm = Me.ShowYokoTariffPopup(frm)
                prm = Me.ShowYokoTariffPopup(frm, actionType)
                'END YANAI 要望番号481
                tblNm = LMZ100C.TABLE_NM_OUT
                code = "YOKO_TARIFF_CD"
                remark = "YOKO_REM"

            Else

                '運賃タリフPop
                'START YANAI 要望番号481
                'prm = Me.ShowUnchinTariffPopup(frm)
                prm = Me.ShowUnchinTariffPopup(frm, actionType)
                'END YANAI 要望番号481
                tblNm = LMZ230C.TABLE_NM_OUT
                code = "UNCHIN_TARIFF_CD"
                remark = "UNCHIN_TARIFF_REM"

            End If

            If prm.ReturnFlg = True Then

                Dim dr As DataRow = prm.ParamDataSet.Tables(tblNm).Rows(0)

                .txtUnsoTariffCD.TextValue = dr.Item(code).ToString()
                .lblUnsoTariffNM.TextValue = dr.Item(remark).ToString()

                Return True

            End If

        End With

        Return False

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 運賃タリフマスタ参照POP起動
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <remarks></remarks>
    'Private Function ShowUnchinTariffPopup(ByVal frm As LMB020F) As LMFormData
    ''' <summary>
    ''' 運賃タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowUnchinTariffPopup(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As LMFormData
        'END YANAI 要望番号481

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ230DS()
        Dim dt As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            '.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            '.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            .Item("STR_DATE") = frm.imdNyukaDate.TextValue
            'START YANAI 要望番号481
            '.Item("UNCHIN_TARIFF_CD") = frm.txtUnsoTariffCD.TextValue
            If (LMB020C.ActionType.ENTER).Equals(actionType) = True Then
                .Item("UNCHIN_TARIFF_CD") = frm.txtUnsoTariffCD.TextValue
            End If
            'END YANAI 要望番号481
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        If String.IsNullOrEmpty(frm.txtUnsoTariffCD.TextValue) = True Then
            frm.lblUnsoTariffNM.TextValue = String.Empty
        End If

        '2011/08/25 岸 まとめ検証結果(画面共通)№3対応
        'Pop起動
        Return Me._LMBconH.FormShow(ds, "LMZ230", "", Me._PopupSkipFlg)
        'Return Me.PopFormShow(prm, "LMZ230")

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 横持ちタリフマスタ参照POP起動
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <remarks></remarks>
    'Private Function ShowYokoTariffPopup(ByVal frm As LMB020F) As LMFormData
    ''' <summary>
    ''' 横持ちタリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowYokoTariffPopup(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As LMFormData
        'END YANAI 要望番号481

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ100DS()
        Dim dt As DataTable = ds.Tables(LMZ100C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START YANAI 要望番号481
            '.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            '.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            '.Item("YOKO_TARIFF_CD") = frm.txtUnsoTariffCD.TextValue
            If (LMB020C.ActionType.ENTER).Equals(actionType) = True Then
                .Item("YOKO_TARIFF_CD") = frm.txtUnsoTariffCD.TextValue
            End If
            'END YANAI 要望番号481
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        If String.IsNullOrEmpty(frm.txtUnsoTariffCD.TextValue) = True Then
            frm.lblUnsoTariffNM.TextValue = String.Empty
        End If

        '2011/08/25 岸 まとめ検証結果(画面共通)№3対応
        'Pop起動
        Return Me._LMBconH.FormShow(ds, "LMZ100", "", Me._PopupSkipFlg)
        'Return Me.PopFormShow(prm, "LMZ100")

    End Function

    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
    ''' <summary>
    ''' タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnShiharaiTariffPop(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As Boolean

        Dim prm As LMFormData = Nothing
        Dim tblNm As String = String.Empty
        Dim code As String = String.Empty
        Dim remark As String = String.Empty

        With frm

            If LMB020C.TARIFF_YOKO.Equals(.cmbUnchinKbn.SelectedValue.ToString()) = True Then

                '横持ちタリフPop
                prm = Me.ShowShiharaiYokoTariffPopup(frm, actionType)
                tblNm = LMZ320C.TABLE_NM_OUT
                code = "YOKO_TARIFF_CD"
                remark = "YOKO_REM"

            Else

                '支払タリフPop
                prm = Me.ShowShiharaiTariffPopup(frm, actionType)
                tblNm = LMZ290C.TABLE_NM_OUT
                code = "SHIHARAI_TARIFF_CD"
                remark = "SHIHARAI_TARIFF_REM"

            End If

            If prm.ReturnFlg = True Then

                Dim dr As DataRow = prm.ParamDataSet.Tables(tblNm).Rows(0)

                .txtShiharaiTariffCD.TextValue = dr.Item(code).ToString()
                .lblShiharaiTariffNM.TextValue = dr.Item(remark).ToString()

                Return True

            End If

        End With

        Return False

    End Function

    ''' <summary>
    ''' 支払タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowShiharaiTariffPopup(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ290DS()
        Dim dt As DataTable = ds.Tables(LMZ290C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("STR_DATE") = frm.imdNyukaDate.TextValue
            If (LMB020C.ActionType.ENTER).Equals(actionType) = True Then
                .Item("SHIHARAI_TARIFF_CD") = frm.txtShiharaiTariffCD.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        If String.IsNullOrEmpty(frm.txtShiharaiTariffCD.TextValue) = True Then
            frm.lblShiharaiTariffNM.TextValue = String.Empty
        End If

        Return Me._LMBconH.FormShow(ds, "LMZ290", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowShiharaiYokoTariffPopup(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ320DS()
        Dim dt As DataTable = ds.Tables(LMZ320C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            If (LMB020C.ActionType.ENTER).Equals(actionType) = True Then
                .Item("YOKO_TARIFF_CD") = frm.txtShiharaiTariffCD.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        If String.IsNullOrEmpty(frm.txtShiharaiTariffCD.TextValue) = True Then
            frm.lblShiharaiTariffNM.TextValue = String.Empty
        End If

        'Pop起動
        Return Me._LMBconH.FormShow(ds, "LMZ320", "", Me._PopupSkipFlg)

    End Function
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End



    'START YANAI 要望番号481
    '''' <summary>
    '''' 棟・室・ゾーンマスタ参照POP起動
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <remarks></remarks>
    'Private Function ShowToshitsuZonePopup(ByVal frm As LMB020F, ByVal rowNo As Integer) As LMFormData
    ''' <summary>
    ''' 棟・室・ゾーンマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowToshitsuZonePopup(ByVal frm As LMB020F, ByVal rowNo As Integer, ByVal actionType As LMB020C.ActionType) As LMFormData
        'END YANAI 要望番号481

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ120DS()
        Dim dt As DataTable = ds.Tables(LMZ120C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim spr As SheetView = frm.sprDetail.ActiveSheet
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("WH_CD") = frm.cmbSoko.SelectedValue.ToString()
            'START YANAI 要望番号481
            '.Item("TOU_NO") = Me._LMBconV.GetCellValue(spr.Cells(rowNo, LMB020G.sprDetailDef.TOU_NO.ColNo))
            '.Item("SITU_NO") = Me._LMBconV.GetCellValue(spr.Cells(rowNo, LMB020G.sprDetailDef.SHITSU_NO.ColNo))
            '.Item("ZONE_CD") = Me._LMBconV.GetCellValue(spr.Cells(rowNo, LMB020G.sprDetailDef.ZONE_CD.ColNo))
            If (LMB020C.ActionType.ENTER).Equals(actionType) = True Then
                .Item("TOU_NO") = Me._LMBconV.GetCellValue(spr.Cells(rowNo, LMB020G.sprDetailDef.TOU_NO.ColNo))
                .Item("SITU_NO") = Me._LMBconV.GetCellValue(spr.Cells(rowNo, LMB020G.sprDetailDef.SHITSU_NO.ColNo))
                .Item("ZONE_CD") = Me._LMBconV.GetCellValue(spr.Cells(rowNo, LMB020G.sprDetailDef.ZONE_CD.ColNo))
            End If
            'END YANAI 要望番号481
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        '2011/08/25 岸 まとめ検証結果(画面共通)№3対応
        'Pop起動
        Return Me._LMBconH.FormShow(ds, "LMZ120", "", Me._PopupSkipFlg)
        'Return Me.PopFormShow(prm, "LMZ120")

    End Function

    ''' <summary>
    ''' 作業Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="sagyoStr">後ろ2桁</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSagyoPop(ByVal frm As LMB020F, ByVal objNm As String, ByVal sagyoStr As String, ByVal actionType As LMB020C.ActionType) As Boolean

        Select Case actionType

            Case LMB020C.ActionType.ENTER

                'START YANAI 要望番号481
                'Return Me.SetReturnSagyoPopEnter(frm, objNm)
                Return Me.SetReturnSagyoPopEnter(frm, objNm, actionType)
                'END YANAI 要望番号481

            Case LMB020C.ActionType.MASTEROPEN

                'START YANAI 要望番号481
                'Return Me.SetReturnSagyoPopOpenMaster(frm, objNm, sagyoStr)
                Return Me.SetReturnSagyoPopOpenMaster(frm, objNm, sagyoStr, actionType)
                'END YANAI 要望番号481

        End Select

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 作業マスタ参照POP起動(F10)
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <param name="objNm">フォーカス位置コントロール名</param>
    '''' <param name="sagyoStr">後ろ2桁</param>
    '''' <returns>True:選択有 False:選択無</returns>
    '''' <remarks></remarks>
    'Private Function SetReturnSagyoPopOpenMaster(ByVal frm As LMB020F, ByVal objNm As String, ByVal sagyoStr As String) As Boolean
    ''' <summary>
    ''' 作業マスタ参照POP起動(F10)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="sagyoStr">後ろ2桁</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSagyoPopOpenMaster(ByVal frm As LMB020F, ByVal objNm As String, ByVal sagyoStr As String, ByVal actionType As LMB020C.ActionType) As Boolean
        'END YANAI 要望番号481

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim arr As ArrayList = New ArrayList()
        Dim max As Integer = LMB020C.SAGYO_MAX_REC
        Dim type As String = sagyoStr.Substring(0, 1)
        Dim txtNm As String = String.Concat(LMB020C.SAGYO_CD, type)
        Dim ctl As Win.InputMan.LMImTextBox() = New Win.InputMan.LMImTextBox() {}
        Dim chkCtl As Win.InputMan.LMImTextBox = Nothing
        Dim cnt As Integer = 0
        Dim chkNmCtl As Win.InputMan.LMImTextBox = Nothing
        Dim chkFlgCtl As Win.InputMan.LMImTextBox = Nothing
        Dim chkRmkCtl As Win.InputMan.LMImTextBox = Nothing

        For i As Integer = 1 To max

            '作業コードコントロールの取得
            chkCtl = DirectCast(frm.Controls.Find(String.Concat(txtNm, i.ToString()), True)(0), Win.InputMan.LMImTextBox)
            chkNmCtl = DirectCast(frm.Controls.Find(String.Concat(LMB020C.SAGYO_NM, txtNm.Substring(txtNm.Length - 1, 1), i.ToString()), True)(0), Win.InputMan.LMImTextBox)
            chkFlgCtl = DirectCast(frm.Controls.Find(String.Concat(LMB020C.SAGYO_FL, txtNm.Substring(txtNm.Length - 1, 1), i.ToString()), True)(0), Win.InputMan.LMImTextBox)
            chkRmkCtl = DirectCast(frm.Controls.Find(String.Concat(LMB020C.SAGYO_RMK_SIJI, txtNm.Substring(txtNm.Length - 1, 1), i.ToString()), True)(0), Win.InputMan.LMImTextBox)

            '値が入っていないものを判定
            If String.IsNullOrEmpty(chkCtl.TextValue) = True Then

                '現在のカウントを設定
                cnt = ctl.Length

                '領域の確保
                ReDim Preserve ctl(cnt)

                'コントロール配列に設定
                ctl(cnt) = chkCtl

                chkNmCtl.TextValue = String.Empty
                chkFlgCtl.TextValue = String.Empty
                chkRmkCtl.TextValue = String.Empty

            End If

        Next

        '処理結果のカウント
        cnt = ctl.Length
        Dim msg As String = String.Empty
        Select Case type

            Case LMB020C.SagyoData.L.ToString()

                '2017/09/25 修正 李↓
                msg = lgm.Selector({"作業(大)", "Work(L)", "작업(大)", "中国語"})
                '2017/09/25 修正 李↑

            Case LMB020C.SagyoData.M.ToString()

                '2017/09/25 修正 李↓
                msg = lgm.Selector({"作業(中)", "Work(M)", "작업(中)", "中国語"})
                '2017/09/25 修正 李↑

        End Select

        '設定カウントチェック
        If Me._V.IsSagyoPopupChk(cnt, msg) = False Then
            Return False
        End If

        'ナビゲート処理
        'START YANAI 要望番号481
        'Dim prm As LMFormData = Me.ShowSagyoPopup(frm, String.Empty, cnt)
        Dim prm As LMFormData = Me.ShowSagyoPopup(frm, String.Empty, cnt, actionType)
        'END YANAI 要望番号481

        '戻り値がある場合、設定
        If prm.ReturnFlg = True Then

            Dim dt As DataTable = prm.ParamDataSet.Tables(LMZ200C.TABLE_NM_OUT)
            Dim rowMax As Integer = dt.Rows.Count - 1

            '戻り値の行数分設定
            For i As Integer = 0 To rowMax
                ctl(i).TextValue = dt.Rows(i).Item("SAGYO_CD").ToString()
                Me._G.GetTextControl(Me.GetSagyoCtlNm(ctl(i).Name)(1).ToString()).TextValue = dt.Rows(i).Item("SAGYO_RYAK").ToString()
                Me._G.GetTextControl(Me.GetSagyoCtlNm(ctl(i).Name)(4).ToString()).TextValue = dt.Rows(i).Item("WH_SAGYO_REMARK").ToString()
            Next

            Return True

        End If

        Return False

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 作業マスタ参照POP起動(Enter)
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <param name="objNm">フォーカス位置コントロール名</param>
    '''' <returns>True:選択有 False:選択無</returns>
    '''' <remarks></remarks>
    'Private Function SetReturnSagyoPopEnter(ByVal frm As LMB020F, ByVal objNm As String) As Boolean
    ''' <summary>
    ''' 作業マスタ参照POP起動(Enter)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSagyoPopEnter(ByVal frm As LMB020F, ByVal objNm As String, ByVal actionType As LMB020C.ActionType) As Boolean
        'END YANAI 要望番号481

        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(objNm)
        'START YANAI 要望番号481
        'Dim prm As LMFormData = Me.ShowSagyoPopup(frm, txtCtl.TextValue, 1)
        Dim prm As LMFormData = Me.ShowSagyoPopup(frm, txtCtl.TextValue, 1, actionType)
        'END YANAI 要望番号481

        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ200C.TABLE_NM_OUT).Rows(0)
            Dim arrCtlNm As ArrayList = Me.GetSagyoCtlNm(objNm)

            txtCtl.TextValue = dr.Item("SAGYO_CD").ToString()
            Me._G.GetTextControl(arrCtlNm(1).ToString()).TextValue = dr.Item("SAGYO_RYAK").ToString()
            Me._G.GetTextControl(arrCtlNm(4).ToString()).TextValue = dr.Item("WH_SAGYO_REMARK").ToString()
            Return True

        End If

        Return False

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 作業項目マスタ参照POP起動
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <param name="value">コードのテキスト</param>
    '''' <param name="sagyoCnt">選択可能数</param>
    '''' <remarks></remarks>
    'Private Function ShowSagyoPopup(ByVal frm As LMB020F, ByVal value As String, ByVal sagyoCnt As Integer) As LMFormData
    ''' <summary>
    ''' 作業項目マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="value">コードのテキスト</param>
    ''' <param name="sagyoCnt">選択可能数</param>
    ''' <remarks></remarks>
    Private Function ShowSagyoPopup(ByVal frm As LMB020F, ByVal value As String, ByVal sagyoCnt As Integer, ByVal actionType As LMB020C.ActionType) As LMFormData
        'END YANAI 要望番号481

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ200DS()
        Dim dt As DataTable = ds.Tables(LMZ200C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            'START YANAI 要望番号481
            '.Item("SAGYO_CD") = value
            If (LMB020C.ActionType.ENTER).Equals(actionType) = True Then
                .Item("SAGYO_CD") = value
            End If
            'END YANAI 要望番号481
            .Item("SAGYO_CNT") = sagyoCnt
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        '2011/08/25 岸 まとめ検証結果(画面共通)№3対応
        'Pop起動
        Return Me._LMBconH.FormShow(ds, "LMZ200", "", Me._PopupSkipFlg)
        'Return Me.PopFormShow(prm, "LMZ200")

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 出荷元Popの戻り値を設定
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <param name="objNm">フォーカス位置コントロール名</param>
    '''' <returns>True:選択有 False:選択無</returns>
    '''' <remarks></remarks>
    'Private Function SetReturnShukkamotoPop(ByVal frm As LMB020F, ByVal objNm As String) As Boolean
    ''' <summary>
    ''' 出荷元Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnShukkamotoPop(ByVal frm As LMB020F, ByVal objNm As String, ByVal actionType As LMB020C.ActionType) As Boolean
        'END YANAI 要望番号481

        'START YANAI 要望番号481
        'Dim prm As LMFormData = Me.ShowDestPopup(frm, objNm)
        Dim prm As LMFormData = Me.ShowDestPopup(frm, objNm, -1, actionType)
        'END YANAI 要望番号481
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtShukkaMotoCD.TextValue = dr.Item("DEST_CD").ToString()
                .lblShukkaMotoNM.TextValue = dr.Item("DEST_NM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 届け先マスタ参照POP起動
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <param name="objNm">フォーカス位置コントロール名</param>
    '''' <param name="rowNo">行番号　初期値 = -1</param>
    '''' <remarks></remarks>
    'Private Function ShowDestPopup(ByVal frm As LMB020F, ByVal objNm As String, Optional ByVal rowNo As Integer = -1) As LMFormData
    ''' <summary>
    ''' 届け先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="rowNo">行番号　初期値 = -1</param>
    ''' <remarks></remarks>
    Private Function ShowDestPopup(ByVal frm As LMB020F, ByVal objNm As String, Optional ByVal rowNo As Integer = -1, Optional ByVal actionType As LMB020C.ActionType = LMB020C.ActionType.MASTEROPEN) As LMFormData
        'END YANAI 要望番号481

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            'START YANAI 要望番号481
            'If rowNo = -1 Then
            '    .Item("DEST_CD") = Me._G.GetTextControl(objNm).TextValue
            'Else
            '    .Item("DEST_CD") = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.DEST_CD.ColNo))
            'End If
            If (LMB020C.ActionType.ENTER).Equals(actionType) = True Then
                If rowNo = -1 Then
                    .Item("DEST_CD") = Me._G.GetTextControl(objNm).TextValue
                Else
                    .Item("DEST_CD") = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.DEST_CD.ColNo))
                End If
            End If
            'END YANAI 要望番号481
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        If rowNo = -1 AndAlso String.IsNullOrEmpty(Me._G.GetTextControl(objNm).TextValue) = True Then
            frm.lblShukkaMotoNM.TextValue = String.Empty
        End If

        '2011/08/25 岸 まとめ検証結果(画面共通)№3対応
        'Pop起動
        Return Me._LMBconH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)
        'Return Me.PopFormShow(prm, "LMZ210")

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 運送会社Popの戻り値を設定
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <returns>True:選択有 False:選択無</returns>
    '''' <remarks></remarks>
    'Private Function SetReturnUnsocoPop(ByVal frm As LMB020F) As Boolean
    ''' <summary>
    ''' 運送会社Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnsocoPop(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As Boolean
        'END YANAI 要望番号481

        'START YANAI 要望番号481
        'Dim prm As LMFormData = Me.ShowUnsocoPopup(frm)
        Dim prm As LMFormData = Me.ShowUnsocoPopup(frm, actionType)
        'END YANAI 要望番号481
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtUnsoCd.TextValue = dr.Item("UNSOCO_CD").ToString()
                .txtTrnBrCD.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
                .lblTrnNM.TextValue = String.Concat(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString())

                '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
                .txtShiharaiTariffCD.TextValue = dr.Item("UNCHIN_TARIFF_CD").ToString
                .lblShiharaiTariffNM.TextValue = dr.Item("UNCHIN_TARIFF_REM").ToString
                '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

            End With

            'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
            '運送会社コードOLD設定
            Me._G.SetUnsoCdOld(frm)

            '運賃タリフセットからタリフコードを設定
            Call Me._G.GetUnchinTariffSet(frm, False)
            'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

            Return True

        End If

        Return False

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 運送会社マスタ参照POP起動
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <remarks></remarks>
    'Private Function ShowUnsocoPopup(ByVal frm As LMB020F) As LMFormData
    ''' <summary>
    ''' 運送会社マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowUnsocoPopup(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As LMFormData
        'END YANAI 要望番号481

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ250DS()
        Dim dt As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START YANAI 要望番号481
            '.Item("UNSOCO_CD") = frm.txtUnsoCd.TextValue
            '.Item("UNSOCO_BR_CD") = frm.txtTrnBrCD.TextValue
            If (LMB020C.ActionType.ENTER).Equals(actionType) = True Then
                .Item("UNSOCO_CD") = frm.txtUnsoCd.TextValue
                .Item("UNSOCO_BR_CD") = frm.txtTrnBrCD.TextValue
            End If
            'END YANAI 要望番号481
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        If String.IsNullOrEmpty(frm.txtUnsoCd.TextValue) = True AndAlso _
            String.IsNullOrEmpty(frm.txtTrnBrCD.TextValue) = True Then
            frm.lblTrnNM.TextValue = String.Empty
        End If

        '2011/08/25 岸 まとめ検証結果(画面共通)№3対応
        'Pop起動
        Return Me._LMBconH.FormShow(ds, "LMZ250", "", Me._PopupSkipFlg)
        'Return Me.PopFormShow(prm, "LMZ250")

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 荷主Popの戻り値を設定
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <returns>True:選択有 False:選択無</returns>
    '''' <remarks></remarks>
    'Private Function SetReturnCustPop(ByVal frm As LMB020F) As Boolean
    ''' <summary>
    ''' 荷主Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As Boolean
        'END YANAI 要望番号481

        'START YANAI 要望番号481
        'Dim prm As LMFormData = Me.ShowCustPopup(frm)
        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)
        'END YANAI 要望番号481
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                .lblCustNm.TextValue = String.Concat(dr.Item("CUST_NM_L").ToString(), dr.Item("CUST_NM_M").ToString())

            End With

            Return True

        End If

        Return False

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 荷主マスタ参照POP起動
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <remarks></remarks>
    'Private Function ShowCustPopup(ByVal frm As LMB020F) As LMFormData
    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As LMFormData
        'END YANAI 要望番号481

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START YANAI 要望番号481
            '.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            '.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            '.Item("CUST_CD_S") = "00"
            '.Item("CUST_CD_SS") = "00"
            If (LMB020C.ActionType.ENTER).Equals(actionType) = True OrElse _
                (LMB020C.ActionType.INIT).Equals(actionType) = True Then
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                .Item("CUST_CD_S") = "00"
                .Item("CUST_CD_SS") = "00"
            End If
            'END YANAI 要望番号481
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.OFF
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        '2011/08/25 岸 まとめ検証結果(画面共通)№3対応
        'Pop起動
        Return Me._LMBconH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 注意書Popの戻り値を設定
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <param name="objNm">フォーカス位置コントロール名</param>
    '''' <returns>True:選択有 False:選択無</returns>
    '''' <remarks></remarks>
    'Private Function SetReturnRemarkPop(ByVal frm As LMB020F, ByVal objNm As String) As Boolean
    ''' <summary>
    ''' 注意書Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnRemarkPop(ByVal frm As LMB020F, ByVal objNm As String, ByVal actionType As LMB020C.ActionType) As Boolean
        'END YANAI 要望番号481

        Dim ctl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(objNm)
        'START YANAI 要望番号481
        'Dim prm As LMFormData = Me.ShowRemarkPopup(frm, ctl.TextValue)
        Dim prm As LMFormData = Me.ShowRemarkPopup(frm, ctl.TextValue, actionType)
        'END YANAI 要望番号481
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ270C.TABLE_NM_OUT).Rows(0)
            'START YANAI 要望番号516
            'ctl.TextValue = dr.Item("REMARK").ToString()
            If String.IsNullOrEmpty(ctl.TextValue) = True Then
                '値が設定されていない場合は、戻り値をそのまま設定
                ctl.TextValue = dr.Item("REMARK").ToString()
            Else
                '値が設定されている場合は、元々の値 & 半角スペース & 戻り値を設定
                ctl.TextValue = String.Concat(ctl.TextValue, Space(1), dr.Item("REMARK").ToString())
            End If
            'END YANAI 要望番号516
            Return True
        End If

        Return False

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 注意書テーブル参照POP起動
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <param name="value">検索条件</param>
    '''' <remarks></remarks>
    'Private Function ShowRemarkPopup(ByVal frm As LMB020F, ByVal value As String) As LMFormData
    ''' <summary>
    ''' 注意書テーブル参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="value">検索条件</param>
    ''' <remarks></remarks>
    Private Function ShowRemarkPopup(ByVal frm As LMB020F, ByVal value As String, ByVal actionType As LMB020C.ActionType) As LMFormData
        'END YANAI 要望番号481

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ270DS()
        Dim dt As DataTable = ds.Tables(LMZ270C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("USER_CD") = LMUserInfoManager.GetUserID()
            .Item("SUB_KB") = LMB020C.YOTO_INKA
            'START YANAI 要望番号481
            '.Item("REMARK") = value
            If (LMB020C.ActionType.ENTER).Equals(actionType) = True Then
                .Item("REMARK") = value
            End If
            'END YANAI 要望番号481
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        '2011/08/25 岸 まとめ検証結果(画面共通)№3対応
        'Pop起動
        Return Me._LMBconH.FormShow(ds, "LMZ270", "", Me._PopupSkipFlg)
        'Return Me.PopFormShow(prm, "LMZ270")

    End Function

    ''' <summary>
    ''' 棟室マスタ照会画面Pop処理(置場)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetTouSituPop(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As Boolean

        '荷主マスタ参照POP起動
        Dim prm As LMFormData = Me.ShowTouSituPopup(frm, actionType)
        '戻り値の設定
        If prm.ReturnFlg = True Then
            'LMZ260Cデータセット取得
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ120C.TABLE_NM_OUT).Rows(0)
            '当画面項目へセット
            With frm
                .txtTouNo.TextValue = dr.Item("TOU_NO").ToString()
                .txtSituNo.TextValue = dr.Item("SITU_NO").ToString()
                .txtZoneCd.TextValue = dr.Item("ZONE_CD").ToString()
            End With
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' 棟室マスタ参照POP起動(置場)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowTouSituPopup(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ120DS()
        Dim dt As DataTable = ds.Tables(LMZ120C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        'Keyをデータセット
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("WH_CD") = frm.cmbSoko.SelectedValue
            If actionType = LMB020C.ActionType.ENTER Then
                .Item("TOU_NO") = frm.txtTouNo.TextValue
                .Item("SITU_NO") = frm.txtSituNo.TextValue
                .Item("ZONE_CD") = frm.txtZoneCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ120")

    End Function

    ''' <summary>
    ''' Pop起動処理
    ''' </summary>
    ''' <param name="prm">パラメータクラス</param>
    ''' <param name="id">画面ID</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Private Function PopFormShow(ByVal prm As LMFormData, ByVal id As String) As LMFormData

        LMFormNavigate.NextFormNavigate(Me, id, prm)

        Return prm

    End Function


#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMB020F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetGMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' ガイダンスメッセージを設定する
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetGMessage(ByVal frm As LMB020F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub

    ''' <summary>
    ''' 権限エラーメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rtnResult">チェック結果</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetAuthorityMessage(ByVal frm As LMB020F, ByVal rtnResult As Boolean) As Boolean

        '権限エラーの場合、メッセージ表示
        If rtnResult = False Then

            MyBase.ShowMessage(frm, "E016")

        End If

        Return rtnResult

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(画面情報格納)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMB020F, ByVal ds As DataSet, ByVal actionType As LMB020C.ActionType) As Boolean

        '入荷(大)のデータ設定
        Dim rtnResult As Boolean = Me.SetInkaLData(frm, ds)

        '入荷(中)のデータ設定
        rtnResult = rtnResult AndAlso Me.SetInkaMData(frm, ds)

        '入荷(小)と在庫のデータ設定
        rtnResult = rtnResult AndAlso Me.SetInkaSData(frm, ds)

        '入荷L単位で入荷WKを検品済みに更新する
        rtnResult = rtnResult AndAlso Me.SetInkaWkData(frm, ds)

        '作業(大)のデータ設定
        rtnResult = rtnResult AndAlso Me.SetSagyoLData(frm, ds)

        '作業(中)のデータ設定
        rtnResult = rtnResult AndAlso Me.SetSagyoMData(frm, ds)

        '作業データ(共通)の設定
        rtnResult = rtnResult AndAlso Me.SetSagyoComData(ds)

        '運送(中) , 運賃のデータ設定
        rtnResult = rtnResult AndAlso Me.SetUnsoMUnchinData(frm, ds, actionType)

        '運送(大)のデータ設定
        rtnResult = rtnResult AndAlso Me.SetUnsoLData(frm, ds)

        '印刷種別の設定
        rtnResult = rtnResult AndAlso Me.SetPrintType(frm, ds, actionType)

        'タブレット項目のデータ設定
        rtnResult = rtnResult AndAlso Me.SetTabletItemData(frm, ds, actionType)


        Return rtnResult

    End Function

    ''' <summary>
    ''' INKA_Lの情報を格納
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetInkaLData(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean

        Dim dr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)

        With frm

            dr.Item("INKA_NO_L") = .lblKanriNoL.TextValue
            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr.Item("WH_CD") = .cmbSoko.SelectedValue
            dr.Item("INKA_TP") = .cmbNyukaType.SelectedValue
#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
            dr.Item("INKA_KB_NM") = .txtNyukaKbn.TextValue
            dr.Item("INKA_STATE_KB") = Me.SetStateData(frm, ds, dr, frm.txtShinshokuKbnKbn.TextValue)
            dr.Item("INKA_STATE_KB_NM") = .txtShinshokuKbn.TextValue
#Else
            dr.Item("INKA_KB_NM") = .lblNyukaKbn.TextValue
            dr.Item("INKA_STATE_KB") = Me.SetStateData(frm, ds, dr, frm.lblShinshokuKbn.KbnValue)
            dr.Item("INKA_STATE_KB_NM") = .lblShinshokuKbn.TextValue
#End If

            dr.Item("INKA_DATE") = .imdNyukaDate.TextValue

            'ADD 2017/07/11 
#If True Then 'インターコンチ　総保入期限切れ防止対応
            If .imdStorageDueDate.Enabled = True _
                AndAlso (True).Equals(ChkInkaSLotIA(frm, ds)) Then

                If .imdStorageDueDate.TextValue.Trim = "" Then
                    Dim mmCnt As Integer = Convert.ToInt32(Convert.ToDouble( _
                          MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                          .Select("KBN_GROUP_CD = 'S109' AND KBN_CD = '01'")(0).Item("VALUE1")))

                    Dim tmpdate As Date = Date.Parse(Format(Convert.ToInt32(dr.Item("INKA_DATE")), "0000/00/00"))

                    dr.Item("STORAGE_DUE_DATE") = DateAdd("m", mmCnt, tmpdate).ToString("yyyyMMdd")
                Else
                    dr.Item("STORAGE_DUE_DATE") = .imdStorageDueDate.TextValue
                End If
            Else
                dr.Item("STORAGE_DUE_DATE") = String.Empty

            End If
#Else
            .imdStorageDueDate.Visible = False
            .lblTitleStorageDueDate.Visible = False
#End If
            dr.Item("FURI_NO") = .txtHuriKanriNo.TextValue
            dr.Item("BUYER_ORD_NO_L") = .txtBuyerOrdNo.TextValue
            dr.Item("OUTKA_FROM_ORD_NO_L") = .txtOrderNo.TextValue
            dr.Item("HOKAN_FREE_KIKAN") = .numFreeKikan.Value
            dr.Item("HOKAN_STR_DATE") = .imdHokanStrDate.TextValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("CUST_NM") = .lblCustNm.TextValue
            dr.Item("TAX_KB") = .cmbKazeiKbn.SelectedValue
            dr.Item("TOUKI_HOKAN_YN") = .cmbToukiHokanUmu.SelectedValue
            dr.Item("HOKAN_YN") = .cmbZenkiHokanUmu.SelectedValue
            dr.Item("NIYAKU_YN") = .cmbNiyakuUmu.SelectedValue
            dr.Item("INKA_PLAN_QT") = .numPlanQT.Value
            dr.Item("INKA_PLAN_QT_UT") = .cmbPlanQtUt.SelectedValue
            dr.Item("INKA_TTL_NB") = .numNyukaCnt.Value
            dr.Item("REMARK_OUT") = .txtNyubanL.TextValue.ToUpper()
            dr.Item("REMARK") = .txtNyukaComment.TextValue.ToUpper()

            dr.Item("WH_KENPIN_WK_STATUS") = .cmbWhWkStatus.SelectedValue

            '現場作業指示ステータス
            If RecordStatus.NEW_REC.Equals(.lblSituation.RecordStatus) = True Then
                dr.Item("WH_TAB_SAGYO_SIJI_STATUS") = LMB020C.WH_TAB_SIJI_00
            End If
            'Else
            '    If LMB020C.WH_TAB_SIJI_01.Equals(.cmbWHSagyoSijiStatus.SelectedValue) Then
            '        dr.Item("WH_TAB_SAGYO_SIJI_STATUS") = LMB020C.WH_TAB_SIJI_02
            '    Else
            '        dr.Item("WH_TAB_SAGYO_SIJI_STATUS") = .cmbWHSagyoSijiStatus.SelectedValue
            '    End If
            'End If
            If LMB020C.WH_TAB_SAGYO_05.Equals(.cmbWHSagyoStatus.SelectedValue) Then
                '取込済みのときは現場作業指示ステータスは変更しない
                dr.Item("WH_TAB_SAGYO_SIJI_STATUS") = .cmbWHSagyoSijiStatus.SelectedValue
            End If

            'タブレットデータ取込の未・済
            If LMB020C.WH_TAB_SAGYO_05.Equals(.cmbWHSagyoStatus.SelectedValue) Then
                dr.Item("WH_TAB_IMP_YN") = LMB020C.WH_TAB_IMP_YN_01
            Else
                dr.Item("WH_TAB_IMP_YN") = LMB020C.WH_TAB_IMP_YN_00
            End If

            '現場作業ステータス
            dr.Item("WH_TAB_SAGYO_STATUS") = .cmbWHSagyoStatus.SelectedValue

            '現場作業有無
            If .chkTablet.Checked = True Then
                dr.Item("WH_TAB_YN") = LMB020C.WH_TAB_YN_01
            Else
                dr.Item("WH_TAB_YN") = LMB020C.WH_TAB_YN_00
            End If

            '取込実行フラグ
            dr.Item("WH_TAB_IMP_PROC_FLG") = frm.txtWhWorkImpFlg.TextValue

            '出荷止
            'Del 2019/10/09 要望管理007373  dr.Item("STOP_ALLOC") = If(.chkStopAlloc.Checked, LMB020C.StopAllocYN.Yes, LMB020C.StopAllocYN.No)  'ADD 2019/08/01 要望管理005237

            '再指示不要フラグ
            If .chkNoSiji.Checked = True Then
                dr.Item("WH_TAB_NO_SIJI_FLG") = LMB020C.WH_TAB_NO_SIJI_YN_01
            Else
                dr.Item("WH_TAB_NO_SIJI_FLG") = LMB020C.WH_TAB_NO_SIJI_YN_00
            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' INKA_Mの情報を格納
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetInkaMData(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean

        Dim inkaNoM As String = frm.lblKanriNoM.TextValue

        '入荷中番がない場合、スルー
        If String.IsNullOrEmpty(inkaNoM) = True Then
            Return True
        End If

        Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)
        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprGoodsDef.ActiveSheet
        Dim max As Integer = spr.Rows.Count - 1

        With frm.sprGoodsDef

            For i As Integer = 0 To max

                '詳細の入荷中番と明細の入荷中番が同じ行を特定
                If inkaNoM.Equals(Me._LMBconV.GetCellValue(.ActiveSheet.Cells(i, LMB020G.sprGoodsDef.KANRI_NO.ColNo))) = False Then
                    Continue For
                End If

                '詳細情報を明細に反映
                .SetCellValue(i, LMB020G.sprGoodsDef.SORT_NO.ColNo, frm.numSort.TextValue)
                .SetCellValue(i, LMB020G.sprGoodsDef.ORDER_NO.ColNo, frm.txtOrderNoM.TextValue)
                .SetCellValue(i, LMB020G.sprGoodsDef.ALL_KOSU.ColNo, frm.numSumCnt.TextValue)
                .SetCellValue(i, LMB020G.sprGoodsDef.ALL_SURYO.ColNo, frm.numSuryo.TextValue)
                .SetCellValue(i, LMB020G.sprGoodsDef.GOODS_COMMENT.ColNo, frm.txtGoodsComment.TextValue)

                Dim sagyoUmu As String = String.Empty
                If String.IsNullOrEmpty(frm.txtSagyoCdM1.TextValue) = False OrElse _
                    String.IsNullOrEmpty(frm.txtSagyoCdM2.TextValue) = False OrElse _
                    String.IsNullOrEmpty(frm.txtSagyoCdM3.TextValue) = False OrElse _
                    String.IsNullOrEmpty(frm.txtSagyoCdM4.TextValue) = False OrElse _
                    String.IsNullOrEmpty(frm.txtSagyoCdM5.TextValue) = False Then
                    sagyoUmu = LMB020C.SAGYO_ARI
                Else
                    sagyoUmu = LMB020C.SAGYO_NASI
                End If
                .SetCellValue(i, LMB020G.sprGoodsDef.SAGYO_UMU.ColNo, sagyoUmu)

            Next

        End With

        'ヘッダ部の情報を反映
        ds = Me.SetInkaMHeaderData(frm, ds)

        Return True

    End Function

    ''' <summary>
    ''' 入荷(中)のヘッダ項目の反映処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetInkaMHeaderData(ByVal frm As LMB020F, ByVal ds As DataSet) As DataSet

        With frm

            Dim inkaNoM As String = .lblKanriNoM.TextValue
            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()
            Dim inkaNoL As String = .lblKanriNoL.TextValue
            Dim sql As String = String.Concat(" NRS_BR_CD = '", brCd, "' " _
                                              , " AND INKA_NO_L = '", inkaNoL, "' " _
                                              , " AND INKA_NO_M = '", inkaNoM, "' " _
                                              )
            Dim inkaMDr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(String.Concat(sql))(0)

            inkaMDr.Item("NRS_BR_CD") = brCd
            inkaMDr.Item("INKA_NO_L") = inkaNoL
            inkaMDr.Item("INKA_NO_M") = inkaNoM
            inkaMDr.Item("PRINT_SORT") = .numSort.Value
            inkaMDr.Item("OUTKA_FROM_ORD_NO_M") = .txtOrderNoM.TextValue
            inkaMDr.Item("BUYER_ORD_NO_M") = .txtBuyerOrdNoM.TextValue
            inkaMDr.Item("SUM_KOSU") = frm.numSumCnt.TextValue
            inkaMDr.Item("SUM_SURYO_M") = frm.numSuryo.TextValue
            inkaMDr.Item("REMARK") = .txtGoodsComment.TextValue
            'START YANAI 要望番号905
            inkaMDr.Item("SUM_JURYO_M") = frm.numTare.TextValue
            'END YANAI 要望番号905

            Dim sagyoUmu As String = String.Empty
            If String.IsNullOrEmpty(frm.txtSagyoCdM1.TextValue) = False OrElse _
                String.IsNullOrEmpty(frm.txtSagyoCdM2.TextValue) = False OrElse _
                String.IsNullOrEmpty(frm.txtSagyoCdM3.TextValue) = False OrElse _
                String.IsNullOrEmpty(frm.txtSagyoCdM4.TextValue) = False OrElse _
                String.IsNullOrEmpty(frm.txtSagyoCdM5.TextValue) = False Then
                sagyoUmu = LMB020C.SAGYO_ARI
            Else
                sagyoUmu = LMB020C.SAGYO_NASI
            End If
            inkaMDr.Item("SAGYO_UMU") = sagyoUmu

            Return ds

        End With

    End Function

    'START ADD 2013/09/10 KURIHARA WIT対応
    ''' <summary>
    ''' INKA_WKの情報を格納
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetInkaWkData(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean
        Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_WK)
        Dim dr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_WK).NewRow

        '入荷作業進捗区分が 40:検品済のとき、入荷WKの検品確定済フラグを更新
        Dim drl As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)
        If LMB020C.STATE_NYUKAKEPPINZUMI.Equals(drl.Item("INKA_STATE_KB")) Then

            With frm
                dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
                dr.Item("INKA_NO_L") = .lblKanriNoL.TextValue
                dr.Item("KENPIN_KAKUTEI_FLG") = LMB020C.KENPIN_KAKUTEI
            End With

            dt.Rows.Add(dr)

        End If

        Return True

    End Function
    'END   ADD 2013/09/10 KURIHARA WIT対応

    ''' <summary>
    ''' INKA_Sの情報を格納
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetInkaSData(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean

        'エラーになる場合があるので別インスタンス
        Dim inkaSDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S).Copy

        Dim sql As String = String.Concat(Me._G.SetInkaSSql(frm.lblKanriNoM.TextValue))
        Dim drs As DataRow() = inkaSDt.Select(sql, "INKA_NO_S")
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
        Dim recNo As Integer = 0

        With spr.ActiveSheet

            If -1 < max AndAlso drs.Count <> 0 Then

                Dim inkaMDr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(sql)(0)
                Dim sidIrime As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(inkaMDr.Item("STD_IRIME_NB_M").ToString()))
                Dim sidWt As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(inkaMDr.Item("STD_WT_KGS").ToString()))

                For i As Integer = 0 To max

                    '行番号を特定
                    recNo = Convert.ToInt32(Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.RECNO.ColNo)))

                    '値設定
                    drs(recNo).Item("INKA_NO_S") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.KANRI_NO_S.ColNo))
                    drs(recNo).Item("LOT_NO") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.LOT_NO.ColNo)).ToString().ToUpper()
                    drs(recNo).Item("TOU_NO") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.TOU_NO.ColNo))
                    drs(recNo).Item("SITU_NO") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.SHITSU_NO.ColNo))
                    drs(recNo).Item("ZONE_CD") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.ZONE_CD.ColNo))
                    'START YANAI 要望番号548
                    'drs(recNo).Item("LOCA") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.LOCA.ColNo)).ToUpper()
                    drs(recNo).Item("LOCA") = Me._LMBconV.GetCellValueNotTrim(.Cells(i, LMB020G.sprDetailDef.LOCA.ColNo)).ToUpper()
                    'END YANAI 要望番号548
                    drs(recNo).Item("KONSU") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.NB.ColNo))
                    drs(recNo).Item("HASU") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.HASU.ColNo))
                    drs(recNo).Item("KOSU_S") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.SUM.ColNo))
                    drs(recNo).Item("IRIME") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.IRIME.ColNo))
                    'START YANAI 運送・運行・請求メモNo.48
                    'drs(recNo).Item("BETU_WT") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.JURYO.ColNo))
                    drs(recNo).Item("BETU_WT") = Convert.ToString( _
                                                                    Me._G.ToRound( _
                                                                                  Convert.ToDecimal(Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.IRIME.ColNo))) * _
                                                                                  sidWt / _
                                                                                  sidIrime _
                                                                                  , LMB020C.JURYO_ROUND_POS _
                                                                                 ) _
                                                                 )
                    'END YANAI 運送・運行・請求メモNo.48
                    drs(recNo).Item("STD_IRIME_UT") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.TANI.ColNo))
                    drs(recNo).Item("SURYO_S") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.SURYO.ColNo))
                    drs(recNo).Item("JURYO_S") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.JURYO.ColNo))
                    drs(recNo).Item("LT_DATE") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.LT_DATE.ColNo))
                    'START YANAI 要望番号548
                    'drs(recNo).Item("REMARK") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.REMARK.ColNo)).ToUpper()
                    'drs(recNo).Item("REMARK_OUT") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.REMARK_OUT.ColNo)).ToUpper()
                    drs(recNo).Item("REMARK") = Me._LMBconV.GetCellValueNotTrim(.Cells(i, LMB020G.sprDetailDef.REMARK.ColNo)).ToUpper()
                    drs(recNo).Item("REMARK_OUT") = Me._LMBconV.GetCellValueNotTrim(.Cells(i, LMB020G.sprDetailDef.REMARK_OUT.ColNo)).ToUpper()
                    'END YANAI 要望番号548
                    drs(recNo).Item("GOODS_COND_KB_1") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.GOODS_COND_KB_1.ColNo))
                    drs(recNo).Item("GOODS_COND_KB_2") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.GOODS_COND_KB_2.ColNo))
                    drs(recNo).Item("GOODS_COND_KB_3") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.GOODS_COND_KB_3.ColNo))
                    drs(recNo).Item("OFB_KB") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.OFB_KBN.ColNo))
                    drs(recNo).Item("SPD_KB") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.SPD_KBN_S.ColNo))
                    drs(recNo).Item("SERIAL_NO") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.SERIAL_NO.ColNo))
                    drs(recNo).Item("GOODS_CRT_DATE") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.GOODS_CRT_DATE.ColNo))
                    drs(recNo).Item("DEST_CD") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.DEST_CD.ColNo))
                    drs(recNo).Item("DEST_NM") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.DEST_NM.ColNo))
                    drs(recNo).Item("ALLOC_PRIORITY") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.ALLOC_PRIORITY.ColNo))
                    drs(recNo).Item("LOT_CTL_KB") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.LOT_CTL_KB.ColNo))
                    drs(recNo).Item("LT_DATE_CTL_KB") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.LT_DATE_CTL_KB.ColNo))
                    drs(recNo).Item("CRT_DATE_CTL_KB") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.CRT_DATE_CTL_KB.ColNo))

                    drs(recNo).Item("IMG_YN") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.IMG_YN.ColNo))
                    drs(recNo).Item("BUG_YN") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.BUG_YN.ColNo))

                    ''2013.07.16 追加START
                    'If String.IsNullOrEmpty(Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.KENPIN_NO.ColNo))) = True Then
                    '    drs(recNo).Item("KENPIN_NO") = String.Empty
                    'Else
                    '    drs(recNo).Item("KENPIN_NO") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.KENPIN_NO.ColNo))
                    'End If
                    'drs(recNo).Item("GOODS_CD_NRS") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.GOODS_CD_NRS.ColNo))
                    'drs(recNo).Item("GOODS_CD_CUST") = Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.GOODS_CD_CUST.ColNo))
                    ''2013.07.16 追加END


                    ''CSV対応
                    'Dim csvDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_CSV_DATA)
                    'If String.IsNullOrEmpty(csvDt.Columns.Count.ToString) = False Then

                    '    Dim csvmax As Integer = csvDt.Rows.Count - 1
                    '    'CSVデータ追加
                    '    For i As Integer = 0 To csvmax

                    '        dr.Item("REMARK_OUT") = csvDt.Rows(i).Item("入番").ToString()
                    '        dr.Item("LOT_NO") = csvDt.Rows(i).Item("ロット").ToString()
                    '        dr.Item("SERIAL_NO") = csvDt.Rows(i).Item("ｼﾘﾝﾀﾞｰ").ToString()
                    '        dr.Item("REMARK") = csvDt.Rows(i).Item("コメント").ToString()
                    '        dr.Item("TOU_NO") = csvDt.Rows(i).Item("置場").ToString().Substring(0, 2)
                    '        dr.Item("SITU_NO") = csvDt.Rows(i).Item("置場").ToString().Substring(2, 1)  'ZONE_CDとの見分けが困難、室NOのほとんどが1バイトのための対応
                    '        dr.Item("ZONE_CD") = csvDt.Rows(i).Item("置場").ToString().Substring(3, 1)    'ZONE_CDとの見分けが困難、ゾーンCDのほとんどが1バイトのための対応
                    '    Next
                    'End If
                    ''''CSV対応おわり
                    '在庫の情報を設定
                    Call Me.SetZaikoData(ds, inkaMDr, drs(recNo))

                Next

            End If

        End With

        '別インスタンスの値を反映
        ds = Me.DataTableImport(Me._Ds, LMB020C.TABLE_NM_INKA_S, inkaSDt)

        Return True

    End Function

    ''' <summary>
    ''' UNSO_Lの情報を格納
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoLData(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With frm

            '運送(大)タブにある入荷(大)情報を設定
            Dim inkaLDr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)
            Dim tariff As String = .cmbUnchinUmu.SelectedValue.ToString()
            inkaLDr.Item("UNCHIN_TP") = tariff
            inkaLDr.Item("UNCHIN_KB") = .cmbUnchinKbn.SelectedValue
            Dim unsoDt As DataTable = ds.Tables(LMB020C.TABLE_NM_UNSO_L)
            Dim msg As String = String.Empty

            'レコードがない場合、空行追加
            If unsoDt.Rows.Count < 1 Then
                unsoDt.Rows.Add(Me.SetInitUnsoLData(unsoDt.NewRow()))
            End If
            Dim unsoDr As DataRow = unsoDt.Rows(0)

            '画面の値を設定
            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()
            unsoDr.Item("NRS_BR_CD") = brCd
            unsoDr.Item("YUSO_BR_CD") = .cmbYusoBrCd.SelectedValue.ToString()
            unsoDr.Item("CUST_CD_L") = inkaLDr.Item("CUST_CD_L").ToString()
            unsoDr.Item("CUST_CD_M") = inkaLDr.Item("CUST_CD_M").ToString()
            unsoDr.Item("VCLE_KB") = .cmbSharyoKbn.SelectedValue
            unsoDr.Item("UNSO_ONDO_KB") = .cmbTrnThermoKbn.SelectedValue
            unsoDr.Item("UNSO_CD") = .txtUnsoCd.TextValue
            unsoDr.Item("UNSO_BR_CD") = .txtTrnBrCD.TextValue
            unsoDr.Item("UNSOCO_NM") = .lblUnsoNm.TextValue
            unsoDr.Item("UNSOCO_BR_NM") = .lblUnsoBrNm.TextValue
            unsoDr.Item("TARE_YN") = .lblTareYn.TextValue
            unsoDr.Item("SEIQ_UNCHIN") = .numUnchin.Value
            unsoDr.Item("SEIQ_TARIFF_CD") = .txtUnsoTariffCD.TextValue
            Dim remStr As String = .lblUnsoTariffNM.TextValue
            unsoDr.Item("UNCHIN_TARIFF_REM") = remStr
            unsoDr.Item("YOKO_REM") = remStr
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
            unsoDr.Item("SHIHARAI_TARIFF_CD") = .txtShiharaiTariffCD.TextValue
            Dim remShiharai As String = .lblShiharaiTariffNM.TextValue
            unsoDr.Item("SHIHARAI_TARIFF_REM") = remShiharai
            unsoDr.Item("SHIHARAI_YOKO_REM") = remShiharai
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

            '運送サブの計算処理を呼び出す
            Dim unsoH As LMFControlH = New LMFControlH(frm, MyBase.GetPGID())

            'START YANAI 要望番号790
            'Dim wt As Decimal = unsoH.GetJuryoData(ds _
            '                                       , LMB020C.TABLE_NM_INKA_M _
            '                                       , LMB020C.TABLE_NM_UNSO_L _
            '                                       , LMB020C.TABLE_NM_UNSO_M _
            '                                       , "TARE_YN" _
            '                                       , "PKG_NB_UT2" _
            '                                       , "PKG_NB" _
            '                                       , "STD_IRIME_NB_M" _
            '                                       , "STD_WT_KGS" _
            '                                       )
            Dim wt As Decimal = unsoH.GetJuryoInkaData(ds _
                                                   , LMB020C.TABLE_NM_INKA_M _
                                                   , LMB020C.TABLE_NM_UNSO_L _
                                                   , LMB020C.TABLE_NM_UNSO_M _
                                                   , "TARE_YN" _
                                                   , "PKG_NB_UT2" _
                                                   , "PKG_NB" _
                                                   , "SUM_JURYO_M" _
                                                   , "STD_WT_KGS" _
                                                   )
            'END YANAI 要望番号790

            '運送重量の上限チェック
            If Me._V.IsCalcOver(wt.ToString(), LMB020C.UNSO_JURYO_MIN, LMB020C.UNSO_JURYO_MAX, .lblTitleUnso.TextValue) = False Then
                Return False
            End If

            '運送重量の切り上げ処理
            unsoDr.Item("UNSO_WT") = Math.Ceiling(wt)

            unsoDr.Item("ORIG_CD") = .txtShukkaMotoCD.TextValue
            unsoDr.Item("ORIG_CD_NM") = .lblShukkaMotoNM.TextValue

            '倉庫Mから取得
            Dim sokoDrs As DataRow() = Me._LMBconV.SelectSokoListDataRow(.cmbSoko.SelectedValue.ToString())
            Dim destCd As String = String.Empty
            If 0 < sokoDrs.Length Then
                destCd = sokoDrs(0).Item("UNSO_HATTI_CD").ToString()
            End If
            unsoDr.Item("DEST_CD") = destCd

            unsoDr.Item("KYORI") = .numKyori.Value
            unsoDr.Item("REMARK") = .txtUnchinComment.TextValue

            '梱数の合計
            Dim inkaSDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S).Copy
            Dim nb As String = unsoH.SetNbData(ds.Tables(LMB020C.TABLE_NM_UNSO_M)).ToString()

            '上限チェック
            '2017/09/25 修正 李↓
            msg = lgm.Selector({"運送梱包個数", "Transportation packing number", "운송포장개수", "中国語"})
            '2017/09/25 修正 李↑

            If Me._V.IsCalcOver(nb, LMB020C.NB_MIN, LMB020C.NB_MAX, msg) = False Then
                Return False
            End If
            unsoDr.Item("UNSO_PKG_NB") = nb

            '入荷(大)の情報を設定
            unsoDr.Item("OUTKA_PLAN_DATE") = inkaLDr.Item("INKA_DATE").ToString()
            unsoDr.Item("ARR_PLAN_DATE") = inkaLDr.Item("INKA_DATE").ToString()
            unsoDr.Item("CUST_REF_NO") = inkaLDr.Item("OUTKA_FROM_ORD_NO_L").ToString()

            '運送無しの場合、削除処理
            Dim delFlg As String = LMConst.FLG.OFF
            If LMB020C.TEHAI_NRS.Equals(tariff) = False Then
                delFlg = LMConst.FLG.ON
            End If
            unsoDr.Item("SYS_DEL_FLG") = delFlg

            unsoDr.Item("TARIFF_BUNRUI_KB") = .cmbUnchinKbn.SelectedValue.ToString()
            unsoDr.Item("TAX_KB") = .cmbTax.SelectedValue.ToString()
            unsoDr.Item("UNSO_TEHAI_KB") = .cmbUnchinUmu.SelectedValue.ToString()
            unsoDr.Item("UNCHIN") = .numUnchin.TextValue

        End With

        Return True

    End Function

    ''' <summary>
    ''' UNSO_Mの情報を格納
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoMUnchinData(ByVal frm As LMB020F, ByVal ds As DataSet, ByVal actionType As LMB020C.ActionType) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '保存以外、スルー
        If LMB020C.ActionType.SAVE <> actionType Then
            Return True
        End If

        '運送(中),運賃はクリア
        Dim unsoMDt As DataTable = ds.Tables(LMB020C.TABLE_NM_UNSO_M)
        Dim unsoMDr As DataRow = Nothing
        Dim unchinDt As DataTable = ds.Tables(LMB020C.TABLE_NM_UNCHIN)
        unsoMDt.Clear()
        unchinDt.Clear()

        '日陸手配以外、スルー
        If LMB020C.TEHAI_NRS.Equals(frm.cmbUnchinUmu.SelectedValue.ToString()) = False Then
            Return True
        End If

        '設定情報
        Dim inkaLDr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)
        Dim inkaMDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)
        Dim inkaSDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S).Copy
        Dim inkaSDrs As DataRow() = Nothing
        Dim cnt As Integer = 0
        Dim nb As Decimal = 0
        Dim qt As Decimal = 0
        Dim kosu As Decimal = 0
        Dim max As Integer = inkaMDt.Rows.Count - 1
        Dim eigyoKbn As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim ondoKbn As String = frm.cmbTrnThermoKbn.SelectedValue.ToString()
        Dim irisu As Decimal = 0
        Dim konsuIri As Decimal = 0
        Dim hasu As Decimal = 0
        Dim lotNo As String = String.Empty
        Dim msg As String = String.Empty

        For i As Integer = 0 To max

            '削除データの場合、スルー
            If LMConst.FLG.ON.Equals(inkaMDt.Rows(i).Item("SYS_DEL_FLG").ToString()) = True Then
                Continue For
            End If

            '運送(中)レコードの新スタンス生成
            unsoMDr = unsoMDt.NewRow()
            With unsoMDr

                .Item("NRS_BR_CD") = eigyoKbn
                .Item("UNSO_NO_M") = inkaMDt.Rows(i).Item("INKA_NO_M").ToString()
                .Item("GOODS_CD_NRS") = inkaMDt.Rows(i).Item("GOODS_CD_NRS").ToString()
                .Item("GOODS_NM") = inkaMDt.Rows(i).Item("GOODS_NM").ToString()

                '変数の初期化
                nb = 0
                qt = 0
                hasu = 0
                kosu = 0
                konsuIri = 0
                irisu = Convert.ToDecimal(Me._LMBconG.FormatNumValue(inkaMDt.Rows(i).Item("PKG_NB").ToString()))

                '紐付く入荷(小)を取得
                inkaSDrs = inkaSDt.Select(Me.SetSqlSelectInkaS(frm, inkaMDt.Rows(i).Item("INKA_NO_M").ToString()))
                cnt = inkaSDrs.Length - 1
                For j As Integer = 0 To cnt
                    nb += Convert.ToDecimal(inkaSDrs(j).Item("KONSU").ToString())
                    qt += Convert.ToDecimal(inkaSDrs(j).Item("SURYO_S").ToString())
                    hasu += Convert.ToDecimal(inkaSDrs(j).Item("HASU").ToString())
                    kosu += Convert.ToDecimal(inkaSDrs(j).Item("KOSU_S").ToString())
                Next
                If 0 < inkaSDrs.Length Then
                    lotNo = inkaSDrs(0).Item("LOT_NO").ToString()
                End If

                '2017/09/25 修正 李↓
                msg = lgm.Selector({"運送数量", "Transportation quantity", "운송수량", "中国語"})
                '2017/09/25 修正 李↑

                '上限チェック
                If Me._V.IsCalcOver(qt.ToString(), LMB020C.SURYO_MIN, LMB020C.SURYO_MAX, msg) = False Then
                    Return False
                End If

                '数量の設定
                .Item("UNSO_TTL_QT") = qt

                '運送個数と梱包個数が0でない場合
                If 0 <> nb AndAlso irisu <> 0 Then

                    '運送個数 + 梱包個数 / 端数(切捨て)
                    konsuIri = System.Math.Floor(hasu / irisu)
                    nb = nb + konsuIri

                    '端数 - 梱包個数 / 端数(切捨て)
                    hasu = hasu - konsuIri * irisu

                End If

                '2017/09/25 修正 李↓
                msg = lgm.Selector({"運送個数", "Transportation number", "운송개수", "中国語"})
                '2017/09/25 修正 李↑

                '上限チェック
                If Me._V.IsCalcOver(nb.ToString(), LMB020C.NB_MIN, LMB020C.NB_MAX, "運送個数") = False Then
                    Return False
                End If

                .Item("UNSO_TTL_NB") = nb
                .Item("HASU") = hasu

                .Item("NB_UT") = inkaMDt.Rows(i).Item("NB_UT").ToString()
                .Item("QT_UT") = inkaMDt.Rows(i).Item("PKG_NB_UT2").ToString()
                .Item("BETU_WT") = inkaMDt.Rows(i).Item("STD_WT_KGS").ToString()

                '属性初期値
                .Item("ZAI_REC_NO") = String.Empty
                .Item("IRIME") = inkaMDt.Rows(i).Item("STD_IRIME_NB_M").ToString()
                .Item("IRIME_UT") = String.Empty
                .Item("SIZE_KB") = String.Empty
                .Item("ABUKA_CD") = String.Empty
                .Item("PKG_NB") = irisu
                .Item("LOT_NO") = lotNo
                .Item("UNSO_ONDO_KB") = ondoKbn
                .Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                'START YANAI 要望番号1016
                .Item("UP_KBN") = LMConst.FLG.OFF
                'END YANAI 要望番号1016

            End With

            '行追加
            unsoMDt.Rows.Add(unsoMDr)

        Next

        Return True

    End Function

    ''' <summary>
    ''' Printの情報を格納
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetPrintType(ByVal frm As LMB020F, ByVal ds As DataSet, ByVal actionType As LMB020C.ActionType) As Boolean

        If LMB020C.ActionType.PRINT <> actionType Then
            Return True
        End If

        ds.Tables(LMB020C.TABLE_NM_PRINT_TYPE).Rows.Clear()
        Dim dr As DataRow = ds.Tables(LMB020C.TABLE_NM_PRINT_TYPE).NewRow()

        dr.Item("PRINT_TYPE") = frm.cmbPrint.SelectedValue

        ds.Tables(LMB020C.TABLE_NM_PRINT_TYPE).Rows.Add(dr)

        Return True

    End Function

    ''' <summary>
    ''' 距離設定
    ''' </summary>
    ''' <param name="unsoLDr">DataRow</param>
    ''' <param name="unchinDr">DataRow</param>
    ''' <returns>値</returns>
    ''' <remarks></remarks>
    Private Function SetKyoriData(ByVal unsoLDr As DataRow, ByVal unchinDr As DataRow) As String

        If 0 < Convert.ToInt32(unsoLDr.Item("KYORI").ToString()) Then
            Return unsoLDr.Item("KYORI").ToString()
        End If

        Return "0"

    End Function

    ''' <summary>
    ''' SAGYO_Lの情報を格納
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoLData(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean

        Return Me.SetSagyoData(frm, ds, LMB020C.SagyoData.L, LMB020C.MAEZERO)

    End Function

    ''' <summary>
    ''' SAGYO_Mの情報を格納
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoMData(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean

        Return Me.SetSagyoData(frm, ds, LMB020C.SagyoData.M, frm.lblKanriNoM.TextValue)

    End Function

    ''' <summary>
    ''' SAGYOの情報を格納
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="type">設定するタイプ</param>
    ''' <param name="inkaNoM">入荷中番</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoData(ByVal frm As LMB020F, ByVal ds As DataSet, ByVal type As LMB020C.SagyoData, ByVal inkaNoM As String) As Boolean

        With frm

            Dim inkaLDr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)
            Dim sagyoDt As DataTable = ds.Tables(LMB020C.TABLE_NM_SAGYO)
            Dim sql As String = Me._G.SetSagyoSql(inkaLDr, inkaNoM)

            Dim sagyoDrs As DataRow() = sagyoDt.Select(sql, "SAGYO_REC_NO")

            '画面の値を別DataTableに設定
            Dim guiDs As DataSet = Me.SetSagyoGuiData(frm, ds, type, inkaNoM)

            'エラーの場合、スルー
            If guiDs Is Nothing = True Then
                Return False
            End If

            Dim guiDt As DataTable = guiDs.Tables(LMB020C.TABLE_NM_SAGYO)
            Dim setDrs As DataRow() = Nothing
            Dim cnt As Integer = 0
            Dim max As Integer = sagyoDrs.Length - 1

            For i As Integer = 0 To max
                setDrs = guiDt.Select(String.Concat(sql, " AND SAGYO_CD = '", sagyoDrs(i).Item("SAGYO_CD").ToString(), "' "))
                cnt = setDrs.Length - 1

                '画面にない値を保持している場合
                If cnt < 0 Then

                    'DB登録済みの場合、'2'を設定
                    If LMB020C.UPDATE.Equals(sagyoDrs(i).Item("UP_KBN")) = True Then
                        sagyoDrs(i).Item("UP_KBN") = LMB020C.DELETE
                        'START YANAI 要望番号376
                        sagyoDrs(i).Item("SYS_DEL_FLG") = "01"
                        'END YANAI 要望番号376
                    Else
                        sagyoDrs(i).Delete()
                    End If
                    Continue For

                End If

                sagyoDrs(i).Item("GOODS_CD_NRS") = setDrs(0).Item("GOODS_CD_NRS").ToString()
                sagyoDrs(i).Item("GOODS_NM_NRS") = setDrs(0).Item("GOODS_NM_NRS").ToString()
                sagyoDrs(i).Item("LOT_NO") = setDrs(0).Item("LOT_NO").ToString()
                sagyoDrs(i).Item("SAGYO_NB") = setDrs(0).Item("SAGYO_NB").ToString()
                sagyoDrs(i).Item("SAGYO_GK") = setDrs(0).Item("SAGYO_GK").ToString()
                sagyoDrs(i).Item("SEIQTO_CD") = setDrs(0).Item("SEIQTO_CD").ToString()
                sagyoDrs(i).Item("REMARK_SIJI") = setDrs(0).Item("REMARK_SIJI").ToString()
                'DataTableに設定済みの場合
                If LMB020C.INSERT.Equals(sagyoDrs(i).Item("UP_KBN")) = False Then
                    sagyoDrs(i).Item("UP_KBN") = LMB020C.UPDATE
                End If

                '設定したレコードは削除
                For j As Integer = 0 To cnt
                    setDrs(j).Delete()
                Next

            Next

            '画面の情報を反映できていないレコードを設定
            max = guiDt.Rows.Count - 1
            For i As Integer = 0 To max
                sagyoDt.ImportRow(guiDt.Rows(i))
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 作業レコードの共通値設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoComData(ByVal ds As DataSet) As Boolean

        '動的に換わる項目について全レコード設定
        Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_SAGYO)
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max
            dt.Rows(i).Item("SAGYO_COMP") = Me.SetSagyoEndData(dt.Rows(i))
        Next

        Return True

    End Function

    ''' <summary>
    ''' 作業画面の値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="type">作業タイプ</param>
    ''' <param name="inkaNoM">入荷中番号</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoGuiData(ByVal frm As LMB020F, ByVal ds As DataSet, ByVal type As LMB020C.SagyoData, ByVal inkaNoM As String) As DataSet

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '入荷(大)の情報
        Dim inkaLDr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)

        '入荷(中)の情報
        Dim sql As String = Me.SetSqlSelectInkaS(frm, inkaNoM)
        Dim inkaMDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)
        Dim inkaMDrs As DataRow() = inkaMDt.Select(sql)

        '請求先コードを設定
        Dim seiqtoCd As String = Me.SetSagyoSeiqtoCd(inkaMDt, inkaLDr, inkaMDrs, type)

        '入荷(小)の情報
        Dim inkaSDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S).Copy
        Dim inkaSDrs As DataRow() = inkaSDt.Select(sql, " INKA_NO_S ")
        Dim lotNo As String = String.Empty
        Dim msg As String = String.Empty
        If 0 < inkaSDrs.Length Then
            lotNo = inkaSDrs(0).Item("LOT_NO").ToString()
        ElseIf LMB020C.MAEZERO.Equals(inkaNoM) = True AndAlso _
            0 < ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows.Count Then
            Dim inkaMDr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows(0)
            Dim sql2 As String = Me.SetSqlSelectInkaS(frm, inkaMDr.Item("INKA_NO_M").ToString())
            Dim inkaSDrs2 As DataRow() = inkaSDt.Select(sql2, " INKA_NO_S ")
            If 0 < inkaSDrs2.Length Then
                lotNo = inkaSDrs2(0).Item("LOT_NO").ToString()
            End If
        End If

        'Dim sagyoNb As Decimal = Me.SetNbData(inkaSDrs) 'notes 1807 2013/02/08 コメントアウト
        Dim sagyoNb As Decimal = Me.SetNbDataSagyo(inkaSDrs, inkaMDrs) 'notes 1807 2013/02/08 追記
        Dim sagyoNbData As String = String.Empty

        '作業(大)の場合、全レコード
        If LMB020C.MAEZERO.Equals(inkaNoM) = True Then

            'sagyoNb = Me.SetNbData(inkaSDt.Select(String.Empty)) 'notes 1807 2013/02/08 コメントアウト
            sagyoNb = Me.SetNbDataSagyo(inkaSDt.Select(String.Empty), inkaMDt.Select(String.Empty)) 'notes 1807 2013/02/08 追記

        End If

        '小数点が増えることはないため四捨五入不要
        sagyoNbData = sagyoNb.ToString()

        '2017/09/25 修正 李↓
        msg = lgm.Selector({"今回請求数", "This claim number", "이번 청구수", "中国語"})
        '2017/09/25 修正 李↑

        '上限チェック
        If Me._V.IsCalcOver(sagyoNbData, LMB020C.NB_MIN, LMB020C.NB_MAX, msg) = False Then
            Return Nothing
        End If

        '設定するコントロールの文字
        Dim typeStr As String = type.ToString()
        Dim sagyoStr As String = String.Concat(LMB020C.SAGYO_PK, typeStr)
        Dim txtStr As String = String.Concat(LMB020C.SAGYO_CD, typeStr)
        Dim lblStr As String = String.Concat(LMB020C.SAGYO_NM, typeStr)
        Dim flgStr As String = String.Concat(LMB020C.SAGYO_FL, typeStr)
        Dim upFlgStr As String = String.Concat(LMB020C.SAGYO_UP, typeStr)
        Dim rmkSijiStr As String = String.Concat(LMB020C.SAGYO_RMK_SIJI, typeStr)

        '画面の値を別DataTableに設定
        Dim sagyoDs As DataSet = New LMB020DS()
        Dim sagyoDt As DataTable = sagyoDs.Tables(LMB020C.TABLE_NM_SAGYO)
        Dim sagyoDr As DataRow = Nothing

        '作業レコード最大数ループ
        Dim max As Integer = LMB020C.SAGYO_MAX_REC - 1
        Dim sagyoCd As String = String.Empty
        Dim goodsCd As String = String.Empty
        Dim goodsNm As String = String.Empty

        'キャッシュの値(作業項目M)
        Dim mSagyoDrs As DataRow() = Nothing
        Dim invTani As String = String.Empty
        Dim sagyoUp As String = String.Empty
        Dim sagyoGk As String = String.Empty
        Dim sagyoNm As String = String.Empty
        Dim zeiKbn As String = String.Empty
        Dim recNo As String = String.Empty

        If 0 < inkaMDrs.Length Then
            goodsCd = inkaMDrs(0).Item("GOODS_CD_NRS").ToString()
            goodsNm = inkaMDrs(0).Item("GOODS_NM").ToString()
        End If

        Dim eigyoCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim custCdL As String = frm.txtCustCdL.TextValue
        Dim custCdM As String = frm.txtCustCdM.TextValue

        '2017/09/25 修正 李↓
        Dim stageMi As String = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_S052, "' AND KBN_CD = '", LMB020C.FLG_OFF, "' "))(0).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString()
        Dim stageKn As String = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_S052, "' AND KBN_CD = '", LMB020C.FLG_ON, "' "))(0).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString()
        '2017/09/25 修正 李↑

        Dim stage As String = String.Empty

        For i As Integer = 0 To max

            '初期設定
            recNo = (i + 1).ToString()
            sagyoNbData = sagyoNb.ToString()

            '作業コードの設定
            sagyoCd = Me._G.GetTextControl(String.Concat(txtStr, recNo)).TextValue

            '値があるか判定
            If String.IsNullOrEmpty(sagyoCd) = True Then

                'ない場合、次の行へ
                Continue For

            End If

            '行追加
            sagyoDr = Me.SetInitSagyoLData(sagyoDt.NewRow())

            '作業(大)と(中)で設定値が異なる
            If LMB020C.MAEZERO.Equals(inkaNoM) = True Then
                sagyoDr.Item("IOZS_KB") = LMB020C.MOTODATA_KBN_INKA_L
            Else
                sagyoDr.Item("IOZS_KB") = LMB020C.MOTODATA_KBN_INKA_M
                'SBS残作業№36　修正（入荷（大）の作業レコードにはロット番号を設定しない）
                '入荷(小)の情報
                sagyoDr.Item("LOT_NO") = lotNo
            End If

            invTani = String.Empty
            sagyoUp = String.Empty
            zeiKbn = String.Empty

            '作業入力欄の値
            stage = Me.SetSagyoEndData(sagyoDr)
            sagyoDr.Item("SAGYO_COMP") = stage
            If LMB020C.FLG_OFF.Equals(stage) = True Then
                sagyoDr.Item("SAGYO_COMP_NM") = stageMi
            Else
                sagyoDr.Item("SAGYO_COMP_NM") = stageKn
            End If
            sagyoDr.Item("INOUTKA_NO_LM") = String.Concat(frm.lblKanriNoL.TextValue, inkaNoM)
            sagyoDr.Item("SAGYO_CD") = sagyoCd
            sagyoDr.Item("SAGYO_RYAK") = Me._G.GetTextControl(String.Concat(lblStr, recNo)).TextValue
            sagyoDr.Item("REMARK_SIJI") = Me._G.GetTextControl(String.Concat(rmkSijiStr, recNo)).TextValue

            'キャッシュの値(作業項目M)
            'START YANAI 要望番号376
            'mSagyoDrs = Me._LMBconV.SelectSagyoListDataRow(eigyoCd, sagyoCd, custCdL)
            Dim SelectSagyoString As String = String.Empty
            '削除フラグ
            SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")
            '作業コード
            SelectSagyoString = String.Concat(SelectSagyoString, " AND SAGYO_CD = '", sagyoCd, "' ")
            '営業所コード
            SelectSagyoString = String.Concat(SelectSagyoString, " AND NRS_BR_CD = '", eigyoCd, "' ")
            '荷主コード
            SelectSagyoString = String.Concat(SelectSagyoString, " AND (CUST_CD_L = '", custCdL, "' OR CUST_CD_L = 'ZZZZZ')")

            mSagyoDrs = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(SelectSagyoString)
            'END YANAI 要望番号376
            If 0 < mSagyoDrs.Length Then

                sagyoNm = mSagyoDrs(0).Item("SAGYO_NM").ToString()
                invTani = mSagyoDrs(0).Item("INV_TANI").ToString()
                sagyoUp = mSagyoDrs(0).Item("SAGYO_UP").ToString()
                zeiKbn = mSagyoDrs(0).Item("ZEI_KBN").ToString()

                '請求金額計算区分 = '01'の場合、1を設定
                If LMB020C.SEIQ_CALC_ZERO.Equals(mSagyoDrs(0).Item("KOSU_BAI").ToString()) = True Then
                    sagyoNbData = 1.ToString()
                End If

            Else

                '取得できない場合、初期値設定
                sagyoNbData = 0.ToString()
                sagyoUp = 0.ToString()

            End If
            sagyoDr.Item("SAGYO_NM") = sagyoNm
            sagyoDr.Item("INV_TANI") = invTani
            sagyoDr.Item("SAGYO_UP") = sagyoUp
            sagyoDr.Item("TAX_KB") = zeiKbn
            sagyoDr.Item("SAGYO_NB") = sagyoNbData

            '小数点が増えることはないため四捨五入不要
            'START YANAI メモ②No.16
            'sagyoGk = (Convert.ToDecimal(sagyoUp) * Convert.ToDecimal(sagyoNbData)).ToString()
            sagyoGk = Convert.ToString(Math.Round((Convert.ToDecimal(sagyoUp) * Convert.ToDecimal(sagyoNbData)), MidpointRounding.AwayFromZero))
            'END YANAI メモ②No.16

            '2017/09/25 修正 李↓
            msg = lgm.Selector({"作業金額", "Work amount", "작업금액", "中国語"})
            '2017/09/25 修正 李↑

            '上限チェック
            If Me._V.IsCalcOver(sagyoGk, LMB020C.SURYO_MIN, LMB020C.SURYO_MAX, msg) = False Then
                Return Nothing
            End If
            sagyoDr.Item("SAGYO_GK") = sagyoGk

            '入荷(中)の情報
            sagyoDr.Item("GOODS_CD_NRS") = goodsCd
            sagyoDr.Item("GOODS_NM_NRS") = goodsNm

            '常にロックの情報を設定
            sagyoDr.Item("NRS_BR_CD") = eigyoCd
            sagyoDr.Item("CUST_CD_L") = custCdL
            sagyoDr.Item("CUST_CD_M") = custCdM

            sagyoDr.Item("SEIQTO_CD") = seiqtoCd

            '行追加
            sagyoDt.Rows.Add(sagyoDr)

        Next

        Return sagyoDs

    End Function

    ''' <summary>
    ''' ZAIKOの情報を格納
    ''' </summary>
    ''' <param name="inkaMDr">DataRow</param>
    ''' <param name="inkaSDr">DataRow</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetZaikoData(ByVal ds As DataSet, ByVal inkaMDr As DataRow, ByVal inkaSDr As DataRow) As Boolean

        Dim inkaLDr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)
        'START YANAI 要望番号1001
        Dim max As Integer = 0
        'END YANAI 要望番号1001
        With inkaSDr

            '設定先データの取得 
            Dim zaikoDr As DataRow = Nothing
            Dim inkaMNo As String = .Item("INKA_NO_M").ToString()
            Dim inkaSNo As String = .Item("INKA_NO_S").ToString()
            Dim zaikoDt As DataTable = ds.Tables(LMB020C.TABLE_NM_ZAI)
            Dim zaikoDrs As DataRow() = zaikoDt.Select(String.Concat("INKA_NO_M = '", inkaMNo, "' " _
                                                                     , " AND INKA_NO_S = '", inkaSNo, "' "))
            If 0 < zaikoDrs.Length Then

                'すでにある場合
                zaikoDr = zaikoDrs(0)

            Else

                'ない場合、空行追加
                zaikoDt.Rows.Add(zaikoDt.NewRow())
                zaikoDr = zaikoDt.Rows(zaikoDt.Rows.Count - 1)

                '管理番号の設定
                zaikoDr.Item("INKA_NO_M") = inkaMNo
                zaikoDr.Item("INKA_NO_S") = inkaSNo
                zaikoDr.Item("UP_KBN") = 0
                zaikoDr.Item("SMPL_FLAG") = LMB020C.FLG_OFF
                zaikoDr.Item("INKO_DATE") = String.Empty

                'WIT対応 2013.12.09 Start
                zaikoDr.Item("GOODS_KANRI_NO") = .Item("TORIKOMI_GOODS_KANRI_NO").ToString
                'WIT対応 2013.12.09 End

            End If

            Dim irisu As String = inkaMDr.Item("PKG_NB").ToString()
            Dim konsu As String = .Item("KONSU").ToString()
            Dim irime As String = .Item("IRIME").ToString()
            Dim hasu As String = .Item("HASU").ToString()

            zaikoDr.Item("ALCTD_NB") = 0
            zaikoDr.Item("ALCTD_QT") = 0

            Dim poraZaiNb As String = Me.SetPoraZaiNb(konsu, irisu, hasu)

            'zaikoDr.Item("PORA_ZAI_NB") = poraZaiNb
            zaikoDr.Item("ALLOC_CAN_NB") = poraZaiNb
            zaikoDr.Item("PORA_ZAI_QT") = Me.SetZaikoSuData(poraZaiNb, irime)
            zaikoDr.Item("ALLOC_CAN_QT") = zaikoDr.Item("PORA_ZAI_QT").ToString()
            'START YANAI 要望番号1001
            'zaikoDr.Item("INKO_PLAN_DATE") = inkaLDr.Item("INKA_DATE").ToString()
            'END YANAI 要望番号1001
            zaikoDr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

            'START YANAI 要望番号1001
            ''入庫日設定
            'Select Case inkaLDr.Item("INKA_STATE_KB").ToString()

            '    '入荷作業進捗区分が予定("10")、受付票印刷("20")、受付済("30")、入荷検品済("40")の時
            '    Case "10", "20", "30", "40"
            '        zaikoDr.Item("INKO_DATE") = zaikoDr.Item("INKO_DATE").ToString()

            '    Case Else '上記以外（入荷後）
            '        zaikoDr.Item("INKO_DATE") = inkaLDr.Item("INKA_DATE").ToString()
            'End Select
            'EDIからの登録の場合、入荷(小)はあるが、在庫データがない状態で、入荷(小)を1度表示しないと在庫データが作成されなそうなので、
            '入荷(小)を表示しなくても在庫データを作成するように対応
            Dim zaikoDrs2 As DataRow() = Nothing
            Dim inkaMDrs As DataRow() = Nothing
            Dim inkaSDrs As DataRow() = Nothing
            max = ds.Tables(LMB020C.TABLE_NM_INKA_S).Rows.Count - 1
            For i As Integer = 0 To max
                If (inkaMNo).Equals(ds.Tables(LMB020C.TABLE_NM_INKA_S).Rows(i).Item("INKA_NO_M").ToString) = False OrElse _
                    (inkaSNo).Equals(ds.Tables(LMB020C.TABLE_NM_INKA_S).Rows(i).Item("INKA_NO_S").ToString) = False Then
                    '上記の処理にて作成した入荷(中)(小)の組み合わせは作成の必要がない
                    zaikoDrs2 = zaikoDt.Select(String.Concat("INKA_NO_M = '", ds.Tables(LMB020C.TABLE_NM_INKA_S).Rows(i).Item("INKA_NO_M").ToString, "' " _
                                                           , " AND INKA_NO_S = '", ds.Tables(LMB020C.TABLE_NM_INKA_S).Rows(i).Item("INKA_NO_S").ToString, "' "))
                    If 0 = zaikoDrs2.Length Then
                        '20130322 要望番号1934 START
                        '入荷(小)の削除フラグが立っている在庫データは作成しない
                        If (LMConst.FLG.ON).Equals(ds.Tables(LMB020C.TABLE_NM_INKA_S).Rows(i).Item("SYS_DEL_FLG").ToString()) = True Then
                            Continue For
                        Else
                            '20130322 要望番号1934  END
                            'ない場合、空行追加
                            zaikoDt.Rows.Add(zaikoDt.NewRow())
                            zaikoDr = zaikoDt.Rows(zaikoDt.Rows.Count - 1)

                            '管理番号の設定
                            zaikoDr.Item("INKA_NO_M") = ds.Tables(LMB020C.TABLE_NM_INKA_S).Rows(i).Item("INKA_NO_M").ToString
                            zaikoDr.Item("INKA_NO_S") = ds.Tables(LMB020C.TABLE_NM_INKA_S).Rows(i).Item("INKA_NO_S").ToString
                            zaikoDr.Item("UP_KBN") = 0
                            zaikoDr.Item("SMPL_FLAG") = LMB020C.FLG_OFF
                            zaikoDr.Item("INKO_DATE") = String.Empty

                            inkaMDrs = ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(String.Concat("INKA_NO_M = '", ds.Tables(LMB020C.TABLE_NM_INKA_S).Rows(i).Item("INKA_NO_M").ToString, "'"))
                            irisu = inkaMDrs(0).Item("PKG_NB").ToString

                            inkaSDrs = ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(String.Concat("INKA_NO_M = '", ds.Tables(LMB020C.TABLE_NM_INKA_S).Rows(i).Item("INKA_NO_M").ToString, "' " _
                                                                                              , " AND INKA_NO_S = '", ds.Tables(LMB020C.TABLE_NM_INKA_S).Rows(i).Item("INKA_NO_S").ToString, "' "))
                            konsu = inkaSDrs(0).Item("KONSU").ToString()
                            irime = inkaSDrs(0).Item("IRIME").ToString()
                            hasu = inkaSDrs(0).Item("HASU").ToString()

                            zaikoDr.Item("ALCTD_NB") = 0
                            zaikoDr.Item("ALCTD_QT") = 0

                            poraZaiNb = Me.SetPoraZaiNb(konsu, irisu, hasu)

                            zaikoDr.Item("ALLOC_CAN_NB") = poraZaiNb
                            zaikoDr.Item("PORA_ZAI_QT") = Me.SetZaikoSuData(poraZaiNb, irime)
                            zaikoDr.Item("ALLOC_CAN_QT") = zaikoDr.Item("PORA_ZAI_QT").ToString()
                            zaikoDr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                            '20130322 要望番号1934 START

                            'WIT対応 2013.12.10 Start
                            zaikoDr.Item("GOODS_KANRI_NO") = ds.Tables(LMB020C.TABLE_NM_INKA_S).Rows(i).Item("TORIKOMI_GOODS_KANRI_NO").ToString
                            'WIT対応 2013.12.10 End

                        End If
                        '20130322 要望番号1934  END
                    End If
                End If
            Next

            '全部の在庫データの入庫日・入庫予定日を再設定
            max = ds.Tables(LMB020C.TABLE_NM_ZAI).Rows.Count - 1
            For i As Integer = 0 To max
                '入庫日設定
                Select Case inkaLDr.Item("INKA_STATE_KB").ToString()

                    '入荷作業進捗区分が予定("10")、受付票印刷("20")、受付済("30")、入荷検品済("40")の時
                    Case "10", "20", "30", "40"
                        ds.Tables(LMB020C.TABLE_NM_ZAI).Rows(i).Item("INKO_DATE") = ds.Tables(LMB020C.TABLE_NM_ZAI).Rows(i).Item("INKO_DATE").ToString()

                    Case Else '上記以外（入荷後）
                        ds.Tables(LMB020C.TABLE_NM_ZAI).Rows(i).Item("INKO_DATE") = inkaLDr.Item("INKA_DATE").ToString()
                End Select
                '入庫予定日設定
                ds.Tables(LMB020C.TABLE_NM_ZAI).Rows(i).Item("INKO_PLAN_DATE") = inkaLDr.Item("INKA_DATE").ToString()
            Next
            'END YANAI 要望番号1001

        End With

        Return True

    End Function

    ''' <summary>
    ''' UNSO_Lの初期値設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Private Function SetInitUnsoLData(ByVal dr As DataRow) As DataRow

        With dr

            .Item("TRIP_NO") = String.Empty
            .Item("BIN_KB") = LMB020C.BIN_KBN_NOMAL
            .Item("JIYU_KB") = LMB020C.JIYU_KBN_TORIHIKI
            .Item("DENP_NO") = String.Empty
            .Item("OUTKA_PLAN_TIME") = String.Empty
            .Item("ARR_PLAN_TIME") = String.Empty
            .Item("ARR_ACT_TIME") = String.Empty
            .Item("SHIP_CD") = String.Empty
            .Item("PC_KB") = LMB020C.PC_KB_MOTO
            .Item("MOTO_DATA_KB") = LMB020C.MOTODATA_KBN_INKA
            .Item("BUY_CHU_NO") = String.Empty
            .Item("AREA_CD") = String.Empty
            .Item("TYUKEI_HAISO_FLG") = "00"
            .Item("SYUKA_TYUKEI_CD") = String.Empty
            .Item("HAIKA_TYUKEI_CD") = String.Empty
            .Item("TRIP_NO_SYUKA") = String.Empty
            .Item("TRIP_NO_HAIKA") = String.Empty
            .Item("FIXED_CHK") = 0
            .Item("GROUP_CHK") = 0
            .Item("UP_KBN") = LMB020C.INSERT

            'エラー記録表（支払）No3:(新規登録後すぐに編集で運賃確定済みのエラー)  2012/08/24 本明 Start
            .Item("SHIHARAI_TARIFF_CD") = String.Empty
            .Item("SHIHARAI_ETARIFF_CD") = String.Empty
            .Item("SHIHARAI_FIXED_CHK") = 0
            .Item("SHIHARAI_GROUP_CHK") = 0
            .Item("SHIHARAI_TARIFF_REM") = String.Empty
            .Item("SHIHARAI_YOKO_REM") = String.Empty
            'エラー記録表（支払）No3:(新規登録後すぐに編集で運賃確定済みのエラー)  2012/08/24 本明 End

        End With

        Return dr

    End Function

    ''' <summary>
    ''' 梱数の合計
    ''' </summary>
    ''' <param name="drs">DataRow配列</param>
    ''' <returns>梱数</returns>
    ''' <remarks></remarks>
    Private Function SetNbData(ByVal drs As DataRow()) As Decimal

        SetNbData = 0

        Dim max As Integer = drs.Length - 1
        For i As Integer = max To 0 Step -1
            SetNbData += Convert.ToDecimal(drs(i).Item("KONSU").ToString())
            drs(i).Delete()
        Next

        Return SetNbData

    End Function

    ''' <summary>
    ''' 梱数の合計(作業用)
    ''' </summary>
    ''' <param name="drs">DataRow配列</param>
    ''' <returns>梱数</returns>
    ''' <remarks></remarks>
    Private Function SetNbDataSagyo(ByVal drs As DataRow(), ByVal drm As DataRow()) As Decimal

        'Notes1807
        Dim SetNbData As Decimal = 0
        Dim setHasu As Decimal = 0
        Dim setPkgNb As Decimal = 0
        Dim inkaM As String = String.Empty
        Dim konsu As Decimal = 0

        Dim max As Integer = drs.Length - 1
        Dim maxM As Integer = drm.Length - 1

        For j As Integer = maxM To 0 Step -1
            setPkgNb = Convert.ToDecimal(drm(j).Item("PKG_NB").ToString()) '入数
            inkaM = drm(j).Item("INKA_NO_M").ToString()
            For i As Integer = max To 0 Step -1
                If inkaM.Equals(drs(i).Item("INKA_NO_M").ToString()) = False Then
                    Continue For
                End If
#If False Then      'UPD 2018/10/17 002305 入荷データ登録_発生した付帯作業(中)の作業料明細の作業数が1個多い
                If (drs(i).Item("KONSU").ToString()) = "0" OrElse _
                    String.IsNullOrEmpty(drs(i).Item("KONSU").ToString) = True Then '0か空白なら1に変更
                    drs(i).Item("KONSU") = "1".ToString
                End If

#Else
                If String.IsNullOrEmpty(drs(i).Item("KONSU").ToString) = True Then  '空白なら1に変更
                    drs(i).Item("KONSU") = "1".ToString
                End If
#End If

                konsu = Convert.ToDecimal(drs(i).Item("KONSU").ToString()) '梱数

                If String.IsNullOrEmpty(drs(i).Item("HASU").ToString) = True Then
                    drs(i).Item("HASU") = "0".ToString
                End If

                setHasu = Convert.ToDecimal(drs(i).Item("HASU").ToString())

                If setPkgNb.ToString <> "0" Then '入数ゼロの割り算は異常終了してしまう。
#If True Then  'UPD 2018/10/12 002305 入荷データ登録_発生した付帯作業(中)の作業料明細の作業数が1個多い
                    SetNbData = SetNbData + (konsu + Math.Ceiling(setHasu / setPkgNb))                   '梱数+(端数/入数)
#Else
                    ''                                              切り捨て追加
                    'SetNbData = SetNbData + (konsu + Math.Ceiling(Math.Floor(setHasu / setPkgNb)))                   '梱数+(端数/入数)
                    '                             
                    If setHasu = 0 Then
                        SetNbData = SetNbData + konsu
                    Else
                        '                                              切り上げ追加
                        'SetNbData = SetNbData + (konsu + Math.Ceiling(Math.Floor(setHasu / setPkgNb)))                   '梱数+(端数/入数)
                        SetNbData = CDec(SetNbData + konsu + (Math.Ceiling((setHasu / setPkgNb) + 0.999999)))                 '梱数+(端数/入数)

                    End If
#End If
                End If
            Next
        Next 'maxM
        Return SetNbData

    End Function

    ''' <summary>
    ''' SAGYOの初期値設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Private Function SetInitSagyoLData(ByVal dr As DataRow) As DataRow

        With dr

            Dim inkaLDr As DataRow = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)

            .Item("SKYU_CHK") = LMB020C.FLG_OFF
            .Item("SAGYO_SIJI_NO") = String.Empty
            .Item("SKYU_MEI_NO") = String.Empty
            .Item("DEST_SAGYO_FLG") = "00"
            .Item("DEST_CD") = String.Empty
            .Item("DEST_NM") = String.Empty
            .Item("LOT_NO") = String.Empty
            .Item("REMARK_ZAI") = String.Empty
            .Item("SYS_DEL_FLG") = 0
            .Item("UP_KBN") = 0

        End With

        Return dr

    End Function

    ''' <summary>
    ''' ステージの設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="inkaStateKb">進捗区分</param>
    ''' <returns>ステージ</returns>
    ''' <remarks></remarks>
    Private Function SetStateData(ByVal frm As LMB020F, ByVal ds As DataSet, ByVal dr As DataRow, ByVal inkaStateKb As String) As String

        If String.IsNullOrEmpty(inkaStateKb) = True Then
            inkaStateKb = LMB020C.STATE_YOTEI
        End If

        Dim newinkaStateKb As String = inkaStateKb
        SetStateData = String.Empty

        With frm

            '印刷フラグを初期化
            dr.Item("PRINT_FLG") = LMConst.FLG.OFF

            '元の入荷進捗区分が予定の場合のみ行う
            If LMB020C.STATE_YOTEI.Equals(newinkaStateKb) = True OrElse String.IsNullOrEmpty(inkaStateKb) = True Then
                Dim inkaMDrs As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_M).Select("SYS_DEL_FLG = '0'")
                Dim inkaSDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S)
                Dim max As Integer = inkaMDrs.Length - 1
                If max < 0 Then
                    Return LMB020C.STATE_YOTEI
                End If
                For i As Integer = 0 To max

                    '各入荷(中)に入荷(小)が存在しない場合
                    If 0 = inkaSDt.Select(Me.SetSqlSelectInkaS(frm, inkaMDrs(i).Item("INKA_NO_M").ToString())).Length Then
                        Return LMB020C.STATE_YOTEI
                    End If

                Next
            End If

            '倉庫の情報を取得
            Dim drs As DataRow() = Me._LMBconV.SelectSokoListDataRow(.cmbSoko.SelectedValue.ToString())

            '取得できない場合、スルー
            If drs.Length < 1 Then
                If String.IsNullOrEmpty(inkaStateKb) = True Then
                    Return LMB020C.STATE_YOTEI
                Else
                    Return newinkaStateKb
                End If
            End If

            '印刷フラグを設定
            dr.Item("PRINT_FLG") = Me.SetPrintFlg(frm, inkaStateKb, ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)("CHECKLIST_PRT_DATE").ToString())

            '20'
            If LMB020C.FLG_OFF.Equals(drs(0).Item("INKA_YOTEI_YN").ToString()) = False Then
                If Convert.ToInt32(inkaStateKb) < Convert.ToInt32(LMB020C.STATE_UKETUKEHYOINSATU) Then
                    newinkaStateKb = LMB020C.STATE_YOTEI
                Else
                    '変わらない又は後退する場合は、変更しない
                    Return newinkaStateKb
                End If
            End If

            '30'
            If LMB020C.FLG_OFF.Equals(drs(0).Item("INKA_UKE_PRT_YN").ToString()) = False Then
                If Convert.ToInt32(inkaStateKb) < Convert.ToInt32(LMB020C.STATE_UKETUKEZUMI) _
                AndAlso (String.IsNullOrEmpty(ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)("CHECKLIST_PRT_DATE").ToString()) = False _
                         OrElse LMConst.FLG.ON.Equals(dr.Item("PRINT_FLG").ToString())) Then
                    newinkaStateKb = LMB020C.STATE_UKETUKEHYOINSATU
                Else
                    '変わらない又は後退する場合は、変更しない
                    Return newinkaStateKb
                End If
            End If

            '40'
            If LMB020C.FLG_OFF.Equals(drs(0).Item("INKA_KENPIN_YN").ToString()) = False Then
                If Convert.ToInt32(inkaStateKb) < Convert.ToInt32(LMB020C.STATE_NYUKAKEPPINZUMI) Then
                    newinkaStateKb = LMB020C.STATE_UKETUKEZUMI
                Else
                    '変わらない又は後退する場合は、変更しない
                    Return newinkaStateKb
                End If
            End If

            'どの分岐にも入らない場合、検品済
            If LMB020C.STATE_YOTEI.Equals(newinkaStateKb) = True Then
                If Convert.ToInt32(inkaStateKb) < Convert.ToInt32(LMB020C.STATE_NYUKAKEPPINZUMI) Then
                    newinkaStateKb = LMB020C.STATE_NYUKAKEPPINZUMI
                Else
                    '変わらない又は後退する場合は、変更しない
                    Return newinkaStateKb
                End If
            End If

        End With

        Return newinkaStateKb

    End Function

    ''' <summary>
    ''' 印刷フラグ
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="motoStage">元進捗</param>
    ''' <returns>印刷フラグ</returns>
    ''' <remarks></remarks>
    Private Function SetPrintFlg(ByVal frm As LMB020F, ByVal motoStage As String, ByVal chkPrtFlg As String) As String

        '新規登録(複写を含む)の場合、印刷
        Select Case frm.lblSituation.RecordStatus

            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC

                Return LMConst.FLG.ON

        End Select

        '更新時は元進捗が'10'(予定)でかつ印刷していない場合、印刷
        If LMB020C.STATE_YOTEI.Equals(motoStage) = True AndAlso String.IsNullOrEmpty(chkPrtFlg) = True Then
            Return LMConst.FLG.ON
        End If

        Return LMConst.FLG.OFF

    End Function

    ''' <summary>
    ''' 作業完了区分の設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoEndData(ByVal dr As DataRow) As String

        Dim inkaLDr As DataRow = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)

        SetSagyoEndData = dr.Item("SAGYO_COMP").ToString()

        '01'以外が設定されている場合
        If LMB020C.SAGYO_KANRYO_ON.Equals(SetSagyoEndData) = False Then

            Select Case inkaLDr.Item("INKA_STATE_KB").ToString()

                Case LMB020C.STATE_NYUKOZUMI, LMB020C.STATE_NYUKOHOUKOKUZUMI

                    SetSagyoEndData = LMB020C.SAGYO_KANRYO_ON

                Case Else

                    SetSagyoEndData = LMB020C.SAGYO_KANRYO_OFF

            End Select

        End If

        Return SetSagyoEndData

    End Function

    ''' <summary>
    ''' 複写処理時のデータセット設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetCopyData(ByVal ds As DataSet) As DataSet

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '入荷(大)レコードの入荷(大)番号を初期化
        Dim inkaLDr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)
        inkaLDr.Item("INKA_NO_L") = String.Empty
        '修正 2015.05.20 営業所またぎ処理のため引数の値をそのまま設定
        'inkaLDr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
        inkaLDr.Item("NRS_BR_CD") = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("NRS_BR_CD")
        inkaLDr.Item("INKA_STATE_KB") = LMB020C.STATE_YOTEI
        inkaLDr.Item("INKA_STATE_KB_NM") = String.Empty
        inkaLDr.Item("INKA_KB") = LMB020C.INKAKBN_NOMAL

        inkaLDr.Item("WH_KENPIN_WK_STATUS") = String.Empty

        inkaLDr.Item("WH_TAB_SAGYO_SIJI_STATUS") = LMB020C.WH_TAB_SIJI_00
        inkaLDr.Item("WH_TAB_SAGYO_STATUS") = String.Empty

        Dim inkaKbn As String = String.Empty
        Dim kbnDrs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_N006, "' AND KBN_CD = '", LMB020C.INKAKBN_NOMAL, "'"))
        If 0 < kbnDrs.Length Then

            '2017/09/25 修正 李↓
            inkaKbn = kbnDrs(0).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString()
            '2017/09/25 修正 李↑

        End If

        inkaLDr.Item("INKA_KB_NM") = inkaKbn
        inkaLDr.Item("HOKAN_STR_DATE") = String.Empty
        inkaLDr.Item("STORAGE_DUE_DATE") = String.Empty     'ADD 2017/10/04 総保入期限

        '入荷(中)レコードの入荷(大)番号を初期化
        Dim inkaMDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)
        Dim max As Integer = inkaMDt.Rows.Count - 1
        For i As Integer = 0 To max

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'inkaMDt.Rows(i).Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            inkaMDt.Rows(i).Item("NRS_BR_CD") = ds.Tables(LMB020C.TABLE_NM_INKA_M)(0).Item("NRS_BR_CD")
            inkaMDt.Rows(i).Item("INKA_NO_L") = String.Empty
            inkaMDt.Rows(i).Item("UP_KBN") = LMConst.FLG.OFF
            inkaMDt.Rows(i).Item("SUM_SURYO_M") = 0
            inkaMDt.Rows(i).Item("SUM_KOSU") = 0
            inkaMDt.Rows(i).Item("SUM_JURYO_M") = 0
            inkaMDt.Rows(i).Item("HIKIATE") = LMB020C.HIKIATE_NASI

        Next

        '入荷(小)レコードの削除
        Dim inkaSDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S)
        inkaSDt.Clear()

        '在庫レコードの削除
        Dim zaiDt As DataTable = ds.Tables(LMB020C.TABLE_NM_ZAI)
        zaiDt.Clear()

        'ADD 2022/11/07 倉庫写真アプリ対応 START
        '入荷写真データレコードの削除
        Dim inkaPDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_PHOTO)
        inkaPDt.Clear()
        'ADD 2022/11/07 倉庫写真アプリ対応 END

        '運送(大)レコードの運送番号(大)をクリア
        Dim unsoLDt As DataTable = ds.Tables(LMB020C.TABLE_NM_UNSO_L)
        If 0 < unsoLDt.Rows.Count Then
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'unsoLDt.Rows(0).Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            unsoLDt.Rows(0).Item("NRS_BR_CD") = ds.Tables(LMB020C.TABLE_NM_UNSO_L)(0).Item("NRS_BR_CD")
            unsoLDt.Rows(0).Item("UNSO_NO_L") = String.Empty
            unsoLDt.Rows(0).Item("UP_KBN") = LMConst.FLG.OFF
            'START UMANO 要望番号1387 支払処理修正。
            unsoLDt.Rows(0).Item("TRIP_NO") = String.Empty
            unsoLDt.Rows(0).Item("TRIP_NO_SYUKA") = String.Empty
            unsoLDt.Rows(0).Item("TRIP_NO_TYUKEI") = String.Empty
            unsoLDt.Rows(0).Item("TRIP_NO_HAIKA") = String.Empty
            'END UMANO 要望番号1387 支払処理修正。

        End If

        '運送(中)レコードの運送番号(中)をクリア
        Dim unsoMDt As DataTable = ds.Tables(LMB020C.TABLE_NM_UNSO_M)
        max = unsoMDt.Rows.Count - 1
        For i As Integer = 0 To max
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'unsoMDt.Rows(i).Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            unsoMDt.Rows(i).Item("NRS_BR_CD") = ds.Tables(LMB020C.TABLE_NM_UNSO_M)(0).Item("NRS_BR_CD")
            unsoMDt.Rows(i).Item("UNSO_NO_L") = String.Empty
            'START YANAI 要望番号1016
            unsoMDt.Rows(i).Item("UP_KBN") = LMConst.FLG.OFF
            'END YANAI 要望番号1016
        Next

        '作業レコードの設定
        Dim sagyoDt As DataTable = ds.Tables(LMB020C.TABLE_NM_SAGYO)
        max = sagyoDt.Rows.Count - 1
        Dim no As String = String.Empty
        Dim compNm As String = String.Empty
        Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_S052, "' AND KBN_CD = '", LMB020C.FLG_OFF, "' "))
        If 0 < drs.Length Then

            '2017/09/25 修正 李↓
            compNm = drs(0).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString()
            '2017/09/25 修正 李↑

        End If
        For i As Integer = 0 To max
            '後ろ3桁を設定
            no = sagyoDt.Rows(i).Item("INOUTKA_NO_LM").ToString()
            sagyoDt.Rows(i).Item("INOUTKA_NO_LM") = no.Substring(no.Length - 3, 3)
            sagyoDt.Rows(i).Item("SAGYO_REC_NO") = String.Empty
            sagyoDt.Rows(i).Item("SAGYO_COMP") = LMB020C.FLG_OFF
            '2017/08/03 Shinoda Add Start 複写で作業請求済みフラグを引き継いでしまう件を修正
            sagyoDt.Rows(i).Item("SKYU_CHK") = LMB020C.FLG_OFF
            '2017/08/03 Shinoda Add End
            sagyoDt.Rows(i).Item("SAGYO_COMP_NM") = compNm
            sagyoDt.Rows(i).Item("UP_KBN") = LMConst.FLG.OFF
        Next

        'MAX_NOの初期化
        Me.SetInkaMmaxSeqCler()

        Return ds

    End Function

    ''' <summary>
    ''' データの上書き処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">設定先テーブル名</param>
    ''' <param name="setDt">設定元DataTable</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DataTableImport(ByVal ds As DataSet, ByVal tblNm As String, ByVal setDt As DataTable) As DataSet

        '現在設定してある値をクリア
        Dim dt As DataTable = ds.Tables(tblNm)
        dt.Clear()

        '全ての情報をImport
        Dim max As Integer = setDt.Rows.Count - 1
        For i As Integer = 0 To max
            dt.ImportRow(setDt.Rows(i))
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 作業レコードの請求差コードを取得
    ''' </summary>
    ''' <param name="inkaMDt">入荷(中)のDataTable</param>
    ''' <param name="inkaLDr">入荷(大)の情報</param>
    ''' <param name="inkaMDrs">入荷(中)の情報</param>
    ''' <param name="type">作業レコードタイプ</param>
    ''' <returns>請求先コード</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoSeiqtoCd(ByVal inkaMDt As DataTable, ByVal inkaLDr As DataRow, ByVal inkaMDrs As DataRow(), ByVal type As LMB020C.SagyoData) As String

        SetSagyoSeiqtoCd = String.Empty

        Dim dr As DataRow = Nothing

        '大レコードの場合、入荷(中)の先頭レコード
        If LMB020C.SagyoData.L = type Then

            Dim selDrs As DataRow() = inkaMDt.Select(String.Empty, "INKA_NO_M")
            If selDrs.Length < 1 Then
                Return SetSagyoSeiqtoCd
            End If
            dr = selDrs(0)

        Else

            '入荷(中)レコードがない場合、スルー
            If inkaMDrs.Length < 1 Then
                Return SetSagyoSeiqtoCd
            End If
            dr = inkaMDrs(0)

        End If

        '中レコードの場合、商品ごとの請求先コードを設定
        Dim drs As DataRow() = Me._LMBconV.SelectCustListDataRow(dr.Item("CUST_CD_L").ToString(), dr.Item("CUST_CD_M").ToString(), dr.Item("CUST_CD_S").ToString(), dr.Item("CUST_CD_SS").ToString())
        If 0 < drs.Length Then
            SetSagyoSeiqtoCd = drs(0).Item("SAGYO_SEIQTO_CD").ToString()
        End If

        Return SetSagyoSeiqtoCd

    End Function

    ''' <summary>
    ''' LMF800DSを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinCalcDataSet(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = New LMF800DS()
        Dim tblNm As String = String.Empty
        Dim max As Integer = setDs.Tables.Count - 1
        Dim cnt As Integer = ds.Tables.Count - 1
        Dim setFlg As Boolean = True
        For i As Integer = 0 To max

            setFlg = True
            tblNm = setDs.Tables(i).TableName

            For j As Integer = 0 To cnt

                '同じ名前のものは追加しない
                If tblNm.Equals(ds.Tables(j).TableName) = True Then
                    setFlg = False
                    Exit For
                End If

            Next

            '違うテーブルの場合
            If setFlg = True Then

                'テーブル追加
                ds = Me.SetBetuDataTable(ds, setDs, tblNm)

            End If

        Next

        Return ds

    End Function

    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
    ''' <summary>
    ''' LMF810DSを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetShiharaiCalcDataSet(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = New LMF810DS()
        Dim tblNm As String = String.Empty
        Dim max As Integer = setDs.Tables.Count - 1
        Dim cnt As Integer = ds.Tables.Count - 1
        Dim setFlg As Boolean = True
        For i As Integer = 0 To max

            setFlg = True
            tblNm = setDs.Tables(i).TableName

            For j As Integer = 0 To cnt

                '同じ名前のものは追加しない
                If tblNm.Equals(ds.Tables(j).TableName) = True Then
                    setFlg = False
                    Exit For
                End If

            Next

            '違うテーブルの場合
            If setFlg = True Then

                'テーブル追加
                ds = Me.SetBetuDataTable(ds, setDs, tblNm)

            End If

        Next

        Return ds

    End Function
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

    ''' <summary>
    ''' DataTable追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="setDs">DataSet</param>
    ''' <param name="tblNm">Table名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetBetuDataTable(ByVal ds As DataSet, ByVal setDs As DataSet, ByVal tblNm As String) As DataSet

        'DataTableのインスタンス生成
        Dim setDt As DataTable = setDs.Tables(tblNm).Copy
        setDt.TableName = tblNm

        'テーブル追加
        ds.Tables.Add(setDt)

        Return ds

    End Function

    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetIkkatsuHenkoData(ByVal frm As LMB020F, ByVal arr As ArrayList) As DataSet

        With frm.sprGoodsDef.ActiveSheet

            Dim injun As String = frm.numHenkoInjun.Value.ToString()
            Dim inkaMDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M)
            Dim max As Integer = arr.Count - 1
            Dim recNo As Integer = 0
            Dim cnt As Integer = 0
            For i As Integer = 0 To max

                recNo = Convert.ToInt32(Me._LMBconV.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMB020G.sprGoodsDef.RECNO.ColNo)))

                '入荷(中)レコードの印順を変更
                inkaMDt.Rows(recNo).Item("PRINT_SORT") = injun

            Next

            Return Me._Ds

        End With

    End Function

    ''' <summary>
    ''' 一括変更処理(置場)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetToSituZoneData(ByVal frm As LMB020F, ByVal arr As ArrayList) As DataSet

        Dim spr As Win.Spread.LMSpread = frm.sprDetail

        With frm.sprDetail.ActiveSheet

            Dim touNo As String = frm.txtTouNo.TextValue.ToString()
            Dim situNo As String = frm.txtSituNo.TextValue.ToString()
            Dim zoneCd As String = frm.txtZoneCd.TextValue.ToString()
            Dim location As String = frm.txtLocation.TextValue.ToString()

            Dim inkaSDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_S)

            Dim max As Integer = arr.Count - 1

            For i As Integer = 0 To max

                Dim inkNoM As String = Me._LMBconV.GetCellValue(frm.sprGoodsDef.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMB020G.sprGoodsDef.KANRI_NO.ColNo)).ToString()

                Dim rowList As DataRow() = inkaSDt.Select("INKA_NO_M =" + inkNoM)

                For Each row As DataRow In rowList
                    row("TOU_NO") = touNo
                    row("SITU_NO") = situNo
                    row("ZONE_CD") = zoneCd
                    row("LOCA") = location
                Next

                '入荷(小)レコードの棟・室・ゾーン・ロケーションを変更
                'spr.SetCellValue(rowNo, LMB020G.sprDetailDef.TOU_NO.ColNo, touNo)
                'spr.SetCellValue(rowNo, LMB020G.sprDetailDef.SHITSU_NO.ColNo, situNo)
                'spr.SetCellValue(rowNo, LMB020G.sprDetailDef.ZONE_CD.ColNo, zoneCd)
                'spr.SetCellValue(rowNo, LMB020G.sprDetailDef.LOCA.ColNo, location)

            Next

            Return Me._Ds

        End With

    End Function

#End Region

#Region "サーバアクセス"

    ''' <summary>
    ''' サーバアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <param name="readDBFLG">1 リード指定</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ServerAccess(ByVal ds As DataSet, ByVal actionStr As String, Optional ByVal readDBFLG As String = "") As DataSet
        If readDBFLG.Equals("1") = True Then
            'DBリードオンリー設定 ADD 2021/11/01
            Return MyBase.CallWSA("LMB020BLF", actionStr, ds, True)
        Else
            Return MyBase.CallWSA("LMB020BLF", actionStr, ds)
        End If
    End Function

#End Region

#Region "行追加"

    ''' <summary>
    ''' 入荷(中)データ追加処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rtnDs">戻りDataSet</param>
    ''' <remarks></remarks>
    Private Function AddInkaMData(ByVal frm As LMB020F, ByVal rtnDs As DataSet) As Boolean

        Dim dt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M)
        Dim rtnDt As DataTable = rtnDs.Tables(LMZ020C.TABLE_NM_OUT)
        Dim max As Integer = rtnDt.Rows.Count - 1
        Dim maxDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_MAX_NO)


        'SEQの限界値、チェック
        '"入荷管理番号M"の部分を画面の項目から取ってくると()がついており余計なため、暫定的にこのままとする　20151104 tsunehira
        If Me._LMBconV.IsMaxSeqChk(maxDt.Rows(maxDt.Rows.Count - 1), "INKA_NO_M", max + 1, frm.lblTitleKanriNoM.TextValue) = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Return False
        End If

        Dim rowCnt As Integer = 0
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim inkaLNo As String = frm.lblKanriNoL.TextValue
        Dim maxSeq As String = String.Empty
        Dim nbUt As String = String.Empty
        Dim rtnDr As DataRow = Nothing

        For i As Integer = 0 To max

            '行番号の設定
            rowCnt = dt.Rows.Count

            '空行追加
            dt.Rows.Add(dt.NewRow())

            With dt.Rows(rowCnt)

                maxSeq = Me.SetInkaMmaxSeq()
                rtnDr = rtnDt.Rows(i)

                .Item("NRS_BR_CD") = brCd
                .Item("INKA_NO_L") = inkaLNo
                .Item("INKA_NO_M") = maxSeq
                .Item("GOODS_CD_NRS") = rtnDr.Item("GOODS_CD_NRS").ToString()
                .Item("GOODS_CD_CUST") = rtnDr.Item("GOODS_CD_CUST").ToString()
                .Item("OUTKA_FROM_ORD_NO_M") = String.Empty
                .Item("BUYER_ORD_NO_M") = String.Empty
                .Item("REMARK") = String.Empty
                .Item("PRINT_SORT") = 99
                .Item("GOODS_NM") = rtnDr.Item("GOODS_NM_1").ToString()
                .Item("ONDO_KB") = rtnDr.Item("ONDO_KB").ToString()
                .Item("SUM_KOSU") = 0 'sの個数合計　rowcount*kosu
                nbUt = rtnDr.Item("NB_UT").ToString()
                .Item("NB_UT") = nbUt
                .Item("ONDO_STR_DATE") = rtnDr.Item("ONDO_STR_DATE").ToString()
                .Item("ONDO_END_DATE") = rtnDr.Item("ONDO_END_DATE").ToString()
                .Item("PKG_NB") = rtnDr.Item("PKG_NB").ToString()
                .Item("PKG_NB_UT1") = nbUt
                .Item("PKG_NB_UT2") = rtnDr.Item("PKG_UT").ToString()
                .Item("STD_IRIME_NB_M") = rtnDr.Item("STD_IRIME_NB").ToString()
                .Item("STD_IRIME_UT") = rtnDr.Item("STD_IRIME_UT").ToString()
                .Item("SUM_SURYO_M") = 0 'kosu*入り目(rowの数だけまわす)
                .Item("SUM_JURYO_M") = 0 'Sの重量をsum(rowの数だけまわす)
                .Item("SHOBO_CD") = String.Empty
                .Item("HIKIATE") = LMB020C.HIKIATE_NASI
                .Item("SAGYO_UMU") = String.Empty
                .Item("SYS_DEL_FLG") = 0
                .Item("EDI_KOSU") = 0
                .Item("EDI_SURYO") = 0
                .Item("CUST_CD_L") = rtnDr.Item("CUST_CD_L").ToString()
                .Item("CUST_CD_M") = rtnDr.Item("CUST_CD_M").ToString()
                .Item("CUST_CD_S") = rtnDr.Item("CUST_CD_S").ToString()
                .Item("CUST_CD_SS") = rtnDr.Item("CUST_CD_SS").ToString()
                .Item("STD_WT_KGS") = rtnDr.Item("STD_WT_KGS").ToString()
                .Item("LOT_CTL_KB") = rtnDr.Item("LOT_CTL_KB").ToString()
                .Item("LT_DATE_CTL_KB") = rtnDr.Item("LT_DATE_CTL_KB").ToString()
                .Item("CRT_DATE_CTL_KB") = rtnDr.Item("CRT_DATE_CTL_KB").ToString()
                'START YANAI メモ②No.20
                .Item("EDI_FLG") = String.Empty
                'END YANAI メモ②No.20
                .Item("UP_KBN") = 0
                '(2012.12.27)要望番号1692関連 項目追加 -- START --
                .Item("TARE_YN") = rtnDr.Item("TARE_YN").ToString()
                '(2012.12.27)要望番号1692関連 項目追加 --  END  --
            End With

        Next

        '入荷(小)の情報をクリア
        frm.sprDetail.CrearSpread()

        '入荷(中)情報表示
        Call Me._G.SetInkaMData(Me._Ds)

        '追加したレコードの詳細情報表示
        Call Me._G.ClearInkaMControl()
        Call Me._G.SetInkaMInforData(Me._Ds, dt.Rows.Count - 1)

        '作業情報を設定(1行しかありえないrtnDrため固定で設定)
        Call Me.SetRtnGoodsDataAtSagyo(frm, rtnDr)

        Return True

    End Function

    ''' <summary>
    ''' 入荷(中)データ追加前処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="flgMdata">行追加(中)判定フラグ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function AddInkaMDataAction(ByVal frm As LMB020F, ByVal flgMdata As String, ByVal flgMMerge As String) As Boolean

        '計算処理(データ反映のため)
        Dim rtnResult As Boolean = Me.AllCalculation(frm)

        '入力(中)のデータを設定
        rtnResult = rtnResult AndAlso Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.INIT_M)

        '--------要望番号1904 （中）（小）の個数違いによるワーニングを出す
        Dim chkResult As Boolean = Me.SetKosuChkMessage(frm)

        If chkResult = False Then
            Exit Function
        End If

        '--------要望番号1904 （中）（小）の個数違いによるワーニングを出す

        '商品Pop表示


        Dim prm As LMFormData = Nothing
        If rtnResult = True Then

            If flgMdata = "0" Then '("1":CSV取り込み　0:入荷M明細　2:入荷検品WKより取込)

                'START YANAI 要望番号481
                'prm = Me.ShowGoodsPopup(frm)
                prm = Me.ShowGoodsPopup(frm, LMB020C.ActionType.INIT_M)
                'END YANAI 要望番号481
                rtnResult = rtnResult AndAlso prm.ReturnFlg
            ElseIf flgMdata = "2" Then
                prm = Me.ShowGoodsKenpinPopup(frm)
                rtnResult = rtnResult AndAlso prm.ReturnFlg

                If rtnResult = False Then
                    Return rtnResult
                Else
                    rtnResult = Me.TorikomiKenpinData(frm, prm.ParamDataSet, flgMMerge)
                    Return rtnResult

                End If
            Else
                Return rtnResult '("1":CSV取り込み　)
            End If
        End If

        Return rtnResult AndAlso Me.AddInkaMData(frm, prm.ParamDataSet)

    End Function

    'START YANAI 要望番号557
    '''' <summary>
    '''' 入荷(小)データ追加処理
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <param name="inkaMNo">入荷中番</param>
    '''' <remarks></remarks>
    'Private Function AddInkaSData(ByVal frm As LMB020F, ByVal inkaMNo As String, Optional ByVal rowNo As Integer = -1) As Boolean
    ''' <summary>
    ''' 入荷(小)データ追加処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="inkaMNo">入荷中番</param>
    ''' <remarks></remarks>
    Private Function AddInkaSData(ByVal frm As LMB020F, _
                                  ByVal inkaMNo As String, _
                                  Optional ByVal rowNo As Integer = -1, _
                                  Optional ByVal copyCnt As Integer = -1, _
                                  Optional ByVal copyMax As Integer = -1) As Boolean
        'END YANAI 要望番号557

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim dt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_S)
        Dim maxDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_MAX_NO)
        'START YANAI 要望番号557
        Dim rtnResult As Boolean = False
        Dim rowIndex As Integer = 0
        'END YANAI 要望番号557
        'csv対応
        Dim csvDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_CSV_DATA)
        Dim msg As String = String.Empty

        '2017/09/25 修正 李↓
        msg = lgm.Selector({"入荷管理番号", "Stock control number", "입고관리번호", "中国語"})
        '2017/09/25 修正 李↑

        'SEQの限界値、チェック
        If Me._LMBconV.IsMaxSeqChk(maxDt.Select(Me._G.SetInkaSSql(inkaMNo))(0), "MAX_INKA_NO_S", 1, msg) = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            'START YANAI 要望番号557
            If copyMax <> -1 Then
                '入荷(小)情報表示
                Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.INIT_S, inkaMNo)

                '合計個数・数量・重量の計算処理
                If Me.SetSumData(frm, "sprDetail") = False Then
                    Logger.EndLog(MyBase.GetType.Name, "AddInkaSData")
                    Return False
                End If

                '入力(中)のデータを設定
                rtnResult = Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.INIT_S)

                If rtnResult = True Then
                    '入荷(中)情報表示
                    Call Me._G.SetInkaMData(Me._Ds)

                    '入荷(中)の詳細情報表示
                    rowIndex = Me.GetInkaNoM(frm, inkaMNo)
                    Call Me.SetInkaMInforData(frm, rowIndex)

                End If

            End If
            'END YANAI 要望番号557
            Return False
        End If

        '入力(中)のデータを設定

        'START YANAI 要望番号557
        'If Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.INIT_S) = False Then
        '    '処理終了アクション
        '    Call Me.EndAction(frm)
        '    Return False
        'End If
        If copyCnt = -1 OrElse copyCnt = 0 Then
            If Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.INIT_S) = False Then
                '処理終了アクション
                Call Me.EndAction(frm)
                Return False
            End If
        End If
        'END YANAI 要望番号557

        Dim max As Integer = dt.Columns.Count - 1
        Dim dr As DataRow = dt.NewRow()
        For i As Integer = 0 To max
            dr.Item(i) = String.Empty
        Next
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
        dr.Item("NRS_BR_CD") = brCd
        dr.Item("INKA_NO_L") = frm.lblKanriNoL.TextValue
        dr.Item("INKA_NO_M") = inkaMNo
        dr.Item("INKA_NO_S") = Me.SetInkaSmaxSeq(inkaMNo)
        Dim sql As String = String.Concat("NRS_BR_CD = '", brCd, "' " _
                                          , " AND INKA_NO_L = '", frm.lblKanriNoL.TextValue, "' " _
                                          , " AND INKA_NO_M = '", inkaMNo, "' " _
                                          )
        Dim inkaMDr As DataRow = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(sql)(0)
        dr.Item("IRIME") = inkaMDr.Item("STD_IRIME_NB_M").ToString()

        Dim irimeTani As String = inkaMDr.Item("STD_IRIME_UT").ToString()
        Dim juryo As String = Me._LMBconG.FormatNumValue(inkaMDr.Item("STD_WT_KGS").ToString())

        Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_I001, "' AND KBN_CD = '", irimeTani, "'"))
        Dim kbnNm As String = String.Empty
        If 0 < drs.Length Then

            '2017/09/25 修正 李↓
            kbnNm = drs(0).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString()
            '2017/09/25 修正 李↑

        End If
#If True Then   'ADD 2020/05/22 007999 
        Dim custCd As String = frm.txtCustCdL.TextValue
        Dim JJ_FLG As String = LMConst.FLG.OFF
        Dim sGOODS_COND_KB_3 As String = String.Empty
        Dim drjj As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C038' AND KBN_NM1 = '", brCd, "' And KBN_NM2 = '", custCd, "'"))
        If 0 < drjj.Length Then
            JJ_FLG = LMConst.FLG.ON
            sGOODS_COND_KB_3 = drjj(0).Item("KBN_NM3").ToString.Trim
        End If
#End If
        dr.Item("STD_IRIME_NM") = kbnNm
        dr.Item("IRIME") = frm.numStdIrime.Value.ToString()
        dr.Item("JURYO_S") = "0"
        dr.Item("ALLOC_PRIORITY") = LMB020C.WARIATE_FREE
        dr.Item("OFB_KB") = "01"
        dr.Item("SPD_KB") = "01"
#If True Then   'ADD 2020/05/22 007999 
        If JJ_FLG.Equals(LMConst.FLG.ON) Then
            dr.Item("GOODS_COND_KB_3") = sGOODS_COND_KB_3   '状態 荷主　ジョンソンエンドジョンソン専用設定
        End If
#End If
        dr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
        dr.Item("UP_KBN") = LMConst.FLG.OFF
        dr.Item("LOT_CTL_KB") = inkaMDr.Item("LOT_CTL_KB").ToString()
        dr.Item("LT_DATE_CTL_KB") = inkaMDr.Item("LT_DATE_CTL_KB").ToString()
        dr.Item("CRT_DATE_CTL_KB") = inkaMDr.Item("CRT_DATE_CTL_KB").ToString()
        dr.Item("STD_WT_KGS") = inkaMDr.Item("STD_WT_KGS").ToString()

        ''2013.07.16 追加START①
        'dr.Item("KENPIN_NO") = inkaMDr.Item("KENPIN_NO").ToString()
        'dr.Item("GOODS_CD_NRS") = inkaMDr.Item("GOODS_CD_NRS").ToString()
        'dr.Item("GOODS_CD_CUST") = inkaMDr.Item("GOODS_CD_CUST").ToString()
        ''2013.07.16 追加END①

        'START YANAI 要望番号427
        Dim inkaNoS As String = String.Empty
        'END YANAI 要望番号427
#If True Then   'ADD 2020/08/06 014005   【LMS】商品マスタ_入荷仮置場機能の追加
        '荷主詳細　入荷商品詳細置場情報設定有か確認
        Dim CUST_DETAILS_A1FLG As String = LMConst.FLG.OFF

        Dim sSql As String = "NRS_BR_CD = '" & brCd.ToString & "' AND CUST_CD = '" & frm.txtCustCdL.TextValue.ToString() & frm.txtCustCdM.TextValue.ToString() & "' " _
                                 & " AND SUB_KB = 'A1' AND SET_NAIYO = '1'"
        Dim drCD As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(sSql)
        If drCD.Length > 0 Then
            CUST_DETAILS_A1FLG = LMConst.FLG.ON
        End If
        Dim TOU_NO As String = String.Empty
        Dim SITU_NO As String = String.Empty
        Dim ZONE_CD As String = String.Empty
        Dim LOCA As String = String.Empty
        If CUST_DETAILS_A1FLG.Equals(LMConst.FLG.ON) Then
            '商品詳細より置場情報取得
            '入荷(中)の詳細情報表示
            rowIndex = Me.GetInkaNoM(frm, inkaMNo)

            Me._Ds.Tables(LMB020C.TABLE_NM_GOODS_DETAILS_GET).Clear()

            Dim dtGet As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_GOODS_DETAILS_GET)

            Dim drGet As DataRow = dtGet.NewRow()

            drGet.Item("NRS_BR_CD") = brCd
            drGet.Item("GOODS_CD_NRS") = Me.GetInkaMInforData(Me._Ds, rowIndex)
            drGet.Item("SUB_KB") = "02".ToString
            '行追加()
            dtGet.Rows.Add(drGet)

            Dim rtnDs As DataSet = Nothing
            rtnDs = Me.ServerAccess(Me._Ds, "SelectDataGoodsMeisaiOkiba")

            If rtnDs.Tables("LMB020_GOODS_DETAILS_SET").Rows.Count > 0 Then
                Dim SET_NAIYO As String = rtnDs.Tables("LMB020_GOODS_DETAILS_SET").Rows(0).Item("SET_NAIYO").ToString
                SET_NAIYO = String.Concat(SET_NAIYO, Space(17))

                TOU_NO = SET_NAIYO.Substring(0, 2)
                SITU_NO = SET_NAIYO.Substring(2, 2)
                ZONE_CD = SET_NAIYO.Substring(4, 2)
                LOCA = SET_NAIYO.Substring(6, 10)

                TOU_NO = RTrim(TOU_NO)
                SITU_NO = RTrim(SITU_NO)
                ZONE_CD = RTrim(ZONE_CD)
                LOCA = RTrim(LOCA)

                dr.Item("TOU_NO") = TOU_NO.ToString
                dr.Item("SITU_NO") = SITU_NO.ToString
                dr.Item("ZONE_CD") = ZONE_CD.ToString
                dr.Item("LOCA") = LOCA.ToString
            End If
        End If
#End If
        '行複写時
        If rowNo <> -1 Then
            'START YANAI 要望番号493
            'Dim drCopy As DataRow = dt.Rows(rowNo)
            sql = String.Concat("NRS_BR_CD = '", brCd, "' " _
                              , " AND INKA_NO_L = '", frm.lblKanriNoL.TextValue, "' " _
                              , " AND INKA_NO_M = '", inkaMNo, "' " _
                              , " AND INKA_NO_S = '", Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.KANRI_NO_S.ColNo)), "'" _
                                              )
            Dim drCopy As DataRow = dt.Select(sql)(0)
            'END YANAI 要望番号493
            '行コピー
            For i As Integer = 0 To max
                If String.IsNullOrEmpty(dr.Item(i).ToString) = True Then
                    dr.Item(i) = drCopy.Item(i)
                End If
            Next
            dr.Item("JURYO_S") = drCopy.Item("JURYO_S")
            dr.Item("IRIME") = drCopy.Item("IRIME")
            dr.Item("ALLOC_PRIORITY") = drCopy.Item("ALLOC_PRIORITY")
            dr.Item("GOODS_COND_KB_1") = drCopy.Item("GOODS_COND_KB_1")
            dr.Item("GOODS_COND_KB_2") = drCopy.Item("GOODS_COND_KB_2")
            dr.Item("GOODS_COND_KB_3") = drCopy.Item("GOODS_COND_KB_3")
            dr.Item("OFB_KB") = drCopy.Item("OFB_KB")
            dr.Item("SPD_KB") = drCopy.Item("SPD_KB")
            dr.Item("LOT_CTL_KB") = drCopy.Item("LOT_CTL_KB").ToString()
            dr.Item("LT_DATE_CTL_KB") = drCopy.Item("LT_DATE_CTL_KB").ToString()
            dr.Item("CRT_DATE_CTL_KB") = drCopy.Item("CRT_DATE_CTL_KB").ToString()

            'ADD 2022/11/07 倉庫写真アプリ対応 START
            dr.Item("PHOTO_YN") = String.Empty
            'ADD 2022/11/07 倉庫写真アプリ対応 END

            ''2013.07.16 追加START①
            'dr.Item("GOODS_CD_NRS") = drCopy.Item("GOODS_CD_NRS").ToString()
            'dr.Item("GOODS_CD_CUST") = drCopy.Item("GOODS_CD_CUST").ToString()
            ''2013.07.16 追加END①

            'START YANAI 要望番号427
            inkaNoS = drCopy.Item("INKA_NO_S").ToString()
            'END YANAI 要望番号427

        End If

        ''行追加()
        dt.Rows.Add(dr)

        'START YANAI 要望番号557
        ''入荷(小)情報表示
        If csvDt.Rows.Count <> 0 Then
            Call Me._G.SetInkaSData_CSV(Me._Ds, LMB020C.ActionType.INIT_S, inkaMNo)
        Else
            Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.EDIT, inkaMNo)
        End If
        ''START YANAI 要望番号427
        If rowNo <> -1 Then
            Call Me.SetDefChk(frm, inkaNoS)
        End If
        ''END YANAI 要望番号427

        ''ロット№にフォーカス
        frm.sprDetail.Focus()
        frm.sprDetail.ActiveSheet.SetActiveCell(max, LMB020C.SprInkaSColumnIndex.LOT_NO)

        If String.IsNullOrEmpty(csvDt.Rows.ToString) = True Then '(csvが空白なら下へ。)

            If (copyCnt).Equals(copyMax) = True Then
                '複写時は、最終複写の時のみ行う

                '入荷(小)情報表示
                Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.INIT_S, inkaMNo)

                If copyMax <> -1 Then

                    '合計個数・数量・重量の計算処理
                    If Me.SetSumData(frm, "sprDetail") = False Then
                        Logger.EndLog(MyBase.GetType.Name, "AddInkaSData")
                        Return False
                    End If

                    '入力(中)のデータを設定
                    rtnResult = Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.INIT_S)

                    If rtnResult = True Then
                        '入荷(中)情報表示
                        Call Me._G.SetInkaMData(Me._Ds)

                        '入荷(中)の詳細情報表示
                        rowIndex = Me.GetInkaNoM(frm, inkaMNo)
                        Call Me.SetInkaMInforData(frm, rowIndex)

                    End If

                End If

                'START YANAI 要望番号427
                If rowNo <> -1 Then
                    Call Me.SetDefChk(frm, inkaNoS)
                End If
                'END YANAI 要望番号427

                'ロット№にフォーカス
                frm.sprDetail.Focus()
                frm.sprDetail.ActiveSheet.SetActiveCell(max, LMB020C.SprInkaSColumnIndex.LOT_NO)
            End If
            'END YANAI 要望番号557
        End If
        Return True

    End Function

#If True Then    'ADD 2020/06/06 

    ''' <summary>
    ''' 入荷(中)の詳細情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="recNo">データテーブルの行番号</param>
    ''' <param name="inkaMNo">入荷中番号</param>
    ''' <remarks></remarks>
    Friend Function GetInkaMInforData(ByVal ds As DataSet, ByVal recNo As Integer, Optional ByVal inkaMNo As String = "") As String

        '入荷(中)の情報を設定
        Dim inkaMDr As DataRow = Nothing
            If String.IsNullOrEmpty(inkaMNo) = True Then
                inkaMDr = ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows(recNo)
            Else
                inkaMDr = ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(String.Concat("INKA_NO_M = '", inkaMNo, "' "))(0)
            End If

        Return inkaMDr.Item("GOODS_CD_NRS").ToString()
    End Function
#End If

#End Region

    '2013.07.18 追加START
#Region "行追加(入荷検品WK用)"

    ''' <summary>
    ''' 入荷(中)データ追加処理(入荷検品WK用)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rtnDs">戻りDataSet</param>
    ''' <remarks></remarks>
    Private Function AddInkaMDataKpWk(ByVal frm As LMB020F, ByVal rtnDs As DataSet, ByVal flgMMerge As String) As Boolean

        Dim dt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M)
        Dim rtnDt As DataTable = rtnDs.Tables(LMZ020C.TABLE_NM_OUT)
        Dim max As Integer = rtnDt.Rows.Count - 1
        Dim maxDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_MAX_NO)

        'SEQの限界値、チェック
        If Me._LMBconV.IsMaxSeqChk(maxDt.Rows(maxDt.Rows.Count - 1), "INKA_NO_M", max + 1, frm.lblTitleKanriNoM.TextValue) = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Return False
        End If

        '2014.07.29 Ri [レコードがないときにアベンドするので行わないようにする] Add -ST-
        If rtnDt.Rows.Count < 1 Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Return False
        End If
        Dim kpWkRows() As DataRow = Nothing
        '2014.07.29 Ri [レコードがないときにアベンドするので行わないようにする] Add -ED-

        Dim rowCnt As Integer = 0
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim inkaLNo As String = frm.lblKanriNoL.TextValue
        Dim maxSeq As String = String.Empty
        Dim nbUt As String = String.Empty
        Dim rtnDr As DataRow = Nothing

        Dim preGoodsCdNrs As String = String.Empty
        'Dim preGoodsCdCust As String = String.Empty

        For i As Integer = 0 To max

            '行番号の設定
            rowCnt = dt.Rows.Count

            '空行追加
            dt.Rows.Add(dt.NewRow())

            With dt.Rows(rowCnt)

                maxSeq = Me.SetInkaMmaxSeq()
                rtnDr = rtnDt.Rows(i)

                '2014.07.28 Ri [ｱｸﾞﾘﾏｰﾄ対応] -ST-
                Select Case flgMMerge
                    Case "1"
                        kpWkRows = Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_WK_DATA).Select("GOODS_CD_NRS = '" + rtnDr.Item("GOODS_CD_NRS").ToString() + "' AND INKA_NO_M <> ''")

                        If (Not kpWkRows Is Nothing) AndAlso kpWkRows.Count > 0 Then
                            '同一商品キーの空行削除
                            dt.Rows.Remove(dt.Rows(rowCnt))
                            Continue For
                        End If

                    Case Else
                        '処理なし
                End Select
                '2014.07.28 Ri [ｱｸﾞﾘﾏｰﾄ対応] -ED-

                '入荷検品選択より選択した単位で同一商品キーの場合は入荷(中)を追加しない(1行のみ)
                If i = 0 OrElse preGoodsCdNrs.Equals(rtnDr.Item("GOODS_CD_NRS").ToString()) = False Then
                    'If i = 0 OrElse preGoodsCdCust.Equals(rtnDr.Item("GOODS_CD_CUST").ToString()) = False Then

                    .Item("NRS_BR_CD") = brCd
                    .Item("INKA_NO_L") = inkaLNo
                    .Item("INKA_NO_M") = maxSeq
                    .Item("GOODS_CD_NRS") = rtnDr.Item("GOODS_CD_NRS").ToString()

                    preGoodsCdNrs = rtnDr.Item("GOODS_CD_NRS").ToString()
                    'preGoodsCdCust = rtnDr.Item("GOODS_CD_CUST").ToString()

                    .Item("GOODS_CD_CUST") = rtnDr.Item("GOODS_CD_CUST").ToString()
                    .Item("OUTKA_FROM_ORD_NO_M") = String.Empty
                    .Item("BUYER_ORD_NO_M") = String.Empty
                    .Item("REMARK") = String.Empty
                    .Item("PRINT_SORT") = 99
                    .Item("GOODS_NM") = rtnDr.Item("GOODS_NM_1").ToString()
                    .Item("ONDO_KB") = rtnDr.Item("ONDO_KB").ToString()
                    .Item("SUM_KOSU") = 0 'sの個数合計　rowcount*kosu
                    nbUt = rtnDr.Item("NB_UT").ToString()
                    .Item("NB_UT") = nbUt
                    .Item("ONDO_STR_DATE") = rtnDr.Item("ONDO_STR_DATE").ToString()
                    .Item("ONDO_END_DATE") = rtnDr.Item("ONDO_END_DATE").ToString()
                    .Item("PKG_NB") = rtnDr.Item("PKG_NB").ToString()
                    .Item("PKG_NB_UT1") = nbUt
                    .Item("PKG_NB_UT2") = rtnDr.Item("PKG_UT").ToString()
                    .Item("STD_IRIME_NB_M") = rtnDr.Item("STD_IRIME_NB").ToString()
                    .Item("STD_IRIME_UT") = rtnDr.Item("STD_IRIME_UT").ToString()
                    .Item("SUM_SURYO_M") = 0 'kosu*入り目(rowの数だけまわす)
                    .Item("SUM_JURYO_M") = 0 'Sの重量をsum(rowの数だけまわす)
                    .Item("SHOBO_CD") = String.Empty
                    .Item("HIKIATE") = LMB020C.HIKIATE_NASI
                    .Item("SAGYO_UMU") = String.Empty
                    .Item("SYS_DEL_FLG") = 0
                    .Item("EDI_KOSU") = 0
                    .Item("EDI_SURYO") = 0
                    .Item("CUST_CD_L") = rtnDr.Item("CUST_CD_L").ToString()
                    .Item("CUST_CD_M") = rtnDr.Item("CUST_CD_M").ToString()
                    .Item("CUST_CD_S") = rtnDr.Item("CUST_CD_S").ToString()
                    .Item("CUST_CD_SS") = rtnDr.Item("CUST_CD_SS").ToString()
                    .Item("STD_WT_KGS") = rtnDr.Item("STD_WT_KGS").ToString()
                    .Item("LOT_CTL_KB") = rtnDr.Item("LOT_CTL_KB").ToString()
                    .Item("LT_DATE_CTL_KB") = rtnDr.Item("LT_DATE_CTL_KB").ToString()
                    .Item("CRT_DATE_CTL_KB") = rtnDr.Item("CRT_DATE_CTL_KB").ToString()
                    'START YANAI メモ②No.20
                    .Item("EDI_FLG") = String.Empty
                    'END YANAI メモ②No.20
                    .Item("UP_KBN") = 0
                    '(2012.12.27)要望番号1692関連 項目追加 -- START --
                    .Item("TARE_YN") = rtnDr.Item("TARE_YN").ToString()
                    '(2012.12.27)要望番号1692関連 項目追加 --  END  --
                Else
                    '同一商品キーの空行削除
                    dt.Rows.Remove(dt.Rows(rowCnt))

                End If

            End With

        Next

        '入荷(小)の情報をクリア
        frm.sprDetail.CrearSpread()

        '入荷(中)情報表示
        Call Me._G.SetInkaMData(Me._Ds)

        '追加したレコードの詳細情報表示
        Call Me._G.ClearInkaMControl()
        Call Me._G.SetInkaMInforData(Me._Ds, dt.Rows.Count - 1)

        '作業情報を設定(1行しかありえないrtnDrため固定で設定)
        Call Me.SetRtnGoodsDataAtSagyo(frm, rtnDr)

        Return True

    End Function

#End Region
    '2013.07.18 追加END

#Region "行追加(検品取込)"

    '追加開始 2015.01.16 韓国CALT対応
    ''' <summary>
    ''' 入荷(中)データ追加前処理(検品取込)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="flgMdata">行追加(中)判定フラグ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function AddKenpinInkaMDataAction(ByVal frm As LMB020F, ByVal flgMdata As String, ByVal flgMMerge As String) As Boolean

        '計算処理(データ反映のため)
        Dim rtnResult As Boolean = Me.AllCalculation(frm)

        '入力(中)のデータを設定
        rtnResult = rtnResult AndAlso Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.INIT_M)

        '商品Pop表示
        Dim prm As LMFormData = Nothing
        If rtnResult = True Then
            prm = Me.ShowKenpinGoodsKenpinPopup(frm)
            rtnResult = rtnResult AndAlso prm.ReturnFlg
        End If

        '下記の用なInkaMを追加するメソッドを追加予定
        Return rtnResult AndAlso Me.AddKenpinInkaMData(frm, prm.ParamDataSet)

    End Function

    ''' <summary>
    ''' 入荷(中)データ追加処理(検品取込)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rtnDs">戻りDataSet</param>
    ''' <remarks></remarks>
    Private Function AddKenpinInkaMData(ByVal frm As LMB020F, ByVal rtnDs As DataSet) As Boolean

        Dim dt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M)
        'Dim rtnDt As DataTable = rtnDs.Tables(LMZ020C.TABLE_NM_OUT)
        Dim rtnDt As DataTable = rtnDs.Tables(LMB050C.TABLE_NM_OUT)
        Dim max As Integer = rtnDt.Rows.Count - 1
        Dim maxDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_MAX_NO)

        'SEQの限界値、チェック
        If Me._LMBconV.IsMaxSeqChk(maxDt.Rows(maxDt.Rows.Count - 1), "INKA_NO_M", max + 1, frm.lblTitleKanriNoM.TextValue) = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Return False
        End If

        Dim rowCnt As Integer = 0
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim inkaLNo As String = frm.lblKanriNoL.TextValue
        Dim maxSeq As String = String.Empty
        Dim nbUt As String = String.Empty
        Dim rtnDr As DataRow = Nothing

        Dim cstcdL As String = frm.txtCustCdL.ToString()
        Dim cstcdM As String = frm.txtCustCdM.ToString()
        Dim cstcdS As String = String.Empty
        Dim cstcdSS As String = String.Empty

        For i As Integer = 0 To max

            '行番号の設定
            rowCnt = dt.Rows.Count

            '空行追加
            dt.Rows.Add(dt.NewRow())

            With dt.Rows(rowCnt)

                maxSeq = Me.SetInkaMmaxSeq()
                rtnDr = rtnDt.Rows(i)

                .Item("NRS_BR_CD") = brCd
                .Item("INKA_NO_L") = inkaLNo
                .Item("INKA_NO_M") = maxSeq
                .Item("GOODS_CD_NRS") = rtnDr.Item("GOODS_CD_NRS").ToString()
                .Item("GOODS_CD_CUST") = rtnDr.Item("GOODS_CD_CUST").ToString()
                .Item("OUTKA_FROM_ORD_NO_M") = String.Empty
                .Item("BUYER_ORD_NO_M") = String.Empty
                .Item("REMARK") = rtnDr.Item("REMARK_L").ToString()
                .Item("PRINT_SORT") = 99
                .Item("GOODS_NM") = rtnDr.Item("GOODS_NM").ToString()
                .Item("ONDO_KB") = rtnDr.Item("ONDO_KB").ToString()
                .Item("SUM_KOSU") = rtnDr.Item("JISSEKI_INKA_NB").ToString()
                nbUt = rtnDr.Item("NB_UT").ToString()
                .Item("NB_UT") = nbUt
                .Item("ONDO_STR_DATE") = rtnDr.Item("ONDO_STR_DATE").ToString()
                .Item("ONDO_END_DATE") = rtnDr.Item("ONDO_END_DATE").ToString()
                .Item("PKG_NB") = rtnDr.Item("PKG_NB").ToString()
                .Item("PKG_NB_UT1") = nbUt
                .Item("PKG_NB_UT2") = rtnDr.Item("PKG_UT").ToString()
                .Item("STD_IRIME_NB_M") = rtnDr.Item("STD_IRIME_NB").ToString()
                .Item("STD_IRIME_UT") = rtnDr.Item("STD_IRIME_UT").ToString()
                .Item("SUM_SURYO_M") = rtnDr.Item("JISSEKI_INKA_QT").ToString()
                .Item("SUM_JURYO_M") = CType(rtnDr.Item("JISSEKI_INKA_NB"), Integer) * CType(rtnDr.Item("STD_WT_KGS"), Integer)
                '.Item("NT_GR_CONV_RATE") = String.Empty
                .Item("SHOBO_CD") = String.Empty
                .Item("HIKIATE") = LMB020C.HIKIATE_NASI
                .Item("SAGYO_UMU") = String.Empty
                .Item("SYS_DEL_FLG") = 0
                .Item("EDI_KOSU") = 0
                .Item("EDI_SURYO") = 0
                .Item("STD_IRIME_NB") = String.Empty
                .Item("STD_WT_KGS") = String.Empty
                .Item("CUST_CD_L") = cstcdL
                .Item("CUST_CD_M") = cstcdM
                .Item("CUST_CD_S") = cstcdS
                .Item("CUST_CD_SS") = cstcdSS
                .Item("TARE_YN") = String.Empty
                .Item("LOT_CTL_KB") = String.Empty
                .Item("LT_DATE_CTL_KB") = String.Empty
                .Item("CRT_DATE_CTL_KB") = String.Empty
                .Item("EDI_FLG") = String.Empty
                .Item("UP_KBN") = 0
                .Item("JISSEKI_FLAG") = String.Empty

            End With

            '入荷(小)の作成
            Me.AddKenpinInkaSData(frm, maxSeq, dt.Rows(rowCnt), rtnDr)

        Next

        '入荷(小)の情報をクリア
        'frm.sprDetail.CrearSpread()

        '入荷(中)情報表示
        Call Me._G.SetInkaMData(Me._Ds)

        '追加したレコードの詳細情報表示
        Call Me._G.ClearInkaMControl()
        Call Me._G.SetInkaMInforData(Me._Ds, dt.Rows.Count - 1)

        '作業情報を設定(1行しかありえないrtnDrため固定で設定)
        'Call Me.SetRtnGoodsDataAtSagyo(frm, rtnDr)

        Return True

    End Function

    ''' <summary>
    ''' 入荷(小)データ追加処理(検品取込)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="inkaMNo">入荷中番</param>
    ''' <remarks></remarks>
    Private Function AddKenpinInkaSData(ByVal frm As LMB020F, _
                                  ByVal inkaMNo As String, _
                                  ByVal dtRow As DataRow, _
                                  ByVal rtnRow As DataRow, _
                                  Optional ByVal rowNo As Integer = -1, _
                                  Optional ByVal copyCnt As Integer = -1, _
                                  Optional ByVal copyMax As Integer = -1) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim dt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_S)
        Dim maxDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_MAX_NO)
        Dim rtnResult As Boolean = False
        Dim rowIndex As Integer = 0

        'SEQの限界値、チェック
        If Me._LMBconV.IsMaxSeqChk(maxDt.Select(Me._G.SetInkaSSql(inkaMNo))(0), "MAX_INKA_NO_S", 1, "入荷管理番号S") = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            If copyMax <> -1 Then
                '入荷(小)情報表示
                Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.INIT_S, inkaMNo)

                '合計個数・数量・重量の計算処理
                If Me.SetSumData(frm, "sprDetail") = False Then
                    Logger.EndLog(MyBase.GetType.Name, "AddInkaSData")
                    Return False
                End If

                '入力(中)のデータを設定
                rtnResult = Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.INIT_S)

                If rtnResult = True Then
                    '入荷(中)情報表示
                    Call Me._G.SetInkaMData(Me._Ds)

                    '入荷(中)の詳細情報表示
                    rowIndex = Me.GetInkaNoM(frm, inkaMNo)
                    Call Me.SetInkaMInforData(frm, rowIndex)

                End If

            End If
            Return False
        End If

        Dim max As Integer = dt.Columns.Count - 1
        Dim dr As DataRow = dt.NewRow()
        For i As Integer = 0 To max
            dr.Item(i) = String.Empty
        Next

        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim inkaLNo As String = frm.lblKanriNoL.TextValue
        Dim inkaSNo As String = Me.SetInkaSmaxSeq(inkaMNo)
        Dim cstcdL As String = frm.txtCustCdL.ToString()
        Dim cstcdM As String = frm.txtCustCdM.ToString()

        Dim intsouKosu As Integer = Convert.ToInt32(rtnRow.Item("JISSEKI_INKA_NB").ToString())
        Dim douSouSuryo As Double = Convert.ToDouble(rtnRow.Item("JISSEKI_INKA_QT").ToString())
        Dim intIrisu As Integer = Convert.ToInt32(rtnRow.Item("PKG_NB").ToString())
        Dim douIrime As Double = Convert.ToDouble(rtnRow.Item("IRIME").ToString())
        Dim douStdIrime As Double = Convert.ToDouble(rtnRow.Item("STD_IRIME_NB").ToString())
        Dim douStdwtkgs As Double = Convert.ToDouble(rtnRow.Item("STD_WT_KGS").ToString())

        Dim douKonsu As Double = intsouKosu 'douSouSuryo / intsouKosu
        Dim douHasu As Double = 0 'douSouSuryo Mod intsouKosu
        Dim douKosu As Double = intsouKosu 'intsouKosu / intIrisu
        Dim douSuryo As Double = douSouSuryo 'douSouSuryo / douIrime
        Dim douJuryo As Double = intsouKosu * douStdwtkgs '(douKosu * douIrime * douStdwtkgs) / douStdIrime

        '入荷(小)の作成
        With dr

            .Item("NRS_BR_CD") = brCd
            .Item("INKA_NO_L") = inkaLNo
            .Item("INKA_NO_M") = inkaMNo
            .Item("INKA_NO_S") = inkaSNo
            .Item("ZAI_REC_NO") = String.Empty
            .Item("LOT_NO") = rtnRow.Item("LOT_NO").ToString()
            .Item("LOCA") = rtnRow.Item("LOCA").ToString()
            .Item("TOU_NO") = rtnRow.Item("TOU_NO").ToString()
            .Item("SITU_NO") = rtnRow.Item("SITU_NO").ToString()
            .Item("ZONE_CD") = rtnRow.Item("ZONE_CD").ToString()
            .Item("KONSU") = douKonsu.ToString()
            .Item("HASU") = douHasu.ToString()
            .Item("IRIME") = rtnRow.Item("IRIME").ToString()
            .Item("BETU_WT") = String.Empty
            .Item("SERIAL_NO") = String.Empty
            .Item("GOODS_COND_KB_1") = String.Empty
            .Item("GOODS_COND_KB_2") = String.Empty
            .Item("GOODS_COND_KB_3") = String.Empty
            .Item("GOODS_CRT_DATE") = String.Empty
            .Item("LT_DATE") = String.Empty
            .Item("SPD_KB") = "01"
            .Item("OFB_KB") = "01"
            .Item("DEST_CD") = String.Empty
            .Item("REMARK") = rtnRow.Item("REMARK_L")
            .Item("ALLOC_PRIORITY") = String.Empty 'LMB020C.WARIATE_FREE
            .Item("REMARK_OUT") = rtnRow.Item("REMARK_M")
            .Item("SYS_DEL_FLG") = LMConst.FLG.OFF
            .Item("KOSU_S") = douKosu.ToString()
            '.Item("STD_IRIME_NB_S") = rtnRow.Item("STD_IRIME_NB")
            .Item("STD_IRIME_UT") = rtnRow.Item("STD_IRIME_UT")

            Dim irimeTani As String = dtRow.Item("STD_IRIME_UT").ToString()
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_I001, "' AND KBN_CD = '", irimeTani, "'"))
            Dim kbnNm As String = String.Empty
            If 0 < drs.Length Then

                '2017/09/25 修正 李↓
                kbnNm = drs(0).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString()
                '2017/09/25 修正 李↑

            End If
            .Item("STD_IRIME_NM") = kbnNm

            .Item("SURYO_S") = douSuryo.ToString()
            .Item("JURYO_S") = douJuryo.ToString()
            .Item("STD_WT_KGS") = douStdwtkgs.ToString()
            .Item("DEST_NM") = String.Empty
            .Item("SUM_KONSU_S") = rtnRow.Item("JISSEKI_INKA_NB")
            .Item("LOT_CTL_KB") = String.Empty
            .Item("LT_DATE_CTL_KB") = String.Empty
            .Item("CRT_DATE_CTL_KB") = String.Empty
            .Item("ZAI_REC_CNT") = String.Empty
            .Item("UP_KBN") = LMConst.FLG.OFF

        End With

        '行追加()
        dt.Rows.Add(dr)

        Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.EDIT, inkaMNo)

        'ロット№にフォーカス
        frm.sprDetail.Focus()
        frm.sprDetail.ActiveSheet.SetActiveCell(max, LMB020C.SprInkaSColumnIndex.LOT_NO)

        Return True

    End Function
    '追加終了 2015.01.16 韓国CALT対応

#End Region

#Region "行削除"

    ''' <summary>
    ''' 入荷(中)行削除
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteInkaMData(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.COPY))

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then

            '並び替え
            Call Me._G.sprGoodsSortColumnCommand()

            arr = Me._LMBconH.GetCheckList(frm.sprGoodsDef.ActiveSheet, LMB020G.sprGoodsDef.DEF.ColNo)

        End If

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._LMBconV.IsSelectChk(arr.Count)

        '計算処理
        rtnResult = rtnResult AndAlso Me.AllCalculation(frm)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInkaMAddChk(LMB020C.ActionType.DEL_M, arr)

        '引当済チェック
        rtnResult = rtnResult AndAlso Me._V.IsHikiateDelMChk(Me._Ds, arr)

        '在庫移動チェック
        rtnResult = rtnResult AndAlso Me._V.IsIdoTrsDelMChk(Me._Ds, arr)

        'START YANAI メモ②No.20
        'EDIデータチェック
        rtnResult = rtnResult AndAlso Me._V.IsEDIChk(Me._Ds, arr)
        'END YANAI メモ②No.20

        '入力中のデータを設定
        rtnResult = rtnResult AndAlso Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.DEL_M)


        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        ''--------要望番号1904 （中）（小）の個数違いによるワーニングを出す
        'Dim chkResult As Boolean = Me.SetKosuChkMessage(frm)

        'If chkResult = False Then
        '    '処理終了アクション
        '    Call Me.EndAction(frm)
        '    Exit Sub
        'End If

        ''--------要望番号1904 （中）（小）の個数違いによるワーニングを出す
        '--------要望番号1904 （中）（小）の個数違いによるワーニングを出す
        Dim maxchk As Integer = arr.Count - 1
        Dim recNoChk As Integer = 0
        For i As Integer = 0 To maxchk

            '行番号を設定
            recNoChk = Me.GetRecNo(frm.sprGoodsDef.ActiveSheet, arr(i).ToString(), LMB020G.sprGoodsDef.RECNO.ColNo)

            '入荷中番を設定
            Dim inkanoM As String = String.Empty
            inkanoM = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows(recNoChk).Item("INKA_NO_M").ToString()


            Call Me._G.SetInkaMInforData(Me._Ds, -1, inkanoM)

            Dim chkResult As Boolean = Me.SetKosuChkMessage(frm)

            If chkResult = False Then
                '処理終了アクション
                Call Me.EndAction(frm)

                '入荷(中)の詳細情報クリア
                Call Me._G.ClearInkaMControl()
                frm.sprDetail.CrearSpread()
                Call Me._G.SetInkaMInforData(Me._Ds, -1, inkanoM)

                '入荷(小)情報表示
                Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.SAVE, frm.lblKanriNoM.TextValue)

                Exit Sub
            End If

        Next

        '--------要望番号1904 （中）（小）の個数違いによるワーニングを出す

        '画面の入荷(中)の情報をクリア
        Call Me._G.ClearInkaMControl()

        '画面の入荷(小)の情報をクリア
        frm.sprDetail.CrearSpread()

        Dim max As Integer = arr.Count - 1
        Dim recNo As Integer = 0
        Dim inkaMNo As String = String.Empty
        Dim inkaMDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M)
        Dim inkaSDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_S)
        Dim unsoMDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_UNSO_M)
        Dim sagyoDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_SAGYO)
        Dim zaikoDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_ZAI)
        Dim sql As String = String.Empty
        Dim spr As SheetView = frm.sprGoodsDef.ActiveSheet
        For i As Integer = max To 0 Step -1

            '行番号を設定
            recNo = Me.GetRecNo(spr, arr(i).ToString(), LMB020G.sprGoodsDef.RECNO.ColNo)

            '入荷中番を設定
            inkaMNo = inkaMDt.Rows(recNo).Item("INKA_NO_M").ToString()

            '2014.07.30 Ri [ｱｸﾞﾘ対応] Add -ST-
            Call Me.DeleteKenpinRowMData(inkaMDt.Rows(recNo))
            '2014.07.30 Ri [ｱｸﾞﾘ対応] Add -ED-

            'ADD 2022/11/07 倉庫写真アプリ対応 START
            '入荷写真データのレコード削除
            Call Me.DeleteInkaPhotoRowMData(inkaMDt.Rows(recNo))
            'ADD 2022/11/07 倉庫写真アプリ対応 END

            '入荷(中)のレコード削除
            Call Me.DeleteRowData(inkaMDt.Rows(recNo))

            '紐付く入荷(小)のレコード削除
            sql = Me._G.SetInkaSSql(inkaMNo)
            Call Me.DeleteTabelData(inkaSDt, sql)

            '紐付く在庫のレコード削除
            Call Me.DeleteTabelData(zaikoDt, sql)

            '紐付く作業のレコード削除
            Call Me.DeleteTabelData(sagyoDt, String.Concat("INOUTKA_NO_LM LIKE '%", inkaMNo, "' "))

            'START YANAI 要望番号1016
            '紐付く運送(中)のレコード削除
            Call Me.DeleteTabelData(unsoMDt, String.Concat("UNSO_NO_M ='", inkaMNo, "' "))
            'END YANAI 要望番号1016

            'INKA_TORI_FLGリセット対象として追加
            Call Me.SetKenpinWkToriResetTarget(frm, inkaMNo)    'ADD 2019/12/02 006350

        Next

        '入荷(中)の情報を設定
        Call Me._G.SetInkaMData(Me._Ds)
        If frm.sprGoodsDef.ActiveSheet.Rows.Count <> 0 Then
            Dim firstMNo As String = Me._LMBconV.GetCellValue(frm.sprGoodsDef.ActiveSheet.Cells(0, LMB020G.sprGoodsDef.KANRI_NO.ColNo))
            Call Me._G.SetInkaMInforData(Me._Ds, -1, firstMNo)
            '入荷(小)情報表示
            Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.DOUBLECLICK, firstMNo)
            '重量合計再計算
            Call Me.SetJuryoReData(frm)
        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        'ロック制御
        Call Me._G.SetControlsStatus(LMB020C.ActionType.DEL_M, Me._Ds)

    End Sub

    ''' <summary>
    ''' 入荷(小)行削除
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteInkaSData(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '並び替え
        Call Me._G.sprDetailSortColumnCommand()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMB020C.ActionType.DEL_S)
        If rtnResult = False Then
            '処理終了アクション
            MyBase.ShowMessage(frm, "E016")
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMBconH.GetCheckList(frm.sprDetail.ActiveSheet, LMB020G.sprDetailDef.DEF.ColNo)
        End If

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._LMBconV.IsSelectChk(arr.Count)

        '引当済チェック
        rtnResult = rtnResult AndAlso Me._V.IsHikiateDelSChk(Me._Ds)

        '在庫移動チェック
        rtnResult = rtnResult AndAlso Me._V.IsIdoTrsDelSChk(Me._Ds, arr)

        '入力中のデータを設定
        rtnResult = rtnResult AndAlso Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.DEL_S)

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        Dim max As Integer = arr.Count - 1
        Dim recNo As Integer = 0
        Dim inkaNoM As String = frm.lblKanriNoM.TextValue
        Dim inkaSNo As String = String.Empty
        Dim inkaSDrs As DataRow() = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(Me._G.SetInkaSSql(inkaNoM), "INKA_NO_S")
        Dim zaikoDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_ZAI)
        Dim spr As SheetView = frm.sprDetail.ActiveSheet
        For i As Integer = max To 0 Step -1

            '行番号を設定
            recNo = Me.GetRecNo(spr, arr(i).ToString(), LMB020G.sprDetailDef.RECNO.ColNo)

            '入荷小番を設定
            inkaSNo = inkaSDrs(recNo).Item("INKA_NO_S").ToString()

            '2014.07.30 Ri [ｱｸﾞﾘ対応] Add -ST-
            Call Me.DeleteKenpinRowSData(inkaSDrs(recNo))
            '2014.07.30 Ri [ｱｸﾞﾘ対応] Add -ED-

            'ADD 2022/11/07 倉庫写真アプリ対応 START
            '入荷写真データのレコード削除
            Call Me.DeleteInkaPhotoRowSData(inkaSDrs(recNo))
            'ADD 2022/11/07 倉庫写真アプリ対応 END

            '入荷(小)のレコード削除
            Call Me.DeleteRowData(inkaSDrs(recNo))

            '紐付く在庫のレコード削除
            Call Me.DeleteTabelData(zaikoDt, String.Concat("INKA_NO_M = '", inkaNoM, "' AND INKA_NO_S = '", inkaSNo, "' "))

            'INKA_TORI_FLGリセット対象として追加
            Call Me.SetKenpinWkToriResetTarget(frm, inkaNoM, inkaSNo)    'ADD 2019/12/02 006350

        Next

        '処理終了アクション(ロック制御により解除)
        Call Me.EndAction(frm)

        '入荷(小)の情報を設定
        Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.EDIT, inkaNoM)

    End Sub

    ''' <summary>
    ''' メインテーブル行削除処理
    ''' </summary>
    ''' <param name="dt">データテーブル</param>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="arr">リスト</param>
    ''' <remarks></remarks>
    Private Sub DeleteTabelData(ByVal dt As DataTable, ByVal spr As SheetView, ByVal arr As ArrayList)

        Dim max As Integer = arr.Count - 1

        '削除のみのループ
        For i As Integer = 0 To max

            '行削除処理
            Call Me.DeleteRowData(dt.Rows(Convert.ToInt32(Me._LMBconV.GetCellValue(spr.Cells(Convert.ToInt32(arr(i)), LMB020G.sprGoodsDef.RECNO.ColNo)))))

        Next

    End Sub

    ''' <summary>
    ''' メインテーブル行削除処理
    ''' </summary>
    ''' <param name="dt">データテーブル</param>
    ''' <param name="sql">SQL</param>
    ''' <remarks></remarks>
    Private Sub DeleteTabelData(ByVal dt As DataTable, ByVal sql As String)

        Dim drs As DataRow() = dt.Select(sql)

        Dim max As Integer = drs.Length - 1

        '削除のみのループ
        For i As Integer = max To 0 Step -1

            '行削除処理
            Call Me.DeleteRowData(drs(i))

        Next

    End Sub

    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <param name="dr">データロウ</param>
    ''' <remarks></remarks>
    Private Sub DeleteRowData(ByVal dr As DataRow)

        If LMConst.FLG.ON.Equals(dr.Item("UP_KBN").ToString()) = True Then

            '削除フラグをON
            dr.Item("SYS_DEL_FLG") = LMConst.FLG.ON

        Else

            '行自体を削除
            dr.Delete()

        End If

    End Sub

    ''' <summary>
    ''' CSVデータによる入荷(中)追加処理(AddInkaMData由来)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub AddCSV_M_data(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.INIT_M))

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInkaMAddChk(LMB020C.ActionType.INIT_M)

        '入荷(中)の追加処理
        rtnResult = rtnResult AndAlso Me.AddInkaMDataAction_CSV(frm)

        '処理終了アクション(ロック制御により解除)
        Call Me.EndAction(frm)

        '入荷(小)の追加処理
        'rtnResult = rtnResult AndAlso Me.AddInkaSData(frm, frm.lblKanriNoM.TextValue)
        rtnResult = rtnResult AndAlso Me.AddCSV_S_data(frm, frm.lblKanriNoM.TextValue)

        'ロック制御
        Call Me._G.SetControlsStatus(LMB020C.ActionType.INIT_M, Me._Ds)

        If rtnResult = True Then

            ''検索条件クリア
            'frm.txtSerchGoodsCd.TextValue = String.Empty
            'frm.txtSerchGoodsNm.TextValue = String.Empty

            'スクロールバーを一番下に設定
            Call Me.SetEndScrollGoods(frm)
            Call Me.SetEndScrollDetail(frm)

        End If

    End Sub

    ''' <summary>
    '''CSVデータによる入荷(中)追加処理(AddInkaMData由来) 入荷(中)データ追加処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rtnDs">戻りDataSet</param>
    ''' <remarks></remarks>
    Private Function AddCSV_M_data(ByVal frm As LMB020F, ByVal rtnDs As DataSet) As Boolean

        Dim dt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M)
        'Dim csvdt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_CSV_DATA)
        'Dim rtnDt As DataTable = rtnDs.Tables(LMZ020C.TABLE_NM_OUT)
        Dim rtnDt As DataTable = rtnDs.Tables("LMB020_GOODS_NM") 'キャッシュからDSへ
        Dim max As Integer = rtnDt.Rows.Count - 1
        Dim maxDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_MAX_NO)

        ''SEQの限界値、チェック
        'If Me._LMBconV.IsMaxSeqChk(maxDt.Rows(maxDt.Rows.Count - 1), "INKA_NO_M", max + 1, "入荷管理番号M") = False Then
        '    '処理終了アクション
        '    Call Me.EndAction(frm)
        '    Return False
        'End If

        Dim rowCnt As Integer = 0
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim inkaLNo As String = frm.lblKanriNoL.TextValue
        Dim maxSeq As String = String.Empty
        Dim nbUt As String = String.Empty
        Dim rtnDr As DataRow = Nothing

        For i As Integer = 0 To max

            '行番号の設定
            rowCnt = dt.Rows.Count

            '空行追加
            dt.Rows.Add(dt.NewRow())

            With dt.Rows(rowCnt)

                maxSeq = Me.SetInkaMmaxSeq()
                rtnDr = rtnDt.Rows(i)

                .Item("NRS_BR_CD") = brCd
                .Item("INKA_NO_L") = inkaLNo
                .Item("INKA_NO_M") = maxSeq
                '.Item("GOODS_CD_NRS") = rtnDr.Item("GOODS_CD_NRS").ToString()
                .Item("GOODS_CD_CUST") = rtnDr.Item("GOODS_CD_CUST").ToString()
                '.Item("OUTKA_FROM_ORD_NO_M") = String.Empty
                '.Item("BUYER_ORD_NO_M") = String.Empty
                '.Item("REMARK") = String.Empty
                .Item("PRINT_SORT") = 99
                .Item("GOODS_NM") = rtnDr.Item("GOODS_NM_1").ToString()
                '.Item("ONDO_KB") = rtnDr.Item("ONDO_KB").ToString()
                .Item("SUM_KOSU") = 0
                'nbUt = rtnDr.Item("NB_UT").ToString()
                '.Item("NB_UT") = nbUt
                '.Item("ONDO_STR_DATE") = rtnDr.Item("ONDO_STR_DATE").ToString()
                '.Item("ONDO_END_DATE") = rtnDr.Item("ONDO_END_DATE").ToString()
                '.Item("PKG_NB") = rtnDr.Item("PKG_NB").ToString()
                '.Item("PKG_NB_UT1") = nbUt
                '.Item("PKG_NB_UT2") = rtnDr.Item("PKG_UT").ToString()
                '.Item("STD_IRIME_NB_M") = rtnDr.Item("STD_IRIME_NB").ToString()
                '.Item("STD_IRIME_UT") = rtnDr.Item("STD_IRIME_UT").ToString()
                .Item("SUM_SURYO_M") = 0
                .Item("SUM_JURYO_M") = 0
                '.Item("SHOBO_CD") = String.Empty
                .Item("HIKIATE") = LMB020C.HIKIATE_NASI
                .Item("SAGYO_UMU") = String.Empty
                .Item("SYS_DEL_FLG") = 0
                .Item("EDI_KOSU") = 0
                .Item("EDI_SURYO") = 0
                '.Item("CUST_CD_L") = rtnDr.Item("CUST_CD_L").ToString()
                '.Item("CUST_CD_M") = rtnDr.Item("CUST_CD_M").ToString()
                '.Item("CUST_CD_S") = rtnDr.Item("CUST_CD_S").ToString()
                '.Item("CUST_CD_SS") = rtnDr.Item("CUST_CD_SS").ToString()
                '.Item("STD_WT_KGS") = rtnDr.Item("STD_WT_KGS").ToString()
                '.Item("LOT_CTL_KB") = rtnDr.Item("LOT_CTL_KB").ToString()
                '.Item("LT_DATE_CTL_KB") = rtnDr.Item("LT_DATE_CTL_KB").ToString()
                '.Item("CRT_DATE_CTL_KB") = rtnDr.Item("CRT_DATE_CTL_KB").ToString()
                ''START YANAI メモ②No.20
                '.Item("EDI_FLG") = String.Empty
                ''END YANAI メモ②No.20
                '.Item("UP_KBN") = 0
                ''(2012.12.27)要望番号1692関連 項目追加 -- START --
                '.Item("TARE_YN") = rtnDr.Item("TARE_YN").ToString()
                ''(2012.12.27)要望番号1692関連 項目追加 --  END  --
            End With

        Next

        '入荷(小)の情報をクリア
        frm.sprDetail.CrearSpread()

        '入荷(中)情報表示 あとでコメント解除
        Call Me._G.SetInkaMData(Me._Ds)

        ''追加したレコードの詳細情報表示
        'Call Me._G.ClearInkaMControl()
        'Call Me._G.SetInkaMInforData(Me._Ds, dt.Rows.Count - 1)

        ''作業情報を設定(1行しかありえないため固定で設定)
        'Call Me.SetRtnGoodsDataAtSagyo(frm, rtnDr)

        Return True

    End Function

    ''' <summary>
    ''' 入荷(中)データ追加前処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function AddInkaMDataAction_CSV(ByVal frm As LMB020F) As Boolean

        '計算処理(データ反映のため)暫定コメントアウト
        'Dim rtnResult As Boolean = Me.AllCalculation(frm)
        Dim rtnResult As Boolean

        '入力(中)のデータを設定
        rtnResult = rtnResult AndAlso Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.INIT_M)


        '商品Pop表示
        Dim prm As LMFormData = Nothing
        'If rtnResult = True Then

        'START YANAI 要望番号481
        'prm = Me.ShowGoodsPopup(frm)
        'prm = Me.ShowGoodsPopup_CSV(frm, LMB020C.ActionType.INIT_M)
        'prm = Me.ShowGoodsPopup_CSV(frm, LMB020C.ActionType.ENTER)
        'END YANAI 要望番号481
        rtnResult = rtnResult AndAlso prm.ReturnFlg

        'End If

        Return rtnResult AndAlso Me.AddCSV_M_data(frm, prm.ParamDataSet)

    End Function

    ''' <summary>
    ''' CSVデータによる入荷(小)追加処理(AddInkaSData由来)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub AddCSV_S_data(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.INIT_S))

        Dim arr As ArrayList = Me._V.IsSprSSelectChk()
        Dim cnt As Integer = arr.Count

        ''行複写の追加チェック（未選択、単一行選択チェック）
        'If copyFlg = True Then
        '    rtnResult = rtnResult AndAlso Me._LMBconV.IsSelectChk(cnt)
        '    rtnResult = rtnResult AndAlso Me._LMBconV.IsSelectOneChk(cnt)

        '    'START YANAI 要望番号557
        '    rtnResult = rtnResult AndAlso Me._V.IsInkaNoSOverChk()
        '    'END YANAI 要望番号557

        'End If

        '入荷中番の必須チェック
        rtnResult = rtnResult AndAlso Me._V.IsInkaMNoHissuChk()

        '処理終了アクション(ロック制御により解除)
        Call Me.EndAction(frm)

        ''入荷(小)の追加処理
        'If copyFlg = True Then
        '    'START YANAI 要望番号557
        '    'rtnResult = rtnResult AndAlso Me.AddInkaSData(frm, frm.lblKanriNoM.TextValue, Convert.ToInt32(arr(0)))
        '    Dim max As Integer = Convert.ToInt32(frm.numRowCopyScnt.TextValue) - 1
        '    For i As Integer = 0 To max
        '        rtnResult = rtnResult AndAlso Me.AddInkaSData(frm, frm.lblKanriNoM.TextValue, Convert.ToInt32(arr(0)), i, max)
        '    Next
        '    'END YANAI 要望番号557
        'Else
        rtnResult = rtnResult AndAlso Me.AddInkaSData(frm, frm.lblKanriNoM.TextValue)   '☆☆☆
        'rtnResult = rtnResult AndAlso Me.AddCSV_S_data(frm, frm.lblKanriNoM.TextValue) '☆20130215
        'End If

        If rtnResult = True Then

            'スクロールバーを一番下に設定
            Call Me.SetEndScrollDetail(frm)

        End If

    End Sub

    'START YANAI 要望番号557
    '''' <summary>
    '''' 入荷(小)データ追加処理
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <param name="inkaMNo">入荷中番</param>
    '''' <remarks></remarks>
    'Private Function AddInkaSData(ByVal frm As LMB020F, ByVal inkaMNo As String, Optional ByVal rowNo As Integer = -1) As Boolean
    ''' <summary>
    ''' 入荷(小)データ追加処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="inkaMNo">入荷中番</param>
    ''' <remarks></remarks>
    Private Function AddCSV_S_data(ByVal frm As LMB020F, _
                          ByVal inkaMNo As String, _
                          Optional ByVal rowNo As Integer = -1, _
                          Optional ByVal copyCnt As Integer = -1, _
                          Optional ByVal copyMax As Integer = -1) As Boolean

        'END YANAI 要望番号557

        Dim dt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_S)
        Dim maxDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_MAX_NO)
        'START YANAI 要望番号557
        Dim rtnResult As Boolean = False
        Dim rowIndex As Integer = 0
        'END YANAI 要望番号557

        'CSV対応
        Dim csvDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_CSV_DATA)

        'SEQの限界値、チェック
        'If Me._LMBconV.IsMaxSeqChk(maxDt.Select(Me._G.SetInkaSSql(inkaMNo))(0), "MAX_INKA_NO_S", 1, "入荷管理番号S") = False Then
        '処理終了アクション
        Call Me.EndAction(frm)
        'START YANAI 要望番号557
        If copyMax <> -1 Then
            '入荷(小)情報表示
            Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.INIT_S, inkaMNo) '☆

            '合計個数・数量・重量の計算処理
            If Me.SetSumData(frm, "sprDetail") = False Then
                Logger.EndLog(MyBase.GetType.Name, "AddInkaSData")
                Return False
            End If

            '入力(中)のデータを設定
            'rtnResult = Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.INIT_S)

            'If rtnResult = True Then
            '    '入荷(中)情報表示
            '    Call Me._G.SetInkaMData(Me._Ds)

            '    '入荷(中)の詳細情報表示
            '    rowIndex = Me.GetInkaNoM(frm, inkaMNo)
            '    Call Me.SetInkaMInforData(frm, rowIndex)

            'End If

        End If
        'END YANAI 要望番号557
        'Return False
        'End If

        '入力(中)のデータを設定

        'START YANAI 要望番号557
        'If Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.INIT_S) = False Then
        '    '処理終了アクション
        '    Call Me.EndAction(frm)
        '    Return False
        'End If

        If copyCnt = -1 OrElse copyCnt = 0 Then
            If Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.INIT_S) = False Then
                '処理終了アクション
                Call Me.EndAction(frm)
                Return False
            End If
        End If
        'END YANAI 要望番号557

        Dim max As Integer = dt.Columns.Count - 1
        Dim dr As DataRow = dt.NewRow()
        'For i As Integer = 0 To max
        '    dr.Item(i) = String.Empty
        'Next
        'Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
        'dr.Item("NRS_BR_CD") = brCd
        'dr.Item("INKA_NO_L") = frm.lblKanriNoL.TextValue
        'dr.Item("INKA_NO_M") = inkaMNo
        dr.Item("INKA_NO_S") = Me.SetInkaSmaxSeq(inkaMNo)
        'Dim sql As String = String.Concat("NRS_BR_CD = '", brCd, "' " _
        '                                  , " AND INKA_NO_L = '", frm.lblKanriNoL.TextValue, "' " _
        '                                  , " AND INKA_NO_M = '", inkaMNo, "' " _
        '                                  )
        'Dim inkaMDr As DataRow = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(sql)(0)
        'dr.Item("IRIME") = inkaMDr.Item("STD_IRIME_NB_M").ToString()

        'Dim irimeTani As String = inkaMDr.Item("STD_IRIME_UT").ToString()
        'Dim juryo As String = Me._LMBconG.FormatNumValue(inkaMDr.Item("STD_WT_KGS").ToString())

        'Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_I001, "' AND KBN_CD = '", irimeTani, "'"))
        'Dim kbnNm As String = String.Empty
        'If 0 < drs.Length Then
        '    kbnNm = drs(0).Item("KBN_NM1").ToString()
        'End If

        'dr.Item("STD_IRIME_NM") = kbnNm
        'dr.Item("IRIME") = frm.numStdIrime.Value.ToString()
        'dr.Item("JURYO_S") = "0"
        'dr.Item("ALLOC_PRIORITY") = LMB020C.WARIATE_FREE
        'dr.Item("OFB_KB") = "01"
        'dr.Item("SPD_KB") = "01"
        'dr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
        'dr.Item("UP_KBN") = LMConst.FLG.OFF
        'dr.Item("LOT_CTL_KB") = inkaMDr.Item("LOT_CTL_KB").ToString()
        'dr.Item("LT_DATE_CTL_KB") = inkaMDr.Item("LT_DATE_CTL_KB").ToString()
        'dr.Item("CRT_DATE_CTL_KB") = inkaMDr.Item("CRT_DATE_CTL_KB").ToString()
        'dr.Item("STD_WT_KGS") = inkaMDr.Item("STD_WT_KGS").ToString()

        ''START YANAI 要望番号427
        Dim inkaNoS As String = String.Empty
        ''END YANAI 要望番号427

        ''行複写時
        'If rowNo <> -1 Then
        '    'START YANAI 要望番号493
        '    'Dim drCopy As DataRow = dt.Rows(rowNo)
        '    Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString() '20130215
        '    Dim sql As String = String.Concat("NRS_BR_CD = '", brCd, "' " _
        '                      , " AND INKA_NO_L = '", frm.lblKanriNoL.TextValue, "' " _
        '                      , " AND INKA_NO_M = '", inkaMNo, "' " _
        '                      , " AND INKA_NO_S = '", Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.KANRI_NO_S.ColNo)), "'" _
        '                                      )
        '    Dim drCopy As DataRow = dt.Select(sql)(0)
        '    '    'END YANAI 要望番号493
        '    '    '行コピー
        '    '    For i As Integer = 0 To max
        '    '        If String.IsNullOrEmpty(dr.Item(i).ToString) = True Then
        '    '            dr.Item(i) = drCopy.Item(i)
        '    '        End If
        '    '    Next
        '    dr.Item("JURYO_S") = drCopy.Item("JURYO_S")
        '    dr.Item("IRIME") = drCopy.Item("IRIME")
        '    dr.Item("ALLOC_PRIORITY") = drCopy.Item("ALLOC_PRIORITY")
        '    dr.Item("GOODS_COND_KB_1") = drCopy.Item("GOODS_COND_KB_1")
        '    dr.Item("GOODS_COND_KB_2") = drCopy.Item("GOODS_COND_KB_2")
        '    dr.Item("GOODS_COND_KB_3") = drCopy.Item("GOODS_COND_KB_3")
        '    dr.Item("OFB_KB") = drCopy.Item("OFB_KB")
        '    dr.Item("SPD_KB") = drCopy.Item("SPD_KB")
        '    dr.Item("LOT_CTL_KB") = drCopy.Item("LOT_CTL_KB").ToString()
        '    dr.Item("LT_DATE_CTL_KB") = drCopy.Item("LT_DATE_CTL_KB").ToString()
        '    dr.Item("CRT_DATE_CTL_KB") = drCopy.Item("CRT_DATE_CTL_KB").ToString()

        '    'dr.Item("JURYO_S") = drCopy.Item("JURYO_S")
        '    'dr.Item("IRIME") = drCopy.Item("IRIME")
        '    'dr.Item("ALLOC_PRIORITY") = drCopy.Item("ALLOC_PRIORITY")
        '    'dr.Item("GOODS_COND_KB_1") = drCopy.Item("GOODS_COND_KB_1")
        '    'dr.Item("GOODS_COND_KB_2") = drCopy.Item("GOODS_COND_KB_2")
        '    'dr.Item("GOODS_COND_KB_3") = drCopy.Item("GOODS_COND_KB_3")
        '    'dr.Item("OFB_KB") = drCopy.Item("OFB_KB")
        '    'dr.Item("SPD_KB") = drCopy.Item("SPD_KB")
        '    'dr.Item("LOT_CTL_KB") = drCopy.Item("LOT_CTL_KB").ToString()
        '    'dr.Item("LT_DATE_CTL_KB") = drCopy.Item("LT_DATE_CTL_KB").ToString()
        '    'dr.Item("CRT_DATE_CTL_KB") = drCopy.Item("CRT_DATE_CTL_KB").ToString()

        '    ''    'START YANAI 要望番号427
        '    ''            inkaNoS = drCopy.Item("INKA_NO_S").ToString()
        '    '    'END YANAI 要望番号427

        'End If




        'CSV対応
        If String.IsNullOrEmpty(csvDt.Columns.Count.ToString) = False Then

            Dim csvmax As Integer = csvDt.Rows.Count - 1
            Dim csvdr As DataRow = csvDt.NewRow()
            Dim rowMax As Integer = dt.Rows.Count - 1
            Dim inkaSno As Integer = Convert.ToInt32("000")

            'CSVデータ追加
            For i As Integer = 0 To csvmax

                dr.Item("REMARK_OUT") = csvDt.Rows(i).Item("REMARK_OUT").ToString()
                dr.Item("LOT_NO") = csvDt.Rows(i).Item("LOT_NO").ToString()
                dr.Item("SERIAL_NO") = csvDt.Rows(i).Item("SERIAL_NO").ToString()
                dr.Item("REMARK") = csvDt.Rows(i).Item("REMARK").ToString()
                dr.Item("TOU_NO") = csvDt.Rows(i).Item("LOC").ToString().Substring(0, 2)
                dr.Item("SITU_NO") = csvDt.Rows(i).Item("LOC").ToString().Substring(2, 1)  'ZONE_CDとの見分けが困難、室NOのほとんどが1バイトのための対応
                dr.Item("ZONE_CD") = csvDt.Rows(i).Item("LOC").ToString().Substring(3, 1)    'ZONE_CDとの見分けが困難、ゾーンCDのほとんどが1バイトのための対応
                dr.Item("INKA_NO_S") = Convert.ToString(inkaSno + 1)
            Next
            Me._G.SetInkaSData_CSV(Me._Ds, LMB020C.ActionType.INIT_S, inkaMNo)

        Else
            '行追加()
            dt.Rows.Add(dr)
        End If

        '以下コメントアウト
        ''行追加
        'dt.Rows.Add(dr)

        ''START YANAI 要望番号557
        ' ''入荷(小)情報表示
        Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.EDIT, inkaMNo) '☆

        ''START YANAI 要望番号427
        'If rowNo <> -1 Then
        '    Call Me.SetDefChk(frm, inkaNoS)
        'End If
        ''END YANAI 要望番号427

        ''ロット№にフォーカス
        'frm.sprDetail.Focus()
        'frm.sprDetail.ActiveSheet.SetActiveCell(max, LMB020C.SprInkaSColumnIndex.LOT_NO)


        ''複写は無関係なのでコメント        
        'If (copyCnt).Equals(copyMax) = True Then
        '    '複写時は、最終複写の時のみ行う

        '    '入荷(小)情報表示
        '    Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.INIT_S, inkaMNo)

        '    If copyMax <> -1 Then

        '        '合計個数・数量・重量の計算処理
        '        If Me.SetSumData(frm, "sprDetail") = False Then
        '            Logger.EndLog(MyBase.GetType.Name, "AddInkaSData")
        '            Return False
        '        End If

        '        '入力(中)のデータを設定
        '        rtnResult = Me.SetDataSetInData(frm, Me._Ds, LMB020C.ActionType.INIT_S)

        '        If rtnResult = True Then
        '            '入荷(中)情報表示
        '            Call Me._G.SetInkaMData(Me._Ds)

        '            '入荷(中)の詳細情報表示
        '            rowIndex = Me.GetInkaNoM(frm, inkaMNo)
        '            Call Me.SetInkaMInforData(frm, rowIndex)

        '        End If

        '    End If

        '    'START YANAI 要望番号427
        '    If rowNo <> -1 Then
        '        Call Me.SetDefChk(frm, inkaNoS)
        '    End If
        'END YANAI 要望番号427

        'ロット№にフォーカス
        'frm.sprDetail.Focus()
        'frm.sprDetail.ActiveSheet.SetActiveCell(max, LMB020C.SprInkaSColumnIndex.LOT_NO)
        'End If
        'END YANAI 要望番号557

        Return True

    End Function

    '2014.07.30 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ST-
    ''' <summary>
    ''' 検品ワーク削除処理
    ''' </summary>
    ''' <param name="dr">データロウ</param>
    ''' <remarks></remarks>
    Private Sub DeleteKenpinRowMData(ByVal dr As DataRow)

        '検品ワークデータ
        Dim kpWkDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_WK_DATA)
        Dim kpWkRows() As DataRow = Nothing
        Dim delRowCnt As Integer = 0

        kpWkRows = kpWkDt.Select(String.Concat("INKA_NO_M = '", dr.Item("INKA_NO_M").ToString(), "'"))

        For Each rows As DataRow In kpWkRows
            '削除行の割出し
            delRowCnt = kpWkDt.Rows.IndexOf(rows)
            '行自体を削除
            kpWkDt.Rows(delRowCnt).Delete()
        Next

    End Sub

    ''' <summary>
    ''' 検品ワーク削除処理
    ''' </summary>
    ''' <param name="dr">データロウ</param>
    ''' <remarks></remarks>
    Private Sub DeleteKenpinRowSData(ByVal dr As DataRow)

        '検品ワークデータ
        Dim kpWkDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_WK_DATA)
        Dim kpWkRows() As DataRow = Nothing
        Dim delRowCnt As Integer = 0

        kpWkRows = kpWkDt.Select(String.Concat("INKA_NO_S = '", dr.Item("INKA_NO_S").ToString(), "'"))

        For Each rows As DataRow In kpWkRows
            '削除行の割出し
            delRowCnt = kpWkDt.Rows.IndexOf(rows)
            '行自体を削除
            kpWkDt.Rows(delRowCnt).Delete()
        Next

    End Sub
    '2014.07.30 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ED-

    'ADD S 2019/12/02 006350
    ''' <summary>
    ''' 入荷検品ワーク.INKA_TORI_FLGのリセット対象をデータテーブルに追加する
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="inkaNoM">入荷管理番号(中)</param>
    ''' <param name="inkaNoS">入荷管理番号(小)</param>
    ''' <remarks></remarks>
    Private Sub SetKenpinWkToriResetTarget(ByVal frm As LMB020F, ByVal inkaNoM As String, Optional ByVal inkaNoS As String = "")

        Dim targetTbl As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_WK_TORI_RESET)
        Dim targetRow As DataRow = targetTbl.NewRow
        targetRow("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
        targetRow("CUST_CD_L") = frm.txtCustCdL.TextValue
        targetRow("INKA_NO_L") = frm.lblKanriNoL.TextValue
        targetRow("INKA_NO_M") = inkaNoM
        targetRow("INKA_NO_S") = inkaNoS
        targetTbl.Rows.Add(targetRow)

    End Sub
    'ADD E 2019/12/02 006350

    'ADD 2022/11/07 倉庫写真アプリ対応 START
    ''' <summary>
    ''' 入荷写真データ削除処理
    ''' </summary>
    ''' <param name="dr">データロウ</param>
    ''' <remarks></remarks>
    Private Sub DeleteInkaPhotoRowMData(ByVal dr As DataRow)

        '入荷写真データ
        Dim inkaPDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_PHOTO)
        Dim inkaPRows() As DataRow = Nothing
        Dim delRowCnt As Integer = 0

        inkaPRows = inkaPDt.Select(String.Concat("INKA_NO_M = '", dr.Item("INKA_NO_M").ToString(), "'"))

        For Each rows As DataRow In inkaPRows
            '削除行の割出し
            delRowCnt = inkaPDt.Rows.IndexOf(rows)
            '行自体を削除
            inkaPDt.Rows(delRowCnt).Delete()
        Next

    End Sub

    ''' <summary>
    ''' 入荷写真データ削除処理
    ''' </summary>
    ''' <param name="dr">データロウ</param>
    ''' <remarks></remarks>
    Private Sub DeleteInkaPhotoRowSData(ByVal dr As DataRow)

        '入荷写真データ
        Dim inkaPDt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_PHOTO)
        Dim inkaPRows() As DataRow = Nothing
        Dim delRowCnt As Integer = 0

        inkaPRows = inkaPDt.Select(String.Concat("INKA_NO_M = '", dr.Item("INKA_NO_M").ToString(), "'",
                                                 " AND INKA_NO_S = '", dr.Item("INKA_NO_S").ToString(), "'"))

        For Each rows As DataRow In inkaPRows
            '削除行の割出し
            delRowCnt = inkaPDt.Rows.IndexOf(rows)
            '行自体を削除
            inkaPDt.Rows(delRowCnt).Delete()
        Next

    End Sub
    'ADD 2022/11/07 倉庫写真アプリ対応 END

#End Region

#Region "計算"

    ''' <summary>
    ''' 全行計算処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function AllCalculation(ByVal frm As LMB020F) As Boolean

        '入数がゼロの場合、計算をしない
        Dim irisu As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(frm.numIrisu.TextValue))
        If 0 = irisu Then
            Return True
        End If

        '入荷中番がない場合、スルー
        Dim inkaNoM As String = frm.lblKanriNoM.TextValue
        If String.IsNullOrEmpty(inkaNoM) = True Then
            Return True
        End If

        Dim dr As DataRow = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(String.Concat("INKA_NO_M = '", inkaNoM, "' "))(0)
        Dim sidIrime As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(dr.Item("STD_IRIME_NB_M").ToString()))
        Dim sidWt As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(dr.Item("STD_WT_KGS").ToString()))

        Dim max As Integer = frm.sprDetail.ActiveSheet.Rows.Count - 1
        For i As Integer = 0 To max

            '入数 * 梱数 + 端数の計算結果を個数に設定
            If Me.SetKosuData(frm, irisu, sidIrime, sidWt, i) = False Then
                Return False
            End If

        Next

        '合計個数・数量・重量の計算処理
        If Me.SetSumData(frm, "sprDetail") = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 入数 * 梱数 + 端数
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="irisu">入数</param>
    ''' <param name="stdIrime">標準入目</param>
    ''' <param name="stdWt">標準重量</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetKosuData(ByVal frm As LMB020F, ByVal irisu As Decimal, ByVal stdIrime As Decimal, ByVal stdWt As Decimal, ByVal rowNo As Integer) As Boolean

        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        With spr.ActiveSheet

            Dim konsu As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(Me._LMBconV.GetCellValue(.Cells(rowNo, LMB020G.sprDetailDef.NB.ColNo))))
            Dim hasu As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(Me._LMBconV.GetCellValue(.Cells(rowNo, LMB020G.sprDetailDef.HASU.ColNo))))

            '入数 , 端数チェック
            If Me._V.IsHasuIrisuChk(irisu, konsu, hasu, rowNo) = False Then
                Return False
            End If

            '個数範囲チェック
            Dim kosu As Decimal = konsu * irisu + hasu
            If Me._V.IsCalcOver(kosu.ToString(), LMB020C.KOSU_MIN, LMB020C.KOSU_MAX, LMB020G.sprDetailDef.SUM.ColName) = False Then
                Me._LMBconV.SetErrorControl(spr, rowNo, LMB020G.sprDetailDef.NB.ColNo)
                Return False
            End If

            '単純に計算した値を設定
            spr.SetCellValue(rowNo, LMB020G.sprDetailDef.SUM.ColNo, kosu.ToString())

            '個数 * 入目 * 標準重量 / 標準入目の計算結果を重量に設定
            If Me.SetJuryoCalcData(frm, kosu, stdWt, Convert.ToDecimal(Me._LMBconG.FormatNumValue(Me._LMBconV.GetCellValue(.Cells(rowNo, LMB020G.sprDetailDef.IRIME.ColNo)))), stdIrime, rowNo) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 入数 * 梱数 + 端数
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">入数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetKosuData(ByVal frm As LMB020F, ByVal objNm As String) As Boolean

        With frm

            '入荷(小)以外にフォーカスがある場合、終了
            If .sprDetail.Name.Equals(objNm) = False Then
                Return True
            End If

            'フォーカス位置列番号を取得
            Dim colNo As Integer = .sprDetail.ActiveSheet.ActiveColumnIndex

            Select Case colNo

                Case LMB020G.sprDetailDef.NB.ColNo _
                    , LMB020G.sprDetailDef.HASU.ColNo _
                    , LMB020G.sprDetailDef.IRIME.ColNo

                    '入数がゼロの場合、計算をしない
                    Dim irisu As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(frm.numIrisu.TextValue))
                    If 0 = irisu Then
                        Return True
                    End If

                    '入荷中番がない場合、スルー
                    Dim inkaNoM As String = frm.lblKanriNoM.TextValue
                    If String.IsNullOrEmpty(inkaNoM) = True Then
                        Return True
                    End If

                    Dim dr As DataRow = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(String.Concat("INKA_NO_M = '", inkaNoM, "' "))(0)
                    Dim sidIrime As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(dr.Item("STD_IRIME_NB_M").ToString()))
                    Dim sidWt As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(dr.Item("STD_WT_KGS").ToString()))
                    Dim rowNo As Integer = .sprDetail.ActiveSheet.ActiveRowIndex

                    '入数 * 梱数 + 端数の計算結果を個数に設定
                    If Me.SetKosuData(frm, irisu, sidIrime, sidWt, rowNo) = False Then
                        Return False
                    End If

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 個数 * 入目
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">スプレッド名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetSuryoData(ByVal frm As LMB020F, ByVal objNm As String) As Boolean

        With frm

            '入荷(小)以外にフォーカスがある場合、終了
            If .sprDetail.Name.Equals(objNm) = False Then
                Return True
            End If

            'フォーカス位置列番号を取得
            Dim colNo As Integer = .sprDetail.ActiveSheet.ActiveColumnIndex

            Select Case colNo

                Case LMB020G.sprDetailDef.NB.ColNo _
                    , LMB020G.sprDetailDef.HASU.ColNo _
                    , LMB020G.sprDetailDef.IRIME.ColNo

                    '入数がゼロの場合、計算をしない
                    Dim irisu As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(frm.numIrisu.TextValue))
                    If 0 = irisu Then
                        Return True
                    End If

                    '入荷中番がない場合、スルー
                    Dim inkaNoM As String = frm.lblKanriNoM.TextValue
                    If String.IsNullOrEmpty(inkaNoM) = True Then
                        Return True
                    End If

                    Dim rowNo As Integer = .sprDetail.ActiveSheet.ActiveRowIndex
                    Dim g As LMBControlG = Me._LMBconG
                    Dim v As LMBControlV = Me._LMBconV
                    Dim kosu As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(v.GetCellValue(.sprDetail.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.SUM.ColNo))))
                    Dim irime As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(v.GetCellValue(.sprDetail.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.IRIME.ColNo))))

                    '個数 * 入目の計算結果を数量に設定
                    If Me.SetSuryoData(frm, kosu, irime, rowNo) = False Then
                        Return False
                    End If

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 重量再計算
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">スプレッド名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetJuryoData(ByVal frm As LMB020F, ByVal objNm As String) As Boolean

        With frm

            '入荷(小)以外にフォーカスがある場合、終了
            If .sprDetail.Name.Equals(objNm) = False Then
                Return True
            End If

            'フォーカス位置列番号を取得
            Dim colNo As Integer = .sprDetail.ActiveSheet.ActiveColumnIndex

            Select Case colNo

                Case LMB020G.sprDetailDef.NB.ColNo _
                    , LMB020G.sprDetailDef.HASU.ColNo _
                    , LMB020G.sprDetailDef.IRIME.ColNo

                    Dim rowNo As Integer = .sprDetail.ActiveSheet.ActiveRowIndex
                    Dim g As LMBControlG = Me._LMBconG
                    Dim v As LMBControlV = Me._LMBconV

                    Dim kosu As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(v.GetCellValue(.sprDetail.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.SUM.ColNo))))
                    Dim stdWt As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(v.GetCellValue(.sprDetail.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.STD_WT.ColNo))))
                    Dim irime As Decimal = Convert.ToDecimal(Me._LMBconG.FormatNumValue(v.GetCellValue(.sprDetail.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.IRIME.ColNo))))
                    Dim stdIrime As Decimal = Convert.ToDecimal(frm.numStdIrime.TextValue)

                    '個数 * 入目 * 標準重量 / 標準入目の計算結果を重量に設定
                    If Me.SetJuryoCalcData(frm, kosu, stdWt, irime, stdIrime, rowNo) = False Then
                        Return False
                    End If

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 個数 * 入目
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="kosu">個数</param>
    ''' <param name="irime">入目</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetSuryoData(ByVal frm As LMB020F, ByVal kosu As Decimal, ByVal irime As Decimal, ByVal rowNo As Integer) As Boolean

        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        With spr.ActiveSheet

            Dim g As LMBControlG = Me._LMBconG
            Dim v As LMBControlV = Me._LMBconV

            '数量範囲チェック
            If Me._V.IsCalcOver((kosu * irime).ToString(), LMB020C.SURYO_MIN, LMB020C.SURYO_MAX, LMB020G.sprDetailDef.SURYO.ColName) = False Then
                Me._LMBconV.SetErrorControl(spr, rowNo, LMB020G.sprDetailDef.NB.ColNo)
                Return False
            End If

            '単純に計算した値を設定
            spr.SetCellValue(rowNo, LMB020G.sprDetailDef.SURYO.ColNo, (kosu * irime).ToString())

        End With

        Return True

    End Function

    ''' <summary>
    ''' 個数 * 入目 * 標準重量 / 標準入目
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="kosu">個数</param>
    ''' <param name="stdWt">標準重量</param>
    ''' <param name="irime">入目</param>
    ''' <param name="stdIrime">標準入目</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetJuryoCalcData(ByVal frm As LMB020F, ByVal kosu As Decimal, ByVal stdWt As Decimal _
                                      , ByVal irime As Decimal, ByVal stdIrime As Decimal, ByVal rowNo As Integer) As Boolean

        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        With spr.ActiveSheet

            Dim g As LMBControlG = Me._LMBconG
            Dim v As LMBControlV = Me._LMBconV

            '数量範囲チェック
            If Me._V.IsCalcOver((kosu * irime * stdWt / stdIrime).ToString(), LMB020C.JURYO_MIN, LMB020C.SURYO_MAX, LMB020G.sprDetailDef.JURYO.ColName) = False Then
                Me._LMBconV.SetErrorControl(spr, rowNo, LMB020G.sprDetailDef.NB.ColNo)
                Return False
            End If

            '計算した値を設定
            'START YANAI 運送・運行・請求メモNo.48
            'spr.SetCellValue(rowNo, LMB020G.sprDetailDef.JURYO.ColNo, System.Math.Round(kosu * irime * stdWt / stdIrime, LMB020C.JURYO_ROUND_POS).ToString())
            spr.SetCellValue(rowNo, LMB020G.sprDetailDef.JURYO.ColNo, (kosu * Me._G.ToRound(irime * stdWt / stdIrime, LMB020C.JURYO_ROUND_POS)).ToString())
            'END YANAI 運送・運行・請求メモNo.48

        End With

        Return True

    End Function

    ''' <summary>
    ''' 入荷小の個数・数量・重量合計
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">スプレッド名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetSumData(ByVal frm As LMB020F, ByVal objNm As String) As Boolean

        With frm

            '入荷(小)以外にフォーカスがある場合、終了
            If .sprDetail.Name.Equals(objNm) = False Then
                Return True
            End If

            Dim g As LMBControlG = Me._LMBconG
            Dim v As LMBControlV = Me._LMBconV

            'チェックリスト取得
            Dim arr As ArrayList = Nothing
            Dim max As Integer = frm.sprDetail.ActiveSheet.Rows.Count - 1
            Dim spr As SheetView = frm.sprDetail.ActiveSheet
            Dim kosu As Decimal = 0
            Dim suryo As Decimal = 0
            Dim juryo As Decimal = 0

            Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
            Dim sql As String = String.Concat("NRS_BR_CD = '", brCd, "' " _
                                  , " AND INKA_NO_L = '", frm.lblKanriNoL.TextValue, "' " _
                                  , " AND INKA_NO_M = '", frm.lblKanriNoM.TextValue, "' " _
                                  )
            Dim inkaMDr As DataRow = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(sql)(0)

            For i As Integer = 0 To max

                '個数・数量・重量を加算
                kosu = kosu + Convert.ToDecimal(Me._LMBconG.FormatNumValue(v.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMB020G.sprDetailDef.SUM.ColNo))))
                suryo = suryo + Convert.ToDecimal(Me._LMBconG.FormatNumValue(v.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMB020G.sprDetailDef.SURYO.ColNo))))
                juryo = juryo + System.Math.Round(Convert.ToDecimal(Me._LMBconG.FormatNumValue(v.GetCellValue(spr.Cells(i, LMB020G.sprDetailDef.JURYO.ColNo)))), LMB020C.JURYO_ROUND_POS)

            Next

            '個数合計、数量合計・重量の計算結果を設定
            If Me.SetSuryoData(frm, kosu, suryo, juryo) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 入荷小の個数・数量合計
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="kosu">個数合計</param>
    ''' <param name="suryo">数量合計</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetSuryoData(ByVal frm As LMB020F, ByVal kosu As Decimal, ByVal suryo As Decimal, ByVal juryo As Decimal) As Boolean

        Dim g As LMBControlG = Me._LMBconG
        Dim v As LMBControlV = Me._LMBconV

        '個数範囲チェック
        If Me._V.IsCalcOver((kosu).ToString(), LMB020C.KOSU_MIN, LMB020C.KOSU_MAX, frm.lblTitleSumCnt.TextValue) = False Then
            Me._LMBconV.SetErrorControl(frm.numSumCnt)
            Return False
        End If

        '数量範囲チェック
        If Me._V.IsCalcOver((suryo).ToString(), LMB020C.SURYO_MIN, LMB020C.SURYO_MAX, frm.lblTitleSuryo.TextValue) = False Then
            Me._LMBconV.SetErrorControl(frm.numSuryo)
            Return False
        Else
            If frm.numSuryo.ReadOnly = False Then
                frm.numSuryo.BackColor = Utility.LMGUIUtility.GetSystemInputBackColor
            End If
        End If

        '重量範囲チェック
        If Me._V.IsCalcOver((juryo).ToString(), LMB020C.JURYO_MIN, LMB020C.JURYO_MAX, frm.lblTitleTARE.TextValue) = False Then
            Me._LMBconV.SetErrorControl(frm.numTare)
            Return False
        Else
            If frm.numTare.ReadOnly = False Then
                frm.numTare.BackColor = Utility.LMGUIUtility.GetSystemInputBackColor
            End If
        End If

        '単純に計算した値を設定
        frm.numSumCnt.Value = kosu
        frm.numSuryo.Value = suryo
        frm.numTare.Value = juryo
        frm.numEntryCnt.Value = kosu

        Return True

    End Function

    ''' <summary>
    ''' value1 - value2の計算
    ''' </summary>
    ''' <param name="value1">値1</param>
    ''' <param name="value2">値2</param>
    ''' <returns>計算値</returns>
    ''' <remarks></remarks>
    Private Function SubtractData(ByVal value1 As String, ByVal value2 As String) As String

        Return (Convert.ToDecimal(Me._LMBconG.FormatNumValue(value1)) - Convert.ToDecimal(Me._LMBconG.FormatNumValue(value2))).ToString()

    End Function

    ''' <summary>
    ''' 実予在庫数量
    ''' </summary>
    ''' <param name="konsu">梱数</param>
    ''' <param name="irime">入目</param>
    ''' <returns>実予在庫数量</returns>
    ''' <remarks></remarks>
    Private Function SetZaikoSuData(ByVal konsu As String, ByVal irime As String) As String

        Return (Convert.ToDecimal(Me._LMBconG.FormatNumValue(konsu)) * Convert.ToDecimal(Me._LMBconG.FormatNumValue(irime))).ToString()

    End Function

    ''' <summary>
    ''' 実予在庫梱数
    ''' </summary>
    ''' <param name="konsu"></param>
    ''' <param name="irisu"></param>
    ''' <param name="hasu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetPoraZaiNb(ByVal konsu As String, ByVal irisu As String, ByVal hasu As String) As String

        Return (Convert.ToDecimal(Me._LMBconG.FormatNumValue(konsu)) * Convert.ToDecimal(Me._LMBconG.FormatNumValue(irisu)) _
               + Convert.ToDecimal(Me._LMBconG.FormatNumValue(hasu))).ToString()

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub StartAction(ByVal frm As LMB020F)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(frm)

    End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub EndAction(ByVal frm As LMB020F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 入荷中番のSEQを採番
    ''' </summary>
    ''' <returns>新規採番した値</returns>
    ''' <remarks></remarks>
    Private Function SetInkaMmaxSeq() As String

        Dim dt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_MAX_NO)
        Dim dr As DataRow = dt.NewRow()

        '最終行の入荷中番 + 1の値を設定
        Dim maxInkaMNo As String = Me.GetMaxSeq(LMB020C.TABLE_NM_MAX_NO, "INKA_NO_M", dt.Rows(dt.Rows.Count - 1).Item("INKA_NO_M").ToString(), False)
        dr.Item("INKA_NO_M") = maxInkaMNo
        dr.Item("MAX_INKA_NO_S") = LMB020C.MAEZERO

        '行の追加
        dt.Rows.Add(dr)

        Return maxInkaMNo

    End Function

    ''' <summary>
    ''' 複写時、入荷小番だけ初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaMmaxSeqCler()

        Dim dt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_MAX_NO)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            dr = dt.Rows(i)
            dr.Item("MAX_INKA_NO_S") = LMB020C.MAEZERO
        Next

    End Sub

    ''' <summary>
    ''' 入荷小番のSEQを採番
    ''' </summary>
    ''' <param name="inkaMNo">入荷中番</param>
    ''' <returns>新規採番した値</returns>
    ''' <remarks></remarks>
    Private Function SetInkaSmaxSeq(ByVal inkaMNo As String) As String

        Return Me.GetMaxSeq(LMB020C.TABLE_NM_MAX_NO, "MAX_INKA_NO_S", inkaMNo, True)

    End Function

    ''' <summary>
    ''' 新規SEQを採番
    ''' </summary>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="colNm">列名</param>
    ''' <param name="inkaMNo">入荷中番</param>
    ''' <param name="setFlg">採番した値を設定するかのフラグ</param>
    ''' <returns>SEQ</returns>
    ''' <remarks></remarks>
    Private Function GetMaxSeq(ByVal tblNm As String, ByVal colNm As String, ByVal inkaMNo As String, ByVal setFlg As Boolean) As String

        Dim dr As DataRow = Me._Ds.Tables(tblNm).Select(Me._G.SetInkaSSql(inkaMNo))(0)

        Dim oldSeq As String = dr.Item(colNm).ToString()
        Dim newSeq As Integer = Convert.ToInt32(oldSeq) + 1
        If setFlg = True Then
            dr.Item(colNm) = newSeq
        End If

        Return Me._G.SetZeroData(newSeq.ToString(), LMB020C.MAEZERO)

    End Function

    ''' <summary>
    ''' レコード番号を取得
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="value">行数</param>
    ''' <param name="colNo">列番号</param>
    ''' <returns>レコード番号</returns>
    ''' <remarks></remarks>
    Private Function GetRecNo(ByVal spr As SheetView, ByVal value As String, ByVal colNo As Integer) As Integer

        Return Convert.ToInt32(Me._LMBconV.GetCellValue(spr.Cells(Convert.ToInt32(value), colNo)))

    End Function

    ''' <summary>
    ''' 作業コントロール名のリストを生成
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>コントロール名のリスト</returns>
    ''' <remarks>
    ''' リスト
    ''' ①：隠し(PK)名
    ''' ②：ラベル名
    ''' ③：フラグ名
    ''' ④：隠し(UP_KBN)名
    ''' </remarks>
    Private Function GetSagyoCtlNm(ByVal objNm As String) As ArrayList

        GetSagyoCtlNm = New ArrayList()

        '後ろ2桁を取得
        Dim ctlNm As String = objNm.Substring(objNm.Length - 2, 2)

        '隠し(PK)名を設定
        GetSagyoCtlNm.Add(String.Concat(LMB020C.SAGYO_PK, ctlNm))

        'ラベル名を設定
        GetSagyoCtlNm.Add(String.Concat(LMB020C.SAGYO_NM, ctlNm))

        'フラグ名を設定
        GetSagyoCtlNm.Add(String.Concat(LMB020C.SAGYO_FL, ctlNm))

        '隠し(UP_KBN)名を設定
        GetSagyoCtlNm.Add(String.Concat(LMB020C.SAGYO_UP, ctlNm))

        'REMARK_SIJIを設定
        GetSagyoCtlNm.Add(String.Concat(LMB020C.SAGYO_RMK_SIJI, ctlNm))

        Return GetSagyoCtlNm

    End Function

    ''' <summary>
    ''' Enter処理の特殊フォーカス移動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="eventFlg">Enterの場合、True</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <remarks></remarks>
    Private Sub NextFocusedControl(ByVal frm As LMB020F, ByVal objNm As String, ByVal eventFlg As Boolean)

        With frm

            'Enter以外の場合、スルー
            If eventFlg = False Then
                Exit Sub
            End If

            'フォーカス位置で移動先を切り替える
            Select Case objNm
                Case .txtSerchGoodsCd.Name, .txtSerchGoodsNm.Name

                    '入荷(小)が表示されている場合
                    Dim spr As Win.Spread.LMSpread = .sprDetail
                    If 0 <> spr.ActiveSheet.Rows.Count Then

                        '1行目のLotにフォーカス

                        .ActiveControl = spr
                        spr.Focus()
                        spr.ActiveSheet.SetActiveCell(0, LMB020G.sprDetailDef.LOT_NO.ColNo)
                        Exit Sub

                    End If

            End Select

            'タブ移動
            Call Me.NextFocusedControl(frm, eventFlg)

        End With

    End Sub

    ''' <summary>
    ''' 次コントロールにフォーカス移動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="eventFlg">Enterボタンの場合、True</param>
    ''' <remarks></remarks>
    Private Sub NextFocusedControl(ByVal frm As LMB020F, ByVal eventFlg As Boolean)

        'Enter以外の場合、スルー
        If eventFlg = False Then
            Exit Sub
        End If

        frm.SelectNextControl(frm.ActiveControl, True, True, True, True)

    End Sub

    ''' <summary>
    ''' 入荷(小)全レコードをチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInkaSZenRecChk(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean

        '全行チェック
        If Me._V.IsInkaSZenRecChk(ds) = False Then

            '処理終了アクション(ロック制御により解除 2重処理になる)
            Call Me.EndAction(frm)

            '入荷(中)の詳細情報クリア
            Call Me._G.ClearInkaMControl()
            frm.sprDetail.CrearSpread()

            '入荷(中)の詳細情報表示
            Dim inkaMno As String = String.Empty

            'START YANAI 要望番号495
            If Me._Ds.Tables(LMB020C.TABLE_NM_ERR).Rows.Count = 0 Then
                Return False
            End If
            'END YANAI 要望番号495

            inkaMno = Me._Ds.Tables(LMB020C.TABLE_NM_ERR).Rows(0).Item("INKA_M_NO").ToString()
            Call Me._G.SetInkaMInforData(Me._Ds, -1, inkaMno)
            Dim inkaSno As String = String.Empty
            inkaSno = Me._Ds.Tables(LMB020C.TABLE_NM_ERR).Rows(0).Item("INKA_S_NO").ToString()
            If String.IsNullOrEmpty(inkaSno) = False Then

                '入荷(小)情報表示
                Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.SAVE, frm.lblKanriNoM.TextValue)
            End If

            'ERRデータテーブルを初期化
            Me._Ds.Tables(LMB020C.TABLE_NM_ERR).Clear()

            Return False

        End If

        Return True

    End Function

    '要望番号:1350 terakawa 2012.08.27 Start
    ''' <summary>
    ''' 同一置き場（同一商品・ロット）チェック
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <returns>OK または Close処理の場合:True　Cancelの場合:False</returns>
    ''' <remarks></remarks>
    Private Function GoodsLotCheck(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean

        '要望番号:1511 KIM 2012/10/12 START

        'Dim whCd As String = frm.cmbSoko.SelectedValue.ToString()
        'Dim sokoDrs As DataRow() = Me._LMBconV.SelectSokoListDataRow(whCd)
        'Dim goodsLotCheckYn As String = String.Empty
        'Dim serverChkFlg As Boolean = False
        'If 0 < sokoDrs.Length Then
        '    goodsLotCheckYn = sokoDrs(0).Item("GOODSLOT_CHECK_YN").ToString()
        'End If

        'If goodsLotCheckYn = "01" Then
        '    '同一置き場に同一商品・ロットがある場合ワーニング
        '    '重複チェック（画面側）
        '    '画面側チェックの場合、フラグはFalse
        '    If Me._V.IsGoodsLotChk(ds, serverChkFlg) = False Then
        '        Return False
        '    End If

        '    '重複チェック（サーバー側）
        '    ds = Me.ServerAccess(ds, "ChkGoodsLot")

        '    If ds.Tables(LMB020C.TABLE_NM_WORNING).Rows.Count > 0 Then
        '        'サーバー側チェックの場合、フラグはTrue
        '        serverChkFlg = True
        '        Return Me._V.IsWorningChk(ds, serverChkFlg)
        '    End If
        'End If

        '荷主明細マスタを参照し、チェック有無を確認する
        Dim nrsbrCd As String = frm.cmbEigyo.SelectedValue.ToString() '20160728 営業所コード追加
        Dim custCd As String = frm.txtCustCdL.TextValue()
        Dim custDetailDrs As DataRow() = Me._LMBconV.SelectCustDetailsListDataRow(nrsbrCd, custCd)
        Dim goodsLotCheckYn As String = String.Empty
        Dim chkFlg As Boolean = False
        Dim serverChkFlg As Boolean = False
        If 0 < custDetailDrs.Length Then

            For i As Integer = 0 To custDetailDrs.Length - 1
                If custDetailDrs(i).Item("SUB_KB").ToString().Equals("41") = True Then
                    chkFlg = True
                    Exit For
                End If
            Next

        End If

        If chkFlg = True Then
            '同一置き場に同一商品・ロットがある場合ワーニング
            '重複チェック（画面側）
            '画面側チェックの場合、フラグはFalse
            If Me._V.IsGoodsLotChk(ds, serverChkFlg) = False Then
                Return False
            End If

            '重複チェック（サーバー側）
            ds = Me.ServerAccess(ds, "ChkGoodsLot")

            If ds.Tables(LMB020C.TABLE_NM_WORNING).Rows.Count > 0 Then
                'サーバー側チェックの場合、フラグはTrue
                serverChkFlg = True
                Return Me._V.IsWorningChk(ds, serverChkFlg)
            End If
        End If

        '要望番号:1511 KIM 2012/10/12 END

        Return True

    End Function
    '要望番号:1350 terakawa 2012.08.27 End

    ''' <summary>
    ''' 入荷QRによる検品データの妥当性を確認する。
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsInkaQrCheck(ByVal frm As LMB020F _
                                 , ByVal ds As DataSet) As Boolean

        Dim IsUnmatchInkaQrGoods As Boolean = False

        Dim isCurrect As Boolean = _V.IsCurrectInkaQrData(ds, IsUnmatchInkaQrGoods)
        If (isCurrect = False) Then

            If (IsUnmatchInkaQrGoods) Then

                '　検品実績(入荷QR)を エクセルファイルとして出力する。
                Me.SetMessageStoreInkaQr(ds.Tables(LMB020C.TABLE_NM_INKA_SEQ_QR))
                If (IsMessageStoreExist()) Then
                    Me.MessageStoreDownload()
                End If
            End If

        End If


        Return isCurrect

    End Function




    ''' <summary>
    ''' Save処理時の確認メッセージを表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>OK または Close処理の場合:True　Cancelの場合:False</returns>
    ''' <remarks></remarks>
    Private Function SaveConfirmMessage(ByVal frm As LMB020F, ByVal actionType As LMB020C.ActionType) As Boolean

        '閉じる処理時は確認メッセージを表示しない
        If LMB020C.ActionType.CLOSE = actionType Then
            Return True
        End If

        '確認メッセージ表示
        Return True

    End Function

    ''' <summary>
    ''' 入荷(小)取得のSQL
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="inkaNoM">入荷(中)番号</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSqlSelectInkaS(ByVal frm As LMB020F, ByVal inkaNoM As String) As String

        Return String.Concat(" INKA_NO_L = '", frm.lblKanriNoL.TextValue, "' AND INKA_NO_M = '", inkaNoM, "' AND SYS_DEL_FLG = '0' ")

    End Function

    ''' <summary>
    ''' 入荷(中)の詳細情報を表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub SetInkaMInforData(ByVal frm As LMB020F, ByVal rowNo As Integer)

        With frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '入荷(中)の詳細情報クリア
            Call Me._G.ClearInkaMControl()

            'レコード番号を取得
            If .sprGoodsDef.ActiveSheet.RowCount = 0 Then
                Exit Sub
            End If
            Dim recNo As Integer = Convert.ToInt32(Me._LMBconV.GetCellValue(.sprGoodsDef.ActiveSheet.Cells(rowNo, LMB020G.sprGoodsDef.RECNO.ColNo)))

            '入荷(中)の詳細情報表示
            Call Me._G.SetInkaMInforData(Me._Ds, recNo)

            '入荷(小)情報表示
            Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.DOUBLECLICK, .lblKanriNoM.TextValue)

            '重量合計再計算
            Call Me.SetJuryoReData(frm)

        End With

    End Sub

    ''' <summary>
    ''' 合計重量を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetJuryoReData(ByVal frm As LMB020F) As Boolean

        With frm

            Dim juryo As Decimal = 0
            Dim max As Integer = frm.sprDetail.ActiveSheet.Rows.Count - 1
            Dim spr As SheetView = frm.sprDetail.ActiveSheet
            Dim v As LMBControlV = Me._LMBconV

            For i As Integer = 0 To max

                '重量を加算
                juryo = juryo + Convert.ToDecimal(Me._LMBconG.FormatNumValue(v.GetCellValue(spr.Cells(i, LMB020G.sprDetailDef.JURYO.ColNo))))

            Next

            frm.numTare.Value = juryo

        End With

        Return True

    End Function

    ''' <summary>
    ''' 更新処理のサーバアクセスメソッドを取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>メソッド名</returns>
    ''' <remarks></remarks>
    Private Function GetUpdateAction(ByVal frm As LMB020F) As String

        GetUpdateAction = String.Empty

        Select Case frm.lblEdit.TextValue

            Case LMB020C.ActionType.DATEEDIT.ToString()

                GetUpdateAction = "UpdateSaveDateAction"

            Case LMB020C.ActionType.UNSOEDIT.ToString()

                GetUpdateAction = "UpdateSaveUnsoAction"

            Case Else

                GetUpdateAction = "UpdateSaveAction"

        End Select

        Return GetUpdateAction

    End Function

    ''' <summary>
    ''' 入荷(中)スプレッドの中の管理番号のRowIndexを返却
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetInkaNoM(ByVal frm As LMB020F, ByVal inkaNoM As String) As Integer

        Dim max As Integer = frm.sprGoodsDef.ActiveSheet.Rows.Count - 1
        For i As Integer = 0 To max
            If inkaNoM.Equals(Me._LMBconV.GetCellValue(frm.sprGoodsDef.ActiveSheet.Cells(i, LMB020G.sprGoodsDef.KANRI_NO.ColNo))) = True Then
                Return i
            End If
        Next

        Return -1

    End Function

    ''' <summary>
    ''' 商品情報明細のスクロールバーを最終行に設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetEndScrollGoods(ByVal frm As LMB020F)

        Call Me.SetEndScroll(frm.sprGoodsDef, True)

    End Sub

    ''' <summary>
    ''' 入荷(小)明細のスクロールバーを最終行に設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetEndScrollDetail(ByVal frm As LMB020F)

        Call Me.SetEndScroll(frm.sprDetail, False)

    End Sub

    ''' <summary>
    ''' 明細のスクロールバーを最終行に設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="setFlg">アクティブセルを設定するかのフラグ</param>
    ''' <remarks></remarks>
    Private Sub SetEndScroll(ByVal spr As Win.Spread.LMSpread, ByVal setFlg As Boolean)

        With spr

            Dim maxRow As Integer = .ActiveSheet.Rows.Count - 1
            If maxRow < 0 Then
                Exit Sub
            End If

            spr.SetViewportTopRow(0, maxRow)

            If setFlg = True Then

                spr.ActiveSheet.SetActiveCell(maxRow, 0)

            End If

        End With

    End Sub

    ''' <summary>
    ''' 商品選択時の戻り(作業)を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetRtnGoodsDataAtSagyo(ByVal frm As LMB020F, ByVal dr As DataRow)

        With frm

            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()
            Dim custLCd As String = .txtCustCdL.TextValue

            '作業1に設定
            Call Me.SetRtnGoodsDataAtSagyo(brCd, custLCd, dr.Item("INKA_KAKO_SAGYO_KB_1").ToString(), .txtSagyoCdM1, .lblSagyoNmM1, .txtSagyoRemarkM1)

            '作業2に設定
            Call Me.SetRtnGoodsDataAtSagyo(brCd, custLCd, dr.Item("INKA_KAKO_SAGYO_KB_2").ToString(), .txtSagyoCdM2, .lblSagyoNmM2, .txtSagyoRemarkM2)

            '作業3に設定
            Call Me.SetRtnGoodsDataAtSagyo(brCd, custLCd, dr.Item("INKA_KAKO_SAGYO_KB_3").ToString(), .txtSagyoCdM3, .lblSagyoNmM3, .txtSagyoRemarkM3)

            '作業4に設定
            Call Me.SetRtnGoodsDataAtSagyo(brCd, custLCd, dr.Item("INKA_KAKO_SAGYO_KB_4").ToString(), .txtSagyoCdM4, .lblSagyoNmM4, .txtSagyoRemarkM4)

            '作業5に設定
            Call Me.SetRtnGoodsDataAtSagyo(brCd, custLCd, dr.Item("INKA_KAKO_SAGYO_KB_5").ToString(), .txtSagyoCdM5, .lblSagyoNmM5, .txtSagyoRemarkM5)

        End With

    End Sub

    ''' <summary>
    ''' 商品選択時の戻り(作業)を設定
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="sagyoCd">作業コード</param>
    ''' <param name="txtCtl">コードコントロール</param>
    ''' <param name="lblCtl">名称コントロール</param>
    ''' <remarks></remarks>
    Private Sub SetRtnGoodsDataAtSagyo(ByVal brCd As String _
                                       , ByVal custLCd As String _
                                       , ByVal sagyoCd As String _
                                       , ByVal txtCtl As Win.InputMan.LMImTextBox _
                                       , ByVal lblCtl As Win.InputMan.LMImTextBox _
                                       , ByVal txtRmk As Win.InputMan.LMImTextBox _
                                       )

        '名称をキャッシュから取得
        Dim sagyoNm As String = String.Empty
        Dim sagyoRmk As String = String.Empty
        'START YANAI 要望番号376
        'Dim drs As DataRow() = Me._LMBconV.SelectSagyoListDataRow(brCd, sagyoCd, custLCd)
        Dim SelectSagyoString As String = String.Empty
        '削除フラグ
        SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")
        '作業コード
        SelectSagyoString = String.Concat(SelectSagyoString, " AND SAGYO_CD = '", sagyoCd, "' ")
        '営業所コード
        SelectSagyoString = String.Concat(SelectSagyoString, " AND NRS_BR_CD = '", brCd, "' ")
        '荷主コード
        SelectSagyoString = String.Concat(SelectSagyoString, " AND (CUST_CD_L = '", custLCd, "' OR CUST_CD_L = 'ZZZZZ')")

        Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(SelectSagyoString)
        'END YANAI 要望番号376
        If 0 < drs.Length Then
            sagyoNm = drs(0).Item("SAGYO_RYAK").ToString()
            sagyoRmk = drs(0).Item("WH_SAGYO_REMARK").ToString()
        End If

        '値を画面に設定
        txtCtl.TextValue = sagyoCd
        lblCtl.TextValue = sagyoNm
        txtRmk.TextValue = sagyoRmk
    End Sub

    'START YANAI 要望番号427
    ''' <summary>
    ''' スプレッドの複写元の行をチェックオン
    ''' </summary>
    ''' <param name="inkaNoS">入荷管理番号(小)</param>
    ''' <remarks></remarks>
    Friend Sub SetDefChk(ByVal frm As LMB020F, ByVal inkaNoS As String)

        With frm.sprDetail

            Dim max As Integer = .ActiveSheet.Rows.Count - 1
            Dim kanriNo As String = String.Empty

            For i As Integer = 0 To max
                kanriNo = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMB020G.sprDetailDef.KANRI_NO_S.ColNo))
                If (inkaNoS).Equals(kanriNo) = True Then
                    '値設定
                    .SetCellValue(i, LMB020G.sprDetailDef.DEF.ColNo, True.ToString())
                    '.SetCellValue(i, LMB020G.sprDetailDef.DEF.ColNo, LMConst.FLG.ON)
                    Exit For
                End If
            Next

        End With

    End Sub
    'END YANAI 要望番号427


    ''' <summary>
    ''' INKA_SのLOt NOにIAがあるかチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:IAあり, False:IAなし</returns>
    ''' <remarks></remarks>
    Private Function ChkInkaSLotIA(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean
        '区分マスタより対象ロットか判定用データ取得（２桁）
        Dim kbnDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "I010", "' AND KBN_CD = '01'"))
        Dim sChekRot As String = CStr(IIf(kbnDr.Length = 0, "", kbnDr(0).Item("KBN_NM1").ToString))

        'エラーになる場合があるので別インスタンス
        Dim inkaSDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S).Copy

        Dim sql As String = String.Concat(Me._G.SetInkaSSql(frm.lblKanriNoM.TextValue))
        Dim drs As DataRow() = inkaSDt.Select(sql, "INKA_NO_S")
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1

        With spr.ActiveSheet
            If -1 < max AndAlso drs.Count <> 0 Then

                For i As Integer = 0 To max
                    If Right(Me._LMBconV.GetCellValue(.Cells(i, LMB020G.sprDetailDef.LOT_NO.ColNo)).ToString().ToUpper(), 2) = sChekRot Then
                        Return True
                    End If

                Next

            End If

        End With

        Return False

    End Function
#End Region

#Region "文書管理起動"
    ''' <summary>
    ''' LMS文書管理(LMU010)起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ShowBunshoKanri(ByVal frm As LMB020F, ByVal prm As LMFormData, ByVal keyNo As String, ByVal keyTypeKbn As String)

        ''LMS文書管理(LMU010)画面の処理表示用データ設定
        Dim prmDs As DataSet = New LMU010DS
        Dim row As DataRow = prmDs.Tables(LMControlC.LMU010C_TABLE_NM_IN).NewRow
        row("ENT_SYSID_KBN") = "06"
        row("KEY_TYPE_KBN") = keyTypeKbn
        row("KEY_NO") = keyNo

        prmDs.Tables(LMControlC.LMU010C_TABLE_NM_IN).Rows.Add(row)
        prm.ParamDataSet = prmDs

        'LMS文書管理呼出
        LMFormNavigate.NextFormNavigate(Me, "LMU010", prm)

    End Sub
#End Region

    'ADD 2022/11/07 倉庫写真アプリ対応 START
#Region "写真選択起動"
    ''' <summary>
    ''' 写真選択(LMB070)起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ShowPhotoSel(ByVal frm As LMB020F, ByVal prm As LMFormData, ByVal rowNo As Integer, ByVal inkaL As String, ByVal inkaM As String, ByVal inkaS As String)

        '写真選択(LMB070)画面の処理表示用データ設定
        Dim prmDs As DataSet = New LMB070DS
        Dim row As DataRow = prmDs.Tables("LMB070IN").NewRow
        row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        row("INKA_NO_L") = inkaL
        row("INKA_NO_M") = inkaM
        row("INKA_NO_S") = inkaS

        If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then
            '参照モードの場合
            row("DISP_MODE") = DispMode.VIEW
        Else
            '編集モードの場合
            row("DISP_MODE") = String.Empty
        End If

        prmDs.Tables("LMB070IN").Rows.Add(row)

        '前回登録データ設定
        Dim inkaPDrs As DataRow() = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_PHOTO).Select(
            String.Concat("INKA_NO_M = '", inkaM, "' AND INKA_NO_S = '", inkaS, "'"), "NO ASC , FILE_PATH ASC")
        If inkaPDrs.Count > 0 Then
            For Each photorow In inkaPDrs
                Dim drow As DataRow = prmDs.Tables("LMB070IN_INKA_PHOTO").NewRow

                drow("NRS_BR_CD") = photorow("NRS_BR_CD")
                drow("INKA_NO_L") = photorow("INKA_NO_L")
                drow("INKA_NO_M") = photorow("INKA_NO_M")
                drow("INKA_NO_S") = photorow("INKA_NO_S")
                drow("NO") = photorow("NO")
                drow("SHOHIN_NM") = photorow("SHOHIN_NM")
                drow("SATSUEI_DATE") = photorow("SATSUEI_DATE")
                drow("USER_LNM") = photorow("USER_LNM")
                drow("SYS_UPD_DATE") = photorow("SYS_UPD_DATE")
                drow("SYS_UPD_TIME") = photorow("SYS_UPD_TIME")
                drow("FILE_PATH") = photorow("FILE_PATH")

                prmDs.Tables("LMB070IN_INKA_PHOTO").Rows.Add(drow)
            Next
        End If

        prm.ParamDataSet = prmDs

        '写真選択呼出
        LMFormNavigate.NextFormNavigate(Me, "LMB070", prm)

        If prm.ReturnFlg = True Then

            '前回登録データの入れ替え
            Dim spr As Win.Spread.LMSpread = frm.sprDetail
            Dim inkaSDrs As DataRow() = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(
                                        String.Concat("INKA_NO_M = '", inkaM, "' AND INKA_NO_S = '", inkaS, "'"))
            Dim delRowCnt As Integer = 0
            For Each photorow As DataRow In inkaPDrs
                '削除行の割出し
                delRowCnt = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_PHOTO).Rows.IndexOf(photorow)
                '行自体を削除
                Me._Ds.Tables(LMB020C.TABLE_NM_INKA_PHOTO).Rows(delRowCnt).Delete()
            Next

            If prm.ParamDataSet.Tables("LMB070OUT_INKA_PHOTO").Rows.Count = 0 Then
                inkaSDrs(0).Item("PHOTO_YN") = LMB020C.FLG_OFF
                spr.SetCellValue(rowNo, LMB020G.sprDetailDef.ENT_PHOTO.ColNo, String.Empty)
            Else
                inkaSDrs(0).Item("PHOTO_YN") = LMB020C.FLG_ON
                spr.SetCellValue(rowNo, LMB020G.sprDetailDef.ENT_PHOTO.ColNo, LMB020C.PHOTO_YN_NM)

                Dim dt As DataTable = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_PHOTO)
                Dim inkaPDrs2 As DataRow() = prm.ParamDataSet.Tables("LMB070OUT_INKA_PHOTO").Select()
                For Each row2 In inkaPDrs2
                    Dim drow As DataRow = dt.NewRow

                    drow("NRS_BR_CD") = row2("NRS_BR_CD")
                    drow("INKA_NO_L") = row2("INKA_NO_L")
                    drow("INKA_NO_M") = row2("INKA_NO_M")
                    drow("INKA_NO_S") = row2("INKA_NO_S")
                    drow("NO") = row2("NO")
                    drow("SHOHIN_NM") = row2("SHOHIN_NM")
                    drow("SATSUEI_DATE") = row2("SATSUEI_DATE")
                    drow("USER_LNM") = row2("USER_LNM")
                    drow("SYS_UPD_DATE") = row2("SYS_UPD_DATE")
                    drow("SYS_UPD_TIME") = row2("SYS_UPD_TIME")
                    drow("FILE_PATH") = row2("FILE_PATH")

                    dt.Rows.Add(drow)
                Next
            End If

        End If

    End Sub
#End Region
    'ADD 2022/11/07 倉庫写真アプリ対応 END

#Region "保管・荷役料最終計算日 による編集ボタンの有効無効判定"

    ''' <summary>
    ''' 保管・荷役料最終計算日 による編集ボタンの有効無効判定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    Private Function IsEnabledEditBtn(ByVal frm As LMB020F) As Boolean

        Dim ret As Boolean = True

        If Not frm.lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) Then
            Return frm.FunctionKey.F2ButtonEnabled
        End If
        If Me._DtHokanNiyakuCalculation Is Nothing Then
            Dim prmDs As DataSet = New LMB020DS()
            Dim dr As DataRow = prmDs.Tables("LMB020IN").NewRow

            dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            dr.Item("INKA_NO_L") = frm.lblKanriNoL.TextValue
            prmDs.Tables("LMB020IN").Rows.Add(dr)

            prmDs = Me.ServerAccess(prmDs, "SelectHokanNiyakuCalculation", "1")

            Me._DtHokanNiyakuCalculation = New DataTable
            Me._DtHokanNiyakuCalculation.Merge(prmDs.Tables(LMB020C.TABLE_NM_HOKAN_NIYAKU_CALCULATION))
        End If
        If Me._DtHokanNiyakuCalculation.Rows.Count() = 0 Then
            Return ret
        End If

        Dim drHokanNiyakuCalculationArr As DataRow()
        If Me._DtHokanNiyakuCalculation.Select("INKA_M_EXISTS = '1'").Count > 0 Then
            drHokanNiyakuCalculationArr = Me._DtHokanNiyakuCalculation.Select("INKA_M_EXISTS = '1'")
        Else
            drHokanNiyakuCalculationArr = Me._DtHokanNiyakuCalculation.Select("INKA_M_EXISTS = '0'")
        End If
        For Each drHokanNiyakuCalculation As DataRow In drHokanNiyakuCalculationArr
            Dim checkDate As String
            ' 保管料起算日 を変更した後は編集ボタンを押下できるようになる動作は妥当ではないため、比較対象は常に入荷日とする。
            'If drHokanNiyakuCalculation.Item("HOKAN_STR_DATE").ToString().Trim() = "" Then
            checkDate = drHokanNiyakuCalculation.Item("INKA_DATE").ToString()
            'Else
            '    checkDate = drHokanNiyakuCalculation.Item("HOKAN_STR_DATE").ToString()
            'End If
            Dim hokanNiyakuCalculation As String = drHokanNiyakuCalculation.Item("HOKAN_NIYAKU_CALCULATION").ToString()
            If hokanNiyakuCalculation >= checkDate Then
                ret = False
                Exit For
            End If
        Next

        Return ret

    End Function

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey1Press")

        Call Me.ShiftInsertStatus(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey2Press")

        Call Me.ShiftEditMode(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey2Press")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey3Press")

        Call Me.ShiftCopyMode(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey3Press")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey4Press")

        Call Me.DeleteAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey4Press")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し(検品取込)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '追加開始 2014.01.13 韓国CALT対応
        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey5Press")

        Call Me.KenpinTorikomiAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey5Press")
        '追加終了 2014.01.13 韓国CALT対応

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し(CSV)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey6Press")

        Call Me.TorikomiAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey6Press")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey7Press")

        Call Me.ShiftUnsoEditMode(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey7Press")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey8Press")

        Call Me.ShiftKisanbiEditMode(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey8Press")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey9Press")

        Call Me.AddInkaMData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        Call Me.SaveInkaItemData(frm, LMB020C.ActionType.SAVE)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMB020F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' フォームでKEYを押下時、発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub LMB020F_KeyDown(ByVal frm As LMB020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMB020F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMB020F_KeyDown")

    End Sub

    ''' <summary>
    ''' スプレッド(下部)でDoubleClickした時に発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprGoodsDef_CellDoubleClick(ByVal frm As LMB020F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprGoodsDef_CellDoubleClick")

        Call Me.RowSelection(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprGoodsDef_CellDoubleClick")

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprGoodsDef_LeaveCell(ByVal frm As LMB020F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprGoodsDef_LeaveCell")

        Call Me.RowLeaveCellSelection(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprGoodsDef_LeaveCell")

    End Sub

    ''' <summary>
    ''' 印刷ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnPrint_Click")

        'UPD 2017/06/29 アクサルタGHSラベル対応
        If frm.cmbPrint.SelectedValue.Equals("06") = False Then
            Call Me.PrintAction(frm)
        Else
            'GHSラベル
            Call Me.PrintGHSAction(frm)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnPrint_Click")

    End Sub

    ''' <summary>
    ''' 行削除(中)ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnRowDelM_Click(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnRowDelM_Click")

        Call Me.DeleteInkaMData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnRowDelM_Click")

    End Sub

    ''' <summary>
    ''' 行追加(中)ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnRowAddM_Click(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnRowAddM_Click")

        Call Me.AddInkaMData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnRowAddM_Click")

    End Sub

    ''' <summary>
    ''' 行削除(小)ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnRowDelS_Click(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnRowDelS_Click")

        Call Me.DeleteInkaSData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnRowDelS_Click")

    End Sub

    ''' <summary>
    ''' 行追加(小)ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnRowAddS_Click(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnRowAddS_Click")

        Call Me.AddInkaSData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnRowAddS_Click")

    End Sub

    ''' <summary>
    ''' 行複写(小)ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnRowCopyS_Click(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnRowCopyS_Click")

        Call Me.AddInkaSData(frm, True)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnRowCopyS_Click")

    End Sub

    ''' <summary>
    ''' 分析票ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnCoaAdd_Click(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnCoaAdd_Click")

        Call Me.AddCoaMst(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnCoaAdd_Click")

    End Sub

    ''' <summary>
    ''' イエローカードボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnYCardAdd_Click(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnYCardAdd_Click")

        Call Me.AddYCardMst(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnYCardAdd_Click")

    End Sub

    ''' <summary>
    ''' 運送有無の値変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub cmbUnchinUmu_SelectedValueChanged(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbUnchinUmu_SelectedValueChanged")

        Call Me.SetUnchinUmuInitCd(frm)

        Call Me.SetLockControl()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbUnchinUmu_SelectedValueChanged")

    End Sub

    ''' <summary>
    ''' 運送手配の値変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub cmbUnchinKbn_SelectedValueChanged(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbUnchinKbn_SelectedValueChanged")

        Call Me.SetTariffInitCd(frm)

        'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
        '運送会社コードOLD設定
        Me._G.SetUnsoCdOld(frm)

        '運賃タリフセットからタリフコードを設定
        Call Me._G.GetUnchinTariffSet(frm, True)
        'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
        Call Me.SetShiharaiTariffInitCd(frm)
        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End


        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbUnchinKbn_SelectedValueChanged")

    End Sub

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Function sprDetail_Change(ByVal frm As LMB020F) As Boolean

        Logger.StartLog(MyBase.GetType.Name, "sprDetail_Change")

        '個数の計算処理
        If Me.SetKosuData(frm, "sprDetail") = False Then
            Logger.EndLog(MyBase.GetType.Name, "sprDetail_Change")
            Return False
        End If

        '数量の計算処理
        If Me.SetSuryoData(frm, "sprDetail") = False Then
            Logger.EndLog(MyBase.GetType.Name, "sprDetail_Change")
            Return False
        End If

        '重量の計算処理
        If Me.SetJuryoData(frm, "sprDetail") = False Then
            Logger.EndLog(MyBase.GetType.Name, "sprDetail_Change")
            Return False
        End If

        '合計個数・数量・重量の計算処理
        If Me.SetSumData(frm, "sprDetail") = False Then
            Logger.EndLog(MyBase.GetType.Name, "sprDetail_Change")
            Return False
        End If

        Logger.EndLog(MyBase.GetType.Name, "sprDetail_Change")
        Return True

    End Function

    'ADD 2022/11/07 倉庫写真アプリ対応 START
    ''' <summary>
    ''' スプレッドのセルダブルクリックイベント
    ''' </summary>
    ''' <param name="frm"></param>
    Friend Sub sprDetail_CellDoubleClick(ByVal frm As LMB020F)

        Dim prm As LMFormData = New LMFormData()
        Dim prmDs As DataSet = New LMB080DS
        Dim inkaNoS As String = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(
                                                            frm.sprDetail.ActiveSheet.ActiveRowIndex,
                                                            LMB020G.sprDetailDef.KANRI_NO_S.ColNo))

        Dim inkaPDrs As DataRow() = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_PHOTO).Select(
                        String.Concat("INKA_NO_M = '", frm.lblKanriNoM.TextValue, "' AND INKA_NO_S = '", inkaNoS, "'"),
                        "NO ASC , FILE_PATH ASC")

        '登録済み画像データが存在する場合
        If inkaPDrs.Count > 0 Then

            For Each photorow In inkaPDrs
                Dim drow As DataRow = prmDs.Tables("LMB080IN").NewRow

                drow("NRS_BR_CD") = photorow("NRS_BR_CD")
                drow("INKA_NO_L") = photorow("INKA_NO_L")
                drow("INKA_NO_M") = photorow("INKA_NO_M")
                drow("INKA_NO_S") = photorow("INKA_NO_S")
                drow("NO") = photorow("NO")
                drow("SHOHIN_NM") = photorow("SHOHIN_NM")
                drow("SATSUEI_DATE") = photorow("SATSUEI_DATE")
                drow("USER_LNM") = photorow("USER_LNM")
                drow("FILE_PATH") = photorow("FILE_PATH")

                prmDs.Tables("LMB080IN").Rows.Add(drow)
            Next

            '登録済み画像照会画面の表示
            prm.ParamDataSet = prmDs
            LMFormNavigate.NextFormNavigate(Me, "LMB080", prm)

        End If

    End Sub
    'ADD 2022/11/07 倉庫写真アプリ対応 END

    ''' <summary>
    ''' 一括変更ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnHenko_Click(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnHenko_Click")

        Call Me.IkkatsuHenkoAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnHenko_Click")

    End Sub

    ''' <summary>
    ''' 一括変更ボタン押下イベント（置場）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnAllChange_Click(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnAllChange_Click")

        Call Me.ChangeToshitsuZone(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnAllChange_Click")

    End Sub

    'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
    ''' <summary>
    ''' 運送会社コードの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub txtUnsoCd_Leave(ByRef frm As LMB020F)

        Logger.StartLog(Me.GetType.Name, "txtUnsoCd_Leave")

        If (frm.txtUnsoCd.TextValue).Equals(frm.txtUnsoCdOld.TextValue) = False OrElse _
            (frm.txtTrnBrCD.TextValue).Equals(frm.txtTrnBrCDOld.TextValue) = False Then
            Call Me.LeaveUnsoCdAction(frm)
        End If

        Logger.EndLog(Me.GetType.Name, "txtUnsoCd_Leave")

    End Sub
    'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

    'ADD GHSラベル対応 ラベル種類
    ''' <summary>
    ''' btnPri Select変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub cmbPrint_SelectedValueChanged(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbPrint_SelectedValueChanged")

        If frm.cmbPrint.SelectedValue.Equals("06") = True Then
            'GHSラベル時
            frm.cmbLabelTYpe.Visible = True
            frm.cmbLabelTYpe.SelectedValue = "01"

        Else
            'GHSラベル以外
            frm.cmbLabelTYpe.Visible = False

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbPrint_SelectedValueChanged")

    End Sub

    ''' <summary>
    ''' 実行押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnJIKKOU_Click(ByRef frm As LMB020F)

        Logger.StartLog(Me.GetType.Name, "btnJIKKOU_Click")

        Call Me.JikkouAction(frm)

        Logger.EndLog(Me.GetType.Name, "btnJIKKOU_Click")

    End Sub

    ''' <summary>
    ''' 画像登録ボタン押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnAddImg_Click(ByRef frm As LMB020F)

        Logger.StartLog(Me.GetType.Name, "btnAddImg_Click")

        Call Me.AddImgAction(frm)

        Logger.EndLog(Me.GetType.Name, "btnAddImg_Click")

    End Sub

    'ADD 2022/11/07 倉庫写真アプリ対応 START
    ''' <summary>
    ''' 写真選択ボタン押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnPhotoSel_Click(ByRef frm As LMB020F)

        Logger.StartLog(Me.GetType.Name, "btnPhotoSel_Click")

        Call Me.PhotoSelAction(frm)

        Logger.EndLog(Me.GetType.Name, "btnPhotoSel_Click")

    End Sub
    'ADD 2022/11/07 倉庫写真アプリ対応 END

    ''' <summary>
    ''' 現場作業取込ボタン押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnWHSagyoTorikomi_Click(ByRef frm As LMB020F)

        Logger.StartLog(Me.GetType.Name, "btnWHSagyoTorikomi_Click")

        Call Me.TabletTorikomiAction(frm)

        Logger.EndLog(Me.GetType.Name, "btnWHSagyoTorikomi_Click")

    End Sub

    'Add Start 2019/10/09 要望管理007373
    ''' <summary>
    ''' 出荷止クリック時処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub chkStopAlloc_Click(ByVal frm As LMB020F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "chkStopAlloc_Click")

        Call Me.StopAlloc(frm)

        Logger.EndLog(Me.GetType.Name, "chkStopAlloc_Click")

    End Sub
    'Add End   2019/10/09 要望管理007373

    ''' <summary>
    ''' ☆☆☆取込処理(CSV取込)☆☆☆
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub TorikomiData(ByVal frm As LMB020F)

        Dim rtDs As DataSet = Nothing 'CSV関連

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.CSVINPUT))

        '↓↓↓CSV読み込み↓↓↓
        'ファイルの取込
        Dim rtnFlg As Boolean = Me.GetCSVData(frm, Me._Ds, rtDs) '商品マスタキャッシュ取得まで
        If rtnFlg = False Then

            '処理終了アクション
            Call Me.EndAction(frm)

            ''ファンクションキーの設定
            Call Me._G.SetFunctionKey(LMB020C.ActionType.EDIT)

            '特殊モードを通常に設定
            frm.lblEdit.TextValue = String.Empty

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus(LMB020C.ActionType.EDIT, Me._Ds)

            'ログ出力
            MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikomiData")

            'フォーカスの設定
            Call Me._G.SetFoucus(LMB020C.ActionType.EDIT)

            Exit Sub

        End If

        If _Ds.Tables(LMB020C.TABLE_NM_CSV_DATA).Rows.Count = 0 Then
            'メッセージエリアの設定
            '2015.10.21 tusnehira add
            '英語化対応
            MyBase.ShowMessage(frm, "E656")
            'MyBase.ShowMessage(frm, "E469", New String() {"取込対象のデータ"})

            '処理終了アクション
            Call Me.EndAction(frm)

            ''ファンクションキーの設定
            Call Me._G.SetFunctionKey(LMB020C.ActionType.EDIT)

            '特殊モードを通常に設定
            frm.lblEdit.TextValue = String.Empty

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus(LMB020C.ActionType.EDIT, Me._Ds)

            'ログ出力
            MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikomiData")

            'フォーカスの設定
            Call Me._G.SetFoucus(LMB020C.ActionType.EDIT)

            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "TorikomiData")

        '↑↑↑CSV読み込み↑↑↑

        rtnResult = rtnResult AndAlso Me._V.IsInkaMAddChk(LMB020C.ActionType.INIT_M)

        '明細向けのINKA_Mの最大件数を取得
        Dim maxInkaM As Integer = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows.Count - 1
        Dim inkaNoM As String = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows(maxInkaM).Item("INKA_NO_M").ToString()

        '要望対応:1955 terakawa 2013.03.16 Start
        Dim drInkaM As DataRow() = Nothing
        '要望対応:1955 terakawa 2013.03.16 End

        '入荷(中)の詳細情報表示
        Call Me.SetInitInkaMData(Me._Ds, inkaNoM)

        For i As Integer = 0 To rtDs.Tables("LMB020_CSV_DATA").Rows.Count - 1
            '要望対応:1955 terakawa 2013.03.16 Start
            drInkaM = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M) _
                      .Select("GOODS_CD_CUST = '" + rtDs.Tables("LMB020_CSV_DATA").Rows(i).Item("GOODS_CD_CUST").ToString() + "'")

            rtDs.Tables("LMB020_CSV_DATA").Rows(i).Item("INKA_NO_M") = drInkaM(drInkaM.Length - 1).Item("INKA_NO_M")
            rtDs.Tables("LMB020_CSV_DATA").Rows(i).Item("INKA_NO_S") = Me.SetInkaSmaxSeq(drInkaM(drInkaM.Length - 1).Item("INKA_NO_M").ToString)

            'rtDs.Tables("LMB020_CSV_DATA").Rows(i).Item("INKA_NO_M") = inkaNoM
            'rtDs.Tables("LMB020_CSV_DATA").Rows(i).Item("INKA_NO_S") = Me.SetInkaSmaxSeq(inkaNoM)
            '要望対応:1955 terakawa 2013.03.16 End
        Next

        If String.IsNullOrEmpty(inkaNoM) = False Then
            'rtDsからMe._DsのINKA_NO_Sへ全て渡すメソッド★★★
            Call Me.setInkaNoS(rtDs, Me._Ds)
            '入荷(小)情報表示
            Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.INIT, inkaNoM)
        End If

        '計算処理
        rtnResult = Me.AllCalculation(frm)

        ''処理終了アクション(ロック制御により解除)
        Call Me.EndAction(frm)

        ''ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMB020C.ActionType.EDIT)

        '特殊モードを通常に設定
        frm.lblEdit.TextValue = String.Empty

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMB020C.ActionType.EDIT, Me._Ds)


        ' '' ''Dim inkaNoM As String = frm.lblKanriNoM.TextValue
        ' '' ''If String.IsNullOrEmpty(inkaNoM) = False Then

        ' '' ''明細向けのINKA_Mの最大件数を取得
        '' ''Dim maxInkaM As Integer = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows.Count - 1


        ' '' ''入荷管理番号Mの最大値を取得。
        '' ''Dim inkaMNo As String = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows(maxInkaM).Item("INKA_NO_M").ToString()
        '' ''Dim dt As DataTable = rtDs.Tables(LMB020C.TABLE_NM_CSV_DATA)

        '' ''For I As Integer = 0 To dt.Rows.Count - 1
        '' ''    dt.Rows(I).Item("INKA_NO_M") = inkaMNo
        '' ''Next
        ' '' ''入荷(小)情報表示
        '' ''Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.DOUBLECLICK, inkaMNo)
        ' '' ''入荷(小)情報表示
        '' ''Me.AddInkaSData(frm, inkaMNo) '( Private Function SetNbDataSagyoのsetPkgNb =convertで落ちる。EXEはなんともない★)
        ' '' ''Me._G.SetInkaSData_CSV(dt, Me._Ds, inkaMNo)

        ' '' ''End If

        ' '' ''If rtnResult = True Then

        ' '' ''    '検索条件クリア
        ' '' ''    frm.txtSerchGoodsCd.TextValue = String.Empty
        ' '' ''    frm.txtSerchGoodsNm.TextValue = String.Empty

        '    'スクロールバーを一番下に設定
        Call Me.SetEndScrollGoods(frm)
        Call Me.SetEndScrollDetail(frm)

        'End If

        '↓↓↓CSV読み込み↓↓↓

        ''証明再表示がらみ
        'Dim inkaMNo As String = "001".ToString
        'Dim dt As DataTable = rtDs.Tables(LMB020C.TABLE_NM_CSV_DATA)
        ''入荷(小)情報表示
        'Me._G.SetInkaSData_CSV(dt, Me._Ds, inkaMNo)

        ''ロック制御
        'Call Me._G.SetControlsStatus(LMB020C.ActionType.EDIT, Me._Ds)


        ''処理終了アクション
        'Call Me.EndAction(frm)

        'メッセージエリアの設定
        '2015.10.21 tusnehira add
        '英語化対応
        MyBase.ShowMessage(frm, "G069")
        'MyBase.ShowMessage(frm, "G002", New String() {"取込処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikomiData")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMB020C.ActionType.EDIT)

        Me._Ds.Tables("LMB020_CSV_DATA").Clear()
        '↑↑↑CSV読み込み↑↑↑


    End Sub

    '2013.07.16 追加START
    ''' <summary>
    ''' ☆☆☆取込処理(入荷検品WK取込)☆☆☆
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function TorikomiKenpinData(ByVal frm As LMB020F, ByVal prmDs As DataSet, ByVal flgMMerge As String) As Boolean
        'Private Sub TorikomiKenpinData(ByVal frm As LMB020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me.SetAuthorityMessage(frm, Me._V.IsAuthority(LMB020C.ActionType.CSVINPUT))

        '↓↓↓入荷検品WK読み込み↓↓↓
        '入荷検品WKテーブルの取込
        Dim rtnFlg As Boolean = Me.GetKenpinData(frm, Me._Ds, prmDs, flgMMerge)

        'リターンフラグチェック
        If rtnFlg = False Then

            '処理終了アクション
            Call Me.EndAction(frm)

            ''ファンクションキーの設定
            Call Me._G.SetFunctionKey(LMB020C.ActionType.EDIT)

            '特殊モードを通常に設定
            frm.lblEdit.TextValue = String.Empty

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus(LMB020C.ActionType.EDIT, Me._Ds)

            'ログ出力
            MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikomiKenpinData")

            'フォーカスの設定
            Call Me._G.SetFoucus(LMB020C.ActionType.EDIT)

            Exit Function

        End If

        '検品データZERO件チェック
        If _Ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA).Rows.Count = 0 Then
            'メッセージエリアの設定
            '2015.10.21 tusnehira add
            '英語化対応
            MyBase.ShowMessage(frm, "E656")
            'MyBase.ShowMessage(frm, "E469", New String() {"取込対象のデータ"})

            '処理終了アクション
            Call Me.EndAction(frm)

            ''ファンクションキーの設定
            Call Me._G.SetFunctionKey(LMB020C.ActionType.EDIT)

            '特殊モードを通常に設定
            frm.lblEdit.TextValue = String.Empty

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus(LMB020C.ActionType.EDIT, Me._Ds)

            'ログ出力
            MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikomiKenpinData")

            'フォーカスの設定
            Call Me._G.SetFoucus(LMB020C.ActionType.EDIT)

            Exit Function
        End If

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "TorikomiKenpinData")

        rtnResult = rtnResult AndAlso Me._V.IsInkaMAddChk(LMB020C.ActionType.INIT_M)

        '明細向けのINKA_Mの最大件数を取得
        Dim maxInkaM As Integer = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows.Count - 1
        Dim inkaNoM As String = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows(maxInkaM).Item("INKA_NO_M").ToString()

        Dim drInkaM As DataRow() = Nothing

        '入荷(中)の詳細情報表示
        Call Me.SetInitInkaMData(Me._Ds, inkaNoM)

        '20414.07.29 Ri [ｱｸﾞﾘマート対応] Add　-ST-
        Dim svMNo As String = String.Empty
        Dim pvMNo As String = String.Empty
        Dim svSpeciKey As String = String.Empty
        Dim pvSpeciKey As String = String.Empty
        Dim sNo As String = String.Empty
        Dim kpWkRows() As DataRow = Nothing
        Dim sotDt As DataTable = Nothing
        Dim sotRows() As DataRow = Nothing
        '20414.07.29 Ri [ｱｸﾞﾘマート対応] Add　-ED-

        Select Case flgMMerge
            Case "1"
                '管理番号採番処理前にソートをかける
                sotDt = Me._Ds.Tables("LMB020_KENPIN_DATA").Copy
#If False Then ' JT物流入荷検品対応 20160726 changed inoue
                sotRows = sotDt.Select(String.Empty, "GOODS_CD_NRS ASC, TOU_NO ASC, SITU_NO ASC, ZONE_CD ASC, LOCA ASC, LOT_NO ASC")
#ElseIf False Then ' フィルメニッヒ入荷検品対応 20170310 changed by inoue 
                sotRows = sotDt.Select(String.Empty, "GOODS_CD_NRS ASC, TOU_NO ASC, SITU_NO ASC, ZONE_CD ASC, LOCA ASC, LOT_NO ASC, GOODS_CRT_DATE ASC")
#Else
                sotRows = sotDt.Select(String.Empty, "GOODS_CD_NRS ASC, TOU_NO ASC, SITU_NO ASC, ZONE_CD ASC, LOCA ASC, LOT_NO ASC, GOODS_CRT_DATE ASC, LT_DATE ASC ")
#End If
                Me._Ds.Tables("LMB020_KENPIN_DATA").Clear()
                For Each Row As DataRow In sotRows
                    Me._Ds.Tables("LMB020_KENPIN_DATA").ImportRow(Row)
                Next
            Case Else
                '処理なし
        End Select

#If True Then ' JT物流入荷検品対応 20160726 added inoue
        ' 製造日の取込有無指定
        Dim isSetGoodsCrtDate As Boolean = Me.IsReadGoodsCrtDate()
#End If

        For i As Integer = 0 To Me._Ds.Tables("LMB020_KENPIN_DATA").Rows.Count - 1

            '2014.07.29 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ST-
            'drInkaM = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M) _
            '          .Select("GOODS_CD_NRS = '" + Me._Ds.Tables("LMB020_KENPIN_DATA").Rows(i).Item("GOODS_CD_NRS").ToString() + "'")

            'Me._Ds.Tables("LMB020_KENPIN_DATA").Rows(i).Item("INKA_NO_M") = drInkaM(drInkaM.Length - 1).Item("INKA_NO_M")
            'Me._Ds.Tables("LMB020_KENPIN_DATA").Rows(i).Item("INKA_NO_S") = Me.SetInkaSmaxSeq(drInkaM(drInkaM.Length - 1).Item("INKA_NO_M").ToString)

            Select Case flgMMerge
                Case "1"

                    pvSpeciKey = String.Concat(Me._Ds.Tables("LMB020_KENPIN_DATA").Rows(i).Item("TOU_NO"), _
                                               Me._Ds.Tables("LMB020_KENPIN_DATA").Rows(i).Item("SITU_NO"), _
                                               Me._Ds.Tables("LMB020_KENPIN_DATA").Rows(i).Item("ZONE_CD"), _
                                               Me._Ds.Tables("LMB020_KENPIN_DATA").Rows(i).Item("LOCA"), _
                                               Me._Ds.Tables("LMB020_KENPIN_DATA").Rows(i).Item("LOT_NO")
                                              )
#If False Then ' JT物流入荷検品対応 20160726 changed inoue
#ElseIf False Then ' フィルメニッヒ入荷検品対応 20170310 changed by inoue 
#Else

                    Dim kenpinWkRow As DataRow = Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA).Rows(i)

                    ' 製造日をマージ条件に追加
                    pvSpeciKey = String.Concat(pvSpeciKey, "-", kenpinWkRow.Item("GOODS_CRT_DATE"))

                    '消費有効期限をマージ条件に追加
                    pvSpeciKey = String.Concat(pvSpeciKey, "-", kenpinWkRow.Item("LT_DATE"))

                    Dim rows As IEnumerable(Of DataRow) = Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_WK_DATA).AsEnumerable() _
                                                            .Where(Function(row) row.Item("GOODS_CD_NRS").Equals(kenpinWkRow.Item("GOODS_CD_NRS")) AndAlso _
                                                                                 row.Item("TOU_NO").Equals(kenpinWkRow.Item("TOU_NO")) AndAlso _
                                                                                 row.Item("SITU_NO").Equals(kenpinWkRow.Item("SITU_NO")) AndAlso _
                                                                                 row.Item("ZONE_CD").Equals(kenpinWkRow.Item("ZONE_CD")) AndAlso _
                                                                                 row.Item("LOCA").Equals(kenpinWkRow.Item("LOCA")) AndAlso _
                                                                                 row.Item("LOT_NO").Equals(kenpinWkRow.Item("LOT_NO")) AndAlso _
                                                                                 row.Item("GOODS_CRT_DATE").Equals(kenpinWkRow.Item("GOODS_CRT_DATE")) AndAlso _
                                                                                 row.Item("LT_DATE").Equals(kenpinWkRow.Item("LT_DATE")) AndAlso _
                                                                                 String.IsNullOrEmpty(row.Item("INKA_NO_M").ToString()) = False)

                    drInkaM = rows.ToArray()
#End If

                    '検品ワークをチェックして、なければINKA_Mより取得
                    If (drInkaM Is Nothing OrElse drInkaM.Count < 1) Then
                        drInkaM = Nothing
                        drInkaM = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Select("GOODS_CD_NRS = '" + Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA).Rows(i).Item("GOODS_CD_NRS").ToString() + "'")

                        ''S採番
                        'pvMNo = drInkaM(drInkaM.Length - 1).Item("INKA_NO_M").ToString()
                        'If Not svMNo.Equals(pvMNo) Then
                        '    svMNo = pvMNo

                        '    If Not svSpeciKey.Equals(pvSpeciKey) Then
                        '        svSpeciKey = pvSpeciKey
                        '        sNo = Me.SetInkaSmaxSeq(drInkaM(drInkaM.Length - 1).Item("INKA_NO_M").ToString)
                        '    End If

                        'End If
                    Else
                        'sNo = drInkaM(drInkaM.Length - 1).Item("INKA_NO_S").ToString()
                    End If

                    '============================================================================================
                    'S採番
                    pvMNo = drInkaM(drInkaM.Length - 1).Item("INKA_NO_M").ToString()
                    If Not svMNo.Equals(pvMNo) Then
                        svMNo = pvMNo

                        If Not String.IsNullOrEmpty(svSpeciKey) Then
                            pvSpeciKey = String.Empty
                        End If

                        If Not svSpeciKey.Equals(pvSpeciKey) Then
                            svSpeciKey = pvSpeciKey
                            sNo = Me.SetInkaSmaxSeq(drInkaM(drInkaM.Length - 1).Item("INKA_NO_M").ToString)
                        End If
                    Else
                        If Not svSpeciKey.Equals(pvSpeciKey) Then
                            svSpeciKey = pvSpeciKey
                            sNo = Me.SetInkaSmaxSeq(drInkaM(drInkaM.Length - 1).Item("INKA_NO_M").ToString)
                        End If
                    End If
                    '============================================================================================

                    '検品データMS設定
                    Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA).Rows(i).Item("INKA_NO_M") = drInkaM(drInkaM.Length - 1).Item("INKA_NO_M")
                    Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA).Rows(i).Item("INKA_NO_S") = sNo

                    '検品ワークデータLMS設定[一件しか返りません]
                    kpWkRows = Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_WK_DATA).Select(String.Concat("NRS_BR_CD = '", Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA).Rows(i).Item("NRS_BR_CD"), _
                                                                                                   "' AND WH_CD ='", Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA).Rows(i).Item("WH_CD"), _
                                                                                                   "' AND CUST_CD_L = '", Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA).Rows(i).Item("CUST_CD_L"), _
                                                                                                   "' AND INPUT_DATE ='", Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA).Rows(i).Item("INPUT_DATE"), _
                                                                                                   "' AND SEQ = '", Me._Ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA).Rows(i).Item("SEQ"), "'"))

                    kpWkRows(0).Item("INKA_NO_L") = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("INKA_NO_L")
                    kpWkRows(0).Item("INKA_NO_M") = drInkaM(drInkaM.Length - 1).Item("INKA_NO_M")
                    kpWkRows(0).Item("INKA_NO_S") = sNo

                Case Else
                    drInkaM = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Select("GOODS_CD_NRS = '" + Me._Ds.Tables("LMB020_KENPIN_DATA").Rows(i).Item("GOODS_CD_NRS").ToString() + "'")
                    Me._Ds.Tables("LMB020_KENPIN_DATA").Rows(i).Item("INKA_NO_M") = drInkaM(drInkaM.Length - 1).Item("INKA_NO_M")
                    Me._Ds.Tables("LMB020_KENPIN_DATA").Rows(i).Item("INKA_NO_S") = Me.SetInkaSmaxSeq(drInkaM(drInkaM.Length - 1).Item("INKA_NO_M").ToString)
            End Select
            '2014.07.29 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ST-
        Next

        If String.IsNullOrEmpty(inkaNoM) = False Then
            'rtDsからMe._DsのINKA_NO_Sへ全て渡すメソッド★★★

            '2014.07.29 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ST-
            'Call Me.setInkaNoSKenpin(prmDs, Me._Ds)
            Select Case flgMMerge
                Case "1"
                    Call Me.setInkaNoSKenpinMerge(prmDs, Me._Ds)
                Case Else
                    Call Me.setInkaNoSKenpin(prmDs, Me._Ds)
            End Select
            '2014.07.29 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ED-

            '入荷(小)情報表示
            Call Me._G.SetInkaSData(Me._Ds, LMB020C.ActionType.INIT, inkaNoM)
        End If

        '計算処理
        rtnResult = Me.AllCalculation(frm)

        ''処理終了アクション(ロック制御により解除)
        Call Me.EndAction(frm)

        ''ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMB020C.ActionType.EDIT)

        '特殊モードを通常に設定
        frm.lblEdit.TextValue = String.Empty

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMB020C.ActionType.EDIT, Me._Ds)

        '    'スクロールバーを一番下に設定
        Call Me.SetEndScrollGoods(frm)
        Call Me.SetEndScrollDetail(frm)

        'End If

        'メッセージエリアの設定
        '2015.10.21 tusnehira add
        '英語化対応
        MyBase.ShowMessage(frm, "G069")
        'MyBase.ShowMessage(frm, "G002", New String() {"取込処理", ""})


        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikomiKenpinData")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMB020C.ActionType.EDIT)

        Me._Ds.Tables("LMB020_KENPIN_DATA").Clear()
        '↑↑↑入荷検品WK読み込み↑↑↑


    End Function
    '2013.07.16 追加END

#Region "CSV読み取り関連"

    ''' <summary>
    ''' CSVファイル読取
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCSVData(ByVal frm As LMB020F, ByVal ds As DataSet, ByRef dsCsv As DataSet) As Boolean
        'Public Function GetCSVData(ByVal frm As LMC010F, ByRef csvDt As LMC010DS.LMC010_OUTKA_CSV_INDataTable) As Boolean


        Dim filePath As String = String.Empty
        Dim fileName As String = String.Empty

        'ファイルパス取得
        '取得先のCSVファイルのパス・ファイル名(20130215後でチェックDB項目)
        Dim kbnDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select( _
                                                  String.Concat("KBN_GROUP_CD = 'E034' AND KBN_CD = '01'"))
        If 0 < kbnDr.Length Then
            filePath = kbnDr(0).Item("KBN_NM1").ToString
            fileName = kbnDr(0).Item("KBN_NM2").ToString
        End If

        'ファイル存在チェック
        If Me._V.IsFileExist(filePath, fileName) = False Then
            Return False
        End If

        'CSVファイル読込み
        dsCsv = Me.SetCSVFromDatasetTo(ds, filePath, fileName)

        dsCsv = Me.IsExitsGoodsCheck(frm, dsCsv)


        Dim goodsDs As DataSet = New LMZ020DS
        goodsDs = Me.SetGoodsLMZ020(dsCsv)
        '
        'Me.AddInkaSData(frm, frm.lblKanriNoM.TextValue)
        Me.AddInkaMData(frm, goodsDs)
        'Me.AddCSV_M_data(frm, dsCsv)



        Return True

    End Function

    ''' <summary>
    ''' 商品マスタPOPUPのdsに合うようにコピー
    ''' </summary>
    ''' <param name="dsCsv"></param>
    ''' <remarks></remarks>    
    Private Function SetGoodsLMZ020(ByVal dsCsv As DataSet) As DataSet

        Dim max As Integer = dsCsv.Tables("LMB020_GOODS_NM").Rows.Count - 1
        'Dim dr As DataRow = dsCsv.Tables("LMB020_GOODS_NM").Rows(dsCsv.Tables("LMB020_GOODS_NM").Rows.Count - 1)

        Dim goodsDs As DataSet = New LMZ020DS
        Dim goodsRow As DataRow
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dsCsv.Tables("LMB020_GOODS_NM").Rows(i)
            goodsRow = goodsDs.Tables(LMZ020C.TABLE_NM_OUT).NewRow()

            goodsRow.Item("NRS_BR_CD") = dr.Item("NRS_BR_CD")
            goodsRow.Item("GOODS_NM_1") = dr.Item("GOODS_NM")
            goodsRow.Item("STD_IRIME_NM") = dr.Item("STD_IRIME_NB_M")
            goodsRow.Item("GOODS_CD_CUST") = dr.Item("GOODS_CD_CUST")
            goodsRow.Item("NB_UT_NM") = ("").ToString
            goodsRow.Item("NB_UT") = dr.Item("NB_UT")
            goodsRow.Item("PKG_NM") = ("").ToString
            goodsRow.Item("SEARCH_KEY_1") = ("").ToString
            goodsRow.Item("SEARCH_KEY_2") = ("").ToString
            goodsRow.Item("ONDO_KB_NM") = ("").ToString
            goodsRow.Item("ONDO_KB") = dr.Item("ONDO_KB")
            goodsRow.Item("SHOBO_CD") = dr.Item("SHOBO_CD")
            goodsRow.Item("CUST_NM_S") = ("").ToString
            goodsRow.Item("CUST_NM_SS") = ("").ToString
            goodsRow.Item("STD_IRIME_NB") = dr.Item("STD_IRIME_NB")
            goodsRow.Item("STD_IRIME_UT") = dr.Item("STD_IRIME_UT")
            goodsRow.Item("STD_IRIME_UT_NM") = ("").ToString
            goodsRow.Item("PKG_NB") = dr.Item("PKG_NB")
            goodsRow.Item("PKG_UT") = dr.Item("PKG_NB_UT1")
            goodsRow.Item("PKG_UT_NM") = ("").ToString
            goodsRow.Item("CUST_CD_S") = dr.Item("CUST_CD_S")
            goodsRow.Item("CUST_CD_SS") = dr.Item("CUST_CD_SS")
            goodsRow.Item("CUST_CD_L") = dr.Item("CUST_CD_L")
            goodsRow.Item("CUST_CD_M") = dr.Item("CUST_CD_M")
            goodsRow.Item("CUST_NM_L") = ("").ToString
            goodsRow.Item("CUST_NM_M") = ("").ToString
            goodsRow.Item("GOODS_CD_NRS") = dr.Item("GOODS_CD_NRS")
            goodsRow.Item("GOODS_NM_2") = ("").ToString
            goodsRow.Item("UP_GP_CD_1") = ("").ToString
            goodsRow.Item("KIKEN_KB") = ("").ToString
            goodsRow.Item("DOKU_KB") = ("").ToString
            goodsRow.Item("UNSO_ONDO_KB") = ("").ToString
            goodsRow.Item("ONDO_STR_DATE") = dr.Item("ONDO_STR_DATE")
            goodsRow.Item("ONDO_END_DATE") = dr.Item("ONDO_STR_DATE")
            goodsRow.Item("STD_WT_KGS") = dr.Item("STD_WT_KGS")
            goodsRow.Item("INKA_KAKO_SAGYO_KB_1") = ("").ToString
            goodsRow.Item("INKA_KAKO_SAGYO_KB_2") = ("").ToString
            goodsRow.Item("INKA_KAKO_SAGYO_KB_3") = ("").ToString
            goodsRow.Item("INKA_KAKO_SAGYO_KB_4") = ("").ToString
            goodsRow.Item("INKA_KAKO_SAGYO_KB_5") = ("").ToString
            goodsRow.Item("TARE_YN") = dr.Item("TARE_YN")
            goodsRow.Item("SP_NHS_YN") = ("").ToString
            goodsRow.Item("COA_YN") = ("").ToString
            goodsRow.Item("LOT_CTL_KB") = dr.Item("LOT_CTL_KB")
            goodsRow.Item("LT_DATE_CTL_KB") = dr.Item("LT_DATE_CTL_KB")
            goodsRow.Item("CRT_DATE_CTL_KB") = ("").ToString
            goodsRow.Item("SKYU_MEI_YN") = ("").ToString
            goodsRow.Item("HIKIATE_ALERT_YN") = ("").ToString


            goodsDs.Tables(LMZ020C.TABLE_NM_OUT).Rows.Add(goodsRow)

        Next

        Return goodsDs

    End Function

    ''' <summary>
    ''' CSVからデータセットへ設定 2013/02/14 追加
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>    
    Private Function SetCSVFromDatasetTo(ByVal ds As DataSet, ByVal filePath As String, ByVal filename As String) As DataSet

        Dim dr As DataRow
        Dim fileString As String = String.Empty
        Dim gyoCount As Integer = 0
        Dim stock As String = String.Empty
        Dim stockFlag As Boolean = False
        Dim kugiri As String = ","

        ' StreamReader の新しいインスタンスを生成する
        Dim cReader As New System.IO.StreamReader(String.Concat(filePath, filename), System.Text.Encoding.Default)

        '先頭行飛ばしカウントの数だけ行を読み込む
        cReader.ReadLine()

        ' 読み込みできる文字がなくなるまで繰り返す
        While (cReader.Peek() >= 0)

            ' ファイルを 1 行ずつ読み込む
            Dim stBuffer As String = cReader.ReadLine()
            dr = ds.Tables(LMB020C.TABLE_NM_CSV_DATA).NewRow()

            'カラムカウント
            Dim columnCount As Integer = 0

            For Each aryf As String In Split(stBuffer, kugiri)

                'ダブルクォーテーションで始まっている場合
                If Left(aryf, 1).Equals(Chr(34)) And Right(aryf, 1).Equals(Chr(34)) = False Then
                    stock = aryf
                    stockFlag = True
                    Continue For
                End If

                If stockFlag = True Then
                    'ダブルクォーテーションで閉じられている場合
                    If Right(aryf, 1).Equals(Chr(34)) Then
                        aryf = String.Concat(stock, kugiri, aryf)
                        stockFlag = False
                        stock = String.Empty

                        'ダブルクォーテーションで閉じられていない場合
                    Else
                        stock = String.Concat(stock, kugiri, aryf)
                        Continue For
                    End If
                End If

                'ダブルクォーテーションで囲まれていた場合は、除外した文字列をセットする
                If Left(aryf, 1).Equals(Chr(34)) And Right(aryf, 1).Equals(Chr(34)) Then
                    aryf = Mid(aryf, 2, Len(aryf) - 2)
                End If

                dr(columnCount) = aryf
                columnCount += 1
            Next

            ds.Tables(LMB020C.TABLE_NM_CSV_DATA).Rows.Add(dr)

        End While

        'cReader を閉じる
        cReader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 入荷(小)データ追加処理
    ''' </summary>
    ''' <param name="rtDs">rtDs</param>
    ''' <param name="ds">Me._Ds</param>
    ''' <remarks></remarks>
    Private Function setInkaNoS(ByVal rtDs As DataSet, ByVal ds As DataSet) As DataSet

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim rtDt As DataTable = rtDs.Tables("LMB020_CSV_DATA")
        Dim meDt As DataTable = ds.Tables("LMB020_INKA_S")

        Dim max As Integer = rtDt.Rows.Count - 1

        '要望番号:1955(入荷（小）の取り込みができない) 2013/03/22 START
        Dim maxCol As Integer = meDt.Columns.Count - 1

        For i As Integer = 0 To max

            Dim dr As DataRow = meDt.NewRow()
            'For j As Integer = 0 To max
            For j As Integer = 0 To maxCol
                dr.Item(j) = String.Empty
            Next
            '要望番号:1955(入荷（小）の取り込みができない) 2013/03/22 END
            Dim brCd As String = rtDs.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()
            dr.Item("NRS_BR_CD") = brCd
            dr.Item("INKA_NO_L") = rtDs.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L").ToString()
            dr.Item("INKA_NO_M") = rtDt.Rows(i).Item("INKA_NO_M").ToString()
            dr.Item("INKA_NO_S") = rtDt.Rows(i).Item("INKA_NO_S").ToString()
            Dim sql As String = String.Concat("NRS_BR_CD = '", brCd, "' " _
                                              , " AND INKA_NO_L = '", rtDs.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L").ToString(), "' " _
                                              , " AND INKA_NO_M = '", rtDt.Rows(i).Item("INKA_NO_M").ToString(), "' " _
                                              )
            Dim inkaMDr As DataRow = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(sql)(0)

            Dim irime As String

            If "0".Equals(rtDt.Rows(i).Item("IRIME").ToString()) = True Then
                irime = ds.Tables("LMB020_GOODS_NM").Rows(0).Item("STD_IRIME_NB").ToString()
            Else
                irime = rtDt.Rows(i).Item("IRIME").ToString() 'CSV側入目
            End If

            dr.Item("IRIME") = irime

            ''Dim irimeTani As String = rtDt.Rows(i).Item("STD_IRIME_UT").ToString()
            ''Dim juryo As String = ds.Tables("LMB020_GOODS_NM").Rows(0).Item("STD_WT_KGS").ToString()
            Dim csvIrime As Decimal = Convert.ToDecimal(rtDt.Rows(i).Item("IRIME").ToString()) 'csv.入目
            Dim sprKosu As Decimal = Convert.ToDecimal(("1").ToString()) '個数(以前はcsv.個数)
            Dim stdWt As Decimal = Convert.ToDecimal(ds.Tables("LMB020_GOODS_NM").Rows(0).Item("STD_WT_KGS").ToString()) '商品M.標準重量
            Dim csvRow As Decimal = Convert.ToDecimal(rtDt.Rows.Count) 'CSV.行数
            Dim stdIrime As Decimal = Convert.ToDecimal(ds.Tables("LMB020_GOODS_NM").Rows(0).Item("STD_IRIME_NB").ToString()) '商品M.標準入目

            'SUM_KOSU
            Dim sumKosu As Decimal = 0
            sumKosu = sumKosu + sprKosu
            ds.Tables("LMB020_GOODS_NM").Rows(0).Item("SUM_KOSU") = sumKosu

            'SUM_JURYO_M 
            '1.csvから読み込んだ個数*入目の合計を入れる(それぞれの行を掛けて、合算)
            '2.入目がzeroなら、商品M.標準入り目を使う。
            Dim sumSuryoM As Decimal = 0

            If "0".Equals(rtDt.Rows(i).Item("IRIME").ToString()) = True Then
                sumSuryoM = sumSuryoM + (sprKosu * stdIrime) 'csvの入目がゼロの時
            Else
                sumSuryoM = sumSuryoM + (sprKosu * csvIrime)
            End If

            ds.Tables("LMB020_GOODS_NM").Rows(0).Item("SUM_SURYO_M") = sumSuryoM

            'SUM_JURYO_M = SUM(商品Mの標準重量*CSVの入目/商品M.標準入目*CSVの個数)
            Dim sumJuryoM As Decimal = sumJuryoM + (stdWt * csvIrime / stdIrime * csvRow)
            ds.Tables("LMB020_GOODS_NM").Rows(0).Item("SUM_JURYO_M") = sumJuryoM

            Dim irimeTani As String = inkaMDr.Item("STD_IRIME_UT").ToString()
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_I001, "' AND KBN_CD = '", irimeTani, "'"))
            Dim kbnNm As String = String.Empty
            If 0 < drs.Length Then

                '2017/09/25 修正 李↓
                kbnNm = drs(0).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString()
                '2017/09/25 修正 李↑

            End If

            dr.Item("LOT_NO") = rtDt.Rows(i).Item("LOT_NO").ToString()
            dr.Item("SERIAL_NO") = rtDt.Rows(i).Item("SERIAL_NO").ToString()
            dr.Item("REMARK_OUT") = rtDt.Rows(i).Item("REMARK_OUT").ToString()
            dr.Item("REMARK") = rtDt.Rows(i).Item("REMARK").ToString()
            dr.Item("TOU_NO") = rtDt.Rows(i).Item("LOC").ToString().Substring(0, 2)
            dr.Item("SITU_NO") = rtDt.Rows(i).Item("LOC").ToString().Substring(2, 1)  'ZONE_CDとの見分けが困難、室NOのほとんどが1バイトのための対応
            dr.Item("ZONE_CD") = rtDt.Rows(i).Item("LOC").ToString().Substring(3, 1)    'ZONE_CDとの見分けが困難、ゾーンCDのほとんどが1バイトのための対応
            dr.Item("STD_IRIME_NM") = kbnNm
            dr.Item("IRIME") = irime
            dr.Item("JURYO_S") = "0"
            dr.Item("ALLOC_PRIORITY") = LMB020C.WARIATE_FREE
            dr.Item("OFB_KB") = "01"
            dr.Item("SPD_KB") = "01"
            dr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
            dr.Item("UP_KBN") = LMConst.FLG.OFF
            dr.Item("LOT_CTL_KB") = inkaMDr.Item("LOT_CTL_KB").ToString()
            dr.Item("LT_DATE_CTL_KB") = inkaMDr.Item("LT_DATE_CTL_KB").ToString()
            dr.Item("CRT_DATE_CTL_KB") = inkaMDr.Item("CRT_DATE_CTL_KB").ToString()
            ''2013.07.16 追加START①
            'dr.Item("GOODS_CD_NRS") = inkaMDr.Item("GOODS_CD_NRS").ToString()
            'dr.Item("GOODS_CD_CUST") = inkaMDr.Item("GOODS_CD_CUST").ToString()
            ''2013.07.16 追加END①
            dr.Item("STD_WT_KGS") = inkaMDr.Item("STD_WT_KGS").ToString()
            dr.Item("STD_IRIME_UT") = inkaMDr.Item("STD_IRIME_UT").ToString()
            dr.Item("KONSU") = "1"
            dr.Item("KOSU_S") = "1"
            dr.Item("HASU") = "0"

            Dim inkaNoS As String = String.Empty

            ''行追加()
            meDt.Rows.Add(dr)


        Next

        Return ds

    End Function

#End Region '"CSV読み取り関連"

    '2013.07.16 追加START
#Region "入荷検品WK読取関連"

    ''' <summary>
    ''' 入荷検品WK読取
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKenpinData(ByVal frm As LMB020F, ByVal ds As DataSet, ByRef dsKpWk As DataSet, ByVal flgMMerge As String) As Boolean


        '入荷検品WK読込み
        ds = Me.SetKenpinDatasetTo(ds, dsKpWk, flgMMerge)

        '商品存在チェック
        ds = Me.IsExitsGoodsKenpinCheck(frm, ds)

        '商品マスタに検品商品情報を複写
        Dim goodsDs As DataSet = New LMZ020DS
        goodsDs = Me.SetGoodsLMZ020(ds)

        '入荷中作成
        Me.AddInkaMDataKpWk(frm, goodsDs, flgMMerge)

        Return True

    End Function

    ' ''' <summary>
    ' ''' 商品マスタPOPUPのdsに合うようにコピー
    ' ''' </summary>
    ' ''' <param name="dsCsv"></param>
    ' ''' <remarks></remarks>    
    'Private Function SetGoodsLMZ020(ByVal dsCsv As DataSet) As DataSet

    '    Dim max As Integer = dsCsv.Tables("LMB020_GOODS_NM").Rows.Count - 1

    '    Dim goodsDs As DataSet = New LMZ020DS
    '    Dim goodsRow As DataRow
    '    Dim dr As DataRow

    '    For i As Integer = 0 To max

    '        dr = dsCsv.Tables("LMB020_GOODS_NM").Rows(i)
    '        goodsRow = goodsDs.Tables(LMZ020C.TABLE_NM_OUT).NewRow()

    '        goodsRow.Item("NRS_BR_CD") = dr.Item("NRS_BR_CD")
    '        goodsRow.Item("GOODS_NM_1") = dr.Item("GOODS_NM")
    '        goodsRow.Item("STD_IRIME_NM") = dr.Item("STD_IRIME_NB_M")
    '        goodsRow.Item("GOODS_CD_CUST") = dr.Item("GOODS_CD_CUST")
    '        goodsRow.Item("NB_UT_NM") = ("").ToString
    '        goodsRow.Item("NB_UT") = dr.Item("NB_UT")
    '        goodsRow.Item("PKG_NM") = ("").ToString
    '        goodsRow.Item("SEARCH_KEY_1") = ("").ToString
    '        goodsRow.Item("SEARCH_KEY_2") = ("").ToString
    '        goodsRow.Item("ONDO_KB_NM") = ("").ToString
    '        goodsRow.Item("ONDO_KB") = dr.Item("ONDO_KB")
    '        goodsRow.Item("SHOBO_CD") = dr.Item("SHOBO_CD")
    '        goodsRow.Item("CUST_NM_S") = ("").ToString
    '        goodsRow.Item("CUST_NM_SS") = ("").ToString
    '        goodsRow.Item("STD_IRIME_NB") = dr.Item("STD_IRIME_NB")
    '        goodsRow.Item("STD_IRIME_UT") = dr.Item("STD_IRIME_UT")
    '        goodsRow.Item("STD_IRIME_UT_NM") = ("").ToString
    '        goodsRow.Item("PKG_NB") = dr.Item("PKG_NB")
    '        goodsRow.Item("PKG_UT") = dr.Item("PKG_NB_UT1")
    '        goodsRow.Item("PKG_UT_NM") = ("").ToString
    '        goodsRow.Item("CUST_CD_S") = dr.Item("CUST_CD_S")
    '        goodsRow.Item("CUST_CD_SS") = dr.Item("CUST_CD_SS")
    '        goodsRow.Item("CUST_CD_L") = dr.Item("CUST_CD_L")
    '        goodsRow.Item("CUST_CD_M") = dr.Item("CUST_CD_M")
    '        goodsRow.Item("CUST_NM_L") = ("").ToString
    '        goodsRow.Item("CUST_NM_M") = ("").ToString
    '        goodsRow.Item("GOODS_CD_NRS") = dr.Item("GOODS_CD_NRS")
    '        goodsRow.Item("GOODS_NM_2") = ("").ToString
    '        goodsRow.Item("UP_GP_CD_1") = ("").ToString
    '        goodsRow.Item("KIKEN_KB") = ("").ToString
    '        goodsRow.Item("DOKU_KB") = ("").ToString
    '        goodsRow.Item("UNSO_ONDO_KB") = ("").ToString
    '        goodsRow.Item("ONDO_STR_DATE") = dr.Item("ONDO_STR_DATE")
    '        goodsRow.Item("ONDO_END_DATE") = dr.Item("ONDO_STR_DATE")
    '        goodsRow.Item("STD_WT_KGS") = dr.Item("STD_WT_KGS")
    '        goodsRow.Item("INKA_KAKO_SAGYO_KB_1") = ("").ToString
    '        goodsRow.Item("INKA_KAKO_SAGYO_KB_2") = ("").ToString
    '        goodsRow.Item("INKA_KAKO_SAGYO_KB_3") = ("").ToString
    '        goodsRow.Item("INKA_KAKO_SAGYO_KB_4") = ("").ToString
    '        goodsRow.Item("INKA_KAKO_SAGYO_KB_5") = ("").ToString
    '        goodsRow.Item("TARE_YN") = dr.Item("TARE_YN")
    '        goodsRow.Item("SP_NHS_YN") = ("").ToString
    '        goodsRow.Item("COA_YN") = ("").ToString
    '        goodsRow.Item("LOT_CTL_KB") = dr.Item("LOT_CTL_KB")
    '        goodsRow.Item("LT_DATE_CTL_KB") = dr.Item("LT_DATE_CTL_KB")
    '        goodsRow.Item("CRT_DATE_CTL_KB") = ("").ToString
    '        goodsRow.Item("SKYU_MEI_YN") = ("").ToString
    '        goodsRow.Item("HIKIATE_ALERT_YN") = ("").ToString


    '        goodsDs.Tables(LMZ020C.TABLE_NM_OUT).Rows.Add(goodsRow)

    '    Next

    '    Return goodsDs

    'End Function

#If True Then ' JT物流入荷検品対応 20160726 added inoue

    ''' <summary>
    ''' 製造日取込有無判定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsReadGoodsCrtDate() As Boolean

        If (Me._Ds IsNot Nothing AndAlso Me._Ds.Tables.Contains("LMB020_KENPIN_DATA")) Then

            Dim table As DataTable = Me._Ds.Tables("LMB020_KENPIN_DATA")

            ' 取込対象データに一件でも製造日単位に管理するデータがあれば製造日を対象とする
            Return (table.AsEnumerable() _
                    .Where(Function(row) _
                               LMB020C.CHK_TANI.GOODS_AND_LOT_AND_IREME_AND_MFG_DATE _
                                .Equals(row.Item("CHK_TANI"))).Count > 0)

        End If

        Return False

    End Function

#End If


    ''' <summary>
    ''' 検品WKからデータセットへ設定
    ''' </summary>
    ''' <param name="dsKp"></param>
    ''' <remarks></remarks>    
    Private Function SetKenpinDatasetTo(ByVal ds As DataSet, ByVal dsKp As DataSet, ByVal flgMMerge As String) As DataSet

        Dim dr As DataRow
        Dim kenpinRow As DataRow
        Dim max As Integer = dsKp.Tables("LMB040OUT").Rows.Count - 1

        '2014.07.29 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ST-
        Dim kenpinWkRows() As DataRow = Nothing
        Dim kenpinWkRow As DataRow = Nothing
        '2014.07.29 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ED-

        For i As Integer = 0 To max

            dr = dsKp.Tables("LMB040OUT").Rows(i)

            '2014.07.29 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ST-
            Select Case flgMMerge
                Case "1"
                    kenpinWkRows = ds.Tables(LMB020C.TABLE_NM_KENPIN_WK_DATA).Select(String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD"), _
                                                                                                   "' AND WH_CD ='", dr.Item("WH_CD"), _
                                                                                                   "' AND CUST_CD_L = '", dr.Item("CUST_CD_L"), _
                                                                                                   "' AND INPUT_DATE ='", dr.Item("INPUT_DATE"), _
                                                                                                   "' AND SEQ = '", dr.Item("SEQ"), "'"))
                    '一致レコードが見つかった場合、後続処理を回避(既に追加されている行であるため不要であると判断する。)
                    If (Not kenpinWkRows Is Nothing) AndAlso kenpinWkRows.Count > 0 Then
                        Continue For
                    Else
                        kenpinWkRow = Nothing
                        kenpinWkRow = ds.Tables(LMB020C.TABLE_NM_KENPIN_WK_DATA).NewRow()

                        kenpinWkRow.Item("NRS_BR_CD") = dr.Item("NRS_BR_CD")
                        kenpinWkRow.Item("WH_CD") = dr.Item("WH_CD")
                        kenpinWkRow.Item("CUST_CD_L") = dr.Item("CUST_CD_L")
                        kenpinWkRow.Item("INPUT_DATE") = dr.Item("INPUT_DATE")
                        kenpinWkRow.Item("SEQ") = dr.Item("SEQ")
                        kenpinWkRow.Item("GOODS_CD_NRS") = dr.Item("GOODS_CD_NRS")
                        kenpinWkRow.Item("SERIAL_NO") = dr.Item("SERIAL_NO")

                        kenpinWkRow.Item("LOT_NO") = dr.Item("LOT_NO")

#If True Then ' JT物流入荷検品対応 20160726 added inoue
                        kenpinWkRow.Item("GOODS_CRT_DATE") = dr.Item("GOODS_CRT_DATE")
#End If
#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
                        kenpinWkRow.Item("LT_DATE") = dr.Item("LT_DATE")
#End If
                        kenpinWkRow.Item("TOU_NO") = dr.Item("TOU_NO")
                        kenpinWkRow.Item("SITU_NO") = dr.Item("SITU_NO")
                        kenpinWkRow.Item("ZONE_CD") = dr.Item("ZONE_CD")
                        kenpinWkRow.Item("LOCA") = dr.Item("LOCA")

                        ds.Tables(LMB020C.TABLE_NM_KENPIN_WK_DATA).Rows.Add(kenpinWkRow)
                    End If

                Case Else
                    '処理なし
            End Select
            '2014.07.29 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ED-

            kenpinRow = ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA).NewRow()
            'dr = dsKp.Tables("LMB040OUT").Rows(i)

            kenpinRow.Item("NRS_BR_CD") = dr.Item("NRS_BR_CD")
            kenpinRow.Item("KENPIN_DATE") = dr.Item("KENPIN_DATE")
            kenpinRow.Item("GOODS_NM") = dr.Item("GOODS_NM")
            kenpinRow.Item("GOODS_CD_CUST") = dr.Item("GOODS_CD_CUST")
            kenpinRow.Item("STD_IRIME_NB") = dr.Item("STD_IRIME_NB")
            kenpinRow.Item("STD_IRIME_UT") = dr.Item("STD_IRIME_UT")
            kenpinRow.Item("PKG_UT") = dr.Item("PKG_UT")
            kenpinRow.Item("PKG_NB") = dr.Item("PKG_NB")
            kenpinRow.Item("NB_UT") = dr.Item("NB_UT")
            kenpinRow.Item("USER_NM") = dr.Item("USER_NM")
            kenpinRow.Item("LOT_NO") = dr.Item("LOT_NO")
            '2014.02.17 WIT対応 START
            kenpinRow.Item("SERIAL_NO") = dr.Item("SERIAL_NO")
            '2014.02.17 WIT対応 END

#If True Then ' JT物流入荷検品対応 20160726 added inoue
            kenpinRow.Item("GOODS_CRT_DATE") = dr.Item("GOODS_CRT_DATE")
            kenpinRow.Item("CHK_TANI") = dr.Item("CHK_TANI")
#End If
#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
            kenpinRow.Item("LT_DATE") = dr.Item("LT_DATE")
#End If
            'WIT対応 START
            'kenpinRow.Item("KENPIN_NO") = dr.Item("KENPIN_NO")
            kenpinRow.Item("WH_CD") = dr.Item("WH_CD")
            kenpinRow.Item("INPUT_DATE") = dr.Item("INPUT_DATE")
            kenpinRow.Item("SEQ") = dr.Item("SEQ")
            kenpinRow.Item("GOODS_KANRI_NO") = dr.Item("GOODS_KANRI_NO")
            'WIT対応 END
            kenpinRow.Item("TOU_NO") = dr.Item("TOU_NO")
            kenpinRow.Item("SITU_NO") = dr.Item("SITU_NO")
            kenpinRow.Item("ZONE_CD") = dr.Item("ZONE_CD")
            kenpinRow.Item("LOCA") = dr.Item("LOCA")
            kenpinRow.Item("GOODS_CD_NRS") = dr.Item("GOODS_CD_NRS")
            kenpinRow.Item("CUST_CD_L") = dr.Item("CUST_CD_L")
            kenpinRow.Item("CUST_CD_M") = dr.Item("CUST_CD_M")
            kenpinRow.Item("ONDO_KB") = dr.Item("ONDO_KB")
            kenpinRow.Item("ONDO_STR_DATE") = dr.Item("ONDO_STR_DATE")
            kenpinRow.Item("ONDO_END_DATE") = dr.Item("ONDO_END_DATE")
            kenpinRow.Item("STD_WT_KGS") = dr.Item("STD_WT_KGS")
            kenpinRow.Item("KONSU") = dr.Item("KONSU")
            kenpinRow.Item("HASU") = dr.Item("HASU")
            kenpinRow.Item("BETU_WT") = dr.Item("BETU_WT")
            kenpinRow.Item("SYS_UPD_DATE") = dr.Item("SYS_UPD_DATE")
            kenpinRow.Item("SYS_UPD_TIME") = dr.Item("SYS_UPD_TIME")
            kenpinRow.Item("CUST_CD_S") = dr.Item("CUST_CD_S")
            kenpinRow.Item("CUST_CD_SS") = dr.Item("CUST_CD_SS")
            kenpinRow.Item("TARE_YN") = dr.Item("TARE_YN")
            kenpinRow.Item("LOT_CTL_KB") = dr.Item("LOT_CTL_KB")
            kenpinRow.Item("LT_DATE_CTL_KB") = dr.Item("LT_DATE_CTL_KB")
            kenpinRow.Item("CRT_DATE_CTL_KB") = dr.Item("CRT_DATE_CTL_KB")

            ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA).Rows.Add(kenpinRow)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 入荷(小)データ追加処理(入荷検品WK取込時)
    ''' </summary>
    ''' <param name="ds">Me._Ds</param>
    ''' <remarks></remarks>
    Private Function setInkaNoSKenpin(ByVal prmDs As DataSet, ByVal ds As DataSet) As DataSet

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'Dim rtDt As DataTable = prmDs.Tables("LMB020_KENPIN_DATA")
        'Dim rtDt As DataTable = prmDs.Tables("LMB040OUT")
        Dim rtDt As DataTable = ds.Tables("LMB020_KENPIN_DATA")
        Dim meDt As DataTable = ds.Tables("LMB020_INKA_S")

        Dim max As Integer = rtDt.Rows.Count - 1

        Dim maxCol As Integer = meDt.Columns.Count - 1

        For i As Integer = 0 To max

            Dim dr As DataRow = meDt.NewRow()
            For j As Integer = 0 To maxCol
                dr.Item(j) = String.Empty
            Next
            Dim brCd As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()
            dr.Item("NRS_BR_CD") = brCd
            dr.Item("INKA_NO_L") = ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L").ToString()
            dr.Item("INKA_NO_M") = rtDt.Rows(i).Item("INKA_NO_M").ToString()
            dr.Item("INKA_NO_S") = rtDt.Rows(i).Item("INKA_NO_S").ToString()
            Dim sql As String = String.Concat("NRS_BR_CD = '", brCd, "' " _
                                              , " AND INKA_NO_L = '", ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L").ToString(), "' " _
                                              , " AND INKA_NO_M = '", rtDt.Rows(i).Item("INKA_NO_M").ToString(), "' " _
                                              )
            Dim inkaMDr As DataRow = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(sql)(0)

            Dim irime As String

            If "0".Equals(rtDt.Rows(i).Item("STD_IRIME_NB").ToString()) = True OrElse
                String.IsNullOrEmpty(rtDt.Rows(i).Item("STD_IRIME_NB").ToString()) = True Then
                irime = ds.Tables("LMB020_GOODS_NM").Rows(0).Item("STD_IRIME_NB").ToString()
            Else
                irime = rtDt.Rows(i).Item("STD_IRIME_NB").ToString() '入荷検品WKの入目
            End If

            dr.Item("IRIME") = irime

            ''Dim irimeTani As String = rtDt.Rows(i).Item("STD_IRIME_UT").ToString()
            ''Dim juryo As String = ds.Tables("LMB020_GOODS_NM").Rows(0).Item("STD_WT_KGS").ToString()
            Dim kpIrime As Decimal = Convert.ToDecimal(rtDt.Rows(i).Item("STD_IRIME_NB").ToString()) '入荷検品WK.入目
            Dim sprKonsu As Decimal = Convert.ToDecimal(rtDt.Rows(i).Item("KONSU").ToString()) '個数(入荷検品WK個数)
            Dim sprIrisu As Decimal = Convert.ToDecimal(ds.Tables("LMB020_GOODS_NM").Rows(0).Item("PKG_NB").ToString()) '商品M.入数
            Dim sprHasu As Decimal = Convert.ToDecimal(rtDt.Rows(i).Item("HASU").ToString()) '端数(入荷検品WK個数)
            Dim sprKosu As Decimal = sprKonsu * sprIrisu + sprHasu
            Dim stdWt As Decimal = Convert.ToDecimal(ds.Tables("LMB020_GOODS_NM").Rows(0).Item("STD_WT_KGS").ToString()) '商品M.標準重量
            Dim kpRow As Decimal = Convert.ToDecimal(rtDt.Rows.Count) '入荷検品WK.選択行数
            Dim stdIrime As Decimal = Convert.ToDecimal(ds.Tables("LMB020_GOODS_NM").Rows(0).Item("STD_IRIME_NB").ToString()) '商品M.標準入目

            'SUM_KOSU
            Dim sumKosu As Decimal = 0
            sumKosu = sumKosu + sprKosu
            ds.Tables("LMB020_GOODS_NM").Rows(0).Item("SUM_KOSU") = sumKosu

            'SUM_JURYO_M 
            '1.csvから読み込んだ個数*入目の合計を入れる(それぞれの行を掛けて、合算)
            '2.入目がzeroなら、商品M.標準入り目を使う。
            Dim sumSuryoM As Decimal = 0

            If "0".Equals(rtDt.Rows(i).Item("STD_IRIME_NB").ToString()) = True OrElse
                String.IsNullOrEmpty(rtDt.Rows(i).Item("STD_IRIME_NB").ToString()) = True Then
                sumSuryoM = sumSuryoM + (sprKosu * stdIrime) '入荷検品WKの入目がゼロの時
            Else
                sumSuryoM = sumSuryoM + (sprKosu * kpIrime)
            End If

            ds.Tables("LMB020_GOODS_NM").Rows(0).Item("SUM_SURYO_M") = sumSuryoM

            'SUM_JURYO_M = SUM(商品Mの標準重量*入荷検品WKの入目/商品M.標準入目*入荷検品WKの選択行数)
            Dim sumJuryoM As Decimal = sumJuryoM + (stdWt * kpIrime / stdIrime * kpRow)
            ds.Tables("LMB020_GOODS_NM").Rows(0).Item("SUM_JURYO_M") = sumJuryoM

            Dim irimeTani As String = inkaMDr.Item("STD_IRIME_UT").ToString()
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_I001, "' AND KBN_CD = '", irimeTani, "'"))
            Dim kbnNm As String = String.Empty
            If 0 < drs.Length Then

                '2017/09/25 修正 李↓
                kbnNm = drs(0).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString()
                '2017/09/25 修正 李↑

            End If

            dr.Item("LOT_NO") = rtDt.Rows(i).Item("LOT_NO").ToString()
            '↓入荷検品WKに持っていないので空白セット
            '2014.02.17 WIT対応START
            'dr.Item("SERIAL_NO") = String.Empty
            dr.Item("SERIAL_NO") = rtDt.Rows(i).Item("SERIAL_NO").ToString()
            '2014.02.17 WIT対応END

#If True Then ' JT物流入荷検品対応 20160726 added inoue
            dr.Item("GOODS_CRT_DATE") = rtDt.Rows(i).Item("GOODS_CRT_DATE").ToString()
#End If
#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
            dr.Item("LT_DATE") = rtDt.Rows(i).Item("LT_DATE").ToString()
#End If
            dr.Item("REMARK_OUT") = String.Empty
            dr.Item("REMARK") = String.Empty
            '↑入荷検品WKに持っていないので空白セット
            dr.Item("TOU_NO") = rtDt.Rows(i).Item("TOU_NO").ToString()
            dr.Item("SITU_NO") = rtDt.Rows(i).Item("SITU_NO").ToString()
            dr.Item("ZONE_CD") = rtDt.Rows(i).Item("ZONE_CD").ToString()
            dr.Item("LOCA") = rtDt.Rows(i).Item("LOCA").ToString()
            dr.Item("STD_IRIME_NM") = kbnNm
            dr.Item("IRIME") = irime
            dr.Item("JURYO_S") = "0"
            dr.Item("ALLOC_PRIORITY") = LMB020C.WARIATE_FREE
            dr.Item("OFB_KB") = "01"
            dr.Item("SPD_KB") = "01"
            dr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
            dr.Item("UP_KBN") = LMConst.FLG.OFF
            dr.Item("LOT_CTL_KB") = inkaMDr.Item("LOT_CTL_KB").ToString()
            dr.Item("LT_DATE_CTL_KB") = inkaMDr.Item("LT_DATE_CTL_KB").ToString()
            dr.Item("CRT_DATE_CTL_KB") = inkaMDr.Item("CRT_DATE_CTL_KB").ToString()
            ''2013.07.16 追加START①
            'dr.Item("GOODS_CD_NRS") = inkaMDr.Item("GOODS_CD_NRS").ToString()
            'dr.Item("GOODS_CD_CUST") = inkaMDr.Item("GOODS_CD_CUST").ToString()
            ''2013.07.16 追加END①
            dr.Item("STD_WT_KGS") = inkaMDr.Item("STD_WT_KGS").ToString()
            dr.Item("STD_IRIME_UT") = inkaMDr.Item("STD_IRIME_UT").ToString()
            dr.Item("KONSU") = rtDt.Rows(i).Item("KONSU").ToString()
            dr.Item("KOSU_S") = rtDt.Rows(i).Item("KONSU").ToString()
            dr.Item("HASU") = rtDt.Rows(i).Item("HASU").ToString()
            dr.Item("SURYO_S") = sprKosu * stdIrime

            'WIT対応 2013.12.10 Start
            dr.Item("TORIKOMI_GOODS_KANRI_NO") = rtDt.Rows(i).Item("GOODS_KANRI_NO").ToString()
            'WIT対応 2013.12.10 End

            Dim inkaNoS As String = String.Empty

            '行追加
            meDt.Rows.Add(dr)
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 入荷(小)データ追加処理(入荷検品WK取込時)
    ''' </summary>
    ''' <param name="ds">Me._Ds</param>
    ''' <remarks></remarks>
    Private Function setInkaNoSKenpinMerge(ByVal prmDs As DataSet, ByVal ds As DataSet) As DataSet

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'Mデータセット関連
        Dim mDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)

        'Sデータセット関連
        Dim sDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S)
        Dim sDr As DataRow = Nothing

        '検品データデータセット関連
        Dim kpDt As DataTable = ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA)
        Dim kpRows() As DataRow = Nothing

        '検品ワークデータデータセット関連
        Dim kpWkDt As DataTable = ds.Tables(LMB020C.TABLE_NM_KENPIN_WK_DATA)
        Dim kpWkRows() As DataRow = Nothing

        'Mの行カウント
        Dim mMax As Integer = mDt.Rows.Count - 1

        '対象商品の行番号取得
        Dim gdDt As DataTable = ds.Tables("LMB020_GOODS_NM")
        Dim gdRows() As DataRow = Nothing

        'Sの行カウント
        Dim sCnt As Integer = 0

        'Sの列カウント
        Dim sMaxCol As Integer = sDt.Columns.Count - 1

        '商品情報格納行番号
        Dim gdMaxRow As Integer = 0

        'Skey
        Dim sPrevKey As String = String.Empty
        Dim sSvKey As String = String.Empty

        '各計算値
        Dim kpIrime As Decimal = 0         '入荷検品WK.入目
        Dim sprKonsu As Decimal = 0        '個数(入荷検品WK個数)
        Dim sprIrisu As Decimal = 0        '商品M.入数
        Dim sprHasu As Decimal = 0         '端数(入荷検品WK個数)
        Dim sprKosu As Decimal = 0         '個数
        Dim stdWt As Decimal = 0           '商品M.標準重量
        Dim kpRow As Decimal = 0           '入荷検品WK.選択行数
        Dim stdIrime As Decimal = 0        '商品M.標準入目

        '商品系計算値
        Dim sumKosu As Decimal = 0
        Dim sumSuryoM As Decimal = 0
        Dim sumJuryoM As Decimal = 0

        'SUM値
        Dim masterHasu As Decimal = 0
        Dim masterSuryoS As Decimal = 0
        Dim masterKonsu As Decimal = 0

        '入目単位格納変数
        Dim irime As String = String.Empty '入目

        'データ取込済フラグ
        Dim duplicateFlg As Boolean = False

        'Mループ
        For i As Integer = 0 To mMax

            '検品データより管理番号中で抽出
            kpRows = kpDt.Select(String.Concat("INKA_NO_M ='", mDt.Rows(i).Item("INKA_NO_M"), "'"), "INKA_NO_S ASC")

            sPrevKey = String.Empty
            sSvKey = String.Empty

            kpIrime = 0
            sprKonsu = 0
            sprIrisu = 0
            sprHasu = 0
            sprKosu = 0
            stdWt = 0
            kpRow = 0
            stdIrime = 0

            sumKosu = 0
            sumSuryoM = 0
            sumJuryoM = 0

            masterKonsu = 0
            masterHasu = 0
            masterSuryoS = 0

            gdMaxRow = 0

            '商品データ取得行番号設定
            If (Not kpRows Is Nothing) AndAlso kpRows.Count > 0 Then
                gdRows = gdDt.Select(String.Concat("GOODS_CD_NRS = '", kpRows(kpRows.Count - 1).Item("GOODS_CD_NRS"), "'"))
                gdMaxRow = gdDt.Rows.IndexOf(gdRows(gdRows.Count - 1))
            End If

            'Sループ(商品,棟,室,ZONE,ロケール,ロット番)でS割れ
            For Each Row As DataRow In kpRows

                '行カウントUp
                sCnt += 1

                'スルーキー設定
                sPrevKey = Row.Item("INKA_NO_S").ToString()

                '取込チェック(直前データと現在データをチェック)
                kpWkRows = Nothing

                If sCnt = kpRows.Count Then
                    kpWkRows = kpWkDt.Select(String.Concat("INKA_NO_M = '", Row.Item("INKA_NO_M").ToString(), "' AND INKA_NO_S ='", Row.Item("INKA_NO_S"), "'"))
                Else
                    kpWkRows = kpWkDt.Select(String.Concat("INKA_NO_M = '", kpRows(sCnt).Item("INKA_NO_M").ToString(), "' AND INKA_NO_S ='", kpRows(sCnt).Item("INKA_NO_S"), "'"))
                End If

                If (Not kpWkRows Is Nothing) AndAlso kpWkRows.Count > 0 Then
                    duplicateFlg = True
                Else
                    duplicateFlg = False
                End If

                '通常キーブレイク判定 [前行SUM値返却 + 前行コミット+ SUM値クリア + キー入替 + 次行作成 + 初期設定]
                If Not sPrevKey.Equals(sSvKey) Then

                    If Not String.IsNullOrEmpty(sSvKey) Then
                        '====================================
                        '=========== 前行SUM値返却 ==========
                        '====================================
                        'SUMしたところで用途がない・・・
                        'ds.Tables("LMB020_GOODS_NM").Rows(0).Item("SUM_KOSU") = sumKosu
                        'ds.Tables("LMB020_GOODS_NM").Rows(0).Item("SUM_SURYO_M") = sumSuryoM
                        'ds.Tables("LMB020_GOODS_NM").Rows(0).Item("SUM_JURYO_M") = sumJuryoM
                        'SUMしたところで用途がない・・・
                        sDr.Item("KONSU") = masterKonsu
                        sDr.Item("KOSU_S") = masterKonsu
                        sDr.Item("HASU") = masterHasu
                        sDr.Item("SURYO_S") = masterSuryoS

                        '====================================
                        '============= 前行追加 =============
                        '====================================
                        sDt.Rows.Add(sDr)
                    End If

                    '====================================
                    '============ SUM値クリア ===========
                    '====================================
                    kpIrime = 0
                    sprKonsu = 0
                    sprIrisu = 0
                    sprHasu = 0
                    sprKosu = 0
                    stdWt = 0
                    kpRow = 0
                    stdIrime = 0

                    sumKosu = 0
                    sumSuryoM = 0
                    sumJuryoM = 0

                    masterKonsu = 0
                    masterHasu = 0
                    masterSuryoS = 0

                    '====================================
                    '============ キー入替え ============
                    '====================================
                    sSvKey = sPrevKey

                    '====================================
                    '============= 次行作成 =============
                    '====================================
                    sDr = sDt.NewRow()
                    For j As Integer = 0 To sMaxCol
                        sDr.Item(j) = String.Empty
                    Next

                    '====================================
                    '====== 初期確定パラメータ設定 ======
                    '====================================
                    Dim brCd As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()

                    sDr.Item("NRS_BR_CD") = brCd
                    sDr.Item("INKA_NO_L") = ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L").ToString()
                    sDr.Item("INKA_NO_M") = Row.Item("INKA_NO_M").ToString()
                    sDr.Item("INKA_NO_S") = Row.Item("INKA_NO_S").ToString()

                    irime = String.Empty
                    If "0".Equals(Row.Item("STD_IRIME_NB").ToString()) = True OrElse
                        String.IsNullOrEmpty(Row.Item("STD_IRIME_NB").ToString()) = True Then
                        irime = ds.Tables("LMB020_GOODS_NM").Rows(gdMaxRow).Item("STD_IRIME_NB").ToString()
                    Else
                        irime = Row.Item("STD_IRIME_NB").ToString() '入荷検品WKの入目
                    End If

                    sDr.Item("IRIME") = irime

                    Dim sql As String = String.Concat("NRS_BR_CD = '", brCd, "' " _
                                      , " AND INKA_NO_L = '", ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L").ToString(), "' " _
                                      , " AND INKA_NO_M = '", Row.Item("INKA_NO_M").ToString(), "' " _
                                      )
                    Dim inkaMDr As DataRow = Me._Ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(sql)(0)


                    Dim irimeTani As String = inkaMDr.Item("STD_IRIME_UT").ToString()
                    Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_I001, "' AND KBN_CD = '", irimeTani, "'"))
                    Dim kbnNm As String = String.Empty
                    If 0 < drs.Length Then

                        '2017/09/25 修正 李↓
                        kbnNm = drs(0).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString()
                        '2017/09/25 修正 李↑

                    End If

                    sDr.Item("LOT_NO") = Row.Item("LOT_NO").ToString()
                    '※ ※ ※ LMには載せない ※ ※ ※
                    sDr.Item("SERIAL_NO") = String.Empty
                    '※ ※ ※ LMには載せない ※ ※ ※

#If True Then ' JT物流入荷検品対応 20160726 added inoue
                    sDr.Item("GOODS_CRT_DATE") = Row.Item("GOODS_CRT_DATE").ToString()
#End If
#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
                    sDr.Item("LT_DATE") = Row.Item("LT_DATE").ToString()
#End If
                    sDr.Item("REMARK_OUT") = String.Empty
                    sDr.Item("REMARK") = String.Empty
                    sDr.Item("TOU_NO") = Row.Item("TOU_NO").ToString()
                    sDr.Item("SITU_NO") = Row.Item("SITU_NO").ToString()
                    sDr.Item("ZONE_CD") = Row.Item("ZONE_CD").ToString()
                    sDr.Item("LOCA") = Row.Item("LOCA").ToString()
                    sDr.Item("STD_IRIME_NM") = kbnNm
                    sDr.Item("IRIME") = irime
                    sDr.Item("JURYO_S") = "0"
                    sDr.Item("ALLOC_PRIORITY") = LMB020C.WARIATE_FREE
                    sDr.Item("OFB_KB") = "01"
                    sDr.Item("SPD_KB") = "01"
                    sDr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                    sDr.Item("UP_KBN") = LMConst.FLG.OFF
                    sDr.Item("LOT_CTL_KB") = inkaMDr.Item("LOT_CTL_KB").ToString()
                    sDr.Item("LT_DATE_CTL_KB") = inkaMDr.Item("LT_DATE_CTL_KB").ToString()
                    sDr.Item("CRT_DATE_CTL_KB") = inkaMDr.Item("CRT_DATE_CTL_KB").ToString()
                    sDr.Item("STD_WT_KGS") = inkaMDr.Item("STD_WT_KGS").ToString()
                    sDr.Item("STD_IRIME_UT") = inkaMDr.Item("STD_IRIME_UT").ToString()
                    sDr.Item("TORIKOMI_GOODS_KANRI_NO") = Row.Item("GOODS_KANRI_NO").ToString()

                End If

                '====================================
                '========== SUM行のSUM処理 ========== 
                '====================================
                kpIrime = Convert.ToDecimal(Row.Item("STD_IRIME_NB").ToString())                                          '入荷検品WK.入目
                sprKonsu = Convert.ToDecimal(Row.Item("KONSU").ToString())                                                '個数(入荷検品WK個数)
                sprIrisu = Convert.ToDecimal(ds.Tables("LMB020_GOODS_NM").Rows(gdMaxRow).Item("PKG_NB").ToString())       '商品M.入数
                sprHasu = Convert.ToDecimal(Row.Item("HASU").ToString())                                                  '端数(入荷検品WK個数)
                sprKosu = sprKonsu * sprIrisu + sprHasu                                                                   '個数
                stdWt = Convert.ToDecimal(ds.Tables("LMB020_GOODS_NM").Rows(gdMaxRow).Item("STD_WT_KGS").ToString())      '商品M.標準重量
                kpRow = Convert.ToDecimal(kpRows.Count)                                                                   '入荷検品WK.選択行数
                stdIrime = Convert.ToDecimal(ds.Tables("LMB020_GOODS_NM").Rows(gdMaxRow).Item("STD_IRIME_NB").ToString()) '商品M.標準入目

                'SUM_KOSU
                sumKosu = sumKosu + sprKosu

                'SUM_SURYO_M 
                If "0".Equals(Row.Item("STD_IRIME_NB").ToString()) = True OrElse
                    String.IsNullOrEmpty(Row.Item("STD_IRIME_NB").ToString()) = True Then
                    sumSuryoM = sumSuryoM + (sprKosu * stdIrime)
                Else
                    sumSuryoM = sumSuryoM + (sprKosu * kpIrime)
                End If

                'SUM_JURYO_M
                sumJuryoM = sumJuryoM + (stdWt * kpIrime / stdIrime * 1)

                '梱数/梱数S
                If "0".Equals(Row.Item("KONSU").ToString()) = True OrElse
                    String.IsNullOrEmpty(Row.Item("KONSU").ToString()) = True Then
                    masterKonsu = masterKonsu + 0
                Else
                    masterKonsu = masterKonsu + Convert.ToDecimal(Row.Item("KONSU").ToString())
                End If

                '端数
                If "0".Equals(Row.Item("HASU").ToString()) = True OrElse
                    String.IsNullOrEmpty(Row.Item("HASU").ToString()) = True Then
                    masterHasu = masterHasu + 0
                Else
                    masterHasu = masterHasu + Convert.ToDecimal(Row.Item("HASU").ToString())
                End If

                '数量
                masterSuryoS = masterSuryoS + (sprKosu * stdIrime)

                '最終行到達確認
                If sCnt = kpRows.Count Then

                    '====================================
                    '=========== 前行SUM値返却 ==========
                    '====================================
                    'SUMしたところで用途がない・・・
                    'ds.Tables("LMB020_GOODS_NM").Rows(0).Item("SUM_KOSU") = sumKosu
                    'ds.Tables("LMB020_GOODS_NM").Rows(0).Item("SUM_SURYO_M") = sumSuryoM
                    'ds.Tables("LMB020_GOODS_NM").Rows(0).Item("SUM_JURYO_M") = sumJuryoM
                    'SUMしたところで用途がない・・・
                    sDr.Item("KONSU") = masterKonsu
                    sDr.Item("KOSU_S") = masterKonsu
                    sDr.Item("HASU") = masterHasu
                    sDr.Item("SURYO_S") = masterSuryoS

                    '====================================
                    '============ 最終行作成 ============
                    '====================================
                    sDt.Rows.Add(sDr)

                    '====================================
                    '========= カウンタリセット =========
                    '====================================
                    sCnt = 0
                End If

            Next

        Next

        Return ds

    End Function

#End Region '"入荷検品WK読み取り関連"
    '2013.07.16 追加END

#Region "マスタ存在チェック"

    ''' <summary>
    ''' 商品マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">dataser</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExitsGoodsCheck(ByVal frm As LMB020F, ByVal ds As DataSet) As DataSet

        With Me._Frm

            Dim drGoods As DataRow() = Nothing
            Dim dr As DataRow = Nothing
            Dim goodsCdCust As String = Nothing
            Dim max As Integer = ds.Tables(LMB020C.TABLE_NM_CSV_DATA).Rows.Count - 1

            '要望番号:1946(入荷CSV取り込みで複数商品出来ない) 2013/03/22 START
            For i As Integer = 0 To max

                'goodsCdCust = ds.Tables(LMB020C.TABLE_NM_CSV_DATA).Rows(0).Item("GOODS_CD_CUST").ToString()
                goodsCdCust = ds.Tables(LMB020C.TABLE_NM_CSV_DATA).Rows(i).Item("GOODS_CD_CUST").ToString()

                '要望対応:1955 terakawa 2013.03.16 Start
                '同じ商品コードの場合、複数件追加しない
                If ds.Tables("LMB020_GOODS_NM").Select("GOODS_CD_CUST = '" + goodsCdCust + "'").Length > 0 Then
                    Continue For
                End If
                '要望対応:1955 terakawa 2013.03.16 End

                'キャッシュテーブルより検索結果を取得
                Dim goodsDs As MGoodsDS = New MGoodsDS
                Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'goodsDr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                goodsDr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
                goodsDr.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                goodsDr.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                goodsDr.Item("SYS_DEL_FLG") = "0"
                goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
                Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)

                drGoods = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_CUST = '" _
                                                                                      , goodsCdCust, "'"))


                If drGoods.Length = 0 Then
                    drGoods = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_CUST LIKE '%" _
                                                                        , goodsCdCust, "%'"))
                End If

                If drGoods.Length > 1 Then
                    'LIKEで1件以上はエラーメッセージ
                    'EXCEL起動 
                    MyBase.MessageStoreDownload(True)
                    MyBase.ShowMessage(frm, "E235")
                End If

                dr = ds.Tables("LMB020_GOODS_NM").NewRow
                dr.Item("NRS_BR_CD") = drGoods(0).Item("NRS_BR_CD")
                dr.Item("INKA_NO_L") = ("").ToString
                dr.Item("INKA_NO_M") = ("").ToString
                dr.Item("GOODS_CD_NRS") = drGoods(0).Item("GOODS_CD_NRS")
                dr.Item("GOODS_CD_CUST") = drGoods(0).Item("GOODS_CD_CUST")
                dr.Item("OUTKA_FROM_ORD_NO_M") = ("").ToString
                dr.Item("BUYER_ORD_NO_M") = ("").ToString
                dr.Item("REMARK") = ("").ToString
                dr.Item("PRINT_SORT") = ("").ToString
                dr.Item("GOODS_NM") = drGoods(0).Item("GOODS_NM_1")
                dr.Item("ONDO_KB") = drGoods(0).Item("ONDO_KB")
                dr.Item("SUM_KOSU") = (0) '明細S入力後、csvから読み込んだ個数の合計を入れる
                dr.Item("NB_UT") = drGoods(0).Item("NB_UT")
                dr.Item("ONDO_STR_DATE") = drGoods(0).Item("ONDO_STR_DATE")
                dr.Item("ONDO_END_DATE") = drGoods(0).Item("ONDO_END_DATE")
                dr.Item("PKG_NB") = drGoods(0).Item("PKG_NB")
                dr.Item("PKG_NB_UT1") = drGoods(0).Item("PKG_UT")
                dr.Item("PKG_NB_UT2") = ("").ToString
                dr.Item("STD_IRIME_NB_M") = ("").ToString
                dr.Item("STD_IRIME_UT") = drGoods(0).Item("STD_IRIME_UT")
                dr.Item("SUM_SURYO_M") = (0) '明細S入力後、1.csv.個数*csvから読み込んだ入目の合計を入れる(それぞれの行を掛けて、合算)2.いり目がzeroなら、商品M.標準入り目を使う。
                dr.Item("SUM_JURYO_M") = (0) '明細S入力後、SUM(商品Mの標準重量*CSVの入目/商品M.標準入目*csvの個数)
                dr.Item("SHOBO_CD") = drGoods(0).Item("SHOBO_CD")
                dr.Item("HIKIATE") = ("").ToString
                dr.Item("SAGYO_UMU") = ("").ToString
                dr.Item("SYS_DEL_FLG") = drGoods(0).Item("SYS_DEL_FLG")
                dr.Item("EDI_KOSU") = (0)
                dr.Item("EDI_SURYO") = (0)
                dr.Item("STD_IRIME_NB") = drGoods(0).Item("STD_IRIME_NB")
                dr.Item("STD_WT_KGS") = drGoods(0).Item("STD_WT_KGS")
                dr.Item("CUST_CD_L") = drGoods(0).Item("CUST_CD_L")
                dr.Item("CUST_CD_M") = drGoods(0).Item("CUST_CD_M")
                dr.Item("CUST_CD_S") = drGoods(0).Item("CUST_CD_S")
                dr.Item("CUST_CD_SS") = drGoods(0).Item("CUST_CD_SS")
                dr.Item("TARE_YN") = drGoods(0).Item("TARE_YN")
                dr.Item("LOT_CTL_KB") = drGoods(0).Item("LOT_CTL_KB")
                dr.Item("LT_DATE_CTL_KB") = drGoods(0).Item("LT_DATE_CTL_KB")
                dr.Item("CRT_DATE_CTL_KB") = drGoods(0).Item("CRT_DATE_CTL_KB")
                dr.Item("EDI_FLG") = ("").ToString
                dr.Item("UP_KBN") = ("").ToString
                dr.Item("JISSEKI_FLAG") = ("").ToString


                ds.Tables("LMB020_GOODS_NM").Rows.Add(dr)

            Next
            '要望番号:1946(入荷CSV取り込みで複数商品出来ない) 2013/03/22 END

        End With

        Return ds

    End Function

#End Region 'マスタ存在チェック

    '2013.07.16 追加START
#Region "マスタ存在チェック(入荷検品WK取込用)"

    ''' <summary>
    ''' 商品マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">dataser</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExitsGoodsKenpinCheck(ByVal frm As LMB020F, ByVal ds As DataSet) As DataSet

        With Me._Frm

            Dim dsKp As DataTable = ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA)
            Dim dr As DataRow = Nothing
            Dim goodsCdNrs As String = Nothing
            Dim max As Integer = ds.Tables(LMB020C.TABLE_NM_KENPIN_DATA).Rows.Count - 1

            '商品情報DS(GOODS_NM)一旦クリア　※前回情報が残ってしまう為
            ds.Tables("LMB020_GOODS_NM").Clear()

            For i As Integer = 0 To max

                'goodsCdNrs = ds.Tables(LMB020C.TABLE_NM_CSV_DATA).Rows(i).Item("GOODS_CD_NRS").ToString()

                ' ''キャッシュテーブルより検索結果を取得
                ''Dim goodsDs As MGoodsDS = New MGoodsDS
                ''Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
                ''goodsDr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                ''goodsDr.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                ''goodsDr.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                ''goodsDr.Item("SYS_DEL_FLG") = "0"
                ''goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
                ''Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)

                ''drGoods = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_NRS = '" _
                ''                                                                      , goodsCdNrs, "'"))


                ''If drGoods.Length = 0 Then
                ''    'LIKEで1件以上はエラーメッセージ
                ''    'EXCEL起動 
                ''    MyBase.MessageStoreDownload(True)
                ''    MyBase.ShowMessage(frm, "E235")
                ''End If

                dr = ds.Tables("LMB020_GOODS_NM").NewRow
                dr.Item("NRS_BR_CD") = dsKp(i).Item("NRS_BR_CD")
                dr.Item("INKA_NO_L") = ("").ToString
                dr.Item("INKA_NO_M") = ("").ToString
                dr.Item("GOODS_CD_NRS") = dsKp(i).Item("GOODS_CD_NRS")
                dr.Item("GOODS_CD_CUST") = dsKp(i).Item("GOODS_CD_CUST")
                dr.Item("OUTKA_FROM_ORD_NO_M") = ("").ToString
                dr.Item("BUYER_ORD_NO_M") = ("").ToString
                dr.Item("REMARK") = ("").ToString
                dr.Item("PRINT_SORT") = ("").ToString
                dr.Item("GOODS_NM") = dsKp(i).Item("GOODS_NM")
                dr.Item("ONDO_KB") = dsKp(i).Item("ONDO_KB")
                dr.Item("SUM_KOSU") = (0) '明細S入力後、csvから読み込んだ個数の合計を入れる
                dr.Item("NB_UT") = dsKp(i).Item("NB_UT")
                dr.Item("ONDO_STR_DATE") = dsKp(i).Item("ONDO_STR_DATE")
                dr.Item("ONDO_END_DATE") = dsKp(i).Item("ONDO_END_DATE")
                dr.Item("PKG_NB") = dsKp(i).Item("PKG_NB")
                dr.Item("PKG_NB_UT1") = dsKp(i).Item("PKG_UT")
                dr.Item("PKG_NB_UT2") = ("").ToString
                dr.Item("STD_IRIME_NB_M") = ("").ToString
                dr.Item("STD_IRIME_UT") = dsKp(i).Item("STD_IRIME_UT")
                dr.Item("SUM_SURYO_M") = (0) '明細S入力後、1.csv.個数*csvから読み込んだ入目の合計を入れる(それぞれの行を掛けて、合算)2.いり目がzeroなら、商品M.標準入り目を使う。
                dr.Item("SUM_JURYO_M") = (0) '明細S入力後、SUM(商品Mの標準重量*CSVの入目/商品M.標準入目*csvの個数)
                dr.Item("SHOBO_CD") = ("").ToString
                dr.Item("HIKIATE") = ("").ToString
                dr.Item("SAGYO_UMU") = ("").ToString
                dr.Item("SYS_DEL_FLG") = "0"
                dr.Item("EDI_KOSU") = (0)
                dr.Item("EDI_SURYO") = (0)
                dr.Item("STD_IRIME_NB") = dsKp(i).Item("STD_IRIME_NB")
                dr.Item("STD_WT_KGS") = dsKp(i).Item("STD_WT_KGS")
                dr.Item("CUST_CD_L") = dsKp(i).Item("CUST_CD_L")
                dr.Item("CUST_CD_M") = dsKp(i).Item("CUST_CD_M")
                dr.Item("CUST_CD_S") = dsKp(i).Item("CUST_CD_S")
                dr.Item("CUST_CD_SS") = dsKp(i).Item("CUST_CD_SS")
                dr.Item("TARE_YN") = dsKp(i).Item("TARE_YN")
                dr.Item("LOT_CTL_KB") = dsKp(i).Item("LOT_CTL_KB")
                dr.Item("LT_DATE_CTL_KB") = dsKp(i).Item("LT_DATE_CTL_KB")
                dr.Item("CRT_DATE_CTL_KB") = dsKp(i).Item("CRT_DATE_CTL_KB")
                dr.Item("EDI_FLG") = ("").ToString
                dr.Item("UP_KBN") = ("").ToString
                dr.Item("JISSEKI_FLAG") = ("").ToString


                ds.Tables("LMB020_GOODS_NM").Rows.Add(dr)

            Next

        End With

        Return ds

    End Function

#End Region 'マスタ存在チェック(入荷検品WK取込用)
    '2013.07.16 追加END

    ''' <summary>
    ''' 棟 + 室 + ZONE（置き場情報）温度管理チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsOndoCheck(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean

        Return Me._V.IsOndoCheck(ds)

    End Function

    ''' <summary>
    ''' 棟 + 室  危険物倉庫、一般倉庫チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSokoCheck(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean

        Return Me._V.IsSokoCheck(ds)

    End Function
    ''' <summary>
    ''' 新規入荷チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTouSituZoneCheck(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean

        With frm

            Dim nrsbrcd As String = .cmbEigyo.SelectedValue.ToString
            Dim sokocd As String = .cmbSoko.SelectedValue.ToString
            Dim inakLNo As String = .lblKanriNoL.TextValue.Trim
            Dim custcd As String = .txtCustCdL.TextValue.Trim

            '入荷(中)全行
            Dim dtM As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)
            Dim maxM As Integer = dtM.Rows.Count - 1
            Dim inakMNo As String = String.Empty
            Dim goodsNRS As String = String.Empty

            '小
            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty

            '入荷(中)がない場合は終了
            If 0 > maxM Then Return True

            '新規入荷チェックを行うか、荷主明細マスタ(M_CUST_DETAILS)の用途区分（荷主明細）(Y008).新規入荷チェック不要フラグ(A2)で判定
            'DataSet設定
            Dim chkDs As DataSet = New LMZ340DS()
            Dim row As DataRow = chkDs.Tables(LMZ340C.TABLE_NM_IN).NewRow
            Dim inTbl As DataTable = chkDs.Tables(LMZ340C.TABLE_NM_IN)

            row("NRS_BR_CD") = nrsbrcd
            row("CUST_CD") = custcd

            inTbl.Rows.Add(row)

            chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCheckFlg", chkDs)

            If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_FLG).Rows.Count > 0 Then
                Dim flgA2 As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_FLG).Rows(0).Item("FLG_A2").ToString

                'フラグが1で設定されている場合、エラーなしで処理終了
                If "1".Equals(flgA2) Then
                    Return True
                End If

            End If

            Dim msg As String = String.Empty

            '倉庫マスタチェック
            Dim sokoDrs As DataRow() = Me._LMBconV.SelectSokoListDataRow(sokocd)
            If sokoDrs.Length < 1 Then Return Me._LMBconV.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", sokocd))
            If sokoDrs(0).Item("LOC_MANAGER_YN").ToString = "00" Then
                Return True
            End If

            '★★★属性チェック
            For i As Integer = 0 To maxM

                '行削除されているデータはチェックしない
                If LMConst.FLG.ON.Equals(dtM.Rows(i).Item("SYS_DEL_FLG").ToString()) Then
                    Continue For
                End If

                inakMNo = dtM.Rows(i).Item("INKA_NO_M").ToString()
                goodsNRS = dtM.Rows(i).Item("GOODS_CD_NRS").ToString()

                '入荷(小)全行
                Dim sqlS As String = String.Concat("NRS_BR_CD = ", " '", nrsbrcd, "' ",
                                                 " AND INKA_NO_L = ", " '", inakLNo, "' ",
                                                 " AND INKA_NO_M = ", " '", inakMNo, "' ")

                Dim drS As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(sqlS)
                Dim maxS As Integer = drS.Length - 1

                '棟室マスタ・ゾーンマスタ
                For j As Integer = 0 To maxS

                    touNo = drS(j).Item("TOU_NO").ToString()
                    situNo = drS(j).Item("SITU_NO").ToString()
                    zoneCd = drS(j).Item("ZONE_CD").ToString()

                    '棟室マスタ
                    Dim tousituDr As DataRow() = Me._LMBconV.SelectTouSituListDataRow(nrsbrcd, sokocd, touNo, situNo)

                    '棟室マスタが取得出来ない場合、危険物チェックを行わない
                    If tousituDr.Length.Equals(0) Then
                        Continue For
                    End If

                    '自社他社情報を取得する
                    Dim isTasya As Boolean = tousituDr(0).Item("JISYATASYA_KB").ToString.Equals("02")

                    Dim kanriNo As String = String.Concat(" (", inakMNo, "-", "", drS(j).Item("INKA_NO_S").ToString, ") ")

                    '判定
                    '自社の場合チェックを行う
                    If isTasya.Equals(False) Then

                        '属性系チェック
                        'DataSet設定
                        chkDs = New LMZ340DS()
                        row = chkDs.Tables(LMZ340C.TABLE_NM_IN).NewRow
                        inTbl = chkDs.Tables(LMZ340C.TABLE_NM_IN)

                        row("NRS_BR_CD") = nrsbrcd
                        row("GOODS_CD_NRS") = goodsNRS
                        row("WH_CD") = sokocd
                        row("TOU_NO") = touNo
                        row("SITU_NO") = situNo
                        row("ZONE_CD") = zoneCd

                        inTbl.Rows.Add(row)

                        chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCheckAttr", chkDs)

                        msg = String.Concat(touNo, "-", situNo, "-", zoneCd, kanriNo)

                        If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows.Count > 0 Then
                            Dim DokuKbErr As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows(0).Item("DOKU_KB_ERR").ToString
                            Dim KouathuGasKbErr As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows(0).Item("KOUATHUGAS_KB_ERR").ToString
                            Dim YakuzihoKbErr As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows(0).Item("YAKUZIHO_KB_ERR").ToString
                            Dim ShoboCdErr As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows(0).Item("SHOBO_CD_ERR").ToString

                            'フラグが1で設定されている場合、エラー
                            If "1".Equals(DokuKbErr) Then
                                If (MyBase.ShowMessage(frm, "W299", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                            End If
                            If "1".Equals(KouathuGasKbErr) Then
                                If (MyBase.ShowMessage(frm, "W300", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                            End If
                            If "1".Equals(YakuzihoKbErr) Then
                                If (MyBase.ShowMessage(frm, "W301", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                            End If
                            If "1".Equals(ShoboCdErr) Then
                                If (MyBase.ShowMessage(frm, "W302", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                            End If
                        End If

                    End If

                Next

            Next

            '★★★最大保管数量チェック
            '入荷(小)を棟室順に処理
            Dim drSCapa As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(Nothing, "TOU_NO ASC, SITU_NO ASC")
            Dim maxSCapa As Integer = drSCapa.Length - 1

            '入荷(小)がない場合は終了
            If 0 > maxSCapa Then Return True

            'ブレイクキー
            Dim keyTouNo As String = String.Empty
            Dim keySituNo As String = String.Empty
            Dim keyTouSituSkip As Boolean = False

            '棟室の貯蔵最大数量
            Dim MaxQty As Decimal
            '棟室の現在の在庫の数量
            Dim ZaiQty As Decimal
            '入庫可能商品数量
            Dim chkQty As Decimal

            '棟室マスタ
            For k As Integer = 0 To maxSCapa

                '行削除されているデータはチェックしない
                If LMConst.FLG.ON.Equals(drSCapa(k).Item("SYS_DEL_FLG").ToString()) Then
                    Continue For
                End If

                If Not keyTouNo.Equals(drSCapa(k).Item("TOU_NO").ToString()) _
                    OrElse Not keySituNo.Equals(drSCapa(k).Item("SITU_NO").ToString()) Then

                    '棟室が変わったら、最大保管数量を取得
                    keyTouNo = drSCapa(k).Item("TOU_NO").ToString()
                    keySituNo = drSCapa(k).Item("SITU_NO").ToString()

                    MaxQty = 0
                    ZaiQty = 0
                    keyTouSituSkip = False

                    msg = String.Concat(keyTouNo, "-", keySituNo)

                    'DataSet設定
                    chkDs = New LMZ340DS()
                    row = chkDs.Tables(LMZ340C.TABLE_NM_IN).NewRow
                    inTbl = chkDs.Tables(LMZ340C.TABLE_NM_IN)

                    row("NRS_BR_CD") = nrsbrcd
                    row("WH_CD") = sokocd
                    row("TOU_NO") = keyTouNo
                    row("SITU_NO") = keySituNo
                    row("INKA_NO_L") = inakLNo  '編集中の入荷データは除外

                    inTbl.Rows.Add(row)

                    '貯蔵最大数量検索
                    chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCheckCapa", chkDs)

                    If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_CAPA).Rows.Count > 0 Then
                        MaxQty = Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_CAPA).Rows(0).Item("MAX_QTY").ToString)
                        ZaiQty = Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_CAPA).Rows(0).Item("ZAI_QTY").ToString)
                    End If

                    '貯蔵最大数量 が 0 の場合、チェック対象外とする。
                    If MaxQty <= 0 Then
                        keyTouSituSkip = True
                    End If

                    '入庫可能商品数量を計算
                    chkQty = MaxQty - ZaiQty

                End If

                '貯蔵最大数量 が 0 または 既にエラーとなっている棟室はスキップ
                If keyTouSituSkip = False Then

                    '対象商品数量計算
                    'DataSet設定
                    chkDs = New LMZ340DS()
                    row = chkDs.Tables(LMZ340C.TABLE_NM_IN).NewRow
                    inTbl = chkDs.Tables(LMZ340C.TABLE_NM_IN)

                    '入荷(中)の対象行
                    inakMNo = drSCapa(k).Item("INKA_NO_M").ToString()
                    Dim sqlMCapa As String = String.Concat("NRS_BR_CD = ", " '", nrsbrcd, "' ",
                                                 " AND INKA_NO_L = ", " '", inakLNo, "' ",
                                                 " AND INKA_NO_M = ", " '", inakMNo, "' ")

                    Dim drMCapa As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(sqlMCapa)

                    row("NRS_BR_CD") = drMCapa(0).Item("NRS_BR_CD").ToString()
                    row("GOODS_CD_NRS") = drMCapa(0).Item("GOODS_CD_NRS").ToString()
                    row("INKA_NB") = drSCapa(k).Item("KOSU_S").ToString()

                    inTbl.Rows.Add(row)

                    '入庫商品数量を計算
                    chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCalcQty", chkDs)
                    'UPD 2021/10/04 024123 【LMS】危険物管理_第2弾_アラート機能実装_再実装
                    '危険品でない場合処理しない
                    If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows.Count > 0 AndAlso
                        Not Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows(0).Item("INK_QTY").ToString).Equals(0) Then

                        If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows.Count > 0 Then
                            '入庫可能商品数量から計算した入庫商品数量をマイナス
                            chkQty = chkQty - Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows(0).Item("INK_QTY").ToString)
                        End If

                        '入庫商品数量 >入庫可能商品数量 の場合
                        If chkQty < 0 Then
                            If (MyBase.ShowMessage(frm, "W298", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                            keyTouSituSkip = True
                        End If

                    End If
                End If

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 温度管理アラートチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり(本チェックは常に「True:エラーなし」を返す)</returns>
    ''' <remarks></remarks>
    Private Function IsOndoKanriAlertCheck(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean

        With frm

            Dim nrsBrCd As String = .cmbEigyo.SelectedValue.ToString
            Dim inkaDate As String = .imdNyukaDate.TextValue

            ' 入荷(中)
            Dim dtM As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)
            Dim maxM As Integer = dtM.Rows.Count - 1

            ' 入荷(中) がない場合は終了
            If 0 > maxM Then Return True

            Dim goodsCdNrs As String = String.Empty
            Dim chkedGoodsCdNrsSet As New HashSet(Of String)

            Dim chkDs As DataSet = New LMZ370DS()

            For i As Integer = 0 To maxM

                ' 行削除されているデータはチェックしない。
                If LMConst.FLG.ON.Equals(dtM.Rows(i).Item("SYS_DEL_FLG").ToString()) Then
                    Continue For
                End If

                ' チェック済みの商品の再度のチェックは行わない。
                goodsCdNrs = dtM.Rows(i).Item("GOODS_CD_NRS").ToString()
                If chkedGoodsCdNrsSet.Contains(goodsCdNrs) Then
                    Continue For
                End If
                chkedGoodsCdNrsSet.Add(goodsCdNrs)

                ' 温度管理アラートチェック
                ' DataSet設定
                Dim row As DataRow = chkDs.Tables(LMZ370C.TABLE_NM_IN).NewRow()
                Dim inTbl As DataTable = chkDs.Tables(LMZ370C.TABLE_NM_IN)

                row("NRS_BR_CD") = nrsBrCd
                row("INOUTKA_DATE") = inkaDate
                row("GOODS_CD_NRS") = goodsCdNrs

                inTbl.Rows.Add(row)
            Next

            chkDs = MyBase.CallWSA("LMZ370BLF", "SelectGoodsAndDetails", chkDs)

            ' 温度管理アラートチェック対象商品がない場合は終了
            If chkDs.Tables(LMZ370C.TABLE_NM_OUT).Rows.Count = 0 Then Return True

            ' 警告対象商品が 1件以上存在する場合
            ' 商品名を付与した警告メッセージの表示
            Dim goodsNmMsg As New Text.StringBuilder
            For Each dr As DataRow In chkDs.Tables(LMZ370C.TABLE_NM_OUT).Rows()
                goodsNmMsg.Append(String.Concat(vbCrLf, dr.Item("GOODS_NM").ToString()))
            Next
            Call MyBase.ShowMessage(frm, "W314", New String() {goodsNmMsg.ToString()})

        End With

        Return True

    End Function

    ''' <summary>
    ''' タブレット項目の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetTabletItemData(ByVal frm As LMB020F, ByVal ds As DataSet, ByVal actionType As LMB020C.ActionType) As Boolean

        If LMB020C.ActionType.SAVE <> actionType Then
            Return True
        End If

        With frm

            Dim ilTbl As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_L)
            Dim nrsBrCd As String = ilTbl.Rows(0).Item("NRS_BR_CD").ToString
            Dim whCd As String = ilTbl.Rows(0).Item("WH_CD").ToString
            Dim sokoDr() As DataRow = Nothing
            sokoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                                                                             "WH_CD = '", whCd, "' "))
            'ロケ管理対象外の倉庫の場合は現場作業なし
            If sokoDr.Length > 0 Then
                If "00".Equals(sokoDr(0).Item("LOC_MANAGER_YN").ToString) Then
                    ilTbl.Rows(0).Item("WH_TAB_YN") = "00"
                End If
            End If

            '出荷Sすべてが他社倉庫保管の商品の場合は現場作業なし
            Dim isTbl As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S)
            Dim jisya As Boolean = False
            Dim tasya As Boolean = False
            For Each isRow As DataRow In isTbl.Rows
                If LMConst.FLG.OFF.Equals(isRow.Item("SYS_DEL_FLG").ToString) Then
                    Dim tsRow() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TOU_SITU).Select( _
                                                 String.Concat("NRS_BR_CD = '", isRow.Item("NRS_BR_CD").ToString, "'", _
                                                          " AND TOU_NO    = '", isRow.Item("TOU_NO").ToString, "'", _
                                                          " AND SITU_NO   = '", isRow.Item("SITU_NO").ToString, "'", _
                                                          " AND WH_CD     = '", ilTbl.Rows(0).Item("WH_CD").ToString, "'"))
                    If tsRow.Length = 0 Then
                        Continue For
                    End If
                    If "02".Equals(tsRow(0).Item("JISYATASYA_KB").ToString) Then
                        tasya = True
                    Else
                        jisya = True
                    End If
                End If
            Next
            If tasya = True AndAlso jisya = False Then
                ilTbl.Rows(0).Item("WH_TAB_YN") = "00"
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 変更確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CompareDataset() As Boolean

        '入荷L
        Dim dtInL As DataTable = _Ds.Tables(LMB020C.TABLE_NM_INKA_L)
        Dim dtInLCmpr As DataTable = _DsCmpr.Tables(LMB020C.TABLE_NM_INKA_L)

        If Not dtInL.Rows(0).Item("INKA_DATE").Equals(dtInLCmpr.Rows(0).Item("INKA_DATE")) Then
            Return True
        End If
        If Not dtInL.Rows(0).Item("BUYER_ORD_NO_L").Equals(dtInLCmpr.Rows(0).Item("BUYER_ORD_NO_L")) Then
            Return True
        End If
        If Not dtInL.Rows(0).Item("OUTKA_FROM_ORD_NO_L").Equals(dtInLCmpr.Rows(0).Item("OUTKA_FROM_ORD_NO_L")) Then
            Return True
        End If
        If Not dtInL.Rows(0).Item("REMARK").Equals(dtInLCmpr.Rows(0).Item("REMARK")) Then
            Return True
        End If
        If Not dtInL.Rows(0).Item("REMARK_OUT").Equals(dtInLCmpr.Rows(0).Item("REMARK_OUT")) Then
            Return True
        End If
        If Not dtInL.Rows(0).Item("WH_TAB_YN").Equals(dtInLCmpr.Rows(0).Item("WH_TAB_YN")) Then
            Return True
        End If

        '運送会社
        Dim dtUnso As DataTable = _Ds.Tables(LMB020C.TABLE_NM_UNSO_L)
        Dim dtUnsoCmpr As DataTable = _DsCmpr.Tables(LMB020C.TABLE_NM_UNSO_L)
        If Not (LMConst.FLG.OFF.Equals(dtUnso.Rows(0).Item("UP_KBN").ToString()) AndAlso _
                LMConst.FLG.ON.Equals(dtUnso.Rows(0).Item("SYS_DEL_FLG").ToString())) Then
            If dtUnso.Rows.Count <> dtUnsoCmpr.Rows.Count Then
                Return True
            End If
            If Not dtUnsoCmpr.Rows(0).Item("UNSO_CD").Equals(dtUnso.Rows(0).Item("UNSO_CD")) Then
                Return True
            End If
            If Not dtUnsoCmpr.Rows(0).Item("UNSO_BR_CD").Equals(dtUnso.Rows(0).Item("UNSO_BR_CD")) Then
                Return True
            End If

        End If


        '入荷M
        Dim dtInM As DataTable = _Ds.Tables(LMB020C.TABLE_NM_INKA_M)
        Dim dtInMCmpr As DataTable = _DsCmpr.Tables(LMB020C.TABLE_NM_INKA_M)
        If dtInM.Rows.Count <> dtInMCmpr.Rows.Count Then
            Return True
        End If
        For i As Integer = 0 To dtInM.Rows.Count - 1
            If Not dtInM.Rows(0).Item("INKA_NO_M").Equals(dtInMCmpr.Rows(0).Item("INKA_NO_M")) Then
                Return True
            End If
        Next


        '入荷小
        Dim dtInS As DataTable = _Ds.Tables(LMB020C.TABLE_NM_INKA_S)
        Dim dtInSCmpr As DataTable = _DsCmpr.Tables(LMB020C.TABLE_NM_INKA_S)
        If dtInM.Rows.Count <> dtInMCmpr.Rows.Count Then
            Return True
        End If
        For i As Integer = 0 To dtInSCmpr.Rows.Count - 1
            Dim drInS() As DataRow = dtInS.Select(String.Concat("INKA_NO_L = '", dtInSCmpr.Rows(i).Item("INKA_NO_L").ToString, _
                                                                "' AND INKA_NO_M = '", dtInSCmpr.Rows(i).Item("INKA_NO_M").ToString, _
                                                                "' AND INKA_NO_S = '", dtInSCmpr.Rows(i).Item("INKA_NO_S").ToString, "' "))
            'Key合致するものがなければ新規追加とみなす
            If drInS.Length = 0 Then
                Return True
            End If
            'ロット
            If Not drInS(0).Item("LOT_NO").Equals(dtInSCmpr.Rows(i).Item("LOT_NO")) Then
                Return True
            End If
            '棟
            If Not drInS(0).Item("TOU_NO").Equals(dtInSCmpr.Rows(i).Item("TOU_NO")) Then
                Return True
            End If
            '室
            If Not drInS(0).Item("SITU_NO").Equals(dtInSCmpr.Rows(i).Item("SITU_NO")) Then
                Return True
            End If
            'ZONE
            If Not drInS(0).Item("ZONE_CD").Equals(dtInSCmpr.Rows(i).Item("ZONE_CD")) Then
                Return True
            End If
            'LOCA
            If Not drInS(0).Item("LOCA").Equals(dtInSCmpr.Rows(i).Item("LOCA")) Then
                Return True
            End If
            '梱数
            If Not drInS(0).Item("KONSU").Equals(dtInSCmpr.Rows(i).Item("KONSU")) Then
                Return True
            End If
            '端数
            If Not drInS(0).Item("HASU").Equals(dtInSCmpr.Rows(i).Item("HASU")) Then
                Return True
            End If
            '入目
            If Not drInS(0).Item("IRIME").Equals(dtInSCmpr.Rows(i).Item("IRIME")) Then
                Return True
            End If
            'シリアル
            If Not drInS(0).Item("SERIAL_NO").Equals(dtInSCmpr.Rows(i).Item("SERIAL_NO")) Then
                Return True
            End If

        Next

        '作業
        Dim dtSagyo As DataTable = _Ds.Tables(LMB020C.TABLE_NM_SAGYO)
        Dim dtSagyoCmpr As DataTable = _DsCmpr.Tables(LMB020C.TABLE_NM_SAGYO)
        '件数
        If dtSagyo.Rows.Count <> dtSagyoCmpr.Rows.Count Then
            Return True
        End If
        For i As Integer = 0 To dtSagyo.Rows.Count - 1
            '作業レコードNo
            If Not dtSagyo.Rows(i).Item("SAGYO_REC_NO").Equals(dtSagyoCmpr.Rows(i).Item("SAGYO_REC_NO")) Then
                Return True
            End If
            '作業CD
            If Not dtSagyo.Rows(i).Item("SAGYO_CD").Equals(dtSagyoCmpr.Rows(i).Item("SAGYO_CD")) Then
                Return True
            End If
            '現場作業用備考
            If Not dtSagyo.Rows(i).Item("REMARK_SIJI").Equals(dtSagyoCmpr.Rows(i).Item("REMARK_SIJI")) Then
                Return True
            End If
            '削除行(作業L)
            If "2".Equals(dtSagyo(i).Item("UP_KBN").ToString) AndAlso _
               "01".Equals(dtSagyo(i).Item("SYS_DEL_FLG").ToString) Then
                Return True
            End If

        Next

    End Function

    ''' <summary>
    ''' 現場作業指示ステータス更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetWhhSijiStatus(ByVal frm As LMB020F)

        If Not ("0".Equals(frm.lblSituation.RecordStatus) AndAlso _
            DispMode.EDIT.Equals(frm.lblSituation.DispMode)) Then
            Exit Sub
        End If

        Dim dt As DataTable = _Ds.Tables(LMB020C.TABLE_NM_INKA_L)

        '現場作業指示ステータスの確認
        If LMB020C.WH_TAB_SIJI_00.Equals(dt.Rows(0).Item("WH_TAB_SAGYO_SIJI_STATUS")) Then
            '未指示の場合は処理なし
            Exit Sub
        End If

        '再指示不要チェックの確認
        If "01".Equals(dt.Rows(0).Item("WH_TAB_NO_SIJI_FLG")) Then
            'チェック有の場合、現場作業指示ステータスを確認
            If LMB020C.WH_TAB_SIJI_02.Equals(dt.Rows(0).Item("WH_TAB_SAGYO_SIJI_STATUS")) Then
                '現場作業指示ステータスが指示後変更の場合、指示済みに変更
                dt.Rows(0).Item("WH_TAB_SAGYO_SIJI_STATUS") = LMB020C.WH_TAB_SIJI_01
            End If
            '再指示不要チェック有の場合は処理終了
            Exit Sub
        End If

        'データテーブルの比較
        If CompareDataset() = True Then
            dt.Rows(0).Item("WH_TAB_SAGYO_SIJI_STATUS") = LMB020C.WH_TAB_SIJI_02
        End If

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class