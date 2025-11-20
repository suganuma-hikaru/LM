' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : ＳＣＭ
'  プログラムID     :  LMM170C : 郵便番号マスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMM170Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM170V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM170F


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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM170F, ByRef v As LMMControlV)

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

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsSaveExistCheck()


        Return rtnResult

    End Function

    ''' <summary>
    ''' 保存時のtrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtJisCd.TextValue = .txtJisCd.TextValue.Trim()
            .txtZipNo.TextValue = .txtZipNo.TextValue.Trim()
            .txtKenK.TextValue = .txtKenK.TextValue.Trim()
            .txtCityK.TextValue = .txtCityK.TextValue.Trim()
            .txtTownK.TextValue = .txtTownK.TextValue.Trim()
            .txtKenN.TextValue = .txtKenN.TextValue.Trim()
            .txtCityN.TextValue = .txtCityN.TextValue.Trim()
            .txtTownN.TextValue = .txtTownN.TextValue.Trim()

        End With

    End Sub


    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleCheck() As Boolean

        With Me._Frm
            '**********編集部のチェック
            'JISコード
            .txtJisCd.ItemName = "JISコード"
            .txtJisCd.IsHissuCheck = True
            .txtJisCd.IsForbiddenWordsCheck = True
            .txtJisCd.IsByteCheck = 7
            .txtJisCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtJisCd) = False Then
                Return False
            End If

            '郵便番号
            .txtZipNo.ItemName = "郵便番号"
            .txtZipNo.IsHissuCheck = True
            .txtZipNo.IsForbiddenWordsCheck = True
            .txtZipNo.IsByteCheck = 7
            .txtZipNo.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtZipNo) = False Then
                Return False
            End If

            '都道府県(カナ)
            .txtKenK.ItemName = "都道府県(カナ)"
            .txtKenK.IsHissuCheck = True
            .txtKenK.IsForbiddenWordsCheck = True
            .txtKenK.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtKenK) = False Then
                Return False
            End If

            '市区町村名(カナ)
            .txtCityK.ItemName = "市区町村名(カナ)"
            .txtCityK.IsHissuCheck = True
            .txtCityK.IsForbiddenWordsCheck = True
            .txtCityK.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtCityK) = False Then
                Return False
            End If

            '町域名(カナ)
            .txtTownK.ItemName = "町域名(カナ)"
            .txtTownK.IsForbiddenWordsCheck = True
            .txtTownK.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtTownK) = False Then
                Return False
            End If

            '都道府県(漢字)
            .txtKenN.ItemName = "都道府県(漢字)"
            .txtKenN.IsHissuCheck = True
            .txtKenN.IsForbiddenWordsCheck = True
            .txtKenN.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtKenN) = False Then
                Return False
            End If

            '市区町村名(漢字)
            .txtCityN.ItemName = "市区町村名(漢字)"
            .txtCityN.IsHissuCheck = True
            .txtCityN.IsForbiddenWordsCheck = True
            .txtCityN.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtCityN) = False Then
                Return False
            End If

            '町域名(漢字)
            .txtTownN.ItemName = "町域名(漢字)"
            .txtTownN.IsForbiddenWordsCheck = True
            .txtTownN.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtTownN) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' JISマスタ存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSaveExistCheck() As Boolean


        With Me._Frm

            Dim jis As String = .txtJisCd.TextValue
            Dim drs As DataRow() = Me._Vcon.SelectJisListDataRow(jis)

            '0件（データなし）
            If drs.Length < 1 Then
                MyBase.ShowMessage("E079", New String() {"JISマスタ", jis})
                Call Me._Vcon.SetErrorControl(.txtJisCd)
                Return False
            End If

            'マスタの値を設定
            .txtJisCd.TextValue = drs(0).Item("JIS_CD").ToString()

            Return True

        End With

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
        If Me.IsKensakuSingleCheck() = False Then
            Return False
        End If

        '関連チェック

        If Me.IsKensakuHissuCheck() = False Then
            Return False
        End If


        Return True

    End Function

    ''' <summary>
    ''' 検索押下時スプレッド単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleCheck() As Boolean

        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '郵便番号
            vCell.SetValidateCell(0, LMM170G.sprDetailDef.ZIP_NO.ColNo)
            vCell.ItemName = "郵便番号"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsHankakuCheck = True
            vCell.IsByteCheck = 7
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            'JISコード
            vCell.SetValidateCell(0, LMM170G.sprDetailDef.JIS_CD.ColNo)
            vCell.ItemName = "JISコード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsHankakuCheck = True
            vCell.IsByteCheck = 7
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '市区町村名(漢字)
            vCell.SetValidateCell(0, LMM170G.sprDetailDef.CITY_N.ColNo)
            vCell.ItemName = "市区町村名(漢字)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '町域名(漢字)
            vCell.SetValidateCell(0, LMM170G.sprDetailDef.TOWN_N.ColNo)
            vCell.ItemName = "町域名(漢字)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If


        End With

        Return True


    End Function

    ''' <summary>
    ''' 検索押下時スプレッド関連チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsKensakuHissuCheck() As Boolean

        With Me._Frm

            Dim yb As String = String.Empty
            Dim tm As String = String.Empty
            yb = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMM170G.sprDetailDef.ZIP_NO.ColNo))
            tm = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMM170G.sprDetailDef.KEN_N.ColNo))
            If String.IsNullOrEmpty(yb) = True _
                AndAlso String.IsNullOrEmpty(tm) = True Then
                MyBase.ShowMessage("E270", New String() {"郵便番号", "都道府県名"})
                Return False

            End If
            Return True

        End With

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM170C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM170C.EventShubetsu.SHINKI           '新規
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

            Case LMM170C.EventShubetsu.HENSHU          '編集
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

            Case LMM170C.EventShubetsu.SAKUJO          '削除・復活
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

            Case LMM170C.EventShubetsu.KENSAKU         '検索
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

            Case LMM170C.EventShubetsu.MASTEROPEN          'マスタ参照
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


            Case LMM170C.EventShubetsu.HOZON           '保存
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

            Case LMM170C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM170C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM170C.EventShubetsu.ENTER          'Enter
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
        Return False

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM170C.EventShubetsu) As Boolean


        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'Return Me._Vcon.SetFocusErrMessage()
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM170C.EventShubetsu.MASTEROPEN) = True Then
                Call Me._Vcon.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm

            Select Case objNm


                Case .txtJisCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtJisCd}
                    msg = New String() {String.Concat(LMMControlC.CD)}

            End Select

            Dim focusCtl As Control = Me._Frm.ActiveControl
            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl)


        End With

    End Function

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM170F) As Boolean

        If frm.lblSituation.RecordStatus = RecordStatus.DELETE_REC Then
            MyBase.ShowMessage("E035")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal ctl As Control)

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor()

        If TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            DirectCast(ctl, Win.InputMan.LMComboKubun).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            DirectCast(ctl, Win.InputMan.LMImNumber).BackColorDef = errorColor

        End If

        ctl.Focus()
        ctl.Select()

    End Sub


#End Region 'Method

End Class
