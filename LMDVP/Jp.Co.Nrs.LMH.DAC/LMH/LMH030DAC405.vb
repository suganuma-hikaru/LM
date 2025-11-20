' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷データ検索
'  EDI荷主ID　　　　:  120　　　 : インターコンチ(横浜) セミ標準複写 2018/08/25
'  作  成  者       :  daikoku
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH030DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030DAC405
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const(EDI取込系)"
#Region "EDI取込取込系処理"
#Region "セミEDI(取込画面)処理 抽出用SQL"
#Region "商品マスタ読込"
#Region "SELECT_M_GOODS(セミEDI時)"
    ''' <summary>
    ''' 商品マスタ読込(SELECT句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_M_GOODS As String _
                    = " SELECT                                                     " & vbNewLine _
                    & " COUNT(*)                       AS  MST_CNT                 " & vbNewLine _
                    & ",NRS_BR_CD                      AS  NRS_BR_CD               " & vbNewLine _
                    & ",GOODS_CD_NRS                   AS  GOODS_CD_NRS            " & vbNewLine _
                    & ",CUST_CD_L                      AS  CUST_CD_L               " & vbNewLine _
                    & ",CUST_CD_M                      AS  CUST_CD_M               " & vbNewLine _
                    & ",CUST_CD_S                      AS  CUST_CD_S               " & vbNewLine _
                    & ",CUST_CD_SS                     AS  CUST_CD_SS              " & vbNewLine _
                    & ",GOODS_CD_CUST                  AS  GOODS_CD_CUST           " & vbNewLine _
                    & ",SEARCH_KEY_1                   AS  SEARCH_KEY_1            " & vbNewLine _
                    & ",SEARCH_KEY_2                   AS  SEARCH_KEY_2            " & vbNewLine _
                    & ",CUST_COST_CD1                  AS  CUST_COST_CD1           " & vbNewLine _
                    & ",CUST_COST_CD2                  AS  CUST_COST_CD2           " & vbNewLine _
                    & ",JAN_CD                         AS  JAN_CD                  " & vbNewLine _
                    & ",GOODS_NM_1                     AS  GOODS_NM_1              " & vbNewLine _
                    & ",GOODS_NM_2                     AS  GOODS_NM_2              " & vbNewLine _
                    & ",GOODS_NM_3                     AS  GOODS_NM_3              " & vbNewLine _
                    & ",UP_GP_CD_1                     AS  UP_GP_CD_1              " & vbNewLine _
                    & ",SHOBO_CD                       AS  SHOBO_CD                " & vbNewLine _
                    & ",KIKEN_KB                       AS  KIKEN_KB                " & vbNewLine _
                    & ",UN                             AS  UN                      " & vbNewLine _
                    & ",PG_KB                          AS  PG_KB                   " & vbNewLine _
                    & ",CLASS_1                        AS  CLASS_1                 " & vbNewLine _
                    & ",CLASS_2                        AS  CLASS_2                 " & vbNewLine _
                    & ",CLASS_3                        AS  CLASS_3                 " & vbNewLine _
                    & ",CHEM_MTRL_KB                   AS  CHEM_MTRL_KB            " & vbNewLine _
                    & ",DOKU_KB                        AS  DOKU_KB                 " & vbNewLine _
                    & ",GAS_KANRI_KB                   AS  GAS_KANRI_KB            " & vbNewLine _
                    & ",ONDO_KB                        AS  ONDO_KB                 " & vbNewLine _
                    & ",UNSO_ONDO_KB                   AS  UNSO_ONDO_KB            " & vbNewLine _
                    & ",ONDO_MX                        AS  ONDO_MX                 " & vbNewLine _
                    & ",ONDO_MM                        AS  ONDO_MM                 " & vbNewLine _
                    & ",ONDO_STR_DATE                  AS  ONDO_STR_DATE           " & vbNewLine _
                    & ",ONDO_END_DATE                  AS  ONDO_END_DATE           " & vbNewLine _
                    & ",ONDO_UNSO_STR_DATE             AS  ONDO_UNSO_STR_DATE      " & vbNewLine _
                    & ",ONDO_UNSO_END_DATE             AS  ONDO_UNSO_END_DATE      " & vbNewLine _
                    & ",KYOKAI_GOODS_KB                AS  KYOKAI_GOODS_KB         " & vbNewLine _
                    & ",ALCTD_KB                       AS  ALCTD_KB                " & vbNewLine _
                    & ",NB_UT                          AS  NB_UT                   " & vbNewLine _
                    & ",PKG_NB                         AS  PKG_NB                  " & vbNewLine _
                    & ",PKG_UT                         AS  PKG_UT                  " & vbNewLine _
                    & ",PLT_PER_PKG_UT                 AS  PLT_PER_PKG_UT          " & vbNewLine _
                    & ",STD_IRIME_NB                   AS  STD_IRIME_NB            " & vbNewLine _
                    & ",STD_IRIME_UT                   AS  STD_IRIME_UT            " & vbNewLine _
                    & ",STD_WT_KGS                     AS  STD_WT_KGS              " & vbNewLine _
                    & ",STD_CBM                        AS  STD_CBM                 " & vbNewLine _
                    & ",INKA_KAKO_SAGYO_KB_1           AS  INKA_KAKO_SAGYO_KB_1    " & vbNewLine _
                    & ",INKA_KAKO_SAGYO_KB_2           AS  INKA_KAKO_SAGYO_KB_2    " & vbNewLine _
                    & ",INKA_KAKO_SAGYO_KB_3           AS  INKA_KAKO_SAGYO_KB_3    " & vbNewLine _
                    & ",INKA_KAKO_SAGYO_KB_4           AS  INKA_KAKO_SAGYO_KB_4    " & vbNewLine _
                    & ",INKA_KAKO_SAGYO_KB_5           AS  INKA_KAKO_SAGYO_KB_5    " & vbNewLine _
                    & ",OUTKA_KAKO_SAGYO_KB_1          AS  OUTKA_KAKO_SAGYO_KB_1   " & vbNewLine _
                    & ",OUTKA_KAKO_SAGYO_KB_2          AS  OUTKA_KAKO_SAGYO_KB_2   " & vbNewLine _
                    & ",OUTKA_KAKO_SAGYO_KB_3          AS  OUTKA_KAKO_SAGYO_KB_3   " & vbNewLine _
                    & ",OUTKA_KAKO_SAGYO_KB_4          AS  OUTKA_KAKO_SAGYO_KB_4   " & vbNewLine _
                    & ",OUTKA_KAKO_SAGYO_KB_5          AS  OUTKA_KAKO_SAGYO_KB_5   " & vbNewLine _
                    & ",PKG_SAGYO                      AS  PKG_SAGYO               " & vbNewLine _
                    & ",TARE_YN                        AS  TARE_YN                 " & vbNewLine _
                    & ",SP_NHS_YN                      AS  SP_NHS_YN               " & vbNewLine _
                    & ",COA_YN                         AS  COA_YN                  " & vbNewLine _
                    & ",LOT_CTL_KB                     AS  LOT_CTL_KB              " & vbNewLine _
                    & ",LT_DATE_CTL_KB                 AS  LT_DATE_CTL_KB          " & vbNewLine _
                    & ",CRT_DATE_CTL_KB                AS  CRT_DATE_CTL_KB         " & vbNewLine _
                    & ",DEF_SPD_KB                     AS  DEF_SPD_KB              " & vbNewLine _
                    & ",KITAKU_AM_UT_KB                AS  KITAKU_AM_UT_KB         " & vbNewLine _
                    & ",KITAKU_GOODS_UP                AS  KITAKU_GOODS_UP         " & vbNewLine _
                    & ",ORDER_KB                       AS  ORDER_KB                " & vbNewLine _
                    & ",ORDER_NB                       AS  ORDER_NB                " & vbNewLine _
                    & ",SHIP_CD_L                      AS  SHIP_CD_L               " & vbNewLine _
                    & ",SKYU_MEI_YN                    AS  SKYU_MEI_YN             " & vbNewLine _
                    & ",HIKIATE_ALERT_YN               AS  HIKIATE_ALERT_YN        " & vbNewLine _
                    & ",OUTKA_ATT                      AS  OUTKA_ATT               " & vbNewLine _
                    & ",PRINT_NB                       AS  PRINT_NB                " & vbNewLine _
                    & ",CONSUME_PERIOD_DATE            AS  CONSUME_PERIOD_DATE     " & vbNewLine _
                    & ",SYS_ENT_DATE                   AS  SYS_ENT_DATE            " & vbNewLine _
                    & ",SYS_ENT_TIME                   AS  SYS_ENT_TIME            " & vbNewLine _
                    & ",SYS_ENT_PGID                   AS  SYS_ENT_PGID            " & vbNewLine _
                    & ",SYS_ENT_USER                   AS  SYS_ENT_USER            " & vbNewLine _
                    & ",SYS_UPD_DATE                   AS  SYS_UPD_DATE            " & vbNewLine _
                    & ",SYS_UPD_TIME                   AS  SYS_UPD_TIME            " & vbNewLine _
                    & ",SYS_UPD_PGID                   AS  SYS_UPD_PGID            " & vbNewLine _
                    & ",SYS_UPD_USER                   AS  SYS_UPD_USER            " & vbNewLine _
                    & ",SYS_DEL_FLG                    AS  SYS_DEL_FLG             " & vbNewLine _
                    & " FROM                                                       " & vbNewLine _
                    & " $LM_MST$..M_GOODS              M_GOODS                     " & vbNewLine


#Region "SELECT_M_CUST_DETAILS(セミEDI時)"

    ''' <summary>
    ''' 荷主明細マスタ読込(SELECT句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SELECT_M_CUST_DETL As String _
                    = " SELECT                                                     " & vbNewLine _
                    & " COUNT(*)                       AS  MST_CNT                 " & vbNewLine _
                    & " FROM $LM_MST$..M_CUST_DETAILS       M_CUST_DETAILS              " & vbNewLine

#End Region

#Region "SELECT_M_CUST_DETL_ALL(全件)(セミEDI時)"

    ''' <summary>
    ''' 荷主明細マスタ読込(SELECT句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SELECT_M_CUST_DETL_ALL As String _
                    = " SELECT                                                     " & vbNewLine _
                    & " NRS_BR_CD                      AS NRS_BR_CD                " & vbNewLine _
                    & ",CUST_CD	                       AS CUST_CD                  " & vbNewLine _
                    & ",CUST_CD_EDA                    AS CUST_CD_EDA              " & vbNewLine _
                    & ",CUST_CLASS                     AS CUST_CLASS               " & vbNewLine _
                    & ",SUB_KB                         AS SUB_KB                   " & vbNewLine _
                    & ",SET_NAIYO                      AS SET_NAIYO                " & vbNewLine _
                    & ",SET_NAIYO_2	                   AS SET_NAIYO_2              " & vbNewLine _
                    & ",SET_NAIYO_3	                   AS SET_NAIYO_3              " & vbNewLine _
                    & ",REMARK                         AS REMARK                   " & vbNewLine _
                    & " FROM $LM_MST$..M_CUST_DETAILS     M_CUST_DETAILS           " & vbNewLine

#End Region

#Region "WHERE1_M_GOODS(セミEDI時)"
    ''' <summary>
    ''' 商品マスタ読込(WHERE句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE1_M_GOODS As String = _
                                       " WHERE                                              " & vbNewLine _
                                     & " M_GOODS.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
                                     & " AND                                                " & vbNewLine _
                                     & " M_GOODS.CUST_CD_L = @CUST_CD_L                     " & vbNewLine _
                                     & " AND                                                " & vbNewLine _
                                     & " M_GOODS.CUST_CD_M = @CUST_CD_M                     " & vbNewLine _
                                     & " AND                                                " & vbNewLine _
                                     & " M_GOODS.GOODS_CD_CUST = @GOODS_CD_CUST             " & vbNewLine _
                                     & " AND                                                " & vbNewLine _
                                     & " M_GOODS.SYS_DEL_FLG = '0'                          " & vbNewLine

    ''' <summary>
    ''' 商品マスタ読込(WHERE句+　横浜フィルメ対応)
    ''' </summary>
    ''' <remarks>ADD 2016/07/20 </remarks>
    Private Const SQL_WHERE1_M_GOODS_FLM As String = _
                                       " AND                                                " & vbNewLine _
                                     & " M_GOODS.STD_IRIME_NB = @STD_IRIME_NB               " & vbNewLine


    ''' <summary>
    ''' 荷主明細マスタ読込(WHERE句)[フィルメニッヒ対応]
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE1_M_CUST_DETAILS_FIR As String = _
                                       " WHERE                                              " & vbNewLine _
                                     & " M_CUST_DETAILS.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                     & " AND                                                " & vbNewLine _
                                     & " M_CUST_DETAILS.CUST_CD = @CUST_CD_L                " & vbNewLine _
                                     & " AND                                                " & vbNewLine _
                                     & " SUB_KB = '0L'                                      " & vbNewLine

    ''' <summary>
    ''' 荷主明細マスタ読込(WHERE句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE1_M_CUST_DETAILS As String = _
                                       " WHERE                                              " & vbNewLine _
                                     & " M_CUST_DETAILS.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                     & " AND                                                " & vbNewLine _
                                     & " M_CUST_DETAILS.CUST_CD = @CUST_CD_L                " & vbNewLine _
                                     & " AND                                                " & vbNewLine _
                                     & " SUB_KB = '87'                                      " & vbNewLine


    ''' <summary>
    ''' 荷主明細マスタ読込(全件)(WHERE句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_M_CUST_DETAILS_ALL As String = _
                                       " WHERE                                              " & vbNewLine _
                                     & " M_CUST_DETAILS.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                     & " AND                                                " & vbNewLine _
                                     & " M_CUST_DETAILS.CUST_CD = @CUST_CD_L                " & vbNewLine _
                                     & " AND                                                " & vbNewLine _
                                     & " M_CUST_DETAILS.SYS_DEL_FLG = '0'                   " & vbNewLine

#End Region

#Region "GROUP_BY_M_GOODS(セミEDI時)"
    ''' <summary>
    ''' 商品マスタ読込(GROUP BY句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_M_GOODS As String _
                    = " GROUP BY              " & vbNewLine _
                    & " NRS_BR_CD             " & vbNewLine _
                    & ",GOODS_CD_NRS          " & vbNewLine _
                    & ",CUST_CD_L             " & vbNewLine _
                    & ",CUST_CD_M             " & vbNewLine _
                    & ",CUST_CD_S             " & vbNewLine _
                    & ",CUST_CD_SS            " & vbNewLine _
                    & ",GOODS_CD_CUST         " & vbNewLine _
                    & ",SEARCH_KEY_1          " & vbNewLine _
                    & ",SEARCH_KEY_2          " & vbNewLine _
                    & ",CUST_COST_CD1         " & vbNewLine _
                    & ",CUST_COST_CD2         " & vbNewLine _
                    & ",JAN_CD                " & vbNewLine _
                    & ",GOODS_NM_1            " & vbNewLine _
                    & ",GOODS_NM_2            " & vbNewLine _
                    & ",GOODS_NM_3            " & vbNewLine _
                    & ",UP_GP_CD_1            " & vbNewLine _
                    & ",SHOBO_CD              " & vbNewLine _
                    & ",KIKEN_KB              " & vbNewLine _
                    & ",UN                    " & vbNewLine _
                    & ",PG_KB                 " & vbNewLine _
                    & ",CLASS_1               " & vbNewLine _
                    & ",CLASS_2               " & vbNewLine _
                    & ",CLASS_3               " & vbNewLine _
                    & ",CHEM_MTRL_KB          " & vbNewLine _
                    & ",DOKU_KB               " & vbNewLine _
                    & ",GAS_KANRI_KB          " & vbNewLine _
                    & ",ONDO_KB               " & vbNewLine _
                    & ",UNSO_ONDO_KB          " & vbNewLine _
                    & ",ONDO_MX               " & vbNewLine _
                    & ",ONDO_MM               " & vbNewLine _
                    & ",ONDO_STR_DATE         " & vbNewLine _
                    & ",ONDO_END_DATE         " & vbNewLine _
                    & ",ONDO_UNSO_STR_DATE    " & vbNewLine _
                    & ",ONDO_UNSO_END_DATE    " & vbNewLine _
                    & ",KYOKAI_GOODS_KB       " & vbNewLine _
                    & ",ALCTD_KB              " & vbNewLine _
                    & ",NB_UT                 " & vbNewLine _
                    & ",PKG_NB                " & vbNewLine _
                    & ",PKG_UT                " & vbNewLine _
                    & ",PLT_PER_PKG_UT        " & vbNewLine _
                    & ",STD_IRIME_NB          " & vbNewLine _
                    & ",STD_IRIME_UT          " & vbNewLine _
                    & ",STD_WT_KGS            " & vbNewLine _
                    & ",STD_CBM               " & vbNewLine _
                    & ",INKA_KAKO_SAGYO_KB_1  " & vbNewLine _
                    & ",INKA_KAKO_SAGYO_KB_2  " & vbNewLine _
                    & ",INKA_KAKO_SAGYO_KB_3  " & vbNewLine _
                    & ",INKA_KAKO_SAGYO_KB_4  " & vbNewLine _
                    & ",INKA_KAKO_SAGYO_KB_5  " & vbNewLine _
                    & ",OUTKA_KAKO_SAGYO_KB_1 " & vbNewLine _
                    & ",OUTKA_KAKO_SAGYO_KB_2 " & vbNewLine _
                    & ",OUTKA_KAKO_SAGYO_KB_3 " & vbNewLine _
                    & ",OUTKA_KAKO_SAGYO_KB_4 " & vbNewLine _
                    & ",OUTKA_KAKO_SAGYO_KB_5 " & vbNewLine _
                    & ",PKG_SAGYO             " & vbNewLine _
                    & ",TARE_YN               " & vbNewLine _
                    & ",SP_NHS_YN             " & vbNewLine _
                    & ",COA_YN                " & vbNewLine _
                    & ",LOT_CTL_KB            " & vbNewLine _
                    & ",LT_DATE_CTL_KB        " & vbNewLine _
                    & ",CRT_DATE_CTL_KB       " & vbNewLine _
                    & ",DEF_SPD_KB            " & vbNewLine _
                    & ",KITAKU_AM_UT_KB       " & vbNewLine _
                    & ",KITAKU_GOODS_UP       " & vbNewLine _
                    & ",ORDER_KB              " & vbNewLine _
                    & ",ORDER_NB              " & vbNewLine _
                    & ",SHIP_CD_L             " & vbNewLine _
                    & ",SKYU_MEI_YN           " & vbNewLine _
                    & ",HIKIATE_ALERT_YN      " & vbNewLine _
                    & ",OUTKA_ATT             " & vbNewLine _
                    & ",PRINT_NB              " & vbNewLine _
                    & ",CONSUME_PERIOD_DATE   " & vbNewLine _
                    & ",SYS_ENT_DATE          " & vbNewLine _
                    & ",SYS_ENT_TIME          " & vbNewLine _
                    & ",SYS_ENT_PGID          " & vbNewLine _
                    & ",SYS_ENT_USER          " & vbNewLine _
                    & ",SYS_UPD_DATE          " & vbNewLine _
                    & ",SYS_UPD_TIME          " & vbNewLine _
                    & ",SYS_UPD_PGID          " & vbNewLine _
                    & ",SYS_UPD_USER          " & vbNewLine _
                    & ",SYS_DEL_FLG           " & vbNewLine

#End Region

#End Region
#End Region

#Region "届先マスタ読込"
#Region "SELECT_M_DEST"
    ''' <summary>
    ''' 届先マスタ読込(複合)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_M_DEST As String = " SELECT                                   " & vbNewLine _
                                     & " COUNT(*)                      AS MST_CNT          " & vbNewLine _
                                     & ",NRS_BR_CD                     AS NRS_BR_CD        " & vbNewLine _
                                     & ",CUST_CD_L                     AS CUST_CD_L        " & vbNewLine _
                                     & ",DEST_CD                       AS DEST_CD          " & vbNewLine _
                                     & ",EDI_CD                        AS EDI_CD           " & vbNewLine _
                                     & ",DEST_NM                       AS DEST_NM          " & vbNewLine _
                                     & ",ZIP                           AS ZIP              " & vbNewLine _
                                     & ",AD_1                          AS AD_1             " & vbNewLine _
                                     & ",AD_2                          AS AD_2             " & vbNewLine _
                                     & ",AD_3                          AS AD_3             " & vbNewLine _
                                     & ",CUST_DEST_CD                  AS CUST_DEST_CD     " & vbNewLine _
                                     & ",SALES_CD                      AS SALES_CD         " & vbNewLine _
                                     & ",SP_NHS_KB                     AS SP_NHS_KB        " & vbNewLine _
                                     & ",COA_YN                        AS COA_YN           " & vbNewLine _
                                     & ",SP_UNSO_CD                    AS SP_UNSO_CD       " & vbNewLine _
                                     & ",SP_UNSO_BR_CD                 AS SP_UNSO_BR_CD    " & vbNewLine _
                                     & ",DELI_ATT                      AS DELI_ATT         " & vbNewLine _
                                     & ",CARGO_TIME_LIMIT              AS CARGO_TIME_LIMIT " & vbNewLine _
                                     & ",LARGE_CAR_YN                  AS LARGE_CAR_YN     " & vbNewLine _
                                     & ",TEL                           AS TEL              " & vbNewLine _
                                     & ",FAX                           AS FAX              " & vbNewLine _
                                     & ",UNCHIN_SEIQTO_CD              AS UNCHIN_SEIQTO_CD " & vbNewLine _
                                     & ",JIS                           AS JIS              " & vbNewLine _
                                     & ",KYORI                         AS KYORI            " & vbNewLine _
                                     & ",PICK_KB                       AS PICK_KB          " & vbNewLine _
                                     & ",BIN_KB                        AS BIN_KB           " & vbNewLine _
                                     & ",MOTO_CHAKU_KB                 AS MOTO_CHAKU_KB    " & vbNewLine _
                                     & ",URIAGE_CD                     AS URIAGE_CD        " & vbNewLine _
                                     & ",REMARK                        AS REMARK           " & vbNewLine _
                                     & ",SYS_ENT_DATE                  AS  SYS_ENT_DATE    " & vbNewLine _
                                     & ",SYS_ENT_TIME                  AS  SYS_ENT_TIME    " & vbNewLine _
                                     & ",SYS_ENT_PGID                  AS  SYS_ENT_PGID    " & vbNewLine _
                                     & ",SYS_ENT_USER                  AS  SYS_ENT_USER    " & vbNewLine _
                                     & ",SYS_UPD_DATE                  AS  SYS_UPD_DATE    " & vbNewLine _
                                     & ",SYS_UPD_TIME                  AS  SYS_UPD_TIME    " & vbNewLine _
                                     & ",SYS_UPD_PGID                  AS  SYS_UPD_PGID    " & vbNewLine _
                                     & ",SYS_UPD_USER                  AS  SYS_UPD_USER    " & vbNewLine _
                                     & ",SYS_DEL_FLG                   AS  SYS_DEL_FLG     " & vbNewLine _
                                     & " FROM                                              " & vbNewLine _
                                     & " $LM_MST$..M_DEST              M_DEST              " & vbNewLine _
                                     & " WHERE                                             " & vbNewLine _
                                     & " M_DEST.NRS_BR_CD            = @NRS_BR_CD          " & vbNewLine _
                                     & " AND                                               " & vbNewLine _
                                     & " M_DEST.CUST_CD_L            = @CUST_CD_L          " & vbNewLine

#End Region

#Region "GROUP_BY_M_DEST"
    ''' <summary>
    ''' 届先マスタ(GROUP BY 句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_M_DEST As String = " GROUP BY                     " & vbNewLine _
                                     & "NRS_BR_CD                                " & vbNewLine _
                                     & ",CUST_CD_L                               " & vbNewLine _
                                     & ",DEST_CD                                 " & vbNewLine _
                                     & ",EDI_CD                                  " & vbNewLine _
                                     & ",DEST_NM                                 " & vbNewLine _
                                     & ",ZIP                                     " & vbNewLine _
                                     & ",AD_1                                    " & vbNewLine _
                                     & ",AD_2                                    " & vbNewLine _
                                     & ",AD_3                                    " & vbNewLine _
                                     & ",CUST_DEST_CD                            " & vbNewLine _
                                     & ",SALES_CD                                " & vbNewLine _
                                     & ",SP_NHS_KB                               " & vbNewLine _
                                     & ",COA_YN                                  " & vbNewLine _
                                     & ",SP_UNSO_CD                              " & vbNewLine _
                                     & ",SP_UNSO_BR_CD                           " & vbNewLine _
                                     & ",DELI_ATT                                " & vbNewLine _
                                     & ",CARGO_TIME_LIMIT                        " & vbNewLine _
                                     & ",LARGE_CAR_YN                            " & vbNewLine _
                                     & ",TEL                                     " & vbNewLine _
                                     & ",FAX                                     " & vbNewLine _
                                     & ",UNCHIN_SEIQTO_CD                        " & vbNewLine _
                                     & ",JIS                                     " & vbNewLine _
                                     & ",KYORI                                   " & vbNewLine _
                                     & ",PICK_KB                                 " & vbNewLine _
                                     & ",BIN_KB                                  " & vbNewLine _
                                     & ",MOTO_CHAKU_KB                           " & vbNewLine _
                                     & ",URIAGE_CD                               " & vbNewLine _
                                     & ",REMARK                                  " & vbNewLine _
                                     & ",SYS_ENT_DATE                            " & vbNewLine _
                                     & ",SYS_ENT_TIME                            " & vbNewLine _
                                     & ",SYS_ENT_PGID                            " & vbNewLine _
                                     & ",SYS_ENT_USER                            " & vbNewLine _
                                     & ",SYS_UPD_DATE                            " & vbNewLine _
                                     & ",SYS_UPD_TIME                            " & vbNewLine _
                                     & ",SYS_UPD_PGID                            " & vbNewLine _
                                     & ",SYS_UPD_USER                            " & vbNewLine _
                                     & ",SYS_DEL_FLG                             " & vbNewLine

#End Region

#End Region

    '2015.05.14 引当単位区分の抽出 追加START
#Region "区分マスタ読込"
    Private Const SQL_SELECT_Z_KBN As String = " SELECT                                        " & vbNewLine _
                                     & " Z_KBN.KBN_NM2                          AS NISUGATA    " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..Z_KBN                        Z_KBN          " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " Z_KBN.KBN_GROUP_CD = @KBN_GROUP_CD                    " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " Z_KBN.KBN_NM1   = @KBN_CD                             " & vbNewLine


#End Region
    '2015.05.14 引当単位区分の抽出 追加END

#Region "SQL_SelectCountM_HOL"
    ''' <summary>
    ''' 休日マスタ取得(複合)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SelectCountM_HOL As String = " SELECT                             " & vbNewLine _
                                     & " COUNT(*)                                       " & vbNewLine _
                                     & " FROM                                           " & vbNewLine _
                                     & "    $LM_MST$..M_HOL                             " & vbNewLine _
                                     & " WHERE                                          " & vbNewLine _
                                     & "    HOL = @HOL                                  " & vbNewLine
#End Region

#Region "SQL_GET_MEISAI_COUNT"
    ''' <summary>
    ''' EDI明細件数取得(複合)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_MEISAI_COUNT As String = " SELECT                             " & vbNewLine _
                                     & " COUNT(*)                                       " & vbNewLine _
                                     & " FROM                                           " & vbNewLine _
                                     & "    $LM_TRN$..H_OUTKAEDI_M                      " & vbNewLine _
                                     & " WHERE                                          " & vbNewLine _
                                     & "    NRS_BR_CD   = @NRS_BR_CD                    " & vbNewLine _
                                     & " AND                                            " & vbNewLine _
                                     & "    EDI_CTL_NO  = @EDI_CTL_NO                   " & vbNewLine _
                                     & " AND                                            " & vbNewLine _
                                     & "    SYS_DEL_FLG = '0'                           " & vbNewLine

#End Region

#Region "キャンセルデータ抽出"
    ''' <summary>
    ''' キャンセルデータの抽出
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CANCER_DATA As String = " SELECT                                        " & vbNewLine _
                                                   & "      COL.OUTKA_NO_L           AS OUTKA_CTL_NO " & vbNewLine _
                                                   & "     ,HOL.EDI_CTL_NO           AS EDI_CTL_NO   " & vbNewLine _
                                                   & " FROM $LM_TRN$..H_OUTKAEDI_L   AS HOL          " & vbNewLine _
                                                   & " LEFT JOIN                                     " & vbNewLine _
                                                   & "      $LM_TRN$..C_OUTKA_L      AS COL          " & vbNewLine _
                                                   & "   ON HOL.NRS_BR_CD          = COL.NRS_BR_CD   " & vbNewLine _
                                                   & "  AND HOL.OUTKA_CTL_NO       = COL.OUTKA_NO_L  " & vbNewLine _
                                                   & "  AND COL.SYS_DEL_FLG        = '0'             " & vbNewLine _
                                                   & " WHERE                                         " & vbNewLine _
                                                   & "      HOL.SYS_DEL_FLG        = '0'             " & vbNewLine _
                                                   & "  AND HOL.DEL_KB            <> '3'             " & vbNewLine

    ''' <summary>
    ''' 抽出条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_CANCER_DATA As String = "  AND HOL.NRS_BR_CD          = @NRS_BR_CD      --@パラ " & vbNewLine _
                                                  & "  AND HOL.CUST_CD_L          = @CUST_CD_L      --@パラ " & vbNewLine _
                                                  & "  AND HOL.CUST_CD_M          = @CUST_CD_M      --@パラ " & vbNewLine _
                                                  & "  AND HOL.CUST_ORD_NO     LIKE @CUST_ORD_NO    --@パラ " & vbNewLine


    ''' <summary>
    ''' キャンセルデータの抽出
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CANCER_DATA_DTL As String = " SELECT                                        " & vbNewLine _
                                                   & "      COL.OUTKA_NO_L           AS OUTKA_CTL_NO " & vbNewLine _
                                                   & "     ,HOL.EDI_CTL_NO           AS EDI_CTL_NO   " & vbNewLine _
                                                   & " FROM $LM_TRN$..H_OUTKAEDI_L   AS HOL          " & vbNewLine _
                                                   & " LEFT JOIN                                     " & vbNewLine _
                                                   & "      $LM_TRN$..H_OUTKAEDI_M   AS HOM          " & vbNewLine _
                                                   & "   ON HOL.NRS_BR_CD          = HOM.NRS_BR_CD   " & vbNewLine _
                                                   & "  AND HOL.EDI_CTL_NO         = HOM.EDI_CTL_NO  " & vbNewLine _
                                                   & "  AND HOM.SYS_DEL_FLG        = '0'             " & vbNewLine _
                                                   & " LEFT JOIN                                     " & vbNewLine _
                                                   & "      $LM_TRN$..C_OUTKA_L      AS COL          " & vbNewLine _
                                                   & "   ON HOL.NRS_BR_CD          = COL.NRS_BR_CD   " & vbNewLine _
                                                   & "  AND HOL.OUTKA_CTL_NO       = COL.OUTKA_NO_L  " & vbNewLine _
                                                   & "  AND COL.SYS_DEL_FLG        = '0'             " & vbNewLine _
                                                   & " WHERE                                         " & vbNewLine _
                                                   & "      HOL.SYS_DEL_FLG        = '0'             " & vbNewLine _
                                                   & "  AND HOL.DEL_KB            <> '3'             " & vbNewLine

    ''' <summary>
    ''' 抽出条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_CANCER_DATA_DTL As String = "  AND HOL.NRS_BR_CD       = @NRS_BR_CD      --@パラ " & vbNewLine _
                                                   & "  AND HOL.CUST_CD_L          = @CUST_CD_L      --@パラ " & vbNewLine _
                                                   & "  AND HOL.CUST_CD_M          = @CUST_CD_M      --@パラ " & vbNewLine _
                                                   & "  AND HOL.CUST_ORD_NO + HOM.CUST_ORD_NO_DTL      = @CUST_ORD_NO    --@パラ " & vbNewLine _
                                                   & "  ORDER BY COL.OUTKA_NO_L DESC                 --@パラ " & vbNewLine

    ''' <summary>
    ''' キャンセルデータの抽出
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CANCER_DATA_DTL_CNT As String = " SELECT                                        " & vbNewLine _
                                                   & "      COL.OUTKA_NO_L           AS OUTKA_CTL_NO " & vbNewLine _
                                                   & "     ,HOL.EDI_CTL_NO           AS EDI_CTL_NO   " & vbNewLine _
                                                   & " FROM $LM_TRN$..H_OUTKAEDI_L   AS HOL          " & vbNewLine _
                                                   & " LEFT JOIN                                     " & vbNewLine _
                                                   & "      $LM_TRN$..C_OUTKA_L      AS COL          " & vbNewLine _
                                                   & "   ON HOL.NRS_BR_CD          = COL.NRS_BR_CD   " & vbNewLine _
                                                   & "  AND HOL.OUTKA_CTL_NO       = COL.OUTKA_NO_L  " & vbNewLine _
                                                   & "  AND COL.SYS_DEL_FLG        = '0'             " & vbNewLine _
                                                   & " LEFT JOIN                                     " & vbNewLine _
                                                   & "      $LM_TRN$..C_OUTKA_M   AS COM             " & vbNewLine _
                                                   & "   ON COL.NRS_BR_CD          = COM.NRS_BR_CD   " & vbNewLine _
                                                   & "  AND COL.OUTKA_NO_L         = COM.OUTKA_NO_L  " & vbNewLine _
                                                   & "  AND COM.SYS_DEL_FLG        = '0'             " & vbNewLine _
                                                   & " WHERE                                         " & vbNewLine _
                                                   & "      HOL.SYS_DEL_FLG        = '0'             " & vbNewLine _
                                                   & "  AND HOL.DEL_KB            <> '3'             " & vbNewLine _
                                                   & "  AND COL.SYS_DEL_FLG        = '0'             " & vbNewLine _
                                                   & "  AND COM.SYS_DEL_FLG        = '0'             " & vbNewLine

    ''' <summary>
    ''' 抽出条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_CANCER_DATA_DTL_CNT As String = "  AND HOL.NRS_BR_CD   = @NRS_BR_CD      --@パラ " & vbNewLine _
                                                   & "  AND HOL.CUST_CD_L          = @CUST_CD_L      --@パラ " & vbNewLine _
                                                   & "  AND HOL.CUST_CD_M          = @CUST_CD_M      --@パラ " & vbNewLine _
                                                   & "  AND HOL.CUST_ORD_NO        LIKE @CUST_ORD_NO    --@パラ " & vbNewLine

    ''' <summary>
    ''' 抽出条件(明細のみ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_CANCER_DATA_DTL_ONLY_CNT As String = "  AND HOL.NRS_BR_CD   = @NRS_BR_CD      --@パラ " & vbNewLine _
                                                   & "  AND HOL.CUST_CD_L          = @CUST_CD_L      --@パラ " & vbNewLine _
                                                   & "  AND HOL.CUST_CD_M          = @CUST_CD_M      --@パラ " & vbNewLine _
                                                   & "  AND COM.CUST_ORD_NO_DTL    LIKE @CUST_ORD_NO    --@パラ " & vbNewLine

    ''' <summary>
    ''' 抽出条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_CANCER_DATA_DTL_M_FREE_C25 As String = "  AND HOL.NRS_BR_CD   = @NRS_BR_CD --@パラ " & vbNewLine _
                                                   & "  AND HOL.CUST_CD_L          = @CUST_CD_L      --@パラ " & vbNewLine _
                                                   & "  AND HOL.CUST_CD_M          = @CUST_CD_M      --@パラ " & vbNewLine _
                                                   & "  AND HOM.FREE_C25        LIKE @CUST_ORD_NO    --@パラ " & vbNewLine


#End Region

#Region "受信テーブル同一データ抽出"

    ''' <summary>
    ''' 受信テーブル同一データ抽出(アルベマール)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COMPARE_CHECK_ARB As String = " SELECT                                                  " & vbNewLine _
                                                        & "  COUNT(*) AS CNT                                         " & vbNewLine _
                                                        & " ,CNT.NRS_BR_CD                                           " & vbNewLine _
                                                        & " ,CNT.CUST_CD_L                                           " & vbNewLine _
                                                        & " ,CNT.CUST_CD_M                                           " & vbNewLine _
                                                        & " ,CNT.ORDER_NO                                            " & vbNewLine _
                                                        & " ,CNT.ITEM_NO                                             " & vbNewLine _
                                                        & " ,CNT.MATERIAL_NO                                         " & vbNewLine _
                                                        & " ,CNT.SHIP_TO_PARTY_CD                                    " & vbNewLine _
                                                        & " ,CNT.SOLD_TO_PARTY_CD                                    " & vbNewLine _
                                                        & " ,CNT.REF_PO_NUMBER                                       " & vbNewLine _
                                                        & " ,CNT.REQUESTED_DATE                                      " & vbNewLine _
                                                        & " ,CNT.PLANNED_GO_DATE                                     " & vbNewLine _
                                                        & " ,CNT.ORDER_QUANTITY                                      " & vbNewLine _
                                                        & " ,CNT.SO_REJECTION_CD                                     " & vbNewLine _
                                                        & " ,CNT.SYS_DEL_FLG                                         " & vbNewLine _
                                                        & "FROM                                                      " & vbNewLine _
                                                        & "(                                                         " & vbNewLine _
                                                        & " SELECT                                                   " & vbNewLine _
                                                        & "  TOP 1                                                   " & vbNewLine _
                                                        & "  ARB.NRS_BR_CD                                           " & vbNewLine _
                                                        & " ,ARB.CUST_CD_L                                           " & vbNewLine _
                                                        & " ,ARB.CUST_CD_M                                           " & vbNewLine _
                                                        & " ,ARB.ORDER_NO                                            " & vbNewLine _
                                                        & " ,ARB.ITEM_NO                                             " & vbNewLine _
                                                        & " ,ARB.MATERIAL_NO                                         " & vbNewLine _
                                                        & " ,ARB.SHIP_TO_PARTY_CD                                    " & vbNewLine _
                                                        & " ,ARB.SOLD_TO_PARTY_CD                                    " & vbNewLine _
                                                        & " ,ARB.REF_PO_NUMBER                                       " & vbNewLine _
                                                        & " ,ARB.REQUESTED_DATE                                      " & vbNewLine _
                                                        & " ,ARB.PLANNED_GO_DATE                                     " & vbNewLine _
                                                        & " ,ARB.ORDER_QUANTITY                                      " & vbNewLine _
                                                        & " ,ARB.SO_REJECTION_CD                                     " & vbNewLine _
                                                        & " ,ARB.SYS_DEL_FLG                                         " & vbNewLine _
                                                        & " ,ARB.DEL_KB                                              " & vbNewLine _
                                                        & " ,ARB.SYS_ENT_DATE + ARB.SYS_ENT_TIME AS SYS_ENT_DATETIME " & vbNewLine _
                                                        & "  FROM $LM_TRN$..H_OUTKAEDI_DTL_ARB ARB                   " & vbNewLine _
                                                        & " WHERE                                                    " & vbNewLine _
                                                        & " ARB.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " ARB.CUST_CD_L = @CUST_CD_L                               " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " ARB.CUST_CD_M = @CUST_CD_M                               " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " ARB.ORDER_NO = @ORDER_NO                                 " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " ARB.ITEM_NO = @ITEM_NO                                   " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " ARB.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                                        & " GROUP BY                                                 " & vbNewLine _
                                                        & "  ARB.NRS_BR_CD                                           " & vbNewLine _
                                                        & " ,ARB.CUST_CD_L                                           " & vbNewLine _
                                                        & " ,ARB.CUST_CD_M                                           " & vbNewLine _
                                                        & " ,ARB.ORDER_NO                                            " & vbNewLine _
                                                        & " ,ARB.ITEM_NO                                             " & vbNewLine _
                                                        & " ,ARB.MATERIAL_NO                                         " & vbNewLine _
                                                        & " ,ARB.SHIP_TO_PARTY_CD                                    " & vbNewLine _
                                                        & " ,ARB.SOLD_TO_PARTY_CD                                    " & vbNewLine _
                                                        & " ,ARB.REF_PO_NUMBER                                       " & vbNewLine _
                                                        & " ,ARB.REQUESTED_DATE                                      " & vbNewLine _
                                                        & " ,ARB.PLANNED_GO_DATE                                     " & vbNewLine _
                                                        & " ,ARB.ORDER_QUANTITY                                      " & vbNewLine _
                                                        & " ,ARB.SO_REJECTION_CD                                     " & vbNewLine _
                                                        & " ,ARB.SYS_DEL_FLG                                         " & vbNewLine _
                                                        & " ,ARB.DEL_KB                                              " & vbNewLine _
                                                        & " ,ARB.SYS_ENT_DATE + ARB.SYS_ENT_TIME                     " & vbNewLine _
                                                        & " ORDER BY                                                 " & vbNewLine _
                                                        & "  ARB.SYS_ENT_DATE + ARB.SYS_ENT_TIME DESC                " & vbNewLine _
                                                        & "  ) CNT                                                   " & vbNewLine _
                                                        & " WHERE                                                    " & vbNewLine _
                                                        & " CNT.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.CUST_CD_L = @CUST_CD_L                               " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.CUST_CD_M = @CUST_CD_M                               " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.ORDER_NO = @ORDER_NO                                 " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.ITEM_NO = @ITEM_NO                                   " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.MATERIAL_NO = @MATERIAL_NO                           " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.SHIP_TO_PARTY_CD = @SHIP_TO_PARTY_CD                 " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.SOLD_TO_PARTY_CD = @SOLD_TO_PARTY_CD                 " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.REF_PO_NUMBER = @REF_PO_NUMBER                       " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.REQUESTED_DATE = @REQUESTED_DATE                     " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.PLANNED_GO_DATE = @PLANNED_GO_DATE                   " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.ORDER_QUANTITY = @ORDER_QUANTITY                     " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.SO_REJECTION_CD = @SO_REJECTION_CD                   " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                                        & " GROUP BY                                                 " & vbNewLine _
                                                        & "   CNT.NRS_BR_CD                                          " & vbNewLine _
                                                        & " ,CNT.CUST_CD_L                                           " & vbNewLine _
                                                        & " ,CNT.CUST_CD_M                                           " & vbNewLine _
                                                        & " ,CNT.ORDER_NO                                            " & vbNewLine _
                                                        & " ,CNT.ITEM_NO                                             " & vbNewLine _
                                                        & " ,CNT.MATERIAL_NO                                         " & vbNewLine _
                                                        & " ,CNT.SHIP_TO_PARTY_CD                                    " & vbNewLine _
                                                        & " ,CNT.SOLD_TO_PARTY_CD                                    " & vbNewLine _
                                                        & " ,CNT.REF_PO_NUMBER                                       " & vbNewLine _
                                                        & " ,CNT.REQUESTED_DATE                                      " & vbNewLine _
                                                        & " ,CNT.PLANNED_GO_DATE                                     " & vbNewLine _
                                                        & " ,CNT.ORDER_QUANTITY                                      " & vbNewLine _
                                                        & " ,CNT.SO_REJECTION_CD                                     " & vbNewLine _
                                                        & " ,CNT.SYS_DEL_FLG                                         " & vbNewLine

    'Private Const SQL_SELECT_COMPARE_CHECK_ARB As String = " SELECT                                   " & vbNewLine _
    '                                                     & "COUNT(*) AS CNT                           " & vbNewLine _
    '                                                     & "FROM $LM_TRN$..H_OUTKAEDI_DTL_ARB ARB     " & vbNewLine _
    '                                                     & "WHERE                                     " & vbNewLine _
    '                                                     & "ARB.NRS_BR_CD = @NRS_BR_CD                " & vbNewLine _
    '                                                     & "AND                                       " & vbNewLine _
    '                                                     & "ARB.CUST_CD_L = @CUST_CD_L                " & vbNewLine _
    '                                                     & "AND                                       " & vbNewLine _
    '                                                     & "ARB.CUST_CD_M = @CUST_CD_M                " & vbNewLine _
    '                                                     & "AND                                       " & vbNewLine _
    '                                                     & "ARB.ORDER_NO = @ORDER_NO                  " & vbNewLine _
    '                                                     & "AND                                       " & vbNewLine _
    '                                                     & "ARB.ITEM_NO = @ITEM_NO                    " & vbNewLine _
    '                                                     & "AND                                       " & vbNewLine _
    '                                                     & "ARB.MATERIAL_NO = @MATERIAL_NO            " & vbNewLine _
    '                                                     & "AND                                       " & vbNewLine _
    '                                                     & "ARB.SHIP_TO_PARTY_CD = @SHIP_TO_PARTY_CD  " & vbNewLine _
    '                                                     & "AND                                       " & vbNewLine _
    '                                                     & "ARB.SOLD_TO_PARTY_CD = @SOLD_TO_PARTY_CD  " & vbNewLine _
    '                                                     & "AND                                       " & vbNewLine _
    '                                                     & "ARB.REF_PO_NUMBER = @REF_PO_NUMBER        " & vbNewLine _
    '                                                     & "AND                                       " & vbNewLine _
    '                                                     & "ARB.REQUESTED_DATE = @REQUESTED_DATE      " & vbNewLine _
    '                                                     & "AND                                       " & vbNewLine _
    '                                                     & "ARB.PLANNED_GO_DATE = @PLANNED_GO_DATE    " & vbNewLine _
    '                                                     & "AND                                       " & vbNewLine _
    '                                                     & "ARB.ORDER_QUANTITY = @ORDER_QUANTITY      " & vbNewLine _
    '                                                     & "AND                                       " & vbNewLine _
    '                                                     & "ARB.SO_REJECTION_CD = @SO_REJECTION_CD    " & vbNewLine _
    '                                                     & "AND                                       " & vbNewLine _
    '                                                     & "ARB.SYS_DEL_FLG = '0'                     " & vbNewLine

    '2015.05.21 追加START 契約者コード削除START
    ''' <summary>
    ''' 受信テーブル同一データ抽出(ＭＲＣデュポン)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COMPARE_CHECK_MRC As String = " SELECT                                                  " & vbNewLine _
                                                        & "  COUNT(*) AS CNT                                         " & vbNewLine _
                                                        & " ,CNT.NRS_BR_CD                                           " & vbNewLine _
                                                        & " ,CNT.CUST_CD_L                                           " & vbNewLine _
                                                        & " ,CNT.CUST_CD_M                                           " & vbNewLine _
                                                        & "-- ,CNT.KEIYAKUSAKI_CD                                      " & vbNewLine _
                                                        & " ,CNT.TORIHIKI_NO                                         " & vbNewLine _
                                                        & " ,CNT.SHUKKASAKI_CD                                       " & vbNewLine _
                                                        & " ,CNT.SHUKKASAKI_NM                                       " & vbNewLine _
                                                        & " ,CNT.ADDRESS                                             " & vbNewLine _
                                                        & " ,CNT.GOODS_NM                                            " & vbNewLine _
                                                        & " ,CNT.SASIZU_SURYO                                        " & vbNewLine _
                                                        & " ,CNT.SHUKKA_BI                                           " & vbNewLine _
                                                        & " ,CNT.NOUKI                                               " & vbNewLine _
                                                        & " ,CNT.BIKO                                                " & vbNewLine _
                                                        & " ,CNT.SYS_DEL_FLG                                         " & vbNewLine _
                                                        & "FROM                                                      " & vbNewLine _
                                                        & "(                                                         " & vbNewLine _
                                                        & " SELECT                                                   " & vbNewLine _
                                                        & "  TOP 1                                                   " & vbNewLine _
                                                        & "  MRC.NRS_BR_CD                                           " & vbNewLine _
                                                        & " ,MRC.CUST_CD_L                                           " & vbNewLine _
                                                        & " ,MRC.CUST_CD_M                                           " & vbNewLine _
                                                        & "-- ,MRC.KEIYAKUSAKI_CD                                      " & vbNewLine _
                                                        & " ,MRC.TORIHIKI_NO                                         " & vbNewLine _
                                                        & " ,MRC.SHUKKASAKI_CD                                       " & vbNewLine _
                                                        & " ,MRC.SHUKKASAKI_NM                                       " & vbNewLine _
                                                        & " ,MRC.ADDRESS                                             " & vbNewLine _
                                                        & " ,MRC.GOODS_NM                                            " & vbNewLine _
                                                        & " ,MRC.SASIZU_SURYO                                        " & vbNewLine _
                                                        & " ,MRC.SHUKKA_BI                                           " & vbNewLine _
                                                        & " ,MRC.NOUKI                                               " & vbNewLine _
                                                        & " ,MRC.BIKO                                                " & vbNewLine _
                                                        & " ,MRC.SYS_DEL_FLG                                         " & vbNewLine _
                                                        & " ,MRC.DEL_KB                                              " & vbNewLine _
                                                        & " ,MRC.SYS_ENT_DATE + MRC.SYS_ENT_TIME AS SYS_ENT_DATETIME " & vbNewLine _
                                                        & "  FROM $LM_TRN$..H_OUTKAEDI_DTL_MRC MRC                   " & vbNewLine _
                                                        & " WHERE                                                    " & vbNewLine _
                                                        & " MRC.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " MRC.CUST_CD_L = @CUST_CD_L                               " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " MRC.CUST_CD_M = @CUST_CD_M                               " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " MRC.TORIHIKI_NO = @TORIHIKI_NO                           " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " MRC.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                                        & " GROUP BY                                                 " & vbNewLine _
                                                        & "  MRC.NRS_BR_CD                                           " & vbNewLine _
                                                        & " ,MRC.CUST_CD_L                                           " & vbNewLine _
                                                        & " ,MRC.CUST_CD_M                                           " & vbNewLine _
                                                        & "-- ,MRC.KEIYAKUSAKI_CD                                      " & vbNewLine _
                                                        & " ,MRC.TORIHIKI_NO                                         " & vbNewLine _
                                                        & " ,MRC.SHUKKASAKI_CD                                       " & vbNewLine _
                                                        & " ,MRC.SHUKKASAKI_NM                                       " & vbNewLine _
                                                        & " ,MRC.ADDRESS                                             " & vbNewLine _
                                                        & " ,MRC.GOODS_NM                                            " & vbNewLine _
                                                        & " ,MRC.SASIZU_SURYO                                        " & vbNewLine _
                                                        & " ,MRC.SHUKKA_BI                                           " & vbNewLine _
                                                        & " ,MRC.NOUKI                                               " & vbNewLine _
                                                        & " ,MRC.BIKO                                                " & vbNewLine _
                                                        & " ,MRC.SYS_DEL_FLG                                         " & vbNewLine _
                                                        & " ,MRC.DEL_KB                                              " & vbNewLine _
                                                        & " ,MRC.SYS_ENT_DATE + MRC.SYS_ENT_TIME                     " & vbNewLine _
                                                        & " ORDER BY                                                 " & vbNewLine _
                                                        & "  MRC.SYS_ENT_DATE + MRC.SYS_ENT_TIME DESC                " & vbNewLine _
                                                        & "  ) CNT                                                   " & vbNewLine _
                                                        & " WHERE                                                    " & vbNewLine _
                                                        & " CNT.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.CUST_CD_L = @CUST_CD_L                               " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.CUST_CD_M = @CUST_CD_M                               " & vbNewLine _
                                                        & "-- AND                                                      " & vbNewLine _
                                                        & "-- CNT.KEIYAKUSAKI_CD = @KEIYAKUSAKI_CD                     " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.TORIHIKI_NO = @TORIHIKI_NO                           " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.SHUKKASAKI_CD = @SHUKKASAKI_CD                       " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.SHUKKASAKI_NM = @SHUKKASAKI_NM                       " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.ADDRESS = @ADDRESS                                   " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.GOODS_NM = @GOODS_NM                                 " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.SASIZU_SURYO = @SASIZU_SURYO                         " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.SHUKKA_BI = @SHUKKA_BI                               " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.NOUKI = @NOUKI                                       " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.BIKO = @BIKO                                         " & vbNewLine _
                                                        & " AND                                                      " & vbNewLine _
                                                        & " CNT.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                                        & " GROUP BY                                                 " & vbNewLine _
                                                        & "  CNT.NRS_BR_CD                                           " & vbNewLine _
                                                        & " ,CNT.CUST_CD_L                                           " & vbNewLine _
                                                        & " ,CNT.CUST_CD_M                                           " & vbNewLine _
                                                        & "-- ,CNT.KEIYAKUSAKI_CD                                      " & vbNewLine _
                                                        & " ,CNT.TORIHIKI_NO                                         " & vbNewLine _
                                                        & " ,CNT.SHUKKASAKI_CD                                       " & vbNewLine _
                                                        & " ,CNT.SHUKKASAKI_NM                                       " & vbNewLine _
                                                        & " ,CNT.ADDRESS                                             " & vbNewLine _
                                                        & " ,CNT.GOODS_NM                                            " & vbNewLine _
                                                        & " ,CNT.SASIZU_SURYO                                        " & vbNewLine _
                                                        & " ,CNT.SHUKKA_BI                                           " & vbNewLine _
                                                        & " ,CNT.NOUKI                                               " & vbNewLine _
                                                        & " ,CNT.BIKO                                                " & vbNewLine _
                                                        & " ,CNT.SYS_DEL_FLG                                         " & vbNewLine
    '2015.05.21 追加START 契約者コード削除END

#End Region

#Region "H_OUTKAEDI_DTL_ARB(INSERT)"

    ''' <summary>
    ''' INSERT（H_OUTKAEDI_DTL_ARB）
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_INSERT_OUTKAEDI_DTL_ARB As String = "INSERT INTO    " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_DTL_ARB   " & vbNewLine _
                                        & "(                                " & vbNewLine _
                                        & " DEL_KB                          " & vbNewLine _
                                        & ",CRT_DATE                        " & vbNewLine _
                                        & ",FILE_NAME                       " & vbNewLine _
                                        & ",REC_NO                          " & vbNewLine _
                                        & ",GYO                             " & vbNewLine _
                                        & ",NRS_BR_CD                       " & vbNewLine _
                                        & ",EDI_CTL_NO                      " & vbNewLine _
                                        & ",EDI_CTL_NO_CHU                  " & vbNewLine _
                                        & ",OUTKA_CTL_NO                    " & vbNewLine _
                                        & ",OUTKA_CTL_NO_CHU                " & vbNewLine _
                                        & ",CUST_CD_L                       " & vbNewLine _
                                        & ",CUST_CD_M                       " & vbNewLine _
                                        & ",PRTFLG                          " & vbNewLine _
                                        & ",ORDER_NO                        " & vbNewLine _
                                        & ",ITEM_NO                         " & vbNewLine _
                                        & ",SO_PO_CREATE_USER               " & vbNewLine _
                                        & ",MATERIAL_NO                     " & vbNewLine _
                                        & ",SKU_DESCRIPTION                 " & vbNewLine _
                                        & ",PLANT                           " & vbNewLine _
                                        & ",SHIP_TO_PARTY_CD                " & vbNewLine _
                                        & ",SHIP_TO_PARTY_NAME              " & vbNewLine _
                                        & ",SOLD_TO_PARTY_CD                " & vbNewLine _
                                        & ",SOLD_TO_PARTY_NAME              " & vbNewLine _
                                        & ",REF_PO_NUMBER                   " & vbNewLine _
                                        & ",SO_REJECTION_CD                 " & vbNewLine _
                                        & ",SO_PO_CREATE_DATE               " & vbNewLine _
                                        & ",REQUESTED_DATE                  " & vbNewLine _
                                        & ",PLANNED_GO_DATE                 " & vbNewLine _
                                        & ",ORDER_UNIT                      " & vbNewLine _
                                        & ",ORDER_QUANTITY                  " & vbNewLine _
                                        & ",RECORD_STATUS                   " & vbNewLine _
                                        & ",JISSEKI_SHORI_FLG               " & vbNewLine _
                                        & ",JISSEKI_USER                    " & vbNewLine _
                                        & ",JISSEKI_DATE                    " & vbNewLine _
                                        & ",JISSEKI_TIME                    " & vbNewLine _
                                        & ",SEND_USER                       " & vbNewLine _
                                        & ",SEND_DATE                       " & vbNewLine _
                                        & ",SEND_TIME                       " & vbNewLine _
                                        & ",DELETE_USER                     " & vbNewLine _
                                        & ",DELETE_DATE                     " & vbNewLine _
                                        & ",DELETE_TIME                     " & vbNewLine _
                                        & ",DELETE_EDI_NO                   " & vbNewLine _
                                        & ",DELETE_EDI_NO_CHU               " & vbNewLine _
                                        & ",PRT_USER                        " & vbNewLine _
                                        & ",PRT_DATE                        " & vbNewLine _
                                        & ",PRT_TIME                        " & vbNewLine _
                                        & ",EDI_USER                        " & vbNewLine _
                                        & ",EDI_DATE                        " & vbNewLine _
                                        & ",EDI_TIME                        " & vbNewLine _
                                        & ",OUTKA_USER                      " & vbNewLine _
                                        & ",OUTKA_DATE                      " & vbNewLine _
                                        & ",OUTKA_TIME                      " & vbNewLine _
                                        & ",UPD_USER                        " & vbNewLine _
                                        & ",UPD_DATE                        " & vbNewLine _
                                        & ",UPD_TIME                        " & vbNewLine _
                                        & ",SYS_ENT_DATE                    " & vbNewLine _
                                        & ",SYS_ENT_TIME                    " & vbNewLine _
                                        & ",SYS_ENT_PGID                    " & vbNewLine _
                                        & ",SYS_ENT_USER                    " & vbNewLine _
                                        & ",SYS_UPD_DATE                    " & vbNewLine _
                                        & ",SYS_UPD_TIME                    " & vbNewLine _
                                        & ",SYS_UPD_PGID                    " & vbNewLine _
                                        & ",SYS_UPD_USER                    " & vbNewLine _
                                        & ",SYS_DEL_FLG                     " & vbNewLine _
                                        & ")VALUES(                        " & vbNewLine _
                                        & " @DEL_KB                           " & vbNewLine _
                                        & ",@CRT_DATE                        " & vbNewLine _
                                        & ",@FILE_NAME                       " & vbNewLine _
                                        & ",@REC_NO                          " & vbNewLine _
                                        & ",@GYO                             " & vbNewLine _
                                        & ",@NRS_BR_CD                       " & vbNewLine _
                                        & ",@EDI_CTL_NO                      " & vbNewLine _
                                        & ",@EDI_CTL_NO_CHU                  " & vbNewLine _
                                        & ",@OUTKA_CTL_NO                    " & vbNewLine _
                                        & ",@OUTKA_CTL_NO_CHU                " & vbNewLine _
                                        & ",@CUST_CD_L                       " & vbNewLine _
                                        & ",@CUST_CD_M                       " & vbNewLine _
                                        & ",@PRTFLG                          " & vbNewLine _
                                        & ",@ORDER_NO                        " & vbNewLine _
                                        & ",@ITEM_NO                         " & vbNewLine _
                                        & ",@SO_PO_CREATE_USER               " & vbNewLine _
                                        & ",@MATERIAL_NO                     " & vbNewLine _
                                        & ",@SKU_DESCRIPTION                 " & vbNewLine _
                                        & ",@PLANT                           " & vbNewLine _
                                        & ",@SHIP_TO_PARTY_CD                " & vbNewLine _
                                        & ",@SHIP_TO_PARTY_NAME              " & vbNewLine _
                                        & ",@SOLD_TO_PARTY_CD                " & vbNewLine _
                                        & ",@SOLD_TO_PARTY_NAME              " & vbNewLine _
                                        & ",@REF_PO_NUMBER                   " & vbNewLine _
                                        & ",@SO_REJECTION_CD                 " & vbNewLine _
                                        & ",@SO_PO_CREATE_DATE               " & vbNewLine _
                                        & ",@REQUESTED_DATE                  " & vbNewLine _
                                        & ",@PLANNED_GO_DATE                 " & vbNewLine _
                                        & ",@ORDER_UNIT                      " & vbNewLine _
                                        & ",@ORDER_QUANTITY                  " & vbNewLine _
                                        & ",@RECORD_STATUS                   " & vbNewLine _
                                        & ",@JISSEKI_SHORI_FLG               " & vbNewLine _
                                        & ",@JISSEKI_USER                    " & vbNewLine _
                                        & ",@JISSEKI_DATE                    " & vbNewLine _
                                        & ",@JISSEKI_TIME                    " & vbNewLine _
                                        & ",@SEND_USER                       " & vbNewLine _
                                        & ",@SEND_DATE                       " & vbNewLine _
                                        & ",@SEND_TIME                       " & vbNewLine _
                                        & ",@DELETE_USER                     " & vbNewLine _
                                        & ",@DELETE_DATE                     " & vbNewLine _
                                        & ",@DELETE_TIME                     " & vbNewLine _
                                        & ",@DELETE_EDI_NO                   " & vbNewLine _
                                        & ",@DELETE_EDI_NO_CHU               " & vbNewLine _
                                        & ",@PRT_USER                        " & vbNewLine _
                                        & ",@PRT_DATE                        " & vbNewLine _
                                        & ",@PRT_TIME                        " & vbNewLine _
                                        & ",@EDI_USER                        " & vbNewLine _
                                        & ",@EDI_DATE                        " & vbNewLine _
                                        & ",@EDI_TIME                        " & vbNewLine _
                                        & ",@OUTKA_USER                      " & vbNewLine _
                                        & ",@OUTKA_DATE                      " & vbNewLine _
                                        & ",@OUTKA_TIME                      " & vbNewLine _
                                        & ",@UPD_USER                        " & vbNewLine _
                                        & ",@UPD_DATE                        " & vbNewLine _
                                        & ",@UPD_TIME                        " & vbNewLine _
                                        & ",@SYS_ENT_DATE                    " & vbNewLine _
                                        & ",@SYS_ENT_TIME                    " & vbNewLine _
                                        & ",@SYS_ENT_PGID                    " & vbNewLine _
                                        & ",@SYS_ENT_USER                    " & vbNewLine _
                                        & ",@SYS_UPD_DATE                    " & vbNewLine _
                                        & ",@SYS_UPD_TIME                    " & vbNewLine _
                                        & ",@SYS_UPD_PGID                    " & vbNewLine _
                                        & ",@SYS_UPD_USER                    " & vbNewLine _
                                        & ",@SYS_DEL_FLG                     " & vbNewLine _
                                        & ")" & vbNewLine

#End Region

#Region "H_OUTKAEDI_DTL_MRC(INSERT)"

    ''' <summary>
    ''' INSERT（H_OUTKAEDI_DTL_MRC）
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_INSERT_OUTKAEDI_DTL_MRC As String = "INSERT INTO    " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_DTL_MRC   " & vbNewLine _
                                        & "(                                " & vbNewLine _
                                        & " DEL_KB                          " & vbNewLine _
                                        & ",CRT_DATE                        " & vbNewLine _
                                        & ",FILE_NAME                       " & vbNewLine _
                                        & ",REC_NO                          " & vbNewLine _
                                        & ",GYO                             " & vbNewLine _
                                        & ",NRS_BR_CD                       " & vbNewLine _
                                        & ",EDI_CTL_NO                      " & vbNewLine _
                                        & ",EDI_CTL_NO_CHU                  " & vbNewLine _
                                        & ",OUTKA_CTL_NO                    " & vbNewLine _
                                        & ",OUTKA_CTL_NO_CHU                " & vbNewLine _
                                        & ",CUST_CD_L                       " & vbNewLine _
                                        & ",CUST_CD_M                       " & vbNewLine _
                                        & ",PRTFLG                          " & vbNewLine _
                                        & ",SOKO_CD                         " & vbNewLine _
                                        & ",KEIYAKUSAKI_CD                  " & vbNewLine _
                                        & ",TORIHIKI_NO                     " & vbNewLine _
                                        & ",SHUKKASAKI_CD                   " & vbNewLine _
                                        & ",SHUKKASAKI_NM                   " & vbNewLine _
                                        & ",ADDRESS                         " & vbNewLine _
                                        & ",SEIHIN_BUNRUI                   " & vbNewLine _
                                        & ",GOODS_NM                        " & vbNewLine _
                                        & ",SIKICHOU_NM                     " & vbNewLine _
                                        & ",KYAKUSAKI_HINBAN                " & vbNewLine _
                                        & ",SASIZU_SURYO                    " & vbNewLine _
                                        & ",SASIZU_SURYO_TANI               " & vbNewLine _
                                        & ",KANRI_SURYO                     " & vbNewLine _
                                        & ",KANRI_SURYO_TANI                " & vbNewLine _
                                        & ",LOT_NO_SITEI                    " & vbNewLine _
                                        & ",SHUKKA_BI                       " & vbNewLine _
                                        & ",NOUKI                           " & vbNewLine _
                                        & ",BIKO                            " & vbNewLine _
                                        & ",SITEI_TRUCK                     " & vbNewLine _
                                        & ",UKEHARAI_KBN                    " & vbNewLine _
                                        & ",JISSEKI_SHORI_FLG               " & vbNewLine _
                                        & ",JISSEKI_USER                    " & vbNewLine _
                                        & ",JISSEKI_DATE                    " & vbNewLine _
                                        & ",JISSEKI_TIME                    " & vbNewLine _
                                        & ",SEND_USER                       " & vbNewLine _
                                        & ",SEND_DATE                       " & vbNewLine _
                                        & ",SEND_TIME                       " & vbNewLine _
                                        & ",DELETE_USER                     " & vbNewLine _
                                        & ",DELETE_DATE                     " & vbNewLine _
                                        & ",DELETE_TIME                     " & vbNewLine _
                                        & ",DELETE_EDI_NO                   " & vbNewLine _
                                        & ",DELETE_EDI_NO_CHU               " & vbNewLine _
                                        & ",PRT_USER                        " & vbNewLine _
                                        & ",PRT_DATE                        " & vbNewLine _
                                        & ",PRT_TIME                        " & vbNewLine _
                                        & ",EDI_USER                        " & vbNewLine _
                                        & ",EDI_DATE                        " & vbNewLine _
                                        & ",EDI_TIME                        " & vbNewLine _
                                        & ",OUTKA_USER                      " & vbNewLine _
                                        & ",OUTKA_DATE                      " & vbNewLine _
                                        & ",OUTKA_TIME                      " & vbNewLine _
                                        & ",UPD_USER                        " & vbNewLine _
                                        & ",UPD_DATE                        " & vbNewLine _
                                        & ",UPD_TIME                        " & vbNewLine _
                                        & ",SYS_ENT_DATE                    " & vbNewLine _
                                        & ",SYS_ENT_TIME                    " & vbNewLine _
                                        & ",SYS_ENT_PGID                    " & vbNewLine _
                                        & ",SYS_ENT_USER                    " & vbNewLine _
                                        & ",SYS_UPD_DATE                    " & vbNewLine _
                                        & ",SYS_UPD_TIME                    " & vbNewLine _
                                        & ",SYS_UPD_PGID                    " & vbNewLine _
                                        & ",SYS_UPD_USER                    " & vbNewLine _
                                        & ",SYS_DEL_FLG                     " & vbNewLine _
                                        & ")VALUES(                          " & vbNewLine _
                                        & " @DEL_KB                          " & vbNewLine _
                                        & ",@CRT_DATE                        " & vbNewLine _
                                        & ",@FILE_NAME                       " & vbNewLine _
                                        & ",@REC_NO                          " & vbNewLine _
                                        & ",@GYO                             " & vbNewLine _
                                        & ",@NRS_BR_CD                       " & vbNewLine _
                                        & ",@EDI_CTL_NO                      " & vbNewLine _
                                        & ",@EDI_CTL_NO_CHU                  " & vbNewLine _
                                        & ",@OUTKA_CTL_NO                    " & vbNewLine _
                                        & ",@OUTKA_CTL_NO_CHU                " & vbNewLine _
                                        & ",@CUST_CD_L                       " & vbNewLine _
                                        & ",@CUST_CD_M                       " & vbNewLine _
                                        & ",@PRTFLG                          " & vbNewLine _
                                        & ",@SOKO_CD                         " & vbNewLine _
                                        & ",@KEIYAKUSAKI_CD                  " & vbNewLine _
                                        & ",@TORIHIKI_NO                     " & vbNewLine _
                                        & ",@SHUKKASAKI_CD                   " & vbNewLine _
                                        & ",@SHUKKASAKI_NM                   " & vbNewLine _
                                        & ",@ADDRESS                         " & vbNewLine _
                                        & ",@SEIHIN_BUNRUI                   " & vbNewLine _
                                        & ",@GOODS_NM                        " & vbNewLine _
                                        & ",@SIKICHOU_NM                     " & vbNewLine _
                                        & ",@KYAKUSAKI_HINBAN                " & vbNewLine _
                                        & ",@SASIZU_SURYO                    " & vbNewLine _
                                        & ",@SASIZU_SURYO_TANI               " & vbNewLine _
                                        & ",@KANRI_SURYO                     " & vbNewLine _
                                        & ",@KANRI_SURYO_TANI                " & vbNewLine _
                                        & ",@LOT_NO_SITEI                    " & vbNewLine _
                                        & ",@SHUKKA_BI                       " & vbNewLine _
                                        & ",@NOUKI                           " & vbNewLine _
                                        & ",@BIKO                            " & vbNewLine _
                                        & ",@SITEI_TRUCK                     " & vbNewLine _
                                        & ",@UKEHARAI_KBN                    " & vbNewLine _
                                        & ",@JISSEKI_SHORI_FLG               " & vbNewLine _
                                        & ",@JISSEKI_USER                    " & vbNewLine _
                                        & ",@JISSEKI_DATE                    " & vbNewLine _
                                        & ",@JISSEKI_TIME                    " & vbNewLine _
                                        & ",@SEND_USER                       " & vbNewLine _
                                        & ",@SEND_DATE                       " & vbNewLine _
                                        & ",@SEND_TIME                       " & vbNewLine _
                                        & ",@DELETE_USER                     " & vbNewLine _
                                        & ",@DELETE_DATE                     " & vbNewLine _
                                        & ",@DELETE_TIME                     " & vbNewLine _
                                        & ",@DELETE_EDI_NO                   " & vbNewLine _
                                        & ",@DELETE_EDI_NO_CHU               " & vbNewLine _
                                        & ",@PRT_USER                        " & vbNewLine _
                                        & ",@PRT_DATE                        " & vbNewLine _
                                        & ",@PRT_TIME                        " & vbNewLine _
                                        & ",@EDI_USER                        " & vbNewLine _
                                        & ",@EDI_DATE                        " & vbNewLine _
                                        & ",@EDI_TIME                        " & vbNewLine _
                                        & ",@OUTKA_USER                      " & vbNewLine _
                                        & ",@OUTKA_DATE                      " & vbNewLine _
                                        & ",@OUTKA_TIME                      " & vbNewLine _
                                        & ",@UPD_USER                        " & vbNewLine _
                                        & ",@UPD_DATE                        " & vbNewLine _
                                        & ",@UPD_TIME                        " & vbNewLine _
                                        & ",@SYS_ENT_DATE                    " & vbNewLine _
                                        & ",@SYS_ENT_TIME                    " & vbNewLine _
                                        & ",@SYS_ENT_PGID                    " & vbNewLine _
                                        & ",@SYS_ENT_USER                    " & vbNewLine _
                                        & ",@SYS_UPD_DATE                    " & vbNewLine _
                                        & ",@SYS_UPD_TIME                    " & vbNewLine _
                                        & ",@SYS_UPD_PGID                    " & vbNewLine _
                                        & ",@SYS_UPD_USER                    " & vbNewLine _
                                        & ",@SYS_DEL_FLG                     " & vbNewLine _
                                        & ")" & vbNewLine

#End Region

#End Region

#Region "セミEDI(画面取込)処理 更新用SQL)"
#Region "H_OUTKAEDI_L(取込削除時)"
    ''' <summary>
    ''' EDI出荷(大)セミEDI(取込削除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DEL_OUTKAIEDI_L As String _
                        = " UPDATE $LM_TRN$..H_OUTKAEDI_L  SET          " & vbNewLine _
                        & " DEL_KB              = '1'                   " & vbNewLine _
                        & ",UPD_USER            = @UPD_USER             " & vbNewLine _
                        & ",UPD_DATE            = @UPD_DATE             " & vbNewLine _
                        & ",UPD_TIME            = @UPD_TIME             " & vbNewLine _
                        & ",SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                        & ",SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                        & ",SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                        & ",SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                        & ",SYS_DEL_FLG         = '1'                   " & vbNewLine _
                        & " WHERE NRS_BR_CD     = @NRS_BR_CD            " & vbNewLine _
                        & " AND   EDI_CTL_NO    = @EDI_CTL_NO           " & vbNewLine


#End Region

#Region "H_OUTKAEDI_M(取込削除)"
    ''' <summary>
    ''' EDI出荷(中)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DEL_OUTKAIEDI_M As String _
                        = " UPDATE $LM_TRN$..H_OUTKAEDI_M  SET          " & vbNewLine _
                        & " DEL_KB              = '1'                   " & vbNewLine _
                        & ",UPD_USER            = @UPD_USER             " & vbNewLine _
                        & ",UPD_DATE            = @UPD_DATE             " & vbNewLine _
                        & ",UPD_TIME            = @UPD_TIME             " & vbNewLine _
                        & ",SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                        & ",SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                        & ",SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                        & ",SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                        & ",SYS_DEL_FLG         = '1'                   " & vbNewLine _
                        & " WHERE NRS_BR_CD     = @NRS_BR_CD            " & vbNewLine _
                        & " AND  EDI_CTL_NO     = @EDI_CTL_NO           " & vbNewLine _
                        & " --AND  EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU     " & vbNewLine
#End Region

#Region "H_OUTKAEDI_DTL_ARB(取込削除)"
    ' ''' <summary>
    ' ''' アルベマール受信テーブル(DTL)更新
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Const SQL_UPDATE_DEL_OUTKAIEDI_DTL_ARB As String _
    '                    = " UPDATE $LM_TRN$..H_OUTKAEDI_DTL_ARB  SET    " & vbNewLine _
    '                    & " DEL_KB              = '1'                   " & vbNewLine _
    '                    & "--,DELETE_USER         = @DELETE_USER   	    " & vbNewLine _
    '                    & "--,DELETE_DATE         = @DELETE_DATE       	" & vbNewLine _
    '                    & "--,DELETE_TIME         = @DELETE_TIME          " & vbNewLine _
    '                    & "--,DELETE_EDI_NO       = @DELETE_EDI_NO        " & vbNewLine _
    '                    & "--,DELETE_EDI_NO_CHU   = @DELETE_EDI_NO_CHU    " & vbNewLine _
    '                    & ",UPD_USER            = @UPD_USER             " & vbNewLine _
    '                    & ",UPD_DATE            = @UPD_DATE             " & vbNewLine _
    '                    & ",UPD_TIME            = @UPD_TIME             " & vbNewLine _
    '                    & ",SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
    '                    & ",SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
    '                    & ",SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
    '                    & ",SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
    '                    & ",SYS_DEL_FLG         = '1'                   " & vbNewLine _
    '                    & " WHERE NRS_BR_CD     = @NRS_BR_CD            " & vbNewLine _
    '                    & " AND  EDI_CTL_NO     = @EDI_CTL_NO           " & vbNewLine


    ''' <summary>
    ''' 受信テーブル(DTL)共通更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DEL_OUTKAIEDI_DTL As String _
                        = " UPDATE $LM_TRN$..$RCV_NM$  SET    " & vbNewLine _
                        & " DEL_KB              = '1'                   " & vbNewLine _
                        & ",UPD_USER            = @UPD_USER             " & vbNewLine _
                        & ",UPD_DATE            = @UPD_DATE             " & vbNewLine _
                        & ",UPD_TIME            = @UPD_TIME             " & vbNewLine _
                        & ",SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                        & ",SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                        & ",SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                        & ",SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                        & ",SYS_DEL_FLG         = '1'                   " & vbNewLine _
                        & " WHERE NRS_BR_CD     = @NRS_BR_CD            " & vbNewLine _
                        & " AND  EDI_CTL_NO     = @EDI_CTL_NO           " & vbNewLine

#End Region

#End Region

#Region "セミEDI(画面取込)処理 新規用SQL"
#Region "H_OUTKAEDI_L(INSERT)"
    ''' <summary>
    ''' INSERT（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTKAEDI_L As String = "INSERT INTO          " & vbNewLine _
                                         & "$LM_TRN$..H_OUTKAEDI_L        " & vbNewLine _
                                         & "(                          " & vbNewLine _
                                         & " DEL_KB                    " & vbNewLine _
                                         & ",NRS_BR_CD                 " & vbNewLine _
                                         & ",EDI_CTL_NO                " & vbNewLine _
                                         & ",OUTKA_CTL_NO              " & vbNewLine _
                                         & ",OUTKA_KB                  " & vbNewLine _
                                         & ",SYUBETU_KB                " & vbNewLine _
                                         & ",NAIGAI_KB                 " & vbNewLine _
                                         & ",OUTKA_STATE_KB            " & vbNewLine _
                                         & ",OUTKAHOKOKU_YN            " & vbNewLine _
                                         & ",PICK_KB                   " & vbNewLine _
                                         & ",NRS_BR_NM                 " & vbNewLine _
                                         & ",WH_CD                     " & vbNewLine _
                                         & ",WH_NM                     " & vbNewLine _
                                         & ",OUTKA_PLAN_DATE           " & vbNewLine _
                                         & ",OUTKO_DATE                " & vbNewLine _
                                         & ",ARR_PLAN_DATE             " & vbNewLine _
                                         & ",ARR_PLAN_TIME             " & vbNewLine _
                                         & ",HOKOKU_DATE               " & vbNewLine _
                                         & ",TOUKI_HOKAN_YN            " & vbNewLine _
                                         & ",CUST_CD_L                 " & vbNewLine _
                                         & ",CUST_CD_M                 " & vbNewLine _
                                         & ",CUST_NM_L                 " & vbNewLine _
                                         & ",CUST_NM_M                 " & vbNewLine _
                                         & ",SHIP_CD_L                 " & vbNewLine _
                                         & ",SHIP_CD_M                 " & vbNewLine _
                                         & ",SHIP_NM_L                 " & vbNewLine _
                                         & ",SHIP_NM_M                 " & vbNewLine _
                                         & ",EDI_DEST_CD               " & vbNewLine _
                                         & ",DEST_CD                   " & vbNewLine _
                                         & ",DEST_NM                   " & vbNewLine _
                                         & ",DEST_ZIP                  " & vbNewLine _
                                         & ",DEST_AD_1                 " & vbNewLine _
                                         & ",DEST_AD_2                 " & vbNewLine _
                                         & ",DEST_AD_3                 " & vbNewLine _
                                         & ",DEST_AD_4                 " & vbNewLine _
                                         & ",DEST_AD_5                 " & vbNewLine _
                                         & ",DEST_TEL                  " & vbNewLine _
                                         & ",DEST_FAX                  " & vbNewLine _
                                         & ",DEST_MAIL                 " & vbNewLine _
                                         & ",DEST_JIS_CD               " & vbNewLine _
                                         & ",SP_NHS_KB                 " & vbNewLine _
                                         & ",COA_YN                    " & vbNewLine _
                                         & ",CUST_ORD_NO               " & vbNewLine _
                                         & ",BUYER_ORD_NO              " & vbNewLine _
                                         & ",UNSO_MOTO_KB              " & vbNewLine _
                                         & ",UNSO_TEHAI_KB             " & vbNewLine _
                                         & ",SYARYO_KB                 " & vbNewLine _
                                         & ",BIN_KB                    " & vbNewLine _
                                         & ",UNSO_CD                   " & vbNewLine _
                                         & ",UNSO_NM                   " & vbNewLine _
                                         & ",UNSO_BR_CD                " & vbNewLine _
                                         & ",UNSO_BR_NM                " & vbNewLine _
                                         & ",UNCHIN_TARIFF_CD          " & vbNewLine _
                                         & ",EXTC_TARIFF_CD            " & vbNewLine _
                                         & ",REMARK                    " & vbNewLine _
                                         & ",UNSO_ATT                  " & vbNewLine _
                                         & ",DENP_YN                   " & vbNewLine _
                                         & ",PC_KB                     " & vbNewLine _
                                         & ",UNCHIN_YN                 " & vbNewLine _
                                         & ",NIYAKU_YN                 " & vbNewLine _
                                         & ",OUT_FLAG                  " & vbNewLine _
                                         & ",AKAKURO_KB                " & vbNewLine _
                                         & ",JISSEKI_FLAG              " & vbNewLine _
                                         & ",JISSEKI_USER              " & vbNewLine _
                                         & ",JISSEKI_DATE              " & vbNewLine _
                                         & ",JISSEKI_TIME              " & vbNewLine _
                                         & ",FREE_N01                  " & vbNewLine _
                                         & ",FREE_N02                  " & vbNewLine _
                                         & ",FREE_N03                  " & vbNewLine _
                                         & ",FREE_N04                  " & vbNewLine _
                                         & ",FREE_N05                  " & vbNewLine _
                                         & ",FREE_N06                  " & vbNewLine _
                                         & ",FREE_N07                  " & vbNewLine _
                                         & ",FREE_N08                  " & vbNewLine _
                                         & ",FREE_N09                  " & vbNewLine _
                                         & ",FREE_N10                  " & vbNewLine _
                                         & ",FREE_C01                  " & vbNewLine _
                                         & ",FREE_C02                  " & vbNewLine _
                                         & ",FREE_C03                  " & vbNewLine _
                                         & ",FREE_C04                  " & vbNewLine _
                                         & ",FREE_C05                  " & vbNewLine _
                                         & ",FREE_C06                  " & vbNewLine _
                                         & ",FREE_C07                  " & vbNewLine _
                                         & ",FREE_C08                  " & vbNewLine _
                                         & ",FREE_C09                  " & vbNewLine _
                                         & ",FREE_C10                  " & vbNewLine _
                                         & ",FREE_C11                  " & vbNewLine _
                                         & ",FREE_C12                  " & vbNewLine _
                                         & ",FREE_C13                  " & vbNewLine _
                                         & ",FREE_C14                  " & vbNewLine _
                                         & ",FREE_C15                  " & vbNewLine _
                                         & ",FREE_C16                  " & vbNewLine _
                                         & ",FREE_C17                  " & vbNewLine _
                                         & ",FREE_C18                  " & vbNewLine _
                                         & ",FREE_C19                  " & vbNewLine _
                                         & ",FREE_C20                  " & vbNewLine _
                                         & ",FREE_C21                  " & vbNewLine _
                                         & ",FREE_C22                  " & vbNewLine _
                                         & ",FREE_C23                  " & vbNewLine _
                                         & ",FREE_C24                  " & vbNewLine _
                                         & ",FREE_C25                  " & vbNewLine _
                                         & ",FREE_C26                  " & vbNewLine _
                                         & ",FREE_C27                  " & vbNewLine _
                                         & ",FREE_C28                  " & vbNewLine _
                                         & ",FREE_C29                  " & vbNewLine _
                                         & ",FREE_C30                  " & vbNewLine _
                                         & ",CRT_USER                  " & vbNewLine _
                                         & ",CRT_DATE                  " & vbNewLine _
                                         & ",CRT_TIME                  " & vbNewLine _
                                         & ",UPD_USER                  " & vbNewLine _
                                         & ",UPD_DATE                  " & vbNewLine _
                                         & ",UPD_TIME                  " & vbNewLine _
                                         & ",SCM_CTL_NO_L              " & vbNewLine _
                                         & ",EDIT_FLAG                 " & vbNewLine _
                                         & ",MATCHING_FLAG             " & vbNewLine _
                                         & ",SYS_ENT_DATE              " & vbNewLine _
                                         & ",SYS_ENT_TIME              " & vbNewLine _
                                         & ",SYS_ENT_PGID              " & vbNewLine _
                                         & ",SYS_ENT_USER              " & vbNewLine _
                                         & ",SYS_UPD_DATE              " & vbNewLine _
                                         & ",SYS_UPD_TIME              " & vbNewLine _
                                         & ",SYS_UPD_PGID              " & vbNewLine _
                                         & ",SYS_UPD_USER              " & vbNewLine _
                                         & ",SYS_DEL_FLG               " & vbNewLine _
                                         & ")VALUES(                   " & vbNewLine _
                                         & " @DEL_KB                   " & vbNewLine _
                                         & ",@NRS_BR_CD                " & vbNewLine _
                                         & ",@EDI_CTL_NO               " & vbNewLine _
                                         & ",@OUTKA_CTL_NO             " & vbNewLine _
                                         & ",@OUTKA_KB                 " & vbNewLine _
                                         & ",@SYUBETU_KB               " & vbNewLine _
                                         & ",@NAIGAI_KB                " & vbNewLine _
                                         & ",@OUTKA_STATE_KB           " & vbNewLine _
                                         & ",@OUTKAHOKOKU_YN           " & vbNewLine _
                                         & ",@PICK_KB                  " & vbNewLine _
                                         & ",@NRS_BR_NM                " & vbNewLine _
                                         & ",@WH_CD                    " & vbNewLine _
                                         & ",@WH_NM                    " & vbNewLine _
                                         & ",@OUTKA_PLAN_DATE          " & vbNewLine _
                                         & ",@OUTKO_DATE               " & vbNewLine _
                                         & ",@ARR_PLAN_DATE            " & vbNewLine _
                                         & ",@ARR_PLAN_TIME            " & vbNewLine _
                                         & ",@HOKOKU_DATE              " & vbNewLine _
                                         & ",@TOUKI_HOKAN_YN           " & vbNewLine _
                                         & ",@CUST_CD_L                " & vbNewLine _
                                         & ",@CUST_CD_M                " & vbNewLine _
                                         & ",@CUST_NM_L                " & vbNewLine _
                                         & ",@CUST_NM_M                " & vbNewLine _
                                         & ",@SHIP_CD_L                " & vbNewLine _
                                         & ",@SHIP_CD_M                " & vbNewLine _
                                         & ",@SHIP_NM_L                " & vbNewLine _
                                         & ",@SHIP_NM_M                " & vbNewLine _
                                         & ",@EDI_DEST_CD              " & vbNewLine _
                                         & ",@DEST_CD                  " & vbNewLine _
                                         & ",@DEST_NM                  " & vbNewLine _
                                         & ",@DEST_ZIP                 " & vbNewLine _
                                         & ",@DEST_AD_1                " & vbNewLine _
                                         & ",@DEST_AD_2                " & vbNewLine _
                                         & ",@DEST_AD_3                " & vbNewLine _
                                         & ",@DEST_AD_4                " & vbNewLine _
                                         & ",@DEST_AD_5                " & vbNewLine _
                                         & ",@DEST_TEL                 " & vbNewLine _
                                         & ",@DEST_FAX                 " & vbNewLine _
                                         & ",@DEST_MAIL                " & vbNewLine _
                                         & ",@DEST_JIS_CD              " & vbNewLine _
                                         & ",@SP_NHS_KB                " & vbNewLine _
                                         & ",@COA_YN                   " & vbNewLine _
                                         & ",@CUST_ORD_NO              " & vbNewLine _
                                         & ",@BUYER_ORD_NO             " & vbNewLine _
                                         & ",@UNSO_MOTO_KB             " & vbNewLine _
                                         & ",@UNSO_TEHAI_KB            " & vbNewLine _
                                         & ",@SYARYO_KB                " & vbNewLine _
                                         & ",@BIN_KB                   " & vbNewLine _
                                         & ",@UNSO_CD                  " & vbNewLine _
                                         & ",@UNSO_NM                  " & vbNewLine _
                                         & ",@UNSO_BR_CD               " & vbNewLine _
                                         & ",@UNSO_BR_NM               " & vbNewLine _
                                         & ",@UNCHIN_TARIFF_CD         " & vbNewLine _
                                         & ",@EXTC_TARIFF_CD           " & vbNewLine _
                                         & ",@REMARK                   " & vbNewLine _
                                         & ",@UNSO_ATT                 " & vbNewLine _
                                         & ",@DENP_YN                  " & vbNewLine _
                                         & ",@PC_KB                    " & vbNewLine _
                                         & ",@UNCHIN_YN                " & vbNewLine _
                                         & ",@NIYAKU_YN                " & vbNewLine _
                                         & ",@OUT_FLAG                 " & vbNewLine _
                                         & ",@AKAKURO_KB               " & vbNewLine _
                                         & ",@JISSEKI_FLAG             " & vbNewLine _
                                         & ",@JISSEKI_USER             " & vbNewLine _
                                         & ",@JISSEKI_DATE             " & vbNewLine _
                                         & ",@JISSEKI_TIME             " & vbNewLine _
                                         & ",@FREE_N01                 " & vbNewLine _
                                         & ",@FREE_N02                 " & vbNewLine _
                                         & ",@FREE_N03                 " & vbNewLine _
                                         & ",@FREE_N04                 " & vbNewLine _
                                         & ",@FREE_N05                 " & vbNewLine _
                                         & ",@FREE_N06                 " & vbNewLine _
                                         & ",@FREE_N07                 " & vbNewLine _
                                         & ",@FREE_N08                 " & vbNewLine _
                                         & ",@FREE_N09                 " & vbNewLine _
                                         & ",@FREE_N10                 " & vbNewLine _
                                         & ",@FREE_C01                 " & vbNewLine _
                                         & ",@FREE_C02                 " & vbNewLine _
                                         & ",@FREE_C03                 " & vbNewLine _
                                         & ",@FREE_C04                 " & vbNewLine _
                                         & ",@FREE_C05                 " & vbNewLine _
                                         & ",@FREE_C06                 " & vbNewLine _
                                         & ",@FREE_C07                 " & vbNewLine _
                                         & ",@FREE_C08                 " & vbNewLine _
                                         & ",@FREE_C09                 " & vbNewLine _
                                         & ",@FREE_C10                 " & vbNewLine _
                                         & ",@FREE_C11                 " & vbNewLine _
                                         & ",@FREE_C12                 " & vbNewLine _
                                         & ",@FREE_C13                 " & vbNewLine _
                                         & ",@FREE_C14                 " & vbNewLine _
                                         & ",@FREE_C15                 " & vbNewLine _
                                         & ",@FREE_C16                 " & vbNewLine _
                                         & ",@FREE_C17                 " & vbNewLine _
                                         & ",@FREE_C18                 " & vbNewLine _
                                         & ",@FREE_C19                 " & vbNewLine _
                                         & ",@FREE_C20                 " & vbNewLine _
                                         & ",@FREE_C21                 " & vbNewLine _
                                         & ",@FREE_C22                 " & vbNewLine _
                                         & ",@FREE_C23                 " & vbNewLine _
                                         & ",@FREE_C24                 " & vbNewLine _
                                         & ",@FREE_C25                 " & vbNewLine _
                                         & ",@FREE_C26                 " & vbNewLine _
                                         & ",@FREE_C27                 " & vbNewLine _
                                         & ",@FREE_C28                 " & vbNewLine _
                                         & ",@FREE_C29                 " & vbNewLine _
                                         & ",@FREE_C30                 " & vbNewLine _
                                         & ",@CRT_USER                 " & vbNewLine _
                                         & ",@CRT_DATE                 " & vbNewLine _
                                         & ",@CRT_TIME                 " & vbNewLine _
                                         & ",@UPD_USER                 " & vbNewLine _
                                         & ",@UPD_DATE                 " & vbNewLine _
                                         & ",@UPD_TIME                 " & vbNewLine _
                                         & ",@SCM_CTL_NO_L             " & vbNewLine _
                                         & ",@EDIT_FLAG                " & vbNewLine _
                                         & ",@MATCHING_FLAG            " & vbNewLine _
                                         & ",@SYS_ENT_DATE             " & vbNewLine _
                                         & ",@SYS_ENT_TIME             " & vbNewLine _
                                         & ",@SYS_ENT_PGID             " & vbNewLine _
                                         & ",@SYS_ENT_USER             " & vbNewLine _
                                         & ",@SYS_UPD_DATE             " & vbNewLine _
                                         & ",@SYS_UPD_TIME             " & vbNewLine _
                                         & ",@SYS_UPD_PGID             " & vbNewLine _
                                         & ",@SYS_UPD_USER             " & vbNewLine _
                                         & ",@SYS_DEL_FLG              " & vbNewLine _
                                         & ")                          " & vbNewLine
#End Region

#Region "H_OUTKAEDI_M(INSERT)"
    ''' <summary>
    ''' INSERT（H_OUTKAEDI_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTKAEDI_M As String = "INSERT INTO          " & vbNewLine _
                                         & "$LM_TRN$..H_OUTKAEDI_M        " & vbNewLine _
                                         & "(                             " & vbNewLine _
                                         & " DEL_KB                       " & vbNewLine _
                                         & ",NRS_BR_CD                    " & vbNewLine _
                                         & ",EDI_CTL_NO                   " & vbNewLine _
                                         & ",EDI_CTL_NO_CHU               " & vbNewLine _
                                         & ",OUTKA_CTL_NO                 " & vbNewLine _
                                         & ",OUTKA_CTL_NO_CHU             " & vbNewLine _
                                         & ",COA_YN                       " & vbNewLine _
                                         & ",CUST_ORD_NO_DTL              " & vbNewLine _
                                         & ",BUYER_ORD_NO_DTL             " & vbNewLine _
                                         & ",CUST_GOODS_CD                " & vbNewLine _
                                         & ",NRS_GOODS_CD                 " & vbNewLine _
                                         & ",GOODS_NM                     " & vbNewLine _
                                         & ",RSV_NO                       " & vbNewLine _
                                         & ",LOT_NO                       " & vbNewLine _
                                         & ",SERIAL_NO                    " & vbNewLine _
                                         & ",ALCTD_KB                     " & vbNewLine _
                                         & ",OUTKA_PKG_NB                 " & vbNewLine _
                                         & ",OUTKA_HASU                   " & vbNewLine _
                                         & ",OUTKA_QT                     " & vbNewLine _
                                         & ",OUTKA_TTL_NB                 " & vbNewLine _
                                         & ",OUTKA_TTL_QT                 " & vbNewLine _
                                         & ",KB_UT                        " & vbNewLine _
                                         & ",QT_UT                        " & vbNewLine _
                                         & ",PKG_NB                       " & vbNewLine _
                                         & ",PKG_UT                       " & vbNewLine _
                                         & ",ONDO_KB                      " & vbNewLine _
                                         & ",UNSO_ONDO_KB                 " & vbNewLine _
                                         & ",IRIME                        " & vbNewLine _
                                         & ",IRIME_UT                     " & vbNewLine _
                                         & ",BETU_WT                      " & vbNewLine _
                                         & ",REMARK                       " & vbNewLine _
                                         & ",OUT_KB                       " & vbNewLine _
                                         & ",AKAKURO_KB                   " & vbNewLine _
                                         & ",JISSEKI_FLAG                 " & vbNewLine _
                                         & ",JISSEKI_USER                 " & vbNewLine _
                                         & ",JISSEKI_DATE                 " & vbNewLine _
                                         & ",JISSEKI_TIME                 " & vbNewLine _
                                         & ",SET_KB                       " & vbNewLine _
                                         & ",FREE_N01                     " & vbNewLine _
                                         & ",FREE_N02                     " & vbNewLine _
                                         & ",FREE_N03                     " & vbNewLine _
                                         & ",FREE_N04                     " & vbNewLine _
                                         & ",FREE_N05                     " & vbNewLine _
                                         & ",FREE_N06                     " & vbNewLine _
                                         & ",FREE_N07                     " & vbNewLine _
                                         & ",FREE_N08                     " & vbNewLine _
                                         & ",FREE_N09                     " & vbNewLine _
                                         & ",FREE_N10                     " & vbNewLine _
                                         & ",FREE_C01                     " & vbNewLine _
                                         & ",FREE_C02                     " & vbNewLine _
                                         & ",FREE_C03                     " & vbNewLine _
                                         & ",FREE_C04                     " & vbNewLine _
                                         & ",FREE_C05                     " & vbNewLine _
                                         & ",FREE_C06                     " & vbNewLine _
                                         & ",FREE_C07                     " & vbNewLine _
                                         & ",FREE_C08                     " & vbNewLine _
                                         & ",FREE_C09                     " & vbNewLine _
                                         & ",FREE_C10                     " & vbNewLine _
                                         & ",FREE_C11                     " & vbNewLine _
                                         & ",FREE_C12                     " & vbNewLine _
                                         & ",FREE_C13                     " & vbNewLine _
                                         & ",FREE_C14                     " & vbNewLine _
                                         & ",FREE_C15                     " & vbNewLine _
                                         & ",FREE_C16                     " & vbNewLine _
                                         & ",FREE_C17                     " & vbNewLine _
                                         & ",FREE_C18                     " & vbNewLine _
                                         & ",FREE_C19                     " & vbNewLine _
                                         & ",FREE_C20                     " & vbNewLine _
                                         & ",FREE_C21                     " & vbNewLine _
                                         & ",FREE_C22                     " & vbNewLine _
                                         & ",FREE_C23                     " & vbNewLine _
                                         & ",FREE_C24                     " & vbNewLine _
                                         & ",FREE_C25                     " & vbNewLine _
                                         & ",FREE_C26                     " & vbNewLine _
                                         & ",FREE_C27                     " & vbNewLine _
                                         & ",FREE_C28                     " & vbNewLine _
                                         & ",FREE_C29                     " & vbNewLine _
                                         & ",FREE_C30                     " & vbNewLine _
                                         & ",CRT_USER                     " & vbNewLine _
                                         & ",CRT_DATE                     " & vbNewLine _
                                         & ",CRT_TIME                     " & vbNewLine _
                                         & ",UPD_USER                     " & vbNewLine _
                                         & ",UPD_DATE                     " & vbNewLine _
                                         & ",UPD_TIME                     " & vbNewLine _
                                         & ",SCM_CTL_NO_L                 " & vbNewLine _
                                         & ",SCM_CTL_NO_M                 " & vbNewLine _
                                         & ",SYS_ENT_DATE                 " & vbNewLine _
                                         & ",SYS_ENT_TIME                 " & vbNewLine _
                                         & ",SYS_ENT_PGID                 " & vbNewLine _
                                         & ",SYS_ENT_USER                 " & vbNewLine _
                                         & ",SYS_UPD_DATE                 " & vbNewLine _
                                         & ",SYS_UPD_TIME                 " & vbNewLine _
                                         & ",SYS_UPD_PGID                 " & vbNewLine _
                                         & ",SYS_UPD_USER                 " & vbNewLine _
                                         & ",SYS_DEL_FLG                  " & vbNewLine _
                                         & ")VALUES(                      " & vbNewLine _
                                         & " @DEL_KB                      " & vbNewLine _
                                         & ",@NRS_BR_CD                   " & vbNewLine _
                                         & ",@EDI_CTL_NO                  " & vbNewLine _
                                         & ",@EDI_CTL_NO_CHU              " & vbNewLine _
                                         & ",@OUTKA_CTL_NO                " & vbNewLine _
                                         & ",@OUTKA_CTL_NO_CHU            " & vbNewLine _
                                         & ",@COA_YN                      " & vbNewLine _
                                         & ",@CUST_ORD_NO_DTL             " & vbNewLine _
                                         & ",@BUYER_ORD_NO_DTL            " & vbNewLine _
                                         & ",@CUST_GOODS_CD               " & vbNewLine _
                                         & ",@NRS_GOODS_CD                " & vbNewLine _
                                         & ",@GOODS_NM                    " & vbNewLine _
                                         & ",@RSV_NO                      " & vbNewLine _
                                         & ",@LOT_NO                      " & vbNewLine _
                                         & ",@SERIAL_NO                   " & vbNewLine _
                                         & ",@ALCTD_KB                    " & vbNewLine _
                                         & ",@OUTKA_PKG_NB                " & vbNewLine _
                                         & ",@OUTKA_HASU                  " & vbNewLine _
                                         & ",@OUTKA_QT                    " & vbNewLine _
                                         & ",@OUTKA_TTL_NB                " & vbNewLine _
                                         & ",@OUTKA_TTL_QT                " & vbNewLine _
                                         & ",@KB_UT                       " & vbNewLine _
                                         & ",@QT_UT                       " & vbNewLine _
                                         & ",@PKG_NB                      " & vbNewLine _
                                         & ",@PKG_UT                      " & vbNewLine _
                                         & ",@ONDO_KB                     " & vbNewLine _
                                         & ",@UNSO_ONDO_KB                " & vbNewLine _
                                         & ",@IRIME                       " & vbNewLine _
                                         & ",@IRIME_UT                    " & vbNewLine _
                                         & ",@BETU_WT                     " & vbNewLine _
                                         & ",@REMARK                      " & vbNewLine _
                                         & ",@OUT_KB                      " & vbNewLine _
                                         & ",@AKAKURO_KB                  " & vbNewLine _
                                         & ",@JISSEKI_FLAG                " & vbNewLine _
                                         & ",@JISSEKI_USER                " & vbNewLine _
                                         & ",@JISSEKI_DATE                " & vbNewLine _
                                         & ",@JISSEKI_TIME                " & vbNewLine _
                                         & ",@SET_KB                      " & vbNewLine _
                                         & ",@FREE_N01                    " & vbNewLine _
                                         & ",@FREE_N02                    " & vbNewLine _
                                         & ",@FREE_N03                    " & vbNewLine _
                                         & ",@FREE_N04                    " & vbNewLine _
                                         & ",@FREE_N05                    " & vbNewLine _
                                         & ",@FREE_N06                    " & vbNewLine _
                                         & ",@FREE_N07                    " & vbNewLine _
                                         & ",@FREE_N08                    " & vbNewLine _
                                         & ",@FREE_N09                    " & vbNewLine _
                                         & ",@FREE_N10                    " & vbNewLine _
                                         & ",@FREE_C01                    " & vbNewLine _
                                         & ",@FREE_C02                    " & vbNewLine _
                                         & ",@FREE_C03                    " & vbNewLine _
                                         & ",@FREE_C04                    " & vbNewLine _
                                         & ",@FREE_C05                    " & vbNewLine _
                                         & ",@FREE_C06                    " & vbNewLine _
                                         & ",@FREE_C07                    " & vbNewLine _
                                         & ",@FREE_C08                    " & vbNewLine _
                                         & ",@FREE_C09                    " & vbNewLine _
                                         & ",@FREE_C10                    " & vbNewLine _
                                         & ",@FREE_C11                    " & vbNewLine _
                                         & ",@FREE_C12                    " & vbNewLine _
                                         & ",@FREE_C13                    " & vbNewLine _
                                         & ",@FREE_C14                    " & vbNewLine _
                                         & ",@FREE_C15                    " & vbNewLine _
                                         & ",@FREE_C16                    " & vbNewLine _
                                         & ",@FREE_C17                    " & vbNewLine _
                                         & ",@FREE_C18                    " & vbNewLine _
                                         & ",@FREE_C19                    " & vbNewLine _
                                         & ",@FREE_C20                    " & vbNewLine _
                                         & ",@FREE_C21                    " & vbNewLine _
                                         & ",@FREE_C22                    " & vbNewLine _
                                         & ",@FREE_C23                    " & vbNewLine _
                                         & ",@FREE_C24                    " & vbNewLine _
                                         & ",@FREE_C25                    " & vbNewLine _
                                         & ",@FREE_C26                    " & vbNewLine _
                                         & ",@FREE_C27                    " & vbNewLine _
                                         & ",@FREE_C28                    " & vbNewLine _
                                         & ",@FREE_C29                    " & vbNewLine _
                                         & ",@FREE_C30                    " & vbNewLine _
                                         & ",@CRT_USER                    " & vbNewLine _
                                         & ",@CRT_DATE                    " & vbNewLine _
                                         & ",@CRT_TIME                    " & vbNewLine _
                                         & ",@UPD_USER                    " & vbNewLine _
                                         & ",@UPD_DATE                    " & vbNewLine _
                                         & ",@UPD_TIME                    " & vbNewLine _
                                         & ",@SCM_CTL_NO_L                " & vbNewLine _
                                         & ",@SCM_CTL_NO_M                " & vbNewLine _
                                         & ",@SYS_ENT_DATE                " & vbNewLine _
                                         & ",@SYS_ENT_TIME                " & vbNewLine _
                                         & ",@SYS_ENT_PGID                " & vbNewLine _
                                         & ",@SYS_ENT_USER                " & vbNewLine _
                                         & ",@SYS_UPD_DATE                " & vbNewLine _
                                         & ",@SYS_UPD_TIME                " & vbNewLine _
                                         & ",@SYS_UPD_PGID                " & vbNewLine _
                                         & ",@SYS_UPD_USER                " & vbNewLine _
                                         & ",@SYS_DEL_FLG                 " & vbNewLine _
                                         & ")                             " & vbNewLine
#End Region

#Region "H_OUTKAEDI_L(同一データ整合性差異ありの場合の更新処理)"
    ''' <summary>
    ''' EDI出荷(大)セミEDI(同一データ整合性差異ありの場合の更新処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_EDIT_OUTKAEDI_L As String _
                        = " UPDATE $LM_TRN$..H_OUTKAEDI_L  SET          " & vbNewLine _
                        & " DEL_KB              = '3'                   " & vbNewLine _
                        & ",UPD_USER            = @UPD_USER             " & vbNewLine _
                        & ",UPD_DATE            = @UPD_DATE             " & vbNewLine _
                        & ",UPD_TIME            = @UPD_TIME             " & vbNewLine _
                        & ",SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                        & ",SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                        & ",SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                        & ",SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                        & ",SYS_DEL_FLG         = '0'                   " & vbNewLine _
                        & " FROM                                        " & vbNewLine _
                        & " $LM_TRN$..H_OUTKAEDI_L  HOEL                " & vbNewLine _
                        & " LEFT JOIN                                   " & vbNewLine _
                        & " (SELECT                                     " & vbNewLine _
                        & "   NRS_BR_CD                                 " & vbNewLine _
                        & "  ,EDI_CTL_NO                                " & vbNewLine _
                        & "  ,DEL_KB                                    " & vbNewLine _
                        & " FROM $LM_TRN$..H_OUTKAEDI_M                 " & vbNewLine _
                        & " WHERE NRS_BR_CD     = @NRS_BR_CD            " & vbNewLine _
                        & " AND   EDI_CTL_NO    = @EDI_CTL_NO           " & vbNewLine _
                        & " GROUP BY                                    " & vbNewLine _
                        & "   NRS_BR_CD                                 " & vbNewLine _
                        & "  ,EDI_CTL_NO                                " & vbNewLine _
                        & "  ,DEL_KB                                    " & vbNewLine _
                        & " )  HOEM                                     " & vbNewLine _
                        & " ON                                          " & vbNewLine _
                        & "     HOEL.NRS_BR_CD    = HOEM.NRS_BR_CD      " & vbNewLine _
                        & " AND HOEL.EDI_CTL_NO   = HOEM.EDI_CTL_NO     " & vbNewLine _
                        & " WHERE HOEL.NRS_BR_CD  = @NRS_BR_CD          " & vbNewLine _
                        & " AND   HOEL.EDI_CTL_NO = @EDI_CTL_NO         " & vbNewLine _
                        & " AND   HOEM.DEL_KB     = '3'                 " & vbNewLine


#End Region

#Region "H_OUTKAEDI_M(同一データ整合性差異ありの場合の更新処理)"
    ''' <summary>
    ''' EDI出荷(中)セミEDI(同一データ整合性差異ありの場合の更新処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_EDIT_OUTKAEDI_M As String _
                        = " UPDATE $LM_TRN$..H_OUTKAEDI_M  SET          " & vbNewLine _
                        & " DEL_KB              = '3'                   " & vbNewLine _
                        & ",UPD_USER            = @UPD_USER             " & vbNewLine _
                        & ",UPD_DATE            = @UPD_DATE             " & vbNewLine _
                        & ",UPD_TIME            = @UPD_TIME             " & vbNewLine _
                        & ",SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                        & ",SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                        & ",SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                        & ",SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                        & ",SYS_DEL_FLG         = '0'                   " & vbNewLine _
                        & " WHERE NRS_BR_CD     = @NRS_BR_CD            " & vbNewLine _
                        & " AND  EDI_CTL_NO     = @EDI_CTL_NO           " & vbNewLine

#End Region

#End Region

#End Region

#End Region

#Region "Const(出荷登録系)"
#Region "出荷データ作成系処理"
#Region "出荷登録用EDIデータ 抽出用SQL"
#Region "H_OUTKAEDI_L データ抽出用"
    ''' <summary>
    ''' H_OUTKAEDI_Lデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const L_DEF_SQL_SELECT_DATA As String = " SELECT                                                                             " & vbNewLine _
                                             & " '0'                                                 AS DEL_KB                           " & vbNewLine _
                                             & ",H_OUTKAEDI_L.NRS_BR_CD                              AS NRS_BR_CD                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.EDI_CTL_NO                             AS EDI_CTL_NO                       " & vbNewLine _
                                             & ",H_OUTKAEDI_L.OUTKA_CTL_NO                           AS OUTKA_CTL_NO                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.OUTKA_KB                               AS OUTKA_KB                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYUBETU_KB                             AS SYUBETU_KB                       " & vbNewLine _
                                             & ",H_OUTKAEDI_L.NAIGAI_KB                              AS NAIGAI_KB                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.OUTKA_STATE_KB                         AS OUTKA_STATE_KB                   " & vbNewLine _
                                             & ",H_OUTKAEDI_L.OUTKAHOKOKU_YN                         AS OUTKAHOKOKU_YN                   " & vbNewLine _
                                             & ",H_OUTKAEDI_L.PICK_KB                                AS PICK_KB                          " & vbNewLine _
                                             & ",H_OUTKAEDI_L.NRS_BR_NM                              AS NRS_BR_NM                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.WH_CD                                  AS WH_CD                            " & vbNewLine _
                                             & ",H_OUTKAEDI_L.WH_NM                                  AS WH_NM                            " & vbNewLine _
                                             & ",H_OUTKAEDI_L.OUTKA_PLAN_DATE                        AS OUTKA_PLAN_DATE                  " & vbNewLine _
                                             & ",H_OUTKAEDI_L.OUTKO_DATE                             AS OUTKO_DATE                       " & vbNewLine _
                                             & ",H_OUTKAEDI_L.ARR_PLAN_DATE                          AS ARR_PLAN_DATE                    " & vbNewLine _
                                             & ",H_OUTKAEDI_L.ARR_PLAN_TIME                          AS ARR_PLAN_TIME                    " & vbNewLine _
                                             & ",H_OUTKAEDI_L.HOKOKU_DATE                            AS HOKOKU_DATE                      " & vbNewLine _
                                             & ",H_OUTKAEDI_L.TOUKI_HOKAN_YN                         AS TOUKI_HOKAN_YN                   " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CUST_CD_L                              AS CUST_CD_L                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CUST_CD_M                              AS CUST_CD_M                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CUST_NM_L                              AS CUST_NM_L                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CUST_NM_M                              AS CUST_NM_M                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SHIP_CD_L                              AS SHIP_CD_L                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SHIP_CD_M                              AS SHIP_CD_M                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SHIP_NM_L                              AS SHIP_NM_L                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SHIP_NM_M                              AS SHIP_NM_M                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.EDI_DEST_CD                            AS EDI_DEST_CD                      " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_CD                                AS DEST_CD                          " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_NM                                AS DEST_NM                          " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_ZIP                               AS DEST_ZIP                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_AD_1                              AS DEST_AD_1                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_AD_2                              AS DEST_AD_2                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_AD_3                              AS DEST_AD_3                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_AD_4                              AS DEST_AD_4                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_AD_5                              AS DEST_AD_5                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_TEL                               AS DEST_TEL                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_FAX                               AS DEST_FAX                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_MAIL                              AS DEST_MAIL                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_JIS_CD                            AS DEST_JIS_CD                      " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SP_NHS_KB                              AS SP_NHS_KB                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.COA_YN                                 AS COA_YN                           " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CUST_ORD_NO                            AS CUST_ORD_NO                      " & vbNewLine _
                                             & ",H_OUTKAEDI_L.BUYER_ORD_NO                           AS BUYER_ORD_NO                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNSO_MOTO_KB                           AS UNSO_MOTO_KB                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNSO_TEHAI_KB                          AS UNSO_TEHAI_KB                    " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYARYO_KB                              AS SYARYO_KB                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.BIN_KB                                 AS BIN_KB                           " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNSO_CD                                AS UNSO_CD                          " & vbNewLine _
                                             & ",M_UNSOCO.UNSOCO_NM                                  AS UNSO_NM                          " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNSO_BR_CD                             AS UNSO_BR_CD                       " & vbNewLine _
                                             & ",M_UNSOCO.UNSOCO_BR_NM                               AS UNSO_BR_NM                       " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNCHIN_TARIFF_CD                       AS UNCHIN_TARIFF_CD                 " & vbNewLine _
                                             & ",H_OUTKAEDI_L.EXTC_TARIFF_CD                         AS EXTC_TARIFF_CD                   " & vbNewLine _
                                             & ",H_OUTKAEDI_L.REMARK                                 AS REMARK                           " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNSO_ATT                               AS UNSO_ATT                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DENP_YN                                AS DENP_YN                          " & vbNewLine _
                                             & ",H_OUTKAEDI_L.PC_KB                                  AS PC_KB                            " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNCHIN_YN                              AS UNCHIN_YN                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.NIYAKU_YN                              AS NIYAKU_YN                        " & vbNewLine _
                                             & ",'1'                                                 AS OUT_FLAG                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.AKAKURO_KB                             AS AKAKURO_KB                       " & vbNewLine _
                                             & ",H_OUTKAEDI_L.JISSEKI_FLAG                           AS JISSEKI_FLAG                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.JISSEKI_USER                           AS JISSEKI_USER                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.JISSEKI_DATE                           AS JISSEKI_DATE                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.JISSEKI_TIME                           AS JISSEKI_TIME                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N01                               AS FREE_N01                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N02                               AS FREE_N02                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N03                               AS FREE_N03                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N04                               AS FREE_N04                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N05                               AS FREE_N05                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N06                               AS FREE_N06                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N07                               AS FREE_N07                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N08                               AS FREE_N08                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N09                               AS FREE_N09                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N10                               AS FREE_N10                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C01                               AS FREE_C01                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C02                               AS FREE_C02                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C03                               AS FREE_C03                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C04                               AS FREE_C04                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C05                               AS FREE_C05                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C06                               AS FREE_C06                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C07                               AS FREE_C07                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C08                               AS FREE_C08                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C09                               AS FREE_C09                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C10                               AS FREE_C10                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C11                               AS FREE_C11                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C12                               AS FREE_C12                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C13                               AS FREE_C13                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C14                               AS FREE_C14                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C15                               AS FREE_C15                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C16                               AS FREE_C16                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C17                               AS FREE_C17                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C18                               AS FREE_C18                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C19                               AS FREE_C19                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C20                               AS FREE_C20                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C21                               AS FREE_C21                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C22                               AS FREE_C22                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C23                               AS FREE_C23                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C24                               AS FREE_C24                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C25                               AS FREE_C25                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C26                               AS FREE_C26                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C27                               AS FREE_C27                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C28                               AS FREE_C28                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C29                               AS FREE_C29                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C30                               AS FREE_C30                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CRT_USER                               AS CRT_USER                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CRT_DATE                               AS CRT_DATE                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CRT_TIME                               AS CRT_TIME                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UPD_USER                               AS UPD_USER                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UPD_DATE                               AS UPD_DATE                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UPD_TIME                               AS UPD_TIME                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SCM_CTL_NO_L                           AS SCM_CTL_NO_L                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.EDIT_FLAG                              AS EDIT_FLAG                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.MATCHING_FLAG                          AS MATCHING_FLAG                    " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_ENT_DATE                           AS SYS_ENT_DATE                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_ENT_TIME                           AS SYS_ENT_TIME                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_ENT_PGID                           AS SYS_ENT_PGID                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_ENT_USER                           AS SYS_ENT_USER                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_UPD_DATE                           AS SYS_UPD_DATE                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_UPD_TIME                           AS SYS_UPD_TIME                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_UPD_PGID                           AS SYS_UPD_PGID                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_UPD_USER                           AS SYS_UPD_USER                     " & vbNewLine _
                                             & ",'0'                                                 AS SYS_DEL_FLG                      " & vbNewLine _
                                             & " FROM                                                                                    " & vbNewLine _
                                             & " $LM_TRN$..H_OUTKAEDI_L         H_OUTKAEDI_L                                             " & vbNewLine _
                                             & " LEFT JOIN                                                                               " & vbNewLine _
                                             & " $LM_MST$..M_UNSOCO             M_UNSOCO                                                 " & vbNewLine _
                                             & " ON                                                                                      " & vbNewLine _
                                             & " H_OUTKAEDI_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                                             " & vbNewLine _
                                             & " AND                                                                                     " & vbNewLine _
                                             & " H_OUTKAEDI_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                                               " & vbNewLine _
                                             & " AND                                                                                     " & vbNewLine _
                                             & " H_OUTKAEDI_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                                         " & vbNewLine _
                                             & " WHERE                                                                                   " & vbNewLine _
                                             & " H_OUTKAEDI_L.SYS_DEL_FLG   = '0'                                                        " & vbNewLine _
                                             & " AND                                                                                     " & vbNewLine _
                                             & " H_OUTKAEDI_L.NRS_BR_CD     = @NRS_BR_CD                                                 " & vbNewLine _
                                             & " AND                                                                                     " & vbNewLine _
                                             & " H_OUTKAEDI_L.EDI_CTL_NO    = @EDI_CTL_NO                                                " & vbNewLine _
                                             & " AND                                                                                     " & vbNewLine _
                                             & " H_OUTKAEDI_L.SYS_UPD_DATE  = @SYS_UPD_DATE                                              " & vbNewLine _
                                             & " AND                                                                                     " & vbNewLine _
                                             & " H_OUTKAEDI_L.SYS_UPD_TIME  = @SYS_UPD_TIME                                              " & vbNewLine

#End Region

#Region "H_OUTKAEDI_Mデータ抽出用"
    ''' <summary>
    ''' H_OUTKAEDI_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const M_DEF_SQL_SELECT_DATA As String = " SELECT                                                             " & vbNewLine _
                                        & " '0'                                             AS  DEL_KB                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.NRS_BR_CD                          AS  NRS_BR_CD                " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                         AS  EDI_CTL_NO               " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO_CHU                     AS  EDI_CTL_NO_CHU           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_CTL_NO                       AS  OUTKA_CTL_NO             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_CTL_NO_CHU                   AS  OUTKA_CTL_NO_CHU         " & vbNewLine _
                                        & ",H_OUTKAEDI_M.COA_YN                             AS  COA_YN                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CUST_ORD_NO_DTL                    AS  CUST_ORD_NO_DTL          " & vbNewLine _
                                        & ",H_OUTKAEDI_M.BUYER_ORD_NO_DTL                   AS  BUYER_ORD_NO_DTL         " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CUST_GOODS_CD                      AS  CUST_GOODS_CD            " & vbNewLine _
                                        & ",H_OUTKAEDI_M.NRS_GOODS_CD                       AS  NRS_GOODS_CD             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.GOODS_NM                           AS  GOODS_NM                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.RSV_NO                             AS  RSV_NO                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.LOT_NO                             AS  LOT_NO                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SERIAL_NO                          AS  SERIAL_NO                " & vbNewLine _
                                        & ",H_OUTKAEDI_M.ALCTD_KB                           AS  ALCTD_KB                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_PKG_NB                       AS  OUTKA_PKG_NB             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_HASU                         AS  OUTKA_HASU               " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_QT                           AS  OUTKA_QT                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_TTL_NB                       AS  OUTKA_TTL_NB             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_TTL_QT                       AS  OUTKA_TTL_QT             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.KB_UT                              AS  KB_UT                    " & vbNewLine _
                                        & ",H_OUTKAEDI_M.QT_UT                              AS  QT_UT                    " & vbNewLine _
                                        & ",H_OUTKAEDI_M.PKG_NB                             AS  PKG_NB                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.PKG_UT                             AS  PKG_UT                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.ONDO_KB                            AS  ONDO_KB                  " & vbNewLine _
                                        & ",H_OUTKAEDI_M.UNSO_ONDO_KB                       AS  UNSO_ONDO_KB             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.IRIME                              AS  IRIME                    " & vbNewLine _
                                        & ",H_OUTKAEDI_M.IRIME_UT                           AS  IRIME_UT                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.BETU_WT                            AS  BETU_WT                  " & vbNewLine _
                                        & ",H_OUTKAEDI_M.REMARK                             AS  REMARK                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUT_KB                             AS  OUT_KB                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.AKAKURO_KB                         AS  AKAKURO_KB               " & vbNewLine _
                                        & ",H_OUTKAEDI_M.JISSEKI_FLAG                       AS  JISSEKI_FLAG             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.JISSEKI_USER                       AS  JISSEKI_USER             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.JISSEKI_DATE                       AS  JISSEKI_DATE             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.JISSEKI_TIME                       AS  JISSEKI_TIME             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SET_KB                             AS  SET_KB                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N01                           AS  FREE_N01                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N02                           AS  FREE_N02                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N03                           AS  FREE_N03                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N04                           AS  FREE_N04                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N05                           AS  FREE_N05                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N06                           AS  FREE_N06                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N07                           AS  FREE_N07                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N08                           AS  FREE_N08                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N09                           AS  FREE_N09                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N10                           AS  FREE_N10                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C01                           AS  FREE_C01                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C02                           AS  FREE_C02                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C03                           AS  FREE_C03                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C04                           AS  FREE_C04                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C05                           AS  FREE_C05                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C06                           AS  FREE_C06                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C07                           AS  FREE_C07                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C08                           AS  FREE_C08                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C09                           AS  FREE_C09                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C10                           AS  FREE_C10                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C11                           AS  FREE_C11                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C12                           AS  FREE_C12                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C13                           AS  FREE_C13                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C14                           AS  FREE_C14                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C15                           AS  FREE_C15                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C16                           AS  FREE_C16                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C17                           AS  FREE_C17                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C18                           AS  FREE_C18                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C19                           AS  FREE_C19                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C20                           AS  FREE_C20                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C21                           AS  FREE_C21                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C22                           AS  FREE_C22                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C23                           AS  FREE_C23                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C24                           AS  FREE_C24                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C25                           AS  FREE_C25                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C26                           AS  FREE_C26                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C27                           AS  FREE_C27                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C28                           AS  FREE_C28                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C29                           AS  FREE_C29                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C30                           AS  FREE_C30                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CRT_USER                           AS  CRT_USER                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CRT_DATE                           AS  CRT_DATE                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CRT_TIME                           AS  CRT_TIME                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.UPD_USER                           AS  UPD_USER                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.UPD_DATE                           AS  UPD_DATE                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.UPD_TIME                           AS  UPD_TIME                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SCM_CTL_NO_L                       AS  SCM_CTL_NO_L             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SCM_CTL_NO_M                       AS  SCM_CTL_NO_M             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_ENT_DATE                       AS  SYS_ENT_DATE             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_ENT_TIME                       AS  SYS_ENT_TIME             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_ENT_PGID                       AS  SYS_ENT_PGID             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_ENT_USER                       AS  SYS_ENT_USER             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_UPD_DATE                       AS  SYS_UPD_DATE             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_UPD_TIME                       AS  SYS_UPD_TIME             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_UPD_PGID                       AS  SYS_UPD_PGID             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_UPD_USER                       AS  SYS_UPD_USER             " & vbNewLine _
                                        & ",'0'                                             AS  SYS_DEL_FLG              " & vbNewLine _
                                        & ",M_DESTGOODS.SAGYO_KB_1                          AS  SAGYO_KB_1               " & vbNewLine _
                                        & ",M_DESTGOODS.SAGYO_KB_2                          AS  SAGYO_KB_2               " & vbNewLine _
                                        & " FROM                                                                         " & vbNewLine _
                                        & " $LM_TRN$..H_OUTKAEDI_M                                                       " & vbNewLine _
                                        & " INNER JOIN                                                                   " & vbNewLine _
                                        & " $LM_TRN$..H_OUTKAEDI_L    H_OUTKAEDI_L                                       " & vbNewLine _
                                        & " ON                                                                           " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD = H_OUTKAEDI_L.NRS_BR_CD                              " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_M.EDI_CTL_NO = H_OUTKAEDI_L.EDI_CTL_NO                            " & vbNewLine _
                                        & " LEFT JOIN                                                                    " & vbNewLine _
                                        & " $LM_MST$..M_DESTGOODS    M_DESTGOODS                                         " & vbNewLine _
                                        & " ON                                                                           " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD = M_DESTGOODS.NRS_BR_CD                               " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_GOODS_CD = M_DESTGOODS.GOODS_CD_NRS                         " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.CUST_CD_L = M_DESTGOODS.CUST_CD_L                               " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.CUST_CD_M = M_DESTGOODS.CUST_CD_M                               " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.DEST_CD = M_DESTGOODS.CD                                        " & vbNewLine _
                                        & " WHERE                                                                        " & vbNewLine _
                                        & " H_OUTKAEDI_L.SYS_DEL_FLG  = '0'                                              " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_M.SYS_DEL_FLG  = '0'                                              " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_M.OUT_KB   = '0'                                                  " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.NRS_BR_CD         = @NRS_BR_CD                                  " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.EDI_CTL_NO         = @EDI_CTL_NO                                " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.SYS_UPD_DATE  = @SYS_UPD_DATE                                   " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.SYS_UPD_TIME  = @SYS_UPD_TIME                                   " & vbNewLine
#End Region

#Region "特殊追加条件(WHERE追加条件)"
    ''' <summary>
    ''' 追加WHERE条件文
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine
#End Region

#End Region

#Region "出荷登録処理 更新用SQL"
#Region "H_OUTKAEDI_L(通常出荷登録)"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKASAVEEDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET       " & vbNewLine _
                                              & " DEL_KB            = @DEL_KB             " & vbNewLine _
                                              & ",OUTKA_CTL_NO      = @OUTKA_CTL_NO       " & vbNewLine _
                                              & ",OUTKA_KB          = @OUTKA_KB           " & vbNewLine _
                                              & ",SYUBETU_KB        = @SYUBETU_KB         " & vbNewLine _
                                              & ",NAIGAI_KB         = @NAIGAI_KB          " & vbNewLine _
                                              & ",OUTKA_STATE_KB    = @OUTKA_STATE_KB     " & vbNewLine _
                                              & ",OUTKAHOKOKU_YN    = @OUTKAHOKOKU_YN     " & vbNewLine _
                                              & ",PICK_KB           = @PICK_KB            " & vbNewLine _
                                              & ",NRS_BR_NM         = @NRS_BR_NM          " & vbNewLine _
                                              & ",WH_CD             = @WH_CD              " & vbNewLine _
                                              & ",WH_NM             = @WH_NM              " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE   = @OUTKA_PLAN_DATE    " & vbNewLine _
                                              & ",OUTKO_DATE        = @OUTKO_DATE         " & vbNewLine _
                                              & ",ARR_PLAN_DATE     = @ARR_PLAN_DATE      " & vbNewLine _
                                              & ",ARR_PLAN_TIME     = @ARR_PLAN_TIME      " & vbNewLine _
                                              & ",HOKOKU_DATE       = @HOKOKU_DATE        " & vbNewLine _
                                              & ",TOUKI_HOKAN_YN    = @TOUKI_HOKAN_YN     " & vbNewLine _
                                              & ",CUST_CD_L         = @CUST_CD_L          " & vbNewLine _
                                              & ",CUST_CD_M         = @CUST_CD_M          " & vbNewLine _
                                              & ",CUST_NM_L         = @CUST_NM_L          " & vbNewLine _
                                              & ",CUST_NM_M         = @CUST_NM_M          " & vbNewLine _
                                              & ",SHIP_CD_L         = @SHIP_CD_L          " & vbNewLine _
                                              & ",SHIP_CD_M         = @SHIP_CD_M          " & vbNewLine _
                                              & ",SHIP_NM_L         = @SHIP_NM_L          " & vbNewLine _
                                              & ",SHIP_NM_M         = @SHIP_NM_M          " & vbNewLine _
                                              & ",EDI_DEST_CD       = @EDI_DEST_CD        " & vbNewLine _
                                              & ",DEST_CD           = @DEST_CD            " & vbNewLine _
                                              & ",DEST_NM           = @DEST_NM            " & vbNewLine _
                                              & ",DEST_ZIP          = @DEST_ZIP           " & vbNewLine _
                                              & ",DEST_AD_1         = @DEST_AD_1          " & vbNewLine _
                                              & ",DEST_AD_2         = @DEST_AD_2          " & vbNewLine _
                                              & ",DEST_AD_3         = @DEST_AD_3          " & vbNewLine _
                                              & ",DEST_AD_4         = @DEST_AD_4          " & vbNewLine _
                                              & ",DEST_AD_5         = @DEST_AD_5          " & vbNewLine _
                                              & ",DEST_TEL          = @DEST_TEL           " & vbNewLine _
                                              & ",DEST_FAX          = @DEST_FAX           " & vbNewLine _
                                              & ",DEST_MAIL         = @DEST_MAIL          " & vbNewLine _
                                              & ",DEST_JIS_CD       = @DEST_JIS_CD        " & vbNewLine _
                                              & ",SP_NHS_KB         = @SP_NHS_KB          " & vbNewLine _
                                              & ",COA_YN            = @COA_YN             " & vbNewLine _
                                              & ",CUST_ORD_NO       = @CUST_ORD_NO        " & vbNewLine _
                                              & ",BUYER_ORD_NO      = @BUYER_ORD_NO       " & vbNewLine _
                                              & ",UNSO_MOTO_KB      = @UNSO_MOTO_KB       " & vbNewLine _
                                              & ",UNSO_TEHAI_KB     = @UNSO_TEHAI_KB      " & vbNewLine _
                                              & ",SYARYO_KB         = @SYARYO_KB          " & vbNewLine _
                                              & ",BIN_KB            = @BIN_KB             " & vbNewLine _
                                              & ",UNSO_CD           = @UNSO_CD            " & vbNewLine _
                                              & ",UNSO_NM           = @UNSO_NM            " & vbNewLine _
                                              & ",UNSO_BR_CD        = @UNSO_BR_CD         " & vbNewLine _
                                              & ",UNSO_BR_NM        = @UNSO_BR_NM         " & vbNewLine _
                                              & ",UNCHIN_TARIFF_CD  = @UNCHIN_TARIFF_CD   " & vbNewLine _
                                              & ",EXTC_TARIFF_CD    = @EXTC_TARIFF_CD     " & vbNewLine _
                                              & ",REMARK            = @REMARK             " & vbNewLine _
                                              & ",UNSO_ATT          = @UNSO_ATT           " & vbNewLine _
                                              & ",DENP_YN           = @DENP_YN            " & vbNewLine _
                                              & ",PC_KB             = @PC_KB           	  " & vbNewLine _
                                              & ",UNCHIN_YN         = @UNCHIN_YN          " & vbNewLine _
                                              & ",NIYAKU_YN         = @NIYAKU_YN          " & vbNewLine _
                                              & ",OUT_FLAG          = @OUT_FLAG           " & vbNewLine _
                                              & ",AKAKURO_KB        = @AKAKURO_KB         " & vbNewLine _
                                              & ",JISSEKI_FLAG      = @JISSEKI_FLAG       " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER       " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE       " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME       " & vbNewLine _
                                              & ",FREE_N01          = @FREE_N01           " & vbNewLine _
                                              & ",FREE_N02          = @FREE_N02           " & vbNewLine _
                                              & ",FREE_N03          = @FREE_N03           " & vbNewLine _
                                              & ",FREE_N04          = @FREE_N04           " & vbNewLine _
                                              & ",FREE_N05          = @FREE_N05           " & vbNewLine _
                                              & ",FREE_N06          = @FREE_N06           " & vbNewLine _
                                              & ",FREE_N07          = @FREE_N07           " & vbNewLine _
                                              & ",FREE_N08          = @FREE_N08           " & vbNewLine _
                                              & ",FREE_N09          = @FREE_N09           " & vbNewLine _
                                              & ",FREE_N10          = @FREE_N10           " & vbNewLine _
                                              & ",FREE_C01          = @FREE_C01           " & vbNewLine _
                                              & ",FREE_C02          = @FREE_C02           " & vbNewLine _
                                              & ",FREE_C03          = @FREE_C03           " & vbNewLine _
                                              & ",FREE_C04          = @FREE_C04           " & vbNewLine _
                                              & ",FREE_C05          = @FREE_C05           " & vbNewLine _
                                              & ",FREE_C06          = @FREE_C06           " & vbNewLine _
                                              & ",FREE_C07          = @FREE_C07           " & vbNewLine _
                                              & ",FREE_C08          = @FREE_C08           " & vbNewLine _
                                              & ",FREE_C09          = @FREE_C09           " & vbNewLine _
                                              & ",FREE_C10          = @FREE_C10           " & vbNewLine _
                                              & ",FREE_C11          = @FREE_C11           " & vbNewLine _
                                              & ",FREE_C12          = @FREE_C12           " & vbNewLine _
                                              & ",FREE_C13          = @FREE_C13           " & vbNewLine _
                                              & ",FREE_C14          = @FREE_C14           " & vbNewLine _
                                              & ",FREE_C15          = @FREE_C15           " & vbNewLine _
                                              & ",FREE_C16          = @FREE_C16           " & vbNewLine _
                                              & ",FREE_C17          = @FREE_C17           " & vbNewLine _
                                              & ",FREE_C18          = @FREE_C18           " & vbNewLine _
                                              & ",FREE_C19          = @FREE_C19           " & vbNewLine _
                                              & ",FREE_C20          = @FREE_C20           " & vbNewLine _
                                              & ",FREE_C21          = @FREE_C21           " & vbNewLine _
                                              & ",FREE_C22          = @FREE_C22           " & vbNewLine _
                                              & ",FREE_C23          = @FREE_C23           " & vbNewLine _
                                              & ",FREE_C24          = @FREE_C24           " & vbNewLine _
                                              & ",FREE_C25          = @FREE_C25           " & vbNewLine _
                                              & ",FREE_C26          = @FREE_C26           " & vbNewLine _
                                              & ",FREE_C27          = @FREE_C27           " & vbNewLine _
                                              & ",FREE_C28          = @FREE_C28           " & vbNewLine _
                                              & ",FREE_C29          = @FREE_C29           " & vbNewLine _
                                              & ",FREE_C30          = @FREE_C30           " & vbNewLine _
                                              & ",CRT_USER          = @CRT_USER           " & vbNewLine _
                                              & ",CRT_DATE          = @CRT_DATE           " & vbNewLine _
                                              & ",CRT_TIME          = @CRT_TIME           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER           " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE           " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME           " & vbNewLine _
                                              & ",SCM_CTL_NO_L      = @SCM_CTL_NO_L       " & vbNewLine _
                                              & ",EDIT_FLAG         = @EDIT_FLAG          " & vbNewLine _
                                              & ",MATCHING_FLAG     = @MATCHING_FLAG      " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE       " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME       " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID       " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER       " & vbNewLine _
                                              & ",SYS_DEL_FLG       = @SYS_DEL_FLG        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD          " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO         " & vbNewLine
#End Region

#Region "H_OUTKAEDI_L(まとめ先の更新用)"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_MATOMESAKI_OUTKAEDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET      " & vbNewLine _
                                              & " FREE_C30          = @FREE_C30                        " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                        " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                        " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                        " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                    " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                    " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                    " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                    " & vbNewLine _
                                              & "WHERE                                                 " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                       " & vbNewLine _
                                              & " AND                                                  " & vbNewLine _
                                              & " OUTKA_CTL_NO      = @OUTKA_CTL_NO                    " & vbNewLine _
                                              & " AND                                                  " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                      " & vbNewLine _
                                              & " AND                                                  " & vbNewLine _
                                              & " SYS_DEL_FLG       <> '1'                             " & vbNewLine


#End Region

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' H_OUTKAEDI_MのUPDATE文（H_OUTKAEDI_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKAEDI_M As String = "UPDATE $LM_TRN$..H_OUTKAEDI_M SET      " & vbNewLine _
                                          & " DEL_KB            =  @DEL_KB            	    " & vbNewLine _
                                          & ",OUTKA_CTL_NO      =  @OUTKA_CTL_NO            " & vbNewLine _
                                          & ",OUTKA_CTL_NO_CHU  =  @OUTKA_CTL_NO_CHU        " & vbNewLine _
                                          & ",COA_YN            =  @COA_YN                  " & vbNewLine _
                                          & ",CUST_ORD_NO_DTL   =  @CUST_ORD_NO_DTL         " & vbNewLine _
                                          & ",BUYER_ORD_NO_DTL  =  @BUYER_ORD_NO_DTL        " & vbNewLine _
                                          & ",CUST_GOODS_CD     =  @CUST_GOODS_CD           " & vbNewLine _
                                          & ",NRS_GOODS_CD      =  @NRS_GOODS_CD            " & vbNewLine _
                                          & ",GOODS_NM          =  @GOODS_NM                " & vbNewLine _
                                          & ",RSV_NO            =  @RSV_NO                  " & vbNewLine _
                                          & ",LOT_NO            =  @LOT_NO                  " & vbNewLine _
                                          & ",SERIAL_NO         =  @SERIAL_NO               " & vbNewLine _
                                          & ",ALCTD_KB          =  @ALCTD_KB                " & vbNewLine _
                                          & ",OUTKA_PKG_NB      =  @OUTKA_PKG_NB            " & vbNewLine _
                                          & ",OUTKA_HASU        =  @OUTKA_HASU              " & vbNewLine _
                                          & ",OUTKA_QT          =  @OUTKA_QT                " & vbNewLine _
                                          & ",OUTKA_TTL_NB      =  @OUTKA_TTL_NB            " & vbNewLine _
                                          & ",OUTKA_TTL_QT      =  @OUTKA_TTL_QT            " & vbNewLine _
                                          & ",KB_UT             =  @KB_UT                   " & vbNewLine _
                                          & ",QT_UT             =  @QT_UT                   " & vbNewLine _
                                          & ",PKG_NB            =  @PKG_NB                  " & vbNewLine _
                                          & ",PKG_UT            =  @PKG_UT                  " & vbNewLine _
                                          & ",ONDO_KB           =  @ONDO_KB                 " & vbNewLine _
                                          & ",UNSO_ONDO_KB      =  @UNSO_ONDO_KB            " & vbNewLine _
                                          & ",IRIME             =  @IRIME                   " & vbNewLine _
                                          & ",IRIME_UT          =  @IRIME_UT                " & vbNewLine _
                                          & ",BETU_WT           =  @BETU_WT                 " & vbNewLine _
                                          & ",REMARK            =  @REMARK                  " & vbNewLine _
                                          & ",OUT_KB            =  @OUT_KB                  " & vbNewLine _
                                          & ",AKAKURO_KB        =  @AKAKURO_KB              " & vbNewLine _
                                          & ",JISSEKI_FLAG      =  @JISSEKI_FLAG            " & vbNewLine _
                                          & ",JISSEKI_USER      =  @JISSEKI_USER            " & vbNewLine _
                                          & ",JISSEKI_DATE      =  @JISSEKI_DATE            " & vbNewLine _
                                          & ",JISSEKI_TIME      =  @JISSEKI_TIME            " & vbNewLine _
                                          & ",SET_KB            =  @SET_KB                  " & vbNewLine _
                                          & ",FREE_N01          =  @FREE_N01                " & vbNewLine _
                                          & ",FREE_N02          =  @FREE_N02                " & vbNewLine _
                                          & ",FREE_N03          =  @FREE_N03                " & vbNewLine _
                                          & ",FREE_N04          =  @FREE_N04                " & vbNewLine _
                                          & ",FREE_N05          =  @FREE_N05                " & vbNewLine _
                                          & ",FREE_N06          =  @FREE_N06                " & vbNewLine _
                                          & ",FREE_N07          =  @FREE_N07                " & vbNewLine _
                                          & ",FREE_N08          =  @FREE_N08                " & vbNewLine _
                                          & ",FREE_N09          =  @FREE_N09                " & vbNewLine _
                                          & ",FREE_N10          =  @FREE_N10                " & vbNewLine _
                                          & ",FREE_C01          =  @FREE_C01                " & vbNewLine _
                                          & ",FREE_C02          =  @FREE_C02                " & vbNewLine _
                                          & ",FREE_C03          =  @FREE_C03                " & vbNewLine _
                                          & ",FREE_C04          =  @FREE_C04                " & vbNewLine _
                                          & ",FREE_C05          =  @FREE_C05                " & vbNewLine _
                                          & ",FREE_C06          =  @FREE_C06                " & vbNewLine _
                                          & ",FREE_C07          =  @FREE_C07                " & vbNewLine _
                                          & ",FREE_C08          =  @FREE_C08                " & vbNewLine _
                                          & ",FREE_C09          =  @FREE_C09                " & vbNewLine _
                                          & ",FREE_C10          =  @FREE_C10                " & vbNewLine _
                                          & ",FREE_C11          =  @FREE_C11                " & vbNewLine _
                                          & ",FREE_C12          =  @FREE_C12                " & vbNewLine _
                                          & ",FREE_C13          =  @FREE_C13                " & vbNewLine _
                                          & ",FREE_C14          =  @FREE_C14                " & vbNewLine _
                                          & ",FREE_C15          =  @FREE_C15                " & vbNewLine _
                                          & ",FREE_C16          =  @FREE_C16                " & vbNewLine _
                                          & ",FREE_C17          =  @FREE_C17                " & vbNewLine _
                                          & ",FREE_C18          =  @FREE_C18                " & vbNewLine _
                                          & ",FREE_C19          =  @FREE_C19                " & vbNewLine _
                                          & ",FREE_C20          =  @FREE_C20                " & vbNewLine _
                                          & ",FREE_C21          =  @FREE_C21                " & vbNewLine _
                                          & ",FREE_C22          =  @FREE_C22                " & vbNewLine _
                                          & ",FREE_C23          =  @FREE_C23                " & vbNewLine _
                                          & ",FREE_C24          =  @FREE_C24                " & vbNewLine _
                                          & ",FREE_C25          =  @FREE_C25                " & vbNewLine _
                                          & ",FREE_C26          =  @FREE_C26                " & vbNewLine _
                                          & ",FREE_C27          =  @FREE_C27                " & vbNewLine _
                                          & ",FREE_C28          =  @FREE_C28                " & vbNewLine _
                                          & ",FREE_C29          =  @FREE_C29                " & vbNewLine _
                                          & ",FREE_C30          =  @FREE_C30                " & vbNewLine _
                                          & ",CRT_USER          =  @CRT_USER                " & vbNewLine _
                                          & ",CRT_DATE          =  @CRT_DATE                " & vbNewLine _
                                          & ",CRT_TIME          =  @CRT_TIME                " & vbNewLine _
                                          & ",UPD_USER          =  @UPD_USER                " & vbNewLine _
                                          & ",UPD_DATE          =  @UPD_DATE                " & vbNewLine _
                                          & ",UPD_TIME          =  @UPD_TIME                " & vbNewLine _
                                          & ",SCM_CTL_NO_L      =  @SCM_CTL_NO_L            " & vbNewLine _
                                          & ",SCM_CTL_NO_M      =  @SCM_CTL_NO_M            " & vbNewLine _
                                          & ",SYS_UPD_DATE      =  @SYS_UPD_DATE            " & vbNewLine _
                                          & ",SYS_UPD_TIME      =  @SYS_UPD_TIME            " & vbNewLine _
                                          & ",SYS_UPD_PGID      =  @SYS_UPD_PGID            " & vbNewLine _
                                          & ",SYS_UPD_USER      =  @SYS_UPD_USER            " & vbNewLine _
                                          & ",SYS_DEL_FLG       =  @SYS_DEL_FLG             " & vbNewLine _
                                          & "WHERE   NRS_BR_CD  =  @NRS_BR_CD               " & vbNewLine _
                                          & "AND EDI_CTL_NO     =  @EDI_CTL_NO              " & vbNewLine _
                                          & "AND EDI_CTL_NO_CHU =  @EDI_CTL_NO_CHU          " & vbNewLine
#End Region

#Region "C_OUTKA_L(まとめ)"


    ''' <summary>
    ''' まとめ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKAL_MATOME As String = "UPDATE $LM_TRN$..C_OUTKA_L SET " & vbNewLine _
                                          & " OUTKA_PKG_NB      = @OUTKA_PKG_NB        " & vbNewLine _
                                          & ",CUST_ORD_NO       = @CUST_ORD_NO         " & vbNewLine _
                                          & ",SYS_UPD_DATE      = @SYS_UPD_DATE        " & vbNewLine _
                                          & ",SYS_UPD_TIME      = @SYS_UPD_TIME        " & vbNewLine _
                                          & ",SYS_UPD_PGID      = @SYS_UPD_PGID        " & vbNewLine _
                                          & ",SYS_UPD_USER      = @SYS_UPD_USER        " & vbNewLine _
                                          & "WHERE   NRS_BR_CD  = @NRS_BR_CD           " & vbNewLine _
                                          & "AND OUTKA_NO_L     = @OUTKA_NO_L          " & vbNewLine _
                                          & "AND SYS_DEL_FLG     <> '1'                " & vbNewLine

#If True Then 'フィルメニッヒ セミEDI対応  20160926 added inoue
    ''' <summary>
    ''' まとめ(注文番号更新付)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKAL_MATOME_WITH_BUYER_ORD_NO As String _
        = "UPDATE $LM_TRN$..C_OUTKA_L SET " & vbNewLine _
        & " OUTKA_PKG_NB      = @OUTKA_PKG_NB        " & vbNewLine _
        & ",CUST_ORD_NO       = @CUST_ORD_NO         " & vbNewLine _
        & ",BUYER_ORD_NO      = @BUYER_ORD_NO        " & vbNewLine _
        & ",REMARK            = @REMARK              " & vbNewLine _
        & ",SYS_UPD_DATE      = @SYS_UPD_DATE        " & vbNewLine _
        & ",SYS_UPD_TIME      = @SYS_UPD_TIME        " & vbNewLine _
        & ",SYS_UPD_PGID      = @SYS_UPD_PGID        " & vbNewLine _
        & ",SYS_UPD_USER      = @SYS_UPD_USER        " & vbNewLine _
        & "WHERE NRS_BR_CD    = @NRS_BR_CD           " & vbNewLine _
        & "  AND OUTKA_NO_L   = @OUTKA_NO_L          " & vbNewLine _
        & "  AND SYS_DEL_FLG  <> '1'                 " & vbNewLine
#End If


#End Region

#Region "F_UNSO_L(まとめ)"
    ''' <summary>
    ''' F_UNSO_LのUPDATE文（F_UNSOI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_MATOMESAKI_UNSO_L As String = "UPDATE $LM_TRN$..F_UNSO_L SET   " & vbNewLine _
                                              & " UNSO_WT          = @UNSO_WT               " & vbNewLine _
                                              & ",UNSO_PKG_NB       = @UNSO_PKG_NB          " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE         " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME         " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID         " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER         " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD            " & vbNewLine _
                                              & "AND UNSO_NO_L   = @UNSO_NO_L               " & vbNewLine _
                                              & "AND SYS_DEL_FLG     <> '1'                 " & vbNewLine
#End Region

#Region "M_DEST UPDATE(届先項目差異有りの場合)(自動追加更新時使用)"
    ''' <summary>
    ''' 届先マスタUPDATE文（M_DEST）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_M_DEST As String = "UPDATE $LM_MST$..M_DEST SET       " & vbNewLine _
                                              & " DEST_NM           = @DEST_NM       	" & vbNewLine _
                                              & ",ZIP               = @ZIP              " & vbNewLine _
                                              & ",AD_1              = @AD_1             " & vbNewLine _
                                              & ",AD_2              = @AD_2             " & vbNewLine _
                                              & ",AD_3              = @AD_3             " & vbNewLine _
                                              & ",TEL               = @TEL              " & vbNewLine _
                                              & ",JIS               = @JIS              " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE     " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME     " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID     " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER     " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD        " & vbNewLine _
                                              & "AND CUST_CD_L      = @CUST_CD_L        " & vbNewLine _
                                              & "AND DEST_CD        = @DEST_CD          " & vbNewLine

#End Region

#End Region

#Region "出荷登録処理 新規用"
#Region "C_OUTKA_L(INSERT)"

    ''' <summary>
    ''' INSERT（OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTKA_L As String = "INSERT INTO                                        " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_L                                      " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",OUTKA_NO_L                                              " & vbNewLine _
                                         & ",FURI_NO                                                 " & vbNewLine _
                                         & ",OUTKA_KB                                                " & vbNewLine _
                                         & ",SYUBETU_KB                                              " & vbNewLine _
                                         & ",OUTKA_STATE_KB                                          " & vbNewLine _
                                         & ",OUTKAHOKOKU_YN                                          " & vbNewLine _
                                         & ",PICK_KB                                                 " & vbNewLine _
                                         & ",DENP_NO                                                 " & vbNewLine _
                                         & ",ARR_KANRYO_INFO                                         " & vbNewLine _
                                         & ",WH_CD                                                   " & vbNewLine _
                                         & ",OUTKA_PLAN_DATE                                         " & vbNewLine _
                                         & ",OUTKO_DATE                                              " & vbNewLine _
                                         & ",ARR_PLAN_DATE                                           " & vbNewLine _
                                         & ",ARR_PLAN_TIME                                           " & vbNewLine _
                                         & ",HOKOKU_DATE                                             " & vbNewLine _
                                         & ",TOUKI_HOKAN_YN                                          " & vbNewLine _
                                         & ",END_DATE                                                " & vbNewLine _
                                         & ",CUST_CD_L                                               " & vbNewLine _
                                         & ",CUST_CD_M                                               " & vbNewLine _
                                         & ",SHIP_CD_L                                               " & vbNewLine _
                                         & ",SHIP_CD_M                                               " & vbNewLine _
                                         & ",DEST_CD                                                 " & vbNewLine _
                                         & ",DEST_AD_3                                               " & vbNewLine _
                                         & ",DEST_TEL                                                " & vbNewLine _
                                         & ",NHS_REMARK                                              " & vbNewLine _
                                         & ",SP_NHS_KB                                               " & vbNewLine _
                                         & ",COA_YN                                                  " & vbNewLine _
                                         & ",CUST_ORD_NO                                             " & vbNewLine _
                                         & ",BUYER_ORD_NO                                            " & vbNewLine _
                                         & ",REMARK                                                  " & vbNewLine _
                                         & ",OUTKA_PKG_NB                                            " & vbNewLine _
                                         & ",DENP_YN                                                 " & vbNewLine _
                                         & ",PC_KB                                                   " & vbNewLine _
                                         & ",NIYAKU_YN                                               " & vbNewLine _
                                         & ",ALL_PRINT_FLAG                                          " & vbNewLine _
                                         & ",NIHUDA_FLAG                                             " & vbNewLine _
                                         & ",NHS_FLAG                                                " & vbNewLine _
                                         & ",DENP_FLAG                                               " & vbNewLine _
                                         & ",COA_FLAG                                                " & vbNewLine _
                                         & ",HOKOKU_FLAG                                             " & vbNewLine _
                                         & ",MATOME_PICK_FLAG                                        " & vbNewLine _
                                         & ",LAST_PRINT_DATE                                         " & vbNewLine _
                                         & ",LAST_PRINT_TIME                                         " & vbNewLine _
                                         & ",SASZ_USER                                               " & vbNewLine _
                                         & ",OUTKO_USER                                              " & vbNewLine _
                                         & ",KEN_USER                                                " & vbNewLine _
                                         & ",OUTKA_USER                                              " & vbNewLine _
                                         & ",HOU_USER                                                " & vbNewLine _
                                         & ",ORDER_TYPE                                              " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ",DEST_KB                                                 " & vbNewLine _
                                         & ",DEST_NM                                                 " & vbNewLine _
                                         & ",DEST_AD_1                                               " & vbNewLine _
                                         & ",DEST_AD_2                                               " & vbNewLine _
                                         & ",WH_TAB_STATUS                                           " & vbNewLine _
                                         & ",WH_TAB_YN                                               " & vbNewLine _
                                         & ")VALUES(                                                 " & vbNewLine _
                                         & " @NRS_BR_CD                                              " & vbNewLine _
                                         & ",@OUTKA_NO_L                                             " & vbNewLine _
                                         & ",@FURI_NO                                                " & vbNewLine _
                                         & ",@OUTKA_KB                                               " & vbNewLine _
                                         & ",@SYUBETU_KB                                             " & vbNewLine _
                                         & ",@OUTKA_STATE_KB                                         " & vbNewLine _
                                         & ",@OUTKAHOKOKU_YN                                         " & vbNewLine _
                                         & ",@PICK_KB                                                " & vbNewLine _
                                         & ",@DENP_NO                                                " & vbNewLine _
                                         & ",@ARR_KANRYO_INFO                                        " & vbNewLine _
                                         & ",@WH_CD                                                  " & vbNewLine _
                                         & ",@OUTKA_PLAN_DATE                                        " & vbNewLine _
                                         & ",@OUTKO_DATE                                             " & vbNewLine _
                                         & ",@ARR_PLAN_DATE                                          " & vbNewLine _
                                         & ",@ARR_PLAN_TIME                                          " & vbNewLine _
                                         & ",@HOKOKU_DATE                                            " & vbNewLine _
                                         & ",@TOUKI_HOKAN_YN                                         " & vbNewLine _
                                         & ",@END_DATE                                               " & vbNewLine _
                                         & ",@CUST_CD_L                                              " & vbNewLine _
                                         & ",@CUST_CD_M                                              " & vbNewLine _
                                         & ",@SHIP_CD_L                                              " & vbNewLine _
                                         & ",@SHIP_CD_M                                              " & vbNewLine _
                                         & ",@DEST_CD                                                " & vbNewLine _
                                         & ",@DEST_AD_3                                              " & vbNewLine _
                                         & ",@DEST_TEL                                               " & vbNewLine _
                                         & ",@NHS_REMARK                                             " & vbNewLine _
                                         & ",@SP_NHS_KB                                              " & vbNewLine _
                                         & ",@COA_YN                                                 " & vbNewLine _
                                         & ",@CUST_ORD_NO                                            " & vbNewLine _
                                         & ",@BUYER_ORD_NO                                           " & vbNewLine _
                                         & ",@REMARK                                                 " & vbNewLine _
                                         & ",@OUTKA_PKG_NB                                           " & vbNewLine _
                                         & ",@DENP_YN                                                " & vbNewLine _
                                         & ",@PC_KB                                                  " & vbNewLine _
                                         & ",@NIYAKU_YN                                              " & vbNewLine _
                                         & ",@ALL_PRINT_FLAG                                         " & vbNewLine _
                                         & ",@NIHUDA_FLAG                                            " & vbNewLine _
                                         & ",@NHS_FLAG                                               " & vbNewLine _
                                         & ",@DENP_FLAG                                              " & vbNewLine _
                                         & ",@COA_FLAG                                               " & vbNewLine _
                                         & ",@HOKOKU_FLAG                                            " & vbNewLine _
                                         & ",@MATOME_PICK_FLAG                                       " & vbNewLine _
                                         & ",@LAST_PRINT_DATE                                        " & vbNewLine _
                                         & ",@LAST_PRINT_TIME                                        " & vbNewLine _
                                         & ",@SASZ_USER                                              " & vbNewLine _
                                         & ",@OUTKO_USER                                             " & vbNewLine _
                                         & ",@KEN_USER                                               " & vbNewLine _
                                         & ",@OUTKA_USER                                             " & vbNewLine _
                                         & ",@HOU_USER                                               " & vbNewLine _
                                         & ",@ORDER_TYPE                                             " & vbNewLine _
                                         & ",@SYS_ENT_DATE                                           " & vbNewLine _
                                         & ",@SYS_ENT_TIME                                           " & vbNewLine _
                                         & ",@SYS_ENT_PGID                                           " & vbNewLine _
                                         & ",@SYS_ENT_USER                                           " & vbNewLine _
                                         & ",@SYS_UPD_DATE                                           " & vbNewLine _
                                         & ",@SYS_UPD_TIME                                           " & vbNewLine _
                                         & ",@SYS_UPD_PGID                                           " & vbNewLine _
                                         & ",@SYS_UPD_USER                                           " & vbNewLine _
                                         & ",@SYS_DEL_FLG                                            " & vbNewLine _
                                         & ",@DEST_KB                                                " & vbNewLine _
                                         & ",@DEST_NM                                                " & vbNewLine _
                                         & ",@DEST_AD_1                                              " & vbNewLine _
                                         & ",@DEST_AD_2                                              " & vbNewLine _
                                         & ",@WH_TAB_STATUS                                          " & vbNewLine _
                                         & ",@WH_TAB_YN                                              " & vbNewLine _
                                         & ")                                                        " & vbNewLine
#End Region

#Region "OUTKA_M"

    ''' <summary>
    ''' INSERT（OUTKA_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTKA_M As String = "INSERT INTO                                        " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_M                                      " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",OUTKA_NO_L                                              " & vbNewLine _
                                         & ",OUTKA_NO_M                                              " & vbNewLine _
                                         & ",EDI_SET_NO                                              " & vbNewLine _
                                         & ",COA_YN                                                  " & vbNewLine _
                                         & ",CUST_ORD_NO_DTL                                         " & vbNewLine _
                                         & ",BUYER_ORD_NO_DTL                                        " & vbNewLine _
                                         & ",GOODS_CD_NRS                                            " & vbNewLine _
                                         & ",RSV_NO                                                  " & vbNewLine _
                                         & ",LOT_NO                                                  " & vbNewLine _
                                         & ",SERIAL_NO                                               " & vbNewLine _
                                         & ",ALCTD_KB                                                " & vbNewLine _
                                         & ",OUTKA_PKG_NB                                            " & vbNewLine _
                                         & ",OUTKA_HASU                                              " & vbNewLine _
                                         & ",OUTKA_QT                                                " & vbNewLine _
                                         & ",OUTKA_TTL_NB                                            " & vbNewLine _
                                         & ",OUTKA_TTL_QT                                            " & vbNewLine _
                                         & ",ALCTD_NB                                                " & vbNewLine _
                                         & ",ALCTD_QT                                                " & vbNewLine _
                                         & ",BACKLOG_NB                                              " & vbNewLine _
                                         & ",BACKLOG_QT                                              " & vbNewLine _
                                         & ",UNSO_ONDO_KB                                            " & vbNewLine _
                                         & ",IRIME                                                   " & vbNewLine _
                                         & ",IRIME_UT                                                " & vbNewLine _
                                         & ",OUTKA_M_PKG_NB                                          " & vbNewLine _
                                         & ",REMARK                                                  " & vbNewLine _
                                         & ",SIZE_KB                                                 " & vbNewLine _
                                         & ",ZAIKO_KB                                                " & vbNewLine _
                                         & ",SOURCE_CD                                               " & vbNewLine _
                                         & ",YELLOW_CARD                                             " & vbNewLine _
                                         & ",PRINT_SORT                                              " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ")VALUES(                                                 " & vbNewLine _
                                         & " @NRS_BR_CD                                              " & vbNewLine _
                                         & ",@OUTKA_NO_L                                             " & vbNewLine _
                                         & ",@OUTKA_NO_M                                             " & vbNewLine _
                                         & ",@EDI_SET_NO                                             " & vbNewLine _
                                         & ",@COA_YN                                                 " & vbNewLine _
                                         & ",@CUST_ORD_NO_DTL                                        " & vbNewLine _
                                         & ",@BUYER_ORD_NO_DTL                                       " & vbNewLine _
                                         & ",@GOODS_CD_NRS                                           " & vbNewLine _
                                         & ",@RSV_NO                                                 " & vbNewLine _
                                         & ",@LOT_NO                                                 " & vbNewLine _
                                         & ",@SERIAL_NO                                              " & vbNewLine _
                                         & ",@ALCTD_KB                                               " & vbNewLine _
                                         & ",@OUTKA_PKG_NB                                           " & vbNewLine _
                                         & ",@OUTKA_HASU                                             " & vbNewLine _
                                         & ",@OUTKA_QT                                               " & vbNewLine _
                                         & ",@OUTKA_TTL_NB                                           " & vbNewLine _
                                         & ",@OUTKA_TTL_QT                                           " & vbNewLine _
                                         & ",@ALCTD_NB                                               " & vbNewLine _
                                         & ",@ALCTD_QT                                               " & vbNewLine _
                                         & ",@BACKLOG_NB                                             " & vbNewLine _
                                         & ",@BACKLOG_QT                                             " & vbNewLine _
                                         & ",@UNSO_ONDO_KB                                           " & vbNewLine _
                                         & ",@IRIME                                                  " & vbNewLine _
                                         & ",@IRIME_UT                                               " & vbNewLine _
                                         & ",@OUTKA_M_PKG_NB                                         " & vbNewLine _
                                         & ",@REMARK                                                 " & vbNewLine _
                                         & ",@SIZE_KB                                                " & vbNewLine _
                                         & ",@ZAIKO_KB                                               " & vbNewLine _
                                         & ",@SOURCE_CD                                              " & vbNewLine _
                                         & ",@YELLOW_CARD                                            " & vbNewLine _
                                         & ",@PRINT_SORT                                             " & vbNewLine _
                                         & ",@SYS_ENT_DATE                                           " & vbNewLine _
                                         & ",@SYS_ENT_TIME                                           " & vbNewLine _
                                         & ",@SYS_ENT_PGID                                           " & vbNewLine _
                                         & ",@SYS_ENT_USER                                           " & vbNewLine _
                                         & ",@SYS_UPD_DATE                                           " & vbNewLine _
                                         & ",@SYS_UPD_TIME                                           " & vbNewLine _
                                         & ",@SYS_UPD_PGID                                           " & vbNewLine _
                                         & ",@SYS_UPD_USER                                           " & vbNewLine _
                                         & ",@SYS_DEL_FLG                                            " & vbNewLine _
                                         & ")                                                        " & vbNewLine

#End Region

#Region "SAGYO"

    ''' <summary>
    ''' INSERT（SAGYO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SAGYO As String = "INSERT INTO                                          " & vbNewLine _
                                         & "$LM_TRN$..E_SAGYO                                        " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",SAGYO_REC_NO                                            " & vbNewLine _
                                         & ",SAGYO_COMP                                              " & vbNewLine _
                                         & ",SKYU_CHK                                                " & vbNewLine _
                                         & ",SAGYO_SIJI_NO                                           " & vbNewLine _
                                         & ",INOUTKA_NO_LM                                           " & vbNewLine _
                                         & ",WH_CD                                                   " & vbNewLine _
                                         & ",IOZS_KB                                                 " & vbNewLine _
                                         & ",SAGYO_CD                                                " & vbNewLine _
                                         & ",SAGYO_NM                                                " & vbNewLine _
                                         & ",CUST_CD_L                                               " & vbNewLine _
                                         & ",CUST_CD_M                                               " & vbNewLine _
                                         & ",DEST_CD                                                 " & vbNewLine _
                                         & ",DEST_NM                                                 " & vbNewLine _
                                         & ",GOODS_CD_NRS                                            " & vbNewLine _
                                         & ",GOODS_NM_NRS                                            " & vbNewLine _
                                         & ",LOT_NO                                                  " & vbNewLine _
                                         & ",INV_TANI                                                " & vbNewLine _
                                         & ",SAGYO_NB                                                " & vbNewLine _
                                         & ",SAGYO_UP                                                " & vbNewLine _
                                         & ",SAGYO_GK                                                " & vbNewLine _
                                         & ",TAX_KB                                                  " & vbNewLine _
                                         & ",SEIQTO_CD                                               " & vbNewLine _
                                         & ",REMARK_ZAI                                              " & vbNewLine _
                                         & ",REMARK_SKYU                                             " & vbNewLine _
                                         & ",REMARK_SIJI                                             " & vbNewLine _
                                         & ",SAGYO_COMP_CD                                           " & vbNewLine _
                                         & ",SAGYO_COMP_DATE                                         " & vbNewLine _
                                         & ",DEST_SAGYO_FLG                                          " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ")SELECT                                                  " & vbNewLine _
                                         & " @NRS_BR_CD                                              " & vbNewLine _
                                         & ",@SAGYO_REC_NO                                           " & vbNewLine _
                                         & ",@SAGYO_COMP                                             " & vbNewLine _
                                         & ",@SKYU_CHK                                               " & vbNewLine _
                                         & ",@SAGYO_SIJI_NO                                          " & vbNewLine _
                                         & ",@INOUTKA_NO_LM                                          " & vbNewLine _
                                         & ",@WH_CD                                                  " & vbNewLine _
                                         & ",@IOZS_KB                                                " & vbNewLine _
                                         & ",@SAGYO_CD                                               " & vbNewLine _
                                         & ",M_SAGYO.SAGYO_NM                                        " & vbNewLine _
                                         & ",@CUST_CD_L                                              " & vbNewLine _
                                         & ",@CUST_CD_M                                              " & vbNewLine _
                                         & ",@DEST_CD                                                " & vbNewLine _
                                         & ",@DEST_NM                                                " & vbNewLine _
                                         & ",@GOODS_CD_NRS                                           " & vbNewLine _
                                         & ",@GOODS_NM_NRS                                           " & vbNewLine _
                                         & ",@LOT_NO                                                 " & vbNewLine _
                                         & ",M_SAGYO.INV_TANI                                        " & vbNewLine _
                                         & ",@SAGYO_NB                                               " & vbNewLine _
                                         & ",M_SAGYO.SAGYO_UP                                        " & vbNewLine _
                                         & ",@SAGYO_GK                                               " & vbNewLine _
                                         & ",M_SAGYO.ZEI_KBN                                         " & vbNewLine _
                                         & ",M_CUST.SAGYO_SEIQTO_CD                                  " & vbNewLine _
                                         & ",M_SAGYO.SAGYO_REMARK                                    " & vbNewLine _
                                         & ",@REMARK_SKYU                                            " & vbNewLine _
                                         & ",M_SAGYO.WH_SAGYO_REMARK                                 " & vbNewLine _
                                         & ",@SAGYO_COMP_CD                                          " & vbNewLine _
                                         & ",@SAGYO_COMP_DATE                                        " & vbNewLine _
                                         & ",@DEST_SAGYO_FLG                                         " & vbNewLine _
                                         & ",@SYS_ENT_DATE                                           " & vbNewLine _
                                         & ",@SYS_ENT_TIME                                           " & vbNewLine _
                                         & ",@SYS_ENT_PGID                                           " & vbNewLine _
                                         & ",@SYS_ENT_USER                                           " & vbNewLine _
                                         & ",@SYS_UPD_DATE                                           " & vbNewLine _
                                         & ",@SYS_UPD_TIME                                           " & vbNewLine _
                                         & ",@SYS_UPD_PGID                                           " & vbNewLine _
                                         & ",@SYS_UPD_USER                                           " & vbNewLine _
                                         & ",@SYS_DEL_FLG                                            " & vbNewLine _
                                         & "FROM                                                     " & vbNewLine _
                                         & "$LM_MST$..M_SAGYO   M_SAGYO                              " & vbNewLine _
                                         & ",$LM_MST$..M_GOODS  M_GOODS                              " & vbNewLine _
                                         & "LEFT JOIN                                                " & vbNewLine _
                                         & "$LM_MST$..M_CUST  M_CUST                                 " & vbNewLine _
                                         & "ON                                                       " & vbNewLine _
                                         & "M_GOODS.NRS_BR_CD  = M_CUST.NRS_BR_CD                    " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.CUST_CD_L  = M_CUST.CUST_CD_L                    " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.CUST_CD_M  = M_CUST.CUST_CD_M                    " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.CUST_CD_S  = M_CUST.CUST_CD_S                    " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.CUST_CD_SS  = M_CUST.CUST_CD_SS                  " & vbNewLine _
                                         & "WHERE                                                    " & vbNewLine _
                                         & "M_SAGYO.SAGYO_CD  = @SAGYO_CD                            " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.NRS_BR_CD  = @NRS_BR_CD                          " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.GOODS_CD_NRS  = @GOODS_CD_NRS                    " & vbNewLine

#End Region

#Region "UNSO_L"

    Private Const SQL_INSERT_UNSO_L As String = "INSERT INTO $LM_TRN$..F_UNSO_L" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",UNSO_NO_L                    " & vbNewLine _
                                              & ",YUSO_BR_CD                   " & vbNewLine _
                                              & ",INOUTKA_NO_L                 " & vbNewLine _
                                              & ",TRIP_NO                      " & vbNewLine _
                                              & ",UNSO_CD                      " & vbNewLine _
                                              & ",UNSO_BR_CD                   " & vbNewLine _
                                              & ",BIN_KB                       " & vbNewLine _
                                              & ",JIYU_KB                      " & vbNewLine _
                                              & ",DENP_NO                      " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE              " & vbNewLine _
                                              & ",OUTKA_PLAN_TIME              " & vbNewLine _
                                              & ",ARR_PLAN_DATE                " & vbNewLine _
                                              & ",ARR_PLAN_TIME                " & vbNewLine _
                                              & ",ARR_ACT_TIME                 " & vbNewLine _
                                              & ",CUST_CD_L                    " & vbNewLine _
                                              & ",CUST_CD_M                    " & vbNewLine _
                                              & ",CUST_REF_NO                  " & vbNewLine _
                                              & ",SHIP_CD                      " & vbNewLine _
                                              & ",ORIG_CD                      " & vbNewLine _
                                              & ",DEST_CD                      " & vbNewLine _
                                              & ",UNSO_PKG_NB                  " & vbNewLine _
                                              & ",NB_UT                        " & vbNewLine _
                                              & ",UNSO_WT                      " & vbNewLine _
                                              & ",UNSO_ONDO_KB                 " & vbNewLine _
                                              & ",PC_KB                        " & vbNewLine _
                                              & ",TARIFF_BUNRUI_KB             " & vbNewLine _
                                              & ",VCLE_KB                      " & vbNewLine _
                                              & ",MOTO_DATA_KB                 " & vbNewLine _
                                              & ",TAX_KB                       " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",SEIQ_TARIFF_CD               " & vbNewLine _
                                              & ",SEIQ_ETARIFF_CD              " & vbNewLine _
                                              & ",AD_3                         " & vbNewLine _
                                              & ",UNSO_TEHAI_KB                " & vbNewLine _
                                              & ",BUY_CHU_NO                   " & vbNewLine _
                                              & ",AREA_CD                      " & vbNewLine _
                                              & ",TYUKEI_HAISO_FLG             " & vbNewLine _
                                              & ",SYUKA_TYUKEI_CD              " & vbNewLine _
                                              & ",HAIKA_TYUKEI_CD              " & vbNewLine _
                                              & ",TRIP_NO_SYUKA                " & vbNewLine _
                                              & ",TRIP_NO_TYUKEI               " & vbNewLine _
                                              & ",TRIP_NO_HAIKA                " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & "--'START UMANO 要望番号1302 支払運賃に伴う修正" & vbNewLine _
                                              & ",SHIHARAI_TARIFF_CD           " & vbNewLine _
                                              & ",SHIHARAI_ETARIFF_CD          " & vbNewLine _
                                              & "--'END   UMANO 要望番号1302 支払運賃に伴う修正" & vbNewLine _
                                              & " )SELECT                      " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@UNSO_NO_L                   " & vbNewLine _
                                              & ",@YUSO_BR_CD                  " & vbNewLine _
                                              & ",@INOUTKA_NO_L                " & vbNewLine _
                                              & ",@TRIP_NO                     " & vbNewLine _
                                              & ",@UNSO_CD                     " & vbNewLine _
                                              & ",@UNSO_BR_CD                  " & vbNewLine _
                                              & ",@BIN_KB                      " & vbNewLine _
                                              & ",@JIYU_KB                     " & vbNewLine _
                                              & ",@DENP_NO                     " & vbNewLine _
                                              & ",@OUTKA_PLAN_DATE             " & vbNewLine _
                                              & ",@OUTKA_PLAN_TIME             " & vbNewLine _
                                              & ",@ARR_PLAN_DATE               " & vbNewLine _
                                              & ",@ARR_PLAN_TIME               " & vbNewLine _
                                              & ",@ARR_ACT_TIME                " & vbNewLine _
                                              & ",@CUST_CD_L                   " & vbNewLine _
                                              & ",@CUST_CD_M                   " & vbNewLine _
                                              & ",@CUST_REF_NO                 " & vbNewLine _
                                              & ",@SHIP_CD                     " & vbNewLine _
                                              & ",M_SOKO.SOKO_DEST_CD          " & vbNewLine _
                                              & ",@DEST_CD                     " & vbNewLine _
                                              & ",@UNSO_PKG_NB                 " & vbNewLine _
                                              & ",@NB_UT                       " & vbNewLine _
                                              & ",@UNSO_WT                     " & vbNewLine _
                                              & ",@UNSO_ONDO_KB                " & vbNewLine _
                                              & ",@PC_KB                       " & vbNewLine _
                                              & ",@TARIFF_BUNRUI_KB            " & vbNewLine _
                                              & ",@VCLE_KB                     " & vbNewLine _
                                              & ",@MOTO_DATA_KB                " & vbNewLine _
                                              & ",@TAX_KB                      " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@SEIQ_TARIFF_CD              " & vbNewLine _
                                              & ",@SEIQ_ETARIFF_CD             " & vbNewLine _
                                              & ",@AD_3                        " & vbNewLine _
                                              & ",@UNSO_TEHAI_KB               " & vbNewLine _
                                              & ",@BUY_CHU_NO                  " & vbNewLine _
                                              & ",@AREA_CD                     " & vbNewLine _
                                              & ",@TYUKEI_HAISO_FLG            " & vbNewLine _
                                              & ",@SYUKA_TYUKEI_CD             " & vbNewLine _
                                              & ",@HAIKA_TYUKEI_CD             " & vbNewLine _
                                              & ",@TRIP_NO_SYUKA               " & vbNewLine _
                                              & ",@TRIP_NO_TYUKEI              " & vbNewLine _
                                              & ",@TRIP_NO_HAIKA               " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & "--'START UMANO 要望番号1302 支払運賃に伴う修正" & vbNewLine _
                                              & ",@SHIHARAI_TARIFF_CD          " & vbNewLine _
                                              & ",@SHIHARAI_ETARIFF_CD         " & vbNewLine _
                                              & "--'END   UMANO 要望番号1302 支払運賃に伴う修正" & vbNewLine _
                                              & "FROM                          " & vbNewLine _
                                              & "$LM_MST$..M_SOKO   M_SOKO     " & vbNewLine _
                                              & ",$LM_MST$..M_CUST  M_CUST     " & vbNewLine _
                                              & "WHERE                                     " & vbNewLine _
                                              & "M_SOKO.NRS_BR_CD  = @NRS_BR_CD            " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_SOKO.WH_CD  = @WH_CD                    " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.NRS_BR_CD  = @NRS_BR_CD            " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.CUST_CD_L  = @CUST_CD_L            " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.CUST_CD_M  = @CUST_CD_M            " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.CUST_CD_S  = '00'                  " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.CUST_CD_SS  = '00'                 " & vbNewLine

#End Region

#Region "UNSO_M"

    Private Const SQL_INSERT_UNSO_M As String = "INSERT INTO $LM_TRN$..F_UNSO_M" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",UNSO_NO_L                    " & vbNewLine _
                                              & ",UNSO_NO_M                    " & vbNewLine _
                                              & ",GOODS_CD_NRS                 " & vbNewLine _
                                              & ",GOODS_NM                     " & vbNewLine _
                                              & ",UNSO_TTL_NB                  " & vbNewLine _
                                              & ",NB_UT                        " & vbNewLine _
                                              & ",UNSO_TTL_QT                  " & vbNewLine _
                                              & ",QT_UT                        " & vbNewLine _
                                              & ",HASU                         " & vbNewLine _
                                              & ",ZAI_REC_NO                   " & vbNewLine _
                                              & ",UNSO_ONDO_KB                 " & vbNewLine _
                                              & ",IRIME                        " & vbNewLine _
                                              & ",IRIME_UT                     " & vbNewLine _
                                              & ",BETU_WT                      " & vbNewLine _
                                              & ",SIZE_KB                      " & vbNewLine _
                                              & ",ZBUKA_CD                     " & vbNewLine _
                                              & ",ABUKA_CD                     " & vbNewLine _
                                              & ",PKG_NB                       " & vbNewLine _
                                              & ",LOT_NO                       " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@UNSO_NO_L                   " & vbNewLine _
                                              & ",@UNSO_NO_M                   " & vbNewLine _
                                              & ",@GOODS_CD_NRS                " & vbNewLine _
                                              & ",@GOODS_NM                    " & vbNewLine _
                                              & ",@UNSO_TTL_NB                 " & vbNewLine _
                                              & ",@NB_UT                       " & vbNewLine _
                                              & ",@UNSO_TTL_QT                 " & vbNewLine _
                                              & ",@QT_UT                       " & vbNewLine _
                                              & ",@HASU                        " & vbNewLine _
                                              & ",@ZAI_REC_NO                  " & vbNewLine _
                                              & ",@UNSO_ONDO_KB                " & vbNewLine _
                                              & ",@IRIME                       " & vbNewLine _
                                              & ",@IRIME_UT                    " & vbNewLine _
                                              & ",@BETU_WT                     " & vbNewLine _
                                              & ",@SIZE_KB                     " & vbNewLine _
                                              & ",@ZBUKA_CD                    " & vbNewLine _
                                              & ",@ABUKA_CD                    " & vbNewLine _
                                              & ",@PKG_NB                      " & vbNewLine _
                                              & ",@LOT_NO                      " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "UNCHIN"

    Private Const SQL_INSERT_UNCHIN As String = "INSERT INTO $LM_TRN$..F_UNCHIN_TRS" & vbNewLine _
                                              & "(                                 " & vbNewLine _
                                              & " YUSO_BR_CD                       " & vbNewLine _
                                              & ",NRS_BR_CD                        " & vbNewLine _
                                              & ",UNSO_NO_L                        " & vbNewLine _
                                              & ",UNSO_NO_M                        " & vbNewLine _
                                              & ",CUST_CD_L                        " & vbNewLine _
                                              & ",CUST_CD_M                        " & vbNewLine _
                                              & ",CUST_CD_S                        " & vbNewLine _
                                              & ",CUST_CD_SS                       " & vbNewLine _
                                              & ",SEIQ_GROUP_NO                    " & vbNewLine _
                                              & ",SEIQ_GROUP_NO_M                  " & vbNewLine _
                                              & ",SEIQTO_CD                        " & vbNewLine _
                                              & ",UNTIN_CALCULATION_KB             " & vbNewLine _
                                              & ",SEIQ_SYARYO_KB                   " & vbNewLine _
                                              & ",SEIQ_PKG_UT                      " & vbNewLine _
                                              & ",SEIQ_NG_NB                       " & vbNewLine _
                                              & ",SEIQ_DANGER_KB                   " & vbNewLine _
                                              & ",SEIQ_TARIFF_BUNRUI_KB            " & vbNewLine _
                                              & ",SEIQ_TARIFF_CD                   " & vbNewLine _
                                              & ",SEIQ_ETARIFF_CD                  " & vbNewLine _
                                              & ",SEIQ_KYORI                       " & vbNewLine _
                                              & ",SEIQ_WT                          " & vbNewLine _
                                              & ",SEIQ_UNCHIN                      " & vbNewLine _
                                              & ",SEIQ_CITY_EXTC                   " & vbNewLine _
                                              & ",SEIQ_WINT_EXTC                   " & vbNewLine _
                                              & ",SEIQ_RELY_EXTC                   " & vbNewLine _
                                              & ",SEIQ_TOLL                        " & vbNewLine _
                                              & ",SEIQ_INSU                        " & vbNewLine _
                                              & ",SEIQ_FIXED_FLAG                  " & vbNewLine _
                                              & ",DECI_NG_NB                       " & vbNewLine _
                                              & ",DECI_KYORI                       " & vbNewLine _
                                              & ",DECI_WT                          " & vbNewLine _
                                              & ",DECI_UNCHIN                      " & vbNewLine _
                                              & ",DECI_CITY_EXTC                   " & vbNewLine _
                                              & ",DECI_WINT_EXTC                   " & vbNewLine _
                                              & ",DECI_RELY_EXTC                   " & vbNewLine _
                                              & ",DECI_TOLL                        " & vbNewLine _
                                              & ",DECI_INSU                        " & vbNewLine _
                                              & ",KANRI_UNCHIN                     " & vbNewLine _
                                              & ",KANRI_CITY_EXTC                  " & vbNewLine _
                                              & ",KANRI_WINT_EXTC                  " & vbNewLine _
                                              & ",KANRI_RELY_EXTC                  " & vbNewLine _
                                              & ",KANRI_TOLL                       " & vbNewLine _
                                              & ",KANRI_INSU                       " & vbNewLine _
                                              & ",REMARK                           " & vbNewLine _
                                              & ",SIZE_KB                          " & vbNewLine _
                                              & ",TAX_KB                           " & vbNewLine _
                                              & ",SAGYO_KANRI                      " & vbNewLine _
                                              & ",SYS_ENT_DATE                     " & vbNewLine _
                                              & ",SYS_ENT_TIME                     " & vbNewLine _
                                              & ",SYS_ENT_PGID                     " & vbNewLine _
                                              & ",SYS_ENT_USER                     " & vbNewLine _
                                              & ",SYS_UPD_DATE                     " & vbNewLine _
                                              & ",SYS_UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_PGID                     " & vbNewLine _
                                              & ",SYS_UPD_USER                     " & vbNewLine _
                                              & ",SYS_DEL_FLG                      " & vbNewLine _
                                              & " )VALUES(                         " & vbNewLine _
                                              & " @YUSO_BR_CD                      " & vbNewLine _
                                              & ",@NRS_BR_CD                       " & vbNewLine _
                                              & ",@UNSO_NO_L                       " & vbNewLine _
                                              & ",@UNSO_NO_M                       " & vbNewLine _
                                              & ",@CUST_CD_L                       " & vbNewLine _
                                              & ",@CUST_CD_M                       " & vbNewLine _
                                              & ",@CUST_CD_S                       " & vbNewLine _
                                              & ",@CUST_CD_SS                      " & vbNewLine _
                                              & ",@SEIQ_GROUP_NO                   " & vbNewLine _
                                              & ",@SEIQ_GROUP_NO_M                 " & vbNewLine _
                                              & ",@SEIQTO_CD                       " & vbNewLine _
                                              & ",@UNTIN_CALCULATION_KB            " & vbNewLine _
                                              & ",@SEIQ_SYARYO_KB                  " & vbNewLine _
                                              & ",@SEIQ_PKG_UT                     " & vbNewLine _
                                              & ",@SEIQ_NG_NB                      " & vbNewLine _
                                              & ",@SEIQ_DANGER_KB                  " & vbNewLine _
                                              & ",@SEIQ_TARIFF_BUNRUI_KB           " & vbNewLine _
                                              & ",@SEIQ_TARIFF_CD                  " & vbNewLine _
                                              & ",@SEIQ_ETARIFF_CD                 " & vbNewLine _
                                              & ",@SEIQ_KYORI                      " & vbNewLine _
                                              & ",@SEIQ_WT                         " & vbNewLine _
                                              & ",@SEIQ_UNCHIN                     " & vbNewLine _
                                              & ",@SEIQ_CITY_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_WINT_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_RELY_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_TOLL                       " & vbNewLine _
                                              & ",@SEIQ_INSU                       " & vbNewLine _
                                              & ",@SEIQ_FIXED_FLAG                 " & vbNewLine _
                                              & ",@DECI_NG_NB                      " & vbNewLine _
                                              & ",@DECI_KYORI                      " & vbNewLine _
                                              & ",@DECI_WT                         " & vbNewLine _
                                              & ",@DECI_UNCHIN                     " & vbNewLine _
                                              & ",@DECI_CITY_EXTC                  " & vbNewLine _
                                              & ",@DECI_WINT_EXTC                  " & vbNewLine _
                                              & ",@DECI_RELY_EXTC                  " & vbNewLine _
                                              & ",@DECI_TOLL                       " & vbNewLine _
                                              & ",@DECI_INSU                       " & vbNewLine _
                                              & ",@KANRI_UNCHIN                    " & vbNewLine _
                                              & ",@KANRI_CITY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_WINT_EXTC                 " & vbNewLine _
                                              & ",@KANRI_RELY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_TOLL                      " & vbNewLine _
                                              & ",@KANRI_INSU                      " & vbNewLine _
                                              & ",@REMARK                          " & vbNewLine _
                                              & ",@SIZE_KB                         " & vbNewLine _
                                              & ",@TAX_KB                          " & vbNewLine _
                                              & ",@SAGYO_KANRI                     " & vbNewLine _
                                              & ",@SYS_ENT_DATE                    " & vbNewLine _
                                              & ",@SYS_ENT_TIME                    " & vbNewLine _
                                              & ",@SYS_ENT_PGID                    " & vbNewLine _
                                              & ",@SYS_ENT_USER                    " & vbNewLine _
                                              & ",@SYS_UPD_DATE                    " & vbNewLine _
                                              & ",@SYS_UPD_TIME                    " & vbNewLine _
                                              & ",@SYS_UPD_PGID                    " & vbNewLine _
                                              & ",@SYS_UPD_USER                    " & vbNewLine _
                                              & ",@SYS_DEL_FLG                     " & vbNewLine _
                                              & ")                                 " & vbNewLine

#End Region

#Region "M_DEST(自動追加時使用)"
    ''' <summary>
    ''' 届先マスタINSERT文（M_DEST）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_M_DEST As String = "INSERT INTO $LM_MST$..M_DEST        " & vbNewLine _
                                       & "(                                   " & vbNewLine _
                                       & "      NRS_BR_CD                     " & vbNewLine _
                                       & "      ,CUST_CD_L                    " & vbNewLine _
                                       & "      ,DEST_CD                      " & vbNewLine _
                                       & "      ,EDI_CD                       " & vbNewLine _
                                       & "      ,DEST_NM                      " & vbNewLine _
                                       & "      ,ZIP                          " & vbNewLine _
                                       & "      ,AD_1                         " & vbNewLine _
                                       & "      ,AD_2                         " & vbNewLine _
                                       & "      ,AD_3                         " & vbNewLine _
                                       & "      ,CUST_DEST_CD                 " & vbNewLine _
                                       & "      ,SALES_CD                     " & vbNewLine _
                                       & "      ,SP_NHS_KB                    " & vbNewLine _
                                       & "      ,COA_YN                       " & vbNewLine _
                                       & "      ,SP_UNSO_CD                   " & vbNewLine _
                                       & "      ,SP_UNSO_BR_CD                " & vbNewLine _
                                       & "      ,DELI_ATT                     " & vbNewLine _
                                       & "      ,CARGO_TIME_LIMIT             " & vbNewLine _
                                       & "      ,LARGE_CAR_YN                 " & vbNewLine _
                                       & "      ,TEL                          " & vbNewLine _
                                       & "      ,FAX                          " & vbNewLine _
                                       & "      ,UNCHIN_SEIQTO_CD             " & vbNewLine _
                                       & "      ,JIS                          " & vbNewLine _
                                       & "      ,KYORI                        " & vbNewLine _
                                       & "      ,PICK_KB                      " & vbNewLine _
                                       & "      ,BIN_KB                       " & vbNewLine _
                                       & "      ,MOTO_CHAKU_KB                " & vbNewLine _
                                       & "      ,URIAGE_CD                    " & vbNewLine _
                                       & "      ,SHIHARAI_AD                  " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                 " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                 " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                 " & vbNewLine _
                                       & "      ,SYS_ENT_USER                 " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                 " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                 " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                 " & vbNewLine _
                                       & "      ,SYS_UPD_USER                 " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                  " & vbNewLine _
                                       & "      ) VALUES (                    " & vbNewLine _
                                       & "      @NRS_BR_CD                    " & vbNewLine _
                                       & "      ,@CUST_CD_L                   " & vbNewLine _
                                       & "      ,@DEST_CD                     " & vbNewLine _
                                       & "      ,@EDI_CD                      " & vbNewLine _
                                       & "      ,@DEST_NM                     " & vbNewLine _
                                       & "      ,@ZIP                         " & vbNewLine _
                                       & "      ,@AD_1                        " & vbNewLine _
                                       & "      ,@AD_2                        " & vbNewLine _
                                       & "      ,@AD_3                        " & vbNewLine _
                                       & "      ,@CUST_DEST_CD                " & vbNewLine _
                                       & "      ,@SALES_CD                    " & vbNewLine _
                                       & "      ,@SP_NHS_KB                   " & vbNewLine _
                                       & "      ,@COA_YN                      " & vbNewLine _
                                       & "      ,@SP_UNSO_CD                  " & vbNewLine _
                                       & "      ,@SP_UNSO_BR_CD               " & vbNewLine _
                                       & "      ,@DELI_ATT                    " & vbNewLine _
                                       & "      ,@CARGO_TIME_LIMIT            " & vbNewLine _
                                       & "      ,@LARGE_CAR_YN                " & vbNewLine _
                                       & "      ,@TEL                         " & vbNewLine _
                                       & "      ,@FAX                         " & vbNewLine _
                                       & "      ,@UNCHIN_SEIQTO_CD            " & vbNewLine _
                                       & "      ,@JIS                         " & vbNewLine _
                                       & "      ,@KYORI                       " & vbNewLine _
                                       & "      ,@PICK_KB                     " & vbNewLine _
                                       & "      ,@BIN_KB                      " & vbNewLine _
                                       & "      ,@MOTO_CHAKU_KB               " & vbNewLine _
                                       & "      ,@URIAGE_CD                   " & vbNewLine _
                                       & "--'(2012.10.02)START UMANO 要望番号1485 支払運賃に伴う修正" & vbNewLine _
                                       & "      ,@AD_1 + @AD_2 + @AD_3        " & vbNewLine _
                                       & "--'(2012.10.02)END   UMANO 要望番号1485 支払運賃に伴う修正" & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                 " & vbNewLine _
                                       & ")                                   " & vbNewLine

#End Region

#End Region
#End Region

#End Region

#Region "Field"
    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder

    ''' <summary>
    ''' ORDER BY句作成
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSqlOrderBy As StringBuilder

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region

#Region "Method(EDI取込処理系)"
#Region "Select"
#Region "商品マスタ"
    ''' <summary>
    ''' 商品情報読込(商品マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMstGoods(ByVal ds As DataSet) As DataSet

        Me._Row = ds.Tables("LMH030_M_GOODS").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC405.SQL_SELECT_M_GOODS)

        Me._SqlPrmList = New ArrayList()

        'SQL条件設定(Where)
        Me._StrSql.Append(LMH030DAC405.SQL_WHERE1_M_GOODS)

        'ADD 2016/07/20 フィルメ対応
        If ds.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='0L'").Count > 0 Then
            'SQL条件設定(Where + 入り目を追加)
            Me._StrSql.Append(LMH030DAC405.SQL_WHERE1_M_GOODS_FLM)

        End If

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC405.SQL_GROUP_BY_M_GOODS)

        'パラメータ設定
        Call Me.SetSelectMstGoodsParameter(Me._Row)

        'ADD 2016/07/20 フィルメ対応
        If ds.Tables("LMH030_M_CUST_DETAILS").Select("SUB_KB='0L'").Count > 0 Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME_NB", Me.NullConvertString(Me._Row.Item("STD_IRIME_NB")), DBDataType.NUMERIC))
        End If

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "SelectDataMgoods", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''商品件数の設定
        Dim goodsCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            Call Me.SetGoodsMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_GOODS")

            '処理件数の設定
            goodsCnt = ds.Tables("LMH030_M_GOODS").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(goodsCnt)
        Return ds

    End Function

    ''' <summary>
    ''' 商品マスタマッピング
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="map"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetGoodsMap(ByVal reader As SqlDataReader, ByVal map As Hashtable) As Hashtable

        '取得データの格納先をマッピング
        map.Add("MST_CNT", "MST_CNT")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("CUST_COST_CD1", "CUST_COST_CD1")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("GOODS_NM_2", "GOODS_NM_2")
        map.Add("GOODS_NM_3", "GOODS_NM_3")
        map.Add("UP_GP_CD_1", "UP_GP_CD_1")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("KIKEN_KB", "KIKEN_KB")
        map.Add("UN", "UN")
        map.Add("PG_KB", "PG_KB")
        map.Add("CLASS_1", "CLASS_1")
        map.Add("CLASS_2", "CLASS_2")
        map.Add("CLASS_3", "CLASS_3")
        map.Add("CHEM_MTRL_KB", "CHEM_MTRL_KB")
        map.Add("DOKU_KB", "DOKU_KB")
        map.Add("GAS_KANRI_KB", "GAS_KANRI_KB")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("ONDO_MX", "ONDO_MX")
        map.Add("ONDO_MM", "ONDO_MM")
        map.Add("ONDO_STR_DATE", "ONDO_STR_DATE")
        map.Add("ONDO_END_DATE", "ONDO_END_DATE")
        map.Add("ONDO_UNSO_STR_DATE", "ONDO_UNSO_STR_DATE")
        map.Add("ONDO_UNSO_END_DATE", "ONDO_UNSO_END_DATE")
        map.Add("KYOKAI_GOODS_KB", "KYOKAI_GOODS_KB")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("NB_UT", "NB_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("PLT_PER_PKG_UT", "PLT_PER_PKG_UT")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("STD_CBM", "STD_CBM")
        map.Add("INKA_KAKO_SAGYO_KB_1", "INKA_KAKO_SAGYO_KB_1")
        map.Add("INKA_KAKO_SAGYO_KB_2", "INKA_KAKO_SAGYO_KB_2")
        map.Add("INKA_KAKO_SAGYO_KB_3", "INKA_KAKO_SAGYO_KB_3")
        map.Add("INKA_KAKO_SAGYO_KB_4", "INKA_KAKO_SAGYO_KB_4")
        map.Add("INKA_KAKO_SAGYO_KB_5", "INKA_KAKO_SAGYO_KB_5")
        map.Add("OUTKA_KAKO_SAGYO_KB_1", "OUTKA_KAKO_SAGYO_KB_1")
        map.Add("OUTKA_KAKO_SAGYO_KB_2", "OUTKA_KAKO_SAGYO_KB_2")
        map.Add("OUTKA_KAKO_SAGYO_KB_3", "OUTKA_KAKO_SAGYO_KB_3")
        map.Add("OUTKA_KAKO_SAGYO_KB_4", "OUTKA_KAKO_SAGYO_KB_4")
        map.Add("OUTKA_KAKO_SAGYO_KB_5", "OUTKA_KAKO_SAGYO_KB_5")
        map.Add("PKG_SAGYO", "PKG_SAGYO")
        map.Add("TARE_YN", "TARE_YN")
        map.Add("SP_NHS_YN", "SP_NHS_YN")
        map.Add("COA_YN", "COA_YN")
        map.Add("LOT_CTL_KB", "LOT_CTL_KB")
        map.Add("LT_DATE_CTL_KB", "LT_DATE_CTL_KB")
        map.Add("CRT_DATE_CTL_KB", "CRT_DATE_CTL_KB")
        map.Add("DEF_SPD_KB", "DEF_SPD_KB")
        map.Add("KITAKU_AM_UT_KB", "KITAKU_AM_UT_KB")
        map.Add("KITAKU_GOODS_UP", "KITAKU_GOODS_UP")
        map.Add("ORDER_KB", "ORDER_KB")
        map.Add("ORDER_NB", "ORDER_NB")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("SKYU_MEI_YN", "SKYU_MEI_YN")
        map.Add("HIKIATE_ALERT_YN", "HIKIATE_ALERT_YN")
        map.Add("OUTKA_ATT", "OUTKA_ATT")
        map.Add("PRINT_NB", "PRINT_NB")
        map.Add("CONSUME_PERIOD_DATE", "CONSUME_PERIOD_DATE")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        Return map

    End Function

#End Region

#Region "荷主明細マスタ（SUB_KB=87）"
    ''' <summary>
    ''' 件数取得処理(荷主明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMstCustD87(ByVal ds As DataSet) As DataSet

        Me._Row = ds.Tables("LMH030_M_GOODS").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC405.SELECT_M_CUST_DETL)

        Me._SqlPrmList = New ArrayList()

        'SQL条件設定(Where)
        Me._StrSql.Append(LMH030DAC405.SQL_WHERE1_M_CUST_DETAILS)

        'パラメータ設定
        Call Me.SetSelectMstCustDetailParameter(Me._Row)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectMstCustD87", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function
#End Region


#Region "荷主明細マスタ（SUB_KB=0L）"
    ''' <summary>
    ''' 件数取得処理(荷主明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMstCustD0L(ByVal ds As DataSet) As DataSet

        Me._Row = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC405.SELECT_M_CUST_DETL)

        Me._SqlPrmList = New ArrayList()

        'SQL条件設定(Where)
        Me._StrSql.Append(LMH030DAC405.SQL_WHERE1_M_CUST_DETAILS_FIR)

        'パラメータ設定
        Call Me.SetSelectMstCustDetailParameter(Me._Row)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectMstCustD0L", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function
#End Region

#Region "届先マスタ"
    ''' <summary>
    ''' 届先マスタ情報読込(届先マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMstDest(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_M_DEST")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMH030DAC405.SQL_SELECT_M_DEST)

        If dt.Rows(0)("DEST_CD").ToString() <> String.Empty Then
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND M_DEST.DEST_CD = @DEST_CD")
        End If

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMdestParameter(dt, 1)

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC405.SQL_GROUP_BY_M_DEST)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "SelectDataMdestOutkaToroku", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''届先件数の設定
        Dim destCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            Call Me.SetDestMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_DEST")

            '処理件数の設定
            destCnt = ds.Tables("LMH030_M_DEST").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(destCnt)
        Return ds
    End Function

    ''' <summary>
    ''' 届先マスタをHashTableに設定
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="map"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDestMap(ByVal reader As SqlDataReader, ByVal map As Hashtable) As Hashtable

        '取得データの格納先をマッピング
        map.Add("MST_CNT", "MST_CNT")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("EDI_CD", "EDI_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("CUST_DEST_CD", "CUST_DEST_CD")
        map.Add("SALES_CD", "SALES_CD")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("COA_YN", "COA_YN")
        map.Add("SP_UNSO_CD", "SP_UNSO_CD")
        map.Add("SP_UNSO_BR_CD", "SP_UNSO_BR_CD")
        map.Add("DELI_ATT", "DELI_ATT")
        map.Add("CARGO_TIME_LIMIT", "CARGO_TIME_LIMIT")
        map.Add("LARGE_CAR_YN", "LARGE_CAR_YN")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("UNCHIN_SEIQTO_CD", "UNCHIN_SEIQTO_CD")
        map.Add("JIS", "JIS")
        map.Add("KYORI", "KYORI")
        map.Add("PICK_KB", "PICK_KB")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("MOTO_CHAKU_KB", "MOTO_CHAKU_KB")
        map.Add("URIAGE_CD", "URIAGE_CD")
        map.Add("REMARK", "REMARK")                 '2015.05.13 追加START 
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        Return map

    End Function



    ''' <summary>
    ''' 件数取得処理(届先マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectLikeDataMdest(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMH030DAC405.SQL_SELECT_M_DEST)

        If dt.Rows(0)("DEST_CD").ToString() = String.Empty Then
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND M_DEST.EDI_CD like @EDI_DEST_CD")
        Else
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND M_DEST.DEST_CD like @DEST_CD")
        End If

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMdestLikeParameter(dt)

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC405.SQL_GROUP_BY_M_DEST)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMdest", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''届先件数の設定
        Dim destCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            Call Me.SetDestMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_DEST")

            '処理件数の設定
            destCnt = ds.Tables("LMH030_M_DEST").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(destCnt)
        Return ds
    End Function

#End Region

    '2015.05.14 引当単位区分の抽出 追加START
#Region "区分マスタ"

    ''' <summary>
    ''' 件数取得処理(区分マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataZkbn(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC405.SQL_SELECT_Z_KBN)

        Dim dt As DataTable = ds.Tables("LMH030_JUDGE")
        Dim drSemiEdiInfo As DataTable = ds.Tables("LMH030_SEMIEDI_INFO")

        '2015.05.15 テルモ要望対応　追加START
        If drSemiEdiInfo.Rows.Count = 0 Then
            drSemiEdiInfo = ds.Tables("LMH030_OUTKAEDI_L")
        End If
        '2015.05.15 テルモ要望対応　追加END

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetKbnParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), drSemiEdiInfo.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "SelectDataZkbn", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''届先件数の設定
        Dim nisCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            '取得データの格納先をマッピング
            map.Add("NISUGATA", "NISUGATA")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_Z_KBN")

            '処理件数の設定
            nisCnt = ds.Tables("LMH030_Z_KBN").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(nisCnt)
        Return ds

    End Function

#End Region
    '2015.05.14 引当単位区分の抽出 追加END

#Region "明細数取得"
    ''' <summary>
    ''' 明細数取得
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetMeisaiCount(ByVal sNRS_BR_CD As String, ByVal sEDI_CTL_NO As String) As Integer

        Dim sSQL As String = SQL_GET_MEISAI_COUNT

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(sSQL, sNRS_BR_CD))

        Dim rtn As Integer = 0

        'SQLパラメータ初期化
        _SqlPrmList = New ArrayList

        'パラメータ設定
        _SqlPrmList.Add(GetSqlParameter("@NRS_BR_CD", sNRS_BR_CD, DBDataType.CHAR))
        _SqlPrmList.Add(GetSqlParameter("@EDI_CTL_NO", sEDI_CTL_NO, DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("GetMeisaiCount", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得内容の設定
        reader.Read()
        rtn = Convert.ToInt32(reader(0))
        reader.Close()

        Return rtn

    End Function

#End Region

#Region "キャンセルデータ抽出"
    Private Function SelectCancelData(ByVal ds As DataSet) As DataSet
        Me._Row = ds.Tables("LMH030INOUT").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        If ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("DTL_DATACHECK_FLG").ToString().Equals(LMConst.FLG.ON) = True Then
            'SQL作成(SELECT)
            Me._StrSql.Append(LMH030DAC405.SQL_SELECT_CANCER_DATA_DTL)
            'SQL作成(WHERE)
            Me._StrSql.Append(LMH030DAC405.SQL_WHERE_CANCER_DATA_DTL)

            'オーダー番号＋オーダー番号明細チェックの場合
        ElseIf ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("DTL_DATACHECK_FLG").ToString().Equals("3") = True Then
            'SQL作成(SELECT)
            Me._StrSql.Append(LMH030DAC405.SQL_SELECT_CANCER_DATA_DTL_CNT)
            'SQL作成(WHERE)
            Me._StrSql.Append(LMH030DAC405.SQL_WHERE_CANCER_DATA_DTL_CNT)

            'オーダー番号明細のみチェックの場合
        ElseIf ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("DTL_DATACHECK_FLG").ToString().Equals("4") = True Then
            'SQL作成(SELECT)
            Me._StrSql.Append(LMH030DAC405.SQL_SELECT_CANCER_DATA_DTL_CNT)
            'SQL作成(WHERE)
            Me._StrSql.Append(LMH030DAC405.SQL_WHERE_CANCER_DATA_DTL_ONLY_CNT)

            '2015.04.28 Ｍ・Ｒ・Ｃデュポン対応 FREE_C02の(オーダー番号明細のみ)チェックの場合 追加START
        ElseIf ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("DTL_DATACHECK_FLG").ToString().Equals("5") = True Then
            'SQL作成(SELECT)
            Me._StrSql.Append(LMH030DAC405.SQL_SELECT_CANCER_DATA_DTL)
            'SQL作成(WHERE)
            Me._StrSql.Append(LMH030DAC405.SQL_WHERE_CANCER_DATA_DTL_M_FREE_C25)
            '2015.04.28 Ｍ・Ｒ・Ｃデュポン対応 M_FREE_C25_NOの(オーダー番号明細のみ)チェックの場合 追加END
        Else
            'SQL作成(SELECT)
            Me._StrSql.Append(LMH030DAC405.SQL_SELECT_CANCER_DATA)
            'SQL作成(WHERE)
            Me._StrSql.Append(LMH030DAC405.SQL_WHERE_CANCER_DATA)
        End If

        'パラメタ設定
        Me._SqlPrmList = New ArrayList()

        'パラム置換
        Me.SetSelecCancelDataParameter(Me._Row, ds)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "SelectCancelData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        Dim Cnt As Integer = 0
        If reader.HasRows() = True Then

            Call Me.SetCancelMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030OUT")

            '処理件数の設定
            Cnt = ds.Tables("LMH030OUT").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(Cnt)

        Return ds
    End Function

    ''' <summary>
    ''' キャンセルデータ(キャンセル時に出荷管理番号と荷主オーダー番号を返す)をHashTableに設定
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="map"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCancelMap(ByVal reader As SqlDataReader, ByVal map As Hashtable) As Hashtable

        '取得データの格納先をマッピング
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")

        Return map

    End Function

#End Region

#Region "受信テーブル同一データ抽出"

    Private Function SelectCompareData(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成(SELECT)
        Dim sql As String = String.Empty
        If ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("RCV_NM_DTL").ToString().Equals("H_OUTKAEDI_DTL_ARB") = True Then
            'スキーマ設定
            sql = Me.SetSchemaNm(LMH030DAC405.SQL_SELECT_COMPARE_CHECK_ARB, ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("NRS_BR_CD").ToString())
            '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 START
        ElseIf ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("RCV_NM_DTL").ToString().Equals("H_OUTKAEDI_DTL_MRC") = True Then
            'スキーマ設定
            sql = Me.SetSchemaNm(LMH030DAC405.SQL_SELECT_COMPARE_CHECK_MRC, ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("NRS_BR_CD").ToString())
            '2015.03.23 千葉・Ｍ・Ｒ・Ｃデュポン対応 END
        End If

        'パラメタ設定
        Me._SqlPrmList = New ArrayList()

        'パラメータ置換
        Me.SetCompareDataParameter(ds)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "SelectCompareData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()

        If reader.HasRows() = True Then
            MyBase.SetResultCount(Convert.ToInt32(reader("CNT")))
        Else
            MyBase.SetResultCount(0)
        End If

        reader.Close()

        Return ds

    End Function

#End Region

#Region "荷主明細マスタ（SUB_KB検索条件に含まない）"
    ''' <summary>
    ''' 全件取得処理(荷主明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMstCustAll(ByVal ds As DataSet) As DataSet

        Me._Row = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC405.SELECT_M_CUST_DETL_ALL)

        Me._SqlPrmList = New ArrayList()

        'SQL条件設定(Where)
        Me._StrSql.Append(LMH030DAC405.SQL_WHERE_M_CUST_DETAILS_ALL)

        'パラメータ設定
        Call Me.SetSelectMstCustDetailParameter(Me._Row)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectMstCustAll", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            Call Me.SetMstCust(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_CUST_DETAILS")

        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 荷主明細マスタ(全件)
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="map"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetMstCust(ByVal reader As SqlDataReader, ByVal map As Hashtable) As Hashtable

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("CUST_CD_EDA", "CUST_CD_EDA")
        map.Add("CUST_CLASS", "CUST_CLASS")
        map.Add("SUB_KB", "SUB_KB")
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("SET_NAIYO_2", "SET_NAIYO_2")
        map.Add("SET_NAIYO_3", "SET_NAIYO_3")
        map.Add("REMARK", "REMARK")

        Return map

    End Function


#End Region


#End Region

#Region "Update"
#Region "H_OUTKAEDI_L"
    ''' <summary>
    ''' EDI出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaEdiLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録、紐付けSQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA, LMH030DAC.EventShubetsu.HIMODUKE
                setSql = LMH030DAC405.SQL_UPDATE_OUTKASAVEEDI_L

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータ設定
        Call Me.SetOutkaEdiLComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(inTbl.Rows(0), Me._SqlPrmList)
        Call Me.SetJissekiParameterEdiLM(inTbl.Rows(0), dtEventShubetsu)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "UpdateOutkaEdiLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' EDI出荷(中)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaEdiMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediMTbl As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        Dim loopflg As Integer = 0

        Dim rtn As Integer = 0

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録、紐付けSQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA, LMH030DAC.EventShubetsu.HIMODUKE
                setSql = LMH030DAC405.SQL_UPDATE_OUTKAEDI_M
                loopflg = 1

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = ediMTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = ediMTbl.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter(Me._SqlPrmList)
            Call Me.SetJissekiParameterEdiLM(ediMTbl.Rows(i), dtEventShubetsu)
            Call Me.SetOutkaEdiMComParameter(Me._Row, Me._SqlPrmList)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))


            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC405", "UpdateOutkaEdiMData", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

            '処理回数の判定
            If loopflg = 0 Then
                Exit For
            End If

        Next

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_L(セミEDI：取消更新)"
    ''' <summary>
    ''' EDI出荷(大)テーブル更新(取消更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)テーブル取消更新SQLの構築・発行</remarks>
    Private Function UpdateDelOutkaEdiL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim tbOutkaEdiRcvIn As DataTable = ds.Tables("LMH030INOUT")
        Dim sSql As String = String.Empty

        'INTableの条件rowの格納
        Me._Row = tbOutkaEdiRcvIn.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '出荷EDI CONST名
        sSql = LMH030DAC405.SQL_UPDATE_DEL_OUTKAIEDI_L

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sSql, Me._Row.Item("NRS_BR_CD").ToString()))


        'パラメータ設定
        Call Me.SetUpdateDelOutkaEdiParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)


        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "UpdateDelOutkaEdiL", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_M(セミEDI：取消更新)"
    ''' <summary>
    ''' EDI出荷(中)テーブル更新(セミEDI：取消更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateDelOutkaEdiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim tbOutkaEdiRcvIn As DataTable = ds.Tables("LMH030INOUT")
        Dim sSql As String = String.Empty

        Dim rtn As Integer = 0

        'EDI出荷SQL
        sSql = LMH030DAC405.SQL_UPDATE_DEL_OUTKAIEDI_M

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sSql, Me._Row.Item("NRS_BR_CD").ToString()))
        Dim max As Integer = tbOutkaEdiRcvIn.Rows.Count() - 1


        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = tbOutkaEdiRcvIn.Rows(0)

        'パラメータ設定
        Call Me.SetUpdateDelOutkaEdiParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "UpdateDelOutkaEdiM", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_DTL_XXX(セミEDI：受信テーブル取消更新)"
    ''' <summary>
    ''' EDI受信(DTL)テーブル更新(セミEDI：受信テーブル取消更新)※各荷主レイアウトが異なる
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信(DTL)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateDelRcvDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim tbOutkaEdiRcvIn As DataTable = ds.Tables("LMH030INOUT")

        Dim rtn As Integer = 0

        'EDI出荷SQL

        'スキーマ名設定
        If String.IsNullOrEmpty(ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("RCV_NM_DTL").ToString()) = True Then
            Return ds
            Exit Function
        End If

        Dim sql As String = Me.SetSchemaNm(LMH030DAC405.SQL_UPDATE_DEL_OUTKAIEDI_DTL, ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("NRS_BR_CD").ToString())
        sql = sql.Replace("$RCV_NM$", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("RCV_NM_DTL").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))
        'Dim max As Integer = tbOutkaEdiRcvIn.Rows.Count() - 1


        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = tbOutkaEdiRcvIn.Rows(0)

        'パラメータ設定
        Call Me.SetUpdateDelOutkaEdiParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "UpdateDelRcvDtl", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return ds

    End Function

#End Region

#End Region

#Region "Insert"
#Region "H_OUTKAEDI_L(セミEDI)"
    ''' <summary>
    ''' EDI出荷(大)テーブル新規登録(セミEDI)
    ''' </summary>
    ''' <param name="setDs">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertOutkaEdiL(ByVal setDs As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtOutkaEdiL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = dtOutkaEdiL.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC405.SQL_INSERT_OUTKAEDI_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetInsertOutkaEdiLParameter(Me._Row, Me._SqlPrmList, LMH030DAC.EventShubetsu.TORIKOMI)
        Call Me.SetDataInsertParameterEdi(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "InsertOutkaEdiL", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        MyBase.SetResultCount(rtn)

        Return setDs
        'Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_M(セミEDI)"
    ''' <summary>
    ''' EDI出荷(中)テーブル新規登録
    ''' </summary>
    ''' <param name="setDs">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function InsertOutkaEdiM(ByVal setDs As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtOurkaEdiM As DataTable = setDs.Tables("LMH030_OUTKAEDI_M")

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC405.SQL_INSERT_OUTKAEDI_M _
                                                                       , setDs.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("NRS_BR_CD").ToString()))
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dtOurkaEdiM.Rows(0)

        'パラメータ設定
        Call Me.SetInsertOutkaEdiMParameter(Me._Row, Me._SqlPrmList, LMH030DAC.EventShubetsu.TORIKOMI)
        Call Me.SetDataInsertParameterEdi(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "InsertOutkaEdiM", cmd)


        'SQLの発行
        'MyBase.GetUpdateResult(cmd)
        MyBase.GetInsertResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return setDs

    End Function

#End Region

#Region "H_OUTKAEDI_L(セミEDI)同一データ整合性差異有りの場合の更新"
    ''' <summary>
    ''' EDI出荷(大)テーブル更新処理(セミEDI)
    ''' </summary>
    ''' <param name="setDs">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateEditOutkaEdiL(ByVal setDs As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtOutkaEdiL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = dtOutkaEdiL.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC405.SQL_UPDATE_EDIT_OUTKAEDI_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetInsertOutkaEdiLParameter(Me._Row, Me._SqlPrmList, LMH030DAC.EventShubetsu.TORIKOMI)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "UpdateEditOutkaEdiL", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(rtn)

        Return setDs
        'Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_M(セミEDI)同一データ整合性差異有りの場合の更新"
    ''' <summary>
    ''' EDI出荷(中)テーブル更新処理(セミEDI)
    ''' </summary>
    ''' <param name="setDs">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateEditOutkaEdiM(ByVal setDs As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtOutkaEdiL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = dtOutkaEdiL.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC405.SQL_UPDATE_EDIT_OUTKAEDI_M, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetInsertOutkaEdiLParameter(Me._Row, Me._SqlPrmList, LMH030DAC.EventShubetsu.TORIKOMI)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "UpdateEditOutkaEdiM", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return setDs
        'Return ds

    End Function

#End Region

#End Region

#End Region

#Region "Method(出荷登録処理系)"
#Region "Select"
#Region "出荷登録処理用EDIデータ 抽出処理"
    ''' <summary>
    ''' EDI出荷データL取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectEdiL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC405.L_DEF_SQL_SELECT_DATA)      'SQL構築
        Call Me.setSQLSelectDataExists()                           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "SelectEdiL", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_KB", "OUTKA_KB")
        map.Add("SYUBETU_KB", "SYUBETU_KB")
        map.Add("NAIGAI_KB", "NAIGAI_KB")
        map.Add("OUTKA_STATE_KB", "OUTKA_STATE_KB")
        map.Add("OUTKAHOKOKU_YN", "OUTKAHOKOKU_YN")
        map.Add("PICK_KB", "PICK_KB")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("HOKOKU_DATE", "HOKOKU_DATE")
        map.Add("TOUKI_HOKAN_YN", "TOUKI_HOKAN_YN")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("SHIP_CD_M", "SHIP_CD_M")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("SHIP_NM_M", "SHIP_NM_M")
        map.Add("EDI_DEST_CD", "EDI_DEST_CD")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_AD_4", "DEST_AD_4")
        map.Add("DEST_AD_5", "DEST_AD_5")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_FAX", "DEST_FAX")
        map.Add("DEST_MAIL", "DEST_MAIL")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("COA_YN", "COA_YN")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("UNSO_MOTO_KB", "UNSO_MOTO_KB")
        map.Add("UNSO_TEHAI_KB", "UNSO_TEHAI_KB")
        map.Add("SYARYO_KB", "SYARYO_KB")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("REMARK", "REMARK")
        map.Add("UNSO_ATT", "UNSO_ATT")
        map.Add("DENP_YN", "DENP_YN")
        map.Add("PC_KB", "PC_KB")
        map.Add("UNCHIN_YN", "UNCHIN_YN")
        map.Add("NIYAKU_YN", "NIYAKU_YN")
        map.Add("OUT_FLAG", "OUT_FLAG")
        map.Add("AKAKURO_KB", "AKAKURO_KB")
        map.Add("JISSEKI_FLAG", "JISSEKI_FLAG")
        map.Add("JISSEKI_USER", "JISSEKI_USER")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("JISSEKI_TIME", "JISSEKI_TIME")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("FREE_N06", "FREE_N06")
        map.Add("FREE_N07", "FREE_N07")
        map.Add("FREE_N08", "FREE_N08")
        map.Add("FREE_N09", "FREE_N09")
        map.Add("FREE_N10", "FREE_N10")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")
        map.Add("FREE_C06", "FREE_C06")
        map.Add("FREE_C07", "FREE_C07")
        map.Add("FREE_C08", "FREE_C08")
        map.Add("FREE_C09", "FREE_C09")
        map.Add("FREE_C10", "FREE_C10")
        map.Add("FREE_C11", "FREE_C11")
        map.Add("FREE_C12", "FREE_C12")
        map.Add("FREE_C13", "FREE_C13")
        map.Add("FREE_C14", "FREE_C14")
        map.Add("FREE_C15", "FREE_C15")
        map.Add("FREE_C16", "FREE_C16")
        map.Add("FREE_C17", "FREE_C17")
        map.Add("FREE_C18", "FREE_C18")
        map.Add("FREE_C19", "FREE_C19")
        map.Add("FREE_C20", "FREE_C20")
        map.Add("FREE_C21", "FREE_C21")
        map.Add("FREE_C22", "FREE_C22")
        map.Add("FREE_C23", "FREE_C23")
        map.Add("FREE_C24", "FREE_C24")
        map.Add("FREE_C25", "FREE_C25")
        map.Add("FREE_C26", "FREE_C26")
        map.Add("FREE_C27", "FREE_C27")
        map.Add("FREE_C28", "FREE_C28")
        map.Add("FREE_C29", "FREE_C29")
        map.Add("FREE_C30", "FREE_C30")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
        map.Add("EDIT_FLAG", "EDIT_FLAG")
        map.Add("MATCHING_FLAG", "MATCHING_FLAG")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_OUTKAEDI_L")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030_OUTKAEDI_L").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' EDI出荷データM取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷データMテーブル更新対象データ結果取得SQLの構築・発行</remarks>
    Private Function SelectEdiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC405.M_DEF_SQL_SELECT_DATA)      'SQL構築(データ抽出用)

        Call Me.setSQLSelectDataExists()                           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "SelectEdiM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_CTL_NO_CHU", "OUTKA_CTL_NO_CHU")
        map.Add("COA_YN", "COA_YN")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("NRS_GOODS_CD", "NRS_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKA_HASU", "OUTKA_HASU")
        map.Add("OUTKA_QT", "OUTKA_QT")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("OUTKA_TTL_QT", "OUTKA_TTL_QT")
        map.Add("KB_UT", "KB_UT")
        map.Add("QT_UT", "QT_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("REMARK", "REMARK")
        map.Add("OUT_KB", "OUT_KB")
        map.Add("AKAKURO_KB", "AKAKURO_KB")
        map.Add("JISSEKI_FLAG", "JISSEKI_FLAG")
        map.Add("JISSEKI_USER", "JISSEKI_USER")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("JISSEKI_TIME", "JISSEKI_TIME")
        map.Add("SET_KB", "SET_KB")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("FREE_N06", "FREE_N06")
        map.Add("FREE_N07", "FREE_N07")
        map.Add("FREE_N08", "FREE_N08")
        map.Add("FREE_N09", "FREE_N09")
        map.Add("FREE_N10", "FREE_N10")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")
        map.Add("FREE_C06", "FREE_C06")
        map.Add("FREE_C07", "FREE_C07")
        map.Add("FREE_C08", "FREE_C08")
        map.Add("FREE_C09", "FREE_C09")
        map.Add("FREE_C10", "FREE_C10")
        map.Add("FREE_C11", "FREE_C11")
        map.Add("FREE_C12", "FREE_C12")
        map.Add("FREE_C13", "FREE_C13")
        map.Add("FREE_C14", "FREE_C14")
        map.Add("FREE_C15", "FREE_C15")
        map.Add("FREE_C16", "FREE_C16")
        map.Add("FREE_C17", "FREE_C17")
        map.Add("FREE_C18", "FREE_C18")
        map.Add("FREE_C19", "FREE_C19")
        map.Add("FREE_C20", "FREE_C20")
        map.Add("FREE_C21", "FREE_C21")
        map.Add("FREE_C22", "FREE_C22")
        map.Add("FREE_C23", "FREE_C23")
        map.Add("FREE_C24", "FREE_C24")
        map.Add("FREE_C25", "FREE_C25")
        map.Add("FREE_C26", "FREE_C26")
        map.Add("FREE_C27", "FREE_C27")
        map.Add("FREE_C28", "FREE_C28")
        map.Add("FREE_C29", "FREE_C29")
        map.Add("FREE_C30", "FREE_C30")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
        map.Add("SCM_CTL_NO_M", "SCM_CTL_NO_M")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SAGYO_KB_1", "SAGYO_KB_1")
        map.Add("SAGYO_KB_2", "SAGYO_KB_2")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_OUTKAEDI_M")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030_OUTKAEDI_M").Rows.Count())
        reader.Close()

        Return ds

    End Function

#End Region

#End Region

#Region "Update"
#Region "C_OUTKA_L"
    ''' <summary>
    ''' 出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_C_OUTKA_L")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録(まとめ時)
            Case LMH030DAC.EventShubetsu.SAVEOUTKA
                setSql = LMH030DAC405.SQL_UPDATE_OUTKAL_MATOME

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetOutkaLComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(inTbl.Rows(0), Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "UpdateOutkaLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function


    ''' <summary>
    ''' 出荷(大)テーブル更新(注文番号更新付)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaLDataWithBuyerOrdNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_C_OUTKA_L")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録(まとめ時)
            Case LMH030DAC.EventShubetsu.SAVEOUTKA
                setSql = LMH030DAC405.SQL_UPDATE_OUTKAL_MATOME_WITH_BUYER_ORD_NO

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetOutkaLComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(inTbl.Rows(0), Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "UpdateOutkaLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function


#End Region

#Region "まとめ先EDI出荷(大)"
    ''' <summary>
    ''' EDI出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateMatomesakiEdiLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録SQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA
                setSql = LMH030DAC405.SQL_UPDATE_MATOMESAKI_OUTKAEDI_L

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, inTbl.Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetOutkaEdiLMatomeParameter(inTbl.Rows(0), Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "UpdateMatomesakiEdiLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "まとめ先運送(大)"
    ''' <summary>
    ''' 運送(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateMatomesakiUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_UNSO_L")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録SQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA
                setSql = LMH030DAC405.SQL_UPDATE_MATOMESAKI_UNSO_L

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUnsoLComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "UpdateMatomesakiUnsoLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "M_DEST(自動更新用)"
    ''' <summary>
    ''' 届先マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateMDestData(ByVal ds As DataSet) As DataSet

        Dim inTbl As DataTable = ds.Tables("LMH030_M_DEST")
        Dim setSql As String = String.Empty
        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'SQL文のコンパイル
        setSql = SQL_UPDATE_M_DEST

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMdestParameter(inTbl, 1)
        Call Me.SetMdestUpdateParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "UpdateMDestData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "Insert"
#Region "C_OUTKA_L"
    ''' <summary>
    ''' 出荷(大)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertOutkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim outkaLTbl As DataTable = ds.Tables("LMH030_C_OUTKA_L")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        'INTableの条件rowの格納
        Me._Row = outkaLTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC405.SQL_INSERT_OUTKA_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetOutkaLComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "InsertOutkaLData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "C_OUTKA_M"
    ''' <summary>
    ''' 出荷(中)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function InsertOutkaMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim outkaMTbl As DataTable = ds.Tables("LMH030_C_OUTKA_M")

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC405.SQL_INSERT_OUTKA_M _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = outkaMTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = outkaMTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetOutkaMComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC405", "UpdateOutkaMData", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "E_SAGYO_REC"
    ''' <summary>
    ''' 作業レコードの新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業レコードの新規作成SQLの構築・発行</remarks>
    Private Function InsertSagyoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim sagyoTbl As DataTable = ds.Tables("LMH030_E_SAGYO")

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC405.SQL_INSERT_SAGYO _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = sagyoTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = sagyoTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetSagyoParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC405", "InsertSagyoData", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "UNSO_L"
    ''' <summary>
    ''' 運送(大)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoLTbl As DataTable = ds.Tables("LMH030_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = unsoLTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC405.SQL_INSERT_UNSO_L _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetUnsoLComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "InsertUnsoLData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "UNSO_M"
    ''' <summary>
    ''' 運送（中）テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送（中）テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsoMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoMTbl As DataTable = ds.Tables("LMH030_UNSO_M")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC405.SQL_INSERT_UNSO_M _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = unsoMTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()
            '条件rowの格納
            Me._Row = unsoMTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetUnsoMComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC405", "InsertUnsoMData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "UNCHIN"
    ''' <summary>
    ''' 運賃テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃新規登録SQLの構築・発行</remarks>
    Private Function InsertUnchinData(ByVal ds As DataSet) As DataSet

        If ds.Tables("F_UNCHIN_TRS").Rows.Count = 0 Then
            'F_UNCHIN_TRSが0件ということは本来無いが、一応念のために0件の時はINSERT処理が行われないようにする
            Return ds
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("F_UNCHIN_TRS")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC405.SQL_INSERT_UNCHIN _
                                                                       , ds.Tables("F_UNCHIN_TRS").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetUnchinComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC405", "InsertUnchinData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "M_DEST(自動追加用)"
    ''' <summary>
    ''' 届先マスタ新規登録(荷送人コードまたは届先コードを元に新規登録:日興産業専用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertMDestData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_M_DEST_SHIP_CD_L")
        '★★★2011.11.11 要望番号439 届先マスタ自動追加 追加 START
        Dim mDestShipCnt As Integer = ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows.Count
        Dim mDestCnt As Integer = ds.Tables("LMH030_M_DEST").Rows.Count
        '★★★2011.11.11 要望番号439 届先マスタ自動追加 追加 END

        'DataSetのIN情報を取得
        '★★★2011.11.11 要望番号439 届先マスタ自動追加 修正 START
        If mDestShipCnt > 0 AndAlso ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("INSERT_TARGET_FLG").Equals("1") = True Then
            inTbl = ds.Tables("LMH030_M_DEST_SHIP_CD_L")
        ElseIf mDestCnt > 0 AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG").Equals("1") = True Then
            inTbl = ds.Tables("LMH030_M_DEST")
        End If
        '★★★2011.11.11 要望番号439 届先マスタ自動追加 修正 END

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC405.SQL_INSERT_M_DEST, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetMdestInsertParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "InsertMDestData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#End Region

#End Region

#Region "パラメータ設定"
#Region "EDI出荷(大)更新パラメータ設定"
    ''' <summary>
    ''' EDI出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInsertOutkaEdiLParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            If String.IsNullOrEmpty(.Item("DEL_KB").ToString()) = True Then
                prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@DEL_KB", BaseConst.FLG.OFF, DBDataType.CHAR))
            Else
                prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            End If
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", Me.NullConvertString(.Item("OUTKA_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", Me.NullConvertString(.Item("SYUBETU_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NAIGAI_KB", Me.NullConvertString(.Item("NAIGAI_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", Me.NullConvertString(.Item("OUTKA_STATE_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", Me.NullConvertString(.Item("OUTKAHOKOKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(.Item("PICK_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_NM", Me.NullConvertString(.Item("NRS_BR_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(.Item("WH_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_NM", Me.NullConvertString(.Item("WH_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(.Item("OUTKA_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", Me.NullConvertString(.Item("OUTKO_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", Me.NullConvertString(.Item("ARR_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", Me.NullConvertString(.Item("ARR_PLAN_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", Me.NullConvertString(.Item("HOKOKU_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(.Item("TOUKI_HOKAN_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", Me.NullConvertString(.Item("CUST_NM_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", Me.NullConvertString(.Item("CUST_NM_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", Me.NullConvertString(.Item("SHIP_CD_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", Me.NullConvertString(.Item("SHIP_CD_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NM_L", Me.NullConvertString(.Item("SHIP_NM_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NM_M", Me.NullConvertString(.Item("SHIP_NM_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", Me.NullConvertString(.Item("EDI_DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", Me.NullConvertString(.Item("DEST_ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", Me.NullConvertString(.Item("DEST_AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", Me.NullConvertString(.Item("DEST_AD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", Me.NullConvertString(.Item("DEST_AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_4", Me.NullConvertString(.Item("DEST_AD_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_5", Me.NullConvertString(.Item("DEST_AD_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", Me.NullConvertString(.Item("DEST_TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_FAX", Me.NullConvertString(.Item("DEST_FAX")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_MAIL", Me.NullConvertString(.Item("DEST_MAIL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", Me.NullConvertString(.Item("DEST_JIS_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(.Item("SP_NHS_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", Me.NullConvertString(.Item("CUST_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(.Item("BUYER_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_MOTO_KB", Me.NullConvertString(.Item("UNSO_MOTO_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", Me.NullConvertString(.Item("UNSO_TEHAI_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYARYO_KB", Me.NullConvertString(.Item("SYARYO_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", Me.NullConvertString(.Item("BIN_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.NullConvertString(.Item("UNSO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NM", Me.NullConvertString(.Item("UNSO_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me.NullConvertString(.Item("UNSO_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_NM", Me.NullConvertString(.Item("UNSO_BR_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", Me.NullConvertString(.Item("UNCHIN_TARIFF_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", Me.NullConvertString(.Item("EXTC_TARIFF_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ATT", Me.NullConvertString(.Item("UNSO_ATT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_YN", Me.NullConvertString(.Item("DENP_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", Me.NullConvertString(.Item("PC_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_YN", Me.NullConvertString(.Item("UNCHIN_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(.Item("NIYAKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_FLAG", Me.NullConvertString(.Item("OUT_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", Me.NullConvertString(.Item("AKAKURO_KB")), DBDataType.CHAR))

            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", Me.NullConvertString(.Item("JISSEKI_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.NullConvertString(.Item("JISSEKI_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.NullConvertString(.Item("JISSEKI_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.NullConvertString(.Item("JISSEKI_TIME")), DBDataType.CHAR))

            prmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.NullConvertZero(.Item("FREE_N01")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.NullConvertZero(.Item("FREE_N02")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.NullConvertZero(.Item("FREE_N03")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.NullConvertZero(.Item("FREE_N04")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.NullConvertZero(.Item("FREE_N05")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.NullConvertZero(.Item("FREE_N06")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.NullConvertZero(.Item("FREE_N07")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.NullConvertZero(.Item("FREE_N08")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.NullConvertZero(.Item("FREE_N09")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.NullConvertZero(.Item("FREE_N10")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", Me.NullConvertString(.Item("FREE_C01")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", Me.NullConvertString(.Item("FREE_C02")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", Me.NullConvertString(.Item("FREE_C03")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C04", Me.NullConvertString(.Item("FREE_C04")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C05", Me.NullConvertString(.Item("FREE_C05")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C06", Me.NullConvertString(.Item("FREE_C06")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C07", Me.NullConvertString(.Item("FREE_C07")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C08", Me.NullConvertString(.Item("FREE_C08")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C09", Me.NullConvertString(.Item("FREE_C09")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C10", Me.NullConvertString(.Item("FREE_C10")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C11", Me.NullConvertString(.Item("FREE_C11")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C12", Me.NullConvertString(.Item("FREE_C12")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C13", Me.NullConvertString(.Item("FREE_C13")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C14", Me.NullConvertString(.Item("FREE_C14")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C15", Me.NullConvertString(.Item("FREE_C15")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C16", Me.NullConvertString(.Item("FREE_C16")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C17", Me.NullConvertString(.Item("FREE_C17")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C18", Me.NullConvertString(.Item("FREE_C18")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C19", Me.NullConvertString(.Item("FREE_C19")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C20", Me.NullConvertString(.Item("FREE_C20")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C21", Me.NullConvertString(.Item("FREE_C21")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C22", Me.NullConvertString(.Item("FREE_C22")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C23", Me.NullConvertString(.Item("FREE_C23")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C24", Me.NullConvertString(.Item("FREE_C24")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C25", Me.NullConvertString(.Item("FREE_C25")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C26", Me.NullConvertString(.Item("FREE_C26")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C27", Me.NullConvertString(.Item("FREE_C27")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C28", Me.NullConvertString(.Item("FREE_C28")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C29", Me.NullConvertString(.Item("FREE_C29")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", Me.NullConvertString(.Item("FREE_C30")), DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", Me.NullConvertString(.Item("SCM_CTL_NO_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDIT_FLAG", Me.NullConvertString(.Item("EDIT_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATCHING_FLAG", Me.NullConvertString(.Item("MATCHING_FLAG")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "EDI出荷(中)更新パラメータ設定"
    ''' <summary>
    ''' EDI出荷(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInsertOutkaEdiMParameter(ByVal conditionRow As DataRow _
                                       , ByVal prmList As ArrayList _
                                       , Optional ByVal eventShubetsu As Integer = 0)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            If String.IsNullOrEmpty(.Item("DEL_KB").ToString()) = True Then
                prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@DEL_KB", BaseConst.FLG.OFF, DBDataType.CHAR))
            Else
                prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            End If
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", .Item("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_CHU", .Item("OUTKA_CTL_NO_CHU").ToString(), DBDataType.CHAR))

            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", .Item("CUST_GOODS_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", .Item("NRS_GOODS_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
#If False Then  '2017/08/25
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", Me.FormatNumValue(.Item("OUTKA_HASU").ToString()), DBDataType.NUMERIC))
#Else
            '端数に出荷総個数をセット
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", Me.FormatNumValue(.Item("OUTKA_TTL_NB").ToString()), DBDataType.NUMERIC))
#End If
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", Me.FormatNumValue(.Item("OUTKA_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me.FormatNumValue(.Item("OUTKA_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", Me.FormatNumValue(.Item("OUTKA_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KB_UT", .Item("KB_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@QT_UT", .Item("QT_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.FormatNumValue(.Item("PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PKG_UT", .Item("PKG_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_KB", .Item("ONDO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_KB", .Item("OUT_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", .Item("AKAKURO_KB").ToString(), DBDataType.CHAR))

            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", .Item("JISSEKI_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.NullConvertString(.Item("JISSEKI_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.NullConvertString(.Item("JISSEKI_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.NullConvertString(.Item("JISSEKI_TIME")), DBDataType.CHAR))

            prmList.Add(MyBase.GetSqlParameter("@SET_KB", .Item("SET_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.FormatNumValue(.Item("FREE_N01").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.FormatNumValue(.Item("FREE_N02").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.FormatNumValue(.Item("FREE_N03").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.FormatNumValue(.Item("FREE_N04").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.FormatNumValue(.Item("FREE_N05").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.FormatNumValue(.Item("FREE_N06").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.FormatNumValue(.Item("FREE_N07").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.FormatNumValue(.Item("FREE_N08").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.FormatNumValue(.Item("FREE_N09").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.FormatNumValue(.Item("FREE_N10").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", .Item("FREE_C01").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", .Item("FREE_C02").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", .Item("FREE_C03").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C04", .Item("FREE_C04").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C05", .Item("FREE_C05").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C06", .Item("FREE_C06").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C07", .Item("FREE_C07").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C08", .Item("FREE_C08").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C09", .Item("FREE_C09").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C10", .Item("FREE_C10").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C11", .Item("FREE_C11").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C12", .Item("FREE_C12").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C13", .Item("FREE_C13").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C14", .Item("FREE_C14").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C15", .Item("FREE_C15").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C16", .Item("FREE_C16").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C17", .Item("FREE_C17").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C18", .Item("FREE_C18").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C19", .Item("FREE_C19").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C20", .Item("FREE_C20").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C21", .Item("FREE_C21").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C22", .Item("FREE_C22").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C23", .Item("FREE_C23").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C24", .Item("FREE_C24").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C25", .Item("FREE_C25").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C26", .Item("FREE_C26").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C27", .Item("FREE_C27").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C28", .Item("FREE_C28").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C29", .Item("FREE_C29").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", .Item("FREE_C30").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))


        End With

    End Sub

#End Region

#Region "商品マスタパラメータ設定(セミEDI用)"
    ''' <summary>
    ''' 商品マスタパラメータ設定(セミEDI用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectMstGoodsParameter(ByVal drMgoods As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", drMgoods.Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", Me.NullConvertString(drMgoods.Item("GOODS_CD_CUST")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(drMgoods.Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(drMgoods.Item("CUST_CD_M")), DBDataType.CHAR))

    End Sub

#End Region

#Region "荷主明細マスタパラメータ設定(セミEDI用)"
    ''' <summary>
    ''' 荷主明細マスタパラメータ設定(セミEDI用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectMstCustDetailParameter(ByVal drMgoods As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", drMgoods.Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(drMgoods.Item("CUST_CD_L")), DBDataType.CHAR))


    End Sub

#End Region

#Region "届先マスタパラメータ設定(セミEDI用)"
    ''' <summary>
    ''' 届先マスタパラメータ設定(セミEDI用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectMstDestParameter(ByVal semiInfoDr As DataRow, ByVal dtRcvDtl As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dtRcvDtl.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(dtRcvDtl.Rows(0).Item("NONYU_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(semiInfoDr.Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(semiInfoDr.Item("CUST_CD_M")), DBDataType.CHAR))

    End Sub

#End Region

    '2015.05.14 引当単位区分の抽出 追加START
#Region "区分マスタパラメータ設定(セミEDI用)"
    ''' <summary>
    ''' 区分マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKbnParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", dt.Rows(0).Item("KBN_GROUP_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_CD", dt.Rows(0).Item("KBN_CD"), DBDataType.CHAR))
    End Sub

#End Region
    '2015.05.14 引当単位区分の抽出 追加END

#Region "EDI出荷(大・中共通)の削除UPDATE文(セミEDI用)"
    ''' <summary>
    ''' EDI出荷(大・中共通)の削除UPDATE文(セミEDI用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdateDelOutkaEdiParameter(ByVal drOutkaEdiRcvIn As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.GetColonEditTime(MyBase.GetSystemTime())))

        prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(drOutkaEdiRcvIn.Item("NRS_BR_CD")), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(drOutkaEdiRcvIn.Item("EDI_CTL_NO")), DBDataType.CHAR))
        'prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(drOutkaEdiRcvIn.Item("EDI_CTL_NO_CHU")), DBDataType.CHAR))
    End Sub

#End Region

#Region "キャンセルデータ抽出パラメータ設定(セミEDI用)"
    ''' <summary>
    ''' キャンセルデータ抽出パラメータ設定(セミEDI用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelecCancelDataParameter(ByVal drInOut As DataRow, ByVal ds As DataSet)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(drInOut.Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(drInOut.Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(drInOut.Item("CUST_CD_M")), DBDataType.CHAR))

        If ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("DTL_DATACHECK_FLG").ToString().Equals(LMConst.FLG.ON) = True Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", String.Concat(Me.NullConvertString(drInOut.Item("CUST_ORD_NO"))), DBDataType.NVARCHAR))
        Else
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", String.Concat("%", Me.NullConvertString(drInOut.Item("CUST_ORD_NO")), "%"), DBDataType.NVARCHAR))
        End If

    End Sub

#End Region

#Region "EDI受信(DTL)同一パラメータ設定"

    ''' <summary>
    ''' EDI受信(DTL)同一パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetCompareDataParameter(ByVal ds As DataSet)

        'EDI受信（DTL）共通項目
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(ds.Tables("LMH030INOUT").Rows(0).Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(ds.Tables("LMH030INOUT").Rows(0).Item("CUST_CD_M")), DBDataType.CHAR))

        ''EDI受信（DTL）荷主個別項目
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_NO", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item("COLUMN_1")), DBDataType.NVARCHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ITEM_NO", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item("COLUMN_2")), DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATERIAL_NO", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item("COLUMN_4")), DBDataType.NVARCHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_TO_PARTY_CD", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item("COLUMN_7")), DBDataType.NVARCHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOLD_TO_PARTY_CD", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item("COLUMN_9")), DBDataType.NVARCHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REF_PO_NUMBER", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item("COLUMN_11")), DBDataType.NVARCHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REQUESTED_DATE", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item("COLUMN_14")), DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PLANNED_GO_DATE", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item("COLUMN_15")), DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_QUANTITY", Me.NullConvertZero(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item("COLUMN_17")), DBDataType.NUMERIC))

        Dim setColKey As String = String.Empty
        '2015.03.27 千葉・MRCデュポン対応 START
        If ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("RCV_NM_DTL").ToString().Equals("H_OUTKAEDI_DTL_ARB") = True Then

            'EDI受信（DTL）荷主個別項目(アルベマール)
            '荷主オーダー番号
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_DEST_CUST_ORD_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_NO", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '荷主注文番号(明細番号)
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_CUST_ORD_NO_DTL_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ITEM_NO", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '商品コード
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_CUST_GOODS_CD_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATERIAL_NO", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '届け先コード
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_DEST_CD_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_TO_PARTY_CD", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '売上先コード
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_SHIP_CD_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOLD_TO_PARTY_CD", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '買主オーダー番号
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_BUYER_ORD_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REF_PO_NUMBER", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            'SAP キャンセル理由コード
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_DEL_KB_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SO_REJECTION_CD", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '納入日ds
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_ARR_PLAN_DATE_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REQUESTED_DATE", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '出荷日
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_OUTKA_PLAN_DATE_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PLANNED_GO_DATE", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '出荷数量
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_OUTKA_TTL_QT_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_QUANTITY", Me.NullConvertZero(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NUMERIC))

        ElseIf ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("RCV_NM_DTL").ToString().Equals("H_OUTKAEDI_DTL_MRC") = True Then

            'EDI受信（DTL）荷主個別項目(ＭＲＣデュポン)
            ''倉庫
            'setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_FREE_C01_NO").ToString())
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            ''2015.05.21 MRCデュポン対応　削除START
            ''契約先
            'setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_SHIP_CD_NO").ToString())
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEIYAKUSAKI_CD", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            ''2015.05.21 MRCデュポン対応　削除END
            '取引ＮＯ
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C25_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TORIHIKI_NO", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '出荷先
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_DEST_CD_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHUKKASAKI_CD", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '出荷先翻訳
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_DEST_NM_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHUKKASAKI_NM", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '住所
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_DEST_ZIP_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ADDRESS", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            ''製品分類
            'setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C01_NO").ToString())
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIHIN_BUNRUI", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '品名
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_CUST_GOODS_CD_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            ''色調翻訳
            'setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_REMARK_NO").ToString())
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SIKICHOU_NM", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            ''客先品番
            'setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C02_NO").ToString())
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYAKUSAKI_HINBAN", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '指図数量
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_OUTKA_TTL_NB_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SASIZU_SURYO", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NUMERIC))
            ''指図数量単位
            'setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C03_NO").ToString())
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SASIZU_SURYO_TANI", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            ''管理数量
            'setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_N01_NO").ToString())
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_SURYO", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NUMERIC))
            ''管理数量単位
            'setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_IRIME_UT_NO").ToString())
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_SURYO_TANI", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            ''同一ロット指定
            'setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C24_NO").ToString())
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO_SITEI", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '出荷日
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_OUTKA_PLAN_DATE_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHUKKA_BI", Me.NullConvertString(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '納期
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_ARR_PLAN_DATE_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NOUKI", Me.NullConvertZero(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '備考
            setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_CUST_ORD_NO_DTL_NO").ToString())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIKO", Me.NullConvertZero(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            ''指定トラック
            'setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C25_NO").ToString())
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITEI_TRUCK", Me.NullConvertZero(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            ''受払区分
            'setColKey = String.Concat("COLUMN_", ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C26_NO").ToString())
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UKEHARAI_KBN", Me.NullConvertZero(ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))

        End If
        '2015.03.27 千葉・MRCデュポン対応 END

    End Sub

#End Region

#Region "SQL"
#Region "スキーマ名称設定"
    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#Region "届先マスタ抽出パラメータ設定"
    ''' <summary>
    ''' 届先マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMdestParameter(ByVal dt As DataTable, ByVal prmUpdFlg As Integer)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))

        If prmUpdFlg = 1 Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("DEST_CD"), DBDataType.NVARCHAR))
        Else
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("SHIP_CD_L"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", dt.Rows(0).Item("SHIP_CD_L"), DBDataType.NVARCHAR))
        End If

    End Sub

#If True Then ' フィルメニッヒ セミEDI対応  20160926 added inoue
    ''' <summary>
    ''' 届先マスタのパラメータ設定(LIKE検索)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMdestLikeParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", String.Concat(dt.Rows(0).Item("DEST_CD"), "%"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", String.Concat(dt.Rows(0).Item("EDI_DEST_CD"), "%"), DBDataType.NVARCHAR))

    End Sub

#End If

#End Region

#Region "EDI出荷(大,中),EDI受信TBL抽出パラメータ設定"
    ''' <summary>
    '''  パラメータ設定（EDI出荷(大・中),EDI受信テーブル・存在チェック）
    ''' </summary>
    ''' <remarks>出荷登録時出荷マスタ検索用SQLの構築</remarks>
    Private Sub setSQLSelectDataExists()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定(共通）
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row("EDI_CTL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me._Row("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "共通パラメータ設定"
    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetSysdataParameter(prmList)

    End Sub

    ''' <summary>
    ''' 新規登録の共通パラメータ設定(EDI9
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameterEdi(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Call Me.SetSysdataParameter(prmList)

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        Call Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList)

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 抽出条件(日時:EDI(大))
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime(ByVal dr As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", dr.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", dr.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "EDI出荷(大)更新パラメータ設定"
    ''' <summary>
    ''' EDI出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", Me.NullConvertString(.Item("OUTKA_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", Me.NullConvertString(.Item("SYUBETU_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NAIGAI_KB", Me.NullConvertString(.Item("NAIGAI_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", Me.NullConvertString(.Item("OUTKA_STATE_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", Me.NullConvertString(.Item("OUTKAHOKOKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(.Item("PICK_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_NM", Me.NullConvertString(.Item("NRS_BR_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(.Item("WH_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_NM", Me.NullConvertString(.Item("WH_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(.Item("OUTKA_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", Me.NullConvertString(.Item("OUTKO_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", Me.NullConvertString(.Item("ARR_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", Me.NullConvertString(.Item("ARR_PLAN_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", Me.NullConvertString(.Item("HOKOKU_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(.Item("TOUKI_HOKAN_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", Me.NullConvertString(.Item("CUST_NM_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", Me.NullConvertString(.Item("CUST_NM_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", Me.NullConvertString(.Item("SHIP_CD_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", Me.NullConvertString(.Item("SHIP_CD_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NM_L", Me.NullConvertString(.Item("SHIP_NM_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NM_M", Me.NullConvertString(.Item("SHIP_NM_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", Me.NullConvertString(.Item("EDI_DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", Me.NullConvertString(.Item("DEST_ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", Me.NullConvertString(.Item("DEST_AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", Me.NullConvertString(.Item("DEST_AD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", Me.NullConvertString(.Item("DEST_AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_4", Me.NullConvertString(.Item("DEST_AD_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_5", Me.NullConvertString(.Item("DEST_AD_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", Me.NullConvertString(.Item("DEST_TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_FAX", Me.NullConvertString(.Item("DEST_FAX")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_MAIL", Me.NullConvertString(.Item("DEST_MAIL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", Me.NullConvertString(.Item("DEST_JIS_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(.Item("SP_NHS_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", Me.NullConvertString(.Item("CUST_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(.Item("BUYER_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_MOTO_KB", Me.NullConvertString(.Item("UNSO_MOTO_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", Me.NullConvertString(.Item("UNSO_TEHAI_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYARYO_KB", Me.NullConvertString(.Item("SYARYO_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", Me.NullConvertString(.Item("BIN_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.NullConvertString(.Item("UNSO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NM", Me.NullConvertString(.Item("UNSO_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me.NullConvertString(.Item("UNSO_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_NM", Me.NullConvertString(.Item("UNSO_BR_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", Me.NullConvertString(.Item("UNCHIN_TARIFF_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", Me.NullConvertString(.Item("EXTC_TARIFF_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ATT", Me.NullConvertString(.Item("UNSO_ATT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_YN", Me.NullConvertString(.Item("DENP_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", Me.NullConvertString(.Item("PC_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_YN", Me.NullConvertString(.Item("UNCHIN_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(.Item("NIYAKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_FLAG", Me.NullConvertString(.Item("OUT_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", Me.NullConvertString(.Item("AKAKURO_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", Me.NullConvertString(.Item("JISSEKI_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.NullConvertZero(.Item("FREE_N01")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.NullConvertZero(.Item("FREE_N02")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.NullConvertZero(.Item("FREE_N03")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.NullConvertZero(.Item("FREE_N04")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.NullConvertZero(.Item("FREE_N05")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.NullConvertZero(.Item("FREE_N06")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.NullConvertZero(.Item("FREE_N07")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.NullConvertZero(.Item("FREE_N08")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.NullConvertZero(.Item("FREE_N09")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.NullConvertZero(.Item("FREE_N10")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", Me.NullConvertString(.Item("FREE_C01")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", Me.NullConvertString(.Item("FREE_C02")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", Me.NullConvertString(.Item("FREE_C03")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C04", Me.NullConvertString(.Item("FREE_C04")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C05", Me.NullConvertString(.Item("FREE_C05")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C06", Me.NullConvertString(.Item("FREE_C06")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C07", Me.NullConvertString(.Item("FREE_C07")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C08", Me.NullConvertString(.Item("FREE_C08")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C09", Me.NullConvertString(.Item("FREE_C09")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C10", Me.NullConvertString(.Item("FREE_C10")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C11", Me.NullConvertString(.Item("FREE_C11")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C12", Me.NullConvertString(.Item("FREE_C12")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C13", Me.NullConvertString(.Item("FREE_C13")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C14", Me.NullConvertString(.Item("FREE_C14")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C15", Me.NullConvertString(.Item("FREE_C15")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C16", Me.NullConvertString(.Item("FREE_C16")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C17", Me.NullConvertString(.Item("FREE_C17")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C18", Me.NullConvertString(.Item("FREE_C18")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C19", Me.NullConvertString(.Item("FREE_C19")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C20", Me.NullConvertString(.Item("FREE_C20")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C21", Me.NullConvertString(.Item("FREE_C21")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C22", Me.NullConvertString(.Item("FREE_C22")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C23", Me.NullConvertString(.Item("FREE_C23")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C24", Me.NullConvertString(.Item("FREE_C24")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C25", Me.NullConvertString(.Item("FREE_C25")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C26", Me.NullConvertString(.Item("FREE_C26")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C27", Me.NullConvertString(.Item("FREE_C27")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C28", Me.NullConvertString(.Item("FREE_C28")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C29", Me.NullConvertString(.Item("FREE_C29")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", Me.NullConvertString(.Item("FREE_C30")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", Me.NullConvertString(.Item("CRT_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me.NullConvertString(.Item("CRT_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", Me.NullConvertString(.Item("CRT_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", Me.NullConvertString(.Item("SCM_CTL_NO_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDIT_FLAG", Me.NullConvertString(.Item("EDIT_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATCHING_FLAG", Me.NullConvertString(.Item("MATCHING_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "EDI出荷(大)更新パラメータ設定(実績日時用)"
    ''' <summary>
    ''' 更新時のパラメータ実績日時(EDI出荷(大,中)用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJissekiParameterEdiLM(ByVal row As DataRow, ByVal eventShubetsu As Integer)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)

            Case LMH030DAC.EventShubetsu.SAVEOUTKA, LMH030DAC.EventShubetsu.HIMODUKE

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.NullConvertString(row.Item("JISSEKI_USER")), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.NullConvertString(row.Item("JISSEKI_DATE")), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.NullConvertString(row.Item("JISSEKI_TIME")), DBDataType.CHAR))

            Case LMH030DAC.EventShubetsu.CREATEJISSEKI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime))

            Case Else

        End Select

    End Sub

#End Region

#Region "まとめ先EDI出荷(大)更新パラメータ設定"
    ''' <summary>
    ''' まとめ先EDI出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiLMatomeParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", String.Concat("04-", .Item("EDI_CTL_NO").ToString())))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))

        End With

    End Sub

#End Region

#Region "EDI出荷(中)更新パラメータ設定"
    ''' <summary>
    ''' EDI出荷(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", .Item("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_CHU", .Item("OUTKA_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", .Item("CUST_GOODS_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", .Item("NRS_GOODS_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", Me.FormatNumValue(.Item("OUTKA_HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", Me.FormatNumValue(.Item("OUTKA_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me.FormatNumValue(.Item("OUTKA_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", Me.FormatNumValue(.Item("OUTKA_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KB_UT", .Item("KB_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@QT_UT", .Item("QT_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.FormatNumValue(.Item("PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PKG_UT", .Item("PKG_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_KB", .Item("ONDO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_KB", .Item("OUT_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", .Item("AKAKURO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", .Item("JISSEKI_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SET_KB", .Item("SET_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.FormatNumValue(.Item("FREE_N01").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.FormatNumValue(.Item("FREE_N02").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.FormatNumValue(.Item("FREE_N03").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.FormatNumValue(.Item("FREE_N04").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.FormatNumValue(.Item("FREE_N05").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.FormatNumValue(.Item("FREE_N06").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.FormatNumValue(.Item("FREE_N07").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.FormatNumValue(.Item("FREE_N08").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.FormatNumValue(.Item("FREE_N09").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.FormatNumValue(.Item("FREE_N10").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", .Item("FREE_C01").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", .Item("FREE_C02").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", .Item("FREE_C03").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C04", .Item("FREE_C04").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C05", .Item("FREE_C05").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C06", .Item("FREE_C06").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C07", .Item("FREE_C07").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C08", .Item("FREE_C08").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C09", .Item("FREE_C09").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C10", .Item("FREE_C10").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C11", .Item("FREE_C11").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C12", .Item("FREE_C12").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C13", .Item("FREE_C13").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C14", .Item("FREE_C14").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C15", .Item("FREE_C15").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C16", .Item("FREE_C16").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C17", .Item("FREE_C17").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C18", .Item("FREE_C18").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C19", .Item("FREE_C19").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C20", .Item("FREE_C20").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C21", .Item("FREE_C21").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C22", .Item("FREE_C22").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C23", .Item("FREE_C23").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C24", .Item("FREE_C24").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C25", .Item("FREE_C25").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C26", .Item("FREE_C26").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C27", .Item("FREE_C27").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C28", .Item("FREE_C28").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C29", .Item("FREE_C29").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", .Item("FREE_C30").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", .Item("CRT_USER").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", .Item("CRT_TIME").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))
        End With

    End Sub

#End Region

#Region "出荷(大)更新パラメータ設定"
    ''' <summary>
    ''' 出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me.NullConvertString(.Item("OUTKA_NO_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FURI_NO", Me.NullConvertString(.Item("FURI_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", Me.NullConvertString(.Item("OUTKA_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", Me.NullConvertString(.Item("SYUBETU_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", Me.NullConvertString(.Item("OUTKA_STATE_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", Me.NullConvertString(.Item("OUTKAHOKOKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(.Item("PICK_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", Me.NullConvertString(.Item("DENP_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_KANRYO_INFO", Me.NullConvertString(.Item("ARR_KANRYO_INFO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(.Item("WH_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(.Item("OUTKA_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", Me.NullConvertString(.Item("OUTKO_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", Me.NullConvertString(.Item("ARR_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", Me.NullConvertString(.Item("ARR_PLAN_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(.Item("TOUKI_HOKAN_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@END_DATE", Me.NullConvertString(.Item("END_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", Me.NullConvertString(.Item("SHIP_CD_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", Me.NullConvertString(.Item("SHIP_CD_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", Me.NullConvertString(.Item("DEST_AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", Me.NullConvertString(.Item("DEST_TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", Me.NullConvertString(.Item("NHS_REMARK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(.Item("SP_NHS_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", Me.NullConvertString(.Item("CUST_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(.Item("BUYER_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.NullConvertZero(.Item("OUTKA_PKG_NB")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DENP_YN", Me.NullConvertString(.Item("DENP_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", Me.NullConvertString(.Item("PC_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(.Item("NIYAKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALL_PRINT_FLAG", Me.NullConvertString(.Item("ALL_PRINT_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIHUDA_FLAG", Me.NullConvertString(.Item("NIHUDA_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_FLAG", Me.NullConvertString(.Item("NHS_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_FLAG", Me.NullConvertString(.Item("DENP_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_FLAG", Me.NullConvertString(.Item("COA_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_FLAG", Me.NullConvertString(.Item("HOKOKU_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATOME_PICK_FLAG", Me.NullConvertString(.Item("MATOME_PICK_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_DATE", Me.NullConvertString(.Item("LAST_PRINT_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_TIME", Me.NullConvertString(.Item("LAST_PRINT_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SASZ_USER", Me.NullConvertString(.Item("SASZ_USER")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_USER", Me.NullConvertString(.Item("OUTKO_USER")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEN_USER", Me.NullConvertString(.Item("KEN_USER")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", Me.NullConvertString(.Item("OUTKA_USER")), DBDataType.CHAR))

            'イベント種別の判断
            Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)

                '出荷登録処理
                '出荷登録SQL CONST名
                Case LMH030DAC.EventShubetsu.SAVEOUTKA
                    prmList.Add(MyBase.GetSqlParameter("@HOU_USER", Me.NullConvertString(.Item("HOU_USER")), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", Me.NullConvertString(.Item("HOKOKU_DATE")), DBDataType.CHAR))

                    '実績作成SQL CONST名
                Case LMH030DAC.EventShubetsu.CREATEJISSEKI
                    prmList.Add(MyBase.GetSqlParameter("@HOU_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))

            End Select
            prmList.Add(MyBase.GetSqlParameter("@ORDER_TYPE", Me.NullConvertString(.Item("ORDER_TYPE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_KB", Me.NullConvertString(.Item("DEST_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", Me.NullConvertString(.Item("DEST_AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", Me.NullConvertString(.Item("DEST_AD_2")), DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", Me.NullConvertString(.Item("WH_TAB_STATUS")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_YN", Me.NullConvertString(.Item("WH_TAB_YN")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "出荷(中)更新パラメータ設定"
    ''' <summary>
    ''' 出荷(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_SET_NO", .Item("EDI_SET_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", Me.FormatNumValue(.Item("OUTKA_HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", Me.FormatNumValue(.Item("OUTKA_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me.FormatNumValue(.Item("OUTKA_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", Me.FormatNumValue(.Item("OUTKA_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BACKLOG_NB", Me.FormatNumValue(.Item("BACKLOG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BACKLOG_QT", Me.FormatNumValue(.Item("BACKLOG_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_M_PKG_NB", Me.FormatNumValue(.Item("OUTKA_M_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAIKO_KB", .Item("ZAIKO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SOURCE_CD", .Item("SOURCE_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YELLOW_CARD", .Item("YELLOW_CARD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", Me.FormatNumValue(.Item("PRINT_SORT").ToString()), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "作業更新パラメータ設定"
    ''' <summary>
    ''' 作業の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Item("SAGYO_REC_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", .Item("SAGYO_COMP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SKYU_CHK", .Item("SKYU_CHK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Item("SAGYO_SIJI_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", .Item("INOUTKA_NO_LM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", .Item("SAGYO_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", .Item("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", .Item("SAGYO_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", .Item("SAGYO_GK").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", .Item("REMARK_SKYU").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", .Item("SAGYO_COMP_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", .Item("SAGYO_COMP_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_SAGYO_FLG", .Item("DEST_SAGYO_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "運送(大)パラメータ設定"
    ''' <summary>
    ''' 運送(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIYU_KB", .Item("JIYU_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", .Item("DENP_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_TIME", .Item("OUTKA_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_ACT_TIME", .Item("ARR_ACT_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", .Item("CUST_REF_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD", .Item("SHIP_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_PKG_NB", Me.FormatNumValue(.Item("UNSO_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_WT", Me.FormatNumValue(.Item("UNSO_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", .Item("PC_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", .Item("TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@VCLE_KB", .Item("VCLE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_3", .Item("AD_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", .Item("UNSO_TEHAI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUY_CHU_NO", .Item("BUY_CHU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AREA_CD", .Item("AREA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TYUKEI_HAISO_FLG", .Item("TYUKEI_HAISO_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_CD", .Item("SYUKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_CD", .Item("HAIKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", .Item("TRIP_NO_SYUKA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", .Item("TRIP_NO_TYUKEI").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", .Item("TRIP_NO_HAIKA").ToString(), DBDataType.CHAR))

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            'END UMANO 要望番号1302 支払運賃に伴う修正。

        End With

    End Sub

#End Region

#Region "運送(中)パラメータ設定"
    ''' <summary>
    ''' 運送(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_NB", Me.FormatNumValue(.Item("UNSO_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_QT", Me.FormatNumValue(.Item("UNSO_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@QT_UT", .Item("QT_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HASU", Me.FormatNumValue(.Item("HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZBUKA_CD", .Item("ZBUKA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ABUKA_CD", .Item("ABUKA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.FormatNumValue(.Item("PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "運賃パラメータ設定"
    ''' <summary>
    ''' 運賃の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnchinComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", .Item("SEIQ_GROUP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", .Item("SEIQ_GROUP_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))       '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            prmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_SYARYO_KB", .Item("SEIQ_SYARYO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_PKG_UT", .Item("SEIQ_PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_NG_NB", Me.FormatNumValue(.Item("SEIQ_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_DANGER_KB", .Item("SEIQ_DANGER_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_BUNRUI_KB", .Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_KYORI", Me.FormatNumValue(.Item("SEIQ_KYORI").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WT", Me.FormatNumValue(.Item("SEIQ_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_UNCHIN", Me.FormatNumValue(.Item("SEIQ_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_CITY_EXTC", Me.FormatNumValue(.Item("SEIQ_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WINT_EXTC", Me.FormatNumValue(.Item("SEIQ_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_RELY_EXTC", Me.FormatNumValue(.Item("SEIQ_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TOLL", Me.FormatNumValue(.Item("SEIQ_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_INSU", Me.FormatNumValue(.Item("SEIQ_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_FIXED_FLAG", .Item("SEIQ_FIXED_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DECI_NG_NB", Me.FormatNumValue(.Item("DECI_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_KYORI", Me.FormatNumValue(.Item("DECI_KYORI").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WT", Me.FormatNumValue(.Item("DECI_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", Me.FormatNumValue(.Item("DECI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", Me.FormatNumValue(.Item("DECI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", Me.FormatNumValue(.Item("DECI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", Me.FormatNumValue(.Item("DECI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", Me.FormatNumValue(.Item("DECI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_INSU", Me.FormatNumValue(.Item("DECI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_UNCHIN", Me.FormatNumValue(.Item("KANRI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_CITY_EXTC", Me.FormatNumValue(.Item("KANRI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_WINT_EXTC", Me.FormatNumValue(.Item("KANRI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_RELY_EXTC", Me.FormatNumValue(.Item("KANRI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_TOLL", Me.FormatNumValue(.Item("KANRI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_INSU", Me.FormatNumValue(.Item("KANRI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_KANRI", .Item("SAGYO_KANRI").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "届先マスタ自動追加用パラメータ設定"
    ''' <summary>
    ''' 届先マスタの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetMdestInsertParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CD", Me.NullConvertString(.Item("EDI_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP", Me.NullConvertString(.Item("ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_1", Me.NullConvertString(.Item("AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_2", Me.NullConvertString(.Item("AD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_3", Me.NullConvertString(.Item("AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_DEST_CD", Me.NullConvertString(.Item("CUST_DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SALES_CD", Me.NullConvertString(.Item("SALES_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(.Item("SP_NHS_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_UNSO_CD", Me.NullConvertString(.Item("SP_UNSO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_UNSO_BR_CD", Me.NullConvertString(.Item("SP_UNSO_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELI_ATT", Me.NullConvertString(.Item("DELI_ATT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CARGO_TIME_LIMIT", Me.NullConvertString(.Item("CARGO_TIME_LIMIT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LARGE_CAR_YN", Me.NullConvertString(.Item("LARGE_CAR_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TEL", Me.NullConvertString(.Item("TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FAX", Me.NullConvertString(.Item("FAX")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_SEIQTO_CD", Me.NullConvertString(.Item("UNCHIN_SEIQTO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIS", Me.NullConvertString(.Item("JIS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KYORI", Me.NullConvertZero(.Item("KYORI")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(.Item("PICK_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", Me.NullConvertString(.Item("BIN_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTO_CHAKU_KB", Me.NullConvertString(.Item("MOTO_CHAKU_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@URIAGE_CD", Me.NullConvertString(.Item("URIAGE_CD")), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "届先マスタ更新パラメータ設定(セミEDI標準荷主共通)"

    ''' <summary>
    ''' 届先マスタの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetMdestUpdateParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")

            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP", Me.NullConvertString(.Item("ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_1", Me.NullConvertString(.Item("AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_2", Me.NullConvertString(.Item("AD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_3", Me.NullConvertString(.Item("AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TEL", Me.NullConvertString(.Item("TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIS", Me.NullConvertString(.Item("JIS")), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "Null変換"
    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertString(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = String.Empty
        End If

        Return value

    End Function

    ''' <summary>
    ''' Null変換（数値）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertZero(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = 0
        End If

        Return value

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

#Region "時間コロン編集"
    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

#End Region

#Region "UPDATE文の発行"
    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <remarks></remarks>
    Private Sub UpdateResultChk(ByVal cmd As SqlCommand)

        Dim updateCnt As Integer = 0

        updateCnt = MyBase.GetUpdateResult(cmd)
        'SQLの発行
        If updateCnt < 1 Then
            MyBase.SetMessage("E011")
        End If

        MyBase.SetResultCount(updateCnt)

    End Sub
#End Region

#End Region

#End Region

#Region "Insert"
#Region "H_OUTKAEDI_DTL_XXX(セミEDI)"
    ''' <summary>
    ''' EDI受信(DTL)テーブル新規登録
    ''' </summary>
    ''' <param name="setDs">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信(DTL)テーブル更新SQLの構築・発行</remarks>
    Private Function InsertOutkaEdiRcvDtl(ByVal setDs As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtOutkaEdiRcvDtl As DataTable = setDs.Tables("LMH030_EDI_TORIKOMI_DTL")

        'スキーマ名設定
        Dim sql As String = String.Empty
        If setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("RCV_NM_DTL").ToString().Equals("H_OUTKAEDI_DTL_ARB") = True Then
            sql = Me.SetSchemaNm(LMH030DAC405.SQL_INSERT_OUTKAEDI_DTL_ARB, setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("NRS_BR_CD").ToString())
        ElseIf setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("RCV_NM_DTL").ToString().Equals("H_OUTKAEDI_DTL_MRC") = True Then
            sql = Me.SetSchemaNm(LMH030DAC405.SQL_INSERT_OUTKAEDI_DTL_MRC, setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("NRS_BR_CD").ToString())
        End If

        ''受信DTLテーブル名設定
        'Dim drSemiEdiInfo As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        'sql = Me.SetTableNmOutkaEdiRcvDtl(sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dtOutkaEdiRcvDtl.Rows(0)

        'パラメータ設定
        Call Me.SetInsertOutkaEdiRcvDtlParameter(setDs, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC405", "InsertOutkaEdiRcvDtl", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return setDs

    End Function

#End Region

#Region "休日判定"
    '#Region "休日マスタ件数取得処理"

    '    ''' <summary>
    '    ''' 休日マスタ件数取得処理
    '    ''' </summary>
    '    ''' <param name="sDate"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function SelectCountM_HOL(ByVal sNRS_BR_CD As String, ByVal sDate As String) As Integer

    '        'SQL文のコンパイル
    '        Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(SQL_SelectCountM_HOL, sNRS_BR_CD))

    '        Dim rtn As Integer = 0
    '        Dim rtnSuc As Boolean = False

    '        'SQLパラメータ初期化
    '        _SqlPrmList = New ArrayList

    '        'パラメータ設定
    '        _SqlPrmList.Add(GetSqlParameter("@HOL", sDate, DBDataType.NVARCHAR))

    '        'パラメータの反映
    '        For Each obj As Object In _SqlPrmList
    '            cmd.Parameters.Add(obj)
    '        Next

    '        MyBase.Logger.WriteSQLLog("SelectCountM_HOL", "SelectData", cmd)

    '        'SQLの発行
    '        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

    '        '処理件数の設定
    '        reader.Read()
    '        rtn = Convert.ToInt32(reader(0))
    '        reader.Close()

    '        Return rtn

    '    End Function

    '#End Region

#End Region

    ''' <summary>
    ''' 受信DTLテーブル名設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTableNmOutkaEdiRcvDtl(ByVal sql As String) As String

        Dim rcvTblNm As String = _Row("RCV_NM_DTL").ToString()
        sql = sql.Replace("$RCV_DTL$", rcvTblNm)

        Return sql

    End Function

#Region "EDI受信(DTL)更新パラメータ設定"

    ''' <summary>
    ''' EDI受信(DTL)更新パラメータ設定
    ''' </summary>
    ''' <param name="setDs">DataSet</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInsertOutkaEdiRcvDtlParameter(ByVal setDs As DataSet, ByVal prmList As ArrayList)

        'With conditionRow

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
        Dim setColKey As String = String.Empty

        setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_DEL_KB_NO").ToString())

        '2015.03.27 千葉・MRCデュポン対応 修正START
        If String.IsNullOrEmpty(setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_DEL_KB_NO").ToString()) = True Then
            '2015.03.27 千葉・MRCデュポン対応 修正END
            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", LMConst.FLG.OFF, DBDataType.CHAR))
            '削除フラグ
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        Else

            If String.IsNullOrEmpty(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey).ToString()) = False Then
                prmList.Add(MyBase.GetSqlParameter("@DEL_KB", LMConst.FLG.ON, DBDataType.CHAR))
                '削除フラグ
                prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.ON, DBDataType.CHAR))
            Else
                prmList.Add(MyBase.GetSqlParameter("@DEL_KB", LMConst.FLG.OFF, DBDataType.CHAR))
                '削除フラグ
                prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
            End If

        End If

        'EDI受信（DTL）共通項目
        prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item("FILE_NAME_RCV")), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@REC_NO", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item("REC_NO")), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@GYO", "000", DBDataType.CHAR))

        'prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(setDs.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(setDs.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("EDI_CTL_NO")), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(setDs.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("EDI_CTL_NO_CHU")), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", String.Concat(Left(Me.NullConvertString(setDs.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("EDI_CTL_NO")).ToString(), 1), "00000000"), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_CHU", "000", DBDataType.CHAR))

        'prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(setDs.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("CUST_CD_L")), DBDataType.CHAR))
        'prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(setDs.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("CUST_CD_M")), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("CUST_CD_L")), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("CUST_CD_M")), DBDataType.CHAR))

        prmList.Add(MyBase.GetSqlParameter("@PRTFLG", LMConst.FLG.OFF, DBDataType.CHAR))

        '2015.03.27 千葉・MRCデュポン対応 START
        If setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("RCV_NM_DTL").ToString().Equals("H_OUTKAEDI_DTL_ARB") = True Then
            'EDI受信（DTL）荷主個別項目(アルベマール)
            '荷主オーダー番号
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_DEST_CUST_ORD_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@ORDER_NO", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '荷主注文番号(明細番号)
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_CUST_ORD_NO_DTL_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@ITEM_NO", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            'SAP作成ユーザー
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_FREE_C01_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SO_PO_CREATE_USER", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '商品コード
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_CUST_GOODS_CD_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@MATERIAL_NO", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '商品名
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_GOODS_NM_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SKU_DESCRIPTION", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            'プラントコード
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_FREE_C03_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@PLANT", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '届け先コード
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_DEST_CD_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SHIP_TO_PARTY_CD", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '届先名
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_FREE_C24_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SHIP_TO_PARTY_NAME", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '売上先コード
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_SHIP_CD_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SOLD_TO_PARTY_CD", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '売上先名
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_FREE_C25_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SOLD_TO_PARTY_NAME", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '買主オーダー番号
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_BUYER_ORD_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@REF_PO_NUMBER", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            'SAP キャンセル理由コード
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_DEL_KB_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SO_REJECTION_CD", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            'SAP作成日
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_FREE_C02_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SO_PO_CREATE_DATE", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '納入日
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_ARR_PLAN_DATE_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@REQUESTED_DATE", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '出荷日
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_OUTKA_PLAN_DATE_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@PLANNED_GO_DATE", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '入り目単位
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_IRIME_UT_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@ORDER_UNIT", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '出荷数量
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_OUTKA_TTL_QT_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@ORDER_QUANTITY", Me.NullConvertZero(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NUMERIC))
        ElseIf setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("RCV_NM_DTL").ToString().Equals("H_OUTKAEDI_DTL_MRC") = True Then

            'EDI受信（DTL）荷主個別項目(ＭＲＣデュポン)
            '倉庫
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_FREE_C01_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SOKO_CD", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            ''2015.05.21 MRCデュポン対応　修正START(契約者コードはセットしない→届け先コードが(契約者コード＋出荷先コード))
            ''契約先
            'setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_SHIP_CD_NO").ToString())
            'prmList.Add(MyBase.GetSqlParameter("@KEIYAKUSAKI_CD", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEIYAKUSAKI_CD", String.Empty, DBDataType.NVARCHAR))
            ''2015.05.21 MRCデュポン対応　修正END
            '取引ＮＯ
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C25_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@TORIHIKI_NO", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '出荷先
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_DEST_CD_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SHUKKASAKI_CD", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '出荷先翻訳
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_DEST_NM_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SHUKKASAKI_NM", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '住所
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_DEST_ZIP_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@ADDRESS", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '製品分類
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C01_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SEIHIN_BUNRUI", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '品名
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_CUST_GOODS_CD_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '色調翻訳
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_REMARK_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SIKICHOU_NM", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '客先品番
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C02_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@KYAKUSAKI_HINBAN", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '指図数量
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_OUTKA_TTL_NB_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SASIZU_SURYO", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NUMERIC))
            '指図数量単位
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C03_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SASIZU_SURYO_TANI", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))
            '管理数量
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_N01_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@KANRI_SURYO", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NUMERIC))
            '管理数量単位(先方入り目単位は取り込まないように修正)
            'setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_IRIME_UT_NO").ToString())
            'prmList.Add(MyBase.GetSqlParameter("@KANRI_SURYO_TANI", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_SURYO_TANI", String.Empty, DBDataType.NVARCHAR))
            '同一ロット指定
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C24_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO_SITEI", Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))

            '2015.05.13 千葉・MRCデュポン対応 START
            '出荷日
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_OUTKA_PLAN_DATE_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@SHUKKA_BI", Left(Replace(Me.NullConvertString(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)).ToString(), "/", String.Empty), 8), DBDataType.CHAR))
            '納期
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("L_ARR_PLAN_DATE_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@NOUKI", Left(Replace(Me.NullConvertZero(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)).ToString(), "/", String.Empty), 8), DBDataType.CHAR))
            '2015.05.13 千葉・MRCデュポン対応 END

            '備考
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_CUST_ORD_NO_DTL_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@BIKO", Me.NullConvertZero(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            '指定トラック(指定トラックは取り込まないように修正)
            'setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C25_NO").ToString())
            'prmList.Add(MyBase.GetSqlParameter("@SITEI_TRUCK", Me.NullConvertZero(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITEI_TRUCK", String.Empty, DBDataType.NVARCHAR))
            '受払区分
            setColKey = String.Concat("COLUMN_", setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("M_FREE_C26_NO").ToString())
            prmList.Add(MyBase.GetSqlParameter("@UKEHARAI_KBN", Me.NullConvertZero(setDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(0).Item(setColKey)), DBDataType.CHAR))

        End If
        '2015.03.27 千葉・MRCデュポン対応 END

        'EDI受信（DTL）共通項目
        prmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", "", DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", LMConst.FLG.ON, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", "", DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", "", DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", "", DBDataType.CHAR))

        prmList.Add(MyBase.GetSqlParameter("@SEND_USER", "", DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SEND_DATE", "", DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SEND_TIME", "", DBDataType.CHAR))

        prmList.Add(MyBase.GetSqlParameter("@DELETE_USER", "", DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", "", DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", "", DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", "", DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO_CHU", "", DBDataType.CHAR))

        prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))

        prmList.Add(MyBase.GetSqlParameter("@PRT_USER", String.Empty, DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@PRT_DATE", String.Empty, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@PRT_TIME", String.Empty, DBDataType.CHAR))

        prmList.Add(MyBase.GetSqlParameter("@EDI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@EDI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@EDI_TIME", updTime))

        prmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", String.Empty, DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE", String.Empty, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@OUTKA_TIME", String.Empty, DBDataType.CHAR))

        'システム管理用項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime()))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime()))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

        'End With

    End Sub

#End Region

#End Region

End Class
