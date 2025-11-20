' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM320V : 請求項目マスタメンテナンス
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
''' LMM320Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM320V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM320F


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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM320F, ByVal v As LMMControlV)

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
    ''' 入力チェック。
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
    ''' 保存時のtrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm
            .txtComment.TextValue = .txtComment.TextValue.Trim()
            .txtSeiqkmkCd.TextValue = .txtSeiqkmkCd.TextValue.Trim()
            .txtSeiqkmkCdS.TextValue = .txtSeiqkmkCdS.TextValue.Trim()
            .txtSeiqknkNm.TextValue = .txtSeiqknkNm.TextValue.Trim()
            .numInJyun.TextValue = .numInJyun.TextValue.Trim()
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
            '請求グループコード
            .cmbGroupCd.ItemName = "請求グループコード"
            .cmbGroupCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbGroupCd) = False Then
                'エラーあり
                Return False
            End If
            '請求項目コード
            .txtSeiqkmkCd.ItemName = "請求項目コード"
            .txtSeiqkmkCd.IsHissuCheck = True
            .txtSeiqkmkCd.IsForbiddenWordsCheck = True
            .txtSeiqkmkCd.IsFullByteCheck = 2
            .txtSeiqkmkCd.IsHankakuCheck = True
            If MyBase.IsValidateCheck(.txtSeiqkmkCd) = False Then
                'エラーあり
                Return False
            End If
            '請求項目コード小分類
            .txtSeiqkmkCdS.ItemName = "請求項目コード小分類"
            .txtSeiqkmkCdS.IsHissuCheck = False
            .txtSeiqkmkCdS.IsForbiddenWordsCheck = True
            .txtSeiqkmkCdS.IsHankakuCheck = True
            If MyBase.IsValidateCheck(.txtSeiqkmkCdS) = False Then
                'エラーあり
                Return False
            End If
            '請求項目名
            .txtSeiqknkNm.ItemName = "請求項目名"
            .txtSeiqknkNm.IsHissuCheck = True
            .txtSeiqknkNm.IsForbiddenWordsCheck = True
            .txtSeiqknkNm.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtSeiqknkNm) = False Then
                'エラーあり
                Return False
            End If
            '課税区分
            .cmbTaxKb.ItemName = "課税区分"
            .cmbTaxKb.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbTaxKb) = False Then
                'エラーあり
                Return False
            End If
            '経理科目コード
            .cmbKeiriCd.ItemName = "経理科目コード"
            .cmbKeiriCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbKeiriCd) = False Then
                'エラーあり
                Return False
            End If
            '備考
            .txtComment.ItemName = "備考"
            .txtComment.IsForbiddenWordsCheck = True
            .txtComment.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtComment) = False Then
                'エラーあり
                Return False
            End If

            '印順
            .numInJyun.ItemName = "印順"
            .numInJyun.IsHissuCheck = True
            If MyBase.IsValidateCheck(.numInJyun) = False Then
                'エラーあり
                Return False
            End If
        End With

        '請求項目コード小分類（内容チェック）
        Dim groupCd As String = Me._Frm.cmbGroupCd.SelectedValue.ToString()
        Dim seiqkmkCdS As String = Me._Frm.txtSeiqkmkCdS.TextValue
        Select Case groupCd
            Case "03", "05"
                '運賃、横持料
                '未入力および"1"を許可
                If (Not String.IsNullOrEmpty(seiqkmkCdS)) AndAlso (Not "1".Equals(seiqkmkCdS)) Then
                    MyBase.ShowMessage("E02H")
                    Me.SetErrorControl(Me._Frm.txtSeiqkmkCdS)
                    Return False
                End If
            Case Else
                '運賃、横持料以外
                '未入力のみ許可
                If Not String.IsNullOrEmpty(seiqkmkCdS) Then
                    MyBase.ShowMessage("E02H")
                    Me.SetErrorControl(Me._Frm.txtSeiqkmkCdS)
                    Return False
                End If
        End Select

        '印順（範囲チェック）
        Dim _MinHani As Integer = 0     '範囲最小値
        Dim _MaxHani As Integer = 99    '範囲最大値
        Dim _InJyunStr As String = Convert.ToString(Me._Frm.numInJyun.Value)
        Dim _InJyunInt As Integer = Convert.ToInt32(_InJyunStr)

        '印順（範囲チェック（最小値））
        If _MinHani <= _InJyunInt = False Then

            MyBase.ShowMessage("E014", New String() {"印順", Convert.ToString(_MinHani), Convert.ToString(_MaxHani)})
            Me.SetErrorControl(Me._Frm.numInJyun)
            Return False

        End If

        '印順（範囲チェック（最大値））
        If _InJyunInt <= _MaxHani = False Then
            MyBase.ShowMessage("E014", New String() {"印順", Convert.ToString(_MinHani), Convert.ToString(_MaxHani)})
            Me.SetErrorControl(Me._Frm.numInJyun)
            Return False
        End If
        Dim _InJyunLength As Integer = 2
        '印順（桁数チェック)
        Dim _InJyun As String = Convert.ToString(_InJyunInt)
        If _InJyun.Length <= _InJyunLength = False Then
            MyBase.ShowMessage("E023", New String() {"印順", Convert.ToString(_InJyunLength)})
            Me.SetErrorControl(Me._Frm.numInJyun)
            Return False
        End If

        Return True

    End Function


    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM320F) As Boolean

        If frm.lblSituation.RecordStatus = RecordStatus.DELETE_REC Then
            MyBase.ShowMessage("E035")
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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM320C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM320C.EventShubetsu.SHINKI           '新規
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

            Case LMM320C.EventShubetsu.HENSHU          '編集
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

            Case LMM320C.EventShubetsu.HUKUSHA          '複写
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


            Case LMM320C.EventShubetsu.SAKUJO          '削除・復活
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

            Case LMM320C.EventShubetsu.KENSAKU         '検索
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

            Case LMM320C.EventShubetsu.MASTEROPEN          'マスタ参照
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

            Case LMM320C.EventShubetsu.HOZON           '保存
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

            Case LMM320C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM320C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM320C.EventShubetsu.ENTER          'Enter
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
    ''' 検索押下時スプレッド単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleCheck() As Boolean

        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '印順
            vCell.SetValidateCell(0, LMM320G.sprDetailDef.PRINT_SORT.ColNo)
            vCell.ItemName = "印順"
            vCell.IsByteCheck = 2
            vCell.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '請求項目コード
            vCell.SetValidateCell(0, LMM320G.sprDetailDef.SEIQKMK_CD.ColNo)
            vCell.ItemName = "請求項目コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '請求項目名
            vCell.SetValidateCell(0, LMM320G.sprDetailDef.SEIQKMK_NM.ColNo)
            vCell.ItemName = "請求項目名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '備考
            vCell.SetValidateCell(0, LMM320G.sprDetailDef.REMARK.ColNo)
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
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM320C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing

        Return False

    End Function

#End Region 'Method

End Class
