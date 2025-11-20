' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI560V : TSMC請求データ計算
'  作  成  者       :  [HORI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports FarPoint.Win.Spread

''' <summary>
''' LMI560Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMI560V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI560F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMIControlV

    ''' <summary>
    ''' チェックリストを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI560F)

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
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal EVENTSHUBETSU As LMI560C.EventShubetsu) As Boolean

        '権限の設定
        Dim _Kengen As String = LMUserInfoManager.GetAuthoLv

        Select Case _Kengen
            Case LMConst.AuthoKBN.AGENT                '外部
                Select Case EVENTSHUBETSU
                    Case LMI560C.EventShubetsu.TOJIRU  '閉じるイベント
                        Return True
                    Case Else
                        MyBase.ShowMessage("E016")     '閉じる以外のイベント
                        Return False
                End Select
            Case LMConst.AuthoKBN.VIEW               '閲覧者

                Select Case EVENTSHUBETSU
                    '実行、前回計算取消イベント
                    Case LMI560C.EventShubetsu.JIKKOU, LMI560C.EventShubetsu.ZENKEISANTORI
                        MyBase.ShowMessage("E016")
                        Return False
                    Case Else                           '実行、前回計算取消以外のイベント
                        Return True
                End Select
        End Select

        Return True

    End Function

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck(ByVal SHUBETSU As LMI560C.EventShubetsu) As Boolean

        With Me._Frm

            Select Case SHUBETSU
                Case LMI560C.EventShubetsu.KENSAKU
                    '検索

                    '営業所
                    .cmbBr.ItemName = "営業所"
                    .cmbBr.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbBr) = False Then
                        Return False
                    End If

                    '請求先コード
                    .txtSeqtoCd.ItemName = "請求先コード"
                    .txtSeqtoCd.IsForbiddenWordsCheck = True
                    .txtSeqtoCd.IsByteCheck = 7
                    If MyBase.IsValidateCheck(.txtSeqtoCd) = False Then
                        Return False
                    End If

                    '請求月
                    .imdInvDate.ItemName = "請求月"
                    .imdInvDate.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.imdInvDate) = False Then
                        Return False
                    End If

                    '【スプレッド項目単項目チェック】
                    Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

                    '請求先名
                    vCell.SetValidateCell(0, LMI560G.sprDetailDef.SEIQTO_NM.ColNo)
                    vCell.ItemName = "請求先名"
                    vCell.IsForbiddenWordsCheck = True
                    vCell.IsByteCheck = 240
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                Case LMI560C.EventShubetsu.JIKKOU
                    '実行

                Case LMI560C.EventShubetsu.MASTER
                    'マスタ

                    '営業所
                    .cmbBr.ItemName = "営業所"
                    .cmbBr.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbBr) = False Then
                        Return False
                    End If
            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <param name="SHUBETSU"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function isRelationCheck(ByVal SHUBETSU As LMI560C.EventShubetsu) As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Call Me.getCheckList()

        '未選択チェック
        If Me.IsSelectChk(Me._ChkList.Count()) = False Then
            Me.ShowMessage("E009")
            Return False
        End If

        '複数選択チェック
        If LMI560C.EventShubetsu.ZENKEISANTORI.Equals(SHUBETSU) Then
            '前回計算取消処理のみ
            If Me.IsSelectOneChk(Me._ChkList.Count()) = False Then
                Me.ShowMessage("E008")
                Return False
            End If
        End If

        'チェックが付いている行が最新かチェック
        With ""
            Dim spr As SheetView = Me._Frm.sprDetail.ActiveSheet
            For i As Integer = 1 To spr.RowCount - 1
                'チェックが付いている行のみ
                If Me.GetCellValue(spr.Cells(i, LMI560G.sprDetailDef.DEF.ColNo)).Equals(LMConst.FLG.ON) = False Then
                    Continue For
                End If

                '請求先コード
                Dim seiqtoCd As String = spr.Cells(i, LMI560G.sprDetailDef.SEIQTO_CD.ColNo).Value.ToString()
                '最終請求日
                Dim lastDate As String = spr.Cells(i, LMI560G.sprDetailDef.LAST_DATE_ORG.ColNo).Value.ToString()

                '請求先コードを主請求先としている荷主、最終請求日が画面と一致するデータを検索
                Dim drCust As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("OYA_SEIQTO_CD = '", seiqtoCd, "' AND HOKAN_NIYAKU_CALCULATION = '", lastDate, "'"))
                If drCust.Length = 0 Then
                    Me.ShowMessage("E032")
                    Return False
                End If
            Next
        End With

        '実行時のみ
        If LMI560C.EventShubetsu.JIKKOU.Equals(SHUBETSU) Then
            Dim spr As SheetView = Me._Frm.sprDetail.ActiveSheet
            For i As Integer = 1 To spr.RowCount - 1
                'チェックが付いている行のみ
                If Me.GetCellValue(spr.Cells(i, LMI560G.sprDetailDef.DEF.ColNo)).Equals(LMConst.FLG.ON) = False Then
                    Continue For
                End If

                '請求先コード
                Dim seiqtoCd As String = spr.Cells(i, LMI560G.sprDetailDef.SEIQTO_CD.ColNo).Value.ToString()
                '締日
                Dim closekb As String = spr.Cells(i, LMI560G.sprDetailDef.CLOSE_KB.ColNo).Value.ToString()
                '前回請求日(今回請求日より算出)
                Dim invDate As String = String.Concat(Left(Me._Frm.imdInvDate.TextValue, 6), "01")
                invDate = Me.GetSimeDate(Me.GetAddDate(invDate, "m", -1), closekb)

                '請求先コードを主請求先としている荷主を検索
                Dim drCust As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("OYA_SEIQTO_CD = '", seiqtoCd, "'"))

                '保管荷役最終計算日が前回請求日より前ならばエラー
                For Each dr As DataRow In drCust
                    If dr.Item("HOKAN_NIYAKU_CALCULATION").ToString < invDate Then
                        Dim custCd As String = String.Concat(
                        dr.Item("CUST_CD_L").ToString,
                        "-",
                        dr.Item("CUST_CD_M").ToString,
                        "-",
                        dr.Item("CUST_CD_S").ToString)
                        Me.ShowMessage("E335", New String() {custCd})
                        Return False
                    End If
                Next
            Next
        End If

        Return True

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMI560C.SprColumnIndex.DEF

        '選択された行の行番号を取得
        Me._ChkList = Me.SprSelectList(defNo, Me._Frm.sprDetail)

        Return Me._ChkList

    End Function

#End Region 'Method

#Region "部品化"

    ''' <summary>
    ''' 選択行有無判別
    ''' </summary>
    ''' <param name="rowCnt">選択行数（省略可）</param>
    ''' <returns>-1：選択行無し　0：単一行選択中　1：複数行選択中</returns>
    ''' <remarks></remarks>
    Friend Function FindSelectedRow(Optional ByRef rowCnt As Integer = 0) As Integer

        With Me._Frm.sprDetail.Sheets(0)

            Dim rowIdx As Integer = -1

            For i As Integer = 1 To .RowCount - 1
                If .Cells(i, LMI560G.sprDetailDef.DEF.ColNo).Value.ToString = True.ToString Then

                    rowCnt = rowCnt + 1

                    If rowIdx = 0 Then
                        rowIdx = 1
                    End If
                    If rowIdx <> 1 Then
                        rowIdx = 0
                    End If
                End If
            Next

            Return rowIdx

        End With

    End Function

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得（LMSpreadSearch)
    ''' </summary>
    ''' <param name="defNo">チェックボックスセルのカラム№</param>
    ''' <param name="sprDetail">対象スプレッド</param>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Function SprSelectList(ByVal defNo As Integer, ByRef sprDetail As Spread.LMSpreadSearch) As ArrayList

        With sprDetail.ActiveSheet

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

        ElseIf TypeOf aCell.Editor Is CellType.DateTimeCellType Then

            '日付の場合、Value値を yyyyMMdd に変換して返却
            If aCell.Value Is Nothing = False AndAlso String.IsNullOrEmpty(aCell.Value.ToString()) = False Then
                GetCellValue = Convert.ToDateTime(aCell.Value).ToString("yyyyMMdd")
            End If

        Else
            'テキストの場合、Trimした値を返却
            GetCellValue = aCell.Text.Trim()

        End If

        Return GetCellValue

    End Function

    ''' <summary>
    ''' 未選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectChk(ByVal chkCnt As Integer) As Boolean

        'チェック件数が0件
        If chkCnt = 0 Then

            Return Me.SetErrMessage("E009")

        End If

        Return True

    End Function

    ''' <summary>
    ''' 単一行選択チェック
    ''' </summary>
    ''' <param name="chkCnt">チェック行カウント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectOneChk(ByVal chkCnt As Integer) As Boolean

        If 1 < chkCnt Then
            Return Me.SetErrMessage("E008")
        End If

        Return True

    End Function

    ''' <summary>
    ''' メッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetErrMessage(ByVal id As String) As Boolean

        MyBase.ShowMessage(id)
        Return False

    End Function

    ''' <summary>
    ''' 締日を考慮した日付を返す
    ''' </summary>
    ''' <param name="baseDate">基準日(yyyyMMdd / yyyyMM)</param>
    ''' <param name="closeKb">締日区分(区分マスタ S008 準拠)</param>
    ''' <returns></returns>
    Friend Function GetSimeDate(ByVal baseDate As String, ByVal closeKb As String) As String

        If Not "00".Equals(closeKb) Then
            '締日区分が末日以外ならそのまま結合して完了
            Return String.Concat(Left(baseDate, 6), closeKb)
        End If

        '基準月の月末日を求める
        Dim dt As DateTime = DateTime.ParseExact(String.Concat(Left(baseDate, 6), "01"), "yyyyMMdd", Nothing)
        Return dt.AddMonths(1).AddDays(-1).ToString("yyyyMMdd")

    End Function

    ''' <summary>
    ''' 日数に指定数(年,月,日)を加減算して返す
    ''' </summary>
    ''' <param name="baseDate">基準日(yyyyMMdd)</param>
    ''' <param name="where">加減算する場所(y, m, d)</param>
    ''' <param name="value">加減算する値(+, -)</param>
    ''' <returns></returns>
    Friend Function GetAddDate(ByVal baseDate As String, ByVal where As String, ByVal value As Integer) As String

        Dim dt As DateTime = DateTime.ParseExact(baseDate, "yyyyMMdd", Nothing)

        Select Case where.ToLower
            Case "y"
                Return dt.AddYears(value).ToString("yyyyMMdd")
            Case "m"
                Return dt.AddMonths(value).ToString("yyyyMMdd")
            Case "d"
                Return dt.AddDays(value).ToString("yyyyMMdd")
        End Select

        Return baseDate

    End Function

#End Region

End Class
