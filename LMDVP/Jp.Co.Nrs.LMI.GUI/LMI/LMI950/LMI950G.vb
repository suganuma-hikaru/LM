' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI950G : 運賃データ確認（千葉日産物流）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMI950Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI950G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI950F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconG As LMFControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI950F, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMFconG = g

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
            .F1ButtonName = "実績作成"
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMFControlC.FUNCTION_KENSAKU
            .F10ButtonName = LMFControlC.FUNCTION_POP
            .F11ButtonName = String.Empty
            .F12ButtonName = LMFControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = always
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

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

            .pnlCondition.TabIndex = LMI950C.CtlTabIndex.CONDITION
            .cmbEigyo.TabIndex = LMI950C.CtlTabIndex.EIGYO
            .imdOutkaDate.TabIndex = LMI950C.CtlTabIndex.OUTKA_DATA
            .txtCustCdL.TabIndex = LMI950C.CtlTabIndex.CUSTCDL
            .lblCustNm.TabIndex = LMI950C.CtlTabIndex.CUSTNM
            .sprDetail.TabIndex = LMI950C.CtlTabIndex.DETAIL

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

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .Focus()
            .imdOutkaDate.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbEigyo.SelectedValue = Nothing
            .txtCustCdL.TextValue = String.Empty
            .lblCustNm.TextValue = String.Empty
            .imdOutkaDate.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        With Me._Frm

            '営業所の値を設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd
            Me._Frm.cmbEigyo.ReadOnly = True

            '初期荷主の値を設定
            Dim custCdL As String = "00145"
            .txtCustCdL.TextValue = custCdL

            Dim drsCust As DataRow() = Me._LMFconG.SelectCustListDataRow(brCd, custCdL)

            If 0 < drsCust.Length Then
                .lblCustNm.TextValue = drsCust(0).Item("CUST_NM_L").ToString()
            End If

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SHORI As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.SHORI, "処理", 40, True)
        Public Shared HORYU As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.HORYU, "保留", 40, True)
        Public Shared SEND As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.SEND, "送信", 40, True)
        Public Shared OUTKA_NO_L As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.OUTKA_NO_L, "出荷管理番号", 95, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.DEST_NM, "届先名", 380, True)
        Public Shared KYORI As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.KYORI, "距離", 90, True)
        Public Shared JURYO As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.JURYO, "重量", 90, True)
        Public Shared UNCHIN As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.UNCHIN, "運賃", 90, True)
        Public Shared HOUKOKU_UNCHIN As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.HOUKOKU_UNCHIN, "報告運賃", 90, False)
        Public Shared OUTKA_DATE As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.OUTKA_DATE, "出荷日", 80, True)
        Public Shared KOJO_KANRI_NO As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.KOJO_KANRI_NO, "工場管理番号", 130, True)

        Public Shared CRT_DATE As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.CRT_DATE, "CRT_DATE", 0, False)
        Public Shared FILE_NAME As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.FILE_NAME, "FILE_NAME", 0, False)
        Public Shared REC_NO As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.REC_NO, "REC_NO", 0, False)
        Public Shared UNSO_NO_L As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.UNSO_NO_L, "UNSO_NO_L", 0, False)
        Public Shared UNSO_L_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.UNSO_L_UPD_DATE, "UNSO_L_UPD_DATE", 0, False)
        Public Shared UNSO_L_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.UNSO_L_UPD_TIME, "UNSO_L_UPD_TIME", 0, False)
        Public Shared UNCHIN_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.UNCHIN_UPD_DATE, "UNCHIN_UPD_DATE", 0, False)
        Public Shared UNCHIN_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI950C.SprColumnIndex.UNCHIN_UPD_TIME, "UNCHIN_UPD_TIME", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpread = Me._Frm.sprDetail
        With spr

            'スプレッドの行をクリア
            .CrearSpread()
            .ActiveSheet.Rows.Count = 0

            '列数設定
            .ActiveSheet.ColumnCount = LMI950C.SprColumnIndex.LAST

            .SetColProperty(New LMI950G.sprDetailDef(), False)

            '列固定位置を設定
            .ActiveSheet.FrozenColumnCount = LMI950G.sprDetailDef.OUTKA_NO_L.ColNo + 1

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの文字色設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadColor()

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim sht As FarPoint.Win.Spread.SheetView = spr.ActiveSheet
        Dim max As Integer = sht.Rows.Count - 1

        For i As Integer = 0 To max

            If Me._LMFconG.GetCellValue(sht.Cells(i, LMI950G.sprDetailDef.HORYU.ColNo)) = "保留" Then
                '保留の場合
                sht.Rows(i).ForeColor = Color.Blue
            End If

            If CDec(Me._LMFconG.GetCellValue(sht.Cells(i, LMI950G.sprDetailDef.HOUKOKU_UNCHIN.ColNo))) = CDec(0) Then
                '報告運賃0円の場合
                sht.Cells(i, LMI950G.sprDetailDef.HOUKOKU_UNCHIN.ColNo).ForeColor = Color.Red
            End If

        Next


    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetSpread(ByVal ds As DataSet) As Boolean

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim dt As DataTable = ds.Tables(LMI950C.TABLE_NM_OUT)

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()
            .ActiveSheet.Rows.Count = 0

            .SuspendLayout()

            '----データ挿入----'
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Return True
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = Me.StyleInfoChk(spr)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sNum5 As StyleInfo = Me.StyleInfoNum5(spr)
            sNum5.HorizontalAlignment = CellHorizontalAlignment.Right
            Dim sNum12 As StyleInfo = Me.StyleInfoNum12(spr)
            sNum12.HorizontalAlignment = CellHorizontalAlignment.Right
            Dim sNum12dec3 As StyleInfo = Me.StyleInfoNum12dec3(spr)
            sNum12dec3.HorizontalAlignment = CellHorizontalAlignment.Right

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dr = dt.Rows(i)

                'セルスタイル設定
                .SetCellStyle(i, LMI950G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMI950G.sprDetailDef.SHORI.ColNo, sLabel)
                .SetCellStyle(i, LMI950G.sprDetailDef.HORYU.ColNo, sLabel)
                .SetCellStyle(i, LMI950G.sprDetailDef.SEND.ColNo, sLabel)
                .SetCellStyle(i, LMI950G.sprDetailDef.OUTKA_NO_L.ColNo, sLabel)
                .SetCellStyle(i, LMI950G.sprDetailDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI950G.sprDetailDef.KYORI.ColNo, sNum5)
                .SetCellStyle(i, LMI950G.sprDetailDef.JURYO.ColNo, sNum12dec3)
                .SetCellStyle(i, LMI950G.sprDetailDef.UNCHIN.ColNo, sNum12)
                .SetCellStyle(i, LMI950G.sprDetailDef.HOUKOKU_UNCHIN.ColNo, sNum12)
                .SetCellStyle(i, LMI950G.sprDetailDef.OUTKA_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI950G.sprDetailDef.KOJO_KANRI_NO.ColNo, sLabel)

                .SetCellStyle(i, LMI950G.sprDetailDef.UNSO_L_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI950G.sprDetailDef.UNSO_L_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMI950G.sprDetailDef.UNCHIN_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI950G.sprDetailDef.UNCHIN_UPD_TIME.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMI950G.sprDetailDef.DEF.ColNo, False.ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.SHORI.ColNo, dr.Item("SHORI_KB").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.HORYU.ColNo, dr.Item("HORYU").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.SEND.ColNo, dr.Item("SEND").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.OUTKA_NO_L.ColNo, dr.Item("OUTKA_NO_L").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.KYORI.ColNo, dr.Item("DECI_KYORI").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.JURYO.ColNo, dr.Item("DECI_WT").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.UNCHIN.ColNo, dr.Item("DECI_UNCHIN").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.HOUKOKU_UNCHIN.ColNo, dr.Item("HOUKOKU_UNCHIN").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.OUTKA_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SHUKKA_DATE").ToString()))
                .SetCellValue(i, LMI950G.sprDetailDef.KOJO_KANRI_NO.ColNo, dr.Item("KOJO_KANRI_NO").ToString())

                .SetCellValue(i, LMI950G.sprDetailDef.CRT_DATE.ColNo, dr.Item("CRT_DATE").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.FILE_NAME.ColNo, dr.Item("FILE_NAME").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.REC_NO.ColNo, dr.Item("REC_NO").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.UNSO_NO_L.ColNo, dr.Item("UNSO_NO_L").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.UNSO_L_UPD_DATE.ColNo, dr.Item("UNSO_L_UPD_DATE").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.UNSO_L_UPD_TIME.ColNo, dr.Item("UNSO_L_UPD_TIME").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.UNCHIN_UPD_DATE.ColNo, dr.Item("UNCHIN_UPD_DATE").ToString())
                .SetCellValue(i, LMI950G.sprDetailDef.UNCHIN_UPD_TIME.ColNo, dr.Item("UNCHIN_UPD_TIME").ToString())

            Next

            .ResumeLayout(True)

            Return True

        End With

    End Function

    ''' <summary>
    ''' スプレッドのデータを更新
    ''' </summary>
    ''' <param name="frm">frm</param>
    ''' <param name="arr">arr</param>
    ''' <remarks></remarks>
    Friend Function SetUpdSpread(ByVal frm As LMI950F, ByVal arr As ArrayList) As Boolean

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim max As Integer = arr.Count - 1
        Dim rowNo As Integer = 0
        With spr

            .SuspendLayout()

            For i As Integer = 0 To max

                rowNo = Convert.ToInt32(arr(i))

                'セルに値を設定(送信)
                .SetCellValue(rowNo, LMI950G.sprDetailDef.SEND.ColNo, "済")

            Next

            .ResumeLayout(True)

            Return True

        End With

    End Function

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
    ''' セルのプロパティを設定(Number 整数12桁 小数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum12dec3(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, True, 3, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数12桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum12(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999999, True, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数5桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum5(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 99999, True, 0, , ",")

    End Function

#End Region

#End Region

#End Region

End Class
