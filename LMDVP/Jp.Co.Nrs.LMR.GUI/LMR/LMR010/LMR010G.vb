' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMR       : 完了
'  プログラムID     :  LMR010    : 完了取込
'  作  成  者       :  [矢内正之]
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
''' LMR010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMR010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMR010F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMR010F, ByVal g As LMRControlG)

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
            .F11ButtonName = "完　了"
            .F12ButtonName = "閉じる"


            Select Case mode
                Case LMR010C.MODE_DEFAULT
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
                    .F11ButtonEnabled = True
                    .F12ButtonEnabled = always

                Case LMR010C.MODE_KANRYO
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

            End Select

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
            .CmbEigyo.TabIndex = LMR010C.CtlTabIndex.EIGYO
            .txtTantoCD.TabIndex = LMR010C.CtlTabIndex.TANTOCD
            .txtCustCD.TabIndex = LMR010C.CtlTabIndex.CUSTCD
            .cmbKanryo.TabIndex = LMR010C.CtlTabIndex.KANRYO
            .imdNyukaDate_From.TabIndex = LMR010C.CtlTabIndex.NYUKADATE_FROM
            .imdNyukaDate_To.TabIndex = LMR010C.CtlTabIndex.NYUKADATE_TO
            .sprKanryo.TabIndex = LMR010C.CtlTabIndex.SPDKANRYO

            'TabStop

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = LMR010C.MODE_DEFAULT)

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As String = "")

        With Me._Frm
            .CmbEigyo.Focus()
        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .CmbEigyo.SelectedValue = String.Empty
            .txtTantoCD.TextValue = String.Empty
            .txtCustCD.TextValue = String.Empty
            .cmbKanryo.SelectedValue = String.Empty
            .imdNyukaDate_From.TextValue = String.Empty
            .imdNyukaDate_To.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByRef frm As LMR010F)

        '初期値が存在するコントロール
        frm.CmbEigyo.SelectedValue() = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()     '（自）営業所

        '2014.08.04 FFEM高取対応 START
        'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

        If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
            frm.CmbEigyo.ReadOnly = True
        Else
            frm.CmbEigyo.ReadOnly = False
        End If
        '2014.08.04 FFEM高取対応 END

        '初期荷主情報取得
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST). _
                                    Select("SYS_DEL_FLG = '0'" & _
                                           " AND USER_CD = '" & LM.Base.LMUserInfoManager.GetUserID() & "'" & _
                                           " AND DEFAULT_CUST_YN = '01'")

        If getDr.Length() > 0 Then
            frm.txtCustCD.TextValue = getDr(0).Item("CUST_CD_L").ToString()                  '（初期）荷主コード（大）
            frm.lblCustNM.TextValue = getDr(0).Item("CUST_NM_L").ToString()                  '（初期）荷主名（大）
        End If

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

#End Region

#Region "検索結果表示"


#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprKanryoDef
        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMR010C.sprKanryoColumnIndex.DEF, " ", 20, True)
        Public Shared KANRI_NO_L As SpreadColProperty = New SpreadColProperty(LMR010C.sprKanryoColumnIndex.KANRI_NO, "管理番号(大)", 80, True)
        Public Shared PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMR010C.sprKanryoColumnIndex.PLAN_DATE, "入出荷/作業日", 100, True)
        Public Shared ORDER_NO As SpreadColProperty = New SpreadColProperty(LMR010C.sprKanryoColumnIndex.ORDER_NO, "オーダー番号", 100, True)
        Public Shared TANTO_USER As SpreadColProperty = New SpreadColProperty(LMR010C.sprKanryoColumnIndex.TANTO_USER, "担当者", 100, True)
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMR010C.sprKanryoColumnIndex.CUST_NM, "荷主名", 300, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMR010C.sprKanryoColumnIndex.DEST_NM, "届先名", 300, True)
        Public Shared KONPO_KOSU As SpreadColProperty = New SpreadColProperty(LMR010C.sprKanryoColumnIndex.KONPO_KOSU, "梱包個数", 100, True)
        'invisible
        Public Shared CHK_INKA_STATE_KB As SpreadColProperty = New SpreadColProperty(LMR010C.sprKanryoColumnIndex.CHK_INKA_STATE_KB, "入荷進捗区分", 10, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMR010C.sprKanryoColumnIndex.CUST_CD_L, "荷主コード大", 10, False)
        'START YANAI 要望番号932
        Public Shared SCNT As SpreadColProperty = New SpreadColProperty(LMR010C.sprKanryoColumnIndex.SCNT, "出荷(小)件数", 10, False)
        'END YANAI 要望番号932
        Public Shared TSMC_QTY_SUMI As SpreadColProperty = New SpreadColProperty(LMR010C.sprKanryoColumnIndex.TSMC_QTY_SUMI, "TSMC検品済数", 10, False)
        Public Shared TSMC_QTY As SpreadColProperty = New SpreadColProperty(LMR010C.sprKanryoColumnIndex.TSMC_QTY, "TSMCシステム個数", 10, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprKanryo.CrearSpread()

            '列数設定
            .sprKanryo.Sheets(0).ColumnCount = LMR010C.sprKanryoColCount     '一覧

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprKanryo.SetColProperty(New sprKanryoDef)
            .sprKanryo.SetColProperty(New LMR010G.sprKanryoDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.ユーザー名で固定)
            '.sprKanryo.Sheets(0).FrozenColumnCount = sprKanryoDef.KANRI_NO_L.ColNo + 1

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(.sprKanryo, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(.sprKanryo, CellHorizontalAlignment.Left)
            Dim sText As StyleInfo = LMSpreadUtility.GetTextCell(.sprKanryo, InputControl.ALL_MIX_IME_OFF, 10, True)

            '列設定
            '.sprKanryo.Sheets(0).AddRows(.sprKanryo.Sheets(0).Rows.Count, 1)
            '値設定
            .sprKanryo.SetCellStyle(0, sprKanryoDef.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprKanryo, True))
            .sprKanryo.SetCellStyle(0, sprKanryoDef.KANRI_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprKanryo, InputControl.ALL_MIX_IME_OFF, 10, False))  '管理№(大)
            .sprKanryo.SetCellStyle(0, sprKanryoDef.PLAN_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprKanryo, True))                                 '入出荷予定日
            .sprKanryo.SetCellStyle(0, sprKanryoDef.ORDER_NO.ColNo, LMSpreadUtility.GetTextCell(.sprKanryo, InputControl.ALL_MIX_IME_OFF, 30, False))   'オーダー番号
            .sprKanryo.SetCellStyle(0, sprKanryoDef.TANTO_USER.ColNo, LMSpreadUtility.GetTextCell(.sprKanryo, InputControl.ALL_MIX_IME_OFF, 20, False)) '担当名
            .sprKanryo.SetCellStyle(0, sprKanryoDef.CUST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprKanryo, InputControl.ALL_MIX_IME_OFF, 122, False))   '荷主名
            .sprKanryo.SetCellStyle(0, sprKanryoDef.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprKanryo, InputControl.ALL_MIX_IME_OFF, 80, False))    '届先名
            .sprKanryo.SetCellStyle(0, sprKanryoDef.KONPO_KOSU.ColNo, LMSpreadUtility.GetNumberCell(.sprKanryo, 0, 999999999, True, 0))                 '梱包個数

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMR010F)

        With frm.sprKanryo

            'visible
            .Sheets(0).Cells(0, sprKanryoDef.DEF.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprKanryoDef.KANRI_NO_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprKanryoDef.PLAN_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprKanryoDef.ORDER_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprKanryoDef.TANTO_USER.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprKanryoDef.CUST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprKanryoDef.DEST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprKanryoDef.KONPO_KOSU.ColNo).Value = String.Empty

            'invisible
            .Sheets(0).Cells(0, sprKanryoDef.CHK_INKA_STATE_KB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprKanryoDef.CUST_CD_L.ColNo).Value = String.Empty
            'START YANAI 要望番号932
            .Sheets(0).Cells(0, sprKanryoDef.SCNT.ColNo).Value = String.Empty
            'END YANAI 要望番号932
            .Sheets(0).Cells(0, sprKanryoDef.TSMC_QTY_SUMI.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprKanryoDef.TSMC_QTY.ColNo).Value = String.Empty
        End With

    End Sub

    '''' <summary>
    '''' パラメータINの値を画面項目に設定
    '''' </summary>
    '''' <param name="frm"></param>
    '''' <remarks></remarks>
    Friend Sub SetInParamValue(ByVal frm As LMR010F, ByVal ds As DataSet)

        With frm
            Dim dt As DataTable = ds.Tables(LMControlC.LMR010C_TABLE_NM_IN)
            Dim dr As DataRow = dt.Rows(0)

            .CmbEigyo.SelectedValue = dr.Item("NRS_BR_CD").ToString
            '.txtTantoCD.TextValue = dr.Item("TANTO_USER_CD").ToString
            '.txtCustCD.TextValue = dr.Item("CUST_CD").ToString
            .cmbKanryo.SelectedValue = dr.Item("KANRYO_SYUBETU").ToString
            '.imdNyukaDate_From.TextValue = dr.Item("INOUTKA_DATE_FROM").ToString
            '.imdNyukaDate_To.TextValue = dr.Item("INOUTKA_DATE_TO").ToString

            '.sprKanryo.Sheets(0).Cells(0, sprKanryoDef.KANRI_NO_L.ColNo).Value = dr.Item("INOUTKA_NO_L").ToString
            '.sprKanryo.Sheets(0).Cells(0, sprKanryoDef.ORDER_NO.ColNo).Value = dr.Item("INOUTKA_ORD_NO").ToString
            '.sprKanryo.Sheets(0).Cells(0, sprKanryoDef.TANTO_USER.ColNo).Value = dr.Item("TANTO_USER_NM").ToString
            '.sprKanryo.Sheets(0).Cells(0, sprKanryoDef.CUST_NM.ColNo).Value = dr.Item("CUST_NM").ToString
            '.sprKanryo.Sheets(0).Cells(0, sprKanryoDef.DEST_NM.ColNo).Value = dr.Item("DEST_NM").ToString

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal frm As LMR010F, ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprKanryo

        With spr

            .SuspendLayout()

            '----データ挿入----'

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count()
            If 0 = lngcnt Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count(), lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            Dim dr As DataRow = dt.NewRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprKanryoDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprKanryoDef.KANRI_NO_L.ColNo, sLabel)  '管理№(大)
                .SetCellStyle(i, sprKanryoDef.PLAN_DATE.ColNo, sLabel)   '入出荷予定日
                .SetCellStyle(i, sprKanryoDef.ORDER_NO.ColNo, sLabel)    'オーダー番号
                .SetCellStyle(i, sprKanryoDef.TANTO_USER.ColNo, sLabel)  '担当名
                .SetCellStyle(i, sprKanryoDef.CUST_NM.ColNo, sLabel)     '荷主名
                .SetCellStyle(i, sprKanryoDef.DEST_NM.ColNo, sLabel)     '届先名
                .SetCellStyle(i, sprKanryoDef.KONPO_KOSU.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ",")) '梱包個数
                .SetCellStyle(i, sprKanryoDef.DEST_NM.ColNo, sLabel)     '届先名
                .SetCellStyle(i, sprKanryoDef.CHK_INKA_STATE_KB.ColNo, sLabel)     '入荷進捗区分(出荷確定用)
                .SetCellStyle(i, sprKanryoDef.CUST_CD_L.ColNo, sLabel)     '入荷進捗区分(出荷確定用)
                'START YANAI 要望番号932
                .SetCellStyle(i, sprKanryoDef.SCNT.ColNo, sLabel)        '出荷(小)件数
                'END YANAI 要望番号932
                .SetCellStyle(i, sprKanryoDef.TSMC_QTY_SUMI.ColNo, sLabel)  ' TSMC検品済数
                .SetCellStyle(i, sprKanryoDef.TSMC_QTY.ColNo, sLabel)       ' TSMCシステム個数

                'セルに値を設定
                .SetCellValue(i, sprKanryoDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprKanryoDef.KANRI_NO_L.ColNo, dr.Item("INOUTKA_NO_L").ToString()) '管理№(大)
                .SetCellValue(i, sprKanryoDef.PLAN_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("INOUTKA_DATE").ToString()))  '入出荷予定日
                .SetCellValue(i, sprKanryoDef.ORDER_NO.ColNo, dr.Item("INOUTKA_ORD_NO").ToString()) 'オーダー番号
                .SetCellValue(i, sprKanryoDef.TANTO_USER.ColNo, dr.Item("TANTO_USER").ToString())   '担当名
                .SetCellValue(i, sprKanryoDef.CUST_NM.ColNo, dr.Item("CUST_NM").ToString())         '荷主名
                .SetCellValue(i, sprKanryoDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())         '届先名
                .SetCellValue(i, sprKanryoDef.KONPO_KOSU.ColNo, dr.Item("PKG_NB").ToString())       '梱包個数
                .SetCellValue(i, sprKanryoDef.CHK_INKA_STATE_KB.ColNo, dr.Item("CHK_INKA_STATE_KB").ToString())       '入荷進捗区分(出荷確定用)
                .SetCellValue(i, sprKanryoDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())       '入荷進捗区分(出荷確定用)
                'START YANAI 要望番号932
                .SetCellValue(i, sprKanryoDef.SCNT.ColNo, dr.Item("SCNT").ToString())       '出荷(小)件数
                'END YANAI 要望番号932
                .SetCellValue(i, sprKanryoDef.TSMC_QTY_SUMI.ColNo, dr.Item("TSMC_QTY_SUMI").ToString()) ' TSMC検品済数
                .SetCellValue(i, sprKanryoDef.TSMC_QTY.ColNo, dr.Item("TSMC_QTY").ToString())           ' TSMCシステム個数

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドをチェック状態にする
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Public Sub SpreadCheckOn(ByVal frm As LMR010F)

        Dim spr As LMSpreadSearch = Me._Frm.sprKanryo

        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1

        If max = 0 Then
            Exit Sub
        End If


        For i As Integer = 1 To max
            'チェックON
            spr.SetCellValue(i, sprKanryoDef.DEF.ColNo, LMR010C.CHECK_TRUE)
        Next

    End Sub

#End Region 'Spread

#End Region

End Class
