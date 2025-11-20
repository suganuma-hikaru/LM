' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI570G : TSMC請求データ検索
'  作  成  者       :  [HORI]
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
''' LMI570Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI570G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI570F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI570F, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

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
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

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
            .grpSelect.TabIndex = LMI570C.CtlTabIndex.GRP_SELECT
            .cmbBr.TabIndex = LMI570C.CtlTabIndex.CMB_BR
            .txtCustCdL.TabIndex = LMI570C.CtlTabIndex.TXT_CUSTCD_L
            .txtCustCdM.TabIndex = LMI570C.CtlTabIndex.TXT_CUSTCD_M
            .txtCustCdS.TabIndex = LMI570C.CtlTabIndex.TXT_CUSTCD_S
            .txtCustCdSs.TabIndex = LMI570C.CtlTabIndex.TXT_CUSTCD_SS
            .lblCustNm.TabIndex = LMI570C.CtlTabIndex.LBL_CUSTNM
            .txtSekySaki.TabIndex = LMI570C.CtlTabIndex.TXT_SEKY
            .lblSeqtoNm.TabIndex = LMI570C.CtlTabIndex.LBL_SEKY
            .imdInvDate.TabIndex = LMI570C.CtlTabIndex.IMD_INV_DATE
            .cmbPrint.TabIndex = LMI570C.CtlTabIndex.CMB_PRINT
            .btnPrint.TabIndex = LMI570C.CtlTabIndex.BTN_PRINT
            .sprMeisai.TabIndex = LMI570C.CtlTabIndex.SPE_DETAIL

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

        'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

        If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
            Me._Frm.cmbBr.ReadOnly = True
        Else
            Me._Frm.cmbBr.ReadOnly = False
        End If

        '請求日の設定
        Me._Frm.imdInvDate.TextValue = Me.SetControlDate(strDate, -1)

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
            .cmbBr.SelectedValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .txtCustCdS.TextValue = String.Empty
            .txtCustCdSs.TextValue = String.Empty
            .lblCustNm.TextValue = String.Empty
            .txtSekySaki.TextValue = String.Empty
            .lblSeqtoNm.TextValue = String.Empty
            .imdInvDate.TextValue = String.Empty
            .cmbPrint.SelectedValue = "01"
        End With

    End Sub

    ''' <summary>
    ''' コントロールの背景色の初期化
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetBackColor(ByVal frm As LMI570F)

        With Me._Frm
            Me._ControlG.SetBackColor(.txtCustCdL)
            Me._ControlG.SetBackColor(.txtCustCdM)
            Me._ControlG.SetBackColor(.txtCustCdS)
            Me._ControlG.SetBackColor(.txtCustCdSs)
            Me._ControlG.SetBackColor(.txtSekySaki)
            Me._ControlG.SetBackColor(.cmbBr)
            Me._ControlG.SetBackColor(.imdInvDate)
            Me._ControlG.SetBackColor(.cmbPrint)
        End With

    End Sub

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

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataSet)

        '参考値の設定
        Call Me.SetSpread(ds)

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprMeisaiDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI570C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SEIKYU_DATE As SpreadColProperty = New SpreadColProperty(LMI570C.SprColumnIndex.INV_DATE, "請求日", 90, True)
        Public Shared SEIQTO_CD As SpreadColProperty = New SpreadColProperty(LMI570C.SprColumnIndex.SEIQTO_CD, "請求先コード", 120, True)
        Public Shared SEIQTO_NM As SpreadColProperty = New SpreadColProperty(LMI570C.SprColumnIndex.SEIQTO_NM, "請求先名", 450, True)
        Public Shared CREATE_DATE As SpreadColProperty = New SpreadColProperty(LMI570C.SprColumnIndex.CREATE_DATE, "作成日", 100, True)
        Public Shared CREATE_USER_NM As SpreadColProperty = New SpreadColProperty(LMI570C.SprColumnIndex.CREATE_USER, "作成者", 120, True)
        Public Shared JOB_NO As SpreadColProperty = New SpreadColProperty(LMI570C.SprColumnIndex.JOB_NO, "JOB番号", 100, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpreadSearch = Me._Frm.sprMeisai

        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 7

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMI570G.sprMeisaiDef(), False)

            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            '列設定
            .SetCellStyle(0, sprMeisaiDef.SEIKYU_DATE.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.SEIQTO_CD.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.SEIQTO_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 240, False))
            .SetCellStyle(0, sprMeisaiDef.CREATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.CREATE_USER_NM.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.JOB_NO.ColNo, lbl)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMI570F)

        With frm.sprMeisai

            .Sheets(0).Cells(0, sprMeisaiDef.SEIKYU_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.SEIQTO_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.SEIQTO_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.CREATE_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.CREATE_USER_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.JOB_NO.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        Dim spr As LMSpreadSearch = Me._Frm.sprMeisai
        Dim dtOut As New DataSet
        Dim sekyflg As String = String.Empty

        With spr

            .SuspendLayout()
            .Sheets(0).Rows.Count = 1
            'データ挿入
            '行数設定
            Dim tbl As DataTable = ds.Tables(LMI570C.TABLE_NM_OUT)
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
            Dim CUST_NM_LM As String = String.Empty
            '値設定
            For i As Integer = 1 To lngcnt

                dr = tbl.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprMeisaiDef.DEF.ColNo, def)
                .SetCellStyle(i, sprMeisaiDef.SEIKYU_DATE.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.SEIQTO_CD.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.SEIQTO_NM.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.CREATE_DATE.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.CREATE_USER_NM.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.JOB_NO.ColNo, lbl)

                'セルに値を設定
                .SetCellValue(i, sprMeisaiDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprMeisaiDef.SEIKYU_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("INV_DATE_TO").ToString()))

                .SetCellValue(i, sprMeisaiDef.SEIQTO_CD.ColNo, dr.Item("SEIQTO_CD").ToString())
                .SetCellValue(i, sprMeisaiDef.SEIQTO_NM.ColNo, dr.Item("SEIQTO_NM").ToString())
                .SetCellValue(i, sprMeisaiDef.CREATE_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("ENT_DATE").ToString()))
                .SetCellValue(i, sprMeisaiDef.CREATE_USER_NM.ColNo, dr.Item("ENT_USER_NM").ToString())
                .SetCellValue(i, sprMeisaiDef.JOB_NO.ColNo, dr.Item("JOB_NO").ToString())
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 年月日付の設定
    ''' </summary>
    ''' <param name="strDate">加算対象日付</param>
    ''' <param name="addCnt">加算月</param>
    ''' <returns>編集後の値</returns>
    ''' <remarks></remarks>
    Friend Function SetControlDate(ByVal strDate As String, ByVal addCnt As Integer) As String


        Dim strYear As String = String.Empty
        Dim strMonth As String = String.Empty

        'システム日付を引数より加算
        strDate = Convert.ToString(DateSerial(Convert.ToInt32(strDate.Substring(0, 4)),
                                                Convert.ToInt32(strDate.Substring(4, 2)) + addCnt,
                                                Convert.ToInt32(strDate.Substring(6, 2))))
        '年月の編集
        strYear = Convert.ToString(strDate).Substring(0, 4)
        strMonth = Convert.ToString(strDate).Substring(5, 2)


        Return String.Concat(strYear, strMonth)

    End Function

#End Region

#End Region

End Class
