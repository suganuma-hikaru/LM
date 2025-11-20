' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI060H : 三井化学ポリウレタン運賃計算「危険品一割増」処理
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
'Imports Microsoft.Office.Interop
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI060ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI060H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI060V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI060G

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
        Dim frm As LMI060F = New LMI060F(Me)

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
        Me._G = New LMI060G(Me, frm, Me._LMIConG)

        'Validateクラスの設定
        Me._V = New LMI060V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール初期設定
        Call Me._G.ClearControl(MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        '運賃再計算方法設定マスタの取得した値をスプレッドに設定
        Dim rtnDs As DataSet = Me.UnchinRecastData(frm)

        '運賃再計算方法設定マスタの取得した値をスプレッドに設定
        Call Me._G.SetSpread(rtnDs)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

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
    Friend Sub ActionControl(ByVal eventShubetsu As LMI060C.EventShubetsu, ByVal frm As LMI060F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        '入力チェック
        If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
           Me._V.IsKanrenCheck(eventShubetsu) = False Then
            '処理終了アクション
            Me._LMIConH.EndAction(frm)
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMI060C.EventShubetsu.MAKE    '作成

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '作成処理時、既存データ検索処理
                Call Me.MakeDataCHK(frm)

                'エラー時はメッセージを表示して終了
                If MyBase.IsMessageExist() = True Then
                    If MyBase.ShowMessage() = MsgBoxResult.Ok Then
                        'OKが押された場合は削除処理を行う

                        '削除処理
                        Call Me.DeleteData(frm)
                    Else
                        'キャンセルが押された場合は処理終了
                        '処理終了アクション
                        Me._LMIConH.EndAction(frm)
                        Exit Sub
                    End If
                End If

                '作成処理
                Call Me.MakeData(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI060C.EventShubetsu.PRINT    '印刷

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
                Call Me.PrintData(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI060F) As Boolean

        Return True

    End Function

    ''' <summary>
    ''' SPREADのクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMI060F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        '要望番号:1482 KIM 2012.10.10 START
        '荷主の設定処理
        'Call Me._G.SpreadCellClick(frm, e.NewRow)
        '要望番号:1482 KIM 2012.10.10 END

    End Sub

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI060F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMI060F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    '''作成押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnMake_Click(ByRef frm As LMI060F)

        Logger.StartLog(Me.GetType.Name, "btnMake_Click")

        '「作成」処理
        Me.ActionControl(LMI060C.EventShubetsu.MAKE, frm)

        Logger.EndLog(Me.GetType.Name, "btnMake_Click")

    End Sub

    ''' <summary>
    '''印刷押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LMI060F)

        Logger.StartLog(Me.GetType.Name, "btnPrint_Click")

        '「印刷」処理
        Me.ActionControl(LMI060C.EventShubetsu.PRINT, frm)

        Logger.EndLog(Me.GetType.Name, "btnPrint_Click")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function UnchinRecastData(ByVal frm As LMI060F) As DataSet

        ''SPREAD初期化
        frm.sprDetail.CrearSpread()

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim rtDs As DataSet = New LMI060DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UnchinRecastData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI060BLF", "SelectUnchinRecastData", rtDs)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return rtnDs
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UnchinRecastData")

        Return rtnDs

    End Function

    ''' <summary>
    ''' 作成処理時、既存データ検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function MakeDataCHK(ByVal frm As LMI060F) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim rtDs As DataSet = New LMI060DS()

        'InDataSetの場合
        '要望番号:1482 KIM 2012.10.10 START
        'Call Me.SetInData(frm, rtDs)
        '要望番号:1916 SHINOHARA 2013.03.22 START printFlg追加
        Dim printFlg As String = String.Empty
        printFlg = "0" '0:印刷ボタン以外　1:印刷ボタン
        Call Me.SetInSpreadData(frm, rtDs, printFlg)
        '要望番号:1916 SHINOHARA 2013.03.22 END
        '要望番号:1482 KIM 2012.10.10 END

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "MakeDataCHK")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI060BLF", "MakeDataCHK", rtDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "MakeDataCHK")

        Return rtnDs

    End Function

    ''' <summary>
    ''' 作成処理時、既存データ削除処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal frm As LMI060F) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim rtDs As DataSet = New LMI060DS()

        'InDataSetの場合
        '要望番号:1482 KIM 2012.10.10 END
        'Call Me.SetInData(frm, rtDs)
        '要望番号:1916 SHINOHARA 2013.03.22 START printFlg追加
        Dim printFlg As String = String.Empty
        printFlg = "0" '0:印刷ボタン以外　1:印刷ボタン
        Call Me.SetInSpreadData(frm, rtDs, printFlg)
        '要望番号:1916 SHINOHARA 2013.03.22 END
        '要望番号:1482 KIM 2012.10.10 END

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '==== WSAクラス呼出（削除処理） ====
        rtDs = MyBase.CallWSA("LMI060BLF", "DeleteSaveAction", rtDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        Return rtDs

    End Function

    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub MakeData(ByVal frm As LMI060F)

        'DataSet設定
        Dim rtDs As DataSet = New LMI060DS()

        'InDataSetの場合
        '要望番号:1916 SHINOHARA 2013.03.22 START printFlg追加
        Dim printFlg As String = String.Empty
        printFlg = "0" '0:印刷ボタン以外　1:印刷ボタン
        Call Me.SetInSpreadData(frm, rtDs, printFlg)
        '要望番号:1916 SHINOHARA 2013.03.22 END

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "MakeData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI060BLF", "MakeData", rtDs)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"作成処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "MakeData")

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub PrintData(ByVal frm As LMI060F)

        'DataSet設定
        Dim rtDs As DataSet = New LMI060DS()

        'InDataSetの場合
        '要望番号:1482 KIM 2012.10.10 START
        'Call Me.SetInData(frm, rtDs)
        '要望番号:1916 SHINOHARA 2013.03.22 START printFlg追加
        Dim printFlg As String = String.Empty
        printFlg = "1" '0:印刷ボタン以外　1:印刷ボタン
        Call Me.SetInSpreadData(frm, rtDs, printFlg)
        '要望番号:1916 SHINOHARA 2013.03.22 END
        '要望番号:1482 KIM 2012.10.10 END

        rtDs.Merge(New RdPrevInfoDS)
        rtDs.Tables(LMConst.RD).Clear()

        'サーバに渡すレコードが存在する場合、更新処理
        If 0 < rtDs.Tables(LMI060C.TABLE_NM_IN).Rows.Count Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

            '==== WSAクラス呼出（変更処理） ====
            rtDs = MyBase.CallWSA("LMI060BLF", "PrintData", rtDs)

        End If

        'プレビュー判定 
        Dim prevDt As DataTable = rtDs.Tables(LMConst.RD)
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

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInData(ByVal frm As LMI060F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMI060C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue

        '要望番号:1482 KIM 2012.10.10 START
        'dr("CUST_CD_L") = frm.txtCustCdL.TextValue
        'dr("CUST_CD_M") = frm.txtCustCdM.TextValue
        'dr("CUST_CD_S") = frm.txtCustCdS.TextValue
        'dr("CUST_CD_SS") = frm.txtCustCdSS.TextValue
        dr("CUST_CD_L") = String.Empty
        dr("CUST_CD_M") = String.Empty
        dr("CUST_CD_S") = String.Empty
        dr("CUST_CD_SS") = String.Empty
        '要望番号:1482 KIM 2012.10.10 END

        dr("DATE_FROM") = frm.imdDateFrom.TextValue
        dr("DATE_TO") = frm.imdDateTo.TextValue

        '要望番号:1482 KIM 2012.10.10 START
        'dr("TARIFF_CD") = frm.lblTariffCd.TextValue
        'dr("WARIMASHI_NR") = frm.lblWarimashi.TextValue
        'dr("ROUND_KB") = frm.lblRoundKb.TextValue
        'dr("ROUND_UT") = frm.lblRoundUt.TextValue
        'dr("ROUND_UT_LEN") = frm.lblRoundUtLen.TextValue
        'dr("FREE_C01") = frm.lblFreeC01.TextValue
        dr("TARIFF_CD") = String.Empty
        dr("WARIMASHI_NR") = String.Empty
        dr("ROUND_KB") = String.Empty
        dr("ROUND_UT") = String.Empty
        dr("ROUND_UT_LEN") = String.Empty
        dr("FREE_C01") = String.Empty
        '要望番号:1482 KIM 2012.10.10 END

        dr("PRINT_KB") = frm.cmbPrint.SelectedValue

        '検索条件をデータセットに設定
        rtDs.Tables(LMI060C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInSpreadData(ByVal frm As LMI060F, ByRef rtDs As DataSet, ByVal printFlg As String)

        Dim dr As DataRow = Nothing
        Dim max As Integer = frm.sprDetail.ActiveSheet.Rows.Count - 1
        '要望番号:1482 KIM 2012.10.10 START
        'Dim custCdL As String = frm.txtCustCdL.TextValue
        '要望番号:1482 KIM 2012.10.10 END
        Dim spdCustCdL As String = String.Empty
        'START YANAI 要望番号1447 山九（危険品一割増されていない）
        '要望番号:1482 KIM 2012.10.10 START
        'Dim custCdM As String = frm.txtCustCdM.TextValue
        '要望番号:1482 KIM 2012.10.10 END
        Dim spdCustCdM As String = String.Empty
        '要望番号:1482 KIM 2012.10.10 START
        'Dim custCdS As String = frm.txtCustCdS.TextValue
        '要望番号:1482 KIM 2012.10.10 END
        Dim spdCustCdS As String = String.Empty
        '要望番号:1482 KIM 2012.10.10 START
        'Dim custCdSS As String = frm.txtCustCdSS.TextValue
        '要望番号:1482 KIM 2012.10.10 END
        Dim spdCustCdSS As String = String.Empty
        'END YANAI 要望番号1447 山九（危険品一割増されていない）

        '要望番号:1557 KIM 2012/11/02 START
        Dim chkDrs As DataRow() = Nothing
        '要望番号:1557 KIM 2012/11/02 END

        '要望番号:1916 SHINOHARA 2013/03/22 20:00 START

        Dim chkCustCdL As String = String.Empty
        Dim chkCustCdM As String = String.Empty
        Dim chkCustCdS As String = String.Empty
        Dim chkCustCdSS As String = String.Empty

        If printFlg = "0" Then '0は実行ボタン押下
            For j As Integer = 0 To max 'チェックボックスのある明細を全件検索
                If Me._LMIconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(j, LMI060C.SprColumnIndex.DEF)).Equals(LMConst.FLG.ON) = True Then
                    chkCustCdL = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(j), LMI060G.sprDetailDef.CUST_CD_L.ColNo))
                    chkCustCdM = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(j), LMI060G.sprDetailDef.CUST_CD_M.ColNo))
                    chkCustCdS = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(j), LMI060G.sprDetailDef.CUST_CD_S.ColNo))
                    chkCustCdSS = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(j), LMI060G.sprDetailDef.CUST_CD_SS.ColNo))

                    For k As Integer = 0 To max 'チェックボックスのある明細と同じ荷主、かつチェックボックスオフを探し、オンにする。
                        If chkCustCdL = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(k), LMI060G.sprDetailDef.CUST_CD_L.ColNo)) AndAlso _
                            chkCustCdM = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(k), LMI060G.sprDetailDef.CUST_CD_M.ColNo)) AndAlso _
                            chkCustCdS = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(k), LMI060G.sprDetailDef.CUST_CD_S.ColNo)) AndAlso _
                            chkCustCdSS = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(k), LMI060G.sprDetailDef.CUST_CD_SS.ColNo)) Then
                            Me._G.setChkSpread(k)

                        End If
                    Next

                End If
            Next



        End If '実行ボタン押下

        '要望番号:1916 SHINOHARA 2013/03/22 END

        For i As Integer = 0 To max
            spdCustCdL = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.CUST_CD_L.ColNo))
            'START YANAI 要望番号1447 山九（危険品一割増されていない）
            'If (custCdL).Equals(spdCustCdL) = True Then
            spdCustCdM = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.CUST_CD_M.ColNo))
            spdCustCdS = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.CUST_CD_S.ColNo))
            spdCustCdSS = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.CUST_CD_SS.ColNo))
            '要望番号:1482 KIM 2012.10.10 START
            'If (custCdL).Equals(spdCustCdL) = True AndAlso _
            '    (custCdM).Equals(spdCustCdM) = True AndAlso _
            '    (custCdS).Equals(spdCustCdS) = True AndAlso _
            '    (custCdSS).Equals(spdCustCdSS) = True Then
            If Me._LMIconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060C.SprColumnIndex.DEF)).Equals(LMConst.FLG.ON) = True Then
                '要望番号:1482 KIM 2012.10.10 END
                'END YANAI 要望番号1447 山九（危険品一割増されていない）

                '要望番号:1557 KIM 2012/11/02 START
                '要望番号:1916 SHINOHARA 2013/03/22 START
                If printFlg = "1" Then '1は印刷ボタン押下
                    chkDrs = rtDs.Tables(LMI060C.TABLE_NM_IN).Select("CUST_CD_L='" & spdCustCdL & "' AND CUST_CD_M='" & spdCustCdM & "' AND CUST_CD_S='" & spdCustCdS & "' AND CUST_CD_SS='" & spdCustCdSS & "' ")
                    If 0 < chkDrs.Length Then
                        Continue For
                    End If
                End If
                '要望番号:1916 SHINOHARA 2013/03/22 END
                '要望番号:1557 KIM 2012/11/02 END

                dr = rtDs.Tables(LMI060C.TABLE_NM_IN).NewRow()

                '検索条件　単項目部
                dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                dr("CUST_CD_L") = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.CUST_CD_L.ColNo))
                dr("CUST_CD_M") = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.CUST_CD_M.ColNo))
                dr("CUST_CD_S") = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.CUST_CD_S.ColNo))
                dr("CUST_CD_SS") = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.CUST_CD_SS.ColNo))
                dr("DATE_FROM") = frm.imdDateFrom.TextValue
                dr("DATE_TO") = frm.imdDateTo.TextValue
                dr("TARIFF_CD") = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.TARIFF_CD.ColNo))
                dr("WARIMASHI_NR") = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.WARIMASHI_NR.ColNo))
                dr("ROUND_KB") = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.ROUND_KB.ColNo))
                dr("ROUND_UT") = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.ROUND_UT.ColNo))
                dr("ROUND_UT_LEN") = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.ROUND_UT_LEN.ColNo))
                dr("FREE_C01") = Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMI060G.sprDetailDef.FREE_C01.ColNo))
                dr("PRINT_KB") = frm.cmbPrint.SelectedValue

                '検索条件をデータセットに設定
                rtDs.Tables(LMI060C.TABLE_NM_IN).Rows.Add(dr)

            End If

        Next
    End Sub

#End Region

#End Region 'Method

End Class
