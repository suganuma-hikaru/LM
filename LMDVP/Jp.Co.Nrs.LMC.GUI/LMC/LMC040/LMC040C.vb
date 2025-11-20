' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC040C : 在庫引当
'  作  成  者       :  kishi
' ==========================================================================

''' <summary>
''' LMC040定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC040C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    'Public Const TABLE_NM_IN As String = "LMC040IN"
    'Public Const TABLE_NM_OUT As String = "LMC040OUT"
    Public Const TABLE_NM_OUTZAI As String = "LMC040OUT_ZAI"
    Public Const TABLE_NM_OUTZAI_PALETTE As String = "LMC040OUT_ZAI_PALETTE"
    Public Const TABLE_NM_INCUSTFURI As String = "LMC040IN_CUST_FURI"
    Public Const TABLE_NM_OUTCUSTFURI As String = "LMC040OUT_CUST_FURI"
    Public Const TABLE_NM_ZAI As String = "LMC040_ZAI"
    '20170623 在庫不整合修正Add
    Public Const TABLE_NM_OUT_S As String = "LMC040_OUTKA_S"
    '20170623 在庫不整合修正End
    'START YANAI 20110914 一括引当対応
    Public Const TABLE_NM_IN2 As String = "LMC040IN2"
    'END YANAI 20110914 一括引当対応

    '2013.02.13 要望番号1824 START
    Public Const TABLE_NM_M_GOODS_DETAILS As String = "LMC040_M_GOODS_DETAILS"
    '2013.02.13 要望番号1824 END

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    '再描画
    Public Const NEW_MODE As String = "0"   '描画
    Public Const RE_MODE As String = "1"    '再描画

    'プログラムID
    Public Const PGID_LMC010 As String = "LMC010"
    Public Const PGID_LMC020 As String = "LMC020"
    Public Const PGID_LMD010 As String = "LMD010"

    '入目の桁数
    Public Const IRIME_MAX_NUM As String = "999999.999"
    Public Const IRIME_MIN_NUM As String = "0.001"

    '0固定値
    Public Const PLUS_ZERO As String = "0.000"
    Public Const MINUS_ZERO As String = "-0.000"

    'イベント種別
    Public Enum EventShubetsu As Integer

        TANINUSI = 0    '他荷主
        KENSAKU         '検索
        SENTAKU         '選択
        CLOSE           '閉じる
        DOUBLE_CLICK    'ダブルクリック
        CAL_KONSU       '梱数・端数変更
        CAL_SURYO       '数量変更
        CAL_IRIME       '入目変更
        CHANGE_SPREAD   'スプレッド変更
        SYOKI           '初期処理
        OPT_KOSU        '出荷単位（個数）
        OPT_SURYO       '出荷単位（数量）
        OPT_KOWAKE      '出荷単位（小分け）
        OPT_SAMPLE      '出荷単位（サンプル）

    End Enum

    '(2013.03.12)要望番号1229 -- START --
    'START YANAI 要望番号780
    ''スプレッド列数
    'Public Const SprZaikoColCount As Integer = 36   '描画
    ''スプレッド列数
    'Public Const SprZaikoColCount As Integer = 37   '描画
    'END YANAI 要望番号780
    'スプレッド列数
    'Public Const SprZaikoColCount As Integer = 38   '描画

    'Public Const SprZaikoColCount As Integer = 41   '描画

    ' 030509【LMS】BYK商品6桁対応
    'スプレッド列数
    Public Const SprZaikoColCount As Integer = 42   '描画

    '(2013.03.12)要望番号1229 --  END  --

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
        KEEP_GOODS_NM
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
        'START YANAI 要望番号776
        INKO_DATE_ZAI
        'END YANAI 要望番号776
        'START YANAI 要望番号780
        INKA_DATE_KANRI_KB
        'END YANAI 要望番号780
        '(2013.03.12)要望番号1229 -- START --
        INKA_STATE_KB
        '(2013.03.12)要望番号1229 --  END  --
        GOODS_CD_NRS
        SHOBO_CD
        SHOBO_NM
        LAST

    End Enum

    ' DataSet部列インデックス用列挙対(LMC040OUT_ZAI)
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
        GOODS_KANRI_NO
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
        'START YANAI 要望番号499
        CUST_CD_L_GOODS
        CUST_CD_M_GOODS
        'END YANAI 要望番号499
        'START YANAI 要望番号780
        INKA_DATE2
        INKA_DATE_KANRI_KB
        'END YANAI 要望番号780
        '(2013.03.12)要望番号1229 -- START --
        SPD_KB_FLG
        INKA_STATE_KB
        '(2013.03.12)要望番号1229 --  END  --
        IRIME_UT
        OUTKA_NO_L
        SHOBO_CD
        SHOBO_NM
        OUTKA_HASU_SAGYO_KB_1
        OUTKA_HASU_SAGYO_KB_2
        OUTKA_HASU_SAGYO_KB_3
        BYK_KEEP_GOODS_CD
        KEEP_GOODS_NM
        IS_BYK_KEEP_GOODS_CD
    End Enum

    ''' <summary>
    ''' TabIndex
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        FRMSHUKATANI = 0
        FRMCNT
        FRMAMOUNT
        SPRZAIKO
        TXTSERIALNO
        TXTRSVNO
        TXTLOTNO
        NUMIRIME
        OPTCNT
        OPTAMT
        OPTKOWAKE
        OPTSAMPLE
        NUMSYUKKAKOSU
        NUMSYUKKAHASU
        NUMSYUKKASOUAMT

    End Enum


#If True Then ' フィルメニッヒ セミEDI対応  20160912 added inoue

    ''' <summary>
    ''' 引当画面特殊並替え用フラグ(荷主明細Ｍ.SUB_KB=02)
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Class SORT_FLG

        ''' <summary>
        ''' 東レ
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TORAY As String = "01"

        ''' <summary>
        ''' 棟優先(日医工)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NICHIIKO As String = "02"

        ''' <summary>
        ''' 引当可能個数より置場優先(群馬DIC)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DIC As String = "03"

        ''' <summary>
        ''' LOT番号優先(ジェイティ物流)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const JT As String = "04"

        ''' <summary>
        ''' 割り当て優先(ロンザ)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const LONZA As String = "05"

        ''' <summary>
        ''' 商品状態設定品優先(フィルメニッヒ用)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const FIRMENICH As String = "06"

        ''' <summary>
        ''' ダメージ品・ロケーションが入ってないデータは対象外（自動引当時）(アクサルタ用)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const AXALTA As String = "07"
    End Class

#End If

    ''' <summary>
    ''' 区分マスタ 区分分類コード
    ''' </summary>
    Public Class KbnConst
        ''' <summary>
        ''' BYKキープ品 
        ''' </summary>
        Public Const BYK_KEEP_GOODS_CD As String = "B039"
    End Class

End Class
