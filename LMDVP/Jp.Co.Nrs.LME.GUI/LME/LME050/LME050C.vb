' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME050  : 作業個数引当
'  作  成  者       :  YANAI
' ==========================================================================

''' <summary>
''' LME050定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LME050C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LME050IN"
    Public Const TABLE_NM_OUT As String = "LME050OUT"
    Public Const TABLE_NM_OUTZAI As String = "LME050OUT_ZAI"

    '再描画
    Public Const NEW_MODE As String = "0"   '描画
    Public Const RE_MODE As String = "1"    '再描画

    '入目の桁数
    Public Const IRIME_MAX_NUM As String = "999999.999"
    Public Const IRIME_MIN_NUM As String = "0.001"

    '0固定値
    Public Const PLUS_ZERO As String = "0.000"
    Public Const MINUS_ZERO As String = "-0.000"

    'イベント種別
    Public Enum EventShubetsu As Integer

        KENSAKU = 0     '検索
        SENTAKU         '選択
        CLOSE           '閉じる
        DOUBLE_CLICK    'ダブルクリック
        CAL_KONSU       '梱数・端数変更
        CAL_SURYO       '数量変更
        CHANGE_SPREAD   'スプレッド変更
        SYOKI           '初期処理
        'START YANAI 要望番号1424 支払処理
        CAL_IRIME       '入目変更
        'END YANAI 要望番号1424 支払処理

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprZaikoColumnIndex

        DEF = 0
        LOT_NO
        IRIME
        TOU_NO
        SHITSU_NO
        ZONE_CD
        LOCA
        HIKI_CNT
        HIKI_AMT
        HIKI_KANO_CNT
        HIKI_KANO_UT
        HIKI_KANO_AMT
        NAKAMI
        GAIKAN
        CUST_STATUS
        REMARK
        INKO_DATE
        OFB_KBN
        SPD_KBN_S
        YOYAKU_NO
        SERIAL_NO
        GOODS_CRT_DATE
        ALLOC_PRIORITY
        REMARK_OUT
        LT_DATE
        DEST_NM
        PORA_ZAI_NB
        TAX_KB
        HIKIATE_ALERT_NM
        ZAI_REC_NO
        NRS_BR_CD
        WH_CD
        OUTKA_PLAN_DATE
        SMPL_FLG
        GOODS_CD_NRS_FROM
        INKO_DATE_ZAI
        INKA_DATE_KANRI_KB
        LAST

    End Enum

    ' DataSet部列インデックス用列挙対(LME050OUT_ZAI)
    Public Enum DsOutZaiColumnIndex

        NRS_BR_CD = 0
        ZAI_REC_NO
        WH_CD
        TOU_NO
        SITU_NO
        ZONE_CD
        LOCA
        LOT_NO
        CUST_CD_L
        CUST_CD_M
        GOODS_CD_NRS
        INKA_NO_L
        INKA_NO_M
        INKA_NO_S
        ALLOC_PRIORITY
        RSV_NO
        SERIAL_NO
        HOKAN_YN
        TAX_KB
        GOODS_COND_KB_1
        GOODS_COND_KB_2
        GOODS_COND_KB_3
        OFB_KB
        SPD_KB
        REMARK_OUT
        PORA_ZAI_NB
        ALCTD_NB
        ALLOC_CAN_NB
        IRIME
        PORA_ZAI_QT
        ALCTD_QT
        ALLOC_CAN_QT
        INKO_DATE
        INKO_PLAN_DATE
        ZERO_FLAG
        LT_DATE
        GOODS_CRT_DATE
        DEST_CD_P
        REMARK
        SMPL_FLAG
        GOODS_COND_NM_1
        GOODS_COND_NM_2
        GOODS_COND_NM_3
        ALLOC_PRIORITY_NM
        OFB_KB_NM
        SPD_KB_NM
        HIKIATE_ALERT_NM
        CONSUME_PERIOD_DATE
        TAX_KB_NM
        NB_UT_NM
        IRIME_UT_NM
        OUTKA_PLAN_DATE
        BUYER_ORD_NO_DTL
        DEST_NM
        GOODS_CD_CUST
        GOODS_NM_1
        OUTKA_ATT
        SEARCH_KEY_1
        UNSO_ONDO_KB
        PKG_UT
        STD_IRIME_NB
        STD_WT_KGS
        TARE_YN
        HIKIATE_ALERT_YN
        PKG_NB
        STD_IRIME_UT
        NB_UT
        INKA_DATE
        CUST_CD_S
        CUST_CD_SS
        IDO_DATE
        HOKAN_STR_DATE
        COA_YN
        OUTKA_KAKO_SAGYO_KB_1
        OUTKA_KAKO_SAGYO_KB_2
        OUTKA_KAKO_SAGYO_KB_3
        OUTKA_KAKO_SAGYO_KB_4
        OUTKA_KAKO_SAGYO_KB_5
        SIZE_KB
        SYS_UPD_DATE
        SYS_UPD_TIME
        GOODS_CD_NRS_FROM
        CUST_CD_L_GOODS
        CUST_CD_M_GOODS
        INKA_DATE2
        INKA_DATE_KANRI_KB

    End Enum

    ''' <summary>
    ''' TabIndex
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        FRMCNT = 0
        FRMAMOUNT
        SPRZAIKO
        TXTSERIALNO
        TXTRSVNO
        TXTLOTNO
        NUMIRIME
        NUMSYUKKAKOSU
        NUMSYUKKAHASU
        NUMSYUKKASOUAMT

    End Enum

End Class
