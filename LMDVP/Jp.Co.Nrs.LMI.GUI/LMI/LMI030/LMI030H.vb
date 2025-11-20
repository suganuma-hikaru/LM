' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI030H : 請求データ作成 [デュポン用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Microsoft.Office.Interop

''' <summary>
''' LMI030ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI030H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI030V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI030G

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
        Dim frm As LMI030F = New LMI030F(Me)

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
        Me._G = New LMI030G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMI030V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G007")

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
    Friend Sub ActionControl(ByVal eventShubetsu As LMI030C.EventShubetsu, ByVal frm As LMI030F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMI030C.EventShubetsu.MAKE    '作成

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                'START YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない
                ''入力チェック
                'If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                '   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                '    '処理終了アクション
                '    Me._LMIConH.EndAction(frm)
                '    Exit Sub
                'End If
                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me.IsCustDetailsServerChk(frm, eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If
                'END YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない

                '作成処理
                Call Me.MakeData(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI030C.EventShubetsu.EXCEL    'EXCEL出力

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                'START YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない
                ''入力チェック
                'If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                '   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                '    '処理終了アクション
                '    Me._LMIConH.EndAction(frm)
                '    Exit Sub
                'End If
                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me.IsCustDetailsServerChk(frm, eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If
                'END YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない

                'EXCEL出力データ検索処理
                rtnDs = Me.SelectExcel(frm)

                'EXCEL出力データ作成処理
                Call Me.MakeExcel(frm, rtnDs)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI030C.EventShubetsu.KENSAKU    '検索

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                'START YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない
                ''入力チェック
                'If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                '   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                '    '処理終了アクション
                '    Me._LMIConH.EndAction(frm)
                '    Exit Sub
                'End If
                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me.IsCustDetailsServerChk(frm, eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If
                'END YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない

                '検索処理
                rtnDs = Me.KensakuData(frm)

                'Spread表示処理
                Me.SetSpread(frm, rtnDs)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI030C.EventShubetsu.PRINT    '印刷

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                'START YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない
                ''入力チェック
                'If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                '   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                '    '処理終了アクション
                '    Me._LMIConH.EndAction(frm)
                '    Exit Sub
                'End If
                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me.IsCustDetailsServerChk(frm, eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If
                'END YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない

                '印刷処理
                Call Me.PrintData(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI030C.EventShubetsu.MASTER    'マスタ参照

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '現在フォーカスのあるコントロール名の取得
                Dim objNm As String = frm.FocusedControlName()

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '荷主コード
                Call Me.ShowPopup(frm, objNm, prm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI030F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByVal frm As LMI030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey8Press")

        '「EXCEL出力」処理
        Me.ActionControl(LMI030C.EventShubetsu.EXCEL, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey8Press")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey9Press")

        '「検索」処理
        Me.ActionControl(LMI030C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMI030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True

        End If

        Me.ActionControl(LMI030C.EventShubetsu.MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI030F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMI030F, ByVal e As FormClosingEventArgs)

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
    Friend Sub btnMake_Click(ByRef frm As LMI030F)

        Logger.StartLog(Me.GetType.Name, "btnMake_Click")

        '「作成」処理
        Me.ActionControl(LMI030C.EventShubetsu.MAKE, frm)

        Logger.EndLog(Me.GetType.Name, "btnMake_Click")

    End Sub

    ''' <summary>
    '''印刷押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LMI030F)

        Logger.StartLog(Me.GetType.Name, "btnPrint_Click")

        '「印刷」処理
        Me.ActionControl(LMI030C.EventShubetsu.PRINT, frm)

        Logger.EndLog(Me.GetType.Name, "btnPrint_Click")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub MakeData(ByVal frm As LMI030F)

        'DataSet設定
        Dim rtDs As DataSet = New LMI030DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "MakeData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA( _
                                             "LMI030BLF", _
                                             "MakeData", _
                                             rtDs)

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
    ''' EXCEL出力データ検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectExcel(ByVal frm As LMI030F) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMI030DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectExcel")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), _
                                                         "LMI030BLF", _
                                                         "SelectExcel", _
                                                         rtDs, _
                                                         Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))), _
                                                         -1)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return rtnDs
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"Excel出力処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectExcel")

        Return rtnDs

    End Function

    ''' <summary>
    ''' EXCEL出力データ出力処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub MakeExcel(ByVal frm As LMI030F, ByVal ds As DataSet)

        If ds.Tables(LMI030C.TABLE_NM_OUT_EXCEL).Rows.Count = 0 Then
            MyBase.ShowMessage(frm, "E296", New String() {"Excel出力対象データ"})
            Exit Sub
        End If

        '区分マスタ検索処理
        Dim excelFlg As String = String.Empty
        If (LMI030C.CUSTCD00088).Equals(frm.txtCustCdL.TextValue) = True  Then
            'DKKの場合
            excelFlg = LMI030C.EXCEL_DKK_FLG
        ElseIf (LMI030C.CUSTCD00089).Equals(frm.txtCustCdL.TextValue) = True Then
            'TEFの場合
            excelFlg = LMI030C.EXCEL_TEF_FLG
        ElseIf (LMI030C.CUSTCD00188).Equals(frm.txtCustCdL.TextValue) = True OrElse _
               (LMI030C.CUSTCD00514).Equals(frm.txtCustCdL.TextValue) = True Then
            'ELEの場合
            excelFlg = LMI030C.EXCEL_ELE_FLG
        ElseIf (LMI030C.CUSTCD00588).Equals(frm.txtCustCdL.TextValue) = True Then
            'AXAの場合
            excelFlg = LMI030C.EXCEL_AXA_FLG
        Else
            MyBase.ShowMessage(frm, "E296", New String() {"Excel出力対象データ"})
            Exit Sub
        End If

        Dim xlsPathKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'E018' AND KBN_CD = '", excelFlg, "'"))
        Dim xlsPath As String = xlsPathKbn(0).Item("KBN_NM1").ToString
        Dim xlsFileName As String = String.Concat(xlsPathKbn(0).Item("KBN_NM2").ToString, Mid(frm.imdSeiqtoTo.TextValue, 1, 6), ".xls")

        'ファイルの存在確認
        Dim fileExistsFlg As Boolean = False
        Dim fileAddFlg As Boolean = False
        If System.IO.File.Exists(String.Concat(xlsPath, xlsFileName)) = True Then
            fileExistsFlg = True
            '上書き確認をする
            Select Case MyBase.ShowMessage(frm, "W190", New String() {xlsFileName})
                Case MsgBoxResult.Yes '「はい」押下時
                    fileAddFlg = True

                Case MsgBoxResult.No '「いいえ」押下時
                    fileAddFlg = False

                Case MsgBoxResult.Cancel '「キャンセル」押下時
                    Exit Sub

            End Select

        End If

        With ds.Tables(LMI030C.TABLE_NM_OUT_EXCEL)

            '編集処理
            Dim max As Integer = .Rows.Count - 1
            Dim custCdL As String = String.Empty
            Dim custCdS As String = String.Empty
            Dim custCdSS As String = String.Empty
            Dim ofbKb As String = String.Empty
            For i As Integer = 0 To max
                '全角を半角にする
                .Rows(i).Item("CUST_ORD_NO") = StrConv(.Rows(i).Item("CUST_ORD_NO").ToString, VbStrConv.Narrow)

                'PRT_TYPE2の値設定
                custCdL = .Rows(i).Item("CUST_CD_L").ToString
                custCdS = .Rows(i).Item("CUST_CD_S").ToString
                custCdSS = .Rows(i).Item("CUST_CD_SS").ToString
                ofbKb = .Rows(i).Item("OFB_KB").ToString
                If ((LMI030C.CUSTCD00088).Equals(custCdL) = True OrElse _
                    (LMI030C.CUSTCD00187).Equals(custCdL) = True) AndAlso _
                   (LMI030C.CUSTCDS03).Equals(custCdS) = True AndAlso _
                   (LMI030C.CUSTCDSS00).Equals(custCdSS) = True Then
                    .Rows(i).Item("PRT_TYPE2") = "1"
                ElseIf ((LMI030C.CUSTCD00088).Equals(custCdL) = True OrElse _
                        (LMI030C.CUSTCD00187).Equals(custCdL) = True) AndAlso _
                       (LMI030C.CUSTCDS03).Equals(custCdS) = False AndAlso _
                       (LMI030C.CUSTCDSS00).Equals(custCdSS) = False Then
                    .Rows(i).Item("PRT_TYPE2") = "1"
                ElseIf ((LMI030C.CUSTCD00089).Equals(custCdL) = True OrElse _
                        (LMI030C.CUSTCD00188).Equals(custCdL) = True OrElse _
                        (LMI030C.CUSTCD00514).Equals(custCdL) = True) AndAlso _
                       (LMI030C.CUSTCDS03).Equals(custCdS) = True Then
                    .Rows(i).Item("PRT_TYPE2") = "1"
                ElseIf ((LMI030C.CUSTCD00089).Equals(custCdL) = True OrElse _
                        (LMI030C.CUSTCD00188).Equals(custCdL) = True OrElse _
                        (LMI030C.CUSTCD00514).Equals(custCdL) = True) AndAlso _
                       (LMI030C.CUSTCDS03).Equals(custCdS) = False AndAlso _
                       (LMI030C.CUSTCDSS02).Equals(custCdSS) = True Then
                    .Rows(i).Item("PRT_TYPE2") = "1"
                ElseIf (LMI030C.CUSTCD00554).Equals(custCdL) = True Then
                    .Rows(i).Item("PRT_TYPE2") = "1"
                ElseIf ((LMI030C.CUSTCD00587).Equals(custCdL) = True OrElse _
                        (LMI030C.CUSTCD00588).Equals(custCdL) = True OrElse _
                        (LMI030C.CUSTCD00589).Equals(custCdL) = True) AndAlso _
                       (LMI030C.CUSTCDSS00).Equals(custCdSS) = False Then
                    .Rows(i).Item("PRT_TYPE2") = "1"
                ElseIf ((LMI030C.CUSTCD00587).Equals(custCdL) = True OrElse _
                        (LMI030C.CUSTCD00588).Equals(custCdL) = True OrElse _
                        (LMI030C.CUSTCD00589).Equals(custCdL) = True) AndAlso _
                       (LMI030C.CUSTCDSS00).Equals(custCdSS) = True AndAlso _
                       ("02").Equals(ofbKb) = True Then
                    .Rows(i).Item("PRT_TYPE2") = "1"
                ElseIf ((LMI030C.CUSTCD00688).Equals(custCdL) = True OrElse _
                        (LMI030C.CUSTCD00689).Equals(custCdL) = True) AndAlso _
                       ("02").Equals(ofbKb) = True Then
                    .Rows(i).Item("PRT_TYPE2") = "1"
                Else
                    .Rows(i).Item("PRT_TYPE2") = "0"
                End If

                'PRT_TYPE1の値設定
                If ("1").Equals(.Rows(i).Item("PRT_TYPE2").ToString) = True Then
                    .Rows(i).Item("PRT_TYPE1") = "0"
                Else
                    .Rows(i).Item("PRT_TYPE1") = "1"
                End If
            Next

            'DataSetの値を二次元配列に格納する
            Dim rowMax As Integer = .Rows.Count - 1
            Dim colMax As Integer = 0
            Dim excelData(0, 0) As String
            If (excelFlg).Equals(LMI030C.EXCEL_DKK_FLG) = True OrElse _
                (excelFlg).Equals(LMI030C.EXCEL_AXA_FLG) = True Then
                'DKKの場合
                colMax = LMI030C.DKK_COL
                ReDim excelData(rowMax, colMax)
                excelData = Me.ExcelDKK(ds, rowMax, colMax, fileAddFlg)
            ElseIf (excelFlg).Equals(LMI030C.EXCEL_TEF_FLG) = True Then
                'TEFの場合
                colMax = LMI030C.TEF_COL
                ReDim excelData(rowMax, colMax)
                excelData = Me.ExcelTEF(ds, rowMax, colMax, fileAddFlg)
            ElseIf (excelFlg).Equals(LMI030C.EXCEL_ELE_FLG) = True Then
                'ELEの場合
                colMax = LMI030C.ELE_COL
                ReDim excelData(rowMax, colMax)
                excelData = Me.ExcelELE(ds, rowMax, colMax, fileAddFlg)
            Else
                Exit Sub
            End If


            'EXCEL起動
            Dim xlsApp As New Excel.Application
            Dim xlsBook As Excel.Workbook = Nothing
            Dim xlsSheets As Excel.Sheets = Nothing
            Dim xlsSheet As Excel.Worksheet = Nothing

            Dim startCell As Excel.Range = Nothing
            Dim endCell As Excel.Range = Nothing
            Dim range As Excel.Range = Nothing
            Dim rowCnt As Integer = 0

            If fileAddFlg = False Then
                '新規作成の場合
                xlsBook = xlsApp.Workbooks.Add()
                xlsSheet = DirectCast(xlsBook.Worksheets(1), Excel.Worksheet)
                startCell = DirectCast(xlsSheet.Cells(1, 1), Excel.Range)
                endCell = DirectCast(xlsSheet.Cells(rowMax + 2, colMax + 1), Excel.Range)
            Else
                '追記の場合
                xlsBook = xlsApp.Workbooks.Open(String.Concat(xlsPath, xlsFileName))
                xlsSheets = xlsBook.Worksheets
                xlsSheet = DirectCast(xlsBook.Worksheets(1), Excel.Worksheet)
                xlsApp.Visible = False

                Dim xlCell As Excel.Range = Nothing
                rowCnt = 1
                Do
                    xlCell = DirectCast(xlsSheet.Cells(rowCnt, 1), Excel.Range)
                    If String.IsNullOrEmpty(Convert.ToString(xlCell.Value)) = True Then
                        Exit Do
                    End If
                    rowCnt = rowCnt + 1
                Loop
                startCell = DirectCast(xlsSheet.Cells(rowCnt, 1), Excel.Range)
                endCell = DirectCast(xlsSheet.Cells(rowMax + rowCnt, colMax + 1), Excel.Range)

            End If
            range = xlsSheet.Range(startCell, endCell)
            range.Value = excelData

            xlsApp.DisplayAlerts = False '保存時の問合せのダイアログを非表示に設定

            Try
                '保存時、上書き確認ポップで「いいえ」「キャンセル」選択時にエラーになるため
                If fileAddFlg = False Then
                    '新規作成の場合
                    System.IO.Directory.CreateDirectory(xlsPath)
                    xlsBook.SaveAs(String.Concat(xlsPath, xlsFileName))
                Else
                    '追記の場合
                    xlsBook.Save()
                End If
            Catch ex As Exception

            End Try
            xlsApp.DisplayAlerts = True      '元に戻す

            xlsSheets = Nothing
            xlsSheet = Nothing
            xlsBook.Close(False) 'Excelを閉じる
            xlsBook = Nothing
            xlsApp.Quit()
            xlsApp = Nothing

        End With

    End Sub

    ''' <summary>
    ''' Excel出力値の設定(DKK)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function ExcelDKK(ByVal ds As DataSet, ByVal rowMax As Integer, ByVal colMax As Integer, ByVal fileAddFlg As Boolean) As String(,)

        'DataSetの値を二次元配列に格納する
        Dim excelData(rowMax + 1, colMax + 1) As String
        Dim titleRow As Integer = 0

        If fileAddFlg = False Then
            'タイトル列を設定
            excelData(titleRow, 0) = LMI030C.DKK_COL_01
            excelData(titleRow, 1) = LMI030C.DKK_COL_02
            excelData(titleRow, 2) = LMI030C.DKK_COL_03
            excelData(titleRow, 3) = LMI030C.DKK_COL_04
            excelData(titleRow, 4) = LMI030C.DKK_COL_05
            excelData(titleRow, 5) = LMI030C.DKK_COL_06
            excelData(titleRow, 6) = LMI030C.DKK_COL_07
            excelData(titleRow, 7) = LMI030C.DKK_COL_08
            excelData(titleRow, 8) = LMI030C.DKK_COL_09
            excelData(titleRow, 9) = LMI030C.DKK_COL_10
            excelData(titleRow, 10) = LMI030C.DKK_COL_11
            excelData(titleRow, 11) = LMI030C.DKK_COL_12
            excelData(titleRow, 12) = LMI030C.DKK_COL_13
            excelData(titleRow, 13) = LMI030C.DKK_COL_14
            excelData(titleRow, 14) = LMI030C.DKK_COL_15
            excelData(titleRow, 15) = LMI030C.DKK_COL_16
            excelData(titleRow, 16) = LMI030C.DKK_COL_17
            titleRow = 1
        End If

        '値を設定
        With ds.Tables(LMI030C.TABLE_NM_OUT_EXCEL)
            For i As Integer = 0 To rowMax
                excelData(i + titleRow, 0) = .Rows(i).Item("UNSO_NO").ToString
                excelData(i + titleRow, 1) = .Rows(i).Item("OUTKA_PLAN_DATE").ToString
                excelData(i + titleRow, 2) = .Rows(i).Item("TRS").ToString
                excelData(i + titleRow, 3) = .Rows(i).Item("CUST_ORD_NO").ToString
                excelData(i + titleRow, 4) = .Rows(i).Item("DEST_NM").ToString
                excelData(i + titleRow, 5) = .Rows(i).Item("SHI").ToString
                excelData(i + titleRow, 6) = .Rows(i).Item("SEIQ_KYORI").ToString
                excelData(i + titleRow, 7) = .Rows(i).Item("SEIQ_NG_NB").ToString
                excelData(i + titleRow, 8) = Convert.ToString(Convert.ToDouble(.Rows(i).Item("SEIQ_WT").ToString))
                excelData(i + titleRow, 9) = Convert.ToString(Convert.ToDouble(.Rows(i).Item("DECI_UNCHIN").ToString))
                excelData(i + titleRow, 10) = .Rows(i).Item("GOODS_NM_1").ToString
                excelData(i + titleRow, 11) = .Rows(i).Item("ARR_PLAN_DATE").ToString
                excelData(i + titleRow, 12) = .Rows(i).Item("FRB_CD").ToString
                excelData(i + titleRow, 13) = .Rows(i).Item("DEPART").ToString
                excelData(i + titleRow, 14) = .Rows(i).Item("CUST_GOODS_CD").ToString
                excelData(i + titleRow, 15) = .Rows(i).Item("SRC_CD").ToString
                excelData(i + titleRow, 16) = .Rows(i).Item("DEST_CD").ToString
            Next
        End With

        Return excelData

    End Function

    ''' <summary>
    ''' Excel出力値の設定(TEF)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function ExcelTEF(ByVal ds As DataSet, ByVal rowMax As Integer, ByVal colMax As Integer, ByVal fileAddFlg As Boolean) As String(,)

        'DataSetの値を二次元配列に格納する
        Dim excelData(rowMax + 1, colMax + 1) As String
        Dim titleRow As Integer = 0

        If fileAddFlg = False Then
            'タイトル列を設定
            excelData(titleRow, 0) = LMI030C.TEF_COL_01
            excelData(titleRow, 1) = LMI030C.TEF_COL_02
            excelData(titleRow, 2) = LMI030C.TEF_COL_03
            excelData(titleRow, 3) = LMI030C.TEF_COL_04
            excelData(titleRow, 4) = LMI030C.TEF_COL_05
            excelData(titleRow, 5) = LMI030C.TEF_COL_06
            excelData(titleRow, 6) = LMI030C.TEF_COL_07
            excelData(titleRow, 7) = LMI030C.TEF_COL_08
            excelData(titleRow, 8) = LMI030C.TEF_COL_09
            excelData(titleRow, 9) = LMI030C.TEF_COL_10
            excelData(titleRow, 10) = LMI030C.TEF_COL_11
            excelData(titleRow, 11) = LMI030C.TEF_COL_12
            excelData(titleRow, 12) = LMI030C.TEF_COL_13
            excelData(titleRow, 13) = LMI030C.TEF_COL_14
            titleRow = 1
        End If

        '値を設定
        With ds.Tables(LMI030C.TABLE_NM_OUT_EXCEL)
            For i As Integer = 0 To rowMax
                excelData(i + titleRow, 0) = .Rows(i).Item("OUTKA_PLAN_DATE").ToString
                excelData(i + titleRow, 1) = .Rows(i).Item("CUST_ORD_NO").ToString
                excelData(i + titleRow, 2) = .Rows(i).Item("DEST_NM").ToString
                excelData(i + titleRow, 3) = .Rows(i).Item("SHI").ToString
                excelData(i + titleRow, 4) = .Rows(i).Item("SEIQ_WT").ToString
                excelData(i + titleRow, 5) = .Rows(i).Item("SEIQ_KYORI").ToString
                excelData(i + titleRow, 6) = .Rows(i).Item("SEIQ_NG_NB").ToString
                excelData(i + titleRow, 7) = .Rows(i).Item("DECI_UNCHIN").ToString
                excelData(i + titleRow, 8) = .Rows(i).Item("GOODS_NM_1").ToString
                excelData(i + titleRow, 9) = .Rows(i).Item("FRB_CD").ToString
                excelData(i + titleRow, 10) = .Rows(i).Item("SRC_CD").ToString
                excelData(i + titleRow, 11) = .Rows(i).Item("DUPONT_PKG_UT").ToString
                excelData(i + titleRow, 12) = .Rows(i).Item("CUST_GOODS_CD").ToString
                excelData(i + titleRow, 13) = .Rows(i).Item("DEST_CD").ToString
            Next
        End With

        Return excelData

    End Function

    ''' <summary>
    ''' Excel出力値の設定(ELE)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function ExcelELE(ByVal ds As DataSet, ByVal rowMax As Integer, ByVal colMax As Integer, ByVal fileAddFlg As Boolean) As String(,)

        'DataSetの値を二次元配列に格納する
        Dim excelData(rowMax + 1, colMax + 1) As String
        Dim titleRow As Integer = 0

        If fileAddFlg = False Then
            'タイトル列を設定
            excelData(titleRow, 0) = LMI030C.ELE_COL_01
            excelData(titleRow, 1) = LMI030C.ELE_COL_02
            excelData(titleRow, 2) = LMI030C.ELE_COL_03
            excelData(titleRow, 3) = LMI030C.ELE_COL_04
            excelData(titleRow, 4) = LMI030C.ELE_COL_05
            excelData(titleRow, 5) = LMI030C.ELE_COL_06
            excelData(titleRow, 6) = LMI030C.ELE_COL_07
            excelData(titleRow, 7) = LMI030C.ELE_COL_08
            excelData(titleRow, 8) = LMI030C.ELE_COL_09
            excelData(titleRow, 9) = LMI030C.ELE_COL_10
            excelData(titleRow, 10) = LMI030C.ELE_COL_11
            excelData(titleRow, 11) = LMI030C.ELE_COL_12
            excelData(titleRow, 12) = LMI030C.ELE_COL_13
            excelData(titleRow, 13) = LMI030C.ELE_COL_14
            titleRow = 1
        End If

        '値を設定
        With ds.Tables(LMI030C.TABLE_NM_OUT_EXCEL)
            For i As Integer = 0 To rowMax
                excelData(i + titleRow, 0) = .Rows(i).Item("OUTKA_PLAN_DATE").ToString
                excelData(i + titleRow, 1) = .Rows(i).Item("CUST_ORD_NO").ToString
                excelData(i + titleRow, 2) = .Rows(i).Item("DEST_NM").ToString
                excelData(i + titleRow, 3) = .Rows(i).Item("SHI").ToString
                excelData(i + titleRow, 4) = .Rows(i).Item("SEIQ_WT").ToString
                excelData(i + titleRow, 5) = .Rows(i).Item("SEIQ_KYORI").ToString
                excelData(i + titleRow, 6) = .Rows(i).Item("SEIQ_NG_NB").ToString
                excelData(i + titleRow, 7) = .Rows(i).Item("DECI_UNCHIN").ToString
                excelData(i + titleRow, 8) = .Rows(i).Item("GOODS_NM_1").ToString
                excelData(i + titleRow, 9) = .Rows(i).Item("FRB_CD").ToString
                excelData(i + titleRow, 10) = .Rows(i).Item("SRC_CD").ToString
                excelData(i + titleRow, 11) = .Rows(i).Item("DUPONT_PKG_UT").ToString
                excelData(i + titleRow, 12) = .Rows(i).Item("CUST_GOODS_CD").ToString
                excelData(i + titleRow, 13) = .Rows(i).Item("DEST_CD").ToString
            Next
        End With

        Return excelData

    End Function

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function KensakuData(ByVal frm As LMI030F) As DataSet

        ''SPREAD初期化
        frm.sprDetail.CrearSpread()

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim rtDs As DataSet = New LMI030DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "KensakuData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), _
                                                         "LMI030BLF", _
                                                         "SelectListData", _
                                                         rtDs, _
                                                         Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))), _
                                                         -1)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return rtnDs
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"検索処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "KensakuData")

        Return rtnDs

    End Function

    ''' <summary>
    ''' 検索結果をSpreadに表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SetSpread(ByVal frm As LMI030F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMI030C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G016", New String() {Convert.ToString(frm.sprDetail.ActiveSheet.Rows.Count)})

    End Sub

    ''' <summary>
    ''' マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>参照Popupが開く場合のコントロールです。</remarks>
    Private Sub ShowPopup(ByVal frm As LMI030F, ByVal objNM As String, ByRef prm As LMFormData)


        'オブジェクト名による分岐
        Select Case objNM

            Case "txtCustCdL", "txtCustCdM", "txtCustCdS", "txtCustCdSS" '荷主マスタ参照

                Dim prmDs As DataSet = New LMZ260DS
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
                If Me._PopupSkipFlg = False Then
                    row("CUST_CD_L") = frm.txtCustCdL.TextValue
                    row("CUST_CD_M") = frm.txtCustCdM.TextValue
                    row("CUST_CD_S") = frm.txtCustCdS.TextValue
                    row("CUST_CD_SS") = frm.txtCustCdSS.TextValue
                End If
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                'START YANAI 要望番号830
                row("SEARCH_CS_FLG") = LMConst.FLG.ON
                'END YANAI 要望番号830
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                frm.lblCustNmL.TextValue = String.Empty
                frm.lblCustNmM.TextValue = String.Empty
                frm.lblCustNmS.TextValue = String.Empty
                frm.lblCustNmSS.TextValue = String.Empty

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

        End Select

        '戻り処理
        If prm.ReturnFlg = True Then
            Select Case objNM

                Case "txtCustCdL", "txtCustCdM", "txtCustCdS", "txtCustCdSS" '荷主マスタ参照

                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                        frm.txtCustCdL.TextValue = .Item("CUST_CD_L").ToString()      '荷主コード（大）
                        frm.lblCustNmL.TextValue = .Item("CUST_NM_L").ToString()      '荷主名（大）
                        frm.txtCustCdM.TextValue = .Item("CUST_CD_M").ToString()      '荷主コード（中）
                        frm.lblCustNmM.TextValue = .Item("CUST_NM_M").ToString()      '荷主名（中）
                        frm.txtCustCdS.TextValue = .Item("CUST_CD_S").ToString()      '荷主コード（小）
                        frm.lblCustNmS.TextValue = .Item("CUST_NM_S").ToString()      '荷主名（小）
                        frm.txtCustCdSS.TextValue = .Item("CUST_CD_SS").ToString()    '荷主コード（極小）
                        frm.lblCustNmSS.TextValue = .Item("CUST_NM_SS").ToString()    '荷主名（極小）
                    End With

            End Select

        End If

        MyBase.ShowMessage(frm, "G007")

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub PrintData(ByVal frm As LMI030F)

        'DataSet設定
        Dim rtnDs As DataSet = New LMI030DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtnDs)

        rtnDs.Merge(New RdPrevInfoDS)
        rtnDs.Tables(LMConst.RD).Clear()

        'サーバに渡すレコードが存在する場合、更新処理
        If 0 < rtnDs.Tables(LMI030C.TABLE_NM_IN).Rows.Count Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

            '==== WSAクラス呼出（変更処理） ====
            rtnDs = MyBase.CallWSA("LMI030BLF", "PrintData", rtnDs)

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

    'START YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない
    ''' <summary>
    ''' 荷主明細検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function IsCustDetailsServerChk(ByVal frm As LMI030F, ByVal eventShubetsu As LMI030C.EventShubetsu) As Boolean

        Dim eventNm As String = String.Empty
        If LMI030C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
            eventNm = LMI030C.EVENTNAME_EXCEL
        ElseIf LMI030C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
            eventNm = LMI030C.EVENTNAME_KENSAKU
        ElseIf LMI030C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
            eventNm = LMI030C.EVENTNAME_MASTER
        ElseIf LMI030C.EventShubetsu.CLOSE.Equals(eventShubetsu) = True Then
            eventNm = LMI030C.EVENTNAME_CLOSE
        ElseIf LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = True Then
            eventNm = LMI030C.EVENTNAME_MAKE
        ElseIf LMI030C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
            eventNm = LMI030C.EVENTNAME_PRINT
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMI030DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCustDetails")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI030BLF", "SelectCustDetails", rtDs)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm, "E431", New String() {eventNm})
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCustDetails")

        Return True

    End Function
    'END YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInData(ByVal frm As LMI030F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMI030C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("CUST_CD_L") = frm.txtCustCdL.TextValue
        dr("CUST_CD_M") = frm.txtCustCdM.TextValue
        dr("CUST_CD_S") = frm.txtCustCdS.TextValue
        dr("CUST_CD_SS") = frm.txtCustCdSS.TextValue
        dr("SKYU_DATE_FROM") = frm.imdSeiqtoFrom.TextValue
        dr("SKYU_DATE_TO") = frm.imdSeiqtoTo.TextValue

        If ("01").Equals(frm.cmbSeikyu.SelectedValue) = True Then
            '全部選択時
            dr("SEKY_KMK1") = "01"
            dr("SEKY_KMK2") = "02"
            dr("SEKY_KMK3") = "03"
        ElseIf ("02").Equals(frm.cmbSeikyu.SelectedValue) = True Then
            '保管選択時
            dr("SEKY_KMK1") = "01"
            dr("SEKY_KMK2") = "00"
            dr("SEKY_KMK3") = "00"
        ElseIf ("03").Equals(frm.cmbSeikyu.SelectedValue) = True Then
            '請求選択時
            dr("SEKY_KMK1") = "00"
            dr("SEKY_KMK2") = "02"
            dr("SEKY_KMK3") = "00"
        ElseIf ("04").Equals(frm.cmbSeikyu.SelectedValue) = True Then
            '請求選択時
            dr("SEKY_KMK1") = "00"
            dr("SEKY_KMK2") = "00"
            dr("SEKY_KMK3") = "03"
        End If

        dr("DEPART") = frm.cmbJigyoubu.SelectedValue
        dr("PRINT_KB") = frm.cmbPrint.SelectedValue
        'START YANAI 要望番号830
        'スキーマ名取得用
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
        dr.Item("USER_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
        'END YANAI 要望番号830
        dr("MAIN_BR") = Me.GetMainBrCd()

        '検索条件をデータセットに設定
        rtDs.Tables(LMI030C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' デュポン業務主営業所コードの取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMainBrCd() As String

        Dim mainBrRr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'D018' AND KBN_CD ='00'"))

        Return mainBrRr(0).Item("KBN_NM1").ToString()

    End Function

#End Region

#End Region 'Method

End Class
