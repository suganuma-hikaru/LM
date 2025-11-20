' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMD     : 在庫サブシステム
'  プログラムID   : LMD060G : 月末在庫履歴作成
'  作  成  者     : 
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMD060Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD060G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD060F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconG As LMDControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD060F, ByVal g As LMDControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMDconG = g

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
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = LMDControlC.FUNCTION_DELETE
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMDControlC.FUNCTION_KENSAKU
            .F10ButtonName = LMDControlC.FUNCTION_POP
            .F11ButtonName = LMDControlC.FUNCTION_JIKKOUE
            .F12ButtonName = LMDControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = always
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
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

            'control
            '.cmbEigyo.TabIndex = LMD060C.CtlTabIndex.EIGYO
            .txtTantouCd.TabIndex = LMD060C.CtlTabIndex.TANTOU_CD
            .txtCustCdL.TabIndex = LMD060C.CtlTabIndex.CUST_CDL
            .txtCustCdM.TabIndex = LMD060C.CtlTabIndex.CUST_CDM
            .cmbSimebi.TabIndex = LMD060C.CtlTabIndex.SIMEBI_KB
            .imdZaiRirekiDate.TabIndex = LMD060C.CtlTabIndex.ZAIRIREKIDATE
            .sprCreate.TabIndex = LMD060C.CtlTabIndex.SPR

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '項目をクリア
        Call Me.ClearControl()

        '初期値設定
        Call Me.SetInitData()

    End Sub

    ''' <summary>
    ''' 初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInitData()

        With Me._Frm

            '営業所コンボに自営業所を設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As String = "")

        With Me._Frm

            'フォーカス位置初期化
            .Focus()
            .txtTantouCd.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .txtTantouCd.TextValue = String.Empty
            .lblTantouNM.TextValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNm.TextValue = String.Empty
            .cmbSimebi.SelectedValue = Nothing
            .imdZaiRirekiDate.TextValue = String.Empty

        End With

    End Sub

#End Region '設定・制御

#End Region 'Form

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(固定)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprCreateDef

        'スプレッド(タイトル列)の設定

        'visible
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMD060C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared CUST_CD As SpreadColProperty = New SpreadColProperty(LMD060C.SprColumnIndex.CUST_CD, "荷主コード", 100, True)
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMD060C.SprColumnIndex.CUST_NM, "荷主名", 300, True)
        Public Shared CLOSE_KB_NM As SpreadColProperty = New SpreadColProperty(LMD060C.SprColumnIndex.CLOSE_KB_NM, "締日区分", 100, True)
        Public Shared CULC_DATE As SpreadColProperty = New SpreadColProperty(LMD060C.SprColumnIndex.CULC_DATE, "最終計算日", 100, True)
        Public Shared TANTO_NM As SpreadColProperty = New SpreadColProperty(LMD060C.SprColumnIndex.TANTO_NM, "担当者", 150, True)

        'invisible
        Public Shared RIREKI_1 As SpreadColProperty = New SpreadColProperty(LMD060C.SprColumnIndex.RIREKI_1, "履歴日１", 100, False)
        Public Shared ZAI_REC_NO_1 As SpreadColProperty = New SpreadColProperty(LMD060C.SprColumnIndex.ZAI_REC_NO_1, "在庫レコード番号", 0, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMD060C.SprColumnIndex.CUST_CD_L, "荷主コード（大）", 0, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMD060C.SprColumnIndex.CUST_CD_M, "荷主コード（中）", 0, False)
        Public Shared CLOSE_KB As SpreadColProperty = New SpreadColProperty(LMD060C.SprColumnIndex.CLOSE_KB, "締日区分", 0, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMD060C.SprColumnIndex.SYS_UPD_DATE, "更新日", 0, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMD060C.SprColumnIndex.SYS_UPD_TIME, "更新時間", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpreadSearch = Me._Frm.sprCreate

        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .Sheets(0).ColumnCount = LMD060C.SprColumnIndex.LAST

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprCreateDef)
            .SetColProperty(New sprCreateDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.ユーザー名で固定)
            .Sheets(0).FrozenColumnCount = sprCreateDef.RIREKI_1.ColNo + 1

            '列設定
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sDate As StyleInfo = Me.StyleInfoLabelDate(spr)

            .SetCellStyle(0, sprCreateDef.CUST_CD.ColNo, sLabel)
            .SetCellStyle(0, sprCreateDef.CUST_NM.ColNo, Me.StyleInfoTextMix(spr, 122))
            .SetCellStyle(0, sprCreateDef.CLOSE_KB_NM.ColNo, sLabel)
            .SetCellStyle(0, sprCreateDef.CULC_DATE.ColNo, sDate)
            .SetCellStyle(0, sprCreateDef.TANTO_NM.ColNo, Me.StyleInfoTextMix(spr, 20))
            .SetCellStyle(0, sprCreateDef.RIREKI_1.ColNo, sLabel)
            .SetCellStyle(0, sprCreateDef.ZAI_REC_NO_1.ColNo, sLabel)
            .SetCellStyle(0, sprCreateDef.CLOSE_KB.ColNo, sLabel)
            .SetCellStyle(0, sprCreateDef.CUST_CD_L.ColNo, sLabel)
            .SetCellStyle(0, sprCreateDef.CUST_CD_M.ColNo, sLabel)
            .SetCellStyle(0, sprCreateDef.SYS_UPD_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprCreateDef.SYS_UPD_TIME.ColNo, sLabel)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        Dim spr As LMSpread = Me._Frm.sprCreate
        Dim max As Integer = spr.ActiveSheet.Columns.Count - 1

        With spr

            For i As Integer = 1 To max
                .SetCellValue(0, i, String.Empty)
            Next

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        Dim spr As LMSpreadSearch = Me._Frm.sprCreate
        Dim dt As DataTable = ds.Tables(LMD060C.TABLE_NM_OUT)

        '追加列数
        Dim addcnt As Integer = Convert.ToInt32(dt.Rows(0).Item("REC_CNT")) - 1

        With spr

            .SuspendLayout()

            '----データ挿入----'
            '列再設定
            Call Me.sprCreateKahen(addcnt, LMD060C.SprColumnIndex.LAST)

            '行数設定
            Dim lngcnt As Integer = Convert.ToInt32(dt.Rows(0).Item("ROW_CNT"))

            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sDate As StyleInfo = Me.StyleInfoLabelDate(spr)

            '読取用DataRow設定
            Dim dr As DataRow = Nothing       'メインデータテーブル用(dt)

            '格納変数初期化
            Dim sprRow As Integer = 0     'スプレッド出力行番号
            Dim nowCol As Integer = 0     '現カラム番号格納変数
            Dim tmprireki As String = String.Empty

            '値設定
            For i As Integer = 1 To dt.Rows.Count()

                dr = dt.Rows(i - 1)

                '荷主コードが変わった場合
                If i = 1 OrElse (i > 1 AndAlso (dt.Rows(i - 2).Item("CUST_CD").Equals(dr.Item("CUST_CD")) = False)) Then

                    For j As Integer = nowCol To .ActiveSheet.ColumnCount() - 1
                        .SetCellStyle(sprRow, j, sLabel)
                    Next

                    sprRow = sprRow + 1                            '編集行 + 1 
                    nowCol = LMD060C.SprColumnIndex.LAST - 1

                    'セルスタイル設定（固定列）
                    .SetCellStyle(sprRow, LMD060G.sprCreateDef.DEF.ColNo, sDEF)
                    .SetCellStyle(sprRow, LMD060G.sprCreateDef.CUST_CD.ColNo, sLabel)
                    .SetCellStyle(sprRow, LMD060G.sprCreateDef.CUST_NM.ColNo, sLabel)
                    .SetCellStyle(sprRow, LMD060G.sprCreateDef.CLOSE_KB_NM.ColNo, sLabel)
                    .SetCellStyle(sprRow, LMD060G.sprCreateDef.CULC_DATE.ColNo, sDate)
                    .SetCellStyle(sprRow, LMD060G.sprCreateDef.TANTO_NM.ColNo, sLabel)
                    .SetCellStyle(sprRow, LMD060G.sprCreateDef.RIREKI_1.ColNo, sLabel)
                    .SetCellStyle(sprRow, LMD060G.sprCreateDef.ZAI_REC_NO_1.ColNo, sLabel)
                    .SetCellStyle(sprRow, LMD060G.sprCreateDef.CUST_CD_L.ColNo, sLabel)
                    .SetCellStyle(sprRow, LMD060G.sprCreateDef.CUST_CD_M.ColNo, sLabel)
                    .SetCellStyle(sprRow, LMD060G.sprCreateDef.CLOSE_KB.ColNo, sLabel)
                    .SetCellStyle(sprRow, LMD060G.sprCreateDef.SYS_UPD_DATE.ColNo, sLabel)
                    .SetCellStyle(sprRow, LMD060G.sprCreateDef.SYS_UPD_TIME.ColNo, sLabel)

                    For k As Integer = LMD060C.SprColumnIndex.LAST To .ActiveSheet.ColumnCount() - 1
                        .SetCellStyle(sprRow, k, sLabel)
                    Next

                    'セルに値を設定（固定列）
                    .SetCellValue(sprRow, LMD060G.sprCreateDef.CUST_CD.ColNo, dr.Item("CUST_CD").ToString())
                    .SetCellValue(sprRow, LMD060G.sprCreateDef.CUST_NM.ColNo, dr.Item("CUST_NM").ToString())
                    .SetCellValue(sprRow, LMD060G.sprCreateDef.CLOSE_KB_NM.ColNo, dr.Item("CLOSE_KB_NM").ToString())
                    .SetCellValue(sprRow, LMD060G.sprCreateDef.CULC_DATE.ColNo, Me.changeSlashDate(dr.Item("CULC_DATE").ToString()))
                    .SetCellValue(sprRow, LMD060G.sprCreateDef.TANTO_NM.ColNo, dr.Item("TANTO_NM").ToString())
                    .SetCellValue(sprRow, LMD060G.sprCreateDef.RIREKI_1.ColNo, Me.changeSlashDate(dr.Item("RIREKI_DATE").ToString()))
                    .SetCellValue(sprRow, LMD060G.sprCreateDef.ZAI_REC_NO_1.ColNo, dr.Item("ZAI_REC_NO").ToString())
                    .SetCellValue(sprRow, LMD060G.sprCreateDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                    .SetCellValue(sprRow, LMD060G.sprCreateDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                    .SetCellValue(sprRow, LMD060G.sprCreateDef.CLOSE_KB.ColNo, dr.Item("CLOSE_KB").ToString())
                    .SetCellValue(sprRow, LMD060G.sprCreateDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                    .SetCellValue(sprRow, LMD060G.sprCreateDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())

                Else

                    '可変項目（履歴日）設定
                    tmprireki = dr.Item("RIREKI_DATE").ToString()
                    If String.IsNullOrEmpty(tmprireki) = False Then
                        nowCol = nowCol + 1
                        .SetCellStyle(sprRow, nowCol, sLabel)
                        .SetCellValue(sprRow, nowCol, Me.changeSlashDate(dr.Item("RIREKI_DATE").ToString()))
                    End If

                End If

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッド列再設定(可変列)
    ''' </summary>
    ''' <param name="newcnt">追加列（履歴日）数</param>
    ''' <param name="colcnt">固定列</param>
    ''' <remarks>可変列名設定</remarks>
    Friend Sub sprCreateKahen(ByVal newcnt As Integer, ByVal colcnt As Integer)

        Call Me.InitSpread()
        Dim colnum As Integer = colcnt - 1

        'セルに設定するスタイルの取得
        Dim sLabel As StyleInfo = Me.StyleInfoLabel(Me._Frm.sprCreate)

        With Me._Frm.sprCreate

            '追加列数が0以上の場合（履歴日を持つデータが存在する）、履歴日1を表示する
            If newcnt >= 0 Then
                '総列数再設定
                .Sheets(0).ColumnCount = colcnt + newcnt
                .Sheets(0).ColumnHeader.Columns(LMD060C.SprColumnIndex.RIREKI_1).Visible = True
            End If

            '列名、列幅、セル設定
            For i As Integer = 1 To newcnt

                colnum = colnum + 1
                .Sheets(0).ColumnHeader.Cells(0, colnum).Text = "履歴日" & StrConv((i + 1).ToString(), VbStrConv.Wide)
                .Sheets(0).ColumnHeader.Columns(colnum).Width = 100
                .SetCellStyle(0, colnum, sLabel)

            Next

            .Sheets(0).ColumnHeader.DefaultStyle.Font = .Sheets(0).ColumnHeader.Columns(0).Font

        End With

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
    ''' セルのプロパティを設定(Label)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabel(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(MIX)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextMix(ByVal spr As LMSpreadSearch, ByVal length As Integer) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, length, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(日付ラベル)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabelDate(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetDateTimeCell(spr, True)

    End Function

#End Region 'プロパティ

#Region "その他"

    ''' <summary>
    ''' 履歴日をスラッシュ編集する
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function changeSlashDate(ByVal str As String) As String

        Dim tmp As Integer = 0
        If Integer.TryParse(str, tmp) Then
            str = tmp.ToString("0000/00/00")
        Else
            str = ""
        End If

        Return str

    End Function

#End Region 'その他

#End Region 'ユーティリティ

#End Region

End Class
