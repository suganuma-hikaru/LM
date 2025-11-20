' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : ＳＣＭ
'  プログラムID     :  LMN010V : 出荷データ一覧
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports FarPoint.Win.Spread

''' <summary>
''' LMN010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMN010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMN010F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMN010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

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
    Friend Function IsInputCheck(Optional ByVal mode As String = LMN010C.MODE_DEFAULT) As Boolean

        With Me._Frm

            If mode = LMN010C.MODE_DEFAULT Then

                '【単項目チェック】

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' ユーザーマスタの存在チェック
    ''' </summary>
    ''' <param name="text">チェック対象文字列</param>
    ''' <returns>ユーザー名</returns>
    ''' <remarks></remarks>
    Private Function IsExistUserNm(ByVal text As String) As String

        ''存在チェック
        Dim userNm As String = String.Empty
        'Dim strSqlUser As String = String.Empty
        'strSqlUser = "USER_ID = '" & text & "'"
        'strSqlUser = strSqlUser & "AND SYS_DEL_FLG = '0'"
        'Dim userRows As DataRow() = Me.GetCachedMasterDataSet().Tables(LMConst.CacheTBL.USER).Select(strSqlUser)
        'If userRows.LenLMh = 0 Then
        '    '存在エラー時
        '    Me.ShowMessage("E024")    'エラーメッセージ
        '    IsExistUserNm = ""
        'Else
        '    '正常時               
        '    userNm = userRows(0).Item("USER_NM").ToString
        'End If

        IsExistUserNm = userNm

    End Function

    ''' <summary>
    ''' スプレッドの項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadInputChk() As Boolean

        With Me._Frm

            'Dim vCell As LMValidatableCells = New LMValidatableCells(.sprCurrencySearch)

            ''銀行名
            'vCell.SetValidateCell(0, LMN010G.sprCurrencySearchDef.BANKNM.ColNo)
            'vCell.ItemName = "銀行名"
            'vCell.IsHissuCheck = True
            'If Me.IsValidateCheck(vCell) = False Then Return False

        End With

        Return True

    End Function

    ''' <summary>
    ''' 選択行有無判別
    ''' </summary>
    ''' <param name="rowCnt">選択行数（省略可）</param>
    ''' <returns>-1：選択行無し　0：単一行選択中　1：複数行選択中</returns>
    ''' <remarks></remarks>
    Friend Function FindSelectedRow(Optional ByRef rowCnt As Integer = 0) As Integer

        'With Me._Frm.sprCurrencySearch.Sheets(0)

        '    Dim rowIdx As Integer = -1

        '    For i As Integer = 1 To .RowCount - 1
        '        If .Cells(i, LMN010G.sprCurrencySearchDef.sDEF.ColNo).Value.ToString = True.ToString Then

        '            rowCnt = rowCnt + 1

        '            If rowIdx = 0 Then
        '                rowIdx = 1
        '            End If
        '            If rowIdx <> 1 Then
        '                rowIdx = 0
        '            End If
        '        End If
        '    Next

        '    Return rowIdx

        'End With

    End Function

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Function SprSelectCount() As ArrayList

        Dim defNo As Integer = LMN010G.sprDetailDef.DEF.ColNo

        With Me._Frm.sprDetail.ActiveSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If Me.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

            Return list

        End With

    End Function

    ''' <summary>
    ''' セルから値を取得
    ''' </summary>
    ''' <param name="aCell">セル</param>
    ''' <returns>取得した値</returns>
    ''' <remarks></remarks>
    Friend Function GetCellValue(ByVal aCell As Cell) As String

        GetCellValue = String.Empty

        If TypeOf aCell.Editor Is CellType.ComboBoxCellType Then

            'コンボボックスの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.CheckBoxCellType Then

            'チェックボックスの場合、Booleanの値をStringに変換
            If aCell.Text.Equals("True") = True Then
                GetCellValue = LMConst.FLG.ON
            ElseIf aCell.Text.Equals("False") = True Then
                GetCellValue = LMConst.FLG.OFF
            Else
                GetCellValue = aCell.Text
            End If

        ElseIf TypeOf aCell.Editor Is CellType.NumberCellType Then

            'ナンバーの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            Else
                GetCellValue = 0.ToString()
            End If

        Else

            'テキストの場合、Trimした値を返却
            GetCellValue = aCell.Text.Trim()

        End If

        Return GetCellValue

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMN010C.EventShubetsu) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv

        Select Case eventShubetsu

            '検索
            Case LMN010C.EventShubetsu.KENSAKU
                If kengen.Equals(LMConst.AuthoKBN.AGENT) = True Then  '50:外部の場合エラー
                    MyBase.ShowMessage("E016")
                    Return False
                End If

                '設定
            Case LMN010C.EventShubetsu.SETTEI
                If kengen.Equals(LMConst.AuthoKBN.AGENT) = True Or kengen.Equals(LMConst.AuthoKBN.VIEW) Then '50:外部または10:閲覧の場合エラー
                    MyBase.ShowMessage("E016")
                    Return False
                End If

                '実績取込
            Case LMN010C.EventShubetsu.JISSEKI_TORIKOMI
                If kengen.Equals(LMConst.AuthoKBN.AGENT) = True Or kengen.Equals(LMConst.AuthoKBN.VIEW) Then '50:外部または10:閲覧の場合エラー
                    MyBase.ShowMessage("E016")
                    Return False
                End If

                '実績送信
            Case LMN010C.EventShubetsu.JISSEKI_SOUSHIN
                If kengen.Equals(LMConst.AuthoKBN.AGENT) = True Or kengen.Equals(LMConst.AuthoKBN.VIEW) Then '50:外部または10:閲覧の場合エラー
                    MyBase.ShowMessage("E016")
                    Return False
                End If

                '送信指示
            Case LMN010C.EventShubetsu.SOUSHIN_SHIJI
                If kengen.Equals(LMConst.AuthoKBN.AGENT) = True Or kengen.Equals(LMConst.AuthoKBN.VIEW) Then '50:外部または10:閲覧の場合エラー
                    MyBase.ShowMessage("E016")
                    Return False
                End If

                'Spreadダブルクリック
            Case LMN010C.EventShubetsu.SPREAD_DOUBLE_CLICK
                If kengen.Equals(LMConst.AuthoKBN.AGENT) Then '50:外部の場合エラー
                    MyBase.ShowMessage("E016")
                    Return False
                End If

                '欠品照会
            Case LMN010C.EventShubetsu.KEPPIN_SHOUKAI
                If kengen.Equals(LMConst.AuthoKBN.AGENT) Then '50:外部の場合エラー
                    MyBase.ShowMessage("E016")
                    Return False
                End If

                '欠品状態更新
            Case LMN010C.EventShubetsu.KEPPIN_JOUTAI_UPD
                If kengen.Equals(LMConst.AuthoKBN.AGENT) = True Or kengen.Equals(LMConst.AuthoKBN.VIEW) Then '50:外部または10:閲覧の場合エラー
                    MyBase.ShowMessage("E016")
                    Return False
                End If

        End Select

        Return True

    End Function

#Region "検索"

    ''' <summary>
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean

        '単項目チェック
        If Me.IsKensakuSingleCheck() = False Then
            Return False
        End If

        '関連チェック
        If Me.IsKensakuRelationCheck() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleCheck() As Boolean

        With Me._Frm

            '******************** ヘッダ項目の入力チェック ********************
            '荷主
            .cmbCustCd.ItemName = "荷主"
            .cmbCustCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbCustCd) = False Then
                Return False
            End If

            'EDI取込日FROM
            .imdEDITorikomiDate_From.ItemName = "EDI取込日FROM"
            If .imdEDITorikomiDate_From.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"EDI取込日FROM", "8"})
                Return False
            End If
            If MyBase.IsValidateCheck(.imdEDITorikomiDate_From) = False Then
                Return False
            End If

            'EDI取込日TO
            .imdEDITorikomiDate_To.ItemName = "EDI取込日TO"
            If .imdEDITorikomiDate_To.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"EDI取込日TO", "8"})
                Return False
            End If
            If MyBase.IsValidateCheck(.imdEDITorikomiDate_To) = False Then
                Return False
            End If

            '出荷日FROM
            .imdShukkaDate_From.ItemName = "出荷日FROM"
            If .imdShukkaDate_From.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"出荷日FROM", "8"})
                Return False
            End If
            If MyBase.IsValidateCheck(.imdShukkaDate_From) = False Then
                Return False
            End If

            '出荷日TO
            .imdShukkaDate_To.ItemName = "出荷日TO"
            If .imdShukkaDate_To.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"出荷日TO", "8"})
                Return False
            End If
            If MyBase.IsValidateCheck(.imdShukkaDate_To) = False Then
                Return False
            End If

            '納入日FROM
            .imdNounyuDate_From.ItemName = "納入日FROM"
            If .imdNounyuDate_From.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"納入日FROM", "8"})
                Return False
            End If
            If MyBase.IsValidateCheck(.imdNounyuDate_From) = False Then
                Return False
            End If

            '納入日TO
            .imdNounyuDate_To.ItemName = "納入日TO"
            If .imdNounyuDate_To.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"納入日TO", "8"})
                Return False
            End If
            If MyBase.IsValidateCheck(.imdNounyuDate_To) = False Then
                Return False
            End If

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            'オーダーNO
            vCell.SetValidateCell(0, LMN010G.sprDetailDef.CUST_ORD_NO_L.ColNo)
            vCell.ItemName = "オーダーNO"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 8
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '出荷先名
            vCell.SetValidateCell(0, LMN010G.sprDetailDef.DEST_NM.ColNo)
            vCell.ItemName = "出荷先名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '出荷先住所
            vCell.SetValidateCell(0, LMN010G.sprDetailDef.DEST_AD.ColNo)
            vCell.ItemName = "出荷先住所"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '出荷先郵便番号
            vCell.SetValidateCell(0, LMN010G.sprDetailDef.DEST_ZIP.ColNo)
            vCell.ItemName = "出荷先郵便番号"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 8
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '備考1
            vCell.SetValidateCell(0, LMN010G.sprDetailDef.REMARK_1.ColNo)
            vCell.ItemName = "備考1"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '備考2
            vCell.SetValidateCell(0, LMN010G.sprDetailDef.REMARK_2.ColNo)
            vCell.ItemName = "備考2"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 検索押下時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuRelationCheck() As Boolean

        With Me._Frm

            '******************** ヘッダ項目の関連チェック ********************
            'EDI取込日FROM > EDI取込日TO はエラー
            If (Not String.IsNullOrEmpty(.imdEDITorikomiDate_From.TextValue)) And (Not String.IsNullOrEmpty(.imdEDITorikomiDate_To.TextValue)) Then
                If String.Compare((.imdEDITorikomiDate_From.TextValue), (.imdEDITorikomiDate_To.TextValue)) > 0 Then
                    MyBase.ShowMessage("E039", New String() {"EDI取込日TO", "EDI取込日FROM"})
                    Return False
                End If
            End If

            '出荷日FROM > 出荷日TO はエラー
            If (Not String.IsNullOrEmpty(.imdShukkaDate_From.TextValue)) And (Not String.IsNullOrEmpty(.imdShukkaDate_To.TextValue)) Then
                If String.Compare((.imdShukkaDate_From.TextValue), (.imdShukkaDate_To.TextValue)) > 0 Then
                    MyBase.ShowMessage("E039", New String() {"出荷日TO", "出荷日FROM"})
                    Return False
                End If
            End If

            '入荷日FROM > 入荷日TO はエラー
            If (Not String.IsNullOrEmpty(.imdNounyuDate_From.TextValue)) And (Not String.IsNullOrEmpty(.imdNounyuDate_To.TextValue)) Then
                If String.Compare((.imdNounyuDate_From.TextValue), (.imdNounyuDate_To.TextValue)) > 0 Then
                    MyBase.ShowMessage("E039", New String() {"入荷日TO", "入荷日FROM"})
                    Return False
                End If
            End If

        End With

        Return True

    End Function

#End Region

#Region "設定"

    ''' <summary>
    ''' 設定ボタン押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsSetteiInputChk(ByRef list As ArrayList) As Boolean

        '単項目チェック
        If Me.IsSetteiSingleCheck() = False Then
            Return False
        End If

        '関連チェック
        If Me.IsSetteiRelationCheck(list) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 設定ボタン押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSetteiSingleCheck() As Boolean

        With Me._Frm

            '******************** ヘッダ項目の入力チェック ********************
            '出荷日
            .imdShukkaDate.ItemName = "出荷日"
            If .imdShukkaDate.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"出荷日", "8"})
                Return False
            End If
            If MyBase.IsValidateCheck(.imdShukkaDate) = False Then
                Return False
            End If

            '納入日
            .imdNounyuDate.ItemName = "納入日"
            If .imdNounyuDate.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"納入日", "8"})
                Return False
            End If
            If MyBase.IsValidateCheck(.imdNounyuDate) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 設定ボタン押下時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSetteiRelationCheck(ByVal list As ArrayList) As Boolean

        Dim listCnt As Integer = list.Count

        With Me._Frm

            '******************** ヘッダ項目の関連チェック ********************

            '出荷日 > 納入日 はエラー
            If (.imdShukkaDate.TextValue <> "") And (.imdNounyuDate.TextValue <> "") Then
                If String.Compare((.imdShukkaDate.TextValue), (.imdNounyuDate.TextValue)) > 0 Then
                    MyBase.ShowMessage("E039", New String() {"納入日", "出荷日"})
                    Return False
                End If
            End If

            '倉庫、出荷日、納入日のすべてに入力が無い場合はエラー
            If (.cmbWare.SelectedValue.ToString = "") And (.imdShukkaDate.TextValue = "") And (.imdNounyuDate.TextValue = "") Then
                MyBase.ShowMessage("E019", New String() {"設定項目"})
                Return False
            End If


            '******************** Spread項目の関連チェック ********************
            Dim defNo As Integer

            With .sprDetail.ActiveSheet

                'チェックボックス未選択はエラー
                If listCnt = 0 Then

                    MyBase.ShowMessage("E033")
                    Return False

                End If

                '異なる出荷日のレコードが選択されている場合はエラー
                Dim outkaDate As String = .Cells(Convert.ToInt32(list(0)), LMN010G.sprDetailDef.OUTKA_DATE.ColNo).Text

                If listCnt >= 2 Then

                    For Each defNo In list

                        If outkaDate <> .Cells(defNo, LMN010G.sprDetailDef.OUTKA_DATE.ColNo).Text Then

                            MyBase.ShowMessage("E142")
                            Return False

                        End If

                    Next

                End If

            End With

            'ポンプアップ作業チェック
            If (Not String.IsNullOrEmpty(.imdShukkaDate.TextValue)) And (Not String.IsNullOrEmpty(.imdNounyuDate.TextValue)) Then

                If .imdShukkaDate.TextValue = .imdNounyuDate.TextValue Then

                    For Each defNo In list

                        If .sprDetail.ActiveSheet.Cells(defNo, LMN010G.sprDetailDef.MOUSHIOKURI_KBN.ColNo).Value.ToString = "23" Then

                            MyBase.ShowMessage("E141")
                            Return False

                        End If

                    Next

                End If

            End If

            'ヘッダ部とSpread部の出荷日、納入日関連チェック
            'ヘッダ部に入力がある場合はヘッダ部優先(チェック無し)
            If (String.IsNullOrEmpty(.imdShukkaDate.TextValue)) Or (String.IsNullOrEmpty(.imdNounyuDate.TextValue)) Then
                For Each defNo In list
                    Dim sprOutka As String = .sprDetail.ActiveSheet.Cells(defNo, LMN010G.sprDetailDef.OUTKA_DATE.ColNo).Text
                    Dim sprArr As String = .sprDetail.ActiveSheet.Cells(defNo, LMN010G.sprDetailDef.ARR_DATE.ColNo).Text

                    If (Not String.IsNullOrEmpty(.imdShukkaDate.TextValue)) And (Not String.IsNullOrEmpty(sprArr)) Then
                        If String.Compare(.imdShukkaDate.TextValue, sprArr) > 0 Then
                            MyBase.ShowMessage("E166", New String() {"出荷日", "納入日"})
                            Return False
                        End If
                    End If

                    If (Not String.IsNullOrEmpty(.imdNounyuDate.TextValue)) And (Not String.IsNullOrEmpty(sprOutka)) Then
                        If String.Compare(sprOutka, .imdNounyuDate.TextValue) > 0 Then
                            MyBase.ShowMessage("E039", New String() {"納入日", "出荷日"})
                            Return False
                        End If
                    End If
                Next
            End If


        End With

        Return True

    End Function

#End Region

#Region "送信指示"

    ''' <summary>
    ''' 送信指示押下時入力チェック
    ''' </summary>
    ''' <param name="list"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSoushinShijiInputChk(ByVal list As ArrayList) As Boolean

        '単項目チェック
        If Me.IsSoushinShijiSingleCheck(list) = False Then
            Return False
        End If

        '関連チェック
        If Me.IsSoushinRelationCheck(list) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 送信指示ボタン押下時単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSoushinShijiSingleCheck(ByVal list As ArrayList) As Boolean

        '******************** Spread項目の入力チェック ********************
        Dim defNo As Integer
        For Each defNo In list

            With Me._Frm.sprDetail.ActiveSheet

                '倉庫
                If (String.IsNullOrEmpty(.Cells(defNo, LMN010G.sprDetailDef.SOKO_NM.ColNo).Text)) Then
                    MyBase.ShowMessage("E001", New String() {"倉庫名"})
                    Return False
                End If

                '出荷日
                If (String.IsNullOrEmpty(.Cells(defNo, LMN010G.sprDetailDef.OUTKA_DATE.ColNo).Text)) Then
                    MyBase.ShowMessage("E001", New String() {"出荷日"})
                    Return False
                End If

                '納入日
                If (String.IsNullOrEmpty(.Cells(defNo, LMN010G.sprDetailDef.ARR_DATE.ColNo).Text)) Then
                    MyBase.ShowMessage("E001", New String() {"納入日"})
                    Return False
                End If

            End With

        Next

        Return True

    End Function

    ''' <summary>
    ''' 送信指示ボタン押下時関連チェック
    ''' </summary>
    ''' <param name="list"></param>
    ''' <remarks></remarks>
    Private Function IsSoushinRelationCheck(ByVal list As ArrayList) As Boolean

        With Me._Frm.sprDetail.ActiveSheet

            'ステータスチェック
            Dim defNo As Integer
            For Each defNo In list

                If (.Cells(defNo, LMN010G.sprDetailDef.STATUS_KBN.ColNo).Value).ToString <> "01" Then
                    Me.ShowMessage("E143")
                    Return False
                End If

            Next

            'TODO（後でコメントはずす）
            ''欠品フラグ実施判定
            'If String.IsNullOrEmpty(.Cells(defNo, LMN010G.sprDetailDef.KEPPIN_FLG.ColNo).Text) Then
            '    MyBase.ShowMessage("E145")
            '    Return False
            'End If

        End With

        Return True

    End Function

#End Region

#Region "欠品照会"

    ''' <summary>
    ''' 欠品照会押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKeppinShoukaiInputChk() As Boolean

        '単項目チェック
        If Me.IsKeppinShoukaiSingleCheck() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 欠品照会押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKeppinShoukaiSingleCheck() As Boolean

        With Me._Frm

            '******************** ヘッダ項目の入力チェック ********************
            '荷主
            .cmbCustCd.ItemName = "荷主"
            .cmbCustCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbCustCd) = False Then
                Return False
            End If

        End With

        Return True

    End Function


#End Region

#Region "欠品状態更新"

    ''' <summary>
    ''' 欠品状態更新ボタン押下時入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsUpdKeppinJoutaiInputChk() As Boolean

        '関連チェック
        If Me.IsUpdKeppinJoutaiRelationCheck() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 欠品状態更新ボタン押下時関連チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Function IsUpdKeppinJoutaiRelationCheck() As Boolean

        'Spread表示数取得
        Dim count As Integer = Me._Frm.sprDetail.Sheets(0).RowCount

        'Spread表示行の存在をチェック
        If count > 1 Then
            '一覧に表示されているレコードのステータスをチェック
            For i As Integer = 1 To count - 1
                'ステータスが「設定済」のレコードが一行でも存在したらTRUEを返す
                If Me._Frm.sprDetail.Sheets(0).Cells(i, LMN010G.sprDetailDef.STATUS_KBN.ColNo).Value.ToString() = "01" Then

                    Return True

                End If

            Next

        Else
            'Spread表示行が存在しない場合エラー
            Me.ShowMessage("E024")

            Return False

        End If

        'ステータスが「設定済」のレコードが存在しない場合エラー表示
        Me.ShowMessage("E079", New String() {"一覧", "ステータスが「設定済み」のレコード"})
        Return False

    End Function

#End Region

#Region "実績取込"

    ''' <summary>
    ''' 実績取込押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsJissekiTorikomiInputChk() As Boolean

        '単項目チェック
        If Me.IsJissekiTorikomiSingleCheck() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 実績取込押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsJissekiTorikomiSingleCheck() As Boolean

        With Me._Frm

            '******************** ヘッダ項目の入力チェック ********************
            '荷主
            .cmbCustCd.ItemName = "荷主"
            .cmbCustCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbCustCd) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region

#Region "ダブルクリック"

    ''' <summary>
    ''' ダブルクリック時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsDoubleClickInputChk(ByVal e As FarPoint.Win.Spread.CellClickEventArgs) As Boolean

        '関連チェック
        If Me.DoubleClickRelationCheck(e) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索押下時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DoubleClickRelationCheck(ByVal e As FarPoint.Win.Spread.CellClickEventArgs) As Boolean

        '倉庫コードが未設定の場合エラー
        If String.IsNullOrEmpty(Me._Frm.sprDetail.ActiveSheet.Cells(e.Row, LMN010G.sprDetailDef.SOKO_NM.ColNo).Value.ToString()) Then
            MyBase.ShowMessage("E203")
            Return False
        End If

        Return True

    End Function


#End Region


#End Region 'Method

End Class
