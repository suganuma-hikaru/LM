' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMH     : EDIサブシステム
'  プログラムID   : LMH080G : EDI出荷データ検索
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

''' <summary>
''' LMH080Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH080G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH080F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconG As LMHControlG

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconV As LMHControlV

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH080F)

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
            .F4ButtonName = "削　除"
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = always
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = always
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
            .F12ButtonEnabled = always

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
            .imdEdiDateFrom.TabIndex = LMH080C.CtlTabIndex_MAIN.EDIDATEFROM
            .imdEdiDateTo.TabIndex = LMH080C.CtlTabIndex_MAIN.EDIDATETO
            .grpSTATUS.TabIndex = LMH080C.CtlTabIndex_MAIN.GRPSTATUS
            .sprEdiList.TabIndex = LMH080C.CtlTabIndex_MAIN.SPREDILIST

            'GroupBox chkSTA
            .optMikakunin.TabIndex = LMH080C.CtlTabIndex_optSTA.OPTMIKKUNIN
            .optKakuninZumi.TabIndex = LMH080C.CtlTabIndex_optSTA.OPTKAKUNINZUMI
            .optSoshinZumi.TabIndex = LMH080C.CtlTabIndex_optSTA.OPTSOSHINZUMI
            .optSakujoZumi.TabIndex = LMH080C.CtlTabIndex_optSTA.OPTSAKUJOZUMI
            .optAll.TabIndex = LMH080C.CtlTabIndex_optSTA.OPTALL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByRef frm As LMH080F, ByVal sysdate As String, ByVal prmDs As DataSet)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コントロールに初期値設定
        Call Me.SetInitControl(id, frm, sysdate, prmDs)

    End Sub

#Region "コントロール初期化(共通)"

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .txtCustCD_M.TextValue = String.Empty

        End With

    End Sub

#End Region

    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMH080F, ByVal sysdate As String, ByVal prmDs As DataSet)

        With Me._Frm
            '初期値が存在するコントロール
            .optMikakunin.Checked() = True
            .optKakuninZumi.Checked() = False
            .optSoshinZumi.Checked() = False
            .optSakujoZumi.Checked() = False
            .optAll.Checked() = False
            .cmbEigyo.SelectedValue() = prmDs.Tables(LMH080C.TABLE_NM_IN).Rows(0)("NRS_BR_CD").ToString()     '営業所
            .txtCustCD_L.TextValue = prmDs.Tables(LMH080C.TABLE_NM_IN).Rows(0)("CUST_CD_L").ToString()        '荷主コード（大）
            .txtCustCD_M.TextValue = prmDs.Tables(LMH080C.TABLE_NM_IN).Rows(0)("CUST_CD_M").ToString()        '荷主コード（中）
            If String.IsNullOrEmpty(String.Concat(.txtCustCD_L.TextValue, .txtCustCD_M.TextValue)) = False Then
                .lblCustNM.TextValue = Me.GetCachedCust(.txtCustCD_L.TextValue, .txtCustCD_M.TextValue, "00", "00")
            End If
            .imdEdiDateFrom.TextValue = sysdate
            .imdEdiDateTo.TextValue = sysdate
        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey(LMH080C.MODE_DEFAULT)
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = LMH080C.MODE_DEFAULT)

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
            .grpSTATUS.Focus()
        End With

    End Sub

    ''' <summary>
    ''' 荷主キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedCust(ByVal custCdL As String, _
                                   ByVal custCdM As String, _
                                   ByVal custCdS As String, _
                                   ByVal custCdSS As String) As String

        Dim dr As DataRow() = Nothing

        '荷主名称
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                           "CUST_CD_L = '", custCdL, "' AND " _
                                                                         , "CUST_CD_M = '", custCdM, "' AND " _
                                                                         , "CUST_CD_S = '", custCdS, "' AND " _
                                                                         , "CUST_CD_SS = '", custCdSS, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
        If 0 < dr.Length Then
            Return String.Concat(dr(0).Item("CUST_NM_L").ToString, " ", dr(0).Item("CUST_NM_M").ToString)
        End If

        Return String.Empty

    End Function


#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataTable)

        'スプレッドに明細データ設定
        Call Me.SetSpread(ds)

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprEdiListDef
        'スプレッド(タイトル列)の設定

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared DELIV_NO As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.DELIV_NO, "Delivery No.", 100, True)
        Public Shared GOODS_CD As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.GOODS_CD, "商品コード", 100, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.GOODS_NM, "商品名", 150, True)
        Public Shared QT As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.QT, "数量", 80, True)
        Public Shared NET_WT As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.NET_WT, "重量", 80, True)
        Public Shared QT_UT As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.QT_UT, "単位", 60, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.LOT_NO, "ロットNo.", 100, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.DEST_NM, "届先名", 150, True)
        Public Shared OUTER_PKG As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.OUTER_PKG, "外装個数", 80, False)
        Public Shared PKG_UT As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.PKG_UT, "荷姿", 60, True)
        Public Shared INKA_DATE_YMD As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.INKA_DATE_YMD, "入荷日", 80, True)
        Public Shared INKA_NB As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.INKA_NB, "入荷個数", 80, True)
        Public Shared REPLY_DATE As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.REPLY_DATE, "返信日", 80, True)
        Public Shared INKA_CTL_NO_L As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.INKA_CTL_NO_L, "入荷管理番号(大)", 110, True)
        Public Shared CRT_DATE As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.CRT_DATE, "データ取込日", 80, True)
        Public Shared FILE_NAME As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.FILE_NAME, "ファイル名", 200, True)
        Public Shared DATA_SEQ As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.DATA_SEQ, "ファイル行数", 100, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.CUST_CD_L, "荷主コード(大)", 100, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.CUST_CD_M, "荷主コード(中)", 100, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.NRS_BR_CD, "営業所コード", 100, False)
        Public Shared DEL_KB As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.DEL_KB, "削除区分", 100, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.SYS_UPD_DATE, "更新日", 100, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH080C.SprColumnIndex.SYS_UPD_TIME, "更新時間", 100, False)


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
            .sprEdiList.Sheets(0).ColumnCount = 24

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprEdiList.SetColProperty(New sprEdiListDef)

            '列固定位置を設定します。
            .sprEdiList.Sheets(0).FrozenColumnCount = sprEdiListDef.GOODS_NM.ColNo + 1

            '列設定
            .sprEdiList.SetCellStyle(0, sprEdiListDef.DELIV_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 10, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.GOODS_CD.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 20, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 60, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.QT.ColNo, LMSpreadUtility.GetNumberCell(.sprEdiList, 0, 99999999, True, 0))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.NET_WT.ColNo, LMSpreadUtility.GetNumberCell(.sprEdiList, 0, 99999999, True, 3))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.QT_UT.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 10, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 105, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUTER_PKG.ColNo, LMSpreadUtility.GetNumberCell(.sprEdiList, 0, 99999999, True, 0))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.PKG_UT.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.INKA_DATE_YMD.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.INKA_NB.ColNo, LMSpreadUtility.GetNumberCell(.sprEdiList, 0, 99999999, True, 0))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.REPLY_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.INKA_CTL_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 9, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.CRT_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.FILE_NAME.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 300, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.DATA_SEQ.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 5, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.CUST_CD_M.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 2, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 2, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.DEL_KB.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 8, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 9, True))

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMH080F)

        With frm.sprEdiList

            .Sheets(0).Cells(0, sprEdiListDef.DEF.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.GOODS_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.GOODS_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.QT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.NET_WT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.QT_UT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.LOT_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.DEST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.OUTER_PKG.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.PKG_UT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.INKA_DATE_YMD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.INKA_NB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.REPLY_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.INKA_CTL_NO_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.CRT_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.FILE_NAME.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.DATA_SEQ.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.CUST_CD_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.CUST_CD_M.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.NRS_BR_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.DEL_KB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.SYS_UPD_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.SYS_UPD_TIME.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprEdiList

        With spr

            .SuspendLayout()

            '----データ挿入----'

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count()
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, True, ",")

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定

                ''*****表示列*****
                .SetCellStyle(i, sprEdiListDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprEdiListDef.DELIV_NO.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.GOODS_CD.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.QT.ColNo, sNum)
                .SetCellStyle(i, sprEdiListDef.NET_WT.ColNo, sNum)
                .SetCellStyle(i, sprEdiListDef.QT_UT.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.LOT_NO.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.OUTER_PKG.ColNo, sNum)
                .SetCellStyle(i, sprEdiListDef.PKG_UT.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.INKA_DATE_YMD.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.INKA_NB.ColNo, sNum)
                .SetCellStyle(i, sprEdiListDef.REPLY_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.INKA_CTL_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.CRT_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.FILE_NAME.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.DATA_SEQ.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.CUST_CD_M.ColNo, sLabel)
                ' ''*****隠し列*****
                .SetCellStyle(i, sprEdiListDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.DEL_KB.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.SYS_UPD_TIME.ColNo, sLabel)

                  ''セルに値を設定

                ''*****表示列*****
                .SetCellValue(i, sprEdiListDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprEdiListDef.DELIV_NO.ColNo, dr.Item("H4_DELIVERY_NO").ToString())
                .SetCellValue(i, sprEdiListDef.GOODS_CD.ColNo, dr.Item("L2_ITEM_CODE").ToString())
                .SetCellValue(i, sprEdiListDef.GOODS_NM.ColNo, dr.Item("L2_NAME_INTERNAL").ToString())
                .SetCellValue(i, sprEdiListDef.QT.ColNo, dr.Item("L2_QUANTITY").ToString())
                .SetCellValue(i, sprEdiListDef.NET_WT.ColNo, dr.Item("L2_GROSS").ToString())
                .SetCellValue(i, sprEdiListDef.QT_UT.ColNo, dr.Item("L2_UOM").ToString())
                .SetCellValue(i, sprEdiListDef.LOT_NO.ColNo, dr.Item("L2_BATCH_NO").ToString())
                .SetCellValue(i, sprEdiListDef.DEST_NM.ColNo, dr.Item("H3_NAME_ALL").ToString())
                .SetCellValue(i, sprEdiListDef.OUTER_PKG.ColNo, dr.Item("OUTER_PKG").ToString())
                .SetCellValue(i, sprEdiListDef.PKG_UT.ColNo, dr.Item("L2_QUANTITY_UOM").ToString())
                .SetCellValue(i, sprEdiListDef.INKA_DATE_YMD.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("INKA_DATE").ToString()))
                .SetCellValue(i, sprEdiListDef.INKA_NB.ColNo, dr.Item("INKA_NB").ToString())
                .SetCellValue(i, sprEdiListDef.REPLY_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("MISOUCYAKU_DATE").ToString()))
                .SetCellValue(i, sprEdiListDef.INKA_CTL_NO_L.ColNo, dr.Item("INKA_CTL_NO_L").ToString())
                .SetCellValue(i, sprEdiListDef.CRT_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("CRT_DATE").ToString()))
                .SetCellValue(i, sprEdiListDef.FILE_NAME.ColNo, dr.Item("FILE_NAME").ToString())
                .SetCellValue(i, sprEdiListDef.DATA_SEQ.ColNo, dr.Item("REC_NO").ToString())
                .SetCellValue(i, sprEdiListDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprEdiListDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                ' ''*****隠し列*****
                .SetCellValue(i, sprEdiListDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprEdiListDef.DEL_KB.ColNo, dr.Item("DEL_KB").ToString())
                .SetCellValue(i, sprEdiListDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprEdiListDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#Region "コントロール取得"

    ''' <summary>
    ''' フォームに検索した結果(Text)を取得
    ''' </summary>
    ''' <param name="objNm">コントロール名</param>
    ''' <returns>LMImTextBox</returns>
    ''' <remarks></remarks>
    Friend Function GetTextControl(ByVal objNm As String) As Win.InputMan.LMImTextBox

        Return DirectCast(Me._Frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox)

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function
#End Region

#End Region

End Class
