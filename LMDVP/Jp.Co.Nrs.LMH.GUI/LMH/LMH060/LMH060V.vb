' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH060  : EDI出荷データ荷主コード設定
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMH060Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMH060V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH060F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMHControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH060F, ByVal v As LMHControlV, ByVal g As LMH060G)

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
    ''' 単項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMH060C.EventShubetsu) As Boolean

        Dim custDr() As DataRow = Nothing

        '【単項目チェック】
        With Me._Frm

            If  LMH060C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
                '荷主コード(大)
                .txtCustCdL.ItemName() = "荷主コード(大)"
                .txtCustCdL.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If
            End If

            If LMH060C.EventShubetsu.NINUSHISET.Equals(eventShubetsu) = True Then
                '荷主コード(大)
                .txtCustCdL.ItemName() = "荷主コード(大)"
                .txtCustCdL.IsHissuCheck() = True
                .txtCustCdL.IsForbiddenWordsCheck() = True
                .txtCustCdL.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If
            End If

            If LMH060C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
                '荷主コード(中)
                .txtCustCdM.ItemName() = "荷主コード(中)"
                .txtCustCdM.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If
            End If

            If LMH060C.EventShubetsu.NINUSHISET.Equals(eventShubetsu) = True Then
                '荷主コード(中)
                .txtCustCdM.ItemName() = "荷主コード(中)"
                .txtCustCdM.IsHissuCheck() = True
                .txtCustCdM.IsForbiddenWordsCheck() = True
                .txtCustCdM.IsFullByteCheck() = 2
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If
            End If

            If LMH060C.EventShubetsu.NINUSHISET.Equals(eventShubetsu) = True Then
                '荷主マスタ存在チェック
                custDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                 "CUST_CD_M = '", .txtCustCdM.TextValue, "'"))
                If custDr.Length = 0 Then
                    MyBase.ShowMessage("E079", New String() {"荷主マスタ", String.Concat(.txtCustCdL.TextValue, "-", .txtCustCdM.TextValue)})
                    .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.txtCustCdL)
                    Return False
                End If
            End If



            '******************** スプレッド項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetails)

            If LMH060C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '荷主コード
                vCell.SetValidateCell(0, LMH060G.sprDetailsDef.CUSTCD.ColNo)
                vCell.ItemName() = "荷主コード"
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 7
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMH060C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '荷主名
                vCell.SetValidateCell(0, LMH060G.sprDetailsDef.CUSTNM.ColNo)
                vCell.ItemName() = "荷主名"
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 60
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMH060C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                'EDI番号
                vCell.SetValidateCell(0, LMH060G.sprDetailsDef.EDICTLNO.ColNo)
                vCell.ItemName() = "EDI番号"
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 9
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMH060C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '届先コード
                vCell.SetValidateCell(0, LMH060G.sprDetailsDef.DESTCD.ColNo)
                vCell.ItemName() = "届先コード"
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 15
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMH060C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '届先名
                vCell.SetValidateCell(0, LMH060G.sprDetailsDef.DESTNM.ColNo)
                vCell.ItemName() = "届先名"
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 60
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMH060C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '在庫部課コード
                vCell.SetValidateCell(0, LMH060G.sprDetailsDef.ZBUKACD.ColNo)
                vCell.ItemName() = "在庫部課コード"
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 7
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If


            'スプレッドのチェック
            Dim arr As ArrayList = Nothing
            arr = Me.GetCheckList(LMH060G.sprDetailsDef.DEF.ColNo, .sprDetails)
            Dim max As Integer = arr.Count - 1
            Dim sprmax As Integer = .sprDetails.ActiveSheet.Rows.Count - 1
            Dim intRow As Integer = 0

            If LMH060C.EventShubetsu.NINUSHISET.Equals(eventShubetsu) = True OrElse _
                LMH060C.EventShubetsu.CANCEL.Equals(eventShubetsu) = True OrElse _
                LMH060C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '未選択チェック
                If max < 0 Then
                    MyBase.ShowMessage("E009")
                    Return False
                End If
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
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMH060C.EventShubetsu) As Boolean

        Dim ediCustDr() As DataRow = Nothing
        Dim custDetailDr() As DataRow = Nothing
        Dim setNaiyoNinushiSetCheck As String = String.Empty
        Dim setNaiyoHozonCheck As String = String.Empty
        Dim spdCustCdL As String = String.Empty
        Dim spdCustCdM As String = String.Empty

        '【関連項目チェック】
        With Me._Frm

            If LMH060C.EventShubetsu.NINUSHISET.Equals(eventShubetsu) = True Then
                'EDI対象荷主マスタ存在チェック
                ediCustDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EDI_CUST).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                                        "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                        "CUST_CD_M = '", .txtCustCdM.TextValue, "'"))
                If ediCustDr.Length = 0 Then
                    MyBase.ShowMessage("E209", New String() {String.Concat("荷主コード=", .txtCustCdL.TextValue, "-", .txtCustCdM.TextValue)})
                    .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.txtCustCdL)
                    Return False
                End If

                If ("1").Equals(ediCustDr(0).Item("NINUSHI_SET_FLG").ToString) = False Then
                    MyBase.ShowMessage("E209", New String() {String.Concat("荷主コード=", .txtCustCdL.TextValue, "-", .txtCustCdM.TextValue)})
                    .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.txtCustCdL)
                    Return False
                End If
            End If

            If LMH060C.EventShubetsu.NINUSHISET.Equals(eventShubetsu) = True Then
                '荷主明細マスタ存在チェック
                '要望番号:1253 terakawa 2012.07.13 Start
                'custDetailDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("SUB_KB = '32' AND ", _
                '                                                                                               "CUST_CD = '", String.Concat(.txtCustCdL.TextValue, .txtCustCdM.TextValue), "'"))
                custDetailDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                                               "SUB_KB = '32' AND ", _
                                                                                                               "CUST_CD = '", String.Concat(.txtCustCdL.TextValue, .txtCustCdM.TextValue), "'"))
                '要望番号:1253 terakawa 2012.07.13 End
                If custDetailDr.Length = 0 Then
                    MyBase.ShowMessage("E079", New String() {"荷主明細マスタ", String.Concat(.txtCustCdL.TextValue, "-", .txtCustCdM.TextValue)})
                    .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.txtCustCdL)
                    Return False
                Else
                    setNaiyoNinushiSetCheck = String.Concat(custDetailDr(0).Item("SET_NAIYO").ToString, "00")
                End If
            End If


            'スプレッドのチェック
            Dim arr As ArrayList = Nothing
            arr = Me.GetCheckList(LMH060G.sprDetailsDef.DEF.ColNo, .sprDetails)
            Dim max As Integer = arr.Count - 1
            Dim sprmax As Integer = .sprDetails.ActiveSheet.Rows.Count - 1
            Dim intRow As Integer = 0

            For i As Integer = 0 To max
                intRow = Convert.ToInt32(arr(i).ToString)

                If LMH060C.EventShubetsu.NINUSHISET.Equals(eventShubetsu) = True Then
                    '対象荷主チェック
                    If (setNaiyoNinushiSetCheck).Equals(Me._Vcon.GetCellValue(.sprDetails.ActiveSheet.Cells(intRow, LMH060G.sprDetailsDef.CUSTCDUPD.ColNo))) = False Then
                        MyBase.ShowMessage("E490", New String() {"荷主セット処理", String.Concat(Convert.ToString(intRow), "行目")})
                        Return False
                    End If
                End If

                '一覧の荷主コードを設定
                spdCustCdL = Mid(Me._Vcon.GetCellValue(.sprDetails.ActiveSheet.Cells(intRow, LMH060G.sprDetailsDef.CUSTCD.ColNo)), 1, 5)
                spdCustCdM = Mid(Me._Vcon.GetCellValue(.sprDetails.ActiveSheet.Cells(intRow, LMH060G.sprDetailsDef.CUSTCD.ColNo)), 7, 2)

                If LMH060C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                    'EDI対象荷主マスタ存在チェック
                    ediCustDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EDI_CUST).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                                            "CUST_CD_L = '", spdCustCdL, "' AND ", _
                                                                                                            "CUST_CD_M = '", spdCustCdM, "'"))
                    If ediCustDr.Length = 0 Then
                        MyBase.ShowMessage("E209", New String() {String.Concat(Convert.ToString(intRow), "行目　", "荷主コード=", spdCustCdL, "-", spdCustCdM)})
                        Return False
                    End If

                    If ("1").Equals(ediCustDr(0).Item("NINUSHI_SET_FLG").ToString) = False Then
                        MyBase.ShowMessage("E209", New String() {String.Concat(Convert.ToString(intRow), "行目　", "荷主コード=", spdCustCdL, "-", spdCustCdM)})
                        Return False
                    End If
                End If

                If LMH060C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                    '荷主明細マスタ存在チェック
                    '要望番号:1253 terakawa 2012.07.13 Start
                    'custDetailDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("SUB_KB = '32' AND ", _
                    '                                                                                               "CUST_CD = '", String.Concat(spdCustCdL, spdCustCdM), "'"))
                    custDetailDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                                                   "SUB_KB = '32' AND ", _
                                                                                                                   "CUST_CD = '", String.Concat(spdCustCdL, spdCustCdM), "'"))
                    '要望番号:1253 terakawa 2012.07.13 End
                    If custDetailDr.Length = 0 Then
                        MyBase.ShowMessage("E492", New String() {"荷主明細マスタ", String.Concat(spdCustCdL, "-", spdCustCdM), String.Concat(Convert.ToString(intRow), "行目")})
                        Return False
                    Else
                        setNaiyoHozonCheck = String.Concat(custDetailDr(0).Item("SET_NAIYO").ToString, "00")
                    End If
                End If

                If LMH060C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                    '対象荷主チェック
                    If (setNaiyoHozonCheck).Equals(Me._Vcon.GetCellValue(.sprDetails.ActiveSheet.Cells(intRow, LMH060G.sprDetailsDef.CUSTCDUPD.ColNo))) = False Then
                        MyBase.ShowMessage("E490", New String() {"荷主セット処理", String.Concat(Convert.ToString(intRow), "行目")})
                        Return False
                    End If
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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMH060C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMH060C.EventShubetsu.NINUSHISET   '荷主セット
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

            Case LMH060C.EventShubetsu.CANCEL       'キャンセル
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

            Case LMH060C.EventShubetsu.KENSAKU      '検索
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

            Case LMH060C.EventShubetsu.MASTER       'マスタ参照
                '10:閲覧者の場合エラー
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

            Case LMH060C.EventShubetsu.HOZON        '登録
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

            Case LMH060C.EventShubetsu.CLOSE        '閉じる
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
    Friend Function GetCheckList(ByVal defNo As Integer, ByVal sprDetail As Spread.LMSpreadSearch) As ArrayList

        Return Me._Vcon.SprSelectList(defNo, sprDetail)

    End Function

#End Region 'Method

End Class
