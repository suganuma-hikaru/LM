' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : 商品
'  プログラムID     :  LMM510    : 商品マスタ一覧表
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM510DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM510DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                           " & vbNewLine _
                                            & "	MG.NRS_BR_CD                                           AS NRS_BR_CD    " & vbNewLine _
                                            & ",'67'                                                     AS PTN_ID       " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                         " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                        " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD       " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                         " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                    " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID       " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                                   " & vbNewLine _
                                            & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                        " & vbNewLine _
                                            & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                        " & vbNewLine _
                                            & "      ELSE MR3.RPT_ID                                                                    " & vbNewLine _
                                            & " END              AS RPT_ID                                                              " & vbNewLine _
                                            & ",MG.SYS_DEL_FLG              AS SYS_DEL_FLG                                              " & vbNewLine _
                                            & ",MG.NRS_BR_CD                AS NRS_BR_CD                                                " & vbNewLine _
                                            & ",MG.CUST_CD_L                AS CUST_CD_L                                                " & vbNewLine _
                                            & ",MG.CUST_CD_M                AS CUST_CD_M                                                " & vbNewLine _
                                            & ",MG.CUST_CD_S                AS CUST_CD_S                                                " & vbNewLine _
                                            & ",MG.CUST_CD_SS               AS CUST_CD_SS                                               " & vbNewLine _
                                            & ",MC.CUST_NM_L                AS CUST_NM_L                                                " & vbNewLine _
                                            & ",MC.CUST_NM_M                AS CUST_NM_M                                                " & vbNewLine _
                                            & ",MC.CUST_NM_S                AS CUST_NM_S                                                " & vbNewLine _
                                            & ",MC.CUST_NM_SS               AS CUST_NM_SS                                               " & vbNewLine _
                                            & ",MC.OYA_SEIQTO_CD            AS SEIQTO_CD                                                " & vbNewLine _
                                            & ",MSEIQTO.SEIQTO_NM           AS SEIQTO_NM                                                " & vbNewLine _
                                            & ",MSEIQTO.SEIQTO_BUSYO_NM     AS SEIQTO_BUSYO_NM                                          " & vbNewLine _
                                            & ",MG.GOODS_CD_NRS             AS GOODS_CD_NRS                                             " & vbNewLine _
                                            & ",MG.GOODS_CD_CUST            AS GOODS_CD_CUST                                            " & vbNewLine _
                                            & ",MG.SEARCH_KEY_1             AS SEARCH_KEY_1                                             " & vbNewLine _
                                            & ",MG.SEARCH_KEY_2             AS SEARCH_KEY_2                                             " & vbNewLine _
                                            & ",MG.CUST_COST_CD1            AS CUST_COST_CD1                                            " & vbNewLine _
                                            & ",MG.CUST_COST_CD2            AS CUST_COST_CD2                                            " & vbNewLine _
                                            & ",MG.JAN_CD                   AS JAN_CD                                                   " & vbNewLine _
                                            & ",MG.GOODS_NM_1               AS GOODS_NM_1                                               " & vbNewLine _
                                            & ",MG.GOODS_NM_2               AS GOODS_NM_2                                               " & vbNewLine _
                                            & ",MG.GOODS_NM_3               AS GOODS_NM_3                                               " & vbNewLine _
                                            & ",MG.UP_GP_CD_1               AS UP_GP_CD_1                                               " & vbNewLine _
                                            & ",DAYTBL.STR_DATE             AS STR_DATE                                                 " & vbNewLine _
                                            & ",MTANKA.STORAGE_1            AS STORAGE_1                                                " & vbNewLine _
                                            & ",MTANKA.STORAGE_KB1          AS STORAGE_KB1                                              " & vbNewLine _
                                            & ",KBN1.KBN_NM1                AS STORAGE_KB1_NM                                           " & vbNewLine _
                                            & ",MTANKA.STORAGE_2            AS STORAGE_2                                                " & vbNewLine _
                                            & ",MTANKA.STORAGE_KB2          AS STORAGE_KB2                                              " & vbNewLine _
                                            & ",KBN2.KBN_NM1                AS STORAGE_KB2_NM                                           " & vbNewLine _
                                            & ",MTANKA.HANDLING_IN          AS HANDLING_IN                                              " & vbNewLine _
                                            & ",MTANKA.HANDLING_IN_KB       AS HANDLING_IN_KB                                           " & vbNewLine _
                                            & ",KBN3.KBN_NM1                AS HANDLING_IN_KB_NM                                        " & vbNewLine _
                                            & ",MTANKA.HANDLING_OUT         AS HANDLING_OUT                                             " & vbNewLine _
                                            & ",MTANKA.HANDLING_OUT_KB      AS HANDLING_OUT_KB                                          " & vbNewLine _
                                            & ",KBN4.KBN_NM1                AS HANDLING_OUT_KB_NM                                       " & vbNewLine _
                                            & ",MTANKA.MINI_TEKI_IN_AMO     AS MINI_TEKI_IN_AMO                                         " & vbNewLine _
                                            & ",MG.SHOBO_CD                 AS SHOBO_CD                                                 " & vbNewLine _
                                            & ",MSHOBO.RUI                  AS RUI                                                      " & vbNewLine _
                                            & ",KBN5.KBN_NM1                AS RUI_NM                                                   " & vbNewLine _
                                            & ",MSHOBO.HINMEI               AS HINMEI                                                   " & vbNewLine _
                                            & ",MSHOBO.SEISITSU             AS SEISITSU                                                 " & vbNewLine _
                                            & ",MSHOBO.SYU                  AS SYU                                                      " & vbNewLine _
                                            & ",KBN6.KBN_NM1                AS SYU_NM                                                   " & vbNewLine _
                                            & ",MG.KIKEN_KB                 AS KIKEN_KB                                                 " & vbNewLine _
                                            & ",KBN7.KBN_NM1                AS KIKEN_KB_NM                                              " & vbNewLine _
                                            & ",MG.UN                       AS UN                                                       " & vbNewLine _
                                            & ",MG.PG_KB                    AS PG_KB                                                    " & vbNewLine _
                                            & ",KBN8.KBN_NM1                AS PG_KB_NM                                                 " & vbNewLine _
                                            & ",MG.CLASS_1                  AS CLASS_1                                                  " & vbNewLine _
                                            & ",MG.CLASS_2                  AS CLASS_2                                                  " & vbNewLine _
                                            & ",MG.CLASS_3                  AS CLASS_3                                                  " & vbNewLine _
                                            & ",MG.CHEM_MTRL_KB             AS CHEM_MTRL_KB                                             " & vbNewLine _
                                            & ",KBN9.KBN_NM1                AS CHEM_MTRL_KB_NM                                          " & vbNewLine _
                                            & ",MG.DOKU_KB                  AS DOKU_KB                                                  " & vbNewLine _
                                            & ",KBN10.KBN_NM1               AS DOKU_KB_NM                                               " & vbNewLine _
                                            & ",MG.GAS_KANRI_KB             AS GAS_KANRI_KB                                             " & vbNewLine _
                                            & ",KBN11.KBN_NM1               AS GAS_KANRI_KB_NM                                          " & vbNewLine _
                                            & ",MG.ONDO_KB                  AS ONDO_KB                                                  " & vbNewLine _
                                            & ",KBN12.KBN_NM1               AS ONDO_KB_NM                                               " & vbNewLine _
                                            & ",MG.UNSO_ONDO_KB             AS UNSO_ONDO_KB                                             " & vbNewLine _
                                            & ",KBN13.KBN_NM1               AS UNSO_ONDO_KB_NM                                          " & vbNewLine _
                                            & ",MG.ONDO_MX                  AS ONDO_MX                                                  " & vbNewLine _
                                            & ",MG.ONDO_MM                  AS ONDO_MM                                                  " & vbNewLine _
                                            & ",MG.ONDO_STR_DATE            AS ONDO_STR_DATE                                            " & vbNewLine _
                                            & ",MG.ONDO_END_DATE            AS ONDO_END_DATE                                            " & vbNewLine _
                                            & ",MG.ONDO_UNSO_STR_DATE       AS ONDO_UNSO_STR_DATE                                       " & vbNewLine _
                                            & ",MG.ONDO_UNSO_END_DATE       AS ONDO_UNSO_END_DATE                                       " & vbNewLine _
                                            & ",MG.KYOKAI_GOODS_KB          AS KYOKAI_GOODS_KB                                          " & vbNewLine _
                                            & ",KBN14.KBN_NM1               AS KYOKAI_GOODS_KB_NM                                       " & vbNewLine _
                                            & ",MG.ALCTD_KB                 AS ALCTD_KB                                                 " & vbNewLine _
                                            & ",KBN15.KBN_NM1               AS ALCTD_KB_NM                                              " & vbNewLine _
                                            & ",MG.NB_UT                    AS NB_UT                                                    " & vbNewLine _
                                            & ",KBN16.KBN_NM1               AS NB_UT_NM                                                 " & vbNewLine _
                                            & ",MG.PKG_NB                   AS PKG_NB                                                   " & vbNewLine _
                                            & ",MG.PKG_UT                   AS PKG_UT                                                   " & vbNewLine _
                                            & ",KBN17.KBN_NM1               AS PKG_UT_NM                                                " & vbNewLine _
                                            & ",MG.PLT_PER_PKG_UT           AS PLT_PER_PKG_UT                                           " & vbNewLine _
                                            & ",MG.STD_IRIME_NB             AS STD_IRIME_NB                                             " & vbNewLine _
                                            & ",MG.STD_IRIME_UT             AS STD_IRIME_UT                                             " & vbNewLine _
                                            & ",KBN18.KBN_NM1               AS STD_IRIME_UT_NM                                          " & vbNewLine _
                                            & ",MG.STD_WT_KGS               AS STD_WT_KGS                                               " & vbNewLine _
                                            & ",MG.STD_CBM                  AS STD_CBM                                                  " & vbNewLine _
                                            & ",KBN19.VALUE1                AS NT_GR_CONV_RATE                                          " & vbNewLine _
                                            & ",MG.TARE_YN                  AS TARE_YN                                                  " & vbNewLine _
                                            & ",KBN20.KBN_NM1               AS TARE_YN_NM                                               " & vbNewLine _
                                            & ",MG.DEF_SPD_KB               AS DEF_SPD_KB                                               " & vbNewLine _
                                            & ",KBN21.KBN_NM1               AS DEF_SPD_KB_NM                                            " & vbNewLine _
                                            & ",MG.KITAKU_AM_UT_KB          AS KITAKU_AM_UT_KB                                          " & vbNewLine _
                                            & ",KBN22.KBN_NM1               AS KITAKU_AM_UT_KB_NM                                       " & vbNewLine _
                                            & ",MG.KITAKU_GOODS_UP          AS KITAKU_GOODS_UP                                          " & vbNewLine _
                                            & ",MG.ORDER_KB                 AS ORDER_KB                                                 " & vbNewLine _
                                            & ",KBN23.KBN_NM1               AS ORDER_KB_NM                                              " & vbNewLine _
                                            & ",MG.ORDER_NB                 AS ORDER_NB                                                 " & vbNewLine _
                                            & ",MG.PRINT_NB                 AS PRINT_NB                                                 " & vbNewLine _
                                            & ",MG.SP_NHS_YN                AS SP_NHS_YN                                                " & vbNewLine _
                                            & ",KBN24.KBN_NM1               AS SP_NHS_YN_NM                                             " & vbNewLine _
                                            & ",MG.COA_YN                   AS COA_YN                                                   " & vbNewLine _
                                            & ",KBN25.KBN_NM1               AS COA_YN_NM                                                " & vbNewLine _
                                            & ",MG.LOT_CTL_KB               AS LOT_CTL_KB                                               " & vbNewLine _
                                            & ",KBN26.KBN_NM1               AS LOT_CTL_KB_NM                                            " & vbNewLine _
                                            & ",MG.LT_DATE_CTL_KB           AS LT_DATE_CTL_KB                                           " & vbNewLine _
                                            & ",KBN27.KBN_NM1               AS LT_DATE_CTL_KB_NM                                        " & vbNewLine _
                                            & ",MG.CRT_DATE_CTL_KB          AS CRT_DATE_CTL_KB                                          " & vbNewLine _
                                            & ",KBN28.KBN_NM1               AS CRT_DATE_CTL_KB_NM                                       " & vbNewLine _
                                            & ",MG.SKYU_MEI_YN              AS SKYU_MEI_YN                                              " & vbNewLine _
                                            & ",KBN29.KBN_NM1               AS SKYU_MEI_YN_NM                                           " & vbNewLine _
                                            & ",MG.HIKIATE_ALERT_YN         AS HIKIATE_ALERT_YN                                         " & vbNewLine _
                                            & ",KBN30.KBN_NM1               AS HIKIATE_ALERT_YN_NM                                      " & vbNewLine _
                                            & ",MG.SHIP_CD_L                AS SHIP_CD_L                                                " & vbNewLine _
                                            & ",MG.INKA_KAKO_SAGYO_KB_1     AS INKA_KAKO_SAGYO_KB_1                                     " & vbNewLine _
                                            & ",MG.INKA_KAKO_SAGYO_KB_2     AS INKA_KAKO_SAGYO_KB_2                                     " & vbNewLine _
                                            & ",MG.INKA_KAKO_SAGYO_KB_3     AS INKA_KAKO_SAGYO_KB_3                                     " & vbNewLine _
                                            & ",MG.INKA_KAKO_SAGYO_KB_4     AS INKA_KAKO_SAGYO_KB_4                                     " & vbNewLine _
                                            & ",MG.INKA_KAKO_SAGYO_KB_5     AS INKA_KAKO_SAGYO_KB_5                                     " & vbNewLine _
                                            & ",MG.OUTKA_KAKO_SAGYO_KB_1    AS OUTKA_KAKO_SAGYO_KB_1                                    " & vbNewLine _
                                            & ",MG.OUTKA_KAKO_SAGYO_KB_2    AS OUTKA_KAKO_SAGYO_KB_2                                    " & vbNewLine _
                                            & ",MG.OUTKA_KAKO_SAGYO_KB_3    AS OUTKA_KAKO_SAGYO_KB_3                                    " & vbNewLine _
                                            & ",MG.OUTKA_KAKO_SAGYO_KB_4    AS OUTKA_KAKO_SAGYO_KB_4                                    " & vbNewLine _
                                            & ",MG.OUTKA_KAKO_SAGYO_KB_5    AS OUTKA_KAKO_SAGYO_KB_5                                    " & vbNewLine _
                                            & ",MSAGYO1.SAGYO_NM            AS INKA_KAKO_SAGYO_NM_1                                     " & vbNewLine _
                                            & ",MSAGYO2.SAGYO_NM            AS INKA_KAKO_SAGYO_NM_2                                     " & vbNewLine _
                                            & ",MSAGYO3.SAGYO_NM            AS INKA_KAKO_SAGYO_NM_3                                     " & vbNewLine _
                                            & ",MSAGYO4.SAGYO_NM            AS INKA_KAKO_SAGYO_NM_4                                     " & vbNewLine _
                                            & ",MSAGYO5.SAGYO_NM            AS INKA_KAKO_SAGYO_NM_5                                     " & vbNewLine _
                                            & ",MSAGYO6.SAGYO_NM            AS OUTKA_KAKO_SAGYO_NM_1                                    " & vbNewLine _
                                            & ",MSAGYO7.SAGYO_NM            AS OUTKA_KAKO_SAGYO_NM_2                                    " & vbNewLine _
                                            & ",MSAGYO8.SAGYO_NM            AS OUTKA_KAKO_SAGYO_NM_3                                    " & vbNewLine _
                                            & ",MSAGYO9.SAGYO_NM            AS OUTKA_KAKO_SAGYO_NM_4                                    " & vbNewLine _
                                            & ",MSAGYO10.SAGYO_NM           AS OUTKA_KAKO_SAGYO_NM_5                                    " & vbNewLine _
                                            & ",MG.PKG_SAGYO                AS PKG_SAGYO                                                " & vbNewLine _
                                            & ",MSAGYO11.SAGYO_NM           AS PKG_SAGYO_NM                                             " & vbNewLine _
                                            & ",MG.OUTKA_ATT                AS OUTKA_ATT                                                " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_01                                             " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_02                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_03                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_04                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_05                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_06                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_07                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_08                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_09                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_10                                              " & vbNewLine _
                                            & ",USER1.USER_NM               AS SYS_ENT_USER                                             " & vbNewLine _
                                            & ",MG.SYS_ENT_DATE             AS SYS_ENT_DATE                                             " & vbNewLine _
                                            & ",MG.SYS_ENT_TIME             AS SYS_ENT_TIME                                             " & vbNewLine _
                                            & ",USER2.USER_NM               AS SYS_UPD_USER                                             " & vbNewLine _
                                            & ",MG.SYS_UPD_DATE             AS SYS_UPD_DATE                                             " & vbNewLine _
                                            & ",MG.SYS_UPD_TIME             AS SYS_UPD_TIME                                             " & vbNewLine _
                                            & ",KBN31.KBN_NM1               AS SIZE_KB                                                  " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_TAITLE_01                                             " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_TAITLE_02                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_TAITLE_03                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_TAITLE_04                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_TAITLE_05                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_TAITLE_06                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_TAITLE_07                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_TAITLE_08                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_TAITLE_09                                               " & vbNewLine _
                                            & ",''                          AS SET_NAIYO_TAITLE_10                                              " & vbNewLine _
                                            & ",MG.WIDTH                    AS WIDTH                                                    " & vbNewLine _
                                            & ",MG.HEIGHT                   AS HEIGHT                                                   " & vbNewLine _
                                            & ",MG.DEPTH                    AS DEPTH                                                    " & vbNewLine _
                                            & ",MG.VOLUME                   AS VOLUME                                                   " & vbNewLine _
                                            & ",MG.OCCUPY_VOLUME            AS OCCUPY_VOLUME                                            " & vbNewLine

    ''' <summary>
    ''' GOODS_DETAILSデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DETAILS As String = "SELECT                                                                                " & vbNewLine _
                                            & " MGD.NRS_BR_CD                   AS NRS_BR_CD                                            " & vbNewLine _
                                            & ",MGD.GOODS_CD_NRS                AS GOODS_CD_NRS                                         " & vbNewLine _
                                            & ",MGD.GOODS_CD_NRS_EDA            AS GOODS_CD_NRS_EDA                                     " & vbNewLine _
                                            & ",KBN1.KBN_NM1                    AS SUB_KB                                               " & vbNewLine _
                                            & ",MGD.SET_NAIYO                   AS SET_NAIYO                                            " & vbNewLine _
                                            & " FROM $LM_MST$..M_GOODS_DETAILS  AS MGD                                                  " & vbNewLine _
                                            & "--用途区分名                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN AS KBN1                                                        " & vbNewLine _
                                            & "ON  MGD.SUB_KB = KBN1.KBN_CD                                                             " & vbNewLine _
                                            & "AND KBN1.KBN_GROUP_CD = 'Y007'                                                           " & vbNewLine _
                                            & "AND KBN1.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                            & " WHERE MGD.SYS_DEL_FLG = '0'                                                             " & vbNewLine





    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM                                                                                 " & vbNewLine _
                                     & "--商品M                                                                              " & vbNewLine _
                                     & "          $LM_MST$..M_GOODS AS MG                                                    " & vbNewLine _
                                     & "--荷主名                                                                             " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST AS MC                                                     " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MC.NRS_BR_CD                                                      " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MC.CUST_CD_L                                                      " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MC.CUST_CD_M                                                      " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MC.CUST_CD_S                                                      " & vbNewLine _
                                     & "AND MG.CUST_CD_SS = MC.CUST_CD_SS                                                    " & vbNewLine _
                                     & "AND MC.SYS_DEL_FLG = '0'                                                             " & vbNewLine _
                                     & "--請求先会社名                                                                       " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SEIQTO AS MSEIQTO                                              " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MSEIQTO.NRS_BR_CD                                                 " & vbNewLine _
                                     & "AND MC.OYA_SEIQTO_CD = MSEIQTO.SEIQTO_CD                                             " & vbNewLine _
                                     & "AND MSEIQTO.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
                                     & "--適応開始日                                                                         " & vbNewLine _
                                     & "LEFT JOIN                                                                                        " & vbNewLine _
                                     & " (SELECT                                                                                         " & vbNewLine _
                                     & "   MTAN.STR_DATE AS STR_DATE                                                                     " & vbNewLine _
                                     & "  ,ROW_NUMBER() OVER (PARTITION BY MTAN.NRS_BR_CD ORDER BY MTAN.STR_DATE DESC) AS NUM            " & vbNewLine _
                                     & "  FROM                                                                                           " & vbNewLine _
                                     & "  $LM_MST$..M_GOODS MG                                                                           " & vbNewLine _
                                     & "  LEFT JOIN $LM_MST$..M_TANKA MTAN                                                               " & vbNewLine _
                                     & "  ON  MTAN.NRS_BR_CD = MG.NRS_BR_CD                                                              " & vbNewLine _
                                     & "  AND MTAN.CUST_CD_L = MG.CUST_CD_L                                                              " & vbNewLine _
                                     & "  AND MTAN.CUST_CD_M = MG.CUST_CD_M                                                              " & vbNewLine _
                                     & "  AND MTAN.UP_GP_CD_1 = MG.UP_GP_CD_1                                                            " & vbNewLine _
                                     & "  AND MTAN.STR_DATE <= @SYS_DATE                                                                 " & vbNewLine _
                                     & "  AND MTAN.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
                                     & "  WHERE                                                                                          " & vbNewLine _
                                     & "      MG.NRS_BR_CD = @NRS_BR_CD                                                                  " & vbNewLine _
                                     & "  AND (MG.GOODS_CD_NRS = @GOODS_CD_NRS)                                                          " & vbNewLine _
                                     & " ) DAYTBL                                                                                        " & vbNewLine _
                                     & " ON DAYTBL.NUM = 1                                                                               " & vbNewLine _
                                     & "--保管料(通常)                                                                       " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_TANKA AS MTANKA                                                " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MTANKA.NRS_BR_CD                                                  " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MTANKA.CUST_CD_L                                                  " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MTANKA.CUST_CD_M                                                  " & vbNewLine _
                                     & "AND MG.UP_GP_CD_1 = MTANKA.UP_GP_CD_1                                                " & vbNewLine _
                                     & "AND MTANKA.STR_DATE = DAYTBL.STR_DATE                                                " & vbNewLine _
                                     & "--保管料建区分名(温度管理なし)                                                       " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN1                                                    " & vbNewLine _
                                     & "ON  MTANKA.STORAGE_KB1 = KBN1.KBN_CD                                                 " & vbNewLine _
                                     & "AND KBN1.KBN_GROUP_CD = 'T005'                                                       " & vbNewLine _
                                     & "AND KBN1.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                     & "--保管料建区分名(温度管理あり)                                                       " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN2                                                    " & vbNewLine _
                                     & "ON  MTANKA.STORAGE_KB2 = KBN2.KBN_CD                                                 " & vbNewLine _
                                     & "AND KBN2.KBN_GROUP_CD = 'T005'                                                       " & vbNewLine _
                                     & "AND KBN2.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                     & "--荷役料建(入庫)区分名                                                               " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN3                                                    " & vbNewLine _
                                     & "ON  MTANKA.HANDLING_IN_KB = KBN3.KBN_CD                                              " & vbNewLine _
                                     & "AND KBN3.KBN_GROUP_CD = 'T005'                                                       " & vbNewLine _
                                     & "AND KBN3.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                     & "--荷役料建(出庫)区分名                                                               " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN4                                                    " & vbNewLine _
                                     & "ON  MTANKA.HANDLING_OUT_KB = KBN4.KBN_CD                                             " & vbNewLine _
                                     & "AND KBN4.KBN_GROUP_CD = 'T005'                                                       " & vbNewLine _
                                     & "AND KBN4.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                     & "--類別区分                                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SHOBO AS MSHOBO                                                " & vbNewLine _
                                     & "ON  MG.SHOBO_CD = MSHOBO.SHOBO_CD                                                    " & vbNewLine _
                                     & "AND MSHOBO.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                     & "--類別区分名                                                                         " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN5                                                    " & vbNewLine _
                                     & "ON  MSHOBO.RUI = KBN5.KBN_CD                                                         " & vbNewLine _
                                     & "AND KBN5.KBN_GROUP_CD = 'S004'                                                       " & vbNewLine _
                                     & "AND KBN5.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                     & "--危険種別名                                                                         " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN6                                                    " & vbNewLine _
                                     & "ON  MSHOBO.SYU = KBN6.KBN_CD                                                         " & vbNewLine _
                                     & "AND KBN6.KBN_GROUP_CD = 'S022'                                                       " & vbNewLine _
                                     & "AND KBN6.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                     & "--危険品区分名                                                                       " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN7                                                    " & vbNewLine _
                                     & "ON  MG.KIKEN_KB = KBN7.KBN_CD                                                        " & vbNewLine _
                                     & "AND KBN7.KBN_GROUP_CD = 'K008'                                                       " & vbNewLine _
                                     & "AND KBN7.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                     & "--PG区分名                                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN8                                                    " & vbNewLine _
                                     & "ON  MG.PG_KB = KBN8.KBN_CD                                                           " & vbNewLine _
                                     & "AND KBN8.KBN_GROUP_CD = 'P002'                                                       " & vbNewLine _
                                     & "AND KBN8.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                     & "--化学物質区分名                                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN9                                                    " & vbNewLine _
                                     & "ON  MG.CHEM_MTRL_KB = KBN9.KBN_CD                                                    " & vbNewLine _
                                     & "AND KBN9.KBN_GROUP_CD = 'K007'                                                       " & vbNewLine _
                                     & "AND KBN9.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                     & "--毒劇区分名                                                                         " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN10                                                   " & vbNewLine _
                                     & "ON  MG.DOKU_KB = KBN10.KBN_CD                                                        " & vbNewLine _
                                     & "AND KBN10.KBN_GROUP_CD = 'G001'                                                      " & vbNewLine _
                                     & "AND KBN10.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--ガス管理区分名                                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN11                                                   " & vbNewLine _
                                     & "ON  MG.GAS_KANRI_KB = KBN11.KBN_CD                                                   " & vbNewLine _
                                     & "AND KBN11.KBN_GROUP_CD = 'G002'                                                      " & vbNewLine _
                                     & "AND KBN11.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--温度管理区分名                                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN12                                                   " & vbNewLine _
                                     & "ON  MG.ONDO_KB = KBN12.KBN_CD                                                        " & vbNewLine _
                                     & "AND KBN12.KBN_GROUP_CD = 'O002'                                                      " & vbNewLine _
                                     & "AND KBN12.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--運送温度区分名                                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN13                                                   " & vbNewLine _
                                     & "ON  MG.UNSO_ONDO_KB = KBN13.KBN_CD                                                   " & vbNewLine _
                                     & "AND KBN13.KBN_GROUP_CD = 'U006'                                                      " & vbNewLine _
                                     & "AND KBN13.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--倉庫協会品目区分名                                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN14                                                   " & vbNewLine _
                                     & "ON  MG.KYOKAI_GOODS_KB = KBN14.KBN_CD                                                " & vbNewLine _
                                     & "AND KBN14.KBN_GROUP_CD = 'K004'                                                      " & vbNewLine _
                                     & "AND KBN14.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--引当単位区分名                                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN15                                                   " & vbNewLine _
                                     & "ON  MG.ALCTD_KB = KBN15.KBN_CD                                                       " & vbNewLine _
                                     & "AND KBN15.KBN_GROUP_CD = 'H012'                                                      " & vbNewLine _
                                     & "AND KBN15.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--個数単位名                                                                         " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN16                                                   " & vbNewLine _
                                     & "ON  MG.NB_UT = KBN16.KBN_CD                                                          " & vbNewLine _
                                     & "AND KBN16.KBN_GROUP_CD = 'K002'                                                      " & vbNewLine _
                                     & "AND KBN16.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--包装単位名(=入数単位)                                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN17                                                   " & vbNewLine _
                                     & "ON  MG.PKG_UT = KBN17.KBN_CD                                                         " & vbNewLine _
                                     & "AND KBN17.KBN_GROUP_CD = 'N001'                                                      " & vbNewLine _
                                     & "AND KBN17.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--標準入目単位名                                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN18                                                   " & vbNewLine _
                                     & "ON  MG.STD_IRIME_UT = KBN18.KBN_CD                                                   " & vbNewLine _
                                     & "AND KBN18.KBN_GROUP_CD = 'I001'                                                      " & vbNewLine _
                                     & "AND KBN18.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--風袋重量                                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN19                                                   " & vbNewLine _
                                     & "ON  MG.PKG_UT = KBN19.KBN_CD                                                         " & vbNewLine _
                                     & "AND KBN19.KBN_GROUP_CD = 'N001'                                                      " & vbNewLine _
                                     & "AND KBN19.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--風袋加算フラグ名                                                                   " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN20                                                   " & vbNewLine _
                                     & "ON  MG.TARE_YN = KBN20.KBN_CD                                                        " & vbNewLine _
                                     & "AND KBN20.KBN_GROUP_CD = 'U009'                                                      " & vbNewLine _
                                     & "AND KBN20.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--既定の保留品区分名                                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN21                                                   " & vbNewLine _
                                     & "ON  MG.DEF_SPD_KB = KBN21.KBN_CD                                                     " & vbNewLine _
                                     & "AND KBN21.KBN_GROUP_CD = 'H003'                                                      " & vbNewLine _
                                     & "AND KBN21.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--寄託価格単位区分名                                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN22                                                   " & vbNewLine _
                                     & "ON  MG.KITAKU_AM_UT_KB = KBN22.KBN_CD                                                " & vbNewLine _
                                     & "AND KBN22.KBN_GROUP_CD = 'T003'                                                      " & vbNewLine _
                                     & "AND KBN22.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--発注点区分名                                                                       " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN23                                                   " & vbNewLine _
                                     & "ON  MG.ORDER_KB = KBN23.KBN_CD                                                       " & vbNewLine _
                                     & "AND KBN23.KBN_GROUP_CD = 'H007'                                                      " & vbNewLine _
                                     & "AND KBN23.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--指定納品書区分名                                                                   " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN24                                                   " & vbNewLine _
                                     & "ON  MG.SP_NHS_YN = KBN24.KBN_CD                                                      " & vbNewLine _
                                     & "AND KBN24.KBN_GROUP_CD = 'U009'                                                      " & vbNewLine _
                                     & "AND KBN24.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--分析表区分名                                                                       " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN25                                                   " & vbNewLine _
                                     & "ON  MG.COA_YN = KBN25.KBN_CD                                                         " & vbNewLine _
                                     & "AND KBN25.KBN_GROUP_CD = 'U001'                                                      " & vbNewLine _
                                     & "AND KBN25.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--ロット管理レベル名                                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN26                                                   " & vbNewLine _
                                     & "ON  MG.LOT_CTL_KB = KBN26.KBN_CD                                                     " & vbNewLine _
                                     & "AND KBN26.KBN_GROUP_CD = 'R002'                                                      " & vbNewLine _
                                     & "AND KBN26.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--賞味期限管理の有無名                                                               " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN27                                                   " & vbNewLine _
                                     & "ON  MG.LT_DATE_CTL_KB = KBN27.KBN_CD                                                 " & vbNewLine _
                                     & "AND KBN27.KBN_GROUP_CD = 'U009'                                                      " & vbNewLine _
                                     & "AND KBN27.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--製造日管理の有無名                                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN28                                                   " & vbNewLine _
                                     & "ON  MG.CRT_DATE_CTL_KB = KBN28.KBN_CD                                                " & vbNewLine _
                                     & "AND KBN28.KBN_GROUP_CD = 'U009'                                                      " & vbNewLine _
                                     & "AND KBN28.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--請求明細書出力フラグ名                                                             " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN29                                                   " & vbNewLine _
                                     & "ON  MG.SKYU_MEI_YN = KBN29.KBN_CD                                                    " & vbNewLine _
                                     & "AND KBN29.KBN_GROUP_CD = 'U009'                                                      " & vbNewLine _
                                     & "AND KBN29.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--引当注意品フラグ名                                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN30                                                   " & vbNewLine _
                                     & "ON  MG.HIKIATE_ALERT_YN = KBN30.KBN_CD                                               " & vbNewLine _
                                     & "AND KBN30.KBN_GROUP_CD = 'U009'                                                      " & vbNewLine _
                                     & "AND KBN30.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--宅急便サイズ名                                                                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN AS KBN31                                                   " & vbNewLine _
                                     & "ON  MG.SIZE_KB = KBN31.KBN_CD                                                        " & vbNewLine _
                                     & "AND KBN31.KBN_GROUP_CD = 'T010'                                                      " & vbNewLine _
                                     & "AND KBN31.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                     & "--入荷時加工作業名1                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SAGYO AS MSAGYO1                                               " & vbNewLine _
                                     & "ON  MG.INKA_KAKO_SAGYO_KB_1 = MSAGYO1.SAGYO_CD                                       " & vbNewLine _
                                     & "AND MSAGYO1.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
                                     & "--入荷時加工作業名2                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SAGYO AS MSAGYO2                                               " & vbNewLine _
                                     & "ON  MG.INKA_KAKO_SAGYO_KB_2 = MSAGYO2.SAGYO_CD                                       " & vbNewLine _
                                     & "AND MSAGYO2.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
                                     & "--入荷時加工作業名3                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SAGYO AS MSAGYO3                                               " & vbNewLine _
                                     & "ON  MG.INKA_KAKO_SAGYO_KB_3 = MSAGYO3.SAGYO_CD                                       " & vbNewLine _
                                     & "AND MSAGYO3.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
                                     & "--入荷時加工作業名4                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SAGYO AS MSAGYO4                                               " & vbNewLine _
                                     & "ON  MG.INKA_KAKO_SAGYO_KB_4 = MSAGYO4.SAGYO_CD                                       " & vbNewLine _
                                     & "AND MSAGYO4.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
                                     & "--入荷時加工作業名5                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SAGYO AS MSAGYO5                                               " & vbNewLine _
                                     & "ON  MG.INKA_KAKO_SAGYO_KB_5 = MSAGYO5.SAGYO_CD                                       " & vbNewLine _
                                     & "AND MSAGYO5.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
                                     & "--出荷時加工作業名1                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SAGYO AS MSAGYO6                                               " & vbNewLine _
                                     & "ON  MG.OUTKA_KAKO_SAGYO_KB_1 = MSAGYO6.SAGYO_CD                                      " & vbNewLine _
                                     & "AND MSAGYO6.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
                                     & "--出荷時加工作業名2                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SAGYO AS MSAGYO7                                               " & vbNewLine _
                                     & "ON  MG.OUTKA_KAKO_SAGYO_KB_2 = MSAGYO7.SAGYO_CD                                      " & vbNewLine _
                                     & "AND MSAGYO7.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
                                     & "--出荷時加工作業名3                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SAGYO AS MSAGYO8                                               " & vbNewLine _
                                     & "ON  MG.OUTKA_KAKO_SAGYO_KB_3 = MSAGYO8.SAGYO_CD                                      " & vbNewLine _
                                     & "AND MSAGYO8.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
                                     & "--出荷時加工作業名4                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SAGYO AS MSAGYO9                                               " & vbNewLine _
                                     & "ON  MG.OUTKA_KAKO_SAGYO_KB_4 = MSAGYO9.SAGYO_CD                                      " & vbNewLine _
                                     & "AND MSAGYO9.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
                                     & "--出荷時加工作業名5                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SAGYO AS MSAGYO10                                              " & vbNewLine _
                                     & "ON  MG.OUTKA_KAKO_SAGYO_KB_5 = MSAGYO10.SAGYO_CD                                     " & vbNewLine _
                                     & "AND MSAGYO10.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
                                     & "--梱包作業料名                                                                       " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SAGYO AS MSAGYO11                                              " & vbNewLine _
                                     & "ON  MG.PKG_SAGYO = MSAGYO11.SAGYO_CD                                                 " & vbNewLine _
                                     & "AND MSAGYO11.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
                                     & "--作成者                                                                             " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..S_USER AS USER1                                                  " & vbNewLine _
                                     & "ON  MG.SYS_ENT_USER = USER1.USER_CD                                                  " & vbNewLine _
                                     & "--更新者                                                                             " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..S_USER AS USER2                                                  " & vbNewLine _
                                     & "ON  MG.SYS_UPD_USER = USER2.USER_CD                                                  " & vbNewLine _
                                     & "--出荷Lでの荷主帳票パターン取得                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                  " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR1.NRS_BR_CD                                                    " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR1.CUST_CD_L                                                    " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR1.CUST_CD_M                                                    " & vbNewLine _
                                     & "AND '00' = MCR1.CUST_CD_S                                                            " & vbNewLine _
                                     & "AND MCR1.PTN_ID = '67'                                                               " & vbNewLine _
                                     & "--帳票パターン取得                                                                   " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                                                        " & vbNewLine _
                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                   " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                                                         " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                                                         " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得                                                " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                  " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                    " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                    " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                    " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                    " & vbNewLine _
                                     & "AND MCR2.PTN_ID = '67'                                                               " & vbNewLine _
                                     & "--帳票パターン取得                                                                   " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                                                        " & vbNewLine _
                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                   " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                                                         " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                                                         " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                                                   " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR3                                                        " & vbNewLine _
                                     & "ON  MR3.NRS_BR_CD = MG.NRS_BR_CD                                                     " & vbNewLine _
                                     & "AND MR3.PTN_ID = '67'                                                                " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                                                         " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "WHERE                                                                                " & vbNewLine _
                                     & "MG.SYS_DEL_FLG = '0'                                                                 " & vbNewLine


    '2011/9/9 SBS)佐川 GROPU BY 不要のため削除
    '''' <summary>
    '''' GROUP BY（商品コード）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_GROUP_BY As String = "GROUP BY                                             " & vbNewLine _
    '                                      & "CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID   " & vbNewLine _
    '                                      & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID  " & vbNewLine _
    '                                      & "      ELSE MR3.RPT_ID                              " & vbNewLine _
    '                                      & " END                                               " & vbNewLine _
    '                                        & ",MG.SYS_DEL_FLG                                  " & vbNewLine _
    '                                        & ",MG.NRS_BR_CD                                    " & vbNewLine _
    '                                        & ",MG.CUST_CD_L                                    " & vbNewLine _
    '                                        & ",MG.CUST_CD_M                                    " & vbNewLine _
    '                                        & ",MG.CUST_CD_S                                    " & vbNewLine _
    '                                        & ",MG.CUST_CD_SS                                   " & vbNewLine _
    '                                        & ",MC.CUST_NM_L                                    " & vbNewLine _
    '                                        & ",MC.CUST_NM_M                                    " & vbNewLine _
    '                                        & ",MC.CUST_NM_S                                    " & vbNewLine _
    '                                        & ",MC.CUST_NM_SS                                   " & vbNewLine _
    '                                        & ",MC.OYA_SEIQTO_CD                                " & vbNewLine _
    '                                        & ",MSEIQTO.SEIQTO_NM                               " & vbNewLine _
    '                                        & ",MSEIQTO.SEIQTO_BUSYO_NM                         " & vbNewLine _
    '                                        & ",MG.GOODS_CD_NRS                                 " & vbNewLine _
    '                                        & ",MG.GOODS_CD_CUST                                " & vbNewLine _
    '                                        & ",MG.SEARCH_KEY_1                                 " & vbNewLine _
    '                                        & ",MG.SEARCH_KEY_2                                 " & vbNewLine _
    '                                        & ",MG.CUST_COST_CD1                                " & vbNewLine _
    '                                        & ",MG.CUST_COST_CD2                                " & vbNewLine _
    '                                        & ",MG.JAN_CD                                       " & vbNewLine _
    '                                        & ",MG.GOODS_NM_1                                   " & vbNewLine _
    '                                        & ",MG.GOODS_NM_2                                   " & vbNewLine _
    '                                        & ",MG.GOODS_NM_3                                   " & vbNewLine _
    '                                        & ",MG.UP_GP_CD_1                                   " & vbNewLine _
    '                                        & ",MTANKA.STORAGE_1                                " & vbNewLine _
    '                                        & ",MTANKA.STORAGE_KB1                              " & vbNewLine _
    '                                        & ",KBN1.KBN_NM1                                    " & vbNewLine _
    '                                        & ",MTANKA.STORAGE_2                                " & vbNewLine _
    '                                        & ",MTANKA.STORAGE_KB2                              " & vbNewLine _
    '                                        & ",KBN2.KBN_NM1                                    " & vbNewLine _
    '                                        & ",MTANKA.HANDLING_IN                              " & vbNewLine _
    '                                        & ",MTANKA.HANDLING_IN_KB                           " & vbNewLine _
    '                                        & ",KBN3.KBN_NM1                                    " & vbNewLine _
    '                                        & ",MTANKA.HANDLING_OUT                             " & vbNewLine _
    '                                        & ",MTANKA.HANDLING_OUT_KB                          " & vbNewLine _
    '                                        & ",KBN4.KBN_NM1                                    " & vbNewLine _
    '                                        & ",MTANKA.MINI_TEKI_IN_AMO                         " & vbNewLine _
    '                                        & ",MG.SHOBO_CD                                     " & vbNewLine _
    '                                        & ",MSHOBO.RUI                                      " & vbNewLine _
    '                                        & ",KBN5.KBN_NM1                                    " & vbNewLine _
    '                                        & ",MSHOBO.HINMEI                                   " & vbNewLine _
    '                                        & ",MSHOBO.SEISITSU                                 " & vbNewLine _
    '                                        & ",MSHOBO.SYU                                      " & vbNewLine _
    '                                        & ",KBN6.KBN_NM1                                    " & vbNewLine _
    '                                        & ",MG.KIKEN_KB                                     " & vbNewLine _
    '                                        & ",KBN7.KBN_NM1                                    " & vbNewLine _
    '                                        & ",MG.UN                                           " & vbNewLine _
    '                                        & ",MG.PG_KB                                        " & vbNewLine _
    '                                        & ",KBN8.KBN_NM1                                    " & vbNewLine _
    '                                        & ",MG.CLASS_1                                      " & vbNewLine _
    '                                        & ",MG.CLASS_2                                      " & vbNewLine _
    '                                        & ",MG.CLASS_3                                      " & vbNewLine _
    '                                        & ",MG.CHEM_MTRL_KB                                 " & vbNewLine _
    '                                        & ",KBN9.KBN_NM1                                    " & vbNewLine _
    '                                        & ",MG.DOKU_KB                                      " & vbNewLine _
    '                                        & ",KBN10.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.GAS_KANRI_KB                                 " & vbNewLine _
    '                                        & ",KBN11.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.ONDO_KB                                      " & vbNewLine _
    '                                        & ",KBN12.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.UNSO_ONDO_KB                                 " & vbNewLine _
    '                                        & ",KBN13.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.ONDO_MX                                      " & vbNewLine _
    '                                        & ",MG.ONDO_MM                                      " & vbNewLine _
    '                                        & ",MG.ONDO_STR_DATE                                " & vbNewLine _
    '                                        & ",MG.ONDO_END_DATE                                " & vbNewLine _
    '                                        & ",MG.ONDO_UNSO_STR_DATE                           " & vbNewLine _
    '                                        & ",MG.ONDO_UNSO_END_DATE                           " & vbNewLine _
    '                                        & ",MG.KYOKAI_GOODS_KB                              " & vbNewLine _
    '                                        & ",KBN14.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.ALCTD_KB                                     " & vbNewLine _
    '                                        & ",KBN15.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.NB_UT                                        " & vbNewLine _
    '                                        & ",KBN16.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.PKG_NB                                       " & vbNewLine _
    '                                        & ",MG.PKG_UT                                       " & vbNewLine _
    '                                        & ",KBN17.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.PLT_PER_PKG_UT                               " & vbNewLine _
    '                                        & ",MG.STD_IRIME_NB                                 " & vbNewLine _
    '                                        & ",MG.STD_IRIME_UT                                 " & vbNewLine _
    '                                        & ",KBN18.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.STD_WT_KGS                                   " & vbNewLine _
    '                                        & ",MG.STD_CBM                                      " & vbNewLine _
    '                                        & ",KBN19.VALUE1                                    " & vbNewLine _
    '                                        & ",MG.TARE_YN                                      " & vbNewLine _
    '                                        & ",KBN20.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.DEF_SPD_KB                                   " & vbNewLine _
    '                                        & ",KBN21.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.KITAKU_AM_UT_KB                              " & vbNewLine _
    '                                        & ",KBN22.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.KITAKU_GOODS_UP                              " & vbNewLine _
    '                                        & ",MG.ORDER_KB                                     " & vbNewLine _
    '                                        & ",KBN23.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.ORDER_NB                                     " & vbNewLine _
    '                                        & ",MG.PRINT_NB                                     " & vbNewLine _
    '                                        & ",MG.SP_NHS_YN                                    " & vbNewLine _
    '                                        & ",KBN24.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.COA_YN                                       " & vbNewLine _
    '                                        & ",KBN25.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.LOT_CTL_KB                                   " & vbNewLine _
    '                                        & ",KBN26.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.LT_DATE_CTL_KB                               " & vbNewLine _
    '                                        & ",KBN27.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.CRT_DATE_CTL_KB                              " & vbNewLine _
    '                                        & ",KBN28.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.SKYU_MEI_YN                                  " & vbNewLine _
    '                                        & ",KBN29.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.HIKIATE_ALERT_YN                             " & vbNewLine _
    '                                        & ",KBN30.KBN_NM1                                   " & vbNewLine _
    '                                        & ",MG.SHIP_CD_L                                    " & vbNewLine _
    '                                        & ",MG.INKA_KAKO_SAGYO_KB_1                         " & vbNewLine _
    '                                        & ",MG.INKA_KAKO_SAGYO_KB_2                         " & vbNewLine _
    '                                        & ",MG.INKA_KAKO_SAGYO_KB_3                         " & vbNewLine _
    '                                        & ",MG.INKA_KAKO_SAGYO_KB_4                         " & vbNewLine _
    '                                        & ",MG.INKA_KAKO_SAGYO_KB_5                         " & vbNewLine _
    '                                        & ",MG.OUTKA_KAKO_SAGYO_KB_1                        " & vbNewLine _
    '                                        & ",MG.OUTKA_KAKO_SAGYO_KB_2                        " & vbNewLine _
    '                                        & ",MG.OUTKA_KAKO_SAGYO_KB_3                        " & vbNewLine _
    '                                        & ",MG.OUTKA_KAKO_SAGYO_KB_4                        " & vbNewLine _
    '                                        & ",MG.OUTKA_KAKO_SAGYO_KB_5                        " & vbNewLine _
    '                                        & ",MSAGYO1.SAGYO_NM                                " & vbNewLine _
    '                                        & ",MSAGYO2.SAGYO_NM                                " & vbNewLine _
    '                                        & ",MSAGYO3.SAGYO_NM                                " & vbNewLine _
    '                                        & ",MSAGYO4.SAGYO_NM                                " & vbNewLine _
    '                                        & ",MSAGYO5.SAGYO_NM                                " & vbNewLine _
    '                                        & ",MSAGYO6.SAGYO_NM                                " & vbNewLine _
    '                                        & ",MSAGYO7.SAGYO_NM                                " & vbNewLine _
    '                                        & ",MSAGYO8.SAGYO_NM                                " & vbNewLine _
    '                                        & ",MSAGYO9.SAGYO_NM                                " & vbNewLine _
    '                                        & ",MSAGYO10.SAGYO_NM                               " & vbNewLine _
    '                                        & ",MG.PKG_SAGYO                                    " & vbNewLine _
    '                                        & ",MSAGYO11.SAGYO_NM                               " & vbNewLine _
    '                                        & ",MG.OUTKA_ATT                                    " & vbNewLine _
    '                                        & ",USER1.USER_NM                                   " & vbNewLine _
    '                                        & ",MG.SYS_ENT_DATE                                 " & vbNewLine _
    '                                        & ",MG.SYS_ENT_TIME                                 " & vbNewLine _
    '                                        & ",USER2.USER_NM                                   " & vbNewLine _
    '                                        & ",MG.SYS_UPD_DATE                                 " & vbNewLine _
    '                                        & ",MG.SYS_UPD_TIME                                 " & vbNewLine
                                            

    ''' <summary>
    ''' ORDER BY（荷主商品コード）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                    " & vbNewLine _
                                         & "     MGD.GOODS_CD_NRS_EDA                   " & vbNewLine


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
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM510IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM510DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMM510DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ初期化/設定
        Call Me.SetParamSearch()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM510DAC", "SelectMPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        Return ds

    End Function


    ''' <summary>
    ''' 商品マスタ一覧表データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ一覧表データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM510IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM510DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM510DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        'Me._StrSql.Append(LMM510DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用Group By句)
        'Me._StrSql.Append(LMM510DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ初期化/設定
        Call Me.SetParamSearch()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM510DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("SEIQTO_BUSYO_NM", "SEIQTO_BUSYO_NM")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
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
        map.Add("STR_DATE", "STR_DATE")
        map.Add("STORAGE_1", "STORAGE_1")
        map.Add("STORAGE_KB1", "STORAGE_KB1")
        map.Add("STORAGE_KB1_NM", "STORAGE_KB1_NM")
        map.Add("STORAGE_2", "STORAGE_2")
        map.Add("STORAGE_KB2", "STORAGE_KB2")
        map.Add("STORAGE_KB2_NM", "STORAGE_KB2_NM")
        map.Add("HANDLING_IN", "HANDLING_IN")
        map.Add("HANDLING_IN_KB", "HANDLING_IN_KB")
        map.Add("HANDLING_IN_KB_NM", "HANDLING_IN_KB_NM")
        map.Add("HANDLING_OUT", "HANDLING_OUT")
        map.Add("HANDLING_OUT_KB", "HANDLING_OUT_KB")
        map.Add("HANDLING_OUT_KB_NM", "HANDLING_OUT_KB_NM")
        map.Add("MINI_TEKI_IN_AMO", "MINI_TEKI_IN_AMO")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("RUI", "RUI")
        map.Add("RUI_NM", "RUI_NM")
        map.Add("HINMEI", "HINMEI")
        map.Add("SEISITSU", "SEISITSU")
        map.Add("SYU", "SYU")
        map.Add("SYU_NM", "SYU_NM")
        map.Add("KIKEN_KB", "KIKEN_KB")
        map.Add("KIKEN_KB_NM", "KIKEN_KB_NM")
        map.Add("UN", "UN")
        map.Add("PG_KB", "PG_KB")
        map.Add("PG_KB_NM", "PG_KB_NM")
        map.Add("CLASS_1", "CLASS_1")
        map.Add("CLASS_2", "CLASS_2")
        map.Add("CLASS_3", "CLASS_3")
        map.Add("CHEM_MTRL_KB", "CHEM_MTRL_KB")
        map.Add("CHEM_MTRL_KB_NM", "CHEM_MTRL_KB_NM")
        map.Add("DOKU_KB", "DOKU_KB")
        map.Add("DOKU_KB_NM", "DOKU_KB_NM")
        map.Add("GAS_KANRI_KB", "GAS_KANRI_KB")
        map.Add("GAS_KANRI_KB_NM", "GAS_KANRI_KB_NM")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_KB_NM", "ONDO_KB_NM")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("UNSO_ONDO_KB_NM", "UNSO_ONDO_KB_NM")
        map.Add("ONDO_MX", "ONDO_MX")
        map.Add("ONDO_MM", "ONDO_MM")
        map.Add("ONDO_STR_DATE", "ONDO_STR_DATE")
        map.Add("ONDO_END_DATE", "ONDO_END_DATE")
        map.Add("ONDO_UNSO_STR_DATE", "ONDO_UNSO_STR_DATE")
        map.Add("ONDO_UNSO_END_DATE", "ONDO_UNSO_END_DATE")
        map.Add("KYOKAI_GOODS_KB", "KYOKAI_GOODS_KB")
        map.Add("KYOKAI_GOODS_KB_NM", "KYOKAI_GOODS_KB_NM")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("ALCTD_KB_NM", "ALCTD_KB_NM")
        map.Add("NB_UT", "NB_UT")
        map.Add("NB_UT_NM", "NB_UT_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("PKG_UT_NM", "PKG_UT_NM")
        map.Add("PLT_PER_PKG_UT", "PLT_PER_PKG_UT")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("STD_IRIME_UT_NM", "STD_IRIME_UT_NM")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("STD_CBM", "STD_CBM")
        map.Add("NT_GR_CONV_RATE", "NT_GR_CONV_RATE")
        map.Add("TARE_YN", "TARE_YN")
        map.Add("TARE_YN_NM", "TARE_YN_NM")
        map.Add("DEF_SPD_KB", "DEF_SPD_KB")
        map.Add("DEF_SPD_KB_NM", "DEF_SPD_KB_NM")
        map.Add("KITAKU_AM_UT_KB", "KITAKU_AM_UT_KB")
        map.Add("KITAKU_AM_UT_KB_NM", "KITAKU_AM_UT_KB_NM")
        map.Add("KITAKU_GOODS_UP", "KITAKU_GOODS_UP")
        map.Add("ORDER_KB", "ORDER_KB")
        map.Add("ORDER_KB_NM", "ORDER_KB_NM")
        map.Add("ORDER_NB", "ORDER_NB")
        map.Add("PRINT_NB", "PRINT_NB")
        map.Add("SP_NHS_YN", "SP_NHS_YN")
        map.Add("SP_NHS_YN_NM", "SP_NHS_YN_NM")
        map.Add("COA_YN", "COA_YN")
        map.Add("COA_YN_NM", "COA_YN_NM")
        map.Add("LOT_CTL_KB", "LOT_CTL_KB")
        map.Add("LOT_CTL_KB_NM", "LOT_CTL_KB_NM")
        map.Add("LT_DATE_CTL_KB", "LT_DATE_CTL_KB")
        map.Add("LT_DATE_CTL_KB_NM", "LT_DATE_CTL_KB_NM")
        map.Add("CRT_DATE_CTL_KB", "CRT_DATE_CTL_KB")
        map.Add("CRT_DATE_CTL_KB_NM", "CRT_DATE_CTL_KB_NM")
        map.Add("SKYU_MEI_YN", "SKYU_MEI_YN")
        map.Add("SKYU_MEI_YN_NM", "SKYU_MEI_YN_NM")
        map.Add("HIKIATE_ALERT_YN", "HIKIATE_ALERT_YN")
        map.Add("HIKIATE_ALERT_YN_NM", "HIKIATE_ALERT_YN_NM")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
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
        map.Add("INKA_KAKO_SAGYO_NM_1", "INKA_KAKO_SAGYO_NM_1")
        map.Add("INKA_KAKO_SAGYO_NM_2", "INKA_KAKO_SAGYO_NM_2")
        map.Add("INKA_KAKO_SAGYO_NM_3", "INKA_KAKO_SAGYO_NM_3")
        map.Add("INKA_KAKO_SAGYO_NM_4", "INKA_KAKO_SAGYO_NM_4")
        map.Add("INKA_KAKO_SAGYO_NM_5", "INKA_KAKO_SAGYO_NM_5")
        map.Add("OUTKA_KAKO_SAGYO_NM_1", "OUTKA_KAKO_SAGYO_NM_1")
        map.Add("OUTKA_KAKO_SAGYO_NM_2", "OUTKA_KAKO_SAGYO_NM_2")
        map.Add("OUTKA_KAKO_SAGYO_NM_3", "OUTKA_KAKO_SAGYO_NM_3")
        map.Add("OUTKA_KAKO_SAGYO_NM_4", "OUTKA_KAKO_SAGYO_NM_4")
        map.Add("OUTKA_KAKO_SAGYO_NM_5", "OUTKA_KAKO_SAGYO_NM_5")
        map.Add("PKG_SAGYO", "PKG_SAGYO")
        map.Add("PKG_SAGYO_NM", "PKG_SAGYO_NM")
        map.Add("OUTKA_ATT", "OUTKA_ATT")
        map.Add("SET_NAIYO_01", "SET_NAIYO_01")
        map.Add("SET_NAIYO_02", "SET_NAIYO_02")
        map.Add("SET_NAIYO_03", "SET_NAIYO_03")
        map.Add("SET_NAIYO_04", "SET_NAIYO_04")
        map.Add("SET_NAIYO_05", "SET_NAIYO_05")
        map.Add("SET_NAIYO_06", "SET_NAIYO_06")
        map.Add("SET_NAIYO_07", "SET_NAIYO_07")
        map.Add("SET_NAIYO_08", "SET_NAIYO_08")
        map.Add("SET_NAIYO_09", "SET_NAIYO_09")
        map.Add("SET_NAIYO_10", "SET_NAIYO_10")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("SET_NAIYO_TAITLE_01", "SET_NAIYO_TAITLE_01")
        map.Add("SET_NAIYO_TAITLE_02", "SET_NAIYO_TAITLE_02")
        map.Add("SET_NAIYO_TAITLE_03", "SET_NAIYO_TAITLE_03")
        map.Add("SET_NAIYO_TAITLE_04", "SET_NAIYO_TAITLE_04")
        map.Add("SET_NAIYO_TAITLE_05", "SET_NAIYO_TAITLE_05")
        map.Add("SET_NAIYO_TAITLE_06", "SET_NAIYO_TAITLE_06")
        map.Add("SET_NAIYO_TAITLE_07", "SET_NAIYO_TAITLE_07")
        map.Add("SET_NAIYO_TAITLE_08", "SET_NAIYO_TAITLE_08")
        map.Add("SET_NAIYO_TAITLE_09", "SET_NAIYO_TAITLE_09")
        map.Add("SET_NAIYO_TAITLE_10", "SET_NAIYO_TAITLE_10")
        map.Add("WIDTH", "WIDTH")
        map.Add("HEIGHT", "HEIGHT")
        map.Add("DEPTH", "DEPTH")
        map.Add("VOLUME", "VOLUME")
        map.Add("OCCUPY_VOLUME", "OCCUPY_VOLUME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM510OUT")

        Return ds

    End Function

    ''' <summary>
    ''' DETAILSデータ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>DETAILSデータ取得SQLの構築・発行</remarks>
    Private Function SelectDetailsData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM510IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM510DAC.SQL_SELECT_DETAILS)   'SQL構築(データ抽出用Select句)
        Call Me.SetConditionMasterSQL2()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMM510DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ初期化/設定
        'Call Me.SetParamSearch()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM510DAC", "SelectPrintData", cmd)

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


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM510_GOODS_DETAILS")

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


            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            andstr.Append("AND") 
            andstr.Append(" MG.NRS_BR_CD = @NRS_BR_CD")
            andstr.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '商品key
            whereStr = .Item("GOODS_CD_NRS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (MG.GOODS_CD_NRS = @GOODS_CD_NRS)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", whereStr, DBDataType.CHAR))
            End If


            If andstr.Length <> 0 Then
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub


    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL2()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row


            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            andstr.Append("AND")
            andstr.Append(" MGD.NRS_BR_CD = @NRS_BR_CD")
            andstr.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '商品key
            whereStr = .Item("GOODS_CD_NRS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (MGD.GOODS_CD_NRS = @GOODS_CD_NRS)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", whereStr, DBDataType.CHAR))
            End If


            If andstr.Length <> 0 Then
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub


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

#End Region

#Region "パラメータの設定"

    ''' <summary>
    ''' パラメータ設定モジュール(検索用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSearch()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))

    End Sub

#End Region
#End Region

End Class
