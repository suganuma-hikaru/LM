' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ     : ｼｽﾃﾑ管理
'  プログラムID     :  LMJ020G : 未使用荷主データ退避
'  作  成  者       :  s.kobayashi
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMJ020Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMJ020G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMJ020F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMJconG As LMJControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMJ020F, ByVal g As LMJControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMJconG = g

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
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = LMJControlC.FUNCTION_CREATE
            .F8ButtonName = String.Empty
            .F9ButtonName = LMJControlC.FUNCTION_SEARCH
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = LMJControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = always
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = always
            .F10ButtonEnabled = lock
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

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

            .cmbShori.TabIndex = LMJ020C.CtlTabIndex.SHORI
            .cmbEigyo.TabIndex = LMJ020C.CtlTabIndex.EIGYO
            .imdLastUpdDate.TabIndex = LMJ020C.CtlTabIndex.LastUpdDate

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        '日付コントロールの書式設定
        Call Me._LMJconG.SetDateFormat(Me._Frm.imdLastUpdDate)

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(ByVal ds As DataSet, ByVal nowDate As String)

        With Me._Frm

            Dim lock As Boolean = True

            'ロック制御
            Me._LMJconG.SetLockInputMan(.cmbEigyo, lock)

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            'フォーカス位置の初期化
            .Focus()

            .cmbShori.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbShori.SelectedValue = Nothing
            .cmbEigyo.SelectedValue = Nothing
            .imdLastUpdDate.Value = Nothing

        End With

    End Sub

    ''' <summary>
    ''' 初期値の設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        With Me._Frm

            '自営業を設定
            .cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()

            '処理内容の設定
            .cmbShori.SelectedValue = LMJ020C.SHORI_ESCAPE

            '日付
            .imdLastUpdDate.TextValue = Format(Now, "yyyyMMdd")
        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMJ020C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMJ020C.SprColumnIndex.CUST_CD_L, "荷主コード" & vbCrLf & "(大)", 90, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMJ020C.SprColumnIndex.CUST_NM_L, "荷主名(大)", 324, True)
        Public Shared LAST_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMJ020C.SprColumnIndex.LAST_UPD_DATE, "最終更新日", 80, True)
        Public Shared TANTO_USER As SpreadColProperty = New SpreadColProperty(LMJ020C.SprColumnIndex.TANTO_USER, "担当者", 120, True)
        Public Shared TAIHI_DATE As SpreadColProperty = New SpreadColProperty(LMJ020C.SprColumnIndex.TAIHI_DATE, "退避日", 80, True)
        Public Shared TAIHI_USER As SpreadColProperty = New SpreadColProperty(LMJ020C.SprColumnIndex.TAIHI_USER, "退避実行者", 120, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMJ020C.SprColumnIndex.NRS_BR_CD, "営業所コード", 90, False)
        Public Shared PROCESS_KB As SpreadColProperty = New SpreadColProperty(LMJ020C.SprColumnIndex.PROCESS_KB, "処理区分", 324, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(Optional ByVal lock As Boolean = True)

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = 9

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef)

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMJ020G.sprDetailDef.DEF.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, LMJ020G.sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMJ020G.sprDetailDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMJ020G.sprDetailDef.LAST_UPD_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail, lock, CellType.DateTimeFormat.ShortDate))
            .sprDetail.SetCellStyle(0, LMJ020G.sprDetailDef.TANTO_USER.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMJ020G.sprDetailDef.TAIHI_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail, lock, CellType.DateTimeFormat.ShortDate))
            .sprDetail.SetCellStyle(0, LMJ020G.sprDetailDef.TAIHI_USER.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMJ020G.sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMJ020G.sprDetailDef.PROCESS_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

        End With

    End Sub

    ''' <summary>
    ''' SPREAD初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadInitValue()

        With _Frm.sprDetail.ActiveSheet

            .Cells(0, LMJ020G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .Cells(0, LMJ020G.sprDetailDef.CUST_CD_L.ColNo).Value = String.Empty
            .Cells(0, LMJ020G.sprDetailDef.CUST_NM_L.ColNo).Value = String.Empty
            .Cells(0, LMJ020G.sprDetailDef.LAST_UPD_DATE.ColNo).Value = String.Empty
            .Cells(0, LMJ020G.sprDetailDef.TANTO_USER.ColNo).Value = String.Empty
            .Cells(0, LMJ020G.sprDetailDef.TAIHI_DATE.ColNo).Value = String.Empty
            .Cells(0, LMJ020G.sprDetailDef.TAIHI_USER.ColNo).Value = String.Empty
            .Cells(0, LMJ020G.sprDetailDef.NRS_BR_CD.ColNo).Value = String.Empty
            .Cells(0, LMJ020G.sprDetailDef.PROCESS_KB.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As DataSet = New DataSet()

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

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            Dim dRow As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dRow = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.CUST_CD_L.ColNo, sLabel)  '荷主コード(大)
                .SetCellStyle(i, sprDetailDef.CUST_NM_L.ColNo, sLabel)  '荷主名(大)
                .SetCellStyle(i, sprDetailDef.LAST_UPD_DATE.ColNo, sLabel)  '最終更新日
                .SetCellStyle(i, sprDetailDef.TANTO_USER.ColNo, sLabel)  '担当者
                .SetCellStyle(i, sprDetailDef.TAIHI_DATE.ColNo, sLabel)  '退避日
                .SetCellStyle(i, sprDetailDef.TAIHI_USER.ColNo, sLabel)   '退避ユーザ
                .SetCellStyle(i, sprDetailDef.NRS_BR_CD.ColNo, sLabel)  '退避日
                .SetCellStyle(i, sprDetailDef.PROCESS_KB.ColNo, sLabel)   '退避ユーザ

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.CUST_CD_L.ColNo, dRow.Item("CUST_CD_L").ToString)
                .SetCellValue(i, sprDetailDef.CUST_NM_L.ColNo, dRow.Item("CUST_NM_L").ToString)
                .SetCellValue(i, sprDetailDef.LAST_UPD_DATE.ColNo, DateFormatUtility.EditSlash(dRow.Item("LAST_UPD_DATE").ToString))
                .SetCellValue(i, sprDetailDef.TANTO_USER.ColNo, dRow.Item("TANTO_USER").ToString)
                .SetCellValue(i, sprDetailDef.TAIHI_DATE.ColNo, DateFormatUtility.EditSlash(dRow.Item("TAIHI_DATE").ToString))
                .SetCellValue(i, sprDetailDef.TAIHI_USER.ColNo, dRow.Item("TAIHI_USER").ToString)
                .SetCellValue(i, sprDetailDef.NRS_BR_CD.ColNo, dRow.Item("NRS_BR_CD").ToString)
                .SetCellValue(i, sprDetailDef.PROCESS_KB.ColNo, dRow.Item("PROCESS_KB").ToString)
            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#End Region

#End Region

End Class
