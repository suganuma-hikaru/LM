' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMD100C : 在庫テーブル照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports FarPoint.Win.Spread

''' <summary>
''' LMD100Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMD100V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD100F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMDControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD100F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMDControlV(handlerClass, DirectCast(frm, Form))

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 検索単項目チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuSingleCheck(Optional ByVal mode As String = LMD100C.MODE_DEFAULT) As Boolean

        '検索項目のTrim
        Call Me.TrimSpaceTextValue()

        With Me._Frm

            'ヘッダ部項目
            '営業所コンボ
            '20151030 tsunehira add Start
            '英語化対応
            .cmbEigyo.ItemName = .lblEigyo.TextValue
            '20151030 tsunehira add End
            '.cmbEigyo.ItemName = "営業所"
            .cmbEigyo.IsHissuCheck = True
            If MyBase.IsValidateCheck(Me._Frm.cmbEigyo) = False Then
                Return False
            End If

            '倉庫コンボ
            '20151030 tsunehira add Start
            '英語化対応
            .cmbSoko.ItemName = .LmTitleLabel1.TextValue
            '20151030 tsunehira add End
            '.cmbSoko.ItemName = "倉庫"
            .cmbSoko.IsHissuCheck = True
            If MyBase.IsValidateCheck(Me._Frm.cmbSoko) = False Then
                Return False
            End If

        End With

        'Spread部項目
        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprZai)

        With vCell

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '荷主商品コード
            .SetValidateCell(0, LMD100G.sprDetailDef.GOODS_CD_CUST.ColNo)
            .ItemName = LMD100G.sprDetailDef.GOODS_CD_CUST.ColName
            .IsForbiddenWordsCheck = chkFlg
            .IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '商品名
            .SetValidateCell(0, LMD100G.sprDetailDef.GOODS_NM.ColNo)
            .ItemName = LMD100G.sprDetailDef.GOODS_NM.ColName
            .IsForbiddenWordsCheck = chkFlg
            .IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            'ロットNo.
            .SetValidateCell(0, LMD100G.sprDetailDef.LOT_NO.ColNo)
            .ItemName = LMD100G.sprDetailDef.LOT_NO.ColName
            .IsForbiddenWordsCheck = chkFlg
            .IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '入目
            .SetValidateCell(0, LMD100G.sprDetailDef.IRIME.ColNo)
            .ItemName = LMD100G.sprDetailDef.IRIME.ColName
            .IsForbiddenWordsCheck = chkFlg
            .IsByteCheck = 13
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '備考小（社内）
            .SetValidateCell(0, LMD100G.sprDetailDef.REMARK.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            .ItemName = LMD100G.sprDetailDef.REMARK.ColName
            '20151030 tsunehira add End
            '.ItemName = "備考小（社内）" '改行防止のため直接指定
            .IsForbiddenWordsCheck = chkFlg
            .IsByteCheck = 15
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '備考小（社外）
            .SetValidateCell(0, LMD100G.sprDetailDef.REMARK_OUT.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            .ItemName = LMD100G.sprDetailDef.REMARK_OUT.ColName
            '20151030 tsunehira add End
            '.ItemName = "備考小（社外）" '改行防止のため直接指定
            .IsForbiddenWordsCheck = chkFlg
            .IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '届先名
            .SetValidateCell(0, LMD100G.sprDetailDef.DEST_NM.ColNo)
            .ItemName = LMD100G.sprDetailDef.DEST_NM.ColName
            .IsForbiddenWordsCheck = chkFlg
            .IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk() As Boolean

        Return True

    End Function

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Function SprSelectCount() As ArrayList

        Dim defNo As Integer = LMD100G.sprDetailDef.DEF.ColNo

        With Me._Frm.sprZai.ActiveSheet

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
    ''' データ選択時選択行共通チェック
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SentakuCheck(ByVal list As ArrayList) As Boolean

        '選択行件数を取得
        Dim listCount As Integer = list.Count

        '単一行／未選択チェック
        If listCount = 0 Then  '未選択の場合エラー
            MyBase.ShowMessage("E009")
            Return False

        ElseIf 1 < listCount Then  '複数行選択の場合エラー
            MyBase.ShowMessage("E008")
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 届先コードチェック
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function DestCdCheck(ByVal motoDestCd As String, ByVal dr As DataRow) As Boolean

        Dim popDestCd As String = dr.Item("DEST_CD").ToString()

        '呼び元画面の届先コードと、Popの届先コードが相違なる場合、エラー
        If motoDestCd.Equals(popDestCd) = False AndAlso String.IsNullOrEmpty(popDestCd) = False Then
            '2015.10.22 tusnehira add
            '英語化対応
            MyBase.ShowMessage("E774")
            'MyBase.ShowMessage("E227", New String() {"届先"})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 営業所コード、倉庫コード変更チェック
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function BrSokoCheck(ByVal dr As DataRow, ByVal brCd As String, ByVal sokoCd As String) As Boolean

        '選択データの営業所コード、倉庫コード取得
        Dim selectBrCd As String = dr.Item("NRS_BR_CD").ToString()
        Dim selectSokoCd As String = dr.Item("WH_CD").ToString()

        '選択データと遷移元画面設定情報が一致しない場合エラー
        If (brCd <> selectBrCd) OrElse (sokoCd <> selectSokoCd) Then
            MyBase.ShowMessage("E015")
            'MyBase.ShowMessage("E217", New String() {"営業所または倉庫", "初期表示"})
            Return False
            Exit Function

        End If

        Return True

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            'スプレッドのスペース除去
            Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprZai, 0)

        End With

    End Sub

#End Region 'Method

End Class
