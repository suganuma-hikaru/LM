' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI          : データ管理サブ
'  プログラムID     :  LMIControlV  : LMIValidate 共通処理
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMIControlValidateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMIControlV
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMIControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As Form, ByVal g As LMIControlG)

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

            Case LMIControlC.MASTEROPEN

                rtnResult = Me.SetFocusErrMessage(rtnResult)
                rtnResult = rtnResult AndAlso Me.IsFoucsValueChk(ctl, max, lblCtl, True)

            Case LMIControlC.ENTER

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
                                          , Optional ByVal msgType As LMIControlC.CustMsgType = LMIControlC.CustMsgType.CUST_L _
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
                                    , ByVal msgType As LMIControlC.CustMsgType _
                                    ) As String

        SetCustMsg = String.Empty

        Select Case msgType

            Case LMIControlC.CustMsgType.CUST_L

                SetCustMsg = custLCd

            Case LMIControlC.CustMsgType.CUST_M

                SetCustMsg = Me._G.EditConcatData(custLCd, custMCd)

            Case LMIControlC.CustMsgType.CUST_S

                SetCustMsg = Me._G.EditConcatData(custLCd, custMCd)
                SetCustMsg = Me._G.EditConcatData(SetCustMsg, custSCd)

            Case LMIControlC.CustMsgType.CUST_SS

                SetCustMsg = Me._G.EditConcatData(custLCd, custMCd)
                SetCustMsg = Me._G.EditConcatData(SetCustMsg, custSCd)
                SetCustMsg = Me._G.EditConcatData(SetCustMsg, custSSCd)

        End Select


        Return SetCustMsg

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <param name="kbnNm1">区分名1</param>
    ''' <param name="kbnNm2">区分名2</param>
    ''' <param name="kbnNm3">区分名3</param>
    ''' <param name="kbnNm4">区分名4</param>
    ''' <param name="kbnNm5">区分名5</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectKbnListDataRow(Optional ByVal kbnCd As String = "" _
                                         , Optional ByVal groupCd As String = "" _
                                         , Optional ByVal kbnNm1 As String = "" _
                                         , Optional ByVal kbnNm2 As String = "" _
                                         , Optional ByVal kbnNm3 As String = "" _
                                         , Optional ByVal kbnNm4 As String = "" _
                                         , Optional ByVal kbnNm5 As String = "" _
                                         ) As DataRow()

        Return Me._G.SelectKbnListDataRow(kbnCd, groupCd, kbnNm1, kbnNm2, kbnNm3, kbnNm4, kbnNm5)

    End Function

    ''' <summary>
    ''' EDI対象荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="inOutKbn"></param>
    ''' <param name="nrsBrCd"></param>
    ''' <param name="whCd"></param>
    ''' <param name="custCdL"></param>
    ''' <param name="custCdM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SelectEdiCustListDataRow(ByVal inOutKbn As String, _
                                             ByVal nrsBrCd As String, _
                                             ByVal whCd As String, _
                                             ByVal custCdL As String, _
                                             ByVal custCdM As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EDI_CUST).Select(Me.SelectEdiCustString(inOutKbn, nrsBrCd, whCd, custCdL, custCdM))

    End Function

    ''' <summary>
    ''' EDI対象荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="inOutKbn"></param>
    ''' <param name="nrsBrCd"></param>
    ''' <param name="whCd"></param>
    ''' <param name="custCdL"></param>
    ''' <param name="custCdM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectEdiCustString(ByVal inOutKbn As String, _
                                         ByVal nrsBrCd As String, _
                                         ByVal whCd As String, _
                                         ByVal custCdL As String, _
                                         ByVal custCdM As String) As String

        SelectEdiCustString = String.Empty

        '削除フラグ
        SelectEdiCustString = String.Concat(SelectEdiCustString, " SYS_DEL_FLG = '0' ")

        '入出荷区分
        SelectEdiCustString = String.Concat(SelectEdiCustString, " AND INOUT_KB = '", inOutKbn, "' ")

        '営業所コード
        SelectEdiCustString = String.Concat(SelectEdiCustString, " AND NRS_BR_CD =  '", nrsBrCd, "' ")

        '倉庫コード
        SelectEdiCustString = String.Concat(SelectEdiCustString, " AND WH_CD =  '", whCd, "' ")

        '荷主コード(大)
        SelectEdiCustString = String.Concat(SelectEdiCustString, " AND CUST_CD_L =  '", custCdL, "' ")

        If String.IsNullOrEmpty(custCdM) = True Then

            '荷主コード(中)
            SelectEdiCustString = String.Concat(SelectEdiCustString, " AND CUST_CD_M =  '00' ")
        Else
            '荷主コード(中)
            SelectEdiCustString = String.Concat(SelectEdiCustString, " AND CUST_CD_M =  '", custCdM, "' ")

        End If

        Return SelectEdiCustString

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)から指定した区分グループコードのデータを抽出する
    ''' </summary>
    ''' <param name="groupCd"></param>
    ''' <param name="kbnCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SelectKBNGroupListDataRow(ByVal groupCd As String _
                                        , Optional ByVal kbnCd As String = "") As DataRow()

        If (String.IsNullOrEmpty(groupCd)) Then
            Return New DataRow() {}
        End If

        Dim qry As System.Data.EnumerableRowCollection(Of DataRow) = _
            MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).AsEnumerable()

        If (qry IsNot Nothing) Then
            qry = qry.Where(Function(r) groupCd.Equals(r.Item("KBN_GROUP_CD")))
        End If

        If (String.IsNullOrEmpty(kbnCd) = False) Then
            qry = qry.Where(Function(row) kbnCd.Equals(row.Item("KBN_CD")))
        End If

        Return qry.OrderBy(Function(row) row.Item("SORT")).ToArray()

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
        value = value.Replace(LMIControlC.ZENKAKU_SPACE, String.Empty)
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

#Region "SPREAD関連"

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
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <param name="defNo">チェックボックスセルのカラム№</param>
    ''' <param name="sprDetail">対象スプレッド</param>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Overloads Function SprSelectList(ByVal defNo As Integer, ByVal sprDetail As Spread.LMSpreadSearch) As ArrayList

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
    ''' 未選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectChk(ByVal chkCnt As Integer) As Boolean

        'チェック件数が0件
        If chkCnt = 0 Then

            Return False

        End If

        Return True

    End Function


    'START KIM 20121016 特定荷主対応（ハネウェル）
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
    ''' Enter押下イベントの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetEnterEvent(ByVal frm As LMFormSxga)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ENTER時にセルを右移動させる
        Call Me.SetSpreadEnterEvent(frm)

    End Sub

    ''' <summary>
    ''' Spread上でのEnter押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSpreadEnterEvent(ByVal frm As LMFormSxga)

        'フォーム内のSpreadを取得
        Dim arr As ArrayList = New ArrayList()
        arr = New ArrayList()
        Me.GetTarget(Of Win.Spread.LMSpread)(arr, frm)
        Dim im As New FarPoint.Win.Spread.InputMap

        For Each spr As Win.Spread.LMSpread In arr

            ' 非編集セルでの[Enter]キーを「次列へ移動」とします
            im = spr.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)

            '編集中セルでの[Enter]キーを「次列へ移動」とします
            im = spr.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)

        Next

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

    'END KIM 20121016 特定荷主対応（ハネウェル）

#End Region

End Class
