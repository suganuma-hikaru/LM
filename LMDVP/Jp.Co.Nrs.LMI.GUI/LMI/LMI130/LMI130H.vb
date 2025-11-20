' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI130  : 日医工詰め合わせ画面
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMI130ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI130H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI130V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI130G

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConH As LMIControlH

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconV As LMIControlV

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
        Dim frm As LMI130F = New LMI130F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMIConG = New LMIControlG(DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._LMIconV = New LMIControlV(Me, sForm, Me._LMIConG)

        'Hnadler共通クラスの設定
        Me._LMIConH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI130G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMI130V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        '数値コントロールの書式設定
        Call Me._G.SetNumberControl()

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G006")

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMI130C.EventShubetsu, ByVal frm As LMI130F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMI130C.EventShubetsu.ADD    '追加

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '追加処理
                Call Me.AddData(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm, "G006")

            Case LMI130C.EventShubetsu.NIFUDAPRINT    '荷札印刷

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '印刷処理
                Call Me.PrintData(frm, eventShubetsu)

                '処理終了アクション
                Me._LMIConH.EndAction(frm, "G006")

            Case LMI130C.EventShubetsu.KONPOPRINT    '梱包明細荷札印刷

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '印刷処理
                Call Me.PrintData(frm, eventShubetsu)

                '処理終了アクション
                Me._LMIConH.EndAction(frm, "G006")

            Case LMI130C.EventShubetsu.PRINT    '印刷

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '印刷処理
                Call Me.PrintData(frm, eventShubetsu)

                '処理終了アクション
                Me._LMIConH.EndAction(frm, "G006")

            Case LMI130C.EventShubetsu.CLEAR    'クリア

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                'クリア処理
                Call Me.clearData(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm, "G006")

            Case LMI130C.EventShubetsu.ROWDEL    '行削除

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '行削除処理
                Call Me.RowDelData(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm, "G006")

        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI130F) As Boolean

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
    Friend Sub FunctionKey1Press(ByVal frm As LMI130F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey1Press")

        Me.ActionControl(LMI130C.EventShubetsu.ADD, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByVal frm As LMI130F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey5Press")

        Me.ActionControl(LMI130C.EventShubetsu.NIFUDAPRINT, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey5Press")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByVal frm As LMI130F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey6Press")

        Me.ActionControl(LMI130C.EventShubetsu.KONPOPRINT, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey6Press")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByVal frm As LMI130F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey7Press")

        Me.ActionControl(LMI130C.EventShubetsu.PRINT, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey7Press")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByVal frm As LMI130F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey8Press")

        Me.ActionControl(LMI130C.EventShubetsu.CLEAR, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey8Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI130F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMI130F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    '''印刷押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LMI130F)

        Logger.StartLog(Me.GetType.Name, "btnPrint_Click")

        '「印刷」処理
        Me.ActionControl(LMI130C.EventShubetsu.PRINT, frm)

        Logger.EndLog(Me.GetType.Name, "btnPrint_Click")

    End Sub

    ''' <summary>
    '''行削除押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnRowDel_Click(ByRef frm As LMI130F)

        Logger.StartLog(Me.GetType.Name, "btnRowDel_Click")

        '「行削除」処理
        Me.ActionControl(LMI130C.EventShubetsu.ROWDEL, frm)

        Logger.EndLog(Me.GetType.Name, "btnRowDel_Click")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' 追加処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub AddData(ByVal frm As LMI130F)

        'DataSet設定
        Dim rtDs As DataSet = New LMI130DS()

        'InDataSetの場合
        Call Me.SetInAddData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectAddData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI130BLF", "SelectAddData", rtDs)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        '取得した値を一覧に追加
        If 0 < rtnDs.Tables(LMI130C.TABLE_NM_INOUT).Rows.Count Then
            Call Me.SuccessSelect(frm, rtnDs)
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G002", New String() {"追加処理", ""})
        Else
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "E024")
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectAddData")

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理（画面別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMI130F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMI130C.TABLE_NM_INOUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        '追加条件の出荷管理番号クリア
        Call Me._G.ClearOutkaNo()

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub PrintData(ByVal frm As LMI130F, ByVal eventShubetsu As LMI130C.EventShubetsu)

        'DataSet設定
        Dim rtnDs As DataSet = New LMI130DS()

        'InDataSetの場合
        Call Me.SetInPrintData(frm, rtnDs, eventShubetsu)

        rtnDs.Merge(New RdPrevInfoDS)
        rtnDs.Tables(LMConst.RD).Clear()

        'サーバに渡すレコードが存在する場合
        If 0 < rtnDs.Tables(LMI130C.TABLE_NM_INOUT).Rows.Count Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

            '==== WSAクラス呼出（変更処理） ====
            rtnDs = MyBase.CallWSA("LMI130BLF", "PrintData", rtnDs)

        End If

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowNonPopUpMessage(frm)
            Exit Sub
        End If

        '詰め合わせ明細書印刷の際、作業レコードを作成する
        Dim printType As String = rtnDs.Tables(LMI130C.TABLE_NM_INOUT).Rows(0).Item("PRINT_KB").ToString()

        If (printType = "01") Then
            rtnDs = MyBase.CallWSA("LMI130BLF", "InsertSagyoRecord", rtnDs)

        End If

        'プレビュー判定 
        Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)
        If 0 < prevDt.Rows.Count Then

            'プレビューの生成
            Dim prevFrm As RDViewer = New RDViewer()

            'データ設定
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始
            prevFrm.Run()

            'プレビューフォームの表示
            prevFrm.Show()

            'フォーカス設定
            prevFrm.Focus()

        End If

    End Sub

    ''' <summary>
    ''' クリア処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ClearData(ByVal frm As LMI130F)

        'スプレッドのクリア処理
        Call Me._G.ClearSpread()

    End Sub

    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowDelData(ByVal frm As LMI130F)

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        arr = Me._V.GetCheckList(LMI130G.sprDetailsDef.DEF.ColNo, frm.sprDetails)
        Dim max As Integer = arr.Count - 1

        For i As Integer = max To 0 Step -1

            '選択された行を物理削除
            frm.sprDetails.ActiveSheet.Rows(Convert.ToInt32(arr(i))).Remove()

        Next

    End Sub

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(追加時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInAddData(ByVal frm As LMI130F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMI130C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("OUTKA_NO_L") = Mid(frm.txtOutkaNo.TextValue, 1, 9)
        dr("OUTKA_NO_M") = Mid(frm.txtOutkaNo.TextValue, 10, 3)
        dr("OUTKA_NO_S") = Mid(frm.txtOutkaNo.TextValue, 13, 3)

        '検索条件をデータセットに設定
        rtDs.Tables(LMI130C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(印刷時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInPrintData(ByVal frm As LMI130F, ByRef rtDs As DataSet, ByVal eventShubetsu As LMI130C.EventShubetsu)

        Dim dr As DataRow = rtDs.Tables(LMI130C.TABLE_NM_INOUT).NewRow()
        Dim outkaNo As String = String.Empty
        Dim custCd As String = String.Empty

        With frm.sprDetails.ActiveSheet

            Dim max As Integer = .Rows.Count - 1
            Dim intRow As Integer = 0
            Dim printKb As String = String.Empty

            Select Case eventShubetsu
                Case LMI130C.EventShubetsu.PRINT       '印刷
                    printKb = Convert.ToString(frm.cmbPrint.SelectedValue)
                Case LMI130C.EventShubetsu.NIFUDAPRINT '荷札印刷
                    printKb = "02"
                Case LMI130C.EventShubetsu.KONPOPRINT  '梱包明細印刷
                    printKb = "01"
            End Select

            For i As Integer = 0 To max
                dr = rtDs.Tables(LMI130C.TABLE_NM_INOUT).NewRow()

                intRow = i

                outkaNo = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.OUTKANO.ColNo)).Replace("-", String.Empty)
                custCd = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.CUSTCD.ColNo)).Replace("-", String.Empty)

                dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                dr("OUTKA_NO_L") = Mid(outkaNo, 1, 9)
                dr("OUTKA_NO_M") = Mid(outkaNo, 10, 3)
                dr("OUTKA_NO_S") = Mid(outkaNo, 13, 3)
                dr("DEST_CD") = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.DESTCD.ColNo))
                dr("DEST_NM") = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.DESTNM.ColNo))
                dr("GOODS_CD_NRS") = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.GOODSCDNRS.ColNo))
                dr("GOODS_CD_CUST") = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.GOODSCDCUST.ColNo))
                dr("GOODS_NM_1") = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.GOODSNM1.ColNo))
                dr("GOODS_NM_2") = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.GOODSNM2.ColNo))
                dr("LT_DATE") = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.LTDATE.ColNo))
                dr("LOT_NO") = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.LOTNO.ColNo))
                dr("GOODS_SYUBETU") = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.GOODSSYUBETU.ColNo))
                dr("PRINT_KB") = printKb
                dr("TSUME_NB") = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.TSUMENB.ColNo))
                dr("PRT_NB") = frm.numPrtCnt.Value
                dr("WH_CD") = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.WHCD.ColNo))
                dr("OUTKA_PLAN_DATE") = Me._LMIConG.GetCellValue(.Cells(intRow, LMI130G.sprDetailsDef.COMPDATE.ColNo))
                dr("CUST_CD_L") = Mid(custCd, 1, 5)
                dr("CUST_CD_M") = Mid(custCd, 6, 2)

                'データセットに設定
                rtDs.Tables(LMI130C.TABLE_NM_INOUT).Rows.Add(dr)
            Next

        End With

    End Sub

#End Region

#Region "その他処理"

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCheckList(ByVal defNo As Integer, ByVal sprDetail As Spread.LMSpread) As ArrayList

        Return Me._LMIconV.SprSelectList2(defNo, sprDetail)

    End Function

#End Region

#End Region 'Method

End Class
