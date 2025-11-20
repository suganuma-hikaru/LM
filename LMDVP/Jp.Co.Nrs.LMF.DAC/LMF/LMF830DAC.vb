' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF830    : 手配指示作成処理
'  作  成  者       :  umano
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Base

''' <summary>
''' LMF830DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF830DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SELECT処理 SQL"

#Region "手配指示作成"

#Region "SELECT句(H_SENDUNSOEDI_LM)"

    ''' <summary>
    ''' 手配指示作成データ抽出用(LMデータ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SENDUNSOEDI_LM As String = " SELECT                                                                                                               " & vbNewLine _
                                           & "            FLL.NRS_BR_CD                                                                                  AS NRS_BR_CD         " & vbNewLine _
                                           & "           ,FL.UNSO_NO_L                                                                                   AS UNSO_NO_L         " & vbNewLine _
                                           & "           ,CASE WHEN HSUL.SEND_NO IS NULL AND @TEHAI_SYUBETSU = '01' THEN ROW_NUMBER() OVER(PARTITION BY FL.UNSO_NO_L ORDER BY FLL.NRS_BR_CD,FL.UNSO_NO_L)  " & vbNewLine _
                                           & "                 WHEN HSUL.SEND_NO IS NULL AND @TEHAI_SYUBETSU <> '01' THEN ROW_NUMBER() OVER(PARTITION BY FL.UNSO_NO_L, FLL.TRIP_NO ORDER BY FLL.NRS_BR_CD,FL.UNSO_NO_L)  " & vbNewLine _
                                           & "                 WHEN HSUL.SEND_NO IS NOT NULL AND @TEHAI_SYUBETSU = '01' THEN HSUL.SEND_NO + ROW_NUMBER() OVER(PARTITION BY FL.UNSO_NO_L ORDER BY FLL.NRS_BR_CD,FL.UNSO_NO_L)             " & vbNewLine _
                                           & "                 ELSE HSUL.SEND_NO + ROW_NUMBER() OVER(PARTITION BY FL.UNSO_NO_L, FLL.TRIP_NO ORDER BY FLL.NRS_BR_CD,FL.UNSO_NO_L)             " & vbNewLine _
                                           & "                 END SEND_NO                                                                                                    " & vbNewLine _
                                           & "           ,FLL.TRIP_NO                                                                                    AS TRIP_NO           " & vbNewLine _
                                           & "           ,CASE WHEN HSUL.SEND_NO IS NULL THEN '00'                                                                            " & vbNewLine _
                                           & "                 ELSE '01'                                                                                                      " & vbNewLine _
                                           & "                 END HENKO_KB                                                                                                   " & vbNewLine _
                                           & "           ,FL.MOTO_DATA_KB                                                                                AS MOTO_DATA_KB      " & vbNewLine _
                                           & "           ,FLL.UNSOCO_CD                                                                                  AS UNSO_CD           " & vbNewLine _
                                           & "           ,FLL.UNSOCO_BR_CD                                                                               AS UNSO_BR_CD        " & vbNewLine _
                                           & "           ,FL.ORIG_CD                                                                                     AS WH_CD             " & vbNewLine _
                                           & "           ,ISNULL(MDEST_F.DEST_NM,'')                                                                     AS WH_NM             " & vbNewLine _
                                           & "           ,ISNULL(MDEST_F.ZIP,'')                                                                         AS WH_ZIP            " & vbNewLine _
                                           & "           ,ISNULL(MDEST_F.AD_1,'')                                                                        AS WH_AD_1           " & vbNewLine _
                                           & "           ,ISNULL(MDEST_F.AD_2,'')                                                                        AS WH_AD_2           " & vbNewLine _
                                           & "           ,ISNULL(MDEST_F.AD_3,'')                                                                        AS WH_AD_3           " & vbNewLine _
                                           & "           ,ISNULL(MDEST_F.TEL,'')                                                                         AS WH_TEL            " & vbNewLine _
                                           & "           ,FL.OUTKA_PLAN_DATE                                                                             AS OUTKA_PLAN_DATE   " & vbNewLine _
                                           & "           ,FL.ARR_PLAN_DATE                                                                               AS ARR_PLAN_DATE     " & vbNewLine _
                                           & "           ,FL.ARR_PLAN_TIME                                                                               AS ARR_PLAN_TIME     " & vbNewLine _
                                           & "           ,FL.CUST_CD_L                                                                                   AS CUST_CD_L         " & vbNewLine _
                                           & "           ,FL.CUST_CD_M                                                                                   AS CUST_CD_M         " & vbNewLine _
                                           & "           ,MCUST.CUST_NM_L                                                                                AS CUST_NM_L         " & vbNewLine _
                                           & "           ,MCUST.CUST_NM_M                                                                                AS CUST_NM_M         " & vbNewLine _
                                           & "           ,MDEST_S.DEST_NM                                                                                AS SHIP_NM_L         " & vbNewLine _
                                           & "           ,FL.DEST_CD                                                                                     AS DEST_CD           " & vbNewLine _
                                           & "           ,CASE WHEN CL.DEST_KB = '00' THEN MDEST_D.DEST_NM                                                                    " & vbNewLine _
                                           & "                 WHEN CL.DEST_KB = '01' THEN CL.DEST_NM                                                                         " & vbNewLine _
                                           & "                 ELSE MDEST_D.DEST_NM                                                                                           " & vbNewLine _
                                           & "                 END DEST_NM                                                                                                    " & vbNewLine _
                                           & "           ,MDEST_D.ZIP                                                                                    AS DEST_ZIP          " & vbNewLine _
                                           & "           ,CASE WHEN CL.DEST_KB = '00' THEN MDEST_D.AD_1                                                                       " & vbNewLine _
                                           & "                 WHEN CL.DEST_KB = '01' THEN CL.DEST_AD_1                                                                       " & vbNewLine _
                                           & "                 ELSE MDEST_D.AD_1                                                                                              " & vbNewLine _
                                           & "                 END DEST_AD_1                                                                                                  " & vbNewLine _
                                           & "           ,CASE WHEN CL.DEST_KB = '00' THEN MDEST_D.AD_2                                                                       " & vbNewLine _
                                           & "                 WHEN CL.DEST_KB = '01' THEN CL.DEST_AD_2                                                                       " & vbNewLine _
                                           & "                 ELSE MDEST_D.AD_2                                                                                              " & vbNewLine _
                                           & "                 END DEST_AD_2                                                                                                  " & vbNewLine _
                                           & "           ,CASE WHEN CL.DEST_KB = '00' THEN MDEST_D.AD_3                                                                       " & vbNewLine _
                                           & "                 WHEN CL.DEST_KB = '01' THEN CL.DEST_AD_3                                                                       " & vbNewLine _
                                           & "                 ELSE MDEST_D.AD_3                                                                                              " & vbNewLine _
                                           & "                 END DEST_AD_3                                                                                                  " & vbNewLine _
                                           & "           ,CASE WHEN CL.DEST_KB = '00' THEN MDEST_D.TEL                                                                        " & vbNewLine _
                                           & "                 WHEN CL.DEST_KB = '01' THEN CL.DEST_TEL                                                                        " & vbNewLine _
                                           & "                 ELSE MDEST_D.TEL                                                                                               " & vbNewLine _
                                           & "                 END DEST_TEL                                                                                                   " & vbNewLine _
                                           & "           ,MDEST_D.FAX                                                                                    AS DEST_FAX          " & vbNewLine _
                                           & "           ,MDEST_D.JIS                                                                                    AS DEST_JIS_CD       " & vbNewLine _
                                           & "           ,MDEST_D.SP_NHS_KB                                                                              AS SP_NHS_KB         " & vbNewLine _
                                           & "           ,MDEST_D.COA_YN                                                                                 AS COA_YN            " & vbNewLine _
                                           & "           ,ISNULL(CL.CUST_ORD_NO,FL.CUST_REF_NO)                                                          AS CUST_ORD_NO       " & vbNewLine _
                                           & "           ,FL.BUY_CHU_NO                                                                                  AS BUYER_ORD_NO      " & vbNewLine _
                                           & "           ,ISNULL(CL.NHS_REMARK,'')                                                                       AS NHS_REMARK        " & vbNewLine _
                                           & "           ,ISNULL(CL.REMARK,'')                                                                           AS REMARK            " & vbNewLine _
                                           & "           ,FL.REMARK                                                                                      AS UNSO_ATT          " & vbNewLine _
                                           & "           ,ISNULL(CL.DENP_YN,'')                                                                          AS DENP_YN           " & vbNewLine _
                                           & "           ,ISNULL(CL.PC_KB,'')                                                                            AS PC_KB             " & vbNewLine _
                                           & "           ,FLL.BIN_KB                                                                                     AS BIN_KB            " & vbNewLine _
                                           & "           ,''                                                                                             AS BIN_NM            " & vbNewLine _
                                           & "           ,FL.UNSO_PKG_NB                                                                                 AS UNSO_PKG_NB       " & vbNewLine _
                                           & "           ,FL.UNSO_WT                                                                                     AS UNSO_WT           " & vbNewLine _
                                           & "--           ,ISNULL(FU.DECI_KYORI,0)                                                                        AS KYORI             " & vbNewLine _
                                           & "--           ,ISNULL(FU.DECI_UNCHIN,0)                                                                       AS DECI_UNCHIN       " & vbNewLine _
                                           & "           ,ISNULL(FS.DECI_KYORI,0)                                                                        AS KYORI             " & vbNewLine _
                                           & "           ,ISNULL(FS.DECI_UNCHIN,0)                                                                       AS DECI_UNCHIN       " & vbNewLine _
                                           & "           ,''                                                                                             AS BIKO1_HED         " & vbNewLine _
                                           & "           ,''                                                                                             AS BIKO2_HED         " & vbNewLine _
                                           & "           ,''                                                                                             AS BIKO3_HED         " & vbNewLine _
                                           & "           ,''                                                                                             AS BIKO4_HED         " & vbNewLine _
                                           & ",FM.UNSO_NO_M                                                                                       AS UNSO_NO_M                                        " & vbNewLine _
                                           & "--,CASE WHEN HSUL.SEND_NO IS NULL THEN ROW_NUMBER() OVER(PARTITION BY FL.UNSO_NO_L, FM.UNSO_NO_M, FLL.TRIP_NO ORDER BY FLL.NRS_BR_CD,FL.UNSO_NO_L)        " & vbNewLine _
                                           & "--ELSE HSUL.SEND_NO + ROW_NUMBER() OVER(PARTITION BY FL.UNSO_NO_L, FM.UNSO_NO_M, FLL.TRIP_NO ORDER BY FLL.NRS_BR_CD,FL.UNSO_NO_L)                         " & vbNewLine _
                                           & "--END SEND_NO                                                                                                                                             " & vbNewLine _
                                           & ",FM.GOODS_CD_NRS                                                                                    AS GOODS_CD_NRS                                     " & vbNewLine _
                                           & ",FM.GOODS_NM                                                                                        AS GOODS_NM                                         " & vbNewLine _
                                           & ",ISNULL(MGOODS.GOODS_CD_CUST,'')                                                                               AS COODS_CD_CUST                                    " & vbNewLine _
                                           & ",FM.UNSO_ONDO_KB                                                                                    AS UNSO_ONDO_KB                                     " & vbNewLine _
                                           & ",Z_KBN_ONDO.KBN_NM1                                                                                 AS UNSO_ONDO_NM                                     " & vbNewLine _
                                           & ",ISNULL(MGOODS.ONDO_MX,0)                                                                                     AS ONDO_MX                                          " & vbNewLine _
                                           & ",ISNULL(MGOODS.ONDO_MM,0)                                                                                     AS ONDO_MM                                          " & vbNewLine _
                                           & ",''                                                                                                 AS CUST_ORD_NO_DTL                                  " & vbNewLine _
                                           & ",''                                                                                                 AS BUYER_ORD_NO_DTL                                 " & vbNewLine _
                                           & ",''                                                                                                 AS REMARK_OUTKA                                     " & vbNewLine _
                                           & ",FM.REMARK                                                                                          AS REMARK_UNSO                                      " & vbNewLine _
                                           & ",FM.LOT_NO                                                                                          AS LOT_NO                                           " & vbNewLine _
                                           & ",''                                                                                                 AS SERIAL_NO                                        " & vbNewLine _
                                           & ",FM.IRIME                                                                                           AS IRIME                                            " & vbNewLine _
                                           & ",FM.IRIME_UT                                                                                        AS IRIME_UT                                         " & vbNewLine _
                                           & ",ISNULL(MGOODS.PKG_UT,'')                                                                                      AS PKG_UT                                           " & vbNewLine _
                                           & ",FM.UNSO_TTL_NB                                                                                     AS OUTKA_TTL_NB                                     " & vbNewLine _
                                           & ",FM.UNSO_TTL_QT                                                                                     AS OUTKA_TTL_QT                                     " & vbNewLine _
                                           & ",FM.BETU_WT                                                                                         AS BETU_WT                                          " & vbNewLine _
                                           & ",''                                                                                                 AS BIKO1_DTL                                        " & vbNewLine _
                                           & ",''                                                                                                 AS BIKO2_DTL                                        " & vbNewLine _
                                           & ",''                                                                                                 AS BIKO3_DTL                                        " & vbNewLine _
                                           & ",''                                                                                                 AS BIKO4_DTL                                        " & vbNewLine _
                                           & ",0                                                                                                  AS NUM1_DTL                                         " & vbNewLine _
                                           & ",0                                                                                                  AS NUM2_DTL                                         " & vbNewLine _
                                           & ",''                                                                                                 AS QR_CODE                                          " & vbNewLine _                                           
                                           & ",@SYS_DEL_FLG                                                                                       AS SYS_DEL_FLG                                      " & vbNewLine

#End Region

#Region "FROM句(H_SENDUNSOEDI_LM)"

    Private Const SQL_FROM_SENDUNSOEDI_LM As String = " FROM $LM_TRN$..F_UNSO_LL FLL                                                                                         " & vbNewLine _
                                               & "           LEFT JOIN $LM_TRN$..F_UNSO_L FL                                                                          " & vbNewLine _
                                               & "           ON                                                                                                       " & vbNewLine _
                                               & "               FLL.NRS_BR_CD = FL.NRS_BR_CD                                                                         " & vbNewLine _
                                               & "           AND FLL.TRIP_NO = FL.TRIP_NO                                                                             " & vbNewLine _
                                               & "           AND FLL.SYS_DEL_FLG = 0                                                                                  " & vbNewLine _
                                               & "--           LEFT JOIN $LM_TRN$..F_UNCHIN_TRS FU                                                                      " & vbNewLine _
                                               & "--           ON                                                                                                       " & vbNewLine _
                                               & "--               FL.NRS_BR_CD = FU.NRS_BR_CD                                                                          " & vbNewLine _
                                               & "--           AND FL.UNSO_NO_L = FU.UNSO_NO_L                                                                          " & vbNewLine _
                                               & "--           AND FU.SYS_DEL_FLG = '0'                                                                                 " & vbNewLine _
                                               & "           LEFT JOIN $LM_TRN$..F_SHIHARAI_TRS FS                                                                      " & vbNewLine _
                                               & "           ON                                                                                                       " & vbNewLine _
                                               & "               FL.NRS_BR_CD = FS.NRS_BR_CD                                                                          " & vbNewLine _
                                               & "           AND FL.UNSO_NO_L = FS.UNSO_NO_L                                                                          " & vbNewLine _
                                               & "           AND FS.SYS_DEL_FLG = '0'                                                                                 " & vbNewLine _
                                               & "           LEFT JOIN $LM_TRN$..C_OUTKA_L CL                                                                         " & vbNewLine _
                                               & "           ON                                                                                                       " & vbNewLine _
                                               & "               CL.NRS_BR_CD = FL.NRS_BR_CD                                                                          " & vbNewLine _
                                               & "           AND CL.OUTKA_NO_L = FL.INOUTKA_NO_L                                                                      " & vbNewLine _
                                               & "           AND FL.MOTO_DATA_KB = '20'                                                                               " & vbNewLine _
                                               & "           AND CL.SYS_DEL_FLG = '0'                                                                                 " & vbNewLine _
                                               & "           LEFT JOIN $LM_MST$..M_DEST MDEST_F                                                                       " & vbNewLine _
                                               & "           ON                                                                                                       " & vbNewLine _
                                               & "               MDEST_F.NRS_BR_CD = FL.NRS_BR_CD                                                                     " & vbNewLine _
                                               & "           AND MDEST_F.DEST_CD = FL.ORIG_CD                                                                         " & vbNewLine _
                                               & "           AND MDEST_F.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                                               & "           LEFT JOIN $LM_MST$..M_DEST MDEST_D                                                                       " & vbNewLine _
                                               & "           ON                                                                                                       " & vbNewLine _
                                               & "               MDEST_D.NRS_BR_CD = FL.NRS_BR_CD                                                                     " & vbNewLine _
                                               & "           AND MDEST_D.CUST_CD_L = FL.CUST_CD_L                                                                     " & vbNewLine _
                                               & "           AND MDEST_D.DEST_CD = FL.DEST_CD                                                                         " & vbNewLine _
                                               & "           AND MDEST_D.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                                               & "           LEFT JOIN $LM_MST$..M_DEST MDEST_S                                                                       " & vbNewLine _
                                               & "           ON                                                                                                       " & vbNewLine _
                                               & "               MDEST_S.NRS_BR_CD = FL.NRS_BR_CD                                                                     " & vbNewLine _
                                               & "           AND MDEST_S.CUST_CD_L = FL.CUST_CD_L                                                                     " & vbNewLine _
                                               & "           AND MDEST_S.DEST_CD = FL.SHIP_CD                                                                         " & vbNewLine _
                                               & "           AND MDEST_S.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                                               & "           LEFT JOIN $LM_MST$..M_CUST MCUST                                                                         " & vbNewLine _
                                               & "           ON                                                                                                       " & vbNewLine _
                                               & "               MCUST.NRS_BR_CD = FL.NRS_BR_CD                                                                       " & vbNewLine _
                                               & "           AND MCUST.CUST_CD_L = FL.CUST_CD_L                                                                       " & vbNewLine _
                                               & "           AND MCUST.CUST_CD_M = FL.CUST_CD_M                                                                       " & vbNewLine _
                                               & "           AND MCUST.CUST_CD_S = '00'                                                                               " & vbNewLine _
                                               & "           AND MCUST.CUST_CD_SS = '00'                                                                              " & vbNewLine _
                                               & "           AND MCUST.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                               & "           LEFT JOIN                                                                                                " & vbNewLine _
                                               & "           (                                                                                                        " & vbNewLine _
                                               & "            SELECT                                                                                                  " & vbNewLine _
                                               & "            NRS_BR_CD     AS NRS_BR_CD                                                                              " & vbNewLine _
                                               & "           ,TRIP_NO       AS TRIP_NO                                                                                " & vbNewLine _
                                               & "           ,UNSO_NO_L     AS UNSO_NO_L                                                                              " & vbNewLine _
                                               & "           ,MAX(SEND_NO)  AS SEND_NO                                                                                " & vbNewLine _
                                               & "            FROM $LM_TRN$..H_SENDUNSOEDI_LM                                                                         " & vbNewLine _
                                               & "            GROUP BY                                                                                                " & vbNewLine _
                                               & "            NRS_BR_CD                                                                                               " & vbNewLine _
                                               & "           ,TRIP_NO                                                                                                 " & vbNewLine _
                                               & "           ,UNSO_NO_L                                                                                               " & vbNewLine _
                                               & "           ) HSUL                                                                                                   " & vbNewLine _
                                               & "           ON                                                                                                       " & vbNewLine _
                                               & "               HSUL.NRS_BR_CD = @NRS_BR_CD                                                                          " & vbNewLine _
                                               & "--           AND HSUL.TRIP_NO   = @H_UNSOEDI_TRIP_NO                                                                  " & vbNewLine _
                                               & "           AND HSUL.TRIP_NO   = @TRIP_NO                                                                          " & vbNewLine _
                                               & "           AND HSUL.UNSO_NO_L = @UNSO_NO_L                                                                          " & vbNewLine _
                                               & "     LEFT JOIN $LM_TRN$..F_UNSO_M FM                                                         " & vbNewLine _
                                               & "     ON                                                                                      " & vbNewLine _
                                               & "         FL.NRS_BR_CD = FM.NRS_BR_CD                                                         " & vbNewLine _
                                               & "     AND FL.UNSO_NO_L = FM.UNSO_NO_L                                                         " & vbNewLine _
                                               & "     AND FM.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                               & "     LEFT JOIN $LM_MST$..M_GOODS MGOODS                                                      " & vbNewLine _
                                               & "     ON                                                                                      " & vbNewLine _
                                               & "         MGOODS.NRS_BR_CD = FM.NRS_BR_CD                                                     " & vbNewLine _
                                               & "     AND MGOODS.GOODS_CD_NRS = FM.GOODS_CD_NRS                                               " & vbNewLine _
                                               & "     AND MGOODS.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
                                               & "     LEFT JOIN $LM_MST$..M_SOKO MSOKO                                                        " & vbNewLine _
                                               & "     ON                                                                                      " & vbNewLine _
                                               & "         MSOKO.NRS_BR_CD = FL.NRS_BR_CD                                                      " & vbNewLine _
                                               & "     AND MSOKO.WH_CD = FL.ORIG_CD                                                            " & vbNewLine _
                                               & "     AND FL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                               & "     LEFT JOIN $LM_MST$..Z_KBN Z_KBN_ONDO                                                    " & vbNewLine _
                                               & "     ON                                                                                      " & vbNewLine _
                                               & "         Z_KBN_ONDO.KBN_GROUP_CD = 'U006'                                                    " & vbNewLine _
                                               & "     AND Z_KBN_ONDO.KBN_CD = FM.UNSO_ONDO_KB                                                 " & vbNewLine _
                                               & "     AND Z_KBN_ONDO.SYS_DEL_FLG = '0'                                                        " & vbNewLine

#End Region

#Region "SELECT句(H_SENDUNSOEDI_LM過去分)"

    ''' <summary>
    ''' 手配指示作成データ抽出用(LMデータ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_PRE_SENDUNSOEDI_LM As String = " SELECT                                                            " & vbNewLine _
                                           & "            HEU.NRS_BR_CD                        AS NRS_BR_CD          " & vbNewLine _
                                           & "           ,HEU.UNSO_NO_L                        AS UNSO_NO_L          " & vbNewLine _
                                           & "           ,HEU.SEND_NO                          AS SEND_NO            " & vbNewLine _
                                           & "           ,HEU.TRIP_NO                          AS TRIP_NO            " & vbNewLine _
                                           & "           ,HEU.HENKO_KB                         AS HENKO_KB           " & vbNewLine _
                                           & "           ,HEU.MOTO_DATA_KB                     AS MOTO_DATA_KB       " & vbNewLine _
                                           & "           ,HEU.UNSO_CD                          AS UNSO_CD            " & vbNewLine _
                                           & "           ,HEU.UNSO_BR_CD                       AS UNSO_BR_CD         " & vbNewLine _
                                           & "           ,HEU.WH_CD                            AS WH_CD              " & vbNewLine _
                                           & "           ,HEU.WH_NM                            AS WH_NM              " & vbNewLine _
                                           & "           ,HEU.WH_ZIP                           AS WH_ZIP             " & vbNewLine _
                                           & "           ,HEU.WH_AD_1                          AS WH_AD_1            " & vbNewLine _
                                           & "           ,HEU.WH_AD_2                          AS WH_AD_2            " & vbNewLine _
                                           & "           ,HEU.WH_AD_3                          AS WH_AD_3            " & vbNewLine _
                                           & "           ,HEU.WH_TEL                           AS WH_TEL             " & vbNewLine _
                                           & "           ,HEU.OUTKA_PLAN_DATE                  AS OUTKA_PLAN_DATE    " & vbNewLine _
                                           & "           ,HEU.ARR_PLAN_DATE                    AS ARR_PLAN_DATE      " & vbNewLine _
                                           & "           ,HEU.ARR_PLAN_TIME                    AS ARR_PLAN_TIME      " & vbNewLine _
                                           & "           ,HEU.CUST_CD_L                        AS CUST_CD_L          " & vbNewLine _
                                           & "           ,HEU.CUST_CD_M                        AS CUST_CD_M          " & vbNewLine _
                                           & "           ,HEU.CUST_NM_L                        AS CUST_NM_L          " & vbNewLine _
                                           & "           ,HEU.CUST_NM_M                        AS CUST_NM_M          " & vbNewLine _
                                           & "           ,HEU.SHIP_NM_L                        AS SHIP_NM_L          " & vbNewLine _
                                           & "           ,HEU.DEST_CD                          AS DEST_CD            " & vbNewLine _
                                           & "           ,HEU.DEST_NM                          AS DEST_NM            " & vbNewLine _
                                           & "           ,HEU.DEST_ZIP                         AS DEST_ZIP           " & vbNewLine _
                                           & "           ,HEU.DEST_AD_1                        AS DEST_AD_1          " & vbNewLine _
                                           & "           ,HEU.DEST_AD_2                        AS DEST_AD_2          " & vbNewLine _
                                           & "           ,HEU.DEST_AD_3                        AS DEST_AD_3          " & vbNewLine _
                                           & "           ,HEU.DEST_TEL                         AS DEST_TEL           " & vbNewLine _
                                           & "           ,HEU.DEST_FAX                         AS DEST_FAX           " & vbNewLine _
                                           & "           ,HEU.DEST_JIS_CD                      AS DEST_JIS_CD        " & vbNewLine _
                                           & "           ,HEU.SP_NHS_KB                        AS SP_NHS_KB          " & vbNewLine _
                                           & "           ,HEU.COA_YN                           AS COA_YN             " & vbNewLine _
                                           & "           ,HEU.CUST_ORD_NO                      AS CUST_ORD_NO        " & vbNewLine _
                                           & "           ,HEU.BUYER_ORD_NO                     AS BUYER_ORD_NO       " & vbNewLine _
                                           & "           ,HEU.NHS_REMARK                       AS NHS_REMARK         " & vbNewLine _
                                           & "           ,HEU.REMARK                           AS REMARK             " & vbNewLine _
                                           & "           ,HEU.UNSO_ATT                         AS UNSO_ATT           " & vbNewLine _
                                           & "           ,HEU.DENP_YN                          AS DENP_YN            " & vbNewLine _
                                           & "           ,HEU.PC_KB                            AS PC_KB              " & vbNewLine _
                                           & "           ,HEU.BIN_KB                           AS BIN_KB             " & vbNewLine _
                                           & "           ,HEU.BIN_NM                           AS BIN_NM             " & vbNewLine _
                                           & "           ,HEU.UNSO_PKG_NB                      AS UNSO_PKG_NB        " & vbNewLine _
                                           & "           ,HEU.UNSO_WT                          AS UNSO_WT            " & vbNewLine _
                                           & "           ,HEU.KYORI                            AS KYORI              " & vbNewLine _
                                           & "           ,HEU.DECI_UNCHIN                      AS DECI_UNCHIN        " & vbNewLine _
                                           & "           ,HEU.BIKO1_HED                        AS BIKO1_HED          " & vbNewLine _
                                           & "           ,HEU.BIKO2_HED                        AS BIKO2_HED          " & vbNewLine _
                                           & "           ,HEU.BIKO3_HED                        AS BIKO3_HED          " & vbNewLine _
                                           & "           ,HEU.BIKO4_HED                        AS BIKO4_HED          " & vbNewLine _
                                           & "           ,HEU.UNSO_NO_M                        AS UNSO_NO_M          " & vbNewLine _
                                           & "           ,HEU.GOODS_CD_NRS                     AS GOODS_CD_NRS       " & vbNewLine _
                                           & "           ,HEU.GOODS_NM                         AS GOODS_NM           " & vbNewLine _
                                           & "           ,HEU.COODS_CD_CUST                    AS COODS_CD_CUST      " & vbNewLine _
                                           & "           ,HEU.UNSO_ONDO_KB                     AS UNSO_ONDO_KB       " & vbNewLine _
                                           & "           ,HEU.UNSO_ONDO_NM                     AS UNSO_ONDO_NM       " & vbNewLine _
                                           & "           ,HEU.ONDO_MX                          AS ONDO_MX            " & vbNewLine _
                                           & "           ,HEU.ONDO_MM                          AS ONDO_MM            " & vbNewLine _
                                           & "           ,HEU.CUST_ORD_NO_DTL                  AS CUST_ORD_NO_DTL    " & vbNewLine _
                                           & "           ,HEU.BUYER_ORD_NO_DTL                 AS BUYER_ORD_NO_DTL   " & vbNewLine _
                                           & "           ,HEU.REMARK_OUTKA                     AS REMARK_OUTKA       " & vbNewLine _
                                           & "           ,HEU.REMARK_UNSO                      AS REMARK_UNSO        " & vbNewLine _
                                           & "           ,HEU.LOT_NO                           AS LOT_NO             " & vbNewLine _
                                           & "           ,HEU.SERIAL_NO                        AS SERIAL_NO          " & vbNewLine _
                                           & "           ,HEU.IRIME                            AS IRIME              " & vbNewLine _
                                           & "           ,HEU.IRIME_UT                         AS IRIME_UT           " & vbNewLine _
                                           & "           ,HEU.PKG_UT                           AS PKG_UT             " & vbNewLine _
                                           & "           ,HEU.OUTKA_TTL_NB                     AS OUTKA_TTL_NB       " & vbNewLine _
                                           & "           ,HEU.OUTKA_TTL_QT                     AS OUTKA_TTL_QT       " & vbNewLine _
                                           & "           ,HEU.BETU_WT                          AS BETU_WT            " & vbNewLine _
                                           & "           ,HEU.BIKO1_DTL                        AS BIKO1_DTL          " & vbNewLine _
                                           & "           ,HEU.BIKO2_DTL                        AS BIKO2_DTL          " & vbNewLine _
                                           & "           ,HEU.BIKO3_DTL                        AS BIKO3_DTL          " & vbNewLine _
                                           & "           ,HEU.BIKO4_DTL                        AS BIKO4_DTL          " & vbNewLine _
                                           & "           ,HEU.NUM1_DTL                         AS NUM1_DTL           " & vbNewLine _
                                           & "           ,HEU.NUM2_DTL                         AS NUM2_DTL           " & vbNewLine _
                                           & "           ,HEU.QR_CODE                          AS QR_CODE            " & vbNewLine _
                                           & "           ,HEU.SYS_DEL_FLG                      AS SYS_DEL_FLG        " & vbNewLine _
                                           & "            FROM $LM_TRN$..H_SENDUNSOEDI_LM HEU,                       " & vbNewLine _
                                           & "            (SELECT                                                    " & vbNewLine _
                                           & "                   NRS_BR_CD                     AS NRS_BR_CD          " & vbNewLine _
                                           & "                 , UNSO_NO_L                     AS UNSO_NO_L          " & vbNewLine _
                                           & "                 , UNSO_NO_M                     AS UNSO_NO_M          " & vbNewLine _
                                           & "                 , TRIP_NO                       AS TRIP_NO            " & vbNewLine _
                                           & "                 , MAX(SEND_NO)                  AS SEND_NO            " & vbNewLine _
                                           & "             FROM $LM_TRN$..H_SENDUNSOEDI_LM                           " & vbNewLine _
                                           & "             WHERE                                                     " & vbNewLine _
                                           & "                   NRS_BR_CD = @SUB_NRS_BR_CD                          " & vbNewLine _
                                           & "              AND  UNSO_NO_L = @SUB_UNSO_NO_L                          " & vbNewLine _
                                           & "              AND  TRIP_NO   = @SUB_TRIP_NO                            " & vbNewLine _
                                           & "             GROUP BY                                                  " & vbNewLine _
                                           & "                   NRS_BR_CD                                           " & vbNewLine _
                                           & "                 , UNSO_NO_L                                           " & vbNewLine _
                                           & "                 , UNSO_NO_M                                           " & vbNewLine _
                                           & "                 , TRIP_NO                                             " & vbNewLine _
                                           & "            )SUB_HEU                                                   " & vbNewLine _
                                           & "            WHERE HEU.NRS_BR_CD = SUB_HEU.NRS_BR_CD                    " & vbNewLine _
                                           & "              AND HEU.UNSO_NO_L = SUB_HEU.UNSO_NO_L                    " & vbNewLine _
                                           & "              AND HEU.UNSO_NO_M = SUB_HEU.UNSO_NO_M                    " & vbNewLine _
                                           & "              AND HEU.SEND_NO   = SUB_HEU.SEND_NO                      " & vbNewLine


#End Region

#Region "TEHAIINFO_TBL(COUNT)"

    ''' <summary>
    ''' 手配情報テーブル件数抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_TEHAIINFO_TBL As String = "  SELECT                                                                          " & vbNewLine _
                                              & "  COUNT(*)                                                    AS TEHAI_CNT        " & vbNewLine _
                                              & " ,TI.NRS_BR_CD                                                AS NRS_BR_CD        " & vbNewLine _
                                              & " ,TI.UNSO_NO_L                                                AS UNSO_NO_L        " & vbNewLine _
                                              & " ,TI.TRIP_NO                                                  AS TRIP_NO          " & vbNewLine _
                                              & "  FROM $LM_TRN$..H_TEHAIINFO_TBL TI                                                 " & vbNewLine _
                                              & "  WHERE                                                                           " & vbNewLine _
                                              & "      TI.NRS_BR_CD = @NRS_BR_CD                                                   " & vbNewLine _
                                              & "  AND TI.UNSO_NO_L = @UNSO_NO_L                                                   " & vbNewLine _
                                              & "  AND TI.TRIP_NO   = @TRIP_NO                                                     " & vbNewLine _
                                              & "  GROUP BY                                                                        " & vbNewLine _
                                              & "  TI.NRS_BR_CD                                                                    " & vbNewLine _
                                              & " ,TI.UNSO_NO_L                                                                    " & vbNewLine _
                                              & " ,TI.TRIP_NO                                                                      " & vbNewLine

#End Region

    ''' <summary>
    ''' 手配指示作成データ抽出用(Lデータ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_XNG_ROJITYPE As String = " SELECT                                                                                                                                                                                                         " & vbNewLine _
                                   & "   CASE MAIN_DELI_KB WHEN '1' THEN '幹線' WHEN '3' THEN '直送' END    AS MAIN_DELI_KB                                                                                                                          " & vbNewLine _
                                   & ",TRIP_NO                                                                                                                                                                                                       " & vbNewLine _
                                   & ",LAW_KBN                                                                                                                                                                                                       " & vbNewLine _
                                   & ",HIN_GROUP                                                                                                                                                                                                     " & vbNewLine _
                                   & ",HIN_GROUP_NM                                                                                                                                                                                                  " & vbNewLine _
                                   & ",PKG_UT                                                                                                                                                                                                        " & vbNewLine _
                                   & ",DECI_WT DECI_WT                                                                                                                                                                                               " & vbNewLine _
                                   & ",YOKI_WT * DECI_NG_NB AS YOKI_WT                                                                                                                                                                               " & vbNewLine _
                                   & ",REMARK                                                                                                                                                                                                        " & vbNewLine _
                                   & ",SEIQ_KYORI                                                                                                                                                                                                  " & vbNewLine _
                                   & ",UNSO_NO_M                                                                                                                                                                                                     " & vbNewLine _
                                   & ",TSUMIKOMI_JIKAN                                                                                                                                                                                               " & vbNewLine _
                                   & ",TSUMIKOMI_BASYO                                                                                                                                                                                               " & vbNewLine _
                                   & ",KIJI_REMARK_1                                                                                                                                                                                                 " & vbNewLine _
                                   & ",KIJI_REMARK_2                                                                                                                                                                                                 " & vbNewLine _
                                   & ",CUST_ORD_NO                                                                                                                                                                                                   " & vbNewLine _
                                   & ",GROSS_WEIGHT                                                                                                                                                                                                  " & vbNewLine _
                                   & "FROM                                                                                                                                                                                                           " & vbNewLine _
                                   & "(                                                                                                                                                                                                              " & vbNewLine _
                                   & "SELECT                                                                                                                                                                                                         " & vbNewLine _
                                   & "     UNSOLL.TRIP_NO			 AS TRIP_NO                                                                                                                                                                      " & vbNewLine _
                                   & " , (SELECT MGD.SET_NAIYO FROM $LM_MST$..M_GOODS_DETAILS MGD WHERE GOODS.NRS_BR_CD = MGD.NRS_BR_CD AND GOODS.GOODS_CD_NRS = MGD.GOODS_CD_NRS AND MGD.SUB_KB = '14') AS 	LAW_KBN                                  " & vbNewLine _
                                   & " , GOODS.SEARCH_KEY_1	AS 	HIN_GROUP                                                                                                                                                                            " & vbNewLine _
                                   & " , (SELECT ZK.KBN_NM2 FROM $LM_MST$..Z_KBN ZK WHERE ZK.KBN_GROUP_CD = 'S086' AND GOODS.SEARCH_KEY_1 = ZK.KBN_NM1)	AS 	HIN_GROUP_NM                                                                             " & vbNewLine _
                                   & " , (SELECT MGD.SET_NAIYO FROM $LM_MST$..M_GOODS_DETAILS MGD WHERE GOODS.NRS_BR_CD = MGD.NRS_BR_CD AND GOODS.GOODS_CD_NRS = MGD.GOODS_CD_NRS AND MGD.SUB_KB = '15')	AS 	PKG_UT                                   " & vbNewLine _
                                   & " , CONVERT(NUMERIC(12,3),ISNULL((SELECT MGD.SET_NAIYO FROM $LM_MST$..M_GOODS_DETAILS MGD WHERE GOODS.NRS_BR_CD = MGD.NRS_BR_CD AND GOODS.GOODS_CD_NRS = MGD.GOODS_CD_NRS AND MGD.SUB_KB = '17'),0)) AS 	YOKI_WT  " & vbNewLine _
                                   & " , UNCHIN.DECI_NG_NB     AS DECI_NG_NB                                                                                                                                                                         " & vbNewLine _
                                   & " , UNCHIN.DECI_WT - ISNULL(MUNCHIN.DECI_WT,0)        AS DECI_WT                                                                                                                                                " & vbNewLine _
                                   & " , UNCHIN.REMARK         AS REMARK                                                                                                                                                                             " & vbNewLine _
                                   & " , CASE WHEN MK1.KYORI IS NOT NULL THEN MK1.KYORI                                                                                                                                                              " & vbNewLine _
                                   & "        WHEN MK2.KYORI IS NOT NULL THEN MK2.KYORI                                                                                                                                                              " & vbNewLine _
                                   & "        ELSE 0                                                                                                                                                                                                 " & vbNewLine _
                                   & "   END	                 AS SEIQ_KYORI                                                                                                                                                                       " & vbNewLine _
                                   & " , UNSO.UNSO_NO_L        AS UNSO_NO_L                                                                                                                                                                          " & vbNewLine _
                                   & " , UNSO.MAIN_DELI_KB     AS MAIN_DELI_KB                                                                                                                                                                       " & vbNewLine _
                                   & " , UNSOM.UNSO_NO_M       AS UNSO_NO_M                                                                                                                                                                          " & vbNewLine _
                                   & " , HL.FREE_C15           AS TSUMIKOMI_JIKAN                                                                                                                                                                    " & vbNewLine _
                                   & " , HL.FREE_C24           AS TSUMIKOMI_BASYO                                                                                                                                                                    " & vbNewLine _
                                   & " , RTRIM(HL.FREE_C12) + ' ' + RTRIM(HL.FREE_C13) + ' ' + RTRIM(HL.FREE_C14)       AS KIJI_REMARK_1                                                                                                             " & vbNewLine _
                                   & " , RTRIM(HL.FREE_C15) + ' ' + RTRIM(HL.FREE_C24)                                  AS KIJI_REMARK_2                                                                                                             " & vbNewLine _
                                   & " , RTRIM(HL.FREE_C01)                                                             AS CUST_ORD_NO                                                                                                               " & vbNewLine _
                                   & " , (ISNULL(GOODS.STD_WT_KGS,0) * ISNULL(UNSOM.UNSO_TTL_NB,0)) + ISNULL(MGD17.SET_NAIYO,0) * ISNULL(UNSOM.UNSO_TTL_NB,0)    AS  GROSS_WEIGHT                                                                    " & vbNewLine _
                                   & "FROM                                                                                                                                                                                                           " & vbNewLine _
                                   & " $LM_TRN$..F_UNSO_LL AS UNSOLL                                                                                                                                                                                   " & vbNewLine _
                                   & " --運送L                                                                                                                                                                                                       " & vbNewLine _
                                   & " LEFT JOIN $LM_TRN$..F_UNSO_L AS UNSO                                                                                                                                                                            " & vbNewLine _
                                   & " ON UNSOLL.TRIP_NO=UNSO.TRIP_NO                                                                                                                                                                                " & vbNewLine _
                                   & " AND UNSO.NRS_BR_CD =  @NRS_BR_CD                                                                                                                                                                              " & vbNewLine _
                                   & " LEFT JOIN $LM_TRN$..F_UNCHIN_TRS AS UNCHIN                                                                                                                                                                      " & vbNewLine _
                                   & " ON UNCHIN.UNSO_NO_L=UNSO.UNSO_NO_L                                                                                                                                                                            " & vbNewLine _
                                   & " AND UNSO.SYS_DEL_FLG='0' --運送LL                                                                                                                                                                             " & vbNewLine _
                                   & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L AS HL                                                                                                                                                                        " & vbNewLine _
                                   & " ON UNSO.NRS_BR_CD = HL.NRS_BR_CD                                                                                                                                                                              " & vbNewLine _
                                   & " AND UNSO.UNSO_NO_L = RIGHT(RTRIM(HL.FREE_C30),9)                                                                                                                                                               " & vbNewLine _
                                   & " AND UNSO.SYS_DEL_FLG='0'                                                                                                                                                                                      " & vbNewLine _
                                   & " LEFT JOIN (                                                                                                                                                                                                   " & vbNewLine _
                                   & "      SELECT SEIQ_GROUP_NO AS UNSO_NO_L,SUM(DECI_WT) AS DECI_WT FROM $LM_TRN$..F_UNCHIN_TRS                                                                                                                      " & vbNewLine _
                                   & "      WHERE NRS_BR_CD =  @NRS_BR_CD AND SEIQ_GROUP_NO <> '' AND SEIQ_GROUP_NO <> UNSO_NO_L AND SYS_DEL_FLG='0'                                                                                                 " & vbNewLine _
                                   & "      GROUP BY SEIQ_GROUP_NO) MUNCHIN                                                                                                                                                                          " & vbNewLine _
                                   & " ON UNCHIN.UNSO_NO_L=MUNCHIN.UNSO_NO_L --まとめ運賃重量                                                                                                                                                        " & vbNewLine _
                                   & " --運送Ｍ                                                                                                                                                                                                      " & vbNewLine _
                                   & " LEFT JOIN $LM_TRN$..F_UNSO_M AS UNSOM                                                                                                                                                                           " & vbNewLine _
                                   & "  ON UNSO.UNSO_NO_L=UNSOM.UNSO_NO_L                                                                                                                                                                            " & vbNewLine _
                                   & " --請求マスタ                                                                                                                                                                                                  " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_SEIQTO AS SEIQTO                                                                                                                                                                          " & vbNewLine _
                                   & " ON UNCHIN.NRS_BR_CD = SEIQTO.NRS_BR_CD                                                                                                                                                                        " & vbNewLine _
                                   & " AND UNCHIN.SEIQTO_CD = SEIQTO.SEIQTO_CD                                                                                                                                                                       " & vbNewLine _
                                   & " --商品マスタ                                                                                                                                                                                                  " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_GOODS AS GOODS                                                                                                                                                                            " & vbNewLine _
                                   & " ON UNSOM.NRS_BR_CD=GOODS.NRS_BR_CD                                                                                                                                                                            " & vbNewLine _
                                   & " AND UNSOM.GOODS_CD_NRS=GOODS.GOODS_CD_NRS                                                                                                                                                                     " & vbNewLine _
                                   & " --商品マスタ明細(荷姿)                                                                                                                                                                                        " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_GOODS_DETAILS AS MGD17                                                                                                                                                                  " & vbNewLine _
                                   & " ON GOODS.NRS_BR_CD=MGD17.NRS_BR_CD                                                                                                                                                                            " & vbNewLine _
                                   & " AND GOODS.GOODS_CD_NRS=MGD17.GOODS_CD_NRS                                                                                                                                                                     " & vbNewLine _
                                   & " AND MGD17.SUB_KB = '17'                                                                                                                                                                                       " & vbNewLine _
                                   & " AND MGD17.SYS_DEL_FLG = '0'                                                                                                                                                                                   " & vbNewLine _
                                   & " --荷主マスタ                                                                                                                                                                                                  " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_CUST AS CUST                                                                                                                                                                              " & vbNewLine _
                                   & " ON GOODS.NRS_BR_CD=CUST.NRS_BR_CD                                                                                                                                                                             " & vbNewLine _
                                   & " AND GOODS.CUST_CD_L=CUST.CUST_CD_L                                                                                                                                                                            " & vbNewLine _
                                   & " AND GOODS.CUST_CD_M = CUST.CUST_CD_M                                                                                                                                                                          " & vbNewLine _
                                   & " AND GOODS.CUST_CD_S= CUST.CUST_CD_S                                                                                                                                                                           " & vbNewLine _
                                   & " AND GOODS.CUST_CD_SS= CUST.CUST_CD_SS                                                                                                                                                                         " & vbNewLine _
                                   & " --営業所マスタ                                                                                                                                                                                                " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_NRS_BR AS NRS                                                                                                                                                                             " & vbNewLine _
                                   & " ON UNCHIN.NRS_BR_CD = NRS.NRS_BR_CD                                                                                                                                                                           " & vbNewLine _
                                   & " --届先マスタ                                                                                                                                                                                                  " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_DEST AS DEST                                                                                                                                                                              " & vbNewLine _
                                   & " ON UNSO.NRS_BR_CD = DEST.NRS_BR_CD                                                                                                                                                                            " & vbNewLine _
                                   & " AND UNSO.CUST_CD_L = DEST.CUST_CD_L                                                                                                                                                                           " & vbNewLine _
                                   & " AND UNSO.DEST_CD   = DEST.DEST_CD                                                                                                                                                                             " & vbNewLine _
                                   & " --JISマスタ                                                                                                                                                                                                   " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_JIS AS JIS                                                                                                                                                                                " & vbNewLine _
                                   & " ON JIS.JIS_CD   = DEST.JIS                                                                                                                                                                                    " & vbNewLine _
                                   & "--運送会社マスタ                                                                                                                                                                                               " & vbNewLine _
                                   & "LEFT JOIN $LM_MST$..M_UNSOCO MUNSO                                                                                                                                                                               " & vbNewLine _
                                   & "ON UNSOLL.NRS_BR_CD = MUNSO.NRS_BR_CD                                                                                                                                                                          " & vbNewLine _
                                   & "AND UNSOLL.UNSOCO_CD = MUNSO.UNSOCO_CD                                                                                                                                                                         " & vbNewLine _
                                   & "AND UNSOLL.UNSOCO_BR_CD = MUNSO.UNSOCO_BR_CD                                                                                                                                                                   " & vbNewLine _
                                   & "                                                                                                                                                                                                               " & vbNewLine _
                                   & "--届先マスタ（届先配達）                                                                                                                                                                                 " & vbNewLine _
                                   & "LEFT JOIN $LM_MST$..M_DEST MD1                                                                                                                                                                           " & vbNewLine _
                                   & "ON HL.NRS_BR_CD = MD1.NRS_BR_CD                                                                                                                                                                          " & vbNewLine _
                                   & "and HL.CUST_CD_L = MD1.CUST_CD_L                                                                                                                                                                         " & vbNewLine _
                                   & "and HL.DEST_CD = MD1.DEST_CD                                                                                                                                                                             " & vbNewLine _
                                   & "--届先マスタ（積込先）                                                                                                                                                                                   " & vbNewLine _
                                   & "LEFT JOIN $LM_MST$..M_DEST MD2                                                                                                                                                                           " & vbNewLine _
                                   & "ON HL.NRS_BR_CD = MD2.NRS_BR_CD                                                                                                                                                                          " & vbNewLine _
                                   & "and HL.CUST_CD_L = MD2.CUST_CD_L                                                                                                                                                                         " & vbNewLine _
                                   & "and HL.FREE_C22 = MD2.DEST_CD                                                                                                                                                                            " & vbNewLine _
                                   & "--距離程M(ORIG_CD < DEST_CD)                                                                                                                                                                             " & vbNewLine _
                                   & "LEFT JOIN $LM_MST$..M_KYORI MK1                                                                                                                                                                          " & vbNewLine _
                                   & "ON  MK1.SYS_DEL_FLG = '0'                                                                                                                                                                                " & vbNewLine _
                                   & "AND MK1.NRS_BR_CD = HL.NRS_BR_CD                                                                                                                                                                         " & vbNewLine _
                                   & "AND MK1.KYORI_CD  = CUST.BETU_KYORI_CD                                                                                                                                                                   " & vbNewLine _
                                   & "AND MK1.ORIG_JIS_CD = MD2.JIS                                                                                                                                                                            " & vbNewLine _
                                   & "AND MK1.DEST_JIS_CD = HL.DEST_JIS_CD                                                                                                                                                                     " & vbNewLine _
                                   & "--距離程M(DEST_CD < ORIG_CD)                                                                                                                                                                             " & vbNewLine _
                                   & "LEFT JOIN $LM_MST$..M_KYORI MK2                                                                                                                                                                          " & vbNewLine _
                                   & "ON  MK2.SYS_DEL_FLG = '0'                                                                                                                                                                                " & vbNewLine _
                                   & "AND MK2.NRS_BR_CD = HL.NRS_BR_CD                                                                                                                                                                         " & vbNewLine _
                                   & "AND MK2.KYORI_CD  = CUST.BETU_KYORI_CD                                                                                                                                                                   " & vbNewLine _
                                   & "AND MK2.ORIG_JIS_CD = HL.DEST_JIS_CD                                                                                                                                                                     " & vbNewLine _
                                   & "AND MK2.DEST_JIS_CD = MD2.JIS                                                                                                                                                                            " & vbNewLine _
                                   & "WHERE UNCHIN.NRS_BR_CD   = @NRS_BR_CD                                                                                                                                                                          " & vbNewLine _
                                   & " AND UNCHIN.SYS_DEL_FLG = '0'                                                                                                                                                                                  " & vbNewLine _
                                   & " AND UNSOLL.NRS_BR_CD = @NRS_BR_CD                                                                                                                                                                             " & vbNewLine _
                                   & " AND UNSOLL.TRIP_NO = @TRIP_NO                                                                                                                                                                                 " & vbNewLine _
                                   & " AND UNSO.UNSO_NO_L = @UNSO_NO_L                                                                                                                                                                               " & vbNewLine _
                                   & ") BASE                                                                                                                                                                                                         " & vbNewLine _
                                   & "                                                                                                                                                                                                               " & vbNewLine _
                                   & "ORDER BY                                                                                                                                                                                                       " & vbNewLine _
                                   & "    MAIN_DELI_KB                                                                                                                                                                                               " & vbNewLine _
                                   & "    ,TRIP_NO                                                                                                                                                                                                   " & vbNewLine

#End Region

#Region "GROUP BY"
    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY                      " & vbNewLine _
                                         & "       UNSO_L.NRS_BR_CD        " & vbNewLine _
                                         & "     , UNSO_L.TRIP_NO          " & vbNewLine _
                                         & "     , UNSO_L.OUTKA_PLAN_DATE  " & vbNewLine _
                                         & "     , UNSO_L.ARR_PLAN_DATE    " & vbNewLine _
                                         & "     , UNSO_L.UNSO_WT          " & vbNewLine _
                                         & "     , UNSO_L.UNSO_NO_L        " & vbNewLine _
                                         & "     , UNSO_L.MOTO_DATA_KB     " & vbNewLine _
                                         & "     , UNSO_L.SYS_ENT_USER     " & vbNewLine _
                                         & "     , UNSO_M.GOODS_NM         " & vbNewLine _
                                         & "     , UNSO_LL.DRIVER_CD       " & vbNewLine _
                                         & "     , M_CUST.CUST_NM_L        " & vbNewLine _
                                         & "     , M_DEST.DEST_NM          " & vbNewLine _
                                         & "     , M_DEST.AD_1             " & vbNewLine _
                                         & "     , UNSO_M.UNSO_NO_M        " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY_L
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_L As String = " ORDER BY                      " & vbNewLine _
                                         & "       FLL.NRS_BR_CD             " & vbNewLine _
                                         & "     , FLL.TRIP_NO               " & vbNewLine _
                                         & "     , FL.UNSO_NO_L              " & vbNewLine

    ''' <summary>
    ''' ORDER BY_PRE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_PRE_ORDER_BY As String = " ORDER BY                    " & vbNewLine _
                                         & "       HEU.NRS_BR_CD             " & vbNewLine _
                                         & "     , HEU.UNSO_NO_L             " & vbNewLine _
                                         & "     , HEU.UNSO_NO_M             " & vbNewLine _
                                         & "     , HEU.SEND_NO               " & vbNewLine

    ''' <summary>
    ''' ORDER BY_M
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_M As String = " ORDER BY                      " & vbNewLine _
                                         & "       FLL.NRS_BR_CD             " & vbNewLine _
                                         & "     , FLL.TRIP_NO               " & vbNewLine _
                                         & "     , FL.UNSO_NO_L              " & vbNewLine _
                                         & "     , FM.UNSO_NO_M              " & vbNewLine

#End Region

#End Region

#Region "INSERT処理 SQL"

#Region "H_SENDUNSOEDI_LM"

    ''' <summary>
    '''  運送会社用EDI送信データLM追加処理
    ''' </summary>
    ''' <remarks>運送会社用EDI送信データLMへINSERT</remarks>

    Private Const SQL_INSERT_H_SENDUNSOEDI_LM As String = "INSERT INTO $LM_TRN$..H_SENDUNSOEDI_LM" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",UNSO_NO_L                    " & vbNewLine _
                                              & ",UNSO_NO_M                    " & vbNewLine _
                                              & ",SEND_NO                      " & vbNewLine _
                                              & ",TRIP_NO                      " & vbNewLine _
                                              & ",HENKO_KB                     " & vbNewLine _
                                              & ",MOTO_DATA_KB                 " & vbNewLine _
                                              & ",UNSO_CD                      " & vbNewLine _
                                              & ",UNSO_BR_CD                   " & vbNewLine _
                                              & ",WH_CD                        " & vbNewLine _
                                              & ",WH_NM                        " & vbNewLine _
                                              & ",WH_ZIP                       " & vbNewLine _
                                              & ",WH_AD_1                      " & vbNewLine _
                                              & ",WH_AD_2                      " & vbNewLine _
                                              & ",WH_AD_3                      " & vbNewLine _
                                              & ",WH_TEL                       " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE              " & vbNewLine _
                                              & ",ARR_PLAN_DATE                " & vbNewLine _
                                              & ",ARR_PLAN_TIME                " & vbNewLine _
                                              & ",CUST_CD_L                    " & vbNewLine _
                                              & ",CUST_CD_M                    " & vbNewLine _
                                              & ",CUST_NM_L                    " & vbNewLine _
                                              & ",CUST_NM_M                    " & vbNewLine _
                                              & ",SHIP_NM_L                    " & vbNewLine _
                                              & ",DEST_CD                      " & vbNewLine _
                                              & ",DEST_NM                      " & vbNewLine _
                                              & ",DEST_ZIP                     " & vbNewLine _
                                              & ",DEST_AD_1                    " & vbNewLine _
                                              & ",DEST_AD_2                    " & vbNewLine _
                                              & ",DEST_AD_3                    " & vbNewLine _
                                              & ",DEST_TEL                     " & vbNewLine _
                                              & ",DEST_FAX                     " & vbNewLine _
                                              & ",DEST_JIS_CD                  " & vbNewLine _
                                              & ",SP_NHS_KB                    " & vbNewLine _
                                              & ",COA_YN                       " & vbNewLine _
                                              & ",CUST_ORD_NO                  " & vbNewLine _
                                              & ",BUYER_ORD_NO                 " & vbNewLine _
                                              & ",NHS_REMARK                   " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",UNSO_ATT                     " & vbNewLine _
                                              & ",DENP_YN                      " & vbNewLine _
                                              & ",PC_KB                        " & vbNewLine _
                                              & ",BIN_KB                       " & vbNewLine _
                                              & ",BIN_NM                       " & vbNewLine _
                                              & ",UNSO_PKG_NB                  " & vbNewLine _
                                              & ",UNSO_WT                      " & vbNewLine _
                                              & ",KYORI                        " & vbNewLine _
                                              & ",DECI_UNCHIN                  " & vbNewLine _
                                              & ",BIKO1_HED                    " & vbNewLine _
                                              & ",BIKO2_HED                    " & vbNewLine _
                                              & ",BIKO3_HED                    " & vbNewLine _
                                              & ",BIKO4_HED                    " & vbNewLine _
                                              & ",GOODS_CD_NRS                 " & vbNewLine _
                                              & ",GOODS_NM                     " & vbNewLine _
                                              & ",COODS_CD_CUST                " & vbNewLine _
                                              & ",UNSO_ONDO_KB                 " & vbNewLine _
                                              & ",UNSO_ONDO_NM                 " & vbNewLine _
                                              & ",ONDO_MX                      " & vbNewLine _
                                              & ",ONDO_MM                      " & vbNewLine _
                                              & ",CUST_ORD_NO_DTL              " & vbNewLine _
                                              & ",BUYER_ORD_NO_DTL             " & vbNewLine _
                                              & ",REMARK_OUTKA                 " & vbNewLine _
                                              & ",REMARK_UNSO                  " & vbNewLine _
                                              & ",LOT_NO                       " & vbNewLine _
                                              & ",SERIAL_NO                    " & vbNewLine _
                                              & ",IRIME                        " & vbNewLine _
                                              & ",IRIME_UT                     " & vbNewLine _
                                              & ",PKG_UT                       " & vbNewLine _
                                              & ",OUTKA_TTL_NB                 " & vbNewLine _
                                              & ",OUTKA_TTL_QT                 " & vbNewLine _
                                              & ",BETU_WT                      " & vbNewLine _
                                              & ",BIKO1_DTL                    " & vbNewLine _
                                              & ",BIKO2_DTL                    " & vbNewLine _
                                              & ",BIKO3_DTL                    " & vbNewLine _
                                              & ",BIKO4_DTL                    " & vbNewLine _
                                              & ",NUM1_DTL                     " & vbNewLine _
                                              & ",NUM2_DTL                     " & vbNewLine _
                                              & ",QR_CODE                      " & vbNewLine _
                                              & ",SEND_SHORI_FLG               " & vbNewLine _
                                              & ",SEND_USER                    " & vbNewLine _
                                              & ",SEND_DATE                    " & vbNewLine _
                                              & ",SEND_TIME                    " & vbNewLine _
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
                                              & ",@SEND_NO                     " & vbNewLine _
                                              & ",@TRIP_NO                     " & vbNewLine _
                                              & ",@HENKO_KB                    " & vbNewLine _
                                              & ",@MOTO_DATA_KB                " & vbNewLine _
                                              & ",@UNSO_CD                     " & vbNewLine _
                                              & ",@UNSO_BR_CD                  " & vbNewLine _
                                              & ",@WH_CD                       " & vbNewLine _
                                              & ",@WH_NM                       " & vbNewLine _
                                              & ",@WH_ZIP                      " & vbNewLine _
                                              & ",@WH_AD_1                     " & vbNewLine _
                                              & ",@WH_AD_2                     " & vbNewLine _
                                              & ",@WH_AD_3                     " & vbNewLine _
                                              & ",@WH_TEL                      " & vbNewLine _
                                              & ",@OUTKA_PLAN_DATE             " & vbNewLine _
                                              & ",@ARR_PLAN_DATE               " & vbNewLine _
                                              & ",@ARR_PLAN_TIME               " & vbNewLine _
                                              & ",@CUST_CD_L                   " & vbNewLine _
                                              & ",@CUST_CD_M                   " & vbNewLine _
                                              & ",@CUST_NM_L                   " & vbNewLine _
                                              & ",@CUST_NM_M                   " & vbNewLine _
                                              & ",@SHIP_NM_L                   " & vbNewLine _
                                              & ",@DEST_CD                     " & vbNewLine _
                                              & ",@DEST_NM                     " & vbNewLine _
                                              & ",@DEST_ZIP                    " & vbNewLine _
                                              & ",@DEST_AD_1                   " & vbNewLine _
                                              & ",@DEST_AD_2                   " & vbNewLine _
                                              & ",@DEST_AD_3                   " & vbNewLine _
                                              & ",@DEST_TEL                    " & vbNewLine _
                                              & ",@DEST_FAX                    " & vbNewLine _
                                              & ",@DEST_JIS_CD                 " & vbNewLine _
                                              & ",@SP_NHS_KB                   " & vbNewLine _
                                              & ",@COA_YN                      " & vbNewLine _
                                              & ",@CUST_ORD_NO                 " & vbNewLine _
                                              & ",@BUYER_ORD_NO                " & vbNewLine _
                                              & ",@NHS_REMARK                  " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@UNSO_ATT                    " & vbNewLine _
                                              & ",@DENP_YN                     " & vbNewLine _
                                              & ",@PC_KB                       " & vbNewLine _
                                              & ",@BIN_KB                      " & vbNewLine _
                                              & ",@BIN_NM                      " & vbNewLine _
                                              & ",@UNSO_PKG_NB                 " & vbNewLine _
                                              & ",@UNSO_WT                     " & vbNewLine _
                                              & ",@KYORI                       " & vbNewLine _
                                              & ",@DECI_UNCHIN                 " & vbNewLine _
                                              & ",@BIKO1_HED                   " & vbNewLine _
                                              & ",@BIKO2_HED                   " & vbNewLine _
                                              & ",@BIKO3_HED                   " & vbNewLine _
                                              & ",@BIKO4_HED                   " & vbNewLine _
                                              & ",@GOODS_CD_NRS                " & vbNewLine _
                                              & ",@GOODS_NM                    " & vbNewLine _
                                              & ",@COODS_CD_CUST               " & vbNewLine _
                                              & ",@UNSO_ONDO_KB                " & vbNewLine _
                                              & ",@UNSO_ONDO_NM                " & vbNewLine _
                                              & ",@ONDO_MX                     " & vbNewLine _
                                              & ",@ONDO_MM                     " & vbNewLine _
                                              & ",@CUST_ORD_NO_DTL             " & vbNewLine _
                                              & ",@BUYER_ORD_NO_DTL            " & vbNewLine _
                                              & ",@REMARK_OUTKA                " & vbNewLine _
                                              & ",@REMARK_UNSO                 " & vbNewLine _
                                              & ",@LOT_NO                      " & vbNewLine _
                                              & ",@SERIAL_NO                   " & vbNewLine _
                                              & ",@IRIME                       " & vbNewLine _
                                              & ",@IRIME_UT                    " & vbNewLine _
                                              & ",@PKG_UT                      " & vbNewLine _
                                              & ",@OUTKA_TTL_NB                " & vbNewLine _
                                              & ",@OUTKA_TTL_QT                " & vbNewLine _
                                              & ",@BETU_WT                     " & vbNewLine _
                                              & ",@BIKO1_DTL                   " & vbNewLine _
                                              & ",@BIKO2_DTL                   " & vbNewLine _
                                              & ",@BIKO3_DTL                   " & vbNewLine _
                                              & ",@BIKO4_DTL                   " & vbNewLine _
                                              & ",@NUM1_DTL                    " & vbNewLine _
                                              & ",@NUM2_DTL                    " & vbNewLine _
                                              & ",@QR_CODE                     " & vbNewLine _
                                              & ",'2'                          " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
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

#Region "H_SENDUNSOEDI_L"
    ''' <summary>
    '''  運送会社用EDI送信データL追加処理
    ''' </summary>
    ''' <remarks>運送会社用EDI送信データLへINSERT</remarks>

    Private Const SQL_INSERT_H_SENDUNSOEDI_L As String = "INSERT INTO $LM_TRN$..H_SENDUNSOEDI_L" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",UNSO_NO_L                    " & vbNewLine _
                                              & ",SEND_NO                      " & vbNewLine _
                                              & ",TRIP_NO                      " & vbNewLine _
                                              & ",HENKO_KB                     " & vbNewLine _
                                              & ",MOTO_DATA_KB                 " & vbNewLine _
                                              & ",UNSO_CD                      " & vbNewLine _
                                              & ",UNSO_BR_CD                   " & vbNewLine _
                                              & ",WH_CD                        " & vbNewLine _
                                              & ",WH_NM                        " & vbNewLine _
                                              & ",WH_ZIP                       " & vbNewLine _
                                              & ",WH_AD_1                      " & vbNewLine _
                                              & ",WH_AD_2                      " & vbNewLine _
                                              & ",WH_AD_3                      " & vbNewLine _
                                              & ",WH_TEL                       " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE              " & vbNewLine _
                                              & ",ARR_PLAN_DATE                " & vbNewLine _
                                              & ",ARR_PLAN_TIME                " & vbNewLine _
                                              & ",CUST_CD_L                    " & vbNewLine _
                                              & ",CUST_CD_M                    " & vbNewLine _
                                              & ",CUST_NM_L                    " & vbNewLine _
                                              & ",CUST_NM_M                    " & vbNewLine _
                                              & ",SHIP_NM_L                    " & vbNewLine _
                                              & ",DEST_CD                      " & vbNewLine _
                                              & ",DEST_NM                      " & vbNewLine _
                                              & ",DEST_ZIP                     " & vbNewLine _
                                              & ",DEST_AD_1                    " & vbNewLine _
                                              & ",DEST_AD_2                    " & vbNewLine _
                                              & ",DEST_AD_3                    " & vbNewLine _
                                              & ",DEST_TEL                     " & vbNewLine _
                                              & ",DEST_FAX                     " & vbNewLine _
                                              & ",DEST_JIS_CD                  " & vbNewLine _
                                              & ",SP_NHS_KB                    " & vbNewLine _
                                              & ",COA_YN                       " & vbNewLine _
                                              & ",CUST_ORD_NO                  " & vbNewLine _
                                              & ",BUYER_ORD_NO                 " & vbNewLine _
                                              & ",NHS_REMARK                   " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",UNSO_ATT                     " & vbNewLine _
                                              & ",DENP_YN                      " & vbNewLine _
                                              & ",PC_KB                        " & vbNewLine _
                                              & ",BIN_KB                       " & vbNewLine _
                                              & ",BIN_NM                       " & vbNewLine _
                                              & ",UNSO_PKG_NB                  " & vbNewLine _
                                              & ",UNSO_WT                      " & vbNewLine _
                                              & ",KYORI                        " & vbNewLine _
                                              & ",DECI_UNCHIN                  " & vbNewLine _
                                              & ",BIKO1                        " & vbNewLine _
                                              & ",BIKO2                        " & vbNewLine _
                                              & ",BIKO3                        " & vbNewLine _
                                              & ",BIKO4                        " & vbNewLine _
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
                                              & ",@SEND_NO                     " & vbNewLine _
                                              & ",@TRIP_NO                     " & vbNewLine _
                                              & ",@HENKO_KB                    " & vbNewLine _
                                              & ",@MOTO_DATA_KB                " & vbNewLine _
                                              & ",@UNSO_CD                     " & vbNewLine _
                                              & ",@UNSO_BR_CD                  " & vbNewLine _
                                              & ",@WH_CD                       " & vbNewLine _
                                              & ",@WH_NM                       " & vbNewLine _
                                              & ",@WH_ZIP                      " & vbNewLine _
                                              & ",@WH_AD_1                     " & vbNewLine _
                                              & ",@WH_AD_2                     " & vbNewLine _
                                              & ",@WH_AD_3                     " & vbNewLine _
                                              & ",@WH_TEL                      " & vbNewLine _
                                              & ",@OUTKA_PLAN_DATE             " & vbNewLine _
                                              & ",@ARR_PLAN_DATE               " & vbNewLine _
                                              & ",@ARR_PLAN_TIME               " & vbNewLine _
                                              & ",@CUST_CD_L                   " & vbNewLine _
                                              & ",@CUST_CD_M                   " & vbNewLine _
                                              & ",@CUST_NM_L                   " & vbNewLine _
                                              & ",@CUST_NM_M                   " & vbNewLine _
                                              & ",@SHIP_NM_L                   " & vbNewLine _
                                              & ",@DEST_CD                     " & vbNewLine _
                                              & ",@DEST_NM                     " & vbNewLine _
                                              & ",@DEST_ZIP                    " & vbNewLine _
                                              & ",@DEST_AD_1                   " & vbNewLine _
                                              & ",@DEST_AD_2                   " & vbNewLine _
                                              & ",@DEST_AD_3                   " & vbNewLine _
                                              & ",@DEST_TEL                    " & vbNewLine _
                                              & ",@DEST_FAX                    " & vbNewLine _
                                              & ",@DEST_JIS_CD                 " & vbNewLine _
                                              & ",@SP_NHS_KB                   " & vbNewLine _
                                              & ",@COA_YN                      " & vbNewLine _
                                              & ",@CUST_ORD_NO                 " & vbNewLine _
                                              & ",@BUYER_ORD_NO                " & vbNewLine _
                                              & ",@NHS_REMARK                  " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@UNSO_ATT                    " & vbNewLine _
                                              & ",@DENP_YN                     " & vbNewLine _
                                              & ",@PC_KB                       " & vbNewLine _
                                              & ",@BIN_KB                      " & vbNewLine _
                                              & ",@BIN_NM                      " & vbNewLine _
                                              & ",@UNSO_PKG_NB                 " & vbNewLine _
                                              & ",@UNSO_WT                     " & vbNewLine _
                                              & ",@KYORI                       " & vbNewLine _
                                              & ",@DECI_UNCHIN                 " & vbNewLine _
                                              & ",@BIKO1                       " & vbNewLine _
                                              & ",@BIKO2                       " & vbNewLine _
                                              & ",@BIKO3                       " & vbNewLine _
                                              & ",@BIKO4                       " & vbNewLine _
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

#Region "H_SENDUNSOEDI_M"
    ''' <summary>
    '''  運送会社用EDI送信データM追加処理
    ''' </summary>
    ''' <remarks>運送会社用EDI送信データMへINSERT</remarks>

    Private Const SQL_INSERT_H_SENDUNSOEDI_M As String = "INSERT INTO $LM_TRN$..H_SENDUNSOEDI_M" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",UNSO_NO_L                    " & vbNewLine _
                                              & ",UNSO_NO_M                    " & vbNewLine _
                                              & ",SEND_NO                      " & vbNewLine _
                                              & ",TRIP_NO                      " & vbNewLine _
                                              & ",GOODS_CD_NRS                 " & vbNewLine _
                                              & ",GOODS_NM                     " & vbNewLine _
                                              & ",COODS_CD_CUST                " & vbNewLine _
                                              & ",UNSO_ONDO_KB                 " & vbNewLine _
                                              & ",UNSO_ONDO_NM                 " & vbNewLine _
                                              & ",ONDO_MX                      " & vbNewLine _
                                              & ",ONDO_MM                      " & vbNewLine _
                                              & ",CUST_ORD_NO_DTL              " & vbNewLine _
                                              & ",BUYER_ORD_NO_DTL             " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",REMARK_UNSO                  " & vbNewLine _
                                              & ",LOT_NO                       " & vbNewLine _
                                              & ",SERIAL_NO                    " & vbNewLine _
                                              & ",IRIME                        " & vbNewLine _
                                              & ",IRIME_UT                     " & vbNewLine _
                                              & ",PKG_UT                       " & vbNewLine _
                                              & ",OUTKA_TTL_NB                 " & vbNewLine _
                                              & ",OUTKA_TTL_QT                 " & vbNewLine _
                                              & ",BETU_WT                      " & vbNewLine _
                                              & ",BIKO1                        " & vbNewLine _
                                              & ",BIKO2                        " & vbNewLine _
                                              & ",BIKO3                        " & vbNewLine _
                                              & ",BIKO4                        " & vbNewLine _
                                              & ",SEND_SHORI_FLG               " & vbNewLine _
                                              & ",SEND_USER                    " & vbNewLine _
                                              & ",SEND_DATE                    " & vbNewLine _
                                              & ",SEND_TIME                    " & vbNewLine _
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
                                              & ",@SEND_NO                     " & vbNewLine _
                                              & ",@TRIP_NO                     " & vbNewLine _
                                              & ",@GOODS_CD_NRS                " & vbNewLine _
                                              & ",@GOODS_NM                    " & vbNewLine _
                                              & ",@COODS_CD_CUST               " & vbNewLine _
                                              & ",@UNSO_ONDO_KB                " & vbNewLine _
                                              & ",@UNSO_ONDO_NM                " & vbNewLine _
                                              & ",@ONDO_MX                     " & vbNewLine _
                                              & ",@ONDO_MM                     " & vbNewLine _
                                              & ",@CUST_ORD_NO_DTL             " & vbNewLine _
                                              & ",@BUYER_ORD_NO_DTL            " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@REMARK_UNSO                 " & vbNewLine _
                                              & ",@LOT_NO                      " & vbNewLine _
                                              & ",@SERIAL_NO                   " & vbNewLine _
                                              & ",@IRIME                       " & vbNewLine _
                                              & ",@IRIME_UT                    " & vbNewLine _
                                              & ",@PKG_UT                      " & vbNewLine _
                                              & ",@OUTKA_TTL_NB                " & vbNewLine _
                                              & ",@OUTKA_TTL_QT                " & vbNewLine _
                                              & ",@BETU_WT                     " & vbNewLine _
                                              & ",@BIKO1                       " & vbNewLine _
                                              & ",@BIKO2                       " & vbNewLine _
                                              & ",@BIKO3                       " & vbNewLine _
                                              & ",@BIKO4                       " & vbNewLine _
                                              & ",'2'                          " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
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

#Region "H_TEHAIINFO_TBL"
    ''' <summary>
    '''  手配情報TBL追加処理
    ''' </summary>
    ''' <remarks>手配情報TBLへINSERT</remarks>

    Private Const SQL_INSERT_H_TEHAIINFO_TBL As String = "INSERT INTO $LM_TRN$..H_TEHAIINFO_TBL" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",UNSO_NO_L                    " & vbNewLine _
                                              & ",TRIP_NO                      " & vbNewLine _
                                              & ",TEHAI_SYUBETSU               " & vbNewLine _
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
                                              & ",@TRIP_NO                     " & vbNewLine _
                                              & ",@TEHAI_SYUBETSU              " & vbNewLine _
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

#End Region

#Region "UPDATE処理 SQL"

    Private Const SQL_UPDATE_H_SENDUNSOEDI_LM As String = "UPDATE $LM_TRN$..H_SENDUNSOEDI_LM SET   " & vbNewLine _
                                              & " SYS_DEL_FLG         = @SYS_DEL_FLG           " & vbNewLine _
                                              & ",SYS_UPD_DATE        = @SYS_UPD_DATE          " & vbNewLine _
                                              & ",SYS_UPD_TIME        = @SYS_UPD_TIME          " & vbNewLine _
                                              & ",SYS_UPD_PGID        = @SYS_UPD_PGID          " & vbNewLine _
                                              & ",SYS_UPD_USER        = @SYS_UPD_USER          " & vbNewLine _
                                              & "WHERE NRS_BR_CD      = @NRS_BR_CD             " & vbNewLine _
                                              & "  AND UNSO_NO_L      = @UNSO_NO_L             " & vbNewLine _
                                              & "  AND UNSO_NO_M      = @UNSO_NO_M             " & vbNewLine _
                                              & "  AND SEND_NO        = @SEND_NO               " & vbNewLine


    Private Const SQL_UPDATE_TEHAIINFO_TBL As String = "UPDATE $LM_TRN$..H_TEHAIINFO_TBL SET       " & vbNewLine _
                                                  & " TEHAI_SYUBETSU      = @TEHAI_SYUBETSU        " & vbNewLine _
                                                  & ",SYS_DEL_FLG         = @SYS_DEL_FLG           " & vbNewLine _
                                                  & ",SYS_UPD_DATE        = @SYS_UPD_DATE          " & vbNewLine _
                                                  & ",SYS_UPD_TIME        = @SYS_UPD_TIME          " & vbNewLine _
                                                  & ",SYS_UPD_PGID        = @SYS_UPD_PGID          " & vbNewLine _
                                                  & ",SYS_UPD_USER        = @SYS_UPD_USER          " & vbNewLine _
                                                  & "WHERE NRS_BR_CD      = @NRS_BR_CD             " & vbNewLine _
                                                  & "  AND UNSO_NO_L      = @UNSO_NO_L             " & vbNewLine _
                                                  & "  AND TRIP_NO        = @TRIP_NO               " & vbNewLine

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
    ''' 手配指示作成対象データ(L+M)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSendUnsoEdiLMInitData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF830IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF830DAC.SQL_SELECT_SENDUNSOEDI_LM)    'SQL構築(データ抽出用SELECT句)
        Me._StrSql.Append(LMF830DAC.SQL_FROM_SENDUNSOEDI_LM)      'SQL構築(データ抽出用FROM句)
        Call Me.SetConditionInputSQL()                           'SQL構築(条件設定)
        'Me._StrSql.Append(LMF830DAC.SQL_GROUP_BY)                'SQL構築(データ抽出用GROUP BY句)
        Me._StrSql.Append(LMF830DAC.SQL_ORDER_BY_L)              'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF830DAC", "SelectSendUnsoEdiLMInitData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("SEND_NO", "SEND_NO")
        map.Add("TRIP_NO", "TRIP_NO")
        map.Add("HENKO_KB", "HENKO_KB")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("WH_ZIP", "WH_ZIP")
        map.Add("WH_AD_1", "WH_AD_1")
        map.Add("WH_AD_2", "WH_AD_2")
        map.Add("WH_AD_3", "WH_AD_3")
        map.Add("WH_TEL", "WH_TEL")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_FAX", "DEST_FAX")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("COA_YN", "COA_YN")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("NHS_REMARK", "NHS_REMARK")
        map.Add("REMARK", "REMARK")
        map.Add("UNSO_ATT", "UNSO_ATT")
        map.Add("DENP_YN", "DENP_YN")
        map.Add("PC_KB", "PC_KB")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("BIN_NM", "BIN_NM")
        map.Add("UNSO_PKG_NB", "UNSO_PKG_NB")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("KYORI", "KYORI")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("BIKO1_HED", "BIKO1_HED")
        map.Add("BIKO2_HED", "BIKO2_HED")
        map.Add("BIKO3_HED", "BIKO3_HED")
        map.Add("BIKO4_HED", "BIKO4_HED")

        'map.Add("SYS_DEL_FLG_L", "SYS_DEL_FLG_L")

        '取得データの格納先をマッピング
        'map.Add("NRS_BR_CD", "NRS_BR_CD")
        'map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        'map.Add("SEND_NO", "SEND_NO")
        'map.Add("TRIP_NO", "TRIP_NO")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("COODS_CD_CUST", "COODS_CD_CUST")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("UNSO_ONDO_NM", "UNSO_ONDO_NM")
        map.Add("ONDO_MX", "ONDO_MX")
        map.Add("ONDO_MM", "ONDO_MM")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("REMARK_OUTKA", "REMARK_OUTKA")
        map.Add("REMARK_UNSO", "REMARK_UNSO")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("OUTKA_TTL_QT", "OUTKA_TTL_QT")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("BIKO1_DTL", "BIKO1_DTL")
        map.Add("BIKO2_DTL", "BIKO2_DTL")
        map.Add("BIKO3_DTL", "BIKO3_DTL")
        map.Add("BIKO4_DTL", "BIKO4_DTL")
        map.Add("NUM1_DTL", "NUM1_DTL")
        map.Add("NUM2_DTL", "NUM2_DTL")
        map.Add("QR_CODE", "QR_CODE")

        'map.Add("SYS_DEL_FLG_M", "SYS_DEL_FLG_M")

        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF830OUT_LM")

        Return ds

    End Function

    ''' <summary>
    ''' 手配指示作成過去対象データ(L+M)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectPreSendUnsoEdiLMInitData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF830IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF830DAC.SQL_SELECT_PRE_SENDUNSOEDI_LM)    'SQL構築(データ抽出用SELECT句)
        Call Me.SetConditionPreInputSQL()                         'SQL構築(条件設定)
        Me._StrSql.Append(LMF830DAC.SQL_PRE_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF830DAC", "SelectPreSendUnsoEdiLMInitData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("SEND_NO", "SEND_NO")
        map.Add("TRIP_NO", "TRIP_NO")
        map.Add("HENKO_KB", "HENKO_KB")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("WH_ZIP", "WH_ZIP")
        map.Add("WH_AD_1", "WH_AD_1")
        map.Add("WH_AD_2", "WH_AD_2")
        map.Add("WH_AD_3", "WH_AD_3")
        map.Add("WH_TEL", "WH_TEL")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_FAX", "DEST_FAX")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("COA_YN", "COA_YN")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("NHS_REMARK", "NHS_REMARK")
        map.Add("REMARK", "REMARK")
        map.Add("UNSO_ATT", "UNSO_ATT")
        map.Add("DENP_YN", "DENP_YN")
        map.Add("PC_KB", "PC_KB")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("BIN_NM", "BIN_NM")
        map.Add("UNSO_PKG_NB", "UNSO_PKG_NB")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("KYORI", "KYORI")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("BIKO1_HED", "BIKO1_HED")
        map.Add("BIKO2_HED", "BIKO2_HED")
        map.Add("BIKO3_HED", "BIKO3_HED")
        map.Add("BIKO4_HED", "BIKO4_HED")

        '取得データの格納先をマッピング
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("COODS_CD_CUST", "COODS_CD_CUST")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("UNSO_ONDO_NM", "UNSO_ONDO_NM")
        map.Add("ONDO_MX", "ONDO_MX")
        map.Add("ONDO_MM", "ONDO_MM")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("REMARK_OUTKA", "REMARK_OUTKA")
        map.Add("REMARK_UNSO", "REMARK_UNSO")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("OUTKA_TTL_QT", "OUTKA_TTL_QT")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("BIKO1_DTL", "BIKO1_DTL")
        map.Add("BIKO2_DTL", "BIKO2_DTL")
        map.Add("BIKO3_DTL", "BIKO3_DTL")
        map.Add("BIKO4_DTL", "BIKO4_DTL")
        map.Add("NUM1_DTL", "NUM1_DTL")
        map.Add("NUM2_DTL", "NUM2_DTL")
        map.Add("QR_CODE", "QR_CODE")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF830OUT_LM_PRE")

        Return ds

    End Function

    ''' <summary>
    ''' 手配情報TBL抽出件数
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectTehaiInfoTblData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF830IN")

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF830DAC.SQL_TEHAIINFO_TBL)    'SQL構築(データ抽出用SELECT句)
        Call Me.SetTehaiInfoParam(Me._Row, Me._SqlPrmList)          'SQL構築(更新値設定)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF830DAC", "SelectTehaiInfoTblData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim tehaiCnt As Integer = 0

        '処理件数の設定
        reader.Read()
        If reader.HasRows = False Then
            MyBase.SetResultCount(tehaiCnt)
        Else
            MyBase.SetResultCount(Convert.ToInt32(reader("TEHAI_CNT")))
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionInputSQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" FLL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '運行番号
            whereStr = .Item("TRIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FLL.TRIP_NO = @TRIP_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", whereStr, DBDataType.CHAR))
            End If

            '運送番号
            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FL.UNSO_NO_L = @UNSO_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", whereStr, DBDataType.CHAR))
            End If

            '2013.12.11 要望番号2063 追加START
            '運送会社EDI用運行番号
            whereStr = .Item("H_UNSOEDI_TRIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H_UNSOEDI_TRIP_NO", whereStr, DBDataType.CHAR))
            End If
            '2013.12.11 要望番号2063 追加START

            ''トランデータより取得の為コメント
            ''便区分
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            ''運送会社コード
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", .Item("UNSOCO_CD").ToString(), DBDataType.NVARCHAR))
            ''運送会社支店コード
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", .Item("UNSOCO_BR_CD").ToString(), DBDataType.NVARCHAR))
            ''運送個数
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_PKG_NB", .Item("UNSO_PKG_NB").ToString(), DBDataType.NUMERIC))
            ''運送重量
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_WT", .Item("UNSO_WT").ToString(), DBDataType.NUMERIC))
            ''確定運賃
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("DECI_UNCHIN").ToString(), DBDataType.NUMERIC))

            '削除フラグ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

            '手配種別
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEHAI_SYUBETSU", .Item("TEHAI_SYUBETSU").ToString(), DBDataType.CHAR))

        End With

    End Sub

    Private Sub SetConditionPreInputSQL()

        'Me._StrSql.Append(" WHERE ")
        'Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HEU.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '運送番号
            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HEU.UNSO_NO_L = @UNSO_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", whereStr, DBDataType.CHAR))
            End If

            '運行番号
            whereStr = .Item("TRIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HEU.TRIP_NO = @TRIP_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", whereStr, DBDataType.CHAR))
            End If

            '営業所コード
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SUB_NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            '運送番号
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SUB_UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            '運行番号
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SUB_TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParameterInputSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '運行番号
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))

            '運送番号
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub


#End Region

#Region "データ抽出処理(項目編集用)"

    ''' <summary>
    ''' 運送会社:エクシング,荷主:ロジスティック用データ抽出
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectXngRojiInitData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF830IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF830DAC.SQL_SELECT_XNG_ROJITYPE)    'SQL構築(データ抽出用SELECT句)
        Call Me.SetParameterInputSQL()                           'SQL構築(条件設定)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF830DAC", "SelectXngRojiInitData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        ''取得データの格納先をマッピング
        map.Add("MAIN_DELI_KB", "MAIN_DELI_KB")
        map.Add("TRIP_NO", "TRIP_NO")
        map.Add("LAW_KBN", "LAW_KBN")
        map.Add("HIN_GROUP", "HIN_GROUP")
        map.Add("HIN_GROUP_NM", "HIN_GROUP_NM")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("DECI_WT", "DECI_WT")
        map.Add("YOKI_WT", "YOKI_WT")
        map.Add("REMARK", "REMARK")
        map.Add("SEIQ_KYORI", "SEIQ_KYORI")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("TSUMIKOMI_JIKAN", "TSUMIKOMI_JIKAN")
        map.Add("TSUMIKOMI_BASYO", "TSUMIKOMI_BASYO")
        map.Add("KIJI_REMARK_1", "KIJI_REMARK_1")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("GROSS_WEIGHT", "GROSS_WEIGHT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF830OUT_XNG_ROJI")

        Return ds

    End Function

#End Region


#Region "追加処理"

#Region "新規追加処理"

    ''' <summary>
    ''' 運送会社用EDI送信データ(LM)追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社用EDI送信データ(LM)を新規追加する</remarks>
    Private Function InsertSendUnsoEdiLMData(ByVal ds As DataSet) As DataSet

        'DataSetのOUT情報を取得
        Dim outTblL As DataTable = ds.Tables("LMF830OUT_LM")

        For i As Integer = 0 To outTblL.Rows.Count - 1

            'INTableの条件rowの格納
            Me._Row = outTblL.Rows(i)

            'SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            '新規登録処理　---------------------------------------------------

            'SQL作成
            Me._StrSql.Append(LMF830DAC.SQL_INSERT_H_SENDUNSOEDI_LM)     'SQL構築(データ抽出用INSERT句)
            Call Me.SetSendUnsoEdiLParam(Me._Row, Me._SqlPrmList)       'SQL構築(更新値設定)
            Call Me.SetSendUnsoEdiMParam(Me._Row, Me._SqlPrmList)       'SQL構築(更新値設定)
            Call Me.SetDataInsertParameter(Me._SqlPrmList)              'SQL構築(システム項目設定)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF830DAC", "InsertSendUnsoEdiLMData", cmd)

            'SQLの発行
            Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()
            reader.Close()

        Next

        Return ds

    End Function

    '2013.12.11 要望番号2063 追加START
    ''' <summary>
    ''' 運送会社用EDI送信データ(LM)追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社用EDI送信データ(LM)を新規追加する</remarks>
    Private Function InsertDelFlgSendUnsoEdiLMData(ByVal ds As DataSet) As DataSet

        'DataSetのOUT情報を取得
        Dim outTblL As DataTable = ds.Tables("LMF830OUT_LM_PRE")

        For i As Integer = 0 To outTblL.Rows.Count - 1

            'INTableの条件rowの格納
            Me._Row = outTblL.Rows(i)

            '削除用パラメータに変更
            Me._Row.Item("SEND_NO") = Convert.ToInt64(Me._Row.Item("SEND_NO").ToString()) + 1
            Me._Row.Item("SYS_DEL_FLG") = "1"

            'SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            '新規登録処理　---------------------------------------------------

            'SQL作成
            Me._StrSql.Append(LMF830DAC.SQL_INSERT_H_SENDUNSOEDI_LM)     'SQL構築(データ抽出用INSERT句)
            Call Me.SetSendUnsoEdiLParam(Me._Row, Me._SqlPrmList)       'SQL構築(更新値設定)
            Call Me.SetSendUnsoEdiMParam(Me._Row, Me._SqlPrmList)       'SQL構築(更新値設定)
            Call Me.SetDataInsertParameter(Me._SqlPrmList)              'SQL構築(システム項目設定)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF830DAC", "InsertDelFlgSendUnsoEdiLMData", cmd)

            'SQLの発行
            Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()
            reader.Close()

        Next

        Return ds

    End Function
    '2013.12.11 要望番号2063 追加END

    ''' <summary>
    ''' 運送会社用EDI送信データ(L)追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社用EDI送信データ(L)を新規追加する</remarks>
    Private Function InsertSendUnsoEdiLData(ByVal ds As DataSet) As DataSet

        'DataSetのOUT情報を取得
        Dim outTblL As DataTable = ds.Tables("LMF830OUT_L")

        For i As Integer = 0 To outTblL.Rows.Count - 1

            'INTableの条件rowの格納
            Me._Row = outTblL.Rows(i)

            'SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            '新規登録処理　---------------------------------------------------

            'SQL作成
            Me._StrSql.Append(LMF830DAC.SQL_INSERT_H_SENDUNSOEDI_L)     'SQL構築(データ抽出用INSERT句)
            Call Me.SetSendUnsoEdiLParam(Me._Row, Me._SqlPrmList)       'SQL構築(更新値設定)
            Call Me.SetDataInsertParameter(Me._SqlPrmList)              'SQL構築(システム項目設定)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF830DAC", "InsertSendUnsoEdiLData", cmd)

            'SQLの発行
            Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()
            reader.Close()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運送会社用EDI送信データ(M)追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社用EDI送信データ(M)を新規追加する</remarks>
    Private Function InsertSendUnsoEdiMData(ByVal ds As DataSet) As DataSet

        'DataSetのOUT情報を取得
        Dim outTblM As DataTable = ds.Tables("LMF830OUT_M")

        For j As Integer = 0 To outTblM.Rows.Count - 1

            'INTableの条件rowの格納
            Me._Row = outTblM.Rows(j)

            'SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            '新規登録処理　---------------------------------------------------

            'SQL作成
            Me._StrSql.Append(LMF830DAC.SQL_INSERT_H_SENDUNSOEDI_M)     'SQL構築(データ抽出用INSERT句)
            Call Me.SetSendUnsoEdiMParam(Me._Row, Me._SqlPrmList)       'SQL構築(更新値設定)
            Call Me.SetDataInsertParameter(Me._SqlPrmList)              'SQL構築(システム項目設定)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF830DAC", "InsertSendUnsoEdiMData", cmd)

            'SQLの発行
            Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()
            reader.Close()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 手配情報TBL追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>手配情報TBLを新規追加する</remarks>
    Private Function InsertTehaiInfoTblData(ByVal ds As DataSet) As DataSet

        'DataSetのOUT情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF830IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化/設定
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '新規登録処理　---------------------------------------------------

        'SQL作成
        Me._StrSql.Append(LMF830DAC.SQL_INSERT_H_TEHAIINFO_TBL)     'SQL構築(データ抽出用INSERT句)
        Call Me.SetTehaiInfoParam(Me._Row, Me._SqlPrmList)          'SQL構築(更新値設定)
        Call Me.SetDataInsertParameter(Me._SqlPrmList)              'SQL構築(システム項目設定)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF830DAC", "InsertTehaiInfoTblData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(運送会社用EDI送信データ(L))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSendUnsoEdiLParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))                   '営業所コード
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))                   '運送番号L
            prmList.Add(MyBase.GetSqlParameter("@SEND_NO", .Item("SEND_NO").ToString(), DBDataType.CHAR))                   '伝送№
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))                   '運行番号
            prmList.Add(MyBase.GetSqlParameter("@HENKO_KB", .Item("HENKO_KB").ToString(), DBDataType.CHAR))                   '変更区分
            prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))                   '元データ区分
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))                   '運送会社コード
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))                   '運送会社支店コード
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))                   '倉庫(保管場所)コード
            prmList.Add(MyBase.GetSqlParameter("@WH_NM", .Item("WH_NM").ToString(), DBDataType.NVARCHAR))                   '倉庫(保管場所)名
            prmList.Add(MyBase.GetSqlParameter("@WH_ZIP", .Item("WH_ZIP").ToString(), DBDataType.NVARCHAR))                   '倉庫(保管場所)郵便番号
            prmList.Add(MyBase.GetSqlParameter("@WH_AD_1", .Item("WH_AD_1").ToString(), DBDataType.NVARCHAR))                   '倉庫(保管場所)住所1
            prmList.Add(MyBase.GetSqlParameter("@WH_AD_2", .Item("WH_AD_2").ToString(), DBDataType.NVARCHAR))                   '倉庫(保管場所)住所2
            prmList.Add(MyBase.GetSqlParameter("@WH_AD_3", .Item("WH_AD_3").ToString(), DBDataType.NVARCHAR))                   '倉庫(保管場所)住所3
            prmList.Add(MyBase.GetSqlParameter("@WH_TEL", .Item("WH_TEL").ToString(), DBDataType.NVARCHAR))                   '倉庫(保管場所)電話番号
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))                   '出荷日
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))                   '納入予定日
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))                   '納入予定時刻　名称
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))                   '荷主コード（大）
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))                   '荷主コード（中）
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", .Item("CUST_NM_L").ToString(), DBDataType.NVARCHAR))                   '荷主名（大）
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", .Item("CUST_NM_M").ToString(), DBDataType.NVARCHAR))                   '荷主名（中）
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NM_L", .Item("SHIP_NM_L").ToString(), DBDataType.NVARCHAR))                   '荷送人名
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))                   '届先コード
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))                   '届先名称
            prmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", .Item("DEST_ZIP").ToString(), DBDataType.NVARCHAR))                   '届先郵便番号
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", .Item("DEST_AD_1").ToString(), DBDataType.NVARCHAR))                   '届先住所1
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", .Item("DEST_AD_2").ToString(), DBDataType.NVARCHAR))                   '届先住所2
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", .Item("DEST_AD_3").ToString(), DBDataType.NVARCHAR))                   '届先住所3
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", .Item("DEST_TEL").ToString(), DBDataType.NVARCHAR))                   '届先電話番号
            prmList.Add(MyBase.GetSqlParameter("@DEST_FAX", .Item("DEST_FAX").ToString(), DBDataType.NVARCHAR))                   '届先FAX
            prmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", .Item("DEST_JIS_CD").ToString(), DBDataType.NVARCHAR))                   '届先JISコード
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", .Item("SP_NHS_KB").ToString(), DBDataType.CHAR))                   '指定納品書区分
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))                   '分析表添付区分
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", .Item("CUST_ORD_NO").ToString(), DBDataType.NVARCHAR))                   '荷主注文番号（全体）
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", .Item("BUYER_ORD_NO").ToString(), DBDataType.NVARCHAR))                   '買主注文番号（全体）
            prmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", .Item("NHS_REMARK").ToString(), DBDataType.NVARCHAR))                   '納品書摘要
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))                   '出荷時注意事項
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ATT", .Item("UNSO_ATT").ToString(), DBDataType.NVARCHAR))                   '配送時注意事項
            prmList.Add(MyBase.GetSqlParameter("@DENP_YN", .Item("DENP_YN").ToString(), DBDataType.CHAR))                   '送り状作成有無
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", .Item("PC_KB").ToString(), DBDataType.CHAR))                   '元着払区分
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))                   '便区分
            prmList.Add(MyBase.GetSqlParameter("@BIN_NM", .Item("BIN_NM").ToString(), DBDataType.NVARCHAR))                   '便名
            prmList.Add(MyBase.GetSqlParameter("@UNSO_PKG_NB", .Item("UNSO_PKG_NB").ToString(), DBDataType.NUMERIC))                   '運送梱包個数
            prmList.Add(MyBase.GetSqlParameter("@UNSO_WT", .Item("UNSO_WT").ToString(), DBDataType.NUMERIC))                   '運送重量
            prmList.Add(MyBase.GetSqlParameter("@KYORI", .Item("KYORI").ToString(), DBDataType.NUMERIC))                   '距離（㎞）
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("DECI_UNCHIN").ToString(), DBDataType.NUMERIC))                   '確定請求運賃
            prmList.Add(MyBase.GetSqlParameter("@BIKO1_HED", .Item("BIKO1_HED").ToString(), DBDataType.NVARCHAR))                   '備考1
            prmList.Add(MyBase.GetSqlParameter("@BIKO2_HED", .Item("BIKO2_HED").ToString(), DBDataType.NVARCHAR))                   '備考2
            prmList.Add(MyBase.GetSqlParameter("@BIKO3_HED", .Item("BIKO3_HED").ToString(), DBDataType.NVARCHAR))                   '備考3
            prmList.Add(MyBase.GetSqlParameter("@BIKO4_HED", .Item("BIKO4_HED").ToString(), DBDataType.NVARCHAR))                   '備考4

            'prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG_L").ToString(), DBDataType.CHAR))         '削除フラグ

        End With

    End Sub


    ''' <summary>
    ''' パラメータ設定モジュール(運送会社用EDI送信データ(M))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSendUnsoEdiMParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            'prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))                      '営業所コード
            'prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))                      '運送番号L
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))                      '運送番号M
            'prmList.Add(MyBase.GetSqlParameter("@SEND_NO", .Item("SEND_NO").ToString(), DBDataType.CHAR))                          '伝送№
            'prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))                          '運行番号
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))                '日陸商品CD
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))                    '商品名
            prmList.Add(MyBase.GetSqlParameter("@COODS_CD_CUST", .Item("COODS_CD_CUST").ToString(), DBDataType.NVARCHAR))          '荷主商品CD
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))                '運送時温度区分
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_NM", .Item("UNSO_ONDO_NM").ToString(), DBDataType.NVARCHAR))            '運送時温度名称
            prmList.Add(MyBase.GetSqlParameter("@ONDO_MX", .Item("ONDO_MX").ToString(), DBDataType.NUMERIC))                       '温度上限
            prmList.Add(MyBase.GetSqlParameter("@ONDO_MM", .Item("ONDO_MM").ToString(), DBDataType.NUMERIC))                       '温度下限
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))      '荷主注文番号（明細単位）
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))    '買主注文番号（明細単位）
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUTKA", .Item("REMARK_OUTKA").ToString(), DBDataType.NVARCHAR))                        '備考（社内）
            prmList.Add(MyBase.GetSqlParameter("@REMARK_UNSO", .Item("REMARK_UNSO").ToString(), DBDataType.NVARCHAR))              '備考（運送）
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))                        'ロット№
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))                  'シリアル№
            prmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))                           '入目
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))                    '入目単位
            prmList.Add(MyBase.GetSqlParameter("@PKG_UT", .Item("PKG_UT").ToString(), DBDataType.CHAR))                            '荷姿CD
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", .Item("OUTKA_TTL_NB").ToString(), DBDataType.NUMERIC))             '出荷個数
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", .Item("OUTKA_TTL_QT").ToString(), DBDataType.NUMERIC))             '出荷数量
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", .Item("BETU_WT").ToString(), DBDataType.NUMERIC))                       '個別重量
            prmList.Add(MyBase.GetSqlParameter("@BIKO1_DTL", .Item("BIKO1_DTL").ToString(), DBDataType.NVARCHAR))                          '備考1
            prmList.Add(MyBase.GetSqlParameter("@BIKO2_DTL", .Item("BIKO2_DTL").ToString(), DBDataType.NVARCHAR))                          '備考2
            prmList.Add(MyBase.GetSqlParameter("@BIKO3_DTL", .Item("BIKO3_DTL").ToString(), DBDataType.NVARCHAR))                          '備考3
            prmList.Add(MyBase.GetSqlParameter("@BIKO4_DTL", .Item("BIKO4_DTL").ToString(), DBDataType.NVARCHAR))                          '備考4

            prmList.Add(MyBase.GetSqlParameter("@NUM1_DTL", .Item("NUM1_DTL").ToString(), DBDataType.NUMERIC))                     '数値備考1
            prmList.Add(MyBase.GetSqlParameter("@NUM2_DTL", .Item("NUM2_DTL").ToString(), DBDataType.NUMERIC))                     '数値備考2
            prmList.Add(MyBase.GetSqlParameter("@QR_CODE", .Item("QR_CODE").ToString(), DBDataType.NVARCHAR))                       'QRコード

            'prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG_M").ToString(), DBDataType.CHAR))                '削除フラグ
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))                '削除フラグ

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(手配情報TBL)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTehaiInfoParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))                   '営業所コード
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))                   '運送番号L
            '2013.12.11 要望番号2063 修正START
            'prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("H_UNSOEDI_TRIP_NO").ToString(), DBDataType.CHAR))             '運送会社EDI用運行番号
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))                       '運行番号
            '2013.12.11 要望番号2063 修正END
            prmList.Add(MyBase.GetSqlParameter("@TEHAI_SYUBETSU", .Item("TEHAI_SYUBETSU").ToString(), DBDataType.CHAR))         '手配種別
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))         '削除フラグ


        End With

    End Sub

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetDataInsertParameter(ByVal prmList As ArrayList) As String()

        '更新日時
        Dim sysDateTime As String() = Me.SetInsSysdataParameter(prmList)

        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", sysDateTime(0), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", sysDateTime(1), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.VARCHAR))
        'prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        Return sysDateTime

    End Function

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetInsSysdataParameter(ByVal prmList As ArrayList) As String()

        '更新日時
        Dim sysDateTime As String() = New String() {MyBase.GetSystemDate(), MyBase.GetSystemTime()}

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.VARCHAR))

        Return sysDateTime

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(運送会社用EDI送信(L+M)過去分)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataUpdatePreParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))                   '営業所コード
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))                   '運送番号L
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))                   '運送番号M
            prmList.Add(MyBase.GetSqlParameter("@SEND_NO", .Item("SEND_NO").ToString(), DBDataType.CHAR))                       '伝送№
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", "1", DBDataType.CHAR))                                           '削除フラグ

        End With

    End Sub

#End Region

#End Region

#Region "更新処理"

    ''' <summary>
    ''' 運送会社用EDI送信のデータ(L+M)過去分の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdatePreSendUnsoEdiLMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF830OUT_LM_PRE")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF830DAC.SQL_UPDATE_H_SENDUNSOEDI_LM)         'SQL構築(UPDATE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetDataUpdatePreParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetDataInsertParameter(Me._SqlPrmList)              'SQL構築(システム項目設定)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF830DAC", "UpdatePreSendUnsoEdiLMData", cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = False Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 手配情報テーブルの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateTehaiInfoTblData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        '2013.12.11 要望番号2063 修正START
        Dim inTbl As DataTable = ds.Tables("LMF830IN")
        'Dim inTbl As DataTable = ds.Tables("LMF830OUT_LM")
        '2013.12.11 要望番号2063 修正END

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF830DAC.SQL_UPDATE_TEHAIINFO_TBL)         'SQL構築(UPDATE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetTehaiInfoParam(Me._Row, Me._SqlPrmList)
        Call Me.SetDataInsertParameter(Me._SqlPrmList)              'SQL構築(システム項目設定)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF830DAC", "UpdateTehaiInfoTblData", cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = False Then
            Return ds
        End If

        Return ds

    End Function

#End Region

#Region "設定処理"

#Region "SQL"

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

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd))

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer) As Boolean

        '判定
        If cnt < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

End Class
