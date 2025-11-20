' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC040C : 在庫引当
'  作  成  者       :  矢内
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC040ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC040H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMC040V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMC040G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconV As LMCControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconH As LMCControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconG As LMCControlG

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索結果格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _SelectZaiko As DataTable

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' 営業所
    ''' </summary>
    ''' <remarks></remarks>
    Private _Eigyo As String

    ''' <summary>
    ''' 倉庫
    ''' </summary>
    ''' <remarks></remarks>
    Private _Soko As String

    ''' <summary>
    ''' 他荷主モードフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _TaninusiFlg As String

    ''' <summary>
    ''' Leave処理を行うかのフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _LeaveFlg As String = LMConst.FLG.ON

    'START YANAI 要望番号507
    ''' <summary>
    ''' 出荷(小)件数
    ''' </summary>
    ''' <remarks></remarks>
    Private _OutkaSCnt As Integer
    'END YANAI 要望番号507

    ''' <summary>
    ''' 選択押下処理フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _SelectFlg As String = LMConst.FLG.OFF

    '2013.02.13 要望番号1824 START
    ''' <summary>
    ''' ロンザエラーフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _lnzErrFlg As String = LMConst.FLG.OFF
    '2013.02.13 要望番号1824 END

    '2014.09.11 追加START
    ''' <summary>
    ''' 行追加フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _AddRecFlg As String
    '2014.09.11 追加END

#End Region 'Field

#Region "Method"

#Region "初期処理"

    'START KIM 要望番号1479 一括引当時、引当画面の速度改善

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△開始")

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        Me._Prm = prm

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet()

        '営業所・倉庫の保存
        Dim inDr As DataRow = Me._PrmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0)
        Me._Eigyo = inDr("NRS_BR_CD").ToString()
        Me._Soko = inDr("WH_CD").ToString()
        Me._TaninusiFlg = inDr("TANINUSI_FLG").ToString()
        Me._OutkaSCnt = Convert.ToInt32(inDr("OUTKA_S_CNT").ToString())

        '2014.09.11 追加START
        Me._AddRecFlg = inDr("ADD_FLG").ToString()
        '2014.09.11 追加END

        '2014.08.12 修正START
        Dim dr As DataRow()
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
                                                                           "NRS_BR_CD = '", inDr("NRS_BR_CD").ToString(), "' AND " _
                                                                         , "CUST_CD = '", inDr("CUST_CD_L").ToString(), "' AND " _
                                                                         , "SUB_KB = '80'"))

        If dr.Length > 0 Then
            inDr("LIKE_FLG") = "1"
        End If

        '★★★フォームを作成する前に引当処理判定を行う
        Dim frm As LMC040F = Nothing

        '出荷検索画面以外からの遷移の場合、フォームを作成する
        If (LMC040C.PGID_LMC010).Equals(MyBase.RootPGID()) = True Then

            'Validateクラスの設定
            MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△ValidateNew開始")
            Me._V = New LMC040V(Me, frm)

            'Gamenクラスの設定
            MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△GamenNew開始")
            Me._G = New LMC040G(Me, frm)

            'START 2012/3/4 CSV引当対応（引当数量・個数=1）
            '個数の計算をする
            'Call Me.SetCalSumForWithoutForm(LMC040C.EventShubetsu.SYOKI, Me._PrmDs)
            Call Me.SetCalSumForWithoutForm(LMC040C.EventShubetsu.SYOKI, Me._PrmDs, inDr("HIKIATE_FLG").ToString())
            'END   2012/3/4 CSV引当対応（引当数量・個数=1）

        Else

            'フォームの作成
            MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△FormNew開始")
            frm = New LMC040F(Me)

            'Validateクラスの設定
            MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△ValidateNew開始")
            Me._V = New LMC040V(Me, frm)

            'Gamenクラスの設定
            MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△GamenNew開始")
            Me._G = New LMC040G(Me, frm)

            'フォームの初期化
            MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△InitControl開始")
            MyBase.InitControl(frm)

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            Call MyBase.TitleSwitching(frm)
            '2015.10.15 英語化対応END

            'キーイベントをフォームで受け取る
            frm.KeyPreview = True

            'ファンクションキーの設定
            Me._G.SetFunctionKey(Me._TaninusiFlg, Me._OutkaSCnt)

            'タブインデックスの設定
            Me._G.SetTabIndex()

            'コントロール個別設定
            Me._G.SetControl(MyBase.GetPGID())

            Me._G.SetInitValue(frm)

            'Validate共通クラスの設定
            MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△共通ConのNew開始")
            Me._LMCconV = New LMCControlV(Me, DirectCast(frm, Form))

            'Hnadler共通クラスの設定
            Me._LMCconH = New LMCControlH(DirectCast(frm, Form))

            'Gamen共通クラスの設定
            Me._LMCconG = New LMCControlG(Me, DirectCast(frm, Form))

            'INの値を画面に表示
            MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△Me._G.SetInitForm開始")
            '2014.09.11 修正START
            'Me._G.SetInitForm(frm, Me._PrmDs)
            Me._G.SetInitForm(frm, Me._PrmDs, Me._AddRecFlg)
            '2014.09.11 修正END

            'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
            MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△ Me._G.InitSpread開始")
            Me._G.InitSpread()

            '呼び元による分岐
            If MyBase.RootPGID.Equals("LMD010") Then

                'オプションボタンロック制御
                frm.optCnt.Checked = True
                frm.optAmt.Enabled = False
                frm.optKowake.Enabled = False
                frm.optSample.Enabled = False

            End If

        End If

        'START KIM 倉庫システム2.0 特定荷主対応 2012/9/20
        'If ("01").Equals(inDr("HIKIATE_FLG").ToString()) = True OrElse _
        '     ("02").Equals(inDr("HIKIATE_FLG").ToString()) = True Then
        If ("01").Equals(inDr("HIKIATE_FLG").ToString()) = True OrElse _
           ("02").Equals(inDr("HIKIATE_FLG").ToString()) = True OrElse _
           ("03").Equals(inDr("HIKIATE_FLG").ToString()) = True Then
            'END KIM 倉庫システム2.0 特定荷主対応 2012/9/20

            '自動引当時

            '自動引当処理を行う
            MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△自動引当開始")
            Dim rtnDs As DataSet = Me.SelectDataAutoHiki(frm, Me._PrmDs)
            MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△自動引当できた")

            Dim openFlg As Boolean = False
            If rtnDs Is Nothing = True Then
                openFlg = True
            End If

            If openFlg = False Then
                If rtnDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT).Rows.Count = 0 Then
                    openFlg = True
                End If
            End If

            If openFlg = True Then

                '自動引当時、エラーの場合

                If (LMC040C.PGID_LMC010).Equals(MyBase.RootPGID()) = True Then
                    '出荷検索画面から遷移の場合
                    'リターンフラグにFalseをセット
                    Me._Prm.ReturnFlg = False

                    '画面を閉じる
                    LMFormNavigate.Revoke(Me)
                    Exit Sub

                Else

                    '出荷検索画面以外からの遷移の場合
                    '画面の入力項目の制御
                    Call _G.SetControlsStatus()

                    'フォーカスの設定
                    Call Me._G.SetFoucus()

                    '呼び出し元画面情報を設定
                    frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

                    'フォームの表示
                    frm.ShowDialog()

                    Exit Sub

                End If

            End If

            'outのパラメータをセット
            Me._Prm.ParamDataSet = rtnDs

            'リターンフラグにTrueをセット
            Me._Prm.ReturnFlg = True
            MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△自動引当処理終了")

            '画面を閉じる
            LMFormNavigate.Revoke(Me)
            MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△画面も閉じれた")
            Exit Sub

        Else
            '手動引当時

            'メッセージの表示
            MyBase.ShowMessage(frm, "G007")

            '検索処理を行う
            Me.SelectData(frm, LMC040C.NEW_MODE, LMC040C.EventShubetsu.KENSAKU, Me._PrmDs)

            'フォーカスの設定
            Call Me._G.SetFoucus()

            '2014.09.11 修正START
            '画面の入力項目の制御
            Call Me._G.SetControlsStatus(Me._AddRecFlg)
            '2014.09.11 修正END

            '呼び出し元画面情報を設定
            frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

            'フォームの表示
            frm.ShowDialog()

            'カーソルを元に戻す
            Cursor.Current = Cursors.Default

        End If

    End Sub

    '''' <summary>
    '''' ハンドラクラスの初期処理メソッド
    '''' </summary>
    '''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    'Public Sub Main(ByVal prm As LMFormData)

    '    MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△開始")

    '    'カーソルを砂時計にする
    '    Cursor.Current = Cursors.WaitCursor

    '    Me._Prm = prm

    '    '画面間データを取得する
    '    Me._PrmDs = prm.ParamDataSet()

    '    '営業所・倉庫の保存
    '    Dim inDr As DataRow = Me._PrmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0)
    '    Me._Eigyo = inDr("NRS_BR_CD").ToString()
    '    Me._Soko = inDr("WH_CD").ToString()
    '    Me._TaninusiFlg = inDr("TANINUSI_FLG").ToString()
    '    'START YANAI 要望番号507
    '    Me._OutkaSCnt = Convert.ToInt32(inDr("OUTKA_S_CNT").ToString())
    '    'END YANAI 要望番号507

    '    'フォームの作成
    '    MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△FormNew開始")
    '    Dim frm As LMC040F = New LMC040F(Me)

    '    'Validateクラスの設定
    '    MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△ValidateNew開始")
    '    Me._V = New LMC040V(Me, frm)

    '    'Gamenクラスの設定
    '    MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△GamenNew開始")
    '    Me._G = New LMC040G(Me, frm)

    '    'フォームの初期化
    '    MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△InitControl開始")
    '    MyBase.InitControl(frm)

    '    'キーイベントをフォームで受け取る
    '    frm.KeyPreview = True

    '    'ファンクションキーの設定
    '    'START YANAI 要望番号507
    '    'Me._G.SetFunctionKey(Me._TaninusiFlg)
    '    Me._G.SetFunctionKey(Me._TaninusiFlg, Me._OutkaSCnt)
    '    'END YANAI 要望番号507

    '    'タブインデックスの設定
    '    Me._G.SetTabIndex()

    '    'コントロール個別設定
    '    Me._G.SetControl(MyBase.GetPGID())

    '    Me._G.SetInitValue(frm)

    '    'Validate共通クラスの設定
    '    MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△共通ConのNew開始")
    '    Me._LMCconV = New LMCControlV(Me, DirectCast(frm, Form))

    '    'Hnadler共通クラスの設定
    '    Me._LMCconH = New LMCControlH(DirectCast(frm, Form))

    '    'Gamen共通クラスの設定
    '    Me._LMCconG = New LMCControlG(Me, DirectCast(frm, Form))

    '    'INの値を画面に表示
    '    MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△Me._G.SetInitForm開始")
    '    Me._G.SetInitForm(frm, Me._PrmDs)

    '    'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
    '    MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△△ Me._G.InitSpread開始")
    '    Me._G.InitSpread()

    '    '呼び元による分岐
    '    If MyBase.RootPGID.Equals("LMD010") Then

    '        'オプションボタンロック制御
    '        frm.optCnt.Checked = True
    '        frm.optAmt.Enabled = False
    '        frm.optKowake.Enabled = False
    '        frm.optSample.Enabled = False

    '    End If

    '    'START YANAI 要望番号341
    '    'If ("01").Equals(inDr("HIKIATE_FLG").ToString()) = True Then
    '    If ("01").Equals(inDr("HIKIATE_FLG").ToString()) = True OrElse _
    '         ("02").Equals(inDr("HIKIATE_FLG").ToString()) = True Then
    '        'END YANAI 要望番号341
    '        '自動引当時

    '        '自動引当処理を行う
    '        MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△自動引当開始")
    '        Dim rtnDs As DataSet = Me.SelectDataAutoHiki(frm, Me._PrmDs)
    '        MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△自動引当できた")

    '        'START YANAI 要望番号1200 自動引当・一括引当変更
    '        'If rtnDs Is Nothing = True Then
    '        Dim openFlg As Boolean = False
    '        If rtnDs Is Nothing = True Then
    '            openFlg = True
    '        End If

    '        If openFlg = False Then
    '            If rtnDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT).Rows.Count = 0 Then
    '                openFlg = True
    '            End If
    '        End If

    '        If openFlg = True Then
    '            'END YANAI 要望番号1200 自動引当・一括引当変更

    '            '自動引当時、エラーの場合
    '            'START YANAI メモ②No.15,16,17
    '            '画面の入力項目の制御
    '            'Call _G.SetControlsStatus()

    '            ''フォーカスの設定
    '            'Call Me._G.SetFoucus()

    '            ''呼び出し元画面情報を設定
    '            'frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

    '            ''フォームの表示
    '            'frm.ShowDialog()

    '            'Exit Sub
    '            If (LMC040C.PGID_LMC010).Equals(MyBase.RootPGID()) = True Then
    '                '出荷検索画面から遷移の場合
    '                'リターンフラグにFalseをセット
    '                Me._Prm.ReturnFlg = False

    '                '画面を閉じる
    '                LMFormNavigate.Revoke(Me)
    '                Exit Sub

    '            Else
    '                '出荷検索画面以外からの遷移の場合
    '                '画面の入力項目の制御
    '                Call _G.SetControlsStatus()

    '                'フォーカスの設定
    '                Call Me._G.SetFoucus()

    '                '呼び出し元画面情報を設定
    '                frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

    '                'フォームの表示
    '                frm.ShowDialog()

    '                Exit Sub

    '            End If
    '            'END YANAI メモ②No.15,16,17

    '        End If

    '        'outのパラメータをセット
    '        Me._Prm.ParamDataSet = rtnDs

    '        'リターンフラグにTrueをセット
    '        Me._Prm.ReturnFlg = True
    '        MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△自動引当処理終了")

    '        '画面を閉じる
    '        LMFormNavigate.Revoke(Me)
    '        MyBase.Logger.WriteLog(0, "LMC040H", "引当画面", "△△画面も閉じれた")
    '        Exit Sub

    '    Else
    '        '手動引当時

    '        'メッセージの表示
    '        MyBase.ShowMessage(frm, "G007")

    '        '検索処理を行う
    '        Me.SelectData(frm, LMC040C.NEW_MODE, LMC040C.EventShubetsu.KENSAKU, Me._PrmDs)

    '        'フォーカスの設定
    '        Call Me._G.SetFoucus()

    '        '画面の入力項目の制御
    '        Call Me._G.SetControlsStatus()

    '        '呼び出し元画面情報を設定
    '        frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

    '        'フォームの表示
    '        frm.ShowDialog()

    '        'カーソルを元に戻す
    '        Cursor.Current = Cursors.Default

    '    End If

    'End Sub

    'END KIM 要望番号1479 一括引当時、引当画面の速度改善

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 検索以外のイベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMC040C.EventShubetsu, ByVal frm As LMC040F)

        'チェックリスト格納変数
        Dim list As ArrayList = New ArrayList()

        '権限チェック
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        'ダブルクリックの場合、検索行をクリックした場合は、処理を行わない
        If LMC040C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) Then
            'クリックした行が検索行の場合
            If 0.Equals(frm.sprZaiko.Sheets(0).ActiveRow.Index()) = True Then
                Exit Sub
            End If
        End If


        Select Case eventShubetsu
            Case LMC040C.EventShubetsu.TANINUSI   '他荷主

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                '商品キーの取得。
                '遷移元画面からは商品コードしかパラメータとして渡されてないので、M_FURI_GOODSとのJOINで必要な商品キーはこのタイミングで取得。
                'ただし、一度取得すればいいので、他荷主ボタン押下が2回目以降は取得処理が行われないようにする
                Me._G.SetGoodsNrs()

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Eigyo, Me._Soko) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)
                    Exit Sub
                End If

                'ファンクションキーの設定
                'START YANAI 要望番号507
                'Me._G.SetFunctionKey(Me._TaninusiFlg)
                Me._G.SetFunctionKey(Me._TaninusiFlg, Me._OutkaSCnt)
                'END YANAI 要望番号507

                '検索処理を行う
                Me.SelectData(frm, LMC040C.NEW_MODE, LMC040C.EventShubetsu.TANINUSI, Me._PrmDs)

                'フォーカスの設定
                Me._G.SetFoucus()

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                '画面の入力項目の制御（ここで行わないと全項目がロック解除されてしまうため）
                Me._G.SetControlsStatus()

            Case LMC040C.EventShubetsu.KENSAKU  '検索

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                'LeaveフラグをOffに設定
                Me._LeaveFlg = LMConst.FLG.OFF

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Eigyo, Me._Soko) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)

                    'LeaveフラグをOnに設定
                    Me._LeaveFlg = LMConst.FLG.ON
                    Exit Sub

                End If

                'ファンクションキーの設定
                'START YANAI 要望番号507
                'Me._G.SetFunctionKey(Me._TaninusiFlg)
                Me._G.SetFunctionKey(Me._TaninusiFlg, Me._OutkaSCnt)
                'END YANAI 要望番号507

                '検索処理を行う
                Me.SelectData(frm, LMC040C.NEW_MODE, LMC040C.EventShubetsu.KENSAKU, Me._PrmDs)

                'フォーカスの設定
                Me._G.SetFoucus()

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                '画面の入力項目の制御（ここで行わないと全項目がロック解除されてしまうため）
                Me._G.SetControlsStatus()

                'LeaveフラグをOnに設定
                Me._LeaveFlg = LMConst.FLG.ON

            Case LMC040C.EventShubetsu.SENTAKU  '選択

                Me._SelectFlg = LMConst.FLG.ON

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                '個数の計算をする
                Dim calsumFlg As Boolean = True
                Select Case frm.ActiveControl.Name
                    Case "numSyukkaKosu", "numSyukkaHasu"
                        Me._G.SetCalSum(LMC040C.EventShubetsu.CAL_KONSU)
                    Case "numSyukkaSouAmt"
                        Me._G.SetCalSum(LMC040C.EventShubetsu.CAL_SURYO)
                        'START YANAI 20111027 入り目対応
                        'Case "numIrime"
                        '    Me._G.SetCalSum(LMC040C.EventShubetsu.CAL_IRIME)
                        'END YANAI 20111027 入り目対応
                End Select
                If calsumFlg = False Then

                    Me._SelectFlg = LMConst.FLG.OFF

                    Exit Sub
                End If

                '引当個数・数量ALLゼロチェック
                If Me._V.IsAllZeroChk() = True Then
                    'ALLゼロ時、上から順に引当個数・数量を設定していく。
                    Me._G.SetAllZero()

                End If

                '引当個数、引当数量の計算をする
                Me._G.SetHikiSum(eventShubetsu)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Eigyo, Me._Soko) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu) = False Then

                    Me._SelectFlg = LMConst.FLG.OFF

                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)
                    Exit Sub
                End If

                '選択処理を行う
                Dim rtnDs As DataSet = Me.OutDataSet(frm)

                If rtnDs Is Nothing = False Then

                    'outのパラメータをセット
                    Me._Prm.ParamDataSet = rtnDs

                    Me._SelectFlg = LMConst.FLG.OFF

                    '画面を閉じる
                    frm.Close()
                    Exit Sub

                End If

                Me._SelectFlg = LMConst.FLG.OFF

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                'START YANAI 20111027 入り目対応
            'Case LMC040C.EventShubetsu.CAL_KONSU, LMC040C.EventShubetsu.CAL_SURYO, LMC040C.EventShubetsu.CAL_IRIME  '梱数・端数・数量・入目変更
            Case LMC040C.EventShubetsu.CAL_KONSU, LMC040C.EventShubetsu.CAL_SURYO  '梱数・端数・数量変更
                'END YANAI 20111027 入り目対応

                'フラグ判定
                If LMConst.FLG.ON.Equals(Me._LeaveFlg) = False Then
                    Exit Sub
                End If

                'メッセージのクリア
                MyBase.ClearMessageAria(DirectCast(frm, Jp.Co.Nrs.LM.GUI.Win.Interface.ILMForm))

                '個数の計算をする
                Dim calsumFlg As Boolean = Me._G.SetCalSum(eventShubetsu)
                If calsumFlg = False Then
                    Exit Sub
                End If

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Eigyo, Me._Soko) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)
                    Exit Sub
                End If

            Case LMC040C.EventShubetsu.CHANGE_SPREAD    'スプレッドの引当個数、引当数量変更

                If (Me._SelectFlg).Equals(LMConst.FLG.ON) = True Then
                    Exit Sub
                End If

                'メッセージのクリア
                MyBase.ClearMessageAria(frm)

                'チェックオンオフ
                Me._G.SetCheckOnOff()

                '引当個数、引当数量の計算をする
                Me._G.SetHikiSum(eventShubetsu)

            Case LMC040C.EventShubetsu.OPT_KOSU      '出荷単位変更（個数）
                '画面の入力項目の制御（ここで行わないと全項目がロック解除されてしまうため）
                '2014.09.11 修正START
                'Me._G.SetControlsStatus()
                Me._G.SetControlsStatus(Me._AddRecFlg)
                '2014.09.11 修正END
            Case LMC040C.EventShubetsu.OPT_SURYO     '出荷単位変更（数量）
                '画面の入力項目の制御（ここで行わないと全項目がロック解除されてしまうため）
                Me._G.SetControlsStatus()
            Case LMC040C.EventShubetsu.OPT_KOWAKE    '出荷単位変更（小分け）
                '画面の入力項目の制御（ここで行わないと全項目がロック解除されてしまうため）
                Me._G.SetControlsStatus()
            Case LMC040C.EventShubetsu.OPT_SAMPLE    '出荷単位変更（サンプル）
                '画面の入力項目の制御（ここで行わないと全項目がロック解除されてしまうため）
                Me._G.SetControlsStatus()

        End Select

        'デフォルトのメッセージを設定
        Call Me.SetDefMessage(frm)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMC040F, ByVal reFlg As String, ByVal eventShubetsu As LMC040C.EventShubetsu, ByVal prmDs As DataSet)

        '閾値の取得
        Dim dr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0)

        'DataSet設定
        Dim rtDs As DataSet = New LMC040DS()
        If (LMC040C.EventShubetsu.KENSAKU).Equals(eventShubetsu) = True Then
            Me.SetDataSetInData(frm, rtDs)
        ElseIf (LMC040C.EventShubetsu.TANINUSI).Equals(eventShubetsu) = True Then
            Me.SetDataSetInDataTaninushi(frm, rtDs)
        End If

        'SPREAD(表示行)初期化
        frm.sprZaiko.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Nothing
        If (LMC040C.EventShubetsu.KENSAKU).Equals(eventShubetsu) = True Then
            rtnDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), _
                                                          "LMC040BLF", "SelectListData", rtDs, Convert.ToInt32(Convert.ToDecimal(dr.Item("VALUE1"))))
        ElseIf (LMC040C.EventShubetsu.TANINUSI).Equals(eventShubetsu) = True Then
            rtnDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), _
                                                          "LMC040BLF", "SelectListDataTANINUSI", rtDs, Convert.ToInt32(Convert.ToDecimal(dr.Item("VALUE1"))))
        End If


        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then
            Me._SelectZaiko = rtnDs.Tables(LMC040C.TABLE_NM_OUTZAI)
            Me.SuccessSelect(frm, rtnDs, reFlg, prmDs, eventShubetsu)
            If (LMC040C.EventShubetsu.TANINUSI).Equals(eventShubetsu) = True Then
                Me._G.SetInitFormTaninusi(frm, Me._SelectZaiko)
                'START YANAI 要望番号510
                '個数・数量の計算(Me._G.SetInitFormTaninusiにて初期化してしまっているため、ここで再計算)
                Dim calsumFlg As Boolean = Me._G.SetCalSum(LMC040C.EventShubetsu.KENSAKU)
                'END YANAI 要望番号510
            End If
        Else
            MyBase.ShowMessage(frm, "G001")
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

    End Sub

    ''' <summary>
    ''' 検索処理（自動引当）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectDataAutoHiki(ByVal frm As LMC040F, ByVal prmDs As DataSet) As DataSet

        '閾値の取得
        Dim dr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectDataAutoHiki")

        '==========================
        'WSAクラス呼出
        '==========================
        'START YANAI メモ②No.27
        'Dim rtnDs As DataSet = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), _
        '                                              "LMC040BLF", "SelectListData", prmDs, Convert.ToInt32(Convert.ToDecimal(dr.Item("VALUE1"))))
        Dim rtnDs As DataSet = Nothing
        If ("01").Equals(prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("TANINUSI_FLG").ToString()) = False Then

            'START KIM 要望番号1479 一括引当時、引当画面の速度改善
            'rtnDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), _
            '                                  "LMC040BLF", "SelectListData", prmDs, Convert.ToInt32(Convert.ToDecimal(dr.Item("VALUE1"))))
            If frm Is Nothing Then
                rtnDs = Me.CallWSAActionWithoutForm("LMC040BLF", "SelectListData", prmDs, Convert.ToInt32(Convert.ToDecimal(dr.Item("VALUE1"))))
            Else
                rtnDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), _
                                                              "LMC040BLF", "SelectListData", prmDs, Convert.ToInt32(Convert.ToDecimal(dr.Item("VALUE1"))))
            End If
            'END KIM 要望番号1479 一括引当時、引当画面の速度改善
        Else

            'START KIM 要望番号1479 一括引当時、引当画面の速度改善
            'rtnDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), _
            '                                  "LMC040BLF", "SelectListDataTANINUSI", prmDs, Convert.ToInt32(Convert.ToDecimal(dr.Item("VALUE1"))))
            If frm Is Nothing Then
                rtnDs = Me.CallWSAActionWithoutForm("LMC040BLF", "SelectListDataTANINUSI", prmDs, Convert.ToInt32(Convert.ToDecimal(dr.Item("VALUE1"))))
            Else
                rtnDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), _
                                                              "LMC040BLF", "SelectListDataTANINUSI", prmDs, Convert.ToInt32(Convert.ToDecimal(dr.Item("VALUE1"))))
            End If
            'END KIM 要望番号1479 一括引当時、引当画面の速度改善
        End If
        'END YANAI メモ②No.27

        Dim hikiDs As DataSet = New DataSet()

        '検索成功時、引当計算処理を行う
        If rtnDs Is Nothing = False Then

            '自動引当時のチェック
            'START YANAI 20110914 一括引当対応
            'If Me._V.IsAutoCheck(rtnDs) = True Then
            If Me._V.IsAutoCheck(rtnDs, prmDs) = True Then
                'END YANAI 20110914 一括引当対応
                'チェックがOKの場合はセット（遷移元が出荷編集、在庫振替それぞれの場合で、全量の意味合いが異なるので、ここで分岐）
                If (LMC040C.PGID_LMC020).Equals(MyBase.RootPGID()) = True Then
                    Select Case prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("SORT_FLG").ToString()
                        Case "11", "12"
                            '自動倉庫引当（汎用）、日医工（自動倉庫）
                            hikiDs = SetHikiateLMC020_Palette(frm, rtnDs, prmDs)
                        Case Else
                            hikiDs = SetHikiateLMC020(frm, rtnDs, prmDs)
                    End Select

                ElseIf (LMC040C.PGID_LMD010).Equals(MyBase.RootPGID()) = True Then
                    hikiDs = Me.SetHikiateLMD010(frm, rtnDs, prmDs)

                ElseIf (LMC040C.PGID_LMC010).Equals(MyBase.RootPGID()) = True Then
                    Select Case prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("SORT_FLG").ToString()
                        Case "11", "12"
                            '自動倉庫引当（汎用）、日医工（自動倉庫）
                            hikiDs = Me.SetHikiateLMC010_Palette(frm, rtnDs, prmDs)
                        Case Else
                            hikiDs = Me.SetHikiateLMC010(frm, rtnDs, prmDs)
                    End Select
                    If hikiDs Is Nothing = True Then

                        If Me._lnzErrFlg = LMConst.FLG.ON Then
                            MyBase.SetMessage("E536", New String() {})
                        ElseIf Me._lnzErrFlg = "2" Then
                            MyBase.SetMessage("E537", New String() {})
                        Else
                            MyBase.SetMessage("E192")
                        End If

                        'START YANAI 要望番号1200 自動引当・一括引当変更
                    ElseIf hikiDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT).Rows.Count = 0 Then
                        MyBase.SetMessage("E192")
                        'END YANAI 要望番号1200 自動引当・一括引当変更
                    End If
                Else
                    hikiDs = SetHikiateLMC020(frm, rtnDs, prmDs)
                End If

            Else
                hikiDs = Nothing
                'START YANAI メモ②No.15,16,17
                If (LMC040C.PGID_LMC010).Equals(MyBase.RootPGID()) = True Then
                    MyBase.SetMessage("E192")
                End If
                'END YANAI メモ②No.15,16,17
            End If

        Else
            hikiDs = Nothing
            'START YANAI EDIメモNo.69
            If (LMC040C.PGID_LMC010).Equals(MyBase.RootPGID()) = True Then
                MyBase.SetMessage("E192")
            End If
            'END YANAI EDIメモNo.69
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectDataAutoHiki")

        Return hikiDs

    End Function

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMC040F, ByVal rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMControlC.LMC040C_TABLE_NM_IN).NewRow()

        '検索条件　入力部（単項目）
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("WH_CD") = frm.cmbSoko.SelectedValue
        dr("CUST_CD_L") = frm.lblCustCD_L.TextValue
        dr("CUST_CD_M") = frm.lblCustCD_M.TextValue
        dr("GOODS_CD_NRS") = frm.lblGoodsNRS.TextValue
        dr("GOODS_CD_CUST") = frm.lblGoodsCD.TextValue
        dr("SERIAL_NO") = frm.txtSerialNO.TextValue
        dr("RSV_NO") = frm.txtRsvNO.TextValue

        'START KIM 要望番号1315 2012/11/21
        'dr("LOT_NO") = frm.txtLotNO.TextValue
        'dr("LOT_NO") = frm.txtLotNO.TextValue.Replace("%", "[%]").Replace("_", "[_]").Replace("[", "[[]")
        dr("LOT_NO") = frm.txtLotNO.TextValue.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]")   '要望番号:1823（ロットＮｏの検索条件に%を含んだ場合、置換される値がおかしい）対応　 2013/02/05 本明
        'END   KIM 要望番号1315 2012/11/21

        dr("IRIME") = frm.numIrime.TextValue
        'START YANAI 要望番号412
        dr("GOODS_NM") = frm.lblGoodsNM.TextValue
        'END YANAI 要望番号412

        '検索条件　入力部（スプレッド）
        With frm.sprZaiko.ActiveSheet
            dr("TOU_NO") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.TOU_NO.ColNo))
            dr("SITU_NO") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.SHITSU_NO.ColNo))
            dr("ZONE_CD") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.ZONE_CD.ColNo))
            dr("LOCA") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.LOCA.ColNo))
            dr("GOODS_COND_KB_1") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.NAKAMI.ColNo))
            dr("GOODS_COND_KB_2") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.GAIKAN.ColNo))
            dr("GOODS_COND_KB_3") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.CUST_STATUS.ColNo))
            dr("REMARK") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.REMARK.ColNo))
            dr("OFB_KB") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.OFB_KBN.ColNo))
            dr("SPD_KB") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.SPD_KBN_S.ColNo))
            dr("BYK_KEEP_GOODS_CD") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.KEEP_GOODS_NM.ColNo))
            dr("REMARK_OUT") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.REMARK_OUT.ColNo))
            dr("TAX_KB") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.TAX_KB.ColNo))
            dr("HIKIATE_ALERT_YN") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.HIKIATE_ALERT_NM.ColNo))
            dr("DEST_NM") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.DEST_NM.ColNo))
            dr("ZAI_REC_NO") = Me._LMCconG.GetCellValue(.Cells(0, LMC040G.sprZaiko.ZAI_REC_NO.ColNo))
        End With
        dr("HIKIATE_FLG") = "00"
        'START YANAI No.4
        '要望番号:1592 terakawa 2012.11.16 Start
        'dr("SORT_FLG") = "00"
        Dim sortFlg As String = Me._PrmDs.Tables("LMC040IN").Rows(0).Item("SORT_FLG").ToString()
        If String.IsNullOrEmpty(sortFlg) = False Then
            dr("SORT_FLG") = sortFlg
        Else
            dr("SORT_FLG") = "00"
        End If

        '要望番号:1592 terakawa 2012.11.16 End
        'END YANAI No.4

        'START YANAI 20110914 一括引当対応
        dr("OUTKA_PLAN_DATE") = frm.txtOutkaPlanDate.TextValue
        'END YANAI 20110914 一括引当対応

        'START YANAI 要望番号547
        dr("PGID") = MyBase.RootPGID()
        'END YANAI 要望番号547

        '検索条件をデータセットに設定
        rtDs.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(他荷主時の検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInDataTaninushi(ByVal frm As LMC040F, ByVal rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMControlC.LMC040C_TABLE_NM_IN).NewRow()

        '検索条件　入力部（単項目）
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue

        'Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.FURI_GOODS).Select(String.Concat("CD_NRS = '", frm.lblGoodsNRS.TextValue, "'"))
        'dr("GOODS_CD_NRS") = drs(0).Item("CD_NRS_TO").ToString
        dr("GOODS_CD_NRS") = frm.lblGoodsNRS.TextValue

        dr("HIKIATE_FLG") = "00"
        'START YANAI No.4
        dr("SORT_FLG") = "00"
        'END YANAI No.4
        'START YANAI 要望番号412
        dr("GOODS_NM") = frm.lblGoodsNM.TextValue
        'END YANAI 要望番号412

        'START KIM 要望番号1315 2012/11/21
        ''START YANAI 要望番号554
        ''ロット番号
        'dr("LOT_NO") = frm.txtLotNO.TextValue
        ''END YANAI 要望番号554
        'dr("LOT_NO") = frm.txtLotNO.TextValue.Replace("%", "[%]").Replace("_", "[_]").Replace("[", "[[]")
        dr("LOT_NO") = frm.txtLotNO.TextValue.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]")   '要望番号:1823（ロットＮｏの検索条件に%を含んだ場合、置換される値がおかしい）対応　 2013/02/05 本明
        'END   KIM 要望番号1315 2012/11/21

        '検索条件をデータセットに設定
        rtDs.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理（画面別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMC040F, ByVal ds As DataSet, ByVal reFlg As String, ByVal prmDs As DataSet, ByVal eventShubetsu As LMC040C.EventShubetsu)

        Dim dt As DataTable = ds.Tables(LMC040C.TABLE_NM_OUTZAI)

        '画面解除
        MyBase.UnLockedControls(frm)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Me._G.InitSpread()

        '取得データをSPREADに表示
        Me._G.SetSpread(frm, dt, prmDs)

        'START YANAI 要望番号389
        'SPREADの値をチェックし、他荷主ボタン押下可・不可の判定をする
        If Me._G.chkTaninusi(frm) = True Then
            Me._TaninusiFlg = "01"
            'ファンクションキーの設定
            'START YANAI 要望番号507
            'Me._G.SetFunctionKey(Me._TaninusiFlg)
            Me._G.SetFunctionKey(Me._TaninusiFlg, Me._OutkaSCnt)
            'END YANAI 要望番号507
        End If
        'END YANAI 要望番号389

        Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyo.SelectedValue, _
                                             "' AND CUST_CD = '", frm.lblCustCD_L.TextValue, _
                                             "' AND SUB_KB = '80'"))

        If custDetailsDr.Length > 0 AndAlso String.IsNullOrEmpty(frm.lblGoodsNM.TextValue) = True Then
        Else

            '個数・数量の計算
            Dim calsumFlg As Boolean = Me._G.SetCalSum(LMC040C.EventShubetsu.KENSAKU)

        End If

        'START YANAI 要望番号389
        'Me._CntSelect = dt.Rows.Count.ToString()
        Me._CntSelect = Convert.ToString(frm.sprZaiko.ActiveSheet.Rows.Count - 1)
        'END YANAI 要望番号389

        'メッセージエリアの設定
        If LMC040C.NEW_MODE.Equals(reFlg) = True Then
            'START YANAI 要望番号389
            'If dt.Rows.Count = 0 Then
            If ("0").Equals(Me._CntSelect) = True Then
                'END YANAI 要望番号389
                MyBase.ShowMessage(frm, "G001")
            Else
                MyBase.ShowMessage(frm, "G016", New String() {_CntSelect})
            End If
        End If

    End Sub

    ''' <summary>
    ''' 一括引当（遷移元が出荷検索の場合）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetHikiateLMC010(ByVal frm As LMC040F, ByVal ds As DataSet, ByVal prmDs As DataSet) As DataSet

        'START YANAI 要望番号1239 一括引当時にエラー
        ''行数設定
        'Dim lngcnt As Integer = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Count - 1
        'END YANAI 要望番号1239 一括引当時にエラー

        'START KIM 要望番号1479 一括引当時、引当画面の速度改善

        '出荷個数・出荷数量

        'Dim syukkakosu As Integer = Convert.ToInt32(frm.numHikiZanCnt.Value)
        'Dim syukkasuryo As Decimal = Convert.ToDecimal(frm.numHikiZanAmt.Value)

        'フォームを参照しないので、パラメータから直接値を取得する
        Dim syukkakosu As Decimal = Me._V._numHikiZanCnt
#If True Then  'UPD 2018/12/26 ITW EDIテストでエラー対応
        syukkakosu = Convert.ToInt32(syukkakosu)
#End If
        Dim syukkasuryo As Decimal = Me._V._numHikiZanAmt

        'END KIM 要望番号1479 一括引当時、引当画面の速度改善

        Dim rtDs As DataSet = New LMC040DS()
        Dim dt As DataTable = rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT)
        Dim indt As DataTable = prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN)
        Dim outDr As DataRow = dt.NewRow()
        'START YANAI 20110914 一括引当対応
        Dim indt2 As DataTable = prmDs.Tables(LMC040C.TABLE_NM_IN2)

        If indt2 Is Nothing = False Then
            Dim lngcnt2 As Integer = indt2.Rows.Count - 1
            Dim zaiDr() As DataRow = Nothing
            For i As Integer = 0 To lngcnt2
                zaiDr = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Select(String.Concat("ZAI_REC_NO = '", indt2.Rows(i).Item("ZAI_REC_NO").ToString(), "'"))
                If 0 < zaiDr.Length Then
                    zaiDr(0).Item("ALCTD_NB") = indt2.Rows(i).Item("ALCTD_NB").ToString()
                    zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB").ToString()) - _
                                                                     Convert.ToDecimal(indt2.Rows(i).Item("ALLOC_CAN_NB").ToString()))
                    zaiDr(0).Item("ALCTD_QT") = indt2.Rows(i).Item("ALCTD_QT").ToString()
                    zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT").ToString()) - _
                                                                     Convert.ToDecimal(indt2.Rows(i).Item("ALLOC_CAN_QT").ToString()))
                    If ("0").Equals(zaiDr(0).Item("ALLOC_CAN_NB").ToString()) = True OrElse ("0").Equals(zaiDr(0).Item("ALLOC_CAN_QT").ToString()) = True Then
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Remove(zaiDr(0))
                    End If
                End If
            Next
        End If
        'END YANAI 20110914 一括引当対応

        Dim ofbFlg As Boolean = False

        'START YANAI 要望番号1239 一括引当時にエラー
        '行数設定
        Dim lngcnt As Integer = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Count - 1
        'END YANAI 要望番号1239 一括引当時にエラー

        '値設定
        For i As Integer = 0 To lngcnt

            If ("02").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OFB_KB")) = True Then
                '簿外品の時は引当しない
#If False Then   'UPD 2019/10/16 007816【LMS】千葉物流センターテルモ出荷止め、簿外品が絡むと一括引当が出来ない
                ofbFlg = True
                Exit For

#Else
                '千葉　テルモ場合はスキップ,その他は現行まま自動引当を行わない(sys真田)
                If ("10").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("NRS_BR_CD")) = True AndAlso _
                   ("00409").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_L")) = True AndAlso _
                   ("00").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_M")) = True Then

                    Continue For

                Else
                    ofbFlg = True
                    Exit For

                End If

#End If
            End If
            '2013.02.13 要望番号1824 START
            '千葉　ロンザの場合、
            If ("10").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("NRS_BR_CD")) = True AndAlso _
               ("00182").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_L")) = True AndAlso _
               ("00").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_M")) = True Then

                '①割当優先区分が"リザーブ"の場合は引当しない
                If ("20").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_PRIORITY")) = True Then
                    Me._lnzErrFlg = LMConst.FLG.ON
                    Exit For
                End If

                '②商品明細マスタが存在する場合
                If ds.Tables(LMC040C.TABLE_NM_M_GOODS_DETAILS).Rows.Count > 0 Then

                    '賞味期限 < 有効期限日数 + 出荷日　の場合は引当しない
                    If Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_M_GOODS_DETAILS).Rows(0).Item("SET_NAIYO")) > 0 Then

                        Dim outkaPlanDate As String = ds.Tables(LMC040C.TABLE_NM_M_GOODS_DETAILS).Rows(0).Item("OUTKA_PLAN_DATE").ToString()
                        Dim eDate As String = Convert.ToString(DateAdd("d", Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_M_GOODS_DETAILS).Rows(0).Item("SET_NAIYO")), _
                                                         String.Concat(Left(outkaPlanDate, 4), "/", Mid(outkaPlanDate, 5, 2), "/", Mid(outkaPlanDate, 7, 2))))

                        eDate = String.Concat(Left(eDate, 4), Mid(eDate, 6, 2), Mid(eDate, 9, 2))
                        If Convert.ToString(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("LT_DATE")) < eDate Then
                            Me._lnzErrFlg = "2"
                            Exit For
                        End If

                    End If

                End If

            End If
            '2013.02.13 要望番号1824 END

            'START YANAI 要望番号1200 自動引当・一括引当変更
            If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_1").ToString) = False Then
                '商品状態区分(中身)が設定されている場合は、自動引当対象外
                Continue For
            End If

            If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_2").ToString) = False Then
                '商品状態区分(外観)が設定されている場合は、自動引当対象外
                Continue For
            End If
#If True Then   'ADD 2020/05/25 007999 
            Dim JJ_FLG As String = indt.Rows(0)("JJ_FLG").ToString()
            Dim sGOODS_COND_KB_3 As String = indt.Rows(0)("JJ_HIKIATE").ToString()
            If JJ_FLG.Equals(LMConst.FLG.ON) Then
                'ジョンソン&ジョンソンs専用
                If (sGOODS_COND_KB_3).Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_3").ToString) = False Then
                    '状態 荷主が設定されている値以外は、自動引当・一括引当対象外
                    Continue For
                End If
            End If
#End If
            If ("1.000").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SPD_KB_FLG").ToString) = False Then
                '商品状態区分(外観)が設定されている場合は、自動引当対象外
                Continue For
            End If
            'END YANAI 要望番号1200 自動引当・一括引当変更

            outDr("ZAI_REC_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ZAI_REC_NO")
            outDr("TOU_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("TOU_NO")
            outDr("SITU_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SITU_NO")
            outDr("ZONE_CD") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ZONE_CD")
            outDr("LOCA") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("LOCA")
            outDr("LOT_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("LOT_NO")
            outDr("INKA_NO_L") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_NO_L")
            outDr("INKA_NO_M") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_NO_M")
            outDr("INKA_NO_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_NO_S")
            outDr("ALLOC_PRIORITY") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_PRIORITY")
            outDr("RSV_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("RSV_NO")
            outDr("SERIAL_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SERIAL_NO")
            outDr("HOKAN_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("HOKAN_YN")
            outDr("TAX_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("TAX_KB")
            outDr("GOODS_COND_KB_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_1")
            outDr("GOODS_COND_KB_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_2")
            outDr("GOODS_COND_KB_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_3")
            outDr("OFB_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OFB_KB")
            outDr("SPD_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SPD_KB")
            outDr("REMARK_OUT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("REMARK_OUT")
            outDr("PORA_ZAI_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("PORA_ZAI_NB")

            outDr("IRIME") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("IRIME")
            outDr("PORA_ZAI_QT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("PORA_ZAI_QT")
            outDr("INKO_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKO_DATE")
            outDr("INKO_PLAN_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKO_PLAN_DATE")
            outDr("ZERO_FLAG") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ZERO_FLAG")
            outDr("LT_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("LT_DATE")
            outDr("GOODS_CRT_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_CRT_DATE")
            outDr("DEST_CD_P") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("DEST_CD_P")
            outDr("REMARK") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("REMARK")
            outDr("SMPL_FLAG") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SMPL_FLAG")
            outDr("GOODS_COND_NM_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_NM_1")
            outDr("GOODS_COND_NM_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_NM_2")
            outDr("GOODS_COND_NM_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_NM_3")
            outDr("ALLOC_PRIORITY_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_PRIORITY_NM")
            outDr("OFB_KB_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OFB_KB_NM")
            outDr("SPD_KB_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SPD_KB_NM")
            outDr("GOODS_CD_NRS_FROM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_CD_NRS_FROM")
            outDr("KONSU") = "-1"
            outDr("HASU") = "-1"
            outDr("SURYO") = "-1"
            outDr("ALCTD_KB") = "-1"
            'outDr("OUTKA_NO_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_NO_S")
            outDr("BUYER_ORD_NO_DTL") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("BUYER_ORD_NO_DTL")
            outDr("SERIAL_NO_L") = "-1"
            outDr("RSV_NO_L") = "-1"
            outDr("LOT_NO_L") = "-1"
            outDr("IRIME_L") = "-1"

            '引当個数を求める
            If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB").ToString()) = False Then
                If syukkakosu < Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB").ToString()) Then
                    '出荷個数 < 引当可能個数 の時
                    outDr("ALCTD_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB"))
                    outDr("HIKI_KOSU") = syukkakosu
#If False Then  'UPD 2018/12/26 ITW EDIテストでエラー対応
                    outDr("ALCTD_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB")) + syukkakosu

#Else
                    outDr("ALCTD_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB")) + Convert.ToInt32(syukkakosu)

#End If
                    syukkakosu = 0
                Else
                    '引当可能個数 < 出荷個数 の時
                    outDr("ALCTD_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB"))
                    outDr("HIKI_KOSU") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB").ToString())
                    outDr("ALCTD_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB")) + _
                                        Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB").ToString())
                    syukkakosu = syukkakosu - Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB").ToString())
                End If
            End If

            '引当数量を求める
            If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT").ToString()) = False Then
                If syukkasuryo < Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT").ToString()) Then
                    '出荷個数 < 引当可能個数 の時
                    outDr("ALCTD_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_QT"))
                    outDr("HIKI_SURYO") = syukkasuryo
                    outDr("ALCTD_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_QT")) + _
                                        syukkasuryo
                    syukkasuryo = 0
                Else
                    '引当可能個数 < 出荷個数 の時
                    outDr("ALCTD_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_QT"))
                    outDr("HIKI_SURYO") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT").ToString())
                    outDr("ALCTD_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_QT")) + _
                                        Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT").ToString())
                    syukkasuryo = syukkasuryo - Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT").ToString())
                End If
            End If

            If ("04").Equals(indt.Rows(0)("ALCTD_KB").ToString()) = True Then
                'サンプルの時
                outDr("ALCTD_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB")
                outDr("ALCTD_QT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_QT")
                outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB"))
                outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT"))
                outDr("ALLOC_CAN_NB_HOZON") = "0"
                outDr("ALLOC_CAN_QT_HOZON") = "0"
                syukkakosu = 0
                syukkasuryo = 0
            ElseIf ("03").Equals(indt.Rows(0)("ALCTD_KB").ToString()) = True Then
                '小分けの時
                outDr("ALLOC_CAN_NB") = "1"
                outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("ALCTD_QT").ToString)
                outDr("ALLOC_CAN_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB"))
                outDr("ALLOC_CAN_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT"))
            Else
                outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB")) - Convert.ToInt32(outDr("ALCTD_NB")) + Convert.ToInt32(outDr("ALCTD_NB_HOZON"))
                outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("ALCTD_QT")) + Convert.ToDecimal(outDr("ALCTD_QT_HOZON"))
                outDr("ALLOC_CAN_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB"))
                outDr("ALLOC_CAN_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT"))
            End If

            outDr("ALCTD_KOSU") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB")
            outDr("ALCTD_SURYO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT")

            outDr("GOODS_CD_CUST") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_CD_CUST")
            outDr("NM_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_NM_1")
            outDr("OUTKA_ATT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_ATT")
            outDr("SEARCH_KEY_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SEARCH_KEY_1")
            outDr("UNSO_ONDO_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("UNSO_ONDO_KB")
            outDr("PKG_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("PKG_UT")
            outDr("STD_IRIME_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("STD_IRIME_NB")
            outDr("STD_WT_KGS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("STD_WT_KGS")
            outDr("TARE_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("TARE_YN")
            outDr("HIKIATE_ALERT_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("HIKIATE_ALERT_YN")
            outDr("STD_IRIME_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("STD_IRIME_UT")
            outDr("PKG_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("PKG_NB")
            outDr("NB_UT_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("NB_UT_NM")
            outDr("IRIME_UT_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("IRIME_UT_NM")
            outDr("GOODS_CD_NRS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_CD_NRS")
            'START ADD 2013/09/10 KOBAYASHI WIT対応
            outDr("GOODS_KANRI_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_KANRI_NO")
            'END   ADD 2013/09/10 KOBAYASHI WIT対応
            outDr("CUST_CD_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_S")
            outDr("CUST_CD_SS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_SS")
            outDr("IDO_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("IDO_DATE")
            outDr("INKA_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_DATE")
            outDr("HOKAN_STR_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("HOKAN_STR_DATE")
            outDr("COA_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("COA_YN")
            outDr("OUTKA_KAKO_SAGYO_KB_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_1")
            outDr("OUTKA_KAKO_SAGYO_KB_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_2")
            outDr("OUTKA_KAKO_SAGYO_KB_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_3")
            outDr("OUTKA_KAKO_SAGYO_KB_4") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_4")
            outDr("OUTKA_KAKO_SAGYO_KB_5") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_5")
            outDr("SIZE_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SIZE_KB")
            outDr("NB_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("NB_UT")
            'START YANAI 要望番号499
            outDr("CUST_CD_L_GOODS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_L_GOODS")
            outDr("CUST_CD_M_GOODS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_M_GOODS")
            'END YANAI 要望番号499
            'START YANAI 要望番号780
            outDr("INKA_DATE_KANRI_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_DATE_KANRI_KB")
            'END YANAI 要望番号780

            outDr("SYS_UPD_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SYS_UPD_DATE")
            outDr("SYS_UPD_TIME") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SYS_UPD_TIME")

            outDr("SHOBO_CD") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SHOBO_CD")
            outDr("SHOBO_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SHOBO_NM")

            '設定値をデータセットに設定
            rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT).Rows.Add(outDr)
            If syukkakosu = 0 AndAlso syukkasuryo = 0 Then
                '出荷個数・出荷数量分、引当終わったら、処理を抜ける
                Exit For
            End If

            outDr = dt.NewRow()

        Next

        If ofbFlg = True Then
            'ofbFlg = Trueの場合は、実質引当対象がなかった場合
            Return Nothing

            '2013.02.13 要望番号1824 START
        ElseIf Me._lnzErrFlg <> LMConst.FLG.OFF Then
            Return Nothing
            '2013.02.13 要望番号1824 END
        Else
            Return rtDs
        End If

    End Function

    ''' <summary>
    ''' 一括引当（遷移元が出荷検索の場合／自動倉庫パレット対応版）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetHikiateLMC010_Palette(ByVal frm As LMC040F, ByVal ds As DataSet, ByVal prmDs As DataSet) As DataSet

        Dim syukkakosu As Decimal = Me._V._numHikiZanCnt
        syukkakosu = Convert.ToInt32(syukkakosu)
        Dim syukkasuryo As Decimal = Me._V._numHikiZanAmt
        Dim rtDs As DataSet = New LMC040DS()
        Dim dt As DataTable = rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT)
        Dim indt As DataTable = prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN)
        Dim outDr As DataRow = dt.NewRow()
        Dim indt2 As DataTable = prmDs.Tables(LMC040C.TABLE_NM_IN2)

        If indt2 IsNot Nothing Then
            Dim lngcnt2 As Integer = indt2.Rows.Count - 1
            Dim zaiDr() As DataRow = Nothing
            For i As Integer = 0 To lngcnt2
                zaiDr = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Select(String.Concat("ZAI_REC_NO = '", indt2.Rows(i).Item("ZAI_REC_NO").ToString(), "'"))
                If 0 < zaiDr.Length Then
                    zaiDr(0).Item("ALCTD_NB") = indt2.Rows(i).Item("ALCTD_NB").ToString()
                    zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB").ToString()) -
                                                                     Convert.ToDecimal(indt2.Rows(i).Item("ALLOC_CAN_NB").ToString()))
                    zaiDr(0).Item("ALCTD_QT") = indt2.Rows(i).Item("ALCTD_QT").ToString()
                    zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT").ToString()) -
                                                                     Convert.ToDecimal(indt2.Rows(i).Item("ALLOC_CAN_QT").ToString()))
                    If ("0").Equals(zaiDr(0).Item("ALLOC_CAN_NB").ToString()) = True OrElse ("0").Equals(zaiDr(0).Item("ALLOC_CAN_QT").ToString()) = True Then
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Remove(zaiDr(0))
                    End If
                End If
            Next
        End If

        '優先パレット情報のループ
        For p As Integer = 0 To ds.Tables(LMC040C.TABLE_NM_OUTZAI_PALETTE).Rows.Count - 1

            '棟,室,ZONE,LOCAを合わせてパレット名とする
            Dim pPalette As String = String.Concat(
                    ds.Tables(LMC040C.TABLE_NM_OUTZAI_PALETTE).Rows(p).Item("TOU_NO").ToString(),
                    "-",
                    ds.Tables(LMC040C.TABLE_NM_OUTZAI_PALETTE).Rows(p).Item("SITU_NO").ToString(),
                    "-",
                    ds.Tables(LMC040C.TABLE_NM_OUTZAI_PALETTE).Rows(p).Item("ZONE_CD").ToString(),
                    "-",
                    ds.Tables(LMC040C.TABLE_NM_OUTZAI_PALETTE).Rows(p).Item("LOCA").ToString())

            '在庫情報のループ
            For z As Integer = 0 To ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Count - 1

                '棟,室,ZONE,LOCAを合わせてパレット名とする
                Dim zPalette As String = String.Concat(
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("TOU_NO").ToString(),
                        "-",
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SITU_NO").ToString(),
                        "-",
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ZONE_CD").ToString(),
                        "-",
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("LOCA").ToString())

                'パレット名が異なればスキップ
                If Not zPalette.Equals(pPalette) Then
                    Continue For
                End If

                '簿外品の場合
                If ("02").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OFB_KB")) Then
                    '土気 テルモ(変動保管料なし：00409）の場合はスキップ,その他は自動引当を行わない
                    If ("15").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("NRS_BR_CD")) AndAlso
                            ("00409").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_L")) AndAlso
                            ("00").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_M")) Then
                        Continue For
                        '土気 テルモ(変動保管料あり：00001）
                    ElseIf ("15").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("NRS_BR_CD")) AndAlso
                            ("00001").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_L")) AndAlso
                            ("00").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_M")) Then
                        Continue For
                    Else
                        Return Nothing
                    End If
                End If

                '千葉 ロンザの場合
                If ("10").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("NRS_BR_CD")) AndAlso
                        ("00182").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_L")) AndAlso
                        ("00").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_M")) Then
                    '①割当優先区分が"リザーブ"の場合は引当しない
                    If ("20").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_PRIORITY")) = True Then
                        Me._lnzErrFlg = LMConst.FLG.ON
                        Return Nothing
                    End If

                    '②商品明細マスタが存在する場合
                    If ds.Tables(LMC040C.TABLE_NM_M_GOODS_DETAILS).Rows.Count > 0 Then
                        '賞味期限 < 有効期限日数 + 出荷日　の場合は引当しない
                        If Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_M_GOODS_DETAILS).Rows(0).Item("SET_NAIYO")) > 0 Then
                            Dim outkaPlanDate As String = ds.Tables(LMC040C.TABLE_NM_M_GOODS_DETAILS).Rows(0).Item("OUTKA_PLAN_DATE").ToString()
                            Dim eDate As String = Convert.ToString(DateAdd("d", Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_M_GOODS_DETAILS).Rows(0).Item("SET_NAIYO")),
                                                         String.Concat(Left(outkaPlanDate, 4), "/", Mid(outkaPlanDate, 5, 2), "/", Mid(outkaPlanDate, 7, 2))))

                            eDate = String.Concat(Left(eDate, 4), Mid(eDate, 6, 2), Mid(eDate, 9, 2))
                            If Convert.ToString(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("LT_DATE")) < eDate Then
                                Me._lnzErrFlg = "2"
                                Return Nothing
                            End If
                        End If
                    End If
                End If

                '商品状態区分(中身)が設定されている場合は、自動引当対象外
                If Not String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_KB_1").ToString) Then
                    Continue For
                End If

                '商品状態区分(外観)が設定されている場合は、自動引当対象外
                If Not String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_KB_2").ToString) Then
                    Continue For
                End If

                'ジョンソン&ジョンソン専用
                '状態 荷主が設定されている値以外は、自動引当・一括引当対象外
                If indt.Rows(0)("JJ_FLG").ToString.Equals(LMConst.FLG.ON) Then
                    '状態 荷主が設定されている値以外は、自動引当・一括引当対象外
                    If Not indt.Rows(0)("JJ_HIKIATE").ToString.Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_KB_3").ToString) Then
                        Continue For
                    End If
                End If

                '商品状態区分(外観)が設定されている場合は、自動引当対象外
                If Not "1.000".Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SPD_KB_FLG").ToString) Then
                    Continue For
                End If

                '引当処理
                outDr("ZAI_REC_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ZAI_REC_NO")
                outDr("TOU_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("TOU_NO")
                outDr("SITU_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SITU_NO")
                outDr("ZONE_CD") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ZONE_CD")
                outDr("LOCA") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("LOCA")
                outDr("LOT_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("LOT_NO")
                outDr("INKA_NO_L") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKA_NO_L")
                outDr("INKA_NO_M") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKA_NO_M")
                outDr("INKA_NO_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKA_NO_S")
                outDr("ALLOC_PRIORITY") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_PRIORITY")
                outDr("RSV_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("RSV_NO")
                outDr("SERIAL_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SERIAL_NO")
                outDr("HOKAN_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("HOKAN_YN")
                outDr("TAX_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("TAX_KB")
                outDr("GOODS_COND_KB_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_KB_1")
                outDr("GOODS_COND_KB_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_KB_2")
                outDr("GOODS_COND_KB_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_KB_3")
                outDr("OFB_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OFB_KB")
                outDr("SPD_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SPD_KB")
                outDr("REMARK_OUT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("REMARK_OUT")
                outDr("PORA_ZAI_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("PORA_ZAI_NB")
                outDr("IRIME") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("IRIME")
                outDr("PORA_ZAI_QT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("PORA_ZAI_QT")
                outDr("INKO_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKO_DATE")
                outDr("INKO_PLAN_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKO_PLAN_DATE")
                outDr("ZERO_FLAG") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ZERO_FLAG")
                outDr("LT_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("LT_DATE")
                outDr("GOODS_CRT_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_CRT_DATE")
                outDr("DEST_CD_P") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("DEST_CD_P")
                outDr("REMARK") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("REMARK")
                outDr("SMPL_FLAG") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SMPL_FLAG")
                outDr("GOODS_COND_NM_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_NM_1")
                outDr("GOODS_COND_NM_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_NM_2")
                outDr("GOODS_COND_NM_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_NM_3")
                outDr("ALLOC_PRIORITY_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_PRIORITY_NM")
                outDr("OFB_KB_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OFB_KB_NM")
                outDr("SPD_KB_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SPD_KB_NM")
                outDr("GOODS_CD_NRS_FROM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_CD_NRS_FROM")
                outDr("KONSU") = "-1"
                outDr("HASU") = "-1"
                outDr("SURYO") = "-1"
                outDr("ALCTD_KB") = "-1"
                outDr("BUYER_ORD_NO_DTL") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("BUYER_ORD_NO_DTL")
                outDr("SERIAL_NO_L") = "-1"
                outDr("RSV_NO_L") = "-1"
                outDr("LOT_NO_L") = "-1"
                outDr("IRIME_L") = "-1"

                '引当個数を求める
                If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB").ToString()) = False Then
                    If syukkakosu < Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB").ToString()) Then
                        '出荷個数 < 引当可能個数 の時
                        outDr("ALCTD_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_NB"))
                        outDr("HIKI_KOSU") = syukkakosu
                        outDr("ALCTD_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_NB")) + Convert.ToInt32(syukkakosu)
                        syukkakosu = 0
                    Else
                        '引当可能個数 < 出荷個数 の時
                        outDr("ALCTD_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_NB"))
                        outDr("HIKI_KOSU") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB").ToString())
                        outDr("ALCTD_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_NB")) +
                                Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB").ToString())
                        syukkakosu = syukkakosu - Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB").ToString())
                    End If
                End If

                '引当数量を求める
                If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT").ToString()) = False Then
                    If syukkasuryo < Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT").ToString()) Then
                        '出荷個数 < 引当可能個数 の時
                        outDr("ALCTD_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_QT"))
                        outDr("HIKI_SURYO") = syukkasuryo
                        outDr("ALCTD_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_QT")) + syukkasuryo
                        syukkasuryo = 0
                    Else
                        '引当可能個数 < 出荷個数 の時
                        outDr("ALCTD_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_QT"))
                        outDr("HIKI_SURYO") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT").ToString())
                        outDr("ALCTD_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_QT")) +
                                Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT").ToString())
                        syukkasuryo = syukkasuryo - Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT").ToString())
                    End If
                End If

                If ("04").Equals(indt.Rows(0)("ALCTD_KB").ToString()) = True Then
                    'サンプルの時
                    outDr("ALCTD_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_NB")
                    outDr("ALCTD_QT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_QT")
                    outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB"))
                    outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT"))
                    outDr("ALLOC_CAN_NB_HOZON") = "0"
                    outDr("ALLOC_CAN_QT_HOZON") = "0"
                    syukkakosu = 0
                    syukkasuryo = 0
                ElseIf ("03").Equals(indt.Rows(0)("ALCTD_KB").ToString()) = True Then
                    '小分けの時
                    outDr("ALLOC_CAN_NB") = "1"
                    outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("ALCTD_QT").ToString)
                    outDr("ALLOC_CAN_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB"))
                    outDr("ALLOC_CAN_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT"))
                Else
                    outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB")) - Convert.ToInt32(outDr("ALCTD_NB")) + Convert.ToInt32(outDr("ALCTD_NB_HOZON"))
                    outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("ALCTD_QT")) + Convert.ToDecimal(outDr("ALCTD_QT_HOZON"))
                    outDr("ALLOC_CAN_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB"))
                    outDr("ALLOC_CAN_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT"))
                End If

                outDr("ALCTD_KOSU") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB")
                outDr("ALCTD_SURYO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT")
                outDr("GOODS_CD_CUST") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_CD_CUST")
                outDr("NM_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_NM_1")
                outDr("OUTKA_ATT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OUTKA_ATT")
                outDr("SEARCH_KEY_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SEARCH_KEY_1")
                outDr("UNSO_ONDO_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("UNSO_ONDO_KB")
                outDr("PKG_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("PKG_UT")
                outDr("STD_IRIME_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("STD_IRIME_NB")
                outDr("STD_WT_KGS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("STD_WT_KGS")
                outDr("TARE_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("TARE_YN")
                outDr("HIKIATE_ALERT_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("HIKIATE_ALERT_YN")
                outDr("STD_IRIME_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("STD_IRIME_UT")
                outDr("PKG_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("PKG_NB")
                outDr("NB_UT_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("NB_UT_NM")
                outDr("IRIME_UT_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("IRIME_UT_NM")
                outDr("GOODS_CD_NRS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_CD_NRS")
                outDr("GOODS_KANRI_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_KANRI_NO")
                outDr("CUST_CD_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_S")
                outDr("CUST_CD_SS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_SS")
                outDr("IDO_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("IDO_DATE")
                outDr("INKA_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKA_DATE")
                outDr("HOKAN_STR_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("HOKAN_STR_DATE")
                outDr("COA_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("COA_YN")
                outDr("OUTKA_KAKO_SAGYO_KB_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OUTKA_KAKO_SAGYO_KB_1")
                outDr("OUTKA_KAKO_SAGYO_KB_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OUTKA_KAKO_SAGYO_KB_2")
                outDr("OUTKA_KAKO_SAGYO_KB_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OUTKA_KAKO_SAGYO_KB_3")
                outDr("OUTKA_KAKO_SAGYO_KB_4") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OUTKA_KAKO_SAGYO_KB_4")
                outDr("OUTKA_KAKO_SAGYO_KB_5") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OUTKA_KAKO_SAGYO_KB_5")
                outDr("SIZE_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SIZE_KB")
                outDr("NB_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("NB_UT")
                outDr("CUST_CD_L_GOODS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_L_GOODS")
                outDr("CUST_CD_M_GOODS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_M_GOODS")
                outDr("INKA_DATE_KANRI_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKA_DATE_KANRI_KB")
                outDr("SYS_UPD_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SYS_UPD_DATE")
                outDr("SYS_UPD_TIME") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SYS_UPD_TIME")
                outDr("SHOBO_CD") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SHOBO_CD")
                outDr("SHOBO_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SHOBO_NM")

                '設定値をデータセットに追加
                rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT).Rows.Add(outDr)

                '出荷個数・出荷数量分、引当終わったら処理を抜ける
                If syukkakosu = 0 AndAlso syukkasuryo = 0 Then
                    Return rtDs
                End If

                outDr = dt.NewRow()

            Next z

        Next p

        Return rtDs

    End Function

    ''' <summary>
    ''' 自動引当（遷移元が出荷編集の場合）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetHikiateLMC020(ByVal frm As LMC040F, ByVal ds As DataSet, ByVal prmDs As DataSet) As DataSet

        'START YANAI 要望番号1239 一括引当時にエラー
        ''行数設定
        'Dim lngcnt As Integer = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Count - 1
        'END YANAI 要望番号1239 一括引当時にエラー

        '出荷個数・出荷数量
        Dim syukkakosu As Integer = Convert.ToInt32(frm.numHikiZanCnt.Value)
        Dim syukkasuryo As Decimal = Convert.ToDecimal(frm.numHikiZanAmt.Value)

        Dim rtDs As DataSet = New LMC040DS()
        Dim dt As DataTable = rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT)
        Dim indt As DataTable = prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN)
        Dim outDr As DataRow = dt.NewRow()

        'START YANAI 20110914 一括引当対応
        Dim indt2 As DataTable = prmDs.Tables(LMC040C.TABLE_NM_IN2)

        If indt2 Is Nothing = False Then
            Dim lngcnt2 As Integer = indt2.Rows.Count - 1
            Dim zaiDr() As DataRow = Nothing
            For i As Integer = 0 To lngcnt2
                zaiDr = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Select(String.Concat("ZAI_REC_NO = '", indt2.Rows(i).Item("ZAI_REC_NO").ToString(), "'"))
                If 0 < zaiDr.Length Then
                    zaiDr(0).Item("ALCTD_NB") = indt2.Rows(i).Item("ALCTD_NB").ToString()
                    zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB").ToString()) - _
                                                                     Convert.ToDecimal(indt2.Rows(i).Item("ALLOC_CAN_NB").ToString()))
                    zaiDr(0).Item("ALCTD_QT") = indt2.Rows(i).Item("ALCTD_QT").ToString()
                    zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT").ToString()) - _
                                                                     Convert.ToDecimal(indt2.Rows(i).Item("ALLOC_CAN_QT").ToString()))
                    If ("0").Equals(zaiDr(0).Item("ALLOC_CAN_NB").ToString()) = True OrElse ("0").Equals(zaiDr(0).Item("ALLOC_CAN_QT").ToString()) = True Then
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Remove(zaiDr(0))
                    End If
                End If
            Next
        End If
        'END YANAI 20110914 一括引当対応

        'START YANAI 要望番号1239 一括引当時にエラー
        '行数設定
        Dim lngcnt As Integer = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Count - 1
        'END YANAI 要望番号1239 一括引当時にエラー

        'Start s.kobayashi 要望管理1954
        Dim sumiZaiDr() As DataRow
        'End S.kobayashi要望管理1954

#If True Then ' フィルメニッヒ セミEDI対応  20161003 added inoue

        ' 商品状態の無視実施判定
        Dim isIgnoreGoodsCond As Boolean = Me.IsIgnoreGoodsCond(Me._PrmDs)

#End If
        'ADD 2017/04/24 アクサルタ対応
        Dim IsIgnoreAXALTA As Boolean = Me.IsIgnoreAXALTA(Me._PrmDs)

#If True Then   'ADD 2020/05/25 007999   【LMS】セミEDI_ジョンソンエンドジョンソンJ&J_土気自動倉庫預かり_入荷・出荷新規
        Dim JJ_FLG As String = indt.Rows(0)("JJ_FLG").ToString()
        Dim sGOODS_COND_KB_3 As String = indt.Rows(0)("JJ_HIKIATE").ToString()
#End If

        '値設定
        For i As Integer = 0 To lngcnt

#If False Then ' フィルメニッヒ セミEDI対応  20161003 changed inoue
            'START YANAI 要望番号1200 自動引当・一括引当変更
            If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_1").ToString) = False Then
                '商品状態区分(中身)が設定されている場合は、自動引当対象外
                Continue For
            End If

            If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_2").ToString) = False Then
                '商品状態区分(外観)が設定されている場合は、自動引当対象外
                Continue For
            End If
#Else

            If (isIgnoreGoodsCond = False) Then

                'START YANAI 要望番号1200 自動引当・一括引当変更
                If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_1").ToString) = False Then
                    '商品状態区分(中身)が設定されている場合は、自動引当対象外
                    Continue For
                End If

                If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_2").ToString) = False Then
                    '商品状態区分(外観)が設定されている場合は、自動引当対象外
                    Continue For
                End If

            End If

#End If

            'ADD 2017/04/24 Start
            If (IsIgnoreAXALTA = True) Then

                ' 自動引当
                If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_NM_3").ToString) = False Then
                    'ダメージ品（状態　荷主欄の記載のあるもの）の場合は、自動引当対象外
                    Continue For
                End If

                If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("LOCA").ToString) = True Then
                    'ロケーションが入ってない場合は、自動引当対象外
                    Continue For
                End If

            End If
            'ADD 2017/04/24 End

#If True Then   'ADD 2020/05/25 007999 
            If JJ_FLG.Equals(LMConst.FLG.ON) Then
                'ジョンソン&ジョンソンs専用
                If (sGOODS_COND_KB_3).Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_3").ToString) = False Then
                    '状態 荷主が設定されている値以外は、自動引当・一括引当対象外
                    Continue For
                End If
            End If
#End If
            If ("1.000").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SPD_KB_FLG").ToString) = False Then
                '商品状態区分(外観)が設定されている場合は、自動引当対象外
                Continue For
            End If

            'END YANAI 要望番号1200 自動引当・一括引当変更


            '(2013.03.21)要望番号1229 小分け、サンプル時は入荷完了された商品のみ引当可 -- START --
            If ("03").Equals(indt.Rows(0).Item("ALCTD_KB").ToString()) = True Or _
                 ("04").Equals(indt.Rows(0).Item("ALCTD_KB").ToString()) = True Then
                If Convert.ToDouble(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_STATE_KB")) < 50 Then
                    '次の行へ
                    Continue For
                End If
            End If
            '(2013.03.21)要望番号1229 小分け、サンプル時は入荷完了された商品のみ引当可 --  END --

            outDr("ZAI_REC_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ZAI_REC_NO")
            outDr("TOU_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("TOU_NO")
            outDr("SITU_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SITU_NO")
            outDr("ZONE_CD") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ZONE_CD")
            outDr("LOCA") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("LOCA")
            outDr("LOT_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("LOT_NO")
            outDr("INKA_NO_L") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_NO_L")
            outDr("INKA_NO_M") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_NO_M")
            outDr("INKA_NO_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_NO_S")
            outDr("ALLOC_PRIORITY") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_PRIORITY")
            outDr("RSV_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("RSV_NO")
            outDr("SERIAL_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SERIAL_NO")
            outDr("HOKAN_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("HOKAN_YN")
            outDr("TAX_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("TAX_KB")
            outDr("GOODS_COND_KB_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_1")
            outDr("GOODS_COND_KB_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_2")
            outDr("GOODS_COND_KB_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_3")
            outDr("OFB_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OFB_KB")
            outDr("SPD_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SPD_KB")
            outDr("REMARK_OUT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("REMARK_OUT")
            outDr("PORA_ZAI_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("PORA_ZAI_NB")

            outDr("IRIME") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("IRIME")
            outDr("PORA_ZAI_QT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("PORA_ZAI_QT")
            outDr("INKO_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKO_DATE")
            outDr("INKO_PLAN_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKO_PLAN_DATE")
            outDr("ZERO_FLAG") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ZERO_FLAG")
            outDr("LT_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("LT_DATE")
            outDr("GOODS_CRT_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_CRT_DATE")
            outDr("DEST_CD_P") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("DEST_CD_P")
            outDr("REMARK") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("REMARK")
            outDr("SMPL_FLAG") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SMPL_FLAG")
            outDr("GOODS_COND_NM_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_NM_1")
            outDr("GOODS_COND_NM_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_NM_2")
            outDr("GOODS_COND_NM_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_NM_3")
            outDr("ALLOC_PRIORITY_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_PRIORITY_NM")
            outDr("OFB_KB_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OFB_KB_NM")
            outDr("SPD_KB_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SPD_KB_NM")
            outDr("GOODS_CD_NRS_FROM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_CD_NRS_FROM")
            outDr("KONSU") = "-1"
            outDr("HASU") = "-1"
            outDr("SURYO") = "-1"
            outDr("ALCTD_KB") = "-1"
            'outDr("OUTKA_NO_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_NO_S")
            outDr("BUYER_ORD_NO_DTL") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("BUYER_ORD_NO_DTL")
            outDr("SERIAL_NO_L") = "-1"
            outDr("RSV_NO_L") = "-1"
            outDr("LOT_NO_L") = "-1"
            outDr("IRIME_L") = "-1"

            'Start s.kobayashi 要望管理1954
            '引当可能個数・数量の上書き
            If 0 < ds.Tables(LMC040C.TABLE_NM_ZAI).Rows.Count Then
                sumiZaiDr = ds.Tables(LMC040C.TABLE_NM_ZAI).Select(String.Concat("ZAI_REC_NO = '", ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ZAI_REC_NO").ToString(), "'"))
                If 0 < sumiZaiDr.Length Then
                    '20170623 在庫不整合修正Add
                    If ds.Tables(LMC040C.TABLE_NM_OUT_S).Select(String.Concat("ZAI_REC_NO = '", ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ZAI_REC_NO").ToString(), "'")).Length = 0 Then
                        '20170623 在庫不整合修正End
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB") = sumiZaiDr(0).Item("ALLOC_CAN_NB").ToString
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT") = sumiZaiDr(0).Item("ALLOC_CAN_QT").ToString
                        '引当可能個数が０の場合、抜ける
                        If Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT")) <= 0 Then
                            Continue For
                        End If
                        'Dim dtOutZai As DataTable = ds.Tables(LMC040C.TABLE_NM_OUTZAI)
                        'Dim outDrZai As DataRow = dtOutZai.NewRow()
                        'outDrZai("ALLOC_CAN_NB") = "1"
                        'ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Add(outDrZai)
                    End If
                End If
            End If
            'End S.kobayashi要望管理1954

            '引当個数を求める
            If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB").ToString()) = False Then
                If syukkakosu < Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB").ToString()) Then
                    '出荷個数 < 引当可能個数 の時
                    outDr("ALCTD_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB"))
                    outDr("HIKI_KOSU") = syukkakosu
                    outDr("ALCTD_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB")) + syukkakosu
                    syukkakosu = 0
                Else
                    '引当可能個数 < 出荷個数 の時
                    outDr("ALCTD_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB"))
                    outDr("HIKI_KOSU") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB").ToString())
                    outDr("ALCTD_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB")) + _
                                        Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB").ToString())
                    syukkakosu = syukkakosu - Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB").ToString())
                End If
            End If

            '引当数量を求める
            If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT").ToString()) = False Then
                If syukkasuryo < Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT").ToString()) Then
                    '出荷個数 < 引当可能個数 の時
                    outDr("ALCTD_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_QT"))
                    outDr("HIKI_SURYO") = syukkasuryo
                    outDr("ALCTD_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_QT")) + _
                                        syukkasuryo
                    syukkasuryo = 0
                Else
                    '引当可能個数 < 出荷個数 の時
                    outDr("ALCTD_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_QT"))
                    outDr("HIKI_SURYO") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT").ToString())
                    outDr("ALCTD_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_QT")) + _
                                        Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT").ToString())
                    syukkasuryo = syukkasuryo - Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT").ToString())
                End If
            End If

            If ("04").Equals(indt.Rows(0)("ALCTD_KB").ToString()) = True Then
                'サンプルの時
                'START YANAI 20110906 サンプル対応
                'outDr("ALCTD_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB")
                'outDr("ALCTD_QT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_QT")
                'START END 20110906 サンプル対応
                outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB")) - Convert.ToInt32(outDr("HIKI_KOSU"))
                outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("HIKI_SURYO"))
                outDr("ALLOC_CAN_NB_HOZON") = "0"
                outDr("ALLOC_CAN_QT_HOZON") = "0"
                syukkakosu = 0
                syukkasuryo = 0
            ElseIf ("03").Equals(indt.Rows(0)("ALCTD_KB").ToString()) = True Then
                '小分けの時
                'START YANAI 20110913 小分け対応
                'outDr("ALLOC_CAN_NB") = "1"
                outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB")) - Convert.ToInt32(outDr("ALCTD_NB"))
                'END YANAI 20110913 小分け対応
                outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("ALCTD_QT").ToString)
                outDr("ALLOC_CAN_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB"))
                outDr("ALLOC_CAN_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT"))
            Else
                outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB")) - Convert.ToInt32(outDr("ALCTD_NB")) + Convert.ToInt32(outDr("ALCTD_NB_HOZON"))
                outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("ALCTD_QT")) + Convert.ToDecimal(outDr("ALCTD_QT_HOZON"))
                outDr("ALLOC_CAN_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB"))
                outDr("ALLOC_CAN_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT"))
                '20170623 念のためのチェック
                If Convert.ToInt32(outDr("PORA_ZAI_NB")) - Convert.ToInt32(outDr("ALCTD_NB")) <> Convert.ToInt32(outDr("ALLOC_CAN_NB")) Then
                    outDr("ALLOC_CAN_NB") = Convert.ToInt32(outDr("PORA_ZAI_NB")) - Convert.ToInt32(outDr("ALCTD_NB"))
                End If
                If Convert.ToDecimal(outDr("PORA_ZAI_QT")) - Convert.ToDecimal(outDr("ALCTD_QT")) <> Convert.ToDecimal(outDr("ALLOC_CAN_QT")) Then
                    outDr("ALLOC_CAN_QT") = Convert.ToDecimal(outDr("PORA_ZAI_QT")) - Convert.ToDecimal(outDr("ALCTD_QT"))
                End If
                '20170623 念のためのチェック
            End If

            outDr("ALCTD_KOSU") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB")
            outDr("ALCTD_SURYO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT")

            outDr("GOODS_CD_CUST") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_CD_CUST")
            outDr("NM_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_NM_1")
            outDr("OUTKA_ATT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_ATT")
            outDr("SEARCH_KEY_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SEARCH_KEY_1")
            outDr("UNSO_ONDO_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("UNSO_ONDO_KB")
            outDr("PKG_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("PKG_UT")
            outDr("STD_IRIME_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("STD_IRIME_NB")
            outDr("STD_WT_KGS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("STD_WT_KGS")
            outDr("TARE_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("TARE_YN")
            outDr("HIKIATE_ALERT_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("HIKIATE_ALERT_YN")
            outDr("STD_IRIME_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("STD_IRIME_UT")
            outDr("PKG_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("PKG_NB")
            outDr("NB_UT_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("NB_UT_NM")
            outDr("IRIME_UT_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("IRIME_UT_NM")
            outDr("GOODS_CD_NRS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_CD_NRS")
            'START ADD 2013/09/10 KOBAYASHI WIT対応
            outDr("GOODS_KANRI_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_KANRI_NO")
            'END   ADD 2013/09/10 KOBAYASHI WIT対応
            outDr("CUST_CD_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_S")
            outDr("CUST_CD_SS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_SS")
            outDr("IDO_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("IDO_DATE")
            outDr("INKA_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_DATE")
            outDr("HOKAN_STR_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("HOKAN_STR_DATE")
            outDr("COA_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("COA_YN")
            outDr("OUTKA_KAKO_SAGYO_KB_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_1")
            outDr("OUTKA_KAKO_SAGYO_KB_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_2")
            outDr("OUTKA_KAKO_SAGYO_KB_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_3")
            outDr("OUTKA_KAKO_SAGYO_KB_4") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_4")
            outDr("OUTKA_KAKO_SAGYO_KB_5") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_5")
            outDr("SIZE_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SIZE_KB")
            outDr("NB_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("NB_UT")
            'START YANAI 要望番号499
            outDr("CUST_CD_L_GOODS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_L_GOODS")
            outDr("CUST_CD_M_GOODS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_M_GOODS")
            'END YANAI 要望番号499
            'START YANAI 要望番号780
            outDr("INKA_DATE_KANRI_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_DATE_KANRI_KB")
            'END YANAI 要望番号780
            '(2013.3.21)要望番号1229 -- START --
            '不要
            '(2013.3.12)要望番号1229 -- START --
            'outDr("INKA_STATE_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_STATE_KB")
            '(2013.3.12)要望番号1229 --  END  --
            '(2013.3.21)要望番号1229 --  END  --

            outDr("SYS_UPD_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SYS_UPD_DATE")
            outDr("SYS_UPD_TIME") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SYS_UPD_TIME")

            outDr("SHOBO_CD") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SHOBO_CD")
            outDr("SHOBO_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SHOBO_NM")

            '設定値をデータセットに設定
            rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT).Rows.Add(outDr)
            If syukkakosu = 0 AndAlso syukkasuryo = 0 Then
                '出荷個数・出荷数量分、引当終わったら、処理を抜ける
                Exit For
            End If

            outDr = dt.NewRow()

        Next

        Return rtDs

    End Function

    ''' <summary>
    ''' 自動引当（遷移元が出荷編集の場合／自動倉庫パレット対応版）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetHikiateLMC020_Palette(ByVal frm As LMC040F, ByVal ds As DataSet, ByVal prmDs As DataSet) As DataSet

        Dim syukkakosu As Integer = Convert.ToInt32(frm.numHikiZanCnt.Value)
        Dim syukkasuryo As Decimal = Convert.ToDecimal(frm.numHikiZanAmt.Value)
        Dim rtDs As DataSet = New LMC040DS()
        Dim dt As DataTable = rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT)
        Dim indt As DataTable = prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN)
        Dim outDr As DataRow = dt.NewRow()
        Dim indt2 As DataTable = prmDs.Tables(LMC040C.TABLE_NM_IN2)
        Dim sumiZaiDr() As DataRow

        If indt2 IsNot Nothing Then
            Dim lngcnt2 As Integer = indt2.Rows.Count - 1
            Dim zaiDr() As DataRow = Nothing
            For i As Integer = 0 To lngcnt2
                zaiDr = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Select(String.Concat("ZAI_REC_NO = '", indt2.Rows(i).Item("ZAI_REC_NO").ToString(), "'"))
                If 0 < zaiDr.Length Then
                    zaiDr(0).Item("ALCTD_NB") = indt2.Rows(i).Item("ALCTD_NB").ToString()
                    zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB").ToString()) -
                                                                     Convert.ToDecimal(indt2.Rows(i).Item("ALLOC_CAN_NB").ToString()))
                    zaiDr(0).Item("ALCTD_QT") = indt2.Rows(i).Item("ALCTD_QT").ToString()
                    zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT").ToString()) -
                                                                     Convert.ToDecimal(indt2.Rows(i).Item("ALLOC_CAN_QT").ToString()))
                    If ("0").Equals(zaiDr(0).Item("ALLOC_CAN_NB").ToString()) = True OrElse ("0").Equals(zaiDr(0).Item("ALLOC_CAN_QT").ToString()) = True Then
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Remove(zaiDr(0))
                    End If
                End If
            Next
        End If

        '引当時に商品状態を無視するか判定する(フィルメ用)
        Dim isIgnoreGoodsCond As Boolean = Me.IsIgnoreGoodsCond(Me._PrmDs)

        '自動引当時にダメージ品・ロケーションが入ってないデータは対象外にする(アクサルタ用)
        Dim isIgnoreAXALTA As Boolean = Me.IsIgnoreAXALTA(Me._PrmDs)

        '優先パレット情報のループ
        For p As Integer = 0 To ds.Tables(LMC040C.TABLE_NM_OUTZAI_PALETTE).Rows.Count - 1

            '棟,室,ZONE,LOCAを合わせてパレット名とする
            Dim pPalette As String = String.Concat(
                    ds.Tables(LMC040C.TABLE_NM_OUTZAI_PALETTE).Rows(p).Item("TOU_NO").ToString(),
                    "-",
                    ds.Tables(LMC040C.TABLE_NM_OUTZAI_PALETTE).Rows(p).Item("SITU_NO").ToString(),
                    "-",
                    ds.Tables(LMC040C.TABLE_NM_OUTZAI_PALETTE).Rows(p).Item("ZONE_CD").ToString(),
                    "-",
                    ds.Tables(LMC040C.TABLE_NM_OUTZAI_PALETTE).Rows(p).Item("LOCA").ToString())

            '在庫情報のループ
            For z As Integer = 0 To ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Count - 1

                '棟,室,ZONE,LOCAを合わせてパレット名とする
                Dim zPalette As String = String.Concat(
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("TOU_NO").ToString(),
                        "-",
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SITU_NO").ToString(),
                        "-",
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ZONE_CD").ToString(),
                        "-",
                        ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("LOCA").ToString())

                'パレット名が異なればスキップ
                If Not zPalette.Equals(pPalette) Then
                    Continue For
                End If

                '引当時に商品状態を無視するか判定する(フィルメ用)
                If Not isIgnoreGoodsCond Then
                    '商品状態区分(中身)が設定されている場合は、自動引当対象外
                    If Not String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_KB_1").ToString) Then
                        Continue For
                    End If

                    '商品状態区分(外観)が設定されている場合は、自動引当対象外
                    If Not String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_KB_2").ToString) Then
                        Continue For
                    End If
                End If

                '自動引当時にダメージ品・ロケーションが入ってないデータは対象外にする(アクサルタ用)
                If isIgnoreAXALTA Then
                    '商品状態区分(外観)が設定されている場合は、自動引当対象外
                    If Not String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_NM_3").ToString) Then
                        Continue For
                    End If

                    'ロケーションが入ってない場合は、自動引当対象外
                    If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("LOCA").ToString) Then
                        Continue For
                    End If
                End If

                'ジョンソン&ジョンソン専用
                If indt.Rows(0)("JJ_FLG").ToString.Equals(LMConst.FLG.ON) Then
                    '状態 荷主が設定されている値以外は、自動引当・一括引当対象外
                    If Not indt.Rows(0)("JJ_HIKIATE").ToString.Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_KB_3").ToString) Then
                        Continue For
                    End If
                End If

                '商品状態区分(外観)が設定されている場合は、自動引当対象外
                If Not "1.000".Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SPD_KB_FLG").ToString) Then
                    Continue For
                End If

                '小分け、サンプル時は入荷完了された商品のみ引当可
                If "03".Equals(indt.Rows(0).Item("ALCTD_KB").ToString) OrElse "04".Equals(indt.Rows(0).Item("ALCTD_KB").ToString) Then
                    If Convert.ToDouble(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKA_STATE_KB")) < 50 Then
                        Continue For
                    End If
                End If

                '引当処理
                outDr("ZAI_REC_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ZAI_REC_NO")
                outDr("TOU_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("TOU_NO")
                outDr("SITU_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SITU_NO")
                outDr("ZONE_CD") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ZONE_CD")
                outDr("LOCA") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("LOCA")
                outDr("LOT_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("LOT_NO")
                outDr("INKA_NO_L") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKA_NO_L")
                outDr("INKA_NO_M") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKA_NO_M")
                outDr("INKA_NO_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKA_NO_S")
                outDr("ALLOC_PRIORITY") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_PRIORITY")
                outDr("RSV_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("RSV_NO")
                outDr("SERIAL_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SERIAL_NO")
                outDr("HOKAN_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("HOKAN_YN")
                outDr("TAX_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("TAX_KB")
                outDr("GOODS_COND_KB_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_KB_1")
                outDr("GOODS_COND_KB_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_KB_2")
                outDr("GOODS_COND_KB_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_KB_3")
                outDr("OFB_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OFB_KB")
                outDr("SPD_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SPD_KB")
                outDr("REMARK_OUT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("REMARK_OUT")
                outDr("PORA_ZAI_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("PORA_ZAI_NB")
                outDr("IRIME") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("IRIME")
                outDr("PORA_ZAI_QT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("PORA_ZAI_QT")
                outDr("INKO_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKO_DATE")
                outDr("INKO_PLAN_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKO_PLAN_DATE")
                outDr("ZERO_FLAG") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ZERO_FLAG")
                outDr("LT_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("LT_DATE")
                outDr("GOODS_CRT_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_CRT_DATE")
                outDr("DEST_CD_P") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("DEST_CD_P")
                outDr("REMARK") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("REMARK")
                outDr("SMPL_FLAG") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SMPL_FLAG")
                outDr("GOODS_COND_NM_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_NM_1")
                outDr("GOODS_COND_NM_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_NM_2")
                outDr("GOODS_COND_NM_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_COND_NM_3")
                outDr("ALLOC_PRIORITY_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_PRIORITY_NM")
                outDr("OFB_KB_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OFB_KB_NM")
                outDr("SPD_KB_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SPD_KB_NM")
                outDr("GOODS_CD_NRS_FROM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_CD_NRS_FROM")
                outDr("KONSU") = "-1"
                outDr("HASU") = "-1"
                outDr("SURYO") = "-1"
                outDr("ALCTD_KB") = "-1"
                outDr("BUYER_ORD_NO_DTL") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("BUYER_ORD_NO_DTL")
                outDr("SERIAL_NO_L") = "-1"
                outDr("RSV_NO_L") = "-1"
                outDr("LOT_NO_L") = "-1"
                outDr("IRIME_L") = "-1"

                '引当可能個数・数量の上書き
                If 0 < ds.Tables(LMC040C.TABLE_NM_ZAI).Rows.Count Then
                    sumiZaiDr = ds.Tables(LMC040C.TABLE_NM_ZAI).Select(String.Concat("ZAI_REC_NO = '", ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ZAI_REC_NO").ToString(), "'"))
                    If 0 < sumiZaiDr.Length Then
                        If ds.Tables(LMC040C.TABLE_NM_OUT_S).Select(String.Concat("ZAI_REC_NO = '", ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ZAI_REC_NO").ToString(), "'")).Length = 0 Then
                            ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB") = sumiZaiDr(0).Item("ALLOC_CAN_NB").ToString
                            ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT") = sumiZaiDr(0).Item("ALLOC_CAN_QT").ToString
                            '引当可能個数が0の場合、抜ける
                            If Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT")) <= 0 Then
                                Continue For
                            End If
                        End If
                    End If
                End If

                '引当個数を求める
                If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB").ToString()) = False Then
                    If syukkakosu < Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB").ToString()) Then
                        '出荷個数 < 引当可能個数 の時
                        outDr("ALCTD_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_NB"))
                        outDr("HIKI_KOSU") = syukkakosu
                        outDr("ALCTD_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_NB")) + syukkakosu
                        syukkakosu = 0
                    Else
                        '引当可能個数 < 出荷個数 の時
                        outDr("ALCTD_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_NB"))
                        outDr("HIKI_KOSU") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB").ToString())
                        outDr("ALCTD_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_NB")) +
                            Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB").ToString())
                        syukkakosu = syukkakosu - Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB").ToString())
                    End If
                End If

                '引当数量を求める
                If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT").ToString()) = False Then
                    If syukkasuryo < Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT").ToString()) Then
                        '出荷個数 < 引当可能個数 の時
                        outDr("ALCTD_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_QT"))
                        outDr("HIKI_SURYO") = syukkasuryo
                        outDr("ALCTD_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_QT")) + syukkasuryo
                        syukkasuryo = 0
                    Else
                        '引当可能個数 < 出荷個数 の時
                        outDr("ALCTD_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_QT"))
                        outDr("HIKI_SURYO") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT").ToString())
                        outDr("ALCTD_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALCTD_QT")) +
                            Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT").ToString())
                        syukkasuryo = syukkasuryo - Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT").ToString())
                    End If
                End If

                If "04".Equals(indt.Rows(0)("ALCTD_KB").ToString) Then
                    'サンプルの時
                    outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB")) - Convert.ToInt32(outDr("HIKI_KOSU"))
                    outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("HIKI_SURYO"))
                    outDr("ALLOC_CAN_NB_HOZON") = "0"
                    outDr("ALLOC_CAN_QT_HOZON") = "0"
                    syukkakosu = 0
                    syukkasuryo = 0
                ElseIf "03".Equals(indt.Rows(0)("ALCTD_KB").ToString) Then
                    '小分けの時
                    outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB")) - Convert.ToInt32(outDr("ALCTD_NB"))
                    outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("ALCTD_QT").ToString)
                    outDr("ALLOC_CAN_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB"))
                    outDr("ALLOC_CAN_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT"))
                Else
                    outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB")) - Convert.ToInt32(outDr("ALCTD_NB")) + Convert.ToInt32(outDr("ALCTD_NB_HOZON"))
                    outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("ALCTD_QT")) + Convert.ToDecimal(outDr("ALCTD_QT_HOZON"))
                    outDr("ALLOC_CAN_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB"))
                    outDr("ALLOC_CAN_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT"))
                    If Convert.ToInt32(outDr("PORA_ZAI_NB")) - Convert.ToInt32(outDr("ALCTD_NB")) <> Convert.ToInt32(outDr("ALLOC_CAN_NB")) Then
                        outDr("ALLOC_CAN_NB") = Convert.ToInt32(outDr("PORA_ZAI_NB")) - Convert.ToInt32(outDr("ALCTD_NB"))
                    End If
                    If Convert.ToDecimal(outDr("PORA_ZAI_QT")) - Convert.ToDecimal(outDr("ALCTD_QT")) <> Convert.ToDecimal(outDr("ALLOC_CAN_QT")) Then
                        outDr("ALLOC_CAN_QT") = Convert.ToDecimal(outDr("PORA_ZAI_QT")) - Convert.ToDecimal(outDr("ALCTD_QT"))
                    End If
                End If

                outDr("ALCTD_KOSU") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_NB")
                outDr("ALCTD_SURYO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("ALLOC_CAN_QT")
                outDr("GOODS_CD_CUST") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_CD_CUST")
                outDr("NM_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_NM_1")
                outDr("OUTKA_ATT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OUTKA_ATT")
                outDr("SEARCH_KEY_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SEARCH_KEY_1")
                outDr("UNSO_ONDO_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("UNSO_ONDO_KB")
                outDr("PKG_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("PKG_UT")
                outDr("STD_IRIME_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("STD_IRIME_NB")
                outDr("STD_WT_KGS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("STD_WT_KGS")
                outDr("TARE_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("TARE_YN")
                outDr("HIKIATE_ALERT_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("HIKIATE_ALERT_YN")
                outDr("STD_IRIME_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("STD_IRIME_UT")
                outDr("PKG_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("PKG_NB")
                outDr("NB_UT_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("NB_UT_NM")
                outDr("IRIME_UT_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("IRIME_UT_NM")
                outDr("GOODS_CD_NRS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_CD_NRS")
                outDr("GOODS_KANRI_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("GOODS_KANRI_NO")
                outDr("CUST_CD_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_S")
                outDr("CUST_CD_SS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_SS")
                outDr("IDO_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("IDO_DATE")
                outDr("INKA_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKA_DATE")
                outDr("HOKAN_STR_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("HOKAN_STR_DATE")
                outDr("COA_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("COA_YN")
                outDr("OUTKA_KAKO_SAGYO_KB_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OUTKA_KAKO_SAGYO_KB_1")
                outDr("OUTKA_KAKO_SAGYO_KB_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OUTKA_KAKO_SAGYO_KB_2")
                outDr("OUTKA_KAKO_SAGYO_KB_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OUTKA_KAKO_SAGYO_KB_3")
                outDr("OUTKA_KAKO_SAGYO_KB_4") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OUTKA_KAKO_SAGYO_KB_4")
                outDr("OUTKA_KAKO_SAGYO_KB_5") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("OUTKA_KAKO_SAGYO_KB_5")
                outDr("SIZE_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SIZE_KB")
                outDr("NB_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("NB_UT")
                outDr("CUST_CD_L_GOODS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_L_GOODS")
                outDr("CUST_CD_M_GOODS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("CUST_CD_M_GOODS")
                outDr("INKA_DATE_KANRI_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("INKA_DATE_KANRI_KB")
                outDr("SYS_UPD_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SYS_UPD_DATE")
                outDr("SYS_UPD_TIME") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SYS_UPD_TIME")
                outDr("SHOBO_CD") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SHOBO_CD")
                outDr("SHOBO_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(z).Item("SHOBO_NM")

                '設定値をデータセットに追加
                rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT).Rows.Add(outDr)

                '出荷個数・出荷数量分、引当終わったら、処理を抜ける
                If syukkakosu = 0 AndAlso syukkasuryo = 0 Then
                    Return rtDs
                End If

                outDr = dt.NewRow()

            Next z

        Next p

        Return rtDs

    End Function

    ''' <summary>
    ''' 自動引当（遷移元が在庫振替の場合）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetHikiateLMD010(ByVal frm As LMC040F, ByVal ds As DataSet, ByVal prmDs As DataSet) As DataSet

        '行数設定
        Dim lngcnt As Integer = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Count - 1

        Dim rtDs As DataSet = New LMC040DS()
        Dim dt As DataTable = rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT)
        Dim indt As DataTable = prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN)
        Dim outDr As DataRow = Nothing

        '値設定
        For i As Integer = 0 To lngcnt
            outDr = dt.NewRow()

            'START YANAI 要望番号1200 自動引当・一括引当変更
            If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_1").ToString) = False Then
                '商品状態区分(中身)が設定されている場合は、自動引当対象外
                Continue For
            End If

            If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_2").ToString) = False Then
                '商品状態区分(外観)が設定されている場合は、自動引当対象外
                Continue For
            End If

            If ("1.000").Equals(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SPD_KB_FLG").ToString) = False Then
                '商品状態区分(外観)が設定されている場合は、自動引当対象外
                Continue For
            End If
            'END YANAI 要望番号1200 自動引当・一括引当変更

            outDr("ZAI_REC_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ZAI_REC_NO")
            outDr("TOU_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("TOU_NO")
            outDr("SITU_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SITU_NO")
            outDr("ZONE_CD") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ZONE_CD")
            outDr("LOCA") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("LOCA")
            outDr("LOT_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("LOT_NO")
            outDr("INKA_NO_L") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_NO_L")
            outDr("INKA_NO_M") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_NO_M")
            outDr("INKA_NO_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_NO_S")
            outDr("ALLOC_PRIORITY") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_PRIORITY")
            outDr("RSV_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("RSV_NO")
            outDr("SERIAL_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SERIAL_NO")
            outDr("HOKAN_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("HOKAN_YN")
            outDr("TAX_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("TAX_KB")
            outDr("GOODS_COND_KB_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_1")
            outDr("GOODS_COND_KB_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_2")
            outDr("GOODS_COND_KB_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_KB_3")
            outDr("OFB_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OFB_KB")
            outDr("SPD_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SPD_KB")
            outDr("REMARK_OUT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("REMARK_OUT")
            outDr("PORA_ZAI_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("PORA_ZAI_NB")

            outDr("IRIME") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("IRIME")
            outDr("PORA_ZAI_QT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("PORA_ZAI_QT")
            outDr("INKO_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKO_DATE")
            outDr("INKO_PLAN_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKO_PLAN_DATE")
            outDr("ZERO_FLAG") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ZERO_FLAG")
            outDr("LT_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("LT_DATE")
            outDr("GOODS_CRT_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_CRT_DATE")
            outDr("DEST_CD_P") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("DEST_CD_P")
            outDr("REMARK") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("REMARK")
            outDr("SMPL_FLAG") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SMPL_FLAG")
            outDr("GOODS_COND_NM_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_NM_1")
            outDr("GOODS_COND_NM_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_NM_2")
            outDr("GOODS_COND_NM_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_COND_NM_3")
            outDr("ALLOC_PRIORITY_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_PRIORITY_NM")
            outDr("OFB_KB_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OFB_KB_NM")
            outDr("SPD_KB_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SPD_KB_NM")
            outDr("GOODS_CD_NRS_FROM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_CD_NRS_FROM")
            outDr("KONSU") = "-1"
            outDr("HASU") = "-1"
            outDr("SURYO") = "-1"
            outDr("ALCTD_KB") = "-1"
            'outDr("OUTKA_NO_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_NO_S")
            outDr("BUYER_ORD_NO_DTL") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("BUYER_ORD_NO_DTL")
            outDr("SERIAL_NO_L") = "-1"
            outDr("RSV_NO_L") = "-1"
            outDr("LOT_NO_L") = "-1"
            outDr("IRIME_L") = "-1"

            '引当個数を求める
            If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB").ToString()) = False Then
                '引当可能個数 < 出荷個数 の時
                outDr("ALCTD_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB"))
                outDr("HIKI_KOSU") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB").ToString())
                outDr("ALCTD_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB")) + _
                                    Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB").ToString())
            End If

            '引当数量を求める
            If String.IsNullOrEmpty(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT").ToString()) = False Then
                '引当可能個数 < 出荷個数 の時
                outDr("ALCTD_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_QT"))
                outDr("HIKI_SURYO") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT").ToString())
                outDr("ALCTD_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_QT")) + _
                                    Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT").ToString())
            End If

            If ("04").Equals(indt.Rows(0)("ALCTD_KB").ToString()) = True Then
                'サンプルの時
                outDr("ALCTD_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_NB")
                outDr("ALCTD_QT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALCTD_QT")
                outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB"))
                outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT"))
                outDr("ALLOC_CAN_NB_HOZON") = "0"
                outDr("ALLOC_CAN_QT_HOZON") = "0"
            ElseIf ("03").Equals(indt.Rows(0)("ALCTD_KB").ToString()) = True Then
                '小分けの時
                outDr("ALLOC_CAN_NB") = "1"
                outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("ALCTD_QT").ToString)
                outDr("ALLOC_CAN_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB"))
                outDr("ALLOC_CAN_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT"))
            Else
                outDr("ALLOC_CAN_NB") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB")) - Convert.ToInt32(outDr("ALCTD_NB").ToString)
                outDr("ALLOC_CAN_QT") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT")) - Convert.ToDecimal(outDr("ALCTD_QT").ToString)
                outDr("ALLOC_CAN_NB_HOZON") = Convert.ToInt32(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB"))
                outDr("ALLOC_CAN_QT_HOZON") = Convert.ToDecimal(ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT"))
            End If

            outDr("ALCTD_KOSU") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_NB")
            outDr("ALCTD_SURYO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("ALLOC_CAN_QT")

            outDr("GOODS_CD_CUST") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_CD_CUST")
            outDr("NM_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_NM_1")
            outDr("OUTKA_ATT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_ATT")
            outDr("SEARCH_KEY_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SEARCH_KEY_1")
            outDr("UNSO_ONDO_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("UNSO_ONDO_KB")
            outDr("PKG_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("PKG_UT")
            outDr("STD_IRIME_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("STD_IRIME_NB")
            outDr("STD_WT_KGS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("STD_WT_KGS")
            outDr("TARE_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("TARE_YN")
            outDr("HIKIATE_ALERT_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("HIKIATE_ALERT_YN")
            outDr("STD_IRIME_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("STD_IRIME_UT")
            outDr("PKG_NB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("PKG_NB")
            outDr("NB_UT_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("NB_UT_NM")
            outDr("IRIME_UT_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("IRIME_UT_NM")
            outDr("GOODS_CD_NRS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_CD_NRS")
            'START ADD 2013/09/10 KOBAYASHI WIT対応
            outDr("GOODS_KANRI_NO") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("GOODS_KANRI_NO")
            'END   ADD 2013/09/10 KOBAYASHI WIT対応
            outDr("CUST_CD_S") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_S")
            outDr("CUST_CD_SS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_SS")
            outDr("IDO_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("IDO_DATE")
            outDr("INKA_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_DATE")
            outDr("HOKAN_STR_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("HOKAN_STR_DATE")
            outDr("COA_YN") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("COA_YN")
            outDr("OUTKA_KAKO_SAGYO_KB_1") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_1")
            outDr("OUTKA_KAKO_SAGYO_KB_2") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_2")
            outDr("OUTKA_KAKO_SAGYO_KB_3") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_3")
            outDr("OUTKA_KAKO_SAGYO_KB_4") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_4")
            outDr("OUTKA_KAKO_SAGYO_KB_5") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("OUTKA_KAKO_SAGYO_KB_5")
            outDr("SIZE_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SIZE_KB")
            outDr("NB_UT") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("NB_UT")
            'START YANAI 要望番号499
            outDr("CUST_CD_L_GOODS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_L_GOODS")
            outDr("CUST_CD_M_GOODS") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("CUST_CD_M_GOODS")
            'END YANAI 要望番号499
            'START YANAI 要望番号780
            outDr("INKA_DATE_KANRI_KB") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("INKA_DATE_KANRI_KB")
            'END YANAI 要望番号780

            outDr("SYS_UPD_DATE") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SYS_UPD_DATE")
            outDr("SYS_UPD_TIME") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SYS_UPD_TIME")

            outDr("SHOBO_CD") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SHOBO_CD")
            outDr("SHOBO_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("SHOBO_NM")

            outDr("BYK_KEEP_GOODS_CD") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("BYK_KEEP_GOODS_CD")
            outDr("KEEP_GOODS_NM") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("KEEP_GOODS_NM")
            outDr("IS_BYK_KEEP_GOODS_CD") = ds.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(i).Item("IS_BYK_KEEP_GOODS_CD")

            '設定値をデータセットに設定
            rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT).Rows.Add(outDr)

        Next

        Return rtDs

    End Function

    ''' <summary>
    ''' 選択
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function OutDataSet(ByVal frm As LMC040F) As DataSet

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me._V.getCheckList()
        Dim lngcnt As Integer = Me._ChkList.Count() - 1

        Dim rtDs As DataSet = New LMC040DS()
        Dim dt As DataTable = rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT)
        Dim outDr As DataRow = dt.NewRow()

        '値設定
        For i As Integer = 0 To lngcnt
            With frm.sprZaiko.ActiveSheet

                If ("0").Equals(Me._LMCconG.GetCellValue(frm.sprZaiko.ActiveSheet.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.HIKI_CNT.ColNo))) = True AndAlso _
                    (LMC040C.PLUS_ZERO).Equals(Me._LMCconG.GetCellValue(frm.sprZaiko.ActiveSheet.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.HIKI_AMT.ColNo))) = True AndAlso _
                    frm.optSample.Checked = False Then
                    Continue For
                End If

                outDr = dt.NewRow()

                'rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT).Rows.Add(rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT).NewRow())

                Dim setRows() As DataRow = _SelectZaiko.Select(String.Concat("ZAI_REC_NO = '", Me._LMCconG.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.ZAI_REC_NO.ColNo)), "'"))

                outDr("ZAI_REC_NO") = setRows(0)(LMC040C.DsOutZaiColumnIndex.ZAI_REC_NO).ToString()
                outDr("TOU_NO") = setRows(0)(LMC040C.DsOutZaiColumnIndex.TOU_NO).ToString()
                outDr("SITU_NO") = setRows(0)(LMC040C.DsOutZaiColumnIndex.SITU_NO).ToString()
                outDr("ZONE_CD") = setRows(0)(LMC040C.DsOutZaiColumnIndex.ZONE_CD).ToString()
                outDr("LOCA") = setRows(0)(LMC040C.DsOutZaiColumnIndex.LOCA).ToString()
                outDr("LOT_NO") = setRows(0)(LMC040C.DsOutZaiColumnIndex.LOT_NO).ToString().ToUpper
                outDr("INKA_NO_L") = setRows(0)(LMC040C.DsOutZaiColumnIndex.INKA_NO_L).ToString()
                outDr("INKA_NO_M") = setRows(0)(LMC040C.DsOutZaiColumnIndex.INKA_NO_M).ToString()
                outDr("INKA_NO_S") = setRows(0)(LMC040C.DsOutZaiColumnIndex.INKA_NO_S).ToString()
                outDr("ALLOC_PRIORITY") = setRows(0)(LMC040C.DsOutZaiColumnIndex.ALLOC_PRIORITY).ToString()
                outDr("RSV_NO") = setRows(0)(LMC040C.DsOutZaiColumnIndex.RSV_NO).ToString()
                outDr("SERIAL_NO") = setRows(0)(LMC040C.DsOutZaiColumnIndex.SERIAL_NO).ToString()
                outDr("HOKAN_YN") = setRows(0)(LMC040C.DsOutZaiColumnIndex.HOKAN_YN).ToString()
                outDr("TAX_KB") = setRows(0)(LMC040C.DsOutZaiColumnIndex.TAX_KB).ToString()
                outDr("GOODS_COND_KB_1") = setRows(0)(LMC040C.DsOutZaiColumnIndex.GOODS_COND_KB_1).ToString()
                outDr("GOODS_COND_KB_2") = setRows(0)(LMC040C.DsOutZaiColumnIndex.GOODS_COND_KB_2).ToString()
                outDr("GOODS_COND_KB_3") = setRows(0)(LMC040C.DsOutZaiColumnIndex.GOODS_COND_KB_3).ToString()
                outDr("OFB_KB") = setRows(0)(LMC040C.DsOutZaiColumnIndex.OFB_KB).ToString()
                outDr("SPD_KB") = setRows(0)(LMC040C.DsOutZaiColumnIndex.SPD_KB).ToString()
                outDr("REMARK_OUT") = setRows(0)(LMC040C.DsOutZaiColumnIndex.REMARK_OUT).ToString()
                outDr("PORA_ZAI_NB") = setRows(0)(LMC040C.DsOutZaiColumnIndex.PORA_ZAI_NB).ToString()
                'outDr("ALCTD_NB") = Me._LMCconG.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.HIKI_CNT.ColNo))
                'outDr("ALLOC_CAN_NB") = Convert.ToInt32(setRows(0)(LMC040C.DsOutZaiColumnIndex.ALLOC_CAN_NB).ToString()) - Convert.ToInt32(outDr("ALCTD_NB").ToString)
                outDr("IRIME") = setRows(0)(LMC040C.DsOutZaiColumnIndex.IRIME).ToString()
                outDr("PORA_ZAI_QT") = setRows(0)(LMC040C.DsOutZaiColumnIndex.PORA_ZAI_QT).ToString()
                'outDr("ALCTD_QT") = Me._LMCconG.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.HIKI_AMT.ColNo))
                'outDr("ALLOC_CAN_QT") = Convert.ToDecimal(setRows(0)(LMC040C.DsOutZaiColumnIndex.ALLOC_CAN_QT).ToString()) - Convert.ToDecimal(outDr("ALCTD_QT").ToString)
                outDr("INKO_DATE") = setRows(0)(LMC040C.DsOutZaiColumnIndex.INKO_DATE).ToString()
                outDr("INKO_PLAN_DATE") = setRows(0)(LMC040C.DsOutZaiColumnIndex.INKO_PLAN_DATE).ToString()
                outDr("ZERO_FLAG") = setRows(0)(LMC040C.DsOutZaiColumnIndex.ZERO_FLAG).ToString()
                outDr("LT_DATE") = setRows(0)(LMC040C.DsOutZaiColumnIndex.LT_DATE).ToString()
                outDr("GOODS_CRT_DATE") = setRows(0)(LMC040C.DsOutZaiColumnIndex.GOODS_CRT_DATE).ToString()
                outDr("DEST_CD_P") = setRows(0)(LMC040C.DsOutZaiColumnIndex.DEST_CD_P).ToString()
                outDr("REMARK") = setRows(0)(LMC040C.DsOutZaiColumnIndex.REMARK).ToString()
                outDr("SMPL_FLAG") = setRows(0)(LMC040C.DsOutZaiColumnIndex.SMPL_FLAG).ToString()
                outDr("GOODS_COND_NM_1") = setRows(0)(LMC040C.DsOutZaiColumnIndex.GOODS_COND_NM_1).ToString()
                outDr("GOODS_COND_NM_2") = setRows(0)(LMC040C.DsOutZaiColumnIndex.GOODS_COND_NM_2).ToString()
                outDr("GOODS_COND_NM_3") = setRows(0)(LMC040C.DsOutZaiColumnIndex.GOODS_COND_NM_3).ToString()
                outDr("ALLOC_PRIORITY_NM") = setRows(0)(LMC040C.DsOutZaiColumnIndex.ALLOC_PRIORITY_NM).ToString()
                outDr("OFB_KB_NM") = setRows(0)(LMC040C.DsOutZaiColumnIndex.OFB_KB_NM).ToString()
                outDr("SPD_KB_NM") = setRows(0)(LMC040C.DsOutZaiColumnIndex.SPD_KB_NM).ToString()
                outDr("GOODS_CD_NRS_FROM") = setRows(0)(LMC040C.DsOutZaiColumnIndex.GOODS_CD_NRS_FROM).ToString()
                outDr("KONSU") = frm.numSyukkaKosu.TextValue
#If False Then  'UPD 2018/10/26 端数　カンマ付きで設定されている　"4,230"
                 outDr("HASU") = frm.numSyukkaHasu.TextValue
#Else
                outDr("HASU") = frm.numSyukkaHasu.Value
#End If
                outDr("KOSU") = frm.numSyukkaSouCnt.TextValue
                outDr("SURYO") = frm.numSyukkaSouAmt.TextValue

                '今回引当てる前の値
                outDr("ALCTD_NB_HOZON") = setRows(0)(LMC040C.DsOutZaiColumnIndex.ALCTD_NB).ToString()
                outDr("ALLOC_CAN_NB_HOZON") = setRows(0)(LMC040C.DsOutZaiColumnIndex.ALLOC_CAN_NB).ToString()
                outDr("ALCTD_QT_HOZON") = setRows(0)(LMC040C.DsOutZaiColumnIndex.ALCTD_QT).ToString()
                outDr("ALLOC_CAN_QT_HOZON") = setRows(0)(LMC040C.DsOutZaiColumnIndex.ALLOC_CAN_QT).ToString()

                '今回引当てる値
                outDr("HIKI_KOSU") = Me._LMCconG.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.HIKI_CNT.ColNo))
                outDr("HIKI_SURYO") = Me._LMCconG.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.HIKI_AMT.ColNo))

                '今回引当てた後の計算結果の値
                outDr("ALCTD_NB") = Convert.ToInt32(Me._LMCconG.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.HIKI_CNT.ColNo))) + _
                    Convert.ToInt32(setRows(0)(LMC040C.DsOutZaiColumnIndex.ALCTD_NB).ToString())
                'START YANAI 20110913 小分け対応
                'outDr("ALLOC_CAN_NB") = Convert.ToInt32(setRows(0)(LMC040C.DsOutZaiColumnIndex.ALLOC_CAN_NB).ToString()) - _
                '                        Convert.ToInt32(Me._LMCconG.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.HIKI_CNT.ColNo)))
                If frm.optKowake.Checked = False Then
                    '小分け以外の場合
                    outDr("ALLOC_CAN_NB") = Convert.ToInt32(setRows(0)(LMC040C.DsOutZaiColumnIndex.ALLOC_CAN_NB).ToString()) - _
                                            Convert.ToInt32(Me._LMCconG.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.HIKI_CNT.ColNo)))
                Else
                    '小分けの場合
                    outDr("ALLOC_CAN_NB") = Convert.ToInt32(setRows(0)(LMC040C.DsOutZaiColumnIndex.ALLOC_CAN_NB).ToString()) - 1
                End If
                'END YANAI 20110913 小分け対応
                outDr("ALCTD_QT") = Convert.ToDecimal(Me._LMCconG.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.HIKI_AMT.ColNo))) + _
                                    Convert.ToDecimal(setRows(0)(LMC040C.DsOutZaiColumnIndex.ALCTD_QT).ToString())
                outDr("ALLOC_CAN_QT") = Convert.ToDecimal(setRows(0)(LMC040C.DsOutZaiColumnIndex.ALLOC_CAN_QT).ToString()) - _
                                         Convert.ToDecimal(Me._LMCconG.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.HIKI_AMT.ColNo)))

                If frm.optCnt.Checked = True Then
                    outDr("ALCTD_KB") = "01"
                ElseIf frm.optAmt.Checked = True Then
                    outDr("ALCTD_KB") = "02"
                ElseIf frm.optKowake.Checked = True Then
                    outDr("ALCTD_KB") = "03"
                ElseIf frm.optSample.Checked = True Then
                    outDr("ALCTD_KB") = "04"
                    'START YANAI 20110906 サンプル対応
                    'outDr("ALCTD_NB") = setRows(0)(LMC040C.DsOutZaiColumnIndex.ALCTD_NB).ToString()
                    'outDr("ALLOC_CAN_NB") = setRows(0)(LMC040C.DsOutZaiColumnIndex.ALLOC_CAN_NB).ToString()
                    'outDr("ALCTD_QT") = setRows(0)(LMC040C.DsOutZaiColumnIndex.ALCTD_QT).ToString()
                    'outDr("ALLOC_CAN_QT") = setRows(0)(LMC040C.DsOutZaiColumnIndex.ALLOC_CAN_QT).ToString()
                    'END YANAI 20110906 サンプル対応
                End If

                outDr("ALCTD_KOSU") = Me._LMCconG.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.HIKI_KANO_CNT.ColNo))
                outDr("ALCTD_SURYO") = Me._LMCconG.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LMC040G.sprZaiko.HIKI_KANO_AMT.ColNo))
                'outDr("OUTKA_NO_S") = setRows(0)(LMC040C.DsOutZaiColumnIndex.OUTKA_NO_S).ToString()
                outDr("BUYER_ORD_NO_DTL") = setRows(0)(LMC040C.DsOutZaiColumnIndex.BUYER_ORD_NO_DTL).ToString()
                outDr("SERIAL_NO_L") = frm.txtSerialNO.TextValue
                outDr("RSV_NO_L") = frm.txtRsvNO.TextValue
                outDr("LOT_NO_L") = frm.txtLotNO.TextValue
                outDr("IRIME_L") = frm.numIrime.TextValue
                outDr("GOODS_CD_CUST") = setRows(0)(LMC040C.DsOutZaiColumnIndex.GOODS_CD_CUST).ToString()
                outDr("NM_1") = setRows(0)(LMC040C.DsOutZaiColumnIndex.GOODS_NM_1).ToString()
                outDr("OUTKA_ATT") = setRows(0)(LMC040C.DsOutZaiColumnIndex.OUTKA_ATT).ToString()
                outDr("SEARCH_KEY_1") = setRows(0)(LMC040C.DsOutZaiColumnIndex.SEARCH_KEY_1).ToString()
                outDr("UNSO_ONDO_KB") = setRows(0)(LMC040C.DsOutZaiColumnIndex.UNSO_ONDO_KB).ToString()
                outDr("PKG_UT") = setRows(0)(LMC040C.DsOutZaiColumnIndex.PKG_UT).ToString()
                outDr("STD_IRIME_NB") = setRows(0)(LMC040C.DsOutZaiColumnIndex.STD_IRIME_NB).ToString()
                outDr("STD_WT_KGS") = setRows(0)(LMC040C.DsOutZaiColumnIndex.STD_WT_KGS).ToString()
                outDr("TARE_YN") = setRows(0)(LMC040C.DsOutZaiColumnIndex.TARE_YN).ToString()
                outDr("HIKIATE_ALERT_YN") = setRows(0)(LMC040C.DsOutZaiColumnIndex.HIKIATE_ALERT_YN).ToString()
                outDr("STD_IRIME_UT") = setRows(0)(LMC040C.DsOutZaiColumnIndex.STD_IRIME_UT).ToString()
                outDr("PKG_NB") = setRows(0)(LMC040C.DsOutZaiColumnIndex.PKG_NB).ToString()
                outDr("NB_UT_NM") = setRows(0)(LMC040C.DsOutZaiColumnIndex.NB_UT_NM).ToString()
                outDr("IRIME_UT_NM") = setRows(0)(LMC040C.DsOutZaiColumnIndex.IRIME_UT_NM).ToString()
                outDr("GOODS_CD_NRS") = setRows(0)(LMC040C.DsOutZaiColumnIndex.GOODS_CD_NRS).ToString()
                'START ADD 2013/09/10 KOBAYASHI WIT対応
                outDr("GOODS_KANRI_NO") = setRows(0)(LMC040C.DsOutZaiColumnIndex.GOODS_KANRI_NO).ToString()
                'END   ADD 2013/09/10 KOBAYASHI WIT対応
                outDr("CUST_CD_S") = setRows(0)(LMC040C.DsOutZaiColumnIndex.CUST_CD_S).ToString()
                outDr("CUST_CD_SS") = setRows(0)(LMC040C.DsOutZaiColumnIndex.CUST_CD_SS).ToString()
                outDr("IDO_DATE") = setRows(0)(LMC040C.DsOutZaiColumnIndex.IDO_DATE).ToString()
                outDr("INKA_DATE") = setRows(0)(LMC040C.DsOutZaiColumnIndex.INKA_DATE).ToString()
                outDr("HOKAN_STR_DATE") = setRows(0)(LMC040C.DsOutZaiColumnIndex.HOKAN_STR_DATE).ToString()
                outDr("COA_YN") = setRows(0)(LMC040C.DsOutZaiColumnIndex.COA_YN).ToString()
                outDr("OUTKA_KAKO_SAGYO_KB_1") = setRows(0)(LMC040C.DsOutZaiColumnIndex.OUTKA_KAKO_SAGYO_KB_1).ToString()
                outDr("OUTKA_KAKO_SAGYO_KB_2") = setRows(0)(LMC040C.DsOutZaiColumnIndex.OUTKA_KAKO_SAGYO_KB_2).ToString()
                outDr("OUTKA_KAKO_SAGYO_KB_3") = setRows(0)(LMC040C.DsOutZaiColumnIndex.OUTKA_KAKO_SAGYO_KB_3).ToString()
                outDr("OUTKA_KAKO_SAGYO_KB_4") = setRows(0)(LMC040C.DsOutZaiColumnIndex.OUTKA_KAKO_SAGYO_KB_4).ToString()
                outDr("OUTKA_KAKO_SAGYO_KB_5") = setRows(0)(LMC040C.DsOutZaiColumnIndex.OUTKA_KAKO_SAGYO_KB_5).ToString()
                outDr("SIZE_KB") = setRows(0)(LMC040C.DsOutZaiColumnIndex.SIZE_KB).ToString()
                outDr("NB_UT") = setRows(0)(LMC040C.DsOutZaiColumnIndex.NB_UT).ToString()
                'START YANAI 要望番号499
                outDr("CUST_CD_L_GOODS") = setRows(0)(LMC040C.DsOutZaiColumnIndex.CUST_CD_L_GOODS).ToString()
                outDr("CUST_CD_M_GOODS") = setRows(0)(LMC040C.DsOutZaiColumnIndex.CUST_CD_M_GOODS).ToString()
                'END YANAI 要望番号499
                'START YANAI 要望番号780
                outDr("INKA_DATE_KANRI_KB") = setRows(0)(LMC040C.DsOutZaiColumnIndex.INKA_DATE_KANRI_KB).ToString()
                'END YANAI 要望番号780
                outDr("IRIME_UT") = setRows(0)(LMC040C.DsOutZaiColumnIndex.STD_IRIME_UT).ToString

                outDr("SYS_UPD_DATE") = setRows(0)(LMC040C.DsOutZaiColumnIndex.SYS_UPD_DATE).ToString()
                outDr("SYS_UPD_TIME") = setRows(0)(LMC040C.DsOutZaiColumnIndex.SYS_UPD_TIME).ToString()

                outDr("SHOBO_CD") = setRows(0)(LMC040C.DsOutZaiColumnIndex.SHOBO_CD).ToString()
                outDr("SHOBO_NM") = setRows(0)(LMC040C.DsOutZaiColumnIndex.SHOBO_NM).ToString()

                '要望番号1995 端数出荷時作業区分対応尾
                outDr("OUTKA_HASU_SAGYO_KB_1") = setRows(0)("OUTKA_HASU_SAGYO_KB_1").ToString()
                outDr("OUTKA_HASU_SAGYO_KB_2") = setRows(0)("OUTKA_HASU_SAGYO_KB_2").ToString()
                outDr("OUTKA_HASU_SAGYO_KB_3") = setRows(0)("OUTKA_HASU_SAGYO_KB_3").ToString()
                outDr("NRS_BR_CD") = setRows(0)(LMC040C.DsOutZaiColumnIndex.NRS_BR_CD).ToString()
                outDr("CUST_CD_L") = setRows(0)(LMC040C.DsOutZaiColumnIndex.CUST_CD_L).ToString()

                outDr("BYK_KEEP_GOODS_CD") = setRows(0)("BYK_KEEP_GOODS_CD").ToString()
                outDr("KEEP_GOODS_NM") = setRows(0)("KEEP_GOODS_NM").ToString()
                outDr("IS_BYK_KEEP_GOODS_CD") = setRows(0)("IS_BYK_KEEP_GOODS_CD").ToString()


                '設定値をデータセットに設定
                rtDs.Tables(LMControlC.LMC040C_TABLE_NM_OUT).Rows.Add(outDr)

            End With

        Next

        Return rtDs

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMC040F) As Boolean

        If Me._Prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_OUT) Is Nothing = True _
            OrElse Me._Prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_OUT).Rows.Count = 0 Then

            'リターンコードの設定
            Me._Prm.ReturnFlg = False
        Else

            'リターンコードの設定
            Me._Prm.ReturnFlg = True

        End If

    End Function

    ''' <summary>
    ''' デフォルトメッセージを設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDefMessage(ByVal frm As LMC040F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = True Then

            'メッセージ設定
            MyBase.ShowMessage(frm, "G003")

        End If

    End Sub

    'START KIM 要望番号1479 一括引当時、引当画面の速度改善

    ''' <summary>
    ''' 個数・数量を求める
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCalSumForWithoutForm(ByVal eventShubetsu As LMC040C.EventShubetsu, ByVal prmDs As DataSet, _
                                       ByVal hikiFlg As String)

        With prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN)

            'START 2013/3/4 CSV引当対応（引当数量=入目・個数=1）
            If hikiFlg.Equals("03") = True Then
                '格納変数に計算済みの値を設定する（1固定）
                Me._V._numHikiZanCnt = 1
                Me._V._numHikiZanAmt = Convert.ToDecimal(.Rows(0)("IRIME").ToString())
                Exit Sub
            End If
            'END   2013/3/4 CSV引当対応（引当数量=入目・個数=1）

            Dim souAmt As Decimal = 0
            Dim souCnt As Decimal = 0
            Dim kosu As Decimal = 0
            Dim hasu As Decimal = 0

            Dim numHikiZanCnt As Decimal = Convert.ToDecimal(.Rows(0)("BACKLOG_NB").ToString())
            Dim numHikiZanAmt As Decimal = Convert.ToDecimal(.Rows(0)("BACKLOG_QT").ToString())
            Dim numHikiSumiCnt As Decimal = Convert.ToDecimal(.Rows(0)("ALCTD_NB").ToString())
            Dim numSyukkaKosu As Decimal = Convert.ToDecimal(.Rows(0)("KONSU").ToString())
            Dim numIrisu As Decimal = Convert.ToDecimal(.Rows(0)("PKG_NB").ToString())
            Dim numSyukkaHasu As Decimal = Convert.ToDecimal(.Rows(0)("HASU").ToString())
            Dim numHikiSumiAmt As Decimal = Convert.ToDecimal(.Rows(0)("ALCTD_QT").ToString())
            Dim numSyukkaSouAmt As Decimal = Convert.ToDecimal(.Rows(0)("SURYO").ToString())
            Dim numSyukkaSouCnt As Decimal = Convert.ToDecimal(.Rows(0)("KOSU").ToString())

            If kosu = 0 Then
                '出荷個数計算
                kosu = Convert.ToDecimal( _
                       Convert.ToDecimal(numSyukkaKosu) _
                     * Convert.ToDecimal(numIrisu) _
                     + Convert.ToDecimal(numSyukkaHasu))
            End If

            '値設定
            If souAmt <> 0 Then
                numSyukkaSouAmt = souAmt
            End If

            If souCnt <> 0 OrElse hasu <> 0 Then
                numSyukkaKosu = souCnt
                numSyukkaHasu = hasu
            ElseIf (LMC040C.EventShubetsu.KENSAKU).Equals(eventShubetsu) = False AndAlso _
                souCnt = 0 AndAlso _
                (.Rows(0)("ALCTD_KB").ToString().Equals("03") = True OrElse .Rows(0)("ALCTD_KB").ToString().Equals("04") = True) Then
                numSyukkaKosu = 1
                numSyukkaHasu = 0
                kosu = 1
            End If

            If .Rows(0)("ALCTD_KB").ToString().Equals("04") = True Then  'サンプルチェック時
                numSyukkaKosu = 0
                numSyukkaHasu = 0
                kosu = 0
            End If

            numHikiZanCnt = kosu - Convert.ToDecimal(numHikiSumiCnt)
            numHikiZanAmt = Convert.ToDecimal(numSyukkaSouAmt) - Convert.ToDecimal(numHikiSumiAmt)
            numSyukkaSouCnt = kosu

            '格納変数に計算済みの値を設定する
            Me._V._numHikiZanCnt = numHikiZanCnt
            Me._V._numHikiZanAmt = numHikiZanAmt

        End With

    End Sub

    'START KIM 要望番号1479 一括引当時、引当画面の速度改善
    ''' <summary>
    ''' WSAクラス呼出（フォームなし）
    ''' </summary>
    ''' <param name="BLF">BLFファイル名</param>
    ''' <param name="methodName">メソッド名</param>
    ''' <param name="rtDs">データセット</param>
    ''' <returns>WSA呼出後のデータセット</returns>
    ''' <returns2>取得エラー時=Nothing。取得成功時=rtnDSを設定。取得0件の時もrtnDSを設定しているのは、呼び元画面にてSpreadクリアの判定に使用するため。</returns2>
    ''' <remarks></remarks>
    Private Function CallWSAActionWithoutForm(ByVal BLF As String, _
                                              ByVal methodName As String, ByRef rtDs As DataSet, ByVal rc As Integer _
                                                , Optional ByVal mc As Integer = -1) As DataSet
        'エラーメッセージクリア
        MyBase.ClearMessageData()

        '閾値の設定
        MyBase.SetLimitCount(rc)

        '表示最大件数の設定
        MyBase.SetMaxResultCount(mc)

        Dim rtnDs As DataSet = MyBase.CallWSA(BLF, methodName, rtDs)

        'メッセージ判定
        If MyBase.IsMessageExist() = False Then

            '検索成功時
            Return rtnDs

        End If

        Return Nothing

    End Function

    'END KIM 要望番号1479 一括引当時、引当画面の速度改善

#If True Then ' フィルメニッヒ セミEDI対応  20161003 added inoue

    ''' <summary>
    ''' 引当時に商品状態を無視するか判定する(フィルメ用)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsIgnoreGoodsCond(ByVal ds As DataSet) As Boolean

        Return (ds.Tables.Contains(LMControlC.LMC040C_TABLE_NM_IN) AndAlso _
                ds.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows.Count > 0 AndAlso _
                LMC040C.SORT_FLG.FIRMENICH.Equals(ds.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("SORT_FLG")))

    End Function

#End If

    ''' <summary>
    ''' 自動引当時にダメージ品・ロケーションが入ってないデータは対象外にする(アクサルタ用)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsIgnoreAXALTA(ByVal ds As DataSet) As Boolean

        Return (ds.Tables.Contains(LMControlC.LMC040C_TABLE_NM_IN) AndAlso _
                ds.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows.Count > 0 AndAlso _
                LMC040C.SORT_FLG.AXALTA.Equals(ds.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("SORT_FLG")))

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分"

    ''' <summary>
    ''' F5押下時処理呼び出し(他荷主処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByVal frm As LMC040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '「他荷主」処理
        Me.ActionControl(LMC040C.EventShubetsu.TANINUSI, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMC040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '「検索」処理
        Me.ActionControl(LMC040C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMC040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectEvent")

        '「選択」処理
        Me.ActionControl(LMC040C.EventShubetsu.SENTAKU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMC040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        '終了処理  
        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMC040F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' 梱数の値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numSyukkaKosu_Leave(ByVal frm As LMC040F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CAL_KOSU")

        '「個数」処理
        Me.ActionControl(LMC040C.EventShubetsu.CAL_KONSU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CAL_KOSU")

    End Sub

    ''' <summary>
    ''' 端数の値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numSyukkaHasu_Leave(ByVal frm As LMC040F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CAL_KOSU")

        '「端数」処理
        Me.ActionControl(LMC040C.EventShubetsu.CAL_KONSU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CAL_KOSU")

    End Sub

    ''' <summary>
    ''' 数量の値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numSyukkaSuryo_Leave(ByVal frm As LMC040F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CAL_SURYO")

        '「数量」処理
        Me.ActionControl(LMC040C.EventShubetsu.CAL_SURYO, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CAL_SURYO")

    End Sub

    'START YANAI 20111027 入り目対応
    '''' <summary>
    '''' 入目の値変更イベント
    '''' </summary>
    '''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    '''' <remarks></remarks>
    'Friend Sub numIrime_Leave(ByVal frm As LMC040F)

    '    MyBase.Logger.StartLog(MyBase.GetType.Name, "CAL_IRIME")

    '    '「入目」処理
    '    Me.ActionControl(LMC040C.EventShubetsu.CAL_IRIME, frm)

    '    MyBase.Logger.EndLog(MyBase.GetType.Name, "CAL_IRIME")

    'End Sub
    'END YANAI 20111027 入り目対応

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub sprZaiko_Change(ByVal frm As LMC040F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprZaiko_Change")

        '「スプレッド変更」処理
        Me.ActionControl(LMC040C.EventShubetsu.CHANGE_SPREAD, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprZaiko_Change")

    End Sub

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub optCnt_Selected(ByVal frm As LMC040F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "optCnt_Selected")

        '「スプレッド変更」処理
        Me.ActionControl(LMC040C.EventShubetsu.OPT_KOSU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "optCnt_Selected")

    End Sub

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub optAmt_Selected(ByVal frm As LMC040F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "optAmt_Selected")

        '「スプレッド変更」処理
        Me.ActionControl(LMC040C.EventShubetsu.OPT_SURYO, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "optAmt_Selected")

    End Sub

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub optKowake_Selected(ByVal frm As LMC040F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "optKowake_Selected")

        '「スプレッド変更」処理
        Me.ActionControl(LMC040C.EventShubetsu.OPT_KOWAKE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "optKowake_Selected")

    End Sub

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub optSample_Selected(ByVal frm As LMC040F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "optSample_Selected")

        '「スプレッド変更」処理
        Me.ActionControl(LMC040C.EventShubetsu.OPT_SAMPLE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "optSample_Selected")

    End Sub

    ''' <summary>
    ''' フォームがアクティブになるときに発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub LMC040F_Activated(ByVal frm As LMC040F)
        Me._G.SetFoucus()
    End Sub

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub sprZaiko_KeysAdd(ByVal frm As LMC040F)

        'チェックオンオフ
        Me._G.SetCheckOnOffKeysAdd(frm)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class