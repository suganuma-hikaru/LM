' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運賃サブシステム
'  プログラムID     :  LMF070H : 運賃試算比較
'  作  成  者       :  yamanaka
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI

''' <summary>
''' LMF070ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMF070H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF070F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMF070V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMF070G

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFConV As LMFControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFConH As LMFControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFConG As LMFControlG

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

#End Region

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Me._Frm = New LMF070F(Me)

        'Gamen共通クラスの設定
        Me._LMFConG = New LMFControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._LMFConV = New LMFControlV(Me, DirectCast(Me._Frm, Form), Me._LMFConG)

        'Hnadler共通クラスの設定
        Me._LMFConH = New LMFControlH(Me._Frm, MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMF070V(Me, Me._Frm, Me._LMFConV, Me._LMFConG)

        'Gamenクラスの設定
        Me._G = New LMF070G(Me, Me._Frm, Me._LMFConG)

        'フォームの初期化
        Call Me.InitControl(Me._Frm)

        'キーイベントをフォームで受け取る
        Me._Frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'スプレッドの検索行に初期値を設定する
        Call Me._G.InitSpread()

        'メッセージの表示
        Call MyBase.ShowMessage(Me._Frm, "G007")

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        Me._Frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal frm As LMF070F) As Boolean

        '処理開始アクション
        Me._LMFConH.StartAction(Me._Frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF070C.EventShubetsu.KENSAKU)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(MyBase.GetSystemDateTime(0))

        '他営業所選択チェック
        rtnResult = rtnResult AndAlso Me._V.IsUserNrsBrCdChk(frm)

        '名称設定
        rtnResult = rtnResult AndAlso Me._G.SetMstData(MyBase.GetSystemDateTime(0))

        'エラーの場合、終了
        If rtnResult = False Then
            '終了処理
            Me._LMFConH.EndAction(frm)
            Return rtnResult
        End If

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetMaxResultCount(mc)

        '検索条件の設定
        Dim rtnDs As DataSet = Me.SetDataSetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)

        rtnDs = MyBase.CallWSA(blf, "SelectListData", rtnDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '取得項目の表示
        If MyBase.IsMessageExist() = False Then

            '検索成功時共通処理を行う
            Call Me.SuccessSelect(frm, rtnDs)

        Else

            '検索失敗時共通処理を行う
            Call Me.FailureSelect(rtnDs)

        End If

        '画面項目クリア
        Call Me._G.ClearGui()

        '終了処理
        Me._LMFConH.EndAction(frm)

    End Function

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMF070F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        If Me._V.IsAuthority(LMF070C.EventShubetsu.MASTEROPEN) = False Then
            '終了処理
            Me._LMFConH.EndAction(frm)
            Exit Sub
        End If

        If Me._V.IsFocusChk(objNm, LMF070C.EventShubetsu.MASTEROPEN) = False Then
            '終了処理
            Me._LMFConH.EndAction(frm)
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMF070C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Me._LMFConH.EndAction(frm)

        'メッセージの表示
        Call MyBase.ShowMessage(Me._Frm, "G007")

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMF070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthority(LMF070C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF070C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then

            'フォーカス移動処理
            Call Me._LMFConH.NextFocusedControl(frm, eventFlg)

            Exit Sub

        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMF070C.EventShubetsu.ENTER)

        '処理終了アクション
        Me._LMFConH.EndAction(Me._Frm)

        'メッセージの表示
        Call MyBase.ShowMessage(Me._Frm, "G007")

        'フォーカス移動処理
        Call Me._LMFConH.NextFocusedControl(frm, eventFlg)

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMF070F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMF070C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '旧運賃、旧割増運賃の合計値を設定
        Call Me.SetOldUnchin(frm, ds)

        '新運賃計算
        Call Me.UnchinGet(frm, ds)

        '取得データをSPREADに表示
        Call Me._G.SetSpread(ds, frm)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            '運賃計算プログラムでエラーがある場合は表示
            MyBase.ShowMessage(frm)

        Else

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {dt.Rows.Count.ToString()})

        End If

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理(検索結果が0件の場合))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal ds As DataSet)

        'SPREAD(表示行)初期化
        Me._Frm.sprUnchin.CrearSpread()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        '合計値クリア
        Call Me._G.ClearNum()

        'メッセージ表示
        MyBase.ShowMessage(Me._Frm)

    End Sub

    ''' <summary>
    ''' 旧運賃、旧割増運賃の合計値を設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetOldUnchin(ByVal frm As LMF070F, ByVal ds As DataSet) As Boolean

        Dim prm As LMFormData = New LMFormData()
        Dim dt As DataTable = ds.Tables(LMF070C.TABLE_NM_OUT)
        Dim unchindr As DataRow = Nothing
        Dim prmDr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1

        '旧運賃用
        Dim unchin As Decimal = 0
        Dim oldunchinsum As Decimal = 0
        Dim oldextc As Decimal = 0
        Dim oldcity As Decimal = 0
        Dim oldtouki As Decimal = 0
        Dim oldkiken As Decimal = 0
        Dim oldrely As Decimal = 0
        Dim oldtoll As Decimal = 0
        Dim oldinsu As Decimal = 0
        Dim oldeunchin As Decimal = 0
        Dim eunchin As Decimal = 0
        Dim eunchinsum As Decimal = 0
        Dim oldunchin As Decimal = 0


        For i As Integer = 0 To max

            unchindr = ds.Tables(LMF070C.TABLE_NM_OUT).Rows(i)
            '旧運賃情報取得()
            unchin = Convert.ToDecimal(unchindr.Item("DECI_UNCHIN").ToString())
            '旧割増運賃情報取得
            oldcity = Convert.ToDecimal(unchindr.Item("DECI_CITY_EXTC").ToString())
            oldtouki = Convert.ToDecimal(unchindr.Item("DECI_WINT_EXTC").ToString())
            oldrely = Convert.ToDecimal(unchindr.Item("DECI_RELY_EXTC").ToString())
            oldtoll = Convert.ToDecimal(unchindr.Item("DECI_TOLL").ToString())
            oldinsu = Convert.ToDecimal(unchindr.Item("DECI_INSU").ToString())
            oldunchinsum = 0
            '旧運賃情報の合計
            oldunchinsum += unchin
            eunchin = 0
            '旧割増合計を設定
            eunchin += oldtouki + oldrely + oldinsu + oldcity + oldtoll + oldkiken

            eunchinsum += eunchin

            oldunchin += oldunchinsum + eunchin

            '取得した運賃を確定運賃に格納
            unchindr.Item("DECI_UNCHIN") = unchin + eunchin
        Next

        '旧運賃の上限チェック
        If Me._V.IsCalcOver(oldunchin.ToString, LMFControlC.MIN_0, LMF070C.MAX_13, frm.lblTitleOldTariffSum.Text) = False Then
            frm.numOldTariffSum.Value = LMF070C.MAX_13

            MyBase.SetMessage("G038", New String() {MyBase.GetResultCount.ToString(), LMF070C.OVER})
            Return False
        End If

        '旧運賃合計をラベルに設定
        frm.numOldTariffSum.Value = Convert.ToInt32(oldunchin)

        '旧割増運賃の上限チェック
        If Me._V.IsCalcOver(eunchin.ToString, LMFControlC.MIN_0, LMF070C.MAX_13, frm.lblTitleOldETariffSum.Text) = False Then
            frm.numOldETariffSum.Value = LMF070C.MAX_13

            MyBase.SetMessage("G038", New String() {MyBase.GetResultCount.ToString(), LMF070C.OVER})
            Return False
        End If

        '旧割増運賃合計をラベルに設定
        frm.numOldETariffSum.Value = Convert.ToInt32(eunchinsum)

        Return True

    End Function

    ''' <summary>
    ''' 新運賃取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function UnchinGet(ByVal frm As LMF070F, ByVal ds As DataSet) As DataSet



        '新タリフコードまたは発地コードまたは距離程コードが入力されていない場合処理終了
        Dim tariffCd As String = frm.txtNewTariffCd.TextValue
        Dim sokoCd As String = frm.cmbSoko.SelectedValue.ToString()
        If String.IsNullOrEmpty(tariffCd) = True AndAlso _
           String.IsNullOrEmpty(sokoCd) = True Then

            frm.numNewTariffSum.Value = 0
            frm.numNewETariffSum.Value = 0

            Return ds

        End If

        Dim dt As DataTable = ds.Tables(LMF070C.TABLE_NM_OUT)

        ''新運賃、割増運賃用
        Dim tariffSum As Decimal = 0
        Dim extc As Decimal = 0
        Dim city As Decimal = 0
        Dim touki As Decimal = 0
        Dim kiken As Decimal = 0
        Dim rely As Decimal = 0
        Dim toll As Decimal = 0
        Dim insu As Decimal = 0
        Dim seiqUnchin As Decimal = 0
        Dim sumunchin As Decimal = 0
        Dim extcSum As Decimal = 0



        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)

        Dim prmDr As DataRow = Nothing

        '運賃計算で帰ってきたrowカウントを取得()
        Dim maxs As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To maxs

            prmDr = dt.Rows(i)

            city = Convert.ToDecimal(Me._LMFConG.FormatNumValue(prmDr.Item("NEW_DECI_CITY_EXTC").ToString()))
            touki = Convert.ToDecimal(Me._LMFConG.FormatNumValue(prmDr.Item("NEW_DECI_WINT_EXTC").ToString()))
            rely = Convert.ToDecimal(Me._LMFConG.FormatNumValue(prmDr.Item("NEW_DECI_RELY_EXTC").ToString()))
            toll = Convert.ToDecimal(Me._LMFConG.FormatNumValue(prmDr.Item("NEW_DECI_TOLL").ToString()))
            insu = Convert.ToDecimal(Me._LMFConG.FormatNumValue(prmDr.Item("NEW_DECI_INSU").ToString()))

            '新運賃を取得
            seiqUnchin = Convert.ToDecimal(Me._LMFConG.FormatNumValue(prmDr.Item("NEW_DECI_UNCHIN").ToString()))


            '新割増運賃合計を設定
            extc = touki + rely + insu + city + toll + kiken
            extcSum += extc

            '新運賃の合計を設定
            tariffSum += seiqUnchin

            sumunchin += seiqUnchin + extcSum

            Dim dr As DataRow = ds.Tables(LMF070C.TABLE_NM_OUT).Rows(i)

            '明細用の新運賃を設定
            dt.Rows(i).Item("NEW_DECI_UNCHIN") = seiqUnchin + extcSum

        Next

        '新運賃の上限チェック
        If Me._V.IsCalcOver(sumunchin.ToString, LMFControlC.MIN_0, LMF070C.MAX_13, frm.lblTitleNewTariffSum.Text) = False Then
            frm.numNewTariffSum.Value = LMF070C.MAX_13

            MyBase.SetMessage("G038", New String() {MyBase.GetResultCount.ToString(), LMF070C.OVER})
            Return ds
        End If

        '新運賃のラベルに値を設定
        frm.numNewTariffSum.Value = Convert.ToInt32(sumunchin)

        '新割増運賃の上限チェック
        If Me._V.IsCalcOver(extcSum.ToString, LMFControlC.MIN_0, LMF070C.MAX_13, frm.lblTitleNewETariffSum.Text) = False Then
            frm.numNewETariffSum.Value = LMF070C.MAX_13
            MyBase.SetMessage("G038", New String() {MyBase.GetResultCount.ToString(), LMF070C.OVER})
            Return ds
        End If

        '新割増運賃のラベルに値を設定
        frm.numNewETariffSum.Value = Convert.ToInt32(extcSum)

        Return ds

    End Function

#End Region

#Region "データセット"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMF070F) As DataSet

        Dim ds As DataSet = New LMF070DS()
        Dim dt As DataTable = ds.Tables(LMF070C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim nrsBrCd As String = String.Empty
        Dim sokoJis As String = String.Empty
        Dim sokoNm As String = String.Empty

        With frm

            'マスタより運賃請求先マスタコードを取得
            Dim sokoDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat( _
                                                                                     "WH_CD = '", .cmbSoko.SelectedValue, "' AND", _
                                                                                     " NRS_BR_CD = ", .cmbNrsBrCd.SelectedValue.ToString(), "' AND", _
                                                                                     "SYS_DEL_FLG = '0'"))

            If sokoDr.Length > 0 Then

                nrsbrcd = sokoDr(0).Item("NRS_BR_CD").ToString()
                sokoNm = sokoDr(0).Item("WH_NM").ToString()
                sokoJis = sokoDr(0).Item("JIS_CD").ToString()

            End If

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("OUTKA_PLAN_DATE") = .imdOutakaDate_From.TextValue
            dr("OUTKA_PLAN_TO") = .imdOutakaDate_To.TextValue
            dr("CUST_CD_L") = .txtCustCdL.TextValue
            dr("CUST_CD_M") = .txtCustCdM.TextValue
            dr("SEIQ_TARIFF_CD") = .txtOldTariffCd.TextValue
            dr("SEIQ_ETARIFF_CD") = .txtOldETariffCd.TextValue
            dr("UNSOCO_CD") = .txtUnsoCd.TextValue
            dr("UNSOCO_BR_CD") = .txtUnsoBrCd.TextValue
            dr("SEIQTO_CD") = .txtSeiqtoCd.TextValue
            dr("SPREAD_DEST_NM") = Me._LMFConG.GetCellValue(frm.sprUnchin.ActiveSheet.Cells(0, LMF070G.sprUnchinDef.DEST_NM.ColNo))
            dr("SPREAD_DEST_JIS_CD") = Me._LMFConG.GetCellValue(frm.sprUnchin.ActiveSheet.Cells(0, LMF070G.sprUnchinDef.DEST_JIS_NM.ColNo))
            dr("SPREAD_INOUTKA_NO_L") = Me._LMFConG.GetCellValue(frm.sprUnchin.ActiveSheet.Cells(0, LMF070G.sprUnchinDef.INOUTKA_NO.ColNo))
            dr("MOTO_DATA_KB") = Me._LMFConG.GetCellValue(frm.sprUnchin.ActiveSheet.Cells(0, LMF070G.sprUnchinDef.MOTO_DATA_KB.ColNo))
            dr("UNSOCO_NM") = Me._LMFConG.GetCellValue(frm.sprUnchin.ActiveSheet.Cells(0, LMF070G.sprUnchinDef.UNSOCO_CD.ColNo))
            dr("NEW_TARIFF_CD") = .txtNewTariffCd.TextValue
            dr("NEW_ETARIFF_CD") = .txtNewETariffCd.TextValue
            dr("NEW_KYORI_CD") = .txtKyoriteiCd.TextValue
            dr("NEW_NRS_BR_CD") = nrsBrCd
            dr("NEW_ORIG_JIS") = sokoJis
            dr("NEW_SOKO_NM") = sokoNm

            dt.Rows.Add(dr)

        End With

        Return ds

    End Function

#End Region

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMF070F, ByVal objNm As String, ByVal actionType As LMF070C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Me._LMFConH.StartAction(frm)

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    '荷主コード(大)、荷主コード(中)
                    Call Me.SetReturnCustPop(frm, actionType)

                Case .txtOldTariffCd.Name

                    '旧タリフコード
                    Call Me.SetReturnTariffPop(frm, actionType)

                Case .txtNewTariffCd.Name

                    '新タリフコード
                    Call Me.SetReturnNewTariffPop(frm, actionType)

                Case .txtUnsoCd.Name, .txtUnsoBrCd.Name

                    '運送会社コード、運送支社コード
                    Call Me.SetReturnUnsocoPop(frm, actionType)

                Case .txtSeiqtoCd.Name

                    '請求先コード
                    Call Me.SetReturnSeqtoPop(frm, actionType)

                Case .txtOldETariffCd.Name

                    '旧割増タリフコード
                    Call Me.SetReturnWarimashiPop(frm, actionType)

                Case .txtNewETariffCd.Name

                    '新割増タリフコード
                    Call Me.SetReturnNewWarimashiPop(frm, actionType)

                Case .txtKyoriteiCd.Name

                    '距離程コード
                    Call Me.KyoriteiPop(frm, actionType)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actinType">アクションタイプ</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, actinType)

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

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actinType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            If actinType = LMF070C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S
        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFConH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnTariffPop(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowTariffPopup(frm, actinType)
        Dim tariffCd As String = frm.txtNewTariffCd.TextValue

        If prm.ReturnFlg = True Then

            'タリフマスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ230C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtOldTariffCd.TextValue = dr.Item("UNCHIN_TARIFF_CD").ToString()
                .lblOldTariffNm.TextValue = dr.Item("UNCHIN_TARIFF_REM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowTariffPopup(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ230DS()
        Dim dt As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim tariffCd As String = frm.txtNewTariffCd.TextValue

        dt.Clear()

        With dr

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("STR_DATE") = frm.imdOutakaDate_From.TextValue
            If actinType = LMF070C.EventShubetsu.ENTER Then
                .Item("UNCHIN_TARIFF_CD") = frm.txtOldTariffCd.TextValue
            End If

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

            '行追加
            dt.Rows.Add(dr)

            'Pop起動
            Return Me._LMFConH.FormShow(ds, "LMZ230", "", Me._PopupSkipFlg)

        End With

    End Function

    ''' <summary>
    ''' タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnNewTariffPop(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowNewTariffPopup(frm, actinType)
        Dim tariffCd As String = frm.txtNewTariffCd.TextValue

        If prm.ReturnFlg = True Then

            'タリフマスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ230C.TABLE_NM_OUT).Rows(0)

            With frm

                '新タリフコードが入力されている
                .txtNewTariffCd.TextValue = dr.Item("UNCHIN_TARIFF_CD").ToString()
                .lblNewTariffNm.TextValue = dr.Item("UNCHIN_TARIFF_REM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowNewTariffPopup(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ230DS()
        Dim dt As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            '新運賃タリフコードが入力されているとき
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("STR_DATE") = frm.imdOutakaDate_From.TextValue

            If actinType = LMF070C.EventShubetsu.ENTER Then
                .Item("UNCHIN_TARIFF_CD") = frm.txtNewTariffCd.TextValue
            End If

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

            '行追加
            dt.Rows.Add(dr)

            'Pop起動
            Return Me._LMFConH.FormShow(ds, "LMZ230", "", Me._PopupSkipFlg)

        End With

    End Function

    ''' <summary>
    ''' 運送会社マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnsocoPop(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowUnsocoPop(frm, actinType)

        If prm.ReturnFlg = True Then

            'タリフマスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtUnsoCd.TextValue = dr.Item("UNSOCO_CD").ToString()
                .txtUnsoBrCd.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
                .lblUnsoNm.TextValue = String.Concat(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString())

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 運送会社マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowUnsocoPop(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ250DS()
        Dim dt As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

            If actinType = LMF070C.EventShubetsu.ENTER Then

                .Item("UNSOCO_CD") = frm.txtUnsoCd.TextValue
                .Item("UNSOCO_BR_CD") = frm.txtUnsoBrCd.TextValue

            End If

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFConH.FormShow(ds, "LMZ250", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 請求先マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSeqtoPop(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowSeqtoPop(frm, actinType)

        If prm.ReturnFlg = True Then

            'タリフマスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtSeiqtoCd.TextValue = dr.Item("SEIQTO_CD").ToString()
                .lblSeiqtoNm.TextValue = dr.Item("SEIQTO_NM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 請求マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowSeqtoPop(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ220DS()
        Dim dt As DataTable = ds.Tables(LMZ220C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

            If actinType = LMF070C.EventShubetsu.ENTER Then

                .Item("SEIQTO_CD") = frm.txtSeiqtoCd.TextValue

            End If

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFConH.FormShow(ds, "LMZ220", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 割増タリフマスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnWarimashiPop(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowWarimashiPop(frm, actinType)
        Dim etariffCd As String = frm.txtNewETariffCd.TextValue

        If prm.ReturnFlg = True Then

            'タリフマスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ240C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtOldETariffCd.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()
                .lblOldETariffNm.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 割増タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowWarimashiPop(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ240DS()
        Dim dt As DataTable = ds.Tables(LMZ240C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim etariffCd As String = frm.txtNewETariffCd.TextValue

        With dr

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

            If actinType = LMF070C.EventShubetsu.ENTER Then

                .Item("EXTC_TARIFF_CD") = frm.txtOldETariffCd.TextValue

            End If

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFConH.FormShow(ds, "LMZ240", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 割増タリフマスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnNewWarimashiPop(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowNewWarimashiPop(frm, actinType)
        Dim etariffCd As String = frm.txtNewETariffCd.TextValue

        If prm.ReturnFlg = True Then

            'タリフマスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ240C.TABLE_NM_OUT).Rows(0)

            With frm

                '新割増タリフコードが入力されている場合
                .txtNewETariffCd.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()
                .lblNewETariffNm.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 割増タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowNewWarimashiPop(ByVal frm As LMF070F, ByVal actinType As LMF070C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ240DS()
        Dim dt As DataTable = ds.Tables(LMZ240C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim etariffCd As String = frm.txtNewETariffCd.TextValue

        With dr

            '新割増タリフコードが入力されている時
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

            If actinType = LMF070C.EventShubetsu.ENTER Then

                .Item("EXTC_TARIFF_CD") = frm.txtNewETariffCd.TextValue

            End If

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFConH.FormShow(ds, "LMZ240", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 距離程マスタ照会(LMZ080)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function KyoriteiPop(ByVal frm As LMF070F, ByVal actionType As LMF070C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowKyoriPopup(frm, actionType)

        If prm.ReturnFlg = True Then
            '距離程マスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ080C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtKyoriteiCd.TextValue = dr.Item("KYORI_CD").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 距離呈マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowKyoriPopup(ByVal frm As LMF070F, ByVal actionType As LMF070C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ080DS()
        Dim dt As DataTable = ds.Tables(LMZ080C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            If actionType = LMF060C.EventShubetsu.ENTER Then

                .Item("KYORI_CD") = frm.txtKyoriteiCd.TextValue

            End If

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFConH.FormShow(ds, "LMZ080", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMF070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "SelectCurrencyEvent")
        '検索処理
        Me.SelectData(frm)

        Logger.EndLog(Me.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMF070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMF070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMF070F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

         Call Me.CloseForm(frm, e)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub KeyDown(ByVal frm As LMF070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMF070F, ByVal e As FormClosingEventArgs) As Boolean

        Return True

    End Function

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

End Class