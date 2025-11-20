' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ         : 共通
'  プログラムID     :  LMZControlH : LMZ共通ハンドラクラス
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports System
Imports System.Reflection
Imports FarPoint.Win.Spread

''' <summary>
''' LMBControlハンドラクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2010/03/01 金
''' </histry>
Public Class LMZControlH
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    
    ''' <summary>
    ''' 共通クラス(G)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMZControlG

    ''' <summary>
    ''' PGID
    ''' </summary>
    ''' <remarks></remarks>
    Private _Pgid As String

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New(ByRef frm As LMFormPopL, ByVal pgid As String, ByVal h As LM.Base.GUI.LMBaseGUIHandler)

        MyBase.SetPGID(pgid)

        Me._ControlG = New LMZControlG(frm)

        Me._Pgid = pgid

    End Sub

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New(ByRef frm As LMFormPopLL, ByVal pgid As String, ByVal h As LM.Base.GUI.LMBaseGUIHandler)

        MyBase.SetPGID(pgid)

        Me._ControlG = New LMZControlG(frm)

        Me._Pgid = pgid

    End Sub


    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New(ByRef frm As LMFormPopM, ByVal pgid As String, ByVal h As LM.Base.GUI.LMBaseGUIHandler)

        MyBase.SetPGID(pgid)

        Me._ControlG = New LMZControlG(frm)

        Me._Pgid = pgid

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub StartAction(ByVal frm As Form)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(DirectCast(frm, Win.Interface.ILMForm))

    End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub EndAction(ByVal frm As Form)

        '画面解除
        MyBase.UnLockedControls(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub FailureSelect(ByVal frm As Form)

        '画面解除
        MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">メッセージ置換文字列(処理名)</param>
    ''' <param name="cntSelect">検索結果件数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ConfirmMsg(ByVal frm As Form, ByVal msg As String, ByVal cntSelect As String) As Boolean

        If MyBase.ShowMessage(frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G016", New String() {cntSelect})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' サーバーアクセス処理呼出
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rtDs"></param>
    ''' <param name="rn"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Friend Function CallWSAAction(ByVal frm As Form _
                                  , ByVal spr As Spread.LMSpreadSearch _
                                  , ByVal rtDs As DataSet _
                                  , Optional ByVal rn As Integer = -1 _
                                  ) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        MyBase.SetLimitCount(Me.SetLimit(rn))

        Dim rtnDs As DataSet = Me.ServerAccess(rtDs)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            If MyBase.IsWarningMessageExist() = True Then         'Warningの場合

                If Me.ShowWarningMsg(frm, MyBase.ShowMessage(frm)) = True Then

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = Me.ServerAccess(rtDs)

                End If

                Return rtnDs

            Else

                'SPREAD(表示行)初期化
                spr.CrearSpread()

                'メッセージエリアの設定
                MyBase.ShowMessage(frm)

                Return rtnDs

            End If

        End If

        Return rtnDs

    End Function

    ''' <summary>
    ''' 初期処理時サーバーアクセス処理
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="setForceFlg"></param>
    ''' <param name="rn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function LoadCallWSAAction(ByVal s As LMZControlS _
                                       , Optional ByVal setForceFlg As Boolean = False _
                                       , Optional ByVal rn As Integer = -1 _
                                       ) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(setForceFlg)

        '閾値の設定
        MyBase.SetLimitCount(Me.SetLimit(rn))

        Dim rtnDs As DataSet = Me.ServerAccess(s.PrmDs)

        'カウント保持
        s.Cnt = MyBase.GetResultCount()

        Return rtnDs

    End Function

    '要望対応:1248 terakawa 2013.03.21 Start
    ''' <summary>
    ''' MyDataサーバーアクセス処理
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function LoadMyWSAAction(ByVal s As LMZControlS) As Boolean

        Dim rtnDs As DataSet = Me.MyServerAccess(s.PrmDs)

        'カウント数が1以上の場合、True
        If MyBase.GetResultCount() > 0 Then
            Return True
        End If

        Return False

    End Function
    '要望対応:1248 terakawa 2013.03.21 End

    ''' <summary>
    ''' 閾値設定
    ''' </summary>
    ''' <param name="rn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetLimit(Optional ByVal rn As Integer = -1) As Integer

        If rn <> -1 Then
            Return rn
        End If

        Return Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                             (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))

    End Function

    ''' <summary>
    ''' サーバーアクセス
    ''' </summary>
    ''' <param name="rtDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ServerAccess(ByVal rtDs As DataSet) As DataSet

        Return MyBase.CallWSA(String.Concat(Me._Pgid, "BLF"), "SelectListData", rtDs)

    End Function

    '要望対応:1248 terakawa 2013.03.21 Start
    ''' <summary>
    ''' MyDataサーバーアクセス
    ''' </summary>
    ''' <param name="rtDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function MyServerAccess(ByVal rtDs As DataSet) As DataSet

        Return MyBase.CallWSA(String.Concat(Me._Pgid, "BLF"), "SelectMyData", rtDs)

    End Function
    '要望対応:1248 terakawa 2013.03.21 End


    ''' <summary>
    ''' 確認メッセージの表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>OKの場合:True　Cancelの場合:False</returns>
    ''' <remarks></remarks>
    Friend Function SetMessageC001(ByVal frm As Form, ByVal msg As String) As Boolean

        '確認メッセージ表示
        If MyBase.ShowMessage(frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 各画面データセットのOUTテーブルへデータを格納する
    ''' </summary>
    ''' <param name="ds">各画面のDS</param>
    ''' <param name="tblNm">抽出データ配列</param>
    ''' <remarks></remarks>
    Friend Function SetDataSetOutListData(ByVal ds As DataSet, ByVal tblNm As String, ByVal drs As DataRow()) As DataSet

        Dim dt As DataTable = ds.Tables(tblNm)
        Dim max As Integer = drs.Length - 1
        For i As Integer = 0 To max
            dt.ImportRow(drs(i))
        Next

        Return ds

    End Function

    ''' <summary>
    ''' セッションクラスのOUTテーブルに格納
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="outTbl"></param>
    ''' <param name="count"></param>
    ''' <param name="tblNm"></param>
    ''' <remarks></remarks>
    Friend Sub SetOutds(ByVal s As LMZControlS, ByVal outTbl As DataTable _
                            , ByVal count As Integer, ByVal tblNm As String)

        Dim sOutDt As DataTable = s.OutDs.Tables(tblNm)
        sOutDt.Clear()
        Dim max As Integer = count - 1
        For i As Integer = 0 To max
            sOutDt.ImportRow(outTbl.Rows(i))
        Next

    End Sub



    ''' <summary>
    ''' 返却パラメータセット
    ''' </summary>
    ''' <param name="s">共通セッション</param>
    ''' <param name="rtnDs"></param>
    ''' <param name="tblNm"></param>
    ''' <param name="rowI"></param>
    ''' <remarks></remarks>
    Friend Sub SetRtnParam(ByVal s As LMZControlS, ByVal rtnDs As DataSet, ByVal tblNm As String, ByVal rowI As Integer)

        Dim dRow As DataRow = s.OutDs.Tables(tblNm).Rows(rowI)

        '返却パラメータへ選択行を格納
        Me.SetDataSetOutListData(rtnDs, tblNm, New DataRow() {dRow})

        '返却パラメータへDS設定
        s.FormPrm.ParamDataSet = rtnDs

        '返却フラグを立てる
        s.FormPrm.ReturnFlg = True

    End Sub
    ''' <summary>
    ''' キャッシュ抽出条件設定
    ''' </summary>
    ''' <param name="andstr"></param>
    ''' <param name="whereStr">画面で入力された値</param>
    ''' <param name="ptn">条件</param>
    ''' <param name="colNm">項目名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetWhereData(ByVal andstr As System.Text.StringBuilder, ByVal whereStr As String, ByVal ptn As LMZControlC.ConditionPattern _
                                 , ByVal colNm As String) As String

        SetWhereData = String.Empty

        If String.IsNullOrEmpty(whereStr) = False Then
            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If


            Select Case ptn
                '完全一致
                Case LMZControlC.ConditionPattern.equal

                    SetWhereData = String.Concat(" ", colNm, " = '", whereStr, "'", vbNewLine)
                    '前方一致
                Case LMZControlC.ConditionPattern.pre

                    SetWhereData = String.Concat(" ", colNm, " LIKE '", Me.EscForLike(whereStr), "%'", vbNewLine)
                    '部分一致
                Case LMZControlC.ConditionPattern.all

                    SetWhereData = String.Concat(" ", colNm, " LIKE '", "%", Me.EscForLike(whereStr), "%'", vbNewLine)

                Case LMZControlC.ConditionPattern.more

                    SetWhereData = String.Concat(" ", colNm, " >= '", whereStr, "'", vbNewLine)

                Case LMZControlC.ConditionPattern.less

                    SetWhereData = String.Concat(" ", colNm, " <= '", whereStr, "'", vbNewLine)


            End Select

        End If

        Return SetWhereData

    End Function

    ''' <summary>
    ''' 検索条件のエスケープ
    ''' </summary>
    ''' <param name="value">条件文字</param>
    ''' <returns>エスケープ処理後の文字</returns>
    ''' <remarks></remarks>
    Private Function EscForLike(ByVal value As String) As String

        Return System.Text.RegularExpressions.Regex.Replace(value, "([\[\]*%])", "[$1]")

    End Function

    ''' <summary>
    ''' キャッシュからデータ取得
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="cacheTbl">キャッシュ</param>
    ''' <param name="whereStr">キャッシュ条件</param>
    ''' <param name="sort">ソート</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SelectListData(ByVal ds As DataSet, ByVal tblNm As String, ByVal cacheTbl As String, ByVal whereStr As String, ByVal sort As String) As DataSet

        Dim locationRows As DataRow() = MyBase.GetLMCachedDataTable(cacheTbl).Select(whereStr, sort)

        'メッセージ設定
        Dim cnt As Integer = locationRows.Length
        Dim lmt As Integer = Me.SetLimit()
        If cnt = 0 Then
            '0件
            MyBase.SetMessage("G001")
            '要望番号1552:(【SBS】マスタ参照画面の件数ワーニングの条件誤り) 2012/10/31 本明 Start
            'ElseIf 500 < cnt Then
        ElseIf lmt < cnt Then
            '要望番号1552:(【SBS】マスタ参照画面の件数ワーニングの条件誤り) 2012/10/31 本明 End
            'lmt件以上
            MyBase.SetMessage("W001", New String() {Convert.ToString(lmt)})
        End If

        If 0 < cnt Then
            '正常時 OUTテーブルへデータ格納
            Me.SetDataSetOutListData(ds, tblNm, locationRows)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 画面ヘッダー項目のセット
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="tblNm">データテーブル名</param>
    ''' <param name="cacheTbl">キャッシュテーブル名</param>
    ''' <param name="whereStr">where条件</param>
    ''' <param name="mstNm">置換文字：マスタ名</param>
    ''' <param name="mstCd">置換文字：各マスタのキー項目</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SelectHeaderData(ByVal ds As DataSet, ByVal tblNm As String _
                                         , ByVal cacheTbl As String, ByVal whereStr As String _
                                         , ByVal mstNm As String, ByVal mstCd As String) As DataSet

        Dim locationRows As DataRow() = MyBase.GetLMCachedDataTable(cacheTbl).Select(whereStr)

        'メッセージ設定
        Dim cnt As Integer = locationRows.Length
        Dim lmt As Integer = Me.SetLimit()

        If cnt = 0 Then
            '0件
            MyBase.SetMessage("E079", New String() {mstNm})

        End If

        If 0 < cnt Then
            '正常時 OUTテーブルへデータ格納
            Me.SetDataSetOutListData(ds, tblNm, locationRows)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 初期処理ロードメッセージチェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="prmdrow"></param>
    ''' <param name="count"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function LoadMsgChk(ByVal frm As Form, ByVal prmdrow As DataRow, ByVal count As Integer) As Boolean

        '初期検索フラグ = 1の場合
        ' 且つ 0件以外 且つ (ワーニングがない または OKを選択)した場合は検索を行う
        If prmdrow("DEFAULT_SEARCH_FLG").Equals(LMConst.FLG.ON) = True _
            AndAlso 0 < count _
            AndAlso (MyBase.IsWarningMessageExist() = False _
                     OrElse Me.ShowWarningMsg(frm, MyBase.ShowMessage(frm)) = True) _
                     Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Convert.ToString(count)})
            Return True

        End If

        MyBase.ShowMessage(frm, "G007")
        Return False

    End Function


    ''' <summary>
    ''' 取得件数によるメッセージ表示処理
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function CountRows(ByVal frm As Form, ByVal spr As Spread.LMSpreadSearch, ByVal outTbl As DataTable) As Boolean

        If MyBase.IsMessageExist() = True Then

            Dim warningChk As Boolean = MyBase.IsWarningMessageExist()
            Dim msg As Integer = MyBase.ShowMessage(frm)

            If warningChk = True Then         'Warningの場合(500件以上)

                Return Me.ShowWarningMsg(frm, msg)

            Else 'Errorの場合(0件)

                'SPREAD(表示行)初期化
                spr.CrearSpread()

                Return False

            End If
        Else     '上記以外

            Return True

        End If

    End Function

    ''' <summary>
    ''' 初期処理検索
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetLoadData(ByVal s As LMZControlS, ByVal frm As Form, ByVal dt As DataTable, ByVal tblNm As String) As DataTable

        If 0 < dt.Rows.Count Then
            Return dt
        End If

        s.OutDs = Me.LoadCallWSAAction(s, True)
        Return s.OutDs.Tables(tblNm)

    End Function

    ''' <summary>
    ''' 画面終了処理
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="tblNm"></param>
    ''' <remarks></remarks>
    Friend Sub CloseForm(ByVal s As LMZControlS, ByVal tblNm As String)

        If s.FormPrm.ParamDataSet.Tables(tblNm) Is Nothing = True _
            OrElse s.FormPrm.ParamDataSet.Tables(tblNm).Rows.Count = 0 Then

            'リターンコードの設定
            s.FormPrm.ReturnFlg = False
        Else

            'リターンコードの設定
            s.FormPrm.ReturnFlg = True

        End If

    End Sub


    ''' <summary>
    ''' ワーニングメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">メッセージの戻り値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowWarningMsg(ByVal frm As Form, ByVal msg As Integer) As Boolean

        'メッセージを表示し、戻り値により処理を分ける
        If MsgBoxResult.Ok <> msg Then '「いいえ」を選択
            MyBase.ShowMessage(frm, "G007")
            Return False
        End If

        Return True

    End Function

#Region "ENTERイベント"

    ''' <summary>
    ''' 次コントロールにフォーカス移動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub NextFocusedControl(ByVal frm As Form)

        'Spread上にコントロールがある場合、処理終了
        Dim sprFocusFlg As Boolean = False
        'フォーム内のSpreadを取得
        Dim arr As ArrayList = New ArrayList()
        arr = New ArrayList()
        Me._ControlG.GetTarget(Of Win.Spread.LMSpread)(arr, frm)
        For Each spr As Win.Spread.LMSpread In arr
            If frm.ActiveControl.Equals(spr) Then
                sprFocusFlg = True
            End If
        Next

        '次コントロールにフォーカス移動
        If sprFocusFlg = False Then
            frm.SelectNextControl(frm.ActiveControl, True, True, True, True)
        End If

    End Sub

    ''' <summary>
    ''' Enter押下イベントの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetEnterEvent(ByVal frm As Form)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '2011/08/23  福田　共通動作(右セル移動不可) スタート

        'ENTER時にセルを右移動させる
        'Call Me.SetSpreadEnterEvent(frm)

        '2011/08/23  福田　共通動作(右セル移動不可) エンド

    End Sub

    ''' <summary>
    ''' Spread上でのEnter押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSpreadEnterEvent(ByVal frm As Form)

        'フォーム内のSpreadを取得
        Dim arr As ArrayList = New ArrayList()
        arr = New ArrayList()
        Me._ControlG.GetTarget(Of Win.Spread.LMSpread)(arr, frm)
        Dim im As New FarPoint.Win.Spread.InputMap

        For Each spr As Win.Spread.LMSpread In arr

            ' 非編集セルでの[Enter]キーを「次列へ移動」とします
            im = spr.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)

            '編集中セルでの[Enter]キーを「次列へ移動」とします
            im = spr.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)

        Next

    End Sub

#End Region

#End Region 'Method

End Class
