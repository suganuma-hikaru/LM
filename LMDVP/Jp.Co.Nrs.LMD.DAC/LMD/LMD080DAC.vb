' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD080  : 荷主システム在庫数と日陸在庫数との照合
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMD080DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD080DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "変数"

#End Region

#Region "荷主在庫数データの検索"

#Region "荷主在庫数データの検索 SQL SELECT句"

    ''' <summary>
    ''' 荷主在庫数データの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ZAISHOCUST As String = " SELECT                                                                     " & vbNewLine _
                                                  & " ZAISHOCUST.NRS_BR_CD                         AS NRS_BR_CD                  " & vbNewLine _
                                                  & ",ZAISHOCUST.CUST_CD_L                         AS CUST_CD_L                  " & vbNewLine _
                                                  & ",ZAISHOCUST.CUST_CD_M                         AS CUST_CD_M                  " & vbNewLine _
                                                  & ",ZAISHOCUST.CHECK_DATE                        AS CHECK_DATE                 " & vbNewLine _
                                                  & ",ZAISHOCUST.GYO_NO                            AS GYO_NO                     " & vbNewLine _
                                                  & ",ZAISHOCUST.EDA_NO                            AS EDA_NO                     " & vbNewLine _
                                                  & ",ZAISHOCUST.FILE_NAME                         AS FILE_NAME                  " & vbNewLine _
                                                  & ",ZAISHOCUST.FILE_FOLDER                       AS FILE_FOLDER                " & vbNewLine _
                                                  & ",ZAISHOCUST.WH_CD                             AS WH_CD                      " & vbNewLine _
                                                  & ",ZAISHOCUST.CUST_GOODS_CD                     AS GOODS_CD_CUST              " & vbNewLine _
                                                  & ",ZAISHOCUST.GOODS_NM                          AS GOODS_NM                   " & vbNewLine _
                                                  & ",ZAISHOCUST.LOT_NO                            AS LOT_NO                     " & vbNewLine _
                                                  & ",ZAISHOCUST.SERIAL_NO                         AS SERIAL_NO                  " & vbNewLine _
                                                  & ",ZAISHOCUST.IRIME                             AS IRIME                      " & vbNewLine _
                                                  & ",ZAISHOCUST.IRIME_UT                          AS IRIME_UT                   " & vbNewLine _
                                                  & ",ZAISHOCUST.CLASS_1                           AS CLASS_1                    " & vbNewLine _
                                                  & ",ZAISHOCUST.CLASS_2                           AS CLASS_2                    " & vbNewLine _
                                                  & ",ZAISHOCUST.CLASS_3                           AS CLASS_3                    " & vbNewLine _
                                                  & ",ZAISHOCUST.CLASS_4                           AS CLASS_4                    " & vbNewLine _
                                                  & ",ZAISHOCUST.CLASS_5                           AS CLASS_5                    " & vbNewLine _
                                                  & ",ZAISHOCUST.NB                                AS NB                         " & vbNewLine _
                                                  & ",ZAISHOCUST.QT                                AS QT                         " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_N01                          AS FREE_N01                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_N02                          AS FREE_N02                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_N03                          AS FREE_N03                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_N04                          AS FREE_N04                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_N05                          AS FREE_N05                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_N06                          AS FREE_N06                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_N07                          AS FREE_N07                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_N08                          AS FREE_N08                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_N09                          AS FREE_N09                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_N10                          AS FREE_N10                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_C01                          AS FREE_C01                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_C02                          AS FREE_C02                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_C03                          AS FREE_C03                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_C04                          AS FREE_C04                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_C05                          AS FREE_C05                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_C06                          AS FREE_C06                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_C07                          AS FREE_C07                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_C08                          AS FREE_C08                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_C09                          AS FREE_C09                   " & vbNewLine _
                                                  & ",ZAISHOCUST.FREE_C10                          AS FREE_C10                   " & vbNewLine

#End Region

#Region "荷主在庫数データの検索 SQL FROM句"

    ''' <summary>
    ''' 荷主在庫数データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_ZAISHOCUST As String = "FROM                                                                  " & vbNewLine _
                                                       & "$LM_TRN$..D_ZAI_SHOGOH_CUST ZAISHOCUST                                " & vbNewLine

#Region "荷主在庫数データの検索 SQL ORDER BY句"

    ''' <summary>
    ''' 荷主在庫数データの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_ZAISHOCUST As String = "ORDER BY                                                             " & vbNewLine _
                                                        & " ZAISHOCUST.CUST_GOODS_CD                                            " & vbNewLine _
                                                        & ",ZAISHOCUST.LOT_NO                                                   " & vbNewLine _
                                                        & ",ZAISHOCUST.SERIAL_NO                                                " & vbNewLine _
                                                        & ",ZAISHOCUST.IRIME                                                    " & vbNewLine

#End Region

#End Region

#End Region

#Region "月末在庫データの検索"

#Region "月末在庫データの検索 SQL"

#If False Then      'ADD 2018/08/31 依頼番号 : 001723   【LMS】在庫照合画面_タイムアウトエラーのため実用できず対応
    ''' <summary>
    ''' 月末在庫データの検索 SQL 長いSQLで分けると、意味がわからなくなりそうなので、あえて分けずに記載
    ''' </summary>
    ''' <remarks></remarks>
        Private Const SQL_SELECT_ZAIZAN As String = " SELECT                                                                      " & vbNewLine _
                                              & "         OUTPUT_DATE.NRS_BR_CD                AS NRS_BR_CD                   " & vbNewLine _
                                              & "       , OUTPUT_DATE.CUST_CD_L                AS CUST_CD_L                   " & vbNewLine _
                                              & "       , OUTPUT_DATE.CUST_CD_M                AS CUST_CD_M                   " & vbNewLine _
                                              & "       , @JISSHI_DATE                         AS CHECK_DATE                  " & vbNewLine _
                                              & "       , ''                                   AS GYO_NO                      " & vbNewLine _
                                              & "       , @EDA_NO                              AS EDA_NO                      " & vbNewLine _
                                              & "       , OUTPUT_DATE.WH_CD                    AS WH_CD                       " & vbNewLine _
                                              & "       , ISNULL(OUTPUT_DATE.GOODS_CD_NRS,'')  AS GOODS_CD_NRS                " & vbNewLine _
                                              & "       , ISNULL(OUTPUT_DATE.GOODS_CD_CUST,'') AS GOODS_CD_CUST               " & vbNewLine _
                                              & "       , ISNULL(OUTPUT_DATE.GOODS_NM,'')      AS GOODS_NM                    " & vbNewLine _
                                              & "       , ISNULL(OUTPUT_DATE.LOT_NO,'')        AS LOT_NO                      " & vbNewLine _
                                              & "       , ISNULL(OUTPUT_DATE.SERIAL_NO,'')     AS SERIAL_NO                   " & vbNewLine _
                                              & "       , ISNULL(OUTPUT_DATE.IRIME,'0')        AS IRIME                       " & vbNewLine _
                                              & "       , ISNULL(OUTPUT_DATE.IRIME_UT,'')      AS IRIME_UT                    " & vbNewLine _
                                              & "       , SUM(OUTPUT_DATE.PORA_ZAI_NB)         AS NB                          " & vbNewLine _
                                              & "       , SUM(OUTPUT_DATE.PORA_ZAI_QT)         AS QT                          " & vbNewLine _
                                              & " FROM (SELECT                                                                " & vbNewLine _
                                              & "               BASE_DATE.NRS_BR_CD   AS NRS_BR_CD                            " & vbNewLine _
                                              & "             , BASE_DATE.CUST_CD_L   AS CUST_CD_L                            " & vbNewLine _
                                              & "             , BASE_DATE.CUST_CD_M   AS CUST_CD_M                            " & vbNewLine _
                                              & "             , BASE_DATE.WH_CD       AS WH_CD                                " & vbNewLine _
                                              & "             , MG.GOODS_CD_NRS       AS GOODS_CD_NRS                         " & vbNewLine _
                                              & "             , CASE WHEN CUST_D.SET_NAIYO = '03' THEN LEFT(MG.CUST_COST_CD2,20) ELSE MG.GOODS_CD_CUST END AS GOODS_CD_CUST " & vbNewLine _
                                              & "             , MG.GOODS_NM_1         AS GOODS_NM                             " & vbNewLine _
                                              & "             , BASE_DATE.LOT_NO      AS LOT_NO                               " & vbNewLine _
                                              & "             , BASE_DATE.SERIAL_NO   AS SERIAL_NO                            " & vbNewLine _
                                              & "             , ZAITRS.IRIME          AS IRIME                                " & vbNewLine _
                                              & "             , MG.STD_IRIME_UT       AS IRIME_UT                             " & vbNewLine _
                                              & "             , BASE_DATE.PORA_ZAI_NB AS PORA_ZAI_NB                          " & vbNewLine _
                                              & "             , BASE_DATE.PORA_ZAI_QT AS PORA_ZAI_QT                          " & vbNewLine _
                                              & "       FROM (SELECT                                                          " & vbNewLine _
                                              & "                     NRS_BR_CD   AS NRS_BR_CD                                " & vbNewLine _
                                              & "                   , CUST_CD_L   AS CUST_CD_L                                " & vbNewLine _
                                              & "                   , CUST_CD_M   AS CUST_CD_M                                " & vbNewLine _
                                              & "                   , WH_CD       AS WH_CD                                    " & vbNewLine _
                                              & "                   , ZAI_REC_NO  AS ZAI_REC_NO                               " & vbNewLine _
                                              & "                   , LOT_NO      AS LOT_NO                                   " & vbNewLine _
                                              & "                   , SERIAL_NO   AS SERIAL_NO                                " & vbNewLine _
                                              & "                   , PORA_ZAI_NB AS PORA_ZAI_NB                              " & vbNewLine _
                                              & "                   , PORA_ZAI_QT AS PORA_ZAI_QT                              " & vbNewLine _
                                              & "             FROM (SELECT                                                    " & vbNewLine _
                                              & "                           NRS_BR_CD        AS NRS_BR_CD                     " & vbNewLine _
                                              & "                         , CUST_CD_L        AS CUST_CD_L                     " & vbNewLine _
                                              & "                         , CUST_CD_M        AS CUST_CD_M                     " & vbNewLine _
                                              & "                         , WH_CD            AS WH_CD                         " & vbNewLine _
                                              & "                         , ZAI_REC_NO       AS ZAI_REC_NO                    " & vbNewLine _
                                              & "                         , LOT_NO           AS LOT_NO                        " & vbNewLine _
                                              & "                         , SERIAL_NO        AS SERIAL_NO                     " & vbNewLine _
                                              & "                         , SUM(PORA_ZAI_NB) AS PORA_ZAI_NB                   " & vbNewLine _
                                              & "                         , SUM(PORA_ZAI_QT) AS PORA_ZAI_QT                   " & vbNewLine _
                                              & "                   FROM                                                      " & vbNewLine _
                                              & "                        --①入荷データ(B_INKA_S)                             " & vbNewLine _
                                              & "                        (SELECT ''                                                    AS OUTKA_NO_L " & vbNewLine _
                                              & "                               , INL1.CUST_CD_L                                       AS CUST_CD_L  " & vbNewLine _
                                              & "                               , INL1.CUST_CD_M                                       AS CUST_CD_M  " & vbNewLine _
                                              & "                               , INL1.WH_CD                                           AS WH_CD      " & vbNewLine _
                                              & "                               , INS1.ZAI_REC_NO                                      AS ZAI_REC_NO " & vbNewLine _
                                              & "                               , ISNULL(INS1.LOT_NO, '')                              AS LOT_NO     " & vbNewLine _
                                              & "                               , ISNULL(INS1.SERIAL_NO, '')                           AS SERIAL_NO  " & vbNewLine _
                                              & "                               , (INS1.KONSU * MG1.PKG_NB) + INS1.HASU                AS PORA_ZAI_NB" & vbNewLine _
                                              & "                               , ((INS1.KONSU * MG1.PKG_NB) + INS1.HASU) * INS1.IRIME AS PORA_ZAI_QT" & vbNewLine _
                                              & "                               , INS1.NRS_BR_CD                                       AS NRS_BR_CD" & vbNewLine _
                                              & "                         FROM                                                " & vbNewLine _
                                              & "                              $LM_TRN$..B_INKA_L INL1                        " & vbNewLine _
                                              & "                              LEFT JOIN $LM_TRN$..B_INKA_M INM1              " & vbNewLine _
                                              & "                              ON INM1.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                              & "                              AND INM1.NRS_BR_CD = INL1.NRS_BR_CD            " & vbNewLine _
                                              & "                              AND INM1.INKA_NO_L = INL1.INKA_NO_L            " & vbNewLine _
                                              & "                              LEFT JOIN $LM_TRN$..B_INKA_S INS1              " & vbNewLine _
                                              & "                              ON INS1.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                              & "                              AND INS1.NRS_BR_CD = INM1.NRS_BR_CD            " & vbNewLine _
                                              & "                              AND INS1.INKA_NO_L = INM1.INKA_NO_L            " & vbNewLine _
                                              & "                              AND INS1.INKA_NO_M = INM1.INKA_NO_M            " & vbNewLine _
                                              & "                              LEFT JOIN $LM_MST$..M_GOODS MG1                " & vbNewLine _
                                              & "                              ON  MG1.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                              & "                              AND MG1.NRS_BR_CD = INL1.NRS_BR_CD             " & vbNewLine _
                                              & "                              AND MG1.GOODS_CD_NRS = INM1.GOODS_CD_NRS       " & vbNewLine _
                                              & "                         WHERE INL1.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                              & "                              AND INL1.NRS_BR_CD   = @NRS_BR_CD              " & vbNewLine _
                                              & "                              AND (INL1.INKA_STATE_KB > '10' OR RTRIM(INS1.ZAI_REC_NO) <> '')" & vbNewLine _
                                              & "                              AND INL1.CUST_CD_L   = @CUST_CD_L              " & vbNewLine _
                                              & "                              AND INL1.CUST_CD_M   = @CUST_CD_M              " & vbNewLine _
                                              & "                              AND INL1.INKA_DATE  <= @JISSHI_DATE            " & vbNewLine _
                                              & "                              AND (INL1.INKA_DATE > '00000000' OR INL1.INKA_STATE_KB < '50')" & vbNewLine _
                                              & "                         --②在庫移動分を加減算(D_IDO_TRS)                   " & vbNewLine _
                                              & "                         --②移動後                                          " & vbNewLine _
                                              & "                         UNION ALL                                           " & vbNewLine _
                                              & "                         SELECT ''                                 AS OUTKA_NO_L  " & vbNewLine _
                                              & "                               , ZAI4.CUST_CD_L                    AS CUST_CD_L   " & vbNewLine _
                                              & "                               , ZAI4.CUST_CD_M                    AS CUST_CD_M   " & vbNewLine _
                                              & "                               , ZAI4.WH_CD                        AS WH_CD       " & vbNewLine _
                                              & "                               , IDO1.N_ZAI_REC_NO                 AS ZAI_REC_NO  " & vbNewLine _
                                              & "                               , ISNULL(ZAI4.LOT_NO, '')           AS LOT_NO      " & vbNewLine _
                                              & "                               , ISNULL(ZAI4.SERIAL_NO, '')        AS SERIAL_NO   " & vbNewLine _
                                              & "                               , IDO1.N_PORA_ZAI_NB                AS PORA_ZAI_NB " & vbNewLine _
                                              & "                               , IDO1.N_PORA_ZAI_NB * IDO1.ZAIK_IRIME AS PORA_ZAI_QT " & vbNewLine _
                                              & "                               , IDO1.NRS_BR_CD                    AS NRS_BR_CD   " & vbNewLine _
                                              & "                         FROM $LM_TRN$..D_IDO_TRS IDO1                       " & vbNewLine _
                                              & "                              LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI4             " & vbNewLine _
                                              & "                              ON  ZAI4.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                              & "                              AND ZAI4.NRS_BR_CD = IDO1.NRS_BR_CD            " & vbNewLine _
                                              & "                              AND ZAI4.ZAI_REC_NO = IDO1.N_ZAI_REC_NO        " & vbNewLine _
                                              & "                         WHERE IDO1.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                              & "                              AND IDO1.NRS_BR_CD   = @NRS_BR_CD              " & vbNewLine _
                                              & "                              AND ZAI4.CUST_CD_L   = @CUST_CD_L              " & vbNewLine _
                                              & "                              AND ZAI4.CUST_CD_M   = @CUST_CD_M              " & vbNewLine _
                                              & "                              AND IDO1.IDO_DATE   <= @JISSHI_DATE            " & vbNewLine _
                                              & "                         --②移動前                                          " & vbNewLine _
                                              & "                         UNION ALL                                           " & vbNewLine _
                                              & "                         SELECT ''                                      AS OUTKA_NO_L  " & vbNewLine _
                                              & "                               , ZAI5.CUST_CD_L                         AS CUST_CD_L   " & vbNewLine _
                                              & "                               , ZAI5.CUST_CD_M                         AS CUST_CD_M   " & vbNewLine _
                                              & "                               , ZAI5.WH_CD                             AS WH_CD       " & vbNewLine _
                                              & "                               , IDO2.O_ZAI_REC_NO                      AS ZAI_REC_NO  " & vbNewLine _
                                              & "                               , ISNULL(ZAI5.LOT_NO, '')                AS LOT_NO      " & vbNewLine _
                                              & "                               , ISNULL(ZAI5.SERIAL_NO, '')             AS SERIAL_NO   " & vbNewLine _
                                              & "                               , IDO2.N_PORA_ZAI_NB * -1                AS PORA_ZAI_NB " & vbNewLine _
                                              & "                               , IDO2.N_PORA_ZAI_NB * IDO2.ZAIK_IRIME * -1 AS PORA_ZAI_QT " & vbNewLine _
                                              & "                               , IDO2.NRS_BR_CD                         AS NRS_BR_CD   " & vbNewLine _
                                              & "                         FROM $LM_TRN$..D_IDO_TRS IDO2                       " & vbNewLine _
                                              & "                              LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI5             " & vbNewLine _
                                              & "                              ON ZAI5.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                              & "                              AND ZAI5.NRS_BR_CD = IDO2.NRS_BR_CD            " & vbNewLine _
                                              & "                              AND ZAI5.ZAI_REC_NO = IDO2.O_ZAI_REC_NO        " & vbNewLine _
                                              & "                         WHERE IDO2.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                              & "                              AND IDO2.NRS_BR_CD   = @NRS_BR_CD              " & vbNewLine _
                                              & "                              AND ZAI5.CUST_CD_L   = @CUST_CD_L              " & vbNewLine _
                                              & "                              AND ZAI5.CUST_CD_M   = @CUST_CD_M              " & vbNewLine _
                                              & "                              AND IDO2.IDO_DATE   <= @JISSHI_DATE            " & vbNewLine _
                                              & "                         --③出荷データ(C_OUTKA_S)                           " & vbNewLine _
                                              & "                         UNION ALL                                           " & vbNewLine _
                                              & "                         SELECT                                              " & vbNewLine _
                                              & "                                 OUTKA_NO_L                                  " & vbNewLine _
                                              & "                               , CUST_CD_L                                   " & vbNewLine _
                                              & "                               , CUST_CD_M                                   " & vbNewLine _
                                              & "                               , WH_CD                                       " & vbNewLine _
                                              & "                               , ZAI_REC_NO                                  " & vbNewLine _
                                              & "                               , LOT_NO                                      " & vbNewLine _
                                              & "                               , SERIAL_NO                                   " & vbNewLine _
                                              & "                               , PORA_ZAI_NB                                 " & vbNewLine _
                                              & "                               , PORA_ZAI_QT                                 " & vbNewLine _
                                              & "                               , NRS_BR_CD                                   " & vbNewLine _
                                              & "                         FROM                                                " & vbNewLine _
                                              & "                             (SELECT                                         " & vbNewLine _
                                              & "                                      OUTS.OUTKA_NO_L            AS OUTKA_NO_L  " & vbNewLine _
                                              & "                                    , OUTL.CUST_CD_L             AS CUST_CD_L   " & vbNewLine _
                                              & "                                    , OUTL.CUST_CD_M             AS CUST_CD_M   " & vbNewLine _
                                              & "                                    , OUTL.WH_CD                 AS WH_CD       " & vbNewLine _
                                              & "                                    , OUTS.ZAI_REC_NO            AS ZAI_REC_NO  " & vbNewLine _
                                              & "                                    , ISNULL(OUTS.LOT_NO, '')    AS LOT_NO      " & vbNewLine _
                                              & "                                    , ISNULL(OUTS.SERIAL_NO, '') AS SERIAL_NO   " & vbNewLine _
                                              & "                                    , OUTS.ALCTD_NB * -1         AS PORA_ZAI_NB " & vbNewLine _
                                              & "                                    , OUTS.ALCTD_QT * -1         AS PORA_ZAI_QT " & vbNewLine _
                                              & "                                    , OUTS.NRS_BR_CD             AS NRS_BR_CD   " & vbNewLine _
                                              & "                              FROM $LM_TRN$..C_OUTKA_L OUTL                  " & vbNewLine _
                                              & "                                   LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM        " & vbNewLine _
                                              & "                                   ON  OUTM.SYS_DEL_FLG = '0'                " & vbNewLine _
                                              & "                                   AND OUTM.NRS_BR_CD    = OUTL.NRS_BR_CD    " & vbNewLine _
                                              & "                                   AND OUTM.OUTKA_NO_L   = OUTL.OUTKA_NO_L   " & vbNewLine _
                                              & "                                   LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS        " & vbNewLine _
                                              & "                                   ON OUTS.SYS_DEL_FLG  = '0'                " & vbNewLine _
                                              & "                                   AND OUTS.NRS_BR_CD    = OUTL.NRS_BR_CD    " & vbNewLine _
                                              & "                                   AND OUTS.OUTKA_NO_L   = OUTL.OUTKA_NO_L   " & vbNewLine _
                                              & "                                   AND OUTS.OUTKA_NO_M   = OUTM.OUTKA_NO_M   " & vbNewLine _
                                              & "                              WHERE OUTL.SYS_DEL_FLG      = '0'              " & vbNewLine _
                                              & "                                   AND OUTM.ALCTD_KB        <> '04'          " & vbNewLine _
                                              & "                                   AND OUTL.OUTKA_STATE_KB  >= '60'          " & vbNewLine _
                                              & "                                   AND OUTL.NRS_BR_CD        = @NRS_BR_CD    " & vbNewLine _
                                              & "                                   AND OUTL.CUST_CD_L        = @CUST_CD_L    " & vbNewLine _
                                              & "                                   AND OUTL.CUST_CD_M        = @CUST_CD_M    " & vbNewLine _
                                              & "                                   AND OUTL.OUTKA_PLAN_DATE <= @JISSHI_DATE  " & vbNewLine _
                                              & "                              ) BASE3                                        " & vbNewLine _
                                              & "                         ) BASE4                                             " & vbNewLine _
                                              & "                   WHERE CUST_CD_L = @CUST_CD_L                              " & vbNewLine _
                                              & "                         AND CUST_CD_M = @CUST_CD_M                          " & vbNewLine _
                                              & "                   GROUP BY                                                  " & vbNewLine _
                                              & "                         CUST_CD_L                                           " & vbNewLine _
                                              & "                       , CUST_CD_M                                           " & vbNewLine _
                                              & "                       , WH_CD                                               " & vbNewLine _
                                              & "                       , ZAI_REC_NO                                          " & vbNewLine _
                                              & "                       , LOT_NO                                              " & vbNewLine _
                                              & "                       , SERIAL_NO                                           " & vbNewLine _
                                              & "                       , NRS_BR_CD                                           " & vbNewLine _
                                              & "             ) BASE                                                          " & vbNewLine _
                                              & "       WHERE (BASE.PORA_ZAI_NB > 0 OR BASE.PORA_ZAI_QT > 0)                  " & vbNewLine _
                                              & "       ) BASE_DATE                                                           " & vbNewLine _
                                              & " LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAITRS                                        " & vbNewLine _
                                              & " ON ZAITRS.NRS_BR_CD    = BASE_DATE.NRS_BR_CD                                " & vbNewLine _
                                              & " AND ZAITRS.ZAI_REC_NO   = BASE_DATE.ZAI_REC_NO                              " & vbNewLine _
                                              & " AND ZAITRS.SYS_DEL_FLG  = '0'                                               " & vbNewLine _
                                              & " LEFT JOIN $LM_MST$..M_GOODS MG                                              " & vbNewLine _
                                              & " ON ZAITRS.NRS_BR_CD    = MG.NRS_BR_CD                                       " & vbNewLine _
                                              & " AND ZAITRS.GOODS_CD_NRS = MG.GOODS_CD_NRS                                   " & vbNewLine _
                                              & " LEFT JOIN $LM_MST$..M_CUST_DETAILS CUST_D                                   " & vbNewLine _
                                              & " ON BASE_DATE.CUST_CD_L + BASE_DATE.CUST_CD_M = CUST_D.CUST_CD               " & vbNewLine _
                                              & " --2015/10/26 拠点CD条件追加 ADACHI                                  " & vbNewLine _
                                              & " AND BASE_DATE.NRS_BR_CD = CUST_D.NRS_BR_CD                                  " & vbNewLine _
                                              & " AND CUST_D.SUB_KB = '36'                                                    " & vbNewLine _
                                              & " AND CUST_D.SET_NAIYO = '03'                                                 " & vbNewLine _
                                              & " AND CUST_D.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                              & " ) OUTPUT_DATE                                                               " & vbNewLine _
                                              & " GROUP BY                                                                    " & vbNewLine _
                                              & "       OUTPUT_DATE.NRS_BR_CD                                                 " & vbNewLine _
                                              & "     , OUTPUT_DATE.CUST_CD_L                                                 " & vbNewLine _
                                              & "     , OUTPUT_DATE.CUST_CD_M                                                 " & vbNewLine _
                                              & "     , OUTPUT_DATE.WH_CD                                                     " & vbNewLine _
                                              & "     , OUTPUT_DATE.GOODS_CD_NRS                                              " & vbNewLine _
                                              & "     , OUTPUT_DATE.GOODS_CD_CUST                                             " & vbNewLine _
                                              & "     , OUTPUT_DATE.GOODS_NM                                                  " & vbNewLine _
                                              & "     , OUTPUT_DATE.LOT_NO                                                    " & vbNewLine _
                                              & "     , OUTPUT_DATE.SERIAL_NO                                                 " & vbNewLine _
                                              & "     , OUTPUT_DATE.IRIME                                                     " & vbNewLine _
                                              & "     , OUTPUT_DATE.IRIME_UT                                                  " & vbNewLine _
                                              & " ORDER BY                                                                    " & vbNewLine _
                                              & "       OUTPUT_DATE.GOODS_CD_NRS                                              " & vbNewLine _
                                              & "     , OUTPUT_DATE.LOT_NO                                                    " & vbNewLine _
                                              & "     , OUTPUT_DATE.IRIME                                                     " & vbNewLine
#Else
    ''' <summary>
    ''' 月末在庫データの検索 SQL 長いSQLで分けると、意味がわからなくなりそうなので、あえて分けずに記載
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ZAIZAN As String = " SELECT                                                                      " & vbNewLine _
                                          & "         OUTPUT_DATE.NRS_BR_CD                AS NRS_BR_CD                   " & vbNewLine _
                                          & "       , OUTPUT_DATE.CUST_CD_L                AS CUST_CD_L                   " & vbNewLine _
                                          & "       , OUTPUT_DATE.CUST_CD_M                AS CUST_CD_M                   " & vbNewLine _
                                          & "       , @JISSHI_DATE                         AS CHECK_DATE                  " & vbNewLine _
                                          & "       , ''                                   AS GYO_NO                      " & vbNewLine _
                                          & "       , @EDA_NO                              AS EDA_NO                      " & vbNewLine _
                                          & "       , OUTPUT_DATE.WH_CD                    AS WH_CD                       " & vbNewLine _
                                          & "       , ISNULL(OUTPUT_DATE.GOODS_CD_NRS,'')  AS GOODS_CD_NRS                " & vbNewLine _
                                          & "       , ISNULL(OUTPUT_DATE.GOODS_CD_CUST,'') AS GOODS_CD_CUST               " & vbNewLine _
                                          & "       , ISNULL(OUTPUT_DATE.GOODS_NM,'')      AS GOODS_NM                    " & vbNewLine _
                                          & "       , ISNULL(OUTPUT_DATE.LOT_NO,'')        AS LOT_NO                      " & vbNewLine _
                                          & "       , ISNULL(OUTPUT_DATE.SERIAL_NO,'')     AS SERIAL_NO                   " & vbNewLine _
                                          & "       , ISNULL(OUTPUT_DATE.IRIME,'0')        AS IRIME                       " & vbNewLine _
                                          & "       , ISNULL(OUTPUT_DATE.IRIME_UT,'')      AS IRIME_UT                    " & vbNewLine _
                                          & "       , SUM(OUTPUT_DATE.PORA_ZAI_NB)         AS NB                          " & vbNewLine _
                                          & "       , SUM(OUTPUT_DATE.PORA_ZAI_QT)         AS QT                          " & vbNewLine _
                                          & " FROM (SELECT                                                                " & vbNewLine _
                                          & "               BASE_DATE.NRS_BR_CD   AS NRS_BR_CD                            " & vbNewLine _
                                          & "             , BASE_DATE.CUST_CD_L   AS CUST_CD_L                            " & vbNewLine _
                                          & "             , BASE_DATE.CUST_CD_M   AS CUST_CD_M                            " & vbNewLine _
                                          & "             , BASE_DATE.WH_CD       AS WH_CD                                " & vbNewLine _
                                          & "             , MG.GOODS_CD_NRS       AS GOODS_CD_NRS                         " & vbNewLine _
                                          & "             , CASE WHEN CUST_D.SET_NAIYO = '03' THEN LEFT(MG.CUST_COST_CD2,20) ELSE MG.GOODS_CD_CUST END AS GOODS_CD_CUST " & vbNewLine _
                                          & "             , MG.GOODS_NM_1         AS GOODS_NM                             " & vbNewLine _
                                          & "             , BASE_DATE.LOT_NO      AS LOT_NO                               " & vbNewLine _
                                          & "             , BASE_DATE.SERIAL_NO   AS SERIAL_NO                            " & vbNewLine _
                                          & "             , ZAITRS.IRIME          AS IRIME                                " & vbNewLine _
                                          & "             , MG.STD_IRIME_UT       AS IRIME_UT                             " & vbNewLine _
                                          & "             , BASE_DATE.PORA_ZAI_NB AS PORA_ZAI_NB                          " & vbNewLine _
                                          & "             , BASE_DATE.PORA_ZAI_QT AS PORA_ZAI_QT                          " & vbNewLine _
                                          & "       FROM (SELECT                                                          " & vbNewLine _
                                          & "                     NRS_BR_CD   AS NRS_BR_CD                                " & vbNewLine _
                                          & "                   , CUST_CD_L   AS CUST_CD_L                                " & vbNewLine _
                                          & "                   , CUST_CD_M   AS CUST_CD_M                                " & vbNewLine _
                                          & "                   , WH_CD       AS WH_CD                                    " & vbNewLine _
                                          & "                   , ZAI_REC_NO  AS ZAI_REC_NO                               " & vbNewLine _
                                          & "                   , LOT_NO      AS LOT_NO                                   " & vbNewLine _
                                          & "                   , SERIAL_NO   AS SERIAL_NO                                " & vbNewLine _
                                          & "                   , PORA_ZAI_NB AS PORA_ZAI_NB                              " & vbNewLine _
                                          & "                   , PORA_ZAI_QT AS PORA_ZAI_QT                              " & vbNewLine _
                                          & "             FROM (SELECT                                                    " & vbNewLine _
                                          & "                           NRS_BR_CD        AS NRS_BR_CD                     " & vbNewLine _
                                          & "                         , CUST_CD_L        AS CUST_CD_L                     " & vbNewLine _
                                          & "                         , CUST_CD_M        AS CUST_CD_M                     " & vbNewLine _
                                          & "                         , WH_CD            AS WH_CD                         " & vbNewLine _
                                          & "                         , ZAI_REC_NO       AS ZAI_REC_NO                    " & vbNewLine _
                                          & "                         , LOT_NO           AS LOT_NO                        " & vbNewLine _
                                          & "                         , SERIAL_NO        AS SERIAL_NO                     " & vbNewLine _
                                          & "                         , SUM(PORA_ZAI_NB) AS PORA_ZAI_NB                   " & vbNewLine _
                                          & "                         , SUM(PORA_ZAI_QT) AS PORA_ZAI_QT                   " & vbNewLine _
                                          & "                   FROM (  --①入荷データ+②移動後+②移動 抽出結果                                 " & vbNewLine _
                                          & "                        SELECT   ''                                              AS OUTKA_NO_L     " & vbNewLine _
                                          & "                               , CHKTBL.CUST_CD_L                                AS CUST_CD_L      " & vbNewLine _
                                          & "                               , CHKTBL.CUST_CD_M                                AS CUST_CD_M      " & vbNewLine _
                                          & "                               , CHKTBL.WH_CD                                    AS WH_CD          " & vbNewLine _
                                          & "                               , CHKTBL.ZAI_REC_NO                               AS ZAI_REC_NO     " & vbNewLine _
                                          & "                               , CHKTBL.LOT_NO                                   AS LOT_NO         " & vbNewLine _
                                          & "                               , CHKTBL.SERIAL_NO                                AS SERIAL_NO      " & vbNewLine _
                                          & "                               , CHKTBL.PORA_ZAI_NB                              AS PORA_ZAI_NB    " & vbNewLine _
                                          & "                               , CHKTBL.PORA_ZAI_QT                              AS PORA_ZAI_QT    " & vbNewLine _
                                          & "                               , CHKTBL.NRS_BR_CD                                AS NRS_BR_CD      " & vbNewLine _
                                          & "                           FROM  $LM_TRN$..D_WK_TORIKOMIZUMI_CHK CHKTBL                            " & vbNewLine _
                                          & "                           WHERE CHKTBL.CUST_CD_L = @CUST_CD_L                                     " & vbNewLine _
                                          & "                             AND CHKTBL.CUST_CD_M = @CUST_CD_M                                     " & vbNewLine _
                                          & "                             AND CHKTBL.NRS_BR_CD = @NRS_BR_CD                                     " & vbNewLine _
                                          & "                         --③出荷データ(C_OUTKA_S)                           " & vbNewLine _
                                          & "                         UNION ALL                                           " & vbNewLine _
                                          & "                         SELECT                                              " & vbNewLine _
                                          & "                                 OUTKA_NO_L                                  " & vbNewLine _
                                          & "                               , CUST_CD_L                                   " & vbNewLine _
                                          & "                               , CUST_CD_M                                   " & vbNewLine _
                                          & "                               , WH_CD                                       " & vbNewLine _
                                          & "                               , ZAI_REC_NO                                  " & vbNewLine _
                                          & "                               , LOT_NO                                      " & vbNewLine _
                                          & "                               , SERIAL_NO                                   " & vbNewLine _
                                          & "                               , PORA_ZAI_NB                                 " & vbNewLine _
                                          & "                               , PORA_ZAI_QT                                 " & vbNewLine _
                                          & "                               , NRS_BR_CD                                   " & vbNewLine _
                                          & "                         FROM                                                " & vbNewLine _
                                          & "                             (SELECT                                         " & vbNewLine _
                                          & "                                      OUTS.OUTKA_NO_L            AS OUTKA_NO_L  " & vbNewLine _
                                          & "                                    , OUTL.CUST_CD_L             AS CUST_CD_L   " & vbNewLine _
                                          & "                                    , OUTL.CUST_CD_M             AS CUST_CD_M   " & vbNewLine _
                                          & "                                    , OUTL.WH_CD                 AS WH_CD       " & vbNewLine _
                                          & "                                    , OUTS.ZAI_REC_NO            AS ZAI_REC_NO  " & vbNewLine _
                                          & "                                    , ISNULL(OUTS.LOT_NO, '')    AS LOT_NO      " & vbNewLine _
                                          & "                                    , ISNULL(OUTS.SERIAL_NO, '') AS SERIAL_NO   " & vbNewLine _
                                          & "                                    , OUTS.ALCTD_NB * -1         AS PORA_ZAI_NB " & vbNewLine _
                                          & "                                    , OUTS.ALCTD_QT * -1         AS PORA_ZAI_QT " & vbNewLine _
                                          & "                                    , OUTS.NRS_BR_CD             AS NRS_BR_CD   " & vbNewLine _
                                          & "                              FROM $LM_TRN$..C_OUTKA_L OUTL                  " & vbNewLine _
                                          & "                                   LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM        " & vbNewLine _
                                          & "                                   ON  OUTM.SYS_DEL_FLG = '0'                " & vbNewLine _
                                          & "                                   AND OUTM.NRS_BR_CD    = OUTL.NRS_BR_CD    " & vbNewLine _
                                          & "                                   AND OUTM.OUTKA_NO_L   = OUTL.OUTKA_NO_L   " & vbNewLine _
                                          & "                                   LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS        " & vbNewLine _
                                          & "                                   ON OUTS.SYS_DEL_FLG  = '0'                " & vbNewLine _
                                          & "                                   AND OUTS.NRS_BR_CD    = OUTL.NRS_BR_CD    " & vbNewLine _
                                          & "                                   AND OUTS.OUTKA_NO_L   = OUTL.OUTKA_NO_L   " & vbNewLine _
                                          & "                                   AND OUTS.OUTKA_NO_M   = OUTM.OUTKA_NO_M   " & vbNewLine _
                                          & "                              WHERE OUTL.SYS_DEL_FLG      = '0'              " & vbNewLine _
                                          & "                                   AND OUTM.ALCTD_KB        <> '04'          " & vbNewLine _
                                          & "                                   AND OUTL.OUTKA_STATE_KB  >= '60'          " & vbNewLine _
                                          & "                                   AND OUTL.NRS_BR_CD        = @NRS_BR_CD    " & vbNewLine _
                                          & "                                   AND OUTL.CUST_CD_L        = @CUST_CD_L    " & vbNewLine _
                                          & "                                   AND OUTL.CUST_CD_M        = @CUST_CD_M    " & vbNewLine _
                                          & "                                   AND OUTL.OUTKA_PLAN_DATE <= @JISSHI_DATE  " & vbNewLine _
                                          & "                              ) BASE3                                        " & vbNewLine _
                                          & "                         ) BASE4                                             " & vbNewLine _
                                          & "                   WHERE CUST_CD_L = @CUST_CD_L                              " & vbNewLine _
                                          & "                         AND CUST_CD_M = @CUST_CD_M                          " & vbNewLine _
                                          & "                   GROUP BY                                                  " & vbNewLine _
                                          & "                         CUST_CD_L                                           " & vbNewLine _
                                          & "                       , CUST_CD_M                                           " & vbNewLine _
                                          & "                       , WH_CD                                               " & vbNewLine _
                                          & "                       , ZAI_REC_NO                                          " & vbNewLine _
                                          & "                       , LOT_NO                                              " & vbNewLine _
                                          & "                       , SERIAL_NO                                           " & vbNewLine _
                                          & "                       , NRS_BR_CD                                           " & vbNewLine _
                                          & "             ) BASE                                                          " & vbNewLine _
                                          & "       WHERE (BASE.PORA_ZAI_NB > 0 OR BASE.PORA_ZAI_QT > 0)                  " & vbNewLine _
                                          & "       ) BASE_DATE                                                           " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAITRS                                        " & vbNewLine _
                                          & " ON ZAITRS.NRS_BR_CD    = BASE_DATE.NRS_BR_CD                                " & vbNewLine _
                                          & " AND ZAITRS.ZAI_REC_NO   = BASE_DATE.ZAI_REC_NO                              " & vbNewLine _
                                          & " AND ZAITRS.SYS_DEL_FLG  = '0'                                               " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_GOODS MG                                              " & vbNewLine _
                                          & " ON ZAITRS.NRS_BR_CD    = MG.NRS_BR_CD                                       " & vbNewLine _
                                          & " AND ZAITRS.GOODS_CD_NRS = MG.GOODS_CD_NRS                                   " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_CUST_DETAILS CUST_D                                   " & vbNewLine _
                                          & " ON BASE_DATE.CUST_CD_L + BASE_DATE.CUST_CD_M = CUST_D.CUST_CD               " & vbNewLine _
                                          & " --2015/10/26 拠点CD条件追加 ADACHI                                  " & vbNewLine _
                                          & " AND BASE_DATE.NRS_BR_CD = CUST_D.NRS_BR_CD                                  " & vbNewLine _
                                          & " AND CUST_D.SUB_KB = '36'                                                    " & vbNewLine _
                                          & " AND CUST_D.SET_NAIYO = '03'                                                 " & vbNewLine _
                                          & " AND CUST_D.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                          & " ) OUTPUT_DATE                                                               " & vbNewLine _
                                          & " GROUP BY                                                                    " & vbNewLine _
                                          & "       OUTPUT_DATE.NRS_BR_CD                                                 " & vbNewLine _
                                          & "     , OUTPUT_DATE.CUST_CD_L                                                 " & vbNewLine _
                                          & "     , OUTPUT_DATE.CUST_CD_M                                                 " & vbNewLine _
                                          & "     , OUTPUT_DATE.WH_CD                                                     " & vbNewLine _
                                          & "     , OUTPUT_DATE.GOODS_CD_NRS                                              " & vbNewLine _
                                          & "     , OUTPUT_DATE.GOODS_CD_CUST                                             " & vbNewLine _
                                          & "     , OUTPUT_DATE.GOODS_NM                                                  " & vbNewLine _
                                          & "     , OUTPUT_DATE.LOT_NO                                                    " & vbNewLine _
                                          & "     , OUTPUT_DATE.SERIAL_NO                                                 " & vbNewLine _
                                          & "     , OUTPUT_DATE.IRIME                                                     " & vbNewLine _
                                          & "     , OUTPUT_DATE.IRIME_UT                                                  " & vbNewLine _
                                          & " ORDER BY                                                                    " & vbNewLine _
                                          & "       OUTPUT_DATE.GOODS_CD_NRS                                              " & vbNewLine _
                                          & "     , OUTPUT_DATE.LOT_NO                                                    " & vbNewLine _
                                          & "     , OUTPUT_DATE.IRIME                                                     " & vbNewLine
#End If


#End Region

#End Region

#Region "荷主在庫数データサマリの検索"

#Region "荷主在庫数データサマリの検索 SQL SELECT句"

    ''' <summary>
    ''' 荷主在庫数データサマリの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ZAISHOCUSTSUM As String = " SELECT                                                                     " & vbNewLine _
                                                     & " ZAISHOCUSTSUM.NRS_BR_CD                      AS NRS_BR_CD                  " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.CUST_CD_L                      AS CUST_CD_L                  " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.CUST_CD_M                      AS CUST_CD_M                  " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.CHECK_DATE                     AS CHECK_DATE                 " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.GYO_NO                         AS GYO_NO                     " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.EDA_NO                         AS EDA_NO                     " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FILE_NAME                      AS FILE_NAME                  " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FILE_FOLDER                    AS FILE_FOLDER                " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.WH_CD                          AS WH_CD                      " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.CUST_GOODS_CD                  AS GOODS_CD_CUST              " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.GOODS_NM                       AS GOODS_NM                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.LOT_NO                         AS LOT_NO                     " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.SERIAL_NO                      AS SERIAL_NO                  " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.IRIME                          AS IRIME                      " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.IRIME_UT                       AS IRIME_UT                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.CLASS_1                        AS CLASS_1                    " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.CLASS_2                        AS CLASS_2                    " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.CLASS_3                        AS CLASS_3                    " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.CLASS_4                        AS CLASS_4                    " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.CLASS_5                        AS CLASS_5                    " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.NB                             AS NB                         " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.QT                             AS QT                         " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_N01                       AS FREE_N01                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_N02                       AS FREE_N02                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_N03                       AS FREE_N03                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_N04                       AS FREE_N04                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_N05                       AS FREE_N05                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_N06                       AS FREE_N06                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_N07                       AS FREE_N07                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_N08                       AS FREE_N08                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_N09                       AS FREE_N09                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_N10                       AS FREE_N10                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_C01                       AS FREE_C01                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_C02                       AS FREE_C02                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_C03                       AS FREE_C03                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_C04                       AS FREE_C04                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_C05                       AS FREE_C05                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_C06                       AS FREE_C06                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_C07                       AS FREE_C07                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_C08                       AS FREE_C08                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_C09                       AS FREE_C09                   " & vbNewLine _
                                                     & ",ZAISHOCUSTSUM.FREE_C10                       AS FREE_C10                   " & vbNewLine

#End Region

#Region "荷主在庫数データサマリの検索 SQL FROM句"

    ''' <summary>
    ''' 荷主在庫数データサマリの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_ZAISHOCUSTSUM As String = "FROM                                                                  " & vbNewLine _
                                                          & "$LM_TRN$..D_ZAI_SHOGOH_CUST_SUM ZAISHOCUSTSUM                         " & vbNewLine

#Region "荷主在庫数データサマリの検索 SQL ORDER BY句"

    ''' <summary>
    ''' 荷主在庫数データサマリの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_ZAISHOCUSTSUM As String = "ORDER BY                                                             " & vbNewLine _
                                                           & " ZAISHOCUSTSUM.CUST_GOODS_CD                                         " & vbNewLine _
                                                           & ",ZAISHOCUSTSUM.LOT_NO                                                " & vbNewLine _
                                                           & ",ZAISHOCUSTSUM.SERIAL_NO                                             " & vbNewLine _
                                                           & ",ZAISHOCUSTSUM.IRIME                                                 " & vbNewLine

#End Region

#End Region

#End Region

#Region "荷主在庫数データ取込制御マスタの検索"

#Region "荷主在庫数データ取込制御マスタの検索 SQL SELECT句"

    ''' <summary>
    ''' 荷主在庫数データ取込制御マスタの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ZAISHO As String = " SELECT                                                                     " & vbNewLine _
                                              & " ZAISHO.NRS_BR_CD                             AS NRS_BR_CD                  " & vbNewLine _
                                              & ",ZAISHO.CUST_CD_L                             AS CUST_CD_L                  " & vbNewLine _
                                              & ",ZAISHO.CUST_CD_M                             AS CUST_CD_M                  " & vbNewLine _
                                              & ",ZAISHO.EDA_NO                                AS EDA_NO                     " & vbNewLine _
                                              & ",ZAISHO.SHOGOH_NAME                           AS SHOGOH_NAME                " & vbNewLine _
                                              & ",ZAISHO.WH_CD                                 AS WH_CD                      " & vbNewLine _
                                              & ",ZAISHO.HEADER_LINE                           AS HEADER_LINE                " & vbNewLine _
                                              & ",ZAISHO.RB_KB                                 AS RB_KB                      " & vbNewLine _
                                              & ",ZAISHO.COL_RB_1                              AS COL_RB_1                   " & vbNewLine _
                                              & ",ZAISHO.DATA_RB_1                             AS DATA_RB_1                  " & vbNewLine _
                                              & ",ZAISHO.COL_RB_2                              AS COL_RB_2                   " & vbNewLine _
                                              & ",ZAISHO.DATA_RB_2                             AS DATA_RB_2                  " & vbNewLine _
                                              & ",ZAISHO.COL_RB_3                              AS COL_RB_3                   " & vbNewLine _
                                              & ",ZAISHO.DATA_RB_3                             AS DATA_RB_3                  " & vbNewLine _
                                              & ",ZAISHO.COL_SKIP_1                            AS COL_SKIP_1                 " & vbNewLine _
                                              & ",ZAISHO.DATA_SKIP_1                           AS DATA_SKIP_1                " & vbNewLine _
                                              & ",ZAISHO.COL_SKIP_2                            AS COL_SKIP_2                 " & vbNewLine _
                                              & ",ZAISHO.DATA_SKIP_2                           AS DATA_SKIP_2                " & vbNewLine _
                                              & ",ZAISHO.COL_SKIP_3                            AS COL_SKIP_3                 " & vbNewLine _
                                              & ",ZAISHO.DATA_SKIP_3                           AS DATA_SKIP_3                " & vbNewLine _
                                              & ",ZAISHO.COL_SKIP_4                            AS COL_SKIP_4                 " & vbNewLine _
                                              & ",ZAISHO.DATA_SKIP_4                           AS DATA_SKIP_4                " & vbNewLine _
                                              & ",ZAISHO.COL_SKIP_5                            AS COL_SKIP_5                 " & vbNewLine _
                                              & ",ZAISHO.DATA_SKIP_5                           AS DATA_SKIP_5                " & vbNewLine _
                                              & ",ZAISHO.COL_CUST_GOODS_CD_1                   AS COL_CUST_GOODS_CD_1        " & vbNewLine _
                                              & ",ZAISHO.COL_CUST_GOODS_CD_2                   AS COL_CUST_GOODS_CD_2        " & vbNewLine _
                                              & ",ZAISHO.COL_CUST_GOODS_CD_3                   AS COL_CUST_GOODS_CD_3        " & vbNewLine _
                                              & ",ZAISHO.INIT_CUST_GOODS_CD                    AS INIT_CUST_GOODS_CD         " & vbNewLine _
                                              & ",ZAISHO.COL_GOODS_NM_1                        AS COL_GOODS_NM_1             " & vbNewLine _
                                              & ",ZAISHO.COL_GOODS_NM_2                        AS COL_GOODS_NM_2             " & vbNewLine _
                                              & ",ZAISHO.COL_GOODS_NM_3                        AS COL_GOODS_NM_3             " & vbNewLine _
                                              & ",ZAISHO.INIT_GOODS_NM                         AS INIT_GOODS_NM              " & vbNewLine _
                                              & ",ZAISHO.COL_LOT_NO                            AS COL_LOT_NO                 " & vbNewLine _
                                              & ",ZAISHO.INIT_LOT_NO                           AS INIT_LOT_NO                " & vbNewLine _
                                              & ",ZAISHO.COL_SERIAL_NO                         AS COL_SERIAL_NO              " & vbNewLine _
                                              & ",ZAISHO.INIT_SERIAL_NO                        AS INIT_SERIAL_NO             " & vbNewLine _
                                              & ",ZAISHO.COL_IRIME                             AS COL_IRIME                  " & vbNewLine _
                                              & ",ZAISHO.INIT_IRIME                            AS INIT_IRIME                 " & vbNewLine _
                                              & ",ZAISHO.CAL_IRIME                             AS CAL_IRIME                  " & vbNewLine _
                                              & ",ZAISHO.COL_IRIME_UT                          AS COL_IRIME_UT               " & vbNewLine _
                                              & ",ZAISHO.INIT_IRIME_UT                         AS INIT_IRIME_UT              " & vbNewLine _
                                              & ",ZAISHO.COL_CLASS_1                           AS COL_CLASS_1                " & vbNewLine _
                                              & ",ZAISHO.INIT_CLASS_1                          AS INIT_CLASS_1               " & vbNewLine _
                                              & ",ZAISHO.COL_CLASS_2                           AS COL_CLASS_2                " & vbNewLine _
                                              & ",ZAISHO.INIT_CLASS_2                          AS INIT_CLASS_2               " & vbNewLine _
                                              & ",ZAISHO.COL_CLASS_3                           AS COL_CLASS_3                " & vbNewLine _
                                              & ",ZAISHO.INIT_CLASS_3                          AS INIT_CLASS_3               " & vbNewLine _
                                              & ",ZAISHO.COL_CLASS_4                           AS COL_CLASS_4                " & vbNewLine _
                                              & ",ZAISHO.INIT_CLASS_4                          AS INIT_CLASS_4               " & vbNewLine _
                                              & ",ZAISHO.COL_CLASS_5                           AS COL_CLASS_5                " & vbNewLine _
                                              & ",ZAISHO.INIT_CLASS_5                          AS INIT_CLASS_5               " & vbNewLine _
                                              & ",ZAISHO.CHK_NB                                AS CHK_NB                     " & vbNewLine _
                                              & ",ZAISHO.COL_NB                                AS COL_NB                     " & vbNewLine _
                                              & ",ZAISHO.INIT_NB                               AS INIT_NB                    " & vbNewLine _
                                              & ",ZAISHO.CAL_NB                                AS CAL_NB                     " & vbNewLine _
                                              & ",ZAISHO.CHK_QT                                AS CHK_QT                     " & vbNewLine _
                                              & ",ZAISHO.COL_QT                                AS COL_QT                     " & vbNewLine _
                                              & ",ZAISHO.INIT_QT                               AS INIT_QT                    " & vbNewLine _
                                              & ",ZAISHO.CAL_QT                                AS CAL_QT                     " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_N01                          AS COL_FREE_N01               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_N01                         AS INIT_FREE_N01              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_N02                          AS COL_FREE_N02               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_N02                         AS INIT_FREE_N02              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_N03                          AS COL_FREE_N03               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_N03                         AS INIT_FREE_N03              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_N04                          AS COL_FREE_N04               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_N04                         AS INIT_FREE_N04              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_N05                          AS COL_FREE_N05               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_N05                         AS INIT_FREE_N05              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_N06                          AS COL_FREE_N06               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_N06                         AS INIT_FREE_N06              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_N07                          AS COL_FREE_N07               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_N07                         AS INIT_FREE_N07              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_N08                          AS COL_FREE_N08               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_N08                         AS INIT_FREE_N08              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_N09                          AS COL_FREE_N09               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_N09                         AS INIT_FREE_N09              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_N10                          AS COL_FREE_N10               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_N10                         AS INIT_FREE_N10              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_C01                          AS COL_FREE_C01               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_C01                         AS INIT_FREE_C01              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_C02                          AS COL_FREE_C02               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_C02                         AS INIT_FREE_C02              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_C03                          AS COL_FREE_C03               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_C03                         AS INIT_FREE_C03              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_C04                          AS COL_FREE_C04               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_C04                         AS INIT_FREE_C04              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_C05                          AS COL_FREE_C05               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_C05                         AS INIT_FREE_C05              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_C06                          AS COL_FREE_C06               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_C06                         AS INIT_FREE_C06              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_C07                          AS COL_FREE_C07               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_C07                         AS INIT_FREE_C07              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_C08                          AS COL_FREE_C08               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_C08                         AS INIT_FREE_C08              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_C09                          AS COL_FREE_C09               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_C09                         AS INIT_FREE_C09              " & vbNewLine _
                                              & ",ZAISHO.COL_FREE_C10                          AS COL_FREE_C10               " & vbNewLine _
                                              & ",ZAISHO.INIT_FREE_C10                         AS INIT_FREE_C10              " & vbNewLine _
                                              & ",ZAISHO.ID_CLASS_1                            AS ID_CLASS_1                 " & vbNewLine _
                                              & ",ZAISHO.NM_CLASS_1                            AS NM_CLASS_1                 " & vbNewLine _
                                              & ",ZAISHO.ID_CLASS_2                            AS ID_CLASS_2                 " & vbNewLine _
                                              & ",ZAISHO.NM_CLASS_2                            AS NM_CLASS_2                 " & vbNewLine _
                                              & ",ZAISHO.ID_CLASS_3                            AS ID_CLASS_3                 " & vbNewLine _
                                              & ",ZAISHO.NM_CLASS_3                            AS NM_CLASS_3                 " & vbNewLine _
                                              & ",ZAISHO.ID_CLASS_4                            AS ID_CLASS_4                 " & vbNewLine _
                                              & ",ZAISHO.NM_CLASS_4                            AS NM_CLASS_4                 " & vbNewLine _
                                              & ",ZAISHO.ID_CLASS_5                            AS ID_CLASS_5                 " & vbNewLine _
                                              & ",ZAISHO.NM_CLASS_5                            AS NM_CLASS_5                 " & vbNewLine _
                                              & ",ZAISHO.DEF_LOT_NO                            AS DEF_LOT_NO                 " & vbNewLine _
                                              & ",ZAISHO.DEF_SERIAL_NO                         AS DEF_SERIAL_NO              " & vbNewLine _
                                              & ",ZAISHO.DEF_IRIME                             AS DEF_IRIME                  " & vbNewLine _
                                              & ",ZAISHO.DEF_IRIME_UT                          AS DEF_IRIME_UT               " & vbNewLine _
                                              & ",ZAISHO.DEF_EQ                                AS DEF_EQ                     " & vbNewLine

#End Region

#Region "荷主在庫数データ取込制御マスタの検索 SQL FROM句"

    ''' <summary>
    ''' 荷主在庫数データ取込制御マスタの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_ZAISHO As String = "FROM                                                                    " & vbNewLine _
                                                   & "$LM_TRN$..D_ZAI_SHOGOH ZAISHO                                           " & vbNewLine

#End Region

#Region "荷主在庫数データ取込制御マスタの検索 SQL ORDER BY句"

    ''' <summary>
    ''' 荷主在庫数データ取込制御マスタの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_ZAISHO As String = "ORDER BY                                                               " & vbNewLine _
                                                    & " ZAISHO.NRS_BR_CD                                                      " & vbNewLine _
                                                    & ",ZAISHO.CUST_CD_L                                                      " & vbNewLine _
                                                    & ",ZAISHO.CUST_CD_M                                                      " & vbNewLine _
                                                    & ",ZAISHO.EDA_NO                                                         " & vbNewLine

#End Region

#End Region

#Region "荷主在庫数データの削除"

    ''' <summary>
    ''' 荷主在庫数データの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_ZAISHOCUST As String = "DELETE FROM $LM_TRN$..D_ZAI_SHOGOH_CUST " & vbNewLine

#End Region

#If True Then   'ADD 2018/08/31 依頼番号 : 001723   【LMS】在庫照合画面_タイムアウトエラーのため実用できず対応

#Region "ワークD_WK_TORIKOMIZUMI_CHKデータの削除"

    ''' <summary>
    ''' D_WK_TORIKOMIZUMI_CHKデータの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_D_WK_TORIKOMIZUMI_CHK As String = "DELETE FROM $LM_TRN$..D_WK_TORIKOMIZUMI_CHK " & vbNewLine

#End Region

#End If

#Region "荷主在庫数データの新規作成"

    ''' <summary>
    ''' 荷主在庫数データ新規作成 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_ZAISHOCUST As String = "INSERT INTO $LM_TRN$..D_ZAI_SHOGOH_CUST            " & vbNewLine _
                                                  & " ( 		                                        " & vbNewLine _
                                                  & " NRS_BR_CD,                                        " & vbNewLine _
                                                  & " CUST_CD_L,                                        " & vbNewLine _
                                                  & " CUST_CD_M,                                        " & vbNewLine _
                                                  & " CHECK_DATE,                                       " & vbNewLine _
                                                  & " GYO_NO,                                           " & vbNewLine _
                                                  & " EDA_NO,                                           " & vbNewLine _
                                                  & " FILE_NAME,                                        " & vbNewLine _
                                                  & " FILE_FOLDER,                                      " & vbNewLine _
                                                  & " WH_CD,                                            " & vbNewLine _
                                                  & " CUST_GOODS_CD,                                    " & vbNewLine _
                                                  & " GOODS_NM,                                         " & vbNewLine _
                                                  & " LOT_NO,                                           " & vbNewLine _
                                                  & " SERIAL_NO,                                        " & vbNewLine _
                                                  & " IRIME,                                            " & vbNewLine _
                                                  & " IRIME_UT,                                         " & vbNewLine _
                                                  & " CLASS_1,                                          " & vbNewLine _
                                                  & " CLASS_2,                                          " & vbNewLine _
                                                  & " CLASS_3,                                          " & vbNewLine _
                                                  & " CLASS_4,                                          " & vbNewLine _
                                                  & " CLASS_5,                                          " & vbNewLine _
                                                  & " NB,                                               " & vbNewLine _
                                                  & " QT,                                               " & vbNewLine _
                                                  & " FREE_N01,                                         " & vbNewLine _
                                                  & " FREE_N02,                                         " & vbNewLine _
                                                  & " FREE_N03,                                         " & vbNewLine _
                                                  & " FREE_N04,                                         " & vbNewLine _
                                                  & " FREE_N05,                                         " & vbNewLine _
                                                  & " FREE_N06,                                         " & vbNewLine _
                                                  & " FREE_N07,                                         " & vbNewLine _
                                                  & " FREE_N08,                                         " & vbNewLine _
                                                  & " FREE_N09,                                         " & vbNewLine _
                                                  & " FREE_N10,                                         " & vbNewLine _
                                                  & " FREE_C01,                                         " & vbNewLine _
                                                  & " FREE_C02,                                         " & vbNewLine _
                                                  & " FREE_C03,                                         " & vbNewLine _
                                                  & " FREE_C04,                                         " & vbNewLine _
                                                  & " FREE_C05,                                         " & vbNewLine _
                                                  & " FREE_C06,                                         " & vbNewLine _
                                                  & " FREE_C07,                                         " & vbNewLine _
                                                  & " FREE_C08,                                         " & vbNewLine _
                                                  & " FREE_C09,                                         " & vbNewLine _
                                                  & " FREE_C10,                                         " & vbNewLine _
                                                  & " SYS_ENT_DATE,                                     " & vbNewLine _
                                                  & " SYS_ENT_TIME,                                     " & vbNewLine _
                                                  & " SYS_ENT_PGID,                                     " & vbNewLine _
                                                  & " SYS_ENT_USER,                                     " & vbNewLine _
                                                  & " SYS_UPD_DATE,                                     " & vbNewLine _
                                                  & " SYS_UPD_TIME,                                     " & vbNewLine _
                                                  & " SYS_UPD_PGID,                                     " & vbNewLine _
                                                  & " SYS_UPD_USER,                                     " & vbNewLine _
                                                  & " SYS_DEL_FLG                                       " & vbNewLine _
                                                  & " ) VALUES (                                        " & vbNewLine _
                                                  & " @NRS_BR_CD,                                       " & vbNewLine _
                                                  & " @CUST_CD_L,                                       " & vbNewLine _
                                                  & " @CUST_CD_M,                                       " & vbNewLine _
                                                  & " @CHECK_DATE,                                      " & vbNewLine _
                                                  & " @GYO_NO,                                          " & vbNewLine _
                                                  & " @EDA_NO,                                          " & vbNewLine _
                                                  & " @FILE_NAME,                                       " & vbNewLine _
                                                  & " @FILE_FOLDER,                                     " & vbNewLine _
                                                  & " @WH_CD,                                           " & vbNewLine _
                                                  & " @GOODS_CD_CUST,                                   " & vbNewLine _
                                                  & " @GOODS_NM,                                        " & vbNewLine _
                                                  & " @LOT_NO,                                          " & vbNewLine _
                                                  & " @SERIAL_NO,                                       " & vbNewLine _
                                                  & " @IRIME,                                           " & vbNewLine _
                                                  & " @IRIME_UT,                                        " & vbNewLine _
                                                  & " @CLASS_1,                                         " & vbNewLine _
                                                  & " @CLASS_2,                                         " & vbNewLine _
                                                  & " @CLASS_3,                                         " & vbNewLine _
                                                  & " @CLASS_4,                                         " & vbNewLine _
                                                  & " @CLASS_5,                                         " & vbNewLine _
                                                  & " @NB,                                              " & vbNewLine _
                                                  & " @QT,                                              " & vbNewLine _
                                                  & " @FREE_N01,                                        " & vbNewLine _
                                                  & " @FREE_N02,                                        " & vbNewLine _
                                                  & " @FREE_N03,                                        " & vbNewLine _
                                                  & " @FREE_N04,                                        " & vbNewLine _
                                                  & " @FREE_N05,                                        " & vbNewLine _
                                                  & " @FREE_N06,                                        " & vbNewLine _
                                                  & " @FREE_N07,                                        " & vbNewLine _
                                                  & " @FREE_N08,                                        " & vbNewLine _
                                                  & " @FREE_N09,                                        " & vbNewLine _
                                                  & " @FREE_N10,                                        " & vbNewLine _
                                                  & " @FREE_C01,                                        " & vbNewLine _
                                                  & " @FREE_C02,                                        " & vbNewLine _
                                                  & " @FREE_C03,                                        " & vbNewLine _
                                                  & " @FREE_C04,                                        " & vbNewLine _
                                                  & " @FREE_C05,                                        " & vbNewLine _
                                                  & " @FREE_C06,                                        " & vbNewLine _
                                                  & " @FREE_C07,                                        " & vbNewLine _
                                                  & " @FREE_C08,                                        " & vbNewLine _
                                                  & " @FREE_C09,                                        " & vbNewLine _
                                                  & " @FREE_C10,                                        " & vbNewLine _
                                                  & " @SYS_ENT_DATE,                                    " & vbNewLine _
                                                  & " @SYS_ENT_TIME,                                    " & vbNewLine _
                                                  & " @SYS_ENT_PGID,                                    " & vbNewLine _
                                                  & " @SYS_ENT_USER,                                    " & vbNewLine _
                                                  & " @SYS_UPD_DATE,                                    " & vbNewLine _
                                                  & " @SYS_UPD_TIME,                                    " & vbNewLine _
                                                  & " @SYS_UPD_PGID,                                    " & vbNewLine _
                                                  & " @SYS_UPD_USER,                                    " & vbNewLine _
                                                  & " @SYS_DEL_FLG                                      " & vbNewLine _
                                                  & " )                                                 " & vbNewLine

#End Region

#Region "荷主在庫数データサマリの削除"

    ''' <summary>
    ''' 荷主在庫数データサマリの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_ZAISHOCUSTSUM As String = "DELETE FROM $LM_TRN$..D_ZAI_SHOGOH_CUST_SUM " & vbNewLine

#End Region

#Region "荷主在庫数データサマリの新規作成"

    ''' <summary>
    ''' 荷主在庫数データサマリ新規作成 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_ZAISHOCUSTSUM As String = "INSERT INTO $LM_TRN$..D_ZAI_SHOGOH_CUST_SUM        " & vbNewLine _
                                                     & " ( 		                                           " & vbNewLine _
                                                     & " NRS_BR_CD,                                        " & vbNewLine _
                                                     & " CUST_CD_L,                                        " & vbNewLine _
                                                     & " CUST_CD_M,                                        " & vbNewLine _
                                                     & " CHECK_DATE,                                       " & vbNewLine _
                                                     & " GYO_NO,                                           " & vbNewLine _
                                                     & " EDA_NO,                                           " & vbNewLine _
                                                     & " WH_CD,                                            " & vbNewLine _
                                                     & " CUST_GOODS_CD,                                    " & vbNewLine _
                                                     & " GOODS_NM,                                         " & vbNewLine _
                                                     & " LOT_NO,                                           " & vbNewLine _
                                                     & " SERIAL_NO,                                        " & vbNewLine _
                                                     & " IRIME,                                            " & vbNewLine _
                                                     & " IRIME_UT,                                         " & vbNewLine _
                                                     & " NB,                                               " & vbNewLine _
                                                     & " QT,                                               " & vbNewLine _
                                                     & " SYS_ENT_DATE,                                     " & vbNewLine _
                                                     & " SYS_ENT_TIME,                                     " & vbNewLine _
                                                     & " SYS_ENT_PGID,                                     " & vbNewLine _
                                                     & " SYS_ENT_USER,                                     " & vbNewLine _
                                                     & " SYS_UPD_DATE,                                     " & vbNewLine _
                                                     & " SYS_UPD_TIME,                                     " & vbNewLine _
                                                     & " SYS_UPD_PGID,                                     " & vbNewLine _
                                                     & " SYS_UPD_USER,                                     " & vbNewLine _
                                                     & " SYS_DEL_FLG                                       " & vbNewLine _
                                                     & " ) VALUES (                                        " & vbNewLine _
                                                     & " @NRS_BR_CD,                                       " & vbNewLine _
                                                     & " @CUST_CD_L,                                       " & vbNewLine _
                                                     & " @CUST_CD_M,                                       " & vbNewLine _
                                                     & " @CHECK_DATE,                                      " & vbNewLine _
                                                     & " @GYO_NO,                                          " & vbNewLine _
                                                     & " @EDA_NO,                                          " & vbNewLine _
                                                     & " @WH_CD,                                           " & vbNewLine _
                                                     & " @GOODS_CD_CUST,                                   " & vbNewLine _
                                                     & " @GOODS_NM,                                        " & vbNewLine _
                                                     & " @LOT_NO,                                          " & vbNewLine _
                                                     & " @SERIAL_NO,                                       " & vbNewLine _
                                                     & " @IRIME,                                           " & vbNewLine _
                                                     & " @IRIME_UT,                                        " & vbNewLine _
                                                     & " @NB,                                              " & vbNewLine _
                                                     & " @QT,                                              " & vbNewLine _
                                                     & " @SYS_ENT_DATE,                                    " & vbNewLine _
                                                     & " @SYS_ENT_TIME,                                    " & vbNewLine _
                                                     & " @SYS_ENT_PGID,                                    " & vbNewLine _
                                                     & " @SYS_ENT_USER,                                    " & vbNewLine _
                                                     & " @SYS_UPD_DATE,                                    " & vbNewLine _
                                                     & " @SYS_UPD_TIME,                                    " & vbNewLine _
                                                     & " @SYS_UPD_PGID,                                    " & vbNewLine _
                                                     & " @SYS_UPD_USER,                                    " & vbNewLine _
                                                     & " @SYS_DEL_FLG                                      " & vbNewLine _
                                                     & " )                                                 " & vbNewLine

#End Region

#If True Then   'ADD 2018/08/31 依頼番号 : 001723   【LMS】在庫照合画面_タイムアウトエラーのため実用できず対応


#Region "D_WK_TORIKOMIZUMI_CHKデータの01新規作成"

    ''' <summary>
    ''' D_WK_TORIKOMIZUMI_CHK01データ新規作成 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_D_WK_TORIKOMIZUMI_CHK01 As String = "   INSERT $LM_TRN$..D_WK_TORIKOMIZUMI_CHK                                      " & vbNewLine _
                                                        & "    (  SYORI_PTN                                                                      " & vbNewLine _
                                                        & "      ,OUTKA_NO_L                                                                     " & vbNewLine _
                                                        & "      ,CUST_CD_L                                                                      " & vbNewLine _
                                                        & "      ,CUST_CD_M                                                                      " & vbNewLine _
                                                        & "      ,WH_CD                                                                          " & vbNewLine _
                                                        & "      ,ZAI_REC_NO                                                                     " & vbNewLine _
                                                        & "      ,LOT_NO                                                                         " & vbNewLine _
                                                        & "      ,SERIAL_NO                                                                      " & vbNewLine _
                                                        & "      ,PORA_ZAI_NB                                                                    " & vbNewLine _
                                                        & "      ,PORA_ZAI_QT                                                                    " & vbNewLine _
                                                        & "      ,NRS_BR_CD                                                                      " & vbNewLine _
                                                        & "    )                                                                                 " & vbNewLine _
                                                        & "      SELECT '01'                                                  AS SYORI_PTN       " & vbNewLine _
                                                        & "            ,''                                                    AS OUTKA_NO_L      " & vbNewLine _
                                                        & "            , INL1.CUST_CD_L                                       AS CUST_CD_L       " & vbNewLine _
                                                        & "            , INL1.CUST_CD_M                                       AS CUST_CD_M       " & vbNewLine _
                                                        & "            , INL1.WH_CD                                           AS WH_CD           " & vbNewLine _
                                                        & "            , INS1.ZAI_REC_NO                                      AS ZAI_REC_NO      " & vbNewLine _
                                                        & "            , ISNULL(INS1.LOT_NO, '')                              AS LOT_NO          " & vbNewLine _
                                                        & "            , ISNULL(INS1.SERIAL_NO, '')                           AS SERIAL_NO       " & vbNewLine _
                                                        & "            , (INS1.KONSU * MG1.PKG_NB) + INS1.HASU                AS PORA_ZAI_NB     " & vbNewLine _
                                                        & "            , ((INS1.KONSU * MG1.PKG_NB) + INS1.HASU) * INS1.IRIME AS PORA_ZAI_QT     " & vbNewLine _
                                                        & "            , INS1.NRS_BR_CD                                       AS NRS_BR_CD       " & vbNewLine _
                                                        & "      FROM  $LM_TRN$..B_INKA_L INL1                                                   " & vbNewLine _
                                                        & "      LEFT JOIN $LM_TRN$..B_INKA_M INM1                                               " & vbNewLine _
                                                        & "        ON INM1.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                                        & "       AND INM1.NRS_BR_CD = INL1.NRS_BR_CD                                            " & vbNewLine _
                                                        & "       AND INM1.INKA_NO_L = INL1.INKA_NO_L                                            " & vbNewLine _
                                                        & "      LEFT JOIN $LM_TRN$..B_INKA_S INS1                                               " & vbNewLine _
                                                        & "        ON INS1.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                                        & "       AND INS1.NRS_BR_CD = INM1.NRS_BR_CD                                            " & vbNewLine _
                                                        & "       AND INS1.INKA_NO_L = INM1.INKA_NO_L                                            " & vbNewLine _
                                                        & "       AND INS1.INKA_NO_M = INM1.INKA_NO_M                                            " & vbNewLine _
                                                        & "      LEFT JOIN $LM_MST$..M_GOODS MG1                                                 " & vbNewLine _
                                                        & "        ON  MG1.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                                        & "       AND MG1.NRS_BR_CD = INL1.NRS_BR_CD                                             " & vbNewLine _
                                                        & "       AND MG1.GOODS_CD_NRS = INM1.GOODS_CD_NRS                                       " & vbNewLine _
                                                        & "      WHERE INL1.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                                        & "        AND INL1.NRS_BR_CD   = @NRS_BR_CD                                             " & vbNewLine _
                                                        & "        AND (INL1.INKA_STATE_KB > '10' OR RTRIM(INS1.ZAI_REC_NO) <> '')               " & vbNewLine _
                                                        & "        AND INL1.CUST_CD_L   = @CUST_CD_L                                             " & vbNewLine _
                                                        & "        AND INL1.CUST_CD_M   = @CUST_CD_M                                             " & vbNewLine _
                                                        & "        AND INL1.INKA_DATE  <= @JISSHI_DATE                                           " & vbNewLine _
                                                        & "        AND (INL1.INKA_DATE > '00000000' OR INL1.INKA_STATE_KB < '50')                " & vbNewLine _
                                                        & "        AND INS1.ZAI_REC_NO IS NOT NULL                                               " & vbNewLine _
                                                        & "        AND MG1.PKG_NB IS NOT NULL                                                    " & vbNewLine


    ''' <summary>
    ''' D_WK_TORIKOMIZUMI_CHK02データ新規作成 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_D_WK_TORIKOMIZUMI_CHK02 As String = " INSERT $LM_TRN$..D_WK_TORIKOMIZUMI_CHK                     " & vbNewLine _
                                                        & "  (  SYORI_PTN                                                     " & vbNewLine _
                                                        & "    ,OUTKA_NO_L                                                    " & vbNewLine _
                                                        & "    ,CUST_CD_L                                                     " & vbNewLine _
                                                        & "    ,CUST_CD_M                                                     " & vbNewLine _
                                                        & "    ,WH_CD                                                         " & vbNewLine _
                                                        & "    ,ZAI_REC_NO                                                    " & vbNewLine _
                                                        & "    ,LOT_NO                                                        " & vbNewLine _
                                                        & "    ,SERIAL_NO                                                     " & vbNewLine _
                                                        & "    ,PORA_ZAI_NB                                                   " & vbNewLine _
                                                        & "    ,PORA_ZAI_QT                                                   " & vbNewLine _
                                                        & "    ,NRS_BR_CD                                                     " & vbNewLine _
                                                        & "  )                                                                " & vbNewLine _
                                                        & " SELECT '02'                                 AS SYORI_PTN          " & vbNewLine _
                                                        & "      , ''                                   AS OUTKA_NO_L         " & vbNewLine _
                                                        & "      , ZAI4.CUST_CD_L                       AS CUST_CD_L          " & vbNewLine _
                                                        & "      , ZAI4.CUST_CD_M                       AS CUST_CD_M          " & vbNewLine _
                                                        & "      , ZAI4.WH_CD                           AS WH_CD              " & vbNewLine _
                                                        & "      , IDO1.N_ZAI_REC_NO                    AS ZAI_REC_NO         " & vbNewLine _
                                                        & "      , ISNULL(ZAI4.LOT_NO, '')              AS LOT_NO             " & vbNewLine _
                                                        & "      , ISNULL(ZAI4.SERIAL_NO, '')           AS SERIAL_NO          " & vbNewLine _
                                                        & "      , IDO1.N_PORA_ZAI_NB                   AS PORA_ZAI_NB        " & vbNewLine _
                                                        & "      , IDO1.N_PORA_ZAI_NB * IDO1.ZAIK_IRIME AS PORA_ZAI_QT        " & vbNewLine _
                                                        & "      , IDO1.NRS_BR_CD                       AS NRS_BR_CD          " & vbNewLine _
                                                        & " FROM $LM_TRN$..D_IDO_TRS IDO1                                     " & vbNewLine _
                                                        & " LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI4                                " & vbNewLine _
                                                        & "   ON  ZAI4.SYS_DEL_FLG = '0'                                      " & vbNewLine _
                                                        & "  AND ZAI4.NRS_BR_CD = IDO1.NRS_BR_CD                              " & vbNewLine _
                                                        & "  AND ZAI4.ZAI_REC_NO = IDO1.N_ZAI_REC_NO                          " & vbNewLine _
                                                        & " WHERE IDO1.SYS_DEL_FLG = '0'                                      " & vbNewLine _
                                                        & "   AND IDO1.NRS_BR_CD   = @NRS_BR_CD                               " & vbNewLine _
                                                        & "   AND ZAI4.CUST_CD_L   = @CUST_CD_L                               " & vbNewLine _
                                                        & "   AND ZAI4.CUST_CD_M   = @CUST_CD_M                               " & vbNewLine _
                                                        & "   AND IDO1.IDO_DATE   <= @JISSHI_DATE                             " & vbNewLine 


    ''' <summary>
    ''' D_WK_TORIKOMIZUMI_CHK03データ新規作成 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_D_WK_TORIKOMIZUMI_CHK03 As String = " INSERT $LM_TRN$..D_WK_TORIKOMIZUMI_CHK                               " & vbNewLine _
                                                        & "  (  SYORI_PTN                                                               " & vbNewLine _
                                                        & "    ,OUTKA_NO_L                                                              " & vbNewLine _
                                                        & "    ,CUST_CD_L                                                               " & vbNewLine _
                                                        & "    ,CUST_CD_M                                                               " & vbNewLine _
                                                        & "    ,WH_CD                                                                   " & vbNewLine _
                                                        & "    ,ZAI_REC_NO                                                              " & vbNewLine _
                                                        & "    ,LOT_NO                                                                  " & vbNewLine _
                                                        & "    ,SERIAL_NO                                                               " & vbNewLine _
                                                        & "    ,PORA_ZAI_NB                                                             " & vbNewLine _
                                                        & "    ,PORA_ZAI_QT                                                             " & vbNewLine _
                                                        & "    ,NRS_BR_CD                                                               " & vbNewLine _
                                                        & "  )                                                                          " & vbNewLine _
                                                        & " SELECT '03'                                    AS SYORI_PTN                 " & vbNewLine _
                                                        & "       , ''                                     AS OUTKA_NO_L                " & vbNewLine _
                                                        & "       , ZAI5.CUST_CD_L                         AS CUST_CD_L                 " & vbNewLine _
                                                        & "       , ZAI5.CUST_CD_M                         AS CUST_CD_M                 " & vbNewLine _
                                                        & "       , ZAI5.WH_CD                             AS WH_CD                     " & vbNewLine _
                                                        & "       , IDO2.O_ZAI_REC_NO                      AS ZAI_REC_NO                " & vbNewLine _
                                                        & "       , ISNULL(ZAI5.LOT_NO, '')                AS LOT_NO                    " & vbNewLine _
                                                        & "       , ISNULL(ZAI5.SERIAL_NO, '')             AS SERIAL_NO                 " & vbNewLine _
                                                        & "       , IDO2.N_PORA_ZAI_NB * -1                AS PORA_ZAI_NB               " & vbNewLine _
                                                        & "       , IDO2.N_PORA_ZAI_NB * IDO2.ZAIK_IRIME * -1 AS PORA_ZAI_QT            " & vbNewLine _
                                                        & "       , IDO2.NRS_BR_CD                         AS NRS_BR_CD                 " & vbNewLine _
                                                        & "  FROM $LM_TRN$..D_IDO_TRS IDO2                                              " & vbNewLine _
                                                        & "  LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI5                                         " & vbNewLine _
                                                        & "    ON ZAI5.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                        & "   AND ZAI5.NRS_BR_CD   = IDO2.NRS_BR_CD                                     " & vbNewLine _
                                                        & "   AND ZAI5.ZAI_REC_NO  = IDO2.O_ZAI_REC_NO                                  " & vbNewLine _
                                                        & " WHERE IDO2.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                        & "   AND IDO2.NRS_BR_CD   = @NRS_BR_CD                                         " & vbNewLine _
                                                        & "   AND ZAI5.CUST_CD_L   = @CUST_CD_L                                         " & vbNewLine _
                                                        & "   AND ZAI5.CUST_CD_M   = @CUST_CD_M                                         " & vbNewLine _
                                                        & "   AND IDO2.IDO_DATE   <= @JISSHI_DATE                                       " & vbNewLine

#End Region

#End If

#Region "Field"

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

#Region "SQLメイン処理"

#Region "荷主在庫数データの検索"

    ''' <summary>
    ''' 荷主在庫数データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectZaiShoCust(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD080IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_SELECT_ZAISHOCUST)       'SQL構築(Select句)
        Me._StrSql.Append(LMD080DAC.SQL_SELECT_FROM_ZAISHOCUST)  'SQL構築(From句)
        Call SetSQLWhereZAISHOCUST(inTbl.Rows(0))                '条件設定
        Me._StrSql.Append(LMD080DAC.SQL_SELECT_ORDER_ZAISHOCUST) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD080DAC", "SelectZaiShoCust", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CHECK_DATE", "CHECK_DATE")
        map.Add("GYO_NO", "GYO_NO")
        map.Add("EDA_NO", "EDA_NO")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("FILE_FOLDER", "FILE_FOLDER")
        map.Add("WH_CD", "WH_CD")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("CLASS_1", "CLASS_1")
        map.Add("CLASS_2", "CLASS_2")
        map.Add("CLASS_3", "CLASS_3")
        map.Add("CLASS_4", "CLASS_4")
        map.Add("CLASS_5", "CLASS_5")
        map.Add("NB", "NB")
        map.Add("QT", "QT")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("FREE_N06", "FREE_N06")
        map.Add("FREE_N07", "FREE_N07")
        map.Add("FREE_N08", "FREE_N08")
        map.Add("FREE_N09", "FREE_N09")
        map.Add("FREE_N10", "FREE_N10")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")
        map.Add("FREE_C06", "FREE_C06")
        map.Add("FREE_C07", "FREE_C07")
        map.Add("FREE_C08", "FREE_C08")
        map.Add("FREE_C09", "FREE_C09")
        map.Add("FREE_C10", "FREE_C10")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD080IN_ZAISHOGOHCUST")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "月末在庫データの検索"

    ''' <summary>
    ''' 月末在庫データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectZaiZan(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD080IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_SELECT_ZAIZAN)       'SQL構築
        Call SetSQLWhereZAIZAN(inTbl.Rows(0))                '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)


        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD080DAC", "SelectZaiZan", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CHECK_DATE", "CHECK_DATE")
        map.Add("GYO_NO", "GYO_NO")
        map.Add("EDA_NO", "EDA_NO")
        map.Add("WH_CD", "WH_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("NB", "NB")
        map.Add("QT", "QT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD080INOUT_ZAIZAN")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "荷主在庫数データサマリの検索"

    ''' <summary>
    ''' 荷主在庫数データサマリの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectZaiShoCustSum(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD080IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_SELECT_ZAISHOCUSTSUM)       'SQL構築(Select句)
        Me._StrSql.Append(LMD080DAC.SQL_SELECT_FROM_ZAISHOCUSTSUM)  'SQL構築(From句)
        Call SetSQLWhereZAISHOCUSTSUM(inTbl.Rows(0))                '条件設定
        Me._StrSql.Append(LMD080DAC.SQL_SELECT_ORDER_ZAISHOCUSTSUM) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD080DAC", "SelectZaiShoCustSum", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CHECK_DATE", "CHECK_DATE")
        map.Add("GYO_NO", "GYO_NO")
        map.Add("EDA_NO", "EDA_NO")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("FILE_FOLDER", "FILE_FOLDER")
        map.Add("WH_CD", "WH_CD")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("CLASS_1", "CLASS_1")
        map.Add("CLASS_2", "CLASS_2")
        map.Add("CLASS_3", "CLASS_3")
        map.Add("CLASS_4", "CLASS_4")
        map.Add("CLASS_5", "CLASS_5")
        map.Add("NB", "NB")
        map.Add("QT", "QT")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("FREE_N06", "FREE_N06")
        map.Add("FREE_N07", "FREE_N07")
        map.Add("FREE_N08", "FREE_N08")
        map.Add("FREE_N09", "FREE_N09")
        map.Add("FREE_N10", "FREE_N10")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")
        map.Add("FREE_C06", "FREE_C06")
        map.Add("FREE_C07", "FREE_C07")
        map.Add("FREE_C08", "FREE_C08")
        map.Add("FREE_C09", "FREE_C09")
        map.Add("FREE_C10", "FREE_C10")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD080IN_ZAISHOGOHCUSTSUM")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "荷主在庫数データ取込制御マスタの検索"

    ''' <summary>
    ''' 荷主在庫数データ取込制御マスタの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ShogohMstData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD080IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_SELECT_ZAISHO)       'SQL構築(Select句)
        Me._StrSql.Append(LMD080DAC.SQL_SELECT_FROM_ZAISHO)  'SQL構築(From句)
        Call SetSQLWhereZAISHO(inTbl.Rows(0))                '条件設定
        Me._StrSql.Append(LMD080DAC.SQL_SELECT_ORDER_ZAISHO) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD080DAC", "ShogohMstData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("EDA_NO", "EDA_NO")
        map.Add("SHOGOH_NAME", "SHOGOH_NAME")
        map.Add("WH_CD", "WH_CD")
        map.Add("HEADER_LINE", "HEADER_LINE")
        map.Add("RB_KB", "RB_KB")
        map.Add("COL_RB_1", "COL_RB_1")
        map.Add("DATA_RB_1", "DATA_RB_1")
        map.Add("COL_RB_2", "COL_RB_2")
        map.Add("DATA_RB_2", "DATA_RB_2")
        map.Add("COL_RB_3", "COL_RB_3")
        map.Add("DATA_RB_3", "DATA_RB_3")
        map.Add("COL_SKIP_1", "COL_SKIP_1")
        map.Add("DATA_SKIP_1", "DATA_SKIP_1")
        map.Add("COL_SKIP_2", "COL_SKIP_2")
        map.Add("DATA_SKIP_2", "DATA_SKIP_2")
        map.Add("COL_SKIP_3", "COL_SKIP_3")
        map.Add("DATA_SKIP_3", "DATA_SKIP_3")
        map.Add("COL_SKIP_4", "COL_SKIP_4")
        map.Add("DATA_SKIP_4", "DATA_SKIP_4")
        map.Add("COL_SKIP_5", "COL_SKIP_5")
        map.Add("DATA_SKIP_5", "DATA_SKIP_5")
        map.Add("COL_CUST_GOODS_CD_1", "COL_CUST_GOODS_CD_1")
        map.Add("COL_CUST_GOODS_CD_2", "COL_CUST_GOODS_CD_2")
        map.Add("COL_CUST_GOODS_CD_3", "COL_CUST_GOODS_CD_3")
        map.Add("INIT_CUST_GOODS_CD", "INIT_CUST_GOODS_CD")
        map.Add("COL_GOODS_NM_1", "COL_GOODS_NM_1")
        map.Add("COL_GOODS_NM_2", "COL_GOODS_NM_2")
        map.Add("COL_GOODS_NM_3", "COL_GOODS_NM_3")
        map.Add("INIT_GOODS_NM", "INIT_GOODS_NM")
        map.Add("COL_LOT_NO", "COL_LOT_NO")
        map.Add("INIT_LOT_NO", "INIT_LOT_NO")
        map.Add("COL_SERIAL_NO", "COL_SERIAL_NO")
        map.Add("INIT_SERIAL_NO", "INIT_SERIAL_NO")
        map.Add("COL_IRIME", "COL_IRIME")
        map.Add("INIT_IRIME", "INIT_IRIME")
        map.Add("CAL_IRIME", "CAL_IRIME")
        map.Add("COL_IRIME_UT", "COL_IRIME_UT")
        map.Add("INIT_IRIME_UT", "INIT_IRIME_UT")
        map.Add("COL_CLASS_1", "COL_CLASS_1")
        map.Add("INIT_CLASS_1", "INIT_CLASS_1")
        map.Add("COL_CLASS_2", "COL_CLASS_2")
        map.Add("INIT_CLASS_2", "INIT_CLASS_2")
        map.Add("COL_CLASS_3", "COL_CLASS_3")
        map.Add("INIT_CLASS_3", "INIT_CLASS_3")
        map.Add("COL_CLASS_4", "COL_CLASS_4")
        map.Add("INIT_CLASS_4", "INIT_CLASS_4")
        map.Add("COL_CLASS_5", "COL_CLASS_5")
        map.Add("INIT_CLASS_5", "INIT_CLASS_5")
        map.Add("CHK_NB", "CHK_NB")
        map.Add("COL_NB", "COL_NB")
        map.Add("INIT_NB", "INIT_NB")
        map.Add("CAL_NB", "CAL_NB")
        map.Add("CHK_QT", "CHK_QT")
        map.Add("COL_QT", "COL_QT")
        map.Add("INIT_QT", "INIT_QT")
        map.Add("CAL_QT", "CAL_QT")
        map.Add("COL_FREE_N01", "COL_FREE_N01")
        map.Add("INIT_FREE_N01", "INIT_FREE_N01")
        map.Add("COL_FREE_N02", "COL_FREE_N02")
        map.Add("INIT_FREE_N02", "INIT_FREE_N02")
        map.Add("COL_FREE_N03", "COL_FREE_N03")
        map.Add("INIT_FREE_N03", "INIT_FREE_N03")
        map.Add("COL_FREE_N04", "COL_FREE_N04")
        map.Add("INIT_FREE_N04", "INIT_FREE_N04")
        map.Add("COL_FREE_N05", "COL_FREE_N05")
        map.Add("INIT_FREE_N05", "INIT_FREE_N05")
        map.Add("COL_FREE_N06", "COL_FREE_N06")
        map.Add("INIT_FREE_N06", "INIT_FREE_N06")
        map.Add("COL_FREE_N07", "COL_FREE_N07")
        map.Add("INIT_FREE_N07", "INIT_FREE_N07")
        map.Add("COL_FREE_N08", "COL_FREE_N08")
        map.Add("INIT_FREE_N08", "INIT_FREE_N08")
        map.Add("COL_FREE_N09", "COL_FREE_N09")
        map.Add("INIT_FREE_N09", "INIT_FREE_N09")
        map.Add("COL_FREE_N10", "COL_FREE_N10")
        map.Add("INIT_FREE_N10", "INIT_FREE_N10")
        map.Add("COL_FREE_C01", "COL_FREE_C01")
        map.Add("INIT_FREE_C01", "INIT_FREE_C01")
        map.Add("COL_FREE_C02", "COL_FREE_C02")
        map.Add("INIT_FREE_C02", "INIT_FREE_C02")
        map.Add("COL_FREE_C03", "COL_FREE_C03")
        map.Add("INIT_FREE_C03", "INIT_FREE_C03")
        map.Add("COL_FREE_C04", "COL_FREE_C04")
        map.Add("INIT_FREE_C04", "INIT_FREE_C04")
        map.Add("COL_FREE_C05", "COL_FREE_C05")
        map.Add("INIT_FREE_C05", "INIT_FREE_C05")
        map.Add("COL_FREE_C06", "COL_FREE_C06")
        map.Add("INIT_FREE_C06", "INIT_FREE_C06")
        map.Add("COL_FREE_C07", "COL_FREE_C07")
        map.Add("INIT_FREE_C07", "INIT_FREE_C07")
        map.Add("COL_FREE_C08", "COL_FREE_C08")
        map.Add("INIT_FREE_C08", "INIT_FREE_C08")
        map.Add("COL_FREE_C09", "COL_FREE_C09")
        map.Add("INIT_FREE_C09", "INIT_FREE_C09")
        map.Add("COL_FREE_C10", "COL_FREE_C10")
        map.Add("INIT_FREE_C10", "INIT_FREE_C10")
        map.Add("ID_CLASS_1", "ID_CLASS_1")
        map.Add("NM_CLASS_1", "NM_CLASS_1")
        map.Add("ID_CLASS_2", "ID_CLASS_2")
        map.Add("NM_CLASS_2", "NM_CLASS_2")
        map.Add("ID_CLASS_3", "ID_CLASS_3")
        map.Add("NM_CLASS_3", "NM_CLASS_3")
        map.Add("ID_CLASS_4", "ID_CLASS_4")
        map.Add("NM_CLASS_4", "NM_CLASS_4")
        map.Add("ID_CLASS_5", "ID_CLASS_5")
        map.Add("NM_CLASS_5", "NM_CLASS_5")
        map.Add("DEF_LOT_NO", "DEF_LOT_NO")
        map.Add("DEF_SERIAL_NO", "DEF_SERIAL_NO")
        map.Add("DEF_IRIME", "DEF_IRIME")
        map.Add("DEF_IRIME_UT", "DEF_IRIME_UT")
        map.Add("DEF_EQ", "DEF_EQ")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD080OUT_ZAISHOGOH")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "荷主在庫数データの物理削除"

    ''' <summary>
    ''' 荷主在庫数データの物理削除①(照合日指定)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteZaiShoCust1(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD080IN_ZAISHOGOHCUST")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_DELETE_ZAISHOCUST)      'SQL構築(Delete句)
        Call SetSQLWhereZAISHOCUSTDEL1(inTbl.Rows(0))           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD080DAC", "DeleteZaiShoCust1", cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 荷主在庫数データの物理削除②(照合日の１か月以上前)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteZaiShoCust2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD080IN_ZAISHOGOHCUST")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_DELETE_ZAISHOCUST)      'SQL構築(Delete句)
        Call SetSQLWhereZAISHOCUSTDEL2(inTbl.Rows(0))           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD080DAC", "DeleteZaiShoCust2", cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#If True Then   'ADD 2018/08/31 依頼番号 : 001723   【LMS】在庫照合画面_タイムアウトエラーのため実用できず対応

#Region "ワークD_WK_TORIKOMIZUMI_CHKデータの物理削除"

    ''' <summary>
    ''' ワークファイル削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteD_WK_TORIKOMIZUMI_CHK(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得(現行のまま使用)
        Dim inTbl As DataTable = ds.Tables("LMD080IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_DELETE_D_WK_TORIKOMIZUMI_CHK)       'SQL構築(Delete句)
        Call SetSQLWhereD_D_WK_TORIKOMIZUMI_CHKDEL(inTbl.Rows(0))           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD080DAC", "DeleteD_WK_TORIKOMIZUMI_CHK", cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#Region "ワークデータの新規追加"

    ''' <summary>
    ''' D_WK_TORIKOMIZUMI_CHKデータの01新規追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertD_WK_TORIKOMIZUMI_CHK01(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得(現行のまま使用)
        Dim inTbl As DataTable = ds.Tables("LMD080IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_INSERT_D_WK_TORIKOMIZUMI_CHK01)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'Dim max As Integer = inTbl.Rows.Count - 1
        'For i As Integer = 0 To max

        'パラメータの初期化
        cmd.Parameters.Clear()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ（条件設定項目）設定
        Call SetD_D_WK_TORIKOMIZUMI_CHK_InsParameter(inTbl.Rows(0), Me._SqlPrmList)

        'SQLパラメータ（個別項目）設定
        'Call Me.SetZaiShoCustParSetD_D_WK_TORIKOMIZUMI_CHK_InsParamet(inTbl.Rows(i))

        'SQLパラメータ（システム項目）設定
        ''Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD080DAC", "InsertD_WK_TORIKOMIZUMI_CHK01", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        'Next

        Return ds

    End Function

    ''' <summary>
    ''' D_WK_TORIKOMIZUMI_CHKデータの02新規追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertD_WK_TORIKOMIZUMI_CHK02(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得(現行のまま使用)
        Dim inTbl As DataTable = ds.Tables("LMD080IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_INSERT_D_WK_TORIKOMIZUMI_CHK02)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'Dim max As Integer = inTbl.Rows.Count - 1
        'For i As Integer = 0 To max

        'パラメータの初期化
        cmd.Parameters.Clear()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ（条件設定項目）設定
        Call SetD_D_WK_TORIKOMIZUMI_CHK_InsParameter(inTbl.Rows(0), Me._SqlPrmList)

        'SQLパラメータ（個別項目）設定
        'Call Me.SetZaiShoCustParSetD_D_WK_TORIKOMIZUMI_CHK_InsParamet(inTbl.Rows(i))

        'SQLパラメータ（システム項目）設定
        ''Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD080DAC", "InsertD_WK_TORIKOMIZUMI_CHK02", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        'Next

        Return ds

    End Function


    ''' <summary>
    ''' D_WK_TORIKOMIZUMI_CHKデータの03新規追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertD_WK_TORIKOMIZUMI_CHK03(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得(現行のまま使用)
        Dim inTbl As DataTable = ds.Tables("LMD080IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_INSERT_D_WK_TORIKOMIZUMI_CHK03)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'Dim max As Integer = inTbl.Rows.Count - 1
        'For i As Integer = 0 To max

        'パラメータの初期化
        cmd.Parameters.Clear()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ（条件設定項目）設定
        Call SetD_D_WK_TORIKOMIZUMI_CHK_InsParameter(inTbl.Rows(0), Me._SqlPrmList)

        'SQLパラメータ（個別項目）設定
        'Call Me.SetZaiShoCustParSetD_D_WK_TORIKOMIZUMI_CHK_InsParamet(inTbl.Rows(i))

        'SQLパラメータ（システム項目）設定
        ''Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD080DAC", "InsertD_WK_TORIKOMIZUMI_CHK03", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds
    End Function

#End Region

#End Region

#End If

#Region "荷主在庫数データの新規追加"

    ''' <summary>
    ''' 荷主在庫数データの新規追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertZaiShoCust(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD080IN_ZAISHOGOHCUST")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_INSERT_ZAISHOCUST)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（個別項目）設定
            Call Me.SetZaiShoCustParameter(inTbl.Rows(i), Me._SqlPrmList)

            'SQLパラメータ（システム項目）設定
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMD080DAC", "InsertZaiShoCust", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region

#Region "荷主在庫数データサマリの物理削除"

    ''' <summary>
    ''' 荷主在庫数データサマリの物理削除①(照合日指定)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteZaiShoCustSum1(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD080IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_DELETE_ZAISHOCUSTSUM)      'SQL構築(Delete句)
        Call SetSQLWhereZAISHOCUSTSUMDEL1(inTbl.Rows(0))           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD080DAC", "DeleteZaiShoCustSum1", cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 荷主在庫数データサマリの物理削除②(照合日の１か月以上前)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteZaiShoCustSum2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD080IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_DELETE_ZAISHOCUSTSUM)      'SQL構築(Delete句)
        Call SetSQLWhereZAISHOCUSTSUMDEL2(inTbl.Rows(0))           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD080DAC", "DeleteZaiShoCustSum2", cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "荷主在庫数データサマリの新規追加"

    ''' <summary>
    ''' 荷主在庫数データサマリの新規追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertZaiShoCustSum(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD080INOUT_ZAIZAN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMD080DAC.SQL_INSERT_ZAISHOCUSTSUM)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（個別項目）設定
            Call Me.SetZaiShoCustSumParameter(inTbl.Rows(i), Me._SqlPrmList)

            'SQLパラメータ（システム項目）設定
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMD080DAC", "InsertZaiShoCustSum", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 荷主在庫数データの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereZAISHOCUST(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("ZAISHOCUST.SYS_DEL_FLG = '0'                                   ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAISHOCUST.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAISHOCUST.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAISHOCUST.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '照合日
            whereStr = .Item("JISSHI_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAISHOCUST.CHECK_DATE = @JISSHI_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSHI_DATE", whereStr, DBDataType.CHAR))
            End If

            '枝番号
            whereStr = .Item("EDA_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAISHOCUST.EDA_NO = @EDA_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDA_NO", whereStr, DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 月末在庫データの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereZAIZAN(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            '営業所
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '荷主コード(大)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))

            '荷主コード(中)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))

            '作成日
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSHI_DATE", .Item("JISSHI_DATE").ToString(), DBDataType.CHAR))

            '枝番号
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDA_NO", .Item("EDA_NO").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 荷主在庫数データサマリの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereZAISHOCUSTSUM(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("ZAISHOCUSTSUM.SYS_DEL_FLG = '0'                                ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAISHOCUSTSUM.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAISHOCUSTSUM.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAISHOCUSTSUM.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '照合日
            whereStr = .Item("JISSHI_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAISHOCUSTSUM.CHECK_DATE = @JISSHI_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSHI_DATE", whereStr, DBDataType.CHAR))
            End If

            '枝番号
            whereStr = .Item("EDA_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAISHOCUSTSUM.EDA_NO = @EDA_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDA_NO", whereStr, DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 荷主在庫数データ取込制御マスタの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereZAISHO(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("ZAISHO.SYS_DEL_FLG = '0'                                       ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND                                                            ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("(                                                              ")
            Me._StrSql.Append(vbNewLine)

            '■ここから下は標準の荷主(標準の荷主="ZZZZZ"のデータ)
            Me._StrSql.Append(" (ZAISHO.CUST_CD_L = 'ZZZZZ'")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND ZAISHO.CUST_CD_M = 'ZZ'")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAISHO.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            Me._StrSql.Append(")")
            Me._StrSql.Append(vbNewLine)

            '■ここから下は画面入力項目での条件
            If String.IsNullOrEmpty(.Item("CUST_CD_L").ToString()) = True Then
                '初期表示時はここで終了
                Me._StrSql.Append(")")
                Me._StrSql.Append(vbNewLine)
                Exit Sub
            End If

            Me._StrSql.Append(" OR ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" ZAISHO.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAISHO.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ZAISHO.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            Me._StrSql.Append(")")
            Me._StrSql.Append(vbNewLine)

        End With

    End Sub

#End Region

#Region "SQL条件設定 荷主在庫数データの削除"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereZAISHOCUSTDEL1(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("1 = 1                                                          ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '照合日
            whereStr = .Item("CHECK_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CHECK_DATE = @CHECK_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHECK_DATE", whereStr, DBDataType.CHAR))
            End If

            '枝番号
            whereStr = .Item("EDA_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EDA_NO = @EDA_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDA_NO", whereStr, DBDataType.NVARCHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereZAISHOCUSTDEL2(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("1 = 1                                                          ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '照合日
            whereStr = String.Concat(Mid(.Item("CHECK_DATE").ToString(), 1, 4), "/", Mid(.Item("CHECK_DATE").ToString(), 5, 2), "/", Mid(.Item("CHECK_DATE").ToString(), 7, 2))
            whereStr = Mid(Convert.ToString(Convert.ToDateTime(whereStr).AddMonths(-1)), 1, 10).Replace("/", String.Empty)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CHECK_DATE <= @CHECK_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHECK_DATE", whereStr, DBDataType.CHAR))
            End If

            '枝番号
            whereStr = .Item("EDA_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EDA_NO = @EDA_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDA_NO", whereStr, DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End Region

#If True Then   'ADD 2018/08/31 依頼番号 : 001723   【LMS】在庫照合画面_タイムアウトエラーのため実用できず

#Region "SQL条件設定 D_WK_TORIKOMIZUMI_CHKデータの削除"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereD_D_WK_TORIKOMIZUMI_CHKDEL(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("1 = 1                                                          ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#End If

#Region "SQL条件設定 D_WK_TORIKOMIZUMI_CHK新規データ"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetD_D_WK_TORIKOMIZUMI_CHK_InsParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSHI_DATE", .Item("JISSHI_DATE").ToString(), DBDataType.CHAR))

        End With
    End Sub

#End Region

#End Region

#Region "SQL条件設定 荷主在庫数データの新規追加"

    ''' <summary>
    ''' 荷主在庫数データの新規追加
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetZaiShoCustParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CHECK_DATE", .Item("CHECK_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO_NO", .Item("GYO_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDA_NO", .Item("EDA_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_FOLDER", .Item("FILE_FOLDER").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CLASS_1", .Item("CLASS_1").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CLASS_2", .Item("CLASS_2").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CLASS_3", .Item("CLASS_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CLASS_4", .Item("CLASS_4").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CLASS_5", .Item("CLASS_5").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NB", .Item("NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@QT", .Item("QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N01", .Item("FREE_N01").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N02", .Item("FREE_N02").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N03", .Item("FREE_N03").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N04", .Item("FREE_N04").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N05", .Item("FREE_N05").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N06", .Item("FREE_N06").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N07", .Item("FREE_N07").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N08", .Item("FREE_N08").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N09", .Item("FREE_N09").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N10", .Item("FREE_N10").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", .Item("FREE_C01").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", .Item("FREE_C02").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", .Item("FREE_C03").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C04", .Item("FREE_C04").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C05", .Item("FREE_C05").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C06", .Item("FREE_C06").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C07", .Item("FREE_C07").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C08", .Item("FREE_C08").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C09", .Item("FREE_C09").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C10", .Item("FREE_C10").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 荷主在庫数データサマリの削除"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereZAISHOCUSTSUMDEL1(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("1 = 1                                                          ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '照合日
            whereStr = .Item("JISSHI_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CHECK_DATE = @JISSHI_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSHI_DATE", whereStr, DBDataType.CHAR))
            End If

            '枝番号
            whereStr = .Item("EDA_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EDA_NO = @EDA_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDA_NO", whereStr, DBDataType.NVARCHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereZAISHOCUSTSUMDEL2(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("1 = 1                                                          ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '照合日
            whereStr = String.Concat(Mid(.Item("JISSHI_DATE").ToString(), 1, 4), "/", Mid(.Item("JISSHI_DATE").ToString(), 5, 2), "/", Mid(.Item("JISSHI_DATE").ToString(), 7, 2))
            whereStr = Mid(Convert.ToString(Convert.ToDateTime(whereStr).AddMonths(-1)), 1, 10).Replace("/", String.Empty)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CHECK_DATE <= @JISSHI_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSHI_DATE", whereStr, DBDataType.CHAR))
            End If

            '枝番号
            whereStr = .Item("EDA_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EDA_NO = @EDA_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDA_NO", whereStr, DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 荷主在庫数データサマリの新規追加"

    ''' <summary>
    ''' 荷主在庫数データサマリの新規追加
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetZaiShoCustSumParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CHECK_DATE", .Item("CHECK_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO_NO", .Item("GYO_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDA_NO", .Item("EDA_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NB", .Item("NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@QT", .Item("QT").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

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

#Region "ユーティリティ"

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

#End Region

#End Region

End Class
