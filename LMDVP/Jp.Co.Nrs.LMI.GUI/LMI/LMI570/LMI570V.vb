' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI570V : TSMC請求データ検索
'  作  成  者       :  [HORI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports FarPoint.Win.Spread

''' <summary>
''' LMI570Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI570V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI570F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI570F, ByVal v As LMIControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

        Me._ChkList = New ArrayList()

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal SHUBETSU As LMI570C.EventShubetsu) As Boolean

        '権限の設定
        Dim _Kengen As String = LMUserInfoManager.GetAuthoLv

        Select Case _Kengen
            Case LMConst.AuthoKBN.AGENT                '外部
                Select Case SHUBETSU
                    Case LMI570C.EventShubetsu.CLOSE
                        Return True
                    Case Else                          '閉じる以外はFalseを返却
                        MyBase.ShowMessage("E016")
                        Return False
                End Select
            Case LMConst.AuthoKBN.VIEW                 '閲覧者
                Select Case SHUBETSU
                    '印刷時
                    Case LMI570C.EventShubetsu.PRINT   '印刷以外はTrueを返却
                        MyBase.ShowMessage("E016")
                        Return False
                    Case Else
                        Return True
                End Select
        End Select

        '上記以外の権限区分はTrueを返却
        Return True

    End Function

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck(ByVal SHUBETSU As LMI570C.EventShubetsu) As Boolean

        With Me._Frm
            Select Case SHUBETSU
                Case LMI570C.EventShubetsu.KENSAKU, LMI570C.EventShubetsu.PRINT, LMI570C.EventShubetsu.MASTER
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

                    '荷主コード（小）
                    .txtCustCdS.ItemName = "荷主コード（小）"
                    .txtCustCdS.IsForbiddenWordsCheck = True
                    .txtCustCdS.IsByteCheck = 2
                    If MyBase.IsValidateCheck(.txtCustCdS) = False Then
                        Return False
                    End If

                    '荷主コード（極小）
                    .txtCustCdSs.ItemName = "荷主コード（極小）"
                    .txtCustCdSs.IsForbiddenWordsCheck = True
                    .txtCustCdSs.IsByteCheck = 2
                    If MyBase.IsValidateCheck(.txtCustCdSs) = False Then
                        Return False
                    End If

                    '請求先コード
                    .txtSekySaki.ItemName = "請求先コード"
                    .txtSekySaki.IsForbiddenWordsCheck = True
                    .txtSekySaki.IsByteCheck = 7
                    If MyBase.IsValidateCheck(.txtSekySaki) = False Then
                        Return False
                    End If

                    Select Case SHUBETSU
                        Case LMI570C.EventShubetsu.KENSAKU, LMI570C.EventShubetsu.PRINT

                            If LMI570C.EventShubetsu.PRINT.Equals(SHUBETSU) = True Then

                                '印刷種別
                                .cmbPrint.ItemName = "印刷種別"
                                .cmbPrint.IsHissuCheck = True
                                If MyBase.IsValidateCheck(.cmbPrint) = False Then
                                    Return False
                                End If

                            End If
                    End Select
            End Select
        End With

        Return True

    End Function

    ''' <summary>
    ''' スプレッド項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadInputChk() As Boolean

        With Me._Frm

            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprMeisai)

            '請求先名
            vCell.SetValidateCell(0, LMI570G.sprMeisaiDef.SEIQTO_NM.ColNo)
            vCell.ItemName = "請求先名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 240
            If Me.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' スプレッド項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadInputPrintChk(ByVal arr As ArrayList) As Boolean

        With Me._Frm

            'チェックリスト格納変数
            Dim max As Integer = arr.Count - 1

            '選択チェック
            If Me.IsSelectDataChk(arr) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectDataChk(ByVal arr As ArrayList) As Boolean

        '選択チェック
        If Me.IsSelectChk(arr.Count) = False Then
            'MyBase.ShowMessage("E009")
            Return False
        End If

        '単一行選択チェック
        If Me.IsSelectOneChk(arr.Count) = False Then
            'MyBase.ShowMessage("E008")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <param name="SHUBETSU">イベント種別</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRelationChk(ByVal SHUBETSU As LMI570C.EventShubetsu) As Boolean

        Dim custcdl As String = Me._Frm.txtCustCdL.TextValue
        Dim sekysaki As String = Me._Frm.txtSekySaki.TextValue
        Dim invdateto As String = Me._Frm.imdInvDate.TextValue

        '検索処理時
        If SHUBETSU.Equals(LMI570C.EventShubetsu.KENSAKU) = True Then

            If String.IsNullOrEmpty(custcdl) = True _
            AndAlso String.IsNullOrEmpty(sekysaki) = True _
            AndAlso String.IsNullOrEmpty(invdateto) = True Then
                Me._Vcon.SetErrorControl(Me._Frm.txtSekySaki)
                Me._Vcon.SetErrorControl(Me._Frm.imdInvDate)
                Me._Vcon.SetErrorControl(Me._Frm.txtCustCdL)
                Me.ShowMessage("E325")
                Return False
            End If
        Else

            '選択チェック
            Dim max As Integer = 0
            Dim chklist As ArrayList = New ArrayList
            With Me._Frm.sprMeisai.ActiveSheet
                max = .Rows.Count - 1
                For i As Integer = 0 To max

                    If LMConst.FLG.ON.Equals(Me._Vcon.GetCellValue(.Cells(i, LMI570G.sprMeisaiDef.DEF.ColNo))) = True Then

                        chklist.Add(i)
                    End If
                Next
            End With

            '未選択チェック
            If Me.IsSelectChk(chklist.Count()) = False Then
                Me.ShowMessage("E009")
                Return False
            End If

            '複数選択チェック
            If Me.IsSelectOneChk(chklist.Count()) = False Then
                Me.ShowMessage("E008")
                Return False
            End If

        End If
        Return True

    End Function

    ''' <summary>
    ''' 選択行有無判別
    ''' </summary>
    ''' <param name="rowCnt">選択行数（省略可）</param>
    ''' <returns>-1：選択行無し　0：単一行選択中　1：複数行選択中</returns>
    ''' <remarks></remarks>
    Friend Function FindSelectedRow(Optional ByRef rowCnt As Integer = 0) As Integer

        With Me._Frm.sprMeisai.Sheets(0)

            Dim rowIdx As Integer = -1

            For i As Integer = 1 To .RowCount - 1
                If .Cells(i, LMI570G.sprMeisaiDef.DEF.ColNo).Value.ToString = True.ToString Then

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
    ''' 未選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectChk(ByVal chkCnt As Integer) As Boolean

        'チェック件数が0件
        If chkCnt = 0 Then

            MyBase.ShowMessage("E009")
            Return False

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
            MyBase.ShowMessage("E008")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMI570C.SprColumnIndex.DEF

        '選択された行の行番号を取得
        Me._ChkList = Me.SprSelectList(defNo, Me._Frm.sprMeisai)

        Return Me._ChkList

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

#End Region 'Method

End Class
