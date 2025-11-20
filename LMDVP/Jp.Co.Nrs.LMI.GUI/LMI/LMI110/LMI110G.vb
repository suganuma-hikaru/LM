' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : 特殊荷主機能
'  プログラムID     :  LMI110G : 日医工製品マスタ登録
'  作  成  者       :  [寺川徹]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports FarPoint.Win.Spread

''' <summary>
''' LMI110Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMI110G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI110F

    ''' <summary>
    ''' Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _H As LMI110H

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI110F, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

    End Sub

#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFunctionKey()

        Dim unLock As Boolean = True
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
            .F9ButtonName = LMIControlC.FUNCTION_KENSAKU
            .F10ButtonName = LMIControlC.FUNCTION_POP
            .F11ButtonName = LMIControlC.FUNCTION_GOODSM
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = True
            .F10ButtonEnabled = True
            .F11ButtonEnabled = True
            .F12ButtonEnabled = True

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
            .cmbEigyo.TabIndex = LMI110C.CtlTabIndex.CMB_EIGYO
            .txtCustCdL.TabIndex = LMI110C.CtlTabIndex.TXT_CUST_CD_L
            .txtCustCdM.TabIndex = LMI110C.CtlTabIndex.TXT_CST_CD_M
            .txtCustNm.TabIndex = LMI110C.CtlTabIndex.TXT_CUST_NM
            .imdEdiDateFrom.TabIndex = LMI110C.CtlTabIndex.IMD_EDI_DATE_FROM
            .imdEdiDateTo.TabIndex = LMI110C.CtlTabIndex.IMD_EDI_DATE_TO
            .chkNotImport.TabIndex = LMI110C.CtlTabIndex.CHK_NOT_IMPORT
            .chkImport.TabIndex = LMI110C.CtlTabIndex.CHK_IMPORT
            .sprNichikoGoods.TabIndex = LMI110C.CtlTabIndex.SPR_NICHIKO_GOODS
            .txtSerchGoodsCd.TabIndex = LMI110C.CtlTabIndex.TXT_SERCH_GOODS_CD
            .txtSerchGoodsNm.TabIndex = LMI110C.CtlTabIndex.TXT_SERCH_GOODS_NM
            .txtGoodsCustCdL.TabIndex = LMI110C.CtlTabIndex.TXT_GOODS_CUST_CD_L
            .txtGoodsCustNmL.TabIndex = LMI110C.CtlTabIndex.TXT_GOODS_CUST_NM_L
            .txtGoodsCustCdM.TabIndex = LMI110C.CtlTabIndex.TXT_GOODS_CUST_CD_M
            .txtGoodsCustNmM.TabIndex = LMI110C.CtlTabIndex.TXT_GOODS_CUST_NM_M
            .txtGoodsCustCdS.TabIndex = LMI110C.CtlTabIndex.TXT_GOODS_CUST_CD_S
            .txtGoodsCustNmS.TabIndex = LMI110C.CtlTabIndex.TXT_GOODS_CUST_NM_S
            .txtGoodsCustCdSS.TabIndex = LMI110C.CtlTabIndex.TXT_GOODS_CUST_CD_SS
            .txtGoodsCustNmSS.TabIndex = LMI110C.CtlTabIndex.TXT_GOODS_CUST_NM_SS
            .txtGoodsNm1.TabIndex = LMI110C.CtlTabIndex.TXT_GOODS_NM_1
            .txtGoodsNm2.TabIndex = LMI110C.CtlTabIndex.TXT_GOODS_NM_2
            .txtGoodsKey.TabIndex = LMI110C.CtlTabIndex.TXT_GOODS_KEY
            .txtGoodsCd.TabIndex = LMI110C.CtlTabIndex.TXT_GOODS_CD
            .cmbTareYn.TabIndex = LMI110C.CtlTabIndex.CMB_TARE_YN
            .txtUpGroupCd1.TabIndex = LMI110C.CtlTabIndex.TXT_UP_GROUP_CD_1
            .cmbLotCtlKb.TabIndex = LMI110C.CtlTabIndex.CMB_LOT_CTL_KB
            .cmbSpNhsYn.TabIndex = LMI110C.CtlTabIndex.CMB_SP_NHS_YN
            .cmbCoaYn.TabIndex = LMI110C.CtlTabIndex.CMB_COA_YN
            .cmbCoaYn.TabIndex = LMI110C.CtlTabIndex.CMB_HIKIATE_ALERT_YN
            .cmbSkyuMeiYn.TabIndex = LMI110C.CtlTabIndex.CMB_SKYU_MEI_YN
        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .imdEdiDateFrom.Focus()

        End With

    End Sub

    ''' <summary>
    ''' 日医工の情報をフォームにセットする
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCustData()


        With Me._Frm
            '日医工の荷主コードをセット
            .txtCustCdL.TextValue = "00171"
            .txtCustCdM.TextValue = "00"

        End With

    End Sub


    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMI110F, ByVal sysDate As String)

        '反映済チェックボックスにチェック
        frm.chkNotImport.Checked = True

        'EDI取込日に対してシステム日付をセット
        frm.imdEdiDateFrom.TextValue = sysDate
        frm.imdEdiDateTo.TextValue = sysDate


    End Sub

    ''' <summary>
    ''' ファンクションキーの制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()
        With _Frm
            Call Me.SetFunctionKey()

        End With

    End Sub


#Region "内部メソッド"
    ''' <summary>
    '''営業所コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetcmbNrsBrCd()

        Me._Frm.cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()
        'Me._Frm.cmbEigyo.SelectedValue = LMI110C.NRS_BR_CD
    End Sub


#End Region

#Region "文字色"
    ''' <summary>
    ''' スプレッドの文字色設定
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadColor(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprNichikoGoods
        Dim dr As DataRow
        Dim lngcnt As Integer = dt.Rows.Count()

        With spr

            If lngcnt = 0 Then
                Exit Sub
            End If

            For i As Integer = 1 To lngcnt
                dr = dt.Rows(i - 1)

                If dr.Item("STATUS").ToString.Equals(LMI110C.MGOODS_DOUBLE) Then
                    'ステータスが"重複"の場合：赤
                    .ActiveSheet.Rows(i).ForeColor = Color.Red
                End If
            Next

        End With


    End Sub
#End Region


#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(商品Spread)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprNichikoGoods

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.DEF, " ", 20, True)
        Public Shared STATUS As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.STATUS, "ステータス", 75, True)
        Public Shared TORIKOMI_KBN As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.TORIKOMI_KBN, "取込区分", 75, True)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.SYS_ENT_DATE, "EDI取込日", 80, True)
        Public Shared GOODS_CD As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.GOODS_CD, "製品コード", 80, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.GOODS_NM, "製品名(漢字)", 200, True)
        Public Shared GOODS_NM_KANA As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.GOODS_NM_KANA, "製品名(カナ)", 100, True)
        Public Shared GOODS_KIKAKU As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.GOODS_KIKAKU, "製品規格(漢字)", 130, True)
        Public Shared GOODS_KIKAKU_KANA As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.GOODS_KIKAKU_KANA, "製品規格(カナ)", 100, True)
        Public Shared JAN_CD As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.JAN_CD, "ＪＡＮコード", 100, True)
        Public Shared KANRI_KB_NM As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.KANRI_KB_NM, "管理区分", 100, True)
        Public Shared ONDO_KB_NM As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.ONDO_KB_NM, "保管温度区分", 100, True)
        Public Shared YUKO_MONTH As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.YUKO_MONTH, "有効期間" & vbCrLf & "月数", 80, True)
        Public Shared PKG_NB As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.PKG_NB, "入数", 80, True)
        Public Shared NB_UT As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.NB_UT, "個数単位", 115, True)
        Public Shared STD_IRIME_NB As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.STD_IRIME_NB, "入目", 90, True)
        Public Shared STD_IRIME_UT As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.STD_IRIME_UT, "入目単位", 115, True)
        Public Shared NB_FORM_LENGTH As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.NB_FORM_LENGTH, "個装縦寸", 80, True)
        Public Shared NB_FORM_WIDTH As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.NB_FORM_WIDTH, "個装横寸", 80, True)
        Public Shared NB_FORM_HEIGHT As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.NB_FORM_HEIGHT, "個装高寸", 80, True)
        Public Shared NB_WT_GS As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.NB_WT_GS, "個装重量", 80, True)
        Public Shared PKG_FORM_LENGTH As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.PKG_FORM_LENGTH, "梱包縦寸", 80, True)
        Public Shared PKG_FORM_WIDTH As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.PKG_FORM_WIDTH, "梱包横寸", 80, True)
        Public Shared PKG_FORM_HEIGHT As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.PKG_FORM_HEIGHT, "梱包高寸", 80, True)
        Public Shared PKG_WT_GS As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.PKG_WT_GS, "梱包重量", 800, True)
        Public Shared TEKIYO_DATE As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.TEKIYO_DATE, "適用年月日", 80, True)


        '**** 隠し列 ****
        Public Shared GOODS_NM_RYAKU As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.GOODS_NM_RYAKU, "", 50, False)
        Public Shared ITF_CD As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.ITF_CD, "", 50, False)
        Public Shared SIIRE_CD As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.SIIRE_CD, "", 50, False)
        Public Shared NB_ML As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.NB_ML, "", 50, False)
        Public Shared PKG_ML As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.PKG_ML, "", 50, False)
        Public Shared PLT_PER_PKG_UT As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.PLT_PER_PKG_UT, "", 50, False)
        Public Shared SURFACE_PKG_NB As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.SURFACE_PKG_NB, "", 50, False)
        Public Shared SURFACE_NUM_ROW As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.SURFACE_NUM_ROW, "", 50, False)
        Public Shared M_GOODS_ONDO_KB As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.M_GOODS_ONDO_KB, "", 50, False)
        Public Shared M_GOODS_STD_IRIME_NB As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.M_GOODS_STD_IRIME_NB, "", 50, False)
        Public Shared M_GOODS_PKG_NB As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.M_GOODS_PKG_NB, "", 50, False)
        Public Shared M_GOODS_CNT As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.M_GOODS_CNT, "", 50, False)
        Public Shared M_SEIHIN_SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.M_SEIHIN_SYS_UPD_DATE, "", 50, False)
        Public Shared M_SEIHIN_SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.M_SEIHIN_SYS_UPD_TIME, "", 50, False)
        Public Shared M_GOODS_SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.M_GOODS_SYS_UPD_DATE, "", 50, False)
        Public Shared M_GOODS_SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.M_GOODS_SYS_UPD_TIME, "", 50, False)
        Public Shared M_SEIHIN_SYS_DEL_FLAG As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.M_SEIHIN_SYS_DEL_FLAG, "", 50, False)
        Public Shared M_GOODS_SYS_DEL_FLAG As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.M_GOODS_SYS_DEL_FLAG, "", 50, False)
        Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.GOODS_CD_NRS, "", 50, False)
        Public Shared ONDO_KB As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.ONDO_KB, "", 50, False)
        Public Shared KANRI_KB As SpreadColProperty = New SpreadColProperty(LMI110C.SprGoodsColumnIndex.KANRI_KB, "", 50, False)


    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        '商品Spreadの初期化処理
        Call Me.InitGoodsSpread()

    End Sub

    ''' <summary>
    ''' 検索結果を商品Spreadに表示
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprNichikoGoods

        With spr

            .SuspendLayout()

            'データ挿入
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lblL As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim numIrime As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.999, True, 3, , ",")
            Dim numIrimeUnLock As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.999, False, 3, , ",")
            Dim num As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999999, True, 0, , ",")
            Dim numUnLock As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999999, False, 0, , ",")
            Dim numKg As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999999.999, True, 3, , ",")
            Dim nbUt As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_K002, True)
            Dim nbUtUnLock As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_K002, False)
            Dim stdIrimeUt As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_I001, True)
            Dim stdIrimeUtUnLock As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_I001, False)


            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprNichikoGoods.DEF.ColNo, def)
                .SetCellStyle(i, sprNichikoGoods.STATUS.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.TORIKOMI_KBN.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.SYS_ENT_DATE.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.GOODS_CD.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.GOODS_NM.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.GOODS_NM_KANA.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.GOODS_KIKAKU.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.GOODS_KIKAKU_KANA.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.JAN_CD.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.KANRI_KB_NM.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.ONDO_KB_NM.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.YUKO_MONTH.ColNo, num)
                .SetCellStyle(i, sprNichikoGoods.PKG_NB.ColNo, num)
                '未反映データの場合、入数、個数単位、入目、入目単位は手入力可能
                If dr.Item("STATUS").ToString = LMI110C.MGOODS_NEW Then
                    .SetCellStyle(i, sprNichikoGoods.PKG_NB.ColNo, numUnLock)
                    .SetCellStyle(i, sprNichikoGoods.NB_UT.ColNo, nbUtUnLock)
                    .SetCellStyle(i, sprNichikoGoods.STD_IRIME_NB.ColNo, numIrimeUnLock)
                    .SetCellStyle(i, sprNichikoGoods.STD_IRIME_UT.ColNo, stdIrimeUtUnLock)
                Else
                    .SetCellStyle(i, sprNichikoGoods.PKG_NB.ColNo, num)
                    .SetCellStyle(i, sprNichikoGoods.NB_UT.ColNo, nbUt)
                    .SetCellStyle(i, sprNichikoGoods.STD_IRIME_NB.ColNo, numIrime)
                    .SetCellStyle(i, sprNichikoGoods.STD_IRIME_UT.ColNo, stdIrimeUt)
                End If
                .SetCellStyle(i, sprNichikoGoods.NB_FORM_LENGTH.ColNo, num)
                .SetCellStyle(i, sprNichikoGoods.NB_FORM_WIDTH.ColNo, num)
                .SetCellStyle(i, sprNichikoGoods.NB_FORM_HEIGHT.ColNo, num)
                .SetCellStyle(i, sprNichikoGoods.NB_WT_GS.ColNo, numKg)
                .SetCellStyle(i, sprNichikoGoods.PKG_FORM_LENGTH.ColNo, num)
                .SetCellStyle(i, sprNichikoGoods.PKG_FORM_WIDTH.ColNo, num)
                .SetCellStyle(i, sprNichikoGoods.PKG_FORM_HEIGHT.ColNo, num)
                .SetCellStyle(i, sprNichikoGoods.PKG_WT_GS.ColNo, numKg)
                .SetCellStyle(i, sprNichikoGoods.TEKIYO_DATE.ColNo, lblL)

                '**** 隠し列 ****
                .SetCellStyle(i, sprNichikoGoods.GOODS_NM_RYAKU.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.ITF_CD.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.SIIRE_CD.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.NB_ML.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.PKG_ML.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.PLT_PER_PKG_UT.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.SURFACE_PKG_NB.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.SURFACE_NUM_ROW.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.M_GOODS_ONDO_KB.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.M_GOODS_STD_IRIME_NB.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.M_GOODS_PKG_NB.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.M_GOODS_CNT.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.M_SEIHIN_SYS_UPD_DATE.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.M_SEIHIN_SYS_UPD_TIME.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.M_GOODS_SYS_UPD_DATE.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.M_GOODS_SYS_UPD_TIME.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.M_SEIHIN_SYS_DEL_FLAG.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.M_GOODS_SYS_DEL_FLAG.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.GOODS_CD_NRS.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.ONDO_KB.ColNo, lblL)
                .SetCellStyle(i, sprNichikoGoods.KANRI_KB.ColNo, lblL)


                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprNichikoGoods.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprNichikoGoods.STATUS.ColNo, dr.Item("STATUS").ToString())
                .SetCellValue(i, sprNichikoGoods.TORIKOMI_KBN.ColNo, dr.Item("TORIKOMI_KBN").ToString())
                .SetCellValue(i, sprNichikoGoods.SYS_ENT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SYS_ENT_DATE").ToString()))
                .SetCellValue(i, sprNichikoGoods.GOODS_CD.ColNo, dr.Item("GOODS_CD_NIK").ToString())
                .SetCellValue(i, sprNichikoGoods.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(i, sprNichikoGoods.GOODS_NM_KANA.ColNo, dr.Item("GOODS_NM_KANA").ToString())
                .SetCellValue(i, sprNichikoGoods.GOODS_KIKAKU.ColNo, dr.Item("GOODS_KIKAKU").ToString())
                .SetCellValue(i, sprNichikoGoods.GOODS_KIKAKU_KANA.ColNo, dr.Item("GOODS_KIKAKU_KANA").ToString())
                .SetCellValue(i, sprNichikoGoods.JAN_CD.ColNo, dr.Item("JAN_CD").ToString())
                .SetCellValue(i, sprNichikoGoods.KANRI_KB_NM.ColNo, dr.Item("KANRI_KB_NM").ToString())
                .SetCellValue(i, sprNichikoGoods.ONDO_KB_NM.ColNo, dr.Item("ONDO_KB_NM").ToString())
                .SetCellValue(i, sprNichikoGoods.YUKO_MONTH.ColNo, dr.Item("YUKO_MONTH").ToString())
                .SetCellValue(i, sprNichikoGoods.PKG_NB.ColNo, dr.Item("PKG_NB").ToString())
                .SetCellValue(i, sprNichikoGoods.NB_UT.ColNo, dr.Item("NB_UT").ToString())
                .SetCellValue(i, sprNichikoGoods.STD_IRIME_NB.ColNo, dr.Item("STD_IRIME_NB").ToString())
                .SetCellValue(i, sprNichikoGoods.STD_IRIME_UT.ColNo, dr.Item("STD_IRIME_UT").ToString())
                .SetCellValue(i, sprNichikoGoods.NB_FORM_LENGTH.ColNo, dr.Item("NB_FORM_LENGTH").ToString())
                .SetCellValue(i, sprNichikoGoods.NB_FORM_WIDTH.ColNo, dr.Item("NB_FORM_WIDTH").ToString())
                .SetCellValue(i, sprNichikoGoods.NB_FORM_HEIGHT.ColNo, dr.Item("NB_FORM_HIGHT").ToString())
                .SetCellValue(i, sprNichikoGoods.NB_WT_GS.ColNo, dr.Item("NB_WT_GS").ToString())
                .SetCellValue(i, sprNichikoGoods.PKG_FORM_LENGTH.ColNo, dr.Item("PKG_FORM_LENGTH").ToString())
                .SetCellValue(i, sprNichikoGoods.PKG_FORM_WIDTH.ColNo, dr.Item("PKG_FORM_WIDTH").ToString())
                .SetCellValue(i, sprNichikoGoods.PKG_FORM_HEIGHT.ColNo, dr.Item("PKG_FORM_HIGHT").ToString())
                .SetCellValue(i, sprNichikoGoods.PKG_WT_GS.ColNo, dr.Item("PKG_WT_GS").ToString())
                .SetCellValue(i, sprNichikoGoods.TEKIYO_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("TEKIYO_DATE").ToString()))

                '**** 隠し列 ****
                .SetCellValue(i, sprNichikoGoods.GOODS_NM_RYAKU.ColNo, dr.Item("GOODS_NM_RYAKU").ToString())
                .SetCellValue(i, sprNichikoGoods.ITF_CD.ColNo, dr.Item("ITF_CD").ToString())
                .SetCellValue(i, sprNichikoGoods.SIIRE_CD.ColNo, dr.Item("SIIRE_CD").ToString())
                .SetCellValue(i, sprNichikoGoods.NB_ML.ColNo, dr.Item("NB_ML").ToString())
                .SetCellValue(i, sprNichikoGoods.PKG_ML.ColNo, dr.Item("PKG_ML").ToString())
                .SetCellValue(i, sprNichikoGoods.PLT_PER_PKG_UT.ColNo, dr.Item("PLT_PER_PKG_UT").ToString())
                .SetCellValue(i, sprNichikoGoods.SURFACE_PKG_NB.ColNo, dr.Item("SURFACE_PKG_NB").ToString())
                .SetCellValue(i, sprNichikoGoods.SURFACE_NUM_ROW.ColNo, dr.Item("SURFACE_NUM_ROW").ToString())
                .SetCellValue(i, sprNichikoGoods.M_GOODS_ONDO_KB.ColNo, dr.Item("M_GOODS_ONDO_KB").ToString())
                .SetCellValue(i, sprNichikoGoods.M_GOODS_STD_IRIME_NB.ColNo, dr.Item("M_GOODS_STD_IRIME_NB").ToString())
                .SetCellValue(i, sprNichikoGoods.M_GOODS_PKG_NB.ColNo, dr.Item("M_GOODS_PKG_NB").ToString())
                .SetCellValue(i, sprNichikoGoods.M_GOODS_CNT.ColNo, dr.Item("M_GOODS_CNT").ToString())
                .SetCellValue(i, sprNichikoGoods.M_SEIHIN_SYS_UPD_DATE.ColNo, dr.Item("M_SEIHIN_SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprNichikoGoods.M_SEIHIN_SYS_UPD_TIME.ColNo, dr.Item("M_SEIHIN_SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprNichikoGoods.M_GOODS_SYS_UPD_DATE.ColNo, dr.Item("M_GOODS_SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprNichikoGoods.M_GOODS_SYS_UPD_TIME.ColNo, dr.Item("M_GOODS_SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprNichikoGoods.M_SEIHIN_SYS_DEL_FLAG.ColNo, dr.Item("M_SEIHIN_SYS_DEL_FLAG").ToString())
                .SetCellValue(i, sprNichikoGoods.M_GOODS_SYS_DEL_FLAG.ColNo, dr.Item("M_GOODS_SYS_DEL_FLAG").ToString())
                .SetCellValue(i, sprNichikoGoods.GOODS_CD_NRS.ColNo, dr.Item("GOODS_CD_NRS").ToString())
                .SetCellValue(i, sprNichikoGoods.ONDO_KB.ColNo, dr.Item("ONDO_KB").ToString())
                .SetCellValue(i, sprNichikoGoods.KANRI_KB.ColNo, dr.Item("KANRI_KB").ToString())



            Next

            .ResumeLayout(True)

        End With

    End Sub



#Region "内部メソッド"

    ''' <summary>
    ''' 商品スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitGoodsSpread()

        '商品Spreadの初期値設定
        Dim sprGoods As LMSpread = Me._Frm.sprNichikoGoods

        With sprGoods

            ''スプレッドの行をクリア
            .CrearSpread()

            ''列数設定
            .ActiveSheet.ColumnCount = 47

            ''スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New sprNichikoGoods)

            '列固定位置を設定します。(ex.入目単位で固定)
            .ActiveSheet.FrozenColumnCount = sprNichikoGoods.GOODS_CD.ColNo + 1
            '.ActiveSheet.FrozenColumnCount = sprNichikoGoods.STD_IRIME_UT.ColNo + 1

            '検索行の設定を行う
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprGoods)

            '**** 表示列 ****
            .SetCellStyle(0, sprNichikoGoods.DEF.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.STATUS.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.TORIKOMI_KBN.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.SYS_ENT_DATE.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.GOODS_CD.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.HAN_NUM_ALPHA, 9, False))
            .SetCellStyle(0, sprNichikoGoods.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.ALL_MIX, 50, False))
            .SetCellStyle(0, sprNichikoGoods.GOODS_NM_KANA.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.ALL_HANKAKU, 50, False))
            .SetCellStyle(0, sprNichikoGoods.GOODS_KIKAKU.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.ALL_MIX, 30, False))
            .SetCellStyle(0, sprNichikoGoods.GOODS_KIKAKU_KANA.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.ALL_HANKAKU, 30, False))
            .SetCellStyle(0, sprNichikoGoods.JAN_CD.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.HAN_NUM_ALPHA, 13, False))
            .SetCellStyle(0, sprNichikoGoods.KANRI_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprGoods, LMKbnConst.KBN_I005, False))
            .SetCellStyle(0, sprNichikoGoods.ONDO_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprGoods, LMKbnConst.KBN_I006, False))
            .SetCellStyle(0, sprNichikoGoods.YUKO_MONTH.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.PKG_NB.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.NB_UT.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.STD_IRIME_NB.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.STD_IRIME_UT.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.NB_FORM_LENGTH.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.NB_FORM_WIDTH.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.NB_FORM_HEIGHT.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.NB_WT_GS.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.PKG_FORM_LENGTH.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.PKG_FORM_WIDTH.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.PKG_FORM_HEIGHT.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.PKG_WT_GS.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.TEKIYO_DATE.ColNo, lbl)

            '**** 隠し列 ****
            .SetCellStyle(0, sprNichikoGoods.GOODS_NM_RYAKU.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.ITF_CD.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.SIIRE_CD.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.NB_ML.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.PKG_ML.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.PLT_PER_PKG_UT.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.SURFACE_PKG_NB.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.SURFACE_NUM_ROW.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.M_GOODS_ONDO_KB.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.M_GOODS_STD_IRIME_NB.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.M_GOODS_PKG_NB.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.M_GOODS_CNT.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.M_SEIHIN_SYS_UPD_DATE.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.M_SEIHIN_SYS_UPD_TIME.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.M_GOODS_SYS_UPD_DATE.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.M_GOODS_SYS_UPD_TIME.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.M_SEIHIN_SYS_DEL_FLAG.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.M_GOODS_SYS_DEL_FLAG.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.GOODS_CD_NRS.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.ONDO_KB.ColNo, lbl)
            .SetCellStyle(0, sprNichikoGoods.KANRI_KB.ColNo, lbl)

        End With

    End Sub


#End Region

#End Region

#End Region

#End Region

End Class
