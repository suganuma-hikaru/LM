' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD070H : 在庫帳票印刷
'  作  成  者       :  成田
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports GrapeCity.Win.Editors.Fields
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMD070ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMD070H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMD070V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMD070G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconV As LMDControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconH As LMDControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconG As LMDControlG

    ''' <summary>
    ''' データ保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _CustCdL As String

    ''' <summary>
    ''' データ保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _CustCdM As String

    '(2012.12.13)要望番号1671 在庫証明書(小･極小)対応 -- START --
    ''' <summary>
    ''' データ保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _CustCdS As String

    ''' <summary>
    ''' データ保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _CustCdSS As String
    '(2012.12.13)要望番号1671 在庫証明書(小･極小)対応 --  END  --

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    '※テスト追加
    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMQconH As LMQControlH

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

        Me._CustCdL = String.Empty
        Me._CustCdM = String.Empty

        'フォームの作成
        Dim frm As LMD070F = New LMD070F(Me)

        'Validate共通クラスの設定
        Me._LMDconV = New LMDControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMDconH = New LMDControlH(DirectCast(frm, Form), MyBase.GetPGID())

        'Gamen共通クラスの設定
        Me._LMDconG = New LMDControlG(Me, DirectCast(frm, Form))

        'Validateクラスの設定
        Me._V = New LMD070V(Me, frm, Me._LMDconV, Me._LMDconG)

        'Gamenクラスの設定
        Me._G = New LMD070G(Me, frm, Me._LMDconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(Me.GetPGID(), frm)

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        '月末在庫コンボ生成
        Call Me._G.SetZaikoDateControl(Nothing)

        '荷動き・単位の設定
        Call Me._G.CreateNiugokiComboBox(frm.cmbNiugoki, LMKbnConst.KBN_Z020)

        '追加開始 --- 2014.11.17
        'エクセル印刷
        Me._LMQconH = New LMQControlH(DirectCast(frm, Form), GetPGID())
        '追加終了 --- 2014.11.17

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
    Private Sub OpenMasterPop(ByVal frm As LMD070F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        'メッセージのクリア
        MyBase.ClearMessageAria(frm)

        '権限チェック()
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD070C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMD070C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMD070C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me._LMDconH.EndAction(frm, Me.GetGMessage())

        '現在の荷主コード(大)･(中)･(小)･(極小)の設定
        Me._CustCdL = frm.txtCustCdL.TextValue
        Me._CustCdM = frm.txtCustCdM.TextValue

        '(2012.12.13)要望番号1671 在庫証明書(小･極小)対応 -- START --
        Me._CustCdS = frm.txtCustCdS.TextValue
        Me._CustCdSS = frm.txtCustCdSs.TextValue
        '(2012.12.13)要望番号1671 在庫証明書(小･極小)対応 --  END  --

        'フォーカス移動処理
        Call Me._LMDconH.NextFocusedControl(frm, True)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMD070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMD070C.EventShubetsu.MASTEROPEN)

        ''カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMD070C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then

            'フォーカス移動処理
            Call Me._LMDconH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMD070C.EventShubetsu.ENTER)

        '処理終了アクション
        Call Me._LMDconH.EndAction(frm, Me.GetGMessage())

        'フォーカス移動処理
        Call Me._LMDconH.NextFocusedControl(frm, eventFlg)

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
    Private Function ShowPopupControl(ByVal frm As LMD070F, ByVal objNm As String, ByVal actionType As LMD070C.EventShubetsu) As Boolean

        With frm

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name, .txtCustCdS.Name, .txtCustCdSs.Name

                    '荷主(大)Lコード
                    Call Me.CustPop(frm, actionType)

            End Select

        End With

        Return True

    End Function

#End Region

#End Region

#Region "マスタPOP"

    ''' <summary>
    ''' 荷主マスタ照会(LMZ260)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function CustPop(ByVal frm As LMD070F, ByVal actionType As LMD070C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)

        If prm.ReturnFlg = True Then

            '荷主マスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm
                .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                .lblCustNmL.TextValue = dr.Item("CUST_NM_L").ToString()
                .lblCustNmM.TextValue = dr.Item("CUST_NM_M").ToString()
                If .txtCustCdS.ReadOnly = False AndAlso .txtCustCdSs.ReadOnly = False Then
                    .txtCustCdS.TextValue = dr.Item("CUST_CD_S").ToString()
                    .txtCustCdSs.TextValue = dr.Item("CUST_CD_SS").ToString()
                    .lblCustNmS.TextValue = dr.Item("CUST_NM_S").ToString()
                    .lblCustNmSs.TextValue = dr.Item("CUST_NM_SS").ToString()
                End If

            End With

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMD070F, ByVal actionType As LMD070C.EventShubetsu) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMD070C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                If frm.txtCustCdS.ReadOnly = False AndAlso _
                   frm.txtCustCdSs.ReadOnly = False Then
                    .Item("CUST_CD_S") = frm.txtCustCdS.TextValue
                    .Item("CUST_CD_SS") = frm.txtCustCdSs.TextValue
                End If
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
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

#Region "印刷区分値変更"

    ''' <summary>
    ''' 印刷区分値変更
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub Print(ByVal frm As LMD070F)

        'ロック制御
        Call Me._G.LockPrint(frm)

        '営業所がNULLのときは処理をしない
        If String.IsNullOrEmpty(frm.cmbEigyo.SelectedValue.ToString()) = False Then

            '月末在庫取得
            Call Me.SetDataSetInDataGetu(frm, New LMD070DS)

        End If

        Select Case frm.cmbPrint.SelectedValue.ToString

            Case LMD070C.PRINT_ZAIKO_SEIGOUSEI_JITU, LMD070C.PRINT_ZAIKO_SEIGOUSEI_SHUKA, LMD070C.PRINT_ZAIKO_SEIGOUSEI_HIKI, LMD070C.PRINT_SYOUBOU_BUNRUI_ALL

                '在庫整合性リストの場合印刷範囲Fromに当日日付を設定
                '当日日付の取得
                Dim nowDate As String = Convert.ToDateTime(DateFormatUtility.EditSlash(MyBase.GetSystemDateTime(0))).ToString("yyyyMMdd")

                frm.imdPrintDateS.TextValue = nowDate


            Case LMD070C.PRINT_FUDOU

                '荷動き・単位の設定
                Call Me._G.CreateNiugokiComboBox(frm.cmbNiugoki, LMKbnConst.KBN_Z020)

                '入庫日にチェック
                frm.chkNyuko.Checked = True

        End Select

        '終了処理
        Call Me._LMDconH.EndAction(frm, Me.GetGMessage())

    End Sub

#End Region

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function PrintShutu(ByVal frm As LMD070F) As Boolean

        '荷主コード(大)･(中)･(小)･(極小)の値の保持
        Me._CustCdL = frm.txtCustCdL.TextValue.Trim()
        Me._CustCdM = frm.txtCustCdM.TextValue.Trim()

        '(2012.12.13)要望番号1671 在庫証明書(小･極小)対応 -- START --
        Me._CustCdS = frm.txtCustCdS.TextValue
        Me._CustCdSS = frm.txtCustCdSs.TextValue
        '(2012.12.13)要望番号1671 在庫証明書(小･極小)対応 --  END  --

        '処理開始アクション
        Call Me._LMDconH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMD070C.EventShubetsu.PRINT) = False Then
            '処理終了アクション
            Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
            Return False
        End If

        '入力チェック
        If Me._V.IsInputCheck(LMD070C.PRINT_FLG) = False Then
            '処理終了アクション
            Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
            Return False
        End If

        'データセット作成
        Dim rtDs As DataSet = Me.SetDataSetInData(frm, New LMD070DS())
        rtDs.Merge(New RdPrevInfoDS)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'START 在庫証明書 処理スピード向上
        Dim wkFlg As Boolean = False

        '(2012.12.13)要望番号1671 在庫証明書(小･極小)対応 -- START --
        'If frm.cmbPrint.SelectedValue.Equals("03") = True Then
        If frm.cmbPrint.SelectedValue.Equals("03") = True Or frm.cmbPrint.SelectedValue.Equals("12") = True Then
            If MessageBox.Show("ワークテーブルを利用して印刷しますか？(LMD571)", "確認", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                wkFlg = True
            End If
            '2013.02.01 消防類別在庫重量表 処理スピード向上 Start
        ElseIf frm.cmbPrint.SelectedValue.Equals("13") = True Then
            If MessageBox.Show("ワークテーブルを利用して印刷しますか？(LMD621)", "確認", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                rtDs.Tables(LMD070C.TABLE_NM_IN).Rows(0).Item("SHOBO_FLG") = "01"
            Else
                rtDs.Tables(LMD070C.TABLE_NM_IN).Rows(0).Item("SHOBO_FLG") = "00"
            End If
            '2013.02.01 消防類別在庫重量表 処理スピード向上 End
        ElseIf frm.cmbPrint.SelectedValue.Equals("15") = True Then
            rtDs.Tables(LMD070C.TABLE_NM_IN).Rows(0).Item("SHOBO_FLG") = "02"
        End If
        'END 在庫証明書 処理スピード向上

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMDControlC.BLF)

        'START 在庫証明書 処理スピード向上
        'Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMD070C.ACTION_ID_CHECK, rtDs)
        Dim rtnDs As DataSet = Nothing
        If wkFlg = True Then
            rtnDs = MyBase.CallWSA(blf, "SelectCheckLMD571", rtDs)
        Else
            'DBリードオンリー対応
            'rtnDs = MyBase.CallWSA(blf, LMD070C.ACTION_ID_CHECK, rtDs)
            Select Case rtDs.Tables(LMD070C.TABLE_NM_IN).Rows(0).Item("PRINT_FLAG").ToString()
                Case "01", "03", "07", "08", "09", "10", "11", "12"
                    'LMD530,LMD550,LMD560,LMD570,LMD590,LMD620,LMD622
                    rtnDs = MyBase.CallWSA(blf, LMD070C.ACTION_ID_CHECK, rtDs, True)
                Case "13", "15"
                    'LMD62x
                    Select Case rtDs.Tables(LMD070C.TABLE_NM_IN).Rows(0).Item("SHOBO_FLG").ToString()
                        Case "01"
                            'LMD621
                            rtnDs = MyBase.CallWSA(blf, LMD070C.ACTION_ID_CHECK, rtDs)
                        Case Else
                            'LMD620,LMD622
                            rtnDs = MyBase.CallWSA(blf, LMD070C.ACTION_ID_CHECK, rtDs, True)
                    End Select
                Case Else
                    'LMD630,もしくは不明
                    rtnDs = MyBase.CallWSA(blf, LMD070C.ACTION_ID_CHECK, rtDs)
            End Select
        End If

        '追加開始 --- 2014.11.17
        'EXCEL 出力
        If String.IsNullOrEmpty(frm.cmbPrintSub.TextValue().ToString()) = False Then
            'サブ選択リストが使用されている場合、実行する。
            If Me.ExcelPrint(frm, rtnDs) = True Then

                MyBase.ShowMessage(frm, "G002", New String() {"印刷", ""})
                '処理終了アクション
                Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
                Return True
            Else
                Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
                Return False
            End If
        End If
        '追加終了 --- 2014.11.17

        'END 在庫証明書 処理スピード向上
        If MyBase.IsMessageExist() = True Then

            If MyBase.IsWarningMessageExist() = True Then         'Warningの場合

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    'START 在庫証明書 処理スピード向上
                    '印刷処理
                    'rtnDs = MyBase.CallWSA(blf, LMD070C.ACTION_ID_PRINT, rtDs)
                    If wkFlg = True Then
                        rtnDs = MyBase.CallWSA(blf, "PrintDataLMD571", rtDs)
                    Else
                        'DBリードオンリー対応
                        'rtnDs = MyBase.CallWSA(blf, LMD070C.ACTION_ID_PRINT, rtDs)
                        Select Case rtDs.Tables(LMD070C.TABLE_NM_IN).Rows(0).Item("PRINT_FLAG").ToString()
                            Case "01", "03", "07", "08", "09", "10", "11", "12"
                                'LMD530,LMD550,LMD560,LMD570,LMD590,LMD620,LMD622
                                rtnDs = MyBase.CallWSA(blf, "PrintDataLMD571", rtDs, True)
                            Case "13", "15"
                                'LMD62x
                                Select Case rtDs.Tables(LMD070C.TABLE_NM_IN).Rows(0).Item("PRINT_FLAG").ToString()
                                    Case "01"
                                        'LMD621
                                        rtnDs = MyBase.CallWSA(blf, "PrintDataLMD571", rtDs)
                                    Case Else
                                        'LMD620,LMD622
                                        rtnDs = MyBase.CallWSA(blf, "PrintDataLMD571", rtDs, True)
                                End Select
                            Case Else
                                'LMD630,もしくは不明
                                rtnDs = MyBase.CallWSA(blf, "PrintDataLMD571", rtDs)
                        End Select
                    End If
                    'END 在庫証明書 処理スピード向上

                Else

                    'いいえを選択時
                    '処理終了アクション
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
                    MyBase.ShowMessage(frm, "G007")
                    Return False

                End If

            Else

                'エラーメッセージ判定
                If MyBase.IsErrorMessageExist() = True Then

                    frm.txtCustCdM.BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                    frm.cmbDataInsDate.BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                    Call Me._LMDconV.SetErrorControl(frm.txtCustCdL)

                End If

                MyBase.ShowMessage(frm)

                '処理終了アクション
                Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
                Return False

            End If

        End If

        'プレビュー判定 
        Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)
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

        '帳票プログラムでエラーがある場合
        If MyBase.IsMessageExist() = True Then

            MyBase.ShowMessage(frm)

        Else

            '終了メッセージ表示
            MyBase.ShowMessage(frm, "G002", New String() {"印刷", ""})

        End If

        '処理終了アクション
        Call Me._LMDconH.EndAction(frm, Me.GetGMessage())

        Return True

    End Function

#End Region

#Region "フォーカスOUT処理"

    ''' <summary>
    ''' フォーカスOUT処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function Focus(ByVal frm As LMD070F) As Boolean

        Dim nowCustCdL As String = frm.txtCustCdL.TextValue
        Dim nowCustCdM As String = frm.txtCustCdM.TextValue

        '前の荷主コード(大)、(中)と今の荷主コード(大)、荷主コード(中)が値がいずれかが変更されていれば処理をすすめる
        If Me._CustCdL.Equals(nowCustCdL) = True _
            AndAlso Me._CustCdM.Equals(nowCustCdM) _
            Then

            Return False

        End If

        '月末在庫の取得
        Call Me.SetDataSetInDataGetu(frm, New LMD070DS)

        'START YANAI 要望番号1057 在庫証明書出力順変更
        '出力順の取得
        Call Me.GetSortKb(frm)
        'END YANAI 要望番号1057 在庫証明書出力順変更

        Return True

    End Function

    ''' <summary>
    ''' ガイダンスメッセージを取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGMessage() As String
        Return "G007"
    End Function

#End Region

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMD070F) As Boolean

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
    Friend Sub FunctionKey7Press(ByRef frm As LMD070F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey10Press(ByRef frm As LMD070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'マスタ参照
        Me.OpenMasterPop(frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMD070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMD070F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub KeyDown(ByVal frm As LMD070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 印刷コンボボックス変更時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub Print(ByVal frm As LMD070F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Me.Print(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォーカス移動時変更時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub Lost(ByVal frm As LMD070F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Me.Focus(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 荷主コード(大)フォーカス移動時変更時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub Got(ByVal frm As LMD070F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '荷主コード(大)のデータを取得
        Me._CustCdL = frm.txtCustCdL.TextValue

        '荷主コード(中)のデータを取得
        Me._CustCdM = frm.txtCustCdM.TextValue

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

#Region "データセット"

    ''' <summary>
    ''' データセット設定(印刷)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMD070F, ByVal ds As DataSet) As DataSet

        With frm

            Dim dt As DataTable = ds.Tables(LMD070C.TABLE_NM_IN)
            Dim dr As DataRow = dt.NewRow()
            Dim Getu As String = .cmbDataInsDate.SelectedValue.ToString()

            '検索条件の格納
            dr("PRINT_FLAG") = .cmbPrint.SelectedValue()
            dr("NRS_BR_CD") = .cmbEigyo.SelectedValue()
            dr("CUST_CD_L") = .txtCustCdL.TextValue.Trim()
            dr("CUST_CD_M") = .txtCustCdM.TextValue.Trim()
            dr("CUST_CD_S") = .txtCustCdS.TextValue.Trim()
            dr("CUST_CD_SS") = .txtCustCdSs.TextValue.Trim()
            dr("OUTKA_PLAN_DATE") = .imdSyukkaDate.TextValue.Trim()
            If LMD070C.SHOKI_ZAIKO_FLG.Equals(Getu) = True OrElse String.IsNullOrEmpty(Getu) = True Then
                dr("GETSUMATSU_ZAIKO") = "00000000"
            Else
                dr("GETSUMATSU_ZAIKO") = .cmbDataInsDate.SelectedValue()
            End If
            dr("PRT_YMD_FROM") = .imdPrintDateS.TextValue.Trim()
            dr("PRT_YMD_TO") = .imdPrintDateE.TextValue.Trim()

            '入庫日
            Dim ChkNyu As Boolean = .chkNyuko.Checked
            '出荷日
            Dim ChkShuka As Boolean = .chkSyukka.Checked

            Dim ChkFlg As String = String.Empty

            '入庫日、出荷日のチェックが両方チェックがついていた場合
            If ChkNyu = True AndAlso
            ChkShuka = True Then

                'チェックフラグを03にする
                ChkFlg = "03"

                '入庫日、出荷日にチェックがついていなかった場合
            ElseIf ChkNyu = False AndAlso
            ChkShuka = False Then

                'チェックフラグを03にする
                ChkFlg = "03"

            End If

            'チェックフラグに値が入っていた場合は処理をしない
            If String.IsNullOrEmpty(ChkFlg) = True Then

                '入庫日にチェックがついている場合
                If ChkNyu = True Then

                    'チェックフラグを01にする
                    ChkFlg = "01"

                    '出荷日にチェックがついている場合
                ElseIf ChkShuka = True Then

                    'チェックフラグを02にする
                    ChkFlg = "02"

                End If

            End If

            '在庫基準
            dr("ZAI_KIJUN") = ChkFlg

            '荷動き・単位
            dr("MOVE_TANNI") = .cmbNiugoki.SelectedValue()

            'START YANAI 要望番号1057 在庫証明書出力順変更
            '出力順
            dr("SORT_KBN") = .cmbSort.SelectedValue()
            'END YANAI 要望番号1057 在庫証明書出力順変更

            '追加開始 2014.11.19 kikuchi 
            dr("PRINT_SUB_FLAG") = .cmbPrintSub.SelectedValue()
            '追加終了 2014.11.19 kikuchi

            'add 2017/06/01　在庫証明書のとき、角印印字するか確認
            'ADD 2017/07/03  在庫証明書（小・極小毎）のとき、角印印字するか確認
            If .cmbPrint.SelectedValue.ToString.Equals(LMD070C.PRINT_ZAIKO_SHOUMEI) _
                Or .cmbPrint.SelectedValue.ToString.Equals(LMD070C.PRINT_ZAIKO_SHOUMEI_S_SS) Then
                If MessageBox.Show("角印を印字しますか？", "確認", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                    '請求書出力内容変更適用年月を取得
                    Dim hanteiYm As String = "000000"
                    Dim kbnDrs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'B043'")
                    If 0 < kbnDrs.Length Then
                        hanteiYm = kbnDrs(0).Item("KBN_NM1").ToString()
                    End If

                    '印刷範囲(開始)と請求書出力内容変更適用年月を比較
                    If Left(dr("PRT_YMD_FROM").ToString, 6) < hanteiYm Then
                        '適用日より前：旧社名
                        dr("KAKUIN_FLG") = "1"
                        dr("KAKUIN_NM") = henkoKakuinNm()
                    Else
                        '適用日以降：新社名
                        dr("KAKUIN_FLG") = "2"
                        dr("KAKUIN_NM") = henkoKakuinNm()
                    End If

                Else
                    dr("KAKUIN_FLG") = LMConst.FLG.OFF
                End If

            End If

            dt.Rows.Add(dr)

        End With

        Return ds

    End Function

    ''' <summary>
    ''' 角印画像ファイル名取得処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function henkoKakuinNm() As String

        'ログインユーザーの部署を取得
        Dim busyo As String = String.Empty
        Dim busyoDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LMUserInfoManager.GetUserID, "'"))
        If 0 < busyoDr.Length Then
            busyo = busyoDr(0).Item("BUSYO_CD").ToString
        End If


        '変更する角印のファイル名を取得
        Dim kakuinNm As String = String.Empty
        Dim kbnDrs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'D033' AND KBN_NM2 = '", busyo, "'"))
        If 0 < kbnDrs.Length Then
            kakuinNm = kbnDrs(0).Item("KBN_NM3").ToString()
        Else
            kakuinNm = "NRS"
        End If

        Return kakuinNm

    End Function

#End Region

#Region "月末在庫"

    ''' <summary>
    ''' データセット設定(月末のデータセットIN)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInDataGetu(ByVal frm As LMD070F, ByVal ds As DataSet) As DataSet

        With frm

            '月末在庫がロックされていたら処理終了()
            If .cmbDataInsDate.ReadOnly = True Then
                Return ds
            End If

            '荷主コードの存在チェック
            Dim CustCdL As String = .txtCustCdL.TextValue
            Dim CustCdM As String = .txtCustCdM.TextValue
            Dim CustCdS As String = .txtCustCdS.TextValue
            Dim CustCdSS As String = .txtCustCdSs.TextValue
            Dim drc As DataRow() = Nothing

            drc = Me._LMDconV.SelectCustListDataRow(CustCdL, CustCdM, CustCdS, CustCdSS)

            Dim count As Integer = drc.Count
            If count < 1 Then
                Return ds
            End If

            Dim dt As DataTable = ds.Tables(LMD070C.TABLE_NM_GETU_IN)
            Dim dr As DataRow = dt.NewRow()

            '検索条件の格納
            dr("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr("CUST_CD_L") = .txtCustCdL.TextValue
            dr("CUST_CD_M") = .txtCustCdM.TextValue
            dr("SYS_DEL_FLG") = "0"

            ds.Tables(LMD070C.TABLE_NM_GETU_IN).Rows.Add(dr)

        End With

        Me.SetDataSetGetuDate(frm, ds)

        Return ds

    End Function

    ''' <summary>
    ''' 月末在庫の日付検索
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetGetuDate(ByVal frm As LMD070F, ByVal ds As DataSet) As DataSet

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '==========================
        'WSAクラス呼出
        '==========================

        Dim blf As String = String.Concat(MyBase.GetPGID(), LMDControlC.BLF)
        'DBリードオンリー対応
        'Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMD070C.ACTION_ID_SELECT, ds)
        Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMD070C.ACTION_ID_SELECT, ds, True)

        '月末在庫コンボ生成
        Call Me._G.SetZaikoDateControl(rtnDs.Tables(LMD070C.TABLE_NM_GETU_OUT))

        '処理終了アクション
        Call Me._LMDconH.EndAction(frm, Me.GetGMessage())

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Return ds

    End Function

#End Region

#Region "出力順"

    'START YANAI 要望番号1057 在庫証明書出力順変更
    ''' <summary>
    ''' 出力順コンボボックス設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub GetSortKb(ByVal frm As LMD070F)

        With frm

            '出荷順がロックされていたら処理終了
            If .cmbSort.ReadOnly = True Then
                Exit Sub
            End If

            '荷主明細の取得
            Dim custDetailsDr As DataRow() = Nothing
            '要望番号:1253 terakawa 2012.07.13 Start
            'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", .txtCustCdL.TextValue, "' ", _
            '                                                                                                "AND SUB_KB = '18'"))
            custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' ", _
                                                                                                            "AND CUST_CD = '", .txtCustCdL.TextValue, "' ", _
                                                                                                            "AND SUB_KB = '18'"))
            '要望番号:1253 terakawa 2012.07.13 End
            If 0 < custDetailsDr.Length Then
                If ("01").Equals(custDetailsDr(0).Item("SET_NAIYO").ToString) = True Then
                    .cmbSort.SelectedValue = "02"
                Else
                    .cmbSort.SelectedValue = "01"
                End If
            Else
                .cmbSort.SelectedValue = "01"
            End If

        End With

    End Sub
    'END YANAI 要望番号1057 在庫証明書出力順変更

#End Region

#Region "LMQメソッド呼出"
    ''' <summary>
    ''' ExcelCreator呼び出し処理
    ''' </summary>
    ''' <param name="ds">出力データ</param>
    ''' <remarks></remarks>
    Private Function ExcelPrint(ByVal frm As LMD070F, ByVal ds As DataSet) As Boolean

        Dim resultFlg As Boolean = False
        Dim prtPrintCD As String = frm.cmbPrintSub.SelectedValue().ToString
        Dim prtExcelID As String = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
              .Select(String.Concat("KBN_GROUP_CD = 'Z021' AND KBN_CD = '", prtPrintCD, "'"))(0).Item("KBN_NM2").ToString()
        Dim prtFileNM As String = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
              .Select(String.Concat("KBN_GROUP_CD = 'Z021' AND KBN_CD = '", prtPrintCD, "'"))(0).Item("KBN_NM3").ToString()

        'EXCEL印刷用テーブルに名称を変更
        ds.Tables("LMD630OUT").TableName = LMQControlC.TABLE_NM_OUT

        If Me._V.CheckParameter(ds, prtExcelID, LMQControlC.ChkObject.ALL_OBJECT) = False Then
            Exit Function
        End If

        resultFlg = Me._LMQconH.ExcelPrint(ds, prtExcelID, prtFileNM)

        Return (resultFlg)

    End Function

#End Region
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class