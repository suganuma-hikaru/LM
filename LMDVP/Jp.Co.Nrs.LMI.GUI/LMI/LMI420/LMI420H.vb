' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI420H : 運賃比較
'  作  成  者       :  daikoku
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Microsoft.Office.Interop

''' <summary>
''' LMI420ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI420H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI420F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI420V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI420G

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
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索結果格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _OutDs As DataSet

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' 印刷種別フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintFlg As String

    ''' <summary>
    '''表示用データテーブル格納
    ''' </summary>
    ''' <remarks></remarks>
    Private _DispDt As DataTable

    ''' <summary>
    '''表示用データテーブル格納(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Private _DitailDispDt As DataTable

    ''' <summary>
    '''行削除データ格納テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private _RowDelDs As DataSet = New LMI420DS

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
        Me._Frm = New LMI420F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMIControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMIControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMIControlH(MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMI420V(Me, Me._Frm, Me._ControlV, Me._ControlG)

        'Gamenクラスの設定
        Me._G = New LMI420G(Me, Me._Frm, Me._ControlG, Me._V)

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitDetailSpread()

        'メッセージの表示
        Call MyBase.ShowMessage(Me._Frm, "G007")

        '画面の入力項目
        'Call Me._G.SetControlsStatus()

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
    ''' データ取込＆結果表示処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetFile()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI420C.EventShubetsu.GETFILE) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI420C.EventShubetsu.GETFILE) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble( _
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1")))

        Dim ds As DataSet = New LMI420DS

        'Excel取込処理
        Me.ExcelImport(ds, Me._Frm)

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        If ds.Tables(LMI420C.TABLE_NM_OUT).Rows.Count > 0 Then
            '検索成功時共通処理を行う
            Call Me.SuccessSelect(ds)

        Else
            '取得件数が0件の場合
            Call Me.FailureSelect(ds)

        End If

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default
    End Sub

#Region "Excel取込処理"
    ''' <summary>
    ''' Excel取込処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ExcelImport(ByRef ds As DataSet, ByVal frm As LMI420F)

        'Excel取込対象ファイルパス
        Dim filePath As String

        '確認メッセージ表示⇒キャンセル選択時：処理終了
        If Me.ShowMessage(frm, "C001", New String() {"運賃データ取込処理"}) = MsgBoxResult.Cancel Then Exit Sub

        frm.ImportFileDialog = New System.Windows.Forms.OpenFileDialog
        frm.ImportFileDialog.InitialDirectory = "C:\"    'GetExcelImportFileDirectory()
        'ファイルダイアログ表示
        If frm.ImportFileDialog.ShowDialog = DialogResult.OK Then
            'Excel取込対象ファイルパス取得
            filePath = frm.ImportFileDialog.FileName()

            'カーソルを砂時計にする
            Cursor.Current = Cursors.WaitCursor

            If fileReadAndDsWrite(ds, filePath) = True Then
                MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

                '==========================
                'WSAクラス呼出
                '==========================
                Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)

                ds = MyBase.CallWSA(blf, "SelectListData", ds)

                'ログ出力
                MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")


            End If

        End If

    End Sub
#End Region

#Region "ファイル処理"
    ''' <summary>
    ''' ファイル読み込み、データセットへ書き出す
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="filename"></param>
    ''' <remarks></remarks>
    Private Function fileReadAndDsWrite(ByRef ds As DataSet, ByVal filename As String) As Boolean

        Dim xlApp As Excel.Application = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing
        Dim xlCell As Excel.Range = Nothing

        xlApp = New Excel.Application()
        xlBooks = xlApp.Workbooks

        'EXCEL OPEN
        xlBook = xlBooks.Open(filename)

        'シート
        xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)     'とりあえず１番目のシートを設定

        '画面非表示
        xlApp.Visible = False

        '最大行を取得(rowNoKey列の最終入力行を取得)
        Dim rowNoMax As Integer = xlApp.ActiveCell.Row  '行の最大数
        Dim rowNoMin As Integer = 1                     '行の最小数
        Dim colNoMax As Integer = 100                   '列の最大数　
        Dim rowNoKey As Integer = 2                     'キーRowNo  
        Dim dr As DataRow
        Dim errflg As Integer = 0
        Dim Y As Integer = 0
        Dim iAmt As Integer = 0

        Dim ORDER_NO As String = String.Empty
        Dim JX_UNCHIN As String = String.Empty

        Dim sUSER_ID As String = Me.GetUserID.Trim

        'DS:JFB820DSEXCELクリア
        ds.Tables(LMI420C.TABLE_NM_IN).Clear()

        xlSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Select()

        '2次元配列を取得
        Dim arrData(,) As Object
        arrData = DirectCast(xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(xlApp.ActiveCell.Row, colNoMax)).Value, Object(,))

        '取込EXCELが既定の列数よりオーバー？
        If colNoMax < xlApp.ActiveCell.Column Then
            errflg = 1
            ''ワーニングメッセージ出力で処理確認
            ''メッセージの表示
            'Dim rtnResult As MsgBoxResult = Me.ShowMessage(frm, "W017", New String() {"削除"})
            'If Not rtnResult = MsgBoxResult.Ok Then  '「はい」選択
            '    Exit Function
            'End If
        Else

            '2次元→DSにセットする
            For j As Integer = rowNoMin To xlApp.ActiveCell.Row     'rowNoMax

                '2行目よりデータ設定（1行目はタイトル行）
                If j > 1 Then

                    'DS登録
                    'Key列が空の場合は空行とみなしデータセットに登録しない
                    If arrData(j, rowNoKey) Is Nothing Then
                        Continue For
                    Else
                        If String.IsNullOrEmpty(arrData(j, rowNoKey).ToString.Trim) Then
                            Continue For 
                        End If
                    End If

                    'レコード新規作成
                    dr = ds.Tables(LMI420C.TABLE_NM_IN).NewRow()

                    ''============= DS格納 ================================================
                    dr("USER_ID") = sUSER_ID
                    dr("ROW_NO") = j.ToString                       'エクセル行番号

                    ''設定項目初期化
                    dr("ORDER_NO") = String.Empty
                    dr("JX_UNCHIN") = "0".ToString

                    '入出荷指図番号
                    If arrData(j, 2) Is Nothing = False Then
                        ORDER_NO = arrData(j, 2).ToString.Trim          'ORDER_NO
                    End If

                    '通常運賃
                    If arrData(j, 24) Is Nothing = False Then
                        JX_UNCHIN = arrData(j, 24).ToString.Trim         'JX_UNCHIN
                    End If

                    dr("NRS_BR_CD") = Me._Frm.cmbNrsBr.SelectedValue    'NRS_BR_CD
                    dr("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue      'CUST_CD_L
                    'UPD 2016/12/26　大阪/塩浜とも先頭8桁だけで処理する
                    'If Me._Frm.cmbNrsBr.SelectedValue.Equals("20") Then
                    '    '大阪
                    '    dr("ORDER_NO") = String.Concat(Left(ORDER_NO.ToString.Trim, 8), "-", Mid(ORDER_NO.ToString.Trim, 9))    'ORDER_NO
                    'Else
                    '    '塩浜
                    '    'dr("ORDER_NO") = Left(ORDER_NO.ToString.Trim, 8)    'ORDER_NO
                    '    dr("ORDER_NO") = ORDER_NO.ToString.Trim            'ORDER_NO
                    'End If
                    dr("ORDER_NO") = Left(ORDER_NO.ToString.Trim, 8)

                    dr("JX_UNCHIN") = JX_UNCHIN.ToString                'JX_UNCHIN

                    'DSにレコード追加
                    ds.Tables(LMI420C.TABLE_NM_IN).Rows.Add(dr)


                End If
            Next
        End If

        'EXCEL CLOSE
        If xlCell IsNot Nothing Then
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCell)
            xlCell = Nothing
        End If

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
        xlSheet = Nothing

        'Excelを閉じる
        xlBook.Close(False)

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
        xlBook = Nothing

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
        xlBooks = Nothing

        xlApp.DisplayAlerts = False
        xlApp.Quit()
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
        xlApp = Nothing

        '------------------------
        'ds編集
        ''Dim outDr As DataRow() = ds.Tables(JFB820C.TABLE_NM_EXCEL).Select(Nothing, "KINOU_KBN,DENP_NO,TAX_CD,KMK_CD,SUB_KMK_CD")
        ''Dim max As Integer = outDr.Length - 1

        '--------------------------
        'EXCELファイルエラー？
        If errflg = 0 Then
            Return True
        Else
            Return False
        End If

    End Function

#End Region 'ファイル処理

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent()

        ''背景色クリア
        'Me._ControlG.SetBackColor(Me._Frm)

        ''カーソル位置の設定
        'Dim objNm As String = Me._Frm.ActiveControl.Name()

        ''権限チェック
        'If Me._V.IsAuthorityChk(LMI420C.EventShubetsu.MASTEROPEN) = False Then
        '    Exit Sub
        'End If

        ''カーソル位置チェック
        'If Me._V.IsFocusChk(objNm, LMI420C.EventShubetsu.MASTEROPEN) = False Then
        '    Exit Sub
        'End If

        ''処理開始アクション：１件時表示あり
        'Me._PopupSkipFlg = True
        'Me._ControlH.StartAction(Me._Frm)

        ''Pop起動処理
        'Call Me.ShowPopupControl(objNm, LMI420C.EventShubetsu.MASTEROPEN)

        ''処理終了アクション
        'Call Me._ControlH.EndAction(Me._Frm) '終了処理　

        ''メッセージエリアの設定
        'Call Me._V.SetBaseMsg()

    End Sub


    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal e As FormClosingEventArgs)

        ''ディスプレイモードが編集の場合処理終了
        'If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
        '    Exit Sub
        'End If

        ''メッセージ表示
        'Select Case MyBase.ShowMessage(Me._Frm, "W002")

        '    Case MsgBoxResult.Yes  '「はい」押下時

        '        If Me.SaveEvent() = False Then

        '            e.Cancel = True

        '        End If

        '    Case MsgBoxResult.Cancel                  '「キャンセル」押下時

        '        e.Cancel = True

        'End Select

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal ds As DataSet)


        '取得データをSPREADに表示
        Call Me._G.SetSprSearch(ds.Tables("LMI420OUT"))

        '取得件数設定
        Me._CntSelect = ds.Tables("LMI420OUT").Rows.Count.ToString()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G016", New String() {Me._CntSelect})


    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理(検索結果が0件の場合))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprSearch.CrearSpread()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()


        'フォーカスの設定
        Call Me._G.SetFoucus()

        '画面項目全クリア
        Call Me._G.ClearControl()

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G001")

    End Sub

 
#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="eventshubetsu">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal objNm As String, ByVal eventshubetsu As LMI420C.EventShubetsu) As Boolean

        With Me._Frm

            ''Select Case objNm
            ''    Case .txtCustCdL.Name

            ''        '荷主マスタ参照POP起動
            ''        Call Me.SetReturnCustPop(objNm, eventshubetsu)

            ''End Select

        End With

        Return True

    End Function

#Region "荷主マスタ"

    '' ''' <summary>
    '' ''' 荷主マスタPopの戻り値を設定
    '' ''' </summary>
    '' ''' <param name="objNm">フォーカス位置コントロール名</param>
    '' ''' <returns>True:選択有 False:選択無</returns>
    '' ''' <remarks></remarks>
    ''Private Function SetReturnCustPop(ByVal objNm As String, ByVal eventshubetsu As LMI420C.EventShubetsu) As Boolean

    ''    Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
    ''    Dim prm As LMFormData = Me.ShowCustPopup(ctl, eventshubetsu)
    ''    If prm.ReturnFlg = True Then

    ''        Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

    ''        With Me._Frm

    ''            .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
    ''            .lblCustNmL.TextValue = dr.Item("CUST_NM_L").ToString()

    ''        End With

    ''        Return True

    ''    End If

    ''    Return False

    ''End Function

    '' ''' <summary>
    '' ''' 荷主マスタ参照POP起動
    '' ''' </summary>
    '' ''' <param name="ctl">コントロール</param>
    '' ''' <remarks></remarks>
    ''Private Function ShowCustPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMI420C.EventShubetsu) As LMFormData

    ''    Dim ds As DataSet = New LMZ260DS()
    ''    Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
    ''    Dim dr As DataRow = dt.NewRow()
    ''    With dr
    ''        .Item("NRS_BR_CD") = Me._Frm.cmbNrsBr.SelectedValue.ToString()
    ''        If eventshubetsu = LMI420C.EventShubetsu.ENTER Then
    ''            .Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
    ''        End If
    ''        .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
    ''        .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF 'キャッシュ検索
    ''        .Item("HYOJI_KBN") = LMZControlC.HYOJI_M
    ''    End With
    ''    dt.Rows.Add(dr)

    ''    'Pop起動
    ''    Return Me._ControlH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    ''End Function

#End Region


#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetInData() As DataSet

        Dim ds As DataSet = New LMI420DS
        Dim dt As DataTable = ds.Tables(LMI420C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm

            dr.Item("NRS_BR_CD") = .cmbNrsBr.SelectedValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            'dr.Item("F_DATE") = .imdOutkaPlanDate_From.TextValue
            'dr.Item("T_DATE") = .imdOutkaPlanDate_To.TextValue
            'dr.Item("DELIVERY_NO") = Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(0, LMI420G.sprSearchDef.DELIVERY_NO.ColNo))
            'dr.Item("DEST_CD") = Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(0, LMI420G.sprSearchDef.DEST_CD.ColNo))
            'dr.Item("DEST_NM") = Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(0, LMI420G.sprSearchDef.DEST_NM.ColNo))

            ds.Tables(LMI420C.TABLE_NM_IN).Rows.Add(dr)

            Return ds

        End With

    End Function

    ''' <summary>
    ''' Excel出力処理用データセット作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetLMI940InDataSet() As DataSet

        Dim prmDs As DataSet = New LMI940DS()
        Dim row As DataRow = prmDs.Tables(LMI420C.TABLE_NM_EXCEL_IN).NewRow

        With Me._Frm

            row("NRS_BR_CD") = .cmbNrsBr.SelectedValue.ToString()
            row("CUST_CD_L") = .txtCustCdL.TextValue.ToString()
            row("CUST_CD_M") = "00"
            prmDs.Tables(LMI420C.TABLE_NM_EXCEL_IN).Rows.Add(row)

        End With

        Return prmDs

    End Function

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(データ取込)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMI420F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "GetFile")

        '検索処理
        Me.GetFile()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "GetFile")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMI420F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "")

        MyBase.Logger.EndLog(MyBase.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMI420F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "")

        '検索処理
        'Me.SelectListEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMI420F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        Me.MasterShowEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMI420F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "")


        MyBase.Logger.EndLog(MyBase.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI420F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI420F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓　========================


    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

End Class