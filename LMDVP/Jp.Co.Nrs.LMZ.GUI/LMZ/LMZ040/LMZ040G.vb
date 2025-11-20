' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ040 : 単価マスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMZ040Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ040G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ040F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConG As LMZControlG


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ040F)

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

#Region "Form"

    ''' <summary>
    ''' 画面ヘッダー部値設定
    ''' </summary>
    ''' <param name="drow">荷主キャッシュから取得したデータロウ配列</param>
    ''' <remarks></remarks>
    Friend Sub CustHeaderDataSet(ByVal drow As DataRow)

        With _Frm

            .lblCustCdL.TextValue = drow("CUST_CD_L").ToString()
            .lblCustNmL.TextValue = drow("CUST_NM_L").ToString()
            .lblCustCdM.TextValue = drow("CUST_CD_M").ToString()
            .lblCustNmM.TextValue = drow("CUST_NM_M").ToString()

        End With

    End Sub

    ''' <summary>
    ''' 画面ヘッダー部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbNrsBrCd, lock)
            Me.SetLockControl(.lblCustCdL, lock)
            Me.SetLockControl(.lblCustNmL, lock)
            Me.SetLockControl(.lblCustCdM, lock)
            Me.SetLockControl(.lblCustNmM, lock)
            .sprTanka.Enabled = Not lock

        End With

    End Sub

    ''' <summary>
    ''' ファンクションキーのロック処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub FunctionKeyLock()

        With Me._Frm.FunctionKey
            .F9ButtonEnabled = False
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
            .F12ButtonEnabled = True

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared UP_GP_CD_1 As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.UP_GP_CD_1, "マスタ" & vbCrLf & "コード", 60, True)    '01.単価マスタ・コード
        Public Shared KIWARI_KB_NM As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.KIWARI_KB_NM, "期割区分", 90, True)                  '02.期割区分名
        Public Shared KIWARI_KB As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.KIWARI_KB, "期割区分", 50, False)                       '02.期割区分
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.REMARK, "摘要", 150, True)                                 '03.摘要(REMARK)
        Public Shared STR_DATE As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.STR_DATE, "開始日", 90, True)                            '04.摘要開始日(YYYYMMDD)
        Public Shared STORAGE_KB1_NM As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.STORAGE_KB1_NM, "保管(常温)", 90, True)            '05.保管料建区分名(温度管理なし) 
        Public Shared STORAGE_KB1 As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.STORAGE_KB1, "保管(常温)区分", 50, False)             '05.保管料建区分(温度管理なし) 
        Public Shared STORAGE_1 As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.STORAGE_1, "金額(常温)", 110, True)                     '06.保管料(温度管理なし)
        Public Shared STORAGE_KB2_NM As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.STORAGE_KB2_NM, "保管(定温)", 90, True)            '08.保管料建区分名(温度管理あり)
        Public Shared STORAGE_KB2 As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.STORAGE_KB2, "保管(定温)区分", 50, False)             '08.保管料建区分(温度管理あり) 
        Public Shared STORAGE_2 As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.STORAGE_2, "金額(定温)", 110, True)                     '09.保管料(温度管理あり)
        Public Shared HANDLING_IN_KB_NM As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.HANDLING_IN_KB_NM, "入庫料建", 90, True)        '11.荷役料建(入庫)区分名
        Public Shared HANDLING_IN_KB As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.HANDLING_IN_KB, "入庫料建", 50, False)             '11.荷役料建(入庫)区分
        Public Shared HANDLING_IN As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.HANDLING_IN, "入庫料", 110, True)                   '12.荷役料(入庫）1
        Public Shared MINI_TEKI_IN_AMO As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.MINI_TEKI_IN_AMO, "入庫保証", 110, True)         '13.最低保証荷役料(入庫)
        Public Shared HANDLING_OUT_KB_NM As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.HANDLING_OUT_KB_NM, "出庫料建", 90, True)      '14.荷役料建(出庫)区分名
        Public Shared HANDLING_OUT_KB As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.HANDLING_OUT_KB, "出庫料建", 50, False)           '14.荷役料建(出庫)区分
        Public Shared HANDLING_OUT As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.HANDLING_OUT, "出庫料", 110, True)                 '15.荷役料(出庫)1
        Public Shared MINI_TEKI_OUT_AMO As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.MINI_TEKI_OUT_AMO, "出庫保証", 110, True)       '16.最低保証荷役料(出庫)
        Public Shared REC_NO As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.REC_NO, "レコード№", 100, True)                           '17.レコード番号
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ040C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(ByVal drow As DataRow)

        With Me._Frm

            'スプレッドの行をクリア
            .sprTanka.CrearSpread()

            '列数設定
            .sprTanka.Sheets(0).ColumnCount = 22

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprTanka.SetColProperty(New sprDetailDef)
            .sprTanka.SetColProperty(New sprDetailDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.ユーザー名で固定)
            .sprTanka.ActiveSheet.FrozenColumnCount = LMZ040G.sprDetailDef.DEF.ColNo + 1

            '列設定
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.UP_GP_CD_1.ColNo, LMSpreadUtility.GetTextCell(.sprTanka, InputControl.HAN_NUM_ALPHA, 3, False))      '01.単価マスタ・コード
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.KIWARI_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprTanka, "K003", False))                          '02.期割区分
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.KIWARI_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprTanka, CellHorizontalAlignment.Left))
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprTanka, InputControl.ALL_MIX, 100, False))              '03.摘要
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.STR_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprTanka, True))                                    '04.摘要開始日(YYYYMMDD)
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.STORAGE_KB1_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprTanka, "T005", False))                     '05.保管料建区分(温度管理なし)
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.STORAGE_KB1.ColNo, LMSpreadUtility.GetLabelCell(.sprTanka, CellHorizontalAlignment.Left))
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.STORAGE_1.ColNo, LMSpreadUtility.GetNumberCell(.sprTanka, 0, 999999999.999, True))                   '06.保管料（温度管理なし）
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.STORAGE_KB2_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprTanka, "T005", False))                     '08.保管料建区分(温度管理あり)
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.STORAGE_KB2.ColNo, LMSpreadUtility.GetLabelCell(.sprTanka, CellHorizontalAlignment.Left))
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.STORAGE_2.ColNo, LMSpreadUtility.GetNumberCell(.sprTanka, 0, 999999999.999, True))                   '09.保管料（温度管理あり） 
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.HANDLING_IN_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprTanka, "T005", False))                  '11.荷役料建（入庫）区分
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.HANDLING_IN_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprTanka, CellHorizontalAlignment.Left))
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.HANDLING_IN.ColNo, LMSpreadUtility.GetNumberCell(.sprTanka, 0, 999999999.999, True))                '12.荷役料（入庫）1
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.MINI_TEKI_IN_AMO.ColNo, LMSpreadUtility.GetNumberCell(.sprTanka, 0, 999999999.999, True))            '13.最低保証荷役料（入庫）
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.HANDLING_OUT_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprTanka, "T005", False))                 '14.荷役料建（出庫）区分
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.HANDLING_OUT_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprTanka, CellHorizontalAlignment.Left))
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.HANDLING_OUT.ColNo, LMSpreadUtility.GetNumberCell(.sprTanka, 0, 999999999.999, True))               '15.荷役料（出庫）1
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.MINI_TEKI_OUT_AMO.ColNo, LMSpreadUtility.GetNumberCell(.sprTanka, 0, 999999999.999, True))           '16.最低保証荷役料（出庫）
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.REC_NO.ColNo, LMSpreadUtility.GetTextCell(.sprTanka, InputControl.HAN_NUMBER, 10, False))            '17.レコード番号
            .sprTanka.SetCellStyle(0, LMZ040G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprTanka))

        End With

        Call Me.SetInitValue(drow)

    End Sub

    ''' <summary>
    ''' 画面初期値設定(スプレッド)
    ''' </summary>
    ''' <param name="drow"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal drow As DataRow)

        With Me._Frm.sprTanka

            .SetCellValue(0, LMZ040G.sprDetailDef.UP_GP_CD_1.ColNo, drow("UP_GP_CD_1").ToString())
            .SetCellValue(0, LMZ040G.sprDetailDef.KIWARI_KB_NM.ColNo, drow("KIWARI_KB").ToString())
            .SetCellValue(0, LMZ040G.sprDetailDef.KIWARI_KB.ColNo, drow("KIWARI_KB").ToString())
            .SetCellValue(0, LMZ040G.sprDetailDef.REMARK.ColNo, drow("REMARK").ToString())
            .SetCellValue(0, LMZ040G.sprDetailDef.STR_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMZ040G.sprDetailDef.STORAGE_KB1_NM.ColNo, drow("STORAGE_KB1").ToString())
            .SetCellValue(0, LMZ040G.sprDetailDef.STORAGE_KB1.ColNo, drow("STORAGE_KB1").ToString())
            .SetCellValue(0, LMZ040G.sprDetailDef.STORAGE_1.ColNo, String.Empty)
            .SetCellValue(0, LMZ040G.sprDetailDef.STORAGE_KB2_NM.ColNo, drow("STORAGE_KB2").ToString())
            .SetCellValue(0, LMZ040G.sprDetailDef.STORAGE_KB2.ColNo, drow("STORAGE_KB2").ToString())
            .SetCellValue(0, LMZ040G.sprDetailDef.STORAGE_2.ColNo, String.Empty)
            .SetCellValue(0, LMZ040G.sprDetailDef.HANDLING_IN_KB_NM.ColNo, drow("HANDLING_IN_KB").ToString())
            .SetCellValue(0, LMZ040G.sprDetailDef.HANDLING_IN_KB.ColNo, drow("HANDLING_IN_KB").ToString())
            .SetCellValue(0, LMZ040G.sprDetailDef.HANDLING_IN.ColNo, String.Empty)
            .SetCellValue(0, LMZ040G.sprDetailDef.MINI_TEKI_IN_AMO.ColNo, String.Empty)
            .SetCellValue(0, LMZ040G.sprDetailDef.HANDLING_OUT_KB_NM.ColNo, drow("HANDLING_OUT_KB").ToString())
            .SetCellValue(0, LMZ040G.sprDetailDef.HANDLING_OUT_KB.ColNo, drow("HANDLING_OUT_KB").ToString())
            .SetCellValue(0, LMZ040G.sprDetailDef.HANDLING_OUT.ColNo, String.Empty)
            .SetCellValue(0, LMZ040G.sprDetailDef.MINI_TEKI_OUT_AMO.ColNo, String.Empty)
            .SetCellValue(0, LMZ040G.sprDetailDef.REC_NO.ColNo, drow("REC_NO").ToString())

        End With

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = drow("NRS_BR_CD").ToString()
            .lblCustCdL.TextValue = drow("CUST_CD_L").ToString()
            .lblCustCdM.TextValue = drow("CUST_CD_M").ToString()

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprTanka
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

            'キーボード操作でチェックボックスＯＮ
            .KeyboardCheckBoxOn = True

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim rLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            Dim dRow As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dRow = dt.Rows(i - 1)

                'セルスタイル設定

                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.UP_GP_CD_1.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.KIWARI_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.KIWARI_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.REMARK.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.STR_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.STORAGE_KB1_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.STORAGE_KB1.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.STORAGE_1.ColNo, rLabel)
                .SetCellStyle(i, sprDetailDef.STORAGE_KB2_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.STORAGE_KB2.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.STORAGE_2.ColNo, rLabel)
                .SetCellStyle(i, sprDetailDef.HANDLING_IN_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.HANDLING_IN_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.HANDLING_IN.ColNo, rLabel)
                .SetCellStyle(i, sprDetailDef.MINI_TEKI_IN_AMO.ColNo, rLabel)
                .SetCellStyle(i, sprDetailDef.HANDLING_OUT_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.HANDLING_OUT_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.HANDLING_OUT.ColNo, rLabel)
                .SetCellStyle(i, sprDetailDef.MINI_TEKI_OUT_AMO.ColNo, rLabel)
                .SetCellStyle(i, sprDetailDef.REC_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)     '行番号

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.UP_GP_CD_1.ColNo, dRow.Item("UP_GP_CD_1").ToString())
                .SetCellValue(i, sprDetailDef.KIWARI_KB_NM.ColNo, dRow.Item("KIWARI_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.KIWARI_KB.ColNo, dRow.Item("KIWARI_KB").ToString())
                .SetCellValue(i, sprDetailDef.REMARK.ColNo, dRow.Item("REMARK").ToString())
                .SetCellValue(i, sprDetailDef.STR_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dRow.Item("STR_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.STORAGE_KB1_NM.ColNo, dRow.Item("STORAGE_KB1_NM").ToString())
                .SetCellValue(i, sprDetailDef.STORAGE_KB1.ColNo, dRow.Item("STORAGE_KB1").ToString())
                .SetCellValue(i, sprDetailDef.STORAGE_1.ColNo, dRow.Item("STORAGE_1").ToString())
                .SetCellValue(i, sprDetailDef.STORAGE_KB2_NM.ColNo, dRow.Item("STORAGE_KB2_NM").ToString())
                .SetCellValue(i, sprDetailDef.STORAGE_KB2.ColNo, dRow.Item("STORAGE_KB2").ToString())
                .SetCellValue(i, sprDetailDef.STORAGE_2.ColNo, dRow.Item("STORAGE_2").ToString())
                .SetCellValue(i, sprDetailDef.HANDLING_IN_KB_NM.ColNo, dRow.Item("HANDLING_IN_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.HANDLING_IN_KB.ColNo, dRow.Item("HANDLING_IN_KB").ToString())
                .SetCellValue(i, sprDetailDef.HANDLING_IN.ColNo, dRow.Item("HANDLING_IN").ToString())
                .SetCellValue(i, sprDetailDef.MINI_TEKI_IN_AMO.ColNo, dRow.Item("MINI_TEKI_IN_AMO").ToString())
                .SetCellValue(i, sprDetailDef.HANDLING_OUT_KB_NM.ColNo, dRow.Item("HANDLING_OUT_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.HANDLING_OUT_KB.ColNo, dRow.Item("HANDLING_OUT_KB").ToString())
                .SetCellValue(i, sprDetailDef.HANDLING_OUT.ColNo, dRow.Item("HANDLING_OUT").ToString())
                .SetCellValue(i, sprDetailDef.MINI_TEKI_OUT_AMO.ColNo, dRow.Item("MINI_TEKI_OUT_AMO").ToString())
                .SetCellValue(i, sprDetailDef.REC_NO.ColNo, dRow.Item("REC_NO").ToString())
                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))

            Next

            .ResumeLayout(True)

        End With


    End Sub

#End Region 'Spread

#Region "部品"

    ''' <summary>
    ''' ロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub SetLockControl(ByVal ctl As Control, Optional ByVal lockFlg As Boolean = False)

        Dim arr As ArrayList = New ArrayList()
        Call Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
        Dim lblArr As ArrayList = New ArrayList()

        'エディット系コントロールのロック
        For Each arrCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In arr

            'テキストボックスの場合、ラベル項目であったら処理対象外とする
            If TypeOf arrCtl Is Win.InputMan.LMImTextBox = True Then

                If DirectCast(arrCtl, Win.InputMan.LMImTextBox).Name.Substring(0, 3).Equals("lbl") = True Then
                    lblArr.Add(arrCtl)
                End If

            End If

            'ロック処理/ロック解除処理を行う
            arrCtl.ReadOnlyStatus = lockFlg
        Next

        'ラベル項目をロック
        For Each lblCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In lblArr
            lblCtl.ReadOnlyStatus = True
        Next

        'ボタンのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMButton)(arr, ctl)
        For Each arrCtl As Win.LMButton In arr
            'ロック処理/ロック解除処理を行う
            Call Me.LockButton(arrCtl, lockFlg)
        Next

        'チェックボックスのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMCheckBox)(arr, ctl)
        For Each arrCtl As Win.LMCheckBox In arr

            'ロック処理/ロック解除処理を行う
            Call Me.LockCheckBox(arrCtl, lockFlg)

        Next

    End Sub


    ''' <summary>
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Private Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

        If TypeOf ownControl Is T _
            OrElse ownControl.GetType.IsSubclassOf(GetType(T)) Then

            '指定されたクラスかその継承クラス
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(チェックボックス)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockCheckBox(ByVal ctl As Win.LMCheckBox, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.EnableStatus = enabledFlg

    End Sub


    ''' <summary>
    ''' 引数のコントロールをロックする(ボタン)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockButton(ByVal ctl As Win.LMButton, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

#End Region

#End Region

End Class
