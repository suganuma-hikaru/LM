' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG010V : 保管料/荷役料計算
'  作  成  者       :  []
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports FarPoint.Win.Spread

''' <summary>
''' LMG010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMG010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG010F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMGControlV

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG010F)

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
    Friend Function IsAuthorityChk(ByVal EVENTSHUBETSU As LMG010C.EventShubetsu) As Boolean

        '権限の設定
        Dim _Kengen As String = LMUserInfoManager.GetAuthoLv

        Select Case _Kengen
            Case LMConst.AuthoKBN.AGENT                '外部
                Select Case EVENTSHUBETSU
                    Case LMG010C.EventShubetsu.TOJIRU  '閉じるイベント
                        Return True
                    Case Else
                        MyBase.ShowMessage("E016")     '閉じる以外のイベント
                        Return False
                End Select
            Case LMConst.AuthoKBN.VIEW               '閲覧者

                Select Case EVENTSHUBETSU
                    '実行、前回計算取消イベント
                    Case LMG010C.EventShubetsu.JIKKOU, LMG010C.EventShubetsu.ZENKEISANTORI
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
    Friend Function IsInputCheck(ByVal SHUBETSU As LMG010C.EventShubetsu) As Boolean

        With Me._Frm
            Select Case SHUBETSU
                Case LMG010C.EventShubetsu.KENSAKU         '検索

                    '営業所
                    .cmbBr.ItemName = "営業所"
                    .cmbBr.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbBr) = False Then
                        Return False
                    End If

                    '荷主コード（大）
                    .txtCustCdL.ItemName = "荷主コード（大）"
                    .txtCustCdL.IsForbiddenWordsCheck = True
                    .txtCustCdL.IsByteCheck = 5
                    If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                        Return False
                    End If

                    '荷主コード（中）
                    .txtCustCdM.ItemName = "荷主コード（中）"
                    .txtCustCdM.IsForbiddenWordsCheck = True
                    .txtCustCdM.IsByteCheck = 2
                    If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                        Return False
                    End If

                    '締日
                    .cmbSimebi.ItemName = "締日"
                    .cmbSimebi.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbSimebi) = False Then
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

                    vCell.SetValidateCell(0, LMG010G.sprDetailDef.CUST_NM.ColNo)
                    vCell.ItemName = "荷主名"
                    vCell.IsForbiddenWordsCheck = True
                    vCell.IsByteCheck = 240
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                Case LMG010C.EventShubetsu.JIKKOU          '実行

                    'バッチ条件
                    .cmbBatch.ItemName = "バッチ条件"
                    .cmbBatch.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbBatch) = False Then
                        Return False
                    End If

                Case LMG010C.EventShubetsu.MASTER          'マスタ

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
    Friend Function isRelationCheck(ByVal SHUBETSU As LMG010C.EventShubetsu) As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        '営業所相違チェック用
        Dim NrsBr As String = String.Empty

        '前回請求日判定用
        Dim InvDate As String = Me._Frm.imdInvDate.TextValue

        '前回計算取消用JOB_NO
        Dim JobNo As String = String.Empty

        '締日区分
        Dim Shimebi As String = Me._Frm.cmbSimebi.SelectedValue.ToString()

        '混在チェック用リスト
        Dim KIWARIList As ArrayList = New ArrayList()

        '前回計算有無チェック用リスト
        Dim SeikyukikanTo As String = String.Empty

        'エラー用
        Dim CustNo As String = String.Empty

        'チェック行リスト取得
        Call Me.getCheckList()

        '相違チェック
        With Me._Frm.sprDetail.Sheets(0)

            Dim rowIdx As Integer = -1

            For i As Integer = 1 To .RowCount - 1
                If .Cells(i, LMG010G.sprDetailDef.DEF.ColNo).Value.ToString = True.ToString Then
                    NrsBr = .Cells(i, LMG010G.sprDetailDef.NRS_BR_CD.ColNo).Value.ToString
                    '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
                    ''営業所相違チェック
                    'If NrsBr.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
                    '    Me.ShowMessage("E178", New String() {"データ処理"})
                    '    Return False
                    'End If

                    '期割区分混在チェック判定用リスト
                    KIWARIList.Add(.Cells(i, LMG010G.sprDetailDef.KIWARI_KBN.ColNo).Value.ToString())

                    '請求期間TO
                    SeikyukikanTo = Replace(.Cells(i, LMG010G.sprDetailDef.SEIKIKAN_TO.ColNo).Value.ToString, "/", "")

                    'JOB_NO
                    JobNo = .Cells(i, LMG010G.sprDetailDef.KIWARI_KBN.ColNo).Value.ToString()

                    '今回請求日
                    InvDate = .Cells(i, LMG010G.sprDetailDef.INV_DATE.ColNo).Value.ToString

                    '前回計算有無判定
                    If SHUBETSU.Equals(LMG010C.EventShubetsu.JIKKOU) = True Then
                        '未完了でもチェックするボタンチェック以外を判定する
                        If Me._Frm.chkMikan.Checked = False Then
                            If SeikyukikanTo >= InvDate = False Then

                                '荷主コード
                                CustNo = .Cells(i, LMG010G.sprDetailDef.CUST_CD.ColNo).Value.ToString

                                Me.ShowMessage("E335", New String() {CustNo})
                                Return False
                            End If
                        End If
                    End If
                End If
            Next

        End With
        Select Case SHUBETSU
            Case LMG010C.EventShubetsu.JIKKOU                                    '実行処理

                '未選択チェック
                If Me.IsSelectChk(Me._ChkList.Count()) = False Then
                    Me.ShowMessage("E009")
                    Return False
                End If

                '期割区分混在チェック
                If Me.IsKonZaiChk(KIWARIList) = False Then
                    Me.ShowMessage("E261")
                    Return False
                End If

            Case LMG010C.EventShubetsu.ZENKEISANTORI                             '前回計算取消処理

                '未選択チェック
                If Me.IsSelectChk(Me._ChkList.Count()) = False Then
                    Me.ShowMessage("E009")
                    Return False
                End If

                '複数選択チェック
                If Me.IsSelectOneChk(Me._ChkList.Count()) = False Then
                    Me.ShowMessage("E008")
                    Return False
                End If

                '前回計算処理有無チェック
                If String.IsNullOrEmpty(JobNo) = True Then
                    Me.ShowMessage("E296", New String() {"前回計算処理"})
                    Return False
                End If
        End Select

        Return True

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMG010C.SprColumnIndex.DEF

        '選択された行の行番号を取得
        Me._ChkList = Me.SprSelectList(defNo, Me._Frm.sprDetail)

        Return Me._ChkList

    End Function

    ''' <summary>
    ''' 期割混在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsKonZaiChk(ByVal KIWARIList As ArrayList) As Boolean

        '混在チェック用文言
        Dim Hiwari As String = "日割り"

        '日割のみの場合
        If Hiwari.Equals(KIWARIList(0).ToString) = True Then
            For i As Integer = 0 To KIWARIList.Count - 1

                '日割以外の場合エラー
                If Hiwari.Equals(KIWARIList(i).ToString) = False Then
                    Return False
                End If
            Next

        Else '日割り以外の場合
            For i As Integer = 0 To KIWARIList.Count - 1

                '日割の場合エラー
                If Hiwari.Equals(KIWARIList(i).ToString) = True Then
                    Return False
                End If
            Next
        End If

        Return True

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
                If .Cells(i, LMG010G.sprDetailDef.DEF.ColNo).Value.ToString = True.ToString Then

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

#End Region

End Class
