' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC651    : 梱包明細
'  作  成  者       :  inoue
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC651DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC651DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    Private Const PTN_ID As String = "C1"

    Class TABLE_NM
        Public Const INPUT As String = "LMC651IN"
        Public Const OUTPUT As String = "LMC651OUT"
        Public Const M_RPT As String = "M_RPT"

    End Class

    Class COLUM_NM
        Public Const NRS_BR_CD As String = "NRS_BR_CD"
        Public Const OUTKA_NO_L As String = "OUTKA_NO_L"
        Public Const PTN_ID As String = "PTN_ID"
        Public Const PTN_CD As String = "PTN_CD"
        Public Const RPT_ID As String = "RPT_ID"

        Public Const CUST_ORD_NO As String = "CUST_ORD_NO"
        Public Const GOODS_CD_NRS As String = "GOODS_CD_NRS"
        Public Const GOODS_CD_CUST As String = "GOODS_CD_CUST"
        Public Const GOODS_NM_1 As String = "GOODS_NM_1"
        Public Const LOT_NO As String = "LOT_NO"
        Public Const IRIME As String = "IRIME"
        Public Const IRIME_UT As String = "IRIME_UT"
        Public Const OUTKA_TTL_NB As String = "OUTKA_TTL_NB"
        Public Const OUTKA_TTL_QT As String = "OUTKA_TTL_QT"
        Public Const HASU As String = "HASU"
        Public Const OUTKA_PKG_NB As String = "OUTKA_PKG_NB"
        Public Const STD_IRIME_NB As String = "STD_IRIME_NB"
        Public Const STD_IRIME_UT As String = "STD_IRIME_UT"
        Public Const STD_WT_KGS As String = "STD_WT_KGS"
        Public Const PKG_NB As String = "PKG_NB"
        Public Const PKG_UT As String = "PKG_UT"
        Public Const NET As String = "NET"
        Public Const CUST_NM_L As String = "CUST_NM_L"
        Public Const AD_1 As String = "AD_1"
        Public Const TEL As String = "TEL"
        Public Const SHIP_NM As String = "SHIP_NM"
        Public Const REMARK_UPPER As String = "REMARK_UPPER"    'ADD 2022/07/25 029847   【LMS】横浜BC　フィルメニッヒ業務改善　

    End Class

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String _
        = " SELECT DISTINCT                                                       " & vbNewLine _
        & "        CL.NRS_BR_CD                                     AS NRS_BR_CD  " & vbNewLine _
        & "      , @PTN_ID                                          AS PTN_ID     " & vbNewLine _
        & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD               " & vbNewLine _
        & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD               " & vbNewLine _
        & "        ELSE MR3.PTN_CD                                                " & vbNewLine _
        & "        END                                              AS PTN_CD     " & vbNewLine _
        & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID               " & vbNewLine _
        & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID               " & vbNewLine _
        & "        ELSE MR3.RPT_ID                                                " & vbNewLine _
        & "        END                                              AS RPT_ID     " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用SELECT区
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String _
        = " SELECT                                                                                            " & vbNewLine _
        & "        CASE WHEN MR2.RPT_ID IS NOT NULL                                                           " & vbNewLine _
        & "             THEN MR2.RPT_ID                                                                       " & vbNewLine _
        & "             WHEN MR1.RPT_ID IS NOT NULL                                                           " & vbNewLine _
        & "             THEN MR1.RPT_ID                                                                       " & vbNewLine _
        & "             ELSE MR3.RPT_ID                                                                       " & vbNewLine _
        & "        END                                                                AS RPT_ID               " & vbNewLine _
        & "      , CM.NRS_BR_CD                                                       AS NRS_BR_CD            " & vbNewLine _
        & "      , CM.OUTKA_NO_L                                                      AS OUTKA_NO_L           " & vbNewLine _
        & "      , CL.CUST_ORD_NO                                                     AS CUST_ORD_NO          " & vbNewLine _
        & "      , CM.GOODS_CD_NRS                                                    AS GOODS_CD_NRS         " & vbNewLine _
        & "      , MG.GOODS_CD_CUST                                                   AS GOODS_CD_CUST        " & vbNewLine _
        & "      , MG.GOODS_NM_1                                                      AS GOODS_NM_1           " & vbNewLine _
        & "      , CM.IRIME                                                           AS IRIME                " & vbNewLine _
        & "      , CM.LOT_NO                                                          AS LOT_NO               " & vbNewLine _
        & "      , ROUND((SUM(CM.CALC_TTL_QT) * MG.STD_WT_KGS) / MG.STD_IRIME_NB, 3)  AS NET                  " & vbNewLine _
        & "      , CM.OUTKA_TTL_NB                                                    AS OUTKA_TTL_NB         " & vbNewLine _
        & "      , MG.STD_IRIME_NB                                                    AS STD_IRIME_NB         " & vbNewLine _
        & "      , MG.STD_IRIME_UT                                                    AS STD_IRIME_UT         " & vbNewLine _
        & "      , MG.STD_WT_KGS                                                      AS STD_WT_KGS           " & vbNewLine _
        & "      , MG.PKG_NB                                                          AS PKG_NB               " & vbNewLine _
        & "      , MG.PKG_UT                                                          AS PKG_UT               " & vbNewLine _
        & "      , MC.CUST_NM_L                                                       AS CUST_NM_L            " & vbNewLine _
        & "      , MC.AD_1                                                            AS AD_1                 " & vbNewLine _
        & "      , MC.TEL                                                             AS TEL                  " & vbNewLine _
        & "      , ISNULL(SP.DEST_NM, '')                                             AS SHIP_NM　            " & vbNewLine

    Private Const SQL_SELECT_DATA655 As String _
        = " SELECT                                                                                            " & vbNewLine _
        & "        CASE WHEN MR2.RPT_ID IS NOT NULL                                                           " & vbNewLine _
        & "             THEN MR2.RPT_ID                                                                       " & vbNewLine _
        & "             WHEN MR1.RPT_ID IS NOT NULL                                                           " & vbNewLine _
        & "             THEN MR1.RPT_ID                                                                       " & vbNewLine _
        & "             ELSE MR3.RPT_ID                                                                       " & vbNewLine _
        & "        END                                                                AS RPT_ID               " & vbNewLine _
        & "      , CM.NRS_BR_CD                                                       AS NRS_BR_CD            " & vbNewLine _
        & "      , CM.OUTKA_NO_L                                                      AS OUTKA_NO_L           " & vbNewLine _
        & "      , CL.CUST_ORD_NO                                                     AS CUST_ORD_NO          " & vbNewLine _
        & "      , CM.GOODS_CD_NRS                                                    AS GOODS_CD_NRS         " & vbNewLine _
        & "      , MG.GOODS_CD_CUST                                                   AS GOODS_CD_CUST        " & vbNewLine _
        & "      , MG.GOODS_NM_1                                                      AS GOODS_NM_1           " & vbNewLine _
        & "      , CM.IRIME                                                           AS IRIME                " & vbNewLine _
        & "      , CM.LOT_NO                                                          AS LOT_NO               " & vbNewLine _
        & "      , ROUND((SUM(CM.CALC_TTL_QT) * MG.STD_WT_KGS) / MG.STD_IRIME_NB, 3)  AS NET                  " & vbNewLine _
        & "      , CM.OUTKA_TTL_NB                                                    AS OUTKA_TTL_NB         " & vbNewLine _
        & "      , MG.STD_IRIME_NB                                                    AS STD_IRIME_NB         " & vbNewLine _
        & "      , MG.STD_IRIME_UT                                                    AS STD_IRIME_UT         " & vbNewLine _
        & "      , MG.STD_WT_KGS                                                      AS STD_WT_KGS           " & vbNewLine _
        & "      , MG.PKG_NB                                                          AS PKG_NB               " & vbNewLine _
        & "      , MG.PKG_UT                                                          AS PKG_UT               " & vbNewLine _
        & "      , MC.CUST_NM_L                                                       AS CUST_NM_L            " & vbNewLine _
        & "      , MC.AD_1                                                            AS AD_1                 " & vbNewLine _
        & "      , MC.TEL                                                             AS TEL                  " & vbNewLine _
        & "      , ISNULL(SP.DEST_NM, '')                                             AS SHIP_NM　            " & vbNewLine _
        & "       ,ISNULL(EDIM.FREE_C04,'')                                           AS REMARK_UPPER         " & vbNewLine



    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String _
        = "   FROM                                                           " & vbNewLine _
        & "        $LM_TRN$..C_OUTKA_L AS CL                                 " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_DEST AS SP                                    " & vbNewLine _
        & "     ON SP.NRS_BR_CD    = CL.NRS_BR_CD                            " & vbNewLine _
        & "    AND SP.CUST_CD_L    = CL.CUST_CD_L                            " & vbNewLine _
        & "    AND SP.DEST_CD      = CL.SHIP_CD_L                            " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        (                                                         " & vbNewLine _
        & "         SELECT                                                   " & vbNewLine _
        & "                 C_OUTKA_S.NRS_BR_CD                                             " & vbNewLine _
        & "               , C_OUTKA_S.OUTKA_NO_L                                            " & vbNewLine _
        & "               , C_OUTKA_M.GOODS_CD_NRS                                          " & vbNewLine _
        & "               , C_OUTKA_S.LOT_NO                                                " & vbNewLine _
        & "               , C_OUTKA_S.IRIME                                                 " & vbNewLine _
        & "               , C_OUTKA_S.IRIME * SUM(C_OUTKA_S.ALCTD_NB)   AS CALC_TTL_QT      " & vbNewLine _
        & "               , SUM(C_OUTKA_S.ALCTD_NB)                     AS OUTKA_TTL_NB     " & vbNewLine _
        & "               , SUM(C_OUTKA_S.ALCTD_QT)                     AS OUTKA_TTL_QT     " & vbNewLine _
        & "         FROM                                                                    " & vbNewLine _
        & "                 $LM_TRN$..C_OUTKA_S                                             " & vbNewLine _
        & "         LEFT JOIN   $LM_TRN$..C_OUTKA_M                                         " & vbNewLine _
        & "         ON                                                                      " & vbNewLine _
        & "         	     C_OUTKA_M.NRS_BR_CD  = C_OUTKA_S.NRS_BR_CD                     " & vbNewLine _
        & "             AND  C_OUTKA_M.OUTKA_NO_L = C_OUTKA_S.OUTKA_NO_L                    " & vbNewLine _
        & "             AND  C_OUTKA_M.OUTKA_NO_M = C_OUTKA_S.OUTKA_NO_M                    " & vbNewLine _
        & "         WHERE                                                                   " & vbNewLine _
        & "                 C_OUTKA_S.NRS_BR_CD   = @NRS_BR_CD                              " & vbNewLine _
        & "             AND C_OUTKA_S.OUTKA_NO_L  = @OUTKA_NO_L                             " & vbNewLine _
        & "             AND C_OUTKA_S.SYS_DEL_FLG = '0'                                     " & vbNewLine _
        & "             $ADD_WHERE_OUTKA_NO_M$                                              " & vbNewLine _
        & "         GROUP BY                                                                " & vbNewLine _
        & "               C_OUTKA_S.NRS_BR_CD                                               " & vbNewLine _
        & "             , C_OUTKA_S.OUTKA_NO_L                                              " & vbNewLine _
        & "             , C_OUTKA_M.GOODS_CD_NRS                                            " & vbNewLine _
        & "             , C_OUTKA_S.IRIME                                                   " & vbNewLine _
        & "             , C_OUTKA_S.LOT_NO                                                  " & vbNewLine _
        & "                                        ) AS CM                   " & vbNewLine _
        & "     ON CM.OUTKA_NO_L  = CL.OUTKA_NO_L                            " & vbNewLine _
        & "    AND CM.NRS_BR_CD   = CL.NRS_BR_CD                             " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_GOODS AS MG                                   " & vbNewLine _
        & "     ON MG.GOODS_CD_NRS = CM.GOODS_CD_NRS                         " & vbNewLine _
        & "    AND MG.NRS_BR_CD    = CM.NRS_BR_CD                            " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_CUST  AS MC                                   " & vbNewLine _
        & "     ON MC.NRS_BR_CD    = MG.NRS_BR_CD                            " & vbNewLine _
        & "    AND MC.CUST_CD_L    = MG.CUST_CD_L                            " & vbNewLine _
        & "    AND MC.CUST_CD_M    = MG.CUST_CD_M                            " & vbNewLine _
        & "    AND MC.CUST_CD_S    = MG.CUST_CD_S                            " & vbNewLine _
        & "    AND MC.CUST_CD_SS   = MG.CUST_CD_SS                           " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_CUST_RPT AS MCR1                              " & vbNewLine _
        & "     ON MCR1.NRS_BR_CD = CL.NRS_BR_CD                             " & vbNewLine _
        & "    AND MCR1.CUST_CD_L = CL.CUST_CD_L                             " & vbNewLine _
        & "    AND MCR1.CUST_CD_M = CL.CUST_CD_M                             " & vbNewLine _
        & "    AND MCR1.CUST_CD_S = '00'                                     " & vbNewLine _
        & "    AND MCR1.PTN_ID    = @PTN_ID                                  " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_RPT AS MR1                                    " & vbNewLine _
        & "     ON MR1.NRS_BR_CD   = MCR1.NRS_BR_CD                          " & vbNewLine _
        & "    AND MR1.PTN_ID      = MCR1.PTN_ID                             " & vbNewLine _
        & "    AND MR1.PTN_CD      = MCR1.PTN_CD                             " & vbNewLine _
        & "    AND MR1.SYS_DEL_FLG = '0'                                     " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_CUST_RPT AS MCR2                              " & vbNewLine _
        & "     ON MCR2.NRS_BR_CD = MG.NRS_BR_CD                             " & vbNewLine _
        & "    AND MCR2.CUST_CD_L = MG.CUST_CD_L                             " & vbNewLine _
        & "    AND MCR2.CUST_CD_M = MG.CUST_CD_M                             " & vbNewLine _
        & "    AND MCR2.CUST_CD_S = MG.CUST_CD_S                             " & vbNewLine _
        & "    AND MCR2.PTN_ID    = @PTN_ID                                  " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_RPT AS MR2                                    " & vbNewLine _
        & "     ON MR2.NRS_BR_CD   = MCR2.NRS_BR_CD                          " & vbNewLine _
        & "    AND MR2.PTN_ID      = MCR2.PTN_ID                             " & vbNewLine _
        & "    AND MR2.PTN_CD      = MCR2.PTN_CD                             " & vbNewLine _
        & "    AND MR2.SYS_DEL_FLG = '0'                                     " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_RPT AS MR3                                    " & vbNewLine _
        & "     ON MR3.NRS_BR_CD      = CL.NRS_BR_CD                         " & vbNewLine _
        & "    AND MR3.PTN_ID         = @PTN_ID                              " & vbNewLine _
        & "    AND MR3.STANDARD_FLAG  = '01'                                 " & vbNewLine _
        & "    AND MR3.SYS_DEL_FLG    = '0'                                  " & vbNewLine

    Private Const SQL_FROM655 As String _
        = "   FROM                                                           " & vbNewLine _
        & "        $LM_TRN$..C_OUTKA_L AS CL                                 " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_DEST AS SP                                    " & vbNewLine _
        & "     ON SP.NRS_BR_CD    = CL.NRS_BR_CD                            " & vbNewLine _
        & "    AND SP.CUST_CD_L    = CL.CUST_CD_L                            " & vbNewLine _
        & "    AND SP.DEST_CD      = CL.SHIP_CD_L                            " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        (                                                         " & vbNewLine _
        & "         SELECT                                                   " & vbNewLine _
        & "                 C_OUTKA_S.NRS_BR_CD                                             " & vbNewLine _
        & "               , C_OUTKA_S.OUTKA_NO_L                                            " & vbNewLine _
        & "               , C_OUTKA_S.OUTKA_NO_M                                            " & vbNewLine _
        & "               , C_OUTKA_M.GOODS_CD_NRS                                          " & vbNewLine _
        & "               , C_OUTKA_S.LOT_NO                                                " & vbNewLine _
        & "               , C_OUTKA_S.IRIME                                                 " & vbNewLine _
        & "               , C_OUTKA_S.IRIME * SUM(C_OUTKA_S.ALCTD_NB)   AS CALC_TTL_QT      " & vbNewLine _
        & "               , SUM(C_OUTKA_S.ALCTD_NB)                     AS OUTKA_TTL_NB     " & vbNewLine _
        & "               , SUM(C_OUTKA_S.ALCTD_QT)                     AS OUTKA_TTL_QT     " & vbNewLine _
        & "         FROM                                                                    " & vbNewLine _
        & "                 $LM_TRN$..C_OUTKA_S                                             " & vbNewLine _
        & "         LEFT JOIN   $LM_TRN$..C_OUTKA_M                                         " & vbNewLine _
        & "         ON                                                                      " & vbNewLine _
        & "         	     C_OUTKA_M.NRS_BR_CD  = C_OUTKA_S.NRS_BR_CD                     " & vbNewLine _
        & "             AND  C_OUTKA_M.OUTKA_NO_L = C_OUTKA_S.OUTKA_NO_L                    " & vbNewLine _
        & "             AND  C_OUTKA_M.OUTKA_NO_M = C_OUTKA_S.OUTKA_NO_M                    " & vbNewLine _
        & "         WHERE                                                                   " & vbNewLine _
        & "                 C_OUTKA_S.NRS_BR_CD   = @NRS_BR_CD                              " & vbNewLine _
        & "             AND C_OUTKA_S.OUTKA_NO_L  = @OUTKA_NO_L                             " & vbNewLine _
        & "             AND C_OUTKA_S.SYS_DEL_FLG = '0'                                     " & vbNewLine _
        & "             $ADD_WHERE_OUTKA_NO_M$                                              " & vbNewLine _
        & "         GROUP BY                                                                " & vbNewLine _
        & "               C_OUTKA_S.NRS_BR_CD                                               " & vbNewLine _
        & "             , C_OUTKA_S.OUTKA_NO_L                                              " & vbNewLine _
        & "             , C_OUTKA_S.OUTKA_NO_M                                              " & vbNewLine _
        & "             , C_OUTKA_M.GOODS_CD_NRS                                            " & vbNewLine _
        & "             , C_OUTKA_S.IRIME                                                   " & vbNewLine _
        & "             , C_OUTKA_S.LOT_NO                                                  " & vbNewLine _
        & "                                        ) AS CM                   " & vbNewLine _
        & "     ON CM.OUTKA_NO_L  = CL.OUTKA_NO_L                            " & vbNewLine _
        & "    AND CM.NRS_BR_CD   = CL.NRS_BR_CD                             " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_GOODS AS MG                                   " & vbNewLine _
        & "     ON MG.GOODS_CD_NRS = CM.GOODS_CD_NRS                         " & vbNewLine _
        & "    AND MG.NRS_BR_CD    = CM.NRS_BR_CD                            " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_CUST  AS MC                                   " & vbNewLine _
        & "     ON MC.NRS_BR_CD    = MG.NRS_BR_CD                            " & vbNewLine _
        & "    AND MC.CUST_CD_L    = MG.CUST_CD_L                            " & vbNewLine _
        & "    AND MC.CUST_CD_M    = MG.CUST_CD_M                            " & vbNewLine _
        & "    AND MC.CUST_CD_S    = MG.CUST_CD_S                            " & vbNewLine _
        & "    AND MC.CUST_CD_SS   = MG.CUST_CD_SS                           " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_CUST_RPT AS MCR1                              " & vbNewLine _
        & "     ON MCR1.NRS_BR_CD = CL.NRS_BR_CD                             " & vbNewLine _
        & "    AND MCR1.CUST_CD_L = CL.CUST_CD_L                             " & vbNewLine _
        & "    AND MCR1.CUST_CD_M = CL.CUST_CD_M                             " & vbNewLine _
        & "    AND MCR1.CUST_CD_S = '00'                                     " & vbNewLine _
        & "    AND MCR1.PTN_ID    = @PTN_ID                                  " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_RPT AS MR1                                    " & vbNewLine _
        & "     ON MR1.NRS_BR_CD   = MCR1.NRS_BR_CD                          " & vbNewLine _
        & "    AND MR1.PTN_ID      = MCR1.PTN_ID                             " & vbNewLine _
        & "    AND MR1.PTN_CD      = MCR1.PTN_CD                             " & vbNewLine _
        & "    AND MR1.SYS_DEL_FLG = '0'                                     " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_CUST_RPT AS MCR2                              " & vbNewLine _
        & "     ON MCR2.NRS_BR_CD = MG.NRS_BR_CD                             " & vbNewLine _
        & "    AND MCR2.CUST_CD_L = MG.CUST_CD_L                             " & vbNewLine _
        & "    AND MCR2.CUST_CD_M = MG.CUST_CD_M                             " & vbNewLine _
        & "    AND MCR2.CUST_CD_S = MG.CUST_CD_S                             " & vbNewLine _
        & "    AND MCR2.PTN_ID    = @PTN_ID                                  " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_RPT AS MR2                                    " & vbNewLine _
        & "     ON MR2.NRS_BR_CD   = MCR2.NRS_BR_CD                          " & vbNewLine _
        & "    AND MR2.PTN_ID      = MCR2.PTN_ID                             " & vbNewLine _
        & "    AND MR2.PTN_CD      = MCR2.PTN_CD                             " & vbNewLine _
        & "    AND MR2.SYS_DEL_FLG = '0'                                     " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_RPT AS MR3                                    " & vbNewLine _
        & "     ON MR3.NRS_BR_CD      = CL.NRS_BR_CD                         " & vbNewLine _
        & "    AND MR3.PTN_ID         = @PTN_ID                              " & vbNewLine _
        & "    AND MR3.STANDARD_FLAG  = '01'                                 " & vbNewLine _
        & "    AND MR3.SYS_DEL_FLG    = '0'                                  " & vbNewLine _
        & "  --★出荷EDI M                                                   " & vbNewLine _
        & "   LEFT JOIN LM_TRN_40..H_OUTKAEDI_M EDIM                         " & vbNewLine _
        & "     ON  EDIM.NRS_BR_CD        = CM.NRS_BR_CD                     " & vbNewLine _
        & "    AND EDIM.OUTKA_CTL_NO     = CM.OUTKA_NO_L                     " & vbNewLine _
        & "    AND EDIM.OUTKA_CTL_NO_CHU = CM.OUTKA_NO_M                     " & vbNewLine _
        & "    AND EDIM.SYS_DEL_FLG = '0'                                    " & vbNewLine

    Private Const SQL_WHERE As String _
        = "  WHERE                                                           " & vbNewLine _
        & "        CL.NRS_BR_CD   = @NRS_BR_CD                               " & vbNewLine _
        & "    AND CL.OUTKA_NO_L  = @OUTKA_NO_L                              " & vbNewLine _
        & "    AND CL.SYS_DEL_FLG = '0'                                      " & vbNewLine


    Private Const SQL_GROUP_BY As String _
        = "  GROUP BY                                                   " & vbNewLine _
        & "        MR1.RPT_ID                                           " & vbNewLine _
        & "      , MR2.RPT_ID                                           " & vbNewLine _
        & "      , MR3.RPT_ID                                           " & vbNewLine _
        & "      , CM.NRS_BR_CD                                         " & vbNewLine _
        & "      , CM.OUTKA_NO_L                                        " & vbNewLine _
        & "      , CL.CUST_ORD_NO                                       " & vbNewLine _
        & "      , CM.GOODS_CD_NRS                                      " & vbNewLine _
        & "      , MG.GOODS_CD_CUST                                     " & vbNewLine _
        & "      , MG.GOODS_NM_1                                        " & vbNewLine _
        & "      , CM.IRIME                                             " & vbNewLine _
        & "      , CM.LOT_NO                                            " & vbNewLine _
        & "      , CM.OUTKA_TTL_NB                                      " & vbNewLine _
        & "      , MG.STD_IRIME_NB                                      " & vbNewLine _
        & "      , MG.STD_IRIME_UT                                      " & vbNewLine _
        & "      , MG.STD_WT_KGS                                        " & vbNewLine _
        & "      , MG.PKG_NB                                            " & vbNewLine _
        & "      , MG.PKG_UT                                            " & vbNewLine _
        & "      , MC.CUST_NM_L                                         " & vbNewLine _
        & "      , MC.AD_1                                              " & vbNewLine _
        & "      , MC.TEL                                               " & vbNewLine _
        & "      , ISNULL(SP.DEST_NM, '')                               " & vbNewLine

    Private Const SQL_GROUP_BY655 As String _
        = "  GROUP BY                                                   " & vbNewLine _
        & "        MR1.RPT_ID                                           " & vbNewLine _
        & "      , MR2.RPT_ID                                           " & vbNewLine _
        & "      , MR3.RPT_ID                                           " & vbNewLine _
        & "      , CM.NRS_BR_CD                                         " & vbNewLine _
        & "      , CM.OUTKA_NO_L                                        " & vbNewLine _
        & "      , CL.CUST_ORD_NO                                       " & vbNewLine _
        & "      , CM.GOODS_CD_NRS                                      " & vbNewLine _
        & "      , MG.GOODS_CD_CUST                                     " & vbNewLine _
        & "      , MG.GOODS_NM_1                                        " & vbNewLine _
        & "      , CM.IRIME                                             " & vbNewLine _
        & "      , CM.LOT_NO                                            " & vbNewLine _
        & "      , CM.OUTKA_TTL_NB                                      " & vbNewLine _
        & "      , MG.STD_IRIME_NB                                      " & vbNewLine _
        & "      , MG.STD_IRIME_UT                                      " & vbNewLine _
        & "      , MG.STD_WT_KGS                                        " & vbNewLine _
        & "      , MG.PKG_NB                                            " & vbNewLine _
        & "      , MG.PKG_UT                                            " & vbNewLine _
        & "      , MC.CUST_NM_L                                         " & vbNewLine _
        & "      , MC.AD_1                                              " & vbNewLine _
        & "      , MC.TEL                                               " & vbNewLine _
        & "      , ISNULL(SP.DEST_NM, '')                               " & vbNewLine _
        & "      , ISNULL(EDIM.FREE_C04, '')                            " & vbNewLine

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String _
        = "  ORDER BY                                                   " & vbNewLine _
        & "        CM.OUTKA_NO_L                                        " & vbNewLine _
        & "      , GOODS_CD_CUST                                        " & vbNewLine _
        & "      , LOT_NO                                               " & vbNewLine _
        & "      , IRIME DESC                                           " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.INPUT)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC651DAC.SQL_SELECT_MPrt)
        Me._StrSql.Append(LMC651DAC.SQL_FROM)
        Me._StrSql.Append(LMC651DAC.SQL_WHERE)

        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item(COLUM_NM.NRS_BR_CD).ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item(COLUM_NM.OUTKA_NO_L).ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", LMC651DAC.PTN_ID, DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString() _
                                         , Me._Row.Item(COLUM_NM.NRS_BR_CD).ToString())

        sql = Me.CreateWhereOutkaNoM(sql, inTbl)

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name _
                                    , System.Reflection.MethodBase.GetCurrentMethod().Name _
                                    , cmd)

            Dim selectCount As Integer = 0

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If (reader.HasRows) Then

                    '取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In New String() _
                        {
                            COLUM_NM.NRS_BR_CD,
                            COLUM_NM.PTN_ID,
                            COLUM_NM.PTN_CD,
                            COLUM_NM.RPT_ID
                        }

                        map.Add(item, item)
                    Next

                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.M_RPT)

                    selectCount = ds.Tables(TABLE_NM.M_RPT).Rows.Count

                End If

            End Using

            Me.SetResultCount(selectCount)

        End Using

        Return ds

    End Function


    ''' <summary>
    ''' 出荷指示書出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷指示書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.INPUT)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成#If true then
#If True Then   'UPD 2022/07/25 029847   【LMS】横浜BC　フィルメニッヒ業務改善　
        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC655" Then
            Me._StrSql.Append(LMC651DAC.SQL_SELECT_DATA655)
            Me._StrSql.Append(LMC651DAC.SQL_FROM655)
            Me._StrSql.Append(LMC651DAC.SQL_WHERE)
            Me._StrSql.Append(LMC651DAC.SQL_GROUP_BY655)
            Me._StrSql.Append(LMC651DAC.SQL_ORDER_BY)
        Else
            Me._StrSql.Append(LMC651DAC.SQL_SELECT_DATA)
            Me._StrSql.Append(LMC651DAC.SQL_FROM)
            Me._StrSql.Append(LMC651DAC.SQL_WHERE)
            Me._StrSql.Append(LMC651DAC.SQL_GROUP_BY)
            Me._StrSql.Append(LMC651DAC.SQL_ORDER_BY)
        End If
#Else
        Me._StrSql.Append(LMC651DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMC651DAC.SQL_FROM)
        Me._StrSql.Append(LMC651DAC.SQL_WHERE)
        Me._StrSql.Append(LMC651DAC.SQL_GROUP_BY)
        Me._StrSql.Append(LMC651DAC.SQL_ORDER_BY)

#End If

        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item(COLUM_NM.NRS_BR_CD).ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item(COLUM_NM.OUTKA_NO_L).ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", LMC651DAC.PTN_ID, DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString() _
                                         , Me._Row.Item(COLUM_NM.NRS_BR_CD).ToString())

        sql = Me.CreateWhereOutkaNoM(sql, inTbl)

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)
            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name _
                                    , System.Reflection.MethodBase.GetCurrentMethod().Name _
                                    , cmd)

            Dim selectCount As Integer = 0

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

#If True Then   'UPD 2022/07/25 029847   【LMS】横浜BC　フィルメニッヒ業務改善
                If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC655" Then
                    '取得データの格納先をマッピング
                    For Each item As String In New String() _
                        {
                            COLUM_NM.RPT_ID,
                            COLUM_NM.NRS_BR_CD,
                            COLUM_NM.OUTKA_NO_L,
                            COLUM_NM.CUST_ORD_NO,
                            COLUM_NM.GOODS_CD_NRS,
                            COLUM_NM.GOODS_CD_CUST,
                            COLUM_NM.GOODS_NM_1,
                            COLUM_NM.IRIME,
                            COLUM_NM.LOT_NO,
                            COLUM_NM.NET,
                            COLUM_NM.OUTKA_TTL_NB,
                            COLUM_NM.STD_IRIME_NB,
                            COLUM_NM.STD_IRIME_UT,
                            COLUM_NM.STD_WT_KGS,
                            COLUM_NM.PKG_NB,
                            COLUM_NM.PKG_UT,
                            COLUM_NM.CUST_NM_L,
                            COLUM_NM.AD_1,
                            COLUM_NM.TEL,
                            COLUM_NM.SHIP_NM,
                            COLUM_NM.REMARK_UPPER
                        }

                        map.Add(item, item)
                    Next
                Else
                    '取得データの格納先をマッピング
                    For Each item As String In New String() _
                        {
                            COLUM_NM.RPT_ID,
                            COLUM_NM.NRS_BR_CD,
                            COLUM_NM.OUTKA_NO_L,
                            COLUM_NM.CUST_ORD_NO,
                            COLUM_NM.GOODS_CD_NRS,
                            COLUM_NM.GOODS_CD_CUST,
                            COLUM_NM.GOODS_NM_1,
                            COLUM_NM.IRIME,
                            COLUM_NM.LOT_NO,
                            COLUM_NM.NET,
                            COLUM_NM.OUTKA_TTL_NB,
                            COLUM_NM.STD_IRIME_NB,
                            COLUM_NM.STD_IRIME_UT,
                            COLUM_NM.STD_WT_KGS,
                            COLUM_NM.PKG_NB,
                            COLUM_NM.PKG_UT,
                            COLUM_NM.CUST_NM_L,
                            COLUM_NM.AD_1,
                            COLUM_NM.TEL,
                            COLUM_NM.SHIP_NM
                        }

                        map.Add(item, item)
                    Next
                End If
#Else
                                '取得データの格納先をマッピング
                For Each item As String In New String() _
                    {
                        COLUM_NM.RPT_ID,
                        COLUM_NM.NRS_BR_CD,
                        COLUM_NM.OUTKA_NO_L,
                        COLUM_NM.CUST_ORD_NO,
                        COLUM_NM.GOODS_CD_NRS,
                        COLUM_NM.GOODS_CD_CUST,
                        COLUM_NM.GOODS_NM_1,
                        COLUM_NM.IRIME,
                        COLUM_NM.LOT_NO,
                        COLUM_NM.NET,
                        COLUM_NM.OUTKA_TTL_NB,
                        COLUM_NM.STD_IRIME_NB,
                        COLUM_NM.STD_IRIME_UT,
                        COLUM_NM.STD_WT_KGS,
                        COLUM_NM.PKG_NB,
                        COLUM_NM.PKG_UT,
                        COLUM_NM.CUST_NM_L,
                        COLUM_NM.AD_1,
                        COLUM_NM.TEL,
                        COLUM_NM.SHIP_NM,
                        COLUM_NM.REMARK_UPPER
                    }

                    map.Add(item, item)
                Next

#End If

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.OUTPUT)

            End Using

            Me.SetResultCount(selectCount)

        End Using


        Return ds

    End Function

    ''' <summary>
    ''' 検索条件OUTKA_NO_Mの設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CreateWhereOutkaNoM(ByVal sql As String, ByVal dt As DataTable) As String

        Dim addOutkaNoM As String = String.Empty

        '検索条件作成()
        '1レコード目の印刷フラグがONの場合はLMC020で中指定で出力

        If Not String.IsNullOrEmpty(dt.Rows(0).Item("PRINT_FLG").ToString) Then
            '中番号連結
            For Each dr As DataRow In dt.Rows
                If LMConst.FLG.ON.Equals(dr.Item("PRINT_FLG").ToString) Then
                    addOutkaNoM = String.Concat(addOutkaNoM, "'", dr.Item("OUTKA_NO_M").ToString, "',")
                End If
            Next

            '一個でもチェックしてた場合、条件編集
            If addOutkaNoM.Length > 0 Then
                addOutkaNoM = addOutkaNoM.Substring(0, addOutkaNoM.Length - 1)
                addOutkaNoM = String.Concat(" AND C_OUTKA_M.OUTKA_NO_M in ( ", _
                                            addOutkaNoM, _
                                            " ) ")
            End If
        End If

        Return sql.Replace("$ADD_WHERE_OUTKA_NO_M$", addOutkaNoM)

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

#End Region

#End Region

End Class
