' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG010G : 保管料/荷役料計算
'  作  成  者       :  []
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
''' LMG010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMG010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG010F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG010F, ByVal g As LMGControlG)

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
            .F4ButtonName = LMGControlC.FUNCTION_ZENKAI_KEISAN_TORIKESHI
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = LMGControlC.FUNCTION_JIKKOU
            .F8ButtonName = LMGControlC.FUNCTION_JOKYO_SHOSAI
            .F9ButtonName = LMGControlC.FUNCTION_KENSAKU
            .F10ButtonName = LMGControlC.FUNCTION_MST_SANSHO
            .F11ButtonName = String.Empty
            .F12ButtonName = LMGControlC.FUNCTION_TOJIRU

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = always
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = always
            .F8ButtonEnabled = always
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = lock
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

            '検索条件
            .grpSelect.TabIndex = LMG010C.CtlTabIndex.GRP_SELECT
            .cmbBr.TabIndex = LMG010C.CtlTabIndex.CMB_BR
            .txtCustCdL.TabIndex = LMG010C.CtlTabIndex.TXT_CUSTCD_L
            .txtCustCdM.TabIndex = LMG010C.CtlTabIndex.TXT_CUSTCD_M
            .lblCustNm.TabIndex = LMG010C.CtlTabIndex.LBL_CUSTNM
            .cmbSimebi.TabIndex = LMG010C.CtlTabIndex.CMB_SHIMEBI
            .imdInvDate.TabIndex = LMG010C.CtlTabIndex.IMD_INV_DATE
            .chkSelectByNrsB.TabIndex = LMG010C.CtlTabIndex.CHK_TANTOSHA
            'モード条件
            .grpMode.TabIndex = LMG010C.CtlTabIndex.GRP_MODE
            .optSeikyuC.TabIndex = LMG010C.CtlTabIndex.OPT_CHECK
            .optSeikyuH.TabIndex = LMG010C.CtlTabIndex.OPT_HONBAN
            .chkMikan.TabIndex = LMG010C.CtlTabIndex.CHK_MIKAN
            .cmbBatch.TabIndex = LMG010C.CtlTabIndex.CMB_BACTH
            'グループ外
            .sprDetail.TabIndex = LMG010C.CtlTabIndex.SPE_DETAIL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByVal strDate As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コントロールの日付書式設定
        Call Me.SetDateControl()

        '営業所の設定
        Me._Frm.cmbBr.SelectedValue = LMUserInfoManager.GetNrsBrCd()

        '2014.08.04 FFEM高取対応 START
        'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

        If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
            Me._Frm.cmbBr.ReadOnly = True
        Else
            Me._Frm.cmbBr.ReadOnly = False
        End If
        '2014.08.04 FFEM高取対応 END

        '締日コンボボックス
        Me._Frm.cmbSimebi.SelectedValue = LMG010C.MATSUJIME

        'SBS高道）初期値の設定 共通クラス呼び出しに変更
        '請求日の設定
        Me._Frm.imdInvDate.TextValue = Me._ControlG.SetControlDate(strDate, -1)

        ' 実行モード チェックに初期値設定
        Me._Frm.optSeikyuH.Checked = True

        'バッチ条件コンボボックス
        Me._Frm.cmbBatch.SelectedValue = LMG010C.ONLINE

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .grpSelect.Focus()
            .txtCustCdL.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .cmbBatch.SelectedValue = String.Empty
            .cmbBr.SelectedValue = String.Empty
            .cmbSimebi.SelectedValue = String.Empty
            .imdInvDate.TextValue = String.Empty
            .chkMikan.SetBinaryValue(LMConst.FLG.OFF)
            .lblCustNm.TextValue = String.Empty
            .optSeikyuC.Checked = False
            .optSeikyuGC.Checked = False
            .optSeikyuH.Checked = False
        End With

    End Sub

    ''' <summary>
    ''' 背景色の初期化
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetBackColor(ByVal frm As LMG010F)

        With Me._Frm
            Me._ControlG.SetBackColor(.txtCustCdL)
            Me._ControlG.SetBackColor(.txtCustCdM)
            Me._ControlG.SetBackColor(.cmbBatch)
            Me._ControlG.SetBackColor(.cmbBr)
            Me._ControlG.SetBackColor(.cmbSimebi)
            Me._ControlG.SetBackColor(.imdInvDate)
        End With
    End Sub

    ''' <summary>
    ''' 荷主名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCustName()

        Dim cust_nm As String = String.Empty
        Dim nrsbrcd As String = Me._Frm.cmbBr.SelectedValue.ToString '20160927 要番2622 tsunehira add 
        Dim custcdl As String = Me._Frm.txtCustCdL.TextValue
        Dim custcdm As String = Me._Frm.txtCustCdM.TextValue

        With Me._Frm
            Dim cust As ArrayList = Me._ControlG.GetCustNm(nrsbrcd, custcdl, custcdm)

            'リストが存在する場合、フォームにデータを設定する。
            If cust.Count >= 1 = True Then

                '2011/08/01 菱刈 取得項目変更 スタート
                '荷主コード(中)の値が入っている場合
                If String.IsNullOrEmpty(custcdm) = False Then
                    .lblCustNm.TextValue = cust(0).ToString()
                Else
                    '荷主コード(中)がブランクの場合
                    .lblCustNm.TextValue = cust(6).ToString()
                End If

                .txtCustCdL.TextValue = cust(2).ToString()
                ' .txtCustCdM.TextValue = cust(3).ToString()

                '2011/08/01 菱刈 取得項目変更 エンド

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
    Public Class sprDetailDef
        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared LAST_INV_DATE As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.LAST_INV_DATE, "最終請求日", 90, True)
        Public Shared SIME_DATE As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.SHIMEBI, "締日", 80, True)
        Public Shared CUST_CD As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.CUST_CD, "荷主コード", 120, True)
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.CUST_NM, "荷主名", 480, True)
        Public Shared SEIKIKAN_TO As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.SEIKIKAN_TO, "請求期間TO", 90, True)
        Public Shared KIWARI_KBN As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.KIWARI_KBN, "期割区分", 100, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.NRS_BR_CD, "営業所CD", 2, False)
        Public Shared UPD_DATE_M_CUST As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.UPD_DATE_M_CUST, "荷主Ｍ更新日付", 10, False)
        Public Shared UPD_TIME_M_CUST As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.UPD_TIME_M_CUST, "荷主Ｍ更新時刻", 10, False)
        Public Shared JOB_NO As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.JOB_NO, "JOB番号", 10, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.CUST_CD_L, "荷主コードL", 5, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.CUST_CD_M, "荷主コードM", 2, False)
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.CUST_CD_S, "荷主コードS", 2, False)
        Public Shared CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.CUST_CD_SS, "荷主コードSS", 2, False)
        Public Shared CLOSE_KB As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.CLOSE_KB, "締日区分", 2, False)
        Public Shared KIWARI_KB As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.KIWARI_KB, "期割区分", 2, False)
        Public Shared INV_DATE As SpreadColProperty = New SpreadColProperty(LMG010C.SprColumnIndex.INV_DATE, "請求日", 90, False)

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
            .ActiveSheet.ColumnCount = 18

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprSagyo.SetColProperty(New sprDetailDef)
            .SetColProperty(New LMG010G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprDetailDef)

            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            '列設定
            .SetCellStyle(0, sprDetailDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.LAST_INV_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SIME_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CUST_CD.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CUST_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 240, False))
            .SetCellStyle(0, sprDetailDef.SEIKIKAN_TO.ColNo, LMSpreadUtility.GetDateTimeCell(spr, True))
            .SetCellStyle(0, sprDetailDef.KIWARI_KBN.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "K003", False))
            .SetCellStyle(0, sprDetailDef.NRS_BR_CD.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.UPD_DATE_M_CUST.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.UPD_TIME_M_CUST.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.JOB_NO.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CUST_CD_L.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CUST_CD_M.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CUST_CD_S.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CUST_CD_SS.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CLOSE_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.KIWARI_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.INV_DATE.ColNo, lbl)


        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMG010F)

        With frm.sprDetail

            .Sheets(0).Cells(0, sprDetailDef.LAST_INV_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SIME_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SEIKIKAN_TO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.KIWARI_KBN.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.NRS_BR_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.UPD_DATE_M_CUST.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.UPD_TIME_M_CUST.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.JOB_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_CD_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_CD_M.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_CD_S.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_CD_SS.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CLOSE_KB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.KIWARI_KB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.INV_DATE.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Friend Sub SetSelectListData(ByVal ds As DataSet)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As New DataSet
        Dim CLOSE_KB As String = String.Empty
        Dim SEIKYUDATE As String = String.Empty
        Dim INV_DATE As String = Me._Frm.imdInvDate.TextValue()
        Dim CALCULATION As String = String.Empty
        Dim MATSUJITU As String = "00"                 '締日区分（末日）
        Dim DTFMT As String = "0000/00/00"             '日付フォーマット

        With spr

            .SuspendLayout()
            .Sheets(0).Rows.Count = 1
            'データ挿入
            '行数設定
            Dim tbl As DataTable = ds.Tables(LMG010C.TABLE_NM_OUT)
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

            '値設定
            For i As Integer = 1 To lngcnt
                dr = tbl.Rows(i - 1)
                If String.IsNullOrEmpty(dr.Item("KIWARI_KB").ToString()) = False Then

                    Dim custl As String = dr.Item("CUST_CD_L").ToString
                    Dim custm As String = dr.Item("CUST_CD_M").ToString
                    Dim custs As String = dr.Item("CUST_CD_S").ToString
                    Dim custss As String = dr.Item("CUST_CD_SS").ToString
                    Dim custcd As String = String.Concat(custl, "-", custm, "-", custs, "-", custss)

                    '請求期間ＴＯの設定
                    SEIKYUDATE = dr.Item("INV_DATE_TO").ToString()

                    '締日区分の取得
                    CLOSE_KB = dr.Item("CLOSE_KB").ToString()

                    '請求最終日の取得
                    CALCULATION = dr.Item("HOKAN_NIYAKU_CALCULATION").ToString()

                    '請求期間ＴＯが存在している場合
                    If String.IsNullOrEmpty(SEIKYUDATE) = False Then
                        '締日区分が末日の場合
                        If MATSUJITU.Equals(CLOSE_KB) = True Then
                            '請求期間ＴＯに1ヶ月＋、1日－
                            SEIKYUDATE = String.Concat(SEIKYUDATE.Substring(0, 6), "01")
                            SEIKYUDATE = Convert.ToString(Date.Parse(Format(CInt(SEIKYUDATE) _
                                                                            , DTFMT)).AddMonths(1).AddDays(-1))

                            INV_DATE = String.Concat(Replace(INV_DATE, "/", "").Substring(0, 6), "01")
                            INV_DATE = Convert.ToString(Date.Parse(Format(CInt(INV_DATE) _
                                                                            , DTFMT)).AddMonths(1).AddDays(-1))

                            SEIKYUDATE = Convert.ToString(SEIKYUDATE).Replace("/", "").Substring(0, 8)

                            If SEIKYUDATE <= CALCULATION = True Then

                                SEIKYUDATE = Convert.ToString(Date.Parse(Format(CInt(String.Concat(SEIKYUDATE.Substring(0, 6), "01")), DTFMT)).AddMonths(2).AddDays(-1))
                            End If
                        Else
                            '請求期間ＴＯ＋締日、1ヶ月＋を結合
                            SEIKYUDATE = String.Concat(SEIKYUDATE.Substring(0, 6), CLOSE_KB)
                            SEIKYUDATE = Convert.ToString(Date.Parse(Format(CInt(SEIKYUDATE), DTFMT)).AddMonths(1))
                            INV_DATE = String.Concat(INV_DATE.Substring(0, 6), CLOSE_KB)
                        End If
                        SEIKYUDATE = SEIKYUDATE.Replace("/", "").Substring(0, 8)
                        INV_DATE = INV_DATE.Replace("/", "").Substring(0, 8)
                    End If
                    'セルスタイル設定
                    .SetCellStyle(i, sprDetailDef.DEF.ColNo, def)
                    .SetCellStyle(i, sprDetailDef.LAST_INV_DATE.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.SIME_DATE.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.CUST_CD.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.CUST_NM.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.SEIKIKAN_TO.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.KIWARI_KBN.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.NRS_BR_CD.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.UPD_DATE_M_CUST.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.UPD_TIME_M_CUST.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.JOB_NO.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.CUST_CD_L.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.CUST_CD_M.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.CUST_CD_S.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.CUST_CD_SS.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.CLOSE_KB.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.KIWARI_KB.ColNo, lbl)
                    .SetCellStyle(i, sprDetailDef.INV_DATE.ColNo, lbl)

                    'セルに値を設定
                    .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                    .SetCellValue(i, sprDetailDef.LAST_INV_DATE.ColNo, DateFormatUtility.EditSlash(CALCULATION))
                    .SetCellValue(i, sprDetailDef.SIME_DATE.ColNo, dr.Item("CLOSE_NM").ToString())
                    .SetCellValue(i, sprDetailDef.CUST_CD.ColNo, custcd)
                    .SetCellValue(i, sprDetailDef.CUST_NM.ColNo, dr.Item("CUST_NM").ToString)
                    .SetCellValue(i, sprDetailDef.SEIKIKAN_TO.ColNo, DateFormatUtility.EditSlash(SEIKYUDATE))
                    .SetCellValue(i, sprDetailDef.KIWARI_KBN.ColNo, dr.Item("KIWARI_NM").ToString)
                    .SetCellValue(i, sprDetailDef.UPD_DATE_M_CUST.ColNo, dr.Item("UPD_DATE_M_CUST").ToString())
                    .SetCellValue(i, sprDetailDef.UPD_TIME_M_CUST.ColNo, dr.Item("UPD_TIME_M_CUST").ToString())
                    .SetCellValue(i, sprDetailDef.JOB_NO.ColNo, dr.Item("JOB_NO").ToString())
                    .SetCellValue(i, sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                    .SetCellValue(i, sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                    .SetCellValue(i, sprDetailDef.CUST_CD_S.ColNo, dr.Item("CUST_CD_S").ToString())
                    .SetCellValue(i, sprDetailDef.CUST_CD_SS.ColNo, dr.Item("CUST_CD_SS").ToString())
                    .SetCellValue(i, sprDetailDef.CLOSE_KB.ColNo, CLOSE_KB)
                    .SetCellValue(i, sprDetailDef.KIWARI_KB.ColNo, dr.Item("KIWARI_KB").ToString())
                    .SetCellValue(i, sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                    .SetCellValue(i, sprDetailDef.INV_DATE.ColNo, INV_DATE)
                End If
            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl()

        With Me._Frm

            Call Me.SetDateFormat(.imdInvDate)

        End With

    End Sub

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <param name="ctl">書式設定を行うコントロール</param>
    ''' <remarks></remarks>
    Private Sub SetDateFormat(ByVal ctl As LMImDate)

        ctl.Format = DateFieldsBuilder.BuildFields("yyyyMM")
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields("yyyy/MM")

    End Sub

#End Region

#End Region

End Class
