' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI962G : EDIワーニング・エラーチェック画面
'  作  成  者       :  
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMI962Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMI962G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI962F

    Private _LMIconG As LMIControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI962F)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMIconG = New LMIControlG(frm)

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
            .F9ButtonName = String.Empty
            .F10ButtonName = String.Empty
            .F11ButtonName = "登　録"
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
            .F9ButtonEnabled = False
            .F10ButtonEnabled = False
            .F11ButtonEnabled = always  '(F11)登録
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

            .cmbShubetu.TabIndex = LMI962C.CtlTabIndex.SHUBETSU
            .cmbNrsBr.TabIndex = LMI962C.CtlTabIndex.NRS_BR
            .cmbNrsWh.TabIndex = LMI962C.CtlTabIndex.NRS_WH
            .grpWarning.TabIndex = LMI962C.CtlTabIndex.GRP_WARNING
            .sprWarning.TabIndex = LMI962C.CtlTabIndex.SPR_WARNING
            .txtLoadNumber.TabIndex = LMI962C.CtlTabIndex.LOAD_NUMBER
            .txtCmdGyo.TabIndex = LMI962C.CtlTabIndex.CMD_GYO
            .txtKomokuNM.TabIndex = LMI962C.CtlTabIndex.KOMOKU_NM
            .txtKomokuVal.TabIndex = LMI962C.CtlTabIndex.KOMOKU_VAL
            .txtMastVal.TabIndex = LMI962C.CtlTabIndex.MST_VAL
            .txtWarning.TabIndex = LMI962C.CtlTabIndex.TXT_WARNING

        End With

    End Sub
    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetForm(ByRef frm As LMI962F, ByVal ds As DataSet) As Boolean

        If ds Is Nothing OrElse ds.Tables(LMI962C.TABLE_NM_HED).Rows.Count = 0 Then
            Return False
        End If

        Dim drHed As DataRow = ds.Tables(LMI962C.TABLE_NM_HED).Rows(0)
        Dim drDtl As DataRow = ds.Tables(LMI962C.TABLE_NM_DTL).Rows(0)

        With frm

            'ヘッダ部
            Select Case drHed.Item("INOUT_KB").ToString
                Case LMI960C.InOutKb.Inka
                    .cmbShubetu.SelectedValue = "10"    '10:入荷登録
                Case LMI960C.InOutKb.Outka
                    .cmbShubetu.SelectedValue = "20"    '20:出荷登録
                Case LMI960C.InOutKb.Unso
                    .cmbShubetu.SelectedValue = "40"    '40:運送登録
            End Select
            .cmbNrsBr.SelectedValue = drHed.Item("NRS_BR_CD").ToString()

            '詳細部
            Call SetDetailData(ds, 0)

        End With

        Return True

    End Function

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="lockFlg">モードによるロック制御を行う。</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = "")

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

            .cmbNrsWh.TextValue = String.Empty
            .txtCustCD_L.TextValue = String.Empty
            .txtCustCD_M.TextValue = String.Empty
            .lblCustNM.TextValue = String.Empty

            .txtOrderNoL.TextValue = String.Empty
            .txtOrderNoM.TextValue = String.Empty
            .txtEdiKanriNoL.TextValue = String.Empty
            .txtEdiKanriNoM.TextValue = String.Empty

            .txtLoadNumber.TextValue = String.Empty
            .txtCmdGyo.TextValue = String.Empty
            .txtKomokuNM.TextValue = String.Empty
            .txtKomokuVal.TextValue = String.Empty
            .txtMastVal.TextValue = String.Empty
            .txtWarning.TextValue = String.Empty

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

    Friend Sub SetDetailData(ByVal ds As DataSet, Optional ByVal rowNo As Integer = -1)

        With Me._Frm

            If rowNo < 0 Then
                rowNo = .sprWarning.ActiveSheet.ActiveRowIndex()
            End If

            .cmbNrsWh.SelectedValue = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.WH_CD.ColNo))
            .txtCustCD_L.TextValue = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.CUST_CD_L.ColNo))
            .txtCustCD_M.TextValue = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.CUST_CD_M.ColNo))

            Dim custDrs As DataRow() = Me._LMIconG.SelectCustListDataRowByNrsBrCd(.cmbNrsBr.SelectedValue.ToString, .txtCustCD_L.TextValue, .txtCustCD_M.TextValue, "00", "00")
            If custDrs.Length > 0 Then
                .lblCustNM.TextValue = String.Concat(custDrs(0).Item("CUST_NM_L"), "　", custDrs(0).Item("CUST_NM_M"))
            End If

            .txtOrderNoL.TextValue = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.ORDER_NO_L.ColNo))
            .txtOrderNoM.TextValue = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.ORDER_NO_M.ColNo))
            .txtEdiKanriNoL.TextValue = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.KANRI_NO_L.ColNo))
            .txtEdiKanriNoM.TextValue = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.KANRI_NO_M.ColNo))

            .txtLoadNumber.TextValue = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.LOAD_NUMBER.ColNo))
            .txtCmdGyo.TextValue = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.CMD_GYO.ColNo))
            .txtKomokuNM.TextValue = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.KOMOKU_NM.ColNo))
#If False Then ' フィルメニッヒ セミEDI対応  20160912 changed inoue
            .txtKomokuVal.TextValue = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.KOMOKU_VAL.ColNo))
#Else
            Dim komokuVal As String = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.KOMOKU_VAL.ColNo))
            Dim addFieldVal As String = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.ADDITIONAL_FIELD_VALUE_1.ColNo))
            Dim mstFlg As String = Me.GetMstFlg(Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.EDI_WARNING_ID.ColNo)))
            If (String.IsNullOrEmpty(addFieldVal) = False AndAlso
                LMI962C.WARNING_ID_FMT.MST_FLG.M_GOODS.Equals(mstFlg)) Then
                .txtKomokuVal.TextValue = String.Format("{0}, {1}", komokuVal, addFieldVal)
            Else
                .txtKomokuVal.TextValue = komokuVal
            End If
#End If
            .txtMastVal.TextValue = Me.GetCellValue(.sprWarning.ActiveSheet.Cells(rowNo, LMI962G.sprWarning.MASTER_VAL.ColNo))


            Dim dr As DataRow = ds.Tables(LMI962C.TABLE_NM_DTL).Rows(rowNo)
            Dim messageInfo As DataRow() = Me.GetMessage(dr.Item("MESSAGE_ID").ToString())
            Dim warningMsg As String = Me.GetWarningMsg(messageInfo(0), dr)
            .txtWarning.TextValue = warningMsg

        End With

    End Sub

    ''' <summary>
    ''' セルから値を取得
    ''' </summary>
    ''' <param name="aCell">セル</param>
    ''' <returns>取得した値</returns>
    ''' <remarks></remarks>
    Friend Function GetCellValue(ByVal aCell As Cell) As String

        GetCellValue = String.Empty

        If TypeOf aCell.Editor Is CellType.ComboBoxCellType Then

            'コンボボックスの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.CheckBoxCellType Then

            'チェックボックスの場合、0 or 1を返却
            If Me.changeBooleanCheckBox(aCell.Text) = True Then
                GetCellValue = 1.ToString()
            Else
                GetCellValue = 0.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.NumberCellType Then

            'ナンバーの場合、Value値を返却
            If aCell.Value Is Nothing = False _
                AndAlso String.IsNullOrEmpty(aCell.Value.ToString()) = False Then
                GetCellValue = aCell.Value.ToString()
            Else
                GetCellValue = 0.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.DateTimeCellType Then

            '日付の場合、Value値を yyyyMMdd に変換して返却
            If aCell.Value Is Nothing = False AndAlso String.IsNullOrEmpty(aCell.Value.ToString()) = False Then
                GetCellValue = Convert.ToDateTime(aCell.Value).ToString("yyyyMMdd")
            End If

        Else
            'テキストの場合、Trimした値を返却
            GetCellValue = aCell.Text.Trim()

        End If

        Return GetCellValue

    End Function

    ''' <summary>
    ''' チェックボックスの値をString型からBoolean型に変換する
    ''' </summary>
    ''' <param name="textValue">obj.text(0:チェック無し,1:チェック有り)</param>
    ''' <returns>True:チェック有り,False:チェック無し</returns>
    ''' <remarks></remarks>
    Private Function changeBooleanCheckBox(ByVal textValue As String) As Boolean

        '"1"の場合Trueを返却
        If (LMConst.FLG.ON.Equals(textValue) = True) _
            OrElse True.ToString().Equals(textValue) = True Then
            Return True
        End If

        '"0"の場合Falseを返却
        Return False

    End Function

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprWarning

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.DEF, " ", 20, True)
        Public Shared SHORI As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.SHORI, "処理", 80, True)
        Public Shared ORDER_NO_L As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.ORDER_NO_L, "オーダー番号(大)", 0, False)
        Public Shared ORDER_NO_M As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.ORDER_NO_M, "オーダー番号(中)", 0, False)
        Public Shared LOAD_NUMBER As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.LOAD_NUMBER, "Load Number", 130, True)
        Public Shared CMD_GYO As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.CMD_GYO, "商品" & vbCrLf & "行番号", 60, True)
        Public Shared MESSAGE_ID As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.MESSAGE_ID, "メッセージ" & vbCrLf & "ID", 100, True)
        Public Shared WARNING_MSG As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.WARNING_MSG, "ワーニング内容", 300, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.GOODS_NM, "商品名", 150, True)
        '2012.06.01 ディック届先マスタ対応 追加START
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.DEST_NM, "届先名", 0, False)
        '2012.06.01 ディック届先マスタ対応 追加END
        Public Shared KOMOKU_NM As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.KOMOKU_NM, "項目名", 150, True)
        Public Shared KOMOKU_VAL As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.KOMOKU_VAL, "項目値", 150, True)
        Public Shared MASTER_VAL As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.MASTER_VAL, "マスタ値", 150, True)
        Public Shared KANRI_NO_L As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.EDI_CTL_NO_L, "EDI管理番号(大)", 0, False)
        Public Shared KANRI_NO_M As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.EDI_CTL_NO_M, "EDI管理番号(中)", 0, False)
        Public Shared EDI_WARNING_ID As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.EDI_WARNING_ID, "ID", 0, False)
#If True Then ' フィルメニッヒ セミEDI対応  20160912 added inoue
        Public Shared ADDITIONAL_FIELD_VALUE_1 As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.ADDITIONAL_FIELD_VALUE_1, "追加項目値", 0, False)
#End If

        '非表示項目
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.CUST_CD_L, "CUST_CD_L", 0, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.CUST_CD_M, "CUST_CD_M", 0, False)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.WH_CD, "WH_CD", 0, False)
        Public Shared CRT_DATE As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.CRT_DATE, "CRT_DATE", 0, False)
        Public Shared FILE_NAME As SpreadColProperty = New SpreadColProperty(LMI962C.SprWarning.FILE_NAME, "FILE_NAME", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprWarning.CrearSpread()

            '列数設定
            .sprWarning.Sheets(0).ColumnCount = LMI962C.SprWarning.LAST_INDEX

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprWarning.SetColProperty(New sprWarning)

            '列固定位置を設定
            .sprWarning.Sheets(0).FrozenColumnCount = sprWarning.SHORI.ColNo + 1

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal prmDs As DataSet)

        Dim spr As LMSpread = Me._Frm.sprWarning

        With spr

            .SuspendLayout()

            'データ挿入
            '行数設定
            Dim dt As DataTable = prmDs.Tables(LMI962C.TABLE_NM_DTL)
            Dim lngcnt As Integer = dt.Rows.Count

            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, True)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim dr As DataRow

            Dim warningMsg As String = String.Empty
            Dim mastShoriKb As String = String.Empty
            Dim messageInfo As DataRow()

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dr = dt.Rows(i)
                'メッセージ情報取得
                messageInfo = Me.GetMessage(dr.Item("MESSAGE_ID").ToString())
                '処理区分取得
                mastShoriKb = Me.GetMastShoriKb(messageInfo(0))
                'ワーニングメッセージ取得
                warningMsg = Me.GetWarningMsg(messageInfo(0), dr)


                'セルスタイル設定（表示項目）
                .SetCellStyle(i, sprWarning.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprWarning.SHORI.ColNo, LMSpreadUtility.GetComboCellKbn(spr, mastShoriKb, False))
                .SetCellStyle(i, sprWarning.ORDER_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprWarning.ORDER_NO_M.ColNo, sLabel)
                .SetCellStyle(i, sprWarning.LOAD_NUMBER.ColNo, sLabel)
                .SetCellStyle(i, sprWarning.CMD_GYO.ColNo, sLabel)
                .SetCellStyle(i, sprWarning.MESSAGE_ID.ColNo, sLabel)
                .SetCellStyle(i, sprWarning.WARNING_MSG.ColNo, sLabel)
                .SetCellStyle(i, sprWarning.GOODS_NM.ColNo, sLabel)
                '2012.06.01 ディック届先マスタ対応 追加START
                .SetCellStyle(i, sprWarning.DEST_NM.ColNo, sLabel)
                '2012.06.01 ディック届先マスタ対応 追加END
                .SetCellStyle(i, sprWarning.KOMOKU_NM.ColNo, sLabel)
                .SetCellStyle(i, sprWarning.KOMOKU_VAL.ColNo, sLabel)
                .SetCellStyle(i, sprWarning.MASTER_VAL.ColNo, sLabel)
                .SetCellStyle(i, sprWarning.KANRI_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprWarning.KANRI_NO_M.ColNo, sLabel)
                .SetCellStyle(i, sprWarning.EDI_WARNING_ID.ColNo, sLabel)
#If True Then ' フィルメニッヒ セミEDI対応  20160912 added inoue
                .SetCellStyle(i, sprWarning.ADDITIONAL_FIELD_VALUE_1.ColNo, sLabel)
#End If

                'セルに値を設定
                .SetCellValue(i, sprWarning.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprWarning.SHORI.ColNo, "01")
                .SetCellValue(i, sprWarning.ORDER_NO_L.ColNo, Me.NullConvertString(dr.Item("CUST_ORD_NO")).ToString())
                .SetCellValue(i, sprWarning.ORDER_NO_M.ColNo, Me.NullConvertString(dr.Item("CUST_ORD_NO_DTL")).ToString())
                .SetCellValue(i, sprWarning.LOAD_NUMBER.ColNo, Me.NullConvertString(dr.Item("SHIPMENT_ID")).ToString())
                .SetCellValue(i, sprWarning.CMD_GYO.ColNo, Me.NullConvertString(dr.Item("CMD_GYO")).ToString())
                .SetCellValue(i, sprWarning.MESSAGE_ID.ColNo, Me.NullConvertString(dr.Item("MESSAGE_ID")).ToString())
                .SetCellValue(i, sprWarning.WARNING_MSG.ColNo, warningMsg)
                .SetCellValue(i, sprWarning.GOODS_NM.ColNo, Me.NullConvertString(dr.Item("GOODS_NM")).ToString())
                '2012.06.01 ディック届先マスタ対応 追加START
                .SetCellValue(i, sprWarning.DEST_NM.ColNo, Me.NullConvertString(dr.Item("DEST_NM")).ToString())
                '2012.06.01 ディック届先マスタ対応 追加END
                .SetCellValue(i, sprWarning.KOMOKU_NM.ColNo, Me.NullConvertString(dr.Item("FIELD_NM")).ToString())
                .SetCellValue(i, sprWarning.KOMOKU_VAL.ColNo, Me.NullConvertString(dr.Item("FIELD_VALUE")).ToString())
                .SetCellValue(i, sprWarning.MASTER_VAL.ColNo, Me.NullConvertString(dr.Item("MST_VALUE")).ToString())
                .SetCellValue(i, sprWarning.KANRI_NO_L.ColNo, Me.NullConvertString(dr.Item("EDI_CTL_NO_L")).ToString())
                .SetCellValue(i, sprWarning.KANRI_NO_M.ColNo, Me.NullConvertString(dr.Item("EDI_CTL_NO_M")).ToString())
                .SetCellValue(i, sprWarning.EDI_WARNING_ID.ColNo, Me.NullConvertString(dr.Item("EDI_WARNING_ID")).ToString())
#If True Then ' フィルメニッヒ セミEDI対応  20160912 added inoue
                Dim ADDITIONAL_FIELD_VALUE_1_NAME As String = "ADDITIONAL_FIELD_VALUE_1"

                If (dr.Table.Columns.Contains(ADDITIONAL_FIELD_VALUE_1_NAME) AndAlso
                    IsDBNull(dr.Item(ADDITIONAL_FIELD_VALUE_1_NAME)) = False AndAlso
                    dr.Item(ADDITIONAL_FIELD_VALUE_1_NAME) IsNot Nothing) Then

                    .SetCellValue(i _
                                , sprWarning.ADDITIONAL_FIELD_VALUE_1.ColNo _
                                , TryCast(dr.Item(ADDITIONAL_FIELD_VALUE_1_NAME), String))
                Else
                    .SetCellValue(i, sprWarning.ADDITIONAL_FIELD_VALUE_1.ColNo, String.Empty)
                End If
#End If

                '非表示項目
                .SetCellValue(i, sprWarning.CUST_CD_L.ColNo, Me.NullConvertString(dr.Item("CUST_CD_L")).ToString())
                .SetCellValue(i, sprWarning.CUST_CD_M.ColNo, Me.NullConvertString(dr.Item("CUST_CD_M")).ToString())
                .SetCellValue(i, sprWarning.WH_CD.ColNo, Me.NullConvertString(dr.Item("WH_CD")).ToString())
                .SetCellValue(i, sprWarning.CRT_DATE.ColNo, Me.NullConvertString(dr.Item("CRT_DATE")).ToString())
                .SetCellValue(i, sprWarning.FILE_NAME.ColNo, Me.NullConvertString(dr.Item("FILE_NAME")).ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 処理区分取得
    ''' </summary>
    ''' <param name="msgInf"></param>
    ''' <returns></returns>
    ''' <remarks>ワーニングIDに付随するボタン数を基に処理コンボの設定値(区分コード)を取得</remarks>
    Private Function GetMastShoriKb(ByVal msgInf As DataRow) As String
        Dim btnCnt As String = String.Empty
        Dim mastKb As String = String.Empty

        btnCnt = msgInf.Item("BUTTON_CNT").ToString()
        If btnCnt.Equals("2") Then
            mastKb = "E015"
        ElseIf btnCnt.Equals("3") Then
            mastKb = "E016"
        Else
            mastKb = String.Empty
        End If

        Return mastKb

    End Function

    ''' <summary>
    ''' ワーニングメッセージ作成
    ''' </summary>
    ''' <param name="msgInf"></param>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks>ワーニングIDからワーニングメッセージを取得し置換処理、改行処理を行う</remarks>
    Private Function GetWarningMsg(ByVal msgInf As DataRow, ByVal dr As DataRow) As String

        Dim warningMsg As String = String.Empty

        Dim parm1 As String = Me.NullConvertString(dr.Item("PARA1")).ToString()
        Dim parm2 As String = Me.NullConvertString(dr.Item("PARA2")).ToString()
        Dim parm3 As String = Me.NullConvertString(dr.Item("PARA3")).ToString()
        Dim parm4 As String = Me.NullConvertString(dr.Item("PARA4")).ToString()
        Dim parm5 As String = Me.NullConvertString(dr.Item("PARA5")).ToString()

        warningMsg = msgInf.Item("MESSAGE_STRING").ToString()

        warningMsg = warningMsg.Replace("[%1]", parm1)
        warningMsg = warningMsg.Replace("[%2]", parm2)
        warningMsg = warningMsg.Replace("[%3]", parm3)
        warningMsg = warningMsg.Replace("[%4]", parm4)
        warningMsg = warningMsg.Replace("[%5]", parm5)
        warningMsg = warningMsg.Replace("[%9]", vbCrLf) '[%9]は改行
        '[%10]以降は追加文言（メッセージマスタの文字数制限を超える場合等）
        warningMsg = warningMsg.Replace("[%10]", "(※)一覧行ダブルクリックで[LMZ020]商品マスタ参照画面を開きます。")
        '2012.06.01 ディック届先マスタ対応 追加START
        warningMsg = warningMsg.Replace("[%11]", "(※)一覧行ダブルクリックで[LMZ210]届先マスタ参照画面を開きます。")
        '2012.06.01 ディック届先マスタ対応 追加END
        Return warningMsg

    End Function

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadHanei(ByVal ds As DataSet)

    End Sub

    Private Function GetMessage(ByVal messageId As String) As DataRow()

        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable("S_MESSAGE").Select("MESSAGE_ID = '" & messageId & "'")
        Return getDr

    End Function

    ''' <summary>
    ''' マスタフラグ取得
    ''' </summary>
    ''' <param name="warningId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMstFlg(ByVal warningId As String) As String

        Dim mstFlg As String = String.Empty
        If (String.IsNullOrEmpty(warningId) = False AndAlso
            warningId.Length >= LMI962C.WARNING_ID_FMT.MST_FLG.START_IDX + LMI962C.WARNING_ID_FMT.MST_FLG.LEN) Then
            mstFlg = warningId.Substring(LMI962C.WARNING_ID_FMT.MST_FLG.START_IDX _
                                       , LMI962C.WARNING_ID_FMT.MST_FLG.LEN)
        End If

        Return mstFlg
    End Function

#End Region 'Spread

#Region "Null変換"

    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertString(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = String.Empty
        End If

        Return value

    End Function

#End Region

#End Region

End Class
