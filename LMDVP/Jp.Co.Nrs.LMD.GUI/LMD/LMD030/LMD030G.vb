' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMD     : 在庫
'  プログラムID   : LMD030G : 在庫履歴
'  作  成  者     : kishi
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMD030Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD030G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD030F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconG As LMDControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD030F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm
        Me._Frm = frm

        'Gamen共通クラスの設定
        Me._LMDconG = New LMDControlG(MyBase.MyHandler, DirectCast(frm, Form))

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' 
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal mode As String)
        Dim always As Boolean = True

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = "削　除"
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = always
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = always
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
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

            ''Main
            .chkSyukkaDelShow.TabIndex = LMD030C.CtlTabIndex_MAIN.CHKSYUKKADELSHOW
            .optCntShow.TabIndex = LMD030C.CtlTabIndex_MAIN.OPTCNTSHOW
            .optAmtShow.TabIndex = LMD030C.CtlTabIndex_MAIN.OPTAMTSHOW
            .sprDetail.TabIndex = LMD030C.CtlTabIndex_MAIN.SPRDETAIL

            'TabStop

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByVal ds As DataSet)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コントロールに初期値設定
        Call Me.SetInitControl(id, ds)

    End Sub

    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMD030C.TABLE_NM_IN).Rows(0)

        '初期値が存在するコントロール
        Me._Frm.lblCustCD_L.TextValue() = dr.Item("CUST_CD_L").ToString()                       '荷主コード(大）
        Me._Frm.lblCustCD_M.TextValue() = dr.Item("CUST_CD_M").ToString()                       '荷主コード(中）
        Me._Frm.lblCustNM_L.TextValue() = dr.Item("CUST_NM").ToString()                         '荷主名（大）
        Me._Frm.lblGoodsCD.TextValue() = dr.Item("GOODS_CD_CUST").ToString()                    '荷主商品コード
        Me._Frm.lblGoodsCDNrs.TextValue() = dr.Item("GOODS_CD_NRS").ToString()                  '商品key（日陸商品コード）：隠し
        Me._Frm.lblNrsBrCd.TextValue() = dr.Item("NRS_BR_CD").ToString()                        '営業所コード：隠し
        Me._Frm.lblGoodsNM.TextValue() = dr.Item("GOODS_NM_1").ToString()                       '商品名
        Me._Frm.lblLotNO.TextValue() = dr.Item("LOT_NO").ToString()                             'ロット№
        Me._Frm.lblIrime.TextValue() = dr.Item("IRIME").ToString()                              '入目
        Me._Frm.lblZaiRecNO.TextValue() = dr.Item("ZAI_REC_NO").ToString()                      '在庫レコード番号
        Me._Frm.lblFinalInvDate.TextValue() = dr.Item("HOKAN_NIYAKU_CALCULATION").ToString()    '最終請求日
        Me._Frm.lblFinalSyukkaDate.TextValue() = dr.Item("LAST_OUTKO_DATE").ToString()          '最終出荷日
        Me._Frm.chkSyukkaDelShow.Checked() = False                                              '出荷取消を表示する
        Me._Frm.optCntShow.Checked() = True                                                     '個数表示

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey(LMD030C.MODE_DEFAULT)
        Call Me.SetControlsStatus()

    End Sub

#Region "未使用"

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = LMD030C.MODE_DEFAULT)

        '**********TODO:削除予定　画面モードによるロック等がある場合は、画面モードを引数でもらい処理を行う。*********************
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

        With Me._Frm.sprDetail

            'ここに各項目のクリア命令を書く

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

#End Region '未使用'

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared STATE_KB As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.STATE_KB, " ", 25, True)
        Public Shared SYUBETU As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.SYUBETU, "種別", 38, True)
        Public Shared IDO_DATE As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.IDO_DATE, "日付", 80, True)
        Public Shared INKA_NB As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.INKA_NB, "入荷個数", 80, True)
        Public Shared INKA_QT As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.INKA_QT, "入荷数量", 80, False)                 '入荷数量（切替）
        Public Shared OUTKA_NB As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.OUTKA_NB, "出荷個数", 80, True)
        Public Shared OUTKA_QT As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.OUTKA_QT, "出荷数量", 80, False)               '出荷数量（切替）
        Public Shared BACKLOG_NB As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.BACKLOG_NB, "残個数", 80, True)
        Public Shared BACKLOG_QT As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.BACKLOG_QT, "残数量", 80, False)             '残数量（切替）
        Public Shared PKG_UT As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.PKG_UT, "単位", 40, True)
        Public Shared STD_IRIME_UT As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.STD_IRIME_UT, "単位", 40, False)           '単位（切替）
        Public Shared TOU_NO As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.TOU_NO, "棟", 40, True)
        Public Shared SITU_NO As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.SITU_NO, "室", 30, True)
        Public Shared ZONE_CD As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.ZONE_CD, "ZONE", 40, True)
        Public Shared LOCA As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.LOCA, "ロケーション", 120, True)
        Public Shared INOUTKA_NO_L As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.INOUTKA_NO_L, "管理番号", 120, True)
        Public Shared O_ZAI_REC_NO As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.O_ZAI_REC_NO, "元在庫" & vbCrLf & "レコード番号", 120, True)
        Public Shared N_ZAI_REC_NO As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.N_ZAI_REC_NO, "先在庫" & vbCrLf & "レコード番号", 120, True)
        Public Shared REMARK_KBN As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.REMARK_KBN, "出荷先", 180, True)
        Public Shared CUST_ORD_NO As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.CUST_ORD_NO, "オーダー番号", 100, True)
        Public Shared BUYER_ORD_NO As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.BUYER_ORD_NO, "注文番号", 100, True)
        Public Shared UNSOCO_NM As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.UNSOCO_NM, "運送会社名", 145, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.REMARK, "備考小(社内)", 120, True)
        Public Shared REMARK_OUT As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.REMARK_OUT, "備考小(社外)", 120, True)
        Public Shared GOODS_COND_NM_1 As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.GOODS_COND_NM_1, "状態 中身", 80, True)
        Public Shared GOODS_COND_NM_2 As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.GOODS_COND_NM_2, "状態 外観", 80, True)
        Public Shared GOODS_COND_NM_3 As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.GOODS_COND_NM_3, "状態 荷主", 80, True)
        Public Shared SPD_KB_NM As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.SPD_KB_NM, "保留品", 60, True)
        Public Shared OFB_KB_NM As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.OFB_KB_NM, "簿外品", 60, True)
        Public Shared ALLOC_PRIORITY_NM As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.ALLOC_PRIORITY_NM, "引当優先度", 85, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.DEST_NM, "届先名", 80, True)
        Public Shared RSV_NO As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.RSV_NO, "予約番号", 80, True)
        Public Shared SORT_KEY As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.SORT_KEY, "順番", 40, True)

        'invisible
        Public Shared STD_IRIME_NB As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.STD_IRIME_NB, "入目", 60, False)
        Public Shared PORA_ZAI_NB As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.PORA_ZAI_NB, "実予在庫梱数", 60, False)
        Public Shared ALLOC_CAN_NB As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.ALLOC_CAN_NB, "引当可能梱数", 60, False)
        Public Shared IDO_SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.IDO_SYS_UPD_DATE, "更新日", 60, False)
        Public Shared IDO_SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.IDO_SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared O_ZAI_SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.O_ZAI_SYS_UPD_DATE, "更新日", 60, False)
        Public Shared O_ZAI_SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.O_ZAI_SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared N_ZAI_SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.N_ZAI_SYS_UPD_DATE, "更新日", 60, False)
        Public Shared N_ZAI_SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.N_ZAI_SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared HOKAN_SEIQTO_CD As SpreadColProperty = New SpreadColProperty(LMD030C.SprColumnIndex.HOKAN_SEIQTO_CD, "保管料請求先", 60, False)


    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.Sheets(0).ColumnCount = 44

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New sprDetailDef)
            .sprDetail.SetColProperty(New sprDetailDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(チェック列で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMD030G.sprDetailDef.DEF.ColNo + 1

            Dim spr As LMSpread = Me._Frm.sprDetail

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(.sprDetail, False)
            Dim lLabel As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left)
            Dim rLabel As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Right)

            '列設定
            .sprDetail.SetCellStyle(-1, sprDetailDef.DEF.ColNo, sDEF)
            .sprDetail.SetCellStyle(-1, sprDetailDef.STATE_KB.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.SYUBETU.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.IDO_DATE.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.INKA_NB.ColNo, rLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.INKA_QT.ColNo, rLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.OUTKA_NB.ColNo, rLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.OUTKA_QT.ColNo, rLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.BACKLOG_NB.ColNo, rLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.BACKLOG_QT.ColNo, rLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.PKG_UT.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.STD_IRIME_UT.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.TOU_NO.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.SITU_NO.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.ZONE_CD.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.LOCA.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.INOUTKA_NO_L.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.O_ZAI_REC_NO.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.N_ZAI_REC_NO.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.REMARK_KBN.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.CUST_ORD_NO.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.BUYER_ORD_NO.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.UNSOCO_NM.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.REMARK.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.REMARK_OUT.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.GOODS_COND_NM_1.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.GOODS_COND_NM_2.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.GOODS_COND_NM_3.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.SPD_KB_NM.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.OFB_KB_NM.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.ALLOC_PRIORITY_NM.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.DEST_NM.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.RSV_NO.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.SORT_KEY.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.STD_IRIME_NB.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.PORA_ZAI_NB.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.ALLOC_CAN_NB.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.IDO_SYS_UPD_DATE.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.IDO_SYS_UPD_TIME.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.O_ZAI_SYS_UPD_DATE.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.O_ZAI_SYS_UPD_TIME.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.N_ZAI_SYS_UPD_DATE.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.N_ZAI_SYS_UPD_TIME.ColNo, lLabel)
            .sprDetail.SetCellStyle(-1, sprDetailDef.HOKAN_SEIQTO_CD.ColNo, lLabel)

        End With

    End Sub

    ''' <summary>
    ''' 自動ソート設定ロック
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSortLock()

        'Me._Frm.sprDetail.Sheets(0).AutoSortColumn(1, False)

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim dt As DataTable = ds.Tables(LMD030C.TABLE_NM_OUT)

        With spr

            .SuspendLayout()

            '----データ挿入----'

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count()
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim rLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
            'Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ",")

            Dim dr As DataRow

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dr = dt.Rows(i)

                'セルスタイル設定
                spr.SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                spr.SetCellStyle(i, sprDetailDef.STATE_KB.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.SYUBETU.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.IDO_DATE.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.INKA_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))
                spr.SetCellStyle(i, sprDetailDef.INKA_QT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))
                spr.SetCellStyle(i, sprDetailDef.OUTKA_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))
                spr.SetCellStyle(i, sprDetailDef.OUTKA_QT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))
                spr.SetCellStyle(i, sprDetailDef.BACKLOG_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))
                spr.SetCellStyle(i, sprDetailDef.BACKLOG_QT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))
                spr.SetCellStyle(i, sprDetailDef.PKG_UT.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.STD_IRIME_UT.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.TOU_NO.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.SITU_NO.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.ZONE_CD.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.LOCA.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.INOUTKA_NO_L.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.O_ZAI_REC_NO.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.N_ZAI_REC_NO.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.REMARK_KBN.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.CUST_ORD_NO.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.BUYER_ORD_NO.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.UNSOCO_NM.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.REMARK.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.REMARK_OUT.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.GOODS_COND_NM_1.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.GOODS_COND_NM_2.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.GOODS_COND_NM_3.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.SPD_KB_NM.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.OFB_KB_NM.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.ALLOC_PRIORITY_NM.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.DEST_NM.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.RSV_NO.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.SORT_KEY.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.STD_IRIME_NB.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.PORA_ZAI_NB.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.ALLOC_CAN_NB.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.IDO_SYS_UPD_DATE.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.IDO_SYS_UPD_TIME.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.O_ZAI_SYS_UPD_DATE.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.O_ZAI_SYS_UPD_TIME.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.N_ZAI_SYS_UPD_DATE.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.N_ZAI_SYS_UPD_TIME.ColNo, lLabel)
                spr.SetCellStyle(i, sprDetailDef.HOKAN_SEIQTO_CD.ColNo, lLabel)

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.STATE_KB.ColNo, dr.Item("STATE_KB").ToString())
                .SetCellValue(i, sprDetailDef.SYUBETU.ColNo, dr.Item("SYUBETU").ToString())
                .SetCellValue(i, sprDetailDef.IDO_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("IDO_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.INKA_NB.ColNo, dr.Item("INKA_NB").ToString())
                .SetCellValue(i, sprDetailDef.INKA_QT.ColNo, dr.Item("INKA_QT").ToString())
                .SetCellValue(i, sprDetailDef.OUTKA_NB.ColNo, dr.Item("OUTKA_NB").ToString())
                .SetCellValue(i, sprDetailDef.OUTKA_QT.ColNo, dr.Item("OUTKA_QT").ToString())
                .SetCellValue(i, sprDetailDef.BACKLOG_NB.ColNo, dr.Item("BACKLOG_NB").ToString())
                .SetCellValue(i, sprDetailDef.BACKLOG_QT.ColNo, dr.Item("BACKLOG_QT").ToString())
                .SetCellValue(i, sprDetailDef.PKG_UT.ColNo, dr.Item("PKG_UT").ToString())
                .SetCellValue(i, sprDetailDef.STD_IRIME_UT.ColNo, dr.Item("STD_IRIME_UT").ToString())
                .SetCellValue(i, sprDetailDef.TOU_NO.ColNo, dr.Item("TOU_NO").ToString())
                .SetCellValue(i, sprDetailDef.SITU_NO.ColNo, dr.Item("SITU_NO").ToString())
                .SetCellValue(i, sprDetailDef.ZONE_CD.ColNo, dr.Item("ZONE_CD").ToString())
                .SetCellValue(i, sprDetailDef.LOCA.ColNo, dr.Item("LOCA").ToString())
                .SetCellValue(i, sprDetailDef.INOUTKA_NO_L.ColNo, dr.Item("INOUTKA_NO_L").ToString())
                .SetCellValue(i, sprDetailDef.O_ZAI_REC_NO.ColNo, dr.Item("O_ZAI_REC_NO").ToString())
                .SetCellValue(i, sprDetailDef.N_ZAI_REC_NO.ColNo, dr.Item("N_ZAI_REC_NO").ToString())
                .SetCellValue(i, sprDetailDef.REMARK_KBN.ColNo, dr.Item("REMARK_KBN").ToString())
                .SetCellValue(i, sprDetailDef.CUST_ORD_NO.ColNo, dr.Item("CUST_ORD_NO").ToString())
                .SetCellValue(i, sprDetailDef.BUYER_ORD_NO.ColNo, dr.Item("BUYER_ORD_NO").ToString())
                .SetCellValue(i, sprDetailDef.UNSOCO_NM.ColNo, dr.Item("UNSOCO_NM").ToString())
                .SetCellValue(i, sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, sprDetailDef.REMARK_OUT.ColNo, dr.Item("REMARK_OUT").ToString())
                .SetCellValue(i, sprDetailDef.GOODS_COND_NM_1.ColNo, dr.Item("GOODS_COND_NM_1").ToString())
                .SetCellValue(i, sprDetailDef.GOODS_COND_NM_2.ColNo, dr.Item("GOODS_COND_NM_2").ToString())
                .SetCellValue(i, sprDetailDef.GOODS_COND_NM_3.ColNo, dr.Item("GOODS_COND_NM_3").ToString())
                .SetCellValue(i, sprDetailDef.SPD_KB_NM.ColNo, dr.Item("SPD_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.OFB_KB_NM.ColNo, dr.Item("OFB_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.ALLOC_PRIORITY_NM.ColNo, dr.Item("ALLOC_PRIORITY_NM").ToString())
                .SetCellValue(i, sprDetailDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i, sprDetailDef.RSV_NO.ColNo, dr.Item("RSV_NO").ToString())
                .SetCellValue(i, sprDetailDef.SORT_KEY.ColNo, dr.Item("SORT_KEY").ToString())
                .SetCellValue(i, sprDetailDef.STD_IRIME_NB.ColNo, dr.Item("STD_IRIME_NB").ToString())
                .SetCellValue(i, sprDetailDef.PORA_ZAI_NB.ColNo, dr.Item("PORA_ZAI_NB").ToString())
                .SetCellValue(i, sprDetailDef.ALLOC_CAN_NB.ColNo, dr.Item("ALLOC_CAN_NB").ToString())
                .SetCellValue(i, sprDetailDef.IDO_SYS_UPD_DATE.ColNo, dr.Item("IDO_SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprDetailDef.IDO_SYS_UPD_TIME.ColNo, dr.Item("IDO_SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprDetailDef.O_ZAI_SYS_UPD_DATE.ColNo, dr.Item("O_ZAI_SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprDetailDef.O_ZAI_SYS_UPD_TIME.ColNo, dr.Item("O_ZAI_SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprDetailDef.N_ZAI_SYS_UPD_DATE.ColNo, dr.Item("N_ZAI_SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprDetailDef.N_ZAI_SYS_UPD_TIME.ColNo, dr.Item("N_ZAI_SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprDetailDef.HOKAN_SEIQTO_CD.ColNo, dr.Item("HOKAN_SEIQTO_CD").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#End Region

End Class
