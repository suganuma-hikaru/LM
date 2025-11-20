' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI320G : 請求明細・鑑作成
'  作  成  者       :  yamanaka
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
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI320Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMI320G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI320F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI320V

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI320F, ByVal g As LMIControlG, ByVal v As LMI320V)

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

#Region "Method"

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
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
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

            '画面入力モードによるロック制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = unLock
            .F10ButtonEnabled = lock
            .F11ButtonEnabled = lock
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

            .cmbNrsBr.TabIndex = LMI320C.CtlTabIndex.CMB_NRS_BR
            .imdSeiqDate.TabIndex = LMI320C.CtlTabIndex.IMD_SEIQ_DATE
            .cmbMake.TabIndex = LMI320C.CtlTabIndex.CMB_MAKE
            .btnMake.TabIndex = LMI320C.CtlTabIndex.BTN_MAKE
            .cmbPrintShubetu.TabIndex = LMI320C.CtlTabIndex.CMB_PRINT_SHUBETU
            .btnPrint.TabIndex = LMI320C.CtlTabIndex.BTN_PRINT_SHUBETU
            .sprDetail.TabIndex = LMI320C.CtlTabIndex.SPR_DETAIL

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .imdSeiqDate.Focus()

        End With

    End Sub

    ''' <summary>
    ''' 初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetDateControl(ByVal sysDate As String)

        With Me._Frm

            .cmbNrsBr.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            .imdSeiqDate.TextValue = sysDate

        End With

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI320C.SprDetailColumnIndex.DEF, " ", 20, True)
        Public Shared SEIQTO_CD As SpreadColProperty = New SpreadColProperty(LMI320C.SprDetailColumnIndex.SEIQTO_CD, "請求先コード", 100, True)
        Public Shared SEIQTO_NM As SpreadColProperty = New SpreadColProperty(LMI320C.SprDetailColumnIndex.SEIQTO_NM, "請求先名", 400, True)
        Public Shared KAGAMI_SHUBETU As SpreadColProperty = New SpreadColProperty(LMI320C.SprDetailColumnIndex.KAGAMI_SHUBETU, "鑑種別", 50, True)
        Public Shared TOKUISAKI_CD As SpreadColProperty = New SpreadColProperty(LMI320C.SprDetailColumnIndex.TOKUISAKI_CD, "得意先コード", 100, True)
        Public Shared HUTANKA As SpreadColProperty = New SpreadColProperty(LMI320C.SprDetailColumnIndex.HUTANKA, "負担課", 100, True)
        Public Shared KIGYO_CD As SpreadColProperty = New SpreadColProperty(LMI320C.SprDetailColumnIndex.KIGYO_CD, "S企業コード", 100, True) 'notes1907 add 

    End Class

    ''' <summary>
    ''' SPREADの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitDetailSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = LMI320C.SprDetailColumnIndex.CLM_NM

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef)

            '列固定位置を設定します。
            .sprDetail.ActiveSheet.FrozenColumnCount = sprDetailDef.HUTANKA.ColNo + 1

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータのコントロール設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        'Spreadの初期値設定
        Dim spr As LMSpread = Me._Frm.sprDetail

        '検索行の設定を行う
        Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

        With spr

            '**** 表示列 ****
            .SetCellStyle(0, sprDetailDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SEIQTO_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUMBER, 7, False))
            .SetCellStyle(0, sprDetailDef.SEIQTO_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprDetailDef.KAGAMI_SHUBETU.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_ALPHA, 1, False))
            .SetCellStyle(0, sprDetailDef.TOKUISAKI_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 8, False))
            .SetCellStyle(0, sprDetailDef.HUTANKA.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 7, False))
            .SetCellStyle(0, sprDetailDef.KIGYO_CD.ColNo, lbl) 'notes1907 add

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail

        With spr

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count

            If lngcnt < 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, True)  '要望対応:1813 yamanaka 2013.02.21 編集不可に変更
            Dim lblL As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            Dim dr As DataRow = dt.NewRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, def)
                .SetCellStyle(i, sprDetailDef.SEIQTO_CD.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.SEIQTO_NM.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.KAGAMI_SHUBETU.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.TOKUISAKI_CD.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.HUTANKA.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.KIGYO_CD.ColNo, lblL) 'notes 1907 add

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.ON)     '要望対応:1813 yamanaka 2013.02.21 FLG_OFF → FLG_ONに変更
                .SetCellValue(i, sprDetailDef.SEIQTO_CD.ColNo, dr.Item("SEIQTO_CD").ToString())
                .SetCellValue(i, sprDetailDef.SEIQTO_NM.ColNo, dr.Item("SEIQTO_NM").ToString())
                .SetCellValue(i, sprDetailDef.KAGAMI_SHUBETU.ColNo, dr.Item("KAGAMI_KB").ToString())
                .SetCellValue(i, sprDetailDef.TOKUISAKI_CD.ColNo, dr.Item("TOKU_CD").ToString())
                .SetCellValue(i, sprDetailDef.HUTANKA.ColNo, dr.Item("HUTANKA").ToString())
                .SetCellValue(i, sprDetailDef.KIGYO_CD.ColNo, dr.Item("KIGYO_CD").ToString()) 'notes1907 add

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#End Region

#End Region

End Class
