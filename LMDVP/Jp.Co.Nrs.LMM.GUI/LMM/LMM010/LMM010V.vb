' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM010V : ユーザーマスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMM010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM010F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM010F, ByVal v As LMMControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = v

        'Gamen共通クラスの設定
        Me._Gcon = New LMMControlG(handlerClass, DirectCast(frm, Form))

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

        'スペース除去
        Call Me._Vcon.TrimSpaceTextvalue(Me._Frm)

        '単項目チェック
        If Me.IsSaveSingleCheck() = False Then
            Return False
        End If

        ''関連チェック
        If Me.IsSaveCheck(Me._Frm) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 行追加/行削除/初期荷主 入力チェック。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsRowCheck(ByVal eventShubetsu As LMM010C.EventShubetsu, ByVal ds As DataSet, Optional ByVal pageNm As String = Nothing) As Boolean

        Dim arr As ArrayList = Nothing

        Select Case eventShubetsu

            Case LMM010C.EventShubetsu.INS_T    '行追加

                '要望番号:1248 yamanaka 2013.03.21 Start
                Dim dt As DataTable = Nothing
                Dim errFlg As Boolean = True
                '選択されたページのスプレッドを格納
                Dim spr As SheetView = Nothing

                '比較用プロパティ
                Dim muchDataL As SpreadColProperty = Nothing
                Dim muchDataM As SpreadColProperty = Nothing

                Select Case pageNm

                    Case Me._Frm.tpgMyCust.Name

                        dt = ds.Tables("LMZ260OUT")

                        spr = Me._Frm.sprDetail2.ActiveSheet
                        muchDataL = LMM010G.sprDetailDef2.CUST_CD_L
                        muchDataM = LMM010G.sprDetailDef2.CUST_CD_M

                        errFlg = Me.IsMuchCheck(pageNm, dt, spr, muchDataL.ColNo, muchDataM.ColNo)

                    Case Me._Frm.tpgMyUnsoco.Name

                        dt = ds.Tables("LMZ250OUT")

                        spr = Me._Frm.sprMyUnsoco.ActiveSheet
                        muchDataL = LMM010G.sprMyUnsocoDef.UNSOCO_CD
                        muchDataM = LMM010G.sprMyUnsocoDef.UNSOCO_BR_CD

                        errFlg = Me.IsMuchCheck(pageNm, dt, spr, muchDataL.ColNo, muchDataM.ColNo)

                    Case Me._Frm.tpgMyTariff.Name

                        dt = ds.Tables("LMZ230OUT")

                        spr = Me._Frm.sprMyTariff.ActiveSheet
                        muchDataL = LMM010G.sprMyTariffDef.UNCHIN_TARIFF

                        errFlg = Me.IsMuchCheck(pageNm, dt, spr, muchDataL.ColNo)

                End Select

                Return errFlg

                'Dim outSDt As DataTable = ds.Tables(LMZ260C.TABLE_NM_OUT)
                'Dim outSRow As DataRow = Nothing
                'outSRow = outSDt.Rows(0)
                'Dim PopCustL As String = String.Empty
                'Dim PopCustM As String = String.Empty
                'PopCustL = outSRow.Item("CUST_CD_L").ToString
                'PopCustM = outSRow.Item("CUST_CD_M").ToString

                'With Me._Frm

                '    Dim sprMax As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1
                '    For i As Integer = 0 To sprMax

                '        If (PopCustL).Equals(_Gcon.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM010C.SprColumnIndex2.CUST_CD_L))) AndAlso _
                '           (PopCustM).Equals(_Gcon.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM010C.SprColumnIndex2.CUST_CD_M))) Then
                '            MyBase.ShowMessage("E177", New String() {"荷主情報"})
                '            Return False
                '        End If
                '    Next

                'End With

                'Return True
                '要望番号:1248 yamanaka 2013.03.21 End

            Case LMM010C.EventShubetsu.DEL_T    '行削除

                With Me._Frm

                    '要望番号:1248 yamanaka 2013.03.22 Start
                    Dim spr As LMSpread = Nothing

                    Select Case pageNm

                        Case .tpgMyCust.Name

                            spr = .sprDetail2

                        Case .tpgMyUnsoco.Name

                            spr = .sprMyUnsoco

                        Case .tpgMyTariff.Name

                            spr = .sprMyTariff

                    End Select

                    '選択ﾁｪｯｸ
                    arr = Nothing
                    arr = Me.getCheckList(spr)

                    If 0 = arr.Count Then
                        spr.Focus()
                        MyBase.ShowMessage("E009")
                        Return False
                    End If

                    ''選択ﾁｪｯｸ
                    'arr = Nothing
                    'arr = Me.getCheckList(.sprDetail2)

                    'If 0 = arr.Count Then
                    '    .sprDetail2.Focus()
                    '    MyBase.ShowMessage("E009")
                    '    Return False
                    'End If
                    '要望番号:1248 yamanaka 2013.03.22 End

                    ''削除対象外ﾁｪｯｸ
                    'Dim arrMax As Integer = arr.Count - 1
                    'If -1 < arrMax Then
                    '    For i As Integer = 0 To arrMax
                    '        If ("01").Equals(_Gcon.GetCellValue(.sprDetail2.ActiveSheet.Cells(Convert.ToInt32((arr.Item(i).ToString)), LMM010C.SprColumnIndex2.DEFAULT_CUST_YN))) Then
                    '            MyBase.ShowMessage("E176")
                    '            Return False
                    '        End If
                    '    Next
                    'End If

                End With

                Return True

            Case LMM010C.EventShubetsu.DEF_T    '初期荷主

                With Me._Frm

                    '選択ﾁｪｯｸ
                    arr = Nothing
                    arr = Me.getCheckList(.sprDetail2)

                    If 0 = arr.Count Then
                        .sprDetail2.Focus()
                        MyBase.ShowMessage("E009")
                        Return False
                    End If

                    '複数選択ﾁｪｯｸ
                    arr = Nothing
                    arr = Me.getCheckList(.sprDetail2)

                    If 1 < arr.Count Then
                        .sprDetail2.Focus()
                        MyBase.ShowMessage("E008")
                        Return False
                    End If

                    '重複選択ﾁｪｯｸ
                    Dim arrMax As Integer = arr.Count - 1

                    If -1 < arrMax Then
                        For i As Integer = 0 To arrMax
                            If (LMMControlC.FLG_ON).Equals(_Gcon.GetCellValue(.sprDetail2.ActiveSheet.Cells(Convert.ToInt32((arr.Item(i).ToString)), LMM010C.SprColumnIndex2.DEFAULT_CUST_YN))) Then
                                '2016.01.06 UMANO 英語化対応START
                                'MyBase.ShowMessage("E196", New String() {"初期荷主", "荷主"})
                                MyBase.ShowMessage("E196")
                                '2016.01.06 UMANO 英語化対応END
                                Return False
                            End If
                        Next
                    End If

                End With

                Return True

        End Select


    End Function

    '要望番号:1248 yamanaka 2013.03.21 Start
    ''' <summary>
    ''' 同データチェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsMuchCheck(ByVal pageNm As String, ByVal dt As DataTable, ByVal spr As SheetView, ByVal muchDataL As Integer, Optional ByVal muchDataM As Integer = 0) As Boolean

        With Me._Frm

            Dim dr As DataRow = Nothing
            'Dim msg As String = String.Empty
            Dim errFlg As Boolean = True
            Dim PopDataL As String = String.Empty
            Dim PopDataM As String = String.Empty
            Dim max As Integer = spr.Rows.Count - 1

            dr = dt.Rows(0)

            Select Case pageNm

                Case .tpgMyCust.Name

                    PopDataL = dr.Item("CUST_CD_L").ToString
                    PopDataM = dr.Item("CUST_CD_M").ToString
                    'msg = "荷主情報"

                Case .tpgMyUnsoco.Name

                    PopDataL = dr.Item("UNSOCO_CD").ToString
                    PopDataM = dr.Item("UNSOCO_BR_CD").ToString
                    'msg = "運送会社情報"

                Case .tpgMyTariff.Name

                    PopDataL = dr.Item("UNCHIN_TARIFF_CD").ToString
                    'msg = "運賃タリフ情報"

            End Select

            For i As Integer = 0 To max

                If (PopDataL).Equals(Me._Gcon.GetCellValue(spr.Cells(i, muchDataL))) Then

                    Select Case pageNm

                        Case .tpgMyCust.Name, .tpgMyUnsoco.Name

                            If (PopDataM).Equals(Me._Gcon.GetCellValue(spr.Cells(i, muchDataM))) Then
                                errFlg = False
                            End If

                        Case Else

                            errFlg = False

                    End Select


                    If errFlg = False Then

                        '2016.01.06 UMANO 英語化対応START
                        Select Case pageNm

                            Case .tpgMyCust.Name
                                MyBase.ShowMessage("E864")
                            Case .tpgMyUnsoco.Name
                                MyBase.ShowMessage("E865")
                            Case .tpgMyTariff.Name
                                MyBase.ShowMessage("E866")

                        End Select
                        '2016.01.06 UMANO 英語化対応END                        

                        Return errFlg

                    End If

                End If

            Next

            Return errFlg

        End With

    End Function
    '要望番号:1248 yamanaka 2013.03.21 End

    '''' <summary>
    '''' 保存時のtrim
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub TrimSpaceTextValue()

    '    With Me._Frm
    '        .txtUserId.TextValue = .txtUserId.TextValue.Trim()
    '        .txtUserNm.TextValue = .txtUserNm.TextValue.Trim()
    '        .txtPw.TextValue = .txtPw.TextValue.Trim()
    '    End With

    'End Sub


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

            'ユーザーコード
            .txtUserId.ItemName = .lblUserId.Text
            .txtUserId.IsHissuCheck = chkFlg
            .txtUserId.IsForbiddenWordsCheck = chkFlg
            .txtUserId.IsFullByteCheck = 5
            .txtUserId.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtUserId) = errorFlg Then
                Return errorFlg
            End If

            'ユーザー名
            .txtUserNm.ItemName = .lblUserNm.Text
            .txtUserNm.IsHissuCheck = chkFlg
            .txtUserNm.IsForbiddenWordsCheck = chkFlg
            .txtUserNm.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtUserNm) = errorFlg Then
                Return errorFlg
            End If

            'メールアドレス
            .txtEMail.ItemName = .lblEMail.Text
            .txtEMail.IsHankakuCheck = chkFlg
            .txtEMail.IsMiddleSpace = chkFlg
            .txtEMail.IsHissuCheck = .chkNoticeYn.Checked
            .txtEMail.IsByteCheck = 50
            If MyBase.IsValidateCheck(.txtEMail) = errorFlg Then
                Return errorFlg
            End If

            '権限レベル
            '2016.01.06 UMANO 英語化対応START
            '.cmbAuthoLv.ItemName = String.Concat(.lblAuthoLv.Text, "レベル")
            .cmbAuthoLv.ItemName = String.Concat(.lblAuthoLv.Text)
            '2016.01.06 UMANO 英語化対応END
            .cmbAuthoLv.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbAuthoLv) = errorFlg Then
                Return errorFlg
            End If

            'パスワード
            .txtPw.ItemName = .lblPw.Text
            .txtPw.IsHissuCheck = chkFlg
            .txtPw.IsForbiddenWordsCheck = chkFlg
            .txtPw.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtPw) = errorFlg Then
                Return errorFlg
            End If

            '入荷の日付初期値
            .cmbInkaDateInit.ItemName = .lblInkaDateInit.Text
            .cmbInkaDateInit.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbInkaDateInit) = errorFlg Then
                Return errorFlg
            End If

            '出荷の日付初期値
            .cmbOutkaDateInit.ItemName = .lblOutkaDateInit.Text
            .cmbOutkaDateInit.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbOutkaDateInit) = errorFlg Then
                Return errorFlg
            End If

            'デフォルトプリンタ1
            .cmbDefPrt1.ItemName = .lblDefPrt1.Text
            If String.IsNullOrEmpty(.cmbDefPrt1.SelectedText) = chkFlg Then
                .cmbDefPrt1.IsHissuCheck = chkFlg
                If MyBase.IsValidateCheck(.cmbDefPrt1) = errorFlg Then
                    Return errorFlg
                End If
            End If

            'デフォルトプリンタ2
            .cmbDefPrt2.ItemName = .lblDefPrt2.Text
            If String.IsNullOrEmpty(.cmbDefPrt2.SelectedText) = chkFlg Then
                .cmbDefPrt2.IsHissuCheck = chkFlg
                If MyBase.IsValidateCheck(.cmbDefPrt2) = errorFlg Then
                    Return errorFlg
                End If
            End If

            'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
            'デフォルトプリンタ3
            .cmbDefPrt3.ItemName = .lblDefPrt3.Text
            If String.IsNullOrEmpty(.cmbDefPrt3.SelectedText) = chkFlg Then
                .cmbDefPrt3.IsHissuCheck = chkFlg
                If MyBase.IsValidateCheck(.cmbDefPrt3) = errorFlg Then
                    Return errorFlg
                End If
            End If

            'デフォルトプリンタ4
            .cmbDefPrt4.ItemName = .lblDefPrt4.Text
            If String.IsNullOrEmpty(.cmbDefPrt4.SelectedText) = chkFlg Then
                .cmbDefPrt4.IsHissuCheck = chkFlg
                If MyBase.IsValidateCheck(.cmbDefPrt4) = errorFlg Then
                    Return errorFlg
                End If
            End If

            'デフォルトプリンタ5
            .cmbDefPrt5.ItemName = .lblDefPrt5.Text
            If String.IsNullOrEmpty(.cmbDefPrt5.SelectedText) = chkFlg Then
                .cmbDefPrt5.IsHissuCheck = chkFlg
                If MyBase.IsValidateCheck(.cmbDefPrt5) = errorFlg Then
                    Return errorFlg
                End If
            End If

            'デフォルトプリンタ6
            .cmbDefPrt6.ItemName = .lblDefPrt6.Text
            If String.IsNullOrEmpty(.cmbDefPrt6.SelectedText) = chkFlg Then
                .cmbDefPrt6.IsHissuCheck = chkFlg
                If MyBase.IsValidateCheck(.cmbDefPrt6) = errorFlg Then
                    Return errorFlg
                End If
            End If

            'デフォルトプリンタ7
            .cmbDefPrt7.ItemName = .lblDefPrt7.Text
            If String.IsNullOrEmpty(.cmbDefPrt7.SelectedText) = chkFlg Then
                .cmbDefPrt7.IsHissuCheck = chkFlg
                If MyBase.IsValidateCheck(.cmbDefPrt7) = errorFlg Then
                    Return errorFlg
                End If
            End If
            'END YANAI 要望番号675 プリンタの設定を個人別を可能にする

            '分析表プリンタ
            .cmbCoaPrt.ItemName = .lblTitleCoaPrt.Text
            If String.IsNullOrEmpty(.cmbCoaPrt.SelectedText) = chkFlg Then
                .cmbCoaPrt.IsHissuCheck = chkFlg
                If MyBase.IsValidateCheck(.cmbCoaPrt) = errorFlg Then
                    Return errorFlg
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveCheck(ByVal frm As LMM010F) As Boolean

        Dim ctl As Control() = Nothing
        Dim focus As Control = Nothing

        With Me._Frm
            '権限がなければSAP連携権限を"有り"にはできない
            Dim authoLv As String = .cmbAuthoLv.SelectedValue.ToString()
            If (Not LMConst.AuthoKBN.LEADER.Equals(authoLv)) AndAlso (Not LMConst.AuthoKBN.MANAGER.Equals(authoLv)) Then
                If "01".Equals(.cmbSapLinkAutho.SelectedValue.ToString()) Then
                    MyBase.ShowMessage("E320", New String() {"権限のないユーザ", "SAP連携は"})
                    .cmbSapLinkAutho.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    ctl = New Control() { .cmbSapLinkAutho}
                    focus = .cmbSapLinkAutho
                    Call Me._Vcon.SetErrorControl(ctl, focus)
                    Return False
                End If
            End If

            Dim sprMax As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1
            Dim defCount As Integer = 0

            'デフォルト荷主Spreadに行が存在しない場合はチェックなし
            If sprMax < 0 Then
                'チェックなし
            Else
                For i As Integer = 0 To sprMax

                    If (LMM010C.DEFAULT_CUST_Y).Equals(_Gcon.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM010C.SprColumnIndex2.DEF_CUST))) OrElse
                       (LMMControlC.FLG_ON).Equals(_Gcon.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM010C.SprColumnIndex2.DEFAULT_CUST_YN))) Then
                        defCount = defCount + 1
                    End If
                Next
                If defCount = 0 Then
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E019", New String() {"初期荷主"})
                    'MyBase.ShowMessage("E019", New String() {frm.tabMyData.Name()})
                    MyBase.ShowMessage("E019", New String() {frm.tpgMyCust.Text}) '20160603 tsunehira add　「初期荷主」が表示されない対応
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If
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
    Friend Function IsRecordStatusChk(ByVal frm As LMM010F) As Boolean

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
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM010F, ByVal eventShubetsu As LMM010C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM010C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM010C.EventShubetsu.HUKUSHA
        '            msg = "複写"

        '        Case LMM010C.EventShubetsu.SAKUJO
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

        Dim defNo As Integer = LMM010G.sprDetailDef.DEF.ColNo

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
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleCheck() As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            'ユーザーコード
            vCell.SetValidateCell(0, LMM010G.sprDetailDef.USER_ID.ColNo)
            vCell.ItemName = LMM010G.sprDetailDef.USER_ID.ColName
            vCell.IsForbiddenWordsCheck = chkFlg
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If
            'ユーザー名
            vCell.SetValidateCell(0, LMM010G.sprDetailDef.USER_NM.ColNo)
            vCell.ItemName = LMM010G.sprDetailDef.USER_NM.ColName
            vCell.IsForbiddenWordsCheck = chkFlg
            vCell.IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM010C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM010C.EventShubetsu.SHINKI           '新規
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen upd start
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
                'Select Case kengen
                '    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                '        kengenFlg = False
                '    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                '        kengenFlg = True
                '    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                '        kengenFlg = True
                '    Case LMConst.AuthoKBN.LEADER    '30:管理職
                '        kengenFlg = True
                '    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                '        kengenFlg = True
                '    Case LMConst.AuthoKBN.AGENT     '50:外部
                '        kengenFlg = False
                'End Select
                '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen upd end

            Case LMM010C.EventShubetsu.HENSHU          '編集
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen upd start
                Dim activeUserCd As String = LMUserInfoManager.GetUserID
                Dim activeUserFlg As Boolean = activeUserCd.Equals(Me._Frm.txtUserId.TextValue)
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = activeUserFlg
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = activeUserFlg
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select
                'Select Case kengen
                '    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                '        kengenFlg = False
                '    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                '        kengenFlg = True
                '    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                '        kengenFlg = True
                '    Case LMConst.AuthoKBN.LEADER    '30:管理職
                '        kengenFlg = True
                '    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                '        kengenFlg = True
                '    Case LMConst.AuthoKBN.AGENT     '50:外部
                '        kengenFlg = False
                'End Select
                '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen upd end

            Case LMM010C.EventShubetsu.HUKUSHA          '複写
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen upd start
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
                'Select Case kengen
                '    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                '        kengenFlg = False
                '    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                '        kengenFlg = True
                '    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                '        kengenFlg = True
                '    Case LMConst.AuthoKBN.LEADER    '30:管理職
                '        kengenFlg = True
                '    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                '        kengenFlg = True
                '    Case LMConst.AuthoKBN.AGENT     '50:外部
                '        kengenFlg = False
                'End Select
                ''2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen upd start

            Case LMM010C.EventShubetsu.SAKUJO          '削除・復活
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

            Case LMM010C.EventShubetsu.KENSAKU         '検索
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

            Case LMM010C.EventShubetsu.MASTEROPEN          'マスタ参照
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

            Case LMM010C.EventShubetsu.HOZON           '保存
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

            Case LMM010C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM010C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM010C.EventShubetsu.ENTER          'Enter
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

    '要望番号:1248 yamanaka 2013.03.22 Start
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList(ByVal sprDetail As LMSpread) As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = 0

        Select Case sprDetail.Name

            Case Me._Frm.sprDetail2.Name

                defNo = LMM010C.SprColumnIndex2.DEF

            Case Me._Frm.sprMyUnsoco.Name

                defNo = LMM010C.SprColumnIndex3.DEF

            Case Me._Frm.sprMyTariff.Name

                defNo = LMM010C.SprColumnIndex4.DEF

        End Select

        '選択された行の行番号を取得
        Return _Vcon.SprSelectList(defNo, sprDetail)

    End Function

    '''' <summary>
    '''' 選択された行の行番号をArrayListに格納
    '''' </summary>
    '''' <remarks></remarks>
    'Friend Function getCheckList(ByVal sprDetail As LMSpread) As ArrayList

    '    'チェックボックスセルのカラム№取得
    '    Dim defNo As Integer = 0
    '    If ("sprDetail2").Equals(sprDetail.Name) = True Then
    '        defNo = LMM010C.SprColumnIndex2.DEF
    '    End If

    '    '選択された行の行番号を取得
    '    Return _Vcon.SprSelectList(defNo, sprDetail)

    'End Function
    '要望番号:1248 yamanaka 2013.03.22 End



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

        ctl.Focus()
        ctl.Select()

    End Sub

#End Region

#End Region 'Method

End Class
