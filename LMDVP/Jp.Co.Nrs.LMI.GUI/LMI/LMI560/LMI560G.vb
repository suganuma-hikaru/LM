' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI560G : TSMC請求データ計算
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
''' LMI560Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMI560G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI560F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI560F, ByVal g As LMIControlG)

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
            .F4ButtonName = "前回計算取消"
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = "実　行"
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = always
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = always
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
            .grpSelect.TabIndex = LMI560C.CtlTabIndex.GRP_SELECT
            .cmbBr.TabIndex = LMI560C.CtlTabIndex.CMB_BR
            .txtSeqtoCd.TabIndex = LMI560C.CtlTabIndex.TXT_SEIQTO_CD
            .lblSeqtoNm.TabIndex = LMI560C.CtlTabIndex.LBL_SEIQTO_NM
            .imdInvDate.TabIndex = LMI560C.CtlTabIndex.IMD_INV_DATE
            'グループ外
            .sprDetail.TabIndex = LMI560C.CtlTabIndex.SPE_DETAIL

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
            .txtSeqtoCd.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm
            .txtSeqtoCd.TextValue = String.Empty
            .lblSeqtoNm.TextValue = String.Empty
            .cmbBr.SelectedValue = String.Empty
            .imdInvDate.TextValue = String.Empty
        End With

    End Sub

    ''' <summary>
    ''' 背景色の初期化
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetBackColor(ByVal frm As LMI560F)

        With Me._Frm
            Me._ControlG.SetBackColor(.txtSeqtoCd)
            Me._ControlG.SetBackColor(.cmbBr)
            Me._ControlG.SetBackColor(.imdInvDate)
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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI560C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared LAST_DATE As SpreadColProperty = New SpreadColProperty(LMI560C.SprColumnIndex.LAST_DATE, "最終請求日", 90, True)
        Public Shared SEIQTO_CD As SpreadColProperty = New SpreadColProperty(LMI560C.SprColumnIndex.SEIQTO_CD, "請求先コード", 120, True)
        Public Shared SEIQTO_NM As SpreadColProperty = New SpreadColProperty(LMI560C.SprColumnIndex.SEIQTO_NM, "請求先名", 480, True)
        Public Shared CLOSE_KB As SpreadColProperty = New SpreadColProperty(LMI560C.SprColumnIndex.CLOSE_KB, "締日区分", 2, False)
        Public Shared LAST_DATE_ORG As SpreadColProperty = New SpreadColProperty(LMI560C.SprColumnIndex.LAST_DATE_ORG, "最終請求日(加工前)", 90, False)
        Public Shared LAST_JOB_NO As SpreadColProperty = New SpreadColProperty(LMI560C.SprColumnIndex.LAST_JOB_NO, "最終JOB番号", 10, False)
        Public Shared BEFORE_DATE As SpreadColProperty = New SpreadColProperty(LMI560C.SprColumnIndex.BEFORE_DATE, "前回請求日", 90, False)
        Public Shared BEFORE_JOB_NO As SpreadColProperty = New SpreadColProperty(LMI560C.SprColumnIndex.BEFORE_JOB_NO, "前回JOB番号", 10, False)

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
            .ActiveSheet.ColumnCount = 9

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMI560G.sprDetailDef(), False)

            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            '列設定
            .SetCellStyle(0, sprDetailDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.LAST_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SEIQTO_CD.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SEIQTO_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 240, False))
            .SetCellStyle(0, sprDetailDef.CLOSE_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.LAST_DATE_ORG.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.LAST_JOB_NO.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.BEFORE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.BEFORE_JOB_NO.ColNo, lbl)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMI560F)

        With frm.sprDetail

            .Sheets(0).Cells(0, sprDetailDef.LAST_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SEIQTO_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SEIQTO_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CLOSE_KB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.LAST_DATE_ORG.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.LAST_JOB_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.BEFORE_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.BEFORE_JOB_NO.ColNo).Value = String.Empty

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
            Dim tbl As DataTable = ds.Tables(LMI560C.TABLE_NM_OUT)
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

                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, def)
                .SetCellStyle(i, sprDetailDef.LAST_DATE.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.SEIQTO_CD.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.SEIQTO_NM.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.CLOSE_KB.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.LAST_DATE_ORG.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.LAST_JOB_NO.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.BEFORE_DATE.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.BEFORE_JOB_NO.ColNo, lbl)

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.LAST_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("LAST_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.SEIQTO_CD.ColNo, dr.Item("SEIQTO_CD").ToString())
                .SetCellValue(i, sprDetailDef.SEIQTO_NM.ColNo, dr.Item("SEIQTO_NM").ToString)
                .SetCellValue(i, sprDetailDef.CLOSE_KB.ColNo, dr.Item("CLOSE_KB").ToString)
                .SetCellValue(i, sprDetailDef.LAST_DATE_ORG.ColNo, dr.Item("LAST_DATE").ToString())
                .SetCellValue(i, sprDetailDef.LAST_JOB_NO.ColNo, dr.Item("LAST_JOB_NO").ToString())
                .SetCellValue(i, sprDetailDef.BEFORE_DATE.ColNo, dr.Item("BEFORE_DATE").ToString())
                .SetCellValue(i, sprDetailDef.BEFORE_JOB_NO.ColNo, dr.Item("BEFORE_JOB_NO").ToString())
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
