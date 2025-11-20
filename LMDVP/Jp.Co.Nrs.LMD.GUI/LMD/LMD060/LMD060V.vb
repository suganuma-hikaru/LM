' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMM     : 在庫サブシステム
'  プログラムID     :  LMD060V : 
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMD060Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMD060V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD060F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMDControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD060F, ByVal v As LMDControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。

        Me.MyHandler = handlerClass
        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

    End Sub

#End Region 'Constructor

#Region "チェックコントロール"

    ''' <summary>
    ''' 入力チェック（検索）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue(LMD060C.ActionType.KENSAKU)

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsHeaderChk()
        rtnResult = rtnResult AndAlso Me.IsSpreadInputChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 入力チェック（実行）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsJikkouChk(ByVal nowDate As String) As Boolean

        With Me._Frm

            '未選択チェック
            Dim rtnResult As Boolean = Me.IsSelectedChk()

            '在庫履歴作成日（フルバイトチェック）
            'rtnResult = rtnResult AndAlso _
            '            Me.IsHissuChk(Me._Frm.imdZaiRirekiDate, Me._Frm.lblTitleZaiRirekiCreate.Text)
            .imdZaiRirekiDate.ItemName() = .lblTitleZaiRirekiCreate.TextValue
            .imdZaiRirekiDate.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.imdZaiRirekiDate) = False Then
                Me._Vcon.SetErrorControl(.imdZaiRirekiDate)
                Return False
            End If

            '在庫履歴作成日（範囲チェック）
            rtnResult = rtnResult AndAlso Me.IsInputDateChk(nowDate)

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 入力チェック（削除）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDeleteChk() As Boolean

        With Me._Frm

            '未選択チェック
            Dim rtnResult As Boolean = Me.IsSelectedChk()

            '履歴日有無チェック
            'rtnResult = rtnResult AndAlso Me.FindRirekiDateFromSelectedRow()

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 入力チェック（マスタ参照）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsMasterPopChk() As Boolean

        With Me._Frm

            '営業所
            Dim rtnResult As Boolean = Me.IsNrsHissuChk()

            '荷主コード(大)
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtCustCdL, "荷主コード(大)", 5)

            '荷主コード(中)
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtCustCdM, "荷主コード(中)", 2)

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 入力チェック（Enter）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>担当者コードtextBoxでのEnterイベント時</remarks>
    Friend Function IsTantoEnterChk() As Boolean

        With Me._Frm

            '担当者コード
            Dim rtnResult As Boolean = Me._Vcon.IsKinsiByteChk(.txtTantouCd, "担当者コード", 5)

            Return rtnResult

        End With

    End Function

#End Region 'チェックコントロール

#Region "InnerMethod"

    ''' <summary>
    ''' 単項目チェック（ヘッダ項目）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHeaderChk() As Boolean

        With Me._Frm

            '営業所
            Dim rtnResult As Boolean = Me.IsNrsHissuChk()

            '荷主コード(大)
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtCustCdL, "荷主コード(大)", 5)

            '荷主コード(中)
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtCustCdM, "荷主コード(中)", 2)

            'コードが両方空だった場合は名称をクリアする
            If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True _
            And String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                .lblCustNm.TextValue = String.Empty
            End If

            '担当者コード
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtTantouCd, "担当者コード", 5)

            '在庫履歴作成日
            rtnResult = rtnResult AndAlso Me._Vcon.IsInputDateFullByteChk(.imdZaiRirekiDate, "在庫履歴作成日")

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 単項目チェック（スプレッド項目）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadInputChk() As Boolean

        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprCreate)

        '荷主名
        Dim rtnResult As Boolean = Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                           , LMD060G.sprCreateDef.CUST_NM.ColNo _
                                                           , LMD060G.sprCreateDef.CUST_NM.ColName, 122)

        '担当者名
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                           , LMD060G.sprCreateDef.TANTO_NM.ColNo _
                                                           , LMD060G.sprCreateDef.TANTO_NM.ColName, 20)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 未選択チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Private Function IsSelectedChk() As Boolean

        '未選択チェック
        If Me.FindSelectedRow() = -1 Then
            Return Me._Vcon.SetErrMessage("E009")
        End If

        Return True

    End Function

    ''' <summary>
    ''' 在庫履歴作成日
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputDateChk(ByVal nowDate As String) As Boolean

        With Me._Frm

            Dim dateTo As Date = DateAdd(DateInterval.Month, -2, Convert.ToDateTime(Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(nowDate)))
            '在庫履歴作成日がシステム日付より2ヶ月以前の場合はエラー
            If .imdZaiRirekiDate.TextValue() < dateTo.ToString("yyyyMMdd") Then

                .imdZaiRirekiDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Call Me._Vcon.SetErrorControl(.imdZaiRirekiDate)
                Return Me._Vcon.SetErrMessage("E039", New String() {"在庫履歴作成日", dateTo.ToString("yyyy/MM/dd")})

            End If

            Dim dateFrom As Date = Convert.ToDateTime(Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(nowDate))
            '在庫履歴作成日が未来日の場合はエラー
            If dateFrom.ToString("yyyyMMdd") < .imdZaiRirekiDate.TextValue() Then

                .imdZaiRirekiDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Call Me._Vcon.SetErrorControl(.imdZaiRirekiDate)
                Return Me._Vcon.SetErrMessage("E123", New String() {"在庫履歴作成日", "未来日"})

            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 必須チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsNrsHissuChk() As Boolean

        Return Me.IsHissuChk(Me._Frm.cmbEigyo, Me._Frm.lblTitleEigyo.Text)

    End Function

    ''' <summary>
    ''' 必須チェック（TextBox）
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHissuChk(ByVal ctl As Win.InputMan.LMImTextBox, ByVal msg As String) As Boolean

        ctl.ItemName = msg
        ctl.IsHissuCheck = True
        Return MyBase.IsValidateCheck(ctl)

    End Function

    ''' <summary>
    ''' 必須チェック（ComboBox）
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHissuChk(ByVal ctl As Win.InputMan.LMImCombo, ByVal msg As String) As Boolean

        ctl.ItemName = msg
        ctl.IsHissuCheck = True
        Return MyBase.IsValidateCheck(ctl)

    End Function

    ''' <summary>
    ''' 必須チェック（LMImDate）
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHissuChk(ByVal ctl As Win.InputMan.LMImDate, ByVal msg As String) As Boolean

        'コントロールに8以下の数字が入力された状態でチェックを書けることができるのでフルバイトチェックで対応する
        If ctl.TextValue.Trim().Length <> 8 Then
            Me._Vcon.SetErrorControl(ctl)
            Return Me._Vcon.SetErrMessage("E038", New String() {msg, "8"})
        End If

        Return True

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue(ByVal actionType As LMD060C.ActionType)

        With Me._Frm

            Select Case actionType

                Case LMD060C.ActionType.KENSAKU   '検索

                    'ヘッダ部
                    .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
                    .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
                    .txtTantouCd.TextValue = .txtTantouCd.TextValue.Trim()

                    'スプレッド部
                    .sprCreate.SetCellValue(0, LMD060C.SprColumnIndex.CUST_NM, _
                                            .sprCreate.ActiveSheet.Cells(0, LMD060C.SprColumnIndex.CUST_NM).Text.Trim())
                    .sprCreate.SetCellValue(0, LMD060C.SprColumnIndex.TANTO_NM, _
                        .sprCreate.ActiveSheet.Cells(0, LMD060C.SprColumnIndex.TANTO_NM).Text.Trim())

                Case Else

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 選択行有無判別
    ''' </summary>
    ''' <param name="rowCnt">選択行数（省略可）</param>
    ''' <returns>-1：選択行無し　0：単一行選択中　1：複数行選択中</returns>
    ''' <remarks></remarks>
    Friend Function FindSelectedRow(Optional ByRef rowCnt As Integer = 0) As Integer

        With Me._Frm.sprCreate.Sheets(0)

            Dim rowIdx As Integer = -1

            For i As Integer = 1 To .RowCount - 1
                If .Cells(i, LMD060G.sprCreateDef.DEF.ColNo).Value IsNot Nothing AndAlso _
                   .Cells(i, LMD060G.sprCreateDef.DEF.ColNo).Value.ToString = True.ToString Then

                    rowCnt = rowCnt + 1

                    If rowIdx = 0 Then
                        rowIdx = 1
                    End If
                    If rowIdx <> 1 Then
                        rowIdx = 0
                    End If
                End If
            Next

            Return rowIdx

        End With

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthority(ByVal actionType As LMD060C.ActionType) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv()
        Dim kengenFlg As Boolean = True

        Select Case actionType

            Case LMD060C.ActionType.DELETE, LMD060C.ActionType.JIKKOU         '削除、実行

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMD060C.ActionType.KENSAKU, LMD060C.ActionType.MASTEROPEN     '検索、マスタ参照

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

        End Select

        Return Me._Vcon.IsAuthorityChk(kengenFlg)

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMD060C.ActionType) As Boolean

        ''フォーカス位置がない場合、スルー
        'If String.IsNullOrEmpty(objNm) = True Then
        '    Return False
        'End If

        '入力チェック
        With Me._Frm

            '荷主コントロールでマスタ参照・Enterイベント発生時、
            If objNm.Equals(.txtCustCdL.Name) OrElse objNm.Equals(.txtCustCdM.Name) Then

                'Enter押下時の場合に荷主コード(大)、荷主コード(中)が空だったらマスタ参照はしない
                Select Case actionType

                    Case LMD060C.ActionType.ENTER
                        If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                            .lblCustNm.TextValue = String.Empty
                            Return False
                        End If

                End Select

                '荷主コード（大、中）の禁止文字チェック
                Return Me.IsMasterPopChk()

                '荷主コントロール以外の項目でマスタ参照
            ElseIf actionType.Equals(LMD060C.ActionType.MASTEROPEN) Then
                Return Me._Vcon.SetErrMessage("G005")

                '担当コードコントロールでEnterイベント発生時
            ElseIf actionType.Equals(LMD060C.ActionType.ENTER) AndAlso objNm.Equals(.txtTantouCd.Name) Then
                '担当者コードの禁止文字チェック
                Return Me.IsTantoEnterChk()
            End If

            Return False

        End With

    End Function

    ''' <summary>
    ''' 履歴日未作成データチェック
    ''' </summary>
    ''' <param name="rowCnt"></param>
    ''' <returns></returns>
    ''' <remarks>履歴日が全く作成されていないデータをチェック</remarks>
    Friend Function FindRirekiDateFromSelectedRow(Optional ByRef rowCnt As Integer = 0) As Boolean

        With Me._Frm.sprCreate.Sheets(0)

            For i As Integer = 1 To .RowCount - 1
                If .Cells(i, LMD060G.sprCreateDef.DEF.ColNo).Value IsNot Nothing AndAlso _
                   .Cells(i, LMD060G.sprCreateDef.DEF.ColNo).Value.ToString = True.ToString Then

                    '履歴日１が存在しない場合、削除不可能
                    If String.IsNullOrEmpty(.Cells(i, LMD060G.sprCreateDef.RIREKI_1.ColNo).Text) Then
                        Return Me._Vcon.SetErrMessage("E160", New String() {"選択したデータ", "履歴が存在しないデータ"})
                    End If

                End If
            Next

            Return True

        End With

    End Function

#End Region 'InnerMethod

End Class
