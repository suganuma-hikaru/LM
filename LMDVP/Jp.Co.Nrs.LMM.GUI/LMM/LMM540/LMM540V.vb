' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM540V : 棟マスタメンテナンス
'  作  成  者       :  [narita]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMM540Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM540V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM540F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM540F, ByVal v As LMMControlV)

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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM540C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM540C.EventShubetsu.SHINKI           '新規
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM540C.EventShubetsu.HENSHU          '編集
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM540C.EventShubetsu.HUKUSHA          '複写
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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


            Case LMM540C.EventShubetsu.SAKUJO          '削除・復活
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM540C.EventShubetsu.KENSAKU         '検索
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

            Case LMM540C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM540C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM540C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM540C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM540C.EventShubetsu.ENTER          'Enter
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

                '2017/10/27 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
            Case LMM540C.EventShubetsu.INS_EXP_T, LMM540C.EventShubetsu.DEL_EXP_T, LMM540C.EventShubetsu.IKKATU_TOUROKU
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select
                '2017/10/27 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

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
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtTouNo.TextValue) = False Then
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
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtTouNo.TextValue) = False Then
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

        '2016.01.06 UMANO 英語化対応START
        'Dim msg As String = "削除・復活"
        Dim msg As String = Me._Frm.FunctionKey.F4ButtonName
        '2016.01.06 UMANO 英語化対応END

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtTouNo.TextValue) = False Then
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
    Friend Function IsSaveInputChk() As Boolean

        With Me._Frm

            'スペース除去
            Call Me._ControlV.TrimSpaceTextvalue(Me._Frm)
            Call Me._ControlV.TrimSpaceSprTextvalue(.sprDetail2 _
                                                    , .sprDetail2.ActiveSheet.Rows.Count - 1 _
                                                    , LMM540G.sprDetailDef2.WH_KYOKA_DATE.ColNo)

            '単項目チェック(編集部)
            Dim rtnResult As Boolean = Me.IsSaveSingleCheck()

            '単項目チェック(棟消防Spread)
            rtnResult = rtnResult AndAlso Me.IsTouChk()


            '単項目チェック(棟チェックマスタSpread)
            rtnResult = rtnResult AndAlso Me.IsExistTouChkSpr(.sprDetail4, LMM540G.sprDetailDef4.DOKU_KB.ColNo)
            rtnResult = rtnResult AndAlso Me.IsExistTouChkSpr(.sprDetail5, LMM540G.sprDetailDef5.KOUATHUGAS_KB.ColNo)
            rtnResult = rtnResult AndAlso Me.IsExistTouChkSpr(.sprDetail6, LMM540G.sprDetailDef6.YAKUZIHO_KB.ColNo)

            '非表示項目（毒劇区分、ガス管理区分）設定
            Call Me.SetCmbValue()

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
            .cmbNrsBrCd.ItemName = .lblEigyosyo.Text
            .cmbNrsBrCd.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = errorFlg Then
                Return errorFlg
            End If

            '倉庫
            .cmbWare.ItemName = .lblWare.Text
            .cmbWare.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbWare) = errorFlg Then
                Return errorFlg
            End If

            '棟番号
            '2016.01.06 UMANO 英語化対応START
            '.txtTouNo.ItemName = LMM540C.TOU
            .txtTouNo.ItemName = .sprDetail.ActiveSheet.GetColumnLabel(0, LMM540C.SprColumnIndex.TOU_NO)
            '2016.01.06 UMANO 英語化対応END
            .txtTouNo.IsHissuCheck = chkFlg
            .txtTouNo.IsForbiddenWordsCheck = chkFlg
            .txtTouNo.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtTouNo) = errorFlg Then
                Return errorFlg
            End If


            '棟名
            '2016.01.06 UMANO 英語化対応START
            '.txtTouNm.ItemName = .lblTou.Text & "名"
            .txtTouNm.ItemName = .sprDetail.ActiveSheet.GetColumnLabel(0, LMM540C.SprColumnIndex.TOU_NM)
            '2016.01.06 UMANO 英語化対応END
            .txtTouNm.IsHissuCheck = chkFlg
            .txtTouNm.IsForbiddenWordsCheck = chkFlg
            .txtTouNm.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtTouNm) = errorFlg Then
                Return errorFlg
            End If

            '倉庫区分
            .cmbSokoKbn.ItemName = .lblSokoKbn.Text
            .cmbSokoKbn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbSokoKbn) = errorFlg Then
                Return errorFlg
            End If

            '保税区分
            .cmbHozeiKbn.ItemName = .lblHozeiKbn.Text
            .cmbHozeiKbn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbHozeiKbn) = errorFlg Then
                Return errorFlg
            End If

            '保安監督者名
            .txtFctMgr.ItemName = .lblFctMgr.Text
            .txtFctMgr.IsHissuCheck = True
            .txtFctMgr.IsForbiddenWordsCheck = True
            .txtFctMgr.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtFctMgr) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.txtFctMgr)
                Return errorFlg
            End If
            Dim userDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", .txtFctMgr.TextValue, "'"))
            If 0 = userDr.Length Then
                MyBase.ShowMessage("E871")
                Call Me._ControlV.SetErrorControl(.txtFctMgr)
                Return errorFlg
            End If


        End With

        Return True

    End Function

    ''' <summary>
    ''' 棟消防Spreadの単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsTouChk() As Boolean

        '**********棟消防スプレッドのチェック
        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetail2)

        With vCell

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False
            Dim spr As LMSpread = Me._Frm.sprDetail2
            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To max

                '許可日
                .SetValidateCell(i, LMM540G.sprDetailDef2.WH_KYOKA_DATE.ColNo)
                .ItemName = LMM540G.sprDetailDef2.WH_KYOKA_DATE.ColName
                .IsFullByteCheck = 10
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

            Next

        End With

        Return True

    End Function



    ''' <summary>
    ''' 棟チェックマスタSpreadの単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExistTouChkSpr(ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread, ByVal kbnColmNo As Integer) As Boolean
        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
        Dim id As String = String.Empty
        Dim chckId As String = String.Empty

        Dim sprName As String = String.Empty

        Select Case spr.Name
            Case "sprDetail4"
                sprName = "毒劇区分"
            Case "sprDetail5"
                sprName = "高圧ガス区分"
            Case "sprDetail6"
                sprName = "薬機法区分"
        End Select

        Dim vCell As LMValidatableCells = New LMValidatableCells(spr)

        '必須チェック
        For i As Integer = 0 To max

            vCell.SetValidateCell(i, kbnColmNo)
            vCell.ItemName = sprName
            vCell.IsHissuCheck = True

            If MyBase.IsValidateCheck(vCell) = False Then
                Call Me._ControlV.SetErrorControl(spr _
                                                      , i _
                                                      , kbnColmNo)
                Return False
            End If
        Next

        '重複チェック(同一の区分が使用されている場合エラー)
        For i As Integer = 0 To max

            With spr.ActiveSheet
                id = Me._ControlV.GetCellValue(.Cells(i, kbnColmNo))
                If i <> max Then
                    For j As Integer = i + 1 To max
                        chckId = Me._ControlV.GetCellValue(.Cells(j, kbnColmNo))
                        If id.Equals(chckId) = True Then

                            MyBase.ShowMessage("E496", {sprName, "当該行：" & (i + 1).ToString() & "行目，" & (j + 1).ToString() & "行目"})
                            Call Me._ControlV.SetErrorControl(spr, {i, j}, {kbnColmNo, kbnColmNo})
                            Return False
                        End If
                    Next
                End If
            End With
        Next

        Return True

    End Function

    ''' <summary>
    ''' 非表示項目（毒劇区分、ガス管理区分）設定
    ''' </summary>
    Private Sub SetCmbValue()

        Dim spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Dim max As Integer
        Dim id As String = String.Empty
        Dim tempId As String = String.Empty

        With Me._Frm
            '毒劇区分
            spr = .sprDetail4
            max = spr.ActiveSheet.Rows.Count - 1


            For i As Integer = 0 To max

                id = Me._ControlV.GetCellValue(spr.ActiveSheet.Cells(i, LMM540G.sprDetailDef4.DOKU_KB.ColNo))

                '毒劇区分を 特定毒物 > 毒物 > 劇物 > なしで設定
                If String.IsNullOrEmpty(id) = False Then

                    If LMM540C.M_Z_KBN_DOKUGEKI_TOKU.Equals(id) Then

                        tempId = LMM540C.M_Z_KBN_DOKUGEKI_TOKU
                        Exit For '最上位なので、以降確認不要

                    ElseIf LMM540C.M_Z_KBN_DOKUGEKI_DOKU.Equals(id) _
                        And tempId.Equals(LMM540C.M_Z_KBN_DOKUGEKI_TOKU) = False Then

                        tempId = LMM540C.M_Z_KBN_DOKUGEKI_DOKU

                    ElseIf LMM540C.M_Z_KBN_DOKUGEKI_GEKI.Equals(id) _
                        And tempId.Equals(LMM540C.M_Z_KBN_DOKUGEKI_TOKU) = False _
                        And tempId.Equals(LMM540C.M_Z_KBN_DOKUGEKI_DOKU) = False Then

                        tempId = LMM540C.M_Z_KBN_DOKUGEKI_GEKI

                    ElseIf LMM540C.M_Z_KBN_DOKUGEKI_NASI.Equals(id) _
                        And String.IsNullOrEmpty(tempId) Then

                        tempId = LMM540C.M_Z_KBN_DOKUGEKI_NASI

                    End If

                End If
            Next


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
    Friend Function IsRowCheck(ByVal eventShubetsu As LMM540C.EventShubetsu, ByVal ds As DataSet, ByVal frm As LMM540F) As Boolean
        Dim arr As ArrayList = Nothing

        Select Case eventShubetsu
            Case LMM540C.EventShubetsu.INS_T    '行追加

                Dim outSDt As DataTable = ds.Tables(LMZ280C.TABLE_NM_OUT)
                Dim outSRow As DataRow = Nothing

                For j As Integer = 0 To outSDt.Rows.Count - 1

                    outSRow = outSDt.Rows(j)
                    Dim ShoboCd As String = String.Empty
                    ShoboCd = outSRow.Item("SHOBO_CD").ToString

                    With Me._Frm

                        Dim sprMax As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1
                        For i As Integer = 0 To sprMax

                            If (ShoboCd).Equals(_ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM540C.SprColumnIndex2.SHOBO_CD))) Then
                                '2016.01.06 UMANO 英語化対応START
                                'MyBase.ShowMessage("E177", New String() {"消防情報"})
                                MyBase.ShowMessage("E177", New String() {frm.grpShoboJoho.Text()})
                                '2016.01.06 UMANO 英語化対応END
                                Return False
                            End If
                        Next

                    End With
                Next

                Return True

            Case LMM540C.EventShubetsu.DEL_T    '行削除

                With Me._Frm
                    '選択ﾁｪｯｸ
                    arr = Nothing
                    arr = Me.getCheckList(.sprDetail2)
                    If 0 = arr.Count Then
                        .sprDetail2.Focus()
                        MyBase.ShowMessage("E009")
                        Return False
                    End If

                End With

                Return True

        End Select

    End Function


    ''' <summary>
    ''' 行追加/行削除 入力チェック 棟チェックマスタスプレッド(3種)
    ''' </summary>
    ''' <param name="eventShubetsu"></param>
    ''' <param name="frm"></param>
    ''' <param name="spr"></param>
    ''' <param name="defColNo"></param>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsTouChkRowCheck(ByVal eventShubetsu As LMM540C.EventShubetsu, ByVal frm As LMM540F, ByVal spr As LMSpread, ByVal defColNo As Integer) As Boolean

        Dim arr As ArrayList = Nothing

        Select Case eventShubetsu
            Case LMM540C.EventShubetsu.INS_DOKU,
                 LMM540C.EventShubetsu.INS_KOUATHUGAS,
                 LMM540C.EventShubetsu.INS_YAKUZIHO     '行追加

                '空行チェック
                If Me.IsKuranChk(spr, defColNo) = False Then
                    Return False
                End If

                Return True

            Case LMM540C.EventShubetsu.DEL_DOKU,
                 LMM540C.EventShubetsu.DEL_KOUATHUGAS,
                 LMM540C.EventShubetsu.DEL_YAKUZIHO     '行削除

                '選択ﾁｪｯｸ
                arr = _ControlV.SprSelectList(defColNo, spr)
                If 0 = arr.Count Then
                    spr.Focus()
                    MyBase.ShowMessage("E009")
                    Return False
                End If

                Return True

        End Select

    End Function

    ''' <summary>
    ''' 一括登録　押下時チェック
    ''' </summary>
    ''' <param name="isExistMCustCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IkkatuTourokuCheck(ByVal isExistMCustCd As Boolean) As Boolean

        Dim lgm As New Utility.lmLangMGR(Nrs.Win.Base.MessageManager.MessageLanguage)


        Return True

    End Function

    '2017/10/26 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

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

            '棟番号
            vCell.SetValidateCell(0, LMM540G.sprDetailDef.TOU_NO.ColNo)
            vCell.ItemName = LMM540G.sprDetailDef.TOU_NO.ColName
            vCell.IsByteCheck = 2
            vCell.IsForbiddenWordsCheck = chkFlg
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If


            '棟番名
            vCell.SetValidateCell(0, LMM540G.sprDetailDef.TOU_NM.ColNo)
            vCell.ItemName = LMM540G.sprDetailDef.TOU_NM.ColName
            vCell.IsByteCheck = 60
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
            defNo = LMM540C.SprColumnIndex2.DEF
        End If

        '選択された行の行番号を取得
        Return _ControlV.SprSelectList(defNo, sprDetail)

    End Function

    '2017/10/24 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckExpList(ByVal sprDetail As Spread.LMSpread) As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = 0
        If ("sprDetail3").Equals(sprDetail.Name) = True Then
            defNo = LMM540C.SprColumnIndex3.DEF
        End If

        '選択された行の行番号を取得
        Return _ControlV.SprSelectList(defNo, sprDetail)

    End Function
    '2017/10/24 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 

    ''' <summary>
    '''行追加時、Spreadに何も入力されていない場合、エラー
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKuranChk(ByVal spr As LMSpread, ByVal defColNo As Integer) As Boolean

        With spr.ActiveSheet

            Dim rowMax As Integer = .Rows.Count - 1
            Dim chkFlg As Boolean = False

            For i As Integer = 0 To rowMax
                If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, defColNo + 1))) = False Then
                    chkFlg = True
                End If

                If chkFlg = False Then
                    MyBase.ShowMessage("E219")
                    Return False
                End If

                '初期値設定
                chkFlg = False
            Next
        End With

        Return True

    End Function
#End Region

#End Region 'Method

End Class
