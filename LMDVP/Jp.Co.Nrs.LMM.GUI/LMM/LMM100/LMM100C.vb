' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM100C : 商品マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Option Strict On
Option Explicit On

''' <summary>
''' LMM100定数定義クラス
''' </summary>
''' <remarks></remarks>
Friend Class LMM100C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMM100IN"
    Friend Const TABLE_NM_GOODS As String = "LMM100OUT"
    Friend Const TABLE_NM_GOODS_DTL As String = "LMM100_GOODS_DETAILS"
    Friend Const TABLE_NM_EDI_CUST As String = "LMM100EDI_CUST"
    Friend Const TABLE_NM_SAVE_CHK As String = "LMM100SAVE_CHK"     'ADD 2020/02/26 010140【LMS】フィルメ誤出荷類似_EDI取込、出荷登録共に個別荷主機能使用(高度化和地)
    Friend Const TABLE_NM_CUST_ZAIKO As String = "LMM100CUST_ZAIKO"

    ''' <summary>
    ''' 引当単位区分【区分マスタ：H012】
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const HIKIATE_TANI_KOSU As String = "01"
    Friend Const HIKIATE_TANI_SURYO As String = "02"

    ''' <summary>
    ''' 温度管理区分【区分マスタ：O002】
    ''' </summary>
    ''' <remarks>01:常温、02:定温</remarks>
    ''' 
    Friend Const ONDO_KANRI_JOON As String = "01"
    Friend Const ONDO_KANRI_TEON As String = "02"

    ''' <summary>
    ''' 運送温度区分【区分マスタ：U006】
    ''' </summary>
    ''' <remarks>01:常温、02:定温</remarks>
    Friend Const UNSO_KANRI_TEON As String = "10"
    Friend Const UNSO_KANRI_HOREI As String = "20"
    Friend Const UNSO_KANRI_NASHI As String = "90"

    ''' <summary>
    ''' 商品印刷種別【区分マスタ：S058】
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const PRINT_TEON_JOON As String = "01"
    Friend Const PRINT_TEON As String = "02"
    Friend Const PRINT_JOON As String = "03"
    Friend Const PRINT_ICHIRAN As String = "04"

    ''' <summary>
    ''' 消防危険品区分【区分マスタ：S126】
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const SHOBO_KIKEN_KIKEN As String = "01"
    Friend Const SHOBO_KIKEN_SHITEIKANEN As String = "02"
    Friend Const SHOBO_KIKEN_HIGAITO As String = "03"

    ''' <summary>
    ''' 危険品区分【区分マスタ：K008】
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const KIKEN_IPPAN As String = "01"
    Friend Const KIKEN_SHOBO As String = "02"
    Friend Const KIKEN_SHOBODOKUGEKI As String = "03"
    Friend Const KIKEN_DOKUGEKI As String = "04"

    '2015.11.02 tusnehira add Start
    '英語化対応
    Public Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Public Const MESSEGE_LANGUAGE_ENGLISH As String = "1"
    '2015.11.02 tusnehira add End

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum EventShubetsu As Integer

        SHINKI = 0
        HENSHU
        FUKUSHA
        SAKUJO_HUKKATU
        TANKA_IKKATU_HENKO
        NINUSHI_IKKATU_HENKO
        KENSAKU
        MASTEROPEN
        HOZON
        TOJIRU
        DOUBLE_CLICK
        PRINT
        ADD_ROW
        DEL_ROW
        ENTER
        TANINUSI            '2015.10.02 他荷主対応ADD
        KIKEN_KAKUNIN
        VOLUME_IKKATU

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        CMB_PRINT = 0

        NUM_WIDTH_BULK
        NUM_DEPTH_BULK
        NUM_HEIGHT_BULK
        NUM_ACTUAL_VOLUME_BULK
        NUM_OCCUPY_VOLUME_BULK

        BTN_PRINT
        SPR_GOODS
        TAB_GOODS_M

        TPG_GOODS
        CMB_BR
        TXT_CUST_CD_L
        LBL_CUST_NM_L
        TXT_CUST_CD_M
        LBL_CUST_NM_M
        TXT_CUST_CD_S
        LBL_CUST_NM_S
        TXT_CUST_CD_SS
        LBL_CUST_NM_SS
        TXT_NISOUNIN
        LBL_NISOUNIN
        TXT_GOODS_NM_1
        TXT_GOODS_NM_2
        TXT_GOODS_NM_3
        LBL_GOODS_KEY
        TXT_GOODS_CD
        PNL_NYUKASAGYO_KBN
        TXT_NYUKASAGYO_KBN_1
        TXT_NYUKASAGYO_KBN_2
        TXT_NYUKASAGYO_KBN_3
        TXT_NYUKASAGYO_KBN_4
        TXT_NYUKASAGYO_KBN_5
        PNL_SHUKKASAGYO_KBN
        TXT_SHUKKASAGYO_KBN_1
        TXT_SHUKKASAGYO_KBN_2
        TXT_SHUKKASAGYO_KBN_3
        TXT_SHUKKASAGYO_KBN_4
        TXT_SHUKKASAGYO_KBN_5
        OPT_KOSU
        OPT_SURYO
        CMB_KOSUTANI
        NUM_HUTAI_JURYO
        CMB_HUTAI_JURYO_KASAN
        NUM_IRISU
        LBL_NISUGATA
        CMB_HOSOTANI
        TXT_KONPOSAGYO_CD
        LBL_KONPOSAGYO_CD
        TXT_SHUKKACYUI_JIKO
        CMB_HOKANKBN_HOKAN
        CMB_HOKANKBN_UNSO
        IMD_ONDOKANRI_START_HOKAN
        IMD_ONDOKANRI_START_UNSO
        IMD_ONDOKANRI_END_HOKAN
        IMD_ONDOKANRI_END_UNSO
        NUM_ONDOKANRI_MAX
        NUM_ONDOKANRI_MIN
        NUM_HYOJYUN_IRIME
        CMB_HYOJYUN_IRIME_TANI
        NUM_HYOJYUN_JURYO
        NUM_HIZYU
        NUM_HYOJYUN_YOSEKI
        TXT_CUST_KANJOCD_1
        TXT_CUST_KANJOCD_2
        TXT_CUST_CATEGORY_1
        TXT_CUST_CATEGORY_2
        NUM_PALETTO_SU
        CMB_LOT_KANRI_LEVEL
        CMB_KITEI_HORYUHIN_KBN
        CMB_KITAKU_KAKAKU_TANI_KBN
        NUM_KITAKU_GOODS_TANKA
        NUM_NIHUDA_INSTU_MAISU
        NUM_INNER_PKG_NB
        TXT_GROUP_CD
        NUM_SHOHIKIGEN_KINSHIBI
        CMB_NOHINSHO
        CMB_BUNSEKIHYO
        CMB_SHOMIKEGENKANRI
        CMB_SEIZOBIKANRI
        CMB_HIKIATE_CHUIHIN
        CMB_SEIKYUMEISAI_SHUTURYOKU
        CMB_UNSO_HOKEN            'ADD 2018/07/17 依頼番号 001540 
        NUM_WIDTH
        NUM_DEPTH
        NUM_HEIGHT
        NUM_ACTUAL_VOLUME
        NUM_OCCUPY_VOLUME
        LBL_CYL_FLG

        PNL_TANKA_JOHO
        LBL_TEKIYOSTART
        NUM_HOKANRYO_TUJO
        CMB_HOKANRYO_TUJO
        NUM_HOKANRYO_TEION
        CMB_HOKANRYO_TEION
        NUM_NIYAKURYO_NYUKO
        CMB_NIYAKURYO_NYUKO
        NUM_NIYAKURYO_SHUKKO
        CMB_NIYAKURYO_SHUKKO
        NUM_NIYAKU_MIN_NYUKO
        LBL_CUST_CD_S
        LBL_CUST_CD_SS

        TPG_GOODS_DETAIL
        CMB_DOKUGEKI            '毒劇区分
        CMB_KOUATHUGAS          '高圧ガス区分
        CMB_YAKUZIHO            '薬事法区分
        CMB_SHOBOKIKEN          '消防危険品区分
        TXT_SHOBOCD             '消防コード
        LBL_SHOBOCD             '消防コード(名称)
        TXT_UN                  'UN
        TXT_PG                  'PG
        TXT_CLASS1              'クラス(正)
        TXT_CLASS2              'クラス(副)
        TXT_CLASS3              'クラス(副)
        TXT_KAIYOUOSEN          '海洋汚染物質
        TXT_BARCODE             'バーコード
        CMB_KAGAKU_BUSITU       '化学物質区分
        CMB_SOKOHINMOKU         '倉庫品目区分
        CMB_HACCHUTEN           '発注点区分
        NUM_HACCHUTEN           '発注点区分(数値)
        CMB_SIZEKBN             '宅配便サイズ
        CMB_OUTER_PACKAGE       '外装
        AVAL_YN                 '使用可能
        BTN_ADD
        BTN_DETAIL
        SPR_GOODS_DETAIL
        LBL_CREATE_USER
        LBL_CREATE_DATE
        LBL_UPDATE_USER
        LBL_UPDATE_DATE
        LBL_UPDATE_TIME
        '20150729 常平add
        OCR_GOODS_CD_CUST
        OCR_GOODS_CD_NM1
        OCR_GOODS_CD_NM2
        OCR_GOODS_CD_STD_IRIME

    End Enum

    ''' <summary>
    ''' Spread(Goods)部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprGoodsColumnIndex

        DEF = 0
        STATUS
        AVAL_YN             'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
        AVAL_YN_NM          'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
        BR_CD
        BR_NM
        CUST_CD
        CUST_CD_L
        CUST_CD_M
        CUST_CD_S
        CUST_CD_SS
        CUST_NM_L
        CUST_NM_M
        CUST_NM_S
        CUST_NM_SS
        GOODS_KEY
        GOODS_CD
        GOODS_NM_1
        GOODS_NM_2
        GOODS_NM_3
        HYOJUN_IRIME
        HYOJUN_IRIME_TANI
        IRIME_TANI_CD
        HOSO_TANI
        NISUGATA_CD
        NISUGATA_NM
        IRI_SU
        HYOJUN_JURYO
        ACTUAL_VOLUME
        OCCUPY_VOLUME
        ONDO_KANRI_KBN
        ONDO_KANRI_NM
        TANKA_GROUP_CD
        DOKUGEKI_KBN
        DOKUGEKI_NM
        KOUATHUGAS_KB
        YAKUZIHO_KB
        SHOBOKIKEN_KB
        SHOBO_CD
        SHOBO_JOHO_NM
        SEIQT_CD
        SEIQT_COMP_NM
        SEIQT_BUSHO_NM
        KIKEN_DATE
        KIKEN_USER_NM
        CUST_CATEGORY_1
        CUST_CATEGORY_2
        CUST_KANJO_KMK_CD_1
        CUST_KANJO_KMK_CD_2
        BAR_CD
        KIKENHIN_KBN
        UN
        PG
        CLASS_1
        CLASS_2
        CLASS_3
        KAIYOUOSEN_KB
        KAIYOUOSEN_KB_NM
        KAGAKUBUSITU_KBN
        GUS_KANRI_KBN
        ONDO_KBN_UNSO
        ONDO_NM_UNSO
        ONDO_MAX
        ONDO_MIN
        ONDO_KANRI_START_HOKAN
        ONDO_KANRI_END_HOKAN
        ONDO_KANRI_START_UNSO
        ONDO_KANRI_END_UNSO
        SOKO_KYOKAI_HIN_KBN
        HIKIATE_TANI_KBN
        KOSU_TANI_KBN
        KOSU_TANI_NM
        HUTAI_JURYO
        PALETTO_HOSOKOSU
        INNER_PKG_NB
        HIZYU
        HYOJYUN_YOSEKI
        NYUKAJI_SAGYO_KBN_1
        NYUKAJI_SAGYO_KBN_2
        NYUKAJI_SAGYO_KBN_3
        NYUKAJI_SAGYO_KBN_4
        NYUKAJI_SAGYO_KBN_5
        SHUKKAJI_SAGYO_KBN_1
        SHUKKAJI_SAGYO_KBN_2
        SHUKKAJI_SAGYO_KBN_3
        SHUKKAJI_SAGYO_KBN_4
        SHUKKAJI_SAGYO_KBN_5
        KONPO_SAGYO_CD
        KONPO_SAGYO_NM
        HUTAI_KASAN_FLG
        SITEI_NOHINSHO_KBN
        BUNSEKI_HYO_KBN
        LOT_KANRI_LEVEL_CD
        SHOMIKIGEN_KANRI_CD
        SEIZOBI_KANRI_CD
        KITEI_HORYUHIN_KBN
        KITAKU_KAKAKU_TANI_KBN
        KITAKU_SHOHIN_TANKA
        HACCHUTEN_KBN
        HACCHU_SURYO
        NISONIN_CD_L
        NISONIN_NM_L
        SEIQT_DTL_SHUTURYOKU_FLG
        UNSO_HOKEN_FLG                          'ADD 2018/07/18 依頼番号 001540 
        HIKIATE_CHUIHIN_FLG
        SHUKKAJI_CHUIJIKO
        NIHUDA_INSATU_SU
        SHOHIKIGEN_KINSHIBI
        TEKIYO_START_DATE
        HOKANRYO_TUJO
        HOKANRYO_TATE_KBN_NASHI
        HOKANRYO_TEION
        HOKANRYO_TATE_KBN_ARI
        NIYAKURYO_NYUKO
        NIYAKURYO_NYUKO_TATE_KBN
        NIYAKURYO_SHUKKO
        NIYAKURYO_SHUKKO_TATE_KBN
        NIYAKU_MIN_NYUKO
        NIYAKU_MIN_SHUKKO
        HOKANRYO_TUJO_CURR_CD
        HOKANRYO_TEION_CURR_CD
        NIYAKURYO_NYUKO_CURR_CD
        NIYAKURYO_SHUKKO_CURR_CD
        NIYAKU_MIN_NYUKO_CURR_CD
        CREATE_DATE
        CREATE_USER
        UPDATE_DATE
        UPDATE_USER
        UPDATE_TIME
        SYS_DEL_FLG
        SIZE_KBN
        '20150729 常平add
        OCR_GOODS_CD_CUST
        OCR_GOODS_CD_NM1
        OCR_GOODS_CD_NM2
        OCR_GOODS_CD_STD_IRIME
#If True Then ' 要望番号2471対応 added 2015.12.14 inoue
        OUTER_PACKEGE ' 外装
#End If
        WIDTH
        DEPTH
        HEIGHT
        CYL_FLG
        '要望番号001995 端数出荷時作業区分対応
        SHUKKAJI_HASU_SAGYO_KBN_1
        SHUKKAJI_HASU_SAGYO_KBN_2
        SHUKKAJI_HASU_SAGYO_KBN_3
#If True Then ' 要望番号2471対応 added 2015.12.14 inoue
        COLUMN_COUNT  ' 総カラム数
#End If
    End Enum

    ''' <summary>
    ''' Spread(GoodsDetail)部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprGoodsDetailColumnIndex

        DEF = 0
        EDA_NO
        YOTO_KBN
        SETTEI_VALUE
        BIKO

    End Enum

#If True Then ' 要望番号2471対応 added 2015.12.14 inoue

    ''' <summary>
    ''' LMM100OUTカラム名定義クラス
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class LMM100OUT_CNAME
        ''' <summary>
        ''' 外装カラム名
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTER_PKG As String = "OUTER_PKG"
    End Class

    ''' <summary>
    ''' M_CUST_DETAILカラム名定義クラス
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class M_CUST_DETAIL_CNAME

        ''' <summary>
        ''' 営業所コード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NRS_BR_CD As String = "NRS_BR_CD"

        ''' <summary>
        ''' 荷主コード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CUST_CD As String = "CUST_CD"

        ''' <summary>
        ''' サブ区分
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SUB_KB As String = "SUB_KB"

        ''' <summary>
        ''' 設定値カラム名
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SET_NAIYO As String = "SET_NAIYO"

        ''' <summary>
        ''' 設定値２カラム名
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SET_NAIYO_2 As String = "SET_NAIYO_2"

        ''' <summary>
        ''' 設定値3カラム名
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SET_NAIYO_3 As String = "SET_NAIYO_3"

    End Class

    ''' <summary>
    ''' Z_KBNカラム名定義クラス
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class Z_KBN_CNAME

        ''' <summary>
        ''' 区分グループコード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const KBN_GROUP_CD As String = "KBN_GROUP_CD"

        ''' <summary>
        ''' 区分名1
        ''' </summary>
        ''' <remarks></remarks>
        Public Const KBN_NM1 As String = "KBN_NM1"

        ''' <summary>
        ''' 区分名2
        ''' </summary>
        ''' <remarks></remarks>
        Public Const KBN_NM2 As String = "KBN_NM2"

        ''' <summary>
        ''' ソート順
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SORT As String = "SORT"

    End Class

#End If
    ''' <summary>
    ''' M_CUST_DETAILカラム名定義クラス
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class YOTO_KBN
        ''' <summary>
        ''' 用途区分【区分マスタ：Y007】
        ''' </summary>
        ''' <remarks></remarks>
        Public Const YOTO_SAFETY_STOCK As String = "62"
        Public Const YOTO_PALETTE_LIMIT As String = "64"

    End Class


End Class
