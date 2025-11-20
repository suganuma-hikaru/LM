' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI140G : 物産アニマルヘルス倉庫内処理検索
'  作  成  者       :  [HORI]
' ==========================================================================

Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI140Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI140G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI140F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI140F)

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
        Dim edit As Boolean = False
        Dim view As Boolean = False
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = "新　規"
            .F2ButtonName = "実績送信"
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = always
            .F2ButtonEnabled = always
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = always
            .F10ButtonEnabled = lock
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region 'FunctionKey

#Region "Mode&Status"

    ''' <summary>
    ''' Dispモードとレコードステータスの設定
    ''' </summary>
    ''' <param name="mode">Dispモード</param>
    ''' <param name="status">レコードステータス</param>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(ByVal mode As String, ByVal status As String)

        With Me._Frm

        End With

    End Sub

#End Region 'Mode&Status

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .cmbEigyo.TabIndex = LMI140C.CtlTabIndex.EIGYO
            .chkJissekiShoriFlg_1.TabIndex = LMI140C.CtlTabIndex.JISSEKI_SHORI_FLG_1
            .chkJissekiShoriFlg_2.TabIndex = LMI140C.CtlTabIndex.JISSEKI_SHORI_FLG_2
            .imdProcDateFrom.TabIndex = LMI140C.CtlTabIndex.PROC_DATE_FROM
            .imdProcDateTo.TabIndex = LMI140C.CtlTabIndex.PROC_DATE_TO
            .chkProcType_1.TabIndex = LMI140C.CtlTabIndex.PROC_TYPE_1
            .chkProcType_2.TabIndex = LMI140C.CtlTabIndex.PROC_TYPE_2
            .chkProcKbn_1.TabIndex = LMI140C.CtlTabIndex.PROC_KBN_1
            .chkProcKbn_2.TabIndex = LMI140C.CtlTabIndex.PROC_KBN_2
            .sprDetail.TabIndex = LMI140C.CtlTabIndex.SPRDETAILS

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        '自営業所の設定
        Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
        Me._Frm.cmbEigyo.SelectedValue = brCd

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .cmbEigyo.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        '******* 表示列 *******
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.DEF, "  ", 20, True)
        Public Shared JISSEKI_FUYO_NM As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.JISSEKI_FUYO_NM, "実績要否", 80, True)
        Public Shared JISSEKI_SHORI_FLG_NM As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.JISSEKI_SHORI_FLG_NM, "ステータス", 100, True)
        Public Shared PROC_TYPE_NM As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.PROC_TYPE_NM, "処理タイプ", 100, True)
        Public Shared PROC_KBN_NM As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.PROC_KBN_NM, "処理区分", 120, True)
        Public Shared PROC_DATE As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.PROC_DATE, "処理日", 90, True)
        Public Shared OUTKA_WH_TYPE_NM As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.OUTKA_WH_TYPE_NM, "出庫倉庫種類", 110, True)
        Public Shared INKA_WH_TYPE_NM As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.INKA_WH_TYPE_NM, "入庫倉庫種類", 110, True)
        Public Shared BEFORE_GOODS_RANK_NM As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.BEFORE_GOODS_RANK_NM, "変更前商品ランク", 120, True)
        Public Shared AFTER_GOODS_RANK_NM As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.AFTER_GOODS_RANK_NM, "変更後商品ランク", 120, True)
        Public Shared GOODS_CD As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.GOODS_CD, "商品CD", 90, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.GOODS_NM, "商品名", 200, True)
        Public Shared NB As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.NB, "個数", 90, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.LOT_NO, "LOT", 160, True)
        Public Shared LT_DATE As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.LT_DATE, "使用期限", 90, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.REMARK, "明細摘要", 200, True)
        Public Shared NRS_PROC_NO As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.NRS_PROC_NO, "NRS処理番号", 100, True)

        '******* 隠し列 *******
        Public Shared DEL_KB As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.DEL_KB, "(削除区分)", 0, False)
        Public Shared CRT_DATE As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.CRT_DATE, "(データ受信日)", 0, False)
        Public Shared FILE_NAME As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.FILE_NAME, "(受信ファイル名)", 0, False)
        Public Shared REC_NO As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.REC_NO, "(レコード番号)", 0, False)
        Public Shared GYO_NO As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.GYO_NO, "(行)", 0, False)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.NRS_BR_CD, "(営業所コード)", 0, False)
        Public Shared PROC_TYPE As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.PROC_TYPE, "(処理タイプ)", 0, False)
        Public Shared PROC_KBN As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.PROC_KBN, "(処理区分)", 0, False)
        Public Shared JISSEKI_FUYO As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.JISSEKI_FUYO, "(実績要否)", 0, False)
        Public Shared OUTKA_WH_TYPE As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.OUTKA_WH_TYPE, "(出庫倉庫種類)", 0, False)
        Public Shared BEFORE_GOODS_RANK As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.BEFORE_GOODS_RANK, "(変更前商品ランク)", 0, False)
        Public Shared AFTER_GOODS_RANK As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.AFTER_GOODS_RANK, "(変更後商品ランク)", 0, False)
        Public Shared YOBI1 As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.YOBI1, "(予備１)", 0, False)
        Public Shared YOBI2 As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.YOBI2, "(予備２)", 0, False)
        Public Shared YOBI3 As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.YOBI3, "(予備３)", 0, False)
        Public Shared YOBI4 As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.YOBI4, "(予備４)", 0, False)
        Public Shared YOBI5 As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.YOBI5, "(予備５)", 0, False)
        Public Shared JISSEKI_SHORI_FLG As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.JISSEKI_SHORI_FLG, "(実績処理フラグ)", 0, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.SYS_UPD_DATE, "(更新日付)", 0, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI140C.SprColumnIndex.SYS_UPD_TIME, "(更新時刻)", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail

        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMI140C.SprColumnIndex.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMI140G.sprDetailDef())

            '列固定位置
            '.ActiveSheet.FrozenColumnCount = LMI140G.sprDetailDef.DEF.ColNo + 1

            '検索行の設定を行う
            Dim sLbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim sTxt As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, False)
            Dim sKbnB047 As StyleInfo = LMSpreadUtility.GetComboCellMaster(spr _
                , LMConst.CacheTBL.KBN _
                , "KBN_NM1" _
                , "REM" _
                , False _
                , New String() {"KBN_GROUP_CD"} _
                , New String() {"B047"} _
                , LMConst.JoinCondition.AND_WORD
                )
            Dim sCustCond As StyleInfo = LMSpreadUtility.GetComboCellMaster(spr _
                , LMConst.CacheTBL.CUSTCOND _
                , "JOTAI_CD" _
                , "JOTAI_NM" _
                , False _
                , New String() {"NRS_BR_CD", "CUST_CD_L"} _
                , New String() {Convert.ToString(Me._Frm.cmbEigyo.SelectedValue()), "00294"} _
                , LMConst.JoinCondition.AND_WORD
                )

            '**** 表示列 ****
            .SetCellStyle(0, LMI140G.sprDetailDef.DEF.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.JISSEKI_FUYO_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "WZ03", False))
            .SetCellStyle(0, LMI140G.sprDetailDef.JISSEKI_SHORI_FLG_NM.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.PROC_TYPE_NM.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.PROC_KBN_NM.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.PROC_DATE.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.OUTKA_WH_TYPE_NM.ColNo, sKbnB047)
            .SetCellStyle(0, LMI140G.sprDetailDef.INKA_WH_TYPE_NM.ColNo, sKbnB047)
            .SetCellStyle(0, LMI140G.sprDetailDef.BEFORE_GOODS_RANK_NM.ColNo, sCustCond)
            .SetCellStyle(0, LMI140G.sprDetailDef.AFTER_GOODS_RANK_NM.ColNo, sCustCond)
            .SetCellStyle(0, LMI140G.sprDetailDef.GOODS_CD.ColNo, sTxt)
            .SetCellStyle(0, LMI140G.sprDetailDef.GOODS_NM.ColNo, sTxt)
            .SetCellStyle(0, LMI140G.sprDetailDef.NB.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.LOT_NO.ColNo, sTxt)
            .SetCellStyle(0, LMI140G.sprDetailDef.LT_DATE.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.REMARK.ColNo, sTxt)
            .SetCellStyle(0, LMI140G.sprDetailDef.NRS_PROC_NO.ColNo, sTxt)
            '**** 隠し列 ****
            .SetCellStyle(0, LMI140G.sprDetailDef.DEL_KB.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.CRT_DATE.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.FILE_NAME.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.REC_NO.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.GYO_NO.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.NRS_BR_CD.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.PROC_TYPE.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.PROC_KBN.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.JISSEKI_FUYO.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.OUTKA_WH_TYPE.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.BEFORE_GOODS_RANK.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.AFTER_GOODS_RANK.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.YOBI1.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.YOBI2.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.YOBI3.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.YOBI4.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.YOBI5.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.JISSEKI_SHORI_FLG.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.SYS_UPD_DATE.ColNo, sLbl)
            .SetCellStyle(0, LMI140G.sprDetailDef.SYS_UPD_TIME.ColNo, sLbl)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpread = Me._Frm.sprDetail

        With spr

            'データ挿入
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                Exit Sub
            End If

            .SuspendLayout()

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999999, True, 0, , ",")

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, LMI140G.sprDetailDef.DEF.ColNo, def)
                .SetCellStyle(i, LMI140G.sprDetailDef.JISSEKI_FUYO_NM.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.JISSEKI_SHORI_FLG_NM.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.PROC_TYPE_NM.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.PROC_KBN_NM.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.PROC_DATE.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.OUTKA_WH_TYPE_NM.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.INKA_WH_TYPE_NM.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.BEFORE_GOODS_RANK_NM.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.AFTER_GOODS_RANK_NM.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.GOODS_CD.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.GOODS_NM.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.NB.ColNo, sNum)
                .SetCellStyle(i, LMI140G.sprDetailDef.LOT_NO.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.LT_DATE.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.REMARK.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.NRS_PROC_NO.ColNo, sLbl)
                '**** 隠し列 ****
                .SetCellStyle(i, LMI140G.sprDetailDef.PROC_KBN.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.JISSEKI_FUYO.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.JISSEKI_SHORI_FLG.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.SYS_UPD_DATE.ColNo, sLbl)
                .SetCellStyle(i, LMI140G.sprDetailDef.SYS_UPD_TIME.ColNo, sLbl)

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, LMI140G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMI140G.sprDetailDef.JISSEKI_FUYO_NM.ColNo, dr.Item("JISSEKI_FUYO_NM").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.JISSEKI_SHORI_FLG_NM.ColNo, dr.Item("JISSEKI_SHORI_FLG_NM").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.PROC_TYPE_NM.ColNo, dr.Item("PROC_TYPE_NM").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.PROC_KBN_NM.ColNo, dr.Item("PROC_KBN_NM").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.PROC_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("PROC_DATE").ToString()))
                .SetCellValue(i, LMI140G.sprDetailDef.OUTKA_WH_TYPE_NM.ColNo, dr.Item("OUTKA_WH_TYPE_NM").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.INKA_WH_TYPE_NM.ColNo, dr.Item("INKA_WH_TYPE_NM").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.BEFORE_GOODS_RANK_NM.ColNo, dr.Item("BEFORE_GOODS_RANK_NM").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.AFTER_GOODS_RANK_NM.ColNo, dr.Item("AFTER_GOODS_RANK_NM").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.GOODS_CD.ColNo, dr.Item("GOODS_CD").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.NB.ColNo, dr.Item("NB").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.LOT_NO.ColNo, dr.Item("LOT_NO").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.LT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("LT_DATE").ToString()))
                .SetCellValue(i, LMI140G.sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.NRS_PROC_NO.ColNo, dr.Item("NRS_PROC_NO").ToString())
                '**** 隠し列 ****
                .SetCellValue(i, LMI140G.sprDetailDef.DEL_KB.ColNo, dr.Item("DEL_KB").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.CRT_DATE.ColNo, dr.Item("CRT_DATE").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.FILE_NAME.ColNo, dr.Item("FILE_NAME").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.REC_NO.ColNo, dr.Item("REC_NO").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.GYO_NO.ColNo, dr.Item("GYO_NO").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.PROC_TYPE.ColNo, dr.Item("PROC_TYPE").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.PROC_KBN.ColNo, dr.Item("PROC_KBN").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.JISSEKI_FUYO.ColNo, dr.Item("JISSEKI_FUYO").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.OUTKA_WH_TYPE.ColNo, dr.Item("OUTKA_WH_TYPE").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.BEFORE_GOODS_RANK.ColNo, dr.Item("BEFORE_GOODS_RANK").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.AFTER_GOODS_RANK.ColNo, dr.Item("AFTER_GOODS_RANK").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.YOBI1.ColNo, dr.Item("YOBI1").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.YOBI2.ColNo, dr.Item("YOBI2").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.YOBI3.ColNo, dr.Item("YOBI3").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.YOBI4.ColNo, dr.Item("YOBI4").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.YOBI5.ColNo, dr.Item("YOBI5").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.JISSEKI_SHORI_FLG.ColNo, dr.Item("JISSEKI_SHORI_FLG").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMI140G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#End Region

End Class
