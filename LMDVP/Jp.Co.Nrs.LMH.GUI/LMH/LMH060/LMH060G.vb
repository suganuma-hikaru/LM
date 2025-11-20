' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH060  : EDI出荷データ荷主コード設定
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
''' LMH060Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH060G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH060F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH060F)

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
            .F1ButtonName = String.Empty
            .F2ButtonName = LMH060C.FUNCTION_SET
            .F3ButtonName = LMH060C.FUNCTION_CANCEL
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMH060C.FUNCTION_KENSAKU
            .F10ButtonName = LMH060C.FUNCTION_MASTER
            .F11ButtonName = LMH060C.FUNCTION_SAVE
            .F12ButtonName = LMH060C.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = always
            .F3ButtonEnabled = always
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = always
            .F12ButtonEnabled = always

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

            .grpSet.TabIndex = LMH060C.CtlTabIndex.GRPSERCH
            .cmbEigyo.TabIndex = LMH060C.CtlTabIndex.EIGYO
            .txtCustCdL.TabIndex = LMH060C.CtlTabIndex.CUSTCDL
            .txtCustCdM.TabIndex = LMH060C.CtlTabIndex.CUSTCDM
            .sprDetails.TabIndex = LMH060C.CtlTabIndex.SPRDETAILS

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String, ByVal ds As DataSet)

        Dim custDr() As DataRow = Nothing

        '自営業所の設定
        Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
        Me._Frm.cmbEigyo.SelectedValue = brCd

        With Me._Frm

            If ds.Tables(LMH060C.TABLE_NM_IN).Rows.Count > 0 Then
                If String.IsNullOrEmpty(ds.Tables(LMH060C.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString) = False Then
                    .cmbEigyo.SelectedValue = ds.Tables(LMH060C.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString
                End If
                If String.IsNullOrEmpty(ds.Tables(LMH060C.TABLE_NM_IN).Rows(0).Item("CUST_CD_L").ToString) = False Then
                    .txtCustCdL.TextValue = ds.Tables(LMH060C.TABLE_NM_IN).Rows(0).Item("CUST_CD_L").ToString
                End If
                If String.IsNullOrEmpty(ds.Tables(LMH060C.TABLE_NM_IN).Rows(0).Item("CUST_CD_M").ToString) = False Then
                    .txtCustCdM.TextValue = ds.Tables(LMH060C.TABLE_NM_IN).Rows(0).Item("CUST_CD_M").ToString
                End If

                '荷主名称
                custDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                 "CUST_CD_M = '", .txtCustCdM.TextValue, "'"))
                If 0 < custDr.Length Then
                    .lblCustNmL.TextValue = custDr(0).Item("CUST_NM_L").ToString
                    .lblCustNmM.TextValue = custDr(0).Item("CUST_NM_M").ToString
                End If

            End If

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

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMH060C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared CUSTCD As SpreadColProperty = New SpreadColProperty(LMH060C.SprColumnIndex.CUSTCD, "荷主コード", 90, True)
        Public Shared CUSTNM As SpreadColProperty = New SpreadColProperty(LMH060C.SprColumnIndex.CUSTNM, "荷主名", 200, True)
        Public Shared EDICTLNO As SpreadColProperty = New SpreadColProperty(LMH060C.SprColumnIndex.EDICTLNO, "EDI番号", 80, True)
        Public Shared OUTKAPLANDATE As SpreadColProperty = New SpreadColProperty(LMH060C.SprColumnIndex.OUTKAPLANDATE, "出荷予定日", 80, True)
        Public Shared DESTCD As SpreadColProperty = New SpreadColProperty(LMH060C.SprColumnIndex.DESTCD, "届先コード", 90, True)
        Public Shared DESTNM As SpreadColProperty = New SpreadColProperty(LMH060C.SprColumnIndex.DESTNM, "届先名", 200, True)
        Public Shared ZBUKACD As SpreadColProperty = New SpreadColProperty(LMH060C.SprColumnIndex.ZBUKACD, "在庫部課コード", 100, True)
        Public Shared CUSTCDUPD As SpreadColProperty = New SpreadColProperty(LMH060C.SprColumnIndex.CUSTCDUPD, "対象荷主コード", 0, False)
        Public Shared RCVNMHED As SpreadColProperty = New SpreadColProperty(LMH060C.SprColumnIndex.RCVNMHED, "更新対象荷主テーブル名", 0, False)
        Public Shared SYSUPDDATE As SpreadColProperty = New SpreadColProperty(LMH060C.SprColumnIndex.SYSUPDDATE, "更新日", 0, False)
        Public Shared SYSUPDTIME As SpreadColProperty = New SpreadColProperty(LMH060C.SprColumnIndex.SYSUPDTIME, "更新時間", 0, False)

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
            .sprDetails.Sheets(0).ColumnCount = LMH060C.SprColumnIndex.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetails.SetColProperty(New sprDetailsDef)

            '列設定
            .sprDetails.SetCellStyle(0, sprDetailsDef.CUSTCD.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX_IME_OFF, 7, False))
            .sprDetails.SetCellStyle(0, sprDetailsDef.CUSTNM.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX, 60, False))
            .sprDetails.SetCellStyle(0, sprDetailsDef.EDICTLNO.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX_IME_OFF, 9, False))
            .sprDetails.SetCellStyle(0, sprDetailsDef.OUTKAPLANDATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetails, True))
            .sprDetails.SetCellStyle(0, sprDetailsDef.DESTCD.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX_IME_OFF, 15, False))
            .sprDetails.SetCellStyle(0, sprDetailsDef.DESTNM.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX, 60, False))
            .sprDetails.SetCellStyle(0, sprDetailsDef.ZBUKACD.ColNo, LMSpreadUtility.GetTextCell(.sprDetails, InputControl.ALL_MIX_IME_OFF, 7, False))

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
                .SetCellStyle(rowCnt, sprDetailsDef.CUSTCD.ColNo, sLabel)    '荷主コード
                .SetCellStyle(rowCnt, sprDetailsDef.CUSTNM.ColNo, sLabel)    '荷主名
                .SetCellStyle(rowCnt, sprDetailsDef.EDICTLNO.ColNo, sLabel)    'EDI番号
                .SetCellStyle(rowCnt, sprDetailsDef.OUTKAPLANDATE.ColNo, sLabel)    '出荷予定日
                .SetCellStyle(rowCnt, sprDetailsDef.DESTCD.ColNo, sLabel)    '届先コード
                .SetCellStyle(rowCnt, sprDetailsDef.DESTNM.ColNo, sLabel)    '届先名
                .SetCellStyle(rowCnt, sprDetailsDef.ZBUKACD.ColNo, sLabel)    '在庫部課コード
                .SetCellStyle(rowCnt, sprDetailsDef.CUSTCDUPD.ColNo, sLabel)    '対象荷主コード
                .SetCellStyle(rowCnt, sprDetailsDef.RCVNMHED.ColNo, sLabel)    '更新対象荷主テーブル名
                .SetCellStyle(rowCnt, sprDetailsDef.SYSUPDDATE.ColNo, sLabel)    '更新日
                .SetCellStyle(rowCnt, sprDetailsDef.SYSUPDTIME.ColNo, sLabel)    '更新時間

                'セルに値を設定
                .SetCellValue(rowCnt, LMH060G.sprDetailsDef.DEF.ColNo, False.ToString())
                .SetCellValue(rowCnt, LMH060G.sprDetailsDef.CUSTCD.ColNo, String.Concat(dt.Rows(i).Item("CUST_CD_L").ToString(), "-", dt.Rows(i).Item("CUST_CD_M").ToString())) '荷主コード
                .SetCellValue(rowCnt, LMH060G.sprDetailsDef.CUSTNM.ColNo, dt.Rows(i).Item("CUST_NM_L").ToString()) '荷主名
                .SetCellValue(rowCnt, LMH060G.sprDetailsDef.EDICTLNO.ColNo, dt.Rows(i).Item("EDI_CTL_NO").ToString()) 'EDI番号
                .SetCellValue(rowCnt, LMH060G.sprDetailsDef.OUTKAPLANDATE.ColNo, DateFormatUtility.EditSlash(dt.Rows(i).Item("OUTKA_PLAN_DATE").ToString())) '出荷予定日
                .SetCellValue(rowCnt, LMH060G.sprDetailsDef.DESTCD.ColNo, dt.Rows(i).Item("DEST_CD").ToString()) '届先コード
                .SetCellValue(rowCnt, LMH060G.sprDetailsDef.DESTNM.ColNo, dt.Rows(i).Item("DEST_NM").ToString()) '届先名
                .SetCellValue(rowCnt, LMH060G.sprDetailsDef.ZBUKACD.ColNo, dt.Rows(i).Item("ZBUKACD").ToString()) '在庫部課コード
                .SetCellValue(rowCnt, LMH060G.sprDetailsDef.CUSTCDUPD.ColNo, dt.Rows(i).Item("CUST_CD_UPD").ToString()) '対象荷主コード
                .SetCellValue(rowCnt, LMH060G.sprDetailsDef.RCVNMHED.ColNo, dt.Rows(i).Item("RCV_NM_HED").ToString()) '更新対象荷主テーブル名
                .SetCellValue(rowCnt, LMH060G.sprDetailsDef.SYSUPDDATE.ColNo, dt.Rows(i).Item("SYS_UPD_DATE").ToString()) '更新日
                .SetCellValue(rowCnt, LMH060G.sprDetailsDef.SYSUPDTIME.ColNo, dt.Rows(i).Item("SYS_UPD_TIME").ToString()) '更新時間

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
