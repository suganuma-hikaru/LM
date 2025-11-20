' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF090H : 支払編集
'  作  成  者       :  YANAI
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF090ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMF090H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMF090V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMF090G

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF090F

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
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFConC As LMFControlC

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 連続入力フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _RenzokuFlg As Boolean = False

    ''' <summary>
    ''' 連続入力ラストデータフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _RenzokuLastFlg As Boolean = False

    ''' <summary>
    ''' 連続入力処理件数カウント
    ''' </summary>
    ''' <remarks></remarks>
    Private _RenzokuCnt As Integer = 0

    ''' <summary>
    ''' InData値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _DsInData As DataSet

    ''' <summary>
    ''' 確定フラグ（保存時、確定も同時に行う場合使用）
    ''' </summary>
    ''' <remarks></remarks>
    Private _KakuteiFlg As Boolean = False
    
    ''' <summary>
    ''' エラーフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _ErrFlg As Boolean = False
    
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
        Dim frm As LMF090F = New LMF090F(Me)

        'Hnadler共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMFConH = New LMFControlH(sForm, MyBase.GetPGID())

        'Gamen共通クラスの設定
        Me._LMFConG = New LMFControlG(sForm)

        'Validate共通クラスの設定
        Me._LMFConV = New LMFControlV(Me, sForm, Me._LMFConG)

        'Gamenクラスの設定
        Me._G = New LMF090G(Me, frm, Me._LMFConG, Me._V)

        'Validateクラスの設定
        Me._V = New LMF090V(Me, frm, Me._LMFConV, Me._LMFConG, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '連続入力フラグの設定
        If ("01").Equals(prmDs.Tables(LMF090C.TABLE_NM_IN).Rows(0).Item("RENZOKU_FLG").ToString) = True Then
            Me._RenzokuFlg = True
        End If

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMF090C.EventShubetsu.MAIN, Me._RenzokuFlg)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの検索行に初期値を設定する
        Call Me._G.InitSpread()

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G007")

        'フォーカスの設定
        Call Me._G.SetFoucus()


        '閾値の設定
        MyBase.SetLimitCount(Me._LMFConG.GetLimitData())


        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetMaxResultCount(mc)



        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================

        Dim ds As DataSet = Me.SetDataSetInData(frm, New LMF090DS())
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)

        Me._RenzokuCnt = 0
        prmDs.Tables(LMF090C.TABLE_NM_IN).Rows(0).Item("RENZOKU_CNT") = Me._RenzokuCnt
        Me._DsInData = prmDs

        prmDs = MyBase.CallWSA(blf, LMF090C.ACTION_ID_SELECT, prmDs)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

        Else

            'メッセージエリアの設定(0件エラー)
            MyBase.ShowMessage(frm)

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {MyBase.GetResultCount.ToString()})

        End If

        MyBase.ShowMessage(frm, "G003")

        '荷主、出荷日のdrから取得
        Dim dt As DataTable = prmDs.Tables(LMF090C.TABLE_NM_OUT)
        Dim max As Integer = dt.Rows.Count - 1

        Dim shiharaiGrouopNo As String = String.Empty
        Dim shiharaiGrouopNoM As String = String.Empty

        Dim unsoNoL As String = String.Empty
        Dim unsoNoM As String = String.Empty

        'キャッシュから名称を取得
        Dim drs As DataRow() = Nothing
        Dim dr As DataRow = Nothing

        For i As Integer = 0 To max

            dr = dt.Rows(i)

            shiharaiGrouopNo = dr.Item("SHIHARAI_GROUP_NO").ToString
            shiharaiGrouopNoM = dr.Item("SHIHARAI_GROUP_NO_M").ToString
            unsoNoL = dr.Item("UNSO_NO_L").ToString
            unsoNoM = dr.Item("UNSO_NO_M").ToString

            'まとめか判定
            If String.IsNullOrEmpty(shiharaiGrouopNo) = False _
                AndAlso String.IsNullOrEmpty(shiharaiGrouopNoM) = False Then

                '荷主がまとめの時は親を表示
                If shiharaiGrouopNo.Equals(unsoNoL) = True _
                    AndAlso shiharaiGrouopNoM.Equals(unsoNoM) = True Then
                    Exit For
                End If

            End If

        Next

        '初期設定
        Call Me._G.SetInitValu(dr)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        '値の設定
        Call Me._G.SetSpread(prmDs)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        '画面の入力項目ロック
        Call Me._G.LockControl(True)

        Me._Ds = prmDs


        '処理終了アクション
        Call Me.EndAction(frm)

        If Me._RenzokuFlg = True Then
            '編集モードにする
            Me.ShiftEditMode(frm)
        End If

        'フォームの表示
        frm.Show()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShiftEditMode(ByVal frm As LMF090F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMF090C.EventShubetsu.HENSHU) = False Then
            '終了処理
            Call Me.EndAction(frm)
            If Me._RenzokuFlg = True Then
                Call Me._G.SetFunctionKey(LMF090C.EventShubetsu.HENSHU, Me._RenzokuFlg)
            End If
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMF090C.EventShubetsu.HENSHU) = False Then
            Call Me.EndAction(frm)
            If Me._RenzokuFlg = True Then
                Call Me._G.SetFunctionKey(LMF090C.EventShubetsu.HENSHU, Me._RenzokuFlg)
            End If
            Exit Sub
        End If

        '確定フラグチェック
        If Me._V.kakutei(_Ds, frm) = False Then
            Call Me.EndAction(frm)
            If Me._RenzokuFlg = True Then
                Call Me._G.SetFunctionKey(LMF090C.EventShubetsu.HENSHU, Me._RenzokuFlg)
            End If
            Exit Sub
        End If

        'START YANAI 要望番号1424 支払処理
        '項目チェック
        If Me._V.IsInputCheckEdit(Me._Ds) = False Then
            Call Me.EndAction(frm)
            If Me._RenzokuFlg = True Then
                Call Me._G.SetFunctionKey(LMF090C.EventShubetsu.HENSHU, Me._RenzokuFlg)
            End If
            Exit Sub
        End If

        'END YANAI 要望番号1424 支払処理

        '==========================
        'WSAクラス呼出
        '==========================
        Me._Ds = MyBase.CallWSA("LMF090BLF", "HaitaData", _Ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm)

            '終了処理
            Call Me.EndAction(frm)

            '画面全ロックの解除
            MyBase.UnLockedControls(frm)

            If Me._RenzokuFlg = True Then
                Call Me._G.SetFunctionKey(LMF090C.EventShubetsu.HENSHU, Me._RenzokuFlg)
            End If

            Exit Sub

        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G003")

        '終了処理
        Call Me.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMF090C.EventShubetsu.HENSHU, Me._RenzokuFlg)

        '画面全ロックの解除
        Me.UnLockedControls(frm)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()
        Call Me.Bunrui(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        '画面全ロックの解除
        Me.UnLockedControls(frm)

        'スプレッド項目の活性化
        Call Me._G.SetSpread(Me._Ds)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        '終了処理
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMF090F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMF090C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF090C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMF090C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMF090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMF090C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF090C.EventShubetsu.ENTER)

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
        Call Me.ShowPopupControl(frm, objNm, LMF090C.EventShubetsu.ENTER)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me._LMFConH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SaveUncoItemData(ByVal frm As LMF090F, ByVal eventShubetsu As LMF090C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        Me._KakuteiFlg = False

        'メッセージ表示
        Select Case MyBase.ShowMessage(frm, "W183")

            Case MsgBoxResult.Yes   '「はい」押下時
                Me._KakuteiFlg = True

            Case MsgBoxResult.No    '「いいえ」押下時
                Me._KakuteiFlg = False

            Case MsgBoxResult.Cancel    '「キャンセル」押下時

                '終了処理
                Call Me.EndAction(frm)

                Return True
        End Select

        '権限チェック
        If Me._V.IsAuthorityChk(LMF090C.EventShubetsu.SAVE) = False Then

            '終了処理
            Call Me.EndAction(frm)

            Return False
        End If

        '項目チェック
        If Me._V.IsInputCheckSave(Me._Ds) = False Then

            '終了処理
            Call Me.EndAction(frm)

            Return False
        End If

        'DataSet設定
        Me._Ds = Me.SetDatasetUp(frm, Me._Ds)
        'メッセージコードの判定
        If Me._ErrFlg = True Then
            Call Me.EndAction(frm)  '終了処理
            Return False
        End If

        '支払重量を元に重量按分処理結果を設定
        Me._Ds = Me.SetUncinData(Me._Ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateInsertData")

        '==========================
        'WSAクラス呼出
        '==========================
        
        '更新処理(保存処理)
        Me._Ds = MyBase.CallWSA("LMF090BLF", "UpdateData", Me._Ds)

        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then

            MyBase.ShowMessage(frm)
            Call Me.EndAction(frm)  '終了処理
            Return False
        End If

        If Me._RenzokuFlg = True Then

            If Me._KakuteiFlg = True Then
                '終了処理
                Call Me.EndAction(frm)
                If Me.Kakutei(frm, LMF090C.EventShubetsu.KAKUTEI) = False Then
                    Me._KakuteiFlg = False

                    Return False
                End If
            End If

            'カーソルを元に戻す
            Cursor.Current = Cursors.Default()

            'スキップ処理を行う
            Me.skipData(frm, LMF090C.EventShubetsu.SKIP)

        Else
            If Me._KakuteiFlg = True Then
                '終了処理
                Call Me.EndAction(frm)
                If Me.Kakutei(frm, LMF090C.EventShubetsu.KAKUTEI) = False Then
                    Me._KakuteiFlg = False

                    Return False
                End If
            End If

            '更新成功の場合、再検索
            Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
            Me._DsInData.Tables(LMF090C.TABLE_NM_IN).Rows(0).Item("RENZOKU_CNT") = Me._RenzokuCnt
            Me._Ds = MyBase.CallWSA(blf, LMF090C.ACTION_ID_SELECT, Me._DsInData)

            'ログ出力
            MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateInsertData")

            '終了処理
            Call Me.EndAction(frm)

            'モード・ステータスの設定
            Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus()

            'スプレッド項目の活性化
            Call Me._G.SetSpread(Me._Ds)

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey(LMF090C.EventShubetsu.SAVE, Me._RenzokuFlg)
            Dim unsoL As String = String.Empty
            Dim unsoM As String = String.Empty
            Dim msg As String = String.Empty

            If String.IsNullOrEmpty(Me._LMFConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF090G.sprDetailDef.GROUP.ColNo))) = True Then

                unsoL = Me._LMFConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF090G.sprDetailDef.UNSO_NO.ColNo))
                unsoM = Me._LMFConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF090G.sprDetailDef.UNSO_NO_EDA.ColNo))
                msg = String.Concat("[ 運送番号L = ", unsoL, " 運送番号M = ", unsoM, "]")

            End If

            'メッセージの設定
            MyBase.ShowMessage(frm, "G002", New String() {"保存", msg})

            'フォーカスの設定
            Call Me._G.SetFoucus()

            'カーソルを元に戻す
            Cursor.Current = Cursors.Default()

        End If

        Me._KakuteiFlg = False

        Return True

    End Function

    ''' <summary>
    ''' 確定処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function Kakutei(ByVal frm As LMF090F, ByVal eventShubetsu As LMF090C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMF090C.EventShubetsu.KAKUTEI) = False Then
            Call Me.EndAction(frm)  '終了処理
            Return False
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMF090C.EventShubetsu.KAKUTEI) = False Then
            Call Me.EndAction(frm)
            Exit Function
        End If

        '確定フラグチェック
        If Me._KakuteiFlg = False Then
            If Me._V.kakutei(_Ds, frm) = False Then

                '終了処理
                Call Me.EndAction(frm)

                Return False

            End If
        End If

        '入力チェック
        If Me._V.IsKakuteiSaveCheck(_Ds, frm) = False Then

            '終了処理
            Call Me.EndAction(frm)

            Return False

        End If

        ' 処理続行確認()
        Select Case eventShubetsu
            Case LMF090C.EventShubetsu.KAKUTEI
                If Me._KakuteiFlg = False Then
                    If Me.ConfirmIKakuteiMsg(frm, "確定") = False Then
                        Call Me.EndAction(frm) '終了処理
                        Return False
                    End If
                End If
            Case Else
        End Select

        Dim dt As DataTable = _Ds.Tables(LMF090C.TABLE_NM_OUT)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        LMUserInfoManager.GetNrsBrCd()

        '==========================
        'WSAクラス呼出
        '==========================

        '更新処理(確定処理)
        Me._Ds = MyBase.CallWSA("LMF090BLF", "UpdateDataKakutei", _Ds)

        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me.EndAction(frm)  '終了処理
            Return False
        End If

        '更新成功の場合、再検索
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        Me._DsInData.Tables(LMF090C.TABLE_NM_IN).Rows(0).Item("RENZOKU_CNT") = Me._RenzokuCnt
        Me._Ds = MyBase.CallWSA(blf, LMF090C.ACTION_ID_SELECT, Me._DsInData)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateInsertData")

        '終了処理
        Call Me.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMF090C.EventShubetsu.KAKUTEI, Me._RenzokuFlg)
        Dim unsoL As String = String.Empty
        Dim unsoM As String = String.Empty
        Dim msg As String = String.Empty

        If String.IsNullOrEmpty(Me._LMFConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF090G.sprDetailDef.GROUP.ColNo))) = True Then

            unsoL = Me._LMFConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF090G.sprDetailDef.UNSO_NO.ColNo))
            unsoM = Me._LMFConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF090G.sprDetailDef.UNSO_NO_EDA.ColNo))
            msg = String.Concat("[ 運送番号L = ", unsoL, " 運送番号M = ", unsoM, "]")

        End If

        'メッセージの設定
        If Me._KakuteiFlg = False Then
            MyBase.ShowMessage(frm, "G002", New String() {"確定", msg})
        Else
            MyBase.ShowMessage(frm, "G002", New String() {"保存", msg})
        End If

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        Return True

    End Function

    ''' <summary>
    ''' 確定解除処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function KakuteiKaijo(ByVal frm As LMF090F, ByVal eventShubetsu As LMF090C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)
      
        '権限チェック
        If Me._V.IsAuthorityChk(LMF090C.EventShubetsu.KAKUTEIKAIJO) = False Then
            Call Me.EndAction(frm)  '終了処理
            Return False
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMF090C.EventShubetsu.KAKUTEIKAIJO) = False Then
            Call Me.EndAction(frm)
            Exit Function
        End If

        '確定フラグチェック
        If Me._V.mikakutei(_Ds, frm) = False Then
            Call Me.EndAction(frm)
            Return False
        End If

        ''処理続行確認
        Select Case eventShubetsu
            Case LMF090C.EventShubetsu.KAKUTEIKAIJO
                If Me.ConfirmIKakuteiMsg(frm, "確定解除") = False Then
                    Call Me.EndAction(frm) '終了処理
                    Return False
                End If
            Case Else
        End Select

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        LMUserInfoManager.GetNrsBrCd()

        '==========================
        'WSAクラス呼出
        '==========================
        
        ''更新処理(確定解除)
        Me._Ds = MyBase.CallWSA("LMF090BLF", "UpdateDataKakuteikaijo", _Ds)

        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me.EndAction(frm)  '終了処理
            Return False
        End If

        '更新成功の場合、再検索
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        Me._DsInData.Tables(LMF090C.TABLE_NM_IN).Rows(0).Item("RENZOKU_CNT") = Me._RenzokuCnt
        Me._Ds = MyBase.CallWSA(blf, LMF090C.ACTION_ID_SELECT, Me._DsInData)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateInsertData")

        '終了処理
        Call Me.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'メッセージの設定
        Dim unsoL As String = String.Empty
        Dim unsoM As String = String.Empty
        Dim msg As String = String.Empty

        If String.IsNullOrEmpty(Me._LMFConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF090G.sprDetailDef.GROUP.ColNo))) = True Then

            unsoL = Me._LMFConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF090G.sprDetailDef.UNSO_NO.ColNo))
            unsoM = Me._LMFConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF090G.sprDetailDef.UNSO_NO_EDA.ColNo))
            msg = String.Concat("[ 運送番号L = ", unsoL, " 運送番号M = ", unsoM, "]")

        End If

        'メッセージの設定
        MyBase.ShowMessage(frm, "G002", New String() {"確定解除", msg})

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        Return True

    End Function

#Region "再計算処理"

    ''' <summary>
    ''' 再計算処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function Silica(ByVal frm As LMF090F) As Boolean

        '処理開始
        Call Me.StartAction(frm)

        '入力チェック
        If Me._V.IsInputCheckSum(_Ds) = False Then
            Call Me.EndAction(frm)
            Return False
        End If

        '整合性チェック
        If Me._V.Seigou(frm, _Ds) = False Then
            Call Me.EndAction(frm)
            Return False
        End If

        Dim dt As DataTable = _Ds.Tables(LMF090C.TABLE_NM_OUT)
        Dim prm As LMFormData = New LMFormData()
        Dim prmDs As DataSet = New LMF810DS()
        Dim prmDt As DataTable = prmDs.Tables("LMF810UNCHIN_CALC_IN")
        Dim prmDr As DataRow = prmDt.NewRow()
        Dim tariffKbn As String = frm.cmbTariffKbn.SelectedValue.ToString()
        Dim tariffNm As String = Me.GetKbnData(tariffKbn,LMKbnConst.KBN_T015) 
        Dim wt As String = frm.numJuryo.Value.ToString()
        Dim kyori As String = frm.numKyori.Value.ToString()
        Dim kosu As String = frm.numKosu.Value.ToString()
        Dim tariffCd As String = frm.txtTariffCd.TextValue
        Dim extcCd As String = frm.txtWarimashi.TextValue
        Dim nisugataKbn As String = frm.cmbNisugata.SelectedValue.ToString()
        Dim nisugataNm As String = Me.GetKbnData(nisugataKbn, LMKbnConst.KBN_N001)
        Dim shashu As String = frm.cmbShashu.SelectedValue.ToString()
        Dim shashuNm As String = Me.GetKbnData(shashu, LMKbnConst.KBN_S012)
        Dim kiken As String = frm.cmbKiken.SelectedValue.ToString()
        Dim kikenNm As String = Me.GetKbnData(kiken, LMKbnConst.KBN_K008)
        Dim dr As DataRow = dt.Rows(0)

        Dim deciKyori As String = frm.numKyori.Value.ToString()
        If (LMFControlC.TARIFF_KONSAI).Equals(tariffKbn) = True OrElse _
            (LMFControlC.TARIFF_KURUMA).Equals(tariffKbn) = True OrElse _
            (LMFControlC.TARIFF_TOKUBIN).Equals(tariffKbn) = True Then
            Dim sime As String = String.Empty
            Dim startDate As String = String.Empty

            sime = dr.Item("UNTIN_CALCULATION_KB").ToString

            ''締め基準が01の場合
            'If LMFControlC.CALC_SHUKKA.Equals(sime) = True Then
            '    '出荷日の設定
            '    startDate = frm.imdShukka.TextValue
            'Else
            '    '納入日の設定
            '    startDate = frm.imdArr.TextValue
            'End If
            '納入日の設定
            startDate = frm.imdArr.TextValue

            Dim unchinTariffDr() As DataRow = Me._LMFConG.SelectUnchinTariffListDataRow(tariffCd, String.Empty, startDate)
            If 0 < unchinTariffDr.Length Then
                If (LMFControlC.TABTP_KOKEN).Equals(unchinTariffDr(0).Item("TABLE_TP").ToString()) OrElse _
                    (LMFControlC.TABTP_TAKEN).Equals(unchinTariffDr(0).Item("TABLE_TP").ToString()) OrElse _
                    (LMFControlC.TABTP_JYUKEN).Equals(unchinTariffDr(0).Item("TABLE_TP").ToString()) Then
                    If String.IsNullOrEmpty(dr.Item("DEST_JIS_CD").ToString) = False Then
                        deciKyori = Mid(dr.Item("DEST_JIS_CD").ToString, 1, 2)
                    Else
                        deciKyori = "0"
                    End If
                End If
            End If
        End If

        '運送日は締め日基準で切り替える
        Dim unsoDate As String = frm.imdArr.TextValue
        'If LMFControlC.CALC_SHUKKA.Equals(dr.Item("UNTIN_CALCULATION_KB").ToString()) = False Then
        '    unsoDate = frm.imdArr.TextValue
        'End If

        With prmDr

            'データテーブルのクリア
            prmDt.Clear()
            prmDr = prmDt.NewRow()

            '運賃計算プログラムのINパラメータ記入
            .Item("ACTION_FLG") = LMFControlC.FLG_OFF
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = dr.Item("CUST_CD_L").ToString()
            .Item("CUST_CD_M") = dr.Item("CUST_CD_M").ToString()
            .Item("DEST_CD") = dr.Item("DEST_CD").ToString()
            .Item("DEST_JIS") = dr.Item("DEST_JIS_CD").ToString()
            .Item("ARR_PLAN_DATE") = dr.Item("ARR_PLAN_DATE").ToString()
            .Item("UNSO_PKG_NB") = kosu
            .Item("NB_UT") = nisugataKbn
            .Item("UNSO_WT") = wt
            .Item("UNSO_ONDO_KB") = dr.Item("UNSO_ONDO_KB").ToString()
            .Item("TARIFF_BUNRUI_KB") = tariffKbn
            .Item("VCLE_KB") = shashu
            .Item("MOTO_DATA_KB") = dr.Item("MOTO_DATA_KB").ToString()
            .Item("SHIHARAI_TARIFF_CD") = tariffCd
            .Item("SHIHARAI_ETARIFF_CD") = extcCd
            .Item("UNSO_TTL_QT") = dr.Item("UNSO_TTL_QT").ToString()
            .Item("SIZE_KB") = dr.Item("SIZE_KB").ToString()
            .Item("UNSO_DATE") = unsoDate
            .Item("CARGO_KB") = ""
            .Item("CAR_TP") = "00"
            .Item("WT_LV") = 0
            .Item("KYORI") = deciKyori
            .Item("DANGER_KB") = kiken
            .Item("GOODS_CD_NRS") = ""


            'パラムに検索条件の追加
            prmDt.Rows.Add(prmDr)
            prm.ParamDataSet = prmDs

            '運賃計算プログラムの呼び出し
            LMFormNavigate.NextFormNavigate(Me, "LMF810", prm)

            '計算結果をOutのテーブルに設定
            Dim rtnDs As DataSet = prm.ParamDataSet
            Dim outTbl As DataTable = rtnDs.Tables("LMF810UNCHIN_CALC_OUT")

            'LMF810RESULTからエラーメッセージを取得する
            Dim errDr As DataRow = rtnDs.Tables("LMF810RESULT").Rows(0)
            Dim hantei As String = String.Empty

            Select Case errDr.Item("STATUS").ToString()

                Case "00"

                    '運賃取得時のみ画面へ適用する
                    Dim outDr As DataRow = outTbl.Rows(0)

                    '運賃の設定
                    dr.Item("DECI_CITY_EXTC") = Convert.ToDecimal(Me._LMFConG.FormatNumValue(outDr.Item("CITY_EXTC").ToString()))
                    dr.Item("DECI_WINT_EXTC") = Convert.ToDecimal(Me._LMFConG.FormatNumValue(outDr.Item("WINT_EXTC").ToString()))
                    dr.Item("DECI_RELY_EXTC") = Convert.ToDecimal(Me._LMFConG.FormatNumValue(outDr.Item("RELY_EXTC").ToString()))
                    dr.Item("DECI_TOLL") = Convert.ToDecimal(Me._LMFConG.FormatNumValue(outDr.Item("TOLL").ToString()))
                    dr.Item("DECI_INSU") = Convert.ToDecimal(Me._LMFConG.FormatNumValue(outDr.Item("INSU").ToString()))
                    dr.Item("DECI_UNCHIN") = Convert.ToDecimal(Me._LMFConG.FormatNumValue(Me._LMFConG.FormatNumValue(outDr.Item("UNCHIN").ToString())))

                    '画面の値を反映
                    dr.Item("DECI_NG_NB") = kosu
                    dr.Item("DECI_KYORI") = kyori
                    dr.Item("DECI_WT") = wt

                Case Else

                    '異常系(返却値からメッセージを設定)
                    MyBase.ShowMessage(frm, errDr.Item("ERROR_CD").ToString, New String() {errDr.Item("YOBI1").ToString})

                    Call Me.EndAction(frm)
                    Return False

            End Select

        End With

        '画面の値を全レコードに反映
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max

            dr = dt.Rows(i)

            dr.Item("SHIHARAI_TARIFF_BUNRUI_KB") = tariffKbn
            dr.Item("SHIHARAI_TARIFF_BUNRUI_NM") = tariffNm
            dr.Item("SHIHARAI_TARIFF_CD") = tariffCd
            dr.Item("SHIHARAI_ETARIFF_CD") = extcCd
            dr.Item("SHIHARAI_PKG_UT_KB") = nisugataKbn
            dr.Item("SHIHARAI_PKG_UT_NM") = nisugataNm
            dr.Item("SHIHARAI_SYARYO_KB") = shashu
            dr.Item("SHIHARAI_SYARYO_NM") = shashuNm
            dr.Item("SHIHARAI_DANGER_KB") = kiken
            dr.Item("SHIHARAI_DANGER_NM") = kikenNm

        Next

        'ロック解除
        Call Me.EndAction(frm)

        'スプレッドの項目設定
        Call Me._G.SetSpread(Me._Ds)

    End Function

    ''' <summary>
    ''' 区分名1を取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <returns>区分名1</returns>
    ''' <remarks></remarks>
    Private Function GetKbnData(ByVal kbnCd As String, ByVal groupCd As String) As String

        GetKbnData = String.Empty
        Dim drs As DataRow() = Me._LMFConG.SelectKbnListDataRow(kbnCd, groupCd)
        If 0 < drs.Length Then
            GetKbnData = drs(0).Item("KBN_NM1").ToString()
        End If

        Return GetKbnData

    End Function

    ''' <summary>
    ''' スキップ処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function skipData(ByVal frm As LMF090F, ByVal eventShubetsu As LMF090C.EventShubetsu) As Boolean

        If (Me._RenzokuCnt).Equals(Me._DsInData.Tables(LMF090C.TABLE_NM_IN).Rows.Count - 1) = True Then
            '最終データの場合はフォームを閉じる
            Me._RenzokuLastFlg = True
            frm.Close()
            Exit Function
        End If

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMF090C.EventShubetsu.MAIN, Me._RenzokuFlg)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの検索行に初期値を設定する
        Call Me._G.InitSpread()

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G007")

        'フォーカスの設定
        Call Me._G.SetFoucus()


        '閾値の設定
        MyBase.SetLimitCount(Me._LMFConG.GetLimitData())


        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetMaxResultCount(mc)



        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================

        Dim ds As DataSet = Me.SetDataSetInData(frm, New LMF090DS())
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)

        '処理するレコード件数目を設定
        Me._RenzokuCnt = Me._RenzokuCnt + 1
        Me._DsInData.Tables(LMF090C.TABLE_NM_IN).Rows(0).Item("RENZOKU_CNT") = Me._RenzokuCnt

        Me._Ds = MyBase.CallWSA(blf, LMF090C.ACTION_ID_SELECT, Me._DsInData)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

        Else

            'メッセージエリアの設定(0件エラー)
            MyBase.ShowMessage(frm)

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {MyBase.GetResultCount.ToString()})

        End If

        MyBase.ShowMessage(frm, "G003")

        '荷主、出荷日のdrから取得
        Dim dt As DataTable = Me._Ds.Tables(LMF090C.TABLE_NM_OUT)
        Dim max As Integer = dt.Rows.Count - 1

        Dim shiharaiGrouopNo As String = String.Empty
        Dim shiharaiGrouopNoM As String = String.Empty

        Dim unsoNoL As String = String.Empty
        Dim unsoNoM As String = String.Empty

        'キャッシュから名称を取得
        Dim drs As DataRow() = Nothing
        Dim dr As DataRow = Nothing

        For i As Integer = 0 To max

            dr = dt.Rows(i)

            shiharaiGrouopNo = dr.Item("SHIHARAI_GROUP_NO").ToString
            shiharaiGrouopNoM = dr.Item("SHIHARAI_GROUP_NO_M").ToString
            unsoNoL = dr.Item("UNSO_NO_L").ToString
            unsoNoM = dr.Item("UNSO_NO_M").ToString

            'まとめか判定
            If String.IsNullOrEmpty(shiharaiGrouopNo) = False _
                AndAlso String.IsNullOrEmpty(shiharaiGrouopNoM) = False Then

                '荷主がまとめの時は親を表示
                If shiharaiGrouopNo.Equals(unsoNoL) = True _
                    AndAlso shiharaiGrouopNoM.Equals(unsoNoM) = True Then
                    Exit For
                End If

            End If

        Next

        '初期設定
        Call Me._G.SetInitValu(dr)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        '値の設定
        Call Me._G.SetSpread(Me._Ds)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        '画面の入力項目ロック
        Call Me._G.LockControl(True)

        '編集モードにする
        Me.ShiftEditMode(frm)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Function

#End Region

#Region "按分処理"

    ''' <summary>
    ''' 按分処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUncinData(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF090C.TABLE_NM_OUT)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1
        Dim soJuryo As Decimal = 0

        '合計値を取得
        For i As Integer = 0 To max

            '重量の合計
            soJuryo += Convert.ToDecimal(dt.Rows(i).Item("DECI_WT").ToString())

        Next

        '親レコード確定情報
        Dim unchinDr As DataRow = dt.Rows(0)
        Dim dUnchin As Decimal = Convert.ToDecimal(Me._LMFConG.FormatNumValue(unchinDr.Item("DECI_UNCHIN").ToString()))
        Dim dCity As Decimal = Convert.ToDecimal(Me._LMFConG.FormatNumValue(unchinDr.Item("DECI_CITY_EXTC").ToString()))
        Dim dWint As Decimal = Convert.ToDecimal(Me._LMFConG.FormatNumValue(unchinDr.Item("DECI_WINT_EXTC").ToString()))
        Dim dRely As Decimal = Convert.ToDecimal(Me._LMFConG.FormatNumValue(unchinDr.Item("DECI_RELY_EXTC").ToString()))
        Dim dToll As Decimal = Convert.ToDecimal(Me._LMFConG.FormatNumValue(unchinDr.Item("DECI_TOLL").ToString()))
        Dim dInsu As Decimal = Convert.ToDecimal(Me._LMFConG.FormatNumValue(unchinDr.Item("DECI_INSU").ToString()))

        Dim unchin As Decimal = 0
        Dim city As Decimal = 0
        Dim wint As Decimal = 0
        Dim rely As Decimal = 0
        Dim toll As Decimal = 0
        Dim insu As Decimal = 0
        Dim juryo As Decimal = 0
        Dim value As Decimal = 0

        For i As Integer = 0 To max

            dr = dt.Rows(i)

            '重量の設定
            juryo = Convert.ToDecimal(Me._LMFConG.FormatNumValue(dr.Item("DECI_WT").ToString()))

            '運賃の按分
            value = Me.AnbunData(dUnchin, soJuryo, juryo)
            unchin += value
            dr.Item("KANRI_UNCHIN") = value

            '都市の按分
            value = Me.AnbunData(dCity, soJuryo, juryo)
            city += value
            dr.Item("KANRI_CITY_EXTC") = value

            '冬期の按分
            value = Me.AnbunData(dWint, soJuryo, juryo)
            wint += value
            dr.Item("KANRI_WINT_EXTC") = value

            '中継料の按分
            value = Me.AnbunData(dRely, soJuryo, juryo)
            rely += value
            dr.Item("KANRI_RELY_EXTC") = value

            '通行料の按分
            value = Me.AnbunData(dToll, soJuryo, juryo)
            toll += value
            dr.Item("KANRI_TOLL") = value

            '保険料の按分
            value = Me.AnbunData(dInsu, soJuryo, juryo)
            insu += value
            dr.Item("KANRI_INSU") = value

        Next

        '端数分を親レコードに設定
        unchinDr.Item("KANRI_UNCHIN") = Convert.ToDecimal(Me._LMFConG.FormatNumValue(unchinDr.Item("KANRI_UNCHIN").ToString())) + dUnchin - unchin
        unchinDr.Item("KANRI_CITY_EXTC") = Convert.ToDecimal(Me._LMFConG.FormatNumValue(unchinDr.Item("KANRI_CITY_EXTC").ToString())) + dCity - city
        unchinDr.Item("KANRI_WINT_EXTC") = Convert.ToDecimal(Me._LMFConG.FormatNumValue(unchinDr.Item("KANRI_WINT_EXTC").ToString())) + dWint - wint
        unchinDr.Item("KANRI_RELY_EXTC") = Convert.ToDecimal(Me._LMFConG.FormatNumValue(unchinDr.Item("KANRI_RELY_EXTC").ToString())) + dRely - rely
        unchinDr.Item("KANRI_TOLL") = Convert.ToDecimal(Me._LMFConG.FormatNumValue(unchinDr.Item("KANRI_TOLL").ToString())) + dToll - toll
        unchinDr.Item("KANRI_INSU") = Convert.ToDecimal(Me._LMFConG.FormatNumValue(unchinDr.Item("KANRI_INSU").ToString())) + dInsu - insu

        Return ds

    End Function

    ''' <summary>
    ''' 按分計算
    ''' </summary>
    ''' <param name="unchin">運賃</param>
    ''' <param name="soJuryo">総重量</param>
    ''' <param name="juryo">重量</param>
    ''' <returns>按分結果</returns>
    ''' <remarks>通常レコードの場合、自レコードの重量と総重量は同値になるので計算してよい</remarks>
    Private Function AnbunData(ByVal unchin As Decimal, ByVal soJuryo As Decimal, ByVal juryo As Decimal) As Decimal

        '総重量がZeroの場合、割当て無し
        If soJuryo = 0 Then
            Return 0
        End If

        '運賃 * 自レコードの重量 / 総重量(切捨て)
        Return System.Math.Floor(unchin * juryo / soJuryo)

    End Function

#End Region

#Region "タリフ分類区分値変更"

    Private Sub Bunrui(ByVal frm As LMF090F)

        'ロック制御
        Call Me._G.Locktairff()

        '終了メッセージ表示
        MyBase.ShowMessage(frm, "G003")

        '終了処理
        Call Me.EndAction(frm)

    End Sub

#End Region

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub CloseForm(ByVal frm As LMF090F, ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        If Me._RenzokuLastFlg = True Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveUncoItemData(frm, LMF090C.EventShubetsu.TOJIRU) = False Then

                    e.Cancel = True

                End If

            Case MsgBoxResult.Cancel                  '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

#Region "Pop"
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

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMF090F, ByVal objNm As String, ByVal actionType As LMF090C.EventShubetsu) As Boolean

        With frm

            Dim tariff As String = .cmbTariffKbn.SelectedValue.ToString
            Dim hikaku As String = "40"
            '処理開始アクション
            Call Me.StartAction(frm)


            Select Case objNm

                Case .txtTariffCd.Name

                    '横持ちの場合
                    If hikaku.Equals(tariff) = True Then

                        '横持ちタリフ
                        Call Me.YokoTariffPop(frm, actionType)

                    Else

                        'タリフコード
                        Call Me.TariffPop(frm, actionType)

                    End If
                Case .txtWarimashi.Name

                    '割増コード
                    Call Me.WarimashiPop(frm, actionType)


            End Select

        End With

        Return True

    End Function

#Region "起動Popタリフマスタ照会、割増タリフマスタ照会のPGが完了しているか確認"

    ''' <summary>
    ''' 支払タリフマスタ照会(LMZ290)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function TariffPop(ByVal frm As LMF090F, ByVal actinType As LMF090C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowTariffPopup(frm, actinType)

        If prm.ReturnFlg = True Then
            'タリフマスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ290C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtTariffCd.TextValue = dr.Item("SHIHARAI_TARIFF_CD").ToString()
                .lblTariffNm.TextValue = dr.Item("SHIHARAI_TARIFF_REM").ToString()
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
    Private Function ShowTariffPopup(ByVal frm As LMF090F, ByVal actinType As LMF090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ290DS()
        Dim dt As DataTable = ds.Tables(LMZ290C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        '運賃締め日基準の取得
        Dim Prmdt As DataTable = _Ds.Tables(LMF090C.TABLE_NM_OUT)
        Dim Prmdr As DataRow = Prmdt.Rows(0)
        Dim sime As String = String.Empty
        Dim data As String = String.Empty

        'sime = Prmdr.Item("UNTIN_CALCULATION_KB").ToString

        ''締め基準が01の場合
        'If LMFControlC.CALC_SHUKKA.Equals(sime) = True Then

        '    '出荷日の設定
        '    data = frm.imdShukka.TextValue

        'Else

        '    '納入日の設定
        '    data = frm.imdArr.TextValue

        'End If
        '納入日の設定
        data = frm.imdArr.TextValue

        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("STR_DATE") = data
            If actinType = LMF090C.EventShubetsu.ENTER Then
                .Item("SHIHARAI_TARIFF_CD") = frm.txtTariffCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFConH.FormShow(ds, "LMZ290", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 割増タリフマスタ照会(LMZ300)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function WarimashiPop(ByVal frm As LMF090F, ByVal actinType As LMF090C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowWarimashiPopup(frm, actinType)

        If prm.ReturnFlg = True Then
            '割増タリフマスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ300C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtWarimashi.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()
                .lblWarimashi.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()
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
    Private Function ShowWarimashiPopup(ByVal frm As LMF090F, ByVal actinType As LMF090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ300DS()
        Dim dt As DataTable = ds.Tables(LMZ300C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            If actinType = LMF090C.EventShubetsu.ENTER Then
                .Item("EXTC_TARIFF_CD") = frm.txtWarimashi.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFConH.FormShow(ds, "LMZ300", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 支払横持ちタリフマスタ照会(LMZ320)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function YokoTariffPop(ByVal frm As LMF090F, ByVal actionType As LMF090C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowYokoTariffPopup(frm, actionType)

        If prm.ReturnFlg = True Then
            '支払横持ちタリフマスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ320C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtTariffCd.TextValue = dr.Item("YOKO_TARIFF_CD").ToString()
                .lblTariffNm.TextValue = dr.Item("YOKO_REM").ToString()

            End With
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 支払横持ちタリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowYokoTariffPopup(ByVal frm As LMF090F, ByVal actionType As LMF090C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ320DS()
        Dim dt As DataTable = ds.Tables(LMZ320C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            If actionType = LMF090C.EventShubetsu.ENTER Then
                .Item("YOKO_TARIFF_CD") = frm.txtTariffCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFConH.FormShow(ds, "LMZ320", "", Me._PopupSkipFlg)

    End Function

#End Region

#End Region

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMF090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Me.ShiftEditMode(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し(確定)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByVal frm As LMF090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Me.Kakutei(frm, LMF090C.EventShubetsu.KAKUTEI)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し(確定解除)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByVal frm As LMF090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Me.KakuteiKaijo(frm, LMF090C.EventShubetsu.KAKUTEIKAIJO)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し(スキップ処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByVal frm As LMF090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Me.skipData(frm, LMF090C.EventShubetsu.SKIP)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMF090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMF090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SaveUncoItemData(frm, LMF090C.EventShubetsu.SAVE)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMF090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 「再計算」ボタン押下時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub ButtonSilica(ByVal frm As LMF090F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Me.Silica(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' タリフ分類クリック時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub Bunrui(ByVal frm As LMF090F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Me.Bunrui(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMF090F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CloseForm(frm, e)

        Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub KeyDown(ByVal frm As LMF090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "データセット"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMF090F, ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF090C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With frm
            'パラメータはLMF080のものを検索条件とする

            dt.Rows.Add(dr)

        End With

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Function SetDatasetUp(ByVal frm As LMF090F, ByVal ds As DataSet) As DataSet

        Dim spr As SheetView = frm.sprDetail.ActiveSheet

        With spr

            Dim dt As DataTable = ds.Tables(LMF090C.TABLE_NM_OUT)
            Dim dr As DataRow = dt.Rows(0)
            Dim syayo As String = String.Empty
            Dim pkg As Decimal = 0
            Dim danger As String = String.Empty
            Dim tariff As String = String.Empty
            Dim warimasi As String = String.Empty
            Dim tariffkb As String = String.Empty
            Dim wt As Decimal = 0
            Dim kyori As Decimal = 0
            Dim unchin As Decimal = 0
            Dim toshi As Decimal = 0
            Dim toki As Decimal = 0
            Dim thukei As Decimal = 0
            Dim koso As Decimal = 0
            Dim sonota As Decimal = 0
            Dim biko As String = String.Empty
            Dim zei As String = String.Empty
            Dim max As Integer = spr.Rows.Count - 1
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max

                '親レコードを特定
                If Me.SetDataJug(spr, i) = False Then
                    Continue For
                End If

                syayo = Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.SYARYO_KB.ColNo)).ToString()
                pkg = Convert.ToDecimal(Me._LMFConG.FormatNumValue(Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.KOSU.ColNo)).ToString()))
                danger = Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.DANGER_KB.ColNo)).ToString()
                tariff = Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.TARIFF_CD.ColNo)).ToString()
                warimasi = Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.WARIMASHI.ColNo)).ToString()
                tariffkb = Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.TARIF_BUN.ColNo)).ToString()
                wt = Convert.ToDecimal(Me._LMFConG.FormatNumValue(Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.JURYO.ColNo)).ToString()))
                kyori = Convert.ToDecimal(Me._LMFConG.FormatNumValue(Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.KYORI.ColNo)).ToString()))
                unchin = Convert.ToDecimal(Me._LMFConG.FormatNumValue(Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.UNCHIN.ColNo)).ToString()))
                toshi = Convert.ToDecimal(Me._LMFConG.FormatNumValue(Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.TOSHI.ColNo)).ToString()))
                toki = Convert.ToDecimal(Me._LMFConG.FormatNumValue(Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.TOKI.ColNo)).ToString()))
                thukei = Convert.ToDecimal(Me._LMFConG.FormatNumValue(Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.TYUKEI.ColNo)).ToString()))
                koso = Convert.ToDecimal(Me._LMFConG.FormatNumValue(Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.KOSO.ColNo)).ToString()))
                sonota = Convert.ToDecimal(Me._LMFConG.FormatNumValue(Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.SONOTA.ColNo)).ToString()))
                biko = Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.REMARK.ColNo)).ToString()
                zei = Me._LMFConG.GetCellValue(.Cells(i, LMF090G.sprDetailDef.ZEI_KB.ColNo)).ToString()

                rowNo = i

                '親レコードの情報を取得したらループ終了
                Exit For

            Next

            Dim sumWt As Decimal = 0
            For j As Integer = 0 To max
                If rowNo <> j Then
                    sumWt = sumWt + Convert.ToDecimal(Me._LMFConG.FormatNumValue(Me._LMFConG.GetCellValue(.Cells(j, LMF090G.sprDetailDef.JURYO.ColNo)).ToString()))
                End If
            Next

            '重量のマイナスチェックを行う
            Me._ErrFlg = False
            If Me._V.IsSeiqwtMinusChk(wt, sumWt) = False Then
                Me._ErrFlg = True
                Return ds
            End If

            dr.Item("SHIHARAI_SYARYO_KB") = syayo
            dr.Item("DECI_NG_NB") = pkg
            dr.Item("SHIHARAI_DANGER_KB") = danger
            dr.Item("SHIHARAI_TARIFF_CD") = tariff
            dr.Item("SHIHARAI_ETARIFF_CD") = warimasi
            dr.Item("SHIHARAI_TARIFF_BUNRUI_KB") = tariffkb
            dr.Item("DECI_WT") = wt
            dr.Item("DECI_KYORI") = kyori
            dr.Item("DECI_CITY_EXTC") = toshi
            dr.Item("DECI_WINT_EXTC") = toki
            dr.Item("DECI_RELY_EXTC") = thukei
            dr.Item("DECI_TOLL") = koso
            dr.Item("DECI_INSU") = sonota
            dr.Item("DECI_UNCHIN") = unchin
            dr.Item("REMARK") = biko
            dr.Item("SHIHARAI_WT") = wt - sumWt

            '税区分は全行に反映
            max = dt.Rows.Count - 1
            For i As Integer = 0 To max
                dt.Rows(i).Item("TAX_KB") = zei
            Next

            Return ds

        End With

    End Function

    ''' <summary>
    ''' 設定する行を特定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:設定する行　False:設定しない行</returns>
    ''' <remarks></remarks>
    Private Function SetDataJug(ByVal spr As SheetView, ByVal rowNo As Integer) As Boolean

        With spr


            Dim groupL As String = Me._LMFConG.GetCellValue(.Cells(rowNo, LMF090G.sprDetailDef.GROUP.ColNo))

            '通常レコードの場合、True
            If String.IsNullOrEmpty(groupL) = True Then
                Return True
            End If

            '親レコードの場合、True
            If groupL.Equals(Me._LMFConG.GetCellValue(.Cells(rowNo, LMF090G.sprDetailDef.UNSO_NO.ColNo))) _
                AndAlso Me._LMFConG.GetCellValue(.Cells(rowNo, LMF090G.sprDetailDef.GROUP_UNSO.ColNo)).Equals(Me._LMFConG.GetCellValue(.Cells(rowNo, LMF090G.sprDetailDef.UNSO_NO_EDA.ColNo))) = True _
                Then
                Return True
            End If

            Return False

        End With

    End Function

#End Region

#Region "ユーティリティ(共通クラスでメソッドが作成されたら共通クラスを使用(開始、終了アクション)"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMF090F)

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
    Private Sub EndAction(ByVal frm As LMF090F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "メッセージ設定(共通クラスでメソッドができたら共通クラスを使用)"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMF090F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find(LMFControlC.MES_AREA, True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetGMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' ガイダンスメッセージを設定する
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetGMessage(ByVal frm As LMF090F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">メッセージ置換文字列(処理名)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmMsg(ByVal frm As LMF090F, ByVal msg As String) As Boolean

        '確認メッセージ
        If MyBase.ShowMessage(frm, "W003", New String() {msg}) = MsgBoxResult.Cancel Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G003")
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 確定処理続行確認
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">メッセージ置換文字列(処理名)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmIKakuteiMsg(ByVal frm As LMF090F, ByVal msg As String) As Boolean

        '確認メッセージ
        If MyBase.ShowMessage(frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G003")
            Return False

        End If

        Return True

    End Function

#End Region

#End Region 'Method

End Class
