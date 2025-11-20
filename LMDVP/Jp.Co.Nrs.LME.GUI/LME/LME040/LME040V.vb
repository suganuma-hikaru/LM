' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME040  : 作業指示書検索
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LME040Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LME040V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LME040F

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LME040G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEconV As LMEControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LME040F, ByVal v As LMEControlV, ByVal g As LME040G)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMEconV = v

        'Gamenクラスの設定
        Me._G = New LME040G(handlerClass, frm)

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 単項目入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(ByVal eventShubetsu As LME040C.EventShubetsu) As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        Dim rtnFlg As Boolean = True

        '【単項目チェック】
        With Me._Frm

            Dim arr As ArrayList = Nothing
            arr = Me.GetCheckList(LME040G.sprDetailsDef.DEF.ColNo, .sprDetails)
            Dim max As Integer = arr.Count - 1
            Dim sprmax As Integer = .sprDetails.ActiveSheet.Rows.Count - 1

            If LME040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '作業日
                .imdSagyoDate.ItemName() = .lblTitleDate.TextValue
                .imdSagyoDate.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdSagyoDate) = False Then
                    Return False
                End If
            End If

            If LME040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '注意事項1
                .txtRemark1.ItemName() = .lblTitleRemark1.TextValue
                .txtRemark1.IsForbiddenWordsCheck() = True
                .txtRemark1.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtRemark1) = False Then
                    Return False
                End If
            End If

            If LME040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '注意事項2
                .txtRemark2.ItemName() = .lblTitleRemark2.TextValue
                .txtRemark2.IsForbiddenWordsCheck() = True
                .txtRemark2.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtRemark2) = False Then
                    Return False
                End If
            End If

            If LME040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '注意事項3
                .txtRemark3.ItemName() = .lblTitleRemark3.TextValue
                .txtRemark3.IsForbiddenWordsCheck() = True
                .txtRemark3.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtRemark3) = False Then
                    Return False
                End If
            End If

            If LME040C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '印刷種別
                .cmbPrint.ItemName() = .lblTitlePrint.Text
                .cmbPrint.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbPrint) = False Then
                    Return False
                End If
            End If

            If LME040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If sprmax < 0 Then
                    '一覧の件数チェック
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E296", New String() {"作業情報"})
                    MyBase.ShowMessage("E853")
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If
            End If

            If LME040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                LME040C.EventShubetsu.ROWADD.Equals(eventShubetsu) = True OrElse _
                LME040C.EventShubetsu.LEAVECELL.Equals(eventShubetsu) = True Then
                If String.IsNullOrEmpty(.txtSagyo1.TextValue) = False AndAlso _
                    .txtSagyo1.ReadOnly = False Then
                    '作業コード1
                    .txtSagyo1.ItemName() = .lblTitleSagyo1.TextValue
                    .txtSagyo1.IsForbiddenWordsCheck() = True
                    .txtSagyo1.IsFullByteCheck() = 5
                    If MyBase.IsValidateCheck(.txtSagyo1) = False Then
                        Return False
                    End If

                    'マスタ存在チェック
                    rtnFlg = Me.IsSagyoExistChk(.txtSagyo1.TextValue, .txtCustCdL.TextValue)
                    If rtnFlg = False Then
                        Me._LMEconV.SetErrorControl(.txtSagyo1)
                        '2016.01.06 UMANO 英語化対応START
                        'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyo1.TextValue})
                        MyBase.ShowMessage("E694", New String() {.txtSagyo1.TextValue})
                        '2016.01.06 UMANO 英語化対応END
                        Return False
                    End If

                    '作業重複チェック
                    If (.txtSagyo1.TextValue = .txtSagyo2.TextValue OrElse _
                        .txtSagyo1.TextValue = .txtSagyo3.TextValue OrElse _
                        .txtSagyo1.TextValue = .txtSagyo4.TextValue OrElse _
                        .txtSagyo1.TextValue = .txtSagyo5.TextValue) Then
                        Me._LMEconV.SetErrorControl(.txtSagyo1)
                        MyBase.ShowMessage("E131")
                        Return False
                    End If

                End If
            End If

            If LME040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                LME040C.EventShubetsu.ROWADD.Equals(eventShubetsu) = True OrElse _
                LME040C.EventShubetsu.LEAVECELL.Equals(eventShubetsu) = True Then
                If String.IsNullOrEmpty(.txtSagyo2.TextValue) = False AndAlso _
                    .txtSagyo2.ReadOnly = False Then
                    '作業コード2
                    .txtSagyo2.ItemName() = .lblTitleSagyo2.TextValue
                    .txtSagyo2.IsForbiddenWordsCheck() = True
                    .txtSagyo2.IsFullByteCheck() = 5
                    If MyBase.IsValidateCheck(.txtSagyo2) = False Then
                        Return False
                    End If

                    'マスタ存在チェック
                    rtnFlg = Me.IsSagyoExistChk(.txtSagyo2.TextValue, .txtCustCdL.TextValue)
                    If rtnFlg = False Then
                        Me._LMEconV.SetErrorControl(.txtSagyo2)
                        '2016.01.06 UMANO 英語化対応START
                        'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyo1.TextValue})
                        MyBase.ShowMessage("E694", New String() {.txtSagyo2.TextValue})
                        '2016.01.06 UMANO 英語化対応END
                        Return False
                    End If

                    '作業重複チェック
                    If (.txtSagyo2.TextValue = .txtSagyo3.TextValue OrElse _
                        .txtSagyo2.TextValue = .txtSagyo4.TextValue OrElse _
                        .txtSagyo2.TextValue = .txtSagyo5.TextValue) Then
                        Me._LMEconV.SetErrorControl(.txtSagyo2)
                        MyBase.ShowMessage("E131")
                        Return False
                    End If

                End If
            End If

            If LME040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                LME040C.EventShubetsu.ROWADD.Equals(eventShubetsu) = True OrElse _
                LME040C.EventShubetsu.LEAVECELL.Equals(eventShubetsu) = True Then
                If String.IsNullOrEmpty(.txtSagyo3.TextValue) = False AndAlso _
                    .txtSagyo3.ReadOnly = False Then
                    '作業コード3
                    .txtSagyo3.ItemName() = .lblTitleSagyo3.TextValue
                    .txtSagyo3.IsForbiddenWordsCheck() = True
                    .txtSagyo3.IsFullByteCheck() = 5
                    If MyBase.IsValidateCheck(.txtSagyo3) = False Then
                        Return False
                    End If

                    'マスタ存在チェック
                    rtnFlg = Me.IsSagyoExistChk(.txtSagyo3.TextValue, .txtCustCdL.TextValue)
                    If rtnFlg = False Then
                        Me._LMEconV.SetErrorControl(.txtSagyo3)
                        '2016.01.06 UMANO 英語化対応START
                        'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyo1.TextValue})
                        MyBase.ShowMessage("E694", New String() {.txtSagyo3.TextValue})
                        '2016.01.06 UMANO 英語化対応END
                        Return False
                    End If

                    '作業重複チェック
                    If (.txtSagyo3.TextValue = .txtSagyo4.TextValue OrElse _
                        .txtSagyo3.TextValue = .txtSagyo5.TextValue) Then
                        Me._LMEconV.SetErrorControl(.txtSagyo3)
                        MyBase.ShowMessage("E131")
                        Return False
                    End If

                End If
            End If

            If LME040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                LME040C.EventShubetsu.ROWADD.Equals(eventShubetsu) = True OrElse _
                LME040C.EventShubetsu.LEAVECELL.Equals(eventShubetsu) = True Then
                If String.IsNullOrEmpty(.txtSagyo4.TextValue) = False AndAlso _
                    .txtSagyo4.ReadOnly = False Then
                    '作業コード4
                    .txtSagyo4.ItemName() = .lblTitleSagyo4.TextValue
                    .txtSagyo4.IsForbiddenWordsCheck() = True
                    .txtSagyo4.IsFullByteCheck() = 5
                    If MyBase.IsValidateCheck(.txtSagyo4) = False Then
                        Return False
                    End If
                    'マスタ存在チェック
                    rtnFlg = Me.IsSagyoExistChk(.txtSagyo4.TextValue, .txtCustCdL.TextValue)
                    If rtnFlg = False Then
                        Me._LMEconV.SetErrorControl(.txtSagyo4)
                        '2016.01.06 UMANO 英語化対応START
                        'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyo1.TextValue})
                        MyBase.ShowMessage("E694", New String() {.txtSagyo4.TextValue})
                        '2016.01.06 UMANO 英語化対応END
                        Return False
                    End If

                    '作業重複チェック
                    If (.txtSagyo4.TextValue = .txtSagyo5.TextValue) Then
                        Me._LMEconV.SetErrorControl(.txtSagyo4)
                        MyBase.ShowMessage("E131")
                        Return False
                    End If

                End If
            End If

            If LME040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                LME040C.EventShubetsu.ROWADD.Equals(eventShubetsu) = True OrElse _
                LME040C.EventShubetsu.LEAVECELL.Equals(eventShubetsu) = True Then
                If String.IsNullOrEmpty(.txtSagyo5.TextValue) = False AndAlso _
                    .txtSagyo5.ReadOnly = False Then
                    '作業コード5
                    .txtSagyo5.ItemName() = .lblTitleSagyo5.TextValue
                    .txtSagyo5.IsForbiddenWordsCheck() = True
                    .txtSagyo5.IsFullByteCheck() = 5
                    If MyBase.IsValidateCheck(.txtSagyo5) = False Then
                        Return False
                    End If

                    'マスタ存在チェック
                    rtnFlg = Me.IsSagyoExistChk(.txtSagyo5.TextValue, .txtCustCdL.TextValue)
                    If rtnFlg = False Then
                        Me._LMEconV.SetErrorControl(.txtSagyo5)
                        '2016.01.06 UMANO 英語化対応START
                        'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyo1.TextValue})
                        MyBase.ShowMessage("E694", New String() {.txtSagyo5.TextValue})
                        '2016.01.06 UMANO 英語化対応END
                        Return False
                    End If
                End If
            End If

            If LME040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                LME040C.EventShubetsu.ROWADD.Equals(eventShubetsu) = True OrElse _
                LME040C.EventShubetsu.LEAVECELL.Equals(eventShubetsu) = True Then
                '作業未入力チェック
                If .txtSagyo1.ReadOnly = False AndAlso _
                    String.IsNullOrEmpty(.txtSagyo1.TextValue) = True AndAlso _
                    String.IsNullOrEmpty(.txtSagyo2.TextValue) = True AndAlso _
                    String.IsNullOrEmpty(.txtSagyo3.TextValue) = True AndAlso _
                    String.IsNullOrEmpty(.txtSagyo4.TextValue) = True AndAlso _
                    String.IsNullOrEmpty(.txtSagyo5.TextValue) = True Then
                    Me._LMEconV.SetErrorControl(.txtSagyo1)
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E296", New String() {"作業情報"})
                    MyBase.ShowMessage("E853")
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If
            End If


            'スプレッドのチェック
            If LME040C.EventShubetsu.ROWDEL.Equals(eventShubetsu) = True Then
                '未選択チェック
                If max < 0 Then
                    MyBase.ShowMessage("E009")
                    Return False
                End If
            End If

            Dim zaiRecNo As String = String.Empty
            If LME040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '在庫レコード番号重複チェック
                For i As Integer = 0 To sprmax
                    zaiRecNo = Me._LMEconV.GetCellValue(.sprDetails.ActiveSheet.Cells(i, LME040G.sprDetailsDef.ZAIRECNO.ColNo))
                    For j As Integer = i + 1 To sprmax
                        If (zaiRecNo).Equals(Me._LMEconV.GetCellValue(.sprDetails.ActiveSheet.Cells(j, LME040G.sprDetailsDef.ZAIRECNO.ColNo))) = True Then
                            '2016.01.06 UMANO 英語化対応START
                            'MyBase.ShowMessage("E488", New String() {String.Concat(i + 1, "行目と", j + 1, "行目")})
                            MyBase.ShowMessage("E488", New String() {Convert.ToString(i + 1), Convert.ToString(j + 1)})
                            '2016.01.06 UMANO 英語化対応END
                            Return False
                        End If
                    Next
                Next
            End If

            Dim lgm As New Jp.Co.Nrs.LM.Utility.lmLangMGR(Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage)
            If .cmbJikkou.ReadOnly = False AndAlso LME040C.EventShubetsu.JIKKOU.Equals(eventShubetsu) = True Then
                '実行種別
                .cmbJikkou.ItemName() = lgm.Selector({"実行種別", "Execution type", "실행종별", "中国語"})
                .cmbJikkou.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbJikkou) = False Then
                    Return False
                End If

                '作業指示書番号
                .txtSagyoSijiNo.ItemName() = lgm.Selector({"作業指示書番号", "Work management number", "작업 지시서 번호", "中国語"})
                .txtSagyoSijiNo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoSijiNo) = False Then
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 単項目入力チェック(ワーニング)
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleWarningCheck(ByVal eventShubetsu As LME040C.EventShubetsu) As Boolean

        Dim rtnFlg As Boolean = True

        '【単項目チェック】
        With Me._Frm

            Dim arr As ArrayList = Nothing
            arr = Me.GetCheckList(LME040G.sprDetailsDef.DEF.ColNo, .sprDetails)
            Dim max As Integer = arr.Count - 1
            Dim sprmax As Integer = .sprDetails.ActiveSheet.Rows.Count - 1

            If LME040C.EventShubetsu.ROWDEL.Equals(eventShubetsu) = True Then
                '残個数チェック
                For i As Integer = 0 To max
                    If ("0").Equals(Me._LMEconV.GetCellValue(.sprDetails.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LME040G.sprDetailsDef.PORAZAINBZAI.ColNo))) = True OrElse _
                        String.IsNullOrEmpty(Me._LMEconV.GetCellValue(.sprDetails.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LME040G.sprDetailsDef.PORAZAINBZAI.ColNo))) = True Then
                        '2016.01.06 UMANO 英語化対応START
                        'If MyBase.ShowMessage("W207", New String() {String.Concat(Convert.ToInt32(arr(i)), "行目")}) = MsgBoxResult.Cancel Then
                        If MyBase.ShowMessage("W207", New String() {Convert.ToString(Convert.ToInt32(arr(i)))}) = MsgBoxResult.Cancel Then
                            '2016.01.06 UMANO 英語化対応END
                            Return False
                        End If
                    End If
                Next
            End If

        End With

        Return True

    End Function


    ''' <summary>
    ''' 単項目入力チェック（保存時）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheckHozon(ByVal eventShubetsu As LME040C.EventShubetsu, ByVal ds As DataSet) As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        Dim rtnFlg As Boolean = True

        '【単項目チェック】
        With Me._Frm

            Dim errFlg As Boolean = False
            Dim dtmax As Integer = 0
            Dim sprmax As Integer = .sprDetails.ActiveSheet.Rows.Count - 1

            Dim dr() As DataRow = Nothing
            Dim chkDr() As DataRow = Nothing
            Dim keyNo As String = String.Empty

            errFlg = False

            If LME040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '作業未入力チェック
                dr = ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Select("UPD_FLG = '0' OR UPD_FLG = '1'", "KEY_NO")
                dtmax = dr.Length - 1

                For i As Integer = 0 To dtmax
                    If (keyNo).Equals(dr(i).Item("KEY_NO").ToString) = False Then
                        'KEY_NO変わった時のみ行う
                        keyNo = dr(i).Item("KEY_NO").ToString
                        chkDr = ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Select(String.Concat("KEY_NO = '", keyNo, "' AND ", _
                                                                                             "(UPD_FLG = '0' OR UPD_FLG = '1') AND ", _
                                                                                             "SAGYO_CD <> '", String.Empty, "'"))
                        If chkDr.Length = 0 Then
                            'エラーの場合
                            errFlg = True
                            Me._LMEconV.SetErrorControl(.txtSagyo1)
                            '2016.01.06 UMANO 英語化対応START
                            'MyBase.ShowMessage("E296", New String() {"作業情報"})
                            MyBase.ShowMessage("E853")
                            '2016.01.06 UMANO 英語化対応END
                            Exit For
                        End If
                    End If
                Next
            End If

            If errFlg = True Then
                'エラー該当行の詳細を表示する
                For i As Integer = 0 To sprmax
                    If (keyNo).Equals(Me._LMEconV.GetCellValue(.sprDetails.ActiveSheet.Cells(i, LME040G.sprDetailsDef.KEYNO.ColNo))) = True Then
                        '作業情報・在庫情報のクリア
                        Call Me._G.ClearMeisaiData()
                        'エラー該当行を表示
                        .sprDetails.ActiveSheet.ActiveRowIndex = i
                        '作業情報・在庫情報の表示
                        Call Me._G.SetMeisaiData(ds)
                        Return False
                    End If
                Next
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LME040C.EventShubetsu) As Boolean

        '【関連項目チェック】
        With Me._Frm

            If LME040C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True _
                OrElse LME040C.EventShubetsu.DELETE.Equals(eventShubetsu) = True Then
                If "01".Equals(.cmbSagyoSijiStatus.SelectedValue) Then
                    MyBase.ShowMessage("E00D")
                    Return False
                End If
            End If

            '実行押下時チェック
            If LME040C.EventShubetsu.JIKKOU.Equals(eventShubetsu) = True Then

                Select Case .cmbJikkou.SelectedValue.ToString
                    Case "01"   '文書管理
                    Case "02"   '現場作業指示取消
                        If Not "01".Equals(.cmbWHSagyoSintyoku.SelectedValue) AndAlso _
                            Not "02".Equals(.cmbWHSagyoSintyoku.SelectedValue) Then
                            MyBase.ShowMessage("E01A")
                            Return False
                        End If

                    Case "03"   '現場作業指示




                End Select

            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSagyoSkyuStatusCheck(ByVal ds As DataSet) As Boolean

        '【関連項目チェック】
        With Me._Frm

            Dim dt As DataTable = ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO)
            For Each dr As DataRow In dt.Rows
                If "01".Equals(dr.Item("SKYU_CHK").ToString) Then
                    MyBase.ShowMessage("E127")
                    Return False
                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LME040C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LME040C.EventShubetsu.HENSHU       '編集
                '10:閲覧者、50:外部の場合エラー
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

            Case LME040C.EventShubetsu.FUKUSHA      '複写
                '10:閲覧者、50:外部の場合エラー
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

            Case LME040C.EventShubetsu.CLOSE        '閉じる
                'すべての権限許可
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
                        kengenFlg = True
                End Select

            Case LME040C.EventShubetsu.DELETE       '削除
                '10:閲覧者、50:外部の場合エラー
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


            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        Return kengenFlg

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCheckList(ByVal defNo As Integer, ByVal sprDetail As Spread.LMSpread) As ArrayList

        Return Me._LMEconV.SprSelectList2(defNo, sprDetail)

    End Function

    ''' <summary>
    ''' 範囲チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsHaniCheck(ByVal value As Decimal, ByVal minData As Decimal, ByVal maxData As Decimal) As Boolean

        If value < minData OrElse _
            maxData < value Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 項目のTrim処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceTextValue()

        With Me._Frm
            '各項目のTrim処理
            .txtRemark1.TextValue = Trim(.txtRemark1.TextValue)
            .txtRemark2.TextValue = Trim(.txtRemark2.TextValue)
            .txtRemark3.TextValue = Trim(.txtRemark3.TextValue)

            .txtSagyo1.TextValue = Trim(.txtSagyo1.TextValue)
            .txtSagyo2.TextValue = Trim(.txtSagyo2.TextValue)
            .txtSagyo3.TextValue = Trim(.txtSagyo3.TextValue)
            .txtSagyo4.TextValue = Trim(.txtSagyo4.TextValue)
            .txtSagyo5.TextValue = Trim(.txtSagyo5.TextValue)

        End With

    End Sub

    ''' <summary>
    ''' 作業項目マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSagyoExistChk(ByVal sagyoCd As String, ByVal custCd As String) As Boolean

        With Me._Frm

            '未入力の場合はTrueを戻す
            If String.IsNullOrEmpty(sagyoCd) = True Then
                Return True
            End If

            '作業コードの存在チェック
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("SAGYO_CD = '", sagyoCd, "' AND ", _
                                                                                                            "(CUST_CD_L = '", custCd, "' OR CUST_CD_L = 'ZZZZZ')"))
            If drs.Length < 1 Then
                Return False
            End If

            Return True

        End With

    End Function

#End Region 'Method

End Class
