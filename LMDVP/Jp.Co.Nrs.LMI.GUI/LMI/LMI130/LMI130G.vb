' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI130  : 日医工詰め合わせ画面
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI130Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI130G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI130F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI130F)

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
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = LMIControlC.FUNCTION_ADD
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = LMIControlC.FUNCTION_NIFUDAPRINT
            .F6ButtonName = LMIControlC.FUNCTION_KONPOPRINT
            .F7ButtonName = LMIControlC.FUNCTION_PRINT
            .F8ButtonName = LMIControlC.FUNCTION_CLEAR
            .F9ButtonName = String.Empty
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = always
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = always
            .F6ButtonEnabled = always
            .F7ButtonEnabled = always
            .F8ButtonEnabled = always
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = lock
            .F11ButtonEnabled = lock
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

            .grpSearch.TabIndex = LMI130C.CtlTabIndex.GRPSEARCH
            .cmbEigyo.TabIndex = LMI130C.CtlTabIndex.EIGYO
            .txtOutkaNo.TabIndex = LMI130C.CtlTabIndex.OUTKANO
            .grpPrint.TabIndex = LMI130C.CtlTabIndex.GRPPRINT
            .cmbPrint.TabIndex = LMI130C.CtlTabIndex.CMBPRINT
            .numPrtCnt.TabIndex = LMI130C.CtlTabIndex.BUSU
            .btnPrint.TabIndex = LMI130C.CtlTabIndex.BTNPRINT
            .btnRowDel.TabIndex = LMI130C.CtlTabIndex.BTNROWDEL
            .sprDetails.TabIndex = LMI130C.CtlTabIndex.SPRDETAILS

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        With Me._Frm


            '自営業所の設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd

            '部数の設定
            .numPrtCnt.Value = 1

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetNumberControl()

        With Me._frm

            Dim d2 As Decimal = Convert.ToDecimal(LMI130C.SORT_MAX)
            Dim sharp2 As String = "#0"

            .numPrtCnt.SetInputFields(sharp2, , 2, 1, , 0, 0, , d2, 0)

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .txtOutkaNo.Focus()

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
    ''' 出荷管理番号のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearOutkaNo()

        With Me._Frm

            .txtOutkaNo.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドのクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearSpread()

        With Me._Frm

            'SPREAD(表示行)初期化
            .sprDetails.CrearSpread()

        End With

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailsDef

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared GOODSNM1 As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.GOODSNM1, "商品名", 200, True)
        Public Shared GOODSNM2 As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.GOODSNM2, "規格名", 200, True)
        Public Shared LTDATE As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.LTDATE, "有効期限", 80, True)
        Public Shared LOTNO As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.LOTNO, "ロット№", 120, True)
        Public Shared TSUMENB As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.TSUMENB, "詰め合わせ個数", 100, True)
        Public Shared ALCTDNB As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.ALCTDNB, "出荷個数", 100, True)
        Public Shared DESTNM As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.DESTNM, "届先名", 200, True)
        Public Shared CUSTNM As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.CUSTNM, "荷主名", 200, True)
        Public Shared CUSTCD As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.CUSTCD, "荷主コード", 110, True)
        Public Shared OUTKANO As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.OUTKANO, "出荷管理番号", 130, True)
        Public Shared GOODSCDNRS As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.GOODSCDNRS, "商品キー", 0, False)
        Public Shared GOODSCDCUST As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.GOODSCDCUST, "商品コード", 0, False)
        Public Shared DESTCD As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.DESTCD, "届先コード", 0, False)
        Public Shared GOODSSYUBETU As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.GOODSSYUBETU, "商品種別", 0, False)
        Public Shared WHCD As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.WHCD, "倉庫コード", 0, False)
        Public Shared COMPDATE As SpreadColProperty = New SpreadColProperty(LMI130C.SprColumnIndex.COMPDATE, "作業日", 0, False)

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
            .sprDetails.Sheets(0).ColumnCount = LMI130C.SprColumnIndex.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetails.SetColProperty(New sprDetailsDef)

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

        Dim ltDate As String = String.Empty

        'セルに設定するスタイルの取得
        Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
        Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ",")
        Dim sGoodsNm As StyleInfo = Me.StyleInfoTextMix(spr, 60, False)
        Dim sDate As StyleInfo = Me.StyleInfoYM(spr, False)
        Dim sLotNo As StyleInfo = Me.StyleInfoTextMixImeOff(spr, 40, False)
        Dim sNb As StyleInfo = Me.StyleInfoNum10(spr, False)

        With spr

            .SuspendLayout()

            If lngcnt = -1 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            '値設定
            For i As Integer = 0 To lngcnt

                '有効期限のスラッシュ編集
                ltDate = String.Empty
                If String.IsNullOrEmpty(dt.Rows(i).Item("LT_DATE").ToString()) = False Then
                    ltDate = DateFormatUtility.EditSlash(dt.Rows(i).Item("LT_DATE").ToString())
                End If

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                .SetCellStyle(rowCnt, sprDetailsDef.DEF.ColNo, sDEF)
                .SetCellStyle(rowCnt, sprDetailsDef.GOODSNM1.ColNo, sGoodsNm)  '商品名
                .SetCellStyle(rowCnt, sprDetailsDef.GOODSNM2.ColNo, sGoodsNm)  '規格名
                .SetCellStyle(rowCnt, sprDetailsDef.LTDATE.ColNo, sDate)       '有効期限
                .SetCellStyle(rowCnt, sprDetailsDef.LOTNO.ColNo, sLotNo)       'ロット№
                .SetCellStyle(rowCnt, sprDetailsDef.TSUMENB.ColNo, sNb)        '詰め合わせ個数
                .SetCellStyle(rowCnt, sprDetailsDef.ALCTDNB.ColNo, sNum)       '引当個数
                .SetCellStyle(rowCnt, sprDetailsDef.DESTNM.ColNo, sLabel)      '届先名
                .SetCellStyle(rowCnt, sprDetailsDef.CUSTNM.ColNo, sLabel)      '荷主名
                .SetCellStyle(rowCnt, sprDetailsDef.CUSTCD.ColNo, sLabel)      '荷主コード
                .SetCellStyle(rowCnt, sprDetailsDef.OUTKANO.ColNo, sLabel)     '出荷管理番号
                .SetCellStyle(rowCnt, sprDetailsDef.GOODSCDNRS.ColNo, sLabel)  '商品キー
                .SetCellStyle(rowCnt, sprDetailsDef.GOODSCDCUST.ColNo, sLabel) '商品コード
                .SetCellStyle(rowCnt, sprDetailsDef.DESTCD.ColNo, sLabel)      '届先コード
                .SetCellStyle(rowCnt, sprDetailsDef.GOODSSYUBETU.ColNo, sLabel)  '商品種別コード
                .SetCellStyle(rowCnt, sprDetailsDef.WHCD.ColNo, sLabel)  '倉庫コード
                .SetCellStyle(rowCnt, sprDetailsDef.COMPDATE.ColNo, sLabel)  '作業日

                'セルに値を設定
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.DEF.ColNo, False.ToString())
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.GOODSNM1.ColNo, dt.Rows(i).Item("GOODS_NM_1").ToString()) '商品名
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.GOODSNM2.ColNo, dt.Rows(i).Item("GOODS_NM_2").ToString()) '規格名
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.LTDATE.ColNo, ltDate)                                     '有効期限
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.LOTNO.ColNo, dt.Rows(i).Item("LOT_NO").ToString())        'ロット№
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.TSUMENB.ColNo, dt.Rows(i).Item("ALCTD_NB").ToString())    '詰め合わせ個数
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.ALCTDNB.ColNo, dt.Rows(i).Item("ALCTD_NB").ToString())    '引当個数
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.DESTNM.ColNo, dt.Rows(i).Item("DEST_NM").ToString())      '届先名
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.CUSTNM.ColNo, dt.Rows(i).Item("CUST_NM_L").ToString())    '荷主名
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.CUSTCD.ColNo, String.Concat(dt.Rows(i).Item("CUST_CD_L").ToString(), "-", dt.Rows(i).Item("CUST_CD_M").ToString(), "-", dt.Rows(i).Item("CUST_CD_S").ToString(), "-", dt.Rows(i).Item("CUST_CD_SS").ToString())) '荷主コード
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.OUTKANO.ColNo, String.Concat(dt.Rows(i).Item("OUTKA_NO_L").ToString(), "-", dt.Rows(i).Item("OUTKA_NO_M").ToString(), "-", dt.Rows(i).Item("OUTKA_NO_S").ToString())) '出荷管理番号
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.GOODSCDNRS.ColNo, dt.Rows(i).Item("GOODS_CD_NRS").ToString())   '商品キー
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.GOODSCDCUST.ColNo, dt.Rows(i).Item("GOODS_CD_CUST").ToString()) '商品コード
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.DESTCD.ColNo, dt.Rows(i).Item("DEST_CD").ToString())      '届先名
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.GOODSSYUBETU.ColNo, dt.Rows(i).Item("GOODS_SYUBETU").ToString())      '商品種別
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.WHCD.ColNo, dt.Rows(i).Item("WH_CD").ToString())      '倉庫コード
                .SetCellValue(rowCnt, LMI130G.sprDetailsDef.COMPDATE.ColNo, dt.Rows(i).Item("OUTKA_PLAN_DATE").ToString())      '作業日

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの行削除
    ''' </summary>
    ''' <param name="chkList"></param>
    ''' <remarks></remarks>
    Friend Sub DelSpread(ByVal chkList As ArrayList)

        Dim spr As LMSpread = _Frm.sprDetails
        Dim intRow As Integer = 0
        Dim max As Integer = chkList.Count - 1

        For i As Integer = max To 0 Step -1

            intRow = Convert.ToInt32(chkList(i))
            spr.Sheets(0).RemoveRows(intRow, 1)

        Next
    End Sub

#End Region 'Spread

#Region "ユーティリティ"

#Region "プロパティ"

    ''' <summary>
    ''' セルのプロパティを設定(CheckBox)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoChk(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetCheckBoxCell(spr, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Date)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoDate(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        '日付スタイルを設定
        StyleInfoDate = LMSpreadUtility.GetDateTimeCell(spr, lock)

        '配置左に設定 
        StyleInfoDate.HorizontalAlignment = CellHorizontalAlignment.Left

        Return StyleInfoDate

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Date)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoYM(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        '日付スタイルを設定
        StyleInfoYM = LMSpreadUtility.GetDateTimeFormatCell(spr, lock, "yyyy/MM")

        '配置左に設定 
        StyleInfoYM.HorizontalAlignment = CellHorizontalAlignment.Left

        Return StyleInfoYM

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(MIX)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextMix(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, length, lock)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(TextHankaku)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextHankaku(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, length, lock)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(TextHankaku)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextMixImeOff(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, length, lock)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数10桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum10(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, lock, 0, , ",")

    End Function

#End Region

#End Region

#End Region

End Class
