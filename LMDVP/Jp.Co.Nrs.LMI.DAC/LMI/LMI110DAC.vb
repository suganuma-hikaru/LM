' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特殊荷主機能
'  プログラムID     :  LMI110DAC : 日医工製品マスタ登録
'  作  成  者       :  [寺川徹]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI110DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI110DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' EXCEL用COLUMタイトル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_COLTITLE As String = "製品コード"


#Region "検索処理 SQL"

#Region "商品マスタ"

    ''' <summary>
    ''' 商品マスタ検索処理(件数取得(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_SELECT As String = " SELECT COUNT(NIK.GOODS_CD_NIK)	   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' 商品マスタ検索処理(データ取得(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_SELECT As String = " SELECT                                                                                                " & vbNewLine _
                                           & "CASE WHEN M_GOODS.GOODS_CD_CUST IS NULL THEN '新規'                                                            " & vbNewLine _
                                           & "     WHEN M_GOODS.GOODS_CD_CUST IS NOT NULL AND                                                              " & vbNewLine _
                                           & "         GOODS_CNT.CNT > 1 THEN '重複'                                                              " & vbNewLine _
                                           & "     ELSE '変更'                                                                                               " & vbNewLine _
                                           & "END                                                  AS STATUS                                                 " & vbNewLine _
                                           & ",NIK.M_GOODS_UPD_FLG                                 AS M_GOODS_UPD_FLG                                        " & vbNewLine _
                                           & ",CASE WHEN NIK.M_GOODS_UPD_FLG = '00' THEN '未反映'                                                            " & vbNewLine _
                                           & "      ELSE '反映済'                                                                                            " & vbNewLine _
                                           & "END                                                  AS TORIKOMI_KBN                                          " & vbNewLine _
                                           & ",NIK.SYS_ENT_DATE                                    AS SYS_ENT_DATE                                           " & vbNewLine _
                                           & ",M_GOODS.GOODS_CD_NRS                                AS GOODS_CD_NRS                                           " & vbNewLine _
                                           & ",NIK.GOODS_CD_NIK                                    AS GOODS_CD_NIK                                           " & vbNewLine _
                                           & ",NIK.GOODS_NM                                        AS GOODS_NM                                               " & vbNewLine _
                                           & ",NIK.GOODS_NM_KANA                                   AS GOODS_NM_KANA                                          " & vbNewLine _
                                           & ",NIK.GOODS_KIKAKU                                    AS GOODS_KIKAKU                                           " & vbNewLine _
                                           & ",NIK.GOODS_KIKAKU_KANA                               AS GOODS_KIKAKU_KANA                                      " & vbNewLine _
                                           & ",NIK.JAN_CD                                          AS JAN_CD                                                 " & vbNewLine _
                                           & ",NIK.KANRI_KB                                        AS KANRI_KB                                               " & vbNewLine _
                                           & ",KBN1.KBN_NM1                                        AS KANRI_KB_NM                                            " & vbNewLine _
                                           & ",NIK.ONDO_KB                                         AS ONDO_KB                                                " & vbNewLine _
                                           & ",KBN2.KBN_NM1                                        AS ONDO_KB_NM                                             " & vbNewLine _
                                           & ",NIK.YUKO_MONTH                                      AS YUKO_MONTH                                             " & vbNewLine _
                                           & ",CASE WHEN M_GOODS.PKG_NB IS NULL THEN                                                                         " & vbNewLine _
                                           & " '0'                                                                                                           " & vbNewLine _
                                           & "Else  M_GOODS.PKG_NB                                                                                           " & vbNewLine _
                                           & "END                                                  AS PKG_NB                                                 " & vbNewLine _
                                           & ",M_GOODS.NB_UT                                       AS NB_UT                                                  " & vbNewLine _
                                           & ",CASE WHEN M_GOODS.STD_IRIME_NB IS NULL THEN                                                                  " & vbNewLine _
                                           & " '0'                                                                                                           " & vbNewLine _
                                           & "Else  M_GOODS.STD_IRIME_NB                                                                                    " & vbNewLine _
                                           & "END                                                  AS STD_IRIME_NB                                          " & vbNewLine _
                                           & ",M_GOODS.STD_IRIME_UT                                AS STD_IRIME_UT                                           " & vbNewLine _
                                           & ",NIK.NB_WT_GS                                        AS NB_WT_GS                                               " & vbNewLine _
                                           & ",NIK.NB_FORM_LENGTH                                  AS NB_FORM_LENGTH                                         " & vbNewLine _
                                           & ",NIK.NB_FORM_WIDTH                                   AS NB_FORM_WIDTH                                          " & vbNewLine _
                                           & ",NIK.NB_FORM_HIGHT                                   AS NB_FORM_HIGHT                                          " & vbNewLine _
                                           & ",NIK.PKG_FORM_LENGTH                                 AS PKG_FORM_LENGTH                                        " & vbNewLine _
                                           & ",NIK.PKG_FORM_WIDTH                                  AS PKG_FORM_WIDTH                                         " & vbNewLine _
                                           & ",NIK.PKG_FORM_HIGHT                                  AS PKG_FORM_HIGHT                                         " & vbNewLine _
                                           & ",NIK.PKG_WT_GS                                       AS PKG_WT_GS                                              " & vbNewLine _
                                           & ",NIK.TEKIYO_DATE                                     AS TEKIYO_DATE                                            " & vbNewLine _
                                           & ",NIK.GOODS_NM_RYAKU                                  AS GOODS_NM_RYAKU                                         " & vbNewLine _
                                           & ",NIK.ITF_CD                                          AS ITF_CD                                                 " & vbNewLine _
                                           & ",NIK.SIIRE_CD                                        AS SIIRE_CD                                               " & vbNewLine _
                                           & ",NIK.NB_ML                                           AS NB_ML                                                  " & vbNewLine _
                                           & ",NIK.PKG_ML                                          AS PKG_ML                                                 " & vbNewLine _
                                           & ",NIK.PLT_PER_PKG_UT                                  AS PLT_PER_PKG_UT                                         " & vbNewLine _
                                           & ",NIK.SURFACE_PKG_NB                                  AS SURFACE_PKG_NB                                         " & vbNewLine _
                                           & ",NIK.SURFACE_NUM_ROW                                 AS SURFACE_NUM_ROW                                        " & vbNewLine _
                                           & ",M_GOODS.ONDO_KB                                     AS M_GOODS_ONDO_KB                                        " & vbNewLine _
                                           & ",CASE WHEN M_GOODS.STD_IRIME_NB IS NULL THEN                                                                   " & vbNewLine _
                                           & " '0'                                                                                                           " & vbNewLine _
                                           & "Else  M_GOODS.STD_IRIME_NB                                                                                     " & vbNewLine _
                                           & "END                                                  AS M_GOODS_STD_IRIME_NB                                   " & vbNewLine _
                                           & ",CASE WHEN M_GOODS.PKG_NB IS NULL THEN                                                                         " & vbNewLine _
                                           & " '0'                                                                                                           " & vbNewLine _
                                           & "Else M_GOODS.PKG_NB                                                                                      " & vbNewLine _
                                           & "END                                                  AS M_GOODS_PKG_NB                                         " & vbNewLine _
                                           & ",GOODS_CNT.CNT                                       AS M_GOODS_CNT                                            " & vbNewLine _
                                           & ",NIK.SYS_UPD_DATE                                    AS M_SEIHIN_SYS_UPD_DATE                                  " & vbNewLine _
                                           & ",NIK.SYS_UPD_TIME                                    AS M_SEIHIN_SYS_UPD_TIME                                  " & vbNewLine _
                                           & ",M_GOODS.SYS_UPD_DATE                                AS M_GOODS_SYS_UPD_DATE                                   " & vbNewLine _
                                           & ",M_GOODS.SYS_UPD_TIME                                AS M_GOODS_SYS_UPD_TIME                                   " & vbNewLine _
                                           & ",NIK.SYS_DEL_FLG                                     AS M_SEIHIN_SYS_DEL_FLAG                                  " & vbNewLine _
                                           & ",M_GOODS.SYS_DEL_FLG                                 AS M_GOODS_SYS_DEL_FLAG                                   " & vbNewLine

    ''' <summary>
    ''' 商品マスタ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_FROM As String = " FROM                                                                                                " & vbNewLine _
                                            & " $LM_TRN$..M_SEHIN_NIK NIK                                        " & vbNewLine _
                                            & "LEFT JOIN                                                         " & vbNewLine _
                                            & "  $LM_MST$..M_GOODS M_GOODS                                       " & vbNewLine _
                                            & "ON                                                                " & vbNewLine _
                                            & " NIK.NRS_BR_CD = M_GOODS.NRS_BR_CD                                " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & "SUBSTRING( NIK.GOODS_CD_NIK,4,6) = M_GOODS.GOODS_CD_CUST          " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " M_GOODS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " M_GOODS.CUST_CD_L = @CUST_CD_L                                   " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " M_GOODS.CUST_CD_M = @CUST_CD_M                                   " & vbNewLine _
                                            & "LEFT JOIN                                                         " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN KBN1                                            " & vbNewLine _
                                            & "ON                                                                " & vbNewLine _
                                            & " NIK.KANRI_KB = KBN1.KBN_CD                                       " & vbNewLine _
                                            & "AND                                                               " & vbNewLine _
                                            & " KBN1.KBN_GROUP_CD = 'I005'                                       " & vbNewLine _
                                            & "LEFT JOIN                                                         " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN KBN2                                            " & vbNewLine _
                                            & "ON                                                                " & vbNewLine _
                                            & " NIK.ONDO_KB = KBN2.KBN_CD                                        " & vbNewLine _
                                            & "AND                                                               " & vbNewLine _
                                            & " KBN2.KBN_GROUP_CD = 'I006'                                       " & vbNewLine _
                                            & "    LEFT JOIN                                                     " & vbNewLine _
                                            & "(SELECT                                                           " & vbNewLine _
                                            & " COUNT(*) AS CNT                                                  " & vbNewLine _
                                            & ",NRS_BR_CD                                                        " & vbNewLine _
                                            & ",GOODS_CD_CUST                                                    " & vbNewLine _
                                            & "--,STD_IRIME_NB                                                   " & vbNewLine _
                                            & "FROM                                                              " & vbNewLine _
                                            & "$LM_MST$..M_GOODS M_GOODS                                         " & vbNewLine _
                                            & "WHERE                                                             " & vbNewLine _
                                            & "CUST_CD_L = @CUST_CD_L                                            " & vbNewLine _
                                            & "AND                                                               " & vbNewLine _
                                            & "CUST_CD_M = @CUST_CD_M                                            " & vbNewLine _
                                            & "AND                                                               " & vbNewLine _
                                            & "M_GOODS.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                            & "GROUP BY                                                          " & vbNewLine _
                                            & "NRS_BR_CD                                                         " & vbNewLine _
                                            & ",GOODS_CD_CUST                                                    " & vbNewLine _
                                            & "--,STD_IRIME_NB                                                   " & vbNewLine _
                                            & ") GOODS_CNT                                                       " & vbNewLine _
                                            & "ON                                                                " & vbNewLine _
                                            & "NIK.NRS_BR_CD = GOODS_CNT.NRS_BR_CD                               " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & "SUBSTRING( NIK.GOODS_CD_NIK,4,6) = GOODS_CNT.GOODS_CD_CUST        " & vbNewLine






    ''' <summary>
    ''' 並び順
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                   " & vbNewLine _
                                         & "    NIK.SYS_UPD_DATE      " & vbNewLine _
                                         & "   ,NIK.SYS_UPD_TIME          " & vbNewLine _
                                         & "   ,NIK.GOODS_CD_NIK        " & vbNewLine

#End Region


#End Region

#Region "保存処理 SQL"

#Region "チェック"



    ''' <summary>
    ''' 商品マスタ重複チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_REPEAT_GOODSM As String = "SELECT                                                         " & vbNewLine _
                                              & "       COUNT(GDS.GOODS_CD_NRS)     AS    SELECT_CNT            " & vbNewLine _
                                              & "FROM                                                           " & vbNewLine _
                                              & "     $LM_MST$..M_GOODS    GDS                                  " & vbNewLine _
                                              & "WHERE                                                          " & vbNewLine _
                                              & "       GDS.NRS_BR_CD          =    @NRS_BR_CD                  " & vbNewLine _
                                              & "AND    GDS.GOODS_CD_NRS       =    @GOODS_CD_NRS               " & vbNewLine


    ''' <summary>
    ''' 商品マスタ重複チェック処理(荷主コード、商品コード関連チェック(件数取得))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_REPEAT_GOODSM_CUST As String = "SELECT                                                         " & vbNewLine _
                                                   & "       COUNT(GDS.GOODS_CD_NRS)     AS    SELECT_CNT            " & vbNewLine _
                                                   & "FROM                                                           " & vbNewLine _
                                                   & "     $LM_MST$..M_GOODS    GDS                                  " & vbNewLine _
                                                   & "WHERE                                                          " & vbNewLine _
                                                   & "       GDS.NRS_BR_CD          =    @NRS_BR_CD                  " & vbNewLine _
                                                   & "AND    GDS.CUST_CD_L          =    @CUST_CD_L                  " & vbNewLine _
                                                   & "AND    GDS.CUST_CD_M          =    @CUST_CD_M                  " & vbNewLine _
                                                   & "AND    GDS.CUST_CD_S          =    @CUST_CD_S                  " & vbNewLine _
                                                   & "AND    GDS.CUST_CD_SS         =    @CUST_CD_SS                 " & vbNewLine _
                                                   & "AND    GDS.GOODS_CD_CUST      =    @GOODS_CD_CUST              " & vbNewLine


#End Region

#Region "新規登録(商品マスタ)"

    ''' <summary>
    ''' 商品マスタ新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_GOODS_M As String = "INSERT INTO                      " & vbNewLine _
                                                 & "    $LM_MST$..M_GOODS         " & vbNewLine _
                                                 & "    (                         " & vbNewLine _
                                                 & "      NRS_BR_CD               " & vbNewLine _
                                                 & "     ,GOODS_CD_NRS            " & vbNewLine _
                                                 & "     ,CUST_CD_L               " & vbNewLine _
                                                 & "     ,CUST_CD_M               " & vbNewLine _
                                                 & "     ,CUST_CD_S               " & vbNewLine _
                                                 & "     ,CUST_CD_SS              " & vbNewLine _
                                                 & "     ,GOODS_CD_CUST           " & vbNewLine _
                                                 & "     ,SEARCH_KEY_1            " & vbNewLine _
                                                 & "     ,SEARCH_KEY_2            " & vbNewLine _
                                                 & "     ,CUST_COST_CD1           " & vbNewLine _
                                                 & "     ,CUST_COST_CD2           " & vbNewLine _
                                                 & "     ,JAN_CD                  " & vbNewLine _
                                                 & "     ,GOODS_NM_1              " & vbNewLine _
                                                 & "     ,GOODS_NM_2              " & vbNewLine _
                                                 & "     ,GOODS_NM_3              " & vbNewLine _
                                                 & "     ,UP_GP_CD_1              " & vbNewLine _
                                                 & "     ,SHOBO_CD                " & vbNewLine _
                                                 & "     ,KIKEN_KB                " & vbNewLine _
                                                 & "     ,UN                      " & vbNewLine _
                                                 & "     ,PG_KB                   " & vbNewLine _
                                                 & "     ,CLASS_1                 " & vbNewLine _
                                                 & "     ,CLASS_2                 " & vbNewLine _
                                                 & "     ,CLASS_3                 " & vbNewLine _
                                                 & "     ,CHEM_MTRL_KB            " & vbNewLine _
                                                 & "     ,DOKU_KB                 " & vbNewLine _
                                                 & "     ,GAS_KANRI_KB            " & vbNewLine _
                                                 & "     ,ONDO_KB                 " & vbNewLine _
                                                 & "     ,UNSO_ONDO_KB            " & vbNewLine _
                                                 & "     ,ONDO_MX                 " & vbNewLine _
                                                 & "     ,ONDO_MM                 " & vbNewLine _
                                                 & "     ,ONDO_STR_DATE           " & vbNewLine _
                                                 & "     ,ONDO_END_DATE           " & vbNewLine _
                                                 & "     ,ONDO_UNSO_STR_DATE      " & vbNewLine _
                                                 & "     ,ONDO_UNSO_END_DATE      " & vbNewLine _
                                                 & "     ,KYOKAI_GOODS_KB         " & vbNewLine _
                                                 & "     ,ALCTD_KB                " & vbNewLine _
                                                 & "     ,NB_UT                   " & vbNewLine _
                                                 & "     ,PKG_NB                  " & vbNewLine _
                                                 & "     ,PKG_UT                  " & vbNewLine _
                                                 & "     ,PLT_PER_PKG_UT          " & vbNewLine _
                                                 & "     ,STD_IRIME_NB            " & vbNewLine _
                                                 & "     ,STD_IRIME_UT            " & vbNewLine _
                                                 & "     ,STD_WT_KGS              " & vbNewLine _
                                                 & "     ,STD_CBM                 " & vbNewLine _
                                                 & "     ,INKA_KAKO_SAGYO_KB_1    " & vbNewLine _
                                                 & "     ,INKA_KAKO_SAGYO_KB_2    " & vbNewLine _
                                                 & "     ,INKA_KAKO_SAGYO_KB_3    " & vbNewLine _
                                                 & "     ,INKA_KAKO_SAGYO_KB_4    " & vbNewLine _
                                                 & "     ,INKA_KAKO_SAGYO_KB_5    " & vbNewLine _
                                                 & "     ,OUTKA_KAKO_SAGYO_KB_1   " & vbNewLine _
                                                 & "     ,OUTKA_KAKO_SAGYO_KB_2   " & vbNewLine _
                                                 & "     ,OUTKA_KAKO_SAGYO_KB_3   " & vbNewLine _
                                                 & "     ,OUTKA_KAKO_SAGYO_KB_4   " & vbNewLine _
                                                 & "     ,OUTKA_KAKO_SAGYO_KB_5   " & vbNewLine _
                                                 & "     ,PKG_SAGYO               " & vbNewLine _
                                                 & "     ,TARE_YN                 " & vbNewLine _
                                                 & "     ,SP_NHS_YN               " & vbNewLine _
                                                 & "     ,COA_YN                  " & vbNewLine _
                                                 & "     ,LOT_CTL_KB              " & vbNewLine _
                                                 & "     ,LT_DATE_CTL_KB          " & vbNewLine _
                                                 & "     ,CRT_DATE_CTL_KB         " & vbNewLine _
                                                 & "     ,DEF_SPD_KB              " & vbNewLine _
                                                 & "     ,KITAKU_AM_UT_KB         " & vbNewLine _
                                                 & "     ,KITAKU_GOODS_UP         " & vbNewLine _
                                                 & "     ,ORDER_KB                " & vbNewLine _
                                                 & "     ,ORDER_NB                " & vbNewLine _
                                                 & "     ,SHIP_CD_L               " & vbNewLine _
                                                 & "     ,SKYU_MEI_YN             " & vbNewLine _
                                                 & "     ,HIKIATE_ALERT_YN        " & vbNewLine _
                                                 & "     ,OUTKA_ATT               " & vbNewLine _
                                                 & "     ,PRINT_NB                " & vbNewLine _
                                                 & "     ,CONSUME_PERIOD_DATE     " & vbNewLine _
                                                 & "     ,SYS_ENT_DATE            " & vbNewLine _
                                                 & "     ,SYS_ENT_TIME            " & vbNewLine _
                                                 & "     ,SYS_ENT_PGID            " & vbNewLine _
                                                 & "     ,SYS_ENT_USER            " & vbNewLine _
                                                 & "     ,SYS_UPD_DATE            " & vbNewLine _
                                                 & "     ,SYS_UPD_TIME            " & vbNewLine _
                                                 & "     ,SYS_UPD_PGID            " & vbNewLine _
                                                 & "     ,SYS_UPD_USER            " & vbNewLine _
                                                 & "     ,SYS_DEL_FLG             " & vbNewLine _
                                                 & "     ,SIZE_KB                 " & vbNewLine _
                                                 & "    )                         " & vbNewLine _
                                                 & "VALUES                        " & vbNewLine _
                                                 & "    (                         " & vbNewLine _
                                                 & "      @NRS_BR_CD              " & vbNewLine _
                                                 & "     ,@GOODS_CD_NRS           " & vbNewLine _
                                                 & "     ,@CUST_CD_L              " & vbNewLine _
                                                 & "     ,@CUST_CD_M              " & vbNewLine _
                                                 & "     ,@CUST_CD_S              " & vbNewLine _
                                                 & "     ,@CUST_CD_SS             " & vbNewLine _
                                                 & "     ,@GOODS_CD_CUST          " & vbNewLine _
                                                 & "     ,@SEARCH_KEY_1           " & vbNewLine _
                                                 & "     ,@SEARCH_KEY_2           " & vbNewLine _
                                                 & "     ,@CUST_COST_CD1          " & vbNewLine _
                                                 & "     ,@CUST_COST_CD2          " & vbNewLine _
                                                 & "     ,@JAN_CD                 " & vbNewLine _
                                                 & "     ,@GOODS_NM_1             " & vbNewLine _
                                                 & "     ,@GOODS_NM_2             " & vbNewLine _
                                                 & "     ,@GOODS_NM_3             " & vbNewLine _
                                                 & "     ,@UP_GP_CD_1             " & vbNewLine _
                                                 & "     ,@SHOBO_CD               " & vbNewLine _
                                                 & "     ,@KIKEN_KB               " & vbNewLine _
                                                 & "     ,@UN                     " & vbNewLine _
                                                 & "     ,@PG_KB                  " & vbNewLine _
                                                 & "     ,@CLASS_1                " & vbNewLine _
                                                 & "     ,@CLASS_2                " & vbNewLine _
                                                 & "     ,@CLASS_3                " & vbNewLine _
                                                 & "     ,@CHEM_MTRL_KB           " & vbNewLine _
                                                 & "     ,@DOKU_KB                " & vbNewLine _
                                                 & "     ,@GAS_KANRI_KB           " & vbNewLine _
                                                 & "     ,@ONDO_KB                " & vbNewLine _
                                                 & "     ,@UNSO_ONDO_KB           " & vbNewLine _
                                                 & "     ,@ONDO_MX                " & vbNewLine _
                                                 & "     ,@ONDO_MM                " & vbNewLine _
                                                 & "     ,@ONDO_STR_DATE          " & vbNewLine _
                                                 & "     ,@ONDO_END_DATE          " & vbNewLine _
                                                 & "     ,@ONDO_UNSO_STR_DATE     " & vbNewLine _
                                                 & "     ,@ONDO_UNSO_END_DATE     " & vbNewLine _
                                                 & "     ,@KYOKAI_GOODS_KB        " & vbNewLine _
                                                 & "     ,@ALCTD_KB               " & vbNewLine _
                                                 & "     ,@NB_UT                  " & vbNewLine _
                                                 & "     ,@PKG_NB                 " & vbNewLine _
                                                 & "     ,@PKG_UT                 " & vbNewLine _
                                                 & "     ,@PLT_PER_PKG_UT         " & vbNewLine _
                                                 & "     ,@STD_IRIME_NB           " & vbNewLine _
                                                 & "     ,@STD_IRIME_UT           " & vbNewLine _
                                                 & "     ,@STD_WT_KGS             " & vbNewLine _
                                                 & "     ,@STD_CBM                " & vbNewLine _
                                                 & "     ,@INKA_KAKO_SAGYO_KB_1   " & vbNewLine _
                                                 & "     ,@INKA_KAKO_SAGYO_KB_2   " & vbNewLine _
                                                 & "     ,@INKA_KAKO_SAGYO_KB_3   " & vbNewLine _
                                                 & "     ,@INKA_KAKO_SAGYO_KB_4   " & vbNewLine _
                                                 & "     ,@INKA_KAKO_SAGYO_KB_5   " & vbNewLine _
                                                 & "     ,@OUTKA_KAKO_SAGYO_KB_1  " & vbNewLine _
                                                 & "     ,@OUTKA_KAKO_SAGYO_KB_2  " & vbNewLine _
                                                 & "     ,@OUTKA_KAKO_SAGYO_KB_3  " & vbNewLine _
                                                 & "     ,@OUTKA_KAKO_SAGYO_KB_4  " & vbNewLine _
                                                 & "     ,@OUTKA_KAKO_SAGYO_KB_5  " & vbNewLine _
                                                 & "     ,@PKG_SAGYO              " & vbNewLine _
                                                 & "     ,@TARE_YN                " & vbNewLine _
                                                 & "     ,@SP_NHS_YN              " & vbNewLine _
                                                 & "     ,@COA_YN                 " & vbNewLine _
                                                 & "     ,@LOT_CTL_KB             " & vbNewLine _
                                                 & "     ,@LT_DATE_CTL_KB         " & vbNewLine _
                                                 & "     ,@CRT_DATE_CTL_KB        " & vbNewLine _
                                                 & "     ,@DEF_SPD_KB             " & vbNewLine _
                                                 & "     ,@KITAKU_AM_UT_KB        " & vbNewLine _
                                                 & "     ,@KITAKU_GOODS_UP        " & vbNewLine _
                                                 & "     ,@ORDER_KB               " & vbNewLine _
                                                 & "     ,@ORDER_NB               " & vbNewLine _
                                                 & "     ,@SHIP_CD_L              " & vbNewLine _
                                                 & "     ,@SKYU_MEI_YN            " & vbNewLine _
                                                 & "     ,@HIKIATE_ALERT_YN       " & vbNewLine _
                                                 & "     ,@OUTKA_ATT              " & vbNewLine _
                                                 & "     ,@PRINT_NB               " & vbNewLine _
                                                 & "     ,@CONSUME_PERIOD_DATE    " & vbNewLine _
                                                 & "     ,@SYS_ENT_DATE           " & vbNewLine _
                                                 & "     ,@SYS_ENT_TIME           " & vbNewLine _
                                                 & "     ,@SYS_ENT_PGID           " & vbNewLine _
                                                 & "     ,@SYS_ENT_USER           " & vbNewLine _
                                                 & "     ,@SYS_UPD_DATE           " & vbNewLine _
                                                 & "     ,@SYS_UPD_TIME           " & vbNewLine _
                                                 & "     ,@SYS_UPD_PGID           " & vbNewLine _
                                                 & "     ,@SYS_UPD_USER           " & vbNewLine _
                                                 & "     ,@SYS_DEL_FLG            " & vbNewLine _
                                                 & "     ,@SIZE_KB                " & vbNewLine _
                                                 & "    )                         " & vbNewLine

#End Region

    '要望番号:1250 terakawa 2012.07.12 Start
#Region "新規登録（商品明細マスタ)"

    ''' <summary>
    ''' 商品マスタ新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_GOODS_DETAIL_M As String = "    INSERT INTO               " & vbNewLine _
                                                 & "    $LM_MST$..M_GOODS_DETAILS " & vbNewLine _
                                                 & "    (                         " & vbNewLine _
                                                 & "     NRS_BR_CD                " & vbNewLine _
                                                 & "    ,GOODS_CD_NRS             " & vbNewLine _
                                                 & "    ,GOODS_CD_NRS_EDA         " & vbNewLine _
                                                 & "    ,SUB_KB                   " & vbNewLine _
                                                 & "    ,SET_NAIYO                " & vbNewLine _
                                                 & "    ,REMARK                   " & vbNewLine _
                                                 & "    ,SYS_ENT_DATE             " & vbNewLine _
                                                 & "    ,SYS_ENT_TIME             " & vbNewLine _
                                                 & "    ,SYS_ENT_PGID             " & vbNewLine _
                                                 & "    ,SYS_ENT_USER             " & vbNewLine _
                                                 & "    ,SYS_UPD_DATE             " & vbNewLine _
                                                 & "    ,SYS_UPD_TIME             " & vbNewLine _
                                                 & "    ,SYS_UPD_PGID             " & vbNewLine _
                                                 & "    ,SYS_UPD_USER             " & vbNewLine _
                                                 & "    ,SYS_DEL_FLG              " & vbNewLine _
                                                 & "    )                         " & vbNewLine _
                                                 & "VALUES                        " & vbNewLine _
                                                 & "    (                         " & vbNewLine _
                                                 & "    @NRS_BR_CD                " & vbNewLine _
                                                 & "    ,@GOODS_CD_NRS            " & vbNewLine _
                                                 & "    ,@GOODS_CD_NRS_EDA        " & vbNewLine _
                                                 & "    ,@SUB_KB                  " & vbNewLine _
                                                 & "    ,@SET_NAIYO               " & vbNewLine _
                                                 & "    ,@REMARK                  " & vbNewLine _
                                                 & "    ,@SYS_ENT_DATE            " & vbNewLine _
                                                 & "    ,@SYS_ENT_TIME            " & vbNewLine _
                                                 & "    ,@SYS_ENT_PGID            " & vbNewLine _
                                                 & "    ,@SYS_ENT_USER            " & vbNewLine _
                                                 & "    ,@SYS_UPD_DATE            " & vbNewLine _
                                                 & "    ,@SYS_UPD_TIME            " & vbNewLine _
                                                 & "    ,@SYS_UPD_PGID            " & vbNewLine _
                                                 & "    ,@SYS_UPD_USER            " & vbNewLine _
                                                 & "    ,@SYS_DEL_FLG             " & vbNewLine _
                                                 & "    )                         " & vbNewLine


#End Region
    '要望番号:1250 terakawa 2012.07.12 End

#Region "更新"

    ''' <summary>
    ''' 商品マスタ更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_GOODS_M As String = "UPDATE                                                       " & vbNewLine _
                                               & "    $LM_MST$..M_GOODS                                      " & vbNewLine _
                                               & "SET                                                        " & vbNewLine _
                                               & "      --NRS_BR_CD               =    @NRS_BR_CD              " & vbNewLine _
                                               & "     --,GOODS_CD_NRS            =    @GOODS_CD_NRS           " & vbNewLine _
                                               & "     --,CUST_CD_L               =    @CUST_CD_L              " & vbNewLine _
                                               & "     --,CUST_CD_M               =    @CUST_CD_M              " & vbNewLine _
                                               & "     --,CUST_CD_S               =    @CUST_CD_S              " & vbNewLine _
                                               & "     --,CUST_CD_SS              =    @CUST_CD_SS             " & vbNewLine _
                                               & "     --,GOODS_CD_CUST           =    @GOODS_CD_CUST          " & vbNewLine _
                                               & "     --,SEARCH_KEY_1            =    @SEARCH_KEY_1           " & vbNewLine _
                                               & "     --,SEARCH_KEY_2            =    @SEARCH_KEY_2           " & vbNewLine _
                                               & "     --,CUST_COST_CD1           =    @CUST_COST_CD1          " & vbNewLine _
                                               & "     --,CUST_COST_CD2           =    @CUST_COST_CD2          " & vbNewLine _
                                               & "     JAN_CD                  =    @JAN_CD                 " & vbNewLine _
                                               & "     ,GOODS_NM_1              =    @GOODS_NM_1             " & vbNewLine _
                                               & "     ,GOODS_NM_2              =    @GOODS_NM_2             " & vbNewLine _
                                               & "     ,GOODS_NM_3              =    @GOODS_NM_3             " & vbNewLine _
                                               & "     --,UP_GP_CD_1              =    @UP_GP_CD_1               " & vbNewLine _
                                               & "     --,SHOBO_CD                =    @SHOBO_CD               " & vbNewLine _
                                               & "     --,KIKEN_KB                =    @KIKEN_KB               " & vbNewLine _
                                               & "     --,UN                      =    @UN                     " & vbNewLine _
                                               & "     --,PG_KB                   =    @PG_KB                  " & vbNewLine _
                                               & "     --,CLASS_1                 =    @CLASS_1                " & vbNewLine _
                                               & "     --,CLASS_2                 =    @CLASS_2                " & vbNewLine _
                                               & "     --,CLASS_3                 =    @CLASS_3                " & vbNewLine _
                                               & "     --,CHEM_MTRL_KB            =    @CHEM_MTRL_KB           " & vbNewLine _
                                               & "     --,DOKU_KB                 =    @DOKU_KB                " & vbNewLine _
                                               & "     --,GAS_KANRI_KB            =    @GAS_KANRI_KB           " & vbNewLine _
                                               & "     --,ONDO_KB                 =    @ONDO_KB                " & vbNewLine _
                                               & "     --,UNSO_ONDO_KB            =    @UNSO_ONDO_KB           " & vbNewLine _
                                               & "     --,ONDO_MX                 =    @ONDO_MX                " & vbNewLine _
                                               & "     --,ONDO_MM                 =    @ONDO_MM                " & vbNewLine _
                                               & "     --,ONDO_STR_DATE           =    @ONDO_STR_DATE          " & vbNewLine _
                                               & "     --,ONDO_END_DATE           =    @ONDO_END_DATE          " & vbNewLine _
                                               & "     --,ONDO_UNSO_STR_DATE      =    @ONDO_UNSO_STR_DATE     " & vbNewLine _
                                               & "     --,ONDO_UNSO_END_DATE      =    @ONDO_UNSO_END_DATE     " & vbNewLine _
                                               & "     --,KYOKAI_GOODS_KB         =    @KYOKAI_GOODS_KB        " & vbNewLine _
                                               & "     --,ALCTD_KB                =    @ALCTD_KB               " & vbNewLine _
                                               & "     --,NB_UT                   =    @NB_UT                  " & vbNewLine _
                                               & "     --,PKG_NB                  =    @PKG_NB                 " & vbNewLine _
                                               & "     --,PKG_UT                  =    @PKG_UT                 " & vbNewLine _
                                               & "     --,PLT_PER_PKG_UT          =    @PLT_PER_PKG_UT         " & vbNewLine _
                                               & "     --,STD_IRIME_NB            =    @STD_IRIME_NB           " & vbNewLine _
                                               & "     --,STD_IRIME_UT            =    @STD_IRIME_UT           " & vbNewLine _
                                               & "     --,STD_WT_KGS              =    @STD_WT_KGS             " & vbNewLine _
                                               & "     --,STD_CBM                 =    @STD_CBM                " & vbNewLine _
                                               & "     --,INKA_KAKO_SAGYO_KB_1    =    @INKA_KAKO_SAGYO_KB_1   " & vbNewLine _
                                               & "     --,INKA_KAKO_SAGYO_KB_2    =    @INKA_KAKO_SAGYO_KB_2   " & vbNewLine _
                                               & "     --,INKA_KAKO_SAGYO_KB_3    =    @INKA_KAKO_SAGYO_KB_3   " & vbNewLine _
                                               & "     --,INKA_KAKO_SAGYO_KB_4    =    @INKA_KAKO_SAGYO_KB_4   " & vbNewLine _
                                               & "     --,INKA_KAKO_SAGYO_KB_5    =    @INKA_KAKO_SAGYO_KB_5   " & vbNewLine _
                                               & "     --,OUTKA_KAKO_SAGYO_KB_1   =    @OUTKA_KAKO_SAGYO_KB_1  " & vbNewLine _
                                               & "     --,OUTKA_KAKO_SAGYO_KB_2   =    @OUTKA_KAKO_SAGYO_KB_2  " & vbNewLine _
                                               & "     --,OUTKA_KAKO_SAGYO_KB_3   =    @OUTKA_KAKO_SAGYO_KB_3  " & vbNewLine _
                                               & "     --,OUTKA_KAKO_SAGYO_KB_4   =    @OUTKA_KAKO_SAGYO_KB_4  " & vbNewLine _
                                               & "     --,OUTKA_KAKO_SAGYO_KB_5   =    @OUTKA_KAKO_SAGYO_KB_5  " & vbNewLine _
                                               & "     --,PKG_SAGYO               =    @PKG_SAGYO              " & vbNewLine _
                                               & "     --,TARE_YN                 =    @TARE_YN                " & vbNewLine _
                                               & "     --,SP_NHS_YN               =    @SP_NHS_YN              " & vbNewLine _
                                               & "     --,COA_YN                  =    @COA_YN                 " & vbNewLine _
                                               & "     --,LOT_CTL_KB              =    @LOT_CTL_KB             " & vbNewLine _
                                               & "     --,LT_DATE_CTL_KB          =    @LT_DATE_CTL_KB         " & vbNewLine _
                                               & "     --,CRT_DATE_CTL_KB         =    @CRT_DATE_CTL_KB        " & vbNewLine _
                                               & "     --,DEF_SPD_KB              =    @DEF_SPD_KB             " & vbNewLine _
                                               & "     --,KITAKU_AM_UT_KB         =    @KITAKU_AM_UT_KB        " & vbNewLine _
                                               & "     --,KITAKU_GOODS_UP         =    @KITAKU_GOODS_UP        " & vbNewLine _
                                               & "     --,ORDER_KB                =    @ORDER_KB               " & vbNewLine _
                                               & "     --,ORDER_NB                =    @ORDER_NB               " & vbNewLine _
                                               & "     --,SHIP_CD_L               =    @SHIP_CD_L              " & vbNewLine _
                                               & "     --,SKYU_MEI_YN             =    @SKYU_MEI_YN            " & vbNewLine _
                                               & "     --,HIKIATE_ALERT_YN        =    @HIKIATE_ALERT_YN       " & vbNewLine _
                                               & "     --,OUTKA_ATT               =    @OUTKA_ATT              " & vbNewLine _
                                               & "     --,PRINT_NB                =    @PRINT_NB               " & vbNewLine _
                                               & "     --,CONSUME_PERIOD_DATE     =    @CONSUME_PERIOD_DATE    " & vbNewLine _
                                               & "     ,SYS_UPD_DATE            =    @SYS_UPD_DATE           " & vbNewLine _
                                               & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME           " & vbNewLine _
                                               & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID           " & vbNewLine _
                                               & "     ,SYS_UPD_USER            =    @SYS_UPD_USER           " & vbNewLine _
                                               & "     --,SIZE_KB                 =    @SIZE_KB                " & vbNewLine _
                                               & "WHERE NRS_BR_CD               =    @NRS_BR_CD              " & vbNewLine _
                                               & "AND   GOODS_CD_NRS            =    @GOODS_CD_NRS           " & vbNewLine _
                                               & "AND   SYS_UPD_DATE            =    @HAITA_DATE             " & vbNewLine _
                                               & "AND   SYS_UPD_TIME            =    @HAITA_TIME             " & vbNewLine


    ''' <summary>
    ''' 商品明細マスタ物理削除SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_GOODS_DTL As String = "DELETE FROM $LM_MST$..M_GOODS_DETAILS    " & vbNewLine _
                                                 & "WHERE   NRS_BR_CD    = @NRS_BR_CD        " & vbNewLine _
                                                 & "AND     GOODS_CD_NRS = @GOODS_CD_NRS     " & vbNewLine
#End Region

#Region "SQL_UpdateM_SEHIN_NIK"

    Private Const SQL_UpdateM_SEHIN_NIK As String = "  UPDATE $LM_TRN$..M_SEHIN_NIK SET         " & vbNewLine _
                                                & "     M_GOODS_UPD_FLG = '01'                  " & vbNewLine _
                                                & "    ,SYS_UPD_DATE = @SYS_UPD_DATE            " & vbNewLine _
                                                & "    ,SYS_UPD_TIME = @SYS_UPD_TIME            " & vbNewLine _
                                                & "    ,SYS_UPD_PGID = @SYS_UPD_PGID            " & vbNewLine _
                                                & "    ,SYS_UPD_USER = @SYS_UPD_USER            " & vbNewLine _
                                                & "  WHERE                                      " & vbNewLine _
                                                & "    NRS_BR_CD = @NRS_BR_CD                   " & vbNewLine _
                                                & "    AND MAKER_CD = @MAKER_CD                 " & vbNewLine _
                                                & "    AND GOODS_CD_NIK = @GOODS_CD_NIK         " & vbNewLine _
                                                & "    AND SYS_UPD_DATE = @HAITA_DATE           " & vbNewLine _
                                                & "    AND SYS_UPD_TIME = @HAITA_TIME           " & vbNewLine

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
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 商品マスタ検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI110INOUT_M_SEIHIN_NIK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI110DAC.SQL_SELECT_COUNT_SELECT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMI110DAC.SQL_SELECT_DATA_FROM)        'SQL構築(カウント用FROM句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Call Me.SetParamCust()                            'パラメータ情報設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI110DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 商品マスタ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI110INOUT_M_SEIHIN_NIK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI110DAC.SQL_SELECT_DATA_SELECT)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMI110DAC.SQL_SELECT_DATA_FROM)        'SQL構築(データ抽出用FROM句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMI110DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)
        Call Me.SetParamCust()                            'パラメータ情報設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI110DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("STATUS", "STATUS")
        map.Add("M_GOODS_UPD_FLG", "M_GOODS_UPD_FLG")
        map.Add("TORIKOMI_KBN", "TORIKOMI_KBN")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_NIK", "GOODS_CD_NIK")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("GOODS_NM_KANA", "GOODS_NM_KANA")
        map.Add("GOODS_KIKAKU", "GOODS_KIKAKU")
        map.Add("GOODS_KIKAKU_KANA", "GOODS_KIKAKU_KANA")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("KANRI_KB", "KANRI_KB")
        map.Add("KANRI_KB_NM", "KANRI_KB_NM")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_KB_NM", "ONDO_KB_NM")
        map.Add("YUKO_MONTH", "YUKO_MONTH")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("NB_WT_GS", "NB_WT_GS")
        map.Add("NB_FORM_LENGTH", "NB_FORM_LENGTH")
        map.Add("NB_FORM_WIDTH", "NB_FORM_WIDTH")
        map.Add("NB_FORM_HIGHT", "NB_FORM_HIGHT")
        map.Add("PKG_FORM_LENGTH", "PKG_FORM_LENGTH")
        map.Add("PKG_FORM_WIDTH", "PKG_FORM_WIDTH")
        map.Add("PKG_FORM_HIGHT", "PKG_FORM_HIGHT")
        map.Add("PKG_WT_GS", "PKG_WT_GS")
        map.Add("TEKIYO_DATE", "TEKIYO_DATE")
        map.Add("GOODS_NM_RYAKU", "GOODS_NM_RYAKU")
        map.Add("ITF_CD", "ITF_CD")
        map.Add("SIIRE_CD", "SIIRE_CD")
        map.Add("NB_ML", "NB_ML")
        map.Add("PKG_ML", "PKG_ML")
        map.Add("PLT_PER_PKG_UT", "PLT_PER_PKG_UT")
        map.Add("SURFACE_PKG_NB", "SURFACE_PKG_NB")
        map.Add("SURFACE_NUM_ROW", "SURFACE_NUM_ROW")
        map.Add("M_GOODS_ONDO_KB", "M_GOODS_ONDO_KB")
        map.Add("M_GOODS_STD_IRIME_NB", "M_GOODS_STD_IRIME_NB")
        map.Add("M_GOODS_PKG_NB", "M_GOODS_PKG_NB")
        map.Add("M_GOODS_CNT", "M_GOODS_CNT")
        map.Add("M_SEIHIN_SYS_UPD_DATE", "M_SEIHIN_SYS_UPD_DATE")
        map.Add("M_SEIHIN_SYS_UPD_TIME", "M_SEIHIN_SYS_UPD_TIME")
        map.Add("M_GOODS_SYS_UPD_DATE", "M_GOODS_SYS_UPD_DATE")
        map.Add("M_GOODS_SYS_UPD_TIME", "M_GOODS_SYS_UPD_TIME")
        map.Add("M_SEIHIN_SYS_DEL_FLAG", "M_SEIHIN_SYS_DEL_FLAG")
        map.Add("M_GOODS_SYS_DEL_FLAG", "M_GOODS_SYS_DEL_FLAG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI110OUT_M_SEIHIN_NIK")

        Return ds

    End Function


    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()

        With Me._Row

            '【EDI取込日(FROM)：<=】
            whereStr = .Item("TORIKOMI_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  NIK.SYS_ENT_DATE >= @TORIKOMI_DATE_FROM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TORIKOMI_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '【EDI取込日(TO)：>=】
            whereStr = .Item("TORIKOMI_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  NIK.SYS_ENT_DATE <= @TORIKOMI_DATE_TO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TORIKOMI_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '【取込区分：=】
            whereStr = .Item("TORIKOMI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  NIK.M_GOODS_UPD_FLG = @TORIKOMI_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TORIKOMI_KB", whereStr, DBDataType.CHAR))
            End If

            '【営業所：=】
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" NIK.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            ''【荷主コード（大）：=】
            'whereStr = .Item("CUST_CD_L").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    If andstr.Length <> 0 Then
            '        andstr.Append("AND")
            '    End If
            '    andstr.Append(" M_GOODS.CUST_CD_L = @CUST_CD_L")
            '    andstr.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            'End If

            ''【荷主コード（中）：=】
            'whereStr = .Item("CUST_CD_M").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    If andstr.Length <> 0 Then
            '        andstr.Append("AND")
            '    End If
            '    andstr.Append(" M_GOODS.CUST_CD_M = @CUST_CD_M")
            '    andstr.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            'End If

            '要望番号1082 terakawa 20120528 Start
            '【製品コード：LIKE %値%】
            whereStr = .Item("GOODS_CD_NIK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  NIK.GOODS_CD_NIK LIKE @GOODS_CD_NIK")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NIK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If
            '要望番号1082 terakawa 20120528 End

            '【製品名(漢字)：LIKE %値%】
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  NIK.GOODS_NM LIKE @GOODS_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【製品名(カナ)：LIKE %値%】
            whereStr = .Item("GOODS_NM_KANA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  NIK.GOODS_NM_KANA LIKE @GOODS_NM_KANA")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_KANA", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【製品名規格(漢字)：LIKE %値%】
            whereStr = .Item("GOODS_KIKAKU").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  NIK.GOODS_KIKAKU LIKE @GOODS_KIKAKU")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_KIKAKU", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【製品名規格(カナ)：LIKE %値%】
            whereStr = .Item("GOODS_KIKAKU_KANA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  NIK.GOODS_KIKAKU_KANA LIKE @GOODS_KIKAKU_KANA")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_KIKAKU_KANA", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【JANコード：LIKE 値%】
            whereStr = .Item("JAN_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  NIK.JAN_CD LIKE @JAN_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JAN_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【管理区分：=】
            whereStr = .Item("KANRI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  NIK.KANRI_KB = @KANRI_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_KB", whereStr, DBDataType.CHAR))
            End If

            '【保管温度区分：=】
            whereStr = .Item("ONDO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  NIK.ONDO_KB = @ONDO_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_KB", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterDtlSQL(ByVal dt As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()

        With Me._Row

            '【営業所コード：=】
            whereStr = .Item("NRS_BR_CD").ToString()
            andstr.Append("  DTL.NRS_BR_CD = @NRS_BR_CD")
            andstr.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '【営業所KEY：IN】
            Dim brKey As String = String.Empty
            Dim max As Integer = dt.Rows.Count - 1
            For i As Integer = 0 To max
                With dt.Rows(i)
                    If String.IsNullOrEmpty(brKey) Then
                        brKey = String.Concat("'", .Item("GOODS_CD_NRS").ToString, "'")
                    Else
                        brKey = String.Concat(brKey, ",", "'", .Item("GOODS_CD_NRS").ToString, "'")
                    End If
                End With
            Next
            brKey = String.Concat("(", brKey, ")")
            andstr.Append(" AND  DTL.GOODS_CD_NRS IN ")
            andstr.Append(brKey)
            andstr.Append(vbNewLine)

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If

        End With

    End Sub

#End Region

#Region "保存処理"

#Region "チェック"


    ''' <summary>
    ''' 商品マスタ重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistGoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("M_GOODS_HANEI")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        Dim rowNo As String = Me._Row.Item("ROW_NO").ToString()
        Dim seihinCd As String = Me._Row.Item("GOODS_CD_CUST").ToString()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI110DAC.SQL_REPEAT_GOODSM)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamGoodsMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI110DAC", "ExistGoodsM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '要望番号1093 terakawa 20120530 Start
        ''処理件数の設定
        'reader.Read()

        ''エラーメッセージの設定
        'If Convert.ToInt32(reader("SELECT_CNT")) > 0 Then
        '    MyBase.SetMessage("E010")
        'End If

        'reader.Close()

        '処理件数の設定
        reader.Read()
        Dim selectCount As Integer = Convert.ToInt32(reader("SELECT_CNT"))
        reader.Close()

        'エラーメッセージの設定
        If selectCount > 0 Then
            MyBase.SetMessage("E010")
            MyBase.SetMessageStore(LMI110DAC.GUIDANCE_KBN, "E010", , rowNo, LMI110DAC.EXCEL_COLTITLE, seihinCd)
        End If

        Return ds
        '要望番号1093 terakawa 20120530 End

    End Function

    ''' <summary>
    ''' 商品マスタ重複チェック(荷主コード、商品コード関連チェック)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistGoodsMCust(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI110OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI110DAC.SQL_REPEAT_GOODSM_CUST)
        If String.IsNullOrEmpty(Me._Row.Item("GOODS_CD_NRS").ToString()) = False Then
            Me._StrSql.Append("AND    GDS.GOODS_CD_NRS       <>   @GOODS_CD_NRS              " & vbNewLine)
        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamGoodsMCustChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI110DAC", "ExistGoodsMCust", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("W134", New String() {"同じ商品コード"})
        End If

        reader.Close()

        Return ds

    End Function


    ''' <summary>
    ''' パラメータ設定モジュール(商品マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGoodsMChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '商品マスタ主キー設定
        Call Me.SetParamPrimaryKeyGoodsM()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(商品マスタ存在チェック(荷主コード、商品コード関連チェック))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGoodsMCustChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
            If String.IsNullOrEmpty(Me._Row.Item("GOODS_CD_NRS").ToString()) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            End If

        End With

    End Sub

   

#End Region

#Region "新規登録/更新"

    ''' <summary>
    ''' 商品マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertGoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("M_GOODS_HANEI")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMI110DAC.SQL_INSERT_GOODS_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsertGoods()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI110DAC", "InsertGoodsM", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

    '要望番号:1250 terakawa 2012.07.12 Start
    ''' <summary>
    ''' 商品明細マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品明細マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertGoodsMDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("M_GOODS_DETAILS")
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQL構築
            Me._StrSql.Append(LMI110DAC.SQL_INSERT_GOODS_DETAIL_M)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            'SQLパラメータ初期化/設定
            Call Me.SetParamInsertGoodsDtl()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI110DAC", "InsertGoodsMDtl", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            cmd.Parameters.Clear()

        Next

        Return ds

    End Function
    '要望番号:1250 terakawa 2012.07.12 End

    ''' <summary>
    ''' 商品マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateGoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("M_GOODS_HANEI")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        Dim rowNo As String = Me._Row.Item("ROW_NO").ToString()
        Dim seihinCd As String = Me._Row.Item("GOODS_CD_CUST").ToString()

        'SQL構築
        Me._StrSql.Append(LMI110DAC.SQL_UPDATE_GOODS_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateGoods()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI110DAC", "UpdateGoodsM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))
        If MyBase.GetResultCount < 1 Then
            MyBase.SetMessage("E011")
            MyBase.SetMessageStore(LMI110DAC.GUIDANCE_KBN, "E011", , rowNo, LMI110DAC.EXCEL_COLTITLE, seihinCd)
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function


    ''' <summary>
    ''' パラメータ設定モジュール(商品マスタ新規登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertGoods()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '商品マスタ全項目
        Call Me.SetParamGoodsM()

        '新規登録共通項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(商品明細マスタ新規登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertGoodsDtl()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_EDA", .Item("GOODS_CD_NRS_EDA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SUB_KB", .Item("SUB_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_NAIYO", .Item("SET_NAIYO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))

        End With

        '新規登録共通項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(商品マスタ更新用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateGoods()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '商品マスタ全項目
        Call Me.SetParamGoodsM()

        '排他項目
        'Call Me.SetParamHaita()
        With Me._Row
            '排他共通項目
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("M_GOODS_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("M_GOODS_SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(商品明細マスタ物理削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDeleteGoodsDtl()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(商品マスタ全項目)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGoodsM()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me.NullConvertString(.Item("GOODS_CD_NRS").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", Me.NullConvertString(.Item("CUST_CD_S").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", Me.NullConvertString(.Item("CUST_CD_SS").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", Me.NullConvertString(.Item("GOODS_CD_CUST").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_1", Me.NullConvertString(.Item("SEARCH_KEY_1").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_2", Me.NullConvertString(.Item("SEARCH_KEY_2").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_COST_CD1", Me.NullConvertString(.Item("CUST_COST_CD1").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_COST_CD2", Me.NullConvertString(.Item("CUST_COST_CD2").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JAN_CD", Me.NullConvertString(.Item("JAN_CD").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", Me.NullConvertString(.Item("GOODS_NM_1").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_2", Me.NullConvertString(.Item("GOODS_NM_2").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_3", Me.NullConvertString(.Item("GOODS_NM_3").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UP_GP_CD_1", Me.NullConvertString(.Item("UP_GP_CD_1").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHOBO_CD", Me.NullConvertString(.Item("SHOBO_CD").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIKEN_KB", Me.NullConvertString(.Item("KIKEN_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UN", Me.NullConvertString(.Item("UN").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PG_KB", Me.NullConvertString(.Item("PG_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CLASS_1", Me.NullConvertString(.Item("CLASS_1").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CLASS_2", Me.NullConvertString(.Item("CLASS_2").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CLASS_3", Me.NullConvertString(.Item("CLASS_3").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHEM_MTRL_KB", Me.NullConvertString(.Item("CHEM_MTRL_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOKU_KB", Me.NullConvertString(.Item("DOKU_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GAS_KANRI_KB", Me.NullConvertString(.Item("GAS_KANRI_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_KB", Me.NullConvertString(.Item("ONDO_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", Me.NullConvertString(.Item("UNSO_ONDO_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_MX", Me.NullConvertZero(.Item("ONDO_MX").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_MM", Me.NullConvertZero(.Item("ONDO_MM").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_STR_DATE", Me.NullConvertString(.Item("ONDO_STR_DATE").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_END_DATE", Me.NullConvertString(.Item("ONDO_END_DATE").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_UNSO_STR_DATE", Me.NullConvertString(.Item("ONDO_UNSO_STR_DATE").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_UNSO_END_DATE", Me.NullConvertString(.Item("ONDO_UNSO_END_DATE").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYOKAI_GOODS_KB", Me.NullConvertString(.Item("KYOKAI_GOODS_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", Me.NullConvertString(.Item("ALCTD_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", Me.NullConvertString(.Item("NB_UT").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.NullConvertZero(.Item("PKG_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_UT", Me.NullConvertString(.Item("PKG_UT").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PLT_PER_PKG_UT", Me.NullConvertZero(.Item("PLT_PER_PKG_UT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME_NB", Me.NullConvertZero(.Item("STD_IRIME_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME_UT", Me.NullConvertString(.Item("STD_IRIME_UT").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_WT_KGS", Me.NullConvertZero(.Item("STD_WT_KGS").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_CBM", Me.NullConvertZero(.Item("STD_CBM").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KAKO_SAGYO_KB_1", Me.NullConvertString(.Item("INKA_KAKO_SAGYO_KB_1").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KAKO_SAGYO_KB_2", Me.NullConvertString(.Item("INKA_KAKO_SAGYO_KB_2").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KAKO_SAGYO_KB_3", Me.NullConvertString(.Item("INKA_KAKO_SAGYO_KB_3").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KAKO_SAGYO_KB_4", Me.NullConvertString(.Item("INKA_KAKO_SAGYO_KB_4").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KAKO_SAGYO_KB_5", Me.NullConvertString(.Item("INKA_KAKO_SAGYO_KB_5").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KAKO_SAGYO_KB_1", Me.NullConvertString(.Item("OUTKA_KAKO_SAGYO_KB_1").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KAKO_SAGYO_KB_2", Me.NullConvertString(.Item("OUTKA_KAKO_SAGYO_KB_2").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KAKO_SAGYO_KB_3", Me.NullConvertString(.Item("OUTKA_KAKO_SAGYO_KB_3").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KAKO_SAGYO_KB_4", Me.NullConvertString(.Item("OUTKA_KAKO_SAGYO_KB_4").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KAKO_SAGYO_KB_5", Me.NullConvertString(.Item("OUTKA_KAKO_SAGYO_KB_5").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_SAGYO", Me.NullConvertString(.Item("PKG_SAGYO").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARE_YN", Me.NullConvertString(.Item("TARE_YN").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SP_NHS_YN", Me.NullConvertString(.Item("SP_NHS_YN").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_CTL_KB", Me.NullConvertString(.Item("LOT_CTL_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE_CTL_KB", Me.NullConvertString(.Item("LT_DATE_CTL_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_CTL_KB", Me.NullConvertString(.Item("CRT_DATE_CTL_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEF_SPD_KB", Me.NullConvertString(.Item("DEF_SPD_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KITAKU_AM_UT_KB", Me.NullConvertString(.Item("KITAKU_AM_UT_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KITAKU_GOODS_UP", Me.NullConvertZero(.Item("KITAKU_GOODS_UP").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_KB", Me.NullConvertString(.Item("ORDER_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_NB", Me.NullConvertZero(.Item("ORDER_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", Me.NullConvertString(.Item("SHIP_CD_L").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_MEI_YN", Me.NullConvertString(.Item("SKYU_MEI_YN").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIKIATE_ALERT_YN", Me.NullConvertString(.Item("HIKIATE_ALERT_YN").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_ATT", Me.NullConvertString(.Item("OUTKA_ATT").ToString()), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_NB", Me.NullConvertZero(.Item("PRINT_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CONSUME_PERIOD_DATE", Me.NullConvertZero(.Item("CONSUME_PERIOD_DATE").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SIZE_KB", Me.NullConvertString(.Item("SIZE_KB").ToString()), DBDataType.CHAR))

        End With

    End Sub


    ''' <summary>
    ''' 製品マスタ（日医工）更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>製品マスタ（日医工）更新SQLの構築・発行</remarks>
    Private Function UpdateSehinM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("M_GOODS_HANEI")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        Dim rowNo As String = Me._Row.Item("ROW_NO").ToString()
        Dim seihinCd As String = Me._Row.Item("GOODS_CD_CUST").ToString()

        'SQL構築
        Me._StrSql.Append(LMI110DAC.SQL_UpdateM_SEHIN_NIK)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MAKER_CD", "376", DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NIK", "376" & .Item("GOODS_CD_CUST").ToString(), DBDataType.CHAR))

            '排他共通項目
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("M_SEIHIN_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("M_SEIHIN_SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI110DAC", "UpdateSehinM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))
        If MyBase.GetResultCount < 1 Then
            MyBase.SetMessage("E011")
            MyBase.SetMessageStore(LMI110DAC.GUIDANCE_KBN, "E011", , rowNo, LMI110DAC.EXCEL_COLTITLE, seihinCd)
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function



#End Region

#End Region

#Region "共通項目"

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(新規時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(商品マスタ主キー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamPrimaryKeyGoodsM()

        With Me._Row
            '主キー
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaita()

        With Me._Row
            '排他共通項目
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub


    ''' <summary>
    ''' パラメータ設定モジュール(荷主情報)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCust()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))

        End With

    End Sub


    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所</param>
    ''' <param name="sverFlg">サーバー切り替え有無フラグTrue:有り</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String _
                                 , ByVal brCd As String _
                                 , Optional ByVal sverFlg As Boolean = False) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#Region "サーバ切り替え"

#Region "Feild"

    ''' <summary>
    ''' 区分マスタ保持用
    ''' </summary>
    ''' <remarks></remarks>
    Private _kbnDs As DataSet

    ''' <summary>
    ''' LMSVer1のコネクション
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMS1 As SqlConnection = New SqlConnection

#End Region

#Region "Const"

#Region "DB切り替え用 SQL"

    ''' <summary>
    ''' 区分マスタ情報保持用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_KBN As String = "SELECT                                    " & vbNewLine _
                                        & "  KBN_GROUP_CD   AS    KBN_GROUP_CD       " & vbNewLine _
                                        & " ,KBN_CD         AS    KBN_CD             " & vbNewLine _
                                        & " ,KBN_KEYWORD    AS    KBN_KEYWORD        " & vbNewLine _
                                        & " ,KBN_NM1        AS    KBN_NM1            " & vbNewLine _
                                        & " ,KBN_NM2        AS    KBN_NM2            " & vbNewLine _
                                        & " ,KBN_NM3        AS    KBN_NM3            " & vbNewLine _
                                        & " ,KBN_NM4        AS    KBN_NM4            " & vbNewLine _
                                        & " ,KBN_NM5        AS    KBN_NM5            " & vbNewLine _
                                        & " ,KBN_NM6        AS    KBN_NM6            " & vbNewLine _
                                        & " ,KBN_NM7        AS    KBN_NM7            " & vbNewLine _
                                        & " ,KBN_NM8        AS    KBN_NM8            " & vbNewLine _
                                        & " ,KBN_NM9        AS    KBN_NM9            " & vbNewLine _
                                        & " ,KBN_NM10       AS    KBN_NM10           " & vbNewLine _
                                        & " ,VALUE1         AS    VALUE1             " & vbNewLine _
                                        & " ,VALUE2         AS    VALUE2             " & vbNewLine _
                                        & " ,VALUE3         AS    VALUE3             " & vbNewLine _
                                        & " ,SORT           AS    SORT               " & vbNewLine _
                                        & " ,REM            AS    REM                " & vbNewLine _
                                        & "FROM                                      " & vbNewLine _
                                        & "    $LM_MST$..Z_KBN KBN                   " & vbNewLine _
                                        & "WHERE                                     " & vbNewLine _
                                        & "    KBN.SYS_DEL_FLG  = '0'                " & vbNewLine _
                                        & "AND KBN.KBN_GROUP_CD ='L001'              " & vbNewLine

#End Region

    Private Const COL_BR_CD As String = "COL_BR_CD"

    Private Const COL_IKO_FLG As String = "COL_IKO_FLG"

    Private Const COL_LMS_SV_NM As String = "COL_LMS_SV_NM"

    Private Const COL_LMS_SCHEMA_NM As String = "COL_LMS_SCHEMA_NM"

    Private Const COL_LMS2_SV_NM As String = "COL_LMS2_SV_NM"

    Private Const COL_LMS2_SCHEMA_NM As String = "COL_LMS2_SCHEMA_NM"

#End Region

#Region "LMS DB OPen/Close"

    ''' <summary>
    ''' LMSVer1のOPEN
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenConnectionLMS1(ByVal brCd As String)

        Me._LMS1.ConnectionString = Me.GetConnectionLMS1(brCd)
        Me._LMS1.Open()

    End Sub

    ''' <summary>
    '''  LMSVer1のCLOSE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseConnectionLMS1()

        Me._LMS1.Close()
        Me._LMS1.Dispose()

    End Sub

#End Region

    ''' <summary>
    ''' 区分マスタ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateKbnDataSet()

        '区分マスタ取得
        Me._kbnDs = New DataSet
        Dim dt As DataTable = New DataTable
        Me._kbnDs.Tables.Add(dt)
        Me._kbnDs.Tables(0).TableName = "Z_KBN"

        For i As Integer = 0 To 17
            Me._kbnDs.Tables("Z_KBN").Columns.Add(SetCol(i))
        Next

        '区分マスタより接続情報取得
        Me.SetConnectDataSet()

    End Sub

    ''' <summary>
    ''' 区分マスタの接続情報を取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConnectDataSet()

        'SQL格納変数の初期化
        Dim sql As StringBuilder = New StringBuilder()

        'SQL作成
        sql.Append(LMI110DAC.SQL_GET_KBN)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )


        MyBase.Logger.WriteSQLLog("LMI110DAC", "SetConnectDataSet", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("KBN_GROUP_CD", "KBN_GROUP_CD")
        map.Add("KBN_CD", "KBN_CD")
        map.Add("KBN_KEYWORD", "KBN_KEYWORD")
        map.Add("KBN_NM1", "KBN_NM1")
        map.Add("KBN_NM2", "KBN_NM2")
        map.Add("KBN_NM3", "KBN_NM3")
        map.Add("KBN_NM4", "KBN_NM4")
        map.Add("KBN_NM5", "KBN_NM5")
        map.Add("KBN_NM6", "KBN_NM6")
        map.Add("KBN_NM7", "KBN_NM7")
        map.Add("KBN_NM8", "KBN_NM8")
        map.Add("KBN_NM9", "KBN_NM9")
        map.Add("KBN_NM10", "KBN_NM10")
        map.Add("VALUE1", "VALUE1")
        map.Add("VALUE2", "VALUE2")
        map.Add("VALUE3", "VALUE3")
        map.Add("SORT", "SORT")
        map.Add("REM", "REM")

        Me._kbnDs = MyBase.SetSelectResultToDataSet(map, Me._kbnDs, reader, "Z_KBN")


    End Sub

    ''' <summary>
    ''' 区分マスタ設定
    ''' </summary>
    ''' <param name="colno"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCol(ByVal colno As Integer) As DataColumn
        Dim col As DataColumn = New DataColumn
        Dim colname As String = String.Empty
        col = New DataColumn
        Select Case colno
            Case 0
                colname = "KBN_GROUP_CD"
            Case 1
                colname = "KBN_CD"
            Case 2
                colname = "KBN_KEYWORD"
            Case 3 'KBN_NM1
                colname = "KBN_NM1"
            Case 4 'KBN_NM2
                colname = "KBN_NM2"
            Case 5 'KBN_NM3
                colname = "KBN_NM3"
            Case 6 'KBN_NM4
                colname = "KBN_NM4"
            Case 7 'KBN_NM5
                colname = "KBN_NM5"
            Case 8 'KBN_NM6
                colname = "KBN_NM6"
            Case 9 'KBN_NM7
                colname = "KBN_NM7"
            Case 10 'KBN_NM8
                colname = "KBN_NM8"
            Case 11 'KBN_NM9
                colname = "KBN_NM9"
            Case 12 'KBN_NM10
                colname = "KBN_NM10"
            Case 13
                colname = "VALUE1"
            Case 14
                colname = "VALUE2"
            Case 15
                colname = "VALUE3"
            Case 16
                colname = "SORT"
            Case 17
                colname = "REM"
        End Select

        col.ColumnName = colname
        col.Caption = colname

        Return col
    End Function

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <remarks></remarks>
    Private Function GetSchemaEDI(ByVal brCd As String) As String

        Dim rtnSchema As String = String.Empty
        Dim dataRows() As DataRow = Me._kbnDs.Tables("Z_KBN").Select("KBN_NM3 = '" & brCd & "'")
        Dim serverAcFlg As String = dataRows(0).Item("KBN_NM4").ToString

        Select Case serverAcFlg
            Case "00"
                rtnSchema = dataRows(0).Item("KBN_NM8").ToString
            Case "01"
                rtnSchema = dataRows(0).Item("KBN_NM6").ToString

        End Select

        Return rtnSchema

    End Function

    ''' <summary>
    ''' LMSVer1の接続文字列取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetConnectionLMS1(ByVal brCd As String) As String

        Dim rtnSchema As String = String.Empty
        Dim dataRows() As DataRow = Me._kbnDs.Tables("Z_KBN").Select("KBN_NM3 = '" & brCd & "'")

        Dim DBName As String = String.Empty
        Dim loginSchemaNM As String = String.Empty
        Dim userId As String = "sa"
        Dim pass As String = "as"

        DBName = dataRows(0).Item("KBN_NM7").ToString
        loginSchemaNM = dataRows(0).Item("KBN_NM8").ToString

        Return String.Concat("Data Source=", DBName, ";Initial Catalog=", loginSchemaNM, ";Persist Security Info=True;User ID=", userId, ";Password=", pass)

    End Function

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
        ElseIf String.IsNullOrEmpty(value.ToString) Then
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

#End Region

End Class
