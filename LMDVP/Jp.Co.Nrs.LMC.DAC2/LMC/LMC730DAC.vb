' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷編集
'  プログラムID     :  LMC730DAC : 船積確認書
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC730DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC730DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "印刷種別"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	 MAIN.NRS_BR_CD                                         AS NRS_BR_CD  " & vbNewLine _
                                            & ", 'B0'                                                   AS PTN_ID     " & vbNewLine _
                                            & ", MAIN.PTN_CD                                            AS PTN_CD     " & vbNewLine _
                                            & ", MAIN.RPT_ID                                            AS RPT_ID     " & vbNewLine


#End Region

#Region "SELECT句"

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                     " & vbNewLine _
                                            & "  MAIN.RPT_ID            AS RPT_ID         " & vbNewLine _
                                            & ", MAIN.NRS_BR_CD         AS NRS_BR_CD      " & vbNewLine _
                                            & ", MAIN.OUTKA_NO_L        AS OUTKA_NO_L     " & vbNewLine _
                                            & ", MAIN.INV_NO            AS INV_NO         " & vbNewLine _
                                            & ", MAIN.CNEE_NM           AS CNEE_NM        " & vbNewLine _
                                            & ", MAIN.CNEE_CD           AS CNEE_CD        " & vbNewLine _
                                            & ", MAIN.DEST_NM           AS DEST_NM        " & vbNewLine _
                                            & ", MAIN.DEST_AD_1         AS DEST_AD_1      " & vbNewLine _
                                            & ", MAIN.GOODS_CD_CUST     AS GOODS_CD_CUST  " & vbNewLine _
                                            & ", MAIN.OUTKA_NO_M        AS OUTKA_NO_M     " & vbNewLine _
                                            & ", MAIN.SERIAL_NO         AS SERIAL_NO      " & vbNewLine _
                                            & ", MAIN.GOODS_NM_1        AS GOODS_NM_1     " & vbNewLine _
                                            & ", SUM(MAIN.ALCTD_QT)     AS ALCTD_QT       " & vbNewLine _
                                            & ", SUM(MAIN.ALCTD_NB)     AS ALCTD_NB       " & vbNewLine _
                                            & ", MAIN.QT_UT             AS QT_UT          " & vbNewLine _
                                            & ", MAIN.INKO_DATE         AS INKO_DATE      " & vbNewLine _
                                            & ", MAIN.TANI              AS TANI           " & vbNewLine _
                                            & ", MAIN.HASU              AS HASU           " & vbNewLine _
                                            & ", MAIN.PKG_UT            AS PKG_UT         " & vbNewLine _
                                            & ", MAIN.REMARK_OUT        AS REMARK_OUT     " & vbNewLine _
                                            & ", MAIN.UN                AS UN             " & vbNewLine _
                                            & ", MAIN.CHEMICAL_NM       AS CHEMICAL_NM    " & vbNewLine _
                                            & ", MAIN.IMCO              AS IMCO           " & vbNewLine _
                                            & ", MAIN.PG                AS PG             " & vbNewLine _
                                            & ", MAIN.FP                AS FP             " & vbNewLine _
                                            & ", MAIN.EL該当品          AS EL該当品       " & vbNewLine _
                                            & ", MAIN.TTL_QT            AS TTL_QT         " & vbNewLine


#End Region

#Region "FROM句"

    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "FROM (                                                                                                        " & vbNewLine _
                                            & "    SELECT                                                                                                    " & vbNewLine _
                                            & "      CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                                                        " & vbNewLine _
                                            & "	 	 WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                                                             " & vbNewLine _
                                            & "	   	 ELSE MR3.PTN_CD END                                            AS PTN_CD                                " & vbNewLine _
                                            & "    , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                        " & vbNewLine _
                                            & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                             " & vbNewLine _
                                            & "		 ELSE MR3.RPT_ID END                                            AS RPT_ID                                " & vbNewLine _
                                            & "    , OUTKA_L.NRS_BR_CD                                              AS NRS_BR_CD                             " & vbNewLine _
                                            & "    , OUTKA_L.OUTKA_NO_L                                             AS OUTKA_NO_L                            " & vbNewLine _
                                            & "    , OUTKA_L.CUST_ORD_NO                                            AS INV_NO                                " & vbNewLine _
                                            & "    , DEST.DEST_NM                                                   AS CNEE_NM                               " & vbNewLine _
                                            & "    , OUTKA_L.SHIP_CD_L                                              AS CNEE_CD                               " & vbNewLine _
                                            & "    , OUTKA_L.DEST_NM                                                AS DEST_NM                               " & vbNewLine _
                                            & "    , OUTKA_L.DEST_AD_1                                              AS DEST_AD_1                             " & vbNewLine _
                                            & "    , GOODS.GOODS_CD_CUST                                            AS GOODS_CD_CUST                         " & vbNewLine _
                                            & "    , OUTKA_M.OUTKA_NO_M                                             AS OUTKA_NO_M                            " & vbNewLine _
                                            & "    , OUTKA_S.OUTKA_NO_S                                             AS OUTKA_NO_S                            " & vbNewLine _
                                            & "    , OUTKA_M.SERIAL_NO                                              AS SERIAL_NO                             " & vbNewLine _
                                            & "    , GOODS.GOODS_NM_1                                               AS GOODS_NM_1                            " & vbNewLine _
                                            & "    , OUTKA_S.ALCTD_QT                                               AS ALCTD_QT                              " & vbNewLine _
                                            & "    , OUTKA_S.ALCTD_NB                                               AS ALCTD_NB                              " & vbNewLine _
                                            & "    , ISNULL(GOODS.STD_IRIME_UT,'')                                  AS QT_UT                                 " & vbNewLine _
                                            & "    , CASE WHEN '' + ZAI_TRS.INKO_DATE = '' THEN                                                              " & vbNewLine _
                                            & "                ZAI_TRS.INKO_PLAN_DATE                                                                        " & vbNewLine _
                                            & "           ELSE ZAI_TRS.INKO_DATE                                                                             " & vbNewLine _
                                            & "      END                                                            AS INKO_DATE                             " & vbNewLine _
                                            & "    , CASE WHEN (OUTKA_S.OUTKA_TTL_NB % GOODS.PKG_NB) = 0 THEN                                                " & vbNewLine _
                                            & "                 OUTKA_S.OUTKA_TTL_NB / GOODS.PKG_NB                                                          " & vbNewLine _
                                            & "           ELSE (OUTKA_S.OUTKA_TTL_NB - OUTKA_S.OUTKA_TTL_NB % GOODS.PKG_NB) / GOODS.PKG_NB                   " & vbNewLine _
                                            & "      END                                                            AS TANI                                  " & vbNewLine _
                                            & "    , CASE WHEN (OUTKA_S.OUTKA_TTL_NB % GOODS.PKG_NB) = 0 THEN  ''                                            " & vbNewLine _
                                            & "           ELSE CONVERT(VARCHAR,(OUTKA_S.OUTKA_TTL_NB % GOODS.PKG_NB)) + '/' + CONVERT(VARCHAR,GOODS.PKG_NB)  " & vbNewLine _
                                            & "      END                                                            AS HASU                                  " & vbNewLine _
                                            & "    , ISNULL(GOODS.PKG_UT,'')                                        AS PKG_UT                                " & vbNewLine _
                                            & "    , ISNULL(INKA_S.REMARK_OUT,'')                                   AS REMARK_OUT                            " & vbNewLine _
                                            & "    , MIN(DTLUTI.GYO)                                                AS GYO                                   " & vbNewLine _
                                            & "    , ISNULL(DTLUTI.L3_UN_NUMBER,'')                                 AS UN                                    " & vbNewLine _
                                            & "    , ISNULL(DTLUTI.L3_PROPER_SHIP_NAME,'')                          AS CHEMICAL_NM                           " & vbNewLine _
                                            & "    , ISNULL(DTLUTI.L3_CLASS_SUBRISK,'')                             AS IMCO                                  " & vbNewLine _
                                            & "    , ISNULL(DTLUTI.L3_PACKING_GROUP,'')                             AS PG                                    " & vbNewLine _
                                            & "    , REPLACE(ISNULL(DTLUTI.L3_FLASH_POINT_FOR_IMO,''),'C','')       AS FP                                    " & vbNewLine _
                                            & "    , ISNULL(DTLUTI.L2_EXPORT_RESTRICT_NEW,'')                       AS EL該当品                              " & vbNewLine _
                                            & "    , OUTKA_QT.TTL_QT                                                AS TTL_QT                                " & vbNewLine _
                                            & "    FROM                                                                                                      " & vbNewLine _
                                            & "        $LM_TRN$..C_OUTKA_L OUTKA_L                                                                           " & vbNewLine _
                                            & "    LEFT JOIN                                                                                                 " & vbNewLine _
                                            & "        $LM_TRN$..C_OUTKA_M OUTKA_M                                                                           " & vbNewLine _
                                            & "     ON OUTKA_L.NRS_BR_CD = OUTKA_M.NRS_BR_CD                                                                 " & vbNewLine _
                                            & "    AND OUTKA_L.OUTKA_NO_L = OUTKA_M.OUTKA_NO_L                                                               " & vbNewLine _
                                            & "    AND OUTKA_M.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                            & "    LEFT JOIN                                                                                                 " & vbNewLine _
                                            & "        $LM_TRN$..C_OUTKA_S OUTKA_S                                                                           " & vbNewLine _
                                            & "     ON OUTKA_M.NRS_BR_CD = OUTKA_S.NRS_BR_CD                                                                 " & vbNewLine _
                                            & "    AND OUTKA_M.OUTKA_NO_L = OUTKA_S.OUTKA_NO_L                                                               " & vbNewLine _
                                            & "    AND OUTKA_M.OUTKA_NO_M = OUTKA_S.OUTKA_NO_M                                                               " & vbNewLine _
                                            & "    AND OUTKA_S.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                            & "    LEFT JOIN ( --総数量を求める                                                                              " & vbNewLine _
                                            & "        SELECT                                                                                                " & vbNewLine _
                                            & "          OUTKA_L.NRS_BR_CD      AS NRS_BR_CD                                                                 " & vbNewLine _
                                            & "        , OUTKA_L.OUTKA_NO_L     AS OUTKA_NO_L                                                                " & vbNewLine _
                                            & "        , SUM(OUTKA_S.ALCTD_QT)  AS TTL_QT                                                                    " & vbNewLine _
                                            & "        FROM                                                                                                  " & vbNewLine _
                                            & "            $LM_TRN$..C_OUTKA_L OUTKA_L                                                                       " & vbNewLine _
                                            & "        LEFT JOIN                                                                                             " & vbNewLine _
                                            & "             $LM_TRN$..C_OUTKA_M OUTKA_M                                                                      " & vbNewLine _
                                            & "         ON OUTKA_L.NRS_BR_CD = OUTKA_M.NRS_BR_CD                                                             " & vbNewLine _
                                            & "        AND OUTKA_L.OUTKA_NO_L = OUTKA_M.OUTKA_NO_L                                                           " & vbNewLine _
                                            & "        AND OUTKA_M.SYS_DEL_FLG = '0'                                                                         " & vbNewLine _
                                            & "        LEFT JOIN                                                                                             " & vbNewLine _
                                            & "             $LM_TRN$..C_OUTKA_S OUTKA_S                                                                      " & vbNewLine _
                                            & "         ON OUTKA_M.NRS_BR_CD = OUTKA_S.NRS_BR_CD                                                             " & vbNewLine _
                                            & "        AND OUTKA_M.OUTKA_NO_L = OUTKA_S.OUTKA_NO_L                                                           " & vbNewLine _
                                            & "        AND OUTKA_M.OUTKA_NO_M = OUTKA_S.OUTKA_NO_M                                                           " & vbNewLine _
                                            & "        AND OUTKA_S.SYS_DEL_FLG = '0'                                                                         " & vbNewLine _
                                            & "        WHERE                                                                                                 " & vbNewLine _
                                            & "            OUTKA_L.NRS_BR_CD = @NRS_BR_CD                                                                    " & vbNewLine _
                                            & "        AND OUTKA_L.CUST_CD_L = @CUST_CD_L                                                                    " & vbNewLine _
                                            & "        AND OUTKA_L.OUTKA_NO_L = @OUTKA_NO_L                                                                  " & vbNewLine _
                                            & "        AND OUTKA_L.SYS_DEL_FLG = '0'                                                                         " & vbNewLine _
                                            & "        GROUP BY                                                                                              " & vbNewLine _
                                            & "          OUTKA_L.NRS_BR_CD                                                                                   " & vbNewLine _
                                            & "        , OUTKA_L.OUTKA_NO_L) OUTKA_QT                                                                        " & vbNewLine _
                                            & "     ON OUTKA_L.NRS_BR_CD = OUTKA_QT.NRS_BR_CD                                                                " & vbNewLine _
                                            & "    AND OUTKA_L.OUTKA_NO_L = OUTKA_QT.OUTKA_NO_L                                                              " & vbNewLine _
                                            & "    LEFT JOIN                                                                                                 " & vbNewLine _
                                            & "        $LM_TRN$..D_ZAI_TRS ZAI_TRS                                                                           " & vbNewLine _
                                            & "     ON OUTKA_S.NRS_BR_CD = ZAI_TRS.NRS_BR_CD                                                                 " & vbNewLine _
                                            & "    AND OUTKA_S.ZAI_REC_NO = ZAI_TRS.ZAI_REC_NO                                                               " & vbNewLine _
                                            & "    AND ZAI_TRS.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                            & "    LEFT JOIN                                                                                                 " & vbNewLine _
                                            & "        $LM_TRN$..B_INKA_S INKA_S                                                                             " & vbNewLine _
                                            & "     ON ZAI_TRS.NRS_BR_CD = INKA_S.NRS_BR_CD                                                                  " & vbNewLine _
                                            & "    AND ZAI_TRS.INKA_NO_L = INKA_S.INKA_NO_L                                                                  " & vbNewLine _
                                            & "    AND ZAI_TRS.INKA_NO_M = INKA_S.INKA_NO_M                                                                  " & vbNewLine _
                                            & "    AND ZAI_TRS.INKA_NO_S = INKA_S.INKA_NO_S                                                                  " & vbNewLine _
                                            & "    AND INKA_S.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                            & "    LEFT JOIN                                                                                                 " & vbNewLine _
                                            & "        $LM_TRN$..H_INKAEDI_HED_UTI HEDUTI                                                                    " & vbNewLine _
                                            & "     ON OUTKA_M.NRS_BR_CD = HEDUTI.NRS_BR_CD                                                                  " & vbNewLine _
                                            & "    AND OUTKA_M.SERIAL_NO = HEDUTI.H4_DELIVERY_NO                                                             " & vbNewLine _
                                            & "    AND HEDUTI.DEL_KB = '0'                                                                                   " & vbNewLine _
                                            & "    LEFT JOIN                                                                                                 " & vbNewLine _
                                            & "        $LM_TRN$..H_INKAEDI_DTL_UTI DTLUTI                                                                    " & vbNewLine _
                                            & "     ON HEDUTI.CRT_DATE = DTLUTI.CRT_DATE                                                                     " & vbNewLine _
                                            & "    AND HEDUTI.FILE_NAME = DTLUTI.FILE_NAME                                                                   " & vbNewLine _
                                            & "    AND HEDUTI.REC_NO = DTLUTI.REC_NO                                                                         " & vbNewLine _
                                            & "    AND DTLUTI.DEL_KB = '0'                                                                                   " & vbNewLine _
                                            & "    LEFT JOIN                                                                                                 " & vbNewLine _
                                            & "        $LM_MST$..M_GOODS GOODS                                                                               " & vbNewLine _
                                            & "     ON OUTKA_L.NRS_BR_CD = GOODS.NRS_BR_CD                                                                   " & vbNewLine _
                                            & "    AND OUTKA_L.CUST_CD_L = GOODS.CUST_CD_L                                                                   " & vbNewLine _
                                            & "    AND OUTKA_L.CUST_CD_M = GOODS.CUST_CD_M                                                                   " & vbNewLine _
                                            & "    AND OUTKA_M.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                                             " & vbNewLine _
                                            & "    AND GOODS.SYS_DEL_FLG = '0'                                                                               " & vbNewLine _
                                            & "    LEFT JOIN                                                                                                 " & vbNewLine _
                                            & "        $LM_MST$..M_DEST DEST                                                                                 " & vbNewLine _
                                            & "     ON OUTKA_L.NRS_BR_CD = DEST.NRS_BR_CD                                                                    " & vbNewLine _
                                            & "    AND OUTKA_L.CUST_CD_L = DEST.CUST_CD_L                                                                    " & vbNewLine _
                                            & "    AND OUTKA_L.SHIP_CD_L = DEST.DEST_CD                                                                      " & vbNewLine _
                                            & "--出荷Lでの荷主帳票パターン取得                                                                               " & vbNewLine _
                                            & "    LEFT JOIN                                                                                                 " & vbNewLine _
                                            & "        $LM_MST$..M_CUST_RPT MCR1                                                                             " & vbNewLine _
                                            & "     ON OUTKA_L.NRS_BR_CD = MCR1.NRS_BR_CD                                                                    " & vbNewLine _
                                            & "    AND OUTKA_L.CUST_CD_L = MCR1.CUST_CD_L                                                                    " & vbNewLine _
                                            & "    AND OUTKA_L.CUST_CD_M = MCR1.CUST_CD_M                                                                    " & vbNewLine _
                                            & "    AND MCR1.PTN_ID = '00'                                                                                    " & vbNewLine _
                                            & "--帳票パターン取得                                                                                            " & vbNewLine _
                                            & "    LEFT JOIN                                                                                                 " & vbNewLine _
                                            & "        $LM_MST$..M_RPT MR1                                                                                   " & vbNewLine _
                                            & "     ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                                        " & vbNewLine _
                                            & "    AND MR1.PTN_ID = MCR1.PTN_ID                                                                              " & vbNewLine _
                                            & "    AND MR1.PTN_CD = MCR1.PTN_CD                                                                              " & vbNewLine _
                                            & "    AND MR1.SYS_DEL_FLG = '0'                                                                                 " & vbNewLine _
                                            & "--商品Mの荷主での荷主帳票パターン取得                                                                         " & vbNewLine _
                                            & "    LEFT JOIN                                                                                                 " & vbNewLine _
                                            & "        $LM_MST$..M_CUST_RPT MCR2                                                                             " & vbNewLine _
                                            & "     ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                                                                      " & vbNewLine _
                                            & "    AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                                                                      " & vbNewLine _
                                            & "    AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                                                                      " & vbNewLine _
                                            & "    AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                                                                      " & vbNewLine _
                                            & "    AND MCR2.PTN_ID = 'B0'                                                                                    " & vbNewLine _
                                            & "--帳票パターン取得                                                                                            " & vbNewLine _
                                            & "    LEFT JOIN                                                                                                 " & vbNewLine _
                                            & "        $LM_MST$..M_RPT MR2                                                                                   " & vbNewLine _
                                            & "     ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                                        " & vbNewLine _
                                            & "    AND MR2.PTN_ID = MCR2.PTN_ID                                                                              " & vbNewLine _
                                            & "    AND MR2.PTN_CD = MCR2.PTN_CD                                                                              " & vbNewLine _
                                            & "    AND MR2.SYS_DEL_FLG = '0'                                                                                 " & vbNewLine _
                                            & "--存在しない場合の帳票パターン取得                                                                            " & vbNewLine _
                                            & "    LEFT LOOP JOIN                                                                                            " & vbNewLine _
                                            & "        $LM_MST$..M_RPT MR3                                                                                   " & vbNewLine _
                                            & "     ON MR3.NRS_BR_CD = OUTKA_L.NRS_BR_CD                                                                     " & vbNewLine _
                                            & "    AND MR3.PTN_ID = 'B0'                                                                                     " & vbNewLine _
                                            & "    AND MR3.STANDARD_FLAG = '01'                                                                              " & vbNewLine _
                                            & "    AND MR3.SYS_DEL_FLG = '0'                                                                                 " & vbNewLine _
                                            & "    WHERE                                                                                                     " & vbNewLine _
                                            & "        OUTKA_L.NRS_BR_CD = @NRS_BR_CD                                                                        " & vbNewLine _
                                            & "    AND OUTKA_L.CUST_CD_L = @CUST_CD_L                                                                        " & vbNewLine _
                                            & "    AND OUTKA_L.OUTKA_NO_L = @OUTKA_NO_L                                                                      " & vbNewLine _
                                            & "    AND OUTKA_L.SYS_DEL_FLG = '0'                                                                             " & vbNewLine _
                                            & "GROUP BY                                                                                                      " & vbNewLine _
                                            & "      CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                                                        " & vbNewLine _
                                            & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                                                             " & vbNewLine _
                                            & "      ELSE MR3.PTN_CD END                                                                                     " & vbNewLine _
                                            & "    , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                        " & vbNewLine _
                                            & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                             " & vbNewLine _
                                            & "      ELSE MR3.RPT_ID END                                                                                     " & vbNewLine _
                                            & "    , OUTKA_L.NRS_BR_CD                                                                                       " & vbNewLine _
                                            & "    , OUTKA_L.OUTKA_NO_L                                                                                      " & vbNewLine _
                                            & "    , OUTKA_L.CUST_ORD_NO                                                                                     " & vbNewLine _
                                            & "    , DEST.DEST_NM                                                                                            " & vbNewLine _
                                            & "    , OUTKA_L.SHIP_CD_L                                                                                       " & vbNewLine _
                                            & "    , OUTKA_L.DEST_NM                                                                                         " & vbNewLine _
                                            & "    , OUTKA_L.DEST_AD_1                                                                                       " & vbNewLine _
                                            & "    , GOODS.GOODS_CD_CUST                                                                                     " & vbNewLine _
                                            & "    , OUTKA_M.OUTKA_NO_M                                                                                      " & vbNewLine _
                                            & "    , OUTKA_S.OUTKA_NO_S                                                                                      " & vbNewLine _
                                            & "    , OUTKA_M.SERIAL_NO                                                                                       " & vbNewLine _
                                            & "    , GOODS.GOODS_NM_1                                                                                        " & vbNewLine _
                                            & "    , GOODS.STD_IRIME_UT                                                                                      " & vbNewLine _
                                            & "    , ZAI_TRS.INKO_DATE                                                                                       " & vbNewLine _
                                            & "    , ZAI_TRS.INKO_PLAN_DATE                                                                                  " & vbNewLine _
                                            & "    , OUTKA_S.ALCTD_QT                                                                                        " & vbNewLine _
                                            & "    , OUTKA_S.ALCTD_NB                                                                                        " & vbNewLine _
                                            & "    , OUTKA_S.OUTKA_TTL_NB                                                                                    " & vbNewLine _
                                            & "    , GOODS.PKG_NB                                                                                            " & vbNewLine _
                                            & "    , GOODS.PKG_UT                                                                                            " & vbNewLine _
                                            & "    , INKA_S.REMARK_OUT                                                                                       " & vbNewLine _
                                            & "    , DTLUTI.L3_UN_NUMBER                                                                                     " & vbNewLine _
                                            & "    , DTLUTI.L3_PROPER_SHIP_NAME                                                                              " & vbNewLine _
                                            & "    , DTLUTI.L3_CLASS_SUBRISK                                                                                 " & vbNewLine _
                                            & "    , DTLUTI.L3_PACKING_GROUP                                                                                 " & vbNewLine _
                                            & "    , DTLUTI.L3_FLASH_POINT_FOR_IMO                                                                           " & vbNewLine _
                                            & "    , DTLUTI.L2_EXPORT_RESTRICT_NEW                                                                           " & vbNewLine _
                                            & "    , OUTKA_QT.TTL_QT                                                                                         " & vbNewLine _
                                            & "       ) MAIN                                                                                                 " & vbNewLine

#End Region

#Region "GROUP BY句"

    ''' <summary>
    ''' 印刷データ抽出用 GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = "GROUP BY              " & vbNewLine _
                                         & "  MAIN.RPT_ID         " & vbNewLine _
                                         & ", MAIN.NRS_BR_CD      " & vbNewLine _
                                         & ", MAIN.OUTKA_NO_L     " & vbNewLine _
                                         & ", MAIN.INV_NO         " & vbNewLine _
                                         & ", MAIN.CNEE_NM        " & vbNewLine _
                                         & ", MAIN.CNEE_CD        " & vbNewLine _
                                         & ", MAIN.DEST_NM        " & vbNewLine _
                                         & ", MAIN.DEST_AD_1      " & vbNewLine _
                                         & ", MAIN.GOODS_CD_CUST  " & vbNewLine _
                                         & ", MAIN.OUTKA_NO_M     " & vbNewLine _
                                         & ", MAIN.SERIAL_NO      " & vbNewLine _
                                         & ", MAIN.GOODS_NM_1     " & vbNewLine _
                                         & ", MAIN.QT_UT          " & vbNewLine _
                                         & ", MAIN.INKO_DATE      " & vbNewLine _
                                         & ", MAIN.TANI           " & vbNewLine _
                                         & ", MAIN.HASU           " & vbNewLine _
                                         & ", MAIN.PKG_UT         " & vbNewLine _
                                         & ", MAIN.REMARK_OUT     " & vbNewLine _
                                         & ", MAIN.UN             " & vbNewLine _
                                         & ", MAIN.CHEMICAL_NM    " & vbNewLine _
                                         & ", MAIN.IMCO           " & vbNewLine _
                                         & ", MAIN.PG             " & vbNewLine _
                                         & ", MAIN.FP             " & vbNewLine _
                                         & ", MAIN.EL該当品       " & vbNewLine _
                                         & ", MAIN.TTL_QT         " & vbNewLine
#End Region

#Region "ORDER BY句"

    ''' <summary>
    ''' 印刷データ抽出用 ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY          " & vbNewLine _
                                         & "  MAIN.PKG_UT     " & vbNewLine _
                                         & ", MAIN.SERIAL_NO  " & vbNewLine

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

#Region "印刷処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC730IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC730DAC.SQL_SELECT_MPRT)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMC730DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Call Me.setIndataParameter(Me._Row)               '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC730DAC", "SelectMPRT", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        '処理件数の設定
        If ds.Tables("M_RPT").Rows.Count < 1 Then
            MyBase.SetMessage("G021")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 印刷対象データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC730IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'RPT_IDのチェック用
        Dim rptId As String = rptTbl.Rows(0).Item("RPT_ID").ToString()

        'SQL作成
        Me._StrSql.Append(LMC730DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMC730DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMC730DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GroupBy句)
        Me._StrSql.Append(LMC730DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)
        Call Me.setIndataParameter(Me._Row)               '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC730DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("INV_NO", "INV_NO")
        map.Add("CNEE_NM", "CNEE_NM")
        map.Add("CNEE_CD", "CNEE_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("QT_UT", "QT_UT")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("TANI", "TANI")
        map.Add("HASU", "HASU")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("UN", "UN")
        map.Add("CHEMICAL_NM", "CHEMICAL_NM")
        map.Add("IMCO", "IMCO")
        map.Add("PG", "PG")
        map.Add("FP", "FP")
        map.Add("EL該当品", "EL該当品")
        map.Add("TTL_QT", "TTL_QT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC730OUT")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 条件文設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setIndataParameter(ByVal dr As DataRow)

        With dr

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

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
