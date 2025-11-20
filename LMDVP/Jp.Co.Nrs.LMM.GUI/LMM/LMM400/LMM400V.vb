' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM400V : 西濃番号マスタメンテ
'  作  成  者       :  adachi
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMM400Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM400V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM400F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMMControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMMControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM400F, ByVal v As LMMControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = v

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSaveInputChk() As Boolean

        'trim
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsSaveSingleCheck()

        Return rtnResult

    End Function

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM400F) As Boolean

        If frm.lblSituation.RecordStatus = RecordStatus.DELETE_REC Then
            MyBase.ShowMessage("E035")
            Return False
        End If

        Return True

    End Function
    ''' <summary>
    ''' 他営業所チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM400F, ByVal eventShubetsu As LMM400C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM400C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM400C.EventShubetsu.HUKUSHA
        '            msg = "複写"

        '        Case LMM400C.EventShubetsu.SAKUJO
        '            msg = "削除・復活"

        '    End Select

        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False

        'End If

        Return True

    End Function

    ''' <summary>
    ''' 保存時のtrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
            .txtZipNo.TextValue = .txtZipNo.TextValue.Trim()
            .txtKenK.TextValue = .txtKenK.TextValue.Trim()
            .txtCityK.TextValue = .txtCityK.TextValue.Trim()
            .txtShiwakeCd.TextValue = .txtShiwakeCd.TextValue.Trim()
            .txtChakuCd.TextValue = .txtChakuCd.TextValue.Trim()
            .txtChakuNm.TextValue = .txtChakuNm.TextValue.Trim()

        End With

    End Sub

    ''' <summary>
    ''' 検索押下時単項目チェック 禁止文字チェックetc
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleChk() As Boolean

        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetail)

            '【荷主名　大】
            vCell.SetValidateCell(0, LMM400G.sprDetailDef.CUST_NM_L.ColNo)
            vCell.ItemName = LMM400G.sprDetailDef.CUST_NM_L.ColName
            vCell.IsByteCheck = 60
            vCell.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【荷主名　中】
            vCell.SetValidateCell(0, LMM400G.sprDetailDef.CUST_NM_M.ColNo)
            vCell.ItemName = LMM400G.sprDetailDef.CUST_NM_M.ColName
            vCell.IsByteCheck = 60
            vCell.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【荷主コード大】
            vCell.SetValidateCell(0, LMM400G.sprDetailDef.CUST_CD_L.ColNo)
            vCell.ItemName = LMM400G.sprDetailDef.CUST_CD_L.ColName
            vCell.IsByteCheck = 5
            vCell.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【荷主コード中】
            vCell.SetValidateCell(0, LMM400G.sprDetailDef.CUST_CD_M.ColNo)
            vCell.ItemName = LMM400G.sprDetailDef.CUST_CD_M.ColName
            vCell.IsByteCheck = 2
            vCell.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【郵便番号】
            vCell.SetValidateCell(0, LMM400G.sprDetailDef.ZIP_NO.ColNo)
            vCell.ItemName = LMM400G.sprDetailDef.ZIP_NO.ColName
            vCell.IsByteCheck = 10
            vCell.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【都道府県名】
            vCell.SetValidateCell(0, LMM400G.sprDetailDef.KEN_K.ColNo)
            vCell.ItemName = LMM400G.sprDetailDef.KEN_K.ColName
            vCell.IsByteCheck = 10
            vCell.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【市区町村名】
            vCell.SetValidateCell(0, LMM400G.sprDetailDef.CITY_K.ColNo)
            vCell.ItemName = LMM400G.sprDetailDef.CITY_K.ColName
            vCell.IsByteCheck = 40
            vCell.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【仕分コード】
            vCell.SetValidateCell(0, LMM400G.sprDetailDef.CITY_K.ColNo)
            vCell.ItemName = LMM400G.sprDetailDef.CITY_K.ColName
            vCell.IsByteCheck = 3
            vCell.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【着点番号】
            vCell.SetValidateCell(0, LMM400G.sprDetailDef.CHAKU_CD.ColNo)
            vCell.ItemName = LMM400G.sprDetailDef.CHAKU_CD.ColName
            vCell.IsByteCheck = 4
            vCell.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【着点名称】
            vCell.SetValidateCell(0, LMM400G.sprDetailDef.CHAKU_NM.ColNo)
            vCell.ItemName = LMM400G.sprDetailDef.CHAKU_NM.ColName
            vCell.IsByteCheck = 40
            vCell.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If



        End With

        Return True

    End Function


    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleCheck() As Boolean

        With Me._Frm
            '**********編集部のチェック

            '営業所
            .cmbNrsBrCd.Name = "営業所"
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            '荷主コードL
            .txtCustCdL.ItemName = "荷主コード(大]"
            .txtCustCdL.IsHissuCheck = True
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If

            '荷主コードM
            .txtCustCdM.ItemName = "荷主コード(中)"
            .txtCustCdM.IsHissuCheck = True
            .txtCustCdM.IsForbiddenWordsCheck = True
            .txtCustCdM.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                Return False
            End If

            '郵便番号
            .txtZipNo.ItemName = "郵便番号"
            .txtZipNo.IsHissuCheck = True
            .txtZipNo.IsForbiddenWordsCheck = True
            .txtZipNo.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtZipNo) = False Then
                Return False
            End If

            '都道府県名
            .txtKenK.ItemName = "都道府県名"
            .txtKenK.IsHissuCheck = True
            .txtKenK.IsForbiddenWordsCheck = True
            .txtKenK.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtKenK) = False Then
                Return False
            End If

            '市区町村名
            .txtCityK.ItemName = "市区町村名"
            .txtCityK.IsHissuCheck = True
            .txtCityK.IsForbiddenWordsCheck = True
            .txtCityK.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtCityK) = False Then
                Return False
            End If

            '仕分コード
            .txtShiwakeCd.ItemName = "仕分コード"
            .txtShiwakeCd.IsHissuCheck = True
            .txtShiwakeCd.IsForbiddenWordsCheck = True
            .txtShiwakeCd.IsByteCheck = 3
            If MyBase.IsValidateCheck(.txtShiwakeCd) = False Then
                Return False
            End If

            '着点番号
            .txtChakuCd.ItemName = "着点番号"
            .txtChakuCd.IsHissuCheck = True
            .txtChakuCd.IsForbiddenWordsCheck = True
            .txtChakuCd.IsByteCheck = 4
            If MyBase.IsValidateCheck(.txtChakuCd) = False Then
                Return False
            End If

            '着点名称
            .txtChakuNm.ItemName = "着点名称"
            .txtChakuNm.IsHissuCheck = True
            .txtChakuNm.IsForbiddenWordsCheck = True
            .txtChakuNm.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtChakuNm) = False Then
                Return False
            End If




        End With

        Return True

    End Function


    ''' <summary>
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean

        'Trimチェック
        '検索
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0, Me._Frm.sprDetail.ActiveSheet.Columns.Count - 1)

        '単項目チェック
        If Me.IsKensakuSingleChk() = False Then
            Return False
        End If


        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM400C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM400C.EventShubetsu.SHINKI           '新規
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM400C.EventShubetsu.HENSHU          '編集
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM400C.EventShubetsu.HUKUSHA          '複写
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select


            Case LMM400C.EventShubetsu.SAKUJO          '削除・復活
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM400C.EventShubetsu.KENSAKU         '検索
                '50:外部の場合エラー
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

            Case LMM400C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM400C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM400C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM400C.EventShubetsu.DCLICK         'ダブルクリック
                '50:外部の場合エラー
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

            Case LMM400C.EventShubetsu.ENTER          'Enter
                '50:外部の場合エラー
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

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        If kengenFlg = True Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If

    End Function


    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM400C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'Return Me._Vcon.SetFocusErrMessage()
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM400C.EventShubetsu.MASTEROPEN) = True Then
                Call Me._Vcon.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing
        Dim clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing
        Dim focusCtl As Control = Me._Frm.ActiveControl

        With Me._Frm

            Select Case objNm

                Case .txtZipNo.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtZipNo}
                    msg = New String() {.lblTitleZipNo.Text}

                Case .txtCustCdL.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtCustCdL}
                    msg = New String() {.lblTitleCustL.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCustNmL, .lblCustNmM}

                Case .txtCustCdM.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtCustCdL}
                    msg = New String() {.lblTitleCustL.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCustNmL, .lblCustNmM}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl, clearCtl)

        End With

    End Function

#End Region 'Method

End Class
