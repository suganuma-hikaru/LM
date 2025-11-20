' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM060V : 運賃マスタメンテナンス
'  作  成  者       :  [kishi]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMM060Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM060V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM060F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMMControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMMControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM060F, ByVal v As LMMControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._ControlV = v

        'Gamen共通クラスの設定
        Me._ControlG = New LMMControlG(handlerClass, DirectCast(frm, Form))

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM060C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM060C.EventShubetsu.SHINKI           '新規
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

            Case LMM060C.EventShubetsu.HENSHU          '編集
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

            Case LMM060C.EventShubetsu.HUKUSHA          '複写
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


            Case LMM060C.EventShubetsu.SAKUJO          '削除・復活
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

            Case LMM060C.EventShubetsu.EXCELINPUT  'EXCEL取込
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

            Case LMM060C.EventShubetsu.EXCELOUTPUT  'EXCEL出力
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

            Case LMM060C.EventShubetsu.KENSAKU         '検索
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

            Case LMM060C.EventShubetsu.MASTEROPEN          'マスタ参照
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

            Case LMM060C.EventShubetsu.HOZON           '保存
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

            Case LMM060C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM060C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM060C.EventShubetsu.ENTER          'Enter
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
    ''' 編集押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsHenshuChk() As Boolean

        Dim msg As String = "編集"

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtUnchinTariffCd.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbNrsBrCd.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

        'データ存在チェックを行う
        If Me._ControlV.IsRecordStatusChk(Me._Frm.lblSituation.RecordStatus) = False Then
            Return False
        End If

        '承認申請中は編集不可
        If "09".Equals(Me._Frm.lblApprovalCd.TextValue) Then
            MyBase.ShowMessage("E01U", New String() {"承認申請中につき編集できません。"})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 複写押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsFukushaChk() As Boolean

        Dim msg As String = "複写"

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtUnchinTariffCd.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbNrsBrCd.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 削除/復活押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsSakujoHukkatuChk() As Boolean

        Dim msg As String = "削除・復活"

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtUnchinTariffCd.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbNrsBrCd.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

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
    Friend Function IsSaveInputChk(Optional ByVal sysDate As String = "") As Boolean

        With Me._Frm

            'スペース除去
            Call Me._ControlV.TrimSpaceTextvalue(Me._Frm)
            Call Me._ControlV.TrimSpaceSprTextvalue(.sprDetail2 _
                                                    , .sprDetail2.ActiveSheet.Rows.Count - 1 _
                                                    , LMM060G.sprDetailDef2.FRRY_EXTC_PART.ColNo)

            '単項目チェック(編集部)
            Dim rtnResult As Boolean = Me.IsSaveSingleCheck()

            '単項目チェック(運賃タリフ(距離刻み/運賃)Spread)
            rtnResult = rtnResult AndAlso Me.IsUnchinTariffChk()

            'マスタ存在チェック
            rtnResult = rtnResult AndAlso Me.IsMstExistChk()

            '関連チェック
            rtnResult = rtnResult AndAlso Me.IsSaveRelationChk(sysDate)

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleCheck() As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '**********編集部のチェック
            '営業所
            .cmbNrsBrCd.ItemName = .lblNrsBrCd.Text
            .cmbNrsBrCd.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = errorFlg Then
                Return errorFlg
            End If

            '運賃タリフコード
            .txtUnchinTariffCd.ItemName = .lblUnchinTariffCd.Text
            .txtUnchinTariffCd.IsHissuCheck = chkFlg
            .txtUnchinTariffCd.IsForbiddenWordsCheck = chkFlg
            .txtUnchinTariffCd.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtUnchinTariffCd) = errorFlg Then
                Return errorFlg
            End If

            '適用開始日
            .imdStrDate.ItemName = .lblStrDate.Text
            .imdStrDate.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.imdStrDate) = errorFlg Then
                Return errorFlg
            End If
            If .imdStrDate.IsDateFullByteCheck = errorFlg Then
                MyBase.ShowMessage("E038", New String() {.lblStrDate.Text, "8"})
                Call Me._ControlV.SetErrorControl(.imdStrDate)
                Return errorFlg
            End If

            'データタイプ
            .cmbDataTp.ItemName = .lblDataTp.Text
            .cmbDataTp.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbDataTp) = errorFlg Then
                Return errorFlg
            End If

            'テーブルタイプ
            .cmbTableTp.ItemName = .lblTableTp.Text
            .cmbTableTp.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbTableTp) = errorFlg Then
                Return errorFlg
            End If

            '備考
            .txtUnchinTariffRem.ItemName = .lblUnchinTariffRem.Text
            .txtUnchinTariffRem.IsForbiddenWordsCheck = chkFlg
            .txtUnchinTariffRem.IsByteCheck = 100
            '2019/07/11 依頼番号:006680 add start
            .txtUnchinTariffRem.IsHissuCheck = chkFlg
            '2019/07/11 依頼番号:006680 add end
            If MyBase.IsValidateCheck(.txtUnchinTariffRem) = errorFlg Then
                Return errorFlg
            End If

            '2次タリフコード
            .txtUnchinTariffCd2.ItemName = .lblUnchinTariffCd2.Text
            .txtUnchinTariffCd2.IsForbiddenWordsCheck = chkFlg
            .txtUnchinTariffCd2.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtUnchinTariffCd2) = errorFlg Then
                Return errorFlg
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 運賃タリフ(距離刻み/運賃)Spreadの単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsUnchinTariffChk() As Boolean

        Return True

    End Function

    ''' <summary>
    ''' マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMstExistChk() As Boolean

        '運賃タリフマスタ存在チェック（運賃タリフコード）
        Dim rtnResult As Boolean = Me.IsUnchinTariffExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinTariffExistChk() As Boolean

        With Me._Frm

            '「運賃タリフコード」チェック
            Dim unchintariffCd As String = String.Empty
            Dim startDate As String = String.Empty

            unchintariffCd = .txtUnchinTariffCd2.TextValue
            startDate = .imdStrDate.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(unchintariffCd) = True Then
                '次項目のチェックへ
            Else
                '運賃タリフマスタの存在チェック
                Dim drs As DataRow() = Nothing
                drs = Me._ControlV.SelectUnchinTariffListDataRow2(unchintariffCd, , startDate)
                If drs.Length < 1 Then
                    MyBase.ShowMessage("E079", New String() {"運賃タリフマスタ", unchintariffCd})
                    .txtUnchinTariffCd2.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Call Me._ControlV.SetErrorControl(.txtUnchinTariffCd2)
                    Return False
                End If

            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 保存押下時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveRelationChk(ByVal sysDate As String) As Boolean

        Dim ctl As Control() = Nothing
        Dim focus As Control = Nothing
        Dim chkWt As Boolean = False
        Dim chkCarTp As Boolean = False
        Dim chkNb As Boolean = False
        Dim chkQt As Boolean = False
        Dim chkTsize As Boolean = False
        Dim msg As String = String.Empty

        '**********編集部のチェック

        With Me._Frm

            ''【データタイプ(登録可否チェック)】
            'Select Case .lblSituation.RecordStatus
            '    Case RecordStatus.NEW_REC, RecordStatus.COPY_REC

            '        If (LMM060C.DATA_TP_KBN_00).Equals(.cmbDataTp.SelectedValue) = True Then
            '            If (RecordStatus.NEW_REC).Equals(.lblSituation.RecordStatus) = True Then
            '                msg = .FunctionKey.F1ButtonName.Replace("　", String.Empty)
            '            ElseIf (RecordStatus.COPY_REC).Equals(.lblSituation.RecordStatus) = True Then
            '                msg = .FunctionKey.F3ButtonName.Replace("　", String.Empty)
            '            End If
            '            MyBase.ShowMessage("E187", New String() {msg, "00（距離刻み）以外"})
            '            ctl = New Control() {.cmbDataTp}
            '            focus = .cmbDataTp
            '            Call Me._ControlV.SetErrorControl(ctl, focus)
            '            Return False
            '        End If

            'End Select

            '【運賃タリフコード・2次タリフコード(同一チェック)】            
            If .txtUnchinTariffCd.TextValue.Equals(.txtUnchinTariffCd2.TextValue) = True Then
                MyBase.ShowMessage("E220", New String() {.lblUnchinTariffCd.Text, .lblUnchinTariffCd2.Text, "タリフコード"})
                ctl = New Control() {.txtUnchinTariffCd, .txtUnchinTariffCd2}
                focus = .txtUnchinTariffCd
                Call Me._ControlV.SetErrorControl(ctl, focus)
                Return False
            End If

            '【運賃タリフコード・2次タリフコード(世代チェック_①)】
            '値がない場合、スルー
            If String.IsNullOrEmpty(.txtUnchinTariffCd.TextValue) = True Then
                'チェックなし
            Else
                Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_R).Select(String.Concat("UNCHIN_TARIFF_CD2 = '", .txtUnchinTariffCd.TextValue, "' AND ", _
                                                                                                                        "SYS_DEL_FLG = '", LMM060C.FLG.OFF, "' "))
                If dr.Length > 0 Then
                    MyBase.ShowMessage("E205", New String() {.lblUnchinTariffCd.Text, .lblUnchinTariffCd2.Text})
                    ctl = New Control() {.txtUnchinTariffCd, .txtUnchinTariffCd2}
                    focus = .txtUnchinTariffCd
                    Call Me._ControlV.SetErrorControl(ctl, focus)
                    Return False
                End If
            End If

            '【運賃タリフコード・2次タリフコード(世代チェック_②)】
            '値がない場合、スルー
            If String.IsNullOrEmpty(.txtUnchinTariffCd2.TextValue) = True Then
                'チェックなし
            Else
                Dim Space As String = String.Empty
                Dim dr2 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_R).Select(String.Concat("UNCHIN_TARIFF_CD = '", .txtUnchinTariffCd2.TextValue, "' AND ", _
                                                                                                                        "UNCHIN_TARIFF_CD2 IS NOT NULL  AND ", _
                                                                                                                        "UNCHIN_TARIFF_CD2 <> '", Space, "'AND ", _
                                                                                                                        "SYS_DEL_FLG = '", LMM060C.FLG.OFF, "' "))
                If dr2.Length > 0 Then
                    MyBase.ShowMessage("E205", New String() {.lblUnchinTariffCd2.Text, .lblUnchinTariffCd.Text})
                    ctl = New Control() {.txtUnchinTariffCd2, .txtUnchinTariffCd}
                    focus = .txtUnchinTariffCd2
                    Call Me._ControlV.SetErrorControl(ctl, focus)
                    Return False
                End If
            End If

            '【運賃タリフコード・2次タリフコード(整合性チェック)】            
            '値がない場合、スルー
            If String.IsNullOrEmpty(.txtUnchinTariffCd2.TextValue) = True Then
                'チェックなし
            Else
                Dim MaxStrDate As DataRow() = Nothing
                Dim StrDate As String = String.Empty
                MaxStrDate = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_R).Select(Me.SetUnchinTafSql(.cmbNrsBrCd.SelectedValue.ToString, sysDate, .txtUnchinTariffCd2.TextValue), "STR_DATE DESC")
                '上記条件でMAX適用開始日が取得できた場合のみチェックを行う。(取得できない場合は2次タリフコードとして登録されてないのでチェックなし)
                If MaxStrDate.Length > 0 Then
                    StrDate = MaxStrDate(0).Item("STR_DATE").ToString
                    Dim dr3 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_R).Select(String.Concat("DATA_TP = '", .cmbDataTp.SelectedValue, "' AND ", _
                                                                                                                "TABLE_TP = '", .cmbTableTp.SelectedValue, "' AND ", _
                                                                                                                "SYS_DEL_FLG = '", LMM060C.FLG.OFF, "' AND ", _
                                                                                                                "STR_DATE = '", StrDate, "' "))
                    If dr3.Length < 1 Then
                        MyBase.ShowMessage("E268", New String() {.lblUnchinTariffCd2.Text})
                        ctl = New Control() {.txtUnchinTariffCd2}
                        focus = .txtUnchinTariffCd2
                        Call Me._ControlV.SetErrorControl(ctl, focus)
                        Return False
                    End If
                End If
            End If

        End With

        '**********運賃タリフ(距離刻み/運賃)スプレッドのチェック
        '【重量・車種・個数・数量・宅急便サイズ(必須チェック)の判定処理】※以下の必須チェックは、データタイプ＝"00"(距離刻み)の場合は除く

        With Me._Frm

            '①計算種別＝"00"（重量・距離）、"03"（重量建て）、"07"（重量・県（重量建））の場合、重量は必須
            If .cmbDataTp.SelectedValue.Equals(LMM060C.DATA_TP_KBN_00) = False _
               AndAlso (.cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_00) = True _
               OrElse .cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_03) = True _
               OrElse .cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_07) = True) Then
                chkWt = True
                msg = "重量"
            End If
            '②計算種別＝"01"（車種・距離）の場合、車種は必須
            If .cmbDataTp.SelectedValue.Equals(LMM060C.DATA_TP_KBN_00) = False _
               AndAlso .cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_01) = True Then
                chkCarTp = True
                msg = "車種"
            End If
            '③計算種別＝"02"（個数建て）、"05"（個数・県）の場合、個数は必須
            If .cmbDataTp.SelectedValue.Equals(LMM060C.DATA_TP_KBN_00) = False _
               AndAlso (.cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_02) = True _
               OrElse .cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_05) = True) Then
                chkNb = True
                msg = "個数"
            End If
            '④計算種別＝"04"（数量建て）の場合、数量は必須
            If .cmbDataTp.SelectedValue.Equals(LMM060C.DATA_TP_KBN_00) = False _
               AndAlso .cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_04) = True Then
                chkQt = True
                msg = "数量"
            End If
            '⑤計算種別＝"06"（宅急便サイズ）の場合、宅急便サイズは必須
            If .cmbDataTp.SelectedValue.Equals(LMM060C.DATA_TP_KBN_00) = False _
               AndAlso .cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_06) = True Then
                chkTsize = True
                msg = "宅急便サイズ"
            End If
        End With

        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetail2)

        With vCell

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False
            Dim spr As LMSpread = Me._Frm.sprDetail2
            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1

            '【データタイプ・距離刻み・運賃Spread(整合性チェック)】データタイプ＝"00"(距離刻み)かつ、距離刻み・運賃Spreadに行（運賃ﾃﾞｰﾀ）が存在した場合はエラー
            If (RecordStatus.NEW_REC).Equals(Me._Frm.lblSituation.RecordStatus) = True _
               OrElse (RecordStatus.COPY_REC).Equals(Me._Frm.lblSituation.RecordStatus) = True Then
                If Me._Frm.cmbDataTp.SelectedValue.Equals(LMM060C.DATA_TP_KBN_00) = True AndAlso max > 1 Then
                    MyBase.ShowMessage("E211", New String() {String.Concat(Me._Frm.lblDataTp.Text, "が00（距離刻み）"), "運賃"})
                    Return errorFlg
                End If
            End If

            '【重量・車種・個数・数量・宅急便サイズ(必須チェック)】
            '１)計算種別に対応する「重量・車種・個数・数量・宅急便サイズ」が追加されていない場合エラー　※データタイプ＝"00"(距離刻み)の場合は除く
            'max = 1の場合、距離刻みしか入力されていない。
            If (chkWt = True AndAlso max = 1) _
             OrElse (chkCarTp = True AndAlso max = 1) _
             OrElse (chkNb = True AndAlso max = 1) _
             OrElse (chkQt = True AndAlso max = 1) _
             OrElse (chkTsize = True AndAlso max = 1) Then
                MyBase.ShowMessage("E001", New String() {msg})
                Return errorFlg
            End If

            '２)計算種別に対応する「重量・車種・個数・数量・宅急便サイズ」が追加されているが値が0や空白の場合エラー　※データタイプ＝"00"(距離刻み)の場合は除く
            For i As Integer = 2 To max

                '既存データの場合(UPD_FLG＝'1')はチェックなし
                If (LMConst.FLG.ON).Equals(_ControlG.GetCellValue(Me._Frm.sprDetail2.ActiveSheet.Cells(i, LMM060C.SprColumnIndex2.UPD_FLG))) = False Then

                    If chkWt = True Then
                        '重量
                        .SetValidateCell(i, LMM060G.sprDetailDef2.WT_LV.ColNo)
                        .ItemName = LMM060G.sprDetailDef2.WT_LV.ColName
                        If (LMM060C.WT).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, LMM060G.sprDetailDef2.WT_LV.ColNo))) = True Then
                            MyBase.ShowMessage("E001", New String() {.ItemName})
                            Return errorFlg
                        End If
                    End If

                    If chkCarTp = True Then
                        '車種
                        .SetValidateCell(i, LMM060G.sprDetailDef2.CAR_TP.ColNo)
                        .ItemName = LMM060G.sprDetailDef2.CAR_TP.ColName
                        .IsHissuCheck = chkFlg
                        If MyBase.IsValidateCheck(vCell) = errorFlg Then
                            Return errorFlg
                        End If
                    End If

                    If chkNb = True Then
                        '個数
                        .SetValidateCell(i, LMM060G.sprDetailDef2.NB.ColNo)
                        .ItemName = LMM060G.sprDetailDef2.NB.ColName
                        If (LMM060C.WT).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, LMM060G.sprDetailDef2.NB.ColNo))) = True Then
                            MyBase.ShowMessage("E001", New String() {.ItemName})
                            Return errorFlg
                        End If
                    End If

                    If chkQt = True Then
                        '数量
                        .SetValidateCell(i, LMM060G.sprDetailDef2.QT.ColNo)
                        .ItemName = LMM060G.sprDetailDef2.QT.ColName
                        If (LMM060C.WT).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, LMM060G.sprDetailDef2.QT.ColNo))) = True Then
                            MyBase.ShowMessage("E001", New String() {.ItemName})
                            Return errorFlg
                        End If
                    End If

                    If chkTsize = True Then
                        '宅急便サイズ
                        .SetValidateCell(i, LMM060G.sprDetailDef2.T_SIZE.ColNo)
                        .ItemName = LMM060G.sprDetailDef2.T_SIZE.ColName
                        .IsHissuCheck = chkFlg
                        If MyBase.IsValidateCheck(vCell) = errorFlg Then
                            Return errorFlg
                        End If
                    End If

                End If

                '【距離1～70（距離刻み）(必須チェック)】運賃が入力されていて(0.000以外)それに対応する距離刻みが未入力の場合はエラー
                For t As Integer = 0 To 90
                    If LMM060C.SprColumnIndex2.KYORI_1 <= t And t <= LMM060C.SprColumnIndex2.KYORI_70 Then
                        If (True).Equals(spr.ActiveSheet.Columns(t).Visible) = True Then
                            If Convert.ToDecimal(LMM060C.UNCHIN).Equals(Convert.ToDecimal(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, t)))) = False _
                             AndAlso Convert.ToDecimal(LMM060C.UNCHIN).Equals(Convert.ToDecimal(_ControlG.GetCellValue(spr.ActiveSheet.Cells(1, t)))) = True Then
                                MyBase.ShowMessage("E224", New String() {"運賃", "対応する距離刻み"})
                                Return False
                            End If
                        End If
                    End If
                Next

            Next

        End With

        '【距離1～70（距離刻み）(大小チェック)】距離刻みが左から右へ値が大きくなるように入力されていない場合はエラー
        Dim sprDetail2 As LMSpread = Me._Frm.sprDetail2

        With sprDetail2

            For i As Integer = 0 To 90
                If LMM060C.SprColumnIndex2.KYORI_1 <= i And i <= LMM060C.SprColumnIndex2.KYORI_70 Then
                    If i <> 6 Then  '距離1は前の距離が存在しないので,チェックなし
                        If (True).Equals(.ActiveSheet.Columns(i).Visible) = True AndAlso _
                           (True).Equals(.ActiveSheet.Columns(i - 1).Visible) = True Then
                            '比較する距離刻みがどちらも0.000の場合,チェックなし
                            Dim kyoriF As String = _ControlG.GetCellValue(.ActiveSheet.Cells(1, (i - 1)))
                            Dim kyoriB As String = _ControlG.GetCellValue(.ActiveSheet.Cells(1, i))
                            If Convert.ToDecimal(kyoriF) = Convert.ToDecimal(LMM060C.UNCHIN) AndAlso _
                               Convert.ToDecimal(kyoriB) = Convert.ToDecimal(LMM060C.UNCHIN) Then
                                'チェックなし
                            Else
                                If Convert.ToDecimal(kyoriB) <= Convert.ToDecimal(kyoriF) Then
                                    MyBase.ShowMessage("E271", New String() {"距離刻み", "左から右へ値が大きく"})
                                    Return False
                                End If
                            End If
                        End If
                    End If
                End If
            Next

        End With

        Return True

    End Function

    Friend Function SetUnchinTafSql(ByVal value As String, ByVal value2 As String, ByVal value3 As String) As String


        Return String.Concat("NRS_BR_CD = '", value, "' " _
                                              , "AND STR_DATE <= '", value2, "' " _
                                              , "AND UNCHIN_TARIFF_CD2 = '", value3, "' " _
                                              , "AND SYS_DEL_FLG = '0' " _
                                             )

    End Function



    ''' <summary>
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean

        'スペース除去
        Call Me.TrinmFindRow()

        '単項目チェック
        If Me.IsKensakuSingleChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 行追加/行削除 入力チェック。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsRowCheck(ByVal eventShubetsu As LMM060C.EventShubetsu, ByVal frm As LMM060F) As Boolean
        Dim arr As ArrayList = Nothing

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False
        Dim ctl As Control() = Nothing
        Dim focus As Control = Nothing

        Select Case eventShubetsu

            Case LMM060C.EventShubetsu.INS_T    '行追加

                With Me._Frm

                    'データタイプ(行追加チェック)
                    .cmbDataTp.ItemName = .lblDataTp.Text
                    If (LMM060C.DATA_TP_KBN_00).Equals(.cmbDataTp.SelectedValue) = chkFlg Then
                        MyBase.ShowMessage("E211", New String() {String.Concat(.lblDataTp.Text, "が00（距離刻み）"), "運賃"})
                        ctl = New Control() {.cmbDataTp}
                        focus = .cmbDataTp
                        Call Me._ControlV.SetErrorControl(ctl, focus)
                        Return errorFlg
                    End If

                    'テーブルタイプ(必須チェック)
                    .cmbTableTp.ItemName = .lblTableTp.Text
                    .cmbTableTp.IsHissuCheck = chkFlg
                    If MyBase.IsValidateCheck(.cmbTableTp) = errorFlg Then
                        Return errorFlg
                    End If

                    '空行チェック　※データタイプ＝"00"（距離刻み）の時はチェックなし
                    Dim sprMax As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1
                    For i As Integer = 2 To sprMax
                        Dim wtlv As String = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM060C.SprColumnIndex2.WT_LV))
                        Dim cartp As String = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM060C.SprColumnIndex2.CAR_TP))
                        Dim nb As String = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM060C.SprColumnIndex2.NB))
                        Dim qt As String = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM060C.SprColumnIndex2.QT))
                        Dim tsize As String = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM060C.SprColumnIndex2.T_SIZE))

                        '①計算種別＝"00"（重量・距離）/"03"（重量建て）/"07"（重量・県（重量建））の場合、既にある行の重量が空白or0の場合エラー
                        If .cmbDataTp.SelectedValue.Equals(LMM060C.DATA_TP_KBN_00) = False _
                           AndAlso (.cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_00) = True _
                           OrElse .cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_03) = True _
                           OrElse .cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_07) = True) Then
                            If String.IsNullOrEmpty(wtlv) = chkFlg OrElse (LMM060C.FLG.OFF).Equals(wtlv) = chkFlg Then
                                MyBase.ShowMessage("E219")
                                Return errorFlg
                            End If
                        End If

                        '②計算種別＝"01"（車種・距離）の場合、既にある行の車種が空白の場合エラー
                        If .cmbDataTp.SelectedValue.Equals(LMM060C.DATA_TP_KBN_00) = False _
                           AndAlso .cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_01) = True Then
                            If String.IsNullOrEmpty(cartp) = chkFlg Then
                                MyBase.ShowMessage("E219")
                                Return errorFlg
                            End If
                        End If

                        '③計算種別＝"02"（個数建て）/"05"（個数・県）の場合、既にある行の個数が空白or0の場合エラー
                        If .cmbDataTp.SelectedValue.Equals(LMM060C.DATA_TP_KBN_00) = False _
                           AndAlso (.cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_02) = True _
                           OrElse .cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_05) = True) Then
                            If String.IsNullOrEmpty(nb) = chkFlg OrElse (LMM060C.FLG.OFF).Equals(nb) = chkFlg Then
                                MyBase.ShowMessage("E219")
                                Return errorFlg
                            End If
                        End If

                        '④計算種別＝"04"（数量建て）の場合、既にある行の数量が空白or0の場合エラー
                        If .cmbDataTp.SelectedValue.Equals(LMM060C.DATA_TP_KBN_00) = False _
                           AndAlso .cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_04) = True Then
                            If String.IsNullOrEmpty(qt) = chkFlg OrElse (LMM060C.FLG.OFF).Equals(qt) = chkFlg Then
                                MyBase.ShowMessage("E219")
                                Return errorFlg
                            End If
                        End If

                        '⑤計算種別＝"06"（宅急便サイズ）の場合、既にある行の宅急便サイズが空白の場合エラー
                        If .cmbDataTp.SelectedValue.Equals(LMM060C.DATA_TP_KBN_00) = False _
                           AndAlso .cmbTableTp.SelectedValue.Equals(LMM060C.TABLE_TP_KBN_06) = True Then
                            If String.IsNullOrEmpty(tsize) = chkFlg Then
                                MyBase.ShowMessage("E219")
                                Return errorFlg
                            End If
                        End If

                    Next

                End With

                Return True

            Case LMM060C.EventShubetsu.DEL_T    '行削除

                With Me._Frm
                    '選択チェック
                    arr = Nothing
                    arr = Me.getCheckList(.sprDetail2)
                    If 0 = arr.Count Then
                        .sprDetail2.Focus()
                        MyBase.ShowMessage("E009")
                        Return errorFlg
                    End If

                End With

                Return True

        End Select


    End Function

    ''' <summary>
    ''' Excel取込時 禁止文字チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsExcelForbiddenWordsCheck(ByVal frm As LMM060F, ByVal dr As DataRow, ByVal ChkCell As String, Optional ByVal KyoriCnt As Integer = 0) As Boolean

        With Me._Frm

            Dim cnt As Integer = 0
            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False
            Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetail2)

            Select Case ChkCell

                '【データタイプ】
                Case .lblDataTp.Text
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("DATA_TP").ToString) = True Then
                        'チェックなし
                        Return True
                    Else
                        Dim dataTp As String = .cmbDataTp.SelectedValue.ToString
                        .lblChkDataTp.TextValue = dr("DATA_TP").ToString
                        .lblChkDataTp.ItemName = .lblDataTp.Text
                        .lblChkDataTp.IsForbiddenWordsCheck = chkFlg
                        If MyBase.IsValidateCheck(.lblChkDataTp) = errorFlg Then
                            'Excel取込時にエラーの場合は取込前に戻すので、画面全ロックする
                            Call Me._ControlG.SetLockControl(Me._Frm, True)
                            Return False
                        End If
                        Return True
                    End If

                    '【テーブルタイプ】
                Case .lblTableTp.Text
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("TABLE_TP").ToString) = True Then
                        'チェックなし
                        Return True
                    Else
                        Dim tableTp As String = .cmbTableTp.SelectedValue.ToString
                        .lblChkTableTp.TextValue = dr("TABLE_TP").ToString
                        .lblChkTableTp.ItemName = .lblTableTp.Text
                        .lblChkTableTp.IsForbiddenWordsCheck = chkFlg
                        If MyBase.IsValidateCheck(.lblChkTableTp) = errorFlg Then
                            'Excel取込時にエラーの場合は取込前に戻すので、画面全ロックする
                            Call Me._ControlG.SetLockControl(Me._Frm, True)
                            Return False
                        End If
                        Return True
                    End If

                    '【車種】
                Case LMM060G.sprDetailDef2.CAR_TP.ColName
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("CAR_TP").ToString) = True Then
                        'チェックなし
                        Return True
                    Else
                        vCell.ItemName = LMM060G.sprDetailDef2.CAR_TP.ColName
                        .lblChkCarTp.TextValue = dr("CAR_TP").ToString
                        .lblChkCarTp.ItemName = .lblChkCarTp.Text
                        .lblChkCarTp.IsForbiddenWordsCheck = chkFlg
                        If MyBase.IsValidateCheck(.lblChkCarTp) = errorFlg Then
                            'Excel取込時にエラーの場合は取込前に戻すので、画面全ロックする
                            Call Me._ControlG.SetLockControl(Me._Frm, True)
                            Return False
                        End If
                        Return True
                    End If

                    '【重量】
                Case LMM060G.sprDetailDef2.WT_LV.ColName
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("WT_LV").ToString) = True Then
                        'チェックなし
                        Return True
                    Else
                        vCell.ItemName = LMM060G.sprDetailDef2.WT_LV.ColName
                        .lblWtLv.TextValue = dr("WT_LV").ToString
                        .lblWtLv.ItemName = .lblWtLv.Text
                        .lblWtLv.IsForbiddenWordsCheck = chkFlg
                        If MyBase.IsValidateCheck(.lblWtLv) = errorFlg Then
                            'Excel取込時にエラーの場合は取込前に戻すので、画面全ロックする
                            Call Me._ControlG.SetLockControl(Me._Frm, True)
                            Return False
                        End If
                        Return True
                    End If

                    '【⑪距離1～70】
                Case LMM060C.KYORI
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("KYORI_" & KyoriCnt).ToString()) = True Then
                        'チェックなし
                        Return True
                    Else
                        If String.IsNullOrEmpty(dr("KYORI_" & KyoriCnt).ToString()) = False Then
                            .lblChkKyori.TextValue = dr("KYORI_" & KyoriCnt).ToString
                            .lblChkKyori.ItemName = .lblChkKyori.Text
                            .lblChkKyori.IsForbiddenWordsCheck = chkFlg
                            If MyBase.IsValidateCheck(.lblChkKyori) = errorFlg Then
                                'Excel取込時にエラーの場合は取込前に戻すので、画面全ロックする
                                Call Me._ControlG.SetLockControl(Me._Frm, True)
                                Return False
                            End If
                        End If
                        Return True
                    End If

                    '【都市割増Ａ】
                Case LMM060G.sprDetailDef2.CITY_EXTC_A.ColName
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("CITY_EXTC_A").ToString()) = True Then
                        'チェックなし
                        Return True
                    Else
                        vCell.ItemName = LMM060G.sprDetailDef2.CITY_EXTC_A.ColName
                        .lblChkCityExtcA.TextValue = dr("CITY_EXTC_A").ToString
                        .lblChkCityExtcA.ItemName = .lblChkCityExtcA.Text
                        .lblChkCityExtcA.IsForbiddenWordsCheck = chkFlg
                        If MyBase.IsValidateCheck(.lblChkCityExtcA) = errorFlg Then
                            'Excel取込時にエラーの場合は取込前に戻すので、画面全ロックする
                            Call Me._ControlG.SetLockControl(Me._Frm, True)
                            Return False
                        End If
                        Return True
                    End If

                    '【都市割増Ｂ】
                Case LMM060G.sprDetailDef2.CITY_EXTC_B.ColName
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("CITY_EXTC_B").ToString()) = True Then
                        'チェックなし
                        Return True
                    Else
                        vCell.ItemName = LMM060G.sprDetailDef2.CITY_EXTC_B.ColName
                        .lblChkCityExtcB.TextValue = dr("CITY_EXTC_B").ToString
                        .lblChkCityExtcB.ItemName = .lblChkCityExtcB.Text
                        .lblChkCityExtcB.IsForbiddenWordsCheck = chkFlg
                        If MyBase.IsValidateCheck(.lblChkCityExtcB) = errorFlg Then
                            'Excel取込時にエラーの場合は取込前に戻すので、画面全ロックする
                            Call Me._ControlG.SetLockControl(Me._Frm, True)
                            Return False
                        End If
                        Return True
                    End If

                    '【冬期割増Ａ】
                Case LMM060G.sprDetailDef2.WINT_EXTC_A.ColName
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("WINT_EXTC_A").ToString()) = True Then
                        'チェックなし
                        Return True
                    Else
                        vCell.ItemName = LMM060G.sprDetailDef2.WINT_EXTC_A.ColName
                        .lblChkWintExtcA.TextValue = dr("WINT_EXTC_A").ToString
                        .lblChkWintExtcA.ItemName = .lblChkWintExtcA.Text
                        .lblChkWintExtcA.IsForbiddenWordsCheck = chkFlg
                        If MyBase.IsValidateCheck(.lblChkWintExtcA) = errorFlg Then
                            'Excel取込時にエラーの場合は取込前に戻すので、画面全ロックする
                            Call Me._ControlG.SetLockControl(Me._Frm, True)
                            Return False
                        End If
                        Return True
                    End If

                    '【冬期割増Ｂ】
                Case LMM060G.sprDetailDef2.WINT_EXTC_B.ColName
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("WINT_EXTC_B").ToString()) = True Then
                        'チェックなし
                        Return True
                    Else
                        vCell.ItemName = LMM060G.sprDetailDef2.WINT_EXTC_B.ColName
                        .lblChkWintExtcB.TextValue = dr("WINT_EXTC_B").ToString
                        .lblChkWintExtcB.ItemName = .lblChkWintExtcB.Text
                        .lblChkWintExtcB.IsForbiddenWordsCheck = chkFlg
                        If MyBase.IsValidateCheck(.lblChkWintExtcB) = errorFlg Then
                            'Excel取込時にエラーの場合は取込前に戻すので、画面全ロックする
                            Call Me._ControlG.SetLockControl(Me._Frm, True)
                            Return False
                        End If
                        Return True
                    End If

                    '【中継料】
                Case LMM060G.sprDetailDef2.RELY_EXTC.ColName
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("RELY_EXTC").ToString()) = True Then
                        'チェックなし
                        Return True
                    Else
                        vCell.ItemName = LMM060G.sprDetailDef2.RELY_EXTC.ColName
                        .lblChkRelyExtc.TextValue = dr("RELY_EXTC").ToString
                        .lblChkRelyExtc.ItemName = .lblChkRelyExtc.Text
                        .lblChkRelyExtc.IsForbiddenWordsCheck = chkFlg
                        If MyBase.IsValidateCheck(.lblChkRelyExtc) = errorFlg Then
                            'Excel取込時にエラーの場合は取込前に戻すので、画面全ロックする
                            Call Me._ControlG.SetLockControl(Me._Frm, True)
                            Return False
                        End If
                        Return True
                    End If

                    '【保険料】
                Case LMM060G.sprDetailDef2.INSU.ColName
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("INSU").ToString()) = True Then
                        'チェックなし
                        Return True
                    Else
                        vCell.ItemName = LMM060G.sprDetailDef2.INSU.ColName
                        .lblChkInsu.TextValue = dr("INSU").ToString
                        .lblChkInsu.ItemName = .lblChkInsu.Text
                        .lblChkInsu.IsForbiddenWordsCheck = chkFlg
                        If MyBase.IsValidateCheck(.lblChkInsu) = errorFlg Then
                            'Excel取込時にエラーの場合は取込前に戻すので、画面全ロックする
                            Call Me._ControlG.SetLockControl(Me._Frm, True)
                            Return False
                        End If
                        Return True
                    End If

                    'START YANAI 要望番号377
                    '    '【10㎏あたりの航送料】
                    'Case Me._ControlV.SetRepMsgData(LMM060G.sprDetailDef2.FRRY_EXTC_10KG.ColName)
                    '    '値がない場合、スルー
                    '    If String.IsNullOrEmpty(dr("FRRY_EXTC_10KG").ToString()) = True Then
                    '        'チェックなし
                    '        Return True
                    '    Else
                    '        vCell.ItemName = Me._ControlV.SetRepMsgData(LMM060G.sprDetailDef2.FRRY_EXTC_10KG.ColName)
                    '        .lblChkFrryExtc10kg.TextValue = dr("FRRY_EXTC_10KG").ToString
                    '        .lblChkFrryExtc10kg.ItemName = .lblChkFrryExtc10kg.Text
                    '        .lblChkFrryExtc10kg.IsForbiddenWordsCheck = chkFlg
                    '        If MyBase.IsValidateCheck(.lblChkFrryExtc10kg) = errorFlg Then
                    '            'Excel取込時にエラーの場合は取込前に戻すので、画面全ロックする
                    '            Call Me._ControlG.SetLockControl(Me._Frm, True)
                    '            Return False
                    '        End If
                    '        Return True
                    '    End If

                    '    '【1件あたりの航送料】
                    'Case Me._ControlV.SetRepMsgData(LMM060G.sprDetailDef2.FRRY_EXTC_PART.ColName)
                    '    '値がない場合、スルー
                    '    If String.IsNullOrEmpty(dr("FRRY_EXTC_PART").ToString()) = True Then
                    '        'チェックなし
                    '        Return True
                    '    Else
                    '        vCell.ItemName = Me._ControlV.SetRepMsgData(LMM060G.sprDetailDef2.FRRY_EXTC_PART.ColName)
                    '        .lblChkFrryExtcPart.TextValue = dr("FRRY_EXTC_PART").ToString
                    '        .lblChkFrryExtcPart.ItemName = .lblChkFrryExtcPart.Text
                    '        .lblChkFrryExtcPart.IsForbiddenWordsCheck = chkFlg
                    '        If MyBase.IsValidateCheck(.lblChkFrryExtcPart) = errorFlg Then
                    '            'Excel取込時にエラーの場合は取込前に戻すので、画面全ロックする
                    '            Call Me._ControlG.SetLockControl(Me._Frm, True)
                    '            Return False
                    '        End If
                    '        Return True
                    '    End If
                    'END YANAI 要望番号377

            End Select

        End With

    End Function

    ''' <summary>
    ''' Excel取込時 存在チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsExcelExistCheck(ByVal frm As LMM060F, ByVal dr As DataRow, ByVal ChkCell As String) As Boolean

        With Me._Frm

            Dim cnt As Integer = 0
            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False
            Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetail2)

            Select Case ChkCell

                '【営業所】
                Case .lblNrsBrCd.Text
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("NRS_BR_CD").ToString) = True Then
                        'チェックなし
                        Return True
                    Else
                        Dim dr2 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD = '", dr("NRS_BR_CD").ToString, "' AND " _
                                                                                                                    , "SYS_DEL_FLG = '", LMM060C.FLG.OFF, "'"))

                        If dr2.Length < 1 Then
                            Return False
                        End If
                    End If
                    Return True

                    '【データタイプ】
                Case .lblDataTp.Text
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("DATA_TP").ToString) = True Then
                        'チェックなし
                        Return True
                    Else
                        Dim dr3 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_CD = '", dr("DATA_TP").ToString, "' AND " _
                                                                                            , "KBN_GROUP_CD = '", LMKbnConst.KBN_U010, "' AND " _
                                                                                            , "SYS_DEL_FLG = '", LMM060C.FLG.OFF, "'"))

                        If dr3.Length < 1 Then
                            Return False
                        End If
                    End If
                    Return True

                    '【テーブルタイプ】
                Case .lblTableTp.Text
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("TABLE_TP").ToString) = True Then
                        'チェックなし
                        Return True
                    Else
                        Dim dr4 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_CD = '", dr("TABLE_TP").ToString, "' AND " _
                                                                                            , "KBN_GROUP_CD = '", LMKbnConst.KBN_U011, "' AND " _
                                                                                            , "SYS_DEL_FLG = '", LMM060C.FLG.OFF, "'"))
                        If dr4.Length < 1 Then
                            Return False
                        End If
                    End If
                    Return True

                    '【車種】
                Case LMM060C.CAR_TP
                    '値がない場合、スルー
                    If String.IsNullOrEmpty(dr("CAR_TP").ToString) = True Then
                        'チェックなし
                        Return True
                    Else
                        Dim dr5 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_CD = '", dr("CAR_TP").ToString, "' AND " _
                                                                                            , "(KBN_GROUP_CD = '", LMKbnConst.KBN_S012, "' ", " OR KBN_GROUP_CD = '", LMKbnConst.KBN_T010, "') AND " _
                                                                                            , "SYS_DEL_FLG = '", LMM060C.FLG.OFF, "'"))
                        If dr5.Length < 1 Then
                            Return False
                        End If
                    End If
                    Return True

            End Select

        End With

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
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM060C.EventShubetsu.MASTEROPEN) = True Then
                Call Me._ControlV.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing
        Dim clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing

        With Me._Frm

            Select Case objNm

                Case .txtUnchinTariffCd2.Name

                    Dim custNm As String = .lblUnchinTariffCd2.Text
                    ctl = New Win.InputMan.LMImTextBox() {.txtUnchinTariffCd2}
                    msg = New String() {.lblUnchinTariffCd2.Text}

            End Select

            Dim focusCtl As Control = Me._Frm.ActiveControl
            Return Me._ControlV.IsFocusChk(actionType, ctl, msg, focusCtl, clearCtl)

        End With

    End Function

#Region "内部メソッド"

    ''' <summary>
    ''' 検索行のTrim処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrinmFindRow()

        With Me._Frm.sprDetail
            Call Me._ControlV.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0)
        End With

    End Sub

    ''' <summary>
    ''' 検索押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleChk() As Boolean

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False


        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '運賃タリフコード
            vCell.SetValidateCell(0, LMM060G.sprDetailDef.UNCHIN_TARIFF_CD.ColNo)
            vCell.ItemName = LMM060G.sprDetailDef.UNCHIN_TARIFF_CD.ColName
            vCell.IsByteCheck = 10
            vCell.IsForbiddenWordsCheck = chkFlg
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If


        End With

        Return True

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList(ByVal sprDetail As Spread.LMSpread) As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = 0
        If ("sprDetail2").Equals(sprDetail.Name) = True Then
            defNo = LMM060C.SprColumnIndex2.DEF
        End If

        '選択された行の行番号を取得
        Return _ControlV.SprSelectList(defNo, sprDetail)

    End Function

#End Region

#End Region 'Method

End Class
