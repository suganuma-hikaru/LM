' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMU       : 文書管理
'  プログラムID     :  LMU020G   : スキャン取込
'  作  成  者       :  NRS)OHNO
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Const.LMConst
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMU020Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 2006/07/21 IWAMOTO
''' </histry>
Public Class LMU020G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMU020F

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMU020F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim edit As Boolean = True
        Dim refer As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            '.SetFKeysType(NFImFunctionKey.FkeyTypes.LIST)

            'ファンクションキー名個別設定
            .F1ButtonName = "スキャンファイル" & vbCrLf & "取込"
            .F2ButtonName = "取込結果フォルダ" & vbCrLf & "Open"
            .F3ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F10ButtonName = String.Empty
            .F11ButtonName = "保存"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = True
            .F2ButtonEnabled = True
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = False
            .F10ButtonEnabled = False
            .F11ButtonEnabled = True
            .F12ButtonEnabled = True

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm
            '.chkCustFlg.TabIndex = 0
            '.chkShipCompFlg.TabIndex = 1
            '.chkTkanFlg.TabIndex = 2
            '.chkTrnFlg.TabIndex = 3
            '.chkPortFlg.TabIndex = 4
            '.chkFactoryFlg.TabIndex = 5
            '.chkDepoFlg.TabIndex = 6
            '.chkCustomFlg.TabIndex = 7
            '.chkOthersFlg.TabIndex = 8
            '.chkDeptFlg.TabIndex = 9
            .sprFileImport.TabIndex = 10
        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal frm As LMU020F)

        'With frm
        '    .sprFileImport.Sheets(0).Cells(0, LMU020G.SprCompDef.JOTAI.ColNo).Value = LMU020C.FLG.OFF
        'End With

    End Sub

    '    ''' <summary>
    '    ''' コントロールの入力制御
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Friend Sub SetControlsStatus()

    '        Dim prv As String = LMUserInfoManager.GetPrvMstKbn()

    '        ''Dim edit As Boolean
    '        ''Dim refer As Boolean

    '        ''With Me._Frm

    '        ' ''編集モード
    '        ''If DispMode.EDIT.Equals(.lblSituation.DispMode) Then

    '        ''    edit = True
    '        ''    refer = False

    '        ''Else '参照モード

    '        ''    edit = False
    '        ''    refer = True

    '        ''End If

    '        ''.[コントロール].ReadOnly = refer
    '        ''.[コントロール].ReadOnly = edit

    '        ''End With

    '    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class SprImportFileDef
        Public Shared CHECK As SpreadColProperty = New SpreadColProperty(0, " ", 19, True)
        Public Shared ORD_NO As SpreadColProperty = New SpreadColProperty(1, "受注番号", 150, True)
        Public Shared FILE_NM As SpreadColProperty = New SpreadColProperty(2, "ファイル名", 150, True)
        Public Shared FILE_TYPE As SpreadColProperty = New SpreadColProperty(3, "ファイルタイプ", 50, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm.sprFileImport
            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .Sheets(0).ColumnCount = 4

            '固定列
            '.Sheets(0).FrozenColumnCount = 3

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New SprImportFileDef)

            'ドラッグで行選択
            .AllowDragFill = True

            '列設定(1行目)
            '.SetCellStyle(0, SprCompDef.JOTAI.ColNo, LMSpreadUtility.GetComboCellKbn(Me._Frm.sprFileImport, NFKbnConst.KBN_S001, False))
            '.SetCellStyle(0, SprCompDef.COMP_CD.ColNo, LMSpreadUtility.GetTextCell(Me._Frm.sprFileImport, InputControl.HAN_NUM_ALPHA, 6, False))
            '.SetCellStyle(0, SprCompDef.YEN_JDE_CD.ColNo, LMSpreadUtility.GetTextCell(Me._Frm.sprFileImport, InputControl.HAN_NUM_ALPHA, 8, False))
            '.SetCellStyle(0, SprImportFileDef.ORD_NO.ColNo, LMSpreadUtility.GetTextCell(Me._Frm.sprFileImport, InputControl.ALL_MIX, 50, False))

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定
    ''' </summary>
    ''' <param name="ordNo">データセット</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ordNo As String, ByVal fileNm As String, ByVal fileType As String)

        Dim spr As LMSpread = Me._Frm.sprFileImport

        With spr

            .SuspendLayout()


            Dim rowsCount As Integer = .ActiveSheet.Rows.Count

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, 1)

            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lblL As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim lblC As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Center)

            Dim dr As DataRow = Nothing

            'セルスタイル設定
            '**** 表示列 ****

            '.SetCellStyle(rowsCount, .ACTION.ColNo, def)
            .SetCellStyle(rowsCount, SprImportFileDef.ORD_NO.ColNo, lblC)
            .SetCellStyle(rowsCount, SprImportFileDef.FILE_NM.ColNo, lblC)
            .SetCellStyle(rowsCount, SprImportFileDef.FILE_TYPE.ColNo, lblC)

            'セル値設定
            '**** 表示列 ****
            .SetCellValue(rowsCount, SprImportFileDef.ORD_NO.ColNo, ordNo.ToString())
            .SetCellValue(rowsCount, SprImportFileDef.FILE_NM.ColNo, fileNm.ToString())
            .SetCellValue(rowsCount, SprImportFileDef.FILE_TYPE.ColNo, fileType.ToString())

            .ResumeLayout(True)

        End With

    End Sub

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

            'チェックボックスの場合、0 or 1を返却
            If Me.changeBooleanCheckBox(aCell.Text) = True Then
                GetCellValue = BaseConst.FLG.ON
            Else
                GetCellValue = BaseConst.FLG.OFF
            End If

        ElseIf TypeOf aCell.Editor Is CellType.NumberCellType Then

            'ナンバーの場合、Value値を返却
            If aCell.Value Is Nothing = False AndAlso String.IsNullOrEmpty(aCell.Value.ToString()) = False Then
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
    ''' チェックボックスの値をString型からBoolean型に変換する
    ''' </summary>
    ''' <param name="textValue">obj.text(0:チェック無し,1:チェック有り)</param>
    ''' <returns>True:チェック有り,False:チェック無し</returns>
    ''' <remarks></remarks>
    Friend Function changeBooleanCheckBox(ByVal textValue As String) As Boolean

        '"1"の場合Trueを返却
        If (LMConst.FLG.ON.Equals(textValue) = True) _
            OrElse True.ToString().Equals(textValue) = True Then
            Return True
        End If

        '"0"の場合Falseを返却
        Return False

    End Function

#End Region 'Spread

#End Region


End Class
