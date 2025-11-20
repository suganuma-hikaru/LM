' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMF     : 運賃
'  プログラムID   : LMF060G : 運賃試算
'  作  成  者     : 菱刈
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

''' <summary>
''' LMF060Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF060G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF060F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconG As LMFControlG

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconV As LMFControlV


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF060F, ByVal v As LMFControlV, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMFconV = v

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
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"
            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = always
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

            'Main
            .sprDetail.TabIndex = LMF060C.CtlTabIndex.DETAIL
            .cmbEigyo.TabIndex = LMF060C.CtlTabIndex.Eigyo
            .cmbPrint.TabIndex = LMF060C.CtlTabIndex.Print
            .btnPrint.TabIndex = LMF060C.CtlTabIndex.btnprint
            .cmbUnso.TabIndex = LMF060C.CtlTabIndex.Unso
            .cmbSyasyu.TabIndex = LMF060C.CtlTabIndex.Syasyu
            .txtCustCdL.TabIndex = LMF060C.CtlTabIndex.CustCdL
            .txtCustCdM.TabIndex = LMF060C.CtlTabIndex.CustCdM
            .lblCustNm.TabIndex = LMF060C.CtlTabIndex.CustNM
            .imdUnsoDate.TabIndex = LMF060C.CtlTabIndex.UnsoDate
            .txtOrigJis.TabIndex = LMF060C.CtlTabIndex.OrigJis
            .lblOrigJisNm.TabIndex = LMF060C.CtlTabIndex.OrigJisNM
            .txtTodokedeJisCd.TabIndex = LMF060C.CtlTabIndex.TodokedeJisCd
            .txtTodokedeJisNm.TabIndex = LMF060C.CtlTabIndex.TodokedeJisNM
            .txtKyoriteiCd.TabIndex = LMF060C.CtlTabIndex.KyoriteiCd
            .btnKyoriSel.TabIndex = LMF060C.CtlTabIndex.Btnkyori
            .numKyori.TabIndex = LMF060C.CtlTabIndex.Kyori
            .txtTariffCd.TabIndex = LMF060C.CtlTabIndex.TariffCd
            .numJyuryo.TabIndex = LMF060C.CtlTabIndex.Jyuryo
            .txtWarimashiCd.TabIndex = LMF060C.CtlTabIndex.WarimashiCd
            .lblWarimashiNm.TabIndex = LMF060C.CtlTabIndex.WarimashiNM
            .btnGet.TabIndex = LMF060C.CtlTabIndex.BtnGet

            
        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        '数値コントロールの書式設定
        Call Me.SetNumberControl()


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

            .cmbPrint.Focus()


        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .sprDetail.TextValue = String.Empty
            .cmbEigyo.SelectedValue = String.Empty
            .cmbPrint.SelectedValue = String.Empty
            .cmbUnso.SelectedValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNm.TextValue = String.Empty
            .txtOrigJis.TextValue = String.Empty
            .lblOrigJisNm.TextValue = String.Empty
            .txtKyoriteiCd.TextValue = String.Empty
            .txtTariffCd.TextValue = String.Empty
            .lblWarimashiNm.TextValue = String.Empty
            .txtWarimashiCd.TextValue = String.Empty
            .cmbSyasyu.SelectedValue = String.Empty
            .imdUnsoDate.TextValue = String.Empty
            .txtTodokedeJisCd.TextValue = String.Empty
            .txtTodokedeJisNm.TextValue = String.Empty
            .numKyori.Value = 0
            .numJyuryo.Value = 0

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        With Me._Frm

            'スプレッドの設定
            Call Me.InitSpread(.sprDetail, New LMF060G.sprDetailDef(), 20)

        End With
    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef
        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared UNSO_TEHAI As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.SEIQ_TARIFF_BUNRUI_KB, "タリフ" & vbCrLf & "分類", 60, True)
        Public Shared VCLE As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.CAR_TP, "車輌区分", 80, True)
        Public Shared ORIG_JIS_CD As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.ORIG_JIS_CD, "発地JIS", 80, True)
        Public Shared ORIG_NM As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.ORIG_JIS_NM, "発地住所", 120, True)
        Public Shared DEST_JIS_CD As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.DEST_JIS_CD, "届先JIS", 80, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.DEST_JIS_NM, "届先住所", 120, True)
        Public Shared SEIQ_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.UNCHIN_TARIFF_CD, "タリフコード", 100, True)
        Public Shared SEIQ_ETARIFF_CD As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.EXTC_TARIFF_CD, "割増" & vbCrLf & "タリフコード", 100, True)
        Public Shared UNSO_DATE As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.STR_DATE, "運送日", 80, True)
        Public Shared KYORITEI As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.KYORI_CD, "距離程" & vbCrLf & "コード", 60, True)
        Public Shared KYORI As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.KYORI, "距離", 60, True)
        Public Shared UNSO_WT As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.WT_LV, "重量", 80, True)
        Public Shared UNCHIN_CAL As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.DECI_UNCHIN, "運賃", 80, True)
        Public Shared TOUKI As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.DECI_WINT_EXTC, "冬期割増", 80, True)
        Public Shared TOSI As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.DECI_CITY_EXTC, "都市割増", 80, True)
        Public Shared CYUKEI As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.DECI_RELY_EXTC, "中継料", 80, True)
        Public Shared KOUSOU As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.DECI_TOLL, "通行・航送料", 80, True)
        Public Shared SYUHAI As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.DECI_INSU, "保険料他", 80, True)
        Public Shared CUST As SpreadColProperty = New SpreadColProperty(LMF060C.SprColumnIndex.CUST, "荷主名", 120, True)


    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(ByVal spr As LMSpread, ByVal columnClass As Object, ByVal columnCnt As Integer)

        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = columnCnt

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(columnClass)

            '列固定位置を設定します。(ex.納入予定で固定)
            Me._Frm.sprDetail.ActiveSheet.FrozenColumnCount = LMF060G.sprDetailDef.DEST_NM.ColNo + 1

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal frm As LMF060F)

        'レイアウト用
        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
        Dim sLabelLeft As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sCom As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, "S031", False)
        Dim sNum5 As StyleInfo = Me.StyleInfoNum5(spr)
        Dim sNum12d3 As StyleInfo = Me.StyleInfoNum9dec3(spr)
        Dim sNum10 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True)
        Dim sNum7 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999, True)
        Dim sNumMax As StyleInfo = Me.StyleInfoNumMax(spr)



        With spr

            Dim lngcnt As Integer = 1

            Dim max As Integer = .ActiveSheet.Rows.Count

            'スプレッドに設定する項目
            Dim nrsbrcd As String = frm.cmbEigyo.SelectedValue.ToString
            Dim unso As String = frm.cmbUnso.TextValue.ToString
            Dim custl As String = frm.txtCustCdL.TextValue.ToString
            Dim custm As String = frm.txtCustCdM.TextValue.ToString
            Dim origjis As String = frm.txtOrigJis.TextValue.ToString
            Dim kyoriteicd As String = frm.txtKyoriteiCd.TextValue.ToString
            Dim tariffcd As String = frm.txtTariffCd.TextValue.ToString

            Dim warimashicd As String = frm.txtWarimashiCd.TextValue.ToString
            Dim syasyu As String = frm.cmbSyasyu.TextValue.ToString
            Dim unsodate As String = frm.imdUnsoDate.TextValue.ToString
            Dim todokesakijiscd As String = frm.txtTodokedeJisCd.TextValue.ToString
            Dim kyori As String = frm.numKyori.TextValue.ToString
            Dim jyutyo As String = frm.numJyuryo.TextValue.ToString
            Dim wint As String = frm.lblWint.TextValue.ToString
            Dim rely As String = frm.lblRely.TextValue.ToString
            Dim insu As String = frm.lblInsu.TextValue.ToString
            Dim city As String = frm.lblCity.TextValue.ToString
            Dim fryy As String = frm.lblFrry.TextValue.ToString
            Dim unchinkei As String = frm.numUnchin.TextValue.ToString
            Dim unchinkeiMeisai As String = frm.lblUnchinMeisai.TextValue.ToString '(2013/02/06 Notes 1826)

            '名称系のの取得用
            Dim custnm As String = String.Empty
            Dim origjisnm As String = String.Empty
            Dim warimasinm As String = String.Empty
            Dim todokesakinm As String = String.Empty

            'それぞれの値をキャッシュから取得
            Dim drs As DataRow() = Nothing
            Dim drt As DataRow() = Nothing
            Dim drw As DataRow() = Nothing
            Dim drto As DataRow() = Nothing
            Dim dro As DataRow() = Nothing
            Dim tariffnm As String = String.Empty

            '(2013/02/06 Notes 1826)
            'unchinkeiから割増分をマイナス
            '画面の隠し項目が使えない場合の時に遣う
            'unchinkeiMeisai = Convert.ToString(Convert.ToDecimal(unchinkei) - _
            '                                   Convert.ToDecimal(wint) - _
            '                                   Convert.ToDecimal(rely) - _
            '                                   Convert.ToDecimal(insu) - _
            '                                   Convert.ToDecimal(city) - _
            '                                   Convert.ToDecimal(city) - _
            '                                   Convert.ToDecimal(fryy))
            'キャッシュ(荷主)
            'drs = Me._LMFconG.SelectCustListDataRow(custl, custm, LMFControlC.FLG_OFF, LMFControlC.FLG_OFF)
            '20160928 要番2622 tsunehira add
            drs = Me._LMFconG.SelectCustListDataRow(nrsbrcd, custl, custm, LMFControlC.FLG_OFF, LMFControlC.FLG_OFF)



            Dim custcount As Integer = drs.Count

            'データが0の場合は処理をしない
            If custcount >= 1 Then
                custnm = String.Concat(drs(0).Item("CUST_NM_L").ToString(), "　", drs(0).Item("CUST_NM_M").ToString())
            End If


            'キャッシュ(発地jis)
            drto = Me._LMFconG.SelectJisListDataRow(origjis)

            Dim oricount As Integer = drto.Count

            'データが0の場合は処理をしない
            If oricount >= 1 Then
                origjisnm = String.Concat(drto(0).Item("KEN").ToString(), drto(0).Item("SHI").ToString())
            End If

            'キャッシュ(届先jis)
            dro = Me._LMFconG.SelectJisListDataRow(todokesakijiscd)

            Dim todocount As Integer = dro.Count

            'データが0の場合は処理をしない
            If todocount >= 1 Then
                todokesakinm = String.Concat(dro(0).Item("KEN").ToString(), dro(0).Item("SHI").ToString())
            End If

            '行の追加
            .ActiveSheet.AddRows(max, lngcnt)


            For i As Integer = max To max


                'セル
                .SetCellStyle(i, LMF060G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMF060G.sprDetailDef.UNSO_TEHAI.ColNo, sLabelLeft)
                .SetCellStyle(i, LMF060G.sprDetailDef.VCLE.ColNo, sLabelLeft)
                .SetCellStyle(i, LMF060G.sprDetailDef.ORIG_JIS_CD.ColNo, sLabelLeft)
                .SetCellStyle(i, LMF060G.sprDetailDef.ORIG_NM.ColNo, sLabelLeft)
                .SetCellStyle(i, LMF060G.sprDetailDef.DEST_JIS_CD.ColNo, sLabelLeft)
                .SetCellStyle(i, LMF060G.sprDetailDef.DEST_NM.ColNo, sLabelLeft)
                .SetCellStyle(i, LMF060G.sprDetailDef.SEIQ_TARIFF_CD.ColNo, sLabelLeft)
                .SetCellStyle(i, LMF060G.sprDetailDef.SEIQ_ETARIFF_CD.ColNo, sLabelLeft)
                .SetCellStyle(i, LMF060G.sprDetailDef.UNSO_DATE.ColNo, sLabelLeft)
                .SetCellStyle(i, LMF060G.sprDetailDef.KYORITEI.ColNo, sLabelLeft)
                .SetCellStyle(i, LMF060G.sprDetailDef.KYORI.ColNo, sNum5)
                .SetCellStyle(i, LMF060G.sprDetailDef.UNSO_WT.ColNo, sNum12d3)
                .SetCellStyle(i, LMF060G.sprDetailDef.UNCHIN_CAL.ColNo, sNumMax)
                .SetCellStyle(i, LMF060G.sprDetailDef.TOUKI.ColNo, sNum7)
                .SetCellStyle(i, LMF060G.sprDetailDef.TOSI.ColNo, sNum7)
                .SetCellStyle(i, LMF060G.sprDetailDef.CYUKEI.ColNo, sNum7)
                .SetCellStyle(i, LMF060G.sprDetailDef.KOUSOU.ColNo, sNum7)
                .SetCellStyle(i, LMF060G.sprDetailDef.SYUHAI.ColNo, sNum7)
                .SetCellStyle(i, LMF060G.sprDetailDef.CUST.ColNo, sLabelLeft)


                '値のセット
                .SetCellValue(i, LMF060G.sprDetailDef.UNSO_TEHAI.ColNo, unso)
                .SetCellValue(i, LMF060G.sprDetailDef.VCLE.ColNo, syasyu)
                .SetCellValue(i, LMF060G.sprDetailDef.ORIG_JIS_CD.ColNo, origjis)
                .SetCellValue(i, LMF060G.sprDetailDef.ORIG_NM.ColNo, origjisnm)
                .SetCellValue(i, LMF060G.sprDetailDef.DEST_JIS_CD.ColNo, todokesakijiscd)
                .SetCellValue(i, LMF060G.sprDetailDef.DEST_NM.ColNo, todokesakinm)
                .SetCellValue(i, LMF060G.sprDetailDef.SEIQ_TARIFF_CD.ColNo, tariffcd)
                .SetCellValue(i, LMF060G.sprDetailDef.SEIQ_ETARIFF_CD.ColNo, warimashicd)
                .SetCellValue(i, LMF060G.sprDetailDef.UNSO_DATE.ColNo, DateFormatUtility.EditSlash(unsodate))
                .SetCellValue(i, LMF060G.sprDetailDef.KYORITEI.ColNo, kyoriteicd)
                .SetCellValue(i, LMF060G.sprDetailDef.KYORI.ColNo, kyori)
                .SetCellValue(i, LMF060G.sprDetailDef.UNSO_WT.ColNo, jyutyo)
                '.SetCellValue(i, LMF060G.sprDetailDef.UNCHIN_CAL.ColNo, unchinkei)  '(2013/02/06 Notes 1826)
                .SetCellValue(i, LMF060G.sprDetailDef.UNCHIN_CAL.ColNo, unchinkeiMeisai)  '(2013/02/06 Notes 1826)
                .SetCellValue(i, LMF060G.sprDetailDef.TOUKI.ColNo, wint)
                .SetCellValue(i, LMF060G.sprDetailDef.TOSI.ColNo, city)
                .SetCellValue(i, LMF060G.sprDetailDef.CYUKEI.ColNo, rely)
                .SetCellValue(i, LMF060G.sprDetailDef.KOUSOU.ColNo, fryy)
                .SetCellValue(i, LMF060G.sprDetailDef.SYUHAI.ColNo, insu)
                .SetCellValue(i, LMF060G.sprDetailDef.CUST.ColNo, custnm)


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
    ''' セルのプロパティを設定(Number 整数9桁　少数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9dec3(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数最大桁[14])
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNumMax(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, LMFControlC.MAX_KETA_SPR, True, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数最大桁[5])
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum5(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 99999, True, 0, , ",")

    End Function

    ''' <summary>
    '''営業所コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetcmbNrsBrCd()

        With Me._Frm

            .cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            .cmbPrint.SelectedValue = LMFControlC.FLG_OFF
            .cmbUnso.SelectedValue = LMFControlC.FLG_OFF

            '2014.08.04 FFEM高取対応 START
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

            If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
                .cmbEigyo.ReadOnly = True
            Else
                .cmbEigyo.ReadOnly = False
            End If
            '2014.08.04 FFEM高取対応 END

        End With

    End Sub

    'START YANAI 要望番号836
    ''' <summary>
    '''発地JISの初期値設定
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SettxtOrigJis()

        With Me._Frm

            'Dim sokoDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat("WH_CD = '", LMUserInfoManager.GetWhCd, "'"))
            Dim sokoDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat("WH_CD = '", LMUserInfoManager.GetWhCd, "' AND NRS_BR_CD = '", .cmbEigyo.SelectedValue.ToString(), "' AND SYS_DEL_FLG = '0' "))
            If 0 < sokoDr.Length Then
                .txtOrigJis.TextValue = sokoDr(0).Item("JIS_CD").ToString
            End If

            Dim jisDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.JIS).Select(String.Concat("JIS_CD = '", .txtOrigJis.TextValue, "'"))
            If 0 < jisDr.Length Then
                .lblOrigJisNm.TextValue = String.Concat(jisDr(0).Item("KEN").ToString, jisDr(0).Item("SHI").ToString)
            End If

        End With

    End Sub
    'END YANAI 要望番号836

#End Region

#End Region

#Region "数値コントロール"

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            
            Dim d5 As Decimal = Convert.ToDecimal("99999")
            Dim d12d3 As Decimal = Convert.ToDecimal("999,999,999.999")
            Dim d10 As Decimal = Convert.ToDecimal("9999999999")


            .numUnchin.SetInputFields("#,###,###,##0", , 10, , , 0, 0, , d10, 0)
            .numWarimashi.SetInputFields("#,###,###,##0", , 10, , , 0, 0, , d10, 0)
            .numKyori.SetInputFields("##,##0", , 5, , , 0, 0, , d5, 0)
            .numJyuryo.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d12d3, 0)

        End With

    End Sub

#End Region

#End Region

End Class
