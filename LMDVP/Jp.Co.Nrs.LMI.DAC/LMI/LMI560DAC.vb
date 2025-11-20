' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI560DAC : TSMC請求データ計算
'  作  成  者       :  [HORI]
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI560DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI560DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' データ検索(1/3)
    ''' </summary>
    Private Const SQL_SELECT_SEARCH_1 As String = "" _
        & "SELECT               " & vbNewLine _
        & "   SEI.SEIQTO_CD     " & vbNewLine _
        & "  ,SEI.SEIQTO_NM     " & vbNewLine _
        & "  ,SEI.CLOSE_KB      " & vbNewLine _
        & "  ,CST.LAST_DATE     " & vbNewLine _
        & "  ,CST.LAST_JOB_NO   " & vbNewLine _
        & "  ,CST.BEFORE_DATE   " & vbNewLine _
        & "  ,CST.BEFORE_JOB_NO " & vbNewLine

    ''' <summary>
    ''' データ検索(2/3)
    ''' </summary>
    Private Const SQL_SELECT_SEARCH_2 As String = "" _
        & "FROM                                                                                                                     " & vbNewLine _
        & "  $LM_MST$..M_SEIQTO AS SEI                                                                                              " & vbNewLine _
        & "LEFT JOIN                                                                                                                " & vbNewLine _
        & "  (                                                                                                                      " & vbNewLine _
        & "    SELECT                                                                                                               " & vbNewLine _
        & "       NRS_BR_CD                                                                                                         " & vbNewLine _
        & "      ,OYA_SEIQTO_CD                                                                                                     " & vbNewLine _
        & "      ,HOKAN_NIYAKU_CALCULATION AS LAST_DATE                                                                             " & vbNewLine _
        & "      ,NEW_JOB_NO AS LAST_JOB_NO                                                                                         " & vbNewLine _
        & "      ,HOKAN_NIYAKU_CALCULATION_OLD AS BEFORE_DATE                                                                       " & vbNewLine _
        & "      ,OLD_JOB_NO AS BEFORE_JOB_NO                                                                                       " & vbNewLine _
        & "      ,ROW_NUMBER() OVER (PARTITION BY NRS_BR_CD, OYA_SEIQTO_CD                                                          " & vbNewLine _
        & "                          ORDER BY NEW_JOB_NO DESC, HOKAN_NIYAKU_CALCULATION DESC) AS ROW_NO                             " & vbNewLine _
        & "      ,CUST_CD_L                                                                                                         " & vbNewLine _
        & "      ,CUST_CD_M                                                                                                         " & vbNewLine _
        & "    FROM                                                                                                                 " & vbNewLine _
        & "      $LM_MST$..M_CUST                                                                                                   " & vbNewLine _
        & "    WHERE                                                                                                                " & vbNewLine _
        & "      SYS_DEL_FLG = '0'                                                                                                  " & vbNewLine _
        & "  ) AS CST                                                                                                               " & vbNewLine _
        & "  ON                                                                                                                     " & vbNewLine _
        & "        CST.NRS_BR_CD = SEI.NRS_BR_CD                                                                                    " & vbNewLine _
        & "    AND CST.OYA_SEIQTO_CD = SEI.SEIQTO_CD                                                                                " & vbNewLine _
        & "    AND CST.ROW_NO = 1                                                                                                   " & vbNewLine _
        & "    AND CST.LAST_DATE < CASE SEI.CLOSE_KB                                                                                " & vbNewLine _
        & "        WHEN '00' THEN FORMAT(DATEADD(DAY, -1, DATEADD(MONTH, 1, CONVERT(DATE, CONCAT(@INV_DATE, '01')))), 'yyyyMMdd')   " & vbNewLine _
        & "        ELSE CONCAT(@INV_DATE, SEI.CLOSE_KB)                                                                             " & vbNewLine _
        & "        END                                                                                                              " & vbNewLine _
        & "WHERE                                                                                                                    " & vbNewLine _
        & "      SEI.NRS_BR_CD = @NRS_BR_CD                                                                                         " & vbNewLine _
        & "  AND SEI.SYS_DEL_FLG = '0'                                                                                              " & vbNewLine _
        & "  AND CST.OYA_SEIQTO_CD IS NOT NULL                                                                                      " & vbNewLine _
        & "  AND CST.LAST_DATE <> ''                                                                                                " & vbNewLine

    ''' <summary>
    ''' データ検索(3/3)
    ''' </summary>
    Private Const SQL_SELECT_SEARCH_3 As String = "" _
        & "ORDER BY                 " & vbNewLine _
        & "   CST.LAST_DATE DESC    " & vbNewLine _
        & "  ,SEI.SEIQTO_CD         " & vbNewLine

    ''' <summary>
    ''' データ件数取得
    ''' </summary>
    ''' <remarks>SQL_SELECT_SEARCH_1と差し替えて利用</remarks>
    Private Const SQL_SELECT_SEARCH_CNT As String = "" _
        & "SELECT                   " & vbNewLine _
        & "  COUNT(*) AS SELECT_CNT " & vbNewLine

#End Region

#Region "前回計算取消 SQL"

    ''' <summary>
    ''' 前々回情報の取得
    ''' </summary>
    Private Const SQL_SELECT_OLD_INFO As String = "" _
        & "SELECT                                   " & vbNewLine _
        & "   @NRS_BR_CD AS NRS_BR_CD               " & vbNewLine _
        & "  ,@SEIQTO_CD AS SEIQTO_CD               " & vbNewLine _
        & "  ,@LAST_DATE AS LAST_DATE               " & vbNewLine _
        & "  ,@LAST_JOB_NO AS LAST_JOB_NO           " & vbNewLine _
        & "  ,@BEFORE_DATE AS BEFORE_DATE           " & vbNewLine _
        & "  ,@BEFORE_JOB_NO AS BEFORE_JOB_NO       " & vbNewLine _
        & "  ,SMT.OLD_DATE                          " & vbNewLine _
        & "  ,SMT.OLD_JOB_NO                        " & vbNewLine _
        & "  ,@BEFORE_DATE_YOBI AS BEFORE_DATE_YOBI " & vbNewLine _
        & "  ,@LINE_NO AS LINE_NO                   " & vbNewLine _
        & "FROM                                     " & vbNewLine _
        & "  (                                      " & vbNewLine _
        & "    SELECT TOP 1                         " & vbNewLine _
        & "       INV_DATE_TO AS OLD_DATE           " & vbNewLine _
        & "      ,JOB_NO AS OLD_JOB_NO              " & vbNewLine _
        & "    FROM                                 " & vbNewLine _
        & "      $LM_TRN$..I_SEKY_MEISAI_TSMC       " & vbNewLine _
        & "    WHERE                                " & vbNewLine _
        & "          NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
        & "      AND SEIQTO_CD = @SEIQTO_CD         " & vbNewLine _
        & "      AND INV_DATE_TO < @BEFORE_DATE     " & vbNewLine _
        & "    ORDER BY                             " & vbNewLine _
        & "      JOB_NO DESC                        " & vbNewLine _
        & "  ) AS SMT                               " & vbNewLine

    ''' <summary>
    ''' 荷主マスタ更新
    ''' </summary>
    Private Const SQL_UPDATE_CUST_CANCEL As String = "" _
        & "UPDATE $LM_MST$..M_CUST                                          " & vbNewLine _
        & "SET                                                              " & vbNewLine _
        & "   HOKAN_NIYAKU_CALCULATION = @HOKAN_NIYAKU_CALCULATION          " & vbNewLine _
        & "  ,HOKAN_NIYAKU_CALCULATION_OLD = @HOKAN_NIYAKU_CALCULATION_OLD  " & vbNewLine _
        & "  ,NEW_JOB_NO = @NEW_JOB_NO                                      " & vbNewLine _
        & "  ,OLD_JOB_NO = @OLD_JOB_NO                                      " & vbNewLine _
        & "  ,SYS_UPD_DATE = @SYS_UPD_DATE                                  " & vbNewLine _
        & "  ,SYS_UPD_TIME = @SYS_UPD_TIME                                  " & vbNewLine _
        & "  ,SYS_UPD_PGID = @SYS_UPD_PGID                                  " & vbNewLine _
        & "  ,SYS_UPD_USER = @SYS_UPD_USER                                  " & vbNewLine _
        & "WHERE                                                            " & vbNewLine _
        & "      NRS_BR_CD = @NRS_BR_CD                                     " & vbNewLine _
        & "  AND OYA_SEIQTO_CD = @SEIQTO_CD                                 " & vbNewLine _
        & "  AND SYS_DEL_FLG = '0'                                          " & vbNewLine

    ''' <summary>
    ''' TSMC在庫データ更新
    ''' </summary>
    Private Const SQL_UPDATE_ZAI_TSMC_CANCEL As String = "" _
        & "UPDATE $LM_TRN$..D_ZAI_TSMC                              " & vbNewLine _
        & "SET                                                      " & vbNewLine _
        & "   LAST_INV_DATE = @LAST_INV_DATE                        " & vbNewLine _
        & "  ,LAST_CLC_DATE = @LAST_CLC_DATE                        " & vbNewLine _
        & "  ,SYS_UPD_DATE = @SYS_UPD_DATE                          " & vbNewLine _
        & "  ,SYS_UPD_TIME = @SYS_UPD_TIME                          " & vbNewLine _
        & "  ,SYS_UPD_PGID = @SYS_UPD_PGID                          " & vbNewLine _
        & "  ,SYS_UPD_USER = @SYS_UPD_USER                          " & vbNewLine _
        & "WHERE                                                    " & vbNewLine _
        & "  EXISTS (                                               " & vbNewLine _
        & "    SELECT                                               " & vbNewLine _
        & "      *                                                  " & vbNewLine _
        & "    FROM                                                 " & vbNewLine _
        & "        $LM_MST$..Z_KBN AS KBN                           " & vbNewLine _
        & "      INNER JOIN                                         " & vbNewLine _
        & "        $LM_MST$..M_CUST AS CST                          " & vbNewLine _
        & "        ON                                               " & vbNewLine _
        & "              CST.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
        & "          AND CST.OYA_SEIQTO_CD = @SEIQTO_CD             " & vbNewLine _
        & "          AND CST.CUST_CD_L = D_ZAI_TSMC.CUST_CD_L       " & vbNewLine _
        & "          AND CST.CUST_CD_M = D_ZAI_TSMC.CUST_CD_M       " & vbNewLine _
        & "          AND CST.CUST_CD_S = KBN.KBN_NM3                " & vbNewLine _
        & "          AND CST.CUST_CD_SS = KBN.KBN_NM4               " & vbNewLine _
        & "          AND CST.SYS_DEL_FLG = '0'                      " & vbNewLine _
        & "    WHERE                                                " & vbNewLine _
        & "          KBN.KBN_GROUP_CD = 'K038'                      " & vbNewLine _
        & "      AND KBN.KBN_NM1 = D_ZAI_TSMC.SUPPLY_CD             " & vbNewLine _
        & "      AND KBN.SYS_DEL_FLG = '0'                          " & vbNewLine _
        & "  )                                                      " & vbNewLine _
        & "  AND D_ZAI_TSMC.LAST_INV_DATE = @LAST_INV_DATE_WHERE    " & vbNewLine _
        & "  AND D_ZAI_TSMC.SYS_DEL_FLG = '0'                       " & vbNewLine

    ''' <summary>
    ''' TSMC請求明細データテーブル削除
    ''' </summary>
    Private Const SQL_DELETE_SEKY_MEISAI_TSMC As String = "" _
        & "DELETE FROM                      " & vbNewLine _
        & "  $LM_TRN$..I_SEKY_MEISAI_TSMC   " & vbNewLine _
        & "WHERE                            " & vbNewLine _
        & "      NRS_BR_CD = @NRS_BR_CD     " & vbNewLine _
        & "  AND JOB_NO = @JOB_NO           " & vbNewLine _
        & "  AND SEIQTO_CD = @SEIQTO_CD     " & vbNewLine

#End Region

#Region "実行処理 SQL"

    ''' <summary>
    ''' JOB番号の取得
    ''' </summary>
    Private Const SQL_SELECT_JOB_NO As String = "" _
        & "SELECT                                                                                               " & vbNewLine _
        & "   CONCAT(KBN.KBN_NM6, RIGHT(CONCAT('000000000', CONVERT(VARCHAR, NUM.NOW_VALUE)), 9)) AS JOB_NO     " & vbNewLine _
        & "  ,NUM.NOW_VALUE AS JOB_NO_ORG                                                                       " & vbNewLine _
        & "FROM                                                                                                 " & vbNewLine _
        & "  $LM_MST$..S_NUMBER AS NUM                                                                          " & vbNewLine _
        & "JOIN                                                                                                 " & vbNewLine _
        & "  $LM_MST$..Z_KBN AS KBN                                                                             " & vbNewLine _
        & "  ON                                                                                                 " & vbNewLine _
        & "        KBN.KBN_GROUP_CD = 'D003'                                                                    " & vbNewLine _
        & "    AND KBN.KBN_CD = @NRS_BR_CD                                                                      " & vbNewLine _
        & "WHERE                                                                                                " & vbNewLine _
        & "  NUM.NUMBER_KBN = '47'                                                                              " & vbNewLine

    ''' <summary>
    ''' 請求対象データ検索
    ''' </summary>
    Private Const SQL_SELECT_CALC_DATA As String = "" _
        & "SELECT                                                                                                                               " & vbNewLine _
        & "   ZAI.NRS_BR_CD                                                                                                                     " & vbNewLine _
        & "  ,@JOB_NO AS JOB_NO                                                                                                                 " & vbNewLine _
        & "  ,ZAI.NRS_WH_CD                                                                                                                     " & vbNewLine _
        & "  ,@SEIQTO_CD AS SEIQTO_CD                                                                                                           " & vbNewLine _
        & "  ,@INV_DATE_FROM AS INV_DATE_FROM                                                                                                   " & vbNewLine _
        & "  ,@INV_DATE_TO AS INV_DATE_TO                                                                                                       " & vbNewLine _
        & "  ,ZAI.OUTKA_NO_L                                                                                                                    " & vbNewLine _
        & "  ,GOD.GOODS_CD_NRS                                                                                                                  " & vbNewLine _
        & "  ,ZAI.LOT_NO                                                                                                                        " & vbNewLine _
        & "  ,ZAI.CYLINDER_NO                                                                                                                   " & vbNewLine _
        & "  ,ZAI.PLT_NO                                                                                                                        " & vbNewLine _
        & "  ,ZAI.DEPLT_NO                                                                                                                      " & vbNewLine _
        & "  ,GOD.STD_IRIME_NB AS IRIME                                                                                                         " & vbNewLine _
        & "  ,'' AS TAX_KB                                                                                                                      " & vbNewLine _
        & "  ,0 AS OVER_DATE                                                                                                                    " & vbNewLine _
        & "  ,0 AS SET_AMO                                                                                                                      " & vbNewLine _
        & "  ,0 AS SET_OVER_AMO                                                                                                                 " & vbNewLine _
        & "  ,0 AS SET_AMO_TTL                                                                                                                  " & vbNewLine _
        & "  ,ZAI.INKA_DATE                                                                                                                     " & vbNewLine _
        & "  ,ZAI.OUTKA_PLAN_DATE                                                                                                               " & vbNewLine _
        & "  ,ZAI.GRLVL1_PPNID                                                                                                                  " & vbNewLine _
        & "  ,ZAI.RETURN_FLAG                                                                                                                   " & vbNewLine _
        & "  ,TNK.STORAGE_1 AS SET_TANKA                                                                                                        " & vbNewLine _
        & "  ,TNK.STORAGE_2 AS SET_TANKA_DEPLT                                                                                                  " & vbNewLine _
        & "  ,TNK.HANDLING_IN AS SET_OVER_TANKA                                                                                                 " & vbNewLine _
        & "  ,TNK.MINI_TEKI_IN_AMO AS SET_AMO_DAYS                                                                                              " & vbNewLine _
        & "  ,ZAI.CUST_CD_L                                                                                                                     " & vbNewLine _
        & "  ,ZAI.CUST_CD_M                                                                                                                     " & vbNewLine _
        & "  ,ZAI.CUST_CD_S                                                                                                                     " & vbNewLine _
        & "  ,ZAI.CUST_CD_SS                                                                                                                    " & vbNewLine _
        & "  ,ZAI.CUST_GOODS_CD                                                                                                                 " & vbNewLine _
        & "  ,GOD.UP_GP_CD_1                                                                                                                    " & vbNewLine _
        & "FROM                                                                                                                                 " & vbNewLine _
        & "  --請求先マスタ                                                                                                                     " & vbNewLine _
        & "  $LM_MST$..M_SEIQTO AS SEI                                                                                                          " & vbNewLine _
        & "  --荷主マスタ                                                                                                                       " & vbNewLine _
        & "  LEFT JOIN                                                                                                                          " & vbNewLine _
        & "    $LM_MST$..M_CUST AS CST                                                                                                          " & vbNewLine _
        & "    ON                                                                                                                               " & vbNewLine _
        & "          CST.NRS_BR_CD = SEI.NRS_BR_CD                                                                                              " & vbNewLine _
        & "      AND CST.OYA_SEIQTO_CD = SEI.SEIQTO_CD                                                                                          " & vbNewLine _
        & "      AND CST.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
        & "  --TSMC在庫データ                                                                                                                   " & vbNewLine _
        & "  LEFT JOIN                                                                                                                          " & vbNewLine _
        & "    (                                                                                                                                " & vbNewLine _
        & "      --パレットではないもの                                                                                                         " & vbNewLine _
        & "      SELECT                                                                                                                         " & vbNewLine _
        & "         ZA1.NRS_BR_CD                                                                                                               " & vbNewLine _
        & "        ,ZA1.CUST_CD_L                                                                                                               " & vbNewLine _
        & "        ,ZA1.CUST_CD_M                                                                                                               " & vbNewLine _
        & "        ,ZA1.OUTKA_NO_L                                                                                                              " & vbNewLine _
        & "        ,ZA1.INKA_DATE                                                                                                               " & vbNewLine _
        & "        ,ZA1.OUTKA_PLAN_DATE                                                                                                         " & vbNewLine _
        & "        ,ZA1.CUST_GOODS_CD                                                                                                           " & vbNewLine _
        & "        ,ZA1.NRS_WH_CD                                                                                                               " & vbNewLine _
        & "        ,ZA1.LOT_NO                                                                                                                  " & vbNewLine _
        & "        ,ZA1.CYLINDER_NO                                                                                                             " & vbNewLine _
        & "        ,ZA1.PLT_NO                                                                                                                  " & vbNewLine _
        & "        ,ZA1.DEPLT_NO                                                                                                                " & vbNewLine _
        & "        ,ZA1.GRLVL1_PPNID                                                                                                            " & vbNewLine _
        & "        ,ZA1.RETURN_FLAG                                                                                                             " & vbNewLine _
        & "        ,KB1.KBN_NM3 AS CUST_CD_S                                                                                                    " & vbNewLine _
        & "        ,KB1.KBN_NM4 AS CUST_CD_SS                                                                                                   " & vbNewLine _
        & "        ,ZA1.ASN_NO                                                                                                                  " & vbNewLine _
        & "      FROM                                                                                                                           " & vbNewLine _
        & "        $LM_TRN$..D_ZAI_TSMC AS ZA1                                                                                                  " & vbNewLine _
        & "        LEFT JOIN                                                                                                                    " & vbNewLine _
        & "          $LM_MST$..Z_KBN AS KB1                                                                                                     " & vbNewLine _
        & "          ON                                                                                                                         " & vbNewLine _
        & "                KB1.KBN_GROUP_CD = 'K038'                                                                                            " & vbNewLine _
        & "            AND KB1.KBN_NM1 = ZA1.SUPPLY_CD                                                                                          " & vbNewLine _
        & "            AND KB1.SYS_DEL_FLG = '0'                                                                                                " & vbNewLine _
        & "      WHERE                                                                                                                          " & vbNewLine _
        & "       (   (ZA1.STATUS  = '02'              AND ZA1.RETURN_FLAG = '00' AND ZA1.OUTKA_PLAN_DATE BETWEEN @INV_DATE_FROM AND @INV_DATE_TO) " & vbNewLine _
        & "        OR (ZA1.STATUS IN('02', '03', '04') AND ZA1.RETURN_FLAG = '01' AND ZA1.OUTKA_PLAN_DATE BETWEEN @INV_DATE_FROM AND @INV_DATE_TO) " & vbNewLine _
        & "        OR (ZA1.STATUS  = '05'                                         AND ZA1.OUTKA_PLAN_DATE BETWEEN @INV_DATE_FROM AND @INV_DATE_TO) " & vbNewLine _
        & "        )                                                                                                                            " & vbNewLine _
        & "        AND ZA1.PLT_NO = ''                                                                                                          " & vbNewLine _
        & "        AND ZA1.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
        & "      UNION ALL                                                                                                                      " & vbNewLine _
        & "      --パレットのもの                                                                                                               " & vbNewLine _
        & "      SELECT                                                                                                                         " & vbNewLine _
        & "         ZA2.NRS_BR_CD                                                                                                               " & vbNewLine _
        & "        ,ZA2.CUST_CD_L                                                                                                               " & vbNewLine _
        & "        ,ZA2.CUST_CD_M                                                                                                               " & vbNewLine _
        & "        ,MIN(ZA2.OUTKA_NO_L) AS OUTKA_NO_L                                                                                           " & vbNewLine _
        & "        ,MAX(ZA2.INKA_DATE) AS INKA_DATE                                                                                             " & vbNewLine _
        & "        ,MAX(ZA2.OUTKA_PLAN_DATE) AS OUTKA_PLAN_DATE                                                                                 " & vbNewLine _
        & "        ,MIN(ZA2.CUST_GOODS_CD) AS CUST_GOODS_CD                                                                                     " & vbNewLine _
        & "        ,MIN(ZA2.NRS_WH_CD) AS NRS_WH_CD                                                                                             " & vbNewLine _
        & "        ,MIN(ZA2.LOT_NO) AS LOT_NO                                                                                                   " & vbNewLine _
        & "        ,MIN(ZA2.CYLINDER_NO) AS CYLINDER_NO                                                                                         " & vbNewLine _
        & "        ,ZA2.PLT_NO                                                                                                                  " & vbNewLine _
        & "        ,MAX(ZA2.DEPLT_NO) AS DEPLT_NO                                                                                               " & vbNewLine _
        & "        ,MIN(ZA2.GRLVL1_PPNID) AS GRLVL1_PPNID                                                                                       " & vbNewLine _
        & "        ,ZA2.RETURN_FLAG                                                                                                             " & vbNewLine _
        & "        ,KB2.KBN_NM3 AS CUST_CD_S                                                                                                    " & vbNewLine _
        & "        ,KB2.KBN_NM4 AS CUST_CD_SS                                                                                                   " & vbNewLine _
        & "        ,ZA2.ASN_NO                                                                                                                  " & vbNewLine _
        & "      FROM                                                                                                                           " & vbNewLine _
        & "        $LM_TRN$..D_ZAI_TSMC AS ZA2                                                                                                  " & vbNewLine _
        & "        LEFT JOIN                                                                                                                    " & vbNewLine _
        & "          $LM_MST$..Z_KBN AS KB2                                                                                                     " & vbNewLine _
        & "          ON                                                                                                                         " & vbNewLine _
        & "                KB2.KBN_GROUP_CD = 'K038'                                                                                            " & vbNewLine _
        & "            AND KB2.KBN_NM1 = ZA2.SUPPLY_CD                                                                                          " & vbNewLine _
        & "            AND KB2.SYS_DEL_FLG = '0'                                                                                                " & vbNewLine _
        & "      WHERE                                                                                                                          " & vbNewLine _
        & "       (   (ZA2.STATUS  = '02'              AND ZA2.RETURN_FLAG = '00' AND ZA2.OUTKA_PLAN_DATE BETWEEN @INV_DATE_FROM AND @INV_DATE_TO) " & vbNewLine _
        & "        OR (ZA2.STATUS IN('02', '03', '04') AND ZA2.RETURN_FLAG = '01' AND ZA2.OUTKA_PLAN_DATE BETWEEN @INV_DATE_FROM AND @INV_DATE_TO) " & vbNewLine _
        & "        OR (ZA2.STATUS  = '05'                                         AND ZA2.OUTKA_PLAN_DATE BETWEEN @INV_DATE_FROM AND @INV_DATE_TO) " & vbNewLine _
        & "        )                                                                                                                            " & vbNewLine _
        & "        AND ZA2.PLT_NO <> ''                                                                                                         " & vbNewLine _
        & "        AND ZA2.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
        & "        AND NOT EXISTS (                                                                                                             " & vbNewLine _
        & "            -- 同じパレットに載った製品の進捗区分が全て「出荷」以降で、                                                              " & vbNewLine _
        & "            -- 同じパレットに載った製品で                                                                                            " & vbNewLine _
        & "            -- 　　最大の (進捗区分が「出荷」の場合の出荷日または進捗区分が「空在庫」以降の場合の再入荷日) が今回請求期間内であれば  " & vbNewLine _
        & "            -- 請求対象となるので、その条件に該当しない、同じパレットに載った製品が存在する場合は、抽出対象外とする                  " & vbNewLine _
        & "          SELECT                                                                                                                     " & vbNewLine _
        & "              'X'                                                                                                                    " & vbNewLine _
        & "          FROM                                                                                                                       " & vbNewLine _
        & "              $LM_TRN$..D_ZAI_TSMC AS ZA3                                                                                            " & vbNewLine _
        & "          WHERE                                                                                                                      " & vbNewLine _
        & "              ZA3.NRS_BR_CD = ZA2.NRS_BR_CD                                                                                          " & vbNewLine _
        & "          AND ZA3.SUPPLY_CD = ZA2.SUPPLY_CD                                                                                          " & vbNewLine _
        & "          AND ZA3.ASN_NO = ZA2.ASN_NO                                                                                                " & vbNewLine _
        & "          AND ZA3.PLT_NO = ZA2.PLT_NO                                                                                                " & vbNewLine _
        & "          AND(   (ZA3.STATUS  = '01')                                                                                                " & vbNewLine _
        & "              OR (ZA3.STATUS  = '02'              AND ZA3.RETURN_FLAG = '00' AND @INV_DATE_TO < ZA3.OUTKA_PLAN_DATE)                 " & vbNewLine _
        & "              OR (ZA3.STATUS IN('02', '03', '04') AND ZA3.RETURN_FLAG = '01' AND @INV_DATE_TO < ZA3.OUTKA_PLAN_DATE)                 " & vbNewLine _
        & "              OR (ZA3.STATUS  = '05'                                         AND @INV_DATE_TO < ZA3.OUTKA_PLAN_DATE)                 " & vbNewLine _
        & "              )                                                                                                                      " & vbNewLine _
        & "          AND ZA3.SYS_DEL_FLG = '0'                                                                                                  " & vbNewLine _
        & "        )                                                                                                                            " & vbNewLine _
        & "      GROUP BY                                                                                                                       " & vbNewLine _
        & "         ZA2.NRS_BR_CD                                                                                                               " & vbNewLine _
        & "        ,ZA2.CUST_CD_L                                                                                                               " & vbNewLine _
        & "        ,ZA2.CUST_CD_M                                                                                                               " & vbNewLine _
        & "        ,KB2.KBN_NM3                                                                                                                 " & vbNewLine _
        & "        ,KB2.KBN_NM4                                                                                                                 " & vbNewLine _
        & "        ,ZA2.ASN_NO                                                                                                                  " & vbNewLine _
        & "        ,ZA2.PLT_NO                                                                                                                  " & vbNewLine _
        & "        ,ZA2.RETURN_FLAG                                                                                                             " & vbNewLine _
        & "    ) AS ZAI                                                                                                                         " & vbNewLine _
        & "    ON                                                                                                                               " & vbNewLine _
        & "          ZAI.NRS_BR_CD = CST.NRS_BR_CD                                                                                              " & vbNewLine _
        & "      AND ZAI.CUST_CD_L = CST.CUST_CD_L                                                                                              " & vbNewLine _
        & "      AND ZAI.CUST_CD_M = CST.CUST_CD_M                                                                                              " & vbNewLine _
        & "      AND ZAI.CUST_CD_S = CST.CUST_CD_S                                                                                              " & vbNewLine _
        & "      AND ZAI.CUST_CD_SS = CST.CUST_CD_SS                                                                                            " & vbNewLine _
        & "  --商品マスタ                                                                                                                       " & vbNewLine _
        & "  LEFT JOIN                                                                                                                          " & vbNewLine _
        & "    $LM_MST$..M_GOODS AS GOD                                                                                                         " & vbNewLine _
        & "    ON                                                                                                                               " & vbNewLine _
        & "          GOD.NRS_BR_CD = ZAI.NRS_BR_CD                                                                                              " & vbNewLine _
        & "      AND GOD.CUST_CD_L = ZAI.CUST_CD_L                                                                                              " & vbNewLine _
        & "      AND GOD.CUST_CD_M = ZAI.CUST_CD_M                                                                                              " & vbNewLine _
        & "      AND GOD.CUST_CD_S = ZAI.CUST_CD_S                                                                                              " & vbNewLine _
        & "      AND GOD.CUST_CD_SS = ZAI.CUST_CD_SS                                                                                            " & vbNewLine _
        & "      AND GOD.GOODS_CD_CUST = ZAI.CUST_GOODS_CD                                                                                      " & vbNewLine _
        & "      AND GOD.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
        & "  --単価マスタ                                                                                                                       " & vbNewLine _
        & "  LEFT JOIN                                                                                                                          " & vbNewLine _
        & "    (                                                                                                                                " & vbNewLine _
        & "      SELECT                                                                                                                         " & vbNewLine _
        & "         TN1.NRS_BR_CD                                                                                                               " & vbNewLine _
        & "        ,TN1.CUST_CD_L                                                                                                               " & vbNewLine _
        & "        ,TN1.CUST_CD_M                                                                                                               " & vbNewLine _
        & "        ,TN1.UP_GP_CD_1                                                                                                              " & vbNewLine _
        & "        ,TN1.STR_DATE                                                                                                                " & vbNewLine _
        & "        ,(SELECT                                                                                                                     " & vbNewLine _
        & "            ISNULL(MIN(TN2.STR_DATE), '99999999')                                                                                    " & vbNewLine _
        & "          FROM                                                                                                                       " & vbNewLine _
        & "            $LM_MST$..M_TANKA AS TN2                                                                                                 " & vbNewLine _
        & "          WHERE                                                                                                                      " & vbNewLine _
        & "                TN2.NRS_BR_CD = TN1.NRS_BR_CD                                                                                        " & vbNewLine _
        & "            AND TN2.CUST_CD_L = TN1.CUST_CD_L                                                                                        " & vbNewLine _
        & "            AND TN2.CUST_CD_M = TN1.CUST_CD_M                                                                                        " & vbNewLine _
        & "            AND TN2.UP_GP_CD_1 = TN1.UP_GP_CD_1                                                                                      " & vbNewLine _
        & "            AND TN2.STR_DATE > TN1.STR_DATE                                                                                          " & vbNewLine _
        & "            AND TN2.SYS_DEL_FLG = '0'                                                                                                " & vbNewLine _
        & "         ) AS NEXT_STR_DATE                                                                                                          " & vbNewLine _
        & "        ,TN1.STORAGE_1                                                                                                               " & vbNewLine _
        & "        ,TN1.STORAGE_2                                                                                                               " & vbNewLine _
        & "        ,TN1.HANDLING_IN                                                                                                             " & vbNewLine _
        & "        ,TN1.MINI_TEKI_IN_AMO                                                                                                        " & vbNewLine _
        & "      FROM                                                                                                                           " & vbNewLine _
        & "        $LM_MST$..M_TANKA AS TN1                                                                                                     " & vbNewLine _
        & "      WHERE                                                                                                                          " & vbNewLine _
        & "        TN1.SYS_DEL_FLG = '0'                                                                                                        " & vbNewLine _
        & "    ) AS TNK                                                                                                                         " & vbNewLine _
        & "    ON                                                                                                                               " & vbNewLine _
        & "          TNK.NRS_BR_CD = GOD.NRS_BR_CD                                                                                              " & vbNewLine _
        & "      AND TNK.CUST_CD_L = GOD.CUST_CD_L                                                                                              " & vbNewLine _
        & "      AND TNK.CUST_CD_M = GOD.CUST_CD_M                                                                                              " & vbNewLine _
        & "      AND TNK.UP_GP_CD_1 = GOD.UP_GP_CD_1                                                                                            " & vbNewLine _
        & "      AND TNK.STR_DATE <= @INV_DATE_TO                                                                                               " & vbNewLine _
        & "      AND TNK.NEXT_STR_DATE > @INV_DATE_TO                                                                                           " & vbNewLine _
        & "WHERE                                                                                                                                " & vbNewLine _
        & "      SEI.NRS_BR_CD = @NRS_BR_CD                                                                                                     " & vbNewLine _
        & "  AND SEI.SEIQTO_CD = @SEIQTO_CD                                                                                                     " & vbNewLine _
        & "  AND SEI.SYS_DEL_FLG = '0'                                                                                                          " & vbNewLine _
        & "  AND ZAI.NRS_BR_CD IS NOT NULL                                                                                                      " & vbNewLine _
        & "ORDER BY                                                                                                                             " & vbNewLine _
        & "   ZAI.OUTKA_NO_L                                                                                                                    " & vbNewLine _
        & "  ,GOD.GOODS_CD_NRS                                                                                                                  " & vbNewLine _
        & "  ,ZAI.LOT_NO                                                                                                                        " & vbNewLine _
        & "  ,ZAI.ASN_NO                                                                                                                        " & vbNewLine

    ''' <summary>
    ''' TSMC請求明細データテーブル登録
    ''' </summary>
    Private Const SQL_INSERT_SEKY_MEISAI_TSMC As String = "" _
        & "INSERT INTO $LM_TRN$..I_SEKY_MEISAI_TSMC " & vbNewLine _
        & "(                                        " & vbNewLine _
        & "   NRS_BR_CD                             " & vbNewLine _
        & "  ,JOB_NO                                " & vbNewLine _
        & "  ,REC_NO                                " & vbNewLine _
        & "  ,NRS_WH_CD                             " & vbNewLine _
        & "  ,SEIQTO_CD                             " & vbNewLine _
        & "  ,INV_DATE_FROM                         " & vbNewLine _
        & "  ,INV_DATE_TO                           " & vbNewLine _
        & "  ,OUTKA_NO_L                            " & vbNewLine _
        & "  ,GOODS_CD_NRS                          " & vbNewLine _
        & "  ,LOT_NO                                " & vbNewLine _
        & "  ,CYLINDER_NO                           " & vbNewLine _
        & "  ,PLT_NO                                " & vbNewLine _
        & "  ,DEPLT_NO                              " & vbNewLine _
        & "  ,IRIME                                 " & vbNewLine _
        & "  ,TAX_KB                                " & vbNewLine _
        & "  ,OVER_DATE                             " & vbNewLine _
        & "  ,SET_AMO                               " & vbNewLine _
        & "  ,SET_OVER_AMO                          " & vbNewLine _
        & "  ,SET_AMO_TTL                           " & vbNewLine _
        & "  ,SET_CLC_DATE                          " & vbNewLine _
        & "  ,UNIT_KB                               " & vbNewLine _
        & "  ,RETURN_FLAG                           " & vbNewLine _
        & "  ,SYS_ENT_DATE                          " & vbNewLine _
        & "  ,SYS_ENT_TIME                          " & vbNewLine _
        & "  ,SYS_ENT_PGID                          " & vbNewLine _
        & "  ,SYS_ENT_USER                          " & vbNewLine _
        & "  ,SYS_UPD_DATE                          " & vbNewLine _
        & "  ,SYS_UPD_TIME                          " & vbNewLine _
        & "  ,SYS_UPD_PGID                          " & vbNewLine _
        & "  ,SYS_UPD_USER                          " & vbNewLine _
        & "  ,SYS_DEL_FLG                           " & vbNewLine _
        & ") VALUES (                               " & vbNewLine _
        & "   @NRS_BR_CD                            " & vbNewLine _
        & "  ,@JOB_NO                               " & vbNewLine _
        & "  ,@REC_NO                               " & vbNewLine _
        & "  ,@NRS_WH_CD                            " & vbNewLine _
        & "  ,@SEIQTO_CD                            " & vbNewLine _
        & "  ,@INV_DATE_FROM                        " & vbNewLine _
        & "  ,@INV_DATE_TO                          " & vbNewLine _
        & "  ,@OUTKA_NO_L                           " & vbNewLine _
        & "  ,@GOODS_CD_NRS                         " & vbNewLine _
        & "  ,@LOT_NO                               " & vbNewLine _
        & "  ,@CYLINDER_NO                          " & vbNewLine _
        & "  ,@PLT_NO                               " & vbNewLine _
        & "  ,@DEPLT_NO                             " & vbNewLine _
        & "  ,@IRIME                                " & vbNewLine _
        & "  ,@TAX_KB                               " & vbNewLine _
        & "  ,@OVER_DATE                            " & vbNewLine _
        & "  ,@SET_AMO                              " & vbNewLine _
        & "  ,@SET_OVER_AMO                         " & vbNewLine _
        & "  ,@SET_AMO_TTL                          " & vbNewLine _
        & "  ,@SET_CLC_DATE                         " & vbNewLine _
        & "  ,@UNIT_KB                              " & vbNewLine _
        & "  ,@RETURN_FLAG                          " & vbNewLine _
        & "  ,@SYS_ENT_DATE                         " & vbNewLine _
        & "  ,@SYS_ENT_TIME                         " & vbNewLine _
        & "  ,@SYS_ENT_PGID                         " & vbNewLine _
        & "  ,@SYS_ENT_USER                         " & vbNewLine _
        & "  ,@SYS_UPD_DATE                         " & vbNewLine _
        & "  ,@SYS_UPD_TIME                         " & vbNewLine _
        & "  ,@SYS_UPD_PGID                         " & vbNewLine _
        & "  ,@SYS_UPD_USER                         " & vbNewLine _
        & "  ,@SYS_DEL_FLG                          " & vbNewLine _
        & ")                                        " & vbNewLine

    ''' <summary>
    ''' TSMC在庫データ更新
    ''' </summary>
    Private Const SQL_UPDATE_ZAI_TSMC As String = "" _
        & "UPDATE $LM_TRN$..D_ZAI_TSMC                                                                                                  " & vbNewLine _
        & "SET                                                                                                                          " & vbNewLine _
        & "   LAST_INV_DATE = @LAST_INV_DATE                                                                                            " & vbNewLine _
        & "  ,LAST_CLC_DATE = @LAST_CLC_DATE                                                                                            " & vbNewLine _
        & "  ,SYS_UPD_DATE = @SYS_UPD_DATE                                                                                              " & vbNewLine _
        & "  ,SYS_UPD_TIME = @SYS_UPD_TIME                                                                                              " & vbNewLine _
        & "  ,SYS_UPD_PGID = @SYS_UPD_PGID                                                                                              " & vbNewLine _
        & "  ,SYS_UPD_USER = @SYS_UPD_USER                                                                                              " & vbNewLine _
        & "WHERE                                                                                                                        " & vbNewLine _
        & "  EXISTS (                                                                                                                   " & vbNewLine _
        & "    SELECT                                                                                                                   " & vbNewLine _
        & "      *                                                                                                                      " & vbNewLine _
        & "    FROM                                                                                                                     " & vbNewLine _
        & "        $LM_MST$..Z_KBN AS KBN                                                                                               " & vbNewLine _
        & "      INNER JOIN                                                                                                             " & vbNewLine _
        & "        $LM_MST$..M_CUST AS CST                                                                                              " & vbNewLine _
        & "        ON                                                                                                                   " & vbNewLine _
        & "              CST.NRS_BR_CD = @NRS_BR_CD                                                                                     " & vbNewLine _
        & "          AND CST.OYA_SEIQTO_CD = @SEIQTO_CD                                                                                 " & vbNewLine _
        & "          AND CST.CUST_CD_L = D_ZAI_TSMC.CUST_CD_L                                                                           " & vbNewLine _
        & "          AND CST.CUST_CD_M = D_ZAI_TSMC.CUST_CD_M                                                                           " & vbNewLine _
        & "          AND CST.CUST_CD_S = KBN.KBN_NM3                                                                                    " & vbNewLine _
        & "          AND CST.CUST_CD_SS = KBN.KBN_NM4                                                                                   " & vbNewLine _
        & "          AND CST.SYS_DEL_FLG = '0'                                                                                          " & vbNewLine _
        & "    WHERE                                                                                                                    " & vbNewLine _
        & "          KBN.KBN_GROUP_CD = 'K038'                                                                                          " & vbNewLine _
        & "      AND KBN.KBN_NM1 = D_ZAI_TSMC.SUPPLY_CD                                                                                 " & vbNewLine _
        & "      AND KBN.SYS_DEL_FLG = '0'                                                                                              " & vbNewLine _
        & "  )                                                                                                                          " & vbNewLine _
        & "  AND (                                                                                                                      " & vbNewLine _
        & "   (   (D_ZAI_TSMC.STATUS  = '02'              AND D_ZAI_TSMC.RETURN_FLAG = '00' AND D_ZAI_TSMC.OUTKA_PLAN_DATE BETWEEN @INV_DATE_FROM AND @INV_DATE_TO) " & vbNewLine _
        & "    OR (D_ZAI_TSMC.STATUS IN('02', '03', '04') AND D_ZAI_TSMC.RETURN_FLAG = '01' AND D_ZAI_TSMC.OUTKA_PLAN_DATE BETWEEN @INV_DATE_FROM AND @INV_DATE_TO) " & vbNewLine _
        & "    OR (D_ZAI_TSMC.STATUS  = '05'                                                AND D_ZAI_TSMC.OUTKA_PLAN_DATE BETWEEN @INV_DATE_FROM AND @INV_DATE_TO) " & vbNewLine _
        & "    )                                                                                                                        " & vbNewLine _
        & "    AND(    D_ZAI_TSMC.PLT_NO = ''                                                                                           " & vbNewLine _
        & "        OR (D_ZAI_TSMC.PLT_NO <> ''                                                                                          " & vbNewLine _
        & "            AND NOT EXISTS (                                                                                                 " & vbNewLine _
        & "              SELECT                                                                                                         " & vbNewLine _
        & "                  'X'                                                                                                        " & vbNewLine _
        & "              FROM                                                                                                           " & vbNewLine _
        & "                  $LM_TRN$..D_ZAI_TSMC AS ZAI_SUBQ                                                                           " & vbNewLine _
        & "              WHERE                                                                                                          " & vbNewLine _
        & "                  ZAI_SUBQ.NRS_BR_CD = D_ZAI_TSMC.NRS_BR_CD                                                                  " & vbNewLine _
        & "              AND ZAI_SUBQ.SUPPLY_CD = D_ZAI_TSMC.SUPPLY_CD                                                                  " & vbNewLine _
        & "              AND ZAI_SUBQ.ASN_NO = D_ZAI_TSMC.ASN_NO                                                                        " & vbNewLine _
        & "              AND ZAI_SUBQ.PLT_NO = D_ZAI_TSMC.PLT_NO                                                                        " & vbNewLine _
        & "              AND(   (ZAI_SUBQ.STATUS  = '01')                                                                               " & vbNewLine _
        & "                  OR (ZAI_SUBQ.STATUS  = '02'              AND ZAI_SUBQ.RETURN_FLAG = '00' AND @INV_DATE_TO < ZAI_SUBQ.OUTKA_PLAN_DATE) " & vbNewLine _
        & "                  OR (ZAI_SUBQ.STATUS IN('02', '03', '04') AND ZAI_SUBQ.RETURN_FLAG = '01' AND @INV_DATE_TO < ZAI_SUBQ.OUTKA_PLAN_DATE) " & vbNewLine _
        & "                  OR (ZAI_SUBQ.STATUS  = '05'                                              AND @INV_DATE_TO < ZAI_SUBQ.OUTKA_PLAN_DATE) " & vbNewLine _
        & "                  )                                                                                                          " & vbNewLine _
        & "              AND ZAI_SUBQ.SYS_DEL_FLG = '0'                                                                                 " & vbNewLine _
        & "              )                                                                                                              " & vbNewLine _
        & "            )                                                                                                                " & vbNewLine _
        & "    )                                                                                                                        " & vbNewLine _
        & "  )                                                                                                                          " & vbNewLine _
        & "  AND D_ZAI_TSMC.SYS_DEL_FLG = '0'                                                                                           " & vbNewLine

    ''' <summary>
    ''' TSMC在庫データ更新2
    ''' 「パレットのもので今回請求対象と同一パレットかつ出荷日または再入荷日が今回請求範囲外」分
    ''' </summary>
    Private Const SQL_UPDATE_ZAI_TSMC_2 As String = "" _
        & "UPDATE $LM_TRN$..D_ZAI_TSMC                           " & vbNewLine _
        & "SET                                                   " & vbNewLine _
        & "      LAST_INV_DATE = @LAST_INV_DATE                  " & vbNewLine _
        & "    , LAST_CLC_DATE = @LAST_CLC_DATE                  " & vbNewLine _
        & "    , SYS_UPD_DATE = @SYS_UPD_DATE                    " & vbNewLine _
        & "    , SYS_UPD_TIME = @SYS_UPD_TIME                    " & vbNewLine _
        & "    , SYS_UPD_PGID = @SYS_UPD_PGID                    " & vbNewLine _
        & "    , SYS_UPD_USER = @SYS_UPD_USER                    " & vbNewLine _
        & "WHERE                                                 " & vbNewLine _
        & "    D_ZAI_TSMC.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
        & "AND D_ZAI_TSMC.PLT_NO <> ''                           " & vbNewLine _
        & "AND D_ZAI_TSMC.LAST_INV_DATE <> @INV_DATE_TO          " & vbNewLine _
        & "AND D_ZAI_TSMC.STATUS IN('02', '03', '04')            " & vbNewLine _
        & "AND EXISTS (                                          " & vbNewLine _
        & "        SELECT                                        " & vbNewLine _
        & "            'X'                                       " & vbNewLine _
        & "        FROM                                          " & vbNewLine _
        & "            $LM_TRN$..D_ZAI_TSMC AS ZAI_SUBQ          " & vbNewLine _
        & "        WHERE                                         " & vbNewLine _
        & "            ZAI_SUBQ.NRS_BR_CD = D_ZAI_TSMC.NRS_BR_CD " & vbNewLine _
        & "        AND ZAI_SUBQ.SUPPLY_CD = D_ZAI_TSMC.SUPPLY_CD " & vbNewLine _
        & "        AND ZAI_SUBQ.ASN_NO = D_ZAI_TSMC.ASN_NO       " & vbNewLine _
        & "        AND ZAI_SUBQ.PLT_NO = D_ZAI_TSMC.PLT_NO       " & vbNewLine _
        & "        AND ZAI_SUBQ.LAST_INV_DATE = @INV_DATE_TO     " & vbNewLine _
        & "        AND ZAI_SUBQ.SYS_DEL_FLG = '0'                " & vbNewLine _
        & "    )                                                 " & vbNewLine _
        & "AND D_ZAI_TSMC.SYS_DEL_FLG = '0'                      " & vbNewLine

    ''' <summary>
    ''' 荷主マスタ更新
    ''' </summary>
    Private Const SQL_UPDATE_CUST As String = "" _
        & "UPDATE $LM_MST$..M_CUST                                          " & vbNewLine _
        & "SET                                                              " & vbNewLine _
        & "   HOKAN_NIYAKU_CALCULATION = @HOKAN_NIYAKU_CALCULATION          " & vbNewLine _
        & "  ,HOKAN_NIYAKU_CALCULATION_OLD = @HOKAN_NIYAKU_CALCULATION_OLD  " & vbNewLine _
        & "  ,NEW_JOB_NO = @NEW_JOB_NO                                      " & vbNewLine _
        & "  ,OLD_JOB_NO = @OLD_JOB_NO                                      " & vbNewLine _
        & "  ,SYS_UPD_DATE = @SYS_UPD_DATE                                  " & vbNewLine _
        & "  ,SYS_UPD_TIME = @SYS_UPD_TIME                                  " & vbNewLine _
        & "  ,SYS_UPD_PGID = @SYS_UPD_PGID                                  " & vbNewLine _
        & "  ,SYS_UPD_USER = @SYS_UPD_USER                                  " & vbNewLine _
        & "WHERE                                                            " & vbNewLine _
        & "      NRS_BR_CD = @NRS_BR_CD                                     " & vbNewLine _
        & "  AND OYA_SEIQTO_CD = @SEIQTO_CD                                 " & vbNewLine _
        & "  AND SYS_DEL_FLG = '0'                                          " & vbNewLine

    ''' <summary>
    ''' ナンバーマスタ更新
    ''' </summary>
    Private Const SQL_UPDATE_NUMBER As String = "" _
        & "UPDATE $LM_MST$..S_NUMBER        " & vbNewLine _
        & "SET                              " & vbNewLine _
        & "   NOW_VALUE = @NOW_VALUE        " & vbNewLine _
        & "  ,SYS_UPD_DATE = @SYS_UPD_DATE  " & vbNewLine _
        & "  ,SYS_UPD_TIME = @SYS_UPD_TIME  " & vbNewLine _
        & "  ,SYS_UPD_PGID = @SYS_UPD_PGID  " & vbNewLine _
        & "  ,SYS_UPD_USER = @SYS_UPD_USER  " & vbNewLine _
        & "WHERE                            " & vbNewLine _
        & "  NUMBER_KBN = '47'              " & vbNewLine

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
    ''' 退避用データセット
    ''' </summary>
    ''' <remarks></remarks>
    Private DsBk As DataSet = New DataSet

    ''' <summary>
    ''' バッチ番号
    ''' </summary>
    ''' <remarks></remarks>
    Private BatchNo As String

    ''' <summary>
    ''' デリゲート（非同期実行用）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Delegate Function WriteStringAsyncDelegate( _
         ByVal cmd As SqlCommand) As Integer

    ''' <summary>
    ''' 排他エラー件数
    ''' </summary>
    ''' <remarks></remarks>
    Private _ErrCnt As Integer

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI560IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI560DAC.SQL_SELECT_SEARCH_CNT)
        Me._StrSql.Append(LMI560DAC.SQL_SELECT_SEARCH_2)

        Call Me.SetConditionMasterSQL()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI560DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI560IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI560DAC.SQL_SELECT_SEARCH_1)
        Me._StrSql.Append(LMI560DAC.SQL_SELECT_SEARCH_2)

        Call Me.SetConditionMasterSQL()

        Me._StrSql.Append(LMI560DAC.SQL_SELECT_SEARCH_3)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI560DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("CLOSE_KB", "CLOSE_KB")
        map.Add("LAST_DATE", "LAST_DATE")
        map.Add("LAST_JOB_NO", "LAST_JOB_NO")
        map.Add("BEFORE_DATE", "BEFORE_DATE")
        map.Add("BEFORE_JOB_NO", "BEFORE_JOB_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI560OUT")

        Return ds

    End Function

#End Region

#Region "前回計算取消処理"

    ''' <summary>
    ''' 前々回情報の取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function SelectOldInfo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI560IN_DEL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI560DAC.SQL_SELECT_OLD_INFO)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_DATE", Me._Row.Item("LAST_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_JOB_NO", Me._Row.Item("LAST_JOB_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BEFORE_DATE", Me._Row.Item("BEFORE_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BEFORE_JOB_NO", Me._Row.Item("BEFORE_JOB_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BEFORE_DATE_YOBI", Me._Row.Item("BEFORE_DATE_YOBI").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LINE_NO", Me._Row.Item("LINE_NO").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI560DAC", "SelectOldInfo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'データ入れ替えのためクリア
        ds.Tables("LMI560IN_DEL").Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("LAST_DATE", "LAST_DATE")
        map.Add("LAST_JOB_NO", "LAST_JOB_NO")
        map.Add("BEFORE_DATE", "BEFORE_DATE")
        map.Add("BEFORE_JOB_NO", "BEFORE_JOB_NO")
        map.Add("OLD_DATE", "OLD_DATE")
        map.Add("OLD_JOB_NO", "OLD_JOB_NO")
        map.Add("BEFORE_DATE_YOBI", "BEFORE_DATE_YOBI")
        map.Add("LINE_NO", "LINE_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI560IN_DEL")

        Return ds

    End Function

    ''' <summary>
    ''' 荷主マスタ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateCustCancel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI560IN_DEL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI560DAC.SQL_UPDATE_CUST_CANCEL)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))

        Dim value As String = Me._Row.Item("BEFORE_DATE").ToString()
        If String.IsNullOrEmpty(value) Then
            value = Me._Row.Item("BEFORE_DATE_YOBI").ToString()
        End If
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_NIYAKU_CALCULATION", value, DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_NIYAKU_CALCULATION_OLD", Me._Row.Item("OLD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_JOB_NO", Me._Row.Item("BEFORE_JOB_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OLD_JOB_NO", Me._Row.Item("OLD_JOB_NO").ToString(), DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMI560DAC", "UpdateCustCancel", cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' TSMC在庫データ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateZaiTsmcCancel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI560IN_DEL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI560DAC.SQL_UPDATE_ZAI_TSMC_CANCEL)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_INV_DATE", String.Empty, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_CLC_DATE", String.Empty, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_INV_DATE_WHERE", Me._Row.Item("LAST_DATE").ToString(), DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMI560DAC", "UpdateZaiTsmcCancel", cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' TSMC請求明細データテーブル削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteSekyMeisaiTsmc(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI560IN_DEL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI560DAC.SQL_DELETE_SEKY_MEISAI_TSMC)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", Me._Row.Item("LAST_JOB_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMI560DAC", "DeleteSekyMeisaiTsmc", cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

#End Region

#Region "実行処理"

    ''' <summary>
    ''' JOB番号の取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function SelectJobNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI560IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI560DAC.SQL_SELECT_JOB_NO)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI560DAC", "SelectJobNo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("JOB_NO", "JOB_NO")
        map.Add("JOB_NO_ORG", "JOB_NO_ORG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI560OUT_JOBNO")

        Return ds

    End Function

    ''' <summary>
    ''' 請求対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function SelectCalcData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI560IN_CALC")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI560DAC.SQL_SELECT_CALC_DATA)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_FROM", Me._Row.Item("INV_DATE_FROM").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_TO", Me._Row.Item("INV_DATE_TO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", Me._Row.Item("JOB_NO").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI560DAC", "SelectCalcData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("JOB_NO", "JOB_NO")
        map.Add("NRS_WH_CD", "NRS_WH_CD")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("INV_DATE_FROM", "INV_DATE_FROM")
        map.Add("INV_DATE_TO", "INV_DATE_TO")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("CYLINDER_NO", "CYLINDER_NO")
        map.Add("PLT_NO", "PLT_NO")
        map.Add("DEPLT_NO", "DEPLT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("OVER_DATE", "OVER_DATE")
        map.Add("SET_AMO", "SET_AMO")
        map.Add("SET_OVER_AMO", "SET_OVER_AMO")
        map.Add("SET_AMO_TTL", "SET_AMO_TTL")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("GRLVL1_PPNID", "GRLVL1_PPNID")
        map.Add("RETURN_FLAG", "RETURN_FLAG")
        map.Add("SET_TANKA", "SET_TANKA")
        map.Add("SET_TANKA_DEPLT", "SET_TANKA_DEPLT")
        map.Add("SET_OVER_TANKA", "SET_OVER_TANKA")
        map.Add("SET_AMO_DAYS", "SET_AMO_DAYS")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("UP_GP_CD_1", "UP_GP_CD_1")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI560OUT_CALC")

        Return ds

    End Function

    ''' <summary>
    ''' TSMC請求明細データテーブル登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSekyMeisaiTsmc(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI560OUT_CALC")

        For Each Me._Row In inTbl.Rows

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQL作成
            Me._StrSql.Append(LMI560DAC.SQL_INSERT_SEKY_MEISAI_TSMC)

            'SQLパラメータ設定
            Me._SqlPrmList = New ArrayList()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", Me._Row.Item("JOB_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", Me._Row.Item("REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me._Row.Item("NRS_WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_FROM", Me._Row.Item("INV_DATE_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_TO", Me._Row.Item("INV_DATE_TO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me._Row.Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me._Row.Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CYLINDER_NO", Me._Row.Item("CYLINDER_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PLT_NO", Me._Row.Item("PLT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPLT_NO", Me._Row.Item("DEPLT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me._Row.Item("IRIME").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", Me._Row.Item("TAX_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OVER_DATE", Me._Row.Item("OVER_DATE").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_AMO", Me._Row.Item("SET_AMO").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_OVER_AMO", Me._Row.Item("SET_OVER_AMO").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_AMO_TTL", Me._Row.Item("SET_AMO_TTL").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_CLC_DATE", Me._Row.Item("SET_CLC_DATE").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNIT_KB", Me._Row.Item("UNIT_KB").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RETURN_FLAG", Me._Row.Item("RETURN_FLAG").ToString(), DBDataType.VARCHAR))
            Call Me.SetParamCommonSystemIns()

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            'ログメッセージの設定
            MyBase.Logger.WriteSQLLog("LMI560DAC", "InsertSekyMeisaiTsmc", cmd)

            'SQLの発行
            Dim reader As Integer = MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' TSMC在庫データ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateZaiTsmc(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI560IN_CALC")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI560DAC.SQL_UPDATE_ZAI_TSMC)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_FROM", Me._Row.Item("INV_DATE_FROM").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_TO", Me._Row.Item("INV_DATE_TO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_INV_DATE", Me._Row.Item("INV_DATE_TO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_CLC_DATE", Me._Row.Item("LAST_DATE").ToString(), DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMI560DAC", "UpdateZaiTsmc", cmd)

        cmd.CommandTimeout = 200

        'SQLの発行
        Dim reader As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' TSMC在庫データ更新2
    ''' 「パレットのもので今回請求対象と同一パレットかつ出荷日または再入荷日が今回請求範囲外」分
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateZaiTsmc2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI560IN_CALC")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI560DAC.SQL_UPDATE_ZAI_TSMC_2)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_TO", Me._Row.Item("INV_DATE_TO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_INV_DATE", Me._Row.Item("INV_DATE_TO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_CLC_DATE", Me._Row.Item("LAST_DATE").ToString(), DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMI560DAC", "UpdateZaiTsmc2", cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 荷主マスタ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateCust(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI560IN_CALC")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI560DAC.SQL_UPDATE_CUST)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_NIYAKU_CALCULATION", Me._Row.Item("INV_DATE_TO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_NIYAKU_CALCULATION_OLD", Me._Row.Item("LAST_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_JOB_NO", Me._Row.Item("JOB_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OLD_JOB_NO", Me._Row.Item("LAST_JOB_NO").ToString(), DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMI560DAC", "UpdateCust", cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' ナンバーマスタ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateNumber(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI560IN_CALC")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI560DAC.SQL_UPDATE_NUMBER)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList()

        Dim jobNo As Double = Convert.ToDouble(Me._Row.Item("JOB_NO_ORG").ToString) + 1
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NOW_VALUE", jobNo.ToString(), DBDataType.NUMERIC))
        Call Me.SetParamCommonSystemUpd()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMI560DAC", "UpdateNumber", cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "パラメータ設定"

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
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' 対象荷主と営業所コード・荷主コード(大)・(中) で紐付き、
        ' 期割り区分が “セット料金” の単価マスタ非削除レコードが存在する場合のみ抽出対象とする。
        ' (TSMC 以外の荷主(サプライヤー) を抽出対象としない)
        Me._StrSql.Append(String.Concat("  AND EXISTS (                                ", vbNewLine))
        Me._StrSql.Append(String.Concat("        SELECT                                ", vbNewLine))
        Me._StrSql.Append(String.Concat("            'X'                               ", vbNewLine))
        Me._StrSql.Append(String.Concat("        FROM                                  ", vbNewLine))
        Me._StrSql.Append(String.Concat("            $LM_MST$..M_TANKA                 ", vbNewLine))
        Me._StrSql.Append(String.Concat("        WHERE                                 ", vbNewLine))
        Me._StrSql.Append(String.Concat("            M_TANKA.NRS_BR_CD = CST.NRS_BR_CD ", vbNewLine))
        Me._StrSql.Append(String.Concat("        AND M_TANKA.CUST_CD_L = CST.CUST_CD_L ", vbNewLine))
        Me._StrSql.Append(String.Concat("        AND M_TANKA.CUST_CD_M = CST.CUST_CD_M ", vbNewLine))
        Me._StrSql.Append(String.Concat("        AND M_TANKA.KIWARI_KB = '05'          ", vbNewLine))
        Me._StrSql.Append(String.Concat("        AND M_TANKA.SYS_DEL_FLG = '0')        ", vbNewLine))

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所コード
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '請求月
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE", .Item("INV_DATE").ToString(), DBDataType.CHAR))

            '請求先コード
            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("  AND SEI.SEIQTO_CD = @SEIQTO_CD ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", whereStr, DBDataType.CHAR))
            End If

            '請求先名
            whereStr = .Item("SEIQTO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("  AND SEI.SEIQTO_NM LIKE @SEIQTO_NM  ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End Region

#Region "共通"

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

End Class
