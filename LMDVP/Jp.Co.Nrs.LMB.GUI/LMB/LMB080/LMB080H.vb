' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB080H : 登録済み画像照会
'  作  成  者       :  matsumoto
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMB080ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMB080H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMB080V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMB080G

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
        Dim frm As LMB080F = New LMB080F(Me)

        Dim popLL As LMFormPopLL = DirectCast(frm, LMFormPopLL)

        'Gamen共通クラスの設定
        Me._LMBConG = New LMBControlG(frm)

        'Validateクラスの設定
        Me._V = New LMB080V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMB080G(Me, frm)

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

        '↓ データ取得の必要があればここにコーディングする。

        '↑ データ取得の必要があればここにコーディングする。

        '明細クリア
        Me._G.RemoveDetailInCtl()

        '取得データを表示
        Me._G.AddDetailInCtl(Me._PrmDs)

        '呼び出し元画面情報を設定
        frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

        'フォームの表示
        frm.ShowDialog()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub


#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 画像ダブルクリック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="sender"></param>
    Private Sub PictureDoubleClick(ByVal frm As LMB080F, ByVal sender As Object)

        '画像照会(LMB090)を表示
        Dim objPic As PictureBox = CType(sender, PictureBox)
        Dim prm As LMFormData = New LMFormData()
        Dim prmDs As DataSet = New LMB090DS
        Dim row As DataRow = prmDs.Tables("LMB090IN").NewRow
        'UPD START 2023/08/18 037916
        'row("FILE_PATH") = objPic.ImageLocation
        row("IMAGE") = objPic.Image
        'UPD END 2023/08/18 037916
        prmDs.Tables("LMB090IN").Rows.Add(row)
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMB090", prm)

    End Sub

#End Region 'イベント定義(一覧)

#Region "個別メソッド"

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMB080F) As Boolean

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
    Friend Sub FunctionKey12Press(ByRef frm As LMB080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMB080F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' 画像ダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub picThumbnailDoubleClick(ByRef frm As LMB080F, ByVal sender As Object, ByVal e As EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "picThumbnailDoubleClick")

        'ダブルクリックアクション処理
        Call Me.PictureDoubleClick(frm, sender)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "picThumbnailDoubleClick")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class