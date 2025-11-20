' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI110C : 日医工製品マスタ登録
'  作  成  者       :  [寺川徹]
' ==========================================================================

''' <summary>
''' LMI110定数定義クラス
''' </summary>
''' <remarks></remarks>
Friend Class LMI110C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMI110INOUT_M_SEIHIN_NIK"
    Friend Const TABLE_NM_OUT As String = "LMI110OUT_M_SEIHIN_NIK"
    Friend Const TABLE_NM_M_GOODS_HANEI As String = "M_GOODS_HANEI"
    '2012.05.18 umano 追加START
    Friend Const TABLE_NM_GUIERROR As String = "LMI110_GUIERROR"
    '2012.05.18 umano 追加END
    '要望番号:1250 terakawa 2012.07.12 Start
    Friend Const TABLE_NM_M_GOODS_DETAILS As String = "M_GOODS_DETAILS"
    '要望番号:1250 terakawa 2012.07.12 End
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
    ''' 保管温度区分【区分マスタ：I006】
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Friend Const HOKAN_ONDO_SHITSUON As String = "01"
    Friend Const HOKAN_ONDO_HOREI As String = "02"
    Friend Const HOKAN_ONDO_REIZO As String = "03"
    Friend Const HOKAN_ONDO_REITO As String = "04"
    Friend Const HOKAN_ONDO_SONOTA As String = "99"

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
    ''' 取込区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NOT_IMPORT As String = "00"
    Public Const IMPORT As String = "01"

    ''' <summary>
    ''' 営業所コード（千葉）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NRS_BR_CD As String = "10"

    '2012.05.18 umano 追加START
    ''' <summary>
    ''' ガイダンス区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"


    ''' <summary>
    ''' エラーEXCEL COLTITLE
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_COLTITLE As String = "製品コード"

    ''' <summary>
    ''' ステータス
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Const MGOODS_NEW As String = "新規"
    Public Const MGOODS_EDIT As String = "変更"
    Public Const MGOODS_DOUBLE As String = "重複"

    ''' <summary>
    ''' 取込区分
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Const HANEI_MI As String = "未反映"
    Public Const HANEI_SUMI As String = "反映済"

    '2012.05.18 umano 追加END

    ''' <summary>
    ''' 開始・終了日付
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Const STR_DATE As String = "0101"
    Public Const END_DATE As String = "1231"

    ''' <summary>
    ''' 引当単位区分
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Const KOSU As String = "01"
    Public Const SURYO As String = "02"

    ''' <summary>
    ''' 賞味期限管理区分
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Const SHOMI_NASHI As String = "00"
    Public Const SHOMI_ARI As String = "01"

    '要望番号:1250 terakawa 2012.07.12 Start
    ''' <summary>
    ''' 用途区分（商品明細）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Const YOUTO_NYUKOBI_KANRRI As String = "09"

    ''' <summary>
    ''' 入庫日管理区分
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Const NYUKOBI_KANRI_ARI As String = "0"
    Public Const NYUKOBI_KANRI_NASHI As String = "1"


    ''' <summary>
    ''' 商品KEY枝番
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Const GOODS_CD_NRS_EDA_0 As String = "00"
    '要望番号:1250 terakawa 2012.07.12 End

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum EventShubetsu As Integer

        KENSAKU = 0
        MASTEROPEN
        SAVEGOODSM
        TOJIRU
        ENTER

    End Enum

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        MASTEROPEN
        SAVE
        CLOSE
        ENTER

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        CMB_EIGYO = 0
        TXT_CUST_CD_L
        TXT_CST_CD_M
        TXT_CUST_NM
        IMD_EDI_DATE_FROM
        IMD_EDI_DATE_TO
        CHK_NOT_IMPORT
        CHK_IMPORT
        SPR_NICHIKO_GOODS
        TXT_SERCH_GOODS_CD
        TXT_SERCH_GOODS_NM
        TXT_GOODS_CUST_CD_L
        TXT_GOODS_CUST_NM_L
        TXT_GOODS_CUST_CD_M
        TXT_GOODS_CUST_NM_M
        TXT_GOODS_CUST_CD_S
        TXT_GOODS_CUST_NM_S
        TXT_GOODS_CUST_CD_SS
        TXT_GOODS_CUST_NM_SS
        TXT_GOODS_NM_1
        TXT_GOODS_NM_2
        TXT_GOODS_KEY
        TXT_GOODS_CD
        CMB_TARE_YN
        TXT_UP_GROUP_CD_1
        CMB_LOT_CTL_KB
        CMB_SP_NHS_YN
        CMB_COA_YN
        CMB_HIKIATE_ALERT_YN
        CMB_SKYU_MEI_YN
       

    End Enum

    ''' <summary>
    ''' Spread(Goods)部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprGoodsColumnIndex

        DEF = 0
        STATUS
        TORIKOMI_KBN
        SYS_ENT_DATE
        GOODS_CD
        GOODS_NM
        GOODS_NM_KANA
        GOODS_KIKAKU
        '列順変更対応 Start
        PKG_NB
        NB_UT
        STD_IRIME_NB
        STD_IRIME_UT
        'GOODS_KIKAKU_KANA
        '列順変更対応 End
        JAN_CD
        KANRI_KB_NM
        ONDO_KB_NM
        '列順変更対応 Start
        GOODS_KIKAKU_KANA
        YUKO_MONTH
        'PKG_NB
        'NB_UT
        'STD_IRIME_NB
        'STD_IRIME_UT
        '列順変更対応 End
        NB_FORM_LENGTH
        NB_FORM_WIDTH
        NB_FORM_HEIGHT
        NB_WT_GS
        PKG_FORM_LENGTH
        PKG_FORM_WIDTH
        PKG_FORM_HEIGHT
        PKG_WT_GS
        TEKIYO_DATE

        GOODS_NM_RYAKU
        ITF_CD
        SIIRE_CD
        NB_ML
        PKG_ML
        PLT_PER_PKG_UT
        SURFACE_PKG_NB
        SURFACE_NUM_ROW
        M_GOODS_ONDO_KB
        M_GOODS_STD_IRIME_NB
        M_GOODS_PKG_NB
        M_GOODS_CNT
        M_SEIHIN_SYS_UPD_DATE
        M_SEIHIN_SYS_UPD_TIME
        M_GOODS_SYS_UPD_DATE
        M_GOODS_SYS_UPD_TIME
        M_SEIHIN_SYS_DEL_FLAG
        M_GOODS_SYS_DEL_FLAG
        GOODS_CD_NRS
        ONDO_KB
        KANRI_KB

    End Enum

End Class
