' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF          : 運送サブ
'  プログラムID     :  LMFControlV  : LMFValidate 共通処理
'  作  成  者       :  [ito]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMFControlValidateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMFControlV
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMFControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">フォームクラス</param>
    ''' <param name="g"></param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Public Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As Form, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        '共通画面クラス
        Me._G = g

    End Sub

#End Region

#Region "共通入力チェック"

    ''' <summary>
    ''' 未選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Public Function IsSelectChk(ByVal chkCnt As Integer) As Boolean

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
    ''' 選択上限チェック
    ''' </summary>
    ''' <param name="count">選択件数</param>
    ''' <param name="kbn">区分コード</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Public Function IsSelectLimitChk(ByVal count As Integer, ByVal kbn As String) As Boolean

        Dim limitData As Integer = Me._G.GetLimitData(kbn, LMKbnConst.KBN_I004)

        '選択した件数が多い場合、エラー
        If limitData < count Then
            Return Me.SetErrMessage("E168", New String() {count.ToString(), limitData.ToString()})
        End If

        Return True

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
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="ctl">コントロール配列</param>
    ''' <param name="msg">置換文字配列</param>
    ''' <param name="lblCtl">ラベルコントロール配列</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Public Function IsFocusChk(ByVal actionType As String _
                                   , ByVal ctl As Win.InputMan.LMImTextBox() _
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

            Case LMFControlC.MASTEROPEN

                rtnResult = Me.SetFocusErrMessage(rtnResult)
                rtnResult = rtnResult AndAlso Me.IsFoucsValueChk(ctl, max, lblCtl, True)

            Case LMFControlC.ENTER

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
    Private Function IsFoucsReadOnlyChk(ByVal ctl As Win.InputMan.LMImTextBox(), ByVal max As Integer) As Boolean

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
    Private Function IsFoucsValueChk(ByVal ctl As Win.InputMan.LMImTextBox() _
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
    Public Function IsKinsiByteChk(ByVal ctl As Win.InputMan.LMImTextBox, ByVal msg As String, ByVal keta As Integer) As Boolean

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
    Public Function IsKinsiByteChk(ByVal vCell As LMValidatableCells, ByVal rowNo As Integer, ByVal colNo As Integer, ByVal msg As String, ByVal keta As Integer) As Boolean

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
    ''' 日付絞込の関連必須チェック
    ''' </summary>
    ''' <param name="cmbDate">絞込コンボ</param>
    ''' <param name="fromDate">日付From</param>
    ''' <param name="toDate">日付To</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Public Function IsDateCtlConnectionChk(ByVal cmbDate As Win.InputMan.LMComboKubun _
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
    Public Function IsDateFromToChk(ByVal fromDate As Win.InputMan.LMImDate _
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
    Public Function IsAuthorityChk(ByVal kengenFlg As Boolean) As Boolean

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

    '(2012.09.06)START UMANO 要望番号1410
    ''' <summary>
    ''' 納入日,運送会社の同値チェック
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="arr">リスト</param>
    ''' <param name="nonyuCol">納入日の列番号</param>
    ''' <param name="unsoCol">運送会社コードの列番号</param>
    ''' <param name="unsoBrCol">運送会社支店コードの列番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsDotiChk(ByVal spr As Win.Spread.LMSpread _
                              , ByVal arr As ArrayList _
                              , ByVal nonyuCol As Integer _
                              , ByVal unsoCol As Integer _
                              , ByVal unsoBrCol As Integer _
                              , Optional ByVal unsoFlg As Boolean = True) As Boolean

        With spr.ActiveSheet

            Dim max As Integer = arr.Count - 1

            'arrがない場合、スルー
            If max < 0 Then
                Return True
            End If

            Dim rowNo As Integer = Convert.ToInt32(arr(0))

            '納入日の必須チェック
            Dim inDate As String = Jp.Co.Nrs.Win.Utility.DateFormatUtility.DeleteSlash(Me._G.GetCellValue(.Cells(rowNo, nonyuCol)))
            '2016.01.06 UMANO 英語化対応START
            'Dim chk As Boolean = Me.IsDotiHissuChk(inDate, LMFControlC.NONYU)
            Dim chk As Boolean = Me.IsDotiHissuChk(inDate, .GetColumnLabel(0, nonyuCol))
            '2016.01.06 UMANO 英語化対応END

            '運送会社コードの必須チェック
            Dim unsoCd As String = Me._G.GetCellValue(.Cells(rowNo, unsoCol))
            '2016.01.06 UMANO 英語化対応START
            'chk = chk AndAlso Me.IsDotiHissuChk(unsoCd, String.Concat(LMFControlC.UNSOCO, LMFControlC.CD))
            chk = chk AndAlso Me.IsDotiHissuChk(unsoCd, .GetColumnLabel(0, unsoCol))
            '2016.01.06 UMANO 英語化対応END

            '運送会社支店コードの必須チェック
            Dim unsoBrCd As String = Me._G.GetCellValue(.Cells(rowNo, unsoBrCol))
            '2016.01.06 UMANO 英語化対応START
            'chk = chk AndAlso Me.IsDotiHissuChk(unsoBrCd, String.Concat(LMFControlC.UNSOCO, LMFControlC.BR_CD))
            chk = chk AndAlso Me.IsDotiHissuChk(unsoBrCd, .GetColumnLabel(0, unsoBrCol))
            '2016.01.06 UMANO 英語化対応END

            'エラーの場合、終了
            If chk = False Then
                Return chk
            End If

            For i As Integer = 1 To max

                rowNo = Convert.ToInt32(arr(i))

                '納入日が違う場合、エラー
                If inDate.Equals(Jp.Co.Nrs.Win.Utility.DateFormatUtility.DeleteSlash(Me._G.GetCellValue(.Cells(rowNo, nonyuCol)))) = False Then
                    '2016.01.06 UMANO 英語化対応START
                    'Return Me.SetErrMessageE227(LMFControlC.NONYU)
                    Return Me.SetErrMessageE227(.GetColumnLabel(0, nonyuCol))
                    '2016.01.06 UMANO 英語化対応END
                End If

                If unsoFlg = True Then

                    '運送会社が違う場合、エラー
                    If unsoCd.Equals(Me._G.GetCellValue(.Cells(rowNo, unsoCol))) = False Then
                        '2016.01.06 UMANO 英語化対応START
                        'Return Me.SetErrMessageE227(LMFControlC.UNSOCO)
                        Return Me.SetErrMessageE227(.GetColumnLabel(0, unsoCol))
                        '2016.01.06 UMANO 英語化対応END
                    End If

                    '運送会社支店が違う場合、エラー
                    If unsoBrCd.Equals(Me._G.GetCellValue(.Cells(rowNo, unsoBrCol))) = False Then
                        '2016.01.06 UMANO 英語化対応START
                        'Return Me.SetErrMessageE227(LMFControlC.UNSOCO)
                        Return Me.SetErrMessageE227(.GetColumnLabel(0, unsoCol))
                        '2016.01.06 UMANO 英語化対応END
                    End If

                End If

            Next

            Return True

        End With

    End Function
    '(2012.09.06)END UMANO 要望番号1410

    ''' <summary>
    ''' 同値チェック時の必須チェック
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Public Function IsDotiHissuChk(ByVal value As String, ByVal msg As String) As Boolean

        If String.IsNullOrEmpty(value) = True Then
            Return Me.SetErrMessage("E001", New String() {msg})
        End If

        Return True

    End Function

    ''' <summary>
    ''' 同値チェック時の必須チェック
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Public Function IsDotiHissuChk(ByVal value As String(), ByVal msg As String) As Boolean

        Dim max As Integer = value.Count - 1
        For i As Integer = 0 To max

            If String.IsNullOrEmpty(value(i)) = False Then
                Return True
            End If

        Next

        Return Me.SetErrMessage("E001", New String() {msg})

    End Function

    ''' <summary>
    ''' 一括処理時のDataTableを設定
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="rtnResult">チェック結果</param>
    ''' <param name="rowNo">ループのカウンタ変数</param>
    ''' <remarks></remarks>
    Public Sub SetErrDt(ByVal dt As DataTable, ByVal dr As DataRow, ByVal rtnResult As Boolean, ByVal rowNo As Integer)

        'エラーがある場合、リストの番号を保持
        If rtnResult = False Then

            dr = dt.NewRow()
            dr.Item("ROW_NO") = rowNo
            dt.Rows.Add(dr)

        End If

    End Sub

    ''' <summary>
    ''' 一括処理時のリストを削除
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>リスト</returns>
    ''' <remarks></remarks>
    Public Function SetErrDt(ByVal dt As DataTable, ByVal arr As ArrayList) As ArrayList

        'エラーになったものを削除
        Dim max As Integer = dt.Rows.Count - 1
        Dim recNo As Integer = 0
        For i As Integer = max To 0 Step -1

            recNo = Convert.ToInt32(dt.Rows(i).Item("ROW_NO").ToString())
            dt.Rows(i).Item("ROW_NO") = arr(recNo)
            arr.RemoveAt(recNo)

        Next

        Return arr

    End Function

#End Region

#Region "エラーメッセージ設定"

    ''' <summary>
    ''' メッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Public Function SetErrMessage(ByVal id As String) As Boolean

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
    Public Function SetErrMessage(ByVal id As String, ByVal msg As String()) As Boolean

        MyBase.ShowMessage(id, msg)
        Return False

    End Function

    ''' <summary>
    ''' E227メッセージを設定
    ''' </summary>
    ''' <param name="msg">置換文字</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetErrMessageE227(ByVal msg As String) As Boolean

        Return Me.SetErrMessage("E227", New String() {msg})

    End Function

    ''' <summary>
    ''' フォーカス位置エラーのメッセージ設定
    ''' </summary>
    ''' <param name="chk">判定フラグ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Public Function SetFocusErrMessage(ByVal chk As Boolean) As Boolean

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
    Public Function SetMstErrMessage(ByVal value1 As String, ByVal value2 As String) As Boolean

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

        '2016.01.06 UMANO 英語化対応START
        'Return Me.SetMstErrMessage("運賃タリフマスタ", tariffCd)
        Return Me.SetErrMessage("E690", New String() {tariffCd})
        '2016.01.06 UMANO 英語化対応END

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ存在チェックエラーメッセージ
    ''' </summary>
    ''' <param name="tariffCd">タリフコード</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetYokoTariffExistErr(ByVal tariffCd As String) As Boolean

        '2016.01.06 UMANO 英語化対応START
        'Return Me.SetMstErrMessage("横持ちタリフマスタ", tariffCd)
        Return Me.SetErrMessage("E691", New String() {tariffCd})
        '2016.01.06 UMANO 英語化対応END

    End Function

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 支払タリフマスタ存在チェックエラーメッセージ
    ''' </summary>
    ''' <param name="tariffCd">タリフコード</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetShiharaiTariffExistErr(ByVal tariffCd As String) As Boolean

        '2016.01.06 UMANO 英語化対応START
        'Return Me.SetMstErrMessage("支払タリフマスタ", tariffCd)
        Return Me.SetErrMessage("E695", New String() {tariffCd})
        '2016.01.06 UMANO 英語化対応END

    End Function

    ''' <summary>
    ''' 支払横持ちタリフマスタ存在チェックエラーメッセージ
    ''' </summary>
    ''' <param name="tariffCd">タリフコード</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetShiharaiYokoTariffExistErr(ByVal tariffCd As String) As Boolean

        '2016.01.06 UMANO 英語化対応START
        'Return Me.SetMstErrMessage("支払横持ちタリフマスタ", tariffCd)
        Return Me.SetErrMessage("E696", New String() {tariffCd})
        '2016.01.06 UMANO 英語化対応END

    End Function

    ''' <summary>
    ''' 支払先マスタ存在チェックエラーメッセージ
    ''' </summary>
    ''' <param name="shiharaitoCd">支払先コード</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetShiharaitoExistErr(ByVal shiharaitoCd As String) As Boolean

        '2016.01.06 UMANO 英語化対応START
        'Return Me.SetMstErrMessage("支払先マスタ", shiharaitoCd)
        Return Me.SetErrMessage("E880", New String() {shiharaitoCd})
        '2016.01.06 UMANO 英語化対応END

    End Function

    ''' <summary>
    ''' 支払割増マスタ存在チェックエラーメッセージ
    ''' </summary>
    ''' <param name="extcCd">支払割増コード</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetShiharaiExtcExistErr(ByVal extcCd As String) As Boolean

        '2016.01.06 UMANO 英語化対応START
        'Return Me.SetMstErrMessage("支払割増タリフマスタ", extcCd)
        Return Me.SetErrMessage("E697", New String() {extcCd})
        '2016.01.06 UMANO 英語化対応END

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#Region "エラーコントロール設定"

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <param name="tab">タブ　初期値 = Nothing</param>
    ''' <param name="tabPage">タブページ　初期値 = Nothing</param>
    ''' <remarks></remarks>
    Public Sub SetErrorControl(ByVal ctl As Control _
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
    Public Sub TrimSpaceSprTextvalue(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer)

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
                    .SetCellValue(rowNo, i, Me._G.GetCellValue(.ActiveSheet.Cells(rowNo, i)))
                End If

            Next

        End With

    End Sub

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
    Public Function SelectCustListDataRow(ByRef custDrs As DataRow() _
                                          , ByVal brCd As String _
                                          , ByVal custLCd As String _
                                          , Optional ByVal custMCd As String = "" _
                                          , Optional ByVal custSCd As String = "" _
                                          , Optional ByVal custSSCd As String = "" _
                                          , Optional ByVal msgType As LMFControlC.CustMsgType = LMFControlC.CustMsgType.CUST_L _
                                          ) As Boolean

        custDrs = Me._G.SelectCustListDataRow(brCd, custLCd, custMCd, custSCd, custSSCd)

        If custDrs.Length < 1 Then
            '2016.01.06 UMANO 英語化対応START
            'Return Me.SetMstErrMessage("荷主マスタ", Me.SetCustMsg(custLCd, custMCd, custSCd, custSSCd, msgType))
            Return Me.SetErrMessage("E767", New String() {Me.SetCustMsg(custLCd, custMCd, custSCd, custSSCd, msgType)})
            '2016.01.06 UMANO 英語化対応END
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
                                    , ByVal msgType As LMFControlC.CustMsgType _
                                    ) As String

        SetCustMsg = String.Empty

        Select Case msgType

            Case LMFControlC.CustMsgType.CUST_L

                SetCustMsg = custLCd

            Case LMFControlC.CustMsgType.CUST_M

                SetCustMsg = Me._G.EditConcatData(custLCd, custMCd)

            Case LMFControlC.CustMsgType.CUST_S

                SetCustMsg = Me._G.EditConcatData(custLCd, custMCd)
                SetCustMsg = Me._G.EditConcatData(SetCustMsg, custSCd)

            Case LMFControlC.CustMsgType.CUST_SS

                SetCustMsg = Me._G.EditConcatData(custLCd, custMCd)
                SetCustMsg = Me._G.EditConcatData(SetCustMsg, custSCd)
                SetCustMsg = Me._G.EditConcatData(SetCustMsg, custSSCd)

        End Select


        Return SetCustMsg

    End Function

    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="unsoDrs">データロウ配列</param>
    ''' <param name="unsoCd">運送会社コード</param>
    ''' <param name="unsoBrCd">運送会社支店コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnsocoListDataRow(ByRef unsoDrs As DataRow() _
                                            , ByVal brCd As String _
                                            , ByVal unsoCd As String _
                                            , Optional ByVal unsoBrCd As String = "" _
                                            ) As Boolean

        unsoDrs = Me._G.SelectUnsocoListDataRow(brCd, unsoCd, unsoBrCd)

        If unsoDrs.Length < 1 Then
            '2016.01.06 UMANO 英語化対応START
            'Return Me.SetMstErrMessage("運送会社マスタ", String.Concat(unsoCd, " - ", unsoBrCd))
            Return Me.SetErrMessage("E829", New String() {String.Concat(unsoCd, " - ", unsoBrCd)})
            '2016.01.06 UMANO 英語化対応END
        End If

        Return True

    End Function

    ''' <summary>
    ''' 割増タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="extcDrs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="extcCd">割増タリフコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectExtcUnchinListDataRow(ByRef extcDrs As DataRow() _
                                                  , ByVal brCd As String _
                                                  , ByVal extcCd As String _
                                                  , Optional ByVal jisCd As String = "0000000" _
                                                  ) As Boolean

        'キャッシュテーブルからデータ抽出
        extcDrs = Me._G.SelectExtcUnchinListDataRow(brCd, extcCd, jisCd)

        '取得できない場合、エラー
        If extcDrs.Length < 1 Then
            '2016.01.06 UMANO 英語化対応START
            'Return Me.SetMstErrMessage("割増タリフマスタ", extcCd)
            Return Me.SetErrMessage("E693", New String() {extcCd})
            '2016.01.06 UMANO 英語化対応END
        End If

        Return True

    End Function

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 支払割増タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="extcDrs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="extcCd">割増タリフコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectExtcShiharaiListDataRow(ByRef extcDrs As DataRow() _
                                                  , ByVal brCd As String _
                                                  , ByVal extcCd As String _
                                                  , Optional ByVal jisCd As String = "0000000" _
                                                  ) As Boolean

        'キャッシュテーブルからデータ抽出
        extcDrs = Me._G.SelectExtcShiharaiListDataRow(brCd, extcCd, jisCd)

        '取得できない場合、エラー
        If extcDrs.Length < 1 Then
            '2016.01.06 UMANO 英語化対応START
            'Return Me.SetMstErrMessage("支払割増タリフマスタ", extcCd)
            Return Me.SetErrMessage("E697", New String() {extcCd})
            '2016.01.06 UMANO 英語化対応END
        End If

        Return True

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    ''' <summary>
    ''' 運賃タリフセットマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="tariffKbn">タリフ分類区分</param>
    ''' <param name="tariffCd">タリフコード</param>
    ''' <param name="setCd">セットマスタコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks>抽出条件(タリフコード)を判定して取得する。</remarks>
    Friend Function SelectTariffSetListDataRow(ByVal brCd As String _
                                               , ByVal custLCd As String _
                                               , ByVal custMCd As String _
                                               , ByVal tariffKbn As String _
                                               , ByVal tariffCd As String _
                                               , Optional ByVal setCd As String = "" _
                                               ) As DataRow()

        Dim tariffCd1 As String = String.Empty
        Dim tariffCd2 As String = String.Empty
        Dim yokoCd As String = String.Empty

        Select Case tariffKbn

            Case LMFControlC.TARIFF_KURUMA

                tariffCd2 = tariffCd

            Case LMFControlC.TARIFF_YOKO

                yokoCd = tariffCd

            Case Else

                tariffCd1 = tariffCd

        End Select

        'キャッシュテーブルからデータ抽出
        Return Me._G.SelectTariffSetListDataRow(brCd, custLCd, custMCd, tariffKbn, tariffCd1, tariffCd2, yokoCd, setCd)

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffDrs">データロウ配列</param>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnchinTariffListDataRow(ByRef tariffDrs As DataRow() _
                                                  , ByVal tariffCd As String _
                                                  , Optional ByVal tariffCdEda As String = "" _
                                                  , Optional ByVal startDate As String = "" _
                                                  ) As Boolean

        'キャッシュテーブルからデータ抽出
        tariffDrs = Me._G.SelectUnchinTariffListDataRow(tariffCd, tariffCdEda, startDate)

        '取得できない場合、エラー
        If tariffDrs.Length < 1 Then
            Return Me.SetUnchinTariffExistErr(tariffCd)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffDrs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="tariffCd">横持ちタリフコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    ''' 
    Public Function SelectYokoTariffListDataRow(ByRef tariffDrs As DataRow(), ByVal brCd As String, ByVal tariffCd As String) As Boolean

        'キャッシュテーブルからデータ抽出
        tariffDrs = Me._G.SelectYokoTariffListDataRow(brCd, tariffCd)

        '取得できない場合、エラー
        If tariffDrs.Length < 1 Then
            Return Me.SetYokoTariffExistErr(tariffCd)
        End If

        Return True

    End Function

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 支払タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffDrs">データロウ配列</param>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectShiharaiTariffListDataRow(ByRef tariffDrs As DataRow() _
                                                  , ByVal tariffCd As String _
                                                  , Optional ByVal tariffCdEda As String = "" _
                                                  , Optional ByVal startDate As String = "" _
                                                  ) As Boolean

        'キャッシュテーブルからデータ抽出
        tariffDrs = Me._G.SelectShiharaiTariffListDataRow(tariffCd, tariffCdEda, startDate)

        '取得できない場合、エラー
        If tariffDrs.Length < 1 Then
            Return Me.SetShiharaiTariffExistErr(tariffCd)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 支払横持ちタリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffDrs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="tariffCd">横持ちタリフコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    ''' 
    Friend Function SelectShiharaiYokoTariffListDataRow(ByRef tariffDrs As DataRow(), ByVal brCd As String, ByVal tariffCd As String) As Boolean

        'キャッシュテーブルからデータ抽出
        tariffDrs = Me._G.SelectShiharaiYokoTariffListDataRow(brCd, tariffCd)

        '取得できない場合、エラー
        If tariffDrs.Length < 1 Then
            Return Me.SetShiharaiYokoTariffExistErr(tariffCd)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 支払先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="shiharaiDrs">データロウ配列</param>
    ''' <param name="shiharaitoCd">支払先コード</param>
     ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectShiharaitoListDataRow(ByRef shiharaiDrs As DataRow() _
                                                  , ByVal shiharaitoCd As String _
                                                  ) As Boolean

        'キャッシュテーブルからデータ抽出
        shiharaiDrs = Me._G.SelectShiharaitoListDataRow(shiharaitoCd)

        '取得できない場合、エラー
        If shiharaiDrs.Length < 1 Then
            Return Me.SetShiharaitoExistErr(shiharaitoCd)
        End If

        Return True

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    ''' <summary>
    ''' 距離程マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kyoriDrs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="origJis">発地JISコード</param>
    ''' <param name="destJis">届先JISコード</param>
    ''' <param name="kyoriCd">距離程コード　初期値 = "000"</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectKyoriListDataRow(ByRef kyoriDrs As DataRow() _
                                          , ByVal brCd As String _
                                          , ByVal origJis As String _
                                          , ByVal destJis As String _
                                          , Optional ByVal kyoriCd As String = "000" _
                                          ) As Boolean

        'キャッシュテーブルからデータ抽出
        kyoriDrs = Me.SelectKyoriListDataRow(brCd, origJis, destJis, kyoriCd)

        '取得できない場合、エラー
        If kyoriDrs.Length < 1 Then
            '2016.01.06 UMANO 英語化対応START
            'Return Me.SetMstErrMessage("距離程マスタ", kyoriCd)
            Return Me.SetErrMessage("E857", New String() {kyoriCd})
            '2016.01.06 UMANO 英語化対応END
        End If

        Return True

    End Function

    ''' <summary>
    ''' 距離程マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="origJis">発地JISコード</param>
    ''' <param name="destJis">届先JISコード</param>
    ''' <param name="kyoriCd">距離程コード　初期値 = "000"</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectKyoriListDataRow(ByVal brCd As String _
                                          , ByVal origJis As String _
                                          , ByVal destJis As String _
                                          , Optional ByVal kyoriCd As String = "000" _
                                          ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KYORI).Select(Me.SelectKyoriString(brCd, origJis, destJis, kyoriCd))

    End Function

    ''' <summary>
    ''' 距離程マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="origJis">発地JISコード</param>
    ''' <param name="destJis">届先JISコード</param>
    ''' <param name="kyoriCd">距離程コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectKyoriString(ByVal brCd As String _
                                       , ByVal origJis As String _
                                       , ByVal destJis As String _
                                       , ByVal kyoriCd As String _
                                       ) As String

        SelectKyoriString = String.Empty

        '削除フラグ
        SelectKyoriString = String.Concat(SelectKyoriString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectKyoriString = String.Concat(SelectKyoriString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '距離程コード
        SelectKyoriString = String.Concat(SelectKyoriString, " AND ", "KYORI_CD = ", " '", kyoriCd, "' ")


        '大小比較で発地と届先を判定
        If Convert.ToInt32(Me._G.FormatNumValue(destJis)) < Convert.ToInt32(Me._G.FormatNumValue(origJis)) Then

            'OrigJISを退避
            Dim value As String = origJis

            '入替
            origJis = destJis
            destJis = value

        End If

        '発地JISコード
        SelectKyoriString = String.Concat(SelectKyoriString, " AND ", "ORIG_JIS_CD = ", " '", origJis, "' ")

        '届先JISコード
        SelectKyoriString = String.Concat(SelectKyoriString, " AND ", "DEST_JIS_CD = ", " '", destJis, "' ")

        Return SelectKyoriString

    End Function

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
            '2016.01.06 UMANO 英語化対応START
            'Return Me.SetMstErrMessage("届先マスタ", destCd)
            Return Me.SetErrMessage("E827", New String() {destCd})
            '2016.01.06 UMANO 英語化対応END
        End If

        Return True

    End Function

    '(2013.03.29)エラーメッセージを設定しないチェックPG追加 -- START --
    ''' <summary>
    ''' 届先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="destDrs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custCdL">荷主(大)コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectDestListDataRow_NoMsg(ByRef destDrs As DataRow() _
                                              , ByVal brCd As String _
                                              , ByVal custCdL As String _
                                              , ByVal destCd As String _
                                               ) As Boolean

        'キャッシュテーブルからデータ抽出
        destDrs = Me.SelectDestListDataRow(brCd, custCdL, destCd)

        '取得できない場合、エラー
        If destDrs.Length < 1 Then
            Return False
        End If

        Return True

    End Function
    '(2013.03.29)エラーメッセージを設定しないチェックPG追加 --  END  --

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

        '届先JISコード
        SelectDestString = String.Concat(SelectDestString, " AND ", "DEST_CD = ", " '", destCd, "' ")

        Return SelectDestString

    End Function

    ''' <summary>
    ''' エリアマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="areaDrs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="areaCd">エリアコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectAreaListDataRow(ByRef areaDrs As DataRow() _
                                          , ByVal brCd As String _
                                          , ByVal areaCd As String _
                                          , ByVal jisCd As String _
                                          , ByVal binKb As String _
                                          ) As Boolean


        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        'Friend Function SelectAreaListDataRow(ByRef areaDrs As DataRow() _
        '                                      , ByVal brCd As String _
        '                                      , ByVal areaCd As String _
        '                                      , ByVal jisCd As String _
        '                                      ) As Boolean
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End


        'キャッシュテーブルからデータ抽出
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        'areaDrs = Me.SelectAreaListDataRow(brCd, areaCd, jisCd)
        areaDrs = Me.SelectAreaListDataRow(brCd, areaCd, jisCd, binKb)
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

        '取得できない場合、エラー
        If areaDrs.Length < 1 Then
            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
            'Return Me.SetMstErrMessage("エリアマスタ", Me._G.EditConcatData(areaCd, jisCd))
            '2016.01.06 UMANO 英語化対応START
            'Return Me.SetMstErrMessage("エリアマスタ", String.Concat(areaCd, "-", binKb, "-", jisCd))
            Return Me.SetErrMessage("E881", New String() {String.Concat(areaCd, "-", binKb, "-", jisCd)})
            '2016.01.06 UMANO 英語化対応END
            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End
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
                                          , ByVal binKb As String _
                                          ) As DataRow()

        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        'Friend Function SelectAreaListDataRow(ByVal brCd As String _
        '                                      , ByVal areaCd As String _
        '                                      , ByVal jisCd As String _
        '                                      ) As DataRow()
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End


        'キャッシュテーブルからデータ抽出
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        'Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.AREA_JIS).Select(Me.SelectAreaString(brCd, areaCd, jisCd))
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.AREA_JIS).Select(Me.SelectAreaString(brCd, areaCd, jisCd, binKb))
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

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
                                      , ByVal binKb As String _
                                      ) As String

        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        'Private Function SelectAreaString(ByVal brCd As String _
        '                                  , ByVal areaCd As String _
        '                                  , ByVal jisCd As String _
        '                                  ) As String
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End



        SelectAreaString = String.Empty

        '営業所コード
        SelectAreaString = String.Concat(SelectAreaString, "NRS_BR_CD = ", " '", brCd, "' ")

        'エリアコード
        SelectAreaString = String.Concat(SelectAreaString, " AND ", "AREA_CD = ", " '", areaCd, "' ")


        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        '便区分
        SelectAreaString = String.Concat(SelectAreaString, " AND ", "BIN_KB = ", " '", binKb, "' ")
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End


        If String.IsNullOrEmpty(jisCd) = False Then

            'JISコード
            SelectAreaString = String.Concat(SelectAreaString, " AND ", "JIS_CD = ", " '", jisCd, "' ")

        End If

        Return SelectAreaString

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

    ''' <summary>
    ''' メニューマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="subId">サブシステムID</param>
    ''' <param name="pgId">プログラムID</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectMenyListDataRow(ByVal subId As String, ByVal pgId As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.MENU).Select(Me.SelectMenyString(subId, pgId))

    End Function

    ''' <summary>
    ''' メニューマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="subId">サブシステムID</param>
    ''' <param name="pgId">プログラムID</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectMenyString(ByVal subId As String, ByVal pgId As String) As String

        SelectMenyString = String.Empty

        'サブシステムID
        SelectMenyString = String.Concat(SelectMenyString, " SUB_SYSTEM_ID = ", " '", subId, "' ")

        'プログラムID
        SelectMenyString = String.Concat(SelectMenyString, " AND ", "FORM_ID = ", " '", pgId, "' ")

        Return SelectMenyString

    End Function

    ''' <summary>
    ''' 車輌マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="carDrs">DataRow配列</param>
    ''' <param name="carNo">車輌番号</param>
    ''' <param name="carKey">車輌キー</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function SelectCarListDataRow(ByRef carDrs As DataRow(), Optional ByVal carNo As String = "", Optional ByVal carKey As String = "") As Boolean

        carDrs = Me.SelectCarListDataRow(carNo, carKey)

        If carDrs.Length < 1 Then

            '2016.01.06 UMANO 英語化対応START
            'Return Me.SetMstErrMessage("車輌マスタ", Me._G.EditConcatData(carKey, carNo))
            Return Me.SetErrMessage("E882", New String() {Me._G.EditConcatData(carKey, carNo)})
            '2016.01.06 UMANO 英語化対応END

        End If

        Return True

    End Function

    ''' <summary>
    ''' 車輌マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="carNo">車輌番号</param>
    ''' <param name="carKey">車輌キー</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectCarListDataRow(Optional ByVal carNo As String = "", Optional ByVal carKey As String = "") As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.VCLE).Select(Me.SelectCarString(carNo, carKey))

    End Function

    ''' <summary>
    ''' 車輌マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="carNo">車輌番号</param>
    ''' <param name="carKey">車輌キー</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectCarString(ByVal carNo As String, ByVal carKey As String) As String

        SelectCarString = String.Empty

        '削除フラグ
        SelectCarString = " SYS_DEL_FLG = '0' "

        If String.IsNullOrEmpty(carNo) = False Then

            '車輌番号
            SelectCarString = String.Concat(SelectCarString, " AND CAR_NO = ", " '", carNo, "' ")

        End If

        If String.IsNullOrEmpty(carKey) = False Then

            '車輌キー
            SelectCarString = String.Concat(SelectCarString, " AND CAR_KEY = ", " '", carKey, "' ")

        End If

        Return SelectCarString

    End Function

    ''' <summary>
    ''' 乗務員マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="driverDrs">DataRow配列</param>
    ''' <param name="driverCd">乗務員コード</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function SelectDriverListDataRow(ByRef driverDrs As DataRow(), ByVal driverCd As String) As Boolean

        driverDrs = Me._G.SelectDriverListDataRow(driverCd)

        If driverDrs.Length < 1 Then

            '2016.01.06 UMANO 英語化対応START
            'Return Me.SetMstErrMessage("乗務員マスタ", driverCd)
            Return Me.SetErrMessage("E883", New String() {driverCd})
            '2016.01.06 UMANO 英語化対応END

        End If

        Return True

    End Function

    ''' <summary>
    ''' 請求先タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="seiqtoDrs">DataRow配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="seqtoCd">請求先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Public Function SelectSeiqtoListDataRow(ByVal seiqtoDrs As DataRow(), ByVal brCd As String, ByVal seqtoCd As String) As Boolean

        seiqtoDrs = Me._G.SelectSeiqtoListDataRow(brCd, seqtoCd)

        If seiqtoDrs.Length < 1 Then

            '2016.01.06 UMANO 英語化対応START
            'Return Me.SetMstErrMessage("請求先マスタ", seqtoCd)
            Return Me.SetErrMessage("E830", New String() {seqtoCd})
            '2016.01.06 UMANO 英語化対応END

        End If

        Return True

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' vbCrLf,"　"を空文字に変換
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns>値</returns>
    ''' <remarks></remarks>
    Public Function SetRepMsgData(ByVal value As String) As String

        value = value.Replace(vbCrLf, String.Empty)
        value = value.Replace(LMFControlC.ZENKAKU_SPACE, String.Empty)
        Return value

    End Function

    ''' <summary>
    ''' 値クリア
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Sub ClearControl(ByVal ctl As Control)

        If TypeOf ctl Is Win.InputMan.LMImCombo = True Then

            DirectCast(ctl, Win.InputMan.LMImCombo).SelectedValue = Nothing

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            DirectCast(ctl, Win.InputMan.LMComboKubun).SelectedValue = Nothing

        ElseIf TypeOf ctl Is Win.InputMan.LMComboNrsBr = True Then

            DirectCast(ctl, Win.InputMan.LMComboNrsBr).SelectedValue = Nothing

        ElseIf TypeOf ctl Is Win.InputMan.LMComboSoko = True Then

            DirectCast(ctl, Win.InputMan.LMComboSoko).SelectedValue = Nothing

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            DirectCast(ctl, Win.InputMan.LMImNumber).Value = 0

        ElseIf TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).TextValue = String.Empty

        ElseIf TypeOf ctl Is Win.InputMan.LMImDate = True Then

            DirectCast(ctl, Win.InputMan.LMImDate).Value = Nothing

        End If

    End Sub

#End Region

    '(2013.01.17)要望番号1617 -- START --
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

#End Region
    '(2013.01.17)要望番号1617 --  END  --

End Class
