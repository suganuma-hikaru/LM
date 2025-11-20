' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG530DAC : 請求鑑取込ﾁｪｯｸﾘｽﾄ(保管荷役料)
'  作  成  者       :  [笈川]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

Public Class LMG530DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                              " & vbNewLine _
                                            & "	HED.NRS_BR_CD                                           AS NRS_BR_CD        " & vbNewLine _
                                            & ",'54'                                                     AS PTN_ID          " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                            " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                           " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD          " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                            " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                       " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID          " & vbNewLine _
                                            & "FROM $LM_TRN$..G_KAGAMI_HED AS HED                                           " & vbNewLine _
                                            & " LEFT OUTER JOIN                                                                                                     " & vbNewLine _
                                            & "(                                                                                                                    " & vbNewLine _
                                            & "SELECT SKYPRT.*,MC.HOKAN_SEIQTO_CD,MC.NIYAKU_SEIQTO_CD,MC.CUST_CD_L,MC.CUST_CD_M,MC.CUST_CD_S FROM                   " & vbNewLine _
                                            & "$LM_TRN$..G_SEKY_MEISAI_PRT SKYPRT                                                                                   " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                                       " & vbNewLine _
                                            & "ON SKYPRT.NRS_BR_CD = MG.NRS_BR_CD and SKYPRT.GOODS_CD_NRS = MG.GOODS_CD_NRS                                         " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST MC                                                                                        " & vbNewLine _
                                            & "ON MG.NRS_BR_CD = MC.NRS_BR_CD and MG.CUST_CD_L = MC.CUST_CD_L                                                       " & vbNewLine _
                                            & "AND MG.CUST_CD_M = MC.CUST_CD_M and MG.CUST_CD_S = MC.CUST_CD_S                                                      " & vbNewLine _
                                            & "AND MG.CUST_CD_SS = MC.CUST_CD_SS                                                                                    " & vbNewLine _
                                            & ") BMC                                                                                                                " & vbNewLine _
                                            & "  ON BMC.NRS_BR_CD = HED.NRS_BR_CD                                                                                   " & vbNewLine _
                                            & "  AND (BMC.HOKAN_SEIQTO_CD = HED.SEIQTO_CD or BMC.NIYAKU_SEIQTO_CD = HED.SEIQTO_CD )                                 " & vbNewLine _
                                            & "  AND BMC.INV_DATE_TO = HED.SKYU_DATE                                                                                " & vbNewLine _
                                            & "  AND BMC.SEKY_FLG = '00'                                                                                            " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_CUST_RPT AS MCR1                                      " & vbNewLine _
                                            & "  ON  HED.NRS_BR_CD = MCR1.NRS_BR_CD                                         " & vbNewLine _
                                            & "  AND BMC.CUST_CD_L = MCR1.CUST_CD_L                                         " & vbNewLine _
                                            & "  AND BMC.CUST_CD_M = MCR1.CUST_CD_M                                         " & vbNewLine _
                                            & "  AND '00' = MCR1.CUST_CD_S                                                  " & vbNewLine _
                                            & "  AND MCR1.PTN_ID = '54'                                                     " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_RPT AS MR1                                            " & vbNewLine _
                                            & "  ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                         " & vbNewLine _
                                            & "  AND MR1.PTN_ID = MCR1.PTN_ID                                               " & vbNewLine _
                                            & "  AND MR1.PTN_CD = MCR1.PTN_CD                                               " & vbNewLine _
                                            & "  AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_CUST_RPT AS MCR2                                      " & vbNewLine _
                                            & "  ON  HED.NRS_BR_CD = MCR2.NRS_BR_CD                                         " & vbNewLine _
                                            & "  AND BMC.CUST_CD_L = MCR2.CUST_CD_L                                         " & vbNewLine _
                                            & "  AND BMC.CUST_CD_M = MCR2.CUST_CD_M                                         " & vbNewLine _
                                            & "  AND BMC.CUST_CD_S = MCR2.CUST_CD_S                                         " & vbNewLine _
                                            & "  AND MCR2.PTN_ID = '54'                                                     " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_RPT AS MR2                                            " & vbNewLine _
                                            & "  ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                         " & vbNewLine _
                                            & "  AND MR2.PTN_ID = MCR2.PTN_ID                                               " & vbNewLine _
                                            & "  AND MR2.PTN_CD = MCR2.PTN_CD                                               " & vbNewLine _
                                            & "  AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_RPT AS MR3                                            " & vbNewLine _
                                            & "  ON  MR3.NRS_BR_CD = HED.NRS_BR_CD                                          " & vbNewLine _
                                            & "  AND MR3.PTN_ID = '54'                                                      " & vbNewLine _
                                            & "  AND MR3.STANDARD_FLAG = '01'                                               " & vbNewLine _
                                            & "  AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                            & "WHERE HED.SKYU_NO = '@SKYU_NO'                                               " & vbNewLine




    ''' <summary>
    ''' 印刷データ抽出用SELCT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT	                                                      " & vbNewLine _
                                        & "    CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID               " & vbNewLine _
                                        & "         WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID               " & vbNewLine _
                                        & "         ELSE MR3.RPT_ID END                  AS RPT_ID            " & vbNewLine _
                                        & "   ,TBL.SKYU_DATE                             AS SKYU_DATE         " & vbNewLine _
                                        & "   ,TBL.SEIQTO_CD                             AS SEIQTO_CD         " & vbNewLine _
                                        & "   ,TBL.SEIQTO_NM                             AS SEIQTO_NM         " & vbNewLine _
                                        & "   ,@SKYU_NO                                 AS SKYU_NO            " & vbNewLine _
                                        & "   ,TBL.JOB_NO                                AS JOB_NO            " & vbNewLine _
                                        & "   ,TBL.CUST_CD_L                             AS CUST_CD_L         " & vbNewLine _
                                        & "   ,TBL.CUST_CD_M                             AS CUST_CD_M         " & vbNewLine _
                                        & "   ,TBL.CUST_CD_S                             AS CUST_CD_S         " & vbNewLine _
                                        & "   ,TBL.CUST_CD_SS                            AS CUST_CD_SS        " & vbNewLine _
                                        & "   ,TBL.CUST_NM_L                             AS CUST_NM_L         " & vbNewLine _
                                        & "   ,TBL.CUST_NM_M                             AS CUST_NM_M         " & vbNewLine _
                                        & "   ,TBL.CUST_NM_S                             AS CUST_NM_S         " & vbNewLine _
                                        & "   ,TBL.CUST_NM_SS                            AS CUST_NM_SS        " & vbNewLine _
                                        & "   ,TBL.STORAGE_AMO_TTL                       AS STORAGE_AMO_TTL   " & vbNewLine _
                                        & "   ,TBL.HANDLING_AMO_TTL                      AS HANDLING_AMO_TTL  " & vbNewLine _
                                        & "FROM $LM_TRN$..G_KAGAMI_HED          AS HED                        " & vbNewLine _
                                        & "     INNER JOIN                                                    " & vbNewLine _
                                        & "     (SELECT                                                       " & vbNewLine _
                                        & "         TBL.SKYU_DATE             AS SKYU_DATE                    " & vbNewLine _
                                        & "        ,TBL.SEIQTO_CD             AS SEIQTO_CD                    " & vbNewLine _
                                        & "        ,TBL.SEIQTO_NM             AS SEIQTO_NM                    " & vbNewLine _
                                        & "        ,TBL.JOB_NO                AS JOB_NO                       " & vbNewLine _
                                        & "        ,TBL.CUST_CD_L             AS CUST_CD_L                    " & vbNewLine _
                                        & "        ,TBL.CUST_CD_M             AS CUST_CD_M                    " & vbNewLine _
                                        & "        ,TBL.CUST_CD_S             AS CUST_CD_S                    " & vbNewLine _
                                        & "        ,TBL.CUST_CD_SS            AS CUST_CD_SS                   " & vbNewLine _
                                        & "        ,TBL.CUST_NM_L             AS CUST_NM_L                    " & vbNewLine _
                                        & "        ,TBL.CUST_NM_M             AS CUST_NM_M                    " & vbNewLine _
                                        & "        ,TBL.CUST_NM_S             AS CUST_NM_S                    " & vbNewLine _
                                        & "        ,TBL.CUST_NM_SS            AS CUST_NM_SS                   " & vbNewLine _
                                        & "        ,SUM(TBL.STORAGE_AMO_TTL)  AS STORAGE_AMO_TTL              " & vbNewLine _
                                        & "        ,SUM(TBL.HANDLING_AMO_TTL) AS HANDLING_AMO_TTL             " & vbNewLine _
                                        & "        ,TBL.SKYU_NO               AS SKYU_NO                      " & vbNewLine _
                                        & "     FROM                                                          " & vbNewLine _
                                        & "        (SELECT                                                    " & vbNewLine _
                                        & "             HED.SKYU_DATE        AS SKYU_DATE                     " & vbNewLine _
                                        & "            ,HED.SEIQTO_CD        AS SEIQTO_CD                     " & vbNewLine _
                                        & "            ,HED.SEIQTO_NM        AS SEIQTO_NM                     " & vbNewLine _
                                        & "            ,TBL.JOB_NO           AS JOB_NO                        " & vbNewLine _
                                        & "            ,TBL.CUST_CD_L        AS CUST_CD_L                     " & vbNewLine _
                                        & "            ,TBL.CUST_CD_M        AS CUST_CD_M                     " & vbNewLine _
                                        & "            ,TBL.CUST_CD_S        AS CUST_CD_S                     " & vbNewLine _
                                        & "            ,TBL.CUST_CD_SS       AS CUST_CD_SS                    " & vbNewLine _
                                        & "            ,TBL.CUST_NM_L        AS CUST_NM_L                     " & vbNewLine _
                                        & "            ,TBL.CUST_NM_M        AS CUST_NM_M                     " & vbNewLine _
                                        & "            ,TBL.CUST_NM_S        AS CUST_NM_S                     " & vbNewLine _
                                        & "            ,TBL.CUST_NM_SS       AS CUST_NM_SS                    " & vbNewLine _
                                        & "            ,TBL.STORAGE_AMO_TTL  AS STORAGE_AMO_TTL               " & vbNewLine _
                                        & "            ,0                    AS HANDLING_AMO_TTL              " & vbNewLine _
                                        & "            ,HED.SKYU_NO          AS SKYU_NO                       " & vbNewLine _
                                        & "         FROM $LM_TRN$..G_KAGAMI_HED    AS HED                     " & vbNewLine _
                                        & "              INNER JOIN                                           " & vbNewLine _
                                        & "                 (SELECT                                           " & vbNewLine _
                                        & "                      SKYPRT.NRS_BR_CD                             " & vbNewLine _
                                        & "                     ,SKYPRT.GOODS_CD_NRS                          " & vbNewLine _
                                        & "                     ,SKYPRT.INV_DATE_TO                           " & vbNewLine _
                                        & "                     ,SKYPRT.JOB_NO                                " & vbNewLine _
                                        & "                     ,SKYPRT.STORAGE_AMO_TTL                       " & vbNewLine _
                                        & "                     ,TBL.CUST_CD_L          AS CUST_CD_L          " & vbNewLine _
                                        & "                     ,TBL.CUST_CD_M          AS CUST_CD_M          " & vbNewLine _
                                        & "                     ,TBL.CUST_CD_S          AS CUST_CD_S          " & vbNewLine _
                                        & "                     ,TBL.CUST_CD_SS         AS CUST_CD_SS         " & vbNewLine _
                                        & "                     ,TBL.CUST_NM_L          AS CUST_NM_L          " & vbNewLine _
                                        & "                     ,TBL.CUST_NM_M          AS CUST_NM_M          " & vbNewLine _
                                        & "                     ,TBL.CUST_NM_S          AS CUST_NM_S          " & vbNewLine _
                                        & "                     ,TBL.CUST_NM_SS         AS CUST_NM_SS         " & vbNewLine _
                                        & "                     ,TBL.HOKAN_SEIQTO_CD    AS HOKAN_SEIQTO_CD    " & vbNewLine _
                                        & "                  FROM $LM_TRN$..G_SEKY_MEISAI_PRT  AS  SKYPRT     " & vbNewLine _
                                        & "                       LEFT OUTER JOIN                             " & vbNewLine _
                                        & "                           (SELECT                                 " & vbNewLine _
                                        & "                                GOODS.GOODS_CD_NRS AS GOODS_CD_NRS " & vbNewLine _
                                        & "                               ,GOODS.NRS_BR_CD    AS NRS_BR_CD    " & vbNewLine _
                                        & "                               ,GOODS.CUST_CD_L    AS CUST_CD_L    " & vbNewLine _
                                        & "                               ,GOODS.CUST_CD_M    AS CUST_CD_M    " & vbNewLine _
                                        & "                               ,GOODS.CUST_CD_S    AS CUST_CD_S    " & vbNewLine _
                                        & "                               ,GOODS.CUST_CD_SS   AS CUST_CD_SS   " & vbNewLine _
                                        & "                               ,CUST.CUST_NM_L                     " & vbNewLine _
                                        & "                               ,CUST.CUST_NM_M                     " & vbNewLine _
                                        & "                               ,CUST.CUST_NM_S                     " & vbNewLine _
                                        & "                               ,CUST.CUST_NM_SS                    " & vbNewLine _
                                        & "                               ,CUST.HOKAN_SEIQTO_CD               " & vbNewLine _
                                        & "                            FROM $LM_MST$..M_GOODS   AS GOODS      " & vbNewLine _
                                        & "                                 LEFT OUTER JOIN                   " & vbNewLine _
                                        & "                                     (SELECT                       " & vbNewLine _
                                        & "                                          CUST.HOKAN_SEIQTO_CD     " & vbNewLine _
                                        & "                                         ,CUST.NRS_BR_CD           " & vbNewLine _
                                        & "                                         ,CUST.CUST_CD_L           " & vbNewLine _
                                        & "                                         ,CUST.CUST_CD_M           " & vbNewLine _
                                        & "                                         ,CUST.CUST_CD_S           " & vbNewLine _
                                        & "                                         ,CUST.CUST_CD_SS          " & vbNewLine _
                                        & "                                         ,CUST.CUST_NM_L           " & vbNewLine _
                                        & "                                         ,CUST.CUST_NM_M           " & vbNewLine _
                                        & "                                         ,CUST.CUST_NM_S           " & vbNewLine _
                                        & "                                         ,CUST.CUST_NM_SS          " & vbNewLine _
                                        & "                                      FROM $LM_MST$..M_CUST CUST )  AS  CUST       " & vbNewLine _
                                        & "                                  ON CUST.NRS_BR_CD  = GOODS.NRS_BR_CD             " & vbNewLine _
                                        & "                                 AND CUST.CUST_CD_L  = GOODS.CUST_CD_L             " & vbNewLine _
                                        & "                                 AND CUST.CUST_CD_M  = GOODS.CUST_CD_M             " & vbNewLine _
                                        & "                                 AND CUST.CUST_CD_S  = GOODS.CUST_CD_S             " & vbNewLine _
                                        & "                                 AND CUST.CUST_CD_SS = GOODS.CUST_CD_SS )  AS  TBL " & vbNewLine _
                                        & "                       ON TBL.GOODS_CD_NRS = SKYPRT.GOODS_CD_NRS                   " & vbNewLine _
                                        & "                      AND TBL.NRS_BR_CD    = SKYPRT.NRS_BR_CD                      " & vbNewLine _
                                        & "                  WHERE                                            " & vbNewLine _
                                        & "                      SKYPRT.SEKY_FLG  =  '00'  )  AS  TBL         " & vbNewLine _
                                        & "                ON TBL.NRS_BR_CD   = HED.NRS_BR_CD                 " & vbNewLine _
                                        & "               AND TBL.INV_DATE_TO = HED.SKYU_DATE                 " & vbNewLine _
                                        & "         WHERE HED.SKYU_NO   = @SKYU_NO                            " & vbNewLine _
                                        & "           AND HED.SEIQTO_CD = TBL.HOKAN_SEIQTO_CD                 " & vbNewLine _
                                        & "        UNION ALL                                                  " & vbNewLine _
                                        & "         SELECT                                                    " & vbNewLine _
                                        & "             HED.SKYU_DATE        AS SKYU_DATE                     " & vbNewLine _
                                        & "            ,HED.SEIQTO_CD        AS SEIQTO_CD                     " & vbNewLine _
                                        & "            ,HED.SEIQTO_NM        AS SEIQTO_NM                     " & vbNewLine _
                                        & "            ,TBL.JOB_NO           AS JOB_NO                        " & vbNewLine _
                                        & "            ,TBL.CUST_CD_L        AS CUST_CD_L                     " & vbNewLine _
                                        & "            ,TBL.CUST_CD_M        AS CUST_CD_M                     " & vbNewLine _
                                        & "            ,TBL.CUST_CD_S        AS CUST_CD_S                     " & vbNewLine _
                                        & "            ,TBL.CUST_CD_SS       AS CUST_CD_SS                    " & vbNewLine _
                                        & "            ,TBL.CUST_NM_L        AS CUST_NM_L                     " & vbNewLine _
                                        & "            ,TBL.CUST_NM_M        AS CUST_NM_M                     " & vbNewLine _
                                        & "            ,TBL.CUST_NM_S        AS CUST_NM_S                     " & vbNewLine _
                                        & "            ,TBL.CUST_NM_SS       AS CUST_NM_SS                    " & vbNewLine _
                                        & "            ,0                    AS STORAGE_AMO_TTL               " & vbNewLine _
                                        & "            ,TBL.HANDLING_AMO_TTL AS HANDLING_AMO_TTL              " & vbNewLine _
                                        & "            ,HED.SKYU_NO          AS SKYU_NO                       " & vbNewLine _
                                        & "         FROM $LM_TRN$..G_KAGAMI_HED    AS HED                     " & vbNewLine _
                                        & "              INNER JOIN                                           " & vbNewLine _
                                        & "                 (SELECT                                           " & vbNewLine _
                                        & "                      SKYPRT.NRS_BR_CD                             " & vbNewLine _
                                        & "                     ,SKYPRT.GOODS_CD_NRS                          " & vbNewLine _
                                        & "                     ,SKYPRT.INV_DATE_TO                           " & vbNewLine _
                                        & "                     ,SKYPRT.JOB_NO                                " & vbNewLine _
                                        & "                     ,SKYPRT.HANDLING_AMO_TTL                      " & vbNewLine _
                                        & "                     ,TBL.CUST_CD_L           AS CUST_CD_L         " & vbNewLine _
                                        & "                     ,TBL.CUST_CD_M           AS CUST_CD_M         " & vbNewLine _
                                        & "                     ,TBL.CUST_CD_S           AS CUST_CD_S         " & vbNewLine _
                                        & "                     ,TBL.CUST_CD_SS          AS CUST_CD_SS        " & vbNewLine _
                                        & "                     ,TBL.CUST_NM_L           AS CUST_NM_L         " & vbNewLine _
                                        & "                     ,TBL.CUST_NM_M           AS CUST_NM_M         " & vbNewLine _
                                        & "                     ,TBL.CUST_NM_S           AS CUST_NM_S         " & vbNewLine _
                                        & "                     ,TBL.CUST_NM_SS          AS CUST_NM_SS        " & vbNewLine _
                                        & "                     ,TBL.NIYAKU_SEIQTO_CD    AS NIYAKU_SEIQTO_CD  " & vbNewLine _
                                        & "                  FROM $LM_TRN$..G_SEKY_MEISAI_PRT  AS  SKYPRT     " & vbNewLine _
                                        & "                       LEFT OUTER JOIN                             " & vbNewLine _
                                        & "                           (SELECT                                 " & vbNewLine _
                                        & "                                GOODS.GOODS_CD_NRS AS GOODS_CD_NRS " & vbNewLine _
                                        & "                               ,GOODS.NRS_BR_CD    AS NRS_BR_CD    " & vbNewLine _
                                        & "                               ,GOODS.CUST_CD_L    AS CUST_CD_L    " & vbNewLine _
                                        & "                               ,GOODS.CUST_CD_M    AS CUST_CD_M    " & vbNewLine _
                                        & "                               ,GOODS.CUST_CD_S    AS CUST_CD_S    " & vbNewLine _
                                        & "                               ,GOODS.CUST_CD_SS   AS CUST_CD_SS   " & vbNewLine _
                                        & "                               ,CUST.CUST_NM_L                     " & vbNewLine _
                                        & "                               ,CUST.CUST_NM_M                     " & vbNewLine _
                                        & "                               ,CUST.CUST_NM_S                     " & vbNewLine _
                                        & "                               ,CUST.CUST_NM_SS                    " & vbNewLine _
                                        & "                               ,CUST.NIYAKU_SEIQTO_CD              " & vbNewLine _
                                        & "                            FROM $LM_MST$..M_GOODS    AS GOODS     " & vbNewLine _
                                        & "                                 LEFT OUTER JOIN                   " & vbNewLine _
                                        & "                                     (SELECT                       " & vbNewLine _
                                        & "                                          CUST.NIYAKU_SEIQTO_CD    " & vbNewLine _
                                        & "                                         ,CUST.NRS_BR_CD           " & vbNewLine _
                                        & "                                         ,CUST.CUST_CD_L           " & vbNewLine _
                                        & "                                         ,CUST.CUST_CD_M           " & vbNewLine _
                                        & "                                         ,CUST.CUST_CD_S           " & vbNewLine _
                                        & "                                         ,CUST.CUST_CD_SS          " & vbNewLine _
                                        & "                                         ,CUST.CUST_NM_L           " & vbNewLine _
                                        & "                                         ,CUST.CUST_NM_M           " & vbNewLine _
                                        & "                                         ,CUST.CUST_NM_S           " & vbNewLine _
                                        & "                                         ,CUST.CUST_NM_SS          " & vbNewLine _
                                        & "                                      FROM $LM_MST$..M_CUST CUST  )  AS  CUST      " & vbNewLine _
                                        & "                                   ON CUST.NRS_BR_CD  = GOODS.NRS_BR_CD            " & vbNewLine _
                                        & "                                  AND CUST.CUST_CD_L  = GOODS.CUST_CD_L            " & vbNewLine _
                                        & "                                  AND CUST.CUST_CD_M  = GOODS.CUST_CD_M            " & vbNewLine _
                                        & "                                  AND CUST.CUST_CD_S  = GOODS.CUST_CD_S            " & vbNewLine _
                                        & "                                  AND CUST.CUST_CD_SS = GOODS.CUST_CD_SS ) AS TBL  " & vbNewLine _
                                        & "                        ON TBL.GOODS_CD_NRS = SKYPRT.GOODS_CD_NRS                  " & vbNewLine _
                                        & "                       AND TBL.NRS_BR_CD    = SKYPRT.NRS_BR_CD                     " & vbNewLine _
                                        & "                  WHERE                                            " & vbNewLine _
                                        & "                      SKYPRT.SEKY_FLG  =  '00'  )  AS  TBL         " & vbNewLine _
                                        & "                ON TBL.NRS_BR_CD   = HED.NRS_BR_CD                 " & vbNewLine _
                                        & "               AND TBL.INV_DATE_TO = HED.SKYU_DATE                 " & vbNewLine _
                                        & "         WHERE HED.SKYU_NO   = @SKYU_NO                            " & vbNewLine _
                                        & "           AND HED.SEIQTO_CD = TBL.NIYAKU_SEIQTO_CD)TBL            " & vbNewLine _
                                        & "        GROUP BY                                                   " & vbNewLine _
                                        & "          TBL.SKYU_DATE                                            " & vbNewLine _
                                        & "         ,TBL.SEIQTO_CD                                            " & vbNewLine _
                                        & "         ,TBL.SEIQTO_NM                                            " & vbNewLine _
                                        & "         ,TBL.SKYU_NO                                              " & vbNewLine _
                                        & "         ,TBL.JOB_NO                                               " & vbNewLine _
                                        & "         ,TBL.CUST_CD_L                                            " & vbNewLine _
                                        & "         ,TBL.CUST_CD_M                                            " & vbNewLine _
                                        & "         ,TBL.CUST_CD_S                                            " & vbNewLine _
                                        & "         ,TBL.CUST_CD_SS                                           " & vbNewLine _
                                        & "         ,TBL.CUST_NM_L                                            " & vbNewLine _
                                        & "         ,TBL.CUST_NM_M                                            " & vbNewLine _
                                        & "         ,TBL.CUST_NM_S                                            " & vbNewLine _
                                        & "         ,TBL.CUST_NM_SS                                           " & vbNewLine _
                                        & "        )TBL                                                       " & vbNewLine _
                                        & "     ON HED.SKYU_NO = TBL.SKYU_NO                                  " & vbNewLine _
                                        & "    LEFT JOIN $LM_MST$..M_CUST_RPT  AS  MCR1                       " & vbNewLine _
                                        & "     ON  HED.NRS_BR_CD = MCR1.NRS_BR_CD                            " & vbNewLine _
                                        & "     AND TBL.CUST_CD_L = MCR1.CUST_CD_L                            " & vbNewLine _
                                        & "     AND TBL.CUST_CD_M = MCR1.CUST_CD_M                            " & vbNewLine _
                                        & "     AND '00'          = MCR1.CUST_CD_S                            " & vbNewLine _
                                        & "     AND MCR1.PTN_ID   = '54'                                      " & vbNewLine _
                                        & "    LEFT JOIN $LM_MST$..M_RPT AS MR1                               " & vbNewLine _
                                        & "     ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                            " & vbNewLine _
                                        & "     AND MR1.PTN_ID    = MCR1.PTN_ID                               " & vbNewLine _
                                        & "     AND MR1.PTN_CD    = MCR1.PTN_CD                               " & vbNewLine _
                                        & "     AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                        & "    LEFT JOIN $LM_MST$..M_CUST_RPT AS MCR2                         " & vbNewLine _
                                        & "     ON  HED.NRS_BR_CD = MCR2.NRS_BR_CD                            " & vbNewLine _
                                        & "     AND TBL.CUST_CD_L = MCR2.CUST_CD_L                            " & vbNewLine _
                                        & "     AND TBL.CUST_CD_M = MCR2.CUST_CD_M                            " & vbNewLine _
                                        & "     AND TBL.CUST_CD_S = MCR2.CUST_CD_S                            " & vbNewLine _
                                        & "     AND MCR2.PTN_ID   = '54'                                      " & vbNewLine _
                                        & "    LEFT JOIN $LM_MST$..M_RPT AS MR2                               " & vbNewLine _
                                        & "     ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                            " & vbNewLine _
                                        & "     AND MR2.PTN_ID    = MCR2.PTN_ID                               " & vbNewLine _
                                        & "     AND MR2.PTN_CD    = MCR2.PTN_CD                               " & vbNewLine _
                                        & "     AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                        & "    LEFT JOIN $LM_MST$..M_RPT AS MR3                               " & vbNewLine _
                                        & "     ON  MR3.NRS_BR_CD = HED.NRS_BR_CD                             " & vbNewLine _
                                        & "     AND MR3.PTN_ID    = '54'                                      " & vbNewLine _
                                        & "     AND MR3.STANDARD_FLAG = '01'                                  " & vbNewLine _
                                        & "     AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                        & "WHERE TBL.SKYU_NO = @SKYU_NO                                       " & vbNewLine _
                                        & "ORDER BY TBL.SKYU_DATE                                             " & vbNewLine _
                                        & "        ,TBL.SEIQTO_CD                                             " & vbNewLine _
                                        & "        ,TBL.CUST_CD_L                                             " & vbNewLine _
                                        & "        ,TBL.CUST_CD_M                                             " & vbNewLine _
                                        & "        ,TBL.CUST_CD_S                                             " & vbNewLine _
                                        & "        ,TBL.CUST_CD_SS                                            " & vbNewLine

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

#Region "検索処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG530IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Dim wherestr As String = Me._Row.Item("SKYU_NO").ToString()
        Me._StrSql.Append(LMG530DAC.SQL_SELECT_MPrt.Replace("@SKYU_NO", wherestr))      'SQL構築(帳票種別用Select)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        MyBase.Logger.WriteSQLLog("LMG530DAC", "SelectMPrt", cmd)

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
    ''' 請求鑑取込チェックリスト出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑取込チェックリスト出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG530IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG530DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用)
        Call Me.SetConditionPrtDataSQL()                  '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG530DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("SKYU_DATE", "SKYU_DATE")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("SKYU_NO", "SKYU_NO")
        map.Add("JOB_NO", "JOB_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("STORAGE_AMO_TTL", "STORAGE_AMO_TTL")
        map.Add("HANDLING_AMO_TTL", "HANDLING_AMO_TTL")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG530OUT")

        Return ds

    End Function

#End Region

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

#Region "パラメタ設定"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール（請求鑑取込チェックリスト出力対象データ検索用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrtDataSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", Me._Row.Item("SKYU_NO").ToString(), DBDataType.CHAR))

    End Sub

#End Region

End Class
