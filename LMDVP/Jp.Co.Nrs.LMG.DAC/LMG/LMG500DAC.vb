' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求
'  プログラムID     :  LMG500    : 保管料・荷役料請求明細印刷
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMG500DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG500DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                           " & vbNewLine _
                                            & "	SE.NRS_BR_CD                                             AS NRS_BR_CD    " & vbNewLine _
                                            & ",'52'                                                     AS PTN_ID       " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                         " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                        " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD       " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                         " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                    " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID       " & vbNewLine
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks>
    ''' 20160819 tsunehira
    ''' $LM_TRN$と千葉のみだったものを修正
    ''' </remarks>
    Private Const SQL_SELECT_JOB As String = " SELECT DISTINCT                                                           " & vbNewLine _
                                            & " SE.JOB_NO                                                AS JOB_NO       " & vbNewLine _
                                            & " FROM $LM_TRN$..G_SEKY_MEISAI_PRT SE                                      " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_GOODS MG                                           " & vbNewLine _
                                            & " ON SE.NRS_BR_CD = MG.NRS_BR_CD                                           " & vbNewLine _
                                            & " AND SE.GOODS_CD_NRS = MG.GOODS_CD_NRS                                    " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_CUST MC                                            " & vbNewLine _
                                            & " ON SE.NRS_BR_CD = MC.NRS_BR_CD                                           " & vbNewLine _
                                            & " AND MG.CUST_CD_L = MC.CUST_CD_L                                          " & vbNewLine _
                                            & " AND MG.CUST_CD_M = MC.CUST_CD_M                                          " & vbNewLine _
                                            & " AND MG.CUST_CD_S = MC.CUST_CD_S                                          " & vbNewLine _
                                            & " WHERE                                                                    " & vbNewLine _
                                            & "     SE.SYS_DEL_FLG = '0'                                                 " & vbNewLine
                                          
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks>
    '''   2011/10/17 修正 須賀
    '''      商品コード取得時、削除フラグを抽出条件から除外
    '''</remarks>
    Private Const SQL_FROM_MPrt As String = "FROM                                                                         " & vbNewLine _
                                          & "   $LM_TRN$..G_SEKY_MEISAI_PRT SE                                            " & vbNewLine _
                                          & "   LEFT JOIN $LM_MST$..M_GOODS MG                                            " & vbNewLine _
                                          & "   ON  MG.NRS_BR_CD = SE.NRS_BR_CD                                           " & vbNewLine _
                                          & "   AND MG.GOODS_CD_NRS = SE.GOODS_CD_NRS                                     " & vbNewLine _
                                          & "   LEFT JOIN $LM_MST$..M_CUST MC                                             " & vbNewLine _
                                          & "   ON  MC.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                          & "   AND MC.NRS_BR_CD = MG.NRS_BR_CD                                           " & vbNewLine _
                                          & "   AND MC.CUST_CD_L = MG.CUST_CD_L                                           " & vbNewLine _
                                          & "   AND MC.CUST_CD_M = MG.CUST_CD_M                                           " & vbNewLine _
                                          & "   AND MC.CUST_CD_S = MG.CUST_CD_S                                           " & vbNewLine _
                                          & "   AND MC.CUST_CD_SS = MG.CUST_CD_SS                                         " & vbNewLine _
                                          & "   LEFT JOIN $LM_MST$..M_SEIQTO MS                                           " & vbNewLine _
                                          & "   ON  MS.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                          & "   AND MS.NRS_BR_CD = SE.NRS_BR_CD                                           " & vbNewLine _
                                          & "   AND MS.SEIQTO_CD = MC.HOKAN_SEIQTO_CD                                     " & vbNewLine _
                                          & "   --荷主帳票パターン取得                                                    " & vbNewLine _
                                          & "   LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                       " & vbNewLine _
                                          & "   ON  SE.NRS_BR_CD = MCR1.NRS_BR_CD                                         " & vbNewLine _
                                          & "   AND MG.CUST_CD_L = MCR1.CUST_CD_L                                         " & vbNewLine _
                                          & "   AND MG.CUST_CD_M = MCR1.CUST_CD_M                                         " & vbNewLine _
                                          & "   AND '00' = MCR1.CUST_CD_S                                                 " & vbNewLine _
                                          & "   AND MCR1.PTN_ID = '52'                                                    " & vbNewLine _
                                          & "   --帳票パターン取得                                                        " & vbNewLine _
                                          & "   LEFT JOIN $LM_MST$..M_RPT MR1                                             " & vbNewLine _
                                          & "   ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                        " & vbNewLine _
                                          & "   AND MR1.PTN_ID = MCR1.PTN_ID                                              " & vbNewLine _
                                          & "   AND MR1.PTN_CD = MCR1.PTN_CD                                              " & vbNewLine _
                                          & "   AND MR1.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                          & "   --商品Mの荷主での荷主帳票パターン取得                                     " & vbNewLine _
                                          & "   LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                       " & vbNewLine _
                                          & "   ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                         " & vbNewLine _
                                          & "   AND MG.CUST_CD_L = MCR2.CUST_CD_L                                         " & vbNewLine _
                                          & "   AND MG.CUST_CD_M = MCR2.CUST_CD_M                                         " & vbNewLine _
                                          & "   AND MG.CUST_CD_S = MCR2.CUST_CD_S                                         " & vbNewLine _
                                          & "   AND MCR2.PTN_ID = '52'                                                    " & vbNewLine _
                                          & "   --帳票パターン取得                                                        " & vbNewLine _
                                          & "   LEFT JOIN $LM_MST$..M_RPT MR2                                             " & vbNewLine _
                                          & "   ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                        " & vbNewLine _
                                          & "   AND MR2.PTN_ID = MCR2.PTN_ID                                              " & vbNewLine _
                                          & "   AND MR2.PTN_CD = MCR2.PTN_CD                                              " & vbNewLine _
                                          & "   AND MR2.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                          & "   --存在しない場合の帳票パターン取得                                        " & vbNewLine _
                                          & "   LEFT JOIN $LM_MST$..M_RPT MR3                                             " & vbNewLine _
                                          & "   ON  MR3.NRS_BR_CD = SE.NRS_BR_CD                                          " & vbNewLine _
                                          & "   AND MR3.PTN_ID = '52'                                                     " & vbNewLine _
                                          & "   AND MR3.STANDARD_FLAG = '01'                                              " & vbNewLine _
                                          & "   AND MR3.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                          & "   WHERE                                                                     " & vbNewLine _
                                          & "       SE.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                          & "   AND SE.SEKY_FLG = @SEKY_FLG                                               " & vbNewLine _
                                          & "   AND SE.SYS_ENT_PGID <> 'IKOU '                                            " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT句(MAIN)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAIN As String = "SELECT                                                             " & vbNewLine _
                                            & " RPT_ID                              AS RPT_ID                     " & vbNewLine _
                                            & ",NRS_BR_CD                           AS NRS_BR_CD                  " & vbNewLine _
                                            & ",SEIQTO_CD                           AS SEIQTO_CD                  " & vbNewLine _
                                            & ",SEIQTO_NM                           AS SEIQTO_NM                  " & vbNewLine _
                                            & ",MAIN.CUST_CD_L                      AS CUST_CD_L                  " & vbNewLine _
                                            & ",MAIN.CUST_CD_M                      AS CUST_CD_M                  " & vbNewLine _
                                            & ",CUST_CD_S                           AS CUST_CD_S                  " & vbNewLine _
                                            & ",CUST_CD_SS                          AS CUST_CD_SS                 " & vbNewLine _
                                            & ",CUST_NM_L                           AS CUST_NM_L                  " & vbNewLine _
                                            & ",CUST_NM_M                           AS CUST_NM_M                  " & vbNewLine _
                                            & ",CUST_NM_S                           AS CUST_NM_S                  " & vbNewLine _
                                            & ",CUST_NM_SS                          AS CUST_NM_SS                 " & vbNewLine _
                                            & ",SEARCH_KEY_1                        AS SEARCH_KEY_1               " & vbNewLine _
                                            & ",''                                  AS SEARCH_KEY_2               " & vbNewLine _
                                            & ",JOB_NO                              AS JOB_NO                     " & vbNewLine _
                                            & ",NRS_BR_NM                           AS NRS_BR_NM                  " & vbNewLine _
                                            & ",INV_DATE_FROM                       AS INV_DATE_FROM              " & vbNewLine _
                                            & ",INV_DATE_TO                         AS INV_DATE_TO                " & vbNewLine _
                                            & ",OFB_KB                              AS OFB_KB                     " & vbNewLine _
                                            & ",ISNULL(OFB_KB_NM,'')                AS OFB_KB_NM                  " & vbNewLine _
                                            & ",GOODS_NM_1                          AS GOODS_NM_1                 " & vbNewLine _
                                            & ",''                                  AS PKG_UT                     " & vbNewLine _
                                            & ",GOODS_CD_CUST                       AS GOODS_CD_CUST              " & vbNewLine _
                                            & ",SERIAL_NO                           AS SERIAL_NO                  " & vbNewLine _
                                            & ",LOT_NO                              AS LOT_NO                     " & vbNewLine _
                                            & ",NB_UT_NM                            AS NB_UT_NM                   " & vbNewLine _
                                            & ",IRIME_UT_NM                         AS IRIME_UT_NM                " & vbNewLine _
                                            & ",IRIME                               AS IRIME                      " & vbNewLine _
                                            & ",INKA_NO_L                           AS INKA_NO_L                  " & vbNewLine _
                                            & ",KISYZAN_NB1                         AS KISYZAN_NB1                " & vbNewLine _
                                            & ",KISYZAN_NB2                         AS KISYZAN_NB2                " & vbNewLine _
                                            & ",KISYZAN_NB3                         AS KISYZAN_NB3                " & vbNewLine _
                                            & ",MATUZAN_NB                          AS MATUZAN_NB                 " & vbNewLine _
                                            & ",SUM(INKO_NB_TTL2)                   AS INKO_NB_TTL2               " & vbNewLine _
                                            & ",SUM(INKO_NB_TTL1)                   AS INKO_NB_TTL1               " & vbNewLine _
                                            & ",SUM(OUTKO_NB_TTL2)                  AS OUTKO_NB_TTL2              " & vbNewLine _
                                            & ",SUM(OUTKO_NB_TTL1)                  AS OUTKO_NB_TTL1              " & vbNewLine _
                                            & ",SUM(SEKI_ARI_NB1)                   AS SEKI_ARI_NB1               " & vbNewLine _
                                            & ",SUM(SEKI_ARI_NB2)                   AS SEKI_ARI_NB2               " & vbNewLine _
                                            & ",SUM(SEKI_ARI_NB3)                   AS SEKI_ARI_NB3               " & vbNewLine _
                                            & ",SUM(STORAGE1)                       AS STORAGE1                   " & vbNewLine _
                                            & ",SUM(STORAGE2)                       AS STORAGE2                   " & vbNewLine _
                                            & ",SUM(STORAGE3)                       AS STORAGE3                   " & vbNewLine _
                                            & ",SUM(STORAGE_AMO_TTL)                AS STORAGE_AMO_TTL            " & vbNewLine _
                                            & ",SUM(HANDLING_NB_TTL)                AS HANDLING_NB_TTL            " & vbNewLine _
                                            & ",SUM(HANDLING_IN1)                   AS HANDLING_IN1               " & vbNewLine _
                                            & ",''                                  AS HANDLING_IN2               " & vbNewLine _
                                            & ",''                                  AS HANDLING_IN3               " & vbNewLine _
                                            & ",SUM(HANDLING_AMO_TTL)               AS HANDLING_AMO_TTL           " & vbNewLine _
                                            & ",KURIKOSI_DATE1                      AS KURIKOSI_DATE1             " & vbNewLine _
                                            & ",KURIKOSI_DATE2                      AS KURIKOSI_DATE2             " & vbNewLine _
                                            & ",KURIKOSI_DATE3                      AS KURIKOSI_DATE3             " & vbNewLine _
                                            & ",MATU_DATE                           AS MATU_DATE                  " & vbNewLine _
                                            & "--デュポン用帳票（LMG502）項目                                     " & vbNewLine _
                                            & ",TAX_KB                              AS TAX_KB                     " & vbNewLine _
                                            & ",CUST_COST_CD2                       AS CUST_COST_CD2              " & vbNewLine _
                                            & ",GOODS_NM_2                          AS GOODS_NM_2                 " & vbNewLine _
                                            & ",GOODS_NM_3                          AS GOODS_NM_3                 " & vbNewLine _
                                            & ",INKO_NB1                            AS INKO_NB1                   " & vbNewLine _
                                            & ",INKO_NB2                            AS INKO_NB2                   " & vbNewLine _
                                            & ",INKO_NB3                            AS INKO_NB3                   " & vbNewLine _
                                            & ",OUTKO_NB1                           AS OUTKO_NB1                  " & vbNewLine _
                                            & ",OUTKO_NB2                           AS OUTKO_NB2                  " & vbNewLine _
                                            & ",OUTKO_NB3                           AS OUTKO_NB3                  " & vbNewLine _
                                            & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                            & ",STO_ITEM_CURR_CD                    AS STO_ITEM_CURR_CD           " & vbNewLine _
                                            & ",HAND_ITEM_CURR_CD                   AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                            & ",ITEM_ROUND_POS                      AS ITEM_ROUND_POS             " & vbNewLine _
                                            & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine _
                                            & "FROM                                                               " & vbNewLine _
                                            & "(                                                                  " & vbNewLine


    ''' <summary>
    ''' データ抽出用SELECT句(LMG502(デュポン用)ロット、シリアル番号を除外)DS対応
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_LMG502 As String = "SELECT                                                             " & vbNewLine _
                                              & " RPT_ID                  AS RPT_ID                                 " & vbNewLine _
                                              & ",NRS_BR_CD               AS NRS_BR_CD                              " & vbNewLine _
                                              & ",SEIQTO_CD               AS SEIQTO_CD                              " & vbNewLine _
                                              & ",SEIQTO_NM               AS SEIQTO_NM                              " & vbNewLine _
                                              & ",CUST_CD_L               AS CUST_CD_L                              " & vbNewLine _
                                              & ",CUST_CD_M               AS CUST_CD_M                              " & vbNewLine _
                                              & ",CUST_CD_S               AS CUST_CD_S                              " & vbNewLine _
                                              & ",CUST_CD_SS              AS CUST_CD_SS                             " & vbNewLine _
                                              & ",CUST_NM_L               AS CUST_NM_L                              " & vbNewLine _
                                              & ",CUST_NM_M               AS CUST_NM_M                              " & vbNewLine _
                                              & ",CUST_NM_S               AS CUST_NM_S                              " & vbNewLine _
                                              & ",CUST_NM_SS              AS CUST_NM_SS                             " & vbNewLine _
                                              & ",SEARCH_KEY_1            AS SEARCH_KEY_1                           " & vbNewLine _
                                              & ",''                      AS SEARCH_KEY_2                           " & vbNewLine _
                                              & ",JOB_NO                  AS JOB_NO                                 " & vbNewLine _
                                              & ",NRS_BR_NM               AS NRS_BR_NM                              " & vbNewLine _
                                              & ",INV_DATE_FROM           AS INV_DATE_FROM                          " & vbNewLine _
                                              & ",INV_DATE_TO             AS INV_DATE_TO                            " & vbNewLine _
                                              & ",OFB_KB                  AS OFB_KB                                 " & vbNewLine _
                                              & ",OFB_KB_NM               AS OFB_KB_NM                              " & vbNewLine _
                                              & ",GOODS_NM_1              AS GOODS_NM_1                             " & vbNewLine _
                                              & ",''                      AS PKG_UT                                 " & vbNewLine _
                                              & ",GOODS_CD_CUST           AS GOODS_CD_CUST                          " & vbNewLine _
                                              & ",''                      AS SERIAL_NO                              " & vbNewLine _
                                              & ",''                      AS LOT_NO                                 " & vbNewLine _
                                              & ",NB_UT_NM                AS NB_UT_NM                               " & vbNewLine _
                                              & ",IRIME_UT_NM             AS IRIME_UT_NM                            " & vbNewLine _
                                              & ",IRIME                   AS IRIME                                  " & vbNewLine _
                                              & ",INKA_NO_L               AS INKA_NO_L                              " & vbNewLine _
                                              & ",SUM(KISYZAN_NB1)        AS KISYZAN_NB1                            " & vbNewLine _
                                              & ",SUM(KISYZAN_NB2)        AS KISYZAN_NB2                            " & vbNewLine _
                                              & ",SUM(KISYZAN_NB3)        AS KISYZAN_NB3                            " & vbNewLine _
                                              & ",SUM(MATUZAN_NB)         AS MATUZAN_NB                             " & vbNewLine _
                                              & ",SUM(INKO_NB_TTL2)       AS INKO_NB_TTL2                           " & vbNewLine _
                                              & ",SUM(INKO_NB_TTL1)       AS INKO_NB_TTL1                           " & vbNewLine _
                                              & ",SUM(OUTKO_NB_TTL2)      AS OUTKO_NB_TTL2                          " & vbNewLine _
                                              & ",SUM(OUTKO_NB_TTL1)      AS OUTKO_NB_TTL1                          " & vbNewLine _
                                              & ",SUM(SEKI_ARI_NB1)       AS SEKI_ARI_NB1                           " & vbNewLine _
                                              & ",SUM(SEKI_ARI_NB2)       AS SEKI_ARI_NB2                           " & vbNewLine _
                                              & ",SUM(SEKI_ARI_NB3)       AS SEKI_ARI_NB3                           " & vbNewLine _
                                              & ",STORAGE1                AS STORAGE1                               " & vbNewLine _
                                              & ",STORAGE2                AS STORAGE2                               " & vbNewLine _
                                              & ",STORAGE3                AS STORAGE3                               " & vbNewLine _
                                              & ",SUM(STORAGE_AMO_TTL)    AS STORAGE_AMO_TTL                        " & vbNewLine _
                                              & ",SUM(HANDLING_NB_TTL)    AS HANDLING_NB_TTL                        " & vbNewLine _
                                              & ",HANDLING_IN1            AS HANDLING_IN1                           " & vbNewLine _
                                              & ",''                      AS HANDLING_IN2                           " & vbNewLine _
                                              & ",''                      AS HANDLING_IN3                           " & vbNewLine _
                                              & ",SUM(HANDLING_AMO_TTL)   AS HANDLING_AMO_TTL                       " & vbNewLine _
                                              & ",KURIKOSI_DATE1          AS KURIKOSI_DATE1                         " & vbNewLine _
                                              & ",KURIKOSI_DATE2          AS KURIKOSI_DATE2                         " & vbNewLine _
                                              & ",KURIKOSI_DATE3          AS KURIKOSI_DATE3                         " & vbNewLine _
                                              & ",MATU_DATE               AS MATU_DATE                              " & vbNewLine _
                                              & ",TAX_KB                  AS TAX_KB                                 " & vbNewLine _
                                              & ",CUST_COST_CD2           AS CUST_COST_CD2                          " & vbNewLine _
                                              & ",GOODS_NM_2              AS GOODS_NM_2                             " & vbNewLine _
                                              & ",GOODS_NM_3              AS GOODS_NM_3                             " & vbNewLine _
                                              & ",SUM(INKO_NB1)           AS INKO_NB1                               " & vbNewLine _
                                              & ",SUM(INKO_NB2)           AS INKO_NB2                               " & vbNewLine _
                                              & ",SUM(INKO_NB3)           AS INKO_NB3                               " & vbNewLine _
                                              & ",SUM(OUTKO_NB1)          AS OUTKO_NB1                              " & vbNewLine _
                                              & ",SUM(OUTKO_NB2)          AS OUTKO_NB2                              " & vbNewLine _
                                              & ",SUM(OUTKO_NB3)          AS OUTKO_NB3                              " & vbNewLine _
                                              & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                              & ",STO_ITEM_CURR_CD        AS STO_ITEM_CURR_CD                       " & vbNewLine _
                                              & ",HAND_ITEM_CURR_CD       AS HAND_ITEM_CURR_CD                      " & vbNewLine _
                                              & ",ITEM_ROUND_POS          AS ITEM_ROUND_POS                         " & vbNewLine _
                                              & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine _
                                              & "FROM (                                                             " & vbNewLine
    ''' <summary>
    ''' データ抽出用SELECT句(LMG503)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_LMG503 As String = "SELECT                                                             " & vbNewLine _
                                              & " RPT_ID                              AS RPT_ID                     " & vbNewLine _
                                              & ",NRS_BR_CD                           AS NRS_BR_CD                  " & vbNewLine _
                                              & ",SEIQTO_CD                           AS SEIQTO_CD                  " & vbNewLine _
                                              & ",SEIQTO_NM                           AS SEIQTO_NM                  " & vbNewLine _
                                              & ",MAIN.CUST_CD_L                      AS CUST_CD_L                  " & vbNewLine _
                                              & ",MAIN.CUST_CD_M                      AS CUST_CD_M                  " & vbNewLine _
                                              & ",CUST_CD_S                           AS CUST_CD_S                  " & vbNewLine _
                                              & ",CUST_CD_SS                          AS CUST_CD_SS                 " & vbNewLine _
                                              & ",CUST_NM_L                           AS CUST_NM_L                  " & vbNewLine _
                                              & ",CUST_NM_M                           AS CUST_NM_M                  " & vbNewLine _
                                              & ",CUST_NM_S                           AS CUST_NM_S                  " & vbNewLine _
                                              & ",CUST_NM_SS                          AS CUST_NM_SS                 " & vbNewLine _
                                              & ",SEARCH_KEY_1                        AS SEARCH_KEY_1               " & vbNewLine _
                                              & ",''                                  AS SEARCH_KEY_2               " & vbNewLine _
                                              & ",JOB_NO                              AS JOB_NO                     " & vbNewLine _
                                              & ",NRS_BR_NM                           AS NRS_BR_NM                  " & vbNewLine _
                                              & ",INV_DATE_FROM                       AS INV_DATE_FROM              " & vbNewLine _
                                              & ",INV_DATE_TO                         AS INV_DATE_TO                " & vbNewLine _
                                              & ",OFB_KB                              AS OFB_KB                     " & vbNewLine _
                                              & ",ISNULL(OFB_KB_NM,'')                AS OFB_KB_NM                  " & vbNewLine _
                                              & ",GOODS_NM_1                          AS GOODS_NM_1                 " & vbNewLine _
                                              & ",CASE PKG_UT                                                       " & vbNewLine _
                                              & "     WHEN 'DR' THEN 'B'                                            " & vbNewLine _
                                              & "     ELSE 'A'  END                   AS PKG_UT                     " & vbNewLine _
                                              & ",GOODS_CD_CUST                       AS GOODS_CD_CUST              " & vbNewLine _
                                              & ",SERIAL_NO                           AS SERIAL_NO                  " & vbNewLine _
                                              & ",LOT_NO                              AS LOT_NO                     " & vbNewLine _
                                              & ",NB_UT_NM                            AS NB_UT_NM                   " & vbNewLine _
                                              & ",IRIME_UT_NM                         AS IRIME_UT_NM                " & vbNewLine _
                                              & ",IRIME                               AS IRIME                      " & vbNewLine _
                                              & ",INKA_NO_L                           AS INKA_NO_L                  " & vbNewLine _
                                              & ",KISYZAN_NB1                         AS KISYZAN_NB1                " & vbNewLine _
                                              & ",KISYZAN_NB2                         AS KISYZAN_NB2                " & vbNewLine _
                                              & ",KISYZAN_NB3                         AS KISYZAN_NB3                " & vbNewLine _
                                              & ",MATUZAN_NB                          AS MATUZAN_NB                 " & vbNewLine _
                                              & ",SUM(INKO_NB_TTL2)                   AS INKO_NB_TTL2               " & vbNewLine _
                                              & ",SUM(INKO_NB_TTL1)                   AS INKO_NB_TTL1               " & vbNewLine _
                                              & ",SUM(OUTKO_NB_TTL2)                  AS OUTKO_NB_TTL2              " & vbNewLine _
                                              & ",SUM(OUTKO_NB_TTL1)                  AS OUTKO_NB_TTL1              " & vbNewLine _
                                              & ",SUM(SEKI_ARI_NB1)                   AS SEKI_ARI_NB1               " & vbNewLine _
                                              & ",SUM(SEKI_ARI_NB2)                   AS SEKI_ARI_NB2               " & vbNewLine _
                                              & ",SUM(SEKI_ARI_NB3)                   AS SEKI_ARI_NB3               " & vbNewLine _
                                              & ",SUM(STORAGE1)                       AS STORAGE1                   " & vbNewLine _
                                              & ",SUM(STORAGE2)                       AS STORAGE2                   " & vbNewLine _
                                              & ",SUM(STORAGE3)                       AS STORAGE3                   " & vbNewLine _
                                              & ",SUM(STORAGE_AMO_TTL)                AS STORAGE_AMO_TTL            " & vbNewLine _
                                              & ",SUM(HANDLING_NB_TTL)                AS HANDLING_NB_TTL            " & vbNewLine _
                                              & ",SUM(HANDLING_IN1)                   AS HANDLING_IN1               " & vbNewLine _
                                              & ",''                                  AS HANDLING_IN2               " & vbNewLine _
                                              & ",''                                  AS HANDLING_IN3               " & vbNewLine _
                                              & ",SUM(HANDLING_AMO_TTL)               AS HANDLING_AMO_TTL           " & vbNewLine _
                                              & ",KURIKOSI_DATE1                      AS KURIKOSI_DATE1             " & vbNewLine _
                                              & ",KURIKOSI_DATE2                      AS KURIKOSI_DATE2             " & vbNewLine _
                                              & ",KURIKOSI_DATE3                      AS KURIKOSI_DATE3             " & vbNewLine _
                                              & ",MATU_DATE                           AS MATU_DATE                  " & vbNewLine _
                                              & "--デュポン用帳票（LMG502）項目                                     " & vbNewLine _
                                              & ",TAX_KB                              AS TAX_KB                     " & vbNewLine _
                                              & ",CUST_COST_CD2                       AS CUST_COST_CD2              " & vbNewLine _
                                              & ",GOODS_NM_2                          AS GOODS_NM_2                 " & vbNewLine _
                                              & ",GOODS_NM_3                          AS GOODS_NM_3                 " & vbNewLine _
                                              & ",INKO_NB1                            AS INKO_NB1                   " & vbNewLine _
                                              & ",INKO_NB2                            AS INKO_NB2                   " & vbNewLine _
                                              & ",INKO_NB3                            AS INKO_NB3                   " & vbNewLine _
                                              & ",OUTKO_NB1                           AS OUTKO_NB1                  " & vbNewLine _
                                              & ",OUTKO_NB2                           AS OUTKO_NB2                  " & vbNewLine _
                                              & ",OUTKO_NB3                           AS OUTKO_NB3                  " & vbNewLine _
                                              & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                              & ",STO_ITEM_CURR_CD                    AS STO_ITEM_CURR_CD           " & vbNewLine _
                                              & ",HAND_ITEM_CURR_CD                   AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                              & ",ITEM_ROUND_POS                      AS ITEM_ROUND_POS             " & vbNewLine _
                                              & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine _
                                              & "FROM                                                               " & vbNewLine _
                                              & "(                                                                  " & vbNewLine


    ''' <summary>
    ''' データ抽出用SELECT句(LMG504)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAIN_504 As String = "SELECT                                                             " & vbNewLine _
                                                & " RPT_ID                              AS RPT_ID                     " & vbNewLine _
                                                & ",NRS_BR_CD                           AS NRS_BR_CD                  " & vbNewLine _
                                                & ",SEIQTO_CD                           AS SEIQTO_CD                  " & vbNewLine _
                                                & ",SEIQTO_NM                           AS SEIQTO_NM                  " & vbNewLine _
                                                & ",MAIN.CUST_CD_L                      AS CUST_CD_L                  " & vbNewLine _
                                                & ",MAIN.CUST_CD_M                      AS CUST_CD_M                  " & vbNewLine _
                                                & ",CUST_CD_S                           AS CUST_CD_S                  " & vbNewLine _
                                                & ",CUST_CD_SS                          AS CUST_CD_SS                 " & vbNewLine _
                                                & ",CUST_NM_L                           AS CUST_NM_L                  " & vbNewLine _
                                                & ",CUST_NM_M                           AS CUST_NM_M                  " & vbNewLine _
                                                & ",CUST_NM_S                           AS CUST_NM_S                  " & vbNewLine _
                                                & ",CUST_NM_SS                          AS CUST_NM_SS                 " & vbNewLine _
                                                & ",SEARCH_KEY_1                        AS SEARCH_KEY_1               " & vbNewLine _
                                                & ",SEARCH_KEY_2                        AS SEARCH_KEY_2               " & vbNewLine _
                                                & ",JOB_NO                              AS JOB_NO                     " & vbNewLine _
                                                & ",NRS_BR_NM                           AS NRS_BR_NM                  " & vbNewLine _
                                                & ",INV_DATE_FROM                       AS INV_DATE_FROM              " & vbNewLine _
                                                & ",INV_DATE_TO                         AS INV_DATE_TO                " & vbNewLine _
                                                & ",OFB_KB                              AS OFB_KB                     " & vbNewLine _
                                                & ",ISNULL(OFB_KB_NM,'')                AS OFB_KB_NM                  " & vbNewLine _
                                                & ",GOODS_NM_1                          AS GOODS_NM_1                 " & vbNewLine _
                                                & ",''                                  AS PKG_UT                     " & vbNewLine _
                                                & ",GOODS_CD_CUST                       AS GOODS_CD_CUST              " & vbNewLine _
                                                & ",SERIAL_NO                           AS SERIAL_NO                  " & vbNewLine _
                                                & ",LOT_NO                              AS LOT_NO                     " & vbNewLine _
                                                & ",NB_UT_NM                            AS NB_UT_NM                   " & vbNewLine _
                                                & ",IRIME_UT_NM                         AS IRIME_UT_NM                " & vbNewLine _
                                                & ",IRIME                               AS IRIME                      " & vbNewLine _
                                                & ",''                                  AS INKA_NO_L                  " & vbNewLine _
                                                & ",KISYZAN_NB1                         AS KISYZAN_NB1                " & vbNewLine _
                                                & ",KISYZAN_NB2                         AS KISYZAN_NB2                " & vbNewLine _
                                                & ",KISYZAN_NB3                         AS KISYZAN_NB3                " & vbNewLine _
                                                & ",MATUZAN_NB                          AS MATUZAN_NB                 " & vbNewLine _
                                                & ",SUM(INKO_NB_TTL2)                   AS INKO_NB_TTL2               " & vbNewLine _
                                                & ",SUM(INKO_NB_TTL1)                   AS INKO_NB_TTL1               " & vbNewLine _
                                                & ",SUM(OUTKO_NB_TTL2)                  AS OUTKO_NB_TTL2              " & vbNewLine _
                                                & ",SUM(OUTKO_NB_TTL1)                  AS OUTKO_NB_TTL1              " & vbNewLine _
                                                & ",SUM(SEKI_ARI_NB1)                   AS SEKI_ARI_NB1               " & vbNewLine _
                                                & ",SUM(SEKI_ARI_NB2)                   AS SEKI_ARI_NB2               " & vbNewLine _
                                                & ",SUM(SEKI_ARI_NB3)                   AS SEKI_ARI_NB3               " & vbNewLine _
                                                & ",SUM(STORAGE1)                       AS STORAGE1                   " & vbNewLine _
                                                & ",SUM(STORAGE2)                       AS STORAGE2                   " & vbNewLine _
                                                & ",SUM(STORAGE3)                       AS STORAGE3                   " & vbNewLine _
                                                & ",SUM(STORAGE_AMO_TTL)                AS STORAGE_AMO_TTL            " & vbNewLine _
                                                & ",SUM(HANDLING_NB_TTL)                AS HANDLING_NB_TTL            " & vbNewLine _
                                                & ",SUM(HANDLING_IN1)                   AS HANDLING_IN1               " & vbNewLine _
                                                & ",SUM(HANDLING_IN2)                   AS HANDLING_IN2               " & vbNewLine _
                                                & ",SUM(HANDLING_IN3)                   AS HANDLING_IN3               " & vbNewLine _
                                                & ",SUM(HANDLING_AMO_TTL)               AS HANDLING_AMO_TTL           " & vbNewLine _
                                                & ",KURIKOSI_DATE1                      AS KURIKOSI_DATE1             " & vbNewLine _
                                                & ",KURIKOSI_DATE2                      AS KURIKOSI_DATE2             " & vbNewLine _
                                                & ",KURIKOSI_DATE3                      AS KURIKOSI_DATE3             " & vbNewLine _
                                                & ",MATU_DATE                           AS MATU_DATE                  " & vbNewLine _
                                                & "--デュポン用帳票（LMG502）項目                                     " & vbNewLine _
                                                & ",''                                  AS TAX_KB                     " & vbNewLine _
                                                & ",''                                  AS CUST_COST_CD2              " & vbNewLine _
                                                & ",''                                  AS GOODS_NM_2                 " & vbNewLine _
                                                & ",''                                  AS GOODS_NM_3                 " & vbNewLine _
                                                & ",''                                  AS INKO_NB1                   " & vbNewLine _
                                                & ",''                                  AS INKO_NB2                   " & vbNewLine _
                                                & ",''                                  AS INKO_NB3                   " & vbNewLine _
                                                & ",''                                  AS OUTKO_NB1                  " & vbNewLine _
                                                & ",''                                  AS OUTKO_NB2                  " & vbNewLine _
                                                & ",''                                  AS OUTKO_NB3                  " & vbNewLine _
                                                & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                                & ",STO_ITEM_CURR_CD                    AS STO_ITEM_CURR_CD           " & vbNewLine _
                                                & ",HAND_ITEM_CURR_CD                   AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                                & ",ITEM_ROUND_POS                      AS ITEM_ROUND_POS             " & vbNewLine _
                                                & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine _
                                                & "FROM                                                               " & vbNewLine _
                                                & "(                                                                  " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT句(MAIN)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAIN_518 As String = "SELECT                                                             " & vbNewLine _
                                            & " RPT_ID                              AS RPT_ID                     " & vbNewLine _
                                            & ",NRS_BR_CD                           AS NRS_BR_CD                  " & vbNewLine _
                                            & ",SEIQTO_CD                           AS SEIQTO_CD                  " & vbNewLine _
                                            & ",SEIQTO_NM                           AS SEIQTO_NM                  " & vbNewLine _
                                            & ",MAIN.CUST_CD_L                      AS CUST_CD_L                  " & vbNewLine _
                                            & ",MAIN.CUST_CD_M                      AS CUST_CD_M                  " & vbNewLine _
                                            & ",CUST_CD_S                           AS CUST_CD_S                  " & vbNewLine _
                                            & ",CUST_CD_SS                          AS CUST_CD_SS                 " & vbNewLine _
                                            & ",CUST_NM_L                           AS CUST_NM_L                  " & vbNewLine _
                                            & ",CUST_NM_M                           AS CUST_NM_M                  " & vbNewLine _
                                            & ",CUST_NM_S                           AS CUST_NM_S                  " & vbNewLine _
                                            & ",CUST_NM_SS                          AS CUST_NM_SS                 " & vbNewLine _
                                            & ",SEARCH_KEY_1                        AS SEARCH_KEY_1               " & vbNewLine _
                                            & ",''                                  AS SEARCH_KEY_2               " & vbNewLine _
                                            & ",JOB_NO                              AS JOB_NO                     " & vbNewLine _
                                            & ",NRS_BR_NM                           AS NRS_BR_NM                  " & vbNewLine _
                                            & ",INV_DATE_FROM                       AS INV_DATE_FROM              " & vbNewLine _
                                            & ",INV_DATE_TO                         AS INV_DATE_TO                " & vbNewLine _
                                            & ",OFB_KB                              AS OFB_KB                     " & vbNewLine _
                                            & ",ISNULL(OFB_KB_NM,'')                AS OFB_KB_NM                  " & vbNewLine _
                                            & ",GOODS_NM_1                          AS GOODS_NM_1                 " & vbNewLine _
                                            & ",''                                  AS PKG_UT                     " & vbNewLine _
                                            & ",GOODS_CD_CUST                       AS GOODS_CD_CUST              " & vbNewLine _
                                            & ",SERIAL_NO                           AS SERIAL_NO                  " & vbNewLine _
                                            & ",LOT_NO                              AS LOT_NO                     " & vbNewLine _
                                            & ",NB_UT_NM                            AS NB_UT_NM                   " & vbNewLine _
                                            & ",IRIME_UT_NM                         AS IRIME_UT_NM                " & vbNewLine _
                                            & ",IRIME                               AS IRIME                      " & vbNewLine _
                                            & ",INKA_NO_L                           AS INKA_NO_L                  " & vbNewLine _
                                            & ",KISYZAN_NB1                         AS KISYZAN_NB1                " & vbNewLine _
                                            & ",KISYZAN_NB2                         AS KISYZAN_NB2                " & vbNewLine _
                                            & ",KISYZAN_NB3                         AS KISYZAN_NB3                " & vbNewLine _
                                            & ",MATUZAN_NB                          AS MATUZAN_NB                 " & vbNewLine _
                                            & ",SUM(INKO_NB_TTL2)                   AS INKO_NB_TTL2               " & vbNewLine _
                                            & ",SUM(INKO_NB_TTL1)                   AS INKO_NB_TTL1               " & vbNewLine _
                                            & ",SUM(OUTKO_NB_TTL2)                  AS OUTKO_NB_TTL2              " & vbNewLine _
                                            & ",SUM(OUTKO_NB_TTL1)                  AS OUTKO_NB_TTL1              " & vbNewLine _
                                            & ",SUM(SEKI_ARI_NB1)                   AS SEKI_ARI_NB1               " & vbNewLine _
                                            & ",SUM(SEKI_ARI_NB2)                   AS SEKI_ARI_NB2               " & vbNewLine _
                                            & ",SUM(SEKI_ARI_NB3)                   AS SEKI_ARI_NB3               " & vbNewLine _
                                            & ",SUM(STORAGE1)                       AS STORAGE1                   " & vbNewLine _
                                            & ",SUM(STORAGE2)                       AS STORAGE2                   " & vbNewLine _
                                            & ",SUM(STORAGE3)                       AS STORAGE3                   " & vbNewLine _
                                            & ",SUM(STORAGE_AMO_TTL)                AS STORAGE_AMO_TTL            " & vbNewLine _
                                            & ",SUM(HANDLING_NB_TTL)                AS HANDLING_NB_TTL            " & vbNewLine _
                                            & ",SUM(HANDLING_IN1)                   AS HANDLING_IN1               " & vbNewLine _
                                            & ",''                                  AS HANDLING_IN2               " & vbNewLine _
                                            & ",''                                  AS HANDLING_IN3               " & vbNewLine _
                                            & ",SUM(HANDLING_AMO_TTL)               AS HANDLING_AMO_TTL           " & vbNewLine _
                                            & ",SUM(HANDLING_OUT1)                  AS HANDLING_OUT1              " & vbNewLine _
                                            & ",KURIKOSI_DATE1                      AS KURIKOSI_DATE1             " & vbNewLine _
                                            & ",KURIKOSI_DATE2                      AS KURIKOSI_DATE2             " & vbNewLine _
                                            & ",KURIKOSI_DATE3                      AS KURIKOSI_DATE3             " & vbNewLine _
                                            & ",MATU_DATE                           AS MATU_DATE                  " & vbNewLine _
                                            & ",TAX_KB                              AS TAX_KB                     " & vbNewLine _
                                            & ",CUST_COST_CD2                       AS CUST_COST_CD2              " & vbNewLine _
                                            & ",GOODS_NM_2                          AS GOODS_NM_2                 " & vbNewLine _
                                            & ",GOODS_NM_3                          AS GOODS_NM_3                 " & vbNewLine _
                                            & ",INKO_NB1                            AS INKO_NB1                   " & vbNewLine _
                                            & ",INKO_NB2                            AS INKO_NB2                   " & vbNewLine _
                                            & ",INKO_NB3                            AS INKO_NB3                   " & vbNewLine _
                                            & ",OUTKO_NB1                           AS OUTKO_NB1                  " & vbNewLine _
                                            & ",OUTKO_NB2                           AS OUTKO_NB2                  " & vbNewLine _
                                            & ",OUTKO_NB3                           AS OUTKO_NB3                  " & vbNewLine _
                                            & ",STO_ITEM_CURR_CD                    AS STO_ITEM_CURR_CD           " & vbNewLine _
                                            & ",HAND_ITEM_CURR_CD                   AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                            & ",ITEM_ROUND_POS                      AS ITEM_ROUND_POS             " & vbNewLine _
                                            & "FROM                                                               " & vbNewLine _
                                            & "(                                                                  " & vbNewLine


    ''' <summary>
    ''' データ抽出用SELECT句(保管料)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_HOKAN As String = "   --保管料                                                        " & vbNewLine _
                                             & "   SELECT                                                  " & vbNewLine _
                                             & "   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                             & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                " & vbNewLine _
                                             & "        ELSE MR3.RPT_ID END          AS RPT_ID                     " & vbNewLine _
                                             & "   ,SE.NRS_BR_CD                     AS NRS_BR_CD                  " & vbNewLine _
                                             & "   ,MS.SEIQTO_CD                     AS SEIQTO_CD                  " & vbNewLine _
                                             & "   ,MS.SEIQTO_NM                     AS SEIQTO_NM                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_L                     AS CUST_CD_L                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_M                     AS CUST_CD_M                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_S                     AS CUST_CD_S                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_SS                    AS CUST_CD_SS                 " & vbNewLine _
                                             & "   ,MC.CUST_NM_L                     AS CUST_NM_L                  " & vbNewLine _
                                             & "   ,MC.CUST_NM_M                     AS CUST_NM_M                  " & vbNewLine _
                                             & "   ,MC.CUST_NM_S                     AS CUST_NM_S                  " & vbNewLine _
                                             & "   ,MC.CUST_NM_SS                    AS CUST_NM_SS                 " & vbNewLine _
                                             & "   ,MG.SEARCH_KEY_1                  AS SEARCH_KEY_1               " & vbNewLine _
                                             & "   ,SE.JOB_NO                        AS JOB_NO                     " & vbNewLine _
                                             & "   ,CASE WHEN SUBSTRING(SE.INV_DATE_TO, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                             & "         THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, MB.NRS_BR_NM)          " & vbNewLine _
                                             & "         ELSE MB.NRS_BR_NM                                         " & vbNewLine _
                                             & "         END  AS NRS_BR_NM                                         " & vbNewLine _
                                             & "   ,SE.INV_DATE_FROM                 AS INV_DATE_FROM              " & vbNewLine _
                                             & "   ,SE.INV_DATE_TO                   AS INV_DATE_TO                " & vbNewLine _
                                             & "   ,SE.OFB_KB                        AS OFB_KB                     " & vbNewLine _
                                             & "   ,KBN_OFB.KBN_NM1                  AS OFB_KB_NM                  " & vbNewLine _
                                             & "   ,MG.GOODS_NM_1                    AS GOODS_NM_1                 " & vbNewLine _
                                             & "   ,''                               AS PKG_UT                     " & vbNewLine _
                                             & "   ,MG.GOODS_CD_CUST                 AS GOODS_CD_CUST              " & vbNewLine _
                                             & "   ,SE.SERIAL_NO                     AS SERIAL_NO                  " & vbNewLine _
                                             & "   ,SE.LOT_NO                        AS LOT_NO                     " & vbNewLine _
                                             & "   ,MG.NB_UT                         AS NB_UT_NM                   " & vbNewLine _
                                             & "   ,MG.STD_IRIME_UT                  AS IRIME_UT_NM                " & vbNewLine _
                                             & "   ,SE.IRIME                         AS IRIME                      " & vbNewLine _
                                             & "   ,SE.INKA_NO_L                     AS INKA_NO_L                  " & vbNewLine _
                                             & "   ,SE.KISYZAN_NB1                   AS KISYZAN_NB1                " & vbNewLine _
                                             & "   ,SE.KISYZAN_NB2                   AS KISYZAN_NB2                " & vbNewLine _
                                             & "   ,SE.KISYZAN_NB3                   AS KISYZAN_NB3                " & vbNewLine _
                                             & "   ,SE.MATUZAN_NB                    AS MATUZAN_NB                 " & vbNewLine _
                                             & "   ,0                                AS INKO_NB_TTL2               " & vbNewLine _
                                             & "   ,0                                AS INKO_NB_TTL1               " & vbNewLine _
                                             & "   ,0                                AS OUTKO_NB_TTL2              " & vbNewLine _
                                             & "   ,0                                AS OUTKO_NB_TTL1              " & vbNewLine _
                                             & "   ,SE.SEKI_ARI_NB1                  AS SEKI_ARI_NB1               " & vbNewLine _
                                             & "   ,SE.SEKI_ARI_NB2                  AS SEKI_ARI_NB2               " & vbNewLine _
                                             & "   ,SE.SEKI_ARI_NB3                  AS SEKI_ARI_NB3               " & vbNewLine _
                                             & "   ,SE.STORAGE1                      AS STORAGE1                   " & vbNewLine _
                                             & "   ,SE.STORAGE2                      AS STORAGE2                   " & vbNewLine _
                                             & "   ,SE.STORAGE3                      AS STORAGE3                   " & vbNewLine _
                                             & "   ,SE.STORAGE_AMO_TTL               AS STORAGE_AMO_TTL            " & vbNewLine _
                                             & "   ,0                                AS HANDLING_NB_TTL            " & vbNewLine _
                                             & "   ,0                                AS HANDLING_IN1               " & vbNewLine _
                                             & "   ,0                                AS HANDLING_AMO_TTL           " & vbNewLine _
                                             & "   ,SE.KURIKOSI_DATE1                AS KURIKOSI_DATE1             " & vbNewLine _
                                             & "   ,SE.KURIKOSI_DATE2                AS KURIKOSI_DATE2             " & vbNewLine _
                                             & "   ,SE.KURIKOSI_DATE3                AS KURIKOSI_DATE3             " & vbNewLine _
                                             & "   ,SE.MATU_DATE                     AS MATU_DATE                  " & vbNewLine _
                                             & "   --デュポン用帳票（LMG502）項目                                  " & vbNewLine _
                                             & "   ,SE.TAX_KB                        AS TAX_KB                     " & vbNewLine _
                                             & "   ,MG.CUST_COST_CD2                 AS CUST_COST_CD2              " & vbNewLine _
                                             & "   ,MG.GOODS_NM_2                    AS GOODS_NM_2                 " & vbNewLine _
                                             & "   ,MG.GOODS_NM_3                    AS GOODS_NM_3                 " & vbNewLine _
                                             & "   ,SE.INKO_NB1                      AS INKO_NB1                   " & vbNewLine _
                                             & "   ,SE.INKO_NB2                      AS INKO_NB2                   " & vbNewLine _
                                             & "   ,SE.INKO_NB3                      AS INKO_NB3                   " & vbNewLine _
                                             & "   ,SE.OUTKO_NB1                     AS OUTKO_NB1                  " & vbNewLine _
                                             & "   ,SE.OUTKO_NB2                     AS OUTKO_NB2                  " & vbNewLine _
                                             & "   ,SE.OUTKO_NB3                     AS OUTKO_NB3                  " & vbNewLine _
                                             & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                             & "   ,KBNC025_ITEM.KBN_NM1             AS STO_ITEM_CURR_CD           " & vbNewLine _
                                             & "   ,KBNC025_ITEM.KBN_NM1             AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                             & "   ,ISNULL(CURR_ITEM.ROUND_POS,0)    AS ITEM_ROUND_POS             " & vbNewLine _
                                             & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT句(保管料LMG503用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_HOKAN_LMG503 As String = "   --保管料                                                 " & vbNewLine _
                                             & "   SELECT                                                  " & vbNewLine _
                                             & "   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                             & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                " & vbNewLine _
                                             & "        ELSE MR3.RPT_ID END          AS RPT_ID                     " & vbNewLine _
                                             & "   ,SE.NRS_BR_CD                     AS NRS_BR_CD                  " & vbNewLine _
                                             & "   ,MS.SEIQTO_CD                     AS SEIQTO_CD                  " & vbNewLine _
                                             & "   ,MS.SEIQTO_NM                     AS SEIQTO_NM                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_L                     AS CUST_CD_L                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_M                     AS CUST_CD_M                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_S                     AS CUST_CD_S                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_SS                    AS CUST_CD_SS                 " & vbNewLine _
                                             & "   ,MC.CUST_NM_L                     AS CUST_NM_L                  " & vbNewLine _
                                             & "   ,MC.CUST_NM_M                     AS CUST_NM_M                  " & vbNewLine _
                                             & "   ,MC.CUST_NM_S                     AS CUST_NM_S                  " & vbNewLine _
                                             & "   ,MC.CUST_NM_SS                    AS CUST_NM_SS                 " & vbNewLine _
                                             & "   ,MG.SEARCH_KEY_1                  AS SEARCH_KEY_1               " & vbNewLine _
                                             & "   ,SE.JOB_NO                        AS JOB_NO                     " & vbNewLine _
                                             & "   ,CASE WHEN SUBSTRING(SE.INV_DATE_TO, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                             & "         THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, MB.NRS_BR_NM)          " & vbNewLine _
                                             & "         ELSE MB.NRS_BR_NM                                         " & vbNewLine _
                                             & "         END  AS NRS_BR_NM                                         " & vbNewLine _
                                             & "   ,SE.INV_DATE_FROM                 AS INV_DATE_FROM              " & vbNewLine _
                                             & "   ,SE.INV_DATE_TO                   AS INV_DATE_TO                " & vbNewLine _
                                             & "   ,SE.OFB_KB                        AS OFB_KB                     " & vbNewLine _
                                             & "   ,KBN_OFB.KBN_NM1                  AS OFB_KB_NM                  " & vbNewLine _
                                             & "   ,MG.GOODS_NM_1                    AS GOODS_NM_1                 " & vbNewLine _
                                             & "   ,MG.PKG_UT                        AS PKG_UT                     " & vbNewLine _
                                             & "   ,MG.GOODS_CD_CUST                 AS GOODS_CD_CUST              " & vbNewLine _
                                             & "   ,SE.SERIAL_NO                     AS SERIAL_NO                  " & vbNewLine _
                                             & "   ,SE.LOT_NO                        AS LOT_NO                     " & vbNewLine _
                                             & "   ,MG.NB_UT                         AS NB_UT_NM                   " & vbNewLine _
                                             & "   ,MG.STD_IRIME_UT                  AS IRIME_UT_NM                " & vbNewLine _
                                             & "   ,SE.IRIME                         AS IRIME                      " & vbNewLine _
                                             & "   ,SE.INKA_NO_L                     AS INKA_NO_L                  " & vbNewLine _
                                             & "   ,SE.KISYZAN_NB1                   AS KISYZAN_NB1                " & vbNewLine _
                                             & "   ,SE.KISYZAN_NB2                   AS KISYZAN_NB2                " & vbNewLine _
                                             & "   ,SE.KISYZAN_NB3                   AS KISYZAN_NB3                " & vbNewLine _
                                             & "   ,SE.MATUZAN_NB                    AS MATUZAN_NB                 " & vbNewLine _
                                             & "   ,0                                AS INKO_NB_TTL2               " & vbNewLine _
                                             & "   ,0                                AS INKO_NB_TTL1               " & vbNewLine _
                                             & "   ,0                                AS OUTKO_NB_TTL2              " & vbNewLine _
                                             & "   ,0                                AS OUTKO_NB_TTL1              " & vbNewLine _
                                             & "   ,SE.SEKI_ARI_NB1                  AS SEKI_ARI_NB1               " & vbNewLine _
                                             & "   ,SE.SEKI_ARI_NB2                  AS SEKI_ARI_NB2               " & vbNewLine _
                                             & "   ,SE.SEKI_ARI_NB3                  AS SEKI_ARI_NB3               " & vbNewLine _
                                             & "   ,SE.STORAGE1                      AS STORAGE1                   " & vbNewLine _
                                             & "   ,SE.STORAGE2                      AS STORAGE2                   " & vbNewLine _
                                             & "   ,SE.STORAGE3                      AS STORAGE3                   " & vbNewLine _
                                             & "   ,SE.STORAGE_AMO_TTL               AS STORAGE_AMO_TTL            " & vbNewLine _
                                             & "   ,0                                AS HANDLING_NB_TTL            " & vbNewLine _
                                             & "   ,0                                AS HANDLING_IN1               " & vbNewLine _
                                             & "   ,0                                AS HANDLING_AMO_TTL           " & vbNewLine _
                                             & "   ,SE.KURIKOSI_DATE1                AS KURIKOSI_DATE1             " & vbNewLine _
                                             & "   ,SE.KURIKOSI_DATE2                AS KURIKOSI_DATE2             " & vbNewLine _
                                             & "   ,SE.KURIKOSI_DATE3                AS KURIKOSI_DATE3             " & vbNewLine _
                                             & "   ,SE.MATU_DATE                     AS MATU_DATE                  " & vbNewLine _
                                             & "   --デュポン用帳票（LMG502）項目                                  " & vbNewLine _
                                             & "   ,SE.TAX_KB                        AS TAX_KB                     " & vbNewLine _
                                             & "   ,MG.CUST_COST_CD2                 AS CUST_COST_CD2              " & vbNewLine _
                                             & "   ,MG.GOODS_NM_2                    AS GOODS_NM_2                 " & vbNewLine _
                                             & "   ,MG.GOODS_NM_3                    AS GOODS_NM_3                 " & vbNewLine _
                                             & "   ,SE.INKO_NB1                      AS INKO_NB1                   " & vbNewLine _
                                             & "   ,SE.INKO_NB2                      AS INKO_NB2                   " & vbNewLine _
                                             & "   ,SE.INKO_NB3                      AS INKO_NB3                   " & vbNewLine _
                                             & "   ,SE.OUTKO_NB1                     AS OUTKO_NB1                  " & vbNewLine _
                                             & "   ,SE.OUTKO_NB2                     AS OUTKO_NB2                  " & vbNewLine _
                                             & "   ,SE.OUTKO_NB3                     AS OUTKO_NB3                  " & vbNewLine _
                                             & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                             & "   ,KBNC025_ITEM.KBN_NM1             AS STO_ITEM_CURR_CD           " & vbNewLine _
                                             & "   ,KBNC025_ITEM.KBN_NM1             AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                             & "   ,ISNULL(CURR_ITEM.ROUND_POS,0)    AS ITEM_ROUND_POS             " & vbNewLine _
                                             & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT句(保管料504用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_HOKAN_504 As String = "   --保管料                                                        " & vbNewLine _
                                                 & "SELECT                                                     " & vbNewLine _
                                                 & "   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                                 & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                " & vbNewLine _
                                                 & "        ELSE MR3.RPT_ID END          AS RPT_ID                     " & vbNewLine _
                                                 & "   ,SE.NRS_BR_CD                     AS NRS_BR_CD                  " & vbNewLine _
                                                 & "   ,MS.SEIQTO_CD                     AS SEIQTO_CD                  " & vbNewLine _
                                                 & "   ,MS.SEIQTO_NM                     AS SEIQTO_NM                  " & vbNewLine _
                                                 & "   ,MG.CUST_CD_L                     AS CUST_CD_L                  " & vbNewLine _
                                                 & "   ,MG.CUST_CD_M                     AS CUST_CD_M                  " & vbNewLine _
                                                 & "   ,MG.CUST_CD_S                     AS CUST_CD_S                  " & vbNewLine _
                                                 & "   ,MG.CUST_CD_SS                    AS CUST_CD_SS                 " & vbNewLine _
                                                 & "   ,MC.CUST_NM_L                     AS CUST_NM_L                  " & vbNewLine _
                                                 & "   ,MC.CUST_NM_M                     AS CUST_NM_M                  " & vbNewLine _
                                                 & "   ,MC.CUST_NM_S                     AS CUST_NM_S                  " & vbNewLine _
                                                 & "   ,MC.CUST_NM_SS                    AS CUST_NM_SS                 " & vbNewLine _
                                                 & "   ,MG.SEARCH_KEY_1                  AS SEARCH_KEY_1               " & vbNewLine _
                                                 & "   ,MG.SEARCH_KEY_2                  AS SEARCH_KEY_2               " & vbNewLine _
                                                 & "   ,SE.JOB_NO                        AS JOB_NO                     " & vbNewLine _
                                                 & "   , CASE WHEN SUBSTRING(SE.INV_DATE_TO, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                                 & "         THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, MB.NRS_BR_NM)          " & vbNewLine _
                                                 & "         ELSE MB.NRS_BR_NM                                         " & vbNewLine _
                                                 & "         END  AS NRS_BR_NM                                         " & vbNewLine _
                                                 & "   ,SE.INV_DATE_FROM                 AS INV_DATE_FROM              " & vbNewLine _
                                                 & "   ,SE.INV_DATE_TO                   AS INV_DATE_TO                " & vbNewLine _
                                                 & "   ,SE.OFB_KB                        AS OFB_KB                     " & vbNewLine _
                                                 & "   ,KBN_OFB.KBN_NM1                  AS OFB_KB_NM                  " & vbNewLine _
                                                 & "   ,MG.GOODS_NM_1                    AS GOODS_NM_1                 " & vbNewLine _
                                                 & "   ,''                               AS PKG_UT                     " & vbNewLine _
                                                 & "   ,MG.GOODS_CD_CUST                 AS GOODS_CD_CUST              " & vbNewLine _
                                                 & "   ,SE.SERIAL_NO                     AS SERIAL_NO                  " & vbNewLine _
                                                 & "   ,SE.LOT_NO                        AS LOT_NO                     " & vbNewLine _
                                                 & "   ,MG.NB_UT                         AS NB_UT_NM                   " & vbNewLine _
                                                 & "   ,MG.STD_IRIME_UT                  AS IRIME_UT_NM                " & vbNewLine _
                                                 & "   ,SE.IRIME                         AS IRIME                      " & vbNewLine _
                                                 & "   ,SUM(SE.KISYZAN_NB1)              AS KISYZAN_NB1                " & vbNewLine _
                                                 & "   ,SUM(SE.KISYZAN_NB2)              AS KISYZAN_NB2                " & vbNewLine _
                                                 & "   ,SUM(SE.KISYZAN_NB3)              AS KISYZAN_NB3                " & vbNewLine _
                                                 & "   ,SUM(SE.MATUZAN_NB)               AS MATUZAN_NB                 " & vbNewLine _
                                                 & "   ,0                                AS INKO_NB_TTL2               " & vbNewLine _
                                                 & "   ,0                                AS INKO_NB_TTL1               " & vbNewLine _
                                                 & "   ,0                                AS OUTKO_NB_TTL2              " & vbNewLine _
                                                 & "   ,0                                AS OUTKO_NB_TTL1              " & vbNewLine _
                                                 & "   ,SUM(SE.SEKI_ARI_NB1)             AS SEKI_ARI_NB1               " & vbNewLine _
                                                 & "   ,SUM(SE.SEKI_ARI_NB2)             AS SEKI_ARI_NB2               " & vbNewLine _
                                                 & "   ,SUM(SE.SEKI_ARI_NB3)             AS SEKI_ARI_NB3               " & vbNewLine _
                                                 & "   ,SE.STORAGE1                      AS STORAGE1                   " & vbNewLine _
                                                 & "   ,SE.STORAGE2                      AS STORAGE2                   " & vbNewLine _
                                                 & "   ,SE.STORAGE3                      AS STORAGE3                   " & vbNewLine _
                                                 & "   ,SUM(SE.STORAGE_AMO_TTL)          AS STORAGE_AMO_TTL            " & vbNewLine _
                                                 & "   ,0                                AS HANDLING_NB_TTL            " & vbNewLine _
                                                 & "   ,0                                AS HANDLING_IN1               " & vbNewLine _
                                                 & "   ,0                                AS HANDLING_IN2               " & vbNewLine _
                                                 & "   ,0                                AS HANDLING_IN3               " & vbNewLine _
                                                 & "   ,0                                AS HANDLING_AMO_TTL           " & vbNewLine _
                                                 & "   ,SE.KURIKOSI_DATE1                AS KURIKOSI_DATE1             " & vbNewLine _
                                                 & "   ,SE.KURIKOSI_DATE2                AS KURIKOSI_DATE2             " & vbNewLine _
                                                 & "   ,SE.KURIKOSI_DATE3                AS KURIKOSI_DATE3             " & vbNewLine _
                                                 & "   ,SE.MATU_DATE                     AS MATU_DATE                  " & vbNewLine _
                                                 & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                                 & "   ,KBNC025_ITEM.KBN_NM1             AS STO_ITEM_CURR_CD           " & vbNewLine _
                                                 & "   ,KBNC025_ITEM.KBN_NM1             AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                                 & "   ,ISNULL(CURR_ITEM.ROUND_POS,0)    AS ITEM_ROUND_POS             " & vbNewLine _
                                                 & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT句(保管料)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_HOKAN_518 As String = "   --保管料                                                    " & vbNewLine _
                                             & "   SELECT                                                          " & vbNewLine _
                                             & "   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                             & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                " & vbNewLine _
                                             & "        ELSE MR3.RPT_ID END          AS RPT_ID                     " & vbNewLine _
                                             & "   ,SE.NRS_BR_CD                     AS NRS_BR_CD                  " & vbNewLine _
                                             & "   ,MS.SEIQTO_CD                     AS SEIQTO_CD                  " & vbNewLine _
                                             & "   ,MS.SEIQTO_NM                     AS SEIQTO_NM                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_L                     AS CUST_CD_L                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_M                     AS CUST_CD_M                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_S                     AS CUST_CD_S                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_SS                    AS CUST_CD_SS                 " & vbNewLine _
                                             & "   ,MC.CUST_NM_L                     AS CUST_NM_L                  " & vbNewLine _
                                             & "   ,MC.CUST_NM_M                     AS CUST_NM_M                  " & vbNewLine _
                                             & "   ,MC.CUST_NM_S                     AS CUST_NM_S                  " & vbNewLine _
                                             & "   ,MC.CUST_NM_SS                    AS CUST_NM_SS                 " & vbNewLine _
                                             & "   ,MG.SEARCH_KEY_1                  AS SEARCH_KEY_1               " & vbNewLine _
                                             & "   ,SE.JOB_NO                        AS JOB_NO                     " & vbNewLine _
                                             & "   ,CASE WHEN SUBSTRING(SE.INV_DATE_TO, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                             & "         THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, MB.NRS_BR_NM)          " & vbNewLine _
                                             & "         ELSE MB.NRS_BR_NM                                         " & vbNewLine _
                                             & "         END  AS NRS_BR_NM                                         " & vbNewLine _
                                             & "   ,SE.INV_DATE_FROM                 AS INV_DATE_FROM              " & vbNewLine _
                                             & "   ,SE.INV_DATE_TO                   AS INV_DATE_TO                " & vbNewLine _
                                             & "   ,SE.OFB_KB                        AS OFB_KB                     " & vbNewLine _
                                             & "   ,KBN_OFB.KBN_NM1                  AS OFB_KB_NM                  " & vbNewLine _
                                             & "   ,MG.GOODS_NM_1                    AS GOODS_NM_1                 " & vbNewLine _
                                             & "   ,''                               AS PKG_UT                     " & vbNewLine _
                                             & "   ,MG.GOODS_CD_CUST                 AS GOODS_CD_CUST              " & vbNewLine _
                                             & "   ,SE.SERIAL_NO                     AS SERIAL_NO                  " & vbNewLine _
                                             & "   ,SE.LOT_NO                        AS LOT_NO                     " & vbNewLine _
                                             & "   ,MG.NB_UT                         AS NB_UT_NM                   " & vbNewLine _
                                             & "   ,MG.STD_IRIME_UT                  AS IRIME_UT_NM                " & vbNewLine _
                                             & "   ,SE.IRIME                         AS IRIME                      " & vbNewLine _
                                             & "   ,SE.INKA_NO_L                     AS INKA_NO_L                  " & vbNewLine _
                                             & "   ,SE.KISYZAN_NB1                   AS KISYZAN_NB1                " & vbNewLine _
                                             & "   ,SE.KISYZAN_NB2                   AS KISYZAN_NB2                " & vbNewLine _
                                             & "   ,SE.KISYZAN_NB3                   AS KISYZAN_NB3                " & vbNewLine _
                                             & "   ,SE.MATUZAN_NB                    AS MATUZAN_NB                 " & vbNewLine _
                                             & "   ,0                                AS INKO_NB_TTL2               " & vbNewLine _
                                             & "   ,0                                AS INKO_NB_TTL1               " & vbNewLine _
                                             & "   ,0                                AS OUTKO_NB_TTL2              " & vbNewLine _
                                             & "   ,0                                AS OUTKO_NB_TTL1              " & vbNewLine _
                                             & "   ,SE.SEKI_ARI_NB1                  AS SEKI_ARI_NB1               " & vbNewLine _
                                             & "   ,SE.SEKI_ARI_NB2                  AS SEKI_ARI_NB2               " & vbNewLine _
                                             & "   ,SE.SEKI_ARI_NB3                  AS SEKI_ARI_NB3               " & vbNewLine _
                                             & "   ,SE.STORAGE1                      AS STORAGE1                   " & vbNewLine _
                                             & "   ,SE.STORAGE2                      AS STORAGE2                   " & vbNewLine _
                                             & "   ,SE.STORAGE3                      AS STORAGE3                   " & vbNewLine _
                                             & "   ,SE.STORAGE_AMO_TTL               AS STORAGE_AMO_TTL            " & vbNewLine _
                                             & "   ,0                                AS HANDLING_NB_TTL            " & vbNewLine _
                                             & "   ,0                                AS HANDLING_IN1               " & vbNewLine _
                                             & "   ,0                                AS HANDLING_OUT1              " & vbNewLine _
                                             & "   ,0                                AS HANDLING_AMO_TTL           " & vbNewLine _
                                             & "   ,SE.KURIKOSI_DATE1                AS KURIKOSI_DATE1             " & vbNewLine _
                                             & "   ,SE.KURIKOSI_DATE2                AS KURIKOSI_DATE2             " & vbNewLine _
                                             & "   ,SE.KURIKOSI_DATE3                AS KURIKOSI_DATE3             " & vbNewLine _
                                             & "   ,SE.MATU_DATE                     AS MATU_DATE                  " & vbNewLine _
                                             & "   --デュポン用帳票（LMG502）項目                                  " & vbNewLine _
                                             & "   ,SE.TAX_KB                        AS TAX_KB                     " & vbNewLine _
                                             & "   ,MG.CUST_COST_CD2                 AS CUST_COST_CD2              " & vbNewLine _
                                             & "   ,MG.GOODS_NM_2                    AS GOODS_NM_2                 " & vbNewLine _
                                             & "   ,MG.GOODS_NM_3                    AS GOODS_NM_3                 " & vbNewLine _
                                             & "   ,SE.INKO_NB1                      AS INKO_NB1                   " & vbNewLine _
                                             & "   ,SE.INKO_NB2                      AS INKO_NB2                   " & vbNewLine _
                                             & "   ,SE.INKO_NB3                      AS INKO_NB3                   " & vbNewLine _
                                             & "   ,SE.OUTKO_NB1                     AS OUTKO_NB1                  " & vbNewLine _
                                             & "   ,SE.OUTKO_NB2                     AS OUTKO_NB2                  " & vbNewLine _
                                             & "   ,SE.OUTKO_NB3                     AS OUTKO_NB3                  " & vbNewLine _
                                             & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                             & "   ,KBNC025_ITEM.KBN_NM1             AS STO_ITEM_CURR_CD           " & vbNewLine _
                                             & "   ,KBNC025_ITEM.KBN_NM1             AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                             & "   ,ISNULL(CURR_ITEM.ROUND_POS,0)    AS ITEM_ROUND_POS             " & vbNewLine _
                                             & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine


    ''' <summary>
    ''' データ抽出用FROM句(保管料)
    ''' </summary>
    ''' <remarks>
    '''   2011/10/17 修正 須賀
    '''      商品コード取得時、削除フラグを抽出条件から除外
    ''' </remarks>
    Private Const SQL_FROM_HOKAN As String = "   FROM                                                            " & vbNewLine _
                                           & "   $LM_TRN$..G_SEKY_MEISAI_PRT SE                                  " & vbNewLine _
                                           & "   INNER JOIN $LM_TRN$..G_SEKY_MEISAI_PRT SEKYP                    " & vbNewLine _
                                           & "   ON SE.NRS_BR_CD = SEKYP.NRS_BR_CD                               " & vbNewLine _
                                           & "   AND SE.JOB_NO = SEKYP.JOB_NO                                    " & vbNewLine _
                                           & "   AND SE.CTL_NO = SEKYP.CTL_NO                                    " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_GOODS MG                                  " & vbNewLine _
                                           & "   ON  MG.NRS_BR_CD = SE.NRS_BR_CD                                 " & vbNewLine _
                                           & "   AND MG.GOODS_CD_NRS = SE.GOODS_CD_NRS                           " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_CUST MC                                   " & vbNewLine _
                                           & "   ON  MC.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                           & "   AND MC.NRS_BR_CD = MG.NRS_BR_CD                                 " & vbNewLine _
                                           & "   AND MC.CUST_CD_L = MG.CUST_CD_L                                 " & vbNewLine _
                                           & "   AND MC.CUST_CD_M = MG.CUST_CD_M                                 " & vbNewLine _
                                           & "   AND MC.CUST_CD_S = MG.CUST_CD_S                                 " & vbNewLine _
                                           & "   AND MC.CUST_CD_SS = MG.CUST_CD_SS                               " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_SEIQTO MS                                 " & vbNewLine _
                                           & "   ON  MS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                           & "   AND MS.NRS_BR_CD = SE.NRS_BR_CD                                 " & vbNewLine _
                                           & "   AND MS.SEIQTO_CD = MC.HOKAN_SEIQTO_CD                           " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_NRS_BR MB                                 " & vbNewLine _
                                           & "   ON  MB.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                           & "   AND MB.NRS_BR_CD = SE.NRS_BR_CD                                 " & vbNewLine _
                                           & "   --荷主帳票パターン取得                                          " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                             " & vbNewLine _
                                           & "   ON  SE.NRS_BR_CD = MCR1.NRS_BR_CD                               " & vbNewLine _
                                           & "   AND MG.CUST_CD_L = MCR1.CUST_CD_L                               " & vbNewLine _
                                           & "   AND MG.CUST_CD_M = MCR1.CUST_CD_M                               " & vbNewLine _
                                           & "   AND '00' = MCR1.CUST_CD_S                                       " & vbNewLine _
                                           & "   AND MCR1.PTN_ID = '52'                                          " & vbNewLine _
                                           & "   --帳票パターン取得                                              " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_RPT MR1                                   " & vbNewLine _
                                           & "   ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                              " & vbNewLine _
                                           & "   AND MR1.PTN_ID = MCR1.PTN_ID                                    " & vbNewLine _
                                           & "   AND MR1.PTN_CD = MCR1.PTN_CD                                    " & vbNewLine _
                                           & "   AND MR1.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                           & "   --商品Mの荷主での荷主帳票パターン取得                           " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                             " & vbNewLine _
                                           & "   ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                               " & vbNewLine _
                                           & "   AND MG.CUST_CD_L = MCR2.CUST_CD_L                               " & vbNewLine _
                                           & "   AND MG.CUST_CD_M = MCR2.CUST_CD_M                               " & vbNewLine _
                                           & "   AND MG.CUST_CD_S = MCR2.CUST_CD_S                               " & vbNewLine _
                                           & "   AND MCR2.PTN_ID = '52'                                          " & vbNewLine _
                                           & "   --帳票パターン取得                                              " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_RPT MR2                                   " & vbNewLine _
                                           & "   ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                              " & vbNewLine _
                                           & "   AND MR2.PTN_ID = MCR2.PTN_ID                                    " & vbNewLine _
                                           & "   AND MR2.PTN_CD = MCR2.PTN_CD                                    " & vbNewLine _
                                           & "   AND MR2.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                           & "   --存在しない場合の帳票パターン取得                              " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..M_RPT MR3                                   " & vbNewLine _
                                           & "   ON  MR3.NRS_BR_CD = SE.NRS_BR_CD                                " & vbNewLine _
                                           & "   AND MR3.PTN_ID = '52'                                           " & vbNewLine _
                                           & "   AND MR3.STANDARD_FLAG = '01'                                    " & vbNewLine _
                                           & "   AND MR3.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..Z_KBN KBN_OFB                               " & vbNewLine _
                                           & "   ON  KBN_OFB.KBN_GROUP_CD = 'B002'                               " & vbNewLine _
                                           & "   AND KBN_OFB.KBN_CD = SE.OFB_KB                                  " & vbNewLine _
                                           & "   AND KBN_OFB.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                           & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                           & "   LEFT JOIN $LM_MST$..Z_KBN AS KBNC025_ITEM                       " & vbNewLine _
                                           & "   ON  KBNC025_ITEM.KBN_GROUP_CD = 'C025'                          " & vbNewLine _
                                           & "   AND KBNC025_ITEM.KBN_NM6      =  (CASE WHEN MC.ITEM_CURR_CD = '' THEN 'JPY' ELSE MC.ITEM_CURR_CD END) " & vbNewLine _
                                           & "   AND KBNC025_ITEM.SYS_DEL_FLG  = '0'                             " & vbNewLine _
                                           & "   LEFT JOIN COM_DB..M_CURR AS CURR_ITEM                           " & vbNewLine _
                                           & "   ON  CURR_ITEM.BASE_CURR_CD = MS.SEIQ_CURR_CD                    " & vbNewLine _
                                           & "   AND CURR_ITEM.CURR_CD      = MC.ITEM_CURR_CD                    " & vbNewLine _
                                           & "   AND CURR_ITEM.SYS_DEL_FLG  = '0'                                " & vbNewLine _
                                           & "   AND CURR_ITEM.UP_FLG       = '00000'                            " & vbNewLine _
                                           & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine _
                                           & "   LEFT JOIN                                                       " & vbNewLine _
                                           & "       LM_MST..Z_KBN RPT_CHG_START_YM                              " & vbNewLine _
                                           & "           ON  RPT_CHG_START_YM.KBN_GROUP_CD = 'B043'              " & vbNewLine _
                                           & "           AND RPT_CHG_START_YM.KBN_CD       = '01'                " & vbNewLine _
                                           & "           AND RPT_CHG_START_YM.SYS_DEL_FLG  = '0'                 " & vbNewLine _
                                           & "   LEFT JOIN                                                       " & vbNewLine _
                                           & "       LM_MST..Z_KBN OLD_NRS_BR_NM                                 " & vbNewLine _
                                           & "           ON  OLD_NRS_BR_NM.KBN_GROUP_CD = 'B044'                 " & vbNewLine _
                                           & "           AND OLD_NRS_BR_NM.KBN_CD       =  SE.NRS_BR_CD          " & vbNewLine _
                                           & "           AND OLD_NRS_BR_NM.SYS_DEL_FLG =  '0'                    " & vbNewLine _
                                           & "   WHERE                                                           " & vbNewLine _
                                           & "       SE.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                           & "   AND SE.SEKY_FLG = @SEKY_FLG                                     " & vbNewLine _
                                           & "   AND SE.SYS_ENT_PGID <> 'IKOU '                                  " & vbNewLine

    ''' <summary>
    ''' データ抽出用FROM句(保管料504)
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private Const SQL_FROM_HOKAN_504 As String = "   FROM                                                            " & vbNewLine _
                                               & "   $LM_TRN$..G_SEKY_MEISAI_PRT SE                                  " & vbNewLine _
                                               & "   LEFT JOIN $LM_MST$..M_GOODS MG                                    " & vbNewLine _
                                               & "   ON  MG.NRS_BR_CD = SE.NRS_BR_CD                                 " & vbNewLine _
                                               & "   AND MG.GOODS_CD_NRS = SE.GOODS_CD_NRS                           " & vbNewLine _
                                               & "   LEFT JOIN $LM_MST$..M_CUST MC                                   " & vbNewLine _
                                               & "   ON  MC.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                               & "   AND MC.NRS_BR_CD = MG.NRS_BR_CD                                 " & vbNewLine _
                                               & "   AND MC.CUST_CD_L = MG.CUST_CD_L                                 " & vbNewLine _
                                               & "   AND MC.CUST_CD_M = MG.CUST_CD_M                                 " & vbNewLine _
                                               & "   AND MC.CUST_CD_S = MG.CUST_CD_S                                 " & vbNewLine _
                                               & "   AND MC.CUST_CD_SS = MG.CUST_CD_SS                               " & vbNewLine _
                                               & "   LEFT JOIN $LM_MST$..M_SEIQTO MS                                 " & vbNewLine _
                                               & "   ON  MS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                               & "   AND MS.NRS_BR_CD = SE.NRS_BR_CD                                 " & vbNewLine _
                                               & "   AND MS.SEIQTO_CD = MC.HOKAN_SEIQTO_CD                           " & vbNewLine _
                                               & "   LEFT JOIN $LM_MST$..M_NRS_BR MB                                 " & vbNewLine _
                                               & "   ON  MB.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                               & "   AND MB.NRS_BR_CD = SE.NRS_BR_CD                                 " & vbNewLine _
                                               & "   --荷主帳票パターン取得                                          " & vbNewLine _
                                               & "   LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                             " & vbNewLine _
                                               & "   ON  SE.NRS_BR_CD = MCR1.NRS_BR_CD                               " & vbNewLine _
                                               & "   AND MG.CUST_CD_L = MCR1.CUST_CD_L                               " & vbNewLine _
                                               & "   AND MG.CUST_CD_M = MCR1.CUST_CD_M                               " & vbNewLine _
                                               & "   AND '00' = MCR1.CUST_CD_S                                       " & vbNewLine _
                                               & "   AND MCR1.PTN_ID = '52'                                          " & vbNewLine _
                                               & "   --帳票パターン取得                                              " & vbNewLine _
                                               & "   LEFT JOIN $LM_MST$..M_RPT MR1                                   " & vbNewLine _
                                               & "   ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                              " & vbNewLine _
                                               & "   AND MR1.PTN_ID = MCR1.PTN_ID                                    " & vbNewLine _
                                               & "   AND MR1.PTN_CD = MCR1.PTN_CD                                    " & vbNewLine _
                                               & "   AND MR1.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                               & "   --商品Mの荷主での荷主帳票パターン取得                                                                         " & vbNewLine _
                                               & "   LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                             " & vbNewLine _
                                               & "   ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                               " & vbNewLine _
                                               & "   AND MG.CUST_CD_L = MCR2.CUST_CD_L                               " & vbNewLine _
                                               & "   AND MG.CUST_CD_M = MCR2.CUST_CD_M                               " & vbNewLine _
                                               & "   AND MG.CUST_CD_S = MCR2.CUST_CD_S                               " & vbNewLine _
                                               & "   AND MCR2.PTN_ID = '52'                                          " & vbNewLine _
                                               & "   --帳票パターン取得                                              " & vbNewLine _
                                               & "   LEFT JOIN $LM_MST$..M_RPT MR2                                   " & vbNewLine _
                                               & "   ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                              " & vbNewLine _
                                               & "   AND MR2.PTN_ID = MCR2.PTN_ID                                    " & vbNewLine _
                                               & "   AND MR2.PTN_CD = MCR2.PTN_CD                                    " & vbNewLine _
                                               & "   AND MR2.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                               & "   --存在しない場合の帳票パターン取得                              " & vbNewLine _
                                               & "   LEFT JOIN $LM_MST$..M_RPT MR3                                   " & vbNewLine _
                                               & "   ON  MR3.NRS_BR_CD = SE.NRS_BR_CD                                " & vbNewLine _
                                               & "   AND MR3.PTN_ID = '52'                                           " & vbNewLine _
                                               & "   AND MR3.STANDARD_FLAG = '01'                                    " & vbNewLine _
                                               & "   AND MR3.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                               & "   LEFT JOIN $LM_MST$..Z_KBN KBN_OFB                               " & vbNewLine _
                                               & "   ON  KBN_OFB.KBN_GROUP_CD = 'B002'                               " & vbNewLine _
                                               & "   AND KBN_OFB.KBN_CD = SE.OFB_KB                                  " & vbNewLine _
                                               & "   AND KBN_OFB.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                               & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                               & "   LEFT JOIN $LM_MST$..Z_KBN AS KBNC025_ITEM                       " & vbNewLine _
                                               & "   ON  KBNC025_ITEM.KBN_GROUP_CD = 'C025'                          " & vbNewLine _
                                               & "   AND KBNC025_ITEM.KBN_NM6      =  (CASE WHEN MC.ITEM_CURR_CD = '' THEN 'JPY' ELSE MC.ITEM_CURR_CD END) " & vbNewLine _
                                               & "   AND KBNC025_ITEM.SYS_DEL_FLG  = '0'                             " & vbNewLine _
                                               & "   LEFT JOIN COM_DB..M_CURR AS CURR_ITEM                           " & vbNewLine _
                                               & "   ON  CURR_ITEM.BASE_CURR_CD = MS.SEIQ_CURR_CD                    " & vbNewLine _
                                               & "   AND CURR_ITEM.CURR_CD      = MC.ITEM_CURR_CD                    " & vbNewLine _
                                               & "   AND CURR_ITEM.SYS_DEL_FLG  = '0'                                " & vbNewLine _
                                               & "   AND CURR_ITEM.UP_FLG       = '00000'                            " & vbNewLine _
                                               & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine _
                                               & "   LEFT JOIN                                                       " & vbNewLine _
                                               & "       LM_MST..Z_KBN RPT_CHG_START_YM                              " & vbNewLine _
                                               & "           ON  RPT_CHG_START_YM.KBN_GROUP_CD = 'B043'              " & vbNewLine _
                                               & "           AND RPT_CHG_START_YM.KBN_CD       = '01'                " & vbNewLine _
                                               & "           AND RPT_CHG_START_YM.SYS_DEL_FLG  = '0'                 " & vbNewLine _
                                               & "   LEFT JOIN                                                       " & vbNewLine _
                                               & "       LM_MST..Z_KBN OLD_NRS_BR_NM                                 " & vbNewLine _
                                               & "           ON  OLD_NRS_BR_NM.KBN_GROUP_CD = 'B044'                 " & vbNewLine _
                                               & "           AND OLD_NRS_BR_NM.KBN_CD       =  SE.NRS_BR_CD          " & vbNewLine _
                                               & "           AND OLD_NRS_BR_NM.SYS_DEL_FLG =  '0'                    " & vbNewLine _
                                               & "   WHERE                                                           " & vbNewLine _
                                               & "       SE.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                               & "   AND SE.SEKY_FLG = '00'                                          " & vbNewLine _
                                               & "   AND SE.SYS_ENT_PGID <> 'IKOU '                                  " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT句(荷役料)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_NIYAKU As String = "   --荷役料                                                        " & vbNewLine _
                                              & "   UNION ALL                                                       " & vbNewLine _
                                              & "   SELECT                                                  " & vbNewLine _
                                              & "   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                              & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                " & vbNewLine _
                                              & "        ELSE MR3.RPT_ID END          AS RPT_ID                     " & vbNewLine _
                                              & "   ,SE.NRS_BR_CD                     AS NRS_BR_CD                  " & vbNewLine _
                                              & "   ,MS.SEIQTO_CD                     AS SEIQTO_CD                  " & vbNewLine _
                                              & "   ,MS.SEIQTO_NM                     AS SEIQTO_NM                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_L                     AS CUST_CD_L                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_M                     AS CUST_CD_M                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_S                     AS CUST_CD_S                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_SS                    AS CUST_CD_SS                 " & vbNewLine _
                                              & "   ,MC.CUST_NM_L                     AS CUST_NM_L                  " & vbNewLine _
                                              & "   ,MC.CUST_NM_M                     AS CUST_NM_M                  " & vbNewLine _
                                              & "   ,MC.CUST_NM_S                     AS CUST_NM_S                  " & vbNewLine _
                                              & "   ,MC.CUST_NM_SS                    AS CUST_NM_SS                 " & vbNewLine _
                                              & "   ,MG.SEARCH_KEY_1                  AS SEARCH_KEY_1               " & vbNewLine _
                                              & "   ,SE.JOB_NO                        AS JOB_NO                     " & vbNewLine _
                                              & "   ,CASE WHEN SUBSTRING(SE.INV_DATE_TO, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                              & "         THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, MB.NRS_BR_NM)          " & vbNewLine _
                                              & "         ELSE MB.NRS_BR_NM                                         " & vbNewLine _
                                              & "         END  AS NRS_BR_NM                                         " & vbNewLine _
                                              & "   ,SE.INV_DATE_FROM                 AS INV_DATE_FROM              " & vbNewLine _
                                              & "   ,SE.INV_DATE_TO                   AS INV_DATE_TO                " & vbNewLine _
                                              & "   ,SE.OFB_KB                        AS OFB_KB                     " & vbNewLine _
                                              & "   ,KBN_OFB.KBN_NM1                  AS OFB_KB_NM                  " & vbNewLine _
                                              & "   ,MG.GOODS_NM_1                    AS GOODS_NM_1                 " & vbNewLine _
                                              & "    ,''                              AS PKG_UT                     " & vbNewLine _
                                              & "   ,MG.GOODS_CD_CUST                 AS GOODS_CD_CUST              " & vbNewLine _
                                              & "   ,SE.SERIAL_NO                     AS SERIAL_NO                  " & vbNewLine _
                                              & "   ,SE.LOT_NO                        AS LOT_NO                     " & vbNewLine _
                                              & "   ,MG.NB_UT                         AS NB_UT_NM                   " & vbNewLine _
                                              & "   ,MG.STD_IRIME_UT                  AS IRIME_UT_NM                " & vbNewLine _
                                              & "   ,SE.IRIME                         AS IRIME                      " & vbNewLine _
                                              & "   ,SE.INKA_NO_L                     AS INKA_NO_L                  " & vbNewLine _
                                              & "   ,SE.KISYZAN_NB1                   AS KISYZAN_NB1                " & vbNewLine _
                                              & "   ,SE.KISYZAN_NB2                   AS KISYZAN_NB2                " & vbNewLine _
                                              & "   ,SE.KISYZAN_NB3                   AS KISYZAN_NB3                " & vbNewLine _
                                              & "   ,SE.MATUZAN_NB                    AS MATUZAN_NB                 " & vbNewLine _
                                              & "   ,SE.INKO_NB_TTL2                  AS INKO_NB_TTL2               " & vbNewLine _
                                              & "   ,SE.INKO_NB_TTL1                  AS INKO_NB_TTL1               " & vbNewLine _
                                              & "   ,SE.OUTKO_NB_TTL2                 AS OUTKO_NB_TTL2              " & vbNewLine _
                                              & "   ,SE.OUTKO_NB_TTL1                 AS OUTKO_NB_TTL1              " & vbNewLine _
                                              & "   ,0                                AS SEKI_ARI_NB1               " & vbNewLine _
                                              & "   ,0                                AS SEKI_ARI_NB2               " & vbNewLine _
                                              & "   ,0                                AS SEKI_ARI_NB3               " & vbNewLine _
                                              & "   ,0                                AS STORAGE1                   " & vbNewLine _
                                              & "   ,0                                AS STORAGE2                   " & vbNewLine _
                                              & "   ,0                                AS STORAGE3                   " & vbNewLine _
                                              & "   ,0                                AS STORAGE_AMO_TTL            " & vbNewLine _
                                              & "   ,SE.INKO_NB_TTL1 + SE.OUTKO_NB_TTL1 - (SE.INKO_NB_TTL2 + SE.OUTKO_NB_TTL2) AS HANDLING_NB_TTL          " & vbNewLine _
                                              & "   ,SE.HANDLING_IN1                  AS HANDLING_IN1               " & vbNewLine _
                                              & "   ,SE.HANDLING_AMO_TTL              AS HANDLING_AMO_TTL           " & vbNewLine _
                                              & "   ,SE.KURIKOSI_DATE1                AS KURIKOSI_DATE1             " & vbNewLine _
                                              & "   ,SE.KURIKOSI_DATE2                AS KURIKOSI_DATE2             " & vbNewLine _
                                              & "   ,SE.KURIKOSI_DATE3                AS KURIKOSI_DATE3             " & vbNewLine _
                                              & "   ,SE.MATU_DATE                     AS MATU_DATE                  " & vbNewLine _
                                              & "   --デュポン用帳票（LMG502）項目                                  " & vbNewLine _
                                              & "   ,SE.TAX_KB                        AS TAX_KB                     " & vbNewLine _
                                              & "   ,MG.CUST_COST_CD2                 AS CUST_COST_CD2              " & vbNewLine _
                                              & "   ,MG.GOODS_NM_2                    AS GOODS_NM_2                 " & vbNewLine _
                                              & "   ,MG.GOODS_NM_3                    AS GOODS_NM_3                 " & vbNewLine _
                                              & "   ,SE.INKO_NB1                      AS INKO_NB1                   " & vbNewLine _
                                              & "   ,SE.INKO_NB2                      AS INKO_NB2                   " & vbNewLine _
                                              & "   ,SE.INKO_NB3                      AS INKO_NB3                   " & vbNewLine _
                                              & "   ,SE.OUTKO_NB1                     AS OUTKO_NB1                  " & vbNewLine _
                                              & "   ,SE.OUTKO_NB2                     AS OUTKO_NB2                  " & vbNewLine _
                                              & "   ,SE.OUTKO_NB3                     AS OUTKO_NB3                  " & vbNewLine _
                                              & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                              & "   ,KBNC025_ITEM.KBN_NM1             AS STO_ITEM_CURR_CD           " & vbNewLine _
                                              & "   ,KBNC025_ITEM.KBN_NM1             AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                              & "   ,ISNULL(CURR_ITEM.ROUND_POS,0)    AS ITEM_ROUND_POS             " & vbNewLine _
                                              & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT句(荷役料LMG503用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_NIYAKU_LMG503 As String = "   --荷役料                                                        " & vbNewLine _
                                              & "   UNION ALL                                                       " & vbNewLine _
                                              & "   SELECT                                                  " & vbNewLine _
                                              & "   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                              & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                " & vbNewLine _
                                              & "        ELSE MR3.RPT_ID END          AS RPT_ID                     " & vbNewLine _
                                              & "   ,SE.NRS_BR_CD                     AS NRS_BR_CD                  " & vbNewLine _
                                              & "   ,MS.SEIQTO_CD                     AS SEIQTO_CD                  " & vbNewLine _
                                              & "   ,MS.SEIQTO_NM                     AS SEIQTO_NM                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_L                     AS CUST_CD_L                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_M                     AS CUST_CD_M                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_S                     AS CUST_CD_S                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_SS                    AS CUST_CD_SS                 " & vbNewLine _
                                              & "   ,MC.CUST_NM_L                     AS CUST_NM_L                  " & vbNewLine _
                                              & "   ,MC.CUST_NM_M                     AS CUST_NM_M                  " & vbNewLine _
                                              & "   ,MC.CUST_NM_S                     AS CUST_NM_S                  " & vbNewLine _
                                              & "   ,MC.CUST_NM_SS                    AS CUST_NM_SS                 " & vbNewLine _
                                              & "   ,MG.SEARCH_KEY_1                  AS SEARCH_KEY_1               " & vbNewLine _
                                              & "   ,SE.JOB_NO                        AS JOB_NO                     " & vbNewLine _
                                              & "   ,CASE WHEN SUBSTRING(SE.INV_DATE_TO, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                              & "         THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, MB.NRS_BR_NM)          " & vbNewLine _
                                              & "         ELSE MB.NRS_BR_NM                                         " & vbNewLine _
                                              & "         END  AS NRS_BR_NM                                         " & vbNewLine _
                                              & "   ,SE.INV_DATE_FROM                 AS INV_DATE_FROM              " & vbNewLine _
                                              & "   ,SE.INV_DATE_TO                   AS INV_DATE_TO                " & vbNewLine _
                                              & "   ,SE.OFB_KB                        AS OFB_KB                     " & vbNewLine _
                                              & "   ,KBN_OFB.KBN_NM1                  AS OFB_KB_NM                  " & vbNewLine _
                                              & "   ,MG.GOODS_NM_1                    AS GOODS_NM_1                 " & vbNewLine _
                                              & "   ,MG.PKG_UT                        AS PKG_UT                     " & vbNewLine _
                                              & "   ,MG.GOODS_CD_CUST                 AS GOODS_CD_CUST              " & vbNewLine _
                                              & "   ,SE.SERIAL_NO                     AS SERIAL_NO                  " & vbNewLine _
                                              & "   ,SE.LOT_NO                        AS LOT_NO                     " & vbNewLine _
                                              & "   ,MG.NB_UT                         AS NB_UT_NM                   " & vbNewLine _
                                              & "   ,MG.STD_IRIME_UT                  AS IRIME_UT_NM                " & vbNewLine _
                                              & "   ,SE.IRIME                         AS IRIME                      " & vbNewLine _
                                              & "   ,SE.INKA_NO_L                     AS INKA_NO_L                  " & vbNewLine _
                                              & "   ,SE.KISYZAN_NB1                   AS KISYZAN_NB1                " & vbNewLine _
                                              & "   ,SE.KISYZAN_NB2                   AS KISYZAN_NB2                " & vbNewLine _
                                              & "   ,SE.KISYZAN_NB3                   AS KISYZAN_NB3                " & vbNewLine _
                                              & "   ,SE.MATUZAN_NB                    AS MATUZAN_NB                 " & vbNewLine _
                                              & "   ,SE.INKO_NB_TTL2                  AS INKO_NB_TTL2               " & vbNewLine _
                                              & "   ,SE.INKO_NB_TTL1                  AS INKO_NB_TTL1               " & vbNewLine _
                                              & "   ,SE.OUTKO_NB_TTL2                 AS OUTKO_NB_TTL2              " & vbNewLine _
                                              & "   ,SE.OUTKO_NB_TTL1                 AS OUTKO_NB_TTL1              " & vbNewLine _
                                              & "   ,0                                AS SEKI_ARI_NB1               " & vbNewLine _
                                              & "   ,0                                AS SEKI_ARI_NB2               " & vbNewLine _
                                              & "   ,0                                AS SEKI_ARI_NB3               " & vbNewLine _
                                              & "   ,0                                AS STORAGE1                   " & vbNewLine _
                                              & "   ,0                                AS STORAGE2                   " & vbNewLine _
                                              & "   ,0                                AS STORAGE3                   " & vbNewLine _
                                              & "   ,0                                AS STORAGE_AMO_TTL            " & vbNewLine _
                                              & "   ,SE.INKO_NB_TTL1 + SE.OUTKO_NB_TTL1 - (SE.INKO_NB_TTL2 + SE.OUTKO_NB_TTL2) AS HANDLING_NB_TTL          " & vbNewLine _
                                              & "   ,SE.HANDLING_IN1                  AS HANDLING_IN1               " & vbNewLine _
                                              & "   ,SE.HANDLING_AMO_TTL              AS HANDLING_AMO_TTL           " & vbNewLine _
                                              & "   ,SE.KURIKOSI_DATE1                AS KURIKOSI_DATE1             " & vbNewLine _
                                              & "   ,SE.KURIKOSI_DATE2                AS KURIKOSI_DATE2             " & vbNewLine _
                                              & "   ,SE.KURIKOSI_DATE3                AS KURIKOSI_DATE3             " & vbNewLine _
                                              & "   ,SE.MATU_DATE                     AS MATU_DATE                  " & vbNewLine _
                                              & "   --デュポン用帳票（LMG502）項目                                  " & vbNewLine _
                                              & "   ,SE.TAX_KB                        AS TAX_KB                     " & vbNewLine _
                                              & "   ,MG.CUST_COST_CD2                 AS CUST_COST_CD2              " & vbNewLine _
                                              & "   ,MG.GOODS_NM_2                    AS GOODS_NM_2                 " & vbNewLine _
                                              & "   ,MG.GOODS_NM_3                    AS GOODS_NM_3                 " & vbNewLine _
                                              & "   ,SE.INKO_NB1                      AS INKO_NB1                   " & vbNewLine _
                                              & "   ,SE.INKO_NB2                      AS INKO_NB2                   " & vbNewLine _
                                              & "   ,SE.INKO_NB3                      AS INKO_NB3                   " & vbNewLine _
                                              & "   ,SE.OUTKO_NB1                     AS OUTKO_NB1                  " & vbNewLine _
                                              & "   ,SE.OUTKO_NB2                     AS OUTKO_NB2                  " & vbNewLine _
                                              & "   ,SE.OUTKO_NB3                     AS OUTKO_NB3                  " & vbNewLine _
                                              & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                              & "   ,KBNC025_ITEM.KBN_NM1             AS STO_ITEM_CURR_CD           " & vbNewLine _
                                              & "   ,KBNC025_ITEM.KBN_NM1             AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                              & "   ,ISNULL(CURR_ITEM.ROUND_POS,0)    AS ITEM_ROUND_POS             " & vbNewLine _
                                              & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine


    ''' <summary>
    ''' データ抽出用SELECT句(荷役料504)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_NIYAKU_504 As String = "   --荷役料                                                        " & vbNewLine _
                                                  & "   UNION ALL                                                       " & vbNewLine _
                                                  & "   SELECT                                                  " & vbNewLine _
                                                  & "   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                                  & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                " & vbNewLine _
                                                  & "        ELSE MR3.RPT_ID END          AS RPT_ID                     " & vbNewLine _
                                                  & "   ,SE.NRS_BR_CD                     AS NRS_BR_CD                  " & vbNewLine _
                                                  & "   ,MS.SEIQTO_CD                     AS SEIQTO_CD                  " & vbNewLine _
                                                  & "   ,MS.SEIQTO_NM                     AS SEIQTO_NM                  " & vbNewLine _
                                                  & "   ,MG.CUST_CD_L                     AS CUST_CD_L                  " & vbNewLine _
                                                  & "   ,MG.CUST_CD_M                     AS CUST_CD_M                  " & vbNewLine _
                                                  & "   ,MG.CUST_CD_S                     AS CUST_CD_S                  " & vbNewLine _
                                                  & "   ,MG.CUST_CD_SS                    AS CUST_CD_SS                 " & vbNewLine _
                                                  & "   ,MC.CUST_NM_L                     AS CUST_NM_L                  " & vbNewLine _
                                                  & "   ,MC.CUST_NM_M                     AS CUST_NM_M                  " & vbNewLine _
                                                  & "   ,MC.CUST_NM_S                     AS CUST_NM_S                  " & vbNewLine _
                                                  & "   ,MC.CUST_NM_SS                    AS CUST_NM_SS                 " & vbNewLine _
                                                  & "   ,MG.SEARCH_KEY_1                  AS SEARCH_KEY_1               " & vbNewLine _
                                                  & "   ,MG.SEARCH_KEY_2                  AS SEARCH_KEY_2               " & vbNewLine _
                                                  & "   ,SE.JOB_NO                        AS JOB_NO                     " & vbNewLine _
                                                  & "   , CASE WHEN SUBSTRING(SE.INV_DATE_TO, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                                  & "         THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, MB.NRS_BR_NM)          " & vbNewLine _
                                                  & "         ELSE MB.NRS_BR_NM                                         " & vbNewLine _
                                                  & "         END  AS NRS_BR_NM                                         " & vbNewLine _
                                                  & "   ,SE.INV_DATE_FROM                 AS INV_DATE_FROM              " & vbNewLine _
                                                  & "   ,SE.INV_DATE_TO                   AS INV_DATE_TO                " & vbNewLine _
                                                  & "   ,SE.OFB_KB                        AS OFB_KB                     " & vbNewLine _
                                                  & "   ,KBN_OFB.KBN_NM1                  AS OFB_KB_NM                  " & vbNewLine _
                                                  & "   ,MG.GOODS_NM_1                    AS GOODS_NM_1                 " & vbNewLine _
                                                  & "   ,''                               AS PKG_UT                     " & vbNewLine _
                                                  & "   ,MG.GOODS_CD_CUST                 AS GOODS_CD_CUST              " & vbNewLine _
                                                  & "   ,SE.SERIAL_NO                     AS SERIAL_NO                  " & vbNewLine _
                                                  & "   ,SE.LOT_NO                        AS LOT_NO                     " & vbNewLine _
                                                  & "   ,MG.NB_UT                         AS NB_UT_NM                   " & vbNewLine _
                                                  & "   ,MG.STD_IRIME_UT                  AS IRIME_UT_NM                " & vbNewLine _
                                                  & "   ,SE.IRIME                         AS IRIME                      " & vbNewLine _
                                                  & "   ,SUM(SE.KISYZAN_NB1)              AS KISYZAN_NB1                " & vbNewLine _
                                                  & "   ,SUM(SE.KISYZAN_NB2)              AS KISYZAN_NB2                " & vbNewLine _
                                                  & "   ,SUM(SE.KISYZAN_NB3)              AS KISYZAN_NB3                " & vbNewLine _
                                                  & "   ,SUM(SE.MATUZAN_NB)               AS MATUZAN_NB                 " & vbNewLine _
                                                  & "   ,SUM(SE.INKO_NB_TTL2)             AS INKO_NB_TTL2               " & vbNewLine _
                                                  & "   ,SUM(SE.INKO_NB_TTL1)             AS INKO_NB_TTL1               " & vbNewLine _
                                                  & "   ,SUM(SE.OUTKO_NB_TTL2)            AS OUTKO_NB_TTL2              " & vbNewLine _
                                                  & "   ,SUM(SE.OUTKO_NB_TTL1)            AS OUTKO_NB_TTL1              " & vbNewLine _
                                                  & "   ,0                                AS SEKI_ARI_NB1               " & vbNewLine _
                                                  & "   ,0                                AS SEKI_ARI_NB2               " & vbNewLine _
                                                  & "   ,0                                AS SEKI_ARI_NB3               " & vbNewLine _
                                                  & "   ,0                                AS STORAGE1                   " & vbNewLine _
                                                  & "   ,0                                AS STORAGE2                   " & vbNewLine _
                                                  & "   ,0                                AS STORAGE3                   " & vbNewLine _
                                                  & "   ,0                                AS STORAGE_AMO_TTL            " & vbNewLine _
                                                  & "   ,SUM(SE.INKO_NB_TTL1) + SUM(SE.OUTKO_NB_TTL1) - (SUM(SE.INKO_NB_TTL2) + SUM(SE.OUTKO_NB_TTL2)) AS HANDLING_NB_TTL " & vbNewLine _
                                                  & "   ,SE.HANDLING_IN1                  AS HANDLING_IN1               " & vbNewLine _
                                                  & "   ,SE.HANDLING_IN2                  AS HANDLING_IN2               " & vbNewLine _
                                                  & "   ,SE.HANDLING_IN3                  AS HANDLING_IN3               " & vbNewLine _
                                                  & "   ,SUM(SE.HANDLING_AMO_TTL)         AS HANDLING_AMO_TTL           " & vbNewLine _
                                                  & "   ,SE.KURIKOSI_DATE1                AS KURIKOSI_DATE1             " & vbNewLine _
                                                  & "   ,SE.KURIKOSI_DATE2                AS KURIKOSI_DATE2             " & vbNewLine _
                                                  & "   ,SE.KURIKOSI_DATE3                AS KURIKOSI_DATE3             " & vbNewLine _
                                                  & "   ,SE.MATU_DATE                     AS MATU_DATE                  " & vbNewLine _
                                                  & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                                  & "   ,KBNC025_ITEM.KBN_NM1             AS STO_ITEM_CURR_CD           " & vbNewLine _
                                                  & "   ,KBNC025_ITEM.KBN_NM1             AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                                  & "   ,ISNULL(CURR_ITEM.ROUND_POS,0)    AS ITEM_ROUND_POS             " & vbNewLine _
                                                  & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT句(荷役料)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_NIYAKU_518 As String = "   --荷役料                                                    " & vbNewLine _
                                              & "   UNION ALL                                                       " & vbNewLine _
                                              & "   SELECT                                                          " & vbNewLine _
                                              & "   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                              & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                " & vbNewLine _
                                              & "        ELSE MR3.RPT_ID END          AS RPT_ID                     " & vbNewLine _
                                              & "   ,SE.NRS_BR_CD                     AS NRS_BR_CD                  " & vbNewLine _
                                              & "   ,MS.SEIQTO_CD                     AS SEIQTO_CD                  " & vbNewLine _
                                              & "   ,MS.SEIQTO_NM                     AS SEIQTO_NM                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_L                     AS CUST_CD_L                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_M                     AS CUST_CD_M                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_S                     AS CUST_CD_S                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_SS                    AS CUST_CD_SS                 " & vbNewLine _
                                              & "   ,MC.CUST_NM_L                     AS CUST_NM_L                  " & vbNewLine _
                                              & "   ,MC.CUST_NM_M                     AS CUST_NM_M                  " & vbNewLine _
                                              & "   ,MC.CUST_NM_S                     AS CUST_NM_S                  " & vbNewLine _
                                              & "   ,MC.CUST_NM_SS                    AS CUST_NM_SS                 " & vbNewLine _
                                              & "   ,MG.SEARCH_KEY_1                  AS SEARCH_KEY_1               " & vbNewLine _
                                              & "   ,SE.JOB_NO                        AS JOB_NO                     " & vbNewLine _
                                              & "   ,CASE WHEN SUBSTRING(SE.INV_DATE_TO, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                              & "         THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, MB.NRS_BR_NM)          " & vbNewLine _
                                              & "         ELSE MB.NRS_BR_NM                                         " & vbNewLine _
                                              & "         END  AS NRS_BR_NM                                         " & vbNewLine _
                                              & "   ,SE.INV_DATE_FROM                 AS INV_DATE_FROM              " & vbNewLine _
                                              & "   ,SE.INV_DATE_TO                   AS INV_DATE_TO                " & vbNewLine _
                                              & "   ,SE.OFB_KB                        AS OFB_KB                     " & vbNewLine _
                                              & "   ,KBN_OFB.KBN_NM1                  AS OFB_KB_NM                  " & vbNewLine _
                                              & "   ,MG.GOODS_NM_1                    AS GOODS_NM_1                 " & vbNewLine _
                                              & "    ,''                              AS PKG_UT                     " & vbNewLine _
                                              & "   ,MG.GOODS_CD_CUST                 AS GOODS_CD_CUST              " & vbNewLine _
                                              & "   ,SE.SERIAL_NO                     AS SERIAL_NO                  " & vbNewLine _
                                              & "   ,SE.LOT_NO                        AS LOT_NO                     " & vbNewLine _
                                              & "   ,MG.NB_UT                         AS NB_UT_NM                   " & vbNewLine _
                                              & "   ,MG.STD_IRIME_UT                  AS IRIME_UT_NM                " & vbNewLine _
                                              & "   ,SE.IRIME                         AS IRIME                      " & vbNewLine _
                                              & "   ,SE.INKA_NO_L                     AS INKA_NO_L                  " & vbNewLine _
                                              & "   ,SE.KISYZAN_NB1                   AS KISYZAN_NB1                " & vbNewLine _
                                              & "   ,SE.KISYZAN_NB2                   AS KISYZAN_NB2                " & vbNewLine _
                                              & "   ,SE.KISYZAN_NB3                   AS KISYZAN_NB3                " & vbNewLine _
                                              & "   ,SE.MATUZAN_NB                    AS MATUZAN_NB                 " & vbNewLine _
                                              & "   ,SE.INKO_NB_TTL2                  AS INKO_NB_TTL2               " & vbNewLine _
                                              & "   ,SE.INKO_NB_TTL1                  AS INKO_NB_TTL1               " & vbNewLine _
                                              & "   ,SE.OUTKO_NB_TTL2                 AS OUTKO_NB_TTL2              " & vbNewLine _
                                              & "   ,SE.OUTKO_NB_TTL1                 AS OUTKO_NB_TTL1              " & vbNewLine _
                                              & "   ,0                                AS SEKI_ARI_NB1               " & vbNewLine _
                                              & "   ,0                                AS SEKI_ARI_NB2               " & vbNewLine _
                                              & "   ,0                                AS SEKI_ARI_NB3               " & vbNewLine _
                                              & "   ,0                                AS STORAGE1                   " & vbNewLine _
                                              & "   ,0                                AS STORAGE2                   " & vbNewLine _
                                              & "   ,0                                AS STORAGE3                   " & vbNewLine _
                                              & "   ,0                                AS STORAGE_AMO_TTL            " & vbNewLine _
                                              & "   ,SE.INKO_NB_TTL1 + SE.OUTKO_NB_TTL1 - (SE.INKO_NB_TTL2 + SE.OUTKO_NB_TTL2) AS HANDLING_NB_TTL          " & vbNewLine _
                                              & "   ,SE.HANDLING_IN1                  AS HANDLING_IN1               " & vbNewLine _
                                              & "   ,SE.HANDLING_OUT1                 AS HANDLING_OUT1              " & vbNewLine _
                                              & "   ,SE.HANDLING_AMO_TTL              AS HANDLING_AMO_TTL           " & vbNewLine _
                                              & "   ,SE.KURIKOSI_DATE1                AS KURIKOSI_DATE1             " & vbNewLine _
                                              & "   ,SE.KURIKOSI_DATE2                AS KURIKOSI_DATE2             " & vbNewLine _
                                              & "   ,SE.KURIKOSI_DATE3                AS KURIKOSI_DATE3             " & vbNewLine _
                                              & "   ,SE.MATU_DATE                     AS MATU_DATE                  " & vbNewLine _
                                              & "   --デュポン用帳票（LMG502）項目                                  " & vbNewLine _
                                              & "   ,SE.TAX_KB                        AS TAX_KB                     " & vbNewLine _
                                              & "   ,MG.CUST_COST_CD2                 AS CUST_COST_CD2              " & vbNewLine _
                                              & "   ,MG.GOODS_NM_2                    AS GOODS_NM_2                 " & vbNewLine _
                                              & "   ,MG.GOODS_NM_3                    AS GOODS_NM_3                 " & vbNewLine _
                                              & "   ,SE.INKO_NB1                      AS INKO_NB1                   " & vbNewLine _
                                              & "   ,SE.INKO_NB2                      AS INKO_NB2                   " & vbNewLine _
                                              & "   ,SE.INKO_NB3                      AS INKO_NB3                   " & vbNewLine _
                                              & "   ,SE.OUTKO_NB1                     AS OUTKO_NB1                  " & vbNewLine _
                                              & "   ,SE.OUTKO_NB2                     AS OUTKO_NB2                  " & vbNewLine _
                                              & "   ,SE.OUTKO_NB3                     AS OUTKO_NB3                  " & vbNewLine _
                                              & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                              & "   ,KBNC025_ITEM.KBN_NM1             AS STO_ITEM_CURR_CD           " & vbNewLine _
                                              & "   ,KBNC025_ITEM.KBN_NM1             AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                              & "   ,ISNULL(CURR_ITEM.ROUND_POS,0)    AS ITEM_ROUND_POS             " & vbNewLine _
                                              & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine


    ''' <summary>
    ''' データ抽出用FROM句(荷役料)
    ''' </summary>
    ''' <remarks>
    '''   2011/10/17 修正 須賀
    '''      商品コード取得時、削除フラグを抽出条件から除外
    ''' </remarks>
    Private Const SQL_FROM_NIYAKU As String = "   FROM                                                            " & vbNewLine _
                                            & "   $LM_TRN$..G_SEKY_MEISAI_PRT SE                                  " & vbNewLine _
                                            & "   INNER JOIN $LM_TRN$..G_SEKY_MEISAI_PRT SEKYP                    " & vbNewLine _
                                            & "   ON SE.NRS_BR_CD = SEKYP.NRS_BR_CD                               " & vbNewLine _
                                            & "   AND SE.JOB_NO = SEKYP.JOB_NO                                    " & vbNewLine _
                                            & "   AND SE.CTL_NO = SEKYP.CTL_NO                                    " & vbNewLine _
                                            & "   LEFT JOIN $LM_MST$..M_GOODS MG                                  " & vbNewLine _
                                            & "   ON  MG.NRS_BR_CD = SE.NRS_BR_CD                                 " & vbNewLine _
                                            & "   AND MG.GOODS_CD_NRS = SE.GOODS_CD_NRS                           " & vbNewLine _
                                            & "   LEFT JOIN $LM_MST$..M_CUST MC                                   " & vbNewLine _
                                            & "   ON  MC.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                            & "   AND MC.NRS_BR_CD = MG.NRS_BR_CD                                 " & vbNewLine _
                                            & "   AND MC.CUST_CD_L = MG.CUST_CD_L                                 " & vbNewLine _
                                            & "   AND MC.CUST_CD_M = MG.CUST_CD_M                                 " & vbNewLine _
                                            & "   AND MC.CUST_CD_S = MG.CUST_CD_S                                 " & vbNewLine _
                                            & "   AND MC.CUST_CD_SS = MG.CUST_CD_SS                               " & vbNewLine _
                                            & "   LEFT JOIN $LM_MST$..M_SEIQTO MS                                 " & vbNewLine _
                                            & "   ON  MS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                            & "   AND MS.NRS_BR_CD = SE.NRS_BR_CD                                 " & vbNewLine _
                                            & "   AND MS.SEIQTO_CD = MC.NIYAKU_SEIQTO_CD                          " & vbNewLine _
                                            & "   LEFT JOIN $LM_MST$..M_NRS_BR MB                                 " & vbNewLine _
                                            & "   ON  MB.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                            & "   AND MB.NRS_BR_CD = SE.NRS_BR_CD                                 " & vbNewLine _
                                            & "   --荷主帳票パターン取得                                          " & vbNewLine _
                                            & "   LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                             " & vbNewLine _
                                            & "   ON  SE.NRS_BR_CD = MCR1.NRS_BR_CD                               " & vbNewLine _
                                            & "   AND MG.CUST_CD_L = MCR1.CUST_CD_L                               " & vbNewLine _
                                            & "   AND MG.CUST_CD_M = MCR1.CUST_CD_M                               " & vbNewLine _
                                            & "   AND '00' = MCR1.CUST_CD_S                                       " & vbNewLine _
                                            & "   AND MCR1.PTN_ID = '52'                                          " & vbNewLine _
                                            & "   --帳票パターン取得                                              " & vbNewLine _
                                            & "   LEFT JOIN $LM_MST$..M_RPT MR1                                   " & vbNewLine _
                                            & "   ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                              " & vbNewLine _
                                            & "   AND MR1.PTN_ID = MCR1.PTN_ID                                    " & vbNewLine _
                                            & "   AND MR1.PTN_CD = MCR1.PTN_CD                                    " & vbNewLine _
                                            & "   AND MR1.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                            & "   --商品Mの荷主での荷主帳票パターン取得                           " & vbNewLine _
                                            & "   LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                             " & vbNewLine _
                                            & "   ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                               " & vbNewLine _
                                            & "   AND MG.CUST_CD_L = MCR2.CUST_CD_L                               " & vbNewLine _
                                            & "   AND MG.CUST_CD_M = MCR2.CUST_CD_M                               " & vbNewLine _
                                            & "   AND MG.CUST_CD_S = MCR2.CUST_CD_S                               " & vbNewLine _
                                            & "   AND MCR2.PTN_ID = '52'                                          " & vbNewLine _
                                            & "   --帳票パターン取得                                              " & vbNewLine _
                                            & "   LEFT JOIN $LM_MST$..M_RPT MR2                                   " & vbNewLine _
                                            & "   ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                              " & vbNewLine _
                                            & "   AND MR2.PTN_ID = MCR2.PTN_ID                                    " & vbNewLine _
                                            & "   AND MR2.PTN_CD = MCR2.PTN_CD                                    " & vbNewLine _
                                            & "   AND MR2.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                            & "   --存在しない場合の帳票パターン取得                              " & vbNewLine _
                                            & "   LEFT JOIN $LM_MST$..M_RPT MR3                                   " & vbNewLine _
                                            & "   ON  MR3.NRS_BR_CD = SE.NRS_BR_CD                                " & vbNewLine _
                                            & "   AND MR3.PTN_ID = '52'                                           " & vbNewLine _
                                            & "   AND MR3.STANDARD_FLAG = '01'                                    " & vbNewLine _
                                            & "   AND MR3.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                            & "   LEFT JOIN $LM_MST$..Z_KBN KBN_OFB                               " & vbNewLine _
                                            & "   ON  KBN_OFB.KBN_GROUP_CD = 'B002'                               " & vbNewLine _
                                            & "   AND KBN_OFB.KBN_CD = SE.OFB_KB                                  " & vbNewLine _
                                            & "   AND KBN_OFB.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                            & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                            & "   LEFT JOIN $LM_MST$..Z_KBN AS KBNC025_ITEM                       " & vbNewLine _
                                            & "   ON  KBNC025_ITEM.KBN_GROUP_CD = 'C025'                          " & vbNewLine _
                                            & "   AND KBNC025_ITEM.KBN_NM6      =  (CASE WHEN MC.ITEM_CURR_CD = '' THEN 'JPY' ELSE MC.ITEM_CURR_CD END) " & vbNewLine _
                                            & "   AND KBNC025_ITEM.SYS_DEL_FLG  = '0'                             " & vbNewLine _
                                            & "   LEFT JOIN COM_DB..M_CURR AS CURR_ITEM                           " & vbNewLine _
                                            & "   ON  CURR_ITEM.BASE_CURR_CD = MS.SEIQ_CURR_CD                    " & vbNewLine _
                                            & "   AND CURR_ITEM.CURR_CD      = MC.ITEM_CURR_CD                    " & vbNewLine _
                                            & "   AND CURR_ITEM.SYS_DEL_FLG  = '0'                                " & vbNewLine _
                                            & "   AND CURR_ITEM.UP_FLG       = '00000'                            " & vbNewLine _
                                            & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine _
                                            & "   LEFT JOIN                                                       " & vbNewLine _
                                            & "       LM_MST..Z_KBN RPT_CHG_START_YM                              " & vbNewLine _
                                            & "           ON  RPT_CHG_START_YM.KBN_GROUP_CD = 'B043'              " & vbNewLine _
                                            & "           AND RPT_CHG_START_YM.KBN_CD       = '01'                " & vbNewLine _
                                            & "           AND RPT_CHG_START_YM.SYS_DEL_FLG  = '0'                 " & vbNewLine _
                                            & "   LEFT JOIN                                                       " & vbNewLine _
                                            & "       LM_MST..Z_KBN OLD_NRS_BR_NM                                 " & vbNewLine _
                                            & "           ON  OLD_NRS_BR_NM.KBN_GROUP_CD = 'B044'                 " & vbNewLine _
                                            & "           AND OLD_NRS_BR_NM.KBN_CD       =  SE.NRS_BR_CD          " & vbNewLine _
                                            & "           AND OLD_NRS_BR_NM.SYS_DEL_FLG =  '0'                    " & vbNewLine _
                                            & "   WHERE                                                           " & vbNewLine _
                                            & "       SE.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                            & "   AND SE.SEKY_FLG = @SEKY_FLG                                     " & vbNewLine _
                                            & "   AND SE.SYS_ENT_PGID <> 'IKOU '                                  " & vbNewLine
    ''' <summary>
    ''' データ抽出用FROM句(荷役料)
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private Const SQL_FROM_NIYAKU_504 As String = "   FROM                                                            " & vbNewLine _
                                                & "   $LM_TRN$..G_SEKY_MEISAI_PRT SE                                  " & vbNewLine _
                                                & "   LEFT JOIN $LM_MST$..M_GOODS MG                                  " & vbNewLine _
                                                & "   ON  MG.NRS_BR_CD = SE.NRS_BR_CD                                 " & vbNewLine _
                                                & "   AND MG.GOODS_CD_NRS = SE.GOODS_CD_NRS                           " & vbNewLine _
                                                & "   LEFT JOIN $LM_MST$..M_CUST MC                                   " & vbNewLine _
                                                & "   ON  MC.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                                & "   AND MC.NRS_BR_CD = MG.NRS_BR_CD                                 " & vbNewLine _
                                                & "   AND MC.CUST_CD_L = MG.CUST_CD_L                                 " & vbNewLine _
                                                & "   AND MC.CUST_CD_M = MG.CUST_CD_M                                 " & vbNewLine _
                                                & "   AND MC.CUST_CD_S = MG.CUST_CD_S                                 " & vbNewLine _
                                                & "   AND MC.CUST_CD_SS = MG.CUST_CD_SS                               " & vbNewLine _
                                                & "   LEFT JOIN $LM_MST$..M_SEIQTO MS                                 " & vbNewLine _
                                                & "   ON  MS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                                & "   AND MS.NRS_BR_CD = SE.NRS_BR_CD                                 " & vbNewLine _
                                                & "   AND MS.SEIQTO_CD = MC.NIYAKU_SEIQTO_CD                          " & vbNewLine _
                                                & "   LEFT JOIN $LM_MST$..M_NRS_BR MB                                 " & vbNewLine _
                                                & "   ON  MB.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                                & "   AND MB.NRS_BR_CD = SE.NRS_BR_CD                                 " & vbNewLine _
                                                & "   --荷主帳票パターン取得                                          " & vbNewLine _
                                                & "   LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                             " & vbNewLine _
                                                & "   ON  SE.NRS_BR_CD = MCR1.NRS_BR_CD                               " & vbNewLine _
                                                & "   AND MG.CUST_CD_L = MCR1.CUST_CD_L                               " & vbNewLine _
                                                & "   AND MG.CUST_CD_M = MCR1.CUST_CD_M                               " & vbNewLine _
                                                & "   AND '00' = MCR1.CUST_CD_S                                       " & vbNewLine _
                                                & "   AND MCR1.PTN_ID = '52'                                          " & vbNewLine _
                                                & "   --帳票パターン取得                                              " & vbNewLine _
                                                & "   LEFT JOIN $LM_MST$..M_RPT MR1                                   " & vbNewLine _
                                                & "   ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                              " & vbNewLine _
                                                & "   AND MR1.PTN_ID = MCR1.PTN_ID                                    " & vbNewLine _
                                                & "   AND MR1.PTN_CD = MCR1.PTN_CD                                    " & vbNewLine _
                                                & "   AND MR1.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                & "   --商品Mの荷主での荷主帳票パターン取得                           " & vbNewLine _
                                                & "   LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                             " & vbNewLine _
                                                & "   ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                               " & vbNewLine _
                                                & "   AND MG.CUST_CD_L = MCR2.CUST_CD_L                               " & vbNewLine _
                                                & "   AND MG.CUST_CD_M = MCR2.CUST_CD_M                               " & vbNewLine _
                                                & "   AND MG.CUST_CD_S = MCR2.CUST_CD_S                               " & vbNewLine _
                                                & "   AND MCR2.PTN_ID = '52'                                          " & vbNewLine _
                                                & "   --帳票パターン取得                                              " & vbNewLine _
                                                & "   LEFT JOIN $LM_MST$..M_RPT MR2                                   " & vbNewLine _
                                                & "   ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                              " & vbNewLine _
                                                & "   AND MR2.PTN_ID = MCR2.PTN_ID                                    " & vbNewLine _
                                                & "   AND MR2.PTN_CD = MCR2.PTN_CD                                    " & vbNewLine _
                                                & "   AND MR2.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                & "   --存在しない場合の帳票パターン取得                              " & vbNewLine _
                                                & "   LEFT JOIN $LM_MST$..M_RPT MR3                                   " & vbNewLine _
                                                & "   ON  MR3.NRS_BR_CD = SE.NRS_BR_CD                                " & vbNewLine _
                                                & "   AND MR3.PTN_ID = '52'                                           " & vbNewLine _
                                                & "   AND MR3.STANDARD_FLAG = '01'                                    " & vbNewLine _
                                                & "   AND MR3.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                & "   LEFT JOIN $LM_MST$..Z_KBN KBN_OFB                               " & vbNewLine _
                                                & "   ON  KBN_OFB.KBN_GROUP_CD = 'B002'                               " & vbNewLine _
                                                & "   AND KBN_OFB.KBN_CD = SE.OFB_KB                                  " & vbNewLine _
                                                & "   AND KBN_OFB.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                                & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                                & "   LEFT JOIN $LM_MST$..Z_KBN AS KBNC025_ITEM                       " & vbNewLine _
                                                & "   ON  KBNC025_ITEM.KBN_GROUP_CD = 'C025'                          " & vbNewLine _
                                                & "   AND KBNC025_ITEM.KBN_NM6      =  (CASE WHEN MC.ITEM_CURR_CD = '' THEN 'JPY' ELSE MC.ITEM_CURR_CD END) " & vbNewLine _
                                                & "   AND KBNC025_ITEM.SYS_DEL_FLG  = '0'                             " & vbNewLine _
                                                & "   LEFT JOIN COM_DB..M_CURR AS CURR_ITEM                           " & vbNewLine _
                                                & "   ON  CURR_ITEM.BASE_CURR_CD = MS.SEIQ_CURR_CD                    " & vbNewLine _
                                                & "   AND CURR_ITEM.CURR_CD      = MC.ITEM_CURR_CD                    " & vbNewLine _
                                                & "   AND CURR_ITEM.SYS_DEL_FLG  = '0'                                " & vbNewLine _
                                                & "   AND CURR_ITEM.UP_FLG       = '00000'                            " & vbNewLine _
                                                & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine _
                                                & "   LEFT JOIN                                                       " & vbNewLine _
                                                & "       LM_MST..Z_KBN RPT_CHG_START_YM                              " & vbNewLine _
                                                & "           ON  RPT_CHG_START_YM.KBN_GROUP_CD = 'B043'              " & vbNewLine _
                                                & "           AND RPT_CHG_START_YM.KBN_CD       = '01'                " & vbNewLine _
                                                & "           AND RPT_CHG_START_YM.SYS_DEL_FLG  = '0'                 " & vbNewLine _
                                                & "   LEFT JOIN                                                       " & vbNewLine _
                                                & "       LM_MST..Z_KBN OLD_NRS_BR_NM                                 " & vbNewLine _
                                                & "           ON  OLD_NRS_BR_NM.KBN_GROUP_CD = 'B044'                 " & vbNewLine _
                                                & "           AND OLD_NRS_BR_NM.KBN_CD       =  SE.NRS_BR_CD          " & vbNewLine _
                                                & "           AND OLD_NRS_BR_NM.SYS_DEL_FLG =  '0'                    " & vbNewLine _
                                                & "   WHERE                                                           " & vbNewLine _
                                                & "       SE.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                                & "   AND SE.SEKY_FLG = '00'                                          " & vbNewLine _
                                                & "   AND SE.SYS_ENT_PGID <> 'IKOU '                                  " & vbNewLine

    ''' <summary>
    ''' GROUP BY 句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = "GROUP BY                                                           " & vbNewLine _
                                         & " RPT_ID                                                            " & vbNewLine _
                                         & ",NRS_BR_CD                                                         " & vbNewLine _
                                         & ",SEIQTO_CD                                                         " & vbNewLine _
                                         & ",SEIQTO_NM                                                         " & vbNewLine _
                                         & ",MAIN.CUST_CD_L                                                    " & vbNewLine _
                                         & ",MAIN.CUST_CD_M                                                    " & vbNewLine _
                                         & ",CUST_CD_S                                                         " & vbNewLine _
                                         & ",CUST_CD_SS                                                        " & vbNewLine _
                                         & ",CUST_NM_L                                                         " & vbNewLine _
                                         & ",CUST_NM_M                                                         " & vbNewLine _
                                         & ",CUST_NM_S                                                         " & vbNewLine _
                                         & ",CUST_NM_SS                                                        " & vbNewLine _
                                         & ",SEARCH_KEY_1                                                      " & vbNewLine _
                                         & ",JOB_NO                                                            " & vbNewLine _
                                         & ",NRS_BR_NM                                                         " & vbNewLine _
                                         & ",INV_DATE_FROM                                                     " & vbNewLine _
                                         & ",INV_DATE_TO                                                       " & vbNewLine _
                                         & ",OFB_KB                                                            " & vbNewLine _
                                         & ",OFB_KB_NM                                                         " & vbNewLine _
                                         & ",GOODS_NM_1                                                        " & vbNewLine _
                                         & ",PKG_UT                                                            " & vbNewLine _
                                         & ",GOODS_CD_CUST                                                     " & vbNewLine _
                                         & ",SERIAL_NO                                                         " & vbNewLine _
                                         & ",LOT_NO                                                            " & vbNewLine _
                                         & ",NB_UT_NM                                                          " & vbNewLine _
                                         & ",IRIME_UT_NM                                                       " & vbNewLine _
                                         & ",IRIME                                                             " & vbNewLine _
                                         & ",INKA_NO_L                                                         " & vbNewLine _
                                         & ",KISYZAN_NB1                                                       " & vbNewLine _
                                         & ",KISYZAN_NB2                                                       " & vbNewLine _
                                         & ",KISYZAN_NB3                                                       " & vbNewLine _
                                         & ",MATUZAN_NB                                                        " & vbNewLine _
                                         & ",KURIKOSI_DATE1                                                    " & vbNewLine _
                                         & ",KURIKOSI_DATE2                                                    " & vbNewLine _
                                         & ",KURIKOSI_DATE3                                                    " & vbNewLine _
                                         & ",MATU_DATE                                                         " & vbNewLine _
                                         & "--デュポン用帳票（LMG502）項目                                     " & vbNewLine _
                                         & ",TAX_KB                                                            " & vbNewLine _
                                         & ",CUST_COST_CD2                                                     " & vbNewLine _
                                         & ",GOODS_NM_2                                                        " & vbNewLine _
                                         & ",GOODS_NM_3                                                        " & vbNewLine _
                                         & ",INKO_NB1                                                          " & vbNewLine _
                                         & ",INKO_NB2                                                          " & vbNewLine _
                                         & ",INKO_NB3                                                          " & vbNewLine _
                                         & ",OUTKO_NB1                                                         " & vbNewLine _
                                         & ",OUTKO_NB2                                                         " & vbNewLine _
                                         & ",OUTKO_NB3                                                         " & vbNewLine _
                                         & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                         & ",STO_ITEM_CURR_CD                                                  " & vbNewLine _
                                         & ",HAND_ITEM_CURR_CD                                                 " & vbNewLine _
                                         & ",ITEM_ROUND_POS                                                    " & vbNewLine _
                                         & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine

    ''' <summary>
    ''' GROUP BY 句 (LMG502(デュポン用)ロット、シリアル番号を除外)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_LMG502 As String = ")MAIN2                                         " & vbNewLine _
                                                & "GROUP BY                                       " & vbNewLine _
                                                & " RPT_ID                                        " & vbNewLine _
                                                & ",NRS_BR_CD                                     " & vbNewLine _
                                                & ",SEIQTO_CD                                     " & vbNewLine _
                                                & ",SEIQTO_NM                                     " & vbNewLine _
                                                & ",CUST_CD_L                                     " & vbNewLine _
                                                & ",CUST_CD_M                                     " & vbNewLine _
                                                & ",CUST_CD_S                                     " & vbNewLine _
                                                & ",CUST_CD_SS                                    " & vbNewLine _
                                                & ",CUST_NM_L                                     " & vbNewLine _
                                                & ",CUST_NM_M                                     " & vbNewLine _
                                                & ",CUST_NM_S                                     " & vbNewLine _
                                                & ",CUST_NM_SS                                    " & vbNewLine _
                                                & ",SEARCH_KEY_1                                  " & vbNewLine _
                                                & ",JOB_NO                                        " & vbNewLine _
                                                & ",NRS_BR_NM                                     " & vbNewLine _
                                                & ",INV_DATE_FROM                                 " & vbNewLine _
                                                & ",INV_DATE_TO                                   " & vbNewLine _
                                                & ",OFB_KB                                        " & vbNewLine _
                                                & ",OFB_KB_NM                                     " & vbNewLine _
                                                & ",GOODS_NM_1                                    " & vbNewLine _
                                                & ",PKG_UT                                        " & vbNewLine _
                                                & ",GOODS_CD_CUST                                 " & vbNewLine _
                                                & ",NB_UT_NM                                      " & vbNewLine _
                                                & ",IRIME_UT_NM                                   " & vbNewLine _
                                                & ",IRIME                                         " & vbNewLine _
                                                & ",INKA_NO_L                                     " & vbNewLine _
                                                & ",STORAGE1                                      " & vbNewLine _
                                                & ",STORAGE2                                      " & vbNewLine _
                                                & ",STORAGE3                                      " & vbNewLine _
                                                & ",HANDLING_IN1                                  " & vbNewLine _
                                                & ",KURIKOSI_DATE1                                " & vbNewLine _
                                                & ",KURIKOSI_DATE2                                " & vbNewLine _
                                                & ",KURIKOSI_DATE3                                " & vbNewLine _
                                                & ",MATU_DATE                                     " & vbNewLine _
                                                & ",TAX_KB                                        " & vbNewLine _
                                                & ",CUST_COST_CD2                                 " & vbNewLine _
                                                & ",GOODS_NM_2                                    " & vbNewLine _
                                                & ",GOODS_NM_3                                    " & vbNewLine _
                                                & "   --(2014.08.21) 追加START 多通貨対応         " & vbNewLine _
                                                & ",STO_ITEM_CURR_CD                              " & vbNewLine _
                                                & ",HAND_ITEM_CURR_CD                             " & vbNewLine _
                                                & ",ITEM_ROUND_POS                                " & vbNewLine _
                                                & "   --(2014.08.21) 追加END   多通貨対応         " & vbNewLine


    ''' <summary>
    ''' GROUP BY 句 保管用504
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_HOKAN_504 As String = "GROUP BY                                         " & vbNewLine _
                                                   & " MR2.PTN_CD                                      " & vbNewLine _
                                                   & ",MR2.RPT_ID                                      " & vbNewLine _
                                                   & ",MR1.PTN_CD                                      " & vbNewLine _
                                                   & ",MR1.RPT_ID                                      " & vbNewLine _
                                                   & ",MR3.RPT_ID                                      " & vbNewLine _
                                                   & ",SE.NRS_BR_CD                                    " & vbNewLine _
                                                   & ",MS.SEIQTO_CD                                    " & vbNewLine _
                                                   & ",MS.SEIQTO_NM                                    " & vbNewLine _
                                                   & ",MG.CUST_CD_L                                    " & vbNewLine _
                                                   & ",MG.CUST_CD_M                                    " & vbNewLine _
                                                   & ",MG.CUST_CD_S                                    " & vbNewLine _
                                                   & ",MG.CUST_CD_SS                                   " & vbNewLine _
                                                   & ",MC.CUST_NM_L                                    " & vbNewLine _
                                                   & ",MC.CUST_NM_M                                    " & vbNewLine _
                                                   & ",MC.CUST_NM_S                                    " & vbNewLine _
                                                   & ",MC.CUST_NM_SS                                   " & vbNewLine _
                                                   & ",MG.SEARCH_KEY_1                                 " & vbNewLine _
                                                   & ",MG.SEARCH_KEY_2                                 " & vbNewLine _
                                                   & ",SE.JOB_NO                                       " & vbNewLine _
                                                   & ",CASE WHEN SUBSTRING(SE.INV_DATE_TO, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                                   & "      THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, MB.NRS_BR_NM)          " & vbNewLine _
                                                   & "      ELSE MB.NRS_BR_NM                                         " & vbNewLine _
                                                   & "      END                                                       " & vbNewLine _
                                                   & ",SE.INV_DATE_FROM                                " & vbNewLine _
                                                   & ",SE.INV_DATE_TO                                  " & vbNewLine _
                                                   & ",SE.OFB_KB                                       " & vbNewLine _
                                                   & ",KBN_OFB.KBN_NM1                                 " & vbNewLine _
                                                   & ",MG.GOODS_NM_1                                   " & vbNewLine _
                                                   & ",PKG_UT                                          " & vbNewLine _
                                                   & ",MG.GOODS_CD_CUST                                " & vbNewLine _
                                                   & ",SE.SERIAL_NO                                    " & vbNewLine _
                                                   & ",SE.LOT_NO                                       " & vbNewLine _
                                                   & ",MG.NB_UT                                        " & vbNewLine _
                                                   & ",MG.STD_IRIME_UT                                 " & vbNewLine _
                                                   & ",SE.IRIME                                        " & vbNewLine _
                                                   & ",SE.STORAGE1                                     " & vbNewLine _
                                                   & ",SE.STORAGE2                                     " & vbNewLine _
                                                   & ",SE.STORAGE3                                     " & vbNewLine _
                                                   & ",SE.KURIKOSI_DATE1                               " & vbNewLine _
                                                   & ",SE.KURIKOSI_DATE2                               " & vbNewLine _
                                                   & ",SE.KURIKOSI_DATE3                               " & vbNewLine _
                                                   & ",SE.MATU_DATE                                    " & vbNewLine _
                                                   & "   --(2014.08.21) 追加START 多通貨対応           " & vbNewLine _
                                                   & "   ,KBNC025_ITEM.KBN_NM1                         " & vbNewLine _
                                                   & "   ,KBNC025_ITEM.KBN_NM1                         " & vbNewLine _
                                                   & "   ,ISNULL(CURR_ITEM.ROUND_POS,0)                " & vbNewLine _
                                                   & "   --(2014.08.21) 追加END   多通貨対応           " & vbNewLine

    ''' <summary>
    ''' GROUP BY 句 荷役504
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_NIYAKU_504 As String = "GROUP BY                                         " & vbNewLine _
                                                    & " MR2.PTN_CD                                      " & vbNewLine _
                                                    & ",MR2.RPT_ID                                      " & vbNewLine _
                                                    & ",MR1.PTN_CD                                      " & vbNewLine _
                                                    & ",MR1.RPT_ID                                      " & vbNewLine _
                                                    & ",MR3.RPT_ID                                      " & vbNewLine _
                                                    & ",SE.NRS_BR_CD                                    " & vbNewLine _
                                                    & ",MS.SEIQTO_CD                                    " & vbNewLine _
                                                    & ",MS.SEIQTO_NM                                    " & vbNewLine _
                                                    & ",MG.CUST_CD_L                                    " & vbNewLine _
                                                    & ",MG.CUST_CD_M                                    " & vbNewLine _
                                                    & ",MG.CUST_CD_S                                    " & vbNewLine _
                                                    & ",MG.CUST_CD_SS                                   " & vbNewLine _
                                                    & ",MC.CUST_NM_L                                    " & vbNewLine _
                                                    & ",MC.CUST_NM_M                                    " & vbNewLine _
                                                    & ",MC.CUST_NM_S                                    " & vbNewLine _
                                                    & ",MC.CUST_NM_SS                                   " & vbNewLine _
                                                    & ",MG.SEARCH_KEY_1                                 " & vbNewLine _
                                                    & ",MG.SEARCH_KEY_2                                 " & vbNewLine _
                                                    & ",SE.JOB_NO                                       " & vbNewLine _
                                                    & ",CASE WHEN SUBSTRING(SE.INV_DATE_TO, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                                    & "      THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, MB.NRS_BR_NM)          " & vbNewLine _
                                                    & "      ELSE MB.NRS_BR_NM                                         " & vbNewLine _
                                                    & "      END                                                       " & vbNewLine _
                                                    & ",SE.INV_DATE_FROM                                " & vbNewLine _
                                                    & ",SE.INV_DATE_TO                                  " & vbNewLine _
                                                    & ",SE.OFB_KB                                       " & vbNewLine _
                                                    & ",KBN_OFB.KBN_NM1                                 " & vbNewLine _
                                                    & ",MG.GOODS_NM_1                                   " & vbNewLine _
                                                    & ",PKG_UT                                          " & vbNewLine _
                                                    & ",MG.GOODS_CD_CUST                                " & vbNewLine _
                                                    & ",SE.SERIAL_NO                                    " & vbNewLine _
                                                    & ",SE.LOT_NO                                       " & vbNewLine _
                                                    & ",MG.NB_UT                                        " & vbNewLine _
                                                    & ",MG.STD_IRIME_UT                                 " & vbNewLine _
                                                    & ",SE.IRIME                                        " & vbNewLine _
                                                    & ",SE.HANDLING_IN1                                 " & vbNewLine _
                                                    & ",SE.HANDLING_IN2                                 " & vbNewLine _
                                                    & ",SE.HANDLING_IN3                                 " & vbNewLine _
                                                    & ",SE.KURIKOSI_DATE1                               " & vbNewLine _
                                                    & ",SE.KURIKOSI_DATE2                               " & vbNewLine _
                                                    & ",SE.KURIKOSI_DATE3                               " & vbNewLine _
                                                    & ",SE.MATU_DATE                                    " & vbNewLine _
                                                    & "   --(2014.08.21) 追加START 多通貨対応           " & vbNewLine _
                                                    & "   ,KBNC025_ITEM.KBN_NM1                         " & vbNewLine _
                                                    & "   ,KBNC025_ITEM.KBN_NM1                         " & vbNewLine _
                                                    & "   ,ISNULL(CURR_ITEM.ROUND_POS,0)                " & vbNewLine _
                                                    & "   --(2014.08.21) 追加END   多通貨対応           " & vbNewLine


    ''' <summary>
    ''' GROUP BY 句 MAIN504
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_MAIN_504 As String = "GROUP BY                                      " & vbNewLine _
                                                  & " RPT_ID                                       " & vbNewLine _
                                                  & ",NRS_BR_CD                                    " & vbNewLine _
                                                  & ",SEIQTO_CD                                    " & vbNewLine _
                                                  & ",SEIQTO_NM                                    " & vbNewLine _
                                                  & ",CUST_CD_L                                    " & vbNewLine _
                                                  & ",CUST_CD_M                                    " & vbNewLine _
                                                  & ",CUST_CD_S                                    " & vbNewLine _
                                                  & ",CUST_CD_SS                                   " & vbNewLine _
                                                  & ",CUST_NM_L                                    " & vbNewLine _
                                                  & ",CUST_NM_M                                    " & vbNewLine _
                                                  & ",CUST_NM_S                                    " & vbNewLine _
                                                  & ",CUST_NM_SS                                   " & vbNewLine _
                                                  & ",SEARCH_KEY_1                                 " & vbNewLine _
                                                  & ",SEARCH_KEY_2                                 " & vbNewLine _
                                                  & ",JOB_NO                                       " & vbNewLine _
                                                  & ",NRS_BR_NM                                    " & vbNewLine _
                                                  & ",INV_DATE_FROM                                " & vbNewLine _
                                                  & ",INV_DATE_TO                                  " & vbNewLine _
                                                  & ",OFB_KB                                       " & vbNewLine _
                                                  & ",OFB_KB_NM                                    " & vbNewLine _
                                                  & ",GOODS_NM_1                                   " & vbNewLine _
                                                  & ",PKG_UT                                       " & vbNewLine _
                                                  & ",GOODS_CD_CUST                                " & vbNewLine _
                                                  & ",SERIAL_NO                                    " & vbNewLine _
                                                  & ",LOT_NO                                       " & vbNewLine _
                                                  & ",NB_UT_NM                                     " & vbNewLine _
                                                  & ",IRIME_UT_NM                                  " & vbNewLine _
                                                  & ",IRIME                                        " & vbNewLine _
                                                  & ",KISYZAN_NB1                                  " & vbNewLine _
                                                  & ",KISYZAN_NB2                                  " & vbNewLine _
                                                  & ",KISYZAN_NB3                                  " & vbNewLine _
                                                  & ",MATUZAN_NB                                   " & vbNewLine _
                                                  & ",KURIKOSI_DATE1                               " & vbNewLine _
                                                  & ",KURIKOSI_DATE2                               " & vbNewLine _
                                                  & ",KURIKOSI_DATE3                               " & vbNewLine _
                                                  & ",MATU_DATE                                    " & vbNewLine _
                                                  & "   --(2014.08.21) 追加START 多通貨対応        " & vbNewLine _
                                                  & ",STO_ITEM_CURR_CD                             " & vbNewLine _
                                                  & ",HAND_ITEM_CURR_CD                            " & vbNewLine _
                                                  & ",ITEM_ROUND_POS                               " & vbNewLine _
                                                  & "   --(2014.08.21) 追加END   多通貨対応        " & vbNewLine



    ''' <summary>
    ''' ORDER BY 句（①請求先コード、②荷主コード(大、中、小、極小)、③荷主カテゴリ1、④簿外品区分、⑤商品名、⑥商品コード、⑦ロットNo.、⑧入荷管理番号L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                           " & vbNewLine _
                                         & " SEIQTO_CD                                                         " & vbNewLine _
                                         & ",CUST_CD_L                                                         " & vbNewLine _
                                         & ",CUST_CD_M                                                         " & vbNewLine _
                                         & ",CUST_CD_S                                                         " & vbNewLine _
                                         & ",CUST_CD_SS                                                        " & vbNewLine _
                                         & ",SEARCH_KEY_1                                                      " & vbNewLine _
                                         & ",OFB_KB                                                            " & vbNewLine _
                                         & ",GOODS_NM_1                                                        " & vbNewLine _
                                         & ",GOODS_CD_CUST                                                     " & vbNewLine _
                                         & ",LOT_NO                                                            " & vbNewLine _
                                         & ",INKA_NO_L                                                         " & vbNewLine

    ''' <summary>
    ''' ORDER BY 句（①荷主コード(大、中、小、極小)、②課税区分、③簿外品区分、④荷主カテゴリ1、⑤荷主勘定科目コード、⑥商品名)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMG502 As String = "ORDER BY                                       " & vbNewLine _
                                                & " CUST_CD_L                                     " & vbNewLine _
                                                & ",CUST_CD_M                                     " & vbNewLine _
                                                & ",CUST_CD_S                                     " & vbNewLine _
                                                & ",CUST_CD_SS                                    " & vbNewLine _
                                                & ",TAX_KB                                        " & vbNewLine _
                                                & ",OFB_KB                                        " & vbNewLine _
                                                & ",SEARCH_KEY_1                                  " & vbNewLine _
                                                & ",CUST_COST_CD2                                 " & vbNewLine _
                                                & ",GOODS_NM_1                                    " & vbNewLine
    ''' <summary>
    ''' ORDER BY 句 LMG503（①請求先コード、②荷主コード(大、中)、③荷姿、④荷主カテゴリ1、⑤商品名、⑥商品コード、⑦ロットNo.、⑧入荷管理番号L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMG503 As String = "ORDER BY                                       " & vbNewLine _
                                                & " SEIQTO_CD                                     " & vbNewLine _
                                                & ",CUST_CD_L                                     " & vbNewLine _
                                                & ",CUST_CD_M                                     " & vbNewLine _
                                                & ",PKG_UT                                        " & vbNewLine _
                                                & ",SEARCH_KEY_1                                  " & vbNewLine _
                                                & ",GOODS_NM_1                                    " & vbNewLine _
                                                & ",GOODS_CD_CUST                                 " & vbNewLine _
                                                & ",LOT_NO                                        " & vbNewLine _
                                                & ",INKA_NO_L                                     " & vbNewLine

    ''' <summary>
    ''' ORDER BY 句504
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_504 As String = "ORDER BY                                                           " & vbNewLine _
                                             & " SEIQTO_CD                                                         " & vbNewLine _
                                             & ",CUST_CD_L                                                         " & vbNewLine _
                                             & ",CUST_CD_M                                                         " & vbNewLine _
                                             & ",CUST_CD_S                                                         " & vbNewLine _
                                             & ",CUST_CD_SS                                                        " & vbNewLine _
                                             & ",SEARCH_KEY_1                                                      " & vbNewLine _
                                             & ",OFB_KB                                                            " & vbNewLine _
                                             & ",GOODS_NM_1                                                        " & vbNewLine _
                                             & ",GOODS_CD_CUST                                                     " & vbNewLine _
                                             & ",LOT_NO                                                            " & vbNewLine

    ''' <summary>
    ''' ORDER BY 句505
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_505 As String = "ORDER BY                                                           " & vbNewLine _
                                             & " SEIQTO_CD                                                         " & vbNewLine _
                                             & ",CUST_CD_L                                                         " & vbNewLine _
                                             & ",CUST_CD_M                                                         " & vbNewLine _
                                             & ",CUST_CD_S                                                         " & vbNewLine _
                                             & ",CUST_CD_SS                                                        " & vbNewLine _
                                             & ",SEARCH_KEY_1                                                      " & vbNewLine _
                                             & ",SEARCH_KEY_2                                                      " & vbNewLine _
                                             & ",OFB_KB                                                            " & vbNewLine _
                                             & ",GOODS_NM_1                                                        " & vbNewLine _
                                             & ",GOODS_CD_CUST                                                     " & vbNewLine _
                                             & ",LOT_NO                                                            " & vbNewLine

    ''' <summary>
    ''' ORDER BY 句507（①請求先コード、②荷主コード(大、中、小、極小)、③荷主カテゴリ1、④簿外品区分、⑤荷姿、⑥商品名、⑦商品コード、⑧ロットNo.、⑨入荷管理番号L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_507 As String = "ORDER BY                                                           " & vbNewLine _
                                         & " SEIQTO_CD                                                         " & vbNewLine _
                                         & ",CUST_CD_L                                                         " & vbNewLine _
                                         & ",CUST_CD_M                                                         " & vbNewLine _
                                         & ",CUST_CD_S                                                         " & vbNewLine _
                                         & ",CUST_CD_SS                                                        " & vbNewLine _
                                         & ",SEARCH_KEY_1                                                      " & vbNewLine _
                                         & ",OFB_KB                                                            " & vbNewLine _
                                         & ",NB_UT_NM                                                          " & vbNewLine _
                                         & ",GOODS_NM_1                                                        " & vbNewLine _
                                         & ",GOODS_CD_CUST                                                     " & vbNewLine _
                                         & ",LOT_NO                                                            " & vbNewLine _
                                         & ",INKA_NO_L                                                         " & vbNewLine

#Region "LMG585"

    ''' <summary>
    ''' データ抽出用SELECT句(MAIN)
    ''' </summary>
    ''' <remarks>変動保管料対応版</remarks>
    Private Const SQL_SELECT_MAIN_585 As String = "SELECT                                                             " & vbNewLine _
                                            & " RPT_ID                              AS RPT_ID                     " & vbNewLine _
                                            & ",NRS_BR_CD                           AS NRS_BR_CD                  " & vbNewLine _
                                            & ",SEIQTO_CD                           AS SEIQTO_CD                  " & vbNewLine _
                                            & ",SEIQTO_NM                           AS SEIQTO_NM                  " & vbNewLine _
                                            & ",MAIN.CUST_CD_L                      AS CUST_CD_L                  " & vbNewLine _
                                            & ",MAIN.CUST_CD_M                      AS CUST_CD_M                  " & vbNewLine _
                                            & ",CUST_CD_S                           AS CUST_CD_S                  " & vbNewLine _
                                            & ",CUST_CD_SS                          AS CUST_CD_SS                 " & vbNewLine _
                                            & ",CUST_NM_L                           AS CUST_NM_L                  " & vbNewLine _
                                            & ",CUST_NM_M                           AS CUST_NM_M                  " & vbNewLine _
                                            & ",CUST_NM_S                           AS CUST_NM_S                  " & vbNewLine _
                                            & ",CUST_NM_SS                          AS CUST_NM_SS                 " & vbNewLine _
                                            & ",SEARCH_KEY_1                        AS SEARCH_KEY_1               " & vbNewLine _
                                            & ",''                                  AS SEARCH_KEY_2               " & vbNewLine _
                                            & ",JOB_NO                              AS JOB_NO                     " & vbNewLine _
                                            & ",NRS_BR_NM                           AS NRS_BR_NM                  " & vbNewLine _
                                            & ",INV_DATE_FROM                       AS INV_DATE_FROM              " & vbNewLine _
                                            & ",INV_DATE_TO                         AS INV_DATE_TO                " & vbNewLine _
                                            & ",OFB_KB                              AS OFB_KB                     " & vbNewLine _
                                            & ",ISNULL(OFB_KB_NM,'')                AS OFB_KB_NM                  " & vbNewLine _
                                            & ",GOODS_NM_1                          AS GOODS_NM_1                 " & vbNewLine _
                                            & ",''                                  AS PKG_UT                     " & vbNewLine _
                                            & ",GOODS_CD_CUST                       AS GOODS_CD_CUST              " & vbNewLine _
                                            & ",SERIAL_NO                           AS SERIAL_NO                  " & vbNewLine _
                                            & ",LOT_NO                              AS LOT_NO                     " & vbNewLine _
                                            & ",NB_UT_NM                            AS NB_UT_NM                   " & vbNewLine _
                                            & ",IRIME_UT_NM                         AS IRIME_UT_NM                " & vbNewLine _
                                            & ",IRIME                               AS IRIME                      " & vbNewLine _
                                            & ",INKA_NO_L                           AS INKA_NO_L                  " & vbNewLine _
                                            & ",KISYZAN_NB1                         AS KISYZAN_NB1                " & vbNewLine _
                                            & ",KISYZAN_NB2                         AS KISYZAN_NB2                " & vbNewLine _
                                            & ",KISYZAN_NB3                         AS KISYZAN_NB3                " & vbNewLine _
                                            & ",MATUZAN_NB                          AS MATUZAN_NB                 " & vbNewLine _
                                            & ",SUM(INKO_NB_TTL2)                   AS INKO_NB_TTL2               " & vbNewLine _
                                            & ",SUM(INKO_NB_TTL1)                   AS INKO_NB_TTL1               " & vbNewLine _
                                            & ",SUM(OUTKO_NB_TTL2)                  AS OUTKO_NB_TTL2              " & vbNewLine _
                                            & ",SUM(OUTKO_NB_TTL1)                  AS OUTKO_NB_TTL1              " & vbNewLine _
                                            & ",SUM(SEKI_ARI_NB1)                   AS SEKI_ARI_NB1               " & vbNewLine _
                                            & ",SUM(SEKI_ARI_NB2)                   AS SEKI_ARI_NB2               " & vbNewLine _
                                            & ",SUM(SEKI_ARI_NB3)                   AS SEKI_ARI_NB3               " & vbNewLine _
                                            & ",SUM(STORAGE1)                       AS STORAGE1                   " & vbNewLine _
                                            & ",SUM(STORAGE2)                       AS STORAGE2                   " & vbNewLine _
                                            & ",SUM(STORAGE3)                       AS STORAGE3                   " & vbNewLine _
                                            & ",SUM(STORAGE_AMO_TTL)                AS STORAGE_AMO_TTL            " & vbNewLine _
                                            & ",SUM(HANDLING_NB_TTL)                AS HANDLING_NB_TTL            " & vbNewLine _
                                            & ",SUM(HANDLING_IN1)                   AS HANDLING_IN1               " & vbNewLine _
                                            & ",''                                  AS HANDLING_IN2               " & vbNewLine _
                                            & ",''                                  AS HANDLING_IN3               " & vbNewLine _
                                            & ",SUM(HANDLING_AMO_TTL)               AS HANDLING_AMO_TTL           " & vbNewLine _
                                            & ",KURIKOSI_DATE1                      AS KURIKOSI_DATE1             " & vbNewLine _
                                            & ",KURIKOSI_DATE2                      AS KURIKOSI_DATE2             " & vbNewLine _
                                            & ",KURIKOSI_DATE3                      AS KURIKOSI_DATE3             " & vbNewLine _
                                            & ",MATU_DATE                           AS MATU_DATE                  " & vbNewLine _
                                            & "--デュポン用帳票（LMG502）項目                                     " & vbNewLine _
                                            & ",TAX_KB                              AS TAX_KB                     " & vbNewLine _
                                            & ",CUST_COST_CD2                       AS CUST_COST_CD2              " & vbNewLine _
                                            & ",GOODS_NM_2                          AS GOODS_NM_2                 " & vbNewLine _
                                            & ",GOODS_NM_3                          AS GOODS_NM_3                 " & vbNewLine _
                                            & ",INKO_NB1                            AS INKO_NB1                   " & vbNewLine _
                                            & ",INKO_NB2                            AS INKO_NB2                   " & vbNewLine _
                                            & ",INKO_NB3                            AS INKO_NB3                   " & vbNewLine _
                                            & ",OUTKO_NB1                           AS OUTKO_NB1                  " & vbNewLine _
                                            & ",OUTKO_NB2                           AS OUTKO_NB2                  " & vbNewLine _
                                            & ",OUTKO_NB3                           AS OUTKO_NB3                  " & vbNewLine _
                                            & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                            & ",STO_ITEM_CURR_CD                    AS STO_ITEM_CURR_CD           " & vbNewLine _
                                            & ",HAND_ITEM_CURR_CD                   AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                            & ",ITEM_ROUND_POS                      AS ITEM_ROUND_POS             " & vbNewLine _
                                            & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine _
                                            & ",STRAGE_HENDO_NASHI_AMO_TTL          AS STRAGE_HENDO_NASHI_AMO_TTL " & vbNewLine _
                                            & ",CASE WHEN INKA_DATE = ''                                          " & vbNewLine _
                                            & "     THEN ''                                                       " & vbNewLine _
                                            & "     ELSE FORMAT(CONVERT(DATE, INKA_DATE), 'yyyy/MM/dd')           " & vbNewLine _
                                            & "     END                             AS INKA_DATE                  " & vbNewLine _
                                            & ",CASE WHEN INKA_DATE = ''                                          " & vbNewLine _
                                            & "     THEN ''                                                       " & vbNewLine _
                                            & "     ELSE CONVERT(CHAR, VAR_RATE)                                  " & vbNewLine _
                                            & "     END                             AS VAR_RATE                   " & vbNewLine _
                                            & "FROM                                                               " & vbNewLine _
                                            & "(                                                                  " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT句(保管料)
    ''' </summary>
    ''' <remarks>変動保管料対応版</remarks>
    Private Const SQL_SELECT_HOKAN_585 As String = "   --保管料                                                        " & vbNewLine _
                                             & "   SELECT                                                  " & vbNewLine _
                                             & "   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                             & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                " & vbNewLine _
                                             & "        ELSE MR3.RPT_ID END          AS RPT_ID                     " & vbNewLine _
                                             & "   ,SE.NRS_BR_CD                     AS NRS_BR_CD                  " & vbNewLine _
                                             & "   ,MS.SEIQTO_CD                     AS SEIQTO_CD                  " & vbNewLine _
                                             & "   ,MS.SEIQTO_NM                     AS SEIQTO_NM                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_L                     AS CUST_CD_L                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_M                     AS CUST_CD_M                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_S                     AS CUST_CD_S                  " & vbNewLine _
                                             & "   ,MG.CUST_CD_SS                    AS CUST_CD_SS                 " & vbNewLine _
                                             & "   ,MC.CUST_NM_L                     AS CUST_NM_L                  " & vbNewLine _
                                             & "   ,MC.CUST_NM_M                     AS CUST_NM_M                  " & vbNewLine _
                                             & "   ,MC.CUST_NM_S                     AS CUST_NM_S                  " & vbNewLine _
                                             & "   ,MC.CUST_NM_SS                    AS CUST_NM_SS                 " & vbNewLine _
                                             & "   ,MG.SEARCH_KEY_1                  AS SEARCH_KEY_1               " & vbNewLine _
                                             & "   ,SE.JOB_NO                        AS JOB_NO                     " & vbNewLine _
                                             & "   ,CASE WHEN SUBSTRING(SE.INV_DATE_TO, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                             & "         THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, MB.NRS_BR_NM)          " & vbNewLine _
                                             & "         ELSE MB.NRS_BR_NM                                         " & vbNewLine _
                                             & "         END  AS NRS_BR_NM                                         " & vbNewLine _
                                             & "   ,SE.INV_DATE_FROM                 AS INV_DATE_FROM              " & vbNewLine _
                                             & "   ,SE.INV_DATE_TO                   AS INV_DATE_TO                " & vbNewLine _
                                             & "   ,SE.OFB_KB                        AS OFB_KB                     " & vbNewLine _
                                             & "   ,KBN_OFB.KBN_NM1                  AS OFB_KB_NM                  " & vbNewLine _
                                             & "   ,MG.GOODS_NM_1                    AS GOODS_NM_1                 " & vbNewLine _
                                             & "   ,''                               AS PKG_UT                     " & vbNewLine _
                                             & "   ,MG.GOODS_CD_CUST                 AS GOODS_CD_CUST              " & vbNewLine _
                                             & "   ,SE.SERIAL_NO                     AS SERIAL_NO                  " & vbNewLine _
                                             & "   ,SE.LOT_NO                        AS LOT_NO                     " & vbNewLine _
                                             & "   ,MG.NB_UT                         AS NB_UT_NM                   " & vbNewLine _
                                             & "   ,MG.STD_IRIME_UT                  AS IRIME_UT_NM                " & vbNewLine _
                                             & "   ,SE.IRIME                         AS IRIME                      " & vbNewLine _
                                             & "   ,SE.INKA_NO_L                     AS INKA_NO_L                  " & vbNewLine _
                                             & "   ,SE.KISYZAN_NB1                   AS KISYZAN_NB1                " & vbNewLine _
                                             & "   ,SE.KISYZAN_NB2                   AS KISYZAN_NB2                " & vbNewLine _
                                             & "   ,SE.KISYZAN_NB3                   AS KISYZAN_NB3                " & vbNewLine _
                                             & "   ,SE.MATUZAN_NB                    AS MATUZAN_NB                 " & vbNewLine _
                                             & "   ,0                                AS INKO_NB_TTL2               " & vbNewLine _
                                             & "   ,0                                AS INKO_NB_TTL1               " & vbNewLine _
                                             & "   ,0                                AS OUTKO_NB_TTL2              " & vbNewLine _
                                             & "   ,0                                AS OUTKO_NB_TTL1              " & vbNewLine _
                                             & "   ,SE.SEKI_ARI_NB1                  AS SEKI_ARI_NB1               " & vbNewLine _
                                             & "   ,SE.SEKI_ARI_NB2                  AS SEKI_ARI_NB2               " & vbNewLine _
                                             & "   ,SE.SEKI_ARI_NB3                  AS SEKI_ARI_NB3               " & vbNewLine _
                                             & "   ,SE.STORAGE1                      AS STORAGE1                   " & vbNewLine _
                                             & "   ,SE.STORAGE2                      AS STORAGE2                   " & vbNewLine _
                                             & "   ,SE.STORAGE3                      AS STORAGE3                   " & vbNewLine _
                                             & "   ,SE.STORAGE_AMO_TTL               AS STORAGE_AMO_TTL            " & vbNewLine _
                                             & "   ,0                                AS HANDLING_NB_TTL            " & vbNewLine _
                                             & "   ,0                                AS HANDLING_IN1               " & vbNewLine _
                                             & "   ,0                                AS HANDLING_AMO_TTL           " & vbNewLine _
                                             & "   ,SE.KURIKOSI_DATE1                AS KURIKOSI_DATE1             " & vbNewLine _
                                             & "   ,SE.KURIKOSI_DATE2                AS KURIKOSI_DATE2             " & vbNewLine _
                                             & "   ,SE.KURIKOSI_DATE3                AS KURIKOSI_DATE3             " & vbNewLine _
                                             & "   ,SE.MATU_DATE                     AS MATU_DATE                  " & vbNewLine _
                                             & "   --デュポン用帳票（LMG502）項目                                  " & vbNewLine _
                                             & "   ,SE.TAX_KB                        AS TAX_KB                     " & vbNewLine _
                                             & "   ,MG.CUST_COST_CD2                 AS CUST_COST_CD2              " & vbNewLine _
                                             & "   ,MG.GOODS_NM_2                    AS GOODS_NM_2                 " & vbNewLine _
                                             & "   ,MG.GOODS_NM_3                    AS GOODS_NM_3                 " & vbNewLine _
                                             & "   ,SE.INKO_NB1                      AS INKO_NB1                   " & vbNewLine _
                                             & "   ,SE.INKO_NB2                      AS INKO_NB2                   " & vbNewLine _
                                             & "   ,SE.INKO_NB3                      AS INKO_NB3                   " & vbNewLine _
                                             & "   ,SE.OUTKO_NB1                     AS OUTKO_NB1                  " & vbNewLine _
                                             & "   ,SE.OUTKO_NB2                     AS OUTKO_NB2                  " & vbNewLine _
                                             & "   ,SE.OUTKO_NB3                     AS OUTKO_NB3                  " & vbNewLine _
                                             & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                             & "   ,KBNC025_ITEM.KBN_NM1             AS STO_ITEM_CURR_CD           " & vbNewLine _
                                             & "   ,KBNC025_ITEM.KBN_NM1             AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                             & "   ,ISNULL(CURR_ITEM.ROUND_POS,0)    AS ITEM_ROUND_POS             " & vbNewLine _
                                             & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine _
                                             & "   ,SE.STRAGE_HENDO_NASHI_AMO_TTL    AS STRAGE_HENDO_NASHI_AMO_TTL " & vbNewLine _
                                             & "   ,SE.INKA_DATE                     AS INKA_DATE                  " & vbNewLine _
                                             & "   ,SE.VAR_RATE                      AS VAR_RATE                   " & vbNewLine

    ''' <summary>
    ''' データ抽出用SELECT句(荷役料)
    ''' </summary>
    ''' <remarks>変動保管料対応版</remarks>
    Private Const SQL_SELECT_NIYAKU_585 As String = "   --荷役料                                                        " & vbNewLine _
                                              & "   UNION ALL                                                       " & vbNewLine _
                                              & "   SELECT                                                  " & vbNewLine _
                                              & "   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                              & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                " & vbNewLine _
                                              & "        ELSE MR3.RPT_ID END          AS RPT_ID                     " & vbNewLine _
                                              & "   ,SE.NRS_BR_CD                     AS NRS_BR_CD                  " & vbNewLine _
                                              & "   ,MS.SEIQTO_CD                     AS SEIQTO_CD                  " & vbNewLine _
                                              & "   ,MS.SEIQTO_NM                     AS SEIQTO_NM                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_L                     AS CUST_CD_L                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_M                     AS CUST_CD_M                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_S                     AS CUST_CD_S                  " & vbNewLine _
                                              & "   ,MG.CUST_CD_SS                    AS CUST_CD_SS                 " & vbNewLine _
                                              & "   ,MC.CUST_NM_L                     AS CUST_NM_L                  " & vbNewLine _
                                              & "   ,MC.CUST_NM_M                     AS CUST_NM_M                  " & vbNewLine _
                                              & "   ,MC.CUST_NM_S                     AS CUST_NM_S                  " & vbNewLine _
                                              & "   ,MC.CUST_NM_SS                    AS CUST_NM_SS                 " & vbNewLine _
                                              & "   ,MG.SEARCH_KEY_1                  AS SEARCH_KEY_1               " & vbNewLine _
                                              & "   ,SE.JOB_NO                        AS JOB_NO                     " & vbNewLine _
                                              & "   ,CASE WHEN SUBSTRING(SE.INV_DATE_TO, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                              & "         THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, MB.NRS_BR_NM)          " & vbNewLine _
                                              & "         ELSE MB.NRS_BR_NM                                         " & vbNewLine _
                                              & "         END  AS NRS_BR_NM                                         " & vbNewLine _
                                              & "   ,SE.INV_DATE_FROM                 AS INV_DATE_FROM              " & vbNewLine _
                                              & "   ,SE.INV_DATE_TO                   AS INV_DATE_TO                " & vbNewLine _
                                              & "   ,SE.OFB_KB                        AS OFB_KB                     " & vbNewLine _
                                              & "   ,KBN_OFB.KBN_NM1                  AS OFB_KB_NM                  " & vbNewLine _
                                              & "   ,MG.GOODS_NM_1                    AS GOODS_NM_1                 " & vbNewLine _
                                              & "    ,''                              AS PKG_UT                     " & vbNewLine _
                                              & "   ,MG.GOODS_CD_CUST                 AS GOODS_CD_CUST              " & vbNewLine _
                                              & "   ,SE.SERIAL_NO                     AS SERIAL_NO                  " & vbNewLine _
                                              & "   ,SE.LOT_NO                        AS LOT_NO                     " & vbNewLine _
                                              & "   ,MG.NB_UT                         AS NB_UT_NM                   " & vbNewLine _
                                              & "   ,MG.STD_IRIME_UT                  AS IRIME_UT_NM                " & vbNewLine _
                                              & "   ,SE.IRIME                         AS IRIME                      " & vbNewLine _
                                              & "   ,SE.INKA_NO_L                     AS INKA_NO_L                  " & vbNewLine _
                                              & "   ,SE.KISYZAN_NB1                   AS KISYZAN_NB1                " & vbNewLine _
                                              & "   ,SE.KISYZAN_NB2                   AS KISYZAN_NB2                " & vbNewLine _
                                              & "   ,SE.KISYZAN_NB3                   AS KISYZAN_NB3                " & vbNewLine _
                                              & "   ,SE.MATUZAN_NB                    AS MATUZAN_NB                 " & vbNewLine _
                                              & "   ,SE.INKO_NB_TTL2                  AS INKO_NB_TTL2               " & vbNewLine _
                                              & "   ,SE.INKO_NB_TTL1                  AS INKO_NB_TTL1               " & vbNewLine _
                                              & "   ,SE.OUTKO_NB_TTL2                 AS OUTKO_NB_TTL2              " & vbNewLine _
                                              & "   ,SE.OUTKO_NB_TTL1                 AS OUTKO_NB_TTL1              " & vbNewLine _
                                              & "   ,0                                AS SEKI_ARI_NB1               " & vbNewLine _
                                              & "   ,0                                AS SEKI_ARI_NB2               " & vbNewLine _
                                              & "   ,0                                AS SEKI_ARI_NB3               " & vbNewLine _
                                              & "   ,0                                AS STORAGE1                   " & vbNewLine _
                                              & "   ,0                                AS STORAGE2                   " & vbNewLine _
                                              & "   ,0                                AS STORAGE3                   " & vbNewLine _
                                              & "   ,0                                AS STORAGE_AMO_TTL            " & vbNewLine _
                                              & "   ,SE.INKO_NB_TTL1 + SE.OUTKO_NB_TTL1 - (SE.INKO_NB_TTL2 + SE.OUTKO_NB_TTL2) AS HANDLING_NB_TTL          " & vbNewLine _
                                              & "   ,SE.HANDLING_IN1                  AS HANDLING_IN1               " & vbNewLine _
                                              & "   ,SE.HANDLING_AMO_TTL              AS HANDLING_AMO_TTL           " & vbNewLine _
                                              & "   ,SE.KURIKOSI_DATE1                AS KURIKOSI_DATE1             " & vbNewLine _
                                              & "   ,SE.KURIKOSI_DATE2                AS KURIKOSI_DATE2             " & vbNewLine _
                                              & "   ,SE.KURIKOSI_DATE3                AS KURIKOSI_DATE3             " & vbNewLine _
                                              & "   ,SE.MATU_DATE                     AS MATU_DATE                  " & vbNewLine _
                                              & "   --デュポン用帳票（LMG502）項目                                  " & vbNewLine _
                                              & "   ,SE.TAX_KB                        AS TAX_KB                     " & vbNewLine _
                                              & "   ,MG.CUST_COST_CD2                 AS CUST_COST_CD2              " & vbNewLine _
                                              & "   ,MG.GOODS_NM_2                    AS GOODS_NM_2                 " & vbNewLine _
                                              & "   ,MG.GOODS_NM_3                    AS GOODS_NM_3                 " & vbNewLine _
                                              & "   ,SE.INKO_NB1                      AS INKO_NB1                   " & vbNewLine _
                                              & "   ,SE.INKO_NB2                      AS INKO_NB2                   " & vbNewLine _
                                              & "   ,SE.INKO_NB3                      AS INKO_NB3                   " & vbNewLine _
                                              & "   ,SE.OUTKO_NB1                     AS OUTKO_NB1                  " & vbNewLine _
                                              & "   ,SE.OUTKO_NB2                     AS OUTKO_NB2                  " & vbNewLine _
                                              & "   ,SE.OUTKO_NB3                     AS OUTKO_NB3                  " & vbNewLine _
                                              & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                              & "   ,KBNC025_ITEM.KBN_NM1             AS STO_ITEM_CURR_CD           " & vbNewLine _
                                              & "   ,KBNC025_ITEM.KBN_NM1             AS HAND_ITEM_CURR_CD          " & vbNewLine _
                                              & "   ,ISNULL(CURR_ITEM.ROUND_POS,0)    AS ITEM_ROUND_POS             " & vbNewLine _
                                              & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine _
                                              & "   ,SE.STRAGE_HENDO_NASHI_AMO_TTL    AS STRAGE_HENDO_NASHI_AMO_TTL " & vbNewLine _
                                              & "   ,SE.INKA_DATE                     AS INKA_DATE                  " & vbNewLine _
                                              & "   ,SE.VAR_RATE                      AS VAR_RATE                   " & vbNewLine

    ''' <summary>
    ''' GROUP BY 句
    ''' </summary>
    ''' <remarks>変動保管料対応版</remarks>
    Private Const SQL_GROUP_BY_585 As String = "GROUP BY                                                           " & vbNewLine _
                                         & " RPT_ID                                                            " & vbNewLine _
                                         & ",NRS_BR_CD                                                         " & vbNewLine _
                                         & ",SEIQTO_CD                                                         " & vbNewLine _
                                         & ",SEIQTO_NM                                                         " & vbNewLine _
                                         & ",MAIN.CUST_CD_L                                                    " & vbNewLine _
                                         & ",MAIN.CUST_CD_M                                                    " & vbNewLine _
                                         & ",CUST_CD_S                                                         " & vbNewLine _
                                         & ",CUST_CD_SS                                                        " & vbNewLine _
                                         & ",CUST_NM_L                                                         " & vbNewLine _
                                         & ",CUST_NM_M                                                         " & vbNewLine _
                                         & ",CUST_NM_S                                                         " & vbNewLine _
                                         & ",CUST_NM_SS                                                        " & vbNewLine _
                                         & ",SEARCH_KEY_1                                                      " & vbNewLine _
                                         & ",JOB_NO                                                            " & vbNewLine _
                                         & ",NRS_BR_NM                                                         " & vbNewLine _
                                         & ",INV_DATE_FROM                                                     " & vbNewLine _
                                         & ",INV_DATE_TO                                                       " & vbNewLine _
                                         & ",OFB_KB                                                            " & vbNewLine _
                                         & ",OFB_KB_NM                                                         " & vbNewLine _
                                         & ",GOODS_NM_1                                                        " & vbNewLine _
                                         & ",PKG_UT                                                            " & vbNewLine _
                                         & ",GOODS_CD_CUST                                                     " & vbNewLine _
                                         & ",SERIAL_NO                                                         " & vbNewLine _
                                         & ",LOT_NO                                                            " & vbNewLine _
                                         & ",NB_UT_NM                                                          " & vbNewLine _
                                         & ",IRIME_UT_NM                                                       " & vbNewLine _
                                         & ",IRIME                                                             " & vbNewLine _
                                         & ",INKA_NO_L                                                         " & vbNewLine _
                                         & ",KISYZAN_NB1                                                       " & vbNewLine _
                                         & ",KISYZAN_NB2                                                       " & vbNewLine _
                                         & ",KISYZAN_NB3                                                       " & vbNewLine _
                                         & ",MATUZAN_NB                                                        " & vbNewLine _
                                         & ",KURIKOSI_DATE1                                                    " & vbNewLine _
                                         & ",KURIKOSI_DATE2                                                    " & vbNewLine _
                                         & ",KURIKOSI_DATE3                                                    " & vbNewLine _
                                         & ",MATU_DATE                                                         " & vbNewLine _
                                         & "--デュポン用帳票（LMG502）項目                                     " & vbNewLine _
                                         & ",TAX_KB                                                            " & vbNewLine _
                                         & ",CUST_COST_CD2                                                     " & vbNewLine _
                                         & ",GOODS_NM_2                                                        " & vbNewLine _
                                         & ",GOODS_NM_3                                                        " & vbNewLine _
                                         & ",INKO_NB1                                                          " & vbNewLine _
                                         & ",INKO_NB2                                                          " & vbNewLine _
                                         & ",INKO_NB3                                                          " & vbNewLine _
                                         & ",OUTKO_NB1                                                         " & vbNewLine _
                                         & ",OUTKO_NB2                                                         " & vbNewLine _
                                         & ",OUTKO_NB3                                                         " & vbNewLine _
                                         & "   --(2014.08.21) 追加START 多通貨対応                             " & vbNewLine _
                                         & ",STO_ITEM_CURR_CD                                                  " & vbNewLine _
                                         & ",HAND_ITEM_CURR_CD                                                 " & vbNewLine _
                                         & ",ITEM_ROUND_POS                                                    " & vbNewLine _
                                         & "   --(2014.08.21) 追加END   多通貨対応                             " & vbNewLine _
                                         & ",STRAGE_HENDO_NASHI_AMO_TTL                                        " & vbNewLine _
                                         & ",INKA_DATE                                                         " & vbNewLine _
                                         & ",VAR_RATE                                                          " & vbNewLine

    ''' <summary>
    ''' ORDER BY 句
    ''' </summary>
    ''' <remarks>変動保管料対応版</remarks>
    Private Const SQL_ORDER_BY_585 As String = "ORDER BY                                                           " & vbNewLine _
                                         & " SEIQTO_CD                                                         " & vbNewLine _
                                         & ",CUST_CD_L                                                         " & vbNewLine _
                                         & ",CUST_CD_M                                                         " & vbNewLine _
                                         & ",CUST_CD_S                                                         " & vbNewLine _
                                         & ",CUST_CD_SS                                                        " & vbNewLine _
                                         & ",SEARCH_KEY_1                                                      " & vbNewLine _
                                         & ",OFB_KB                                                            " & vbNewLine _
                                         & ",GOODS_NM_1                                                        " & vbNewLine _
                                         & ",GOODS_CD_CUST                                                     " & vbNewLine _
                                         & ",LOT_NO                                                            " & vbNewLine _
                                         & ",INKA_NO_L                                                         " & vbNewLine _
                                         & ",INKA_DATE                                                         " & vbNewLine

#End Region '"LMG585"

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
    Private Function SelectJobNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG500IN")
        '担当分のみ表示フラグ取得
        Dim tantoUserFlg As String = inTbl.Rows(0).Item("TANTO_USER_FLG").ToString()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG500DAC.SQL_SELECT_JOB)      'SQL構築(帳票種別用Select句)
        Call Me.SetConditionMasterSQL_JOB_No(tantoUserFlg)  '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG500DAC", "SelectJobNo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("JOB_NO", "JOB_NO")
        

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG500OUT")

        Return ds

    End Function
    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG500IN")
        '担当分のみ表示フラグ取得
        Dim tantoUserFlg As String = inTbl.Rows(0).Item("TANTO_USER_FLG").ToString()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG500DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMG500DAC.SQL_FROM_MPrt)        'SQL構築(帳票種別用From句)
        Call Me.SetConditionMasterSQL_Mprt(tantoUserFlg)  '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG500DAC", "SelectMPrt", cmd)

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
    ''' 保管料・荷役料請求明細出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>保管料・荷役料請求明細出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG500IN")
        '担当分のみ表示フラグ取得
        Dim tantoUserFlg As String = inTbl.Rows(0).Item("TANTO_USER_FLG").ToString()
        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'RPT_IDのチェック用
        Dim rptId As String = rptTbl.Rows(0).Item("RPT_ID").ToString()

        'SQL作成
        '出力帳票がLMG502(保管料・荷役料請求明細書(デュポン))の場合は、LMG502(保管料・荷役料請求明細書(デュポン))を構築
        'If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMG502" Then
        Select Case rptId
            Case "LMG502"
                'SQL作成
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_LMG502)        'SQL構築(データ抽出用Select句デュポン用)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_MAIN)          'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_HOKAN)         'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_HOKAN)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_NIYAKU)        'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_NIYAKU)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(" ) MAIN ")
                Me._StrSql.Append(LMG500DAC.SQL_GROUP_BY)             'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(LMG500DAC.SQL_GROUP_BY_LMG502)      'SQL構築(データ抽出用GROUP BY句デュポン用)
                Me._StrSql.Append(LMG500DAC.SQL_ORDER_BY_LMG502)      'SQL構築(データ抽出用ORDER BY句デュポン用)
            Case "LMG503"
                'SQL作成
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_LMG503)        'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_HOKAN_LMG503)  'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_HOKAN)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_NIYAKU_LMG503) 'SQL構築(データ抽出用Select句荷役料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_NIYAKU)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(" ) MAIN ")
                Me._StrSql.Append(LMG500DAC.SQL_GROUP_BY)      'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(LMG500DAC.SQL_ORDER_BY_LMG503)      'SQL構築(データ抽出用ORDER BY句)

                'Else
                '作成中の暫定処置↓
            Case "LMG504"
                'SQL作成
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_MAIN_504)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_HOKAN_504)     'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_HOKAN_504)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(LMG500DAC.SQL_GROUP_BY_HOKAN_504)   'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_NIYAKU_504)    'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_NIYAKU_504)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(LMG500DAC.SQL_GROUP_BY_NIYAKU_504)  'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(" ) MAIN ")
                Me._StrSql.Append(LMG500DAC.SQL_GROUP_BY_MAIN_504)    'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(LMG500DAC.SQL_ORDER_BY_504)         'SQL構築(データ抽出用ORDER BY句)
            Case "LMG505"
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_MAIN_504)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_HOKAN_504)     'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_HOKAN_504)
                Call Me.SetConditionMasterSQL_505(tantoUserFlg)       'SQL構築(条件設定)
                Me._StrSql.Append(LMG500DAC.SQL_GROUP_BY_HOKAN_504)         'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_NIYAKU_504)    'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_NIYAKU_504)
                Call Me.SetConditionMasterSQL_505(tantoUserFlg)       'SQL構築(条件設定)
                Me._StrSql.Append(LMG500DAC.SQL_GROUP_BY_NIYAKU_504)  'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(" ) MAIN ")
                Me._StrSql.Append(LMG500DAC.SQL_GROUP_BY_MAIN_504)    'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(LMG500DAC.SQL_ORDER_BY_505)         'SQL構築(データ抽出用ORDER BY
            Case "LMG507"
                'SQL作成
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_MAIN)          'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_HOKAN)         'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_HOKAN)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_NIYAKU)        'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_NIYAKU)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(" ) MAIN ")
                Me._StrSql.Append(LMG500DAC.SQL_GROUP_BY)             'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(LMG500DAC.SQL_ORDER_BY_507)         'SQL構築(データ抽出用ORDER BY句)
            Case "LMG518"
                'SQL作成
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_MAIN_518)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_HOKAN_518)     'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_HOKAN)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_NIYAKU_518)    'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_NIYAKU)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(" ) MAIN ")
                Me._StrSql.Append(LMG500DAC.SQL_GROUP_BY)             'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(LMG500DAC.SQL_ORDER_BY)             'SQL構築(データ抽出用ORDER BY句)
            Case "LMG585"
                'SQL作成
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_MAIN_585)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_HOKAN_585)     'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_HOKAN)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_NIYAKU_585)    'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_NIYAKU)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(" ) MAIN ")
                Me._StrSql.Append(LMG500DAC.SQL_GROUP_BY_585)         'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(LMG500DAC.SQL_ORDER_BY_585)         'SQL構築(データ抽出用ORDER BY句)
            Case Else
                'SQL作成
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_MAIN)          'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_HOKAN)         'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_HOKAN)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(LMG500DAC.SQL_SELECT_NIYAKU)        'SQL構築(データ抽出用Select句保管料)
                Me._StrSql.Append(LMG500DAC.SQL_FROM_NIYAKU)
                Call Me.SetConditionMasterSQL(tantoUserFlg)           'SQL構築(条件設定)
                Me._StrSql.Append(" ) MAIN ")
                Me._StrSql.Append(LMG500DAC.SQL_GROUP_BY)             'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(LMG500DAC.SQL_ORDER_BY)             'SQL構築(データ抽出用ORDER BY句)

                'End If
        End Select

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG500DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()


        '取得データの格納先をマッピング


        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("JOB_NO", "JOB_NO")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("INV_DATE_FROM", "INV_DATE_FROM")
        map.Add("INV_DATE_TO", "INV_DATE_TO")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("OFB_KB_NM", "OFB_KB_NM")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("NB_UT_NM", "NB_UT_NM")
        map.Add("IRIME_UT_NM", "IRIME_UT_NM")
        map.Add("IRIME", "IRIME")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("KISYZAN_NB1", "KISYZAN_NB1")
        map.Add("KISYZAN_NB2", "KISYZAN_NB2")
        map.Add("KISYZAN_NB3", "KISYZAN_NB3")
        map.Add("MATUZAN_NB", "MATUZAN_NB")
        map.Add("INKO_NB_TTL2", "INKO_NB_TTL2")
        map.Add("INKO_NB_TTL1", "INKO_NB_TTL1")
        map.Add("OUTKO_NB_TTL2", "OUTKO_NB_TTL2")
        map.Add("OUTKO_NB_TTL1", "OUTKO_NB_TTL1")
        map.Add("SEKI_ARI_NB1", "SEKI_ARI_NB1")
        map.Add("SEKI_ARI_NB2", "SEKI_ARI_NB2")
        map.Add("SEKI_ARI_NB3", "SEKI_ARI_NB3")
        map.Add("STORAGE1", "STORAGE1")
        map.Add("STORAGE2", "STORAGE2")
        map.Add("STORAGE3", "STORAGE3")
        map.Add("STORAGE_AMO_TTL", "STORAGE_AMO_TTL")
        map.Add("HANDLING_NB_TTL", "HANDLING_NB_TTL")
        map.Add("HANDLING_IN1", "HANDLING_IN1")
        map.Add("HANDLING_IN2", "HANDLING_IN2")
        map.Add("HANDLING_IN3", "HANDLING_IN3")
        map.Add("HANDLING_AMO_TTL", "HANDLING_AMO_TTL")
        map.Add("KURIKOSI_DATE1", "KURIKOSI_DATE1")
        map.Add("KURIKOSI_DATE2", "KURIKOSI_DATE2")
        map.Add("KURIKOSI_DATE3", "KURIKOSI_DATE3")
        map.Add("MATU_DATE", "MATU_DATE")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        map.Add("GOODS_NM_2", "GOODS_NM_2")
        map.Add("GOODS_NM_3", "GOODS_NM_3")
        map.Add("INKO_NB1", "INKO_NB1")
        map.Add("INKO_NB2", "INKO_NB2")
        map.Add("INKO_NB3", "INKO_NB3")
        map.Add("OUTKO_NB1", "OUTKO_NB1")
        map.Add("OUTKO_NB2", "OUTKO_NB2")
        map.Add("OUTKO_NB3", "OUTKO_NB3")
        '2014.08.21 追加START 多通貨対応
        map.Add("STO_ITEM_CURR_CD", "STO_ITEM_CURR_CD")
        map.Add("HAND_ITEM_CURR_CD", "HAND_ITEM_CURR_CD")
        map.Add("ITEM_ROUND_POS", "ITEM_ROUND_POS")
        '2014.08.21 追加END 多通貨対応
        map.Add("PKG_UT", "PKG_UT")

        Select Case rptId
            Case "LMG518"
                map.Add("HANDLING_OUT1", "HANDLING_OUT1")
        End Select

        Select Case rptId
            Case "LMG585"
                map.Add("STRAGE_HENDO_NASHI_AMO_TTL", "STRAGE_HENDO_NASHI_AMO_TTL")
                map.Add("INKA_DATE", "INKA_DATE")
                map.Add("VAR_RATE", "VAR_RATE")
        End Select

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG500OUT")


        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_Mprt(ByVal tantouUserFlg As String)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SE.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード大
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MC.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード中
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MC.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード小
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MC.CUST_CD_S LIKE @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード極小
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MC.CUST_CD_SS LIKE @CUST_CD_GOS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_GOS", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '請求先コード
            'whereStr = .Item("SEIQTO_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND MS.SEIQTO_CD LIKE @SEIQTO_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            'End If

            '請求先コード 20160819 要番：2605 tsunehira add start 
            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MS.SEIQTO_CD = @SEIQTO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", whereStr, DBDataType.CHAR))
            End If
            '20160819 要番：2605 tsunehira add end

            '請求期間TO
            whereStr = .Item("INV_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SE.INV_DATE_TO = @INV_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            'JOB番号
            'whereStr = .Item("JOB_NO").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND SE.JOB_NO = @JOB_NO")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", whereStr, DBDataType.CHAR))
            'End If

            'JOB番号
            whereStr = .Item("JOB_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                If InStr(whereStr, ",") > 1 Then
                    Dim strJOBNOList As String = String.Empty
                    For Each strJOB_NO As String In whereStr.Split(CChar(","))
                        If strJOBNOList.Equals(String.Empty) = True Then
                            strJOBNOList &= "'" & strJOB_NO & "'"
                        Else
                            strJOBNOList &= ",'" & strJOB_NO & "'"
                        End If
                    Next
                    Me._StrSql.Append(" AND SE.JOB_NO in (" & strJOBNOList & ")")
                    Me._StrSql.Append(vbNewLine)
                Else
                    Me._StrSql.Append(" AND SE.JOB_NO = @JOB_NO")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", whereStr, DBDataType.CHAR))
                End If


            End If

            'USER_CD
            If tantouUserFlg = "1" Then
                whereStr = .Item("USER_CD").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    ' '' ''        Me._StrSql.Append(" AND TC.USER_CD = @USER_CD")
                    Me._StrSql.Append(" AND SE.SYS_ENT_USER = @USER_CD")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", whereStr, DBDataType.CHAR))
                End If
            End If

            '請求フラグ
            whereStr = .Item("SEKY_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) Then
                ' 値が設定されていない場合、"00"：本番 のデータを抽出
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_FLG", "00", DBDataType.CHAR))
            Else
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_FLG", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_JOB_No(ByVal tantouUserFlg As String)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SE.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード大
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MC.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード中
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MC.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード小
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MC.CUST_CD_S LIKE @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード極小
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MC.CUST_CD_SS LIKE @CUST_CD_GOS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_GOS", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '請求先コード
            'whereStr = .Item("SEIQTO_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND (MC.HOKAN_SEIQTO_CD LIKE @SEIQTO_CD OR MC.NIYAKU_SEIQTO_CD LIKE @SEIQTO_CD)")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            'End If

            '請求先コード 20160819 要番：2605 tsunehira add start 
            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (MC.HOKAN_SEIQTO_CD = @SEIQTO_CD OR MC.NIYAKU_SEIQTO_CD = @SEIQTO_CD)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", whereStr, DBDataType.CHAR))
            End If
            '20160819 要番：2605 end add start

            '請求期間TO
            whereStr = .Item("INV_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SE.INV_DATE_TO = @INV_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            'JOB番号
            whereStr = .Item("JOB_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                If InStr(whereStr, ",") > 1 Then
                    Dim strJOBNOList As String = String.Empty
                    For Each strJOB_NO As String In whereStr.Split(CChar(","))
                        If strJOBNOList.Equals(String.Empty) = True Then
                            strJOBNOList &= "'" & strJOB_NO & "'"
                        Else
                            strJOBNOList &= ",'" & strJOB_NO & "'"
                        End If
                    Next
                    Me._StrSql.Append(" AND SE.JOB_NO in (" & strJOBNOList & ")")
                    Me._StrSql.Append(vbNewLine)
                Else
                    Me._StrSql.Append(" AND SE.JOB_NO = @JOB_NO")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", whereStr, DBDataType.CHAR))
                End If


            End If


            '請求フラグ
            whereStr = .Item("SEKY_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) Then
                ' 値が設定されていない場合、"00"：本番 のデータを抽出
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_FLG", "00", DBDataType.CHAR))
            Else
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_FLG", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(ByVal tantouUserFlg As String)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SE.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード大
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MC.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード中
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MC.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード小
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MC.CUST_CD_S LIKE @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード極小
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MC.CUST_CD_SS LIKE @CUST_CD_GOS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_GOS", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '請求先コード
            'whereStr = .Item("SEIQTO_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND MS.SEIQTO_CD LIKE @SEIQTO_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            'End If

            '請求先コード 20160819 要番：2605 tsunehira add start 
            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MS.SEIQTO_CD = @SEIQTO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", whereStr, DBDataType.CHAR))
            End If


            '請求期間TO
            whereStr = .Item("INV_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SE.INV_DATE_TO = @INV_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            ''JOB番号
            'whereStr = .Item("JOB_NO").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND SE.JOB_NO = @JOB_NO")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", whereStr, DBDataType.CHAR))
            'End If


            'JOB番号
            whereStr = .Item("JOB_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                If InStr(whereStr, ",") > 1 Then
                    Dim strJOBNOList As String = String.Empty
                    For Each strJOB_NO As String In whereStr.Split(CChar(","))
                        If strJOBNOList.Equals(String.Empty) = True Then
                            strJOBNOList &= "'" & strJOB_NO & "'"
                        Else
                            strJOBNOList &= ",'" & strJOB_NO & "'"
                        End If
                    Next
                    Me._StrSql.Append(" AND SE.JOB_NO in (" & strJOBNOList & ")")
                    Me._StrSql.Append(vbNewLine)
                Else
                    Me._StrSql.Append(" AND SE.JOB_NO = @JOB_NO")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", whereStr, DBDataType.CHAR))
                End If


            End If

            'USER_CD
            If tantouUserFlg = "1" Then
                whereStr = .Item("USER_CD").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND SE.SYS_ENT_USER = @USER_CD")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", whereStr, DBDataType.CHAR))
                End If
            End If

            '請求フラグ
            whereStr = .Item("SEKY_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) Then
                ' 値が設定されていない場合、"00"：本番 のデータを抽出
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_FLG", "00", DBDataType.CHAR))
            Else
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_FLG", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール LMG505用
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_505(ByVal tantouUserFlg As String)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SE.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード大
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MC.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If


            '請求期間TO
            whereStr = .Item("INV_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SE.INV_DATE_TO = @INV_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            'JOB番号
            whereStr = .Item("JOB_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SE.JOB_NO = @JOB_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionUserCd()


        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            'USER_CD
            whereStr = .Item("USER_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", whereStr, DBDataType.CHAR))

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
