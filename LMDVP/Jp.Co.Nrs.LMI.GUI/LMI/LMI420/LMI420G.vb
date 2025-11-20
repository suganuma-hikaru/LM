' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI420G : 運賃比較
'  作  成  者       :  daikoku
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports FarPoint.Win.Spread

''' <summary>
''' LMI420Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMI420G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI420F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

    ''' <summary>
    ''' 初期処理か判断するフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShokiFlg As Boolean = True

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI420V

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI420F, ByVal g As LMIControlG, ByVal v As LMI420V)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

        Me._V = v

    End Sub

#End Region

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim unLock As Boolean = True
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            .F1ButtonName = "データ取込"
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"


            '常に使用不可キー
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = lock
            .F11ButtonEnabled = lock

            '常に使用可能キー
            .F1ButtonEnabled = unLock
            .F12ButtonEnabled = unLock

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

            '******************* ヘッダー部 *****************************
            .cmbNrsBr.TabIndex = LMI420C.CtlTabIndex.CMB_NRS_BR
            .txtCustCdL.TabIndex = LMI420C.CtlTabIndex.TXT_CUST_CD_L
            .sprSearch.TabIndex = LMI420C.CtlTabIndex.SPR_SEARCH

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        '初期設定
        Call Me.SetDataControl(sysDate)

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .txtCustCdL.Focus()

        End With

    End Sub

    ''' <summary>
    ''' 画面項目クリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            '.sprDetail.CrearSpread()
            '.lblEdiCtlNo.TextValue = String.Empty

        End With
    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataControl(ByVal sysDate As String)

        With Me._Frm

            .cmbNrsBr.SelectedValue = LMUserInfoManager.GetNrsBrCd()

            '荷主コード(仮)取得
            Dim drCust As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C032" _
                                                                                            , "' AND KBN_NM1 = '", LMUserInfoManager.GetNrsBrCd, "'"))

            If 0 < drCust.Length Then
                .txtCustCdL.TextValue = drCust(0).Item("KBN_NM2").ToString()
            End If

            '荷主コード(仮)より名称を取得し、ラベルに表示を行う
            Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", .txtCustCdL.TextValue _
                                                                                                        , "' AND CUST_CD_M = '00'"))
            If 0 < dr.Length Then
                .lblCustNmL.TextValue = dr(0).Item("CUST_NM_L").ToString()
            End If

            '荷主CDに対象荷主CDを再設定
            Dim custCD As String = String.Empty
            drCust = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C032" _
                                                                                                        , "' AND KBN_NM1 = '", LMUserInfoManager.GetNrsBrCd, "'"))

            For i As Integer = 0 To drCust.Length - 1
                If String.Empty.Equals(custCD) Then
                    custCD = drCust(i).Item("KBN_NM2").ToString.Trim
                Else
                    custCD = String.Concat(custCD, ",", drCust(i).Item("KBN_NM2").ToString.Trim)
                End If

            Next

            .txtCustCdL.TextValue = custCD.ToString()

        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprSearchDef

        '**** 表示列 ****
        'Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI420C.SprSearchColumnIndex.DEF, " ", 20, True)
        Public Shared ORDER_NO As SpreadColProperty = New SpreadColProperty(LMI420C.SprSearchColumnIndex.ORDER_NO, "入出荷指図番号", 100, True)
        Public Shared NRS_UNCHIN As SpreadColProperty = New SpreadColProperty(LMI420C.SprSearchColumnIndex.NRS_UNCHIN, "NRS 運賃", 100, True)
        Public Shared JX_UNCHIN As SpreadColProperty = New SpreadColProperty(LMI420C.SprSearchColumnIndex.JX_UNCHIN, "JX 運賃", 100, True)
        Public Shared SAGAKU As SpreadColProperty = New SpreadColProperty(LMI420C.SprSearchColumnIndex.SAGAKU, "差額", 100, True)
        Public Shared OUTKA_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMI420C.SprSearchColumnIndex.OUTKA_PLAN_DATE, "出荷日", 100, True)
        Public Shared UNSO_NO_L As SpreadColProperty = New SpreadColProperty(LMI420C.SprSearchColumnIndex.UNSO_NO_L, "運送番号", 100, True)

    End Class

    ''' <summary>
    ''' SPREADの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitDetailSpread()

        '検索スプレッド初期化
        Call SetSprSearch()

    End Sub

    ''' <summary>
    ''' SPREADのコントロール設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSprSearch()

        'Spreadの初期値設定
        Dim sprSearch As LMSpread = Me._Frm.sprSearch

        With sprSearch

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMI420C.SprSearchColumnIndex.UNSO_NO_L + 1

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New sprSearchDef)

            '列固定位置を設定します。
            .ActiveSheet.FrozenColumnCount = sprSearchDef.ORDER_NO.ColNo + 1

            .Sheets(0).Rows(0).Visible = False      '検索行を非表示にする

            '検索行の設定を行う
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprSearch)
            Dim delNo As StyleInfo = LMSpreadUtility.GetTextCell(sprSearch, InputControl.ALL_MIX, 30, False)
            Dim orderNo As StyleInfo = LMSpreadUtility.GetTextCell(sprSearch, InputControl.ALL_HANKAKU, 15, False)
            Dim text As StyleInfo = LMSpreadUtility.GetTextCell(sprSearch, InputControl.ALL_MIX, 60, False)
            '**** 表示列 ****
            '.SetCellStyle(0, sprSearchDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.ORDER_NO.ColNo, orderNo)
            .SetCellStyle(0, sprSearchDef.NRS_UNCHIN.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.JX_UNCHIN.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.SAGAKU.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.OUTKA_PLAN_DATE.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.UNSO_NO_L.ColNo, lbl)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSprSearch(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprSearch

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

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lblL As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim numNb As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -999999999, 999999999, True, 0, , ",")

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '**** 表示列 ****
                '.SetCellStyle(i, sprSearchDef.DEF.ColNo, def)
                .SetCellStyle(i, sprSearchDef.ORDER_NO.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.NRS_UNCHIN.ColNo, numNb)
                .SetCellStyle(i, sprSearchDef.JX_UNCHIN.ColNo, numNb)
                .SetCellStyle(i, sprSearchDef.SAGAKU.ColNo, numNb)
                .SetCellStyle(i, sprSearchDef.OUTKA_PLAN_DATE.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.UNSO_NO_L.ColNo, lblL)

                'セル値設定
                '**** 表示列 ****
                '.SetCellValue(i, sprSearchDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprSearchDef.ORDER_NO.ColNo, dr.Item("ORDER_NO").ToString())
                .SetCellValue(i, sprSearchDef.NRS_UNCHIN.ColNo, dr.Item("NRS_UNCHIN").ToString())
                .SetCellValue(i, sprSearchDef.JX_UNCHIN.ColNo, dr.Item("JX_UNCHIN").ToString())
                .SetCellValue(i, sprSearchDef.SAGAKU.ColNo, dr.Item("SAGAKU").ToString())
                .SetCellValue(i, sprSearchDef.OUTKA_PLAN_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("OUTKA_PLAN_DATE").ToString()))
                .SetCellValue(i, sprSearchDef.UNSO_NO_L.ColNo, dr.Item("UNSO_NO_L").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#End Region

End Class
