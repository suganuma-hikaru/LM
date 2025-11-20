' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI961G : GLIS見積情報照会（ハネウェル）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMI961Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI961G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI961F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconG As LMFControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI961F, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMFconG = g

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
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = "受注作成"
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMIControlC.FUNCTION_KENSAKU
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = always
            .F2ButtonEnabled = lock
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

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

        End With

    End Sub

    ''' <summary>
    ''' F12(閉じる)以外のロック
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LockFunctionKeyExceptF12()

        With Me._Frm.FunctionKey

            .Enabled = True

            'F12(閉じる)以外のロック
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = False
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
            .F12ButtonEnabled = True

        End With

    End Sub

#End Region 'FunctionKey

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .pnlCondition.TabIndex = LMI961C.CtlTabIndex.PNL_CONDITION
            .chkYushutsu.TabIndex = LMI961C.CtlTabIndex.CHK_YUSHUTSU
            .chkYunyu.TabIndex = LMI961C.CtlTabIndex.CHK_YUNYU
            .chkKokunai.TabIndex = LMI961C.CtlTabIndex.CHK_KOKUNAI
            .txtEstNo.TabIndex = LMI961C.CtlTabIndex.EST_NO
            .txtEstNoEda.TabIndex = LMI961C.CtlTabIndex.EST_NO_EDA
            .txtFwdUserNm.TabIndex = LMI961C.CtlTabIndex.FWD_USER_NM
            .txtEstMakeUserNm.TabIndex = LMI961C.CtlTabIndex.EST_MAKE_USER_NM
            .txtGoodsNm.TabIndex = LMI961C.CtlTabIndex.GOODS_NM
            .txtSearchRem.TabIndex = LMI961C.CtlTabIndex.SEARCH_REM
            .pnlSakuseiNaiyo.TabIndex = LMI961C.CtlTabIndex.PNL_SAKUSEI_NAIYO
            .optYuki.TabIndex = LMI961C.CtlTabIndex.OPT_YUKI
            .optKaeri.TabIndex = LMI961C.CtlTabIndex.OPT_KAERI
            .sprDetail.TabIndex = LMI961C.CtlTabIndex.DETAIL

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
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .Focus()
            .chkYushutsu.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .chkYushutsu.Checked = False
            .chkYunyu.Checked = False
            .chkKokunai.Checked = False
            .optYuki.Checked = True
            .txtEstNo.TextValue = String.Empty
            .txtEstNoEda.TextValue = String.Empty
            .txtFwdUserNm.TextValue = String.Empty
            .txtEstMakeUserNm.TextValue = String.Empty
            .txtGoodsNm.TextValue = String.Empty
            .txtSearchRem.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        With Me._Frm

            .chkYushutsu.Checked = False
            .chkYunyu.Checked = False
            .chkKokunai.Checked = False
            .optYuki.Checked = True

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
        '表示項目
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared EST_NO_DISP As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.EST_NO_DISP, "見積番号", 110, True)
        Public Shared JOB_CATEGORY_NM As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.JOB_CATEGORY_NM, "カテゴリ", 60, True)
        Public Shared MOVE_TYPE_NM As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.MOVE_TYPE_NM, "Move Type", 80, True)
        Public Shared SEARCH_REM As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.SEARCH_REM, "見積コメント", 150, True)
        Public Shared PORT_NM As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.PORT_NM, "仕向出地", 100, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.GOODS_NM, "貨物", 200, True)
        Public Shared CONT_SEQ As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.CONT_SEQ, "運送番号", 60, True)
        Public Shared CONT_TRN_NM As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.CONT_TRN_NM, "運送名", 200, True)
        Public Shared PLACE_CD_A_NM As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.PLACE_CD_A_NM, "出荷元", 200, True)
        Public Shared STAR_PLACE_NM As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.STAR_PLACE_NM, "納入先", 200, True)
        Public Shared PLACE_CD_B_NM As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.PLACE_CD_B_NM, "着地", 200, True)
        Public Shared EXPIRY_TO_DATE_FWD As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.EXPIRY_TO_DATE_FWD, "見積期限", 80, True)
        Public Shared FWD_USER_NM As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.FWD_USER_NM, "フォワーダー" & vbCrLf & "担当者", 90, True)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.SYS_ENT_USER_NM, "見積作成者", 90, True)

        '隠し項目
        Public Shared EST_NO As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.EST_NO, "見積番号", 0, False)
        Public Shared EST_NO_EDA As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.EST_NO_EDA, "見積番号枝番", 0, False)
        Public Shared JOB_OUT_EXP_KBN As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.JOB_OUT_EXP_KBN, "Outbound Export Job", 0, False)
        Public Shared JOB_OUT_IMP_KBN As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.JOB_OUT_IMP_KBN, "Outbound Import Job", 0, False)
        Public Shared JOB_IN_EXP_KBN As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.JOB_IN_EXP_KBN, "Inbound Export Job", 0, False)
        Public Shared JOB_IN_IMP_KBN As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.JOB_IN_IMP_KBN, "Inbound Import Job", 0, False)
        Public Shared JOB_LOC_KBN As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.JOB_LOC_KBN, "Local Job", 0, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.SYS_UPD_DATE, "システム更新日", 0, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.SYS_UPD_TIME, "システム更新時間", 0, False)
        Public Shared CONT_LEG_SEQ As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.CONT_LEG_SEQ, "CONT_LEG_SEQ", 0, False)
        Public Shared CARGO_SEQ As SpreadColProperty = New SpreadColProperty(LMI961C.SprColumnIndex.CARGO_SEQ, "CARGO_SEQ", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpread = Me._Frm.sprDetail
        With spr

            'スプレッドの行をクリア
            .CrearSpread()
            .ActiveSheet.Rows.Count = 1

            '列数設定
            .ActiveSheet.ColumnCount = LMI961C.SprColumnIndex.LAST

            .SetColProperty(New LMI961G.sprDetailDef(), False)

            '列固定位置を設定
            .ActiveSheet.FrozenColumnCount = LMI961G.sprDetailDef.EST_NO_DISP.ColNo + 1

            ''セルに設定するスタイルの取得
            'Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)

            ''セルスタイル設定(検索条件)
            '.SetCellStyle(0, LMI961G.sprDetailDef.EST_NO_DISP.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 10, False))
            '.SetCellStyle(0, LMI961G.sprDetailDef.JOB_CATEGORY_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUMBER, 3, False))
            '.SetCellStyle(0, LMI961G.sprDetailDef.MOVE_TYPE_NM.ColNo, sLabel)
            '.SetCellStyle(0, LMI961G.sprDetailDef.SEARCH_REM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 40, False))
            '.SetCellStyle(0, LMI961G.sprDetailDef.PORT_NM.ColNo, sLabel)
            '.SetCellStyle(0, LMI961G.sprDetailDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 100, False))
            '.SetCellStyle(0, LMI961G.sprDetailDef.STAR_PLACE_NM.ColNo, sLabel)
            '.SetCellStyle(0, LMI961G.sprDetailDef.EXPIRY_TO_DATE_FWD.ColNo, sLabel)
            '.SetCellStyle(0, LMI961G.sprDetailDef.FWD_USER_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 30, False))
            '.SetCellStyle(0, LMI961G.sprDetailDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 30, False))

            .ActiveSheet.Rows(0).Height = 0

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetSpread(ByVal ds As DataSet) As Boolean

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim dt As DataTable = ds.Tables("GLZ9300OUT_EST_LIST")

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()
            .ActiveSheet.Rows.Count = 1

            .SuspendLayout()

            '----データ挿入----'
            '行数設定
            Dim lDataCnt As Integer = dt.Rows.Count
            If lDataCnt = 0 Then
                .ResumeLayout(True)
                Return True
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lDataCnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = Me.StyleInfoChk(spr)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sLabelRight As StyleInfo = Me.StyleInfoLabelRight(spr)

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lDataCnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMI961G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMI961G.sprDetailDef.EST_NO_DISP.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.JOB_CATEGORY_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.MOVE_TYPE_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.SEARCH_REM.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.PORT_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.CONT_SEQ.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.CONT_TRN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.PLACE_CD_A_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.STAR_PLACE_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.PLACE_CD_B_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.EXPIRY_TO_DATE_FWD.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.FWD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.EST_NO.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.EST_NO_EDA.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.JOB_OUT_EXP_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.JOB_OUT_IMP_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.JOB_IN_EXP_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.JOB_IN_IMP_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.JOB_LOC_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.CONT_LEG_SEQ.ColNo, sLabel)
                .SetCellStyle(i, LMI961G.sprDetailDef.CARGO_SEQ.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMI961G.sprDetailDef.EST_NO_DISP.ColNo, dr.Item("EST_NO_EDA_DISP").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.JOB_CATEGORY_NM.ColNo, dr.Item("JOB_CATEGORY_NM").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.MOVE_TYPE_NM.ColNo, dr.Item("MOVE_TYPE_NM").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.SEARCH_REM.ColNo, dr.Item("SEARCH_REM").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.PORT_NM.ColNo, dr.Item("PORT_NM").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.CONT_SEQ.ColNo, dr.Item("CONT_SEQ").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.CONT_TRN_NM.ColNo, dr.Item("CONT_TRN_NM").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.PLACE_CD_A_NM.ColNo, dr.Item("PLACE_CD_A_NM").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.STAR_PLACE_NM.ColNo, dr.Item("STAR_PLACE_NM").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.PLACE_CD_B_NM.ColNo, dr.Item("PLACE_CD_B_NM").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.EXPIRY_TO_DATE_FWD.ColNo, DateFormatUtility.EditSlash(dr.Item("EXPIRY_TO_DATE_FWD").ToString()))
                .SetCellValue(i, LMI961G.sprDetailDef.FWD_USER_NM.ColNo, dr.Item("FWD_USER_NM").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.EST_NO.ColNo, dr.Item("EST_NO").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.EST_NO_EDA.ColNo, dr.Item("EST_NO_EDA").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.JOB_OUT_EXP_KBN.ColNo, dr.Item("JOB_OUT_EXP_KBN").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.JOB_OUT_IMP_KBN.ColNo, dr.Item("JOB_OUT_IMP_KBN").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.JOB_IN_EXP_KBN.ColNo, dr.Item("JOB_IN_EXP_KBN").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.JOB_IN_IMP_KBN.ColNo, dr.Item("JOB_IN_IMP_KBN").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.JOB_LOC_KBN.ColNo, dr.Item("JOB_LOC_KBN").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.CONT_LEG_SEQ.ColNo, dr.Item("CONT_LEG_SEQ").ToString())
                .SetCellValue(i, LMI961G.sprDetailDef.CARGO_SEQ.ColNo, dr.Item("CARGO_SEQ").ToString())

            Next

            .ResumeLayout(True)

            Return True

        End With

    End Function

    ''' <summary>
    ''' スプレッドのデータを更新
    ''' </summary>
    ''' <param name="frm">frm</param>
    ''' <param name="arr">arr</param>
    ''' <remarks></remarks>
    Friend Function SetUpdSpread(ByVal frm As LMI961F, ByVal arr As ArrayList, ByVal eventShubetsu As LMI961C.EventShubetsu) As Boolean

    End Function

#End Region

#Region "ユーティリティ"

#Region "プロパティ"

    ''' <summary>
    ''' セルのプロパティを設定(CheckBox)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoChk(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetCheckBoxCell(spr, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Label)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabel(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Label)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabelRight(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数14桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum14(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, LMFControlC.MAX_KETA_SPR, True, 0, , ",")

    End Function

#End Region

#End Region

#End Region

End Class
