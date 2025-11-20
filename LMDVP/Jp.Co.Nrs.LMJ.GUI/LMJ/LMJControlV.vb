' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ          : システム管理サブ
'  プログラムID     :  LMJControlV  : LMJValidate 共通処理
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMJControlValidateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMJControlV
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMJControlG

#End Region 'Field

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

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As Form, ByVal g As LMJControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._G = g

    End Sub

#End Region

#Region "共通入力チェック"

    ''' <summary>
    ''' 権限チェックのエラーメッセージ設定
    ''' </summary>
    ''' <param name="kengenFlg">チェック結果</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal kengenFlg As Boolean) As Boolean

        If kengenFlg = False Then
            Return Me.SetErrMessage("E016")
        End If

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="ctl">コントロール配列</param>
    ''' <param name="msg">置換文字配列</param>
    ''' <param name="lblCtl">ラベルコントロール配列</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal actionType As String _
                                   , ByVal ctl As InputMan.LMImTextBox() _
                                   , ByVal msg As String() _
                                   , ByVal lblCtl As Control() _
                                   ) As Boolean

        'メインのコントロールでの判定のみ
        'フォーカス位置が参照可能チェック
        Dim max As Integer = -1

        '何も無い場合、エラー
        If ctl Is Nothing = False Then
            max = ctl.Length - 1
        End If

        Dim rtnResult As Boolean = Me.IsFoucsReadOnlyChk(ctl, max)

        Select Case actionType

            Case LMJControlC.MASTEROPEN

                rtnResult = Me.SetFocusErrMessage(rtnResult)
                rtnResult = rtnResult AndAlso Me.IsFoucsValueChk(ctl, max, lblCtl, True)

            Case LMJControlC.ENTER

                rtnResult = rtnResult AndAlso Me.IsFoucsValueChk(ctl, max, lblCtl, False)

        End Select

        '禁止文字チェック
        rtnResult = rtnResult AndAlso Me.IsFoucsForbiddenWordsChk(ctl, msg, max)

        Return rtnResult

    End Function

    ''' <summary>
    ''' フォーカス位置が参照可能判定
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="max">ループ変数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>メインのコントロールでの判定</remarks>
    Private Function IsFoucsReadOnlyChk(ByVal ctl As InputMan.LMImTextBox(), ByVal max As Integer) As Boolean

        '何も無い場合、エラー
        If ctl Is Nothing = True Then
            Return False
        End If

        For i As Integer = 0 To max

            'ロックされている場合、エラー
            If ctl(i).ReadOnly = True Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置の禁止文字チェック
    ''' </summary>
    ''' <param name="ctl">コントロール配列</param>
    ''' <param name="msg">置換文字配列</param>
    ''' <param name="max">ループ変数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFoucsForbiddenWordsChk(ByVal ctl As InputMan.LMImTextBox(), ByVal msg As String(), ByVal max As Integer) As Boolean

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
    Private Function IsFoucsForbiddenWordsChk(ByVal ctl As InputMan.LMImTextBox, ByVal msg As String) As Boolean

        'コントロールが無い場合、スルー
        If ctl Is Nothing = True Then
            Return True
        End If

        '禁止文字チェック
        ctl.ItemName = msg
        ctl.IsHissuCheck = False
        ctl.IsForbiddenWordsCheck = True
        Return MyBase.IsValidateCheck(ctl)

    End Function

    ''' <summary>
    ''' 値が入っているかを判定
    ''' </summary>
    ''' <param name="ctl">コントロール配列</param>
    ''' <param name="max">ループ変数</param>
    ''' <param name="lblCtl">ラベルコントロール配列</param>
    ''' <param name="rtnFLg">Enterの場合：False　マスタ参照の場合：True</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFoucsValueChk(ByVal ctl As InputMan.LMImTextBox() _
                                     , ByVal max As Integer _
                                     , ByVal lblCtl As Control() _
                                     , ByVal rtnFLg As Boolean _
                                     ) As Boolean

        'どれかに値がある場合、スルー
        For i As Integer = 0 To max
            If Me.IsFoucsValueChk(ctl(i)) = True Then
                Return True
            End If
        Next

        '何もない場合、スルー
        If lblCtl Is Nothing = True Then
            Return rtnFLg
        End If

        '全てに値がない場合、Labelの値をクリア
        max = lblCtl.Length - 1
        For i As Integer = 0 To max
            Call Me.ClearControl(lblCtl(i))
        Next

        Return rtnFLg

    End Function

    ''' <summary>
    ''' 値が入っているかを判定
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFoucsValueChk(ByVal ctl As InputMan.LMImTextBox) As Boolean

        If ctl Is Nothing = True Then
            Return True
        End If

        If String.IsNullOrEmpty(ctl.TextValue) = True Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 日付のフルバイトチェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="str">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInputDateFullByteChk(ByVal ctl As InputMan.LMImDate, ByVal str As String) As Boolean

        If ctl.IsDateFullByteCheck = False Then

            Return Me.SetErrMessage("E038", New String() {str, "8"})

        End If

        Return True

    End Function

#End Region

#Region "キャッシュから値取得"

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custDrs">データロウ配列</param>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード　初期値 = ""</param>
    ''' <param name="custSCd">荷主(小)コード　初期値 = ""</param>
    ''' <param name="custSSCd">荷主(極小)コード　初期値 = ""</param>
    ''' <param name="msgType">置換文字タイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function SelectCustListDataRow(ByRef custDrs As DataRow() _
                                          , ByVal custLCd As String _
                                          , Optional ByVal custMCd As String = "" _
                                          , Optional ByVal custSCd As String = "" _
                                          , Optional ByVal custSSCd As String = "" _
                                          , Optional ByVal msgType As LMJControlC.CustMsgType = LMJControlC.CustMsgType.CUST_L _
                                          ) As Boolean

        custDrs = Me._G.SelectCustListDataRow(custLCd, custMCd, custSCd, custSSCd)

        If custDrs.Length < 1 Then
            Return Me.SetMstErrMessage("荷主マスタ", Me.SetCustMsg(custLCd, custMCd, custSCd, custSSCd, msgType))
        End If

        Return True

    End Function

    ''' <summary>
    ''' 荷主M存在チェックの置換文字
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード　初期値 = ""</param>
    ''' <param name="custSCd">荷主(小)コード　初期値 = ""</param>
    ''' <param name="custSSCd">荷主(極小)コード　初期値 = ""</param>
    ''' <param name="msgType">置換文字タイプ</param>
    ''' <returns>置換文字</returns>
    ''' <remarks></remarks>
    Private Function SetCustMsg(ByVal custLCd As String _
                                    , ByVal custMCd As String _
                                    , ByVal custSCd As String _
                                    , ByVal custSSCd As String _
                                    , ByVal msgType As LMJControlC.CustMsgType _
                                    ) As String

        SetCustMsg = String.Empty

        Select Case msgType

            Case LMJControlC.CustMsgType.CUST_L

                SetCustMsg = custLCd

            Case LMJControlC.CustMsgType.CUST_M

                SetCustMsg = Me._G.EditConcatData(custLCd, custMCd)

            Case LMJControlC.CustMsgType.CUST_S

                SetCustMsg = Me._G.EditConcatData(custLCd, custMCd)
                SetCustMsg = Me._G.EditConcatData(SetCustMsg, custSCd)

            Case LMJControlC.CustMsgType.CUST_SS

                SetCustMsg = Me._G.EditConcatData(custLCd, custMCd)
                SetCustMsg = Me._G.EditConcatData(SetCustMsg, custSCd)
                SetCustMsg = Me._G.EditConcatData(SetCustMsg, custSSCd)

        End Select


        Return SetCustMsg

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
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetFocusErrMessage() As Boolean

        Return Me.SetErrMessage("G005")

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
                               , Optional ByVal tab As LMTab = Nothing _
                               , Optional ByVal tabPage As System.Windows.Forms.TabPage = Nothing _
                               )

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor()

        If TypeOf ctl Is InputMan.LMImCombo = True Then

            DirectCast(ctl, InputMan.LMImCombo).BackColorDef = errorColor

        ElseIf TypeOf ctl Is InputMan.LMComboKubun = True Then

            DirectCast(ctl, InputMan.LMComboKubun).BackColorDef = errorColor

        ElseIf TypeOf ctl Is InputMan.LMComboNrsBr = True Then

            DirectCast(ctl, InputMan.LMComboNrsBr).BackColorDef = errorColor

        ElseIf TypeOf ctl Is InputMan.LMComboSoko = True Then

            DirectCast(ctl, InputMan.LMComboSoko).BackColorDef = errorColor

        ElseIf TypeOf ctl Is InputMan.LMImNumber = True Then

            DirectCast(ctl, InputMan.LMImNumber).BackColorDef = errorColor

        ElseIf TypeOf ctl Is InputMan.LMImTextBox = True Then

            DirectCast(ctl, InputMan.LMImTextBox).BackColorDef = errorColor

        ElseIf TypeOf ctl Is InputMan.LMImDate = True Then

            DirectCast(ctl, InputMan.LMImDate).BackColorDef = errorColor

        End If

        If tab Is Nothing = False Then
            tab.SelectedTab = tabPage
        End If

        ctl.Focus()
        ctl.Select()

    End Sub

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
    ''' セルから値を取得
    ''' </summary>
    ''' <param name="aCell">セル</param>
    ''' <returns>取得した値</returns>
    ''' <remarks></remarks>
    Friend Function GetCellValueNotTrim(ByVal aCell As Cell) As String

        GetCellValueNotTrim = String.Empty

        'テキストの場合、Trimしないで値を返却
        GetCellValueNotTrim = aCell.Text()

        Return GetCellValueNotTrim

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

#End Region


#Region "ユーティリティ"

    ''' <summary>
    ''' vbCrLf,"　"を空文字に変換
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns>値</returns>
    ''' <remarks></remarks>
    Friend Function SetRepMsgData(ByVal value As String) As String

        value = value.Replace(vbCrLf, String.Empty)
        value = value.Replace(LMJControlC.ZENKAKU_SPACE, String.Empty)
        Return value

    End Function

    ''' <summary>
    ''' 値クリア
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Sub ClearControl(ByVal ctl As Control)

        If TypeOf ctl Is InputMan.LMImCombo = True Then

            DirectCast(ctl, InputMan.LMImCombo).SelectedValue = Nothing

        ElseIf TypeOf ctl Is InputMan.LMComboKubun = True Then

            DirectCast(ctl, InputMan.LMComboKubun).SelectedValue = Nothing

        ElseIf TypeOf ctl Is InputMan.LMComboNrsBr = True Then

            DirectCast(ctl, InputMan.LMComboNrsBr).SelectedValue = Nothing

        ElseIf TypeOf ctl Is InputMan.LMComboSoko = True Then

            DirectCast(ctl, InputMan.LMComboSoko).SelectedValue = Nothing

        ElseIf TypeOf ctl Is InputMan.LMImNumber = True Then

            DirectCast(ctl, InputMan.LMImNumber).Value = 0

        ElseIf TypeOf ctl Is InputMan.LMImTextBox = True Then

            DirectCast(ctl, InputMan.LMImTextBox).TextValue = String.Empty

        ElseIf TypeOf ctl Is InputMan.LMImDate = True Then

            DirectCast(ctl, InputMan.LMImDate).Value = Nothing

        End If

    End Sub

#End Region

End Class
