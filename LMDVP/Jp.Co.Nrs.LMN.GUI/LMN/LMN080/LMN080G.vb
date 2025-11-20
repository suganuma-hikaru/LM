' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : ＳＣＭ
'  プログラムID   : LMN080G : 欠品警告
'  作  成  者     : 佐川央
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports GrapeCity.Win.Editors

''' <summary>
''' LMN080Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMN080G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMN080F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMN080F)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

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
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検索"
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = always
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
            .F12ButtonEnabled = always

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

            ''Main
            '.grpSearch.TabIndex = 0
            '.cmbCustCd.TabIndex = 1
            '.chkKeppinOnly.TabIndex = 2
            '.sprDetail.TabIndex = 3

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コンボボックスの設定
        Call Me.CreateComboBox()

    End Sub

    ''' <summary>
    ''' コンボボックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateComboBox()

        '区分マスタ検索処理（荷主コンボ設定用）
        Dim cd As String = String.Empty
        Dim item As String = String.Empty
        Dim sort As String = "KBN_CD"
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_S032, "' AND SYS_DEL_FLG = '0'"), sort)

        Dim max As Integer = getDr.Length - 1
        For i As Integer = 0 To max

            item = getDr(i).Item("KBN_NM3").ToString()
            cd = getDr(i).Item("KBN_NM1").ToString()

            Me._Frm.cmbCustCd.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))

        Next

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = LMN080C.MODE_DEFAULT)

        Dim noMnb As Boolean = True
        Dim dtTori As Boolean = True

        With Me._Frm


        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As String = "")

        With Me._Frm


        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm


        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

    End Sub


#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataSet)

        '参考値の設定
        Call Me.SetWareSpread(ds)

        Call Me.SetItemSpread(ds)

    End Sub

    ''' <summary>
    ''' 検索結果ヘッダー部表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHeaderData(ByVal strKNAPTA As String, ByVal strKBN As String)

        With Me._Frm

        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprWareDetailDef
        'スプレッド(タイトル列)の設定
        Public Shared WARE_CD As SpreadColProperty = New SpreadColProperty(0, "倉庫コード", 80, False)
        Public Shared WARE_NM As SpreadColProperty = New SpreadColProperty(1, "倉庫名称", 200, True)
        Public Shared ORDER_NUM As SpreadColProperty = New SpreadColProperty(2, "今回引当出荷オーダー数", 180, True)
        Public Shared PLAN_HIN_NUM As SpreadColProperty = New SpreadColProperty(3, "出荷予定品目数", 180, True)
        Public Shared KEPPIN_HIN_NUM As SpreadColProperty = New SpreadColProperty(4, "欠品品目数", 180, True)
        Public Shared KEPPINKIGU_HIN_NUM As SpreadColProperty = New SpreadColProperty(5, "欠品危惧品目数", 180, True)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(下部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprItemDetailDef
        'スプレッド(タイトル列)の設定
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(0, "荷主名称", 150, True)
        Public Shared ITEM_CD As SpreadColProperty = New SpreadColProperty(1, "荷主商品コード", 80, True)
        Public Shared ITEM_NM As SpreadColProperty = New SpreadColProperty(2, "商品名称", 230, True)
        Public Shared NYUSHUKKA_DATE As SpreadColProperty = New SpreadColProperty(3, "出荷日", 80, True)
        Public Shared KEPPIN_NUM As SpreadColProperty = New SpreadColProperty(4, "欠品数", 60, True)
        Public Shared ZAIKO_NUM As SpreadColProperty = New SpreadColProperty(5, "予定" & vbCrLf & "在庫数", 60, True)
        Public Shared HIKIATE_NUM As SpreadColProperty = New SpreadColProperty(6, "今回" & vbCrLf & "引当数", 60, True)
        Public Shared DETAIL_NUM As SpreadColProperty = New SpreadColProperty(7, "内訳", 60, True)
        Public Shared SHUKKASAKI_NM As SpreadColProperty = New SpreadColProperty(8, "出荷先名称", 300, True)
        Public Shared ORDER_NO As SpreadColProperty = New SpreadColProperty(9, "オーダーＮＯ", 80, True)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitWareSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprSokoDetail.CrearSpread()

            '列数設定
            .sprSokoDetail.Sheets(0).ColumnCount = 6

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprSokoDetail.SetColProperty(New sprWareDetailDef)

            '列固定位置を設定します。
            .sprSokoDetail.Sheets(0).FrozenColumnCount = sprWareDetailDef.WARE_NM.ColNo + 1

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitItemSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.Sheets(0).ColumnCount = 10

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprItemDetailDef)

            '列固定位置を設定します。
            .sprDetail.Sheets(0).FrozenColumnCount = sprItemDetailDef.ITEM_NM.ColNo + 1

        End With

    End Sub
    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMN080F, ByVal ds As DataSet)

        '荷主コンボボックスに初期値を設定
        frm.cmbCustCd.SelectedValue = ds.Tables(LMN080C.TABLE_NM_IN).Rows(0).Item("SCM_CUST_CD")

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(倉庫部明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetWareSpread(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprSokoDetail
        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()
            .Sheets(0).Rows.Count = 0
            'データ挿入()
            '行数設定()
            Dim tbl As DataTable = ds.Tables("LMN080OUT_L")
            Dim lngcnt As Integer = tbl.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If
            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            Dim dRow As DataRow

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dRow = tbl.Rows(i)

                'セルスタイル設定
                .SetCellStyle(i, sprWareDetailDef.WARE_NM.ColNo, sLabel)
                .SetCellStyle(i, sprWareDetailDef.ORDER_NUM.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999, True, 0, True, ","))
                .SetCellStyle(i, sprWareDetailDef.PLAN_HIN_NUM.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999, True, 0, True, ","))
                .SetCellStyle(i, sprWareDetailDef.KEPPIN_HIN_NUM.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999, True, 0, True, ","))
                .SetCellStyle(i, sprWareDetailDef.KEPPINKIGU_HIN_NUM.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999, True, 0, True, ","))

                'セルに値を設定
                .SetCellValue(i, sprWareDetailDef.WARE_CD.ColNo, dRow.Item("SOKO_CD").ToString())
                .SetCellValue(i, sprWareDetailDef.WARE_NM.ColNo, dRow.Item("SOKO_NM").ToString())
                .SetCellValue(i, sprWareDetailDef.ORDER_NUM.ColNo, dRow.Item("HIKIATE_ORD_NB").ToString())
                .SetCellValue(i, sprWareDetailDef.PLAN_HIN_NUM.ColNo, dRow.Item("PLAN_HINMOKU_NB").ToString())
                .SetCellValue(i, sprWareDetailDef.KEPPIN_HIN_NUM.ColNo, dRow.Item("KEPPIN_NB").ToString())
                .SetCellValue(i, sprWareDetailDef.KEPPINKIGU_HIN_NUM.ColNo, dRow.Item("PRE_KEPPIN_NB").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(商品明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetItemSpread(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()
            .Sheets(0).Rows.Count = 0
            'データ挿入
            '行数設定
            Dim tbl As DataTable = ds.Tables("LMN080OUT_M")
            Dim lngcnt As Integer = tbl.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If
            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            Dim dRow As DataRow

            '表示項目設定用
            Dim keyGoodsCd As String = String.Empty
            Dim keyDate As String = String.Empty

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dRow = tbl.Rows(i)

                'セルスタイル設定
                .SetCellStyle(i, sprItemDetailDef.CUST_NM.ColNo, sLabel)
                .SetCellStyle(i, sprItemDetailDef.ITEM_CD.ColNo, sLabel)
                .SetCellStyle(i, sprItemDetailDef.ITEM_NM.ColNo, sLabel)
                .SetCellStyle(i, sprItemDetailDef.NYUSHUKKA_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprItemDetailDef.KEPPIN_NUM.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999, True, 0, True, ","))
                .SetCellStyle(i, sprItemDetailDef.ZAIKO_NUM.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999, True, 0, True, ","))
                .SetCellStyle(i, sprItemDetailDef.HIKIATE_NUM.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999, True, 0, True, ","))
                .SetCellStyle(i, sprItemDetailDef.DETAIL_NUM.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999, True, 0, True, ","))
                .SetCellStyle(i, sprItemDetailDef.SHUKKASAKI_NM.ColNo, sLabel)
                .SetCellStyle(i, sprItemDetailDef.ORDER_NO.ColNo, sLabel)

                '表示項目設定
                '比較用行項目取得
                Dim GoodsCd As String = dRow.Item("GOODS_CD_CUST").ToString()
                Dim OutkaDate As String = dRow.Item("OUTKA_DATE").ToString()
                '日付表示編集
                Dim year As String = OutkaDate.Substring(0, 4)
                Dim month As String = OutkaDate.Substring(4, 2)
                Dim day As String = OutkaDate.Substring(6, 2)
                Dim setDate As String = String.Concat(year, "/", month, "/", day)

                '商品コードが異なる場合、すべて表示
                If (Not String.Equals(keyGoodsCd, GoodsCd)) Then
                    'セルに値を設定
                    .SetCellValue(i, sprItemDetailDef.CUST_NM.ColNo, dRow.Item("CUST_NM").ToString())
                    .SetCellValue(i, sprItemDetailDef.ITEM_CD.ColNo, dRow.Item("GOODS_CD_CUST").ToString())
                    .SetCellValue(i, sprItemDetailDef.ITEM_NM.ColNo, dRow.Item("GOODS_NM").ToString())
                    .SetCellValue(i, sprItemDetailDef.NYUSHUKKA_DATE.ColNo, setDate)
                    .SetCellValue(i, sprItemDetailDef.KEPPIN_NUM.ColNo, dRow.Item("KEPPIN_NB").ToString())
                    .SetCellValue(i, sprItemDetailDef.ZAIKO_NUM.ColNo, dRow.Item("PLAN_ZAIKO_NB").ToString())
                    .SetCellValue(i, sprItemDetailDef.HIKIATE_NUM.ColNo, dRow.Item("HIKIATE_NB").ToString())
                    .SetCellValue(i, sprItemDetailDef.DETAIL_NUM.ColNo, dRow.Item("DETAIL_NB").ToString())
                    .SetCellValue(i, sprItemDetailDef.SHUKKASAKI_NM.ColNo, dRow.Item("DEST_NM").ToString())
                    .SetCellValue(i, sprItemDetailDef.ORDER_NO.ColNo, dRow.Item("CUST_ORD_NO_L").ToString())
                    'キー更新
                    keyGoodsCd = GoodsCd
                    keyDate = OutkaDate

                    '商品コードが一致し、出荷日が異なる場合、出荷日から表示
                ElseIf (Not String.Equals(keyDate, OutkaDate)) Then
                    'セルに値を設定
                    .SetCellValue(i, sprItemDetailDef.CUST_NM.ColNo, "")
                    .SetCellValue(i, sprItemDetailDef.ITEM_CD.ColNo, "")
                    .SetCellValue(i, sprItemDetailDef.ITEM_NM.ColNo, "")
                    .SetCellValue(i, sprItemDetailDef.NYUSHUKKA_DATE.ColNo, setDate)
                    .SetCellValue(i, sprItemDetailDef.KEPPIN_NUM.ColNo, dRow.Item("KEPPIN_NB").ToString())
                    .SetCellValue(i, sprItemDetailDef.ZAIKO_NUM.ColNo, dRow.Item("PLAN_ZAIKO_NB").ToString())
                    .SetCellValue(i, sprItemDetailDef.HIKIATE_NUM.ColNo, dRow.Item("HIKIATE_NB").ToString())
                    .SetCellValue(i, sprItemDetailDef.DETAIL_NUM.ColNo, dRow.Item("DETAIL_NB").ToString())
                    .SetCellValue(i, sprItemDetailDef.SHUKKASAKI_NM.ColNo, dRow.Item("DEST_NM").ToString())
                    .SetCellValue(i, sprItemDetailDef.ORDER_NO.ColNo, dRow.Item("CUST_ORD_NO_L").ToString())
                    'キー更新
                    keyDate = OutkaDate

                    'それ以外の場合、内訳から表示
                Else
                    'セルに値を設定
                    .SetCellValue(i, sprItemDetailDef.CUST_NM.ColNo, "")
                    .SetCellValue(i, sprItemDetailDef.ITEM_CD.ColNo, "")
                    .SetCellValue(i, sprItemDetailDef.ITEM_NM.ColNo, "")
                    .SetCellValue(i, sprItemDetailDef.NYUSHUKKA_DATE.ColNo, "")
                    .SetCellValue(i, sprItemDetailDef.KEPPIN_NUM.ColNo, "")
                    .SetCellValue(i, sprItemDetailDef.ZAIKO_NUM.ColNo, "")
                    .SetCellValue(i, sprItemDetailDef.HIKIATE_NUM.ColNo, "")
                    .SetCellValue(i, sprItemDetailDef.DETAIL_NUM.ColNo, dRow.Item("DETAIL_NB").ToString())
                    .SetCellValue(i, sprItemDetailDef.SHUKKASAKI_NM.ColNo, dRow.Item("DEST_NM").ToString())
                    .SetCellValue(i, sprItemDetailDef.ORDER_NO.ColNo, dRow.Item("CUST_ORD_NO_L").ToString())

                End If

            Next

            .ResumeLayout(True)

        End With

    End Sub
#End Region 'Spread

#End Region

End Class
