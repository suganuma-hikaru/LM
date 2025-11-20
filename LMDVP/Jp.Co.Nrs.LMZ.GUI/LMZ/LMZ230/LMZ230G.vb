' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ230G : 運賃タリフマスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMZ230Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ230G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ230F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConG As LMZControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ230F)

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

#Region "Form"

    ''' <summary>
    ''' 画面ヘッダー部値設定
    ''' </summary>
    ''' <param name="drow">荷主キャッシュから取得したデータロウ配列</param>
    ''' <remarks></remarks>
    Friend Sub CustHeaderDataSet(ByVal drow As DataRow)

        With Me._Frm

            .txtCustCdL.TextValue = drow("CUST_CD_L").ToString()
            .lblCustNmL.TextValue = drow("CUST_NM_L").ToString()
            .txtCustCdM.TextValue = drow("CUST_CD_M").ToString()
            .lblCustNmM.TextValue = drow("CUST_NM_M").ToString()

        End With

    End Sub

    ''' <summary>
    ''' 画面ヘッダー部クリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CustHeaderClear()

        With Me._Frm

            .lblCustNmL.TextValue = String.Empty
            .lblCustNmM.TextValue = String.Empty

        End With

    End Sub
#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ230C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared UNCHIN_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMZ230C.SprColumnIndex.UNCHIN_TARIFF_CD, "タリフコード", 100, True)
        Public Shared UNCHIN_TARIFF_REM As SpreadColProperty = New SpreadColProperty(LMZ230C.SprColumnIndex.UNCHIN_TARIFF_REM, "備考", 325, True)
        Public Shared TABLE_TP_NM As SpreadColProperty = New SpreadColProperty(LMZ230C.SprColumnIndex.TABLE_TP_NM, "タリフタイプ", 265, True)
        Public Shared TABLE_TP As SpreadColProperty = New SpreadColProperty(LMZ230C.SprColumnIndex.TABLE_TP, "タリフタイプ区分", 80, False)
        Public Shared STR_DATE As SpreadColProperty = New SpreadColProperty(LMZ230C.SprColumnIndex.STR_DATE, "適用開始日", 90, True)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ230C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(ByVal drow As DataRow)

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = 7

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New sprDetailDef)
            .sprDetail.SetColProperty(New sprDetailDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMZ230G.sprDetailDef.DEF.ColNo + 1
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left)

            '列設定
            .sprDetail.SetCellStyle(0, LMZ230G.sprDetailDef.UNCHIN_TARIFF_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 10, False))
            .sprDetail.SetCellStyle(0, LMZ230G.sprDetailDef.UNCHIN_TARIFF_REM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))
            .sprDetail.SetCellStyle(0, LMZ230G.sprDetailDef.TABLE_TP_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "U011", False))
            .sprDetail.SetCellStyle(0, LMZ230G.sprDetailDef.TABLE_TP.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMZ230G.sprDetailDef.STR_DATE.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMZ230G.sprDetailDef.ROW_INDEX.ColNo, lbl)

        End With

        Call Me.SetInitValue(drow)

    End Sub

    ''' <summary>
    ''' 営業所コンボボックス初期値とスプレッド初期値を設定します
    ''' </summary>
    ''' <param name="drow"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal drow As DataRow)

        With _Frm

            .cmbNrsBrCd.SelectedValue = drow("NRS_BR_CD")
            .lblStrDate.TextValue = drow("STR_DATE").ToString()

            Dim custCdL As String = String.Empty
            Dim custCdM As String = String.Empty
            custCdL = drow("CUST_CD_L").ToString()
            custCdM = drow("CUST_CD_M").ToString()

            .txtCustCdL.TextValue = custCdL
            .txtCustCdM.TextValue = custCdM

            If String.IsNullOrEmpty(custCdL) = False _
                AndAlso String.IsNullOrEmpty(custCdM) = True Then
                .txtCustCdM.TextValue = "00"
            End If


        End With

        With _Frm.sprDetail

            .SetCellValue(0, LMZ230G.sprDetailDef.UNCHIN_TARIFF_CD.ColNo, drow("UNCHIN_TARIFF_CD").ToString())
            .SetCellValue(0, LMZ230G.sprDetailDef.UNCHIN_TARIFF_REM.ColNo, drow("UNCHIN_TARIFF_REM").ToString())
            .SetCellValue(0, LMZ230G.sprDetailDef.TABLE_TP_NM.ColNo, drow("TABLE_TP").ToString())
            .SetCellValue(0, LMZ230G.sprDetailDef.TABLE_TP.ColNo, drow("TABLE_TP").ToString())
            '.SetCellValue(0, LMZ230G.sprDetailDef.STR_DATE.ColNo, drow("STR_DATE").ToString())

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As DataSet = New DataSet()

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()
            '行数設定
            'Dim tbl As DataTable = dtOut.Tables(LMZ230C.TABLE_NM_OUT)
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            'キーボード操作でチェックボックスＯＮ
            .KeyboardCheckBoxOn = True

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            Dim dRow As DataRow = Nothing
            Dim strDate As String = String.Empty


            '値設定
            For i As Integer = 1 To lngcnt

                dRow = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.UNCHIN_TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNCHIN_TARIFF_REM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TABLE_TP_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TABLE_TP.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.STR_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)

                .SetCellValue(i, sprDetailDef.UNCHIN_TARIFF_CD.ColNo, dRow.Item("UNCHIN_TARIFF_CD").ToString())
                .SetCellValue(i, sprDetailDef.UNCHIN_TARIFF_REM.ColNo, dRow.Item("UNCHIN_TARIFF_REM").ToString())
                .SetCellValue(i, sprDetailDef.TABLE_TP_NM.ColNo, dRow.Item("TABLE_TP_NM").ToString())
                .SetCellValue(i, sprDetailDef.TABLE_TP.ColNo, dRow.Item("TABLE_TP").ToString())

                strDate = DateFormatUtility.EditSlash(dRow.Item("STR_DATE").ToString())
                .SetCellValue(i, sprDetailDef.STR_DATE.ColNo, strDate)

                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))
            Next

            .ResumeLayout(True)

        End With

    End Sub


#End Region 'Spread

#Region "部品"

    ''' <summary>
    ''' ロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub SetLockControl(ByVal ctl As Control, Optional ByVal lockFlg As Boolean = False)

        Dim arr As ArrayList = New ArrayList()
        Call Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
        Dim lblArr As ArrayList = New ArrayList()

        'エディット系コントロールのロック
        For Each arrCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In arr

            'テキストボックスの場合、ラベル項目であったら処理対象外とする
            If TypeOf arrCtl Is Win.InputMan.LMImTextBox = True Then

                If DirectCast(arrCtl, Win.InputMan.LMImTextBox).Name.Substring(0, 3).Equals("lbl") = True Then
                    lblArr.Add(arrCtl)
                End If

            End If

            'ロック処理/ロック解除処理を行う
            arrCtl.ReadOnlyStatus = lockFlg
        Next

        'ラベル項目をロック
        For Each lblCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In lblArr
            lblCtl.ReadOnlyStatus = True
        Next

        'ボタンのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMButton)(arr, ctl)
        For Each arrCtl As Win.LMButton In arr
            'ロック処理/ロック解除処理を行う
            Call Me.LockButton(arrCtl, lockFlg)
        Next

        'チェックボックスのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMCheckBox)(arr, ctl)
        For Each arrCtl As Win.LMCheckBox In arr

            'ロック処理/ロック解除処理を行う
            Call Me.LockCheckBox(arrCtl, lockFlg)

        Next

    End Sub


    ''' <summary>
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Private Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

        If TypeOf ownControl Is T _
            OrElse ownControl.GetType.IsSubclassOf(GetType(T)) Then

            '指定されたクラスかその継承クラス
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(チェックボックス)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockCheckBox(ByVal ctl As Win.LMCheckBox, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.EnableStatus = enabledFlg

    End Sub


    ''' <summary>
    ''' 引数のコントロールをロックする(ボタン)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockButton(ByVal ctl As Win.LMButton, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

    '要望対応:1248 terakawa 2013.03.21 Start
#Region "マイ運賃タリフ"

    ''' <summary>
    ''' マイ運賃タリフオプションボタンを設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="myUnchinTariffFlg"></param>
    ''' <remarks></remarks>
    Friend Sub SetMyUnchinTariffOptionButton(ByVal frm As LMZ230F, ByVal myUnchinTariffFlg As Boolean)

        If myUnchinTariffFlg = True Then
            'マイ運賃タリフオプションボタンを有効
            frm.optMyUnchinTariff.Checked = True
        Else
            '全件オプションボタンを有効
            frm.optAll.Checked = True
        End If

    End Sub

    ''' <summary>
    ''' マイ運賃タリフオプションボタンをロックします
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub LockMyUnchinTariffOptionButton(ByVal frm As LMZ230F)

        'マイ運賃タリフオプションボタンをロック
        frm.optMyUnchinTariff.Enabled = False

    End Sub

#End Region
    '要望対応:1248 terakawa 2013.03.21 End


#End Region

#End Region

End Class
