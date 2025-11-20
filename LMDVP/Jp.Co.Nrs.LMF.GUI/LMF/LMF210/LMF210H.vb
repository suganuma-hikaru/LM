' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF210H : 運行情報一覧検索
'  作  成  者       :  [ito]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
'WAS処理で使用中
Imports Jp.Co.Nrs.LM.GUI.Win
'選択処理で使用中
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMF210ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMF210H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"


    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF210F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMF210V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMF210G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconV As LMFControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconH As LMFControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconG As LMFControlG

    ''' <summary>
    ''' パラメータのNFFormDataをクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    ''' 共通クラスでメソッドが作成され次第削除
    Private _FormPrm As LMFormData

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
        Dim frm As LMF210F = New LMF210F(Me)

        'インスタンスの生成
        Me._FormPrm = prm

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMFconG = New LMFControlG(sForm)

        'Validate共通クラスの設定
        Me._LMFconV = New LMFControlV(Me, sForm, Me._LMFconG)

        'Hnadler共通クラスの設定
        Me._LMFconH = New LMFControlH(sForm, MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMF210V(Me, frm, Me._LMFconV, Me._LMFconG)

        'Gamenクラスの設定
        Me._G = New LMF210G(Me, frm)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        '初期値設定(スプレッド)
        Call Me._G.SetInitValue()

        'スプレッドの検索行に初期値を設定する
        Call Me._G.InitSpread(prmDs)

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G007")

        '営業所を自営業所に設定
        Me._G.SetNrsBrCd()

        'フォーカスの設定
        Call Me._G.SetFoucus()

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
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub CloseForm(ByVal frm As LMF210F)

        If Me._FormPrm.ParamDataSet.Tables(LMF210C.TABLE_NM_OUT) Is Nothing = True _
            OrElse Me._FormPrm.ParamDataSet.Tables(LMF210C.TABLE_NM_OUT).Rows.Count = 0 Then

            'リターンコードの設定
            Me._FormPrm.ReturnFlg = False
        Else

            'リターンコードの設定
            Me._FormPrm.ReturnFlg = True

        End If

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMF210F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        Call Me._LMFconH.NextFocusedControl(frm, eventFlg)


    End Sub


#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMF210F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey9Press")

        Call Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMF210F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        '選択処理()
        Call Me.RowOkSelect(frm)

        If Me.FormPrm.ReturnFlg = True Then
            frm.Close()
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMF210F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMF210F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMF210F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Logger.StartLog(Me.GetType.Name, "RowSelection")

        '選択処理
        Call Me.RowSelection(frm, e)

        '選択行の取得に成功時自フォームを閉じる
        
        If Me.FormPrm.ReturnFlg = True Then
            frm.Close()
        End If

        Logger.EndLog(Me.GetType.Name, "RowSelection")

    End Sub
    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub KeyDown(ByVal frm As LMF210F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectListEvent(ByVal frm As LMF210F) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        '検索条件格納
        Dim ds As DataSet = Me.SetDataSetInData(frm, New LMF210DS())

        '入力チェック()
        If Me._V.IsInputCheck() = False Then

            '終了処理
            Call Me.EndAction(frm)

            Return False

        End If


        '閾値の設定
        MyBase.SetLimitCount(Me._LMFconG.GetLimitData())


        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetMaxResultCount(mc)



        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '==========================
        'WSAクラス呼出
        '==========================
        
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMF210C.ACTION_ID_SELECT, ds)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            'Warningの場合
            If MyBase.IsWarningMessageExist() = True Then

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA(blf, LMF210C.ACTION_ID_SELECT, ds)

                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G008", New String() {MyBase.GetResultCount.ToString()})

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(False)

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '処理終了アクション
                    Call Me.EndAction(frm)
                    Return False

                End If

            Else

                'メッセージエリアの設定(0件エラー)
                MyBase.ShowMessage(frm)

            End If

        Else

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {MyBase.GetResultCount.ToString()})

        End If

        '値の再設定
        Call Me._G.SetSpread(rtnDs)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Function

#Region "選択処理"

    ''' <summary>
    ''' 選択処理（ダブルクリック時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMF210F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, e.Row)

    End Sub
    ''' <summary>
    ''' 選択処理（OKボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowOkSelect(ByVal frm As LMF210F)

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMF210G.sprDetailDef.DEF.ColNo
        '共通クラスでメソッドが作成され次第下記項目をコメント化解除
        'Dim arr As ArrayList = Me._LMFconV.SprSelectCount(frm.sprDetail, defNo)
        Dim arr As ArrayList = Me.SprSelectCount(frm.sprDetail, defNo)


        '単一選択チェック
        If Me._LMFconV.IsSelectOneChk(arr.Count) = False Then
            Exit Sub
        End If

        '未選択チェック
        If Me._LMFconV.IsSelectChk(arr.Count) = False Then
            Exit Sub
        End If

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, Convert.ToInt32(arr(0)))

    End Sub
#End Region



#Region "共通クラスでメソッドが作成され次第そちらを使用(画面モードを設定・取得)"

    ''' <summary>
    ''' 画面のモードを設定・取得します
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property FormPrm() As LMFormData
        Get
            Return _FormPrm
        End Get
        Set(ByVal value As LMFormData)
            _FormPrm = value
        End Set
    End Property
#End Region


#Region "データセット"
    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMF210F, ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr("TRIP_DATE_FROM") = .imdFrom.TextValue
            dr("TRIP_DATE_TO") = .imdTo.TextValue
            Dim wtFrom As String = String.Empty
            If String.IsNullOrEmpty(.numLoadWtZanFrom.TextValue) = False Then
                wtFrom = .numLoadWtZanFrom.Value.ToString()
            End If
            dr("LOAD_WT_FROM") = wtFrom

            Dim wtTo As String = String.Empty
            If String.IsNullOrEmpty(.numLoadWtZanTo.TextValue) = False Then
                wtTo = .numLoadWtZanTo.Value.ToString()
            End If
            dr("LOAD_WT_To") = wtTo

            Dim ondoFrom As String = String.Empty
            If String.IsNullOrEmpty(.numOnkanFrom.TextValue) = False Then
                ondoFrom = .numOnkanFrom.Value.ToString()
            End If
            dr("UNSO_ONDO_FROM") = ondoFrom

            Dim ondoTo As String = String.Empty
            If String.IsNullOrEmpty(.numOnkanTo.TextValue) = False Then
                ondoTo = .numOnkanTo.Value.ToString()
            End If
            dr("UNSO_ONDO_TO") = ondoTo

            dr("TRIP_NO") = Me.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF210G.sprDetailDef.TRIP_NO.ColNo))
            dr("UNSOCO_NM") = Me.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF210G.sprDetailDef.UNSO_NM.ColNo))
            dr("CAR_NO") = Me.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF210G.sprDetailDef.CAR_NO.ColNo))
            dr("JSHA_KB") = Me.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF210G.sprDetailDef.JSHA_NM.ColNo))
            dr("DRIVER_NM") = Me.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF210G.sprDetailDef.DRIVER_NM.ColNo))
            dr("BIN_KB") = Me.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF210G.sprDetailDef.BIN.ColNo))
            dr("REMARK") = Me.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF210G.sprDetailDef.REMARK.ColNo))


            dt.Rows.Add(dr)

        End With

        Return ds

    End Function

#End Region
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================


    #End Region 'イベント振分け

#Region "スプレッド"

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="defNo"></param>
    ''' <returns></returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Function SprSelectCount(ByVal spr As LMSpread, ByVal defNo As Integer) As ArrayList

        With spr.ActiveSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If Me.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

            Return list

        End With

    End Function

    ''' <summary>
    ''' 値取得
    ''' </summary>
    ''' <param name="aCell"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetCellValue(ByVal aCell As Cell) As String

        GetCellValue = String.Empty

        If TypeOf aCell.Editor Is CellType.ComboBoxCellType Then

            'コンボボックスの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.CheckBoxCellType Then

            'チェックボックスの場合、Booleanの値をStringに変換
            If aCell.Text.Equals("True") = True Then
                GetCellValue = LMConst.FLG.ON
            ElseIf aCell.Text.Equals("False") = True Then
                GetCellValue = LMConst.FLG.OFF
            Else
                GetCellValue = aCell.Text
            End If

        ElseIf TypeOf aCell.Editor Is CellType.NumberCellType Then

            'ナンバーの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            Else
                GetCellValue = 0.ToString()
            End If

        Else

            'テキストの場合、Trimした値を返却
            GetCellValue = aCell.Text.Trim()

        End If

        Return GetCellValue

    End Function

#Region "共通クラスでメソッドが作成されたら共通クラスを使用(返却パラメータ系)"

    ''' <summary>
    ''' 選択行をフォームパラメータに戻す
    ''' </summary>
    ''' <param name="rowIdx">選択行インデクス</param>
    ''' <remarks></remarks>
    Private Sub SelectionRowToFrm(ByVal frm As LMF210F, ByVal rowIdx As Integer)

        If 0 < rowIdx Then

            Dim ds As DataSet = New LMF210DS()
            Dim dt As DataTable = ds.Tables(LMF210C.TABLE_NM_OUT)
            Dim dr As DataRow = dt.NewRow()

            With frm.sprDetail.ActiveSheet

                dr.Item("TRIP_NO") = Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF210G.sprDetailDef.TRIP_NO.ColNo))
                dr.Item("TRIP_DATE") = Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF210G.sprDetailDef.TRIP_DATE.ColNo))
                dr.Item("UNSOCO_NM") = Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF210G.sprDetailDef.UNSO_NM.ColNo))
                dr.Item("CAR_NO") = Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF210G.sprDetailDef.CAR_NO.ColNo))
                dr.Item("BIN") = Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF210G.sprDetailDef.BIN.ColNo))
                dr.Item("JSHA_NM") = Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF210G.sprDetailDef.JSHA_NM.ColNo))
                dr.Item("REMARK") = Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF210G.sprDetailDef.REMARK.ColNo))
                dr.Item("DRIVER_NM") = Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF210G.sprDetailDef.DRIVER_NM.ColNo))
                dr.Item("UNSO_PKG_NB") = Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF210G.sprDetailDef.UNSO_PKG_NB.ColNo))
                dr.Item("UNSO_WT") = Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF210G.sprDetailDef.UNSO_WT.ColNo))
                dr.Item("LOAD_WT") = Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF210G.sprDetailDef.LOAD_WT.ColNo))
                dr.Item("UNSO_ONDO") = Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF210G.sprDetailDef.ON_KAN.ColNo))
                dr.Item("PAY_UNCHIN") = Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF210G.sprDetailDef.PAY_AMT.ColNo))

            End With

            dt.Rows.Add(dr)

            Me._FormPrm.ParamDataSet = ds
            Me._FormPrm.ReturnFlg = True

        End If

    End Sub

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
#End Region

#End Region

#Region "ユーティリティ(共通クラスでメソッドが作成されたら共通クラスを使用"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMF210F)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(frm)

    End Sub


    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub EndAction(ByVal frm As LMF210F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "メッセージ設定(共通クラスでメソッドができたら共通クラスを使用)"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMF210F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetGMessage(frm)

        End If

    End Sub
    ''' <summary>
    ''' ガイダンスメッセージを設定する
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetGMessage(ByVal frm As LMF210F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub
#End Region


#End Region 'Method

End Class
