' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫
'  プログラムID     :  LMD560    : 在庫整合性リスト印刷
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD560DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD560DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "SQL"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private SQL_MPrt As String = String.Empty

    ''' <summary>
    ''' 在庫数比較用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private SQL_ZAI_NB As String = String.Empty

    ''' <summary>
    ''' 出荷予定比較用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private SQL_ALCTD_NB As String = String.Empty

    ''' <summary>
    ''' 引当数比較用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private SQL_ALLOC_CAN_NB As String = String.Empty

    ''' <summary>
    ''' 帳票種別取得用SQL文作成
    ''' </summary>
    ''' <param name="custCdL"></param>
    ''' <param name="custCdM"></param>
    ''' <remarks></remarks>
    Private Sub CreateSqlMprt(ByVal custCdL As String, ByVal custCdM As String)

        SQL_MPrt = "SELECT DISTINCT                                                                 " & vbNewLine _
                 & " ZAI.NRS_BR_CD                                               AS NRS_BR_CD       " & vbNewLine _
                 & ",'29'                                                        AS PTN_ID          " & vbNewLine _
                 & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                               " & vbNewLine _
                 & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                               " & vbNewLine _
                 & "      ELSE MR3.PTN_CD END                                    AS PTN_CD          " & vbNewLine _
                 & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                               " & vbNewLine _
                 & "  	  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                               " & vbNewLine _
                 & "      ELSE MR3.RPT_ID END                                    AS RPT_ID          " & vbNewLine _
                 & "--在庫データよりパターン取得                                                    " & vbNewLine _
                 & "FROM                                                                            " & vbNewLine _
                 & "$LM_TRN$..D_ZAI_TRS ZAI                                                         " & vbNewLine _
                 & "--商品Mデータの取得                                                             " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_GOODS MG                                                  " & vbNewLine _
                 & "ON  MG.NRS_BR_CD = ZAI.NRS_BR_CD                                                " & vbNewLine _
                 & "AND MG.GOODS_CD_NRS = ZAI.GOODS_CD_NRS                                          " & vbNewLine _
                 & "--荷主帳票パターン取得                                                          " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                             " & vbNewLine _
                 & "ON  ZAI.NRS_BR_CD = MCR1.NRS_BR_CD                                              " & vbNewLine _
                 & "AND ZAI.CUST_CD_L = MCR1.CUST_CD_L                                              " & vbNewLine _
                 & "AND ZAI.CUST_CD_M = MCR1.CUST_CD_M                                              " & vbNewLine _
                 & "AND '00' = MCR1.CUST_CD_S                                                       " & vbNewLine _
                 & "AND MCR1.PTN_ID = '29'                                                          " & vbNewLine _
                 & "--帳票パターン取得                                                              " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_RPT MR1                                                   " & vbNewLine _
                 & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                              " & vbNewLine _
                 & "AND MR1.PTN_ID = MCR1.PTN_ID                                                    " & vbNewLine _
                 & "AND MR1.PTN_CD = MCR1.PTN_CD                                                    " & vbNewLine _
                 & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                 & "--商品Mの荷主での荷主帳票パターン取得                                           " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                             " & vbNewLine _
                 & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                               " & vbNewLine _
                 & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                               " & vbNewLine _
                 & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                               " & vbNewLine _
                 & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                               " & vbNewLine _
                 & "AND MCR2.PTN_ID = '29'                                                          " & vbNewLine _
                 & "--帳票パターン取得                                                              " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_RPT MR2                                                   " & vbNewLine _
                 & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                              " & vbNewLine _
                 & "AND MR2.PTN_ID = MCR2.PTN_ID                                                    " & vbNewLine _
                 & "AND MR2.PTN_CD = MCR2.PTN_CD                                                    " & vbNewLine _
                 & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                 & "--存在しない場合の帳票パターン取得                                              " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_RPT MR3                                                   " & vbNewLine _
                 & "ON  MR3.NRS_BR_CD = ZAI.NRS_BR_CD                                               " & vbNewLine _
                 & "AND MR3.PTN_ID = '29'                                                           " & vbNewLine _
                 & "AND MR3.STANDARD_FLAG = '01'                                                    " & vbNewLine _
                 & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                 & "WHERE                                                                           " & vbNewLine _
                 & "    ZAI.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
                 & "AND ZAI.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_MPrt = SQL_MPrt & vbNewLine _
                 & "AND ZAI.CUST_CD_L = @CUST_CD_L                                                  " & vbNewLine _
                 & "AND ZAI.CUST_CD_M = @CUST_CD_M                                                  " & vbNewLine
        End If

    End Sub

    ''' <summary>
    ''' 在庫数比較用SQL文作成
    ''' </summary>
    ''' <param name="custCdL"></param>
    ''' <param name="custCdM"></param>
    ''' <remarks></remarks>
    Private Sub CreateSqlZaiNb(ByVal custCdL As String, ByVal custCdM As String)

        SQL_ZAI_NB = "SELECT                                                                                 " & vbNewLine _
                   & "CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                       " & vbNewLine _
                   & " WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                           " & vbNewLine _
                   & " ELSE MR3.RPT_ID END          AS RPT_ID                                                " & vbNewLine _
                   & ",MAIN1.NM                     AS NM                                                    " & vbNewLine _
                   & ",MAIN1.NRS_BR_CD              AS NRS_BR_CD                                             " & vbNewLine _
                   & ",MAIN1.NRS_BR_NM              AS NRS_BR_NM                                             " & vbNewLine _
                   & ",MAIN1.PTN                    AS PTN                                                   " & vbNewLine _
                   & ",MAIN1.CUST_CD_L              AS CUST_CD_L                                             " & vbNewLine _
                   & ",MAIN1.CUST_CD_M              AS CUST_CD_M                                             " & vbNewLine _
                   & ",MAIN1.CUST_NM_L              AS CUST_NM_L                                             " & vbNewLine _
                   & ",MAIN1.ZAI_REC_NO             AS ZAI_REC_NO                                            " & vbNewLine _
                   & ",MAIN1.GOODS_CD_NRS           AS GOODS_CD_NRS                                          " & vbNewLine _
                   & ",MAIN1.GOODS_CD_CUST          AS GOODS_CD_CUST                                         " & vbNewLine _
                   & ",MAIN1.GOODS_NM               AS GOODS_NM                                              " & vbNewLine _
                   & ",MAIN1.LOT_NO                 AS LOT_NO                                                " & vbNewLine _
                   & ",MAIN1.SERIAL_NO              AS SERIAL_NO                                             " & vbNewLine _
                   & ",MAIN1.HIKAKU_ZAI_NB          AS HIKAKU_ZAI_NB                                         " & vbNewLine _
                   & ",MAIN1.PORA_ZAI_NB            AS PORA_ZAI_NB                                           " & vbNewLine _
                   & ",MAIN1.EXTRA_ZAI_NB           AS EXTRA_ZAI_NB                                          " & vbNewLine _
                   & ",MAIN1.HIKAKU_ZAI_QT          AS HIKAKU_ZAI_QT                                         " & vbNewLine _
                   & ",MAIN1.PORA_ZAI_QT            AS PORA_ZAI_QT                                           " & vbNewLine _
                   & ",MAIN1.EXTRA_ZAI_QT           AS EXTRA_ZAI_QT                                          " & vbNewLine _
                   & "FROM                                                                                   " & vbNewLine _
                   & "  (SELECT                                                                              " & vbNewLine _
                   & "     KBN1.KBN_NM1             AS NM                                                    " & vbNewLine _
                   & "    ,@NRS_BR_CD               AS NRS_BR_CD                                             " & vbNewLine _
                   & "    ,MB.NRS_BR_NM             AS NRS_BR_NM                                             " & vbNewLine _
                   & "    ,@PTN                     AS PTN                                                   " & vbNewLine _
                   & "    ,MAIN2.CUST_CD_L          AS CUST_CD_L                                             " & vbNewLine _
                   & "    ,MAIN2.CUST_CD_M          AS CUST_CD_M                                             " & vbNewLine _
                   & "    ,MC.CUST_NM_L             AS CUST_NM_L                                             " & vbNewLine _
                   & "    ,MAIN2.ZAI_REC_NO         AS ZAI_REC_NO                                            " & vbNewLine _
                   & "    ,MAIN2.GOODS_CD_NRS       AS GOODS_CD_NRS                                          " & vbNewLine _
                   & "    ,MG.GOODS_CD_CUST         AS GOODS_CD_CUST                                         " & vbNewLine _
                   & "    ,MG.GOODS_NM_1            AS GOODS_NM                                              " & vbNewLine _
                   & "    ,MAIN2.LOT_NO             AS LOT_NO                                                " & vbNewLine _
                   & "    ,MAIN2.SERIAL_NO          AS SERIAL_NO                                             " & vbNewLine _
                   & "    ,SUM(HIKAKU_ZAI_NB)       AS HIKAKU_ZAI_NB                                         " & vbNewLine _
                   & "    ,SUM(PORA_ZAI_NB)         AS PORA_ZAI_NB                                           " & vbNewLine _
                   & "    ,SUM(EXTRA_ZAI_NB)        AS EXTRA_ZAI_NB                                          " & vbNewLine _
                   & "    ,SUM(HIKAKU_ZAI_QT)       AS HIKAKU_ZAI_QT                                         " & vbNewLine _
                   & "    ,SUM(PORA_ZAI_QT)         AS PORA_ZAI_QT                                           " & vbNewLine _
                   & "    ,SUM(EXTRA_ZAI_QT)        AS EXTRA_ZAI_QT                                          " & vbNewLine _
                   & "    FROM                                                                               " & vbNewLine _
                   & "      (SELECT                                                                          " & vbNewLine _
                   & "        CUST_CD_L             AS CUST_CD_L                                             " & vbNewLine _
                   & "       ,CUST_CD_M             AS CUST_CD_M                                             " & vbNewLine _
                   & "       ,CUST_NM_L             AS CUST_NM_L                                             " & vbNewLine _
                   & "       ,ZAI_REC_NO            AS ZAI_REC_NO                                            " & vbNewLine _
                   & "       ,GOODS_CD_NRS          AS GOODS_CD_NRS                                          " & vbNewLine _
                   & "       ,LOT_NO                AS LOT_NO                                                " & vbNewLine _
                   & "       ,SERIAL_NO             AS SERIAL_NO                                             " & vbNewLine _
                   & "       ,SUM(PORA_ZAI_NB)      AS HIKAKU_ZAI_NB                                         " & vbNewLine _
                   & "       ,0                     AS PORA_ZAI_NB                                           " & vbNewLine _
                   & "       ,0                     AS EXTRA_ZAI_NB                                          " & vbNewLine _
                   & "       ,SUM(PORA_ZAI_QT)      AS HIKAKU_ZAI_QT                                         " & vbNewLine _
                   & "       ,0                     AS PORA_ZAI_QT                                           " & vbNewLine _
                   & "       ,0                     AS EXTRA_ZAI_QT                                          " & vbNewLine _
                   & "       FROM                                                                            " & vbNewLine _
                   & "       --在庫データ(D_ZAI_TRS)                                                         " & vbNewLine _
                   & "         (SELECT DISTINCT                                                              " & vbNewLine _
                   & "           CUST_CD_L          AS CUST_CD_L                                             " & vbNewLine _
                   & "          ,CUST_CD_M          AS CUST_CD_M                                             " & vbNewLine _
                   & "          ,CUST_NM_L          AS CUST_NM_L                                             " & vbNewLine _
                   & "          ,ZAI_REC_NO         AS ZAI_REC_NO                                            " & vbNewLine _
                   & "          ,GOODS_CD_NRS       AS GOODS_CD_NRS                                          " & vbNewLine _
                   & "          ,LOT_NO             AS LOT_NO                                                " & vbNewLine _
                   & "          ,SERIAL_NO          AS SERIAL_NO                                             " & vbNewLine _
                   & "          ,PORA_ZAI_NB        AS PORA_ZAI_NB                                           " & vbNewLine _
                   & "          ,ALCTD_NB           AS ALCTD_NB                                              " & vbNewLine _
                   & "          ,ALLOC_CAN_NB       AS ALLOC_CAN_NB                                          " & vbNewLine _
                   & "          ,PORA_ZAI_QT        AS PORA_ZAI_QT                                           " & vbNewLine _
                   & "          ,ALCTD_QT           AS ALCTD_QT                                              " & vbNewLine _
                   & "          ,ALLOC_CAN_QT       AS ALLOC_CAN_QT                                          " & vbNewLine _
                   & "          FROM                                                                         " & vbNewLine _
                   & "            (SELECT                                                                    " & vbNewLine _
                   & "              ZAI1.CUST_CD_L        AS CUST_CD_L                                       " & vbNewLine _
                   & "             ,ZAI1.CUST_CD_M        AS CUST_CD_M                                       " & vbNewLine _
                   & "             ,''                    AS CUST_NM_L                                       " & vbNewLine _
                   & "             ,ZAI1.ZAI_REC_NO       AS ZAI_REC_NO                                      " & vbNewLine _
                   & "             ,ZAI1.GOODS_CD_NRS     AS GOODS_CD_NRS                                    " & vbNewLine _
                   & "             ,ZAI1.LOT_NO           AS LOT_NO                                          " & vbNewLine _
                   & "             ,ZAI1.SERIAL_NO        AS SERIAL_NO                                       " & vbNewLine _
                   & "             ,ZAI1.PORA_ZAI_NB      AS PORA_ZAI_NB                                     " & vbNewLine _
                   & "             ,ZAI1.ALCTD_NB         AS ALCTD_NB                                        " & vbNewLine _
                   & "             ,ZAI1.ALLOC_CAN_NB     AS ALLOC_CAN_NB                                    " & vbNewLine _
                   & "             ,ZAI1.PORA_ZAI_QT      AS PORA_ZAI_QT                                     " & vbNewLine _
                   & "             ,ZAI1.ALCTD_QT         AS ALCTD_QT                                        " & vbNewLine _
                   & "             ,ZAI1.ALLOC_CAN_QT     AS ALLOC_CAN_QT                                    " & vbNewLine _
                   & "             FROM                                                                      " & vbNewLine _
                   & "             $LM_TRN$..D_ZAI_TRS ZAI1                                                  " & vbNewLine _
                   & "             WHERE                                                                     " & vbNewLine _
                   & "                 ZAI1.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                   & "             AND ZAI1.NRS_BR_CD = @NRS_BR_CD                                           " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "             AND ZAI1.CUST_CD_L = @CUST_CD_L                                           " & vbNewLine _
                   & "             AND ZAI1.CUST_CD_M = @CUST_CD_M                                           " & vbNewLine
        End If
        SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "             AND (ZAI1.PORA_ZAI_NB <> 0 OR ZAI1.PORA_ZAI_QT <> 0)                      " & vbNewLine _
                   & "            ) BASE1                                                                    " & vbNewLine _
                   & "         ) BASE2                                                                       " & vbNewLine _
                   & "       GROUP BY                                                                        " & vbNewLine _
                   & "        CUST_CD_L                                                                      " & vbNewLine _
                   & "       ,CUST_CD_M                                                                      " & vbNewLine _
                   & "       ,CUST_NM_L                                                                      " & vbNewLine _
                   & "       ,ZAI_REC_NO                                                                     " & vbNewLine _
                   & "       ,GOODS_CD_NRS                                                                   " & vbNewLine _
                   & "       ,LOT_NO                                                                         " & vbNewLine _
                   & "       ,SERIAL_NO                                                                      " & vbNewLine _
                   & "       --入出荷の履歴                                                                  " & vbNewLine _
                   & "       UNION ALL                                                                       " & vbNewLine _
                   & "       SELECT                                                                          " & vbNewLine _
                   & "        CUST_CD_L                AS CUST_CD_L                                          " & vbNewLine _
                   & "       ,CUST_CD_M                AS CUST_CD_M                                          " & vbNewLine _
                   & "       ,CUST_NM_L                AS CUST_NM_L                                          " & vbNewLine _
                   & "       ,ZAI_REC_NO               AS ZAI_REC_NO                                         " & vbNewLine _
                   & "       ,GOODS_CD_NRS             AS GODOS_CD_NRS                                       " & vbNewLine _
                   & "       ,LOT_NO                   AS LOT_NO                                             " & vbNewLine _
                   & "       ,SERIAL_NO                AS SERIAL_NO                                          " & vbNewLine _
                   & "       ,0                        AS HIKAKU_ZAI_NB                                      " & vbNewLine _
                   & "       ,SUM(PORA_ZAI_NB)         AS PORA_ZAI_NB                                        " & vbNewLine _
                   & "       ,0                        AS EXTRA_ZAI_NB                                       " & vbNewLine _
                   & "       ,0                        AS HIKAKU_ZAI_QT                                      " & vbNewLine _
                   & "       ,SUM(PORA_ZAI_QT)         AS PORA_ZAI_QT                                        " & vbNewLine _
                   & "       ,0                        AS EXTRA_ZAI_QT                                       " & vbNewLine _
                   & "       --月末在庫履歴(D_ZAI_ZAN_JITSU)                                                 " & vbNewLine _
                   & "       FROM                                                                            " & vbNewLine _
                   & "         (SELECT                                                                       " & vbNewLine _
                   & "           ''                                AS OUTKA_NO_L                             " & vbNewLine _
                   & "          ,''                                AS OUTKA_NO_M                             " & vbNewLine _
                   & "          ,''                                AS OUTKA_NO_S                             " & vbNewLine _
                   & "          ,ZAI2.CUST_CD_L                    AS CUST_CD_L                              " & vbNewLine _
                   & "          ,ZAI2.CUST_CD_M                    AS CUST_CD_M                              " & vbNewLine _
                   & "          ,''                                AS CUST_NM_L                              " & vbNewLine _
                   & "          ,ZAN.ZAI_REC_NO                    AS ZAI_REC_NO                             " & vbNewLine _
                   & "          ,ZAI2.GOODS_CD_NRS                 AS GOODS_CD_NRS                           " & vbNewLine _
                   & "          ,ISNULL(ZAI2.LOT_NO, '')           AS LOT_NO                                 " & vbNewLine _
                   & "          ,ISNULL(ZAI2.SERIAL_NO, '')        AS SERIAL_NO                              " & vbNewLine _
                   & "          ,ZAN.PORA_ZAI_NB                   AS PORA_ZAI_NB                            " & vbNewLine _
                   & "          ,ZAN.PORA_ZAI_QT                   AS PORA_ZAI_QT                            " & vbNewLine _
                   & "          FROM                                                                         " & vbNewLine _
                   & "          $LM_TRN$..D_ZAI_ZAN_JITSU ZAN                                                " & vbNewLine _
                   & "          LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI2                                           " & vbNewLine _
                   & "          ON  ZAI2.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                   & "          AND ZAI2.NRS_BR_CD = ZAN.NRS_BR_CD                                           " & vbNewLine _
                   & "          AND ZAI2.ZAI_REC_NO = ZAN.ZAI_REC_NO                                         " & vbNewLine _
                   & "          LEFT JOIN $LM_TRN$..B_INKA_L INL2                                            " & vbNewLine _
                   & "          ON  INL2.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                   & "          AND INL2.NRS_BR_CD = ZAI2.NRS_BR_CD                                          " & vbNewLine _
                   & "          AND INL2.INKA_NO_L = ZAI2.INKA_NO_L                                          " & vbNewLine _
                   & "          WHERE                                                                        " & vbNewLine _
                   & "              ZAN.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                   & "          AND ZAN.NRS_BR_CD = @NRS_BR_CD                                               " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "          AND ZAI2.CUST_CD_L = @CUST_CD_L                                              " & vbNewLine _
                   & "          AND ZAI2.CUST_CD_M = @CUST_CD_M                                              " & vbNewLine
        End If
        SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "          AND ZAN.RIREKI_DATE = @RIREKI_DATE                                           " & vbNewLine _
                   & "          AND INL2.INKA_STATE_KB !< '50'                                               " & vbNewLine _
                   & "          AND (ZAN.PORA_ZAI_NB <> 0 OR ZAN.PORA_ZAI_QT <> 0)                           " & vbNewLine _
                   & "          --入荷データ(B_INKA_S)                                                       " & vbNewLine _
                   & "          UNION ALL                                                                    " & vbNewLine _
                   & "          SELECT                                                                       " & vbNewLine _
                   & "           ''                                                        AS OUTKA_NO_L     " & vbNewLine _
                   & "          ,''                                                        AS OUTKA_NO_M     " & vbNewLine _
                   & "          ,''                                                        AS OUTKA_NO_S     " & vbNewLine _
                   & "          ,INL1.CUST_CD_L                                            AS CUST_CD_L      " & vbNewLine _
                   & "          ,INL1.CUST_CD_M                                            AS CUST_CD_M      " & vbNewLine _
                   & "          ,''                                                        AS CUST_NM_L      " & vbNewLine _
                   & "          ,INS1.ZAI_REC_NO                                           AS ZAI_REC_NO     " & vbNewLine _
                   & "          ,INM1.GOODS_CD_NRS                                         AS GOODS_CD_NRS   " & vbNewLine _
                   & "          ,ISNULL(INS1.LOT_NO, '')                                   AS LOT_NO         " & vbNewLine _
                   & "          ,ISNULL(INS1.SERIAL_NO, '')                                AS SERIAL_NO      " & vbNewLine _
                   & "          ,(INS1.KONSU * MG1.PKG_NB) + INS1.HASU                     AS PORA_ZAI_NB    " & vbNewLine _
                   & "          ,((INS1.KONSU * MG1.PKG_NB) + INS1.HASU) * INS1.IRIME      AS PORA_ZAI_QT    " & vbNewLine _
                   & "          FROM                                                                         " & vbNewLine _
                   & "          $LM_TRN$..B_INKA_L INL1                                                      " & vbNewLine _
                   & "          LEFT JOIN $LM_TRN$..B_INKA_M INM1                                            " & vbNewLine _
                   & "          ON  INM1.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                   & "          AND INM1.NRS_BR_CD = INL1.NRS_BR_CD                                          " & vbNewLine _
                   & "          AND INM1.INKA_NO_L = INL1.INKA_NO_L                                          " & vbNewLine _
                   & "          LEFT JOIN $LM_TRN$..B_INKA_S INS1                                            " & vbNewLine _
                   & "          ON  INS1.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                   & "          AND INS1.NRS_BR_CD = INL1.NRS_BR_CD                                          " & vbNewLine _
                   & "          AND INS1.INKA_NO_L = INL1.INKA_NO_L                                          " & vbNewLine _
                   & "          AND INS1.INKA_NO_M = INM1.INKA_NO_M                                          " & vbNewLine _
                   & "          LEFT JOIN $LM_MST$..M_GOODS MG1                                              " & vbNewLine _
                   & "          ON  MG1.NRS_BR_CD = INL1.NRS_BR_CD                                           " & vbNewLine _
                   & "          AND MG1.GOODS_CD_NRS = INM1.GOODS_CD_NRS                                     " & vbNewLine _
                   & "          WHERE                                                                        " & vbNewLine _
                   & "              INL1.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                   & "          AND INL1.NRS_BR_CD = @NRS_BR_CD                                              " & vbNewLine _
                   & "          AND (INL1.INKA_STATE_KB > '10' OR RTRIM(INS1.ZAI_REC_NO) <> '')              " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "          AND INL1.CUST_CD_L = @CUST_CD_L                                              " & vbNewLine _
                   & "          AND INL1.CUST_CD_M = @CUST_CD_M                                              " & vbNewLine
        End If
        SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "          AND (INL1.INKA_DATE > @RIREKI_DATE OR INL1.INKA_STATE_KB < '50')             " & vbNewLine _
                   & "          --在庫移動分を加減算(D_IDO_TRS)                                              " & vbNewLine _
                   & "          --移動後                                                                     " & vbNewLine _
                   & "          UNION ALL                                                                    " & vbNewLine _
                   & "          SELECT                                                                       " & vbNewLine _
                   & "           ''                                          AS OUTKA_NO_L                   " & vbNewLine _
                   & "          ,''                                          AS OUTKA_NO_M                   " & vbNewLine _
                   & "          ,''                                          AS OUTKA_NO_S                   " & vbNewLine _
                   & "          ,ZAI3.CUST_CD_L                              AS CUST_CD_L                    " & vbNewLine _
                   & "          ,ZAI3.CUST_CD_M                              AS CUST_CD_M                    " & vbNewLine _
                   & "          ,''                                          AS CUST_NM_L                    " & vbNewLine _
                   & "          ,IDO1.N_ZAI_REC_NO                           AS ZAI_REC_NO                   " & vbNewLine _
                   & "          ,ZAI3.GOODS_CD_NRS                           AS GOODS_CD_NRS                 " & vbNewLine _
                   & "          ,ISNULL(ZAI3.LOT_NO, '')                     AS LOT_NO                       " & vbNewLine _
                   & "          ,ISNULL(ZAI4.SERIAL_NO, '')                  AS SERIAL_NO                    " & vbNewLine _
                   & "          ,IDO1.N_PORA_ZAI_NB                          AS PORA_ZAI_NB                  " & vbNewLine _
                   & "          ,IDO1.N_PORA_ZAI_NB * IDO1.ZAIK_IRIME           AS PORA_ZAI_QT                  " & vbNewLine _
                   & "          FROM                                                                         " & vbNewLine _
                   & "          $LM_TRN$..D_IDO_TRS IDO1                                                     " & vbNewLine _
                   & "          LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI3                                           " & vbNewLine _
                   & "          ON  ZAI3.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                   & "          AND ZAI3.NRS_BR_CD = IDO1.NRS_BR_CD                                          " & vbNewLine _
                   & "          AND ZAI3.ZAI_REC_NO = IDO1.O_ZAI_REC_NO                                      " & vbNewLine _
                   & "          LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI4                                           " & vbNewLine _
                   & "          ON  ZAI4.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                   & "          AND ZAI4.NRS_BR_CD = IDO1.NRS_BR_CD                                          " & vbNewLine _
                   & "          AND ZAI4.ZAI_REC_NO = IDO1.N_ZAI_REC_NO                                      " & vbNewLine _
                   & "          WHERE                                                                        " & vbNewLine _
                   & "              IDO1.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                   & "          AND IDO1.NRS_BR_CD = @NRS_BR_CD                                              " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "          AND ZAI3.CUST_CD_L = @CUST_CD_L                                              " & vbNewLine _
                   & "          AND ZAI3.CUST_CD_M = @CUST_CD_M                                              " & vbNewLine
        End If
        SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "          AND IDO1.IDO_DATE > @RIREKI_DATE                                             " & vbNewLine _
                   & "          --移動前                                                                     " & vbNewLine _
                   & "          UNION ALL                                                                    " & vbNewLine _
                   & "          SELECT                                                                       " & vbNewLine _
                   & "           ''                                          AS OUTKA_NO_L                   " & vbNewLine _
                   & "          ,''                                          AS OUTKA_NO_M                   " & vbNewLine _
                   & "          ,''                                          AS OUTKA_NO_S                   " & vbNewLine _
                   & "          ,ZAI5.CUST_CD_L                              AS CUST_CD_L                    " & vbNewLine _
                   & "          ,ZAI5.CUST_CD_M                              AS CUST_CD_M                    " & vbNewLine _
                   & "          ,''                                          AS CUST_NM_L                    " & vbNewLine _
                   & "          ,IDO2.O_ZAI_REC_NO                           AS ZAI_REC_NO                   " & vbNewLine _
                   & "          ,ZAI5.GOODS_CD_NRS                           AS GOODS_CD_NRS                 " & vbNewLine _
                   & "          ,ISNULL(ZAI5.LOT_NO, '')                     AS LOT_NO                       " & vbNewLine _
                   & "          ,ISNULL(ZAI5.SERIAL_NO, '')                  AS SERIAL_NO                    " & vbNewLine _
                   & "          ,IDO2.N_PORA_ZAI_NB * -1                     AS PORA_ZAI_NB                  " & vbNewLine _
                   & "          ,IDO2.N_PORA_ZAI_NB * IDO2.ZAIK_IRIME * -1      AS PORA_ZAI_QT                  " & vbNewLine _
                   & "          FROM                                                                         " & vbNewLine _
                   & "          $LM_TRN$..D_IDO_TRS IDO2                                                     " & vbNewLine _
                   & "          LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI5                                           " & vbNewLine _
                   & "          ON  ZAI5.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                   & "          AND ZAI5.NRS_BR_CD = IDO2.NRS_BR_CD                                          " & vbNewLine _
                   & "          AND ZAI5.ZAI_REC_NO = IDO2.O_ZAI_REC_NO                                      " & vbNewLine _
                   & "          WHERE                                                                        " & vbNewLine _
                   & "              IDO2.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                   & "          AND IDO2.NRS_BR_CD = @NRS_BR_CD                                              " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "          AND ZAI5.CUST_CD_L = @CUST_CD_L                                              " & vbNewLine _
                   & "          AND ZAI5.CUST_CD_M = @CUST_CD_M                                              " & vbNewLine
        End If
        SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "          AND IDO2.IDO_DATE > @RIREKI_DATE                                             " & vbNewLine _
                   & "          --出荷データ(C_OUTKA_S)                                                      " & vbNewLine _
                   & "          UNION ALL                                                                    " & vbNewLine _
                   & "          SELECT DISTINCT                                                              " & vbNewLine _
                   & "           OUTKA_NO_L                                                                  " & vbNewLine _
                   & "          ,OUTKA_NO_M                                                                  " & vbNewLine _
                   & "          ,OUTKA_NO_S                                                                  " & vbNewLine _
                   & "          ,CUST_CD_L                                                                   " & vbNewLine _
                   & "          ,CUST_CD_M                                                                   " & vbNewLine _
                   & "          ,CUST_NM_L                                                                   " & vbNewLine _
                   & "          ,ZAI_REC_NO                                                                  " & vbNewLine _
                   & "          ,GOODS_CD_NRS                                                                " & vbNewLine _
                   & "          ,LOT_NO                                                                      " & vbNewLine _
                   & "          ,SERIAL_NO                                                                   " & vbNewLine _
                   & "          ,PORA_ZAI_NB                                                                 " & vbNewLine _
                   & "          ,PORA_ZAI_QT                                                                 " & vbNewLine _
                   & "          FROM                                                                         " & vbNewLine _
                   & "            (SELECT                                                                    " & vbNewLine _
                   & "              OUTS.OUTKA_NO_L                   AS OUTKA_NO_L                          " & vbNewLine _
                   & "	           ,OUTS.OUTKA_NO_M                   AS OUTKA_NO_M                          " & vbNewLine _
                   & "	           ,OUTS.OUTKA_NO_S                   AS OUTKA_NO_S                          " & vbNewLine _
                   & "             ,OUTL.CUST_CD_L                    AS CUST_CD_L                           " & vbNewLine _
                   & "             ,OUTL.CUST_CD_M                    AS CUST_CD_M                           " & vbNewLine _
                   & "             ,''                                AS CUST_NM_L                           " & vbNewLine _
                   & "             ,OUTS.ZAI_REC_NO                   AS ZAI_REC_NO                          " & vbNewLine _
                   & "             ,OUTM.GOODS_CD_NRS                 AS GOODS_CD_NRS                        " & vbNewLine _
                   & "             ,ISNULL(OUTS.LOT_NO, '')           AS LOT_NO                              " & vbNewLine _
                   & "             ,ISNULL(OUTS.SERIAL_NO, '')        AS SERIAL_NO                           " & vbNewLine _
                   & "             ,OUTS.ALCTD_NB * -1                AS PORA_ZAI_NB                         " & vbNewLine _
                   & "             ,OUTS.ALCTD_QT * -1                AS PORA_ZAI_QT                         " & vbNewLine _
                   & "             FROM                                                                      " & vbNewLine _
                   & "             $LM_TRN$..C_OUTKA_L OUTL                                                  " & vbNewLine _
                   & "             LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                        " & vbNewLine _
                   & "             ON  OUTM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                   & "             AND OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                       " & vbNewLine _
                   & "             AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                     " & vbNewLine _
                   & "             LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                        " & vbNewLine _
                   & "             ON  OUTS.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                   & "             AND OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                       " & vbNewLine _
                   & "             AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                     " & vbNewLine _
                   & "             AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                     " & vbNewLine _
                   & "             WHERE                                                                     " & vbNewLine _
                   & "                 OUTL.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                   & "             AND OUTM.ALCTD_KB <> '04'                                                 " & vbNewLine _
                   & "             AND OUTL.OUTKA_STATE_KB !< '60'                                           " & vbNewLine _
                   & "             AND OUTL.NRS_BR_CD = @NRS_BR_CD                                           " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "          AND OUTL.CUST_CD_L = @CUST_CD_L                                              " & vbNewLine _
                   & "          AND OUTL.CUST_CD_M = @CUST_CD_M                                              " & vbNewLine
        End If
        SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "             AND OUTL.OUTKA_PLAN_DATE > @RIREKI_DATE                                   " & vbNewLine _
                   & "            ) BASE3                                                                    " & vbNewLine _
                   & "         ) BASE4                                                                       " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "         WHERE                                                                         " & vbNewLine _
                   & "             CUST_CD_L = @CUST_CD_L                                                    " & vbNewLine _
                   & "         AND CUST_CD_M = @CUST_CD_M                                                    " & vbNewLine
        End If
        SQL_ZAI_NB = SQL_ZAI_NB & vbNewLine _
                   & "         GROUP BY                                                                      " & vbNewLine _
                   & "          CUST_CD_L                                                                    " & vbNewLine _
                   & "         ,CUST_CD_M                                                                    " & vbNewLine _
                   & "         ,CUST_NM_L                                                                    " & vbNewLine _
                   & "         ,ZAI_REC_NO                                                                   " & vbNewLine _
                   & "         ,GOODS_CD_NRS                                                                 " & vbNewLine _
                   & "         ,LOT_NO                                                                       " & vbNewLine _
                   & "         ,SERIAL_NO                                                                    " & vbNewLine _
                   & "      ) MAIN2                                                                          " & vbNewLine _
                   & "      LEFT JOIN $LM_MST$..M_GOODS MG                                                   " & vbNewLine _
                   & "      ON  MG.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                   & "      AND MG.CUST_CD_L = MAIN2.CUST_CD_L                                               " & vbNewLine _
                   & "      AND MG.CUST_CD_M = MAIN2.CUST_CD_M                                               " & vbNewLine _
                   & "      AND MG.GOODS_CD_NRS = MAIN2.GOODS_CD_NRS                                         " & vbNewLine _
                   & "      LEFT JOIN $LM_MST$..M_CUST MC                                                    " & vbNewLine _
                   & "      ON  MC.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                   & "      AND MC.CUST_CD_L = MAIN2.CUST_CD_L                                               " & vbNewLine _
                   & "      AND MC.CUST_CD_M = MAIN2.CUST_CD_M                                               " & vbNewLine _
                   & "      AND MC.CUST_CD_S = '00'                                                          " & vbNewLine _
                   & "      AND MC.CUST_CD_SS = '00'                                                         " & vbNewLine _
                   & "      LEFT JOIN $LM_MST$..M_NRS_BR MB                                                  " & vbNewLine _
                   & "      ON  MB.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                   & "      LEFT JOIN $LM_MST$..Z_KBN KBN1                                                   " & vbNewLine _
                   & "      ON  KBN_GROUP_CD = 'P007'                                                        " & vbNewLine _
                   & "      AND KBN_CD = '00'                                                                " & vbNewLine _
                   & "      GROUP BY                                                                         " & vbNewLine _
                   & "       KBN1.KBN_NM1                                                                    " & vbNewLine _
                   & "      ,MB.NRS_BR_NM                                                                    " & vbNewLine _
                   & "      ,MAIN2.CUST_CD_L                                                                 " & vbNewLine _
                   & "      ,MAIN2.CUST_CD_M                                                                 " & vbNewLine _
                   & "      ,MC.CUST_NM_L                                                                    " & vbNewLine _
                   & "      ,MAIN2.ZAI_REC_NO                                                                " & vbNewLine _
                   & "      ,MAIN2.GOODS_CD_NRS                                                              " & vbNewLine _
                   & "      ,MG.GOODS_CD_CUST                                                                " & vbNewLine _
                   & "      ,MG.GOODS_NM_1                                                                   " & vbNewLine _
                   & "      ,MAIN2.LOT_NO                                                                    " & vbNewLine _
                   & "      ,MAIN2.SERIAL_NO                                                                 " & vbNewLine _
                   & "  ) MAIN1                                                                              " & vbNewLine _
                   & "--帳票パターン取得用商品Mデータ取得                                                    " & vbNewLine _
                   & "LEFT JOIN $LM_MST$..M_GOODS MGR                                                        " & vbNewLine _
                   & "ON  MGR.NRS_BR_CD = MAIN1.NRS_BR_CD                                                    " & vbNewLine _
                   & "AND MGR.GOODS_CD_NRS = MAIN1.GOODS_CD_NRS                                              " & vbNewLine _
                   & "--荷主帳票パターン取得                                                                 " & vbNewLine _
                   & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                    " & vbNewLine _
                   & "ON  MAIN1.NRS_BR_CD = MCR1.NRS_BR_CD                                                   " & vbNewLine _
                   & "AND MAIN1.CUST_CD_L = MCR1.CUST_CD_L                                                   " & vbNewLine _
                   & "AND MAIN1.CUST_CD_M = MCR1.CUST_CD_M                                                   " & vbNewLine _
                   & "AND '00' = MCR1.CUST_CD_S                                                              " & vbNewLine _
                   & "AND MCR1.PTN_ID = '29'                                                                 " & vbNewLine _
                   & "--帳票パターン取得                                                                     " & vbNewLine _
                   & "LEFT JOIN $LM_MST$..M_RPT MR1                                                          " & vbNewLine _
                   & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                     " & vbNewLine _
                   & "AND MR1.PTN_ID = MCR1.PTN_ID                                                           " & vbNewLine _
                   & "AND MR1.PTN_CD = MCR1.PTN_CD                                                           " & vbNewLine _
                   & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                   & "--商品Mの荷主での荷主帳票パターン取得                                                  " & vbNewLine _
                   & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                    " & vbNewLine _
                   & "ON  MGR.NRS_BR_CD = MCR2.NRS_BR_CD                                                     " & vbNewLine _
                   & "AND MGR.CUST_CD_L = MCR2.CUST_CD_L                                                     " & vbNewLine _
                   & "AND MGR.CUST_CD_M = MCR2.CUST_CD_M                                                     " & vbNewLine _
                   & "AND MGR.CUST_CD_S = MCR2.CUST_CD_S                                                     " & vbNewLine _
                   & "AND MCR2.PTN_ID = '29'                                                                 " & vbNewLine _
                   & "--帳票パターン取得                                                                     " & vbNewLine _
                   & "LEFT JOIN $LM_MST$..M_RPT MR2                                                          " & vbNewLine _
                   & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                     " & vbNewLine _
                   & "AND MR2.PTN_ID = MCR2.PTN_ID                                                           " & vbNewLine _
                   & "AND MR2.PTN_CD = MCR2.PTN_CD                                                           " & vbNewLine _
                   & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                   & "--存在しない場合の帳票パターン取得                                                     " & vbNewLine _
                   & "LEFT JOIN $LM_MST$..M_RPT MR3                                                          " & vbNewLine _
                   & "ON  MR3.NRS_BR_CD = MAIN1.NRS_BR_CD                                                    " & vbNewLine _
                   & "AND MR3.PTN_ID = '29'                                                                  " & vbNewLine _
                   & "AND MR3.STANDARD_FLAG = '01'                                                           " & vbNewLine _
                   & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                   & "WHERE                                                                                  " & vbNewLine _
                   & "(HIKAKU_ZAI_NB <> PORA_ZAI_NB OR HIKAKU_ZAI_QT <> PORA_ZAI_QT)                         " & vbNewLine _
                   & "ORDER BY                                                                               " & vbNewLine _
                   & " NRS_BR_CD                                                                             " & vbNewLine _
                   & ",CUST_CD_L                                                                             " & vbNewLine _
                   & ",CUST_CD_M                                                                             " & vbNewLine _
                   & ",GOODS_CD_NRS                                                                          " & vbNewLine _
                   & ",LOT_NO                                                                                " & vbNewLine _
                   & ",ZAI_REC_NO                                                                            " & vbNewLine _
                   & ",SERIAL_NO                                                                             " & vbNewLine

    End Sub

    ''' <summary>
    ''' 出荷予定比較用SQL作成
    ''' </summary>
    ''' <param name="custCdL"></param>
    ''' <param name="custCdM"></param>
    ''' <remarks></remarks>
    Private Sub CreateSqlAlctdNb(ByVal custCdL As String, ByVal custCdM As String)

        SQL_ALCTD_NB = "SELECT                                                                       " & vbNewLine _
                     & "CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                             " & vbNewLine _
                     & " WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                 " & vbNewLine _
                     & " ELSE MR3.RPT_ID END   AS RPT_ID                                             " & vbNewLine _
                     & ",MAIN1.NM                                                                    " & vbNewLine _
                     & ",MAIN1.NRS_BR_CD                                                             " & vbNewLine _
                     & ",MAIN1.NRS_BR_NM                                                             " & vbNewLine _
                     & ",MAIN1.PTN                                                                   " & vbNewLine _
                     & ",MAIN1.CUST_CD_L                                                             " & vbNewLine _
                     & ",MAIN1.CUST_CD_M                                                             " & vbNewLine _
                     & ",MAIN1.CUST_NM_L                                                             " & vbNewLine _
                     & ",MAIN1.ZAI_REC_NO                                                            " & vbNewLine _
                     & ",MAIN1.GOODS_CD_NRS                                                          " & vbNewLine _
                     & ",MAIN1.GOODS_CD_CUST                                                         " & vbNewLine _
                     & ",MAIN1.GOODS_NM                                                              " & vbNewLine _
                     & ",MAIN1.LOT_NO                                                                " & vbNewLine _
                     & ",MAIN1.SERIAL_NO                                                             " & vbNewLine _
                     & ",MAIN1.HIKAKU_ZAI_NB                                                         " & vbNewLine _
                     & ",MAIN1.PORA_ZAI_NB                                                           " & vbNewLine _
                     & ",MAIN1.EXTRA_ZAI_NB                                                          " & vbNewLine _
                     & ",MAIN1.HIKAKU_ZAI_QT                                                         " & vbNewLine _
                     & ",MAIN1.PORA_ZAI_QT                                                           " & vbNewLine _
                     & ",MAIN1.EXTRA_ZAI_QT                                                          " & vbNewLine _
                     & "FROM                                                                         " & vbNewLine _
                     & "(SELECT                                                                      " & vbNewLine _
                     & " KBN1.KBN_NM1                 AS NM                                          " & vbNewLine _
                     & ",@NRS_BR_CD                   AS NRS_BR_CD                                   " & vbNewLine _
                     & ",MB.NRS_BR_NM                 AS NRS_BR_NM                                   " & vbNewLine _
                     & ",@PTN                         AS PTN                                         " & vbNewLine _
                     & ",MAIN2.CUST_CD_L              AS CUST_CD_L                                   " & vbNewLine _
                     & ",MAIN2.CUST_CD_M              AS CUST_CD_M                                   " & vbNewLine _
                     & ",MC.CUST_NM_L                 AS CUST_NM_L                                   " & vbNewLine _
                     & ",MAIN2.ZAI_REC_NO             AS ZAI_REC_NO                                  " & vbNewLine _
                     & ",MAIN2.GOODS_CD_NRS           AS GOODS_CD_NRS                                " & vbNewLine _
                     & ",MG.GOODS_CD_CUST             AS GOODS_CD_CUST                               " & vbNewLine _
                     & ",MG.GOODS_NM_1                AS GOODS_NM                                    " & vbNewLine _
                     & ",MAIN2.LOT_NO                 AS LOT_NO                                      " & vbNewLine _
                     & ",MAIN2.SERIAL_NO              AS SERIAL_NO                                   " & vbNewLine _
                     & ",SUM(HIKAKU_ZAI_NB)           AS HIKAKU_ZAI_NB                               " & vbNewLine _
                     & ",SUM(PORA_ZAI_NB)             AS PORA_ZAI_NB                                 " & vbNewLine _
                     & ",SUM(EXTRA_ZAI_NB)            AS EXTRA_ZAI_NB                                " & vbNewLine _
                     & ",SUM(HIKAKU_ZAI_QT)           AS HIKAKU_ZAI_QT                               " & vbNewLine _
                     & ",SUM(PORA_ZAI_QT)             AS PORA_ZAI_QT                                 " & vbNewLine _
                     & ",SUM(EXTRA_ZAI_QT)            AS EXTRA_ZAI_QT                                " & vbNewLine _
                     & ",MAX(ZAI_FLAG)                AS ZAI_FLAG                                    " & vbNewLine _
                     & "FROM                                                                         " & vbNewLine _
                     & "  (SELECT                                                                    " & vbNewLine _
                     & "    CUST_CD_L                 AS CUST_CD_L                                   " & vbNewLine _
                     & "   ,CUST_CD_M                 AS CUST_CD_M                                   " & vbNewLine _
                     & "   ,CUST_NM_L                 AS CUST_NM_L                                   " & vbNewLine _
                     & "   ,ZAI_REC_NO                AS ZAI_REC_NO                                  " & vbNewLine _
                     & "   ,GOODS_CD_NRS              AS GOODS_CD_NRS                                " & vbNewLine _
                     & "   ,LOT_NO                    AS LOT_NO                                      " & vbNewLine _
                     & "   ,SERIAL_NO                 AS SERIAL_NO                                   " & vbNewLine _
                     & "   ,SUM(ALCTD_NB)             AS HIKAKU_ZAI_NB                               " & vbNewLine _
                     & "   ,0                         AS PORA_ZAI_NB                                 " & vbNewLine _
                     & "   ,0                         AS EXTRA_ZAI_NB                                " & vbNewLine _
                     & "   ,SUM(ALCTD_QT)             AS HIKAKU_ZAI_QT                               " & vbNewLine _
                     & "   ,0                         AS PORA_ZAI_QT                                 " & vbNewLine _
                     & "   ,0                         AS EXTRA_ZAI_QT                                " & vbNewLine _
                     & "   ,'1'                       AS ZAI_FLAG                                    " & vbNewLine _
                     & "   FROM                                                                      " & vbNewLine _
                     & "   --在庫データ(D_ZAI_TRS)                                                   " & vbNewLine _
                     & "     (SELECT DISTINCT                                                        " & vbNewLine _
                     & "       CUST_CD_L              AS CUST_CD_L                                   " & vbNewLine _
                     & "      ,CUST_CD_M              AS CUST_CD_M                                   " & vbNewLine _
                     & "      ,CUST_NM_L              AS CUST_NM_L                                   " & vbNewLine _
                     & "      ,ZAI_REC_NO             AS ZAI_REC_NO                                  " & vbNewLine _
                     & "      ,GOODS_CD_NRS           AS GOODS_CD_NRS                                " & vbNewLine _
                     & "      ,LOT_NO                 AS LOT_NO                                      " & vbNewLine _
                     & "      ,SERIAL_NO              AS SERIAL_NO                                   " & vbNewLine _
                     & "      ,PORA_ZAI_NB            AS PORA_ZAI_NB                                 " & vbNewLine _
                     & "      ,ALCTD_NB               AS ALCTD_NB                                    " & vbNewLine _
                     & "      ,ALLOC_CAN_NB           AS ALLOC_CAN_NB                                " & vbNewLine _
                     & "      ,PORA_ZAI_QT            AS PORA_ZAI_QT                                 " & vbNewLine _
                     & "      ,ALCTD_QT               AS ALCTD_QT                                    " & vbNewLine _
                     & "      ,ALLOC_CAN_QT           AS ALLOC_CAN_QT                                " & vbNewLine _
                     & "      FROM                                                                   " & vbNewLine _
                     & "        (SELECT                                                              " & vbNewLine _
                     & "          ZAI1.CUST_CD_L            AS CUST_CD_L                             " & vbNewLine _
                     & "         ,ZAI1.CUST_CD_M            AS CUST_CD_M                             " & vbNewLine _
                     & "         ,''                        AS CUST_NM_L                             " & vbNewLine _
                     & "         ,ZAI1.ZAI_REC_NO           AS ZAI_REC_NO                            " & vbNewLine _
                     & "         ,ZAI1.GOODS_CD_NRS         AS GOODS_CD_NRS                          " & vbNewLine _
                     & "         ,ZAI1.LOT_NO               AS LOT_NO                                " & vbNewLine _
                     & "         ,ZAI1.SERIAL_NO            AS SERIAL_NO                             " & vbNewLine _
                     & "         ,ZAI1.PORA_ZAI_NB          AS PORA_ZAI_NB                           " & vbNewLine _
                     & "         ,ZAI1.ALCTD_NB             AS ALCTD_NB                              " & vbNewLine _
                     & "         ,ZAI1.ALLOC_CAN_NB         AS ALLOC_CAN_NB                          " & vbNewLine _
                     & "         ,ZAI1.PORA_ZAI_QT          AS PORA_ZAI_QT                           " & vbNewLine _
                     & "         ,ZAI1.ALCTD_QT             AS ALCTD_QT                              " & vbNewLine _
                     & "         ,ZAI1.ALLOC_CAN_QT         AS ALLOC_CAN_QT                          " & vbNewLine _
                     & "         FROM                                                                " & vbNewLine _
                     & "         $LM_TRN$..D_ZAI_TRS ZAI1                                            " & vbNewLine _
                     & "         WHERE                                                               " & vbNewLine _
                     & "             ZAI1.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                     & "         AND ZAI1.NRS_BR_CD = @NRS_BR_CD                                     " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ALCTD_NB = SQL_ALCTD_NB & vbNewLine _
                     & "         AND ZAI1.CUST_CD_L = @CUST_CD_L                                     " & vbNewLine _
                     & "         AND ZAI1.CUST_CD_M = @CUST_CD_M                                     " & vbNewLine
        End If
        SQL_ALCTD_NB = SQL_ALCTD_NB & vbNewLine _
                     & "         AND (ZAI1.ALCTD_NB <> 0 OR ZAI1.ALCTD_QT <> 0)                      " & vbNewLine _
                     & "        ) BASE1                                                              " & vbNewLine _
                     & "     ) BASE2                                                                 " & vbNewLine _
                     & "     GROUP BY                                                                " & vbNewLine _
                     & "      CUST_CD_L                                                              " & vbNewLine _
                     & "     ,CUST_CD_M                                                              " & vbNewLine _
                     & "     ,CUST_NM_L                                                              " & vbNewLine _
                     & "     ,ZAI_REC_NO                                                             " & vbNewLine _
                     & "     ,GOODS_CD_NRS                                                           " & vbNewLine _
                     & "     ,LOT_NO                                                                 " & vbNewLine _
                     & "     ,SERIAL_NO                                                              " & vbNewLine _
                     & "     --入出荷の履歴                                                          " & vbNewLine _
                     & "     UNION ALL                                                               " & vbNewLine _
                     & "     SELECT                                                                  " & vbNewLine _
                     & "      CUST_CD_L                  AS CUST_CD_L                                " & vbNewLine _
                     & "     ,CUST_CD_M                  AS CUST_CD_M                                " & vbNewLine _
                     & "     ,CUST_NM_L                  AS CUST_NM_L                                " & vbNewLine _
                     & "     ,ZAI_REC_NO                 AS ZAI_REC_NO                               " & vbNewLine _
                     & "     ,GOODS_CD_NRS               AS GODOS_CD_NRS                             " & vbNewLine _
                     & "     ,LOT_NO                     AS LOT_NO                                   " & vbNewLine _
                     & "     ,SERIAL_NO                  AS SERIAL_NO                                " & vbNewLine _
                     & "     ,0                          AS HIKAKU_ZAI_NB                            " & vbNewLine _
                     & "     ,SUM(PORA_ZAI_NB)           AS PORA_ZAI_NB                              " & vbNewLine _
                     & "     ,0                          AS EXTRA_ZAI_NB                             " & vbNewLine _
                     & "     ,0                          AS HIKAKU_ZAI_QT                            " & vbNewLine _
                     & "     ,SUM(PORA_ZAI_QT)           AS PORA_ZAI_QT                              " & vbNewLine _
                     & "     ,0                          AS EXTRA_ZAI_QT                             " & vbNewLine _
                     & "     ,'0'                        AS ZAI_FLAG                                 " & vbNewLine _
                     & "     FROM                                                                    " & vbNewLine _
                     & "     --出荷データ(C_OUTKA_S)                                                 " & vbNewLine _
                     & "       (SELECT DISTINCT                                                      " & vbNewLine _
                     & "        OUTKA_NO_L                                                           " & vbNewLine _
                     & "       ,OUTKA_NO_M                                                           " & vbNewLine _
                     & "       ,OUTKA_NO_S                                                           " & vbNewLine _
                     & "       ,CUST_CD_L                                                            " & vbNewLine _
                     & "       ,CUST_CD_M                                                            " & vbNewLine _
                     & "       ,CUST_NM_L                                                            " & vbNewLine _
                     & "       ,ZAI_REC_NO                                                           " & vbNewLine _
                     & "       ,GOODS_CD_NRS                                                         " & vbNewLine _
                     & "       ,LOT_NO                                                               " & vbNewLine _
                     & "       ,SERIAL_NO                                                            " & vbNewLine _
                     & "       ,PORA_ZAI_NB                                                          " & vbNewLine _
                     & "       ,PORA_ZAI_QT                                                          " & vbNewLine _
                     & "       FROM                                                                  " & vbNewLine _
                     & "         (SELECT                                                             " & vbNewLine _
                     & "           OUTS.OUTKA_NO_L                AS OUTKA_NO_L                      " & vbNewLine _
                     & "           ,OUTS.OUTKA_NO_M               AS OUTKA_NO_M                      " & vbNewLine _
                     & "           ,OUTS.OUTKA_NO_S               AS OUTKA_NO_S                      " & vbNewLine _
                     & "           ,OUTL.CUST_CD_L                AS CUST_CD_L                       " & vbNewLine _
                     & "           ,OUTL.CUST_CD_M                AS CUST_CD_M                       " & vbNewLine _
                     & "           ,''                            AS CUST_NM_L                       " & vbNewLine _
                     & "           ,OUTS.ZAI_REC_NO               AS ZAI_REC_NO                      " & vbNewLine _
                     & "           ,OUTM.GOODS_CD_NRS             AS GOODS_CD_NRS                    " & vbNewLine _
                     & "           ,ISNULL(OUTS.LOT_NO, '')       AS LOT_NO                          " & vbNewLine _
                     & "           ,ISNULL(OUTS.SERIAL_NO, '')    AS SERIAL_NO                       " & vbNewLine _
                     & "           ,OUTS.ALCTD_NB                 AS PORA_ZAI_NB                     " & vbNewLine _
                     & "           ,OUTS.ALCTD_QT                 AS PORA_ZAI_QT                     " & vbNewLine _
                     & "           FROM                                                              " & vbNewLine _
                     & "           $LM_TRN$..C_OUTKA_S OUTS                                          " & vbNewLine _
                     & "           LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                " & vbNewLine _
                     & "           ON  OUTM.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                     & "           AND OUTM.NRS_BR_CD = OUTS.NRS_BR_CD                               " & vbNewLine _
                     & "           AND OUTM.OUTKA_NO_L = OUTS.OUTKA_NO_L                             " & vbNewLine _
                     & "           AND OUTM.OUTKA_NO_M = OUTS.OUTKA_NO_M                             " & vbNewLine _
                     & "           LEFT JOIN $LM_TRN$..C_OUTKA_L OUTL                                " & vbNewLine _
                     & "           ON  OUTL.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                     & "           AND OUTL.NRS_BR_CD = OUTM.NRS_BR_CD                               " & vbNewLine _
                     & "           AND OUTL.OUTKA_NO_L = OUTM.OUTKA_NO_L                             " & vbNewLine _
                     & "           WHERE                                                             " & vbNewLine _
                     & "               OUTS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                     & "           AND OUTM.ALCTD_KB <> '04'                                         " & vbNewLine _
                     & "           AND OUTL.OUTKA_STATE_KB < '60'                                    " & vbNewLine _
                     & "           AND OUTM.GOODS_CD_NRS_FROM = ''                                  " & vbNewLine _
                     & "           AND OUTL.NRS_BR_CD = @NRS_BR_CD                                   " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ALCTD_NB = SQL_ALCTD_NB & vbNewLine _
                     & "           AND OUTL.CUST_CD_L = @CUST_CD_L                                   " & vbNewLine _
                     & "           AND OUTL.CUST_CD_M = @CUST_CD_M                                   " & vbNewLine
        End If
        SQL_ALCTD_NB = SQL_ALCTD_NB & vbNewLine _
                     & "           UNION ALL                                                         " & vbNewLine _
                     & "           SELECT                                                            " & vbNewLine _
                     & "           OUTS.OUTKA_NO_L                AS OUTKA_NO_L                      " & vbNewLine _
                     & "           ,OUTS.OUTKA_NO_M               AS OUTKA_NO_M                      " & vbNewLine _
                     & "           ,OUTS.OUTKA_NO_S               AS OUTKA_NO_S                      " & vbNewLine _
                     & "           ,ZAI.CUST_CD_L                 AS CUST_CD_L                       " & vbNewLine _
                     & "           ,ZAI.CUST_CD_M                 AS CUST_CD_M                       " & vbNewLine _
                     & "           ,''                            AS CUST_NM_L                       " & vbNewLine _
                     & "           ,OUTS.ZAI_REC_NO               AS ZAI_REC_NO                      " & vbNewLine _
                     & "           ,OUTM.GOODS_CD_NRS_FROM        AS GOODS_CD_NRS                    " & vbNewLine _
                     & "           ,ISNULL(OUTS.LOT_NO, '')       AS LOT_NO                          " & vbNewLine _
                     & "           ,ISNULL(OUTS.SERIAL_NO, '')    AS SERIAL_NO                       " & vbNewLine _
                     & "           ,OUTS.ALCTD_NB                 AS PORA_ZAI_NB                     " & vbNewLine _
                     & "           ,OUTS.ALCTD_QT                 AS PORA_ZAI_QT                     " & vbNewLine _
                     & "           FROM                                                              " & vbNewLine _
                     & "           $LM_TRN$..C_OUTKA_S OUTS                                          " & vbNewLine _
                     & "           LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                " & vbNewLine _
                     & "           ON  OUTM.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                     & "           AND OUTM.NRS_BR_CD = OUTS.NRS_BR_CD                               " & vbNewLine _
                     & "           AND OUTM.OUTKA_NO_L = OUTS.OUTKA_NO_L                             " & vbNewLine _
                     & "           AND OUTM.OUTKA_NO_M = OUTS.OUTKA_NO_M                             " & vbNewLine _
                     & "           LEFT JOIN $LM_TRN$..C_OUTKA_L OUTL                                " & vbNewLine _
                     & "           ON  OUTL.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                     & "           AND OUTL.NRS_BR_CD = OUTM.NRS_BR_CD                               " & vbNewLine _
                     & "           AND OUTL.OUTKA_NO_L = OUTM.OUTKA_NO_L                             " & vbNewLine _
                     & "           LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                 " & vbNewLine _
                     & "           ON  ZAI.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                     & "           AND OUTS.NRS_BR_CD = ZAI.NRS_BR_CD                                " & vbNewLine _
                     & "           AND OUTS.ZAI_REC_NO = ZAI.ZAI_REC_NO                              " & vbNewLine _
                     & "           WHERE                                                             " & vbNewLine _
                     & "               OUTS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                     & "           AND OUTM.ALCTD_KB <> '04'                                         " & vbNewLine _
                     & "           AND OUTL.OUTKA_STATE_KB < '60'                                    " & vbNewLine _
                     & "           AND OUTM.GOODS_CD_NRS_FROM <> ''                                  " & vbNewLine _
                     & "           AND OUTL.NRS_BR_CD = @NRS_BR_CD                                   " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ALCTD_NB = SQL_ALCTD_NB & vbNewLine _
                     & "           AND ZAI.CUST_CD_L = @CUST_CD_L                                    " & vbNewLine _
                     & "           AND ZAI.CUST_CD_M = @CUST_CD_M                                    " & vbNewLine
        End If
        SQL_ALCTD_NB = SQL_ALCTD_NB & vbNewLine _
                     & "         ) BASE3                                                             " & vbNewLine _
                     & "     ) BASE4                                                                 " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ALCTD_NB = SQL_ALCTD_NB & vbNewLine _
                     & "     WHERE                                                                   " & vbNewLine _
                     & "         CUST_CD_L = @CUST_CD_L                                              " & vbNewLine _
                     & "     AND CUST_CD_M = @CUST_CD_M                                              " & vbNewLine
        End If
        SQL_ALCTD_NB = SQL_ALCTD_NB & vbNewLine _
                     & "     GROUP BY                                                                " & vbNewLine _
                     & "      CUST_CD_L                                                              " & vbNewLine _
                     & "     ,CUST_CD_M                                                              " & vbNewLine _
                     & "     ,CUST_NM_L                                                              " & vbNewLine _
                     & "     ,ZAI_REC_NO                                                             " & vbNewLine _
                     & "     ,GOODS_CD_NRS                                                           " & vbNewLine _
                     & "     ,LOT_NO                                                                 " & vbNewLine _
                     & "     ,SERIAL_NO                                                              " & vbNewLine _
                     & "  ) MAIN2                                                                    " & vbNewLine _
                     & "  LEFT JOIN $LM_MST$..M_GOODS MG                                             " & vbNewLine _
                     & "  ON  MG.NRS_BR_CD = @NRS_BR_CD                                              " & vbNewLine _
                     & "  AND MG.CUST_CD_L = MAIN2.CUST_CD_L                                         " & vbNewLine _
                     & "  AND MG.CUST_CD_M = MAIN2.CUST_CD_M                                         " & vbNewLine _
                     & "  AND MG.GOODS_CD_NRS = MAIN2.GOODS_CD_NRS                                   " & vbNewLine _
                     & "  LEFT JOIN $LM_MST$..M_CUST MC                                              " & vbNewLine _
                     & "  ON  MC.NRS_BR_CD = @NRS_BR_CD                                              " & vbNewLine _
                     & "  AND MC.CUST_CD_L = MAIN2.CUST_CD_L                                         " & vbNewLine _
                     & "  AND MC.CUST_CD_M = MAIN2.CUST_CD_M                                         " & vbNewLine _
                     & "  AND MC.CUST_CD_S = '00'                                                    " & vbNewLine _
                     & "  AND MC.CUST_CD_SS = '00'                                                   " & vbNewLine _
                     & "  LEFT JOIN $LM_MST$..M_NRS_BR MB                                            " & vbNewLine _
                     & "  ON  MB.NRS_BR_CD = @NRS_BR_CD                                              " & vbNewLine _
                     & "  LEFT JOIN $LM_MST$..Z_KBN KBN1                                             " & vbNewLine _
                     & "  ON  KBN_GROUP_CD = 'P007'                                                  " & vbNewLine _
                     & "  AND KBN_CD = '00'                                                          " & vbNewLine _
                     & "  GROUP BY                                                                   " & vbNewLine _
                     & "   KBN1.KBN_NM1                                                              " & vbNewLine _
                     & "  ,MB.NRS_BR_NM                                                              " & vbNewLine _
                     & "  ,MAIN2.CUST_CD_L                                                           " & vbNewLine _
                     & "  ,MAIN2.CUST_CD_M                                                           " & vbNewLine _
                     & "  ,MC.CUST_NM_L                                                              " & vbNewLine _
                     & "  ,MAIN2.ZAI_REC_NO                                                          " & vbNewLine _
                     & "  ,MAIN2.GOODS_CD_NRS                                                        " & vbNewLine _
                     & "  ,MG.GOODS_CD_CUST                                                          " & vbNewLine _
                     & "  ,MG.GOODS_NM_1                                                             " & vbNewLine _
                     & "  ,MAIN2.LOT_NO                                                              " & vbNewLine _
                     & "  ,MAIN2.SERIAL_NO                                                           " & vbNewLine _
                     & ") MAIN1                                                                      " & vbNewLine _
                     & "--帳票パターン取得用商品Mデータ取得                                          " & vbNewLine _
                     & "LEFT JOIN $LM_MST$..M_GOODS MGR                                              " & vbNewLine _
                     & "ON  MGR.NRS_BR_CD = @NRS_BR_CD                                               " & vbNewLine _
                     & "AND MGR.GOODS_CD_NRS = MAIN1.GOODS_CD_NRS                                    " & vbNewLine _
                     & "--荷主帳票パターン取得                                                       " & vbNewLine _
                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                          " & vbNewLine _
                     & "ON  @NRS_BR_CD = MCR1.NRS_BR_CD                                              " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ALCTD_NB = SQL_ALCTD_NB & vbNewLine _
                     & "AND @CUST_CD_L = MCR1.CUST_CD_L                                              " & vbNewLine _
                     & "AND @CUST_CD_M = MCR1.CUST_CD_M                                              " & vbNewLine
        End If
        SQL_ALCTD_NB = SQL_ALCTD_NB & vbNewLine _
                     & "AND '00' = MCR1.CUST_CD_S                                                    " & vbNewLine _
                     & "AND MCR1.PTN_ID = '29'                                                       " & vbNewLine _
                     & "--帳票パターン取得                                                           " & vbNewLine _
                     & "LEFT JOIN $LM_MST$..M_RPT MR1                                                " & vbNewLine _
                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                           " & vbNewLine _
                     & "AND MR1.PTN_ID = MCR1.PTN_ID                                                 " & vbNewLine _
                     & "AND MR1.PTN_CD = MCR1.PTN_CD                                                 " & vbNewLine _
                     & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                     & "--商品Mの荷主での荷主帳票パターン取得                                        " & vbNewLine _
                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                          " & vbNewLine _
                     & "ON  MGR.NRS_BR_CD = MCR2.NRS_BR_CD                                           " & vbNewLine _
                     & "AND MGR.CUST_CD_L = MCR2.CUST_CD_L                                           " & vbNewLine _
                     & "AND MGR.CUST_CD_M = MCR2.CUST_CD_M                                           " & vbNewLine _
                     & "AND MGR.CUST_CD_S = MCR2.CUST_CD_S                                           " & vbNewLine _
                     & "AND MCR2.PTN_ID = '29'                                                       " & vbNewLine _
                     & "--帳票パターン取得                                                           " & vbNewLine _
                     & "LEFT JOIN $LM_MST$..M_RPT MR2                                                " & vbNewLine _
                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                           " & vbNewLine _
                     & "AND MR2.PTN_ID = MCR2.PTN_ID                                                 " & vbNewLine _
                     & "AND MR2.PTN_CD = MCR2.PTN_CD                                                 " & vbNewLine _
                     & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                     & "--存在しない場合の帳票パターン取得                                           " & vbNewLine _
                     & "LEFT JOIN $LM_MST$..M_RPT MR3                                                " & vbNewLine _
                     & "ON  MR3.NRS_BR_CD = @NRS_BR_CD                                               " & vbNewLine _
                     & "AND MR3.PTN_ID = '29'                                                        " & vbNewLine _
                     & "AND MR3.STANDARD_FLAG = '01'                                                 " & vbNewLine _
                     & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                     & "WHERE                                                                        " & vbNewLine _
                     & "    (HIKAKU_ZAI_NB <> PORA_ZAI_NB OR HIKAKU_ZAI_QT <> PORA_ZAI_QT)           " & vbNewLine _
                     & "ORDER BY                                                                     " & vbNewLine _
                     & " NRS_BR_CD                                                                   " & vbNewLine _
                     & ",CUST_CD_L                                                                   " & vbNewLine _
                     & ",CUST_CD_M                                                                   " & vbNewLine _
                     & ",GOODS_CD_NRS                                                                " & vbNewLine _
                     & ",LOT_NO                                                                      " & vbNewLine _
                     & ",ZAI_REC_NO                                                                  " & vbNewLine _
                     & ",SERIAL_NO                                                                   " & vbNewLine

    End Sub

    ''' <summary>
    ''' 引当数比較用SQL作成
    ''' </summary>
    ''' <param name="custCdL"></param>
    ''' <param name="custCdM"></param>
    ''' <remarks></remarks>
    Private Sub CreateSqlAllocCanNb(ByVal custCdL As String, ByVal custCdM As String)

        SQL_ALLOC_CAN_NB = "SELECT                                                                                           " & vbNewLine _
                         & "CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                 " & vbNewLine _
                         & " WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                     " & vbNewLine _
                         & " ELSE MR3.RPT_ID END   AS RPT_ID                                                                 " & vbNewLine _
                         & ",MAIN1.NM                                                                                        " & vbNewLine _
                         & ",MAIN1.NRS_BR_CD                                                                                 " & vbNewLine _
                         & ",MAIN1.NRS_BR_NM                                                                                 " & vbNewLine _
                         & ",MAIN1.PTN                                                                                       " & vbNewLine _
                         & ",MAIN1.CUST_CD_L                                                                                 " & vbNewLine _
                         & ",MAIN1.CUST_CD_M                                                                                 " & vbNewLine _
                         & ",MAIN1.CUST_NM_L                                                                                 " & vbNewLine _
                         & ",MAIN1.ZAI_REC_NO                                                                                " & vbNewLine _
                         & ",MAIN1.GOODS_CD_NRS                                                                              " & vbNewLine _
                         & ",MAIN1.GOODS_CD_CUST                                                                             " & vbNewLine _
                         & ",MAIN1.GOODS_NM                                                                                  " & vbNewLine _
                         & ",MAIN1.LOT_NO                                                                                    " & vbNewLine _
                         & ",MAIN1.SERIAL_NO                                                                                 " & vbNewLine _
                         & ",MAIN1.HIKAKU_ZAI_NB                                                                             " & vbNewLine _
                         & ",MAIN1.PORA_ZAI_NB                                                                               " & vbNewLine _
                         & ",MAIN1.EXTRA_ZAI_NB                                                                              " & vbNewLine _
                         & ",MAIN1.HIKAKU_ZAI_QT                                                                             " & vbNewLine _
                         & ",MAIN1.PORA_ZAI_QT                                                                               " & vbNewLine _
                         & ",MAIN1.EXTRA_ZAI_QT                                                                              " & vbNewLine _
                         & "FROM                                                                                             " & vbNewLine _
                         & "(SELECT                                                                                          " & vbNewLine _
                         & " KBN1.KBN_NM1                AS NM                                                               " & vbNewLine _
                         & ",@NRS_BR_CD                  AS NRS_BR_CD                                                        " & vbNewLine _
                         & ",MB.NRS_BR_NM                AS NRS_BR_NM                                                        " & vbNewLine _
                         & ",@PTN                        AS PTN                                                              " & vbNewLine _
                         & ",MAIN2.CUST_CD_L             AS CUST_CD_L                                                        " & vbNewLine _
                         & ",MAIN2.CUST_CD_M             AS CUST_CD_M                                                        " & vbNewLine _
                         & ",MC.CUST_NM_L                AS CUST_NM_L                                                        " & vbNewLine _
                         & ",MAIN2.ZAI_REC_NO            AS ZAI_REC_NO                                                       " & vbNewLine _
                         & ",MAIN2.GOODS_CD_NRS          AS GOODS_CD_NRS                                                     " & vbNewLine _
                         & ",MG.GOODS_CD_CUST            AS GOODS_CD_CUST                                                    " & vbNewLine _
                         & ",MG.GOODS_NM_1               AS GOODS_NM                                                         " & vbNewLine _
                         & ",MAIN2.LOT_NO                AS LOT_NO                                                           " & vbNewLine _
                         & ",MAIN2.SERIAL_NO             AS SERIAL_NO                                                        " & vbNewLine _
                         & ",SUM(HIKAKU_ZAI_NB)          AS HIKAKU_ZAI_NB                                                    " & vbNewLine _
                         & ",SUM(PORA_ZAI_NB)            AS PORA_ZAI_NB                                                      " & vbNewLine _
                         & ",SUM(EXTRA_ZAI_NB)           AS EXTRA_ZAI_NB                                                     " & vbNewLine _
                         & ",SUM(HIKAKU_ZAI_QT)          AS HIKAKU_ZAI_QT                                                    " & vbNewLine _
                         & ",SUM(PORA_ZAI_QT)            AS PORA_ZAI_QT                                                      " & vbNewLine _
                         & ",SUM(EXTRA_ZAI_QT)           AS EXTRA_ZAI_QT                                                     " & vbNewLine _
                         & "FROM                                                                                             " & vbNewLine _
                         & "  (SELECT                                                                                        " & vbNewLine _
                         & "    CUST_CD_L                AS CUST_CD_L                                                        " & vbNewLine _
                         & "   ,CUST_CD_M                AS CUST_CD_M                                                        " & vbNewLine _
                         & "   ,CUST_NM_L                AS CUST_NM_L                                                        " & vbNewLine _
                         & "   ,ZAI_REC_NO               AS ZAI_REC_NO                                                       " & vbNewLine _
                         & "   ,GOODS_CD_NRS             AS GOODS_CD_NRS                                                     " & vbNewLine _
                         & "   ,LOT_NO                   AS LOT_NO                                                           " & vbNewLine _
                         & "   ,SERIAL_NO                AS SERIAL_NO                                                        " & vbNewLine _
                         & "   ,SUM(PORA_ZAI_NB)         AS HIKAKU_ZAI_NB                                                    " & vbNewLine _
                         & "   ,SUM(ALCTD_NB)            AS PORA_ZAI_NB                                                      " & vbNewLine _
                         & "   ,SUM(ALLOC_CAN_NB)        AS EXTRA_ZAI_NB                                                     " & vbNewLine _
                         & "   ,0                        AS HIKAKU_ZAI_QT                                                    " & vbNewLine _
                         & "   ,0                        AS PORA_ZAI_QT                                                      " & vbNewLine _
                         & "   ,0                        AS EXTRA_ZAI_QT                                                     " & vbNewLine _
                         & "   FROM                                                                                          " & vbNewLine _
                         & "   --在庫データ(D_ZAI_TRS)                                                                       " & vbNewLine _
                         & "     (SELECT DISTINCT                                                                            " & vbNewLine _
                         & "       CUST_CD_L             AS CUST_CD_L                                                        " & vbNewLine _
                         & "      ,CUST_CD_M             AS CUST_CD_M                                                        " & vbNewLine _
                         & "      ,CUST_NM_L             AS CUST_NM_L                                                        " & vbNewLine _
                         & "      ,ZAI_REC_NO            AS ZAI_REC_NO                                                       " & vbNewLine _
                         & "      ,GOODS_CD_NRS          AS GOODS_CD_NRS                                                     " & vbNewLine _
                         & "      ,LOT_NO                AS LOT_NO                                                           " & vbNewLine _
                         & "      ,SERIAL_NO             AS SERIAL_NO                                                        " & vbNewLine _
                         & "      ,PORA_ZAI_NB           AS PORA_ZAI_NB                                                      " & vbNewLine _
                         & "      ,ALCTD_NB              AS ALCTD_NB                                                         " & vbNewLine _
                         & "      ,ALLOC_CAN_NB          AS ALLOC_CAN_NB                                                     " & vbNewLine _
                         & "      ,PORA_ZAI_QT           AS PORA_ZAI_QT                                                      " & vbNewLine _
                         & "      ,ALCTD_QT              AS ALCTD_QT                                                         " & vbNewLine _
                         & "      ,ALLOC_CAN_QT          AS ALLOC_CAN_QT                                                     " & vbNewLine _
                         & "      FROM                                                                                       " & vbNewLine _
                         & "        (SELECT                                                                                  " & vbNewLine _
                         & "          ZAI1.CUST_CD_L           AS CUST_CD_L                                                  " & vbNewLine _
                         & "         ,ZAI1.CUST_CD_M           AS CUST_CD_M                                                  " & vbNewLine _
                         & "         ,''                       AS CUST_NM_L                                                  " & vbNewLine _
                         & "         ,ZAI1.ZAI_REC_NO          AS ZAI_REC_NO                                                 " & vbNewLine _
                         & "         ,ZAI1.GOODS_CD_NRS        AS GOODS_CD_NRS                                               " & vbNewLine _
                         & "         ,ZAI1.LOT_NO              AS LOT_NO                                                     " & vbNewLine _
                         & "         ,ZAI1.SERIAL_NO           AS SERIAL_NO                                                  " & vbNewLine _
                         & "         ,ZAI1.PORA_ZAI_NB         AS PORA_ZAI_NB                                                " & vbNewLine _
                         & "         ,ZAI1.ALCTD_NB            AS ALCTD_NB                                                   " & vbNewLine _
                         & "         ,ZAI1.ALLOC_CAN_NB        AS ALLOC_CAN_NB                                               " & vbNewLine _
                         & "         ,ZAI1.PORA_ZAI_QT         AS PORA_ZAI_QT                                                " & vbNewLine _
                         & "         ,ZAI1.ALCTD_QT            AS ALCTD_QT                                                   " & vbNewLine _
                         & "         ,ZAI1.ALLOC_CAN_QT        AS ALLOC_CAN_QT                                               " & vbNewLine _
                         & "         FROM                                                                                    " & vbNewLine _
                         & "         $LM_TRN$..D_ZAI_TRS ZAI1                                                                " & vbNewLine _
                         & "         WHERE                                                                                   " & vbNewLine _
                         & "             ZAI1.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                         & "         AND ZAI1.NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ALLOC_CAN_NB = SQL_ALLOC_CAN_NB & vbNewLine _
                         & "         AND ZAI1.CUST_CD_L = @CUST_CD_L                                                         " & vbNewLine _
                         & "         AND ZAI1.CUST_CD_M = @CUST_CD_M                                                         " & vbNewLine
        End If
        SQL_ALLOC_CAN_NB = SQL_ALLOC_CAN_NB & vbNewLine _
                         & "         AND (ZAI1.PORA_ZAI_NB <> 0 OR ZAI1.ALCTD_NB <> 0 OR ZAI1.ALLOC_CAN_NB <> 0)             " & vbNewLine _
                         & "        ) BASE1                                                                                  " & vbNewLine _
                         & "     ) BASE2                                                                                     " & vbNewLine _
                         & "     GROUP BY                                                                                    " & vbNewLine _
                         & "      CUST_CD_L                                                                                  " & vbNewLine _
                         & "     ,CUST_CD_M                                                                                  " & vbNewLine _
                         & "     ,CUST_NM_L                                                                                  " & vbNewLine _
                         & "     ,ZAI_REC_NO                                                                                 " & vbNewLine _
                         & "     ,GOODS_CD_NRS                                                                               " & vbNewLine _
                         & "     ,LOT_NO                                                                                     " & vbNewLine _
                         & "     ,SERIAL_NO                                                                                  " & vbNewLine _
                         & "  ) MAIN2                                                                                        " & vbNewLine _
                         & "  LEFT JOIN $LM_MST$..M_GOODS MG                                                                 " & vbNewLine _
                         & "  ON  MG.NRS_BR_CD = @NRS_BR_CD                                                                  " & vbNewLine _
                         & "  AND MG.CUST_CD_L = MAIN2.CUST_CD_L                                                             " & vbNewLine _
                         & "  AND MG.CUST_CD_M = MAIN2.CUST_CD_M                                                             " & vbNewLine _
                         & "  AND MG.GOODS_CD_NRS = MAIN2.GOODS_CD_NRS                                                       " & vbNewLine _
                         & "  LEFT JOIN $LM_MST$..M_CUST MC                                                                  " & vbNewLine _
                         & "  ON  MC.NRS_BR_CD = @NRS_BR_CD                                                                  " & vbNewLine _
                         & "  AND MC.CUST_CD_L = MAIN2.CUST_CD_L                                                             " & vbNewLine _
                         & "  AND MC.CUST_CD_M = MAIN2.CUST_CD_M                                                             " & vbNewLine _
                         & "  AND MC.CUST_CD_S = '00'                                                                        " & vbNewLine _
                         & "  AND MC.CUST_CD_SS = '00'                                                                       " & vbNewLine _
                         & "  LEFT JOIN $LM_MST$..M_NRS_BR MB                                                                " & vbNewLine _
                         & "  ON  MB.NRS_BR_CD = @NRS_BR_CD                                                                  " & vbNewLine _
                         & "  LEFT JOIN $LM_MST$..Z_KBN KBN1                                                                 " & vbNewLine _
                         & "  ON  KBN1.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
                         & "  AND KBN_GROUP_CD = 'P007'                                                                      " & vbNewLine _
                         & "  AND KBN_CD = '00'                                                                              " & vbNewLine _
                         & "  GROUP BY                                                                                       " & vbNewLine _
                         & "   KBN1.KBN_NM1                                                                                  " & vbNewLine _
                         & "  ,MB.NRS_BR_NM                                                                                  " & vbNewLine _
                         & "  ,MAIN2.CUST_CD_L                                                                               " & vbNewLine _
                         & "  ,MAIN2.CUST_CD_M                                                                               " & vbNewLine _
                         & "  ,MC.CUST_NM_L                                                                                  " & vbNewLine _
                         & "  ,MAIN2.ZAI_REC_NO                                                                              " & vbNewLine _
                         & "  ,MAIN2.GOODS_CD_NRS                                                                            " & vbNewLine _
                         & "  ,MG.GOODS_CD_CUST                                                                              " & vbNewLine _
                         & "  ,MG.GOODS_NM_1                                                                                 " & vbNewLine _
                         & "  ,MAIN2.LOT_NO                                                                                  " & vbNewLine _
                         & "  ,MAIN2.SERIAL_NO                                                                               " & vbNewLine _
                         & ") MAIN1                                                                                          " & vbNewLine _
                         & "--帳票パターン取得用商品Mデータ取得                                                              " & vbNewLine _
                         & "LEFT JOIN $LM_MST$..M_GOODS MGR                                                                  " & vbNewLine _
                         & "ON  MGR.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                         & "AND MGR.NRS_BR_CD = @NRS_BR_CD                                                                   " & vbNewLine _
                         & "AND MGR.GOODS_CD_NRS = MAIN1.GOODS_CD_NRS                                                        " & vbNewLine _
                         & "--荷主帳票パターン取得                                                                           " & vbNewLine _
                         & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                              " & vbNewLine _
                         & "ON  @NRS_BR_CD = MCR1.NRS_BR_CD                                                                  " & vbNewLine
        If String.IsNullOrEmpty(custCdL) = False OrElse String.IsNullOrEmpty(custCdM) = False Then
            SQL_ALLOC_CAN_NB = SQL_ALLOC_CAN_NB & vbNewLine _
                         & "AND @CUST_CD_L = MCR1.CUST_CD_L                                                                  " & vbNewLine _
                         & "AND @CUST_CD_M = MCR1.CUST_CD_M                                                                  " & vbNewLine
        End If
        SQL_ALLOC_CAN_NB = SQL_ALLOC_CAN_NB & vbNewLine _
                         & "AND '00' = MCR1.CUST_CD_S                                                                        " & vbNewLine _
                         & "AND MCR1.PTN_ID = '29'                                                                           " & vbNewLine _
                         & "--帳票パターン取得                                                                               " & vbNewLine _
                         & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                    " & vbNewLine _
                         & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                               " & vbNewLine _
                         & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                     " & vbNewLine _
                         & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                     " & vbNewLine _
                         & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                         & "--商品Mの荷主での荷主帳票パターン取得                                                            " & vbNewLine _
                         & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                              " & vbNewLine _
                         & "ON  MGR.NRS_BR_CD = MCR2.NRS_BR_CD                                                               " & vbNewLine _
                         & "AND MGR.CUST_CD_L = MCR2.CUST_CD_L                                                               " & vbNewLine _
                         & "AND MGR.CUST_CD_M = MCR2.CUST_CD_M                                                               " & vbNewLine _
                         & "AND MGR.CUST_CD_S = MCR2.CUST_CD_S                                                               " & vbNewLine _
                         & "AND MCR2.PTN_ID = '29'                                                                           " & vbNewLine _
                         & "--帳票パターン取得                                                                               " & vbNewLine _
                         & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                    " & vbNewLine _
                         & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                               " & vbNewLine _
                         & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                     " & vbNewLine _
                         & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                     " & vbNewLine _
                         & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                         & "--存在しない場合の帳票パターン取得                                                               " & vbNewLine _
                         & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                    " & vbNewLine _
                         & "ON  MR3.NRS_BR_CD = @NRS_BR_CD                                                                   " & vbNewLine _
                         & "AND MR3.PTN_ID = '29'                                                                            " & vbNewLine _
                         & "AND MR3.STANDARD_FLAG = '01'                                                                     " & vbNewLine _
                         & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                         & "WHERE                                                                                            " & vbNewLine _
                         & "HIKAKU_ZAI_NB - PORA_ZAI_NB <> EXTRA_ZAI_NB                                                      " & vbNewLine _
                         & "ORDER BY                                                                                         " & vbNewLine _
                         & " NRS_BR_CD                                                                                       " & vbNewLine _
                         & ",CUST_CD_L                                                                                       " & vbNewLine _
                         & ",CUST_CD_M                                                                                       " & vbNewLine _
                         & ",GOODS_CD_NRS                                                                                    " & vbNewLine _
                         & ",LOT_NO                                                                                          " & vbNewLine _
                         & ",ZAI_REC_NO                                                                                      " & vbNewLine _
                         & ",SERIAL_NO                                                                                       " & vbNewLine

    End Sub

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
        Dim inTbl As DataTable = ds.Tables("LMD560IN")
        '荷主コード取得
        Dim custCdL As String = inTbl.Rows(0).Item("CUST_CD_L").ToString
        Dim custCdM As String = inTbl.Rows(0).Item("CUST_CD_M").ToString

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Call Me.CreateSqlMprt(custCdL, custCdM)           'SQL作成
        Me._StrSql.Append(SQL_MPrt)                       'SQL構築
        Call Me.SetConditionMasterSQLMprt()               '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'システム用のため、タイムアウト無期限で設定
        cmd.CommandTimeout = 0

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD560DAC", "SelectMPrt", cmd)

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
    ''' 出荷指示書出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷指示書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD560IN")
        '荷主コード取得
        Dim custCdL As String = inTbl.Rows(0).Item("CUST_CD_L").ToString
        Dim custCdM As String = inTbl.Rows(0).Item("CUST_CD_M").ToString
        '印刷種別取得
        Dim Ptn As String = inTbl.Rows(0).Item("PTN").ToString

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Select Case Ptn

            Case "1" '在庫数
                Call Me.CreateSqlZaiNb(custCdL, custCdM)           'SQL作成
                Me._StrSql.Append(SQL_ZAI_NB)                      'SQL構築
                Call Me.SetConditionMasterSQL()                    '条件設定

            Case "2" '出荷予定
                Call Me.CreateSqlAlctdNb(custCdL, custCdM)         'SQL作成
                Me._StrSql.Append(SQL_ALCTD_NB)                    'SQL構築
                Call Me.SetConditionMasterSQL()                    '条件設定

            Case "3" '引当数
                Call Me.CreateSqlAllocCanNb(custCdL, custCdM)      'SQL作成
                Me._StrSql.Append(SQL_ALLOC_CAN_NB)                'SQL構築
                Call Me.SetConditionMasterSQL()                    '条件設定

            Case Else 'その他（とりあえず在庫数を出力）
                Call Me.CreateSqlZaiNb(custCdL, custCdM)           'SQL作成
                Me._StrSql.Append(SQL_ZAI_NB)                      'SQL構築
                Call Me.SetConditionMasterSQL()                    '条件設定

        End Select

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'システム用のため、タイムアウト無期限で設定
        cmd.CommandTimeout = 0

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD560DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NM", "NM")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("PTN", "PTN")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("HIKAKU_ZAI_NB", "HIKAKU_ZAI_NB")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("EXTRA_ZAI_NB", "EXTRA_ZAI_NB")
        map.Add("HIKAKU_ZAI_QT", "HIKAKU_ZAI_QT")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("EXTRA_ZAI_QT", "EXTRA_ZAI_QT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD560OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQLMprt()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            If String.IsNullOrEmpty(.Item("CUST_CD_L").ToString()) = False OrElse _
               String.IsNullOrEmpty(.Item("CUST_CD_M").ToString()) = False Then

                '荷主コード大
                whereStr = .Item("CUST_CD_L").ToString()
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

                '荷主コード中
                whereStr = .Item("CUST_CD_M").ToString()
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))

            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '印刷種別
            whereStr = .Item("PTN").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN", whereStr, DBDataType.CHAR))

            If String.IsNullOrEmpty(.Item("CUST_CD_L").ToString()) = False OrElse _
               String.IsNullOrEmpty(.Item("CUST_CD_M").ToString()) = False Then

                '荷主コード大
                whereStr = .Item("CUST_CD_L").ToString()
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

                '荷主コード中
                whereStr = .Item("CUST_CD_M").ToString()
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))

            End If

            '出力日付
            whereStr = .Item("OUTPUT_DATE").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTPUT_DATE", whereStr, DBDataType.CHAR))

            '在庫履歴日
            whereStr = .Item("RIREKI_DATE").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RIREKI_DATE", whereStr, DBDataType.CHAR))

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
