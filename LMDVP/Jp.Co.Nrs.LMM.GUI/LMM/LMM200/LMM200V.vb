' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : ＳＣＭ
'  プログラムID     :  LMM200C : 車輌マスタメンテ
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
''' LMM200Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM200V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM200F


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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM200F, ByRef v As LMMControlV)

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

        '関連チェック
        rtnResult = rtnResult AndAlso Me.IsSaveExistCheck()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 保存時のtrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .lblCarKey.TextValue = .lblCarKey.TextValue.Trim()
            .txtCarNo.TextValue = .txtCarNo.TextValue.Trim()
            .txtTrailerNo.TextValue = .txtTrailerNo.TextValue.Trim()
            .txtUnsocoCd.TextValue = .txtUnsocoCd.TextValue.Trim()
            .txtUnsocoBrCd.TextValue = .txtUnsocoBrCd.TextValue.Trim()
            .lblUnsocoNm.TextValue = .lblUnsocoNm.TextValue.Trim()

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
            '車輌番号(前)
            '2016.01.06 UMANO 英語化対応START
            '.txtCarNo.ItemName = "車輌番号(前)"
            .txtCarNo.ItemName = String.Concat(.LmTitleLabel14.Text(), "(", .grpFront.Text(), ")")
            '2016.01.06 UMANO 英語化対応END
            .txtCarNo.IsHissuCheck = True
            .txtCarNo.IsForbiddenWordsCheck = True
            .txtCarNo.IsByteCheck = 20
            .txtCarNo.IsHankakuCheck = True
            If MyBase.IsValidateCheck(.txtCarNo) = False Then
                Return False
            End If

            'If .cmbCarTpKb.SelectedValue.ToString() = "03" Then

            '車輌番号(後)
            '2016.01.06 UMANO 英語化対応START
            '.txtTrailerNo.ItemName = "車輌番号(後)"
            .txtTrailerNo.ItemName = String.Concat(.LmTitleLabel20.Text(), "(", .grpBack.Text(), ")")
            '2016.01.06 UMANO 英語化対応END
            '.txtTrailerNo.IsHissuCheck = True
            .txtTrailerNo.IsForbiddenWordsCheck = True
            .txtTrailerNo.IsByteCheck = 20
            .txtTrailerNo.IsHankakuCheck = True
            If MyBase.IsValidateCheck(.txtTrailerNo) = False Then
                Return False
            End If

            'End If

            '使用可能
            '2016.01.06 UMANO 英語化対応START
            '.cmbAvalYn.ItemName = "使用可能"
            .cmbAvalYn.ItemName = .LmTitleLabel28.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbAvalYn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbAvalYn) = False Then
                Return False
            End If

            '運送会社コード
            '2016.01.06 UMANO 英語化対応START
            '.txtUnsocoCd.ItemName = "運送会社コード"
            .txtUnsocoCd.ItemName = .sprDetail.ActiveSheet.GetColumnLabel(0, LMM200C.SprColumnIndex.UNSOCO_CD)
            '2016.01.06 UMANO 英語化対応END
            .txtUnsocoCd.IsForbiddenWordsCheck = True
            .txtUnsocoCd.IsByteCheck = 5
            .txtUnsocoCd.IsHankakuCheck = True
            If MyBase.IsValidateCheck(.txtUnsocoCd) = False Then
                Return False
            End If

            '運送会社支店コード
            '2016.01.06 UMANO 英語化対応START
            '.txtUnsocoBrCd.ItemName = "運送会社支店コード"
            .txtUnsocoBrCd.ItemName = .sprDetail.ActiveSheet.GetColumnLabel(0, LMM200C.SprColumnIndex.UNSOCO_BR_CD)
            '2016.01.06 UMANO 英語化対応END
            .txtUnsocoBrCd.IsForbiddenWordsCheck = True
            .txtUnsocoBrCd.IsByteCheck = 3
            .txtUnsocoBrCd.IsHankakuCheck = True
            If MyBase.IsValidateCheck(.txtUnsocoBrCd) = False Then
                Return False
            End If

            '温度管理可否区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbTempYn.ItemName = "温度管理可否区分"
            .cmbTempYn.ItemName = .LmTitleLabel18.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbTempYn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbTempYn) = False Then
                Return False
            End If

            '複数温度車室
            '2016.01.06 UMANO 英語化対応START
            '.cmbFukusuOndoYn.ItemName = "複数温度車室"
            .cmbFukusuOndoYn.ItemName = .LmTitleLabel2.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbFukusuOndoYn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbFukusuOndoYn) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSaveExistCheck() As Boolean

        With Me._Frm

            '運送会社コード関連チェック
            If Me.IsUnsoCdChk() = False Then
                Return False
            End If

            If Me._Frm.cmbTempYn.SelectedValue.ToString() = "00" Then
                Return True

            End If

            Dim ondoMm As Decimal = Convert.ToDecimal(.numOndoMm.TextValue)
            Dim ondoMx As Decimal = Convert.ToDecimal(.numOndoMx.TextValue)


            '大小チェック
            If Me._Vcon.IsLargeSmallChk(ondoMm, ondoMx, False) = False Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E182", New String() {"設定可能温度・上限", "設定可能温度・下限"})
                MyBase.ShowMessage("E182", New String() {.LmTitleLabel11.Text(), .LmTitleLabel7.Text()})
                '2016.01.06 UMANO 英語化対応END
                Call Me._Vcon.SetErrorControl(.numOndoMm)
                Call Me._Vcon.SetErrorControl(.numOndoMx)
                Return False
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運送会社コード関連必須チェック/存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsUnsoCdChk() As Boolean

        With Me._Frm

            '2011.08.24 検証結果一覧№8対応 START
            '存在チェック前にセット入力チェック
            If Me.IsUnsocoSetChk(.txtUnsocoCd, .txtUnsocoBrCd) = False Then
                Return False
            End If
            '2011.08.24 検証結果一覧№14対応 END

            Dim brcd As String = .cmbNrsBrCd.SelectedValue.ToString
            Dim unsococd As String = .txtUnsocoCd.TextValue
            Dim unsocobrcd As String = .txtUnsocoBrCd.TextValue

            '運送会社コード、支店コード双方未入力の場合処理終了
            If String.IsNullOrEmpty(unsococd) _
               AndAlso String.IsNullOrEmpty(unsocobrcd) Then
                Return True
            End If

            '2011.08.24 検証結果一覧№14対応 START
            ''運送会社コード関連必須チェック
            'If String.IsNullOrEmpty(unsococd) Then
            '    MyBase.ShowMessage("E224", New String() {"運送会社支店コード", "運送会社コード"})
            '    Me._Vcon.SetErrorControl(New Control() {.txtUnsocoCd, .txtUnsocoBrCd}, .txtUnsocoCd)
            '    Return False
            'ElseIf String.IsNullOrEmpty(unsocobrcd) Then
            '    MyBase.ShowMessage("E224", New String() {"運送会社コード", "運送会社支店コード"})
            '    Me._Vcon.SetErrorControl(New Control() {.txtUnsocoCd, .txtUnsocoBrCd}, .txtUnsocoBrCd)
            '    Return False
            'End If
            '2011.08.24 検証結果一覧№14対応 END

            'マスタ存在チェック
            Dim drs As DataRow() = Nothing

            drs = Me._Vcon.SelectUnsocoListDataRow(brcd, unsococd, unsocobrcd)

            '0件（データなし）
            If drs.Length < 1 Then
                '2011.08.24 残作業一覧 №29対応
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E079", New String() {"運送会社マスタ", String.Concat(unsococd, " - ", unsocobrcd)})
                MyBase.ShowMessage("E829", New String() {String.Concat(unsococd, " - ", unsocobrcd)})
                '2016.01.06 UMANO 英語化対応END
                Call Me.goodsCtlErrSet()
                Return False
            End If

            '2件以上
            If drs.Length > 1 Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E206", New String() {unsococd, "運送会社支店コード"})
                MyBase.ShowMessage("E206", New String() {unsococd, .sprDetail.ActiveSheet.GetColumnLabel(0, LMM200C.SprColumnIndex.UNSOCO_BR_CD)})
                '2016.01.06 UMANO 英語化対応END
                'エラー時コントロール設定
                Call Me.goodsCtlErrSet()
                Return False
            End If

            '1件（データあり）
            .txtUnsocoCd.TextValue = drs(0).Item("UNSOCO_CD").ToString()
            .txtUnsocoBrCd.TextValue = drs(0).Item("UNSOCO_BR_CD").ToString()
            .lblUnsocoNm.TextValue = String.Concat(drs(0).Item("UNSOCO_NM").ToString(), " ", drs(0).Item("UNSOCO_BR_NM").ToString())

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運送会社のセット入力チェック
    ''' </summary>
    ''' <param name="compCd">運送会社コード</param>
    ''' <param name="sitenCd">運送会社支店コード</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsocoSetChk(ByVal compCd As Win.InputMan.LMImTextBox _
                                          , ByVal sitenCd As Win.InputMan.LMImTextBox) As Boolean


        With Me._Frm

            Dim unsocoCd As String = compCd.TextValue
            Dim unsocoBrCd As String = sitenCd.TextValue

            '両方に値がない場合、スルー
            If String.IsNullOrEmpty(unsocoCd) = True _
                AndAlso String.IsNullOrEmpty(unsocoBrCd) = True Then
                Return True
            End If

            '両方に値がある場合、スルー
            If String.IsNullOrEmpty(unsocoCd) = False _
                AndAlso String.IsNullOrEmpty(unsocoBrCd) = False Then
                Return True
            End If

            '片方に値がある場合、エラー
            Dim errorControl As Control() = New Control() {compCd, sitenCd}
            Call Me._Vcon.SetErrorControl(errorControl, compCd)
            '2016.01.06 UMANO 英語化対応START
            'Return Me._Vcon.SetErrMessage("E017", New String() {"運送会社コード", "運送会社支店コード"})
            Return Me._Vcon.SetErrMessage("E017", New String() {.sprDetail.ActiveSheet.GetColumnLabel(0, LMM200C.SprColumnIndex.UNSOCO_CD), .sprDetail.ActiveSheet.GetColumnLabel(0, LMM200C.SprColumnIndex.UNSOCO_BR_CD)})
            '2016.01.06 UMANO 英語化対応END

        End With

    End Function

    ''' <summary>
    ''' 運送会社マスタ存在チェックエラー設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub goodsCtlErrSet()

        With Me._Frm

            .lblUnsocoNm.TextValue = String.Empty
            Call Me._Vcon.SetErrorControl(.txtUnsocoCd)
            Call Me._Vcon.SetErrorControl(.txtUnsocoBrCd)

        End With

    End Sub

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
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleCheck() As Boolean

        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)


            '運送会社コード
            vCell.SetValidateCell(0, LMM200G.sprDetailDef.UNSOCO_CD.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運送会社コード"
            vCell.ItemName = LMM200G.sprDetailDef.UNSOCO_CD.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsHankakuCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社支店コード
            vCell.SetValidateCell(0, LMM200G.sprDetailDef.UNSOCO_BR_CD.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運送会社支店コード"
            vCell.ItemName = LMM200G.sprDetailDef.UNSOCO_BR_CD.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsHankakuCheck = True
            vCell.IsByteCheck = 3
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社名
            vCell.SetValidateCell(0, LMM200G.sprDetailDef.UNSOCO_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運送会社名"
            vCell.ItemName = LMM200G.sprDetailDef.UNSOCO_NM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社支店名
            vCell.SetValidateCell(0, LMM200G.sprDetailDef.UNSOCO_BR_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "支店名"
            vCell.ItemName = LMM200G.sprDetailDef.UNSOCO_BR_NM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '車輌番号(前)
            vCell.SetValidateCell(0, LMM200G.sprDetailDef.CAR_NO.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "車輌番号"
            vCell.ItemName = LMM200G.sprDetailDef.CAR_NO.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsHankakuCheck = True
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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM200C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM200C.EventShubetsu.SHINKI           '新規
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

            Case LMM200C.EventShubetsu.HENSHU          '編集
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

            Case LMM200C.EventShubetsu.HUKUSHA          '複写
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

            Case LMM200C.EventShubetsu.SAKUJO          '削除・復活
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

            Case LMM200C.EventShubetsu.KENSAKU         '検索
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

            Case LMM200C.EventShubetsu.MASTEROPEN          'マスタ参照
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

            Case LMM200C.EventShubetsu.HOZON           '保存
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

            Case LMM200C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM200C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM200C.EventShubetsu.ENTER          'Enter
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM200C.EventShubetsu) As Boolean


        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM200C.EventShubetsu.MASTEROPEN) = True Then
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


                Case .txtUnsocoCd.Name, .txtUnsocoBrCd.Name

                    Dim unsocoNm As String = .lblUnsocoNm.Text
                    ctl = New Win.InputMan.LMImTextBox() {.txtUnsocoCd, .txtUnsocoBrCd}
                    msg = New String() {String.Concat(unsocoNm, LMMControlC.CD), String.Concat(unsocoNm, LMMControlC.BR_CD)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUnsocoNm}


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
    Friend Function IsRecordStatusChk(ByVal frm As LMM200F) As Boolean

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
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM200F, ByVal eventShubetsu As LMM200C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM200C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM200C.EventShubetsu.SAKUJO
        '            msg = "削除・復活"

        '        Case LMM200C.EventShubetsu.HUKUSHA
        '            msg = "複写"

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
