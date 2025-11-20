' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME030  : 作業指示書検索
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LME030Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LME030G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LME030F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LME030F)

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
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True
        Dim edit As Boolean = False
        Dim view As Boolean = False
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = LME030C.FUNCTION_SINKI
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = LME030C.EVENTNAME_COMPLETE
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LME030C.FUNCTION_KENSAKU
            .F10ButtonName = LME030C.FUNCTION_MASTER
            .F11ButtonName = String.Empty
            .F12ButtonName = LME030C.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = always
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = always
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
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

#End Region 'FunctionKey

#Region "Mode&Status"

    ''' <summary>
    ''' Dispモードとレコードステータスの設定
    ''' </summary>
    ''' <param name="mode">Dispモード</param>
    ''' <param name="status">レコードステータス</param>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(ByVal mode As String, ByVal status As String)

        With Me._Frm

        End With

    End Sub

#End Region 'Mode&Status

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .grpSearch.TabIndex = LME030C.CtlTabIndex.GRPSERCH
            .cmbEigyo.TabIndex = LME030C.CtlTabIndex.EIGYO
            .txtCustCdL.TabIndex = LME030C.CtlTabIndex.CUSTCDL
            .txtCustCdM.TabIndex = LME030C.CtlTabIndex.CUSTCDM
            .imdDateFrom.TabIndex = LME030C.CtlTabIndex.DATEFROM
            .imdDateTo.TabIndex = LME030C.CtlTabIndex.DATETO
            .sprDetails.TabIndex = LME030C.CtlTabIndex.SPRDETAILS

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        '自営業所の設定
        Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
        Me._Frm.cmbEigyo.SelectedValue = brCd

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .cmbEigyo.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

        End With

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailsDef

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SAGYOSIJINO As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.SAGYOSIJINO, "作業指示書番号", 110, True)
        Public Shared SAGYODATE As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.SAGYODATE, "作業日", 80, True)
        Public Shared CUSTNM As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.CUSTNM, "荷主名", 200, True)
        Public Shared GOODSNM As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.GOODSNM, "商品名", 200, True)
        Public Shared SAGYONM As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.SAGYONM, "作業内容", 200, True)
        Public Shared USERNM As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.USERNM, "作成者", 100, True)
        Public Shared NRSBRCD As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.NRSBRCD, "営業所", 0, False)
        Public Shared CUSTCDL As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.CUSTCDL, "荷主コード(大)", 0, False)
        Public Shared CUSTCDM As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.CUSTCDM, "荷主コード(中)", 0, False)
        Public Shared WHTABSTATUSNM As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.WHTABSTATUSNM, "現場作業指示", 100, True)
        Public Shared SYSUPDDATE As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.SYSUPDDATE, "更新年月日", 0, False)
        Public Shared SYSUPDTIME As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.SYSUPDTIME, "更新時間", 0, False)
        Public Shared SAGYOSIJISTATUSNM As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.SAGYOSIJISTATUSNM, "作業進捗", 70, True)
        Public Shared SAGYOSIJISTATUS As SpreadColProperty = New SpreadColProperty(LME030C.SprColumnIndex.SAGYOSIJISTATUS, "作業進捗区分", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetails.CrearSpread()

            '列数設定
            .sprDetails.Sheets(0).ColumnCount = LME030C.SprColumnIndex.LAST

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetails.SetColProperty(New sprDetailsDef)
            .sprDetails.SetColProperty(New LME030G.sprDetailsDef(), False)
            '2015.10.15 英語化対応END

            '列設定
            .sprDetails.SetCellStyle(0, sprDetailsDef.SAGYOSIJINO.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX_IME_OFF, 10, False))
            .sprDetails.SetCellStyle(0, sprDetailsDef.SAGYODATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetails, True))
            .sprDetails.SetCellStyle(0, sprDetailsDef.CUSTNM.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX, 60, False))
            .sprDetails.SetCellStyle(0, sprDetailsDef.GOODSNM.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX_IME_OFF, 60, False))
            .sprDetails.SetCellStyle(0, sprDetailsDef.SAGYONM.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX, 60, False))
            .sprDetails.SetCellStyle(0, sprDetailsDef.USERNM.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX, 60, True))
            .sprDetails.SetCellStyle(0, sprDetailsDef.NRSBRCD.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX, 2, True))
            .sprDetails.SetCellStyle(0, sprDetailsDef.CUSTCDL.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX_IME_OFF, 5, True))
            .sprDetails.SetCellStyle(0, sprDetailsDef.CUSTCDM.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX_IME_OFF, 2, True))
            .sprDetails.SetCellStyle(0, sprDetailsDef.WHTABSTATUSNM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetails, "S118", False))
            .sprDetails.SetCellStyle(0, sprDetailsDef.SYSUPDDATE.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX, 8, False))
            .sprDetails.SetCellStyle(0, sprDetailsDef.SYSUPDTIME.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX, 9, False))
            .sprDetails.SetCellStyle(0, sprDetailsDef.SAGYOSIJISTATUSNM.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX, 2, True))
            .sprDetails.SetCellStyle(0, sprDetailsDef.SAGYOSIJISTATUS.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX, 0, False))
        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpread = Me._Frm.sprDetails
        Dim lngcnt As Integer = dt.Rows.Count - 1
        Dim rowCnt As Integer = 0

        'セルに設定するスタイルの取得
        Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
        Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

        With spr

            .SuspendLayout()

            If lngcnt = -1 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            'SPREAD(表示行)初期化
            .CrearSpread()

            '値設定
            For i As Integer = 0 To lngcnt

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                .SetCellStyle(rowCnt, sprDetailsDef.DEF.ColNo, sDEF)
                .SetCellStyle(rowCnt, sprDetailsDef.SAGYOSIJINO.ColNo, sLabel)    '作業指示書番号
                .SetCellStyle(rowCnt, sprDetailsDef.SAGYODATE.ColNo, sLabel)      '作業日
                .SetCellStyle(rowCnt, sprDetailsDef.CUSTNM.ColNo, sLabel)         '荷主名
                .SetCellStyle(rowCnt, sprDetailsDef.GOODSNM.ColNo, sLabel)        '商品名
                .SetCellStyle(rowCnt, sprDetailsDef.SAGYONM.ColNo, sLabel)        '作業内容
                .SetCellStyle(rowCnt, sprDetailsDef.USERNM.ColNo, sLabel)         '作成者
                .SetCellStyle(rowCnt, sprDetailsDef.NRSBRCD.ColNo, sLabel)        '営業所コード
                .SetCellStyle(rowCnt, sprDetailsDef.CUSTCDL.ColNo, sLabel)        '荷主コード(大)
                .SetCellStyle(rowCnt, sprDetailsDef.CUSTCDM.ColNo, sLabel)        '荷主コード(中)
                .SetCellStyle(rowCnt, sprDetailsDef.WHTABSTATUSNM.ColNo, sLabel)    '現場作業指示
                .SetCellStyle(rowCnt, sprDetailsDef.SYSUPDDATE.ColNo, sLabel)     '更新年月日
                .SetCellStyle(rowCnt, sprDetailsDef.SYSUPDTIME.ColNo, sLabel)     '更新時間
                .SetCellStyle(rowCnt, sprDetailsDef.SAGYOSIJISTATUSNM.ColNo, sLabel)     '作業進捗
                .SetCellStyle(rowCnt, sprDetailsDef.SAGYOSIJISTATUS.ColNo, sLabel)     '作業進捗
                'セルに値を設定
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.DEF.ColNo, False.ToString())
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.SAGYOSIJINO.ColNo, dt.Rows(i).Item("SAGYO_SIJI_NO").ToString()) '作業指示書番号
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.SAGYODATE.ColNo, DateFormatUtility.EditSlash(dt.Rows(i).Item("SAGYO_COMP_DATE").ToString())) '作業日
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.CUSTNM.ColNo, dt.Rows(i).Item("CUST_NM_L").ToString()) '荷主名
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.GOODSNM.ColNo, dt.Rows(i).Item("GOODS_NM").ToString()) '商品名
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.SAGYONM.ColNo, dt.Rows(i).Item("SAGYO_NM").ToString()) '作業内容
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.USERNM.ColNo, dt.Rows(i).Item("USER_NM").ToString()) '作成者
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.NRSBRCD.ColNo, dt.Rows(i).Item("NRS_BR_CD").ToString()) '営業所コード
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.CUSTCDL.ColNo, dt.Rows(i).Item("CUST_CD_L").ToString()) '荷主コード(大)
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.CUSTCDM.ColNo, dt.Rows(i).Item("CUST_CD_M").ToString()) '荷主コード(中)
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.WHTABSTATUSNM.ColNo, dt.Rows(i).Item("WH_TAB_STATUS_NM").ToString()) '現場作業指示
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.SYSUPDDATE.ColNo, dt.Rows(i).Item("SYS_UPD_DATE").ToString()) '更新年月日
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.SYSUPDTIME.ColNo, dt.Rows(i).Item("SYS_UPD_TIME").ToString()) '更新時間
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.SAGYOSIJISTATUSNM.ColNo, dt.Rows(i).Item("SAGYO_SIJI_STATUS_NM").ToString()) '作業進捗
                .SetCellValue(rowCnt, LME030G.sprDetailsDef.SAGYOSIJISTATUS.ColNo, dt.Rows(i).Item("SAGYO_SIJI_STATUS").ToString()) '作業進捗
            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#Region "ユーティリティ"

#Region "プロパティ"


#End Region

#Region "前埋め設定"

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

#End Region

#End Region

End Class
