' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI      : 請求
'  プログラムID     :  LMI030G  : 請求データ作成 [デュポン用]
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

''' <summary>
''' LMI030Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI030G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI030F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI030F)

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
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = LMIControlC.FUNCTION_EXEL
            .F9ButtonName = LMIControlC.FUNCTION_KENSAKU
            .F10ButtonName = LMIControlC.FUNCTION_POP
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
            .F8ButtonEnabled = always
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
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

            .cmbEigyo.TabIndex = LMI030C.CtlTabIndex.EIGYO
            .txtCustCdL.TabIndex = LMI030C.CtlTabIndex.CUSTCDL
            .txtCustCdM.TabIndex = LMI030C.CtlTabIndex.CUSTCDM
            .txtCustCdS.TabIndex = LMI030C.CtlTabIndex.CUSTCDS
            .txtCustCdSS.TabIndex = LMI030C.CtlTabIndex.CUSTCDSS
            .imdSeiqtoFrom.TabIndex = LMI030C.CtlTabIndex.SEIKYUFROM
            .imdSeiqtoTo.TabIndex = LMI030C.CtlTabIndex.SEIKYUTO
            .cmbJigyoubu.TabIndex = LMI030C.CtlTabIndex.JIGYO
            .cmbSeikyu.TabIndex = LMI030C.CtlTabIndex.CMBSEIKYU
            .btnMake.TabIndex = LMI030C.CtlTabIndex.BTNMAKE
            .cmbPrint.TabIndex = LMI030C.CtlTabIndex.CMBPRINT
            .btnPrint.TabIndex = LMI030C.CtlTabIndex.BTNPRINT
            .sprDetail.TabIndex = LMI030C.CtlTabIndex.SPRDETAILS

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
            '前月の末日を求める
            Dim lastDate As String = Convert.ToString(DateSerial(Convert.ToInt32(Mid(sysDate, 1, 4)), Convert.ToInt32(Mid(sysDate, 5, 2)), 1).AddDays(-1))
            lastDate = String.Concat( _
                                    Mid(lastDate, 1, 4), _
                                    Mid(lastDate, 6, 2), _
                                    Mid(lastDate, 9, 2) _
                                   )
            Dim firstDate As String = String.Concat( _
                                                    Mid(lastDate, 1, 6), _
                                                    "01" _
                                                   )
            .imdSeiqtoFrom.TextValue = firstDate
            .imdSeiqtoTo.TextValue = lastDate

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
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        '******* 表示列 *******
        'START YANAI 要望番号830
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI030C.SprColumnIndex.DEF, "  ", 20, False)
        'END YANAI 要望番号830
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMI030C.SprColumnIndex.CUST_NM, "荷主名", 300, True)
        Public Shared DEPART As SpreadColProperty = New SpreadColProperty(LMI030C.SprColumnIndex.DEPART, "事業部", 200, True)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMI030C.SprColumnIndex.NRS_BR_NM, "営業所", 150, True)
        Public Shared SEKYUTSUKI As SpreadColProperty = New SpreadColProperty(LMI030C.SprColumnIndex.SEKYUTSUKI, "請求月", 80, True)
        Public Shared SHINCHOKU As SpreadColProperty = New SpreadColProperty(LMI030C.SprColumnIndex.SHINCHOKU, "進捗", 120, True)
        'START YANAI 要望番号830
        Public Shared TANTO_NM As SpreadColProperty = New SpreadColProperty(LMI030C.SprColumnIndex.TANTO_NM, "担当者名", 200, True)
        Public Shared SHINCHOKU_KB As SpreadColProperty = New SpreadColProperty(LMI030C.SprColumnIndex.SHINCHOKU_KB, "進捗区分", 0, False)
        'END YANAI 要望番号830

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        'レイアウト用
        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim sLabelRight As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
        Dim sLabelLeft As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sTxt As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, False)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999, False)
        Dim sCmb As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, Nothing, False)
        Dim sTxtlo As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, True)

        With spr

            '行クリア
            .CrearSpread()

            .SuspendLayout()

            .ActiveSheet.Rows.Count = 0

            .SetColProperty(New LMI030G.sprDetailDef())

            'START YANAI 要望番号830
            '.ActiveSheet.ColumnCount = 6
            .ActiveSheet.ColumnCount = 8
            'END YANAI 要望番号830

            ''列固定位置を設定します。(ex.納入予定で固定)
            '.ActiveSheet.FrozenColumnCount = LMI030G.sprDetailDef.DEF.ColNo + 1

            'スプレッドの設定
            .SetCellStyle(0, LMI030G.sprDetailDef.DEF.ColNo, sCheck)
            .SetCellStyle(0, LMI030G.sprDetailDef.CUST_NM.ColNo, sLabelLeft)
            .SetCellStyle(0, LMI030G.sprDetailDef.DEPART.ColNo, sLabelLeft)
            .SetCellStyle(0, LMI030G.sprDetailDef.NRS_BR_NM.ColNo, sLabelLeft)
            .SetCellStyle(0, LMI030G.sprDetailDef.SEKYUTSUKI.ColNo, sLabelLeft)
            .SetCellStyle(0, LMI030G.sprDetailDef.SHINCHOKU.ColNo, sLabelLeft)
            'START YANAI 要望番号830
            .SetCellStyle(0, LMI030G.sprDetailDef.TANTO_NM.ColNo, sLabelLeft)
            .SetCellStyle(0, LMI030G.sprDetailDef.SHINCHOKU_KB.ColNo, sLabelLeft)
            'END YANAI 要望番号830

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        'レイアウト用
        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim sLabelRight As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
        Dim sLabelLeft As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sTxt As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, False)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999, False)
        Dim sCmb As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, Nothing, False)
        Dim sTxtlo As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, True)
        Dim max As Integer = dt.Rows.Count - 1
        Dim setCnt As Integer = 0

        With spr

            '行クリア
            .CrearSpread()

            .SuspendLayout()

            .ActiveSheet.Rows.Count = 0

            sTxt.BackColor = Color.White
            sCmb.BackColor = Color.White

            'START YANAI 要望番号830
            '.ActiveSheet.ColumnCount = 6
            .ActiveSheet.ColumnCount = 8
            'END YANAI 要望番号830

            Dim rowCnt As Integer = 0
            Dim outDr() As DataRow = Nothing
            Dim stateKb As String = String.Empty
            Dim departCd As String = String.Empty
            Dim departNm As String = String.Empty
            '表示開始年月、表示終了年月を設定
            Dim startYear As Integer = Convert.ToInt32(Mid(Me._Frm.imdSeiqtoFrom.TextValue, 1, 4))
            Dim startMonth As Integer = Convert.ToInt32(Mid(Me._Frm.imdSeiqtoFrom.TextValue, 5, 2))
            Dim endYear As Integer = Convert.ToInt32(Mid(Me._Frm.imdSeiqtoTo.TextValue, 1, 4))
            Dim endMonth As Integer = Convert.ToInt32(Mid(Me._Frm.imdSeiqtoTo.TextValue, 5, 2))
            '営業所名を取得
            Dim nrsBrNm As String = String.Empty
            'START YANAI 要望番号830
            'outDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyo.SelectedValue, "'"))
            'If 0 < outDr.Length Then
            '    nrsBrNm = outDr(0).Item("NRS_BR_NM").ToString
            'End If
            'END YANAI 要望番号830
            '要望番号:1253 terakawa 2012.07.13 Start
            Dim nrsBrCd As String = String.Empty
            '要望番号:1253 terakawa 2012.07.13 End
            Dim custCdL As String = String.Empty
            Dim custCdM As String = String.Empty
            Dim custCdS As String = String.Empty
            Dim custCdSS As String = String.Empty
            Dim custNm As String = String.Empty
            'START YANAI 要望番号830
            Dim tantoNm As String = String.Empty
            Dim shinchoku As String = String.Empty
            Dim custDetailsDr() As DataRow = Nothing
            'END YANAI 要望番号830

            For i As Integer = 0 To max
                startYear = Convert.ToInt32(Mid(Me._Frm.imdSeiqtoFrom.TextValue, 1, 4))
                startMonth = Convert.ToInt32(Mid(Me._Frm.imdSeiqtoFrom.TextValue, 5, 2))

                If (custCdL).Equals(dt.Rows(i).Item("CUST_CD_L").ToString) = False OrElse _
                    (custCdM).Equals(dt.Rows(i).Item("CUST_CD_M").ToString) = False OrElse _
                    (custCdS).Equals(dt.Rows(i).Item("CUST_CD_S").ToString) = False OrElse _
                    (custCdSS).Equals(dt.Rows(i).Item("CUST_CD_SS").ToString) = False Then

                    'START YANAI 要望番号830
                    outDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD = '", dt.Rows(i).Item("NRS_BR_CD").ToString, "'"))
                    If 0 < outDr.Length Then
                        nrsBrNm = outDr(0).Item("NRS_BR_NM").ToString
                    End If
                    'END YANAI 要望番号830
                    '要望番号:1253 terakawa 2012.07.13 Start
                    '営業所名を取得
                    nrsBrCd = dt.Rows(i).Item("NRS_BR_CD").ToString()
                    '要望番号:1253 terakawa 2012.07.13 End

                    custCdL = dt.Rows(i).Item("CUST_CD_L").ToString
                    custCdM = dt.Rows(i).Item("CUST_CD_M").ToString
                    custCdS = dt.Rows(i).Item("CUST_CD_S").ToString
                    custCdSS = dt.Rows(i).Item("CUST_CD_SS").ToString
                    '荷主名を取得
                    custNm = dt.Rows(i).Item("CUST_NM").ToString
                    tantoNm = dt.Rows(i).Item("TANTO_NM").ToString
                    outDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", custCdL, "' AND ", _
                                                                                                    "CUST_CD_M = '", custCdM, "' AND ", _
                                                                                                    "CUST_CD_S = '", custCdS, "' AND ", _
                                                                                                    "CUST_CD_SS = '", custCdSS, "'"))

                    '事業所名を設定
                    departCd = dt.Rows(i).Item("DEPART").ToString
                    departNm = dt.Rows(i).Item("DEPART_NM").ToString

                    Do
                        stateKb = String.Empty
                        .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

                        outDr = dt.Select(String.Concat("CUST_CD_L = '", custCdL, "' AND ", _
                                                        "CUST_CD_M = '", custCdM, "' AND ", _
                                                        "CUST_CD_S = '", custCdS, "' AND ", _
                                                        "CUST_CD_SS = '", custCdSS, "' AND ", _
                                                        "SEKY_YM = '", startYear, MaeCoverData(Convert.ToString(startMonth), "0", 2), "'"))

                        'START YANAI 要望番号830
                        shinchoku = String.Empty
                        '要望番号:1253 terakawa 2012.07.13 Start
                        'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD LIKE '", custCdL, custCdM, custCdS, custCdSS, "%' AND ", _
                        '                                                                                                "SUB_KB = '14'"))
                        custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                                                                                                        "CUST_CD LIKE '", custCdL, custCdM, custCdS, custCdSS, "%' AND ", _
                                                                                                                        "SUB_KB = '14'"))
                        '要望番号:1253 terakawa 2012.07.13 End
                        If 0 < custDetailsDr.Length Then
                            stateKb = "対象外"
                        Else
                            If 0 < outDr.Length Then
                                '作成済みの場合
                                shinchoku = outDr(0).Item("SHINCHOKU").ToString()
                                If ("01").Equals(shinchoku) = True Then
                                    stateKb = "計算済"
                                End If
                                If ("02").Equals(shinchoku) = True Then
                                    stateKb = "作成済"
                                End If
                            Else
                                '未作成の場合
                                stateKb = "未計算"
                            End If

                        End If

                        'If 0 < outDr.Length Then
                        '    '作成済みの場合
                        '    stateKb = "作成"
                        'Else
                        '    '未作成の場合
                        '    stateKb = "未作成"
                        'End If
                        'END YANAI 要望番号830

                        'スプレッドの設定
                        .SetCellStyle(rowCnt, LMI030G.sprDetailDef.DEF.ColNo, sCheck)
                        .SetCellStyle(rowCnt, LMI030G.sprDetailDef.CUST_NM.ColNo, sLabelLeft)
                        .SetCellStyle(rowCnt, LMI030G.sprDetailDef.DEPART.ColNo, sLabelLeft)
                        .SetCellStyle(rowCnt, LMI030G.sprDetailDef.NRS_BR_NM.ColNo, sLabelLeft)
                        .SetCellStyle(rowCnt, LMI030G.sprDetailDef.SEKYUTSUKI.ColNo, sLabelLeft)
                        .SetCellStyle(rowCnt, LMI030G.sprDetailDef.SHINCHOKU.ColNo, sLabelLeft)
                        'START YANAI 要望番号830
                        .SetCellStyle(rowCnt, LMI030G.sprDetailDef.TANTO_NM.ColNo, sLabelLeft)
                        .SetCellStyle(rowCnt, LMI030G.sprDetailDef.SHINCHOKU_KB.ColNo, sLabelLeft)
                        'END YANAI 要望番号830

                        .SetCellValue(rowCnt, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                        .SetCellValue(rowCnt, sprDetailDef.CUST_NM.ColNo, custNm)
                        .SetCellValue(rowCnt, sprDetailDef.DEPART.ColNo, departNm)
                        .SetCellValue(rowCnt, sprDetailDef.NRS_BR_NM.ColNo, nrsBrNm)
                        .SetCellValue(rowCnt, sprDetailDef.SEKYUTSUKI.ColNo, String.Concat(startYear, "/", MaeCoverData(Convert.ToString(startMonth), "0", 2)))
                        'START YANAI 要望番号830
                        '.SetCellValue(rowCnt, sprDetailDef.SHINCHOKU.ColNo, stateKb)
                        .SetCellValue(rowCnt, sprDetailDef.SHINCHOKU.ColNo, stateKb)
                        .SetCellValue(rowCnt, sprDetailDef.TANTO_NM.ColNo, tantoNm)
                        .SetCellValue(rowCnt, sprDetailDef.SHINCHOKU_KB.ColNo, shinchoku)
                        'END YANAI 要望番号830

                        rowCnt = rowCnt + 1

                        If (startYear).Equals(endYear) AndAlso _
                           (startMonth).Equals(endMonth) Then
                            Exit Do
                        End If

                        startMonth = startMonth + 1
                        If startMonth = 13 Then
                            startYear = startYear + 1
                            startMonth = 1
                        End If
                    Loop

                End If
            Next

            .ResumeLayout(True)

        End With

    End Sub

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

#End Region

#End Region

#End Region

End Class
