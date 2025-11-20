' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH070G : 手入力入荷データ分報告用ＥＤＩデータ作成
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMH070Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH070G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH070F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH070F)

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
            .F11ButtonName = "保　存"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = always   '(F9) 検索
            .F10ButtonEnabled = False
            .F11ButtonEnabled = always  '(F11)確定
            .F12ButtonEnabled = always  '(F12)閉じる

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


        End With



    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

    End Sub

    '''' <summary>
    '''' コントロールの入力制御
    '''' </summary>
    '''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    '''' <remarks></remarks>
    'Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = LMH020C.MODE_DEFAULT)

    '    Dim noMnb As Boolean = True
    '    Dim dtTori As Boolean = True

    '    With Me._Frm


    '    End With

    'End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As String = "")

        With Me._Frm

            .txtInOutkaMngNo.Select()
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

    ''' <summary>
    ''' 検索結果ヘッダー部表示(EDI)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetHeaderEdi(ByVal rtnDs As DataSet, ByVal prmDs As DataSet)

        Dim drIn As DataRow = prmDs.Tables(LMH070C.TABLE_NM_IN).Rows(0)
        Dim drOut As DataRow = rtnDs.Tables(LMH070C.TABLE_NM_OUT_L).Rows(0)

        With Me._Frm

            .cmbNrsBr.SelectedValue = drIn("NRS_BR_CD")
            .cmbNrsWh.SelectedValue = drIn("WH_CD")
            .txtEdiCustCD_L.TextValue = drIn("CUST_CD_L").ToString()
            .txtEdiCustCD_M.TextValue = drIn("CUST_CD_M").ToString()
            .lblCustNM_Edi.TextValue = drOut("CUST_NM").ToString()
            .txtDestCd_Edi.TextValue = drOut("DEST_CD").ToString()
            .lblDestNM_Edi.TextValue = drOut("DEST_NM").ToString()
            .txtEdiMngNo.TextValue = drIn("EDI_CTL_NO").ToString()
            .txtOrdNo_Edi.TextValue = drOut("OUTKA_FROM_ORD_NO").ToString()
            .imdInOutkaDate_Edi.TextValue = drOut("INOUTKA_DATE").ToString()

        End With

    End Sub

    ''' <summary>
    ''' 検索結果ヘッダー部表示(EDI)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetHeaderInOutka(ByVal rtnDs As DataSet)

        Dim drOut As DataRow = rtnDs.Tables(LMH070C.TABLE_NM_INOUTKA).Rows(0)

        With Me._Frm

            .txtCustCD_L.TextValue = drOut("CUST_CD_L").ToString()
            .txtCustCD_M.TextValue = drOut("CUST_CD_M").ToString()
            .lblCustNM.TextValue = drOut("CUST_NM").ToString()
            .txtDestCd.TextValue = drOut("DEST_CD").ToString()
            .lblDestNM.TextValue = drOut("DEST_NM").ToString()
            .txtOrdNo.TextValue = drOut("OUTKA_FROM_ORD_NO").ToString()
            .imdInOutkaDate.TextValue = drOut("PLAN_DATE").ToString()

        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(左部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprGoodsInfoEdiDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.DEF, " ", 20, True)
        Public Shared LINK_NO As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.CTL_NO, "紐付け番号", 90, True)
        Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.GOODS_CD_NRS, "商品キー", 150, True)
        Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.GOODS_CD_CUST, "荷主商品" & vbCrLf & "コード", 120, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.GOODS_NM, "商品名", 150, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.LOT_NO, "ロット№", 100, True)
        Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.IRIME, "入目", 90, True)
        Public Shared IRIME_UT As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.IRIME_UT, "入目単位", 50, True)
        Public Shared NB As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.NB, "個数", 90, True)
        Public Shared EDI_NO As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.EDI_NO, "EDI管理番号", 10, False)


    End Class

    ''' <summary>
    ''' スプレッド列定義体(右部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprGoodsInfoInOutkaDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.DEF, " ", 20, True)
        Public Shared INOUTKA_CTL_NO_M As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.CTL_NO, "入出荷管理" & vbCrLf & "番号（中）", 90, True)
        Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.GOODS_CD_NRS, "商品キー", 150, True)
        Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.GOODS_CD_CUST, "荷主商品" & vbCrLf & "コード", 120, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.GOODS_NM, "商品名", 150, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.LOT_NO, "ロット№", 100, True)
        Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.IRIME, "入目", 90, True)
        Public Shared IRIME_UT As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.IRIME_UT, "入目単位", 50, True)
        Public Shared NB As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.NB, "個数", 90, True)
        Public Shared EDI_NO As SpreadColProperty = New SpreadColProperty(LMH070C.SprDtl.EDI_NO, "EDI管理番号", 10, False)


    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprGoodsInfoEdi.CrearSpread()
            .sprGoodsInfoInOutka.CrearSpread()

            '列数設定
            .sprGoodsInfoEdi.Sheets(0).ColumnCount = 10   '検索一覧(EDI)
            .sprGoodsInfoInOutka.Sheets(0).ColumnCount = 10   '検索一覧(入出荷)

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprGoodsInfoEdi.SetColProperty(New sprGoodsInfoEdiDef)
            .sprGoodsInfoInOutka.SetColProperty(New sprGoodsInfoInOutkaDef)

        End With

    End Sub

    '''' <summary>
    '''' 初期値を設定します
    '''' </summary>
    '''' <param name="frm"></param>
    '''' <remarks></remarks>
    'Friend Sub SetInitValue(ByVal frm As LMH070F)

    '    With frm.sprGoodsInfoEdi

    '        With frm.sprGoodsInfoEdi

    '            .SuspendLayout()
    '            .Sheets(0).Rows.Count = 1
    '            'データ挿入
    '            '行数設定

    '            Dim lngcnt As Integer = 4
    '            If lngcnt = 0 Then
    '                .ResumeLayout(True)
    '                Exit Sub
    '            End If
    '            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

    '            'セルに設定するスタイルの取得
    '            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(frm.sprGoodsInfoEdi, True)
    '            Dim sLabel_L As StyleInfo = LMSpreadUtility.GetLabelCell(frm.sprGoodsInfoEdi, CellHorizontalAlignment.Left)
    '            Dim sLabel_R As StyleInfo = LMSpreadUtility.GetLabelCell(frm.sprGoodsInfoEdi, CellHorizontalAlignment.Right)
    '            Dim sText As StyleInfo = LMSpreadUtility.GetTextCell(frm.sprGoodsInfoEdi, InputControl.ALL_MIX_IME_OFF, 3, False)

    '            'Dim dRow As DataRow

    '            '値設定
    '            For i As Integer = 0 To lngcnt

    '                'dRow = tbl.Rows(i - 1)

    '                'セルスタイル設定
    '                .SetCellStyle(i, sprGoodsInfoEdiDef.DEF.ColNo, sDEF)
    '                .SetCellStyle(i, sprGoodsInfoEdiDef.LINK_NO.ColNo, sText)
    '                .SetCellStyle(i, sprGoodsInfoEdiDef.GOODS_CD_NRS.ColNo, sLabel_L)
    '                .SetCellStyle(i, sprGoodsInfoEdiDef.GOODS_CD_CUST.ColNo, sLabel_L)
    '                .SetCellStyle(i, sprGoodsInfoEdiDef.GOODS_NM.ColNo, sLabel_L)
    '                .SetCellStyle(i, sprGoodsInfoEdiDef.LOT_NO.ColNo, sLabel_L)
    '                .SetCellStyle(i, sprGoodsInfoEdiDef.IRIME.ColNo, sLabel_R)
    '                .SetCellStyle(i, sprGoodsInfoEdiDef.IRIME_UT.ColNo, sLabel_L)
    '                .SetCellStyle(i, sprGoodsInfoEdiDef.NB.ColNo, sLabel_R)

    '            Next

    '            .ResumeLayout(True)

    '        End With

    '    End With

    'End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(EDI)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadEdi(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprGoodsInfoEdi

        With spr

            .SuspendLayout()
            'データ挿入
            '行数設定
            Dim dtOut As DataTable = ds.Tables(LMH070C.TABLE_NM_OUT_M)
            Dim lngcnt As Integer = dtOut.Rows.Count

            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, True)
            Dim sLabel_L As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sLabel_R As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
            Dim sText As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 3, False)
            '値設定
            For i As Integer = 0 To lngcnt - 1

                'セルスタイル設定
                .SetCellStyle(i, sprGoodsInfoEdiDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprGoodsInfoEdiDef.LINK_NO.ColNo, sText)
                .SetCellStyle(i, sprGoodsInfoEdiDef.GOODS_CD_NRS.ColNo, sLabel_L)
                .SetCellStyle(i, sprGoodsInfoEdiDef.GOODS_CD_CUST.ColNo, sLabel_L)
                .SetCellStyle(i, sprGoodsInfoEdiDef.GOODS_NM.ColNo, sLabel_L)
                .SetCellStyle(i, sprGoodsInfoEdiDef.LOT_NO.ColNo, sLabel_L)
                .SetCellStyle(i, sprGoodsInfoEdiDef.IRIME.ColNo, sLabel_R)
                .SetCellStyle(i, sprGoodsInfoEdiDef.IRIME_UT.ColNo, sLabel_L)
                .SetCellStyle(i, sprGoodsInfoEdiDef.NB.ColNo, sLabel_R)
                .SetCellStyle(i, sprGoodsInfoEdiDef.EDI_NO.ColNo, sLabel_R)


                'セルに値を設定
                .SetCellValue(i, sprGoodsInfoEdiDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprGoodsInfoEdiDef.GOODS_CD_NRS.ColNo, dtOut.Rows(i)("NRS_GOODS_CD").ToString())
                .SetCellValue(i, sprGoodsInfoEdiDef.GOODS_CD_CUST.ColNo, dtOut.Rows(i)("CUST_GOODS_CD").ToString())
                .SetCellValue(i, sprGoodsInfoEdiDef.GOODS_NM.ColNo, dtOut.Rows(i)("GOODS_NM").ToString())
                .SetCellValue(i, sprGoodsInfoEdiDef.LOT_NO.ColNo, dtOut.Rows(i)("LOT_NO").ToString())
                .SetCellValue(i, sprGoodsInfoEdiDef.IRIME.ColNo, dtOut.Rows(i)("IRIME").ToString())
                .SetCellValue(i, sprGoodsInfoEdiDef.IRIME_UT.ColNo, dtOut.Rows(i)("IRIME_UT").ToString())
                .SetCellValue(i, sprGoodsInfoEdiDef.NB.ColNo, dtOut.Rows(i)("NB").ToString())
                .SetCellValue(i, sprGoodsInfoEdiDef.EDI_NO.ColNo, dtOut.Rows(i)("EDI_CTL_NO_CHU").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(入出荷)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadInOutka(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprGoodsInfoInOutka

        With spr

            .SuspendLayout()
            'データ挿入
            '行数設定
            Dim dtOut As DataTable = ds.Tables(LMH070C.TABLE_NM_INOUTKA)
            Dim lngcnt As Integer = dtOut.Rows.Count()

            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, True)
            Dim sLabel_L As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sLabel_R As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            '値設定
            For i As Integer = 0 To lngcnt - 1

                'セルスタイル設定
                .SetCellStyle(i, sprGoodsInfoInOutkaDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprGoodsInfoInOutkaDef.INOUTKA_CTL_NO_M.ColNo, sLabel_L)
                .SetCellStyle(i, sprGoodsInfoInOutkaDef.GOODS_CD_NRS.ColNo, sLabel_L)
                .SetCellStyle(i, sprGoodsInfoInOutkaDef.GOODS_CD_CUST.ColNo, sLabel_L)
                .SetCellStyle(i, sprGoodsInfoInOutkaDef.GOODS_NM.ColNo, sLabel_L)
                .SetCellStyle(i, sprGoodsInfoInOutkaDef.LOT_NO.ColNo, sLabel_L)
                .SetCellStyle(i, sprGoodsInfoInOutkaDef.IRIME.ColNo, sLabel_R)
                .SetCellStyle(i, sprGoodsInfoInOutkaDef.IRIME_UT.ColNo, sLabel_L)
                .SetCellStyle(i, sprGoodsInfoInOutkaDef.NB.ColNo, sLabel_R)
                .SetCellStyle(i, sprGoodsInfoInOutkaDef.EDI_NO.ColNo, sLabel_R)


                'セルに値を設定
                .SetCellValue(i, sprGoodsInfoInOutkaDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprGoodsInfoInOutkaDef.INOUTKA_CTL_NO_M.ColNo, dtOut.Rows(i)("INOUTKA_NO_M").ToString())
                .SetCellValue(i, sprGoodsInfoInOutkaDef.GOODS_CD_NRS.ColNo, dtOut.Rows(i)("GOODS_CD_NRS").ToString())
                .SetCellValue(i, sprGoodsInfoInOutkaDef.GOODS_CD_CUST.ColNo, dtOut.Rows(i)("GOODS_CD_CUST").ToString())
                .SetCellValue(i, sprGoodsInfoInOutkaDef.GOODS_NM.ColNo, dtOut.Rows(i)("GOODS_NM").ToString())
                .SetCellValue(i, sprGoodsInfoInOutkaDef.LOT_NO.ColNo, dtOut.Rows(i)("LOT_NO").ToString())
                .SetCellValue(i, sprGoodsInfoInOutkaDef.IRIME.ColNo, dtOut.Rows(i)("IRIME").ToString())
                .SetCellValue(i, sprGoodsInfoInOutkaDef.IRIME_UT.ColNo, dtOut.Rows(i)("IRIME_UT").ToString())
                .SetCellValue(i, sprGoodsInfoInOutkaDef.NB.ColNo, dtOut.Rows(i)("NB").ToString())
                .SetCellValue(i, sprGoodsInfoInOutkaDef.EDI_NO.ColNo, dtOut.Rows(i)("EDI_CTL_NO").ToString())


            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 紐付番号仮設定
    ''' </summary>
    ''' <param name="ediRow"></param>
    ''' <remarks></remarks>
    Public Sub SetLinkNo(ByVal ediRow As Integer, ByVal inoutNo As String)

        Dim ediSpr As LMSpread = Me._Frm.sprGoodsInfoEdi
        Dim inoutSpr As LMSpread = Me._Frm.sprGoodsInfoInOutka

        With ediSpr
            .SetCellValue(ediRow, sprGoodsInfoEdiDef.LINK_NO.ColNo, inoutNo)

        End With

    End Sub

    ''' <summary>
    ''' 紐付番号クリア
    ''' </summary>
    ''' <param name="ediRow"></param>
    ''' <remarks></remarks>
    Public Sub ClearLinkNo(ByVal ediRow As Integer)

        Me._Frm.sprGoodsInfoEdi.SetCellValue(ediRow, sprGoodsInfoEdiDef.LINK_NO.ColNo, String.Empty)

    End Sub

#End Region 'Spread

#End Region

End Class
