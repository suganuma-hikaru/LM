' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI320H : 請求明細・鑑作成
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI320ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI320H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI320F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI320V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI320G

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

    ''' <summary>
    ''' 共通Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMIControlH

    ''' <summary>
    '''検索結果格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _OutDs As DataSet

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

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
        Me._Frm = New LMI320F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMIControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMIControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMIControlH(MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMI320V(Me, Me._Frm, Me._ControlV, Me._ControlG)

        'Gamenクラスの設定
        Me._G = New LMI320G(Me, Me._Frm, Me._ControlG, Me._V)

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitDetailSpread()
        Call Me._G.SetInitValue()

        'コントロール個別設定
        Call Me._G.SetDateControl(MyBase.GetSystemDateTime(0))

        '検索処理
        Call Me.SelectListEvent()

        'メッセージの表示
        Call MyBase.ShowMessage(Me._Frm, "G007")

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        Me._Frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectListEvent()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI320C.EventShubetsu.KENSAKU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'DataSet設定
        Dim rtnDs As DataSet = Me.SetSerchData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)

        rtnDs = MyBase.CallWSA(blf, "SelectListData", rtnDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables(LMI320C.TABLE_NM_OUT).Rows.Count > 0 Then
            '検索成功時共通処理を行う
            Call Me.SuccessSelect(rtnDs)

        ElseIf rtnDs IsNot Nothing Then
            '取得件数が0件の場合
            Call Me.FailureSelect(rtnDs)

        End If

    End Sub

    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnMakeClick()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI320C.EventShubetsu.SAKUSEI) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI320C.EventShubetsu.SAKUSEI) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'データセット
        Dim rtnDs As DataSet = Me.SetInData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DoMake")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)

        rtnDs = MyBase.CallWSA(blf, "DoMake", rtnDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DoMake")

        'メッセージコードの判定
        If Me.ShowStorePrintData(Me._Frm) = True Then
            '終了メッセージ表示
            MyBase.ShowMessage(Me._Frm, "G002", New String() {"作成", ""})
        End If

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)


    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnPrintClick()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI320C.EventShubetsu.PRINT) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI320C.EventShubetsu.PRINT) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'データセット
        Dim rtnDs As DataSet = Me.SetInData()
        rtnDs.Merge(New RdPrevInfoDS)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DoPrint")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)

        rtnDs = MyBase.CallWSA(blf, "DoPrint", rtnDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DoPrint")

        'メッセージコードの判定
        If Me.ShowStorePrintData(Me._Frm) = True Then
            '終了メッセージ表示
            MyBase.ShowMessage(Me._Frm, "G002", New String() {"印刷", ""})
        End If

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

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprDetail.CrearSpread()

        '取得データをSPREADに表示
        Dim dt As DataTable = Me._OutDs.Tables(LMI320C.TABLE_NM_OUT)
        Call Me._G.SetSpread(dt)

        '取得件数設定
        Me._CntSelect = dt.Rows.Count.ToString()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G008", New String() {Me._CntSelect})


    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理(検索結果が0件の場合))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprDetail.CrearSpread()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G001")

    End Sub

    ''' <summary>
    ''' エラー出力処理
    ''' </summary>
    ''' <returns>出力する場合:False　出力しない場合:True</returns>
    ''' <remarks></remarks>
    Private Function ShowStorePrintData(ByVal frm As LMI320F) As Boolean

        If MyBase.IsMessageStoreExist() = True Then

            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")
            Return False

        End If

        Return True

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSerchData() As DataSet

        Dim ds As DataSet = New LMI320DS
        Dim dr As DataRow = ds.Tables(LMI320C.TABLE_NM_IN).NewRow()

        With Me._Frm

            dr.Item("NRS_BR_CD") = .cmbNrsBr.SelectedValue
            dr.Item("SEIQTO_CD") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMI320G.sprDetailDef.SEIQTO_CD.ColNo))
            dr.Item("SEIQTO_NM") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMI320G.sprDetailDef.SEIQTO_NM.ColNo))
            dr.Item("KAGAMI_KB") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMI320G.sprDetailDef.KAGAMI_SHUBETU.ColNo))
            dr.Item("TOKU_CD") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMI320G.sprDetailDef.TOKUISAKI_CD.ColNo))
            dr.Item("HUTANKA") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMI320G.sprDetailDef.HUTANKA.ColNo))

            ds.Tables(LMI320C.TABLE_NM_IN).Rows.Add(dr)

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(作成・印刷処理)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Function SetInData() As DataSet

        With Me._Frm

            Dim ds As DataSet = New LMI320DS
            Dim dt As DataTable = ds.Tables(LMI320C.TABLE_NM_IN)
            Dim dr As DataRow = Nothing
            Dim arr As ArrayList = Me._V.GetCheckList

            For i As Integer = 0 To arr.Count - 1

                dr = dt.NewRow
                dr.Item("NRS_BR_CD") = .cmbNrsBr.SelectedValue
                dr.Item("SEIQTO_CD") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI320G.sprDetailDef.SEIQTO_CD.ColNo))
                dr.Item("SEIQ_DATE") = .imdSeiqDate.TextValue
                dr.Item("PRINT_SHUBETU") = .cmbPrintShubetu.SelectedValue
                dr.Item("ROW_NO") = arr(i)
                dt.Rows.Add(dr)

            Next

            Return ds

        End With

    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMI320F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI320F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓　========================

    ''' <summary>
    ''' 印刷ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub PrintClick(ByVal frm As LMI320F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintClick")

        '印刷処理
        Call Me.BtnPrintClick()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "PrintClick")

    End Sub

    ''' <summary>
    ''' 作成ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub MakeClick(ByVal frm As LMI320F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MakeClick")

        '作成処理
        Call Me.BtnMakeClick()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MakeClick")

    End Sub

    ''========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

End Class