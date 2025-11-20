' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC030G : 送状番号入力
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMC030Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC030G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMC030F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMC030F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F5ButtonName = "追　加"
            .F6ButtonName = "更　新"
            .F7ButtonName = String.Empty
            '.F8ButtonName = "取　込"
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = String.Empty
            .F11ButtonName = "保　存"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F5ButtonEnabled = True
            .F6ButtonEnabled = True
            .F7ButtonEnabled = False
            '.F8ButtonEnabled = True
            .F8ButtonEnabled = False
            .F9ButtonEnabled = False
            .F10ButtonEnabled = False
            .F11ButtonEnabled = always
            .F12ButtonEnabled = always

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .txtSyukkaLNo.TabIndex = LMC030C.FrmControlIndex.SYUKKA_L_NO
            .txtDenpNo.TabIndex = LMC030C.FrmControlIndex.DENP_NO
            .sprOkuriList.TabIndex = LMC030C.FrmControlIndex.OKURI_LIST

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        Dim noMnb As Boolean = True
        Dim dtTori As Boolean = True

        With Me._Frm

            '入力文字数制限
            .txtSyukkaLNo.MaxLength = 9
            .txtDenpNo.MaxLength = 20

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm
            .txtSyukkaLNo.Focus()
        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With _Frm
            .txtSyukkaLNo.TextValue = String.Empty
            .txtDenpNo.TextValue = String.Empty
        End With

    End Sub

    ''' <summary>
    ''' 先頭および末尾の文字『a』を除外する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub RemoveBarcodeA()

        Dim removeString As String = "a"

        With _Frm

            '数値型の場合、そのまま
            If IsNumeric(.txtDenpNo.TextValue) = True OrElse String.IsNullOrEmpty(.txtDenpNo.TextValue) = True Then Exit Sub

            If .txtDenpNo.TextValue.Substring(.txtDenpNo.TextValue.Length - 1, 1) = removeString _
            AndAlso .txtDenpNo.TextValue.Substring(0, 1) = removeString Then

                .txtDenpNo.TextValue = .txtDenpNo.TextValue.Substring(1, .txtDenpNo.TextValue.Length - 2)

            End If

        End With

    End Sub
#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprOkuriListDef

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMC030C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared OUTKA_CTL_NO As SpreadColProperty = New SpreadColProperty(LMC030C.SprColumnIndex.OUTKA_CTL_NO, "出荷管理番号(大)", 140, True)
        Public Shared DENP_NO As SpreadColProperty = New SpreadColProperty(LMC030C.SprColumnIndex.DENP_NO, "送り状番号", 150, True)
        Public Shared UNSO_NM As SpreadColProperty = New SpreadColProperty(LMC030C.SprColumnIndex.UNSO_NM, "運送会社名", 200, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMC030C.SprColumnIndex.CUST_NM_L, "届先名", 200, True)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprOkuriList.CrearSpread()

            '列数設定
            .sprOkuriList.Sheets(0).ColumnCount = 5

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprOkuriList.SetColProperty(New sprOkuriListDef)
            .sprOkuriList.SetColProperty(New sprOkuriListDef, False)
            '2015.10.15 英語化対応END

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable, ByVal chkList As ArrayList)

        Dim spr As LMSpread = Me._Frm.sprOkuriList

        With spr

            .SuspendLayout()

            Dim lngcnt As Integer = dt.Rows.Count() - 1

            If lngcnt = -1 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            '値設定
            For i As Integer = 0 To lngcnt

                Dim intRow As Integer = Convert.ToInt32(chkList(i))

                'セルに値を設定
                .SetCellValue(intRow, sprOkuriListDef.UNSO_NM.ColNo, dt.Rows(i)("UNSOCO_NM").ToString())
                .SetCellValue(intRow, sprOkuriListDef.CUST_NM_L.ColNo, dt.Rows(i)("DEST_NM").ToString())
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの行追加
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub AddSpread(ByVal frm As LMC030F)
        Dim spr As LMSpread = _Frm.sprOkuriList

        With spr

            Dim intRow As Integer = .Sheets(0).Rows.Count
            .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            'セルスタイル設定
            .SetCellStyle(intRow, sprOkuriListDef.DEF.ColNo, sDEF)
            .SetCellStyle(intRow, sprOkuriListDef.OUTKA_CTL_NO.ColNo, sLabel)
            .SetCellStyle(intRow, sprOkuriListDef.DENP_NO.ColNo, sLabel)
            .SetCellStyle(intRow, sprOkuriListDef.UNSO_NM.ColNo, sLabel)
            .SetCellStyle(intRow, sprOkuriListDef.CUST_NM_L.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(intRow, sprOkuriListDef.DEF.ColNo, LMC030C.CHECKED_TRUE)
            .SetCellValue(intRow, sprOkuriListDef.OUTKA_CTL_NO.ColNo, frm.txtSyukkaLNo.TextValue)
            .SetCellValue(intRow, sprOkuriListDef.DENP_NO.ColNo, frm.txtDenpNo.TextValue)
            .SetCellValue(intRow, sprOkuriListDef.UNSO_NM.ColNo, String.Empty)
            .SetCellValue(intRow, sprOkuriListDef.CUST_NM_L.ColNo, String.Empty)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの行削除
    ''' </summary>
    ''' <param name="chkList"></param>
    ''' <remarks></remarks>
    Friend Sub DelSpread(ByVal chkList As ArrayList)

        Dim spr As LMSpread = _Frm.sprOkuriList
        Dim intRow As Integer
        Dim max As Integer = chkList.Count - 1

        For i As Integer = max To 0 Step -1

            intRow = Convert.ToInt32(chkList(i))

            spr.Sheets(0).RemoveRows(intRow, 1)

        Next
    End Sub

#End Region 'Spread

#End Region 'Method

End Class
