' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME040  : 作業指示書検索
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LME040Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LME040G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LME040F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEconV As LMEControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LME040F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        '画面共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)

        'Validate共通クラスの設定
        Me._LMEconV = New LMEControlV(handlerClass, sForm)

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
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = LME040C.FUNCTION_HENSHU
            .F3ButtonName = LME040C.FUNCTION_FUKUSHA
            .F4ButtonName = LME040C.EVENTNAME_DELETE
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = LME040C.FUNCTION_MASTER
            .F11ButtonName = LME040C.FUNCTION_HOZON
            .F12ButtonName = LME040C.FUNCTION_CLOSE

            Select Case mode
                Case LME040C.MODE_SINKI '新規
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
                    .F11ButtonEnabled = always
                    .F12ButtonEnabled = always

                Case LME040C.MODE_VIEW '編集(参照モード)
                    'ファンクションキーの制御
                    .F1ButtonEnabled = lock
                    .F2ButtonEnabled = always
                    .F3ButtonEnabled = always
                    .F4ButtonEnabled = always
                    .F5ButtonEnabled = lock
                    .F6ButtonEnabled = lock
                    .F7ButtonEnabled = lock
                    .F8ButtonEnabled = lock
                    .F9ButtonEnabled = lock
                    .F10ButtonEnabled = lock
                    .F11ButtonEnabled = lock
                    .F12ButtonEnabled = always

                Case LME040C.MODE_EDIT '編集(編集モード)
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
                    .F11ButtonEnabled = always
                    .F12ButtonEnabled = always

                    '2015.10.15 英語化対応START
                    'タイトルテキスト・フォント設定の切り替え
                    .TitleSwitching(Me._Frm)
                    '2015.10.15 英語化対応END

            End Select

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

    ''' <summary>
    ''' シチュエーションラベルの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSituation(ByVal dispMode As String, ByVal recordStatus As String)

        '編集部の項目をクリア
        With Me._frm
            .lblSituation.DispMode = dispMode
            .lblSituation.RecordStatus = recordStatus
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

            .cmbPrint.TabIndex = LME040C.CtlTabIndex.CMBPRINT
            .btnPrint.TabIndex = LME040C.CtlTabIndex.BTNPRINT
            .grpSagyoSiji.TabIndex = LME040C.CtlTabIndex.GRPSAGYOSIJI
            .cmbEigyo.TabIndex = LME040C.CtlTabIndex.EIGYO
            .txtCustCdL.TabIndex = LME040C.CtlTabIndex.CUSTCDL
            .lblCustNmL.TabIndex = LME040C.CtlTabIndex.CUSTNML
            .txtCustCdM.TabIndex = LME040C.CtlTabIndex.CUSTCDM
            .lblCustNmM.TabIndex = LME040C.CtlTabIndex.CUSTNMM
            .imdSagyoDate.TabIndex = LME040C.CtlTabIndex.SAGYODATE
            .cmbIozsKb.TabIndex = LME040C.CtlTabIndex.IOZSKB
            .txtRemark1.TabIndex = LME040C.CtlTabIndex.REMARK1
            .txtRemark2.TabIndex = LME040C.CtlTabIndex.REMARK2
            .txtRemark3.TabIndex = LME040C.CtlTabIndex.REMARK3
            .grpZaiko.TabIndex = LME040C.CtlTabIndex.GRPZAIKO
            .btnRowAdd.TabIndex = LME040C.CtlTabIndex.BTNROWADD
            .btnRowDel.TabIndex = LME040C.CtlTabIndex.BTNROWDEL
            .sprDetails.TabIndex = LME040C.CtlTabIndex.SPRDETAILS
            .grpSagyo.TabIndex = LME040C.CtlTabIndex.GRPSAGYO
            .txtSagyo1.TabIndex = LME040C.CtlTabIndex.SAGYOCD1
            .lblSagyo1.TabIndex = LME040C.CtlTabIndex.SAGYONM1
            .txtSagyoRemark1.TabIndex = LME040C.CtlTabIndex.SAGYORMK1
            .txtSagyo2.TabIndex = LME040C.CtlTabIndex.SAGYOCD2
            .lblSagyo2.TabIndex = LME040C.CtlTabIndex.SAGYONM2
            .txtSagyoRemark2.TabIndex = LME040C.CtlTabIndex.SAGYORMK2
            .txtSagyo3.TabIndex = LME040C.CtlTabIndex.SAGYOCD3
            .lblSagyo3.TabIndex = LME040C.CtlTabIndex.SAGYONM3
            .txtSagyoRemark3.TabIndex = LME040C.CtlTabIndex.SAGYORMK3
            .txtSagyo4.TabIndex = LME040C.CtlTabIndex.SAGYOCD4
            .lblSagyo4.TabIndex = LME040C.CtlTabIndex.SAGYONM4
            .txtSagyoRemark4.TabIndex = LME040C.CtlTabIndex.SAGYORMK4
            .txtSagyo5.TabIndex = LME040C.CtlTabIndex.SAGYOCD5
            .lblSagyo5.TabIndex = LME040C.CtlTabIndex.SAGYONM5
            .txtSagyoRemark5.TabIndex = LME040C.CtlTabIndex.SAGYORMK5

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String, ByVal ds As DataSet)

        With Me._Frm

            '自営業所の設定
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            Dim brCd As String = ds.Tables(LME040C.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString
            .cmbEigyo.SelectedValue = brCd
            '入出在その他区分
            .cmbIozsKb.SelectedValue = "30"
            '荷主コード(大)
            .txtCustCdL.TextValue = ds.Tables(LME040C.TABLE_NM_IN).Rows(0).Item("CUST_CD_L").ToString
            '荷主コード(中)
            .txtCustCdM.TextValue = ds.Tables(LME040C.TABLE_NM_IN).Rows(0).Item("CUST_CD_M").ToString

            Dim custDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                              "CUST_CD_M = '", .txtCustCdM.TextValue, "'"))
            If custDr.Length > 0 Then
                '荷主名(大)
                .lblCustNmL.TextValue = custDr(0).Item("CUST_NM_L").ToString
                '荷主名(中)
                .lblCustNmM.TextValue = custDr(0).Item("CUST_NM_M").ToString
                '作業料請求先マスターコード
                .txtSagyoSeiqtoCd.TextValue = custDr(0).Item("SAGYO_SEIQTO_CD").ToString
                '主請求先マスターコード
                .txtOyaSeiqtoCd.TextValue = custDr(0).Item("OYA_SEIQTO_CD").ToString
            End If

            '自倉庫の設定
            Dim whCd As String = LMUserInfoManager.GetWhCd()
            .txtSokoCd.TextValue = whCd

            'タブレット項目の初期値設定
            .cmbWHSagyoSintyoku.SelectedValue = "00"

            .cmbSagyoSijiStatus.SelectedValue = "00"

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(ByVal mode As String)

        Dim always As Boolean = True
        Dim lock As Boolean = False

        With Me._Frm

            Select Case mode
                Case LME040C.MODE_SINKI '新規

                    Me._Frm.btnPrint.Enabled = lock
                    Me._Frm.btnRowAdd.Enabled = always
                    Me._Frm.btnRowDel.Enabled = always

                    Me._Frm.cmbPrint.ReadOnly = True
                    Me._Frm.imdSagyoDate.ReadOnly = False
                    Me._Frm.txtRemark1.ReadOnly = False
                    Me._Frm.txtRemark2.ReadOnly = False
                    Me._Frm.txtRemark3.ReadOnly = False
                    Me._Frm.txtSagyo1.ReadOnly = True
                    Me._Frm.txtSagyo2.ReadOnly = True
                    Me._Frm.txtSagyo3.ReadOnly = True
                    Me._Frm.txtSagyo4.ReadOnly = True
                    Me._Frm.txtSagyo5.ReadOnly = True
                    Me._Frm.txtSagyoRemark1.ReadOnly = True
                    Me._Frm.txtSagyoRemark2.ReadOnly = True
                    Me._Frm.txtSagyoRemark3.ReadOnly = True
                    Me._Frm.txtSagyoRemark4.ReadOnly = True
                    Me._Frm.txtSagyoRemark5.ReadOnly = True
                    Me._Frm.txtSagyoSijiNo.ReadOnly = True

                    Me._Frm.btnJikkou.Enabled = lock
                    Me._Frm.cmbJikkou.ReadOnly = True

                Case LME040C.MODE_VIEW '編集(参照モード)

                    Me._Frm.btnPrint.Enabled = always
                    Me._Frm.btnRowAdd.Enabled = lock
                    Me._Frm.btnRowDel.Enabled = lock

                    Me._Frm.cmbPrint.ReadOnly = False
                    Me._Frm.imdSagyoDate.ReadOnly = True
                    Me._Frm.txtRemark1.ReadOnly = True
                    Me._Frm.txtRemark2.ReadOnly = True
                    Me._Frm.txtRemark3.ReadOnly = True
                    Me._Frm.txtSagyo1.ReadOnly = True
                    Me._Frm.txtSagyo2.ReadOnly = True
                    Me._Frm.txtSagyo3.ReadOnly = True
                    Me._Frm.txtSagyo4.ReadOnly = True
                    Me._Frm.txtSagyo5.ReadOnly = True
                    Me._Frm.txtSagyoRemark1.ReadOnly = True
                    Me._Frm.txtSagyoRemark2.ReadOnly = True
                    Me._Frm.txtSagyoRemark3.ReadOnly = True
                    Me._Frm.txtSagyoRemark4.ReadOnly = True
                    Me._Frm.txtSagyoRemark5.ReadOnly = True
                    Me._Frm.txtSagyoSijiNo.ReadOnly = True

                    Me._Frm.btnJikkou.Enabled = always
                    Me._Frm.cmbJikkou.ReadOnly = False

                Case LME040C.MODE_EDIT '編集(編集モード)

                    Me._Frm.btnPrint.Enabled = lock
                    Me._Frm.btnRowAdd.Enabled = always
                    Me._Frm.btnRowDel.Enabled = always

                    Me._Frm.cmbPrint.ReadOnly = True
                    Me._Frm.imdSagyoDate.ReadOnly = False
                    Me._Frm.txtRemark1.ReadOnly = False
                    Me._Frm.txtRemark2.ReadOnly = False
                    Me._Frm.txtRemark3.ReadOnly = False
                    Me._Frm.txtSagyo1.ReadOnly = False
                    Me._Frm.txtSagyo2.ReadOnly = False
                    Me._Frm.txtSagyo3.ReadOnly = False
                    Me._Frm.txtSagyo4.ReadOnly = False
                    Me._Frm.txtSagyo5.ReadOnly = False
                    Me._Frm.txtSagyoRemark1.ReadOnly = False
                    Me._Frm.txtSagyoRemark2.ReadOnly = False
                    Me._Frm.txtSagyoRemark3.ReadOnly = False
                    Me._Frm.txtSagyoRemark4.ReadOnly = False
                    Me._Frm.txtSagyoRemark5.ReadOnly = False
                    Me._Frm.txtSagyoSijiNo.ReadOnly = True

                    Me._Frm.btnJikkou.Enabled = lock
                    Me._Frm.cmbJikkou.ReadOnly = True

            End Select

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .imdSagyoDate.Focus()

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
    ''' コントロール値のクリア(複写時)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlFukusha()

        With Me._Frm

            '作業指示書番号のクリア
            .txtSagyoSijiNo.TextValue = String.Empty

            'スプレッドの行をクリア
            .sprDetails.CrearSpread()

            '作業情報・在庫情報のクリア
            Call Me.ClearMeisaiData()

        End With

    End Sub

    ''' <summary>
    ''' 作業指示書データの表示
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSagyoSiji(ByVal ds As DataSet)

        Dim dr() As DataRow = ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Select("UPD_FLG = '0' OR UPD_FLG = '1'")

        With Me._Frm

            'If dr.Length > 0 Then
            '    .imdSagyoDate.TextValue = dr(0).Item("SAGYO_COMP_DATE").ToString
            'End If

            If ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows.Count > 0 Then
                .txtRemark1.TextValue = ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("REMARK_1").ToString
                .txtRemark2.TextValue = ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("REMARK_2").ToString
                .txtRemark3.TextValue = ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("REMARK_3").ToString
                .txtSagyoSijiNo.TextValue = ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("SAGYO_SIJI_NO").ToString
                .cmbWHSagyoSintyoku.SelectedValue = ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("WH_TAB_STATUS").ToString
                .cmbSagyoSijiStatus.SelectedValue = ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("SAGYO_SIJI_STATUS").ToString
                .imdSagyoDate.TextValue = ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("SAGYO_SIJI_DATE").ToString
            End If

        End With

    End Sub

    ''' <summary>
    ''' 作業情報・在庫情報のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearMeisaiData()

        With Me._Frm

            '作業情報部
            .txtSagyo1.TextValue = String.Empty
            .lblSagyo1.TextValue = String.Empty
            .txtSagyoRemark1.TextValue = String.Empty

            .txtSagyo2.TextValue = String.Empty
            .lblSagyo2.TextValue = String.Empty
            .txtSagyoRemark2.TextValue = String.Empty

            .txtSagyo3.TextValue = String.Empty
            .lblSagyo3.TextValue = String.Empty
            .txtSagyoRemark3.TextValue = String.Empty

            .txtSagyo4.TextValue = String.Empty
            .lblSagyo4.TextValue = String.Empty
            .txtSagyoRemark4.TextValue = String.Empty

            .txtSagyo5.TextValue = String.Empty
            .lblSagyo5.TextValue = String.Empty
            .txtSagyoRemark5.TextValue = String.Empty

            .txtKeyNo.TextValue = String.Empty

            '在庫情報部
            .lblGoodsNm.TextValue = String.Empty
            .lblTou.TextValue = String.Empty
            .lblSitu.TextValue = String.Empty
            .lblZone.TextValue = String.Empty
            .lblLoca.TextValue = String.Empty
            .lblLotNo.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 作業情報・在庫情報の表示
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetMeisaiData(ByVal ds As DataSet)

        With Me._Frm

            If .sprDetails.ActiveSheet.Rows.Count = 0 Then
                Exit Sub
            End If

            Dim rowNo As Integer = .sprDetails.ActiveSheet.ActiveRow.Index
            Dim dr() As DataRow = ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Select(String.Concat("KEY_NO = '", Me._LMEconV.GetCellValue(.sprDetails.ActiveSheet.Cells(rowNo, sprDetailsDef.KEYNO.ColNo)), "' AND ", _
                                                                                               "SAGYO_CD <> '", String.Empty, "' AND", _
                                                                                               "(UPD_FLG = '0' OR UPD_FLG = '1')"))
            Dim max As Integer = dr.Length - 1

            '作業情報部
            For i As Integer = 0 To max
                Select Case i
                    Case 0
                        .txtSagyo1.TextValue = dr(i).Item("SAGYO_CD").ToString
                        .lblSagyo1.TextValue = dr(i).Item("SAGYO_NM").ToString
                        .txtSagyoRemark1.TextValue = dr(i).Item("REMARK_SIJI").ToString
                    Case 1
                        .txtSagyo2.TextValue = dr(i).Item("SAGYO_CD").ToString
                        .lblSagyo2.TextValue = dr(i).Item("SAGYO_NM").ToString
                        .txtSagyoRemark2.TextValue = dr(i).Item("REMARK_SIJI").ToString
                    Case 2
                        .txtSagyo3.TextValue = dr(i).Item("SAGYO_CD").ToString
                        .lblSagyo3.TextValue = dr(i).Item("SAGYO_NM").ToString
                        .txtSagyoRemark3.TextValue = dr(i).Item("REMARK_SIJI").ToString
                    Case 3
                        .txtSagyo4.TextValue = dr(i).Item("SAGYO_CD").ToString
                        .lblSagyo4.TextValue = dr(i).Item("SAGYO_NM").ToString
                        .txtSagyoRemark4.TextValue = dr(i).Item("REMARK_SIJI").ToString
                    Case 4
                        .txtSagyo5.TextValue = dr(i).Item("SAGYO_CD").ToString
                        .lblSagyo5.TextValue = dr(i).Item("SAGYO_NM").ToString
                        .txtSagyoRemark5.TextValue = dr(i).Item("REMARK_SIJI").ToString
                End Select
            Next

            .txtKeyNo.TextValue = Me._LMEconV.GetCellValue(.sprDetails.ActiveSheet.Cells(rowNo, sprDetailsDef.KEYNO.ColNo))


            '在庫情報部
            .lblGoodsNm.TextValue = Me._LMEconV.GetCellValue(.sprDetails.ActiveSheet.Cells(rowNo, sprDetailsDef.GOODSNM.ColNo))
            .lblTou.TextValue = Me._LMEconV.GetCellValue(.sprDetails.ActiveSheet.Cells(rowNo, sprDetailsDef.TOUNO.ColNo))
            .lblSitu.TextValue = Me._LMEconV.GetCellValue(.sprDetails.ActiveSheet.Cells(rowNo, sprDetailsDef.SITUNO.ColNo))
            .lblZone.TextValue = Me._LMEconV.GetCellValue(.sprDetails.ActiveSheet.Cells(rowNo, sprDetailsDef.ZONECD.ColNo))
            .lblLoca.TextValue = Me._LMEconV.GetCellValue(.sprDetails.ActiveSheet.Cells(rowNo, sprDetailsDef.LOCA.ColNo))
            .lblLotNo.TextValue = Me._LMEconV.GetCellValue(.sprDetails.ActiveSheet.Cells(rowNo, sprDetailsDef.LOTNO.ColNo))

        End With

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailsDef

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared GOODSNM As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.GOODSNM, "商品名", 200, True)
        Public Shared GOODSCDCUST As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.GOODSCDCUST, "商品コード", 100, True)
        Public Shared TOUNO As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.TOUNO, "棟", 50, True)
        Public Shared SITUNO As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.SITUNO, "室", 40, True)
        Public Shared ZONECD As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.ZONECD, "ZONE", 40, True)
        Public Shared LOCA As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.LOCA, "ロケーション", 100, True)
        Public Shared LOTNO As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.LOTNO, "ロット№", 100, True)
        Public Shared SAGYONB As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.SAGYONB, "作業個数", 80, True)
        Public Shared PORAZAINB As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.PORAZAINB, "残個数", 80, True)
        Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.IRIME, "入目", 60, True)
        Public Shared PORAZAIQT As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.PORAZAIQT, "残数量", 80, True)
        Public Shared LTDATE As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.LTDATE, "賞味期限", 80, True)
        Public Shared GOODSCONDKB1 As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.GOODSCONDKB1, "状態 中身", 80, True)
        Public Shared GOODSCONDKB2 As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.GOODSCONDKB2, "状態 外観", 80, True)
        Public Shared GOODSCONDKB3 As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.GOODSCONDKB3, "状態 荷主", 80, True)
        Public Shared OFBKB As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.OFBKB, "薄外品", 80, True)
        Public Shared SPDKB As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.SPDKB, "保留品", 80, True)
        Public Shared SERIALNO As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.SERIALNO, "シリアル№", 100, True)
        Public Shared GOODSCRTDATE As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.GOODSCRTDATE, "製造日", 80, True)
        Public Shared DESTCD As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.DESTCD, "届先コード", 100, True)
        Public Shared ALLOCPRIORITY As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.ALLOCPRIORITY, "割当優先", 80, True)
        Public Shared INKODATE As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.INKODATE, "入荷日", 80, True)
        Public Shared INKOPLANDATE As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.INKOPLANDATE, "入荷予定日", 80, True)
        Public Shared GOODSCDNRS As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.GOODSCDNRS, "商品キー", 0, False)
        Public Shared WHCD As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.WHCD, "倉庫コード", 0, False)
        Public Shared TAXKB As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.TAXKB, "課税区分", 0, False)
        Public Shared ZAIRECNO As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.ZAIRECNO, "在庫レコード番号", 0, False)
        Public Shared INKANOLM As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.INKANOLM, "入荷管理番号(大) + (中)", 0, False)
        Public Shared PORAZAINBZAI As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.PORAZAINBZAI, "実予在庫個数(現在)", 0, False)
        Public Shared DESTNM As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.DESTNM, "届先名", 0, False)
        Public Shared KEYNO As SpreadColProperty = New SpreadColProperty(LME040C.SprColumnIndex.KEYNO, "キー番号", 0, False)

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
            .sprDetails.Sheets(0).ColumnCount = LME040C.SprColumnIndex.LAST

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetails.SetColProperty(New sprDetailsDef)
            .sprDetails.SetColProperty(New LME040G.sprDetailsDef(), False)
            '2015.10.15 英語化対応END

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprDetails
        Dim rowCnt As Integer = 0
        Dim dr() As DataRow = ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Select("UPD_FLG = '0' OR UPD_FLG = '1'")
        Dim lngcnt As Integer = dr.Length - 1

        'セルに設定するスタイルの取得
        Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
        Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sDate As StyleInfo = Me.StyleInfoDate(spr, True)

        Dim max As Integer = 0
        Dim addFlg As Boolean = True

        With spr

            .SuspendLayout()

            If lngcnt = -1 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            'SPREAD(表示行)初期化
            .CrearSpread()

            '値設定
            For i As Integer = 0 To lngcnt

                addFlg = True
                'KEY_NOが一覧に存在しない場合のみ一覧に表示
                max = .ActiveSheet.Rows.Count - 1
                For j As Integer = 0 To max
                    If (dr(i).Item("KEY_NO").ToString).Equals(Me._LMEconV.GetCellValue(.ActiveSheet.Cells(j, sprDetailsDef.KEYNO.ColNo))) = True Then
                        '同じKEY_NOが存在した場合
                        addFlg = False
                        Continue For
                    End If
                Next
                If addFlg = False Then
                    Continue For
                End If

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                .SetCellStyle(rowCnt, sprDetailsDef.DEF.ColNo, sDEF)
                .SetCellStyle(rowCnt, sprDetailsDef.GOODSNM.ColNo, sLabel)  '商品名
                .SetCellStyle(rowCnt, sprDetailsDef.GOODSCDCUST.ColNo, sLabel)  '商品コード
                .SetCellStyle(rowCnt, sprDetailsDef.TOUNO.ColNo, sLabel)  '棟
                .SetCellStyle(rowCnt, sprDetailsDef.SITUNO.ColNo, sLabel)  '室
                .SetCellStyle(rowCnt, sprDetailsDef.ZONECD.ColNo, sLabel)  'ZONE
                .SetCellStyle(rowCnt, sprDetailsDef.LOCA.ColNo, sLabel)  'ロケーション
                .SetCellStyle(rowCnt, sprDetailsDef.LOTNO.ColNo, sLabel)  'ロット№
                .SetCellStyle(rowCnt, sprDetailsDef.SAGYONB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))  '作業個数
                .SetCellStyle(rowCnt, sprDetailsDef.PORAZAINB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))  '残個数
                .SetCellStyle(rowCnt, sprDetailsDef.IRIME.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999.999, True, 3, True, ","))  '入目
                .SetCellStyle(rowCnt, sprDetailsDef.PORAZAIQT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, True, ","))  '残数量
                .SetCellStyle(rowCnt, sprDetailsDef.LTDATE.ColNo, sDate)  '賞味期限
                .SetCellStyle(rowCnt, sprDetailsDef.GOODSCONDKB1.ColNo, sLabel)  '状態 中身
                .SetCellStyle(rowCnt, sprDetailsDef.GOODSCONDKB2.ColNo, sLabel)  '状態 外観
                .SetCellStyle(rowCnt, sprDetailsDef.GOODSCONDKB3.ColNo, sLabel)  '状態 荷主
                .SetCellStyle(rowCnt, sprDetailsDef.OFBKB.ColNo, sLabel)  '薄外品
                .SetCellStyle(rowCnt, sprDetailsDef.SPDKB.ColNo, sLabel)  '保留品
                .SetCellStyle(rowCnt, sprDetailsDef.SERIALNO.ColNo, sLabel)  'シリアル№
                .SetCellStyle(rowCnt, sprDetailsDef.GOODSCRTDATE.ColNo, sDate)  '製造日
                .SetCellStyle(rowCnt, sprDetailsDef.DESTCD.ColNo, sLabel)  '届先コード
                .SetCellStyle(rowCnt, sprDetailsDef.ALLOCPRIORITY.ColNo, sLabel)  '割当優先
                .SetCellStyle(rowCnt, sprDetailsDef.INKODATE.ColNo, sDate)  '入荷日
                .SetCellStyle(rowCnt, sprDetailsDef.INKOPLANDATE.ColNo, sDate)  '入荷予定日
                .SetCellStyle(rowCnt, sprDetailsDef.GOODSCDNRS.ColNo, sLabel)  '商品キー
                .SetCellStyle(rowCnt, sprDetailsDef.WHCD.ColNo, sLabel)  '倉庫コード
                .SetCellStyle(rowCnt, sprDetailsDef.TAXKB.ColNo, sLabel)  '課税区分
                .SetCellStyle(rowCnt, sprDetailsDef.ZAIRECNO.ColNo, sLabel)  '在庫レコード番号
                .SetCellStyle(rowCnt, sprDetailsDef.INKANOLM.ColNo, sLabel)  '入荷管理番号(大) + (中)
                .SetCellStyle(rowCnt, sprDetailsDef.PORAZAINBZAI.ColNo, sLabel)  '実予在庫個数(現在)
                .SetCellStyle(rowCnt, sprDetailsDef.DESTNM.ColNo, sLabel)  '届先名
                .SetCellStyle(rowCnt, sprDetailsDef.KEYNO.ColNo, sLabel)  'キー番号

                'セルに値を設定
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.DEF.ColNo, False.ToString())
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.GOODSNM.ColNo, dr(i).Item("GOODS_NM_NRS").ToString()) '商品名
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.GOODSCDCUST.ColNo, dr(i).Item("GOODS_CD_CUST").ToString()) '商品コード
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.TOUNO.ColNo, dr(i).Item("TOU_NO").ToString()) '棟
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.SITUNO.ColNo, dr(i).Item("SITU_NO").ToString()) '室
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.ZONECD.ColNo, dr(i).Item("ZONE_CD").ToString()) 'ZONE
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.LOCA.ColNo, dr(i).Item("LOCA").ToString()) 'ロケーション
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.LOTNO.ColNo, dr(i).Item("LOT_NO").ToString()) 'ロット№
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.SAGYONB.ColNo, dr(i).Item("SAGYO_NB").ToString()) '作業個数
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.PORAZAINB.ColNo, dr(i).Item("PORA_ZAI_NB").ToString()) '残個数
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.IRIME.ColNo, dr(i).Item("IRIME").ToString()) '入目
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.PORAZAIQT.ColNo, dr(i).Item("PORA_ZAI_QT").ToString()) '残数量
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.LTDATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr(i).Item("LT_DATE").ToString())) '賞味期限
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.GOODSCONDKB1.ColNo, dr(i).Item("GOODS_COND_KB_1_NM").ToString()) '状態 中身
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.GOODSCONDKB2.ColNo, dr(i).Item("GOODS_COND_KB_2_NM").ToString()) '状態 外観
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.GOODSCONDKB3.ColNo, dr(i).Item("GOODS_COND_KB_3_NM").ToString()) '状態 荷主
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.OFBKB.ColNo, dr(i).Item("OFB_KB_NM").ToString()) '薄外品
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.SPDKB.ColNo, dr(i).Item("SPD_KB_NM").ToString()) '保留品
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.SERIALNO.ColNo, dr(i).Item("SERIAL_NO").ToString()) 'シリアル№
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.GOODSCRTDATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr(i).Item("GOODS_CRT_DATE").ToString())) '製造日
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.DESTCD.ColNo, dr(i).Item("DEST_CD_P").ToString()) '届先コード
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.ALLOCPRIORITY.ColNo, dr(i).Item("ALLOC_PRIORITY_NM").ToString()) '割当優先
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.INKODATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr(i).Item("INKO_DATE").ToString())) '入荷日
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.INKOPLANDATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr(i).Item("INKO_PLAN_DATE").ToString())) '入荷予定日
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.GOODSCDNRS.ColNo, dr(i).Item("GOODS_CD_NRS").ToString()) '商品キー
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.WHCD.ColNo, dr(i).Item("WH_CD").ToString()) '倉庫コード
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.TAXKB.ColNo, dr(i).Item("TAX_KB").ToString()) '課税区分
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.ZAIRECNO.ColNo, dr(i).Item("ZAI_REC_NO").ToString()) '在庫レコード番号
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.INKANOLM.ColNo, dr(i).Item("INOUTKA_NO_LM").ToString()) '入荷管理番号(大) + (中)
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.PORAZAINBZAI.ColNo, dr(i).Item("PORA_ZAI_NB_ZAI").ToString()) '実予在庫個数(現在)
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.DESTNM.ColNo, dr(i).Item("DEST_NM").ToString()) '届先名
                .SetCellValue(rowCnt, LME040G.sprDetailsDef.KEYNO.ColNo, dr(i).Item("KEY_NO").ToString()) 'キー番号
            Next

            .ResumeLayout(True)

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
    ''' セルのプロパティを設定(Date)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoDate(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        '日付スタイルを設定
        StyleInfoDate = LMSpreadUtility.GetDateTimeCell(spr, lock)

        '配置左に設定 
        StyleInfoDate.HorizontalAlignment = CellHorizontalAlignment.Left

        Return StyleInfoDate

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(MIX)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextMix(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, length, lock)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(TextHankaku)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextHankaku(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, length, lock)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(TextHankaku)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextMixImeOff(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, length, lock)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数10桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum10(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, lock, 0, , ",")

    End Function

#End Region

#Region "コントロール取得"

    ''' <summary>
    ''' フォームに検索した結果(Text)を取得
    ''' </summary>
    ''' <param name="objNm">コントロール名</param>
    ''' <returns>LMImTextBox</returns>
    ''' <remarks></remarks>
    Friend Function GetTextControl(ByVal objNm As String) As Win.InputMan.LMImTextBox

        Return DirectCast(Me._frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox)

    End Function

#End Region

#End Region

#End Region

End Class
