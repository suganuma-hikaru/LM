' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI210  : ハネウェル管理(鈴木商館)
'  作  成  者       :  [KIM]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI210ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI210H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI210V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI210G

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
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet = New LMI210DS

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
        Dim frm As LMI210F = New LMI210F(Me)

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
        Me._G = New LMI210G(Me, frm, Me._LMIConG)

        'Validateクラスの設定
        Me._V = New LMI210V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMI210G.MODE_SHOKI)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定 & 初期値設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G006")

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
    Friend Sub ActionControl(ByVal eventShubetsu As LMI210C.EventShubetsu, ByVal frm As LMI210F)

        'パラメータクラス生成
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        '処理開始アクション
        Me._LMIConH.StartAction(frm)

        '入力チェック
        If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
           Me._V.IsKanrenCheck(eventShubetsu) = False Then
            '処理終了アクション
            Me._LMIConH.EndAction(frm, "G006")
            Exit Sub
        End If

        'イベント種別による分岐
        Select Case eventShubetsu

            Case LMI210C.EventShubetsu.KENSAKU    '検索(F9)

                '検索処理
                Call Me.KensakuMain(frm)

            Case LMI210C.EventShubetsu.EXCEL     'Excel出力

                'ハネウェル管理（鈴木商館）Excel作成処理呼び出し
                Call Me.ShowExcelLMI910(frm)

            Case LMI210C.EventShubetsu.DOUBLECLICK     'セルダブルクリック

                '履歴表示画面呼び出し処理
                Call Me.ShowLMI230(frm)

        End Select

        '処理終了アクション
        Me._LMIConH.EndAction(frm, "G006")

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI210F, ByVal e As FormClosingEventArgs) As Boolean

        Return True

    End Function

    Private Sub ChangeOptionBtn(ByVal frm As LMI210F)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

    End Sub

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI210F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey9Press")

        Call Me.ActionControl(LMI210C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMI210F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        Call Me.ActionControl(LMI210C.EventShubetsu.EXCEL, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI210F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMI210F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm, e)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' Spreadセルダブルクリックイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub SpreadCellDoubleClick(ByVal frm As LMI210F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SpreadCellDoubleClick")

        Call Me.ActionControl(LMI210C.EventShubetsu.DOUBLECLICK, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SpreadCellDoubleClick")

    End Sub

    Friend Sub optButtomChange(ByRef frm As LMI210F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "optButtomChange")

        Call Me.ChangeOptionBtn(frm)

        Logger.EndLog(Me.GetType.Name, "optButtomChange")

    End Sub
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' 検索処理 (F9)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub KensakuMain(ByVal frm As LMI210F)

        '一覧クリア
        Call Me._G.InitSpread()

        'データセット設定
        Dim ds As DataSet = Me.SetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "KensakuMain")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blfNm As String = String.Empty
        If frm.optOutka.Checked = True Then
            '出荷検索の場合
            blfNm = "SelectKensakuOutkaData"
        ElseIf frm.optInka.Checked = True Then
            '入荷検索の場合
            blfNm = "SelectKensakuInkaData"
        End If

        '検索データ取得
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form) _
                                                       , "LMI210BLF", blfNm, ds _
                                                       , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
                                                       , Convert.ToInt32(Convert.ToDouble( _
                                                         MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                                         .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "KensakuMain")

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()
            Exit Sub
        End If

        'エラーがある場合、メッセージ表示
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        Me._Ds = rtnDs

        '検索件数が0件の場合、メッセージ設定
        If Me._Ds.Tables(LMI210C.TABLE_NM_OUT).Rows.Count = 0 Then
            MyBase.ShowMessage(frm, "G001")
            Call Me._G.SetFunctionKey(LMI210G.MODE_SHOKI)
            Exit Sub
        End If

        'スプレッドに検索結果を設定
        If frm.optOutka.Checked = True Then
            '出荷検索の場合
            Call Me._G.SetSpreadOutka(frm.sprDetails, rtnDs)
        Else
            '入荷検索の場合
            Call Me._G.SetSpreadInka(frm.sprDetails, rtnDs)
        End If

        'ファンクションキー設定
        Call Me._G.SetFunctionKey(LMI210G.MODE_SEARCH)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"検索処理", ""})

    End Sub

    ''' <summary>
    ''' Excel作成(LMI90)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowExcelLMI910(ByVal frm As LMI210F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'InDataSet設定
        Dim prmDs As DataSet = Me.SetLMI910InDataSet(frm)
        prm.ParamDataSet = prmDs

        'Excel作成処理呼出
        LMFormNavigate.NextFormNavigate(Me, "LMI910", prm)

        If prm.ReturnFlg = False Then
            'メッセージエリアの設定
            MyBase.SetMessage("E501")
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"Excel出力処理", ""})

    End Sub

    'LMI230：履歴表示画面未使用によりコメント -- START --
    ''' <summary>
    ''' 一覧ダブルクリック：履歴表示画面を開く (LMI230)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowLMI230(ByVal frm As LMI210F)

        ''パラメータクラス生成
        'Dim prm As LMFormData = New LMFormData()

        ''DataSet設定
        'Dim prmDs As DataSet = Nothing
        'Dim row As DataRow = Nothing
        'Dim rowNo As Integer = frm.sprDetails.ActiveSheet.ActiveRowIndex
        'Dim colNo As Integer = LMI210C.SprColumnIndexOUTKA.SERIAL_NO

        'With frm.sprDetails.ActiveSheet

        '    If .Columns(LMI210C.SprColumnIndexOUTKA.DUMMY1).Visible = False Then
        '        colNo = LMI210C.SprColumnIndexINKA.SERIAL_NO
        '    End If

        '    'パラメータ設定
        '    prmDs = New LMI230DS
        '    row = prmDs.Tables(LMI230C.TABLE_NM_IN).NewRow
        '    row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
        '    row("SERIAL_NO") = Me._LMIconV.GetCellValue(.Cells(rowNo, colNo))
        '    prmDs.Tables(LMI230C.TABLE_NM_IN).Rows.Add(row)
        '    prm.ParamDataSet = prmDs

        'End With

        ''履歴表示画面を開く
        'LMFormNavigate.NextFormNavigate(Me, "LMI230", prm)

    End Sub
    'LMI230：履歴表示画面未使用によりコメント --  END  --

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' INデータセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInData(ByVal frm As LMI210F) As DataSet

        Dim rtDs As DataSet = New LMI210DS()
        Dim dr As DataRow = rtDs.Tables(LMI210C.TABLE_NM_IN).NewRow()

        dr = rtDs.Tables(LMI210C.TABLE_NM_IN).NewRow()

        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue

        '2013.08.15 要望対応2095 START
        '冷媒商品設定
        dr("COOLANT_GOODS_KB") = frm.cmbCoolantGoodsKb.SelectedValue
        '2013.08.15 要望対応2095 END

        '入出荷日FROM設定
        If frm.optInka.Checked = True Then
            dr("INKA_DATE_FROM") = frm.imdInkaDateFrom.TextValue
        ElseIf frm.optOutka.Checked = True Then
            dr("OUTKA_DATE_FROM") = frm.imdOutkaDateFrom.TextValue
        End If

        '入出荷日TO設定
        If frm.optInka.Checked = True Then
            dr("INKA_DATE_TO") = frm.imdInkaDateTo.TextValue
        ElseIf frm.optOutka.Checked = True Then
            dr("OUTKA_DATE_TO") = frm.imdOutkaDateTo.TextValue
        End If


        '入出在その他区分の設定
        If frm.optInka.Checked = True Then
            dr("IOZS_KB") = "10"
        ElseIf frm.optOutka.Checked = True Then
            dr("IOZS_KB") = "20"
        Else
            dr("IOZS_KB") = String.Empty
        End If

        'ｼﾘﾝﾀﾞﾀｲﾌﾟの設定
        dr("CYLINDER_TYPE") = frm.cmbCylinderType.SelectedValue
        '遅延開始日
        dr("BASE_DATE") = frm.imdBaseDate.TextValue

        'データセットに設定
        rtDs.Tables(LMI210C.TABLE_NM_IN).Rows.Add(dr)

        Return rtDs

    End Function

    ''' <summary>
    ''' Excel出力処理用データセット作成
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetLMI910InDataSet(ByVal frm As LMI210F) As DataSet

        Dim prmDs As DataSet = New LMI910DS()
        Dim row As DataRow = Nothing
        Dim inkaFlg As Boolean = False

        With frm.sprDetails.ActiveSheet

            If .Columns(LMI210C.SprColumnIndexOUTKA.DUMY1).Visible = False Then
                inkaFlg = True
            End If

            For i As Integer = 0 To .Rows.Count - 1

                row = prmDs.Tables(LMI210C.TABLE_NM_EXCEL_IN).NewRow

                If inkaFlg = True Then '返却

                    row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
                    row("IOZS_KB") = "10"
                    row("SERIAL_NO") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.SERIAL_NO))
                    row("YOUKINO") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.YOUKINO))
                    row("CYLINDER_TYPE") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.CYLINDER_TYPE))
                    row("NEXT_TEST_DATE") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.NEXT_TEST_DATE))
                    row("WH_CD") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.NEXT_TEST_DATE))
                    row("WH_NM") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.NEXT_TEST_DATE))
                    row("INOUT_DATE") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.INOUT_DATE))
                    row("TOFROM_CD_OUT") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.TOFROM_CD_OUT))
                    row("TOFROM_NM_OUT") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.TOFROM_NM_OUT))
                    row("INOUT_DATE_OUT") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.INOUT_DATE_OUT))
                    row("TOFROM_CD") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.TOFROM_CD))
                    row("TOFROM_NM") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.TOFROM_NM))
                    row("SHIP_CD_L") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.SHIP_CD_L))
                    row("SHIP_NM_L") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.SHIP_NM_L))
                    row("LAYT_DAY") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.LAYT_DAY))
                    row("PENALTY") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.PENALTY))
                    row("BONUS") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.BONUS))
                    row("SALES_TO") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexINKA.SALES_TO))
                    row("BUYER_ORD_NO_DTL") = String.Empty
                    row("CUST_ORD_NO_DTL") = String.Empty

                Else '出荷

                    row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
                    row("IOZS_KB") = "20"
                    row("SERIAL_NO") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexOUTKA.SERIAL_NO))
                    row("YOUKINO") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexOUTKA.YOUKINO))
                    row("CYLINDER_TYPE") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexOUTKA.CYLINDER_TYPE))
                    row("NEXT_TEST_DATE") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexOUTKA.NEXT_TEST_DATE))
                    row("WH_CD") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexOUTKA.NEXT_TEST_DATE))
                    row("WH_NM") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexOUTKA.NEXT_TEST_DATE))
                    row("INOUT_DATE") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexOUTKA.INOUT_DATE))
                    row("TOFROM_CD_OUT") = String.Empty
                    row("TOFROM_NM_OUT") = String.Empty
                    row("INOUT_DATE_OUT") = String.Empty
                    row("TOFROM_CD") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexOUTKA.TOFROM_CD))
                    row("TOFROM_NM") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexOUTKA.TOFROM_NM))
                    row("SHIP_CD_L") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexOUTKA.SHIP_CD_L))
                    row("SHIP_NM_L") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexOUTKA.SHIP_NM_L))
                    row("LAYT_DAY") = String.Empty
                    row("PENALTY") = String.Empty
                    row("BONUS") = String.Empty
                    row("SALES_TO") = String.Empty
                    row("BUYER_ORD_NO_DTL") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexOUTKA.BUYER_ORD_NO_DTL))
                    row("CUST_ORD_NO_DTL") = Me._LMIconV.GetCellValue(.Cells(i, LMI210C.SprColumnIndexOUTKA.CUST_ORD_NO_DTL))

                End If

                prmDs.Tables(LMI210C.TABLE_NM_EXCEL_IN).Rows.Add(row)

            Next

        End With

        Return prmDs

    End Function

#End Region

#End Region 'Method

End Class
