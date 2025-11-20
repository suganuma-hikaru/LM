' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運賃サブシステム
'  プログラムID     :  LMF060H : 運賃試算
'  作  成  者       :  菱刈
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMF060ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMF060H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMF060V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMF060G

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

#End Region 'Field

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
        Dim frm As LMF060F = New LMF060F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMFConG = New LMFControlG(sForm)

        'Validate共通クラスの設定
        Me._LMFConV = New LMFControlV(Me, sForm, Me._LMFConG)

        'Hnadler共通クラスの設定
        Me._LMFConH = New LMFControlH(sForm, MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMF060V(Me, frm, Me._LMFConV, Me._LMFConG)

        'Gamenクラスの設定
        Me._G = New LMF060G(Me, frm, Me._LMFConV, Me._LMFConG)

        'フォームの初期化
        Call Me.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの検索行に初期値を設定する
        Call Me._G.SetInitValue()

        'メッセージの表示
        Me.ShowMessage(frm, "G003")

        '画面の入力項目の制御
        Call _G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        '営業所は自営業所
        Me._G.SetcmbNrsBrCd()

        'START YANAI 要望番号836
        '発地JISの初期値設定
        Me._G.SettxtOrigJis()
        'END YANAI 要望番号836

        'フォームの表示
        frm.Show()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMF060F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMF060C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF060C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMF060C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me.EndAction(frm)


    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMF060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMF060C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF060C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then

            'フォーカス移動処理
            Call Me._LMFConH.NextFocusedControl(frm, eventFlg)

            'メッセージ設定
            Call Me.ShowGMessage(frm)

            Exit Sub

        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMF060C.EventShubetsu.ENTER)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me._LMFConH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMF060F, ByVal objNm As String, ByVal actionType As LMF060C.EventShubetsu) As Boolean


        Dim hantei As String = "40"

        With frm

            '処理開始アクション
            Call Me.StartAction(frm)

            Select Case objNm

                Case .txtCustCdL.Name

                    '荷主(大)Lコード
                    Call Me.CustPop(frm, actionType)

                Case .txtCustCdM.Name
                    '荷主(中)Mコード
                    Call Me.CustPop(frm, actionType)

                Case .txtOrigJis.Name

                    '発地JISコード
                    Call Me.OrigJisPop(frm, actionType)

                Case .txtTodokedeJisCd.Name

                    '届先コード
                    Call Me.TodokedeJisPop(frm, actionType)

                Case .txtKyoriteiCd.Name

                    '距離程コード
                    Call Me.KyoriteiPop(frm, actionType)

                Case .txtTariffCd.Name

                    'タリフ分類区分が40の場合横もちのポップを開く
                    Dim tariff As String = .cmbUnso.SelectedValue.ToString()

                    If hantei.Equals(tariff) = True Then
                        '横持ち
                        Call Me.YokoTariffPop(frm, actionType)
                    Else

                        '横持ち以外
                        Call Me.TariffPop(frm, actionType)

                    End If
                Case .txtWarimashiCd.Name

                    '割増コード
                    Call Me.WarimashiPop(frm, actionType)

            End Select

        End With

        Return True

    End Function

#Region "距離取得処理"

    ''' <summary>
    ''' 距離取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub KyoriAction(ByVal frm As LMF060F)

        With frm

            '処理開始アクション
            Call Me.StartAction(frm)

            '権限チェック
            If Me._V.IsAuthorityChk(LMF060C.EventShubetsu.KYORIGET) = False Then

                '終了処理
                Call Me.EndAction(frm)
                Exit Sub
            End If

            '入力チェック
            If Me._V.IsInputKyoriCheck() = False Then

                '終了処理
                Call Me.EndAction(frm)
                Exit Sub
            End If

            '検索条件の設定
            Dim ds As DataSet = Me.SetDataSetInData(frm, New LMF060DS)


            '閾値の設定
            MyBase.SetLimitCount(Me._LMFConG.GetLimitData())


            ''ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

            ''==========================
            ''WSAクラス呼出
            ''==========================


            Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
            Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMF060C.ACTION_ID_SELECT, ds)

            'メッセージ判定
            If MyBase.IsMessageExist() = True Then

                MyBase.ShowMessage(frm)

                Call Me.SetErrorControl(.txtKyoriteiCd)
                Call Me.SetErrorControl(.txtTodokedeJisCd)
                Call Me.SetErrorControl(.txtOrigJis)

                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Sub

            End If

            Dim dr As DataRow = rtnDs.Tables(LMF060C.TABLE_NM_IN).Rows(0)
            Dim Kyori As String = dr.Item("KYORI").ToString()

            '距離に設定
            .numKyori.Value = Kyori

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G003")

        End With

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

#End Region

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function PrintAction(ByVal frm As LMF060F) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMF060C.EventShubetsu.PRINT) = False Then

            '終了処理
            Call Me.EndAction(frm)
            Return False
        End If

    
        Dim data As String = MyBase.GetSystemDateTime(0)
        '入力チェック
        If Me._V.IsInputPrintCheck(data) = False Then

            '終了処理
            Call Me.EndAction(frm)

            Return False

        End If

        '検索条件の設定
        Dim ds As DataSet = Me.SetDataSetInData(frm, New LMF060DS)

        '閾値の設定
        MyBase.SetLimitCount(Me._LMFConG.GetLimitData())

        ''ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        ''==========================
        ''WSAクラス呼出
        ''==========================

        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMF060C.ACTION_ID_SELECT, ds)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            MyBase.ShowMessage(frm)
            Call Me.SetErrorControl(frm.txtKyoriteiCd)
            Call Me.SetErrorControl(frm.txtTodokedeJisCd)
            Call Me.SetErrorControl(frm.txtOrigJis)

            '終了処理
            Call Me.EndAction(frm)
            Return False

        End If

        '距離程コードの再設定
        Dim dt As DataTable = rtnDs.Tables("M_KYORI")

        Dim dr As DataRow = dt.Rows(0)

        frm.txtKyoriteiCd.TextValue = dr.Item("KYORI_CD").ToString
        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G003")

        '印刷処理
        'TODO:呼び出し先PG完了待ち
        Call Me.PrintJikouAction(frm, blf)
        '終了処理
        Call Me.EndAction(frm)

        Return True

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function PrintJikouAction(ByVal frm As LMF060F, ByVal blf As String) As Boolean

        '印刷処理
        'データセットのINの情報を設定
        Dim Print As DataSet = Me.SetDataSetInDataPrint(frm)

       
        ''==========================
        ''WSAクラス呼出
        ''==========================
        Dim rtnDsP As DataSet = MyBase.CallWSA(blf, "SelectDataPrint", Print)


        'メッセージ判定
        If IsMessageExist() = True Then

            'エラーメッセージ判定
            If MyBase.IsErrorMessageExist() = False Then


                '処理終了アクション
                Call Me.EndAction(frm)
                '印刷処理でエラーメッセージあったらメッセージを表示してG003を設定(ガイダンスメッセージ)
                MyBase.ShowMessage(frm)
                MyBase.ShowMessage(frm, "G003")
                Return False
            Else
                'エラーメッセージを表示
                MyBase.ShowMessage(frm)
                Return False
            End If
        End If


        '終了メッセージ表示
        '2016.01.06 UMANO 英語化対応START
        'MyBase.SetMessage("G002", New String() {"印刷", ""})
        MyBase.SetMessage("G002", New String() {frm.btnPrint.Text(), ""})
        '2016.01.06 UMANO 英語化対応END

        Dim prevDt As DataTable = rtnDsP.Tables(LMConst.RD)
        If prevDt.Rows.Count > 0 Then

            'プレビューの生成 
            Dim prevFrm As New RDViewer()

            'データ設定 
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始 
            prevFrm.Run()

            'プレビューフォームの表示 
            prevFrm.Show()

            'プレビューフォームを画面の前面に表示
            prevFrm.Focus()

        End If



        Return True
    End Function

    ''' <summary>
    ''' 行削除
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteInkaSData(ByVal frm As LMF060F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMF060C.EventShubetsu.DEL) = False Then

            '終了処理
            Call Me.EndAction(frm)
            Exit Sub

        End If

        Dim rtnResult As Boolean = True

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            'チェックがついている件数
            arr = Me._LMFConG.GetCheckList(frm.sprDetail.ActiveSheet, LMF060G.sprDetailDef.DEF.ColNo)
        End If

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._LMFConV.IsSelectChk(arr.Count)

        'エラーがある場合、スルー
        If rtnResult = False Then
            MyBase.ShowMessage(frm, "E009")

            '処理終了アクション
            Call Me.EndAction(frm)

            Exit Sub
        End If

        For i As Integer = arr.Count - 1 To 0 Step -1

            '選択された行を物理削除
            frm.sprDetail.ActiveSheet.Rows(Convert.ToInt32(arr(i))).Remove()

        Next

        '画面全ロックの解除
        Me.UnLockedControls(frm)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

#End Region 'イベント定義(一覧)

#Region "運賃取得、試算結果退避処理"
    ''' <summary>
    ''' 運賃取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function UnchinGet(ByVal frm As LMF060F) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMF060C.EventShubetsu.UNCHINGET) = False Then

            '終了処理
            Call Me.EndAction(frm)
            Return False

        End If

        Dim data As String = MyBase.GetSystemDateTime.ToString
        '入力チェック
        If Me._V.IsInputUnchinCheck(data) = False Then

            '終了処理
            Call Me.EndAction(frm)
            Return False

        End If

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMF800DS()
        Dim dt As DataTable = ds.Tables("UNCHIN_CALC_IN")
        Dim dr As DataRow = dt.NewRow()
        Dim Todo As String = frm.txtTodokedeJisCd.TextValue

        With dr

            .Item("ACTION_FLG") = "00"
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            .Item("DEST_CD") = ""
            .Item("DEST_JIS") = Todo
            .Item("ARR_PLAN_DATE") = frm.imdUnsoDate.TextValue
            .Item("UNSO_PKG_NB") = 0
            .Item("NB_UT") = LMConst.FLG.OFF
            .Item("UNSO_WT") = frm.numJyuryo.TextValue
            .Item("UNSO_ONDO_KB") = ""
            .Item("TARIFF_BUNRUI_KB") = frm.cmbUnso.SelectedValue
            .Item("VCLE_KB") = frm.cmbSyasyu.SelectedValue
            .Item("MOTO_DATA_KB") = "40"
            .Item("SEIQ_TARIFF_CD") = frm.txtTariffCd.TextValue
            .Item("SEIQ_ETARIFF_CD") = frm.txtWarimashiCd.TextValue
            .Item("UNSO_TTL_QT") = 0
            .Item("SIZE_KB") = ""
            .Item("UNSO_DATE") = frm.imdUnsoDate.TextValue
            .Item("CARGO_KB") = ""
            .Item("CAR_TP") = "00"
            .Item("WT_LV") = 0
            .Item("KYORI") = frm.numKyori.TextValue
            .Item("DANGER_KB") = LMConst.FLG.ON     ' 一般品
            .Item("GOODS_CD_NRS") = ""

        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        LMFormNavigate.NextFormNavigate(Me, "LMF800", prm)

        Dim outTbl As DataTable = prm.ParamDataSet.Tables("UNCHIN_CALC_OUT")

        'データセット((LMF810C.TABLE_NM_OUT)
        Dim outDr As DataRow = outTbl.Rows(0)

        With frm

            Dim err As DataTable = prm.ParamDataSet.Tables("LMF800RESULT")
            Dim count As Integer = err.Rows.Count

            'LMF800RESULTからエラーメッセージを取得する
            Dim errDr As DataRow = err.Rows(0)
            Dim hantei As String = String.Empty

            hantei = errDr.Item("STATUS").ToString
            Dim nomal As String = "00"
           
            Select Case hantei

                Case nomal

                    '運賃プログラム取得後(運賃金額、(内割増分)にデータセット
                    .numUnchin.Value = Me._LMFConG.FormatNumValue(outDr.Item("UNCHIN").ToString())

                    Dim city As Decimal = Convert.ToDecimal(Me._LMFConG.FormatNumValue(outDr.Item("CITY_EXTC").ToString()))
                    Dim touki As Decimal = Convert.ToDecimal(Me._LMFConG.FormatNumValue(outDr.Item("WINT_EXTC").ToString()))
                    Dim rely As Decimal = Convert.ToDecimal(Me._LMFConG.FormatNumValue(outDr.Item("RELY_EXTC").ToString()))
                    Dim toll As Decimal = Convert.ToDecimal(Me._LMFConG.FormatNumValue(outDr.Item("TOLL").ToString()))
                    Dim insu As Decimal = Convert.ToDecimal(Me._LMFConG.FormatNumValue(outDr.Item("INSU").ToString()))
                    Dim unchin As Decimal = Convert.ToDecimal(_LMFConG.FormatNumValue(outDr.Item("UNCHIN").ToString())) '(2013/02/06 Notes 1826)

                    '取得したデータをラベルに設定(割増)
                    .lblWint.Value = touki
                    .lblRely.Value = rely
                    .lblInsu.Value = insu
                    .lblCity.Value = city
                    .lblFrry.Value = toll
                    .lblUnchinMeisai.Value = unchin '(2013/02/06 Notes 1826)


                    '合計値を設定
                    .numWarimashi.Value = touki + rely + insu + city + toll

                    '割増分を運賃に上乗せし、最終的な金額を表示。(2013/02/06 Notes 1826)
                    .numUnchin.Value = unchin + touki + rely + insu + city + toll '(ヘッダ用) (2013/02/06 Notes 1826)

                Case Else

                    '異常系(返却値からメッセージを設定)
                    MyBase.ShowMessage(frm, errDr.Item("ERROR_CD").ToString, New String() {errDr.Item("YOBI1").ToString})

                    'ゼロを設定
                    .numUnchin.Value = 0
                    .numWarimashi.Value = 0

                    Call Me.EndAction(frm)

                    Return False
            End Select

        
        End With


        '画面全ロックの解除
        Me.UnLockedControls(frm)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Function

    ''' <summary>
    ''' 試算結果退避処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function SisanSet(ByVal frm As LMF060F) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMF060C.EventShubetsu.SISANSET)

        Dim dt As DataTable = Nothing

        '値セットする時のスプレッドの呼び出し
        Call Me._G.SetSpread(frm)

        '画面全ロックの解除
        Me.UnLockedControls(frm)

        '処理終了アクション
        Call Me.EndAction(frm)


    End Function

#End Region

#Region "データセット"
    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMF060F, ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF060C.TABLE_NM_IN)

        'データテーブルの項目をクリア
        dt.Clear()

        Dim dr As DataRow = dt.NewRow()

        With frm


            Dim origJis As String = .txtOrigJis.TextValue
            Dim destJis As String = .txtTodokedeJisCd.TextValue

            .txtTodokedeJisCd.TextValue = destJis.ToString()
            .txtOrigJis.TextValue = origJis.ToString()

            '大小比較で発地と届先を判定
            If .txtTodokedeJisCd.TextValue < .txtOrigJis.TextValue Then
                'OrigJISを退避
                Dim value As String = .txtOrigJis.TextValue

                '入替
                origJis = destJis
                destJis = value


            End If



            '検索条件の格納
            dr("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr("KYORI_CD") = .txtKyoriteiCd.TextValue
            dr("ORIG_JIS_CD") = origJis
            dr("DEST_JIS_CD") = destJis



            '検索条件の追加
            dt.Rows.Add(dr)
        End With

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(印刷用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInDataPrint(ByVal frm As LMF060F) As DataSet

        '印刷用のデータセットのINの情報を設定

        Dim ds As DataSet = New LMF540DS
        Dim dt As DataTable = ds.Tables(LMF060C.TABLE_NM_540_IN)

        'データテーブルの項目をクリア
        dt.Clear()

        Dim dr As DataRow = dt.NewRow()

        '要望番号:1556 KIM 2012/11/02 START
        Dim changeFlg As String = "0"
        '要望番号:1556 KIM 2012/11/02 START

        With frm

            Dim origJis As String = .txtOrigJis.TextValue
            Dim destJis As String = .txtTodokedeJisCd.TextValue

            .txtTodokedeJisCd.TextValue = destJis.ToString()
            .txtOrigJis.TextValue = origJis.ToString()

            '大小比較で発地と届先を判定
            If .txtTodokedeJisCd.TextValue < .txtOrigJis.TextValue Then
                'OrigJISを退避
                Dim value As String = .txtOrigJis.TextValue

                '入替
                origJis = destJis
                destJis = value

                '要望番号:1556 KIM 2012/11/02 START
                changeFlg = "1"
                '要望番号:1556 KIM 2012/11/02 START

            End If

            Dim Kyori As String = .numKyori.TextValue

            Dim Kyorikanma As String = Replace(Kyori, ",", "")

            dr("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr("CUST_CD_L") = .txtCustCdL.TextValue
            dr("CUST_CD_M") = .txtCustCdM.TextValue
            dr("ORIG_JIS_CD") = origJis
            dr("DEST_JIS_CD") = destJis
            dr("KYORI") = Kyorikanma
            dr("KYORI_CD") = .txtKyoriteiCd.TextValue
            dr("UNCHIN_TARIFF_CD") = .txtTariffCd.TextValue
            dr("EXTC_TARIFF_CD") = .txtWarimashiCd.TextValue

            Dim datanow As String = .imdUnsoDate.TextValue
            dr("STR_DATE") = datanow

            '運送日が入力されているとき
            If String.IsNullOrEmpty(datanow) = False Then
                Dim data4 As String = String.Concat(datanow.Substring(4, 2), datanow.Substring(6, 2))
                dr("STR_DATE_4") = data4
            End If

            '要望番号:1556 KIM 2012/11/02 START
            dr("CHANGE_FLG") = changeFlg
            '要望番号:1556 KIM 2012/11/02 START

            '検索条件の追加
            dt.Rows.Add(dr)
        End With


        Return ds

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMF060F)

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
    Private Sub EndAction(ByVal frm As LMF060F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        If MyBase.IsMessageExist() = True Then

            '運賃計算プログラムでエラーがある場合は表示
            MyBase.ShowMessage(frm)
        Else
            'メッセージエリアの設定
            Call Me.ShowGMessage(frm)
        End If

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' サーバアクセス後のエラー設定
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Sub SetErrorControl(ByVal ctl As Control)

        '値がない場合、スルー
        If String.IsNullOrEmpty(DirectCast(ctl, Win.InputMan.LMImTextBox).TextValue) = True Then
            Exit Sub
        End If

        'エラー設定
        Call Me._LMFConV.SetErrorControl(ctl)

    End Sub

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMF060F)

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
    Private Sub SetGMessage(ByVal frm As LMF060F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub
#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMF060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        'マスタ参照処理の呼び出し
        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMF060F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMF060F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    ''' <summary>
    ''' 距離取得ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnKyoriSel_Click(ByVal frm As LMF060F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnKyoriSel_Click")

        Call Me.KyoriAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnKyoriSel_Click")

    End Sub

    ''' <summary>
    ''' 印刷ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByVal frm As LMF060F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnPrint_Click")

        Call Me.PrintAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnPrint_Click")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub KeyDown(ByVal frm As LMF060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 行削除ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnDel_Click(ByVal frm As LMF060F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnDel_Click")

        Call Me.DeleteInkaSData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnDel_Click")

    End Sub

    ''' <summary>
    ''' 運賃取得ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnGet_Click(ByVal frm As LMF060F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnGet_Click")

        Call Me.UnchinGet(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnGet_Click")

    End Sub

    ''' <summary>
    ''' 試算結果退避処理ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnSet_Click(ByVal frm As LMF060F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnSet_Click")

        Call Me.SisanSet(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnSet_Click")

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMF060F) As Boolean

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ照会(LMZ260)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function CustPop(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)

        If prm.ReturnFlg = True Then

            '荷主マスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                .lblCustNm.TextValue = Me._LMFConG.EditConcatData(dr.Item("CUST_NM_L").ToString(), dr.Item("CUST_NM_M").ToString(), LMFControlC.ZENKAKU_SPACE)
                'START YANAI 要望番号836
                .txtKyoriteiCd.TextValue = dr.Item("BETU_KYORI_CD").ToString()

                If ("00").Equals(.cmbSyasyu.SelectedValue) OrElse _
                    ("01").Equals(.cmbSyasyu.SelectedValue) OrElse _
                    ("02").Equals(.cmbSyasyu.SelectedValue) OrElse _
                    ("03").Equals(.cmbSyasyu.SelectedValue) Then
                    .txtTariffCd.TextValue = dr.Item("UNCHIN_TARIFF_CD2").ToString()
                    .lblTariffNm.TextValue = dr.Item("UNCHIN_TARIFF_REM2").ToString()
                Else
                    .txtTariffCd.TextValue = dr.Item("UNCHIN_TARIFF_CD1").ToString()
                    .lblTariffNm.TextValue = dr.Item("UNCHIN_TARIFF_REM1").ToString()
                End If

                .txtWarimashiCd.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()
                .lblWarimashiNm.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()
                'END YANAI 要望番号836

            End With

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMF060C.EventShubetsu.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ260")

    End Function

    ''' <summary>
    ''' JISマスタ照会(LMZ070)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function OrigJisPop(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowJisPopup(frm, actionType)

        If prm.ReturnFlg = True Then
            'ここはJISマスタ照会or郵便番号マスタ
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ070C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtOrigJis.TextValue = dr.Item("JIS_CD").ToString()
                .lblOrigJisNm.TextValue = String.Concat(dr.Item("KEN").ToString(), dr.Item("SHI").ToString())

            End With
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' JISマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowJisPopup(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ070DS()
        Dim dt As DataTable = ds.Tables(LMZ070C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            'START SHINOHARA 要望番号513
            If actionType = LMF060C.EventShubetsu.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("JIS_CD") = frm.txtOrigJis.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ070")

    End Function

    ''' <summary>
    ''' JISマスタ照会(LMZ070)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function TodokedeJisPop(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowToJisPopup(frm, actionType)

        If prm.ReturnFlg = True Then
            'ここはJISマスタ照会or郵便番号マスタ
            '郵便番号マスタ照会の場合
            'Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ060C.TABLE_NM_OUT).Rows(0)
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ070C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtTodokedeJisCd.TextValue = dr.Item("JIS_CD").ToString()
                .txtTodokedeJisNm.TextValue = String.Concat(dr.Item("KEN").ToString(), dr.Item("SHI").ToString())
            End With
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' JISマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowToJisPopup(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ070DS()
        Dim dt As DataTable = ds.Tables(LMZ070C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            'START SHINOHARA 要望番号513
            If actionType = LMF060C.EventShubetsu.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("JIS_CD") = frm.txtTodokedeJisCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ070")

    End Function

    ''' <summary>
    ''' 距離程マスタ照会(LMZ080)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function KyoriteiPop(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As Boolean

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
    Private Function ShowKyoriPopup(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ080DS()
        Dim dt As DataTable = ds.Tables(LMZ080C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMF060C.EventShubetsu.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("KYORI_CD") = frm.txtKyoriteiCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ080")

    End Function

    ''' <summary>
    ''' タリフマスタ照会(LMZ230)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function TariffPop(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowTariffPopup(frm, actionType)

        If prm.ReturnFlg = True Then
            'タリフマスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ230C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtTariffCd.TextValue = dr.Item("UNCHIN_TARIFF_CD").ToString()
                .lblTariffNm.TextValue = dr.Item("UNCHIN_TARIFF_REM").ToString()

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
    Private Function ShowTariffPopup(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As LMFormData

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
            'START SHINOHARA 要望番号513
            If actionType = LMF060C.EventShubetsu.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("UNCHIN_TARIFF_CD") = frm.txtTariffCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("STR_DATE") = frm.imdUnsoDate.TextValue
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ230")

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ照会(LMZ230)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function YokoTariffPop(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowYokoTariffPopup(frm, actionType)

        If prm.ReturnFlg = True Then
            '横持ちタリフマスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ100C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtTariffCd.TextValue = dr.Item("YOKO_TARIFF_CD").ToString()
                .lblTariffNm.TextValue = dr.Item("YOKO_REM").ToString()

            End With
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowYokoTariffPopup(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ100DS()
        Dim dt As DataTable = ds.Tables(LMZ100C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMF060C.EventShubetsu.ENTER Then
                'END SHINOHARA 要望番号513
                '.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                '.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                .Item("YOKO_TARIFF_CD") = frm.txtTariffCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFConH.FormShow(ds, "LMZ100", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 割増タリフマスタ照会(LMZ240)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function WarimashiPop(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowWarimashiPopup(frm, actionType)

        If prm.ReturnFlg = True Then
            '割増タリフマスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ240C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtWarimashiCd.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()
                .lblWarimashiNm.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()
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
    Private Function ShowWarimashiPopup(ByVal frm As LMF060F, ByVal actionType As LMF060C.EventShubetsu) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ240DS()
        Dim dt As DataTable = ds.Tables(LMZ240C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMF060C.EventShubetsu.ENTER Then
                'END SHINOHARA 要望番号513
                '.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                '.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                .Item("EXTC_TARIFF_CD") = frm.txtWarimashiCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ240")

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

#End Region 'イベント振分け

#End Region 'Method

End Class