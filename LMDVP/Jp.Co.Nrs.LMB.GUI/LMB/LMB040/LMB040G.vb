' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB040G : 入荷検品選択
'  作  成  者       :  小林
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMB040Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB040G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB040F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBConG As LMBControlG

    Friend objSprlDef As Object = Nothing            'ADD 2018/11/06 依頼番号 : 002669   【LMS】ハネウェル管理_次回定検日項目追加
    Friend sprDetailDef As sprDetailDefault         'ADD 2018/11/06 依頼番号 : 002669   【LMS】ハネウェル管理_次回定検日項目追加


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB040F)

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

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.POP_L)

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
            .F10ButtonName = String.Empty
            .F11ButtonName = "選　択"
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
            .F9ButtonEnabled = True
            .F10ButtonEnabled = False
            .F11ButtonEnabled = True
            .F12ButtonEnabled = True

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

    Friend Sub SetInitForm(ByVal frm As LMB040F, ByVal prmDs As DataSet)

        Dim custNm As String = String.Empty
        Dim strSqlCust As String = String.Empty

        With prmDs.Tables(LMB040C.TABLE_NM_IN)
            frm.cmbNrsBrCd.SelectedValue = .Rows(0)("NRS_BR_CD").ToString()
            frm.lblCustCDL.TextValue = .Rows(0)("CUST_CD_L").ToString()
            frm.lblCustCDM.TextValue = .Rows(0)("CUST_CD_M").ToString()

            Dim getCustDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST). _
            Select(String.Concat("SYS_DEL_FLG = '0' AND CUST_CD_L = '", .Rows(0).Item("CUST_CD_L").ToString(), _
                "' AND CUST_CD_M = '", .Rows(0).Item("CUST_CD_M").ToString(), "' AND CUST_CD_S = '00' AND CUST_CD_SS = '00'"))

            If getCustDr.Length() > 0 Then
                frm.lblCustNmLM.TextValue = String.Concat(getCustDr(0).Item("CUST_NM_L").ToString(), getCustDr(0).Item("CUST_NM_M").ToString())
            End If

            '20174.04.22(初期表示でチェックオン) 黎 追加 --ST--
            frm.chkMishori.Checked = True
            '20174.04.22(初期表示でチェックオン) 黎 追加 --ED--

        End With


    End Sub

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .imdSysEntDate.TabIndex = LMB040C.CtlTabIndex_MAIN.SYS_ENT_DATE
            '追加開始 2015.05.15 要望番号:2292
            .imdSysEntDateTo.TabIndex = LMB040C.CtlTabIndex_MAIN.SYS_ENT_DATE_TO
            '追加終了 2015.05.15 要望番号:2292
            .txtSAGYO_USER_CD.TabIndex = LMB040C.CtlTabIndex_MAIN.SAGYO_USER_CD
            .chkMishori.TabIndex = LMB040C.CtlTabIndex_MAIN.CHK_MISHORI
            .sprDetail.TabIndex = LMB040C.CtlTabIndex_MAIN.SPRDETAIL
        End With

    End Sub

    ''' <summary>
    ''' 画面ヘッダー部値設定
    ''' </summary>
    ''' <param name="drow">荷主キャッシュから取得したデータロウ配列</param>
    ''' <remarks></remarks>
    Friend Sub HeaderDataSet(ByVal drow As DataRow)

        With _Frm

            .lblCustCdL.TextValue = drow("CUST_CD_L").ToString()
            .lblCustNmLM.TextValue = String.Concat(drow("CUST_NM_L").ToString(), drow("CUST_CD_M").ToString())
            .txtSAGYO_USER_CD.TextValue = String.Empty
            .lblSAGYO_USER_NM.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 画面ヘッダー部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbNrsBrCd, lock)
            Me.SetLockControl(.lblCustCdL, lock)
            Me.SetLockControl(.lblCustNmLM, lock)
            Me.SetLockControl(.lblCustCdM, lock)
            Me.SetLockControl(.lblSAGYO_USER_NM, lock)
            Me.SetLockControl(.chkMishori, lock)
            .sprDetail.Enabled = Not lock

        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()

    End Sub
    ''' <summary>
    ''' 検索条件の荷主名のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearTantoNM()

        With Me._Frm

            If String.IsNullOrEmpty(.txtSAGYO_USER_CD.TextValue) = True Then
                .lblSAGYO_USER_NM.TextValue = String.Empty
            End If

        End With

    End Sub
#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDefault
#If False Then      'UPD 2018/11/06 依頼番号 : 002669   【LMS】ハネウェル管理_次回定検日項目追加 スプレット列移動

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared MST_EXISTS_MARK As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.MST_EXISTS_MARK, " ", 20, True)
        '2013.07.19 追加START
        Public Shared KENPIN_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.KENPIN_DATE, "検品日", 75, True)
        '2013.07.19 追加END
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.GOODS_NM, "正式名", 150, True)
        Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.GOODS_CD_CUST, "商品コード", 80, True)
        Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.IRIME, "入目", 60, True)
        Public Shared IRIME_UT As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.IRIME_UT, "単位", 30, True)
        Public Shared PKG_UT As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.PKG_UT, "荷姿", 30, True)
        Public Shared PKG_NB As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.PKG_NB, "入数", 50, True)
        Public Shared NB_UT_1 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.NB_UT_1, "単位", 30, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.LOT_NO, "ロットNO", 80, True)
        '2014.02.17 WIT対応START
        Public Shared SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.SERIAL_NO, "シリアル№", 80, True)
        '2014.02.17 WIT対応END

#If True Then ' JT物流入荷検品対応 20160726 added inoue
        Public Shared GOODS_CRT_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.GOODS_CRT_DATE, "製造日", 80, True)
        Public Shared CHK_TANI As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.CHK_TANI, "検品単位", 0, False)
#End If
#If True Then ' フィルメニッヒ入荷検品対応 20170311 added inoue
        Public Shared LT_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.LT_DATE, "賞味有効期限", 85, True)
#End If
        '2013.11.25 WIT対応START
        'Public Shared KENPIN_NO As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.KENPIN_NO, "入荷検品NO", 90, True)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.WH_CD, "倉庫コード", 10, False)
        Public Shared INPUT_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.INPUT_DATE, "入力日", 10, False)
        Public Shared SEQ As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.SEQ, "SEQ", 10, False)

        Public Shared TORIKOMI_FLG_NM As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.TORIKOMI_FLG_NM, "取込状況", 60, True)
        Public Shared GOODS_KANRI_NO As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.GOODS_KANRI_NO, "商品管理番号", 10, False)
        '2013.11.25 WIT対応END
        Public Shared OKIBA As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.OKIBA, "置場", 100, True)
        Public Shared KENPIN_KAKUTEI_TTL_NB As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.KENPIN_KAKUTEI_TTL_NB, "個数", 60, True)
        Public Shared NB_UT_2 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.NB_UT_2, "単位", 30, True)
        '2013.07.19 追加START
        Public Shared USER_NM As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.USER_NM, "作業者", 100, True)
        '2013.07.19 追加END
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.NRS_BR_CD, "営業所コード", 10, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.CUST_CD_L, "荷主コードL", 10, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.CUST_CD_M, "荷主コードM", 10, False)
        Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.GOODS_CD_NRS, "商品キー", 10, False)
        Public Shared TOU_NO As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.TOU_NO, "棟番号", 10, False)
        Public Shared SITU_NO As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.SITU_NO, "室番号", 10, False)
        Public Shared ZONE_CD As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.ZONE_CD, "ZONEコード", 10, False)
        Public Shared LOCA As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.LOCA, "ロケーション", 10, False)
        Public Shared ONDO_KB As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.ONDO_KB, "温度", 10, False)
        Public Shared ONDO_STR_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.ONDO_STR_DATE, "保管温度管理期間From", 10, False)
        Public Shared ONDO_END_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.ONDO_END_DATE, "保管温度管理期間To", 10, False)
        Public Shared STD_WT_KGS As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.STD_WT_KGS, "標準重量", 10, False)
        Public Shared KONSU As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.KONSU, "梱数", 10, False)
        Public Shared HASU As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.HASU, "端数", 10, False)
        Public Shared BETU_WT As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.BETU_WT, "個別重量(KGS)", 10, False)
        Public Shared INKA_KAKO_SAGYO_KB_1 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.INKA_KAKO_SAGYO_KB_1, "入荷作業１", 10, False)
        Public Shared INKA_KAKO_SAGYO_KB_2 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.INKA_KAKO_SAGYO_KB_2, "入荷作業２", 10, False)
        Public Shared INKA_KAKO_SAGYO_KB_3 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.INKA_KAKO_SAGYO_KB_3, "入荷作業３", 10, False)
        Public Shared INKA_KAKO_SAGYO_KB_4 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.INKA_KAKO_SAGYO_KB_4, "入荷作業４", 10, False)
        Public Shared INKA_KAKO_SAGYO_KB_5 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.INKA_KAKO_SAGYO_KB_5, "入荷作業５", 10, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.SYS_UPD_DATE, "更新日（排他用）", 10, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.SYS_UPD_TIME, "更新時間（排他用）", 10, False)
        Public Shared MST_EXISTS_KBN As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.MST_EXISTS_KBN, "M登録区分）", 10, False)
        '2013.07.18 追加START
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.CUST_CD_S, "荷主コードS", 10, False)
        Public Shared CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.CUST_CD_SS, "荷主コードSS", 10, False)
        Public Shared TARE_YN As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.TARE_YN, "風袋加算フラグ", 10, False)
        Public Shared LOT_CTL_KB As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.LOT_CTL_KB, "ロット管理レベル", 10, False)
        Public Shared LT_DATE_CTL_KB As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.LT_DATE_CTL_KB, "賞味期限管理の有無", 10, False)
        Public Shared CRT_DATE_CTL_KB As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.CRT_DATE_CTL_KB, "製造日管理の有無", 10, False)
        '2013.07.18 追加END
        Public Shared NEXT_TEST_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.NEXT_TEST_DATE, "次回定検日", 80, True)        'ADD 2018/11/06 依頼番号 : 002669   【LMS】ハネウェル管理_次回定検日項目追加

#Else


        'スプレッド(タイトル列)の設定
        Public DEF As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.DEF, " ", 20, True)
        Public MST_EXISTS_MARK As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.MST_EXISTS_MARK, " ", 20, True)
        '2013.07.19 追加START
        Public KENPIN_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.KENPIN_DATE, "検品日", 75, True)
        '2013.07.19 追加END
        Public GOODS_NM As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.GOODS_NM, "正式名", 150, True)
        Public GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.GOODS_CD_CUST, "商品コード", 80, True)
        Public IRIME As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.IRIME, "入目", 60, True)
        Public IRIME_UT As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.IRIME_UT, "単位", 30, True)
        Public PKG_UT As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.PKG_UT, "荷姿", 30, True)
        Public PKG_NB As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.PKG_NB, "入数", 50, True)
        Public NB_UT_1 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.NB_UT_1, "単位", 30, True)
        Public LOT_NO As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.LOT_NO, "ロットNO", 80, True)
        '2014.02.17 WIT対応START
        Public SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.SERIAL_NO, "シリアル№", 80, True)
        '2014.02.17 WIT対応END

#If True Then ' JT物流入荷検品対応 20160726 added inoue
        Public GOODS_CRT_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.GOODS_CRT_DATE, "製造日", 80, True)
        Public CHK_TANI As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.CHK_TANI, "検品単位", 0, False)
#End If
#If True Then ' フィルメニッヒ入荷検品対応 20170311 added inoue
        Public LT_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.LT_DATE, "賞味有効期限", 85, True)
#End If
        '2013.11.25 WIT対応START
        'Public  KENPIN_NO As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.KENPIN_NO, "入荷検品NO", 90, True)
        Public WH_CD As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.WH_CD, "倉庫コード", 10, False)
        Public INPUT_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.INPUT_DATE, "入力日", 10, False)
        Public SEQ As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.SEQ, "SEQ", 10, False)

        Public TORIKOMI_FLG_NM As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.TORIKOMI_FLG_NM, "取込状況", 60, True)
        Public GOODS_KANRI_NO As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.GOODS_KANRI_NO, "商品管理番号", 10, False)
        '2013.11.25 WIT対応END
        Public OKIBA As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.OKIBA, "置場", 100, True)
        Public KENPIN_KAKUTEI_TTL_NB As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.KENPIN_KAKUTEI_TTL_NB, "個数", 60, True)
        Public NB_UT_2 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.NB_UT_2, "単位", 30, True)
        '2013.07.19 追加START
        Public USER_NM As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.USER_NM, "作業者", 100, True)
        '2013.07.19 追加END
        Public NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.NRS_BR_CD, "営業所コード", 10, False)
        Public CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.CUST_CD_L, "荷主コードL", 10, False)
        Public CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.CUST_CD_M, "荷主コードM", 10, False)
        Public GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.GOODS_CD_NRS, "商品キー", 10, False)
        Public TOU_NO As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.TOU_NO, "棟番号", 10, False)
        Public SITU_NO As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.SITU_NO, "室番号", 10, False)
        Public ZONE_CD As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.ZONE_CD, "ZONEコード", 10, False)
        Public LOCA As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.LOCA, "ロケーション", 10, False)
        Public ONDO_KB As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.ONDO_KB, "温度", 10, False)
        Public ONDO_STR_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.ONDO_STR_DATE, "保管温度管理期間From", 10, False)
        Public ONDO_END_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.ONDO_END_DATE, "保管温度管理期間To", 10, False)
        Public STD_WT_KGS As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.STD_WT_KGS, "標準重量", 10, False)
        Public KONSU As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.KONSU, "梱数", 10, False)
        Public HASU As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.HASU, "端数", 10, False)
        Public BETU_WT As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.BETU_WT, "個別重量(KGS)", 10, False)
        Public INKA_KAKO_SAGYO_KB_1 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.INKA_KAKO_SAGYO_KB_1, "入荷作業１", 10, False)
        Public INKA_KAKO_SAGYO_KB_2 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.INKA_KAKO_SAGYO_KB_2, "入荷作業２", 10, False)
        Public INKA_KAKO_SAGYO_KB_3 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.INKA_KAKO_SAGYO_KB_3, "入荷作業３", 10, False)
        Public INKA_KAKO_SAGYO_KB_4 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.INKA_KAKO_SAGYO_KB_4, "入荷作業４", 10, False)
        Public INKA_KAKO_SAGYO_KB_5 As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.INKA_KAKO_SAGYO_KB_5, "入荷作業５", 10, False)
        Public SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.SYS_UPD_DATE, "更新日（排他用）", 10, False)
        Public SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.SYS_UPD_TIME, "更新時間（排他用）", 10, False)
        Public MST_EXISTS_KBN As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.MST_EXISTS_KBN, "M登録区分）", 10, False)
        '2013.07.18 追加START
        Public CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.CUST_CD_S, "荷主コードS", 10, False)
        Public CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.CUST_CD_SS, "荷主コードSS", 10, False)
        Public TARE_YN As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.TARE_YN, "風袋加算フラグ", 10, False)
        Public LOT_CTL_KB As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.LOT_CTL_KB, "ロット管理レベル", 10, False)
        Public LT_DATE_CTL_KB As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.LT_DATE_CTL_KB, "賞味期限管理の有無", 10, False)
        Public CRT_DATE_CTL_KB As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.CRT_DATE_CTL_KB, "製造日管理の有無", 10, False)
        '2013.07.18 追加END
        Public NEXT_TEST_DATE As SpreadColProperty = New SpreadColProperty(LMB040C.SprColumnIndex.NEXT_TEST_DATE, "次回定検日", 80, True)        'ADD 2018/11/06 依頼番号 : 002669   【LMS】ハネウェル管理_次回定検日項目追加

#End If
    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(ByVal drow As DataRow)

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
#If False Then ' JT物流入荷検品対応 20160726 changed inoue
            '2013.07.19 修正START
            '.sprDetail.Sheets(0).ColumnCount = 37
            '.sprDetail.Sheets(0).ColumnCount = 49
            '2014.02.17 修正START
            .sprDetail.Sheets(0).ColumnCount = 50
            '2014.02.17 修正END
            '2013.07.19 修正END
#Else
            .sprDetail.Sheets(0).ColumnCount = LMB040C.SprColumnIndex.INDEX_COUNT
#End If
            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New sprDetailDef)
#If False Then      'UPD 2018/11/06 依頼番号 : 002669   【LMS】ハネウェル管理_次回定検日項目追加

            .sprDetail.SetColProperty(New sprDetailDef, False)
#Else
            objSprlDef = New sprDetailDefault
            .sprDetail.SetColProperty(objSprlDef, True)
            sprDetailDef = DirectCast(objSprlDef, sprDetailDefault)
#End If

            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.商品名で固定)
            .sprDetail.Sheets(0).FrozenColumnCount = sprDetailDef.GOODS_NM.ColNo + 1

            '列設定

            .sprDetail.SetCellStyle(0, sprDetailDef.DEF.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.MST_EXISTS_MARK.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            '2013.07.19 追加START
            .sprDetail.SetCellStyle(0, sprDetailDef.KENPIN_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            '2013.07.19 追加END
            .sprDetail.SetCellStyle(0, sprDetailDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 60, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.GOODS_CD_CUST.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 20, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.IRIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.IRIME_UT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.PKG_UT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.PKG_NB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.NB_UT_1.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 40, False))
            '2014.02.17 追加START
            .sprDetail.SetCellStyle(0, sprDetailDef.SERIAL_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 40, False))
            '2014.02.17 追加END
#If True Then ' JT物流入荷検品対応 20160726 added inoue
            .sprDetail.SetCellStyle(0, sprDetailDef.GOODS_CRT_DATE.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 8, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.CHK_TANI.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
#End If


#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
            .sprDetail.SetCellStyle(0, sprDetailDef.LT_DATE.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 8, False))
#End If

            '2013.11.25 WIT対応START
            '.sprDetail.SetCellStyle(0, LMB040G.sprDetailDef.KENPIN_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA_L, 9, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.WH_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.INPUT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.SEQ.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.TORIKOMI_FLG_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Center))
            .sprDetail.SetCellStyle(0, sprDetailDef.GOODS_KANRI_NO.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            '2013.11.25 WIT対応END
            .sprDetail.SetCellStyle(0, sprDetailDef.OKIBA.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 19, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.KENPIN_KAKUTEI_TTL_NB.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, 0, 999999, True))
            .sprDetail.SetCellStyle(0, sprDetailDef.NB_UT_2.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            '2013.07.19 追加START
            .sprDetail.SetCellStyle(0, sprDetailDef.USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            '2013.07.19 追加END
            .sprDetail.SetCellStyle(0, sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_CD_M.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.GOODS_CD_NRS.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.TOU_NO.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.SITU_NO.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.ZONE_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.LOCA.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.ONDO_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.ONDO_STR_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.ONDO_END_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.STD_WT_KGS.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.KONSU.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.HASU.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.BETU_WT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.INKA_KAKO_SAGYO_KB_1.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.INKA_KAKO_SAGYO_KB_2.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.INKA_KAKO_SAGYO_KB_3.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.INKA_KAKO_SAGYO_KB_4.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.INKA_KAKO_SAGYO_KB_5.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.MST_EXISTS_KBN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            '2013.07.18 追加START
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_CD_S.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_CD_SS.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.TARE_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.LOT_CTL_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.LT_DATE_CTL_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.CRT_DATE_CTL_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            '2013.07.18 追加END
            .sprDetail.SetCellStyle(0, sprDetailDef.NEXT_TEST_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))           'ADD 2018/11/06 依頼番号 : 002669   【LMS】ハネウェル管理_次回定検日項目追加
        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As DataSet = New DataSet()

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            'キーボード操作でチェックボックスＯＮ
            '.KeyboardCheckBoxOn = True

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim rLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
#If True Then ' JT物流入荷検品対応 20160726 added inoue
            Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr, True)
            sDate.HorizontalAlignment = CellHorizontalAlignment.Left
#End If
            Dim tNumCell As New FarPoint.Win.Spread.CellType.NumberCellType()
            tNumCell.ShowSeparator = True   'セパレータ表示する(おまけ)
            tNumCell.DecimalPlaces = 3      '小数点以下３桁
            tNumCell.FixedPoint = True      '小数点以下を固定表示(必ず0.000と表示する)

            Dim dRow As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dRow = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.MST_EXISTS_MARK.ColNo, sLabel)
                '2013.07.19  追加START
                .SetCellStyle(i, sprDetailDef.KENPIN_DATE.ColNo, sLabel)
                '2013.07.19  追加END
                .SetCellStyle(i, sprDetailDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GOODS_CD_CUST.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.IRIME.ColNo, rLabel)
                .ActiveSheet.Cells(i, sprDetailDef.IRIME.ColNo).CellType = tNumCell
                .SetCellStyle(i, sprDetailDef.IRIME_UT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.PKG_UT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.PKG_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 99999999999999, True, 0, True, ","))
                .SetCellStyle(i, sprDetailDef.NB_UT_1.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.LOT_NO.ColNo, sLabel)
                '2014.02.17 WIT対応START
                .SetCellStyle(i, sprDetailDef.SERIAL_NO.ColNo, sLabel)
                '2014.02.17 WIT対応END
#If True Then ' JT物流入荷検品対応 20160726 added inoue
                .SetCellStyle(i, sprDetailDef.GOODS_CRT_DATE.ColNo, sDate)
                .SetCellStyle(i, sprDetailDef.CHK_TANI.ColNo, sLabel)
#End If

#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
                .SetCellStyle(i, sprDetailDef.LT_DATE.ColNo, sDate)
#End If
                '2013.11.25 WIT対応START
                '.SetCellStyle(i, sprDetailDef.KENPIN_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.WH_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INPUT_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SEQ.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TORIKOMI_FLG_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GOODS_KANRI_NO.ColNo, sLabel)
                '2013.11.25 WIT対応END
                .SetCellStyle(i, sprDetailDef.OKIBA.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.KENPIN_KAKUTEI_TTL_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 99999999999999, True, 0, True, ","))
                .SetCellStyle(i, sprDetailDef.NB_UT_2.ColNo, sLabel)
                '2013.07.19 追加START
                .SetCellStyle(i, sprDetailDef.USER_NM.ColNo, sLabel)
                '2013.07.19 追加END
                .SetCellStyle(i, sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GOODS_CD_NRS.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TOU_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SITU_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ZONE_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.LOCA.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ONDO_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ONDO_STR_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ONDO_END_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.STD_WT_KGS.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.KONSU.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.HASU.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.BETU_WT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INKA_KAKO_SAGYO_KB_1.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INKA_KAKO_SAGYO_KB_2.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INKA_KAKO_SAGYO_KB_3.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INKA_KAKO_SAGYO_KB_4.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INKA_KAKO_SAGYO_KB_5.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.MST_EXISTS_KBN.ColNo, sLabel)
                '2013.07.18 追加START
                .SetCellStyle(i, sprDetailDef.CUST_CD_S.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_CD_SS.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TARE_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.LOT_CTL_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.LT_DATE_CTL_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CRT_DATE_CTL_KB.ColNo, sLabel)
                '2013.07.18 追加END
                .SetCellStyle(i, sprDetailDef.NEXT_TEST_DATE.ColNo, sLabel)         'ADD 2018/11/06 依頼番号 : 002669   【LMS】ハネウェル管理_次回定検日項目追加

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.MST_EXISTS_MARK.ColNo, dRow.Item("MST_EXISTS_MARK").ToString())
                '2013.07.19 追加START
                .SetCellValue(i, sprDetailDef.KENPIN_DATE.ColNo, dRow.Item("KENPIN_DATE").ToString())
                '2013.07.19 追加END
                .SetCellValue(i, sprDetailDef.GOODS_NM.ColNo, dRow.Item("GOODS_NM").ToString())
                .SetCellValue(i, sprDetailDef.GOODS_CD_CUST.ColNo, dRow.Item("GOODS_CD_CUST").ToString())

#If False Then ' フィルメニッヒ入荷検品対応 20170310 changed by inoue 
                .ActiveSheet.SetValue(i, sprDetailDef.IRIME.ColNo, Convert.ToDouble(dRow.Item("STD_IRIME_NB")), False)
#Else
                If (Convert.ToDouble(dRow.Item("STD_IRIME_NB")) > 0) Then
                    .ActiveSheet.SetValue(i, sprDetailDef.IRIME.ColNo, Convert.ToDouble(dRow.Item("STD_IRIME_NB")), False)
                End If
#End If
                .SetCellValue(i, sprDetailDef.IRIME_UT.ColNo, dRow.Item("STD_IRIME_UT").ToString())
                .SetCellValue(i, sprDetailDef.PKG_UT.ColNo, dRow.Item("PKG_UT").ToString())
                .SetCellValue(i, sprDetailDef.PKG_NB.ColNo, dRow.Item("PKG_NB").ToString())
                .SetCellValue(i, sprDetailDef.NB_UT_1.ColNo, dRow.Item("NB_UT").ToString())
                .SetCellValue(i, sprDetailDef.LOT_NO.ColNo, dRow.Item("LOT_NO").ToString())
                '2014.02.17 WIT対応START
                .SetCellValue(i, sprDetailDef.SERIAL_NO.ColNo, dRow.Item("SERIAL_NO").ToString())
                '2014.02.17 WIT対応END
#If True Then ' JT物流入荷検品対応 20160726 added inoue
                If (String.IsNullOrEmpty(TryCast(dRow.Item("GOODS_CRT_DATE"), String)) = False) Then
                    .SetCellValue(i, sprDetailDef.GOODS_CRT_DATE.ColNo, DateFormatUtility.EditSlash(dRow.Item("GOODS_CRT_DATE").ToString()))
                End If
                .SetCellValue(i, sprDetailDef.CHK_TANI.ColNo, dRow.Item("CHK_TANI").ToString())
#End If

#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
                If (String.IsNullOrEmpty(TryCast(dRow.Item("LT_DATE"), String)) = False) Then
                    .SetCellValue(i, sprDetailDef.LT_DATE.ColNo, DateFormatUtility.EditSlash(dRow.Item("LT_DATE").ToString()))
                End If
#End If
                '2013.11.25 WIT対応START
                '.SetCellValue(i, sprDetailDef.KENPIN_NO.ColNo, dRow.Item("KENPIN_NO").ToString())
                .SetCellValue(i, sprDetailDef.WH_CD.ColNo, dRow.Item("WH_CD").ToString())
                .SetCellValue(i, sprDetailDef.INPUT_DATE.ColNo, dRow.Item("INPUT_DATE").ToString())
                .SetCellValue(i, sprDetailDef.SEQ.ColNo, dRow.Item("SEQ").ToString())
                .SetCellValue(i, sprDetailDef.TORIKOMI_FLG_NM.ColNo, dRow.Item("TORIKOMI_FLG_NM").ToString())
                .SetCellValue(i, sprDetailDef.GOODS_KANRI_NO.ColNo, dRow.Item("GOODS_KANRI_NO").ToString())
                '2013.11.25 WIT対応END
                .SetCellValue(i, sprDetailDef.OKIBA.ColNo, Me.SetOkibaFormat(dRow))
                .SetCellValue(i, sprDetailDef.KENPIN_KAKUTEI_TTL_NB.ColNo, dRow.Item("KENPIN_KAKUTEI_TTL_NB").ToString())
                .SetCellValue(i, sprDetailDef.NB_UT_2.ColNo, dRow.Item("NB_UT").ToString())
                '2013.07.19 追加START
                .SetCellValue(i, sprDetailDef.USER_NM.ColNo, dRow.Item("USER_NM").ToString())
                '2013.07.19 追加END
                .SetCellValue(i, sprDetailDef.NRS_BR_CD.ColNo, dRow.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_L.ColNo, dRow.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_M.ColNo, dRow.Item("CUST_CD_M").ToString())
                .SetCellValue(i, sprDetailDef.GOODS_CD_NRS.ColNo, dRow.Item("GOODS_CD_NRS").ToString())
                .SetCellValue(i, sprDetailDef.TOU_NO.ColNo, dRow.Item("TOU_NO").ToString())
                .SetCellValue(i, sprDetailDef.SITU_NO.ColNo, dRow.Item("SITU_NO").ToString())
                .SetCellValue(i, sprDetailDef.ZONE_CD.ColNo, dRow.Item("ZONE_CD").ToString())
                .SetCellValue(i, sprDetailDef.LOCA.ColNo, dRow.Item("LOCA").ToString())
                .SetCellValue(i, sprDetailDef.ONDO_KB.ColNo, dRow.Item("ONDO_KB").ToString())
                .SetCellValue(i, sprDetailDef.ONDO_STR_DATE.ColNo, dRow.Item("ONDO_STR_DATE").ToString())
                .SetCellValue(i, sprDetailDef.ONDO_END_DATE.ColNo, dRow.Item("ONDO_END_DATE").ToString())
                .SetCellValue(i, sprDetailDef.STD_WT_KGS.ColNo, dRow.Item("STD_WT_KGS").ToString())
                .SetCellValue(i, sprDetailDef.KONSU.ColNo, dRow.Item("KONSU").ToString())
                .SetCellValue(i, sprDetailDef.HASU.ColNo, dRow.Item("HASU").ToString())
                .SetCellValue(i, sprDetailDef.BETU_WT.ColNo, dRow.Item("BETU_WT").ToString())
                .SetCellValue(i, sprDetailDef.INKA_KAKO_SAGYO_KB_1.ColNo, dRow.Item("INKA_KAKO_SAGYO_KB_1").ToString())
                .SetCellValue(i, sprDetailDef.INKA_KAKO_SAGYO_KB_2.ColNo, dRow.Item("INKA_KAKO_SAGYO_KB_2").ToString())
                .SetCellValue(i, sprDetailDef.INKA_KAKO_SAGYO_KB_3.ColNo, dRow.Item("INKA_KAKO_SAGYO_KB_3").ToString())
                .SetCellValue(i, sprDetailDef.INKA_KAKO_SAGYO_KB_4.ColNo, dRow.Item("INKA_KAKO_SAGYO_KB_4").ToString())
                .SetCellValue(i, sprDetailDef.INKA_KAKO_SAGYO_KB_5.ColNo, dRow.Item("INKA_KAKO_SAGYO_KB_5").ToString())
                .SetCellValue(i, sprDetailDef.SYS_UPD_DATE.ColNo, dRow.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprDetailDef.SYS_UPD_TIME.ColNo, dRow.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprDetailDef.MST_EXISTS_KBN.ColNo, dRow.Item("MST_EXISTS_KBN").ToString())
                '2013.07.18 追加START
                .SetCellValue(i, sprDetailDef.CUST_CD_S.ColNo, dRow.Item("CUST_CD_S").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_SS.ColNo, dRow.Item("CUST_CD_SS").ToString())
                .SetCellValue(i, sprDetailDef.TARE_YN.ColNo, dRow.Item("TARE_YN").ToString())
                .SetCellValue(i, sprDetailDef.LOT_CTL_KB.ColNo, dRow.Item("LOT_CTL_KB").ToString())
                .SetCellValue(i, sprDetailDef.LT_DATE_CTL_KB.ColNo, dRow.Item("LT_DATE_CTL_KB").ToString())
                .SetCellValue(i, sprDetailDef.CRT_DATE_CTL_KB.ColNo, dRow.Item("CRT_DATE_CTL_KB").ToString())
                '2013.07.18 追加END
                .SetCellValue(i, sprDetailDef.NEXT_TEST_DATE.ColNo, dRow.Item("NEXT_TEST_DATE").ToString())    'ADD 2018/11/06 依頼番号 : 002669   【LMS】ハネウェル管理_次回定検日項目追加

            Next

            .ResumeLayout(True)

        End With


    End Sub

    ''' <summary>
    ''' 置き場のフォーマットを設定
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetOkibaFormat(ByVal row As DataRow) As String
        Dim kugiriStr As String = "-"
        Return String.Concat(row.Item("TOU_NO").ToString().Trim, kugiriStr, row.Item("SITU_NO").ToString().Trim, kugiriStr, row.Item("ZONE_CD").ToString().Trim, kugiriStr, row.Item("LOCA").ToString().Trim)

    End Function

#End Region 'Spread

#Region "部品"

    ''' <summary>
    ''' ロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub SetLockControl(ByVal ctl As Control, Optional ByVal lockFlg As Boolean = False)

        Dim arr As ArrayList = New ArrayList()
        Call Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
        Dim lblArr As ArrayList = New ArrayList()

        'エディット系コントロールのロック
        For Each arrCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In arr

            'テキストボックスの場合、ラベル項目であったら処理対象外とする
            If TypeOf arrCtl Is Win.InputMan.LMImTextBox = True Then

                If DirectCast(arrCtl, Win.InputMan.LMImTextBox).Name.Substring(0, 3).Equals("lbl") = True Then
                    lblArr.Add(arrCtl)
                End If

            End If

            'ロック処理/ロック解除処理を行う
            arrCtl.ReadOnlyStatus = lockFlg
        Next

        'ラベル項目をロック
        For Each lblCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In lblArr
            lblCtl.ReadOnlyStatus = True
        Next

        'ボタンのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMButton)(arr, ctl)
        For Each arrCtl As Win.LMButton In arr
            'ロック処理/ロック解除処理を行う
            Call Me.LockButton(arrCtl, lockFlg)
        Next

        'チェックボックスのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMCheckBox)(arr, ctl)
        For Each arrCtl As Win.LMCheckBox In arr

            'ロック処理/ロック解除処理を行う
            Call Me.LockCheckBox(arrCtl, lockFlg)

        Next

    End Sub


    ''' <summary>
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Private Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

        If TypeOf ownControl Is T _
            OrElse ownControl.GetType.IsSubclassOf(GetType(T)) Then

            '指定されたクラスかその継承クラス
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(チェックボックス)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockCheckBox(ByVal ctl As Win.LMCheckBox, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.EnableStatus = enabledFlg

    End Sub


    ''' <summary>
    ''' 引数のコントロールをロックする(ボタン)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockButton(ByVal ctl As Win.LMButton, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

#End Region

#End Region

End Class
