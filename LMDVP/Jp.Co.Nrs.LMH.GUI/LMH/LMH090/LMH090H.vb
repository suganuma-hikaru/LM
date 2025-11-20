' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDIサブシステム
'  プログラムID     :  LMH090H : 現品票印刷
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports System.Text
Imports System.IO
Imports Microsoft.Office.Interop
Imports Jp.Co.Nrs.LM.Base.GUI

''' <summary>
''' LMH090ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMH090H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMH090V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMH090G

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconH As LMHControlH

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconV As LMHControlV

    ''' <summary>
    ''' Gamen共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconG As LMHControlG

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 画面間データ
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

        '画面間データを取得する
        _PrmDs = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMH090F = New LMH090F(Me)

        'Gamenクラスの設定
        Me._G = New LMH090G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMH090V(Me, frm, Me._G)

        Me._LMHconH = New LMHControlH(DirectCast(frm, Form), MyBase.GetPGID)

        Me._LMHconG = New LMHControlG(frm)

        Me._LMHconV = New LMHControlV(Me, DirectCast(frm, Form), Me._LMHconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(Me.GetPGID())

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        '初期値を設定
        Me._G.SetInitValue(frm, _PrmDs)

        If _PrmDs.Tables("LMH090_IN").Rows(0).Item("OUTKA_FROM_ORD_NO").ToString <> "" Then
            'データの取得と表示
            SelectListEvent(frm, _PrmDs)
        End If

        '画面の入力項目の制御
        Call _G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "外部Method"

#End Region '外部Method

#Region "内部Method"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMH090F, prmDs As DataSet)

        '検索条件の設定
        Dim ds As DataSet = New LMH090DS()
        Dim dt As DataTable = ds.Tables("SELECT_LIST_DATA_IN")
        Dim dr As DataRow = dt.NewRow()

        With frm.sprEdiList.ActiveSheet

            dr.Item("NRS_BR_CD") = prmDs.Tables("LMH090_IN").Rows(0).Item("NRS_BR_CD").ToString
            dr.Item("CUST_CD_L") = prmDs.Tables("LMH090_IN").Rows(0).Item("CUST_CD_L").ToString
            dr.Item("CUST_CD_M") = prmDs.Tables("LMH090_IN").Rows(0).Item("CUST_CD_M").ToString
            dr.Item("OUTKA_FROM_ORD_NO") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.OUTKA_FROM_ORD_NO.ColNo))
            dr.Item("INKA_DATE") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.INKA_DATE.ColNo))
            dr.Item("CUST_GOODS_CD") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.CUST_GOODS_CD.ColNo))
            dr.Item("LOT_NO") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.LOT_NO.ColNo))
            If .Cells(0, _G.sprEdiListDef.STD_IRIME.ColNo).Text <> "" Then
                dr.Item("STD_IRIME") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.STD_IRIME.ColNo))
            End If

        End With

        dt.Rows.Add(dr)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMHconH.CallWSAAction(DirectCast(frm, Form), _
                                                        "LMH090BLF", _
                                                        "SelectListData", _
                                                        ds, _
                                                        Me._LMHconG.GetLimitData(), _
                                                        Me._LMHconG.GetLimitData() _
                                                        )

        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then

            Call Me.SuccessSelect(frm, rtnDs)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 検索成功時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMH090F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables("SELECT_LIST_DATA_OUT")

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行)初期化
        frm.sprEdiList.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        Me._CntSelect = dt.Rows.Count.ToString()

        'データテーブルのカウントを設定
        Dim cnt As Integer = dt.Rows.Count()

        'カウントが0件以上の時メッセージの上書き
        If cnt > 0 Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})

        End If

    End Sub

#End Region '内部Method

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Print(ByVal frm As LMH090F)

        '処理開始アクション
        Call Me._LMHconH.StartAction(frm)

        Dim rtnResult As Boolean = True

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk()

        'チェックボックスの確認
        rtnResult = rtnResult AndAlso Me._V.IsSelectDataChk()

        'チェック
#If True Then   'ADD 2019/12/17
        If Me._V.GenpinPrintInDataCHK(Me._G) = False Then
            Call Me._LMHconH.EndAction(frm) '終了処理
            Exit Sub

        End If
#End If

        '確認メッセージ表示
        rtnResult = rtnResult AndAlso Me._LMHconH.SetMessageC001(frm, "印刷")

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me._LMHconH.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As DataSet = New LMH503DS()
        Dim dt As DataTable = ds.Tables("PRINT_DATA_IN")

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 1 To .Rows.Count - 1

                'チェックなしの行は対象外
                If "0".Equals(Me._LMHconV.GetCellValue(.Cells(i, _G.sprEdiListDef.DEF.ColNo))) Then
                    Continue For
                End If

                Dim dr As DataRow = dt.NewRow()

                dr.Item("SHORI_KB") = "再発行"
                dr.Item("NRS_BR_CD") = _PrmDs.Tables("LMH090_IN").Rows(0).Item("NRS_BR_CD").ToString
                dr.Item("WH_CD") = _PrmDs.Tables("LMH090_IN").Rows(0).Item("WH_CD").ToString
                dr.Item("CUST_CD_L") = _PrmDs.Tables("LMH090_IN").Rows(0).Item("CUST_CD_L").ToString
                dr.Item("CUST_CD_M") = _PrmDs.Tables("LMH090_IN").Rows(0).Item("CUST_CD_M").ToString
                dr.Item("CRT_DATE") = Me._LMHconV.GetCellValue(.Cells(i, _G.sprEdiListDef.CRT_DATE.ColNo))
                dr.Item("FILE_NAME") = Me._LMHconV.GetCellValue(.Cells(i, _G.sprEdiListDef.FILE_NAME.ColNo))
                dr.Item("REC_NO") = Me._LMHconV.GetCellValue(.Cells(i, _G.sprEdiListDef.REC_NO.ColNo))
                dr.Item("HED_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(i, _G.sprEdiListDef.HED_UPD_DATE.ColNo))
                dr.Item("HED_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(i, _G.sprEdiListDef.HED_UPD_TIME.ColNo))
                dr.Item("GYO") = Me._LMHconV.GetCellValue(.Cells(i, _G.sprEdiListDef.GYO.ColNo))
                dr.Item("DTL_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(i, _G.sprEdiListDef.DTL_UPD_DATE.ColNo))
                dr.Item("DTL_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(i, _G.sprEdiListDef.DTL_UPD_TIME.ColNo))

                '行追加
                dt.Rows.Add(dr)

            Next

        End With

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

        'サーバサイド処理
        Dim rtnDs As DataSet = Nothing
        rtnResult = rtnResult AndAlso Me.ActionData(frm, ds, "LMH090BLF", "PrintData", rtnDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "PrintData")

        '正常終了の場合
        If rtnResult = True Then

            '終了メッセージ表示
            MyBase.ShowMessage(frm, "G002", New String() {"印刷", ""})

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

        End If

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMH090F) As Boolean

        Return True

    End Function

    ''' <summary>
    ''' サーバアクセス(チェック有)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="blfName">BLF名</param>
    ''' <param name="actionId">アクションID</param>
    ''' <param name="rtnDs">戻りDataSet 初期値 = Nothing</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ActionData(ByVal frm As LMH090F _
                                , ByVal ds As DataSet _
                                , ByVal blfName As String _
                                , ByVal actionId As String _
                                , Optional ByRef rtnDs As DataSet = Nothing _
                                ) As Boolean

        'サーバアクセス
        rtnDs = MyBase.CallWSA(blfName, actionId, ds)

        'エラーがある場合、メッセージ設定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return False
        End If

        'エラーが保持されている場合、False
        If MyBase.IsMessageStoreExist = True Then
            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")
            Return False
        End If

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMH090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMH090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMH090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMH090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMH090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMH090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し(印刷処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMH090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Print")

        '印刷処理
        Me.Print(frm)

        Logger.EndLog(Me.GetType.Name, "Print")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMH090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMH090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "SelectListEvent")

        Me.SelectListEvent(frm, _PrmDs)

        Logger.EndLog(Me.GetType.Name, "SelectListEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMH090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMH090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMH090F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMH090F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class