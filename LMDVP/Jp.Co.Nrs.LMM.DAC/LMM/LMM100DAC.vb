' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM100DAC : 商品マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM100DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM100DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    Public Const GUIDANCE_KBN As String = "00"
    Public Const EXCEL_COL_TITLE As String = "荷主商品コード"


#Region "編集処理 SQL"

    ''' <summary>
    ''' 排他チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_HAITA_CHK As String = "SELECT                                                         " & vbNewLine _
                                          & "       COUNT(GDS.GOODS_CD_NRS)     AS    SELECT_CNT            " & vbNewLine _
                                          & "FROM                                                           " & vbNewLine _
                                          & "     $LM_MST$..M_GOODS    GDS                                " & vbNewLine _
                                          & "WHERE                                                          " & vbNewLine _
                                          & "       GDS.NRS_BR_CD          =    @NRS_BR_CD                  " & vbNewLine _
                                          & "AND    GDS.GOODS_CD_NRS       =    @GOODS_CD_NRS               " & vbNewLine _
                                          & "AND    GDS.SYS_UPD_DATE       =    @HAITA_DATE                 " & vbNewLine _
                                          & "AND    GDS.SYS_UPD_TIME       =    @HAITA_TIME                 " & vbNewLine

    'START YANAI 要望番号739
    '''' <summary>
    '''' 在庫データ存在チェック
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_EXIST_ZAIKO As String = "SELECT                                                  " & vbNewLine _
    '                                        & "    ISNULL(SUM(ZAI.PORA_ZAI_NB),0)   AS SELECT_CNT      " & vbNewLine _
    '                                        & "FROM                                                    " & vbNewLine _
    '                                        & "    $LM_TRN_EDI$..D_ZAI_TRS   ZAI                       " & vbNewLine _
    '                                        & "WHERE ZAI.NRS_BR_CD    = @NRS_BR_CD                     " & vbNewLine _
    '                                        & "AND   ZAI.GOODS_CD_NRS = @GOODS_CD_NRS                  " & vbNewLine _
    '                                        & "AND   ZAI.SYS_DEL_FLG  = '0'                            " & vbNewLine
    ''' <summary>
    ''' 在庫データ存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_ZAIKO As String = "SELECT                                                  " & vbNewLine _
                                            & "    ISNULL(SUM(ZAI.PORA_ZAI_NB),0)   AS SELECT_CNT      " & vbNewLine _
                                            & "   ,count(*)                         AS REC_CNT --ADD 2018/06/01      " & vbNewLine _
                                            & "FROM                                                    " & vbNewLine _
                                            & "    $LM_TRN$..D_ZAI_TRS   ZAI                           " & vbNewLine _
                                            & "WHERE ZAI.NRS_BR_CD    = @NRS_BR_CD                     " & vbNewLine _
                                            & "AND   ZAI.GOODS_CD_NRS = @GOODS_CD_NRS                  " & vbNewLine _
                                            & "AND   ZAI.SYS_DEL_FLG  = '0'                            " & vbNewLine
    'END YANAI 要望番号739

    ''' <summary>
    ''' 在庫データ存在チェック(荷主コードS・SS 編集可否判定用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_CUST_ZAIKO As String = "" _
        & "SELECT TOP 1                     " & vbNewLine _
        & "      NRS_BR_CD                  " & vbNewLine _
        & "    , ZAI_REC_NO                 " & vbNewLine _
        & "FROM                             " & vbNewLine _
        & "    $LM_TRN$..D_ZAI_TRS          " & vbNewLine _
        & "WHERE                            " & vbNewLine _
        & "    NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
        & "AND CUST_CD_L = @CUST_CD_L       " & vbNewLine _
        & "AND CUST_CD_M = @CUST_CD_M       " & vbNewLine _
        & "AND GOODS_CD_NRS = @GOODS_CD_NRS " & vbNewLine _
        & "AND NOT(    PORA_ZAI_NB = 0      " & vbNewLine _
        & "        AND ALCTD_NB = 0         " & vbNewLine _
        & "        AND ALLOC_CAN_NB = 0)    " & vbNewLine _
        & "AND SYS_DEL_FLG = 0              " & vbNewLine _
        & ""

#End Region

#Region "検索処理 SQL"

#Region "商品マスタ"

    ''' <summary>
    ''' 商品マスタ検索処理(件数取得(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_SELECT As String = " SELECT COUNT(GDS.GOODS_CD_NRS)	   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' 商品マスタ検索処理(データ取得(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_SELECT As String = " SELECT                                                                                                " & vbNewLine _
                                            & "     GDS.NRS_BR_CD                                                        AS    NRS_BR_CD              " & vbNewLine _
                                            & "    ,BRM.NRS_BR_NM                                                        AS    NRS_BR_NM              " & vbNewLine _
                                            & "    ,GDS.GOODS_CD_NRS                                                     AS    GOODS_CD_NRS           " & vbNewLine _
                                            & "    ,GDS.CUST_CD_L                                                        AS    CUST_CD_L              " & vbNewLine _
                                            & "    ,GDS.CUST_CD_M                                                        AS    CUST_CD_M              " & vbNewLine _
                                            & "    ,GDS.CUST_CD_S                                                        AS    CUST_CD_S              " & vbNewLine _
                                            & "    ,GDS.CUST_CD_SS                                                       AS    CUST_CD_SS             " & vbNewLine _
                                            & "    ,GDS.CUST_CD_L + '-' + GDS.CUST_CD_M + '-' + GDS.CUST_CD_S + '-' + GDS.CUST_CD_SS AS    CUST_CD    " & vbNewLine _
                                            & "    ,CST.CUST_NM_L                                                        AS    CUST_NM_L              " & vbNewLine _
                                            & "    ,CST.CUST_NM_M                                                        AS    CUST_NM_M              " & vbNewLine _
                                            & "    ,CST.CUST_NM_S                                                        AS    CUST_NM_S              " & vbNewLine _
                                            & "    ,CST.CUST_NM_SS                                                       AS    CUST_NM_SS             " & vbNewLine _
                                            & "    ,GDS.GOODS_CD_CUST                                                    AS    GOODS_CD_CUST          " & vbNewLine _
                                            & "    ,GDS.GOODS_NM_1                                                       AS    GOODS_NM_1             " & vbNewLine _
                                            & "    ,GDS.SEARCH_KEY_1                                                     AS    SEARCH_KEY_1           " & vbNewLine _
                                            & "    ,GDS.SEARCH_KEY_2                                                     AS    SEARCH_KEY_2           " & vbNewLine _
                                            & "    ,GDS.CUST_COST_CD1                                                    AS    CUST_COST_CD1          " & vbNewLine _
                                            & "    ,GDS.CUST_COST_CD2                                                    AS    CUST_COST_CD2          " & vbNewLine _
                                            & "    ,GDS.JAN_CD                                                           AS    JAN_CD                 " & vbNewLine _
                                            & "    ,GDS.GOODS_NM_2                                                       AS    GOODS_NM_2             " & vbNewLine _
                                            & "    ,GDS.GOODS_NM_3                                                       AS    GOODS_NM_3             " & vbNewLine _
                                            & "    ,GDS.UP_GP_CD_1                                                       AS    UP_GP_CD_1             " & vbNewLine _
                                            & "    ,GDS.SHOBO_CD                                                         AS    SHOBO_CD               " & vbNewLine _
                                            & "    ,KBN1.KBN_NM1 + ' ' + SBO.HINMEI + ' ' + SBO.SEISITSU + ' ' + ISNULL(KBN2.KBN_NM1, '') AS SHOBO_INFO   " & vbNewLine _
                                            & "    ,GDS.KIKEN_KB                                                         AS    KIKEN_KB               " & vbNewLine _
                                            & "    ,GDS.UN                                                               AS    UN                     " & vbNewLine _
                                            & "    ,GDS.PG_KB                                                            AS    PG_KB                  " & vbNewLine _
                                            & "    ,GDS.CLASS_1                                                          AS    CLASS_1                " & vbNewLine _
                                            & "    ,GDS.CLASS_2                                                          AS    CLASS_2                " & vbNewLine _
                                            & "    ,GDS.CLASS_3                                                          AS    CLASS_3                " & vbNewLine _
                                            & "    ,GDS.CHEM_MTRL_KB                                                     AS    CHEM_MTRL_KB           " & vbNewLine _
                                            & "    ,GDS.DOKU_KB                                                          AS    DOKU_KB                " & vbNewLine _
                                            & "    ,GDS.KOUATHUGAS_KB                                                    AS    KOUATHUGAS_KB          " & vbNewLine _
                                            & "    ,GDS.YAKUZIHO_KB                                                      AS    YAKUZIHO_KB            " & vbNewLine _
                                            & "    ,GDS.SHOBOKIKEN_KB                                                    AS    SHOBOKIKEN_KB          " & vbNewLine _
                                            & "    ,GDS.KAIYOUOSEN_KB                                                    AS    KAIYOUOSEN_KB          " & vbNewLine _
                                            & "    ,KBN3.KBN_NM1                                                         AS    DOKU_KB_NM             " & vbNewLine _
                                            & "    ,GDS.GAS_KANRI_KB                                                     AS    GAS_KANRI_KB           " & vbNewLine _
                                            & "    ,GDS.ONDO_KB                                                          AS    ONDO_KB                " & vbNewLine _
                                            & "    ,KBN4.KBN_NM1                                                         AS    ONDO_KB_NM             " & vbNewLine _
                                            & "    ,GDS.UNSO_ONDO_KB                                                     AS    UNSO_ONDO_KB           " & vbNewLine _
                                            & "    ,ISNULL(GDS.ONDO_MX,0)                                                AS    ONDO_MX                " & vbNewLine _
                                            & "    ,ISNULL(GDS.ONDO_MM,0)                                                AS    ONDO_MM                " & vbNewLine _
                                            & "    ,GDS.ONDO_STR_DATE                                                    AS    ONDO_STR_DATE          " & vbNewLine _
                                            & "    ,GDS.ONDO_END_DATE                                                    AS    ONDO_END_DATE          " & vbNewLine _
                                            & "    ,GDS.ONDO_UNSO_STR_DATE                                               AS    ONDO_UNSO_STR_DATE     " & vbNewLine _
                                            & "    ,GDS.ONDO_UNSO_END_DATE                                               AS    ONDO_UNSO_END_DATE     " & vbNewLine _
                                            & "    ,GDS.KYOKAI_GOODS_KB                                                  AS    KYOKAI_GOODS_KB        " & vbNewLine _
                                            & "    ,GDS.ALCTD_KB                                                         AS    ALCTD_KB               " & vbNewLine _
                                            & "    ,ISNULL(GDS.NB_UT,0)                                                  AS    NB_UT                  " & vbNewLine _
                                            & "    ,KBN15.KBN_NM1                                                        AS    NB_UT_NM               " & vbNewLine _
                                            & "    ,ISNULL(KBN13.VALUE1,0)                                               AS    NT_GR_CONV_RATE        " & vbNewLine _
                                            & "    ,ISNULL(GDS.PKG_NB,0)                                                 AS    PKG_NB                 " & vbNewLine _
                                            & "    ,GDS.PKG_UT                                                           AS    PKG_UT                 " & vbNewLine _
                                            & "    ,KBN13.KBN_NM1                                                        AS    PKG_UT_NM              " & vbNewLine _
                                            & "    ,ISNULL(GDS.PLT_PER_PKG_UT,0)                                         AS    PLT_PER_PKG_UT         " & vbNewLine _
                                            & "    ,ISNULL(GDS.INNER_PKG_NB,0)                                           AS    INNER_PKG_NB           " & vbNewLine _
                                            & "    ,ISNULL(GDS.STD_IRIME_NB,0)                                           AS    STD_IRIME_NB           " & vbNewLine _
                                            & "    ,GDS.STD_IRIME_UT                                                     AS    STD_IRIME_UT           " & vbNewLine _
                                            & "    ,KBN14.KBN_NM1                                                        AS    STD_IRIME_NM           " & vbNewLine _
                                            & "    ,ISNULL(GDS.STD_WT_KGS,0)                                             AS    STD_WT_KGS             " & vbNewLine _
                                            & "    ,ISNULL(GDS.STD_CBM,0)                                                AS    STD_CBM                " & vbNewLine _
                                            & "    ,GDS.INKA_KAKO_SAGYO_KB_1                                             AS    INKA_KAKO_SAGYO_KB_1   " & vbNewLine _
                                            & "    ,GDS.INKA_KAKO_SAGYO_KB_2                                             AS    INKA_KAKO_SAGYO_KB_2   " & vbNewLine _
                                            & "    ,GDS.INKA_KAKO_SAGYO_KB_3                                             AS    INKA_KAKO_SAGYO_KB_3   " & vbNewLine _
                                            & "    ,GDS.INKA_KAKO_SAGYO_KB_4                                             AS    INKA_KAKO_SAGYO_KB_4   " & vbNewLine _
                                            & "    ,GDS.INKA_KAKO_SAGYO_KB_5                                             AS    INKA_KAKO_SAGYO_KB_5   " & vbNewLine _
                                            & "    ,GDS.OUTKA_KAKO_SAGYO_KB_1                                            AS    OUTKA_KAKO_SAGYO_KB_1  " & vbNewLine _
                                            & "    ,GDS.OUTKA_KAKO_SAGYO_KB_2                                            AS    OUTKA_KAKO_SAGYO_KB_2  " & vbNewLine _
                                            & "    ,GDS.OUTKA_KAKO_SAGYO_KB_3                                            AS    OUTKA_KAKO_SAGYO_KB_3  " & vbNewLine _
                                            & "    ,GDS.OUTKA_KAKO_SAGYO_KB_4                                            AS    OUTKA_KAKO_SAGYO_KB_4  " & vbNewLine _
                                            & "    ,GDS.OUTKA_KAKO_SAGYO_KB_5                                            AS    OUTKA_KAKO_SAGYO_KB_5  " & vbNewLine _
                                            & "    ,GDS.PKG_SAGYO                                                        AS    PKG_SAGYO              " & vbNewLine _
                                            & "    ,SGY.SAGYO_NM                                                         AS    PKG_SAGYO_NM           " & vbNewLine _
                                            & "    ,GDS.TARE_YN                                                          AS    TARE_YN                " & vbNewLine _
                                            & "    ,GDS.SP_NHS_YN                                                        AS    SP_NHS_YN              " & vbNewLine _
                                            & "    ,GDS.COA_YN                                                           AS    COA_YN                 " & vbNewLine _
                                            & "    ,GDS.LOT_CTL_KB                                                       AS    LOT_CTL_KB             " & vbNewLine _
                                            & "    ,GDS.LT_DATE_CTL_KB                                                   AS    LT_DATE_CTL_KB         " & vbNewLine _
                                            & "    ,GDS.CRT_DATE_CTL_KB                                                  AS    CRT_DATE_CTL_KB        " & vbNewLine _
                                            & "    ,GDS.DEF_SPD_KB                                                       AS    DEF_SPD_KB             " & vbNewLine _
                                            & "    ,GDS.KITAKU_AM_UT_KB                                                  AS    KITAKU_AM_UT_KB        " & vbNewLine _
                                            & "    ,ISNULL(GDS.KITAKU_GOODS_UP,0)                                        AS    KITAKU_GOODS_UP        " & vbNewLine _
                                            & "    ,GDS.ORDER_KB                                                         AS    ORDER_KB               " & vbNewLine _
                                            & "    ,ISNULL(GDS.ORDER_NB,0)                                               AS    ORDER_NB               " & vbNewLine _
                                            & "    ,GDS.SHIP_CD_L                                                        AS    SHIP_CD_L              " & vbNewLine _
                                            & "    ,DST.DEST_NM                                                          AS    SHIP_NM_L              " & vbNewLine _
                                            & "    ,GDS.SKYU_MEI_YN                                                      AS    SKYU_MEI_YN            " & vbNewLine _
                                            & "    ,GDS.HIKIATE_ALERT_YN                                                 AS    HIKIATE_ALERT_YN       " & vbNewLine _
                                            & "    ,GDS.UNSO_HOKEN_YN                                                    AS    UNSO_HOKEN_YN   --ADD 2018/07/17 " & vbNewLine _
                                            & "    ,GDS.OUTKA_ATT                                                        AS    OUTKA_ATT              " & vbNewLine _
                                            & "    ,ISNULL(GDS.PRINT_NB,0)                                               AS    PRINT_NB               " & vbNewLine _
                                            & "    ,ISNULL(GDS.CONSUME_PERIOD_DATE,0)                                    AS    CONSUME_PERIOD_DATE    " & vbNewLine _
                                            & "    ,CST.OYA_SEIQTO_CD                                                    AS    SEIQTO_CD              " & vbNewLine _
                                            & "    ,SQT.SEIQTO_NM                                                        AS    SEIQTO_NM              " & vbNewLine _
                                            & "    ,SQT.SEIQTO_BUSYO_NM                                                  AS    SEIQTO_BUSYO_NM        " & vbNewLine _
                                            & "    ,TNK.STR_DATE                                                         AS    STR_DATE               " & vbNewLine _
                                            & "    ,ISNULL(TNK.STORAGE_1,0)                                              AS    STORAGE_1              " & vbNewLine _
                                            & "    ,TNK.STORAGE_KB1                                                      AS    STORAGE_KB1            " & vbNewLine _
                                            & "    ,ISNULL(TNK.STORAGE_2,0)                                              AS    STORAGE_2              " & vbNewLine _
                                            & "    ,TNK.STORAGE_KB2                                                      AS    STORAGE_KB2            " & vbNewLine _
                                            & "    ,ISNULL(TNK.HANDLING_IN,0)                                            AS    HANDLING_IN            " & vbNewLine _
                                            & "    ,TNK.HANDLING_IN_KB                                                   AS    HANDLING_IN_KB         " & vbNewLine _
                                            & "    ,ISNULL(TNK.HANDLING_OUT,0)                                           AS    HANDLING_OUT           " & vbNewLine _
                                            & "    ,TNK.HANDLING_OUT_KB                                                  AS    HANDLING_OUT_KB        " & vbNewLine _
                                            & "    ,ISNULL(TNK.MINI_TEKI_IN_AMO,0)                                       AS    MINI_TEKI_IN_AMO       " & vbNewLine _
                                            & "    ,ISNULL(TNK.MINI_TEKI_OUT_AMO,0)                                      AS    MINI_TEKI_OUT_AMO      " & vbNewLine _
                                            & "    ,TNK.KIWARI_KB                                                        AS    KIWARI_KB              " & vbNewLine _
                                            & "    ,CST_CURR.ITEM_CURR_CD                                                AS    ITEM_CURR_CD           " & vbNewLine _
                                            & "    ,GDS.SYS_ENT_DATE                                                     AS    SYS_ENT_DATE           " & vbNewLine _
                                            & "    ,USE1.USER_NM                                                         AS    SYS_ENT_USER_NM        " & vbNewLine _
                                            & "    ,GDS.SYS_UPD_DATE                                                     AS    SYS_UPD_DATE           " & vbNewLine _
                                            & "    ,GDS.SYS_UPD_TIME                                                     AS    SYS_UPD_TIME           " & vbNewLine _
                                            & "    ,USE2.USER_NM                                                         AS    SYS_UPD_USER_NM        " & vbNewLine _
                                            & "    ,GDS.SYS_DEL_FLG                                                      AS    SYS_DEL_FLG            " & vbNewLine _
                                            & "    ,KBN30.KBN_NM1                                                        AS    SYS_DEL_NM             " & vbNewLine _
                                            & "    ,GDS.SIZE_KB                                                          AS    SIZE_KB                " & vbNewLine _
                                            & "    ,GDS.OCR_GOODS_CD_CUST												 AS    OCR_GOODS_CD_CUST      " & vbNewLine _
                                            & "    ,GDS.OCR_GOODS_CD_NM1												 AS    OCR_GOODS_CD_NM1       " & vbNewLine _
                                            & "    ,GDS.OCR_GOODS_CD_NM2												 AS    OCR_GOODS_CD_NM2     　" & vbNewLine _
                                            & "    ,GDS.OCR_GOODS_CD_STD_IRIME											 AS    OCR_GOODS_CD_STD_IRIME " & vbNewLine _
                                            & "-- 要望番号2471対応 added 2015.12.14                                                                   " & vbNewLine _
                                            & "    ,GDS.OUTER_PKG				            							 AS    OUTER_PKG              " & vbNewLine _
                                            & "    ,GDS.KIKEN_DATE				            							 AS    KIKEN_DATE             " & vbNewLine _
                                            & "    ,USE3.USER_NM    				            						 AS    KIKEN_USER_NM          " & vbNewLine _
                                            & "    ,GDS.WIDTH    				            						     AS    WIDTH                  " & vbNewLine _
                                            & "    ,GDS.HEIGHT    				                 						 AS    HEIGHT                 " & vbNewLine _
                                            & "    ,GDS.DEPTH    				                						 AS    DEPTH                  " & vbNewLine _
                                            & "    ,GDS.VOLUME      			            	    					 AS    ACTUAL_VOLUME          " & vbNewLine _
                                            & "    ,GDS.OCCUPY_VOLUME			            	    					 AS    OCCUPY_VOLUME          " & vbNewLine _
                                            & "    ,ISNULL(KBN13.KBN_NM6,0)                                              AS    CYL_FLG                " & vbNewLine _
                                            & "    ,GDS.OUTKA_HASU_SAGYO_KB_1                                            AS    OUTKA_HASU_SAGYO_KB_1  " & vbNewLine _
                                            & "    ,GDS.OUTKA_HASU_SAGYO_KB_2                                            AS    OUTKA_HASU_SAGYO_KB_2  " & vbNewLine _
                                            & "    ,GDS.OUTKA_HASU_SAGYO_KB_3                                            AS    OUTKA_HASU_SAGYO_KB_3  " & vbNewLine _
                                            & "    ,ISNULL(GDS.AVAL_YN,'')  AS    AVAL_YN         --ADD 2019/04/22 依頼番号 : 005252                  " & vbNewLine _
                                            & "    ,ISNULL(KBN16.KBN_NM1,'') AS   AVAL_YN_NM      --ADD 2019/04/22 依頼番号 : 005252                  " & vbNewLine _
                                            & "    ,ISNULL(GDS.HIZYU,0)                                                  AS    HIZYU                  " & vbNewLine _
                                            & "    ,ISNULL(KBN17.KBN_NM1,'')                                             AS    KAIYOUOSEN_KB_NM       " & vbNewLine

    ''' <summary>
    ''' 商品マスタ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_FROM As String = " FROM                                                                                                " & vbNewLine _
                                            & "    $LM_MST$..M_GOODS    GDS                                                                           " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_NRS_BR    BRM                                                                    " & vbNewLine _
                                            & "ON  BRM.NRS_BR_CD       =    GDS.NRS_BR_CD                                                             " & vbNewLine _
                                            & "AND BRM.SYS_DEL_FLG     =    '0'                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST      CST                                                                    " & vbNewLine _
                                            & "ON  CST.NRS_BR_CD       =    GDS.NRS_BR_CD                                                             " & vbNewLine _
                                            & "AND CST.CUST_CD_L       =    GDS.CUST_CD_L                                                             " & vbNewLine _
                                            & "AND CST.CUST_CD_M       =    GDS.CUST_CD_M                                                             " & vbNewLine _
                                            & "AND CST.CUST_CD_S       =    GDS.CUST_CD_S                                                             " & vbNewLine _
                                            & "AND CST.CUST_CD_SS      =    GDS.CUST_CD_SS                                                            " & vbNewLine _
                                            & "AND CST.SYS_DEL_FLG     =    '0'                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST      CST_CURR                                                                    " & vbNewLine _
                                            & "ON  CST_CURR.NRS_BR_CD       =    GDS.NRS_BR_CD                                                             " & vbNewLine _
                                            & "AND CST_CURR.CUST_CD_L       =    GDS.CUST_CD_L                                                             " & vbNewLine _
                                            & "AND CST_CURR.CUST_CD_M       =    GDS.CUST_CD_M                                                             " & vbNewLine _
                                            & "AND CST_CURR.CUST_CD_S       =    '00'                                                             " & vbNewLine _
                                            & "AND CST_CURR.CUST_CD_SS      =    '00'                                                            " & vbNewLine _
                                            & "AND CST_CURR.SYS_DEL_FLG     =    '0'                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SHOBO    SBO                                                                     " & vbNewLine _
                                            & "ON  SBO.SHOBO_CD        =    GDS.SHOBO_CD                                                              " & vbNewLine _
                                            & "AND SBO.SYS_DEL_FLG     =    '0'                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN      KBN1                                                                    " & vbNewLine _
                                            & "ON  KBN1.KBN_GROUP_CD   =    'S004'                                                                    " & vbNewLine _
                                            & "AND KBN1.KBN_CD         =    SBO.RUI                                                                   " & vbNewLine _
                                            & "AND KBN1.SYS_DEL_FLG    =    '0'                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN      KBN2                                                                    " & vbNewLine _
                                            & "ON  KBN2.KBN_GROUP_CD   =    'S022'                                                                    " & vbNewLine _
                                            & "AND KBN2.KBN_CD         =    SBO.SYU                                                                   " & vbNewLine _
                                            & "AND KBN2.SYS_DEL_FLG    =    '0'                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN      KBN3                                                                    " & vbNewLine _
                                            & "ON  KBN3.KBN_GROUP_CD   =    'G001'                                                                    " & vbNewLine _
                                            & "AND KBN3.KBN_CD         =    GDS.DOKU_KB                                                               " & vbNewLine _
                                            & "AND KBN3.SYS_DEL_FLG    =    '0'                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN      KBN4                                                                    " & vbNewLine _
                                            & "ON  KBN4.KBN_GROUP_CD   =    'O002'                                                                    " & vbNewLine _
                                            & "AND KBN4.KBN_CD         =    GDS.ONDO_KB                                                               " & vbNewLine _
                                            & "AND KBN4.SYS_DEL_FLG    =    '0'                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN      KBN13                                                                   " & vbNewLine _
                                            & "ON  KBN13.KBN_GROUP_CD  =    'N001'                                                                    " & vbNewLine _
                                            & "AND KBN13.KBN_CD        =    GDS.PKG_UT                                                                " & vbNewLine _
                                            & "AND KBN13.SYS_DEL_FLG   =    '0'                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN      KBN14                                                                   " & vbNewLine _
                                            & "ON  KBN14.KBN_GROUP_CD  =    'I001'                                                                    " & vbNewLine _
                                            & "AND KBN14.KBN_CD        =    GDS.STD_IRIME_UT                                                          " & vbNewLine _
                                            & "AND KBN14.SYS_DEL_FLG   =    '0'                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN      KBN15                                                                   " & vbNewLine _
                                            & "ON  KBN15.KBN_GROUP_CD  =    'K002'                                                                    " & vbNewLine _
                                            & "AND KBN15.KBN_CD        =    GDS.NB_UT                                                                 " & vbNewLine _
                                            & "AND KBN15.SYS_DEL_FLG   =    '0'                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SAGYO    SGY                                                                     " & vbNewLine _
                                            & "ON  SGY.SAGYO_CD        =    GDS.PKG_SAGYO                                                             " & vbNewLine _
                                            & "AND SGY.SYS_DEL_FLG     =    '0'                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DEST    DST                                                                      " & vbNewLine _
                                            & "ON  DST.NRS_BR_CD        =    GDS.NRS_BR_CD                                                            " & vbNewLine _
                                            & "AND DST.CUST_CD_L        =    GDS.CUST_CD_L                                                            " & vbNewLine _
                                            & "AND DST.DEST_CD          =    GDS.SHIP_CD_L                                                            " & vbNewLine _
                                            & "AND DST.SYS_DEL_FLG      =    '0'                                                                      " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SEIQTO    SQT                                                                    " & vbNewLine _
                                            & "ON  SQT.NRS_BR_CD        =    GDS.NRS_BR_CD                                                            " & vbNewLine _
                                            & "AND SQT.SEIQTO_CD        =    CST.OYA_SEIQTO_CD                                                        " & vbNewLine _
                                            & "AND SQT.SYS_DEL_FLG      =    '0'                                                                      " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_TANKA TNK                                                                        " & vbNewLine _
                                            & "ON   TNK.NRS_BR_CD   =  GDS.NRS_BR_CD                                                                  " & vbNewLine _
                                            & "AND  TNK.CUST_CD_L   =  GDS.CUST_CD_L                                                                  " & vbNewLine _
                                            & "AND  TNK.CUST_CD_M   =  GDS.CUST_CD_M                                                                  " & vbNewLine _
                                            & "AND  TNK.UP_GP_CD_1  =  GDS.UP_GP_CD_1                                                                 " & vbNewLine _
                                            & "LEFT JOIN                                                                                              " & vbNewLine _
                                            & "        (SELECT                                                                                        " & vbNewLine _
                                            & "            TNK.NRS_BR_CD           AS    NRS_BR_CD                                                    " & vbNewLine _
                                            & "           ,TNK.CUST_CD_L           AS    CUST_CD_L                                                    " & vbNewLine _
                                            & "           ,TNK.CUST_CD_M           AS    CUST_CD_M                                                    " & vbNewLine _
                                            & "           ,TNK.UP_GP_CD_1          AS    UP_GP_CD_1                                                   " & vbNewLine _
                                            & "           ,MAX(TNK.STR_DATE)       AS    STR_DATE                                                     " & vbNewLine _
                                            & "        FROM                                                                                           " & vbNewLine _
                                            & "           $LM_MST$..M_TANKA TNK                                                                       " & vbNewLine _
                                            & "        WHERE                                                                                          " & vbNewLine _
                                            & "            TNK.SYS_DEL_FLG =   '0'                                                                    " & vbNewLine _
                                            & "        AND TNK.STR_DATE    <= @SYS_DATE                                                               " & vbNewLine _
                                            & "        GROUP BY                                                                                       " & vbNewLine _
                                            & "            TNK.NRS_BR_CD                                                                              " & vbNewLine _
                                            & "           ,TNK.CUST_CD_L                                                                              " & vbNewLine _
                                            & "           ,TNK.CUST_CD_M                                                                              " & vbNewLine _
                                            & "           ,TNK.UP_GP_CD_1                                                                             " & vbNewLine _
                                            & "        ) TNK2                                                                                         " & vbNewLine _
                                            & "ON   TNK2.NRS_BR_CD   =   TNK.NRS_BR_CD                                                                " & vbNewLine _
                                            & "AND  TNK2.CUST_CD_L   =   TNK.CUST_CD_L                                                                " & vbNewLine _
                                            & "AND  TNK2.CUST_CD_M   =   TNK.CUST_CD_M                                                                " & vbNewLine _
                                            & "AND  TNK2.UP_GP_CD_1  =   TNK.UP_GP_CD_1                                                               " & vbNewLine _
                                            & "AND  TNK2.STR_DATE    =   TNK.STR_DATE                                                                 " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..S_USER      USE1                                                                   " & vbNewLine _
                                            & "ON  USE1.USER_CD       =    GDS.SYS_ENT_USER                                                           " & vbNewLine _
                                            & "AND USE1.SYS_DEL_FLG   =    '0'                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..S_USER      USE2                                                                   " & vbNewLine _
                                            & "ON  USE2.USER_CD       =    GDS.SYS_UPD_USER                                                           " & vbNewLine _
                                            & "AND USE2.SYS_DEL_FLG   =    '0'                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN      KBN30                                                                   " & vbNewLine _
                                            & "ON  KBN30.KBN_GROUP_CD  =    'S051'                                                                    " & vbNewLine _
                                            & "AND KBN30.KBN_CD        =    GDS.SYS_DEL_FLG                                                           " & vbNewLine _
                                            & "AND KBN30.SYS_DEL_FLG   =    '0'                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..S_USER      USE3                                                                   " & vbNewLine _
                                            & "ON  USE3.USER_CD       =    GDS.KIKEN_USER_ID                                                          " & vbNewLine _
                                            & "AND USE3.SYS_DEL_FLG   =    '0'                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN      KBN16                                                                   " & vbNewLine _
                                            & "ON  KBN16.KBN_GROUP_CD    =    'K017'                                                                  " & vbNewLine _
                                            & "AND KBN16.KBN_CD          =    GDS.AVAL_YN                                                             " & vbNewLine _
                                            & "AND KBN16.SYS_DEL_FLG     =    '0'                                                                     " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN      KBN17                                                                   " & vbNewLine _
                                            & "ON  KBN17.KBN_GROUP_CD    =    'G013'                                                                  " & vbNewLine _
                                            & "AND KBN17.KBN_NM2         =    GDS.KAIYOUOSEN_KB                                                       " & vbNewLine _
                                            & "AND KBN17.SYS_DEL_FLG     =    '0'                                                                     " & vbNewLine


    ''' <summary>
    ''' 並び順
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                   " & vbNewLine _
                                         & "    GDS.GOODS_CD_CUST      " & vbNewLine _
                                         & "   ,GDS.GOODS_NM_1         " & vbNewLine _
                                         & "   ,GDS.NRS_BR_CD          " & vbNewLine _
                                         & "   ,GDS.CUST_CD_L          " & vbNewLine _
                                         & "   ,GDS.CUST_CD_M          " & vbNewLine _
                                         & "   ,GDS.CUST_CD_S          " & vbNewLine _
                                         & "   ,GDS.CUST_CD_SS         " & vbNewLine

#End Region

#Region "商品明細マスタ"

    ''' <summary>
    ''' 商品明細マスタ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DTL_DATA As String = " SELECT                                                         " & vbNewLine _
                                                & "     DTL.NRS_BR_CD             AS    NRS_BR_CD                  " & vbNewLine _
                                                & "    ,DTL.GOODS_CD_NRS          AS    GOODS_CD_NRS               " & vbNewLine _
                                                & "    ,DTL.GOODS_CD_NRS_EDA      AS    GOODS_CD_NRS_EDA           " & vbNewLine _
                                                & "    ,DTL.SUB_KB                AS    SUB_KB                     " & vbNewLine _
                                                & "    ,DTL.SET_NAIYO             AS    SET_NAIYO                  " & vbNewLine _
                                                & "    ,DTL.REMARK                AS    REMARK                     " & vbNewLine _
                                                & "FROM                                                            " & vbNewLine _
                                                & "    $LM_MST$..M_GOODS_DETAILS    DTL                            " & vbNewLine


    ''' <summary>
    ''' 商品明細マスタ検索処理(並び順)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDERBY_DTL_DATA As String = "ORDER BY                                                         " & vbNewLine _
                                                & "     DTL.NRS_BR_CD                                                " & vbNewLine _
                                                & "    ,DTL.GOODS_CD_NRS                                             " & vbNewLine _
                                                & "    ,DTL.GOODS_CD_NRS_EDA                                         " & vbNewLine _
                                                & "    ,DTL.SUB_KB                                                   " & vbNewLine

#End Region

#End Region

#Region "削除/復活処理 SQL"

    ''' <summary>
    ''' 商品マスタ更新SQL(論理削除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_DEL_GOODS_M As String = "UPDATE                                                     " & vbNewLine _
                                                & "    $LM_MST$..M_GOODS                                      " & vbNewLine _
                                                & "SET                                                        " & vbNewLine _
                                                & "      SYS_UPD_DATE            =    @SYS_UPD_DATE           " & vbNewLine _
                                                & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME           " & vbNewLine _
                                                & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID           " & vbNewLine _
                                                & "     ,SYS_UPD_USER            =    @SYS_UPD_USER           " & vbNewLine _
                                                & "     ,SYS_DEL_FLG             =    @SYS_DEL_FLG            " & vbNewLine _
                                                & "WHERE NRS_BR_CD               =    @NRS_BR_CD              " & vbNewLine _
                                                & "AND   GOODS_CD_NRS            =    @GOODS_CD_NRS           " & vbNewLine _
                                                & "AND   SYS_UPD_DATE            =    @HAITA_DATE             " & vbNewLine _
                                                & "AND   SYS_UPD_TIME            =    @HAITA_TIME             " & vbNewLine


    ''' <summary>
    ''' 商品明細マスタ更新SQL(論理削除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_DEL_GOODS_DTL As String = "UPDATE                                                   " & vbNewLine _
                                                & "    $LM_MST$..M_GOODS_DETAILS                              " & vbNewLine _
                                                & "SET                                                        " & vbNewLine _
                                                & "      SYS_UPD_DATE            =    @SYS_UPD_DATE           " & vbNewLine _
                                                & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME           " & vbNewLine _
                                                & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID           " & vbNewLine _
                                                & "     ,SYS_UPD_USER            =    @SYS_UPD_USER           " & vbNewLine _
                                                & "     ,SYS_DEL_FLG             =    @SYS_DEL_FLG            " & vbNewLine _
                                                & "WHERE NRS_BR_CD               =    @NRS_BR_CD              " & vbNewLine _
                                                & "AND   GOODS_CD_NRS            =    @GOODS_CD_NRS           " & vbNewLine



#End Region

#Region "単価一括変更処理 SQL"

    ''' <summary>
    ''' 商品マスタ更新SQL(一括変更)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_IKKATU As String = "UPDATE                                                     " & vbNewLine _
                                              & "    $LM_MST$..M_GOODS                                      " & vbNewLine _
                                              & "SET                                                        " & vbNewLine _
                                              & "      UP_GP_CD_1              =    @UP_GP_CD_1             " & vbNewLine _
                                              & "     ,SYS_UPD_DATE            =    @SYS_UPD_DATE           " & vbNewLine _
                                              & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME           " & vbNewLine _
                                              & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID           " & vbNewLine _
                                              & "     ,SYS_UPD_USER            =    @SYS_UPD_USER           " & vbNewLine _
                                              & "WHERE NRS_BR_CD               =    @NRS_BR_CD              " & vbNewLine _
                                              & "AND   GOODS_CD_NRS            =    @GOODS_CD_NRS           " & vbNewLine _
                                              & "AND   SYS_UPD_DATE            =    @HAITA_DATE             " & vbNewLine _
                                              & "AND   SYS_UPD_TIME            =    @HAITA_TIME             " & vbNewLine


#End Region

    'START YANAI 要望番号372
#Region "荷主一括変更処理 SQL"

    ''' <summary>
    ''' 商品マスタ更新SQL(荷主一括変更)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_NINUSHI_IKKATU As String = "UPDATE                                             " & vbNewLine _
                                              & "    $LM_MST$..M_GOODS                                      " & vbNewLine _
                                              & "SET                                                        " & vbNewLine _
                                              & "      CUST_CD_S               =    @CUST_CD_S              " & vbNewLine _
                                              & "     ,CUST_CD_SS              =    @CUST_CD_SS             " & vbNewLine _
                                              & "     ,SYS_UPD_DATE            =    @SYS_UPD_DATE           " & vbNewLine _
                                              & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME           " & vbNewLine _
                                              & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID           " & vbNewLine _
                                              & "     ,SYS_UPD_USER            =    @SYS_UPD_USER           " & vbNewLine _
                                              & "WHERE NRS_BR_CD               =    @NRS_BR_CD              " & vbNewLine _
                                              & "AND   GOODS_CD_NRS            =    @GOODS_CD_NRS           " & vbNewLine _
                                              & "AND   SYS_UPD_DATE            =    @HAITA_DATE             " & vbNewLine _
                                              & "AND   SYS_UPD_TIME            =    @HAITA_TIME             " & vbNewLine


#End Region
    'END YANAI 要望番号372

#Region "保存処理 SQL"

#Region "チェック"

    ''' <summary>
    ''' 単価マスタ存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_TANKAM As String = "SELECT                                                  " & vbNewLine _
                                             & "       COUNT(TNK.NRS_BR_CD)      AS    SELECT_CNT       " & vbNewLine _
                                             & "FROM                                                    " & vbNewLine _
                                             & "     $LM_MST$..M_TANKA    TNK                           " & vbNewLine _
                                             & "WHERE                                                   " & vbNewLine _
                                             & "       TNK.NRS_BR_CD    =    @NRS_BR_CD                 " & vbNewLine _
                                             & "AND    TNK.CUST_CD_L    =    @CUST_CD_L                 " & vbNewLine _
                                             & "AND    TNK.CUST_CD_M    =    @CUST_CD_M                 " & vbNewLine _
                                             & "AND    TNK.UP_GP_CD_1   =    @UP_GP_CD_1                " & vbNewLine _
                                             & "AND    TNK.STR_DATE     <=   @SYSTEM_DATE               " & vbNewLine _
                                             & "AND    TNK.SYS_DEL_FLG  =    '0'                        " & vbNewLine

    ''' <summary>
    ''' 単価マスタ存在チェック(未来適用分)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_TANKAM_MIRAI As String = "SELECT                                                  " & vbNewLine _
                                                   & "       COUNT(TNK.NRS_BR_CD)      AS    SELECT_CNT       " & vbNewLine _
                                                   & "FROM                                                    " & vbNewLine _
                                                   & "     $LM_MST$..M_TANKA    TNK                           " & vbNewLine _
                                                   & "WHERE                                                   " & vbNewLine _
                                                   & "       TNK.NRS_BR_CD    =    @NRS_BR_CD                 " & vbNewLine _
                                                   & "AND    TNK.CUST_CD_L    =    @CUST_CD_L                 " & vbNewLine _
                                                   & "AND    TNK.CUST_CD_M    =    @CUST_CD_M                 " & vbNewLine _
                                                   & "AND    TNK.UP_GP_CD_1   =    @UP_GP_CD_1                " & vbNewLine _
                                                   & "AND    TNK.STR_DATE     >   @SYSTEM_DATE                " & vbNewLine _
                                                   & "AND    TNK.SYS_DEL_FLG  =    '0'                        " & vbNewLine


    ''' <summary>
    ''' 単価マスタ存在チェック(温度管理有り)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_TANKAM_ONDO As String = "SELECT                                             " & vbNewLine _
                                                  & "       TNK.STORAGE_2      AS    STORAGE_2          " & vbNewLine _
                                                  & "      ,TNK.KIWARI_KB      AS    KIWARI_KB          " & vbNewLine _
                                                  & "FROM                                               " & vbNewLine _
                                                  & "     $LM_MST$..M_TANKA    TNK                      " & vbNewLine _
                                                  & "WHERE                                              " & vbNewLine _
                                                  & "       TNK.NRS_BR_CD    =    @NRS_BR_CD            " & vbNewLine _
                                                  & "AND    TNK.CUST_CD_L    =    @CUST_CD_L            " & vbNewLine _
                                                  & "AND    TNK.CUST_CD_M    =    @CUST_CD_M            " & vbNewLine _
                                                  & "AND    TNK.UP_GP_CD_1   =    @UP_GP_CD_1           " & vbNewLine _
                                                  & "AND    TNK.STR_DATE     <=   @SYSTEM_DATE          " & vbNewLine _
                                                  & "AND    TNK.SYS_DEL_FLG  =    '0'                   " & vbNewLine _
                                                  & "ORDER BY    TNK.STR_DATE DESC                      " & vbNewLine


    ''' <summary>
    ''' 単価マスタ存在チェック(温度管理有り(未来適用データ))
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_TANKAM_ONDO_MIRAI As String = "SELECT                                             " & vbNewLine _
                                                        & "       COUNT(NRS_BR_CD)     AS    SELECT_CNT       " & vbNewLine _
                                                        & "FROM                                               " & vbNewLine _
                                                        & "     $LM_MST$..M_TANKA    TNK                      " & vbNewLine _
                                                        & "WHERE                                              " & vbNewLine _
                                                        & "       TNK.NRS_BR_CD    =    @NRS_BR_CD            " & vbNewLine _
                                                        & "AND    TNK.CUST_CD_L    =    @CUST_CD_L            " & vbNewLine _
                                                        & "AND    TNK.CUST_CD_M    =    @CUST_CD_M            " & vbNewLine _
                                                        & "AND    TNK.UP_GP_CD_1   =    @UP_GP_CD_1           " & vbNewLine _
                                                        & "AND    TNK.STR_DATE     >    @SYSTEM_DATE          " & vbNewLine _
                                                        & "AND    TNK.SYS_DEL_FLG  =    '0'                   " & vbNewLine _
                                                        & "AND    TNK.STORAGE_2    =    0                     " & vbNewLine


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

#If True Then   'ADD 2020/02/26 010140【LMS】フィルメ誤出荷類似_EDI取込、出荷登録共に個別荷主機能使用(高度化和地)
    ''' <summary>
    ''' 荷主商品コードで商品マスタ取得処理(荷主コード、荷主商品コード)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_GOODSM_CUST As String = "SELECT                                                         " & vbNewLine _
                                                   & "  NRS_BR_CD                                                    " & vbNewLine _
                                                   & " ,GOODS_CD_NRS                                                 " & vbNewLine _
                                                   & " ,CUST_CD_L                                                    " & vbNewLine _
                                                   & " ,CUST_CD_M                                                    " & vbNewLine _
                                                   & " ,CUST_CD_S                                                    " & vbNewLine _
                                                   & " ,CUST_CD_SS                                                   " & vbNewLine _
                                                   & " ,GOODS_CD_CUST                                                " & vbNewLine _
                                                   & " ,STD_IRIME_NB                                                 " & vbNewLine _
                                                   & " ,PKG_NB                                                       " & vbNewLine _
                                                   & "FROM                                                           " & vbNewLine _
                                                   & "     $LM_MST$..M_GOODS    GDS                                  " & vbNewLine _
                                                   & "WHERE                                                          " & vbNewLine _
                                                   & "       GDS.NRS_BR_CD          =    @NRS_BR_CD                  " & vbNewLine _
                                                   & "AND    GDS.CUST_CD_L          =    @CUST_CD_L                  " & vbNewLine _
                                                   & "AND    GDS.CUST_CD_M          =    @CUST_CD_M                  " & vbNewLine _
                                                   & "AND    GDS.CUST_CD_S          =    @CUST_CD_S                  " & vbNewLine _
                                                   & "AND    GDS.CUST_CD_SS         =    @CUST_CD_SS                 " & vbNewLine _
                                                   & "AND    GDS.GOODS_CD_CUST      =    @GOODS_CD_CUST              " & vbNewLine

#End If

    ''' <summary>
    ''' 混在チェック①
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MIX_KIWARI_CHK_1 As String = "SELECT                                           " & vbNewLine _
                                               & "        COUNT(MAIN.SELECT_CNT)    AS    SELECT_CNT " & vbNewLine _
                                               & "  FROM (                                           " & vbNewLine _
                                               & "        SELECT                                     " & vbNewLine _
                                               & "             COUNT(TNK.KIWARI_KB)    AS SELECT_CNT " & vbNewLine

    ''' <summary>
    ''' 混在チェック①
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MIX_KIWARI_CHK_1_2 As String = ") MAIN                                           " & vbNewLine

    ''' <summary>
    ''' 混在チェック②
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MIX_KIWARI_CHK_2 As String = "SELECT                                           " & vbNewLine _
                                               & "        TNK.KIWARI_KB           AS    KIWARI_KB    " & vbNewLine


    'START YANAI 要望番号512
    '''' <summary>
    '''' 混在チェック
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_MIX_KIWARI_CHK As String = "FROM   $LM_MST$..M_GOODS    GDS                       " & vbNewLine _
    '                                           & "LEFT JOIN    $LM_MST$..M_TANKA    TNK              " & vbNewLine _
    '                                           & "ON     TNK.NRS_BR_CD    =    GDS.NRS_BR_CD         " & vbNewLine _
    '                                           & "AND    TNK.CUST_CD_L    =    GDS.CUST_CD_L         " & vbNewLine _
    '                                           & "AND    TNK.CUST_CD_M    =    GDS.CUST_CD_M         " & vbNewLine _
    '                                           & "AND    TNK.UP_GP_CD_1   =    GDS.UP_GP_CD_1        " & vbNewLine _
    '                                           & "WHERE                                              " & vbNewLine _
    '                                           & "       GDS.NRS_BR_CD    =    @NRS_BR_CD            " & vbNewLine _
    '                                           & "AND    GDS.CUST_CD_L    =    @CUST_CD_L            " & vbNewLine _
    '                                           & "AND    GDS.CUST_CD_M    =    @CUST_CD_M            " & vbNewLine _
    '                                           & "AND    GDS.GOODS_CD_NRS <>   @GOODS_CD_NRS         " & vbNewLine _
    '                                           & "GROUP BY TNK.KIWARI_KB                             " & vbNewLine
    ''' <summary>
    ''' 混在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MIX_KIWARI_CHK As String = "FROM   $LM_MST$..M_GOODS    GDS                       " & vbNewLine _
                                               & "LEFT JOIN    $LM_MST$..M_TANKA    TNK              " & vbNewLine _
                                               & "ON     TNK.NRS_BR_CD    =    GDS.NRS_BR_CD         " & vbNewLine _
                                               & "AND    TNK.CUST_CD_L    =    GDS.CUST_CD_L         " & vbNewLine _
                                               & "AND    TNK.CUST_CD_M    =    GDS.CUST_CD_M         " & vbNewLine _
                                               & "AND    TNK.UP_GP_CD_1   =    GDS.UP_GP_CD_1        " & vbNewLine _
                                               & "WHERE                                              " & vbNewLine _
                                               & "       GDS.NRS_BR_CD    =    @NRS_BR_CD            " & vbNewLine _
                                               & "AND    GDS.CUST_CD_L    =    @CUST_CD_L            " & vbNewLine _
                                               & "AND    GDS.CUST_CD_M    =    @CUST_CD_M            " & vbNewLine _
                                               & "--START KIM 要望番号1500                           " & vbNewLine _
                                               & "--AND    GDS.GOODS_CD_NRS <>   @GOODS_CD_NRS       " & vbNewLine _
                                               & "$GOODS_CD_NRS$                                     " & vbNewLine _
                                               & "--END KIM 要望番号1500                             " & vbNewLine _
                                               & "AND    TNK.KIWARI_KB IS NOT NULL                   " & vbNewLine _
                                               & "GROUP BY TNK.KIWARI_KB                             " & vbNewLine
    'END YANAI 要望番号512

    ''' <summary>
    ''' 坪貸請求先コード差異チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CHK_TUBO As String = "SELECT                                           " & vbNewLine _
                                         & "    COUNT(ZNE.SEIQTO_CD)  AS  SELECT_CNT         " & vbNewLine _
                                         & "FROM                                             " & vbNewLine _
                                         & "    $LM_MST$..M_ZONE    ZNE                      " & vbNewLine _
                                         & "LEFT JOIN    $LM_MST$..M_CUST    CST             " & vbNewLine _
                                         & "ON    CST.HOKAN_SEIQTO_CD    =    ZNE.SEIQTO_CD  " & vbNewLine _
                                         & "AND CST.SYS_DEL_FLG          =    0              " & vbNewLine _
                                         & "WHERE                                            " & vbNewLine _
                                         & "    CST.NRS_BR_CD       =    @NRS_BR_CD          " & vbNewLine _
                                         & "AND    CST.CUST_CD_L    =    @CUST_CD_L          " & vbNewLine _
                                         & "AND    CST.CUST_CD_M    =    @CUST_CD_M          " & vbNewLine _
                                         & "AND    CST.CUST_CD_S    =    @CUST_CD_S          " & vbNewLine _
                                         & "AND    CST.CUST_CD_SS   =    @CUST_CD_SS         " & vbNewLine

    'START YANAI 要望番号739
    '''' <summary>
    '''' 在庫データ存在チェック
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_EXIST_ZAIT As String = "SELECT COUNT(ZAITRS.NRS_BR_CD) AS SELECT_CNT                                                       " & vbNewLine _
    '                                       & " FROM $LM_TRN$..D_ZAI_TRS ZAITRS                                                                   " & vbNewLine _
    '                                       & " WHERE  ZAITRS.NRS_BR_CD   =  @NRS_BR_CD                                                           " & vbNewLine _
    '                                       & " AND ZAITRS.GOODS_CD_NRS = @GOODS_CD_NRS                                                           " & vbNewLine _
    '                                       & " AND ZAITRS.SYS_DEL_FLG = '0'                                                                      " & vbNewLine
    ''' <summary>
    ''' 在庫データ存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_ZAIT As String = "SELECT COUNT(ZAITRS.NRS_BR_CD) AS SELECT_CNT                                                       " & vbNewLine _
                                           & " FROM $LM_TRN$..D_ZAI_TRS ZAITRS                                                                   " & vbNewLine _
                                           & " WHERE  ZAITRS.NRS_BR_CD   =  @NRS_BR_CD                                                           " & vbNewLine _
                                           & " AND ZAITRS.GOODS_CD_NRS = @GOODS_CD_NRS                                                           " & vbNewLine _
                                           & " AND ZAITRS.SYS_DEL_FLG = '0'                                                                      " & vbNewLine
    'END YANAI 要望番号739

#End Region

#Region "新規登録"

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
                                                 & "     ,INNER_PKG_NB            " & vbNewLine _
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
                                                 & "     ,UNSO_HOKEN_YN  -- 2018/07/18  " & vbNewLine _
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
                                                 & "     ,OCR_GOODS_CD_CUST       " & vbNewLine _
                                                 & "     ,OCR_GOODS_CD_NM1        " & vbNewLine _
                                                 & "     ,OCR_GOODS_CD_NM2        " & vbNewLine _
                                                 & "     ,OCR_GOODS_CD_STD_IRIME  " & vbNewLine _
                                                 & "-- 要望番号2471対応           " & vbNewLine _
                                                 & "     ,OUTER_PKG               " & vbNewLine _
                                                 & "     ,WIDTH                   " & vbNewLine _
                                                 & "     ,HEIGHT                  " & vbNewLine _
                                                 & "     ,DEPTH                   " & vbNewLine _
                                                 & "     ,VOLUME                  " & vbNewLine _
                                                 & "     ,OCCUPY_VOLUME           " & vbNewLine _
                                                 & "     ,OUTKA_HASU_SAGYO_KB_1   " & vbNewLine _
                                                 & "     ,OUTKA_HASU_SAGYO_KB_2   " & vbNewLine _
                                                 & "     ,OUTKA_HASU_SAGYO_KB_3   " & vbNewLine _
                                                 & "     ,AVAL_YN                 " & vbNewLine _
                                                 & "     ,HIZYU                   " & vbNewLine _
                                                 & "     ,KOUATHUGAS_KB           " & vbNewLine _
                                                 & "     ,YAKUZIHO_KB             " & vbNewLine _
                                                 & "     ,SHOBOKIKEN_KB           " & vbNewLine _
                                                 & "     ,KAIYOUOSEN_KB           " & vbNewLine _
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
                                                 & "     ,@INNER_PKG_NB           " & vbNewLine _
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
                                                 & "     ,@UNSO_HOKEN_YN  --2018/07/18  " & vbNewLine _
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
                                                 & "     ,@OCR_GOODS_CD_CUST      " & vbNewLine _
                                                 & "     ,@OCR_GOODS_CD_NM1       " & vbNewLine _
                                                 & "     ,@OCR_GOODS_CD_NM2       " & vbNewLine _
                                                 & "     ,@OCR_GOODS_CD_STD_IRIME " & vbNewLine _
                                                 & "-- 要望番号2471対応           " & vbNewLine _
                                                 & "     ,@OUTER_PKG              " & vbNewLine _
                                                 & "     ,@WIDTH                  " & vbNewLine _
                                                 & "     ,@HEIGHT                 " & vbNewLine _
                                                 & "     ,@DEPTH                  " & vbNewLine _
                                                 & "     ,@ACTUAL_VOLUME          " & vbNewLine _
                                                 & "     ,@OCCUPY_VOLUME          " & vbNewLine _
                                                 & "     ,@OUTKA_HASU_SAGYO_KB_1  " & vbNewLine _
                                                 & "     ,@OUTKA_HASU_SAGYO_KB_2  " & vbNewLine _
                                                 & "     ,@OUTKA_HASU_SAGYO_KB_3  " & vbNewLine _
                                                 & "     ,@AVAL_YN                " & vbNewLine _
                                                 & "     ,@HIZYU                  " & vbNewLine _
                                                 & "     ,@KOUATHUGAS_KB          " & vbNewLine _
                                                 & "     ,@YAKUZIHO_KB            " & vbNewLine _
                                                 & "     ,@SHOBOKIKEN_KB          " & vbNewLine _
                                                 & "     ,@KAIYOUOSEN_KB          " & vbNewLine _
                                                 & "    )                         " & vbNewLine
    ''' <summary>
    ''' 商品明細マスタ新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_GOODS_DLT As String = "INSERT INTO                   " & vbNewLine _
                                                 & "    $LM_MST$..M_GOODS_DETAILS " & vbNewLine _
                                                 & "    (                         " & vbNewLine _
                                                 & "      NRS_BR_CD               " & vbNewLine _
                                                 & "     ,GOODS_CD_NRS            " & vbNewLine _
                                                 & "     ,GOODS_CD_NRS_EDA        " & vbNewLine _
                                                 & "     ,SUB_KB                  " & vbNewLine _
                                                 & "     ,SET_NAIYO               " & vbNewLine _
                                                 & "     ,REMARK                  " & vbNewLine _
                                                 & "     ,SYS_ENT_DATE            " & vbNewLine _
                                                 & "     ,SYS_ENT_TIME            " & vbNewLine _
                                                 & "     ,SYS_ENT_PGID            " & vbNewLine _
                                                 & "     ,SYS_ENT_USER            " & vbNewLine _
                                                 & "     ,SYS_UPD_DATE            " & vbNewLine _
                                                 & "     ,SYS_UPD_TIME            " & vbNewLine _
                                                 & "     ,SYS_UPD_PGID            " & vbNewLine _
                                                 & "     ,SYS_UPD_USER            " & vbNewLine _
                                                 & "     ,SYS_DEL_FLG             " & vbNewLine _
                                                 & "    )                         " & vbNewLine _
                                                 & "VALUES                        " & vbNewLine _
                                                 & "    (                         " & vbNewLine _
                                                 & "      @NRS_BR_CD              " & vbNewLine _
                                                 & "     ,@GOODS_CD_NRS           " & vbNewLine _
                                                 & "     ,@GOODS_CD_NRS_EDA       " & vbNewLine _
                                                 & "     ,@SUB_KB                 " & vbNewLine _
                                                 & "     ,@SET_NAIYO              " & vbNewLine _
                                                 & "     ,@REMARK                 " & vbNewLine _
                                                 & "     ,@SYS_ENT_DATE           " & vbNewLine _
                                                 & "     ,@SYS_ENT_TIME           " & vbNewLine _
                                                 & "     ,@SYS_ENT_PGID           " & vbNewLine _
                                                 & "     ,@SYS_ENT_USER           " & vbNewLine _
                                                 & "     ,@SYS_UPD_DATE           " & vbNewLine _
                                                 & "     ,@SYS_UPD_TIME           " & vbNewLine _
                                                 & "     ,@SYS_UPD_PGID           " & vbNewLine _
                                                 & "     ,@SYS_UPD_USER           " & vbNewLine _
                                                 & "     ,@SYS_DEL_FLG            " & vbNewLine _
                                                 & "    )                         " & vbNewLine

#End Region

#Region "他荷主"

    '2015.10.02 他荷主対応START

    ''' <summary>
    ''' 商品マスタ重複チェック処理(荷主コード、商品コード、入目の関連チェック(件数取得))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_FURI_GOODSM As String = "SELECT                                                         " & vbNewLine _
                                                   & "       COUNT(GDS.GOODS_CD_NRS)     AS    SELECT_CNT            " & vbNewLine _
                                                   & "FROM                                                           " & vbNewLine _
                                                   & "     $LM_MST$..M_GOODS    GDS                                  " & vbNewLine _
                                                   & "WHERE                                                          " & vbNewLine _
                                                   & "       GDS.NRS_BR_CD          =    @NRS_BR_CD                  " & vbNewLine _
                                                   & "AND    GDS.CUST_CD_L          =    @CUST_CD_L                  " & vbNewLine _
                                                   & "AND    GDS.CUST_CD_M          =    @CUST_CD_M                  " & vbNewLine _
                                                   & "--AND    GDS.CUST_CD_S          =    @CUST_CD_S                  " & vbNewLine _
                                                   & "--AND    GDS.CUST_CD_SS         =    @CUST_CD_SS                 " & vbNewLine _
                                                   & "AND    GDS.GOODS_CD_CUST      =    @GOODS_CD_CUST              " & vbNewLine _
                                                   & "AND    GDS.STD_IRIME_NB       =    @STD_IRIME_NB               " & vbNewLine

    ''' <summary>
    ''' 商品マスタ新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TANINUSI_GOODS_M As String = "INSERT INTO                      " & vbNewLine _
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
                                                 & "     ,INNER_PKG_NB            " & vbNewLine _
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
                                                 & "     ,UNSO_HOKEN_YN           " & vbNewLine _
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
                                                 & "     ,OCR_GOODS_CD_CUST       " & vbNewLine _
                                                 & "     ,OCR_GOODS_CD_NM1        " & vbNewLine _
                                                 & "     ,OCR_GOODS_CD_NM2        " & vbNewLine _
                                                 & "     ,OCR_GOODS_CD_STD_IRIME  " & vbNewLine _
                                                 & "-- 要望番号2471対応           " & vbNewLine _
                                                 & "     ,OUTER_PKG               " & vbNewLine _
                                                 & "     ,HIZYU                   " & vbNewLine _
                                                 & "     ,KOUATHUGAS_KB           " & vbNewLine _
                                                 & "     ,YAKUZIHO_KB             " & vbNewLine _
                                                 & "     ,SHOBOKIKEN_KB           " & vbNewLine _
                                                 & "     ,KAIYOUOSEN_KB           " & vbNewLine _
                                                 & "    )                         " & vbNewLine _
                                                 & "VALUES                        " & vbNewLine _
                                                 & "    (                         " & vbNewLine _
                                                 & "      @NRS_BR_CD              " & vbNewLine _
                                                 & "     ,@GOODS_CD_NRS_FURI      " & vbNewLine _
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
                                                 & "     ,@INNER_PKG_NB           " & vbNewLine _
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
                                                 & "     ,@UNSO_HOKEN_YN  --2018/07/18   " & vbNewLine _
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
                                                 & "     ,@OCR_GOODS_CD_CUST      " & vbNewLine _
                                                 & "     ,@OCR_GOODS_CD_NM1       " & vbNewLine _
                                                 & "     ,@OCR_GOODS_CD_NM2       " & vbNewLine _
                                                 & "     ,@OCR_GOODS_CD_STD_IRIME " & vbNewLine _
                                                 & "-- 要望番号2471対応           " & vbNewLine _
                                                 & "     ,@OUTER_PKG              " & vbNewLine _
                                                 & "     ,@HIZYU                  " & vbNewLine _
                                                 & "     ,@KOUATHUGAS_KB          " & vbNewLine _
                                                 & "     ,@YAKUZIHO_KB            " & vbNewLine _
                                                 & "     ,@SHOBOKIKEN_KB          " & vbNewLine _
                                                 & "     ,@KAIYOUOSEN_KB          " & vbNewLine _
                                                 & "    )                         " & vbNewLine


    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_FURIKAE_GOODS_M As String = "INSERT INTO $LM_MST$..M_FURI_GOODS    " & vbNewLine _
                                       & "(                                            " & vbNewLine _
                                       & "       NRS_BR_CD                             " & vbNewLine _
                                       & "      ,CD_NRS                                " & vbNewLine _
                                       & "      ,CD_NRS_TO                             " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                          " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                          " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                          " & vbNewLine _
                                       & "      ,SYS_ENT_USER                          " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                          " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                          " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                          " & vbNewLine _
                                       & "      ,SYS_UPD_USER                          " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                           " & vbNewLine _
                                       & "      ) VALUES (                             " & vbNewLine _
                                       & "       @NRS_BR_CD                            " & vbNewLine _
                                       & "      ,@GOODS_CD_NRS                         " & vbNewLine _
                                       & "      ,@GOODS_CD_NRS_FURI                    " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                         " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                         " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                         " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                         " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                         " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                         " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                         " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                         " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                          " & vbNewLine _
                                       & ")                                            " & vbNewLine

#End Region

#Region "更新"

    ''' <summary>
    ''' 商品マスタ更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_GOODS_M As String = "UPDATE                                                       " & vbNewLine _
                                               & "    $LM_MST$..M_GOODS                                      " & vbNewLine _
                                               & "SET                                                        " & vbNewLine _
                                               & "      NRS_BR_CD               =    @NRS_BR_CD              " & vbNewLine _
                                               & "     ,GOODS_CD_NRS            =    @GOODS_CD_NRS           " & vbNewLine _
                                               & "     ,CUST_CD_L               =    @CUST_CD_L              " & vbNewLine _
                                               & "     ,CUST_CD_M               =    @CUST_CD_M              " & vbNewLine _
                                               & "     ,CUST_CD_S               =    @CUST_CD_S              " & vbNewLine _
                                               & "     ,CUST_CD_SS              =    @CUST_CD_SS             " & vbNewLine _
                                               & "     ,GOODS_CD_CUST           =    @GOODS_CD_CUST          " & vbNewLine _
                                               & "     ,SEARCH_KEY_1            =    @SEARCH_KEY_1           " & vbNewLine _
                                               & "     ,SEARCH_KEY_2            =    @SEARCH_KEY_2           " & vbNewLine _
                                               & "     ,CUST_COST_CD1           =    @CUST_COST_CD1          " & vbNewLine _
                                               & "     ,CUST_COST_CD2           =    @CUST_COST_CD2          " & vbNewLine _
                                               & "     ,JAN_CD                  =    @JAN_CD                 " & vbNewLine _
                                               & "     ,GOODS_NM_1              =    @GOODS_NM_1             " & vbNewLine _
                                               & "     ,GOODS_NM_2              =    @GOODS_NM_2             " & vbNewLine _
                                               & "     ,GOODS_NM_3              =    @GOODS_NM_3             " & vbNewLine _
                                               & "     ,UP_GP_CD_1              =    @UP_GP_CD_1             " & vbNewLine _
                                               & "     ,SHOBO_CD                =    @SHOBO_CD               " & vbNewLine _
                                               & "     ,KIKEN_KB                =    @KIKEN_KB               " & vbNewLine _
                                               & "     ,UN                      =    @UN                     " & vbNewLine _
                                               & "     ,PG_KB                   =    @PG_KB                  " & vbNewLine _
                                               & "     ,CLASS_1                 =    @CLASS_1                " & vbNewLine _
                                               & "     ,CLASS_2                 =    @CLASS_2                " & vbNewLine _
                                               & "     ,CLASS_3                 =    @CLASS_3                " & vbNewLine _
                                               & "     ,CHEM_MTRL_KB            =    @CHEM_MTRL_KB           " & vbNewLine _
                                               & "     ,DOKU_KB                 =    @DOKU_KB                " & vbNewLine _
                                               & "     ,GAS_KANRI_KB            =    @GAS_KANRI_KB           " & vbNewLine _
                                               & "     ,ONDO_KB                 =    @ONDO_KB                " & vbNewLine _
                                               & "     ,UNSO_ONDO_KB            =    @UNSO_ONDO_KB           " & vbNewLine _
                                               & "     ,ONDO_MX                 =    @ONDO_MX                " & vbNewLine _
                                               & "     ,ONDO_MM                 =    @ONDO_MM                " & vbNewLine _
                                               & "     ,ONDO_STR_DATE           =    @ONDO_STR_DATE          " & vbNewLine _
                                               & "     ,ONDO_END_DATE           =    @ONDO_END_DATE          " & vbNewLine _
                                               & "     ,ONDO_UNSO_STR_DATE      =    @ONDO_UNSO_STR_DATE     " & vbNewLine _
                                               & "     ,ONDO_UNSO_END_DATE      =    @ONDO_UNSO_END_DATE     " & vbNewLine _
                                               & "     ,KYOKAI_GOODS_KB         =    @KYOKAI_GOODS_KB        " & vbNewLine _
                                               & "     ,ALCTD_KB                =    @ALCTD_KB               " & vbNewLine _
                                               & "     ,NB_UT                   =    @NB_UT                  " & vbNewLine _
                                               & "     ,PKG_NB                  =    @PKG_NB                 " & vbNewLine _
                                               & "     ,PKG_UT                  =    @PKG_UT                 " & vbNewLine _
                                               & "     ,PLT_PER_PKG_UT          =    @PLT_PER_PKG_UT         " & vbNewLine _
                                               & "     ,INNER_PKG_NB            =    @INNER_PKG_NB           " & vbNewLine _
                                               & "     ,STD_IRIME_NB            =    @STD_IRIME_NB           " & vbNewLine _
                                               & "     ,STD_IRIME_UT            =    @STD_IRIME_UT           " & vbNewLine _
                                               & "     ,STD_WT_KGS              =    @STD_WT_KGS             " & vbNewLine _
                                               & "     ,STD_CBM                 =    @STD_CBM                " & vbNewLine _
                                               & "     ,INKA_KAKO_SAGYO_KB_1    =    @INKA_KAKO_SAGYO_KB_1   " & vbNewLine _
                                               & "     ,INKA_KAKO_SAGYO_KB_2    =    @INKA_KAKO_SAGYO_KB_2   " & vbNewLine _
                                               & "     ,INKA_KAKO_SAGYO_KB_3    =    @INKA_KAKO_SAGYO_KB_3   " & vbNewLine _
                                               & "     ,INKA_KAKO_SAGYO_KB_4    =    @INKA_KAKO_SAGYO_KB_4   " & vbNewLine _
                                               & "     ,INKA_KAKO_SAGYO_KB_5    =    @INKA_KAKO_SAGYO_KB_5   " & vbNewLine _
                                               & "     ,OUTKA_KAKO_SAGYO_KB_1   =    @OUTKA_KAKO_SAGYO_KB_1  " & vbNewLine _
                                               & "     ,OUTKA_KAKO_SAGYO_KB_2   =    @OUTKA_KAKO_SAGYO_KB_2  " & vbNewLine _
                                               & "     ,OUTKA_KAKO_SAGYO_KB_3   =    @OUTKA_KAKO_SAGYO_KB_3  " & vbNewLine _
                                               & "     ,OUTKA_KAKO_SAGYO_KB_4   =    @OUTKA_KAKO_SAGYO_KB_4  " & vbNewLine _
                                               & "     ,OUTKA_KAKO_SAGYO_KB_5   =    @OUTKA_KAKO_SAGYO_KB_5  " & vbNewLine _
                                               & "     ,PKG_SAGYO               =    @PKG_SAGYO              " & vbNewLine _
                                               & "     ,TARE_YN                 =    @TARE_YN                " & vbNewLine _
                                               & "     ,SP_NHS_YN               =    @SP_NHS_YN              " & vbNewLine _
                                               & "     ,COA_YN                  =    @COA_YN                 " & vbNewLine _
                                               & "     ,LOT_CTL_KB              =    @LOT_CTL_KB             " & vbNewLine _
                                               & "     ,LT_DATE_CTL_KB          =    @LT_DATE_CTL_KB         " & vbNewLine _
                                               & "     ,CRT_DATE_CTL_KB         =    @CRT_DATE_CTL_KB        " & vbNewLine _
                                               & "     ,DEF_SPD_KB              =    @DEF_SPD_KB             " & vbNewLine _
                                               & "     ,KITAKU_AM_UT_KB         =    @KITAKU_AM_UT_KB        " & vbNewLine _
                                               & "     ,KITAKU_GOODS_UP         =    @KITAKU_GOODS_UP        " & vbNewLine _
                                               & "     ,ORDER_KB                =    @ORDER_KB               " & vbNewLine _
                                               & "     ,ORDER_NB                =    @ORDER_NB               " & vbNewLine _
                                               & "     ,SHIP_CD_L               =    @SHIP_CD_L              " & vbNewLine _
                                               & "     ,SKYU_MEI_YN             =    @SKYU_MEI_YN            " & vbNewLine _
                                               & "     ,HIKIATE_ALERT_YN        =    @HIKIATE_ALERT_YN       " & vbNewLine _
                                               & "     ,UNSO_HOKEN_YN        =       @UNSO_HOKEN_YN          " & vbNewLine _
                                               & "     ,OUTKA_ATT               =    @OUTKA_ATT              " & vbNewLine _
                                               & "     ,PRINT_NB                =    @PRINT_NB               " & vbNewLine _
                                               & "     ,CONSUME_PERIOD_DATE     =    @CONSUME_PERIOD_DATE    " & vbNewLine _
                                               & "     ,SYS_UPD_DATE            =    @SYS_UPD_DATE           " & vbNewLine _
                                               & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME           " & vbNewLine _
                                               & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID           " & vbNewLine _
                                               & "     ,SYS_UPD_USER            =    @SYS_UPD_USER           " & vbNewLine _
                                               & "     ,SIZE_KB                 =    @SIZE_KB                " & vbNewLine _
                                               & "     ,OCR_GOODS_CD_CUST		=	 @OCR_GOODS_CD_CUST      " & vbNewLine _
                                               & "     ,OCR_GOODS_CD_NM1		=    @OCR_GOODS_CD_NM1       " & vbNewLine _
                                               & "     ,OCR_GOODS_CD_NM2		=    @OCR_GOODS_CD_NM2       " & vbNewLine _
                                               & "     ,OCR_GOODS_CD_STD_IRIME  =    @OCR_GOODS_CD_STD_IRIME " & vbNewLine _
                                               & "-- 要望番号2471対応                                        " & vbNewLine _
                                               & "     ,OUTER_PKG               =    @OUTER_PKG              " & vbNewLine _
                                               & "     ,WIDTH                   =    @WIDTH                  " & vbNewLine _
                                               & "     ,HEIGHT                  =    @HEIGHT                 " & vbNewLine _
                                               & "     ,DEPTH                   =    @DEPTH                  " & vbNewLine _
                                               & "     ,VOLUME                  =    @ACTUAL_VOLUME          " & vbNewLine _
                                               & "     ,OCCUPY_VOLUME           =    @OCCUPY_VOLUME          " & vbNewLine _
                                               & "     ,OUTKA_HASU_SAGYO_KB_1   =    @OUTKA_HASU_SAGYO_KB_1  " & vbNewLine _
                                               & "     ,OUTKA_HASU_SAGYO_KB_2   =    @OUTKA_HASU_SAGYO_KB_2  " & vbNewLine _
                                               & "     ,OUTKA_HASU_SAGYO_KB_3   =    @OUTKA_HASU_SAGYO_KB_3  " & vbNewLine _
                                               & "     ,AVAL_YN                 =    @AVAL_YN                " & vbNewLine _
                                               & "     ,HIZYU                   =    @HIZYU                  " & vbNewLine _
                                               & "     ,KOUATHUGAS_KB           =    @KOUATHUGAS_KB          " & vbNewLine _
                                               & "     ,YAKUZIHO_KB             =    @YAKUZIHO_KB            " & vbNewLine _
                                               & "     ,SHOBOKIKEN_KB           =    @SHOBOKIKEN_KB          " & vbNewLine _
                                               & "     ,KAIYOUOSEN_KB           =    @KAIYOUOSEN_KB          " & vbNewLine _
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

#Region "危険品情報確認処理 SQL"

    ''' <summary>
    ''' 商品マスタ更新SQL(危険品情報確認処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_CONF_KIKEN_GOODS As String = "UPDATE                                           " & vbNewLine _
                                              & "    $LM_MST$..M_GOODS                                      " & vbNewLine _
                                              & "SET                                                        " & vbNewLine _
                                              & "      KIKEN_DATE              =    @SYS_UPD_DATE           " & vbNewLine _
                                              & "     ,KIKEN_USER_ID           =    @SYS_UPD_USER           " & vbNewLine _
                                              & "     ,SYS_UPD_DATE            =    @SYS_UPD_DATE           " & vbNewLine _
                                              & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME           " & vbNewLine _
                                              & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID           " & vbNewLine _
                                              & "     ,SYS_UPD_USER            =    @SYS_UPD_USER           " & vbNewLine _
                                              & "WHERE NRS_BR_CD               =    @NRS_BR_CD              " & vbNewLine _
                                              & "AND   GOODS_CD_NRS            =    @GOODS_CD_NRS           " & vbNewLine _
                                              & "AND   SYS_UPD_DATE            =    @HAITA_DATE             " & vbNewLine _
                                              & "AND   SYS_UPD_TIME            =    @HAITA_TIME             " & vbNewLine


#End Region

#Region "商品マスタ更新SQL(容積一括)"

    ''' <summary>
    ''' 商品マスタ更新SQL(容積一括)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_VOLUME As String = "UPDATE                                              " & vbNewLine _
                                              & "    $LM_MST$..M_GOODS                               " & vbNewLine _
                                              & "SET                                                 " & vbNewLine _
                                              & "      WIDTH                   =    @WIDTH           " & vbNewLine _
                                              & "     ,HEIGHT                  =    @HEIGHT          " & vbNewLine _
                                              & "     ,DEPTH                   =    @DEPTH           " & vbNewLine _
                                              & "     ,VOLUME                  =    @ACTUAL_VOLUME   " & vbNewLine _
                                              & "     ,OCCUPY_VOLUME           =    @OCCUPY_VOLUME   " & vbNewLine _
                                              & "     ,SYS_UPD_DATE            =    @SYS_UPD_DATE    " & vbNewLine _
                                              & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME    " & vbNewLine _
                                              & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID    " & vbNewLine _
                                              & "     ,SYS_UPD_USER            =    @SYS_UPD_USER    " & vbNewLine _
                                              & "WHERE NRS_BR_CD               =    @NRS_BR_CD       " & vbNewLine _
                                              & "AND   GOODS_CD_NRS            =    @GOODS_CD_NRS    " & vbNewLine _
                                              & "AND   SYS_UPD_DATE            =    @HAITA_DATE      " & vbNewLine _
                                              & "AND   SYS_UPD_TIME            =    @HAITA_TIME      " & vbNewLine


#End Region

#Region "X-Track"

    ''' <summary>
    ''' X-Track用存在チェック（SKU）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_XTRACK_CHK_SKU As String = "" _
            & "SELECT                               " & vbNewLine _
            & "  COUNT(*) AS SELECT_CNT             " & vbNewLine _
            & "FROM                                 " & vbNewLine _
            & "  LM_MST..M_CUST_DETAILS AS CDT      " & vbNewLine _
            & "LEFT JOIN                            " & vbNewLine _
            & "  ABM_DB..XM_GOODS AS GOD            " & vbNewLine _
            & "  ON  GOD.TENANT_CD = CDT.SET_NAIYO  " & vbNewLine _
            & "  AND GOD.SYS_DEL_FLG = '0'          " & vbNewLine _
            & "WHERE                                " & vbNewLine _
            & "      CDT.NRS_BR_CD = @NRS_BR_CD     " & vbNewLine _
            & "  AND CDT.CUST_CD = @CUST_CD_L       " & vbNewLine _
            & "  AND CDT.SUB_KB = 'A9'              " & vbNewLine _
            & "  AND CDT.SYS_DEL_FLG = '0'          " & vbNewLine _
            & "  AND GOD.GOODS_CD = @SKU            " & vbNewLine

    ''' <summary>
    ''' X-Track用存在チェック（原産国）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_XTRACK_CHK_GENSAN As String = "" _
            & "SELECT                           " & vbNewLine _
            & "  COUNT(*) AS SELECT_CNT         " & vbNewLine _
            & "FROM                             " & vbNewLine _
            & "  ABM_DB..Z_KBN                  " & vbNewLine _
            & "WHERE                            " & vbNewLine _
            & "      KBN_GROUP_CD = 'G10008'    " & vbNewLine _
            & "  AND KBN_LANG = 'ja'            " & vbNewLine _
            & "  AND KBN_NM1 = @GENSAN_NM       " & vbNewLine _
            & "  AND SYS_DEL_FLG = '0'          " & vbNewLine

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

#Region "編集処理"

    ''' <summary>
    ''' 商品マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function HaitaChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_HAITA_CHK)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "HaitaChk", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("SELECT_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 在庫データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データテーブル検索結果取得SQLの構築・発行</remarks>
    Private Function ExistZaiko(ByVal ds As DataSet) As DataSet

        'START YANAI 要望番号739
        ''区分マスタを検索し、保持変数に格納
        'Me.CreateKbnDataSet()
        'END YANAI 要望番号739

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_EXIST_ZAIKO)
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString(), True)

        'START YANAI 要望番号739
        ''営業所毎にサーバーを切り替える
        'Dim dataRows() As DataRow = Me._kbnDs.Tables("Z_KBN").Select("KBN_NM3 = '" & Me._Row.Item("USER_BR_CD").ToString() & "'")
        'Dim serverAcFlg As String = dataRows(0).Item("KBN_NM4").ToString

        'Select Case serverAcFlg
        '    Case "00"
        '        'サーバーの切り替えを行う
        '        ds = Me.ExistZaiko2(ds, sql)
        '    Case "01"
        'END YANAI 要望番号739
        '現在のサーバのまま検索を行う
        ds = Me.ExistZaiko1(ds, sql)
        'START YANAI 要望番号739
        'End Select
        'END YANAI 要望番号739

        Return ds

    End Function

    ''' <summary>
    ''' 在庫データ存在チェック(現在のサーバ接続)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データテーブル検索結果取得SQLの構築・発行</remarks>
    Private Function ExistZaiko1(ByVal ds As DataSet, ByVal sql As String) As DataSet

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistZaikoChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "ExistZaiko1", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
#If False Then  'UPD 2018/06/01
        If Convert.ToInt32(reader("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("W150", New String() {"在庫データ", "編集"})
        End If

#Else
        If Convert.ToInt32(reader("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("W150", New String() {"在庫データ", "編集"})
        ElseIf Convert.ToInt32(reader("REC_CNT")) > 0 Then
            MyBase.SetMessage("W150", New String() {"在庫0でも在庫レコード", "編集"})

        End If

        'Add Start 2019/07/31 要望管理006855
        Dim dtZaiko As DataTable = ds.Tables("LMM100ZAIKO")
        dtZaiko.Clear()
        Dim drZaiko As DataRow = dtZaiko.NewRow
        drZaiko.Item("SUM_PORA_ZAI_NB") = CStr(reader("SELECT_CNT"))
        drZaiko.Item("REC_CNT") = CStr(reader("REC_CNT"))
        dtZaiko.Rows.Add(drZaiko)
        'Add End   2019/07/31 要望管理006855
#End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 在庫データ存在チェック(旧サーバ接続)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データテーブル検索結果取得SQLの構築・発行</remarks>
    Private Function ExistZaiko2(ByVal ds As DataSet, ByVal sql As String) As DataSet

        '******************************** LMSコネクション開始 *****************************
        Using Me._LMS1

            Try

                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(Me._Row.Item("USER_BR_CD").ToString())

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(sql, Me._LMS1)

                'SQLパラメータ初期化/設定
                Call Me.SetParamExistZaikoChk()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMM100DAC", "ExistZaiko2", cmd)

                'SQLの発行
                Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                cmd.Parameters.Clear()

                '処理件数の設定
                reader.Read()

                'エラーメッセージの設定
                If Convert.ToInt32(reader("SELECT_CNT")) > 0 Then
                    MyBase.SetMessage("W150", New String() {"在庫データ", "編集"})
                End If

                reader.Close()

                Return ds

            Catch
                Throw
            Finally
                'LMSVer1のClose処理
                Call Me.CloseConnectionLMS1()

            End Try
        End Using

    End Function

    ''' <summary>
    ''' 在庫データ存在チェック(荷主コードS・SS 編集可否判定用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ExistCustZaiko(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100IN")

        ' IN Table の条件 row の格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_EXIST_CUST_ZAIKO)
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString(), True)

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()
            With Me._Row
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            End With

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                ' 取得データの格納先をマッピング
                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("ZAI_REC_NO", "ZAI_REC_NO")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM100CUST_ZAIKO")

            End Using

            cmd.Parameters.Clear()

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '主キー
        Call Me.SetParamPrimaryKeyGoodsM()

        '排他項目
        Call Me.SetParamHaita()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(在庫データ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistZaikoChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '主キー
        Call Me.SetParamPrimaryKeyGoodsM()

    End Sub

#End Region

#Region "削除/復活処理"

    ''' <summary>
    ''' 商品マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ更新SQLの構築・発行</remarks>
    Private Function DeleteGoodsData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM100DAC.SQL_UPD_DEL_GOODS_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdDelGoods()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "DeleteGoodsData", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))
        If MyBase.GetResultCount < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 商品明細マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ更新SQLの構築・発行</remarks>
    Private Function DeleteGoodsDtlData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM100DAC.SQL_UPD_DEL_GOODS_DTL)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdDelGoods()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "DeleteGoodsDtlData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(商品マスタ論理削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdDelGoods()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

        End With

        '主キー
        Call Me.SetParamPrimaryKeyGoodsM()

        '排他項目
        Call Me.SetParamHaita()

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(商品明細マスタ論理削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdDelGoodsDtl()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

        End With

        '主キー
        Call Me.SetParamPrimaryKeyGoodsM()

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

    End Sub

#End Region

#Region "単価一括変更処理"

    ''' <summary>
    ''' 単価一括変更処理(商品マスタ更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateIkkatu(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM100DAC.SQL_UPDATE_IKKATU)  '更新用SQL

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'メッセージIDの初期化
        MyBase.SetMessage(String.Empty)

        '保管料存在チェック
        Call Me.ExistTankaMOndoKbn(ds)
        Dim errorFlg As Integer = MyBase.GetResultCount

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            '保管料存在チェックでエラーの場合、温度管理区分により処理を分ける
            If errorFlg <> 0 _
            AndAlso Me._Row.Item("ONDO_KB").Equals("02") Then
                Select Case errorFlg
                    Case 1 '現在適用中の単価レコードの保管料が0円
                        MyBase.SetMessageStore("00" _
                                             , "E215" _
                                             , New String() {"単価マスタ", "温度管理有り", "保管料単価"} _
                                             , Me._Row.Item("RECORD_NO").ToString() _
                                             , "商品コード" _
                                             , Me._Row.Item("GOODS_CD_CUST").ToString())

                    Case 2 '未来適用分の単価レコードの保管料が0円
                        MyBase.SetMessageStore("00" _
                                             , "E215" _
                                             , New String() {"未来適用開始分単価マスタ", "温度管理有り", "保管料単価"} _
                                             , Me._Row.Item("RECORD_NO").ToString() _
                                             , "商品コード" _
                                             , Me._Row.Item("GOODS_CD_CUST").ToString())


                End Select

            Else

                'SQLパラメータ初期化/設定
                Call Me.SetParamUpdateIkkatu()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMM100DAC", "UpdateIkkatu", cmd)

                'SQLの発行
                If MyBase.GetUpdateResult(cmd) < 1 Then
                    MyBase.SetMessageStore("00" _
                                           , "E011" _
                                           ,
                                           , Me._Row.Item("RECORD_NO").ToString() _
                                           , "商品コード" _
                                           , Me._Row.Item("GOODS_CD_CUST").ToString())

                End If

                cmd.Parameters.Clear()

            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(単価一括変更用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateIkkatu()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UP_GP_CD_1", .Item("UP_GP_CD_1").ToString(), DBDataType.NVARCHAR))

        End With

        '主キー
        Call Me.SetParamPrimaryKeyGoodsM()

        '排他項目
        Call Me.SetParamHaita()

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

    End Sub

#End Region

    'START YANAI 要望番号372
#Region "荷主一括変更処理"
    ''' <summary>
    ''' 商品マスタ荷主一括変更処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateNinushi(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_UPDATE_NINUSHI_IKKATU)   'SQL構築(Update用)
        Call Me.SetParamUpdateNinushiIkkatu()                    '更新用パラメータ設定
        Call Me.SetParamCommonSystemUpd()                        'システム共通更新項目

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "UpdateNinushi", cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetUpdateResult(cmd)

        '処理件数の設定
        If reader < 1 = True Then
            MyBase.SetMessage("E011")
            MyBase.SetMessageStore("00" _
                                   , "E011" _
                                   ,
                                   , Me._Row.Item("RECORD_NO").ToString() _
                                   , "商品コード" _
                                   , Me._Row.Item("GOODS_CD_CUST").ToString())
        Else
            MyBase.SetMessage(Nothing)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 荷主一括更新用SQLパラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateNinushiIkkatu()

        'パラメータ設定
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

#End Region
    'END YANAI 要望番号372

    '2015.10.02 他荷主対応START
#Region "他荷主処理"

#Region "チェック"

    ''' <summary>
    ''' 商品マスタ存在チェック(振替先)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistGoodsMTaninusi(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_EXIST_FURI_GOODSM)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamGoodsMCustChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "ExistGoodsMTaninusi", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        If MyBase.GetResultCount > 0 Then
            'エラーメッセージ
            'MyBase.SetMessage("E160", New String() {"振替先", "商品"})
            MyBase.SetMessageStore(LMM100DAC.GUIDANCE_KBN, "E160", New String() {"振替先", "商品"}, Me._Row.Item("RECORD_NO").ToString(), LMM100DAC.EXCEL_COL_TITLE, Me._Row.Item("GOODS_CD_CUST").ToString())
        End If

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(商品マスタ存在チェック(荷主コード、商品コード、入目の関連チェック))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGoodsMCustTaninusiChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME_NB", .Item("STD_IRIME_NB").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "他荷主処理(新規登録)"

    ''' <summary>
    ''' 商品マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertTaninusiMgoods(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM100DAC.SQL_INSERT_TANINUSI_GOODS_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsertGoods()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "InsertTaninusiMgoods", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 振替商品対象マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ更新SQLの構築・発行</remarks>
    Private Function InsertTaninusiFuriGoods(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM100DAC.SQL_INSERT_FURIKAE_GOODS_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsertGoods()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "InsertTaninusiFuriGoods", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))
        If MyBase.GetResultCount < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

#End Region

#End Region
    '2015.10.02 他荷主対応END

#Region "検索処理"

    ''' <summary>
    ''' 商品マスタ検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_SELECT_COUNT_SELECT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM100DAC.SQL_SELECT_DATA_FROM)        'SQL構築(カウント用FROM句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "SelectData", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMM100IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_SELECT_DATA_SELECT)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM100DAC.SQL_SELECT_DATA_FROM)        'SQL構築(データ抽出用FROM句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM100DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("CUST_COST_CD1", "CUST_COST_CD1")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("GOODS_NM_2", "GOODS_NM_2")
        map.Add("GOODS_NM_3", "GOODS_NM_3")
        map.Add("UP_GP_CD_1", "UP_GP_CD_1")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("SHOBO_INFO", "SHOBO_INFO")
        map.Add("KIKEN_KB", "KIKEN_KB")
        map.Add("UN", "UN")
        map.Add("PG_KB", "PG_KB")
        map.Add("CLASS_1", "CLASS_1")
        map.Add("CLASS_2", "CLASS_2")
        map.Add("CLASS_3", "CLASS_3")
        map.Add("CHEM_MTRL_KB", "CHEM_MTRL_KB")
        map.Add("DOKU_KB", "DOKU_KB")
        map.Add("DOKU_KB_NM", "DOKU_KB_NM")
        map.Add("GAS_KANRI_KB", "GAS_KANRI_KB")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_KB_NM", "ONDO_KB_NM")
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
        map.Add("NB_UT_NM", "NB_UT_NM")
        map.Add("NT_GR_CONV_RATE", "NT_GR_CONV_RATE")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("PKG_UT_NM", "PKG_UT_NM")
        map.Add("PLT_PER_PKG_UT", "PLT_PER_PKG_UT")
        map.Add("INNER_PKG_NB", "INNER_PKG_NB")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("STD_IRIME_NM", "STD_IRIME_NM")
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
        map.Add("PKG_SAGYO_NM", "PKG_SAGYO_NM")
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
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("SKYU_MEI_YN", "SKYU_MEI_YN")
        map.Add("HIKIATE_ALERT_YN", "HIKIATE_ALERT_YN")
        map.Add("UNSO_HOKEN_YN", "UNSO_HOKEN_YN")           'ADD 2018/07/18
        map.Add("OUTKA_ATT", "OUTKA_ATT")
        map.Add("PRINT_NB", "PRINT_NB")
        map.Add("CONSUME_PERIOD_DATE", "CONSUME_PERIOD_DATE")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("SEIQTO_BUSYO_NM", "SEIQTO_BUSYO_NM")
        map.Add("STR_DATE", "STR_DATE")
        map.Add("STORAGE_1", "STORAGE_1")
        map.Add("STORAGE_KB1", "STORAGE_KB1")
        map.Add("STORAGE_2", "STORAGE_2")
        map.Add("STORAGE_KB2", "STORAGE_KB2")
        map.Add("HANDLING_IN", "HANDLING_IN")
        map.Add("HANDLING_IN_KB", "HANDLING_IN_KB")
        map.Add("HANDLING_OUT", "HANDLING_OUT")
        map.Add("HANDLING_OUT_KB", "HANDLING_OUT_KB")
        map.Add("MINI_TEKI_IN_AMO", "MINI_TEKI_IN_AMO")
        map.Add("MINI_TEKI_OUT_AMO", "MINI_TEKI_OUT_AMO")
        map.Add("KIWARI_KB", "KIWARI_KB")
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        map.Add("SIZE_KB", "SIZE_KB")
        '20150729 常平add
        map.Add("OCR_GOODS_CD_CUST", "OCR_GOODS_CD_CUST")
        map.Add("OCR_GOODS_CD_NM1", "OCR_GOODS_CD_NM1")
        map.Add("OCR_GOODS_CD_NM2", "OCR_GOODS_CD_NM2")
        map.Add("OCR_GOODS_CD_STD_IRIME", "OCR_GOODS_CD_STD_IRIME")
#If True Then ' 要望番号2471対応 added 2015.12.14 inoue
        map.Add("OUTER_PKG", "OUTER_PKG")
#End If
        map.Add("HIZYU", "HIZYU")
        map.Add("KOUATHUGAS_KB", "KOUATHUGAS_KB")
        map.Add("YAKUZIHO_KB", "YAKUZIHO_KB")
        map.Add("SHOBOKIKEN_KB", "SHOBOKIKEN_KB")
        map.Add("KAIYOUOSEN_KB", "KAIYOUOSEN_KB")
        map.Add("KAIYOUOSEN_KB_NM", "KAIYOUOSEN_KB_NM")
        map.Add("KIKEN_DATE", "KIKEN_DATE")
        map.Add("KIKEN_USER_NM", "KIKEN_USER_NM")

        map.Add("WIDTH", "WIDTH")
        map.Add("HEIGHT", "HEIGHT")
        map.Add("DEPTH", "DEPTH")
        map.Add("ACTUAL_VOLUME", "ACTUAL_VOLUME")
        map.Add("OCCUPY_VOLUME", "OCCUPY_VOLUME")
        map.Add("CYL_FLG", "CYL_FLG")

        '要望対応1995 端数出荷時作業区分対応
        map.Add("OUTKA_HASU_SAGYO_KB_1", "OUTKA_HASU_SAGYO_KB_1")
        map.Add("OUTKA_HASU_SAGYO_KB_2", "OUTKA_HASU_SAGYO_KB_2")
        map.Add("OUTKA_HASU_SAGYO_KB_3", "OUTKA_HASU_SAGYO_KB_3")

        map.Add("AVAL_YN", "AVAL_YN")               'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
        map.Add("AVAL_YN_NM", "AVAL_YN_NM")         'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM100OUT")

        Return ds

    End Function

#If True Then   'ADD 2020/02/26  010140【LMS】フィルメ誤出荷類似_EDI取込、出荷登録共に個別荷主機能使用(高度化和地)

    Private Function GetGoodsMcust(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_GET_GOODSM_CUST)        'SQL構築(データ抽出用Select句)
        If String.IsNullOrEmpty(Me._Row.Item("GOODS_CD_NRS").ToString()) = False Then
            Me._StrSql.Append("AND    GDS.GOODS_CD_NRS       <>   @GOODS_CD_NRS              " & vbNewLine)
        End If

        Call Me.SetConditionMasteGOODSM_CUSTSQL(inTbl)                 '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "GET_GOODSM_CUST", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("PKG_NB", "PKG_NB")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM100GoodsCust")

        Return ds

    End Function
#End If

    ''' <summary>
    ''' 商品明細マスタ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品明細マスタ検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectDtlListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_SELECT_DTL_DATA)
        Call Me.SetConditionMasterDtlSQL(inTbl)                   '条件設定
        Me._StrSql.Append(LMM100DAC.SQL_ORDERBY_DTL_DATA)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "SelectDtlListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_NRS_EDA", "GOODS_CD_NRS_EDA")
        map.Add("SUB_KB", "SUB_KB")
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("REMARK", "REMARK")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM100_GOODS_DETAILS")

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

            '【営業所コード：=】
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '【荷主コード(大)：LIKE 値%】
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.CUST_CD_L LIKE @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【荷主コード(中)：LIKE 値%】
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.CUST_CD_M LIKE @CUST_CD_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【荷主コード(小)：LIKE 値%】
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.CUST_CD_S LIKE @CUST_CD_S")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【荷主コード(極小)：LIKE 値%】
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.CUST_CD_SS LIKE @CUST_CD_SS")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【荷主名(大)：LIKE %値%】
            whereStr = .Item("CUST_NM_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.CUST_NM_L LIKE @CUST_NM_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【商品コード(荷主)：LIKE 値%】
            whereStr = .Item("GOODS_CD_CUST").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.GOODS_CD_CUST LIKE @GOODS_CD_CUST")
                andstr.Append(vbNewLine)
                'START YANAI 要望番号886
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                'END YANAI 要望番号886
            End If

            '【商品名1：LIKE %値%】
            whereStr = .Item("GOODS_NM_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.GOODS_NM_1 LIKE @GOODS_NM_1")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【単価グループコード1：LIKE 値%】
            whereStr = .Item("UP_GP_CD_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.UP_GP_CD_1 LIKE @UP_GP_CD_1")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UP_GP_CD_1", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【消防コード1：LIKE 値%】
            whereStr = .Item("SHOBO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.SHOBO_CD LIKE @SHOBO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHOBO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【毒劇区分：=】
            whereStr = .Item("DOKU_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.DOKU_KB = @DOKU_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOKU_KB", whereStr, DBDataType.CHAR))
            End If

            '【温度区分：=】
            whereStr = .Item("ONDO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.ONDO_KB = @ONDO_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_KB", whereStr, DBDataType.CHAR))
            End If

            '【包装単位：=】
            whereStr = .Item("PKG_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.PKG_UT = @PKG_UT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_UT", whereStr, DBDataType.CHAR))
            End If

            '【標準入目単位：=】
            whereStr = .Item("STD_IRIME_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.STD_IRIME_UT = @STD_IRIME_UT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME_UT", whereStr, DBDataType.CHAR))
            End If

            '【請求先コード：LIKE 値%】
            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.OYA_SEIQTO_CD LIKE @SEIQTO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【請求先会社名：LIKE %値%】
            whereStr = .Item("SEIQTO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  SQT.SEIQTO_NM LIKE @SEIQTO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【請求先部署名：LIKE %値%】
            whereStr = .Item("SEIQTO_BUSYO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  SQT.SEIQTO_BUSYO_NM LIKE @SEIQTO_BUSYO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_BUSYO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【削除フラグ：=】
            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.SYS_DEL_FLG = @SYS_DEL_FLG")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            '【単価マスタ情報取得条件】
            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If
            andstr.Append("  (TNK2.NRS_BR_CD IS NOT NULL OR TNK.UP_GP_CD_1 IS NULL)")
            andstr.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", Me.GetSystemDate(), DBDataType.CHAR))

            '【危険品情報最終確認者：LIKE %値%】
            whereStr = .Item("KIKEN_USER_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  USE3.USER_NM LIKE @KIKEN_USER_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIKEN_USER_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'ADD START 2018/11/09 要望番号002599
            '【実容積：=】
            whereStr = .Item("ACTUAL_VOLUME").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.VOLUME = @ACTUAL_VOLUME")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ACTUAL_VOLUME", whereStr, DBDataType.NUMERIC))
            End If

            '【占有容積：=】
            whereStr = .Item("OCCUPY_VOLUME").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.OCCUPY_VOLUME = @OCCUPY_VOLUME")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OCCUPY_VOLUME", whereStr, DBDataType.NUMERIC))
            End If
            'ADD END   2018/11/09 要望番号002599
#If True Then       'ADD 2019/04/22 'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
            whereStr = .Item("AVAL_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                If whereStr = "01" Then
                    '可の場合
                    andstr.Append("  GDS.AVAL_YN  in ('01','')")
                    andstr.Append(vbNewLine)

                Else
                    andstr.Append("  GDS.AVAL_YN = @AVAL_YN")
                    andstr.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AVAL_YN", whereStr, DBDataType.CHAR))

                End If
            End If

#End If

#If True Then   'ADD 2019/06/21 006318   【LMS】ハネウェル業務改善_商品マスタを荷主カテゴリ１(CustomerOrderNo)で検索
            '【荷主カテゴリ２1：LIKE %値%】
            whereStr = .Item("SEARCH_KEY_2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  GDS.SEARCH_KEY_2 LIKE @SEARCH_KEY_2")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_2", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

#End If

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

#If True Then   'ADD 2020/02/27 010140【LMS】フィルメ誤出荷類似_EDI取込、出荷登録共に個別荷主機能使用(高度化和地)
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasteGOODSM_CUSTSQL(ByVal dt As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.CHAR))

            '【荷主商品コード】
            whereStr = .Item("GOODS_CD_NRS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End If

    ''' <summary>
    ''' X-Track用存在チェック（SKU）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function XTrackChk_SKU(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100XTRACK_CHK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(LMM100DAC.SQL_SELECT_XTRACK_CHK_SKU)

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKU", Me._Row.Item("SKU").ToString, DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "XTrackChk_SKU", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' X-Track用存在チェック（原産国）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function XTrackChk_Gensan(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100XTRACK_CHK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(LMM100DAC.SQL_SELECT_XTRACK_CHK_GENSAN)

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GENSAN_NM", Me._Row.Item("GENSAN_NM").ToString, DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "XTrackChk_Gensan", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

#End Region

#Region "保存処理"

#Region "チェック"

    ''' <summary>
    ''' 単価マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>単価マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistTankaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_EXIST_TANKAM)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamTankaMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "ExistTankaM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        If MyBase.GetResultCount = 0 Then
            Call Me.ExistTankaMMirai()
            If MyBase.GetResultCount > 0 Then
                MyBase.SetMessage("E374", New String() {"単価グループコード", "未来の適用開始日しか存在しません"})
            Else
                MyBase.SetMessage("E079", New String() {"単価マスタ", "単価グループコード"})
            End If
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 単価マスタ存在チェック(未来データ存在チェック)
    ''' </summary>
    ''' <remarks>単価マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Sub ExistTankaMMirai()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_EXIST_TANKAM_MIRAI)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamTankaMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "ExistTankaMMirai", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

    End Sub

    ''' <summary>
    ''' 単価マスタ存在チェック(温度管理有り)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>単価マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistTankaMOndoKbn(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_EXIST_TANKAM_ONDO)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamTankaMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "ExistTankaMOndoKbn", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(0)  'エラー無し

        'エラーメッセージの設定
        If Convert.ToInt32(reader("STORAGE_2")) = 0 Then
            MyBase.SetResultCount(1) 'エラー有り(現在適用中のレコードの保管料が０円) 
        End If
        '混在チェック用に期割り区分取得
        Me._Row.Item("KIWARI_KB") = reader("KIWARI_KB")

        reader.Close()

        '未来適用開始のレコードに保管料が０円のレコードがある場合エラー
        Select Case Me._Row.Item("WARNING_FLG").ToString
            Case "1" '現在使用可能レコードの保管料が0円のワーニングでOK押下済み
                Call Me.ExistTankaMOndoKbnMirai()
            Case "2", "3", "4", "5" '既にワーニング回答済みの場合
                MyBase.SetResultCount(0)
            Case Else
                If MyBase.GetResultCount() = 0 Then
                    Call Me.ExistTankaMOndoKbnMirai()
                End If
        End Select

        Return ds

    End Function

    ''' <summary>
    ''' 単価マスタ存在チェック(温度管理有り(未来適用データ))
    ''' </summary>
    ''' <remarks>単価マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Sub ExistTankaMOndoKbnMirai()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_EXIST_TANKAM_ONDO_MIRAI)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamTankaMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "ExistTankaMOndoKbnMirai", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        'エラーメッセージの設定
        If Convert.ToInt32(reader("SELECT_CNT")) > 0 Then
            MyBase.SetResultCount(2)
        Else
            MyBase.SetResultCount(0)
        End If
        reader.Close()

    End Sub

    ''' <summary>
    ''' 商品マスタ重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistGoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_REPEAT_GOODSM)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamGoodsMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "ExistGoodsM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E010")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 商品マスタ重複チェック(荷主コード、商品コード関連チェック)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistGoodsMCust(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_REPEAT_GOODSM_CUST)
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

        MyBase.Logger.WriteSQLLog("LMM100DAC", "ExistGoodsMCust", cmd)

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
    ''' 混在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function MixKiwariChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Dim sql2 As StringBuilder = New StringBuilder()

        'START KIM 要望番号1500
        Dim strSqlChk As String = LMM100DAC.SQL_MIX_KIWARI_CHK
        Dim strReplace As String = String.Empty
        If String.IsNullOrEmpty(Me._Row.Item("GOODS_CD_NRS").ToString()) = False Then
            strReplace = "AND    GDS.GOODS_CD_NRS <>   @GOODS_CD_NRS "
        End If
        strSqlChk = strSqlChk.Replace("$GOODS_CD_NRS$", strReplace)
        'END KIM 要望番号1500

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_MIX_KIWARI_CHK_1)
        'START KIM 要望番号1500
        'Me._StrSql.Append(LMM100DAC.SQL_MIX_KIWARI_CHK)
        Me._StrSql.Append(strSqlChk)
        'END KIM 要望番号1500
        Me._StrSql.Append(LMM100DAC.SQL_MIX_KIWARI_CHK_1_2)

        sql2.Append(LMM100DAC.SQL_MIX_KIWARI_CHK_2)
        'START KIM 要望番号1500
        'sql2.Append(LMM100DAC.SQL_MIX_KIWARI_CHK)
        sql2.Append(strSqlChk)
        'END KIM 要望番号1500

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamMixKiwariChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "MixKiwariChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()

        'エラーメッセージの設定
        If MyBase.GetResultCount >= 2 Then
            MyBase.SetMessage("W135", New String() {"期割区分", "商品"})
        ElseIf MyBase.GetResultCount = 1 Then

            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql2.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            cmd.Parameters.Clear()

            'SQLパラメータ初期化/設定
            Call Me.SetParamMixKiwariChk()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            reader = MyBase.GetSelectResult(cmd)
            reader.Read()
            If reader("KIWARI_KB").Equals(Me._Row.Item("KIWARI_KB")) = False Then
                MyBase.SetMessage("W135", New String() {"期割区分", "商品"})
            End If
            reader.Close()
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 坪貸請求先コード差異チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ゾーンマスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function TuboChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_CHK_TUBO)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamTuboBeforeChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "TuboChk", cmd)

        'SQLの発行(修正前)
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Dim updateBefore As Integer = Convert.ToInt32(reader("SELECT_CNT"))
        reader.Close()

        cmd.Parameters.Clear()

        'SQLパラメータ初期化/設定
        Call Me.SetParamTuboAfterChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'SQLの発行(修正前)
        reader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Dim updateAfter As Integer = Convert.ToInt32(reader("SELECT_CNT"))
        reader.Close()

        '更新前件数と更新後件数のどちらかが"0"の場合にワーニングを設定する
        If updateBefore = 0 _
        OrElse updateAfter = 0 Then
            If updateBefore < updateAfter Then
                MyBase.SetMessage("W157", New String() {"坪貸対象外請求先", "坪貸対象請求先"})
            ElseIf updateAfter < updateBefore Then
                MyBase.SetMessage("W157", New String() {"坪貸対象請求先", "坪貸対象外請求先"})
            End If
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 在庫データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistZaiT(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_EXIST_ZAIT)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamZaiTrsChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "ExistZaiT", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        If MyBase.GetResultCount > 0 Then
            MyBase.SetMessage("E260", New String() {"在庫データ"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(単価マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamTankaMChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            '主キー
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UP_GP_CD_1", .Item("UP_GP_CD_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYSTEM_DATE", Me.GetSystemDate(), DBDataType.CHAR))

        End With

    End Sub

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME_NB", .Item("STD_IRIME_NB").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(混在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamMixKiwariChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(坪貸請求先コード差異チェック(編集前))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamTuboBeforeChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            '主キー
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CHK_CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CHK_CUST_CD_SS").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(坪貸請求先コード差異チェック(編集後))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamTuboAfterChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            '主キー
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(在庫データ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamZaiTrsChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))

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
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM100DAC.SQL_INSERT_GOODS_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsertGoods()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "InsertGoodsM", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 商品明細マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品明細マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertGoodsMDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100_GOODS_DETAILS")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM100DAC.SQL_INSERT_GOODS_DLT)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamInsertGoodsDtl()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM100DAC", "InsertGoodsMDtl", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 商品マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateGoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM100DAC.SQL_UPDATE_GOODS_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateGoods()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "UpdateGoodsM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))
        If MyBase.GetResultCount < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 商品明細マスタ物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品明細マスタ物理削除SQLの構築・発行</remarks>
    Private Function DeleteGoodsMDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM100DAC.SQL_DELETE_GOODS_DTL)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDeleteGoodsDtl()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "DeleteGoodsMDtl", cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

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
        Call Me.SetParamHaita()

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_1", .Item("SEARCH_KEY_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_2", .Item("SEARCH_KEY_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_COST_CD1", .Item("CUST_COST_CD1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_COST_CD2", .Item("CUST_COST_CD2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JAN_CD", .Item("JAN_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", .Item("GOODS_NM_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_2", .Item("GOODS_NM_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_3", .Item("GOODS_NM_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UP_GP_CD_1", .Item("UP_GP_CD_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHOBO_CD", .Item("SHOBO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIKEN_KB", .Item("KIKEN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UN", .Item("UN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PG_KB", .Item("PG_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CLASS_1", .Item("CLASS_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CLASS_2", .Item("CLASS_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CLASS_3", .Item("CLASS_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHEM_MTRL_KB", .Item("CHEM_MTRL_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOKU_KB", .Item("DOKU_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GAS_KANRI_KB", .Item("GAS_KANRI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_KB", .Item("ONDO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_MX", .Item("ONDO_MX").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_MM", .Item("ONDO_MM").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_STR_DATE", .Item("ONDO_STR_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_END_DATE", .Item("ONDO_END_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_UNSO_STR_DATE", .Item("ONDO_UNSO_STR_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_UNSO_END_DATE", .Item("ONDO_UNSO_END_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYOKAI_GOODS_KB", .Item("KYOKAI_GOODS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_NB", .Item("PKG_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_UT", .Item("PKG_UT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PLT_PER_PKG_UT", .Item("PLT_PER_PKG_UT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INNER_PKG_NB", .Item("INNER_PKG_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME_NB", .Item("STD_IRIME_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME_UT", .Item("STD_IRIME_UT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_WT_KGS", .Item("STD_WT_KGS").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_CBM", .Item("STD_CBM").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KAKO_SAGYO_KB_1", .Item("INKA_KAKO_SAGYO_KB_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KAKO_SAGYO_KB_2", .Item("INKA_KAKO_SAGYO_KB_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KAKO_SAGYO_KB_3", .Item("INKA_KAKO_SAGYO_KB_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KAKO_SAGYO_KB_4", .Item("INKA_KAKO_SAGYO_KB_4").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KAKO_SAGYO_KB_5", .Item("INKA_KAKO_SAGYO_KB_5").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KAKO_SAGYO_KB_1", .Item("OUTKA_KAKO_SAGYO_KB_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KAKO_SAGYO_KB_2", .Item("OUTKA_KAKO_SAGYO_KB_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KAKO_SAGYO_KB_3", .Item("OUTKA_KAKO_SAGYO_KB_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KAKO_SAGYO_KB_4", .Item("OUTKA_KAKO_SAGYO_KB_4").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KAKO_SAGYO_KB_5", .Item("OUTKA_KAKO_SAGYO_KB_5").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_SAGYO", .Item("PKG_SAGYO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARE_YN", .Item("TARE_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SP_NHS_YN", .Item("SP_NHS_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_CTL_KB", .Item("LOT_CTL_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE_CTL_KB", .Item("LT_DATE_CTL_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_CTL_KB", .Item("CRT_DATE_CTL_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEF_SPD_KB", .Item("DEF_SPD_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KITAKU_AM_UT_KB", .Item("KITAKU_AM_UT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KITAKU_GOODS_UP", .Item("KITAKU_GOODS_UP").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_KB", .Item("ORDER_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_NB", .Item("ORDER_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", .Item("SHIP_CD_L").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_MEI_YN", .Item("SKYU_MEI_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIKIATE_ALERT_YN", .Item("HIKIATE_ALERT_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_HOKEN_YN", .Item("UNSO_HOKEN_YN").ToString(), DBDataType.CHAR))            'ADD 2018/07/18
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_ATT", .Item("OUTKA_ATT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_NB", .Item("PRINT_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CONSUME_PERIOD_DATE", .Item("CONSUME_PERIOD_DATE").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            '20150730 常平add
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OCR_GOODS_CD_CUST", .Item("OCR_GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OCR_GOODS_CD_NM1", .Item("OCR_GOODS_CD_NM1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OCR_GOODS_CD_NM2", .Item("OCR_GOODS_CD_NM2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OCR_GOODS_CD_STD_IRIME", .Item("OCR_GOODS_CD_STD_IRIME").ToString(), DBDataType.NVARCHAR))
            '2015.10.02 他荷主対応START
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FURI", .Item("GOODS_CD_NRS_FURI").ToString(), DBDataType.CHAR))
            '2015.10.02 他荷主対応END

#If True Then ' 要望番号2471対応 added 2015.12.14 inoue
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTER_PKG", .Item("OUTER_PKG").ToString(), DBDataType.CHAR))
#End If
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIZYU", .Item("HIZYU").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOUATHUGAS_KB", .Item("KOUATHUGAS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YAKUZIHO_KB", .Item("YAKUZIHO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHOBOKIKEN_KB", .Item("SHOBOKIKEN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KAIYOUOSEN_KB", .Item("KAIYOUOSEN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WIDTH", .Item("WIDTH").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HEIGHT", .Item("HEIGHT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPTH", .Item("DEPTH").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ACTUAL_VOLUME", .Item("ACTUAL_VOLUME").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OCCUPY_VOLUME", .Item("OCCUPY_VOLUME").ToString(), DBDataType.NUMERIC))

            '要望対応1995 端数出荷時作業区分対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU_SAGYO_KB_1", .Item("OUTKA_HASU_SAGYO_KB_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU_SAGYO_KB_2", .Item("OUTKA_HASU_SAGYO_KB_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU_SAGYO_KB_3", .Item("OUTKA_HASU_SAGYO_KB_3").ToString(), DBDataType.NVARCHAR))

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AVAL_YN", .Item("AVAL_YN").ToString(), DBDataType.CHAR))        'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能

        End With

    End Sub

#End Region

#End Region

#Region "危険品情報確認処理"
    ''' <summary>
    '''危険品情報確認処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ConfirmKikenGoods(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_UPDATE_CONF_KIKEN_GOODS)   'SQL構築(Update用)
        Call Me.SetParamConfirmKikenGoods()                    '更新用パラメータ設定
        Call Me.SetParamCommonSystemUpd()                        'システム共通更新項目

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "ConfirmKikenGoods", cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetUpdateResult(cmd)

        '処理件数の設定
        If reader < 1 = True Then
            MyBase.SetMessage("E011")
            MyBase.SetMessageStore("00" _
                                   , "E011" _
                                   , _
                                   , Me._Row.Item("RECORD_NO").ToString() _
                                   , "商品コード" _
                                   , Me._Row.Item("GOODS_CD_CUST").ToString())
        Else
            MyBase.SetMessage(Nothing)
        End If

        Return ds
    End Function
    ''' <summary>
    ''' 危険品情報確認処理用SQLパラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamConfirmKikenGoods()

        'パラメータ設定
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub
#End Region
#Region "容積一括更新処理"
    ''' <summary>
    ''' 容積一括更新処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function UpdateGoodsVolume(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM100OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMM100DAC.SQL_UPDATE_VOLUME)            'SQL構築(Update用)
        Call Me.SetParamGoodsVolume()                             '更新用パラメータ設定
        Call Me.SetParamCommonSystemUpd()                         'システム共通更新項目

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM100DAC", "UpdateMGoodsVolume", cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetUpdateResult(cmd)

        '処理件数の設定
        If reader < 1 = True Then
            MyBase.SetMessage("E011")
            MyBase.SetMessageStore("00" _
                                   , "E011" _
                                   , _
                                   , Me._Row.Item("RECORD_NO").ToString() _
                                   , "商品コード" _
                                   , Me._Row.Item("GOODS_CD_CUST").ToString())
        Else
            MyBase.SetMessage(Nothing)
        End If

        Return ds
    End Function
    ''' <summary>
    ''' 危険品情報確認処理用SQLパラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGoodsVolume()

        'パラメータ設定
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WIDTH", .Item("WIDTH").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HEIGHT", .Item("HEIGHT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPTH", .Item("DEPTH").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ACTUAL_VOLUME", .Item("ACTUAL_VOLUME").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OCCUPY_VOLUME", .Item("OCCUPY_VOLUME").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub
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

        'START YANAI 要望番号739
        'If sverFlg = True Then

        '    'トラン系スキーマ名設定(EDIサーバ接続)
        '    sql = sql.Replace("$LM_TRN_EDI$", Me.GetSchemaEDI(brCd))

        'End If
        'END YANAI 要望番号739

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
        sql.Append(LMM100DAC.SQL_GET_KBN)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )


        MyBase.Logger.WriteSQLLog("LMM100DAC", "SetConnectDataSet", cmd)

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

#End Region

End Class
