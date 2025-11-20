' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC030H : 送状番号入力
'  作  成  者       :  nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMC030ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC030H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMC030V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMC030G

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconH As LMCControlH

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
        Dim frm As LMC030F = New LMC030F(Me)

        'Validateクラスの設定
        Me._V = New LMC030V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMC030G(Me, frm)

        'Hnadler共通クラスの設定
        Me._LMCconH = New LMCControlH(DirectCast(frm, Form))

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'コントロールの入力制限
        Call Me._G.SetControlsStatus()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        '↓ データ取得の必要があればここにコーディングする。


        '↑ データ取得の必要があればここにコーディングする。

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "外部Method"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMC030C.EventShubetsu, ByVal frm As LMC030F)

        Call Me._LMCconH.StartAction(frm)

        '権限チェック（共通）
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Call Me._LMCconH.EndAction(frm)
            Exit Sub
        End If

        'イベント種別による分岐
        Select Case eventShubetsu

            '追加処理
            Case LMC030C.EventShubetsu.TSUIKA

                '先頭・末尾のaを除外
                Call Me._G.RemoveBarcodeA()

                '項目チェック
                If Me._V.IsTsuikaSingleCheck() = False Then
                    Call Me._LMCconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me._LMCconH.EndAction(frm)

                'スプレッド追加
                Call Me._G.AddSpread(frm)

                '画面設定
                Call Me._G.ClearControl()

                Call Me._G.SetFoucus()

            Case LMC030C.EventShubetsu.KOUSHIN

                If Me._V.IsKoshinKanrenCheck() = False Then
                    Call Me._LMCconH.EndAction(frm)
                    Exit Sub
                End If

                '名称取得処理
                Call Me.GetNameSippingCompAndDestination(frm)

            Case LMC030C.EventShubetsu.HOZON

                If Me._V.IsHozonKanrenCheck() = False Then
                    Call Me._LMCconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.Hozon(frm)

        End Select

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub
#End Region '外部Method

#Region "内部Method"

    ''' <summary>
    ''' 名称取得処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetNameSippingCompAndDestination(ByVal frm As LMC030F)
        'DataSet設定
        Dim rtDs As DataSet = New LMC030DS()

        Dim chkList As ArrayList = Me._V.getCheckList()
        Call Me.SetDataSetInData(frm, rtDs, chkList)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        'WSAクラス呼出
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC030BLF", "SelectListData", rtDs)

        '名称取得成功時共通処理を行う
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
        Else
            Call Me.SetSelectData(frm, rtnDs, chkList)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub Hozon(ByVal frm As LMC030F)

        '続行確認
        Dim rtn As MsgBoxResult

        'rtn = Me.ShowMessage(frm, "C001", New String() {"保存"})
        rtn = MsgBoxResult.Ok

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMC030DS()
        Dim chkList As ArrayList = Me._V.getCheckList()
        Call Me.SetDataSetInData(frm, ds, chkList)

        'WSAクラス呼出（保存処理）
        ds = MyBase.CallWSA("LMC030BLF", "HozonData", ds)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist() = True Then

            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")

            Call Me._LMCconH.EndAction(frm)
            Exit Sub
        Else

            '保存成功時処理
            Call Me.SuccessHozon(frm)

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DataSave")

        Call Me._LMCconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 名称取得成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <param name="chkList">リスト</param>
    ''' <remarks></remarks>
    Private Sub SetSelectData(ByVal frm As LMC030F, ByVal ds As DataSet, ByVal chkList As ArrayList)

        Dim dt As DataTable = ds.Tables(LMC030C.TABLE_NM_OUT)
      
        'メッセージエリアの設定
        If MyBase.IsMessageStoreExist() = True Then

            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")

        Else

            MyBase.ShowMessage(frm, "G002", New String() {"名称取得処理", String.Empty})

        End If

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt, chkList)

    End Sub

    ''' <summary>
    ''' 保存成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SuccessHozon(ByVal frm As LMC030F)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"保存", String.Empty})

        Dim chkList As ArrayList = Me._V.getCheckList()
        Call Me._G.DelSpread(chkList)

    End Sub

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="rtDs">DataSet</param>
    ''' <param name="chkList">リスト</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMC030F, ByRef rtDs As DataSet, ByVal chkList As ArrayList)

        Dim max As Integer = chkList.Count() - 1
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        Dim NrsBrCd As String = LMUserInfoManager.GetNrsBrCd()
        'Dim NrsBrCd As String = rtDs.Tables(LMC030C.TABLE_NM_IN)(0).Item("NRS_BR_CD").ToString()

        Dim rtDt As DataTable = rtDs.Tables(LMC030C.TABLE_NM_IN)
        Dim rtDr As DataRow = Nothing
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            With frm.sprOkuriList.ActiveSheet

                rtDr = rtDt.NewRow()
                rowNo = Convert.ToInt32(chkList(i))
                rtDr.Item("OUTKA_NO_L") = .Cells(rowNo, LMC030G.sprOkuriListDef.OUTKA_CTL_NO.ColNo).Value()
                rtDr.Item("DENP_NO") = .Cells(rowNo, LMC030G.sprOkuriListDef.DENP_NO.ColNo).Value()
                rtDr.Item("NRS_BR_CD") = NrsBrCd
                rtDr.Item("ROW_NO") = rowNo

                rtDt.Rows.Add(rtDr)

            End With

        Next

    End Sub

#End Region 'DataSet設定

#End Region '内部Method

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMC030F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMC030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "AddList")

        '「追加」処理
        Call Me.ActionControl(LMC030C.EventShubetsu.TSUIKA, frm)

        Logger.EndLog(MyBase.GetType.Name, "AddList")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMC030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '「更新」処理
        Call Me.ActionControl(LMC030C.EventShubetsu.KOUSHIN, frm)

        Logger.EndLog(MyBase.GetType.Name, "SelectListData")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMC030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "Uptake")

        '「取込」処理
        Call Me.ActionControl(LMC030C.EventShubetsu.TORIKOMI, frm)

        Logger.EndLog(MyBase.GetType.Name, "Uptake")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMC030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "DataSave")

        '「保存」処理
        Call Me.ActionControl(LMC030C.EventShubetsu.HOZON, frm)

        Logger.EndLog(MyBase.GetType.Name, "DataSave")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMC030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMC030F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub


#End Region 'イベント振分け

#End Region 'Method

End Class