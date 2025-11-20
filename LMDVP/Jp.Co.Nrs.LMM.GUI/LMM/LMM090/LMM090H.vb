' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM090H : 荷主マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports GrapeCity.Win.Editors
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMM090ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM090H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM090F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM090V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM090G

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMMControlG

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMMControlV

    ''' <summary>
    ''' 共通Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMMControlH

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索条件格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _InDs As DataSet

    ''' <summary>
    '''検索結果格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _OutDs As DataSet

    ''' <summary>
    '''表示用データテーブル格納
    ''' </summary>
    ''' <remarks></remarks>
    Private _DispDt As DataTable

    '要望番号:349 yamanaka 2012.07.10 Start
    ''' <summary>
    '''表示用荷主明細データテーブル格納(大)
    ''' </summary>
    ''' <remarks></remarks>
    Private _DispDtDtlL As DataTable

    ''' <summary>
    '''表示用荷主明細データテーブル格納(中)
    ''' </summary>
    ''' <remarks></remarks>
    Private _DispDtDtlM As DataTable

    ''' <summary>
    '''表示用荷主明細データテーブル格納(小)
    ''' </summary>
    ''' <remarks></remarks>
    Private _DispDtDtlS As DataTable

    ''' <summary>
    '''表示用荷主明細データテーブル格納(極小)
    ''' </summary>
    ''' <remarks></remarks>
    Private _DispDtDtlSS As DataTable
    '要望番号:349 yamanaka 2012.07.10 End

    ''' <summary>
    ''' 押下ボタンを判断するための変数
    ''' </summary>
    ''' <remarks></remarks>
    Private _ClickObject As LMM090C.ClickObject = LMM090C.ClickObject.INIT

    ''' <summary>
    ''' 編集ボタン押下時、入力チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private _BeforeEditLastCalcDate As String = String.Empty

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    '要望番号:349 yamanaka 2012.07.10 Start
    ''' <summary>
    ''' 複写ボタンを判断するための変数
    ''' </summary>
    ''' <remarks></remarks>
    Private _HukusyaObject As LMM090C.HukushaObject = LMM090C.HukushaObject.INIT

    ''' <summary>
    '''最大枝番格納フィールド(チェック時使用)
    ''' </summary>
    ''' <remarks></remarks>
    Private _MaxEdaNo As Integer

    ''' <summary>
    '''最大枝番格納フィールド(設定時使用)
    ''' </summary>
    ''' <remarks></remarks>
    Private _MaxEdaNoSet As Integer
    '要望番号:349 yamanaka 2012.07.10 End

    ''' <summary>
    ''' コンボボックス用データセット（製品セグメント）
    ''' </summary>
    Private _dsComboProductSeg As DataSet = Nothing

    ''' <summary>
    ''' 在庫存在フラグ
    ''' </summary>
    ''' <remarks>TRUE:存在する、FALSE;存在しない</remarks>
    Private _ExistZaikoFlg As Boolean = False

    ''' <summary>
    ''' 編集時、画面初期表示時点での最低保証摘要単位区分
    ''' </summary>
    ''' <remarks>在庫存在時の入力制限チェックに使用</remarks>
    Private _BeforeHoshoMinKbn As String = String.Empty

#End Region

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'フォームの作成
        Me._Frm = New LMM090F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMMControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMMControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMMControlH("LMM090", Me._ControlV, Me._ControlG)

        'Gamenクラスの設定
        Me._G = New LMM090G(Me, Me._Frm, Me._ControlG)

        'Validateクラスの設定
        Me._V = New LMM090V(Me, Me._Frm, Me._ControlV)

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(Me._Frm)
        '2015.10.15 英語化対応END

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(Me._Frm.cmbBr, Me._Frm.cmbSoko)

        Me.SelectComboItemCurrCd()

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '2011/08/11 福田 共通動作(右セル移動不可) スタート
        'Enter押下イベントの設定
        'Call Me._ControlH.SetEnterEvent(Me._Frm)
        '2011/08/11 福田 共通動作(右セル移動不可) エンド

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'コンボボックス用の値取得
        Me.GetComboData()

        'コンボボックス設定
        Call Me._G.SetComboControl(_dsComboProductSeg)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call Me._V.SetBaseMsg()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ClickObject)

        '外部倉庫用ABP対策
        Dim drABP As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'G203' AND KBN_NM1 = '", LM.Base.LMUserInfoManager.GetNrsBrCd, "'"))
        If drABP.Length > 0 Then
            '製品セグメントの必須を無効
            Me._Frm.cmbProductSegCd.HissuLabelVisible = False
            '真荷主の必須を無効
            Me._Frm.txtTcustBpCd.HissuLabelVisible = False
            Me._Frm.lblTcustBpNm.HissuLabelVisible = False
        End If

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ClickObject)

        'フォームの表示
        Me._Frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 新規処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NewDataEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM090C.EventShubetsu.SHINKI) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        '要望番号:349 yamanaka 2012.07.10 Start
        '枝番の設定
        Me._MaxEdaNo = -1
        Me._MaxEdaNoSet = -1
        '要望番号:349 yamanaka 2012.07.10 End

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        '変数初期化
        Me._ClickObject = LMM090C.ClickObject.INIT
        '要望番号:349 yamanaka 2012.07.26 Start
        Me._HukusyaObject = LMM090C.HukushaObject.INIT
        '要望番号:349 yamanaka 2012.07.26 End

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ClickObject)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ClickObject)

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDataEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM090C.EventShubetsu.HENSHU) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        '編集ボタン押下時チェック
        If Me._V.IsHenshuChk(Me._ClickObject) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Me._InDs = New LMM090DS()
        Call Me.SetDataSetHaitaChk()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditChk")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnds As DataSet = MyBase.CallWSA("LMM090BLF", "EditChk", Me._InDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditChk")

        '在庫存在フラグの設定
        Me._ExistZaikoFlg = False
        If CInt(rtnds.Tables("LMM090ZAIKO").Rows(0).Item("REC_CNT")) > 0 Then
            Me._ExistZaikoFlg = True
        End If

        'メッセージコードの判定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm) 'エラーメッセージ表示
            Call Me._ControlH.EndAction(Me._Frm) '終了処理
        Else

            '編集モード切り替え処理
            Select Case _ClickObject
                Case LMM090C.ClickObject.CUST_L
                    Call Me.ChangeEditMode(Me._Frm.sprCustDetailL)
                Case LMM090C.ClickObject.CUST_M
                    Call Me.ChangeEditMode(Me._Frm.sprCustDetailM)
                Case LMM090C.ClickObject.CUST_S
                    Call Me.ChangeEditMode(Me._Frm.sprCustDetailS)
                Case LMM090C.ClickObject.CUST_SS
                    Call Me.ChangeEditMode(Me._Frm.sprCustDetailSS)
                Case Else
                    '処理なし
            End Select

        End If

        '最低保証摘要単位区分の値を退避
        _BeforeHoshoMinKbn = Me._Frm.cmbHoshoMinKbn.SelectedValue.ToString()

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ClickObject)

    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM090C.EventShubetsu.FUKUSHA) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '複写ボタン押下時チェック
        If Me._V.IsFukushaChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '要望番号:349 yamanaka 2012.07.10 Start
        '枝番の設定
        Me._MaxEdaNo = -1
        Me._MaxEdaNoSet = -1
        '要望番号:349 yamanaka 2012.07.10 End

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.COPY_REC)

        '荷主別帳票マスタ情報をSpreadに設定
        Call Me._G.SetSpreadPrt(Me._DispDt, Me._ClickObject)

        '要望番号:349 yamanaka 2012.07.10 Start
        '荷主明細マスタ情報をSpreadに設定
        Select Case _ClickObject
            Case LMM090C.ClickObject.CUST_M
                Call Me._G.SetSpreadDtl(Me._DispDtDtlM, Me._Frm.sprCustDetailM)
                Call Me._G.SetSpreadDtl(Me._DispDtDtlS, Me._Frm.sprCustDetailS)
                Call Me._G.SetSpreadDtl(Me._DispDtDtlSS, Me._Frm.sprCustDetailSS)
            Case LMM090C.ClickObject.CUST_S
                Call Me._G.SetSpreadDtl(Me._DispDtDtlS, Me._Frm.sprCustDetailS)
                Call Me._G.SetSpreadDtl(Me._DispDtDtlSS, Me._Frm.sprCustDetailSS)
            Case LMM090C.ClickObject.CUST_SS
                Call Me._G.SetSpreadDtl(Me._DispDtDtlSS, Me._Frm.sprCustDetailSS)
            Case Else
                '処理なし
        End Select
        '要望番号:349 yamanaka 2012.07.10 End

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ClickObject)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ClickObject)

    End Sub

    ''' <summary>
    ''' 削除/復活処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM090C.EventShubetsu.SAKUJO_HUKKATU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '削除/復活ボタン押下時チェック
        If Me._V.IsSakujoHukkatuChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '処理続行メッセージ表示
        If Me.ConfirmMsg(LMM090C.EventShubetsu.SAKUJO_HUKKATU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM090DS()
        Call Me.SetDatasetDelData(ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '==========================
        'WSAクラス呼出
        '==========================
        MyBase.CallWSA("LMM090BLF", "DeleteData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        'キャッシュ最新化
        Call Me.GetNewCache()

        '処理完了メッセージ表示
        Call Me.SetCompleteMessage(LMM090C.EventShubetsu.SAKUJO_HUKKATU _
                                   , Me._Frm.txtCustCdL.TextValue _
                                   , Me._Frm.lblCustCdM.TextValue _
                                   , Me._Frm.lblCustCdS.TextValue _
                                   , Me._Frm.lblCustCdSS.TextValue
                                   )

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ClickObject)

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ClickObject)


    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectListEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM090C.EventShubetsu.KENSAKU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuInputChk = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
            If MyBase.ShowMessage(Me._Frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._V.SetBaseMsg() '基本メッセージの表示
                Call Me._ControlH.EndAction(Me._Frm) '終了処理　
                Exit Sub
            End If
        End If

        '検索処理を行う
        Call Me.SelectData()

        'START YANAI 要望番号544
        '変数初期化
        Me._ClickObject = LMM090C.ClickObject.INIT
        'END YANAI 要望番号544

        '要望番号:349 yamanaka 2012.07.26 Start
        Me._HukusyaObject = LMM090C.HukushaObject.INIT
        '要望番号:349 yamanaka 2012.07.26 End

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ClickObject)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent()

        '背景色クリア
        Me._ControlG.SetBackColor(Me._Frm)

        'カーソル位置の設定
        Dim objNm As String = Me._Frm.ActiveControl.Name()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM090C.EventShubetsu.MASTEROPEN) = False Then
            Exit Sub
        End If

        'カーソル位置チェック
        If Me._V.IsFocusChk(objNm, LMMControlC.MASTEROPEN) = False Then
            Exit Sub
        End If

        '処理開始アクション
        Call Me.StartAction()

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(objNm, LMM090C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me._ControlH.EndAction(Me._Frm) '終了処理　

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="eventShubetu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveEvent(ByVal eventShubetu As LMM090C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM090C.EventShubetsu.HOZON) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        '単項目/関連チェック
        If Me._V.IsSaveChk(MyBase.GetSystemDateTime(0), Me._ClickObject, Me._BeforeEditLastCalcDate) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        '最低保証摘要単位区分の変更可能チェック
        If _ExistZaikoFlg Then
            '在庫データが存在する場合
            Dim AfterHoshoMinKbn As String = Me._Frm.cmbHoshoMinKbn.SelectedValue.ToString()

            If ("04".Equals(_BeforeHoshoMinKbn)) AndAlso (Not "04".Equals(AfterHoshoMinKbn)) Then
                '商品入り目(ロット無し)からロット有りに変更された
                MyBase.ShowMessage(Me._Frm, "E375", New String() {"在庫が存在する", "最低保証摘要単位区分を商品入り目以外へは変更"})
                Call Me._ControlH.EndAction(Me._Frm) '終了処理　
                Return False

            ElseIf (Not "04".Equals(_BeforeHoshoMinKbn)) AndAlso ("04".Equals(AfterHoshoMinKbn)) Then
                'ロット有りから商品入目(ロット無し)に変更された
                MyBase.ShowMessage(Me._Frm, "E375", New String() {"在庫が存在する", "最低保証摘要単位区分を商品入り目へは変更"})
                Call Me._ControlH.EndAction(Me._Frm) '終了処理　
                Return False
            End If
        End If

        'マスタ存在チェック（サーバーアクセス）
        If Me.IsSaveExistMstServer() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM090DS()
        Call Me.SetDataSetSave(ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveData")

        '==========================
        'WSAクラス呼出
        '==========================
        If Me._Frm.lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) Then
            '更新処理
            ds = MyBase.CallWSA("LMM090BLF", "UpdateData", ds)
        Else
            '新規登録処理
            ds = MyBase.CallWSA("LMM090BLF", "InsertData", ds)
        End If

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            ''2011.09.08 検証結果_導入時要望№1対応 START
            'If MyBase.GetMessageID.Equals("E079") Then
            '    Me._ControlV.SetErrorControl(Me._Frm.txtZipNo, Me._Frm.tabCust, Me._Frm.tpgCustM)
            'End If
            'MyBase.ShowMessage(Me._Frm)
            'Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            'Return False
            ''2011.09.08 検証結果_導入時要望№1対応 END
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveData")

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'キャッシュ最新化
        Call Me.GetNewCache()

        '処理完了メッセージ表示
        Dim cdM As String = Me._Frm.lblCustCdM.TextValue
        Dim cdS As String = Me._Frm.lblCustCdS.TextValue
        Dim cdSS As String = Me._Frm.lblCustCdSS.TextValue
        If ds.Tables(LMM090C.TABLE_NM_CUST).Rows.Count > 0 Then
            Dim dr As DataRow = ds.Tables(LMM090C.TABLE_NM_CUST).Rows(0)
            cdM = dr.Item("CUST_CD_M").ToString()
            cdS = dr.Item("CUST_CD_S").ToString()
            cdSS = dr.Item("CUST_CD_SS").ToString()
        End If

        Call Me.SetCompleteMessage(LMM090C.EventShubetsu.HOZON _
                                   , Me._Frm.txtCustCdL.TextValue _
                                   , cdM _
                                   , cdS _
                                   , cdSS
                                   )

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'START YANAI 要望番号544
        '変数初期化
        Me._ClickObject = LMM090C.ClickObject.INIT
        'END YANAI 要望番号544

        '要望番号:349 yamanaka 2012.07.26 Start
        Me._HukusyaObject = LMM090C.HukushaObject.INIT
        '要望番号:349 yamanaka 2012.07.26 End

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ClickObject)

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ClickObject)

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(Me._Frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveEvent(LMM090C.EventShubetsu.TOJIRU) = False Then

                    e.Cancel = True

                End If


            Case MsgBoxResult.Cancel                  '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

    ''' <summary>
    '''  Spreadダブルクリック処理
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SprCellSelect(ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(Me._Frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._ControlH.EndAction(Me._Frm)  '終了処理
                Exit Sub
            End If
        End If

        Call Me.RowSelection(e.Row)

    End Sub

    ''' <summary>
    ''' 行追加処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RowAdd()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM090C.EventShubetsu.ADD_ROW) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsAddRowChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '行追加処理を行う
        Call Me._G.AddRow()

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RowDel()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM090C.EventShubetsu.DEL_ROW) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(Me._Frm.sprCustPrt.ActiveSheet, LMM090G.sprCustPrtDef.DEF.ColNo)

        '単項目/関連チェック
        If Me._V.IsDelRowChk(list) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェック行削除
        Call Me._G.DelateDtl(list)

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    '要望番号:349 yamanaka 2012.07.10 Start
    ''' <summary>
    ''' 荷主明細用行追加処理
    ''' </summary>
    ''' <param name="trgSpr">対象スプレッドを設定</param>
    ''' <remarks></remarks>
    Private Sub DetailRowAdd(ByVal trgSpr As LMM090C.SprDetailObject)

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM090C.EventShubetsu.ADD_ROW) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '空行・最大枝番チェック
        Select Case trgSpr
            Case LMM090C.SprDetailObject.CUST_L
                If Me._V.IsDetailAddRowChk(Me._MaxEdaNo, Me._Frm.sprCustDetailL) = False Then
                    Call Me._ControlH.EndAction(Me._Frm) '終了処理　
                    Exit Sub
                End If
            Case LMM090C.SprDetailObject.CUST_M
                If Me._V.IsDetailAddRowChk(Me._MaxEdaNo, Me._Frm.sprCustDetailM) = False Then
                    Call Me._ControlH.EndAction(Me._Frm) '終了処理　
                    Exit Sub
                End If
            Case LMM090C.SprDetailObject.CUST_S
                If Me._V.IsDetailAddRowChk(Me._MaxEdaNo, Me._Frm.sprCustDetailS) = False Then
                    Call Me._ControlH.EndAction(Me._Frm) '終了処理　
                    Exit Sub
                End If
            Case LMM090C.SprDetailObject.CUST_SS
                If Me._V.IsDetailAddRowChk(Me._MaxEdaNo, Me._Frm.sprCustDetailSS) = False Then
                    Call Me._ControlH.EndAction(Me._Frm) '終了処理　
                    Exit Sub
                End If
            Case Else
                '処理なし
        End Select


        '採番データを採番用に編集する。
        Me._MaxEdaNo = Me._MaxEdaNo + 1

        '行追加処理を行う
        Select Case trgSpr
            Case LMM090C.SprDetailObject.CUST_L
                Call Me._G.DetailAddRow(Me._MaxEdaNo, Me._Frm.sprCustDetailL)
            Case LMM090C.SprDetailObject.CUST_M
                Call Me._G.DetailAddRow(Me._MaxEdaNo, Me._Frm.sprCustDetailM)
            Case LMM090C.SprDetailObject.CUST_S
                Call Me._G.DetailAddRow(Me._MaxEdaNo, Me._Frm.sprCustDetailS)
            Case LMM090C.SprDetailObject.CUST_SS
                Call Me._G.DetailAddRow(Me._MaxEdaNo, Me._Frm.sprCustDetailSS)
            Case Else
                '処理なし
        End Select

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 荷主明細用行削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DetailRowDel(ByVal trgSpr As LMM090C.SprDetailObject)

        Dim list As ArrayList = New ArrayList

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM090C.EventShubetsu.DEL_ROW) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Select Case trgSpr
            Case LMM090C.SprDetailObject.CUST_L
                list = Me._ControlH.GetCheckList(Me._Frm.sprCustDetailL.ActiveSheet, LMM090G.sprCustDetailL.DEF.ColNo)
            Case LMM090C.SprDetailObject.CUST_M
                list = Me._ControlH.GetCheckList(Me._Frm.sprCustDetailM.ActiveSheet, LMM090G.sprCustDetailM.DEF.ColNo)
            Case LMM090C.SprDetailObject.CUST_S
                list = Me._ControlH.GetCheckList(Me._Frm.sprCustDetailS.ActiveSheet, LMM090G.sprCustDetailS.DEF.ColNo)
            Case LMM090C.SprDetailObject.CUST_SS
                list = Me._ControlH.GetCheckList(Me._Frm.sprCustDetailSS.ActiveSheet, LMM090G.sprCustDetailSS.DEF.ColNo)
            Case Else
                '処理なし
        End Select

        '単項目/関連チェック
        If Me._V.IsDelRowChk(list) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェック行削除
        Select Case trgSpr
            Case LMM090C.SprDetailObject.CUST_L
                Call Me._G.DetailDelate(list, Me._Frm.sprCustDetailL)
            Case LMM090C.SprDetailObject.CUST_M
                Call Me._G.DetailDelate(list, Me._Frm.sprCustDetailM)
            Case LMM090C.SprDetailObject.CUST_S
                Call Me._G.DetailDelate(list, Me._Frm.sprCustDetailS)
            Case LMM090C.SprDetailObject.CUST_SS
                Call Me._G.DetailDelate(list, Me._Frm.sprCustDetailSS)
            Case Else
                '処理なし
        End Select


        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub
    '要望番号:349 yamanaka 2012.07.10 End

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal e As System.Windows.Forms.KeyEventArgs)

        With Me._Frm

            'カーソル位置の設定
            Dim objNm As String = .ActiveControl.Name()

            '権限チェック
            If Me._V.IsAuthorityChk(LMM090C.EventShubetsu.ENTER) = False Then
                Call Me._ControlH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
                Exit Sub
            End If

            'カーソル位置チェック
            If Me._V.IsFocusChk(objNm, LMMControlC.ENTER) = False Then
                Call Me._ControlH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
                Exit Sub
            End If

            '処理開始アクション
            Call Me.StartAction()

            'Pop起動処理：１件時表示なし
            Me._PopupSkipFlg = False
            Call Me.ShowPopupControl(objNm, LMM090C.EventShubetsu.ENTER)

            '処理終了アクション
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　

            'メッセージエリアの設定
            Call Me._V.SetBaseMsg()

            'フォーカス移動処理
            Call Me._ControlH.NextFocusedControl(Me._Frm, True)

        End With

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeaveイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprFindLeaveCell(ByVal frm As LMM090F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        '編集モードの場合、処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = True Then
            Exit Sub
        End If

        Dim rowNo As Integer = e.NewRow
        If rowNo < 1 Then
            Exit Sub
        End If

        '同じ行の場合、スルー
        If e.Row = rowNo Then
            Exit Sub
        End If

        Call Me.RowSelection(rowNo)

    End Sub

    ''' <summary>
    ''' スプレッドのセルLeave処理
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LeaveSprCell(ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        '参照場合、スルー
        If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then
            Exit Sub
        End If

        '行がない場合、スルー
        If Me._Frm.sprCustPrt.ActiveSheet.Rows.Count < 1 Then
            Exit Sub
        End If

        'スプレッドが使用不可の場合、処理終了
        If Me._Frm.sprCustPrt.ActiveSheet.Cells(e.Row, e.Column).Locked = True Then
            Exit Sub
        End If

        'Leaveセルが帳票種類ID以外の場合、処理終了
        If e.Column.Equals(LMM090G.sprCustPrtDef.PTN_ID.ColNo) = False Then
            Exit Sub
        End If

        '処理開始アクション
        Call Me.StartAction()

        'スプレッドの内容を保持
        If Me._DispDt IsNot Nothing Then
            Me._DispDt.Rows.Clear()
        Else
            Dim ds As DataSet = New LMM090DS
            Me._DispDt = ds.Tables(LMM090C.TABLE_NM_CUST_PRT)
        End If

        With Me._Frm.sprCustPrt.ActiveSheet
            Dim max As Integer = .Rows.Count - 1
            Dim dr As DataRow = Nothing
            For i As Integer = 0 To max
                dr = Me._DispDt.NewRow()
                dr.Item("PTN_ID") = Me._ControlG.GetCellValue(.Cells(i, LMM090G.sprCustPrtDef.PTN_ID.ColNo))
                dr.Item("PTN_CD") = Me._ControlG.GetCellValue(.Cells(i, LMM090G.sprCustPrtDef.PTN_NM.ColNo))
                Me._DispDt.Rows.Add(dr)
            Next

        End With


        'スプレッド貼りなおし
        Me._G.SetSpreadPrt(Me._DispDt, Me._ClickObject)

        '処理終了アクション
        Call Me._ControlH.EndAction(Me._Frm) '終了処理　

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StartAction()

        'マスタメンテ共通処理
        Me._ControlH.StartAction(Me._Frm)

        '背景色クリア
        Me._ControlG.SetBackColor(Me._Frm)

    End Sub

    ''' <summary>
    ''' 編集処理切り替え処理
    ''' </summary>
    ''' <param name="spr">Spread</param>
    ''' <remarks></remarks>
    Private Sub ChangeEditMode(ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread)

        '要望番号:349 yamanaka 2012.07.10 Start
        '最大枝番の設定
        Me._MaxEdaNo = -1
        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
        Dim edaNo As Integer = 0
        For i As Integer = 0 To max
            edaNo = Convert.ToInt32(Me._ControlV.GetCellValue(spr.ActiveSheet.Cells(i, LMM090C.sprCustDetailColumnIndex.EDA_NO)))
            If Me._MaxEdaNo < edaNo Then
                Me._MaxEdaNo = edaNo
            End If
        Next
        Me._MaxEdaNoSet = Me._MaxEdaNo
        '要望番号:349 yamanaka 2012.07.10 End

        '保存時入力チェック用値保持
        Me._BeforeEditLastCalcDate = Me._Frm.imdLastCalc.TextValue

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ClickObject)

        '荷主別帳票マスタ情報表示設定
        If Me._ClickObject.Equals(LMM090C.ClickObject.CUST_S) Then
            Call Me._G.SetSpreadPrt(Me._DispDt, Me._ClickObject)
        End If

        '要望番号:349 yamanaka 2012.07.10 Start
        '商品明細マスタ情報表示設定
        Select Case _ClickObject
            Case LMM090C.ClickObject.CUST_L
                Call Me._G.SetSpreadDtl(Me._DispDtDtlL, Me._Frm.sprCustDetailL)
            Case LMM090C.ClickObject.CUST_M
                Call Me._G.SetSpreadDtl(Me._DispDtDtlM, Me._Frm.sprCustDetailM)
            Case LMM090C.ClickObject.CUST_S
                Call Me._G.SetSpreadDtl(Me._DispDtDtlS, Me._Frm.sprCustDetailS)
            Case LMM090C.ClickObject.CUST_SS
                Call Me._G.SetSpreadDtl(Me._DispDtDtlSS, Me._Frm.sprCustDetailSS)
            Case Else
                '処理なし
        End Select
        '要望番号:349 yamanaka 2012.07.10 End

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectData()

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble(
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))

        MyBase.SetLimitCount(lc)

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble(
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1")))

        MyBase.SetMaxResultCount(mc)

        'DataSet設定
        Me._InDs = New LMM090DS()
        Call SetDataSetInData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(Me._Frm, "LMM090BLF", "SelectListData", Me._InDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(Me._Frm _
        '                                                , "LMM090BLF" _
        '                                                , "SelectListData" _
        '                                                , Me._InDs _
        '                                                , Me._ControlH.GetLimitCount() _
        '                                                 , , _
        '                                                 Convert.ToInt32(Convert.ToDouble( _
        '                                                 MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
        '                                                .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables(LMM090C.TABLE_NM_CUST).Rows.Count > 0 Then
            '検索成功時共通処理を行う
            Call Me.SuccessSelect(rtnDs)
        ElseIf rtnDs IsNot Nothing Then
            '取得件数が0件の場合
            Call Me.FailureSelect(rtnDs)
        End If

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprCust.CrearSpread()

        '取得データをSPREADに表示
        Dim dt As DataTable = Me._OutDs.Tables(LMM090C.TABLE_NM_CUST)
        Call Me._G.SetSpread(dt)

        '取得件数設定
        Me._CntSelect = dt.Rows.Count.ToString()

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G008", New String() {Me._CntSelect})

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ClickObject)

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理(検索結果が0件の場合))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprCust.CrearSpread()

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ClickObject)

    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal rowNo As Integer)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM090C.EventShubetsu.DOUBLE_CLICK) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._ControlV.GetCellValue(Me._Frm.sprCust.ActiveSheet.Cells(rowNo, LMM090G.sprCustDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ClickObject)

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo, Me._OutDs.Tables(LMM090C.TABLE_NM_TARIFF))

        '荷主別帳票マスタ情報をSpreadに設定
        Call Me.GetCustPrtDisplayData()
        Call Me._G.SetSpreadPrt(Me._DispDt, Me._ClickObject)

        '要望番号:349 yamanaka 2012.07.12 Start
        '荷主明細マスタ情報をSpread大に設定
        Call Me.GetCustDetailLDisplayData(LMM090C.SprDetailObject.CUST_L, Me._DispDtDtlL)
        Call Me._G.SetSpreadDtl(Me._DispDtDtlL, Me._Frm.sprCustDetailL)

        '荷主明細マスタ情報をSpread中に設定
        Call Me.GetCustDetailLDisplayData(LMM090C.SprDetailObject.CUST_M, Me._DispDtDtlM)
        Call Me._G.SetSpreadDtl(Me._DispDtDtlM, Me._Frm.sprCustDetailM)

        '荷主明細マスタ情報をSpread小に設定
        Call Me.GetCustDetailLDisplayData(LMM090C.SprDetailObject.CUST_S, Me._DispDtDtlS)
        Call Me._G.SetSpreadDtl(Me._DispDtDtlS, Me._Frm.sprCustDetailS)

        '荷主明細マスタ情報をSpread極小に設定
        Call Me.GetCustDetailLDisplayData(LMM090C.SprDetailObject.CUST_SS, Me._DispDtDtlSS)
        Call Me._G.SetSpreadDtl(Me._DispDtDtlSS, Me._Frm.sprCustDetailSS)
        '要望番号:349 yamanaka 2012.07.12 End

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G013")

    End Sub

    ''' <summary>
    ''' 荷主別帳票マスタSpread表示用にDataTaleを編集
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetCustPrtDisplayData()

        With Me._Frm

            Dim dt As DataTable = Me._OutDs.Tables(LMM090C.TABLE_NM_CUST_PRT)

            '表示対象データを取得
            Me._DispDt = Nothing
            Dim filter As String = String.Empty
            filter = String.Concat(filter, "NRS_BR_CD = '", .cmbBr.SelectedValue, "'")
            filter = String.Concat(filter, " AND CUST_CD_L = '", .txtCustCdL.TextValue, "'")
            filter = String.Concat(filter, " AND CUST_CD_M = '", .lblCustCdM.TextValue, "'")
            filter = String.Concat(filter, " AND CUST_CD_S = '", .lblCustCdS.TextValue, "'")
            Dim orderBy As String = "PTN_ID"
            Dim selectDr As DataRow() = dt.Select(filter, orderBy)
            Dim max As Integer = selectDr.Length - 1
            Dim setDS As DataSet = New LMM090DS()
            Me._DispDt = setDS.Tables(LMM090C.TABLE_NM_CUST_PRT)

            For i As Integer = 0 To max
                Me._DispDt.ImportRow(selectDr(i))
            Next

        End With

    End Sub

    '要望番号:349 yamanaka 2012.07.12 Start
    ''' <summary>
    ''' 荷主明細マスタSpread表示用にDataTaleを編集(共通)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetCustDetailLDisplayData(ByVal trgSpr As LMM090C.SprDetailObject, ByRef DispDtDtl As DataTable)

        With Me._Frm

            Dim dt As DataTable = Me._OutDs.Tables(LMM090C.TABLE_NM_CUST_DETAILS)

            '表示対象データを取得
            DispDtDtl = Nothing
            Dim filter As String = String.Empty
            filter = String.Concat(filter, "NRS_BR_CD = '", .cmbBr.SelectedValue, "'")
            Select Case trgSpr
                Case LMM090C.SprDetailObject.CUST_L
                    filter = String.Concat(filter, " AND CUST_CD = '", .txtCustCdL.TextValue, "'")
                Case LMM090C.SprDetailObject.CUST_M
                    filter = String.Concat(filter, " AND CUST_CD = '", .txtCustCdL.TextValue, .lblCustCdM.TextValue, "'")
                Case LMM090C.SprDetailObject.CUST_S
                    filter = String.Concat(filter, " AND CUST_CD = '", .txtCustCdL.TextValue, .lblCustCdM.TextValue, .lblCustCdS.TextValue, "'")
                Case LMM090C.SprDetailObject.CUST_SS
                    filter = String.Concat(filter, " AND CUST_CD = '", .txtCustCdL.TextValue, .lblCustCdM.TextValue, .lblCustCdS.TextValue, .lblCustCdSS.TextValue, "'")
                Case Else
                    '処理なし
            End Select
            Dim orderBy As String = "CUST_CD_EDA"
            Dim selectDr As DataRow() = dt.Select(filter, orderBy)
            Dim max As Integer = selectDr.Length - 1
            Dim setDS As DataSet = New LMM090DS()
            DispDtDtl = setDS.Tables(LMM090C.TABLE_NM_CUST_DETAILS)

            For i As Integer = 0 To max
                DispDtDtl.ImportRow(selectDr(i))
            Next

        End With

    End Sub
    '要望番号:349 yamanaka 2012.07.12 End

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmMsg(ByVal eventShubetu As LMM090C.EventShubetsu) As Boolean

        Select Case eventShubetu
            Case LMM090C.EventShubetsu.SAKUJO_HUKKATU
                '処理続行メッセージ表示
                Dim msg As String = String.Empty

                '2016.01.06 UMANO 英語化対応START
                Dim str As String() = Split(Me._Frm.FunctionKey.F4ButtonName, "･")
                '2016.01.06 UMANO 英語化対応END

                Select Case Me._Frm.lblSituation.RecordStatus
                    Case RecordStatus.DELETE_REC
                        '2016.01.06 UMANO 英語化対応START
                        'msg = "復活"
                        msg = str(1)
                    Case RecordStatus.NOMAL_REC
                        'msg = "削除"
                        msg = str(0)
                        '2016.01.06 UMANO 英語化対応END
                End Select
                If MyBase.ShowMessage(Me._Frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
                    Call Me._V.SetBaseMsg() 'メッセージエリアの設定
                    Exit Function
                End If

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 処理完了メッセージ
    ''' </summary>
    ''' <param name="eventShubetu">イベント種別</param>
    ''' <param name="cdL">荷主コード(大)</param>
    ''' <param name="cdM">荷主コード(中)</param>
    ''' <param name="cdS">荷主コード(小)</param>
    ''' <param name="cdSS">荷主コード(極小)</param>
    ''' <remarks></remarks>
    Private Sub SetCompleteMessage(ByVal eventShubetu As LMM090C.EventShubetsu _
                                   , ByVal cdL As String _
                                   , ByVal cdM As String _
                                   , ByVal cdS As String _
                                   , ByVal cdSS As String
                                   )

        With Me._Frm

            '置換文字設定変数
            Dim msgL As String = cdL
            Dim msgLM As String = String.Concat(msgL, "-", cdM)
            Dim msgLMS As String = String.Concat(msgLM, "-", cdS)
            Dim msgLMSSS As String = String.Concat(msgLMS, "-", cdSS)
            '2016.01.06 UMANO 英語化対応START
            'msgL = String.Concat("[荷主コード = ", msgL, "]")
            'msgLM = String.Concat("[荷主コード = ", msgLM, "]")
            'msgLMS = String.Concat("[荷主コード = ", msgLMS, "]")
            'msgLMSSS = String.Concat("[荷主コード = ", msgLMSSS, "]")
            '2016.01.06 UMANO 英語化対応END

            Select Case eventShubetu
                Case LMM090C.EventShubetsu.SAKUJO_HUKKATU

                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage(Me._Frm, "G002", New String() {"削除・復活", msgLMSSS})
                    MyBase.ShowMessage(Me._Frm, "G002", New String() { .FunctionKey.F4ButtonName, msgLMSSS})
                    '2016.01.06 UMANO 英語化対応END

                Case LMM090C.EventShubetsu.HOZON

                    '設定置換文字列設定変数
                    Dim saveMsg As String = msgLMSSS

                    '編集時、編集対象によりメッセージを分ける
                    If .lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) Then
                        Select Case Me._ClickObject
                            Case LMM090C.ClickObject.CUST_L
                                saveMsg = msgL
                            Case LMM090C.ClickObject.CUST_M
                                saveMsg = msgLM
                            Case LMM090C.ClickObject.CUST_S
                                saveMsg = msgLMS
                            Case LMM090C.ClickObject.CUST_SS
                                saveMsg = msgLMSSS
                        End Select
                    End If

                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage(Me._Frm, "G002", New String() {"保存", saveMsg})
                    MyBase.ShowMessage(Me._Frm, "G082", New String() { .FunctionKey.F11ButtonName, saveMsg})
                    '2016.01.06 UMANO 英語化対応END

            End Select

        End With

    End Sub

    ''' <summary>
    ''' キャッシュ最新化処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetNewCache()

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.CUST)
        'MyBase.LMCacheMasterData(LMConst.CacheTBL.CUST_RPT)
        MyBase.LMCacheMasterData(LMConst.CacheTBL.UNCHIN_TARIFF_SET)

    End Sub

    ''' <summary>
    ''' 保存押下時マスタ存在チェック（サーバーアクセス）
    ''' </summary>
    ''' <returns></returns>
    Private Function IsSaveExistMstServer() As Boolean

        Dim rtnResult As Boolean = True
        Dim ds As DataSet = New LMM090DS()

        With Me._Frm
            Dim tpg As System.Windows.Forms.TabPage = .tpgCustL

            '******************** 荷主コード(中)タブ ********************
            tpg = .tpgCustM

            '最低保証適用単位区分
            If .cmbHoshoMinKbn.SelectedValue.ToString() <> "03" Then
                '選択値が「商品ロット入り目入荷毎に分割」以外の場合
                Dim dsVar As DataSet = New LMM090DS()
                Dim drVar As DataRow = dsVar.Tables(LMM090C.TABLE_NM_VAR_STRAGE).NewRow()
                drVar("NRS_BR_CD") = .cmbBr.SelectedValue
                drVar("CUST_CD_L") = .txtCustCdL.TextValue
                drVar("CUST_CD_M") = .lblCustCdM.TextValue
                dsVar.Tables(LMM090C.TABLE_NM_VAR_STRAGE).Rows.Add(drVar)

                dsVar = Me.CallWSA("LMM090BLF", "SelectVarStrage", dsVar)

                If dsVar.Tables(LMM090C.TABLE_NM_VAR_STRAGE).Rows.Count > 0 Then
                    Call Me._ControlV.SetErrorControl(.cmbHoshoMinKbn, .tabCust, tpg)
                    MyBase.ShowMessage(Me._Frm, "E02O")
                    Return False
                End If
            End If

            '******************** 荷主コード(小)タブ ********************
            tpg = .tpgCustS

            '真荷主
            .lblTcustBpNm.TextValue = String.Empty
            If Not String.IsNullOrEmpty(.txtTcustBpCd.TextValue) Then
                If Not Me.SetReturnTcustPop(.txtTcustBpCd.Name, LMM090C.EventShubetsu.HOZON) Then
                    Call Me._ControlV.SetMstErrMessage("真荷主", .txtTcustBpCd.TextValue)
                    Call Me._ControlV.SetErrorControl(.txtTcustBpCd, .tabCust, tpg)
                    Return False
                End If
            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' コンボボックス用の値取得
    ''' </summary>
    Private Sub GetComboData()

        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)

        _dsComboProductSeg = New LMM090DS()

        '製品セグメント取得
        Dim dt As DataTable = _dsComboProductSeg.Tables("LMM090COMBO_SEIHINA")
        Dim dr As DataRow = dt.NewRow()
        dr.Item("KBN_LANG") = If("0".Equals(lgm.MessageLanguage()), "ja", "en")
        dt.Rows.Add(dr)
        _dsComboProductSeg = MyBase.CallWSA("LMM090BLF", "SelectComboSeihin", _dsComboProductSeg)

    End Sub

    'START YANAI 要望番号824
#Region "請求先マスタ"

    ''' <summary>
    ''' 担当者名取得
    ''' </summary>
    ''' <param name="EventShubetsu">イベント種別</param>
    ''' <remarks></remarks>
    Private Function SetReturnTanto(ByVal EventShubetsu As LMM090C.EventShubetsu) As Boolean

        If (LMM090C.EventShubetsu.ENTER).Equals(EventShubetsu) = False Then
            Return True
        End If

        With Me._Frm

            Dim userDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", .txtTantoCd.TextValue, "'"))

            .lblTantoNm.TextValue = String.Empty
            If 0 < userDr.Length Then
                .lblTantoNm.TextValue = userDr(0).Item("USER_NM").ToString
            End If

        End With

        Return True

    End Function

#End Region
    'END YANAI 要望番号824

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal objNm As String, ByVal eventShubetu As LMM090C.EventShubetsu) As Boolean

        With Me._Frm

            Select Case objNm
                Case .txtMainCustCd.Name

                    '荷主マスタ参照POP起動
                    Call Me.SetReturnCustPop(objNm, eventShubetu)

                Case .txtSampleSagyoCd.Name

                    '作業項目マスタ参照POP起動
                    Call Me.SetReturnSagyoKmkPop(objNm, eventShubetu)

                Case .txtZipNo.Name

                    '郵便番号マスタ参照POP起動
                    Call Me.SetReturnZipPop(objNm, eventShubetu)

                Case .txtCustKyoriCd.Name

                    '距離程マスタ参照POP起動
                    Call Me.SetReturnKyoriPop(objNm, eventShubetu)

                Case .txtUnchinTarifTonNyuka.Name _
                     , .txtUnchinTarifTonShukka.Name _
                     , .txtUnchinTarifShadateNyuka.Name _
                     , .txtUnchinTarifShadateShukka.Name

                    '運賃タリフマスタ参照POP起動
                    Call Me.SetReturnUnchinTariffPop(objNm, eventShubetu)

                Case .txtWarimashiTarifNyuka.Name _
                     , .txtWarimashiTarifShukka.Name

                    '割増運賃タリフマスタ参照POP起動
                    Call Me.SetReturnExtcTariffPop(objNm, eventShubetu)

                Case .txtYokomochiTarifNyuka.Name _
                     , .txtYokomochiTarifShukka.Name

                    '横持ちタリフマスタ参照POP起動
                    Call Me.SetReturnYokoTariffPop(objNm, eventShubetu)

                Case .txtShiteiUnsoCompCd.Name _
                    , .txtShiteiUnsoShitenCd.Name

                    '運送会社マスタ参照POP起動
                    Call Me.SetReturnUnsoPop(eventShubetu)

                    'ADD Start 2018/10/25 要望番号001820
                Case .txtInkaOrigCd.Name

                    '届先マスタ参照POP起動
                    Call Me.SetReturnInkaOrigPop(objNm, eventShubetu)
                    'ADD End   2018/10/25 要望番号001820

                Case .txtSeiqCd.Name _
                     , .txtHokanSeiqCd.Name _
                     , .txtNiyakuSeiqCd.Name _
                     , .txtUnchinSeiqCd.Name _
                     , .txtSagyoSeiqCd.Name

                    '請求先マスタ参照POP起動
                    Call Me.SetReturnUnchinSeiToPop(objNm, eventShubetu)

                    'START YANAI 要望番号824
                Case .txtTantoCd.Name
                    '
                    'ユーザーキャッシュ
                    Call Me.SetReturnTanto(eventShubetu)

                    'END YANAI 要望番号824

                Case .txtTcustBpCd.Name

                    '真荷主参照POP起動
                    Call Me.SetReturnTcustPop(objNm, eventShubetu)

            End Select

        End With

        Return True

    End Function

#Region "荷主マスタ"

    ''' <summary>
    ''' 荷主マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal objNm As String, ByVal eventShubetu As LMM090C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowCustPopup(ctl, eventShubetu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
            With Me._Frm
                ctl.TextValue = dr.Item("CUST_CD_L").ToString()
                .lblMainCustNm.TextValue = dr.Item("CUST_NM_L").ToString()
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal EventShubetsu As LMM090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If EventShubetsu = LMM090C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF 'キャッシュ検索
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_M   '検証結果(メモ)№77対応(2011.09.12)

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "作業項目マスタ"

    ''' <summary>
    ''' 作業項目マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSagyoKmkPop(ByVal objNm As String, ByVal EventShubetsu As LMM090C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowSagyoKmkPopup(ctl, EventShubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ200C.TABLE_NM_OUT).Rows(0)
            With Me._Frm
                ctl.TextValue = dr.Item("SAGYO_CD").ToString()
                .lblSampleSagyoNm.TextValue = dr.Item("SAGYO_NM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 作業項目マスタ参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowSagyoKmkPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal EventShubetsu As LMM090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ200DS()
        Dim dt As DataTable = ds.Tables(LMZ200C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            .Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
            'START SHINOHARA 要望番号513
            If EventShubetsu = LMM090C.EventShubetsu.ENTER Then
                .Item("SAGYO_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SAGYO_CNT") = LMConst.FLG.ON

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ200", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "郵便番号マスタ"

    ''' <summary>
    ''' 郵便番号Popの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnZipPop(ByVal objNm As String, ByVal EventShubetsu As LMM090C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowZipPopup(ctl, EventShubetsu)

        If prm.ReturnFlg = True Then

            'PopUpから取得したデータをコントロールにセット
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ060C.TABLE_NM_OUT).Rows(0)

            '住所1(都道府県+市区町村名)
            Dim add1 As String = String.Concat(dr.Item("KEN_N").ToString(), dr.Item("CITY_N").ToString())

            With Me._Frm
                ctl.TextValue = dr.Item("ZIP_NO").ToString()         '郵便番号

                If String.IsNullOrEmpty(.txtAdd1.TextValue) _
                AndAlso String.IsNullOrEmpty(.txtAdd2.TextValue) Then
                    .txtAdd1.TextValue = add1
                    .txtAdd2.TextValue = dr.Item("TOWN_N").ToString    '住所2(町域名
                End If
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 郵便番号マスタ参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowZipPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal EventShubetsu As LMM090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ060DS()
        Dim dt As DataTable = ds.Tables(LMZ060C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            'START SHINOHARA 要望番号513
            If EventShubetsu = LMM090C.EventShubetsu.ENTER Then
                .Item("ZIP_NO") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ060", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "距離程マスタ"

    ''' <summary>
    ''' 距離程Popの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnKyoriPop(ByVal objNm As String, ByVal EventShubetsu As LMM090C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowKyoriPopup(ctl, EventShubetsu)

        If prm.ReturnFlg = True Then
            '距離程マスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ080C.TABLE_NM_OUT).Rows(0)
            With Me._Frm
                ctl.TextValue = dr.Item("KYORI_CD").ToString()
                .lblCustKyoriNm.TextValue = dr.Item("KYORI_REM").ToString()
            End With
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 距離程マスタ参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowKyoriPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal EventShubetsu As LMM090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ080DS()
        Dim dt As DataTable = ds.Tables(LMZ080C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue
            'START SHINOHARA 要望番号513
            If EventShubetsu = LMM090C.EventShubetsu.ENTER Then
                .Item("KYORI_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ080", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "運賃タリフマスタ"

    ''' <summary>
    ''' 運賃タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnchinTariffPop(ByVal objNm As String, ByVal EventShubetsu As LMM090C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowUnchinTariffPopup(ctl, EventShubetsu)

        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ230C.TABLE_NM_OUT).Rows(0)

            With Me._Frm

                ctl.TextValue = dr.Item("UNCHIN_TARIFF_CD").ToString()

                Select Case objNm
                    Case .txtUnchinTarifTonNyuka.Name
                        .lblUnchinTarifTonNyuka.TextValue = dr.Item("UNCHIN_TARIFF_REM").ToString()
                    Case .txtUnchinTarifTonShukka.Name
                        .lblUnchinTarifTonShukka.TextValue = dr.Item("UNCHIN_TARIFF_REM").ToString()
                    Case .txtUnchinTarifShadateNyuka.Name
                        .lblUnchinTarifShadateNyuka.TextValue = dr.Item("UNCHIN_TARIFF_REM").ToString()
                    Case .txtUnchinTarifShadateShukka.Name
                        .lblUnchinTarifShadateShukka.TextValue = dr.Item("UNCHIN_TARIFF_REM").ToString()
                End Select

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ参照POP起動
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowUnchinTariffPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal EventShubetsu As LMM090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ230DS()
        Dim dt As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If EventShubetsu = LMM090C.EventShubetsu.ENTER Then
                .Item("UNCHIN_TARIFF_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("STR_DATE") = MyBase.GetSystemDateTime(0)
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF   'キャッシュ検索

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ230", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "割増運賃タリフマスタ"

    ''' <summary>
    ''' 割増運賃タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnExtcTariffPop(ByVal objNm As String, ByVal EventShubetsu As LMM090C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowExtcTariffPopup(ctl, EventShubetsu)

        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ240C.TABLE_NM_OUT).Rows(0)

            With Me._Frm
                ctl.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()

                Select Case objNm
                    Case .txtWarimashiTarifNyuka.Name
                        .lblWarimashiTarifNyuka.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()
                    Case .txtWarimashiTarifShukka.Name
                        .lblWarimashiTarifShukka.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()
                End Select

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 割増運賃タリフマスタ参照POP起動
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowExtcTariffPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal EventShubetsu As LMM090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ240DS()
        Dim dt As DataTable = ds.Tables(LMZ240C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If EventShubetsu = LMM090C.EventShubetsu.ENTER Then
                .Item("EXTC_TARIFF_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF  'キャッシュ検索

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ240", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "横持ちタリフマスタ"

    ''' <summary>
    ''' 横持ちタリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnYokoTariffPop(ByVal objNm As String, ByVal EventShubetsu As LMM090C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowYokoTariffPopup(ctl, EventShubetsu)

        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ100C.TABLE_NM_OUT).Rows(0)

            With Me._Frm
                ctl.TextValue = dr.Item("YOKO_TARIFF_CD").ToString()
                Select Case objNm
                    Case .txtYokomochiTarifNyuka.Name
                        .lblYokomochiTarifNyuka.TextValue = dr.Item("YOKO_REM").ToString()
                    Case .txtYokomochiTarifShukka.Name
                        .lblYokomochiTarifShukka.TextValue = dr.Item("YOKO_REM").ToString()
                End Select

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ参照POP起動
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowYokoTariffPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal EventShubetsu As LMM090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ100DS()
        Dim dt As DataTable = ds.Tables(LMZ100C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If EventShubetsu = LMM090C.EventShubetsu.ENTER Then
                .Item("YOKO_TARIFF_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF  'キャッシュ検索

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ100", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "運送会社マスタ"

    ''' <summary>
    ''' 運送会社Popの戻り値を設定
    ''' </summary>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnsoPop(ByVal EventShubetsu As LMM090C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowUnsoPopup(EventShubetsu)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)

            With Me._Frm

                .txtShiteiUnsoCompCd.TextValue = dr.Item("UNSOCO_CD").ToString()
                .txtShiteiUnsoShitenCd.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
                .lblShiteiUnsoCompNm.TextValue = Me._ControlG.EditConcatData(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString(), Space(1))

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 運送会社マスタ参照POP起動
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowUnsoPopup(ByVal EventShubetsu As LMM090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ250DS()
        Dim dt As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If EventShubetsu = LMM090C.EventShubetsu.ENTER Then
                .Item("UNSOCO_CD") = Me._Frm.txtShiteiUnsoCompCd.TextValue
                .Item("UNSOCO_BR_CD") = Me._Frm.txtShiteiUnsoShitenCd.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ250", "", Me._PopupSkipFlg)

    End Function

#End Region

    'ADD Start 2018/10/25 要望番号001820
#Region "届先マスタ"

    ''' <summary>
    ''' 届先マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnInkaOrigPop(ByVal objNm As String, ByVal EventShubetsu As LMM090C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowDestPopup(ctl, EventShubetsu)

        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)

            With Me._Frm

                .txtInkaOrigCd.TextValue = dr.Item("DEST_CD").ToString()
                .lblInkaOrigNm.TextValue = dr.Item("DEST_NM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 届先マスタ参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowDestPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal EventShubetsu As LMM090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            .Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
            If EventShubetsu = LMM090C.EventShubetsu.ENTER Then
                .Item("DEST_CD") = ctl.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)

    End Function
#End Region
    'ADD End   2018/10/25 要望番号001820

#Region "請求先マスタ"

    ''' <summary>
    ''' 請求先Popの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnchinSeiToPop(ByVal objNm As String, ByVal EventShubetsu As LMM090C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowUnchinSeiToPopup(ctl, EventShubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)
            Dim setNm As String = Me._ControlG.EditConcatData(dr.Item("SEIQTO_NM").ToString(), dr.Item("SEIQTO_BUSYO_NM").ToString(), Space(1))

            With Me._Frm

                ctl.TextValue = dr.Item("SEIQTO_CD").ToString()
                Select Case objNm
                    Case .txtSeiqCd.Name
                        .lblSeiqNm.TextValue = setNm
                    Case .txtHokanSeiqCd.Name
                        .lblHokanSeiqNm.TextValue = setNm
                    Case .txtNiyakuSeiqCd.Name
                        .lblNiyakuSeiqNm.TextValue = setNm
                    Case .txtUnchinSeiqCd.Name
                        .lblUnchinSeiqNm.TextValue = setNm
                    Case .txtSagyoSeiqCd.Name
                        .lblSagyoSeiqNm.TextValue = setNm
                End Select

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 請求先マスタ参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowUnchinSeiToPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal EventShubetsu As LMM090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ220DS()
        Dim dt As DataTable = ds.Tables(LMZ220C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If EventShubetsu = LMM090C.EventShubetsu.ENTER Then
                .Item("SEIQTO_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ220", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "新荷主(ABP)"

    ''' <summary>
    ''' 真荷主Popの戻り値を設定
    ''' </summary>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnTcustPop(ByVal objNm As String, ByVal EventShubetsu As LMM090C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowTcustPopup(ctl, EventShubetsu)

        If prm.ReturnFlg = True Then
            If prm.ParamDataSet.Tables(LMZ350C.TABLE_NM_OUT).Rows.Count = 0 Then
                Return False

            Else
                Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ350C.TABLE_NM_OUT).Rows(0)

                With Me._Frm
                    .txtTcustBpCd.TextValue = dr.Item("BP_CD").ToString()
                    .lblTcustBpNm.TextValue = dr.Item("BP_NM1").ToString()
                End With

                Return True
            End If
        End If

    End Function

    ''' <summary>
    ''' 真荷主参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowTcustPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal EventShubetsu As LMM090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ350DS()
        Dim dt As DataTable = ds.Tables(LMZ350C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            Select Case EventShubetsu
                Case LMM090C.EventShubetsu.ENTER
                    .Item("BP_CD") = ctl.TextValue
                    .Item("CNT_CHK_ONLY_FLG") = LMConst.FLG.OFF

                Case LMM090C.EventShubetsu.HOZON
                    .Item("BP_CD") = ctl.TextValue
                    .Item("CNT_CHK_ONLY_FLG") = LMConst.FLG.ON
            End Select
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ350", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData()

        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        Dim dr As DataRow = Me._InDs.Tables(LMM090C.TABLE_NM_IN).NewRow()

        With Me._Frm.sprCust.ActiveSheet

            Dim custCd As String = Me._ControlV.GetCellValue(.Cells(0, LMM090G.sprCustDef.CUST_CD.ColNo))
            custCd = custCd.PadRight(11, Convert.ToChar(" "))

            '検索条件を設定
            dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM090G.sprCustDef.BR_NM.ColNo))
            dr.Item("CUST_CD_L") = custCd.Substring(0, 5).Trim()
            dr.Item("CUST_CD_M") = custCd.Substring(5, 2).Trim()
            dr.Item("CUST_CD_S") = custCd.Substring(7, 2).Trim()
            dr.Item("CUST_CD_SS") = custCd.Substring(9, 2).Trim()
            dr.Item("CUST_NM_L") = Me._ControlV.GetCellValue(.Cells(0, LMM090G.sprCustDef.CUST_NM_L.ColNo))
            dr.Item("CUST_NM_M") = Me._ControlV.GetCellValue(.Cells(0, LMM090G.sprCustDef.CUST_NM_M.ColNo))
            dr.Item("CUST_NM_S") = Me._ControlV.GetCellValue(.Cells(0, LMM090G.sprCustDef.CUST_NM_S.ColNo))
            dr.Item("CUST_NM_SS") = Me._ControlV.GetCellValue(.Cells(0, LMM090G.sprCustDef.CUST_NM_SS.ColNo))
            dr.Item("SYS_DEL_FLG") = Me._ControlV.GetCellValue(.Cells(0, LMM090G.sprCustDef.STATUS.ColNo))
            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM090G.sprCustDef.BR_CD.ColNo))
            dr.Item("INTEG_WEB_FLG") = Me._ControlV.GetCellValue(.Cells(0, LMM090G.sprCustDef.INTEG_WEB_FLG.ColNo))     'ADD 2018/12/28 依頼番号 : 003453   【LMS】IntegWeb利用状況を、荷主マスタに表示＆データ抽出機能
            dr.Item("KBN_LANG") = If("0".Equals(lgm.MessageLanguage()), "ja", "en")

            Me._InDs.Tables(LMM090C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' 契約通貨コードコンボ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectComboItemCurrCd()

        Dim item As String = String.Empty
        'DataSet設定
        Me._InDs = New LMM090DS()
        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectComboItemCurrCd")
        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me.CallWSA("LMM090BLF", "SelectComboItemCurrCd", Me._InDs)
        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectComboItemCurrCd")
        Dim dt As DataTable = rtnDs.Tables("LMM090OUT")
        With Me._Frm

            Dim max As Integer = dt.Rows.Count() - 1
            .cmbItemCurrCd.Items.Add(New ListItem(New SubItem() {New SubItem(""), New SubItem("")}))

            For i As Integer = 0 To max
                item = dt.Rows(i).Item("ITEM_CURR_CD").ToString()
                '.cmbItemCurrCd.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(i.ToString())}))
                .cmbItemCurrCd.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(item)}))
            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(排他処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetHaitaChk()

        Dim inDr As DataRow = Me._InDs.Tables(LMM090C.TABLE_NM_IN).NewRow()
        Dim tarifDr As DataRow = Nothing

        With Me._Frm

            '********************** 荷主マスタ排他チェック用 ***************************

            '排他処理条件を格納
            inDr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            inDr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            inDr.Item("CUST_CD_M") = .lblCustCdM.TextValue
            inDr.Item("CUST_CD_S") = .lblCustCdS.TextValue
            inDr.Item("CUST_CD_SS") = .lblCustCdSS.TextValue
            inDr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdateDate.TextValue)
            inDr.Item("SYS_UPD_TIME") = .lblUpdateTime.TextValue.Trim

            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'inDr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            inDr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()

            '押下ボタン判断用
            inDr.Item("CLICK_L_FLG") = LMConst.FLG.OFF
            inDr.Item("CLICK_M_FLG") = LMConst.FLG.OFF
            inDr.Item("CLICK_S_FLG") = LMConst.FLG.OFF
            inDr.Item("CLICK_SS_FLG") = LMConst.FLG.OFF
            Select Case Me._ClickObject
                Case LMM090C.ClickObject.CUST_L
                    inDr.Item("CLICK_L_FLG") = LMConst.FLG.ON
                Case LMM090C.ClickObject.CUST_M
                    inDr.Item("CLICK_M_FLG") = LMConst.FLG.ON
                Case LMM090C.ClickObject.CUST_S
                    inDr.Item("CLICK_S_FLG") = LMConst.FLG.ON
                Case LMM090C.ClickObject.CUST_SS
                    inDr.Item("CLICK_SS_FLG") = LMConst.FLG.ON
            End Select

            Me._InDs.Tables(LMM090C.TABLE_NM_IN).Rows.Add(inDr)

            '****************** 運賃タリフセットマスタ排他チェック用 ***********************

            If String.IsNullOrEmpty(.lblUpdateDateTariffN.TextValue) = False Then

                tarifDr = Me._InDs.Tables(LMM090C.TABLE_NM_TARIFF).NewRow()

                '排他処理条件を格納(入荷データ)
                tarifDr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
                tarifDr.Item("CUST_CD_L") = .txtCustCdL.TextValue
                tarifDr.Item("CUST_CD_M") = .lblCustCdM.TextValue
                tarifDr.Item("SET_MST_CD") = .lblSetMstCdTariffN.TextValue
                tarifDr.Item("SYS_UPD_DATE") = .lblUpdateDateTariffN.TextValue
                tarifDr.Item("SYS_UPD_TIME") = .lblUpdateTimeTariffN.TextValue

                'スキーマ名取得用
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'tarifDr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
                tarifDr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()

                Me._InDs.Tables(LMM090C.TABLE_NM_TARIFF).Rows.Add(tarifDr)

            End If

            If String.IsNullOrEmpty(.lblUpdateDateTariffS.TextValue) = False Then

                tarifDr = Me._InDs.Tables(LMM090C.TABLE_NM_TARIFF).NewRow()

                '排他処理条件を格納(出荷データ)
                tarifDr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
                tarifDr.Item("CUST_CD_L") = .txtCustCdL.TextValue
                tarifDr.Item("CUST_CD_M") = .lblCustCdM.TextValue
                tarifDr.Item("SET_MST_CD") = .lblSetMstCdTariffS.TextValue
                tarifDr.Item("SYS_UPD_DATE") = .lblUpdateDateTariffS.TextValue
                tarifDr.Item("SYS_UPD_TIME") = .lblUpdateTimeTariffS.TextValue

                'スキーマ名取得用
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'tarifDr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
                tarifDr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()

                Me._InDs.Tables(LMM090C.TABLE_NM_TARIFF).Rows.Add(tarifDr)

            End If

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(削除復活処理)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal ds As DataSet)

        With Me._Frm

            '削除/復活の切り替えを行う
            Dim delflg As String = String.Empty
            Select Case .lblSituation.RecordStatus
                Case RecordStatus.NOMAL_REC
                    delflg = LMConst.FLG.ON
                Case RecordStatus.DELETE_REC
                    delflg = LMConst.FLG.OFF
            End Select

            '********************** 荷主マスタ削除/復活用 ***************************
            Dim custDr As DataRow = ds.Tables(LMM090C.TABLE_NM_IN).NewRow()

            '更新条件/更新内容を格納
            custDr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            custDr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            custDr.Item("CUST_CD_M") = .lblCustCdM.TextValue
            custDr.Item("CUST_CD_S") = .lblCustCdS.TextValue
            custDr.Item("CUST_CD_SS") = .lblCustCdSS.TextValue
            custDr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdateDate.TextValue)
            custDr.Item("SYS_UPD_TIME") = .lblUpdateTime.TextValue.Trim
            custDr.Item("SYS_DEL_FLG") = delflg

            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'custDr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            custDr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()

            ds.Tables(LMM090C.TABLE_NM_IN).Rows.Add(custDr)

            '****************** 運賃タリフセットマスタ削除/復活用 ***********************

            Dim tarifDr As DataRow = Nothing

            tarifDr = ds.Tables(LMM090C.TABLE_NM_TARIFF).NewRow()

            '更新条件/更新内容を格納(入荷データ)
            tarifDr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            tarifDr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            tarifDr.Item("CUST_CD_M") = .lblCustCdM.TextValue
            tarifDr.Item("SET_MST_CD") = .lblSetMstCdTariffN.TextValue
            tarifDr.Item("SYS_UPD_DATE") = .lblUpdateDateTariffN.TextValue
            tarifDr.Item("SYS_UPD_TIME") = .lblUpdateTimeTariffN.TextValue
            tarifDr.Item("SYS_DEL_FLG") = delflg

            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'tarifDr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            tarifDr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()

            ds.Tables(LMM090C.TABLE_NM_TARIFF).Rows.Add(tarifDr)

            tarifDr = ds.Tables(LMM090C.TABLE_NM_TARIFF).NewRow()

            '更新条件/更新内容を格納(出荷データ)
            tarifDr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            tarifDr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            tarifDr.Item("CUST_CD_M") = .lblCustCdM.TextValue
            tarifDr.Item("SET_MST_CD") = .lblSetMstCdTariffS.TextValue
            tarifDr.Item("SYS_UPD_DATE") = .lblUpdateDateTariffS.TextValue
            tarifDr.Item("SYS_UPD_TIME") = .lblUpdateTimeTariffS.TextValue
            tarifDr.Item("SYS_DEL_FLG") = delflg

            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'tarifDr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            tarifDr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()

            ds.Tables(LMM090C.TABLE_NM_TARIFF).Rows.Add(tarifDr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(保存処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetSave(ByVal ds As DataSet)

        '荷役マスタ更新内容をDataSetに格納する
        Call Me.SetDataSetCustSave(ds)

        '運賃タリフセットマスタ更新内容をDataSetに格納する
        Call Me.SetDataSetTariffSave(ds)

        '荷主別帳票マスタ更新内容をDataSetに格納する
        Call Me.SetDataSetCustPrtSave(ds)

        '要望番号:349 yamanaka 2012.07.12 Start
        '荷主明細マスタ更新内容をDataSetに格納する
        Select Case _ClickObject
            Case LMM090C.ClickObject.INIT
                Call Me.SetDataSetCustDtlSave(ds, Me._Frm.sprCustDetailL)
                Call Me.SetDataSetCustDtlSave(ds, Me._Frm.sprCustDetailM)
                Call Me.SetDataSetCustDtlSave(ds, Me._Frm.sprCustDetailS)
                Call Me.SetDataSetCustDtlSave(ds, Me._Frm.sprCustDetailSS)
            Case LMM090C.ClickObject.CUST_L
                Call Me.SetDataSetCustDtlSave(ds, Me._Frm.sprCustDetailL)
            Case LMM090C.ClickObject.CUST_M
                Call Me.SetDataSetCustDtlSave(ds, Me._Frm.sprCustDetailM)
                If Me._HukusyaObject = LMM090C.HukushaObject.HUKUSHA_M Then
                    Call Me.SetDataSetCustDtlSave(ds, Me._Frm.sprCustDetailS)
                    Call Me.SetDataSetCustDtlSave(ds, Me._Frm.sprCustDetailSS)
                End If
            Case LMM090C.ClickObject.CUST_S
                Call Me.SetDataSetCustDtlSave(ds, Me._Frm.sprCustDetailS)
                If Me._HukusyaObject = LMM090C.HukushaObject.HUKUSHA_S Then
                    Call Me.SetDataSetCustDtlSave(ds, Me._Frm.sprCustDetailSS)
                End If
            Case LMM090C.ClickObject.CUST_SS
                Call Me.SetDataSetCustDtlSave(ds, Me._Frm.sprCustDetailSS)
            Case Else
                '処理なし
        End Select
        '要望番号:349 yamanaka 2012.07.12 End

    End Sub

    ''' <summary>
    ''' 荷主マスタ更新内容格納
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetCustSave(ByVal ds As DataSet)

        Dim tableNm As String = LMM090C.TABLE_NM_IN
        Dim dr As DataRow = ds.Tables(tableNm).NewRow()

        With Me._Frm

            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            '****************** 荷主(大)タブ ******************
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_NM_L") = .txtCustNmL.TextValue
            dr.Item("CUST_OYA_CD") = .txtMainCustCd.TextValue
            dr.Item("UNSO_TEHAI_KB") = .cmbUnsoTehaiKbn.SelectedValue
            dr.Item("SMPL_SAGYO") = .txtSampleSagyoCd.TextValue
            'ADD START 2018/11/14 要望番号001939
            If .optCoaInkaDateY.Checked Then
                dr.Item("COA_INKA_DATE_FLG") = "1"
            Else
                dr.Item("COA_INKA_DATE_FLG") = "0"
            End If
            'ADD END   2018/11/14 要望番号001939
            dr.Item("PRODUCT_SEG_CD") = .cmbProductSegCd.SelectedValue

            '****************** 荷主(中)タブ ******************
            dr.Item("CUST_CD_M") = .lblCustCdM.TextValue
            dr.Item("CUST_NM_M") = .txtCustNmM.TextValue
            dr.Item("ZIP") = .txtZipNo.TextValue
            dr.Item("AD_1") = .txtAdd1.TextValue
            dr.Item("AD_2") = .txtAdd2.TextValue
            dr.Item("AD_3") = .txtAdd3.TextValue
            dr.Item("SAITEI_HAN_KB") = .cmbHoshoMinKbn.SelectedValue
            dr.Item("UNSO_HOKEN_AUTO_YN") = .cmbUnsoHokenAutoKbn.SelectedValue        'ADD 2018/10/22 依頼番号 : 002400   【LMS】運送保険_設定商品を出荷時、運送の保険料欄に保険料を自動入力させる
            dr.Item("TAX_KB") = .cmbKazeiKbn.SelectedValue
            dr.Item("BETU_KYORI_CD") = .txtCustKyoriCd.TextValue
            dr.Item("DEFAULT_SOKO_CD") = .cmbSoko.SelectedValue
            dr.Item("SP_UNSO_CD") = .txtShiteiUnsoCompCd.TextValue
            dr.Item("SP_UNSO_BR_CD") = .txtShiteiUnsoShitenCd.TextValue
            dr.Item("HOKAN_FREE_KIKAN") = .numHokanFree.Value
            dr.Item("INKA_RPT_YN") = .cmbNyukaHokoku.SelectedValue
            dr.Item("ZAI_RPT_YN") = .cmbZaikoHokoku.SelectedValue
            dr.Item("OUTKA_RPT_YN") = .cmbShukkaHokoku.SelectedValue
            'START YANAI 要望番号824
            dr.Item("TANTO_CD") = .txtTantoCd.TextValue
            'END YANAI 要望番号824
            'START OU 要望番号2229
            dr.Item("ITEM_CURR_CD") = .cmbItemCurrCd.TextValue
            'END OU 要望番号2229
            dr.Item("INKA_ORIG_CD") = .txtInkaOrigCd.TextValue      'ADD 2018/10/25 要望番号001820
            dr.Item("INIT_INKA_PLAN_DATE_KB") = .cmbInitDateNyuka.SelectedValue     'ADD 2018/10/31　002192	荷主ごと_入庫日・出荷日の初期値設定
            dr.Item("INIT_OUTKA_PLAN_DATE_KB") = .cmbInitDateShukka.SelectedValue   'ADD 2018/10/31　002192	荷主ごと_入庫日・出荷日の初期値設定
            dr.Item("REMARK") = .txtTekiyo.TextValue     'ADD 2019/07/10 002520

            '****************** 荷主(小)タブ ******************
            dr.Item("CUST_CD_S") = .lblCustCdS.TextValue
            dr.Item("CUST_NM_S") = .txtCustNmS.TextValue
            dr.Item("DENPYO_NM") = .txtCustBetuNm.TextValue
            dr.Item("TCUST_BPCD") = .txtTcustBpCd.TextValue
            dr.Item("OYA_SEIQTO_CD") = .txtSeiqCd.TextValue
            dr.Item("HOKAN_SEIQTO_CD") = .txtHokanSeiqCd.TextValue
            dr.Item("NIYAKU_SEIQTO_CD") = .txtNiyakuSeiqCd.TextValue
            dr.Item("UNCHIN_SEIQTO_CD") = .txtUnchinSeiqCd.TextValue
            dr.Item("SAGYO_SEIQTO_CD") = .txtSagyoSeiqCd.TextValue
            dr.Item("PICK_LIST_KB") = .cmbPikkingKbn.SelectedValue
            dr.Item("UNTIN_CALCULATION_KB") = .cmbUnchinCalc.SelectedValue
            '****************** 荷主(極小)タブ ******************
            dr.Item("CUST_CD_SS") = .lblCustCdSS.TextValue
            dr.Item("CUST_NM_SS") = .txtCustNmSS.TextValue
            dr.Item("PIC") = .txtMainTantoNm.TextValue
            dr.Item("FUKU_PIC") = .txtSubTantoNm.TextValue
            dr.Item("TEL") = .txtTel.TextValue
            dr.Item("FAX") = .txtFax.TextValue
            dr.Item("MAIL") = .txtEmail.TextValue
            dr.Item("SOKO_CHANGE_KB") = .cmbSouKaeSyori.SelectedValue
            dr.Item("HOKAN_NIYAKU_CALCULATION") = .imdLastCalc.TextValue
            dr.Item("HOKAN_NIYAKU_KEISAN_YN") = .cmbCalc.SelectedValue
            dr.Item("SEKY_OFB_KB") = .cmbSeiqHakugaiHinKbn.SelectedValue

            '押下ボタン判断用
            dr.Item("CLICK_L_FLG") = LMConst.FLG.OFF
            dr.Item("CLICK_M_FLG") = LMConst.FLG.OFF
            dr.Item("CLICK_S_FLG") = LMConst.FLG.OFF
            dr.Item("CLICK_SS_FLG") = LMConst.FLG.OFF
            Select Case Me._ClickObject
                Case LMM090C.ClickObject.CUST_L
                    dr.Item("CLICK_L_FLG") = LMConst.FLG.ON
                Case LMM090C.ClickObject.CUST_M
                    dr.Item("CLICK_M_FLG") = LMConst.FLG.ON
                Case LMM090C.ClickObject.CUST_S
                    dr.Item("CLICK_S_FLG") = LMConst.FLG.ON
                Case LMM090C.ClickObject.CUST_SS
                    dr.Item("CLICK_SS_FLG") = LMConst.FLG.ON
            End Select

            '排他処理条件を格納
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdateDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdateTime.TextValue.Trim

            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()

            ds.Tables(tableNm).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' 運賃タリフセットマスタ更新内容格納
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetTariffSave(ByVal ds As DataSet)

        Dim tableNm As String = LMM090C.TABLE_NM_TARIFF
        Dim dr As DataRow = Nothing

        With Me._Frm

            '**************** 入荷情報 *******************

            dr = ds.Tables(tableNm).NewRow()

            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .lblCustCdM.TextValue
            'dr.Item("SET_MST_CD") = .lblSetMstCdTariffN.TextValue
            dr.Item("SET_MST_CD") = LMM090C.SET_MST_CD_NYUKA
            dr.Item("DEST_CD") = .lblDestCdTariffN.TextValue
            'START YANAI 要望番号691
            'dr.Item("SET_KB") = .lblSetKbnTariffN.TextValue
            dr.Item("SET_KB") = LMM090C.SET_KBN_NYUKA
            'END YANAI 要望番号691
            dr.Item("TARIFF_BUNRUI_KB") = .cmbTarifBunruiNyuka.SelectedValue
            dr.Item("UNCHIN_TARIFF_CD1") = .txtUnchinTarifTonNyuka.TextValue
            dr.Item("UNCHIN_TARIFF_CD2") = .txtUnchinTarifShadateNyuka.TextValue
            dr.Item("EXTC_TARIFF_CD") = .txtWarimashiTarifNyuka.TextValue
            dr.Item("YOKO_TARIFF_CD") = .txtYokomochiTarifNyuka.TextValue

            '排他処理条件を格納
            dr.Item("SYS_UPD_DATE") = .lblUpdateDateTariffN.TextValue
            dr.Item("SYS_UPD_TIME") = .lblUpdateTimeTariffN.TextValue

            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()

            ds.Tables(tableNm).Rows.Add(dr)

            '**************** 出荷情報 *******************

            dr = ds.Tables(tableNm).NewRow()

            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .lblCustCdM.TextValue
            'dr.Item("SET_MST_CD") = .lblSetMstCdTariffS.TextValue
            dr.Item("SET_MST_CD") = LMM090C.SET_MST_CD_SHUKKA
            dr.Item("DEST_CD") = .lblDestCdTariffS.TextValue
            'START YANAI 要望番号691
            'dr.Item("SET_KB") = .lblSetKbnTariffS.TextValue
            dr.Item("SET_KB") = LMM090C.SET_KBN_SHUKKA
            'END YANAI 要望番号691
            dr.Item("TARIFF_BUNRUI_KB") = .cmbTarifBunruiShukka.SelectedValue
            dr.Item("UNCHIN_TARIFF_CD1") = .txtUnchinTarifTonShukka.TextValue
            dr.Item("UNCHIN_TARIFF_CD2") = .txtUnchinTarifShadateShukka.TextValue
            dr.Item("EXTC_TARIFF_CD") = .txtWarimashiTarifShukka.TextValue
            dr.Item("YOKO_TARIFF_CD") = .txtYokomochiTarifShukka.TextValue

            '排他処理条件を格納
            dr.Item("SYS_UPD_DATE") = .lblUpdateDateTariffS.TextValue
            dr.Item("SYS_UPD_TIME") = .lblUpdateTimeTariffS.TextValue

            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()

            ds.Tables(tableNm).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' 荷主別帳票マスタ更新内容格納
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetCustPrtSave(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM090C.TABLE_NM_CUST_PRT)
        Dim max As Integer = Me._Frm.sprCustPrt.ActiveSheet.Rows.Count - 1
        Dim dr As DataRow = Nothing
        For i As Integer = 0 To max

            With Me._Frm.sprCustPrt.ActiveSheet

                dr = dt.NewRow()

                dr.Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue
                dr.Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
                dr.Item("CUST_CD_M") = Me._Frm.lblCustCdM.TextValue
                dr.Item("CUST_CD_S") = Me._Frm.lblCustCdS.TextValue
                dr.Item("PTN_ID") = Me._ControlV.GetCellValue(.Cells(i, LMM090G.sprCustPrtDef.PTN_ID.ColNo))
                dr.Item("PTN_CD") = Me._ControlV.GetCellValue(.Cells(i, LMM090G.sprCustPrtDef.PTN_NM.ColNo))

                'スキーマ名取得用
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
                dr.Item("USER_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()

                dt.Rows.Add(dr)

            End With
        Next

    End Sub

    '要望番号:349 yamanaka 2012.07.12 Start
    ''' <summary>
    ''' 荷主明細マスタ更新内容格納
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="spr">Spread</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetCustDtlSave(ByVal ds As DataSet, ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread)

        Dim dt As DataTable = ds.Tables(LMM090C.TABLE_NM_CUST_DETAILS)
        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
        Dim dr As DataRow = Nothing
        Dim edaNo As Integer = Me._MaxEdaNoSet

        For i As Integer = 0 To max

            With spr.ActiveSheet

                dr = dt.NewRow()
                dr.Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue
                If spr.Name.Equals(Me._Frm.sprCustDetailL.Name) = True Then
                    dr.Item("CUST_CD") = Me._Frm.txtCustCdL.TextValue
                    dr.Item("CUST_CLASS") = LMM090C.CUST_CLASS_L
                ElseIf spr.Name.Equals(Me._Frm.sprCustDetailM.Name) = True Then
                    '複写判定
                    If Me._HukusyaObject = LMM090C.HukushaObject.HUKUSHA_M Then
                        dr.Item("CUST_CD") = "M"
                        dr.Item("CUST_CLASS") = LMM090C.CUST_CLASS_M
                    Else
                        dr.Item("CUST_CD") = String.Concat(Me._Frm.txtCustCdL.TextValue _
                                                           , Me._Frm.lblCustCdM.TextValue)
                        dr.Item("CUST_CLASS") = LMM090C.CUST_CLASS_M
                    End If
                ElseIf spr.Name.Equals(Me._Frm.sprCustDetailS.Name) = True Then
                    '複写判定
                    If Me._HukusyaObject = LMM090C.HukushaObject.HUKUSHA_M _
                    OrElse Me._HukusyaObject = LMM090C.HukushaObject.HUKUSHA_S Then
                        dr.Item("CUST_CD") = "S"
                        dr.Item("CUST_CLASS") = LMM090C.CUST_CLASS_S
                    Else
                        dr.Item("CUST_CD") = String.Concat(Me._Frm.txtCustCdL.TextValue _
                                                           , Me._Frm.lblCustCdM.TextValue _
                                                           , Me._Frm.lblCustCdS.TextValue)
                        dr.Item("CUST_CLASS") = LMM090C.CUST_CLASS_S
                    End If
                ElseIf spr.Name.Equals(Me._Frm.sprCustDetailSS.Name) = True Then
                    '複写判定
                    If Me._HukusyaObject = LMM090C.HukushaObject.HUKUSHA_M _
                    OrElse Me._HukusyaObject = LMM090C.HukushaObject.HUKUSHA_S _
                    OrElse Me._HukusyaObject = LMM090C.HukushaObject.HUKUSHA_SS Then
                        dr.Item("CUST_CD") = "SS"
                        dr.Item("CUST_CLASS") = LMM090C.CUST_CLASS_SS
                    Else
                        dr.Item("CUST_CD") = String.Concat(Me._Frm.txtCustCdL.TextValue _
                                                           , Me._Frm.lblCustCdM.TextValue _
                                                           , Me._Frm.lblCustCdS.TextValue _
                                                           , Me._Frm.lblCustCdSS.TextValue)
                        dr.Item("CUST_CLASS") = LMM090C.CUST_CLASS_SS
                    End If
                End If
                If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells _
                                                                  (i, LMM090C.sprCustDetailColumnIndex.EDA_NO))) Then
                    edaNo += 1
                    dr.Item("CUST_CD_EDA") = edaNo.ToString().PadLeft(2, Convert.ToChar("0"))
                Else
                    dr.Item("CUST_CD_EDA") = Me._ControlV.GetCellValue(.Cells _
                                                                       (i, LMM090C.sprCustDetailColumnIndex.EDA_NO))
                End If
                dr.Item("SUB_KB") = Me._ControlV.GetCellValue(.Cells(i, LMM090C.sprCustDetailColumnIndex.YOTO_KBN))
                dr.Item("SET_NAIYO") = Me._ControlV.GetCellValue(.Cells(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE))
                dr.Item("SET_NAIYO_2") = Me._ControlV.GetCellValue(.Cells(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE2))
                dr.Item("SET_NAIYO_3") = Me._ControlV.GetCellValue(.Cells(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE3))
                dr.Item("REMARK") = Me._ControlV.GetCellValue(.Cells(i, LMM090C.sprCustDetailColumnIndex.BIKO))

                'スキーマ名取得用
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
                dr.Item("USER_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()

                dt.Rows.Add(dr)

            End With
        Next

    End Sub
    '要望番号:349 yamanaka 2012.07.12 End

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(新規処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "NewDataEvent")

        Me.NewDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "NewDataEvent")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMM090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除処理
        Me.DeleteDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMM090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        Me.MasterShowEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMM090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveEvent")

        '保存処理
        Me.SaveEvent(LMM090C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM090F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓　========================

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMM090F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        Me.SprCellSelect(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMM090FKeyDown(ByVal frm As LMM090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM090F_KeyDown")

        Call Me.EnterAction(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM090F_KeyDown")

    End Sub

    ''' <summary>
    ''' Addボタン押下時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub AddClick(ByVal frm As LMM090F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "AddClick")

        '行追加処理
        Call Me.RowAdd()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "AddClick")

    End Sub

    ''' <summary>
    ''' Deleteボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub DelClick(ByVal frm As LMM090F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DelClick")

        '行削除処理
        Call Me.RowDel()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DelClick")

    End Sub

    '要望番号:349 yamanaka 2012.07.10 Start
    ''' <summary>
    ''' DetailAddボタン押下時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="trgSpr">対象Spreadの設定</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub DetailAddLClick(ByVal frm As LMM090F, ByVal trgSpr As LMM090C.SprDetailObject, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DetailAddLClick")

        '荷主明細用行追加処理
        Call Me.DetailRowAdd(trgSpr)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DetailAddLClick")

    End Sub

    ''' <summary>
    ''' DetailDeleteボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub DetailDelLClick(ByVal frm As LMM090F, ByVal trgSpr As LMM090C.SprDetailObject, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DetailDelLClick")

        '荷主明細用行削除処理
        Call Me.DetailRowDel(trgSpr)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DetailDelLClick")

    End Sub
    '要望番号:349 yamanaka 2012.07.10 End

    ''' <summary>
    ''' 編集(大)ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub EditLClick(ByVal frm As LMM090F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditLClick")

        '押下ボタンを設定
        Me._ClickObject = LMM090C.ClickObject.CUST_L

        '編集イベント
        Call Me.EditDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditLClick")

    End Sub

    ''' <summary>
    ''' 編集(中)ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub EditMClick(ByVal frm As LMM090F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditMClick")

        '押下ボタンを設定
        Me._ClickObject = LMM090C.ClickObject.CUST_M

        '編集イベント
        Call Me.EditDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditMClick")

    End Sub

    ''' <summary>
    ''' 編集(小)ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub EditSClick(ByVal frm As LMM090F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditSClick")

        '押下ボタンを設定
        Me._ClickObject = LMM090C.ClickObject.CUST_S

        '編集イベント
        Call Me.EditDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditSClick")

    End Sub

    ''' <summary>
    ''' 編集(極小)ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub EditSSClick(ByVal frm As LMM090F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditSSClick")

        '押下ボタンを設定
        Me._ClickObject = LMM090C.ClickObject.CUST_SS

        '編集イベント
        Call Me.EditDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditSSClick")

    End Sub

    ''' <summary>
    ''' 追加(中)ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub FukushaMClick(ByVal frm As LMM090F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FukushaMClick")

        '押下ボタンを設定
        Me._ClickObject = LMM090C.ClickObject.CUST_M
        '要望番号:349 yamanaka 2012.07.26 Start
        Me._HukusyaObject = LMM090C.HukushaObject.HUKUSHA_M
        '要望番号:349 yamanaka 2012.07.26 End

        '編集イベント
        Call Me.CopyDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FukushaMClick")

    End Sub

    ''' <summary>
    ''' 追加(小)ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub FukushaSClick(ByVal frm As LMM090F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FukushaSClick")

        '押下ボタンを設定
        Me._ClickObject = LMM090C.ClickObject.CUST_S
        '要望番号:349 yamanaka 2012.07.26 Start
        Me._HukusyaObject = LMM090C.HukushaObject.HUKUSHA_S
        '要望番号:349 yamanaka 2012.07.26 End

        '編集イベント
        Call Me.CopyDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FukushaSClick")

    End Sub

    ''' <summary>
    ''' 追加(極小)ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub FukushaSSClick(ByVal frm As LMM090F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FukushaSSClick")

        '押下ボタンを設定
        Me._ClickObject = LMM090C.ClickObject.CUST_SS
        '要望番号:349 yamanaka 2012.07.26 Start
        Me._HukusyaObject = LMM090C.HukushaObject.HUKUSHA_SS
        '要望番号:349 yamanaka 2012.07.26 End

        '編集イベント
        Call Me.CopyDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FukushaSSClick")

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeave
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprCustLeaveCell(ByVal frm As LMM090F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprCustLeaveCell")

        Call Me.SprFindLeaveCell(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprCustLeaveCell")

    End Sub

    ''' <summary>
    ''' セルのLeaveイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub LeaveCell(ByVal frm As LMM090F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LeaveCell")

        'セルのLeaveイベントを行う
        Call Me.LeaveSprCell(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LeaveCell")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

#End Region

End Class