' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : ＳＣＭ
'  プログラムID     :  LMM190C : 距離程マスタメンテ
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
''' LMM190Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM190V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM190F


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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM190F, ByRef v As LMMControlV)

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

        '保存時のtrim
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsSaveSingleCheck()

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsSaveMstExistCheck()

        '関連チェック(大小チェック)

        rtnResult = rtnResult AndAlso Me.IsHozonLargeSmallCheck()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 保存時のtrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtKyoriCd.TextValue = .txtKyoriCd.TextValue.Trim()
            .txtOrigJisCd.TextValue = .txtOrigJisCd.TextValue.Trim()
            .lblOrigKenNm.TextValue = .lblOrigKenNm.TextValue.Trim()
            .lblOrigShiNm.TextValue = .lblOrigShiNm.TextValue.Trim()
            .txtDestJisCd.TextValue = .txtDestJisCd.TextValue.Trim()
            .lblDestKenNm.TextValue = .lblDestKenNm.TextValue.Trim()
            .lblDestShiNm.TextValue = .lblDestShiNm.TextValue.Trim()
            .txtKyoriRem.TextValue = .txtKyoriRem.TextValue.Trim()


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
            '距離程コード
            .txtKyoriCd.ItemName = "距離程コード"
            .txtKyoriCd.IsHissuCheck = True
            .txtKyoriCd.IsForbiddenWordsCheck = True
            .txtKyoriCd.IsFullByteCheck = 3
            .txtKyoriCd.IsHankakuCheck = True
            If MyBase.IsValidateCheck(.txtKyoriCd) = False Then
                Return False
            End If

            '発地JISコード
            .txtOrigJisCd.ItemName = "発地JISコード"
            .txtOrigJisCd.IsHissuCheck = True
            .txtOrigJisCd.IsForbiddenWordsCheck = True
            .txtOrigJisCd.IsByteCheck = 7
            .txtOrigJisCd.IsHankakuCheck = True
            If MyBase.IsValidateCheck(.txtOrigJisCd) = False Then
                Return False
            End If

            '届先JISコード
            .txtDestJisCd.ItemName = "届先JISコード"
            .txtDestJisCd.IsHissuCheck = True
            .txtDestJisCd.IsForbiddenWordsCheck = True
            .txtDestJisCd.IsByteCheck = 7
            .txtDestJisCd.IsHankakuCheck = True
            If MyBase.IsValidateCheck(.txtDestJisCd) = False Then
                Return False
            End If

            '備考
            .txtKyoriRem.ItemName = "備考"
            .txtKyoriRem.IsForbiddenWordsCheck = True
            .txtKyoriRem.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtKyoriRem) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 距離程マスタ存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSaveMstExistCheck() As Boolean

        Dim origjis As String = Me._Frm.txtOrigJisCd.TextValue
        Dim destjis As String = Me._Frm.txtDestJisCd.TextValue


        If Me.IsSaveJisExistCheck(origjis, Me._Frm.txtOrigJisCd) = False Then
            Return False
        End If

        If Me.IsSaveJisExistCheck(destjis, Me._Frm.txtDestJisCd) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' JISマスタ存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSaveJisExistCheck(ByVal jiscd As String, ByVal ctl As Win.InputMan.LMImTextBox) As Boolean

        With Me._Frm

            Dim drs As DataRow() = Me._Vcon.SelectJisListDataRow(jiscd)

            If drs.Length < 1 Then
                MyBase.ShowMessage("E079", New String() {"JISマスタ", jiscd})
                Call Me.goodsCtlErrSet(jiscd)
                Return False
            End If

            '2件以上
            If drs.Length > 1 Then
                MyBase.ShowMessage("E206", New String() {jiscd, "JISコード"})
                'エラー時コントロール設定
                Call Me.goodsCtlErrSet(jiscd)
                Return False
            End If

            'マスタの値を設定
            '1件（データあり）
            If .txtOrigJisCd.Name.Equals(ctl.Name) = True Then

                .txtOrigJisCd.TextValue = drs(0).Item("JIS_CD").ToString()
                .lblOrigKenNm.TextValue = drs(0).Item("KEN").ToString()
                .lblOrigShiNm.TextValue = drs(0).Item("SHI").ToString()

            Else

                .txtDestJisCd.TextValue = drs(0).Item("JIS_CD").ToString()
                .lblDestKenNm.TextValue = drs(0).Item("KEN").ToString()
                .lblDestShiNm.TextValue = drs(0).Item("SHI").ToString()

            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' JISマスタ存在チェックエラー設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub goodsCtlErrSet(ByVal jiscd As String)

        With Me._Frm

            If Me._Frm.txtOrigJisCd.TextValue.ToString() = jiscd Then
                .lblOrigKenNm.TextValue = String.Empty
                .lblOrigShiNm.TextValue = String.Empty
                Call Me._Vcon.SetErrorControl(.txtOrigJisCd)

            ElseIf Me._Frm.txtDestJisCd.TextValue.ToString() = jiscd Then
                .lblDestKenNm.TextValue = String.Empty
                .lblDestShiNm.TextValue = String.Empty
                Call Me._Vcon.SetErrorControl(.txtDestJisCd)
            End If

        End With

    End Sub

    ''' <summary>
    ''' スプレッド関連チェック(大小チェック保存時)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsHozonLargeSmallCheck() As Boolean

        With Me._Frm

            Dim oJis As String = String.Empty
            Dim dJis As String = String.Empty
            oJis = Me._Frm.txtOrigJisCd.TextValue
            dJis = Me._Frm.txtDestJisCd.TextValue
            If String.IsNullOrEmpty(oJis) = True _
               OrElse String.IsNullOrEmpty(dJis) = True Then

                Return True

            End If

            If Me._Vcon.IsNumAlphaLargeSmallChk(dJis, oJis, True) = False Then
                MyBase.ShowMessage("E182", New String() {"届先JISコード", "発地JISコード"})
                Return False
            End If

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

        '関連チェック(大小チェック)
        If Me.IsKensakuLargeSmallCheck() = False Then
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


            '距離程コード
            vCell.SetValidateCell(0, LMM190G.sprDetailDef.KYORI_CD.ColNo)
            vCell.ItemName = "距離程コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 3
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '発地JISコード
            vCell.SetValidateCell(0, LMM190G.sprDetailDef.ORIG_JIS_CD.ColNo)
            vCell.ItemName = "発地JISコード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 7
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '発地市区町村名
            vCell.SetValidateCell(0, LMM190G.sprDetailDef.ORIG_SHI.ColNo)
            vCell.ItemName = "発地市区町村名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '届先JISコード
            vCell.SetValidateCell(0, LMM190G.sprDetailDef.DEST_JIS_CD.ColNo)
            vCell.ItemName = "届先JISコード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 7
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '届先市区町村名
            vCell.SetValidateCell(0, LMM190G.sprDetailDef.DEST_SHI.ColNo)
            vCell.ItemName = "届先市区町村名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '備考
            vCell.SetValidateCell(0, LMM190G.sprDetailDef.KYORI_REM.ColNo)
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
    ''' スプレッド関連チェック(大小チェック検索押下時)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsKensakuLargeSmallCheck() As Boolean

        With Me._Frm

            Dim oJis As String = String.Empty
            Dim dJis As String = String.Empty
            oJis = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMM190G.sprDetailDef.ORIG_JIS_CD.ColNo))
            dJis = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMM190G.sprDetailDef.DEST_JIS_CD.ColNo))
            If String.IsNullOrEmpty(oJis) = True _
               OrElse String.IsNullOrEmpty(dJis) = True Then

                Return True

            End If


            If Me._Vcon.IsNumAlphaLargeSmallChk(dJis, oJis, True) = False Then
                MyBase.ShowMessage("E182", New String() {"届先JISコード", "発地JISコード"})
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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM190C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM190C.EventShubetsu.SHINKI           '新規
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

            Case LMM190C.EventShubetsu.HENSHU          '編集
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

            Case LMM190C.EventShubetsu.SAKUJO          '削除・復活
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

            Case LMM190C.EventShubetsu.KENSAKU         '検索
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

            Case LMM190C.EventShubetsu.MASTEROPEN          'マスタ参照
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

            Case LMM190C.EventShubetsu.HOZON           '保存
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

            Case LMM190C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM190C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM190C.EventShubetsu.ENTER          'Enter
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM190C.EventShubetsu) As Boolean


        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM190C.EventShubetsu.MASTEROPEN) = True Then
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


                Case .txtOrigJisCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtOrigJisCd}
                    msg = New String() {String.Concat(.lblOrigKenNm.Text, .lblOrigShiNm.Text, LMMControlC.CD)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblOrigKenNm, .lblOrigShiNm}


                Case .txtDestJisCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtDestJisCd}
                    msg = New String() {String.Concat(.lblDestKenNm.Text, .lblDestShiNm.Text, LMMControlC.CD)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblDestKenNm, .lblDestShiNm}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl, clearCtl)

        End With


    End Function

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM190F) As Boolean

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
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM190F, ByVal eventShubetsu As LMM190C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM190C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM190C.EventShubetsu.SAKUJO
        '            msg = "削除・復活"

        '    End Select

        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False

        'End If

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
