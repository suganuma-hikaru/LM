' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM360V : 請求テンプレートマスタメンテ
'  作  成  者       :  [kishi]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMM360Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM360V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM360F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM360F, ByVal v As LMMControlV)

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

        '関連チェック
        rtnResult = rtnResult AndAlso Me.IsInputConnectionChk()

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputConnectionChk() As Boolean

        With Me._Frm

            Dim lCd As String = .txtSeiqkmkCd.TextValue
            Dim mCd As String = .cmbGroupKbn.SelectedValue.ToString()

            '両方に値がある場合、スルー
            If String.IsNullOrEmpty(lCd) = False _
                AndAlso String.IsNullOrEmpty(mCd) = False Then
                Return True
            End If

            '両方に値がない場合、スルー
            If String.IsNullOrEmpty(lCd) = True _
                AndAlso String.IsNullOrEmpty(mCd) = True Then
                Return True
            End If

            .cmbGroupKbn.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Call Me._Vcon.SetErrorControl(.txtSeiqkmkCd)
            Return Me._Vcon.SetErrMessage("E017", New String() {"請求項目コード", "請求グループコード区分"})

        End With

    End Function
    ''' <summary>
    ''' マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMstExistChk() As Boolean

        '請求先マスタ存在チェック
        Dim rtnResult As Boolean = Me.IsSeiqtoExistCheck()

        If String.IsNullOrEmpty(Me._Frm.txtSeiqkmkCd.TextValue) = False Then
            '請求項目マスタ存在チェック
            rtnResult = rtnResult AndAlso Me.IsSeiqkmkExistCheck()
        End If
        
        Return rtnResult

    End Function
    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM360F) As Boolean

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
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM360F, ByVal eventShubetsu As LMM360C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM360C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM360C.EventShubetsu.HUKUSHA
        '            msg = "複写"

        '        Case LMM360C.EventShubetsu.SAKUJO
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

            .txtSeiqtoCd.TextValue = .txtSeiqtoCd.TextValue.Trim()
            .txtPtnCd.TextValue = .txtPtnCd.TextValue.Trim()
            .txtSeiqkmkCd.TextValue = .txtSeiqkmkCd.TextValue.Trim()
            .txtSeiqkmkCdS.TextValue = .txtSeiqkmkCdS.TextValue.Trim()
            .txtTekiyo.TextValue = .txtTekiyo.TextValue.Trim()

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
            .cmbNrsBrCd.ItemName = .lblTitleEigyo.Text
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            '請求先コード（2011.08.30 検証結果一覧№53対応）
            .txtSeiqtoCd.ItemName = .lblTitleSeiqCd.Text
            .txtSeiqtoCd.IsHissuCheck = True
            .txtSeiqtoCd.IsForbiddenWordsCheck = True
            .txtSeiqtoCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtSeiqtoCd) = False Then
                Return False
            End If

            '請求パターンコード
            .txtPtnCd.ItemName = .lblTitlePtnCd.Text
            .txtPtnCd.IsHissuCheck = True
            .txtPtnCd.IsForbiddenWordsCheck = True
            .txtPtnCd.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtPtnCd) = False Then
                Return False
            End If

            '真荷主コード
            If .lblTcustBpNm.HissuLabelVisible Then
                '外部倉庫用ABP対策として必須マークがある場合のみ
                .txtTcustBpCd.ItemName = .lblTitleTcustBpCd.Text
                .txtTcustBpCd.IsHissuCheck = True
                .txtTcustBpCd.IsForbiddenWordsCheck = True
                .txtTcustBpCd.IsByteCheck = 10
                If MyBase.IsValidateCheck(.txtTcustBpCd) = False Then
                    Return False
                End If
            End If

            '2011.08.25 検証結果一覧№40対応 START
            '請求グループコード
            .cmbGroupKbn.ItemName = .lblTitleGroupKbn.Text
            .cmbGroupKbn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbGroupKbn) = False Then
                Return False
            End If
            '2011.08.25 検証結果一覧№40対応 END

            '請求項目コード
            .txtSeiqkmkCd.ItemName = .lblTitleSeiqkmkCd.Text
            .txtSeiqkmkCd.IsHissuCheck = True    '2011.08.25 検証結果一覧№40対応
            .txtSeiqkmkCd.IsForbiddenWordsCheck = True
            .txtSeiqkmkCd.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtSeiqkmkCd) = False Then
                Return False
            End If

            '請求項目コード小分類
            .txtSeiqkmkCdS.ItemName = .lblTitleSeiqkmkCdS.Text
            .txtSeiqkmkCdS.IsHissuCheck = False
            .txtSeiqkmkCdS.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(.txtSeiqkmkCdS) = False Then
                Return False
            End If

            '摘要
            .txtTekiyo.ItemName = .lblTitleTekiyo.Text
            .txtTekiyo.IsForbiddenWordsCheck = True
            .txtTekiyo.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtTekiyo) = False Then
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

            '請求先コード
            vCell.SetValidateCell(0, LMM360G.sprDetailDef.SEIQTO_CD.ColNo)
            vCell.ItemName = "請求先コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsHankakuCheck = True
            vCell.IsByteCheck = 7
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '請求先名
            vCell.SetValidateCell(0, LMM360G.sprDetailDef.SEIQTO_NM.ColNo)
            vCell.ItemName = "請求先名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '請求パターンコード
            vCell.SetValidateCell(0, LMM360G.sprDetailDef.PTN_CD.ColNo)
            vCell.ItemName = "請求パターンコード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '請求項目コード
            vCell.SetValidateCell(0, LMM360G.sprDetailDef.SEIQKMK_CD.ColNo)
            vCell.ItemName = "請求項目コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '請求項目コード小分類
            vCell.SetValidateCell(0, LMM360G.sprDetailDef.SEIQKMK_CD_S.ColNo)
            vCell.ItemName = "請求項目コード小分類"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '摘要
            vCell.SetValidateCell(0, LMM360G.sprDetailDef.TEKIYO.ColNo)
            vCell.ItemName = "摘要"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 請求先コード存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSeiqtoExistCheck() As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing

            drs = Me._Vcon.SelectSeiqtoListDataRow(.cmbNrsBrCd.SelectedValue.ToString(), .txtSeiqtoCd.TextValue)

            If drs.Length < 1 Then
                MyBase.ShowMessage("E079", New String() {"請求先マスタ", .txtSeiqtoCd.TextValue})
                .lblSeiqNm.TextValue = String.Empty
                Me.SetErrorControl(.txtSeiqtoCd)
                Return False
            End If

            .txtSeiqtoCd.TextValue = drs(0).Item("SEIQTO_CD").ToString()
            .lblSeiqNm.TextValue = String.Concat(drs(0).Item("SEIQTO_NM").ToString(), " ", drs(0).Item("SEIQTO_BUSYO_NM").ToString())

        End With

        Return True


    End Function

    ''' <summary>
    ''' 請求項目コード存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSeiqkmkExistCheck() As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing

            If Me._Vcon.SelectSeiqkmkListDataRow(drs, .txtSeiqkmkCd.TextValue, .txtSeiqkmkCdS.TextValue, .cmbGroupKbn.SelectedValue.ToString()) = False Then
                .cmbGroupKbn.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                .txtSeiqkmkCdS.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Call Me._Vcon.SetErrorControl(.txtSeiqkmkCd)
                Return False
            End If

            .txtSeiqkmkCd.TextValue = drs(0).Item("SEIQKMK_CD").ToString()
            .txtSeiqkmkCdS.TextValue = drs(0).Item("SEIQKMK_CD_S").ToString()

        End With

        Return True


    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM360C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM360C.EventShubetsu.SHINKI           '新規
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

            Case LMM360C.EventShubetsu.HENSHU          '編集
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

            Case LMM360C.EventShubetsu.HUKUSHA          '複写
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


            Case LMM360C.EventShubetsu.SAKUJO          '削除・復活
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

            Case LMM360C.EventShubetsu.KENSAKU         '検索
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

            Case LMM360C.EventShubetsu.MASTEROPEN          'マスタ参照
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

            Case LMM360C.EventShubetsu.HOZON           '保存
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

            Case LMM360C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM360C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM360C.EventShubetsu.ENTER          'Enter
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

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM360C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM360C.EventShubetsu.MASTEROPEN) = True Then
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

                Case .txtSeiqtoCd.Name

                    ctl = New Win.InputMan.LMImTextBox() { .txtSeiqtoCd}
                    msg = New String() { .lblTitleSeiqCd.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() { .lblSeiqNm}

                Case .txtTcustBpCd.Name

                    ctl = New Win.InputMan.LMImTextBox() { .txtTcustBpCd}
                    msg = New String() { .lblTcustBpNm.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() { .lblTcustBpNm}

                Case .txtSeiqkmkCd.Name, .txtSeiqkmkCdS.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtSeiqkmkCd}
                    msg = New String() {.lblTitleSeiqkmkCd.Text}
                    '2011.08.25 検証結果一覧(画面共通)対応
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.cmbGroupKbn, .lblSeiqkmkNm}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl, clearCtl)

        End With

    End Function

#End Region 'Method

End Class
