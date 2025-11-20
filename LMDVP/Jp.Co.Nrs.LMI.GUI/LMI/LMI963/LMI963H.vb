' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI963H : 荷主自動振分画面(手動)（ハネウェル）
'  作  成  者       :  
' ==========================================================================
Option Strict Off
Option Explicit On

Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan

''' <summary>
''' LMI963ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI963H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI963V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI963G

    ''' <summary>
    ''' Sessionクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _S As LMZControlS

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

    ''' <summary>
    ''' ハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConH As LMIControlH

    ''' <summary>
    ''' Validateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConV As LMIControlV

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

    ''' <summary>
    ''' PGID
    ''' </summary>
    ''' <remarks></remarks>
    Private _Pgid As String


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

        'セッションクラスのインスタンス生成
        'Me = New LMZControlS()

        'パラメータオブジェクトを退避
        Me.FormPrm = prm

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        '画面間データを取得する
        Me.PrmDs = Me.FormPrm.ParamDataSet

        'フォームの作成
        Dim frm As LMI963F = New LMI963F(Me)

        Dim popL As LMFormPopL = DirectCast(frm, LMFormPopL)

        'Validate共通クラスの設定
        Me._LMIConV = New LMIControlV(Me, popL, Me._LMIConG)

        ''Hnadler共通クラスの設定
        'Me._LMIConH = New LMIControlH(popL, MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMIConG = New LMIControlG(frm)

        'Validateクラスの設定
        Me._V = New LMI963V(Me, frm, Me._LMIConV)

        'Gamenクラスの設定
        Me._G = New LMI963G(Me, frm)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)

        'ファンクションキーの設定
        Call Me.SetFunctionKey(frm, LMZControlC.F11Pattern.ptn2)

        'タブインデックスの設定
        Call Me.SetTabIndex(frm.sprDetail)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = Me.PrmDs.Tables(LMI963C.TABLE_NM_IN).Rows(0)

        'スプレッド・営業所コンボボックスの初期設定
        Call Me._G.InitSpread(prmdRow)

        '↓ データ取得の必要があればここにコーディングする。

        '検索フラグ判定
        If LMConst.FLG.ON.Equals(prmdRow("SEARCH_CS_FLG")) = True Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")
            '==========================
            'WSAクラス呼出
            '==========================
            Me.OutDs = Me.LoadCallWSAAction()

        Else
            '検索処理(キャッシュ)
            Dim ds As DataSet = New LMI960DS()
            ds.Tables(LMI963C.TABLE_NM_IN).ImportRow(prmdRow)
            Me.OutDs = Me.SelectCustOutListData(frm, ds)

        End If

        Dim outTbl As DataTable = Me.OutDs.Tables(LMI963C.TABLE_NM_OUT)
        Dim count As Integer = outTbl.Rows.Count

        If count = 1 AndAlso prm.SkipFlg <> True Then
            '初期検索結果＝1件 かつ 画面表示フラグがTrue以外の場合画面を開かない
            prm.ParamDataSet = Me.OutDs
            'リターンフラグを立てる
            prm.ReturnFlg = True
            LMFormNavigate.Revoke(Me)
            Exit Sub
        End If

        If LMConst.FLG.ON.Equals(prmdRow("SEARCH_CS_FLG")) = True Then
            count = Me.Cnt
        End If
        If Me.LoadMsgChk(frm, prmdRow, count) = True Then
            outTbl = Me.SetLoadData(frm, outTbl, LMI963C.TABLE_NM_OUT)
            '取得データをSPREADに表示
            Call Me._G.SetSpread(outTbl)
        End If

        '↑ データ取得の必要があればここにコーディングする。

        '呼び出し元画面情報を設定
        frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

        'フォームの表示
        frm.ShowDialog()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'フォーカスの設定
        ' Call Me._LMIConG.SetFoucus(frm.sprDetail)
        Call Me.SetFoucus(frm)

    End Sub

#End Region '初期処理

    ''' <summary>
    ''' タブインデックス取得
    ''' </summary>
    ''' <param name="spr"></param>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex(ByVal spr As LMSpread)

        spr.TabIndex = 0

    End Sub

    ''' <summary>
    ''' 初期処理ロードメッセージチェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="prmdrow"></param>
    ''' <param name="count"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function LoadMsgChk(ByVal frm As Form, ByVal prmdrow As DataRow, ByVal count As Integer) As Boolean

        '初期検索フラグ = 1の場合
        ' 且つ 0件以外 且つ (ワーニングがない または OKを選択)した場合は検索を行う
        If prmdrow("DEFAULT_SEARCH_FLG").Equals(LMConst.FLG.ON) = True _
            AndAlso 0 < count _
            AndAlso (MyBase.IsWarningMessageExist() = False _
                     OrElse Me.ShowWarningMsg(frm, MyBase.ShowMessage(frm)) = True) _
                     Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Convert.ToString(count)})
            Return True

        End If

        MyBase.ShowMessage(frm, "G007")
        Return False

    End Function

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal frm As LMI963F)

        frm.sprDetail.Sheets(0).SetActiveCell(0, LMI963C.SprColumnIndex.DEF)

    End Sub


    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CloseForm(ByVal frm As LMI963F)

        Call Me.CloseForm(LMI963C.TABLE_NM_OUT)

    End Sub

    '''' <summary>
    '''' 検索処理
    '''' </summary>
    '''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    '''' <remarks></remarks>
    'Private Sub SelectListEvent(ByVal frm As LMI963F)

    '    '処理開始アクション
    '    Call Me._LMIConH.StartAction(frm)

    '    '入力チェック
    '    If Me._V.IsInputChk() = False Then

    '        '終了処理
    '        Call Me._LMIConH.EndAction(frm)

    '        Exit Sub

    '    End If

    '    '検索
    '    Dim rtnDs As DataSet = Me.SelectList(frm)

    '    Dim outTbl As DataTable = rtnDs.Tables(LMI963C.TABLE_NM_OUT)
    '    Dim count As Integer = outTbl.Rows.Count

    '    '取得件数による処理変更
    '    If Me.CountRows(frm, frm.sprDetail, outTbl) = True AndAlso 0 < count Then

    '        'セッションクラスのOUTテーブルに設定
    '        Call Me.SetOutds(outTbl, count, LMI963C.TABLE_NM_OUT)
    '        '取得データをSPREADに表示
    '        Call Me._G.SetSpread(outTbl)
    '        'メッセージエリアの設定
    '        MyBase.ShowMessage(frm, "G008", New String() {Convert.ToString(count)})

    '    End If

    '    '終了処理
    '    Call Me._LMIConH.EndAction(frm)

    '    'ファンクションキーの設定
    '    Call Me.SetFunctionKey(frm, LMZControlC.F11Pattern.ptn2)

    '    Call Me.SetFoucus(frm)

    'End Sub

    ''' <summary>
    ''' セッションクラスのOUTテーブルに格納
    ''' </summary>
    ''' <param name="outTbl"></param>
    ''' <param name="count"></param>
    ''' <param name="tblNm"></param>
    ''' <remarks></remarks>
    Friend Sub SetOutds(ByVal outTbl As DataTable _
                            , ByVal count As Integer, ByVal tblNm As String)

        Dim sOutDt As DataTable = Me.OutDs.Tables(tblNm)
        sOutDt.Clear()
        Dim max As Integer = count - 1
        For i As Integer = 0 To max
            sOutDt.ImportRow(outTbl.Rows(i))
        Next

    End Sub

    ''' <summary>
    ''' 取得件数によるメッセージ表示処理
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function CountRows(ByVal frm As Form, ByVal spr As Spread.LMSpread, ByVal outTbl As DataTable) As Boolean

        If MyBase.IsMessageExist() = True Then

            Dim warningChk As Boolean = MyBase.IsWarningMessageExist()
            Dim msg As Integer = MyBase.ShowMessage(frm)

            If warningChk = True Then         'Warningの場合(500件以上)

                Return Me.ShowWarningMsg(frm, msg)

            Else 'Errorの場合(0件)

                'SPREAD(表示行)初期化
                spr.CrearSpread()

                Return False

            End If
        Else     '上記以外

            Return True

        End If

    End Function

    ''' <summary>
    ''' 初期処理検索
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetLoadData(ByVal frm As Form, ByVal dt As DataTable, ByVal tblNm As String) As DataTable

        If 0 < dt.Rows.Count Then
            Return dt
        End If

        Me.OutDs = Me.LoadCallWSAAction(True)
        Return Me.OutDs.Tables(tblNm)

    End Function

    ''' <summary>
    ''' 初期処理時サーバーアクセス処理
    ''' </summary>
    ''' <param name="setForceFlg"></param>
    ''' <param name="rn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function LoadCallWSAAction(Optional ByVal setForceFlg As Boolean = False _
                                       , Optional ByVal rn As Integer = -1
                                       ) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(setForceFlg)

        '閾値の設定
        MyBase.SetLimitCount(Me.SetLimit(rn))

        Dim rtnDs As DataSet = Me.ServerAccess(Me.PrmDs)

        'カウント保持
        Me.Cnt = MyBase.GetResultCount()

        Return rtnDs

    End Function

    '要望対応:1248 terakawa 2013.03.21 Start
    ''' <summary>
    ''' MyDataサーバーアクセス処理
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function LoadMyWSAAction(ByVal s As LMZControlS) As Boolean

        Dim rtnDs As DataSet = Me.MyServerAccess(Me.PrmDs)

        'カウント数が1以上の場合、True
        If MyBase.GetResultCount() > 0 Then
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' MyDataサーバーアクセス
    ''' </summary>
    ''' <param name="rtDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function MyServerAccess(ByVal rtDs As DataSet) As DataSet

        Return MyBase.CallWSA(String.Concat(Me._Pgid, "BLF"), "SelectMyData", rtDs)

    End Function

    ''' <summary>
    ''' 画面終了処理
    ''' </summary>
    ''' <param name="tblNm"></param>
    ''' <remarks></remarks>
    Friend Sub CloseForm(ByVal tblNm As String)

        If Me.FormPrm.ParamDataSet.Tables(tblNm) Is Nothing = True _
            OrElse Me.FormPrm.ParamDataSet.Tables(tblNm).Rows.Count = 0 Then

            'リターンコードの設定
            Me.FormPrm.ReturnFlg = False
        Else

            'リターンコードの設定
            Me.FormPrm.ReturnFlg = True

        End If

    End Sub

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキー(LMPopupL)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Overloads Sub SetFunctionKey(ByVal frm As LMFormPopL, ByVal ptnF11 As LMZControlC.F11Pattern)

        Call Me.SetFunctionKey(frm.FunctionKey, ptnF11)

    End Sub

    ''' <summary>
    ''' ファンクションキー(LMPopupLL)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ptnF11"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetFunctionKey(ByVal frm As LMFormPopLL, ByVal ptnF11 As LMZControlC.F11Pattern)

        Call Me.SetFunctionKey(frm.FunctionKey, ptnF11)

    End Sub

    ''' <summary>
    ''' ファンクションキー(LMPopupM)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ptnF11"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetFunctionKey(ByVal frm As LMFormPopM, ByVal ptnF11 As LMZControlC.F11Pattern)

        Call Me.SetFunctionKey(frm.FunctionKey, ptnF11)

    End Sub


    ''' <summary>
    ''' ファンクションキー設定
    ''' </summary>
    ''' <param name="fKey"></param>
    ''' <param name="ptnF11"></param>
    ''' <remarks></remarks>
    Private Overloads Sub SetFunctionKey(ByVal fKey As Win.InputMan.LMImFunctionKey, ByVal ptnF11 As LMZControlC.F11Pattern)

        Dim always As Boolean = True
        Dim f11Lock As Boolean = False
        Dim f11Str As String = String.Empty

        With fKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.POP_L)
            .Enabled = True

            Select Case ptnF11
                Case LMZControlC.F11Pattern.ptn2
                    f11Str = "Ｏ　Ｋ"
                    f11Lock = True
            End Select

            'ファンクションキー個別設定
            .F11ButtonName = f11Str
            .F12ButtonName = "キャンセル"

            'ファンクションキーの制御
            .F11ButtonEnabled = f11Lock
            .F12ButtonEnabled = always

            'タイトルテキスト・フォント設定の切り替え
            Call _V.TitleSwitch(fKey)

        End With

    End Sub

#End Region 'FunctionKey

    ''' <summary>
    ''' 選択処理（ダブルクリック時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMI963F, ByVal e As Integer)


#If False Then '商品マスタから荷主マスタ参照へ遷移した場合に、ダブルクリックで荷主を選択すると荷主CD(S,SS)が消える問題に対応 Changed 20151110  INOUE 
        '返却パラメータ設定
        'START YANAI 要望番号604
        'Call Me.SelectionRowToFrm(frm, e)
        Call Me.SelectionRowToFrm(frm, e, False)
        'END YANAI 要望番号604
#Else
        Dim rowLMI963IN As DataRow = Me.PrmDs.Tables(LMI963C.TABLE_NM_IN).Rows(0)

        Dim mode As Object = rowLMI963IN.Item("DC_ROW_SELECT_MODE")

        '荷主コードのクリア処理を無効化するか確認
        Dim disabledClearCustSAndSS As Boolean _
            = Not (mode Is Nothing) AndAlso Convert.ToInt16(mode) = LMI963C.DobleClickRowSelectMode.DISABLED_CLEAR_CUST_CD

        Call Me.SelectionRowToFrm(frm, e, disabledClearCustSAndSS)
#End If



    End Sub

    ''' <summary>
    ''' 選択処理（OKボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowOkSelect(ByVal frm As LMI963F)

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMI963G.sprDetailDef.DEF.ColNo
        Dim arr As ArrayList = Me.SprSelectCount(frm.sprDetail, defNo)


        '単一選択チェック
        If Me._LMIConV.IsSelectOneChk(arr.Count) = False Then
            Exit Sub
        End If

        '未選択チェック
        If Me._LMIConV.IsSelectChk(arr.Count) = False Then
            MyBase.ShowMessage(frm, "E009")
            Exit Sub
        End If

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, Convert.ToInt32(arr(0)), True)

        ''リターンコードの設定
        'Me.FormPrm.ReturnFlg = True

    End Sub

    'START YANAI 要望番号604
    ''' <summary>
    ''' 選択処理（大中設定ボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowLMSelect(ByVal frm As LMI963F)

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMI963G.sprDetailDef.DEF.ColNo
        Dim arr As ArrayList = Me.SprSelectCount(frm.sprDetail, defNo)

        '単一選択チェック
        If Me._LMIConV.IsSelectOneChk(arr.Count) = False Then
            Exit Sub
        End If

        '未選択チェック
        If Me._LMIConV.IsSelectChk(arr.Count) = False Then
            Exit Sub
        End If

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, Convert.ToInt32(arr(0)), False)

    End Sub
    'END YANAI 要望番号604

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

#End Region

#End Region 'イベント定義(一覧)

#Region "個別メソッド"

    ''' <summary>
    ''' 検索処理(データセット設定)
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectList(ByVal frm As LMI963F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMI960DS()
        Call SetDatasetCustInData(frm, ds)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = Me.PrmDs.Tables(LMI963C.TABLE_NM_IN).Rows(0)

        Dim rtnDs As DataSet = Nothing

        '検索フラグ判定
        If prmdRow("SEARCH_CS_FLG").Equals(LMConst.FLG.ON) = True Then


            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

            '==========================
            'WSAクラス呼出
            '==========================
            rtnDs = Me.CallWSAAction(frm, frm.sprDetail, ds)

        Else

            'キャッシュテーブルからデータ抽出
            rtnDs = Me.SelectCustOutListData(frm, ds)

        End If

        Return rtnDs

    End Function

    ''' <summary>
    ''' サーバーアクセス処理呼出
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rtDs"></param>
    ''' <param name="rn"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Friend Function CallWSAAction(ByVal frm As Form _
                                  , ByVal spr As Spread.LMSpread _
                                  , ByVal rtDs As DataSet _
                                  , Optional ByVal rn As Integer = -1
                                  ) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        MyBase.SetLimitCount(Me.SetLimit(rn))

        Dim rtnDs As DataSet = Me.ServerAccess(rtDs)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            If MyBase.IsWarningMessageExist() = True Then         'Warningの場合

                If Me.ShowWarningMsg(frm, MyBase.ShowMessage(frm)) = True Then

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = Me.ServerAccess(rtDs)

                End If

                Return rtnDs

            Else

                'SPREAD(表示行)初期化
                spr.CrearSpread()

                'メッセージエリアの設定
                MyBase.ShowMessage(frm)

                Return rtnDs

            End If

        End If

        Return rtnDs

    End Function

    ''' <summary>
    ''' サーバーアクセス
    ''' </summary>
    ''' <param name="rtDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ServerAccess(ByVal rtDs As DataSet) As DataSet

        Return MyBase.CallWSA(String.Concat(Me._Pgid, "BLF"), "SelectListData", rtDs)

    End Function

    ''' <summary>
    ''' ワーニングメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">メッセージの戻り値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowWarningMsg(ByVal frm As Form, ByVal msg As Integer) As Boolean

        'メッセージを表示し、戻り値により処理を分ける
        If MsgBoxResult.Ok <> msg Then '「いいえ」を選択
            MyBase.ShowMessage(frm, "G007")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">LMI963DS</param>
    ''' <returns>抽出された行列データセット</returns>
    ''' <remarks></remarks>
    Private Function SelectCustOutListData(ByVal frm As LMI963F, ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI963C.TABLE_NM_IN)

        'INTableの条件rowの格納
        Dim drow As DataRow = inTbl.Rows(0)

        Dim strSqlCust As New System.Text.StringBuilder()
        Dim whereStr As String = String.Empty
        Dim andstr As New System.Text.StringBuilder()

        '▼▼キャッシュテーブルへの抽出条件文作成▼▼
        '営業所
        andstr.Append(Me.SetWhereData(andstr, drow("NRS_BR_CD").ToString(), LMZControlC.ConditionPattern.equal, "NRS_BR_CD"))

        '荷主名(大)
        andstr.Append(Me.SetWhereData(andstr, drow("CUST_NM_L").ToString(), LMZControlC.ConditionPattern.all, "CUST_NM_L"))

        '荷主コード(大,中)
        andstr.Append(drow("NARROW_DOWN_LIST").ToString())

        '荷主コード(小)
        andstr.Append(Me.SetWhereData(andstr, drow("CUST_CD_S").ToString(), LMZControlC.ConditionPattern.pre, "CUST_CD_S"))

        '荷主コード(極小)
        andstr.Append(Me.SetWhereData(andstr, drow("CUST_CD_SS").ToString(), LMZControlC.ConditionPattern.pre, "CUST_CD_SS"))

        '▲▲キャッシュテーブルへの抽出条件文作成▲▲

        'キャッシュテーブルからデータ抽出
        Dim sort As String = "CUST_CD_L,CUST_CD_M,CUST_CD_S,CUST_CD_SS"

        Return Me.SelectListData(ds, LMI963C.TABLE_NM_OUT, LMConst.CacheTBL.CUST, andstr.ToString(), sort)

    End Function

    ''' <summary>
    ''' キャッシュ抽出条件設定
    ''' </summary>
    ''' <param name="andstr"></param>
    ''' <param name="whereStr">画面で入力された値</param>
    ''' <param name="ptn">条件</param>
    ''' <param name="colNm">項目名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetWhereData(ByVal andstr As System.Text.StringBuilder, ByVal whereStr As String, ByVal ptn As LMZControlC.ConditionPattern _
                                 , ByVal colNm As String) As String

        SetWhereData = String.Empty

        If String.IsNullOrEmpty(whereStr) = False Then
            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If


            Select Case ptn
                '完全一致
                Case LMZControlC.ConditionPattern.equal

                    SetWhereData = String.Concat(" ", colNm, " = '", whereStr, "'", vbNewLine)
                    '前方一致
                Case LMZControlC.ConditionPattern.pre

                    SetWhereData = String.Concat(" ", colNm, " LIKE '", Me.EscForLike(whereStr), "%'", vbNewLine)
                    '部分一致
                Case LMZControlC.ConditionPattern.all

                    SetWhereData = String.Concat(" ", colNm, " LIKE '", "%", Me.EscForLike(whereStr), "%'", vbNewLine)

                Case LMZControlC.ConditionPattern.more

                    SetWhereData = String.Concat(" ", colNm, " >= '", whereStr, "'", vbNewLine)

                Case LMZControlC.ConditionPattern.less

                    SetWhereData = String.Concat(" ", colNm, " <= '", whereStr, "'", vbNewLine)


            End Select

        End If

        Return SetWhereData

    End Function

    '''' <summary>
    '''' キャッシュ抽出条件設定
    '''' </summary>
    '''' <param name="andstr"></param>
    '''' <param name="whereStr">画面で入力された値</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Friend Function SetWhereData2(ByVal andstr As System.Text.StringBuilder, ByVal whereStr As String) As String

    '    SetWhereData2 = String.Empty
    '    Dim andCnt As Integer = 0

    '    If String.IsNullOrEmpty(whereStr) = False Then

    '        If andCnt = 0 Then
    '            andstr.Append(String.Concat(whereStr))
    '        Else
    '            andstr.Append(String.Concat(",", whereStr))
    '        End If

    '    End If

    '    Return SetWhereData2

    'End Function

    ''' <summary>
    ''' 検索条件のエスケープ
    ''' </summary>
    ''' <param name="value">条件文字</param>
    ''' <returns>エスケープ処理後の文字</returns>
    ''' <remarks></remarks>
    Private Function EscForLike(ByVal value As String) As String

        Return System.Text.RegularExpressions.Regex.Replace(value, "([\[\]*%])", "[$1]")

    End Function

    ''' <summary>
    ''' キャッシュからデータ取得
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="cacheTbl">キャッシュ</param>
    ''' <param name="whereStr">キャッシュ条件</param>
    ''' <param name="sort">ソート</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SelectListData(ByVal ds As DataSet, ByVal tblNm As String, ByVal cacheTbl As String, ByVal whereStr As String, ByVal sort As String) As DataSet

        Dim locationRows As DataRow() = MyBase.GetLMCachedDataTable(cacheTbl).Select(whereStr, sort)

        'メッセージ設定
        Dim cnt As Integer = locationRows.Length
        Dim lmt As Integer = Me.SetLimit()
        If cnt = 0 Then
            '0件
            MyBase.SetMessage("G001")
            '要望番号1552:(【SBS】マスタ参照画面の件数ワーニングの条件誤り) 2012/10/31 本明 Start
            'ElseIf 500 < cnt Then
        ElseIf lmt < cnt Then
            '要望番号1552:(【SBS】マスタ参照画面の件数ワーニングの条件誤り) 2012/10/31 本明 End
            'lmt件以上
            MyBase.SetMessage("W001", New String() {Convert.ToString(lmt)})
        End If

        If 0 < cnt Then
            '正常時 OUTテーブルへデータ格納
            Me.SetDataSetOutListData(ds, tblNm, locationRows)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 閾値設定
    ''' </summary>
    ''' <param name="rn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetLimit(Optional ByVal rn As Integer = -1) As Integer

        If rn <> -1 Then
            Return rn
        End If

        Return Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                             (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))

    End Function


    ''' <summary>
    ''' 選択行をフォームパラメータに戻す
    ''' </summary>
    ''' <param name="rowIdx">選択行インデクス</param>
    ''' <remarks></remarks>
    Private Sub SelectionRowToFrm(ByVal frm As LMI963F, ByVal rowIdx As Integer, ByVal custRtnFlg As Boolean)
        'END YANAI 要望番号604

        If 0 <= rowIdx Then

            'データテーブルから選択行を抽出
            Call Me.SetRtnParam(New LMI960DS(), LMI963C.TABLE_NM_OUT, rowIdx)

            If custRtnFlg = False Then
                '戻り値から荷主(小)、荷主(極小)をクリアする
                Me.FormPrm.ParamDataSet.Tables(LMI963C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_S") = String.Empty
                Me.FormPrm.ParamDataSet.Tables(LMI963C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_SS") = String.Empty
                Me.FormPrm.ParamDataSet.Tables(LMI963C.TABLE_NM_OUT).Rows(0).Item("CUST_NM_S") = String.Empty
                Me.FormPrm.ParamDataSet.Tables(LMI963C.TABLE_NM_OUT).Rows(0).Item("CUST_NM_SS") = String.Empty
            End If

        End If

    End Sub

    ''' <summary>
    ''' 返却パラメータセット
    ''' </summary>
    ''' <param name="rtnDs"></param>
    ''' <param name="tblNm"></param>
    ''' <param name="rowI"></param>
    ''' <remarks></remarks>
    Friend Sub SetRtnParam(ByVal rtnDs As DataSet, ByVal tblNm As String, ByVal rowI As Integer)

        Dim dRow As DataRow = Me.OutDs.Tables(tblNm).Rows(rowI)

        '返却パラメータへ選択行を格納
        Me.SetDataSetOutListData(rtnDs, tblNm, New DataRow() {dRow})

        '返却パラメータへDS設定
        Me.FormPrm.ParamDataSet = rtnDs

        '返却フラグを立てる
        Me.FormPrm.ReturnFlg = True

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

#Region "DataSet設定"


    ''' <summary>
    ''' データセット設定(一覧部データ)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetCustInData(ByVal frm As LMI963F, ByVal ds As DataSet)

        Dim drow As DataRow = ds.Tables(LMI963C.TABLE_NM_IN).NewRow

        drow("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue
        drow("ROW_INDEX") = frm.txtCmdGyo.TextValue
        drow("LOAD_NUMBER") = frm.txtLoadNumber.TextValue

        With frm.sprDetail.ActiveSheet
            drow("CUST_CD_L") = .Cells(0, LMI963G.sprDetailDef.CUST_CD_L.ColNo).Text.Trim()
            drow("CUST_CD_M") = .Cells(0, LMI963G.sprDetailDef.CUST_CD_M.ColNo).Text.Trim()
            drow("CUST_CD_S") = .Cells(0, LMI963G.sprDetailDef.CUST_CD_S.ColNo).Text.Trim()
            drow("CUST_CD_SS") = .Cells(0, LMI963G.sprDetailDef.CUST_CD_SS.ColNo).Text.Trim()
            drow("CUST_NM_L") = .Cells(0, LMI963G.sprDetailDef.CUST_NM_L.ColNo).Text.Trim()
            drow("CUST_NM_M") = .Cells(0, LMI963G.sprDetailDef.CUST_NM_M.ColNo).Text.Trim()
            drow("CUST_NM_S") = .Cells(0, LMI963G.sprDetailDef.CUST_NM_S.ColNo).Text.Trim
            drow("CUST_NM_SS") = .Cells(0, LMI963G.sprDetailDef.CUST_NM_SS.ColNo).Text.Trim

        End With

        ds.Tables(LMI963C.TABLE_NM_IN).Rows.Add(drow)

    End Sub


#End Region 'DataSet設定

#End Region '個別メソッド

#Region "イベント振分け"

    ''' <summary>
    ''' F11押下時処理呼び出し(OK処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMI963F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "OKbutton")

        '選択処理
        Call Me.RowOkSelect(frm)

        '選択行の取得に成功時自フォームを閉じる
        If Me.FormPrm.ReturnFlg = True Then
            frm.Close()
        End If

        Logger.EndLog(Me.GetType.Name, "OKbutton")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI963F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "CloseButton")

        '終了処理  
        frm.Close()

        Logger.EndLog(Me.GetType.Name, "CloseButton")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI963F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMI963F, ByVal e As Integer)

        Logger.StartLog(Me.GetType.Name, "RowSelection")

        '選択処理
        Call Me.RowSelection(frm, e)

        '選択行の取得に成功時自フォームを閉じる
        If Me.FormPrm.ReturnFlg = True Then
            frm.Close()
        End If

        Logger.EndLog(Me.GetType.Name, "RowSelection")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

#Region "Field"

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Dim _PrmDs As DataSet

    ''' <summary>
    ''' 最新の検索時取得DS
    ''' </summary>
    ''' <remarks></remarks>
    Dim _OutDs As DataSet

    ''' <summary>
    ''' パラメータのNFFormDataをクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FormPrm As LMFormData

    ''' <summary>
    ''' サーバーアクセス時の取得件数
    ''' </summary>
    ''' <remarks></remarks>
    Private _Cnt As Integer

#End Region 'Field

#Region "Constructor"

    Public Sub New()

    End Sub

#End Region

#Region "Property"

    ''' <summary>
    ''' データセットを設定・取得します
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property PrmDs() As DataSet
        Get
            Return _PrmDs
        End Get
        Set(ByVal value As DataSet)
            _PrmDs = value
        End Set
    End Property

    ''' <summary>
    ''' データセットを設定・取得します
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property OutDs() As DataSet
        Get
            Return _OutDs
        End Get
        Set(ByVal value As DataSet)
            _OutDs = value
        End Set
    End Property

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

    ''' <summary>
    ''' カウントを設定・取得します
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property Cnt() As Integer
        Get
            Return _Cnt
        End Get
        Set(ByVal value As Integer)
            _Cnt = value
        End Set
    End Property
#End Region

End Class
