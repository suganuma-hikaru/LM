' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM030V : 作業項目マスタメンテ
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
''' LMM030Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM030V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM030F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM030F, ByVal v As LMMControlV)

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

        '関連チェック
        If Me.IsSaveRerateCheck() = False Then
            Return False
        End If

        '荷主存在チェック
        If Me.IsSaveExistCheck() = False Then
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

            .txtSagyoCd.TextValue = .txtSagyoCd.TextValue.Trim()
            .txtSagyoNm.TextValue = .txtSagyoNm.TextValue.Trim()
            .txtSagyoRyak.TextValue = .txtSagyoRyak.TextValue.Trim()
            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtRemark.TextValue = .txtRemark.TextValue.Trim()
            .txtSagyoSubCd.TextValue = .txtSagyoSubCd.TextValue.Trim()

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
            .cmbNrsBrCd.ItemName = .TitlelblEigyo.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            '荷主コード(大)
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


            '作業項目名
            '2016.01.06 UMANO 英語化対応START
            '.txtSagyoNm.ItemName = "作業項目名"
            .txtSagyoNm.ItemName = .lblTitleSagyo.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtSagyoNm.IsHissuCheck = True
            .txtSagyoNm.IsForbiddenWordsCheck = True
            .txtSagyoNm.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtSagyoNm) = False Then
                Return False
            End If

            '作業項目略称
            '2016.01.06 UMANO 英語化対応START
            '.txtSagyoRyak.ItemName = "作業項目略称"
            .txtSagyoRyak.ItemName = .lblTitleRyak.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtSagyoRyak.IsHissuCheck = True
            .txtSagyoRyak.IsForbiddenWordsCheck = True
            .txtSagyoRyak.IsByteCheck = 6
            If MyBase.IsValidateCheck(.txtSagyoRyak) = False Then
                Return False
            End If

            '請求の有/無
            '2016.01.06 UMANO 英語化対応START
            '.cmbInvYn.ItemName = "請求の有/無"
            .cmbInvYn.ItemName = .lblTitleInvYn.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbInvYn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbInvYn) = False Then
                Return False
            End If

            '請求書作成時個数で乗する
            '2016.01.06 UMANO 英語化対応START
            '.cmbKosuBai.ItemName = "請求書作成時個数で乗する"
            .cmbKosuBai.ItemName = .lblTitleKosu.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbKosuBai.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbKosuBai) = False Then
                Return False
            End If

            '税区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbZeiKbn.ItemName = "税区分"
            .cmbZeiKbn.ItemName = .lblTitleZei.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbZeiKbn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbZeiKbn) = False Then
                Return False
            End If

            '作業指示明細
            '2016.01.06 UMANO 英語化対応START
            '.cmbSplRpt.ItemName = "作業指示明細"
            .cmbSplRpt.ItemName = .lblTitleSpl.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbSplRpt.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbSplRpt) = False Then
                Return False
            End If

            '進捗管理の有/無
            '2016.01.06 UMANO 英語化対応START
            '.cmbFlwpYn.ItemName = "進捗管理の有/無"
            .cmbFlwpYn.ItemName = .lblTitleFlwp.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbFlwpYn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbFlwpYn) = False Then
                Return False
            End If

            '備考
            '2016.01.06 UMANO 英語化対応START
            '.txtRemark.ItemName = "備考"
            .txtRemark.ItemName = .lblTitleRem.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtRemark.IsForbiddenWordsCheck = True
            .txtRemark.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtRemark) = False Then
                Return False
            End If

            '現場作業の有無
            .cmbWHSagyoYn.ItemName = .lblWHSagyoYn.Text()
            .cmbWHSagyoYn.IsHissuCheck = False
            If MyBase.IsValidateCheck(.cmbWHSagyoYn) = False Then
                Return False
            End If

            '現場作業名
            .txtWhSagyoNm.ItemName = .lblSijiSagyoNm.Text()
            .txtWhSagyoNm.IsForbiddenWordsCheck = True
            .txtWhSagyoNm.IsByteCheck = 120
            If MyBase.IsValidateCheck(.txtWhSagyoNm) = False Then
                Return False
            End If

            '現場作業備考
            .txtWhSagyoRemark.ItemName = .lblSijiRemark.Text()
            .txtWhSagyoRemark.IsForbiddenWordsCheck = True
            .txtWhSagyoRemark.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtWhSagyoRemark) = False Then
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
    Private Function IsSaveRerateCheck() As Boolean

        '置換文字設定
        '2016.01.06 UMANO 英語化対応START
        'Dim msgTani As String = LMM030C.INVTANI
        'Dim msgTanka As String = LMM030C.TANKA
        'Dim msgYn As String = LMM030C.INVMSG
        Dim msgTani As String = Me._Frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM030C.SprColumnIndex.INV_TANI)
        Dim msgTanka As String = Me._Frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM030C.SprColumnIndex.SAGYO_UP)
        Dim msgYn As String = LMM030C.INVMSG
        '2016.01.06 UMANO 英語化対応END

        With Me._Frm

            Dim invTani As String = .cmbInvTani.SelectedValue.ToString()
            Dim sagyoUp As Decimal = Convert.ToDecimal(.numSagyoUp.Value)
            Dim invYn As String = .cmbInvYn.SelectedValue.ToString()

            '単位が選択されて単価が0のときエラー
            If String.IsNullOrEmpty(invTani) = False _
              AndAlso sagyoUp = 0 Then
                Me.SetErrorControl(.numSagyoUp)
                Return Me._Vcon.SetErrMessage("E017", New String() {msgTani, msgTanka})
            End If

            '単価が0ではなく請求単位が未選択のときエラー
            If sagyoUp <> 0 _
              AndAlso String.IsNullOrEmpty(invTani) = True Then
                Me.SetErrorControl(.cmbInvTani)
                Return Me._Vcon.SetErrMessage("E017", New String() {msgTanka, msgTani})
            End If

            '請求有無が○(有)で請求単位が未選択のときエラー
            If LMM030C.ARI.Equals(invYn) = True _
              AndAlso String.IsNullOrEmpty(invTani) = True Then
                Me.SetErrorControl(.cmbInvTani)
                '2016.01.06 UMANO 英語化対応START
                'Return Me._Vcon.SetErrMessage("E187", New String() {msgYn, msgTani})
                Return Me._Vcon.SetErrMessage("E891", New String() {msgTani})
                '2016.01.06 UMANO 英語化対応END
            End If

            '請求有無が○(有)で単価が0のときエラー
            If LMM030C.ARI.Equals(invYn) = True _
              AndAlso sagyoUp = 0 Then
                Me.SetErrorControl(.numSagyoUp)
                '2016.01.06 UMANO 英語化対応START
                'Return Me._Vcon.SetErrMessage("E187", New String() {msgYn, msgTanka})
                Return Me._Vcon.SetErrMessage("E891", New String() {msgTanka})
                '2016.01.06 UMANO 英語化対応END
            End If

        End With

        Return True
   
    End Function

    ''' <summary>
    ''' 存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSaveExistCheck() As Boolean

        Dim custL As String = Me._Frm.txtCustCdL.TextValue
        Dim defaultKbn As String = LMM030C.NINUSHI

        '荷主(大)、荷主(中)の関連チェック
        If Me.IsCustMChk(custL, defaultKbn, defaultKbn) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <param name="custL"></param>
    ''' <param name="custM"></param>
    ''' <param name="defaultKbn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsCustMChk(ByVal custL As String, ByVal custM As String, ByVal defaultKbn As String) As Boolean

        With Me._Frm

            Dim drs As DataRow() = Me._Vcon.SelectCustListDataRow(custL, defaultKbn, defaultKbn, defaultKbn)

            If drs.Length < 1 Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E079", New String() {"荷主マスタ", custL})
                MyBase.ShowMessage("E767", New String() {"荷主マスタ", custL})
                '2016.01.06 UMANO 英語化対応END
                .lblCustNmL.TextValue = String.Empty
                Me.SetErrorControl(.txtCustCdL)
                Return False
            End If

            'マスタの値を設定
            .txtCustCdL.TextValue = drs(0).Item("CUST_CD_L").ToString()
            .lblCustNmL.TextValue = drs(0).Item("CUST_NM_L").ToString()

            Return True

        End With

    End Function


    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM030F) As Boolean

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
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM030F, ByVal eventShubetsu As LMM030C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM030C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM030C.EventShubetsu.HUKUSHA
        '            msg = "複写"

        '        Case LMM030C.EventShubetsu.SAKUJO
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

        Dim defNo As Integer = LMM030G.sprDetailDef.DEF.ColNo

        With Me._Frm.sprDetail.ActiveSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If _Vcon.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
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

            '作業コード
            vCell.SetValidateCell(0, LMM030G.sprDetailDef.SAGYO_CD.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "作業コード"
            vCell.ItemName = LMM030G.sprDetailDef.SAGYO_CD.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '作業項目名
            vCell.SetValidateCell(0, LMM030G.sprDetailDef.SAGYO_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "作業項目名"
            vCell.ItemName = LMM030G.sprDetailDef.SAGYO_NM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '作業項目略称
            vCell.SetValidateCell(0, LMM030G.sprDetailDef.SAGYO_RYAK.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "作業項目略称"
            vCell.ItemName = LMM030G.sprDetailDef.SAGYO_RYAK.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 6
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主コード(大)
            vCell.SetValidateCell(0, LMM030G.sprDetailDef.CUST_CD_L.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主コード(大)"
            vCell.ItemName = LMM030G.sprDetailDef.CUST_CD_L.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名(大)
            vCell.SetValidateCell(0, LMM030G.sprDetailDef.CUST_NM_L.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主名(大)"
            vCell.ItemName = LMM030G.sprDetailDef.CUST_NM_L.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '協力会社作業コード
            vCell.SetValidateCell(0, LMM030G.sprDetailDef.SAGYO_SUB_CD.ColNo)
            vCell.ItemName = LMM030G.sprDetailDef.SAGYO_SUB_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '作業項目名
            vCell.SetValidateCell(0, LMM030G.sprDetailDef.SAGYO_SUB_NM.ColNo)
            vCell.ItemName = LMM030G.sprDetailDef.SAGYO_SUB_NM.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM030C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM030C.EventShubetsu.SHINKI           '新規
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

            Case LMM030C.EventShubetsu.HENSHU          '編集
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

            Case LMM030C.EventShubetsu.HUKUSHA          '複写
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


            Case LMM030C.EventShubetsu.SAKUJO          '削除・復活
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

            Case LMM030C.EventShubetsu.KENSAKU         '検索
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

            Case LMM030C.EventShubetsu.MASTEROPEN          'マスタ参照
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

            Case LMM030C.EventShubetsu.HOZON           '保存
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

            Case LMM030C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM030C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM030C.EventShubetsu.ENTER          'Enter
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM030C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM030C.EventShubetsu.MASTEROPEN) = True Then
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

                Case .txtCustCdL.Name

                    ctl = New Win.InputMan.LMImTextBox() { .txtCustCdL}
                    msg = New String() { .lblTitleCust.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() { .lblCustNmL}

                Case .txtSagyoSubCd.Name

                    ctl = New Win.InputMan.LMImTextBox() { .txtSagyoSubCd}
                    msg = New String() { .lblTitleSagyoSub.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() { .lblSagyoSubNm, .lblCustSubCdL, .lblCustSubNmL}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl, clearCtl)

        End With

    End Function

#Region "部品化検討中"

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal ctl As Control)

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor

        If TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).BackColorDef = errorColor

        End If

        If TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            DirectCast(ctl, Win.InputMan.LMImNumber).BackColorDef = errorColor

        End If

        If TypeOf ctl Is Win.InputMan.LMImCombo = True Then

            DirectCast(ctl, Win.InputMan.LMImCombo).BackColorDef = errorColor

        End If


        ctl.Focus()
        ctl.Select()

    End Sub



#End Region

#End Region 'Method


End Class
