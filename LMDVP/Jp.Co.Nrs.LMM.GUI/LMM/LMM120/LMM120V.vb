' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM120V : 単価マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMM120Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMM120V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM120F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMMControlV

    ''' <summary>
    ''' 特定荷主フラグ（TSMC）
    ''' </summary>
    Friend _flgTSMC As Boolean = False

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM120F, ByVal v As LMMControlV)

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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM120C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM120C.EventShubetsu.SHINKI           '新規
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

            Case LMM120C.EventShubetsu.HENSHU          '編集
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

            Case LMM120C.EventShubetsu.FUKUSHA       '複写
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


            Case LMM120C.EventShubetsu.SAKUJO_HUKKATU          '削除・復活
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

            Case LMM120C.EventShubetsu.KENSAKU         '検索
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

            Case LMM120C.EventShubetsu.MASTEROPEN          'マスタ参照
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

            Case LMM120C.EventShubetsu.HOZON           '保存
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

            Case LMM120C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM120C.EventShubetsu.DOUBLE_CLICK        'ダブルクリック
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

            Case LMM120C.EventShubetsu.ENTER          'Enter
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

        '2016.01.06 UMANO 英語化対応START
        'Dim msg As String = "編集"
        Dim msg As String = Me._Frm.FunctionKey.F2ButtonName
        '2016.01.06 UMANO 英語化対応END

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.lblRecNo.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbBr.SelectedValue.ToString(), msg) = False Then
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

        '2016.01.06 UMANO 英語化対応START
        'Dim msg As String = "複写"
        Dim msg As String = Me._Frm.FunctionKey.F3ButtonName
        '2016.01.06 UMANO 英語化対応END

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.lblRecNo.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbBr.SelectedValue.ToString(), msg) = False Then
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

        '2016.01.06 UMANO 英語化対応START
        'Dim msg As String = "削除・復活"
        Dim msg As String = Me._Frm.FunctionKey.F4ButtonName
        '2016.01.06 UMANO 英語化対応END

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.lblRecNo.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbBr.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

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

        'スペース除去
        Call Me._ControlV.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0)

        '単項目チェック
        If Me.IsKensakuSingleChk() = False Then
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
        If String.IsNullOrEmpty(objNm) = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM050C.EventShubetsu.MASTEROPEN) = True Then
                Call Me._ControlV.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm

            Select Case objNm
                Case .txtCustCdL.Name _
                    , .txtCustCdM.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCustNmL, .lblCustNmM}
                    '2016.01.06 UMANO 英語化対応START
                    'msg = New String() {"荷主コード(大)", "荷主コード(中)"}
                    msg = New String() {.lblTitleCust.Text(), .lblTitleCust.Text()}
                    '2016.01.06 UMANO 英語化対応END
            End Select

            Dim focusCtl As Control = Me._Frm.ActiveControl
            Return Me._ControlV.IsFocusChk(actionType, ctl, msg, focusCtl, clearCtl)

        End With

    End Function

    ''' <summary>
    ''' 保存押下時入力チェック
    ''' </summary>
    ''' <param name="dateStr">システム日付</param>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' <remarks></remarks>
    Friend Function IsSaveChk(ByVal dateStr As String) As Boolean

        With Me._Frm

            'スペース除去
            Call Me._ControlV.TrimSpaceTextvalue(Me._Frm)

            '単項目チェック
            If Me.IsSaveSingleChk() = False Then
                Return False
            End If

            'マスタ存在チェック
            If Me.IsSaveExistMst() = False Then
                Return False
            End If

            '関連チェック
            If Me.IsSaveRelationChk(dateStr) = False Then
                Return False
            End If

        End With

        Return True

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

#Region "内部メソッド"

    ''' <summary>
    ''' 検索押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleChk() As Boolean

        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '【荷主コード(大)】
            vCell.SetValidateCell(0, LMM120G.sprDtlDef.CUST_CD_L.ColNo)
            vCell.ItemName = LMM120G.sprDtlDef.CUST_CD_L.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【荷主名(大)】
            vCell.SetValidateCell(0, LMM120G.sprDtlDef.CUST_NM_L.ColNo)
            vCell.ItemName = LMM120G.sprDtlDef.CUST_NM_L.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【荷主コード(中)】
            vCell.SetValidateCell(0, LMM120G.sprDtlDef.CUST_CD_M.ColNo)
            vCell.ItemName = LMM120G.sprDtlDef.CUST_CD_M.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【荷主名(中)】
            vCell.SetValidateCell(0, LMM120G.sprDtlDef.CUST_NM_M.ColNo)
            vCell.ItemName = LMM120G.sprDtlDef.CUST_NM_M.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【単価マスタコード】
            vCell.SetValidateCell(0, LMM120G.sprDtlDef.TANKA_MST_CD.ColNo)
            vCell.ItemName = LMM120G.sprDtlDef.TANKA_MST_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 3
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleChk() As Boolean

        With Me._Frm

            '【営業所】
            .cmbBr.ItemName = .lblTitleBr.Text
            .cmbBr.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbBr) = False Then
                Return False
            End If

            '【荷主コード(大)】
            '2016.01.06 UMANO 英語化対応START
            '.txtCustCdL.ItemName = "荷主コード(大)"
            .txtCustCdL.ItemName = .lblTitleCust.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCustCdL.IsHissuCheck = True
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsFullByteCheck = 5
            .txtCustCdL.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If

            '【荷主コード(中)】
            '2016.01.06 UMANO 英語化対応START
            '.txtCustCdM.ItemName = "荷主コード(中)"
            .txtCustCdM.ItemName = .lblTitleCust.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCustCdM.IsHissuCheck = True
            .txtCustCdM.IsForbiddenWordsCheck = True
            .txtCustCdM.IsFullByteCheck = 2
            .txtCustCdM.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                Return False
            End If

            '【期割り区分】
            .cmbKiwariKbn.ItemName = .lblTitleKiwariKbn.Text
            .cmbKiwariKbn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbKiwariKbn) = False Then
                Return False
            End If

            '【摘要】
            .txtTekiyo.ItemName = .lblTitleTekiyo.Text
            .txtTekiyo.IsForbiddenWordsCheck = True
            .txtTekiyo.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtTekiyo) = False Then
                Return False
            End If

            '【単価マスタコード】
            .txtTankaMstCd.ItemName = .lblTitleTankaMstCd.Text
            .txtTankaMstCd.IsHissuCheck = True
            .txtTankaMstCd.IsForbiddenWordsCheck = True
            .txtTankaMstCd.IsByteCheck = 3
            If MyBase.IsValidateCheck(.txtTankaMstCd) = False Then
                Return False
            End If

            '【適用開始日】
            .imdTekiyoStart.ItemName = .lblTitleTekiyoStart.Text
            .imdTekiyoStart.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdTekiyoStart) = False Then
                Return False
            End If
            If .imdTekiyoStart.IsDateFullByteCheck = False Then
                MyBase.ShowMessage("E038", New String() {.lblTitleTekiyoStart.Text, "8"})
                Return False
            End If

            '【保管料建区分(温度管理なし)】
            '2016.01.06 UMANO 英語化対応START
            '.cmbHokanKbnNashi.ItemName = "保管料建区分(温度管理なし)"
            .cmbHokanKbnNashi.ItemName = String.Concat(.lblTitleHokanKbn.Text(), "(", .lblTitleOndoKanriNashi.Text(), ")")
            '2016.01.06 UMANO 英語化対応END
            .cmbHokanKbnNashi.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbHokanKbnNashi) = False Then
                Return False
            End If

            '【保管料建区分(温度管理あり)】
            '2016.01.06 UMANO 英語化対応START
            '.cmbHokanKbnAri.ItemName = "保管料建区分(温度管理あり)"
            .cmbHokanKbnAri.ItemName = String.Concat(.lblTitleHokanKbn.Text(), "(", .lblTitleOndoKanriAri.Text(), ")")
            '2016.01.06 UMANO 英語化対応END
            .cmbHokanKbnAri.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbHokanKbnAri) = False Then
                Return False
            End If

            '【荷役料建区分(入庫)】
            '2016.01.06 UMANO 英語化対応START
            '.cmbNiyakuryoKbnNyuko.ItemName = "荷役料建区分(入庫)"
            .cmbNiyakuryoKbnNyuko.ItemName = String.Concat(.lblTitleNiyakuryoKbn.Text(), "(", .lblTitleNyuko.Text(), ")")
            '2016.01.06 UMANO 英語化対応END
            .cmbNiyakuryoKbnNyuko.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNiyakuryoKbnNyuko) = False Then
                Return False
            End If

            '【荷役料建区分(出庫)】
            '2016.01.06 UMANO 英語化対応START
            '.cmbNiyakuryoKbnShukko.ItemName = "荷役料建区分(出庫)"
            .cmbNiyakuryoKbnShukko.ItemName = String.Concat(.lblTitleNiyakuryoKbn.Text(), "(", .lblTitleShukko.Text(), ")")
            '2016.01.06 UMANO 英語化対応END
            .cmbNiyakuryoKbnShukko.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNiyakuryoKbnShukko) = False Then
                Return False
            End If

#If True Then   'ADD 2019/04/18 依頼番号 : 004862   【LMS】単価マスタ_大阪しばらく不使用の単価マスタを分かりやすくする
            '【使用可能】
            .cmbAVAL_YN.ItemName = .blTitleAVAL_YN.Text
            .cmbAVAL_YN.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbAVAL_YN) = False Then
                Return False
            End If
#End If

            '【製品セグメント】
            If .cmbProductSegCd.HissuLabelVisible Then
                '外部倉庫用ABP対策として必須マークがある場合のみ
                .cmbProductSegCd.ItemName = .lblTitleProductSeg.Text
                .cmbProductSegCd.IsHissuCheck = True
                If MyBase.IsValidateCheck(.cmbProductSegCd) = False Then
                    Return False
                End If
            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存押下時関連チェック
    ''' </summary>
    ''' <param name="dateStr">システム日付</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveRelationChk(ByVal dateStr As String) As Boolean

        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        Dim msg1 As String = ""
        Dim msg2 As String = ""

        With Me._Frm

            '混在チェック
            'If .cmbHokanKbnNashi.SelectedValue.Equals(.cmbHokanKbnAri.SelectedValue) = False _
            'OrElse .cmbNiyakuryoKbnNyuko.SelectedValue.Equals(.cmbNiyakuryoKbnShukko.SelectedValue) = False _
            'OrElse .cmbHokanKbnNashi.SelectedValue.Equals(.cmbNiyakuryoKbnNyuko.SelectedValue) = False Then
            '    '2016.01.06 UMANO 英語化対応START
            '    'MyBase.ShowMessage("E216", New String() {"保管料建区分", "荷役料建区分"})
            '    MyBase.ShowMessage("E216", New String() {.lblTitleHokanKbn.Text(), .lblTitleNiyakuryoKbn.Text()})
            '    '2016.01.06 UMANO 英語化対応END
            '    Me._ControlV.SetErrorControl(New Control() {.cmbHokanKbnNashi, .cmbHokanKbnAri, .cmbNiyakuryoKbnNyuko, .cmbNiyakuryoKbnShukko}, .cmbHokanKbnNashi)
            '    Return False
            'End If

            '期割区分
            msg1 = lgm.Selector({"この処理", "this process", "이 처리의", "this process"})
            If (Me._flgTSMC) AndAlso (Not .cmbKiwariKbn.SelectedValue.Equals("05")) Then
                '特定荷主（TSMC）でセット料金でなければエラー
                msg2 = lgm.Selector({String.Concat(.lblTitleKiwariKbn.Text(), "にセット料金以外"),
                                    String.Concat("other than set price to ", .lblTitleKiwariKbn.Text()),
                                    String.Concat(.lblTitleKiwariKbn.Text(), "에 세트 가격 이외"),
                                    String.Concat("other than set price to ", .lblTitleKiwariKbn.Text())})
                MyBase.ShowMessage("E123", New String() {msg1, msg2})
                Me._ControlV.SetErrorControl(New Control() { .cmbKiwariKbn}, .cmbKiwariKbn)
                Return False
            ElseIf (Not Me._flgTSMC) AndAlso (.cmbKiwariKbn.SelectedValue.Equals("05")) Then
                '特定荷主（TSMC）以外でセット料金はエラー
                msg2 = lgm.Selector({String.Concat(.lblTitleKiwariKbn.Text(), "にセット料金"),
                                    String.Concat("set price to ", .lblTitleKiwariKbn.Text()),
                                    String.Concat(.lblTitleKiwariKbn.Text(), "에 세트 가격"),
                                    String.Concat("set price to ", .lblTitleKiwariKbn.Text())})
                MyBase.ShowMessage("E123", New String() {msg1, msg2})
                Me._ControlV.SetErrorControl(New Control() { .cmbKiwariKbn}, .cmbKiwariKbn)
                Return False
            End If

            ' 保管料建区分 温度管理ありとなしは同一
            If .cmbHokanKbnNashi.SelectedValue.Equals(.cmbHokanKbnAri.SelectedValue) = False Then
                MyBase.ShowMessage("E216", New String() {String.Concat(.lblTitleHokanKbn.Text(), " ", .lblTitleOndoKanriNashi.Text()), .lblTitleOndoKanriAri.Text()})
                Me._ControlV.SetErrorControl(New Control() { .cmbHokanKbnNashi, .cmbHokanKbnAri}, .cmbHokanKbnNashi)
                Return False
            End If

            '保管料建区分：パレット建て以外は混在不可
            If (LMM120C.TANKA_KBN_PALETTO_DATE.Equals(.cmbHokanKbnAri.SelectedValue) = False _
                Or LMM120C.TANKA_KBN_PALETTO_DATE.Equals(.cmbHokanKbnNashi.SelectedValue) = False) _
               AndAlso (.cmbHokanKbnNashi.SelectedValue.Equals(.cmbHokanKbnAri.SelectedValue) = False _
                OrElse .cmbNiyakuryoKbnNyuko.SelectedValue.Equals(.cmbNiyakuryoKbnShukko.SelectedValue) = False _
                OrElse .cmbHokanKbnNashi.SelectedValue.Equals(.cmbNiyakuryoKbnNyuko.SelectedValue) = False) Then

                msg1 = lgm.Selector({String.Concat(.lblTitleHokanKbn.Text(), "がパレット建て以外の場合、"),
                                    String.Concat("If the ", .lblTitleHokanKbn.Text(), " is other than Pallet, "),
                                    String.Concat(.lblTitleHokanKbn.Text(), "이 팔렛 아닌 경우, "),
                                    String.Concat("If the ", .lblTitleHokanKbn.Text(), " is other than Pallet, ")})
                MyBase.ShowMessage("E216", New String() {String.Concat(msg1, .lblTitleHokanKbn.Text()), .lblTitleNiyakuryoKbn.Text()})
                Me._ControlV.SetErrorControl(New Control() { .cmbHokanKbnNashi, .cmbHokanKbnAri, .cmbNiyakuryoKbnNyuko, .cmbNiyakuryoKbnShukko}, .cmbHokanKbnNashi)
                Return False

            End If

            '荷役料チェック
            Dim niyakuryoNyukoKbn As String = _Frm.cmbNiyakuryoKbnNyuko.SelectedValue.ToString
            Dim niyakuryoShukkoKbn As String = _Frm.cmbNiyakuryoKbnShukko.SelectedValue.ToString
            If niyakuryoNyukoKbn <> niyakuryoShukkoKbn Then
                MyBase.ShowMessage("E02F")
                Me._ControlV.SetErrorControl(New Control() { .cmbNiyakuryoKbnNyuko, .cmbNiyakuryoKbnShukko}, .cmbHokanKbnNashi)
                Return False
            End If

            '日付確認ワーニングチェック
            Dim chkDate As String = Date.ParseExact(dateStr, "yyyyMMdd", Nothing).AddMonths(6).ToString.Replace("/", "").Substring(0, 8)
            If chkDate < .imdTekiyoStart.TextValue Then
                '2016.01.06 UMANO 英語化対応START
                'If MyBase.ShowMessage("W140", New String() {"適用開始日", "当日より6ヶ月以上先の日付"}) = MsgBoxResult.Ok Then
                If MyBase.ShowMessage("W258", New String() { .lblTitleTekiyoStart.Text()}) = MsgBoxResult.Ok Then
                    '2016.01.06 UMANO 英語化対応END
                    Return True
                Else
                    Me._ControlV.SetErrorControl(.imdTekiyoStart)
                    Me.SetBaseMsg() '基本メッセージの設定
                    Return False
                End If
            End If

        End With

        Return True

    End Function

#Region "マスタ存在チェック"

    ''' <summary>
    ''' 保存押下時マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveExistMst() As Boolean

        With Me._Frm

            '荷主マスタ存在チェック　　　　【荷主】
            Dim rtnResult As Boolean = Me.IsCustExistChk()

            Return rtnResult

        End With

        Return True

    End Function

    ''' <summary>
    '''  荷主マスタ存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsCustExistChk() As Boolean

        With Me._Frm

            Dim brCd As String = .cmbBr.SelectedValue.ToString()
            Dim cdL As String = .txtCustCdL.TextValue
            Dim cdM As String = .txtCustCdM.TextValue

            '名称項目を空にする
            .lblCustNmL.TextValue = String.Empty
            .lblCustNmM.TextValue = String.Empty

            '値がない場合、スルー
            If String.IsNullOrEmpty(cdL) = True _
            OrElse String.IsNullOrEmpty(cdM) = True Then
                Return True
            End If

            '名称取得用テーブル
            Dim ExistChkDr As DataRow() = Nothing

            'マスタ存在チェックを行う
            If Me._ControlV.SelectCustListDataRow(ExistChkDr, cdL, cdM, , , LMMControlC.CustMsgType.CUST_M) = False Then
                Call Me._ControlV.SetErrorControl(New Control() {.txtCustCdL, .txtCustCdM}, .txtCustCdL)
                Return False
            Else
                If ExistChkDr IsNot Nothing Then
                    .txtCustCdL.TextValue = ExistChkDr(0).Item("CUST_CD_L").ToString()
                    .txtCustCdM.TextValue = ExistChkDr(0).Item("CUST_CD_M").ToString()
                    .lblCustNmL.TextValue = ExistChkDr(0).Item("CUST_NM_L").ToString()
                    .lblCustNmM.TextValue = ExistChkDr(0).Item("CUST_NM_M").ToString()
                End If
            End If

            Return True

        End With

    End Function

#End Region

#End Region

#End Region

End Class
