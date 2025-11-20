' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMH     : EDIサブシステム
'  プログラムID   : LMH090G : 現品票印刷
'  作  成  者     : 
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
''' LMH090Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH090G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH090F

    Friend objSprDef As Object = Nothing
    Friend sprEdiListDef As sprEdiListDefault


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH090F)

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
            .F7ButtonName = "印　刷"
            .F8ButtonName = String.Empty
            .F9ButtonName = LMHControlC.FUNCTION_KENSAKU
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = LMHControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = True
            .F8ButtonEnabled = False
            .F9ButtonEnabled = True
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
            .F12ButtonEnabled = True

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

            .sprEdiList.TabIndex = LMH090C.CtlTabIndex_MAIN.SPR_MAIN

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
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        Dim noMnb As Boolean = True
        Dim dtTori As Boolean = True

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As String = "")

        With Me._Frm

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

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="dt">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal dt As DataTable)

        'スプレッド設定
        Call Me.SetSpread(dt)

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprEdiListDefault

        'スプレッド(タイトル列)の設定
        Public DEF As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.DEF, " ", 20, True)
        Public OUTKA_FROM_ORD_NO As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.OUTKA_FROM_ORD_NO, "オーダー番号", 100, True)
        Public INKA_DATE As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.INKA_DATE, "入荷日", 100, True)
        Public CUST_GOODS_CD As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.CUST_GOODS_CD, "商品" & vbCrLf & "コード", 100, True)
        Public GOODS_NM As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.GOODS_NM, "商品名", 300, True)
        Public LOT_NO As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.LOT_NO, "ロット№", 100, True)
        Public STD_IRIME As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.STD_IRIME, "標準入目", 125, True)
        Public NET As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.NET, "内容量", 125, True)
        Public NB As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.NB, "ラベル枚数", 125, True)

        '非表示列
        Public CRT_DATE As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.CRT_DATE, "", 20, False)
        Public FILE_NAME As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.FILE_NAME, "", 20, False)
        Public REC_NO As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.REC_NO, "", 20, False)
        Public HED_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.HED_UPD_DATE, "", 20, False)
        Public HED_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.HED_UPD_TIME, "", 20, False)
        Public GYO As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.GYO, "", 20, False)
        Public DTL_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.DTL_UPD_DATE, "", 20, False)
        Public DTL_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.DTL_UPD_TIME, "", 20, False)
        Public GENPINHYO_CHKFLG As SpreadColProperty = New SpreadColProperty(LMH090C.SprColumnIndex.GENPINHYO_CHKFLG, "", 20, False)    'ADD 2019/12/18 009991

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprEdiList.CrearSpread()

            '列数設定
            .sprEdiList.Sheets(0).ColumnCount = LMH090C.SprColumnIndex.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            objSprDef = New sprEdiListDefault
            .sprEdiList.SetColProperty(objSprDef, False)
            sprEdiListDef = DirectCast(objSprDef, LMH090G.sprEdiListDefault)

            '列設定
            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUTKA_FROM_ORD_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX_IME_OFF, 10, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.INKA_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.CUST_GOODS_CD.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX_IME_OFF, 10, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.GOODS_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprEdiList))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX_IME_OFF, 10, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.STD_IRIME.ColNo, LMSpreadUtility.GetNumberCell(.sprEdiList, 0.001, 999999999.999, False, 3, , ","))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.NET.ColNo, LMSpreadUtility.GetLabelCell(.sprEdiList))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.NB.ColNo, LMSpreadUtility.GetLabelCell(.sprEdiList))

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMH090F, ByVal ds As DataSet)

        With frm.sprEdiList

            .SetCellValue(0, sprEdiListDef.OUTKA_FROM_ORD_NO.ColNo, ds.Tables("LMH090_IN")(0).Item("OUTKA_FROM_ORD_NO").ToString)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpread = Me._Frm.sprEdiList

        With spr

            .SuspendLayout()
            .Sheets(0).Rows.Count = 1
            'データ挿入
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count()
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            '行の追加
            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設
                .SetCellStyle(i, sprEdiListDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprEdiListDef.OUTKA_FROM_ORD_NO.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.INKA_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(spr, True))

                .SetCellStyle(i, sprEdiListDef.CUST_GOODS_CD.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.LOT_NO.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.STD_IRIME.ColNo, LMSpreadUtility.GetNumberCell(spr, 0.001, 999999999.999, True, 3, , ","))
                .SetCellStyle(i, sprEdiListDef.NET.ColNo, LMSpreadUtility.GetNumberCell(spr, 0.001, 999999999.999, True, 3, , ","))
                .SetCellStyle(i, sprEdiListDef.NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ","))

                .SetCellStyle(i, sprEdiListDef.CRT_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.FILE_NAME.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.REC_NO.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.HED_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.HED_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.GYO.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.DTL_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.DTL_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.GENPINHYO_CHKFLG.ColNo, sLabel)      'ADD 2019/12/18 009991

                'セルに値を設定
                .SetCellValue(i, sprEdiListDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprEdiListDef.OUTKA_FROM_ORD_NO.ColNo, dr.Item("OUTKA_FROM_ORD_NO").ToString)
                .SetCellValue(i, sprEdiListDef.INKA_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("INKA_DATE").ToString))
                .SetCellValue(i, sprEdiListDef.CUST_GOODS_CD.ColNo, dr.Item("CUST_GOODS_CD").ToString)
                .SetCellValue(i, sprEdiListDef.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString)
                .SetCellValue(i, sprEdiListDef.LOT_NO.ColNo, dr.Item("LOT_NO").ToString)
                .SetCellValue(i, sprEdiListDef.STD_IRIME.ColNo, dr.Item("STD_IRIME").ToString)
                .SetCellValue(i, sprEdiListDef.NET.ColNo, dr.Item("ZFVYSURYO").ToString)
                '個数計算
                Dim nb As Decimal
                Try
                    nb = CDec(dr.Item("ZFVYBRGEW").ToString) / CDec(dr.Item("STD_IRIME").ToString)
                    If nb Mod 1D <> 0 Then
                        nb = 1
                    End If
                Catch ex As Exception
                    nb = 1
                End Try
                .SetCellValue(i, sprEdiListDef.NB.ColNo, nb.ToString)

                .SetCellValue(i, sprEdiListDef.CRT_DATE.ColNo, dr.Item("CRT_DATE").ToString)
                .SetCellValue(i, sprEdiListDef.FILE_NAME.ColNo, dr.Item("FILE_NAME").ToString)
                .SetCellValue(i, sprEdiListDef.REC_NO.ColNo, dr.Item("REC_NO").ToString)
                .SetCellValue(i, sprEdiListDef.HED_UPD_DATE.ColNo, dr.Item("HED_UPD_DATE").ToString)
                .SetCellValue(i, sprEdiListDef.HED_UPD_TIME.ColNo, dr.Item("HED_UPD_TIME").ToString)
                .SetCellValue(i, sprEdiListDef.GYO.ColNo, dr.Item("GYO").ToString)
                .SetCellValue(i, sprEdiListDef.DTL_UPD_DATE.ColNo, dr.Item("DTL_UPD_DATE").ToString)
                .SetCellValue(i, sprEdiListDef.DTL_UPD_TIME.ColNo, dr.Item("DTL_UPD_TIME").ToString)
                .SetCellValue(i, sprEdiListDef.GENPINHYO_CHKFLG.ColNo, dr.Item("GENPINHYO_CHKFLG").ToString)    'ADD 2019/12/18 009991

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#End Region

#End Region

End Class
