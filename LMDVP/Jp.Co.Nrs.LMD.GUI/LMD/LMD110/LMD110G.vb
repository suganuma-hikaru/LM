' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD110F : 在庫振替検索
'  作  成  者       :  daikoku
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMD110Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD110G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD110F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD110F)

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
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = "マスタ参照"
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
            .F10ButtonEnabled = always
            .F11ButtonEnabled = False
            .F12ButtonEnabled = always

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

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
            .imdFurikaeDate_S.TabIndex = LMD110C.CtlTabIndex_MAIN.FURIKAE_DATE_S
            .imdFurikaeDate_E.TabIndex = LMD110C.CtlTabIndex_MAIN.FURIKAE_DATE_E
            .txtMotoCustCD_L.TabIndex = LMD110C.CtlTabIndex_MAIN.MOTO_CUST_CD_L
            .txtMotoCustCD_M.TabIndex = LMD110C.CtlTabIndex_MAIN.MOTO_CUST_CD_M
            .txtSakiCustCD_L.TabIndex = LMD110C.CtlTabIndex_MAIN.SAKI_CUST_CD_L
            .txtSakiCustCD_M.TabIndex = LMD110C.CtlTabIndex_MAIN.SAKI_CUST_CD_M

            .cmFurikaeKBN.TabIndex = LMD110C.CtlTabIndex_MAIN.FURIKAE_KBN
            .cmYoukiKBN.TabIndex = LMD110C.CtlTabIndex_MAIN.YOUKI_HENKO
            .chkSelectByNrsB.TabIndex = LMD110C.CtlTabIndex_MAIN.MY_CREATE

            .cmbEigyo.TabIndex = LMD110C.CtlTabIndex_MAIN.NRS_BR_CD
            .cmbWare.TabIndex = LMD110C.CtlTabIndex_MAIN.WH_CD

        End With

    End Sub

    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMD110F)

        '=== TODO : 初期荷主取得仕様決定後　修正になる可能性あり ==='

        '初期荷主情報取得
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST). _
        Select("SYS_DEL_FLG = '0' AND USER_CD = '" & _
               LM.Base.LMUserInfoManager.GetUserID() & "' AND DEFAULT_CUST_YN = '01'")

        '初期値が存在するコントロール
        frm.cmbEigyo.SelectedValue() = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()     '（自）営業所
        frm.cmbWare.SelectedValue() = LM.Base.LMUserInfoManager.GetWhCd().ToString()         '（自）倉庫

        '2014.08.04 FFEM高取対応 START
        'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

        If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
            Me._Frm.cmbEigyo.ReadOnly = True
        Else
            Me._Frm.cmbEigyo.ReadOnly = False
        End If

        If getDr.Length() > 0 Then
            frm.txtMotoCustCD_L.TextValue = getDr(0).Item("CUST_CD_L").ToString()                   '（初期）荷主コード（大）")
            frm.lblMotoCustNM_L.TextValue = getDr(0).Item("CUST_NM_L").ToString()                   '（初期）荷主名（大）
            frm.txtMotoCustCD_M.TextValue = getDr(0).Item("CUST_CD_M").ToString()                   '（初期）荷主コード（中）")
            frm.lblMotoCustNM_M.TextValue = getDr(0).Item("CUST_NM_M").ToString()                   '（初期）荷主名（中）
        End If

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = LMD110C.MODE_DEFAULT)

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

            .imdFurikaeDate_S.Focus()
        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey(LMD110C.MODE_DEFAULT)
        Call Me.SetControlsStatus()

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef
        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.DEF, " ", 20, True)                               '選択列
        Public Shared ORDER_NO As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.ORDER_NO, "オーダー番号", 120, True)       'オーダー番号
        Public Shared FURIKAE_DATE As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.FURIKAE_DATE, "振替日", 80, True)       '振替日
        Public Shared FURI_NO As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.FURI_NO, "振替管理番号", 100, True)          '振替管理番号
        Public Shared FURI_MM As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.FURI_NM, "振替区分", 100, True)              '振替区分
        Public Shared YOUKI_NM As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.YOUKI_NM, "容器変更", 80, True)                '容器変更
        Public Shared MOTO_CUST_NM As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.MOTO_CUST_NM, "振替元荷主名", 160, True)       '振替元荷主名称
        Public Shared SAKI_CUST_NM As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.SAKI_CUST_NM, "振替先荷主名", 160, True)       '振替先荷主名称
        Public Shared SAKI_GOODS_NM As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.SAKI_GOODS_NM, "振替先商品名", 160, True)     '振替先商品名
        Public Shared OUTKA_NO_L As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.OUTKA_NO_L, "出荷管理番号", 90, True)             '出荷管理番号
        Public Shared INKA_NO_L As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.INKA_NO_L, "入荷管理番号", 90, True)             '入荷管理番号

        Public Shared UPT_NM As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.UPT_NM, "更新者", 100, True)                  '更新者
        Public Shared UPD_DATE As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.UPD_DATE, "更新日", 80, True)               '更新日
        Public Shared MOTO_TANTO_NM As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.MU_NM, "振替元担当者", 100, True)          '元担当者ユーザー名
        Public Shared SAKI_TANTO_NM As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.SU_NM, "振替先担当者", 100, True)          '先担当者ユーザー名

        'invisible
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.NRS_BR_CD, "営業所コード", 100, False)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.WH_CD, "倉庫コード", 70, False)
        Public Shared HAITA_UPD_TM As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.HAITA_UPD_TM, "更新時間", 80, False)     '未編集更新時間(排他用) FURI_SYS_ENT_DATE
        Public Shared FURI_SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.FURI_SYS_ENT_DATE, "作成日", 80, False)     '振替作成日
        Public Shared OUT_UP_DT As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.OUT_UP_DT, "更新時間", 80, False)           '出荷更新日時
        Public Shared IN_UP_DT As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.IN_UP_DT, "更新時間", 80, False)             '入荷更新日時
        Public Shared FURI_KBN As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.FURI_KBN, "振替区分", 100, False)              '振替区分
        Public Shared YOUKI_HENKO_KBN As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.YOUKI_HENKO_KBN, "容器区分", 100, False)  '容器区分
        Public Shared OUT_TAX_KB As SpreadColProperty = New SpreadColProperty(LMD110C.SprColumnIndex.OUT_TAX_KB, "税区分", 100, False)        '税区分
    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprFurrikae.CrearSpread()

            '列数設定
            .sprFurrikae.Sheets(0).ColumnCount = 24

            '英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprSagyo.SetColProperty(New sprDetailDef)
            .sprFurrikae.SetColProperty(New LMD110G.sprDetailDef(), False)
            '英語化対応END

            '列固定位置を設定します。(ex.ロット番号で固定)
            .sprFurrikae.Sheets(0).FrozenColumnCount = sprDetailDef.FURI_NO.ColNo + 1

            '列設定

            .sprFurrikae.SetCellStyle(0, sprDetailDef.ORDER_NO.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX_IME_OFF, 20, False))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.FURIKAE_DATE.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_HANKAKU, 40, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.FURI_NO.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.HAN_NUM_ALPHA, 8, False))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.FURI_MM.ColNo, LMSpreadUtility.GetNumberCell(.sprFurrikae, 0, 9999999999, True, 0, True, ","))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.YOUKI_NM.ColNo, LMSpreadUtility.GetNumberCell(.sprFurrikae, 0, 9999999999.999, True, 2, True, ","))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.MOTO_CUST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 40, False))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.SAKI_CUST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 40, False))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.SAKI_GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 60, False))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.UPT_NM.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX_IME_OFF, 7, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.OUTKA_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 60, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.INKA_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 60, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.UPD_DATE.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 60, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.MOTO_TANTO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 60, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.SAKI_TANTO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 60, True))

            'invisible
            .sprFurrikae.SetCellStyle(0, sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 2, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.WH_CD.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 3, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.HAITA_UPD_TM.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 20, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.FURI_SYS_ENT_DATE.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 20, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.OUT_UP_DT.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 20, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.IN_UP_DT.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 20, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.FURI_KBN.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 20, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.YOUKI_HENKO_KBN.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 20, True))
            .sprFurrikae.SetCellStyle(0, sprDetailDef.OUT_TAX_KB.ColNo, LMSpreadUtility.GetTextCell(.sprFurrikae, InputControl.ALL_MIX, 20, True))
        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMD110F)


        With frm.sprFurrikae

            .Sheets(0).Cells(0, sprDetailDef.ORDER_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.FURIKAE_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.FURI_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.FURI_MM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.YOUKI_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.MOTO_CUST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SAKI_CUST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SAKI_GOODS_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.UPT_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.UPD_DATE.ColNo).Value = String.Empty

        End With

    End Sub


    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>

    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprFurrikae

        With spr

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count()
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count(), lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sLabeC As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Center)

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定

                '*****表示列*****
                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.ORDER_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.FURIKAE_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.FURI_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.FURI_MM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.YOUKI_NM.ColNo, sLabeC)
                .SetCellStyle(i, sprDetailDef.MOTO_CUST_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAKI_CUST_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAKI_GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OUTKA_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INKA_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UPT_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.MOTO_TANTO_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAKI_TANTO_NM.ColNo, sLabel)

                ''セル値設定
                ''*****表示列*****
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.ORDER_NO.ColNo, dr.Item("ORDER_NO").ToString())
                .SetCellValue(i, sprDetailDef.FURIKAE_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("FURI_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.FURI_NO.ColNo, dr.Item("FURI_NO").ToString())
                .SetCellValue(i, sprDetailDef.FURI_MM.ColNo, dr.Item("FURI_NM").ToString())
                .SetCellValue(i, sprDetailDef.YOUKI_NM.ColNo, dr.Item("YOUKI_NM").ToString())
                .SetCellValue(i, sprDetailDef.MOTO_CUST_NM.ColNo, dr.Item("MOTO_CUST_NM").ToString())
                .SetCellValue(i, sprDetailDef.SAKI_CUST_NM.ColNo, dr.Item("SAKI_CUST_NM").ToString())
                .SetCellValue(i, sprDetailDef.SAKI_GOODS_NM.ColNo, dr.Item("SAKI_GOODS_NM").ToString())
                .SetCellValue(i, sprDetailDef.OUTKA_NO_L.ColNo, dr.Item("OUTKA_NO_L").ToString())
                .SetCellValue(i, sprDetailDef.INKA_NO_L.ColNo, dr.Item("INKA_NO_L").ToString())

                .SetCellValue(i, sprDetailDef.UPT_NM.ColNo, dr.Item("UPD_USER_NM").ToString())
                '.SetCellValue(i, sprDetailDef.UPD_DATE.ColNo, dr.Item("UPD_DATE").ToString())
                .SetCellValue(i, sprDetailDef.UPD_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("UPD_DATE").ToString()))     '更新日
                .SetCellValue(i, sprDetailDef.MOTO_TANTO_NM.ColNo, dr.Item("MOTO_USER_NM").ToString())
                .SetCellValue(i, sprDetailDef.SAKI_TANTO_NM.ColNo, dr.Item("SAKI_USER_NM").ToString())

                ''*****隠し列*****
                .SetCellValue(i, sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprDetailDef.WH_CD.ColNo, dr.Item("WH_CD").ToString())
                .SetCellValue(i, sprDetailDef.HAITA_UPD_TM.ColNo, dr.Item("UP_DT").ToString())
                .SetCellValue(i, sprDetailDef.FURI_SYS_ENT_DATE.ColNo, dr.Item("FURI_SYS_ENT_DATE").ToString())
                .SetCellValue(i, sprDetailDef.OUT_UP_DT.ColNo, dr.Item("OUT_UP_DT").ToString())
                .SetCellValue(i, sprDetailDef.IN_UP_DT.ColNo, dr.Item("IN_UP_DT").ToString())
                .SetCellValue(i, sprDetailDef.FURI_KBN.ColNo, dr.Item("FURI_KBN").ToString())
                .SetCellValue(i, sprDetailDef.YOUKI_HENKO_KBN.ColNo, dr.Item("YOUKI_HENKO_KBN").ToString())
                .SetCellValue(i, sprDetailDef.OUT_TAX_KB.ColNo, dr.Item("TAX_KBN").ToString())
            Next

            .ResumeLayout(True)

        End With

    End Sub


    ''' <summary>
    ''' コロン編集した時刻を取得
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String
        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))
    End Function

#End Region 'Spread

#End Region

End Class
