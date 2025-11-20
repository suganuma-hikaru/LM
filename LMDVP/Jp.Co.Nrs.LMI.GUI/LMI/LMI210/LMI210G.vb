' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI210  : シリンダ在庫画面
'  作  成  者       :  [KIM]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.DSL
Imports System.Text.RegularExpressions
Imports Jp.Co.Nrs.Win.Utility

''' <summary>
''' LMI210Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI210G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI210F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

#End Region 'Field

#Region "Const"

    ''' <summary>
    ''' モード：初期
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const MODE_SHOKI As String = "00"

    ''' <summary>
    ''' モード：検索
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const MODE_SEARCH As String = "01"

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI210F, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMIConG = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <param name="modeFlg">00:初期設定、01:検索</param>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal modeFlg As String)

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
            .F9ButtonName = LMIControlC.FUNCTION_KENSAKU
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

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
            .F10ButtonEnabled = lock
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region 'FunctionKey

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .grpSearch.TabIndex = LMI210C.CtlTabIndex.GRPSEARCH
            .cmbEigyo.TabIndex = LMI210C.CtlTabIndex.EIGYO
            .imdInkaDateFrom.TabIndex = LMI210C.CtlTabIndex.IDODATEFROM
            .imdInkaDateTo.TabIndex = LMI210C.CtlTabIndex.IDODATETO
            '2013.08.15 要望番号2095 START
            .cmbCoolantGoodsKb.TabIndex = LMI210C.CtlTabIndex.COOLANTGOODSKB
            '2013.08.15 要望番号2095 END
            .cmbCylinderType.TabIndex = LMI210C.CtlTabIndex.CYLTYPE
            .imdBaseDate.TabIndex = LMI210C.CtlTabIndex.BASEDATE
            .grpShori.TabIndex = LMI210C.CtlTabIndex.GRPSHORI
            .optInka.TabIndex = LMI210C.CtlTabIndex.OPTINKA
            .optOutka.TabIndex = LMI210C.CtlTabIndex.OPTOUTKA
            .sprDetails.TabIndex = LMI210C.CtlTabIndex.SPRDETAILS

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定、初期値設定
    ''' </summary>
    ''' <param name="sysDate"></param>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        With Me._Frm

            '自営業所の設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd

            'ラジオボタンの設定
            .optInka.Checked = True

            '移動日の初期値設定
            .imdInkaDateFrom.TextValue = (New Date(Now.Year, Now.Month, 1).AddMonths(-1)).ToString("yyyyMMdd") '前月1日  (入荷)
            .imdInkaDateTo.TextValue = (New Date(Now.Year, Now.Month, 1).AddDays(-1)).ToString("yyyyMMdd")     '前月末日 (入荷)
            .imdOutkaDateFrom.TextValue = (New Date(Now.Year, Now.Month, 1).AddMonths(-1)).ToString("yyyyMMdd") '前月1日 (出荷)
            .imdOutkaDateTo.TextValue = (New Date(Now.Year, Now.Month, 1).AddDays(-1)).ToString("yyyyMMdd")     '前月末日(出荷)
            Dim BDateRow() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'C014'") 'キャッシュ使用
            .imdBaseDate.TextValue = BDateRow(0).Item("KBN_NM1").ToString '"20090901"

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            If .optOutka.Checked = True Then
                '出荷の場合
                .imdBaseDate.ReadOnly = True        '基準日     ロック
                .imdInkaDateFrom.ReadOnly = True    '入荷日FROM ロック
                .imdInkaDateTo.ReadOnly = True      '入荷日TO   ロック
                .imdOutkaDateFrom.ReadOnly = False  '出荷日FROM 開放
                .imdOutkaDateTo.ReadOnly = False     '出荷日TO  開放

            ElseIf .optInka.Checked = True Then
                '返却の場合
                .imdBaseDate.ReadOnly = False       '基準日     開放
                .imdInkaDateFrom.ReadOnly = False   '入荷日FROM 開放
                .imdInkaDateTo.ReadOnly = False     '入荷日TO   開放
                .imdOutkaDateFrom.ReadOnly = True   '出荷日FROM ロック
                .imdOutkaDateTo.ReadOnly = True      '出荷日TO  ロック

            End If

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .imdInkaDateFrom.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetails.CrearSpread()

        End With

    End Sub

    '''' <summary>
    '''' 数値コントロールの書式設定
    '''' </summary>
    '''' <remarks></remarks>
    'Friend Sub SetNumberControl()

    '    With Me._Frm

    '        Dim d4 As Decimal = Convert.ToDecimal(LMI210C.NB_MAX_4)
    '        Dim sharp4 As String = "#,##0"

    '        .numKeikaDate.SetInputFields(sharp4, , 4, 1, , 0, 0, , d4, 0)

    '    End With

    'End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(出荷検索の場合)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailsOutka

        'スプレッド(タイトル列)の設定
        Public Shared SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.SERIAL_NO, "シリンダ番号", 100, True)
        Public Shared YOUKINO As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.YOUKINO, "変換後", 100, True)
        Public Shared CYLINDER_TYPE As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.CYLINDER_TYPE, "タイプ", 90, True)
        Public Shared NEXT_TEST_DATE As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.NEXT_TEST_DATE, "定期検査日", 0, False)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.WH_CD, "出荷倉庫", 80, True)
        Public Shared WH_NM As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.WH_NM, "出荷倉庫名称", 120, True)
        Public Shared INOUT_DATE As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.INOUT_DATE, "出荷日", 80, True)
        Public Shared DUMY1 As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.DUMY1, "出荷元CD", 0, False)
        Public Shared DUMY2 As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.DUMY2, "出荷元名称", 0, False)
        Public Shared DUMY3 As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.DUMY3, "出荷日", 0, False)
        Public Shared TOFROM_CD As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.TOFROM_CD, "届先CD", 80, True)
        Public Shared TOFROM_NM As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.TOFROM_NM, "届先名称", 120, True)
        Public Shared SHIP_CD_L As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.SHIP_CD_L, "売上先CD", 80, True)
        Public Shared SHIP_NM_L As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.SHIP_NM_L, "売上先名称", 120, True)
        Public Shared DUMY4 As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.DUMY4, "滞留日数", 0, False)
        Public Shared DUMY5 As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.DUMY5, "遅延金", 0, False)
        Public Shared DUMY6 As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.DUMY6, "奨励金額", 0, False)
        Public Shared DUMY7 As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.DUMY7, "請求先", 0, False)
        Public Shared BUYER_ORD_NO_DTL As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.BUYER_ORD_NO_DTL, "注文番号", 80, True)
        Public Shared CUST_ORD_NO_DTL As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexOUTKA.CUST_ORD_NO_DTL, "オーダ番号", 80, True)

      End Class

    ''' <summary>
    ''' スプレッド列定義体(返却検索の場合)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailsInka

        'スプレッド(タイトル列)の設定
        Public Shared SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.SERIAL_NO, "シリンダ番号", 100, True)
        Public Shared YOUKINO As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.YOUKINO, "変換後", 100, True)
        Public Shared CYLINDER_TYPE As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.CYLINDER_TYPE, "タイプ", 90, True)
        Public Shared NEXT_TEST_DATE As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.NEXT_TEST_DATE, "定期検査日", 0, False)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.WH_CD, "入荷倉庫", 80, True)
        Public Shared WH_NM As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.WH_NM, "入荷倉庫名称", 120, True)
        Public Shared INOUT_DATE As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.INOUT_DATE, "入荷日", 80, True)
        Public Shared TOFROM_CD As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.TOFROM_CD, "出荷元CD", 80, True)
        Public Shared TOFROM_NM As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.TOFROM_NM, "出荷元名称", 120, True)
        Public Shared INOUT_DATE_OUT As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.INOUT_DATE_OUT, "出荷日", 80, True)
        Public Shared TOFROM_CD_OUT As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.TOFROM_CD_OUT, "届先CD", 80, True)
        Public Shared TOFROM_NM_OUT As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.TOFROM_NM_OUT, "届先名称", 120, True)
        Public Shared SHIP_CD_L As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.SHIP_CD_L, "売上先CD", 80, True)
        Public Shared SHIP_NM_L As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.SHIP_NM_L, "売上先名称", 120, True)
        Public Shared LAYT_DAY As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.LAYT_DAY, "滞留日数", 80, True)
        Public Shared PENALTY As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.PENALTY, "遅延金", 80, True)
        Public Shared BONUS As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.BONUS, "奨励金額", 80, True)
        Public Shared SALES_TO As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.SALES_TO, "請求先", 80, True)
        Public Shared DUMY1 As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.DUMY1, "注文番号", 0, False)
        Public Shared DUMY2 As SpreadColProperty = New SpreadColProperty(LMI210C.SprColumnIndexINKA.DUMY2, "オーダ番号", 0, False)
    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetails.CrearSpread()

            If Me._Frm.optInka.Checked = True Then '返却

                '列数設定
                .sprDetails.Sheets(0).ColumnCount = LMI210C.SprColumnIndexINKA.LAST

                'スプレッドの列設定（列名、列幅、列の表示・非表示）
                .sprDetails.SetColProperty(New sprDetailsInka)

            ElseIf Me._Frm.optOutka.Checked = True Then '出荷

                '列数設定
                .sprDetails.Sheets(0).ColumnCount = LMI210C.SprColumnIndexOUTKA.LAST

                'スプレッドの列設定（列名、列幅、列の表示・非表示）
                .sprDetails.SetColProperty(New sprDetailsOutka)

            End If
            

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定（出荷）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadOutka(ByVal spr As LMSpread, ByVal ds As DataSet)

        Dim max As Integer = ds.Tables(LMI210C.TABLE_NM_OUT).Rows.Count - 1
        Dim henkangoStr As String = String.Empty

        With spr

            'スプレッドの行をクリア
            .CrearSpread()
            .SuspendLayout()

            '列数設定
            .Sheets(0).ColumnCount = LMI210C.SprColumnIndexOUTKA.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New sprDetailsOutka)

            '列設定
            Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sLabelRight As StyleInfo = Me.StyleInfoLabelRight(spr)

            Dim rowCnt As Integer = 0

            For i As Integer = 0 To max

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.SERIAL_NO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.YOUKINO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.CYLINDER_TYPE.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.NEXT_TEST_DATE.ColNo, sLabel) 'ダミーのバミリ位置
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.WH_CD.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.WH_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.INOUT_DATE.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.DUMY1.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.DUMY2.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.DUMY3.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.TOFROM_CD.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.TOFROM_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.SHIP_CD_L.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.SHIP_NM_L.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.DUMY4.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.DUMY5.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.DUMY6.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.DUMY7.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.BUYER_ORD_NO_DTL.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsOutka.CUST_ORD_NO_DTL.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(rowCnt, sprDetailsInka.SERIAL_NO.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString())

                If IsNumeric(Mid(Trim(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()), 1, 1)) Then
                    If String.IsNullOrEmpty(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("YOUKI_NO").ToString()) = True Then
                        henkangoStr = "該当無し"
                    Else
                        .SetCellValue(rowCnt, sprDetailsOutka.YOUKINO.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("YOUKI_NO").ToString())
                        henkangoStr = String.Concat(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("YOUKI_NO").ToString(), _
                                                    ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString().Substring(3, 5))
                    End If
                Else
                    henkangoStr = ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()
                End If

                .SetCellValue(rowCnt, sprDetailsOutka.YOUKINO.ColNo, henkangoStr)
                .SetCellValue(rowCnt, sprDetailsOutka.CYLINDER_TYPE.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString())
                .SetCellValue(rowCnt, sprDetailsOutka.NEXT_TEST_DATE.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("NEXT_TEST_DATE").ToString())
                .SetCellValue(rowCnt, sprDetailsOutka.WH_CD.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("WH_CD").ToString())
                .SetCellValue(rowCnt, sprDetailsOutka.WH_NM.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("WH_NM").ToString())
                .SetCellValue(rowCnt, sprDetailsOutka.INOUT_DATE.ColNo, DateFormatUtility.EditSlash(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString()))
                .SetCellValue(rowCnt, sprDetailsOutka.DUMY1.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("TOFROM_CD_OUT").ToString())
                .SetCellValue(rowCnt, sprDetailsOutka.DUMY2.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM_OUT").ToString())
                .SetCellValue(rowCnt, sprDetailsOutka.DUMY3.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE_OUT").ToString())
                .SetCellValue(rowCnt, sprDetailsOutka.TOFROM_CD.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("TOFROM_CD").ToString())
                .SetCellValue(rowCnt, sprDetailsOutka.TOFROM_NM.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM").ToString())
                .SetCellValue(rowCnt, sprDetailsOutka.SHIP_CD_L.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SHIP_CD_L").ToString())
                .SetCellValue(rowCnt, sprDetailsOutka.SHIP_NM_L.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SHIP_NM_L").ToString())
                .SetCellValue(rowCnt, sprDetailsOutka.DUMY4.ColNo, "")  'PG
                .SetCellValue(rowCnt, sprDetailsOutka.DUMY5.ColNo, "")  'PG
                .SetCellValue(rowCnt, sprDetailsOutka.DUMY6.ColNo, "")  'PG
                .SetCellValue(rowCnt, sprDetailsOutka.DUMY7.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SHIP_NM_L").ToString()) '請求先名大と同じ値が入る
                .SetCellValue(rowCnt, sprDetailsOutka.BUYER_ORD_NO_DTL.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("BUYER_ORD_NO_DTL").ToString())
                .SetCellValue(rowCnt, sprDetailsOutka.CUST_ORD_NO_DTL.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("CUST_ORD_NO_DTL").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定（返却）
    ''' </summary>
    ''' <param name="spr"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadInka(ByVal spr As LMSpread, ByVal ds As DataSet)

        Dim max As Integer = ds.Tables(LMI210C.TABLE_NM_OUT).Rows.Count - 1
        Dim henkangoStr As String = String.Empty
        Dim chienbi As String = _Frm.imdBaseDate.TextValue().ToString '遅延金制度開始日
        Dim layt_date As Long = 0
        Dim penalty As String = "0"
        Dim cntup_date As Long = 0
        Dim bonus As String = "0"
        'キャッシュ用空DataRow
        'Dim CSH_dtrow() As DataRow = Nothing
        'Dim CSH_dtrow_Ex1() As DataRow = Nothing
        'Dim CSH_dtrow_Ex2() As DataRow = Nothing
        '
        With spr

            'スプレッドの行をクリア
            .CrearSpread()
            .SuspendLayout()

            '列数設定
            .Sheets(0).ColumnCount = LMI210C.SprColumnIndexINKA.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New sprDetailsInka)

            '列設定
            Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sLabelRight As StyleInfo = Me.StyleInfoLabelRight(spr)
            Dim numNb As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999999, True, 0, , ",")

            Dim rowCnt As Integer = 0

            For i As Integer = 0 To max

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.SERIAL_NO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.YOUKINO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.CYLINDER_TYPE.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.NEXT_TEST_DATE.ColNo, sLabel) 'ダミーのバミリ位置
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.WH_CD.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.WH_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.INOUT_DATE.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.TOFROM_CD.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.TOFROM_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.INOUT_DATE_OUT.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.TOFROM_CD_OUT.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.TOFROM_NM_OUT.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.SHIP_CD_L.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.SHIP_NM_L.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.LAYT_DAY.ColNo, numNb)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.PENALTY.ColNo, numNb)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.BONUS.ColNo, numNb)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.SALES_TO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.DUMY1.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI210G.sprDetailsInka.DUMY2.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(rowCnt, sprDetailsInka.SERIAL_NO.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString())
                
                If IsNumeric(Mid(Trim(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()), 1, 1)) Then
                    If String.IsNullOrEmpty(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("YOUKI_NO").ToString()) = True Then
                        henkangoStr = "該当無し"
                    Else
                        .SetCellValue(rowCnt, sprDetailsInka.YOUKINO.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("YOUKI_NO").ToString())
                        henkangoStr = String.Concat(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("YOUKI_NO").ToString(), _
                                                    ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString().Substring(3, 5))
                    End If
                Else
                    henkangoStr = ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()
                End If

                .SetCellValue(rowCnt, sprDetailsInka.YOUKINO.ColNo, henkangoStr)
                .SetCellValue(rowCnt, sprDetailsInka.CYLINDER_TYPE.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString())
                .SetCellValue(rowCnt, sprDetailsInka.NEXT_TEST_DATE.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("NEXT_TEST_DATE").ToString())
                .SetCellValue(rowCnt, sprDetailsInka.WH_CD.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("WH_CD").ToString())
                .SetCellValue(rowCnt, sprDetailsInka.WH_NM.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("WH_NM").ToString())
                .SetCellValue(rowCnt, sprDetailsInka.INOUT_DATE.ColNo, DateFormatUtility.EditSlash(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString()))
                .SetCellValue(rowCnt, sprDetailsInka.TOFROM_CD.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("TOFROM_CD").ToString())
                .SetCellValue(rowCnt, sprDetailsInka.TOFROM_NM.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM").ToString())
                .SetCellValue(rowCnt, sprDetailsInka.INOUT_DATE_OUT.ColNo, DateFormatUtility.EditSlash(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE_OUT").ToString()))
                .SetCellValue(rowCnt, sprDetailsInka.TOFROM_CD_OUT.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("TOFROM_CD_OUT").ToString())
                .SetCellValue(rowCnt, sprDetailsInka.TOFROM_NM_OUT.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM_OUT").ToString())
                .SetCellValue(rowCnt, sprDetailsInka.SHIP_CD_L.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SHIP_CD_L_OUT").ToString())
                .SetCellValue(rowCnt, sprDetailsInka.SHIP_NM_L.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SHIP_NM_L_OUT").ToString())

                layt_date = 0 '初期化
                penalty = "0" '初期化
                bonus = "0"

                If String.IsNullOrEmpty(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE_OUT").ToString()) = False Then
                    '------滞留日数開始--------------------------2013.02.12ADD
                    Dim syukabi As String = String.Empty '初期化
                    syukabi = Format(DateAdd("d", -1, Convert.ToDateTime(DateFormatUtility.EditSlash(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE_OUT").ToString()))), "yyyyMMdd")
                    syukabi = IIf(Convert.ToDateTime(DateFormatUtility.EditSlash(syukabi)) < Convert.ToDateTime(DateFormatUtility.EditSlash(chienbi)), chienbi, syukabi).ToString

                    '滞留日数
                    layt_date = DateDiff("d", Convert.ToDateTime(DateFormatUtility.EditSlash(syukabi)), Convert.ToDateTime(DateFormatUtility.EditSlash(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString())))

                    Select Case Trim(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString())
                        Case ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C015").ToString() '10kg,20k
                            layt_date = layt_date - Integer.Parse(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0161").ToString()) '365
                        Case Else
                            layt_date = layt_date - Integer.Parse(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0162").ToString()) '365 '75
                    End Select
                    layt_date = Long.Parse(IIf(layt_date < 0, 0, layt_date).ToString)

                    '------滞留日数終了--------------------------

                    '------遅延金開始--------------------------2013.02.14AD

                    Select Case Trim(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString())
                        Case ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0171").ToString(), ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0172").ToString() '10kg,20kg
                            If layt_date >= 1 And layt_date <= 365 Then
                                penalty = ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0181").ToString() '"2000"
                            ElseIf layt_date > 365 Then
                                penalty = ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0182").ToString() '"4000"
                            Else
                                penalty = "0"
                            End If
                        Case ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0173").ToString() '"100KG"
                            'penalty = ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0183").ToString()
                            penalty = Convert.ToString(layt_date * Convert.ToDecimal(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0183").ToString()))
                        Case ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0174").ToString().ToString '"TON"
                            'penalty = ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0184").ToString()
                            penalty = Convert.ToString(layt_date * Convert.ToDecimal(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0184").ToString()))
                        Case Else
                            penalty = "0"
                    End Select

                    '------遅延金終了--------------------------

                    '------ボーナス開始--------------------------2013.02.14ADD


                    Dim B_syukabi As String = IIf(String.IsNullOrEmpty(Trim(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE_OUT").ToString())), _
                                                   "00000000", _
                                                   Trim(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE_OUT").ToString())).ToString

                    syukabi = Format(DateAdd("d", -1, Convert.ToDateTime(DateFormatUtility.EditSlash(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE_OUT").ToString()))), "yyyyMMdd")
                    cntup_date = DateDiff("d", Convert.ToDateTime(DateFormatUtility.EditSlash(syukabi)), Convert.ToDateTime(DateFormatUtility.EditSlash(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString())))

                    'CSH_dtrow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'C015'") 'キャッシュ使用
                    'CSH_dtrow_Ex1 = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'C019'") 'キャッシュ使用

                    If Convert.ToDateTime(DateFormatUtility.EditSlash(B_syukabi)) >= Convert.ToDateTime(DateFormatUtility.EditSlash(chienbi)) Then

                        Select Case Trim(ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString())
                            Case ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C015").ToString() '10kg,20kg

                                Select Case cntup_date
                                    Case 0 To 60
                                        bonus = ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0191").ToString() '"600"
                                    Case 61 To 90
                                        bonus = ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("Z_C0192").ToString() '"300"
                                    Case Else
                                        bonus = "0"
                                End Select
                            Case Else
                                bonus = "0"
                        End Select

                    End If
                    '------ボーナス終了--------------------------
                End If

                .SetCellValue(rowCnt, sprDetailsInka.LAYT_DAY.ColNo, layt_date.ToString)  'PG
                .SetCellValue(rowCnt, sprDetailsInka.PENALTY.ColNo, penalty)   'PG
                .SetCellValue(rowCnt, sprDetailsInka.BONUS.ColNo, bonus)     'PG
                .SetCellValue(rowCnt, sprDetailsInka.SALES_TO.ColNo, ds.Tables(LMI210C.TABLE_NM_OUT).Rows(i).Item("SHIP_NM_L_OUT").ToString()) '請求先名大と同じ値が入る
                .SetCellValue(rowCnt, sprDetailsInka.DUMY1.ColNo, "")  'PG
                .SetCellValue(rowCnt, sprDetailsInka.DUMY2.ColNo, "")  'PG
            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

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
    ''' セルのプロパティを設定(Label)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabel(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Label)右寄せ
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabelRight(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(TextHankakuNumber)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextNUMBER(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUMBER, length, lock)

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

#End Region

#Region "数字チェック"

    ''' <summary>
    ''' 数字チェック
    ''' </summary>
    ''' <param name="value">判定値</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsNumericChk(ByVal value As String) As Boolean

        Dim valLength As Integer = value.Trim.Length

        If Regex.IsMatch(value, "^[0-9]{1," & valLength & "}$") = False Then
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

#End Region

End Class
