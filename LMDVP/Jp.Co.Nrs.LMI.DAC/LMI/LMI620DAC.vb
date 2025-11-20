' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特殊荷主機能
'  プログラムID     :  LMI620    : 最低荷役保証料・差額明細（千葉・日産物流用）
'  作  成  者       :  kurihara
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI620DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI620DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "        INKAL.NRS_BR_CD                                  AS NRS_BR_CD " & vbNewLine _
                                            & "      , 'AS'                                             AS PTN_ID    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD              " & vbNewLine _
                                            & "        ELSE MR3.PTN_CD END                              AS PTN_CD    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID END                              AS RPT_ID    " & vbNewLine


    ''' <summary>
    ''' 帳票種別取得用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private Const SQL_MPrt_FROM As String = "  FROM $LM_TRN$..B_INKA_L INKAL                                      " & vbNewLine _
                                          & "       --入荷M                                                       " & vbNewLine _
                                          & "       LEFT JOIN $LM_TRN$..B_INKA_M INKAM                            " & vbNewLine _
                                          & "            ON INKAL.NRS_BR_CD = INKAM.NRS_BR_CD                     " & vbNewLine _
                                          & "           AND INKAL.INKA_NO_L = INKAM.INKA_NO_L                     " & vbNewLine _
                                          & "           AND INKAM.SYS_DEL_FLG  = '0'                              " & vbNewLine _
                                          & "       --入荷S                                                       " & vbNewLine _
                                          & "       LEFT JOIN $LM_TRN$..B_INKA_S INKAS                            " & vbNewLine _
                                          & "            ON INKAM.NRS_BR_CD = INKAS.NRS_BR_CD                     " & vbNewLine _
                                          & "           AND INKAM.INKA_NO_L = INKAS.INKA_NO_L                     " & vbNewLine _
                                          & "           AND INKAM.INKA_NO_M = INKAS.INKA_NO_M                     " & vbNewLine _
                                          & "           AND INKAS.SYS_DEL_FLG  = '0'                              " & vbNewLine _
                                          & "       --商品マスタ                                                  " & vbNewLine _
                                          & "       LEFT JOIN $LM_MST$..M_GOODS GOODS                             " & vbNewLine _
                                          & "            ON INKAM.NRS_BR_CD    = GOODS.NRS_BR_CD                  " & vbNewLine _
                                          & "           AND INKAM.GOODS_CD_NRS = GOODS.GOODS_CD_NRS               " & vbNewLine _
                                          & "       --荷主マスタ                                                  " & vbNewLine _
                                          & "       LEFT JOIN $LM_MST$..M_CUST CUST                               " & vbNewLine _
                                          & "            ON CUST.NRS_BR_CD  = GOODS.NRS_BR_CD                     " & vbNewLine _
                                          & "           AND CUST.CUST_CD_L  = GOODS.CUST_CD_L                     " & vbNewLine _
                                          & "           AND CUST.CUST_CD_M  = GOODS.CUST_CD_M                     " & vbNewLine _
                                          & "           AND CUST.CUST_CD_S  = GOODS.CUST_CD_S                     " & vbNewLine _
                                          & "           AND CUST.CUST_CD_SS = GOODS.CUST_CD_SS                    " & vbNewLine _
                                          & "           AND CUST.SYS_DEL_FLG  = '0'                               " & vbNewLine _
                                          & "       --荷主帳票パターン取得                                        " & vbNewLine _
                                          & "       LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                           " & vbNewLine _
                                          & "            ON INKAL.NRS_BR_CD = MCR1.NRS_BR_CD                      " & vbNewLine _
                                          & "           AND GOODS.CUST_CD_L = MCR1.CUST_CD_L                      " & vbNewLine _
                                          & "           AND GOODS.CUST_CD_M = MCR1.CUST_CD_M                      " & vbNewLine _
                                          & "           AND '00' = MCR1.CUST_CD_S                                 " & vbNewLine _
                                          & "           AND MCR1.PTN_ID = 'AS'                                    " & vbNewLine _
                                          & "       --帳票パターン取得                                            " & vbNewLine _
                                          & "       LEFT JOIN $LM_MST$..M_RPT MR1                                 " & vbNewLine _
                                          & "            ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                        " & vbNewLine _
                                          & "           AND MR1.PTN_ID = MCR1.PTN_ID                              " & vbNewLine _
                                          & "           AND MR1.PTN_CD = MCR1.PTN_CD                              " & vbNewLine _
                                          & "           AND MR1.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                          & "       --商品Mの荷主での荷主帳票パターン取得                         " & vbNewLine _
                                          & "       LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                           " & vbNewLine _
                                          & "            ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                      " & vbNewLine _
                                          & "           AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                      " & vbNewLine _
                                          & "           AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                      " & vbNewLine _
                                          & "           AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                      " & vbNewLine _
                                          & "           AND MCR2.PTN_ID = 'AS'                                    " & vbNewLine _
                                          & "       --帳票パターン取得                                            " & vbNewLine _
                                          & "       LEFT JOIN $LM_MST$..M_RPT MR2                                 " & vbNewLine _
                                          & "            ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                        " & vbNewLine _
                                          & "           AND MR2.PTN_ID = MCR2.PTN_ID                              " & vbNewLine _
                                          & "           AND MR2.PTN_CD = MCR2.PTN_CD                              " & vbNewLine _
                                          & "           AND MR2.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                          & "       --存在しない場合の帳票パターン取得                            " & vbNewLine _
                                          & "       LEFT JOIN $LM_MST$..M_RPT MR3                                 " & vbNewLine _
                                          & "            ON MR3.NRS_BR_CD = INKAL.NRS_BR_CD                       " & vbNewLine _
                                          & "           AND MR3.PTN_ID = 'AS'                                     " & vbNewLine _
                                          & "           AND MR3.STANDARD_FLAG = '01'                              " & vbNewLine _
                                          & "           AND MR3.SYS_DEL_FLG = '0'                                 " & vbNewLine


    ''' <summary>
    ''' 帳票種別取得用 WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_WHERE As String = " WHERE                                       " & vbNewLine _
                                           & "       INKAL.NRS_BR_CD     =  @NRS_BR_CD     " & vbNewLine _
                                           & "   AND INKAL.CUST_CD_L     =  @CUST_CD_L     " & vbNewLine _
                                           & "   AND INKAL.CUST_CD_M     =  @CUST_CD_M     " & vbNewLine



#Region "SELECT句(印刷データ：MAIN)"

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAIN As String = " --SQL_SELECT_MAIN                                                " & vbNewLine _
                                       & " SELECT                                                                " & vbNewLine _
                                       & "        RPT_ID                                                         " & vbNewLine _
                                       & "      , NRS_BR_CD                                                      " & vbNewLine _
                                       & "      , INOUT_KB                                                       " & vbNewLine _
                                       & "      , F_DATE                                                         " & vbNewLine _
                                       & "      , T_DATE                                                         " & vbNewLine _
                                       & "      , CUST_CD_L                                                      " & vbNewLine _
                                       & "      , CUST_CD_M                                                      " & vbNewLine _
                                       & "      , CUST_CD_S                                                      " & vbNewLine _
                                       & "      , CUST_CD_SS                                                     " & vbNewLine _
                                       & "      , CUST_NM_L                                                      " & vbNewLine _
                                       & "      , CUST_NM_M                                                      " & vbNewLine _
                                       & "      , CUST_NM_S                                                      " & vbNewLine _
                                       & "      , CUST_NM_SS                                                     " & vbNewLine _
                                       & "      , NIYAKU_SEIQTO_CD                                               " & vbNewLine _
                                       & "      , DEST_NM                                                        " & vbNewLine _
                                       & "      , CUST_ORD_NO                                                    " & vbNewLine _
                                       & "      , UNSOCO_NM                                                      " & vbNewLine _
                                       & "      , GOODS_CD_NRS                                                   " & vbNewLine _
                                       & "      , GOODS_CD_CUST                                                  " & vbNewLine _
                                       & "      , GOODS_NM_1                                                     " & vbNewLine _
                                       & "      , INKA_NO_L                                                      " & vbNewLine _
                                       & "      , INOUT_DATE                                                     " & vbNewLine _
                                       & "      , LOT_NO                                                         " & vbNewLine _
                                       & "      , SERIAL_NO                                                      " & vbNewLine _
                                       & "      , TOU_NO                                                         " & vbNewLine _
                                       & "      , SITU_NO                                                        " & vbNewLine _
                                       & "      , ZONE_CD                                                        " & vbNewLine _
                                       & "      , LOCA                                                           " & vbNewLine _
                                       & "      , PKG_UT                                                         " & vbNewLine _
                                       & "      , NB_UT                                                          " & vbNewLine _
                                       & "      , IRIME                                                          " & vbNewLine _
                                       & "      , STD_IRIME_UT                                                   " & vbNewLine _
                                       & "      , TAX_KB                                                         " & vbNewLine _
                                       & "      --項目無し    ,ZAI.INKA_TP                                       " & vbNewLine _
                                       & "      , LT_DATE                                                        " & vbNewLine _
                                       & "      , GOODS_CRT_DATE                                                 " & vbNewLine _
                                       & "      , REMARK_OUT                                                     " & vbNewLine _
                                       & "      , GOODS_COND_KB_1                                                " & vbNewLine _
                                       & "      , GOODS_COND_KB_2                                                " & vbNewLine _
                                       & "      , GOODS_COND_KB_3                                                " & vbNewLine _
                                       & "      , SPD_KB                                                         " & vbNewLine _
                                       & "      , OFB_KB                                                         " & vbNewLine _
                                       & "      --項目無し    ,ZAI.NAIGAI_KB                                     " & vbNewLine _
                                       & "      , ZAIKO_KB                                                       " & vbNewLine _
                                       & "      , IN_NB                                                          " & vbNewLine _
                                       & "      , OUT_NB                                                         " & vbNewLine _
                                       & "      , NIYAKU_TANKA                                                   " & vbNewLine _
                                       & "      , NIYAKU_HOSHO                                                   " & vbNewLine _
                                       & "      , N_AM_TTL                                                       " & vbNewLine _
                                       & "      , N_AM_SAGAKU                                                    " & vbNewLine _
                                       & " FROM                                                                  " & vbNewLine _
                                       & "      (                                                                " & vbNewLine

#End Region


#Region "SELECT句(印刷データ：入荷)"
    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INKA As String = " --SQL_SELECT_INKA                                              " & vbNewLine _
                                   & " SELECT                                                                  " & vbNewLine _
                                   & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                     " & vbNewLine _
                                   & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
                                   & "    ELSE MR3.RPT_ID END                 AS RPT_ID                        " & vbNewLine _
                                   & "  , INKAL.NRS_BR_CD                     AS NRS_BR_CD                     " & vbNewLine _
                                   & "  , 'IN'                                AS INOUT_KB                      " & vbNewLine _
                                   & "  , @F_DATE                             AS F_DATE                        " & vbNewLine _
                                   & "  , @T_DATE                             AS T_DATE                        " & vbNewLine _
                                   & "  , CUST.CUST_CD_L                      AS CUST_CD_L                     " & vbNewLine _
                                   & "  , CUST.CUST_CD_M                      AS CUST_CD_M                     " & vbNewLine _
                                   & "  , CUST.CUST_CD_S                      AS CUST_CD_S                     " & vbNewLine _
                                   & "  , CUST.CUST_CD_SS                     AS CUST_CD_SS                    " & vbNewLine _
                                   & "  , CUST.CUST_NM_L                      AS CUST_NM_L                     " & vbNewLine _
                                   & "  , CUST.CUST_NM_M                      AS CUST_NM_M                     " & vbNewLine _
                                   & "  , CUST.CUST_NM_S                      AS CUST_NM_S                     " & vbNewLine _
                                   & "  , CUST.CUST_NM_SS                     AS CUST_NM_SS                    " & vbNewLine _
                                   & "  , CASE WHEN CUST.NIYAKU_SEIQTO_CD = ''  THEN CUST.NIYAKU_SEIQTO_CD     " & vbNewLine _
                                   & "    ELSE CUST.HOKAN_SEIQTO_CD                                            " & vbNewLine _
                                   & "    END                                 AS NIYAKU_SEIQTO_CD              " & vbNewLine _
                                   & "  , CASE WHEN INKAEDIL.INKA_CTL_NO_L IS NOT NULL THEN DESTEDI.DEST_NM    " & vbNewLine _
                                   & "    ELSE DESTL.DEST_NM                                                   " & vbNewLine _
                                   & "    END                                 AS DEST_NM                       " & vbNewLine _
                                   & "  , INKAL.OUTKA_FROM_ORD_NO_L           AS CUST_ORD_NO                   " & vbNewLine _
                                   & "  , UNSOCO.UNSOCO_NM                    AS UNSOCO_NM                     " & vbNewLine _
                                   & "  , ZAI.GOODS_CD_NRS                    AS GOODS_CD_NRS                  " & vbNewLine _
                                   & "  , GOODS.GOODS_CD_CUST                 AS GOODS_CD_CUST                 " & vbNewLine _
                                   & "  , GOODS.GOODS_NM_1                    AS GOODS_NM_1                    " & vbNewLine _
                                   & "--  , INKAL.INKA_NO_L                     AS INKA_NO_L                     " & vbNewLine _
                                   & "  , CASE WHEN CUST.SAITEI_HAN_KB = '03' THEN INKAL.INKA_NO_L            " & vbNewLine _
                                   & "          ELSE '' END  AS INKA_NO_L                                     " & vbNewLine _
                                   & "  , INKAL.HOKAN_STR_DATE                AS INOUT_DATE                    " & vbNewLine _
                                   & "--  , INKAS.LOT_NO                        AS LOT_NO                        " & vbNewLine _
                                   & "--  , INKAS.SERIAL_NO                     AS SERIAL_NO                     " & vbNewLine _
                                   & "--  , INKAS.TOU_NO                        AS TOU_NO                        " & vbNewLine _
                                   & "--  , INKAS.SITU_NO                       AS SITU_NO                       " & vbNewLine _
                                   & "--  , INKAS.ZONE_CD                       AS ZONE_CD                       " & vbNewLine _
                                   & "--  , INKAS.LOCA                          AS LOCA                          " & vbNewLine _
                                   & "--  , GOODS.PKG_UT                        AS PKG_UT                        " & vbNewLine _
                                   & "--  , GOODS.NB_UT                         AS NB_UT                         " & vbNewLine _
                                   & "--  , INKAS.IRIME                         AS IRIME                         " & vbNewLine _
                                   & "--  , GOODS.STD_IRIME_UT                  AS STD_IRIME_UT                  " & vbNewLine _
                                   & "  , CASE WHEN CUST.SAITEI_HAN_KB <> '00' THEN INKAS.LOT_NO                " & vbNewLine _
                                   & "          ELSE '' END  AS LOT_NO                                         " & vbNewLine _
                                   & "--  , CASE WHEN CUST.SAITEI_HAN_KB <> '00' THEN INKAS.SERIAL_NO             " & vbNewLine _
                                   & "--          ELSE '' END  AS SERIAL_NO                                      " & vbNewLine _
                                   & "  , INKAS.SERIAL_NO                     AS SERIAL_NO                     " & vbNewLine _
                                   & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN INKAS.TOU_NO                " & vbNewLine _
                                   & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN INKAS.TOU_NO                " & vbNewLine _
                                   & "         ELSE '' END  AS TOU_NO                                          " & vbNewLine _
                                   & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN INKAS.SITU_NO               " & vbNewLine _
                                   & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN INKAS.SITU_NO               " & vbNewLine _
                                   & "         ELSE '' END  AS SITU_NO                                         " & vbNewLine _
                                   & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN INKAS.ZONE_CD               " & vbNewLine _
                                   & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN INKAS.ZONE_CD               " & vbNewLine _
                                   & "         ELSE '' END  AS ZONE_CD                                         " & vbNewLine _
                                   & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN INKAS.LOCA                  " & vbNewLine _
                                   & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN INKAS.LOCA                  " & vbNewLine _
                                   & "         ELSE '' END  AS LOCA                                            " & vbNewLine _
                                   & "  , GOODS.PKG_UT                        AS PKG_UT                        " & vbNewLine _
                                   & "  , GOODS.NB_UT                         AS NB_UT                         " & vbNewLine _
                                   & "--  , INKAS.IRIME                         AS IRIME                       " & vbNewLine _
                                   & "  , CASE WHEN CUST.SAITEI_HAN_KB <> '00' THEN INKAS.IRIME                 " & vbNewLine _
                                   & "         ELSE 0 END  AS IRIME                                           " & vbNewLine _
                                   & "  , GOODS.STD_IRIME_UT                  AS STD_IRIME_UT                  " & vbNewLine _
                                   & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                   & "--         ELSE GOODS.PKG_UT END  AS PKG_UT                                " & vbNewLine _
                                   & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                   & "--         ELSE GOODS.NB_UT END  AS NB_UT                                  " & vbNewLine _
                                   & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                   & "--         ELSE INKAS.IRIME END  AS IRIME                                  " & vbNewLine _
                                   & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                   & "--         ELSE GOODS.STD_IRIME_UT END  AS STD_IRIME_UT                    " & vbNewLine _
                                   & "  , ZAI.TAX_KB                          AS TAX_KB                        " & vbNewLine _
                                   & " --項目無し    ,ZAI.INKA_TP           AS INKA_TP                         " & vbNewLine _
                                   & "  , ZAI.LT_DATE                         AS LT_DATE                       " & vbNewLine _
                                   & "  , ZAI.GOODS_CRT_DATE                  AS GOODS_CRT_DATE                " & vbNewLine _
                                   & "  , ZAI.REMARK_OUT                      AS REMARK_OUT                    " & vbNewLine _
                                   & "  , ZAI.GOODS_COND_KB_1                 AS GOODS_COND_KB_1               " & vbNewLine _
                                   & "  , ZAI.GOODS_COND_KB_2                 AS GOODS_COND_KB_2               " & vbNewLine _
                                   & "  , ZAI.GOODS_COND_KB_3                 AS GOODS_COND_KB_3               " & vbNewLine _
                                   & "  , ZAI.SPD_KB                          AS SPD_KB                        " & vbNewLine _
                                   & "  , ZAI.OFB_KB                          AS OFB_KB                        " & vbNewLine _
                                   & " --項目無し    ,ZAI.NAIGAI_KB         AS NAIGAI_KB                       " & vbNewLine _
                                   & "  , CASE WHEN KBN_S006.KBN_NM1  IS NULL THEN 'F'                         " & vbNewLine _
                                   & "    ELSE KBN_S006.KBN_NM1                                                " & vbNewLine _
                                   & "    END                                 AS ZAIKO_KB                      " & vbNewLine _
                                   & "  , (SUM(INKAS.KONSU) * GOODS.PKG_NB) + SUM(INKAS.HASU)                                    AS IN_NB            " & vbNewLine _
                                   & "  , 0                                                                                      AS OUT_NB           " & vbNewLine _
                                   & "  , TANKA.HANDLING_IN                                                                      AS NIYAKU_TANKA     " & vbNewLine _
                                   & "  , TANKA.MINI_TEKI_IN_AMO                                                                 AS NIYAKU_HOSHO     " & vbNewLine _
                                   & "  , ((SUM(INKAS.KONSU) * GOODS.PKG_NB) + SUM(INKAS.HASU)) * TANKA.HANDLING_IN              AS N_AM_TTL         " & vbNewLine _
                                   & "  , TANKA.MINI_TEKI_IN_AMO                                                                                     " & vbNewLine _
                                   & "        -  ((SUM(INKAS.KONSU) * GOODS.PKG_NB) + SUM(INKAS.HASU)) * TANKA.HANDLING_IN       AS N_AM_SAGAKU      " & vbNewLine _
                                   & "                                                                         " & vbNewLine _
                                   & " FROM $LM_TRN$..B_INKA_L INKAL                                           " & vbNewLine _
                                   & "                                                                         " & vbNewLine _
                                   & " --入荷M                                                                 " & vbNewLine _
                                   & " LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                      " & vbNewLine _
                                   & "        ON INKAL.NRS_BR_CD = INKAM.NRS_BR_CD                             " & vbNewLine _
                                   & "       AND INKAL.INKA_NO_L = INKAM.INKA_NO_L                             " & vbNewLine _
                                   & "       AND INKAM.SYS_DEL_FLG  = '0'                                      " & vbNewLine _
                                   & " --入荷S                                                                 " & vbNewLine _
                                   & " LEFT JOIN $LM_TRN$..B_INKA_S INKAS                                      " & vbNewLine _
                                   & "        ON  INKAM.NRS_BR_CD = INKAS.NRS_BR_CD                            " & vbNewLine _
                                   & "       AND INKAM.INKA_NO_L = INKAS.INKA_NO_L                             " & vbNewLine _
                                   & "       AND INKAM.INKA_NO_M = INKAS.INKA_NO_M                             " & vbNewLine _
                                   & "       AND INKAS.SYS_DEL_FLG  = '0'                                      " & vbNewLine _
                                   & " --入荷EDIL                                                              " & vbNewLine _
                                   & " LEFT JOIN (                                                             " & vbNewLine _
                                   & "             SELECT NRS_BR_CD                                            " & vbNewLine _
                                   & "                  , INKA_CTL_NO_L                                        " & vbNewLine _
                                   & "                  , CUST_CD_L                                            " & vbNewLine _
                                   & "                  , OUTKA_MOTO                                           " & vbNewLine _
                                   & "                  , SYS_DEL_FLG                                          " & vbNewLine _
                                   & "              FROM $LM_TRN$..H_INKAEDI_L                                 " & vbNewLine _
                                   & "              GROUP BY                                                   " & vbNewLine _
                                   & "                    NRS_BR_CD                                            " & vbNewLine _
                                   & "                  , INKA_CTL_NO_L                                        " & vbNewLine _
                                   & "                  , CUST_CD_L                                            " & vbNewLine _
                                   & "                  , OUTKA_MOTO                                           " & vbNewLine _
                                   & "                  , SYS_DEL_FLG                                          " & vbNewLine _
                                   & "            ) INKAEDIL                                                   " & vbNewLine _
                                   & "        ON INKAEDIL.NRS_BR_CD     = INKAL.NRS_BR_CD                      " & vbNewLine _
                                   & "       AND INKAEDIL.INKA_CTL_NO_L = INKAL.INKA_NO_L                      " & vbNewLine _
                                   & "       AND INKAEDIL.SYS_DEL_FLG   = '0'                                  " & vbNewLine _
                                   & " --運送L                                                                 " & vbNewLine _
                                   & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                      " & vbNewLine _
                                   & "        ON INKAL.NRS_BR_CD   = UNSOL.NRS_BR_CD                           " & vbNewLine _
                                   & "       AND INKAL.INKA_NO_L   = UNSOL.INOUTKA_NO_L                        " & vbNewLine _
                                   & "       AND UNSOL.MOTO_DATA_KB = '10'                                     " & vbNewLine _
                                   & "       AND UNSOL.SYS_DEL_FLG  = '0'                                      " & vbNewLine _
                                   & " --在庫                                                                  " & vbNewLine _
                                   & " LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                       " & vbNewLine _
                                   & "        ON ZAI.NRS_BR_CD = INKAS.NRS_BR_CD                               " & vbNewLine _
                                   & "       AND ZAI.ZAI_REC_NO = INKAS.ZAI_REC_NO                             " & vbNewLine _
                                   & "       AND ZAI.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                   & " --届先M（出荷元取得入荷L参照）                                          " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_DEST DESTL                                        " & vbNewLine _
                                   & "        ON DESTL.NRS_BR_CD = INKAL.NRS_BR_CD                             " & vbNewLine _
                                   & "       AND DESTL.CUST_CD_L = INKAL.CUST_CD_L                             " & vbNewLine _
                                   & "       AND DESTL.DEST_CD   = UNSOL.ORIG_CD                               " & vbNewLine _
                                   & "       AND DESTL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                   & " --届先M（出荷元取得EDIL参照）                                           " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_DEST DESTEDI                                      " & vbNewLine _
                                   & "        ON DESTEDI.NRS_BR_CD = INKAEDIL.NRS_BR_CD                        " & vbNewLine _
                                   & "       AND DESTEDI.CUST_CD_L = INKAEDIL.CUST_CD_L                        " & vbNewLine _
                                   & "       AND DESTEDI.DEST_CD   = INKAEDIL.OUTKA_MOTO                       " & vbNewLine _
                                   & "       AND DESTEDI.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                   & " --運送会社マスタ                                                        " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_UNSOCO UNSOCO                                     " & vbNewLine _
                                   & "        ON UNSOCO.NRS_BR_CD    = UNSOL.NRS_BR_CD                         " & vbNewLine _
                                   & "       AND UNSOCO.UNSOCO_CD    = UNSOL.UNSO_CD                           " & vbNewLine _
                                   & "       AND UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                        " & vbNewLine _
                                   & "       AND UNSOCO.SYS_DEL_FLG = '0'                                      " & vbNewLine _
                                   & " --商品マスタ                                                            " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                       " & vbNewLine _
                                   & "        ON INKAM.NRS_BR_CD    = GOODS.NRS_BR_CD                          " & vbNewLine _
                                   & "       AND INKAM.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                       " & vbNewLine _
                                   & " --単価マスタ                                                            " & vbNewLine _
                                   & " --UPD START 2023/04/14 過請求改修                                       " & vbNewLine _
                                   & " --LEFT JOIN $LM_MST$..M_TANKA TANKA                                     " & vbNewLine _
                                   & " --       ON TANKA.NRS_BR_CD  = GOODS.NRS_BR_CD                          " & vbNewLine _
                                   & " --      AND TANKA.CUST_CD_L  = GOODS.CUST_CD_L                          " & vbNewLine _
                                   & " --      AND TANKA.CUST_CD_M  = GOODS.CUST_CD_M                          " & vbNewLine _
                                   & " --      AND TANKA.UP_GP_CD_1 = GOODS.UP_GP_CD_1                         " & vbNewLine _
                                   & " --      AND TANKA.STR_DATE <= @F_DATE                                   " & vbNewLine _
                                   & " --      AND TANKA.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                   & " LEFT JOIN (                                                             " & vbNewLine _
                                   & "        SELECT *                                                         " & vbNewLine _
                                   & "             , ROW_NUMBER() OVER (                                       " & vbNewLine _
                                   & "                PARTITION BY NRS_BR_CD, CUST_CD_L, CUST_CD_M, UP_GP_CD_1 " & vbNewLine _
                                   & "                ORDER BY STR_DATE DESC) AS NUM                           " & vbNewLine _
                                   & "          FROM $LM_MST$..M_TANKA                                         " & vbNewLine _
                                   & "          WHERE SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                   & "            AND STR_DATE <= @F_DATE                                      " & vbNewLine _
                                   & "  ) TANKA                                                                " & vbNewLine _
                                   & "        ON TANKA.NRS_BR_CD  = GOODS.NRS_BR_CD                            " & vbNewLine _
                                   & "       AND TANKA.CUST_CD_L  = GOODS.CUST_CD_L                            " & vbNewLine _
                                   & "       AND TANKA.CUST_CD_M  = GOODS.CUST_CD_M                            " & vbNewLine _
                                   & "       AND TANKA.UP_GP_CD_1 = GOODS.UP_GP_CD_1                           " & vbNewLine _
                                   & "       AND TANKA.NUM = 1                                                 " & vbNewLine _
                                   & " --UPD END 2023/04/14 過請求改修                                         " & vbNewLine _
                                   & " --荷主マスタ                                                            " & vbNewLine _
                                   & " LEFT JOIN (                                                             " & vbNewLine _
                                   & "        SELECT                                                           " & vbNewLine _
                                   & "              NRS_BR_CD                                                  " & vbNewLine _
                                   & "            , CUST_CD_L                                                  " & vbNewLine _
                                   & "            , CUST_CD_M                                                  " & vbNewLine _
                                   & "            , CUST_CD_S                                                  " & vbNewLine _
                                   & "            , CUST_CD_SS                                                 " & vbNewLine _
                                   & "            , CUST_NM_L                                                  " & vbNewLine _
                                   & "            , CUST_NM_M                                                  " & vbNewLine _
                                   & "            , CUST_NM_S                                                  " & vbNewLine _
                                   & "            , CUST_NM_SS                                                 " & vbNewLine _
                                   & "            , NIYAKU_SEIQTO_CD                                           " & vbNewLine _
                                   & "            , HOKAN_SEIQTO_CD                                            " & vbNewLine _
                                   & "            , '03' AS SAITEI_HAN_KB -- マスタ値を参照せず常に固定値とする" & vbNewLine _
                                   & "            , SYS_DEL_FLG                                                " & vbNewLine _
                                   & "        FROM                                                             " & vbNewLine _
                                   & "            $LM_MST$..M_CUST                                             " & vbNewLine _
                                   & "  ) CUST                                                                 " & vbNewLine _
                                   & "        ON CUST.NRS_BR_CD  = GOODS.NRS_BR_CD                             " & vbNewLine _
                                   & "       AND CUST.CUST_CD_L  = GOODS.CUST_CD_L                             " & vbNewLine _
                                   & "       AND CUST.CUST_CD_M  = GOODS.CUST_CD_M                             " & vbNewLine _
                                   & "       AND CUST.CUST_CD_S  = GOODS.CUST_CD_S                             " & vbNewLine _
                                   & "       AND CUST.CUST_CD_SS = GOODS.CUST_CD_SS                            " & vbNewLine _
                                   & "       AND CUST.SYS_DEL_FLG  = '0'                                       " & vbNewLine _
                                   & " --商品状態区分2(外観)                                                   " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..Z_KBN KBN_S006                                      " & vbNewLine _
                                   & "        ON KBN_S006.KBN_GROUP_CD = 'S006'                                " & vbNewLine _
                                   & "       AND KBN_S006.KBN_CD = ZAI.GOODS_COND_KB_2                         " & vbNewLine _
                                   & "       AND KBN_S006.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                   & " --荷主帳票パターン取得                                                  " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                     " & vbNewLine _
                                   & "        ON INKAL.NRS_BR_CD = MCR1.NRS_BR_CD                              " & vbNewLine _
                                   & "       AND GOODS.CUST_CD_L = MCR1.CUST_CD_L                              " & vbNewLine _
                                   & "       AND GOODS.CUST_CD_M = MCR1.CUST_CD_M                              " & vbNewLine _
                                   & "       AND '00' = MCR1.CUST_CD_S                                         " & vbNewLine _
                                   & "       AND MCR1.PTN_ID = 'AS'                                            " & vbNewLine _
                                   & " --帳票パターン取得                                                      " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_RPT MR1                                           " & vbNewLine _
                                   & "        ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                " & vbNewLine _
                                   & "       AND MR1.PTN_ID = MCR1.PTN_ID                                      " & vbNewLine _
                                   & "       AND MR1.PTN_CD = MCR1.PTN_CD                                      " & vbNewLine _
                                   & "       AND MR1.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                   & " --商品Mの荷主での荷主帳票パターン取得                                   " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                     " & vbNewLine _
                                   & "        ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                              " & vbNewLine _
                                   & "       AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                              " & vbNewLine _
                                   & "       AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                              " & vbNewLine _
                                   & "       AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                              " & vbNewLine _
                                   & "       AND MCR2.PTN_ID = 'AS'                                            " & vbNewLine _
                                   & " --帳票パターン取得                                                      " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_RPT MR2                                           " & vbNewLine _
                                   & "        ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                " & vbNewLine _
                                   & "       AND MR2.PTN_ID = MCR2.PTN_ID                                      " & vbNewLine _
                                   & "       AND MR2.PTN_CD = MCR2.PTN_CD                                      " & vbNewLine _
                                   & "       AND MR2.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                   & " --存在しない場合の帳票パターン取得                                      " & vbNewLine _
                                   & " LEFT JOIN $LM_MST$..M_RPT MR3                                           " & vbNewLine _
                                   & "        ON MR3.NRS_BR_CD = INKAL.NRS_BR_CD                               " & vbNewLine _
                                   & "       AND MR3.PTN_ID = 'AS'                                             " & vbNewLine _
                                   & "       AND MR3.STANDARD_FLAG = '01'                                      " & vbNewLine _
                                   & "       AND MR3.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                   & "                                                                         " & vbNewLine _
                                   & " WHERE INKAL.CUST_CD_L = '00145'                                         " & vbNewLine _
                                   & "   AND INKAL.CUST_CD_M = '00'                                            " & vbNewLine _
                                   & "   AND INKAL.INKA_STATE_KB >= '50'   -- 'N004'                           " & vbNewLine _
                                   & "   AND INKAL.HOKAN_STR_DATE BETWEEN @F_DATE                              " & vbNewLine _
                                   & "   AND @T_DATE                                                           " & vbNewLine _
                                   & "   AND INKAL.NIYAKU_YN = '01'        -- 'U009'                           " & vbNewLine _
                                   & "   AND INKAL.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                                   & "   AND INKAL.NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
                                   & "   AND TANKA.HANDLING_IN > 0                                             " & vbNewLine _
                                   & "                                                                         " & vbNewLine _
                                   & " GROUP BY                                                                " & vbNewLine _
                                   & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                     " & vbNewLine _
                                   & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
                                   & "    ELSE MR3.RPT_ID END                                                  " & vbNewLine _
                                   & "   , INKAL.NRS_BR_CD                                                     " & vbNewLine _
                                   & "   , CUST.CUST_CD_L                                                      " & vbNewLine _
                                   & "   , CUST.CUST_CD_M                                                      " & vbNewLine _
                                   & "   , CUST.CUST_CD_S                                                      " & vbNewLine _
                                   & "   , CUST.CUST_CD_SS                                                     " & vbNewLine _
                                   & "   , CUST.CUST_NM_L                                                      " & vbNewLine _
                                   & "   , CUST.CUST_NM_M                                                      " & vbNewLine _
                                   & "   , CUST.CUST_NM_S                                                      " & vbNewLine _
                                   & "   , CUST.CUST_NM_SS                                                     " & vbNewLine _
                                   & "   , CASE WHEN CUST.NIYAKU_SEIQTO_CD = ''  THEN CUST.NIYAKU_SEIQTO_CD    " & vbNewLine _
                                   & "     ELSE CUST.HOKAN_SEIQTO_CD                                           " & vbNewLine _
                                   & "     END                                                                 " & vbNewLine _
                                   & "   , CASE WHEN INKAEDIL.INKA_CTL_NO_L IS NOT NULL THEN DESTEDI.DEST_NM   " & vbNewLine _
                                   & "     ELSE DESTL.DEST_NM                                                  " & vbNewLine _
                                   & "     END                                                                 " & vbNewLine _
                                   & "   , INKAL.OUTKA_FROM_ORD_NO_L                                           " & vbNewLine _
                                   & "   , UNSOCO.UNSOCO_NM                                                    " & vbNewLine _
                                   & "   , ZAI.GOODS_CD_NRS                                                    " & vbNewLine _
                                   & "   , GOODS.GOODS_CD_CUST                                                 " & vbNewLine _
                                   & "   , GOODS.GOODS_NM_1                                                    " & vbNewLine _
                                   & "--   , INKAL.INKA_NO_L                                                     " & vbNewLine _
                                   & "   , CASE WHEN CUST.SAITEI_HAN_KB = '03' THEN INKAL.INKA_NO_L            " & vbNewLine _
                                   & "          ELSE '' END                                                    " & vbNewLine _
                                   & "   , INKAL.HOKAN_STR_DATE                                                " & vbNewLine _
                                   & "  , CASE WHEN CUST.SAITEI_HAN_KB <> '00' THEN INKAS.LOT_NO                " & vbNewLine _
                                   & "          ELSE '' END                                                    " & vbNewLine _
                                   & "--  , CASE WHEN CUST.SAITEI_HAN_KB ,. '00' THEN INKAS.SERIAL_NO             " & vbNewLine _
                                   & "--          ELSE '' END                                                    " & vbNewLine _
                                   & "   , INKAS.SERIAL_NO                                                     " & vbNewLine _
                                   & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN INKAS.TOU_NO                " & vbNewLine _
                                   & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN INKAS.TOU_NO                " & vbNewLine _
                                   & "         ELSE '' END                                                     " & vbNewLine _
                                   & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN INKAS.SITU_NO               " & vbNewLine _
                                   & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN INKAS.SITU_NO               " & vbNewLine _
                                   & "         ELSE '' END                                                     " & vbNewLine _
                                   & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN INKAS.ZONE_CD               " & vbNewLine _
                                   & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN INKAS.ZONE_CD               " & vbNewLine _
                                   & "         ELSE '' END                                                     " & vbNewLine _
                                   & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN INKAS.LOCA                  " & vbNewLine _
                                   & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN INKAS.LOCA                  " & vbNewLine _
                                   & "         ELSE '' END                                                     " & vbNewLine _
                                   & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                   & "--         ELSE GOODS.PKG_UT END                                           " & vbNewLine _
                                   & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                   & "--         ELSE GOODS.NB_UT END                                            " & vbNewLine _
                                   & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                   & "--         ELSE INKAS.IRIME END                                            " & vbNewLine _
                                   & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                   & "--         ELSE GOODS.STD_IRIME_UT END                                     " & vbNewLine _
                                   & "--   , INKAS.LOT_NO                                                        " & vbNewLine _
                                   & "--   , INKAS.SERIAL_NO                                                     " & vbNewLine _
                                   & "--   , INKAS.TOU_NO                                                        " & vbNewLine _
                                   & "--   , INKAS.SITU_NO                                                       " & vbNewLine _
                                   & "--   , INKAS.ZONE_CD                                                       " & vbNewLine _
                                   & "--   , INKAS.LOCA                                                          " & vbNewLine _
                                   & "   , GOODS.PKG_UT                                                        " & vbNewLine _
                                   & "   , GOODS.NB_UT                                                         " & vbNewLine _
                                   & "--   , INKAS.IRIME                                                         " & vbNewLine _
                                   & "  , CASE WHEN CUST.SAITEI_HAN_KB <> '00' THEN INKAS.IRIME                 " & vbNewLine _
                                   & "         ELSE 0 END                                                     " & vbNewLine _
                                   & "   , GOODS.STD_IRIME_UT                                                  " & vbNewLine _
                                   & "   , ZAI.TAX_KB                                                          " & vbNewLine _
                                   & " --項目無し    ,ZAI.INKA_TP                                              " & vbNewLine _
                                   & "   , ZAI.LT_DATE                                                         " & vbNewLine _
                                   & "   , ZAI.GOODS_CRT_DATE                                                  " & vbNewLine _
                                   & "   , ZAI.REMARK_OUT                                                      " & vbNewLine _
                                   & "   , ZAI.GOODS_COND_KB_1                                                 " & vbNewLine _
                                   & "   , ZAI.GOODS_COND_KB_2                                                 " & vbNewLine _
                                   & "   , ZAI.GOODS_COND_KB_3                                                 " & vbNewLine _
                                   & "   , ZAI.SPD_KB                                                          " & vbNewLine _
                                   & "   , ZAI.OFB_KB                                                          " & vbNewLine _
                                   & " --項目無し    ,ZAI.NAIGAI_KB                                            " & vbNewLine _
                                   & "   , CASE WHEN KBN_S006.KBN_NM1  IS NULL THEN 'F'                        " & vbNewLine _
                                   & "     ELSE KBN_S006.KBN_NM1                                               " & vbNewLine _
                                   & "     END                                                                 " & vbNewLine _
                                   & "   , GOODS.PKG_NB                                                        " & vbNewLine _
                                   & "   , TANKA.HANDLING_IN                                                   " & vbNewLine _
                                   & "   , TANKA.MINI_TEKI_IN_AMO                                              " & vbNewLine _
                                   & "                                                                         " & vbNewLine _
                                   & " HAVING TANKA.MINI_TEKI_IN_AMO                                           " & vbNewLine _
                                   & "            -  ((SUM(INKAS.KONSU) * GOODS.PKG_NB) + SUM(INKAS.HASU)) * TANKA.HANDLING_IN > 0   " & vbNewLine
    '↑要望番号1689 UMANO修正


    'Private Const SQL_SELECT_INKA As String = " --SQL_SELECT_INKA                                                  " & vbNewLine _
    '                                   & " SELECT                                                                  " & vbNewLine _
    '                                   & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                     " & vbNewLine _
    '                                   & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
    '                                   & "    ELSE MR3.RPT_ID END                 AS RPT_ID                        " & vbNewLine _
    '                                   & "  , INKAL.NRS_BR_CD                     AS NRS_BR_CD                     " & vbNewLine _
    '                                   & "  , 'IN'                                AS INOUT_KB                      " & vbNewLine _
    '                                   & "  , @F_DATE                             AS F_DATE                        " & vbNewLine _
    '                                   & "  , @T_DATE                             AS T_DATE                        " & vbNewLine _
    '                                   & "  , CUST.CUST_CD_L                      AS CUST_CD_L                     " & vbNewLine _
    '                                   & "  , CUST.CUST_CD_M                      AS CUST_CD_M                     " & vbNewLine _
    '                                   & "  , CUST.CUST_CD_S                      AS CUST_CD_S                     " & vbNewLine _
    '                                   & "  , CUST.CUST_CD_SS                     AS CUST_CD_SS                    " & vbNewLine _
    '                                   & "  , CUST.CUST_NM_L                      AS CUST_NM_L                     " & vbNewLine _
    '                                   & "  , CUST.CUST_NM_M                      AS CUST_NM_M                     " & vbNewLine _
    '                                   & "  , CUST.CUST_NM_S                      AS CUST_NM_S                     " & vbNewLine _
    '                                   & "  , CUST.CUST_NM_SS                     AS CUST_NM_SS                    " & vbNewLine _
    '                                   & "  , CASE WHEN CUST.NIYAKU_SEIQTO_CD = ''  THEN CUST.NIYAKU_SEIQTO_CD     " & vbNewLine _
    '                                   & "    ELSE CUST.HOKAN_SEIQTO_CD                                            " & vbNewLine _
    '                                   & "    END                                 AS NIYAKU_SEIQTO_CD              " & vbNewLine _
    '                                   & "  , CASE WHEN INKAEDIL.INKA_CTL_NO_L IS NOT NULL THEN DESTEDI.DEST_NM    " & vbNewLine _
    '                                   & "    ELSE DESTL.DEST_NM                                                   " & vbNewLine _
    '                                   & "    END                                 AS DEST_NM                       " & vbNewLine _
    '                                   & "  , INKAL.OUTKA_FROM_ORD_NO_L           AS CUST_ORD_NO                   " & vbNewLine _
    '                                   & "  , UNSOCO.UNSOCO_NM                    AS UNSOCO_NM                     " & vbNewLine _
    '                                   & "  , ZAI.GOODS_CD_NRS                    AS GOODS_CD_NRS                  " & vbNewLine _
    '                                   & "  , GOODS.GOODS_CD_CUST                 AS GOODS_CD_CUST                 " & vbNewLine _
    '                                   & "  , GOODS.GOODS_NM_1                    AS GOODS_NM_1                    " & vbNewLine _
    '                                   & "  , INKAL.INKA_NO_L                     AS INKA_NO_L                     " & vbNewLine _
    '                                   & "  , INKAL.HOKAN_STR_DATE                AS INOUT_DATE                    " & vbNewLine _
    '                                   & "  , INKAS.LOT_NO                        AS LOT_NO                        " & vbNewLine _
    '                                   & "  , INKAS.SERIAL_NO                     AS SERIAL_NO                     " & vbNewLine _
    '                                   & "  , INKAS.TOU_NO                        AS TOU_NO                        " & vbNewLine _
    '                                   & "  , INKAS.SITU_NO                       AS SITU_NO                       " & vbNewLine _
    '                                   & "  , INKAS.ZONE_CD                       AS ZONE_CD                       " & vbNewLine _
    '                                   & "  , INKAS.LOCA                          AS LOCA                          " & vbNewLine _
    '                                   & "  , GOODS.PKG_UT                        AS PKG_UT                        " & vbNewLine _
    '                                   & "  , GOODS.NB_UT                         AS NB_UT                         " & vbNewLine _
    '                                   & "  , INKAS.IRIME                         AS IRIME                         " & vbNewLine _
    '                                   & "  , GOODS.STD_IRIME_UT                  AS STD_IRIME_UT                  " & vbNewLine _
    '                                   & "  , ZAI.TAX_KB                          AS TAX_KB                        " & vbNewLine _
    '                                   & " --項目無し    ,ZAI.INKA_TP           AS INKA_TP                         " & vbNewLine _
    '                                   & "  , ZAI.LT_DATE                         AS LT_DATE                       " & vbNewLine _
    '                                   & "  , ZAI.GOODS_CRT_DATE                  AS GOODS_CRT_DATE                " & vbNewLine _
    '                                   & "  , ZAI.REMARK_OUT                      AS REMARK_OUT                    " & vbNewLine _
    '                                   & "  , ZAI.GOODS_COND_KB_1                 AS GOODS_COND_KB_1               " & vbNewLine _
    '                                   & "  , ZAI.GOODS_COND_KB_2                 AS GOODS_COND_KB_2               " & vbNewLine _
    '                                   & "  , ZAI.GOODS_COND_KB_3                 AS GOODS_COND_KB_3               " & vbNewLine _
    '                                   & "  , ZAI.SPD_KB                          AS SPD_KB                        " & vbNewLine _
    '                                   & "  , ZAI.OFB_KB                          AS OFB_KB                        " & vbNewLine _
    '                                   & " --項目無し    ,ZAI.NAIGAI_KB         AS NAIGAI_KB                       " & vbNewLine _
    '                                   & "  , CASE WHEN KBN_S006.KBN_NM1  IS NULL THEN 'F'                         " & vbNewLine _
    '                                   & "    ELSE KBN_S006.KBN_NM1                                                " & vbNewLine _
    '                                   & "    END                                 AS ZAIKO_KB                      " & vbNewLine _
    '                                   & "  , (SUM(INKAS.KONSU) * GOODS.PKG_NB) + SUM(INKAS.HASU)                                    AS IN_NB            " & vbNewLine _
    '                                   & "  , 0                                                                                      AS OUT_NB           " & vbNewLine _
    '                                   & "  , TANKA.HANDLING_IN                                                                      AS NIYAKU_TANKA     " & vbNewLine _
    '                                   & "  , 300                                                                                    AS NIYAKU_HOSHO     " & vbNewLine _
    '                                   & "  , ((SUM(INKAS.KONSU) * GOODS.PKG_NB) + SUM(INKAS.HASU)) * TANKA.HANDLING_IN              AS N_AM_TTL         " & vbNewLine _
    '                                   & "  , 300 -  ((SUM(INKAS.KONSU) * GOODS.PKG_NB) + SUM(INKAS.HASU)) * TANKA.HANDLING_IN       AS N_AM_SAGAKU      " & vbNewLine _
    '                                   & "                                                                         " & vbNewLine _
    '                                   & " FROM $LM_TRN$..B_INKA_L INKAL                                           " & vbNewLine _
    '                                   & "                                                                         " & vbNewLine _
    '                                   & " --入荷M                                                                 " & vbNewLine _
    '                                   & " LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                      " & vbNewLine _
    '                                   & "        ON INKAL.NRS_BR_CD = INKAM.NRS_BR_CD                             " & vbNewLine _
    '                                   & "       AND INKAL.INKA_NO_L = INKAM.INKA_NO_L                             " & vbNewLine _
    '                                   & "       AND INKAM.SYS_DEL_FLG  = '0'                                      " & vbNewLine _
    '                                   & " --入荷S                                                                 " & vbNewLine _
    '                                   & " LEFT JOIN $LM_TRN$..B_INKA_S INKAS                                      " & vbNewLine _
    '                                   & "        ON  INKAM.NRS_BR_CD = INKAS.NRS_BR_CD                            " & vbNewLine _
    '                                   & "       AND INKAM.INKA_NO_L = INKAS.INKA_NO_L                             " & vbNewLine _
    '                                   & "       AND INKAM.INKA_NO_M = INKAS.INKA_NO_M                             " & vbNewLine _
    '                                   & "       AND INKAS.SYS_DEL_FLG  = '0'                                      " & vbNewLine _
    '                                   & " --入荷EDIL                                                              " & vbNewLine _
    '                                   & " LEFT JOIN (                                                             " & vbNewLine _
    '                                   & "             SELECT NRS_BR_CD                                            " & vbNewLine _
    '                                   & "                  , INKA_CTL_NO_L                                        " & vbNewLine _
    '                                   & "                  , CUST_CD_L                                            " & vbNewLine _
    '                                   & "                  , OUTKA_MOTO                                           " & vbNewLine _
    '                                   & "                  , SYS_DEL_FLG                                          " & vbNewLine _
    '                                   & "              FROM $LM_TRN$..H_INKAEDI_L                                 " & vbNewLine _
    '                                   & "              GROUP BY                                                   " & vbNewLine _
    '                                   & "                    NRS_BR_CD                                            " & vbNewLine _
    '                                   & "                  , INKA_CTL_NO_L                                        " & vbNewLine _
    '                                   & "                  , CUST_CD_L                                            " & vbNewLine _
    '                                   & "                  , OUTKA_MOTO                                           " & vbNewLine _
    '                                   & "                  , SYS_DEL_FLG                                          " & vbNewLine _
    '                                   & "            ) INKAEDIL                                                   " & vbNewLine _
    '                                   & "        ON INKAEDIL.NRS_BR_CD     = INKAL.NRS_BR_CD                      " & vbNewLine _
    '                                   & "       AND INKAEDIL.INKA_CTL_NO_L = INKAL.INKA_NO_L                      " & vbNewLine _
    '                                   & "       AND INKAEDIL.SYS_DEL_FLG   = '0'                                  " & vbNewLine _
    '                                   & " --運送L                                                                 " & vbNewLine _
    '                                   & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                      " & vbNewLine _
    '                                   & "        ON INKAL.NRS_BR_CD   = UNSOL.NRS_BR_CD                           " & vbNewLine _
    '                                   & "       AND INKAL.INKA_NO_L   = UNSOL.INOUTKA_NO_L                        " & vbNewLine _
    '                                   & "       AND UNSOL.MOTO_DATA_KB = '10'                                     " & vbNewLine _
    '                                   & "       AND UNSOL.SYS_DEL_FLG  = '0'                                      " & vbNewLine _
    '                                   & " --在庫                                                                  " & vbNewLine _
    '                                   & " LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                       " & vbNewLine _
    '                                   & "        ON ZAI.NRS_BR_CD = INKAS.NRS_BR_CD                               " & vbNewLine _
    '                                   & "       AND ZAI.ZAI_REC_NO = INKAS.ZAI_REC_NO                             " & vbNewLine _
    '                                   & "       AND ZAI.SYS_DEL_FLG = '0'                                         " & vbNewLine _
    '                                   & " --届先M（出荷元取得入荷L参照）                                          " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_DEST DESTL                                        " & vbNewLine _
    '                                   & "        ON DESTL.NRS_BR_CD = INKAL.NRS_BR_CD                             " & vbNewLine _
    '                                   & "       AND DESTL.CUST_CD_L = INKAL.CUST_CD_L                             " & vbNewLine _
    '                                   & "       AND DESTL.DEST_CD   = UNSOL.ORIG_CD                               " & vbNewLine _
    '                                   & "       AND DESTL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
    '                                   & " --届先M（出荷元取得EDIL参照）                                           " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_DEST DESTEDI                                      " & vbNewLine _
    '                                   & "        ON DESTEDI.NRS_BR_CD = INKAEDIL.NRS_BR_CD                        " & vbNewLine _
    '                                   & "       AND DESTEDI.CUST_CD_L = INKAEDIL.CUST_CD_L                        " & vbNewLine _
    '                                   & "       AND DESTEDI.DEST_CD   = INKAEDIL.OUTKA_MOTO                       " & vbNewLine _
    '                                   & "       AND DESTEDI.SYS_DEL_FLG = '0'                                     " & vbNewLine _
    '                                   & " --運送会社マスタ                                                        " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_UNSOCO UNSOCO                                     " & vbNewLine _
    '                                   & "        ON UNSOCO.NRS_BR_CD    = UNSOL.NRS_BR_CD                         " & vbNewLine _
    '                                   & "       AND UNSOCO.UNSOCO_CD    = UNSOL.UNSO_CD                           " & vbNewLine _
    '                                   & "       AND UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                        " & vbNewLine _
    '                                   & "       AND UNSOCO.SYS_DEL_FLG = '0'                                      " & vbNewLine _
    '                                   & " --商品マスタ                                                            " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                       " & vbNewLine _
    '                                   & "        ON INKAM.NRS_BR_CD    = GOODS.NRS_BR_CD                          " & vbNewLine _
    '                                   & "       AND INKAM.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                       " & vbNewLine _
    '                                   & " --単価マスタ                                                            " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_TANKA TANKA                                       " & vbNewLine _
    '                                   & "        ON TANKA.NRS_BR_CD  = GOODS.NRS_BR_CD                            " & vbNewLine _
    '                                   & "       AND TANKA.CUST_CD_L  = GOODS.CUST_CD_L                            " & vbNewLine _
    '                                   & "       AND TANKA.CUST_CD_M  = GOODS.CUST_CD_M                            " & vbNewLine _
    '                                   & "       AND TANKA.UP_GP_CD_1 = GOODS.UP_GP_CD_1                           " & vbNewLine _
    '                                   & "       AND TANKA.STR_DATE <= @F_DATE                                     " & vbNewLine _
    '                                   & "       AND TANKA.SYS_DEL_FLG = '0'                                       " & vbNewLine _
    '                                   & " --荷主マスタ                                                            " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_CUST CUST                                         " & vbNewLine _
    '                                   & "        ON CUST.NRS_BR_CD  = GOODS.NRS_BR_CD                             " & vbNewLine _
    '                                   & "       AND CUST.CUST_CD_L  = GOODS.CUST_CD_L                             " & vbNewLine _
    '                                   & "       AND CUST.CUST_CD_M  = GOODS.CUST_CD_M                             " & vbNewLine _
    '                                   & "       AND CUST.CUST_CD_S  = GOODS.CUST_CD_S                             " & vbNewLine _
    '                                   & "       AND CUST.CUST_CD_SS = GOODS.CUST_CD_SS                            " & vbNewLine _
    '                                   & "       AND CUST.SYS_DEL_FLG  = '0'                                       " & vbNewLine _
    '                                   & " --商品状態区分2(外観)                                                   " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..Z_KBN KBN_S006                                      " & vbNewLine _
    '                                   & "        ON KBN_S006.KBN_GROUP_CD = 'S006'                                " & vbNewLine _
    '                                   & "       AND KBN_S006.KBN_CD = ZAI.GOODS_COND_KB_2                         " & vbNewLine _
    '                                   & "       AND KBN_S006.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
    '                                   & " --荷主帳票パターン取得                                                  " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                     " & vbNewLine _
    '                                   & "        ON INKAL.NRS_BR_CD = MCR1.NRS_BR_CD                              " & vbNewLine _
    '                                   & "       AND GOODS.CUST_CD_L = MCR1.CUST_CD_L                              " & vbNewLine _
    '                                   & "       AND GOODS.CUST_CD_M = MCR1.CUST_CD_M                              " & vbNewLine _
    '                                   & "       AND '00' = MCR1.CUST_CD_S                                         " & vbNewLine _
    '                                   & "       AND MCR1.PTN_ID = 'AS'                                            " & vbNewLine _
    '                                   & " --帳票パターン取得                                                      " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_RPT MR1                                           " & vbNewLine _
    '                                   & "        ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                " & vbNewLine _
    '                                   & "       AND MR1.PTN_ID = MCR1.PTN_ID                                      " & vbNewLine _
    '                                   & "       AND MR1.PTN_CD = MCR1.PTN_CD                                      " & vbNewLine _
    '                                   & "       AND MR1.SYS_DEL_FLG = '0'                                         " & vbNewLine _
    '                                   & " --商品Mの荷主での荷主帳票パターン取得                                   " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                     " & vbNewLine _
    '                                   & "        ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                              " & vbNewLine _
    '                                   & "       AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                              " & vbNewLine _
    '                                   & "       AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                              " & vbNewLine _
    '                                   & "       AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                              " & vbNewLine _
    '                                   & "       AND MCR2.PTN_ID = 'AS'                                            " & vbNewLine _
    '                                   & " --帳票パターン取得                                                      " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_RPT MR2                                           " & vbNewLine _
    '                                   & "        ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                " & vbNewLine _
    '                                   & "       AND MR2.PTN_ID = MCR2.PTN_ID                                      " & vbNewLine _
    '                                   & "       AND MR2.PTN_CD = MCR2.PTN_CD                                      " & vbNewLine _
    '                                   & "       AND MR2.SYS_DEL_FLG = '0'                                         " & vbNewLine _
    '                                   & " --存在しない場合の帳票パターン取得                                      " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_RPT MR3                                           " & vbNewLine _
    '                                   & "        ON MR3.NRS_BR_CD = INKAL.NRS_BR_CD                               " & vbNewLine _
    '                                   & "       AND MR3.PTN_ID = 'AS'                                             " & vbNewLine _
    '                                   & "       AND MR3.STANDARD_FLAG = '01'                                      " & vbNewLine _
    '                                   & "       AND MR3.SYS_DEL_FLG = '0'                                         " & vbNewLine _
    '                                   & "                                                                         " & vbNewLine _
    '                                   & " WHERE INKAL.CUST_CD_L = '00145'                                         " & vbNewLine _
    '                                   & "   AND INKAL.CUST_CD_M = '00'                                            " & vbNewLine _
    '                                   & "   AND INKAL.INKA_STATE_KB >= '50'   -- 'N004'                           " & vbNewLine _
    '                                   & "   AND INKAL.HOKAN_STR_DATE BETWEEN @F_DATE                              " & vbNewLine _
    '                                   & "   AND @T_DATE                                                           " & vbNewLine _
    '                                   & "   AND INKAL.NIYAKU_YN = '01'        -- 'U009'                           " & vbNewLine _
    '                                   & "   AND INKAL.SYS_DEL_FLG = '0'                                           " & vbNewLine _
    '                                   & "   AND INKAL.NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
    '                                   & "   AND TANKA.HANDLING_IN > 0                                             " & vbNewLine _
    '                                   & "                                                                         " & vbNewLine _
    '                                   & " GROUP BY                                                                " & vbNewLine _
    '                                   & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                     " & vbNewLine _
    '                                   & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
    '                                   & "    ELSE MR3.RPT_ID END                                                  " & vbNewLine _
    '                                   & "   , INKAL.NRS_BR_CD                                                     " & vbNewLine _
    '                                   & "   , CUST.CUST_CD_L                                                      " & vbNewLine _
    '                                   & "   , CUST.CUST_CD_M                                                      " & vbNewLine _
    '                                   & "   , CUST.CUST_CD_S                                                      " & vbNewLine _
    '                                   & "   , CUST.CUST_CD_SS                                                     " & vbNewLine _
    '                                   & "   , CUST.CUST_NM_L                                                      " & vbNewLine _
    '                                   & "   , CUST.CUST_NM_M                                                      " & vbNewLine _
    '                                   & "   , CUST.CUST_NM_S                                                      " & vbNewLine _
    '                                   & "   , CUST.CUST_NM_SS                                                     " & vbNewLine _
    '                                   & "   , CASE WHEN CUST.NIYAKU_SEIQTO_CD = ''  THEN CUST.NIYAKU_SEIQTO_CD    " & vbNewLine _
    '                                   & "     ELSE CUST.HOKAN_SEIQTO_CD                                           " & vbNewLine _
    '                                   & "     END                                                                 " & vbNewLine _
    '                                   & "   , CASE WHEN INKAEDIL.INKA_CTL_NO_L IS NOT NULL THEN DESTEDI.DEST_NM   " & vbNewLine _
    '                                   & "     ELSE DESTL.DEST_NM                                                  " & vbNewLine _
    '                                   & "     END                                                                 " & vbNewLine _
    '                                   & "   , INKAL.OUTKA_FROM_ORD_NO_L                                           " & vbNewLine _
    '                                   & "   , UNSOCO.UNSOCO_NM                                                    " & vbNewLine _
    '                                   & "   , ZAI.GOODS_CD_NRS                                                    " & vbNewLine _
    '                                   & "   , GOODS.GOODS_CD_CUST                                                 " & vbNewLine _
    '                                   & "   , GOODS.GOODS_NM_1                                                    " & vbNewLine _
    '                                   & "   , INKAL.INKA_NO_L                                                     " & vbNewLine _
    '                                   & "   , INKAL.HOKAN_STR_DATE                                                " & vbNewLine _
    '                                   & "   , INKAS.LOT_NO                                                        " & vbNewLine _
    '                                   & "   , INKAS.SERIAL_NO                                                     " & vbNewLine _
    '                                   & "   , INKAS.TOU_NO                                                        " & vbNewLine _
    '                                   & "   , INKAS.SITU_NO                                                       " & vbNewLine _
    '                                   & "   , INKAS.ZONE_CD                                                       " & vbNewLine _
    '                                   & "   , INKAS.LOCA                                                          " & vbNewLine _
    '                                   & "   , GOODS.PKG_UT                                                        " & vbNewLine _
    '                                   & "   , GOODS.NB_UT                                                         " & vbNewLine _
    '                                   & "   , INKAS.IRIME                                                         " & vbNewLine _
    '                                   & "   , GOODS.STD_IRIME_UT                                                  " & vbNewLine _
    '                                   & "   , ZAI.TAX_KB                                                          " & vbNewLine _
    '                                   & " --項目無し    ,ZAI.INKA_TP                                              " & vbNewLine _
    '                                   & "   , ZAI.LT_DATE                                                         " & vbNewLine _
    '                                   & "   , ZAI.GOODS_CRT_DATE                                                  " & vbNewLine _
    '                                   & "   , ZAI.REMARK_OUT                                                      " & vbNewLine _
    '                                   & "   , ZAI.GOODS_COND_KB_1                                                 " & vbNewLine _
    '                                   & "   , ZAI.GOODS_COND_KB_2                                                 " & vbNewLine _
    '                                   & "   , ZAI.GOODS_COND_KB_3                                                 " & vbNewLine _
    '                                   & "   , ZAI.SPD_KB                                                          " & vbNewLine _
    '                                   & "   , ZAI.OFB_KB                                                          " & vbNewLine _
    '                                   & " --項目無し    ,ZAI.NAIGAI_KB                                            " & vbNewLine _
    '                                   & "   , CASE WHEN KBN_S006.KBN_NM1  IS NULL THEN 'F'                        " & vbNewLine _
    '                                   & "     ELSE KBN_S006.KBN_NM1                                               " & vbNewLine _
    '                                   & "     END                                                                 " & vbNewLine _
    '                                   & "   , GOODS.PKG_NB                                                        " & vbNewLine _
    '                                   & "   , TANKA.HANDLING_IN                                                   " & vbNewLine _
    '                                   & "                                                                         " & vbNewLine _
    '                                   & " HAVING 300 -  ((SUM(INKAS.KONSU) * GOODS.PKG_NB) + SUM(INKAS.HASU)) * TANKA.HANDLING_IN > 0   " & vbNewLine

#End Region


#Region "SELECT句(印刷データ：出荷)"
    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks> 
    Private Const SQL_SELECT_OUTKA As String = " --SQL_SELECT_OUTKA                                                " & vbNewLine _
                                       & " UNION ALL                                                               " & vbNewLine _
                                       & " SELECT                                                                  " & vbNewLine _
                                       & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                     " & vbNewLine _
                                       & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
                                       & "    ELSE MR3.RPT_ID END                 AS RPT_ID                        " & vbNewLine _
                                       & "  , OUTKAL.NRS_BR_CD                    AS  NRS_BR_CD                    " & vbNewLine _
                                       & "  , 'OUT'                               AS INOUT_KB                      " & vbNewLine _
                                       & "  , @F_DATE                             AS F_DATE                        " & vbNewLine _
                                       & "  , @T_DATE                             AS T_DATE                        " & vbNewLine _
                                       & "  , CUST.CUST_CD_L                      AS CUST_CD_L                     " & vbNewLine _
                                       & "  , CUST.CUST_CD_M                      AS CUST_CD_M                     " & vbNewLine _
                                       & "  , CUST.CUST_CD_S                      AS CUST_CD_S                     " & vbNewLine _
                                       & "  , CUST.CUST_CD_SS                     AS CUST_CD_SS                    " & vbNewLine _
                                       & "  , CUST.CUST_NM_L                      AS CUST_NM_L                     " & vbNewLine _
                                       & "  , CUST.CUST_NM_M                      AS CUST_NM_M                     " & vbNewLine _
                                       & "  , CUST.CUST_NM_S                      AS CUST_NM_S                     " & vbNewLine _
                                       & "  , CUST.CUST_NM_SS                     AS CUST_NM_SS                    " & vbNewLine _
                                       & "  , CASE WHEN CUST.NIYAKU_SEIQTO_CD = ''  THEN CUST.NIYAKU_SEIQTO_CD     " & vbNewLine _
                                       & "    ELSE CUST.HOKAN_SEIQTO_CD                                            " & vbNewLine _
                                       & "    END AS NIYAKU_SEIQTO_CD                                              " & vbNewLine _
                                       & "  , CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM                  " & vbNewLine _
                                       & "         WHEN OUTKAL.DEST_KB = '02' THEN OUTKAEDIL.DEST_NM               " & vbNewLine _
                                       & "    ELSE DESTL.DEST_NM                                                   " & vbNewLine _
                                       & "    END                                 AS DEST_NM                       " & vbNewLine _
                                       & "  , OUTKAL.CUST_ORD_NO                  AS CUST_ORD_NO                   " & vbNewLine _
                                       & "  , UNSOCO.UNSOCO_NM                    AS UNSOCO_NM                     " & vbNewLine _
                                       & "  , ZAI.GOODS_CD_NRS                    AS GOODS_CD_NRS                  " & vbNewLine _
                                       & "  , GOODS.GOODS_CD_CUST                 AS GOODS_CD_CUST                 " & vbNewLine _
                                       & "  , GOODS.GOODS_NM_1                    AS GOODS_NM_1                    " & vbNewLine _
                                       & "--  , OUTKAS.INKA_NO_L                    AS INKA_NO_L                     " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB = '03' THEN OUTKAS.INKA_NO_L           " & vbNewLine _
                                       & "          ELSE '' END  AS INKA_NO_L                                         " & vbNewLine _
                                       & "  , OUTKAL.END_DATE                     AS INOUT_DATE                    " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB <> '00' THEN OUTKAS.LOT_NO               " & vbNewLine _
                                       & "          ELSE '' END  AS LOT_NO                                         " & vbNewLine _
                                       & "--  , CASE WHEN CUST.SAITEI_HAN_KB <> '00' THEN OUTKAS.SERIAL_NO            " & vbNewLine _
                                       & "--          ELSE '' END  AS SERIAL_NO                                     " & vbNewLine _
                                       & "  , OUTKAS.SERIAL_NO                    AS SERIAL_NO                     " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN OUTKAS.TOU_NO               " & vbNewLine _
                                       & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN OUTKAS.TOU_NO               " & vbNewLine _
                                       & "         ELSE '' END  AS TOU_NO                                          " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN OUTKAS.SITU_NO              " & vbNewLine _
                                       & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN OUTKAS.SITU_NO              " & vbNewLine _
                                       & "         ELSE '' END  AS SITU_NO                                         " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN OUTKAS.ZONE_CD              " & vbNewLine _
                                       & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN OUTKAS.ZONE_CD              " & vbNewLine _
                                       & "         ELSE '' END  AS ZONE_CD                                         " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN OUTKAS.LOCA                 " & vbNewLine _
                                       & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN OUTKAS.LOCA                 " & vbNewLine _
                                       & "         ELSE '' END  AS LOCA                                            " & vbNewLine _
                                       & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                       & "--         ELSE GOODS.PKG_UT END  AS PKG_UT                                " & vbNewLine _
                                       & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                       & "--         ELSE GOODS.NB_UT END  AS NB_UT                                  " & vbNewLine _
                                       & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                       & "--         ELSE OUTKAS.IRIME END  AS IRIME                                 " & vbNewLine _
                                       & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                       & "--         ELSE GOODS.STD_IRIME_UT END  AS STD_IRIME_UT                    " & vbNewLine _
                                       & "--  , OUTKAS.LOT_NO                       AS LOT_NO                        " & vbNewLine _
                                       & "--  , OUTKAS.SERIAL_NO                    AS SERIAL_NO                     " & vbNewLine _
                                       & "--  , OUTKAS.TOU_NO                       AS TOU_NO                        " & vbNewLine _
                                       & "--  , OUTKAS.SITU_NO                      AS SITU_NO                       " & vbNewLine _
                                       & "--  , OUTKAS.ZONE_CD                      AS ZONE_CD                       " & vbNewLine _
                                       & "--  , OUTKAS.LOCA                         AS LOCA                          " & vbNewLine _
                                       & "  , GOODS.PKG_UT                        AS PKG_UT                        " & vbNewLine _
                                       & "  , GOODS.NB_UT                         AS NB_UT                         " & vbNewLine _
                                       & "--  , OUTKAS.IRIME                        AS IRIME                         " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB <> '00' THEN OUTKAS.IRIME             " & vbNewLine _
                                       & "         ELSE 0 END  AS IRIME                                           " & vbNewLine _
                                       & "  , GOODS.STD_IRIME_UT                  AS STD_IRIME_UT                  " & vbNewLine _
                                       & "  , ZAI.TAX_KB                          AS TAX_KB                        " & vbNewLine _
                                       & "--項目無し    ,ZAI.INKA_TP            AS INKA_TP                         " & vbNewLine _
                                       & "  , ZAI.LT_DATE                         AS LT_DATE                       " & vbNewLine _
                                       & "  , ZAI.GOODS_CRT_DATE                  AS GOODS_CRT_DATE                " & vbNewLine _
                                       & "  , ZAI.REMARK_OUT                      AS REMARK_OUT                    " & vbNewLine _
                                       & "  , ZAI.GOODS_COND_KB_1                 AS GOODS_COND_KB_1               " & vbNewLine _
                                       & "  , ZAI.GOODS_COND_KB_2                 AS GOODS_COND_KB_2               " & vbNewLine _
                                       & "  , ZAI.GOODS_COND_KB_3                 AS GOODS_COND_KB_3               " & vbNewLine _
                                       & "  , ZAI.SPD_KB                          AS SPD_KB                        " & vbNewLine _
                                       & "  , ZAI.OFB_KB                          AS OFB_KB                        " & vbNewLine _
                                       & "--項目無し    ,ZAI.NAIGAI_KB          AS NAIGAI_KB                       " & vbNewLine _
                                       & "  , CASE WHEN KBN_S006.KBN_NM1  IS NULL THEN 'F'                           " & vbNewLine _
                                       & "    ELSE KBN_S006.KBN_NM1                                                  " & vbNewLine _
                                       & "    END                                                   AS ZAIKO_KB      " & vbNewLine _
                                       & "  , 0                                                     AS IN_NB         " & vbNewLine _
                                       & "  , SUM(OUTKAS.ALCTD_NB)                                  AS OUT_NB        " & vbNewLine _
                                       & "  , TANKA.HANDLING_OUT                                    AS NIYAKU_TANKA  " & vbNewLine _
                                       & "  , TANKA.MINI_TEKI_OUT_AMO                               AS NIYAKU_HOSHO  " & vbNewLine _
                                       & "  , (SUM(OUTKAS.ALCTD_NB)) * TANKA.HANDLING_OUT		    AS N_AM_TTL      " & vbNewLine _
                                       & "  , TANKA.MINI_TEKI_OUT_AMO                                                " & vbNewLine _
                                       & "        - SUM(OUTKAS.ALCTD_NB) * TANKA.HANDLING_OUT       AS N_AM_SAGAKU   " & vbNewLine _
                                       & "                                                                         " & vbNewLine _
                                       & " FROM $LM_TRN$..C_OUTKA_L OUTKAL                                         " & vbNewLine _
                                       & "                                                                         " & vbNewLine _
                                       & " --出荷M                                                                 " & vbNewLine _
                                       & " LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM                                    " & vbNewLine _
                                       & "        ON OUTKAL.NRS_BR_CD = OUTKAM.NRS_BR_CD                           " & vbNewLine _
                                       & "       AND OUTKAL.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                         " & vbNewLine _
                                       & "       AND OUTKAM.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
                                       & " --出荷S                                                                 " & vbNewLine _
                                       & " LEFT JOIN $LM_TRN$..C_OUTKA_S OUTKAS                                    " & vbNewLine _
                                       & "        ON OUTKAM.NRS_BR_CD = OUTKAS.NRS_BR_CD                           " & vbNewLine _
                                       & "       AND OUTKAM.OUTKA_NO_L = OUTKAS.OUTKA_NO_L                         " & vbNewLine _
                                       & "       AND OUTKAM.OUTKA_NO_M = OUTKAS.OUTKA_NO_M                         " & vbNewLine _
                                       & "       AND OUTKAS.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
                                       & " --出荷EDIL                                                              " & vbNewLine _
                                       & " LEFT JOIN (                                                             " & vbNewLine _
                                       & "             SELECT DISTINCT                                             " & vbNewLine _
                                       & "                    NRS_BR_CD                                            " & vbNewLine _
                                       & "                  , OUTKA_CTL_NO                                         " & vbNewLine _
                                       & "                  , SHIP_NM_L                                            " & vbNewLine _
                                       & "                  , MIN(DEST_CD)     AS DEST_CD                          " & vbNewLine _
                                       & "                  , MIN(DEST_NM)     AS DEST_NM                          " & vbNewLine _
                                       & "                  , MIN(DEST_AD_1)   AS DEST_AD_1                        " & vbNewLine _
                                       & "                  , MIN(DEST_AD_2)   AS DEST_AD_2                        " & vbNewLine _
                                       & "                  , SYS_DEL_FLG                                          " & vbNewLine _
                                       & "             FROM   $LM_TRN$..H_OUTKAEDI_L                               " & vbNewLine _
                                       & "             GROUP BY                                                    " & vbNewLine _
                                       & "                   NRS_BR_CD                                             " & vbNewLine _
                                       & "                 , OUTKA_CTL_NO                                          " & vbNewLine _
                                       & "                 , SHIP_NM_L                                             " & vbNewLine _
                                       & "                 , SYS_DEL_FLG                                           " & vbNewLine _
                                       & "           ) OUTKAEDIL                                                   " & vbNewLine _
                                       & "        ON OUTKAEDIL.NRS_BR_CD = OUTKAL.NRS_BR_CD                        " & vbNewLine _
                                       & "       AND OUTKAEDIL.OUTKA_CTL_NO = OUTKAL.OUTKA_NO_L                    " & vbNewLine _
                                       & " --運送L                                                                 " & vbNewLine _
                                       & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                      " & vbNewLine _
                                       & "        ON OUTKAL.NRS_BR_CD   = UNSOL.NRS_BR_CD                          " & vbNewLine _
                                       & "       AND OUTKAL.OUTKA_NO_L   = UNSOL.INOUTKA_NO_L                      " & vbNewLine _
                                       & "       AND UNSOL.MOTO_DATA_KB = '20'                                     " & vbNewLine _
                                       & "       AND UNSOL.SYS_DEL_FLG  = '0'                                      " & vbNewLine _
                                       & " --在庫                                                                  " & vbNewLine _
                                       & " LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                       " & vbNewLine _
                                       & "        ON ZAI.NRS_BR_CD = OUTKAS.NRS_BR_CD                              " & vbNewLine _
                                       & "       AND ZAI.ZAI_REC_NO = OUTKAS.ZAI_REC_NO                            " & vbNewLine _
                                       & "       AND ZAI.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                       & " --届先M（出荷元取得出荷L参照）                                          " & vbNewLine _
                                       & " LEFT JOIN $LM_MST$..M_DEST DESTL                                        " & vbNewLine _
                                       & "        ON DESTL.NRS_BR_CD = OUTKAL.NRS_BR_CD                            " & vbNewLine _
                                       & "       AND DESTL.CUST_CD_L = OUTKAL.CUST_CD_L                            " & vbNewLine _
                                       & "       AND DESTL.DEST_CD   = UNSOL.DEST_CD                               " & vbNewLine _
                                       & "       AND DESTL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                       & " --運送会社マスタ                                                        " & vbNewLine _
                                       & " LEFT JOIN $LM_MST$..M_UNSOCO UNSOCO                                     " & vbNewLine _
                                       & "        ON UNSOCO.NRS_BR_CD    = UNSOL.NRS_BR_CD                         " & vbNewLine _
                                       & "       AND UNSOCO.UNSOCO_CD    = UNSOL.UNSO_CD                           " & vbNewLine _
                                       & "       AND UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                        " & vbNewLine _
                                       & " --商品マスタ                                                            " & vbNewLine _
                                       & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                       " & vbNewLine _
                                       & "        ON OUTKAM.NRS_BR_CD    = GOODS.NRS_BR_CD                         " & vbNewLine _
                                       & "       AND OUTKAM.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                      " & vbNewLine _
                                       & "       AND GOODS.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                       & " --単価マスタ                                                            " & vbNewLine _
                                       & " --UPD START 2023/04/14 過請求改修                                       " & vbNewLine _
                                       & " --LEFT JOIN $LM_MST$..M_TANKA TANKA                                     " & vbNewLine _
                                       & " --       ON TANKA.NRS_BR_CD  = GOODS.NRS_BR_CD                          " & vbNewLine _
                                       & " --      AND TANKA.CUST_CD_L  = GOODS.CUST_CD_L                          " & vbNewLine _
                                       & " --      AND TANKA.CUST_CD_M  = GOODS.CUST_CD_M                          " & vbNewLine _
                                       & " --      AND TANKA.UP_GP_CD_1 = GOODS.UP_GP_CD_1                         " & vbNewLine _
                                       & " --      AND TANKA.STR_DATE <= @F_DATE                                   " & vbNewLine _
                                       & " --      AND TANKA.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                       & " LEFT JOIN (                                                             " & vbNewLine _
                                       & "        SELECT *                                                         " & vbNewLine _
                                       & "             , ROW_NUMBER() OVER (                                       " & vbNewLine _
                                       & "                PARTITION BY NRS_BR_CD, CUST_CD_L, CUST_CD_M, UP_GP_CD_1 " & vbNewLine _
                                       & "                ORDER BY STR_DATE DESC) AS NUM                           " & vbNewLine _
                                       & "          FROM $LM_MST$..M_TANKA                                         " & vbNewLine _
                                       & "          WHERE SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                       & "            AND STR_DATE <= @F_DATE                                      " & vbNewLine _
                                       & "  ) TANKA                                                                " & vbNewLine _
                                       & "        ON TANKA.NRS_BR_CD  = GOODS.NRS_BR_CD                            " & vbNewLine _
                                       & "       AND TANKA.CUST_CD_L  = GOODS.CUST_CD_L                            " & vbNewLine _
                                       & "       AND TANKA.CUST_CD_M  = GOODS.CUST_CD_M                            " & vbNewLine _
                                       & "       AND TANKA.UP_GP_CD_1 = GOODS.UP_GP_CD_1                           " & vbNewLine _
                                       & "       AND TANKA.NUM = 1                                                 " & vbNewLine _
                                       & " --UPD END 2023/04/14 過請求改修                                         " & vbNewLine _
                                       & " --荷主マスタ                                                            " & vbNewLine _
                                       & " LEFT JOIN (                                                             " & vbNewLine _
                                       & "        SELECT                                                           " & vbNewLine _
                                       & "              NRS_BR_CD                                                  " & vbNewLine _
                                       & "            , CUST_CD_L                                                  " & vbNewLine _
                                       & "            , CUST_CD_M                                                  " & vbNewLine _
                                       & "            , CUST_CD_S                                                  " & vbNewLine _
                                       & "            , CUST_CD_SS                                                 " & vbNewLine _
                                       & "            , CUST_NM_L                                                  " & vbNewLine _
                                       & "            , CUST_NM_M                                                  " & vbNewLine _
                                       & "            , CUST_NM_S                                                  " & vbNewLine _
                                       & "            , CUST_NM_SS                                                 " & vbNewLine _
                                       & "            , NIYAKU_SEIQTO_CD                                           " & vbNewLine _
                                       & "            , HOKAN_SEIQTO_CD                                            " & vbNewLine _
                                       & "            , '03' AS SAITEI_HAN_KB -- マスタ値を参照せず常に固定値とする" & vbNewLine _
                                       & "            , SYS_DEL_FLG                                                " & vbNewLine _
                                       & "        FROM                                                             " & vbNewLine _
                                       & "            $LM_MST$..M_CUST                                             " & vbNewLine _
                                       & "  ) CUST                                                                 " & vbNewLine _
                                       & "        ON CUST.NRS_BR_CD  = GOODS.NRS_BR_CD                             " & vbNewLine _
                                       & "       AND CUST.CUST_CD_L  = GOODS.CUST_CD_L                             " & vbNewLine _
                                       & "       AND CUST.CUST_CD_M  = GOODS.CUST_CD_M                             " & vbNewLine _
                                       & "       AND CUST.CUST_CD_S  = GOODS.CUST_CD_S                             " & vbNewLine _
                                       & "       AND CUST.CUST_CD_SS = GOODS.CUST_CD_SS                            " & vbNewLine _
                                       & "       AND CUST.SYS_DEL_FLG  = '0'                                       " & vbNewLine _
                                       & " --商品状態区分2(外観)                                                   " & vbNewLine _
                                       & " LEFT JOIN $LM_MST$..Z_KBN KBN_S006                                      " & vbNewLine _
                                       & "        ON KBN_S006.KBN_GROUP_CD = 'S006'                                " & vbNewLine _
                                       & "       AND KBN_S006.KBN_CD = ZAI.GOODS_COND_KB_2                         " & vbNewLine _
                                       & "       AND KBN_S006.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                       & " --荷主帳票パターン取得                                                  " & vbNewLine _
                                       & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                     " & vbNewLine _
                                       & "        ON OUTKAL.NRS_BR_CD = MCR1.NRS_BR_CD                             " & vbNewLine _
                                       & "       AND GOODS.CUST_CD_L = MCR1.CUST_CD_L                              " & vbNewLine _
                                       & "       AND GOODS.CUST_CD_M = MCR1.CUST_CD_M                              " & vbNewLine _
                                       & "       AND '00' = MCR1.CUST_CD_S                                         " & vbNewLine _
                                       & "       AND MCR1.PTN_ID = 'AS'                                            " & vbNewLine _
                                       & " --帳票パターン取得                                                      " & vbNewLine _
                                       & " LEFT JOIN $LM_MST$..M_RPT MR1                                           " & vbNewLine _
                                       & "        ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                " & vbNewLine _
                                       & "       AND MR1.PTN_ID = MCR1.PTN_ID                                      " & vbNewLine _
                                       & "       AND MR1.PTN_CD = MCR1.PTN_CD                                      " & vbNewLine _
                                       & "       AND MR1.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                       & " --商品Mの荷主での荷主帳票パターン取得                                   " & vbNewLine _
                                       & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                     " & vbNewLine _
                                       & "        ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                              " & vbNewLine _
                                       & "       AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                              " & vbNewLine _
                                       & "       AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                              " & vbNewLine _
                                       & "       AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                              " & vbNewLine _
                                       & "       AND MCR2.PTN_ID = 'AS'                                            " & vbNewLine _
                                       & " --帳票パターン取得                                                      " & vbNewLine _
                                       & " LEFT JOIN $LM_MST$..M_RPT MR2                                           " & vbNewLine _
                                       & "        ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                " & vbNewLine _
                                       & "       AND MR2.PTN_ID = MCR2.PTN_ID                                      " & vbNewLine _
                                       & "       AND MR2.PTN_CD = MCR2.PTN_CD                                      " & vbNewLine _
                                       & "       AND MR2.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                       & " --存在しない場合の帳票パターン取得                                      " & vbNewLine _
                                       & " LEFT JOIN $LM_MST$..M_RPT MR3                                           " & vbNewLine _
                                       & "        ON MR3.NRS_BR_CD = OUTKAL.NRS_BR_CD                              " & vbNewLine _
                                       & "       AND MR3.PTN_ID = 'AS'                                             " & vbNewLine _
                                       & "       AND MR3.STANDARD_FLAG = '01'                                      " & vbNewLine _
                                       & "       AND MR3.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                       & "                                                                         " & vbNewLine _
                                       & " WHERE OUTKAL.CUST_CD_L = '00145'                                        " & vbNewLine _
                                       & "   AND OUTKAL.CUST_CD_M = '00'                                           " & vbNewLine _
                                       & "   AND OUTKAL.OUTKA_STATE_KB >= '60'   -- 'S010'                         " & vbNewLine _
                                       & "   AND OUTKAL.END_DATE BETWEEN @F_DATE                                   " & vbNewLine _
                                       & "   AND @T_DATE                                                           " & vbNewLine _
                                       & "   AND OUTKAL.NIYAKU_YN = '01'        -- 'U009'                          " & vbNewLine _
                                       & "   AND OUTKAL.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                       & "   AND OUTKAL.NRS_BR_CD = @NRS_BR_CD                                     " & vbNewLine _
                                       & "   AND TANKA.HANDLING_OUT > 0                                            " & vbNewLine _
                                       & "                                                                         " & vbNewLine _
                                       & " GROUP BY                                                                " & vbNewLine _
                                       & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                     " & vbNewLine _
                                       & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
                                       & "    ELSE MR3.RPT_ID END                                                  " & vbNewLine _
                                       & "   , OUTKAL.NRS_BR_CD                                                    " & vbNewLine _
                                       & "   , CUST.CUST_CD_L                                                      " & vbNewLine _
                                       & "   , CUST.CUST_CD_M                                                      " & vbNewLine _
                                       & "   , CUST.CUST_CD_S                                                      " & vbNewLine _
                                       & "   , CUST.CUST_CD_SS                                                     " & vbNewLine _
                                       & "   , CUST.CUST_NM_L                                                      " & vbNewLine _
                                       & "   , CUST.CUST_NM_M                                                      " & vbNewLine _
                                       & "   , CUST.CUST_NM_S                                                      " & vbNewLine _
                                       & "   , CUST.CUST_NM_SS                                                     " & vbNewLine _
                                       & "   , CASE WHEN CUST.NIYAKU_SEIQTO_CD = ''  THEN CUST.NIYAKU_SEIQTO_CD    " & vbNewLine _
                                       & "     ELSE CUST.HOKAN_SEIQTO_CD                                           " & vbNewLine _
                                       & "     END                                                                 " & vbNewLine _
                                       & "   , CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM                 " & vbNewLine _
                                       & "          WHEN OUTKAL.DEST_KB = '02' THEN OUTKAEDIL.DEST_NM              " & vbNewLine _
                                       & "     ELSE DESTL.DEST_NM                                                  " & vbNewLine _
                                       & "     END                                                                 " & vbNewLine _
                                       & "  , OUTKAL.CUST_ORD_NO                                                   " & vbNewLine _
                                       & "  , UNSOCO.UNSOCO_NM                                                     " & vbNewLine _
                                       & "  , ZAI.GOODS_CD_NRS                                                     " & vbNewLine _
                                       & "  , GOODS.GOODS_CD_CUST                                                  " & vbNewLine _
                                       & "  , GOODS.GOODS_NM_1                                                     " & vbNewLine _
                                       & "--  , OUTKAS.INKA_NO_L                                                     " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB = '03' THEN OUTKAS.INKA_NO_L            " & vbNewLine _
                                       & "          ELSE '' END                                                    " & vbNewLine _
                                       & "  , OUTKAL.END_DATE                                                      " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB <> '00' THEN OUTKAS.LOT_NO               " & vbNewLine _
                                       & "          ELSE '' END                                                    " & vbNewLine _
                                       & "--  , CASE WHEN CUST.SAITEI_HAN_KB <> '00' THEN OUTKAS.SERIAL_NO            " & vbNewLine _
                                       & "--          ELSE '' END                                                   " & vbNewLine _
                                       & "  , OUTKAS.SERIAL_NO                                                     " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN OUTKAS.TOU_NO               " & vbNewLine _
                                       & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN OUTKAS.TOU_NO               " & vbNewLine _
                                       & "         ELSE '' END                                                     " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN OUTKAS.SITU_NO              " & vbNewLine _
                                       & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN OUTKAS.SITU_NO              " & vbNewLine _
                                       & "         ELSE '' END                                                     " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN OUTKAS.ZONE_CD              " & vbNewLine _
                                       & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN OUTKAS.ZONE_CD              " & vbNewLine _
                                       & "         ELSE '' END                                                     " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB = '02' THEN OUTKAS.LOCA                 " & vbNewLine _
                                       & "         WHEN CUST.SAITEI_HAN_KB = '03' THEN OUTKAS.LOCA                 " & vbNewLine _
                                       & "         ELSE '' END                                                     " & vbNewLine _
                                       & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                       & "--         ELSE GOODS.PKG_UT END                                           " & vbNewLine _
                                       & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                       & "--         ELSE GOODS.NB_UT END                                            " & vbNewLine _
                                       & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                       & "--         ELSE OUTKAS.IRIME END                                           " & vbNewLine _
                                       & "--  , CASE WHEN CUST.SAITEI_HAN_KB = '00' THEN ''                          " & vbNewLine _
                                       & "--         ELSE GOODS.STD_IRIME_UT END                                     " & vbNewLine _
                                       & "--  , OUTKAS.LOT_NO                                                        " & vbNewLine _
                                       & "--  , OUTKAS.SERIAL_NO                                                     " & vbNewLine _
                                       & "--  , OUTKAS.TOU_NO                                                        " & vbNewLine _
                                       & "--  , OUTKAS.SITU_NO                                                       " & vbNewLine _
                                       & "--  , OUTKAS.ZONE_CD                                                       " & vbNewLine _
                                       & "--  , OUTKAS.LOCA                                                          " & vbNewLine _
                                       & "  , GOODS.PKG_UT                                                         " & vbNewLine _
                                       & "  , GOODS.NB_UT                                                          " & vbNewLine _
                                       & "--  , OUTKAS.IRIME                                                         " & vbNewLine _
                                       & "  , CASE WHEN CUST.SAITEI_HAN_KB <> '00' THEN OUTKAS.IRIME                 " & vbNewLine _
                                       & "         ELSE 0 END                                                       " & vbNewLine _
                                       & "  , GOODS.STD_IRIME_UT                                                   " & vbNewLine _
                                       & "  , ZAI.TAX_KB                                                           " & vbNewLine _
                                       & "--項目無し    ,ZAI.INKA_TP                                               " & vbNewLine _
                                       & "  , ZAI.LT_DATE                                                          " & vbNewLine _
                                       & "  , ZAI.GOODS_CRT_DATE                                                   " & vbNewLine _
                                       & "  , ZAI.REMARK_OUT                                                       " & vbNewLine _
                                       & "  , ZAI.GOODS_COND_KB_1                                                  " & vbNewLine _
                                       & "  , ZAI.GOODS_COND_KB_2                                                  " & vbNewLine _
                                       & "  , ZAI.GOODS_COND_KB_3                                                  " & vbNewLine _
                                       & "  , ZAI.SPD_KB                                                           " & vbNewLine _
                                       & "  , ZAI.OFB_KB                                                           " & vbNewLine _
                                       & "--項目無し    ,ZAI.NAIGAI_KB                                             " & vbNewLine _
                                       & "  , CASE WHEN KBN_S006.KBN_NM1  IS NULL THEN 'F'                         " & vbNewLine _
                                       & "    ELSE KBN_S006.KBN_NM1                                                " & vbNewLine _
                                       & "    END                                                                  " & vbNewLine _
                                       & "  , TANKA.HANDLING_OUT                                                   " & vbNewLine _
                                       & "  , TANKA.MINI_TEKI_OUT_AMO                                              " & vbNewLine _
                                       & "                                                                         " & vbNewLine _
                                       & "HAVING TANKA.MINI_TEKI_OUT_AMO                                           " & vbNewLine _
                                       & "           - (SUM(ISNULL(OUTKAS.ALCTD_NB , 0)) * TANKA.HANDLING_OUT) > 0 " & vbNewLine _
                                       & "                                                                         " & vbNewLine _
                                       & ") MAIN                                                                   " & vbNewLine
    '↑要望番号1689 UMANO修正

    'Private Const SQL_SELECT_OUTKA As String = " --SQL_SELECT_OUTKA                                                " & vbNewLine _
    '                                   & " UNION ALL                                                               " & vbNewLine _
    '                                   & " SELECT                                                                  " & vbNewLine _
    '                                   & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                     " & vbNewLine _
    '                                   & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
    '                                   & "    ELSE MR3.RPT_ID END                 AS RPT_ID                        " & vbNewLine _
    '                                   & "  , OUTKAL.NRS_BR_CD                    AS  NRS_BR_CD                    " & vbNewLine _
    '                                   & "  , 'OUT'                               AS INOUT_KB                      " & vbNewLine _
    '                                   & "  , @F_DATE                             AS F_DATE                        " & vbNewLine _
    '                                   & "  , @T_DATE                             AS T_DATE                        " & vbNewLine _
    '                                   & "  , CUST.CUST_CD_L                      AS CUST_CD_L                     " & vbNewLine _
    '                                   & "  , CUST.CUST_CD_M                      AS CUST_CD_M                     " & vbNewLine _
    '                                   & "  , CUST.CUST_CD_S                      AS CUST_CD_S                     " & vbNewLine _
    '                                   & "  , CUST.CUST_CD_SS                     AS CUST_CD_SS                    " & vbNewLine _
    '                                   & "  , CUST.CUST_NM_L                      AS CUST_NM_L                     " & vbNewLine _
    '                                   & "  , CUST.CUST_NM_M                      AS CUST_NM_M                     " & vbNewLine _
    '                                   & "  , CUST.CUST_NM_S                      AS CUST_NM_S                     " & vbNewLine _
    '                                   & "  , CUST.CUST_NM_SS                     AS CUST_NM_SS                    " & vbNewLine _
    '                                   & "  , CASE WHEN CUST.NIYAKU_SEIQTO_CD = ''  THEN CUST.NIYAKU_SEIQTO_CD     " & vbNewLine _
    '                                   & "    ELSE CUST.HOKAN_SEIQTO_CD                                            " & vbNewLine _
    '                                   & "    END AS NIYAKU_SEIQTO_CD                                              " & vbNewLine _
    '                                   & "  , CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM                  " & vbNewLine _
    '                                   & "         WHEN OUTKAL.DEST_KB = '02' THEN OUTKAEDIL.DEST_NM               " & vbNewLine _
    '                                   & "    ELSE DESTL.DEST_NM                                                   " & vbNewLine _
    '                                   & "    END                                 AS DEST_NM                       " & vbNewLine _
    '                                   & "  , OUTKAL.CUST_ORD_NO                  AS CUST_ORD_NO                   " & vbNewLine _
    '                                   & "  , UNSOCO.UNSOCO_NM                    AS UNSOCO_NM                     " & vbNewLine _
    '                                   & "  , ZAI.GOODS_CD_NRS                    AS GOODS_CD_NRS                  " & vbNewLine _
    '                                   & "  , GOODS.GOODS_CD_CUST                 AS GOODS_CD_CUST                 " & vbNewLine _
    '                                   & "  , GOODS.GOODS_NM_1                    AS GOODS_NM_1                    " & vbNewLine _
    '                                   & "  , OUTKAS.INKA_NO_L                    AS INKA_NO_L                     " & vbNewLine _
    '                                   & "  , OUTKAL.END_DATE                     AS INOUT_DATE                    " & vbNewLine _
    '                                   & "  , OUTKAS.LOT_NO                       AS LOT_NO                        " & vbNewLine _
    '                                   & "  , OUTKAS.SERIAL_NO                    AS SERIAL_NO                     " & vbNewLine _
    '                                   & "  , OUTKAS.TOU_NO                       AS TOU_NO                        " & vbNewLine _
    '                                   & "  , OUTKAS.SITU_NO                      AS SITU_NO                       " & vbNewLine _
    '                                   & "  , OUTKAS.ZONE_CD                      AS ZONE_CD                       " & vbNewLine _
    '                                   & "  , OUTKAS.LOCA                         AS LOCA                          " & vbNewLine _
    '                                   & "  , GOODS.PKG_UT                        AS PKG_UT                        " & vbNewLine _
    '                                   & "  , GOODS.NB_UT                         AS NB_UT                         " & vbNewLine _
    '                                   & "  , OUTKAS.IRIME                        AS IRIME                         " & vbNewLine _
    '                                   & "  , GOODS.STD_IRIME_UT                  AS STD_IRIME_UT                  " & vbNewLine _
    '                                   & "  , ZAI.TAX_KB                          AS TAX_KB                        " & vbNewLine _
    '                                   & "--項目無し    ,ZAI.INKA_TP            AS INKA_TP                         " & vbNewLine _
    '                                   & "  , ZAI.LT_DATE                         AS LT_DATE                       " & vbNewLine _
    '                                   & "  , ZAI.GOODS_CRT_DATE                  AS GOODS_CRT_DATE                " & vbNewLine _
    '                                   & "  , ZAI.REMARK_OUT                      AS REMARK_OUT                    " & vbNewLine _
    '                                   & "  , ZAI.GOODS_COND_KB_1                 AS GOODS_COND_KB_1               " & vbNewLine _
    '                                   & "  , ZAI.GOODS_COND_KB_2                 AS GOODS_COND_KB_2               " & vbNewLine _
    '                                   & "  , ZAI.GOODS_COND_KB_3                 AS GOODS_COND_KB_3               " & vbNewLine _
    '                                   & "  , ZAI.SPD_KB                          AS SPD_KB                        " & vbNewLine _
    '                                   & "  , ZAI.OFB_KB                          AS OFB_KB                        " & vbNewLine _
    '                                   & "--項目無し    ,ZAI.NAIGAI_KB          AS NAIGAI_KB                       " & vbNewLine _
    '                                   & "  , CASE WHEN KBN_S006.KBN_NM1  IS NULL THEN 'F'                           " & vbNewLine _
    '                                   & "    ELSE KBN_S006.KBN_NM1                                                  " & vbNewLine _
    '                                   & "    END                                                   AS ZAIKO_KB      " & vbNewLine _
    '                                   & "  , 0                                                     AS IN_NB         " & vbNewLine _
    '                                   & "  , SUM(OUTKAS.ALCTD_NB)                                  AS OUT_NB        " & vbNewLine _
    '                                   & "  , TANKA.HANDLING_OUT                                    AS NIYAKU_TANKA  " & vbNewLine _
    '                                   & "  , 300                                                   AS NIYAKU_HOSHO  " & vbNewLine _
    '                                   & "  , (SUM(OUTKAS.ALCTD_NB)) * TANKA.HANDLING_OUT		    AS N_AM_TTL      " & vbNewLine _
    '                                   & "  , 300 - SUM(OUTKAS.ALCTD_NB) * TANKA.HANDLING_OUT       AS N_AM_SAGAKU   " & vbNewLine _
    '                                   & "                                                                         " & vbNewLine _
    '                                   & " FROM $LM_TRN$..C_OUTKA_L OUTKAL                                         " & vbNewLine _
    '                                   & "                                                                         " & vbNewLine _
    '                                   & " --出荷M                                                                 " & vbNewLine _
    '                                   & " LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM                                    " & vbNewLine _
    '                                   & "        ON OUTKAL.NRS_BR_CD = OUTKAM.NRS_BR_CD                           " & vbNewLine _
    '                                   & "       AND OUTKAL.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                         " & vbNewLine _
    '                                   & "       AND OUTKAM.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
    '                                   & " --出荷S                                                                 " & vbNewLine _
    '                                   & " LEFT JOIN $LM_TRN$..C_OUTKA_S OUTKAS                                    " & vbNewLine _
    '                                   & "        ON OUTKAM.NRS_BR_CD = OUTKAS.NRS_BR_CD                           " & vbNewLine _
    '                                   & "       AND OUTKAM.OUTKA_NO_L = OUTKAS.OUTKA_NO_L                         " & vbNewLine _
    '                                   & "       AND OUTKAM.OUTKA_NO_M = OUTKAS.OUTKA_NO_M                         " & vbNewLine _
    '                                   & "       AND OUTKAS.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
    '                                   & " --出荷EDIL                                                              " & vbNewLine _
    '                                   & " LEFT JOIN (                                                             " & vbNewLine _
    '                                   & "             SELECT DISTINCT                                             " & vbNewLine _
    '                                   & "                    NRS_BR_CD                                            " & vbNewLine _
    '                                   & "                  , OUTKA_CTL_NO                                         " & vbNewLine _
    '                                   & "                  , SHIP_NM_L                                            " & vbNewLine _
    '                                   & "                  , MIN(DEST_CD)     AS DEST_CD                          " & vbNewLine _
    '                                   & "                  , MIN(DEST_NM)     AS DEST_NM                          " & vbNewLine _
    '                                   & "                  , MIN(DEST_AD_1)   AS DEST_AD_1                        " & vbNewLine _
    '                                   & "                  , MIN(DEST_AD_2)   AS DEST_AD_2                        " & vbNewLine _
    '                                   & "                  , SYS_DEL_FLG                                          " & vbNewLine _
    '                                   & "             FROM   $LM_TRN$..H_OUTKAEDI_L                               " & vbNewLine _
    '                                   & "             GROUP BY                                                    " & vbNewLine _
    '                                   & "                   NRS_BR_CD                                             " & vbNewLine _
    '                                   & "                 , OUTKA_CTL_NO                                          " & vbNewLine _
    '                                   & "                 , SHIP_NM_L                                             " & vbNewLine _
    '                                   & "                 , SYS_DEL_FLG                                           " & vbNewLine _
    '                                   & "           ) OUTKAEDIL                                                   " & vbNewLine _
    '                                   & "        ON OUTKAEDIL.NRS_BR_CD = OUTKAL.NRS_BR_CD                        " & vbNewLine _
    '                                   & "       AND OUTKAEDIL.OUTKA_CTL_NO = OUTKAL.OUTKA_NO_L                    " & vbNewLine _
    '                                   & " --運送L                                                                 " & vbNewLine _
    '                                   & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                      " & vbNewLine _
    '                                   & "        ON OUTKAL.NRS_BR_CD   = UNSOL.NRS_BR_CD                          " & vbNewLine _
    '                                   & "       AND OUTKAL.OUTKA_NO_L   = UNSOL.INOUTKA_NO_L                      " & vbNewLine _
    '                                   & "       AND UNSOL.MOTO_DATA_KB = '20'                                     " & vbNewLine _
    '                                   & "       AND UNSOL.SYS_DEL_FLG  = '0'                                      " & vbNewLine _
    '                                   & " --在庫                                                                  " & vbNewLine _
    '                                   & " LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                       " & vbNewLine _
    '                                   & "        ON ZAI.NRS_BR_CD = OUTKAS.NRS_BR_CD                              " & vbNewLine _
    '                                   & "       AND ZAI.ZAI_REC_NO = OUTKAS.ZAI_REC_NO                            " & vbNewLine _
    '                                   & "       AND ZAI.SYS_DEL_FLG = '0'                                         " & vbNewLine _
    '                                   & " --届先M（出荷元取得出荷L参照）                                          " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_DEST DESTL                                        " & vbNewLine _
    '                                   & "        ON DESTL.NRS_BR_CD = OUTKAL.NRS_BR_CD                            " & vbNewLine _
    '                                   & "       AND DESTL.CUST_CD_L = OUTKAL.CUST_CD_L                            " & vbNewLine _
    '                                   & "       AND DESTL.DEST_CD   = UNSOL.DEST_CD                               " & vbNewLine _
    '                                   & "       AND DESTL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
    '                                   & " --運送会社マスタ                                                        " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_UNSOCO UNSOCO                                     " & vbNewLine _
    '                                   & "        ON UNSOCO.NRS_BR_CD    = UNSOL.NRS_BR_CD                         " & vbNewLine _
    '                                   & "       AND UNSOCO.UNSOCO_CD    = UNSOL.UNSO_CD                           " & vbNewLine _
    '                                   & "       AND UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                        " & vbNewLine _
    '                                   & " --商品マスタ                                                            " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                       " & vbNewLine _
    '                                   & "        ON OUTKAM.NRS_BR_CD    = GOODS.NRS_BR_CD                         " & vbNewLine _
    '                                   & "       AND OUTKAM.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                      " & vbNewLine _
    '                                   & "       AND GOODS.SYS_DEL_FLG = '0'                                       " & vbNewLine _
    '                                   & " --単価マスタ                                                            " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_TANKA TANKA                                       " & vbNewLine _
    '                                   & "        ON TANKA.NRS_BR_CD  = GOODS.NRS_BR_CD                            " & vbNewLine _
    '                                   & "       AND TANKA.CUST_CD_L  = GOODS.CUST_CD_L                            " & vbNewLine _
    '                                   & "       AND TANKA.CUST_CD_M  = GOODS.CUST_CD_M                            " & vbNewLine _
    '                                   & "       AND TANKA.UP_GP_CD_1 = GOODS.UP_GP_CD_1                           " & vbNewLine _
    '                                   & "       AND TANKA.STR_DATE <= @F_DATE                                     " & vbNewLine _
    '                                   & "       AND TANKA.SYS_DEL_FLG = '0'                                       " & vbNewLine _
    '                                   & " --荷主マスタ                                                            " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_CUST CUST                                         " & vbNewLine _
    '                                   & "        ON CUST.NRS_BR_CD  = GOODS.NRS_BR_CD                             " & vbNewLine _
    '                                   & "       AND CUST.CUST_CD_L  = GOODS.CUST_CD_L                             " & vbNewLine _
    '                                   & "       AND CUST.CUST_CD_M  = GOODS.CUST_CD_M                             " & vbNewLine _
    '                                   & "       AND CUST.CUST_CD_S  = GOODS.CUST_CD_S                             " & vbNewLine _
    '                                   & "       AND CUST.CUST_CD_SS = GOODS.CUST_CD_SS                            " & vbNewLine _
    '                                   & "       AND CUST.SYS_DEL_FLG  = '0'                                       " & vbNewLine _
    '                                   & " --商品状態区分2(外観)                                                   " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..Z_KBN KBN_S006                                      " & vbNewLine _
    '                                   & "        ON KBN_S006.KBN_GROUP_CD = 'S006'                                " & vbNewLine _
    '                                   & "       AND KBN_S006.KBN_CD = ZAI.GOODS_COND_KB_2                         " & vbNewLine _
    '                                   & "       AND KBN_S006.SYS_DEL_FLG = '0'                                    " & vbNewLine _
    '                                   & " --荷主帳票パターン取得                                                  " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                     " & vbNewLine _
    '                                   & "        ON OUTKAL.NRS_BR_CD = MCR1.NRS_BR_CD                             " & vbNewLine _
    '                                   & "       AND GOODS.CUST_CD_L = MCR1.CUST_CD_L                              " & vbNewLine _
    '                                   & "       AND GOODS.CUST_CD_M = MCR1.CUST_CD_M                              " & vbNewLine _
    '                                   & "       AND '00' = MCR1.CUST_CD_S                                         " & vbNewLine _
    '                                   & "       AND MCR1.PTN_ID = 'AS'                                            " & vbNewLine _
    '                                   & " --帳票パターン取得                                                      " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_RPT MR1                                           " & vbNewLine _
    '                                   & "        ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                " & vbNewLine _
    '                                   & "       AND MR1.PTN_ID = MCR1.PTN_ID                                      " & vbNewLine _
    '                                   & "       AND MR1.PTN_CD = MCR1.PTN_CD                                      " & vbNewLine _
    '                                   & "       AND MR1.SYS_DEL_FLG = '0'                                         " & vbNewLine _
    '                                   & " --商品Mの荷主での荷主帳票パターン取得                                   " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                     " & vbNewLine _
    '                                   & "        ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                              " & vbNewLine _
    '                                   & "       AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                              " & vbNewLine _
    '                                   & "       AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                              " & vbNewLine _
    '                                   & "       AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                              " & vbNewLine _
    '                                   & "       AND MCR2.PTN_ID = 'AS'                                            " & vbNewLine _
    '                                   & " --帳票パターン取得                                                      " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_RPT MR2                                           " & vbNewLine _
    '                                   & "        ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                " & vbNewLine _
    '                                   & "       AND MR2.PTN_ID = MCR2.PTN_ID                                      " & vbNewLine _
    '                                   & "       AND MR2.PTN_CD = MCR2.PTN_CD                                      " & vbNewLine _
    '                                   & "       AND MR2.SYS_DEL_FLG = '0'                                         " & vbNewLine _
    '                                   & " --存在しない場合の帳票パターン取得                                      " & vbNewLine _
    '                                   & " LEFT JOIN $LM_MST$..M_RPT MR3                                           " & vbNewLine _
    '                                   & "        ON MR3.NRS_BR_CD = OUTKAL.NRS_BR_CD                              " & vbNewLine _
    '                                   & "       AND MR3.PTN_ID = 'AS'                                             " & vbNewLine _
    '                                   & "       AND MR3.STANDARD_FLAG = '01'                                      " & vbNewLine _
    '                                   & "       AND MR3.SYS_DEL_FLG = '0'                                         " & vbNewLine _
    '                                   & "                                                                         " & vbNewLine _
    '                                   & " WHERE OUTKAL.CUST_CD_L = '00145'                                        " & vbNewLine _
    '                                   & "   AND OUTKAL.CUST_CD_M = '00'                                           " & vbNewLine _
    '                                   & "   AND OUTKAL.OUTKA_STATE_KB >= '60'   -- 'S010'                         " & vbNewLine _
    '                                   & "   AND OUTKAL.END_DATE BETWEEN @F_DATE                                   " & vbNewLine _
    '                                   & "   AND @T_DATE                                                           " & vbNewLine _
    '                                   & "   AND OUTKAL.NIYAKU_YN = '01'        -- 'U009'                          " & vbNewLine _
    '                                   & "   AND OUTKAL.SYS_DEL_FLG = '0'                                          " & vbNewLine _
    '                                   & "   AND OUTKAL.NRS_BR_CD = @NRS_BR_CD                                     " & vbNewLine _
    '                                   & "   AND TANKA.HANDLING_OUT > 0                                            " & vbNewLine _
    '                                   & "                                                                         " & vbNewLine _
    '                                   & " GROUP BY                                                                " & vbNewLine _
    '                                   & "    CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                     " & vbNewLine _
    '                                   & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
    '                                   & "    ELSE MR3.RPT_ID END                                                  " & vbNewLine _
    '                                   & "   , OUTKAL.NRS_BR_CD                                                    " & vbNewLine _
    '                                   & "   , CUST.CUST_CD_L                                                      " & vbNewLine _
    '                                   & "   , CUST.CUST_CD_M                                                      " & vbNewLine _
    '                                   & "   , CUST.CUST_CD_S                                                      " & vbNewLine _
    '                                   & "   , CUST.CUST_CD_SS                                                     " & vbNewLine _
    '                                   & "   , CUST.CUST_NM_L                                                      " & vbNewLine _
    '                                   & "   , CUST.CUST_NM_M                                                      " & vbNewLine _
    '                                   & "   , CUST.CUST_NM_S                                                      " & vbNewLine _
    '                                   & "   , CUST.CUST_NM_SS                                                     " & vbNewLine _
    '                                   & "   , CASE WHEN CUST.NIYAKU_SEIQTO_CD = ''  THEN CUST.NIYAKU_SEIQTO_CD    " & vbNewLine _
    '                                   & "     ELSE CUST.HOKAN_SEIQTO_CD                                           " & vbNewLine _
    '                                   & "     END                                                                 " & vbNewLine _
    '                                   & "   , CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM                 " & vbNewLine _
    '                                   & "          WHEN OUTKAL.DEST_KB = '02' THEN OUTKAEDIL.DEST_NM              " & vbNewLine _
    '                                   & "     ELSE DESTL.DEST_NM                                                  " & vbNewLine _
    '                                   & "     END                                                                 " & vbNewLine _
    '                                   & "  , OUTKAL.CUST_ORD_NO                                                   " & vbNewLine _
    '                                   & "  , UNSOCO.UNSOCO_NM                                                     " & vbNewLine _
    '                                   & "  , ZAI.GOODS_CD_NRS                                                     " & vbNewLine _
    '                                   & "  , GOODS.GOODS_CD_CUST                                                  " & vbNewLine _
    '                                   & "  , GOODS.GOODS_NM_1                                                     " & vbNewLine _
    '                                   & "  , OUTKAS.INKA_NO_L                                                     " & vbNewLine _
    '                                   & "  , OUTKAL.END_DATE                                                      " & vbNewLine _
    '                                   & "  , OUTKAS.LOT_NO                                                        " & vbNewLine _
    '                                   & "  , OUTKAS.SERIAL_NO                                                     " & vbNewLine _
    '                                   & "  , OUTKAS.TOU_NO                                                        " & vbNewLine _
    '                                   & "  , OUTKAS.SITU_NO                                                       " & vbNewLine _
    '                                   & "  , OUTKAS.ZONE_CD                                                       " & vbNewLine _
    '                                   & "  , OUTKAS.LOCA                                                          " & vbNewLine _
    '                                   & "  , GOODS.PKG_UT                                                         " & vbNewLine _
    '                                   & "  , GOODS.NB_UT                                                          " & vbNewLine _
    '                                   & "  , OUTKAS.IRIME                                                         " & vbNewLine _
    '                                   & "  , GOODS.STD_IRIME_UT                                                   " & vbNewLine _
    '                                   & "  , ZAI.TAX_KB                                                           " & vbNewLine _
    '                                   & "--項目無し    ,ZAI.INKA_TP                                               " & vbNewLine _
    '                                   & "  , ZAI.LT_DATE                                                          " & vbNewLine _
    '                                   & "  , ZAI.GOODS_CRT_DATE                                                   " & vbNewLine _
    '                                   & "  , ZAI.REMARK_OUT                                                       " & vbNewLine _
    '                                   & "  , ZAI.GOODS_COND_KB_1                                                  " & vbNewLine _
    '                                   & "  , ZAI.GOODS_COND_KB_2                                                  " & vbNewLine _
    '                                   & "  , ZAI.GOODS_COND_KB_3                                                  " & vbNewLine _
    '                                   & "  , ZAI.SPD_KB                                                           " & vbNewLine _
    '                                   & "  , ZAI.OFB_KB                                                           " & vbNewLine _
    '                                   & "--項目無し    ,ZAI.NAIGAI_KB                                             " & vbNewLine _
    '                                   & "  , CASE WHEN KBN_S006.KBN_NM1  IS NULL THEN 'F'                         " & vbNewLine _
    '                                   & "    ELSE KBN_S006.KBN_NM1                                                " & vbNewLine _
    '                                   & "    END                                                                  " & vbNewLine _
    '                                   & "  , TANKA.HANDLING_OUT                                                   " & vbNewLine _
    '                                   & "                                                                         " & vbNewLine _
    '                                   & "HAVING 300 - (SUM(ISNULL(OUTKAS.ALCTD_NB , 0)) * TANKA.HANDLING_OUT) > 0 " & vbNewLine _
    '                                   & "                                                                         " & vbNewLine _
    '                                   & ") MAIN                                                                   " & vbNewLine

#End Region


    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                                               " & vbNewLine _
                                         & "   NRS_BR_CD                                            " & vbNewLine _
                                         & " , CUST_CD_L                                            " & vbNewLine _
                                         & " , CUST_CD_M                                            " & vbNewLine _
                                         & " , CUST_CD_S                                            " & vbNewLine _
                                         & " , CUST_CD_SS                                           " & vbNewLine _
                                         & " , GOODS_CD_CUST                                        " & vbNewLine _
                                         & " , INKA_NO_L                                            " & vbNewLine _
                                         & " , LOT_NO                                               " & vbNewLine _
                                         & " , INOUT_DATE                                           " & vbNewLine _
                                         & " , INOUT_KB                                             " & vbNewLine _
                                         & " , DEST_NM                                              " & vbNewLine

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

    ''' <summary>
    ''' ゼロフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ZERO_FLG As String = "0"


#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''帳票パターンマスタ データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタデータ取得 SQLの構築・発行</remarks>
    Private Function SelectMPrintPattern(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI620IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI620DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMI620DAC.SQL_MPrt_FROM)      'SQL構築(帳票種別用FROM句)
        Call Me.SetConditionMasterSQL()                 'SQL構築(印刷データ抽出条件設定)

        Me._StrSql.Append(LMI620DAC.SQL_MPrt_WHERE)     'SQL構築(帳票種別用FROM句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI620DAC", "SelectMPrt", cmd)

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
    ''' 運賃テーブル(F_UNCHIN_TRS)対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル(F_UNCHIN_TRS)対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI620IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI620DAC.SQL_SELECT_MAIN)      'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMI620DAC.SQL_SELECT_INKA)      'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMI620DAC.SQL_SELECT_OUTKA)     'SQL構築(印刷データ抽出用 SELECT句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(印刷データ抽出条件設定)
        Me._StrSql.Append(LMI620DAC.SQL_ORDER_BY)         'SQL構築(印刷データ抽出用 ORDER BY句)


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI620DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUT_KB", "INOUT_KB")
        map.Add("F_DATE", "F_DATE")
        map.Add("T_DATE", "T_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("NIYAKU_SEIQTO_CD", "NIYAKU_SEIQTO_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INOUT_DATE", "INOUT_DATE")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("NB_UT", "NB_UT")
        map.Add("IRIME", "IRIME")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("ZAIKO_KB", "ZAIKO_KB")
        map.Add("IN_NB", "IN_NB")
        map.Add("OUT_NB", "OUT_NB")
        map.Add("NIYAKU_TANKA", "NIYAKU_TANKA")
        map.Add("NIYAKU_HOSHO", "NIYAKU_HOSHO")
        map.Add("N_AM_TTL", "N_AM_TTL")
        map.Add("N_AM_SAGAKU", "N_AM_SAGAKU")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI620OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

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
                'Me._StrSql.Append(" UNCHIN.NRS_BR_CD = @NRS_BR_CD")
                'Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND UNCHIN.CUST_CD_L = @CUST_CD_L")
                'Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND UNCHIN.CUST_CD_M = @CUST_CD_M")
                'Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '出荷日(FROM)
            whereStr = .Item("F_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND UNSO.ARR_PLAN_DATE >= @F_DATE ")
                'Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", whereStr, DBDataType.CHAR))
            End If

            '出荷日(TO)
            whereStr = .Item("T_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND UNSO.ARR_PLAN_DATE <= @T_DATE ")
                'Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", whereStr, DBDataType.CHAR))
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

#End Region

End Class
