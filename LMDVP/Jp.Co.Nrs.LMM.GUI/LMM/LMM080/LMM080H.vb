' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM080H : 運送会社マスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI

''' <summary>
''' LMM080ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM080H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM080V
    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM080G

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConV As LMMControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConH As LMMControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConG As LMMControlG

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

    ''' <summary>
    ''' ParameterDS格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    '''CUSTRPT情報格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CustRptDt As DataTable

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>    
    Private _Ds As DataSet

    ''' <summary>
    '''検索結果格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _OutDs As DataSet

    ''' <summary>
    ''' システム日付の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _sysDate As String

    ''' <summary>
    '''表示用データテーブル格納
    ''' </summary>
    ''' <remarks></remarks>
    Private _DispDt As DataTable

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
        Dim frm As LMM080F = New LMM080F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMMConG = New LMMControlG(frm)

        'Validate共通クラスの設定
        Me._LMMConV = New LMMControlV(Me, sForm, Me._LMMConG)

        'Hnadler共通クラスの設定
        Me._LMMConH = New LMMControlH(MyBase.GetPGID(), Me._LMMConV, Me._LMMConG)

        'Validateクラスの設定
        Me._V = New LMM080V(Me, frm, Me._LMMConV)

        'Gamenクラスの設定
        Me._G = New LMM080G(Me, frm, Me._LMMConG)

        'フォームの初期化
        MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '2011/08/11 福田 共通動作(右セル移動不可) スタート
        'Enter押下イベント設定
        'Call Me._LMMConH.SetEnterEvent(frm)
        '2011/08/11 福田 共通動作(右セル移動不可) エンド

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'スプレッドの検索行に初期値を設定する
        Call Me._G.SetInitValue(frm)

#If True Then ' 名鉄対応(2499) 2016.1.29 added inoue
        ' コンボボックスのItemsの設定
        Call Me._G.SetCmbItems()
#End If

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM080C.EventShubetsu.MAIN)

        'システム日付
        Me._sysDate = MyBase.GetSystemDateTime(0)

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 新規処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NewDataEvent(ByVal frm As LMM080F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM080C.EventShubetsu.SHINKI) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面全ロックの解除
        MyBase.UnLockedControls(frm)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        '画面入力項目のクリア
        Call Me._LMMConG.ClearControl(frm)

        'コンボボックスの値の設定
        Call Me._G.SetcmbValue()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G003")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM080C.EventShubetsu.SHINKI)

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub EditDataEvent(ByVal frm As LMM080F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM080C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '画面の営業所コード・運送会社コード・支店コード
        Dim nrsBrCd As String = Me._G.NrsBrCdSet
        Dim unsoCd As String = Me._G.UnsocoCdSet
        Dim unsoBrCd As String = Me._G.UnsocoBrCdSet

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, unsoCd) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'レコードステータスチェック
        If Me._V.IsRecordStatusChk() = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(LMM080C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        '編集部の内容を設定
        Call Me.SetDatasetItemData(frm)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM080BLF", "HaitaData", Me._Ds)

        'システム日付取得
        Call Me.SysDateset(rtnDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm)

            '終了処理
            Call Me._LMMConH.EndAction(frm)

        Else

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G003")

            '終了処理
            Call Me._LMMConH.EndAction(frm)

            'モード・ステータスの設定
            Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey()

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus()

            '明細Spreadの設定
            Call Me._G.SetSpreadDetail(Me._DispDt)

            'フォーカスの設定
            Call Me._G.SetFoucus(LMM080C.EventShubetsu.HENSHU)

        End If

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent(ByVal frm As LMM080F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM080C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtUnsocoCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(LMM080C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        '画面の営業所コード・運送会社コード・支店コード
        Dim nrsBrCd As String = Me._G.NrsBrCdSet
        Dim unsoCd As String = Me._G.UnsocoCdSet
        Dim unsoBrCd As String = Me._G.UnsocoBrCdSet


        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.COPY_REC)

        'ファンクションキーの設定   
        Call Me._G.SetFunctionKey()

        '編集部の項目複写
        Call Me._G.SetControlsStatus()

        '明細Spreadの設定
        Call Me._G.SetSpreadDetail(Me._DispDt)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM080C.EventShubetsu.HUKUSHA)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G003")

    End Sub

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent(ByVal frm As LMM080F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM080C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '画面の運送会社コード・支店コード
        Dim unsoCd As String = Me._G.UnsocoCdSet
        Dim unsoBrCd As String = Me._G.UnsocoBrCdSet

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, unsoCd) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(LMM080C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        '2016.01.06 UMANO 英語化対応START
        Dim str As String() = Split(frm.FunctionKey.F4ButtonName, "･")
        '2016.01.06 UMANO 英語化対応END

        '削除フラグチェック
        If frm.lblSituation.RecordStatus.Equals(LMConst.FLG.OFF) Then
            '2016.01.06 UMANO 英語化対応START
            'Select Case MyBase.ShowMessage(frm, "C001", New String() {"削除"})
            Select MyBase.ShowMessage(frm, "C001", New String() {str(0)})
                '2016.01.06 UMANO 英語化対応END
                Case MsgBoxResult.Cancel '「キャンセル」押下時
                    Call Me._LMMConH.EndAction(frm)
                    'メッセージ表示
                    MyBase.ShowMessage(frm, "G013")
                    Exit Sub
            End Select
        Else
            '2016.01.06 UMANO 英語化対応START
            'Select Case MyBase.ShowMessage(frm, "C001", New String() {"復活"})
            Select MyBase.ShowMessage(frm, "C001", New String() {str(1)})
                '2016.01.06 UMANO 英語化対応END
                Case MsgBoxResult.Cancel  '「キャンセル」押下時
                    Call Me._LMMConH.EndAction(frm)
                    'メッセージ表示
                    MyBase.ShowMessage(frm, "G013")
                    Exit Sub
            End Select
        End If

        'DataSet設定
        Me._Ds = New LMM080DS()
        Call Me.SetDatasetDelData(frm, Me._Ds)

        '明細Spreadの内容を設定
        Dim dtCust As DataTable = Me._Ds.Tables(LMM080C.TABLE_NM_CUSTRPT)
        dtCust = Me._DispDt.Copy()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM080BLF", "DeleteData", Me._Ds)

        'システム日付取得
        Call Me.SysDateset(rtnDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.UNSOCO)
        MyBase.LMCacheMasterData(LMConst.CacheTBL.UNSO_CUST_RPT)

        'メッセージ用エリアコード格納
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName.Replace("　", String.Empty) _
        '                                              , String.Concat("[", LMM080C.UNSOCD, " = ", unsoCd _
        '                                                              , "-", unsoBrCd, "]")})
        MyBase.ShowMessage(frm, "G081", New String() {frm.FunctionKey.F4ButtonName.Replace("　", String.Empty), String.Concat(unsoCd, "-", unsoBrCd)})
        '2016.01.06 UMANO 英語化対応END

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMM080F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM080C.EventShubetsu.KENSAKU) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuInputChk = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '編集部クリアフラグ
        Dim clearFlg As Integer = 0

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMMConH.EndAction(frm) '終了処理
                'メッセージ設定
                Call Me.SetGMessage(frm)
                Exit Sub
            Else      'OK押下
                clearFlg = 1
            End If
        End If

        '検索処理を行う
        Call Me.SelectData(frm, clearFlg)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM080C.EventShubetsu.KENSAKU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave(ByVal frm As LMM080F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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

        Call Me.RowSelection(frm, rowNo)

    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMM080F, ByVal rowNo As Integer)

        Dim nrsBrCd As String = Me._G.NrsBrCdSetFromSpread(rowNo)
        Dim unsoCd As String = Me._G.UnsocoCdSetFromSpread(rowNo)
        Dim unsoBrCd As String = Me._G.UnsocoBrCdSetFromSpread(rowNo)

        Dim dt2 As DataTable = Me._CustRptDt

        '権限チェック
        If Me._V.IsAuthorityChk(LMM080C.EventShubetsu.DCLICK) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._LMMConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMM080G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'ステータス設定
        Me._G.SetModeAndStatus(, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)

        '明細Spreadに設定する内容を保持
        Call Me.GetDtlDisplayData(frm)

        '明細Spreadにデータ表示
        Call Me._G.SetSpreadDetail(Me._DispDt)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G013")

    End Sub

    ''' <summary>
    ''' 明細スプレッド表示用にDataTaleを編集
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDtlDisplayData(ByVal frm As LMM080F)

        With frm

            Dim dt As DataTable = Me._OutDs.Tables(LMM080C.TABLE_NM_CUSTRPT)

            '表示対象データを取得
            Dim sort As String = Me._G.SetDataTableSort()
            Dim sql As String = Me._G.SetCustRptSql(Me._G.NrsBrCdSet, Me._G.UnsocoCdSet, Me._G.UnsocoBrCdSet)
            Dim setCustEmpDrs As DataRow() = dt.Select(String.Concat(sql, " AND CUST_CD_L = '' "), sort)
            Dim setCustDrs As DataRow() = dt.Select(String.Concat(sql, " AND CUST_CD_L <> '' "), sort)
            Dim orderBy As String = sort

            '保持用テーブルにデータ格納
            Dim maxCustEmpDrs As Integer = setCustEmpDrs.Length - 1
            Dim maxCustDrs As Integer = setCustDrs.Length - 1
            Dim setDS As DataSet = New LMM080DS()
            Me._DispDt = setDS.Tables(LMM080C.TABLE_NM_CUSTRPT)
            For i As Integer = 0 To maxCustEmpDrs
                Me._DispDt.ImportRow(setCustEmpDrs(i))
            Next
            For i As Integer = 0 To maxCustDrs
                Me._DispDt.ImportRow(setCustDrs(i))
            Next

        End With

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent(ByVal frm As LMM080F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMM080C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM080C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMM080C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SaveItemData(ByVal frm As LMM080F, ByVal eventShubetsu As LMM080C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMM080C.EventShubetsu.HOZON)

        '単項目チェック
        rtnResult = rtnResult AndAlso Me._V.IsSaveInputChk()

        '画面の運送会社コードなどを変数に格納
        Dim nrsBrCd As String = Me._G.NrsBrCdSet
        Dim unsoCd As String = Me._G.UnsocoCdSet
        Dim unsoBrCd As String = Me._G.UnsocoBrCdSet

        'スプレッド並び替え処理
        Call Me._G.sprDetailSortColumnCommand()

        'DataSet設定
        Call Me.SetDatasetItemData(frm)

        '枝番限界チェック
        rtnResult = rtnResult AndAlso Me._V.IsEdabanChk(Me._Ds)

        If rtnResult = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Return False
        End If

        '複数件チェック
        If Me._V.IsFukusuChk(Me._Ds) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Return False
        End If

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateInsertData")

        '==========================
        'WSAクラス呼出
        '==========================
        'レコードステータスの判定
        Dim rtnDs As DataSet

        Select Case frm.lblSituation.RecordStatus
            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                '新規登録処理
                rtnDs = MyBase.CallWSA("LMM080BLF", "InsertData", Me._Ds)
            Case Else
                '更新処理(排他処理)
                rtnDs = MyBase.CallWSA("LMM080BLF", "UpdateData", Me._Ds)
        End Select

        'システム日付取得
        Call Me.SysDateset(rtnDs)

        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then
            '2011.09.08 検証結果_導入時要望№1対応 START
            'If MyBase.GetMessageID().Equals("E079") = True Then
            '    Dim zip As String = frm.txtZip.TextValue
            '    MyBase.SetMessage("E079", New String() {"郵便番号マスタ", zip})
            '    Call Me._LMMConV.SetErrorControl(frm.txtZip)
            'End If
            'MyBase.ShowMessage(frm)
            'Call Me._LMMConH.EndAction(frm)  '終了処理
            'Return False
            '2011.09.08 検証結果_導入時要望№1対応 END
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateInsertData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.UNSOCO)
        MyBase.LMCacheMasterData(LMConst.CacheTBL.UNSO_CUST_RPT)

        '処理結果メッセージ表示
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
        '                                              , String.Concat("[", LMM080C.UNSOCD, " = ", unsoCd _
        '                                                              , "-", unsoBrCd, "]")})
        MyBase.ShowMessage(frm, "G081", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
                                                      , String.Concat(unsoCd, "-", unsoBrCd)})
        '2016.01.06 UMANO 英語化対応END

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM080C.EventShubetsu.MAIN)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal frm As LMM080F, ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveItemData(frm, LMM080C.EventShubetsu.TOJIRU) = False Then

                    e.Cancel = True

                End If


            Case MsgBoxResult.Cancel                  '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMM080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM080C.EventShubetsu.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM080C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMMConH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMM080C.EventShubetsu.ENTER)

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'フォーカス移動処理
        Call Me._LMMConH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 行追加
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub RowAdd(ByVal frm As LMM080F)

        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'Dim nrsBrCd As String = LMUserInfoManager.GetNrsBrCd()
        Dim nrsBrCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()
        Dim unsoCd As String = frm.txtUnsocoCd.TextValue
        Dim unsoBrCd As String = frm.txtUnsocoBrCd.TextValue
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty

        '並び替え
        Call Me._G.sprDetailSortColumnCommand()

        'スプレッド明細行存在チェック
        If Me._V.SprRowExistChk() = True Then

            'マスタ参照(行あるとき)
            Call Me.ShowPopupControl(frm, frm.btnRowAdd.Name, LMM080C.EventShubetsu.INS_T)

        Else
            '処理開始アクション
            Call Me._LMMConH.StartAction(frm)

            '空行追加
            Call Me.RowAddJisDt(frm, custCdL, custCdM)
        End If

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

    End Sub

    ''' <summary>
    ''' 行削除
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub RowDel(ByVal frm As LMM080F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        'チェックリスト取得
        Dim arr As ArrayList = Nothing

        '並び替え
        Call Me._G.sprDetailSortColumnCommand()

        arr = Me._LMMConH.GetCheckList(frm.sprDetail2.ActiveSheet, LMM080G.sprDetailDef2.DEF.ColNo)

        '未選択チェック
        Dim rtnResult As Boolean = Me._LMMConV.IsSelectChk(arr.Count)

        '削除不可チェック
        rtnResult = rtnResult AndAlso Me._V.RowDelChk(arr)

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        '選択行を削除する
        Call Me._G.DelateDtl(arr)

        'メッセージの表示
        MyBase.ShowMessage(frm, "G003")

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM080C.EventShubetsu.DEL_T)

    End Sub

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMM080F, ByVal clearflg As Integer)

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble( _
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))

        MyBase.SetLimitCount(lc)

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1")))

        MyBase.SetMaxResultCount(mc)

        'DataSet設定
        Me._FindDs = New LMM080DS()
        Call Me.SetDataSetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, "LMM080BLF", "SelectListData", _FindDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, _
        '                                         "LMM080BLF", "SelectListData", _FindDs _
        '                                 , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
        '                                 (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
        '                                  , , Convert.ToInt32(Convert.ToDouble( _
        '                                     MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
        '                                     .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))
        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then

            'システム日付取得
            Call Me.SysDateset(rtnDs)

            Call Me.SuccessSelect(frm, rtnDs)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        If clearflg = 1 Then
            Call Me._G.SetControlsStatus()
        End If

        'ファンクションキーの制御
        Call Me._G.SetFunctionKey()

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMM080F, ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        Dim dt As DataTable = ds.Tables(LMM080C.TABLE_NM_OUT)
        Dim dt2 As DataTable = ds.Tables(LMM080C.TABLE_NM_CUSTRPT)

        '画面解除
        MyBase.UnLockedControls(frm)

        'SPREAD(表示行・明細)初期化
        frm.sprDetail.CrearSpread()
        frm.sprDetail2.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        '取得データ(CUSTRPT)をPrivate変数に保持
        Call Me.SetDataSetCustRptData(dt2)

        Me._CntSelect = dt.Rows.Count.ToString()

        If LMConst.FLG.OFF.Equals(Me._CntSelect) = False Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})
        End If

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ステータスの設定
        Call Me._G.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' データテーブル(運送会社荷主別)に行追加
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function RowAddJisDt(ByVal frm As LMM080F _
                                 , ByVal custCdL As String _
                                 , ByVal custCdM As String _
                                 ) As Boolean

        Dim ds As DataSet = New LMM080DS()
        Dim dt As DataTable = ds.Tables(LMM080C.TABLE_NM_CUSTRPT)

        Dim dRow As DataRow = Nothing

        For i As Integer = 0 To 1

            dRow = dt.NewRow()

            With frm

                dRow("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dRow("UNSOCO_CD") = .txtUnsocoCd.TextValue
                dRow("UNSOCO_BR_CD") = String.Empty
                dRow("MOTO_TYAKU_KB") = String.Concat("0", (i + 1).ToString())
                dRow("PTN_CD") = String.Empty
#If False Then ' 名鉄対応(2499) 2016.1.27 changed inoue
                dRow("PTN_ID") = LMM080C.INVOICE_PTNID
#Else
                dRow("PTN_ID") = frm.cmbAddPtnId.SelectedValue
#End If
                dRow("CUST_CD_L") = custCdL
                dRow("CUST_CD_M") = custCdM

            End With

            dt.Rows.Add(dRow)

        Next

        Me._G.AddRow(dt)

        Return True

    End Function

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMM080F)

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
    Private Sub SetGMessage(ByVal frm As LMM080F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub


#End Region

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="eventshubetsu">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMM080F, ByVal objNm As String, ByVal eventshubetsu As LMM080C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me._LMMConH.StartAction(frm)

            Select Case objNm

                Case .txtZip.Name

                    Call Me.SetReturnZipPop(frm, objNm, eventshubetsu)

                Case .txtUnchinTariffCd.Name

                    Call Me.SetReturnTariffPop(frm, eventshubetsu)

                Case .txtExtcTariffCd.Name

                    Call Me.SetReturnExtcPop(frm, eventshubetsu)

                Case .txtKyoriCd.Name

                    Call Me.SetReturnKyoriPop(frm, eventshubetsu)

                Case .btnRowAdd.Name

                    Call Me.SetReturnCustPop(frm, eventshubetsu)

                    '(2012.08.17)支払サブ機能対応 --- START ---
                Case .txtShiharaitoCd.Name

                    Call Me.SetReturnShiharaitoPop(frm, eventshubetsu)
                    '(2012.08.17)支払サブ機能対応 ---  END  ---

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 郵便番号Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnZipPop(ByVal frm As LMM080F, ByVal objNm As String, ByVal eventshubetsu As LMM080C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowZipPopup(frm, ctl, eventshubetsu)

        If prm.ReturnFlg = True Then

            'PopUpから取得したデータをコントロールにセット
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ060C.TABLE_NM_OUT).Rows(0)

            '住所1(都道府県+市区町村名)
            Dim add1 As String = String.Concat(dr.Item("KEN_N").ToString(), dr.Item("CITY_N").ToString())

            ctl.TextValue = dr.Item("ZIP_NO").ToString()         '郵便番号

            If String.IsNullOrEmpty(frm.txtAd1.TextValue) _
            AndAlso String.IsNullOrEmpty(frm.txtAd2.TextValue) Then

                frm.txtAd1.TextValue = add1
                frm.txtAd2.TextValue = dr.Item("TOWN_N").ToString    '住所2(町域名)

            End If

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 郵便番号マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowZipPopup(ByVal frm As LMM080F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM080C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ060DS()
        Dim dt As DataTable = ds.Tables(LMZ060C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM080C.EventShubetsu.ENTER Then
                .Item("ZIP_NO") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ060", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 支払運賃タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnTariffPop(ByVal frm As LMM080F, ByVal eventshubetsu As LMM080C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowUnchinTariffPopup(frm, eventshubetsu)
        If prm.ReturnFlg = True Then

            '(2012.08.20)支払サブ機能対応 --- START ---
            'Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ230C.TABLE_NM_OUT).Rows(0)
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ290C.TABLE_NM_OUT).Rows(0)
            '(2012.08.20)支払サブ機能対応 ---  END  ---

            With frm
                '(2012.08.20)支払サブ機能対応 --- START ---
                '.txtUnchinTariffCd.TextValue = dr.Item("UNCHIN_TARIFF_CD").ToString()
                '.lblUnshinTariffRem.TextValue = dr.Item("UNCHIN_TARIFF_REM").ToString()
                .txtUnchinTariffCd.TextValue = dr.Item("SHIHARAI_TARIFF_CD").ToString()
                .lblUnshinTariffRem.TextValue = dr.Item("SHIHARAI_TARIFF_REM").ToString()
                '(2012.08.20)支払サブ機能対応 ---  END  ---
            End With


            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowUnchinTariffPopup(ByVal frm As LMM080F, ByVal eventshubetsu As LMM080C.EventShubetsu) As LMFormData

        '(2012.08.20)支払サブ機能対応 --- START ---
        'Dim ds As DataSet = New LMZ230DS()
        'Dim dt As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)
        Dim ds As DataSet = New LMZ290DS()
        Dim dt As DataTable = ds.Tables(LMZ290C.TABLE_NM_IN)
        '(2012.08.20)支払サブ機能対応 ---  END  ---
        Dim dr As DataRow = dt.NewRow()

        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM080C.EventShubetsu.ENTER Then
                '(2012.08.20)支払サブ機能対応 --- START ---
                '.Item("UNCHIN_TARIFF_CD") = frm.txtUnchinTariffCd.TextValue
                .Item("SHIHARAI_TARIFF_CD") = frm.txtUnchinTariffCd.TextValue
                '(2012.08.20)支払サブ機能対応 ---  END  ---
            End If
            'END SHINOHARA 要望番号513	
            .Item("STR_DATE") = Me._sysDate
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        dt.Rows.Add(dr)

        'Pop起動
        '(2012.08.20)支払サブ機能対応 --- START ---
        'Return Me._LMMConH.FormShow(ds, "LMZ230", "", Me._PopupSkipFlg)
        Return Me._LMMConH.FormShow(ds, "LMZ290", "", Me._PopupSkipFlg)
        '(2012.08.20)支払サブ機能対応 ---  END  ---

    End Function

    ''' <summary>
    ''' 割増タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnExtcPop(ByVal frm As LMM080F, ByVal eventshubetsu As LMM080C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowExtcPopup(frm, eventshubetsu)

        If prm.ReturnFlg = True Then
            '(2012.08.20)支払サブ機能対応 --- START ---
            'Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ240C.TABLE_NM_OUT).Rows(0)
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ300C.TABLE_NM_OUT).Rows(0)
            '(2012.08.20)支払サブ機能対応 ---  END  ---

            With frm
                .txtExtcTariffCd.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()
                .lblExtcTariffRem.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 支払割増タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowExtcPopup(ByVal frm As LMM080F, ByVal eventshubetsu As LMM080C.EventShubetsu) As LMFormData

        '(2012.08.20)支払サブ機能対応 --- START ---
        'Dim ds As DataSet = New LMZ240DS()
        'Dim dt As DataTable = ds.Tables(LMZ240C.TABLE_NM_IN)
        Dim ds As DataSet = New LMZ300DS()
        Dim dt As DataTable = ds.Tables(LMZ300C.TABLE_NM_IN)
        '(2012.08.20)支払サブ機能対応 ---  END  ---
        Dim dr As DataRow = dt.NewRow()

        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM080C.EventShubetsu.ENTER Then
                .Item("EXTC_TARIFF_CD") = frm.txtExtcTariffCd.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        dt.Rows.Add(dr)

        'Pop起動
        '(2012.08.20)支払サブ機能対応 --- START ---
        'Return Me._LMMConH.FormShow(ds, "LMZ240", "", Me._PopupSkipFlg)
        Return Me._LMMConH.FormShow(ds, "LMZ300", "", Me._PopupSkipFlg)
        '(2012.08.20)支払サブ機能対応 ---  END  ---

    End Function

    ''' <summary>
    ''' 距離Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnKyoriPop(ByVal frm As LMM080F, ByVal eventshubetsu As LMM080C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowKyoriPop(frm, eventshubetsu)

        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ080C.TABLE_NM_OUT).Rows(0)

            With frm
                .txtKyoriCd.TextValue = dr.Item("KYORI_CD").ToString()
                .lblKyoriRem.TextValue = dr.Item("KYORI_REM").ToString()
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 距離マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowKyoriPop(ByVal frm As LMM080F, ByVal eventshubetsu As LMM080C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ080DS()
        Dim dt As DataTable = ds.Tables(LMZ080C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM080C.EventShubetsu.ENTER Then
                .Item("KYORI_CD") = frm.txtKyoriCd.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ080", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 荷主Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal frm As LMM080F, ByVal eventshubetsu As LMM080C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, eventshubetsu)

        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            Dim custCdL As String = dr.Item("CUST_CD_L").ToString()
            Dim custCdM As String = dr.Item("CUST_CD_M").ToString()

            'JISスプレッド
            Return Me.RowAddJisDt(frm, custCdL, custCdM)

        End If

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMM080F, ByVal eventshubetsu As LMM080C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            Dim brCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()
            Dim defaFlgOn As String = LMConst.FLG.ON
            Dim csFlgOff As String = LMConst.FLG.OFF

            .Item("NRS_BR_CD") = brCd
            .Item("DEFAULT_SEARCH_FLG") = defaFlgOn
            .Item("SEARCH_CS_FLG") = csFlgOff
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)
        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

    '(2012.08.17)支払サブ機能対応 --- START ---
    ''' <summary>
    ''' 支払先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnShiharaitoPop(ByVal frm As LMM080F, ByVal eventshubetsu As LMM080C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowShiharaitoPopup(frm, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ310C.TABLE_NM_OUT).Rows(0)

            With frm
                .txtShiharaitoCd.TextValue = dr.Item("SHIHARAITO_CD").ToString()
                .lblShiharaitoNm.TextValue = dr.Item("SHIHARAITO_NM").ToString()
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 支払先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowShiharaitoPopup(ByVal frm As LMM080F, ByVal eventshubetsu As LMM080C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ310DS()
        Dim dt As DataTable = ds.Tables(LMZ310C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            If eventshubetsu = LMM080C.EventShubetsu.ENTER Then
                .Item("SHIHARAITO_CD") = frm.txtShiharaitoCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ310", "", Me._PopupSkipFlg)

    End Function

    '(2012.08.17)支払サブ機能対応 ---  END  ---

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMM080F)

        Dim dt As DataTable = Me._FindDs.Tables(LMM080C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With frm.sprDetail.ActiveSheet

            dr("SYS_DEL_FLG") = Me._LMMConV.GetCellValue(.Cells(0, LMM080G.sprDetailDef.SYS_DEL_NM.ColNo))
            dr("NRS_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM080G.sprDetailDef.NRS_BR_NM.ColNo))
            dr("UNSOCO_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM080G.sprDetailDef.UNSOCO_CD.ColNo))
            dr("UNSOCO_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM080G.sprDetailDef.UNSOCO_BR_CD.ColNo))
            dr("UNSOCO_NM") = Me._LMMConV.GetCellValue(.Cells(0, LMM080G.sprDetailDef.UNSOCO_NM.ColNo))
            dr("UNSOCO_BR_NM") = Me._LMMConV.GetCellValue(.Cells(0, LMM080G.sprDetailDef.UNSOCO_BR_NM.ColNo))
            dr("MOTOUKE_KB") = Me._LMMConV.GetCellValue(.Cells(0, LMM080G.sprDetailDef.MOTOUKE_KB_NM.ColNo))
#If False Then ' 名鉄対応(2499) 2016.1.29 changed inoue
            dr("PTN_ID") = LMM080C.INVOICE_PTNID
#End If
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            dr("USER_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM080G.sprDetailDef.NRS_BR_CD.ColNo))

            dt.Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>    
    ''' <remarks></remarks>
    Private Sub SetDatasetItemData(ByVal frm As LMM080F)

        Me._Ds = New LMM080DS()

        Dim nrsBrCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()
        Dim unsoCd As String = frm.txtUnsocoCd.TextValue.Trim()
        Dim unsoBrCd As String = frm.txtUnsocoBrCd.TextValue.Trim()

        With frm

            Dim dt As DataTable = Me._Ds.Tables(LMM080C.TABLE_NM_IN)
            Dim dr As DataRow = dt.NewRow()

            '運送会社マスタ
            dr("NRS_BR_CD") = nrsBrCd
            dr("UNSOCO_CD") = unsoCd
            dr("UNSOCO_BR_CD") = unsoBrCd
            dr("UNSOCO_NM") = .txtUnsocoNm.TextValue.Trim()
            dr("UNSOCO_BR_NM") = .txtUnsocoBrNm.TextValue.Trim()
            dr("MOTOUKE_KB") = .cmbMotoukeKb.SelectedValue
            dr("UNSOCO_KB") = .cmbUnsocoKb.SelectedValue
            dr("ZIP") = .txtZip.TextValue.Trim()
            dr("AD_1") = .txtAd1.TextValue.Trim()
            dr("AD_2") = .txtAd2.TextValue.Trim()
            dr("AD_3") = .txtAd3.TextValue.Trim()
            dr("TEL") = .txtTel.TextValue.Trim()
            dr("FAX") = .txtFax.TextValue.Trim()
            dr("URL") = .txtURL.TextValue.Trim()
            dr("PIC") = .txtPic.TextValue.Trim()
            dr("NRS_SBETU_CD") = .txtNrsSbetuCd.TextValue.Trim()
            dr("NIHUDA_YN") = .cmbNihudaYn.SelectedValue
            dr("TARE_YN") = .cmbTareYn.SelectedValue
            '(2012.08.17)支払サブ機能対応 --- START ---
            dr("SHIHARAITO_CD") = .txtShiharaitoCd.TextValue.Trim()
            '(2012.08.17)支払サブ機能対応 ---  END  ---
            dr("UNCHIN_TARIFF_CD") = .txtUnchinTariffCd.TextValue.Trim()
            dr("EXTC_TARIFF_CD") = .txtExtcTariffCd.TextValue.Trim()
            dr("BETU_KYORI_CD") = .txtKyoriCd.TextValue.Trim()
            dr("LAST_PU_TIME") = .dtpLastPuTime.TextValue.Trim()
            '要望番号:1275 yamanaka 2012.07.13 Start
            dr("CUST_UNSO_RYAKU_NM") = .txtRyakumei.TextValue.Trim()
            '要望番号:1275 yamanaka 2012.07.13 End
            '要望番号:2140 kobayashi 2013.12.24 Start
            dr("PICKLIST_GRP_KBN") = .cmbPickGrpKbn.SelectedValue
            '要望番号:2140 kobayashi 2013.12.24 End

            dr("EDI_USED_KBN") = .cmbEDIUseKbn.SelectedValue
            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim())
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim()
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            dr("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- 
            dr("NIFUDA_SCAN_YN") = .cmbNifudaScanYn.SelectedValue
            '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正終了 ---

            '要望番号:2408 2015.09.17 追加START
            If .chkAutoDenp.Checked = True Then
                dr("AUTO_DENP_NO_FLG") = LMConst.FLG.ON
            Else
                dr("AUTO_DENP_NO_FLG") = LMConst.FLG.OFF
            End If
            dr("AUTO_DENP_NO_KBN") = .cmbAutoDenpKbn.SelectedValue
            '要望番号:2408 2015.09.17 追加END

            dr("TAG_BARCD_KBN") = .cmbTagBcdKbn.SelectedValue  ' FFEM 荷札検品対応 20160610追加 

            dr("WH_NIFUDA_SCAN_YN") = .cmbNifudaScanTabYn.SelectedValue

            dt.Rows.Add(dr)

        End With

        With frm.sprDetail2.ActiveSheet

            '荷主別運送会社マスタ
            Dim max As Integer = .Rows.Count - 1
            Dim dt2 As DataTable = Me._Ds.Tables(LMM080C.TABLE_NM_CUSTRPT)
            dt2.Rows.Clear()
            Dim dr2 As DataRow = Nothing

            Dim moto As Integer = -1
            Dim chaku As Integer = -1

            For i As Integer = 0 To max

                dr2 = dt2.NewRow()

                '編集部の値をデータセットに設定
                dr2("NRS_BR_CD") = nrsBrCd
                dr2("UNSOCO_CD") = unsoCd
                dr2("UNSOCO_BR_CD") = unsoBrCd
#If False Then
                dr2("PTN_ID") = LMM080C.INVOICE_PTNID
#Else
                dr2("PTN_ID") = Me._LMMConV.GetCellValue(.Cells(i, LMM080G.sprDetailDef2.PTN_ID.ColNo))
#End If
                dr2("PTN_CD") = Me._LMMConV.GetCellValue(.Cells(i, LMM080G.sprDetailDef2.PTN_CD.ColNo))
                dr2("CUST_CD_L") = Me._LMMConV.GetCellValue(.Cells(i, LMM080G.sprDetailDef2.CUST_CD_L.ColNo))
                dr2("CUST_CD_M") = Me._LMMConV.GetCellValue(.Cells(i, LMM080G.sprDetailDef2.CUST_CD_M.ColNo))
                dr2("MOTO_TYAKU_KB") = Me._LMMConV.GetCellValue(.Cells(i, LMM080G.sprDetailDef2.MOTO_TYAKU_KB.ColNo))
                Select Case Me._LMMConV.GetCellValue(.Cells(i, LMM080G.sprDetailDef2.MOTO_TYAKU_KB.ColNo))
                    Case LMM080C.MOTO
                        moto = moto + 1
                        dr2("EDABAN") = Me._G.SetZeroData(moto.ToString(), LMM080C.MAEZERO)

                    Case Else
                        chaku = chaku + 1
                        dr2("EDABAN") = Me._G.SetZeroData(chaku.ToString(), LMM080C.MAEZERO)
                End Select

                dt2.Rows.Add(dr2)
            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(削除・復活用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal frm As LMM080F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM080C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("UNSOCO_CD") = .txtUnsocoCd.TextValue.Trim()
            dr("UNSOCO_BR_CD") = .txtUnsocoBrCd.TextValue.Trim()
            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim())
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim()

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            dr("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            Dim delflg As String = String.Empty
            If LMConst.FLG.OFF.Equals(.lblSituation.RecordStatus) = True Then
                delflg = LMConst.FLG.ON
            Else
                delflg = LMConst.FLG.OFF
            End If

            dr("SYS_DEL_FLG") = delflg

        End With

        dt.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(CUSTRPT情報格納)
    ''' </summary>    
    ''' <param name="dt">このハンドラクラスに紐づくデータテーブル</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetCustRptData(ByVal dt As DataTable)

        Me._CustRptDt = dt

    End Sub

    ''' <summary>
    ''' システム日付変数格納
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SysDateset(ByVal ds As DataSet)

        Me._sysDate = ds.Tables(LMM080C.TABLE_NM_DATE).Rows(0).Item("SYS_DATE").ToString()

    End Sub

    ''' <summary>
    ''' INテーブルに新しい行を追加
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub InDatatableNewRowCreate(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM080C.TABLE_NM_IN)
        dt.Rows.Add(dt.NewRow())

    End Sub

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "NewDataEvent")

        Call Me.NewDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "NewDataEvent")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMM080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditDataEvent")

        Call Me.EditDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditDataEvent")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMM080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditDataEvent")

        Call Me.CopyDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditDataEvent")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMM080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除処理
        Call Me.DeleteDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Call Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMM080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        Call Me.MasterShowEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")

    End Sub


    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMM080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveItemData")

        Call Me.SaveItemData(frm, LMM080C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM080F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMM080F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then                
                Exit Sub
            End If
        End If

        Call Me.RowSelection(frm, e.Row)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' 行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROWADD_Click(ByVal frm As LMM080F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_TCUST_Click")

        '「行追加」処理
        Call Me.RowAdd(frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_TCUST_Click")

    End Sub

    ''' <summary>
    ''' 行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROWDEL_Click(ByRef frm As LMM080F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_TCUST_Click")

        '「行削除」処理
        Call Me.RowDel(frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_TCUST_Click")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMM080F_KeyDown(ByVal frm As LMM080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM080F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM080F_KeyDown")

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMM080F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

End Class