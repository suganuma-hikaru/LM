' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運賃
'  プログラムID     :  LMF010C : 運行・運送情報
'  作  成  者       :  kishi
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.Win.Base   '2017/09/25 追加 李
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李
Imports GrapeCity.Win.Editors

''' <summary>
''' LMF010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF010F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconG As LMFControlG

    Friend objSprDef As Object = Nothing
    Friend sprUnsoUnkouDef As sprUnsoUnkouDefault

    '2017/09/25 修正 李↓
    ''2016.01.06 UMANO 英語化対応START
    'Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    ''2016.01.06 UMANO 英語化対応END
    '2017/09/25 修正 李↑

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF010F, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMFconG = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            '(2013.01.17)要望番号1617 -- START --
            '.F1ButtonName = String.Empty
            .F1ButtonName = LMFControlC.FUNCTION_OUTKA
            '(2013.01.17)要望番号1617 --  END  --
            .F2ButtonName = String.Empty
            'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
            '.F3ButtonName = String.Empty
            .F3ButtonName = LMFControlC.FUNCTION_UNSOCOPY
            'END YANAI 要望番号1241 運送検索：運送複写機能を追加する
            '2022.08.22 修正START
            '.F4ButtonName = String.Empty
            .F4ButtonName = LMFControlC.FUNCTION_DATASEND
            '2022.08.22 修正END
            .F5ButtonName = LMFControlC.FUNCTION_UNCONEW
            .F6ButtonName = LMFControlC.FUNCTION_UNCOEDIT
            .F7ButtonName = LMFControlC.FUNCTION_UNSONEW
            '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 --- STRAT ---
            '.F8ButtonName = String.Empty
            .F8ButtonName = LMFControlC.FUNCTION_SYASAIU
            '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 ---  END  ---
            .F9ButtonName = LMFControlC.FUNCTION_KENSAKU
            .F10ButtonName = LMFControlC.FUNCTION_POP
            .F11ButtonName = String.Empty
            .F12ButtonName = LMFControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            '(2013.01.17)要望番号1617 -- START --
            '.F1ButtonEnabled = lock
            .F1ButtonEnabled = always
            '(2013.01.17)要望番号1617 --  END  --
            .F2ButtonEnabled = lock
            'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
            '.F3ButtonEnabled = lock
            .F3ButtonEnabled = always
            'END YANAI 要望番号1241 運送検索：運送複写機能を追加する
            '2022.08.22 修正START
            '.F4ButtonEnabled = lock
            .F4ButtonEnabled = always
            '2022.08.22 修正END
            .F5ButtonEnabled = always
            .F6ButtonEnabled = always
            .F7ButtonEnabled = always
            '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 --- STRAT ---
            '.F8ButtonEnabled = lock
            .F8ButtonEnabled = always
            '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 ---  END  ---
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .cmbEigyo.TabIndex = LMF010C.CtlTabIndex.EIGYO
            .cmbBetsuEigyo.TabIndex = LMF010C.CtlTabIndex.BETSUEIGYO
            .txtUnsocoCd1.TabIndex = LMF010C.CtlTabIndex.UNSOCO_CD1
            .txtUnsocoBrCd1.TabIndex = LMF010C.CtlTabIndex.UNSOCO_BR_CD1
            .lblUnsocoNm1.TabIndex = LMF010C.CtlTabIndex.UNSOCO_NM1
            .txtUnsocoCd2.TabIndex = LMF010C.CtlTabIndex.UNSOCO_CD2
            .txtUnsocoBrCd2.TabIndex = LMF010C.CtlTabIndex.UNSOCO_BR_CD2
            .lblUnsocoNm2.TabIndex = LMF010C.CtlTabIndex.UNSOCO_NM2
            .txtCustCdL.TabIndex = LMF010C.CtlTabIndex.CUST_CDL
            .txtCustCdM.TabIndex = LMF010C.CtlTabIndex.CUST_CDM
            .lblCustNm.TabIndex = LMF010C.CtlTabIndex.CUST_NM
            .cmbJshaKb.TabIndex = LMF010C.CtlTabIndex.JSHA_KB
            .cmbDateKb.TabIndex = LMF010C.CtlTabIndex.DATE_KB
            .imdTripDateFrom.TabIndex = LMF010C.CtlTabIndex.TRIPDATE_FROM
            .imdTripDateTo.TabIndex = LMF010C.CtlTabIndex.TRIPDATE_TO
            .txtCntUserCd.TabIndex = LMF010C.CtlTabIndex.CNTUSER_CD
            .lblCntUserNm.TabIndex = LMF010C.CtlTabIndex.CNTUSER_NM
            .pnlTrip.TabIndex = LMF010C.CtlTabIndex.TRIP
            .optTripN.TabIndex = LMF010C.CtlTabIndex.TRIP_N
            .optTripY.TabIndex = LMF010C.CtlTabIndex.TRIP_Y
            .optTripAll.TabIndex = LMF010C.CtlTabIndex.TRIP_ALL
            .pnlChukei.TabIndex = LMF010C.CtlTabIndex.TYUKEI
            .optChukeiN.TabIndex = LMF010C.CtlTabIndex.CHUKEI_N
            .optChukeiY.TabIndex = LMF010C.CtlTabIndex.CHUKEI_Y
            .optChukeiAll.TabIndex = LMF010C.CtlTabIndex.CHUKEI_ALL
            .pnlUnkoEdit.TabIndex = LMF010C.CtlTabIndex.PNL_UNKO
            .cmbHaiso.TabIndex = LMF010C.CtlTabIndex.HAISO
            .btnUnkoEdit.TabIndex = LMF010C.CtlTabIndex.BTN_UNKO
            'START YANAI 要望番号737 運送検索画面：全体が見えるようにする
            .cmbVisibleKb.TabIndex = LMF010C.CtlTabIndex.VISIBLEKB
            'END YANAI 要望番号737 運送検索画面：全体が見えるようにする
            .pnlEdit.TabIndex = LMF010C.CtlTabIndex.EDIT
            .pnlEvent.TabIndex = LMF010C.CtlTabIndex.PNL_EVENT
            .optEventY.TabIndex = LMF010C.CtlTabIndex.EVENT_Y
            .optEventN.TabIndex = LMF010C.CtlTabIndex.EVENT_N
            .cmbShuSei.TabIndex = LMF010C.CtlTabIndex.SHUSEI
            .btnHenko.TabIndex = LMF010C.CtlTabIndex.HENKO
            .txtTripNo.TabIndex = LMF010C.CtlTabIndex.TRIP_NO
            .cmbBinKb.TabIndex = LMF010C.CtlTabIndex.BIN_KB
            .txtUnsocoCd0.TabIndex = LMF010C.CtlTabIndex.UNSOCO_CD0
            .txtUnsocoBrCd0.TabIndex = LMF010C.CtlTabIndex.UNSOCO_BR_CD0
            .lblUnsocoNm0.TabIndex = LMF010C.CtlTabIndex.UNSOCO_NM0
            .cmbChukeiFrom.TabIndex = LMF010C.CtlTabIndex.CHUKEI_FROM
            .cmbChukeiTo.TabIndex = LMF010C.CtlTabIndex.CHUKEI_TO
            'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
            .pnlFukusha.TabIndex = LMF010C.CtlTabIndex.PNL_FUKUSHA
            .imdOrigDate.TabIndex = LMF010C.CtlTabIndex.ORIG_DATE
            .imdDestDate.TabIndex = LMF010C.CtlTabIndex.DEST_DATE
            'END YANAI 要望番号1241 運送検索：運送複写機能を追加する
            .cmbPrintKb.TabIndex = LMF010C.CtlTabIndex.PRINT_KB
            .btnPrint.TabIndex = LMF010C.CtlTabIndex.BTN_PRINT
            .sprUnsoUnkou.TabIndex = LMF010C.CtlTabIndex.SPR

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal ds As DataSet)

        '項目をクリア
        Call Me.ClearControl()

        '初期値設定
        Call Me.SetInitData()

        'ラジオボタンの初期値
        Call Me.OptChk()

        '修正コンボ生成
        Call Me.CreateComboBox()

        '中継地コンボ生成
        Call Me.CreateJisComboBox(ds)

        'ロック制御
        Call Me.EventLockControl()

    End Sub

    ''' <summary>
    ''' 初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInitData()

        With Me._Frm

            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd
            .cmbBetsuEigyo.SelectedValue = brCd

            '2014.08.04 FFEM高取対応 START
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

            If nrsDr.Item("LOCK_FLG").ToString.Equals("01") Then
                Me._Frm.cmbEigyo.ReadOnly = True
                Me._Frm.cmbBetsuEigyo.ReadOnly = True
            Else
                Me._Frm.cmbEigyo.ReadOnly = False
                Me._Frm.cmbBetsuEigyo.ReadOnly = False
            End If
            '2014.08.04 FFEM高取対応 END

            '初期荷主から値取得
            Dim drs As DataRow() = Me._LMFconG.SelectTCustListDataRow(LMUserInfoManager.GetUserID())
            If 0 < drs.Length Then

                Dim custCdL As String = drs(0).Item("CUST_CD_L").ToString()
                Dim custCdM As String = drs(0).Item("CUST_CD_M").ToString()
                .txtCustCdL.TextValue = custCdL
                .txtCustCdM.TextValue = custCdM
                'drs = Me._LMFconG.SelectCustListDataRow(custCdL, custCdM, LMFControlC.FLG_OFF, LMFControlC.FLG_OFF) 
                drs = Me._LMFconG.SelectCustListDataRow(brCd, custCdL, custCdM, LMFControlC.FLG_OFF, LMFControlC.FLG_OFF) '20160928 要番2622 tsunehira add

                If 0 < drs.Length Then
                    .lblCustNm.TextValue = Me._LMFconG.EditConcatData(drs(0).Item("CUST_NM_L").ToString(), drs(0).Item("CUST_NM_M").ToString(), LMFControlC.ZENKAKU_SPACE)
                End If

            End If

            'START YANAI 要望番号737 運送検索画面：全体が見えるようにする
            .cmbVisibleKb.SelectedValue = "01"
            'END YANAI 要望番号737 運送検索画面：全体が見えるようにする

            'START YANAI 20120615 外部権限の変更(春日部対応)
            If (LMConst.AuthoKBN.AGENT).Equals(LMUserInfoManager.GetAuthoLv()) = True Then
                '外部権限の場合
                .cmbVisibleKb.ReadOnly = True
                'START KIM 20120912 外部権限時、初期値の変更（要望番号：1261）
                'Loginユーザの権限が『外部』の場合、項目表示を『協力会社』に設定し、ロックする
                .cmbVisibleKb.SelectedValue = "02"
                'END KIM 20120912 外部権限時、初期値の変更（要望番号：1261）
                'SHINODA ADD 2017/4/25 要望管理2696対応 Start
                Me._Frm.cmbEigyo.ReadOnly = True
                Me._Frm.cmbBetsuEigyo.ReadOnly = True
                'SHINODA ADD 2017/4/25 要望管理2696対応 End

            End If
            'END YANAI 20120615 外部権限の変更(春日部対応)

        End With

    End Sub

    ''' <summary>
    ''' ラジオボタンの初期値
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OptChk()

        With Me._Frm

            .optChukeiN.Checked = True
            .optTripAll.Checked = True
            .optEventY.Checked = True

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            'フォーカス位置初期化
            .Focus()

            .cmbEigyo.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbEigyo.SelectedValue = Nothing
            .cmbBetsuEigyo.SelectedValue = Nothing
            .txtUnsocoCd1.TextValue = String.Empty
            .txtUnsocoBrCd1.TextValue = String.Empty
            .lblUnsocoNm1.TextValue = String.Empty
            .txtUnsocoCd2.TextValue = String.Empty
            .txtUnsocoBrCd2.TextValue = String.Empty
            .lblUnsocoNm2.TextValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNm.TextValue = String.Empty
            .cmbJshaKb.SelectedValue = Nothing
            .cmbDateKb.SelectedValue = Nothing
            .imdTripDateFrom.TextValue = Nothing
            .imdTripDateTo.TextValue = Nothing
            .txtCntUserCd.TextValue = String.Empty
            .lblCntUserNm.TextValue = String.Empty
            .cmbShuSei.SelectedValue = Nothing
            .cmbHaiso.SelectedValue = Nothing
            .txtTripNo.TextValue = String.Empty
            .cmbBinKb.SelectedValue = Nothing
            .txtUnsocoCd0.TextValue = String.Empty
            .txtUnsocoBrCd0.TextValue = String.Empty
            .lblUnsocoNm0.TextValue = String.Empty
            .cmbChukeiFrom.SelectedValue = Nothing
            .cmbChukeiTo.SelectedValue = Nothing
            'START YANAI 要望番号737 運送検索画面：全体が見えるようにする
            .cmbVisibleKb.SelectedValue = Nothing
            'END YANAI 要望番号737 運送検索画面：全体が見えるようにする

        End With

    End Sub

    ''' <summary>
    ''' 修正コンボのリスト作成
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateComboBox()

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'コンボ生成のSQLを構築
        Dim sql As String = String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_S057, "' AND VALUE1 = '1.000'")
        If Me._Frm.optEventN.Checked = True Then
            sql = String.Concat(sql, " AND KBN_NM3 = '1' ")
        End If

        '2017/09/25 修正 李↓
        'コンボ生成
        Call Me._LMFconG.CreateComboBox(Me._Frm.cmbShuSei, LMConst.CacheTBL.KBN, New String() {"KBN_CD"}, New String() {lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})}, sql, "KBN_CD")
        '2017/09/25 修正 李↑

    End Sub

    ''' <summary>
    ''' イベントオプションボタンによるロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub EventLockControl()

        With Me._Frm

            '解除を選択した場合、全てロック
            If .optEventN.Checked = True Then

                Dim lock As Boolean = True
                Call Me._LMFconG.SetLockInputMan(.txtTripNo, lock)
                Call Me._LMFconG.SetLockInputMan(.txtUnsocoCd0, lock)
                Call Me._LMFconG.SetLockInputMan(.txtUnsocoBrCd0, lock)
                Call Me._LMFconG.SetLockInputMan(.lblUnsocoNm0, lock)
                Call Me._LMFconG.SetLockInputMan(.cmbBinKb, lock)
                Call Me._LMFconG.SetLockInputMan(.cmbChukeiFrom, lock)
                Call Me._LMFconG.SetLockInputMan(.cmbChukeiTo, lock)
                Exit Sub

            End If

            '修正項目の値によるロック制御
            Call Me.ShuseiLockControl()

        End With

    End Sub

    ''' <summary>
    ''' 修正項目によるロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ShuseiLockControl()

        With Me._Frm

            '解除を選択している場合、スルー
            If .optEventN.Checked = True Then
                Exit Sub
            End If

            Dim lock As Boolean = True
            Dim unLock As Boolean = False
            Select Case .cmbShuSei.SelectedValue.ToString()

                Case LMF010C.SHUSEI_TRIP

                    Call Me._LMFconG.SetLockInputMan(.txtTripNo, unLock)
                    Call Me._LMFconG.SetLockInputMan(.txtUnsocoCd0, lock)
                    Call Me._LMFconG.SetLockInputMan(.txtUnsocoBrCd0, lock)
                    Call Me._LMFconG.SetLockInputMan(.lblUnsocoNm0, lock)
                    Call Me._LMFconG.SetLockInputMan(.cmbBinKb, lock)
                    Call Me._LMFconG.SetLockInputMan(.cmbChukeiFrom, lock)
                    Call Me._LMFconG.SetLockInputMan(.cmbChukeiTo, lock)

                Case LMF010C.SHUSEI_BIN

                    Call Me._LMFconG.SetLockInputMan(.txtTripNo, lock)
                    Call Me._LMFconG.SetLockInputMan(.txtUnsocoCd0, lock)
                    Call Me._LMFconG.SetLockInputMan(.txtUnsocoBrCd0, lock)
                    Call Me._LMFconG.SetLockInputMan(.lblUnsocoNm0, lock)
                    Call Me._LMFconG.SetLockInputMan(.cmbBinKb, unLock)
                    Call Me._LMFconG.SetLockInputMan(.cmbChukeiFrom, lock)
                    Call Me._LMFconG.SetLockInputMan(.cmbChukeiTo, lock)

                Case LMF010C.SHUSEI_UNSOCO

                    Call Me._LMFconG.SetLockInputMan(.txtTripNo, lock)
                    Call Me._LMFconG.SetLockInputMan(.txtUnsocoCd0, unLock)
                    Call Me._LMFconG.SetLockInputMan(.txtUnsocoBrCd0, unLock)
                    Call Me._LMFconG.SetLockInputMan(.lblUnsocoNm0, lock)
                    Call Me._LMFconG.SetLockInputMan(.cmbBinKb, lock)
                    Call Me._LMFconG.SetLockInputMan(.cmbChukeiFrom, lock)
                    Call Me._LMFconG.SetLockInputMan(.cmbChukeiTo, lock)

                Case LMF010C.SHUSEI_CHUKEI

                    Call Me._LMFconG.SetLockInputMan(.txtTripNo, lock)
                    Call Me._LMFconG.SetLockInputMan(.txtUnsocoCd0, lock)
                    Call Me._LMFconG.SetLockInputMan(.txtUnsocoBrCd0, lock)
                    Call Me._LMFconG.SetLockInputMan(.lblUnsocoNm0, lock)
                    Call Me._LMFconG.SetLockInputMan(.cmbBinKb, lock)
                    Call Me._LMFconG.SetLockInputMan(.cmbChukeiFrom, unLock)
                    Call Me._LMFconG.SetLockInputMan(.cmbChukeiTo, unLock)

                Case Else

                    Call Me._LMFconG.SetLockInputMan(.txtTripNo, lock)
                    Call Me._LMFconG.SetLockInputMan(.txtUnsocoCd0, lock)
                    Call Me._LMFconG.SetLockInputMan(.txtUnsocoBrCd0, lock)
                    Call Me._LMFconG.SetLockInputMan(.lblUnsocoNm0, lock)
                    Call Me._LMFconG.SetLockInputMan(.cmbBinKb, lock)
                    Call Me._LMFconG.SetLockInputMan(.cmbChukeiFrom, lock)
                    Call Me._LMFconG.SetLockInputMan(.cmbChukeiTo, lock)

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 中継地のJISコンボボックス生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub CreateJisComboBox(ByVal ds As DataSet)

        With Me._Frm

            Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_CMB)
            Dim max As Integer = dt.Rows.Count - 1

            'リストのクリア
            .cmbChukeiFrom.Items.Clear()
            .cmbChukeiTo.Items.Clear()

            Dim cd As String = String.Empty
            Dim item As String = String.Empty

            '空行追加
            .cmbChukeiFrom.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))
            .cmbChukeiTo.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))
            For i As Integer = 0 To max

                cd = dt.Rows(i).Item("CD").ToString()
                item = dt.Rows(i).Item("NM").ToString()

                'アイテム追加
                .cmbChukeiFrom.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))
                .cmbChukeiTo.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))

            Next

        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprUnsoUnkouDefault

        'START YANAI 要望番号737 運送検索画面：全体が見えるようにする
        'スプレッド(タイトル列)の設定
        'Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.DEF, " ", 20, True)
        'Public Shared UNSO_NO_L As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSO_NO_L, "運送番号", 80, True)
        'Public Shared BIN_KB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.BIN_KB, "便区分", 90, True)
        'Public Shared BUNRUI As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.BUNRUI, "タリフ分類", 90, True)
        'Public Shared TARIFF_BUNRUI_KB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TARIFF_BUNRUI_KB, "タリフ分類区分", 0, False)
        'Public Shared UNSOCO_CD_2 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_CD_2, "運送会社(2次)コード", 0, False)
        'Public Shared UNSOCO_BR_CD_2 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_CD_2, "運送会社(2次)支店コード", 0, False)
        'Public Shared UNSOCO_NM_2 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_NM_2, "会社(2次)名", 0, False)
        'Public Shared UNSOCO_BR_NM_2 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_NM_2, "支店(2次)名", 0, False)
        'Public Shared UNSOCO_2 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_2, "運送会社(2次)名", 160, True)
        'Public Shared CUST_REF_NO As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CUST_REF_NO, "荷主参照番号", 120, True)
        'Public Shared ORIG_CD As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.ORIG_CD, "発地コード", 0, False)
        'Public Shared ORIG_NM As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.ORIG_NM, "発地名", 120, True)
        'Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.DEST_CD, "届先コード", 0, False)
        'Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.DEST_NM, "届先名", 120, True)
        'Public Shared DEST_AD As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.DEST_AD, "届先住所", 120, True)
        'Public Shared AREA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.AREA, "エリア名", 120, True)
        'Public Shared UNSO_NB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSO_NB, "総個数", 80, True)
        'Public Shared WT As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.WT, "重量", 80, True)
        'Public Shared SHOMI_WT As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.SHOMI_WT, "正味重量", 80, True)
        'Public Shared INOUTKA_NO_L As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.INOUTKA_NO_L, "管理番号", 80, True)
        'Public Shared OUTKA_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.OUTKA_PLAN_DATE, "出荷予定", 80, True)
        'Public Shared ARR_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.ARR_PLAN_DATE, "納入予定", 80, True)
        'Public Shared TRIP_NO As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TRIP_NO, "運行番号", 80, True)
        'Public Shared DRIVER As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.DRIVER, "乗務員", 120, True)
        'Public Shared TRIP_DATE As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TRIP_DATE, "運行日", 100, True)
        'Public Shared VCLE_KB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.VCLE_KB, "車種", 80, True)
        'Public Shared CAR_NO As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CAR_NO, "車番", 80, True)
        'Public Shared UNSOCO_CD_1 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_CD_1, "運送会社(1次)コード", 0, False)
        'Public Shared UNSOCO_BR_CD_1 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_CD_1, "運送会社(1次)支店コード", 0, False)
        'Public Shared UNSOCO_1 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_1, "運送会社(1次)名", 160, True)
        'Public Shared UNSOCO_NM_1 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_NM_1, "会社(1次)名", 0, False)
        'Public Shared UNSOCO_BR_NM_1 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_NM_1, "支店(1次)名", 0, False)
        'Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CUST_NM, "荷主名", 80, True)
        'Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CUST_NM_L, "荷主(大)名", 0, False)
        'Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CUST_NM_M, "荷主(小)名", 0, False)
        'Public Shared UNSO_REM As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSO_REM, "備考", 80, True)
        'Public Shared UNCHIN As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNCHIN, "運賃", 80, True)
        'Public Shared KYORI As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.KYORI, "距離", 80, True)
        'Public Shared GROUP_NO As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.GROUP_NO, "まとめ番号", 100, True)
        'Public Shared UNSO_ONDO_KB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSO_ONDO_KB, "温管", 140, True)
        'Public Shared MOTO_DATA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.MOTO_DATA, "元データ区分(隠し)", 0, False)
        'Public Shared MOTO_DATA_KB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.MOTO_DATA_KB, "元データ" & vbCrLf & "区分", 80, True)
        'Public Shared SHUKA_RELY_POINT As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.SHUKA_RELY_POINT, "集荷中継地", 100, True)
        'Public Shared HAIKA_RELY_POINT As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.HAIKA_RELY_POINT, "配荷中継地", 100, True)
        'Public Shared TRIP_NO_SHUKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TRIP_NO_SHUKA, "運行番号(集荷)", 130, True)
        'Public Shared TRIP_NO_CHUKEI As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TRIP_NO_CHUKEI, "運行番号(中継)", 130, True)
        'Public Shared TRIP_NO_HAIKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TRIP_NO_HAIKA, "運行番号(配荷)", 130, True)
        'Public Shared UNSOCO_SHUKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_SHUKA, "運送会社(集荷)", 130, True)
        'Public Shared UNSOCO_NM_SHUKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_NM_SHUKA, "会社(集荷)", 0, False)
        'Public Shared UNSOCO_BR_NM_SHUKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_NM_SHUKA, "支店(集荷)", 0, False)
        'Public Shared UNSOCO_CHUKEI As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_CHUKEI, "運送会社(中継)", 130, True)
        'Public Shared UNSOCO_NM_CHUKEI As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_NM_CHUKEI, "会社(中継)", 0, False)
        'Public Shared UNSOCO_BR_NM_CHUKEI As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_NM_CHUKEI, "支店(中継)", 0, False)
        'Public Shared UNSOCO_HAIKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_HAIKA, "運送会社(配荷)", 130, True)
        'Public Shared UNSOCO_NM_HAIKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_NM_HAIKA, "会社(配荷)", 0, False)
        'Public Shared UNSOCO_BR_NM_HAIKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_NM_HAIKA, "支店(配荷)", 0, False)
        'Public Shared TYUKEI_FLG As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TYUKEI_FLG, "中継配送フラグ", 0, False)
        'Public Shared UNSO_TEHAI_KB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSO_TEHAI_KB, "運送手配区分", 0, False)
        'Public Shared CNT_USER As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CNT_USER, "作成者", 120, True)
        'Public Shared CNT_DATE As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CNT_DATE, "作成日", 80, True)
        'Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.NRS_BR_CD, "営業書コード", 0, False)
        'Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.SYS_UPD_DATE, "更新日", 0, False)
        'Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.SYS_UPD_TIME, "更新時間", 0, False)
        'スプレッド(タイトル列)の設定
        Public DEF As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.DEF, " ", 20, True)
        Public UNSO_NO_L As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSO_NO_L, "運送番号", 80, True)
        Public BIN_KB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.BIN_KB, "便区分(運送)", 80, True)  'MOD 2018/12/19 要望管理000880 列名に「(運送)」を付加
        Public BIN_KB_UNSO_LL As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.BIN_KB_UNSO_LL, "便区分(運行)", 80, True)  'ADD 2018/12/19 要望管理000880
        Public BUNRUI As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.BUNRUI, "タリフ分類", 90, True)
        Public TARIFF_BUNRUI_KB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TARIFF_BUNRUI_KB, "タリフ分類区分", 0, False)
        Public UNSOCO_CD_2 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_CD_2, "運送会社(2次)コード", 0, False)
        Public UNSOCO_BR_CD_2 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_CD_2, "運送会社(2次)支店コード", 0, False)
        Public UNSOCO_NM_2 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_NM_2, "会社(2次)名", 0, False)
        Public UNSOCO_BR_NM_2 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_NM_2, "支店(2次)名", 0, False)
        Public UNSOCO_2 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_2, "運送会社(2次)名", 160, True)
        Public CUST_REF_NO As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CUST_REF_NO, "荷主参照番号", 120, True)
        Public ORIG_CD As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.ORIG_CD, "発地コード", 0, False)
        Public ORIG_NM As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.ORIG_NM, "発地名", 120, True)
        Public DEST_CD As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.DEST_CD, "届先コード", 0, False)
        Public DEST_NM As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.DEST_NM, "届先名", 100, True)
        Public DEST_AD As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.DEST_AD, "届先住所", 90, True)
        Public TASYA_WH_NM As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TASYA_WH_NM, "製品置き場" & vbCrLf & "（他社倉庫名称）", 140, True)
        Public AREA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.AREA, "エリア名", 120, True)
        Public UNSO_NB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSO_NB, "総個数", 50, True)
        Public WT As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.WT, "重量", 80, True)
        Public SHOMI_WT As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.SHOMI_WT, "正味重量", 80, True)
        Public INOUTKA_NO_L As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.INOUTKA_NO_L, "管理番号", 80, True)
        Public WH_CD As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.WH_CD, "倉庫コード", 80, False)   'ADD 2019/08/05 005193
        Public OUTKA_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.OUTKA_PLAN_DATE, "出荷予定", 80, True)
        Public ARR_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.ARR_PLAN_DATE, "納入予定", 80, True)
        Public TRIP_NO As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TRIP_NO, "運行番号", 80, True)
        Public DRIVER As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.DRIVER, "乗務員", 120, True)
        Public TRIP_DATE As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TRIP_DATE, "運行日", 100, True)
        Public VCLE_KB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.VCLE_KB, "車種", 80, True)
        Public CAR_NO As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CAR_NO, "車番", 80, True)
        Public UNSOCO_CD_1 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_CD_1, "運送会社(1次)コード", 0, False)
        Public UNSOCO_BR_CD_1 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_CD_1, "運送会社(1次)支店コード", 0, False)
        Public UNSOCO_1 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_1, "運送会社(1次)名", 90, True)
        Public UNSOCO_NM_1 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_NM_1, "会社(1次)名", 0, False)
        Public UNSOCO_BR_NM_1 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_NM_1, "支店(1次)名", 0, False)
        Public CUST_NM As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CUST_NM, "荷主名", 70, True)
        Public CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CUST_NM_L, "荷主(大)名", 0, False)
        Public CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CUST_NM_M, "荷主(小)名", 0, False)
        Public UNSO_REM As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSO_REM, "備考", 80, True)
        Public UNCHIN As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNCHIN, "運賃", 80, True)
        'START UMANO 要望番号1302 支払運賃に伴う修正。
        Public SHIHARAI_UNCHIN As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.SHIHARAI_UNCHIN, "支払運賃", 80, True)
        Public SHIHARAI_FIXED_FLAG As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.SHIHARAI_FIXED_FLAG, "支払料金確定フラグ", 0, False)
        'END UMANO 要望番号1302 支払運賃に伴う修正。
        Public KYORI As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.KYORI, "距離", 50, True)
        Public GROUP_NO As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.GROUP_NO, "まとめ番号", 80, True)
        Public UNSO_ONDO_KB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSO_ONDO_KB, "温管", 40, True)
        Public MOTO_DATA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.MOTO_DATA, "元ﾃﾞｰﾀ区分(隠し)", 50, True)
        Public MOTO_DATA_KB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.MOTO_DATA_KB, "元ﾃﾞｰﾀ" & vbCrLf & "区分", 50, True)
        Public SHUKA_RELY_POINT As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.SHUKA_RELY_POINT, "集荷中継地", 100, True)
        Public HAIKA_RELY_POINT As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.HAIKA_RELY_POINT, "配荷中継地", 100, True)
        Public TRIP_NO_SHUKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TRIP_NO_SHUKA, "運行番号(集荷)", 130, True)
        Public TRIP_NO_CHUKEI As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TRIP_NO_CHUKEI, "運行番号(中継)", 130, True)
        Public TRIP_NO_HAIKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TRIP_NO_HAIKA, "運行番号(配荷)", 130, True)
        Public UNSOCO_SHUKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_SHUKA, "運送会社(集荷)", 130, True)
        Public UNSOCO_NM_SHUKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_NM_SHUKA, "会社(集荷)", 0, False)
        Public UNSOCO_BR_NM_SHUKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_NM_SHUKA, "支店(集荷)", 0, False)
        Public UNSOCO_CHUKEI As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_CHUKEI, "運送会社(中継)", 130, True)
        Public UNSOCO_NM_CHUKEI As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_NM_CHUKEI, "会社(中継)", 0, False)
        Public UNSOCO_BR_NM_CHUKEI As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_NM_CHUKEI, "支店(中継)", 0, False)
        Public UNSOCO_HAIKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_HAIKA, "運送会社(配荷)", 130, True)
        Public UNSOCO_NM_HAIKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_NM_HAIKA, "会社(配荷)", 0, False)
        Public UNSOCO_BR_NM_HAIKA As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSOCO_BR_NM_HAIKA, "支店(配荷)", 0, False)
        Public TYUKEI_FLG As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TYUKEI_FLG, "中継配送フラグ", 0, False)
        Public UNSO_TEHAI_KB As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.UNSO_TEHAI_KB, "運送手配区分", 0, False)
        Public DEST_ADD2 As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.DEST_ADD2, "届先JIS住所", 100, True)
        '要望番号2140 追加START 2013.12.25
        Public ALCTD_STS As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.ALCTD_STS, "引当状況", 80, True)
        '要望番号2140 追加END 2013.12.25
        Public CNT_USER As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CNT_USER, "作成者", 120, True)
        Public CNT_DATE As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.CNT_DATE, "作成日", 80, True)
        Public NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.NRS_BR_CD, "営業書コード", 0, False)
        Public SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.SYS_UPD_DATE, "更新日", 0, False)
        Public SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.SYS_UPD_TIME, "更新時間", 0, False)
        'END YANAI 要望番号737 運送検索画面：全体が見えるようにする
        '要望番号2063 追加START 2013.10.22
        'Public TEHAI_JYOKYO As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TEHAI_JYOKYO, "手配状況", 40, True)
        'Public TEHAI_SYUBETSU As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.TEHAI_SYUBETSU, "手配種別", 60, True)
        '要望番号2063 追加END 2013.10.22

#If True Then ' 西濃自動送り状番号対応 201600705 added inoue
        Public AUTO_DENP_KBN As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.AUTO_DENP_KBN, "自動送状区分", 0, False)
        Public AUTO_DENP_NO As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.AUTO_DENP_NO, "自動送状番号", 0, False)
#End If

        Public DENP_NO As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.DENP_NO, "送り状番号", 100, True)
        '2022.08.22 追加START
        Public PF_SOSHIN As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.PF_SOSHIN, "PF送信区分", 0, False)
        Public PF_SOSHIN_NM As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.PF_SOSHIN_NM, "PF", 40, True)
        '2022.08.22 追加END
        Public KAKUTEI As SpreadColProperty = New SpreadColProperty(LMF010C.SprColumnIndex.KAKUTEI, "確定", 40, True)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim spr As LMSpread = Me._Frm.sprUnsoUnkou

        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            '.ActiveSheet.ColumnCount = 64
            '.ActiveSheet.ColumnCount = 69

#If Flse Then ' 西濃自動送り状番号対応 20160705 changed inoue
            .ActiveSheet.ColumnCount = 68
#Else
            .ActiveSheet.ColumnCount = LMF010C.SprColumnIndex.INDEX_COUNT
#End If
            'END UMANO 要望番号1302 支払運賃に伴う修正。

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New LMF010G.sprUnsoUnkouDef())
            objSprDef = New sprUnsoUnkouDefault
            .SetColProperty(objSprDef, True)
            sprUnsoUnkouDef = DirectCast(objSprDef, sprUnsoUnkouDefault)

            '列固定位置を設定します。(運送会社（2次）で固定)
            '.ActiveSheet.FrozenColumnCount = sprUnsoUnkouDef.UNSOCO_2.ColNo + 1
            '20161012 列名で固定した場合、不具合が起きたので修正
            .ActiveSheet.FrozenColumnCount = 11     '10→11 2018/12/19 要望番号000880

            '列設定(LMF010G.sprUnsoUnkouDef:運行・運送情報)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sEidaisu9 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 9)
            Dim sEidaisu10 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 10)
            Dim sEidaisu20 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 20)
            Dim sMix20 As StyleInfo = Me.StyleInfoTextMix(spr, 20)
            Dim sMix30 As StyleInfo = Me.StyleInfoTextMix(spr, 30)
            Dim sMix40 As StyleInfo = Me.StyleInfoTextMix(spr, 40)
            Dim sMix50 As StyleInfo = Me.StyleInfoTextMix(spr, 50)
            Dim sMix60 As StyleInfo = Me.StyleInfoTextMix(spr, 60)
            Dim sMix80 As StyleInfo = Me.StyleInfoTextMix(spr, 80)
            Dim sMix100 As StyleInfo = Me.StyleInfoTextMix(spr, 100)
            Dim sMix122 As StyleInfo = Me.StyleInfoTextMix(spr, 122)

            .SetCellStyle(0, sprUnsoUnkouDef.UNSO_NO_L.ColNo, sEidaisu9)
            .SetCellStyle(0, sprUnsoUnkouDef.BIN_KB.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_U001, False))
            .SetCellStyle(0, sprUnsoUnkouDef.BIN_KB_UNSO_LL.ColNo, sLabel)  'ADD 2018/12/19 要望番号000880
            .SetCellStyle(0, sprUnsoUnkouDef.BUNRUI.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_T015, False))
            .SetCellStyle(0, sprUnsoUnkouDef.TARIFF_BUNRUI_KB.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_CD_2.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_BR_CD_2.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_NM_2.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_BR_NM_2.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_2.ColNo, sMix122)
            .SetCellStyle(0, sprUnsoUnkouDef.CUST_REF_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 30, False))
            .SetCellStyle(0, sprUnsoUnkouDef.ORIG_CD.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.ORIG_NM.ColNo, sMix80)
            .SetCellStyle(0, sprUnsoUnkouDef.DEST_CD.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.DEST_NM.ColNo, sMix80)
            .SetCellStyle(0, sprUnsoUnkouDef.DEST_AD.ColNo, sMix40)
            .SetCellStyle(0, sprUnsoUnkouDef.TASYA_WH_NM.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.AREA.ColNo, sMix20)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSO_NB.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.WT.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.SHOMI_WT.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.INOUTKA_NO_L.ColNo, sEidaisu9)
            .SetCellStyle(0, sprUnsoUnkouDef.OUTKA_PLAN_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.ARR_PLAN_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.TRIP_NO.ColNo, sEidaisu10)
            .SetCellStyle(0, sprUnsoUnkouDef.DRIVER.ColNo, sMix20)
            .SetCellStyle(0, sprUnsoUnkouDef.TRIP_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.VCLE_KB.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_S023, False))
            .SetCellStyle(0, sprUnsoUnkouDef.CAR_NO.ColNo, sEidaisu20)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_CD_1.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_BR_CD_1.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_1.ColNo, sMix122)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_NM_1.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_BR_NM_1.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.CUST_NM.ColNo, sMix122)
            .SetCellStyle(0, sprUnsoUnkouDef.CUST_NM_L.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.CUST_NM_M.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSO_REM.ColNo, sMix100)
            .SetCellStyle(0, sprUnsoUnkouDef.UNCHIN.ColNo, sLabel)
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            .SetCellStyle(0, sprUnsoUnkouDef.SHIHARAI_UNCHIN.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.SHIHARAI_FIXED_FLAG.ColNo, sLabel)
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            .SetCellStyle(0, sprUnsoUnkouDef.KYORI.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.GROUP_NO.ColNo, sEidaisu9)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSO_ONDO_KB.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_U006, False))
            .SetCellStyle(0, sprUnsoUnkouDef.MOTO_DATA.ColNo, sLabel)

            '2017/09/25 修正 李↓
            .SetCellStyle(0, sprUnsoUnkouDef.MOTO_DATA_KB.ColNo, LMSpreadUtility.GetComboCellMaster(spr, LMConst.CacheTBL.KBN, "KBN_CD",
                                                                                                    lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"}),
                                                                                                    False, New String() {"KBN_GROUP_CD", "VALUE1"}, New String() {LMKbnConst.KBN_M004, "1.000"}, LMConst.JoinCondition.AND_WORD))
            '2017/09/25 修正 李↑

            .SetCellStyle(0, sprUnsoUnkouDef.SHUKA_RELY_POINT.ColNo, sMix50)
            .SetCellStyle(0, sprUnsoUnkouDef.HAIKA_RELY_POINT.ColNo, sMix50)
            .SetCellStyle(0, sprUnsoUnkouDef.TRIP_NO_SHUKA.ColNo, sEidaisu10)
            .SetCellStyle(0, sprUnsoUnkouDef.TRIP_NO_CHUKEI.ColNo, sEidaisu10)
            .SetCellStyle(0, sprUnsoUnkouDef.TRIP_NO_HAIKA.ColNo, sEidaisu10)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_SHUKA.ColNo, sMix122)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_NM_SHUKA.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_BR_NM_SHUKA.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_CHUKEI.ColNo, sMix122)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_NM_CHUKEI.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_BR_NM_CHUKEI.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_HAIKA.ColNo, sMix122)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_NM_HAIKA.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSOCO_BR_NM_HAIKA.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.TYUKEI_FLG.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.UNSO_TEHAI_KB.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.CNT_USER.ColNo, sMix20)
            .SetCellStyle(0, sprUnsoUnkouDef.CNT_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.NRS_BR_CD.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.DEST_ADD2.ColNo, sMix40)
            '要望番号2140 追加START 2013.12.25
            .SetCellStyle(0, sprUnsoUnkouDef.ALCTD_STS.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "H025", False))
            '要望番号2140 追加END 2013.12.25
            .SetCellStyle(0, sprUnsoUnkouDef.SYS_UPD_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.SYS_UPD_TIME.ColNo, sLabel)

#If True Then ' 西濃自動送り状番号対応 2016705 added inoue
            .SetCellStyle(0, sprUnsoUnkouDef.AUTO_DENP_KBN.ColNo, sLabel)
            .SetCellStyle(0, sprUnsoUnkouDef.AUTO_DENP_NO.ColNo, sLabel)
#End If
            .SetCellStyle(0, sprUnsoUnkouDef.DENP_NO.ColNo, sMix80)
            '2022.08.22 追加START
            .SetCellStyle(0, sprUnsoUnkouDef.PF_SOSHIN_NM.ColNo, sLabel)
            '2022.08.22 追加END
            .SetCellStyle(0, sprUnsoUnkouDef.KAKUTEI.ColNo, sLabel)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        Dim spr As LMSpread = Me._Frm.sprUnsoUnkou
        Dim max As Integer = spr.ActiveSheet.Columns.Count - 1

        With spr

            For i As Integer = 1 To max
                .SetCellValue(0, i, String.Empty)
            Next

            'START YANAI 要望番号737 運送検索画面：全体が見えるようにする
            'スプレッドの列の表示・非表示設定
            Call Me.SetSpreadVisible()
            'END YANAI 要望番号737 運送検索画面：全体が見えるようにする

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        Dim spr As LMSpreadSearch = Me._Frm.sprUnsoUnkou
        Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_OUT)

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()

            '非表示制御
            Call Me.HideControl()

            '----データ挿入----'
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sNum10 As StyleInfo = Me.StyleInfoNum10(spr)
            Dim sNumMax As StyleInfo = Me.StyleInfoNumMax(spr)
            Dim sNum9d3 As StyleInfo = Me.StyleInfoNum9dec3(spr)
            Dim sNum5 As StyleInfo = Me.StyleInfoNum5(spr)
            'START 要望番号1243 赤データの表示・・EDI検索
            Dim sNum9d3Minus As StyleInfo = Me.StyleInfoNum9dec3Minus(spr)
            'END 要望番号1243 赤データの表示・・EDI検索

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprUnsoUnkouDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSO_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.BIN_KB.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.BIN_KB_UNSO_LL.ColNo, sLabel)  'ADD 2018/12/19 要望管理000880
                .SetCellStyle(i, sprUnsoUnkouDef.BUNRUI.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.TARIFF_BUNRUI_KB.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_CD_2.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_BR_CD_2.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_NM_2.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_BR_NM_2.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_2.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.CUST_REF_NO.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.ORIG_CD.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.ORIG_NM.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.DEST_CD.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.DEST_AD.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.TASYA_WH_NM.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.AREA.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSO_NB.ColNo, sNum10)
                'START 要望番号1243 赤データの表示・・EDI検索
                '.SetCellStyle(i, sprUnsoUnkouDef.WT.ColNo, sNum9d3)
                '.SetCellStyle(i, sprUnsoUnkouDef.SHOMI_WT.ColNo, sNum9d3)
                .SetCellStyle(i, sprUnsoUnkouDef.WT.ColNo, sNum9d3Minus)
                .SetCellStyle(i, sprUnsoUnkouDef.SHOMI_WT.ColNo, sNum9d3Minus)
                'END 要望番号1243 赤データの表示・・EDI検索
                .SetCellStyle(i, sprUnsoUnkouDef.INOUTKA_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.OUTKA_PLAN_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.ARR_PLAN_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.TRIP_NO.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.DRIVER.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.TRIP_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.VCLE_KB.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.CAR_NO.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_CD_1.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_BR_CD_1.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_1.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_NM_1.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_BR_NM_1.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.CUST_NM.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.CUST_NM_L.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.CUST_NM_M.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSO_REM.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNCHIN.ColNo, sNumMax)
                'START UMANO 要望番号1302 支払運賃に伴う修正。
                .SetCellStyle(i, sprUnsoUnkouDef.SHIHARAI_UNCHIN.ColNo, sNumMax)
                .SetCellStyle(i, sprUnsoUnkouDef.SHIHARAI_FIXED_FLAG.ColNo, sNumMax)
                'END UMANO 要望番号1302 支払運賃に伴う修正。
                .SetCellStyle(i, sprUnsoUnkouDef.KYORI.ColNo, sNum5)
                .SetCellStyle(i, sprUnsoUnkouDef.GROUP_NO.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSO_ONDO_KB.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.MOTO_DATA.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.MOTO_DATA_KB.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.SHUKA_RELY_POINT.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.HAIKA_RELY_POINT.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.TRIP_NO_SHUKA.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.TRIP_NO_CHUKEI.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.TRIP_NO_HAIKA.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_SHUKA.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_NM_SHUKA.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_BR_NM_SHUKA.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_CHUKEI.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_NM_CHUKEI.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_BR_NM_CHUKEI.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_HAIKA.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_NM_HAIKA.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSOCO_BR_NM_HAIKA.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.TYUKEI_FLG.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.UNSO_TEHAI_KB.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.CNT_USER.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.CNT_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.DEST_ADD2.ColNo, sLabel)
                '要望番号2140 追加START 2013.12.25
                .SetCellStyle(i, sprUnsoUnkouDef.ALCTD_STS.ColNo, sLabel)
                '要望番号2140 追加END 2013.12.25
                .SetCellStyle(i, sprUnsoUnkouDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.SYS_UPD_TIME.ColNo, sLabel)

                '要望番号2063 追加START 2013.10.22
                '.SetCellStyle(i, sprUnsoUnkouDef.TEHAI_JYOKYO.ColNo, sLabel)
                '.SetCellStyle(i, sprUnsoUnkouDef.TEHAI_SYUBETSU.ColNo, sLabel)
                '要望番号2063 追加END 2013.10.22

#If True Then ' 西濃自動送り状番号対応 20160705 added inoue
                .SetCellStyle(i, sprUnsoUnkouDef.AUTO_DENP_KBN.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.AUTO_DENP_NO.ColNo, sLabel)
#End If
                .SetCellStyle(i, sprUnsoUnkouDef.DENP_NO.ColNo, sLabel)
                .SetCellStyle(i, sprUnsoUnkouDef.WH_CD.ColNo, sLabel)           'ADD 2019/08/05 005193
	            '2022.08.22 追加START
                .SetCellStyle(i, sprUnsoUnkouDef.PF_SOSHIN_NM.ColNo, sLabel)
                '2022.08.22 追加END
                .SetCellStyle(i, sprUnsoUnkouDef.KAKUTEI.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, sprUnsoUnkouDef.DEF.ColNo, Me._LMFconG.ChangeBooleanCheckBox(dr.Item("C_SELECT").ToString()).ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSO_NO_L.ColNo, dr.Item("UNSO_NO_L").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.BIN_KB.ColNo, dr.Item("BIN").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.BIN_KB_UNSO_LL.ColNo, dr.Item("BIN_UNSO_LL").ToString())   'ADD 2018/12/19 要望管理000880
                .SetCellValue(i, sprUnsoUnkouDef.BUNRUI.ColNo, dr.Item("BUNRUI").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.TARIFF_BUNRUI_KB.ColNo, dr.Item("TARIFF_BUNRUI_KB").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_CD_2.ColNo, dr.Item("UNSOCO_CD").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_BR_CD_2.ColNo, dr.Item("UNSOCO_BR_CD").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_NM_2.ColNo, dr.Item("UNSOCO_NM").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_BR_NM_2.ColNo, dr.Item("UNSOCO_BR_NM").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_2.ColNo, Me._LMFconG.EditConcatData(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, sprUnsoUnkouDef.CUST_REF_NO.ColNo, dr.Item("CUST_REF_NO").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.ORIG_CD.ColNo, dr.Item("ORIG_CD").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.ORIG_NM.ColNo, dr.Item("ORIG_NM").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.DEST_CD.ColNo, dr.Item("DEST_CD").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.DEST_AD.ColNo, dr.Item("DEST_ADD").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.TASYA_WH_NM.ColNo, dr.Item("TASYA_WH_NM").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.AREA.ColNo, dr.Item("AREA").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSO_NB.ColNo, dr.Item("UNSO_PKG_NB").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.WT.ColNo, dr.Item("UNSO_WT").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.SHOMI_WT.ColNo, dr.Item("SHOMI_JURYO").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.INOUTKA_NO_L.ColNo, dr.Item("KANRI_NO").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.OUTKA_PLAN_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("OUTKA_PLAN_DATE").ToString()))
                .SetCellValue(i, sprUnsoUnkouDef.ARR_PLAN_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("ARR_PLAN_DATE").ToString()))
                .SetCellValue(i, sprUnsoUnkouDef.TRIP_NO.ColNo, dr.Item("TRIP_NO").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.DRIVER.ColNo, dr.Item("DRIVER_NM").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.TRIP_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("TRIP_DATE").ToString()))
                .SetCellValue(i, sprUnsoUnkouDef.VCLE_KB.ColNo, dr.Item("CAR_TP").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.CAR_NO.ColNo, dr.Item("CAR_NO").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_CD_1.ColNo, dr.Item("UNSO_CD").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_BR_CD_1.ColNo, dr.Item("UNSO_BR_CD").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_1.ColNo, Me._LMFconG.EditConcatData(dr.Item("UNSO_NM").ToString(), dr.Item("UNSO_BR_NM").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_NM_1.ColNo, dr.Item("UNSO_NM").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_BR_NM_1.ColNo, dr.Item("UNSO_BR_NM").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.CUST_NM.ColNo, Me._LMFconG.EditConcatData(dr.Item("CUST_NM_L").ToString(), dr.Item("CUST_NM_M").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, sprUnsoUnkouDef.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.CUST_NM_M.ColNo, dr.Item("CUST_NM_M").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSO_REM.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNCHIN.ColNo, dr.Item("UNCHIN").ToString())
                'START UMANO 要望番号1302 支払運賃に伴う修正。
                .SetCellValue(i, sprUnsoUnkouDef.SHIHARAI_UNCHIN.ColNo, dr.Item("SHIHARAI_UNCHIN").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.SHIHARAI_FIXED_FLAG.ColNo, dr.Item("SHIHARAI_FIXED_FLAG").ToString())
                'START UMANO 要望番号1302 支払運賃に伴う修正。
                .SetCellValue(i, sprUnsoUnkouDef.KYORI.ColNo, dr.Item("KYORI").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.GROUP_NO.ColNo, dr.Item("GROUP_NO").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSO_ONDO_KB.ColNo, dr.Item("UNSO_ONDO").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.MOTO_DATA_KB.ColNo, dr.Item("MOTO_DATA_KB").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.MOTO_DATA_KB.ColNo, dr.Item("MOTO_DATA").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.SHUKA_RELY_POINT.ColNo, dr.Item("SYUKA_TYUKEI_NM").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.HAIKA_RELY_POINT.ColNo, dr.Item("HAIKA_TYUKEI_NM").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.TRIP_NO_SHUKA.ColNo, dr.Item("TRIP_NO_SYUKA").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.TRIP_NO_CHUKEI.ColNo, dr.Item("TRIP_NO_TYUKEI").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.TRIP_NO_HAIKA.ColNo, dr.Item("TRIP_NO_HAIKA").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_SHUKA.ColNo, Me._LMFconG.EditConcatData(dr.Item("UNSOCO_SYUKA").ToString(), dr.Item("UNSOCO_BR_SYUKA").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_NM_SHUKA.ColNo, dr.Item("UNSOCO_SYUKA").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_BR_NM_SHUKA.ColNo, dr.Item("UNSOCO_BR_SYUKA").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_CHUKEI.ColNo, Me._LMFconG.EditConcatData(dr.Item("UNSOCO_TYUKEI").ToString(), dr.Item("UNSOCO_BR_TYUKEI").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_NM_CHUKEI.ColNo, dr.Item("UNSOCO_BR_TYUKEI").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_BR_NM_CHUKEI.ColNo, dr.Item("UNSOCO_TYUKEI").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_HAIKA.ColNo, Me._LMFconG.EditConcatData(dr.Item("UNSOCO_HAIKA").ToString(), dr.Item("UNSOCO_BR_HAIKA").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_NM_HAIKA.ColNo, dr.Item("UNSOCO_HAIKA").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSOCO_BR_NM_HAIKA.ColNo, dr.Item("UNSOCO_BR_HAIKA").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.TYUKEI_FLG.ColNo, dr.Item("TYUKEI_HAISO_FLG").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.UNSO_TEHAI_KB.ColNo, dr.Item("UNSO_TEHAI_KB").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.CNT_USER.ColNo, dr.Item("SYS_ENT_NM").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.CNT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SYS_ENT_DATE").ToString()))
                .SetCellValue(i, sprUnsoUnkouDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.DEST_ADD2.ColNo, dr.Item("DEST_ADD2").ToString())
                '要望番号2140 追加START 2013.12.25
                .SetCellValue(i, sprUnsoUnkouDef.ALCTD_STS.ColNo, dr.Item("ALCTD_STS").ToString())
                '要望番号2140 追加END 2013.12.25
                .SetCellValue(i, sprUnsoUnkouDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())

                '要望番号2063 追加START 2013.10.22
                '.SetCellValue(i, sprUnsoUnkouDef.TEHAI_JYOKYO.ColNo, dr.Item("TEHAI_JYOKYO").ToString())
                '.SetCellValue(i, sprUnsoUnkouDef.TEHAI_SYUBETSU.ColNo, dr.Item("TEHAI_SYUBETSU").ToString())
                '要望番号2063 追加END 2013.10.22

#If True Then ' 西濃自動送り状番号対応 20160705 added inoue
                .SetCellValue(i, sprUnsoUnkouDef.AUTO_DENP_KBN.ColNo, dr.Item("AUTO_DENP_KBN").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.AUTO_DENP_NO.ColNo, dr.Item("AUTO_DENP_NO").ToString())
#End If
                .SetCellValue(i, sprUnsoUnkouDef.DENP_NO.ColNo, dr.Item("DENP_NO").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.WH_CD.ColNo, dr.Item("WH_CD").ToString())          'ADD 2019/08/05 005193
	            '2022.08.22 追加START
                .SetCellValue(i, sprUnsoUnkouDef.PF_SOSHIN.ColNo, dr.Item("PF_SOSHIN").ToString())
                .SetCellValue(i, sprUnsoUnkouDef.PF_SOSHIN_NM.ColNo, dr.Item("PF_SOSHIN_NM").ToString())
                '2022.08.22 追加END
                .SetCellValue(i, sprUnsoUnkouDef.KAKUTEI.ColNo, dr.Item("SEIQ_FIXED_NM").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''START YANAI 要望番号737 運送検索画面：全体が見えるようにする
    '''' <summary>
    '''' スプレッドの列の表示・非表示を設定(明細)
    '''' </summary>
    '''' <remarks></remarks>
    'Friend Sub SetSpreadVisible()

    '    Dim visibleFlg As Boolean = False
    '    If ("00").Equals(Me._Frm.cmbVisibleKb.SelectedValue) = True Then
    '        '全て
    '        visibleFlg = True
    '    Else
    '        '簡易
    '        visibleFlg = False
    '    End If
    '    aa()
    '    With Me._Frm.sprUnsoUnkou

    '        .SuspendLayout()

    '        .ActiveSheet.Columns(LMF010G.sprUnsoUnkouDef.BUNRUI.ColNo).Visible = visibleFlg
    '        .ActiveSheet.Columns(LMF010G.sprUnsoUnkouDef.UNSOCO_2.ColNo).Visible = visibleFlg
    '        .ActiveSheet.Columns(LMF010G.sprUnsoUnkouDef.CUST_REF_NO.ColNo).Visible = visibleFlg
    '        .ActiveSheet.Columns(LMF010G.sprUnsoUnkouDef.ORIG_NM.ColNo).Visible = visibleFlg
    '        .ActiveSheet.Columns(LMF010G.sprUnsoUnkouDef.AREA.ColNo).Visible = visibleFlg
    '        .ActiveSheet.Columns(LMF010G.sprUnsoUnkouDef.INOUTKA_NO_L.ColNo).Visible = visibleFlg
    '        .ActiveSheet.Columns(LMF010G.sprUnsoUnkouDef.TRIP_NO.ColNo).Visible = visibleFlg
    '        .ActiveSheet.Columns(LMF010G.sprUnsoUnkouDef.DRIVER.ColNo).Visible = visibleFlg
    '        .ActiveSheet.Columns(LMF010G.sprUnsoUnkouDef.TRIP_DATE.ColNo).Visible = visibleFlg
    '        .ActiveSheet.Columns(LMF010G.sprUnsoUnkouDef.VCLE_KB.ColNo).Visible = visibleFlg
    '        .ActiveSheet.Columns(LMF010G.sprUnsoUnkouDef.CAR_NO.ColNo).Visible = visibleFlg
    '        .ActiveSheet.Columns(LMF010G.sprUnsoUnkouDef.UNSO_REM.ColNo).Visible = visibleFlg
    '        .ActiveSheet.Columns(LMF010G.sprUnsoUnkouDef.GROUP_NO.ColNo).Visible = visibleFlg

    '        .ResumeLayout(True)

    '    End With

    'End Sub
    ''END YANAI 要望番号737 運送検索画面：全体が見えるようにする
    'START YANAI 要望番号737 運送検索画面：全体が見えるようにする

    'START KIM 要望番号1261 運送検索画面：項目表示による表示切替
    ''' <summary>
    ''' スプレッドの列の表示・非表示を設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadVisible()

        Dim visibleFlg As Boolean = False
        Dim kyoryokuKaisha As String = "02" '協力会社
        Dim bp_muke As String = "03"        'BP向け     '(2013.01.20)埼玉BP向け対応

        If ("00").Equals(Me._Frm.cmbVisibleKb.SelectedValue) = True Then
            '全て
            visibleFlg = True
        Else
            '簡易・協力会社・BP向け
            visibleFlg = False
        End If

        With Me._Frm.sprUnsoUnkou

            .SuspendLayout()

            .ActiveSheet.Columns(sprUnsoUnkouDef.BUNRUI.ColNo).Visible = visibleFlg     'タリフ分類
            .ActiveSheet.Columns(sprUnsoUnkouDef.UNSOCO_2.ColNo).Visible = visibleFlg   '運送会社(2次) 名称

            '荷主参照番号
            If visibleFlg = False AndAlso (kyoryokuKaisha).Equals(Me._Frm.cmbVisibleKb.SelectedValue) = True Then
                .ActiveSheet.Columns(sprUnsoUnkouDef.CUST_REF_NO.ColNo).Visible = True
            Else
                .ActiveSheet.Columns(sprUnsoUnkouDef.CUST_REF_NO.ColNo).Visible = visibleFlg
            End If

            .ActiveSheet.Columns(sprUnsoUnkouDef.ORIG_NM.ColNo).Visible = visibleFlg       '発地名
            .ActiveSheet.Columns(sprUnsoUnkouDef.AREA.ColNo).Visible = visibleFlg          'エリア名
            .ActiveSheet.Columns(sprUnsoUnkouDef.INOUTKA_NO_L.ColNo).Visible = visibleFlg  '管理番号
            .ActiveSheet.Columns(sprUnsoUnkouDef.TRIP_NO.ColNo).Visible = visibleFlg       '運行番号
            .ActiveSheet.Columns(sprUnsoUnkouDef.DRIVER.ColNo).Visible = visibleFlg        '乗務員
            .ActiveSheet.Columns(sprUnsoUnkouDef.TRIP_DATE.ColNo).Visible = visibleFlg     '運行日
            .ActiveSheet.Columns(sprUnsoUnkouDef.VCLE_KB.ColNo).Visible = visibleFlg       '車種
            .ActiveSheet.Columns(sprUnsoUnkouDef.CAR_NO.ColNo).Visible = visibleFlg        '車番
            .ActiveSheet.Columns(sprUnsoUnkouDef.UNSO_REM.ColNo).Visible = visibleFlg      '備考
            .ActiveSheet.Columns(sprUnsoUnkouDef.GROUP_NO.ColNo).Visible = visibleFlg      'まとめ番号

            '項目表示が"協力会社"の場合、表示しない。
            If (kyoryokuKaisha).Equals(Me._Frm.cmbVisibleKb.SelectedValue) = True Then
                visibleFlg = False
            Else
                visibleFlg = True
            End If

            .ActiveSheet.Columns(sprUnsoUnkouDef.UNCHIN.ColNo).Visible = visibleFlg              '運賃
            .ActiveSheet.Columns(sprUnsoUnkouDef.SHIHARAI_UNCHIN.ColNo).Visible = visibleFlg     '支払運賃
            .ActiveSheet.Columns(sprUnsoUnkouDef.SHIHARAI_FIXED_FLAG.ColNo).Visible = visibleFlg '支払料金確定フラグ

            '(2013.01.20)埼玉BP向け処理の追加 -- START --
            'BP向け特例処理
            If (bp_muke).Equals(Me._Frm.cmbVisibleKb.SelectedValue) = True Then
                .ActiveSheet.Columns(sprUnsoUnkouDef.UNSO_REM.ColNo).Visible = True     '備考：表示
                .ActiveSheet.Columns(sprUnsoUnkouDef.SHOMI_WT.ColNo).Visible = False    '正味重量：非表示
            End If
            '(2013.01.20)埼玉BP向け処理の追加 -- END  --

            .ResumeLayout(True)

        End With

    End Sub
    'END YANAI 要望番号737 運送検索画面：全体が見えるようにする

    ''' <summary>
    ''' チェックボックスを解除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetDefOff(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprUnsoUnkou
        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1
            For i As Integer = 1 To max

                'レコードチェックはずす
                .SetCellValue(i, sprUnsoUnkouDef.DEF.ColNo, False.ToString())

            Next

            'DataSetがない場合、スルー
            If ds Is Nothing = True Then
                Exit Sub
            End If

            Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_ERR)
            max = dt.Rows.Count - 1
            For i As Integer = 0 To max

                'エラー行のチェックはそのまま
                .SetCellValue(Convert.ToInt32(dt.Rows(i).Item("ROW_NO").ToString()), sprUnsoUnkouDef.DEF.ColNo, True.ToString())

            Next

        End With

    End Sub

    ''' <summary>
    ''' 非表示制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideControl()

        Dim lock As Boolean = True

        If Me._Frm.optChukeiN.Checked = True Then

            lock = False

        End If

        Dim startCol As Integer = sprUnsoUnkouDef.SHUKA_RELY_POINT.ColNo
        Dim endCol As Integer = sprUnsoUnkouDef.TYUKEI_FLG.ColNo

        For colNo As Integer = startCol To endCol

            Call Me.HideColumns(colNo, lock)

        Next

    End Sub

    ''' <summary>
    ''' 項目を非表示にする
    ''' </summary>
    ''' <param name="colNo">列番号</param>
    ''' <remarks></remarks>
    Private Sub HideColumns(ByVal colNo As Integer, ByVal lock As Boolean)

        Me._Frm.sprUnsoUnkou.ActiveSheet.Columns(colNo).Visible = lock

    End Sub

    ''' <summary>
    ''' スプレッドの文字色設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadColor()

        Dim max As Integer = Me._Frm.sprUnsoUnkou.ActiveSheet.Rows.Count - 1

        With Me._Frm.sprUnsoUnkou.ActiveSheet

            For i As Integer = 1 To max
                If Me._LMFconG.GetCellValue(.Cells(i, sprUnsoUnkouDef.DEF.ColNo)).ToString.Equals("1") = True Then
                    'チェックON：青文字
                    .Rows(i).ForeColor = Color.Blue
                Else
                    'チェックOFF：黒文字
                    .Rows(i).ForeColor = Color.Black
                End If
            Next

        End With

    End Sub

#End Region 'Spread

#Region "ユーティリティ"

#Region "プロパティ"

    ''' <summary>
    ''' セルのプロパティを設定(CheckBox)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoChk(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetCheckBoxCell(spr, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Label)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabel(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(英大数)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextEidaisu(ByVal spr As LMSpread, ByVal length As Integer) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, length, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(MIX)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextMix(ByVal spr As LMSpread, ByVal length As Integer) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, length, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数10桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum10(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 99999999999, True, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁　少数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9dec3(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数5桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum5(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 99999, True, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数最大桁[14])
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNumMax(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, LMFControlC.MAX_KETA_SPR, True, 0, , ",")

    End Function

    'START 要望番号1243 赤データの表示・・EDI検索
    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁　少数3桁 マイナス値有り)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9dec3Minus(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, -999999999.999, 999999999.999, True, 3, , ",")

    End Function
    'END 要望番号1243 赤データの表示・・EDI検索

#End Region

#End Region

#End Region

End Class
