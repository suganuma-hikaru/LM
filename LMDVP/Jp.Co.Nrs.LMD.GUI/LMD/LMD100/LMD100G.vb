' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD100C : 在庫テーブル照会
'  作  成  者       :  矢内
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMD100Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD100G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD100F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD100F)

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
    Friend Sub SetFunctionKey(ByVal mode As String)

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
            .F10ButtonName = "ロット指定"
            .F11ButtonName = "商品選択"
            .F12ButtonName = "キャンセル"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = always
            .F12ButtonEnabled = always

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

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

            'Main
            '.txtSoko_Nm.TabIndex = 1
            '.txtCustNM_L.TabIndex = 2
            '.txtCustNM_M.TabIndex = 3
            '.sprZai.TabIndex = 4

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
    ''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = LMD100C.MODE_DEFAULT)

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

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.GOODS_CD_CUST, "商品コード", 100, True)                   '荷主商品コード
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.GOODS_NM, "商品名", 290, True)                                 '商品名
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.LOT_NO, "ロット№", 90, True)                                    'ロット№
        Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.IRIME, "入目", 120, True)                                         '入目
        Public Shared ALLOC_CAN_NB As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.ALLOC_CAN_NB, "可能個数", 40, True)                        '引当可能個数
        Public Shared NB_UT As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.NB_UT, "個数単位", 70, True)                                      '個数単位
        Public Shared PKG As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.PKG, "入数", 45, True)                                              '包装
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.REMARK, "備考小(社内)", 120, True)                               'REMARK
        Public Shared REMARK_OUT As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.REMARK_OUT, "備考小(社外)", 120, True)                       '入番（小）
        Public Shared TAX_KB As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.TAX_KB, "課税区分", 70, True)                                    '課税区分
        Public Shared HIKIATE_ALERT_NM As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.HIKIATE_ALERT_NM, "引当注意品", 70, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.DEST_NM, "届先名", 120, True)
        Public Shared OUTKA_FROM_ORD_NO_L As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.OUTKA_FROM_ORD_NO_L, "オーダー番号", 140, True)

        'invisible
        Public Shared CD_NRS As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.CD_NRS, "NRS商品コード", 80, False)                            'NRS商品コード
        Public Shared IRIME_UT As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.IRIME_UT, "入目単位", 70, False)                              '入目単位
        Public Shared HIKIATE_ALERT_YN As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.HIKIATE_ALERT_YN, "引当注意品フラグ", 90, False)      '引当注意品フラグ
        Public Shared SMPL As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.SMPL, "小分けフラグ", 90, False)                                  '小分けフラグ
        Public Shared ZAI_REC_NO As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.ZAI_REC_NO, "在庫レコード番号", 110, False)                 '在庫レコード番号
        Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LMD100C.SprColumnIndex.DEST_CD, "届先コード", 80, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprZai.CrearSpread()

            '列数設定
            .sprZai.Sheets(0).ColumnCount = LMD100C.SprColumnIndex.LAST

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprZai.SetColProperty(New sprDetailDef)
            .sprZai.SetColProperty(New sprDetailDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定
            .sprZai.Sheets(0).FrozenColumnCount = sprDetailDef.LOT_NO.ColNo + 1

            '列設定
            .sprZai.SetCellStyle(0, sprDetailDef.GOODS_CD_CUST.ColNo, LMSpreadUtility.GetTextCell(.sprZai, InputControl.ALL_HANKAKU, 20, False))    '荷主商品コード
            .sprZai.SetCellStyle(0, sprDetailDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprZai, InputControl.ALL_MIX_IME_OFF, 60, False))     '商品名 '検証結果_導入時要望 №62対応(2011.09.13)
            .sprZai.SetCellStyle(0, sprDetailDef.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(.sprZai, InputControl.ALL_MIX_IME_OFF, 40, False))       'ロット番号            
            .sprZai.SetCellStyle(0, sprDetailDef.IRIME.ColNo, LMSpreadUtility.GetTextCell(.sprZai, InputControl.HAN_NUMBER, 13, False))             '入目
            .sprZai.SetCellStyle(0, sprDetailDef.ALLOC_CAN_NB.ColNo, LMSpreadUtility.GetNumberCell(.sprZai, 0, 9999999999, True))                   '引当可能個数            
            .sprZai.SetCellStyle(0, sprDetailDef.NB_UT.ColNo, LMSpreadUtility.GetComboCellKbn(.sprZai, "K002", False))                              '個数単位
            .sprZai.SetCellStyle(0, sprDetailDef.PKG.ColNo, LMSpreadUtility.GetTextCell(.sprZai, InputControl.ALL_MIX, 8, True))                    '入数外装
            .sprZai.SetCellStyle(0, sprDetailDef.REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprZai, InputControl.ALL_MIX, 15, False))               'REMARK
            .sprZai.SetCellStyle(0, sprDetailDef.REMARK_OUT.ColNo, LMSpreadUtility.GetTextCell(.sprZai, InputControl.ALL_MIX, 20, False))           '入番（小）
            .sprZai.SetCellStyle(0, sprDetailDef.TAX_KB.ColNo, LMSpreadUtility.GetComboCellKbn(.sprZai, "Z001", False))                             '課税区分
            .sprZai.SetCellStyle(0, sprDetailDef.HIKIATE_ALERT_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprZai, "U009", False))
            .sprZai.SetCellStyle(0, sprDetailDef.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprZai, InputControl.ALL_MIX, 80, False))              '届先コード
            .sprZai.SetCellStyle(0, sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprZai, InputControl.ALL_MIX, 30, False))  'オーダー番号

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMD100F, ByVal ds As DataSet)

        With frm.sprZai

            Dim dr As DataRow = ds.Tables(LMControlC.LMD100C_TABLE_NM_IN).Rows(0)

            .Sheets(0).Cells(0, sprDetailDef.GOODS_CD_CUST.ColNo).Value = dr.Item("GOODS_CD_CUST").ToString()
            .Sheets(0).Cells(0, sprDetailDef.GOODS_NM.ColNo).Value = dr.Item("GOODS_NM").ToString()
            .Sheets(0).Cells(0, sprDetailDef.LOT_NO.ColNo).Value = dr.Item("LOT_NO").ToString()
            .Sheets(0).Cells(0, sprDetailDef.IRIME.ColNo).Value = dr.Item("IRIME").ToString()
            .Sheets(0).Cells(0, sprDetailDef.IRIME_UT.ColNo).Value = dr.Item("IRIME_UT").ToString()
            .Sheets(0).Cells(0, sprDetailDef.ALLOC_CAN_NB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.NB_UT.ColNo).Value = dr.Item("NB_UT").ToString()
            .Sheets(0).Cells(0, sprDetailDef.PKG.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.REMARK.ColNo).Value = dr.Item("REMARK").ToString()
            .Sheets(0).Cells(0, sprDetailDef.REMARK_OUT.ColNo).Value = dr.Item("REMARK_OUT").ToString()
            .Sheets(0).Cells(0, sprDetailDef.TAX_KB.ColNo).Value = dr.Item("TAX_KB").ToString()
            .Sheets(0).Cells(0, sprDetailDef.HIKIATE_ALERT_NM.ColNo).Value = dr.Item("HIKIATE_ALERT_NM").ToString()
            .Sheets(0).Cells(0, sprDetailDef.DEST_NM.ColNo).Value = dr.Item("DEST_NM").ToString()
            .Sheets(0).Cells(0, sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo).Value = dr.Item("OUTKA_FROM_ORD_NO_L").ToString()

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetSpread(ByVal ds As DataSet, ByVal prmDs As DataSet) As Integer

        Dim spr As LMSpreadSearch = Me._Frm.sprZai
        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()
            .Sheets(0).Rows.Count = 1
            'データ挿入
            '行数設定
            Dim tbl As DataTable = ds.Tables(LMControlC.LMD100C_TABLE_NM_OUT)
            Dim lngcnt As Integer = tbl.Rows.Count - 1
            If lngcnt = -1 Then
                .ResumeLayout(True)
                Return 0
            End If
#If True Then  'ADD 2020/06/23 012642   引き当て時に古いロット（入庫日）がわからない
            '入庫日フラグ取得
            Dim inko_dateFlg As String = ds.Tables("LMD100OUT").Rows(0).Item("INKO_DATE_FLG").ToString()
#End If

            'キーボード操作でチェックボックスON
            .KeyboardCheckBoxOn = True

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim cLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Center)
            Dim rLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            Dim dRow As DataRow
            Dim dRowCnt As Integer = 0
            Dim sRowCnt As Integer = 0
            Dim zaiDr() As DataRow = Nothing

            Dim AllocCanNb As Decimal = 0
            Dim addFlg As Boolean = True

            For i As Integer = 0 To lngcnt
                dRow = tbl.Rows(i)

                zaiDr = prmDs.Tables(LMControlC.LMD100C_TABLE_NM_ZAI).Select(String.Concat("ZAI_REC_NO = '", dRow.Item("ZAI_REC_NO").ToString(), "'"))
                If 0 < zaiDr.Length Then
                    '上書きする引当可能個数が0の時は、列クリア
                    If ("0").Equals(zaiDr(0).Item("ALLOC_CAN_NB").ToString) = True Then
                        '列クリア
                        tbl.Rows.Remove(dRow)
                        lngcnt = lngcnt - 1
                        i = i - 1

                    End If
                End If
                If lngcnt <= i Then
                    Exit For
                End If
            Next

            '.Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt - 1)

            '値設定
            For i As Integer = 0 To lngcnt

                addFlg = True
                dRow = tbl.Rows(i)

                '残数計算
                AllocCanNb = Convert.ToDecimal(dRow.Item("ALLOC_CAN_NB").ToString())

                zaiDr = prmDs.Tables(LMControlC.LMD100C_TABLE_NM_ZAI).Select(String.Concat("ZAI_REC_NO = '", dRow.Item("ZAI_REC_NO").ToString(), "'"))
                If 0 < zaiDr.Length Then
                    '上書きする引当可能個数を設定
                    AllocCanNb = Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB").ToString())
                End If

                For j As Integer = 1 To sRowCnt

                    If String.Concat(dRow.Item("SMPL").ToString(), " ", dRow.Item("GOODS_CD_CUST").ToString()).Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.GOODS_CD_CUST).Value.ToString()) = True AndAlso
                        dRow.Item("NM_1").ToString().Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.GOODS_NM).Value.ToString()) = True AndAlso
                        dRow.Item("LOT_NO").ToString().Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.LOT_NO).Value.ToString()) = True AndAlso
                         String.Concat(Convert.ToDecimal(Me.FormatNumValue(dRow.Item("IRIME").ToString())).ToString("#,##0.000"), dRow.Item("IRIME_UT_NM").ToString()).Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.IRIME).Value.ToString()) = True AndAlso
                        dRow.Item("IRIME_UT_NM").ToString().Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.IRIME_UT).Value.ToString()) = True AndAlso
                        dRow.Item("NB_UT_NM").ToString().Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.NB_UT).Value.ToString()) = True AndAlso
                        String.Concat(Convert.ToDecimal(Me.FormatNumValue(dRow.Item("PKG_NB").ToString())).ToString("#,##0"), dRow.Item("PKG_UT_NM").ToString()).Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.PKG).Value.ToString()) = True AndAlso
                        dRow.Item("REMARK").ToString().Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.REMARK).Value.ToString()) = True AndAlso
                        dRow.Item("REMARK_OUT").ToString().Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.REMARK_OUT).Value.ToString()) = True AndAlso
                        dRow.Item("TAX_KB_NM").ToString().Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.TAX_KB).Value.ToString()) = True AndAlso
                        dRow.Item("HIKIATE_ALERT_NM").ToString().Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.HIKIATE_ALERT_NM).Value.ToString()) = True AndAlso
                        dRow.Item("DEST_NM").ToString().Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.DEST_NM).Value.ToString()) = True AndAlso
                        dRow.Item("OUTKA_FROM_ORD_NO_L").ToString().Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.OUTKA_FROM_ORD_NO_L).Value.ToString()) = True AndAlso
                        dRow.Item("GOODS_CD_NRS").ToString().Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.CD_NRS).Value.ToString()) = True AndAlso
                        dRow.Item("DEST_CD").ToString().Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.DEST_CD).Value.ToString()) = True AndAlso
                        dRow.Item("HIKIATE_ALERT_YN").ToString().Equals(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.HIKIATE_ALERT_YN).Value.ToString()) = True Then

                        'グループバイデータがスプレッドに存在する場合、残数を足す
                        AllocCanNb = AllocCanNb + Convert.ToDecimal(.Sheets(0).Cells(j, LMD100C.SprColumnIndex.ALLOC_CAN_NB).Value.ToString())
                        .SetCellValue(j, sprDetailDef.ALLOC_CAN_NB.ColNo, Convert.ToDecimal(Me.FormatNumValue(AllocCanNb.ToString)).ToString("#,##0"))

                        '.Sheets(0).RemoveRows(.Sheets(0).Rows.Count - 1, 1)
                        'sRowCnt = sRowCnt - 1

                        addFlg = False
                        Exit For
                    End If

                Next

                'グループバイデータがスプレッドに存在しない場合、行出力
                If addFlg = True Then

                    sRowCnt = sRowCnt + 1

                    .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)


                    'セルスタイル設定
                    .SetCellStyle(sRowCnt, sprDetailDef.DEF.ColNo, sDEF)
                    .SetCellStyle(sRowCnt, sprDetailDef.GOODS_CD_CUST.ColNo, sLabel)                      '荷主商品コード
                    .SetCellStyle(sRowCnt, sprDetailDef.GOODS_NM.ColNo, sLabel)                           '商品名
                    .SetCellStyle(sRowCnt, sprDetailDef.LOT_NO.ColNo, sLabel)                             'ロット番号
                    .SetCellStyle(sRowCnt, sprDetailDef.IRIME.ColNo, rLabel)                              '入目
                    .SetCellStyle(sRowCnt, sprDetailDef.ALLOC_CAN_NB.ColNo, rLabel)                       '引当可能個数
                    .SetCellStyle(sRowCnt, sprDetailDef.NB_UT.ColNo, sLabel)                              '個数単位
                    .SetCellStyle(sRowCnt, sprDetailDef.PKG.ColNo, rLabel)                                '入数外装
                    .SetCellStyle(sRowCnt, sprDetailDef.REMARK.ColNo, sLabel)                             'remark
                    .SetCellStyle(sRowCnt, sprDetailDef.REMARK_OUT.ColNo, sLabel)                         '入番（小）
                    .SetCellStyle(sRowCnt, sprDetailDef.TAX_KB.ColNo, sLabel)                             '課税区分
                    .SetCellStyle(sRowCnt, sprDetailDef.HIKIATE_ALERT_NM.ColNo, sLabel)
                    .SetCellStyle(sRowCnt, sprDetailDef.DEST_NM.ColNo, sLabel)
                    .SetCellStyle(sRowCnt, sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo, sLabel)
                    .SetCellStyle(sRowCnt, sprDetailDef.CD_NRS.ColNo, sLabel)                             'NRS商品コード
                    .SetCellStyle(sRowCnt, sprDetailDef.IRIME_UT.ColNo, sLabel)                           '入目単位
                    .SetCellStyle(sRowCnt, sprDetailDef.HIKIATE_ALERT_YN.ColNo, sLabel)                   '引当注意品フラグ
                    .SetCellStyle(sRowCnt, sprDetailDef.SMPL.ColNo, sLabel)                               '小分けフラグ
                    .SetCellStyle(sRowCnt, sprDetailDef.ZAI_REC_NO.ColNo, sLabel)                         '在庫レコード番号
                    .SetCellStyle(sRowCnt, sprDetailDef.DEST_CD.ColNo, sLabel)

                    'セルに値を設定
                    .SetCellValue(sRowCnt, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                    .SetCellValue(sRowCnt, sprDetailDef.GOODS_CD_CUST.ColNo, String.Concat(dRow.Item("SMPL").ToString(), " ", dRow.Item("GOODS_CD_CUST").ToString()))
                    .SetCellValue(sRowCnt, sprDetailDef.GOODS_NM.ColNo, dRow.Item("NM_1").ToString())
                    .SetCellValue(sRowCnt, sprDetailDef.LOT_NO.ColNo, dRow.Item("LOT_NO").ToString())
                    .SetCellValue(sRowCnt, sprDetailDef.IRIME.ColNo, String.Concat(Convert.ToDecimal(Me.FormatNumValue(dRow.Item("IRIME").ToString())).ToString("#,##0.000"), dRow.Item("IRIME_UT_NM").ToString()))
                    .SetCellValue(sRowCnt, sprDetailDef.NB_UT.ColNo, dRow.Item("NB_UT_NM").ToString())
                    .SetCellValue(sRowCnt, sprDetailDef.PKG.ColNo, String.Concat(Convert.ToDecimal(Me.FormatNumValue(dRow.Item("PKG_NB").ToString())).ToString("#,##0"), dRow.Item("PKG_UT_NM").ToString()))
#If False Then  'UPD 2020/06/23 012642   引き当て時に古いロット（入庫日）がわからない
                    .SetCellValue(sRowCnt, sprDetailDef.REMARK.ColNo, dRow.Item("REMARK").ToString())
#Else
                    If inko_dateFlg = "1" Then
                        .SetCellValue(sRowCnt, sprDetailDef.REMARK.ColNo, String.Concat(dRow.Item("INKO_DATE").ToString(), " ", dRow.Item("REMARK").ToString()))
                    Else
                        .SetCellValue(sRowCnt, sprDetailDef.REMARK.ColNo, dRow.Item("REMARK").ToString())
                    End If
#End If
                    .SetCellValue(sRowCnt, sprDetailDef.REMARK_OUT.ColNo, dRow.Item("REMARK_OUT").ToString())
                    .SetCellValue(sRowCnt, sprDetailDef.TAX_KB.ColNo, dRow.Item("TAX_KB_NM").ToString())
                    .SetCellValue(sRowCnt, sprDetailDef.HIKIATE_ALERT_NM.ColNo, dRow.Item("HIKIATE_ALERT_NM").ToString())
                    .SetCellValue(sRowCnt, sprDetailDef.DEST_NM.ColNo, dRow.Item("DEST_NM").ToString())
                    .SetCellValue(sRowCnt, sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo, dRow.Item("OUTKA_FROM_ORD_NO_L").ToString())
                    .SetCellValue(sRowCnt, sprDetailDef.CD_NRS.ColNo, dRow.Item("GOODS_CD_NRS").ToString())
                    .SetCellValue(sRowCnt, sprDetailDef.IRIME_UT.ColNo, dRow.Item("IRIME_UT_NM").ToString())
                    .SetCellValue(sRowCnt, sprDetailDef.HIKIATE_ALERT_YN.ColNo, dRow.Item("HIKIATE_ALERT_YN").ToString())
                    .SetCellValue(sRowCnt, sprDetailDef.SMPL.ColNo, dRow.Item("SMPL").ToString())
                    .SetCellValue(sRowCnt, sprDetailDef.ZAI_REC_NO.ColNo, dRow.Item("ZAI_REC_NO").ToString())
                    .SetCellValue(sRowCnt, sprDetailDef.DEST_CD.ColNo, dRow.Item("DEST_CD").ToString())

                    .SetCellValue(sRowCnt, sprDetailDef.ALLOC_CAN_NB.ColNo, Convert.ToDecimal(Me.FormatNumValue(AllocCanNb.ToString)).ToString("#,##0"))

                End If

            Next

            .ResumeLayout(True)

        End With

        Return Me._Frm.sprZai.ActiveSheet.Rows.Count - 1

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Private Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

    ''' <summary>
    ''' INのデータセットの値を画面に表示
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetInitForm(ByVal frm As LMD100F, ByVal prmDs As DataSet) As Boolean

        Dim custNm As String = String.Empty
        Dim strSqlCust As String = String.Empty

        With prmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN)
            frm.cmbEigyo.SelectedValue = .Rows(0)("NRS_BR_CD").ToString
            frm.cmbSoko.SelectedValue = .Rows(0)("WH_CD").ToString
            frm.lblCustCD_L.TextValue = .Rows(0)("CUST_CD_L").ToString
            frm.lblCustCD_M.TextValue = .Rows(0)("CUST_CD_M").ToString

            '荷主名称はキャッシュより取得
            If Me.GetCustNm(frm, prmDs) = False Then

                Return False

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主名称をキャッシュから取得
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCustNm(ByVal frm As LMD100F, ByVal prmDs As DataSet) As Boolean

        Dim custNm As String = String.Empty
        Dim strSqlCust As String = String.Empty

        With prmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN)
            strSqlCust = String.Concat("NRS_BR_CD = '", .Rows(0)("NRS_BR_CD").ToString, "' ")
            strSqlCust = String.Concat(strSqlCust, "AND CUST_CD_L = '", .Rows(0)("CUST_CD_L").ToString, "' ")
            strSqlCust = String.Concat(strSqlCust, "AND CUST_CD_M = '", .Rows(0)("CUST_CD_M").ToString, "' ")
            strSqlCust = String.Concat(strSqlCust, "AND SYS_DEL_FLG = '0'")
            Dim custRows As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(strSqlCust)
            If 0 < custRows.Count Then
                '存在時は名称を画面に設定
                frm.lblCustNM_L.TextValue = custRows(0).Item("CUST_NM_L").ToString()
                frm.lblCustNM_M.TextValue = custRows(0).Item("CUST_NM_M").ToString()

            Else
                '存在しない時FALSEを返却
                Return False

            End If

        End With

        Return True

    End Function

#End Region 'Spread

#End Region

End Class
