' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM210V : 乗務員マスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMM210Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM210V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM210F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMMControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM210F, ByVal v As LMMControlV)

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
        If Me.IsSaveSingleCheck() = False Then
            Return False
        End If

        Return True

    End Function


    ''' <summary>
    ''' スペース除去(編集部)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtCrewCd.TextValue = .txtCrewCd.TextValue.Trim()
            .txtCrewNm.TextValue = .txtCrewNm.TextValue.Trim()

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
            '営業所
            '2016.01.06 UMANO 英語化対応START
            '.cmbNrsBrCd.ItemName = "営業所"
            .cmbNrsBrCd.ItemName = .lblTitleEigyo.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If
            '乗務員コード
            '2016.01.06 UMANO 英語化対応START
            '.txtCrewCd.ItemName = "乗務員コード"
            .txtCrewCd.ItemName = .lblTitleDriverCd.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCrewCd.IsHissuCheck = True
            .txtCrewCd.IsForbiddenWordsCheck = True
            .txtCrewCd.IsByteCheck = 5
            .txtCrewCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCrewCd) = False Then
                Return False
            End If

            '乗務員氏名
            '2016.01.06 UMANO 英語化対応START
            '.txtCrewNm.ItemName = "乗務員氏名"
            .txtCrewNm.ItemName = .lblTitleDriverNm.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCrewNm.IsHissuCheck = True
            .txtCrewNm.IsForbiddenWordsCheck = True
            .txtCrewNm.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtCrewNm) = False Then
                Return False
            End If
            '勤務可能
            '2016.01.06 UMANO 英語化対応START
            '.cmbWorkPossible.ItemName = "勤務可能"
            .cmbWorkPossible.ItemName = .lblTitleWorkPossible.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbWorkPossible.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbWorkPossible) = False Then
                Return False
            End If
            '大型
            '2016.01.06 UMANO 英語化対応START
            '.cmbLarge.ItemName = " 大型"
            .cmbLarge.ItemName = .lblTitleLarge.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbLarge.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbLarge) = False Then
                Return False
            End If
            'けん引
            '2016.01.06 UMANO 英語化対応START
            '.cmbTraction.ItemName = " けん引"
            .cmbTraction.ItemName = .lblTitleTraction.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbTraction.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbTraction) = False Then
                Return False
            End If
            '乙種1類
            '2016.01.06 UMANO 英語化対応START
            '.cmbOtu1.ItemName = " 乙種1類"
            .cmbOtu1.ItemName = .lblTitleOtu1.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbOtu1.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbOtu1) = False Then
                Return False
            End If
            '乙種2類
            '2016.01.06 UMANO 英語化対応START
            '.cmbOtu2.ItemName = " 乙種2類"
            .cmbOtu2.ItemName = .lblTitleOtu2.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbOtu2.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbOtu2) = False Then
                Return False
            End If
            '乙種3類
            '2016.01.06 UMANO 英語化対応START
            '.cmbOtu3.ItemName = " 乙種3類"
            .cmbOtu3.ItemName = .lblTitleOtu3.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbOtu3.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbOtu3) = False Then
                Return False
            End If
            '乙種4類
            '2016.01.06 UMANO 英語化対応START
            '.cmbOtu4.ItemName = " 乙種4類"
            .cmbOtu4.ItemName = .lblTitleOtu4.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbOtu4.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbOtu4) = False Then
                Return False
            End If
            '乙種5類
            '2016.01.06 UMANO 英語化対応START
            '.cmbOtu5.ItemName = " 乙種5類"
            .cmbOtu5.ItemName = .lblTitleOtu5.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbOtu5.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbOtu5) = False Then
                Return False
            End If
            '乙種6類
            '2016.01.06 UMANO 英語化対応START
            '.cmbOtu6.ItemName = " 乙種6類"
            .cmbOtu6.ItemName = .lblTitleOtu6.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbOtu6.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbOtu6) = False Then
                Return False
            End If
            '移動監視者
            '2016.01.06 UMANO 英語化対応START
            '.cmbMoveKeepWatch.ItemName = "移動監視者"
            .cmbMoveKeepWatch.ItemName = .lblTitleMoveKeepWatch.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbMoveKeepWatch.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbMoveKeepWatch) = False Then
                Return False
            End If
            Return True
        End With

    End Function
    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM210F) As Boolean

        If frm.lblSituation.RecordStatus.Equals(RecordStatus.DELETE_REC) Then
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
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM210F, ByVal eventShubetsu As LMM210C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM210C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM210C.EventShubetsu.HUKUSHA
        '            msg = "複写"

        '        Case LMM210C.EventShubetsu.SAKUJO
        '            msg = "削除・復活"

        '    End Select

        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False

        'End If

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
        If Me.IsKensakuSingleCheck() = False Then
            Return False
        End If

        Return True

    End Function


    ''' <summary>
    ''' 検索押下時スプレッド単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleCheck() As Boolean

        With Me._Frm
            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '乗務員コード
            vCell.SetValidateCell(0, LMM210G.sprDetailDef.DRIVER_CD.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "乗務員コード"
            vCell.ItemName = LMM210G.sprDetailDef.DRIVER_CD.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '乗務員氏名
            vCell.SetValidateCell(0, LMM210G.sprDetailDef.DRIVER_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "乗務員氏名"
            vCell.ItemName = LMM210G.sprDetailDef.DRIVER_NM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM210C.EventShubetsu) As Boolean
       
        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv()
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM210C.EventShubetsu.SHINKI           '新規
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM210C.EventShubetsu.HENSHU          '編集
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM210C.EventShubetsu.HUKUSHA          '複写
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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


            Case LMM210C.EventShubetsu.SAKUJO          '削除・復活
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM210C.EventShubetsu.KENSAKU         '検索
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

            Case LMM210C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM210C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM210C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM210C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM210C.EventShubetsu.ENTER          'Enter
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM210C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM210C.EventShubetsu.MASTEROPEN) = True Then
                Call Me._Vcon.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing
        Dim focusCtl As Control = Me._Frm.ActiveControl

        Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl)

        Return False

    End Function

    ''' <summary>
    ''' 基本メッセージ設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetBaseMsg()

        Select Case Me._Frm.lblSituation.DispMode
            Case DispMode.INIT

                MyBase.ShowMessage("G007")

            Case DispMode.VIEW

                MyBase.ShowMessage("G013")

            Case DispMode.EDIT

                MyBase.ShowMessage("G003")

        End Select

    End Sub
#End Region 'Method

End Class
