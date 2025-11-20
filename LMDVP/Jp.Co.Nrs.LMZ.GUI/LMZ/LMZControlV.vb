' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ         : 共通
'  プログラムID     :  LMZControlV : LMZ共通チェッククラス
'  作  成  者       :  平山
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMBControlValidateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMZControlV
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ(LMFormPopL)
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef frm As LMFormPopL, ByRef handlerClass As LMBaseGUIHandler)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

    End Sub

    ''' <summary>
    ''' コンストラクタ(LMFormPopLL)
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef frm As LMFormPopLL, ByRef handlerClass As LMBaseGUIHandler)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

    End Sub


    ''' <summary>
    ''' コンストラクタ(LMFormPopM)
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef frm As LMFormPopM, ByRef handlerClass As LMBaseGUIHandler)

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
    ''' 選択行数チェック
    ''' </summary>
    ''' <param name="chkCnt">チェック行カウント</param>
    ''' <param name="empCnt">呼出元画面の空行カウント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectColumCntChk(ByVal chkCnt As Integer, ByVal empCnt As Integer) As Boolean

        If empCnt < chkCnt Then
            Return Me.SetErrMessage("E168", chkCnt.ToString(), empCnt.ToString())
        End If

        Return True

    End Function

#End Region

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
    ''' マスタ存在チェックエラーのメッセージ設定
    ''' </summary>
    ''' <param name="value1">置換文字1</param>
    ''' <param name="value2">置換文字2</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetMstErrMessage(ByVal value1 As String, ByVal value2 As String) As Boolean

        Return Me.SetErrMessage("E079", New String() {value1, value2})

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
    ''' メッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <param name="value1">置換文字1</param>
    ''' <param name="value2">置換文字2</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetErrMessage(ByVal id As String, ByVal value1 As String, ByVal value2 As String) As Boolean

        MyBase.ShowMessage(id, New String() {value1, value2})

    End Function


#End Region

#Region "スペース除去"

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

#Region "フォーカス位置チェック"


    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="ctl">ポップアップ起動コントロール配列</param>
    ''' <param name="msg">置換メッセージ配列</param>
    ''' <param name="focusCtl">アクティブコントロール配列</param>
    ''' <param name="clearCtl">クリアコントロール配列</param>
    ''' <returns>True:エラーなし、False:エラー有り</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal ctl As Win.InputMan.LMImTextBox() _
                              , ByVal msg As String() _
                              , ByVal focusCtl As Control _
                              , Optional ByVal clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing _
                              ) As Boolean

        'メインのコントロールでの判定のみ
        'フォーカス位置が参照可能チェック
        Dim max As Integer = -1

        '何も無い場合、エラー
        If ctl Is Nothing = False Then
            max = ctl.Length - 1
        End If

        Dim rtnResult As Boolean = Me.IsFoucsReadOnlyChk(ctl, focusCtl)

        '名称設定コントロールをクリアする
        If rtnResult = True Then
            Call Me.ClearControl(ctl, clearCtl)
        End If

        rtnResult = rtnResult AndAlso Me.IsFoucsValueChk(ctl, max)

        '禁止文字チェック
        rtnResult = rtnResult AndAlso Me.IsFoucsForbiddenWordsChk(ctl, msg, max)

        Return rtnResult

    End Function


    ''' <summary>
    '''  フォーカス位置が参照可能判定
    ''' </summary>
    ''' <param name="focusCtl">アクティブコントロール</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>メインのコントロールでの判定</remarks>
    Private Function IsFoucsReadOnlyChk(ByVal ctl As Win.InputMan.LMImTextBox(), ByVal focusCtl As Control) As Boolean

        'マスタ参照対象コントロールでない場合、エラー
        If ctl Is Nothing = True Then
            Return False
        End If

        'ロックされている場合、エラー
        If DirectCast(focusCtl, Win.InputMan.LMImTextBox).ReadOnly = True Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    '''クリア処理を行う
    ''' </summary>
    ''' <param name="ctl">クリア対象項目</param>
    ''' <remarks></remarks>
    Private Sub ClearControl(ByVal ctl As Win.InputMan.LMImTextBox(), _
                             ByVal clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl())

        'クリアコントロール未設定の場合、処理終了
        If clearCtl Is Nothing Then
            Exit Sub
        End If

        '対象コントロールに値が入っている場合、処理終了
        If IsFoucsValueChk(ctl, ctl.Length - 1) = True Then
            Exit Sub
        End If

        Dim clearMax As Integer = clearCtl.Length - 1

        'エディット系コントロールのクリア処理を行う
        For index As Integer = 0 To clearMax

            'コントロール別にクリア処理を行う
            If TypeOf clearCtl(index) Is Win.InputMan.LMImCombo = True Then

                DirectCast(clearCtl(index), Win.InputMan.LMImCombo).SelectedValue = String.Empty

            ElseIf TypeOf clearCtl(index) Is Win.InputMan.LMComboKubun = True Then

                DirectCast(clearCtl(index), Win.InputMan.LMComboKubun).SelectedValue = String.Empty

            ElseIf TypeOf clearCtl(index) Is Win.InputMan.LMImNumber = True Then

                DirectCast(clearCtl(index), Win.InputMan.LMImNumber).Value = 0

            Else

                clearCtl(index).TextValue = String.Empty

            End If

        Next

    End Sub


    ''' <summary>
    ''' 値が入っているかを判定
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFoucsValueChk(ByVal ctl As Win.InputMan.LMImTextBox) As Boolean

        If ctl Is Nothing = True Then
            Return True
        End If

        If String.IsNullOrEmpty(ctl.TextValue) = True Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 値が入っているかを判定
    ''' </summary>
    ''' <param name="ctl">コントロール配列</param>
    ''' <param name="max">ループ変数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFoucsValueChk(ByVal ctl As Win.InputMan.LMImTextBox() _
                                     , ByVal max As Integer) As Boolean

        '全てに値がない場合、スルー
        For i As Integer = 0 To max
            If Me.IsFoucsValueChk(ctl(i)) = True Then
                Return True
            End If
        Next

        Return False

    End Function

    ''' <summary>
    ''' フォーカス位置の禁止文字チェック
    ''' </summary>
    ''' <param name="ctl">コントロール配列</param>
    ''' <param name="msg">置換文字配列</param>
    ''' <param name="max">ループ変数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFoucsForbiddenWordsChk(ByVal ctl As Win.InputMan.LMImTextBox(), ByVal msg As String(), ByVal max As Integer) As Boolean

        '全てのコントロールの値に対して禁止文字チェック
        For i As Integer = 0 To max

            If Me.IsFoucsForbiddenWordsChk(ctl(i), msg(i)) = False Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置の禁止文字チェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFoucsForbiddenWordsChk(ByVal ctl As Win.InputMan.LMImTextBox, ByVal msg As String) As Boolean

        'コントロールが無い場合、スルー
        If ctl Is Nothing = True Then
            Return True
        End If

        '禁止文字チェック
        ctl.ItemName = msg
        ctl.IsForbiddenWordsCheck = True
        ctl.IsHissuCheck = False
        Return MyBase.IsValidateCheck(ctl)

    End Function

#End Region

#End Region

End Class
