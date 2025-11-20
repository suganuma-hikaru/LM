' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD620    : 消防分類別在庫重量表
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD620DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD620DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "  SELECT DISTINCT                                                     " & vbNewLine _
                                            & "	   ZAIKO.NRS_BR_CD                       AS NRS_BR_CD                " & vbNewLine _
                                            & "	 , 'BF'                                  AS PTN_ID                   " & vbNewLine _
                                            & "	 , CASE WHEN MR2.PTN_CD IS NOT NULL THEN                             " & vbNewLine _
                                            & "	             MR2.PTN_CD                                              " & vbNewLine _
                                            & "	        WHEN MR1.PTN_CD IS NOT NULL THEN                             " & vbNewLine _
                                            & "	             MR1.PTN_CD                                              " & vbNewLine _
                                            & "	   ELSE                                                              " & vbNewLine _
                                            & "	             MR3.PTN_CD                                              " & vbNewLine _
                                            & "	   END                                    AS PTN_CD                  " & vbNewLine _
                                            & "	 , CASE WHEN MR2.PTN_CD IS NOT NULL THEN                             " & vbNewLine _
                                            & "	             MR2.RPT_ID                                              " & vbNewLine _
                                            & "		    WHEN MR1.PTN_CD IS NOT NULL THEN                             " & vbNewLine _
                                            & "	             MR1.RPT_ID                                              " & vbNewLine _
                                            & "	   ELSE                                                              " & vbNewLine _
                                            & "	             MR3.RPT_ID                                              " & vbNewLine _
                                            & "	   END                                    AS RPT_ID                  " & vbNewLine
    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                               " & vbNewLine _
                                            & "   RPT_ID        AS RPT_ID       --帳票ID                             " & vbNewLine _
                                            & " , NRS_BR_CD     AS NRS_BR_CD    --営業所コード                       " & vbNewLine _
                                            & " , NRS_BR_NM     AS NRS_BR_NM    --営業所名                           " & vbNewLine _
                                            & " , WH_NM         AS WH_NM        --倉庫名                             " & vbNewLine _
                                            & " , CUST_CD_L     AS CUST_CD_L    --荷主コード(大)                     " & vbNewLine _
                                            & " , CUST_CD_M     AS CUST_CD_M    --荷主コード(中)                     " & vbNewLine _
                                            & " , TOU_NO        AS TOU_NO       --棟                                 " & vbNewLine _
                                            & " , SITU_NO       AS SITU_NO      --室                                 " & vbNewLine _
                                            & " , SHOBO_CD      AS SHOBO_CD     --消防コード                         " & vbNewLine _
                                            & " , SHOBO_NM      AS SHOBO_NM     --消防品目名                         " & vbNewLine _
                                            & " , SHOBO_RUI_CD  AS SHOBO_RUI_CD --消防類別コード                     " & vbNewLine _
                                            & " , SHOBO_RUI_NM  AS SHOBO_RUI_NM --消防類別品目名                     " & vbNewLine _
                                            & " , ZEN_NB        AS ZEN_NB       --前月在庫分                         " & vbNewLine _
                                            & " , TOU_WT        AS TOU_WT       --重量                               " & vbNewLine _
                                            & " , NB            AS NB           --個数                               " & vbNewLine _
                                            & " , QT            AS QT           --数量                               " & vbNewLine _
                                            & " , @PRINT_FROM   AS PRINT_FROM   --出力日(画面項目)                   " & vbNewLine _
                                            & " FROM(                                                                " & vbNewLine _
                                            & "      SELECT                                                          " & vbNewLine _
                                            & "              CASE WHEN MR2.PTN_CD IS NOT NULL THEN                   " & vbNewLine _
                                            & "                        MR2.RPT_ID                                    " & vbNewLine _
                                            & "  		          WHEN MR1.PTN_CD IS NOT NULL THEN                   " & vbNewLine _
                                            & "                        MR1.RPT_ID                                    " & vbNewLine _
                                            & " 	         ELSE                                                    " & vbNewLine _
                                            & "                        MR3.RPT_ID                                    " & vbNewLine _
                                            & "              END                         AS RPT_ID                   " & vbNewLine _
                                            & "            , ZAIKO.NRS_BR_CD             AS NRS_BR_CD                " & vbNewLine _
                                            & "            , NRS_BR.NRS_BR_NM            AS NRS_BR_NM                " & vbNewLine _
                                            & "            , ZAIKO.CUST_CD_L             AS CUST_CD_L                " & vbNewLine _
                                            & "            , ZAIKO.CUST_CD_M             AS CUST_CD_M                " & vbNewLine _
                                            & "            , SOKO.WH_NM                  AS WH_NM                    " & vbNewLine _
                                            & "            , ZAIKO.TOU_NO                AS TOU_NO                   " & vbNewLine _
                                            & "            , ZAIKO.SITU_NO               AS SITU_NO                  " & vbNewLine _
                                            & "            , GOODS.SHOBO_CD              AS SHOBO_CD                 " & vbNewLine _
                                            & "            , CASE WHEN ISNULL(SHOBO.HINMEI,'') = '' THEN             " & vbNewLine _
                                            & "                   '一般品'                                           " & vbNewLine _
                                            & "              ELSE                                                    " & vbNewLine _
                                            & "                   SHOBO.HINMEI                                       " & vbNewLine _
                                            & "              END                         AS SHOBO_NM                 " & vbNewLine _
                                            & "            , ISNULL(SHOBO.RUI,'')        AS SHOBO_RUI_CD             " & vbNewLine _
                                            & "            , CASE WHEN ISNULL(KBN.KBN_NM1,'') = '' THEN              " & vbNewLine _
                                            & "                   '一般品'                                           " & vbNewLine _
                                            & "              ELSE                                                    " & vbNewLine _
                                            & "                   KBN.KBN_NM1                                        " & vbNewLine _
                                            & "              END                         AS SHOBO_RUI_NM             " & vbNewLine _
                                            & "            , CASE WHEN ISNULL(GOODS.SHOBO_CD,'') = '' THEN           " & vbNewLine _
                                            & "                   0                                                  " & vbNewLine _
                                            & "                   WHEN ISNULL(TOU_SITU_SHOBO.SHOBO_CD,'') = '' THEN  " & vbNewLine _
                                            & "                   1                                                  " & vbNewLine _
                                            & "              ELSE                                                    " & vbNewLine _
                                            & "                   0                                                  " & vbNewLine _
                                            & "              END                         AS ZEN_NB                   " & vbNewLine _
                                            & "            , SUM(ZAIKO.PORA_ZAI_QT) * GOODS.STD_WT_KGS               " & vbNewLine _
                                            & "              / GOODS.STD_IRIME_NB        AS TOU_WT                   " & vbNewLine _
                                            & "            , SUM(ZAIKO.PORA_ZAI_NB)      AS NB                       " & vbNewLine _
                                            & "            , SUM(ZAIKO.PORA_ZAI_QT)      AS QT                       " & vbNewLine

    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "      FROM(                                                                  " & vbNewLine _
                                     & "           SELECT                       --入荷データ                         " & vbNewLine _
                                     & "             INL.NRS_BR_CD             AS NRS_BR_CD                          " & vbNewLine _
                                     & "           , INL.WH_CD                 AS WH_CD                              " & vbNewLine _
                                     & "           , INL.CUST_CD_L             AS CUST_CD_L                          " & vbNewLine _
                                     & "           , INL.CUST_CD_M             AS CUST_CD_M                          " & vbNewLine _
                                     & "           , INS.ZAI_REC_NO            AS ZAI_REC_NO                         " & vbNewLine _
                                     & "           , INM.GOODS_CD_NRS          AS GOODS_CD_NRS                       " & vbNewLine _
                                     & "           , INS.TOU_NO                AS TOU_NO                             " & vbNewLine _
                                     & "           , INS.SITU_NO               AS SITU_NO                            " & vbNewLine _
                                     & "           , (INS.KONSU * MG.PKG_NB) + INS.HASU                              " & vbNewLine _
                                     & "                                       AS PORA_ZAI_NB                        " & vbNewLine _
                                     & "           , ((INS.KONSU * MG.PKG_NB) + INS.HASU) * INS.IRIME                " & vbNewLine _
                                     & "                                       AS PORA_ZAI_QT                        " & vbNewLine _
                                     & "           FROM                                                              " & vbNewLine _
                                     & "               $LM_TRN$..B_INKA_L INL                                        " & vbNewLine _
                                     & "           LEFT JOIN                                                         " & vbNewLine _
                                     & "               $LM_TRN$..B_INKA_M INM                                        " & vbNewLine _
                                     & "            ON INL.NRS_BR_CD = INM.NRS_BR_CD                                 " & vbNewLine _
                                     & "           AND INL.INKA_NO_L = INM.INKA_NO_L                                 " & vbNewLine _
                                     & "           AND INM.SYS_DEL_FLG = '0'	                                     " & vbNewLine _
                                     & "           LEFT JOIN                                                         " & vbNewLine _
                                     & "               $LM_TRN$..B_INKA_S INS                                        " & vbNewLine _
                                     & "            ON INM.NRS_BR_CD = INS.NRS_BR_CD                                 " & vbNewLine _
                                     & "           AND INM.INKA_NO_L = INS.INKA_NO_L                                 " & vbNewLine _
                                     & "           AND INM.INKA_NO_M = INS.INKA_NO_M                                 " & vbNewLine _
                                     & "           AND INS.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                     & "           LEFT JOIN                                                         " & vbNewLine _
                                     & "               $LM_MST$..M_GOODS MG                                          " & vbNewLine _
                                     & "            ON INM.NRS_BR_CD = MG.NRS_BR_CD                                  " & vbNewLine _
                                     & "           AND INM.GOODS_CD_NRS = MG.GOODS_CD_NRS                            " & vbNewLine _
                                     & "           WHERE                                                             " & vbNewLine _
                                     & "               INL.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                     & "           AND INL.CUST_CD_L = @CUST_CD_L                                    " & vbNewLine _
                                     & "           AND INL.CUST_CD_M = @CUST_CD_M                                    " & vbNewLine _
                                     & "           AND INL.INKA_DATE <= @PRINT_FROM                                  " & vbNewLine _
                                     & "           AND(INL.INKA_DATE > '00000000'                                    " & vbNewLine _
                                     & "            OR INL.INKA_STATE_KB < '50')                                     " & vbNewLine _
                                     & "           AND(INL.INKA_STATE_KB > '10'                                      " & vbNewLine _
                                     & "            OR INS.ZAI_REC_NO <> '')                                         " & vbNewLine _
                                     & "           AND INL.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                     & "           --在庫移動分を加減算                                              " & vbNewLine _
                                     & "      UNION ALL                                                              " & vbNewLine _
                                     & "           SELECT                       --移動後                             " & vbNewLine _
                                     & "             IDO1.NRS_BR_CD            AS NRS_BR_CD                          " & vbNewLine _
                                     & "           , ZAI1.WH_CD                AS WH_CD                              " & vbNewLine _
                                     & "           , ZAI1.CUST_CD_L            AS CUST_CD_L                          " & vbNewLine _
                                     & "           , ZAI1.CUST_CD_M            AS CUST_CD_M                          " & vbNewLine _
                                     & "           , IDO1.N_ZAI_REC_NO         AS ZAI_REC_NO                         " & vbNewLine _
                                     & "           , ZAI1.GOODS_CD_NRS         AS GOODS_CD_NRS                       " & vbNewLine _
                                     & "           , ZAI1.TOU_NO               AS TOU_NO                             " & vbNewLine _
                                     & "           , ZAI1.SITU_NO              AS SITU_NO                            " & vbNewLine _
                                     & "           , IDO1.N_PORA_ZAI_NB        AS PORA_ZAI_NB                        " & vbNewLine _
                                     & "           , IDO1.N_PORA_ZAI_NB * IDO1.O_IRIME                               " & vbNewLine _
                                     & "                                       AS PORA_ZAI_QT                        " & vbNewLine _
                                     & "           FROM                                                              " & vbNewLine _
                                     & "               $LM_TRN$..D_IDO_TRS IDO1                                      " & vbNewLine _
                                     & "           LEFT JOIN                                                         " & vbNewLine _
                                     & "               $LM_TRN$..D_ZAI_TRS ZAI1                                      " & vbNewLine _
                                     & "            ON IDO1.NRS_BR_CD = ZAI1.NRS_BR_CD                               " & vbNewLine _
                                     & "           AND IDO1.N_ZAI_REC_NO = ZAI1.ZAI_REC_NO                           " & vbNewLine _
                                     & "           AND ZAI1.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                     & "           WHERE                                                             " & vbNewLine _
                                     & "               IDO1.NRS_BR_CD = @NRS_BR_CD                                   " & vbNewLine _
                                     & "           AND ZAI1.CUST_CD_L = @CUST_CD_L                                   " & vbNewLine _
                                     & "           AND ZAI1.CUST_CD_M = @CUST_CD_M                                   " & vbNewLine _
                                     & "           AND IDO1.IDO_DATE <= @PRINT_FROM                                  " & vbNewLine _
                                     & "           AND IDO1.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                     & "      UNION ALL                                                              " & vbNewLine _
                                     & "           SELECT                       --移動前                             " & vbNewLine _
                                     & "             IDO2.NRS_BR_CD            AS NRS_BR_CD                          " & vbNewLine _
                                     & "           , ZAI2.WH_CD                AS WH_CD                              " & vbNewLine _
                                     & "           , ZAI2.CUST_CD_L            AS CUST_CD_L                          " & vbNewLine _
                                     & "           , ZAI2.CUST_CD_M            AS CUST_CD_M                          " & vbNewLine _
                                     & "           , IDO2.O_ZAI_REC_NO         AS ZAI_REC_NO                         " & vbNewLine _
                                     & "           , ZAI2.GOODS_CD_NRS         AS GOODS_CD_NRS                       " & vbNewLine _
                                     & "           , ZAI2.TOU_NO               AS TOU_NO                             " & vbNewLine _
                                     & "           , ZAI2.SITU_NO              AS SITU_NO                            " & vbNewLine _
                                     & "           , IDO2.N_PORA_ZAI_NB * -1   AS PORA_ZAI_NB                        " & vbNewLine _
                                     & "           , IDO2.N_PORA_ZAI_NB * IDO2.O_IRIME * -1                          " & vbNewLine _
                                     & "                                       AS PORA_ZAI_QT                        " & vbNewLine _
                                     & "           FROM                                                              " & vbNewLine _
                                     & "               $LM_TRN$..D_IDO_TRS IDO2                                      " & vbNewLine _
                                     & "           LEFT JOIN                                                         " & vbNewLine _
                                     & "               $LM_TRN$..D_ZAI_TRS ZAI2                                      " & vbNewLine _
                                     & "            ON IDO2.NRS_BR_CD = ZAI2.NRS_BR_CD                               " & vbNewLine _
                                     & "           AND IDO2.O_ZAI_REC_NO = ZAI2.ZAI_REC_NO                           " & vbNewLine _
                                     & "           AND ZAI2.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                     & "           WHERE                                                             " & vbNewLine _
                                     & "               IDO2.NRS_BR_CD = @NRS_BR_CD                                   " & vbNewLine _
                                     & "           AND ZAI2.CUST_CD_L = @CUST_CD_L                                   " & vbNewLine _
                                     & "           AND ZAI2.CUST_CD_M = @CUST_CD_M                                   " & vbNewLine _
                                     & "           AND IDO2.IDO_DATE <= @PRINT_FROM                                  " & vbNewLine _
                                     & "           AND IDO2.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                     & "      UNION ALL                                                              " & vbNewLine _
                                     & "           SELECT                       --出荷データ                         " & vbNewLine _
                                     & "             OUTL.NRS_BR_CD            AS NRS_BR_CD                          " & vbNewLine _
                                     & "           , OUTL.WH_CD                AS WH_CD                              " & vbNewLine _
                                     & "           , OUTL.CUST_CD_L            AS CUST_CD_L                          " & vbNewLine _
                                     & "           , OUTL.CUST_CD_M            AS CUST_CD_M                          " & vbNewLine _
                                     & "           , OUTS.ZAI_REC_NO           AS ZAI_REC_NO                         " & vbNewLine _
                                     & "           , OUTM.GOODS_CD_NRS         AS GOODS_CD_NRS                       " & vbNewLine _
                                     & "           , OUTS.TOU_NO               AS TOU_NO                             " & vbNewLine _
                                     & "           , OUTS.SITU_NO              AS SITU_NO                            " & vbNewLine _
                                     & "           , OUTS.ALCTD_NB * -1        AS PORA_ZAI_NB                        " & vbNewLine _
                                     & "           , OUTS.ALCTD_QT * -1        AS PORA_ZAI_QT                        " & vbNewLine _
                                     & "           FROM                                                              " & vbNewLine _
                                     & "               $LM_TRN$..C_OUTKA_L OUTL                                      " & vbNewLine _
                                     & "           LEFT JOIN                                                         " & vbNewLine _
                                     & "               $LM_TRN$..C_OUTKA_M OUTM                                      " & vbNewLine _
                                     & "            ON OUTL.NRS_BR_CD = OUTM.NRS_BR_CD                               " & vbNewLine _
                                     & "           AND OUTL.OUTKA_NO_L = OUTM.OUTKA_NO_L                             " & vbNewLine _
                                     & "           AND OUTM.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                     & "           LEFT JOIN                                                         " & vbNewLine _
                                     & "               $LM_TRN$..C_OUTKA_S OUTS                                      " & vbNewLine _
                                     & "            ON OUTM.NRS_BR_CD = OUTS.NRS_BR_CD                               " & vbNewLine _
                                     & "           AND OUTM.OUTKA_NO_L = OUTS.OUTKA_NO_L                             " & vbNewLine _
                                     & "           AND OUTM.OUTKA_NO_M = OUTS.OUTKA_NO_M                             " & vbNewLine _
                                     & "           AND OUTS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                     & "           WHERE                                                             " & vbNewLine _
                                     & "               OUTL.NRS_BR_CD = @NRS_BR_CD                                   " & vbNewLine _
                                     & "           AND OUTL.CUST_CD_L = @CUST_CD_L                                   " & vbNewLine _
                                     & "           AND OUTL.CUST_CD_M = @CUST_CD_M                                   " & vbNewLine _
                                     & "           AND OUTL.OUTKA_PLAN_DATE <= @PRINT_FROM                           " & vbNewLine _
                                     & "           AND OUTM.ALCTD_KB <> '04'                                         " & vbNewLine _
                                     & "           AND OUTL.OUTKA_STATE_KB >= '60'                                   " & vbNewLine _
                                     & "           AND OUTL.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                     & "           )ZAIKO                                                            " & vbNewLine _
                                     & "      LEFT JOIN                                                              " & vbNewLine _
                                     & "          $LM_MST$..M_NRS_BR NRS_BR                                          " & vbNewLine _
                                     & "       ON ZAIKO.NRS_BR_CD = NRS_BR.NRS_BR_CD                                 " & vbNewLine _
                                     & "      LEFT JOIN                                                              " & vbNewLine _
                                     & "          $LM_MST$..M_SOKO SOKO                                              " & vbNewLine _
                                     & "       ON ZAIKO.NRS_BR_CD = SOKO.NRS_BR_CD                                   " & vbNewLine _
                                     & "      AND ZAIKO.WH_CD = SOKO.WH_CD                                           " & vbNewLine _
                                     & "      LEFT JOIN                                                              " & vbNewLine _
                                     & "          $LM_MST$..M_GOODS GOODS                                            " & vbNewLine _
                                     & "       ON ZAIKO.NRS_BR_CD = GOODS.NRS_BR_CD                                  " & vbNewLine _
                                     & "      AND ZAIKO.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                            " & vbNewLine _
                                     & "      LEFT JOIN                                                              " & vbNewLine _
                                     & "          $LM_MST$..M_SHOBO SHOBO                                            " & vbNewLine _
                                     & "       ON GOODS.SHOBO_CD = SHOBO.SHOBO_CD                                    " & vbNewLine _
                                     & "      LEFT JOIN                                                              " & vbNewLine _
                                     & "          $LM_MST$..M_TOU_SITU_SHOBO TOU_SITU_SHOBO                          " & vbNewLine _
                                     & "       ON ZAIKO.NRS_BR_CD = TOU_SITU_SHOBO.NRS_BR_CD                         " & vbNewLine _
                                     & "      AND ZAIKO.WH_CD = TOU_SITU_SHOBO.WH_CD                                 " & vbNewLine _
                                     & "      AND ZAIKO.TOU_NO = TOU_SITU_SHOBO.TOU_NO                               " & vbNewLine _
                                     & "      AND ZAIKO.SITU_NO = TOU_SITU_SHOBO.SITU_NO                             " & vbNewLine _
                                     & "      AND SHOBO.SHOBO_CD = TOU_SITU_SHOBO.SHOBO_CD                           " & vbNewLine _
                                     & "      LEFT JOIN                                                              " & vbNewLine _
                                     & "          $LM_MST$..Z_KBN KBN                                                " & vbNewLine _
                                     & "       ON KBN.KBN_GROUP_CD = 'S004'                                          " & vbNewLine _
                                     & "      AND SHOBO.RUI = KBN.KBN_CD                                             " & vbNewLine _
                                     & "      --在庫の荷主での荷主帳票パターン取得                                   " & vbNewLine _
                                     & "      LEFT JOIN                                                              " & vbNewLine _
                                     & "          $LM_MST$..M_CUST_RPT MCR1                                          " & vbNewLine _
                                     & "       ON ZAIKO.NRS_BR_CD = MCR1.NRS_BR_CD                                   " & vbNewLine _
                                     & "      AND ZAIKO.CUST_CD_L = MCR1.CUST_CD_L                                   " & vbNewLine _
                                     & "      AND ZAIKO.CUST_CD_M = MCR1.CUST_CD_M                                   " & vbNewLine _
                                     & "      AND MCR1.CUST_CD_S = '00'                                              " & vbNewLine _
                                     & "      AND MCR1.PTN_ID = 'BF'                                                 " & vbNewLine _
                                     & "      --帳票パターン取得                                                     " & vbNewLine _
                                     & "      LEFT JOIN                                                              " & vbNewLine _
                                     & "          $LM_MST$..M_RPT MR1                                                " & vbNewLine _
                                     & "       ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                     " & vbNewLine _
                                     & "      AND MR1.PTN_ID = MCR1.PTN_ID                                           " & vbNewLine _
                                     & "      AND MR1.PTN_CD = MCR1.PTN_CD                                           " & vbNewLine _
                                     & "      AND MR1.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                     & "      --商品Mの荷主での荷主帳票パターン取得                                  " & vbNewLine _
                                     & "      LEFT JOIN                                                              " & vbNewLine _
                                     & "          $LM_MST$..M_CUST_RPT MCR2                                          " & vbNewLine _
                                     & "       ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                                   " & vbNewLine _
                                     & "      AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                                   " & vbNewLine _
                                     & "      AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                                   " & vbNewLine _
                                     & "      AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                                   " & vbNewLine _
                                     & "      AND MCR2.PTN_ID = 'BF'                                                 " & vbNewLine _
                                     & "      --帳票パターン取得                                                     " & vbNewLine _
                                     & "      LEFT JOIN                                                              " & vbNewLine _
                                     & "          $LM_MST$..M_RPT MR2                                                " & vbNewLine _
                                     & "       ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                     " & vbNewLine _
                                     & "      AND MR2.PTN_ID = MCR2.PTN_ID                                           " & vbNewLine _
                                     & "      AND MR2.PTN_CD = MCR2.PTN_CD                                           " & vbNewLine _
                                     & "      AND MR2.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                     & "      --存在しない場合の帳票パターン取得                                     " & vbNewLine _
                                     & "      LEFT JOIN                                                              " & vbNewLine _
                                     & "          $LM_MST$..M_RPT MR3                                                " & vbNewLine _
                                     & "       ON MR3.NRS_BR_CD = ZAIKO.NRS_BR_CD                                    " & vbNewLine _
                                     & "      AND MR3.PTN_ID = 'BF'                                                  " & vbNewLine _
                                     & "      AND MR3.STANDARD_FLAG = '01'                                           " & vbNewLine _
                                     & "      AND MR3.SYS_DEL_FLG = '0'                                              " & vbNewLine

    ''' <summary>
    ''' データ抽出用GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = "      GROUP BY                                                           " & vbNewLine _
                                         & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN                            " & vbNewLine _
                                         & "                  MR2.RPT_ID                                             " & vbNewLine _
                                         & "             WHEN MR1.PTN_CD IS NOT NULL THEN                            " & vbNewLine _
                                         & "                  MR1.RPT_ID                                             " & vbNewLine _
                                         & "        ELSE                                                             " & vbNewLine _
                                         & "                  MR3.RPT_ID                                             " & vbNewLine _
                                         & "        END                                                              " & vbNewLine _
                                         & "      , ZAIKO.NRS_BR_CD                                                  " & vbNewLine _
                                         & "      , NRS_BR.NRS_BR_NM                                                 " & vbNewLine _
                                         & "      , ZAIKO.CUST_CD_L                                                  " & vbNewLine _
                                         & "      , ZAIKO.CUST_CD_M                                                  " & vbNewLine _
                                         & "      , SOKO.WH_NM                                                       " & vbNewLine _
                                         & "      , ZAIKO.TOU_NO                                                     " & vbNewLine _
                                         & "      , ZAIKO.SITU_NO                                                    " & vbNewLine _
                                         & "      , GOODS.SHOBO_CD                                                   " & vbNewLine _
                                         & "      , SHOBO.HINMEI                                                     " & vbNewLine _
                                         & "      , SHOBO.RUI                                                        " & vbNewLine _
                                         & "      , KBN.KBN_NM1                                                      " & vbNewLine _
                                         & "      , TOU_SITU_SHOBO.SHOBO_CD                                          " & vbNewLine _
                                         & "      , GOODS.STD_WT_KGS                                                 " & vbNewLine _
                                         & "      , GOODS.STD_IRIME_NB                                               " & vbNewLine _
                                         & "      ) MAIN                                                             " & vbNewLine

    ''' <summary>
    ''' データ抽出用ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                                                                " & vbNewLine _
                                         & "   TOU_NO                                                                " & vbNewLine _
                                         & " , SITU_NO                                                               " & vbNewLine

#End Region

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
    Private _rptRow As Data.DataRow

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
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD620IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD620DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMD620DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetSQLWhereData()                         '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD620DAC", "SelectMPrt", cmd)

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
    ''' 在庫テーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫テーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD620IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'M_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'RPTTableの条件rowの格納
        Me._rptRow = rptTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD620DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMD620DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMD620DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GROUP BY句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMD620DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD620DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_NM", "WH_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("SHOBO_NM", "SHOBO_NM")
        map.Add("SHOBO_RUI_CD", "SHOBO_RUI_CD")
        map.Add("SHOBO_RUI_NM", "SHOBO_RUI_NM")
        map.Add("ZEN_NB", "ZEN_NB")
        map.Add("TOU_WT", "TOU_WT")
        map.Add("NB", "NB")
        map.Add("QT", "QT")
        map.Add("PRINT_FROM", "PRINT_FROM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD620OUT")

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
        Dim strSqlAppend As String = String.Empty


        Me._StrSql.Append("WHERE ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("  NB <> 0")
        Me._StrSql.Append(vbNewLine)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row("CUST_CD_M"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_FROM", Me._Row("PRINT_FROM"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereData()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row("CUST_CD_M"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_FROM", Me._Row("PRINT_FROM"), DBDataType.CHAR))

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
