' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB030H : 入荷報告書
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMB030ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMB030H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMB030V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMB030G

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
        Dim frm As LMB030F = New LMB030F(Me)

        'Validate共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMBconV = New LMBControlV(Me, sForm)

        'Hnadler共通クラスの設定
        Me._LMBconH = New LMBControlH(sForm, MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMBconG = New LMBControlG(sForm)

        'Gamenクラスの設定
        Me._G = New LMB030G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMB030V(Me, frm, Me._LMBconV)


        'フォームの初期化
        Call Me.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMB030C.MODE_DEFAULT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(Me.GetPGID())

        ''メッセージの表示
        Me.ShowMessage(frm, "G007")

        '画面の入力項目の制御
        Call _G.SetControlsStatus()

        '営業所は自営業所
        Me._G.SetcmbNrsBrCd(frm)


        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

#Region "マスタ参照"
    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMB030F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック()
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMB030C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMB030C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then

            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMB030C.EventShubetsu.MASTEROPEN)

        '終了メッセージ設定
        MyBase.SetMessage("G007")


        '処理終了アクション
        Call Me.EndAction(frm)
        'フォーカス移動処理
        Call Me.NextFocusedControl(frm, True)



    End Sub
    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMB030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMB030C.EventShubetsu.MASTEROPEN)

        ''カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMB030C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMB030C.EventShubetsu.ENTER)

        '終了メッセージ設定
        MyBase.SetMessage("G007")

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me.NextFocusedControl(frm, eventFlg)

    End Sub
#Region "Pop"
    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMB030F, ByVal objNm As String, ByVal actionType As LMB030C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me.StartAction(frm)


            Select Case objNm

                Case .txtCustCD_L.Name

                    '荷主(大)Lコード
                    'START YANAI 要望番号481
                    'Call Me.CustPop(frm)
                    Call Me.CustPop(frm, actionType)
                    'END YANAI 要望番号481

                Case .txtCustCD_M.Name
                    '荷主(中)Mコード
                    'START YANAI 要望番号481
                    'Call Me.CustPop(frm)
                    Call Me.CustPop(frm, actionType)
                    'END YANAI 要望番号481


            End Select

        End With

        Return True
    End Function


#End Region

#End Region

#Region "マスタPOP"
    'START YANAI 要望番号481
    '''' <summary>
    '''' 荷主マスタ照会(LMZ260)参照
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <returns>True:選択有 False:選択無</returns>
    '''' <remarks></remarks>
    '''' 
    'Private Function CustPop(ByVal frm As LMB030F) As Boolean
    ''' <summary>
    ''' 荷主マスタ照会(LMZ260)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function CustPop(ByVal frm As LMB030F, ByVal actionType As LMB030C.EventShubetsu) As Boolean
        'END YANAI 要望番号481

        'START YANAI 要望番号481
        'Dim prm As LMFormData = Me.ShowCustPopup(frm)
        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)
        'END YANAI 要望番号481

        If prm.ReturnFlg = True Then

            '荷主マスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtCustCD_L.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCD_M.TextValue = dr.Item("CUST_CD_M").ToString()
                .lblCustNM_L.TextValue = dr.Item("CUST_NM_L").ToString()
                .lblCustNM_M.TextValue = dr.Item("CUST_NM_M").ToString()

            End With


        End If

        Return False

    End Function

    'START YANAI 要望番号481
    '''' <summary>
    '''' 荷主マスタ参照POP起動
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <remarks></remarks>
    'Private Function ShowCustPopup(ByVal frm As LMB030F) As LMFormData
    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMB030F, ByVal actionType As LMB030C.EventShubetsu) As LMFormData
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
            '.Item("CUST_CD_L") = frm.txtCustCD_L.TextValue
            '.Item("CUST_CD_M") = frm.txtCustCD_M.TextValue
            If (LMB030C.EventShubetsu.ENTER).Equals(actionType) = True Then
                .Item("CUST_CD_L") = frm.txtCustCD_L.TextValue
                .Item("CUST_CD_M") = frm.txtCustCD_M.TextValue
            End If
            'END YANAI 要望番号481
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ260")

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

#Region "印刷区分値変更"
    Private Sub Print(ByVal frm As LMB030F)

        'ロック制御
        Call Me._G.Locktairff(frm)

        '終了メッセージ表示
        MyBase.SetMessage("G007")

        '終了処理
        Call Me.EndAction(frm)

    End Sub
#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function PrintShutu(ByVal frm As LMB030F) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMB030C.EventShubetsu.PRINT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck()

        'エラーがある場合、終了
        If rtnResult = False Then

            '画面解除
            MyBase.UnLockedControls(frm)

            'Cursorを元に戻す
            Cursor.Current = Cursors.Default()

            Return rtnResult

        End If

        'Notes 1619 2013/03/12 START
        'ワーニングメッセージ表示(W223)

        If MyBase.ShowMessage(frm, "W223") = MsgBoxResult.Cancel Then '「キャンセル」を選択

            '処理を中断して終了
            MyBase.SetMessage("G007")
            '処理終了アクション
            Call Me.EndAction(frm)
            Return True
            Exit Function
        End If

        'Notes 1619 2013/03/12 END

        'Notes 1619 2013/03/12 START
        Dim inkaFlg As String = String.Empty

        '入荷実績対象荷主かの判断を行う。(EDI荷主マスタから、キャッシュ取得)
        inkaFlg = Me.SetCachedEDICust(frm, inkaFlg)

        'Notes 1619 2013/03/12 END

        'データセットの設定
        Dim rtDs As DataSet = Me.SetPrtDs(frm, New LMB030DS)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintAction")

        '==========================
        'WSAクラス呼出
        '==========================
        rtDs.Merge(New RdPrevInfoDS)
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        rtDs = MyBase.CallWSA(blf, LMB030C.ACTION_ID_PRINT, rtDs)
        If IsMessageExist() = True Then

            Me.EndAction(frm)
            ''メッセージの表示
            Me.ShowMessage(frm, "G007")
            Return False

        End If
        'Notes 1619 2013/03/14 START

        If inkaFlg = "0" Then '入荷実績対象荷主でない場合のみ、進捗区分のみを更新する。

            'データセットの設定
            Dim rtDsInka As DataSet = Me.SetPrtDs(frm, New LMB030DS)

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateInkaState")

            '==========================
            'WSAクラス呼出
            '==========================
            rtDs.Merge(New RdPrevInfoDS)
            'Dim blf As String = String.Concat(MyBase.GetPGID(), LMFControlC.BLF)
            rtDsInka = MyBase.CallWSA(blf, LMB030C.ACTION_ID_UPDATE_INKA, rtDsInka)

            If IsMessageExist() = True Then

                Me.EndAction(frm)
                ''メッセージの表示
                Me.ShowMessage(frm, "G007")
                Return False

            End If

        Else
        End If
        'Notes 1619 2013/03/14 END


        'プレビュー判定 
        Dim prevDt As DataTable = rtDs.Tables(LMConst.RD)
        If prevDt.Rows.Count > 0 Then

            'プレビューの生成 
            Dim prevFrm As New RDViewer()

            'データ設定 
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始 
            prevFrm.Run()

            'プレビューフォームの表示 
            prevFrm.Show()

        End If


        '終了メッセージ表示
        MyBase.SetMessage("G002", New String() {"印刷", ""})


        '処理終了アクション
        Call Me.EndAction(frm)

        Return True


    End Function

#End Region

    ''' <summary>
    ''' 入荷報告書印刷用INデータセット作成
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetPrtDs(ByVal frm As LMB030F, ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables(LMB030C.TABLE_NM_IN).NewRow

        With frm

            '営業所コード
            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue

            '振替含む
            Dim inkaKb As String = String.Empty
            If .chkFurikae.Checked = True Then
                inkaKb = "50"
            End If
            dr.Item("INKA_KB") = inkaKb

            '荷主コード(大）
            If String.IsNullOrEmpty(.txtCustCD_L.TextValue) = False Then
                dr.Item("CUST_CD_L") = .txtCustCD_L.TextValue.Trim()
            End If

            '荷主コード(中）
            If String.IsNullOrEmpty(.txtCustCD_M.TextValue) = False Then
                dr.Item("CUST_CD_M") = .txtCustCD_M.TextValue.Trim()
            End If

            'データ登録日
            If String.IsNullOrEmpty(.imdDataInsDate.TextValue) = False Then
                dr.Item("SYS_ENT_DATE") = .imdDataInsDate.TextValue.Trim()
            End If

            Dim fDate As String = String.Empty
            Dim tDate As String = String.Empty

            '印刷種別による分岐
            Dim prtSyubetu As String = .cmbPrint.SelectedValue.ToString.Trim()

            Select Case prtSyubetu

                Case LMB030C.GETUJI
                    fDate = String.Concat(.imdNyukaDate.TextValue.Substring(0, 6), "01")
                    '(2012.02.28) 月次の範囲は、入力月の1日から翌月1日ではなく入力月の月末とする。
                    'tDate = String.Concat(Format(.imdNyukaDate.Value.AddMonths(1), "yyyyMM"), "01")
                    'tDate = String.Concat(Format(.imdNyukaDate.Value.AddMonths(1).AddDays(-1), "yyyyMMdd"))
                    'fdate当月の末日を求める
                    '当月の末日を求める
                    tDate = Convert.ToString(DateSerial(Convert.ToInt32(Mid(fDate, 1, 4)), Convert.ToInt32(Mid(fDate, 5, 2)), Convert.ToInt32(Mid(fDate, 7, 2))).AddMonths(1))
                    tDate = String.Concat(tDate.Substring(0, 7), "/01")
                    tDate = Convert.ToString(DateSerial(Convert.ToInt32(Mid(tDate, 1, 4)), Convert.ToInt32(Mid(tDate, 6, 2)), Convert.ToInt32(Mid(tDate, 9, 2))).AddDays(-1))
                    tDate = String.Concat( _
                                            Mid(tDate, 1, 4), _
                                            Mid(tDate, 6, 2), _
                                            Mid(tDate, 9, 2) _
                                           )
                Case LMB030C.NITIJI
                    fDate = .imdNyukaDate.TextValue
                    tDate = .imdNyukaDate.TextValue

            End Select

            dr.Item("INKA_DATE_FROM") = fDate
            dr.Item("INKA_DATE_TO") = tDate

        End With

        ds.Tables(LMB030C.TABLE_NM_IN).Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMB030F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMB030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '印刷処理の呼び出し
        Call Me.PrintShutu(frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMB030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'マスタ参照処理の呼び出し
        Call Me.OpenMasterPop(frm)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMB030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMB030F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub KeyDown(ByVal frm As LMB030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 印刷変更時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub Print(ByVal frm As LMB030F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Me.Print(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub


    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
#Region "フォーカス移動"
    ''' <summary>
    ''' 次コントロールにフォーカス移動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="eventFlg">Enterボタンの場合、True</param>
    ''' <remarks></remarks>
    Friend Sub NextFocusedControl(ByVal frm As Form, ByVal eventFlg As Boolean)

        'Enter以外の場合、スルー
        If eventFlg = False Then
            Exit Sub
        End If

        frm.SelectNextControl(frm.ActiveControl, True, True, True, True)

    End Sub
#End Region
#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMB030F)

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
    Private Sub EndAction(ByVal frm As LMB030F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        MyBase.ShowMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region
#Region "データセット"
    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMB030F, ByVal ds As DataSet) As DataSet

        With frm


            '入荷日の値を取得
            Dim Inka As String = .imdNyukaDate.TextValue

            Dim PrintKb As String = .cmbPrint.SelectedValue.ToString

            Dim InkaFrom As String = String.Empty
            Dim InkaTo As String = String.Empty
            Dim InkaKb As String = String.Empty

            Dim dt As DataTable = ds.Tables(LMB030C.TABLE_NM_IN)

            Dim dr As DataRow = dt.NewRow()

            '印刷区分が月次の場合
            If LMB030C.GETUJI.Equals(PrintKb) = True Then

                'チェックボックスにチェックが入っている場合
                If .chkFurikae.Checked = True Then
                    InkaKb = "50"

                End If

                '入荷日のddを固定値で01を設定
                InkaFrom = String.Concat(Inka.Substring(0, 6), "01")

                Dim nowDate As String = String.Concat(DateFormatUtility.EditSlash(InkaFrom))

                Dim nextDate As DateTime = Convert.ToDateTime(DateFormatUtility.EditSlash(nowDate))

                '入荷日のmmを+1
                nextDate = nextDate.AddMonths(1)

                '入荷日のmmを+1、ddを01固定値したデータを設定
                InkaTo = nextDate.ToString(LMB030C.DATE_YYYYMMDD)

            Else

                '入荷日のFROMの値を設定
                InkaFrom = Inka

                '入荷日のTOの値を設定
                InkaTo = Inka

                'チェックボックスにチェックが入っている場合
                If .chkFurikae.Checked = True Then
                    InkaKb = "50"

                End If

            End If

            '検索条件の格納
            dr("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr("CUST_CD_L") = .txtCustCD_L.TextValue
            dr("CUST_CD_M") = .txtCustCD_M.TextValue
            dr("INKA_DATE_FROM") = InkaFrom
            dr("INKA_DATE_TO") = InkaTo
            dr("INKA_KB") = InkaKb
            dr("SYS_ENT_DATE") = .imdDataInsDate.TextValue
            ds.Tables(LMB030C.TABLE_NM_IN).Rows.Add(dr)

        End With

        Return ds

    End Function

#End Region

#Region "Notes1619"
    ''' <summary>
    ''' キャッシュから名称取得（全項目）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function SetCachedEDICust(ByVal frm As LMB030F, ByVal inkaFlg As String) As String

        Dim EDI As String = String.Empty

        With frm
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'GetCachedEDICust(LMUserInfoManager.GetNrsBrCd(), .txtCustCD_L.TextValue, .txtCustCD_M.TextValue)
            GetCachedEDICust(.cmbEigyo.SelectedValue.ToString(), .txtCustCD_L.TextValue, .txtCustCD_M.TextValue)

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'EDI = GetCachedEDICust(LMUserInfoManager.GetNrsBrCd(), .txtCustCD_L.TextValue, .txtCustCD_M.TextValue).ToString
            EDI = GetCachedEDICust(.cmbEigyo.SelectedValue.ToString(), .txtCustCD_L.TextValue, .txtCustCD_M.TextValue).ToString

            If String.IsNullOrEmpty(EDI) Then
                inkaFlg = "0"  '入荷実績対象荷主なし(EDI荷主でない、または存在しても実績対象フラグがゼロ)
            Else
                inkaFlg = "1"  '入荷実績対象荷主あり
            End If

        End With

        Return inkaFlg

    End Function

    ''' <summary>
    ''' 荷主キャッシュから入荷実績対象荷主かの判断
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedEDICust(ByVal nrsBrcd As String, _
    ByVal custCdL As String, _
    ByVal custCdM As String) As String

        Dim dr As DataRow() = Nothing

        '荷主存在
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EDI_CUST).Select(String.Concat( _
                                                                           "INOUT_KB = '1' AND " _
                                                                         , "NRS_BR_CD = '", nrsBrcd, "' AND " _
                                                                         , "CUST_CD_L = '", custCdL, "' AND " _
                                                                         , "CUST_CD_M = '", custCdM, "' AND " _
                                                                         , "FLAG_01 <> '0' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))



        If 0 < dr.Length Then
            Return dr.Length.ToString '入荷実績対象荷主あり
        End If

        Return String.Empty

    End Function

#End Region 'Notes1619


    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class