' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG080G : 状況詳細
'  作  成  者       :  [笈川]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports GrapeCity.Win.Editors.Fields
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMG080Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMG080G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG080F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMGControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG080F, ByVal g As LMGControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

    End Sub

#End Region

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
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = LMGControlC.FUNCTION_YOYAKU_TORIKESI    '予約取消
            .F5ButtonName = String.Empty
            .F6ButtonName = LMGControlC.FUNCTION_SHORI_KEKKA_SHOSAI '処理結果詳細
            .F7ButtonName = String.Empty
            .F8ButtonName = LMGControlC.FUNCTION_KYOUSEI_JIKKOU     '強制実行
            .F9ButtonName = LMGControlC.FUNCTION_KENSAKU            '検索
            .F10ButtonName = String.Empty
            '.F11ButtonName = String.Empty
            .F11ButtonName = LMGControlC.FUNCTION_KYOUSEI_SAKUZYO   '強制削除
            .F12ButtonName = LMGControlC.FUNCTION_TOJIRU            '閉じる

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = always
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = always
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = always
            .F9ButtonEnabled = always
            .F10ButtonEnabled = lock
            '.F11ButtonEnabled = lock
            .F11ButtonEnabled = always
            .F12ButtonEnabled = always

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm
            .cmbBatch.TabIndex = LMG080C.CtlTabIndex.BATCH
            .chkShoriMi.TabIndex = LMG080C.CtlTabIndex.SHORI_MI
            .chkShoriZumi.TabIndex = LMG080C.CtlTabIndex.SHORI_ZUMI
            .chkShoriChu.TabIndex = LMG080C.CtlTabIndex.SHORI_CHU
            .chkTorikeshi.TabIndex = LMG080C.CtlTabIndex.SHORI_TORIKESHI
            .cmbseqflg.TabIndex = LMG080C.CtlTabIndex.MODE
            .imdInvDateFrom.TabIndex = LMG080C.CtlTabIndex.IMD_FROM_DATE
            .imdInvDateTo.TabIndex = LMG080C.CtlTabIndex.IMD_TO_DATE
            .sprDetail.TabIndex = LMG080C.CtlTabIndex.SPREAD

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByVal Hizuke As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

        '初期値の設定
        Call Me.SetFormData(Hizuke)

    End Sub

    ''' <summary>
    ''' 初期値の設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFormData(ByVal Hidsuke As String)

        With Me._Frm
            .cmbBatch.SelectedValue = LMG080C.ONLINE
            .chkShoriMi.Checked = True
            .chkShoriZumi.Checked = True
            .chkShoriChu.Checked = True
            .imdInvDateFrom.TextValue = Hidsuke
            .imdInvDateTo.TextValue = Hidsuke

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
            .cmbBatch.SelectedValue = String.Empty
            .chkShoriMi.Checked = False
            .chkShoriZumi.Checked = False
            .chkShoriChu.Checked = False
            .chkTorikeshi.Checked = False
            .imdInvDateFrom.TextValue = String.Empty
            .imdInvDateTo.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' エラー背景色の初期化
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetBackColor(ByVal frm As LMG080F)
        With frm
            Me._ControlG.SetBackColor(.cmbBatch)
            Me._ControlG.SetBackColor(.chkShoriMi)
            Me._ControlG.SetBackColor(.chkShoriZumi)
            Me._ControlG.SetBackColor(.chkShoriChu)
            Me._ControlG.SetBackColor(.chkTorikeshi)
            Me._ControlG.SetBackColor(.imdInvDateFrom)
            Me._ControlG.SetBackColor(.imdInvDateTo)
        End With
    End Sub


#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataSet)



    End Sub


#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared KEKKA_KB As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.KEKKA_KB, "結果", 320, True)
        Public Shared BATCH_CONDITION_CD As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.BATCH_CONDITION_CD, "バッチ条件区分", 2, False)
        Public Shared BATCH_CONDITION_NM As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.BATCH_CONDITION_NM, "バッチ条件", 90, True)
        Public Shared JIKKOU_MODE_NM As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.JIKKOU_MODE, "実行モード", 90, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.NRS_BR_CD, "営業所ＣＤ", 2, False)     '隠し項目
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.NRS_BR_NM, "営業所", 250, True)
        Public Shared USER_ID As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.USER_ID, "ユーザーＩＤ", 2, False)       '隠し項目
        Public Shared USER_NM As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.USER_NM, "実行指示者", 120, True)
        Public Shared SEKY_DATE As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.SEKY_DATE, "今回請求日", 80, True)
        Public Shared CUST_CD As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.CUST_CD, "荷主コード", 100, True)
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.CUST_NM, "荷主名", 200, True)
        Public Shared JIKKO_DATE As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.JIKKO_DATE, "指示日", 80, True)
        Public Shared JIKKO_TIME As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.JIKKO_TIME, "指示時間", 120, True)
        Public Shared SHORI_DATE As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.SHORI_DATE, "処理開始日", 80, True)
        Public Shared SHORI_TIME As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.SHORI_TIME, "処理開始時間", 120, True)
        Public Shared SYURYO_DATE As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.SYURYO_DATE, "処理終了日", 80, True)
        Public Shared SYURYO_TIME As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.SYURYO_TIME, "処理終了時間", 120, True)
        Public Shared JOB_NO As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.JOB_NO, "JOB番号", 90, True)
        Public Shared JIKKOZUMI_KB As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.JIKKOZUMI_KB, "処理状況", 100, True)
        Public Shared SEKY_FLG As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.SEKY_FLG, "請求フラグ", 2, False)
        Public Shared BATCH_NO As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.BATCH_NO, "バッチ番号", 2, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.CUST_CD_L, "荷主コードL", 2, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.CUST_CD_M, "荷主コードM", 2, False)
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.CUST_CD_S, "荷主コードS", 2, False)
        Public Shared CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.CUST_CD_SS, "荷主コードSS", 2, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.SYS_UPD_DATE, "更新日", 2, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 2, False)
        Public Shared REC_NO As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.REC_NO, "レコード番号", 2, False)
        Public Shared EXEC_STATE_KB As SpreadColProperty = New SpreadColProperty(LMG080C.SprColumnIndex.EXEC_STATE_KB, "処理状況区分", 2, False)
    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        '2014.08.04 高取対応　追加START
        'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
        Dim dr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)
        '2014.08.04 高取対応　追加END

        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            '.ActiveSheet.ColumnCount = 29
            .ActiveSheet.ColumnCount = 30

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprSagyo.SetColProperty(New sprDetailDef)
            .SetColProperty(New LMG080G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprDetailDef)

            '列固定位置を設定します。(ex.ユーザー名で固定)
            .ActiveSheet.FrozenColumnCount = sprDetailDef.DEF.ColNo + 1

            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            '列設定
            .SetCellStyle(0, sprDetailDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.BATCH_CONDITION_CD.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.BATCH_CONDITION_NM.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.JIKKOU_MODE_NM.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))

            '2014.08.04 高取対応　修正START
            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .SetCellStyle(0, sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(spr, "M_NRS_BR", "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .SetCellStyle(0, sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(spr, "M_NRS_BR", "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            '.SetCellStyle(0, sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(spr, "M_NRS_BR", "NRS_BR_CD", "NRS_BR_NM", False))
            '2014.08.04 高取対応　修正END

            .SetCellStyle(0, sprDetailDef.USER_ID.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.USER_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 20, False))
            .SetCellStyle(0, sprDetailDef.SEKY_DATE.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.CUST_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 14, False))
            .SetCellStyle(0, sprDetailDef.CUST_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 240, False))
            .SetCellStyle(0, sprDetailDef.JIKKO_DATE.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.JIKKO_TIME.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.SHORI_DATE.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.SHORI_TIME.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.SYURYO_DATE.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.SYURYO_TIME.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.JOB_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 10, False))
            .SetCellStyle(0, sprDetailDef.JIKKOZUMI_KB.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.KEKKA_KB.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.SEKY_FLG.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.BATCH_NO.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.CUST_CD_M.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.CUST_CD_S.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.CUST_CD_SS.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.REC_NO.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))
            .SetCellStyle(0, sprDetailDef.EXEC_STATE_KB.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right))


        End With
    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMG080F)

        With frm.sprDetail
            .SetCellValue(0, sprDetailDef.BATCH_CONDITION_CD.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.BATCH_CONDITION_NM.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.NRS_BR_CD.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd)
            .SetCellValue(0, sprDetailDef.USER_ID.ColNo, LMUserInfoManager.GetUserID)
            .SetCellValue(0, sprDetailDef.USER_NM.ColNo, LMUserInfoManager.GetUserName)
            .SetCellValue(0, sprDetailDef.SEKY_DATE.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.CUST_CD.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.JIKKO_DATE.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.JOB_NO.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.JIKKOZUMI_KB.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.KEKKA_KB.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.SEKY_FLG.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.BATCH_NO.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.CUST_CD_L.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.CUST_CD_M.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.CUST_CD_S.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.CUST_CD_SS.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.REC_NO.ColNo, String.Empty)
            .SetCellValue(0, sprDetailDef.EXEC_STATE_KB.ColNo, String.Empty)

        End With
    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)
        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As New DataSet
        Dim sekyflg As String = String.Empty
        Dim jikkoTime As String = String.Empty
        Dim shoriTime As String = String.Empty
        Dim shuryoTime As String = String.Empty
        With spr

            .SuspendLayout()
            .Sheets(0).Rows.Count = 1
            'データ挿入
            '行数設定
            Dim tbl As DataTable = ds.Tables(LMG080C.TABLE_NM_OUT)
            Dim lngcnt As Integer = tbl.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If
            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            Dim dr As DataRow
            Dim CUST_CD As String = String.Empty
            '値設定
            For i As Integer = 1 To lngcnt

                dr = tbl.Rows(i - 1)
                jikkoTime = dr.Item("JIKKO_TIME").ToString()
                If String.IsNullOrEmpty(jikkoTime) = False Then
                    jikkoTime = String.Concat(jikkoTime.Substring(0, 2), ":", jikkoTime.Substring(2, 2), ":", _
                                              jikkoTime.Substring(4, 2), ".", jikkoTime.Substring(6, 3))
                End If
                shoriTime = dr.Item("SHORI_TIME").ToString()
                If String.IsNullOrEmpty(shoriTime) = False Then
                    shoriTime = String.Concat(shoriTime.Substring(0, 2), ":", shoriTime.Substring(2, 2), ":", _
                                              shoriTime.Substring(4, 2), ".", shoriTime.Substring(6, 3))
                End If

                shuryoTime = dr.Item("SYURYO_TIME").ToString()
                If String.IsNullOrEmpty(shuryoTime) = False Then
                    shuryoTime = String.Concat(shuryoTime.Substring(0, 2), ":", shuryoTime.Substring(2, 2), ":", _
                                              shuryoTime.Substring(4, 2), ".", shuryoTime.Substring(6, 3))
                End If
                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, def)
                .SetCellStyle(i, sprDetailDef.KEKKA_KB.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.BATCH_CONDITION_CD.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.BATCH_CONDITION_NM.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.JIKKOU_MODE_NM.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.NRS_BR_CD.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.NRS_BR_NM.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.USER_ID.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.USER_NM.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.SEKY_DATE.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.CUST_CD.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.CUST_NM.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.JIKKO_DATE.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.JIKKO_TIME.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.SHORI_DATE.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.SHORI_TIME.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.SYURYO_DATE.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.SYURYO_TIME.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.JOB_NO.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.JIKKOZUMI_KB.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.SEKY_FLG.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.BATCH_NO.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.CUST_CD_L.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.CUST_CD_M.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.CUST_CD_S.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.CUST_CD_SS.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.SYS_UPD_DATE.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.SYS_UPD_TIME.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.REC_NO.ColNo, lbl)

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.KEKKA_KB.ColNo, dr.Item("MESSAGE_STRING").ToString())
                .SetCellValue(i, sprDetailDef.BATCH_CONDITION_CD.ColNo, dr.Item("EXEC_TIMING_KB").ToString())
                .SetCellValue(i, sprDetailDef.BATCH_CONDITION_NM.ColNo, dr.Item("EXEC_TIMING_NM").ToString())
                .SetCellValue(i, sprDetailDef.JIKKOU_MODE_NM.ColNo, dr.Item("SEKY_FLG_NM").ToString())
                .SetCellValue(i, sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, sprDetailDef.USER_ID.ColNo, dr.Item("OPE_USER_CD").ToString())
                .SetCellValue(i, sprDetailDef.USER_NM.ColNo, dr.Item("USER_NM").ToString())
                .SetCellValue(i, sprDetailDef.SEKY_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SEKY_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.CUST_CD.ColNo, dr.Item("CUST_CD").ToString())
                .SetCellValue(i, sprDetailDef.CUST_NM.ColNo, dr.Item("CUST_NM").ToString())
                .SetCellValue(i, sprDetailDef.JIKKO_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("JIKKO_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.JIKKO_TIME.ColNo, jikkoTime)
                .SetCellValue(i, sprDetailDef.SHORI_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SHORI_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.SHORI_TIME.ColNo, shoriTime)
                .SetCellValue(i, sprDetailDef.SYURYO_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SYURYO_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.SYURYO_TIME.ColNo, shuryoTime)
                .SetCellValue(i, sprDetailDef.JOB_NO.ColNo, dr.Item("JOB_NO").ToString())
                .SetCellValue(i, sprDetailDef.JIKKOZUMI_KB.ColNo, dr.Item("EXEC_STATE_NM").ToString())
                .SetCellValue(i, sprDetailDef.SEKY_FLG.ColNo, dr.Item("SEKY_FLG").ToString())
                .SetCellValue(i, sprDetailDef.BATCH_NO.ColNo, dr.Item("BATCH_NO").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_S.ColNo, dr.Item("CUST_CD_S").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_SS.ColNo, dr.Item("CUST_CD_SS").ToString())
                .SetCellValue(i, sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprDetailDef.REC_NO.ColNo, dr.Item("REC_NO").ToString())
                .SetCellValue(i, sprDetailDef.EXEC_STATE_KB.ColNo, dr.Item("EXEC_STATE_KB").ToString())

            Next

            .ResumeLayout(True)

        End With
    End Sub

#End Region 'Spread

#End Region

End Class
