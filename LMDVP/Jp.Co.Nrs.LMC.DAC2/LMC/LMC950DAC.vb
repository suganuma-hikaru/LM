' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC950    : 新潟運輸CSV出力
'  作  成  者       :  []
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC950DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC950DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 新潟運輸CSV作成データ検索用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_NIIGATA_UNYU_CSV As String _
            = " SELECT                                                                             " & vbNewLine _
            & "       OUTL.NRS_BR_CD                                            AS NRS_BR_CD       " & vbNewLine _
            & "     , OUTL.OUTKA_NO_L                                           AS OUTKA_NO_L      " & vbNewLine _
            & "     , OUTL.OUTKA_PLAN_DATE                                      AS OUTKA_PLAN_DATE " & vbNewLine _
            & "     , CASE                                                                         " & vbNewLine _
            & "         WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                 " & vbNewLine _
            & "         WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                 " & vbNewLine _
            & "         ELSE                          DST.DEST_NM                                  " & vbNewLine _
            & "       END                                                       AS DEST_NM_1       " & vbNewLine _
            & "     , OUTL.DEST_TEL                                             AS DEST_TEL        " & vbNewLine _
            & "     , CASE                                                                         " & vbNewLine _
            & "         WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_ZIP                                " & vbNewLine _
            & "         ELSE                          DST.ZIP                                      " & vbNewLine _
            & "       END                                                       AS DEST_ZIP        " & vbNewLine _
            & "     , CASE                                                                         " & vbNewLine _
            & "         WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                               " & vbNewLine _
            & "         WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                               " & vbNewLine _
            & "         ELSE                          DST.AD_1                                     " & vbNewLine _
            & "       END                                                       AS DEST_AD_1       " & vbNewLine _
            & "     , CASE                                                                         " & vbNewLine _
            & "         WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                               " & vbNewLine _
            & "         WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                               " & vbNewLine _
            & "         ELSE                          DST.AD_2                                     " & vbNewLine _
            & "       END                                                       AS DEST_AD_2       " & vbNewLine _
            & "     , OUTL.DEST_AD_3                                            AS DEST_AD_3       " & vbNewLine _
            & "     , OUTL.ARR_PLAN_DATE                                        AS ARR_PLAN_DATE   " & vbNewLine _
            & "     , CASE                                                                         " & vbNewLine _
            & "         WHEN    SHIPDST.DEST_NM <> ''                                              " & vbNewLine _
            & "             AND SHIPDST.DEST_NM IS NOT NULL THEN SHIPDST.DEST_NM                   " & vbNewLine _
            & "         WHEN    CST.DENPYO_NM   <> ''                                              " & vbNewLine _
            & "             AND CST.DENPYO_NM IS NOT NULL   THEN CST.DENPYO_NM                     " & vbNewLine _
            & "         WHEN    MGD2.SET_NAIYO <> ''                                               " & vbNewLine _
            & "             AND MGD2.SET_NAIYO IS NOT NULL  THEN MGD2.SET_NAIYO                    " & vbNewLine _
            & "         ELSE                                     CST.CUST_NM_L                     " & vbNewLine _
            & "       END                                                       AS ATSUKAISYA_NM   " & vbNewLine _
            & "     , CASE                                                                         " & vbNewLine _
            & "         WHEN    OUTL.NRS_BR_CD = '40'                                              " & vbNewLine _
            & "             AND OUTL.CUST_CD_L = '00004'                                           " & vbNewLine _
            & "             AND MCR1.NRS_BR_CD = '40'                                              " & vbNewLine _
            & "             AND MCR1.UNSOCO_CD = '005'                                             " & vbNewLine _
            & "         THEN '香料'                                                                " & vbNewLine _
            & "         ELSE MG.GOODS_NM_1                                                         " & vbNewLine _
            & "      END                                                        AS GOODS_NM_1      " & vbNewLine _
            & "     , OUTM.IRIME                                                AS IRIME           " & vbNewLine _
            & "     , OUTM.IRIME_UT                                             AS IRIME_UT        " & vbNewLine _
            & "     , KB02.KBN_NM1                                              AS ARR_PLAN_TIME   " & vbNewLine _
            & "     , FL.REMARK                                                 AS UNSO_L_REMARK   " & vbNewLine _
            & "     , OUTL.CUST_ORD_NO                                          AS CUST_ORD_NO     " & vbNewLine _
            & "     , OUTL.BUYER_ORD_NO                                         AS BUYER_ORD_NO    " & vbNewLine _
            & "     , OUTL.OUTKA_PKG_NB                                         AS OUTKA_PKG_NB    " & vbNewLine _
            & "     ,(SELECT                                                                       " & vbNewLine _
            & "         SUM (UNSO_WT.UNSO_WT)                                                      " & vbNewLine _
            & "       FROM                                                                         " & vbNewLine _
            & "        (SELECT                                                                     " & vbNewLine _
            & "             SUM(OUTS.ALCTD_QT * MG.STD_WT_KGS / MG.STD_IRIME_NB) AS UNSO_WT        " & vbNewLine _
            & "         FROM                                                                       " & vbNewLine _
            & "             $LM_TRN$..C_OUTKA_L OUTL                                               " & vbNewLine _
            & "         LEFT JOIN                                                                  " & vbNewLine _
            & "             $LM_TRN$..C_OUTKA_M OUTM                                               " & vbNewLine _
            & "                 ON  OUTL.NRS_BR_CD   = OUTM.NRS_BR_CD                              " & vbNewLine _
            & "                 AND OUTL.OUTKA_NO_L  = OUTM.OUTKA_NO_L                             " & vbNewLine _
            & "                 AND OUTM.SYS_DEL_FLG = '0'                                         " & vbNewLine _
            & "         LEFT JOIN                                                                  " & vbNewLine _
            & "             $LM_TRN$..C_OUTKA_S OUTS                                               " & vbNewLine _
            & "                 ON  OUTL.NRS_BR_CD   = OUTS.NRS_BR_CD                              " & vbNewLine _
            & "                 AND OUTL.OUTKA_NO_L  = OUTS.OUTKA_NO_L                             " & vbNewLine _
            & "                 AND OUTM.OUTKA_NO_M  = OUTS.OUTKA_NO_M                             " & vbNewLine _
            & "                 AND OUTS.SYS_DEL_FLG = '0'                                         " & vbNewLine _
            & "         LEFT JOIN                                                                  " & vbNewLine _
            & "             $LM_MST$..M_GOODS MG                                                   " & vbNewLine _
            & "                 ON  OUTM.NRS_BR_CD   = MG.NRS_BR_CD                                " & vbNewLine _
            & "                 AND OUTM.GOODS_CD_NRS  = MG.GOODS_CD_NRS                           " & vbNewLine _
            & "         WHERE                                                                      " & vbNewLine _
            & "             OUTL.SYS_DEL_FLG  = '0'                                                " & vbNewLine _
            & "         AND OUTL.NRS_BR_CD    = @NRS_BR_CD                                         " & vbNewLine _
            & "         AND OUTL.OUTKA_NO_L   = @OUTKA_NO_L                                        " & vbNewLine _
            & "         GROUP BY                                                                   " & vbNewLine _
            & "               OUTS.BETU_WT                                                         " & vbNewLine _
            & "             , OUTS.ALCTD_NB                                                        " & vbNewLine _
            & "             , OUTL.NRS_BR_CD                                                       " & vbNewLine _
            & "             , OUTL.OUTKA_NO_L                                                      " & vbNewLine _
            & "         )  UNSO_WT                                                                 " & vbNewLine _
            & "     )                                                           AS UNSO_WT         " & vbNewLine _
            & "     , @ROW_NO                                                   AS ROW_NO          " & vbNewLine _
            & "     , OUTL.SYS_UPD_DATE                                         AS SYS_UPD_DATE    " & vbNewLine _
            & "     , OUTL.SYS_UPD_TIME                                         AS SYS_UPD_TIME    " & vbNewLine _
            & "     , @FILEPATH                                                 AS FILEPATH        " & vbNewLine _
            & "     , @FILENAME                                                 AS FILENAME        " & vbNewLine _
            & "     , @SYS_DATE                                                 AS SYS_DATE        " & vbNewLine _
            & "     , @SYS_TIME                                                 AS SYS_TIME        " & vbNewLine _
            & " FROM                                                                               " & vbNewLine _
            & "     $LM_TRN$..C_OUTKA_L OUTL                                                       " & vbNewLine _
            & " -- 出荷M                                                                           " & vbNewLine _
            & " LEFT JOIN                                                                          " & vbNewLine _
            & "    (SELECT *                                                                       " & vbNewLine _
            & "     FROM                                                                           " & vbNewLine _
            & "        (SELECT                                                                     " & vbNewLine _
            & "               NRS_BR_CD                                                            " & vbNewLine _
            & "             , GOODS_CD_NRS                                                         " & vbNewLine _
            & "             , IRIME                                                                " & vbNewLine _
            & "             , IRIME_UT                                                             " & vbNewLine _
            & "             , ALCTD_NB                                                             " & vbNewLine _
            & "             , ALCTD_QT                                                             " & vbNewLine _
            & "             , LOT_NO                                                               " & vbNewLine _
            & "             , SYS_DEL_FLG                                                          " & vbNewLine _
            & "             , CUST_ORD_NO_DTL                                                      " & vbNewLine _
            & "             , ROW_NUMBER() OVER (                                                  " & vbNewLine _
            & "                 PARTITION BY                                                       " & vbNewLine _
            & "                     NRS_BR_CD                                                      " & vbNewLine _
            & "                 ORDER BY                                                           " & vbNewLine _
            & "                     OUTKA_NO_L,OUTKA_NO_M                                          " & vbNewLine _
            & "                 ) AS NUM                                                           " & vbNewLine _
            & "             , OUTKA_NO_L                                                           " & vbNewLine _
            & "             , OUTKA_NO_M                                                           " & vbNewLine _
            & "             FROM                                                                   " & vbNewLine _
            & "                 $LM_TRN$..C_OUTKA_M M                                              " & vbNewLine _
            & "             WHERE                                                                  " & vbNewLine _
            & "                 M.NRS_BR_CD = @NRS_BR_CD                                           " & vbNewLine _
            & "             AND M.OUTKA_NO_L IN (@OUTKA_NO_L)                                      " & vbNewLine _
            & "             AND SYS_DEL_FLG = '0'                                                  " & vbNewLine _
            & "         ) AS BASE                                                                  " & vbNewLine _
            & "     WHERE                                                                          " & vbNewLine _
            & "         BASE.NUM = 1                                                               " & vbNewLine _
            & "     ) OUTM                                                                         " & vbNewLine _
            & "         ON  1 = 1                                                                  " & vbNewLine _
            & " -- 出荷M(中MIN)                                                                    " & vbNewLine _
            & " LEFT OUTER JOIN                                                                    " & vbNewLine _
            & "    (SELECT                                                                         " & vbNewLine _
            & "           NRS_BR_CD                                                                " & vbNewLine _
            & "         , OUTKA_NO_L                                                               " & vbNewLine _
            & "         , MIN(OUTKA_NO_M) AS  OUTKA_NO_M                                           " & vbNewLine _
            & "     FROM                                                                           " & vbNewLine _
            & "         $LM_TRN$..C_OUTKA_M                                                        " & vbNewLine _
            & "     WHERE                                                                          " & vbNewLine _
            & "         SYS_DEL_FLG ='0'                                                           " & vbNewLine _
            & "     AND NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
            & "     AND OUTKA_NO_L = @OUTKA_NO_L                                                   " & vbNewLine _
            & "     GROUP BY                                                                       " & vbNewLine _
            & "         NRS_BR_CD,OUTKA_NO_L                                                       " & vbNewLine _
            & "     ) OUTM_MIN                                                                     " & vbNewLine _
            & "     ON  OUTM_MIN.NRS_BR_CD  = OUTL.NRS_BR_CD                                       " & vbNewLine _
            & "     AND OUTM_MIN.OUTKA_NO_L = OUTL.OUTKA_NO_L                                      " & vbNewLine _
            & " -- 出荷M(中MIN)                                                                    " & vbNewLine _
            & " LEFT JOIN                                                                          " & vbNewLine _
            & "     $LM_TRN$..C_OUTKA_M OUTM2                                                      " & vbNewLine _
            & "         ON  OUTM2.NRS_BR_CD   = OUTL.NRS_BR_CD                                     " & vbNewLine _
            & "         AND OUTM2.OUTKA_NO_L  = OUTL.OUTKA_NO_L                                    " & vbNewLine _
            & "         AND OUTM2.OUTKA_NO_M  = OUTM_MIN.OUTKA_NO_M                                " & vbNewLine _
            & "         AND OUTM2.SYS_DEL_FLG = '0'                                                " & vbNewLine _
            & " -- EDI出荷L (EDI管理番号L 順の先頭の出荷管理番号L)                                 " & vbNewLine _
            & " LEFT JOIN (                                                                        " & vbNewLine _
            & "     SELECT                                                                         " & vbNewLine _
            & "            NRS_BR_CD                                                               " & vbNewLine _
            & "          , EDI_CTL_NO                                                              " & vbNewLine _
            & "          , OUTKA_CTL_NO                                                            " & vbNewLine _
            & "      FROM (                                                                        " & vbNewLine _
            & "         SELECT                                                                     " & vbNewLine _
            & "               EDIOUTL.NRS_BR_CD                                                    " & vbNewLine _
            & "             , EDIOUTL.EDI_CTL_NO                                                   " & vbNewLine _
            & "             , EDIOUTL.OUTKA_CTL_NO                                                 " & vbNewLine _
            & "             , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                           " & vbNewLine _
            & "               ELSE ROW_NUMBER() OVER (                                             " & vbNewLine _
            & "                     PARTITION BY                                                   " & vbNewLine _
            & "                           EDIOUTL.NRS_BR_CD                                        " & vbNewLine _
            & "                         , EDIOUTL.OUTKA_CTL_NO                                     " & vbNewLine _
            & "                     ORDER BY                                                       " & vbNewLine _
            & "                           EDIOUTL.NRS_BR_CD                                        " & vbNewLine _
            & "                         , EDIOUTL.EDI_CTL_NO                                       " & vbNewLine _
            & "                     )                                                              " & vbNewLine _
            & "               END AS IDX                                                           " & vbNewLine _
            & "         FROM                                                                       " & vbNewLine _
            & "             $LM_TRN$..H_OUTKAEDI_L EDIOUTL                                         " & vbNewLine _
            & "         WHERE                                                                      " & vbNewLine _
            & "             EDIOUTL.SYS_DEL_FLG = '0'                                              " & vbNewLine _
            & "         AND EDIOUTL.NRS_BR_CD   = @NRS_BR_CD                                       " & vbNewLine _
            & "         AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L                                     " & vbNewLine _
            & "         ) EBASE                                                                    " & vbNewLine _
            & "     WHERE                                                                          " & vbNewLine _
            & "         EBASE.IDX = 1                                                              " & vbNewLine _
            & "     ) TOPEDI                                                                       " & vbNewLine _
            & "         ON  TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                   " & vbNewLine _
            & "         AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                  " & vbNewLine _
            & " -- EDI出荷L                                                                        " & vbNewLine _
            & " LEFT JOIN                                                                          " & vbNewLine _
            & "     $LM_TRN$..H_OUTKAEDI_L EDIL                                                    " & vbNewLine _
            & "         ON  EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                     " & vbNewLine _
            & "         AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                    " & vbNewLine _
            & " -- 運送L                                                                           " & vbNewLine _
            & " LEFT JOIN                                                                          " & vbNewLine _
            & "     $LM_TRN$..F_UNSO_L FL                                                          " & vbNewLine _
            & "         ON  FL.MOTO_DATA_KB   = '20'                                               " & vbNewLine _
            & "         AND FL.NRS_BR_CD      = OUTL.NRS_BR_CD                                     " & vbNewLine _
            & "         AND FL.INOUTKA_NO_L   = OUTL.OUTKA_NO_L                                    " & vbNewLine _
            & "         AND FL.SYS_DEL_FLG    = '0'                                                " & vbNewLine _
            & " -- 届先マスタ                                                                      " & vbNewLine _
            & " LEFT JOIN                                                                          " & vbNewLine _
            & "     $LM_MST$..M_DEST DST                                                           " & vbNewLine _
            & "         ON  DST.NRS_BR_CD   = OUTL.NRS_BR_CD                                       " & vbNewLine _
            & "         AND DST.CUST_CD_L   = OUTL.CUST_CD_L                                       " & vbNewLine _
            & "         AND DST.DEST_CD     = OUTL.DEST_CD                                         " & vbNewLine _
            & "         AND DST.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
            & " -- 届先マスタ                                                                      " & vbNewLine _
            & " LEFT JOIN                                                                          " & vbNewLine _
            & "     $LM_MST$..M_DEST SHIPDST                                                       " & vbNewLine _
            & "         ON  SHIPDST.NRS_BR_CD   = OUTL.NRS_BR_CD                                   " & vbNewLine _
            & "         AND SHIPDST.CUST_CD_L   = OUTL.CUST_CD_L                                   " & vbNewLine _
            & "         AND SHIPDST.DEST_CD     = OUTL.SHIP_CD_L                                   " & vbNewLine _
            & "         AND SHIPDST.SYS_DEL_FLG = '0'                                              " & vbNewLine _
            & " -- 商品マスタ                                                                      " & vbNewLine _
            & " LEFT JOIN                                                                          " & vbNewLine _
            & "     $LM_MST$..M_GOODS MG                                                           " & vbNewLine _
            & "         ON  MG.NRS_BR_CD     = OUTM.NRS_BR_CD                                      " & vbNewLine _
            & "         AND MG.GOODS_CD_NRS  = OUTM.GOODS_CD_NRS                                   " & vbNewLine _
            & "         AND MG.SYS_DEL_FLG   = '0'                                                 " & vbNewLine _
            & " -- 商品明細マスタ                                                                  " & vbNewLine _
            & " LEFT JOIN                                                                          " & vbNewLine _
            & "     $LM_MST$..M_GOODS_DETAILS  MGD2                                                " & vbNewLine _
            & "         ON  MGD2.SUB_KB       = '73'                                               " & vbNewLine _
            & "         AND MGD2.SYS_DEL_FLG  = '0'                                                " & vbNewLine _
            & "         AND MGD2.NRS_BR_CD    = OUTM2.NRS_BR_CD                                    " & vbNewLine _
            & "         AND MGD2.GOODS_CD_NRS = OUTM2.GOODS_CD_NRS                                 " & vbNewLine _
            & " -- 荷主マスタ                                                                      " & vbNewLine _
            & " LEFT JOIN                                                                          " & vbNewLine _
            & "     $LM_MST$..M_CUST CST                                                           " & vbNewLine _
            & "         ON  CST.NRS_BR_CD   = MG.NRS_BR_CD                                         " & vbNewLine _
            & "         AND CST.CUST_CD_L   = MG.CUST_CD_L                                         " & vbNewLine _
            & "         AND CST.CUST_CD_M   = MG.CUST_CD_M                                         " & vbNewLine _
            & "         AND CST.CUST_CD_S   = MG.CUST_CD_S                                         " & vbNewLine _
            & "         AND CST.CUST_CD_SS  = MG.CUST_CD_SS                                        " & vbNewLine _
            & "         AND CST.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
            & " -- 荷主なしでの運送会社荷主別送り状マスタ                                          " & vbNewLine _
            & " LEFT JOIN                                                                          " & vbNewLine _
            & "     $LM_MST$..M_UNSO_CUST_RPT MCR1                                                 " & vbNewLine _
            & "         ON  MCR1.NRS_BR_CD     = FL.NRS_BR_CD                                      " & vbNewLine _
            & "         AND MCR1.UNSOCO_CD     = FL.UNSO_CD                                        " & vbNewLine _
            & "         AND MCR1.UNSOCO_BR_CD  = FL.UNSO_BR_CD                                     " & vbNewLine _
            & "         AND MCR1.PTN_ID        = '11'                                              " & vbNewLine _
            & "         AND MCR1.MOTO_TYAKU_KB = FL.PC_KB                                          " & vbNewLine _
            & "         AND MCR1.CUST_CD_L     = ''                                                " & vbNewLine _
            & "         AND MCR1.CUST_CD_M     = ''                                                " & vbNewLine _
            & "         AND MCR1.SYS_DEL_FLG   = '0'                                               " & vbNewLine _
            & " -- 区分マスタ(納期文言)                                                            " & vbNewLine _
            & " LEFT JOIN                                                                          " & vbNewLine _
            & "     $LM_MST$..Z_KBN KB02                                                           " & vbNewLine _
            & "         ON  KB02.KBN_GROUP_CD = 'N010'                                             " & vbNewLine _
            & "         AND KB02.KBN_CD       = OUTL.ARR_PLAN_TIME                                 " & vbNewLine _
            & "         AND KB02.SYS_DEL_FLG  = '0'                                                " & vbNewLine _
            & " WHERE                                                                              " & vbNewLine _
            & "     OUTL.SYS_DEL_FLG    = '0'                                                      " & vbNewLine _
            & " AND OUTL.NRS_BR_CD      = @NRS_BR_CD                                               " & vbNewLine _
            & " AND OUTL.OUTKA_NO_L IN (@OUTKA_NO_L)                                               " & vbNewLine

#End Region

#Region "更新 SQL"

    Private Const SQL_UPDATE_NIIGATA_UNYU_CSV As String _
            = "UPDATE $LM_TRN$..C_OUTKA_L SET       " & vbNewLine _
            & " DENP_FLAG         = '01'            " & vbNewLine _
            & ",SYS_UPD_DATE      = @SYS_UPD_DATE   " & vbNewLine _
            & ",SYS_UPD_TIME      = @SYS_UPD_TIME   " & vbNewLine _
            & ",SYS_UPD_PGID      = @SYS_UPD_PGID   " & vbNewLine _
            & ",SYS_UPD_USER      = @SYS_UPD_USER   " & vbNewLine _
            & "WHERE NRS_BR_CD    = @NRS_BR_CD      " & vbNewLine _
            & "  AND OUTKA_NO_L   = @OUTKA_NO_L     " & vbNewLine

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
    ''' 新潟運輸CSV作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectNiigataUnyuCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC950IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC950DAC.SQL_SELECT_NIIGATA_UNYU_CSV)

        '条件設定
        Call setSQLSelect()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row("ROW_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILEPATH", Me._Row("FILEPATH"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILENAME", Me._Row("FILENAME"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", Me._Row("SYS_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_TIME", Me._Row("SYS_TIME"), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC950DAC", "SelectNiigataUnyuCsv", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("DEST_NM_1", "DEST_NM_1")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ATSUKAISYA_NM", "ATSUKAISYA_NM")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("UNSO_L_REMARK", "UNSO_L_REMARK")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("FILEPATH", "FILEPATH")
        map.Add("FILENAME", "FILENAME")
        map.Add("SYS_DATE", "SYS_DATE")
        map.Add("SYS_TIME", "SYS_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC950OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC950OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷Lテーブル更新（新潟運輸CSV作成時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateNiigataUnyuCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC950OUT").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC950DAC.SQL_UPDATE_NIIGATA_UNYU_CSV, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC950DAC", "UpdateNiigataUnyuCsv", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

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

    ''' <summary>
    '''  パラメータ設定モジュール（出荷検索）
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub setSQLSelect()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUp()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="setFlg">セットフラグ False:通常のメッセージセット True:一括更新のメッセージセット</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand, Optional ByVal setFlg As Boolean = False) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd), setFlg)

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="setFlg">セットフラグ False:通常のメッセージセット True:一括更新のメッセージセット</param>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer, Optional ByVal setFlg As Boolean = False) As Boolean

        '判定
        If cnt < 1 Then
            If setFlg = False Then
                MyBase.SetMessage("E011")
            Else
                MyBase.SetMessageStore("00", "E011", , Me._Row.Item("ROW_NO").ToString())
            End If
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

#End Region

End Class
