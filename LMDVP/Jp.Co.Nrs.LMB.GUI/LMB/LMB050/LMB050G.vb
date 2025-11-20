' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB050G : 入荷検品取込
'  作  成  者       :  菊池
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMB050Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB050G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB050F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBConG As LMBControlG


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB050F)

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
            .SetFKeysType(LMImFunctionKey.FkeyTypes.POP_L)

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
            .F9ButtonName = "検　索"
            .F10ButtonName = String.Empty
            .F11ButtonName = "選　択"
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
            .F9ButtonEnabled = True
            .F10ButtonEnabled = False
            .F11ButtonEnabled = True
            .F12ButtonEnabled = True

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

    Friend Sub SetInitForm(ByVal frm As LMB050F, ByVal prmDs As DataSet)

        Dim custNm As String = String.Empty
        Dim strSqlCust As String = String.Empty

        With prmDs.Tables(LMB050C.TABLE_NM_IN)
            frm.cmbNrsBrCd.SelectedValue = .Rows(0)("NRS_BR_CD").ToString()
            frm.lblCustCDL.TextValue = .Rows(0)("CUST_CD_L").ToString()
            frm.lblCustCDM.TextValue = .Rows(0)("CUST_CD_M").ToString()
            frm.cmbSoko.SelectedValue = .Rows(0)("WH_CD").ToString()
            frm.imdSysEntDate.TextValue = .Rows(0)("INKA_DATE").ToString()

            Dim getCustDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST). _
            Select(String.Concat("SYS_DEL_FLG = '0' AND CUST_CD_L = '", .Rows(0).Item("CUST_CD_L").ToString(), _
                "' AND CUST_CD_M = '", .Rows(0).Item("CUST_CD_M").ToString(), "' AND CUST_CD_S = '00' AND CUST_CD_SS = '00'"))

            If getCustDr.Length() > 0 Then
                frm.lblCustNmLM.TextValue = String.Concat(getCustDr(0).Item("CUST_NM_L").ToString(), getCustDr(0).Item("CUST_NM_M").ToString())
            End If

        End With


    End Sub

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .imdSysEntDate.TabIndex = LMB050C.CtlTabIndex_MAIN.SYS_ENT_DATE
            .cmbSoko.TabIndex = LMB050C.CtlTabIndex_MAIN.SOKO
            .sprDetail.TabIndex = LMB050C.CtlTabIndex_MAIN.SPRDETAIL

        End With

    End Sub

    ''' <summary>
    ''' 画面ヘッダー部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbNrsBrCd, lock)
            Me.SetLockControl(.lblCustCdL, lock)
            Me.SetLockControl(.lblCustNmLM, lock)
            Me.SetLockControl(.lblCustCdM, lock)
            .sprDetail.Enabled = Not lock

        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.DEF, " ", 20, True)

        Public Shared STATE As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.STATE, " ｽﾃｰﾀｽ ", 60, True)
        Public Shared BUYER_ORD_NO_L As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.BUYER_ORD_NO_L, " 注文番号 ", 80, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.GOODS_NM, " 商品名 ", 150, True)
        Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.GOODS_CD_CUST, " 商品コード ", 80, True)
        Public Shared STD_IRIME_NB As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.STD_IRIME_NB, " 標準入目 ", 70, True)
        Public Shared STD_IRIME_UT As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.STD_IRIME_UT, " 標準入目単位 ", 80, True)
        Public Shared JISSEKI_INKA_NB As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.JISSEKI_INKA_NB, " 個数 ", 60, True)
        Public Shared NB_UT As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.NB_UT, " 個数単位 ", 40, True)
        Public Shared JISSEKI_INKA_QT As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.JISSEKI_INKA_QT, " 数量 ", 60, True)
        Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.IRIME, " 入目 ", 50, True)
        Public Shared JISSEKI_PKG_UT As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.JISSEKI_PKG_UT, " 荷姿単位 ", 80, True)
        Public Shared PKG_NB As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.PKG_NB, " 入数 ", 60, True)
        Public Shared PKG_UT As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.PKG_UT, " 入数単位 ", 80, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.LOT_NO, " ロットNO ", 80, True)
        Public Shared REMARK_L As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.REMARK_L, " 備考大 ", 120, True)
        Public Shared REMARK_M As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.REMARK_M, " 商品別コメント ", 120, True)
        Public Shared TOU_NO As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.TOU_NO, " 棟 ", 30, True)
        Public Shared SITU_NO As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.SITU_NO, " 室 ", 30, True)
        Public Shared ZONE_CD As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.ZONE_CD, " ZONE ", 50, True)
        Public Shared LOCA As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.LOCA, " ロケーション ", 30, True)
        Public Shared OFB_KB As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.OFB_KB, " 簿外品 ", 80, True)
        Public Shared OUTKA_FROM_ORD_NO_L As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.OUTKA_FROM_ORD_NO_L, " オーダー番号 ", 80, True)
        Public Shared CRT_DATE As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.CRT_DATE, " 報告日 ", 80, True)
        Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.GOODS_CD_NRS, " 商品キー ", 10, False)
        Public Shared ONDO_KB As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.ONDO_KB, " 温度 ", 10, False)
        Public Shared ONDO_STR_DATE As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.ONDO_STR_DATE, " 保管温度管理期間From ", 10, False)
        Public Shared ONDO_END_DATE As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.ONDO_END_DATE, " 保管温度管理期間To ", 10, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.SYS_UPD_DATE, " 更新日（排他用） ", 10, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.SYS_UPD_TIME, " 更新時間（排他用） ", 10, False)
        Public Shared M_GOODS_COUNT As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.M_GOODS_COUNT, " 商品Mカウント ", 10, False)
        Public Shared M_GOODS_UT_NB_COUNT As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.M_GOODS_UT_NB_COUNT, " 商品M(入数、入り目比較)カウント ", 10, False)
        Public Shared M_ZONE_COUNT As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.M_ZONE_COUNT, " 更新時間（排他用） ", 10, False)
        Public Shared STD_WT_KGS As SpreadColProperty = New SpreadColProperty(LMB050C.SprColumnIndex.STD_WT_KGS, " 標準重量 ", 10, False)


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
            .sprDetail.Sheets(0).ColumnCount = 34

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New sprDetailDef)
            .sprDetail.SetColProperty(New sprDetailDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.商品名で固定)
            .sprDetail.Sheets(0).FrozenColumnCount = sprDetailDef.GOODS_NM.ColNo + 1

            '列設定
            .SPrDetail.SetCellStyle(0, LMB050G.sprDetailDef.DEF.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.STATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.BUYER_ORD_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 30, False))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 60, False))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.GOODS_CD_CUST.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 20, False))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.STD_IRIME_NB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.STD_IRIME_UT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.JISSEKI_INKA_NB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.NB_UT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.JISSEKI_INKA_QT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.IRIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.JISSEKI_PKG_UT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.PKG_NB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.PKG_UT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 40, False))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.REMARK_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 100, False))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.REMARK_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 100, False))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.TOU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 2, False))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.SITU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 2, False))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.ZONE_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 2, False))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.LOCA.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 10, False))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.OFB_KB.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "B002", False))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 30, False))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.CRT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.GOODS_CD_NRS.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.ONDO_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.ONDO_STR_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.ONDO_END_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.M_GOODS_COUNT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.M_GOODS_UT_NB_COUNT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.M_ZONE_COUNT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMB050G.sprDetailDef.STD_WT_KGS.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

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
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            'キーボード操作でチェックボックスＯＮ
            '.KeyboardCheckBoxOn = True

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim rLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            Dim tNumCell As New FarPoint.Win.Spread.CellType.NumberCellType()
            tNumCell.ShowSeparator = True   'セパレータ表示する(おまけ)
            tNumCell.DecimalPlaces = 3      '小数点以下３桁
            tNumCell.FixedPoint = True      '小数点以下を固定表示(必ず0.000と表示する)

            Dim dRow As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dRow = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.STATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.BUYER_ORD_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GOODS_CD_CUST.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.STD_IRIME_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 99999999999999, True, 0, True, ","))
                .SetCellStyle(i, sprDetailDef.STD_IRIME_UT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.JISSEKI_INKA_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 99999999999999, True, 0, True, ","))
                .SetCellStyle(i, sprDetailDef.NB_UT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.JISSEKI_INKA_QT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.IRIME.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.JISSEKI_PKG_UT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.PKG_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 99999999999999, True, 0, True, ","))
                .SetCellStyle(i, sprDetailDef.PKG_UT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.LOT_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.REMARK_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.REMARK_M.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TOU_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SITU_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ZONE_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.LOCA.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OFB_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CRT_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GOODS_CD_NRS.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ONDO_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ONDO_STR_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ONDO_END_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.M_GOODS_COUNT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.M_GOODS_UT_NB_COUNT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.M_ZONE_COUNT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.STD_WT_KGS.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.STATE.ColNo, dRow.Item("STATE").ToString())
                .SetCellValue(i, sprDetailDef.BUYER_ORD_NO_L.ColNo, dRow.Item("BUYER_ORD_NO_L").ToString())
                .SetCellValue(i, sprDetailDef.GOODS_NM.ColNo, dRow.Item("GOODS_NM").ToString())
                .SetCellValue(i, sprDetailDef.GOODS_CD_CUST.ColNo, dRow.Item("GOODS_CD_CUST").ToString())
                .SetCellValue(i, sprDetailDef.STD_IRIME_NB.ColNo, dRow.Item("STD_IRIME_NB").ToString())
                .SetCellValue(i, sprDetailDef.STD_IRIME_UT.ColNo, dRow.Item("STD_IRIME_UT").ToString())
                .SetCellValue(i, sprDetailDef.JISSEKI_INKA_NB.ColNo, dRow.Item("JISSEKI_INKA_NB").ToString())
                .SetCellValue(i, sprDetailDef.NB_UT.ColNo, dRow.Item("NB_UT").ToString())
                .SetCellValue(i, sprDetailDef.JISSEKI_INKA_QT.ColNo, dRow.Item("JISSEKI_INKA_QT").ToString())
                .SetCellValue(i, sprDetailDef.IRIME.ColNo, dRow.Item("IRIME").ToString())
                .SetCellValue(i, sprDetailDef.JISSEKI_PKG_UT.ColNo, dRow.Item("JISSEKI_PKG_UT").ToString())
                .SetCellValue(i, sprDetailDef.PKG_NB.ColNo, dRow.Item("PKG_NB").ToString())
                .SetCellValue(i, sprDetailDef.PKG_UT.ColNo, dRow.Item("PKG_UT").ToString())
                .SetCellValue(i, sprDetailDef.LOT_NO.ColNo, dRow.Item("LOT_NO").ToString())
                .SetCellValue(i, sprDetailDef.REMARK_L.ColNo, dRow.Item("REMARK_L").ToString())
                .SetCellValue(i, sprDetailDef.REMARK_M.ColNo, dRow.Item("REMARK_M").ToString())
                .SetCellValue(i, sprDetailDef.TOU_NO.ColNo, dRow.Item("TOU_NO").ToString())
                .SetCellValue(i, sprDetailDef.SITU_NO.ColNo, dRow.Item("SITU_NO").ToString())
                .SetCellValue(i, sprDetailDef.ZONE_CD.ColNo, dRow.Item("ZONE_CD").ToString())
                .SetCellValue(i, sprDetailDef.LOCA.ColNo, dRow.Item("LOCA").ToString())
                .SetCellValue(i, sprDetailDef.OFB_KB.ColNo, dRow.Item("OFB_KB").ToString())
                .SetCellValue(i, sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo, dRow.Item("OUTKA_FROM_ORD_NO_L").ToString())
                .SetCellValue(i, sprDetailDef.CRT_DATE.ColNo, dRow.Item("CRT_DATE").ToString())
                .SetCellValue(i, sprDetailDef.GOODS_CD_NRS.ColNo, dRow.Item("GOODS_CD_NRS").ToString())
                .SetCellValue(i, sprDetailDef.ONDO_KB.ColNo, dRow.Item("ONDO_KB").ToString())
                .SetCellValue(i, sprDetailDef.ONDO_STR_DATE.ColNo, dRow.Item("ONDO_STR_DATE").ToString())
                .SetCellValue(i, sprDetailDef.ONDO_END_DATE.ColNo, dRow.Item("ONDO_END_DATE").ToString())
                .SetCellValue(i, sprDetailDef.SYS_UPD_DATE.ColNo, dRow.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprDetailDef.SYS_UPD_TIME.ColNo, dRow.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprDetailDef.M_GOODS_COUNT.ColNo, dRow.Item("M_GOODS_COUNT").ToString())
                .SetCellValue(i, sprDetailDef.M_GOODS_UT_NB_COUNT.ColNo, dRow.Item("M_GOODS_UT_NB_COUNT").ToString())
                .SetCellValue(i, sprDetailDef.M_ZONE_COUNT.ColNo, dRow.Item("M_ZONE_COUNT").ToString())
                .SetCellValue(i, sprDetailDef.STD_WT_KGS.ColNo, dRow.Item("STD_WT_KGS").ToString())

            Next

            .ResumeLayout(True)

        End With


    End Sub

    ''' <summary>
    ''' 置き場のフォーマットを設定
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetOkibaFormat(ByVal row As DataRow) As String
        Dim kugiriStr As String = "-"
        Return String.Concat(row.Item("TOU_NO").ToString().Trim, kugiriStr, row.Item("SITU_NO").ToString().Trim, kugiriStr, row.Item("ZONE_CD").ToString().Trim, kugiriStr, row.Item("LOCA").ToString().Trim)

    End Function

    ''' <summary>
    ''' スプレッドの文字色設定
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadColor(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dr As DataRow
        Dim lngcnt As Integer = dt.Rows.Count()

        With spr

            If lngcnt = 0 Then
                Exit Sub
            End If

            For i As Integer = 1 To lngcnt
                dr = dt.Rows(i - 1)

                If String.IsNullOrEmpty(dr("M_GOODS_COUNT").ToString()) = False AndAlso Convert.ToInt64(dr("M_GOODS_COUNT")) = 0 Then
                    '対応する商品マスタが存在しない
                    .ActiveSheet.Rows(i).ForeColor = Color.Red

                ElseIf String.IsNullOrEmpty(dr("M_GOODS_UT_NB_COUNT").ToString()) = False AndAlso Convert.ToInt64(dr("M_GOODS_UT_NB_COUNT")) > 1 Then
                    '対応する商品マスタが入数・入り目別で複数存在する。
                    .ActiveSheet.Rows(i).ForeColor = Color.Blue

                ElseIf String.IsNullOrEmpty(dr("M_ZONE_COUNT").ToString()) = False AndAlso Convert.ToInt64(dr("M_ZONE_COUNT")) = 0 Then
                    '対応するゾーンマスタが存在しない
                    .ActiveSheet.Rows(i).ForeColor = Color.Red

                End If

            Next

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

#End Region

#End Region

End Class
