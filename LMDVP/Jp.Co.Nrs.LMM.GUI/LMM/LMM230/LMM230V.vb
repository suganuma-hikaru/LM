' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM230V : ユーザーマスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMM230Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM230V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM230F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM230F, ByVal v As LMMControlV)

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
    ''' 行削除 入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsRowCheck() As Boolean

        Dim arr As ArrayList = Nothing

        With Me._Frm

            '選択チェック
            arr = Me.getCheckList(.sprDetail2)

            If 0 = arr.Count Then
                .sprDetail2.Focus()
                MyBase.ShowMessage("E009")
                Return False
            End If

        End With

        Return True

    End Function

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

        'JIS明細存在チェック
        rtnResult = rtnResult AndAlso Me.IsSprRowExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 保存時のtrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm
            .txtAreaCd.TextValue = .txtAreaCd.TextValue.Trim()
            .txtAreaNm.TextValue = .txtAreaNm.TextValue.Trim()
            .txtAreaInfo.TextValue = .txtAreaInfo.TextValue.Trim()
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
            .cmbNrsBrCd.ItemName = "営業所"
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            'エリアコード
            .txtAreaCd.ItemName = "エリアコード"
            .txtAreaCd.IsHissuCheck = True
            .txtAreaCd.IsForbiddenWordsCheck = True
            .txtAreaCd.IsByteCheck = 7
            If MyBase.IsValidateCheck(.txtAreaCd) = False Then
                Return False
            End If

            'エリア名
            .txtAreaNm.ItemName = "エリア名"
            .txtAreaNm.IsHissuCheck = True
            .txtAreaNm.IsForbiddenWordsCheck = True
            .txtAreaNm.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtAreaNm) = False Then
                Return False
            End If

            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
            '便区分
            .cmbBin.ItemName = "便区分"
            .cmbBin.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbBin) = False Then
                Return False
            End If
            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

            '備考
            .txtAreaInfo.ItemName = "備考"
            .txtAreaInfo.IsForbiddenWordsCheck = True
            .txtAreaInfo.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtAreaInfo) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' JIS明細存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSprRowExistChk() As Boolean

        With Me._Frm

            Dim sprMax As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1

            If sprMax < 0 Then
                MyBase.ShowMessage("E001", New String() {"JIS明細"})
                .btnRowAdd.Focus()
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk() As Boolean

        If Me._Frm.lblSituation.RecordStatus = RecordStatus.DELETE_REC Then
            MyBase.ShowMessage("E035")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 他営業所チェック
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <returns>True;エラーなし,False;エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsUserNrsBrCdChk(ByVal eventShubetsu As LMM230C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If Me._Frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then

        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM230C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM230C.EventShubetsu.SAKUJO
        '            msg = "削除・復活"

        '    End Select

        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False

        'End If

        Return True

    End Function

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Function SprSelectCount() As ArrayList

        Dim defNo As Integer = LMM230G.sprDetailDef.DEF.ColNo

        With Me._Frm.sprDetail.ActiveSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If Me._Vcon.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

            Return list

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

        'trim(画面ヘッダー部)
        Call Me.HeaderTrimSpaceTextvalue()

        '単項目チェック
        If Me.IsKensakuSingleCheck() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 画面ヘッダー部Trim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HeaderTrimSpaceTextvalue()

        Me._Frm.txtJis.TextValue = Me._Frm.txtJis.TextValue.Trim()

    End Sub

    ''' <summary>
    ''' 検索押下時スプレッド単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleCheck() As Boolean

        With Me._Frm

            'JISコード
            .txtJis.ItemName = "JISコード"
            .txtJis.IsForbiddenWordsCheck = True
            .txtJis.IsByteCheck = 7
            If MyBase.IsValidateCheck(.txtJis) = False Then
                Return False
            End If

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            'エリアコード
            vCell.SetValidateCell(0, LMM230G.sprDetailDef.AREA_CD.ColNo)
            vCell.ItemName = "エリアコード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 7
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'エリア名
            vCell.SetValidateCell(0, LMM230G.sprDetailDef.AREA_NM.ColNo)
            vCell.ItemName = "エリア名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '備考
            vCell.SetValidateCell(0, LMM230G.sprDetailDef.AREA_INFO.ColNo)
            vCell.ItemName = "備考"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM230C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM230C.EventShubetsu.SHINKI           '新規
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM230C.EventShubetsu.HENSHU          '編集
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM230C.EventShubetsu.HUKUSHA          '複写
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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


            Case LMM230C.EventShubetsu.SAKUJO          '削除・復活
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM230C.EventShubetsu.KENSAKU         '検索
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

            Case LMM230C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True            '2011.08.24 検証結果一覧№36対応
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

            Case LMM230C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM230C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM230C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM230C.EventShubetsu.ENTER          'Enter
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
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList(ByVal sprDetail As Spread.LMSpread) As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = 0
        If ("sprDetail2").Equals(sprDetail.Name) = True Then
            defNo = LMM230C.SprColumnIndex2.DEF
        End If

        '選択された行の行番号を取得
        Return _Vcon.SprSelectList(defNo, sprDetail)

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM230C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM230C.EventShubetsu.MASTEROPEN) = True Then
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

                Case .txtJis.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtJis}
                    msg = New String() {.lblTitleJis.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblKen, .lblShi}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl, clearCtl)

        End With

    End Function

#End Region 'Method

End Class
