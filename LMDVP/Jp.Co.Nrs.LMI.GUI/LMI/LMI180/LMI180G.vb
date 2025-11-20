' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI180  : NRC出荷／回収情報入力
'  作  成  者       :  [YANAI]
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
Imports Microsoft.VisualBasic.FileIO
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI180Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI180G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI180F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI180F, ByVal g As LMIControlG)

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
            .F1ButtonName = LMIControlC.FUNCTION_TORIKOMI
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = String.Empty
            .F11ButtonName = LMIControlC.FUNCTION_HOZON
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

            If Me._Frm.optKaishu.Checked = True Then
                edit = True
            Else
                edit = False
            End If

            'ファンクションキーの制御
            .F1ButtonEnabled = edit
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = lock
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

            .grpMode.TabIndex = LMI180C.CtlTabIndex.GRPMODE
            .optShukka.TabIndex = LMI180C.CtlTabIndex.OPTSHUKKA
            .optKaishu.TabIndex = LMI180C.CtlTabIndex.OPTKAISHU
            .optTorikeshi.TabIndex = LMI180C.CtlTabIndex.OPTTORIKESHI
            .grpExcel.TabIndex = LMI180C.CtlTabIndex.GRPEXCEL
            .imdHokokuDateFrom.TabIndex = LMI180C.CtlTabIndex.HOKOKUDATEFROM
            .imdHokokuDateTo.TabIndex = LMI180C.CtlTabIndex.HOKOKUDATETO
            .btnExcel.TabIndex = LMI180C.CtlTabIndex.BTNEXCEL
            .txtOutkaNoL.TabIndex = LMI180C.CtlTabIndex.OUTKANOL
            .txtSerialNoFrom.TabIndex = LMI180C.CtlTabIndex.SERIALNOFROM
            .txtSerialNoTo.TabIndex = LMI180C.CtlTabIndex.SERIALNOTO
            .imdKaishuDate.TabIndex = LMI180C.CtlTabIndex.KAISHUDATE
            .btnRowAdd.TabIndex = LMI180C.CtlTabIndex.BTNROWADD
            .btnRowDel.TabIndex = LMI180C.CtlTabIndex.BTNROWDEL
            .sprDetails.TabIndex = LMI180C.CtlTabIndex.SPRDETAILS

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

            '取込フォルダ・ファイルの設定
            Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'F004' AND ", _
                                                                                                            "KBN_NM1 = '", .cmbEigyo.SelectedValue, "'"))
            If 0 < kbnDr.Length Then
                '要望番号:1917 yamanaka 2013.03.06 Start
                .txtPath.TextValue = kbnDr(0).Item("KBN_NM2").ToString
                '.lblFolder.TextValue = kbnDr(0).Item("KBN_NM2").ToString
                '.lblFile.TextValue = kbnDr(0).Item("KBN_NM3").ToString
                '要望番号:1917 yamanaka 2013.03.06 End
            End If

            'ラジオボタンの設定
            .optShukka.Checked = True

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            If .optShukka.Checked = True Then
                '出荷の場合
                .txtOutkaNoL.ReadOnly = False
                .txtSerialNoFrom.ReadOnly = False
                .txtSerialNoTo.ReadOnly = False
                .imdKaishuDate.ReadOnly = True
                .btnRowAdd.Enabled = True
                .btnRowDel.Enabled = True
                '要望番号:1917 yamanaka 2013.03.06 Start
                .txtPath.ReadOnly = True
                .btnSelect.Enabled = False
                '要望番号:1917 yamanaka 2013.03.06 End
            ElseIf .optKaishu.Checked = True Then
                '回収の場合
                .txtOutkaNoL.ReadOnly = True
                .txtSerialNoFrom.ReadOnly = True
                .txtSerialNoTo.ReadOnly = True
                .imdKaishuDate.ReadOnly = False
                .btnRowAdd.Enabled = True
                .btnRowDel.Enabled = True
                '要望番号:1917 yamanaka 2013.03.06 Start
                .txtPath.ReadOnly = False
                .btnSelect.Enabled = True
                '要望番号:1917 yamanaka 2013.03.06 End
            ElseIf .optTorikeshi.Checked = True Then
                '取消の場合
                .txtOutkaNoL.ReadOnly = False
                .txtSerialNoFrom.ReadOnly = False
                .txtSerialNoTo.ReadOnly = False
                .imdKaishuDate.ReadOnly = True
                .btnRowAdd.Enabled = False
                .btnRowDel.Enabled = False
                '要望番号:1917 yamanaka 2013.03.06 Start
                .txtPath.ReadOnly = True
                .btnSelect.Enabled = False
                '要望番号:1917 yamanaka 2013.03.06 End
            End If

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .txtOutkaNoL.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .txtCustCD.TextValue = String.Empty
            .lblCustNM.TextValue = String.Empty
            .txtOutkaNoL.TextValue = String.Empty
            .lblDestNm.TextValue = String.Empty
            .txtSerialNoFrom.TextValue = String.Empty
            .txtSerialNoTo.TextValue = String.Empty
            .imdKaishuDate.TextValue = String.Empty

            'スプレッドの行をクリア
            .sprDetails.CrearSpread()

        End With

    End Sub

    ''' <summary>
    ''' 出荷データの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetOutkaData(ByVal ds As DataSet)

        Dim max As Integer = ds.Tables(LMI180C.TABLE_NM_OUT).Rows.Count - 1
        With Me._Frm

            .txtOutkaNoLOld.TextValue = .txtOutkaNoL.TextValue
            .txtCustCD.TextValue = String.Empty
            .lblCustNM.TextValue = String.Empty
            .lblDestNm.TextValue = String.Empty

            If 0 <= max Then

                .txtOutkaNoL.TextValue = String.Empty
                .txtCustCD.TextValue = ds.Tables(LMI180C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_L").ToString
                .lblCustNM.TextValue = ds.Tables(LMI180C.TABLE_NM_OUT).Rows(0).Item("CUST_NM_L").ToString
                .txtOutkaNoL.TextValue = ds.Tables(LMI180C.TABLE_NM_OUT).Rows(0).Item("OUTKA_NO_L").ToString
                .lblDestNm.TextValue = ds.Tables(LMI180C.TABLE_NM_OUT).Rows(0).Item("DEST_NM").ToString

            End If

        End With

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetails

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI180C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SERIALNO As SpreadColProperty = New SpreadColProperty(LMI180C.SprColumnIndex.SERIALNO, "シリアル№", 100, True)
        Public Shared JOTAI As SpreadColProperty = New SpreadColProperty(LMI180C.SprColumnIndex.JOTAI, "状態", 80, True)
        Public Shared NRCRECNO As SpreadColProperty = New SpreadColProperty(LMI180C.SprColumnIndex.NRCRECNO, "NRCレコード番号", 0, False)
        Public Shared OUTKANOL As SpreadColProperty = New SpreadColProperty(LMI180C.SprColumnIndex.OUTKANOL, "出荷管理番号", 0, False)
        Public Shared EDANO As SpreadColProperty = New SpreadColProperty(LMI180C.SprColumnIndex.EDANO, "枝番号", 0, False)
        Public Shared TOROKUKB As SpreadColProperty = New SpreadColProperty(LMI180C.SprColumnIndex.TOROKUKB, "登録区分", 0, False)
        Public Shared HOKOKUDATE As SpreadColProperty = New SpreadColProperty(LMI180C.SprColumnIndex.HOKOKUDATE, "報告日", 0, False)

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
            .sprDetails.Sheets(0).ColumnCount = LMI180C.SprColumnIndex.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetails.SetColProperty(New sprDetails)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの行追加
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub RowAddSpread()


        Dim spr As LMSpread = Me._Frm.sprDetails

        '列設定
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
        Dim sNum7 As StyleInfo = Me.StyleInfoTextNUMBER(spr, 7, False)

        Dim rowCnt As Integer = 0

        With spr

            .SuspendLayout()

            '設定する行数を設定
            rowCnt = .ActiveSheet.Rows.Count

            '行追加
            .ActiveSheet.AddRows(rowCnt, 1)

            'セルスタイル設定
            .SetCellStyle(rowCnt, LMI180G.sprDetails.DEF.ColNo, sCheck)
            .SetCellStyle(rowCnt, LMI180G.sprDetails.SERIALNO.ColNo, sNum7)
            .SetCellStyle(rowCnt, LMI180G.sprDetails.JOTAI.ColNo, sLabel)
            .SetCellStyle(rowCnt, LMI180G.sprDetails.NRCRECNO.ColNo, sLabel)
            .SetCellStyle(rowCnt, LMI180G.sprDetails.OUTKANOL.ColNo, sLabel)
            .SetCellStyle(rowCnt, LMI180G.sprDetails.EDANO.ColNo, sLabel)
            .SetCellStyle(rowCnt, LMI180G.sprDetails.TOROKUKB.ColNo, sLabel)
            .SetCellStyle(rowCnt, LMI180G.sprDetails.HOKOKUDATE.ColNo, sLabel)

            .ResumeLayout(True)

        End With

        'スクロールバーを一番下に設定
        Call Me.SetEndScroll(spr, True)

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal spr As LMSpread, ByVal ds As DataSet, ByVal clearFlg As Boolean)

        Dim max As Integer = ds.Tables(LMI180C.TABLE_NM_OUT).Rows.Count - 1

        With spr

            If clearFlg = True Then
                'スプレッドの行をクリア
                .CrearSpread()
            End If

            .SuspendLayout()

            '列設定
            Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sNum7 As StyleInfo = Me.StyleInfoTextNUMBER(spr, 7, False)

            Dim rowCnt As Integer = 0

            For i As Integer = 0 To max

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMI180G.sprDetails.DEF.ColNo, sCheck)
                '.SetCellStyle(rowCnt, LMI180G.sprDetails.SERIALNO.ColNo, sNum7)
                .SetCellStyle(rowCnt, LMI180G.sprDetails.SERIALNO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI180G.sprDetails.JOTAI.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI180G.sprDetails.NRCRECNO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI180G.sprDetails.OUTKANOL.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI180G.sprDetails.EDANO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI180G.sprDetails.TOROKUKB.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI180G.sprDetails.HOKOKUDATE.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(rowCnt, sprDetails.DEF.ColNo, "False")
                .SetCellValue(rowCnt, sprDetails.SERIALNO.ColNo, ds.Tables(LMI180C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString())
                .SetCellValue(rowCnt, sprDetails.JOTAI.ColNo, String.Empty)
                .SetCellValue(rowCnt, sprDetails.NRCRECNO.ColNo, ds.Tables(LMI180C.TABLE_NM_OUT).Rows(i).Item("NRC_REC_NO").ToString())
                .SetCellValue(rowCnt, sprDetails.OUTKANOL.ColNo, ds.Tables(LMI180C.TABLE_NM_OUT).Rows(i).Item("OUTKA_NO_L").ToString())
                .SetCellValue(rowCnt, sprDetails.EDANO.ColNo, ds.Tables(LMI180C.TABLE_NM_OUT).Rows(i).Item("EDA_NO").ToString())
                .SetCellValue(rowCnt, sprDetails.TOROKUKB.ColNo, ds.Tables(LMI180C.TABLE_NM_OUT).Rows(i).Item("TOROKU_KB").ToString())
                .SetCellValue(rowCnt, sprDetails.HOKOKUDATE.ColNo, ds.Tables(LMI180C.TABLE_NM_OUT).Rows(i).Item("HOKOKU_DATE").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの「状態」を設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadJotai(ByVal spr As LMSpread)

        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
        Dim jotai As String = String.Empty
        Dim nrcRecNo As String = String.Empty
        Dim serialNo As String = String.Empty
        Dim hokokuDate As String = String.Empty
        Dim torokuKb As String = String.Empty

        With spr

            .SuspendLayout()

            For i As Integer = 0 To max
                '入力チェック
                If Me.IsSingleJotaiCheck(spr, i) = False Then
                    .SetCellValue(i, sprDetails.TOROKUKB.ColNo, LMI180C.JOTAIKB_ERR)
                End If

                nrcRecNo = Me._LMIConG.GetCellValue(Me._Frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.NRCRECNO.ColNo)).ToString
                serialNo = Me._LMIConG.GetCellValue(Me._Frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.SERIALNO.ColNo)).ToString
                hokokuDate = Me._LMIConG.GetCellValue(Me._Frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.HOKOKUDATE.ColNo)).ToString
                torokuKb = Me._LMIConG.GetCellValue(Me._Frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.TOROKUKB.ColNo)).ToString

                If (LMI180C.JOTAIKB_ERR).Equals(torokuKb) Then
                    'エラーの場合
                    jotai = LMI180C.JOTAI_ERR
                    torokuKb = LMI180C.JOTAIKB_ERR
                ElseIf String.IsNullOrEmpty(nrcRecNo) = True Then
                    '該当なしの場合
                    jotai = LMI180C.JOTAI_GAITONASHI
                    torokuKb = LMI180C.JOTAIKB_GAITONASHI
                ElseIf String.IsNullOrEmpty(hokokuDate) = False Then
                    '重複の場合
                    jotai = LMI180C.JOTAI_JYUFUKU
                    torokuKb = LMI180C.JOTAIKB_JYUFUKU
                Else
                    jotai = LMI180C.JOTAI_SEIJO
                    torokuKb = LMI180C.JOTAIKB_SEIJO
                End If
                .SetCellValue(i, sprDetails.JOTAI.ColNo, jotai)
                .SetCellValue(i, sprDetails.TOROKUKB.ColNo, torokuKb)
            Next

            '要望番号:1944 取込時、エラー分は取り込まない(回収のみ) 2013/03/14 START
            If Me._Frm.optKaishu.Checked = True Then

                For i As Integer = 0 To max

                    If max < 0 OrElse i > max Then
                        Exit For
                    End If

                    Select Case Me._LMIConG.GetCellValue(Me._Frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.TOROKUKB.ColNo)).ToString

                        Case LMI180C.JOTAIKB_ERR, LMI180C.JOTAIKB_GAITONASHI, LMI180C.JOTAIKB_JYUFUKU

                            .ActiveSheet.RemoveRows(i, 1)
                            i = i - 1
                            max = max - 1

                        Case Else

                            Continue For

                    End Select

                Next

            End If
            '要望番号:1944 取込時、エラー分は取り込まない(回収のみ) 2013/03/14 END

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 単項目入力チェック(状態区分)（エラー）。
    ''' </summary>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleJotaiCheck(ByVal spr As LMSpread, ByVal rowCnt As Integer) As Boolean

        '【単項目チェック】
        With Me._Frm

            If (LMI180C.JOTAIKB_ERR).Equals(Me._LMIConG.GetCellValue(Me._Frm.sprDetails.ActiveSheet.Cells(rowCnt, LMI180G.sprDetails.TOROKUKB.ColNo)).ToString) = True Then
                Return False
            End If

            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
            Dim serialNo As String = String.Empty

            serialNo = Me._LMIConG.GetCellValue(Me._Frm.sprDetails.ActiveSheet.Cells(rowCnt, LMI180G.sprDetails.SERIALNO.ColNo)).ToString
            For i As Integer = 0 To max
                '重複チェック
                If i = rowCnt Then
                    Continue For
                End If

                If (serialNo).Equals(Me._LMIConG.GetCellValue(Me._Frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.SERIALNO.ColNo)).ToString) = True Then
                    '重複データあり
                    Return False
                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 一覧のスクロールバーを最終行に設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="setFlg">アクティブセルを設定するかのフラグ</param>
    ''' <remarks></remarks>
    Private Sub SetEndScroll(ByVal spr As Win.Spread.LMSpread, ByVal setFlg As Boolean)

        With spr

            Dim maxRow As Integer = .ActiveSheet.Rows.Count - 1
            If maxRow < 0 Then
                Exit Sub
            End If

            spr.SetViewportTopRow(0, maxRow)

            If setFlg = True Then

                spr.ActiveSheet.SetActiveCell(maxRow, 0)

            End If

        End With

    End Sub

#End Region

#Region "ユーティリティ"

#Region "ファイル取込(CSV)"

    ''' <summary>
    ''' ファイル取込(CSV)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Function GetFileDataCsv(ByVal frm As LMI180F) As DataSet

        '要望番号:1917 yamanaka 2013.03.06 Start
        Dim folderNm As String = System.IO.Path.GetDirectoryName(frm.txtPath.TextValue)
        Dim extension As String = System.IO.Path.GetExtension(frm.txtPath.TextValue)
        'Dim folderNm As String = frm.lblFolder.TextValue
        'Dim fileNm As String = frm.lblFile.TextValue
        '要望番号:1917 yamanaka 2013.03.06 End
        Dim ds As DataSet = New LMI180DS()
        Dim dr As DataRow = ds.Tables(LMI180C.TABLE_NM_IN).NewRow()

        '要望番号:1917 yamanaka 2013.03.06 Start
        Dim files As String() = Nothing

        If String.IsNullOrEmpty(extension) = True Then
            files = System.IO.Directory.GetFiles(folderNm, "*.txt")
        Else
            files = New String() {frm.txtPath.TextValue}
        End If
        'Dim files As String() = System.IO.Directory.GetFiles(folderNm, "*.txt")
        '要望番号:1917 yamanaka 2013.03.06 End

        Dim max As Integer = files.Count - 1

        For i As Integer = 0 To max
            'SHIFT_JIS形式で読み込む
            Using textParser As New TextFieldParser(files(i), System.Text.Encoding.GetEncoding("Shift_JIS"))
                'Using Reader As New IO.StreamReader(String.Concat(folderNm, fileNm), System.Text.Encoding.GetEncoding("Shift-JIS"))

                'CSVファイル 
                textParser.TextFieldType = FieldType.Delimited

                '区切り文字 
                textParser.SetDelimiters(",")

                'ファイルの終端までループ 
                While Not textParser.EndOfData
                    'While Not Reader.ReadToEnd
                    '1行読み込み 
                    'Dim row As String() = textParser.ReadFields()
                    Dim row As String() = textParser.ReadFields()

                    For Each col As String In row
                        dr = ds.Tables(LMI180C.TABLE_NM_IN).NewRow()

                        dr("NRC_REC_NO") = String.Empty
                        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                        dr("OUTKA_NO_L") = String.Empty
                        dr("EDA_NO") = String.Empty
                        dr("TOROKU_KB") = String.Empty
                        'dr("SERIAL_NO") = Mid(col, Len(col) - 6, 7)
                        dr("SERIAL_NO") = Mid(col, 6, 7)
                        dr("SERIAL_NO_FROM") = String.Empty
                        dr("SERIAL_NO_TO") = String.Empty
                        dr("HOKOKU_DATE") = String.Empty

                        'データセットに設定
                        ds.Tables(LMI180C.TABLE_NM_IN).Rows.Add(dr)
                    Next
                End While

            End Using
        Next

        Return ds

    End Function

#End Region

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
    ''' セルのプロパティを設定(TextHankaku)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextNUMBER(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUMBER, length, lock)

    End Function

#End Region

#End Region

#End Region

End Class
