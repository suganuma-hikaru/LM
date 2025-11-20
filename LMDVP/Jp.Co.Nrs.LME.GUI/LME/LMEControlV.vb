' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME        
'  プログラムID     :  LMEControlV LME編集画面 共通処理
'  作  成  者       :  nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMEControlValidateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMEControlV
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As Form)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

    End Sub

#End Region

#Region "Method"

#Region "共通入力チェック"


    ''' <summary>
    ''' 未選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectChk(ByVal chkCnt As Integer) As Boolean

        'チェック件数が0件
        If chkCnt = 0 Then

            Return Me.SetErrMessage("E009")

        End If

        Return True

    End Function

    ''' <summary>
    ''' 単一行選択チェック
    ''' </summary>
    ''' <param name="chkCnt">チェック行カウント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectOneChk(ByVal chkCnt As Integer) As Boolean

        If 1 < chkCnt Then
            Return Me.SetErrMessage("E008")
        End If

        Return True

    End Function

    ''' <summary>
    ''' MAXSEQチェック
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="colNm">列名</param>
    ''' <param name="rowCnt">行数</param>
    ''' <param name="replaceStr">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsMaxSeqChk(ByVal dr As DataRow _
                                   , ByVal colNm As String _
                                   , ByVal rowCnt As Integer _
                                   , ByVal replaceStr As String _
                                   ) As Boolean

        If 999 < rowCnt + Convert.ToInt32(dr.Item(colNm).ToString()) Then
            MyBase.ShowMessage("E062", New String() {replaceStr})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 計算結果チェック
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="minData">最小値</param>
    ''' <param name="maxData">最大値</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsCalcOver(ByVal value As String _
                               , ByVal minData As String _
                               , ByVal maxData As String _
                               ) As Boolean

        Dim chkData As Decimal = Convert.ToDecimal(value)
        Dim rtnResult As Boolean = Me.IsOverMaxChkeck(chkData, Convert.ToDecimal(maxData))

        rtnResult = rtnResult AndAlso Me.IsOverMinChkeck(chkData, Convert.ToDecimal(minData))

        Return rtnResult

    End Function

    ''' <summary>
    ''' 最大限界値チェック
    ''' </summary>
    ''' <param name="value">判定値</param>
    ''' <param name="maxLength">最大</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsOverMaxChkeck(ByVal value As Decimal, ByVal maxLength As Decimal) As Boolean

        If maxLength < value Then
            Return False
        End If
        Return True

    End Function

    ''' <summary>
    ''' 最小限界値チェック
    ''' </summary>
    ''' <param name="value">判定値</param>
    ''' <param name="bottom">最小</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsOverMinChkeck(ByVal value As Decimal, ByVal bottom As Decimal) As Boolean

        If value < bottom Then
            Return False
        End If
        Return True

    End Function

#End Region

#Region "エラーメッセージ設定"

    ''' <summary>
    ''' メッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetErrMessage(ByVal id As String) As Boolean

        MyBase.ShowMessage(id)
        Return False

    End Function

    ''' <summary>
    ''' メッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetErrMessage(ByVal id As String, ByVal msg As String()) As Boolean

        MyBase.ShowMessage(id, msg)
        Return False

    End Function

    ''' <summary>
    ''' フォーカス位置エラーのメッセージ設定
    ''' </summary>
    ''' <param name="chk">判定フラグ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function SetFocusErrMessage(ByVal chk As Boolean) As Boolean

        If chk = False Then
            Return Me.SetFocusErrMessage()
        End If

        Return True

    End Function


    ''' <summary>
    ''' フォーカス位置エラーのメッセージ設定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetFocusErrMessage() As Boolean

        Return Me.SetErrMessage("G005")

    End Function

    ''' <summary>
    ''' マスタ存在チェックエラーのメッセージ設定
    ''' </summary>
    ''' <param name="value1">置換文字1</param>
    ''' <param name="value2">置換文字2</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetMstErrMessage(ByVal value1 As String, ByVal value2 As String) As Boolean

        Return Me.SetErrMessage("E079", New String() {value1, value2})

    End Function

#End Region

#Region "エラーコントロール設定"

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <param name="tab">タブ　初期値 = Nothing</param>
    ''' <param name="tabPage">タブページ　初期値 = Nothing</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal ctl As Control _
                               , Optional ByVal tab As Jp.Co.Nrs.LM.GUI.Win.LMTab = Nothing _
                               , Optional ByVal tabPage As System.Windows.Forms.TabPage = Nothing)

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor()

        If TypeOf ctl Is Win.InputMan.LMImCombo = True Then

            DirectCast(ctl, Win.InputMan.LMImCombo).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            DirectCast(ctl, Win.InputMan.LMComboKubun).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMComboNrsBr = True Then

            DirectCast(ctl, Win.InputMan.LMComboNrsBr).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMComboSoko = True Then

            DirectCast(ctl, Win.InputMan.LMComboSoko).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            DirectCast(ctl, Win.InputMan.LMImNumber).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImDate = True Then

            DirectCast(ctl, Win.InputMan.LMImDate).BackColorDef = errorColor

        End If

        If tab Is Nothing = False Then
            tab.SelectedTab = tabPage
        End If

        ctl.Focus()
        ctl.Select()

    End Sub

    ''' <summary>
    ''' エラーコントロール設定(明細用)
    ''' </summary>
    ''' <param name="spr">スプレッドシート</param>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="colNo">列番号</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal spr As Spread.LMSpread, ByVal rowNo As Integer, ByVal colNo As Integer)

        With spr.ActiveSheet

            Me.MyForm.ActiveControl = spr
            spr.Focus()
            .SetActiveCell(rowNo, colNo)
            .Cells(rowNo, colNo).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()

        End With

    End Sub

    '''' <summary>
    '''' フォーカス位置チェック
    '''' </summary>
    '''' <param name="actionType">ファンクションキー押下/Enter押下</param>
    '''' <param name="ctl">ポップアップ起動コントロール配列</param>
    '''' <param name="msg">置換メッセージ配列</param>
    '''' <param name="focusCtl">アクティブコントロール配列</param>
    '''' <param name="clearCtl">クリアコントロール配列</param>
    '''' <returns>True:エラーなし、False:エラー有り</returns>
    '''' <remarks></remarks>
    'Friend Function IsFocusChk(ByVal actionType As String _
    '                               , ByVal ctl As Win.InputMan.LMImTextBox() _
    '                               , ByVal msg As String() _
    '                               , ByVal focusCtl As Control _
    '                               , Optional ByVal clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing _
    '                               ) As Boolean

    '    'メインのコントロールでの判定のみ
    '    'フォーカス位置が参照可能チェック
    '    Dim max As Integer = -1

    '    '何も無い場合、エラー
    '    If ctl Is Nothing = False Then
    '        max = ctl.Length - 1
    '    End If

    '    Dim rtnResult As Boolean = Me.IsFoucsReadOnlyChk(ctl, focusCtl)

    '    '名称設定コントロールをクリアする
    '    If rtnResult = True Then
    '        Call Me.ClearControl(ctl, clearCtl)
    '    End If

    '    Select Case actionType

    '        Case LMMControlC.MASTEROPEN

    '            rtnResult = Me.SetFocusErrMessage(rtnResult)

    '        Case LMMControlC.ENTER

    '            rtnResult = rtnResult AndAlso Me.IsFoucsValueChk(ctl, max)

    '    End Select

    '    '禁止文字チェック
    '    rtnResult = rtnResult AndAlso Me.IsFoucsForbiddenWordsChk(ctl, msg, max)

    '    Return rtnResult

    'End Function

    '''' <summary>
    ''''  フォーカス位置が参照可能判定
    '''' </summary>
    '''' <param name="focusCtl">アクティブコントロール</param>
    '''' <returns>True:エラーなし,OK False:エラーあり</returns>
    '''' <remarks>メインのコントロールでの判定</remarks>
    'Private Function IsFoucsReadOnlyChk(ByVal ctl As Win.InputMan.LMImTextBox(), ByVal focusCtl As Control) As Boolean

    '    'マスタ参照対象コントロールでない場合、エラー
    '    If ctl Is Nothing = True Then
    '        Return False
    '    End If

    '    'ロックされている場合、エラー
    '    If DirectCast(focusCtl, Win.InputMan.LMImTextBox).ReadOnly = True Then
    '        Return False
    '    End If

    '    Return True

    'End Function

    '''' <summary>
    '''' フォーカス位置の禁止文字チェック
    '''' </summary>
    '''' <param name="ctl">コントロール配列</param>
    '''' <param name="msg">置換文字配列</param>
    '''' <param name="max">ループ変数</param>
    '''' <returns>True:エラーなし,OK False:エラーあり</returns>
    '''' <remarks></remarks>
    'Private Function IsFoucsForbiddenWordsChk(ByVal ctl As Win.InputMan.LMImTextBox(), ByVal msg As String(), ByVal max As Integer) As Boolean

    '    '全てのコントロールの値に対して禁止文字チェック
    '    For i As Integer = 0 To max

    '        If Me.IsFoucsForbiddenWordsChk(ctl(i), msg(i)) = False Then
    '            Return False
    '        End If

    '    Next

    '    Return True

    'End Function

    '''' <summary>
    '''' 値が入っているかを判定
    '''' </summary>
    '''' <param name="ctl">コントロール配列</param>
    '''' <param name="max">ループ変数</param>
    '''' <returns>True:エラーなし,OK False:エラーあり</returns>
    '''' <remarks></remarks>
    'Private Function IsFoucsValueChk(ByVal ctl As Win.InputMan.LMImTextBox() _
    '                                 , ByVal max As Integer) As Boolean

    '    '全てに値がない場合、スルー
    '    For i As Integer = 0 To max
    '        If Me.IsFoucsValueChk(ctl(i)) = True Then
    '            Return True
    '        End If
    '    Next

    '    Return False

    'End Function

    '''' <summary>
    ''''クリア処理を行う
    '''' </summary>
    '''' <param name="ctl">クリア対象項目</param>
    '''' <remarks></remarks>
    'Private Sub ClearControl(ByVal ctl As Win.InputMan.LMImTextBox(), _
    '                         ByVal clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl())

    '    'クリアコントロール未設定の場合、処理終了
    '    If clearCtl Is Nothing Then
    '        Exit Sub
    '    End If

    '    '対象コントロールに値が入っている場合、処理終了
    '    If IsFoucsValueChk(ctl, ctl.Length - 1) = True Then
    '        Exit Sub
    '    End If

    '    Dim clearMax As Integer = clearCtl.Length - 1

    '    'エディット系コントロールのクリア処理を行う
    '    For index As Integer = 0 To clearMax

    '        'コントロール別にクリア処理を行う
    '        If TypeOf clearCtl(index) Is Win.InputMan.LMImCombo = True Then

    '            DirectCast(clearCtl(index), Win.InputMan.LMImCombo).SelectedValue = String.Empty

    '        ElseIf TypeOf clearCtl(index) Is Win.InputMan.LMComboKubun = True Then

    '            DirectCast(clearCtl(index), Win.InputMan.LMComboKubun).SelectedValue = String.Empty

    '        ElseIf TypeOf clearCtl(index) Is Win.InputMan.LMImNumber = True Then

    '            DirectCast(clearCtl(index), Win.InputMan.LMImNumber).Value = 0

    '        Else

    '            clearCtl(index).TextValue = String.Empty

    '        End If

    '    Next

    'End Sub

#End Region

#Region "スプレッド"

    ''' <summary>
    ''' セルから値を取得
    ''' </summary>
    ''' <param name="aCell">セル</param>
    ''' <returns>取得した値</returns>
    ''' <remarks></remarks>
    Friend Function GetCellValue(ByVal aCell As Cell) As String

        GetCellValue = String.Empty

        If TypeOf aCell.Editor Is CellType.ComboBoxCellType Then

            'コンボボックスの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.CheckBoxCellType Then

            'チェックボックスの場合、0 or 1を返却
            If Me.changeBooleanCheckBox(aCell.Text) = True Then
                GetCellValue = 1.ToString()
            Else
                GetCellValue = 0.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.NumberCellType Then

            'ナンバーの場合、Value値を返却
            If aCell.Value Is Nothing = False _
                AndAlso String.IsNullOrEmpty(aCell.Value.ToString()) = False Then
                GetCellValue = aCell.Value.ToString()
            Else
                GetCellValue = 0.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.DateTimeCellType Then

            '日付の場合、Value値を yyyyMMdd に変換して返却
            If aCell.Value Is Nothing = False AndAlso String.IsNullOrEmpty(aCell.Value.ToString()) = False Then
                GetCellValue = Convert.ToDateTime(aCell.Value).ToString("yyyyMMdd")
            End If

        Else
            'テキストの場合、Trimした値を返却
            GetCellValue = aCell.Text.Trim()

        End If

        Return GetCellValue

    End Function

    ''' <summary>
    ''' チェックボックスの値をString型からBoolean型に変換する
    ''' </summary>
    ''' <param name="textValue">obj.text(0:チェック無し,1:チェック有り)</param>
    ''' <returns>True:チェック有り,False:チェック無し</returns>
    ''' <remarks></remarks>
    Private Function changeBooleanCheckBox(ByVal textValue As String) As Boolean

        '"1"の場合Trueを返却
        If (LMConst.FLG.ON.Equals(textValue) = True) _
            OrElse True.ToString().Equals(textValue) = True Then
            Return True
        End If

        '"0"の場合Falseを返却
        Return False

    End Function

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <param name="defNo">チェックボックスセルのカラム№</param>
    ''' <param name="sprDetail">対象スプレッド</param>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Overloads Function SprSelectList(ByVal defNo As Integer, ByRef sprDetail As Spread.LMSpreadSearch) As ArrayList

        With sprDetail.ActiveSheet

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
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <param name="defNo">チェックボックスセルのカラム№</param>
    ''' <param name="sprDetail">対象スプレッド</param>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Overloads Function SprSelectList2(ByVal defNo As Integer, ByVal sprDetail As Spread.LMSpread) As ArrayList

        With sprDetail.ActiveSheet

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
    ''' スプレッドの値をTrim
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">Max行</param>
    ''' <param name="colNo">Max列</param>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceSprTextvalue(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer, ByVal colNo As Integer)

        Dim aCell As Cell

        With spr

            For i As Integer = 0 To rowNo

                For j As Integer = 0 To colNo

                    aCell = .ActiveSheet.Cells(i, j)

                    If TypeOf aCell.Editor Is CellType.ComboBoxCellType = True _
                        OrElse TypeOf aCell.Editor Is CellType.CheckBoxCellType = True _
                        OrElse TypeOf aCell.Editor Is CellType.DateTimeCellType = True _
                        OrElse TypeOf aCell.Editor Is CellType.NumberCellType = True _
                        Then
                        '処理なし
                    Else
                        .SetCellValue(i, j, Me.GetCellValue(.ActiveSheet.Cells(i, j)))
                    End If

                Next

            Next

        End With

    End Sub

#End Region


#Region "スペース除去"

    ''' <summary>
    ''' スプレッドの値をTrim
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceSprTextvalue(ByVal spr As Win.Spread.LMSpread)

        With spr
            Dim rowMax As Integer = .ActiveSheet.Rows.Count - 1

            For i As Integer = 0 To rowMax

                Call Me.TrimSpaceSprTextvalue(spr, i)

            Next

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの値をTrim
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceSprTextvalue(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer)

        With spr

            Dim colMax As Integer = .ActiveSheet.Columns.Count - 1
            Dim aCell As Cell = Nothing

            For i As Integer = 0 To colMax

                aCell = .ActiveSheet.Cells(rowNo, i)

                If TypeOf aCell.Editor Is CellType.ComboBoxCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.CheckBoxCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.DateTimeCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.NumberCellType = True _
                    Then
                    '処理なし
                Else
                    .SetCellValue(rowNo, i, Me.GetCellValue(.ActiveSheet.Cells(rowNo, i)))
                End If

            Next

        End With

    End Sub

#End Region

#Region "キャッシュから値取得"

    ''' <summary>
    ''' 倉庫マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="whCd">倉庫コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectSokoListDataRow(ByVal whCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(Me.SelectSokoString(whCd))

    End Function

    ''' <summary>
    ''' 倉庫マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="whCd">倉庫コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectSokoString(ByVal whCd As String) As String

        SelectSokoString = String.Empty

        '削除フラグ
        SelectSokoString = String.Concat(SelectSokoString, " SYS_DEL_FLG = '0' ")

        '倉庫コード
        SelectSokoString = String.Concat(SelectSokoString, " AND ", "WH_CD = ", " '", whCd, "' ")

        Return SelectSokoString

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnCd"></param>
    ''' <param name="groupCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SelectKBNListDataRow(ByVal kbnCd As String, ByVal groupCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(Me.SelectKbnString(kbnCd, groupCd))

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnCd"></param>
    ''' <param name="groupCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectKbnString(ByVal kbnCd As String, ByVal groupCd As String) As String

        SelectKbnString = String.Empty

        '削除フラグ
        SelectKbnString = String.Concat(SelectKbnString, " SYS_DEL_FLG = '0' ")

        '区分コード
        SelectKbnString = String.Concat(SelectKbnString, " AND KBN_CD =  '", kbnCd, "' ")

        'グループコード
        SelectKbnString = String.Concat(SelectKbnString, " AND KBN_GROUP_CD =  '", groupCd, "' ")

        Return SelectKbnString

    End Function

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectCustListDataRow(ByVal custLCd As String _
                                          , Optional ByVal custMCd As String = "" _
                                          , Optional ByVal custSCd As String = "" _
                                          , Optional ByVal custSSCd As String = "" _
                                          ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(custLCd, custMCd, custSCd, custSSCd))

    End Function

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectCustString(ByVal custLCd As String _
                                     , ByVal custMCd As String _
                                     , ByVal custSCd As String _
                                     , ByVal custSSCd As String _
                                     ) As String

        SelectCustString = String.Empty

        '削除フラグ
        SelectCustString = String.Concat(SelectCustString, " SYS_DEL_FLG = '0' ")

        '荷主コード（大）
        SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")

        If String.IsNullOrEmpty(custMCd) = False Then

            '荷主コード（中）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_M = ", " '", custMCd, "' ")

        End If

        If String.IsNullOrEmpty(custSCd) = False Then

            '荷主コード（小）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_S = ", " '", custSCd, "' ")

        End If

        If String.IsNullOrEmpty(custSSCd) = False Then

            '荷主コード（極小）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_SS = ", " '", custSSCd, "' ")

        End If

        Return SelectCustString

    End Function

    ''' <summary>
    ''' 作業項目マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="sagyoCd">作業コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectSagyoListDataRow(ByVal sagyoCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(Me.SelectSagyoString(sagyoCd))

    End Function

    ''' <summary>
    ''' 作業項目マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="sagyoCd">作業コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoString(ByVal sagyoCd As String) As String

        SelectSagyoString = String.Empty

        '削除フラグ
        SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")

        '作業コード
        SelectSagyoString = String.Concat(SelectSagyoString, " AND ", "SAGYO_CD = ", " '", sagyoCd, "' ")

        Return SelectSagyoString

    End Function

    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="unsoCd">運送会社コード</param>
    ''' <param name="unsoShitenCd">運送会社支店コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnsocoListDataRow(ByVal unsoCd As String, Optional ByVal unsoShitenCd As String = "") As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(Me.SelectUnsocoString(unsoCd, unsoShitenCd))

    End Function

    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="unsoCd">運送会社コード</param>
    ''' <param name="unsoShitenCd">運送会社支店コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectUnsocoString(ByVal unsoCd As String, Optional ByVal unsoShitenCd As String = "") As String

        SelectUnsocoString = String.Empty

        '削除フラグ
        SelectUnsocoString = String.Concat(SelectUnsocoString, " SYS_DEL_FLG = '0' ")

        '運送会社コード
        SelectUnsocoString = String.Concat(SelectUnsocoString, " AND ", "UNSOCO_CD = ", " '", unsoCd, "' ")

        '運送会社コード
        If String.IsNullOrEmpty(unsoShitenCd) = False Then
            SelectUnsocoString = String.Concat(SelectUnsocoString, " AND ", "UNSOCO_BR_CD = ", " '", unsoShitenCd, "' ")
        End If

        Return SelectUnsocoString

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnchinTariffListDataRow(ByVal tariffCd As String _
                                                  , Optional ByVal tariffCdEda As String = "" _
                                                  , Optional ByVal startDate As String = "" _
                                                  ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF).Select(Me.SelectUnchinTariffString(tariffCd, tariffCdEda, startDate))

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinTariffString(ByVal tariffCd As String _
                                              , Optional ByVal tariffCdEda As String = "" _
                                              , Optional ByVal startDate As String = "" _
                                              ) As String

        SelectUnchinTariffString = String.Empty

        '削除フラグ
        SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " SYS_DEL_FLG = '0' ")

        '運賃タリフコード
        SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " AND ", "UNCHIN_TARIFF_CD = ", " '", tariffCd, "' ")

        '運賃タリフコード枝番
        If String.IsNullOrEmpty(tariffCdEda) = False Then
            SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " AND ", "UNCHIN_TARIFF_CD_EDA = ", " '", tariffCdEda, "' ")
        End If

        '適用開始日
        If String.IsNullOrEmpty(startDate) = False Then
            SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " AND ", "STR_DATE = ", " '", startDate, "' ")
        End If

        Return SelectUnchinTariffString

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="tariffCd">横持ちタリフコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectYokoTariffListDataRow(ByVal brCd As String, ByVal tariffCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.YOKO_TARIFF_HD).Select(Me.SelectYokoTariffString(tariffCd, brCd))

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="tariffCd">横持ちタリフコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectYokoTariffString(ByVal brCd As String, ByVal tariffCd As String) As String

        SelectYokoTariffString = String.Empty

        '削除フラグ
        SelectYokoTariffString = String.Concat(SelectYokoTariffString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectYokoTariffString = String.Concat(SelectYokoTariffString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '横持ちタリフコード
        SelectYokoTariffString = String.Concat(SelectYokoTariffString, " AND ", "YOKO_TARIFF_CD = ", " '", tariffCd, "' ")

        Return SelectYokoTariffString

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="setCd">セットマスタコード</param>
    ''' <param name="tehaiKbn">タリフ分類区分</param>
    ''' <param name="tariffCd1">運賃タリフコード（屯キロ建）</param>
    ''' <param name="tariffCd2">運賃タリフコード（車建）</param>
    ''' <param name="yokoCd">横持ちタリフコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectTariffSetListDataRow(ByVal brCd As String _
                                               , ByVal custLCd As String _
                                               , ByVal custMCd As String _
                                               , ByVal setCd As String _
                                               , Optional ByVal tehaiKbn As String = "" _
                                               , Optional ByVal tariffCd1 As String = "" _
                                               , Optional ByVal tariffCd2 As String = "" _
                                               , Optional ByVal yokoCd As String = "" _
                                               ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET).Select(Me.SelectTariffSetString(brCd, custLCd, custMCd, setCd, tehaiKbn, tariffCd1, tariffCd2, yokoCd))

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="setCd">セットマスタコード</param>
    ''' <param name="tehaiKbn">タリフ分類区分</param>
    ''' <param name="tariffCd1">運賃タリフコード（屯キロ建）</param>
    ''' <param name="tariffCd2">運賃タリフコード（車建）</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Private Function SelectTariffSetString(ByVal brCd As String _
                                           , ByVal custLCd As String _
                                           , ByVal custMCd As String _
                                           , ByVal setCd As String _
                                           , ByVal tehaiKbn As String _
                                           , ByVal tariffCd1 As String _
                                           , ByVal tariffCd2 As String _
                                           , ByVal yokoCd As String _
                                           ) As String

        SelectTariffSetString = String.Empty

        '削除フラグ（キャッシュに削除フラグなし）
        SelectTariffSetString = String.Concat(SelectTariffSetString, " 1 = 1 ")

        '営業所コード
        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '荷主(大)コード
        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")

        '荷主(中)コード
        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "CUST_CD_M = ", " '", custLCd, "' ")

        'セットマスタコード
        If String.IsNullOrEmpty(setCd) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "SET_MST_CD = ", " '", setCd, "' ")

        End If

        'セット区分（キャッシュにセット区分なし）
        'If String.IsNullOrEmpty(tehaiKbn) = False Then

        '    SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "SET_KB = ", " '", tehaiKbn, "' ")

        'End If

        'タリフコード1
        If String.IsNullOrEmpty(tariffCd1) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "UNCHIN_TARIFF_CD1 = ", " '", tariffCd1, "' ")

        End If

        'タリフコード2
        If String.IsNullOrEmpty(tariffCd2) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "UNCHIN_TARIFF_CD2 = ", " '", tariffCd2, "' ")

        End If

        '横持ちタリフコード
        If String.IsNullOrEmpty(yokoCd) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "YOKO_TARIFF_CD = ", " '", yokoCd, "' ")

        End If

        Return SelectTariffSetString

    End Function

    ''' <summary>
    ''' 請求先タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="seqtoCd">請求先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectSeiqtoListDataRow(ByVal brCd As String, ByVal seqtoCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQTO).Select(Me.SelectSeiqtoString(brCd, seqtoCd))

    End Function

    ''' <summary>
    ''' 請求先タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="seqtoCd">請求先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqtoString(ByVal brCd As String, ByVal seqtoCd As String) As String

        SelectSeiqtoString = String.Empty

        '削除フラグ
        SelectSeiqtoString = String.Concat(SelectSeiqtoString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectSeiqtoString = String.Concat(SelectSeiqtoString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '請求先タリフコード
        SelectSeiqtoString = String.Concat(SelectSeiqtoString, " AND ", "SEIQTO_CD = ", " '", seqtoCd, "' ")

        Return SelectSeiqtoString

    End Function

    ''' <summary>
    ''' 届先タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectDestListDataRow(ByVal brCd As String, ByVal custLCd As String, ByVal destCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        '---↓
        'Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(Me.SelectDestString(brCd, custLCd, destCd))

        Dim destMstDs As MDestDS = New MDestDS
        Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
        destMstDr.Item("NRS_BR_CD") = brCd
        destMstDr.Item("CUST_CD_L") = custLCd
        destMstDr.Item("DEST_CD") = destCd
        destMstDr.Item("SYS_DEL_FLG") = "0"
        destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
        Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
        Return rtnDs.Tables(LMConst.CacheTBL.DEST).Select
        '---↑

    End Function

    ''' <summary>
    ''' 届先タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectDestString(ByVal brCd As String, ByVal custLCd As String, ByVal destCd As String) As String

        SelectDestString = String.Empty

        '削除フラグ
        SelectDestString = String.Concat(SelectDestString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectDestString = String.Concat(SelectDestString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '荷主(大)コード
        SelectDestString = String.Concat(SelectDestString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")

        '届先コード
        SelectDestString = String.Concat(SelectDestString, " AND ", "DEST_CD = ", " '", destCd, "' ")

        Return SelectDestString

    End Function

    ''' <summary>
    ''' 棟・室マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="whCd">倉庫コード</param>
    ''' <param name="touNo">棟コード</param>
    ''' <param name="situNo">室コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectToShitsuListDataRow(ByVal brCd As String, ByVal whCd As String, ByVal touNo As String, ByVal situNo As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TOU_SITU_ZONE).Select(Me.SelectToShitsuString(brCd, whCd, touNo, situNo))

    End Function

    ''' <summary>
    ''' 棟・室マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="whCd">倉庫コード</param>
    ''' <param name="touNo">棟コード</param>
    ''' <param name="situNo">室コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectToShitsuString(ByVal brCd As String, ByVal whCd As String, ByVal touNo As String, ByVal situNo As String) As String

        SelectToShitsuString = String.Empty

        '削除フラグ
        SelectToShitsuString = String.Concat(SelectToShitsuString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectToShitsuString = String.Concat(SelectToShitsuString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '倉庫コード
        SelectToShitsuString = String.Concat(SelectToShitsuString, " AND ", "WH_CD = ", " '", whCd, "' ")

        '棟コード
        SelectToShitsuString = String.Concat(SelectToShitsuString, " AND ", "TOU_NO = ", " '", touNo, "' ")

        '室コード
        SelectToShitsuString = String.Concat(SelectToShitsuString, " AND ", "SITU_NO = ", " '", situNo, "' ")

        Return SelectToShitsuString

    End Function

    '''' <summary>
    '''' ZONEマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    '''' </summary>
    '''' <param name="brCd">営業所コード</param>
    '''' <param name="whCd">倉庫コード</param>
    '''' <param name="touNo">棟コード</param>
    '''' <param name="situNo">室コード</param>
    '''' <param name="zoneCd">ZONEコード</param>
    '''' <returns>データロウ配列</returns>
    '''' <remarks></remarks>
    'Friend Function SelectZoneListDataRow(ByVal brCd As String _
    '                                          , ByVal whCd As String _
    '                                          , ByVal touNo As String _
    '                                          , ByVal situNo As String _
    '                                          , ByVal zoneCd As String _
    '                                          ) As DataRow()

    '    'キャッシュテーブルからデータ抽出
    '    'Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.ZONE).Select(Me.SelectZoneString(brCd, whCd, touNo, situNo, zoneCd))

    'End Function

    ''' <summary>
    ''' ZONEマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="whCd">倉庫コード</param>
    ''' <param name="touNo">棟コード</param>
    ''' <param name="situNo">室コード</param>
    ''' <param name="zoneCd">ZONEコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectZoneString(ByVal brCd As String _
                                      , ByVal whCd As String _
                                      , ByVal touNo As String _
                                      , ByVal situNo As String _
                                      , ByVal zoneCd As String _
                                      ) As String

        SelectZoneString = String.Empty

        '削除フラグ
        SelectZoneString = String.Concat(SelectZoneString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectZoneString = String.Concat(SelectZoneString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '倉庫コード
        SelectZoneString = String.Concat(SelectZoneString, " AND ", "WH_CD = ", " '", whCd, "' ")

        '棟コード
        SelectZoneString = String.Concat(SelectZoneString, " AND ", "TOU_NO = ", " '", touNo, "' ")

        '室コード
        SelectZoneString = String.Concat(SelectZoneString, " AND ", "SITU_NO = ", " '", situNo, "' ")

        'ZONEコード
        SelectZoneString = String.Concat(SelectZoneString, " AND ", "ZONE_CD = ", " '", zoneCd, "' ")

        Return SelectZoneString

    End Function

    ''' <summary>
    ''' 商品マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="goodsCd">商品KEY</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectGoodsListDataRow(ByVal brCd As String _
                                              , ByVal goodsCd As String _
                                              ) As DataRow()

        'キャッシュテーブルからデータ抽出
        '---↓
        'Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(Me.SelectGoodsString(brCd, goodsCd))

        Dim goodsDs As MGoodsDS = New MGoodsDS
        Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
        goodsDr.Item("NRS_BR_CD") = brCd
        goodsDr.Item("GOODS_CD_NRS") = goodsCd
        goodsDr.Item("SYS_DEL_FLG") = "0"
        goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
        Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
        Return rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
        '---↑

    End Function

    ''' <summary>
    ''' 商品マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="goodsCd">商品KEY</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectGoodsString(ByVal brCd As String _
                                      , ByVal goodsCd As String _
                                      ) As String

        SelectGoodsString = String.Empty

        '削除フラグ
        SelectGoodsString = String.Concat(SelectGoodsString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectGoodsString = String.Concat(SelectGoodsString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '商品KEY
        SelectGoodsString = String.Concat(SelectGoodsString, " AND ", "GOODS_CD_NRS = ", " '", goodsCd, "' ")

        Return SelectGoodsString

    End Function


#End Region

#End Region

End Class
