' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM240V : 帳票パターンマスタメンテ
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
''' LMM240Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM240V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM240F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM240F, ByVal v As LMMControlV)

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

        ''関連チェック
        rtnResult = rtnResult AndAlso Me.IsInputConnectionChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 保存時のtrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtPtnCd.TextValue = .txtPtnCd.TextValue.Trim()
            .txtPtnNm.TextValue = .txtPtnNm.TextValue.Trim()
            .txtPtnCd2.TextValue = .txtPtnCd2.TextValue.Trim()
            .txtRemark.TextValue = .txtRemark.TextValue.Trim()

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
            '.cmbNrsBrCd.Name = "営業所"
            .cmbNrsBrCd.Name = .LmTitleLabel17.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            '帳票種類ID
            '2016.01.06 UMANO 英語化対応START
            '.cmbPtnId.ItemName = "帳票種類ID"
            .cmbPtnId.ItemName = .LmTitleLabel2.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbPtnId.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbPtnId) = False Then
                Return False
            End If

            '帳票パターンコード
            '2016.01.06 UMANO 英語化対応START
            '.txtPtnCd.ItemName = "帳票パターンコード"
            .txtPtnCd.ItemName = .LmTitleLabel4.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtPtnCd.IsHissuCheck = True
            .txtPtnCd.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtPtnCd) = False Then
                Return False
            End If

            'パターン名
            '2016.01.06 UMANO 英語化対応START
            '.txtPtnNm.ItemName = "パターン名"
            .txtPtnNm.ItemName = .LmTitleLabel3.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtPtnNm.IsHissuCheck = True
            .txtPtnNm.IsForbiddenWordsCheck = True
            .txtPtnNm.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtPtnNm) = False Then
                Return False
            End If

            '帳票パターンコード2
            '2016.01.06 UMANO 英語化対応START
            '.txtPtnCd2.ItemName = "帳票パターンコード2"
            .txtPtnCd2.ItemName = .LmTitleLabel5.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtPtnCd2.IsForbiddenWordsCheck = True
            .txtPtnCd2.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtPtnCd2) = False Then
                Return False
            End If

            '帳票出力先区分
            Dim joukenHissu As Boolean = Not String.IsNullOrEmpty(.cmbRptNm.SelectedValue.ToString())
            '2016.01.06 UMANO 英語化対応START
            '.cmbRptOut.ItemName = "帳票出力先区分"
            .cmbRptOut.ItemName = .LmTitleLabel12.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbRptOut.IsHissuCheck = joukenHissu
            If MyBase.IsValidateCheck(.cmbRptOut) = False Then
                Return False
            End If

            '出力形式区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbOutPut.ItemName = "出力形式区分"
            .cmbOutPut.ItemName = .LmTitleLabel11.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbOutPut.IsHissuCheck = joukenHissu
            If MyBase.IsValidateCheck(.cmbOutPut) = False Then
                Return False
            End If

            'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
            Dim rptOutFlg As Boolean = False
            If ("01").Equals(.cmbRptOut.SelectedValue) = True OrElse _
                ("02").Equals(.cmbRptOut.SelectedValue) = True OrElse _
                ("04").Equals(.cmbRptOut.SelectedValue) = True OrElse _
                ("05").Equals(.cmbRptOut.SelectedValue) = True OrElse _
                ("06").Equals(.cmbRptOut.SelectedValue) = True OrElse _
                ("07").Equals(.cmbRptOut.SelectedValue) = True OrElse _
                ("08").Equals(.cmbRptOut.SelectedValue) = True Then
                rptOutFlg = True
            End If

            If rptOutFlg = True Then
                'ジョブID
                '2016.01.06 UMANO 英語化対応START
                '.cmbJobId.ItemName = "ジョブＩＤ"
                .cmbJobId.ItemName = .LmTitleLabel16.Text()
                '2016.01.06 UMANO 英語化対応END
                .cmbJobId.IsHissuCheck = joukenHissu
                If MyBase.IsValidateCheck(.cmbJobId) = False Then
                    Return False
                End If
            End If
            'END YANAI 要望番号675 プリンタの設定を個人別を可能にする

            '履歴残し有無
            '2016.01.06 UMANO 英語化対応START
            '.cmbHstryFlg.ItemName = "履歴残し有無"
            .cmbHstryFlg.ItemName = .LmTitleLabel19.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbHstryFlg.IsHissuCheck = joukenHissu
            If MyBase.IsValidateCheck(.cmbHstryFlg) = False Then
                Return False
            End If

            '備考
            '2016.01.06 UMANO 英語化対応START
            '.txtRemark.ItemName = "備考"
            .txtRemark.ItemName = .LmTitleLabel13.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtRemark.IsForbiddenWordsCheck = True
            .txtRemark.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtRemark) = False Then
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
    Private Function IsInputConnectionChk() As Boolean

        '帳票出力先区分と他コンボの関連チェック
        Dim rtnResult As Boolean = Me.IsCmbRelationChk()

        '同一チェック
        rtnResult = rtnResult AndAlso Me.IsSameChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 帳票出力先区分と他コンボ関連チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsCmbRelationChk() As Boolean

        With Me._Frm

            '帳票出力先区分
            Dim rptOut As String = .cmbRptOut.SelectedValue.ToString()

            Select Case rptOut

                '区分 = '01'（一般帳票１（日常用））
                Case LMM240C.NICHIJO

                    Return Me.IscmbJobIdChk()

                    '区分 = '02'（一般帳票２（大量））の場合
                Case LMM240C.TAIRYO

                    Return Me.IscmbJobIdChk()
                    '区分 = '03'（プリンタ指定）の場合
                Case LMM240C.NIHUDA

                    Return Me.IscmbPrinterNmChk()

                Case Else

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' ジョブIDコンボボックス必須チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IscmbJobIdChk() As Boolean

        With Me._Frm

            'ジョブＩＤ
            '2016.01.06 UMANO 英語化対応START
            '.cmbJobId.ItemName = "ジョブＩＤ"
            .cmbJobId.ItemName = .LmTitleLabel16.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbJobId.IsHissuCheck = Not String.IsNullOrEmpty(.cmbRptNm.SelectedValue.ToString())
            If MyBase.IsValidateCheck(.cmbJobId) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' プリンタ名コンボボックス必須チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IscmbPrinterNmChk() As Boolean

        With Me._Frm

            'プリンタ名
            '2016.01.06 UMANO 英語化対応START
            '.cmbPrinterNm.ItemName = "プリンタ名"
            .cmbPrinterNm.ItemName = .LmTitleLabel15.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbPrinterNm.IsHissuCheck = Not String.IsNullOrEmpty(.cmbRptNm.SelectedValue.ToString())
            If MyBase.IsValidateCheck(.cmbPrinterNm) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 同一チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>帳票パターンコード2が画面の営業所コード、
    ''' 帳票種類IDに紐づく帳票パターンコード以外の場合エラー</remarks>
    Private Function IsSameChk() As Boolean

        With Me._Frm

            Dim nrsBrCd As String = .cmbNrsBrCd.SelectedValue.ToString()
            Dim ptnId As String = .cmbPtnId.SelectedValue.ToString()
            Dim ptnCd As String = .txtPtnCd2.TextValue

            'パターンコード2が空のときtrue
            If String.IsNullOrEmpty(ptnCd) = True Then
                Return True
            End If
            

            Dim drs As DataRow() = Me._Vcon.SelectRptListDataRow(nrsBrCd, ptnId, ptnCd)

            If drs.Length < 1 Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E224", New String() {String.Concat(LMM240C.CDMSG, "2") _
                '                                         , String.Concat("同様の帳票種類IDに該当する", LMM240C.CDMSG)})
                MyBase.ShowMessage("E892", New String() {.LmTitleLabel5.Text(), .LmTitleLabel4.Text()})
                '2016.01.06 UMANO 英語化対応END
                Call Me._Vcon.SetErrorControl(.txtPtnCd2)
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM240F) As Boolean

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
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM240F, ByVal eventShubetsu As LMM240C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd()) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM240C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM240C.EventShubetsu.HUKUSHA
        '            msg = "複写"

        '        Case LMM240C.EventShubetsu.SAKUJO
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

            '帳票パターンコード
            vCell.SetValidateCell(0, LMM240G.sprDetailDef.PTN_CD.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "帳票パターンコード"
            vCell.ItemName = LMM240G.sprDetailDef.PTN_CD.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '帳票パターン名
            vCell.SetValidateCell(0, LMM240G.sprDetailDef.PTN_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "パターン名"
            vCell.ItemName = LMM240G.sprDetailDef.PTN_NM.ColName
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
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM240C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM240C.EventShubetsu.SHINKI           '新規
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

            Case LMM240C.EventShubetsu.HENSHU          '編集
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

            Case LMM240C.EventShubetsu.HUKUSHA          '複写
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


            Case LMM240C.EventShubetsu.SAKUJO          '削除・復活
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

            Case LMM240C.EventShubetsu.KENSAKU         '検索
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


            Case LMM240C.EventShubetsu.HOZON           '保存
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

            Case LMM240C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM240C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM240C.EventShubetsu.ENTER          'Enter
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM240C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing
        Dim clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing
        Dim focusCtl As Control = Me._Frm.ActiveControl

        Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl, clearCtl)


    End Function

#End Region 'Method

End Class
