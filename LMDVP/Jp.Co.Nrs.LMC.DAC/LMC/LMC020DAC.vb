' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC020    : 出荷データ編集
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMC020DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC020DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    '更新前Select文
    Private Const SELECT_INSERT_DATA As String = "SYS_DEL_FLG = '0' AND UP_KBN = '0' "
    Private Const SELECT_UPDATE_DATA As String = "SYS_DEL_FLG = '0' AND UP_KBN = '1' "
    Private Const SELECT_DELETE_DATA_001 As String = "SYS_DEL_FLG = '1' "
    Private Const SELECT_DELETE_DATA_002 As String = "UP_KBN = '2' "

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "SQL"

#Region "SELECT"

#Region "OUTKA_L"

    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUT_L As String = " SELECT                                                                    " & vbNewLine _
                                            & " OUT_L.NRS_BR_CD                                     AS NRS_BR_CD           " & vbNewLine _
                                            & ",OUT_L.OUTKA_NO_L                                    AS OUTKA_NO_L          " & vbNewLine _
                                            & ",OUT_L.FURI_NO                                       AS FURI_NO             " & vbNewLine _
                                            & ",OUT_L.OUTKA_KB                                      AS OUTKA_KB            " & vbNewLine _
                                            & ",OUT_L.SYUBETU_KB                                    AS SYUBETU_KB          " & vbNewLine _
                                            & ",OUT_L.OUTKA_STATE_KB                                AS OUTKA_STATE_KB      " & vbNewLine _
                                            & ",OUT_L.OUTKAHOKOKU_YN                                AS OUTKAHOKOKU_YN      " & vbNewLine _
                                            & ",OUT_L.PICK_KB                                       AS PICK_KB             " & vbNewLine _
                                            & ",OUT_L.DENP_NO                                       AS DENP_NO             " & vbNewLine _
                                            & ",OUT_L.WH_CD                                         AS WH_CD               " & vbNewLine _
                                            & ",OUT_L.OUTKA_PLAN_DATE                               AS OUTKA_PLAN_DATE     " & vbNewLine _
                                            & ",OUT_L.OUTKO_DATE                                    AS OUTKO_DATE          " & vbNewLine _
                                            & ",OUT_L.ARR_PLAN_DATE                                 AS ARR_PLAN_DATE       " & vbNewLine _
                                            & ",OUT_L.ARR_PLAN_TIME                                 AS ARR_PLAN_TIME       " & vbNewLine _
                                            & ",OUT_L.HOKOKU_DATE                                   AS HOKOKU_DATE         " & vbNewLine _
                                            & ",OUT_L.TOUKI_HOKAN_YN                                AS TOUKI_HOKAN_YN      " & vbNewLine _
                                            & ",OUT_L.END_DATE                                      AS END_DATE            " & vbNewLine _
                                            & ",OUT_L.CUST_CD_L                                     AS CUST_CD_L           " & vbNewLine _
                                            & ",OUT_L.CUST_CD_M                                     AS CUST_CD_M           " & vbNewLine _
                                            & ",OUT_L.SHIP_CD_L                                     AS SHIP_CD_L           " & vbNewLine _
                                            & ",OUT_L.DEST_CD                                       AS DEST_CD             " & vbNewLine _
                                            & ",OUT_L.DEST_AD_3                                     AS DEST_AD_3           " & vbNewLine _
                                            & ",OUT_L.DEST_TEL                                      AS DEST_TEL            " & vbNewLine _
                                            & ",OUT_L.NHS_REMARK                                    AS NHS_REMARK          " & vbNewLine _
                                            & ",OUT_L.SP_NHS_KB                                     AS SP_NHS_KB           " & vbNewLine _
                                            & "--,DEST.COA_YN                                         AS COA_YN              " & vbNewLine _
                                            & ",OUT_L.COA_YN                                         AS COA_YN              " & vbNewLine _
                                            & ",OUT_L.CUST_ORD_NO                                   AS CUST_ORD_NO         " & vbNewLine _
                                            & ",OUT_L.BUYER_ORD_NO                                  AS BUYER_ORD_NO        " & vbNewLine _
                                            & ",OUT_L.REMARK                                        AS REMARK              " & vbNewLine _
                                            & ",OUT_L.OUTKA_PKG_NB                                  AS OUTKA_PKG_NB        " & vbNewLine _
                                            & ",OUT_L.DENP_YN                                       AS DENP_YN             " & vbNewLine _
                                            & ",OUT_L.PC_KB                                         AS PC_KB               " & vbNewLine _
                                            & ",OUT_L.NIYAKU_YN                                     AS NIYAKU_YN           " & vbNewLine _
                                            & ",OUT_L.ALL_PRINT_FLAG                                AS ALL_PRINT_FLAG      " & vbNewLine _
                                            & ",OUT_L.NIHUDA_FLAG                                   AS NIHUDA_FLAG         " & vbNewLine _
                                            & ",OUT_L.NHS_FLAG                                      AS NHS_FLAG            " & vbNewLine _
                                            & ",OUT_L.DENP_FLAG                                     AS DENP_FLAG           " & vbNewLine _
                                            & ",OUT_L.COA_FLAG                                      AS COA_FLAG            " & vbNewLine _
                                            & ",OUT_L.HOKOKU_FLAG                                   AS HOKOKU_FLAG         " & vbNewLine _
                                            & ",OUT_L.MATOME_PICK_FLAG                              AS MATOME_PICK_FLAG    " & vbNewLine _
                                            & ",OUT_L.ORDER_TYPE                                    AS ORDER_TYPE          " & vbNewLine _
                                            & ",OUT_L.DEST_KB                                       AS DEST_KB             " & vbNewLine _
                                            & ",OUT_L.DEST_NM                                       AS DEST_NM2            " & vbNewLine _
                                            & ",OUT_L.DEST_AD_1                                     AS DEST_AD_1           " & vbNewLine _
                                            & ",OUT_L.DEST_AD_2                                     AS DEST_AD_2           " & vbNewLine _
                                            & ",CUST.CUST_NM_L + CUST.CUST_NM_M                     AS CUST_NM             " & vbNewLine _
                                            & ",CUST.CUST_NM_L                                      AS CUST_NM_L           " & vbNewLine _
                                            & ",CUST.CUST_NM_M                                      AS CUST_NM_M           " & vbNewLine _
                                            & ",DEST2.DEST_NM                                       AS SHIP_NM             " & vbNewLine _
                                            & ",DEST.DEST_NM                                        AS DEST_NM             " & vbNewLine _
                                            & ",DEST.AD_1                                           AS AD_1                " & vbNewLine _
                                            & ",DEST.AD_2                                           AS AD_2                " & vbNewLine _
                                            & ",DEST.ZIP                                            AS ZIP                 " & vbNewLine _
                                            & ",Z1.KBN_NM1                                          AS COA_NM              " & vbNewLine _
                                            & ",SOKO.OUTKA_SASHIZU_PRT_YN                           AS OUTKA_SASHIZU_PRT_YN " & vbNewLine _
                                            & ",SOKO.OUTOKA_KANRYO_YN                               AS OUTOKA_KANRYO_YN    " & vbNewLine _
                                            & ",SOKO.OUTKA_KENPIN_YN                                AS OUTKA_KENPIN_YN     " & vbNewLine _
                                            & ",OUT_L.SYS_UPD_DATE                                  AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",OUT_L.SYS_UPD_TIME                                  AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",OUT_L.SYS_DEL_FLG                                   AS SYS_DEL_FLG         " & vbNewLine _
                                            & ",CUST.UNTIN_CALCULATION_KB                           AS UNTIN_CALCULATION_KB  " & vbNewLine _
                                            & ",SOKO.TOU_KANRI_YN                                   AS TOU_KANRI_YN        " & vbNewLine _
                                            & ",OUT_L.OUTKA_STATE_KB                                AS OUTKA_STATE_KB_OLD  " & vbNewLine _
                                            & ",OUT_L.SASZ_USER                                     AS SASZ_USER           " & vbNewLine _
                                            & ",''                                                  AS SASZ_USER_OLD       " & vbNewLine _
                                            & ",'1'                                                 AS UP_KBN              " & vbNewLine _
                                            & ",OUT_L.WH_TAB_STATUS                                 AS WH_TAB_STATUS       " & vbNewLine _
                                            & ",OUT_L.WH_TAB_YN                                     AS WH_TAB_YN           " & vbNewLine _
                                            & ",OUT_L.URGENT_YN                                     AS URGENT_YN           " & vbNewLine _
                                            & ",OUT_L.WH_SIJI_REMARK                                AS WH_SIJI_REMARK      " & vbNewLine _
                                            & ",OUT_L.WH_TAB_NO_SIJI_FLG                            AS WH_TAB_NO_SIJI_FLG  " & vbNewLine _
                                            & ",OUT_L.WH_TAB_HOKOKU_YN                              AS WH_TAB_HOKOKU_YN    " & vbNewLine _
                                            & ",OUT_L.WH_TAB_HOKOKU                                 AS WH_TAB_HOKOKU       " & vbNewLine

    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_OUT_L As String = "FROM                                                    " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_L  OUT_L                                " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_DEST    DEST                        " & vbNewLine _
                                         & "ON     OUT_L.NRS_BR_CD = DEST.NRS_BR_CD                   " & vbNewLine _
                                         & "AND    OUT_L.CUST_CD_L = DEST.CUST_CD_L                   " & vbNewLine _
                                         & "AND    OUT_L.DEST_CD = DEST.DEST_CD                       " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_DEST    DEST2                       " & vbNewLine _
                                         & "ON     OUT_L.NRS_BR_CD = DEST2.NRS_BR_CD                  " & vbNewLine _
                                         & "AND    OUT_L.CUST_CD_L = DEST2.CUST_CD_L                  " & vbNewLine _
                                         & "AND    OUT_L.SHIP_CD_L = DEST2.DEST_CD                    " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_CUST    CUST                        " & vbNewLine _
                                         & "ON     OUT_L.NRS_BR_CD = CUST.NRS_BR_CD                   " & vbNewLine _
                                         & "AND    OUT_L.CUST_CD_L = CUST.CUST_CD_L                   " & vbNewLine _
                                         & "AND    OUT_L.CUST_CD_M = CUST.CUST_CD_M                   " & vbNewLine _
                                         & "AND    CUST.CUST_CD_S = '00'                              " & vbNewLine _
                                         & "AND    CUST.CUST_CD_SS = '00'                             " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
                                         & "ON     OUT_L.COA_YN = Z1.KBN_CD                           " & vbNewLine _
                                         & "AND    Z1.KBN_GROUP_CD = 'B005'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_SOKO    SOKO                        " & vbNewLine _
                                         & "ON     OUT_L.NRS_BR_CD = SOKO.NRS_BR_CD                   " & vbNewLine _
                                         & "AND    OUT_L.WH_CD = SOKO.WH_CD                           " & vbNewLine


    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_OUT_L As String = "WHERE                                                  " & vbNewLine _
                                         & "    OUT_L.NRS_BR_CD              = @NRS_BR_CD             " & vbNewLine _
                                         & "AND OUT_L.OUTKA_NO_L             = @OUTKA_NO_L            " & vbNewLine _
                                         & "AND OUT_L.SYS_DEL_FLG            = '0'                    " & vbNewLine

#End Region

#Region "OUTKA_M"
    'START YANAI メモ②No.20
    '''' <summary>
    '''' SELECT
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_OUT_M As String = " SELECT                                                                    " & vbNewLine _
    '                                        & " OUT_M.NRS_BR_CD                                     AS NRS_BR_CD           " & vbNewLine _
    '                                        & ",OUT_M.OUTKA_NO_L                                    AS OUTKA_NO_L          " & vbNewLine _
    '                                        & ",OUT_M.OUTKA_NO_M                                    AS OUTKA_NO_M          " & vbNewLine _
    '                                        & ",OUT_M.COA_YN                                        AS COA_YN              " & vbNewLine _
    '                                        & ",OUT_M.CUST_ORD_NO_DTL                               AS CUST_ORD_NO_DTL     " & vbNewLine _
    '                                        & ",OUT_M.BUYER_ORD_NO_DTL                              AS BUYER_ORD_NO_DTL    " & vbNewLine _
    '                                        & ",OUT_M.GOODS_CD_NRS                                  AS GOODS_CD_NRS        " & vbNewLine _
    '                                        & ",OUT_M.RSV_NO                                        AS RSV_NO              " & vbNewLine _
    '                                        & ",OUT_M.LOT_NO                                        AS LOT_NO              " & vbNewLine _
    '                                        & ",OUT_M.SERIAL_NO                                     AS SERIAL_NO           " & vbNewLine _
    '                                        & ",OUT_M.ALCTD_KB                                      AS ALCTD_KB            " & vbNewLine _
    '                                        & ",OUT_M.ALCTD_KB                                      AS ALCTD_KB_HOZON      " & vbNewLine _
    '                                        & ",OUT_M.OUTKA_PKG_NB                                  AS OUTKA_PKG_NB        " & vbNewLine _
    '                                        & ",OUT_M.OUTKA_HASU                                    AS OUTKA_HASU          " & vbNewLine _
    '                                        & ",OUT_M.OUTKA_TTL_NB                                  AS OUTKA_TTL_NB        " & vbNewLine _
    '                                        & ",OUT_M.OUTKA_TTL_QT                                  AS OUTKA_TTL_QT        " & vbNewLine _
    '                                        & ",OUT_M.ALCTD_NB                                      AS ALCTD_NB            " & vbNewLine _
    '                                        & ",OUT_M.ALCTD_QT                                      AS ALCTD_QT            " & vbNewLine _
    '                                        & ",OUT_M.BACKLOG_NB                                    AS BACKLOG_NB          " & vbNewLine _
    '                                        & ",OUT_M.BACKLOG_QT                                    AS BACKLOG_QT          " & vbNewLine _
    '                                        & ",OUT_M.UNSO_ONDO_KB                                  AS UNSO_ONDO_KB        " & vbNewLine _
    '                                        & ",OUT_M.IRIME                                         AS IRIME               " & vbNewLine _
    '                                        & ",GOODS.STD_IRIME_UT                                  AS IRIME_UT            " & vbNewLine _
    '                                        & ",OUT_M.OUTKA_M_PKG_NB                                AS OUTKA_M_PKG_NB      " & vbNewLine _
    '                                        & ",OUT_M.REMARK                                        AS REMARK              " & vbNewLine _
    '                                        & ",OUT_M.SIZE_KB                                       AS SIZE_KB             " & vbNewLine _
    '                                        & ",OUT_M.PRINT_SORT                                    AS PRINT_SORT          " & vbNewLine _
    '                                        & ",OUT_M.OUTKA_TTL_NB                                  AS SUM_OUTKA_TTL_NB    " & vbNewLine _
    '                                        & ",GOODS.GOODS_CD_CUST                                 AS GOODS_CD_CUST       " & vbNewLine _
    '                                        & ",GOODS.GOODS_NM_1                                    AS GOODS_NM            " & vbNewLine _
    '                                        & ",CASE WHEN 0 = (SELECT COUNT(OUT_S.NRS_BR_CD)                               " & vbNewLine _
    '                                        & "                FROM $LM_TRN$..C_OUTKA_S OUT_S                              " & vbNewLine _
    '                                        & "                WHERE OUT_S.NRS_BR_CD = OUT_M.NRS_BR_CD                     " & vbNewLine _
    '                                        & "                  AND OUT_S.OUTKA_NO_L = OUT_M.OUTKA_NO_L                   " & vbNewLine _
    '                                        & "                  AND OUT_S.OUTKA_NO_M = OUT_M.OUTKA_NO_M                   " & vbNewLine _
    '                                        & "                  AND OUT_S.SYS_DEL_FLG = '0')                              " & vbNewLine _
    '                                        & "      THEN '未'                                                             " & vbNewLine _
    '                                        & "      ELSE '済'                                                             " & vbNewLine _
    '                                        & " END                                                 AS      HIKIATE        " & vbNewLine _
    '                                        & ",Z1.KBN_NM1                                          AS ALCTD_KB_NM         " & vbNewLine _
    '                                        & ",Z2.KBN_NM1                                          AS NB_UT_NM            " & vbNewLine _
    '                                        & ",Z3.KBN_NM1                                          AS QT_UT_NM            " & vbNewLine _
    '                                        & ",GOODS.PKG_NB                                        AS PKG_NB              " & vbNewLine _
    '                                        & ",GOODS.SEARCH_KEY_1                                  AS SEARCH_KEY_1        " & vbNewLine _
    '                                        & ",GOODS.UNSO_ONDO_KB                                  AS GOODS_UNSO_ONDO_KB  " & vbNewLine _
    '                                        & ",GOODS.PKG_UT                                        AS PKG_UT              " & vbNewLine _
    '                                        & ",GOODS.STD_IRIME_NB                                  AS STD_IRIME_NB        " & vbNewLine _
    '                                        & ",GOODS.STD_WT_KGS                                    AS STD_WT_KGS          " & vbNewLine _
    '                                        & ",GOODS.TARE_YN                                       AS TARE_YN             " & vbNewLine _
    '                                        & ",GOODS.OUTKA_ATT                                     AS OUTKA_ATT           " & vbNewLine _
    '                                        & ",OUT_M.GOODS_CD_NRS_FROM                             AS GOODS_CD_NRS_FROM   " & vbNewLine _
    '                                        & ",GOODS.CUST_CD_S                                     AS CUST_CD_S           " & vbNewLine _
    '                                        & ",GOODS.CUST_CD_SS                                    AS CUST_CD_SS          " & vbNewLine _
    '                                        & ",GOODS.NB_UT                                         AS NB_UT               " & vbNewLine _
    '                                        & ",OUT_M.SYS_UPD_DATE                                  AS SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",OUT_M.SYS_UPD_TIME                                  AS SYS_UPD_TIME        " & vbNewLine _
    '                                        & ",OUT_M.SYS_DEL_FLG                                   AS SYS_DEL_FLG         " & vbNewLine _
    '                                        & ",'1'                                                 AS UP_KBN              " & vbNewLine
    'START YANAI 要望番号499
    '''' <summary>
    '''' SELECT
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_OUT_M As String = " SELECT                                                                    " & vbNewLine _
    '                                            & " OUT_M.NRS_BR_CD                                     AS NRS_BR_CD           " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_NO_L                                    AS OUTKA_NO_L          " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_NO_M                                    AS OUTKA_NO_M          " & vbNewLine _
    '                                            & ",OUT_M.COA_YN                                        AS COA_YN              " & vbNewLine _
    '                                            & ",OUT_M.CUST_ORD_NO_DTL                               AS CUST_ORD_NO_DTL     " & vbNewLine _
    '                                            & ",OUT_M.BUYER_ORD_NO_DTL                              AS BUYER_ORD_NO_DTL    " & vbNewLine _
    '                                            & ",OUT_M.GOODS_CD_NRS                                  AS GOODS_CD_NRS        " & vbNewLine _
    '                                            & ",OUT_M.RSV_NO                                        AS RSV_NO              " & vbNewLine _
    '                                            & ",OUT_M.LOT_NO                                        AS LOT_NO              " & vbNewLine _
    '                                            & ",OUT_M.SERIAL_NO                                     AS SERIAL_NO           " & vbNewLine _
    '                                            & ",OUT_M.ALCTD_KB                                      AS ALCTD_KB            " & vbNewLine _
    '                                            & ",OUT_M.ALCTD_KB                                      AS ALCTD_KB_HOZON      " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_PKG_NB                                  AS OUTKA_PKG_NB        " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_HASU                                    AS OUTKA_HASU          " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_TTL_NB                                  AS OUTKA_TTL_NB        " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_TTL_QT                                  AS OUTKA_TTL_QT        " & vbNewLine _
    '                                            & ",OUT_M.ALCTD_NB                                      AS ALCTD_NB            " & vbNewLine _
    '                                            & ",OUT_M.ALCTD_QT                                      AS ALCTD_QT            " & vbNewLine _
    '                                            & ",OUT_M.BACKLOG_NB                                    AS BACKLOG_NB          " & vbNewLine _
    '                                            & ",OUT_M.BACKLOG_QT                                    AS BACKLOG_QT          " & vbNewLine _
    '                                            & ",OUT_M.UNSO_ONDO_KB                                  AS UNSO_ONDO_KB        " & vbNewLine _
    '                                            & ",OUT_M.IRIME                                         AS IRIME               " & vbNewLine _
    '                                            & ",GOODS.STD_IRIME_UT                                  AS IRIME_UT            " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_M_PKG_NB                                AS OUTKA_M_PKG_NB      " & vbNewLine _
    '                                            & ",OUT_M.REMARK                                        AS REMARK              " & vbNewLine _
    '                                            & ",OUT_M.SIZE_KB                                       AS SIZE_KB             " & vbNewLine _
    '                                            & ",OUT_M.PRINT_SORT                                    AS PRINT_SORT          " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_TTL_NB                                  AS SUM_OUTKA_TTL_NB    " & vbNewLine _
    '                                            & ",GOODS.GOODS_CD_CUST                                 AS GOODS_CD_CUST       " & vbNewLine _
    '                                            & ",GOODS.GOODS_NM_1                                    AS GOODS_NM            " & vbNewLine _
    '                                            & ",CASE WHEN 0 = (SELECT COUNT(OUT_S.NRS_BR_CD)                               " & vbNewLine _
    '                                            & "                FROM $LM_TRN$..C_OUTKA_S OUT_S                              " & vbNewLine _
    '                                            & "                WHERE OUT_S.NRS_BR_CD = OUT_M.NRS_BR_CD                     " & vbNewLine _
    '                                            & "                  AND OUT_S.OUTKA_NO_L = OUT_M.OUTKA_NO_L                   " & vbNewLine _
    '                                            & "                  AND OUT_S.OUTKA_NO_M = OUT_M.OUTKA_NO_M                   " & vbNewLine _
    '                                            & "                  AND OUT_S.SYS_DEL_FLG = '0')                              " & vbNewLine _
    '                                            & "      THEN '未'                                                             " & vbNewLine _
    '                                            & "      ELSE '済'                                                             " & vbNewLine _
    '                                            & " END                                                 AS      HIKIATE        " & vbNewLine _
    '                                            & ",Z1.KBN_NM1                                          AS ALCTD_KB_NM         " & vbNewLine _
    '                                            & ",Z2.KBN_NM1                                          AS NB_UT_NM            " & vbNewLine _
    '                                            & ",Z3.KBN_NM1                                          AS QT_UT_NM            " & vbNewLine _
    '                                            & ",GOODS.PKG_NB                                        AS PKG_NB              " & vbNewLine _
    '                                            & ",GOODS.SEARCH_KEY_1                                  AS SEARCH_KEY_1        " & vbNewLine _
    '                                            & ",GOODS.UNSO_ONDO_KB                                  AS GOODS_UNSO_ONDO_KB  " & vbNewLine _
    '                                            & ",GOODS.PKG_UT                                        AS PKG_UT              " & vbNewLine _
    '                                            & ",GOODS.STD_IRIME_NB                                  AS STD_IRIME_NB        " & vbNewLine _
    '                                            & ",GOODS.STD_WT_KGS                                    AS STD_WT_KGS          " & vbNewLine _
    '                                            & ",GOODS.TARE_YN                                       AS TARE_YN             " & vbNewLine _
    '                                            & ",GOODS.OUTKA_ATT                                     AS OUTKA_ATT           " & vbNewLine _
    '                                            & ",OUT_M.GOODS_CD_NRS_FROM                             AS GOODS_CD_NRS_FROM   " & vbNewLine _
    '                                            & ",GOODS.CUST_CD_S                                     AS CUST_CD_S           " & vbNewLine _
    '                                            & ",GOODS.CUST_CD_SS                                    AS CUST_CD_SS          " & vbNewLine _
    '                                            & ",GOODS.NB_UT                                         AS NB_UT               " & vbNewLine _
    '                                            & ",CASE WHEN ISNULL(OUT_EDIM.EDI_CTL_NO,'')  = ''      THEN '0'               " & vbNewLine _
    '                                            & "                                                     ELSE '1'               " & vbNewLine _
    '                                            & " END                                                 AS EDI_FLG             " & vbNewLine _
    '                                            & ",OUT_M.SYS_UPD_DATE                                  AS SYS_UPD_DATE        " & vbNewLine _
    '                                            & ",OUT_M.SYS_UPD_TIME                                  AS SYS_UPD_TIME        " & vbNewLine _
    '                                            & ",OUT_M.SYS_DEL_FLG                                   AS SYS_DEL_FLG         " & vbNewLine _
    '                                            & ",'1'                                                 AS UP_KBN              " & vbNewLine
    'START YANAI 要望番号573
    '''' <summary>
    '''' SELECT
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_OUT_M As String = " SELECT                                                                    " & vbNewLine _
    '                                            & " OUT_M.NRS_BR_CD                                     AS NRS_BR_CD           " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_NO_L                                    AS OUTKA_NO_L          " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_NO_M                                    AS OUTKA_NO_M          " & vbNewLine _
    '                                            & ",OUT_M.COA_YN                                        AS COA_YN              " & vbNewLine _
    '                                            & ",OUT_M.CUST_ORD_NO_DTL                               AS CUST_ORD_NO_DTL     " & vbNewLine _
    '                                            & ",OUT_M.BUYER_ORD_NO_DTL                              AS BUYER_ORD_NO_DTL    " & vbNewLine _
    '                                            & ",OUT_M.GOODS_CD_NRS                                  AS GOODS_CD_NRS        " & vbNewLine _
    '                                            & ",OUT_M.RSV_NO                                        AS RSV_NO              " & vbNewLine _
    '                                            & ",OUT_M.LOT_NO                                        AS LOT_NO              " & vbNewLine _
    '                                            & ",OUT_M.SERIAL_NO                                     AS SERIAL_NO           " & vbNewLine _
    '                                            & ",OUT_M.ALCTD_KB                                      AS ALCTD_KB            " & vbNewLine _
    '                                            & ",OUT_M.ALCTD_KB                                      AS ALCTD_KB_HOZON      " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_PKG_NB                                  AS OUTKA_PKG_NB        " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_HASU                                    AS OUTKA_HASU          " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_TTL_NB                                  AS OUTKA_TTL_NB        " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_TTL_QT                                  AS OUTKA_TTL_QT        " & vbNewLine _
    '                                            & ",OUT_M.ALCTD_NB                                      AS ALCTD_NB            " & vbNewLine _
    '                                            & ",OUT_M.ALCTD_QT                                      AS ALCTD_QT            " & vbNewLine _
    '                                            & ",OUT_M.BACKLOG_NB                                    AS BACKLOG_NB          " & vbNewLine _
    '                                            & ",OUT_M.BACKLOG_QT                                    AS BACKLOG_QT          " & vbNewLine _
    '                                            & ",OUT_M.UNSO_ONDO_KB                                  AS UNSO_ONDO_KB        " & vbNewLine _
    '                                            & ",OUT_M.IRIME                                         AS IRIME               " & vbNewLine _
    '                                            & ",GOODS.STD_IRIME_UT                                  AS IRIME_UT            " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_M_PKG_NB                                AS OUTKA_M_PKG_NB      " & vbNewLine _
    '                                            & ",OUT_M.REMARK                                        AS REMARK              " & vbNewLine _
    '                                            & ",OUT_M.SIZE_KB                                       AS SIZE_KB             " & vbNewLine _
    '                                            & ",OUT_M.PRINT_SORT                                    AS PRINT_SORT          " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_TTL_NB                                  AS SUM_OUTKA_TTL_NB    " & vbNewLine _
    '                                            & ",GOODS.GOODS_CD_CUST                                 AS GOODS_CD_CUST       " & vbNewLine _
    '                                            & ",GOODS.GOODS_NM_1                                    AS GOODS_NM            " & vbNewLine _
    '                                            & ",CASE WHEN 0 = (SELECT COUNT(OUT_S.NRS_BR_CD)                               " & vbNewLine _
    '                                            & "                FROM $LM_TRN$..C_OUTKA_S OUT_S                              " & vbNewLine _
    '                                            & "                WHERE OUT_S.NRS_BR_CD = OUT_M.NRS_BR_CD                     " & vbNewLine _
    '                                            & "                  AND OUT_S.OUTKA_NO_L = OUT_M.OUTKA_NO_L                   " & vbNewLine _
    '                                            & "                  AND OUT_S.OUTKA_NO_M = OUT_M.OUTKA_NO_M                   " & vbNewLine _
    '                                            & "                  AND OUT_S.SYS_DEL_FLG = '0')                              " & vbNewLine _
    '                                            & "      THEN '未'                                                             " & vbNewLine _
    '                                            & "      ELSE '済'                                                             " & vbNewLine _
    '                                            & " END                                                 AS      HIKIATE        " & vbNewLine _
    '                                            & ",Z1.KBN_NM1                                          AS ALCTD_KB_NM         " & vbNewLine _
    '                                            & ",Z2.KBN_NM1                                          AS NB_UT_NM            " & vbNewLine _
    '                                            & ",Z3.KBN_NM1                                          AS QT_UT_NM            " & vbNewLine _
    '                                            & ",GOODS.PKG_NB                                        AS PKG_NB              " & vbNewLine _
    '                                            & ",GOODS.SEARCH_KEY_1                                  AS SEARCH_KEY_1        " & vbNewLine _
    '                                            & ",GOODS.UNSO_ONDO_KB                                  AS GOODS_UNSO_ONDO_KB  " & vbNewLine _
    '                                            & ",GOODS.PKG_UT                                        AS PKG_UT              " & vbNewLine _
    '                                            & ",GOODS.STD_IRIME_NB                                  AS STD_IRIME_NB        " & vbNewLine _
    '                                            & ",GOODS.STD_WT_KGS                                    AS STD_WT_KGS          " & vbNewLine _
    '                                            & ",GOODS.TARE_YN                                       AS TARE_YN             " & vbNewLine _
    '                                            & ",GOODS.OUTKA_ATT                                     AS OUTKA_ATT           " & vbNewLine _
    '                                            & ",OUT_M.GOODS_CD_NRS_FROM                             AS GOODS_CD_NRS_FROM   " & vbNewLine _
    '                                            & ",GOODS.CUST_CD_S                                     AS CUST_CD_S           " & vbNewLine _
    '                                            & ",GOODS.CUST_CD_SS                                    AS CUST_CD_SS          " & vbNewLine _
    '                                            & ",GOODS.NB_UT                                         AS NB_UT               " & vbNewLine _
    '                                            & ",CASE WHEN ISNULL(OUT_EDIM.EDI_CTL_NO,'')  = ''      THEN '0'               " & vbNewLine _
    '                                            & "                                                     ELSE '1'               " & vbNewLine _
    '                                            & " END                                                 AS EDI_FLG             " & vbNewLine _
    '                                            & ",GOODS.CUST_CD_L                                     AS CUST_CD_L_GOODS     " & vbNewLine _
    '                                            & ",GOODS.CUST_CD_M                                     AS CUST_CD_M_GOODS     " & vbNewLine _
    '                                            & ",OUT_M.SYS_UPD_DATE                                  AS SYS_UPD_DATE        " & vbNewLine _
    '                                            & ",OUT_M.SYS_UPD_TIME                                  AS SYS_UPD_TIME        " & vbNewLine _
    '                                            & ",OUT_M.SYS_DEL_FLG                                   AS SYS_DEL_FLG         " & vbNewLine _
    '                                            & ",'1'                                                 AS UP_KBN              " & vbNewLine
    'START YANAI 要望番号1299 運送(中)の在庫部課・扱い部課保持対応
    '''' <summary>
    '''' SELECT
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_OUT_M As String = " SELECT                                                                    " & vbNewLine _
    '                                            & " OUT_M.NRS_BR_CD                                     AS NRS_BR_CD           " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_NO_L                                    AS OUTKA_NO_L          " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_NO_M                                    AS OUTKA_NO_M          " & vbNewLine _
    '                                            & ",OUT_M.COA_YN                                        AS COA_YN              " & vbNewLine _
    '                                            & ",OUT_M.CUST_ORD_NO_DTL                               AS CUST_ORD_NO_DTL     " & vbNewLine _
    '                                            & ",OUT_M.BUYER_ORD_NO_DTL                              AS BUYER_ORD_NO_DTL    " & vbNewLine _
    '                                            & ",OUT_M.GOODS_CD_NRS                                  AS GOODS_CD_NRS        " & vbNewLine _
    '                                            & ",OUT_M.RSV_NO                                        AS RSV_NO              " & vbNewLine _
    '                                            & ",OUT_M.LOT_NO                                        AS LOT_NO              " & vbNewLine _
    '                                            & ",OUT_M.SERIAL_NO                                     AS SERIAL_NO           " & vbNewLine _
    '                                            & ",OUT_M.ALCTD_KB                                      AS ALCTD_KB            " & vbNewLine _
    '                                            & ",OUT_M.ALCTD_KB                                      AS ALCTD_KB_HOZON      " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_PKG_NB                                  AS OUTKA_PKG_NB        " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_HASU                                    AS OUTKA_HASU          " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_TTL_NB                                  AS OUTKA_TTL_NB        " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_TTL_QT                                  AS OUTKA_TTL_QT        " & vbNewLine _
    '                                            & ",OUT_M.ALCTD_NB                                      AS ALCTD_NB            " & vbNewLine _
    '                                            & ",OUT_M.ALCTD_QT                                      AS ALCTD_QT            " & vbNewLine _
    '                                            & ",OUT_M.BACKLOG_NB                                    AS BACKLOG_NB          " & vbNewLine _
    '                                            & ",OUT_M.BACKLOG_QT                                    AS BACKLOG_QT          " & vbNewLine _
    '                                            & ",OUT_M.UNSO_ONDO_KB                                  AS UNSO_ONDO_KB        " & vbNewLine _
    '                                            & ",OUT_M.IRIME                                         AS IRIME               " & vbNewLine _
    '                                            & ",GOODS.STD_IRIME_UT                                  AS IRIME_UT            " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_M_PKG_NB                                AS OUTKA_M_PKG_NB      " & vbNewLine _
    '                                            & ",OUT_M.REMARK                                        AS REMARK              " & vbNewLine _
    '                                            & ",OUT_M.SIZE_KB                                       AS SIZE_KB             " & vbNewLine _
    '                                            & ",OUT_M.PRINT_SORT                                    AS PRINT_SORT          " & vbNewLine _
    '                                            & ",OUT_M.OUTKA_TTL_NB                                  AS SUM_OUTKA_TTL_NB    " & vbNewLine _
    '                                            & ",GOODS.GOODS_CD_CUST                                 AS GOODS_CD_CUST       " & vbNewLine _
    '                                            & ",GOODS.GOODS_NM_1                                    AS GOODS_NM            " & vbNewLine _
    '                                            & ",CASE WHEN 0 = (SELECT COUNT(OUT_S.NRS_BR_CD)                               " & vbNewLine _
    '                                            & "                FROM $LM_TRN$..C_OUTKA_S OUT_S                              " & vbNewLine _
    '                                            & "                WHERE OUT_S.NRS_BR_CD = OUT_M.NRS_BR_CD                     " & vbNewLine _
    '                                            & "                  AND OUT_S.OUTKA_NO_L = OUT_M.OUTKA_NO_L                   " & vbNewLine _
    '                                            & "                  AND OUT_S.OUTKA_NO_M = OUT_M.OUTKA_NO_M                   " & vbNewLine _
    '                                            & "                  AND OUT_S.SYS_DEL_FLG = '0')                              " & vbNewLine _
    '                                            & "      THEN '未'                                                             " & vbNewLine _
    '                                            & "      ELSE '済'                                                             " & vbNewLine _
    '                                            & " END                                                 AS      HIKIATE        " & vbNewLine _
    '                                            & ",Z1.KBN_NM1                                          AS ALCTD_KB_NM         " & vbNewLine _
    '                                            & ",Z2.KBN_NM1                                          AS NB_UT_NM            " & vbNewLine _
    '                                            & ",Z3.KBN_NM1                                          AS QT_UT_NM            " & vbNewLine _
    '                                            & ",GOODS.PKG_NB                                        AS PKG_NB              " & vbNewLine _
    '                                            & ",GOODS.SEARCH_KEY_1                                  AS SEARCH_KEY_1        " & vbNewLine _
    '                                            & ",GOODS.UNSO_ONDO_KB                                  AS GOODS_UNSO_ONDO_KB  " & vbNewLine _
    '                                            & ",GOODS.PKG_UT                                        AS PKG_UT              " & vbNewLine _
    '                                            & ",GOODS.STD_IRIME_NB                                  AS STD_IRIME_NB        " & vbNewLine _
    '                                            & ",GOODS.STD_WT_KGS                                    AS STD_WT_KGS          " & vbNewLine _
    '                                            & ",GOODS.TARE_YN                                       AS TARE_YN             " & vbNewLine _
    '                                            & ",GOODS.OUTKA_ATT                                     AS OUTKA_ATT           " & vbNewLine _
    '                                            & ",OUT_M.GOODS_CD_NRS_FROM                             AS GOODS_CD_NRS_FROM   " & vbNewLine _
    '                                            & ",GOODS.CUST_CD_S                                     AS CUST_CD_S           " & vbNewLine _
    '                                            & ",GOODS.CUST_CD_SS                                    AS CUST_CD_SS          " & vbNewLine _
    '                                            & ",GOODS.NB_UT                                         AS NB_UT               " & vbNewLine _
    '                                            & ",CASE WHEN ISNULL(OUT_EDIM.EDI_CTL_NO,'')  = ''      THEN '0'               " & vbNewLine _
    '                                            & "                                                     ELSE '1'               " & vbNewLine _
    '                                            & " END                                                 AS EDI_FLG             " & vbNewLine _
    '                                            & ",GOODS.CUST_CD_L                                     AS CUST_CD_L_GOODS     " & vbNewLine _
    '                                            & ",GOODS.CUST_CD_M                                     AS CUST_CD_M_GOODS     " & vbNewLine _
    '                                            & ",OUT_M.SYS_UPD_DATE                                  AS SYS_UPD_DATE        " & vbNewLine _
    '                                            & ",OUT_M.SYS_UPD_TIME                                  AS SYS_UPD_TIME        " & vbNewLine _
    '                                            & ",OUT_M.SYS_DEL_FLG                                   AS SYS_DEL_FLG         " & vbNewLine _
    '                                            & ",'1'                                                 AS UP_KBN              " & vbNewLine _
    '                                            & ",ISNULL(OUT_EDIM.JISSEKI_FLAG,'')                    AS JISSEKI_FLAG        " & vbNewLine _
    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUT_M As String = " SELECT                                                                    " & vbNewLine _
                                                & " OUT_M.NRS_BR_CD                                     AS NRS_BR_CD           " & vbNewLine _
                                                & ",OUT_M.OUTKA_NO_L                                    AS OUTKA_NO_L          " & vbNewLine _
                                                & ",OUT_M.OUTKA_NO_M                                    AS OUTKA_NO_M          " & vbNewLine _
                                                & ",OUT_M.COA_YN                                        AS COA_YN              " & vbNewLine _
                                                & ",OUT_M.CUST_ORD_NO_DTL                               AS CUST_ORD_NO_DTL     " & vbNewLine _
                                                & ",OUT_M.BUYER_ORD_NO_DTL                              AS BUYER_ORD_NO_DTL    " & vbNewLine _
                                                & ",OUT_M.GOODS_CD_NRS                                  AS GOODS_CD_NRS        " & vbNewLine _
                                                & ",OUT_M.RSV_NO                                        AS RSV_NO              " & vbNewLine _
                                                & ",OUT_M.LOT_NO                                        AS LOT_NO              " & vbNewLine _
                                                & ",OUT_M.SERIAL_NO                                     AS SERIAL_NO           " & vbNewLine _
                                                & ",OUT_M.ALCTD_KB                                      AS ALCTD_KB            " & vbNewLine _
                                                & ",OUT_M.ALCTD_KB                                      AS ALCTD_KB_HOZON      " & vbNewLine _
                                                & ",OUT_M.OUTKA_PKG_NB                                  AS OUTKA_PKG_NB        " & vbNewLine _
                                                & ",OUT_M.OUTKA_HASU                                    AS OUTKA_HASU          " & vbNewLine _
                                                & ",OUT_M.OUTKA_TTL_NB                                  AS OUTKA_TTL_NB        " & vbNewLine _
                                                & ",OUT_M.OUTKA_TTL_QT                                  AS OUTKA_TTL_QT        " & vbNewLine _
                                                & ",OUT_M.ALCTD_NB                                      AS ALCTD_NB            " & vbNewLine _
                                                & ",OUT_M.ALCTD_QT                                      AS ALCTD_QT            " & vbNewLine _
                                                & ",OUT_M.BACKLOG_NB                                    AS BACKLOG_NB          " & vbNewLine _
                                                & ",OUT_M.BACKLOG_QT                                    AS BACKLOG_QT          " & vbNewLine _
                                                & ",OUT_M.UNSO_ONDO_KB                                  AS UNSO_ONDO_KB        " & vbNewLine _
                                                & ",OUT_M.IRIME                                         AS IRIME               " & vbNewLine _
                                                & ",GOODS.STD_IRIME_UT                                  AS IRIME_UT            " & vbNewLine _
                                                & ",OUT_M.OUTKA_M_PKG_NB                                AS OUTKA_M_PKG_NB      " & vbNewLine _
                                                & ",OUT_M.REMARK                                        AS REMARK              " & vbNewLine _
                                                & ",OUT_M.SIZE_KB                                       AS SIZE_KB             " & vbNewLine _
                                                & ",OUT_M.PRINT_SORT                                    AS PRINT_SORT          " & vbNewLine _
                                                & ",OUT_M.OUTKA_TTL_NB                                  AS SUM_OUTKA_TTL_NB    " & vbNewLine _
                                                & ",GOODS.GOODS_CD_CUST                                 AS GOODS_CD_CUST       " & vbNewLine _
                                                & ",GOODS.GOODS_NM_1                                    AS GOODS_NM            " & vbNewLine _
                                                & ",CASE WHEN 0 = (SELECT COUNT(OUT_S.NRS_BR_CD)                               " & vbNewLine _
                                                & "                FROM $LM_TRN$..C_OUTKA_S OUT_S                              " & vbNewLine _
                                                & "                WHERE OUT_S.NRS_BR_CD = OUT_M.NRS_BR_CD                     " & vbNewLine _
                                                & "                  AND OUT_S.OUTKA_NO_L = OUT_M.OUTKA_NO_L                   " & vbNewLine _
                                                & "                  AND OUT_S.OUTKA_NO_M = OUT_M.OUTKA_NO_M                   " & vbNewLine _
                                                & "                  AND OUT_S.SYS_DEL_FLG = '0')                              " & vbNewLine _
                                                & "      THEN '未'                                                             " & vbNewLine _
                                                & "      ELSE '済'                                                             " & vbNewLine _
                                                & " END                                                 AS      HIKIATE        " & vbNewLine _
                                                & ",Z1.KBN_NM1                                          AS ALCTD_KB_NM         " & vbNewLine _
                                                & ",Z2.KBN_NM1                                          AS NB_UT_NM            " & vbNewLine _
                                                & ",Z3.KBN_NM1                                          AS QT_UT_NM            " & vbNewLine _
                                                & ",GOODS.PKG_NB                                        AS PKG_NB              " & vbNewLine _
                                                & ",GOODS.SEARCH_KEY_1                                  AS SEARCH_KEY_1        " & vbNewLine _
                                                & ",GOODS.UNSO_ONDO_KB                                  AS GOODS_UNSO_ONDO_KB  " & vbNewLine _
                                                & ",GOODS.PKG_UT                                        AS PKG_UT              " & vbNewLine _
                                                & ",GOODS.STD_IRIME_NB                                  AS STD_IRIME_NB        " & vbNewLine _
                                                & ",GOODS.STD_WT_KGS                                    AS STD_WT_KGS          " & vbNewLine _
                                                & ",GOODS.TARE_YN                                       AS TARE_YN             " & vbNewLine _
                                                & ",GOODS.OUTKA_ATT                                     AS OUTKA_ATT           " & vbNewLine _
                                                & ",OUT_M.GOODS_CD_NRS_FROM                             AS GOODS_CD_NRS_FROM   " & vbNewLine _
                                                & ",GOODS.CUST_CD_S                                     AS CUST_CD_S           " & vbNewLine _
                                                & ",GOODS.CUST_CD_SS                                    AS CUST_CD_SS          " & vbNewLine _
                                                & ",GOODS.NB_UT                                         AS NB_UT               " & vbNewLine _
                                                & ",CASE WHEN ISNULL(OUT_EDIM.EDI_CTL_NO,'')  = ''      THEN '0'               " & vbNewLine _
                                                & "                                                     ELSE '1'               " & vbNewLine _
                                                & " END                                                 AS EDI_FLG             " & vbNewLine _
                                                & ",GOODS.CUST_CD_L                                     AS CUST_CD_L_GOODS     " & vbNewLine _
                                                & ",GOODS.CUST_CD_M                                     AS CUST_CD_M_GOODS     " & vbNewLine _
                                                & ",OUT_M.SYS_UPD_DATE                                  AS SYS_UPD_DATE        " & vbNewLine _
                                                & ",OUT_M.SYS_UPD_TIME                                  AS SYS_UPD_TIME        " & vbNewLine _
                                                & ",OUT_M.SYS_DEL_FLG                                   AS SYS_DEL_FLG         " & vbNewLine _
                                                & ",'1'                                                 AS UP_KBN              " & vbNewLine _
                                                & ",ISNULL(OUT_EDIM.JISSEKI_FLAG,'')                    AS JISSEKI_FLAG        " & vbNewLine _
                                                & ",UNSOM.ZBUKA_CD                                      AS ZBUKA_CD            " & vbNewLine _
                                                & ",UNSOM.ABUKA_CD                                      AS ABUKA_CD            " & vbNewLine _
                                                & "--要望番号1959 追加START                                                    " & vbNewLine _
                                                & ",OUT_EDIM.OUTKA_TTL_NB                               AS EDI_OUTKA_TTL_NB    " & vbNewLine _
                                                & ",OUT_EDIM.OUTKA_TTL_QT                               AS EDI_OUTKA_TTL_QT    " & vbNewLine _
                                                & "--要望番号1959 追加END                                                      " & vbNewLine _
                                                & ",GOODS.SHOBO_CD                                      AS SHOBO_CD            " & vbNewLine _
                                                & ",Z4.KBN_NM1 + ' ' + SBO.HINMEI                       AS SHOBO_NM            " & vbNewLine

    'END YANAI 要望番号1299 運送(中)の在庫部課・扱い部課保持対応
    'END YANAI 要望番号573
    'END YANAI 要望番号499
    'END YANAI メモ②No.20

    'START YANAI メモ②No.20
    '''' <summary>
    '''' FROM
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM_OUT_M As String = "FROM                                                    " & vbNewLine _
    '                                     & "$LM_TRN$..C_OUTKA_M  OUT_M                                " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_GOODS    GOODS                      " & vbNewLine _
    '                                     & "ON     OUT_M.NRS_BR_CD = GOODS.NRS_BR_CD                  " & vbNewLine _
    '                                     & "AND    OUT_M.GOODS_CD_NRS = GOODS.GOODS_CD_NRS            " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
    '                                     & "ON     OUT_M.ALCTD_KB = Z1.KBN_CD                         " & vbNewLine _
    '                                     & "AND    Z1.KBN_GROUP_CD = 'S041'                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..Z_KBN    Z2                           " & vbNewLine _
    '                                     & "ON     GOODS.NB_UT = Z2.KBN_CD                            " & vbNewLine _
    '                                     & "AND    Z2.KBN_GROUP_CD = 'K002'                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..Z_KBN    Z3                           " & vbNewLine _
    '                                     & "ON     GOODS.STD_IRIME_UT  = Z3.KBN_CD                    " & vbNewLine _
    '                                     & "AND    Z3.KBN_GROUP_CD = 'I001'                           " & vbNewLine
    'START YANAI 要望番号1299 運送(中)の在庫部課・扱い部課保持対応
    '''' <summary>
    '''' FROM
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM_OUT_M As String = "FROM                                                    " & vbNewLine _
    '                                     & "$LM_TRN$..C_OUTKA_M  OUT_M                                " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_GOODS    GOODS                      " & vbNewLine _
    '                                     & "ON     OUT_M.NRS_BR_CD = GOODS.NRS_BR_CD                  " & vbNewLine _
    '                                     & "AND    OUT_M.GOODS_CD_NRS = GOODS.GOODS_CD_NRS            " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
    '                                     & "ON     OUT_M.ALCTD_KB = Z1.KBN_CD                         " & vbNewLine _
    '                                     & "AND    Z1.KBN_GROUP_CD = 'S041'                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..Z_KBN    Z2                           " & vbNewLine _
    '                                     & "ON     GOODS.NB_UT = Z2.KBN_CD                            " & vbNewLine _
    '                                     & "AND    Z2.KBN_GROUP_CD = 'K002'                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..Z_KBN    Z3                           " & vbNewLine _
    '                                     & "ON     GOODS.STD_IRIME_UT  = Z3.KBN_CD                    " & vbNewLine _
    '                                     & "AND    Z3.KBN_GROUP_CD = 'I001'                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_TRN$..H_OUTKAEDI_M    OUT_EDIM              " & vbNewLine _
    '                                     & "ON     OUT_EDIM.NRS_BR_CD = OUT_M.NRS_BR_CD               " & vbNewLine _
    '                                     & "AND    OUT_EDIM.OUTKA_CTL_NO = OUT_M.OUTKA_NO_L           " & vbNewLine _
    '                                     & "AND    OUT_EDIM.OUTKA_CTL_NO_CHU = OUT_M.OUTKA_NO_M       " & vbNewLine _
    '                                     & "AND    OUT_EDIM.SYS_DEL_FLG = '0'                         " & vbNewLine _
    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_OUT_M As String = "FROM                                                    " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_M  OUT_M                                " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS    GOODS                      " & vbNewLine _
                                         & "ON     OUT_M.NRS_BR_CD = GOODS.NRS_BR_CD                  " & vbNewLine _
                                         & "AND    OUT_M.GOODS_CD_NRS = GOODS.GOODS_CD_NRS            " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
                                         & "ON     OUT_M.ALCTD_KB = Z1.KBN_CD                         " & vbNewLine _
                                         & "AND    Z1.KBN_GROUP_CD = 'S041'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z2                           " & vbNewLine _
                                         & "ON     GOODS.NB_UT = Z2.KBN_CD                            " & vbNewLine _
                                         & "AND    Z2.KBN_GROUP_CD = 'K002'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z3                           " & vbNewLine _
                                         & "ON     GOODS.STD_IRIME_UT  = Z3.KBN_CD                    " & vbNewLine _
                                         & "AND    Z3.KBN_GROUP_CD = 'I001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..H_OUTKAEDI_M    OUT_EDIM              " & vbNewLine _
                                         & "ON     OUT_EDIM.NRS_BR_CD = OUT_M.NRS_BR_CD               " & vbNewLine _
                                         & "AND    OUT_EDIM.OUTKA_CTL_NO = OUT_M.OUTKA_NO_L           " & vbNewLine _
                                         & "AND    OUT_EDIM.OUTKA_CTL_NO_CHU = OUT_M.OUTKA_NO_M       " & vbNewLine _
                                         & "AND    OUT_EDIM.SYS_DEL_FLG = '0'                         " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..F_UNSO_L    UNSOL                     " & vbNewLine _
                                         & "ON     UNSOL.NRS_BR_CD = OUT_M.NRS_BR_CD                  " & vbNewLine _
                                         & "AND    UNSOL.INOUTKA_NO_L = OUT_M.OUTKA_NO_L              " & vbNewLine _
                                         & "AND    UNSOL.MOTO_DATA_KB = '20'                          " & vbNewLine _
                                         & "AND    UNSOL.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..F_UNSO_M    UNSOM                     " & vbNewLine _
                                         & "ON     UNSOM.NRS_BR_CD = UNSOL.NRS_BR_CD                  " & vbNewLine _
                                         & "AND    UNSOM.UNSO_NO_L = UNSOL.UNSO_NO_L                  " & vbNewLine _
                                         & "AND    UNSOM.UNSO_NO_M = OUT_M.OUTKA_NO_M                 " & vbNewLine _
                                         & "AND    UNSOM.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_SHOBO SBO                           " & vbNewLine _
                                         & "ON     SBO.SHOBO_CD    = GOODS.SHOBO_CD                   " & vbNewLine _
                                         & "AND    SBO.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN Z4                              " & vbNewLine _
                                         & "ON     Z4.KBN_GROUP_CD = 'S004'                           " & vbNewLine _
                                         & "AND    Z4.KBN_CD       = SBO.RUI                          " & vbNewLine _
                                         & "AND    Z4.SYS_DEL_FLG  = '0'                              " & vbNewLine

    'END YANAI 要望番号1299 運送(中)の在庫部課・扱い部課保持対応
    'END YANAI メモ②No.20

    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_OUT_M As String = "WHERE                                                  " & vbNewLine _
                                         & "    OUT_M.NRS_BR_CD              = @NRS_BR_CD             " & vbNewLine _
                                         & "AND OUT_M.OUTKA_NO_L             = @OUTKA_NO_L            " & vbNewLine _
                                         & "AND OUT_M.SYS_DEL_FLG            = '0'                    " & vbNewLine

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_OUT_M As String = "ORDER BY                                            " & vbNewLine _
                                         & " OUT_M.OUTKA_NO_L                                         " & vbNewLine _
                                         & ",OUT_M.PRINT_SORT                                         " & vbNewLine _
                                         & ",OUT_M.OUTKA_NO_M                                         " & vbNewLine


#End Region

#Region "OUTKA_S"
    'START YANAI 20110913 小分け対応
    '''' <summary>
    '''' SELECT
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_OUT_S As String = " SELECT                                                                    " & vbNewLine _
    '                                        & " OUT_S.NRS_BR_CD                                     AS NRS_BR_CD           " & vbNewLine _
    '                                        & ",OUT_S.OUTKA_NO_L                                    AS OUTKA_NO_L          " & vbNewLine _
    '                                        & ",OUT_S.OUTKA_NO_M                                    AS OUTKA_NO_M          " & vbNewLine _
    '                                        & ",OUT_S.OUTKA_NO_S                                    AS OUTKA_NO_S          " & vbNewLine _
    '                                        & ",OUT_S.TOU_NO                                        AS TOU_NO              " & vbNewLine _
    '                                        & ",OUT_S.SITU_NO                                       AS SITU_NO             " & vbNewLine _
    '                                        & ",OUT_S.ZONE_CD                                       AS ZONE_CD             " & vbNewLine _
    '                                        & ",OUT_S.LOCA                                          AS LOCA                " & vbNewLine _
    '                                        & ",OUT_S.LOT_NO                                        AS LOT_NO              " & vbNewLine _
    '                                        & ",OUT_S.SERIAL_NO                                     AS SERIAL_NO           " & vbNewLine _
    '                                        & ",OUT_S.OUTKA_TTL_NB                                  AS OUTKA_TTL_NB        " & vbNewLine _
    '                                        & ",OUT_S.OUTKA_TTL_QT                                  AS OUTKA_TTL_QT        " & vbNewLine _
    '                                        & ",OUT_S.ZAI_REC_NO                                    AS ZAI_REC_NO          " & vbNewLine _
    '                                        & ",OUT_S.INKA_NO_L                                     AS INKA_NO_L           " & vbNewLine _
    '                                        & ",OUT_S.INKA_NO_M                                     AS INKA_NO_M           " & vbNewLine _
    '                                        & ",OUT_S.INKA_NO_S                                     AS INKA_NO_S           " & vbNewLine _
    '                                        & ",OUT_S.ZAI_UPD_FLAG                                  AS ZAI_UPD_FLAG        " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_CAN_NB                                  AS ALCTD_CAN_NB        " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_NB                                      AS ALCTD_NB            " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_CAN_QT                                  AS ALCTD_CAN_QT        " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_QT                                      AS ALCTD_QT            " & vbNewLine _
    '                                        & ",OUT_S.IRIME                                         AS IRIME               " & vbNewLine _
    '                                        & ",OUT_S.BETU_WT                                       AS BETU_WT             " & vbNewLine _
    '                                        & ",OUT_S.COA_FLAG                                      AS COA_FLAG            " & vbNewLine _
    '                                        & ",OUT_S.REMARK                                        AS REMARK              " & vbNewLine _
    '                                        & ",OUT_S.SMPL_FLAG                                     AS SMPL_FLAG           " & vbNewLine _
    '                                        & ",OUT_S.SYS_UPD_DATE                                  AS SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",OUT_S.SYS_UPD_TIME                                  AS SYS_UPD_TIME        " & vbNewLine _
    '                                        & ",OUT_S.SYS_DEL_FLG                                   AS SYS_DEL_FLG         " & vbNewLine _
    '                                        & ",'1'                                                 AS UP_KBN              " & vbNewLine _
    '                                        & ",OUT_S.REC_NO                                        AS REC_NO              " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_CAN_NB                                  AS ALCTD_CAN_NB_GAMEN  " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_NB                                      AS ALCTD_NB_GAMEN      " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_CAN_QT                                  AS ALCTD_CAN_QT_GAMEN  " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_QT                                      AS ALCTD_QT_GAMEN      " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_CAN_NB                                  AS ALCTD_CAN_NB_MATOME " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_NB                                      AS ALCTD_NB_MATOME     " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_CAN_QT                                  AS ALCTD_CAN_QT_MATOME " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_QT                                      AS ALCTD_QT_MATOME     " & vbNewLine
    'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
    '''' <summary>
    '''' SELECT
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_OUT_S As String = " SELECT                                                                    " & vbNewLine _
    '                                        & " OUT_S.NRS_BR_CD                                     AS NRS_BR_CD           " & vbNewLine _
    '                                        & ",OUT_S.OUTKA_NO_L                                    AS OUTKA_NO_L          " & vbNewLine _
    '                                        & ",OUT_S.OUTKA_NO_M                                    AS OUTKA_NO_M          " & vbNewLine _
    '                                        & ",OUT_S.OUTKA_NO_S                                    AS OUTKA_NO_S          " & vbNewLine _
    '                                        & ",OUT_S.TOU_NO                                        AS TOU_NO              " & vbNewLine _
    '                                        & ",OUT_S.SITU_NO                                       AS SITU_NO             " & vbNewLine _
    '                                        & ",OUT_S.ZONE_CD                                       AS ZONE_CD             " & vbNewLine _
    '                                        & ",OUT_S.LOCA                                          AS LOCA                " & vbNewLine _
    '                                        & ",OUT_S.LOT_NO                                        AS LOT_NO              " & vbNewLine _
    '                                        & ",OUT_S.SERIAL_NO                                     AS SERIAL_NO           " & vbNewLine _
    '                                        & ",OUT_S.OUTKA_TTL_NB                                  AS OUTKA_TTL_NB        " & vbNewLine _
    '                                        & ",OUT_S.OUTKA_TTL_QT                                  AS OUTKA_TTL_QT        " & vbNewLine _
    '                                        & ",OUT_S.ZAI_REC_NO                                    AS ZAI_REC_NO          " & vbNewLine _
    '                                        & ",OUT_S.INKA_NO_L                                     AS INKA_NO_L           " & vbNewLine _
    '                                        & ",OUT_S.INKA_NO_M                                     AS INKA_NO_M           " & vbNewLine _
    '                                        & ",OUT_S.INKA_NO_S                                     AS INKA_NO_S           " & vbNewLine _
    '                                        & ",OUT_S.ZAI_UPD_FLAG                                  AS ZAI_UPD_FLAG        " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_CAN_NB                                  AS ALCTD_CAN_NB        " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_NB                                      AS ALCTD_NB            " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_CAN_QT                                  AS ALCTD_CAN_QT        " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_QT                                      AS ALCTD_QT            " & vbNewLine _
    '                                        & ",OUT_S.IRIME                                         AS IRIME               " & vbNewLine _
    '                                        & ",OUT_S.BETU_WT                                       AS BETU_WT             " & vbNewLine _
    '                                        & ",OUT_S.COA_FLAG                                      AS COA_FLAG            " & vbNewLine _
    '                                        & ",OUT_S.REMARK                                        AS REMARK              " & vbNewLine _
    '                                        & ",OUT_S.SMPL_FLAG                                     AS SMPL_FLAG           " & vbNewLine _
    '                                        & ",OUT_S.SYS_UPD_DATE                                  AS SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",OUT_S.SYS_UPD_TIME                                  AS SYS_UPD_TIME        " & vbNewLine _
    '                                        & ",OUT_S.SYS_DEL_FLG                                   AS SYS_DEL_FLG         " & vbNewLine _
    '                                        & ",'1'                                                 AS UP_KBN              " & vbNewLine _
    '                                        & ",OUT_S.REC_NO                                        AS REC_NO              " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_CAN_NB                                  AS ALCTD_CAN_NB_GAMEN  " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_NB                                      AS ALCTD_NB_GAMEN      " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_CAN_QT                                  AS ALCTD_CAN_QT_GAMEN  " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_QT                                      AS ALCTD_QT_GAMEN      " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_CAN_NB                                  AS ALCTD_CAN_NB_MATOME " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_NB                                      AS ALCTD_NB_MATOME     " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_CAN_QT                                  AS ALCTD_CAN_QT_MATOME " & vbNewLine _
    '                                        & ",OUT_S.ALCTD_QT                                      AS ALCTD_QT_MATOME     " & vbNewLine _
    '                                        & ",'1'                                                 AS MATOME_FLG          " & vbNewLine _
    '                                        & ",ISNULL(OUT_S2.OUTKA_NO_L,'')                        AS OUTKA_NO_L2         " & vbNewLine _
    '                                        & ",ISNULL(IDO.N_ZAI_REC_NO,'')                         AS N_ZAI_REC_NO        " & vbNewLine _
    '                                        & ",OUT_S.IRIME                                         AS KOWAKE_IRIME        " & vbNewLine _
    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUT_S As String = " SELECT                                                                    " & vbNewLine _
                                            & " OUT_S.NRS_BR_CD                                     AS NRS_BR_CD           " & vbNewLine _
                                            & ",OUT_S.OUTKA_NO_L                                    AS OUTKA_NO_L          " & vbNewLine _
                                            & ",OUT_S.OUTKA_NO_M                                    AS OUTKA_NO_M          " & vbNewLine _
                                            & ",OUT_S.OUTKA_NO_S                                    AS OUTKA_NO_S          " & vbNewLine _
                                            & ",OUT_S.TOU_NO                                        AS TOU_NO              " & vbNewLine _
                                            & ",OUT_S.SITU_NO                                       AS SITU_NO             " & vbNewLine _
                                            & ",OUT_S.ZONE_CD                                       AS ZONE_CD             " & vbNewLine _
                                            & ",OUT_S.LOCA                                          AS LOCA                " & vbNewLine _
                                            & ",OUT_S.LOT_NO                                        AS LOT_NO              " & vbNewLine _
                                            & ",OUT_S.SERIAL_NO                                     AS SERIAL_NO           " & vbNewLine _
                                            & ",OUT_S.OUTKA_TTL_NB                                  AS OUTKA_TTL_NB        " & vbNewLine _
                                            & ",OUT_S.OUTKA_TTL_QT                                  AS OUTKA_TTL_QT        " & vbNewLine _
                                            & ",OUT_S.ZAI_REC_NO                                    AS ZAI_REC_NO          " & vbNewLine _
                                            & ",OUT_S.INKA_NO_L                                     AS INKA_NO_L           " & vbNewLine _
                                            & ",OUT_S.INKA_NO_M                                     AS INKA_NO_M           " & vbNewLine _
                                            & ",OUT_S.INKA_NO_S                                     AS INKA_NO_S           " & vbNewLine _
                                            & ",OUT_S.ZAI_UPD_FLAG                                  AS ZAI_UPD_FLAG        " & vbNewLine _
                                            & ",OUT_S.ALCTD_CAN_NB                                  AS ALCTD_CAN_NB        " & vbNewLine _
                                            & ",OUT_S.ALCTD_NB                                      AS ALCTD_NB            " & vbNewLine _
                                            & ",OUT_S.ALCTD_CAN_QT                                  AS ALCTD_CAN_QT        " & vbNewLine _
                                            & ",OUT_S.ALCTD_QT                                      AS ALCTD_QT            " & vbNewLine _
                                            & ",OUT_S.IRIME                                         AS IRIME               " & vbNewLine _
                                            & ",OUT_S.BETU_WT                                       AS BETU_WT             " & vbNewLine _
                                            & ",OUT_S.COA_FLAG                                      AS COA_FLAG            " & vbNewLine _
                                            & ",OUT_S.REMARK                                        AS REMARK              " & vbNewLine _
                                            & ",OUT_S.SMPL_FLAG                                     AS SMPL_FLAG           " & vbNewLine _
                                            & ",OUT_S.SYS_UPD_DATE                                  AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",OUT_S.SYS_UPD_TIME                                  AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",OUT_S.SYS_DEL_FLG                                   AS SYS_DEL_FLG         " & vbNewLine _
                                            & ",'1'                                                 AS UP_KBN              " & vbNewLine _
                                            & ",OUT_S.REC_NO                                        AS REC_NO              " & vbNewLine _
                                            & ",OUT_S.ALCTD_CAN_NB                                  AS ALCTD_CAN_NB_GAMEN  " & vbNewLine _
                                            & ",OUT_S.ALCTD_NB                                      AS ALCTD_NB_GAMEN      " & vbNewLine _
                                            & ",OUT_S.ALCTD_CAN_QT                                  AS ALCTD_CAN_QT_GAMEN  " & vbNewLine _
                                            & ",OUT_S.ALCTD_QT                                      AS ALCTD_QT_GAMEN      " & vbNewLine _
                                            & ",OUT_S.ALCTD_CAN_NB                                  AS ALCTD_CAN_NB_MATOME " & vbNewLine _
                                            & ",OUT_S.ALCTD_NB                                      AS ALCTD_NB_MATOME     " & vbNewLine _
                                            & ",OUT_S.ALCTD_CAN_QT                                  AS ALCTD_CAN_QT_MATOME " & vbNewLine _
                                            & ",OUT_S.ALCTD_QT                                      AS ALCTD_QT_MATOME     " & vbNewLine _
                                            & ",'1'                                                 AS MATOME_FLG          " & vbNewLine _
                                            & ",ISNULL(OUT_S2.OUTKA_NO_L,'')                        AS OUTKA_NO_L2         " & vbNewLine _
                                            & ",ISNULL(IDO.N_ZAI_REC_NO,'')                         AS N_ZAI_REC_NO        " & vbNewLine _
                                            & ",ISNULL(IDO.O_ZAI_REC_NO,'')                         AS O_ZAI_REC_NO        " & vbNewLine _
                                            & ",OUT_S.IRIME                                         AS KOWAKE_IRIME        " & vbNewLine _
                                            & ",INKA_L.INKA_DATE                                    AS INKA_DATE           --ADD 2018/11/14 要望番号001939 " & vbNewLine
    'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
    'END YANAI 20110913 小分け対応

    'START YANAI 20110913 小分け対応
    '''' <summary>
    '''' FROM
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM_OUT_S As String = "FROM                                                    " & vbNewLine _
    '                                     & "$LM_TRN$..C_OUTKA_S  OUT_S                                " & vbNewLine
    'START YANAI 要望番号711
    '''' <summary>
    '''' FROM
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM_OUT_S As String = "FROM                                                    " & vbNewLine _
    '                                     & "$LM_TRN$..C_OUTKA_S  OUT_S                                " & vbNewLine _
    '                                     & "LEFT JOIN $LM_TRN$..D_IDO_TRS    IDO                      " & vbNewLine _
    '                                     & "ON     IDO.NRS_BR_CD = OUT_S.NRS_BR_CD                    " & vbNewLine _
    '                                     & "AND    IDO.REC_NO = OUT_S.REC_NO                          " & vbNewLine _
    '                                     & "AND    IDO.SYS_DEL_FLG = '0'                              " & vbNewLine _
    '                                     & "LEFT JOIN $LM_TRN$..C_OUTKA_S  OUT_S2                     " & vbNewLine _
    '                                     & "ON     OUT_S2.NRS_BR_CD = IDO.NRS_BR_CD                   " & vbNewLine _
    '                                     & "AND    OUT_S2.ZAI_REC_NO = IDO.N_ZAI_REC_NO               " & vbNewLine _
    '                                     & "AND    OUT_S2.SYS_DEL_FLG = '0'                           " & vbNewLine _
    'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
    '''' <summary>
    '''' FROM
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM_OUT_S As String = "FROM                                                    " & vbNewLine _
    '                                     & "$LM_TRN$..C_OUTKA_S  OUT_S                                " & vbNewLine _
    '                                     & "LEFT JOIN $LM_TRN$..D_IDO_TRS    IDO                      " & vbNewLine _
    '                                     & "ON     IDO.NRS_BR_CD = OUT_S.NRS_BR_CD                    " & vbNewLine _
    '                                     & "AND    IDO.REC_NO = OUT_S.REC_NO                          " & vbNewLine _
    '                                     & "AND    IDO.SYS_DEL_FLG = '0'                              " & vbNewLine _
    '                                     & "LEFT JOIN $LM_TRN$..C_OUTKA_S  OUT_S2                     " & vbNewLine _
    '                                     & "ON     OUT_S2.NRS_BR_CD = IDO.NRS_BR_CD                   " & vbNewLine _
    '                                     & "AND    OUT_S2.OUTKA_NO_L = @OUTKA_NO_L                    " & vbNewLine _
    '                                     & "AND    OUT_S2.ZAI_REC_NO = IDO.N_ZAI_REC_NO               " & vbNewLine _
    '                                     & "AND    OUT_S2.SYS_DEL_FLG = '0'                           " & vbNewLine _
    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_OUT_S As String = "FROM                                                    " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_S  OUT_S                                " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..D_IDO_TRS    IDO                      " & vbNewLine _
                                         & "ON     IDO.NRS_BR_CD = OUT_S.NRS_BR_CD                    " & vbNewLine _
                                         & "AND    IDO.REC_NO = OUT_S.REC_NO                          " & vbNewLine _
                                         & "AND    IDO.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..C_OUTKA_S  OUT_S2                     " & vbNewLine _
                                         & "ON     OUT_S2.NRS_BR_CD = IDO.NRS_BR_CD                   " & vbNewLine _
                                         & "AND    OUT_S2.OUTKA_NO_L <> @OUTKA_NO_L                   " & vbNewLine _
                                         & "AND    OUT_S2.ZAI_REC_NO = IDO.N_ZAI_REC_NO               " & vbNewLine _
                                         & "AND    OUT_S2.REC_NO = IDO.REC_NO                         " & vbNewLine _
                                         & "AND    OUT_S2.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "--ADD START 2018/11/14 要望番号001939                     " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..B_INKA_L  INKA_L                      " & vbNewLine _
                                         & "ON     INKA_L.NRS_BR_CD = OUT_S.NRS_BR_CD                 " & vbNewLine _
                                         & "AND    INKA_L.INKA_NO_L = OUT_S.INKA_NO_L                 " & vbNewLine _
                                         & "AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "--ADD END   2018/11/14 要望番号001939                     " & vbNewLine

    'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
    'END YANAI 要望番号711
    'END YANAI 20110913 小分け対応

    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_OUT_S As String = "WHERE                                                  " & vbNewLine _
                                         & "    OUT_S.NRS_BR_CD              = @NRS_BR_CD             " & vbNewLine _
                                         & "AND OUT_S.OUTKA_NO_L             = @OUTKA_NO_L            " & vbNewLine _
                                         & "AND OUT_S.SYS_DEL_FLG            = '0'                    " & vbNewLine

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_OUT_S As String = "ORDER BY                                            " & vbNewLine _
                                         & " OUT_S.OUTKA_NO_L                                         " & vbNewLine _
                                         & ",OUT_S.OUTKA_NO_M                                         " & vbNewLine _
                                         & ",OUT_S.OUTKA_NO_S                                         " & vbNewLine

#End Region

#Region "SAGYO"
    'START YANAI 要望番号820
    '''' <summary>
    '''' SELECT
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_SAGYO As String = " SELECT                                                                    " & vbNewLine _
    '                                        & " SAGYO.SAGYO_COMP                                    AS SAGYO_COMP          " & vbNewLine _
    '                                        & ",SAGYO.SKYU_CHK                                      AS SKYU_CHK            " & vbNewLine _
    '                                        & ",SAGYO.SAGYO_REC_NO                                  AS SAGYO_REC_NO        " & vbNewLine _
    '                                        & ",SAGYO.SAGYO_SIJI_NO                                 AS SAGYO_SIJI_NO       " & vbNewLine _
    '                                        & ",SAGYO.INOUTKA_NO_LM                                 AS INOUTKA_NO_LM       " & vbNewLine _
    '                                        & ",SAGYO.NRS_BR_CD                                     AS NRS_BR_CD           " & vbNewLine _
    '                                        & ",SAGYO.WH_CD                                         AS WH_CD               " & vbNewLine _
    '                                        & ",SAGYO.SAGYO_CD                                      AS SAGYO_CD            " & vbNewLine _
    '                                        & ",SAGYO.DEST_SAGYO_FLG                                AS DEST_SAGYO_FLG      " & vbNewLine _
    '                                        & ",SAGYO.CUST_CD_L                                     AS CUST_CD_L           " & vbNewLine _
    '                                        & ",SAGYO.CUST_CD_M                                     AS CUST_CD_M           " & vbNewLine _
    '                                        & ",MSAGYO.SAGYO_RYAK                                   AS SAGYO_RYAK          " & vbNewLine _
    '                                        & ",SAGYO.SAGYO_NM                                      AS SAGYO_NM            " & vbNewLine _
    '                                        & ",SAGYO.DEST_CD                                       AS DEST_CD             " & vbNewLine _
    '                                        & ",SAGYO.DEST_NM                                       AS DEST_NM             " & vbNewLine _
    '                                        & ",SAGYO.LOT_NO                                        AS LOT_NO              " & vbNewLine _
    '                                        & ",SAGYO.INV_TANI                                      AS INV_TANI            " & vbNewLine _
    '                                        & ",SAGYO.SAGYO_NB                                      AS SAGYO_NB            " & vbNewLine _
    '                                        & ",SAGYO.SAGYO_UP                                      AS SAGYO_UP            " & vbNewLine _
    '                                        & ",SAGYO.SAGYO_GK                                      AS SAGYO_GK            " & vbNewLine _
    '                                        & ",SAGYO.TAX_KB                                        AS TAX_KB              " & vbNewLine _
    '                                        & ",SAGYO.SEIQTO_CD                                     AS SEIQTO_CD           " & vbNewLine _
    '                                        & ",SAGYO.REMARK_SKYU                                   AS REMARK_SKYU         " & vbNewLine _
    '                                        & ",SAGYO.SYS_UPD_DATE                                  AS SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",SAGYO.SYS_UPD_TIME                                  AS SYS_UPD_TIME        " & vbNewLine _
    '                                        & ",SAGYO.SYS_DEL_FLG                                   AS SYS_DEL_FLG         " & vbNewLine _
    '                                        & ",'1'                                                 AS UP_KBN              " & vbNewLine
    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGYO As String = " SELECT                                                                    " & vbNewLine _
                                            & " SAGYO.SAGYO_COMP                                    AS SAGYO_COMP          " & vbNewLine _
                                            & ",SAGYO.SKYU_CHK                                      AS SKYU_CHK            " & vbNewLine _
                                            & ",SAGYO.SAGYO_REC_NO                                  AS SAGYO_REC_NO        " & vbNewLine _
                                            & ",SAGYO.SAGYO_SIJI_NO                                 AS SAGYO_SIJI_NO       " & vbNewLine _
                                            & ",SAGYO.INOUTKA_NO_LM                                 AS INOUTKA_NO_LM       " & vbNewLine _
                                            & ",SAGYO.NRS_BR_CD                                     AS NRS_BR_CD           " & vbNewLine _
                                            & ",SAGYO.WH_CD                                         AS WH_CD               " & vbNewLine _
                                            & ",SAGYO.SAGYO_CD                                      AS SAGYO_CD            " & vbNewLine _
                                            & ",SAGYO.DEST_SAGYO_FLG                                AS DEST_SAGYO_FLG      " & vbNewLine _
                                            & ",SAGYO.CUST_CD_L                                     AS CUST_CD_L           " & vbNewLine _
                                            & ",SAGYO.CUST_CD_M                                     AS CUST_CD_M           " & vbNewLine _
                                            & ",MSAGYO.SAGYO_RYAK                                   AS SAGYO_RYAK          " & vbNewLine _
                                            & ",SAGYO.SAGYO_NM                                      AS SAGYO_NM            " & vbNewLine _
                                            & ",SAGYO.DEST_CD                                       AS DEST_CD             " & vbNewLine _
                                            & ",SAGYO.DEST_NM                                       AS DEST_NM             " & vbNewLine _
                                            & ",SAGYO.LOT_NO                                        AS LOT_NO              " & vbNewLine _
                                            & ",SAGYO.INV_TANI                                      AS INV_TANI            " & vbNewLine _
                                            & ",SAGYO.SAGYO_NB                                      AS SAGYO_NB            " & vbNewLine _
                                            & ",SAGYO.SAGYO_UP                                      AS SAGYO_UP            " & vbNewLine _
                                            & ",SAGYO.SAGYO_GK                                      AS SAGYO_GK            " & vbNewLine _
                                            & ",SAGYO.TAX_KB                                        AS TAX_KB              " & vbNewLine _
                                            & ",SAGYO.SEIQTO_CD                                     AS SEIQTO_CD           " & vbNewLine _
                                            & ",SAGYO.REMARK_SKYU                                   AS REMARK_SKYU         " & vbNewLine _
                                            & ",SAGYO.SYS_UPD_DATE                                  AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",SAGYO.SYS_UPD_TIME                                  AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",SAGYO.SYS_DEL_FLG                                   AS SYS_DEL_FLG         " & vbNewLine _
                                            & ",SAGYO.IOZS_KB                                       AS IOZS_KB             " & vbNewLine _
                                            & ",SAGYO.GOODS_CD_NRS                                  AS GOODS_CD_NRS        " & vbNewLine _
                                            & ",SAGYO.GOODS_NM_NRS                                  AS GOODS_NM_NRS        " & vbNewLine _
                                            & ",SAGYO.REMARK_ZAI                                    AS REMARK_ZAI          " & vbNewLine _
                                            & ",SAGYO.SAGYO_COMP_CD                                 AS SAGYO_COMP_CD       " & vbNewLine _
                                            & ",SAGYO.SAGYO_COMP_DATE                               AS SAGYO_COMP_DATE     " & vbNewLine _
                                            & ",'1'                                                 AS UP_KBN              " & vbNewLine _
                                            & ",SAGYO.REMARK_SIJI                                   AS REMARK_SIJI         " & vbNewLine

    'END YANAI 要望番号820

    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_SAGYO As String = "FROM                                                    " & vbNewLine _
                                         & "$LM_TRN$..E_SAGYO  SAGYO                                  " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_SAGYO    MSAGYO                     " & vbNewLine _
                                         & "ON     SAGYO.NRS_BR_CD = MSAGYO.NRS_BR_CD                 " & vbNewLine _
                                         & "AND    SAGYO.SAGYO_CD = MSAGYO.SAGYO_CD                   " & vbNewLine

    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_SAGYO As String = "WHERE                                                  " & vbNewLine _
                                         & "    SAGYO.NRS_BR_CD              = @NRS_BR_CD             " & vbNewLine _
                                         & "AND SAGYO.INOUTKA_NO_LM          LIKE @OUTKA_NO_L         " & vbNewLine _
                                         & "AND SAGYO.SYS_DEL_FLG            = '0'                    " & vbNewLine _
                                         & "AND    (SAGYO.IOZS_KB = '20'                              " & vbNewLine _
                                         & "OR      SAGYO.IOZS_KB = '21')                             " & vbNewLine

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_SAGYO As String = "ORDER BY                                            " & vbNewLine _
                                         & " SAGYO.SAGYO_REC_NO                                       " & vbNewLine

#End Region

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "UNSO_L"
    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNSO_L As String = " SELECT TOP 1                                                             " & vbNewLine _
                                            & " UNSO_L.NRS_BR_CD                                    AS NRS_BR_CD           " & vbNewLine _
                                            & ",UNSO_L.UNSO_NO_L                                    AS UNSO_NO_L           " & vbNewLine _
                                            & ",UNSO_L.YUSO_BR_CD                                   AS YUSO_BR_CD          " & vbNewLine _
                                            & ",UNSO_L.INOUTKA_NO_L                                 AS INOUTKA_NO_L        " & vbNewLine _
                                            & ",UNSO_L.TRIP_NO                                      AS TRIP_NO             " & vbNewLine _
                                            & ",UNSO_L.UNSO_CD                                      AS UNSO_CD             " & vbNewLine _
                                            & ",UNSO_L.UNSO_BR_CD                                   AS UNSO_BR_CD          " & vbNewLine _
                                            & ",UNSOCO.TARE_YN                                      AS TARE_YN             " & vbNewLine _
                                            & ",UNSO_L.BIN_KB                                       AS BIN_KB              " & vbNewLine _
                                            & ",UNSO_L.JIYU_KB                                      AS JIYU_KB             " & vbNewLine _
                                            & ",UNSO_L.DENP_NO                                      AS DENP_NO             " & vbNewLine _
                                            & "--(2015.09.18)要望番号2408 追加START                                        " & vbNewLine _
                                            & ",UNSO_L.AUTO_DENP_KBN                                AS AUTO_DENP_KBN       " & vbNewLine _
                                            & ",UNSO_L.AUTO_DENP_NO                                 AS AUTO_DENP_NO        " & vbNewLine _
                                            & "--(2015.09.18)要望番号2408 追加START                                        " & vbNewLine _
                                            & ",UNSO_L.OUTKA_PLAN_DATE                              AS OUTKA_PLAN_DATE     " & vbNewLine _
                                            & ",UNSO_L.OUTKA_PLAN_TIME                              AS OUTKA_PLAN_TIME     " & vbNewLine _
                                            & ",UNSO_L.ARR_PLAN_DATE                                AS ARR_PLAN_DATE       " & vbNewLine _
                                            & ",UNSO_L.ARR_PLAN_TIME                                AS ARR_PLAN_TIME       " & vbNewLine _
                                            & ",UNSO_L.ARR_ACT_TIME                                 AS ARR_ACT_TIME        " & vbNewLine _
                                            & ",UNSO_L.CUST_CD_L                                    AS CUST_CD_L           " & vbNewLine _
                                            & ",UNSO_L.CUST_CD_M                                    AS CUST_CD_M           " & vbNewLine _
                                            & ",UNSO_L.CUST_REF_NO                                  AS CUST_REF_NO         " & vbNewLine _
                                            & ",UNSO_L.SHIP_CD                                      AS SHIP_CD             " & vbNewLine _
                                            & ",UNSO_L.ORIG_CD                                      AS ORIG_CD             " & vbNewLine _
                                            & ",UNSO_L.DEST_CD                                      AS DEST_CD             " & vbNewLine _
                                            & ",UNSO_L.UNSO_PKG_NB                                  AS UNSO_PKG_NB         " & vbNewLine _
                                            & ",UNSO_L.NB_UT                                        AS NB_UT               " & vbNewLine _
                                            & ",UNSO_L.UNSO_WT                                      AS UNSO_WT             " & vbNewLine _
                                            & ",UNSO_L.UNSO_ONDO_KB                                 AS UNSO_ONDO_KB        " & vbNewLine _
                                            & ",UNSO_L.PC_KB                                        AS PC_KB               " & vbNewLine _
                                            & ",UNSO_L.TARIFF_BUNRUI_KB                             AS TARIFF_BUNRUI_KB    " & vbNewLine _
                                            & ",UNSO_L.VCLE_KB                                      AS VCLE_KB             " & vbNewLine _
                                            & ",UNSO_L.MOTO_DATA_KB                                 AS MOTO_DATA_KB        " & vbNewLine _
                                            & ",UNSO_L.TAX_KB                                       AS TAX_KB              " & vbNewLine _
                                            & ",UNSO_L.REMARK                                       AS REMARK              " & vbNewLine _
                                            & ",UNSO_L.SEIQ_TARIFF_CD                               AS SEIQ_TARIFF_CD      " & vbNewLine _
                                            & ",UNSO_L.SEIQ_ETARIFF_CD                              AS SEIQ_ETARIFF_CD     " & vbNewLine _
                                            & ",UNSO_L.AD_3                                         AS AD_3                " & vbNewLine _
                                            & ",UNSO_L.UNSO_TEHAI_KB                                AS UNSO_TEHAI_KB       " & vbNewLine _
                                            & ",UNSO_L.BUY_CHU_NO                                   AS BUY_CHU_NO          " & vbNewLine _
                                            & ",UNSO_L.AREA_CD                                      AS AREA_CD             " & vbNewLine _
                                            & ",UNSO_L.TYUKEI_HAISO_FLG                             AS TYUKEI_HAISO_FLG    " & vbNewLine _
                                            & ",UNSO_L.SYUKA_TYUKEI_CD                              AS SYUKA_TYUKEI_CD     " & vbNewLine _
                                            & ",UNSO_L.HAIKA_TYUKEI_CD                              AS HAIKA_TYUKEI_CD     " & vbNewLine _
                                            & ",UNSO_L.TRIP_NO_SYUKA                                AS TRIP_NO_SYUKA       " & vbNewLine _
                                            & ",UNSO_L.TRIP_NO_TYUKEI                               AS TRIP_NO_TYUKEI      " & vbNewLine _
                                            & ",UNSO_L.TRIP_NO_HAIKA                                AS TRIP_NO_HAIKA       " & vbNewLine _
                                            & ",UNCHIN.SEIQ_KYORI                                   AS KYORI               " & vbNewLine _
                                            & ",UNSOCO.UNSOCO_NM                                    AS UNSOCO_NM           " & vbNewLine _
                                            & ",UNSOCO.UNSOCO_BR_NM                                 AS UNSOCO_BR_NM        " & vbNewLine _
                                            & ",CASE WHEN UNSO_L.UNSO_TEHAI_KB = '10' OR UNSO_L.UNSO_TEHAI_KB = '20' OR    " & vbNewLine _
                                            & "           UNSO_L.UNSO_TEHAI_KB = '30'                                      " & vbNewLine _
                                            & "      THEN TARRIF.UNCHIN_TARIFF_REM                                         " & vbNewLine _
                                            & "      WHEN UNSO_L.UNSO_TEHAI_KB = '40'                                      " & vbNewLine _
                                            & "      THEN YOKO_TARIFF.YOKO_REM                                             " & vbNewLine _
                                            & "      ELSE ''                                                               " & vbNewLine _
                                            & " END                                                 AS SEIQ_TARIFF_NM      " & vbNewLine _
                                            & ",UNCHIN.SEIQ_FIXED_FLAG                              AS SEIQ_FIXED_FLAG     " & vbNewLine _
                                            & ",UNSOCO.NIHUDA_YN                                    AS NIHUDA_YN           " & vbNewLine _
                                            & ",UNSO_L.SYS_UPD_DATE                                 AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",UNSO_L.SYS_UPD_TIME                                 AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",UNSO_L.SYS_DEL_FLG                                  AS SYS_DEL_FLG         " & vbNewLine _
                                            & ",'1'                                                 AS UP_KBN              " & vbNewLine _
                                            & ",'0'                                                 AS TORIKESI_FLG        " & vbNewLine _
                                            & ",UNSO_L.SHIHARAI_TARIFF_CD                           AS SHIHARAI_TARIFF_CD  " & vbNewLine _
                                            & ",CASE WHEN UNSO_L.UNSO_TEHAI_KB = '10' OR UNSO_L.UNSO_TEHAI_KB = '20' OR    " & vbNewLine _
                                            & "           UNSO_L.UNSO_TEHAI_KB = '30'                                      " & vbNewLine _
                                            & "      THEN SHI_TARRIF.SHIHARAI_TARIFF_REM                                   " & vbNewLine _
                                            & "      WHEN UNSO_L.UNSO_TEHAI_KB = '40'                                      " & vbNewLine _
                                            & "      THEN YOKO_SHIHARAI_TARIFF.YOKO_REM                                    " & vbNewLine _
                                            & "      ELSE ''                                                               " & vbNewLine _
                                            & " END                                                 AS SHIHARAI_TARIFF_NM  " & vbNewLine _
                                            & ",UNSO_L.SHIHARAI_ETARIFF_CD                          AS SHIHARAI_ETARIFF_CD " & vbNewLine _
                                            & ",SHIHARAI.SHIHARAI_FIXED_FLAG                        AS SHIHARAI_FIXED_FLAG " & vbNewLine _
                                            & "--要望番号:1683 yamanaka 2013.03.04 START                                   " & vbNewLine _
                                            & ",SHIHARAI.SHIHARAI_GROUP_NO                          AS SHIHARAI_GROUP_NO   " & vbNewLine _
                                            & ",UNCHIN.SEIQ_GROUP_NO                                AS SEIQ_GROUP_NO       " & vbNewLine _
                                            & "--要望番号:1683 yamanaka 2013.03.04 END                                     " & vbNewLine


    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_UNSO_L As String = "FROM                                                   " & vbNewLine _
                                         & "$LM_TRN$..F_UNSO_L  UNSO_L                                " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_DEST    DEST                        " & vbNewLine _
                                         & "ON     UNSO_L.NRS_BR_CD = DEST.NRS_BR_CD                  " & vbNewLine _
                                         & "AND    UNSO_L.CUST_CD_L = DEST.CUST_CD_L                  " & vbNewLine _
                                         & "AND    UNSO_L.DEST_CD = DEST.DEST_CD                      " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_UNSOCO    UNSOCO                    " & vbNewLine _
                                         & "ON     UNSO_L.NRS_BR_CD = UNSOCO.NRS_BR_CD                " & vbNewLine _
                                         & "AND    UNSO_L.UNSO_CD = UNSOCO.UNSOCO_CD                  " & vbNewLine _
                                         & "AND    UNSO_L.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD            " & vbNewLine _
                                         & "LEFT  JOIN                                                " & vbNewLine _
                                         & "(SELECT                                                   " & vbNewLine _
                                         & "TARRIF2.NRS_BR_CD                                         " & vbNewLine _
                                         & ",TARRIF2.UNCHIN_TARIFF_CD                                 " & vbNewLine _
                                         & ",TARRIF2.UNCHIN_TARIFF_CD_EDA                             " & vbNewLine _
                                         & ",TARRIF2.STR_DATE                                         " & vbNewLine _
                                         & ",TARRIF2.UNCHIN_TARIFF_REM                                " & vbNewLine _
                                         & ",TARRIF2.SYS_DEL_FLG                                      " & vbNewLine _
                                         & "FROM  $LM_MST$..M_UNCHIN_TARIFF  TARRIF2                  " & vbNewLine _
                                         & "WHERE TARRIF2.NRS_BR_CD               = @NRS_BR_CD        " & vbNewLine _
                                         & ")  TARRIF                                                 " & vbNewLine _
                                         & "ON    UNSO_L.NRS_BR_CD                = TARRIF.NRS_BR_CD  " & vbNewLine _
                                         & "AND   UNSO_L.SEIQ_TARIFF_CD           = TARRIF.UNCHIN_TARIFF_CD" & vbNewLine _
                                         & "AND   @STR_DATE                 >= TARRIF.STR_DATE        " & vbNewLine _
                                         & "LEFT  JOIN                                                " & vbNewLine _
                                         & "(SELECT                                                   " & vbNewLine _
                                         & "TARRIF3.NRS_BR_CD                                         " & vbNewLine _
                                         & ",TARRIF3.SHIHARAI_TARIFF_CD                               " & vbNewLine _
                                         & ",TARRIF3.SHIHARAI_TARIFF_CD_EDA                           " & vbNewLine _
                                         & ",TARRIF3.STR_DATE                                         " & vbNewLine _
                                         & ",TARRIF3.SHIHARAI_TARIFF_REM                              " & vbNewLine _
                                         & ",TARRIF3.SYS_DEL_FLG                                      " & vbNewLine _
                                         & "FROM  $LM_MST$..M_SHIHARAI_TARIFF  TARRIF3                " & vbNewLine _
                                         & "WHERE TARRIF3.NRS_BR_CD               = @NRS_BR_CD        " & vbNewLine _
                                         & ")  SHI_TARRIF                                             " & vbNewLine _
                                         & "ON    UNSO_L.NRS_BR_CD                = SHI_TARRIF.NRS_BR_CD  " & vbNewLine _
                                         & "AND   UNSO_L.SHIHARAI_TARIFF_CD       = SHI_TARRIF.SHIHARAI_TARIFF_CD" & vbNewLine _
                                         & "AND   @STR_DATE2                 >= SHI_TARRIF.STR_DATE   " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_YOKO_TARIFF_HD    YOKO_TARIFF       " & vbNewLine _
                                         & "ON     UNSO_L.NRS_BR_CD = YOKO_TARIFF.NRS_BR_CD           " & vbNewLine _
                                         & "AND    UNSO_L.SEIQ_TARIFF_CD = YOKO_TARIFF.YOKO_TARIFF_CD " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..F_UNCHIN_TRS    UNCHIN                " & vbNewLine _
                                         & "ON     UNSO_L.NRS_BR_CD = UNCHIN.NRS_BR_CD                " & vbNewLine _
                                         & "AND    UNSO_L.UNSO_NO_L = UNCHIN.UNSO_NO_L                " & vbNewLine _
                                         & "AND    UNCHIN.SYS_DEL_FLG = 0                             " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_YOKO_TARIFF_HD_SHIHARAI    YOKO_SHIHARAI_TARIFF  " & vbNewLine _
                                         & "ON     UNSO_L.NRS_BR_CD = YOKO_SHIHARAI_TARIFF.NRS_BR_CD               " & vbNewLine _
                                         & "AND    UNSO_L.SHIHARAI_TARIFF_CD = YOKO_SHIHARAI_TARIFF.YOKO_TARIFF_CD " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..F_SHIHARAI_TRS    SHIHARAI            " & vbNewLine _
                                         & "ON     UNSO_L.NRS_BR_CD = SHIHARAI.NRS_BR_CD              " & vbNewLine _
                                         & "AND    UNSO_L.UNSO_NO_L = SHIHARAI.UNSO_NO_L              " & vbNewLine _
                                         & "AND    UNCHIN.SYS_DEL_FLG = 0                             " & vbNewLine

    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_UNSO_L As String = "WHERE                                                 " & vbNewLine _
                                         & "    UNSO_L.NRS_BR_CD              = @NRS_BR_CD            " & vbNewLine _
                                         & "AND UNSO_L.INOUTKA_NO_L           = @OUTKA_NO_L           " & vbNewLine _
                                         & "AND UNSO_L.MOTO_DATA_KB           = '20'                  " & vbNewLine _
                                         & "AND UNSO_L.SYS_DEL_FLG            = '0'                   " & vbNewLine

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_UNSO_L As String = "GROUP BY                                           " & vbNewLine _
                                         & " UNSO_L.NRS_BR_CD                                         " & vbNewLine _
                                         & ",UNSO_L.UNSO_NO_L                                         " & vbNewLine _
                                         & ",UNSO_L.YUSO_BR_CD                                        " & vbNewLine _
                                         & ",UNSO_L.INOUTKA_NO_L                                      " & vbNewLine _
                                         & ",UNSO_L.TRIP_NO                                           " & vbNewLine _
                                         & ",UNSO_L.UNSO_CD                                           " & vbNewLine _
                                         & ",UNSO_L.UNSO_BR_CD                                        " & vbNewLine _
                                         & ",UNSOCO.TARE_YN                                           " & vbNewLine _
                                         & ",UNSO_L.BIN_KB                                            " & vbNewLine _
                                         & ",UNSO_L.JIYU_KB                                           " & vbNewLine _
                                         & ",UNSO_L.DENP_NO                                           " & vbNewLine _
                                         & "--(2015.09.18)要望番号2408 追加START                      " & vbNewLine _
                                         & ",UNSO_L.AUTO_DENP_KBN                                     " & vbNewLine _
                                         & ",UNSO_L.AUTO_DENP_NO                                      " & vbNewLine _
                                         & "--(2015.09.18)要望番号2408 追加START                      " & vbNewLine _
                                         & ",UNSO_L.OUTKA_PLAN_DATE                                   " & vbNewLine _
                                         & ",UNSO_L.OUTKA_PLAN_TIME                                   " & vbNewLine _
                                         & ",UNSO_L.ARR_PLAN_DATE                                     " & vbNewLine _
                                         & ",UNSO_L.ARR_PLAN_TIME                                     " & vbNewLine _
                                         & ",UNSO_L.ARR_ACT_TIME                                      " & vbNewLine _
                                         & ",UNSO_L.CUST_CD_L                                         " & vbNewLine _
                                         & ",UNSO_L.CUST_CD_M                                         " & vbNewLine _
                                         & ",UNSO_L.CUST_REF_NO                                       " & vbNewLine _
                                         & ",UNSO_L.SHIP_CD                                           " & vbNewLine _
                                         & ",UNSO_L.ORIG_CD                                           " & vbNewLine _
                                         & ",UNSO_L.DEST_CD                                           " & vbNewLine _
                                         & ",UNSO_L.UNSO_PKG_NB                                       " & vbNewLine _
                                         & ",UNSO_L.NB_UT                                             " & vbNewLine _
                                         & ",UNSO_L.UNSO_WT                                           " & vbNewLine _
                                         & ",UNSO_L.UNSO_ONDO_KB                                      " & vbNewLine _
                                         & ",UNSO_L.PC_KB                                             " & vbNewLine _
                                         & ",UNSO_L.TARIFF_BUNRUI_KB                                  " & vbNewLine _
                                         & ",UNSO_L.VCLE_KB                                           " & vbNewLine _
                                         & ",UNSO_L.MOTO_DATA_KB                                      " & vbNewLine _
                                         & ",UNSO_L.TAX_KB                                            " & vbNewLine _
                                         & ",UNSO_L.REMARK                                            " & vbNewLine _
                                         & ",UNSO_L.SEIQ_TARIFF_CD                                    " & vbNewLine _
                                         & ",UNSO_L.SEIQ_ETARIFF_CD                                   " & vbNewLine _
                                         & ",UNSO_L.AD_3                                              " & vbNewLine _
                                         & ",UNSO_L.UNSO_TEHAI_KB                                     " & vbNewLine _
                                         & ",UNSO_L.BUY_CHU_NO                                        " & vbNewLine _
                                         & ",UNSO_L.AREA_CD                                           " & vbNewLine _
                                         & ",UNSO_L.TYUKEI_HAISO_FLG                                  " & vbNewLine _
                                         & ",UNSO_L.SYUKA_TYUKEI_CD                                   " & vbNewLine _
                                         & ",UNSO_L.HAIKA_TYUKEI_CD                                   " & vbNewLine _
                                         & ",UNSO_L.TRIP_NO_SYUKA                                     " & vbNewLine _
                                         & ",UNSO_L.TRIP_NO_TYUKEI                                    " & vbNewLine _
                                         & ",UNSO_L.TRIP_NO_HAIKA                                     " & vbNewLine _
                                         & ",UNCHIN.SEIQ_KYORI                                        " & vbNewLine _
                                         & ",UNSOCO.UNSOCO_NM                                         " & vbNewLine _
                                         & ",UNSOCO.UNSOCO_BR_NM                                      " & vbNewLine _
                                         & ",TARRIF.UNCHIN_TARIFF_REM                                 " & vbNewLine _
                                         & ",YOKO_TARIFF.YOKO_REM                                     " & vbNewLine _
                                         & ",UNSO_L.TAX_KB                                            " & vbNewLine _
                                         & ",UNCHIN.SEIQ_FIXED_FLAG                                   " & vbNewLine _
                                         & ",UNSO_L.SYS_UPD_DATE                                      " & vbNewLine _
                                         & ",UNSO_L.SYS_UPD_TIME                                      " & vbNewLine _
                                         & ",UNSO_L.SYS_DEL_FLG                                       " & vbNewLine _
                                         & ",UNSOCO.NIHUDA_YN                                         " & vbNewLine _
                                         & ",TARRIF.STR_DATE                                          " & vbNewLine _
                                         & ",TARRIF.UNCHIN_TARIFF_CD_EDA                              " & vbNewLine _
                                         & ",UNSO_L.SHIHARAI_TARIFF_CD                                " & vbNewLine _
                                         & ",SHI_TARRIF.SHIHARAI_TARIFF_REM                           " & vbNewLine _
                                         & ",YOKO_SHIHARAI_TARIFF.YOKO_REM                            " & vbNewLine _
                                         & ",UNSO_L.SHIHARAI_ETARIFF_CD                               " & vbNewLine _
                                         & ",SHIHARAI.SHIHARAI_FIXED_FLAG                             " & vbNewLine _
                                         & ",SHI_TARRIF.SHIHARAI_TARIFF_CD_EDA                        " & vbNewLine _
                                         & "--要望番号:1683 yamanaka 2013.03.04 START                 " & vbNewLine _
                                         & ",SHIHARAI.SHIHARAI_GROUP_NO                               " & vbNewLine _
                                         & ",UNCHIN.SEIQ_GROUP_NO                                     " & vbNewLine _
                                         & "--要望番号:1683 yamanaka 2013.03.04 END                   " & vbNewLine


    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_UNSO_L As String = "ORDER BY                                           " & vbNewLine _
                                         & " UNSO_L.UNSO_NO_L                                         " & vbNewLine _
                                         & ",TARRIF.STR_DATE                                          " & vbNewLine _
                                         & ",TARRIF.UNCHIN_TARIFF_CD_EDA                              " & vbNewLine _
                                         & ",SHI_TARRIF.SHIHARAI_TARIFF_CD_EDA                        " & vbNewLine

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    '2013.07.04 追加START
#Region "BP_EDI_OUT(BP運送重量再取得用)"

    Private Const SQL_GET_BP_EDI_OUT As String = "SELECT  HBP.OUTKA_CTL_NO AS INOUTKA_NO_L " & vbNewLine _
                                        & "--(2014.01.22)要望番号2149 修正START      " & vbNewLine _
                                        & "      , SUM(TOTAL_QT) AS BP_UNSO_WT       " & vbNewLine _
                                        & "--      , SUM(TOTAL_WT) AS BP_UNSO_WT       " & vbNewLine _
                                        & "--(2014.01.22)要望番号2149 修正END        " & vbNewLine _
                                        & "  FROM $LM_TRN$..H_OUTKAEDI_DTL_BP HBP   " & vbNewLine _
                                        & "LEFT JOIN                                 " & vbNewLine _
                                        & "$LM_TRN$..C_OUTKA_L CL                   " & vbNewLine _
                                        & "ON                                        " & vbNewLine _
                                        & "HBP.NRS_BR_CD = CL.NRS_BR_CD              " & vbNewLine _
                                        & "AND                                       " & vbNewLine _
                                        & "HBP.OUTKA_CTL_NO = CL.OUTKA_NO_L          " & vbNewLine _
                                        & "LEFT JOIN                                 " & vbNewLine _
                                        & "$LM_TRN$..F_UNSO_L FL                    " & vbNewLine _
                                        & "ON                                        " & vbNewLine _
                                        & "CL.NRS_BR_CD = FL.NRS_BR_CD               " & vbNewLine _
                                        & "AND                                       " & vbNewLine _
                                        & "CL.OUTKA_NO_L = FL.INOUTKA_NO_L           " & vbNewLine _
                                        & "AND                                       " & vbNewLine _
                                        & "FL.MOTO_DATA_KB = '20'                    " & vbNewLine _
                                        & "LEFT JOIN                                 " & vbNewLine _
                                        & "$LM_MST$..M_CUST_DETAILS MCD              " & vbNewLine _
                                        & "ON                                        " & vbNewLine _
                                        & "MCD.NRS_BR_CD = HBP.NRS_BR_CD             " & vbNewLine _
                                        & "AND                                       " & vbNewLine _
                                        & "MCD.CUST_CD = HBP.CUST_CD_L               " & vbNewLine _
                                        & "WHERE                                     " & vbNewLine _
                                        & "FL.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
                                        & "AND                                       " & vbNewLine _
                                        & "FL.UNSO_NO_L = @UNSO_NO_L                 " & vbNewLine _
                                        & "AND                                       " & vbNewLine _
                                        & "FL.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                        & "AND                                       " & vbNewLine _
                                        & "CL.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                        & "AND                                       " & vbNewLine _
                                        & "HBP.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                        & "AND                                       " & vbNewLine _
                                        & "MCD.SUB_KB = '58'                         " & vbNewLine _
                                        & "GROUP BY                                  " & vbNewLine _
                                        & "HBP.OUTKA_CTL_NO                          " & vbNewLine
#End Region
    '2013.07.04 追加END

#Region "UNSO_M"
    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNSO_M As String = " SELECT                                                                   " & vbNewLine _
                                            & " UNSO_M.NRS_BR_CD                                    AS NRS_BR_CD           " & vbNewLine _
                                            & ",UNSO_M.UNSO_NO_L                                    AS UNSO_NO_L           " & vbNewLine _
                                            & ",UNSO_M.UNSO_NO_M                                    AS UNSO_NO_M           " & vbNewLine _
                                            & ",UNSO_M.UNSO_TTL_NB                                  AS UNSO_TTL_NB         " & vbNewLine _
                                            & ",UNSO_M.ZBUKA_CD                                     AS ZBUKA_CD             " & vbNewLine _
                                            & ",UNSO_M.ABUKA_CD                                     AS ABUKA_CD             " & vbNewLine _
                                            & ",'1'                                                 AS UP_KBN              " & vbNewLine

    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_UNSO_M As String = "FROM                                                   " & vbNewLine _
                                         & "$LM_TRN$..F_UNSO_M  UNSO_M                                " & vbNewLine

    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_UNSO_M As String = "WHERE                                                 " & vbNewLine _
                                         & "    UNSO_M.NRS_BR_CD              = @NRS_BR_CD            " & vbNewLine _
                                         & "AND UNSO_M.UNSO_NO_L              = @UNSO_NO_L            " & vbNewLine _
                                         & "AND UNSO_M.SYS_DEL_FLG            = '0'                   " & vbNewLine


#End Region

#Region "ZAI_TRS"

    'START YANAI 要望番号780
    '''' <summary>
    '''' SELECT
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_ZAI As String = " SELECT                                                                      " & vbNewLine _
    '                                        & " ZAI.NRS_BR_CD                                       AS NRS_BR_CD           " & vbNewLine _
    '                                        & ",ZAI.ZAI_REC_NO                                      AS ZAI_REC_NO          " & vbNewLine _
    '                                        & ",ZAI.WH_CD                                           AS WH_CD               " & vbNewLine _
    '                                        & ",ZAI.TOU_NO                                          AS TOU_NO              " & vbNewLine _
    '                                        & ",ZAI.SITU_NO                                         AS SITU_NO             " & vbNewLine _
    '                                        & ",ZAI.ZONE_CD                                         AS ZONE_CD             " & vbNewLine _
    '                                        & ",ZAI.LOCA                                            AS LOCA                " & vbNewLine _
    '                                        & ",ZAI.LOT_NO                                          AS LOT_NO              " & vbNewLine _
    '                                        & ",ZAI.CUST_CD_L                                       AS CUST_CD_L           " & vbNewLine _
    '                                        & ",ZAI.CUST_CD_M                                       AS CUST_CD_M           " & vbNewLine _
    '                                        & ",ZAI.GOODS_CD_NRS                                    AS GOODS_CD_NRS        " & vbNewLine _
    '                                        & ",ZAI.INKA_NO_L                                       AS INKA_NO_L           " & vbNewLine _
    '                                        & ",ZAI.INKA_NO_M                                       AS INKA_NO_M           " & vbNewLine _
    '                                        & ",ZAI.INKA_NO_S                                       AS INKA_NO_S           " & vbNewLine _
    '                                        & ",ZAI.ALLOC_PRIORITY                                  AS ALLOC_PRIORITY      " & vbNewLine _
    '                                        & ",ZAI.RSV_NO                                          AS RSV_NO              " & vbNewLine _
    '                                        & ",ZAI.SERIAL_NO                                       AS SERIAL_NO           " & vbNewLine _
    '                                        & ",ZAI.HOKAN_YN                                        AS HOKAN_YN            " & vbNewLine _
    '                                        & ",ZAI.TAX_KB                                          AS TAX_KB              " & vbNewLine _
    '                                        & ",ZAI.GOODS_COND_KB_1                                 AS GOODS_COND_KB_1     " & vbNewLine _
    '                                        & ",ZAI.GOODS_COND_KB_2                                 AS GOODS_COND_KB_2     " & vbNewLine _
    '                                        & ",ZAI.GOODS_COND_KB_3                                 AS GOODS_COND_KB_3     " & vbNewLine _
    '                                        & ",ZAI.OFB_KB                                          AS OFB_KB              " & vbNewLine _
    '                                        & ",ZAI.SPD_KB                                          AS SPD_KB              " & vbNewLine _
    '                                        & ",ZAI.REMARK_OUT                                      AS REMARK_OUT          " & vbNewLine _
    '                                        & ",ZAI.PORA_ZAI_NB                                     AS PORA_ZAI_NB         " & vbNewLine _
    '                                        & ",ZAI.ALCTD_NB                                        AS ALCTD_NB            " & vbNewLine _
    '                                        & ",ZAI.ALLOC_CAN_NB                                    AS ALLOC_CAN_NB        " & vbNewLine _
    '                                        & ",ZAI.IRIME                                           AS IRIME               " & vbNewLine _
    '                                        & ",ZAI.PORA_ZAI_QT                                     AS PORA_ZAI_QT         " & vbNewLine _
    '                                        & ",ZAI.ALCTD_QT                                        AS ALCTD_QT            " & vbNewLine _
    '                                        & ",ZAI.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT        " & vbNewLine _
    '                                        & ",ZAI.INKO_DATE                                       AS INKO_DATE           " & vbNewLine _
    '                                        & ",ZAI.INKO_PLAN_DATE                                  AS INKO_PLAN_DATE      " & vbNewLine _
    '                                        & ",ZAI.ZERO_FLAG                                       AS ZERO_FLAG           " & vbNewLine _
    '                                        & ",ZAI.LT_DATE                                         AS LT_DATE             " & vbNewLine _
    '                                        & ",ZAI.GOODS_CRT_DATE                                  AS GOODS_CRT_DATE      " & vbNewLine _
    '                                        & ",ZAI.DEST_CD_P                                       AS DEST_CD_P           " & vbNewLine _
    '                                        & ",ZAI.REMARK                                          AS REMARK              " & vbNewLine _
    '                                        & ",ZAI.SMPL_FLAG                                       AS SMPL_FLAG           " & vbNewLine _
    '                                        & ",ZAI.SYS_UPD_DATE                                    AS SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",ZAI.SYS_UPD_TIME                                    AS SYS_UPD_TIME        " & vbNewLine _
    '                                        & ",Z1.KBN_NM1                                          AS GOODS_COND_NM_1     " & vbNewLine _
    '                                        & ",Z2.KBN_NM1                                          AS GOODS_COND_NM_2     " & vbNewLine _
    '                                        & ",CUSCON.JOTAI_NM                                     AS GOODS_COND_NM_3     " & vbNewLine _
    '                                        & ",Z3.KBN_NM1                                          AS ALLOC_PRIORITY_NM   " & vbNewLine _
    '                                        & ",Z4.KBN_NM1                                          AS OFB_KB_NM           " & vbNewLine _
    '                                        & ",Z5.KBN_NM1                                          AS SPD_KB_NM           " & vbNewLine _
    '                                        & ",ZAI.SYS_DEL_FLG                                     AS SYS_DEL_FLG         " & vbNewLine _
    '                                        & ",ZAI.ALLOC_CAN_NB                                    AS ALLOC_CAN_NB_HOZON  " & vbNewLine _
    '                                        & ",ZAI.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT_HOZON  " & vbNewLine _
    '                                        & ",ZAI.ALCTD_NB                                        AS ALCTD_NB_HOZON      " & vbNewLine _
    '                                        & ",ZAI.ALCTD_QT                                        AS ALCTD_QT_HOZON      " & vbNewLine _
    '                                        & ",ZAI.ALLOC_CAN_NB                                    AS ALLOC_CAN_NB_GAMEN  " & vbNewLine _
    '                                        & ",ZAI.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT_GAMEN  " & vbNewLine _
    '                                        & ",ZAI.ALCTD_NB                                        AS ALCTD_NB_GAMEN      " & vbNewLine _
    '                                        & ",ZAI.ALCTD_QT                                        AS ALCTD_QT_GAMEN      " & vbNewLine _
    '                                        & ",'1'                                                 AS UP_KBN              " & vbNewLine _
    '                                        & ",'1'                                                 AS MATOME_FLG          " & vbNewLine _
    '                                        & ",CASE WHEN ZAI.SMPL_FLAG = '01'                                             " & vbNewLine _
    '                                        & "      THEN '99'                                                             " & vbNewLine _
    '                                        & "      ELSE ''                                                               " & vbNewLine _
    '                                        & " END                                                 AS ALCTD_KB_FLG        " & vbNewLine _
    '                                        & ",INKA_L.INKA_DATE                                    AS INKA_DATE           " & vbNewLine _
    '                                        & ",IDO.IDO_DATE                                        AS IDO_DATE            " & vbNewLine _
    '                                        & ",INKA_L.HOKAN_STR_DATE                               AS HOKAN_STR_DATE      " & vbNewLine
    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ZAI As String = " SELECT                                                                      " & vbNewLine _
                                            & " ZAI.NRS_BR_CD                                       AS NRS_BR_CD           " & vbNewLine _
                                            & ",ZAI.ZAI_REC_NO                                      AS ZAI_REC_NO          " & vbNewLine _
                                            & ",ZAI.WH_CD                                           AS WH_CD               " & vbNewLine _
                                            & ",ZAI.TOU_NO                                          AS TOU_NO              " & vbNewLine _
                                            & ",ZAI.SITU_NO                                         AS SITU_NO             " & vbNewLine _
                                            & ",ZAI.ZONE_CD                                         AS ZONE_CD             " & vbNewLine _
                                            & ",ZAI.LOCA                                            AS LOCA                " & vbNewLine _
                                            & ",ZAI.LOT_NO                                          AS LOT_NO              " & vbNewLine _
                                            & ",ZAI.CUST_CD_L                                       AS CUST_CD_L           " & vbNewLine _
                                            & ",ZAI.CUST_CD_M                                       AS CUST_CD_M           " & vbNewLine _
                                            & ",ZAI.GOODS_CD_NRS                                    AS GOODS_CD_NRS        " & vbNewLine _
                                            & ",ZAI.GOODS_KANRI_NO                                  AS GOODS_KANRI_NO      " & vbNewLine _
                                            & ",ZAI.INKA_NO_L                                       AS INKA_NO_L           " & vbNewLine _
                                            & ",ZAI.INKA_NO_M                                       AS INKA_NO_M           " & vbNewLine _
                                            & ",ZAI.INKA_NO_S                                       AS INKA_NO_S           " & vbNewLine _
                                            & ",ZAI.ALLOC_PRIORITY                                  AS ALLOC_PRIORITY      " & vbNewLine _
                                            & ",ZAI.RSV_NO                                          AS RSV_NO              " & vbNewLine _
                                            & ",ZAI.SERIAL_NO                                       AS SERIAL_NO           " & vbNewLine _
                                            & ",ZAI.HOKAN_YN                                        AS HOKAN_YN            " & vbNewLine _
                                            & ",ZAI.TAX_KB                                          AS TAX_KB              " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_1                                 AS GOODS_COND_KB_1     " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_2                                 AS GOODS_COND_KB_2     " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_3                                 AS GOODS_COND_KB_3     " & vbNewLine _
                                            & ",ZAI.OFB_KB                                          AS OFB_KB              " & vbNewLine _
                                            & ",ZAI.SPD_KB                                          AS SPD_KB              " & vbNewLine _
                                            & ",ZAI.REMARK_OUT                                      AS REMARK_OUT          " & vbNewLine _
                                            & ",ZAI.PORA_ZAI_NB                                     AS PORA_ZAI_NB         " & vbNewLine _
                                            & ",ZAI.ALCTD_NB                                        AS ALCTD_NB            " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_NB                                    AS ALLOC_CAN_NB        " & vbNewLine _
                                            & ",ZAI.IRIME                                           AS IRIME               " & vbNewLine _
                                            & ",ZAI.PORA_ZAI_QT                                     AS PORA_ZAI_QT         " & vbNewLine _
                                            & ",ZAI.ALCTD_QT                                        AS ALCTD_QT            " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT        " & vbNewLine _
                                            & ",ZAI.INKO_DATE                                       AS INKO_DATE           " & vbNewLine _
                                            & ",ZAI.INKO_PLAN_DATE                                  AS INKO_PLAN_DATE      " & vbNewLine _
                                            & ",ZAI.ZERO_FLAG                                       AS ZERO_FLAG           " & vbNewLine _
                                            & ",ZAI.LT_DATE                                         AS LT_DATE             " & vbNewLine _
                                            & ",ZAI.GOODS_CRT_DATE                                  AS GOODS_CRT_DATE      " & vbNewLine _
                                            & ",ZAI.DEST_CD_P                                       AS DEST_CD_P           " & vbNewLine _
                                            & ",ZAI.REMARK                                          AS REMARK              " & vbNewLine _
                                            & ",ZAI.SMPL_FLAG                                       AS SMPL_FLAG           " & vbNewLine _
                                            & ",ZAI.SYS_UPD_DATE                                    AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",ZAI.SYS_UPD_TIME                                    AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",Z1.KBN_NM1                                          AS GOODS_COND_NM_1     " & vbNewLine _
                                            & ",Z2.KBN_NM1                                          AS GOODS_COND_NM_2     " & vbNewLine _
                                            & ",CUSCON.JOTAI_NM                                     AS GOODS_COND_NM_3     " & vbNewLine _
                                            & ",Z3.KBN_NM1                                          AS ALLOC_PRIORITY_NM   " & vbNewLine _
                                            & ",Z4.KBN_NM1                                          AS OFB_KB_NM           " & vbNewLine _
                                            & ",Z5.KBN_NM1                                          AS SPD_KB_NM           " & vbNewLine _
                                            & ",ZAI.SYS_DEL_FLG                                     AS SYS_DEL_FLG         " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_NB                                    AS ALLOC_CAN_NB_HOZON  " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT_HOZON  " & vbNewLine _
                                            & ",ZAI.ALCTD_NB                                        AS ALCTD_NB_HOZON      " & vbNewLine _
                                            & ",ZAI.ALCTD_QT                                        AS ALCTD_QT_HOZON      " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_NB                                    AS ALLOC_CAN_NB_GAMEN  " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT_GAMEN  " & vbNewLine _
                                            & ",ZAI.ALCTD_NB                                        AS ALCTD_NB_GAMEN      " & vbNewLine _
                                            & ",ZAI.ALCTD_QT                                        AS ALCTD_QT_GAMEN      " & vbNewLine _
                                            & ",'1'                                                 AS UP_KBN              " & vbNewLine _
                                            & ",'1'                                                 AS MATOME_FLG          " & vbNewLine _
                                            & ",CASE WHEN ZAI.SMPL_FLAG = '01'                                             " & vbNewLine _
                                            & "      THEN '99'                                                             " & vbNewLine _
                                            & "      ELSE ''                                                               " & vbNewLine _
                                            & " END                                                 AS ALCTD_KB_FLG        " & vbNewLine _
                                            & ",INKA_L.INKA_DATE                                    AS INKA_DATE           " & vbNewLine _
                                            & ",IDO.IDO_DATE                                        AS IDO_DATE            " & vbNewLine _
                                            & ",INKA_L.HOKAN_STR_DATE                               AS HOKAN_STR_DATE      " & vbNewLine _
                                            & ",GOODSDETAILS.SET_NAIYO                              AS INKA_DATE_KANRI_KB  " & vbNewLine _
    'END YANAI 要望番号780

    'START YANAI 要望番号780
    '''' <summary>
    '''' FROM
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM_ZAI As String = "FROM                                                      " & vbNewLine _
    '                                     & "$LM_TRN$..D_ZAI_TRS  ZAI                                  " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_CUSTCOND    CUSCON                  " & vbNewLine _
    '                                     & "ON     ZAI.NRS_BR_CD = CUSCON.NRS_BR_CD                   " & vbNewLine _
    '                                     & "AND    ZAI.CUST_CD_L = CUSCON.CUST_CD_L                   " & vbNewLine _
    '                                     & "AND    ZAI.GOODS_COND_KB_3 = CUSCON.JOTAI_CD              " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
    '                                     & "ON     ZAI.GOODS_COND_KB_1 = Z1.KBN_CD                    " & vbNewLine _
    '                                     & "AND    Z1.KBN_GROUP_CD = 'S005'                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..Z_KBN    Z2                           " & vbNewLine _
    '                                     & "ON     ZAI.GOODS_COND_KB_2 = Z2.KBN_CD                    " & vbNewLine _
    '                                     & "AND    Z2.KBN_GROUP_CD = 'S006'                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..Z_KBN    Z3                           " & vbNewLine _
    '                                     & "ON     ZAI.ALLOC_PRIORITY = Z3.KBN_CD                     " & vbNewLine _
    '                                     & "AND    Z3.KBN_GROUP_CD = 'W001'                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..Z_KBN    Z4                           " & vbNewLine _
    '                                     & "ON     ZAI.OFB_KB = Z4.KBN_CD                             " & vbNewLine _
    '                                     & "AND    Z4.KBN_GROUP_CD = 'B002'                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..Z_KBN    Z5                           " & vbNewLine _
    '                                     & "ON     ZAI.SPD_KB = Z5.KBN_CD                             " & vbNewLine _
    '                                     & "AND    Z5.KBN_GROUP_CD = 'H003'                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_TRN$..B_INKA_L INKA_L                       " & vbNewLine _
    '                                     & "ON     INKA_L.NRS_BR_CD = ZAI.NRS_BR_CD                   " & vbNewLine _
    '                                     & "AND    INKA_L.INKA_NO_L = ZAI.INKA_NO_L                   " & vbNewLine _
    '                                     & "AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_TRN$..D_IDO_TRS IDO                         " & vbNewLine _
    '                                     & "ON     IDO.NRS_BR_CD = ZAI.NRS_BR_CD                      " & vbNewLine _
    '                                     & "AND    IDO.N_ZAI_REC_NO = ZAI.ZAI_REC_NO                  " & vbNewLine _
    '                                     & "AND    IDO.SYS_DEL_FLG = '0'                              " & vbNewLine
    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_ZAI As String = "FROM                                                      " & vbNewLine _
                                         & "$LM_TRN$..D_ZAI_TRS  ZAI                                  " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_CUSTCOND    CUSCON                  " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = CUSCON.NRS_BR_CD                   " & vbNewLine _
                                         & "AND    ZAI.CUST_CD_L = CUSCON.CUST_CD_L                   " & vbNewLine _
                                         & "AND    ZAI.GOODS_COND_KB_3 = CUSCON.JOTAI_CD              " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_1 = Z1.KBN_CD                    " & vbNewLine _
                                         & "AND    Z1.KBN_GROUP_CD = 'S005'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z2                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_2 = Z2.KBN_CD                    " & vbNewLine _
                                         & "AND    Z2.KBN_GROUP_CD = 'S006'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z3                           " & vbNewLine _
                                         & "ON     ZAI.ALLOC_PRIORITY = Z3.KBN_CD                     " & vbNewLine _
                                         & "AND    Z3.KBN_GROUP_CD = 'W001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z4                           " & vbNewLine _
                                         & "ON     ZAI.OFB_KB = Z4.KBN_CD                             " & vbNewLine _
                                         & "AND    Z4.KBN_GROUP_CD = 'B002'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z5                           " & vbNewLine _
                                         & "ON     ZAI.SPD_KB = Z5.KBN_CD                             " & vbNewLine _
                                         & "AND    Z5.KBN_GROUP_CD = 'H003'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..B_INKA_L INKA_L                       " & vbNewLine _
                                         & "ON     INKA_L.NRS_BR_CD = ZAI.NRS_BR_CD                   " & vbNewLine _
                                         & "AND    INKA_L.INKA_NO_L = ZAI.INKA_NO_L                   " & vbNewLine _
                                         & "AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..D_IDO_TRS IDO                         " & vbNewLine _
                                         & "ON     IDO.NRS_BR_CD = ZAI.NRS_BR_CD                      " & vbNewLine _
                                         & "AND    IDO.N_ZAI_REC_NO = ZAI.ZAI_REC_NO                  " & vbNewLine _
                                         & "AND    IDO.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GOODSDETAILS          " & vbNewLine _
                                         & "ON  GOODSDETAILS.NRS_BR_CD = ZAI.NRS_BR_CD                " & vbNewLine _
                                         & "AND GOODSDETAILS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS          " & vbNewLine _
                                         & "AND GOODSDETAILS.SUB_KB = '09'                            " & vbNewLine _
    'END YANAI 要望番号780

    'START YANAI 要望番号780
    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_ZAI1 As String = "FROM                                                     " & vbNewLine _
                                         & "$LM_TRN$..D_ZAI_TRS  ZAI                                  " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_CUSTCOND    CUSCON                  " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = CUSCON.NRS_BR_CD                   " & vbNewLine _
                                         & "AND    ZAI.CUST_CD_L = CUSCON.CUST_CD_L                   " & vbNewLine _
                                         & "AND    ZAI.GOODS_COND_KB_3 = CUSCON.JOTAI_CD              " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_1 = Z1.KBN_CD                    " & vbNewLine _
                                         & "AND    Z1.KBN_GROUP_CD = 'S005'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z2                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_2 = Z2.KBN_CD                    " & vbNewLine _
                                         & "AND    Z2.KBN_GROUP_CD = 'S006'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z3                           " & vbNewLine _
                                         & "ON     ZAI.ALLOC_PRIORITY = Z3.KBN_CD                     " & vbNewLine _
                                         & "AND    Z3.KBN_GROUP_CD = 'W001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z4                           " & vbNewLine _
                                         & "ON     ZAI.OFB_KB = Z4.KBN_CD                             " & vbNewLine _
                                         & "AND    Z4.KBN_GROUP_CD = 'B002'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z5                           " & vbNewLine _
                                         & "ON     ZAI.SPD_KB = Z5.KBN_CD                             " & vbNewLine _
                                         & "AND    Z5.KBN_GROUP_CD = 'H003'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..B_INKA_L INKA_L                       " & vbNewLine _
                                         & "ON     INKA_L.NRS_BR_CD = ZAI.NRS_BR_CD                   " & vbNewLine _
                                         & "AND    INKA_L.INKA_NO_L = ZAI.INKA_NO_L                   " & vbNewLine _
                                         & "AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..D_IDO_TRS IDO                         " & vbNewLine _
                                         & "ON     IDO.NRS_BR_CD = ZAI.NRS_BR_CD                      " & vbNewLine _
                                         & "AND    IDO.N_ZAI_REC_NO = ZAI.ZAI_REC_NO                  " & vbNewLine _
                                         & "AND    IDO.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GOODSDETAILS          " & vbNewLine _
                                         & "ON  GOODSDETAILS.NRS_BR_CD = ZAI.NRS_BR_CD                " & vbNewLine _
                                         & "AND GOODSDETAILS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS          " & vbNewLine _
                                         & "AND GOODSDETAILS.SUB_KB = '09'                            " & vbNewLine _
                                         & "AND GOODSDETAILS.SET_NAIYO = '01'                         " & vbNewLine
    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_ZAI2 As String = "FROM                                                     " & vbNewLine _
                                         & "$LM_TRN$..D_ZAI_TRS  ZAI                                  " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_CUSTCOND    CUSCON                  " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = CUSCON.NRS_BR_CD                   " & vbNewLine _
                                         & "AND    ZAI.CUST_CD_L = CUSCON.CUST_CD_L                   " & vbNewLine _
                                         & "AND    ZAI.GOODS_COND_KB_3 = CUSCON.JOTAI_CD              " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_1 = Z1.KBN_CD                    " & vbNewLine _
                                         & "AND    Z1.KBN_GROUP_CD = 'S005'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z2                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_2 = Z2.KBN_CD                    " & vbNewLine _
                                         & "AND    Z2.KBN_GROUP_CD = 'S006'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z3                           " & vbNewLine _
                                         & "ON     ZAI.ALLOC_PRIORITY = Z3.KBN_CD                     " & vbNewLine _
                                         & "AND    Z3.KBN_GROUP_CD = 'W001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z4                           " & vbNewLine _
                                         & "ON     ZAI.OFB_KB = Z4.KBN_CD                             " & vbNewLine _
                                         & "AND    Z4.KBN_GROUP_CD = 'B002'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z5                           " & vbNewLine _
                                         & "ON     ZAI.SPD_KB = Z5.KBN_CD                             " & vbNewLine _
                                         & "AND    Z5.KBN_GROUP_CD = 'H003'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..B_INKA_L INKA_L                       " & vbNewLine _
                                         & "ON     INKA_L.NRS_BR_CD = ZAI.NRS_BR_CD                   " & vbNewLine _
                                         & "AND    INKA_L.INKA_NO_L = ZAI.INKA_NO_L                   " & vbNewLine _
                                         & "AND    INKA_L.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..D_IDO_TRS IDO                         " & vbNewLine _
                                         & "ON     IDO.NRS_BR_CD = ZAI.NRS_BR_CD                      " & vbNewLine _
                                         & "AND    IDO.N_ZAI_REC_NO = ZAI.ZAI_REC_NO                  " & vbNewLine _
                                         & "AND    IDO.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GOODSDETAILS          " & vbNewLine _
                                         & "ON  GOODSDETAILS.NRS_BR_CD = ZAI.NRS_BR_CD                " & vbNewLine _
                                         & "AND GOODSDETAILS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS          " & vbNewLine _
                                         & "AND GOODSDETAILS.SUB_KB = '09'                            " & vbNewLine _
                                         & "AND GOODSDETAILS.SET_NAIYO <> '01'                        " & vbNewLine
    'END YANAI 要望番号780

    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_ZAI As String = "WHERE                                                   " & vbNewLine _
                                         & "    ZAI.NRS_BR_CD              = @NRS_BR_CD              " & vbNewLine _
                                         & "AND ZAI.SYS_DEL_FLG            = '0'                     " & vbNewLine _
                                         & "AND ZAI.ZAI_REC_NO             IN(                       " & vbNewLine

    'START YANAI 要望番号853 まとめ処理対応
    ''' <summary>
    ''' WHERE(まとめ処理用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_ZAI_MATOME As String = "WHERE                                            " & vbNewLine _
                                         & "    ZAI.NRS_BR_CD              = @NRS_BR_CD              " & vbNewLine _
                                         & "AND ZAI.GOODS_CD_NRS           = @GOODS_CD_NRS           " & vbNewLine _
                                         & "AND ZAI.LOT_NO                 = @LOT_NO                 " & vbNewLine _
                                         & "AND ZAI.IRIME                  = @IRIME                  " & vbNewLine _
                                         & "AND ZAI.TOU_NO                 = @TOU_NO                 " & vbNewLine _
                                         & "AND ZAI.SITU_NO                = @SITU_NO                " & vbNewLine _
                                         & "AND ZAI.ZONE_CD                = @ZONE_CD                " & vbNewLine _
                                         & "AND ZAI.LOCA                   = @LOCA                   " & vbNewLine _
                                         & "AND ZAI.GOODS_COND_KB_1        = @GOODS_COND_KB_1        " & vbNewLine _
                                         & "AND ZAI.GOODS_COND_KB_2        = @GOODS_COND_KB_2        " & vbNewLine _
                                         & "AND ZAI.REMARK_OUT             = @REMARK_OUT             " & vbNewLine _
                                         & "AND ZAI.REMARK                 = @REMARK                 " & vbNewLine _
                                         & "AND ZAI.LT_DATE                = @LT_DATE                " & vbNewLine _
                                         & "AND ZAI.OFB_KB                 = @OFB_KB                 " & vbNewLine _
                                         & "AND ZAI.SPD_KB                 = @SPD_KB                 " & vbNewLine _
                                         & "AND ZAI.RSV_NO                 = @RSV_NO                 " & vbNewLine _
                                         & "AND ZAI.SERIAL_NO              = @SERIAL_NO              " & vbNewLine _
                                         & "AND ZAI.GOODS_CRT_DATE         = @GOODS_CRT_DATE         " & vbNewLine _
                                         & "AND ZAI.ALLOC_PRIORITY         = @ALLOC_PRIORITY         " & vbNewLine _
                                         & "AND ZAI.SYS_DEL_FLG            = '0'                     " & vbNewLine _
                                         & "AND NOT (                                                " & vbNewLine _
                                         & "         ZAI.PORA_ZAI_NB       = 0                       " & vbNewLine _
                                         & "     AND ZAI.PORA_ZAI_QT       = 0                       " & vbNewLine _
                                         & "     AND ZAI.ALCTD_NB          = 0                       " & vbNewLine _
                                         & "     AND ZAI.ALCTD_QT          = 0                       " & vbNewLine _
                                         & "     AND ZAI.ALLOC_CAN_NB      = 0                       " & vbNewLine _
                                         & "     AND ZAI.ALLOC_CAN_QT      = 0                       " & vbNewLine _
                                         & "        )                                                " & vbNewLine

    ''' <summary>
    ''' WHERE(まとめ処理用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_INKODATE As String = "AND ZAI.INKO_DATE              = @INKO_DATE              " & vbNewLine

    ''' <summary>
    ''' WHERE(まとめ処理用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_INKOPLANDATE As String = "AND ZAI.INKO_PLAN_DATE         = @INKO_PLAN_DATE              " & vbNewLine

    'END YANAI 要望番号853 まとめ処理対応

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_ZAI As String = "ORDER BY                                             " & vbNewLine _
                                         & " ZAI.ZAI_REC_NO                                          " & vbNewLine

    '要望番号:1350 terakawa 2012.08.24 Start
    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GOODS_LOT_CHK As String = "SELECT                                     " & vbNewLine _
                                             & "COUNT(*) AS ZAI_CNT                         " & vbNewLine _
                                             & "FROM                                        " & vbNewLine _
                                             & "$LM_TRN$..D_ZAI_TRS AS ZAI                    " & vbNewLine _
                                             & "LEFT OUTER JOIN                             " & vbNewLine _
                                             & "$LM_MST$..M_GOODS AS MG                       " & vbNewLine _
                                             & "ON MG.NRS_BR_CD = ZAI.NRS_BR_CD             " & vbNewLine _
                                             & "AND MG.GOODS_CD_NRS = ZAI.GOODS_CD_NRS      " & vbNewLine _
                                             & "WHERE                                       " & vbNewLine

    ''' <summary>
    ''' NRS_BR_CD,CUST_CD
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GOODS_LOT_CHK_CUST_CD_L As String = "ZAI.NRS_BR_CD     = @NRS_BR_CD   " & vbNewLine _
                                                        & "AND ZAI.CUST_CD_L = @CUST_CD_L   " & vbNewLine

    Private Const SQL_GOODS_LOT_CHK_CUST_CD_L_DETAIL_PLUS As String = "ZAI.NRS_BR_CD     =    @DETAILS_NRS_BR_CD              " & vbNewLine _
                                                                    & "AND ZAI.CUST_CD_L IN ( @CUST_CD_L, @DETAILS_CUST_CD_L) " & vbNewLine

    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GOODS_LOT_CHK_AFTER As String = "AND ZAI.PORA_ZAI_NB > 0                  " & vbNewLine _
                                                 & "AND ZAI.TOU_NO = @TOU_NO                    " & vbNewLine _
                                                 & "AND ZAI.SITU_NO = @SITU_NO                  " & vbNewLine _
                                                 & "AND ZAI.ZONE_CD = @ZONE_CD                  " & vbNewLine _
                                                 & "AND ZAI.LOCA = @LOCA                        " & vbNewLine _
                                                 & "AND ((MG.GOODS_CD_CUST = @GOODS_CD_CUST AND LOT_NO <> @LOT_NO)  " & vbNewLine _
                                                 & "OR (MG.GOODS_CD_CUST <> @GOODS_CD_CUST AND LOT_NO = @LOT_NO))   " & vbNewLine _
                                                 & "AND ZAI.ZAI_REC_NO <> @ZAI_REC_NO           " & vbNewLine _
                                                 & "AND ZAI.SYS_DEL_FLG = '0'                   " & vbNewLine
    '要望番号:1350 terakawa 2012.08.24 End

    '2019/12/16 要望管理009513 add
    Private Const SQL_IRIME_CHECK As String = "SELECT                                " & vbNewLine _
                                            & "  COUNT(*) AS DIFF_CNT                " & vbNewLine _
                                            & "FROM                                  " & vbNewLine _
                                            & "  $LM_MST$..M_GOODS MG                " & vbNewLine _
                                            & "WHERE                                 " & vbNewLine _
                                            & "  MG.NRS_BR_CD = @NRS_BR_CD           " & vbNewLine _
                                            & "  AND MG.GOODS_CD_NRS = @GOODS_CD_NRS " & vbNewLine _
                                            & "  AND MG.STD_IRIME_NB <> @IRIME       " & vbNewLine _
                                            & "  AND MG.SYS_DEL_FLG = '0'            " & vbNewLine

#End Region

#Region "MAX_NO"
    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAX As String = " SELECT                                                                      " & vbNewLine _
                                            & " OUT_M.OUTKA_NO_M                                    AS OUTKA_NO_M          " & vbNewLine _
                                            & ",(SELECT ISNULL(MAX(OUT_S2.OUTKA_NO_S),0) AS MAX_S                          " & vbNewLine _
                                            & "  FROM $LM_TRN$..C_OUTKA_S  OUT_S2                                          " & vbNewLine _
                                            & " WHERE                                                                      " & vbNewLine _
                                            & "  OUT_S2.NRS_BR_CD            = @NRS_BR_CD                                  " & vbNewLine _
                                            & "AND OUT_S2.OUTKA_NO_L         = @OUTKA_NO_L                                 " & vbNewLine _
                                            & "AND OUT_S2.OUTKA_NO_M         = OUT_M.OUTKA_NO_M                            " & vbNewLine _
                                            & ")                  AS MAX_OUTKA_NO_S                                        " & vbNewLine

    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_MAX As String = "FROM                                                      " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_M  OUT_M                                " & vbNewLine _
                                         & "LEFT JOIN                                                 " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_S  OUT_S                                " & vbNewLine _
                                         & "ON                                                        " & vbNewLine _
                                         & "  OUT_S.NRS_BR_CD             = @NRS_BR_CD                " & vbNewLine _
                                         & "AND OUT_S.OUTKA_NO_L          = @OUTKA_NO_L               " & vbNewLine


    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_MAX As String = "WHERE                                                    " & vbNewLine _
                                         & "    OUT_M.NRS_BR_CD              = @NRS_BR_CD             " & vbNewLine _
                                         & "AND OUT_M.OUTKA_NO_L             = @OUTKA_NO_L            " & vbNewLine

    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_MAX As String = "GROUP BY                                              " & vbNewLine _
                                         & " OUT_M.OUTKA_NO_M                                         " & vbNewLine

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_MAX As String = "ORDER BY                                              " & vbNewLine _
                                         & " OUT_M.OUTKA_NO_M                                         " & vbNewLine

#End Region

#Region "IDO_TRS 移動先データが出荷されているかチェック用"
    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_IDO_TRS As String = " SELECT                                             " & vbNewLine _
                                            & " IDO.N_ZAI_REC_NO      AS       ZAI_REC_NO             " & vbNewLine

    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_IDO_TRS As String = "FROM                                                  " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_S  OUTKAS                               " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..D_IDO_TRS    IDO                      " & vbNewLine _
                                         & "ON     IDO.NRS_BR_CD = OUTKAS.NRS_BR_CD                   " & vbNewLine _
                                         & "AND    IDO.REC_NO = OUTKAS.REC_NO                         " & vbNewLine _
                                         & "AND    IDO.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                         & "AND    0 = (                                              " & vbNewLine _
                                         & "       SELECT COUNT(OUTKAS2.NRS_BR_CD)                    " & vbNewLine _
                                         & "       FROM   $LM_TRN$..C_OUTKA_S OUTKAS2                 " & vbNewLine _
                                         & "       WHERE  OUTKAS2.NRS_BR_CD = IDO.NRS_BR_CD           " & vbNewLine _
                                         & "       AND    OUTKAS2.ZAI_REC_NO = IDO.N_ZAI_REC_NO       " & vbNewLine _
                                         & "       AND    OUTKAS2.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                         & "       )                                                  " & vbNewLine

    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_IDO_TRS As String = "WHERE                                                " & vbNewLine _
                                         & "    OUTKAS.NRS_BR_CD           = @NRS_BR_CD               " & vbNewLine _
                                         & "AND OUTKAS.OUTKA_NO_L          = @OUTKA_NO_L              " & vbNewLine _
                                         & "AND OUTKAS.OUTKA_NO_M          = @OUTKA_NO_M              " & vbNewLine _
                                         & "AND OUTKAS.OUTKA_NO_S          = @OUTKA_NO_S              " & vbNewLine _
                                         & "AND OUTKAS.SYS_DEL_FLG         = '0'                      " & vbNewLine


#End Region

#Region "M_CUST"

    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CUST As String = " SELECT                                             " & vbNewLine _
                                            & " CUST.HOKAN_NIYAKU_CALCULATION AS HOKAN_NIYAKU_CALCULATION " & vbNewLine _
                                            & ",CUST.UNTIN_CALCULATION_KB     AS UNTIN_CALCULATION_KB     " & vbNewLine _
                                            & ",CUST.HOKAN_SEIQTO_CD          AS HOKAN_SEIQTO_CD          " & vbNewLine _
                                            & ",CUST.NIYAKU_SEIQTO_CD         AS NIYAKU_SEIQTO_CD         " & vbNewLine _
                                            & ",CUST.UNCHIN_SEIQTO_CD         AS UNCHIN_SEIQTO_CD         " & vbNewLine _
                                            & ",CUST.SAGYO_SEIQTO_CD          AS SAGYO_SEIQTO_CD          " & vbNewLine

    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_CUST As String = "FROM                                                     " & vbNewLine _
                                         & "$LM_MST$..M_CUST CUST                                     " & vbNewLine

    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_CUST As String = "WHERE                                                " & vbNewLine _
                                         & "    CUST.NRS_BR_CD             = @NRS_BR_CD               " & vbNewLine _
                                         & "AND CUST.CUST_CD_L             = @CUST_CD_L               " & vbNewLine _
                                         & "AND CUST.CUST_CD_M             = @CUST_CD_M               " & vbNewLine _
                                         & "AND CUST.CUST_CD_S             = @CUST_CD_S               " & vbNewLine _
                                         & "AND CUST.CUST_CD_SS            = @CUST_CD_SS              " & vbNewLine


#End Region

    '要望番号:1350 terakawa 2012.08.24 Start
#Region "M_CUST_DETAILS"

    Private Const SQL_SELECT_CUST_DETAILS As String = "SELECT                                    " & vbNewLine _
                                            & "  CUST_D.SET_NAIYO  AS SET_NAIYO                  " & vbNewLine _
                                            & " ,CUST_D.SET_NAIYO_2  AS SET_NAIYO_2              " & vbNewLine _
                                            & "FROM       $LM_MST$..M_CUST_DETAILS CUST_D        " & vbNewLine _
                                            & "WHERE  CUST_D.NRS_BR_CD   = @NRS_BR_CD            " & vbNewLine _
                                            & "  AND  CUST_D.CUST_CD   = @CUST_CD_L              " & vbNewLine _
                                            & "  AND  CUST_D.SUB_KB    = '41'                    " & vbNewLine _
                                            & "  AND  CUST_D.SYS_DEL_FLG = '0'                   " & vbNewLine


    '2019/12/16 要望管理009513 add
    Private Const SQL_SELECT_CUST_DETAILS_9P As String = "SELECT                                 " & vbNewLine _
                                            & "  CUST_D.SET_NAIYO  AS SET_NAIYO                  " & vbNewLine _
                                            & " ,CUST_D.SET_NAIYO_2  AS SET_NAIYO_2              " & vbNewLine _
                                            & "FROM       $LM_MST$..M_CUST_DETAILS CUST_D        " & vbNewLine _
                                            & "WHERE  CUST_D.NRS_BR_CD   = @NRS_BR_CD            " & vbNewLine _
                                            & "  AND  CUST_D.CUST_CD   = @CUST_CD_L              " & vbNewLine _
                                            & "  AND  CUST_D.SUB_KB    = '9P'                    " & vbNewLine _
                                            & "  AND  CUST_D.SYS_DEL_FLG = '0'                   " & vbNewLine


#End Region
    '要望番号:1350 terakawa 2012.08.24 End


#Region "UNCHIN"

    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNCHIN As String = " SELECT                                               " & vbNewLine _
                                              & " UNCHIN.SEIQTO_CD             AS SEIQTO_CD            " & vbNewLine _
                                              & ",UNCHIN.UNTIN_CALCULATION_KB  AS UNTIN_CALCULATION_KB " & vbNewLine _
                                              & ",UNCHIN.SEIQ_TARIFF_BUNRUI_KB AS SEIQ_TARIFF_BUNRUI_KB" & vbNewLine _
                                              & "   FROM $LM_TRN$..F_UNCHIN_TRS UNCHIN                 " & vbNewLine _
                                              & "  WHERE UNCHIN.UNSO_NO_L   = @UNSO_NO_L               " & vbNewLine _
                                              & "    AND UNCHIN.SYS_DEL_FLG = '0'                      " & vbNewLine


#End Region

#Region "EDI_OUTKA_L"

    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_EDI_L As String = " SELECT                                                                    " & vbNewLine _
                                            & " EDI_L.DEL_KB                                    AS DEL_KB          " & vbNewLine _
                                            & ",EDI_L.NRS_BR_CD                                    AS NRS_BR_CD          " & vbNewLine _
                                            & ",EDI_L.EDI_CTL_NO                                    AS EDI_CTL_NO          " & vbNewLine _
                                            & ",EDI_L.OUTKA_CTL_NO                                    AS OUTKA_CTL_NO          " & vbNewLine _
                                            & ",EDI_L.OUTKA_KB                                    AS OUTKA_KB          " & vbNewLine _
                                            & ",EDI_L.SYUBETU_KB                                    AS SYUBETU_KB          " & vbNewLine _
                                            & ",EDI_L.NAIGAI_KB                                    AS NAIGAI_KB          " & vbNewLine _
                                            & ",EDI_L.OUTKA_STATE_KB                                    AS OUTKA_STATE_KB          " & vbNewLine _
                                            & ",EDI_L.OUTKAHOKOKU_YN                                    AS OUTKAHOKOKU_YN          " & vbNewLine _
                                            & ",EDI_L.PICK_KB                                    AS PICK_KB          " & vbNewLine _
                                            & ",EDI_L.NRS_BR_NM                                    AS NRS_BR_NM          " & vbNewLine _
                                            & ",EDI_L.WH_CD                                    AS WH_CD          " & vbNewLine _
                                            & ",EDI_L.WH_NM                                    AS WH_NM          " & vbNewLine _
                                            & ",EDI_L.OUTKA_PLAN_DATE                                    AS OUTKA_PLAN_DATE          " & vbNewLine _
                                            & ",EDI_L.OUTKO_DATE                                    AS OUTKO_DATE          " & vbNewLine _
                                            & ",EDI_L.ARR_PLAN_DATE                                    AS ARR_PLAN_DATE          " & vbNewLine _
                                            & ",EDI_L.ARR_PLAN_TIME                                    AS ARR_PLAN_TIME          " & vbNewLine _
                                            & ",EDI_L.HOKOKU_DATE                                    AS HOKOKU_DATE          " & vbNewLine _
                                            & ",EDI_L.TOUKI_HOKAN_YN                                    AS TOUKI_HOKAN_YN          " & vbNewLine _
                                            & ",EDI_L.CUST_CD_L                                    AS CUST_CD_L          " & vbNewLine _
                                            & ",EDI_L.CUST_CD_M                                    AS CUST_CD_M          " & vbNewLine _
                                            & ",EDI_L.CUST_NM_L                                    AS CUST_NM_L          " & vbNewLine _
                                            & ",EDI_L.CUST_NM_M                                    AS CUST_NM_M          " & vbNewLine _
                                            & ",EDI_L.SHIP_CD_L                                    AS SHIP_CD_L          " & vbNewLine _
                                            & ",EDI_L.SHIP_CD_M                                    AS SHIP_CD_M          " & vbNewLine _
                                            & ",EDI_L.SHIP_NM_L                                    AS SHIP_NM_L          " & vbNewLine _
                                            & ",EDI_L.SHIP_NM_M                                    AS SHIP_NM_M          " & vbNewLine _
                                            & ",EDI_L.EDI_DEST_CD                                    AS EDI_DEST_CD          " & vbNewLine _
                                            & ",EDI_L.DEST_CD                                    AS DEST_CD          " & vbNewLine _
                                            & ",EDI_L.DEST_NM                                    AS DEST_NM          " & vbNewLine _
                                            & ",EDI_L.DEST_ZIP                                    AS DEST_ZIP          " & vbNewLine _
                                            & ",EDI_L.DEST_AD_1                                    AS DEST_AD_1          " & vbNewLine _
                                            & ",EDI_L.DEST_AD_2                                    AS DEST_AD_2          " & vbNewLine _
                                            & ",EDI_L.DEST_AD_3                                    AS DEST_AD_3          " & vbNewLine _
                                            & ",EDI_L.DEST_AD_4                                    AS DEST_AD_4          " & vbNewLine _
                                            & ",EDI_L.DEST_AD_5                                    AS DEST_AD_5          " & vbNewLine _
                                            & ",EDI_L.DEST_TEL                                    AS DEST_TEL          " & vbNewLine _
                                            & ",EDI_L.DEST_FAX                                    AS DEST_FAX          " & vbNewLine _
                                            & ",EDI_L.DEST_MAIL                                    AS DEST_MAIL          " & vbNewLine _
                                            & ",EDI_L.DEST_JIS_CD                                    AS DEST_JIS_CD          " & vbNewLine _
                                            & ",EDI_L.SP_NHS_KB                                    AS SP_NHS_KB          " & vbNewLine _
                                            & ",EDI_L.COA_YN                                    AS COA_YN          " & vbNewLine _
                                            & ",EDI_L.CUST_ORD_NO                                    AS CUST_ORD_NO          " & vbNewLine _
                                            & ",EDI_L.BUYER_ORD_NO                                    AS BUYER_ORD_NO          " & vbNewLine _
                                            & ",EDI_L.UNSO_MOTO_KB                                    AS UNSO_MOTO_KB          " & vbNewLine _
                                            & ",EDI_L.UNSO_TEHAI_KB                                    AS UNSO_TEHAI_KB          " & vbNewLine _
                                            & ",EDI_L.SYARYO_KB                                    AS SYARYO_KB          " & vbNewLine _
                                            & ",EDI_L.BIN_KB                                    AS BIN_KB          " & vbNewLine _
                                            & ",EDI_L.UNSO_CD                                    AS UNSO_CD          " & vbNewLine _
                                            & ",EDI_L.UNSO_NM                                    AS UNSO_NM          " & vbNewLine _
                                            & ",EDI_L.UNSO_BR_CD                                    AS UNSO_BR_CD          " & vbNewLine _
                                            & ",EDI_L.UNSO_BR_NM                                    AS UNSO_BR_NM          " & vbNewLine _
                                            & ",EDI_L.UNCHIN_TARIFF_CD                                    AS UNCHIN_TARIFF_CD          " & vbNewLine _
                                            & ",EDI_L.EXTC_TARIFF_CD                                    AS EXTC_TARIFF_CD          " & vbNewLine _
                                            & ",EDI_L.REMARK                                    AS REMARK          " & vbNewLine _
                                            & ",EDI_L.UNSO_ATT                                    AS UNSO_ATT          " & vbNewLine _
                                            & ",EDI_L.DENP_YN                                    AS DENP_YN          " & vbNewLine _
                                            & ",EDI_L.PC_KB                                    AS PC_KB          " & vbNewLine _
                                            & ",EDI_L.UNCHIN_YN                                    AS UNCHIN_YN          " & vbNewLine _
                                            & ",EDI_L.NIYAKU_YN                                    AS NIYAKU_YN          " & vbNewLine _
                                            & ",EDI_L.OUT_FLAG                                    AS OUT_FLAG          " & vbNewLine _
                                            & ",EDI_L.AKAKURO_KB                                    AS AKAKURO_KB          " & vbNewLine _
                                            & ",EDI_L.JISSEKI_FLAG                                    AS JISSEKI_FLAG          " & vbNewLine _
                                            & ",EDI_L.JISSEKI_USER                                    AS JISSEKI_USER          " & vbNewLine _
                                            & ",EDI_L.JISSEKI_DATE                                    AS JISSEKI_DATE          " & vbNewLine _
                                            & ",EDI_L.JISSEKI_TIME                                    AS JISSEKI_TIME          " & vbNewLine _
                                            & ",EDI_L.FREE_N01                                    AS FREE_N01          " & vbNewLine _
                                            & ",EDI_L.FREE_N02                                    AS FREE_N02          " & vbNewLine _
                                            & ",EDI_L.FREE_N03                                    AS FREE_N03          " & vbNewLine _
                                            & ",EDI_L.FREE_N04                                    AS FREE_N04          " & vbNewLine _
                                            & ",EDI_L.FREE_N05                                    AS FREE_N05          " & vbNewLine _
                                            & ",EDI_L.FREE_N06                                    AS FREE_N06          " & vbNewLine _
                                            & ",EDI_L.FREE_N07                                    AS FREE_N07          " & vbNewLine _
                                            & ",EDI_L.FREE_N08                                    AS FREE_N08          " & vbNewLine _
                                            & ",EDI_L.FREE_N09                                    AS FREE_N09          " & vbNewLine _
                                            & ",EDI_L.FREE_N10                                    AS FREE_N10          " & vbNewLine _
                                            & ",EDI_L.FREE_C01                                    AS FREE_C01          " & vbNewLine _
                                            & ",EDI_L.FREE_C02                                    AS FREE_C02          " & vbNewLine _
                                            & ",EDI_L.FREE_C03                                    AS FREE_C03          " & vbNewLine _
                                            & ",EDI_L.FREE_C04                                    AS FREE_C04          " & vbNewLine _
                                            & ",EDI_L.FREE_C05                                    AS FREE_C05          " & vbNewLine _
                                            & ",EDI_L.FREE_C06                                    AS FREE_C06          " & vbNewLine _
                                            & ",EDI_L.FREE_C07                                    AS FREE_C07          " & vbNewLine _
                                            & ",EDI_L.FREE_C08                                    AS FREE_C08          " & vbNewLine _
                                            & ",EDI_L.FREE_C09                                    AS FREE_C09          " & vbNewLine _
                                            & ",EDI_L.FREE_C10                                    AS FREE_C10          " & vbNewLine _
                                            & ",EDI_L.FREE_C11                                    AS FREE_C11          " & vbNewLine _
                                            & ",EDI_L.FREE_C12                                    AS FREE_C12          " & vbNewLine _
                                            & ",EDI_L.FREE_C13                                    AS FREE_C13          " & vbNewLine _
                                            & ",EDI_L.FREE_C14                                    AS FREE_C14          " & vbNewLine _
                                            & ",EDI_L.FREE_C15                                    AS FREE_C15          " & vbNewLine _
                                            & ",EDI_L.FREE_C16                                    AS FREE_C16          " & vbNewLine _
                                            & ",EDI_L.FREE_C17                                    AS FREE_C17          " & vbNewLine _
                                            & ",EDI_L.FREE_C18                                    AS FREE_C18          " & vbNewLine _
                                            & ",EDI_L.FREE_C19                                    AS FREE_C19          " & vbNewLine _
                                            & ",EDI_L.FREE_C20                                    AS FREE_C20          " & vbNewLine _
                                            & ",EDI_L.FREE_C21                                    AS FREE_C21          " & vbNewLine _
                                            & ",EDI_L.FREE_C22                                    AS FREE_C22          " & vbNewLine _
                                            & ",EDI_L.FREE_C23                                    AS FREE_C23          " & vbNewLine _
                                            & ",EDI_L.FREE_C24                                    AS FREE_C24          " & vbNewLine _
                                            & ",EDI_L.FREE_C25                                    AS FREE_C25          " & vbNewLine _
                                            & ",EDI_L.FREE_C26                                    AS FREE_C26          " & vbNewLine _
                                            & ",EDI_L.FREE_C27                                    AS FREE_C27          " & vbNewLine _
                                            & ",EDI_L.FREE_C28                                    AS FREE_C28          " & vbNewLine _
                                            & ",EDI_L.FREE_C29                                    AS FREE_C29          " & vbNewLine _
                                            & ",EDI_L.FREE_C30                                    AS FREE_C30          " & vbNewLine _
                                            & ",EDI_L.CRT_USER                                    AS CRT_USER          " & vbNewLine _
                                            & ",EDI_L.CRT_DATE                                    AS CRT_DATE          " & vbNewLine _
                                            & ",EDI_L.CRT_TIME                                    AS CRT_TIME          " & vbNewLine _
                                            & ",EDI_L.UPD_USER                                    AS UPD_USER          " & vbNewLine _
                                            & ",EDI_L.UPD_DATE                                    AS UPD_DATE          " & vbNewLine _
                                            & ",EDI_L.UPD_TIME                                    AS UPD_TIME          " & vbNewLine _
                                            & ",EDI_L.SCM_CTL_NO_L                                    AS SCM_CTL_NO_L          " & vbNewLine _
                                            & ",EDI_L.EDIT_FLAG                                    AS EDIT_FLAG          " & vbNewLine _
                                            & ",EDI_L.MATCHING_FLAG                                    AS MATCHING_FLAG          " & vbNewLine

    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_EDI_L As String = "FROM                                                    " & vbNewLine _
                                         & "$LM_TRN$..H_OUTKAEDI_L  EDI_L                             " & vbNewLine


    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_EDI_L As String = "WHERE                                                  " & vbNewLine _
                                         & "    EDI_L.NRS_BR_CD              = @NRS_BR_CD             " & vbNewLine _
                                         & "AND EDI_L.OUTKA_CTL_NO           = @OUTKA_NO_L            " & vbNewLine _
                                         & "AND EDI_L.SYS_DEL_FLG            = '0'                    " & vbNewLine

#End Region

    '要望番号:612 nakamura 2012.11.26 Start
#Region "INKA 振替一括削除用（入荷（大）、入荷（中）、入荷（小）、在庫）"
    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_SELECT_IN_L As String = " SELECT                                                                   " & vbNewLine _
                                            & " IN_L.NRS_BR_CD                  AS NRS_BR_CD                             " & vbNewLine _
                                            & ",IN_L.INKA_NO_L                  AS INKA_NO_L                             " & vbNewLine _
                                            & ",IN_L.FURI_NO                    AS FURI_NO                               " & vbNewLine _
                                            & ",IN_M.INKA_NO_M                  AS INKA_NO_M                             " & vbNewLine _
                                            & ",IN_S.INKA_NO_S                  AS INKA_NO_S                             " & vbNewLine _
                                            & ",IN_S.ZAI_REC_NO                 AS ZAI_REC_NO                            " & vbNewLine _
                                            & ",ZAI.SYS_UPD_DATE                AS SYS_UPD_DATE_ZAI                      " & vbNewLine _
                                            & ",ZAI.SYS_UPD_TIME                AS SYS_UPD_TIME_ZAI                      " & vbNewLine _
                                            & ",CASE WHEN ISNULL(ZAI.ALLOC_CAN_NB , 0) <> SUM(ISNULL(IN_S.KONSU , 0)     " & vbNewLine _
                                            & "                             * ISNULL(GOOD.PKG_NB , 0)                    " & vbNewLine _
                                            & "                             + ISNULL(IN_S.HASU , 0))                     " & vbNewLine _
                                            & "        OR ISNULL(ZAI.ALLOC_CAN_QT,0) <> SUM((ISNULL(IN_S.KONSU , 0)      " & vbNewLine _
                                            & "                             * ISNULL(GOOD.PKG_NB , 0)                    " & vbNewLine _
                                            & "                             + ISNULL(IN_S.HASU , 0))                     " & vbNewLine _
                                            & "                             * ISNULL(IN_S.IRIME , 0))                    " & vbNewLine _
                                            & "      THEN '済'                                                           " & vbNewLine _
                                            & "      ELSE '未'                                                           " & vbNewLine _
                                            & " END                                   AS      HIKIATE                    " & vbNewLine _
                                            & ",(SELECT                                                                  " & vbNewLine _
                                            & "        COUNT(REC_NO)       AS REC_CNT                                    " & vbNewLine _
                                            & "  FROM  $LM_TRN$..D_IDO_TRS    IDO                                               " & vbNewLine _
                                            & "  WHERE NRS_BR_CD    = IN_S.NRS_BR_CD                                     " & vbNewLine _
                                            & "     AND (O_ZAI_REC_NO = IN_S.ZAI_REC_NO                                  " & vbNewLine _
                                            & "     OR N_ZAI_REC_NO = IN_S.ZAI_REC_NO)                                   " & vbNewLine _
                                            & "    AND SYS_DEL_FLG  = '0'                                                " & vbNewLine _
                                            & " )                                     AS ZAI_REC_CNT                     " & vbNewLine


    ''' <summary>
    ''' FROM
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_FROM_IN_L As String = "FROM                                                  " & vbNewLine _
                                     & "$LM_TRN$..B_INKA_L  IN_L                                   " & vbNewLine _
                                     & "LEFT  JOIN                                                 " & vbNewLine _
                                     & " $LM_TRN$..B_INKA_M    IN_M                                " & vbNewLine _
                                     & " ON     IN_L.NRS_BR_CD = IN_M.NRS_BR_CD                    " & vbNewLine _
                                     & " AND    IN_L.INKA_NO_L = IN_M.INKA_NO_L                    " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..B_INKA_S    IN_S                       " & vbNewLine _
                                     & " ON     IN_M.NRS_BR_CD = IN_S.NRS_BR_CD                    " & vbNewLine _
                                     & " AND    IN_M.INKA_NO_L = IN_S.INKA_NO_L                    " & vbNewLine _
                                     & " AND    IN_M.INKA_NO_M = IN_S.INKA_NO_M                    " & vbNewLine _
                                     & " AND    IN_S.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                     & "LEFT JOIN                                                  " & vbNewLine _
                                     & " (SELECT                                                   " & vbNewLine _
                                     & " ZAI.NRS_BR_CD          AS NRS_BR_CD                       " & vbNewLine _
                                     & ",ZAI.INKA_NO_L          AS INKA_NO_L                       " & vbNewLine _
                                     & ",ZAI.INKA_NO_M          AS INKA_NO_M                       " & vbNewLine _
                                     & ",ZAI.INKA_NO_S          AS INKA_NO_S                       " & vbNewLine _
                                     & ",ZAI.GOODS_CD_NRS       AS GOODS_CD_NRS                    " & vbNewLine _
                                     & ",ZAI.SYS_DEL_FLG        AS SYS_DEL_FLG                     " & vbNewLine _
                                     & ",SUM(ZAI.ALLOC_CAN_NB)  AS ALLOC_CAN_NB                    " & vbNewLine _
                                     & ",SUM(ZAI.ALLOC_CAN_QT)  AS ALLOC_CAN_QT                    " & vbNewLine _
                                     & ",ZAI.PORA_ZAI_NB        AS ZAI_REC_CNT                     " & vbNewLine _
                                     & ",ZAI.SYS_UPD_DATE       AS SYS_UPD_DATE                    " & vbNewLine _
                                     & ",ZAI.SYS_UPD_TIME       AS SYS_UPD_TIME                    " & vbNewLine _
                                     & "FROM                                                       " & vbNewLine _
                                     & "$LM_TRN$..D_ZAI_TRS          ZAI                           " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..B_INKA_L  IN_L                         " & vbNewLine _
                                     & "     ON IN_L.NRS_BR_CD  = ZAI.NRS_BR_CD                    " & vbNewLine _
                                     & "    AND IN_L.INKA_NO_L  = ZAI.INKA_NO_L                    " & vbNewLine _
                                     & "WHERE ZAI.NRS_BR_CD                   = @NRS_BR_CD         " & vbNewLine _
                                     & " AND   ZAI.SYS_DEL_FLG                = '0'                " & vbNewLine _
                                     & "GROUP BY                                                   " & vbNewLine _
                                     & "          ZAI.NRS_BR_CD                                    " & vbNewLine _
                                     & "         ,ZAI.INKA_NO_L                                    " & vbNewLine _
                                     & "         ,ZAI.INKA_NO_M                                    " & vbNewLine _
                                     & "         ,ZAI.INKA_NO_S                                    " & vbNewLine _
                                     & "         ,ZAI.GOODS_CD_NRS                                 " & vbNewLine _
                                     & "         ,ZAI.SYS_DEL_FLG                                  " & vbNewLine _
                                     & "         ,ZAI.PORA_ZAI_NB                                  " & vbNewLine _
                                     & "         ,ZAI.SYS_UPD_DATE                                 " & vbNewLine _
                                     & "         ,ZAI.SYS_UPD_TIME                                 " & vbNewLine _
                                     & ") ZAI                                                      " & vbNewLine _
                                     & " ON    IN_M.NRS_BR_CD                  = ZAI.NRS_BR_CD     " & vbNewLine _
                                     & " AND   IN_M.INKA_NO_L                  = ZAI.INKA_NO_L     " & vbNewLine _
                                     & " AND   IN_M.INKA_NO_M                  = ZAI.INKA_NO_M     " & vbNewLine _
                                     & " AND   IN_S.INKA_NO_S                  = ZAI.INKA_NO_S     " & vbNewLine _
                                     & " AND   IN_M.GOODS_CD_NRS               = ZAI.GOODS_CD_NRS  " & vbNewLine _
                                     & " AND   ZAI.SYS_DEL_FLG                = '0'                " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_GOODS    GOOD                       " & vbNewLine _
                                     & "ON     IN_M.NRS_BR_CD = GOOD.NRS_BR_CD                     " & vbNewLine _
                                     & "AND    IN_M.GOODS_CD_NRS = GOOD.GOODS_CD_NRS               " & vbNewLine _
                                     & "AND   GOOD.SYS_DEL_FLG = '0'                               " & vbNewLine


    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_IN_L As String = "WHERE                                                " & vbNewLine _
                                     & "    IN_L.NRS_BR_CD               = @NRS_BR_CD             " & vbNewLine _
                                     & "AND IN_L.FURI_NO                 = @FURI_NO               " & vbNewLine _
                                     & "AND IN_L.SYS_DEL_FLG             = '0'                    " & vbNewLine _
                                     & "GROUP BY                                                   " & vbNewLine _
                                     & " IN_L.NRS_BR_CD                                            " & vbNewLine _
                                     & ",IN_L.INKA_NO_L                                            " & vbNewLine _
                                     & ",IN_L.FURI_NO                                              " & vbNewLine _
                                     & ",IN_M.INKA_NO_M                                            " & vbNewLine _
                                     & ",IN_S.NRS_BR_CD                                            " & vbNewLine _
                                     & ",IN_S.INKA_NO_S                                            " & vbNewLine _
                                     & ",IN_S.ZAI_REC_NO                                           " & vbNewLine _
                                     & ",ZAI.ZAI_REC_CNT                                           " & vbNewLine _
                                     & ",ZAI.SYS_UPD_DATE                                          " & vbNewLine _
                                     & ",ZAI.SYS_UPD_TIME                                          " & vbNewLine _
                                     & ",ZAI.ALLOC_CAN_NB                                          " & vbNewLine _
                                     & ",ZAI.ALLOC_CAN_QT                                          " & vbNewLine

    '要望番号:612 nakamura 2012.11.26 End
#End Region

    '2014/01/22 輸出情報追加 START
#Region "EXPORT_L"

    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_EXPORT_L As String = "  SELECT                                                                 " & vbNewLine _
                                                & "  EXPORT.NRS_BR_CD                              AS NRS_BR_CD             " & vbNewLine _
                                                & " ,EXPORT.OUTKA_NO_L                             AS OUTKA_NO_L            " & vbNewLine _
                                                & " ,EXPORT.SHIP_NM                                AS SHIP_NM               " & vbNewLine _
                                                & " ,EXPORT.DESTINATION                            AS DESTINATION           " & vbNewLine _
                                                & " ,EXPORT.BOOKING_NO                             AS BOOKING_NO            " & vbNewLine _
                                                & " ,EXPORT.VOYAGE_NO                              AS VOYAGE_NO             " & vbNewLine _
                                                & " ,EXPORT.SHIPPER_CD                             AS SHIPPER_CD            " & vbNewLine _
                                                & " ,DEST.DEST_NM                                  AS SHIPPER_NM            " & vbNewLine _
                                                & " ,EXPORT.CONT_LOADING_DATE                      AS CONT_LOADING_DATE     " & vbNewLine _
                                                & " ,EXPORT.STORAGE_TEST_DATE                      AS STORAGE_TEST_DATE     " & vbNewLine _
                                                & " ,EXPORT.STORAGE_TEST_TIME                      AS STORAGE_TEST_TIME     " & vbNewLine _
                                                & " ,EXPORT.DEPARTURE_DATE                         AS DEPARTURE_DATE        " & vbNewLine _
                                                & " ,EXPORT.CONTAINER_NO                           AS CONTAINER_NO          " & vbNewLine _
                                                & " ,EXPORT.CONTAINER_NM                           AS CONTAINER_NM          " & vbNewLine _
                                                & " ,EXPORT.CONTAINER_SIZE                         AS CONTAINER_SIZE        " & vbNewLine _
                                                & " ,EXPORT.SYS_DEL_FLG                            AS SYS_DEL_FLG           " & vbNewLine _
                                                & " ,'1'                                           AS UP_KBN                " & vbNewLine _
                                                & " FROM                                                                    " & vbNewLine _
                                                & " $LM_TRN$..C_EXPORT_L   EXPORT                                           " & vbNewLine _
                                                & " LEFT JOIN $LM_TRN$..C_OUTKA_L    OUTKA_L                                " & vbNewLine _
                                                & "   ON EXPORT.NRS_BR_CD     = OUTKA_L.NRS_BR_CD                           " & vbNewLine _
                                                & "  AND EXPORT.OUTKA_NO_L    = OUTKA_L.OUTKA_NO_L                          " & vbNewLine _
                                                & "  AND OUTKA_L.SYS_DEL_FLG  = '0'                                         " & vbNewLine _
                                                & " LEFT JOIN $LM_MST$..M_DEST    DEST                                      " & vbNewLine _
                                                & "   ON EXPORT.NRS_BR_CD     = DEST.NRS_BR_CD                              " & vbNewLine _
                                                & "  AND OUTKA_L.CUST_CD_L    = DEST.CUST_CD_L                              " & vbNewLine _
                                                & "  AND EXPORT.SHIPPER_CD    = DEST.DEST_CD                                " & vbNewLine _
                                                & "  AND DEST.SYS_DEL_FLG     = '0'                                         " & vbNewLine _
                                                & "WHERE EXPORT.NRS_BR_CD     = @NRS_BR_CD                                  " & vbNewLine _
                                                & "  AND EXPORT.OUTKA_NO_L    = @OUTKA_NO_L                                 " & vbNewLine _
                                                & "  AND EXPORT.SYS_DEL_FLG   = '0'                                         " & vbNewLine

#End Region
    '2014/01/22 輸出情報追加 END

    '2015.07.08 協立化学　シッピングマーク対応　追加START
#Region "C_MARK_HED"

    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_C_MARK_HED As String = "  SELECT                                                             " & vbNewLine _
                                                & "  MARK.NRS_BR_CD                              AS NRS_BR_CD             " & vbNewLine _
                                                & " ,MARK.OUTKA_NO_L                             AS OUTKA_NO_L            " & vbNewLine _
                                                & " ,MARK.OUTKA_NO_M                             AS OUTKA_NO_M            " & vbNewLine _
                                                & " ,MARK.CASE_NO_FROM                           AS CASE_NO_FROM          " & vbNewLine _
                                                & " ,MARK.CASE_NO_TO                             AS CASE_NO_TO            " & vbNewLine _
                                                & " ,MARK.SYS_DEL_FLG                            AS SYS_DEL_FLG           " & vbNewLine _
                                                & " ,'1'                                         AS UP_KBN                " & vbNewLine _
                                                & " FROM                                                                  " & vbNewLine _
                                                & " $LM_TRN$..C_MARK_HED   MARK                                           " & vbNewLine _
                                                & " LEFT JOIN $LM_TRN$..C_OUTKA_L    OUTKA_L                              " & vbNewLine _
                                                & "   ON MARK.NRS_BR_CD     = OUTKA_L.NRS_BR_CD                           " & vbNewLine _
                                                & "  AND MARK.OUTKA_NO_L    = OUTKA_L.OUTKA_NO_L                          " & vbNewLine _
                                                & "  AND OUTKA_L.SYS_DEL_FLG  = '0'                                          " & vbNewLine _
                                                & "WHERE MARK.NRS_BR_CD     = @NRS_BR_CD                                  " & vbNewLine _
                                                & "  AND MARK.OUTKA_NO_L    = @OUTKA_NO_L                                 " & vbNewLine _
                                                & "  AND MARK.SYS_DEL_FLG   = '0'                                         " & vbNewLine

#End Region
    '2015.07.08 協立化学　シッピングマーク対応　追加END

    '2015.07.21 協立化学　シッピングマーク対応　追加START
#Region "C_MARK_DTL"

    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_C_MARK_DTL As String = "  SELECT                                                             " & vbNewLine _
                                                & "  MARK.NRS_BR_CD                              AS NRS_BR_CD             " & vbNewLine _
                                                & " ,MARK.OUTKA_NO_L                             AS OUTKA_NO_L            " & vbNewLine _
                                                & " ,MARK.OUTKA_NO_M                             AS OUTKA_NO_M            " & vbNewLine _
                                                & " ,MARK.MARK_EDA                               AS MARK_EDA              " & vbNewLine _
                                                & " ,MARK.REMARK_INFO                            AS REMARK_INFO           " & vbNewLine _
                                                & " ,MARK.SYS_DEL_FLG                            AS SYS_DEL_FLG           " & vbNewLine _
                                                & " ,'1'                                         AS UP_KBN                " & vbNewLine _
                                                & " FROM                                                                  " & vbNewLine _
                                                & " $LM_TRN$..C_MARK_DTL   MARK                                           " & vbNewLine _
                                                & " LEFT JOIN $LM_TRN$..C_OUTKA_M    OUTKA_M                              " & vbNewLine _
                                                & "   ON MARK.NRS_BR_CD     = OUTKA_M.NRS_BR_CD                           " & vbNewLine _
                                                & "  AND MARK.OUTKA_NO_L    = OUTKA_M.OUTKA_NO_L                          " & vbNewLine _
                                                & "  AND MARK.OUTKA_NO_M    = OUTKA_M.OUTKA_NO_M                          " & vbNewLine _
                                                & "  AND OUTKA_M.SYS_DEL_FLG  = '0'                                          " & vbNewLine _
                                                & "WHERE MARK.NRS_BR_CD     = @NRS_BR_CD                                  " & vbNewLine _
                                                & "  AND MARK.OUTKA_NO_L    = @OUTKA_NO_L                                 " & vbNewLine _
                                                & "  AND MARK.SYS_DEL_FLG   = '0'                                         " & vbNewLine

#End Region
    '2015.07.21 協立化学　シッピングマーク対応　追加END


#If True Then  'ADD 2019/05/30 依頼番号 : 005136   【LMS】出荷毎に寄託価額✕商品　実際の金額を「LMC020 出荷データ編集」画面に出荷金額を表示(群馬本間) 

#Region "F_UNSOM_KITAKUGAKU"

    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_F_UNSOM_KITAKUGAKU As String = "SELECT                                       " & vbNewLine _
                                                & "  UM.UNSO_NO_L                     AS UNSO_NO_L        " & vbNewLine _
                                                & " ,UM.UNSO_NO_M                     AS UNSO_NO_M        " & vbNewLine _
                                                & " ,(UM.BETU_WT  * UNSO_TTL_NB )     AS UNSO_WT          " & vbNewLine _
                                                & " ,ISNULL(GOODS.KITAKU_GOODS_UP,0)  AS KITAKU_GOODS_UP  " & vbNewLine _
                                                & " ,UM.BETU_WT                       AS BETU_WT          " & vbNewLine _
                                                & " ,UM.UNSO_TTL_QT                   AS UNSO_TTL_QT      " & vbNewLine _
                                                & " FROM $LM_TRN$..F_UNSO_M UM                            " & vbNewLine _
                                                & " LEFT JOIN $LM_MST$..M_GOODS GOODS                     " & vbNewLine _
                                                & "   ON GOODS.NRS_BR_CD     = UM.NRS_BR_CD               " & vbNewLine _
                                                & "  AND GOODS.GOODS_CD_NRS  = UM.GOODS_CD_NRS            " & vbNewLine _
                                                & " WHERE UM.NRS_BR_CD       = @NRS_BR_CD                 " & vbNewLine _
                                                & "   AND UM.UNSO_NO_L       = @UNSO_NO_L                " & vbNewLine _
                                                & "   AND UM.SYS_DEL_FLG     = '0'                        " & vbNewLine

#End Region

#End If

#Region "H_INOUTKAEDI_HED_FJF (FFEM)"

    ' FFEM入出荷EDIデータ(ヘッダ) 取得
    Private Const SQL_SELECT_INOUTKAEDI_HED_FJF As String = "" _
        & "SELECT                                              " & vbNewLine _
        & "      H_INOUTKAEDI_HED_FJF.ZFVYHKKBN  AS ZFVYHKKBN  " & vbNewLine _
        & "    , H_INOUTKAEDI_HED_FJF.ZFVYDENTYP AS ZFVYDENTYP " & vbNewLine _
        & "FROM                                                " & vbNewLine _
        & "    $LM_TRN$..H_INOUTKAEDI_HED_FJF                  " & vbNewLine _
        & "WHERE                                               " & vbNewLine _
        & "    H_INOUTKAEDI_HED_FJF.NRS_BR_CD = @NRS_BR_CD     " & vbNewLine _
        & "AND H_INOUTKAEDI_HED_FJF.OUTKA_CTL_NO = @OUTKA_NO_L " & vbNewLine _
        & "AND H_INOUTKAEDI_HED_FJF.INOUT_KB = '0'             " & vbNewLine _
        & "AND H_INOUTKAEDI_HED_FJF.DEL_KB IN('0','2')         " & vbNewLine _
        & ""

#End Region ' "H_INOUTKAEDI_HED_FJF (FFEM)"

#Region "特定の荷主固有のテーブルが存在するか否かの判定SQL"

    ' 特定の荷主固有のテーブルが存在するか否かの判定SQL
    Private Const SQL_GET_TRN_TBL_EXISTS As String = "" _
        & "SELECT                                                       " & vbNewLine _
        & "    CASE WHEN OBJECT_ID('$LM_TRN$..' + @TBL_NM, 'U') IS NULL " & vbNewLine _
        & "        THEN '0'                                             " & vbNewLine _
        & "        ELSE '1'                                             " & vbNewLine _
        & "    END AS TBL_EXISTS                                        " & vbNewLine _
        & ""

#End Region ' "特定の荷主固有のテーブルが存在するか否かの判定SQL"

#End Region

#Region "INSERT"

#Region "OUTKA_L"

    ''' <summary>
    ''' INSERT（OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUT_L As String = "INSERT INTO                                          " & vbNewLine _
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
                                         & ",DEST_KB                                                 " & vbNewLine _
                                         & ",DEST_NM                                                 " & vbNewLine _
                                         & ",DEST_AD_1                                               " & vbNewLine _
                                         & ",DEST_AD_2                                               " & vbNewLine _
                                         & ",WH_TAB_STATUS                                           " & vbNewLine _
                                         & ",WH_TAB_YN                                               " & vbNewLine _
                                         & ",URGENT_YN                                               " & vbNewLine _
                                         & ",WH_SIJI_REMARK                                          " & vbNewLine _
                                         & ",WH_TAB_NO_SIJI_FLG                                      " & vbNewLine _
                                         & ",WH_TAB_HOKOKU_YN                                        " & vbNewLine _
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
                                         & ",@DEST_KB                                                " & vbNewLine _
                                         & ",@DEST_NM2                                               " & vbNewLine _
                                         & ",@DEST_AD_1                                              " & vbNewLine _
                                         & ",@DEST_AD_2                                              " & vbNewLine _
                                         & ",@WH_TAB_STATUS                                          " & vbNewLine _
                                         & ",@WH_TAB_YN                                              " & vbNewLine _
                                         & ",@URGENT_YN                                              " & vbNewLine _
                                         & ",@WH_SIJI_REMARK                                         " & vbNewLine _
                                         & ",@WH_TAB_NO_SIJI_FLG                                     " & vbNewLine _
                                         & ",@WH_TAB_HOKOKU_YN                                       " & vbNewLine _
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

#Region "OUTKA_M"

    ''' <summary>
    ''' INSERT（OUTKA_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUT_M As String = "INSERT INTO                                          " & vbNewLine _
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
                                         & ",GOODS_CD_NRS_FROM                                       " & vbNewLine _
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
                                         & ",@GOODS_CD_NRS_FROM                                      " & vbNewLine _
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

#Region "OUTKA_S"

    ''' <summary>
    ''' INSERT（OUTKA_S）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUT_S As String = "INSERT INTO                                          " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_S                                      " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",OUTKA_NO_L                                              " & vbNewLine _
                                         & ",OUTKA_NO_M                                              " & vbNewLine _
                                         & ",OUTKA_NO_S                                              " & vbNewLine _
                                         & ",TOU_NO                                                  " & vbNewLine _
                                         & ",SITU_NO                                                 " & vbNewLine _
                                         & ",ZONE_CD                                                 " & vbNewLine _
                                         & ",LOCA                                                    " & vbNewLine _
                                         & ",LOT_NO                                                  " & vbNewLine _
                                         & ",SERIAL_NO                                               " & vbNewLine _
                                         & ",OUTKA_TTL_NB                                            " & vbNewLine _
                                         & ",OUTKA_TTL_QT                                            " & vbNewLine _
                                         & ",ZAI_REC_NO                                              " & vbNewLine _
                                         & ",INKA_NO_L                                               " & vbNewLine _
                                         & ",INKA_NO_M                                               " & vbNewLine _
                                         & ",INKA_NO_S                                               " & vbNewLine _
                                         & ",ZAI_UPD_FLAG                                            " & vbNewLine _
                                         & ",ALCTD_CAN_NB                                            " & vbNewLine _
                                         & ",ALCTD_NB                                                " & vbNewLine _
                                         & ",ALCTD_CAN_QT                                            " & vbNewLine _
                                         & ",ALCTD_QT                                                " & vbNewLine _
                                         & ",IRIME                                                   " & vbNewLine _
                                         & ",BETU_WT                                                 " & vbNewLine _
                                         & ",COA_FLAG                                                " & vbNewLine _
                                         & ",REMARK                                                  " & vbNewLine _
                                         & ",SMPL_FLAG                                               " & vbNewLine _
                                         & ",REC_NO                                                  " & vbNewLine _
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
                                         & ",@OUTKA_NO_S                                             " & vbNewLine _
                                         & ",@TOU_NO                                                 " & vbNewLine _
                                         & ",@SITU_NO                                                " & vbNewLine _
                                         & ",@ZONE_CD                                                " & vbNewLine _
                                         & ",@LOCA                                                   " & vbNewLine _
                                         & ",@LOT_NO                                                 " & vbNewLine _
                                         & ",@SERIAL_NO                                              " & vbNewLine _
                                         & ",@OUTKA_TTL_NB                                           " & vbNewLine _
                                         & ",@OUTKA_TTL_QT                                           " & vbNewLine _
                                         & ",@ZAI_REC_NO                                             " & vbNewLine _
                                         & ",@INKA_NO_L                                              " & vbNewLine _
                                         & ",@INKA_NO_M                                              " & vbNewLine _
                                         & ",@INKA_NO_S                                              " & vbNewLine _
                                         & ",@ZAI_UPD_FLAG                                           " & vbNewLine _
                                         & ",@ALCTD_CAN_NB                                           " & vbNewLine _
                                         & ",@ALCTD_NB                                               " & vbNewLine _
                                         & ",@ALCTD_CAN_QT                                           " & vbNewLine _
                                         & ",@ALCTD_QT                                               " & vbNewLine _
                                         & ",@IRIME                                                  " & vbNewLine _
                                         & ",@BETU_WT                                                " & vbNewLine _
                                         & ",@COA_FLAG                                               " & vbNewLine _
                                         & ",@REMARK                                                 " & vbNewLine _
                                         & ",@SMPL_FLAG                                              " & vbNewLine _
                                         & ",@REC_NO                                                 " & vbNewLine _
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
                                         & ")VALUES(                                                 " & vbNewLine _
                                         & " @NRS_BR_CD                                              " & vbNewLine _
                                         & ",@SAGYO_REC_NO                                           " & vbNewLine _
                                         & ",@SAGYO_COMP                                             " & vbNewLine _
                                         & ",@SKYU_CHK                                               " & vbNewLine _
                                         & ",@SAGYO_SIJI_NO                                          " & vbNewLine _
                                         & ",@INOUTKA_NO_LM                                          " & vbNewLine _
                                         & ",@WH_CD                                                  " & vbNewLine _
                                         & ",@IOZS_KB                                                " & vbNewLine _
                                         & ",@SAGYO_CD                                               " & vbNewLine _
                                         & ",@SAGYO_NM                                               " & vbNewLine _
                                         & ",@CUST_CD_L                                              " & vbNewLine _
                                         & ",@CUST_CD_M                                              " & vbNewLine _
                                         & ",@DEST_CD                                                " & vbNewLine _
                                         & ",@DEST_NM                                                " & vbNewLine _
                                         & ",@GOODS_CD_NRS                                           " & vbNewLine _
                                         & ",@GOODS_NM_NRS                                           " & vbNewLine _
                                         & ",@LOT_NO                                                 " & vbNewLine _
                                         & ",@INV_TANI                                               " & vbNewLine _
                                         & ",@SAGYO_NB                                               " & vbNewLine _
                                         & ",@SAGYO_UP                                               " & vbNewLine _
                                         & ",@SAGYO_GK                                               " & vbNewLine _
                                         & ",@TAX_KB                                                 " & vbNewLine _
                                         & ",@SEIQTO_CD                                              " & vbNewLine _
                                         & ",@REMARK_ZAI                                             " & vbNewLine _
                                         & ",@REMARK_SKYU                                            " & vbNewLine _
                                         & ",@REMARK_SIJI                                            " & vbNewLine _
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
                                         & ")                                                        " & vbNewLine

#End Region

    'START UMANO 要望番号1302 支払運賃に伴う修正。
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
                                              & "--(2015.09.18)要望番号2408 追加START " & vbNewLine _
                                              & ",AUTO_DENP_KBN                " & vbNewLine _
                                              & ",AUTO_DENP_NO                 " & vbNewLine _
                                              & "--(2015.09.18)要望番号2408 追加END " & vbNewLine _
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
                                              & ",SHIHARAI_TARIFF_CD           " & vbNewLine _
                                              & ",SHIHARAI_ETARIFF_CD          " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
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
                                              & "--(2015.09.18)要望番号2408 追加START " & vbNewLine _
                                              & ",@AUTO_DENP_KBN               " & vbNewLine _
                                              & ",@AUTO_DENP_NO                " & vbNewLine _
                                              & "--(2015.09.18)要望番号2408 追加END " & vbNewLine _
                                              & ",@OUTKA_PLAN_DATE             " & vbNewLine _
                                              & ",@OUTKA_PLAN_TIME             " & vbNewLine _
                                              & ",@ARR_PLAN_DATE               " & vbNewLine _
                                              & ",@ARR_PLAN_TIME               " & vbNewLine _
                                              & ",@ARR_ACT_TIME                " & vbNewLine _
                                              & ",@CUST_CD_L                   " & vbNewLine _
                                              & ",@CUST_CD_M                   " & vbNewLine _
                                              & ",@CUST_REF_NO                 " & vbNewLine _
                                              & ",@SHIP_CD                     " & vbNewLine _
                                              & ",@ORIG_CD                     " & vbNewLine _
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
                                              & ",@SHIHARAI_TARIFF_CD          " & vbNewLine _
                                              & ",@SHIHARAI_ETARIFF_CD         " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

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

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "SHIHARAI"

    ''' <summary>
    ''' SHIHARAI INSERT用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SHIHARAI As String = "INSERT INTO $LM_TRN$..F_SHIHARAI_TRS" & vbNewLine _
                                              & "(                                 " & vbNewLine _
                                              & " YUSO_BR_CD                       " & vbNewLine _
                                              & ",NRS_BR_CD                        " & vbNewLine _
                                              & ",UNSO_NO_L                        " & vbNewLine _
                                              & ",UNSO_NO_M                        " & vbNewLine _
                                              & ",CUST_CD_L                        " & vbNewLine _
                                              & ",CUST_CD_M                        " & vbNewLine _
                                              & ",CUST_CD_S                        " & vbNewLine _
                                              & ",CUST_CD_SS                       " & vbNewLine _
                                              & ",SHIHARAI_GROUP_NO                " & vbNewLine _
                                              & ",SHIHARAI_GROUP_NO_M              " & vbNewLine _
                                              & ",SHIHARAITO_CD                    " & vbNewLine _
                                              & ",UNTIN_CALCULATION_KB             " & vbNewLine _
                                              & ",SHIHARAI_SYARYO_KB               " & vbNewLine _
                                              & ",SHIHARAI_PKG_UT                  " & vbNewLine _
                                              & ",SHIHARAI_NG_NB                   " & vbNewLine _
                                              & ",SHIHARAI_DANGER_KB               " & vbNewLine _
                                              & ",SHIHARAI_TARIFF_BUNRUI_KB        " & vbNewLine _
                                              & ",SHIHARAI_TARIFF_CD               " & vbNewLine _
                                              & ",SHIHARAI_ETARIFF_CD              " & vbNewLine _
                                              & ",SHIHARAI_KYORI                   " & vbNewLine _
                                              & ",SHIHARAI_WT                      " & vbNewLine _
                                              & ",SHIHARAI_UNCHIN                  " & vbNewLine _
                                              & ",SHIHARAI_CITY_EXTC               " & vbNewLine _
                                              & ",SHIHARAI_WINT_EXTC               " & vbNewLine _
                                              & ",SHIHARAI_RELY_EXTC               " & vbNewLine _
                                              & ",SHIHARAI_TOLL                    " & vbNewLine _
                                              & ",SHIHARAI_INSU                    " & vbNewLine _
                                              & ",SHIHARAI_FIXED_FLAG              " & vbNewLine _
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
                                              & ",@SHIHARAI_GROUP_NO               " & vbNewLine _
                                              & ",@SHIHARAI_GROUP_NO_M             " & vbNewLine _
                                              & ",@SHIHARAITO_CD                   " & vbNewLine _
                                              & ",@UNTIN_CALCULATION_KB            " & vbNewLine _
                                              & ",@SHIHARAI_SYARYO_KB              " & vbNewLine _
                                              & ",@SHIHARAI_PKG_UT                 " & vbNewLine _
                                              & ",@SHIHARAI_NG_NB                  " & vbNewLine _
                                              & ",@SHIHARAI_DANGER_KB              " & vbNewLine _
                                              & ",@SHIHARAI_TARIFF_BUNRUI_KB       " & vbNewLine _
                                              & ",@SHIHARAI_TARIFF_CD              " & vbNewLine _
                                              & ",@SHIHARAI_ETARIFF_CD             " & vbNewLine _
                                              & ",@SHIHARAI_KYORI                  " & vbNewLine _
                                              & ",@SHIHARAI_WT                     " & vbNewLine _
                                              & ",@SHIHARAI_UNCHIN                 " & vbNewLine _
                                              & ",@SHIHARAI_CITY_EXTC              " & vbNewLine _
                                              & ",@SHIHARAI_WINT_EXTC              " & vbNewLine _
                                              & ",@SHIHARAI_RELY_EXTC              " & vbNewLine _
                                              & ",@SHIHARAI_TOLL                   " & vbNewLine _
                                              & ",@SHIHARAI_INSU                   " & vbNewLine _
                                              & ",@SHIHARAI_FIXED_FLAG             " & vbNewLine _
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
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#Region "ZAI_TRS"

    Private Const SQL_INSERT_ZAI_TRS As String = "INSERT INTO $LM_TRN$..D_ZAI_TRS" & vbNewLine _
                                               & "(                              " & vbNewLine _
                                               & " NRS_BR_CD                     " & vbNewLine _
                                               & ",ZAI_REC_NO                    " & vbNewLine _
                                               & ",WH_CD                         " & vbNewLine _
                                               & ",TOU_NO                        " & vbNewLine _
                                               & ",SITU_NO                       " & vbNewLine _
                                               & ",ZONE_CD                       " & vbNewLine _
                                               & ",LOCA                          " & vbNewLine _
                                               & ",LOT_NO                        " & vbNewLine _
                                               & ",CUST_CD_L                     " & vbNewLine _
                                               & ",CUST_CD_M                     " & vbNewLine _
                                               & ",GOODS_CD_NRS                  " & vbNewLine _
                                               & ",GOODS_KANRI_NO                " & vbNewLine _
                                               & ",INKA_NO_L                     " & vbNewLine _
                                               & ",INKA_NO_M                     " & vbNewLine _
                                               & ",INKA_NO_S                     " & vbNewLine _
                                               & ",ALLOC_PRIORITY                " & vbNewLine _
                                               & ",RSV_NO                        " & vbNewLine _
                                               & ",SERIAL_NO                     " & vbNewLine _
                                               & ",HOKAN_YN                      " & vbNewLine _
                                               & ",TAX_KB                        " & vbNewLine _
                                               & ",GOODS_COND_KB_1               " & vbNewLine _
                                               & ",GOODS_COND_KB_2               " & vbNewLine _
                                               & ",GOODS_COND_KB_3               " & vbNewLine _
                                               & ",OFB_KB                        " & vbNewLine _
                                               & ",SPD_KB                        " & vbNewLine _
                                               & ",REMARK_OUT                    " & vbNewLine _
                                               & ",PORA_ZAI_NB                   " & vbNewLine _
                                               & ",ALCTD_NB                      " & vbNewLine _
                                               & ",ALLOC_CAN_NB                  " & vbNewLine _
                                               & ",IRIME                         " & vbNewLine _
                                               & ",PORA_ZAI_QT                   " & vbNewLine _
                                               & ",ALCTD_QT                      " & vbNewLine _
                                               & ",ALLOC_CAN_QT                  " & vbNewLine _
                                               & ",INKO_DATE                     " & vbNewLine _
                                               & ",INKO_PLAN_DATE                " & vbNewLine _
                                               & ",ZERO_FLAG                     " & vbNewLine _
                                               & ",LT_DATE                       " & vbNewLine _
                                               & ",GOODS_CRT_DATE                " & vbNewLine _
                                               & ",DEST_CD_P                     " & vbNewLine _
                                               & ",REMARK                        " & vbNewLine _
                                               & ",SMPL_FLAG                     " & vbNewLine _
                                               & ",SYS_ENT_DATE                  " & vbNewLine _
                                               & ",SYS_ENT_TIME                  " & vbNewLine _
                                               & ",SYS_ENT_PGID                  " & vbNewLine _
                                               & ",SYS_ENT_USER                  " & vbNewLine _
                                               & ",SYS_UPD_DATE                  " & vbNewLine _
                                               & ",SYS_UPD_TIME                  " & vbNewLine _
                                               & ",SYS_UPD_PGID                  " & vbNewLine _
                                               & ",SYS_UPD_USER                  " & vbNewLine _
                                               & ",SYS_DEL_FLG                   " & vbNewLine _
                                               & " )VALUES(                      " & vbNewLine _
                                               & " @NRS_BR_CD                    " & vbNewLine _
                                               & ",@ZAI_REC_NO                   " & vbNewLine _
                                               & ",@WH_CD                        " & vbNewLine _
                                               & ",@TOU_NO                       " & vbNewLine _
                                               & ",@SITU_NO                      " & vbNewLine _
                                               & ",@ZONE_CD                      " & vbNewLine _
                                               & ",@LOCA                         " & vbNewLine _
                                               & ",@LOT_NO                       " & vbNewLine _
                                               & ",@CUST_CD_L                    " & vbNewLine _
                                               & ",@CUST_CD_M                    " & vbNewLine _
                                               & ",@GOODS_CD_NRS                 " & vbNewLine _
                                               & ",@GOODS_KANRI_NO               " & vbNewLine _
                                               & ",@INKA_NO_L                    " & vbNewLine _
                                               & ",@INKA_NO_M                    " & vbNewLine _
                                               & ",@INKA_NO_S                    " & vbNewLine _
                                               & ",@ALLOC_PRIORITY               " & vbNewLine _
                                               & ",@RSV_NO                       " & vbNewLine _
                                               & ",@SERIAL_NO                    " & vbNewLine _
                                               & ",@HOKAN_YN                     " & vbNewLine _
                                               & ",@TAX_KB                       " & vbNewLine _
                                               & ",@GOODS_COND_KB_1              " & vbNewLine _
                                               & ",@GOODS_COND_KB_2              " & vbNewLine _
                                               & ",@GOODS_COND_KB_3              " & vbNewLine _
                                               & ",@OFB_KB                       " & vbNewLine _
                                               & ",@SPD_KB                       " & vbNewLine _
                                               & ",@REMARK_OUT                   " & vbNewLine _
                                               & ",@PORA_ZAI_NB                  " & vbNewLine _
                                               & ",@ALCTD_NB                     " & vbNewLine _
                                               & ",@ALLOC_CAN_NB                 " & vbNewLine _
                                               & ",@IRIME                        " & vbNewLine _
                                               & ",@PORA_ZAI_QT                  " & vbNewLine _
                                               & ",@ALCTD_QT                     " & vbNewLine _
                                               & ",@ALLOC_CAN_QT                 " & vbNewLine _
                                               & ",@INKO_DATE                    " & vbNewLine _
                                               & ",@INKO_PLAN_DATE               " & vbNewLine _
                                               & ",@ZERO_FLAG                    " & vbNewLine _
                                               & ",@LT_DATE                      " & vbNewLine _
                                               & ",@GOODS_CRT_DATE               " & vbNewLine _
                                               & ",@DEST_CD_P                    " & vbNewLine _
                                               & ",@REMARK                       " & vbNewLine _
                                               & ",@SMPL_FLAG                    " & vbNewLine _
                                               & ",@SYS_ENT_DATE                 " & vbNewLine _
                                               & ",@SYS_ENT_TIME                 " & vbNewLine _
                                               & ",@SYS_ENT_PGID                 " & vbNewLine _
                                               & ",@SYS_ENT_USER                 " & vbNewLine _
                                               & ",@SYS_UPD_DATE                 " & vbNewLine _
                                               & ",@SYS_UPD_TIME                 " & vbNewLine _
                                               & ",@SYS_UPD_PGID                 " & vbNewLine _
                                               & ",@SYS_UPD_USER                 " & vbNewLine _
                                               & ",@SYS_DEL_FLG                  " & vbNewLine _
                                               & ")                              " & vbNewLine

#End Region

#Region "IDO_TRS"

    Private Const SQL_INSERT_IDO_TRS As String = "INSERT INTO $LM_TRN$..D_IDO_TRS" & vbNewLine _
                                               & "(                              " & vbNewLine _
                                               & " NRS_BR_CD                     " & vbNewLine _
                                               & ",REC_NO                        " & vbNewLine _
                                               & ",IDO_DATE                      " & vbNewLine _
                                               & ",O_ZAI_REC_NO                  " & vbNewLine _
                                               & ",O_PORA_ZAI_NB                 " & vbNewLine _
                                               & ",O_ALCTD_NB                    " & vbNewLine _
                                               & ",O_ALLOC_CAN_NB                " & vbNewLine _
                                               & ",O_IRIME                       " & vbNewLine _
                                               & ",N_ZAI_REC_NO                  " & vbNewLine _
                                               & ",N_PORA_ZAI_NB                 " & vbNewLine _
                                               & ",N_ALCTD_NB                    " & vbNewLine _
                                               & ",N_ALLOC_CAN_NB                " & vbNewLine _
                                               & ",REMARK_KBN                    " & vbNewLine _
                                               & ",REMARK                        " & vbNewLine _
                                               & ",HOKOKU_DATE                   " & vbNewLine _
                                               & ",ZAIK_ZAN_FLG                  " & vbNewLine _
                                               & ",ZAIK_IRIME                    " & vbNewLine _
                                               & ",SYS_ENT_DATE                  " & vbNewLine _
                                               & ",SYS_ENT_TIME                  " & vbNewLine _
                                               & ",SYS_ENT_PGID                  " & vbNewLine _
                                               & ",SYS_ENT_USER                  " & vbNewLine _
                                               & ",SYS_UPD_DATE                  " & vbNewLine _
                                               & ",SYS_UPD_TIME                  " & vbNewLine _
                                               & ",SYS_UPD_PGID                  " & vbNewLine _
                                               & ",SYS_UPD_USER                  " & vbNewLine _
                                               & ",SYS_DEL_FLG                   " & vbNewLine _
                                               & " )VALUES(                      " & vbNewLine _
                                               & " @NRS_BR_CD                    " & vbNewLine _
                                               & ",@REC_NO                       " & vbNewLine _
                                               & ",@IDO_DATE                     " & vbNewLine _
                                               & ",@O_ZAI_REC_NO                 " & vbNewLine _
                                               & ",@O_PORA_ZAI_NB                " & vbNewLine _
                                               & ",@O_ALCTD_NB                   " & vbNewLine _
                                               & ",@O_ALLOC_CAN_NB               " & vbNewLine _
                                               & ",@O_IRIME                      " & vbNewLine _
                                               & ",@N_ZAI_REC_NO                 " & vbNewLine _
                                               & ",@N_PORA_ZAI_NB                " & vbNewLine _
                                               & ",@N_ALCTD_NB                   " & vbNewLine _
                                               & ",@N_ALLOC_CAN_NB               " & vbNewLine _
                                               & ",@REMARK_KBN                   " & vbNewLine _
                                               & ",@REMARK                       " & vbNewLine _
                                               & ",@HOKOKU_DATE                  " & vbNewLine _
                                               & ",@ZAIK_ZAN_FLG                 " & vbNewLine _
                                               & ",@O_IRIME                      " & vbNewLine _
                                               & ",@SYS_ENT_DATE                 " & vbNewLine _
                                               & ",@SYS_ENT_TIME                 " & vbNewLine _
                                               & ",@SYS_ENT_PGID                 " & vbNewLine _
                                               & ",@SYS_ENT_USER                 " & vbNewLine _
                                               & ",@SYS_UPD_DATE                 " & vbNewLine _
                                               & ",@SYS_UPD_TIME                 " & vbNewLine _
                                               & ",@SYS_UPD_PGID                 " & vbNewLine _
                                               & ",@SYS_UPD_USER                 " & vbNewLine _
                                               & ",@SYS_DEL_FLG                  " & vbNewLine _
                                               & ")                              " & vbNewLine

#End Region

    '2014/01/22 輸出情報追加 START
#Region "EXPORT_L"

    ''' <summary>
    ''' INSERT（EXPORT_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_EXPORT_L As String = "INSERT INTO                        " & vbNewLine _
                                         & "$LM_TRN$..C_EXPORT_L                      " & vbNewLine _
                                         & "(                                         " & vbNewLine _
                                         & " NRS_BR_CD                                " & vbNewLine _
                                         & ",OUTKA_NO_L                               " & vbNewLine _
                                         & ",SHIP_NM                                  " & vbNewLine _
                                         & ",DESTINATION                              " & vbNewLine _
                                         & ",BOOKING_NO                               " & vbNewLine _
                                         & ",VOYAGE_NO                                " & vbNewLine _
                                         & ",SHIPPER_CD                               " & vbNewLine _
                                         & ",CONT_LOADING_DATE                        " & vbNewLine _
                                         & ",STORAGE_TEST_DATE                        " & vbNewLine _
                                         & ",STORAGE_TEST_TIME                        " & vbNewLine _
                                         & ",DEPARTURE_DATE                           " & vbNewLine _
                                         & ",CONTAINER_NO                             " & vbNewLine _
                                         & ",CONTAINER_NM                             " & vbNewLine _
                                         & ",CONTAINER_SIZE                           " & vbNewLine _
                                         & ",SYS_ENT_DATE                             " & vbNewLine _
                                         & ",SYS_ENT_TIME                             " & vbNewLine _
                                         & ",SYS_ENT_PGID                             " & vbNewLine _
                                         & ",SYS_ENT_USER                             " & vbNewLine _
                                         & ",SYS_UPD_DATE                             " & vbNewLine _
                                         & ",SYS_UPD_TIME                             " & vbNewLine _
                                         & ",SYS_UPD_PGID                             " & vbNewLine _
                                         & ",SYS_UPD_USER                             " & vbNewLine _
                                         & ",SYS_DEL_FLG                              " & vbNewLine _
                                         & ")VALUES(                                  " & vbNewLine _
                                         & " @NRS_BR_CD                               " & vbNewLine _
                                         & ",@OUTKA_NO_L                              " & vbNewLine _
                                         & ",@SHIP_NM                                 " & vbNewLine _
                                         & ",@DESTINATION                             " & vbNewLine _
                                         & ",@BOOKING_NO                              " & vbNewLine _
                                         & ",@VOYAGE_NO                               " & vbNewLine _
                                         & ",@SHIPPER_CD                              " & vbNewLine _
                                         & ",@CONT_LOADING_DATE                       " & vbNewLine _
                                         & ",@STORAGE_TEST_DATE                       " & vbNewLine _
                                         & ",@STORAGE_TEST_TIME                       " & vbNewLine _
                                         & ",@DEPARTURE_DATE                          " & vbNewLine _
                                         & ",@CONTAINER_NO                            " & vbNewLine _
                                         & ",@CONTAINER_NM                            " & vbNewLine _
                                         & ",@CONTAINER_SIZE                          " & vbNewLine _
                                         & ",@SYS_ENT_DATE                            " & vbNewLine _
                                         & ",@SYS_ENT_TIME                            " & vbNewLine _
                                         & ",@SYS_ENT_PGID                            " & vbNewLine _
                                         & ",@SYS_ENT_USER                            " & vbNewLine _
                                         & ",@SYS_UPD_DATE                            " & vbNewLine _
                                         & ",@SYS_UPD_TIME                            " & vbNewLine _
                                         & ",@SYS_UPD_PGID                            " & vbNewLine _
                                         & ",@SYS_UPD_USER                            " & vbNewLine _
                                         & ",@SYS_DEL_FLG                             " & vbNewLine _
                                         & ")                                         " & vbNewLine
#End Region
    '2014/01/22 輸出情報追加 END

    '2015.07.08 協立化学　シッピング対応 追加START
#Region "C_MARK_HED"

    ''' <summary>
    ''' INSERT（C_MARK_HED）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_C_MARK_HED As String = "INSERT INTO                                          " & vbNewLine _
                                         & "$LM_TRN$..C_MARK_HED                                      " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",OUTKA_NO_L                                              " & vbNewLine _
                                         & ",OUTKA_NO_M                                              " & vbNewLine _
                                         & ",CASE_NO_FROM                                            " & vbNewLine _
                                         & ",CASE_NO_TO                                              " & vbNewLine _
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
                                         & ",@CASE_NO_FROM                                           " & vbNewLine _
                                         & ",@CASE_NO_TO                                             " & vbNewLine _
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
    '2015.07.08 協立化学　シッピング対応 追加END

    '2015.07.21 協立化学　シッピング対応 追加START
#Region "C_MARK_DTL"

    ''' <summary>
    ''' INSERT（C_MARK_DTL）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_C_MARK_DTL As String = "INSERT INTO                                     " & vbNewLine _
                                         & "$LM_TRN$..C_MARK_DTL                                     " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",OUTKA_NO_L                                              " & vbNewLine _
                                         & ",OUTKA_NO_M                                              " & vbNewLine _
                                         & ",MARK_EDA                                                " & vbNewLine _
                                         & ",REMARK_INFO                                             " & vbNewLine _
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
                                         & ",@MARK_EDA                                               " & vbNewLine _
                                         & ",@REMARK_INFO                                            " & vbNewLine _
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
    '2015.07.21 協立化学　シッピング対応 追加END

#End Region

#Region "UPDATE"

#Region "OUTKA_L"

    ''' <summary>
    ''' UPDATE（OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKA_L As String = "UPDATE $LM_TRN$..C_OUTKA_L SET              " & vbNewLine _
                                              & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
                                              & ",OUTKA_NO_L           = @OUTKA_NO_L          " & vbNewLine _
                                              & ",FURI_NO              = @FURI_NO             " & vbNewLine _
                                              & ",OUTKA_KB             = @OUTKA_KB            " & vbNewLine _
                                              & ",SYUBETU_KB           = @SYUBETU_KB          " & vbNewLine _
                                              & ",OUTKA_STATE_KB       = @OUTKA_STATE_KB      " & vbNewLine _
                                              & ",OUTKAHOKOKU_YN       = @OUTKAHOKOKU_YN      " & vbNewLine _
                                              & ",PICK_KB              = @PICK_KB             " & vbNewLine _
                                              & ",DENP_NO              = @DENP_NO             " & vbNewLine _
                                              & ",ARR_KANRYO_INFO      = @ARR_KANRYO_INFO     " & vbNewLine _
                                              & ",WH_CD                = @WH_CD               " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE      = @OUTKA_PLAN_DATE     " & vbNewLine _
                                              & ",OUTKO_DATE           = @OUTKO_DATE          " & vbNewLine _
                                              & ",ARR_PLAN_DATE        = @ARR_PLAN_DATE       " & vbNewLine _
                                              & ",ARR_PLAN_TIME        = @ARR_PLAN_TIME       " & vbNewLine _
                                              & ",HOKOKU_DATE          = @HOKOKU_DATE         " & vbNewLine _
                                              & ",TOUKI_HOKAN_YN       = @TOUKI_HOKAN_YN      " & vbNewLine _
                                              & ",END_DATE             = @END_DATE            " & vbNewLine _
                                              & ",CUST_CD_L            = @CUST_CD_L           " & vbNewLine _
                                              & ",CUST_CD_M            = @CUST_CD_M           " & vbNewLine _
                                              & ",SHIP_CD_L            = @SHIP_CD_L           " & vbNewLine _
                                              & ",SHIP_CD_M            = @SHIP_CD_M           " & vbNewLine _
                                              & ",DEST_CD              = @DEST_CD             " & vbNewLine _
                                              & ",DEST_AD_3            = @DEST_AD_3           " & vbNewLine _
                                              & ",DEST_TEL             = @DEST_TEL            " & vbNewLine _
                                              & ",NHS_REMARK           = @NHS_REMARK          " & vbNewLine _
                                              & ",SP_NHS_KB            = @SP_NHS_KB           " & vbNewLine _
                                              & ",COA_YN               = @COA_YN              " & vbNewLine _
                                              & ",CUST_ORD_NO          = @CUST_ORD_NO         " & vbNewLine _
                                              & ",BUYER_ORD_NO         = @BUYER_ORD_NO        " & vbNewLine _
                                              & ",REMARK               = @REMARK              " & vbNewLine _
                                              & ",OUTKA_PKG_NB         = @OUTKA_PKG_NB        " & vbNewLine _
                                              & ",DENP_YN              = @DENP_YN             " & vbNewLine _
                                              & ",PC_KB                = @PC_KB               " & vbNewLine _
                                              & ",NIYAKU_YN            = @NIYAKU_YN           " & vbNewLine _
                                              & ",ALL_PRINT_FLAG       = @ALL_PRINT_FLAG      " & vbNewLine _
                                              & ",NIHUDA_FLAG          = @NIHUDA_FLAG         " & vbNewLine _
                                              & ",NHS_FLAG             = @NHS_FLAG            " & vbNewLine _
                                              & ",DENP_FLAG            = @DENP_FLAG           " & vbNewLine _
                                              & ",COA_FLAG             = @COA_FLAG            " & vbNewLine _
                                              & ",HOKOKU_FLAG          = @HOKOKU_FLAG         " & vbNewLine _
                                              & ",MATOME_PICK_FLAG     = @MATOME_PICK_FLAG    " & vbNewLine _
                                              & ",LAST_PRINT_DATE      = @LAST_PRINT_DATE     " & vbNewLine _
                                              & ",LAST_PRINT_TIME      = @LAST_PRINT_TIME     " & vbNewLine _
                                              & ",SASZ_USER            = @SASZ_USER           " & vbNewLine _
                                              & ",OUTKO_USER           = @OUTKO_USER          " & vbNewLine _
                                              & ",KEN_USER             = @KEN_USER            " & vbNewLine _
                                              & ",OUTKA_USER           = @OUTKA_USER          " & vbNewLine _
                                              & ",HOU_USER             = @HOU_USER            " & vbNewLine _
                                              & ",ORDER_TYPE           = @ORDER_TYPE          " & vbNewLine _
                                              & ",DEST_KB              = @DEST_KB             " & vbNewLine _
                                              & ",DEST_NM              = @DEST_NM2            " & vbNewLine _
                                              & ",DEST_AD_1            = @DEST_AD_1           " & vbNewLine _
                                              & ",DEST_AD_2            = @DEST_AD_2           " & vbNewLine _
                                              & ",WH_TAB_STATUS        = @WH_TAB_STATUS       " & vbNewLine _
                                              & ",WH_TAB_YN            = @WH_TAB_YN           " & vbNewLine _
                                              & ",URGENT_YN            = @URGENT_YN           " & vbNewLine _
                                              & ",WH_SIJI_REMARK       = @WH_SIJI_REMARK      " & vbNewLine _
                                              & ",WH_TAB_NO_SIJI_FLG   = @WH_TAB_NO_SIJI_FLG  " & vbNewLine _
                                              & ",WH_TAB_HOKOKU_YN     = @WH_TAB_HOKOKU_YN    " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
                                              & ",SYS_DEL_FLG          = @SYS_DEL_FLG         " & vbNewLine _
                                              & "WHERE   NRS_BR_CD     = @NRS_BR_CD           " & vbNewLine _
                                              & "AND OUTKA_NO_L        = @OUTKA_NO_L          " & vbNewLine

#End Region

#Region "OUTKA_M"

    ''' <summary>
    ''' UPDATE（OUTKA_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKA_M As String = "UPDATE $LM_TRN$..C_OUTKA_M SET              " & vbNewLine _
                                              & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
                                              & ",OUTKA_NO_L           = @OUTKA_NO_L          " & vbNewLine _
                                              & ",OUTKA_NO_M           = @OUTKA_NO_M          " & vbNewLine _
                                              & ",EDI_SET_NO           = @EDI_SET_NO          " & vbNewLine _
                                              & ",COA_YN               = @COA_YN              " & vbNewLine _
                                              & ",CUST_ORD_NO_DTL      = @CUST_ORD_NO_DTL     " & vbNewLine _
                                              & ",BUYER_ORD_NO_DTL     = @BUYER_ORD_NO_DTL    " & vbNewLine _
                                              & ",GOODS_CD_NRS         = @GOODS_CD_NRS        " & vbNewLine _
                                              & ",RSV_NO               = @RSV_NO              " & vbNewLine _
                                              & ",LOT_NO               = @LOT_NO              " & vbNewLine _
                                              & ",SERIAL_NO            = @SERIAL_NO           " & vbNewLine _
                                              & ",ALCTD_KB             = @ALCTD_KB            " & vbNewLine _
                                              & ",OUTKA_PKG_NB         = @OUTKA_PKG_NB        " & vbNewLine _
                                              & ",OUTKA_HASU           = @OUTKA_HASU          " & vbNewLine _
                                              & ",OUTKA_QT             = @OUTKA_QT            " & vbNewLine _
                                              & ",OUTKA_TTL_NB         = @OUTKA_TTL_NB        " & vbNewLine _
                                              & ",OUTKA_TTL_QT         = @OUTKA_TTL_QT        " & vbNewLine _
                                              & ",ALCTD_NB             = @ALCTD_NB            " & vbNewLine _
                                              & ",ALCTD_QT             = @ALCTD_QT            " & vbNewLine _
                                              & ",BACKLOG_NB           = @BACKLOG_NB          " & vbNewLine _
                                              & ",BACKLOG_QT           = @BACKLOG_QT          " & vbNewLine _
                                              & ",UNSO_ONDO_KB         = @UNSO_ONDO_KB        " & vbNewLine _
                                              & ",IRIME                = @IRIME               " & vbNewLine _
                                              & ",IRIME_UT             = @IRIME_UT            " & vbNewLine _
                                              & ",OUTKA_M_PKG_NB       = @OUTKA_M_PKG_NB      " & vbNewLine _
                                              & ",REMARK               = @REMARK              " & vbNewLine _
                                              & ",SIZE_KB              = @SIZE_KB             " & vbNewLine _
                                              & ",ZAIKO_KB             = @ZAIKO_KB            " & vbNewLine _
                                              & ",SOURCE_CD            = @SOURCE_CD           " & vbNewLine _
                                              & ",YELLOW_CARD          = @YELLOW_CARD         " & vbNewLine _
                                              & ",GOODS_CD_NRS_FROM    = @GOODS_CD_NRS_FROM   " & vbNewLine _
                                              & ",PRINT_SORT           = @PRINT_SORT          " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD     = @NRS_BR_CD           " & vbNewLine _
                                              & "AND OUTKA_NO_L        = @OUTKA_NO_L          " & vbNewLine _
                                              & "AND OUTKA_NO_M        = @OUTKA_NO_M          " & vbNewLine

#End Region

#Region "OUTKA_S"

    ''' <summary>
    ''' UPDATE（OUTKA_S）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKA_S As String = "UPDATE $LM_TRN$..C_OUTKA_S SET              " & vbNewLine _
                                              & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
                                              & ",OUTKA_NO_L           = @OUTKA_NO_L          " & vbNewLine _
                                              & ",OUTKA_NO_M           = @OUTKA_NO_M          " & vbNewLine _
                                              & ",OUTKA_NO_S           = @OUTKA_NO_S          " & vbNewLine _
                                              & ",TOU_NO               = @TOU_NO              " & vbNewLine _
                                              & ",SITU_NO              = @SITU_NO             " & vbNewLine _
                                              & ",ZONE_CD              = @ZONE_CD             " & vbNewLine _
                                              & ",LOCA                 = @LOCA                " & vbNewLine _
                                              & ",LOT_NO               = @LOT_NO              " & vbNewLine _
                                              & ",SERIAL_NO            = @SERIAL_NO           " & vbNewLine _
                                              & ",OUTKA_TTL_NB         = @OUTKA_TTL_NB        " & vbNewLine _
                                              & ",OUTKA_TTL_QT         = @OUTKA_TTL_QT        " & vbNewLine _
                                              & ",ZAI_REC_NO           = @ZAI_REC_NO          " & vbNewLine _
                                              & ",INKA_NO_L            = @INKA_NO_L           " & vbNewLine _
                                              & ",INKA_NO_M            = @INKA_NO_M           " & vbNewLine _
                                              & ",INKA_NO_S            = @INKA_NO_S           " & vbNewLine _
                                              & ",ZAI_UPD_FLAG         = @ZAI_UPD_FLAG        " & vbNewLine _
                                              & ",ALCTD_CAN_NB         = @ALCTD_CAN_NB        " & vbNewLine _
                                              & ",ALCTD_NB             = @ALCTD_NB            " & vbNewLine _
                                              & ",ALCTD_CAN_QT         = @ALCTD_CAN_QT        " & vbNewLine _
                                              & ",ALCTD_QT             = @ALCTD_QT            " & vbNewLine _
                                              & ",IRIME                = @IRIME               " & vbNewLine _
                                              & ",BETU_WT              = @BETU_WT             " & vbNewLine _
                                              & ",COA_FLAG             = @COA_FLAG            " & vbNewLine _
                                              & ",REMARK               = @REMARK              " & vbNewLine _
                                              & ",SMPL_FLAG            = @SMPL_FLAG           " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD     = @NRS_BR_CD           " & vbNewLine _
                                              & "AND OUTKA_NO_L        = @OUTKA_NO_L          " & vbNewLine _
                                              & "AND OUTKA_NO_M        = @OUTKA_NO_M          " & vbNewLine _
                                              & "AND OUTKA_NO_S        = @OUTKA_NO_S          " & vbNewLine

#End Region

#Region "SAGYO"

    Private Const SQL_UPDATE_SAGYO_SYS_DATETIME As String = "UPDATE $LM_TRN$..E_SAGYO SET        " & vbNewLine _
                                                           & "       SAGYO_COMP   = @SAGYO_COMP  " & vbNewLine _
                                                           & "      ,SAGYO_NM     = @SAGYO_NM    " & vbNewLine _
                                                           & "      ,DEST_CD      = @DEST_CD     " & vbNewLine _
                                                           & "      ,DEST_NM      = @DEST_NM     " & vbNewLine _
                                                           & "      ,GOODS_CD_NRS = @GOODS_CD_NRS " & vbNewLine _
                                                           & "      ,GOODS_NM_NRS = @GOODS_NM_NRS " & vbNewLine _
                                                           & "      ,LOT_NO       = @LOT_NO      " & vbNewLine _
                                                           & "      ,INV_TANI     = @INV_TANI    " & vbNewLine _
                                                           & "      ,SAGYO_NB     = @SAGYO_NB    " & vbNewLine _
                                                           & "      ,SAGYO_UP     = @SAGYO_UP    " & vbNewLine _
                                                           & "      ,SAGYO_GK     = @SAGYO_GK    " & vbNewLine _
                                                           & "      ,TAX_KB       = @TAX_KB      " & vbNewLine _
                                                           & "      ,SEIQTO_CD    = @SEIQTO_CD   " & vbNewLine _
                                                           & "      ,REMARK_SKYU  = @REMARK_SKYU " & vbNewLine _
                                                           & "      ,REMARK_SIJI  = @REMARK_SIJI " & vbNewLine _
                                                           & "      ,SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                                           & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                                           & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                                           & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                                           & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                           & "  AND  SAGYO_REC_NO = @SAGYO_REC_NO" & vbNewLine

#End Region

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "UNSO_L"

    Private Const SQL_UPDATE_UNSO_L As String = "UPDATE $LM_TRN$..F_UNSO_L SET           " & vbNewLine _
                                              & " NRS_BR_CD          = @NRS_BR_CD        " & vbNewLine _
                                              & ",UNSO_NO_L          = @UNSO_NO_L        " & vbNewLine _
                                              & ",YUSO_BR_CD         = @YUSO_BR_CD       " & vbNewLine _
                                              & ",INOUTKA_NO_L       = @INOUTKA_NO_L     " & vbNewLine _
                                              & ",TRIP_NO            = @TRIP_NO          " & vbNewLine _
                                              & ",UNSO_CD            = @UNSO_CD          " & vbNewLine _
                                              & ",UNSO_BR_CD         = @UNSO_BR_CD       " & vbNewLine _
                                              & ",BIN_KB             = @BIN_KB           " & vbNewLine _
                                              & ",JIYU_KB            = @JIYU_KB          " & vbNewLine _
                                              & ",DENP_NO            = @DENP_NO          " & vbNewLine _
                                              & "--(2015.09.18)要望番号2408 追加START    " & vbNewLine _
                                              & ",AUTO_DENP_KBN      = @AUTO_DENP_KBN    " & vbNewLine _
                                              & ",AUTO_DENP_NO       = @AUTO_DENP_NO     " & vbNewLine _
                                              & "--(2015.09.18)要望番号2408 追加END      " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE    = @OUTKA_PLAN_DATE  " & vbNewLine _
                                              & ",OUTKA_PLAN_TIME    = @OUTKA_PLAN_TIME  " & vbNewLine _
                                              & ",ARR_PLAN_DATE      = @ARR_PLAN_DATE    " & vbNewLine _
                                              & ",ARR_PLAN_TIME      = @ARR_PLAN_TIME    " & vbNewLine _
                                              & ",ARR_ACT_TIME       = @ARR_ACT_TIME     " & vbNewLine _
                                              & ",CUST_CD_L          = @CUST_CD_L        " & vbNewLine _
                                              & ",CUST_CD_M          = @CUST_CD_M        " & vbNewLine _
                                              & ",CUST_REF_NO        = @CUST_REF_NO      " & vbNewLine _
                                              & ",SHIP_CD            = @SHIP_CD          " & vbNewLine _
                                              & ",ORIG_CD            = @ORIG_CD          " & vbNewLine _
                                              & ",DEST_CD            = @DEST_CD          " & vbNewLine _
                                              & ",UNSO_PKG_NB        = @UNSO_PKG_NB      " & vbNewLine _
                                              & ",NB_UT              = @NB_UT            " & vbNewLine _
                                              & ",UNSO_WT            = @UNSO_WT          " & vbNewLine _
                                              & ",UNSO_ONDO_KB       = @UNSO_ONDO_KB     " & vbNewLine _
                                              & ",PC_KB              = @PC_KB            " & vbNewLine _
                                              & ",TARIFF_BUNRUI_KB   = @TARIFF_BUNRUI_KB " & vbNewLine _
                                              & ",VCLE_KB            = @VCLE_KB          " & vbNewLine _
                                              & ",MOTO_DATA_KB       = @MOTO_DATA_KB     " & vbNewLine _
                                              & ",TAX_KB             = @TAX_KB           " & vbNewLine _
                                              & ",REMARK             = @REMARK           " & vbNewLine _
                                              & ",SEIQ_TARIFF_CD     = @SEIQ_TARIFF_CD   " & vbNewLine _
                                              & ",SEIQ_ETARIFF_CD    = @SEIQ_ETARIFF_CD  " & vbNewLine _
                                              & ",AD_3               = @AD_3             " & vbNewLine _
                                              & ",UNSO_TEHAI_KB      = @UNSO_TEHAI_KB    " & vbNewLine _
                                              & ",BUY_CHU_NO         = @BUY_CHU_NO       " & vbNewLine _
                                              & ",AREA_CD            = @AREA_CD          " & vbNewLine _
                                              & ",TYUKEI_HAISO_FLG   = @TYUKEI_HAISO_FLG " & vbNewLine _
                                              & ",SYUKA_TYUKEI_CD    = @SYUKA_TYUKEI_CD  " & vbNewLine _
                                              & ",HAIKA_TYUKEI_CD    = @HAIKA_TYUKEI_CD  " & vbNewLine _
                                              & ",TRIP_NO_SYUKA      = @TRIP_NO_SYUKA    " & vbNewLine _
                                              & ",TRIP_NO_TYUKEI     = @TRIP_NO_TYUKEI   " & vbNewLine _
                                              & ",TRIP_NO_HAIKA      = @TRIP_NO_HAIKA    " & vbNewLine _
                                              & ",SYS_UPD_DATE       = @SYS_UPD_DATE     " & vbNewLine _
                                              & ",SYS_UPD_TIME       = @SYS_UPD_TIME     " & vbNewLine _
                                              & ",SYS_UPD_PGID       = @SYS_UPD_PGID     " & vbNewLine _
                                              & ",SYS_UPD_USER       = @SYS_UPD_USER     " & vbNewLine _
                                              & ",SHIHARAI_TARIFF_CD   = @SHIHARAI_TARIFF_CD  " & vbNewLine _
                                              & ",SHIHARAI_ETARIFF_CD  = @SHIHARAI_ETARIFF_CD " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD        " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L        " & vbNewLine

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#Region "ZAI_TRS"

    '「入庫日」、「届先コード」の更新はしない。
    'データセットが小分け時の追加と共有のため、SQL文自体から削除
    Private Const SQL_UPDATE_ZAI_TRS As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET       " & vbNewLine _
                                               & " NRS_BR_CD         = @NRS_BR_CD      " & vbNewLine _
                                               & ",ZAI_REC_NO        = @ZAI_REC_NO     " & vbNewLine _
                                               & ",WH_CD             = @WH_CD          " & vbNewLine _
                                               & ",TOU_NO            = @TOU_NO         " & vbNewLine _
                                               & ",SITU_NO           = @SITU_NO        " & vbNewLine _
                                               & ",ZONE_CD           = @ZONE_CD        " & vbNewLine _
                                               & ",LOCA              = @LOCA           " & vbNewLine _
                                               & ",LOT_NO            = @LOT_NO         " & vbNewLine _
                                               & ",GOODS_CD_NRS      = @GOODS_CD_NRS   " & vbNewLine _
                                               & ",GOODS_KANRI_NO    = @GOODS_KANRI_NO " & vbNewLine _
                                               & ",INKA_NO_L         = @INKA_NO_L      " & vbNewLine _
                                               & ",INKA_NO_M         = @INKA_NO_M      " & vbNewLine _
                                               & ",INKA_NO_S         = @INKA_NO_S      " & vbNewLine _
                                               & ",ALLOC_PRIORITY    = @ALLOC_PRIORITY " & vbNewLine _
                                               & ",RSV_NO            = @RSV_NO         " & vbNewLine _
                                               & ",SERIAL_NO         = @SERIAL_NO      " & vbNewLine _
                                               & ",HOKAN_YN          = @HOKAN_YN       " & vbNewLine _
                                               & ",TAX_KB            = @TAX_KB         " & vbNewLine _
                                               & ",GOODS_COND_KB_1   = @GOODS_COND_KB_1" & vbNewLine _
                                               & ",GOODS_COND_KB_2   = @GOODS_COND_KB_2" & vbNewLine _
                                               & ",GOODS_COND_KB_3   = @GOODS_COND_KB_3" & vbNewLine _
                                               & ",OFB_KB            = @OFB_KB         " & vbNewLine _
                                               & ",SPD_KB            = @SPD_KB         " & vbNewLine _
                                               & ",REMARK_OUT          = @REMARK_OUT   " & vbNewLine _
                                               & ",PORA_ZAI_NB       = @PORA_ZAI_NB    " & vbNewLine _
                                               & ",ALCTD_NB          = @ALCTD_NB       " & vbNewLine _
                                               & ",ALLOC_CAN_NB      = @ALLOC_CAN_NB   " & vbNewLine _
                                               & ",IRIME             = @IRIME          " & vbNewLine _
                                               & ",PORA_ZAI_QT       = @PORA_ZAI_QT    " & vbNewLine _
                                               & ",ALCTD_QT          = @ALCTD_QT       " & vbNewLine _
                                               & ",ALLOC_CAN_QT      = @ALLOC_CAN_QT   " & vbNewLine _
                                               & ",INKO_PLAN_DATE    = @INKO_PLAN_DATE " & vbNewLine _
                                               & ",LT_DATE           = @LT_DATE        " & vbNewLine _
                                               & ",GOODS_CRT_DATE    = @GOODS_CRT_DATE " & vbNewLine _
                                               & ",REMARK            = @REMARK         " & vbNewLine _
                                               & ",SYS_UPD_DATE      = @SYS_UPD_DATE   " & vbNewLine _
                                               & ",SYS_UPD_TIME      = @SYS_UPD_TIME   " & vbNewLine _
                                               & ",SYS_UPD_PGID      = @SYS_UPD_PGID   " & vbNewLine _
                                               & ",SYS_UPD_USER      = @SYS_UPD_USER   " & vbNewLine _
                                               & "WHERE NRS_BR_CD    = @NRS_BR_CD      " & vbNewLine _
                                               & "  AND ZAI_REC_NO   = @ZAI_REC_NO     " & vbNewLine

    'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
    '「入庫日」、「届先コード」の更新はしない。
    'データセットが小分け時の追加と共有のため、SQL文自体から削除
    '小分けで、更新前のALLOC_CAN_NBが0の場合は、PORA_ZAI_NB、ALLOC_CAN_NBを+1し、PORA_ZAI_QT、ALLOC_CAN_QTは+IRIMEする
    Private Const SQL_UPDATE_ZAI_TRS_DEL As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET       " & vbNewLine _
                                               & " NRS_BR_CD         = @NRS_BR_CD      " & vbNewLine _
                                               & ",ZAI_REC_NO        = @ZAI_REC_NO     " & vbNewLine _
                                               & ",WH_CD             = @WH_CD          " & vbNewLine _
                                               & ",TOU_NO            = @TOU_NO         " & vbNewLine _
                                               & ",SITU_NO           = @SITU_NO        " & vbNewLine _
                                               & ",ZONE_CD           = @ZONE_CD        " & vbNewLine _
                                               & ",LOCA              = @LOCA           " & vbNewLine _
                                               & ",LOT_NO            = @LOT_NO         " & vbNewLine _
                                               & ",GOODS_CD_NRS      = @GOODS_CD_NRS   " & vbNewLine _
                                               & ",GOODS_KANRI_NO    = @GOODS_KANRI_NO " & vbNewLine _
                                               & ",INKA_NO_L         = @INKA_NO_L      " & vbNewLine _
                                               & ",INKA_NO_M         = @INKA_NO_M      " & vbNewLine _
                                               & ",INKA_NO_S         = @INKA_NO_S      " & vbNewLine _
                                               & ",ALLOC_PRIORITY    = @ALLOC_PRIORITY " & vbNewLine _
                                               & ",RSV_NO            = @RSV_NO         " & vbNewLine _
                                               & ",SERIAL_NO         = @SERIAL_NO      " & vbNewLine _
                                               & ",HOKAN_YN          = @HOKAN_YN       " & vbNewLine _
                                               & ",TAX_KB            = @TAX_KB         " & vbNewLine _
                                               & ",GOODS_COND_KB_1   = @GOODS_COND_KB_1" & vbNewLine _
                                               & ",GOODS_COND_KB_2   = @GOODS_COND_KB_2" & vbNewLine _
                                               & ",GOODS_COND_KB_3   = @GOODS_COND_KB_3" & vbNewLine _
                                               & ",OFB_KB            = @OFB_KB         " & vbNewLine _
                                               & ",SPD_KB            = @SPD_KB         " & vbNewLine _
                                               & ",REMARK_OUT        = @REMARK_OUT     " & vbNewLine _
                                               & ",PORA_ZAI_NB       = @PORA_ZAI_NB    " & vbNewLine _
                                               & ",ALCTD_NB          = @ALCTD_NB       " & vbNewLine _
                                               & ",ALLOC_CAN_NB      = @ALLOC_CAN_NB   " & vbNewLine _
                                               & ",IRIME             = @IRIME          " & vbNewLine _
                                               & ",PORA_ZAI_QT       = @PORA_ZAI_QT    " & vbNewLine _
                                               & ",ALCTD_QT          = @ALCTD_QT       " & vbNewLine _
                                               & ",ALLOC_CAN_QT      = @ALLOC_CAN_QT   " & vbNewLine _
                                               & ",INKO_PLAN_DATE    = @INKO_PLAN_DATE " & vbNewLine _
                                               & ",LT_DATE           = @LT_DATE        " & vbNewLine _
                                               & ",GOODS_CRT_DATE    = @GOODS_CRT_DATE " & vbNewLine _
                                               & ",REMARK            = @REMARK         " & vbNewLine _
                                               & ",SYS_UPD_DATE      = @SYS_UPD_DATE   " & vbNewLine _
                                               & ",SYS_UPD_TIME      = @SYS_UPD_TIME   " & vbNewLine _
                                               & ",SYS_UPD_PGID      = @SYS_UPD_PGID   " & vbNewLine _
                                               & ",SYS_UPD_USER      = @SYS_UPD_USER   " & vbNewLine _
                                               & "WHERE NRS_BR_CD    = @NRS_BR_CD      " & vbNewLine _
                                               & "  AND ZAI_REC_NO   = @ZAI_REC_NO     " & vbNewLine

    '要望管理2014
    'Private Const SQL_UPDATE_ZAI_TRS_DEL_KOWAKE As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET            " & vbNewLine _
    '                                                      & " PORA_ZAI_NB       = PORA_ZAI_NB + 1      " & vbNewLine _
    '                                                      & ",ALLOC_CAN_NB      = ALLOC_CAN_NB + 1     " & vbNewLine _
    '                                                      & ",PORA_ZAI_QT       = PORA_ZAI_QT + IRIME  " & vbNewLine _
    '                                                      & ",ALLOC_CAN_QT      = ALLOC_CAN_QT + IRIME " & vbNewLine _
    '                                                      & ",SYS_UPD_DATE      = @SYS_UPD_DATE        " & vbNewLine _
    '                                                      & ",SYS_UPD_TIME      = @SYS_UPD_TIME        " & vbNewLine _
    '                                                      & ",SYS_UPD_PGID      = @SYS_UPD_PGID        " & vbNewLine _
    '                                                      & ",SYS_UPD_USER      = @SYS_UPD_USER        " & vbNewLine _
    '                                                      & "WHERE NRS_BR_CD    = @NRS_BR_CD           " & vbNewLine _
    '                                                      & "  AND ZAI_REC_NO   = @ZAI_REC_NO          " & vbNewLine

    Private Const SQL_UPDATE_ZAI_TRS_DEL_KOWAKE2 As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET            " & vbNewLine _
                                                           & " ALCTD_NB          = CASE WHEN ALCTD_NB = 1 THEN 0 ELSE ALCTD_NB END         " & vbNewLine _
                                                           & ",ALLOC_CAN_NB      = CASE WHEN ALLOC_CAN_NB = 0 THEN 1 ELSE ALLOC_CAN_NB END     " & vbNewLine _
                                                           & ",ALCTD_QT          = ALCTD_QT - @ALCTD_QT " & vbNewLine _
                                                           & ",ALLOC_CAN_QT      = ALLOC_CAN_QT + @ALCTD_QT " & vbNewLine _
                                                           & ",SYS_UPD_DATE      = @SYS_UPD_DATE        " & vbNewLine _
                                                           & ",SYS_UPD_TIME      = @SYS_UPD_TIME        " & vbNewLine _
                                                           & ",SYS_UPD_PGID      = @SYS_UPD_PGID        " & vbNewLine _
                                                           & ",SYS_UPD_USER      = @SYS_UPD_USER        " & vbNewLine _
                                                           & "WHERE NRS_BR_CD    = @NRS_BR_CD           " & vbNewLine _
                                                           & "  AND ZAI_REC_NO   = @ZAI_REC_NO          " & vbNewLine
    'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
    '要望管理2014

#End Region

    'START YANAI 20110913 小分け対応
#Region "ZAI_TRS"

    'START YANAI 要望番号811
    ''データセットが小分け時の追加と共有のため、SQL文自体から削除
    'Private Const SQL_UPDATE_ZAI_TRS_KOWAKE As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET       " & vbNewLine _
    '                                           & " PORA_ZAI_NB       = @PORA_ZAI_NB    " & vbNewLine _
    '                                           & ",ALLOC_CAN_NB      = @ALLOC_CAN_NB   " & vbNewLine _
    '                                           & ",PORA_ZAI_QT       = @PORA_ZAI_QT    " & vbNewLine _
    '                                           & ",ALLOC_CAN_QT      = @ALLOC_CAN_QT   " & vbNewLine _
    '                                           & ",SYS_UPD_DATE      = @SYS_UPD_DATE   " & vbNewLine _
    '                                           & ",SYS_UPD_TIME      = @SYS_UPD_TIME   " & vbNewLine _
    '                                           & ",SYS_UPD_PGID      = @SYS_UPD_PGID   " & vbNewLine _
    '                                           & ",SYS_UPD_USER      = @SYS_UPD_USER   " & vbNewLine _
    '                                           & "WHERE NRS_BR_CD    = @NRS_BR_CD      " & vbNewLine _
    '                                           & "  AND ZAI_REC_NO   = @ZAI_REC_NO     " & vbNewLine
    'データセットが小分け時の追加と共有のため、SQL文自体から削除
    'START YANAI 要望番号930
    'Private Const SQL_UPDATE_ZAI_TRS_KOWAKE As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET       " & vbNewLine _
    '                                           & " PORA_ZAI_NB       = @PORA_ZAI_NB    " & vbNewLine _
    '                                           & ",ALLOC_CAN_NB      = @ALLOC_CAN_NB   " & vbNewLine _
    '                                           & ",PORA_ZAI_QT       = @PORA_ZAI_QT    " & vbNewLine _
    '                                           & ",ALLOC_CAN_QT      = PORA_ZAI_QT - ALCTD_QT " & vbNewLine _
    '                                           & ",SYS_UPD_DATE      = @SYS_UPD_DATE   " & vbNewLine _
    '                                           & ",SYS_UPD_TIME      = @SYS_UPD_TIME   " & vbNewLine _
    '                                           & ",SYS_UPD_PGID      = @SYS_UPD_PGID   " & vbNewLine _
    '                                           & ",SYS_UPD_USER      = @SYS_UPD_USER   " & vbNewLine _
    '                                           & "WHERE NRS_BR_CD    = @NRS_BR_CD      " & vbNewLine _
    '                                           & "  AND ZAI_REC_NO   = @ZAI_REC_NO     " & vbNewLine
    '既に引当されてる場合は更新しないように変更
    Private Const SQL_UPDATE_ZAI_TRS_KOWAKE As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET       " & vbNewLine _
                                               & " PORA_ZAI_NB       = @PORA_ZAI_NB    " & vbNewLine _
                                               & ",ALLOC_CAN_NB      = @PORA_ZAI_NB - ALCTD_NB   " & vbNewLine _
                                               & ",PORA_ZAI_QT       = @PORA_ZAI_QT    " & vbNewLine _
                                               & ",ALLOC_CAN_QT      = @PORA_ZAI_QT - ALCTD_QT " & vbNewLine _
                                               & ",SYS_UPD_DATE      = @SYS_UPD_DATE   " & vbNewLine _
                                               & ",SYS_UPD_TIME      = @SYS_UPD_TIME   " & vbNewLine _
                                               & ",SYS_UPD_PGID      = @SYS_UPD_PGID   " & vbNewLine _
                                               & ",SYS_UPD_USER      = @SYS_UPD_USER   " & vbNewLine _
                                               & "WHERE NRS_BR_CD    = @NRS_BR_CD      " & vbNewLine _
                                               & "  AND ZAI_REC_NO   = @ZAI_REC_NO     " & vbNewLine _
    'END YANAI 要望番号930
    'END YANAI 要望番号811

#End Region

#Region "IDO_TRS"

    Private Const SQL_UPDATE_IDO_TRS As String = "UPDATE $LM_TRN$..D_IDO_TRS SET           " & vbNewLine _
                                               & " O_ALLOC_CAN_NB    = O_ALLOC_CAN_NB + @NB " & vbNewLine _
                                               & ",N_PORA_ZAI_NB     = N_PORA_ZAI_NB - @NB " & vbNewLine _
                                               & ",N_ALLOC_CAN_NB    = N_ALLOC_CAN_NB - @NB " & vbNewLine _
                                               & ",SYS_UPD_DATE      = @SYS_UPD_DATE       " & vbNewLine _
                                               & ",SYS_UPD_TIME      = @SYS_UPD_TIME       " & vbNewLine _
                                               & ",SYS_UPD_PGID      = @SYS_UPD_PGID       " & vbNewLine _
                                               & ",SYS_UPD_USER      = @SYS_UPD_USER       " & vbNewLine _
                                               & "WHERE NRS_BR_CD    = @NRS_BR_CD          " & vbNewLine _
                                               & "  AND REC_NO       = @REC_NO             " & vbNewLine

#End Region
    'END YANAI 20110913 小分け対応

    '要望番号:997 terakawa 2012.10.22 Start
#Region "OUTKAEDI_M"

    ''' <summary>
    ''' UPDATE（OUTKAEDI_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKAEDI_M As String = "UPDATE $LM_TRN$..H_OUTKAEDI_M SET              " & vbNewLine _
                                              & " DEL_KB               = @SYS_DEL_FLG               " & vbNewLine _
                                              & ",UPD_USER             = @UPD_USER                  " & vbNewLine _
                                              & ",UPD_DATE             = @UPD_DATE                  " & vbNewLine _
                                              & ",UPD_TIME             = @UPD_TIME                  " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE              " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME              " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID              " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER              " & vbNewLine _
                                              & ",SYS_DEL_FLG          = @SYS_DEL_FLG               " & vbNewLine _
                                              & "WHERE   NRS_BR_CD     = @NRS_BR_CD                 " & vbNewLine _
                                              & "AND OUTKA_CTL_NO      = @OUTKA_NO_L                " & vbNewLine _
                                              & "AND OUTKA_CTL_NO_CHU  = @OUTKA_NO_M                " & vbNewLine
#End Region

#Region "OUTKAEDI_DTL"

    ''' <summary>
    ''' UPDATE（OUTKAEDI_DTL）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKAEDI_DTL As String = " DEL_KB         = @SYS_DEL_FLG         " & vbNewLine _
                                              & ",DELETE_USER          = @DELETE_USER               " & vbNewLine _
                                              & ",DELETE_DATE          = @DELETE_DATE               " & vbNewLine _
                                              & ",DELETE_TIME          = @DELETE_TIME               " & vbNewLine _
                                              & ",UPD_USER             = @UPD_USER                  " & vbNewLine _
                                              & ",UPD_DATE             = @UPD_DATE                  " & vbNewLine _
                                              & ",UPD_TIME             = @UPD_TIME                  " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE              " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME              " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID              " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER              " & vbNewLine _
                                              & ",SYS_DEL_FLG          = @SYS_DEL_FLG               " & vbNewLine _
                                              & "WHERE   NRS_BR_CD     = @NRS_BR_CD                 " & vbNewLine _
                                              & "AND OUTKA_CTL_NO      = @OUTKA_NO_L                " & vbNewLine _
                                              & "AND OUTKA_CTL_NO_CHU  = @OUTKA_NO_M                " & vbNewLine _
                                              & "AND DEL_KB            <> '1'                       " & vbNewLine _
                                              & "AND SYS_DEL_FLG       <> '1'                       " & vbNewLine

#End Region
    '要望番号:997 terakawa 2012.10.22 End

#Region "印刷"

    'START YANAI 要望番号890
    'Private Const SQL_UPDATE_PRINT As String = "UPDATE $LM_TRN$..C_OUTKA_L SET                    " & vbNewLine _
    '                                         & " OUTKA_STATE_KB    = CASE    WHEN @PRINT_KB = '06'" & vbNewLine _
    '                                         & "                          OR      @PRINT_KB = '07'" & vbNewLine _
    '                                         & "                          OR      @PRINT_KB = '08'" & vbNewLine _
    '                                         & "                             THEN @OUTKA_STATE_KB " & vbNewLine _
    '                                         & "                             ELSE OUTKA_STATE_KB  " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",ALL_PRINT_FLAG    = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
    '                                         & "                             THEN '01'            " & vbNewLine _
    '                                         & "                             ELSE ALL_PRINT_FLAG  " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",NIHUDA_FLAG       = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
    '                                         & "                             THEN @NIHUDA_FLAG    " & vbNewLine _
    '                                         & "                             WHEN @PRINT_KB = '01'" & vbNewLine _
    '                                         & "                             THEN '01'            " & vbNewLine _
    '                                         & "                             ELSE NIHUDA_FLAG     " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",NHS_FLAG          = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
    '                                         & "                          OR      @PRINT_KB = '03'" & vbNewLine _
    '                                         & "                             THEN '01'            " & vbNewLine _
    '                                         & "                             ELSE NHS_FLAG        " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",DENP_FLAG         = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
    '                                         & "                          OR      @PRINT_KB = '02'" & vbNewLine _
    '                                         & "                             THEN '01'            " & vbNewLine _
    '                                         & "                             ELSE DENP_FLAG       " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",COA_FLAG          = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
    '                                         & "                             THEN @COA_FLAG       " & vbNewLine _
    '                                         & "                             WHEN @PRINT_KB = '05'" & vbNewLine _
    '                                         & "                             THEN '01'            " & vbNewLine _
    '                                         & "                             ELSE COA_FLAG        " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",HOKOKU_FLAG       = CASE    WHEN @PRINT_KB = '06'" & vbNewLine _
    '                                         & "                             THEN '01'            " & vbNewLine _
    '                                         & "                             ELSE HOKOKU_FLAG     " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",LAST_PRINT_DATE   = @SYS_UPD_DATE                " & vbNewLine _
    '                                         & ",LAST_PRINT_TIME   = SUBSTRING(@SYS_UPD_TIME,1,6) " & vbNewLine _
    '                                         & ",SASZ_USER         = CASE    WHEN @PRINT_KB = '07'" & vbNewLine _
    '                                         & "                             THEN @SYS_UPD_USER   " & vbNewLine _
    '                                         & "                             ELSE SASZ_USER       " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",HOU_USER          = CASE    WHEN @PRINT_KB = '06'" & vbNewLine _
    '                                         & "                             THEN @SYS_UPD_USER   " & vbNewLine _
    '                                         & "                             ELSE HOU_USER        " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",SYS_UPD_DATE      = @SYS_UPD_DATE                " & vbNewLine _
    '                                         & ",SYS_UPD_TIME      = @SYS_UPD_TIME                " & vbNewLine _
    '                                         & ",SYS_UPD_PGID      = @SYS_UPD_PGID                " & vbNewLine _
    '                                         & ",SYS_UPD_USER      = @SYS_UPD_USER                " & vbNewLine _
    '                                         & "WHERE NRS_BR_CD    = @NRS_BR_CD                   " & vbNewLine _
    '                                         & "  AND OUTKA_NO_L   = @OUTKA_NO_L                  " & vbNewLine _
    '                                         & "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE            " & vbNewLine _
    '                                         & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME            " & vbNewLine
    'START YANAI 要望番号497
    'Private Const SQL_UPDATE_PRINT As String = "UPDATE $LM_TRN$..C_OUTKA_L SET                    " & vbNewLine _
    '                                         & " OUTKA_STATE_KB    = CASE    WHEN @PRINT_KB = '06'" & vbNewLine _
    '                                         & "                          OR      @PRINT_KB = '07'" & vbNewLine _
    '                                         & "                          OR      @PRINT_KB = '08'" & vbNewLine _
    '                                         & "                             THEN @OUTKA_STATE_KB " & vbNewLine _
    '                                         & "                             ELSE OUTKA_STATE_KB  " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",ALL_PRINT_FLAG    = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
    '                                         & "                             THEN '01'            " & vbNewLine _
    '                                         & "                             ELSE ALL_PRINT_FLAG  " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",NIHUDA_FLAG       = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
    '                                         & "                             THEN @NIHUDA_FLAG    " & vbNewLine _
    '                                         & "                             WHEN @PRINT_KB = '01'" & vbNewLine _
    '                                         & "                             THEN '01'            " & vbNewLine _
    '                                         & "                             ELSE NIHUDA_FLAG     " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",NHS_FLAG          = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
    '                                         & "                          OR      @PRINT_KB = '03'" & vbNewLine _
    '                                         & "                          OR      @PRINT_KB = '04'" & vbNewLine _
    '                                         & "                             THEN '01'            " & vbNewLine _
    '                                         & "                             ELSE NHS_FLAG        " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",DENP_FLAG         = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
    '                                         & "                          OR      @PRINT_KB = '02'" & vbNewLine _
    '                                         & "                             THEN '01'            " & vbNewLine _
    '                                         & "                             ELSE DENP_FLAG       " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",COA_FLAG          = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
    '                                         & "                             THEN @COA_FLAG       " & vbNewLine _
    '                                         & "                             WHEN @PRINT_KB = '05'" & vbNewLine _
    '                                         & "                             THEN '01'            " & vbNewLine _
    '                                         & "                             ELSE COA_FLAG        " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",HOKOKU_FLAG       = CASE    WHEN @PRINT_KB = '06'" & vbNewLine _
    '                                         & "                             THEN '01'            " & vbNewLine _
    '                                         & "                             ELSE HOKOKU_FLAG     " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",LAST_PRINT_DATE   = @SYS_UPD_DATE                " & vbNewLine _
    '                                         & ",LAST_PRINT_TIME   = SUBSTRING(@SYS_UPD_TIME,1,6) " & vbNewLine _
    '                                         & ",SASZ_USER         = CASE    WHEN @PRINT_KB = '07'" & vbNewLine _
    '                                         & "                             THEN @SYS_UPD_USER   " & vbNewLine _
    '                                         & "                             ELSE SASZ_USER       " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",HOU_USER          = CASE    WHEN @PRINT_KB = '06'" & vbNewLine _
    '                                         & "                             THEN @SYS_UPD_USER   " & vbNewLine _
    '                                         & "                             ELSE HOU_USER        " & vbNewLine _
    '                                         & "                      END                         " & vbNewLine _
    '                                         & ",SYS_UPD_DATE      = @SYS_UPD_DATE                " & vbNewLine _
    '                                         & ",SYS_UPD_TIME      = @SYS_UPD_TIME                " & vbNewLine _
    '                                         & ",SYS_UPD_PGID      = @SYS_UPD_PGID                " & vbNewLine _
    '                                         & ",SYS_UPD_USER      = @SYS_UPD_USER                " & vbNewLine _
    '                                         & "WHERE NRS_BR_CD    = @NRS_BR_CD                   " & vbNewLine _
    '                                         & "  AND OUTKA_NO_L   = @OUTKA_NO_L                  " & vbNewLine _
    '                                         & "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE            " & vbNewLine _
    '                                         & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME            " & vbNewLine
    Private Const SQL_UPDATE_PRINT As String = "UPDATE $LM_TRN$..C_OUTKA_L SET                    " & vbNewLine _
                                             & " OUTKA_STATE_KB    = CASE    WHEN @PRINT_KB = '07'" & vbNewLine _
                                             & "                          OR      @PRINT_KB = '08'" & vbNewLine _
                                             & "                             THEN @OUTKA_STATE_KB " & vbNewLine _
                                             & "                             ELSE OUTKA_STATE_KB  " & vbNewLine _
                                             & "                      END                         " & vbNewLine _
                                             & ",ALL_PRINT_FLAG    = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
                                             & "                             THEN '01'            " & vbNewLine _
                                             & "                             ELSE ALL_PRINT_FLAG  " & vbNewLine _
                                             & "                      END                         " & vbNewLine _
                                             & ",NIHUDA_FLAG       = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
                                             & "                             THEN @NIHUDA_FLAG    " & vbNewLine _
                                             & "                             WHEN @PRINT_KB = '01'" & vbNewLine _
                                             & "                             THEN '01'            " & vbNewLine _
                                             & "                             ELSE NIHUDA_FLAG     " & vbNewLine _
                                             & "                      END                         " & vbNewLine _
                                             & ",NHS_FLAG          = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
                                             & "                          OR      @PRINT_KB = '03'" & vbNewLine _
                                             & "                          OR      @PRINT_KB = '04'" & vbNewLine _
                                             & "                             THEN '01'            " & vbNewLine _
                                             & "                             ELSE NHS_FLAG        " & vbNewLine _
                                             & "                      END                         " & vbNewLine _
                                             & ",DENP_FLAG         = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
                                             & "                          OR      @PRINT_KB = '02'" & vbNewLine _
                                             & "                             THEN '01'            " & vbNewLine _
                                             & "                             ELSE DENP_FLAG       " & vbNewLine _
                                             & "                      END                         " & vbNewLine _
                                             & ",COA_FLAG          = CASE    WHEN @PRINT_KB = '08'" & vbNewLine _
                                             & "                             THEN @COA_FLAG       " & vbNewLine _
                                             & "                             WHEN @PRINT_KB = '05'" & vbNewLine _
                                             & "                             THEN '01'            " & vbNewLine _
                                             & "                             ELSE COA_FLAG        " & vbNewLine _
                                             & "                      END                         " & vbNewLine _
                                             & ",HOKOKU_FLAG       = CASE    WHEN @PRINT_KB = '06'" & vbNewLine _
                                             & "                             THEN '01'            " & vbNewLine _
                                             & "                             ELSE HOKOKU_FLAG     " & vbNewLine _
                                             & "                      END                         " & vbNewLine _
                                             & ",LAST_PRINT_DATE   = @SYS_UPD_DATE                " & vbNewLine _
                                             & ",LAST_PRINT_TIME   = SUBSTRING(@SYS_UPD_TIME,1,6) " & vbNewLine _
                                             & ",SASZ_USER         = CASE    WHEN @PRINT_KB = '07'" & vbNewLine _
                                             & "                             THEN @SYS_UPD_USER   " & vbNewLine _
                                             & "                             ELSE SASZ_USER       " & vbNewLine _
                                             & "                      END                         " & vbNewLine _
                                             & ",HOU_USER          = CASE    WHEN @PRINT_KB = '06'" & vbNewLine _
                                             & "                             THEN @SYS_UPD_USER   " & vbNewLine _
                                             & "                             ELSE HOU_USER        " & vbNewLine _
                                             & "                      END                         " & vbNewLine _
                                             & ",SYS_UPD_DATE      = @SYS_UPD_DATE                " & vbNewLine _
                                             & ",SYS_UPD_TIME      = @SYS_UPD_TIME                " & vbNewLine _
                                             & ",SYS_UPD_PGID      = @SYS_UPD_PGID                " & vbNewLine _
                                             & ",SYS_UPD_USER      = @SYS_UPD_USER                " & vbNewLine _
                                             & "WHERE NRS_BR_CD    = @NRS_BR_CD                   " & vbNewLine _
                                             & "  AND OUTKA_NO_L   = @OUTKA_NO_L                  " & vbNewLine _
                                             & "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE            " & vbNewLine _
                                             & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME            " & vbNewLine
    'END YANAI 要望番号497
    'END YANAI 要望番号890

    Private Const SQL_UPDATE_PRINT_COA As String = "UPDATE $LM_TRN$..C_OUTKA_L SET                    " & vbNewLine _
                                            & "COA_FLAG          = '01'                          " & vbNewLine _
                                            & ",LAST_PRINT_DATE   = @SYS_UPD_DATE                " & vbNewLine _
                                            & ",LAST_PRINT_TIME   = SUBSTRING(@SYS_UPD_TIME,1,6) " & vbNewLine _
                                            & ",SYS_UPD_DATE      = @SYS_UPD_DATE                " & vbNewLine _
                                            & ",SYS_UPD_TIME      = @SYS_UPD_TIME                " & vbNewLine _
                                            & ",SYS_UPD_PGID      = @SYS_UPD_PGID                " & vbNewLine _
                                            & ",SYS_UPD_USER      = @SYS_UPD_USER                " & vbNewLine _
                                            & "WHERE NRS_BR_CD    = @NRS_BR_CD                   " & vbNewLine _
                                            & "  AND OUTKA_NO_L   = @OUTKA_NO_L                  " & vbNewLine

#End Region

    '2014/01/22 輸出情報追加 START
#Region "C_EXPORT_L"

    ''' <summary>
    ''' UPDATE（C_EXPORT_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_EXPORT_L As String = "UPDATE $LM_TRN$..C_EXPORT_L SET            " & vbNewLine _
                                              & " NRS_BR_CD            = @NRS_BR_CD           " & vbNewLine _
                                              & ",OUTKA_NO_L           = @OUTKA_NO_L          " & vbNewLine _
                                              & ",SHIP_NM              = @SHIP_NM             " & vbNewLine _
                                              & ",DESTINATION          = @DESTINATION         " & vbNewLine _
                                              & ",BOOKING_NO           = @BOOKING_NO          " & vbNewLine _
                                              & ",VOYAGE_NO            = @VOYAGE_NO           " & vbNewLine _
                                              & ",SHIPPER_CD           = @SHIPPER_CD          " & vbNewLine _
                                              & ",CONT_LOADING_DATE    = @CONT_LOADING_DATE   " & vbNewLine _
                                              & ",STORAGE_TEST_DATE    = @STORAGE_TEST_DATE   " & vbNewLine _
                                              & ",STORAGE_TEST_TIME    = @STORAGE_TEST_TIME   " & vbNewLine _
                                              & ",DEPARTURE_DATE       = @DEPARTURE_DATE      " & vbNewLine _
                                              & ",CONTAINER_NO         = @CONTAINER_NO        " & vbNewLine _
                                              & ",CONTAINER_NM         = @CONTAINER_NM        " & vbNewLine _
                                              & ",CONTAINER_SIZE       = @CONTAINER_SIZE      " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD     = @NRS_BR_CD           " & vbNewLine _
                                              & "  AND   OUTKA_NO_L    = @OUTKA_NO_L          " & vbNewLine

#End Region
    '2014/01/22 輸出情報追加 END

    '2015.07.08 協立化学　シッピング対応 追加START
#Region "C_MARK_HED"

    ''' <summary>
    ''' UPDATE（C_MARK_HED）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_C_MARK_HED As String = "UPDATE $LM_TRN$..C_MARK_HED SET         " & vbNewLine _
                                              & " CASE_NO_FROM         = @CASE_NO_FROM       " & vbNewLine _
                                              & ",CASE_NO_TO           = @CASE_NO_TO         " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD     = @NRS_BR_CD           " & vbNewLine _
                                              & "AND OUTKA_NO_L        = @OUTKA_NO_L          " & vbNewLine _
                                              & "AND OUTKA_NO_M        = @OUTKA_NO_M          " & vbNewLine

#End Region
    '2015.07.08 協立化学　シッピング対応 追加END

    '2015.07.21 協立化学　シッピング対応 追加START
#Region "C_MARK_DTL"

    ''' <summary>
    ''' UPDATE（C_MARK_DTL）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_C_MARK_DTL As String = "UPDATE $LM_TRN$..C_MARK_DTL SET          " & vbNewLine _
                                              & " REMARK_INFO          = @REMARK_INFO         " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD     = @NRS_BR_CD           " & vbNewLine _
                                              & "AND OUTKA_NO_L        = @OUTKA_NO_L          " & vbNewLine _
                                              & "AND OUTKA_NO_M        = @OUTKA_NO_M          " & vbNewLine _
                                              & "AND MARK_EDA          = @MARK_EDA            " & vbNewLine

#End Region
    '2015.07.21 協立化学　シッピング対応 追加END

#Region "M_DEST"
    'ADD 2018/05/14 依頼番号　001545 
    Private Const SQL_UPDATE_M_DEST As String = "UPDATE $LM_MST$..M_DEST SET        " & vbNewLine _
                                              & "       TEL = @DEST_TEL               " & vbNewLine _
                                              & "      ,SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                              & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                              & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                              & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                              & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                              & "  AND  CUST_CD_L    = @CUST_CD_L   " & vbNewLine _
                                              & "  AND  DEST_CD     = @DEST_CD      " & vbNewLine _
                                              & "  AND  SYS_DEL_FLG  = '0'          " & vbNewLine _
                                              & "  AND  TEL        <> @DEST_TEL     " & vbNewLine

#End Region
#End Region

#Region "Delete"

#Region "UNSO_L"

    Private Const SQL_DELETE_UNSO_L As String = "DELETE FROM $LM_TRN$..F_UNSO_L    " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD  " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L  " & vbNewLine

#End Region

#Region "UNSO_M"

    Private Const SQL_DELETE_UNSO_M As String = "DELETE FROM $LM_TRN$..F_UNSO_M    " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD  " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L  " & vbNewLine

#End Region

#Region "UNCHIN"

    Private Const SQL_DELETE_UNCHIN As String = "DELETE FROM $LM_TRN$..F_UNCHIN_TRS " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD   " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L   " & vbNewLine

#End Region

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "SHIHARAI"

    Private Const SQL_DELETE_SHIHARAI As String = "DELETE FROM $LM_TRN$..F_SHIHARAI_TRS " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD   " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L   " & vbNewLine

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#Region "SAGYO"

    Private Const SQL_DELETE_SAGYO As String = "DELETE FROM $LM_TRN$..E_SAGYO         " & vbNewLine _
                                             & "WHERE   NRS_BR_CD      = @NRS_BR_CD   " & vbNewLine _
                                             & "  AND   SAGYO_REC_NO   = @SAGYO_REC_NO" & vbNewLine

    ' 2012.12.13 要望番号：612 振替一括削除対応 START Nakamura
    '入荷番号に紐付く作業データの削除
    Private Const SQL_DELETE_SAGYO_IN As String = "DELETE FROM $LM_TRN$..E_SAGYO         " & vbNewLine _
                                             & "WHERE   NRS_BR_CD      = @NRS_BR_CD      " & vbNewLine _
                                             & "  AND   INOUTKA_NO_LM   LIKE @INKA_NO_L  " & vbNewLine _
                                             & "  AND   IOZS_KB        = '11'            " & vbNewLine

    ' 2012.12.13 要望番号：612 振替一括削除対応 END Nakamura


#End Region

#Region "UPDATE_DEL_FLG"

    Private Const SQL_COM_UPDATE_DEL_FLG As String = " SYS_UPD_DATE    = @SYS_UPD_DATE" & vbNewLine _
                                                   & ",SYS_UPD_TIME    = @SYS_UPD_TIME" & vbNewLine _
                                                   & ",SYS_UPD_PGID    = @SYS_UPD_PGID" & vbNewLine _
                                                   & ",SYS_UPD_USER    = @SYS_UPD_USER" & vbNewLine _
                                                   & ",SYS_DEL_FLG     = @SYS_DEL_FLG " & vbNewLine _
                                                   & " WHERE NRS_BR_CD = @NRS_BR_CD   " & vbNewLine

    '2012.11.28 START NAKAMURA 要望番号:612 振替一括削除対応

    Private Const SQL_COM_UPDATE_DEL_FLG_FURI_DEL As String = " SYS_UPD_PGID    = @SYS_UPD_PGID" & vbNewLine _
                                                            & ",SYS_UPD_USER    = @SYS_UPD_USER" & vbNewLine _
                                                            & ",SYS_DEL_FLG     = @SYS_DEL_FLG " & vbNewLine _
                                                            & " WHERE NRS_BR_CD = @NRS_BR_CD   " & vbNewLine

    '2012.11.28 END NAKAMURA
#End Region

#Region "H_OUTKAEDI_DTL_RAPI(Rapidus次回分納情報)"

    ''' <summary>
    ''' H_OUTKAEDI_DTL_RAPI(Rapidus次回分納情報)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DEL_FLG_JIKAI_BUNNOU_OUTKAEDI_DTL_RAPI As String = "" _
            & "UPDATE                             " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_DTL_RAPI  " & vbNewLine _
            & "SET                                " & vbNewLine _
            & "      DEL_KB = @DEL_KB             " & vbNewLine _
            & "    , DELETE_USER = @DELETE_USER   " & vbNewLine _
            & "    , DELETE_DATE = @DELETE_DATE   " & vbNewLine _
            & "    , DELETE_TIME = @DELETE_TIME   " & vbNewLine _
            & "    , UPD_USER = @UPD_USER         " & vbNewLine _
            & "    , UPD_DATE = @UPD_DATE         " & vbNewLine _
            & "    , UPD_TIME = @UPD_TIME         " & vbNewLine _
            & "    , SYS_UPD_DATE = @SYS_UPD_DATE " & vbNewLine _
            & "    , SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine _
            & "    , SYS_UPD_PGID = @SYS_UPD_PGID " & vbNewLine _
            & "    , SYS_UPD_USER = @SYS_UPD_USER " & vbNewLine _
            & "    , SYS_DEL_FLG = @SYS_DEL_FLG   " & vbNewLine _
            & "WHERE                              " & vbNewLine _
            & "    NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
            & "AND CRT_DATE = @CRT_DATE           " & vbNewLine _
            & "AND FILE_NAME = @FILE_NAME         " & vbNewLine _
            & "AND EDI_CTL_NO = @EDI_CTL_NO       " & vbNewLine _
            & ""

#End Region ' "H_OUTKAEDI_DTL_RAPI(Rapidus次回分納情報)"

#Region "H_OUTKAEDI_L(次回分納情報)"

    ''' <summary>
    ''' H_OUTKAEDI_L(次回分納情報)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DEL_FLG_JIKAI_BUNNOU_OUTKAEDI_L As String = "" _
            & "UPDATE                                 " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_L             " & vbNewLine _
            & "SET                                    " & vbNewLine _
            & "      DEL_KB = @DEL_KB                 " & vbNewLine _
            & "    , UPD_USER = @UPD_USER             " & vbNewLine _
            & "    , UPD_DATE = @UPD_DATE             " & vbNewLine _
            & "    , UPD_TIME = @UPD_TIME             " & vbNewLine _
            & "    , SYS_UPD_DATE = @SYS_UPD_DATE     " & vbNewLine _
            & "    , SYS_UPD_TIME = @SYS_UPD_TIME     " & vbNewLine _
            & "    , SYS_UPD_PGID = @SYS_UPD_PGID     " & vbNewLine _
            & "    , SYS_UPD_USER = @SYS_UPD_USER     " & vbNewLine _
            & "    , SYS_DEL_FLG = @SYS_DEL_FLG       " & vbNewLine _
            & "WHERE                                  " & vbNewLine _
            & "    NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
            & "AND EDI_CTL_NO = @EDI_CTL_NO           " & vbNewLine _
            & "AND SYS_UPD_DATE = @EDI_L_SYS_UPD_DATE " & vbNewLine _
            & "AND SYS_UPD_TIME = @EDI_L_SYS_UPD_TIME " & vbNewLine _
            & ""

#End Region ' "H_OUTKAEDI_L(次回分納情報)"

#Region "H_OUTKAEDI_M(次回分納情報)"

    ''' <summary>
    ''' H_OUTKAEDI_M(次回分納情報)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DEL_FLG_JIKAI_BUNNOU_OUTKAEDI_M As String = "" _
            & "UPDATE                             " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_M         " & vbNewLine _
            & "SET                                " & vbNewLine _
            & "      DEL_KB = @DEL_KB             " & vbNewLine _
            & "    , UPD_USER = @UPD_USER         " & vbNewLine _
            & "    , UPD_DATE = @UPD_DATE         " & vbNewLine _
            & "    , UPD_TIME = @UPD_TIME         " & vbNewLine _
            & "    , SYS_UPD_DATE = @SYS_UPD_DATE " & vbNewLine _
            & "    , SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine _
            & "    , SYS_UPD_PGID = @SYS_UPD_PGID " & vbNewLine _
            & "    , SYS_UPD_USER = @SYS_UPD_USER " & vbNewLine _
            & "    , SYS_DEL_FLG = @SYS_DEL_FLG   " & vbNewLine _
            & "WHERE                              " & vbNewLine _
            & "    NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
            & "AND EDI_CTL_NO = @EDI_CTL_NO       " & vbNewLine _
            & ""

#End Region ' "H_OUTKAEDI_M(次回分納情報)"

#End Region

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row2 As Data.DataRow

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

    ''' <summary>
    ''' マスタスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

    ''' <summary>
    ''' トランザクションスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnSchemaNm As String

#End Region

#Region "WIT対応"

#Region "ハンディ荷主情報取得"

    Private Const SQL_CUST_HANDY As String = _
              "SELECT                                                                                                                    " & vbNewLine _
            & "    cust.NRS_BR_CD                                                                                                        " & vbNewLine _
            & "    , cust.WH_CD                                                                                                          " & vbNewLine _
            & "    , cust.CUST_CD_L                                                                                                      " & vbNewLine _
            & "    , cust.CUST_NM_SHORT                                                                                                  " & vbNewLine _
            & "    , cust.FLG_01                                                                                                         " & vbNewLine _
            & "    , cust.FLG_02                                                                                                         " & vbNewLine _
            & "    , cust.FLG_03                                                                                                         " & vbNewLine _
            & "    , cust.FLG_04                                                                                                         " & vbNewLine _
            & "    , cust.FLG_05                                                                                                         " & vbNewLine _
            & "    , cust.FLG_06                                                                                                         " & vbNewLine _
            & "    , cust.FLG_07                                                                                                         " & vbNewLine _
            & "    , cust.FLG_08                                                                                                         " & vbNewLine _
            & "    , cust.FLG_09                                                                                                         " & vbNewLine _
            & "    , cust.FLG_10                                                                                                         " & vbNewLine _
            & "    , cust.FLG_11                                                                                                         " & vbNewLine _
            & "    , cust.FLG_12                                                                                                         " & vbNewLine _
            & "    , cust.FLG_13                                                                                                         " & vbNewLine _
            & "    , cust.FLG_14                                                                                                         " & vbNewLine _
            & "    , cust.FLG_15                                                                                                         " & vbNewLine _
            & "    , cust.FLG_16                                                                                                         " & vbNewLine _
            & "    , cust.FLG_17                                                                                                         " & vbNewLine _
            & "    , cust.FLG_18                                                                                                         " & vbNewLine _
            & "    , cust.FLG_19                                                                                                         " & vbNewLine _
            & "    , cust.FLG_20                                                                                                         " & vbNewLine _
            & "    , s101kbn.KBN_NM1 AS S101_KBN_NM1                                                                                     " & vbNewLine _
            & "    , s101kbn.KBN_NM2 AS S101_KBN_NM2                                                                                     " & vbNewLine _
            & "    , s101kbn.KBN_NM3 AS S101_KBN_NM3                                                                                     " & vbNewLine _
            & "    , s101kbn.KBN_NM4 AS S101_KBN_NM4                                                                                     " & vbNewLine _
            & "    , s101kbn.KBN_NM5 AS S101_KBN_NM5                                                                                     " & vbNewLine _
            & "    , s101kbn.KBN_NM6 AS S101_KBN_NM6                                                                                     " & vbNewLine _
            & "    , s101kbn.KBN_NM7 AS S101_KBN_NM7                                                                                     " & vbNewLine _
            & "    , s101kbn.KBN_NM8 AS S101_KBN_NM8                                                                                     " & vbNewLine _
            & "    , s101kbn.KBN_NM9 AS S101_KBN_NM9                                                                                     " & vbNewLine _
            & "    , s101kbn.KBN_NM10 AS S101_KBN_NM10                                                                                   " & vbNewLine _
            & "FROM                                                                                                                      " & vbNewLine _
            & "    $LM_MST$..M_CUST_HANDY cust                                                                                           " & vbNewLine _
            & "LEFT OUTER JOIN                                                                                                           " & vbNewLine _
            & "    $LM_MST$..Z_KBN s101kbn                                                                                               " & vbNewLine _
            & "ON                                                                                                                        " & vbNewLine _
            & "        cust.GOODS_KANRI_NO_FORMAT_TYPE = s101kbn.KBN_CD                                                                    " & vbNewLine _
            & "    AND s101kbn.KBN_GROUP_CD = 'S101'                                                                                     " & vbNewLine _
            & "    AND s101kbn.SYS_DEL_FLG = '0'                                                                                         " & vbNewLine _
            & "WHERE                                                                                                                     " & vbNewLine _
            & "        cust.NRS_BR_CD = @NRS_BR_CD                                                                                       " & vbNewLine _
            & "    AND cust.CUST_CD_L = @CUST_CD_L                                                                                       " & vbNewLine _
            & "    AND cust.WH_CD = @WH_CD                                                                                               " & vbNewLine _
            & "    AND cust.SYS_DEL_FLG = '0'                                                                                            " & vbNewLine


#End Region

#Region "商品管理番号採番処理"

    Private Const SQL_UPDATE_GOODS_KANRI_NO As String = _
              "UPDATE                                      " & vbNewLine _
            & "    $LM_TRN$..D_ZAI_TRS                     " & vbNewLine _
            & "SET                                         " & vbNewLine _
            & "      GOODS_KANRI_NO = @GOODS_KANRI_NO      " & vbNewLine _
            & "    , SYS_UPD_PGID = @SYS_UPD_PGID          " & vbNewLine _
            & "    , SYS_UPD_USER = @SYS_UPD_USER          " & vbNewLine _
            & "    , SYS_UPD_DATE = @SYS_UPD_DATE          " & vbNewLine _
            & "    , SYS_UPD_TIME = @SYS_UPD_TIME          " & vbNewLine _
            & "WHERE                                       " & vbNewLine _
            & "        NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
            & "    AND ZAI_REC_NO = @ZAI_REC_NO            " & vbNewLine _
            & "    AND SYS_DEL_FLG = '0'                   " & vbNewLine _
            & "                                            " & vbNewLine

    Private Const SQL_SELECT_GOODS_KANRI_NO_SRC As String = _
          "SELECT                                           " & vbNewLine _
        & "    zai.NRS_BR_CD                                " & vbNewLine _
        & "    , zai.ZAI_REC_NO                             " & vbNewLine _
        & "    , goods.GOODS_CD_CUST                        " & vbNewLine _
        & "    , zai.LOT_NO                                 " & vbNewLine _
        & "    , zai.SERIAL_NO                              " & vbNewLine _
        & "    , zai.IRIME                                  " & vbNewLine _
        & "    , zai.GOODS_CRT_DATE                         " & vbNewLine _
        & "    , goods.JAN_CD                               " & vbNewLine _
        & "FROM                                             " & vbNewLine _
        & "    $LM_TRN$..C_OUTKA_L outl                     " & vbNewLine _
        & "LEFT OUTER JOIN                                  " & vbNewLine _
        & "    $LM_TRN$..C_OUTKA_M outm                     " & vbNewLine _
        & "ON                                               " & vbNewLine _
        & "        outl.NRS_BR_CD = outm.NRS_BR_CD          " & vbNewLine _
        & "    AND outl.OUTKA_NO_L = outm.OUTKA_NO_L        " & vbNewLine _
        & "    AND outm.SYS_DEL_FLG = '0'                   " & vbNewLine _
        & "LEFT OUTER JOIN                                  " & vbNewLine _
        & "    $LM_TRN$..C_OUTKA_S outs                     " & vbNewLine _
        & "ON                                               " & vbNewLine _
        & "        outm.NRS_BR_CD = outs.NRS_BR_CD          " & vbNewLine _
        & "    AND outm.OUTKA_NO_L = outs.OUTKA_NO_L        " & vbNewLine _
        & "    AND outm.OUTKA_NO_M = outs.OUTKA_NO_M        " & vbNewLine _
        & "    AND outs.SYS_DEL_FLG = '0'                   " & vbNewLine _
        & "LEFT OUTER JOIN                                  " & vbNewLine _
        & "    $LM_TRN$..D_ZAI_TRS zai                      " & vbNewLine _
        & "ON                                               " & vbNewLine _
        & "        outs.NRS_BR_CD = zai.NRS_BR_CD           " & vbNewLine _
        & "    AND outs.ZAI_REC_NO = zai.ZAI_REC_NO         " & vbNewLine _
        & "    AND zai.SYS_DEL_FLG = '0'                    " & vbNewLine _
        & "LEFT OUTER JOIN                                  " & vbNewLine _
        & "    $LM_MST$..M_GOODS goods                      " & vbNewLine _
        & "ON                                               " & vbNewLine _
        & "        zai.NRS_BR_CD = goods.NRS_BR_CD          " & vbNewLine _
        & "    AND zai.GOODS_CD_NRS = goods.GOODS_CD_NRS    " & vbNewLine _
        & "    AND goods.SYS_DEL_FLG = '0'                  " & vbNewLine _
        & "WHERE                                            " & vbNewLine _
        & "        outl.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
        & "    AND outl.OUTKA_NO_L = @OUTKA_NO_L            " & vbNewLine _
        & "    AND outl.SYS_DEL_FLG = '0'                   " & vbNewLine _
        & "    AND zai.GOODS_KANRI_NO = ''                  " & vbNewLine

#End Region

    '2014.04.10 (黎) PICK_WK削除 --ST--
#Region "PICK_WK削除"
    Private Const SQL_DELETE_PICK_WK As String = " DELETE                                 " & vbNewLine _
                                               & " FROM $LM_TRN$..C_OUTKA_PICK_WK         " & vbNewLine _
                                               & " WHERE                                  " & vbNewLine _
                                               & "      NRS_BR_CD  = @NRS_BR_CD   --@必須 " & vbNewLine _
                                               & "  AND OUTKA_NO_L = @OUTKA_NO_L  --@必須 " & vbNewLine _
                                               & "  AND OUTKA_NO_M = @OUTKA_NO_M  --@必須 " & vbNewLine

#End Region
    '2014.04.10 (黎) PICK_WK削除 --ED--

#End Region

    ' CALT対応 2014.04.24 黎 追加 --ST--
#Region "CALT対応"
#Region "Select"
#Region "キャンセル抽出"
    ''' <summary>
    ''' 出荷LMSキャンセルデータ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKA_LMS_CANCEL As String = _
             "SELECT                                                                 " & vbNewLine _
           & "     CODS.NRS_BR_CD                             AS NRS_BR_CD           " & vbNewLine _
           & "    ,CODS.OUTKA_NO_L                            AS OUTKA_NO_L          " & vbNewLine _
           & "    ,CODS.OUTKA_NO_M                            AS OUTKA_NO_M          " & vbNewLine _
           & "    ,CODS.OUTKA_NO_S                            AS OUTKA_NO_S          " & vbNewLine _
           & "    ,CODS.SEND_SEQ + 1                          AS SEND_SEQ            " & vbNewLine _
           & "    ,'2'                                        AS DATA_KBN            " & vbNewLine _
           & "    ,CODS.WH_CD                                 AS WH_CD               " & vbNewLine _
           & "    ,CODS.CUST_CD_L                             AS CUST_CD_L           " & vbNewLine _
           & "    ,CODS.CUST_CD_M                             AS CUST_CD_M           " & vbNewLine _
           & "    ,CODS.CUST_NM_L                             AS CUST_NM_L           " & vbNewLine _
           & "    ,CODS.OUTKA_PLAN_DATE                       AS OUTKA_PLAN_DATE     " & vbNewLine _
           & "    ,CODS.ARR_PLAN_DATE                         AS ARR_PLAN_DATE       " & vbNewLine _
           & "    ,CODS.DEST_CD                               AS DEST_CD             " & vbNewLine _
           & "    ,CODS.DEST_NM                               AS DEST_NM             " & vbNewLine _
           & "    ,CODS.DEST_AD_1                             AS DEST_AD_1           " & vbNewLine _
           & "    ,CODS.DEST_AD_2                             AS DEST_AD_2           " & vbNewLine _
           & "    ,CODS.DEST_AD_3                             AS DEST_AD_3           " & vbNewLine _
           & "    ,CODS.DEST_TEL                              AS DEST_TEL            " & vbNewLine _
           & "    ,CODS.ZIP                                   AS ZIP                 " & vbNewLine _
           & "    ,CODS.CUST_ORD_NO                           AS CUST_ORD_NO         " & vbNewLine _
           & "    ,CODS.BUYER_ORD_NO                          AS BUYER_ORD_NO        " & vbNewLine _
           & "    ,CODS.REMARK_L                              AS REMARK_L            " & vbNewLine _
           & "    ,CODS.GOODS_CD_NRS                          AS GOODS_CD_NRS        " & vbNewLine _
           & "    ,CODS.GOODS_CD_CUST                         AS GOODS_CD_CUST       " & vbNewLine _
           & "    ,CODS.GOODS_NM_1                            AS GOODS_NM_1          " & vbNewLine _
           & "    ,CODS.REMARK_M                              AS REMARK_M            " & vbNewLine _
           & "    ,CODS.LOT_NO                                AS LOT_NO              " & vbNewLine _
           & "    ,CODS.SERIAL_NO                             AS SERIAL_NO           " & vbNewLine _
           & "    ,CODS.OUTKA_NB                              AS OUTKA_NB            " & vbNewLine _
           & "    ,CODS.OUTKA_QT                              AS OUTKA_QT            " & vbNewLine _
           & "    ,CODS.OUTKA_TTL_NB                          AS OUTKA_TTL_NB        " & vbNewLine _
           & "    ,CODS.OUTKA_TTL_QT                          AS OUTKA_TTL_QT        " & vbNewLine _
           & "    ,CODS.ALCTD_NB                              AS ALCTD_NB            " & vbNewLine _
           & "    ,CODS.ALCTD_QT                              AS ALCTD_QT            " & vbNewLine _
           & "    ,CODS.IRIME                                 AS IRIME               " & vbNewLine _
           & "    ,CODS.BETU_WT                               AS BETU_WT             " & vbNewLine _
           & "    ,CODS.REMARK_S                              AS REMARK_S            " & vbNewLine _
           & "    ,CODS.PKG_NB                                AS PKG_NB              " & vbNewLine _
           & "    ,CODS.PKG_UT                                AS PKG_UT              " & vbNewLine _
           & "    ,CODS.ZAI_REC_NO                            AS ZAI_REC_NO          " & vbNewLine _
           & "    ,CODS.GOODS_COND_KB_1                       AS GOODS_COND_KB_1     " & vbNewLine _
           & "    ,CODS.GOODS_COND_KB_2                       AS GOODS_COND_KB_2     " & vbNewLine _
           & "    ,CODS.GOODS_COND_KB_3                       AS GOODS_COND_KB_3     " & vbNewLine _
           & "    ,CODS.GOODS_CRT_DATE                        AS GOODS_CRT_DATE      " & vbNewLine _
           & "    ,CODS.LT_DATE                               AS LT_DATE             " & vbNewLine _
           & "    ,CODS.SPD_KB                                AS SPD_KB              " & vbNewLine _
           & "    ,CODS.OFB_KB                                AS OFB_KB              " & vbNewLine _
           & "    ,CODS.DEST_CD_P                             AS DEST_CD_P           " & vbNewLine _
           & "    ,'2'                                        AS SEND_SHORI_FLG      " & vbNewLine _
           & "    ,CODS.SEND_USER                             AS SEND_USER           " & vbNewLine _
           & "    ,CODS.SEND_DATE                             AS SEND_DATE           " & vbNewLine _
           & "    ,CODS.SEND_TIME                             AS SEND_TIME           " & vbNewLine _
           & "    ,CODS.SYS_ENT_DATE                          AS SYS_ENT_DATE        " & vbNewLine _
           & "    ,CODS.SYS_ENT_TIME                          AS SYS_ENT_TIME        " & vbNewLine _
           & "    ,CODS.SYS_ENT_PGID                          AS SYS_ENT_PGID        " & vbNewLine _
           & "    ,CODS.SYS_ENT_USER                          AS SYS_ENT_USER        " & vbNewLine _
           & "    ,CODS.SYS_UPD_DATE                          AS SYS_UPD_DATE        " & vbNewLine _
           & "    ,CODS.SYS_UPD_TIME                          AS SYS_UPD_TIME        " & vbNewLine _
           & "    ,CODS.SYS_UPD_PGID                          AS SYS_UPD_PGID        " & vbNewLine _
           & "    ,CODS.SYS_UPD_USER                          AS SYS_UPD_USER        " & vbNewLine _
           & "    ,'1'                                        AS SYS_DEL_FLG         " & vbNewLine _
           & " FROM $LM_TRN$..C_OUTKA_DIRECT_SEND             AS CODS                " & vbNewLine _
           & " LEFT JOIN                                                             " & vbNewLine _
           & "     (                                                                 " & vbNewLine _
           & "       SELECT                                                          " & vbNewLine _
           & "             CODS_IN2.NRS_BR_CD                 AS NRS_BR_CD           " & vbNewLine _
           & "            ,CODS_IN2.OUTKA_NO_L                AS OUTKA_NO_L          " & vbNewLine _
           & "            ,CODS_IN2.OUTKA_NO_M                AS OUTKA_NO_M          " & vbNewLine _
           & "            ,CODS_IN2.OUTKA_NO_S                AS OUTKA_NO_S          " & vbNewLine _
           & "            ,MAX(GET_SEQ.SEND_SEQ)              AS SEND_SEQ            " & vbNewLine _
           & "       FROM $LM_TRN$..C_OUTKA_DIRECT_SEND       AS CODS_IN2            " & vbNewLine _
           & "       LEFT JOIN                                                       " & vbNewLine _
           & "           (                                                           " & vbNewLine _
           & "             SELECT                                                    " & vbNewLine _
           & "                   CODS_IN1.NRS_BR_CD              AS NRS_BR_CD        " & vbNewLine _
           & "                  ,CODS_IN1.OUTKA_NO_L             AS OUTKA_NO_L       " & vbNewLine _
           & "                  ,CODS_IN1.OUTKA_NO_M             AS OUTKA_NO_M       " & vbNewLine _
           & "                  ,CODS_IN1.OUTKA_NO_S             AS OUTKA_NO_S       " & vbNewLine _
           & "                  ,CODS_IN1.SEND_SEQ               AS SEND_SEQ         " & vbNewLine _
           & "             FROM $LM_TRN$..C_OUTKA_DIRECT_SEND    AS CODS_IN1         " & vbNewLine _
           & "             WHERE                                                     " & vbNewLine _
           & "                   CODS_IN1.SYS_DEL_FLG             = '0'              " & vbNewLine _
           & "               AND CODS_IN1.NRS_BR_CD               = @NRS_BR_CD   --@ " & vbNewLine _
           & "               AND CODS_IN1.OUTKA_NO_L              = @OUTKA_NO_L  --@ " & vbNewLine _
           & "               AND CODS_IN1.DATA_KBN               <> '2'              " & vbNewLine _
           & "             GROUP BY                                                  " & vbNewLine _
           & "                   CODS_IN1.NRS_BR_CD                                  " & vbNewLine _
           & "                  ,CODS_IN1.OUTKA_NO_L                                 " & vbNewLine _
           & "                  ,CODS_IN1.OUTKA_NO_M                                 " & vbNewLine _
           & "                  ,CODS_IN1.OUTKA_NO_S                                 " & vbNewLine _
           & "                  ,CODS_IN1.SEND_SEQ                                   " & vbNewLine _
           & "            ) AS GET_SEQ                                               " & vbNewLine _
           & "          ON CODS_IN2.NRS_BR_CD              = GET_SEQ.NRS_BR_CD       " & vbNewLine _
           & "         AND CODS_IN2.OUTKA_NO_L             = GET_SEQ.OUTKA_NO_L      " & vbNewLine _
           & "         AND CODS_IN2.OUTKA_NO_M             = GET_SEQ.OUTKA_NO_M      " & vbNewLine _
           & "         AND CODS_IN2.OUTKA_NO_S             = GET_SEQ.OUTKA_NO_S      " & vbNewLine _
           & "       WHERE                                                           " & vbNewLine _
           & "             CODS_IN2.SYS_DEL_FLG            = '0'                     " & vbNewLine _
           & "         AND CODS_IN2.NRS_BR_CD              = @NRS_BR_CD   --@        " & vbNewLine _
           & "         AND CODS_IN2.OUTKA_NO_L             = @OUTKA_NO_L  --@        " & vbNewLine _
           & "         AND CODS_IN2.DATA_KBN              <> '2'                     " & vbNewLine _
           & "       GROUP BY                                                        " & vbNewLine _
           & "             CODS_IN2.NRS_BR_CD                                        " & vbNewLine _
           & "            ,CODS_IN2.OUTKA_NO_L                                       " & vbNewLine _
           & "            ,CODS_IN2.OUTKA_NO_M                                       " & vbNewLine _
           & "            ,CODS_IN2.OUTKA_NO_S                                       " & vbNewLine _
           & "     ) AS MAX_SEQ                                                      " & vbNewLine _
           & "   ON CODS.NRS_BR_CD    = MAX_SEQ.NRS_BR_CD                            " & vbNewLine _
           & "  AND CODS.OUTKA_NO_L    = MAX_SEQ.OUTKA_NO_L                          " & vbNewLine _
           & "  AND CODS.OUTKA_NO_M    = MAX_SEQ.OUTKA_NO_M                          " & vbNewLine _
           & "  AND CODS.OUTKA_NO_S    = MAX_SEQ.OUTKA_NO_S                          " & vbNewLine _
           & "  AND CODS.SEND_SEQ     = MAX_SEQ.SEND_SEQ                             " & vbNewLine _
           & " LEFT JOIN                                                             " & vbNewLine _
           & "     (                                                                 " & vbNewLine _
           & "       SELECT                                                          " & vbNewLine _
           & "             CODS_IN3.NRS_BR_CD             AS NRS_BR_CD               " & vbNewLine _
           & "            ,CODS_IN3.OUTKA_NO_L            AS OUTKA_NO_L              " & vbNewLine _
           & "            ,CODS_IN3.OUTKA_NO_M            AS OUTKA_NO_M              " & vbNewLine _
           & "            ,CODS_IN3.OUTKA_NO_S            AS OUTKA_NO_S              " & vbNewLine _
           & "            ,CONVERT(INTEGER,MAX(CODS_IN3.SEND_SEQ)) + 1  AS SEND_SEQ  " & vbNewLine _
           & "       FROM $LM_TRN$..C_OUTKA_DIRECT_SEND   AS CODS_IN3                " & vbNewLine _
           & "       WHERE                                                           " & vbNewLine _
           & "             CODS_IN3.NRS_BR_CD              = @NRS_BR_CD  --@         " & vbNewLine _
           & "         AND CODS_IN3.OUTKA_NO_L             = @OUTKA_NO_L --@         " & vbNewLine _
           & "       GROUP BY                                                        " & vbNewLine _
           & "             CODS_IN3.NRS_BR_CD                                        " & vbNewLine _
           & "            ,CODS_IN3.OUTKA_NO_L                                       " & vbNewLine _
           & "            ,CODS_IN3.OUTKA_NO_M                                       " & vbNewLine _
           & "            ,CODS_IN3.OUTKA_NO_S                                       " & vbNewLine _
           & "     )                          AS GET_MAX_SEQ                         " & vbNewLine _
           & "   ON CODS.NRS_BR_CD             = GET_MAX_SEQ.NRS_BR_CD               " & vbNewLine _
           & "  AND CODS.OUTKA_NO_L            = GET_MAX_SEQ.OUTKA_NO_L              " & vbNewLine _
           & "  AND CODS.OUTKA_NO_M            = GET_MAX_SEQ.OUTKA_NO_M              " & vbNewLine _
           & "  AND CODS.OUTKA_NO_S            = GET_MAX_SEQ.OUTKA_NO_S              " & vbNewLine _
           & "  AND (CODS.SEND_SEQ +1)         = GET_MAX_SEQ.SEND_SEQ                " & vbNewLine _
           & " WHERE                                                                 " & vbNewLine _
           & "      CODS.SYS_DEL_FLG           = '0'                                 " & vbNewLine _
           & "  AND CODS.NRS_BR_CD             = @NRS_BR_CD   --@                    " & vbNewLine _
           & "  AND CODS.OUTKA_NO_L            = @OUTKA_NO_L  --@                    " & vbNewLine _
           & "  AND GET_MAX_SEQ.SEND_SEQ      IS NOT NULL                            " & vbNewLine _
           & "  AND MAX_SEQ.OUTKA_NO_L        IS NOT NULL                            " & vbNewLine

    ''' <summary>
    ''' 出荷指示キャンセルデータオーダバイ句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_OUTKA_LMS_CANCEL_ORDER_BY As String = _
             " ORDER BY                                                              " & vbNewLine _
           & "      CODS.NRS_BR_CD  ASC                                              " & vbNewLine _
           & "     ,CODS.OUTKA_NO_L ASC                                              " & vbNewLine _
           & "     ,CODS.OUTKA_NO_M ASC                                              " & vbNewLine _
           & "     ,CODS.OUTKA_NO_S ASC                                              " & vbNewLine _
           & "     ,CODS.SEND_SEQ   ASC                                              " & vbNewLine

#End Region

#Region "倉庫チェック"
    ''' <summary>
    ''' 倉庫チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WH_CD_EXIST As String = _
            " SELECT                                  " & vbNewLine _
          & "       COUNT(1)         AS REC_CNT       " & vbNewLine _
          & " FROM $LM_MST$..Z_KBN   AS S102          " & vbNewLine _
          & " WHERE                                   " & vbNewLine _
          & "      S102.KBN_GROUP_CD = 'S102'         " & vbNewLine _
          & "  AND S102.KBN_NM1      = @NRS_BR_CD --@ " & vbNewLine _
          & "  AND S102.KBN_NM2      = @WH_CD     --@ " & vbNewLine

#End Region

#End Region

#Region "Insert"
    ''' <summary>
    ''' キャンセルデータ作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_DIRECT_SEND As String = _
      "INSERT INTO $LM_TRN$..C_OUTKA_DIRECT_SEND  " & vbNewLine _
    & "       (                                   " & vbNewLine _
    & "         NRS_BR_CD                         " & vbNewLine _
    & "        ,OUTKA_NO_L                        " & vbNewLine _
    & "        ,OUTKA_NO_M                        " & vbNewLine _
    & "        ,OUTKA_NO_S                        " & vbNewLine _
    & "        ,SEND_SEQ                          " & vbNewLine _
    & "        ,DATA_KBN                          " & vbNewLine _
    & "        ,WH_CD                             " & vbNewLine _
    & "        ,CUST_CD_L                         " & vbNewLine _
    & "        ,CUST_CD_M                         " & vbNewLine _
    & "        ,CUST_NM_L                         " & vbNewLine _
    & "        ,OUTKA_PLAN_DATE                   " & vbNewLine _
    & "        ,ARR_PLAN_DATE                     " & vbNewLine _
    & "        ,DEST_CD                           " & vbNewLine _
    & "        ,DEST_NM                           " & vbNewLine _
    & "        ,DEST_AD_1                         " & vbNewLine _
    & "        ,DEST_AD_2                         " & vbNewLine _
    & "        ,DEST_AD_3                         " & vbNewLine _
    & "        ,DEST_TEL                          " & vbNewLine _
    & "        ,ZIP                               " & vbNewLine _
    & "        ,CUST_ORD_NO                       " & vbNewLine _
    & "        ,BUYER_ORD_NO                      " & vbNewLine _
    & "        ,REMARK_L                          " & vbNewLine _
    & "        ,GOODS_CD_NRS                      " & vbNewLine _
    & "        ,GOODS_CD_CUST                     " & vbNewLine _
    & "        ,GOODS_NM_1                        " & vbNewLine _
    & "        ,REMARK_M                          " & vbNewLine _
    & "        ,LOT_NO                            " & vbNewLine _
    & "        ,SERIAL_NO                         " & vbNewLine _
    & "        ,OUTKA_NB                          " & vbNewLine _
    & "        ,OUTKA_QT                          " & vbNewLine _
    & "        ,OUTKA_TTL_NB                      " & vbNewLine _
    & "        ,OUTKA_TTL_QT                      " & vbNewLine _
    & "        ,ALCTD_NB                          " & vbNewLine _
    & "        ,ALCTD_QT                          " & vbNewLine _
    & "        ,IRIME                             " & vbNewLine _
    & "        ,BETU_WT                           " & vbNewLine _
    & "        ,REMARK_S                          " & vbNewLine _
    & "        ,PKG_NB                            " & vbNewLine _
    & "        ,PKG_UT                            " & vbNewLine _
    & "        ,ZAI_REC_NO                        " & vbNewLine _
    & "        ,GOODS_COND_KB_1                   " & vbNewLine _
    & "        ,GOODS_COND_KB_2                   " & vbNewLine _
    & "        ,GOODS_COND_KB_3                   " & vbNewLine _
    & "        ,GOODS_CRT_DATE                    " & vbNewLine _
    & "        ,LT_DATE                           " & vbNewLine _
    & "        ,SPD_KB                            " & vbNewLine _
    & "        ,OFB_KB                            " & vbNewLine _
    & "        ,DEST_CD_P                         " & vbNewLine _
    & "        ,SEND_SHORI_FLG                    " & vbNewLine _
    & "        ,SYS_ENT_DATE                      " & vbNewLine _
    & "        ,SYS_ENT_TIME                      " & vbNewLine _
    & "        ,SYS_ENT_PGID                      " & vbNewLine _
    & "        ,SYS_ENT_USER                      " & vbNewLine _
    & "        ,SYS_UPD_DATE                      " & vbNewLine _
    & "        ,SYS_UPD_TIME                      " & vbNewLine _
    & "        ,SYS_UPD_PGID                      " & vbNewLine _
    & "        ,SYS_UPD_USER                      " & vbNewLine _
    & "        ,SYS_DEL_FLG                       " & vbNewLine _
    & "       )                                   " & vbNewLine _
    & " VALUES(                                   " & vbNewLine _
    & "         @NRS_BR_CD                        " & vbNewLine _
    & "        ,@OUTKA_NO_L                       " & vbNewLine _
    & "        ,@OUTKA_NO_M                       " & vbNewLine _
    & "        ,@OUTKA_NO_S                       " & vbNewLine _
    & "        ,@SEND_SEQ                         " & vbNewLine _
    & "        ,@DATA_KBN                         " & vbNewLine _
    & "        ,@WH_CD                            " & vbNewLine _
    & "        ,@CUST_CD_L                        " & vbNewLine _
    & "        ,@CUST_CD_M                        " & vbNewLine _
    & "        ,@CUST_NM_L                        " & vbNewLine _
    & "        ,@OUTKA_PLAN_DATE                  " & vbNewLine _
    & "        ,@ARR_PLAN_DATE                    " & vbNewLine _
    & "        ,@DEST_CD                          " & vbNewLine _
    & "        ,@DEST_NM                          " & vbNewLine _
    & "        ,@DEST_AD_1                        " & vbNewLine _
    & "        ,@DEST_AD_2                        " & vbNewLine _
    & "        ,@DEST_AD_3                        " & vbNewLine _
    & "        ,@DEST_TEL                         " & vbNewLine _
    & "        ,@ZIP                              " & vbNewLine _
    & "        ,@CUST_ORD_NO                      " & vbNewLine _
    & "        ,@BUYER_ORD_NO                     " & vbNewLine _
    & "        ,@REMARK_L                         " & vbNewLine _
    & "        ,@GOODS_CD_NRS                     " & vbNewLine _
    & "        ,@GOODS_CD_CUST                    " & vbNewLine _
    & "        ,@GOODS_NM_1                       " & vbNewLine _
    & "        ,@REMARK_M                         " & vbNewLine _
    & "        ,@LOT_NO                           " & vbNewLine _
    & "        ,@SERIAL_NO                        " & vbNewLine _
    & "        ,@OUTKA_NB                         " & vbNewLine _
    & "        ,@OUTKA_QT                         " & vbNewLine _
    & "        ,@OUTKA_TTL_NB                     " & vbNewLine _
    & "        ,@OUTKA_TTL_QT                     " & vbNewLine _
    & "        ,@ALCTD_NB                         " & vbNewLine _
    & "        ,@ALCTD_QT                         " & vbNewLine _
    & "        ,@IRIME                            " & vbNewLine _
    & "        ,@BETU_WT                          " & vbNewLine _
    & "        ,@REMARK_S                         " & vbNewLine _
    & "        ,@PKG_NB                           " & vbNewLine _
    & "        ,@PKG_UT                           " & vbNewLine _
    & "        ,@ZAI_REC_NO                       " & vbNewLine _
    & "        ,@GOODS_COND_KB_1                  " & vbNewLine _
    & "        ,@GOODS_COND_KB_2                  " & vbNewLine _
    & "        ,@GOODS_COND_KB_3                  " & vbNewLine _
    & "        ,@GOODS_CRT_DATE                   " & vbNewLine _
    & "        ,@LT_DATE                          " & vbNewLine _
    & "        ,@SPD_KB                           " & vbNewLine _
    & "        ,@OFB_KB                           " & vbNewLine _
    & "        ,@DEST_CD_P                        " & vbNewLine _
    & "        ,@SEND_SHORI_FLG                   " & vbNewLine _
    & "        ,@SYS_DATE                         " & vbNewLine _
    & "        ,@SYS_TIME                         " & vbNewLine _
    & "        ,@SYS_PGID                         " & vbNewLine _
    & "        ,@SYS_USER                         " & vbNewLine _
    & "        ,@SYS_DATE                         " & vbNewLine _
    & "        ,@SYS_TIME                         " & vbNewLine _
    & "        ,@SYS_PGID                         " & vbNewLine _
    & "        ,@SYS_USER                         " & vbNewLine _
    & "        ,@SYS_DEL_FLG                      " & vbNewLine _
    & "       )                                   " & vbNewLine

#End Region

#End Region
    ' CALT対応 2014.04.24 黎 追加 --ST--

    '2018/12/07 ADD START 要望管理002171
#Region "出荷梱包個数自動計算用SQL"

    ''' <summary>
    ''' 自動計算に必要なマスタの設定エラー件数取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CALC_MST_ERR_CNT_SAKURA As String = _
            "SELECT SUM(KB68ERR) + SUM(KB69ERR) + SUM(KB70ERR) AS MST_ERR_CNT   " & vbNewLine _
          & "FROM (                                                             " & vbNewLine _
          & "                                                                   " & vbNewLine _
          & "SELECT OUTKA_M.GOODS_CD_NRS                                        " & vbNewLine _
          & "      --指数を表す'e1'を付加することでより厳密な数値チェックを行う " & vbNewLine _
          & "      ,CASE WHEN ISNUMERIC(MGD68.SET_NAIYO + 'e1') = 1             " & vbNewLine _
          & "       THEN 0                                                      " & vbNewLine _
          & "       ELSE 1                                                      " & vbNewLine _
          & "       END AS KB68ERR                                              " & vbNewLine _
          & "      ,CASE WHEN ISNUMERIC(MGD69.SET_NAIYO + 'e1') = 1             " & vbNewLine _
          & "       THEN 0                                                      " & vbNewLine _
          & "       ELSE 1                                                      " & vbNewLine _
          & "       END AS KB69ERR                                              " & vbNewLine _
          & "      ,CASE WHEN ISNUMERIC(MGD70.SET_NAIYO + 'e1') = 1             " & vbNewLine _
          & "       THEN 0                                                      " & vbNewLine _
          & "       ELSE 1                                                      " & vbNewLine _
          & "       END AS KB70ERR                                              " & vbNewLine _
          & "  FROM $LM_TRN$..C_OUTKA_M OUTKA_M                                 " & vbNewLine _
          & "  LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD68                        " & vbNewLine _
          & "    ON MGD68.NRS_BR_CD    = OUTKA_M.NRS_BR_CD                      " & vbNewLine _
          & "   AND MGD68.GOODS_CD_NRS = OUTKA_M.GOODS_CD_NRS                   " & vbNewLine _
          & "   AND MGD68.SUB_KB = '68'                                         " & vbNewLine _
          & "   AND MGD68.SYS_DEL_FLG = '0'                                     " & vbNewLine _
          & "  LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD69                        " & vbNewLine _
          & "    ON MGD69.NRS_BR_CD    = OUTKA_M.NRS_BR_CD                      " & vbNewLine _
          & "   AND MGD69.GOODS_CD_NRS = OUTKA_M.GOODS_CD_NRS                   " & vbNewLine _
          & "   AND MGD69.SUB_KB = '69'                                         " & vbNewLine _
          & "   AND MGD69.SYS_DEL_FLG = '0'                                     " & vbNewLine _
          & "  LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD70                        " & vbNewLine _
          & "    ON MGD70.NRS_BR_CD    = OUTKA_M.NRS_BR_CD                      " & vbNewLine _
          & "   AND MGD70.GOODS_CD_NRS = OUTKA_M.GOODS_CD_NRS                   " & vbNewLine _
          & "   AND MGD70.SUB_KB = '70'                                         " & vbNewLine _
          & "   AND MGD70.SYS_DEL_FLG = '0'                                     " & vbNewLine _
          & " WHERE OUTKA_M.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
          & "   AND OUTKA_M.OUTKA_NO_L = @OUTKA_NO_L                            " & vbNewLine _
          & "   AND OUTKA_M.SYS_DEL_FLG = '0'                                   " & vbNewLine _
          & "                                                                   " & vbNewLine _
          & ") MST                                                              " & vbNewLine

    ''' <summary>
    ''' サクラ出荷梱包個数自動計算用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CALC_OUTKAPKGNB_SAKURA As String = _
            "SELECT SUM(OUTKA_M.FIX_PKG_CNT) + SUM(OUTKA_M.FULL_PKG_CNT) + CEILING(SUM(OUTKA_M.HASU_PKG_CNT)) AS OUTKA_PKG_NB                                               " & vbNewLine _
          & "FROM (                                                                                                                                                         " & vbNewLine _
          & "                                                                                                                                                               " & vbNewLine _
          & "SELECT OUTKA_M.OUTKA_NO_L                                                                                                                                      " & vbNewLine _
          & "      ,OUTKA_M.GOODS_CD_NRS                                                                                                                                    " & vbNewLine _
          & "      ,MGD68.SET_NAIYO AS MAX1PKG                                                                                                                              " & vbNewLine _
          & "      ,FLOOR(CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO)) AS MAX1PKG_INT                                                                                            " & vbNewLine _
          & "      ,CEILING((1 / CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO)) * 100 ) / 100 AS GOODS_SIZE                                                                        " & vbNewLine _
          & "      ,MGD69.SET_NAIYO AS KONSAI_FLG                                                                                                                           " & vbNewLine _
          & "      ,MGD70.SET_NAIYO AS HASU_FLG                                                                                                                             " & vbNewLine _
          & "      ,OUTKA_M.OUTKA_TTL_NB                                                                                                                                    " & vbNewLine _
          & "      ,CASE WHEN MGD70.SET_NAIYO = '1'                                                                                                                         " & vbNewLine _
          & "       THEN FLOOR(OUTKA_M.OUTKA_TTL_NB / FLOOR(CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO))) + OUTKA_M.OUTKA_TTL_NB % FLOOR(CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO)) " & vbNewLine _
          & "       ELSE CASE WHEN MGD69.SET_NAIYO <> '1'                                                                                                                   " & vbNewLine _
          & "            THEN CEILING(OUTKA_M.OUTKA_TTL_NB / FLOOR(CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO)))                                                                 " & vbNewLine _
          & "            ELSE 0                                                                                                                                             " & vbNewLine _
          & "            END                                                                                                                                                " & vbNewLine _
          & "       END AS FIX_PKG_CNT                                                                                                                                      " & vbNewLine _
          & "      ,CASE WHEN MGD70.SET_NAIYO <> '1' AND MGD69.SET_NAIYO = '1'                                                                                              " & vbNewLine _
          & "       THEN FLOOR(OUTKA_M.OUTKA_TTL_NB / FLOOR(CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO)))                                                                        " & vbNewLine _
          & "       ELSE 0                                                                                                                                                  " & vbNewLine _
          & "       END AS FULL_PKG_CNT                                                                                                                                     " & vbNewLine _
          & "      ,CASE WHEN MGD70.SET_NAIYO <> '1' AND MGD69.SET_NAIYO = '1'                                                                                              " & vbNewLine _
          & "             AND OUTKA_M.OUTKA_TTL_NB % FLOOR(CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO)) > 0                                                                      " & vbNewLine _
          & "       THEN 1                                                                                                                                                  " & vbNewLine _
          & "       ELSE 0                                                                                                                                                  " & vbNewLine _
          & "       END AS HASU_UMU                                                                                                                                         " & vbNewLine _
          & "      ,CASE WHEN MGD70.SET_NAIYO <> '1' AND MGD69.SET_NAIYO = '1'                                                                                              " & vbNewLine _
          & "       THEN CEILING(OUTKA_M.OUTKA_TTL_NB % FLOOR(CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO)) / CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO) * 100) / 100                 " & vbNewLine _
          & "       ELSE 0                                                                                                                                                  " & vbNewLine _
          & "       END AS HASU_PKG_CNT                                                                                                                                     " & vbNewLine _
          & "      ,CASE WHEN MGD70.SET_NAIYO <> '1' AND MGD69.SET_NAIYO = '1'                                                                                              " & vbNewLine _
          & "       THEN OUTKA_M.OUTKA_TTL_NB % FLOOR(CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO))                                                                               " & vbNewLine _
          & "       ELSE 0                                                                                                                                                  " & vbNewLine _
          & "       END AS HASU_GOODS_CNT                                                                                                                                   " & vbNewLine _
          & "      ,CASE WHEN MGD70.SET_NAIYO <> '1' AND MGD69.SET_NAIYO = '1'                                                                                              " & vbNewLine _
          & "       THEN CEILING((1 - FLOOR(CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO)) / CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO)) * 100) / 100                                  " & vbNewLine _
          & "       ELSE 0                                                                                                                                                  " & vbNewLine _
          & "       END AS FULL_PKG_SPACE                                                                                                                                   " & vbNewLine _
          & "      ,CASE WHEN MGD70.SET_NAIYO <> '1' AND MGD69.SET_NAIYO = '1'                                                                                              " & vbNewLine _
          & "             AND OUTKA_M.OUTKA_TTL_NB % FLOOR(CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO)) > 0                                                                      " & vbNewLine _
          & "       THEN 1 - (CEILING(OUTKA_M.OUTKA_TTL_NB % FLOOR(CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO)) / CONVERT(DECIMAL(10,5),MGD68.SET_NAIYO) * 100) / 100)           " & vbNewLine _
          & "       ELSE 0                                                                                                                                                  " & vbNewLine _
          & "       END AS HASU_PKG_SPACE                                                                                                                                   " & vbNewLine _
          & "  FROM $LM_TRN$..C_OUTKA_M OUTKA_M                                                                                                                             " & vbNewLine _
          & "  LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD68                                                                                                                    " & vbNewLine _
          & "    ON MGD68.NRS_BR_CD    = OUTKA_M.NRS_BR_CD                                                                                                                  " & vbNewLine _
          & "   AND MGD68.GOODS_CD_NRS = OUTKA_M.GOODS_CD_NRS                                                                                                               " & vbNewLine _
          & "   AND MGD68.SUB_KB = '68'                                                                                                                                     " & vbNewLine _
          & "   AND MGD68.SYS_DEL_FLG = '0'                                                                                                                                 " & vbNewLine _
          & "  LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD69                                                                                                                    " & vbNewLine _
          & "    ON MGD69.NRS_BR_CD    = OUTKA_M.NRS_BR_CD                                                                                                                  " & vbNewLine _
          & "   AND MGD69.GOODS_CD_NRS = OUTKA_M.GOODS_CD_NRS                                                                                                               " & vbNewLine _
          & "   AND MGD69.SUB_KB = '69'                                                                                                                                     " & vbNewLine _
          & "   AND MGD69.SYS_DEL_FLG = '0'                                                                                                                                 " & vbNewLine _
          & "  LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD70                                                                                                                    " & vbNewLine _
          & "    ON MGD70.NRS_BR_CD    = OUTKA_M.NRS_BR_CD                                                                                                                  " & vbNewLine _
          & "   AND MGD70.GOODS_CD_NRS = OUTKA_M.GOODS_CD_NRS                                                                                                               " & vbNewLine _
          & "   AND MGD70.SUB_KB = '70'                                                                                                                                     " & vbNewLine _
          & "   AND MGD70.SYS_DEL_FLG = '0'                                                                                                                                 " & vbNewLine _
          & "                                                                                                                                                               " & vbNewLine _
          & " WHERE OUTKA_M.NRS_BR_CD = @NRS_BR_CD                                                                                                                          " & vbNewLine _
          & "   AND OUTKA_M.OUTKA_NO_L = @OUTKA_NO_L                                                                                                                        " & vbNewLine _
          & "                                                                                                                                                               " & vbNewLine _
          & ") OUTKA_M                                                                                                                                                      " & vbNewLine

    ''' <summary>
    ''' C_OUTKA_LのUPDATE文(出荷梱包個数自動計算用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKAL_OUTKAPKGNB As String = _
            "UPDATE $LM_TRN$..C_OUTKA_L        " & vbNewLine _
          & "SET OUTKA_PKG_NB  = @OUTKA_PKG_NB " & vbNewLine _
          & "   ,SYS_UPD_DATE  = @SYS_UPD_DATE " & vbNewLine _
          & "   ,SYS_UPD_TIME  = @SYS_UPD_TIME " & vbNewLine _
          & "   ,SYS_UPD_PGID  = @SYS_UPD_PGID " & vbNewLine _
          & "   ,SYS_UPD_USER  = @SYS_UPD_USER " & vbNewLine _
          & "WHERE NRS_BR_CD   = @NRS_BR_CD    " & vbNewLine _
          & "  AND OUTKA_NO_L  = @OUTKA_NO_L   " & vbNewLine _
          & "  AND SYS_DEL_FLG = '0'           " & vbNewLine

#End Region
    '2018/12/07 ADD END   要望管理002171

#Region "タブレット対応"

    Private Const SQL_IS_WH_TAN_NRS_BR_CD As String = _
    "SELECT                                     " & vbNewLine & _
    "    ISNULL(COUNT(*), 0) AS CNT             " & vbNewLine & _
    "FROM $LM_MST$..Z_KBN                       " & vbNewLine & _
    "WHERE KBN_GROUP_CD = 'B007'                " & vbNewLine & _
    "AND VALUE1 = 1                             " & vbNewLine & _
    "AND KBN_CD = @NRS_BR_CD                    " & vbNewLine

#End Region

#End Region

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 出荷データ（大）取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectOutkaLData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "OUTKA_NO_L" _
                                            , "FURI_NO" _
                                            , "OUTKA_KB" _
                                            , "SYUBETU_KB" _
                                            , "OUTKA_STATE_KB" _
                                            , "OUTKAHOKOKU_YN" _
                                            , "PICK_KB" _
                                            , "DENP_NO" _
                                            , "WH_CD" _
                                            , "OUTKA_PLAN_DATE" _
                                            , "OUTKO_DATE" _
                                            , "ARR_PLAN_DATE" _
                                            , "ARR_PLAN_TIME" _
                                            , "HOKOKU_DATE" _
                                            , "TOUKI_HOKAN_YN" _
                                            , "END_DATE" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "SHIP_CD_L" _
                                            , "DEST_CD" _
                                            , "DEST_AD_3" _
                                            , "DEST_TEL" _
                                            , "NHS_REMARK" _
                                            , "SP_NHS_KB" _
                                            , "COA_YN" _
                                            , "CUST_ORD_NO" _
                                            , "BUYER_ORD_NO" _
                                            , "REMARK" _
                                            , "OUTKA_PKG_NB" _
                                            , "DENP_YN" _
                                            , "PC_KB" _
                                            , "NIYAKU_YN" _
                                            , "ALL_PRINT_FLAG" _
                                            , "NIHUDA_FLAG" _
                                            , "NHS_FLAG" _
                                            , "DENP_FLAG" _
                                            , "COA_FLAG" _
                                            , "HOKOKU_FLAG" _
                                            , "MATOME_PICK_FLAG" _
                                            , "ORDER_TYPE" _
                                            , "DEST_KB" _
                                            , "DEST_NM2" _
                                            , "DEST_AD_1" _
                                            , "DEST_AD_2" _
                                            , "CUST_NM" _
                                            , "CUST_NM_L" _
                                            , "CUST_NM_M" _
                                            , "SHIP_NM" _
                                            , "DEST_NM" _
                                            , "AD_1" _
                                            , "AD_2" _
                                            , "ZIP" _
                                            , "COA_NM" _
                                            , "OUTKA_SASHIZU_PRT_YN" _
                                            , "OUTOKA_KANRYO_YN" _
                                            , "OUTKA_KENPIN_YN" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "SYS_DEL_FLG" _
                                            , "UNTIN_CALCULATION_KB" _
                                            , "TOU_KANRI_YN" _
                                            , "OUTKA_STATE_KB_OLD" _
                                            , "SASZ_USER" _
                                            , "SASZ_USER_OLD" _
                                            , "UP_KBN" _
                                            , "WH_TAB_STATUS" _
                                            , "WH_TAB_YN" _
                                            , "URGENT_YN" _
                                            , "WH_SIJI_REMARK" _
                                            , "WH_TAB_NO_SIJI_FLG" _
                                            , "WH_TAB_HOKOKU_YN" _
                                            , "WH_TAB_HOKOKU" _
                                            }

        Dim SQL As String = String.Concat(LMC020DAC.SQL_SELECT_OUT_L, LMC020DAC.SQL_FROM_OUT_L, LMC020DAC.SQL_WHERE_OUT_L)

        Return Me.SelectData(ds, "LMC020_OUTKA_L", SQL, str)

    End Function

    ''' <summary>
    ''' 出荷データ（中）取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectOutkaMData(ByVal ds As DataSet) As DataSet

        'START YANAI メモ②No.20
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "OUTKA_NO_L" _
        '                                    , "OUTKA_NO_M" _
        '                                    , "COA_YN" _
        '                                    , "CUST_ORD_NO_DTL" _
        '                                    , "BUYER_ORD_NO_DTL" _
        '                                    , "GOODS_CD_NRS" _
        '                                    , "RSV_NO" _
        '                                    , "LOT_NO" _
        '                                    , "SERIAL_NO" _
        '                                    , "ALCTD_KB" _
        '                                    , "ALCTD_KB_HOZON" _
        '                                    , "OUTKA_PKG_NB" _
        '                                    , "OUTKA_HASU" _
        '                                    , "OUTKA_TTL_NB" _
        '                                    , "OUTKA_TTL_QT" _
        '                                    , "ALCTD_NB" _
        '                                    , "ALCTD_QT" _
        '                                    , "BACKLOG_NB" _
        '                                    , "BACKLOG_QT" _
        '                                    , "UNSO_ONDO_KB" _
        '                                    , "IRIME" _
        '                                    , "IRIME_UT" _
        '                                    , "OUTKA_M_PKG_NB" _
        '                                    , "REMARK" _
        '                                    , "SIZE_KB" _
        '                                    , "PRINT_SORT" _
        '                                    , "SYS_DEL_FLG" _
        '                                    , "SUM_OUTKA_TTL_NB" _
        '                                    , "GOODS_CD_CUST" _
        '                                    , "GOODS_NM" _
        '                                    , "HIKIATE" _
        '                                    , "ALCTD_KB_NM" _
        '                                    , "NB_UT_NM" _
        '                                    , "QT_UT_NM" _
        '                                    , "PKG_NB" _
        '                                    , "SEARCH_KEY_1" _
        '                                    , "GOODS_UNSO_ONDO_KB" _
        '                                    , "PKG_UT" _
        '                                    , "STD_IRIME_NB" _
        '                                    , "STD_WT_KGS" _
        '                                    , "TARE_YN" _
        '                                    , "OUTKA_ATT" _
        '                                    , "GOODS_CD_NRS_FROM" _
        '                                    , "CUST_CD_S" _
        '                                    , "CUST_CD_SS" _
        '                                    , "NB_UT" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    , "UP_KBN" _
        '                                    }
        'START YANAI 要望番号499
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "OUTKA_NO_L" _
        '                                    , "OUTKA_NO_M" _
        '                                    , "COA_YN" _
        '                                    , "CUST_ORD_NO_DTL" _
        '                                    , "BUYER_ORD_NO_DTL" _
        '                                    , "GOODS_CD_NRS" _
        '                                    , "RSV_NO" _
        '                                    , "LOT_NO" _
        '                                    , "SERIAL_NO" _
        '                                    , "ALCTD_KB" _
        '                                    , "ALCTD_KB_HOZON" _
        '                                    , "OUTKA_PKG_NB" _
        '                                    , "OUTKA_HASU" _
        '                                    , "OUTKA_TTL_NB" _
        '                                    , "OUTKA_TTL_QT" _
        '                                    , "ALCTD_NB" _
        '                                    , "ALCTD_QT" _
        '                                    , "BACKLOG_NB" _
        '                                    , "BACKLOG_QT" _
        '                                    , "UNSO_ONDO_KB" _
        '                                    , "IRIME" _
        '                                    , "IRIME_UT" _
        '                                    , "OUTKA_M_PKG_NB" _
        '                                    , "REMARK" _
        '                                    , "SIZE_KB" _
        '                                    , "PRINT_SORT" _
        '                                    , "SYS_DEL_FLG" _
        '                                    , "SUM_OUTKA_TTL_NB" _
        '                                    , "GOODS_CD_CUST" _
        '                                    , "GOODS_NM" _
        '                                    , "HIKIATE" _
        '                                    , "ALCTD_KB_NM" _
        '                                    , "NB_UT_NM" _
        '                                    , "QT_UT_NM" _
        '                                    , "PKG_NB" _
        '                                    , "SEARCH_KEY_1" _
        '                                    , "GOODS_UNSO_ONDO_KB" _
        '                                    , "PKG_UT" _
        '                                    , "STD_IRIME_NB" _
        '                                    , "STD_WT_KGS" _
        '                                    , "TARE_YN" _
        '                                    , "OUTKA_ATT" _
        '                                    , "GOODS_CD_NRS_FROM" _
        '                                    , "CUST_CD_S" _
        '                                    , "CUST_CD_SS" _
        '                                    , "NB_UT" _
        '                                    , "EDI_FLG" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    , "UP_KBN" _
        '                                    }
        'START YANAI 要望番号573
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "OUTKA_NO_L" _
        '                                    , "OUTKA_NO_M" _
        '                                    , "COA_YN" _
        '                                    , "CUST_ORD_NO_DTL" _
        '                                    , "BUYER_ORD_NO_DTL" _
        '                                    , "GOODS_CD_NRS" _
        '                                    , "RSV_NO" _
        '                                    , "LOT_NO" _
        '                                    , "SERIAL_NO" _
        '                                    , "ALCTD_KB" _
        '                                    , "ALCTD_KB_HOZON" _
        '                                    , "OUTKA_PKG_NB" _
        '                                    , "OUTKA_HASU" _
        '                                    , "OUTKA_TTL_NB" _
        '                                    , "OUTKA_TTL_QT" _
        '                                    , "ALCTD_NB" _
        '                                    , "ALCTD_QT" _
        '                                    , "BACKLOG_NB" _
        '                                    , "BACKLOG_QT" _
        '                                    , "UNSO_ONDO_KB" _
        '                                    , "IRIME" _
        '                                    , "IRIME_UT" _
        '                                    , "OUTKA_M_PKG_NB" _
        '                                    , "REMARK" _
        '                                    , "SIZE_KB" _
        '                                    , "PRINT_SORT" _
        '                                    , "SYS_DEL_FLG" _
        '                                    , "SUM_OUTKA_TTL_NB" _
        '                                    , "GOODS_CD_CUST" _
        '                                    , "GOODS_NM" _
        '                                    , "HIKIATE" _
        '                                    , "ALCTD_KB_NM" _
        '                                    , "NB_UT_NM" _
        '                                    , "QT_UT_NM" _
        '                                    , "PKG_NB" _
        '                                    , "SEARCH_KEY_1" _
        '                                    , "GOODS_UNSO_ONDO_KB" _
        '                                    , "PKG_UT" _
        '                                    , "STD_IRIME_NB" _
        '                                    , "STD_WT_KGS" _
        '                                    , "TARE_YN" _
        '                                    , "OUTKA_ATT" _
        '                                    , "GOODS_CD_NRS_FROM" _
        '                                    , "CUST_CD_S" _
        '                                    , "CUST_CD_SS" _
        '                                    , "NB_UT" _
        '                                    , "CUST_CD_L_GOODS" _
        '                                    , "CUST_CD_M_GOODS" _
        '                                    , "EDI_FLG" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    , "UP_KBN" _
        '                                    }
        'START YANAI 要望番号1299 運送(中)の在庫部課・扱い部課保持対応
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "OUTKA_NO_L" _
        '                                    , "OUTKA_NO_M" _
        '                                    , "COA_YN" _
        '                                    , "CUST_ORD_NO_DTL" _
        '                                    , "BUYER_ORD_NO_DTL" _
        '                                    , "GOODS_CD_NRS" _
        '                                    , "RSV_NO" _
        '                                    , "LOT_NO" _
        '                                    , "SERIAL_NO" _
        '                                    , "ALCTD_KB" _
        '                                    , "ALCTD_KB_HOZON" _
        '                                    , "OUTKA_PKG_NB" _
        '                                    , "OUTKA_HASU" _
        '                                    , "OUTKA_TTL_NB" _
        '                                    , "OUTKA_TTL_QT" _
        '                                    , "ALCTD_NB" _
        '                                    , "ALCTD_QT" _
        '                                    , "BACKLOG_NB" _
        '                                    , "BACKLOG_QT" _
        '                                    , "UNSO_ONDO_KB" _
        '                                    , "IRIME" _
        '                                    , "IRIME_UT" _
        '                                    , "OUTKA_M_PKG_NB" _
        '                                    , "REMARK" _
        '                                    , "SIZE_KB" _
        '                                    , "PRINT_SORT" _
        '                                    , "SYS_DEL_FLG" _
        '                                    , "SUM_OUTKA_TTL_NB" _
        '                                    , "GOODS_CD_CUST" _
        '                                    , "GOODS_NM" _
        '                                    , "HIKIATE" _
        '                                    , "ALCTD_KB_NM" _
        '                                    , "NB_UT_NM" _
        '                                    , "QT_UT_NM" _
        '                                    , "PKG_NB" _
        '                                    , "SEARCH_KEY_1" _
        '                                    , "GOODS_UNSO_ONDO_KB" _
        '                                    , "PKG_UT" _
        '                                    , "STD_IRIME_NB" _
        '                                    , "STD_WT_KGS" _
        '                                    , "TARE_YN" _
        '                                    , "OUTKA_ATT" _
        '                                    , "GOODS_CD_NRS_FROM" _
        '                                    , "CUST_CD_S" _
        '                                    , "CUST_CD_SS" _
        '                                    , "NB_UT" _
        '                                    , "CUST_CD_L_GOODS" _
        '                                    , "CUST_CD_M_GOODS" _
        '                                    , "EDI_FLG" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    , "UP_KBN" _
        '                                    , "JISSEKI_FLAG" _
        '                                    }
        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "OUTKA_NO_L" _
                                            , "OUTKA_NO_M" _
                                            , "COA_YN" _
                                            , "CUST_ORD_NO_DTL" _
                                            , "BUYER_ORD_NO_DTL" _
                                            , "GOODS_CD_NRS" _
                                            , "RSV_NO" _
                                            , "LOT_NO" _
                                            , "SERIAL_NO" _
                                            , "ALCTD_KB" _
                                            , "ALCTD_KB_HOZON" _
                                            , "OUTKA_PKG_NB" _
                                            , "OUTKA_HASU" _
                                            , "OUTKA_TTL_NB" _
                                            , "OUTKA_TTL_QT" _
                                            , "ALCTD_NB" _
                                            , "ALCTD_QT" _
                                            , "BACKLOG_NB" _
                                            , "BACKLOG_QT" _
                                            , "UNSO_ONDO_KB" _
                                            , "IRIME" _
                                            , "IRIME_UT" _
                                            , "OUTKA_M_PKG_NB" _
                                            , "REMARK" _
                                            , "SIZE_KB" _
                                            , "PRINT_SORT" _
                                            , "SYS_DEL_FLG" _
                                            , "SUM_OUTKA_TTL_NB" _
                                            , "GOODS_CD_CUST" _
                                            , "GOODS_NM" _
                                            , "HIKIATE" _
                                            , "ALCTD_KB_NM" _
                                            , "NB_UT_NM" _
                                            , "QT_UT_NM" _
                                            , "PKG_NB" _
                                            , "SEARCH_KEY_1" _
                                            , "GOODS_UNSO_ONDO_KB" _
                                            , "PKG_UT" _
                                            , "STD_IRIME_NB" _
                                            , "STD_WT_KGS" _
                                            , "TARE_YN" _
                                            , "OUTKA_ATT" _
                                            , "GOODS_CD_NRS_FROM" _
                                            , "CUST_CD_S" _
                                            , "CUST_CD_SS" _
                                            , "NB_UT" _
                                            , "CUST_CD_L_GOODS" _
                                            , "CUST_CD_M_GOODS" _
                                            , "EDI_FLG" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "UP_KBN" _
                                            , "JISSEKI_FLAG" _
                                            , "ZBUKA_CD" _
                                            , "ABUKA_CD" _
                                            , "EDI_OUTKA_TTL_NB" _
                                            , "EDI_OUTKA_TTL_QT" _
                                            , "SHOBO_CD" _
                                            , "SHOBO_NM" _
                                            }
        'END YANAI 要望番号1299 運送(中)の在庫部課・扱い部課保持対応
        'END YANAI 要望番号573
        'END YANAI 要望番号499
        'END YANAI メモ②No.20

        Dim SQL As String = String.Concat(LMC020DAC.SQL_SELECT_OUT_M, LMC020DAC.SQL_FROM_OUT_M, LMC020DAC.SQL_WHERE_OUT_M, _
                                          LMC020DAC.SQL_ORDER_BY_OUT_M)

        Return Me.SelectData(ds, "LMC020_OUTKA_M", SQL, str)

    End Function

    ''' <summary>
    ''' 出荷データ（小）取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectOutkaSData(ByVal ds As DataSet) As DataSet

        'START YANAI 20110913 小分け対応
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "OUTKA_NO_L" _
        '                                    , "OUTKA_NO_M" _
        '                                    , "OUTKA_NO_S" _
        '                                    , "TOU_NO" _
        '                                    , "SITU_NO" _
        '                                    , "ZONE_CD" _
        '                                    , "LOCA" _
        '                                    , "LOT_NO" _
        '                                    , "SERIAL_NO" _
        '                                    , "OUTKA_TTL_NB" _
        '                                    , "OUTKA_TTL_QT" _
        '                                    , "ZAI_REC_NO" _
        '                                    , "INKA_NO_L" _
        '                                    , "INKA_NO_M" _
        '                                    , "INKA_NO_S" _
        '                                    , "ZAI_UPD_FLAG" _
        '                                    , "ALCTD_CAN_NB" _
        '                                    , "ALCTD_NB" _
        '                                    , "ALCTD_CAN_QT" _
        '                                    , "ALCTD_QT" _
        '                                    , "IRIME" _
        '                                    , "BETU_WT" _
        '                                    , "COA_FLAG" _
        '                                    , "REMARK" _
        '                                    , "SMPL_FLAG" _
        '                                    , "REC_NO" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    , "SYS_DEL_FLG" _
        '                                    , "UP_KBN" _
        '                                    , "ALCTD_CAN_NB_GAMEN" _
        '                                    , "ALCTD_NB_GAMEN" _
        '                                    , "ALCTD_CAN_QT_GAMEN" _
        '                                    , "ALCTD_QT_GAMEN" _
        '                                    , "ALCTD_CAN_NB_MATOME" _
        '                                    , "ALCTD_NB_MATOME" _
        '                                    , "ALCTD_CAN_QT_MATOME" _
        '                                    , "ALCTD_QT_MATOME" _
        '                                    }
        'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "OUTKA_NO_L" _
        '                                    , "OUTKA_NO_M" _
        '                                    , "OUTKA_NO_S" _
        '                                    , "TOU_NO" _
        '                                    , "SITU_NO" _
        '                                    , "ZONE_CD" _
        '                                    , "LOCA" _
        '                                    , "LOT_NO" _
        '                                    , "SERIAL_NO" _
        '                                    , "OUTKA_TTL_NB" _
        '                                    , "OUTKA_TTL_QT" _
        '                                    , "ZAI_REC_NO" _
        '                                    , "INKA_NO_L" _
        '                                    , "INKA_NO_M" _
        '                                    , "INKA_NO_S" _
        '                                    , "ZAI_UPD_FLAG" _
        '                                    , "ALCTD_CAN_NB" _
        '                                    , "ALCTD_NB" _
        '                                    , "ALCTD_CAN_QT" _
        '                                    , "ALCTD_QT" _
        '                                    , "IRIME" _
        '                                    , "BETU_WT" _
        '                                    , "COA_FLAG" _
        '                                    , "REMARK" _
        '                                    , "SMPL_FLAG" _
        '                                    , "REC_NO" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    , "SYS_DEL_FLG" _
        '                                    , "UP_KBN" _
        '                                    , "ALCTD_CAN_NB_GAMEN" _
        '                                    , "ALCTD_NB_GAMEN" _
        '                                    , "ALCTD_CAN_QT_GAMEN" _
        '                                    , "ALCTD_QT_GAMEN" _
        '                                    , "ALCTD_CAN_NB_MATOME" _
        '                                    , "ALCTD_NB_MATOME" _
        '                                    , "ALCTD_CAN_QT_MATOME" _
        '                                    , "ALCTD_QT_MATOME" _
        '                                    , "MATOME_FLG" _
        '                                    , "OUTKA_NO_L2" _
        '                                    , "N_ZAI_REC_NO" _
        '                                    , "KOWAKE_IRIME" _
        '                                    }
        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "OUTKA_NO_L" _
                                            , "OUTKA_NO_M" _
                                            , "OUTKA_NO_S" _
                                            , "TOU_NO" _
                                            , "SITU_NO" _
                                            , "ZONE_CD" _
                                            , "LOCA" _
                                            , "LOT_NO" _
                                            , "SERIAL_NO" _
                                            , "OUTKA_TTL_NB" _
                                            , "OUTKA_TTL_QT" _
                                            , "ZAI_REC_NO" _
                                            , "INKA_NO_L" _
                                            , "INKA_NO_M" _
                                            , "INKA_NO_S" _
                                            , "ZAI_UPD_FLAG" _
                                            , "ALCTD_CAN_NB" _
                                            , "ALCTD_NB" _
                                            , "ALCTD_CAN_QT" _
                                            , "ALCTD_QT" _
                                            , "IRIME" _
                                            , "BETU_WT" _
                                            , "COA_FLAG" _
                                            , "REMARK" _
                                            , "SMPL_FLAG" _
                                            , "REC_NO" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "SYS_DEL_FLG" _
                                            , "UP_KBN" _
                                            , "ALCTD_CAN_NB_GAMEN" _
                                            , "ALCTD_NB_GAMEN" _
                                            , "ALCTD_CAN_QT_GAMEN" _
                                            , "ALCTD_QT_GAMEN" _
                                            , "ALCTD_CAN_NB_MATOME" _
                                            , "ALCTD_NB_MATOME" _
                                            , "ALCTD_CAN_QT_MATOME" _
                                            , "ALCTD_QT_MATOME" _
                                            , "MATOME_FLG" _
                                            , "OUTKA_NO_L2" _
                                            , "N_ZAI_REC_NO" _
                                            , "O_ZAI_REC_NO" _
                                            , "KOWAKE_IRIME" _
                                            , "INKA_DATE" _
                                            }
        'MOD 2018/11/14 要望番号001939 INKA_DATEを追加
        'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
        'END YANAI 20110913 小分け対応

        Dim SQL As String = String.Concat(LMC020DAC.SQL_SELECT_OUT_S, LMC020DAC.SQL_FROM_OUT_S, LMC020DAC.SQL_WHERE_OUT_S, _
                                              LMC020DAC.SQL_ORDER_BY_OUT_S)

        Return Me.SelectData(ds, "LMC020_OUTKA_S", SQL, str)

    End Function

    ''' <summary>
    ''' 作業データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectSagyoData(ByVal ds As DataSet) As DataSet

        'START YANAI 要望番号820
        'Dim str As String() = New String() {"SAGYO_COMP" _
        '                                    , "SKYU_CHK" _
        '                                    , "SAGYO_REC_NO" _
        '                                    , "SAGYO_SIJI_NO" _
        '                                    , "INOUTKA_NO_LM" _
        '                                    , "NRS_BR_CD" _
        '                                    , "WH_CD" _
        '                                    , "SAGYO_CD" _
        '                                    , "DEST_SAGYO_FLG" _
        '                                    , "CUST_CD_L" _
        '                                    , "CUST_CD_M" _
        '                                    , "SAGYO_RYAK" _
        '                                    , "SAGYO_NM" _
        '                                    , "DEST_CD" _
        '                                    , "DEST_NM" _
        '                                    , "LOT_NO" _
        '                                    , "INV_TANI" _
        '                                    , "SAGYO_NB" _
        '                                    , "SAGYO_UP" _
        '                                    , "SAGYO_GK" _
        '                                    , "TAX_KB" _
        '                                    , "SEIQTO_CD" _
        '                                    , "REMARK_SKYU" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    , "SYS_DEL_FLG" _
        '                                    , "UP_KBN" _
        '                                    }
        Dim str As String() = New String() {"SAGYO_COMP" _
                                            , "SKYU_CHK" _
                                            , "SAGYO_REC_NO" _
                                            , "SAGYO_SIJI_NO" _
                                            , "INOUTKA_NO_LM" _
                                            , "NRS_BR_CD" _
                                            , "WH_CD" _
                                            , "SAGYO_CD" _
                                            , "DEST_SAGYO_FLG" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "SAGYO_RYAK" _
                                            , "SAGYO_NM" _
                                            , "DEST_CD" _
                                            , "DEST_NM" _
                                            , "LOT_NO" _
                                            , "INV_TANI" _
                                            , "SAGYO_NB" _
                                            , "SAGYO_UP" _
                                            , "SAGYO_GK" _
                                            , "TAX_KB" _
                                            , "SEIQTO_CD" _
                                            , "REMARK_SKYU" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "SYS_DEL_FLG" _
                                            , "IOZS_KB" _
                                            , "GOODS_CD_NRS" _
                                            , "GOODS_NM_NRS" _
                                            , "REMARK_ZAI" _
                                            , "SAGYO_COMP_CD" _
                                            , "SAGYO_COMP_DATE" _
                                            , "UP_KBN" _
                                            , "REMARK_SIJI" _
                                            }
        'END YANAI 要望番号820

        Dim SQL As String = String.Concat(LMC020DAC.SQL_SELECT_SAGYO, LMC020DAC.SQL_FROM_SAGYO, LMC020DAC.SQL_WHERE_SAGYO, _
                                              LMC020DAC.SQL_ORDER_BY_SAGYO)

        Return Me.SelectData(ds, "LMC020_SAGYO", SQL, str)

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 運送データ（大）取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectUnsoLData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "UNSO_NO_L" _
                                            , "YUSO_BR_CD" _
                                            , "INOUTKA_NO_L" _
                                            , "TRIP_NO" _
                                            , "UNSO_CD" _
                                            , "UNSO_BR_CD" _
                                            , "TARE_YN" _
                                            , "BIN_KB" _
                                            , "JIYU_KB" _
                                            , "DENP_NO" _
                                            , "AUTO_DENP_KBN" _
                                            , "AUTO_DENP_NO" _
                                            , "OUTKA_PLAN_DATE" _
                                            , "OUTKA_PLAN_TIME" _
                                            , "ARR_PLAN_DATE" _
                                            , "ARR_PLAN_TIME" _
                                            , "ARR_ACT_TIME" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "CUST_REF_NO" _
                                            , "SHIP_CD" _
                                            , "ORIG_CD" _
                                            , "DEST_CD" _
                                            , "UNSO_PKG_NB" _
                                            , "NB_UT" _
                                            , "UNSO_WT" _
                                            , "UNSO_ONDO_KB" _
                                            , "PC_KB" _
                                            , "TARIFF_BUNRUI_KB" _
                                            , "VCLE_KB" _
                                            , "MOTO_DATA_KB" _
                                            , "TAX_KB" _
                                            , "REMARK" _
                                            , "SEIQ_TARIFF_CD" _
                                            , "SEIQ_ETARIFF_CD" _
                                            , "AD_3" _
                                            , "UNSO_TEHAI_KB" _
                                            , "BUY_CHU_NO" _
                                            , "AREA_CD" _
                                            , "TYUKEI_HAISO_FLG" _
                                            , "SYUKA_TYUKEI_CD" _
                                            , "HAIKA_TYUKEI_CD" _
                                            , "TRIP_NO_SYUKA" _
                                            , "TRIP_NO_TYUKEI" _
                                            , "TRIP_NO_HAIKA" _
                                            , "KYORI" _
                                            , "UNSOCO_NM" _
                                            , "UNSOCO_BR_NM" _
                                            , "SEIQ_TARIFF_NM" _
                                            , "SEIQ_FIXED_FLAG" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "SYS_DEL_FLG" _
                                            , "NIHUDA_YN" _
                                            , "UP_KBN" _
                                            , "TORIKESI_FLG" _
                                            , "SHIHARAI_TARIFF_CD" _
                                            , "SHIHARAI_TARIFF_NM" _
                                            , "SHIHARAI_ETARIFF_CD" _
                                            , "SHIHARAI_FIXED_FLAG" _
                                            , "SHIHARAI_GROUP_NO" _
                                            , "SEIQ_GROUP_NO" _
                                            }

        Dim SQL As String = String.Concat(LMC020DAC.SQL_SELECT_UNSO_L, LMC020DAC.SQL_FROM_UNSO_L, LMC020DAC.SQL_WHERE_UNSO_L, SQL_GROUP_BY_UNSO_L, SQL_ORDER_BY_UNSO_L)

        Return Me.SelectData(ds, "LMC020_UNSO_L", SQL, str)

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 運送データ（中）取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectUnsoMData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "UNSO_NO_L" _
                                            , "UNSO_NO_M" _
                                            , "UNSO_TTL_NB" _
                                            , "ZBUKA_CD" _
                                            , "ABUKA_CD" _
                                            , "UP_KBN" _
                                            }

        Dim SQL As String = String.Concat(LMC020DAC.SQL_SELECT_UNSO_M, LMC020DAC.SQL_FROM_UNSO_M, LMC020DAC.SQL_WHERE_UNSO_M)

        Return Me.SelectData(ds, "LMC020_UNSO_M", SQL, str)

    End Function

    ''' <summary>
    ''' 在庫データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectZaiData(ByVal ds As DataSet) As DataSet

        'START 20111116 まとめデータのダブルクリック対応
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "ZAI_REC_NO" _
        '                                    , "WH_CD" _
        '                                    , "TOU_NO" _
        '                                    , "SITU_NO" _
        '                                    , "ZONE_CD" _
        '                                    , "LOCA" _
        '                                    , "LOT_NO" _
        '                                    , "CUST_CD_L" _
        '                                    , "CUST_CD_M" _
        '                                    , "GOODS_CD_NRS" _
        '                                    , "INKA_NO_L" _
        '                                    , "INKA_NO_M" _
        '                                    , "INKA_NO_S" _
        '                                    , "ALLOC_PRIORITY" _
        '                                    , "RSV_NO" _
        '                                    , "SERIAL_NO" _
        '                                    , "HOKAN_YN" _
        '                                    , "TAX_KB" _
        '                                    , "GOODS_COND_KB_1" _
        '                                    , "GOODS_COND_KB_2" _
        '                                    , "GOODS_COND_KB_3" _
        '                                    , "OFB_KB" _
        '                                    , "SPD_KB" _
        '                                    , "REMARK_OUT" _
        '                                    , "PORA_ZAI_NB" _
        '                                    , "ALCTD_NB" _
        '                                    , "ALLOC_CAN_NB" _
        '                                    , "IRIME" _
        '                                    , "PORA_ZAI_QT" _
        '                                    , "ALCTD_QT" _
        '                                    , "ALLOC_CAN_QT" _
        '                                    , "INKO_DATE" _
        '                                    , "INKO_PLAN_DATE" _
        '                                    , "ZERO_FLAG" _
        '                                    , "LT_DATE" _
        '                                    , "GOODS_CRT_DATE" _
        '                                    , "DEST_CD_P" _
        '                                    , "REMARK" _
        '                                    , "SMPL_FLAG" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    , "SYS_DEL_FLG" _
        '                                    , "GOODS_COND_NM_1" _
        '                                    , "GOODS_COND_NM_2" _
        '                                    , "GOODS_COND_NM_3" _
        '                                    , "ALLOC_PRIORITY_NM" _
        '                                    , "OFB_KB_NM" _
        '                                    , "SPD_KB_NM" _
        '                                    , "ALLOC_CAN_NB_HOZON" _
        '                                    , "ALLOC_CAN_QT_HOZON" _
        '                                    , "ALCTD_NB_HOZON" _
        '                                    , "ALCTD_QT_HOZON" _
        '                                    , "ALCTD_NB_GAMEN" _
        '                                    , "ALCTD_QT_GAMEN" _
        '                                    , "ALLOC_CAN_NB_GAMEN" _
        '                                    , "ALLOC_CAN_QT_GAMEN" _
        '                                    , "UP_KBN" _
        '                                    , "ALCTD_KB_FLG" _
        '                                    , "INKA_DATE" _
        '                                    , "IDO_DATE" _
        '                                    , "HOKAN_STR_DATE" _
        '                                    }
        'START YANAI 要望番号780
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "ZAI_REC_NO" _
        '                                    , "WH_CD" _
        '                                    , "TOU_NO" _
        '                                    , "SITU_NO" _
        '                                    , "ZONE_CD" _
        '                                    , "LOCA" _
        '                                    , "LOT_NO" _
        '                                    , "CUST_CD_L" _
        '                                    , "CUST_CD_M" _
        '                                    , "GOODS_CD_NRS" _
        '                                    , "INKA_NO_L" _
        '                                    , "INKA_NO_M" _
        '                                    , "INKA_NO_S" _
        '                                    , "ALLOC_PRIORITY" _
        '                                    , "RSV_NO" _
        '                                    , "SERIAL_NO" _
        '                                    , "HOKAN_YN" _
        '                                    , "TAX_KB" _
        '                                    , "GOODS_COND_KB_1" _
        '                                    , "GOODS_COND_KB_2" _
        '                                    , "GOODS_COND_KB_3" _
        '                                    , "OFB_KB" _
        '                                    , "SPD_KB" _
        '                                    , "REMARK_OUT" _
        '                                    , "PORA_ZAI_NB" _
        '                                    , "ALCTD_NB" _
        '                                    , "ALLOC_CAN_NB" _
        '                                    , "IRIME" _
        '                                    , "PORA_ZAI_QT" _
        '                                    , "ALCTD_QT" _
        '                                    , "ALLOC_CAN_QT" _
        '                                    , "INKO_DATE" _
        '                                    , "INKO_PLAN_DATE" _
        '                                    , "ZERO_FLAG" _
        '                                    , "LT_DATE" _
        '                                    , "GOODS_CRT_DATE" _
        '                                    , "DEST_CD_P" _
        '                                    , "REMARK" _
        '                                    , "SMPL_FLAG" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    , "SYS_DEL_FLG" _
        '                                    , "GOODS_COND_NM_1" _
        '                                    , "GOODS_COND_NM_2" _
        '                                    , "GOODS_COND_NM_3" _
        '                                    , "ALLOC_PRIORITY_NM" _
        '                                    , "OFB_KB_NM" _
        '                                    , "SPD_KB_NM" _
        '                                    , "ALLOC_CAN_NB_HOZON" _
        '                                    , "ALLOC_CAN_QT_HOZON" _
        '                                    , "ALCTD_NB_HOZON" _
        '                                    , "ALCTD_QT_HOZON" _
        '                                    , "ALCTD_NB_GAMEN" _
        '                                    , "ALCTD_QT_GAMEN" _
        '                                    , "ALLOC_CAN_NB_GAMEN" _
        '                                    , "ALLOC_CAN_QT_GAMEN" _
        '                                    , "UP_KBN" _
        '                                    , "MATOME_FLG" _
        '                                    , "ALCTD_KB_FLG" _
        '                                    , "INKA_DATE" _
        '                                    , "IDO_DATE" _
        '                                    , "HOKAN_STR_DATE" _
        '                                    }
        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "ZAI_REC_NO" _
                                            , "WH_CD" _
                                            , "TOU_NO" _
                                            , "SITU_NO" _
                                            , "ZONE_CD" _
                                            , "LOCA" _
                                            , "LOT_NO" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "GOODS_CD_NRS" _
                                            , "GOODS_KANRI_NO" _
                                            , "INKA_NO_L" _
                                            , "INKA_NO_M" _
                                            , "INKA_NO_S" _
                                            , "ALLOC_PRIORITY" _
                                            , "RSV_NO" _
                                            , "SERIAL_NO" _
                                            , "HOKAN_YN" _
                                            , "TAX_KB" _
                                            , "GOODS_COND_KB_1" _
                                            , "GOODS_COND_KB_2" _
                                            , "GOODS_COND_KB_3" _
                                            , "OFB_KB" _
                                            , "SPD_KB" _
                                            , "REMARK_OUT" _
                                            , "PORA_ZAI_NB" _
                                            , "ALCTD_NB" _
                                            , "ALLOC_CAN_NB" _
                                            , "IRIME" _
                                            , "PORA_ZAI_QT" _
                                            , "ALCTD_QT" _
                                            , "ALLOC_CAN_QT" _
                                            , "INKO_DATE" _
                                            , "INKO_PLAN_DATE" _
                                            , "ZERO_FLAG" _
                                            , "LT_DATE" _
                                            , "GOODS_CRT_DATE" _
                                            , "DEST_CD_P" _
                                            , "REMARK" _
                                            , "SMPL_FLAG" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "SYS_DEL_FLG" _
                                            , "GOODS_COND_NM_1" _
                                            , "GOODS_COND_NM_2" _
                                            , "GOODS_COND_NM_3" _
                                            , "ALLOC_PRIORITY_NM" _
                                            , "OFB_KB_NM" _
                                            , "SPD_KB_NM" _
                                            , "ALLOC_CAN_NB_HOZON" _
                                            , "ALLOC_CAN_QT_HOZON" _
                                            , "ALCTD_NB_HOZON" _
                                            , "ALCTD_QT_HOZON" _
                                            , "ALCTD_NB_GAMEN" _
                                            , "ALCTD_QT_GAMEN" _
                                            , "ALLOC_CAN_NB_GAMEN" _
                                            , "ALLOC_CAN_QT_GAMEN" _
                                            , "UP_KBN" _
                                            , "MATOME_FLG" _
                                            , "ALCTD_KB_FLG" _
                                            , "INKA_DATE" _
                                            , "IDO_DATE" _
                                            , "HOKAN_STR_DATE" _
                                            , "INKA_DATE_KANRI_KB" _
                                            }
        'END YANAI 要望番号780
        'END 20111116 まとめデータのダブルクリック対応

        'WHERE文にZAI_REC_NOを設定
        Dim sqlWhere As String = LMC020DAC.SQL_WHERE_ZAI
        Dim max As Integer = ds.Tables("LMC020_OUTKA_S").Rows.Count - 1
        Dim kugiri As String = String.Empty
        For i As Integer = 0 To max
            sqlWhere = String.Concat(sqlWhere, kugiri, "'", ds.Tables("LMC020_OUTKA_S").Rows(i).Item("ZAI_REC_NO"), "'")
            If String.IsNullOrEmpty(kugiri) = True Then
                kugiri = ","
            End If
        Next
        sqlWhere = String.Concat(sqlWhere, ")")

        Dim SQL As String = String.Concat(LMC020DAC.SQL_SELECT_ZAI, LMC020DAC.SQL_FROM_ZAI, sqlWhere, _
                                              LMC020DAC.SQL_ORDER_BY_ZAI)

        Return Me.SelectData(ds, "LMC020_ZAI", SQL, str)

    End Function

    'START YANAI 要望番号853 まとめ処理対応
    ''' <summary>
    ''' 在庫データ取得(まとめ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectZaiDataMATOME(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "ZAI_REC_NO" _
                                            , "WH_CD" _
                                            , "TOU_NO" _
                                            , "SITU_NO" _
                                            , "ZONE_CD" _
                                            , "LOCA" _
                                            , "LOT_NO" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "GOODS_CD_NRS" _
                                            , "GOODS_KANRI_NO" _
                                            , "INKA_NO_L" _
                                            , "INKA_NO_M" _
                                            , "INKA_NO_S" _
                                            , "ALLOC_PRIORITY" _
                                            , "RSV_NO" _
                                            , "SERIAL_NO" _
                                            , "HOKAN_YN" _
                                            , "TAX_KB" _
                                            , "GOODS_COND_KB_1" _
                                            , "GOODS_COND_KB_2" _
                                            , "GOODS_COND_KB_3" _
                                            , "OFB_KB" _
                                            , "SPD_KB" _
                                            , "REMARK_OUT" _
                                            , "PORA_ZAI_NB" _
                                            , "ALCTD_NB" _
                                            , "ALLOC_CAN_NB" _
                                            , "IRIME" _
                                            , "PORA_ZAI_QT" _
                                            , "ALCTD_QT" _
                                            , "ALLOC_CAN_QT" _
                                            , "INKO_DATE" _
                                            , "INKO_PLAN_DATE" _
                                            , "ZERO_FLAG" _
                                            , "LT_DATE" _
                                            , "GOODS_CRT_DATE" _
                                            , "DEST_CD_P" _
                                            , "REMARK" _
                                            , "SMPL_FLAG" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "SYS_DEL_FLG" _
                                            , "GOODS_COND_NM_1" _
                                            , "GOODS_COND_NM_2" _
                                            , "GOODS_COND_NM_3" _
                                            , "ALLOC_PRIORITY_NM" _
                                            , "OFB_KB_NM" _
                                            , "SPD_KB_NM" _
                                            , "ALLOC_CAN_NB_HOZON" _
                                            , "ALLOC_CAN_QT_HOZON" _
                                            , "ALCTD_NB_HOZON" _
                                            , "ALCTD_QT_HOZON" _
                                            , "ALCTD_NB_GAMEN" _
                                            , "ALCTD_QT_GAMEN" _
                                            , "ALLOC_CAN_NB_GAMEN" _
                                            , "ALLOC_CAN_QT_GAMEN" _
                                            , "UP_KBN" _
                                            , "MATOME_FLG" _
                                            , "ALCTD_KB_FLG" _
                                            , "INKA_DATE" _
                                            , "IDO_DATE" _
                                            , "HOKAN_STR_DATE" _
                                            }

        Dim SQL As String = String.Empty
        'WHERE文にZAI_REC_NOを設定
        'START YANAI 要望番号780
        'If String.IsNullOrEmpty(ds.Tables("LMC020_ZAI_MATOME_IN").Rows(0).Item("INKO_DATE").ToString) = False Then
        '    SQL = String.Concat(LMC020DAC.SQL_SELECT_ZAI, LMC020DAC.SQL_FROM_ZAI, LMC020DAC.SQL_WHERE_ZAI_MATOME, _
        '                        LMC020DAC.SQL_WHERE_INKODATE, LMC020DAC.SQL_ORDER_BY_ZAI)

        'Else
        '    SQL = String.Concat(LMC020DAC.SQL_SELECT_ZAI, LMC020DAC.SQL_FROM_ZAI, LMC020DAC.SQL_WHERE_ZAI_MATOME, _
        '                        LMC020DAC.SQL_WHERE_INKOPLANDATE, LMC020DAC.SQL_ORDER_BY_ZAI)

        'End If
        'upd s.kobayashi NotesNo.1572 "01"-->"1"
        If ("1").Equals(ds.Tables("LMC020_ZAI_MATOME_IN").Rows(0).Item("INKA_DATE_KANRI_KB").ToString) = True Then
            SQL = String.Concat(LMC020DAC.SQL_SELECT_ZAI, LMC020DAC.SQL_FROM_ZAI1, LMC020DAC.SQL_WHERE_ZAI_MATOME, _
                                LMC020DAC.SQL_ORDER_BY_ZAI)

        ElseIf String.IsNullOrEmpty(ds.Tables("LMC020_ZAI_MATOME_IN").Rows(0).Item("INKO_DATE").ToString) = False Then
            SQL = String.Concat(LMC020DAC.SQL_SELECT_ZAI, LMC020DAC.SQL_FROM_ZAI2, LMC020DAC.SQL_WHERE_ZAI_MATOME, _
                                LMC020DAC.SQL_WHERE_INKODATE, LMC020DAC.SQL_ORDER_BY_ZAI)

        Else
            SQL = String.Concat(LMC020DAC.SQL_SELECT_ZAI, LMC020DAC.SQL_FROM_ZAI2, LMC020DAC.SQL_WHERE_ZAI_MATOME, _
                                LMC020DAC.SQL_WHERE_INKOPLANDATE, LMC020DAC.SQL_ORDER_BY_ZAI)

        End If
        'END YANAI 要望番号780

        Return Me.SelectData(ds, "LMC020_ZAI_MATOME_OUT", SQL, str)

    End Function
    'END YANAI 要望番号853 まとめ処理対応

    ''' <summary>
    ''' EDI出荷データ（大）取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectEDILData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"DEL_KB" _
                                            , "NRS_BR_CD" _
                                            , "EDI_CTL_NO" _
                                            , "OUTKA_CTL_NO" _
                                            , "OUTKA_KB" _
                                            , "SYUBETU_KB" _
                                            , "NAIGAI_KB" _
                                            , "OUTKA_STATE_KB" _
                                            , "OUTKAHOKOKU_YN" _
                                            , "PICK_KB" _
                                            , "NRS_BR_NM" _
                                            , "WH_CD" _
                                            , "WH_NM" _
                                            , "OUTKA_PLAN_DATE" _
                                            , "OUTKO_DATE" _
                                            , "ARR_PLAN_DATE" _
                                            , "ARR_PLAN_TIME" _
                                            , "HOKOKU_DATE" _
                                            , "TOUKI_HOKAN_YN" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "CUST_NM_L" _
                                            , "CUST_NM_M" _
                                            , "SHIP_CD_L" _
                                            , "SHIP_CD_M" _
                                            , "SHIP_NM_L" _
                                            , "SHIP_NM_M" _
                                            , "EDI_DEST_CD" _
                                            , "DEST_CD" _
                                            , "DEST_NM" _
                                            , "DEST_ZIP" _
                                            , "DEST_AD_1" _
                                            , "DEST_AD_2" _
                                            , "DEST_AD_3" _
                                            , "DEST_AD_4" _
                                            , "DEST_AD_5" _
                                            , "DEST_TEL" _
                                            , "DEST_FAX" _
                                            , "DEST_MAIL" _
                                            , "DEST_JIS_CD" _
                                            , "SP_NHS_KB" _
                                            , "COA_YN" _
                                            , "CUST_ORD_NO" _
                                            , "BUYER_ORD_NO" _
                                            , "UNSO_MOTO_KB" _
                                            , "UNSO_TEHAI_KB" _
                                            , "SYARYO_KB" _
                                            , "BIN_KB" _
                                            , "UNSO_CD" _
                                            , "UNSO_NM" _
                                            , "UNSO_BR_CD" _
                                            , "UNSO_BR_NM" _
                                            , "UNCHIN_TARIFF_CD" _
                                            , "EXTC_TARIFF_CD" _
                                            , "REMARK" _
                                            , "UNSO_ATT" _
                                            , "DENP_YN" _
                                            , "PC_KB" _
                                            , "UNCHIN_YN" _
                                            , "NIYAKU_YN" _
                                            , "OUT_FLAG" _
                                            , "AKAKURO_KB" _
                                            , "JISSEKI_FLAG" _
                                            , "JISSEKI_USER" _
                                            , "JISSEKI_DATE" _
                                            , "JISSEKI_TIME" _
                                            , "FREE_N01" _
                                            , "FREE_N02" _
                                            , "FREE_N03" _
                                            , "FREE_N04" _
                                            , "FREE_N05" _
                                            , "FREE_N06" _
                                            , "FREE_N07" _
                                            , "FREE_N08" _
                                            , "FREE_N09" _
                                            , "FREE_N10" _
                                            , "FREE_C01" _
                                            , "FREE_C02" _
                                            , "FREE_C03" _
                                            , "FREE_C04" _
                                            , "FREE_C05" _
                                            , "FREE_C06" _
                                            , "FREE_C07" _
                                            , "FREE_C08" _
                                            , "FREE_C09" _
                                            , "FREE_C10" _
                                            , "FREE_C11" _
                                            , "FREE_C12" _
                                            , "FREE_C13" _
                                            , "FREE_C14" _
                                            , "FREE_C15" _
                                            , "FREE_C16" _
                                            , "FREE_C17" _
                                            , "FREE_C18" _
                                            , "FREE_C19" _
                                            , "FREE_C20" _
                                            , "FREE_C21" _
                                            , "FREE_C22" _
                                            , "FREE_C23" _
                                            , "FREE_C24" _
                                            , "FREE_C25" _
                                            , "FREE_C26" _
                                            , "FREE_C27" _
                                            , "FREE_C28" _
                                            , "FREE_C29" _
                                            , "FREE_C30" _
                                            , "CRT_USER" _
                                            , "CRT_DATE" _
                                            , "CRT_TIME" _
                                            , "UPD_USER" _
                                            , "UPD_DATE" _
                                            , "UPD_TIME" _
                                            , "SCM_CTL_NO_L" _
                                            , "EDIT_FLAG" _
                                            , "MATCHING_FLAG" _
                                            }

        Dim SQL As String = String.Concat(LMC020DAC.SQL_SELECT_EDI_L, LMC020DAC.SQL_FROM_EDI_L, LMC020DAC.SQL_WHERE_EDI_L)

        Return Me.SelectData(ds, "LMC020_EDI_OUTKA_L", SQL, str)

    End Function

    ''' <summary>
    ''' MAX番号取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectMaxNoData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"OUTKA_NO_M" _
                                            , "MAX_OUTKA_NO_S" _
                                            }

        Dim SQL As String = String.Concat(LMC020DAC.SQL_SELECT_MAX, LMC020DAC.SQL_FROM_MAX, LMC020DAC.SQL_WHERE_MAX, _
                                              LMC020DAC.SQL_GROUP_BY_MAX, LMC020DAC.SQL_ORDER_BY_MAX)

        Return Me.SelectData(ds, "LMC020_MAX_NO", SQL, str)

    End Function

    ''' <summary>
    ''' 請求鑑データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectKagamiData(ByVal ds As DataSet) As DataSet

        Dim strSql As StringBuilder = New StringBuilder()

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMC020_KAGAMI_IN")
        Dim unsoDr As DataRow() = ds.Tables("LMC020_UNSO_L").Select(String.Concat("SYS_DEL_FLG = '0'"))

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim sql As String = String.Empty
        Dim cmd As SqlCommand = Nothing
        Dim reader As SqlDataReader = Nothing
        Dim map As Hashtable = New Hashtable()

        'SQL作成
        '保管料取込区分
        If String.IsNullOrEmpty(Me._Row.Item("STORAGE_SEIQTO_CD").ToString()) = False Then
            Me._StrSql.Append(LMG000DAC.SQL_SELECT_HOKAN_CHK_DATE)    'SQL構築

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの設定
            Call Me.SetSelectParamKagami(Me._SqlPrmList, Me._Row.Item("STORAGE_SEIQTO_CD").ToString())

            'スキーマ名設定
            sql = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

            'SQL文のコンパイル
            cmd = Nothing
            cmd = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectKagamiData", cmd)

            'SQLの発行
            reader = Nothing
            reader = MyBase.GetSelectResult(cmd)

            'DataReader→DataTableへの転記
            map = New Hashtable()

            '取得データの格納先をマッピング
            map.Add("SKYU_DATE", "SKYU_DATE")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020_STORAGE_SKYU_DATE")

            reader.Close()
        End If

        '荷役料取込区分
        If String.IsNullOrEmpty(Me._Row.Item("HANDLING_SEIQTO_CD").ToString()) = False Then
            Me._StrSql.Append(LMG000DAC.SQL_SELECT_NIYAKU_CHK_DATE)    'SQL構築

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの設定
            Call Me.SetSelectParamKagami(Me._SqlPrmList, Me._Row.Item("HANDLING_SEIQTO_CD").ToString())

            'スキーマ名設定
            sql = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

            'SQL文のコンパイル
            cmd = Nothing
            cmd = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectKagamiData", cmd)

            'SQLの発行
            reader = Nothing
            reader = MyBase.GetSelectResult(cmd)

            'DataReader→DataTableへの転記
            map = New Hashtable()

            '取得データの格納先をマッピング
            map.Add("SKYU_DATE", "SKYU_DATE")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020_HANDLING_SKYU_DATE")

            reader.Close()
        End If

        ''運賃取込区分
        'If String.IsNullOrEmpty(Me._Row.Item("UNCHIN_SEIQTO_CD").ToString()) = False AndAlso _
        '    0 < unsoDr.Length Then
        '    If ("10").Equals(unsoDr(0).Item("TARIFF_BUNRUI_KB")) = True OrElse _
        '        ("20").Equals(unsoDr(0).Item("TARIFF_BUNRUI_KB")) = True OrElse _
        '        ("30").Equals(unsoDr(0).Item("TARIFF_BUNRUI_KB")) = True Then
        '        '横持ち以外の時のみ

        '        Me._StrSql.Append(LMG000DAC.SQL_SELECT_UNCHIN_CHK_DATE)    'SQL構築

        '        'SQLパラメータ初期化
        '        Me._SqlPrmList = New ArrayList()

        '        'パラメータの設定
        '        Call Me.SetSelectParamKagami(Me._SqlPrmList, Me._Row.Item("UNCHIN_SEIQTO_CD").ToString())

        '        'スキーマ名設定
        '        sql = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        '        'SQL文のコンパイル
        '        cmd = Nothing
        '        cmd = MyBase.CreateSqlCommand(sql)

        '        'パラメータの反映
        '        For Each obj As Object In Me._SqlPrmList
        '            cmd.Parameters.Add(obj)
        '        Next

        '        MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectKagamiData", cmd)

        '        'SQLの発行
        '        reader = Nothing
        '        reader = MyBase.GetSelectResult(cmd)

        '        'DataReader→DataTableへの転記
        '        map = New Hashtable()

        '        '取得データの格納先をマッピング
        '        map.Add("SKYU_DATE", "SKYU_DATE")

        '        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020_UNCHIN_SKYU_DATE")

        '        reader.Close()
        '    End If
        'End If

        '作業料取込区分
        If String.IsNullOrEmpty(Me._Row.Item("SAGYO_SEIQTO_CD").ToString()) = False Then
            Me._StrSql.Append(LMG000DAC.SQL_SELECT_SAGYO_CHK_DATE)    'SQL構築

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの設定
            Call Me.SetSelectParamKagami(Me._SqlPrmList, Me._Row.Item("SAGYO_SEIQTO_CD").ToString())

            'スキーマ名設定
            sql = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

            'SQL文のコンパイル
            cmd = Nothing
            cmd = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectKagamiData", cmd)

            'SQLの発行
            reader = Nothing
            reader = MyBase.GetSelectResult(cmd)

            'DataReader→DataTableへの転記
            map = New Hashtable()

            '取得データの格納先をマッピング
            map.Add("SKYU_DATE", "SKYU_DATE")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020_SAGYO_SKYU_DATE")

            reader.Close()
        End If

        ''横持料取込区分
        'If String.IsNullOrEmpty(Me._Row.Item("YOKOMOCHI_SEIQTO_CD").ToString()) = False AndAlso _
        '    0 < unsoDr.Length Then
        '    If ("40").Equals(unsoDr(0).Item("TARIFF_BUNRUI_KB")) = True Then
        '        '横持ちの時のみ

        '        Me._StrSql.Append(LMG000DAC.SQL_SELECT_YOKOMOCHI_CHK_DATE)    'SQL構築

        '        'SQLパラメータ初期化
        '        Me._SqlPrmList = New ArrayList()

        '        'パラメータの設定
        '        Call Me.SetSelectParamKagami(Me._SqlPrmList, Me._Row.Item("YOKOMOCHI_SEIQTO_CD").ToString())

        '        'スキーマ名設定
        '        sql = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        '        'SQL文のコンパイル
        '        cmd = Nothing
        '        cmd = MyBase.CreateSqlCommand(sql)

        '        'パラメータの反映
        '        For Each obj As Object In Me._SqlPrmList
        '            cmd.Parameters.Add(obj)
        '        Next

        '        MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectKagamiData", cmd)

        '        'SQLの発行
        '        reader = Nothing
        '        reader = MyBase.GetSelectResult(cmd)

        '        'DataReader→DataTableへの転記
        '        map = New Hashtable()

        '        '取得データの格納先をマッピング
        '        map.Add("SKYU_DATE", "SKYU_DATE")

        '        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020_YOKOMOCHI_SKYU_DATE")

        '        reader.Close()
        '    End If
        'End If

        Return ds

    End Function

    ''' <summary>
    ''' 小分け先在庫対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>小分け先在庫対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectZaiKowakeData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_S")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC020DAC.SQL_SELECT_IDO_TRS)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMC020DAC.SQL_FROM_IDO_TRS)       'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMC020DAC.SQL_WHERE_IDO_TRS)      'SQL構築(データ抽出用Where句)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの設定
        Call Me.SetSelectKowakeParam(Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectZaiKowakeData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020_KOWAKE")

    End Function

    '要望番号:1350 terakawa 2012.08.24 Start
    ''' <summary>
    ''' 同一置場での商品・ロット重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkGoodsLot(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_S")
        Dim inTblM As DataTable = ds.Tables("LMC020_OUTKA_M")
        Dim inTblL As DataTable = ds.Tables("LMC020_OUTKA_L")
        Dim nrsBrCdDetails As String = String.Empty
        Dim custCdLDetails As String = String.Empty

        '荷主明細から同一置き場・商品チェック特殊荷主情報を取得
        If ds.Tables("CUST_DETAILS").Rows.Count > 0 Then
            nrsBrCdDetails = ds.Tables("CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()
            custCdLDetails = ds.Tables("CUST_DETAILS").Rows(0).Item("SET_NAIYO_2").ToString()
        End If

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        'Me._StrSql.Length = 0  
        '20171025 初期化方法間違い修正
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL作成
        Me._StrSql.Append(LMC020DAC.SQL_GOODS_LOT_CHK)
        If String.IsNullOrEmpty(custCdLDetails) Then
            Me._StrSql.Append(LMC020DAC.SQL_GOODS_LOT_CHK_CUST_CD_L)
        Else
            Me._StrSql.Append(LMC020DAC.SQL_GOODS_LOT_CHK_CUST_CD_L_DETAIL_PLUS)
        End If
        Me._StrSql.Append(LMC020DAC.SQL_GOODS_LOT_CHK_AFTER)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DETAILS_NRS_BR_CD", nrsBrCdDetails, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", inTblL.Rows(0).Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DETAILS_CUST_CD_L", custCdLDetails, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row.Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row.Item("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", Me._Row.Item("ZONE_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", Me._Row.Item("LOCA").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", inTblM.Rows(0).Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me._Row.Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me._Row.Item("ZAI_REC_NO").ToString(), DBDataType.NVARCHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "ChkGoodsLot", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("ZAI_CNT")))
        reader.Close()
        Return ds

    End Function
    '要望番号:1350 terakawa 2012.08.24 End
    ''' <summary>
    ''' 出荷(小)と商品マスタの入目が違うものをチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>2019/12/16 要望管理009513 add</remarks>
    Private Function IrimeCheck(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_S")
        Dim inTblM As DataTable = ds.Tables("LMC020_OUTKA_M")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL作成
        Me._StrSql.Append(LMC020DAC.SQL_IRIME_CHECK)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", inTblM.Rows(0).Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me._Row.Item("IRIME").ToString(), DBDataType.NUMERIC))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "IrimeCheck", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("DIFF_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 荷主マスタ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectSubCustData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"HOKAN_NIYAKU_CALCULATION" _
                                            , "UNTIN_CALCULATION_KB" _
                                            , "HOKAN_SEIQTO_CD" _
                                            , "NIYAKU_SEIQTO_CD" _
                                            , "UNCHIN_SEIQTO_CD" _
                                            , "SAGYO_SEIQTO_CD" _
                                            }
        Dim SQL As String = String.Concat(LMC020DAC.SQL_SELECT_CUST, LMC020DAC.SQL_FROM_CUST, LMC020DAC.SQL_WHERE_CUST)

        Return Me.SelectData(ds, "LMC020_CUST", SQL, str)

    End Function

    '要望番号:1350 terakawa 2012.08.24 Start
#Region "M_CUST_DETAILS"
    ''' <summary>
    ''' 荷主明細取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function GetCustDetail(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", inTbl.Rows(0).Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_SELECT_CUST_DETAILS, inTbl.Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "GetCustDetail", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("SET_NAIYO_2", "SET_NAIYO_2")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, "CUST_DETAILS"))

    End Function

    ''' <summary>
    ''' 荷主明細取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>String</returns>
    ''' <remarks>2019/12/16 要望管理009513 add</remarks>
    Private Function GetCustDetail_9P(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", inTbl.Rows(0).Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_SELECT_CUST_DETAILS_9P, inTbl.Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "GetCustDetail_9P", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("SET_NAIYO_2", "SET_NAIYO_2")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, "CUST_DETAILS"))

    End Function

#End Region
    '要望番号:1350 terakawa 2012.08.24 End

    '2014/01/22 輸出情報追加 START
#Region "2014/01/22 輸出情報追加"

    ''' <summary>
    ''' 輸出情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectExportLData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "OUTKA_NO_L" _
                                            , "SHIP_NM" _
                                            , "DESTINATION" _
                                            , "BOOKING_NO" _
                                            , "VOYAGE_NO" _
                                            , "SHIPPER_CD" _
                                            , "SHIPPER_NM" _
                                            , "CONT_LOADING_DATE" _
                                            , "STORAGE_TEST_DATE" _
                                            , "STORAGE_TEST_TIME" _
                                            , "DEPARTURE_DATE" _
                                            , "CONTAINER_NO" _
                                            , "CONTAINER_NM" _
                                            , "CONTAINER_SIZE" _
                                            , "SYS_DEL_FLG" _
                                            , "UP_KBN" _
                                            }

        Dim SQL As String = String.Concat(LMC020DAC.SQL_SELECT_EXPORT_L)
        Return Me.SelectData(ds, "LMC020_EXPORT_L", SQL, str)

    End Function

#End Region
    '2014/01/22 輸出情報追加 END

    '2015.07.08 協立化学　シッピングマーク対応　追加START
#Region "シッピングマーク情報(HED)"

    ''' <summary>
    ''' シッピングマーク情報(HED)取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectMarkHedData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "OUTKA_NO_L" _
                                            , "OUTKA_NO_M" _
                                            , "CASE_NO_FROM" _
                                            , "CASE_NO_TO" _
                                            , "SYS_DEL_FLG" _
                                            , "UP_KBN" _
                                            }

        Dim SQL As String = String.Concat(LMC020DAC.SQL_SELECT_C_MARK_HED)
        Return Me.SelectData(ds, "LMC020_C_MARK_HED", SQL, str)

    End Function

#End Region
    '2015.07.08 協立化学　シッピングマーク対応　追加END

    '2015.07.21 協立化学　シッピングマーク対応　追加START
#Region "シッピングマーク情報(DTL)"

    ''' <summary>
    ''' シッピングマーク情報(DTL)取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectMarkDtlData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "OUTKA_NO_L" _
                                            , "OUTKA_NO_M" _
                                            , "MARK_EDA" _
                                            , "REMARK_INFO" _
                                            , "SYS_DEL_FLG" _
                                            , "UP_KBN" _
                                            }

        Dim SQL As String = String.Concat(LMC020DAC.SQL_SELECT_C_MARK_DTL)
        Return Me.SelectData(ds, "LMC020_C_MARK_DTL", SQL, str)

    End Function

#End Region
    '2015.07.21 協立化学　シッピングマーク対応　追加END

    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="str">マッピング先文字</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet _
                                , ByVal tblNm As String _
                                , ByVal sql As String _
                                , ByVal str As String() _
                                ) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = Nothing
        If ("LMC020_ZAI").Equals(tblNm) = True Then
            inTbl = ds.Tables("LMC020_OUTKA_S")
            If (0).Equals(inTbl.Rows.Count) = True Then
                Return ds
            End If
        ElseIf ("LMC020_UNSO_L").Equals(tblNm) = True Then
            'INTableの条件rowの格納
            inTbl = ds.Tables("LMC020IN")
            Me._Row2 = ds.Tables("LMC020_OUTKA_L").Rows(0)
            'START YANAI 要望番号853 まとめ処理対応
        ElseIf ("LMC020_ZAI_MATOME_OUT").Equals(tblNm) = True Then
            inTbl = ds.Tables("LMC020_ZAI_MATOME_IN")
            'END YANAI 要望番号853 まとめ処理対応
        Else
            inTbl = ds.Tables("LMC020IN")
        End If

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        If ("LMC020_ZAI").Equals(tblNm) = True Then
            Call Me.SetSelectParamZai(Me._SqlPrmList)
        ElseIf ("LMC020_SAGYO").Equals(tblNm) = True Then
            Call Me.SetSelectParamSagyo(Me._SqlPrmList)
        ElseIf ("LMC020_UNSO_L").Equals(tblNm) = True Then
            Call Me.SetSelectParamUnsoL(Me._SqlPrmList)
        ElseIf ("LMC020_CUST").Equals(tblNm) = True Then
            Call Me.SetSelectParamCust(Me._SqlPrmList)
            'START YANAI 要望番号853 まとめ処理対応
        ElseIf ("LMC020_ZAI_MATOME_OUT").Equals(tblNm) = True Then
            Call Me.SetSelectParamZaiMATOME(Me._SqlPrmList)
            'END YANAI 要望番号853 まとめ処理対応
        Else
            Call Me.SetSelectParam(Me._SqlPrmList)
        End If

        'スキーマ名設定
        sql = Me.SetSchemaNm(sql, _Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        Dim max As Integer = str.Length - 1
        For i As Integer = 0 To max
            map.Add(str(i), str(i))
        Next

        '処理件数の設定
        Return MyBase.SetSelectResultToDataSet(map, ds, reader, tblNm)

    End Function

    ''' <summary>
    ''' 運賃のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の運賃のデータ取得SQLの構築・発行</remarks>
    Private Function SelectUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_UNSO_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        'Me._StrSql.Length = 0
        '20171025 初期化方法間違い修正
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_SELECT_UNCHIN, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectUnchinData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
        map.Add("SEIQ_TARIFF_BUNRUI_KB", "SEIQ_TARIFF_BUNRUI_KB")

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, "F_UNCHIN_TRS")

    End Function

#If True Then      'ADD 2019/05/31 依頼番号 : 005136   【LMS】出荷毎に寄託価額✕商品　実際の金額を「LMC020 出荷データ編集」画面に出荷金額を表示(群馬本間) 

    ''' <summary>
    ''' 寄託価格のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の運賃のデータ取得SQLの構築・発行</remarks>
    Private Function SelectKitakugakuData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_UNSO_L")


        If inTbl.Rows.Count = 0 Then
            Return ds
        End If

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_SELECT_F_UNSOM_KITAKUGAKU, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectKitakugakuData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("KITAKU_GOODS_UP", "KITAKU_GOODS_UP")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("UNSO_TTL_QT", "UNSO_TTL_QT")

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020_UNSO_KITAKU")

    End Function
#End If

    ''' <summary>
    ''' 請求ヘッダ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求ヘッダ取得SQLの構築・発行</remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("F_UNCHIN_TRS")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        'Me._StrSql.Length = 0
        '20171025 初期化方法間違い修正
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR)) '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", Me._Row.Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
        Dim brCd As String = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMG000DAC.SQL_SELECT_KEIRI_CHK_DATE, brCd))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectGheaderData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_DATE", "SKYU_DATE")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020_UNCHIN_SKYU_DATE"))

    End Function

    '要望番号：612 nakamura 振替データ一括削除対応 START
    ''' <summary>
    ''' 振替データ一括削除用データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>振替データ一括削除用データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectFuriDelData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_FURI_DEL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC020DAC.SQL_SELECT_IN_L)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMC020DAC.SQL_FROM_IN_L)       'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMC020DAC.SQL_WHERE_IN_L)      'SQL構築(データ抽出用Where句)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの設定
        Call Me.SetSelectParamFuriDel(Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectFuriDelData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("FURI_NO", "FURI_NO")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("SYS_UPD_DATE_ZAI", "SYS_UPD_DATE_ZAI")
        map.Add("SYS_UPD_TIME_ZAI", "SYS_UPD_TIME_ZAI")
        map.Add("HIKIATE", "HIKIATE")
        map.Add("ZAI_REC_CNT", "ZAI_REC_CNT")

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020_FURI_DEL")

    End Function

    '要望番号：612 nakamura 振替データ一括削除対応 END

    ''' <summary>
    ''' FFEM入出荷EDIデータ(ヘッダ) 取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectInoutkaEdiHedFjfData(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMC020DAC.SQL_SELECT_INOUTKAEDI_HED_FJF)

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' パラメータの設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.NVARCHAR))

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            cmd.Parameters.Clear()

            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' DataReader→DataTable への転記
                Dim map As Hashtable = New Hashtable()

                ' 取得データの格納先のマッピング
                map.Add("ZFVYHKKBN", "ZFVYHKKBN")
                map.Add("ZFVYDENTYP", "ZFVYDENTYP")

                Return MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020_INOUTKAEDI_HED_FJF")

            End Using

        End Using

    End Function

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function GetTrnTblExits(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_TBL_EXISTS")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMC020DAC.SQL_GET_TRN_TBL_EXISTS)

        ' パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TBL_NM", Me._Row.Item("TBL_NM").ToString(), DBDataType.NVARCHAR))

        ' スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        Me._Row.Item("TBL_EXISTS") = "0"

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

            ' パラメータの反映
            For Each obj As Object In _SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.Read() Then
                    Me._Row.Item("TBL_EXISTS") = Convert.ToString(reader("TBL_EXISTS"))
                End If

            End Using

        End Using

        Return ds

    End Function

    'START 2012.11.26 振替一括削除対応 nakamura

#End Region

#Region "設定処理"

#Region "Insert"

#Region "OUTKA_L"

    ''' <summary>
    ''' 出荷(大)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertOutkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_OUT_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetOutkaLComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertOutkaLData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "OUTKA_M"

    ''' <summary>
    ''' 出荷(中)テーブル新規登録
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(中)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertOutkaMData(ByVal dr As DataRow) As DataRow

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_OUT_M, dr.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dr

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        'START YANAI 要望番号510
        'Call Me.SetOutkaMComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetOutkaMInsComParameter(Me._Row, Me._SqlPrmList)
        'END YANAI 要望番号510

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertOutkaMData", cmd)

        'SQLの発行
        rtn = MyBase.GetInsertResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

#End Region

#Region "OUTKA_S"

    ''' <summary>
    ''' 出荷(小)テーブル新規登録
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(小)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertOutkaSData(ByVal dr As DataRow) As DataRow

        Dim rtn As Integer = 0

        Dim brCd As String = dr.Item("NRS_BR_CD").ToString()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_OUT_S, brCd))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dr

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetOutkaSComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertOutkaSData", cmd)

        'SQLの発行
        rtn = MyBase.GetInsertResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return dr

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
        Dim inTbl As DataTable = ds.Tables("LMC020_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_UNSO_L _
                                                                       , ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetUnsoLComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertUnsoLData", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMC020_UNSO_M")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_UNSO_M _
                                                                       , ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()
            '条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
            'START YANAI 要望番号673
            'Call Me.SetUnsoMComParameter(Me._Row, Me._SqlPrmList)
            Call Me.SetUnsoMComParameter(Me._Row, Me._SqlPrmList, ds.Tables("LMC020_OUTKA_L").Rows(0).Item("INSUPD_FLG").ToString)
            'END YANAI 要望番号673

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertUnsoMData", cmd)

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
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_UNCHIN _
                                                                       , ds.Tables("F_UNCHIN_TRS").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList, "F_UNCHIN_TRS")
            Call Me.SetUnchinComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertUnchinData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "SHIHARAI"

    ''' <summary>
    ''' (支払)運賃テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>(支払)運賃新規登録SQLの構築・発行</remarks>
    Private Function InsertShiharaiData(ByVal ds As DataSet) As DataSet

        If ds.Tables("F_SHIHARAI_TRS").Rows.Count = 0 Then
            'F_SHIHARAI_TRSが0件ということは本来無いが、一応念のために0件の時はINSERT処理が行われないようにする
            Return ds
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("F_SHIHARAI_TRS")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_SHIHARAI _
                                                                       , ds.Tables("F_SHIHARAI_TRS").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList, "F_SHIHARAI_TRS")
            Call Me.SetShiharaiComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertShiharaiData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#Region "SAGYO"

    ''' <summary>
    ''' 作業テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertSagyoData(ByVal ds As DataSet, ByVal dr As DataRow) As DataSet

        'DataSetのIN情報を取得
        Dim outkaLDr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_SAGYO _
                                                                       , outkaLDr.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dr

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetSagyoParameter(Me._Row, Me._SqlPrmList, outkaLDr)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertSagyoData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return ds

    End Function

#End Region

#Region "ZAI_TRS"

    '''' <summary>
    '''' 在庫テーブル新規登録
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks>在庫テーブル新規登録SQLの構築・発行</remarks>
    'Private Function InsertZaiTrsData(ByVal ds As DataSet) As DataSet

    '    'DataSetのIN情報を取得
    '    Dim inTbl As DataTable = ds.Tables("LMC020_ZAI")

    '    Dim rtn As Integer = 0
    '    Dim drs As DataRow() = inTbl.Select(LMC020DAC.SELECT_INSERT_DATA, " INKA_NO_L , INKA_NO_M , INKA_NO_S ")
    '    Dim max As Integer = drs.Length - 1

    '    '対象レコードが存在する場合
    '    If -1 < max Then

    '        '他テーブル情報を設定
    '        Dim outkaLDr As DataRow = Me.GetOutkaLDataRow(ds, drs(0))
    '        Dim outkaMDr As DataRow = Nothing
    '        Dim outkaNoM As String = String.Empty
    '        Dim chkOutkaNoM As String = String.Empty

    '        'SQL文のコンパイル
    '        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_ZAI_TRS _
    '                                                                       , outkaLDr.Item("NRS_BR_CD").ToString()))

    '        For i As Integer = 0 To max

    '            '出荷中番が変わった場合
    '            chkOutkaNoM = drs(i).Item("OUTKA_NO_M").ToString()
    '            If outkaNoM.Equals(chkOutkaNoM) = False Then

    '                '出荷(中)の情報を取得
    '                outkaMDr = Me.GetOutkaMDataRow(ds, drs(i))

    '                '判定用変数を更新
    '                outkaNoM = chkOutkaNoM

    '            End If

    '            'SQLパラメータ初期化
    '            Me._SqlPrmList = New ArrayList()

    '            '条件rowの格納
    '            Me._Row = drs(i)

    '            'パラメータ設定
    '            Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
    '            Call Me.SetZaiTrsComParameter(Me._Row, outkaLDr, outkaMDr, Me.GetOutkaSDataRow(ds, drs(i)), Me._SqlPrmList, True)

    '            'パラメータの反映
    '            For Each obj As Object In Me._SqlPrmList
    '                cmd.Parameters.Add(obj)
    '            Next

    '            MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertZaiTrsData", cmd)

    '            'SQLの発行
    '            rtn = MyBase.GetInsertResult(cmd)

    '            'パラメータの初期化
    '            cmd.Parameters.Clear()

    '        Next

    '    End If

    '    Return ds

    'End Function

    '''' <summary>
    '''' 在庫テーブル新規登録（小分け先）
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks>在庫テーブル新規登録SQLの構築・発行</remarks>
    'Private Function InsertZaiTrsDataKOWAKE(ByVal ds As DataSet, ByVal newZaiRecNo As String) As DataSet

    '    Dim rtn As Integer = 0

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_ZAI_TRS _
    '                                                                   , Me._Row.Item("NRS_BR_CD").ToString()))

    '    Dim outkaSDr As DataRow() = ds.Tables("LMC020_OUTKA_S").Select(String.Concat("ZAI_REC_NO = '", Me._Row.Item("ZAI_REC_NO"), "' AND ", _
    '                                                                                 "UP_KBN = '0'"))
    '    Dim max As Integer = outkaSDr.Length - 1
    '    Dim outkaMDr As DataRow = Nothing
    '    For i As Integer = 0 To max
    '        If ("01").Equals(outkaSDr(i).Item("SMPL_FLAG")) = True Then
    '            outkaMDr = ds.Tables("LMC020_OUTKA_M").Select(String.Concat("OUTKA_NO_M = '", outkaSDr(i).Item("OUTKA_NO_M"), "'"))(0)

    '            'SQLパラメータ初期化
    '            Me._SqlPrmList = New ArrayList()

    '            'パラメータ設定
    '            Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
    '            Call Me.SetZaiTrsKowakeComParameter(Me._Row, outkaMDr, outkaSDr(i), Me._SqlPrmList, newZaiRecNo)

    '            'パラメータの反映
    '            For Each obj As Object In Me._SqlPrmList
    '                cmd.Parameters.Add(obj)
    '            Next

    '            MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertZaiTrsDataKOWAKE", cmd)

    '            'SQLの発行
    '            rtn = MyBase.GetInsertResult(cmd)

    '            'パラメータの初期化
    '            cmd.Parameters.Clear()
    '        End If
    '    Next

    '    Return ds

    'End Function

    '''' <summary>
    '''' 移動テーブル新規登録（小分け）
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks>移動テーブル新規登録SQLの構築・発行</remarks>
    'Private Function InsertIdoTrsDataKOWAKE(ByVal ds As DataSet, ByVal newZaiRecNo As String) As DataSet

    '    Dim rtn As Integer = 0

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_IDO_TRS _
    '                                                                   , Me._Row.Item("NRS_BR_CD").ToString()))

    '    Dim zaiDr As DataRow = ds.Tables("LMC020_ZAI").Select(String.Concat("ZAI_REC_NO = '", Me._Row.Item("ZAI_REC_NO"), "'"))(0)
    '    Dim outkaSDr As DataRow() = ds.Tables("LMC020_OUTKA_S").Select(String.Concat("ZAI_REC_NO = '", Me._Row.Item("ZAI_REC_NO"), "' AND ", _
    '                                                                                 "UP_KBN = '0'"))
    '    Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")
    '    Dim outLDr As DataRow = inTbl.Rows(0)
    '    Dim max As Integer = outkaSDr.Length - 1

    '    For i As Integer = 0 To max
    '        If ("01").Equals(outkaSDr(i).Item("SMPL_FLAG")) = True Then

    '            'SQLパラメータ初期化
    '            Me._SqlPrmList = New ArrayList()

    '            'パラメータ設定
    '            Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
    '            Call Me.SetIdoTrsKowakeComParameter(Me._Row, zaiDr, outkaSDr(i), outLDr, Me._SqlPrmList, newZaiRecNo)

    '            'パラメータの反映
    '            For Each obj As Object In Me._SqlPrmList
    '                cmd.Parameters.Add(obj)
    '            Next

    '            MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertIdoTrsDataKOWAKE", cmd)

    '            'SQLの発行
    '            rtn = MyBase.GetInsertResult(cmd)

    '            'パラメータの初期化
    '            cmd.Parameters.Clear()
    '        End If
    '    Next

    '    Return ds

    'End Function

    ''' <summary>
    ''' 新規登録（小分け先）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>新規登録SQLの構築・発行</remarks>
    Private Function InsertKOWAKE(ByVal ds As DataSet) As DataSet

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Nothing

        'START YANAI 要望番号772
        'Dim outkaSDr As DataRow() = ds.Tables("LMC020_OUTKA_S").Select(String.Concat("ZAI_REC_NO = '", Me._Row.Item("ZAI_REC_NO"), "' AND ", _
        '                                                                             "UP_KBN = '0'"))
        Dim outkaSDr As DataRow() = ds.Tables("LMC020_OUTKA_S").Select(String.Concat("ZAI_REC_NO = '", Me._Row.Item("ZAI_REC_NO"), "' AND ", _
                                                                                     "N_ZAI_REC_NO <> '' AND ", _
                                                                                     "UP_KBN = '0'"))
        'END YANAI 要望番号772

        'START YANAI 20110913 小分け対応
        If outkaSDr.Length <= 0 Then
            Return ds
        End If

        Dim newOutkaSDr As DataRow = ds.Tables("LMC020_OUTKA_S").NewRow
        'END YANAI 20110913 小分け対応
        Dim max As Integer = outkaSDr.Length - 1
        Dim outkaMDr As DataRow = Nothing
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")
        Dim outLDr As DataRow = inTbl.Rows(0)
        Dim zaiDr As DataRow = Nothing
        Dim newZaiRecNo As String = String.Empty

        'START YANAI 20110913 小分け対応
        'For i As Integer = 0 To max
        '    If ("01").Equals(outkaSDr(i).Item("SMPL_FLAG")) = True Then
        '        newZaiRecNo = NumberMasterUtility.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, outLDr.Item("NRS_BR_CD").ToString())

        '        outkaMDr = ds.Tables("LMC020_OUTKA_M").Select(String.Concat("OUTKA_NO_M = '", outkaSDr(i).Item("OUTKA_NO_M"), "'"))(0)

        '        '①在庫データ作成
        '        cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_ZAI_TRS _
        '                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        '        'SQLパラメータ初期化
        '        Me._SqlPrmList = New ArrayList()

        '        'パラメータ設定
        '        Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        '        Call Me.SetZaiTrsKowakeComParameter(Me._Row, outkaMDr, outkaSDr(i), Me._SqlPrmList, newZaiRecNo)

        '        'パラメータの反映
        '        For Each obj As Object In Me._SqlPrmList
        '            cmd.Parameters.Add(obj)
        '        Next

        '        MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertKOWAKE_ZAI", cmd)

        '        'SQLの発行
        '        rtn = MyBase.GetInsertResult(cmd)

        '        'パラメータの初期化
        '        cmd.Parameters.Clear()


        '        '②移動データ作成
        '        cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_IDO_TRS _
        '                                                               , Me._Row.Item("NRS_BR_CD").ToString()))

        '        zaiDr = ds.Tables("LMC020_ZAI").Select(String.Concat("ZAI_REC_NO = '", Me._Row.Item("ZAI_REC_NO"), "'"))(0)
        '        'SQLパラメータ初期化
        '        Me._SqlPrmList = New ArrayList()

        '        'パラメータ設定
        '        Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        '        Call Me.SetIdoTrsKowakeComParameter(Me._Row, zaiDr, outkaSDr(i), outLDr, Me._SqlPrmList, newZaiRecNo)

        '        'パラメータの反映
        '        For Each obj As Object In Me._SqlPrmList
        '            cmd.Parameters.Add(obj)
        '        Next

        '        MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertKOWAKE_IDO", cmd)

        '        'SQLの発行
        '        rtn = MyBase.GetInsertResult(cmd)

        '        'パラメータの初期化
        '        cmd.Parameters.Clear()

        '    End If
        'Next
        newOutkaSDr.Item("ALCTD_NB") = "0"
        newOutkaSDr.Item("ALCTD_QT") = "0"
        newOutkaSDr.Item("IRIME") = "0"
        'START YANAI 20110913 小分け対応
        'START YANAI 要望番号681
        'Dim num As New NumberMasterUtility
        'newZaiRecNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, outLDr.Item("NRS_BR_CD").ToString())
        newZaiRecNo = outkaSDr(0).Item("N_ZAI_REC_NO").ToString()
        'END YANAI 要望番号681
        'END YANAI 20110913 小分け対応
        For i As Integer = 0 To max
            If ("01").Equals(outkaSDr(i).Item("SMPL_FLAG")) = True Then
                '同じ在庫データの場合に、小分け先の在庫データと移動データは複数件作らず、1件にまとめるため、
                'ここで出荷(小)の値をnewOutkaSDrに入れ替えると共に、まとめている。
                newOutkaSDr.Item("TOU_NO") = outkaSDr(i).Item("TOU_NO").ToString()
                newOutkaSDr.Item("SITU_NO") = outkaSDr(i).Item("SITU_NO").ToString()
                newOutkaSDr.Item("ZONE_CD") = outkaSDr(i).Item("ZONE_CD").ToString()
                newOutkaSDr.Item("LOCA") = outkaSDr(i).Item("LOCA").ToString()
                newOutkaSDr.Item("IRIME") = Convert.ToString( _
                                                             Convert.ToDecimal(newOutkaSDr.Item("IRIME").ToString()) + _
                                                             Convert.ToDecimal(outkaSDr(i).Item("IRIME").ToString()))
                newOutkaSDr.Item("ALCTD_NB") = Convert.ToString( _
                                                                Convert.ToDecimal(newOutkaSDr.Item("ALCTD_NB").ToString()) + _
                                                                Convert.ToDecimal(outkaSDr(i).Item("ALCTD_NB").ToString()))
                newOutkaSDr.Item("ALCTD_QT") = Convert.ToString( _
                                                                Convert.ToDecimal(newOutkaSDr.Item("ALCTD_QT").ToString()) + _
                                                                Convert.ToDecimal(outkaSDr(i).Item("ALCTD_QT").ToString()))
                newOutkaSDr.Item("REC_NO") = outkaSDr(i).Item("REC_NO").ToString()
                'START YANAI 20110913 小分け対応
                'START YANAI 要望番号681
                'outkaSDr(i).Item("N_ZAI_REC_NO") = newZaiRecNo
                'END YANAI 要望番号681
                'END YANAI 20110913 小分け対応
            End If
        Next

        'START YANAI 20110913 小分け対応
        'newZaiRecNo = NumberMasterUtility.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, outLDr.Item("NRS_BR_CD").ToString())
        'END YANAI 20110913 小分け対応

        '①在庫データ作成
        cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_ZAI_TRS _
                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetZaiTrsKowakeComParameter(Me._Row, outkaMDr, newOutkaSDr, Me._SqlPrmList, newZaiRecNo)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertKOWAKE_ZAI", cmd)

        'SQLの発行
        rtn = MyBase.GetInsertResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()


        '②移動データ作成
        cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_IDO_TRS _
                                                               , Me._Row.Item("NRS_BR_CD").ToString()))

        zaiDr = ds.Tables("LMC020_ZAI").Select(String.Concat("ZAI_REC_NO = '", Me._Row.Item("ZAI_REC_NO"), "'"))(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetIdoTrsKowakeComParameter(Me._Row, zaiDr, newOutkaSDr, outLDr, Me._SqlPrmList, newZaiRecNo)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertKOWAKE_IDO", cmd)

        'SQLの発行
        rtn = MyBase.GetInsertResult(cmd)
        'END YANAI 20110913 小分け対応

        Return ds

    End Function

#End Region

    '2014/01/22 輸出情報追加 START
#Region "EXPORT_L"

    ''' <summary>
    ''' 輸出情報テーブル新規登録
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <remarks>輸出情報テーブル新規登録SQLの構築・発行</remarks>
    Private Sub InsertExportLData(ByVal dr As DataRow)

        Dim rtn As Integer = 0

        Dim brCd As String = dr.Item("NRS_BR_CD").ToString()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_EXPORT_L, brCd))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dr

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetExportLComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertExportLData", cmd)

        'SQLの発行
        rtn = MyBase.GetInsertResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

    End Sub

#End Region
    '2014/01/22 輸出情報追加 END

    '2015.07.08 協立化学　シッピング対応 追加START
#Region "C_MARK_HED"

    ''' <summary>
    ''' マーク(HED)テーブル新規登録
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マーク(HED)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertMarkHedData(ByVal dr As DataRow) As DataRow

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_C_MARK_HED, dr.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dr

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList, "LMC020_C_MARK_HED")
        Call Me.SetMarkHedInsComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertMarkHedData", cmd)

        'SQLの発行
        rtn = MyBase.GetInsertResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

#End Region
    '2015.07.08 協立化学　シッピング対応 追加END

    '2015.07.21 協立化学　シッピング対応 追加START
#Region "C_MARK_DTL"

    ''' <summary>
    ''' マーク(DTL)テーブル新規登録
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マーク(DTL)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertMarkDtlData(ByVal dr As DataRow) As DataRow

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_INSERT_C_MARK_DTL, dr.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dr

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList, "LMC020_C_MARK_HED")
        Call Me.SetMarkDtlInsComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertMarkDtlData", cmd)

        'SQLの発行
        rtn = MyBase.GetInsertResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

#End Region
    '2015.07.08 協立化学　シッピング対応 追加END

#End Region

#Region "Update"

#Region "OUTKA_L"

    ''' <summary>
    ''' 出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMC020DAC.SQL_UPDATE_OUTKA_L, SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetOutkaLComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateOutkaLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "OUTKA_M"

    ''' <summary>
    ''' 出荷(中)テーブル更新
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaMData(ByVal dr As DataRow, ByVal INSUPD_FLG As String) As DataRow

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_UPDATE_OUTKA_M _
                                                                       , dr.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dr

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetOutkaMComParameter(Me._Row, Me._SqlPrmList, INSUPD_FLG)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateOutkaMData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

#End Region

#Region "OUTKA_S"

    ''' <summary>
    ''' 出荷(小)テーブル更新
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(小)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaSData(ByVal dr As DataRow) As DataRow

        Dim rtn As Integer = 0

        Dim brCd As String = dr.Item("NRS_BR_CD").ToString()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_UPDATE_OUTKA_S, brCd))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'rowの格納
        Me._Row = dr

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetOutkaSComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateOutkaSData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

#End Region

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnsoLData(ByVal ds As DataSet) As DataSet

        '2013.07.04 追加START
        Dim BpUnsoWt As Decimal = 0
        '2013.07.04 追加END

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMC020DAC.SQL_UPDATE_UNSO_L, LMC020DAC.SQL_COM_UPDATE_CONDITION) _
                                                                       , ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        '2013.07.04 追加START
        BpUnsoWt = Me.SetBpUnsoWt(ds)
        If BpUnsoWt <> 0 Then
            Me._Row.Item("UNSO_WT") = BpUnsoWt
        End If
        '2013.07.04 追加END

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetUnsoLComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateUnsoLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

    '2013.07.04 追加START
    ''' <summary>
    ''' BP運送重量再計算
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>BP受信TBL(DTL)SQLの構築・発行</remarks>
    Private Function SetBpUnsoWt(ByVal ds As DataSet) As Decimal

        Dim BpUnsoWt As Decimal = 0

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_UNSO_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMC020DAC.SQL_GET_BP_EDI_OUT, Me._Row.Item("NRS_BR_CD").ToString()))
        'SQLパラメータ（システム項目）設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "SetBpUnsoWt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        If reader.Read() = True Then
            BpUnsoWt = Convert.ToDecimal(reader("BP_UNSO_WT"))
            reader.Close()
            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()
            Return BpUnsoWt
        End If

        reader.Close()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Return 0

    End Function
    '2013.07.04 追加END

#End Region

#Region "SAGYO"

    ''' <summary>
    ''' 作業テーブル更新(システム項目)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateSagyoData(ByVal dr As DataRow) As DataRow

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMC020DAC.SQL_UPDATE_SAGYO_SYS_DATETIME, LMC020DAC.SQL_COM_UPDATE_CONDITION) _
                                                                       , dr.Item("NRS_BR_CD").ToString()))

        'INTableの条件rowの格納
        Me._Row = dr
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetSagyoPkParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSagyoUpParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateSagyoData", cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = False Then
            Return dr
        End If

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

#End Region

#Region "ZAI_TRS"

    ''' <summary>
    ''' 在庫テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateZaiTrsData(ByVal ds As DataSet, ByVal dr As DataRow) As DataSet

        Dim rtn As Integer = 0

        '他テーブル情報を設定
        Dim OutkaLDr As DataRow = Nothing
        Dim OutkaMDr As DataRow = Nothing
        Dim OutkaSDr As DataRow = Nothing

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMC020DAC.SQL_UPDATE_ZAI_TRS, LMC020DAC.SQL_COM_UPDATE_CONDITION) _
                                                                       , dr.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dr

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetZaiTrsComParameter(Me._Row, OutkaLDr, OutkaMDr, OutkaSDr, Me._SqlPrmList, False)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateZaiTrsData", cmd)

        'SQLの発行
        If Me.UpdateResultChkNoMessage(cmd) = False Then


            '20161207 在庫テーブル更新エラー時にメッセージ表示(ロット、商品KEY)
            Dim LotNo As String = String.Empty
            MyBase.SetMessage("E931", New String() {
             dr.Item("LOT_NO").ToString(), dr.Item("GOODS_CD_NRS").ToString()
            })

            Return ds
        End If

        'パラメータの初期化
        cmd.Parameters.Clear()

        'START YANAI 要望番号681
        'If ("01").Equals(Me._Row.Item("ALCTD_KB_FLG")) = True Then
        If ("01").Equals(Me._Row.Item("ALCTD_KB_FLG")) = True AndAlso _
            ("00").Equals(Me._Row.Item("SMPL_FLAG_ZAI")) = True Then
            'END YANAI 要望番号681
            'Dim newZaiRecNo As String = NumberMasterUtility.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, dr.Item("NRS_BR_CD").ToString())
            ''小分け実施の場合は小分け先在庫を作成
            'ds = Me.InsertZaiTrsDataKOWAKE(ds, newZaiRecNo)
            ''小分け実施の場合は在庫移動を作成
            'ds = Me.InsertIdoTrsDataKOWAKE(ds, newZaiRecNo)
            '小分け実施の場合、小分け先在庫と在庫移動を作成
            ds = Me.InsertKOWAKE(ds)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 在庫テーブル更新（小分け先）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="outSDr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateZaiKOWAKE(ByVal ds As DataSet, ByVal outSDr() As DataRow) As DataSet

        Dim rtn As Integer = 0
        Dim nZaiRecNo As String = String.Empty

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMC020DAC.SQL_UPDATE_ZAI_TRS_KOWAKE) _
                                                                       , outSDr(0).Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = outSDr(0)

        Dim newOutkaSDr As DataRow = ds.Tables("LMC020_ZAI").NewRow
        Dim max As Integer = outSDr.Length - 1

        newOutkaSDr.Item("PORA_ZAI_NB") = "0"
        newOutkaSDr.Item("ALLOC_CAN_NB") = "0"
        newOutkaSDr.Item("PORA_ZAI_QT") = "0"
        newOutkaSDr.Item("ALLOC_CAN_QT") = "0"

        For i As Integer = 0 To max
            If String.IsNullOrEmpty(nZaiRecNo) = False AndAlso _
                (nZaiRecNo).Equals(outSDr(i).Item("N_ZAI_REC_NO").ToString()) = False Then

                'パラメータ設定
                Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
                Call Me.SetZaiKOWAKEComParameter(Me._Row, newOutkaSDr, Me._SqlPrmList)

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateZaiKOWAKE", cmd)

                'SQLの発行
                If Me.UpdateResultChk(cmd) = False Then
                    Return ds
                End If

                'パラメータの初期化
                cmd.Parameters.Clear()
                Me._SqlPrmList = New ArrayList()

                newOutkaSDr.Item("ZAI_REC_NO") = String.Empty
                newOutkaSDr.Item("PORA_ZAI_NB") = "0"
                newOutkaSDr.Item("ALLOC_CAN_NB") = "0"
                newOutkaSDr.Item("PORA_ZAI_QT") = "0"
                newOutkaSDr.Item("ALLOC_CAN_QT") = "0"
                nZaiRecNo = String.Empty

            End If

            If ("1").Equals(outSDr(i).Item("UP_KBN")) = True Then
                If String.IsNullOrEmpty(nZaiRecNo) = True Then
                    nZaiRecNo = outSDr(i).Item("N_ZAI_REC_NO").ToString()
                    newOutkaSDr.Item("ZAI_REC_NO") = outSDr(i).Item("N_ZAI_REC_NO").ToString()
                End If

                '同じ在庫データの場合に、小分け先の在庫データと移動データは複数件作らず、1件にまとめるため、
                'ここで出荷(小)の値をnewOutkaSDrに入れ替えると共に、まとめている。
                newOutkaSDr.Item("PORA_ZAI_NB") = Convert.ToString( _
                                                                Convert.ToDecimal(newOutkaSDr.Item("PORA_ZAI_NB").ToString()) + _
                                                                1)
                newOutkaSDr.Item("ALLOC_CAN_NB") = Convert.ToString( _
                                                                Convert.ToDecimal(newOutkaSDr.Item("ALLOC_CAN_NB").ToString()) + _
                                                                1)
                'START YANAI 要望番号811
                'newOutkaSDr.Item("PORA_ZAI_QT") = Convert.ToString( _
                '                                                Convert.ToDecimal(newOutkaSDr.Item("PORA_ZAI_QT").ToString()) + _
                '                                                Convert.ToDecimal(outSDr(i).Item("IRIME").ToString()) - _
                '                                                Convert.ToDecimal(outSDr(i).Item("ALCTD_QT").ToString()))
                newOutkaSDr.Item("PORA_ZAI_QT") = Convert.ToString( _
                                                                Convert.ToDecimal(newOutkaSDr.Item("PORA_ZAI_QT").ToString()) + _
                                                                Convert.ToDecimal(outSDr(i).Item("IRIME").ToString()))
                'END YANAI 要望番号811
                newOutkaSDr.Item("ALLOC_CAN_QT") = Convert.ToString( _
                                                    Convert.ToDecimal(newOutkaSDr.Item("ALLOC_CAN_QT").ToString()) + _
                                                    Convert.ToDecimal(outSDr(i).Item("IRIME").ToString()) - _
                                                    Convert.ToDecimal(outSDr(i).Item("ALCTD_QT").ToString()))

            End If

        Next

        If String.IsNullOrEmpty(nZaiRecNo) = False Then
            '
            'パラメータ設定
            Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
            Call Me.SetZaiKOWAKEComParameter(Me._Row, newOutkaSDr, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateZaiKOWAKE", cmd)

            'SQLの発行
            'START YANAI 要望番号930
            'If Me.UpdateResultChk(cmd) = False Then
            '    Return ds
            'End If
            MyBase.GetUpdateResult(cmd)
            'END YANAI 要望番号930

            'パラメータの初期化
            cmd.Parameters.Clear()

        End If

        Return ds

    End Function

#End Region

    '要望番号:997 terakawa 2012.10.22 Start
#Region "OUTKAEDI_M"

    ''' <summary>
    ''' EDI出荷(中)テーブル更新
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaEdiMData(ByVal dr As DataRow) As DataRow

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_UPDATE_OUTKAEDI_M _
                                                                       , dr.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dr

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetOutkaEdiMComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateOutkaEdiMData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

#End Region

#Region "OUTKAEDI_DTL"

    ''' <summary>
    ''' EDI受信テーブル更新
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaEdiDtlData(ByVal dr As DataRow, ByVal tblNm As String) As DataRow

        Dim rtn As Integer = 0

        '更新するテーブル名をセット
        Dim sqlDtlNm As String = String.Empty
        sqlDtlNm = String.Concat("UPDATE $LM_TRN$..", tblNm, " SET", vbNewLine)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sqlDtlNm & LMC020DAC.SQL_UPDATE_OUTKAEDI_DTL _
                                                                       , dr.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dr

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetOutkaEdiDtlComParameter(Me._Row, Me._SqlPrmList, tblNm)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateOutkaEdiDtlData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

#End Region
    '要望番号:997 terakawa 2012.10.22 End

    '2014/01/22 輸出情報追加 START
#Region "EXPORT_L"

    ''' <summary>
    ''' 輸出情報テーブル更新
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <remarks>輸出情報テーブル更新SQLの構築・発行</remarks>
    Private Sub UpdateExportLData(ByVal dr As DataRow)

        '条件rowの格納
        Me._Row = dr

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_UPDATE_EXPORT_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetExportLComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateExportLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

    End Sub

    ''' <summary>
    ''' 輸出情報テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>輸出情報テーブル更新SQLの構築・発行</remarks>
    Private Function ComExportLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_EXPORT_L")
        Dim max As Integer = inTbl.Rows.Count - 1

        Dim rtn As Integer = 0

        Dim drs As DataRow = Nothing
        Dim sysDelflg As String = String.Empty
        Dim upKbn As String = String.Empty

        For i As Integer = 0 To max
            drs = inTbl.Rows(i)
            sysDelflg = drs.Item("SYS_DEL_FLG").ToString()
            upKbn = drs.Item("UP_KBN").ToString()

            If ("0").Equals(sysDelflg) = True AndAlso ("0").Equals(upKbn) = True Then
                '新規保存処理
                Me.InsertExportLData(drs)

            ElseIf ("0").Equals(sysDelflg) = True AndAlso ("1").Equals(upKbn) = True Then
                '更新処理
                Me.UpdateExportLData(drs)

            ElseIf ("1").Equals(sysDelflg) = True Then
                '削除処理
                Me.UpdateExportLSysDelFlg(ds)

            End If

        Next

        Return ds

    End Function

#End Region
    '2014/01/22 輸出情報追加 END

    '2015.07.08 協立化学　シッピング対応 追加START
#Region "C_MARK_HED"

    ''' <summary>
    ''' マーク(HED)テーブル更新
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マーク(HED)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateMarkHedData(ByVal dr As DataRow) As DataRow

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_UPDATE_C_MARK_HED _
                                                                       , dr.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dr

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList, "LMC020_C_MARK_HED")
        Call Me.SetMarkHedInsComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateMarkHedData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

#End Region
    '2015.07.08 協立化学　シッピング対応 追加END

    '2015.07.21 協立化学　シッピング対応 追加START
#Region "C_MARK_DTL"

    ''' <summary>
    ''' マーク(DTL)テーブル更新
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マーク(DTL)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateMarkDtlData(ByVal dr As DataRow) As DataRow

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_UPDATE_C_MARK_DTL _
                                                                       , dr.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dr

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList, "LMC020_C_MARK_HED")
        Call Me.SetMarkDtlInsComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateMarkDtlData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

#End Region
    '2015.07.21 協立化学　シッピング対応 追加END

#Region "印刷"

    ''' <summary>
    ''' 印刷時の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdatePrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMC020DAC.SQL_UPDATE_PRINT), brCd))

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", Me._Row.Item("OUTKA_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_KB", Me._Row.Item("PRINT_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIHUDA_FLAG", Me._Row.Item("NIHUDA_FLAG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_FLAG", Me._Row.Item("COA_FLAG").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdatePrintData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function


    ''' <summary>
    ''' 印刷時の更新処理（分析表：COA）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdatePrintDataCOA(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMC020DAC.SQL_UPDATE_PRINT_COA), brCd))

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdatePrintDataCOA", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function
#End Region

#Region "M_DEST"

    ''' <summary>
    ''' 届先M更新　ADD 2018/05/14
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先M更新SQLの構築・発行</remarks>
    Private Function UpdateM_DEST(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_UPDATE_M_DEST _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_TEL", Me._Row.Item("DEST_TEL").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me._Row.Item("DEST_CD").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateM_DEST", cmd)

        'SQLの発行
        Dim result As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region
#End Region

#Region "InsertUpdateDelete割り振り"

#Region "OUTKA_M"

    ''' <summary>
    ''' 出荷(中)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function ComOutkaMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_M")
        Dim max As Integer = inTbl.Rows.Count - 1

        Dim rtn As Integer = 0

        Dim drs As DataRow = Nothing
        Dim sysDelflg As String = String.Empty
        Dim upKbn As String = String.Empty

        For i As Integer = 0 To max
            drs = inTbl.Rows(i)
            sysDelflg = drs.Item("SYS_DEL_FLG").ToString()
            upKbn = drs.Item("UP_KBN").ToString()

            If ("0").Equals(sysDelflg) = True AndAlso ("0").Equals(upKbn) = True Then
                '新規保存処理
                Me.InsertOutkaMData(drs)

            ElseIf ("0").Equals(sysDelflg) = True AndAlso ("1").Equals(upKbn) = True Then
                '更新処理
                Me.UpdateOutkaMData(drs, ds.Tables("LMC020_OUTKA_L").Rows(0).Item("INSUPD_FLG").ToString)

            ElseIf ("1").Equals(sysDelflg) = True Then
                '削除処理
                Me.UpdateOutkaMDelFlg(drs)

            End If

        Next

        Return ds

    End Function

#End Region

#Region "OUTKA_S"

    ''' <summary>
    ''' 出荷(小)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(小)テーブル更新SQLの構築・発行</remarks>
    Private Function ComOutkaSData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_S")
        Dim max As Integer = inTbl.Rows.Count - 1

        Dim rtn As Integer = 0

        Dim drs As DataRow = Nothing
        Dim sysDelflg As String = String.Empty
        Dim upKbn As String = String.Empty

        For i As Integer = 0 To max
            drs = inTbl.Rows(i)
            sysDelflg = drs.Item("SYS_DEL_FLG").ToString()
            upKbn = drs.Item("UP_KBN").ToString()

            If ("0").Equals(sysDelflg) = True AndAlso ("0").Equals(upKbn) = True Then
                '新規保存処理
                Me.InsertOutkaSData(drs)

            ElseIf ("0").Equals(sysDelflg) = True AndAlso ("1").Equals(upKbn) = True Then
                '更新処理
                Me.UpdateOutkaSData(drs)

            ElseIf ("1").Equals(sysDelflg) = True Then
                '削除処理
                Me.UpdateOutkaSDelFlg(drs)

            End If

        Next

        Return ds

    End Function

#End Region

#Region "ZAI_TRS"

    ''' <summary>
    ''' 在庫テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫テーブル更新SQLの構築・発行</remarks>
    Private Function ComZaiTrsData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_ZAI")
        'ソートをするために、強引にselectしている
        Dim drs As DataRow() = inTbl.Select(Nothing, "ZAI_REC_NO")

        Dim max As Integer = drs.Length - 1

        Dim rtn As Integer = 0

        Dim sysDelflg As String = String.Empty
        Dim upKbn As String = String.Empty
        Dim zai_rec_no As String = String.Empty

        'START YANAI 20110913 小分け対応
        Dim outSDr() As DataRow = Nothing
        'END YANAI 20110913 小分け対応

        'START YANAI 20110913 小分け対応
        Dim torikeshiFlg As String = String.Empty
        torikeshiFlg = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("TORIKESHI_FLG").ToString
        'END YANAI 20110913 小分け対応



        For i As Integer = 0 To max
            sysDelflg = drs(i).Item("SYS_DEL_FLG").ToString()
            upKbn = drs(i).Item("UP_KBN").ToString()

            'START YANAI 20110913 小分け対応
            If ("1").Equals(sysDelflg) = True Then
                outSDr = ds.Tables("LMC020_OUTKA_S").Select(String.Concat("ZAI_REC_NO = '", drs(i).Item("ZAI_REC_NO").ToString(), "' AND ", _
                                                              "N_ZAI_REC_NO <> '' AND ", _
                                                              "SYS_DEL_FLG = '0'"))
                If 0 < outSDr.Length Then
                    sysDelflg = "0"
                End If
            End If
            'END YANAI 20110913 小分け対応

            If (zai_rec_no).Equals(drs(i).Item("ZAI_REC_NO").ToString()) = False Then

                If ("0").Equals(sysDelflg) = True AndAlso _
                    (("0").Equals(upKbn) = True OrElse ("1").Equals(upKbn) = True) Then
                    '更新処理
                    Me.UpdateZaiTrsData(ds, drs(i))

                    'START YANAI 20110913 小分け対応
                    If ("0").Equals(upKbn) = False AndAlso _
                        ("01").Equals(torikeshiFlg) = False Then
                        '完了取消以外の時のみ行う

                        outSDr = ds.Tables("LMC020_OUTKA_S").Select(String.Concat("ZAI_REC_NO = '", drs(i).Item("ZAI_REC_NO").ToString(), "' AND ", _
                                                                                  "N_ZAI_REC_NO <> ''"))
                        If 0 < outSDr.Length Then
                            'N_ZAI_REC_NOが空以外のデータがヒットするということは、小分け
                            outSDr = ds.Tables("LMC020_OUTKA_S").Select(String.Concat("ZAI_REC_NO = '", drs(i).Item("ZAI_REC_NO").ToString(), "' AND ", _
                                                                                      "N_ZAI_REC_NO <> '' AND ", _
                                                                                      "SYS_DEL_FLG = '0'"), "N_ZAI_REC_NO")
                            If 0 < outSDr.Length Then
                                '移動データの更新
                                Me.UpdateIdoTrsData(ds, drs(i), outSDr(0).Item("REC_NO").ToString())

                                '在庫データの更新（小分け先）
                                Me.UpdateZaiKOWAKE(ds, outSDr)
                            Else
                                outSDr = ds.Tables("LMC020_OUTKA_S").Select(String.Concat("ZAI_REC_NO = '", drs(i).Item("ZAI_REC_NO").ToString(), "' AND ", _
                                                                                          "N_ZAI_REC_NO <> ''"))
                                '移動データの削除
                                Me.UpdateIdoDelFlg(ds, drs(i), outSDr(0).Item("REC_NO").ToString())
                            End If

                        End If
                    End If
                    'END YANAI 20110913 小分け対応

                ElseIf ("1").Equals(sysDelflg) = True AndAlso _
                    ("1").Equals(upKbn) = True Then
                    '削除処理
                    Me.UpdateZaiTrsDelFlg(ds, drs(i))

                End If

            End If

            zai_rec_no = drs(i).Item("ZAI_REC_NO").ToString()

        Next

        Return ds

    End Function

#End Region

#Region "SAGYO"

    ''' <summary>
    ''' 作業テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業テーブル更新SQLの構築・発行</remarks>
    Private Function ComSagyoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_SAGYO")
        Dim max As Integer = inTbl.Rows.Count - 1

        Dim rtn As Integer = 0

        Dim drs As DataRow = Nothing
        Dim sysDelflg As String = String.Empty
        Dim upKbn As String = String.Empty

        For i As Integer = 0 To max
            drs = inTbl.Rows(i)
            sysDelflg = drs.Item("SYS_DEL_FLG").ToString()
            upKbn = drs.Item("UP_KBN").ToString()

            If ("0").Equals(sysDelflg) = True AndAlso ("0").Equals(upKbn) = True Then
                '新規保存処理
                Me.InsertSagyoData(ds, drs)

            ElseIf ("0").Equals(sysDelflg) = True AndAlso ("1").Equals(upKbn) = True Then
                '更新処理
                Me.UpdateSagyoData(drs)

            ElseIf ("1").Equals(sysDelflg) = True OrElse ("2").Equals(upKbn) = True Then
                '削除処理
                Me.DeleteSagyoData(drs)

            End If

        Next

        Return ds

    End Function

#End Region

    '要望番号:997 terakawa 2012.10.22 Start
#Region "OUTKAEDI_M"

    ''' <summary>
    ''' EDI出荷(中)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function ComOutkaEdiMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_M")
        Dim max As Integer = inTbl.Rows.Count - 1

        Dim rtn As Integer = 0

        Dim drs As DataRow = Nothing

        For i As Integer = 0 To max
            drs = inTbl.Rows(i)

            '更新処理
            Me.UpdateOutkaEdiMData(drs)
        Next

        Return ds

    End Function

#End Region

#Region "OUTKAEDI_DTL"

    ''' <summary>
    ''' EDI受信テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信テーブル更新SQLの構築・発行</remarks>
    Private Function ComOutkaEdiDtlData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_M")
        Dim max As Integer = inTbl.Rows.Count - 1
        Dim tblNm As String = ds.Tables("LMC020_EDI_UPD_TBL").Rows(0).Item("EDI_UPD_RCV_TBL").ToString

        Dim rtn As Integer = 0

        Dim drs As DataRow = Nothing

        For i As Integer = 0 To max
            drs = inTbl.Rows(i)

            '更新処理
            Me.UpdateOutkaEdiDtlData(drs, tblNm)
        Next

        Return ds

    End Function

#End Region
    '要望番号:997 terakawa 2012.10.22 End

    '2015.07.08 協立化学　シッピング対応 追加START
#Region "C_MARK_HED"

    ''' <summary>
    ''' マーク(HED)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マーク(HED)テーブル更新SQLの構築・発行</remarks>
    Private Function ComMarkHedData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_C_MARK_HED")
        Dim max As Integer = inTbl.Rows.Count - 1

        Dim rtn As Integer = 0

        Dim drs As DataRow = Nothing
        Dim sysDelflg As String = String.Empty
        Dim upKbn As String = String.Empty

        For i As Integer = 0 To max
            drs = inTbl.Rows(i)
            sysDelflg = drs.Item("SYS_DEL_FLG").ToString()
            upKbn = drs.Item("UP_KBN").ToString()

            If ("0").Equals(sysDelflg) = True AndAlso ("0").Equals(upKbn) = True Then
                '新規保存処理
                Me.InsertMarkHedData(drs)

            ElseIf ("0").Equals(sysDelflg) = True AndAlso ("1").Equals(upKbn) = True Then
                '更新処理
                Me.UpdateMarkHedData(drs)

            ElseIf ("1").Equals(sysDelflg) = True Then
                '削除処理
                Me.UpdateMarkHedDelFlg(drs)

            End If

        Next

        Return ds

    End Function

#End Region
    '2015.07.08 協立化学　シッピング対応 追加END

    '2015.07.21 協立化学　シッピング対応 追加START
#Region "C_MARK_DTL"

    ''' <summary>
    ''' マーク(DTL)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マーク(DTL)テーブル更新SQLの構築・発行</remarks>
    Private Function ComMarkDtlData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_C_MARK_DTL")
        Dim max As Integer = inTbl.Rows.Count - 1

        Dim rtn As Integer = 0

        Dim drs As DataRow = Nothing
        Dim sysDelflg As String = String.Empty
        Dim upKbn As String = String.Empty

        For i As Integer = 0 To max
            drs = inTbl.Rows(i)
            sysDelflg = drs.Item("SYS_DEL_FLG").ToString()
            upKbn = drs.Item("UP_KBN").ToString()

            If ("0").Equals(sysDelflg) = True AndAlso ("0").Equals(upKbn) = True Then
                '新規保存処理
                Me.InsertMarkDtlData(drs)

            ElseIf ("0").Equals(sysDelflg) = True AndAlso ("1").Equals(upKbn) = True Then
                '更新処理
                Me.UpdateMarkDtlData(drs)

            ElseIf ("1").Equals(sysDelflg) = True Then
                '削除処理
                Me.UpdateMarkDtlDelFlg(drs)

            End If

        Next

        Return ds

    End Function

#End Region
    '2015.07.21 協立化学　シッピング対応 追加END

#End Region

#Region "Delete"

#Region "OUTKA_L"

    ''' <summary>
    ''' 出荷(大)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaLDelFlg(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(" UPDATE $LM_TRN$..C_OUTKA_L     " & vbNewLine)
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMC020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append("   AND OUTKA_NO_L = @OUTKA_NO_L   " & vbNewLine)
        Me._StrSql.Append(LMC020DAC.SQL_COM_UPDATE_CONDITION)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetUpdateDelFlgParameter(Me._Row, Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateOutkaLDelFlg", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "OUTKA_M"

    ''' <summary>
    '''出荷(中)の論理削除(該当SEQのみ)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(中)論理削除SQLの構築・発行</remarks>
    Private Function UpdateOutkaMDelFlg(ByVal dr As DataRow) As DataRow


        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(" UPDATE $LM_TRN$..C_OUTKA_M     " & vbNewLine)
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMC020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append("   AND OUTKA_NO_L = @OUTKA_NO_L  " & vbNewLine)
        Me._StrSql.Append("   AND OUTKA_NO_M = @OUTKA_NO_M  " & vbNewLine)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , dr.Item("NRS_BR_CD").ToString()))

        Me._Row = dr

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._Row, Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row.Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateOutkaMDelFlg", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータ初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

    ''' <summary>
    ''' 出荷(中)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(中)論理削除SQLの構築・発行</remarks>
    Private Function UpdateOutkaMSysDelFlg(ByVal ds As DataSet) As DataSet
        Return Me.UpdateComDelFlg(ds, "C_OUTKA_M")
    End Function
#End Region

#Region "OUTKA_S"

    ''' <summary>
    ''' 出荷(小)の論理削除(該当SEQのみ)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(小)論理削除SQLの構築・発行</remarks>
    Private Function UpdateOutkaSDelFlg(ByVal dr As DataRow) As DataRow

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(" UPDATE $LM_TRN$..C_OUTKA_S     " & vbNewLine)
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMC020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append("   AND OUTKA_NO_L = @OUTKA_NO_L   " & vbNewLine)
        Me._StrSql.Append("   AND OUTKA_NO_M = @OUTKA_NO_M   " & vbNewLine)
        Me._StrSql.Append("   AND OUTKA_NO_S = @OUTKA_NO_S   " & vbNewLine)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , dr.Item("NRS_BR_CD").ToString()))
        Me._Row = dr

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._Row, Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row.Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", Me._Row.Item("OUTKA_NO_S").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateOutkaSDelFlg", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータ初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

    ''' <summary>
    ''' 出荷(小)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(小)論理削除SQLの構築・発行</remarks>
    Private Function UpdateOutkaSSysDelFlg(ByVal ds As DataSet) As DataSet
        Return Me.UpdateComDelFlg(ds, "C_OUTKA_S")
    End Function

#End Region

#Region "SAGYO"

    ''' <summary>
    ''' 作業テーブルの物理削除処理
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業テーブル更新SQLの構築・発行</remarks>
    Private Function DeleteSagyoData(ByVal dr As DataRow) As DataRow

        'DataSetのIN情報を取得

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMC020DAC.SQL_DELETE_SAGYO, LMC020DAC.SQL_COM_UPDATE_CONDITION) _
                                                                       , dr.Item("NRS_BR_CD").ToString()))

        'INTableの条件rowの格納
        Me._Row = dr
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetSagyoPkParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataTimeParameter(Me._SqlPrmList, String.Empty)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "DeleteSagyoSysData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

    ' 2012.12.13 要望番号：612 振替一括削除対応 START Nakamura
    ''' <summary>
    ''' 振替削除時の入荷在側作業の物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DelSagyoDataIn(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_FURI_DEL")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_DELETE_SAGYO_IN _
                                                                       , ds.Tables("LMC020_FURI_DEL").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetSagyoInPkParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataTimeParameter(Me._SqlPrmList, String.Empty)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "DelSagyoDataIn", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return ds

    End Function

    ' 2012.12.13 要望番号：612 振替一括削除対応 END Nakamura

#End Region

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)テーブルの物理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル更新SQLの構築・発行</remarks>
    Private Function DeleteUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMC020DAC.SQL_DELETE_UNSO_L, LMC020DAC.SQL_COM_UPDATE_CONDITION) _
                                                                       , ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetUnsoLPkParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataTimeParameter(Me._SqlPrmList, String.Empty)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "DeleteUnsoLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "UNSO_M"

    ''' <summary>
    ''' 運送(中)テーブルの物理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(中)テーブル更新SQLの構築・発行</remarks>
    Private Function DeleteUnsoMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_DELETE_UNSO_M _
                                                                       , ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUnsoLPkParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "DeleteUnsoMData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "UNCHIN"

    ''' <summary>
    ''' 運賃テーブルの物理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル更新SQLの構築・発行</remarks>
    Private Function DeleteUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_DELETE_UNCHIN _
                                                                       , ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUnsoLPkParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "DeleteUnchinData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "SHIHARAI"

    ''' <summary>
    ''' (支払)運賃テーブルの物理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>(支払)運賃テーブル更新SQLの構築・発行</remarks>
    Private Function DeleteShiharaiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC020DAC.SQL_DELETE_SHIHARAI _
                                                                       , ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUnsoLPkParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "DeleteShiharaiData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    '2015.07.08 協立化学　シッピングマーク対応　追加START
#Region "C_MARK_HED"

    ''' <summary>
    '''マーク(HED)の論理削除
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マーク(HED)論理削除SQLの構築・発行</remarks>
    Private Function UpdateMarkHedDelFlg(ByVal dr As DataRow) As DataRow


        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(" UPDATE $LM_TRN$..C_MARK_HED     " & vbNewLine)
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMC020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append("   AND OUTKA_NO_L = @OUTKA_NO_L  " & vbNewLine)
        Me._StrSql.Append("   AND OUTKA_NO_M = @OUTKA_NO_M  " & vbNewLine)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , dr.Item("NRS_BR_CD").ToString()))

        Me._Row = dr

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUpdateMarkHedDelFlgParameter(Me._Row, Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row.Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateMarkHedDelFlg", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータ初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

    ''' <summary>
    ''' マーク(HED)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マーク(HED)の論理削除SQLの構築・発行</remarks>
    Private Function UpdateMarkHedSysDelFlg(ByVal ds As DataSet) As DataSet
        Return Me.UpdateComDelFlg(ds, "C_MARK_HED")
    End Function

#End Region
    '2015.07.08 協立化学　シッピングマーク対応　追加END

    '2015.07.21 協立化学　シッピングマーク対応　追加START
#Region "C_MARK_DTL"

    ''' <summary>
    '''マーク(DTL)の論理削除
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マーク(DTL)論理削除SQLの構築・発行</remarks>
    Private Function UpdateMarkDtlDelFlg(ByVal dr As DataRow) As DataRow


        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(" UPDATE $LM_TRN$..C_MARK_DTL     " & vbNewLine)
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMC020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append("   AND OUTKA_NO_L = @OUTKA_NO_L  " & vbNewLine)
        Me._StrSql.Append("   AND OUTKA_NO_M = @OUTKA_NO_M  " & vbNewLine)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , dr.Item("NRS_BR_CD").ToString()))

        Me._Row = dr

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUpdateMarkHedDelFlgParameter(Me._Row, Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row.Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateMarkHedDelFlg", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータ初期化
        cmd.Parameters.Clear()

        Return dr

    End Function

    ''' <summary>
    ''' マーク(DTL)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マーク(DTL)の論理削除SQLの構築・発行</remarks>
    Private Function UpdateMarkDtlSysDelFlg(ByVal ds As DataSet) As DataSet
        Return Me.UpdateComDelFlg(ds, "C_MARK_DTL")
    End Function

#End Region
    '2015.07.21 協立化学　シッピングマーク対応　追加END

#Region "ZAI_TRS"

    ''' <summary>
    ''' 在庫テーブルの更新（削除時）
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫テーブル更新QLの構築・発行</remarks>
    Private Function UpdateZaiTrsDelFlg(ByVal ds As DataSet, ByVal dr As DataRow) As DataSet

        Dim rtn As Integer = 0
        Dim rtnDs As DataSet = ds
        '他テーブル情報を設定
        Dim OutkaLDr As DataRow = Nothing
        Dim OutkaMDr As DataRow = Nothing
        Dim OutkaSDr As DataRow = Nothing

        If ds.Tables("LMC020_OUTKA_S").Rows.Count = 0 Then
            '出荷(小)がない場合は終了
            Return ds
        End If

        'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
        ''SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMC020DAC.SQL_UPDATE_ZAI_TRS, LMC020DAC.SQL_COM_UPDATE_CONDITION) _
        '                                                               , dr.Item("NRS_BR_CD").ToString()))
        '条件rowの格納
        Me._Row = dr

        Dim outSDr() As DataRow = Nothing
        outSDr = ds.Tables("LMC020_OUTKA_S").Select("N_ZAI_REC_NO <> ''")
        Dim max As Integer = outSDr.Length - 1
        outSDr = ds.Tables("LMC020_OUTKA_S").Select(String.Concat("ZAI_REC_NO = '", Me._Row.Item("ZAI_REC_NO").ToString(), "'"))
        Dim outMDr() As DataRow = Nothing
        'START KIM 要望番号1487 引当済みの中データ（小でも発生）を①行削除してから大データを出荷削除した場合、アベンド
        'If outSDr.Length > 0 Then
        If outSDr IsNot Nothing AndAlso outSDr.Length > 0 Then
            'END KIM 要望番号1487 引当済みの中データ（小でも発生）を①行削除してから大データを出荷削除した場合、アベンド
            outMDr = ds.Tables("LMC020_OUTKA_M").Select(String.Concat("OUTKA_NO_M = '", outSDr(0).Item("OUTKA_NO_M").ToString(), "' AND ", _
                                                                      "ALCTD_KB = '03'"))
        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Nothing
        'START KIM 要望番号1487 引当済みの中データ（小でも発生）を①行削除してから大データを出荷削除した場合、アベンド
        'If outMDr.Length = 0 Then
        If outMDr Is Nothing OrElse outMDr.Length = 0 Then
            'END KIM 要望番号1487 引当済みの中データ（小でも発生）を①行削除してから大データを出荷削除した場合、アベンド
            '小分け以外の場合
            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMC020DAC.SQL_UPDATE_ZAI_TRS_DEL, LMC020DAC.SQL_COM_UPDATE_CONDITION) _
                                                                       , dr.Item("NRS_BR_CD").ToString()))
        Else
            'Start要望管理2014 小分け削除時、元在庫データにマージしない。
            'If max >= 0 Then
            '    '小分けの場合(出荷時、在庫数が1より多い場合)
            '    cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMC020DAC.SQL_UPDATE_ZAI_TRS_DEL_KOWAKE) _
            '                                                               , dr.Item("NRS_BR_CD").ToString()))
            'Else
            '小分けの場合(出荷時、在庫数が1以下の場合)
            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMC020DAC.SQL_UPDATE_ZAI_TRS_DEL_KOWAKE2) _
                                                                       , dr.Item("NRS_BR_CD").ToString()))
            'End If
            'End要望管理2014 小分け削除時、元在庫データにマージしない。
        End If
        'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
        ''条件rowの格納
        'Me._Row = dr
        'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
        'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
        'Call Me.SetZaiTrsComParameter(Me._Row, OutkaLDr, OutkaMDr, OutkaSDr, Me._SqlPrmList, False)
        'START KIM 要望番号1487 引当済みの中データ（小でも発生）を①行削除してから大データを出荷削除した場合、アベンド
        'If outMDr.Length = 0 Then
        If outMDr Is Nothing OrElse outMDr.Length = 0 Then
            'END KIM 要望番号1487 引当済みの中データ（小でも発生）を①行削除してから大データを出荷削除した場合、アベンド
            '小分け以外の場合
            Call Me.SetZaiTrsDelComParameter(Me._Row, Me._SqlPrmList)
        Else
            'Start要望管理2014 小分け削除時、元在庫データにマージしない。
            If max >= 0 Then
                '小分けの場合(出荷時、在庫数が1より多い場合)
                Call Me.SetZaiTrsDelKowakeParameter(Me._Row, Me._SqlPrmList, outSDr)
            Else
                '小分けの場合(出荷時、在庫数が1以下の場合)
                Call Me.SetZaiTrsDelKowakeParameter2(Me._Row, Me._SqlPrmList, outSDr)
            End If
            'End要望管理2014 小分け削除時、元在庫データにマージしない。
        End If
        'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateZaiTrsDelFlg", cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = False Then
            Return ds
        End If

        'パラメータの初期化
        cmd.Parameters.Clear()

        'START YANAI 20110913 小分け対応
        'If ("01").Equals(Me._Row.Item("ALCTD_KB_FLG")) = True Then
        '    '小分け実施の場合は小分け先在庫を削除する必要があるため、下記にて実行
        '    '①出荷小のREC_NOから、移動先の在庫がすでに出荷されていないか、データ検索をする
        '    rtnDs = Me.SelectZaiKowakeData(ds)

        '    'メッセージコードの設定
        '    Dim dtCnt As Integer = rtnDs.Tables("LMC020_KOWAKE").Rows.Count - 1
        '    If dtCnt = 0 Then
        '        '②小分け先のデータが存在する時、小分け先在庫データの削除処理
        '        Me.UpdateZaiKowakeDelFlg(rtnDs)

        '    End If

        'End If

        'Start要望管理2014 小分け削除時、元在庫データにマージしない。
        ''小分け実施の場合は小分け先在庫を削除する必要があるため、下記にて実行
        ''START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
        ''Dim outSDr() As DataRow = Nothing
        ''END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
        'For i As Integer = 0 To max
        '    'メッセージコードの設定
        '    '①小分け先のデータが存在する時、小分け先在庫データの削除処理
        '    Me.UpdateZaiKowakeDelFlg(ds, outSDr(i))

        '    '②小分け先のデータが存在する時、移動データの削除処理
        '    Me.UpdateIdoDelFlg(ds, outSDr(i), outSDr(i).Item("REC_NO").ToString())
        'Next
        ''END YANAI 20110913 小分け対応
        'End要望管理2014 小分け削除時、元在庫データにマージしない。

        Return ds

    End Function

#End Region

#Region "ZAI_TRS 小分け"

    'START YANAI 20110913 小分け対応
    '''' <summary>
    '''' 小分け先在庫の論理削除
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks></remarks>
    'Private Function UpdateZaiKowakeDelFlg(ByVal ds As DataSet) As DataSet
    ''' <summary>
    ''' 小分け先在庫の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateZaiKowakeDelFlg(ByVal ds As DataSet, ByVal dr As DataRow) As DataSet
        'END YANAI 20110913 小分け対応

        'START YANAI 20110913 小分け対応
        'DataSetのIN情報を取得
        'Dim inTbl As DataTable = ds.Tables("LMC020_KOWAKE")
        'END YANAI 20110913 小分け対応

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(" UPDATE $LM_TRN$..D_ZAI_TRS     " & vbNewLine)
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMC020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append("   AND ZAI_REC_NO = @ZAI_REC_NO   " & vbNewLine)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._Row, Me._SqlPrmList)
        'START YANAI 20110913 小分け対応
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", inTbl.Rows(0).Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", dr.Item("N_ZAI_REC_NO").ToString(), DBDataType.CHAR))
        'END YANAI 20110913 小分け対応

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateZaiKowakeDelFlg", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

    'START YANAI 20110913 小分け対応
#Region "IDO_TRS 小分け"

    ''' <summary>
    ''' 移動データの論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateIdoDelFlg(ByVal ds As DataSet, ByVal dr As DataRow, ByVal recNo As String) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(" UPDATE $LM_TRN$..D_IDO_TRS     " & vbNewLine)
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMC020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append("   AND REC_NO = @REC_NO         " & vbNewLine)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._Row, Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", recNo, DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateIdoDelFlg", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 移動データの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateIdoTrsData(ByVal ds As DataSet, ByVal dr As DataRow, ByVal recNo As String) As DataSet

        Dim outkaSDr As DataRow() = ds.Tables("LMC020_OUTKA_S").Select(String.Concat("ZAI_REC_NO = '", Me._Row.Item("ZAI_REC_NO"), "' AND ", _
                                                                             "SYS_DEL_FLG = '0' AND ", _
                                                                             "UP_KBN = '0'"))

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(LMC020DAC.SQL_UPDATE_IDO_TRS)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectParamIdo(Me._SqlPrmList, dr, recNo, outkaSDr)
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateIdoTrsData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region
    'END YANAI 20110913 小分け対応


    '2012.11.28 要望番号：612 振替削除対応 nakamura START
#Region "INKA_L"
    ''' <summary>
    ''' 入荷(大)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaLDelFlg(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_FURI_DEL")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        'Me._StrSql.Length = 0
        '20171025 初期化方法間違い修正
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(" UPDATE $LM_TRN$..B_INKA_L      " & vbNewLine)
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMC020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append("   AND INKA_NO_L = @INKA_NO_L   " & vbNewLine)
        ''Me._StrSql.Append(LMC020DAC.SQL_COM_UPDATE_CONDITION)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetUpdateDelFlgParameter(Me._Row, Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row.Item("INKA_NO_L").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInkaLDelFlg", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "INKA_M"

    ''' <summary>
    ''' 入荷(中)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(中)論理削除SQLの構築・発行</remarks>
    Private Function UpdateInkaMSysDelFlg(ByVal ds As DataSet) As DataSet
        Return Me.UpdateComDelFlgIN(ds, "B_INKA_M")
    End Function

#End Region

#Region "INKA_S"

    ''' <summary>
    ''' 入荷(小)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(小)論理削除SQLの構築・発行</remarks>
    Private Function UpdateInkaSSysDelFlg(ByVal ds As DataSet) As DataSet
        Return Me.UpdateComDelFlgIN(ds, "B_INKA_S")
    End Function

#End Region

    '2012.11.28 要望番号：612 振替削除対応 nakamura END

#Region "ZAI_TRS"

    ''' <summary>
    ''' 入荷在庫の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateInZaiDelFlg(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_FURI_DEL")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        'Me._StrSql.Length = 0
        '20171025 初期化方法間違い修正
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(" UPDATE $LM_TRN$..D_ZAI_TRS     " & vbNewLine)
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMC020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append("   AND INKA_NO_L = @INKA_NO_L   " & vbNewLine)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        Call Me.SetUpdateDelFlgParameter(Me._Row, Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row.Item("INKA_NO_L").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInZaiDelFlg", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

    '2014/01/22 輸出情報追加 START
#Region "EXPORT_L"

    ''' <summary>
    ''' 輸出情報の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>輸出情報論理削除SQLの構築・発行</remarks>
    Private Sub UpdateExportLSysDelFlg(ByVal ds As DataSet)
        Me.UpdateComDelFlg(ds, "C_EXPORT_L")
    End Sub

#End Region
    '2014/01/22 輸出情報追加 END

#Region "H_OUTKAEDI_DTL_RAPI(Rapidus次回分納情報)"

    ''' <summary>
    ''' H_OUTKAEDI_DTL_RAPI(Rapidus次回分納情報) 論理削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDelFlgOutkaEdiDtlRapiJikaiBunnou(ByVal ds As DataSet) As DataSet

        ' テーブル名
        Const IN_TBL_NM As String = "LMC020_JIKAI_BUNNOU"

        Dim nrsBrCd As String = ds.Tables(IN_TBL_NM).Rows(0).Item("NRS_BR_CD").ToString()

        ' 更新件数初期化
        Dim cnt As Integer = 0

        ' SQLの編集
        Dim sql As String = Me.SetSchemaNm(LMC020DAC.SQL_UPDATE_DEL_FLG_JIKAI_BUNNOU_OUTKAEDI_DTL_RAPI, nrsBrCd)

        ' SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            For i As Integer = 0 To ds.Tables(IN_TBL_NM).Rows.Count() - 1
                Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(i)

                ' SQLパラメータの設定
                Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
                Me._SqlPrmList = New ArrayList()
                With Me._SqlPrmList
                    .Add(MyBase.GetSqlParameter("@DEL_KB", BaseConst.FLG.ON, DBDataType.CHAR))
                    .Add(MyBase.GetSqlParameter("@CRT_DATE", inRow.Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@FILE_NAME", inRow.Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_CTL_NO", inRow.Item("EDI_CTL_NO").ToString(), DBDataType.VARCHAR))

                    .Add(MyBase.GetSqlParameter("@DELETE_DATE", MyBase.GetSystemDate(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@DELETE_TIME", updTime, DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@DELETE_USER", MyBase.GetUserID(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.VARCHAR))
                End With
                Call Me.SetUpdateDelFlgParameter(inRow, Me._SqlPrmList)
                cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

                ' ログ出力
                MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

                cnt += MyBase.GetInsertResult(cmd)

                cmd.Parameters.Clear()

            Next

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region ' "H_OUTKAEDI_DTL_RAPI(Rapidus次回分納情報)"

#Region "H_OUTKAEDI_L(次回分納情報)"

    ''' <summary>
    ''' H_OUTKAEDI_L(次回分納情報) 論理削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDelFlgOutkaEdiL_JikaiBunnou(ByVal ds As DataSet) As DataSet

        ' テーブル名
        Const IN_TBL_NM As String = "LMC020_JIKAI_BUNNOU"

        Dim nrsBrCd As String = ds.Tables(IN_TBL_NM).Rows(0).Item("NRS_BR_CD").ToString()

        ' 更新件数初期化
        Dim cnt As Integer = 0

        ' SQLの編集
        Dim sql As String = Me.SetSchemaNm(LMC020DAC.SQL_UPDATE_DEL_FLG_JIKAI_BUNNOU_OUTKAEDI_L, nrsBrCd)

        ' SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            For i As Integer = 0 To ds.Tables(IN_TBL_NM).Rows.Count() - 1
                Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(i)

                ' SQLパラメータの設定
                Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
                Me._SqlPrmList = New ArrayList()
                With Me._SqlPrmList
                    .Add(MyBase.GetSqlParameter("@DEL_KB", BaseConst.FLG.ON, DBDataType.CHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_CTL_NO", inRow.Item("EDI_CTL_NO").ToString(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_L_SYS_UPD_DATE", inRow.Item("EDI_L_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_L_SYS_UPD_TIME", inRow.Item("EDI_L_SYS_UPD_TIME").ToString(), DBDataType.CHAR))

                    .Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.VARCHAR))
                End With
                Call Me.SetUpdateDelFlgParameter(inRow, Me._SqlPrmList)
                cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

                ' ログ出力
                MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

                cnt += MyBase.GetInsertResult(cmd)

                cmd.Parameters.Clear()

            Next

        End Using

        MyBase.SetResultCount(cnt)

        ' 排他エラー判定
        ' (EDI出荷L レコード自体なしのデータ不整合は更新前にエラーチェック済)
        If cnt <> ds.Tables(IN_TBL_NM).Rows.Count() Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region ' "H_OUTKAEDI_L(次回分納情報)"

#Region "H_OUTKAEDI_M(次回分納情報)"

    ''' <summary>
    ''' H_OUTKAEDI_M(次回分納情報) 論理削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDelFlgOutkaEdiM_JikaiBunnou(ByVal ds As DataSet) As DataSet

        ' テーブル名
        Const IN_TBL_NM As String = "LMC020_JIKAI_BUNNOU"

        Dim nrsBrCd As String = ds.Tables(IN_TBL_NM).Rows(0).Item("NRS_BR_CD").ToString()

        ' 更新件数初期化
        Dim cnt As Integer = 0

        ' SQLの編集
        Dim sql As String = Me.SetSchemaNm(LMC020DAC.SQL_UPDATE_DEL_FLG_JIKAI_BUNNOU_OUTKAEDI_M, nrsBrCd)

        ' SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            For i As Integer = 0 To ds.Tables(IN_TBL_NM).Rows.Count() - 1
                Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(i)

                ' SQLパラメータの設定
                Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
                Me._SqlPrmList = New ArrayList()
                With Me._SqlPrmList
                    .Add(MyBase.GetSqlParameter("@DEL_KB", BaseConst.FLG.ON, DBDataType.CHAR))
                    .Add(MyBase.GetSqlParameter("@EDI_CTL_NO", inRow.Item("EDI_CTL_NO").ToString(), DBDataType.VARCHAR))

                    .Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.VARCHAR))
                    .Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.VARCHAR))
                End With
                Call Me.SetUpdateDelFlgParameter(inRow, Me._SqlPrmList)
                cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

                ' ログ出力
                MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

                cnt += MyBase.GetInsertResult(cmd)

                cmd.Parameters.Clear()

            Next

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region ' "H_OUTKAEDI_M(次回分納情報)"

#Region "SQL"

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系のスキーマ名を設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系のスキーマ名を設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#End Region

#Region "パラメータ"

    ''' <summary>
    ''' パラメータ設定モジュール(マスタ存在チェック)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParam(ByVal prmList As ArrayList)

        With Me._Row

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(マスタ存在チェック)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParamSagyo(ByVal prmList As ArrayList)

        With Me._Row

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", String.Concat(.Item("OUTKA_NO_L").ToString(), "%"), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(マスタ存在チェック)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParamZai(ByVal prmList As ArrayList)

        With Me._Row

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(請求鑑ヘッダ・保管料取込日)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParamKagami(ByVal prmList As ArrayList, ByVal seiqtocd As String)

        With Me._Row

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", seiqtocd, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(マスタ存在チェック)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParamUnsoL(ByVal prmList As ArrayList)

        With Me._Row

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

            '請求運賃計算締め基準の値によって、チェック対象の日付を変更
            Dim checkDate As String = String.Empty
            If ("01").Equals(Me._Row2.Item("UNTIN_CALCULATION_KB")) = True Then
                checkDate = Me._Row2.Item("OUTKA_PLAN_DATE").ToString
            Else
                checkDate = Me._Row2.Item("ARR_PLAN_DATE").ToString
            End If
            prmList.Add(MyBase.GetSqlParameter("@STR_DATE", checkDate, DBDataType.CHAR))

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            '支払運賃計算の基準日は納入予定日とする。
            Dim checkDate2 As String = String.Empty
            checkDate2 = Me._Row2.Item("ARR_PLAN_DATE").ToString
            prmList.Add(MyBase.GetSqlParameter("@STR_DATE2", checkDate2, DBDataType.CHAR))
            'END UMANO 要望番号1302 支払運賃に伴う修正。

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(マスタ存在チェック)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectKowakeParam(ByVal prmList As ArrayList)

        With Me._Row

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", .Item("OUTKA_NO_S").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(荷主マスタ)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParamCust(ByVal prmList As ArrayList)

        With Me._Row

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))

        End With

    End Sub

    'START YANAI 20110913 小分け対応
    ''' <summary>
    ''' パラメータ設定モジュール(移動データ)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParamIdo(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal recNo As String, ByVal outkaSDr() As DataRow)

        With Me._Row

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", recNo, DBDataType.CHAR))

            'START YANAI 要望番号811
            'prmList.Add(MyBase.GetSqlParameter("@NB", Convert.ToString(outkaSDr.Length - 1), DBDataType.CHAR))
            Dim nb As Integer = outkaSDr.Length - 1
            If nb < 0 Then
                nb = 0
            End If
            prmList.Add(MyBase.GetSqlParameter("@NB", Convert.ToString(nb), DBDataType.CHAR))
            'END YANAI 要望番号811

        End With

    End Sub
    'END YANAI 20110913 小分け対応

    'START YANAI 要望番号853 まとめ処理対応
    ''' <summary>
    ''' パラメータ設定モジュール(マスタ存在チェック)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParamZaiMATOME(ByVal prmList As ArrayList)

        With Me._Row

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", .Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", .Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LT_DATE", .Item("LT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OFB_KB", .Item("OFB_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", .Item("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", .Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))

            If String.IsNullOrEmpty(.Item("INKO_DATE").ToString()) = False Then
                prmList.Add(MyBase.GetSqlParameter("@INKO_DATE", .Item("INKO_DATE").ToString(), DBDataType.CHAR))
            Else
                prmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE", .Item("INKO_PLAN_DATE").ToString(), DBDataType.CHAR))
            End If

        End With

    End Sub
    'END YANAI 要望番号853 まとめ処理対応

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", systemTime, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", systemUserID, DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetSysdataParameter(prmList, dataSetNm)

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList, dataSetNm)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

        'START UMANO 要望番号1302 支払運賃に伴う修正。
        'If ("F_UNCHIN_TRS").Equals(dataSetNm) = False Then
        If ("F_UNCHIN_TRS").Equals(dataSetNm) = True OrElse _
           ("F_SHIHARAI_TRS").Equals(dataSetNm) = True OrElse _
           ("LMC020_C_MARK_HED").Equals(dataSetNm) = True Then
        Else
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            Me._Row.Item("SYS_UPD_DATE") = systemDate
            Me._Row.Item("SYS_UPD_TIME") = systemTime
        End If

    End Sub

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime(ByVal dr As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", dr.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", dr.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 論理削除時の共通パラメータ設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdateDelFlgParameter(ByVal dr As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dr.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.ON, DBDataType.CHAR))
        Call Me.SetSysdataParameter(prmList, String.Empty)

    End Sub

    '2015.07.08 協立化学　シッピング対応 追加START
    ''' <summary>
    ''' 論理削除時の共通パラメータ設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdateMarkHedDelFlgParameter(ByVal dr As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dr.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.ON, DBDataType.CHAR))
        Call Me.SetSysdataParameter(prmList, "LMC020_C_MARK_HED")

    End Sub
    '2015.07.08 協立化学　シッピング対応 追加END

    ''2012.11.28 要望番号：612 振替削除対応 START
    '''' <summary>
    '''' 論理削除時の共通パラメータ設定
    '''' </summary>
    '''' <param name="prmList">パラメータ</param>
    '''' <param name="brCd">営業所コード</param>
    '''' <remarks></remarks>
    'Private Function SetUpdateDelFlgParameter(ByVal prmList As ArrayList, ByVal brCd As String) As String()

    '    prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
    '    prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.ON, DBDataType.CHAR))
    '    SetUpdateDelFlgParameter = Me.SetSysdataParameter(prmList)
    '    Return SetUpdateDelFlgParameter

    'End Function
    '2012.11.28 要望番号：612 振替削除対応 START

    ''' <summary>
    ''' 出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FURI_NO", .Item("FURI_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", .Item("OUTKA_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", .Item("SYUBETU_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", .Item("OUTKA_STATE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", .Item("OUTKAHOKOKU_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", .Item("PICK_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", .Item("DENP_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_KANRYO_INFO", .Item("ARR_KANRYO_INFO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", .Item("OUTKO_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", .Item("HOKOKU_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", .Item("TOUKI_HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@END_DATE", .Item("END_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", .Item("SHIP_CD_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", .Item("SHIP_CD_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", .Item("DEST_AD_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", .Item("DEST_TEL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", .Item("NHS_REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", .Item("SP_NHS_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", .Item("CUST_ORD_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", .Item("BUYER_ORD_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DENP_YN", .Item("DENP_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", .Item("PC_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", .Item("NIYAKU_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALL_PRINT_FLAG", .Item("ALL_PRINT_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIHUDA_FLAG", .Item("NIHUDA_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_FLAG", .Item("NHS_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_FLAG", .Item("DENP_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_FLAG", .Item("COA_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_FLAG", .Item("HOKOKU_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATOME_PICK_FLAG", .Item("MATOME_PICK_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_DATE", .Item("LAST_PRINT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_TIME", .Item("LAST_PRINT_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SASZ_USER", .Item("SASZ_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_USER", .Item("OUTKO_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEN_USER", .Item("KEN_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", .Item("OUTKA_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOU_USER", .Item("HOU_USER").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ORDER_TYPE", .Item("ORDER_TYPE").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_KB", .Item("DEST_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM2", .Item("DEST_NM2").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", .Item("DEST_AD_1").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", .Item("DEST_AD_2").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UP_KBN", .Item("UP_KBN").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", .Item("WH_TAB_STATUS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_YN", .Item("WH_TAB_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@URGENT_YN", .Item("URGENT_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_SIJI_REMARK", .Item("WH_SIJI_REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_NO_SIJI_FLG", .Item("WH_TAB_NO_SIJI_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_HOKOKU_YN", .Item("WH_TAB_HOKOKU_YN").ToString(), DBDataType.CHAR))

        End With

    End Sub

    'START YANAI 要望番号510
    ''' <summary>
    ''' 出荷(中)の新規保存時の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaMInsComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_SET_NO", .Item("EDI_SET_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            If String.IsNullOrEmpty(.Item("GOODS_CD_NRS_FROM").ToString()) = True Then
                '正常の引当の場合
                prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Else
                '他荷主引当の場合
                prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS_FROM").ToString(), DBDataType.CHAR))
            End If
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
            If String.IsNullOrEmpty(.Item("GOODS_CD_NRS_FROM").ToString()) = True Then
                '正常の引当の場合
                prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", String.Empty, DBDataType.NVARCHAR))
            Else
                '他荷主引当の場合
                prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            End If
            prmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", Me.FormatNumValue(.Item("PRINT_SORT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UP_KBN", .Item("UP_KBN").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub
    'END YANAI 要望番号510

    ''' <summary>
    ''' 出荷(中)の更新時の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal INSUPD_FLG As String)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_SET_NO", .Item("EDI_SET_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            'START YANAI 20120321 緊急
            'If ("01").Equals(INSUPD_FLG) = False Then
            '    '検索からの編集の場合
            '    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            'ElseIf String.IsNullOrEmpty(.Item("GOODS_CD_NRS_FROM").ToString) = True Then
            '    '正常の引当の場合(新規保存からの編集の場合)
            '    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            'Else
            '    '他荷主引当の場合(新規保存からの編集の場合)
            '    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS_FROM").ToString(), DBDataType.CHAR))
            'End If
            'START YANAI 要望番号998
            'If String.IsNullOrEmpty(.Item("GOODS_CD_NRS_FROM").ToString) = True Then
            '    '正常引当の場合
            '    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            'Else
            '    '他荷主引当の場合
            '    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS_FROM").ToString(), DBDataType.CHAR))
            'End If
            If ("01").Equals(.Item("INSUPD_FLG").ToString()) = True Then
                '新規保存⇒すぐに編集の場合
                If String.IsNullOrEmpty(.Item("GOODS_CD_NRS_FROM").ToString) = True Then
                    '正常引当の場合
                    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
                Else
                    '他荷主引当の場合
                    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS_FROM").ToString(), DBDataType.CHAR))
                End If
            Else
                '検索画面⇒編集の場合
                prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            End If
            'END YANAI 要望番号998
            'END YANAI 20120321 緊急
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
            'START YANAI 20120321 緊急
            'If ("01").Equals(INSUPD_FLG) = False Then
            '    '検索からの編集の場合
            '    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", .Item("GOODS_CD_NRS_FROM").ToString(), DBDataType.CHAR))
            'ElseIf String.IsNullOrEmpty(.Item("GOODS_CD_NRS_FROM").ToString) = True Then
            '    '正常の引当の場合(新規保存からの編集の場合)
            '    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", .Item("GOODS_CD_NRS_FROM").ToString(), DBDataType.CHAR))
            'Else
            '    '他荷主引当の場合(新規保存からの編集の場合)
            '    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            'End If
            'START YANAI 要望番号998
            'If String.IsNullOrEmpty(.Item("GOODS_CD_NRS_FROM").ToString) = True Then
            '    '正常の引当の場合
            '    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", .Item("GOODS_CD_NRS_FROM").ToString(), DBDataType.CHAR))
            'Else
            '    '他荷主引当の場合
            '    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            'End If
            If ("01").Equals(.Item("INSUPD_FLG").ToString()) = True Then
                '新規保存⇒すぐに編集の場合
                If String.IsNullOrEmpty(.Item("GOODS_CD_NRS_FROM").ToString) = True Then
                    '正常の引当の場合
                    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", .Item("GOODS_CD_NRS_FROM").ToString(), DBDataType.CHAR))
                Else
                    '他荷主引当の場合
                    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
                End If
            Else
                '検索画面⇒編集の場合
                prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", .Item("GOODS_CD_NRS_FROM").ToString(), DBDataType.CHAR))
            End If
            'END YANAI 要望番号998
            'END YANAI 20120321 緊急
            prmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", Me.FormatNumValue(.Item("PRINT_SORT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UP_KBN", .Item("UP_KBN").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 出荷(小)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaSComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", .Item("OUTKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", .Item("OUTKA_TTL_NB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", .Item("OUTKA_TTL_QT").ToString(), DBDataType.CHAR))
            'START YANAI 要望番号681
            'prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            Dim zaiRecNo As String = String.Empty
            If String.IsNullOrEmpty(.Item("N_ZAI_REC_NO").ToString()) = False Then
                zaiRecNo = .Item("N_ZAI_REC_NO").ToString()
            Else
                zaiRecNo = .Item("ZAI_REC_NO").ToString()
            End If
            If ("01").Equals(.Item("SMPL_FLG_ZAI").ToString()) = True Then
                zaiRecNo = .Item("ZAI_REC_NO").ToString()
            End If
            '在庫が小分けで作られたデータの場合は
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", zaiRecNo, DBDataType.CHAR))

            'END YANAI 要望番号681
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_UPD_FLAG", .Item("ZAI_UPD_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_CAN_NB", Me.FormatNumValue(.Item("ALCTD_CAN_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_CAN_QT", Me.FormatNumValue(.Item("ALCTD_CAN_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@COA_FLAG", .Item("COA_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SMPL_FLAG", .Item("SMPL_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UP_KBN", .Item("UP_KBN").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 運送(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Call Me.SetUnsoLPkParameter(conditionRow, prmList)
            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIYU_KB", .Item("JIYU_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", .Item("DENP_NO").ToString(), DBDataType.NVARCHAR))
            '要望番号:2408 2015.09.17 追加START
            prmList.Add(MyBase.GetSqlParameter("@AUTO_DENP_KBN", .Item("AUTO_DENP_KBN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AUTO_DENP_NO", .Item("AUTO_DENP_NO").ToString(), DBDataType.NVARCHAR))
            '要望番号:2408 2015.09.17 追加END
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_TIME", .Item("OUTKA_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_ACT_TIME", .Item("ARR_ACT_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", .Item("CUST_REF_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD", .Item("SHIP_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ORIG_CD", .Item("ORIG_CD").ToString(), DBDataType.NVARCHAR))
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
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", .Item("UNSO_TEHAI_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUY_CHU_NO", .Item("BUY_CHU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AREA_CD", .Item("AREA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TYUKEI_HAISO_FLG", .Item("TYUKEI_HAISO_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_CD", .Item("SYUKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_CD", .Item("HAIKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", .Item("TRIP_NO_SYUKA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", .Item("TRIP_NO_TYUKEI").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", .Item("TRIP_NO_HAIKA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UP_KBN", .Item("UP_KBN").ToString(), DBDataType.NVARCHAR))
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            'END UMANO 要望番号1302 支払運賃に伴う修正。

        End With

    End Sub

    ''' <summary>
    ''' 運送(大)PK
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLPkParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    'START YANAI 要望番号673
    '''' <summary>
    '''' 運送(中)の更新パラメータ設定
    '''' </summary>
    '''' <param name="conditionRow">DataRow</param>
    '''' <param name="prmList">パラメータ</param>
    '''' <remarks></remarks>
    'Private Sub SetUnsoMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)
    ''' <summary>
    ''' 運送(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal INSUPD_FLG As String)
        'END YANAI 要望番号673

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))

            'START YANAI 要望番号673
            'prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            If String.IsNullOrEmpty(.Item("GOODS_CD_NRS_FROM").ToString) = True Then
                '他荷主以外の場合
                prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            ElseIf String.IsNullOrEmpty(.Item("GOODS_CD_NRS_FROM").ToString) = False AndAlso _
                    ("01").Equals(INSUPD_FLG) = True Then
                '他荷主の場合(新規作成⇒保存⇒すぐに編集⇒保存の場合
                prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS_FROM").ToString(), DBDataType.CHAR))
            Else
                '他荷主の場合(一覧から遷移⇒編集⇒保存の場合) または
                '新規保存時
                prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            End If
            'END YANAI 要望番号673

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
            prmList.Add(MyBase.GetSqlParameter("@UP_KBN", .Item("UP_KBN").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

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
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))   '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            prmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_SYARYO_KB", .Item("SEIQ_SYARYO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_PKG_UT", .Item("SEIQ_PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_NG_NB", Me.FormatNumValue(.Item("SEIQ_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_DANGER_KB", .Item("SEIQ_DANGER_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_BUNRUI_KB", .Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.CHAR))
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
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_KANRI", .Item("SAGYO_KANRI").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 作業の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="outkaLDr">出荷(大)のDataRow</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal outkaLDr As DataRow)

        With conditionRow

            Call Me.SetSagyoPkParameter(conditionRow, prmList)
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", .Item("SAGYO_COMP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SKYU_CHK", .Item("SKYU_CHK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Item("SAGYO_SIJI_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", .Item("INOUTKA_NO_LM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", .Item("SAGYO_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", .Item("SAGYO_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", .Item("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INV_TANI", .Item("INV_TANI").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", .Item("SAGYO_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_UP", .Item("SAGYO_UP").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", .Item("SAGYO_GK").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))   '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            prmList.Add(MyBase.GetSqlParameter("@REMARK_ZAI", .Item("REMARK_ZAI").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", .Item("REMARK_SKYU").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_SIJI", .Item("REMARK_SIJI").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", .Item("SAGYO_COMP_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", .Item("SAGYO_COMP_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_SAGYO_FLG", .Item("DEST_SAGYO_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UP_KBN", .Item("UP_KBN").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 支払運賃の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetShiharaiComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO", .Item("SHIHARAI_GROUP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO_M", .Item("SHIHARAI_GROUP_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAITO_CD", .Item("SHIHARAITO_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_SYARYO_KB", .Item("SHIHARAI_SYARYO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_PKG_UT", .Item("SHIHARAI_PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_NG_NB", .Item("SHIHARAI_NG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_DANGER_KB", .Item("SHIHARAI_DANGER_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_BUNRUI_KB", .Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_KYORI", .Item("SHIHARAI_KYORI").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_WT", .Item("SHIHARAI_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_UNCHIN", .Item("SHIHARAI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_CITY_EXTC", .Item("SHIHARAI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_WINT_EXTC", .Item("SHIHARAI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_RELY_EXTC", .Item("SHIHARAI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TOLL", .Item("SHIHARAI_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_INSU", .Item("SHIHARAI_INSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_FIXED_FLAG", .Item("SHIHARAI_FIXED_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DECI_NG_NB", .Item("DECI_NG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_KYORI", .Item("DECI_KYORI").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WT", .Item("DECI_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("DECI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", .Item("DECI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", .Item("DECI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", .Item("DECI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", .Item("DECI_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_INSU", .Item("DECI_INSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_UNCHIN", .Item("KANRI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_CITY_EXTC", .Item("KANRI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_WINT_EXTC", .Item("KANRI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_RELY_EXTC", .Item("KANRI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_TOLL", .Item("KANRI_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_INSU", .Item("KANRI_INSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_KANRI", .Item("SAGYO_KANRI").ToString(), DBDataType.CHAR))

        End With

    End Sub

    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 作業のPKパラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoPkParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Item("SAGYO_REC_NO").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ' 2012.12.13 要望番号：612 振替一括削除対応 START Nakamura
    ''' <summary>
    ''' 振替削除時の作業PK
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoInPkParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", String.Concat(.Item("INKA_NO_L").ToString(), "%"), DBDataType.CHAR))

        End With

    End Sub

    ' 2012.12.13 要望番号：612 振替一括削除対応 END Nakamura

    ''' <summary>
    ''' 作業の更新パラメータ（取り消し）
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoUpParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", .Item("SAGYO_COMP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", .Item("SAGYO_NM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", .Item("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INV_TANI", .Item("INV_TANI").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", Me.FormatNumValue(.Item("SAGYO_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_UP", Me.FormatNumValue(.Item("SAGYO_UP").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", Me.FormatNumValue(.Item("SAGYO_GK").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))   '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            prmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", .Item("REMARK_SKYU").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_SIJI", .Item("REMARK_SIJI").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 在庫レコードの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="outkaLDr">出荷(大)レコード</param>
    ''' <param name="outkaMDr">出荷(中)レコード</param>
    ''' <param name="outkaSDr">出荷(小)レコード</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetZaiTrsComParameter(ByVal conditionRow As DataRow _
                                      , ByVal outkaLDr As DataRow _
                                      , ByVal outkaMDr As DataRow _
                                      , ByVal outkaSDr As DataRow _
                                      , ByVal prmList As ArrayList _
                                      , ByVal insFlg As Boolean _
                                      )

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            'START ADD 2013/09/10 KOBAYASHI WIT対応
            prmList.Add(MyBase.GetSqlParameter("@GOODS_KANRI_NO", .Item("GOODS_KANRI_NO").ToString(), DBDataType.CHAR))
            'END   ADD 2013/09/10 KOBAYASHI WIT対応
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", .Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", .Item("HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", .Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", .Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", .Item("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OFB_KB", .Item("OFB_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", Me.FormatNumValue(.Item("PORA_ZAI_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", Me.FormatNumValue(.Item("ALLOC_CAN_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", Me.FormatNumValue(.Item("PORA_ZAI_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", Me.FormatNumValue(.Item("ALLOC_CAN_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKO_DATE", .Item("INKO_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE", .Item("INKO_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZERO_FLAG", .Item("ZERO_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LT_DATE", .Item("LT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", .Item("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD_P", .Item("DEST_CD_P").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SMPL_FLAG", .Item("SMPL_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UP_KBN", .Item("UP_KBN").ToString(), DBDataType.NVARCHAR))

            If insFlg = True Then
                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            End If

        End With

    End Sub

    'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
    ''' <summary>
    ''' 在庫レコードの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetZaiTrsDelComParameter(ByVal conditionRow As DataRow _
                                      , ByVal prmList As ArrayList _
                                      )

        Dim value As String = String.Empty

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            'START ADD 2013/09/10 KOBAYASHI WIT対応
            prmList.Add(MyBase.GetSqlParameter("@GOODS_KANRI_NO", .Item("GOODS_KANRI_NO").ToString(), DBDataType.CHAR))
            'END   ADD 2013/09/10 KOBAYASHI WIT対応
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", .Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", .Item("HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", .Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", .Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", .Item("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OFB_KB", .Item("OFB_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", Me.FormatNumValue(.Item("PORA_ZAI_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", Me.FormatNumValue(.Item("ALLOC_CAN_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", Me.FormatNumValue(.Item("PORA_ZAI_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", Me.FormatNumValue(.Item("ALLOC_CAN_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKO_DATE", .Item("INKO_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE", .Item("INKO_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZERO_FLAG", .Item("ZERO_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LT_DATE", .Item("LT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", .Item("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD_P", .Item("DEST_CD_P").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SMPL_FLAG", .Item("SMPL_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UP_KBN", .Item("UP_KBN").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 在庫レコードの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetZaiTrsDelKowakeParameter(ByVal conditionRow As DataRow _
                                      , ByVal prmList As ArrayList _
                                      , ByVal outSdr() As DataRow _
                                      )

        Dim value As String = String.Empty

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", outSdr(0).Item("O_ZAI_REC_NO").ToString(), DBDataType.CHAR))
            'Start 要望番号2014 小分け
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", outSdr(0).Item("N_ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", outSdr(0).Item("ALCTD_QT").ToString(), DBDataType.NUMERIC))
            'End 要望番号2014 小分け

        End With

    End Sub

    ''' <summary>
    ''' 在庫レコードの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetZaiTrsDelKowakeParameter2(ByVal conditionRow As DataRow _
                                      , ByVal prmList As ArrayList _
                                      , ByVal outSdr() As DataRow _
                                      )

        Dim value As String = String.Empty

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", outSdr(0).Item("ALCTD_QT").ToString(), DBDataType.NUMERIC))

        End With

    End Sub
    'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる

    ''' <summary>
    ''' 在庫レコードの更新パラメータ設定（小分け）
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="outkaMDr">出荷(中)レコード</param>
    ''' <param name="outkaSDr">出荷(小)レコード</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetZaiTrsKowakeComParameter(ByVal conditionRow As DataRow _
                                      , ByVal outkaMDr As DataRow _
                                      , ByVal outkaSDr As DataRow _
                                      , ByVal prmList As ArrayList _
                                      , ByVal newZaiRecNo As String _
                                      )

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", newZaiRecNo, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", outkaSDr.Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", outkaSDr.Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZONE_CD", outkaSDr.Item("ZONE_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCA", outkaSDr.Item("LOCA").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            'START ADD 2013/09/10 KOBAYASHI WIT対応
            prmList.Add(MyBase.GetSqlParameter("@GOODS_KANRI_NO", .Item("GOODS_KANRI_NO").ToString(), DBDataType.CHAR))
            'END   ADD 2013/09/10 KOBAYASHI WIT対応
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", .Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", .Item("HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", .Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", .Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", .Item("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OFB_KB", .Item("OFB_KB").ToString(), DBDataType.CHAR))
#If False Then      'UPD 2018/10/29 依頼番号 : 001800   【LMS】日立FN_小分け_保留区分自動追加
            prmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
#Else
            If ("10").Equals(.Item("NRS_BR_CD").ToString) = True AndAlso _
                ("00010").Equals(.Item("CUST_CD_L").ToString) = True Then
                '千葉BC　荷主CD 00010の場合
                prmList.Add(MyBase.GetSqlParameter("@SPD_KB", "10", DBDataType.CHAR))               '10:小分け目欠品
            Else
                '現行のまま
                prmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
            End If
#End If

            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
            'START YANAI 要望番号681
            'prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", Convert.ToString(Convert.ToInt32(outkaSDr.Item("ALCTD_NB").ToString)), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", "1", DBDataType.NUMERIC))
            'END YANAI 要望番号681
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", "0", DBDataType.NUMERIC))
            'START YANAI 要望番号681
            'prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", Convert.ToString(Convert.ToInt32(outkaSDr.Item("ALCTD_NB").ToString)), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", "1", DBDataType.NUMERIC))
            'END YANAI 要望番号681
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            If Convert.ToDecimal(.Item("ALLOC_CAN_QT_HOZON").ToString()) > Convert.ToDecimal(outkaSDr.Item("IRIME").ToString) Then
                'START YANAI 要望番号681
                'prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", Convert.ToString(Convert.ToDecimal(outkaSDr.Item("IRIME").ToString) - Convert.ToDecimal(outkaSDr.Item("ALCTD_QT").ToString)), DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", outkaSDr.Item("IRIME").ToString(), DBDataType.CHAR))
                'END YANAI 要望番号681
            Else
                '考えられるのは小分けした時に作成された在庫データを引当てた場合
                'START YANAI 要望番号681
                'prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", Convert.ToString(Convert.ToDecimal(.Item("ALLOC_CAN_QT_HOZON").ToString()) - Convert.ToDecimal(outkaSDr.Item("ALCTD_QT").ToString)), DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", .Item("ALLOC_CAN_QT_HOZON").ToString(), DBDataType.CHAR))
                'END YANAI 要望番号681
            End If
            'START YANAI 要望番号681
            'prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", "0", DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Convert.ToDecimal(outkaSDr.Item("ALCTD_QT").ToString), DBDataType.NUMERIC))
            'END YANAI 要望番号681
            If Convert.ToDecimal(.Item("ALLOC_CAN_QT_HOZON").ToString()) > Convert.ToDecimal(outkaSDr.Item("IRIME").ToString) Then
                prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", Convert.ToString(Convert.ToDecimal(outkaSDr.Item("IRIME").ToString) - Convert.ToDecimal(outkaSDr.Item("ALCTD_QT").ToString)), DBDataType.CHAR))
            Else
                '考えられるのは小分けした時に作成された在庫データを引当てた場合
                prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", Convert.ToString(Convert.ToDecimal(.Item("ALLOC_CAN_QT_HOZON").ToString()) - Convert.ToDecimal(outkaSDr.Item("ALCTD_QT").ToString)), DBDataType.CHAR))
            End If
            prmList.Add(MyBase.GetSqlParameter("@INKO_DATE", .Item("INKO_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE", .Item("INKO_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZERO_FLAG", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LT_DATE", .Item("LT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", .Item("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD_P", .Item("DEST_CD_P").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", "小分け目欠品", DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SMPL_FLAG", "01", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UP_KBN", .Item("UP_KBN").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    'START YANAI 20110913 小分け対応
    ''' <summary>
    ''' 在庫レコードの更新パラメータ設定（小分け先更新時）
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="outkaSDr">出荷(小)レコード</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetZaiKOWAKEComParameter(ByVal conditionRow As DataRow _
                                      , ByVal outkaSDr As DataRow _
                                      , ByVal prmList As ArrayList _
                                      )

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", outkaSDr.Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", outkaSDr.Item("PORA_ZAI_NB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", outkaSDr.Item("ALLOC_CAN_NB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", outkaSDr.Item("PORA_ZAI_QT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", outkaSDr.Item("ALLOC_CAN_QT").ToString(), DBDataType.CHAR))

        End With

    End Sub
    'END YANAI 20110913 小分け対応

    ''' <summary>
    ''' 移動レコードの更新パラメータ設定（小分け）
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="zaiDr">在庫レコード</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetIdoTrsKowakeComParameter(ByVal conditionRow As DataRow _
                                      , ByVal zaiDr As DataRow _
                                      , ByVal outSDr As DataRow _
                                      , ByVal outLDr As DataRow _
                                      , ByVal prmList As ArrayList _
                                      , ByVal newZaiRecNo As String _
                                      )

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", outSDr.Item("REC_NO").ToString, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IDO_DATE", outLDr.Item("OUTKO_DATE").ToString, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@O_ZAI_REC_NO", zaiDr.Item("ZAI_REC_NO").ToString, DBDataType.CHAR))
            'START YANAI 要望番号681
            'prmList.Add(MyBase.GetSqlParameter("@O_PORA_ZAI_NB", zaiDr.Item("PORA_ZAI_NB").ToString, DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@O_ALCTD_NB", "1", DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@O_ALLOC_CAN_NB", zaiDr.Item("ALLOC_CAN_NB").ToString, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@O_PORA_ZAI_NB", Convert.ToString(Convert.ToDecimal(zaiDr.Item("PORA_ZAI_NB").ToString) + 1), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@O_ALCTD_NB", "0", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@O_ALLOC_CAN_NB", Convert.ToString(Convert.ToDecimal(zaiDr.Item("ALLOC_CAN_NB").ToString) + 1), DBDataType.CHAR))
            'END YANAI 要望番号681
            prmList.Add(MyBase.GetSqlParameter("@O_IRIME", zaiDr.Item("IRIME").ToString, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@N_ZAI_REC_NO", newZaiRecNo, DBDataType.CHAR))
            'START YANAI 要望番号681
            'prmList.Add(MyBase.GetSqlParameter("@N_PORA_ZAI_NB", outSDr.Item("ALCTD_NB").ToString, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@N_PORA_ZAI_NB", "1", DBDataType.CHAR))
            'END YANAI 要望番号681
            prmList.Add(MyBase.GetSqlParameter("@N_ALCTD_NB", "0", DBDataType.CHAR))
            'START YANAI 要望番号681
            'prmList.Add(MyBase.GetSqlParameter("@N_ALLOC_CAN_NB", outSDr.Item("ALCTD_NB").ToString, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@N_ALLOC_CAN_NB", "1", DBDataType.CHAR))
            'END YANAI 要望番号681
            prmList.Add(MyBase.GetSqlParameter("@REMARK_KBN", "99", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", "小分け出荷の為在庫振替", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", "", DBDataType.CHAR))
            'START YANAI 要望番号681
            'prmList.Add(MyBase.GetSqlParameter("@ZAIK_ZAN_FLG", "00", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAIK_ZAN_FLG", "01", DBDataType.CHAR))
            'END YANAI 要望番号681

        End With

    End Sub

    '要望番号:997 terakawa 2012.10.22 Start
    ''' <summary>
    ''' EDI出荷(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))

            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' EDI受信DTLの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiDtlComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal tblNm As String)

        With conditionRow
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
            If .Item("SYS_DEL_FLG").ToString() = "1" Then
                prmList.Add(MyBase.GetSqlParameter("@DELETE_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                prmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", updTime, DBDataType.CHAR))
            Else
                prmList.Add(MyBase.GetSqlParameter("@DELETE_USER", String.Empty, DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", String.Empty, DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", String.Empty, DBDataType.CHAR))
            End If
        End With

    End Sub

    '要望番号：612 START Nakamura
    ''' <summary>
    ''' パラメータ設定モジュール(振替一括削除検索)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParamFuriDel(ByVal prmList As ArrayList)

        With Me._Row

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FURI_NO", .Item("FURI_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub
    '要望番号：612 End Nakamura

#Region "2014/01/22 輸出情報追加 START"

    ''' <summary>
    ''' 輸出情報の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetExportLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NM", .Item("SHIP_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DESTINATION", .Item("DESTINATION").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BOOKING_NO", .Item("BOOKING_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@VOYAGE_NO", .Item("VOYAGE_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPPER_CD", .Item("SHIPPER_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CONT_LOADING_DATE", .Item("CONT_LOADING_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@STORAGE_TEST_DATE", .Item("STORAGE_TEST_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@STORAGE_TEST_TIME", .Item("STORAGE_TEST_TIME").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEPARTURE_DATE", .Item("DEPARTURE_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CONTAINER_NO", .Item("CONTAINER_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CONTAINER_NM", .Item("CONTAINER_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CONTAINER_SIZE", .Item("CONTAINER_SIZE").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region


    '2015.07.08 協立化学　シッピング対応 追加START
    ''' <summary>
    ''' マーク(HED)の新規保存時の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetMarkHedInsComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CASE_NO_FROM", .Item("CASE_NO_FROM").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@CASE_NO_TO", .Item("CASE_NO_TO").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UP_KBN", .Item("UP_KBN").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' マーク(DTL)の新規保存時の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetMarkDtlInsComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MARK_EDA", .Item("MARK_EDA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_INFO", .Item("REMARK_INFO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UP_KBN", .Item("UP_KBN").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    '2015.07.08 協立化学　シッピング対応 追加END

    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function
    '要望番号:997 terakawa 2012.10.22 End


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

#Region "共通"

    ''' <summary>
    ''' 配下データの論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dbTblNm">DBのTable名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>論理削除SQLの構築・発行</remarks>
    Private Function UpdateComDelFlg(ByVal ds As DataSet, ByVal dbTblNm As String) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(String.Concat(" UPDATE $LM_TRN$..", dbTblNm, "      ", vbNewLine))
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMC020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append(String.Concat("   AND OUTKA_NO_L = @OUTKA_NO_L   ", vbNewLine))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._Row, Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateComDelFlg", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''2012.11.28 要望番号：612 振替一括削除対応 Nakamura START
    '''' <summary>
    '''' 配下データの論理削除（入荷）
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <param name="dbTblNm">DBのTable名</param>
    '''' <returns>DataSet</returns>
    '''' <remarks>論理削除SQLの構築・発行</remarks>
    Private Function UpdateComDelFlgIN(ByVal ds As DataSet, ByVal dbTblNm As String) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_FURI_DEL")

        'SQL格納変数の初期化
        'Me._StrSql.Length = 0
        '20171025 初期化方法間違い修正
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(String.Concat(" UPDATE $LM_TRN$..", dbTblNm, "      ", vbNewLine))
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMC020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append(String.Concat("   AND INKA_NO_L = @INKA_NO_L   ", vbNewLine))

        'SQL文のコンパイル
        Me._Row = inTbl.Rows(0)
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._Row, Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row.Item("INKA_NO_L").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateComDelFlgIN", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function
    '2012.11.28 要望番号：612 振替一括削除対応 Nakamura START

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 出荷(大)のレコードを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Private Function GetOutkaLDataRow(ByVal ds As DataSet, ByVal dr As DataRow) As DataRow

        Dim dt As DataTable = ds.Tables("LMC020_OUTKA_L")

        Return dt.Select(String.Concat("OUTKA_NO_L = '", dr.Item("OUTKA_NO_L").ToString(), "' "))(0)

    End Function

    ''' <summary>
    ''' 出荷(中)のレコードを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Private Function GetOutkaMDataRow(ByVal ds As DataSet, ByVal dr As DataRow) As DataRow

        Dim dt As DataTable = ds.Tables("LMC020_OUTKA_M")

        Return dt.Select(String.Concat("OUTKA_NO_L = '", dr.Item("OUTKA_NO_L").ToString(), "' " _
                                       , "AND OUTKA_NO_M = '", dr.Item("OUTKA_NO_M").ToString(), "' " _
                                       ))(0)

    End Function

    ''' <summary>
    ''' 出荷(小)のレコードを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Private Function GetOutkaSDataRow(ByVal ds As DataSet, ByVal dr As DataRow) As DataRow

        Dim dt As DataTable = ds.Tables("LMC020_OUTKA_S")

        Return dt.Select(String.Concat("OUTKA_NO_L = '", dr.Item("OUTKA_NO_L").ToString(), "' " _
                                       , "AND OUTKA_NO_M = '", dr.Item("OUTKA_NO_M").ToString(), "' " _
                                       , "AND OUTKA_NO_S = '", dr.Item("OUTKA_NO_S").ToString(), "' " _
                                       ))(0)

    End Function

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function


    ''' <summary>
    ''' Update文の発行(エラーメッセージなし：在庫用)20161207
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChkNoMessage(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            'メッセージのセットは中番と同時に参照元で行う
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

#Region "WIT対応"

    ''' <summary>
    ''' 商品管理番号作成元データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGoodsKanriNoSrc(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMC020DAC.SQL_SELECT_GOODS_KANRI_NO_SRC)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectGoodsKanriNoSrc", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")

#If True Then ' JT物流入荷検品対応 20160727 added inoue
        map.Add("IRIME", "IRIME")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
#End If
#If True Then ' 日興産業出荷検品対応 20170207 added inoue
        map.Add("JAN_CD", "JAN_CD")
#End If
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020OUT_GOODS_KANRI_NO_SRC")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC020OUT_GOODS_KANRI_NO_SRC").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 在庫データ更新(商品管理番号)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データ（振替元）更新SQLの構築・発行</remarks>
    Private Function UpdateGoodsKanriNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020IN_UPDATE_GOODS_KANRI_NO")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMC020DAC.SQL_UPDATE_GOODS_KANRI_NO, inRow.Item("NRS_BR_CD").ToString()))

        Dim sysUpdDate As String = String.Empty
        Dim sysUpdTime As String = String.Empty

        Me._Row = inRow

        'パラメータの初期化
        cmd.Parameters.Clear()
        Me._SqlPrmList.Clear()

        'SQLパラメータ（システム項目）設定
        sysUpdDate = MyBase.GetSystemDate()
        sysUpdTime = MyBase.GetSystemTime()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me._Row.Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_KANRI_NO", Me._Row.Item("GOODS_KANRI_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", sysUpdDate, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", sysUpdTime, DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateGoodsKanriNo", cmd)

        'SQLの発行
        Dim result As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' ハンディ対象荷主情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCustHandy(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMC020DAC.SQL_CUST_HANDY)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectCustHandy", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_SHORT", "CUST_NM_SHORT")
        map.Add("FLG_01", "FLG_01")
        map.Add("FLG_02", "FLG_02")
        map.Add("FLG_03", "FLG_03")
        map.Add("FLG_04", "FLG_04")
        map.Add("FLG_05", "FLG_05")
        map.Add("FLG_06", "FLG_06")
        map.Add("FLG_07", "FLG_07")
        map.Add("FLG_08", "FLG_08")
        map.Add("FLG_09", "FLG_09")
        map.Add("FLG_10", "FLG_10")
        map.Add("FLG_11", "FLG_11")
        map.Add("FLG_12", "FLG_12")
        map.Add("FLG_13", "FLG_13")
        map.Add("FLG_14", "FLG_14")
        map.Add("FLG_15", "FLG_15")
        map.Add("FLG_16", "FLG_16")
        map.Add("FLG_17", "FLG_17")
        map.Add("FLG_18", "FLG_18")
        map.Add("FLG_19", "FLG_19")
        map.Add("FLG_20", "FLG_20")
        map.Add("S101_KBN_NM1", "S101_KBN_NM1")
        map.Add("S101_KBN_NM2", "S101_KBN_NM2")
        map.Add("S101_KBN_NM3", "S101_KBN_NM3")
        map.Add("S101_KBN_NM4", "S101_KBN_NM4")
        map.Add("S101_KBN_NM5", "S101_KBN_NM5")
        map.Add("S101_KBN_NM6", "S101_KBN_NM6")
        map.Add("S101_KBN_NM7", "S101_KBN_NM7")
        map.Add("S101_KBN_NM8", "S101_KBN_NM8")
        map.Add("S101_KBN_NM9", "S101_KBN_NM9")
        map.Add("S101_KBN_NM10", "S101_KBN_NM10")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020OUT_CUST_HANDY")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC020OUT_CUST_HANDY").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' PICK_WKの削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DelPickWK(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_PICK_WK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.AppendLine(LMC020DAC.SQL_DELETE_PICK_WK)
        'SERIALが空ではない場合はSERIAL指定で削除
        If String.IsNullOrEmpty(Me._Row.Item("SERIAL_NO").ToString()) = False Then
            Me._StrSql.AppendLine("  AND SERIAL_NO = @SERIAL_NO        --@可変 ")
        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row.Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", Me._Row.Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "DelPickWK", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "CALT対応"
#Region "Select"
    ''' <summary>
    ''' キャンセルデータ抜出
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectSendCancel(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC020_OUTKA_L").Rows(0)

        'SQL構築
        Me._StrSql.AppendLine(LMC020DAC.SQL_SELECT_OUTKA_LMS_CANCEL)
        Me._StrSql.AppendLine(LMC020DAC.SQL_OUTKA_LMS_CANCEL_ORDER_BY)

        'DBスキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParameterOnCalt(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectSendCancel", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("SEND_SEQ", "SEND_SEQ")
        map.Add("DATA_KBN", "DATA_KBN")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("ZIP", "ZIP")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("REMARK_L", "REMARK_L")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("REMARK_M", "REMARK_M")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("OUTKA_NB", "OUTKA_NB")
        map.Add("OUTKA_QT", "OUTKA_QT")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("OUTKA_TTL_QT", "OUTKA_TTL_QT")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("IRIME", "IRIME")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("REMARK_S", "REMARK_S")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("DEST_CD_P", "DEST_CD_P")
        map.Add("SEND_SHORI_FLG", "SEND_SHORI_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020_OUTKA_DEL_OUT")

        MyBase.SetResultCount(ds.Tables("LMC020_OUTKA_DEL_OUT").Rows.Count)
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 倉庫チェック
    ''' 
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkWhCd(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC020_OUTKA_L").Rows(0)

        'SQL構築
        Me._StrSql.AppendLine(LMC020DAC.SQL_SELECT_WH_CD_EXIST)

        'DBスキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetchkWhCdParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "ChkWhCd", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

#Region "Insert"
    ''' <summary>
    ''' 出荷報告データ作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertDirectSend(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC020_OUTKA_DEL_OUT").Rows(0)

        'SQL構築
        Me._StrSql.Append(LMC020DAC.SQL_INSERT_DIRECT_SEND)

        'DBスキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetInsertParameterOnCalt(Me._Row, Me._SqlPrmList)
        Call Me.SetParamCommonSystems()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "InsertDirectSend", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "パラ設定"
    ''' <summary>
    ''' パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetParameterOnCalt(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 倉庫チェックパラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetchkWhCdParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 出荷指示データ作成
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInsertParameterOnCalt(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", .Item("OUTKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_SEQ", .Item("SEND_SEQ").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DATA_KBN", .Item("DATA_KBN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", .Item("CUST_NM_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", .Item("DEST_AD_1").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", .Item("DEST_AD_2").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", .Item("DEST_AD_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", .Item("DEST_TEL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP", .Item("ZIP").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", .Item("CUST_ORD_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", .Item("BUYER_ORD_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_L", .Item("REMARK_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", .Item("GOODS_NM_1").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_M", .Item("REMARK_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NB", .Item("OUTKA_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", .Item("OUTKA_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", .Item("OUTKA_TTL_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", .Item("OUTKA_TTL_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", .Item("ALCTD_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", .Item("ALCTD_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", .Item("BETU_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_S", .Item("REMARK_S").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", .Item("PKG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PKG_UT", .Item("PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", .Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", .Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", .Item("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", .Item("GOODS_CRT_DATE").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LT_DATE", .Item("LT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OFB_KB", .Item("OFB_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD_P", .Item("DEST_CD_P").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_SHORI_FLG", .Item("SEND_SHORI_FLG").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystems()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

#End Region

#End Region

    '2018/12/07 ADD START 要望管理002171
#Region "出荷梱包個数自動計算"

    ''' <summary>
    ''' 自動計算に必要なマスタの設定エラー件数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>設定エラー件数取得SQLの構築・発行</remarks>
    Private Function SelectCalcMstErrCntSakura(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC020DAC.SQL_SELECT_CALC_MST_ERR_CNT_SAKURA)   'SQL構築(データ抽出用Select句)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件設定
        Call Me.SetOutkaLPKeyParameter()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectCalcMstErrCntSakura", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_ERR_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' サクラ出荷梱包個数自動計算結果取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷梱包個数取得SQLの構築・発行</remarks>
    Private Function SelectCalcOutkaPkgNbSakura(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC020DAC.SQL_SELECT_CALC_OUTKAPKGNB_SAKURA)   'SQL構築(データ抽出用Select句)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件設定
        Call Me.SetOutkaLPKeyParameter()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "SelectCalcOutkaPkgNbSakura", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC020_CALC_OUTKA_PKG_NB_SAKURA")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷(大).出荷梱包個数の更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaLOutkaPkgNb(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_OUTKA_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Dim setSql As String = LMC020DAC.SQL_UPDATE_OUTKAL_OUTKAPKGNB

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUpdateOutkaLOutkaPkgNbParameter()
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)
        Call Me.SetOutkaLPKeyParameter()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "UpdateOutkaLOutkaPkgNb", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定（出荷(大)主キー）
    ''' </summary>
    Private Sub SetOutkaLPKeyParameter()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定（出荷(大)更新項目）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdateOutkaLOutkaPkgNbParameter()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me._Row.Item("OUTKA_PKG_NB").ToString(), DBDataType.CHAR))

    End Sub

#End Region
    '2018/12/07 ADD END   要望管理002171

#Region "タブレット対応"

    ''' <summary>
    ''' 営業所がタブレット対応かどうかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>営業所がタブレット対応かどうかを判定SQLの構築・発行</remarks>
    Private Function IsWhTabNrsBrCd(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC020_TABLET_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC020DAC.SQL_IS_WH_TAN_NRS_BR_CD)      'SQL構築

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "IsWhTabNrsBrCd", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        reader.Read()

        '処理件数の設定
        MyBase.SetResultCount(Convert.ToInt32(reader("CNT").ToString()))

        reader.Close()

        Return ds

    End Function

#End Region

End Class
