' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMF     : 運賃
'  プログラムID   : LMF070G : 運賃試算比較
'  作  成  者     : yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李
Imports Jp.Co.Nrs.Win.Base   '2017/09/25 追加 李

''' <summary>
''' LMF070Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF070G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF070F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconG As LMFControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF070F, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Gamen共通クラスの設定
        Me._LMFconG = g

    End Sub

#End Region

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
            .F9ButtonName = "検　索"
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = String.Empty
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
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = False
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .sprUnchin.TabIndex = LMF070C.CtlTabIndex.DETAIL
            .cmbNrsBrCd.TabIndex = LMF070C.CtlTabIndex.NrsBrCd
            .imdOutakaDate_From.TabIndex = LMF070C.CtlTabIndex.OutakaDate_From
            .imdOutakaDate_To.TabIndex = LMF070C.CtlTabIndex.OutakaDate_To
            .txtCustCdL.TabIndex = LMF070C.CtlTabIndex.CustCdL
            .txtCustCdM.TabIndex = LMF070C.CtlTabIndex.CustCdM
            .lblCustNm.TabIndex = LMF070C.CtlTabIndex.CustNm
            .txtOldTariffCd.TabIndex = LMF070C.CtlTabIndex.OldTariffCd
            .lblOldTariffNm.TabIndex = LMF070C.CtlTabIndex.OldTariffNm
            .txtOldETariffCd.TabIndex = LMF070C.CtlTabIndex.OldETariffCd
            .lblOldETariffNm.TabIndex = LMF070C.CtlTabIndex.OldETariffNm
            .txtUnsoCd.TabIndex = LMF070C.CtlTabIndex.TitleUnsoCoNM
            .txtUnsoBrCd.TabIndex = LMF070C.CtlTabIndex.UnsoCd
            .lblUnsoNm.TabIndex = LMF070C.CtlTabIndex.UnsoNm2
            .txtSeiqtoCd.TabIndex = LMF070C.CtlTabIndex.SeiqtoCd
            .lblSeiqtoNm.TabIndex = LMF070C.CtlTabIndex.SeiqtoNm
            .cmbSoko.TabIndex = LMF070C.CtlTabIndex.CmbSoko
            .txtKyoriteiCd.TabIndex = LMF070C.CtlTabIndex.NewKyoriCd
            .txtNewTariffCd.TabIndex = LMF070C.CtlTabIndex.NewTariffCd
            .lblNewTariffNm.TabIndex = LMF070C.CtlTabIndex.NewTariffNm
            .txtNewETariffCd.TabIndex = LMF070C.CtlTabIndex.ETariffCd
            .lblNewETariffNm.TabIndex = LMF070C.CtlTabIndex.ETariffNm
            .numOldTariffSum.TabIndex = LMF070C.CtlTabIndex.OldTariffSum
            .numOldETariffSum.TabIndex = LMF070C.CtlTabIndex.OldETariffSum
            .numNewTariffSum.TabIndex = LMF070C.CtlTabIndex.NewTariffSum
            .numNewETariffSum.TabIndex = LMF070C.CtlTabIndex.NewETariffSum

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysData As String)

        '数値コントロールの書式設定
        Call Me.SetNumberControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        '初期値設定
        Call Me.SetInput(sysData)

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            'フォーカス位置の初期化
            .imdOutakaDate_From.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = Nothing
            .imdOutakaDate_From.TextValue = String.Empty
            .imdOutakaDate_To.TextValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNm.TextValue = String.Empty
            .txtOldTariffCd.TextValue = String.Empty
            .lblOldTariffNm.TextValue = String.Empty
            .txtOldETariffCd.TextValue = String.Empty
            .lblOldETariffNm.TextValue = String.Empty
            .txtUnsoCd.TextValue = String.Empty
            .txtUnsoBrCd.TextValue = String.Empty
            .lblUnsoNm.TextValue = String.Empty
            .txtSeiqtoCd.TextValue = String.Empty
            .lblSeiqtoNm.TextValue = String.Empty
            .cmbSoko.SelectedValue = String.Empty
            .txtKyoriteiCd.TextValue = String.Empty
            .txtNewTariffCd.TextValue = String.Empty
            .lblNewTariffNm.TextValue = String.Empty
            .txtNewETariffCd.TextValue = String.Empty
            .lblNewETariffNm.TextValue = String.Empty
            .numOldTariffSum.Value = 0
            .numOldETariffSum.Value = 0
            .numNewTariffSum.Value = 0
            .numNewETariffSum.Value = 0

        End With

    End Sub

    ''' <summary>
    '''営業所、日付の当月の1日目、当月の最終日の設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetInput(ByVal sysDate As String)

        '自営業所
        Me._Frm.cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()

        '当月日付1日目の取得
        Dim nowDate As String = String.Concat(Convert.ToDateTime(DateFormatUtility.EditSlash(sysDate)).ToString(LMFControlC.DATE_YYYYMM), LMFControlC.FLG_ON)

        Dim nextDate As DateTime = Convert.ToDateTime(DateFormatUtility.EditSlash(nowDate))
        
        Me._Frm.imdOutakaDate_From.TextValue = nowDate

        '当月の最終日取得
        Me._Frm.imdOutakaDate_To.TextValue = nextDate.AddMonths(1).AddDays(-1).ToString(LMFControlC.DATE_YYYYMMDD)


    End Sub

    ''' <summary>
    ''' 名称設定処理
    ''' </summary>
    ''' <param name="nowDate">システム日付</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetMstData(ByVal nowDate As String) As Boolean

        '荷主名の設定
        Call Me.SetCustData()

        'タリフ備考の設定
        Call Me.SetTariffData(nowDate)

        '割増備考の設定
        Call Me.SetExtcData()

        '運送会社名の設定
        Call Me.SetUnsocoData()

        '請求先名の設定
        Call Me.SetSeiqData()

        Return True

    End Function

    ''' <summary>
    ''' 荷主名の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCustData()

        With Me._Frm

            Dim brCd As String = .cmbNrsBrCd.SelectedValue.ToString() '20160928 要番2622 tsunehira add

            '大コードがない場合、スルー
            Dim custL As String = .txtCustCdL.TextValue
            If String.IsNullOrEmpty(custL) = True Then
                Exit Sub
            End If

            '中コードがない場合、スルー
            Dim custM As String = .txtCustCdM.TextValue
            If String.IsNullOrEmpty(custM) = True Then
                Exit Sub
            End If

            'キャッシュに検索
            'Dim drs As DataRow() = Me._LMFconG.SelectCustListDataRow(custL, custM, LMFControlC.FLG_OFF, LMFControlC.FLG_OFF)
            '20160928 要番2622 tsunehira add
            Dim drs As DataRow() = Me._LMFconG.SelectCustListDataRow(brCd, custL, custM, LMFControlC.FLG_OFF, LMFControlC.FLG_OFF)

            '取得できない場合、スルー
            If drs.Length < 1 Then
                Exit Sub
            End If

            '名称を設定
            .lblCustNm.TextValue = Me._LMFconG.EditConcatData(drs(0).Item("CUST_NM_L").ToString(), drs(0).Item("CUST_NM_M").ToString(), LMFControlC.ZENKAKU_SPACE)

        End With

    End Sub

    ''' <summary>
    ''' タリフ備考の設定
    ''' </summary>
    ''' <param name="nowDate">システム日付</param>
    ''' <remarks></remarks>
    Private Sub SetTariffData(ByVal nowDate As String)

        With Me._Frm

            'タリフコードに値がない場合、スルー
            Dim tariffCd As String = .txtOldTariffCd.TextValue
            If String.IsNullOrEmpty(tariffCd) = True Then
                Exit Sub
            End If

            'キャッシュから値取得
            Dim drs As DataRow() = Nothing
            drs = Me._LMFconG.SelectUnchinTariffListDataRow(tariffCd, String.Empty, nowDate)
          
            '取得できない場合、スルー
            If drs.Length < 1 Then
                Exit Sub
            End If

            '名称の設定
            .lblOldTariffNm.TextValue = drs(0).Item("UNCHIN_TARIFF_REM").ToString()

        End With

    End Sub

    ''' <summary>
    ''' 割増備考の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetExtcData()

        With Me._Frm

            '割増タリフコードに値がない場合、スルー
            Dim extcCd As String = .txtOldETariffCd.TextValue
            If String.IsNullOrEmpty(extcCd) = True Then
                Exit Sub
            End If

            'キャッシュに検索
            Dim drs As DataRow() = Me._LMFconG.SelectExtcUnchinListDataRow(.cmbNrsBrCd.SelectedValue.ToString(), extcCd, String.Empty)

            '取得できない場合、スルー
            If drs.Length < 1 Then
                Exit Sub
            End If

            '名称のｓ設定
            .lblOldETariffNm.TextValue = drs(0).Item("EXTC_TARIFF_REM").ToString()

        End With

    End Sub

    ''' <summary>
    ''' 運送会社名の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUnsocoData()

        With Me._Frm

            '営業所コード 20160928 要番2622 tsunehira add
            Dim brCd As String = .cmbNrsBrCd.SelectedValue.ToString()

            '運送会社コードがない場合、スルー
            Dim unsoCd As String = .txtUnsoCd.TextValue
            If String.IsNullOrEmpty(unsoCd) = True Then
                Exit Sub
            End If

            '運送会社支店コードがない場合、スルー
            Dim unsoBrCd As String = .txtUnsoBrCd.TextValue
            If String.IsNullOrEmpty(unsoBrCd) = True Then
                Exit Sub
            End If

            'キャッシュに検索
            ' Dim drs As DataRow() = Me._LMFconG.SelectUnsocoListDataRow(unsoCd, unsoBrCd)
            Dim drs As DataRow() = Me._LMFconG.SelectUnsocoListDataRow(brCd, unsoCd, unsoBrCd)


            '取得できない場合、スルー
            If drs.Length < 1 Then
                Exit Sub
            End If

            '取得できた場合、名称を設定
            .lblUnsoNm.TextValue = Me._LMFconG.EditConcatData(drs(0).Item("UNSOCO_NM").ToString(), drs(0).Item("UNSOCO_BR_NM").ToString(), LMFControlC.ZENKAKU_SPACE)

        End With

    End Sub

    ''' <summary>
    ''' 請求先名の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSeiqData()

        With Me._Frm

            '請求先コードがない場合、スルー
            Dim seiqCd As String = .txtSeiqtoCd.TextValue
            If String.IsNullOrEmpty(seiqCd) = True Then
                Exit Sub
            End If

            'キャッシュに検索
            Dim drs As DataRow() = Me._LMFconG.SelectSeiqtoListDataRow(.cmbNrsBrCd.SelectedValue.ToString(), seiqCd)

            '取得できない場合、スルー
            If drs.Length < 1 Then
                Exit Sub
            End If

            '名称の設定
            .lblSeiqtoNm.TextValue = drs(0).Item("SEIQTO_NM").ToString()

        End With

    End Sub

    ''' <summary>
    ''' 計算部の項目クリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearNum()

        With Me._Frm

            .numOldTariffSum.Value = 0
            .numOldETariffSum.Value = 0
            .numNewTariffSum.Value = 0
            .numNewETariffSum.Value = 0

        End With

    End Sub

    ''' <summary>
    ''' 画面の項目クリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearGui()

        With Me._Frm

            '荷主コードが入力されていない場合は名称を消す
            If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True AndAlso _
            String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                .lblCustNm.TextValue = String.Empty
            End If

            '旧タリフコードコードが入力されていない場合は名称を消す
            If String.IsNullOrEmpty(.txtOldTariffCd.TextValue) = True Then
                .lblOldTariffNm.TextValue = String.Empty
            End If

            '旧割増タリフコードが入力されていない場合は名称を消す
            If String.IsNullOrEmpty(.txtOldETariffCd.TextValue) = True Then
                .lblOldETariffNm.TextValue = String.Empty
            End If

            '請求コードが入力されていない場合は名称を消す
            If String.IsNullOrEmpty(.txtSeiqtoCd.TextValue) = True Then
                .lblSeiqtoNm.TextValue = String.Empty
            End If

            '新タリフコードが入力されていない場合は名称を消す
            If String.IsNullOrEmpty(.txtNewTariffCd.TextValue) = True Then
                .lblNewTariffNm.TextValue = String.Empty
            End If

            '新割増コードが入力されていない場合は名称を消す
            If String.IsNullOrEmpty(.txtNewETariffCd.TextValue) = True Then
                .lblNewETariffNm.TextValue = String.Empty
            End If

        End With

    End Sub

#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataSet, ByVal frm As LMF070F)

        'スプレッドに明細を設定
        Call Me.SetSpread(ds, frm)

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprUnchinDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared OUTKA_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.OUTKA_PLAN_DATE, "出荷日", 80, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.DEST_NM, "届先名", 150, True)
        Public Shared DEST_JIS_NM As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.DEST_JIS_NM, "届先住所", 150, True)
        Public Shared UNSOCO_CD As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.UNSOCO_NM, "運送会社", 150, True)
        Public Shared WT As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.WT, "重量", 80, True)
        Public Shared KYORI As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.OLD_KYORI, "距離", 80, True)
        Public Shared SEIQ_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.SEIQ_TARIFF_CD, "運賃タリフ", 100, True)
        Public Shared SEIQ_ETARIFF_CD As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.SEIQ_ETARIFF_CD, "割増タリフ", 100, True)
        Public Shared SEIQ_UNCHIN As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.SEIQ_UNCHIN, "運賃", 110, True)
        Public Shared NEW_SEIQ_KYORI As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.NEW_KYORI, "新距離", 80, True)
        Public Shared NEW_SEIQ_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.NEW_SEIQ_TARIFF_CD, "新運賃タリフ", 100, True)
        Public Shared NEW_SEIQ_ETARIFF_CD As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.NEW_SEIQ_ETARIFF_CD, "新割増タリフ", 100, True)
        Public Shared NEW_SEIQ_UNCHIN As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.NEW_SEIQ_UNCHIN, "新運賃", 110, True)
        Public Shared MOTO_DATA_KB As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.MOTO_DATA_KB, "元データ" & vbCrLf & "区分", 80, True)
        Public Shared INOUTKA_NO As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.INOUTKA_NO, "管理番号", 100, True)
        Public Shared SEIQ_CD As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.SEIQ_CD, "請求先コード", 100, True)
        Public Shared SEIQ_NM As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.SEIQ_NM, "請求先名", 100, True)
        Public Shared SOKO_NM As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.SOKO_NM, "倉庫", 150, True)
        Public Shared NEW_SOKO_NM As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.NEW_SOKO_NM, "新倉庫", 150, True)

        '隠し項目
        Public Shared MOTO_DATA As SpreadColProperty = New SpreadColProperty(LMF070C.SprColumnIndex.MOTO_DATA, "元データ区分", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim spr As LMSpread = Me._Frm.sprUnchin

        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMF070C.SprColumnIndex.CLM_NM

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMF070G.sprUnchinDef())

            '列固定位置を設定します。
            .ActiveSheet.FrozenColumnCount = LMF070G.sprUnchinDef.NEW_SEIQ_UNCHIN.ColNo + 1

            Dim umuStyle As StyleInfo = Me.StyleInfoCustCond(spr)
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabelRight As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            '2017/09/25 修正 李↓
            Dim sCom As StyleInfo = LMSpreadUtility.GetComboCellMaster(spr, LMConst.CacheTBL.KBN, "KBN_CD",
                                                                       lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"}),
                                                                       False, New String() {"KBN_GROUP_CD", "VALUE1"}, New String() {LMKbnConst.KBN_M004, "1.000"}, LMConst.JoinCondition.AND_WORD)
            '2017/09/25 修正 李↑

            Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
            Dim sTxt80 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 80, False)
            Dim sTxt40 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 40, False)
            Dim sTxt122 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 122, False)
            Dim sTxt9 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 9, False)
            Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999, True)

            'スプレッドの初期設定
            .SetCellStyle(0, LMF070G.sprUnchinDef.DEF.ColNo, sLabel)
            .SetCellStyle(0, LMF070G.sprUnchinDef.OUTKA_PLAN_DATE.ColNo, sLabel)
            .SetCellStyle(0, LMF070G.sprUnchinDef.DEST_NM.ColNo, sTxt80)
            .SetCellStyle(0, LMF070G.sprUnchinDef.DEST_JIS_NM.ColNo, sTxt40)
            .SetCellStyle(0, LMF070G.sprUnchinDef.WT.ColNo, sNum)
            .SetCellStyle(0, LMF070G.sprUnchinDef.KYORI.ColNo, sNum)
            .SetCellStyle(0, LMF070G.sprUnchinDef.SEIQ_TARIFF_CD.ColNo, sNum)
            .SetCellStyle(0, LMF070G.sprUnchinDef.SEIQ_ETARIFF_CD.ColNo, sNum)
            .SetCellStyle(0, LMF070G.sprUnchinDef.SEIQ_UNCHIN.ColNo, sNum)
            .SetCellStyle(0, LMF070G.sprUnchinDef.NEW_SEIQ_KYORI.ColNo, sNum)
            .SetCellStyle(0, LMF070G.sprUnchinDef.NEW_SEIQ_TARIFF_CD.ColNo, sNum)
            .SetCellStyle(0, LMF070G.sprUnchinDef.NEW_SEIQ_ETARIFF_CD.ColNo, sNum)
            .SetCellStyle(0, LMF070G.sprUnchinDef.NEW_SEIQ_UNCHIN.ColNo, sNum)
            .SetCellStyle(0, LMF070G.sprUnchinDef.MOTO_DATA_KB.ColNo, umuStyle)
            .SetCellStyle(0, LMF070G.sprUnchinDef.INOUTKA_NO.ColNo, sTxt9)
            .SetCellStyle(0, LMF070G.sprUnchinDef.SOKO_NM.ColNo, sLabel)
            .SetCellStyle(0, LMF070G.sprUnchinDef.NEW_SOKO_NM.ColNo, sLabel)
            .SetCellStyle(0, LMF070G.sprUnchinDef.SEIQ_CD.ColNo, sLabel)
            .SetCellStyle(0, LMF070G.sprUnchinDef.SEIQ_NM.ColNo, sLabel)
            .SetCellStyle(0, LMF070G.sprUnchinDef.UNSOCO_CD.ColNo, sTxt122)

        End With

    End Sub

    ''' <summary>
    ''' セルのプロパティを設定(CUSTCOND)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoCustCond(ByVal spr As LMSpread) As StyleInfo

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.KBN _
                                                  , "KBN_CD" _
                                                  , lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"}) _
                                                  , False _
                                                  , New String() {"VALUE1", "KBN_GROUP_CD"} _
                                                  , New String() {"1.000", "M004"} _
                                                  , LMConst.JoinCondition.AND_WORD _
                                                  )
        '2017/09/25 修正 李↑

    End Function

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet, ByVal frm As LMF070F)
        Dim spr As LMSpreadSearch = Me._Frm.sprUnchin
        Dim dt As DataTable = ds.Tables(LMF070C.TABLE_NM_OUT)
        Dim row As Integer = 0

        'ロック制御
        Dim lock As Boolean = True

        With spr

            .SuspendLayout()

            'SPREAD(表示行)初期化
            .CrearSpread()

            '----データ挿入----'
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count()
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr, lock)
            Dim sLabelNo As StyleInfo = Me.StyleInfoLabel(spr, False)
            Dim sNum12d2 As StyleInfo = Me.StyleInfoNum12dec2(spr, lock)
            Dim sNum12d3 As StyleInfo = Me.StyleInfoNum9dec3(spr)
            Dim sNum5 As StyleInfo = Me.StyleInfoNum5(spr)
            Dim sCom As StyleInfo = Me.StyleInfoCustCond(spr)

            Dim newtariff As String = frm.txtNewTariffCd.TextValue()
            Dim newetariff As String = frm.txtNewETariffCd.TextValue()

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)
                .SetCellStyle(i, LMF070G.sprUnchinDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMF070G.sprUnchinDef.OUTKA_PLAN_DATE.ColNo, sLabelNo)
                .SetCellStyle(i, LMF070G.sprUnchinDef.DEST_NM.ColNo, sLabelNo)
                .SetCellStyle(i, LMF070G.sprUnchinDef.DEST_JIS_NM.ColNo, sLabelNo)
                .SetCellStyle(i, LMF070G.sprUnchinDef.WT.ColNo, sNum12d3)
                .SetCellStyle(i, LMF070G.sprUnchinDef.KYORI.ColNo, sNum5)
                .SetCellStyle(i, LMF070G.sprUnchinDef.SEIQ_TARIFF_CD.ColNo, sLabelNo)
                .SetCellStyle(i, LMF070G.sprUnchinDef.SEIQ_ETARIFF_CD.ColNo, sLabelNo)
                .SetCellStyle(i, LMF070G.sprUnchinDef.SEIQ_UNCHIN.ColNo, sNum12d2)
                .SetCellStyle(i, LMF070G.sprUnchinDef.NEW_SEIQ_KYORI.ColNo, sNum5)
                .SetCellStyle(i, LMF070G.sprUnchinDef.NEW_SEIQ_TARIFF_CD.ColNo, sLabelNo)
                .SetCellStyle(i, LMF070G.sprUnchinDef.NEW_SEIQ_ETARIFF_CD.ColNo, sLabelNo)
                .SetCellStyle(i, LMF070G.sprUnchinDef.NEW_SEIQ_UNCHIN.ColNo, sNum12d2)
                .SetCellStyle(i, LMF070G.sprUnchinDef.MOTO_DATA_KB.ColNo, sLabelNo)
                .SetCellStyle(i, LMF070G.sprUnchinDef.INOUTKA_NO.ColNo, sLabelNo)
                .SetCellStyle(i, LMF070G.sprUnchinDef.SOKO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF070G.sprUnchinDef.NEW_SOKO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF070G.sprUnchinDef.SEIQ_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF070G.sprUnchinDef.SEIQ_NM.ColNo, sLabelNo)
                .SetCellStyle(i, LMF070G.sprUnchinDef.UNSOCO_CD.ColNo, sLabel)

                '隠し項目
                .SetCellStyle(i, LMF070G.sprUnchinDef.MOTO_DATA.ColNo, sLabelNo)

                'セルに値を設定
                .SetCellValue(i, LMF070G.sprUnchinDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMF070G.sprUnchinDef.OUTKA_PLAN_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("OUTKA_PLAN_DATE").ToString()))
                .SetCellValue(i, LMF070G.sprUnchinDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.DEST_JIS_NM.ColNo, dr.Item("DEST_AD").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.WT.ColNo, dr.Item("SEIQ_WT").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.KYORI.ColNo, dr.Item("SEIQ_KYORI").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.SEIQ_TARIFF_CD.ColNo, dr.Item("SEIQ_TARIFF_CD").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.SEIQ_ETARIFF_CD.ColNo, dr.Item("SEIQ_ETARIFF_CD").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.SEIQ_UNCHIN.ColNo, dr.Item("DECI_UNCHIN").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.NEW_SEIQ_KYORI.ColNo, dr.Item("NEW_SEIQ_KYORI").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.NEW_SEIQ_TARIFF_CD.ColNo, dr.Item("NEW_TARIFF_CD").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.NEW_SEIQ_ETARIFF_CD.ColNo, dr.Item("NEW_ETARIFF_CD").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.NEW_SEIQ_UNCHIN.ColNo, dr.Item("NEW_DECI_UNCHIN").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.MOTO_DATA_KB.ColNo, dr.Item("MOTO_DATA_NM").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.INOUTKA_NO.ColNo, dr.Item("INOUTKA_NO_L").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.SOKO_NM.ColNo, dr.Item("SOKO_NM").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.NEW_SOKO_NM.ColNo, dr.Item("NEW_SOKO_NM").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.SEIQ_CD.ColNo, dr.Item("SEIQTO_CD").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.SEIQ_NM.ColNo, dr.Item("SEIQ_NM").ToString())
                .SetCellValue(i, LMF070G.sprUnchinDef.UNSOCO_CD.ColNo, String.Concat(dr.Item("UNSOCO_NM").ToString(), "　", dr.Item("UNSOCO_BR_NM").ToString()))

                '隠し項目
                .SetCellValue(i, LMF070G.sprUnchinDef.MOTO_DATA.ColNo, dr.Item("MOTO_DATA_KB").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#Region "数値コントロール"

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d15 As Decimal = Convert.ToDecimal("9999999999999")

            .numOldTariffSum.SetInputFields("#,###,###,###,##0", , 13, , , , , , d15, 0)
            .numOldETariffSum.SetInputFields("#,###,###,###,##0", , 13, , , , , , d15, 0)
            .numNewTariffSum.SetInputFields("#,###,###,###,##0", , 13, , , , , , d15, 0)
            .numNewETariffSum.SetInputFields("#,###,###,###,##0", , 13, , , , , , d15, 0)

        End With

    End Sub

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
    Private Function StyleInfoLabel(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(英大数)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextEidaisu(ByVal spr As LMSpread, ByVal length As Integer) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA_U, length, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(MIX)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextMix(ByVal spr As LMSpread, ByVal length As Integer) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, length, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数12桁、少数2)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum12dec2(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.99, True, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数12桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum12(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999999, True, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁　少数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9dec3(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数5桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum5(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 99999, True, 0, )

    End Function

#End Region

#End Region

End Class
