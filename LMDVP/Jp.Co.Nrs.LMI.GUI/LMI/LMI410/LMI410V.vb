' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : 特殊荷主機能
'  プログラムID     :  LMI410V : ビックケミー取込データ確認／報告
'  作  成  者       :  [Umano]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI410Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMI410V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI410F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlC As LMIControlC

    ''' <summary>
    ''' チェックリストを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI410F, ByVal v As LMIControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._ControlV = v

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI410C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI410C.EventShubetsu.TORIKOMI     '取込
                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMI410C.EventShubetsu.IDO_HOUKOKU  '移動報告
                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMI410C.EventShubetsu.IDO_TORIKESI '移動取消
                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMI410C.EventShubetsu.GAMEN_SENI   '画面遷移
                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMI410C.EventShubetsu.PRINT        '印刷
                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMI410C.EventShubetsu.KENSAKU         '検索
                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMI410C.EventShubetsu.MASTEROPEN    'マスタ参照
                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMI410C.EventShubetsu.CLOSE           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMI410C.EventShubetsu.ENTER          'Enter
                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMI410C.EventShubetsu.EXE          '実行
                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMI410C.EventShubetsu.IKKATU_HENKO '一括変更
                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        If kengenFlg = True Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If
        Return False


    End Function

    ''' <summary>
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean


        '単項目チェック
        If Me.IsKensakuSingleChk() = False Then
            Return False
        End If

        '関連チェック
        If Me.IsKensakuKanrenChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As String) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            Return False
        End If

        '判定するコントロール設定先変数
        Dim txtCtl As Win.InputMan.LMImTextBox() = Nothing
        Dim lblCtl As Control() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name, .txtIkkatuCustL.Name, .txtIkkatuCustM.Name

                    Return True

            End Select

            Return Me._ControlV.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)
            'Return False

        End With

    End Function

    ''' <summary>
    ''' 移動報告ボタン押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsHoukokuIdoChk() As Boolean

        With Me._Frm

            '【単項目チェック】
            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            '自営業所チェック
            Dim rtnMsg As String = String.Empty

            rtnMsg = "移動報告"

            If Me.IsNrsChk(rtnMsg) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#Region "関連チェック(移動報告)"

    ''' <summary>
    ''' 移動報告押下時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsJissekiKanrenCheck(ByRef errDs As DataSet) As Hashtable

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim sagyoKbn As String = String.Empty
        Dim fileNm As String = String.Empty
        Dim custCd As String = String.Empty

        errDs = New LMI410DS()
        Dim dr As DataRow

        For i As Integer = 0 To max

            With Me._Frm.sprIdoList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                custCd = .Cells(selectRow, LMI410G.sprIdoList.CUST_CD.ColNo).Value().ToString()
                sagyoKbn = .Cells(selectRow, LMI410G.sprIdoList.SAGYO_STATE_KBN.ColNo).Value().ToString()
                fileNm = .Cells(selectRow, LMI410G.sprIdoList.FILE_NAME.ColNo).Value().ToString()

                If String.IsNullOrEmpty(custCd) = True Then

                    '荷主コードが決まっていない場合はエラー
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMI410C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMI410C.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E193"
                    dr("PARA1") = String.Empty
                    dr("PARA2") = String.Empty
                    dr("KEY_NM") = LMI410C.EXCEL_COLTITLE
                    dr("KEY_VALUE") = fileNm
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMI410C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    errHt.Add(i, String.Empty)
                    Continue For

                ElseIf (sagyoKbn.Equals(LMI410C.CMB_03) OrElse sagyoKbn.Equals(LMI410C.CMB_04)) Then

                    '作成済、報告済の場合はエラー
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMI410C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMI410C.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E269"
                    dr("PARA1") = "報告済"
                    dr("PARA2") = String.Empty
                    dr("KEY_NM") = LMI410C.EXCEL_COLTITLE
                    dr("KEY_VALUE") = fileNm
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMI410C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    errHt.Add(i, String.Empty)
                    Continue For

                End If

            End With

        Next

        Return errHt

    End Function

#End Region

    ''' <summary>
    ''' 一括変更ボタン押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsIkkatuChk() As Boolean

        With Me._Frm

            '【単項目チェック】
            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            Select Case Convert.ToString(.cmbIkkatuKbn.SelectedValue())

                Case LMI410C.CMB_01

                    '一括変更【荷主コード(大)】
                    .txtIkkatuCustL.ItemName = "荷主コード"
                    .txtIkkatuCustL.IsForbiddenWordsCheck = True
                    .txtIkkatuCustL.IsHissuCheck() = True
                    .txtIkkatuCustL.IsByteCheck = 5
                    If MyBase.IsValidateCheck(.txtIkkatuCustL) = False Then
                        Call Me._ControlV.SetErrorControl(.txtIkkatuCustL)
                        Return False
                    End If

                    '一括変更【荷主コード(中)】
                    .txtIkkatuCustM.ItemName = "荷主コード(中)"
                    .txtIkkatuCustM.IsForbiddenWordsCheck = True
                    .txtIkkatuCustM.IsHissuCheck() = True
                    .txtIkkatuCustM.IsByteCheck = 2
                    If MyBase.IsValidateCheck(.txtIkkatuCustM) = False Then
                        Call Me._ControlV.SetErrorControl(.txtIkkatuCustM)
                        Return False
                    End If

                Case LMI410C.CMB_02

                    .imdIkkatuDate.ItemName() = .cmbIkkatuKbn.SelectedText
                    .imdIkkatuDate.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.imdIkkatuDate) = False Then
                        Return False
                    End If

                    '入出荷(振替)日(FROM)
                    If .imdIkkatuDate.IsDateFullByteCheck(8) = False Then
                        MyBase.ShowMessage("E038", New String() {.cmbIkkatuKbn.SelectedText, "8"})
                        Return False
                    End If

            End Select

            '自営業所チェック
            Dim rtnMsg As String = String.Empty

            rtnMsg = "一括変更"

            If Me.IsNrsChk(rtnMsg) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#Region "関連チェック(一括変更)"

    ''' <summary>
    ''' 一括変更押下時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsIkkatuKanrenCheck(ByRef errDs As DataSet) As Hashtable

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim sagyoKbn As String = String.Empty
        Dim fileNm As String = String.Empty
        Dim custCd As String = String.Empty

        errDs = New LMI410DS()
        Dim dr As DataRow

        For i As Integer = 0 To max

            With Me._Frm.sprIdoList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                custCd = .Cells(selectRow, LMI410G.sprIdoList.CUST_CD.ColNo).Value().ToString()
                sagyoKbn = .Cells(selectRow, LMI410G.sprIdoList.SAGYO_STATE_KBN.ColNo).Value().ToString()
                fileNm = .Cells(selectRow, LMI410G.sprIdoList.FILE_NAME.ColNo).Value().ToString()

                If (sagyoKbn.Equals(LMI410C.CMB_03) OrElse sagyoKbn.Equals(LMI410C.CMB_04)) Then

                    '作成済、報告済の場合はエラー
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMI410C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMI410C.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E269"
                    dr("PARA1") = "報告済"
                    dr("PARA2") = String.Empty
                    dr("KEY_NM") = LMI410C.EXCEL_COLTITLE
                    dr("KEY_VALUE") = fileNm
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMI410C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    errHt.Add(i, String.Empty)
                    Continue For

                End If

            End With

        Next

        Return errHt

    End Function

#End Region

    ''' <summary>
    ''' 基本メッセージ設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetBaseMsg()

        MyBase.ShowMessage("G013")

    End Sub


#Region "内部メソッド"

    ''' <summary>
    ''' 検索押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleChk() As Boolean

        With Me._Frm

            If String.IsNullOrEmpty(Convert.ToString(.cmbSearchDate.SelectedValue)) = False AndAlso _
               Convert.ToString(.cmbSearchDate.SelectedValue).Equals(LMI410C.CMB_00) = False Then
                '取込日(FROM)、入出荷(振替)日(FROM)
                If .imdSearchDateFrom.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {String.Concat(.cmbSearchDate.SelectedText, "From"), "8"})
                    Return False
                End If
                '取込日(TO)、入出荷(振替)日(TO)
                If .imdSearchDateTo.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {String.Concat(.cmbSearchDate.SelectedText, "To"), "8"})
                    Return False
                End If
            End If

            '作業者コード
            .txtUserCd.ItemName() = "作業者コード"
            .txtUserCd.IsForbiddenWordsCheck() = True
            .txtUserCd.IsByteCheck() = 5
            If MyBase.IsValidateCheck(.txtUserCd) = False Then
                Return False
            End If

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprIdoList)

            '【取込ファイル名】
            vCell.SetValidateCell(0, LMI410G.sprIdoList.FILE_NAME.ColNo)
            vCell.ItemName = LMI410G.sprIdoList.FILE_NAME.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 300
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【備考】
            vCell.SetValidateCell(0, LMI410G.sprIdoList.TEXT_NM.ColNo)
            vCell.ItemName = LMI410G.sprIdoList.TEXT_NM.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【(現在地)荷主商品コード】
            vCell.SetValidateCell(0, LMI410G.sprIdoList.CURRENT_MATERIAL.ColNo)
            vCell.ItemName = LMI410G.sprIdoList.CURRENT_MATERIAL.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 18
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【(現在地)商品名】
            vCell.SetValidateCell(0, LMI410G.sprIdoList.CURRENT_DESCRIPTION.ColNo)
            vCell.ItemName = LMI410G.sprIdoList.CURRENT_DESCRIPTION.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【(現在地)LOT№】
            vCell.SetValidateCell(0, LMI410G.sprIdoList.CURRENT_BATCH.ColNo)
            vCell.ItemName = LMI410G.sprIdoList.CURRENT_BATCH.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【届先（出荷元）】
            vCell.SetValidateCell(0, LMI410G.sprIdoList.DEST_NM.ColNo)
            vCell.ItemName = LMI410G.sprIdoList.DEST_NM.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【(目的地)荷主商品コード】
            vCell.SetValidateCell(0, LMI410G.sprIdoList.DESTINATION_MATERIAL.ColNo)
            vCell.ItemName = LMI410G.sprIdoList.DESTINATION_MATERIAL.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 18
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【(目的地)商品名】
            vCell.SetValidateCell(0, LMI410G.sprIdoList.DESTINATION_DESCRIPTION.ColNo)
            vCell.ItemName = LMI410G.sprIdoList.DESTINATION_DESCRIPTION.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【(目的地)LOT№】
            vCell.SetValidateCell(0, LMI410G.sprIdoList.DESTINATION_BATCH.ColNo)
            vCell.ItemName = LMI410G.sprIdoList.DESTINATION_BATCH.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 検索押下時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuKanrenChk() As Boolean

        With Me._Frm

            If String.IsNullOrEmpty(Convert.ToString(.cmbSearchDate.SelectedValue)) = False AndAlso _
               Convert.ToString(.cmbSearchDate.SelectedValue).Equals(LMI410C.CMB_00) = False Then
                '【取込日(FROM)、(TO)　または入出荷(振替)日(FROM)、(TO)】　大小チェック
                If String.IsNullOrEmpty(.imdSearchDateFrom.TextValue) = False AndAlso _
                   String.IsNullOrEmpty(.imdSearchDateTo.TextValue) = False AndAlso _
                  Convert.ToInt32(.imdSearchDateFrom.TextValue) <= Convert.ToInt32(.imdSearchDateTo.TextValue) = False Then
                    MyBase.ShowMessage("E039", New String() {String.Concat(.cmbSearchDate.SelectedText, "(To)"), String.Concat(.cmbSearchDate.SelectedText, "(From)")})
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ参照時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsMasterShowInputChk(ByVal objNm As String) As Boolean


        '単項目チェック
        If Me.IsMasterShowSingleChk(objNm) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタポップ単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMasterShowSingleChk(ByVal objNm As String) As Boolean

        With Me._Frm

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name
                    '【荷主コード(大)】
                    .txtCustCdL.ItemName = "荷主コード"
                    .txtCustCdL.IsForbiddenWordsCheck = True
                    .txtCustCdL.IsByteCheck = 5
                    If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                        Call Me._ControlV.SetErrorControl(.txtCustCdL)
                        Return False
                    End If

                    '【荷主コード(中)】
                    .txtCustCdM.ItemName = "荷主コード(中)"
                    .txtCustCdM.IsForbiddenWordsCheck = True
                    .txtCustCdM.IsByteCheck = 2
                    If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                        Call Me._ControlV.SetErrorControl(.txtCustCdM)
                        Return False
                    End If

                Case .txtIkkatuCustL.Name, .txtIkkatuCustM.Name
                    '【荷主コード(大)】
                    .txtIkkatuCustL.ItemName = "荷主コード"
                    .txtIkkatuCustL.IsForbiddenWordsCheck = True
                    .txtIkkatuCustL.IsByteCheck = 5
                    If MyBase.IsValidateCheck(.txtIkkatuCustL) = False Then
                        Call Me._ControlV.SetErrorControl(.txtIkkatuCustL)
                        Return False
                    End If

                    '【荷主コード(中)】
                    .txtIkkatuCustM.ItemName = "荷主コード(中)"
                    .txtIkkatuCustM.IsForbiddenWordsCheck = True
                    .txtIkkatuCustM.IsByteCheck = 2
                    If MyBase.IsValidateCheck(.txtIkkatuCustM) = False Then
                        Call Me._ControlV.SetErrorControl(.txtIkkatuCustM)
                        Return False
                    End If

            End Select

        End With

        Return True

    End Function

#Region "入力チェック(実行ボタン押下)"

    Friend Function IsExeCheck() As Boolean

        With Me._Frm

            '報告作成種別
            .cmbHoukoku.ItemName() = "報告作成種別"
            .cmbHoukoku.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbHoukoku) = False Then
                Return False
            End If

            '【荷主コード(大)】
            .txtCustCdL.ItemName = "荷主コード"
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsHissuCheck() = True
            .txtCustCdL.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustCdL)
                Return False
            End If

            '【荷主コード(中)】
            .txtCustCdM.ItemName = "荷主コード(中)"
            .txtCustCdM.IsForbiddenWordsCheck = True
            .txtCustCdM.IsHissuCheck() = True
            .txtCustCdM.IsByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustCdM)
                Return False
            End If

            '報告作成種別
            .cmbSearchDate.ItemName() = "検索・実行条件種別"
            .cmbSearchDate.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbSearchDate) = False Then
                Return False
            End If

            If String.IsNullOrEmpty(Convert.ToString(.cmbSearchDate.SelectedValue)) = False AndAlso _
               Convert.ToString(.cmbSearchDate.SelectedValue).Equals(LMI410C.CMB_00) = False Then

                .imdSearchDateFrom.ItemName() = String.Concat(.cmbSearchDate.SelectedText, "From")
                .imdSearchDateFrom.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdSearchDateFrom) = False Then
                    Return False
                End If

                '取込日(FROM)、入出荷(振替)日(FROM)
                If .imdSearchDateFrom.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {String.Concat(.cmbSearchDate.SelectedText, "From"), "8"})
                    Return False
                End If
            End If

        End With

        Return True

    End Function

#End Region

#Region "選択チェック"
    ''' <summary>
    ''' 選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSelectDataChk() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Me._ChkList = Me.getCheckList()

        '選択チェック
        If Me._ControlV.IsSelectChk(Me._ChkList.Count()) = False Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

#End Region

#Region "関連チェック"

#End Region

#Region "選択行取得"
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMI410C.SprIdoListColumnIndex.DEF

        Return Me._ControlV.SprSelectList(defNo, Me._Frm.sprIdoList)

    End Function

#End Region

#Region "自営業所チェック"
    ''' <summary>
    ''' 自営業所チェック
    ''' </summary>
    ''' <returns>True:エラーなし, False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsNrsChk(ByVal rtnMsg As String) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        'If LMUserInfoManager.GetNrsBrCd().Equals(Me._Frm.cmbEigyo.SelectedValue.ToString()) = False Then
        '    Return Me._ControlV.SetErrMessage("E178", New String() {rtnMsg})
        'End If

        Return True

    End Function
#End Region

#Region "エラーメッセージIDデータセット格納"
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getErrMsgSetDataSet(ByVal msgId As String, ByVal msgStr As String(), _
                                        ByVal goodsCdNik As String, ByVal selectRow As String, _
                                        ByRef errDs As DataSet) As DataSet

        Dim dr As DataRow

        'エラーがある場合、DataTableに設定
        dr = errDs.Tables(LMI410C.TABLE_NM_GUIERROR).NewRow()
        dr("GUIDANCE_ID") = LMI410C.GUIDANCE_KBN
        dr("MESSAGE_ID") = msgId
        dr("PARA1") = msgStr(0)
        dr("PARA2") = msgStr(1)
        dr("PARA3") = msgStr(2)
        dr("PARA4") = msgStr(3)
        dr("PARA5") = msgStr(4)
        dr("KEY_NM") = LMI410C.EXCEL_COLTITLE
        dr("KEY_VALUE") = goodsCdNik
        dr("ROW_NO") = selectRow.ToString()
        errDs.Tables(LMI410C.TABLE_NM_GUIERROR).Rows.Add(dr)

        Return errDs

    End Function

#End Region

#End Region

#End Region

End Class
