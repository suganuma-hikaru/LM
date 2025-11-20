' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB090H : 画像照会
'  作  成  者       :  matsumoto
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMB090ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMB090H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMB090V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMB090G

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBConG As LMBControlG

    ''' <summary>
    ''' ハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBConH As LMBControlH

    ''' <summary>
    ''' Validateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBConV As LMBControlV

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    ''' <summary>
    ''' パラメータデータセット
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

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

        Me._Prm = prm

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet()

        'フォームの作成
        Dim frm As LMB090F = New LMB090F(Me)

        'Gamen共通クラスの設定
        Me._LMBConG = New LMBControlG(frm)

        'Validateクラスの設定
        Me._V = New LMB090V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMB090G(Me, frm)

        Me._LMBConV = New LMBControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMBConH = New LMBControlH(DirectCast(frm, Form), Me.GetPGID, Me)

        'Gamen共通クラスの設定
        Me._LMBConG = New LMBControlG(DirectCast(frm, Form))

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'ステータスバーの位置調整
        Me._G.SizesetStatusStrip(frm)

        '画面項目の設定
        Me._G.SetInitForm(frm, Me._PrmDs)

        '呼び出し元画面情報を設定
        frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

        'フォームの表示
        frm.ShowDialog()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub


#End Region '初期処理

#Region "イベント定義(一覧)"

#End Region 'イベント定義(一覧)

#Region "個別メソッド"

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMB090F) As Boolean

        'リターンコードの設定
        Me._Prm.ReturnFlg = True

    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMB090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMB090F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

#End Region 'イベント振分け

#End Region 'Method

End Class