' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF          : 運送サブ
'  プログラムID     :  LMMControlV  : LMFValidate 共通処理
'  作  成  者       :  [ito]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMMControlValidateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMMControlV
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMMControlG

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Hcon As LMMControlH

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As Form, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        '共通画面クラス
        Me._G = g

        'Handler共通クラスの設定
        Me._Hcon = New LMMControlH(DirectCast(frm, Form), Me, g)

    End Sub

#End Region

#Region "共通入力チェック"

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsExistDataChk(ByVal frm As Form, ByVal ctl As String) As Boolean

        If String.IsNullOrEmpty(ctl) = True Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="recStatue">対象レコードのレコードステータス</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>削除レコードの場合、エラー</remarks>
    Friend Function IsRecordStatusChk(ByVal recStatue As String) As Boolean

        If recStatue = RecordStatus.DELETE_REC Then
            MyBase.ShowMessage("E035")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 他営業所チェック
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="msg">置換文字列に設定するメッセージ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsUserNrsBrCdChk(ByVal brCd As String, ByVal msg As String) As Boolean

        'ユーザーのログイン営業所と異なる場合エラー
        If brCd.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
            MyBase.ShowMessage("E178", New String() {msg})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    '''  大小チェックを行う
    ''' </summary>
    ''' <param name="largeValue">比較値(大)</param>
    ''' <param name="smallValue">比較値(小)</param>
    ''' <param name="equalFlg">TRUE:値が同じ場合、Trueを返却、FALSE:値が同じ場合、FALSEを返却</param>
    ''' <returns>TRUE:largeValue > smallValue、FALSE:smallValue > largeValue</returns>
    ''' <remarks></remarks>
    Friend Function IsNumAlphaLargeSmallChk(ByVal largeValue As String _
                                 , ByVal smallValue As String _
                                 , Optional ByVal equalFlg As Boolean = True) As Boolean

        '桁数が異なる場合
        If largeValue.Length < smallValue.Length Then
            Return False
        ElseIf smallValue.Length < largeValue.Length Then
            Return True
        End If

        '比較用の値取得
        Dim arrLarge As ArrayList = Me.GetHikakuArray(largeValue)
        Dim arrSmall As ArrayList = Me.GetHikakuArray(smallValue)

        Dim max As Integer = arrLarge.Count - 1

        For i As Integer = 0 To max
            If arrLarge(i).ToString() < arrSmall(i).ToString() Then
                Return False
            ElseIf arrSmall(i).ToString() < arrLarge(i).ToString() Then
                Return True
            End If
        Next

        '値が同じ場合、は"equalFlg"により判定する
        Return equalFlg

    End Function

    ''' <summary>
    '''  大小チェックを行う
    ''' </summary>
    ''' <param name="value">比較値をArrayListに格納する</param>
    ''' <returns>ArrayList </returns>
    ''' <remarks></remarks>
    Private Function GetHikakuArray(ByVal value As String) As ArrayList

        Dim max As Integer = value.Length - 1
        Dim maxConst As Integer = LMMControlC.HAN_ALPHAMERIC.Length - 1

        Dim arr As ArrayList = New ArrayList()
        Dim valChar As String = String.Empty
        For i As Integer = 0 To max
            valChar = value.Substring(i, 1)
            For j As Integer = 0 To maxConst
                If valChar.Equals(LMMControlC.HAN_ALPHAMERIC.Substring(j, 1)) Then
                    arr.Add(j)
                End If
            Next
        Next

        Return arr

    End Function

    ''' <summary>
    ''' MAXSEQチェック
    ''' </summary>
    ''' <param name="value">現在のSEQ</param>
    ''' <param name="rowCnt">追加する行数</param>
    ''' <param name="replaceStr">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsMaxSeqChk(ByVal value As Integer _
                                   , ByVal rowCnt As Integer _
                                   , ByVal replaceStr As String _
                                   ) As Boolean

        If 999 < rowCnt + value Then
            MyBase.ShowMessage("E062", New String() {replaceStr})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' MAXSEQチェック
    ''' </summary>
    ''' <param name="chkNo">値</param>
    ''' <param name="maxNo">Max値</param>
    ''' <param name="chkNm">項目名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsMaxChk(ByVal chkNo As Integer _
                                   , ByVal maxNo As Integer _
                                   , ByVal chkNm As String _
                                   ) As Boolean

        If maxNo < chkNo Then
            MyBase.ShowMessage("E062", New String() {chkNm})
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

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="actionType">ファンクションキー押下/Enter押下</param>
    ''' <param name="ctl">ポップアップ起動コントロール配列</param>
    ''' <param name="msg">置換メッセージ配列</param>
    ''' <param name="focusCtl">アクティブコントロール配列</param>
    ''' <param name="clearCtl">クリアコントロール配列</param>
    ''' <returns>True:エラーなし、False:エラー有り</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal actionType As String _
                                   , ByVal ctl As Win.InputMan.LMImTextBox() _
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

        Select Case actionType

            Case LMMControlC.MASTEROPEN

                rtnResult = Me.SetFocusErrMessage(rtnResult)

            Case LMMControlC.ENTER

                rtnResult = rtnResult AndAlso Me.IsFoucsValueChk(ctl, max)

        End Select

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
    ''' 禁止文字、バイトチェック(InputMan)
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsKinsiByteChk(ByVal ctl As Win.InputMan.LMImTextBox, ByVal msg As String, ByVal keta As Integer) As Boolean

        ctl.ItemName = msg
        ctl.IsForbiddenWordsCheck = True
        ctl.IsByteCheck = keta
        Return MyBase.IsValidateCheck(ctl)

    End Function

    ''' <summary>
    ''' 禁止文字、バイトチェック(Spread)
    ''' </summary>
    ''' <param name="vCell">バリデータセル</param>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="colNo">列番号</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsKinsiByteChk(ByVal vCell As LMValidatableCells, ByVal rowNo As Integer, ByVal colNo As Integer, ByVal msg As String, ByVal keta As Integer) As Boolean

        vCell.SetValidateCell(rowNo, colNo)
        vCell.ItemName = Me.SetRepMsgData(msg)
        vCell.IsForbiddenWordsCheck = True
        vCell.IsByteCheck = keta
        Return MyBase.IsValidateCheck(vCell)

    End Function

    ''' <summary>
    ''' 日付のフルバイトチェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="str">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInputDateFullByteChk(ByVal ctl As Win.InputMan.LMImDate, ByVal str As String) As Boolean

        If ctl.IsDateFullByteCheck = False Then

            Return Me.SetErrMessage("E038", New String() {str, "8"})

        End If

        Return True

    End Function

    ''' <summary>
    ''' 子コードだけかどうかを判定
    ''' </summary>
    ''' <param name="ctlL">コントロール大</param>
    ''' <param name="ctlM">コントロール中</param>
    ''' <param name="msg1">置換文字1</param>
    ''' <param name="msg2">置換文字2</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSetValueChk(ByVal ctlL As Win.InputMan.LMImTextBox _
                                      , ByVal ctlM As Win.InputMan.LMImTextBox _
                                      , ByVal msg1 As String _
                                      , ByVal msg2 As String _
                                      ) As Boolean

        '大コードに値がある場合、スルー
        Dim lCd As String = ctlL.TextValue
        If String.IsNullOrEmpty(lCd) = False Then
            Return True
        End If

        '中コードのみの場合、エラー
        Dim mCd As String = ctlM.TextValue
        If String.IsNullOrEmpty(mCd) = False Then

            ctlM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Call Me.SetErrorControl(ctlL)
            Return Me.SetErrMessage("E017", New String() {msg1, msg2})

        End If

        Return True

    End Function

    ''' <summary>
    ''' 日付絞込の関連必須チェック
    ''' </summary>
    ''' <param name="cmbDate">絞込コンボ</param>
    ''' <param name="fromDate">日付From</param>
    ''' <param name="toDate">日付To</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsDateCtlConnectionChk(ByVal cmbDate As Win.InputMan.LMComboKubun _
                                               , ByVal fromDate As Win.InputMan.LMImDate _
                                               , ByVal toDate As Win.InputMan.LMImDate _
                                               ) As Boolean

        '日付に値が無い場合、スルー
        If String.IsNullOrEmpty(fromDate.TextValue) = True _
            AndAlso String.IsNullOrEmpty(toDate.TextValue) = True _
            Then
            Return True
        End If

        '日付絞込に値が無い場合、エラー
        If String.IsNullOrEmpty(cmbDate.SelectedValue.ToString()) = True Then

            fromDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            toDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Call Me.SetErrorControl(cmbDate)
            Return Me.SetErrMessage("E225")

        End If

        Return True

    End Function

    ''' <summary>
    ''' From + Toの大小チェック
    ''' </summary>
    ''' <param name="fromDate">Fromコントロール</param>
    ''' <param name="toDate">Toコントロール</param>
    ''' <param name="msg1">置換文字1</param>
    ''' <param name="msg2">置換文字2</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsDateFromToChk(ByVal fromDate As Win.InputMan.LMImDate _
                                        , ByVal toDate As Win.InputMan.LMImDate _
                                        , ByVal msg1 As String _
                                        , ByVal msg2 As String _
                                        ) As Boolean

        'Fromに値が無い場合、スルー
        Dim strFrom As String = fromDate.TextValue
        If String.IsNullOrEmpty(strFrom) = True Then
            Return True
        End If

        'Toに値が無い場合、スルー
        Dim strTo As String = toDate.TextValue
        If String.IsNullOrEmpty(strTo) = True Then
            Return True
        End If

        'Fromの方が大きい場合、エラー
        If strTo < strFrom Then

            fromDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Call Me.SetErrorControl(toDate)
            Return Me.SetErrMessage("E039", New String() {msg1, msg2})

        End If

        Return True

    End Function

    ''' <summary>
    ''' 大小チェック
    ''' </summary>
    ''' <param name="large">大きい方の値</param>
    ''' <param name="small">小さい方の値</param>
    ''' <param name="equalFlg">イコールがエラーの場合：True　イコールがエラーではないの場合：False</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsLargeSmallChk(ByVal large As Decimal, ByVal small As Decimal, ByVal equalFlg As Boolean) As Boolean

        '大小比較
        If equalFlg = True Then

            If large <= small Then
                Return False
            End If

        Else

            If large < small Then
                Return False
            End If

        End If

        Return True

    End Function

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
    ''' ワーニング判定
    ''' </summary>
    ''' <param name="msg">メッセージ表示の戻り値</param>
    ''' <returns>True:はい　False:いいえ</returns>
    ''' <remarks></remarks>
    Friend Function IsWarningChk(ByVal msg As Integer) As Boolean

        'メッセージを表示し、戻り値により処理を分ける
        If MsgBoxResult.Ok <> msg Then '「いいえ」を選択
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <param name="defNo">チェックボックスセルのカラム№</param>
    ''' <param name="sprDetail">対象スプレッド</param>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Overloads Function SprSelectList(ByVal defNo As Integer, ByRef sprDetail As Spread.LMSpread) As ArrayList

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
    ''' <returns>False</returns>
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

    ''' <summary>
    ''' 済チェックのエラーメッセージ
    ''' </summary>
    ''' <param name="msg1">置換文字1</param>
    ''' <param name="msg2">置換文字2</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetKakuteiGroupErrMessage(ByVal msg1 As String, ByVal msg2 As String) As Boolean

        Return Me.SetErrMessage("E232", New String() {msg1, msg2})

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ存在チェックエラーメッセージ
    ''' </summary>
    ''' <param name="tariffCd">タリフコード</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetUnchinTariffExistErr(ByVal tariffCd As String) As Boolean

        Return Me.SetMstErrMessage("運賃タリフマスタ", tariffCd)

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ存在チェックエラーメッセージ
    ''' </summary>
    ''' <param name="tariffCd">タリフコード</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetYokoTariffExistErr(ByVal tariffCd As String) As Boolean

        Return Me.SetMstErrMessage("横持ちタリフマスタ", tariffCd)

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

        'タブ頁表示
        If tab Is Nothing = False Then
            tab.SelectedTab = tabPage
        End If

        ctl.Focus()
        ctl.Select()

    End Sub

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="errorCtl">エラーコントロール</param>
    ''' <param name="focusCtl">フォーカス設定コントロール</param>
    ''' <param name="tab">タブ　初期値 = Nothing</param>
    ''' <param name="tabPage">タブページ　初期値 = Nothing</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal errorCtl As Control() _
                               , ByVal focusCtl As Control _
                               , Optional ByVal tab As LMTab = Nothing _
                               , Optional ByVal tabPage As System.Windows.Forms.TabPage = Nothing _
                               )

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor()

        Dim max As Integer = errorCtl.Length - 1
        For i As Integer = 0 To max
            If TypeOf errorCtl(i) Is Win.InputMan.LMImCombo = True Then

                DirectCast(errorCtl(i), Win.InputMan.LMImCombo).BackColorDef = errorColor

            ElseIf TypeOf errorCtl(i) Is Win.InputMan.LMComboKubun = True Then

                DirectCast(errorCtl(i), Win.InputMan.LMComboKubun).BackColorDef = errorColor

            ElseIf TypeOf errorCtl(i) Is Win.InputMan.LMComboNrsBr = True Then

                DirectCast(errorCtl(i), Win.InputMan.LMComboNrsBr).BackColorDef = errorColor

            ElseIf TypeOf errorCtl(i) Is Win.InputMan.LMComboSoko = True Then

                DirectCast(errorCtl(i), Win.InputMan.LMComboSoko).BackColorDef = errorColor

            ElseIf TypeOf errorCtl(i) Is Win.InputMan.LMImNumber = True Then

                DirectCast(errorCtl(i), Win.InputMan.LMImNumber).BackColorDef = errorColor

            ElseIf TypeOf errorCtl(i) Is Win.InputMan.LMImTextBox = True Then

                DirectCast(errorCtl(i), Win.InputMan.LMImTextBox).BackColorDef = errorColor

            ElseIf TypeOf errorCtl(i) Is Win.InputMan.LMImDate = True Then

                DirectCast(errorCtl(i), Win.InputMan.LMImDate).BackColorDef = errorColor

            End If

        Next

        'タブ頁表示
        If tab Is Nothing = False Then
            tab.SelectedTab = tabPage
        End If

        focusCtl.Focus()
        focusCtl.Select()

    End Sub

    ''' <summary>
    ''' エラーコントロール設定(明細用)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="colNo">列番号</param>
    ''' <param name="tab">タブ　初期値 = Nothing</param>
    ''' <param name="tabPage">タブページ　初期値 = Nothing</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal spr As Spread.LMSpread _
                               , ByVal rowNo As Integer _
                               , ByVal colNo As Integer _
                               , Optional ByVal tab As LMTab = Nothing _
                               , Optional ByVal tabPage As System.Windows.Forms.TabPage = Nothing _
                               )

        'タブ頁表示
        If tab Is Nothing = False Then
            tab.SelectedTab = tabPage
        End If

        With spr.ActiveSheet

            Me.MyForm.ActiveControl = spr
            spr.Focus()
            .SetActiveCell(rowNo, colNo)
            .Cells(rowNo, colNo).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()

        End With

    End Sub

    ''' <summary>
    ''' エラーコントロール設定(明細用(複数セルにエラー色可能))
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="colNo">列番号</param>
    ''' <param name="tab">タブ　初期値 = Nothing</param>
    ''' <param name="tabPage">タブページ　初期値 = Nothing</param>
    ''' <remarks>row(0),col(0)に設定したセルにフォーカスを設定する</remarks>
    Friend Sub SetErrorControl(ByVal spr As Spread.LMSpread _
                               , ByVal rowNo() As Integer _
                               , ByVal colNo() As Integer _
                               , Optional ByVal tab As LMTab = Nothing _
                               , Optional ByVal tabPage As System.Windows.Forms.TabPage = Nothing _
                               )

        'タブ頁表示
        If tab Is Nothing = False Then
            tab.SelectedTab = tabPage
        End If

        With spr.ActiveSheet

            Me.MyForm.ActiveControl = spr
            spr.Focus()
            .SetActiveCell(rowNo(0), colNo(0))
            Dim max As Integer = rowNo.Length - 1
            For i As Integer = 0 To max
                .Cells(rowNo(i), colNo(i)).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
            Next
        End With

    End Sub
#End Region

#Region "スペース除去"

    ''' <summary>
    ''' コントロールの値をTrim
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceTextvalue(ByVal ctl As Control)

        Dim arr As ArrayList = New ArrayList()
        Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
        Dim max As Integer = arr.Count - 1
        Dim arrCtl As Nrs.Win.GUI.Win.Interface.IEditableControl = Nothing

        For index As Integer = 0 To max

            arrCtl = DirectCast(arr(index), Nrs.Win.GUI.Win.Interface.IEditableControl)

            'テキストコントロールのTrim処理を行う
            If TypeOf arrCtl Is Win.InputMan.LMImTextBox = True Then
                If DirectCast(arrCtl, Win.InputMan.LMImTextBox).Name.Substring(0, 3).Equals("lbl") = False Then
                    arrCtl.TextValue = arrCtl.TextValue.Trim()
                End If
            End If
        Next

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

    ''' <summary>
    ''' セルから値を取得
    ''' </summary>
    ''' <param name="aCell">セル</param>
    ''' <returns>取得した値</returns>
    ''' <remarks></remarks>
    Public Function GetCellValue(ByVal aCell As Cell) As String

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
                                          , Optional ByVal msgType As LMMControlC.CustMsgType = LMMControlC.CustMsgType.CUST_L _
                                          ) As Boolean

        custDrs = Me.SelectCustListDataRow(custLCd, custMCd, custSCd, custSSCd)

        If custDrs.Length < 1 Then
            Return Me.SetMstErrMessage("荷主マスタ", Me.SetCustMsg(custLCd, custMCd, custSCd, custSSCd, msgType))
        End If

        Return True

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
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(custLCd _
                                                                                             , custMCd _
                                                                                             , custSCd _
                                                                                             , custSSCd) _
                                                                         , "CUST_CD_L , CUST_CD_M,CUST_CD_S,CUST_CD_SS")

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
                                    , ByVal msgType As LMMControlC.CustMsgType _
                                    ) As String

        SetCustMsg = String.Empty

        Select Case msgType

            Case LMMControlC.CustMsgType.CUST_L

                SetCustMsg = custLCd

            Case LMMControlC.CustMsgType.CUST_M

                SetCustMsg = Me._G.EditConcatData(custLCd, custMCd)

            Case LMMControlC.CustMsgType.CUST_S

                SetCustMsg = Me._G.EditConcatData(custLCd, custMCd)
                SetCustMsg = Me._G.EditConcatData(SetCustMsg, custSCd)

            Case LMMControlC.CustMsgType.CUST_SS

                SetCustMsg = Me._G.EditConcatData(custLCd, custMCd)
                SetCustMsg = Me._G.EditConcatData(SetCustMsg, custSCd)
                SetCustMsg = Me._G.EditConcatData(SetCustMsg, custSSCd)

        End Select


        Return SetCustMsg

    End Function

    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="drs">データロウ配列</param>
    ''' <param name="unsoCd">運送会社コード</param>
    ''' <param name="unsoShitenCd">運送会社支店コード</param>
    ''' <param name="msgType">置換文字タイプ</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnsocoListDataRow(ByRef drs As DataRow() _
                                             , ByVal brCd As String _
                                           , ByVal unsoCd As String _
                                           , Optional ByVal unsoShitenCd As String = "" _
                                           , Optional ByVal msgType As LMMControlC.UnsoMsgType = LMMControlC.UnsoMsgType.UNSO_COMP _
                                           ) As Boolean

        'キャッシュテーブルからデータ抽出
        drs = Me.SelectUnsocoListDataRow(brCd, unsoCd, unsoShitenCd)

        '取得できない場合、エラー
        If drs.Length < 1 Then
            Return Me.SetMstErrMessage("運送会社マスタ", Me.SetUnsoMsg(unsoCd, unsoShitenCd, msgType))
        End If

        Return True

    End Function

    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="unsoCd">運送会社コード</param>
    ''' <param name="unsoShitenCd">運送会社支店コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnsocoListDataRow(ByVal brCd As String, ByVal unsoCd As String, Optional ByVal unsoShitenCd As String = "") As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(Me.SelectUnsocoString(brCd, unsoCd, unsoShitenCd))

    End Function

    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="unsoCd">運送会社コード</param>
    ''' <param name="unsoShitenCd">運送会社支店コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectUnsocoString(ByVal brCd As String, ByVal unsoCd As String, Optional ByVal unsoShitenCd As String = "") As String

        SelectUnsocoString = String.Empty

        '削除フラグ
        SelectUnsocoString = String.Concat(SelectUnsocoString, " SYS_DEL_FLG = '0' ")

        '拠点コード
        SelectUnsocoString = String.Concat(SelectUnsocoString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '運送会社コード
        SelectUnsocoString = String.Concat(SelectUnsocoString, " AND ", "UNSOCO_CD = ", " '", unsoCd, "' ")

        '運送会社コード
        If String.IsNullOrEmpty(unsoShitenCd) = False Then
            SelectUnsocoString = String.Concat(SelectUnsocoString, " AND ", "UNSOCO_BR_CD = ", " '", unsoShitenCd, "' ")
        End If

        Return SelectUnsocoString

    End Function

    ''' <summary>
    ''' 運送会社M存在チェックの置換文字
    ''' </summary>
    ''' <param name="unsoCd">運送会社コード</param>
    ''' <param name="unsoShitenCd">運送支店コード</param>
    ''' <param name="msgType">置換文字タイプ</param>
    ''' <returns>置換文字</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoMsg(ByVal unsoCd As String _
                                    , ByVal unsoShitenCd As String _
                                    , ByVal msgType As LMMControlC.UnsoMsgType _
                                    ) As String

        SetUnsoMsg = String.Empty

        Select Case msgType

            Case LMMControlC.UnsoMsgType.UNSO_COMP

                SetUnsoMsg = unsoCd

            Case LMMControlC.UnsoMsgType.UNSO_SHITEN

                SetUnsoMsg = Me._G.EditConcatData(unsoCd, unsoShitenCd)

        End Select

        Return SetUnsoMsg

    End Function

    ''' <summary>
    ''' 割増タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="drs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="extcCd">割増タリフコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectExtcUnchinListDataRow(ByRef drs As DataRow() _
                                                , ByVal brCd As String _
                                                , ByVal extcCd As String _
                                                , Optional ByVal jisCd As String = "0000000" _
                                                ) As Boolean

        'キャッシュテーブルからデータ抽出
        drs = Me.SelectExtcUnchinListDataRow(brCd, extcCd, jisCd)

        '取得できない場合、エラー
        If drs.Length < 1 Then
            Return Me.SetMstErrMessage("割増タリフマスタ", extcCd)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 割増タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="extcCd">割増タリフコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectExtcUnchinListDataRow(ByVal brCd As String _
                                                , ByVal extcCd As String _
                                                , Optional ByVal jisCd As String = "0000000" _
                                                ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EXTC_UNCHIN).Select(Me.SelectExtcUnchinString(brCd, extcCd, jisCd))

    End Function

    ''' <summary>
    ''' 割増タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="extcCd">割増タリフコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectExtcUnchinString(ByVal brCd As String _
                                            , ByVal extcCd As String _
                                            , ByVal jisCd As String _
                                            ) As String

        SelectExtcUnchinString = String.Empty

        '営業所
        SelectExtcUnchinString = String.Concat(SelectExtcUnchinString, " NRS_BR_CD = ", " '", brCd, "' ")

        '割増タリフコード
        SelectExtcUnchinString = String.Concat(SelectExtcUnchinString, " AND ", "EXTC_TARIFF_CD = ", " '", extcCd, "' ")

        'JISコード
        SelectExtcUnchinString = String.Concat(SelectExtcUnchinString, " AND ", "JIS_CD = ", " '", jisCd, "' ")

        Return SelectExtcUnchinString

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="drs">データロウ配列</param>
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
    Friend Function SelectTariffSetListDataRow(ByRef drs As DataRow() _
                                               , ByVal brCd As String _
                                               , ByVal custLCd As String _
                                               , ByVal custMCd As String _
                                               , ByVal setCd As String _
                                               , Optional ByVal tehaiKbn As String = "" _
                                               , Optional ByVal tariffCd1 As String = "" _
                                               , Optional ByVal tariffCd2 As String = "" _
                                               , Optional ByVal yokoCd As String = "" _
                                               ) As Boolean


        'キャッシュテーブルからデータ抽出
        drs = Me.SelectTariffSetListDataRow(brCd, custLCd, custMCd, setCd, tehaiKbn, tariffCd1, tariffCd2, yokoCd)

        '取得できない場合、エラー
        Dim msg As String = String.Empty
        If String.IsNullOrEmpty(tehaiKbn) = False Then
            msg = tehaiKbn
        ElseIf String.IsNullOrEmpty(tariffCd1) = False Then
            msg = tariffCd1
        ElseIf String.IsNullOrEmpty(tariffCd2) = False Then
            msg = tariffCd2
        ElseIf String.IsNullOrEmpty(yokoCd) = False Then
            msg = yokoCd
        End If

        If drs.Length < 1 Then
            Return Me.SetMstErrMessage("運賃タリフセットマスタ", msg)
        End If

        Return True

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

        '営業所コード
        SelectTariffSetString = String.Concat(SelectTariffSetString, " NRS_BR_CD = ", " '", brCd, "' ")

        '荷主(大)コード
        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")

        '荷主(中)コード
        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "CUST_CD_M = ", " '", custLCd, "' ")

        'セットマスタコード
        If String.IsNullOrEmpty(setCd) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "SET_MST_CD = ", " '", setCd, "' ")

        End If

        'セット区分（キャッシュにセット区分なし）
        If String.IsNullOrEmpty(setCd) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "SET_KB = ", " '", tehaiKbn, "' ")

        End If

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
    ''' 運賃タリフセットマスタ（運賃タリフセットマスタ情報の削除チェック用）(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <param name="setKbn">セット区分</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectTariffSetListDelDataRow(ByVal brCd As String _
                                               , ByVal custLCd As String _
                                               , ByVal custMCd As String _
                                               , ByVal destCd As String _
                                               , ByVal setKbn As String _
                                               ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET).Select(Me.SelectTariffSetDelString(brCd, custLCd, custMCd, destCd, setKbn))

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ（運賃タリフセットマスタ情報の削除チェック用）(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <param name="setKbn">セット区分</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Private Function SelectTariffSetDelString(ByVal brCd As String _
                                           , ByVal custLCd As String _
                                           , ByVal custMCd As String _
                                           , ByVal destCd As String _
                                           , ByVal setKbn As String _
                                           ) As String

        SelectTariffSetDelString = String.Empty

        '営業所コード
        SelectTariffSetDelString = String.Concat(SelectTariffSetDelString, " NRS_BR_CD = ", " '", brCd, "' ")

        '荷主(大)コード
        SelectTariffSetDelString = String.Concat(SelectTariffSetDelString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")

        '荷主(中)コード
        SelectTariffSetDelString = String.Concat(SelectTariffSetDelString, " AND ", "CUST_CD_M = ", " '", custLCd, "' ")

        '届先コード
        SelectTariffSetDelString = String.Concat(SelectTariffSetDelString, " AND ", "DEST_CD = ", " '", destCd, "' ")

        'セット区分
        SelectTariffSetDelString = String.Concat(SelectTariffSetDelString, " AND ", "SET_KB = ", " '", setKbn, "' ")

        Return SelectTariffSetDelString

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <param name="tableType">テーブルタイプ</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnchinTariffListDataRow(ByVal tariffCd As String _
                                                  , Optional ByVal tariffCdEda As String = "" _
                                                  , Optional ByVal startDate As String = "" _
                                                  , Optional ByVal tableType As String = "" _
                                                  ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF).Select(Me.SelectUnchinTariffString(tariffCd, tariffCdEda, startDate, tableType))

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <param name="tableType">テーブルタイプ</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinTariffString(ByVal tariffCd As String _
                                              , Optional ByVal tariffCdEda As String = "" _
                                              , Optional ByVal startDate As String = "" _
                                              , Optional ByVal tableType As String = "" _
                                              ) As String

        SelectUnchinTariffString = String.Empty

        '運賃タリフコード
        SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " UNCHIN_TARIFF_CD = ", " '", tariffCd, "' ")

        '運賃タリフコード枝番
        If String.IsNullOrEmpty(tariffCdEda) = False Then
            SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " AND ", "UNCHIN_TARIFF_CD_EDA = ", " '", tariffCdEda, "' ")
        End If

        '適用開始日
        If String.IsNullOrEmpty(startDate) = False Then
            SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " AND ", "STR_DATE = ", " '", startDate, "' ")
        End If

        'テーブルタイプ
        If String.IsNullOrEmpty(tableType) = False Then
            '2019/07/11 依頼番号:006680 del start
            'SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " AND ", "TABLE_TP = ", " '", tableType, "' ")
            '2019/07/11 依頼番号:006680 del end
            '2019/07/11 依頼番号:006680 add start
            If tableType <> "**" Then
                SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " AND ", "TABLE_TP = ", " '", tableType, "' ")
            End If
            '2019/07/11 依頼番号:006680 add end
        Else
            SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " AND ", "TABLE_TP <> ", " '", LMMControlC.FLG_ON, "' ")
        End If

        Return SelectUnchinTariffString

    End Function


    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnchinTariffListDataRow2(ByVal tariffCd As String _
                                                  , Optional ByVal tariffCdEda As String = "" _
                                                  , Optional ByVal startDate As String = "" _
                                                  ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF).Select(Me.SelectUnchinTariffString2(tariffCd, tariffCdEda, startDate))

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinTariffString2(ByVal tariffCd As String _
                                              , Optional ByVal tariffCdEda As String = "" _
                                              , Optional ByVal startDate As String = "" _
                                              ) As String

        SelectUnchinTariffString2 = String.Empty

        '運賃タリフコード()
        SelectUnchinTariffString2 = String.Concat(SelectUnchinTariffString2, " UNCHIN_TARIFF_CD = ", " '", tariffCd, "' ")

        '運賃タリフコード枝番
        If String.IsNullOrEmpty(tariffCdEda) = False Then
            SelectUnchinTariffString2 = String.Concat(SelectUnchinTariffString2, " AND ", "UNCHIN_TARIFF_CD_EDA = ", " '", tariffCdEda, "' ")
        End If

        '適用開始日
        If String.IsNullOrEmpty(startDate) = False Then
            SelectUnchinTariffString2 = String.Concat(SelectUnchinTariffString2, " AND ", "STR_DATE <= ", " '", startDate, "' ")
        End If

        Return SelectUnchinTariffString2

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="drs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="tariffCd">横持ちタリフコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectYokoTariffListDataRow(ByRef drs As DataRow() _
                                                  , ByVal brCd As String _
                                                  , ByVal tariffCd As String) As Boolean

        'キャッシュテーブルからデータ抽出
        drs = Me.SelectYokoTariffListDataRow(brCd, tariffCd)

        '取得できない場合、エラー
        If drs.Length < 1 Then
            Return Me.SetMstErrMessage("横持ちタリフマスタ", tariffCd)
        End If

        Return True

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
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.YOKO_TARIFF_HD).Select(Me.SelectYokoTariffString(brCd, tariffCd))

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
    ''' 距離程マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kyoriDrs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="kyoriCd">距離程コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectKyoriListDataRow(ByRef kyoriDrs As DataRow() _
                                           , ByVal brCd As String _
                                           , ByVal kyoriCd As String _
                                           ) As Boolean

        'キャッシュテーブルからデータ抽出
        kyoriDrs = Me.SelectKyoriListDataRow(brCd, kyoriCd)

        '取得できない場合、エラー
        If kyoriDrs.Length < 1 Then
            Return Me.SetMstErrMessage("距離程マスタ", kyoriCd)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 距離程マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="kyoriCd">距離程コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectKyoriListDataRow(ByVal brCd As String _
                                          , ByVal kyoriCd As String _
                                          ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KYORI_GRP).Select(Me.SelectKyoriString(brCd, kyoriCd))

    End Function

    ''' <summary>
    ''' 距離程マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="kyoriCd">距離程コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectKyoriString(ByVal brCd As String _
                                       , ByVal kyoriCd As String _
                                       ) As String

        SelectKyoriString = String.Empty

        '営業所コード
        SelectKyoriString = String.Concat(SelectKyoriString, " NRS_BR_CD = ", " '", brCd, "' ")

        '距離程コード
        SelectKyoriString = String.Concat(SelectKyoriString, " AND ", "KYORI_CD = ", " '", kyoriCd, "' ")

        Return SelectKyoriString

    End Function

    'START UMANO 要望番号1387 支払処理修正。
    ''' <summary>
    ''' 支払運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <param name="tableType">テーブルタイプ</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectShiharaiTariffListDataRow(ByVal tariffCd As String _
                                                  , Optional ByVal tariffCdEda As String = "" _
                                                  , Optional ByVal startDate As String = "" _
                                                  , Optional ByVal tableType As String = "" _
                                                  ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SHIHARAI_TARIFF).Select(Me.SelectShiharaiTariffString(tariffCd, tariffCdEda, startDate, tableType))

    End Function
    'END UMANO 要望番号1387 支払処理修正。

    'START UMANO 要望番号1387 支払処理修正。
    ''' <summary>
    ''' 支払運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <param name="tableType">テーブルタイプ</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectShiharaiTariffString(ByVal tariffCd As String _
                                              , Optional ByVal tariffCdEda As String = "" _
                                              , Optional ByVal startDate As String = "" _
                                              , Optional ByVal tableType As String = "" _
                                              ) As String

        SelectShiharaiTariffString = String.Empty

        '支払運賃タリフコード
        SelectShiharaiTariffString = String.Concat(SelectShiharaiTariffString, " SHIHARAI_TARIFF_CD = ", " '", tariffCd, "' ")

        '支払運賃タリフコード枝番
        If String.IsNullOrEmpty(tariffCdEda) = False Then
            SelectShiharaiTariffString = String.Concat(SelectShiharaiTariffString, " AND ", "SHIHARAI_TARIFF_CD_EDA = ", " '", tariffCdEda, "' ")
        End If

        '適用開始日
        If String.IsNullOrEmpty(startDate) = False Then
            SelectShiharaiTariffString = String.Concat(SelectShiharaiTariffString, " AND ", "STR_DATE = ", " '", startDate, "' ")
        End If

        'テーブルタイプ
        If String.IsNullOrEmpty(tableType) = False Then
            SelectShiharaiTariffString = String.Concat(SelectShiharaiTariffString, " AND ", "TABLE_TP = ", " '", tableType, "' ")
        Else
            SelectShiharaiTariffString = String.Concat(SelectShiharaiTariffString, " AND ", "TABLE_TP <> ", " '", LMMControlC.FLG_ON, "' ")
        End If

        Return SelectShiharaiTariffString

    End Function
    'END UMANO 要望番号1387 支払処理修正。

    'START UMANO 要望番号1387 支払処理修正。
    ''' <summary>
    ''' 支払割増タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="drs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="extcCd">割増タリフコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectExtcShiharaiListDataRow(ByRef drs As DataRow() _
                                                , ByVal brCd As String _
                                                , ByVal extcCd As String _
                                                , Optional ByVal jisCd As String = "0000000" _
                                                ) As Boolean

        'キャッシュテーブルからデータ抽出
        drs = Me.SelectExtcShiharaiListDataRow(brCd, extcCd, jisCd)

        '取得できない場合、エラー
        If drs.Length < 1 Then
            Return Me.SetMstErrMessage("支払割増タリフマスタ", extcCd)
        End If

        Return True

    End Function
    'END UMANO 要望番号1387 支払処理修正。

    'START UMANO 要望番号1387 支払処理修正。
    ''' <summary>
    ''' 割増タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="extcCd">割増タリフコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectExtcShiharaiListDataRow(ByVal brCd As String _
                                                , ByVal extcCd As String _
                                                , Optional ByVal jisCd As String = "0000000" _
                                                ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EXTC_SHIHARAI).Select(Me.SelectExtcShiharaiString(brCd, extcCd, jisCd))

    End Function
    'END UMANO 要望番号1387 支払処理修正。

    'START UMANO 要望番号1387 支払処理修正。
    ''' <summary>
    ''' 支払割増タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="extcCd">支払割増タリフコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectExtcShiharaiString(ByVal brCd As String _
                                            , ByVal extcCd As String _
                                            , ByVal jisCd As String _
                                            ) As String

        SelectExtcShiharaiString = String.Empty

        '営業所
        SelectExtcShiharaiString = String.Concat(SelectExtcShiharaiString, " NRS_BR_CD = ", " '", brCd, "' ")

        '支払割増タリフコード
        SelectExtcShiharaiString = String.Concat(SelectExtcShiharaiString, " AND ", "EXTC_TARIFF_CD = ", " '", extcCd, "' ")

        'JISコード
        SelectExtcShiharaiString = String.Concat(SelectExtcShiharaiString, " AND ", "JIS_CD = ", " '", jisCd, "' ")

        Return SelectExtcShiharaiString

    End Function
    'END UMANO 要望番号1387 支払処理修正。

    ''' <summary>
    ''' 届先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="destDrs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custCdL">荷主(大)コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectDestListDataRow(ByRef destDrs As DataRow() _
                                          , ByVal brCd As String _
                                          , ByVal custCdL As String _
                                          , ByVal destCd As String _
                                          ) As Boolean

        'キャッシュテーブルからデータ抽出
        destDrs = Me.SelectDestListDataRow(brCd, custCdL, destCd)

        '取得できない場合、エラー
        If destDrs.Length < 1 Then
            Return Me.SetMstErrMessage("届先マスタ", destCd)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 届先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custCdL">荷主(大)コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectDestListDataRow(ByVal brCd As String _
                                          , ByVal custCdL As String _
                                          , ByVal destCd As String _
                                          ) As DataRow()

        'キャッシュテーブルからデータ抽出
        '---↓
        'Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(Me.SelectDestString(brCd, custCdL, destCd))

        Dim destMstDs As MDestDS = New MDestDS
        Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
        destMstDr.Item("NRS_BR_CD") = brCd
        destMstDr.Item("CUST_CD_L") = custCdL
        destMstDr.Item("DEST_CD") = destCd
        destMstDr.Item("SYS_DEL_FLG") = "0"
        destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
        Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
        Return rtnDs.Tables(LMConst.CacheTBL.DEST).Select
        '---↑

    End Function

    ''' <summary>
    ''' 届先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custCdL">荷主(大)コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectDestString(ByVal brCd As String _
                                       , ByVal custCdL As String _
                                       , ByVal destCd As String _
                                       ) As String

        SelectDestString = String.Empty

        '削除フラグ
        SelectDestString = String.Concat(SelectDestString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectDestString = String.Concat(SelectDestString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '荷主(大)コード
        SelectDestString = String.Concat(SelectDestString, " AND ", "CUST_CD_L = ", " '", custCdL, "' ")

        '届先コード
        SelectDestString = String.Concat(SelectDestString, " AND ", "DEST_CD = ", " '", destCd, "' ")

        Return SelectDestString

    End Function

    ''' <summary>
    ''' 届先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    ''' 
    Friend Function SelectDestListDataRow(ByVal brCd As String _
                                          , ByVal destCd As String _
                                          ) As DataRow()

        'キャッシュテーブルからデータ抽出
        '---↓
        'Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(Me.SelectDestString(brCd, destCd))

        Dim destMstDs As MDestDS = New MDestDS
        Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
        destMstDr.Item("NRS_BR_CD") = brCd
        destMstDr.Item("DEST_CD") = destCd
        destMstDr.Item("SYS_DEL_FLG") = "0"
        destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
        Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
        Return rtnDs.Tables(LMConst.CacheTBL.DEST).Select
        '---↑

    End Function

    ''' <summary>
    ''' 届先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectDestString(ByVal brCd As String _
                                       , ByVal destCd As String _
                                       ) As String

        SelectDestString = String.Empty

        '削除フラグ
        SelectDestString = String.Concat(SelectDestString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectDestString = String.Concat(SelectDestString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")


        '届先JISコード
        SelectDestString = String.Concat(SelectDestString, " AND ", "DEST_CD = ", " '", destCd, "' ")

        Return SelectDestString

    End Function

    ''' <summary>
    ''' 届先マスタ（EDI届先コード重複チェック用）(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custCdL">荷主(大)コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <param name="ediCd">EDI届先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectEdiDestListDataRow(ByVal brCd As String _
                                          , ByVal custCdL As String _
                                          , ByVal destCd As String _
                                          , ByVal ediCd As String _
                                          ) As DataRow()

        'キャッシュテーブルからデータ抽出
        '---↓
        'Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(Me.SelectEdiDestString(brCd, custCdL, destCd, ediCd))

        Dim destMstDs As MDestDS = New MDestDS
        Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
        destMstDr.Item("NRS_BR_CD") = brCd
        destMstDr.Item("CUST_CD_L") = custCdL
        destMstDr.Item("EDI_CD") = ediCd
        destMstDr.Item("SYS_DEL_FLG") = "0"
        destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
        Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
        Return rtnDs.Tables(LMConst.CacheTBL.DEST).Select("DEST_CD <> '" & destCd & "'")
        '---↑

    End Function

    ''' <summary>
    ''' 届先マスタ（EDI届先コード重複チェック用）(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custCdL">荷主(大)コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <param name="ediCd">EDI届先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectEdiDestString(ByVal brCd As String _
                                       , ByVal custCdL As String _
                                       , ByVal destCd As String _
                                       , ByVal ediCd As String _
                                       ) As String

        SelectEdiDestString = String.Empty

        '削除フラグ
        SelectEdiDestString = String.Concat(SelectEdiDestString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectEdiDestString = String.Concat(SelectEdiDestString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '荷主(大)コード
        SelectEdiDestString = String.Concat(SelectEdiDestString, " AND ", "CUST_CD_L = ", " '", custCdL, "' ")

        '届先コード
        SelectEdiDestString = String.Concat(SelectEdiDestString, " AND ", "DEST_CD <> ", " '", destCd, "' ")

        'EDI届先コード
        SelectEdiDestString = String.Concat(SelectEdiDestString, " AND ", "EDI_CD = ", " '", ediCd, "' ")

        Return SelectEdiDestString

    End Function

    ''' <summary>
    ''' エリアマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="ariaDrs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="areaCd">エリアコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectAreaListDataRow(ByRef ariaDrs As DataRow() _
                                          , ByVal brCd As String _
                                          , ByVal areaCd As String _
                                          , ByVal jisCd As String _
                                          ) As Boolean

        'キャッシュテーブルからデータ抽出
        ariaDrs = Me.SelectAreaListDataRow(brCd, areaCd, jisCd)

        '取得できない場合、エラー
        If ariaDrs.Length < 1 Then
            Return Me.SetMstErrMessage("エリアマスタ", areaCd)
        End If

        Return True

    End Function

    ''' <summary>
    ''' エリアマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="areaCd">エリアコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectAreaListDataRow(ByVal brCd As String _
                                          , ByVal areaCd As String _
                                          , ByVal jisCd As String _
                                          ) As DataRow()


        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.AREA).Select(Me.SelectAreaString(brCd, areaCd, jisCd))

    End Function

    ''' <summary>
    ''' エリアマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="areaCd">エリアコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectAreaString(ByVal brCd As String _
                                      , ByVal areaCd As String _
                                      , ByVal jisCd As String _
                                      ) As String

        SelectAreaString = String.Empty

        '削除フラグ
        SelectAreaString = String.Concat(SelectAreaString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectAreaString = String.Concat(SelectAreaString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        'エリアコード
        SelectAreaString = String.Concat(SelectAreaString, " AND ", "AREA_CD = ", " '", areaCd, "' ")

        If String.IsNullOrEmpty(jisCd) = False Then

            'JISコード
            SelectAreaString = String.Concat(SelectAreaString, " AND ", "JIS_CD = ", " '", jisCd, "' ")

        End If

        Return SelectAreaString

    End Function

    ''' <summary>
    ''' JISマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="jiscd">JISコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectJisListDataRow(ByVal jiscd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.JIS).Select(Me.SelectJisString(jiscd))

    End Function

    ''' <summary>
    ''' JISマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="jiscd">JISコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectJisString(ByVal jiscd As String) As String

        SelectJisString = String.Empty

        '削除フラグ
        SelectJisString = String.Concat(SelectJisString, " SYS_DEL_FLG = '0' ")

        'JISコード
        SelectJisString = String.Concat(SelectJisString, " AND ", "JIS_CD = ", " '", jiscd, "' ")

        Return SelectJisString

    End Function

    ''' <summary>
    ''' 休日マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="hol">業務休日</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectHolListDataRow(ByVal hol As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.HOL).Select(Me.SelectHolString(hol))

    End Function

    ''' <summary>
    ''' 休日マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="hol">業務休日</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectHolString(ByVal hol As String) As String

        SelectHolString = String.Empty

        '削除フラグ
        SelectHolString = String.Concat(SelectHolString, " SYS_DEL_FLG = '0' ")

        '業務休日
        SelectHolString = String.Concat(SelectHolString, " AND ", "HOL = ", " '", hol, "' ")

        Return SelectHolString

    End Function

    '''' <summary>
    '''' 商品マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    '''' </summary>
    '''' <param name="destDrs">データロウ配列</param>
    '''' <param name="brCd">営業所コード</param>
    '''' <param name="goodsKey">届先コード</param>
    '''' <returns>データロウ配列</returns>
    '''' <remarks></remarks>
    'Friend Function SelectGoodsListDataRow(ByRef destDrs As DataRow() _
    '                                      , ByVal brCd As String _
    '                                      , ByVal goodsKey As String _
    '                                      ) As Boolean

    '    'キャッシュテーブルからデータ抽出
    '    destDrs = Me.SelectGoodsListDataRow(brCd, goodsKey)

    '    '取得できない場合、エラー
    '    If destDrs.Length < 1 Then
    '        Return Me.SetMstErrMessage("商品マスタ", goodsKey)
    '    End If

    '    Return True

    'End Function

    '''' <summary>
    '''' 商品マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    '''' </summary>
    '''' <param name="brCd">営業所コード</param>
    '''' <param name="goodsCd">商品KEY</param>
    '''' <returns>データロウ配列</returns>
    '''' <remarks></remarks>
    'Friend Function SelectGoodsListDataRow(ByVal brCd As String _
    '                                          , ByVal goodsCd As String _
    '                                          ) As DataRow()

    '    'キャッシュテーブルからデータ抽出
    '    Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(Me.SelectGoodsString(brCd, goodsCd))

    'End Function

    '''' <summary>
    '''' 商品マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    '''' </summary>
    '''' <param name="brCd">営業所コード</param>
    '''' <param name="goodsCd">商品KEY</param>
    '''' <returns>検索条件</returns>
    '''' <remarks></remarks>
    'Private Function SelectGoodsString(ByVal brCd As String _
    '                                  , ByVal goodsCd As String _
    '                                  ) As String

    '    SelectGoodsString = String.Empty

    '    '削除フラグ
    '    SelectGoodsString = String.Concat(SelectGoodsString, " SYS_DEL_FLG = '0' ")

    '    '営業所コード
    '    SelectGoodsString = String.Concat(SelectGoodsString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

    '    '商品KEY
    '    SelectGoodsString = String.Concat(SelectGoodsString, " AND ", "GOODS_CD_NRS = ", " '", goodsCd, "' ")

    '    Return SelectGoodsString

    'End Function

    ''' <summary>
    ''' 商品マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="goods">商品コード</param>
    ''' <param name="goodsKey">商品KEY</param>
    ''' <param name="custCdL">荷主コード(大)</param>
    ''' <param name="custCdM">荷主コード(中)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SelectgoodsListDataGoodCdRow(ByVal brCd As String _
                                                       , ByVal goods As String _
                                                       , Optional ByVal goodsKey As String = "" _
                                                       , Optional ByVal custCdL As String = "" _
                                                       , Optional ByVal custCdM As String = "") As DataRow()

        'キャッシュテーブルからデータ抽出
        '---↓
        'Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(Me.SelectgoodsCdString(brCd, goods, goodsKey, custCdL, custCdM))

        Dim goodsDs As MGoodsDS = New MGoodsDS
        Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
        goodsDr.Item("NRS_BR_CD") = brCd
        goodsDr.Item("GOODS_CD_CUST") = goods
        If String.IsNullOrEmpty(goodsKey) = False Then goodsDr.Item("GOODS_CD_NRS") = goodsKey
        If String.IsNullOrEmpty(custCdL) = False Then goodsDr.Item("CUST_CD_L") = custCdL
        If String.IsNullOrEmpty(custCdM) = False Then goodsDr.Item("CUST_CD_M") = custCdM
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
    ''' <param name="goods">商品コード</param>
    ''' <param name="goodsKey">商品KEY</param>
    ''' <param name="custCdL">荷主コード(大)</param>
    ''' <param name="custCdM">荷主コード(中)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectgoodsCdString(ByVal brCd As String _
                                              , ByVal goods As String _
                                              , Optional ByVal goodsKey As String = "" _
                                              , Optional ByVal custCdL As String = "" _
                                              , Optional ByVal custCdM As String = "" _
                                               ) As String

        SelectgoodsCdString = String.Empty
        '削除フラグ
        SelectgoodsCdString = String.Concat(SelectgoodsCdString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectgoodsCdString = String.Concat(SelectgoodsCdString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '商品コード
        SelectgoodsCdString = String.Concat(SelectgoodsCdString, " AND ", "GOODS_CD_CUST = ", " '", goods, "' ")

        If String.IsNullOrEmpty(goodsKey) = False Then
            '商品KEY
            SelectgoodsCdString = String.Concat(SelectgoodsCdString, " AND ", "GOODS_CD_NRS = ", " '", goodsKey, "' ")
        End If

        If String.IsNullOrEmpty(custCdL) = False Then
            '荷主コード(大)
            SelectgoodsCdString = String.Concat(SelectgoodsCdString, " AND ", "CUST_CD_L = ", " '", custCdL, "' ")
        End If

        If String.IsNullOrEmpty(custCdM) = False Then
            '荷主コード(中)
            SelectgoodsCdString = String.Concat(SelectgoodsCdString, " AND ", "CUST_CD_M = ", " '", custCdM, "' ")
        End If

        Return SelectgoodsCdString

    End Function

    ''' <summary>
    ''' 請求先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="drs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="seqtoCd">請求先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectSeiqtoListDataRow(ByRef drs As DataRow() _
                                            , ByVal brCd As String _
                                            , ByVal seqtoCd As String) As Boolean

        'キャッシュテーブルからデータ抽出
        drs = Me.SelectSeiqtoListDataRow(brCd, seqtoCd)

        '取得できない場合、エラー
        If drs.Length < 1 Then
            Return Me.SetMstErrMessage("請求先マスタ", seqtoCd)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 請求先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
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
    ''' 請求先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
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

        '請求先コード
        SelectSeiqtoString = String.Concat(SelectSeiqtoString, " AND ", "SEIQTO_CD = ", " '", seqtoCd, "' ")

        Return SelectSeiqtoString

    End Function

    ''' <summary>
    ''' 請求項目マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="seiqkmkDrs">データロウ配列</param>
    ''' <param name="seiqkmkCd">請求項目コード</param>
    ''' <param name="seiqkmkCdS">請求項目コード小分類</param>
    ''' <param name="grpKb">請求グループコード区分</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectSeiqkmkListDataRow(ByRef seiqkmkDrs As DataRow() _
                                            , ByVal seiqkmkCd As String _
                                            , ByVal seiqkmkCdS As String _
                                            , Optional ByVal grpKb As String = ""
                                            ) As Boolean

        seiqkmkDrs = Me.SelectSeiqkmkListDataRow(seiqkmkCd, seiqkmkCdS, grpKb)

        If seiqkmkDrs.Length < 1 Then
            If String.IsNullOrEmpty(seiqkmkCdS) Then
                Return Me.SetMstErrMessage("請求項目マスタ", String.Concat(seiqkmkCd))
            Else
                Return Me.SetMstErrMessage("請求項目マスタ", String.Concat(seiqkmkCd, "-", seiqkmkCdS))
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' 請求項目マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="seiqkmkCd">請求項目コード</param>
    ''' <param name="seiqkmkCdS">請求項目コード小分類</param>
    ''' <param name="grpKb">請求グループコード区分</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectSeiqkmkListDataRow(ByVal seiqkmkCd As String, ByVal seiqkmkCdS As String, Optional ByVal grpKb As String = "") As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQKMK).Select(Me.SelectSeiqkmkString(seiqkmkCd, seiqkmkCdS, grpKb))

    End Function

    ''' <summary>
    ''' 請求項目マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="seiqkmkCd">請求項目コード</param>
    ''' <param name="seiqkmkCdS">請求項目コード小分類</param>
    ''' <param name="grpKb">請求グループコード区分</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqkmkString(ByVal seiqkmkCd As String, ByVal seiqkmkCdS As String, Optional ByVal grpKb As String = "") As String

        SelectSeiqkmkString = String.Empty

        '削除フラグ
        SelectSeiqkmkString = String.Concat(SelectSeiqkmkString, " SYS_DEL_FLG = '0' ")

        '請求項目コード
        SelectSeiqkmkString = String.Concat(SelectSeiqkmkString, " AND ", "SEIQKMK_CD = ", " '", seiqkmkCd, "' ")

        '請求項目コード小分類
        SelectSeiqkmkString = String.Concat(SelectSeiqkmkString, " AND ", "SEIQKMK_CD_S = ", " '", seiqkmkCdS, "' ")

        '請求グループコード区分
        If String.IsNullOrEmpty(grpKb) = False Then
            SelectSeiqkmkString = String.Concat(SelectSeiqkmkString, " AND ", "GROUP_KB = ", " '", grpKb, "' ")
        End If

        Return SelectSeiqkmkString

    End Function

    ''' <summary>
    ''' 作業項目マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="destDrs">データロウ配列</param>
    ''' <param name="sagyoCd">作業コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectSagyoListDataRow(ByRef destDrs As DataRow() _
                                          , ByVal sagyoCd As String _
                                          , ByVal custCdL As String _
                                          , ByVal brCd As String _
                                          ) As Boolean

        'キャッシュテーブルからデータ抽出
        destDrs = Me.SelectSagyoListDataRow(sagyoCd, custCdL, brCd)

        '取得できない場合、エラー
        If destDrs.Length < 1 Then
            Return Me.SetMstErrMessage("作業項目マスタ", sagyoCd)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 作業項目マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="sagyoCd">作業コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectSagyoListDataRow(ByVal sagyoCd As String _
                                           , ByVal custCdL As String _
                                           , ByVal brCd As String _
                                            ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(Me.SelectSagyoString(sagyoCd, custCdL, brCd))

    End Function

    ''' <summary>
    ''' 作業項目マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="sagyoCd">作業コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoString(ByVal sagyoCd As String _
                                       , ByVal custCdL As String _
                                       , ByVal brCd As String) As String

        SelectSagyoString = String.Empty

        '削除フラグ
        SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")

        '作業コード
        SelectSagyoString = String.Concat(SelectSagyoString, " AND ", "SAGYO_CD = ", " '", sagyoCd, "' ")

        '荷主コード
        SelectSagyoString = String.Concat(SelectSagyoString, " AND ", "CUST_CD_L = ", " '", custCdL, "' ")

        '営業所コード
        SelectSagyoString = String.Concat(SelectSagyoString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        Return SelectSagyoString

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
    ''' 
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
        'SelectToShitsuString = String.Concat(SelectToShitsuString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectToShitsuString = String.Concat(SelectToShitsuString, "NRS_BR_CD = ", " '", brCd, "' ")

        '倉庫コード
        SelectToShitsuString = String.Concat(SelectToShitsuString, " AND ", "WH_CD = ", " '", whCd, "' ")

        '棟コード
        SelectToShitsuString = String.Concat(SelectToShitsuString, " AND ", "TOU_NO = ", " '", touNo, "' ")

        '室コード
        SelectToShitsuString = String.Concat(SelectToShitsuString, " AND ", "SITU_NO = ", " '", situNo, "' ")

        Return SelectToShitsuString

    End Function

    ''' <summary>
    ''' ZONEマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="whCd">倉庫コード</param>
    ''' <param name="touNo">棟コード</param>
    ''' <param name="situNo">室コード</param>
    ''' <param name="zoneCd">ZONEコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectZoneListDataRow(ByVal brCd As String _
                                              , ByVal whCd As String _
                                              , ByVal touNo As String _
                                              , ByVal situNo As String _
                                              , ByVal zoneCd As String _
                                              ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TOU_SITU_ZONE).Select(Me.SelectZoneString(brCd, whCd, touNo, situNo, zoneCd))

    End Function

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
    ''' 単価マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="destDrs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custCdL">荷主コード(大)</param>
    ''' <param name="custCdM">荷主コード(中)</param>
    ''' <param name="tankMCd">単価マスタコード</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectTankaListDataRow(ByRef destDrs As DataRow() _
                                          , ByVal brCd As String _
                                              , ByVal custCdL As String _
                                              , ByVal custCdM As String _
                                              , ByVal tankMCd As String _
                                              , ByVal startDate As String _
                                          ) As Boolean

        'キャッシュテーブルからデータ抽出
        destDrs = Me.SelectTankaListDataRow(brCd, custCdL, custCdM, tankMCd, startDate)

        '取得できない場合、エラー
        If destDrs.Length < 1 Then
            Return Me.SetMstErrMessage("単価マスタ", tankMCd)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 単価マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custCdL">荷主コード(大)</param>
    ''' <param name="custCdM">荷主コード(中)</param>
    ''' <param name="tankMCd">単価マスタコード</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectTankaListDataRow(ByVal brCd As String _
                                              , ByVal custCdL As String _
                                              , ByVal custCdM As String _
                                              , ByVal tankMCd As String _
                                              , ByVal startDate As String _
                                              ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TANKA).Select(Me.SelectTankaString(brCd, custCdL, custCdM, tankMCd, startDate), "STR_DATE DESC")

    End Function

    ''' <summary>
    ''' 単価マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custCdL">荷主コード(大)</param>
    ''' <param name="custCdM">荷主コード(中)</param>
    ''' <param name="tankMCd">単価マスタコード</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectTankaString(ByVal brCd As String _
                                              , ByVal custCdL As String _
                                              , ByVal custCdM As String _
                                              , ByVal tankMCd As String _
                                              , ByVal startDate As String _
                                              ) As String

        SelectTankaString = String.Empty

        '削除フラグ
        SelectTankaString = String.Concat(SelectTankaString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectTankaString = String.Concat(SelectTankaString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '荷主コード(大)
        SelectTankaString = String.Concat(SelectTankaString, " AND ", "CUST_CD_L = ", " '", custCdL, "' ")

        '荷主コード(中)
        SelectTankaString = String.Concat(SelectTankaString, " AND ", "CUST_CD_M = ", " '", custCdM, "' ")

        '単価マスタコード
        SelectTankaString = String.Concat(SelectTankaString, " AND ", "UP_GP_CD_1 = ", " '", tankMCd, "' ")

        '適用開始日
        SelectTankaString = String.Concat(SelectTankaString, " AND ", "STR_DATE <= ", " '", startDate, "' ")

        Return SelectTankaString

    End Function

    ''' <summary>
    ''' 消防マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="destDrs">データロウ配列</param>
    ''' <param name="shoboCd">消防コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectShoboListDataRow(ByRef destDrs As DataRow() _
                                          , ByVal shoboCd As String _
                                          ) As Boolean

        'キャッシュテーブルからデータ抽出
        destDrs = Me.SelectShoboListDataRow(shoboCd)

        '取得できない場合、エラー
        If destDrs.Length < 1 Then
            Return Me.SetMstErrMessage("消防マスタ", shoboCd)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 消防マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="shoboCd">消防コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectShoboListDataRow(ByVal shoboCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SHOBO).Select(Me.SelectShoboString(shoboCd))

    End Function

    ''' <summary>
    ''' 消防マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="shoboCd">消防コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectShoboString(ByVal shoboCd As String) As String

        SelectShoboString = String.Empty

        '削除フラグ
        SelectShoboString = String.Concat(SelectShoboString, " SYS_DEL_FLG = '0' ")

        '消防コード
        SelectShoboString = String.Concat(SelectShoboString, " AND ", "SHOBO_CD = ", " '", shoboCd, "' ")

        Return SelectShoboString

    End Function

    ''' <summary>
    ''' 帳票パターンマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="ptnId">帳票ID</param>
    ''' <param name="ptnCd">帳票ID</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectRptListDataRow(ByVal brCd As String _
                                         , ByVal ptnId As String _
                                         , Optional ByVal ptnCd As String = "" _
                                         ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.RPT).Select(Me.SelectRptString(brCd, ptnId, ptnCd))

    End Function

    ''' <summary>
    '''帳票パターンマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="ptnID">帳票ID</param>
    ''' <param name="ptnCd">帳票ID</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectRptString(ByVal brCd As String _
                                     , ByVal ptnId As String _
                                     , Optional ByVal ptnCd As String = "" _
                                     ) As String

        SelectRptString = String.Empty

        '削除フラグ
        SelectRptString = String.Concat(SelectRptString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectRptString = String.Concat(SelectRptString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '帳票ID
        SelectRptString = String.Concat(SelectRptString, " AND ", "PTN_ID = ", " '", ptnId, "' ")

        '帳票パターンコード
        SelectRptString = String.Concat(SelectRptString, " AND ", "PTN_CD = ", " '", ptnCd, "' ")


        Return SelectRptString

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
        value = value.Replace(LMMControlC.ZENKAKU_SPACE, String.Empty)
        Return value

    End Function

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

    ''' <summary>
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Private Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

        If TypeOf ownControl Is T _
            OrElse ownControl.GetType.IsSubclassOf(GetType(T)) Then

            '指定されたクラスかその継承クラス
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub

    ''' <summary>
    ''' 2つの値を連結して設定
    ''' </summary>
    ''' <param name="value1">値1</param>
    ''' <param name="value2">値2</param>
    ''' <returns>編集後の値</returns>
    ''' <remarks></remarks>
    Friend Function EditConcatData(ByVal value1 As String, ByVal value2 As String, Optional ByVal str As String = " - ") As String

        EditConcatData = value1
        If String.IsNullOrEmpty(EditConcatData) = True Then

            EditConcatData = value2

        Else

            If String.IsNullOrEmpty(value2) = False Then

                EditConcatData = String.Concat(EditConcatData, str, value2)

            End If

        End If

        Return EditConcatData

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

#End Region

End Class
