' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC930    : 現場作業指示
'  作  成  者       :  [HOJO]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports System.Reflection

''' <summary>
''' LMC930DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC930DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "テーブル名"

    Public Class TABLE_NM
        Public Const LMC930IN As String = "LMC930IN"
        Public Const LMC930IN_PICK_HEAD As String = "LMC930IN_TC_PICK_HEAD"
        Public Const LMC930IN_PICK_DTL As String = "LMC930IN_TC_PICK_DTL"
        Public Const LMC930IN_KENPIN_DTL As String = "LMC930IN_TC_KENPIN_DTL"
        Public Const LMC930IN_SAGYO As String = "LMC930IN_TC_SAGYO"
        Public Const LMC930OUT_PICK_HEAD As String = "LMC930OUT_TC_PICK_HEAD"
        Public Const LMC930OUT_PICK_DTL As String = "LMC930OUT_TC_PICK_DTL"
        Public Const LMC930OUT_KENPIN_HEAD As String = "LMC930OUT_TC_KENPIN_HEAD"
        Public Const LMC930OUT_KENPIN_DTL As String = "LMC930OUT_TC_KENPIN_DTL"
        Public Const LMC930OUT_SAGYO As String = "LMC930OUT_TC_SAGYO"
        Public Const LMC930CNT As String = "LMC930CNT"
        Public Const LMC930CHECK As String = "LMC930CHECK"
        Public Const LMC930IN_JISYATASYA As String = "LMC930IN_JISYATASYA"
        Public Const LMC930OUT_JISYATASYA As String = "LMC930OUT_JISYATASYA"
        Public Const LMC930OUT_UNSO As String = "LMC930OUT_UNSO"
        Public Const LMC930_M_SOKO As String = "LMC930_M_SOKO"
        Public Const LMC930_F_UNSO_L As String = "LMC930_F_UNSO_L"
        Public Const LMC930_M_UNSOCO As String = "LMC930_M_UNSOCO"
        Public Const LMC930COMPARE_TC_PICK_HEAD As String = "LMC930COMPARE_TC_PICK_HEAD"
        Public Const LMC930COMPARE_TC_PICK_DTL As String = "LMC930COMPARE_TC_PICK_DTL"
        Public Const LMC930IN_TC_DIFF_COMPARISON As String = "LMC930IN_TC_DIFF_COMPARISON"
        Public Const LMC930IN_M_CUST_DETAILS As String = "LMC930IN_M_CUST_DETAILS"
        Public Const LMC930OUT_M_CUST_DETAILS As String = "LMC930OUT_M_CUST_DETAILS"
    End Class

#End Region

#Region "ファンクション名"

    Public Class FUNCTION_NM
        Public Const PICK_SELECT_HEAD As String = "SelectPickHead"
        Public Const PICK_INSERT_HEAD As String = "InsertPickHead"
        Public Const PICK_SELECT_DTL As String = "SelectPickDtl"
        Public Const PICK_INSERT_DTL As String = "InsertPickDtl"
        Public Const PICK_INSERT_DTL_PLACEHOLDER As String = "InsertPickDtlPlaceHolder"
        Public Const PICK_CANCEL As String = "UpdatePickCancel"
        Public Const PICK_DELETE As String = "UpdatePickDelete"
        Public Const PICK_SELECT_LMS_DATA As String = "SelectLMSPickData"
        Public Const PICK_SELECT_LMS_PICK_DTL_DATA As String = "SelectLMSPickDtlData"
        Public Const PICK_UPDATE_UNSO_DATA As String = "UpdatePickUnsoData"
        Public Const KENPIN_SELECT_HEAD As String = "SelectKenpinHead"
        Public Const KENPIN_SELECT_DTL As String = "SelectKenpinDtl"
        Public Const KENPIN_SELECT_LMS_KENPIN_DTL_DATA As String = "SelectLMSKenpinDtlData"
        Public Const KENPIN_INSERT_HEAD As String = "InsertKenpinHead"
        Public Const KENPIN_INSERT_DTL As String = "InsertKenpinDtl"
        Public Const KENPIN_INSERT_DTL_PLACEHOLDER As String = "InsertKenpinDtlPlaceHolder"
        Public Const KENPIN_CANCEL As String = "UpdateKenpinCancel"
        Public Const KENPIN_DELETE As String = "UpdateKenpinDelete"
        Public Const KENPIN_HEAD_UPDATE_UNSO_DATA As String = "UpdateKenpinHeadUnsoData"
        Public Const KENPIN_DTL_UPDATE_UNSO_DATA As String = "UpdateKenpinDtlUnsoData"
        Public Const SELECT_TC_SAGYO As String = "SelectTcSagyo"
        Public Const SAGYO_SELECT_LMS_DATA As String = "SelectLMSSagyoData"
        Public Const SAGYO_INSERT As String = "InsertSagyo"
        Public Const SAGYO_DELETE As String = "UpdateSagyoDelete"
        Public Const UPDATE_WH_STATUS As String = "UpdateWHSagyoShijiStatus"
        Public Const CHECK_TABLET_USE As String = "CheckTabletUse"
        Public Const CHECK_HAITA As String = "CheckHaita"
        Public Const CHECK_OUTKA_DATA As String = "SelectOutkaCheckData"
        Public Const SELECT_JISYATASYA_COUNT As String = "SelectJisyaTasyaCount"
        Public Const SELECT_SOKO_DATA As String = "SelectSokoData"
        Public Const SELECT_UNSO_DATA As String = "SelectUnsoData"
        Public Const SELECT_UNSOCO_DATA As String = "SelectUnsocoData"
        Public Const SELECT_PICK_HEAD_TO_COMPARE As String = "SelectPickHeadToCompare"
        Public Const SELECT_PICK_DTL_TO_COMPARE As String = "SelectPickDtlToCompare"
        Public Const INSERT_DIFF_COMPARISON As String = "InsertDiffComparison"
        Public Const SELECT_M_CUST_DETAILS As String = "SelectMCustDetails"
        Public Const UPDATE_PICK_HEAD_MEISAI_CANCEL As String = "UpdatePickHeadMeisaiCancel"
        Public Const UPDATE_PICK_DTL_MEISAI_CANCEL As String = "UpdatePickDtlMeisaiCancel"
        Public Const UPDATE_KENPIN_HEAD_MEISAI_CANCEL As String = "UpdateKenpinHeadMeisaiCancel"
        Public Const UPDATE_KENPIN_DTL_MEISAI_CANCEL As String = "UpdateKenpinDtlMeisaiCancel"
    End Class

#End Region

#Region "Select"

#Region "出荷ピックヘッダ取得"
    ''' <summary>
    ''' 出荷ピックヘッダ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TC_PICK_HEAD As String _
    = " SELECT                                                                          " & vbNewLine _
    & "   MAIN.NRS_BR_CD                    AS  NRS_BR_CD                               " & vbNewLine _
    & " , MAIN.OUTKA_NO_L                   AS  OUTKA_NO_L                              " & vbNewLine _
    & " , MAIN.TOU_NO                       AS  TOU_NO                                  " & vbNewLine _
    & " , MAIN.SITU_NO                      AS  SITU_NO                                 " & vbNewLine _
    & " , MAIN.PICK_SEQ                     AS  PICK_SEQ                                " & vbNewLine _
    & " , MAIN.CANCEL_FLG                   AS  CANCEL_FLG                              " & vbNewLine _
    & " , CASE WHEN @PROC_TYPE = '00'  THEN '01'                                        " & vbNewLine _
    & "        WHEN @PROC_TYPE = '01'  THEN '02'                                        " & vbNewLine _
    & "        WHEN @PROC_TYPE = '02'  THEN '02'                                        " & vbNewLine _
    & "        ELSE ''                                                                  " & vbNewLine _
    & "   END                               AS  CANCEL_TYPE                             " & vbNewLine _
    & " , MAIN.PICK_USER_CD                 AS  PICK_USER_CD                            " & vbNewLine _
    & " , MAIN.PICK_USER_NM                 AS  PICK_USER_NM                            " & vbNewLine _
    & " , MAIN.PICK_STATE_KB                AS  PICK_STATE_KB                           " & vbNewLine _
    & " , MAIN.WORK_STATE_KB                AS  WORK_STATE_KB                           " & vbNewLine _
    & " , MAIN.WH_CD                        AS  WH_CD                                   " & vbNewLine _
    & " , MAIN.CUST_CD_L                    AS  CUST_CD_L                               " & vbNewLine _
    & " , MAIN.CUST_CD_M                    AS  CUST_CD_M                               " & vbNewLine _
    & " , MAIN.CUST_NM_L                    AS  CUST_NM_L                               " & vbNewLine _
    & " , MAIN.CUST_NM_M                    AS  CUST_NM_M                               " & vbNewLine _
    & " , MAIN.UNSO_CD                      AS  UNSO_CD                                 " & vbNewLine _
    & " , MAIN.UNSO_NM                      AS  UNSO_NM                                 " & vbNewLine _
    & " , MAIN.UNSO_BR_CD                   AS  UNSO_BR_CD                              " & vbNewLine _
    & " , MAIN.UNSO_BR_NM                   AS  UNSO_BR_NM                              " & vbNewLine _
    & " , MAIN.DEST_CD                      AS  DEST_CD                                 " & vbNewLine _
    & " , MAIN.DEST_NM                      AS  DEST_NM                                 " & vbNewLine _
    & " , MAIN.DEST_AD_1                    AS  DEST_AD_1                               " & vbNewLine _
    & " , MAIN.DEST_AD_2                    AS  DEST_AD_2                               " & vbNewLine _
    & " , MAIN.DEST_AD_3                    AS  DEST_AD_3                               " & vbNewLine _
    & " , MAIN.DEST_TEL                     AS  DEST_TEL                                " & vbNewLine _
    & " , MAIN.OUTKO_DATE                   AS  OUTKO_DATE                              " & vbNewLine _
    & " , MAIN.OUTKA_PLAN_DATE              AS  OUTKA_PLAN_DATE                         " & vbNewLine _
    & " , MAIN.ARR_PLAN_DATE                AS  ARR_PLAN_DATE                           " & vbNewLine _
    & " , MAIN.ARR_PLAN_TIME                AS  ARR_PLAN_TIME                           " & vbNewLine _
    & " , MAIN.REMARK                       AS  REMARK                                  " & vbNewLine _
    & " , MAIN.REMARK_SIJI                  AS  REMARK_SIJI                             " & vbNewLine _
    & " , MAIN.CUST_ORD_NO                  AS  CUST_ORD_NO                             " & vbNewLine _
    & " , MAIN.BUYER_ORD_NO                 AS  BUYER_ORD_NO                            " & vbNewLine _
    & " , MAIN.OUTKA_PKG_NB                 AS  OUTKA_PKG_NB                            " & vbNewLine _
    & " , MAIN.OUTKA_TTL_NB                 AS  OUTKA_TTL_NB                            " & vbNewLine _
    & " , MAIN.OUTKA_TTL_WT                 AS  OUTKA_TTL_WT                            " & vbNewLine _
    & " , MAIN.URGENT_FLG                   AS  URGENT_FLG                              " & vbNewLine _
    & " , MAIN.REMARK_CHK_FLG               AS  REMARK_CHK_FLG                          " & vbNewLine _
    & " , MAIN.JISYATASYA_KB                AS  JISYATASYA_KB                           " & vbNewLine _
    & " , MAIN.SYS_ENT_DATE                 AS  SYS_ENT_DATE                            " & vbNewLine _
    & " , MAIN.SYS_ENT_TIME                 AS  SYS_ENT_TIME                            " & vbNewLine _
    & " , MAIN.SYS_ENT_PGID                 AS  SYS_ENT_PGID                            " & vbNewLine _
    & " , MAIN.SYS_ENT_USER                 AS  SYS_ENT_USER                            " & vbNewLine _
    & " , MAIN.SYS_UPD_DATE                 AS  SYS_UPD_DATE                            " & vbNewLine _
    & " , MAIN.SYS_UPD_TIME                 AS  SYS_UPD_TIME                            " & vbNewLine _
    & " , MAIN.SYS_UPD_PGID                 AS  SYS_UPD_PGID                            " & vbNewLine _
    & " , MAIN.SYS_UPD_USER                 AS  SYS_UPD_USER                            " & vbNewLine _
    & " , MAIN.SYS_DEL_FLG                  AS  SYS_DEL_FLG                             " & vbNewLine _
    & " FROM $LM_TRN$..TC_PICK_HEAD MAIN                                                " & vbNewLine _
    & " INNER JOIN (                                                                    " & vbNewLine _
    & "     SELECT                                                                      " & vbNewLine _
    & "         NRS_BR_CD                                                               " & vbNewLine _
    & "        ,OUTKA_NO_L                                                              " & vbNewLine _
    & "        ,MAX(PICK_SEQ) AS PICK_SEQ                                               " & vbNewLine _
    & "     FROM $LM_TRN$..TC_PICK_HEAD                                                   " & vbNewLine _
    & "     GROUP BY                                                                    " & vbNewLine _
    & "         NRS_BR_CD                                                               " & vbNewLine _
    & "        ,OUTKA_NO_L                                                              " & vbNewLine _
    & "     )MAX_SEQ                                                                    " & vbNewLine _
    & " ON  MAX_SEQ.NRS_BR_CD  = MAIN.NRS_BR_CD                                         " & vbNewLine _
    & " AND MAX_SEQ.OUTKA_NO_L = MAIN.OUTKA_NO_L                                        " & vbNewLine _
    & " AND MAX_SEQ.PICK_SEQ   = MAIN.PICK_SEQ                                          " & vbNewLine _
    & " WHERE                                                                           " & vbNewLine _
    & "     MAIN.NRS_BR_CD  = @NRS_BR_CD                                                " & vbNewLine _
    & " AND MAIN.OUTKA_NO_L = @OUTKA_NO_L                                               " & vbNewLine

#End Region

#Region "出荷ピック明細取得"
    ''' <summary>
    ''' 出荷ピック明細取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TC_PICK_DTL As String _
    = " SELECT                                                                          " & vbNewLine _
    & "   MAIN.NRS_BR_CD                                                                " & vbNewLine _
    & " , MAIN.OUTKA_NO_L                                                               " & vbNewLine _
    & " , MAIN.TOU_NO                                                                   " & vbNewLine _
    & " , MAIN.SITU_NO                                                                  " & vbNewLine _
    & " , MAIN.PICK_SEQ                                                                 " & vbNewLine _
    & " , MAIN.OUTKA_NO_M                                                               " & vbNewLine _
    & " , MAIN.OUTKA_NO_S                                                               " & vbNewLine _
    & " , MAIN.PICK_STATE_KB                                                            " & vbNewLine _
    & " , MAIN.GOODS_CD_NRS                                                             " & vbNewLine _
    & " , MAIN.GOODS_CD_CUST                                                            " & vbNewLine _
    & " , MAIN.GOODS_NM_NRS                                                             " & vbNewLine _
    & " , MAIN.LOT_NO                                                                   " & vbNewLine _
    & " , MAIN.IRIME                                                                    " & vbNewLine _
    & " , MAIN.IRIME_UT                                                                 " & vbNewLine _
    & " , MAIN.NB_UT                                                                    " & vbNewLine _
    & " , MAIN.PKG_NB                                                                   " & vbNewLine _
    & " , MAIN.PKG_UT                                                                   " & vbNewLine _
    & " , MAIN.ZONE_CD                                                                  " & vbNewLine _
    & " , MAIN.LOCA                                                                     " & vbNewLine _
    & " , MAIN.OUT_NB                                                                   " & vbNewLine _
    & " , MAIN.OUT_KONSU                                                                " & vbNewLine _
    & " , MAIN.OUT_HASU                                                                 " & vbNewLine _
    & " , MAIN.OUT_ZAN_NB                                                               " & vbNewLine _
    & " , MAIN.OUT_ZAN_KONSU                                                            " & vbNewLine _
    & " , MAIN.OUT_ZAN_HASU                                                             " & vbNewLine _
    & " , MAIN.OUT_QT                                                                   " & vbNewLine _
    & " , MAIN.OUT_ZAN_QT                                                               " & vbNewLine _
    & " , MAIN.GOODS_COND_KB1                                                           " & vbNewLine _
    & " , MAIN.GOODS_COND_KB2                                                           " & vbNewLine _
    & " , MAIN.GOODS_COND_KB3                                                           " & vbNewLine _
    & " , MAIN.LT_DATE                                                                  " & vbNewLine _
    & " , MAIN.GOODS_CRT_DATE                                                           " & vbNewLine _
    & " , MAIN.SPD_KB                                                                   " & vbNewLine _
    & " , MAIN.OFB_KB                                                                   " & vbNewLine _
    & " , MAIN.RSV_NO                                                                   " & vbNewLine _
    & " , MAIN.SERIAL_NO                                                                " & vbNewLine _
    & " , MAIN.INKA_NO_L                                                                " & vbNewLine _
    & " , MAIN.INKA_NO_M                                                                " & vbNewLine _
    & " , MAIN.INKA_NO_S                                                                " & vbNewLine _
    & " , MAIN.ZAI_REC_NO                                                               " & vbNewLine _
    & " , MAIN.INKA_DATE                                                                " & vbNewLine _
    & " , MAIN.REMARK                                                                   " & vbNewLine _
    & " , MAIN.REMARK_OUT                                                               " & vbNewLine _
    & " , MAIN.JISYATASYA_KB                                                            " & vbNewLine _
    & " , MAIN.CANCEL_DTL_FLG                                                           " & vbNewLine _
    & " , MAIN.SYS_ENT_DATE                                                             " & vbNewLine _
    & " , MAIN.SYS_ENT_TIME                                                             " & vbNewLine _
    & " , MAIN.SYS_ENT_PGID                                                             " & vbNewLine _
    & " , MAIN.SYS_ENT_USER                                                             " & vbNewLine _
    & " , MAIN.SYS_UPD_DATE                                                             " & vbNewLine _
    & " , MAIN.SYS_UPD_TIME                                                             " & vbNewLine _
    & " , MAIN.SYS_UPD_PGID                                                             " & vbNewLine _
    & " , MAIN.SYS_UPD_USER                                                             " & vbNewLine _
    & " , MAIN.SYS_DEL_FLG                                                              " & vbNewLine _
    & " FROM $LM_TRN$..TC_PICK_DTL  MAIN                                                " & vbNewLine _
    & " INNER JOIN (                                                                    " & vbNewLine _
    & "     SELECT                                                                      " & vbNewLine _
    & "         NRS_BR_CD                                                               " & vbNewLine _
    & "        ,OUTKA_NO_L                                                              " & vbNewLine _
    & "        ,MAX(PICK_SEQ) AS PICK_SEQ                                               " & vbNewLine _
    & "     FROM $LM_TRN$..TC_PICK_HEAD                                                 " & vbNewLine _
    & "     GROUP BY                                                                    " & vbNewLine _
    & "         NRS_BR_CD                                                               " & vbNewLine _
    & "        ,OUTKA_NO_L                                                              " & vbNewLine _
    & "     )MAX_SEQ                                                                    " & vbNewLine _
    & " ON  MAX_SEQ.NRS_BR_CD  = MAIN.NRS_BR_CD                                         " & vbNewLine _
    & " AND MAX_SEQ.OUTKA_NO_L = MAIN.OUTKA_NO_L                                        " & vbNewLine _
    & " AND MAX_SEQ.PICK_SEQ   = MAIN.PICK_SEQ                                          " & vbNewLine _
    & " WHERE                                                                           " & vbNewLine _
    & "     MAIN.NRS_BR_CD  = @NRS_BR_CD                                                " & vbNewLine _
    & " AND MAIN.OUTKA_NO_L = @OUTKA_NO_L                                               " & vbNewLine

    ''' <summary>
    ''' 出荷ピック明細取得ORDER句
    ''' </summary>
    Private Const SQL_ORDER_TC_PICK_DTL As String _
    = "ORDER BY  NRS_BR_CD                  " & vbNewLine _
    & "         ,OUTKA_NO_L                 " & vbNewLine _
    & "         ,TOU_NO                     " & vbNewLine _
    & "         ,SITU_NO                    " & vbNewLine _
    & "         ,PICK_SEQ                   " & vbNewLine _
    & "         ,OUTKA_NO_M                 " & vbNewLine _
    & "         ,OUTKA_NO_S                 " & vbNewLine

#End Region

#Region "出荷検品ヘッダ取得"
    ''' <summary>
    ''' 出荷検品ヘッダ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TC_KENPIN_HEAD As String _
    = " SELECT                                                                          " & vbNewLine _
    & "     MAIN.NRS_BR_CD                           AS NRS_BR_CD                       " & vbNewLine _
    & "   , MAIN.OUTKA_NO_L                          AS OUTKA_NO_L                      " & vbNewLine _
    & "   , MAIN.KENPIN_ATTEND_SEQ                   AS KENPIN_ATTEND_SEQ               " & vbNewLine _
    & "   , MAIN.KENPIN_ATTEND_STATE_KB              AS KENPIN_ATTEND_STATE_KB          " & vbNewLine _
    & "   , MAIN.WORK_STATE_KB                       AS WORK_STATE_KB                   " & vbNewLine _
    & "   , MAIN.CANCEL_FLG                          AS CANCEL_FLG                      " & vbNewLine _
    & "   , CASE WHEN @PROC_TYPE = '00'  THEN '01'                                      " & vbNewLine _
    & "          WHEN @PROC_TYPE = '01'  THEN '02'                                      " & vbNewLine _
    & "          WHEN @PROC_TYPE = '02'  THEN '02'                                      " & vbNewLine _
    & "          ELSE ''                                                                " & vbNewLine _
    & "     END                                      AS CANCEL_TYPE                     " & vbNewLine _
    & "   , MAIN.WH_CD                               AS WH_CD                           " & vbNewLine _
    & "   , MAIN.CUST_CD_L                           AS CUST_CD_L                       " & vbNewLine _
    & "   , MAIN.CUST_CD_M                           AS CUST_CD_M                       " & vbNewLine _
    & "   , MAIN.CUST_NM_L                           AS CUST_NM_L                       " & vbNewLine _
    & "   , MAIN.CUST_NM_M                           AS CUST_NM_M                       " & vbNewLine _
    & "   , MAIN.UNSO_CD                             AS UNSO_CD                         " & vbNewLine _
    & "   , MAIN.UNSO_NM                             AS UNSO_NM                         " & vbNewLine _
    & "   , MAIN.UNSO_BR_CD                          AS UNSO_BR_CD                      " & vbNewLine _
    & "   , MAIN.UNSO_BR_NM                          AS UNSO_BR_NM                      " & vbNewLine _
    & "   , MAIN.DEST_CD                             AS DEST_CD                         " & vbNewLine _
    & "   , MAIN.DEST_NM                             AS DEST_NM                         " & vbNewLine _
    & "   , MAIN.DEST_AD_1                           AS DEST_AD_1                       " & vbNewLine _
    & "   , MAIN.DEST_AD_2                           AS DEST_AD_2                       " & vbNewLine _
    & "   , MAIN.DEST_AD_3                           AS DEST_AD_3                       " & vbNewLine _
    & "   , MAIN.OUTKO_DATE                          AS OUTKO_DATE                      " & vbNewLine _
    & "   , MAIN.OUTKA_PLAN_DATE                     AS OUTKA_PLAN_DATE                 " & vbNewLine _
    & "   , MAIN.ARR_PLAN_DATE                       AS ARR_PLAN_DATE                   " & vbNewLine _
    & "   , MAIN.ARR_PLAN_TIME                       AS ARR_PLAN_TIME                   " & vbNewLine _
    & "   , MAIN.REMARK                              AS REMARK                          " & vbNewLine _
    & "   , MAIN.REMARK_SIJI                         AS REMARK_SIJI                     " & vbNewLine _
    & "   , MAIN.CUST_ORD_NO                         AS CUST_ORD_NO                     " & vbNewLine _
    & "   , MAIN.BUYER_ORD_NO                        AS BUYER_ORD_NO                    " & vbNewLine _
    & "   , MAIN.OUTKA_PKG_NB                        AS OUTKA_PKG_NB                    " & vbNewLine _
    & "   , MAIN.OUTKA_L_PKG_NB                      AS OUTKA_L_PKG_NB                  " & vbNewLine _
    & "   , MAIN.OUTKA_TTL_NB                        AS OUTKA_TTL_NB                    " & vbNewLine _
    & "   , MAIN.OUTKA_TTL_WT                        AS OUTKA_TTL_WT                    " & vbNewLine _
    & "   , MAIN.ATTEND_FLG                          AS ATTEND_FLG                      " & vbNewLine _
    & "   , MAIN.URGENT_FLG                          AS URGENT_FLG                      " & vbNewLine _
    & "   , MAIN.NIHUDA_CHK_STATE_KB                 AS NIHUDA_CHK_STATE_KB             " & vbNewLine _
    & "   , MAIN.NIHUDA_CHK_FLG                      AS NIHUDA_CHK_FLG                  " & vbNewLine _
    & "   , MAIN.REMARK_KENPIN_CHK_FLG               AS REMARK_KENPIN_CHK_FLG           " & vbNewLine _
    & "   , MAIN.REMARK_ATTEND_CHK_FLG               AS REMARK_ATTEND_CHK_FLG           " & vbNewLine _
    & "   , MAIN.NIHUDA_FLG                          AS NIHUDA_FLG                      " & vbNewLine _
    & "   , MAIN.NIHUDA_YN                           AS NIHUDA_YN                       " & vbNewLine _
    & "   , MAIN.SYS_ENT_DATE                        AS SYS_ENT_DATE                    " & vbNewLine _
    & "   , MAIN.SYS_ENT_TIME                        AS SYS_ENT_TIME                    " & vbNewLine _
    & "   , MAIN.SYS_ENT_PGID                        AS SYS_ENT_PGID                    " & vbNewLine _
    & "   , MAIN.SYS_ENT_USER                        AS SYS_ENT_USER                    " & vbNewLine _
    & "   , MAIN.SYS_UPD_DATE                        AS SYS_UPD_DATE                    " & vbNewLine _
    & "   , MAIN.SYS_UPD_TIME                        AS SYS_UPD_TIME                    " & vbNewLine _
    & "   , MAIN.SYS_UPD_PGID                        AS SYS_UPD_PGID                    " & vbNewLine _
    & "   , MAIN.SYS_UPD_USER                        AS SYS_UPD_USER                    " & vbNewLine _
    & "   , MAIN.SYS_DEL_FLG                         AS SYS_DEL_FLG                     " & vbNewLine _
    & " FROM $LM_TRN$..TC_KENPIN_HEAD MAIN                                              " & vbNewLine _
    & " INNER JOIN (                                                                    " & vbNewLine _
    & "     SELECT                                                                      " & vbNewLine _
    & "         NRS_BR_CD                            AS NRS_BR_CD                       " & vbNewLine _
    & "       , OUTKA_NO_L                           AS OUTKA_NO_L                      " & vbNewLine _
    & "       , MAX(KENPIN_ATTEND_SEQ)               AS KENPIN_ATTEND_SEQ               " & vbNewLine _
    & "     FROM $LM_TRN$..TC_KENPIN_HEAD                                               " & vbNewLine _
    & "     GROUP BY                                                                    " & vbNewLine _
    & "         NRS_BR_CD                                                               " & vbNewLine _
    & "       , OUTKA_NO_L                                                              " & vbNewLine _
    & "     )MAX_SEQ                                                                    " & vbNewLine _
    & " ON  MAX_SEQ.NRS_BR_CD         = MAIN.NRS_BR_CD                                  " & vbNewLine _
    & " AND MAX_SEQ.OUTKA_NO_L        = MAIN.OUTKA_NO_L                                 " & vbNewLine _
    & " AND MAX_SEQ.KENPIN_ATTEND_SEQ = MAIN.KENPIN_ATTEND_SEQ                          " & vbNewLine _
    & "                                                                                 " & vbNewLine _
    & " WHERE                                                                           " & vbNewLine _
    & "     MAIN.NRS_BR_CD  = @NRS_BR_CD                                                " & vbNewLine _
    & " AND MAIN.OUTKA_NO_L = @OUTKA_NO_L                                               " & vbNewLine

#End Region

#Region "出荷ピックヘッダ登録用データ取得"
    Private Const SQL_SELECT_PICK_HEAD_DATA As String _
    = "SELECT                                                                           " & vbNewLine _
    & " OUTL.NRS_BR_CD                                         AS NRS_BR_CD             " & vbNewLine _
    & ",OUTL.OUTKA_NO_L                                        AS OUTKA_NO_L            " & vbNewLine _
    & ",OUTS.TOU_NO                                            AS TOU_NO                " & vbNewLine _
    & ",OUTS.SITU_NO                                           AS SITU_NO               " & vbNewLine _
    & ",ISNULL((SELECT MAX(PICK_SEQ)                                                    " & vbNewLine _
    & "         FROM $LM_TRN$..TC_PICK_HEAD                                             " & vbNewLine _
    & "         WHERE TC_PICK_HEAD.NRS_BR_CD  = OUTL.NRS_BR_CD                          " & vbNewLine _
    & "           AND TC_PICK_HEAD.OUTKA_NO_L = OUTL.OUTKA_NO_L                         " & vbNewLine _
    & "         GROUP BY TC_PICK_HEAD.NRS_BR_CD,TC_PICK_HEAD.OUTKA_NO_L                 " & vbNewLine _
    & "        ),0) + 1                                        AS PICK_SEQ              " & vbNewLine _
    & ",'00'                                                   AS CANCEL_FLG            " & vbNewLine _
    & ",'00'                                                   AS CANCEL_TYPE           " & vbNewLine _
    & ",TOUSITU.USER_CD                                        AS PICK_USER_CD          " & vbNewLine _
    & ",ISNULL(USR.USER_NM,'')                                 AS PICK_USER_NM          " & vbNewLine _
    & ",TOUSITU.USER_CD_SUB                                    AS PICK_USER_CD_SUB      " & vbNewLine _
    & ",ISNULL(USR_SUB.USER_NM,'')                             AS PICK_USER_NM_SUB      " & vbNewLine _
    & ",CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                " & vbNewLine _
    & "      WHEN TOUSITU.JISYATASYA_KB = '02' THEN '02'                                " & vbNewLine _
    & "      ELSE '00'                                                                  " & vbNewLine _
    & " END                                                    AS PICK_STATE_KB         " & vbNewLine _
    & ",'00'                                                   AS WORK_STATE_KB         " & vbNewLine _
    & ",OUTL.WH_CD                                             AS WH_CD                 " & vbNewLine _
    & ",OUTL.CUST_CD_L                                         AS CUST_CD_L             " & vbNewLine _
    & ",OUTL.CUST_CD_M                                         AS CUST_CD_M             " & vbNewLine _
    & ",CUST.CUST_NM_L                                         AS CUST_NM_L             " & vbNewLine _
    & ",CUST.CUST_NM_M                                         AS CUST_NM_M             " & vbNewLine _
    & ",ISNULL(UNSO.UNSO_CD,'')                                AS UNSO_CD               " & vbNewLine _
    & ",ISNULL(UNSC.UNSOCO_NM,'')                              AS UNSO_NM               " & vbNewLine _
    & ",ISNULL(UNSO.UNSO_BR_CD,'')                             AS UNSO_BR_CD            " & vbNewLine _
    & ",ISNULL(UNSC.UNSOCO_BR_NM,'')                           AS UNSO_BR_NM            " & vbNewLine _
    & ",CASE WHEN OUTL.DEST_KB ='02' THEN ISNULL(EDIL.DEST_CD,'')                       " & vbNewLine _
    & "      ELSE OUTL.DEST_CD                                                          " & vbNewLine _
    & " END                                                    AS DEST_CD               " & vbNewLine _
    & ",CASE WHEN OUTL.DEST_KB ='02' THEN ISNULL(EDIL.DEST_NM,'')                       " & vbNewLine _
    & "      ELSE OUTL.DEST_NM                                                          " & vbNewLine _
    & " END                                                    AS DEST_NM               " & vbNewLine _
    & ",CASE WHEN OUTL.DEST_KB ='01' THEN OUTL.DEST_AD_1                                " & vbNewLine _
    & "      WHEN OUTL.DEST_KB ='02' THEN ISNULL(EDIL.DEST_AD_1,'')                     " & vbNewLine _
    & "      ELSE ISNULL(DEST.AD_1,'')                                                  " & vbNewLine _
    & " END                                                    AS DEST_AD_1             " & vbNewLine _
    & ",CASE WHEN OUTL.DEST_KB ='01' THEN OUTL.DEST_AD_2                                " & vbNewLine _
    & "      WHEN OUTL.DEST_KB ='02' THEN ISNULL(EDIL.DEST_AD_2,'')                     " & vbNewLine _
    & "      ELSE ISNULL(DEST.AD_2,'')                                                  " & vbNewLine _
    & " END                                                    AS DEST_AD_2             " & vbNewLine _
    & ",OUTL.DEST_AD_3                                         AS DEST_AD_3             " & vbNewLine _
    & ",OUTL.DEST_TEL                                          AS DEST_TEL              " & vbNewLine _
    & ",OUTL.OUTKO_DATE                                        AS OUTKO_DATE            " & vbNewLine _
    & ",OUTL.OUTKA_PLAN_DATE                                   AS OUTKA_PLAN_DATE       " & vbNewLine _
    & ",OUTL.ARR_PLAN_DATE                                     AS ARR_PLAN_DATE         " & vbNewLine _
    & ",OUTL.ARR_PLAN_TIME                                     AS ARR_PLAN_TIME         " & vbNewLine _
    & ",OUTL.REMARK                                            AS REMARK                " & vbNewLine _
    & ",OUTL.WH_SIJI_REMARK                                    AS REMARK_SIJI           " & vbNewLine _
    & ",OUTL.CUST_ORD_NO                                       AS CUST_ORD_NO           " & vbNewLine _
    & ",OUTL.BUYER_ORD_NO                                      AS BUYER_ORD_NO          " & vbNewLine _
    & ",OUTL.OUTKA_PKG_NB                                      AS OUTKA_PKG_NB          " & vbNewLine _
    & ",OUTM.OUTKA_TTL_NB                                      AS OUTKA_TTL_NB          " & vbNewLine _
    & ",OUTM.OUTKA_TTL_QT                                      AS OUTKA_TTL_WT          " & vbNewLine _
    & ",OUTL.URGENT_YN                                         AS URGENT_FLG            " & vbNewLine _
    & ",CASE WHEN LEN(OUTL.REMARK + OUTL.WH_SIJI_REMARK) > 0 THEN '00' ELSE '01' END AS REMARK_CHK_FLG        " & vbNewLine _
    & ",ISNULL(TOUSITU.JISYATASYA_KB,'')                       AS JISYATASYA_KB         " & vbNewLine _
    & ",'0'                                                    AS SYS_DEL_FLG           " & vbNewLine _
    & ",OUTL.WH_TAB_HOKOKU_YN                                  AS HOKOKU_FLG            " & vbNewLine _
    & ",'00'                                                   AS HOKOKU_STATE_KB       " & vbNewLine _
    & ",'00'                                                   AS SAGYO_FILE_STATE_KB   " & vbNewLine _
    & "FROM $LM_TRN$..C_OUTKA_L OUTL                                                    " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_CUST CUST                                                  " & vbNewLine _
    & "ON  CUST.NRS_BR_CD  = OUTL.NRS_BR_CD                                             " & vbNewLine _
    & "AND CUST.CUST_CD_L  = OUTL.CUST_CD_L                                             " & vbNewLine _
    & "AND CUST.CUST_CD_M  = OUTL.CUST_CD_M                                             " & vbNewLine _
    & "AND CUST.CUST_CD_S  = '00'                                                       " & vbNewLine _
    & "AND CUST.CUST_CD_SS = '00'                                                       " & vbNewLine _
    & "INNER JOIN                                                                       " & vbNewLine _
    & "    (                                                                            " & vbNewLine _
    & "        SELECT                                                                   " & vbNewLine _
    & "         C_OUTKA_M.NRS_BR_CD                                                     " & vbNewLine _
    & "        ,C_OUTKA_M.OUTKA_NO_L                                                    " & vbNewLine _
    & "        ,SUM(C_OUTKA_M.OUTKA_TTL_NB) AS OUTKA_TTL_NB                             " & vbNewLine _
    & "        ,SUM(C_OUTKA_M.OUTKA_TTL_QT) AS OUTKA_TTL_QT                             " & vbNewLine _
    & "        ,C_OUTKA_M.SYS_DEL_FLG                                                   " & vbNewLine _
    & "        FROM                                                                     " & vbNewLine _
    & "        $LM_TRN$..C_OUTKA_M     C_OUTKA_M                                        " & vbNewLine _
    & "        WHERE                                                                    " & vbNewLine _
    & "        C_OUTKA_M.NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
    & "        AND                                                                      " & vbNewLine _
    & "        C_OUTKA_M.SYS_DEL_FLG = '0'                                              " & vbNewLine _
    & "        GROUP BY                                                                 " & vbNewLine _
    & "         C_OUTKA_M.NRS_BR_CD                                                     " & vbNewLine _
    & "        ,C_OUTKA_M.OUTKA_NO_L                                                    " & vbNewLine _
    & "        ,C_OUTKA_M.SYS_DEL_FLG                                                   " & vbNewLine _
    & "    ) OUTM                                                                       " & vbNewLine _
    & "ON     OUTM.NRS_BR_CD  = OUTL.NRS_BR_CD                                          " & vbNewLine _
    & "AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                            " & vbNewLine _
    & "LEFT JOIN                                                                        " & vbNewLine _
    & "    (                                                                            " & vbNewLine _
    & "    SELECT                                                                       " & vbNewLine _
    & "        NRS_BR_CD                                                                " & vbNewLine _
    & "        ,OUTKA_NO_L                                                              " & vbNewLine _
    & "        ,TOU_NO                                                                  " & vbNewLine _
    & "        ,SITU_NO                                                                 " & vbNewLine _
    & "    FROM                                                                         " & vbNewLine _
    & "        $LM_TRN$..C_OUTKA_S C_OUTKA_S                                            " & vbNewLine _
    & "    WHERE                                                                        " & vbNewLine _
    & "        C_OUTKA_S.SYS_DEL_FLG = '0'                                              " & vbNewLine _
    & "    GROUP BY                                                                     " & vbNewLine _
    & "        NRS_BR_CD                                                                " & vbNewLine _
    & "        ,OUTKA_NO_L                                                              " & vbNewLine _
    & "        ,TOU_NO                                                                  " & vbNewLine _
    & "        ,SITU_NO                                                                 " & vbNewLine _
    & "    ) OUTS                                                                       " & vbNewLine _
    & "ON  OUTS.NRS_BR_CD  = OUTL.NRS_BR_CD                                             " & vbNewLine _
    & "AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                            " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_DEST DEST                                                  " & vbNewLine _
    & "ON  DEST.NRS_BR_CD = OUTL.NRS_BR_CD                                              " & vbNewLine _
    & "AND DEST.CUST_CD_L = OUTL.CUST_CD_L                                              " & vbNewLine _
    & "AND DEST.DEST_CD   = OUTL.DEST_CD                                                " & vbNewLine _
    & "                                                                                 " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..F_UNSO_L UNSO                                                " & vbNewLine _
    & "ON  UNSO.NRS_BR_CD    = OUTL.NRS_BR_CD                                           " & vbNewLine _
    & "AND UNSO.INOUTKA_NO_L = OUTL.OUTKA_NO_L                                          " & vbNewLine _
    & "AND UNSO.MOTO_DATA_KB = '20'                                                     " & vbNewLine _
    & "AND UNSO.SYS_DEL_FLG  = '0'                                                      " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_UNSOCO UNSC                                                " & vbNewLine _
    & "ON  UNSC.NRS_BR_CD    = OUTL.NRS_BR_CD                                           " & vbNewLine _
    & "AND UNSC.UNSOCO_CD    = UNSO.UNSO_CD                                             " & vbNewLine _
    & "AND UNSC.UNSOCO_BR_CD = UNSO.UNSO_BR_CD                                          " & vbNewLine _
    & "LEFT JOIN                                                                        " & vbNewLine _
    & "(                                                                                " & vbNewLine _
    & "     SELECT                                                                      " & vbNewLine _
    & "            NRS_BR_CD                                                            " & vbNewLine _
    & "          , EDI_CTL_NO                                                           " & vbNewLine _
    & "          , OUTKA_CTL_NO                                                         " & vbNewLine _
    & "      FROM (                                                                     " & vbNewLine _
    & "             SELECT                                                              " & vbNewLine _
    & "                    EDIOUTL.NRS_BR_CD                                            " & vbNewLine _
    & "                  , EDIOUTL.EDI_CTL_NO                                           " & vbNewLine _
    & "                  , EDIOUTL.OUTKA_CTL_NO                                         " & vbNewLine _
    & "                  , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                   " & vbNewLine _
    & "                    ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD       " & vbNewLine _
    & "                                                       , EDIOUTL.OUTKA_CTL_NO    " & vbNewLine _
    & "                                                ORDER BY EDIOUTL.NRS_BR_CD       " & vbNewLine _
    & "                                                       , EDIOUTL.EDI_CTL_NO      " & vbNewLine _
    & "                                           )                                     " & vbNewLine _
    & "                    END AS IDX                                                   " & vbNewLine _
    & "              FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                                " & vbNewLine _
    & "             WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                    " & vbNewLine _
    & "               AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                             " & vbNewLine _
    & "               AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L                            " & vbNewLine _
    & "           ) EBASE                                                               " & vbNewLine _
    & "     WHERE EBASE.IDX = 1                                                         " & vbNewLine _
    & ") TOPEDI                                                                         " & vbNewLine _
    & "ON  TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                         " & vbNewLine _
    & "AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                        " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                            " & vbNewLine _
    & "ON  EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                           " & vbNewLine _
    & "AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                          " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_TOU_SITU TOUSITU                                           " & vbNewLine _
    & "ON  TOUSITU.WH_CD   = OUTL.WH_CD                                                 " & vbNewLine _
    & "AND TOUSITU.TOU_NO  = OUTS.TOU_NO                                                " & vbNewLine _
    & "AND TOUSITU.SITU_NO = OUTS.SITU_NO                                               " & vbNewLine _
    & "LEFT JOIN $LM_MST$..S_USER USR                                                   " & vbNewLine _
    & "ON USR.USER_CD = TOUSITU.USER_CD                                                 " & vbNewLine _
    & "LEFT JOIN $LM_MST$..S_USER USR_SUB                                               " & vbNewLine _
    & "ON USR_SUB.USER_CD = TOUSITU.USER_CD_SUB                                         " & vbNewLine _
    & "WHERE                                                                            " & vbNewLine _
    & "    OUTL.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
    & "AND OUTL.NRS_BR_CD   = @NRS_BR_CD                                                " & vbNewLine _
    & "AND OUTL.OUTKA_NO_L  = @OUTKA_NO_L                                               " & vbNewLine

#End Region

#Region "出荷ピック明細登録用データ取得"

    ''' <summary>
    ''' 出荷ピック明細登録用データ取得
    ''' </summary>
    ''' <remarks>SQL_INSERT_TC_PICK_DTL(出荷ピック明細登録)のSELECT句と同じ(システム共通項目を除く)</remarks>
    Private Const SQL_SELECT_LMS_PICK_DTL_DATA As String _
    = "SELECT                                                                                    " & vbNewLine _
    & " OUTS.NRS_BR_CD                                             AS NRS_BR_CD                  " & vbNewLine _
    & ",OUTS.OUTKA_NO_L                                            AS OUTKA_NO_L                 " & vbNewLine _
    & ",OUTS.TOU_NO                                                AS TOU_NO                     " & vbNewLine _
    & ",OUTS.SITU_NO                                               AS SITU_NO                    " & vbNewLine _
    & ",(SELECT MAX(PICK_SEQ)                                                                    " & vbNewLine _
    & "        FROM $LM_TRN$..TC_PICK_HEAD                                                       " & vbNewLine _
    & "        WHERE TC_PICK_HEAD.NRS_BR_CD  = OUTL.NRS_BR_CD                                    " & vbNewLine _
    & "          AND TC_PICK_HEAD.OUTKA_NO_L = OUTL.OUTKA_NO_L                                   " & vbNewLine _
    & "        GROUP BY TC_PICK_HEAD.NRS_BR_CD,TC_PICK_HEAD.OUTKA_NO_L                           " & vbNewLine _
    & "       )                                                    AS PICK_SEQ                   " & vbNewLine _
    & ",OUTS.OUTKA_NO_M                                            AS OUTKA_NO_M                 " & vbNewLine _
    & ",OUTS.OUTKA_NO_S                                            AS OUTKA_NO_S                 " & vbNewLine _
    & ",CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                         " & vbNewLine _
    & "      WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                         " & vbNewLine _
    & "      ELSE '00'                                                                           " & vbNewLine _
    & " END                                                        AS PICK_STATE_KB              " & vbNewLine _
    & ",GDS.GOODS_CD_NRS                                           AS GOODS_CD_NRS               " & vbNewLine _
    & ",GDS.GOODS_CD_CUST                                          AS GOODS_CD_CUST              " & vbNewLine _
    & ",ISNULL(GDSDTL74.SET_NAIYO, GDS.GOODS_NM_1)                 AS GOODS_NM_NRS               " & vbNewLine _
    & ",OUTS.LOT_NO                                                AS LOT_NO                     " & vbNewLine _
    & ",OUTM.IRIME                                                 AS IRIME                      " & vbNewLine _
    & ",OUTM.IRIME_UT                                              AS IRIME_UT                   " & vbNewLine _
    & ",GDS.NB_UT                                                  AS NB_UT                      " & vbNewLine _
    & ",GDS.PKG_NB                                                 AS PKG_NB                     " & vbNewLine _
    & ",GDS.PKG_UT                                                 AS PKG_UT                     " & vbNewLine _
    & ",OUTS.ZONE_CD                                               AS ZONE_CD                    " & vbNewLine _
    & ",OUTS.LOCA                                                  AS LOCA                       " & vbNewLine _
    & ",OUTS.ALCTD_NB                                              AS OUT_NB                     " & vbNewLine _
    & ",CONVERT(NUMERIC(10,0),CONVERT(int,OUTS.ALCTD_NB) / CONVERT(int,GDS.PKG_NB)) AS OUT_KONSU " & vbNewLine _
    & ",OUTS.ALCTD_NB % GDS.PKG_NB                                 AS OUT_HASU                   " & vbNewLine _
    & ",OUTS.ALCTD_CAN_NB                                          AS OUT_ZAN_NB                 " & vbNewLine _
    & ",CONVERT(NUMERIC(10,0),CONVERT(int,OUTS.ALCTD_CAN_NB) / CONVERT(int,GDS.PKG_NB)) AS OUT_ZAN_KONSU " & vbNewLine _
    & ",OUTS.ALCTD_CAN_NB % GDS.PKG_NB                             AS OUT_ZAN_HASU               " & vbNewLine _
    & ",OUTS.ALCTD_QT                                              AS OUT_QT                     " & vbNewLine _
    & ",ZAI.ALLOC_CAN_QT                                           AS OUT_ZAN_QT                 " & vbNewLine _
    & ",ZAI.GOODS_COND_KB_1                                        AS GOODS_COND_KB1             " & vbNewLine _
    & ",ZAI.GOODS_COND_KB_2                                        AS GOODS_COND_KB2             " & vbNewLine _
    & ",ZAI.GOODS_COND_KB_3                                        AS GOODS_COND_KB3             " & vbNewLine _
    & ",ZAI.LT_DATE                                                AS LT_DATE                    " & vbNewLine _
    & ",ZAI.GOODS_CRT_DATE                                         AS GOODS_CRT_DATE             " & vbNewLine _
    & ",ZAI.SPD_KB                                                 AS SPD_KB                     " & vbNewLine _
    & ",ZAI.OFB_KB                                                 AS OFB_KB                     " & vbNewLine _
    & ",ZAI.RSV_NO                                                 AS RSV_NO                     " & vbNewLine _
    & ",ZAI.SERIAL_NO                                              AS SERIAL_NO                  " & vbNewLine _
    & ",OUTS.INKA_NO_L                                             AS INKA_NO_L                  " & vbNewLine _
    & ",OUTS.INKA_NO_M                                             AS INKA_NO_M                  " & vbNewLine _
    & ",OUTS.INKA_NO_S                                             AS INKA_NO_S                  " & vbNewLine _
    & ",OUTS.ZAI_REC_NO                                            AS ZAI_REC_NO                 " & vbNewLine _
    & ",CASE WHEN BINL.INKA_STATE_KB < '50' THEN ISNULL(BINL.INKA_DATE, '')                      " & vbNewLine _
    & "      ELSE ISNULL(ZAI.INKO_DATE, '')                                                      " & vbNewLine _
    & " END                                                        AS INKA_DATE                  " & vbNewLine _
    & ",ZAI.REMARK                                                 AS REMARK                     " & vbNewLine _
    & ",ZAI.REMARK_OUT                                             AS REMARK_OUT                 " & vbNewLine _
    & ",ISNULL(TOUSITU.JISYATASYA_KB,'')                           AS JISYATASYA_KB              " & vbNewLine _
    & "FROM $LM_TRN$..C_OUTKA_L OUTL                                                             " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                                        " & vbNewLine _
    & "ON  OUTM.NRS_BR_CD   = OUTL.NRS_BR_CD                                                     " & vbNewLine _
    & "AND OUTM.OUTKA_NO_L  = OUTL.OUTKA_NO_L                                                    " & vbNewLine _
    & "AND OUTM.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                                        " & vbNewLine _
    & "ON  OUTS.NRS_BR_CD  = OUTM.NRS_BR_CD                                                      " & vbNewLine _
    & "AND OUTS.OUTKA_NO_L = OUTM.OUTKA_NO_L                                                     " & vbNewLine _
    & "AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                     " & vbNewLine _
    & "AND OUTS.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_GOODS GDS                                                           " & vbNewLine _
    & "ON  GDS.NRS_BR_CD    = OUTM.NRS_BR_CD                                                     " & vbNewLine _
    & "AND GDS.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                                  " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GDSDTL74                                              " & vbNewLine _
    & "ON  GDSDTL74.NRS_BR_CD    = GDS.NRS_BR_CD                                                 " & vbNewLine _
    & "AND GDSDTL74.GOODS_CD_NRS = GDS.GOODS_CD_NRS                                              " & vbNewLine _
    & "AND GDSDTL74.SUB_KB = '74'                                                                " & vbNewLine _
    & "AND GDSDTL74.SET_NAIYO <> ''                                                              " & vbNewLine _
    & "AND GDSDTL74.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                                         " & vbNewLine _
    & "ON  ZAI.NRS_BR_CD  = OUTS.NRS_BR_CD                                                       " & vbNewLine _
    & "AND ZAI.ZAI_REC_NO = OUTS.ZAI_REC_NO                                                      " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..B_INKA_L BINL                                                         " & vbNewLine _
    & "ON   BINL.NRS_BR_CD = OUTS.NRS_BR_CD                                                      " & vbNewLine _
    & "AND  BINL.INKA_NO_L = OUTS.INKA_NO_L                                                      " & vbNewLine _
    & "AND  BINL.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_TOU_SITU TOUSITU                                                    " & vbNewLine _
    & "ON  TOUSITU.WH_CD   = OUTL.WH_CD                                                          " & vbNewLine _
    & "AND TOUSITU.TOU_NO  = OUTS.TOU_NO                                                         " & vbNewLine _
    & "AND TOUSITU.SITU_NO = OUTS.SITU_NO                                                        " & vbNewLine _
    & "WHERE                                                                                     " & vbNewLine _
    & "    OUTL.SYS_DEL_FLG = '0'                                                            	 " & vbNewLine _
    & "AND OUTL.NRS_BR_CD  = @NRS_BR_CD                                                          " & vbNewLine _
    & "AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                                         " & vbNewLine

    ''' <summary>
    ''' 出荷ピック明細登録用データ取得ORDER句
    ''' </summary>
    Private Const SQL_ORDER_LMS_PICK_DTL_DATA As String _
    = "ORDER BY  NRS_BR_CD                  " & vbNewLine _
    & "         ,OUTKA_NO_L                 " & vbNewLine _
    & "         ,TOU_NO                     " & vbNewLine _
    & "         ,SITU_NO                    " & vbNewLine _
    & "         ,PICK_SEQ                   " & vbNewLine _
    & "         ,OUTKA_NO_M                 " & vbNewLine _
    & "         ,OUTKA_NO_S                 " & vbNewLine

#End Region

#Region "出荷検品明細登録用データ取得"

    ''' <summary>
    ''' 出荷検品明細登録用データ取得
    ''' </summary>
    ''' <remarks>SQL_INSERT_TC_KENPIN_DTL(出荷検品明細登録)のSELECT句と同じ(システム共通項目を除く)</remarks>
    Private Const SQL_SELECT_LMS_KENPIN_DTL_DATA As String _
    = "SELECT                                                                           " & vbNewLine _
    & " OUTS.NRS_BR_CD                                             AS NRS_BR_CD         " & vbNewLine _
    & ",OUTS.OUTKA_NO_L                                            AS OUTKA_NO_L        " & vbNewLine _
    & ",(SELECT MAX(KENPIN_ATTEND_SEQ)                                                  " & vbNewLine _
    & "         FROM $LM_TRN$..TC_KENPIN_HEAD                                           " & vbNewLine _
    & "         WHERE TC_KENPIN_HEAD.NRS_BR_CD  = OUTL.NRS_BR_CD                        " & vbNewLine _
    & "           AND TC_KENPIN_HEAD.OUTKA_NO_L = OUTL.OUTKA_NO_L                       " & vbNewLine _
    & "         GROUP BY TC_KENPIN_HEAD.NRS_BR_CD,TC_KENPIN_HEAD.OUTKA_NO_L             " & vbNewLine _
    & "         )                                                 AS KENPIN_ATTEND_SEQ  " & vbNewLine _
    & ",OUTS.OUTKA_NO_M                                           AS OUTKA_NO_M         " & vbNewLine _
    & ",OUTS.OUTKA_NO_S                                           AS OUTKA_NO_S         " & vbNewLine _
    & ",1                                                         AS KENPIN_DTL_SEQ     " & vbNewLine _
    & ",CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                " & vbNewLine _
    & "      WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                " & vbNewLine _
    & "      ELSE '00'                                                                  " & vbNewLine _
    & " END                                                       AS KENPIN_STATE_KB    " & vbNewLine _
    & ",CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                " & vbNewLine _
    & "      WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                " & vbNewLine _
    & "      ELSE '00'                                                                  " & vbNewLine _
    & " END                                                       AS ATTEND_STATE_KB    " & vbNewLine _
    & ",GDS.GOODS_CD_NRS                                          AS GOODS_CD_NRS       " & vbNewLine _
    & ",GDS.GOODS_CD_CUST                                         AS GOODS_CD_CUST      " & vbNewLine _
    & ",ISNULL(GDSDTL74.SET_NAIYO, GDS.GOODS_NM_1)                AS GOODS_NM_NRS       " & vbNewLine _
    & ",OUTS.LOT_NO                                               AS LOT_NO             " & vbNewLine _
    & ",OUTM.IRIME                                                AS IRIME              " & vbNewLine _
    & ",OUTM.IRIME_UT                                             AS IRIME_UT           " & vbNewLine _
    & ",GDS.NB_UT                                                 AS NB_UT              " & vbNewLine _
    & ",GDS.PKG_NB                                                AS PKG_NB             " & vbNewLine _
    & ",GDS.PKG_UT                                                AS PKG_UT             " & vbNewLine _
    & ",OUTS.TOU_NO                                               AS TOU_NO             " & vbNewLine _
    & ",OUTS.SITU_NO                                              AS SITU_NO            " & vbNewLine _
    & ",OUTS.ZONE_CD                                              AS ZONE_CD            " & vbNewLine _
    & ",OUTS.LOCA                                                 AS LOCA               " & vbNewLine _
    & ",OUTS.ALCTD_NB                                             AS OUT_NB             " & vbNewLine _
    & ",CONVERT(NUMERIC(10,0),CONVERT(int,OUTS.ALCTD_NB) / CONVERT(int,GDS.PKG_NB)) AS OUT_KONSU " & vbNewLine _
    & ",OUTS.ALCTD_NB % GDS.PKG_NB                                AS OUT_HASU           " & vbNewLine _
    & ",OUTS.ALCTD_CAN_NB                                         AS OUT_ZAN_NB         " & vbNewLine _
    & ",CONVERT(NUMERIC(10,0),CONVERT(int,OUTS.ALCTD_CAN_NB) / CONVERT(int,GDS.PKG_NB)) AS OUT_ZAN_KONSU " & vbNewLine _
    & ",OUTS.ALCTD_CAN_NB % GDS.PKG_NB                            AS OUT_ZAN_HASU       " & vbNewLine _
    & ",OUTS.ALCTD_QT                                             AS OUT_QT             " & vbNewLine _
    & ",ISNULL(ZAI.ALLOC_CAN_QT ,0)                               AS OUT_ZAN_QT         " & vbNewLine _
    & ",ISNULL(ZAI.GOODS_COND_KB_1,'')                            AS GOODS_COND_KB1     " & vbNewLine _
    & ",ISNULL(ZAI.GOODS_COND_KB_2 ,'')                           AS GOODS_COND_KB2     " & vbNewLine _
    & ",ISNULL(ZAI.GOODS_COND_KB_3 ,'')                           AS GOODS_COND_KB3     " & vbNewLine _
    & ",ISNULL(ZAI.LT_DATE ,'')                                   AS LT_DATE            " & vbNewLine _
    & ",ISNULL(ZAI.GOODS_CRT_DATE,'')                             AS GOODS_CRT_DATE     " & vbNewLine _
    & ",ISNULL(ZAI.SPD_KB,'')                                     AS SPD_KB             " & vbNewLine _
    & ",ISNULL(ZAI.OFB_KB,'')                                     AS OFB_KB             " & vbNewLine _
    & ",ISNULL(ZAI.RSV_NO,'')                                     AS RSV_NO             " & vbNewLine _
    & ",ISNULL(ZAI.SERIAL_NO,'')                                  AS SERIAL_NO          " & vbNewLine _
    & ",OUTS.INKA_NO_L                                            AS INKA_NO_L          " & vbNewLine _
    & ",OUTS.INKA_NO_M                                            AS INKA_NO_M          " & vbNewLine _
    & ",OUTS.INKA_NO_S                                            AS INKA_NO_S          " & vbNewLine _
    & ",OUTS.ZAI_REC_NO                                           AS ZAI_REC_NO         " & vbNewLine _
    & ",CASE WHEN BINL.INKA_STATE_KB < '50' THEN ISNULL(BINL.INKA_DATE, '')             " & vbNewLine _
    & "      ELSE ISNULL(ZAI.INKO_DATE, '')                                             " & vbNewLine _
    & " END                                                       AS INKA_DATE          " & vbNewLine _
    & ",ISNULL(ZAI.REMARK,'')                                     AS REMARK             " & vbNewLine _
    & ",ISNULL(ZAI.REMARK_OUT,'')                                 AS REMARK_OUT         " & vbNewLine _
    & ",ISNULL(TOUSITU.JISYATASYA_KB,'')                          AS JISYATASYA_KB      " & vbNewLine _
    & "FROM $LM_TRN$..C_OUTKA_L OUTL                                                    " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                               " & vbNewLine _
    & "ON  OUTM.NRS_BR_CD  = OUTL.NRS_BR_CD                                             " & vbNewLine _
    & "AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                            " & vbNewLine _
    & "AND OUTM.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                               " & vbNewLine _
    & "ON  OUTS.NRS_BR_CD  = OUTM.NRS_BR_CD                                             " & vbNewLine _
    & "AND OUTS.OUTKA_NO_L = OUTM.OUTKA_NO_L                                            " & vbNewLine _
    & "AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                            " & vbNewLine _
    & "AND OUTS.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_GOODS GDS                                                  " & vbNewLine _
    & "ON  GDS.NRS_BR_CD    = OUTM.NRS_BR_CD                                            " & vbNewLine _
    & "AND GDS.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                         " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GDSDTL74                                     " & vbNewLine _
    & "ON  GDSDTL74.NRS_BR_CD    = GDS.NRS_BR_CD                                        " & vbNewLine _
    & "AND GDSDTL74.GOODS_CD_NRS = GDS.GOODS_CD_NRS                                     " & vbNewLine _
    & "AND GDSDTL74.SUB_KB = '74'                                                       " & vbNewLine _
    & "AND GDSDTL74.SET_NAIYO <> ''                                                     " & vbNewLine _
    & "AND GDSDTL74.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                                " & vbNewLine _
    & "ON  ZAI.NRS_BR_CD  = OUTS.NRS_BR_CD                                              " & vbNewLine _
    & "AND ZAI.ZAI_REC_NO = OUTS.ZAI_REC_NO                                             " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..B_INKA_L BINL                                                " & vbNewLine _
    & "ON   BINL.NRS_BR_CD = OUTS.NRS_BR_CD                                             " & vbNewLine _
    & "AND  BINL.INKA_NO_L = OUTS.INKA_NO_L                                             " & vbNewLine _
    & "AND  BINL.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_TOU_SITU TOUSITU                                           " & vbNewLine _
    & "ON  TOUSITU.WH_CD   = OUTL.WH_CD                                                 " & vbNewLine _
    & "AND TOUSITU.TOU_NO  = OUTS.TOU_NO                                                " & vbNewLine _
    & "AND TOUSITU.SITU_NO = OUTS.SITU_NO                                               " & vbNewLine _
    & "WHERE                                                                            " & vbNewLine _
    & "    OUTL.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
    & "AND OUTL.NRS_BR_CD  = @NRS_BR_CD                                                 " & vbNewLine _
    & "AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                                " & vbNewLine

    ''' <summary>
    ''' 出荷検品明細登録用データ取得ORDER句
    ''' </summary>
    Private Const SQL_ORDER_LMS_KENPIN_DTL_DATA As String _
    = "ORDER BY  NRS_BR_CD                  " & vbNewLine _
    & "         ,OUTKA_NO_L                 " & vbNewLine _
    & "         ,KENPIN_ATTEND_SEQ          " & vbNewLine _
    & "         ,OUTKA_NO_M                 " & vbNewLine _
    & "         ,OUTKA_NO_S                 " & vbNewLine _
    & "         ,KENPIN_DTL_SEQ             " & vbNewLine _

#End Region

#Region "作業取得"

    ''' <summary>
    ''' 作業取得
    ''' </summary>
    Private Const SQL_SELECT_TC_SAGYO As String _
    = "SELECT                                                                           " & vbNewLine _
    & "   MAIN.NRS_BR_CD                                                                " & vbNewLine _
    & " , MAIN.SAGYO_REC_NO                                                             " & vbNewLine _
    & " , MAIN.OUTKA_NO_L                                                               " & vbNewLine _
    & " , MAIN.OUTKA_NO_M                                                               " & vbNewLine _
    & " , MAIN.OUTKA_NO_S                                                               " & vbNewLine _
    & " , MAIN.WORK_SEQ                                                                 " & vbNewLine _
    & " , MAIN.SAGYO_STATE1_KB                                                          " & vbNewLine _
    & " , MAIN.SAGYO_STATE2_KB                                                          " & vbNewLine _
    & " , MAIN.SAGYO_STATE3_KB                                                          " & vbNewLine _
    & " , MAIN.WH_CD                                                                    " & vbNewLine _
    & " , MAIN.GOODS_CD_NRS                                                             " & vbNewLine _
    & " , MAIN.GOODS_NM_NRS                                                             " & vbNewLine _
    & " , MAIN.IRIME                                                                    " & vbNewLine _
    & " , MAIN.IRIME_UT                                                                 " & vbNewLine _
    & " , MAIN.PKG_NB                                                                   " & vbNewLine _
    & " , MAIN.PKG_UT                                                                   " & vbNewLine _
    & " , MAIN.LOT_NO                                                                   " & vbNewLine _
    & " , MAIN.TOU_NO                                                                   " & vbNewLine _
    & " , MAIN.SITU_NO                                                                  " & vbNewLine _
    & " , MAIN.ZONE_CD                                                                  " & vbNewLine _
    & " , MAIN.LOCA                                                                     " & vbNewLine _
    & " , MAIN.SAGYO_CD                                                                 " & vbNewLine _
    & " , MAIN.SAGYO_NM                                                                 " & vbNewLine _
    & " , MAIN.INV_TANI                                                                 " & vbNewLine _
    & " , MAIN.KOSU_BAI                                                                 " & vbNewLine _
    & " , MAIN.SAGYO_NB                                                                 " & vbNewLine _
    & " , MAIN.REMARK                                                                   " & vbNewLine _
    & " , MAIN.JISYATASYA_KB                                                            " & vbNewLine _
    & " , MAIN.IOZS_KB                                                                  " & vbNewLine _
    & " , MAIN.SYS_UPD_DATE                                                             " & vbNewLine _
    & " , MAIN.SYS_UPD_TIME                                                             " & vbNewLine _
    & " , MAIN.SYS_UPD_PGID                                                             " & vbNewLine _
    & " , MAIN.SYS_UPD_USER                                                             " & vbNewLine _
    & " , MAIN.SYS_DEL_FLG                                                              " & vbNewLine _
    & " FROM $LM_TRN$..TC_SAGYO  MAIN                                                   " & vbNewLine _
    & " INNER JOIN (                                                                    " & vbNewLine _
    & "     SELECT                                                                      " & vbNewLine _
    & "         NRS_BR_CD                                                               " & vbNewLine _
    & "        ,OUTKA_NO_L                                                              " & vbNewLine _
    & "        ,MAX(WORK_SEQ) AS WORK_SEQ                                               " & vbNewLine _
    & "     FROM $LM_TRN$..TC_SAGYO                                                     " & vbNewLine _
    & "     GROUP BY                                                                    " & vbNewLine _
    & "         NRS_BR_CD                                                               " & vbNewLine _
    & "        ,OUTKA_NO_L                                                              " & vbNewLine _
    & "     )MAX_SEQ                                                                    " & vbNewLine _
    & " ON  MAX_SEQ.NRS_BR_CD  = MAIN.NRS_BR_CD                                         " & vbNewLine _
    & " AND MAX_SEQ.OUTKA_NO_L = MAIN.OUTKA_NO_L                                        " & vbNewLine _
    & " AND MAX_SEQ.WORK_SEQ   = MAIN.WORK_SEQ                                          " & vbNewLine _
    & " WHERE                                                                           " & vbNewLine _
    & "     MAIN.NRS_BR_CD  = @NRS_BR_CD                                                " & vbNewLine _
    & " AND MAIN.OUTKA_NO_L = @OUTKA_NO_L                                               " & vbNewLine

    ''' <summary>
    '''作業取得ORDER句
    ''' </summary>
    Private Const SQL_ORDER_TC_SAGYO As String _
    = "ORDER BY  NRS_BR_CD                  " & vbNewLine _
    & "         ,SAGYO_REC_NO               " & vbNewLine _
    & "         ,OUTKA_NO_L                 " & vbNewLine _
    & "         ,OUTKA_NO_M                 " & vbNewLine _
    & "         ,OUTKA_NO_S                 " & vbNewLine _
    & "         ,WORK_SEQ                   " & vbNewLine _

#End Region

#Region "出荷作業登録用データ取得"
    Private Const SQL_SELECT_SAGYO_DATA As String _
    = " --大 KOSU_BAI = 01                                                                            " & vbNewLine _
    & "  SELECT                                                                                       " & vbNewLine _
    & "   SAGYO.NRS_BR_CD                                           AS NRS_BR_CD                      " & vbNewLine _
    & "  ,SAGYO.SAGYO_REC_NO                                        AS SAGYO_REC_NO                   " & vbNewLine _
    & "  ,COUTL.OUTKA_NO_L                                          AS OUTKA_NO_L                     " & vbNewLine _
    & "  ,'000'                                                     AS OUTKA_NO_M                     " & vbNewLine _
    & "  ,'000'                                                     AS OUTKA_NO_S                     " & vbNewLine _
    & "  ,ISNULL((SELECT MAX(KENPIN_ATTEND_SEQ) FROM $LM_TRN$..TC_KENPIN_HEAD                         " & vbNewLine _
    & "          WHERE NRS_BR_CD  = @NRS_BR_CD                                                        " & vbNewLine _
    & "          AND   OUTKA_NO_L = @OUTKA_NO_L                                                       " & vbNewLine _
    & "          GROUP BY NRS_BR_CD,OUTKA_NO_L)                                                       " & vbNewLine _
    & "          ,0)+1                                              AS WORK_SEQ                       " & vbNewLine _
    & "  ,'00'                                                      AS SAGYO_STATE1_KB                " & vbNewLine _
    & "  ,'00'                                                      AS SAGYO_STATE2_KB                " & vbNewLine _
    & "  ,'00'                                                      AS SAGYO_STATE3_KB                " & vbNewLine _
    & "  ,SAGYO.WH_CD                                               AS WH_CD                          " & vbNewLine _
    & "  ,SAGYO.GOODS_CD_NRS                                        AS GOODS_CD_NRS                   " & vbNewLine _
    & "  ,SAGYO.GOODS_NM_NRS                                        AS GOODS_NM_NRS                   " & vbNewLine _
    & "  ,0                                                         AS IRIME                          " & vbNewLine _
    & "  ,''                                                        AS IRIME_UT                       " & vbNewLine _
    & "  ,0                                                         AS PKG_NB                         " & vbNewLine _
    & "  ,''                                                        AS PKG_UT                         " & vbNewLine _
    & "  ,''                                                        AS LOT_NO                         " & vbNewLine _
    & "  ,''                                                        AS TOU_NO                         " & vbNewLine _
    & "  ,''                                                        AS SITU_NO                        " & vbNewLine _
    & "  ,''                                                        AS ZONE_CD                        " & vbNewLine _
    & "  ,''                                                        AS LOCA                           " & vbNewLine _
    & "  ,SAGYO.SAGYO_CD                                            AS SAGYO_CD                       " & vbNewLine _
    & "  ,MSAGYO.WH_SAGYO_NM                                        AS SAGYO_NM                       " & vbNewLine _
    & "  ,SAGYO.INV_TANI                                            AS INV_TANI                       " & vbNewLine _
    & "  ,MSAGYO.KOSU_BAI                                           AS KOSU_BAI                       " & vbNewLine _
    & "  ,SAGYO.SAGYO_NB                                            AS SAGYO_NB                       " & vbNewLine _
    & "  ,SAGYO.REMARK_SIJI                                         AS REMARK                         " & vbNewLine _
    & "  ,'01'                                                      AS JISYATASYA_KB                  " & vbNewLine _
    & "  ,SAGYO.IOZS_KB                                             AS IOZS_KB                        " & vbNewLine _
    & " FROM                                                                                          " & vbNewLine _
    & " $LM_TRN$..E_SAGYO  SAGYO                                                                      " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_SAGYO    MSAGYO                                                         " & vbNewLine _
    & " ON     SAGYO.NRS_BR_CD = MSAGYO.NRS_BR_CD                                                     " & vbNewLine _
    & " AND    SAGYO.SAGYO_CD = MSAGYO.SAGYO_CD                                                       " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..C_OUTKA_L COUTL                                                           " & vbNewLine _
    & " ON     COUTL.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    COUTL.OUTKA_NO_L = LEFT(SAGYO.INOUTKA_NO_LM, 9)                                        " & vbNewLine _
    & " AND    COUTL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..Z_KBN INV_TANI_KBN                                                        " & vbNewLine _
    & " ON     INV_TANI_KBN.KBN_GROUP_CD = 'S027'                                                     " & vbNewLine _
    & " AND    INV_TANI_KBN.KBN_CD = SAGYO.INV_TANI                                                   " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_CUST MCUST                                                              " & vbNewLine _
    & " ON     MCUST.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_L = SAGYO.CUST_CD_L                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_M = SAGYO.CUST_CD_M                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_S = '00'                                                                 " & vbNewLine _
    & " AND    MCUST.CUST_CD_SS = '00'                                                                " & vbNewLine _
    & " WHERE                                                                                         " & vbNewLine _
    & "     SAGYO.NRS_BR_CD              = @NRS_BR_CD                                                 " & vbNewLine _
    & " AND LEFT(SAGYO.INOUTKA_NO_LM, 9) = @OUTKA_NO_L                                                " & vbNewLine _
    & " AND MSAGYO.KOSU_BAI              = '01'                                                       " & vbNewLine _
    & " AND MSAGYO.WH_SAGYO_YN           = '01'                                                       " & vbNewLine _
    & " AND SAGYO.SYS_DEL_FLG            = '0'                                                        " & vbNewLine _
    & " AND SAGYO.IOZS_KB                = '20'                                                       " & vbNewLine _
    & "                                                                                               " & vbNewLine _
    & "  --中 KOSU_BAI = 01                                                                           " & vbNewLine _
    & "  UNION ALL                                                                                    " & vbNewLine _
    & "  SELECT                                                                                       " & vbNewLine _
    & "   SAGYO.NRS_BR_CD                                           AS NRS_BR_CD                      " & vbNewLine _
    & "  ,SAGYO.SAGYO_REC_NO                                        AS SAGYO_REC_NO                   " & vbNewLine _
    & "  ,COUTL.OUTKA_NO_L                                          AS OUTKA_NO_L                     " & vbNewLine _
    & "  ,COUTM.OUTKA_NO_M                                          AS OUTKA_NO_M                     " & vbNewLine _
    & "  ,'000'                                                     AS OUTKA_NO_S                     " & vbNewLine _
    & "  ,ISNULL((SELECT MAX(KENPIN_ATTEND_SEQ) FROM $LM_TRN$..TC_KENPIN_HEAD                         " & vbNewLine _
    & "          WHERE NRS_BR_CD  = @NRS_BR_CD                                                        " & vbNewLine _
    & "          AND   OUTKA_NO_L = @OUTKA_NO_L                                                       " & vbNewLine _
    & "          GROUP BY NRS_BR_CD,OUTKA_NO_L)                                                       " & vbNewLine _
    & "          ,0)+1                                              AS WORK_SEQ                       " & vbNewLine _
    & "  ,'00'                                                      AS SAGYO_STATE1_KB                " & vbNewLine _
    & "  ,'00'                                                      AS SAGYO_STATE2_KB                " & vbNewLine _
    & "  ,'00'                                                      AS SAGYO_STATE3_KB                " & vbNewLine _
    & "  ,SAGYO.WH_CD                                               AS WH_CD                          " & vbNewLine _
    & "  ,GOODS.GOODS_CD_NRS                                        AS GOODS_CD_NRS                   " & vbNewLine _
    & "  ,GOODS.GOODS_NM_1                                          AS GOODS_NM_NRS                   " & vbNewLine _
    & "  ,COUTM.IRIME                                               AS IRIME                          " & vbNewLine _
    & "  ,COUTM.IRIME_UT                                            AS IRIME_UT                       " & vbNewLine _
    & "  ,GOODS.PKG_NB                                              AS PKG_NB                         " & vbNewLine _
    & "  ,GOODS.PKG_UT                                              AS PKG_UT                         " & vbNewLine _
    & "  ,''                                                        AS LOT_NO                         " & vbNewLine _
    & "  ,''                                                        AS TOU_NO                         " & vbNewLine _
    & "  ,''                                                        AS SITU_NO                        " & vbNewLine _
    & "  ,''                                                        AS ZONE_CD                        " & vbNewLine _
    & "  ,''                                                        AS LOCA                           " & vbNewLine _
    & "  ,SAGYO.SAGYO_CD                                            AS SAGYO_CD                       " & vbNewLine _
    & "  ,MSAGYO.WH_SAGYO_NM                                        AS SAGYO_NM                       " & vbNewLine _
    & "  ,SAGYO.INV_TANI                                            AS INV_TANI                       " & vbNewLine _
    & "  ,MSAGYO.KOSU_BAI                                           AS KOSU_BAI                       " & vbNewLine _
    & "  ,SAGYO.SAGYO_NB                                            AS SAGYO_NB                       " & vbNewLine _
    & "  ,SAGYO.REMARK_SIJI                                         AS REMARK                         " & vbNewLine _
    & "  ,'01'                                                      AS JISYATASYA_KB                  " & vbNewLine _
    & "  ,SAGYO.IOZS_KB                                             AS IOZS_KB                        " & vbNewLine _
    & " FROM                                                                                          " & vbNewLine _
    & " $LM_TRN$..E_SAGYO  SAGYO                                                                      " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_SAGYO    MSAGYO                                                         " & vbNewLine _
    & " ON     SAGYO.NRS_BR_CD = MSAGYO.NRS_BR_CD                                                     " & vbNewLine _
    & " AND    SAGYO.SAGYO_CD = MSAGYO.SAGYO_CD                                                       " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..C_OUTKA_L COUTL                                                           " & vbNewLine _
    & " ON     COUTL.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    COUTL.OUTKA_NO_L = LEFT(SAGYO.INOUTKA_NO_LM, 9)                                        " & vbNewLine _
    & " AND    COUTL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..C_OUTKA_M COUTM                                                           " & vbNewLine _
    & " ON     COUTM.NRS_BR_CD = COUTL.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    COUTM.OUTKA_NO_L = COUTL.OUTKA_NO_L                                                    " & vbNewLine _
    & " AND    COUTM.OUTKA_NO_M = RIGHT(SAGYO.INOUTKA_NO_LM, 3)                                       " & vbNewLine _
    & " AND    COUTM.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                                             " & vbNewLine _
    & " ON     GOODS.NRS_BR_CD = COUTM.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    GOODS.GOODS_CD_NRS = COUTM.GOODS_CD_NRS                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_CUST MCUST                                                              " & vbNewLine _
    & " ON     MCUST.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_L = SAGYO.CUST_CD_L                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_M = SAGYO.CUST_CD_M                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_S = '00'                                                                 " & vbNewLine _
    & " AND    MCUST.CUST_CD_SS = '00'                                                                " & vbNewLine _
    & " WHERE                                                                                         " & vbNewLine _
    & "     SAGYO.NRS_BR_CD              = @NRS_BR_CD                                                 " & vbNewLine _
    & " AND LEFT(SAGYO.INOUTKA_NO_LM, 9) = @OUTKA_NO_L                                                " & vbNewLine _
    & " AND MSAGYO.KOSU_BAI              = '01'                                                       " & vbNewLine _
    & " AND MSAGYO.WH_SAGYO_YN           = '01'                                                       " & vbNewLine _
    & " AND SAGYO.SYS_DEL_FLG            = '0'                                                        " & vbNewLine _
    & " AND SAGYO.IOZS_KB                = '21'                                                       " & vbNewLine _
    & "  --大 KOSU_BAI = 02                                                                           " & vbNewLine _
    & " UNION ALL                                                                                     " & vbNewLine _
    & " SELECT                                                                                        " & vbNewLine _
    & "   SAGYO.NRS_BR_CD                                           AS NRS_BR_CD                      " & vbNewLine _
    & "  ,SAGYO.SAGYO_REC_NO                                        AS SAGYO_REC_NO                   " & vbNewLine _
    & "  ,COUTL.OUTKA_NO_L                                          AS OUTKA_NO_L                     " & vbNewLine _
    & "  ,COUTM.OUTKA_NO_M                                          AS OUTKA_NO_M                     " & vbNewLine _
    & "  ,COUTS.OUTKA_NO_S                                          AS OUTKA_NO_S                     " & vbNewLine _
    & "  ,ISNULL((SELECT MAX(KENPIN_ATTEND_SEQ) FROM $LM_TRN$..TC_KENPIN_HEAD                         " & vbNewLine _
    & "          WHERE NRS_BR_CD  = @NRS_BR_CD                                                        " & vbNewLine _
    & "          AND   OUTKA_NO_L = @OUTKA_NO_L                                                       " & vbNewLine _
    & "          GROUP BY NRS_BR_CD,OUTKA_NO_L)                                                       " & vbNewLine _
    & "          ,0)+1                                              AS WORK_SEQ                       " & vbNewLine _
    & "  ,CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                            " & vbNewLine _
    & "        WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                            " & vbNewLine _
    & "        ELSE                                   '00'                                            " & vbNewLine _
    & "   END                                                       AS SAGYO_STATE1_KB                " & vbNewLine _
    & "  ,CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                            " & vbNewLine _
    & "        WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                            " & vbNewLine _
    & "        ELSE                                   '00'                                            " & vbNewLine _
    & "   END                                                       AS SAGYO_STATE2_KB                " & vbNewLine _
    & "  ,CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                            " & vbNewLine _
    & "        WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                            " & vbNewLine _
    & "        ELSE                                   '00'                                            " & vbNewLine _
    & "   END                                                       AS SAGYO_STATE3_KB                " & vbNewLine _
    & "  ,SAGYO.WH_CD                                               AS WH_CD                          " & vbNewLine _
    & "  ,GOODS.GOODS_CD_NRS                                        AS GOODS_CD_NRS                   " & vbNewLine _
    & "  ,GOODS.GOODS_NM_1                                          AS GOODS_NM_NRS                   " & vbNewLine _
    & "  ,COUTS.IRIME                                               AS IRIME                          " & vbNewLine _
    & "  ,COUTM.IRIME_UT                                            AS IRIME_UT                       " & vbNewLine _
    & "  ,GOODS.PKG_NB                                              AS PKG_NB                         " & vbNewLine _
    & "  ,GOODS.PKG_UT                                              AS PKG_UT                         " & vbNewLine _
    & "  ,COUTS.LOT_NO                                              AS LOT_NO                         " & vbNewLine _
    & "  ,COUTS.TOU_NO                                              AS TOU_NO                         " & vbNewLine _
    & "  ,COUTS.SITU_NO                                             AS SITU_NO                        " & vbNewLine _
    & "  ,COUTS.ZONE_CD                                             AS ZONE_CD                        " & vbNewLine _
    & "  ,COUTS.LOCA                                                AS LOCA                           " & vbNewLine _
    & "  ,SAGYO.SAGYO_CD                                            AS SAGYO_CD                       " & vbNewLine _
    & "  ,MSAGYO.WH_SAGYO_NM                                        AS SAGYO_NM                       " & vbNewLine _
    & "  ,SAGYO.INV_TANI                                            AS INV_TANI                       " & vbNewLine _
    & "  ,MSAGYO.KOSU_BAI                                           AS KOSU_BAI                       " & vbNewLine _
    & "  ,COUTS.ALCTD_NB                                            AS SAGYO_NB                       " & vbNewLine _
    & "  ,SAGYO.REMARK_SIJI                                         AS REMARK                         " & vbNewLine _
    & "  ,TOUSITU.JISYATASYA_KB                                     AS JISYATASYA_KB                  " & vbNewLine _
    & "  ,SAGYO.IOZS_KB                                             AS IOZS_KB                        " & vbNewLine _
    & " FROM                                                                                          " & vbNewLine _
    & " $LM_TRN$..E_SAGYO  SAGYO                                                                      " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_SAGYO    MSAGYO                                                         " & vbNewLine _
    & " ON     SAGYO.NRS_BR_CD = MSAGYO.NRS_BR_CD                                                     " & vbNewLine _
    & " AND    SAGYO.SAGYO_CD = MSAGYO.SAGYO_CD                                                       " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..C_OUTKA_L  COUTL                                                          " & vbNewLine _
    & " ON     COUTL.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    COUTL.OUTKA_NO_L = LEFT(SAGYO.INOUTKA_NO_LM, 9)                                        " & vbNewLine _
    & " AND    COUTL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..C_OUTKA_M COUTM                                                           " & vbNewLine _
    & " ON     COUTM.NRS_BR_CD = COUTL.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    COUTM.OUTKA_NO_L = COUTL.OUTKA_NO_L                                                    " & vbNewLine _
    & " AND    COUTM.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..C_OUTKA_S COUTS                                                           " & vbNewLine _
    & " ON     COUTS.NRS_BR_CD = COUTM.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    COUTS.OUTKA_NO_L = COUTM.OUTKA_NO_L                                                    " & vbNewLine _
    & " AND    COUTS.OUTKA_NO_M = COUTM.OUTKA_NO_M                                                    " & vbNewLine _
    & " AND    COUTS.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                                             " & vbNewLine _
    & " ON     GOODS.NRS_BR_CD = COUTM.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    GOODS.GOODS_CD_NRS = COUTM.GOODS_CD_NRS                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_CUST MCUST                                                              " & vbNewLine _
    & " ON     MCUST.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND     MCUST.CUST_CD_L = SAGYO.CUST_CD_L                                                     " & vbNewLine _
    & " AND    MCUST.CUST_CD_M = SAGYO.CUST_CD_M                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_S = '00'                                                                 " & vbNewLine _
    & " AND    MCUST.CUST_CD_SS = '00'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_TOU_SITU TOUSITU                                                        " & vbNewLine _
    & " ON  TOUSITU.WH_CD   = COUTL.WH_CD                                                             " & vbNewLine _
    & " AND TOUSITU.TOU_NO  = COUTS.TOU_NO                                                            " & vbNewLine _
    & " AND TOUSITU.SITU_NO = COUTS.SITU_NO                                                           " & vbNewLine _
    & " WHERE                                                                                         " & vbNewLine _
    & "     SAGYO.NRS_BR_CD              = @NRS_BR_CD                                                 " & vbNewLine _
    & " AND LEFT(SAGYO.INOUTKA_NO_LM,9)  = @OUTKA_NO_L                                                " & vbNewLine _
    & " AND MSAGYO.KOSU_BAI              = '02'                                                       " & vbNewLine _
    & " AND MSAGYO.WH_SAGYO_YN           = '01'                                                       " & vbNewLine _
    & " AND SAGYO.SYS_DEL_FLG            = '0'                                                        " & vbNewLine _
    & " AND SAGYO.IOZS_KB                = '20'                                                       " & vbNewLine _
    & "  --中 KOSU_BAI = 02                                                                           " & vbNewLine _
    & " UNION ALL                                                                                     " & vbNewLine _
    & " SELECT                                                                                        " & vbNewLine _
    & "   SAGYO.NRS_BR_CD                                           AS NRS_BR_CD                      " & vbNewLine _
    & "  ,SAGYO.SAGYO_REC_NO                                        AS SAGYO_REC_NO                   " & vbNewLine _
    & "  ,COUTL.OUTKA_NO_L                                          AS OUTKA_NO_L                     " & vbNewLine _
    & "  ,COUTM.OUTKA_NO_M                                          AS OUTKA_NO_M                     " & vbNewLine _
    & "  ,COUTS.OUTKA_NO_S                                          AS OUTKA_NO_S                     " & vbNewLine _
    & "  ,ISNULL((SELECT MAX(KENPIN_ATTEND_SEQ) FROM $LM_TRN$..TC_KENPIN_HEAD                         " & vbNewLine _
    & "          WHERE NRS_BR_CD  = @NRS_BR_CD                                                        " & vbNewLine _
    & "          AND   OUTKA_NO_L = @OUTKA_NO_L                                                       " & vbNewLine _
    & "          GROUP BY NRS_BR_CD,OUTKA_NO_L)                                                       " & vbNewLine _
    & "          ,0)+1                                              AS WORK_SEQ                       " & vbNewLine _
    & "  ,CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                            " & vbNewLine _
    & "        WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                            " & vbNewLine _
    & "        ELSE                                   '00'                                            " & vbNewLine _
    & "   END                                                       AS SAGYO_STATE1_KB                " & vbNewLine _
    & "  ,CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                            " & vbNewLine _
    & "        WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                            " & vbNewLine _
    & "        ELSE                                   '00'                                            " & vbNewLine _
    & "   END                                                       AS SAGYO_STATE2_KB                " & vbNewLine _
    & "  ,CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                            " & vbNewLine _
    & "        WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                            " & vbNewLine _
    & "        ELSE                                   '00'                                            " & vbNewLine _
    & "   END                                                       AS SAGYO_STATE3_KB                " & vbNewLine _
    & "  ,SAGYO.WH_CD                                               AS WH_CD                          " & vbNewLine _
    & "  ,GOODS.GOODS_CD_NRS                                        AS GOODS_CD_NRS                   " & vbNewLine _
    & "  ,GOODS.GOODS_NM_1                                          AS GOODS_NM_NRS                   " & vbNewLine _
    & "  ,COUTS.IRIME                                               AS IRIME                          " & vbNewLine _
    & "  ,COUTM.IRIME_UT                                            AS IRIME_UT                       " & vbNewLine _
    & "  ,GOODS.PKG_NB                                              AS PKG_NB                         " & vbNewLine _
    & "  ,GOODS.PKG_UT                                              AS PKG_UT                         " & vbNewLine _
    & "  ,COUTS.LOT_NO                                              AS LOT_NO                         " & vbNewLine _
    & "  ,COUTS.TOU_NO                                              AS TOU_NO                         " & vbNewLine _
    & "  ,COUTS.SITU_NO                                             AS SITU_NO                        " & vbNewLine _
    & "  ,COUTS.ZONE_CD                                             AS ZONE_CD                        " & vbNewLine _
    & "  ,COUTS.LOCA                                                AS LOCA                           " & vbNewLine _
    & "  ,SAGYO.SAGYO_CD                                            AS SAGYO_CD                       " & vbNewLine _
    & "  ,MSAGYO.WH_SAGYO_NM                                        AS SAGYO_NM                       " & vbNewLine _
    & "  ,SAGYO.INV_TANI                                            AS INV_TANI                       " & vbNewLine _
    & "  ,MSAGYO.KOSU_BAI                                           AS KOSU_BAI                       " & vbNewLine _
    & "  ,COUTS.ALCTD_NB                                            AS SAGYO_NB                       " & vbNewLine _
    & "  ,SAGYO.REMARK_SIJI                                         AS REMARK                         " & vbNewLine _
    & "  ,TOUSITU.JISYATASYA_KB                                     AS JISYATASYA_KB                  " & vbNewLine _
    & "  ,SAGYO.IOZS_KB                                             AS IOZS_KB                        " & vbNewLine _
    & " FROM                                                                                          " & vbNewLine _
    & " $LM_TRN$..E_SAGYO  SAGYO                                                                      " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_SAGYO    MSAGYO                                                         " & vbNewLine _
    & " ON     SAGYO.NRS_BR_CD = MSAGYO.NRS_BR_CD                                                     " & vbNewLine _
    & " AND    SAGYO.SAGYO_CD = MSAGYO.SAGYO_CD                                                       " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..C_OUTKA_L  COUTL                                                          " & vbNewLine _
    & " ON     COUTL.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    COUTL.OUTKA_NO_L = LEFT(SAGYO.INOUTKA_NO_LM, 9)                                        " & vbNewLine _
    & " AND    COUTL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..C_OUTKA_M COUTM                                                           " & vbNewLine _
    & " ON     COUTM.NRS_BR_CD = COUTL.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    COUTM.OUTKA_NO_L = COUTL.OUTKA_NO_L                                                    " & vbNewLine _
    & " AND    COUTM.OUTKA_NO_M = RIGHT(SAGYO.INOUTKA_NO_LM, 3)                                       " & vbNewLine _
    & " AND    COUTM.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..C_OUTKA_S COUTS                                                           " & vbNewLine _
    & " ON     COUTS.NRS_BR_CD = COUTM.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    COUTS.OUTKA_NO_L = COUTM.OUTKA_NO_L                                                    " & vbNewLine _
    & " AND    COUTS.OUTKA_NO_M = COUTM.OUTKA_NO_M                                                    " & vbNewLine _
    & " AND    COUTS.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                                             " & vbNewLine _
    & " ON      GOODS.NRS_BR_CD = COUTM.NRS_BR_CD                                                     " & vbNewLine _
    & " AND     GOODS.GOODS_CD_NRS = COUTM.GOODS_CD_NRS                                               " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_CUST MCUST                                                              " & vbNewLine _
    & " ON     MCUST.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_L = SAGYO.CUST_CD_L                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_M = SAGYO.CUST_CD_M                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_S = '00'                                                                 " & vbNewLine _
    & " AND    MCUST.CUST_CD_SS = '00'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_TOU_SITU TOUSITU                                                        " & vbNewLine _
    & " ON  TOUSITU.WH_CD   = COUTL.WH_CD                                                             " & vbNewLine _
    & " AND TOUSITU.TOU_NO  = COUTS.TOU_NO                                                            " & vbNewLine _
    & " AND TOUSITU.SITU_NO = COUTS.SITU_NO                                                           " & vbNewLine _
    & " WHERE                                                                                         " & vbNewLine _
    & "     SAGYO.NRS_BR_CD              = @NRS_BR_CD                                                 " & vbNewLine _
    & " AND LEFT(SAGYO.INOUTKA_NO_LM,9)  = @OUTKA_NO_L                                                " & vbNewLine _
    & " AND MSAGYO.KOSU_BAI              = '02'                                                       " & vbNewLine _
    & " AND MSAGYO.WH_SAGYO_YN           = '01'                                                       " & vbNewLine _
    & " AND SAGYO.SYS_DEL_FLG            = '0'                                                        " & vbNewLine _
    & " AND SAGYO.IOZS_KB                = '21'                                                       " & vbNewLine

#End Region

#Region "チェック用データ取得"
    ''' <summary>
    ''' チェック用データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CHECK_DATA As String _
    = " SELECT                                                                        " & vbNewLine _
    & "  OUTL.NRS_BR_CD                          AS NRS_BR_CD                         " & vbNewLine _
    & " ,OUTL.OUTKA_NO_L                         AS OUTKA_NO_L                        " & vbNewLine _
    & " ,ISNULL(OUTM.OUTKA_NO_M,'')              AS OUTKA_NO_M                        " & vbNewLine _
    & " ,ISNULL(OUTS.OUTKA_NO_S,'')              AS OUTKA_NO_S                        " & vbNewLine _
    & " ,OUTL.OUTKA_STATE_KB                     AS OUTKA_STATE_KB                    " & vbNewLine _
    & " ,OUTL.WH_TAB_YN                          AS WH_TAB_YN                         " & vbNewLine _
    & " ,OUTL.WH_TAB_STATUS                      AS WH_TAB_STATUS                     " & vbNewLine _
    & " ,ISNULL(OUTM.OUTKA_TTL_NB,0)             AS OUTKA_TTL_NB                      " & vbNewLine _
    & " ,ISNULL(OUTM.ALCTD_NB,0)                 AS ALCTD_NB                          " & vbNewLine _
    & " ,ISNULL(OUTM.BACKLOG_NB,0)               AS BACKLOG_NB                        " & vbNewLine _
    & " ,ISNULL(TOUSITU.TOU_NO,'')               AS TOU_NO                            " & vbNewLine _
    & " ,ISNULL(TOUSITU.SITU_NO,'')              AS SITU_NO                           " & vbNewLine _
    & " ,ISNULL(TOUSITU.USER_CD,'')              AS USER_CD                           " & vbNewLine _
    & " ,ISNULL(TOUSITU.JISYATASYA_KB,'')        AS JISYATASYA_KB                     " & vbNewLine _
    & " ,ISNULL(SUSER.USER_NM,'')                AS USER_NM                           " & vbNewLine _
    & " FROM $LM_TRN$..C_OUTKA_L OUTL                                                 " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                            " & vbNewLine _
    & " ON  OUTM.NRS_BR_CD   = OUTL.NRS_BR_CD                                         " & vbNewLine _
    & " AND OUTM.OUTKA_NO_L  = OUTL.OUTKA_NO_L                                        " & vbNewLine _
    & " AND OUTM.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                            " & vbNewLine _
    & " ON  OUTS.NRS_BR_CD   = OUTM.NRS_BR_CD                                         " & vbNewLine _
    & " AND OUTS.OUTKA_NO_L  = OUTM.OUTKA_NO_L                                        " & vbNewLine _
    & " AND OUTS.OUTKA_NO_M  = OUTM.OUTKA_NO_M                                        " & vbNewLine _
    & " AND OUTS.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_TOU_SITU TOUSITU                                        " & vbNewLine _
    & " ON  TOUSITU.NRS_BR_CD = OUTS.NRS_BR_CD                                        " & vbNewLine _
    & " AND TOUSITU.WH_CD     = OUTL.WH_CD                                            " & vbNewLine _
    & " AND TOUSITU.TOU_NO    = OUTS.TOU_NO                                           " & vbNewLine _
    & " AND TOUSITU.SITU_NO   = OUTS.SITU_NO                                          " & vbNewLine _
    & " LEFT JOIN $LM_MST$..S_USER SUSER                                              " & vbNewLine _
    & " ON  SUSER.USER_CD     = TOUSITU.USER_CD                                       " & vbNewLine _
    & " AND SUSER.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
    & " WHERE                                                                         " & vbNewLine _
    & "     OUTL.NRS_BR_CD  = @NRS_BR_CD                                              " & vbNewLine _
    & " AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                             " & vbNewLine

#End Region

#Region "自社他社件数取得"
    ''' <summary>
    ''' 自社他社件数取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_JISYATASYA_COUNT As String _
    = " SELECT                     　                                    " & vbNewLine _
    & "  SUM(JISYA_CNT) AS JISYA_CNT                                     " & vbNewLine _
    & " ,SUM(TASYA_CNT) AS TASYA_CNT                                     " & vbNewLine _
    & " FROM (                                                           " & vbNewLine _
    & "  SELECT                                                          " & vbNewLine _
    & "   CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN 1                  " & vbNewLine _
    & "   ELSE 0                                                         " & vbNewLine _
    & "   END AS JISYA_CNT                                               " & vbNewLine _
    & "  ,CASE WHEN TOUSITU.JISYATASYA_KB = '02' THEN 1                  " & vbNewLine _
    & "   ELSE 0                                                         " & vbNewLine _
    & "   END AS TASYA_CNT                                               " & vbNewLine _
    & "  FROM $LM_TRN$..C_OUTKA_S OUTS                                   " & vbNewLine _
    & "  LEFT JOIN $LM_TRN$..C_OUTKA_L OUTL                              " & vbNewLine _
    & "  ON  OUTL.NRS_BR_CD  = OUTS.NRS_BR_CD                            " & vbNewLine _
    & "  AND OUTL.OUTKA_NO_L = OUTS.OUTKA_NO_L                           " & vbNewLine _
    & "  LEFT JOIN $LM_MST$..M_TOU_SITU TOUSITU                          " & vbNewLine _
    & "  ON  TOUSITU.NRS_BR_CD = OUTS.NRS_BR_CD                          " & vbNewLine _
    & "  AND TOUSITU.WH_CD     = OUTL.WH_CD                              " & vbNewLine _
    & "  AND TOUSITU.TOU_NO    = OUTS.TOU_NO                             " & vbNewLine _
    & "  AND TOUSITU.SITU_NO   = OUTS.SITU_NO                            " & vbNewLine _
    & "  WHERE                                                           " & vbNewLine _
    & "      OUTS.NRS_BR_CD  = @NRS_BR_CD                                " & vbNewLine _
    & "  AND OUTS.OUTKA_NO_L = @OUTKA_NO_L                               " & vbNewLine _
    & "  AND OUTS.OUTKA_NO_M = @OUTKA_NO_M                               " & vbNewLine _
    & " ) A                                                              " & vbNewLine

#End Region

#Region "運送会社情報取得"
    ''' <summary>
    ''' 運送会社情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNSOCO_DATA As String _
    = " SELECT                     　                                   " & vbNewLine _
    & "  NRS_BR_CD      AS NRS_BR_CD                                    " & vbNewLine _
    & " ,UNSOCO_CD      AS UNSO_CD                                      " & vbNewLine _
    & " ,UNSOCO_NM      AS UNSO_NM                                      " & vbNewLine _
    & " ,UNSOCO_BR_CD   AS UNSO_BR_CD                                   " & vbNewLine _
    & " ,UNSOCO_BR_NM   AS UNSO_BR_NM                                   " & vbNewLine _
    & " ,WH_NIFUDA_SCAN_YN AS NIHUDA_CHK_FLG                            " & vbNewLine _
    & " FROM $LM_MST$..M_UNSOCO                                         " & vbNewLine _
    & " WHERE                                                           " & vbNewLine _
    & "     NRS_BR_CD    = @NRS_BR_CD                                   " & vbNewLine _
    & " AND UNSOCO_CD    = @UNSO_CD                                     " & vbNewLine _
    & " AND UNSOCO_BR_CD = @UNSO_BR_CD                                  " & vbNewLine

#End Region

#Region "検品明細取得"
    ''' <summary>
    ''' 出荷検品明細取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TC_KENPIN_DTL As String _
    = " SELECT                                                                          " & vbNewLine _
    & "    MAIN.NRS_BR_CD                                   AS NRS_BR_CD                        " & vbNewLine _
    & "  , MAIN.OUTKA_NO_L                                  AS OUTKA_NO_L                       " & vbNewLine _
    & "  , MAIN.KENPIN_ATTEND_SEQ                           AS KENPIN_ATTEND_SEQ                " & vbNewLine _
    & "  , MAIN.OUTKA_NO_M                                  AS OUTKA_NO_M                       " & vbNewLine _
    & "  , MAIN.OUTKA_NO_S                                  AS OUTKA_NO_S                       " & vbNewLine _
    & "  , MAIN.KENPIN_DTL_SEQ                              AS KENPIN_DTL_SEQ                   " & vbNewLine _
    & "  , MAIN.KENPIN_STATE_KB                             AS KENPIN_STATE_KB                  " & vbNewLine _
    & "  , MAIN.ATTEND_STATE_KB                             AS ATTEND_STATE_KB                  " & vbNewLine _
    & "  , MAIN.GOODS_CD_NRS                                AS GOODS_CD_NRS                     " & vbNewLine _
    & "  , MAIN.GOODS_CD_CUST                               AS GOODS_CD_CUST                    " & vbNewLine _
    & "  , MAIN.GOODS_NM_NRS                                AS GOODS_NM_NRS                     " & vbNewLine _
    & "  , MAIN.LOT_NO                                      AS LOT_NO                           " & vbNewLine _
    & "  , MAIN.IRIME                                       AS IRIME                            " & vbNewLine _
    & "  , MAIN.IRIME_UT                                    AS IRIME_UT                         " & vbNewLine _
    & "  , MAIN.NB_UT                                       AS NB_UT                            " & vbNewLine _
    & "  , MAIN.PKG_NB                                      AS PKG_NB                           " & vbNewLine _
    & "  , MAIN.PKG_UT                                      AS PKG_UT                           " & vbNewLine _
    & "  , MAIN.TOU_NO                                      AS TOU_NO                           " & vbNewLine _
    & "  , MAIN.SITU_NO                                     AS SITU_NO                          " & vbNewLine _
    & "  , MAIN.ZONE_CD                                     AS ZONE_CD                          " & vbNewLine _
    & "  , MAIN.LOCA                                        AS LOCA                             " & vbNewLine _
    & "  , MAIN.OUT_NB                                      AS OUT_NB                           " & vbNewLine _
    & "  , MAIN.OUT_KONSU                                   AS OUT_KONSU                        " & vbNewLine _
    & "  , MAIN.OUT_HASU                                    AS OUT_HASU                         " & vbNewLine _
    & "  , MAIN.OUT_ZAN_NB                                  AS OUT_ZAN_NB                       " & vbNewLine _
    & "  , MAIN.OUT_ZAN_KONSU                               AS OUT_ZAN_KONSU                    " & vbNewLine _
    & "  , MAIN.OUT_ZAN_HASU                                AS OUT_ZAN_HASU                     " & vbNewLine _
    & "  , MAIN.OUT_QT                                      AS OUT_QT                           " & vbNewLine _
    & "  , MAIN.OUT_ZAN_QT                                  AS OUT_ZAN_QT                       " & vbNewLine _
    & "  , MAIN.GOODS_COND_KB1                              AS GOODS_COND_KB1                   " & vbNewLine _
    & "  , MAIN.GOODS_COND_KB2                              AS GOODS_COND_KB2                   " & vbNewLine _
    & "  , MAIN.GOODS_COND_KB3                              AS GOODS_COND_KB3                   " & vbNewLine _
    & "  , MAIN.LT_DATE                                     AS LT_DATE                          " & vbNewLine _
    & "  , MAIN.GOODS_CRT_DATE                              AS GOODS_CRT_DATE                   " & vbNewLine _
    & "  , MAIN.SPD_KB                                      AS SPD_KB                           " & vbNewLine _
    & "  , MAIN.OFB_KB                                      AS OFB_KB                           " & vbNewLine _
    & "  , MAIN.RSV_NO                                      AS RSV_NO                           " & vbNewLine _
    & "  , MAIN.SERIAL_NO                                   AS SERIAL_NO                        " & vbNewLine _
    & "  , MAIN.INKA_NO_L                                   AS INKA_NO_L                        " & vbNewLine _
    & "  , MAIN.INKA_NO_M                                   AS INKA_NO_M                        " & vbNewLine _
    & "  , MAIN.INKA_NO_S                                   AS INKA_NO_S                        " & vbNewLine _
    & "  , MAIN.ZAI_REC_NO                                  AS ZAI_REC_NO                       " & vbNewLine _
    & "  , MAIN.INKA_DATE                                   AS INKA_DATE                        " & vbNewLine _
    & "  , MAIN.REMARK                                      AS REMARK                           " & vbNewLine _
    & "  , MAIN.REMARK_OUT                                  AS REMARK_OUT                       " & vbNewLine _
    & "  , MAIN.JISYATASYA_KB                               AS JISYATASYA_KB                    " & vbNewLine _
    & "  , MAIN.CANCEL_DTL_FLG                              AS CANCEL_DTL_FLG                   " & vbNewLine _
    & "  , MAIN.SYS_ENT_DATE                                AS SYS_ENT_DATE                     " & vbNewLine _
    & "  , MAIN.SYS_ENT_TIME                                AS SYS_ENT_TIME                     " & vbNewLine _
    & "  , MAIN.SYS_ENT_PGID                                AS SYS_ENT_PGID                     " & vbNewLine _
    & "  , MAIN.SYS_ENT_USER                                AS SYS_ENT_USER                     " & vbNewLine _
    & "  , MAIN.SYS_UPD_DATE                                AS SYS_UPD_DATE                     " & vbNewLine _
    & "  , MAIN.SYS_UPD_TIME                                AS SYS_UPD_TIME                     " & vbNewLine _
    & "  , MAIN.SYS_UPD_PGID                                AS SYS_UPD_PGID                     " & vbNewLine _
    & "  , MAIN.SYS_UPD_USER                                AS SYS_UPD_USER                     " & vbNewLine _
    & "  , MAIN.SYS_DEL_FLG                                 AS SYS_DEL_FLG                      " & vbNewLine _
    & " FROM $LM_TRN$..TC_KENPIN_DTL MAIN                                               " & vbNewLine _
    & " INNER JOIN (                                                                    " & vbNewLine _
    & "     SELECT                                                                      " & vbNewLine _
    & "         NRS_BR_CD                            AS NRS_BR_CD                       " & vbNewLine _
    & "       , OUTKA_NO_L                           AS OUTKA_NO_L                      " & vbNewLine _
    & "       , MAX(KENPIN_ATTEND_SEQ)               AS KENPIN_ATTEND_SEQ               " & vbNewLine _
    & "     FROM $LM_TRN$..TC_KENPIN_DTL                                                " & vbNewLine _
    & "     GROUP BY                                                                    " & vbNewLine _
    & "         NRS_BR_CD                                                               " & vbNewLine _
    & "       , OUTKA_NO_L                                                              " & vbNewLine _
    & "     )MAX_SEQ                                                                    " & vbNewLine _
    & " ON  MAX_SEQ.NRS_BR_CD         = MAIN.NRS_BR_CD                                  " & vbNewLine _
    & " AND MAX_SEQ.OUTKA_NO_L        = MAIN.OUTKA_NO_L                                 " & vbNewLine _
    & " AND MAX_SEQ.KENPIN_ATTEND_SEQ = MAIN.KENPIN_ATTEND_SEQ                          " & vbNewLine _
    & "                                                                                 " & vbNewLine _
    & " WHERE                                                                           " & vbNewLine _
    & "     MAIN.NRS_BR_CD  = @NRS_BR_CD                                                " & vbNewLine _
    & " AND MAIN.OUTKA_NO_L = @OUTKA_NO_L                                               " & vbNewLine
#End Region

#Region "出荷差分比較用出荷ピックヘッダ取得"

    ''' <summary>
    ''' 出荷差分比較用出荷ピックヘッダ取得
    ''' </summary>
    Private Const SQL_SELECT_TC_PICK_HEAD_TO_COMPARE As String _
    = "SELECT TOP 1                       " & vbNewLine _
    & "       NRS_BR_CD                   " & vbNewLine _
    & "      ,OUTKA_NO_L                  " & vbNewLine _
    & "      ,PICK_SEQ                    " & vbNewLine _
    & "      ,UNSO_NM                     " & vbNewLine _
    & "      ,UNSO_BR_NM                  " & vbNewLine _
    & "      ,DEST_NM                     " & vbNewLine _
    & "      ,DEST_AD_1                   " & vbNewLine _
    & "      ,DEST_AD_2                   " & vbNewLine _
    & "      ,DEST_AD_3                   " & vbNewLine _
    & "      ,OUTKO_DATE                  " & vbNewLine _
    & "      ,OUTKA_PLAN_DATE             " & vbNewLine _
    & "      ,ARR_PLAN_DATE               " & vbNewLine _
    & "      ,ARR_PLAN_TIME               " & vbNewLine _
    & "      ,REMARK                      " & vbNewLine _
    & "      ,REMARK_SIJI                 " & vbNewLine _
    & "      ,CUST_ORD_NO                 " & vbNewLine _
    & "      ,BUYER_ORD_NO                " & vbNewLine _
    & "      ,OUTKA_PKG_NB                " & vbNewLine _
    & "      ,OUTKA_TTL_NB                " & vbNewLine _
    & "      ,OUTKA_TTL_WT                " & vbNewLine _
    & "      ,URGENT_FLG                  " & vbNewLine _
    & "  FROM $LM_TRN$..TC_PICK_HEAD      " & vbNewLine _
    & " WHERE NRS_BR_CD = @NRS_BR_CD      " & vbNewLine _
    & "   AND OUTKA_NO_L = @OUTKA_NO_L    " & vbNewLine _
    & "   AND PICK_SEQ = (SELECT MAX(PICK_SEQ)               " & vbNewLine _
    & "                     FROM $LM_TRN$..TC_PICK_HEAD      " & vbNewLine _
    & "                    WHERE NRS_BR_CD = @NRS_BR_CD      " & vbNewLine _
    & "                      AND OUTKA_NO_L = @OUTKA_NO_L    " & vbNewLine _
    & "                      AND PICK_SEQ < @PICK_SEQ_AFT    " & vbNewLine _
    & "                      AND PICK_STATE_KB <> '99'       " & vbNewLine _
    & "                  )                                   " & vbNewLine _
    & "UNION ALL                          " & vbNewLine _
    & "SELECT TOP 1                       " & vbNewLine _
    & "       NRS_BR_CD                   " & vbNewLine _
    & "      ,OUTKA_NO_L                  " & vbNewLine _
    & "      ,PICK_SEQ                    " & vbNewLine _
    & "      ,UNSO_NM                     " & vbNewLine _
    & "      ,UNSO_BR_NM                  " & vbNewLine _
    & "      ,DEST_NM                     " & vbNewLine _
    & "      ,DEST_AD_1                   " & vbNewLine _
    & "      ,DEST_AD_2                   " & vbNewLine _
    & "      ,DEST_AD_3                   " & vbNewLine _
    & "      ,OUTKO_DATE                  " & vbNewLine _
    & "      ,OUTKA_PLAN_DATE             " & vbNewLine _
    & "      ,ARR_PLAN_DATE               " & vbNewLine _
    & "      ,ARR_PLAN_TIME               " & vbNewLine _
    & "      ,REMARK                      " & vbNewLine _
    & "      ,REMARK_SIJI                 " & vbNewLine _
    & "      ,CUST_ORD_NO                 " & vbNewLine _
    & "      ,BUYER_ORD_NO                " & vbNewLine _
    & "      ,OUTKA_PKG_NB                " & vbNewLine _
    & "      ,OUTKA_TTL_NB                " & vbNewLine _
    & "      ,OUTKA_TTL_WT                " & vbNewLine _
    & "      ,URGENT_FLG                  " & vbNewLine _
    & "  FROM $LM_TRN$..TC_PICK_HEAD      " & vbNewLine _
    & " WHERE NRS_BR_CD = @NRS_BR_CD      " & vbNewLine _
    & "   AND OUTKA_NO_L = @OUTKA_NO_L    " & vbNewLine _
    & "   AND PICK_SEQ = @PICK_SEQ_AFT    " & vbNewLine _
    & " ORDER BY PICK_SEQ                 " & vbNewLine

#End Region

#Region "出荷差分比較用出荷ピック明細取得"

    ''' <summary>
    ''' 出荷差分比較用出荷ピック明細取得
    ''' </summary>
    Private Const SQL_SELECT_TC_PICK_DTL_TO_COMPARE As String _
    = "SELECT DISTINCT                                                            " & vbNewLine _
    & "       ISNULL(DTL_AFT.OUTKA_NO_M, DTL_BEF.OUTKA_NO_M)  OUTKA_NO_M          " & vbNewLine _
    & "      ,ISNULL(DTL_AFT.GOODS_NM_NRS, DTL_BEF.GOODS_NM_NRS)  GOODS_NM_NRS    " & vbNewLine _
    & "      ,(SELECT OUTKA_NO_S + ','                                            " & vbNewLine _
    & "          FROM $LM_TRN$..TC_PICK_DTL                                       " & vbNewLine _
    & "         WHERE NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
    & "           AND OUTKA_NO_L = @OUTKA_NO_L                                    " & vbNewLine _
    & "           AND OUTKA_NO_M = DTL_BEF.OUTKA_NO_M                             " & vbNewLine _
    & "           AND PICK_SEQ = @PICK_SEQ_BEF                                    " & vbNewLine _
    & "           FOR XML PATH('')                                                " & vbNewLine _
    & "       ) OUTKA_NO_S_BEFORE                                                 " & vbNewLine _
    & "      ,(SELECT OUTKA_NO_S + ','                                            " & vbNewLine _
    & "          FROM $LM_TRN$..TC_PICK_DTL                                       " & vbNewLine _
    & "         WHERE NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
    & "           AND OUTKA_NO_L = @OUTKA_NO_L                                    " & vbNewLine _
    & "           AND OUTKA_NO_M = DTL_AFT.OUTKA_NO_M                             " & vbNewLine _
    & "           AND PICK_SEQ = @PICK_SEQ_AFT                                    " & vbNewLine _
    & "           FOR XML PATH('')                                                " & vbNewLine _
    & "       ) OUTKA_NO_S_AFTER                                                  " & vbNewLine _
    & "  FROM (SELECT *                                                           " & vbNewLine _
    & "          FROM $LM_TRN$..TC_PICK_DTL                                       " & vbNewLine _
    & "         WHERE NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
    & "           AND OUTKA_NO_L = @OUTKA_NO_L                                    " & vbNewLine _
    & "           AND PICK_SEQ = @PICK_SEQ_BEF                                    " & vbNewLine _
    & "       ) DTL_BEF                                                           " & vbNewLine _
    & "  FULL JOIN                                                                " & vbNewLine _
    & "       (SELECT *                                                           " & vbNewLine _
    & "          FROM $LM_TRN$..TC_PICK_DTL                                       " & vbNewLine _
    & "         WHERE NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
    & "           AND OUTKA_NO_L = @OUTKA_NO_L                                    " & vbNewLine _
    & "           AND PICK_SEQ = @PICK_SEQ_AFT                                    " & vbNewLine _
    & "       ) DTL_AFT                                                           " & vbNewLine _
    & "    ON DTL_BEF.OUTKA_NO_M = DTL_AFT.OUTKA_NO_M                             " & vbNewLine _
    & " ORDER BY ISNULL(DTL_AFT.OUTKA_NO_M, DTL_BEF.OUTKA_NO_M)                   " & vbNewLine _
    & "         ,ISNULL(DTL_AFT.GOODS_NM_NRS, DTL_BEF.GOODS_NM_NRS)               " & vbNewLine

#End Region

#Region "荷主明細マスタ取得"

    ''' <summary>
    ''' 荷主明細マスタ取得
    ''' </summary>
    Private Const SQL_SELECT_M_CUST_DETAILS As String _
    = "SELECT SET_NAIYO               " & vbNewLine _
    & "  FROM LM_MST..M_CUST_DETAILS  " & vbNewLine _
    & " WHERE NRS_BR_CD = @NRS_BR_CD  " & vbNewLine _
    & "   AND CUST_CD = @CUST_CD      " & vbNewLine _
    & "   AND SUB_KB = @SUB_KB        " & vbNewLine _
    & "   AND SYS_DEL_FLG = '0'       " & vbNewLine

#End Region

#End Region

#Region "Insert"

#Region "出荷ピックヘッダ登録"
    ''' <summary>
    ''' 出荷ピックヘッダ登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TC_PICK_HEAD As String _
    = "INSERT INTO $LM_TRN$..TC_PICK_HEAD( 		                                        " & vbNewLine _
    & "   NRS_BR_CD                                                                     " & vbNewLine _
    & " , OUTKA_NO_L                                                                    " & vbNewLine _
    & " , TOU_NO                                                                        " & vbNewLine _
    & " , SITU_NO                                                                       " & vbNewLine _
    & " , PICK_SEQ                                                                      " & vbNewLine _
    & " , CANCEL_FLG                                                                    " & vbNewLine _
    & " , CANCEL_TYPE                                                                   " & vbNewLine _
    & " , PICK_USER_CD                                                                  " & vbNewLine _
    & " , PICK_USER_NM                                                                  " & vbNewLine _
    & " , PICK_USER_CD_SUB                                                              " & vbNewLine _
    & " , PICK_USER_NM_SUB                                                              " & vbNewLine _
    & " , PICK_STATE_KB                                                                 " & vbNewLine _
    & " , WORK_STATE_KB                                                                 " & vbNewLine _
    & " , WH_CD                                                                         " & vbNewLine _
    & " , CUST_CD_L                                                                     " & vbNewLine _
    & " , CUST_CD_M                                                                     " & vbNewLine _
    & " , CUST_NM_L                                                                     " & vbNewLine _
    & " , CUST_NM_M                                                                     " & vbNewLine _
    & " , UNSO_CD                                                                       " & vbNewLine _
    & " , UNSO_NM                                                                       " & vbNewLine _
    & " , UNSO_BR_CD                                                                    " & vbNewLine _
    & " , UNSO_BR_NM                                                                    " & vbNewLine _
    & " , DEST_CD                                                                       " & vbNewLine _
    & " , DEST_NM                                                                       " & vbNewLine _
    & " , DEST_AD_1                                                                     " & vbNewLine _
    & " , DEST_AD_2                                                                     " & vbNewLine _
    & " , DEST_AD_3                                                                     " & vbNewLine _
    & " , DEST_TEL                                                                      " & vbNewLine _
    & " , OUTKO_DATE                                                                    " & vbNewLine _
    & " , OUTKA_PLAN_DATE                                                               " & vbNewLine _
    & " , ARR_PLAN_DATE                                                                 " & vbNewLine _
    & " , ARR_PLAN_TIME                                                                 " & vbNewLine _
    & " , REMARK                                                                        " & vbNewLine _
    & " , REMARK_SIJI                                                                   " & vbNewLine _
    & " , CUST_ORD_NO                                                                   " & vbNewLine _
    & " , BUYER_ORD_NO                                                                  " & vbNewLine _
    & " , OUTKA_PKG_NB                                                                  " & vbNewLine _
    & " , OUTKA_TTL_NB                                                                  " & vbNewLine _
    & " , OUTKA_TTL_WT                                                                  " & vbNewLine _
    & " , URGENT_FLG                                                                    " & vbNewLine _
    & " , REMARK_CHK_FLG                                                                " & vbNewLine _
    & " , JISYATASYA_KB                                                                 " & vbNewLine _
    & " , HOKOKU_FLG                                                                    " & vbNewLine _
    & " , HOKOKU_STATE_KB                                                               " & vbNewLine _
    & " , SAGYO_FILE_STATE_KB                                                           " & vbNewLine _
    & " , SYS_ENT_DATE                                                                  " & vbNewLine _
    & " , SYS_ENT_TIME                                                                  " & vbNewLine _
    & " , SYS_ENT_PGID                                                                  " & vbNewLine _
    & " , SYS_ENT_USER                                                                  " & vbNewLine _
    & " , SYS_UPD_DATE                                                                  " & vbNewLine _
    & " , SYS_UPD_TIME                                                                  " & vbNewLine _
    & " , SYS_UPD_PGID                                                                  " & vbNewLine _
    & " , SYS_UPD_USER                                                                  " & vbNewLine _
    & " , SYS_DEL_FLG                                                                   " & vbNewLine _
    & ") 	                                                                            " & vbNewLine _
    & " VALUES                                                                          " & vbNewLine _
    & "(                                                                                " & vbNewLine _
    & "   @NRS_BR_CD                                                                    " & vbNewLine _
    & " , @OUTKA_NO_L                                                                   " & vbNewLine _
    & " , @TOU_NO                                                                       " & vbNewLine _
    & " , @SITU_NO                                                                      " & vbNewLine _
    & " , @PICK_SEQ                                                                     " & vbNewLine _
    & " , @CANCEL_FLG                                                                   " & vbNewLine _
    & " , @CANCEL_TYPE                                                                  " & vbNewLine _
    & " , @PICK_USER_CD                                                                 " & vbNewLine _
    & " , @PICK_USER_NM                                                                 " & vbNewLine _
    & " , @PICK_USER_CD_SUB                                                             " & vbNewLine _
    & " , @PICK_USER_NM_SUB                                                             " & vbNewLine _
    & " , @PICK_STATE_KB                                                                " & vbNewLine _
    & " , @WORK_STATE_KB                                                                " & vbNewLine _
    & " , @WH_CD                                                                        " & vbNewLine _
    & " , @CUST_CD_L                                                                    " & vbNewLine _
    & " , @CUST_CD_M                                                                    " & vbNewLine _
    & " , @CUST_NM_L                                                                    " & vbNewLine _
    & " , @CUST_NM_M                                                                    " & vbNewLine _
    & " , @UNSO_CD                                                                      " & vbNewLine _
    & " , @UNSO_NM                                                                      " & vbNewLine _
    & " , @UNSO_BR_CD                                                                   " & vbNewLine _
    & " , @UNSO_BR_NM                                                                   " & vbNewLine _
    & " , @DEST_CD                                                                      " & vbNewLine _
    & " , @DEST_NM                                                                      " & vbNewLine _
    & " , @DEST_AD_1                                                                    " & vbNewLine _
    & " , @DEST_AD_2                                                                    " & vbNewLine _
    & " , @DEST_AD_3                                                                    " & vbNewLine _
    & " , @DEST_TEL                                                                    " & vbNewLine _
    & " , @OUTKO_DATE                                                                   " & vbNewLine _
    & " , @OUTKA_PLAN_DATE                                                              " & vbNewLine _
    & " , @ARR_PLAN_DATE                                                                " & vbNewLine _
    & " , @ARR_PLAN_TIME                                                                " & vbNewLine _
    & " , @REMARK                                                                       " & vbNewLine _
    & " , @REMARK_SIJI                                                                  " & vbNewLine _
    & " , @CUST_ORD_NO                                                                  " & vbNewLine _
    & " , @BUYER_ORD_NO                                                                 " & vbNewLine _
    & " , @OUTKA_PKG_NB                                                                 " & vbNewLine _
    & " , @OUTKA_TTL_NB                                                                 " & vbNewLine _
    & " , @OUTKA_TTL_WT                                                                 " & vbNewLine _
    & " , @URGENT_FLG                                                                   " & vbNewLine _
    & " , @REMARK_CHK_FLG                                                               " & vbNewLine _
    & " , @JISYATASYA_KB                                                                " & vbNewLine _
    & " , @HOKOKU_FLG                                                                   " & vbNewLine _
    & " , @HOKOKU_STATE_KB                                                              " & vbNewLine _
    & " , @SAGYO_FILE_STATE_KB                                                          " & vbNewLine _
    & " , @SYS_ENT_DATE                                                                 " & vbNewLine _
    & " , @SYS_ENT_TIME                                                                 " & vbNewLine _
    & " , @SYS_ENT_PGID                                                                 " & vbNewLine _
    & " , @SYS_ENT_USER                                                                 " & vbNewLine _
    & " , @SYS_UPD_DATE                                                                 " & vbNewLine _
    & " , @SYS_UPD_TIME                                                                 " & vbNewLine _
    & " , @SYS_UPD_PGID                                                                 " & vbNewLine _
    & " , @SYS_UPD_USER                                                                 " & vbNewLine _
    & " , @SYS_DEL_FLG                                                                  " & vbNewLine _
    & " )                                                                               " & vbNewLine

#End Region

#Region "出荷ピック明細登録"

    ''' <summary>
    ''' 出荷ピック明細登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TC_PICK_DTL As String _
    = "INSERT INTO $LM_TRN$..TC_PICK_DTL (                                                       " & vbNewLine _
    & "  NRS_BR_CD                                                                               " & vbNewLine _
    & ", OUTKA_NO_L                                                                              " & vbNewLine _
    & ", TOU_NO                                                                                  " & vbNewLine _
    & ", SITU_NO                                                                                 " & vbNewLine _
    & ", PICK_SEQ                                                                                " & vbNewLine _
    & ", OUTKA_NO_M                                                                              " & vbNewLine _
    & ", OUTKA_NO_S                                                                              " & vbNewLine _
    & ", PICK_STATE_KB                                                                           " & vbNewLine _
    & ", GOODS_CD_NRS                                                                            " & vbNewLine _
    & ", GOODS_CD_CUST                                                                           " & vbNewLine _
    & ", GOODS_NM_NRS                                                                            " & vbNewLine _
    & ", LOT_NO                                                                                  " & vbNewLine _
    & ", IRIME                                                                                   " & vbNewLine _
    & ", IRIME_UT                                                                                " & vbNewLine _
    & ", NB_UT                                                                                   " & vbNewLine _
    & ", PKG_NB                                                                                  " & vbNewLine _
    & ", PKG_UT                                                                                  " & vbNewLine _
    & ", ZONE_CD                                                                                 " & vbNewLine _
    & ", LOCA                                                                                    " & vbNewLine _
    & ", OUT_NB                                                                                  " & vbNewLine _
    & ", OUT_KONSU                                                                               " & vbNewLine _
    & ", OUT_HASU                                                                                " & vbNewLine _
    & ", OUT_ZAN_NB                                                                              " & vbNewLine _
    & ", OUT_ZAN_KONSU                                                                           " & vbNewLine _
    & ", OUT_ZAN_HASU                                                                            " & vbNewLine _
    & ", OUT_QT                                                                                  " & vbNewLine _
    & ", OUT_ZAN_QT                                                                              " & vbNewLine _
    & ", GOODS_COND_KB1                                                                          " & vbNewLine _
    & ", GOODS_COND_KB2                                                                          " & vbNewLine _
    & ", GOODS_COND_KB3                                                                          " & vbNewLine _
    & ", LT_DATE                                                                                 " & vbNewLine _
    & ", GOODS_CRT_DATE                                                                          " & vbNewLine _
    & ", SPD_KB                                                                                  " & vbNewLine _
    & ", OFB_KB                                                                                  " & vbNewLine _
    & ", RSV_NO                                                                                  " & vbNewLine _
    & ", SERIAL_NO                                                                               " & vbNewLine _
    & ", INKA_NO_L                                                                               " & vbNewLine _
    & ", INKA_NO_M                                                                               " & vbNewLine _
    & ", INKA_NO_S                                                                               " & vbNewLine _
    & ", ZAI_REC_NO                                                                              " & vbNewLine _
    & ", INKA_DATE                                                                               " & vbNewLine _
    & ", REMARK                                                                                  " & vbNewLine _
    & ", REMARK_OUT                                                                              " & vbNewLine _
    & ", JISYATASYA_KB                                                                           " & vbNewLine _
    & ", SYS_ENT_DATE                                                                            " & vbNewLine _
    & ", SYS_ENT_TIME                                                                            " & vbNewLine _
    & ", SYS_ENT_PGID                                                                            " & vbNewLine _
    & ", SYS_ENT_USER                                                                            " & vbNewLine _
    & ", SYS_UPD_DATE                                                                            " & vbNewLine _
    & ", SYS_UPD_TIME                                                                            " & vbNewLine _
    & ", SYS_UPD_PGID                                                                            " & vbNewLine _
    & ", SYS_UPD_USER                                                                            " & vbNewLine _
    & ", SYS_DEL_FLG                                                                             " & vbNewLine _
    & " )                                                                                        " & vbNewLine _
    & "SELECT                                                                                    " & vbNewLine _
    & " OUTS.NRS_BR_CD                                             AS NRS_BR_CD                  " & vbNewLine _
    & ",OUTS.OUTKA_NO_L                                            AS OUTKA_NO_L                 " & vbNewLine _
    & ",OUTS.TOU_NO                                                AS TOU_NO                     " & vbNewLine _
    & ",OUTS.SITU_NO                                               AS SITU_NO                    " & vbNewLine _
    & ",(SELECT MAX(PICK_SEQ)                                                                    " & vbNewLine _
    & "        FROM $LM_TRN$..TC_PICK_HEAD                                                       " & vbNewLine _
    & "        WHERE TC_PICK_HEAD.NRS_BR_CD  = OUTL.NRS_BR_CD                                    " & vbNewLine _
    & "          AND TC_PICK_HEAD.OUTKA_NO_L = OUTL.OUTKA_NO_L                                   " & vbNewLine _
    & "        GROUP BY TC_PICK_HEAD.NRS_BR_CD,TC_PICK_HEAD.OUTKA_NO_L                           " & vbNewLine _
    & "       )                                                    AS PICK_SEQ                   " & vbNewLine _
    & ",OUTS.OUTKA_NO_M                                            AS OUTKA_NO_M                 " & vbNewLine _
    & ",OUTS.OUTKA_NO_S                                            AS OUTKA_NO_S                 " & vbNewLine _
    & ",CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                         " & vbNewLine _
    & "      WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                         " & vbNewLine _
    & "      ELSE '00'                                                                           " & vbNewLine _
    & " END                                                        AS PICK_STATE_KB              " & vbNewLine _
    & ",GDS.GOODS_CD_NRS                                           AS GOODS_CD_NRS               " & vbNewLine _
    & ",GDS.GOODS_CD_CUST                                          AS GOODS_CD_CUST              " & vbNewLine _
    & ",$GOODS_NM$                                                 AS GOODS_NM_NRS               " & vbNewLine _
    & ",OUTS.LOT_NO                                                AS LOT_NO                     " & vbNewLine _
    & ",OUTM.IRIME                                                 AS IRIME                      " & vbNewLine _
    & ",OUTM.IRIME_UT                                              AS IRIME_UT                   " & vbNewLine _
    & ",GDS.NB_UT                                                  AS NB_UT                      " & vbNewLine _
    & ",GDS.PKG_NB                                                 AS PKG_NB                     " & vbNewLine _
    & ",GDS.PKG_UT                                                 AS PKG_UT                     " & vbNewLine _
    & ",OUTS.ZONE_CD                                               AS ZONE_CD                    " & vbNewLine _
    & ",OUTS.LOCA                                                  AS LOCA                       " & vbNewLine _
    & ",OUTS.ALCTD_NB                                              AS OUT_NB                     " & vbNewLine _
    & ",CONVERT(NUMERIC(10,0),CONVERT(int,OUTS.ALCTD_NB) / CONVERT(int,GDS.PKG_NB)) AS OUT_KONSU " & vbNewLine _
    & ",OUTS.ALCTD_NB % GDS.PKG_NB                                 AS OUT_HASU                   " & vbNewLine _
    & ",OUTS.ALCTD_CAN_NB                                          AS OUT_ZAN_NB                 " & vbNewLine _
    & ",CONVERT(NUMERIC(10,0),CONVERT(int,OUTS.ALCTD_CAN_NB) / CONVERT(int,GDS.PKG_NB)) AS OUT_ZAN_KONSU " & vbNewLine _
    & ",OUTS.ALCTD_CAN_NB % GDS.PKG_NB                             AS OUT_ZAN_HASU               " & vbNewLine _
    & ",OUTS.ALCTD_QT                                              AS OUT_QT                     " & vbNewLine _
    & ",ZAI.ALLOC_CAN_QT                                           AS OUT_ZAN_QT                 " & vbNewLine _
    & ",ZAI.GOODS_COND_KB_1                                        AS GOODS_COND_KB1             " & vbNewLine _
    & ",ZAI.GOODS_COND_KB_2                                        AS GOODS_COND_KB2             " & vbNewLine _
    & ",ZAI.GOODS_COND_KB_3                                        AS GOODS_COND_KB3             " & vbNewLine _
    & ",ZAI.LT_DATE                                                AS LT_DATE                    " & vbNewLine _
    & ",ZAI.GOODS_CRT_DATE                                         AS GOODS_CRT_DATE             " & vbNewLine _
    & ",ZAI.SPD_KB                                                 AS SPD_KB                     " & vbNewLine _
    & ",ZAI.OFB_KB                                                 AS OFB_KB                     " & vbNewLine _
    & ",ZAI.RSV_NO                                                 AS RSV_NO                     " & vbNewLine _
    & ",ZAI.SERIAL_NO                                              AS SERIAL_NO                  " & vbNewLine _
    & ",OUTS.INKA_NO_L                                             AS INKA_NO_L                  " & vbNewLine _
    & ",OUTS.INKA_NO_M                                             AS INKA_NO_M                  " & vbNewLine _
    & ",OUTS.INKA_NO_S                                             AS INKA_NO_S                  " & vbNewLine _
    & ",OUTS.ZAI_REC_NO                                            AS ZAI_REC_NO                 " & vbNewLine _
    & ",CASE WHEN BINL.INKA_STATE_KB < '50' THEN ISNULL(BINL.INKA_DATE, '')                      " & vbNewLine _
    & "      ELSE ISNULL(ZAI.INKO_DATE, '')                                                      " & vbNewLine _
    & " END                                                        AS INKA_DATE                  " & vbNewLine _
    & ",ZAI.REMARK                                                 AS REMARK                     " & vbNewLine _
    & ",ZAI.REMARK_OUT                                             AS REMARK_OUT                 " & vbNewLine _
    & ",ISNULL(TOUSITU.JISYATASYA_KB,'')                           AS JISYATASYA_KB              " & vbNewLine _
    & ",@SYS_ENT_DATE                                                                            " & vbNewLine _
    & ",@SYS_ENT_TIME                                                                            " & vbNewLine _
    & ",@SYS_ENT_PGID                                                                            " & vbNewLine _
    & ",@SYS_ENT_USER                                                                            " & vbNewLine _
    & ",@SYS_UPD_DATE                                                                            " & vbNewLine _
    & ",@SYS_UPD_TIME                                                                            " & vbNewLine _
    & ",@SYS_UPD_PGID                                                                            " & vbNewLine _
    & ",@SYS_UPD_USER                                                                            " & vbNewLine _
    & ",'0'                                                        AS SYS_DEL_FLG                " & vbNewLine _
    & "FROM $LM_TRN$..C_OUTKA_L OUTL                                                             " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                                        " & vbNewLine _
    & "ON  OUTM.NRS_BR_CD   = OUTL.NRS_BR_CD                                                     " & vbNewLine _
    & "AND OUTM.OUTKA_NO_L  = OUTL.OUTKA_NO_L                                                    " & vbNewLine _
    & "AND OUTM.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                                        " & vbNewLine _
    & "ON  OUTS.NRS_BR_CD  = OUTM.NRS_BR_CD                                                      " & vbNewLine _
    & "AND OUTS.OUTKA_NO_L = OUTM.OUTKA_NO_L                                                     " & vbNewLine _
    & "AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                     " & vbNewLine _
    & "AND OUTS.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_GOODS GDS                                                           " & vbNewLine _
    & "ON  GDS.NRS_BR_CD    = OUTM.NRS_BR_CD                                                     " & vbNewLine _
    & "AND GDS.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                                  " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GDSDTL74                                              " & vbNewLine _
    & "ON  GDSDTL74.NRS_BR_CD    = GDS.NRS_BR_CD                                                 " & vbNewLine _
    & "AND GDSDTL74.GOODS_CD_NRS = GDS.GOODS_CD_NRS                                              " & vbNewLine _
    & "AND GDSDTL74.SUB_KB = '74'                                                                " & vbNewLine _
    & "AND GDSDTL74.SET_NAIYO <> ''                                                              " & vbNewLine _
    & "AND GDSDTL74.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                                         " & vbNewLine _
    & "ON  ZAI.NRS_BR_CD  = OUTS.NRS_BR_CD                                                       " & vbNewLine _
    & "AND ZAI.ZAI_REC_NO = OUTS.ZAI_REC_NO                                                      " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..B_INKA_L BINL                                                         " & vbNewLine _
    & "ON   BINL.NRS_BR_CD = OUTS.NRS_BR_CD                                                      " & vbNewLine _
    & "AND  BINL.INKA_NO_L = OUTS.INKA_NO_L                                                      " & vbNewLine _
    & "AND  BINL.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_TOU_SITU TOUSITU                                                    " & vbNewLine _
    & "ON  TOUSITU.WH_CD   = OUTL.WH_CD                                                          " & vbNewLine _
    & "AND TOUSITU.TOU_NO  = OUTS.TOU_NO                                                         " & vbNewLine _
    & "AND TOUSITU.SITU_NO = OUTS.SITU_NO                                                        " & vbNewLine _
    & "$JOIN_EDI_M$" _
    & "WHERE                                                                                     " & vbNewLine _
    & "    OUTL.SYS_DEL_FLG = '0'                                                            	 " & vbNewLine _
    & "AND OUTL.NRS_BR_CD  = @NRS_BR_CD                                                          " & vbNewLine _
    & "AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                                         " & vbNewLine

    Private Const SQL_INSERT_TC_PICK_DTL_GOODS_NM_1 As String _
    = "ISNULL(GDSDTL74.SET_NAIYO, GDS.GOODS_NM_1)"

    Private Const SQL_INSERT_TC_PICK_DTL_GOODS_NM_2 As String _
    = " CASE WHEN ISNULL(EDIM.FREE_C04, '') = '' THEN ISNULL(GDSDTL74.SET_NAIYO, GDS.GOODS_NM_1) " & vbNewLine _
    & "      ELSE EDIM.FREE_C04                                      " & vbNewLine _
    & " END       "

    Private Const SQL_INSERT_TC_PICK_DTL_JOIN_EDI_M As String _
    = "LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                                        " & vbNewLine _
    & "ON  EDIM.NRS_BR_CD = OUTM.NRS_BR_CD                                          " & vbNewLine _
    & "AND EDIM.OUTKA_CTL_NO = OUTM.OUTKA_NO_L                                      " & vbNewLine _
    & "AND EDIM.OUTKA_CTL_NO_CHU = OUTM.OUTKA_NO_M                                  " & vbNewLine _
    & "AND EDIM.SYS_DEL_FLG = '0'                                                   " & vbNewLine


    ''' <summary>
    ''' 出荷ピック明細登録(全項目プレースホルダ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TC_PICK_DTL_PLACEHOLDER As String _
    = "INSERT INTO $LM_TRN$..TC_PICK_DTL (                                              " & vbNewLine _
    & "  NRS_BR_CD                                                                      " & vbNewLine _
    & ", OUTKA_NO_L                                                                     " & vbNewLine _
    & ", TOU_NO                                                                         " & vbNewLine _
    & ", SITU_NO                                                                        " & vbNewLine _
    & ", PICK_SEQ                                                                       " & vbNewLine _
    & ", OUTKA_NO_M                                                                     " & vbNewLine _
    & ", OUTKA_NO_S                                                                     " & vbNewLine _
    & ", PICK_STATE_KB                                                                  " & vbNewLine _
    & ", GOODS_CD_NRS                                                                   " & vbNewLine _
    & ", GOODS_CD_CUST                                                                  " & vbNewLine _
    & ", GOODS_NM_NRS                                                                   " & vbNewLine _
    & ", LOT_NO                                                                         " & vbNewLine _
    & ", IRIME                                                                          " & vbNewLine _
    & ", IRIME_UT                                                                       " & vbNewLine _
    & ", NB_UT                                                                          " & vbNewLine _
    & ", PKG_NB                                                                         " & vbNewLine _
    & ", PKG_UT                                                                         " & vbNewLine _
    & ", ZONE_CD                                                                        " & vbNewLine _
    & ", LOCA                                                                           " & vbNewLine _
    & ", OUT_NB                                                                         " & vbNewLine _
    & ", OUT_KONSU                                                                      " & vbNewLine _
    & ", OUT_HASU                                                                       " & vbNewLine _
    & ", OUT_ZAN_NB                                                                     " & vbNewLine _
    & ", OUT_ZAN_KONSU                                                                  " & vbNewLine _
    & ", OUT_ZAN_HASU                                                                   " & vbNewLine _
    & ", OUT_QT                                                                         " & vbNewLine _
    & ", OUT_ZAN_QT                                                                     " & vbNewLine _
    & ", GOODS_COND_KB1                                                                 " & vbNewLine _
    & ", GOODS_COND_KB2                                                                 " & vbNewLine _
    & ", GOODS_COND_KB3                                                                 " & vbNewLine _
    & ", LT_DATE                                                                        " & vbNewLine _
    & ", GOODS_CRT_DATE                                                                 " & vbNewLine _
    & ", SPD_KB                                                                         " & vbNewLine _
    & ", OFB_KB                                                                         " & vbNewLine _
    & ", RSV_NO                                                                         " & vbNewLine _
    & ", SERIAL_NO                                                                      " & vbNewLine _
    & ", INKA_NO_L                                                                      " & vbNewLine _
    & ", INKA_NO_M                                                                      " & vbNewLine _
    & ", INKA_NO_S                                                                      " & vbNewLine _
    & ", ZAI_REC_NO                                                                     " & vbNewLine _
    & ", INKA_DATE                                                                      " & vbNewLine _
    & ", REMARK                                                                         " & vbNewLine _
    & ", REMARK_OUT                                                                     " & vbNewLine _
    & ", JISYATASYA_KB                                                                  " & vbNewLine _
    & ", SYS_ENT_DATE                                                                   " & vbNewLine _
    & ", SYS_ENT_TIME                                                                   " & vbNewLine _
    & ", SYS_ENT_PGID                                                                   " & vbNewLine _
    & ", SYS_ENT_USER                                                                   " & vbNewLine _
    & ", SYS_UPD_DATE                                                                   " & vbNewLine _
    & ", SYS_UPD_TIME                                                                   " & vbNewLine _
    & ", SYS_UPD_PGID                                                                   " & vbNewLine _
    & ", SYS_UPD_USER                                                                   " & vbNewLine _
    & ", SYS_DEL_FLG                                                                    " & vbNewLine _
    & " ) VALUES (                                                                      " & vbNewLine _
    & " @NRS_BR_CD                                                                      " & vbNewLine _
    & ",@OUTKA_NO_L                                                                     " & vbNewLine _
    & ",@TOU_NO                                                                         " & vbNewLine _
    & ",@SITU_NO                                                                        " & vbNewLine _
    & ",@PICK_SEQ                                                                       " & vbNewLine _
    & ",@OUTKA_NO_M                                                                     " & vbNewLine _
    & ",@OUTKA_NO_S                                                                     " & vbNewLine _
    & ",@PICK_STATE_KB                                                                  " & vbNewLine _
    & ",@GOODS_CD_NRS                                                                   " & vbNewLine _
    & ",@GOODS_CD_CUST                                                                  " & vbNewLine _
    & ",@GOODS_NM_NRS                                                                   " & vbNewLine _
    & ",@LOT_NO                                                                         " & vbNewLine _
    & ",@IRIME                                                                          " & vbNewLine _
    & ",@IRIME_UT                                                                       " & vbNewLine _
    & ",@NB_UT                                                                          " & vbNewLine _
    & ",@PKG_NB                                                                         " & vbNewLine _
    & ",@PKG_UT                                                                         " & vbNewLine _
    & ",@ZONE_CD                                                                        " & vbNewLine _
    & ",@LOCA                                                                           " & vbNewLine _
    & ",@OUT_NB                                                                         " & vbNewLine _
    & ",@OUT_KONSU                                                                      " & vbNewLine _
    & ",@OUT_HASU                                                                       " & vbNewLine _
    & ",@OUT_ZAN_NB                                                                     " & vbNewLine _
    & ",@OUT_ZAN_KONSU                                                                  " & vbNewLine _
    & ",@OUT_ZAN_HASU                                                                   " & vbNewLine _
    & ",@OUT_QT                                                                         " & vbNewLine _
    & ",@OUT_ZAN_QT                                                                     " & vbNewLine _
    & ",@GOODS_COND_KB1                                                                 " & vbNewLine _
    & ",@GOODS_COND_KB2                                                                 " & vbNewLine _
    & ",@GOODS_COND_KB3                                                                 " & vbNewLine _
    & ",@LT_DATE                                                                        " & vbNewLine _
    & ",@GOODS_CRT_DATE                                                                 " & vbNewLine _
    & ",@SPD_KB                                                                         " & vbNewLine _
    & ",@OFB_KB                                                                         " & vbNewLine _
    & ",@RSV_NO                                                                         " & vbNewLine _
    & ",@SERIAL_NO                                                                      " & vbNewLine _
    & ",@INKA_NO_L                                                                      " & vbNewLine _
    & ",@INKA_NO_M                                                                      " & vbNewLine _
    & ",@INKA_NO_S                                                                      " & vbNewLine _
    & ",@ZAI_REC_NO                                                                     " & vbNewLine _
    & ",@INKA_DATE                                                                      " & vbNewLine _
    & ",@REMARK                                                                         " & vbNewLine _
    & ",@REMARK_OUT                                                                     " & vbNewLine _
    & ",@JISYATASYA_KB                                                                  " & vbNewLine _
    & ",@SYS_ENT_DATE                                                                   " & vbNewLine _
    & ",@SYS_ENT_TIME                                                                   " & vbNewLine _
    & ",@SYS_ENT_PGID                                                                   " & vbNewLine _
    & ",@SYS_ENT_USER                                                                   " & vbNewLine _
    & ",@SYS_UPD_DATE                                                                   " & vbNewLine _
    & ",@SYS_UPD_TIME                                                                   " & vbNewLine _
    & ",@SYS_UPD_PGID                                                                   " & vbNewLine _
    & ",@SYS_UPD_USER                                                                   " & vbNewLine _
    & ",@SYS_DEL_FLG                                                                    " & vbNewLine _
    & " )                                                                               " & vbNewLine

#End Region

#Region "出荷検品ヘッダ登録"

    ''' <summary>
    ''' 出荷検品ヘッダ登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TC_KENPIN_HEAD As String _
    = "INSERT INTO $LM_TRN$..TC_KENPIN_HEAD (                                           " & vbNewLine _
    & "  NRS_BR_CD                                                                      " & vbNewLine _
    & ", OUTKA_NO_L                                                                     " & vbNewLine _
    & ", KENPIN_ATTEND_SEQ                                                              " & vbNewLine _
    & ", KENPIN_ATTEND_STATE_KB                                                         " & vbNewLine _
    & ", WORK_STATE_KB                                                                  " & vbNewLine _
    & ", CANCEL_FLG                                                                     " & vbNewLine _
    & ", CANCEL_TYPE                                                                    " & vbNewLine _
    & ", WH_CD                                                                          " & vbNewLine _
    & ", CUST_CD_L                                                                      " & vbNewLine _
    & ", CUST_CD_M                                                                      " & vbNewLine _
    & ", CUST_NM_L                                                                      " & vbNewLine _
    & ", CUST_NM_M                                                                      " & vbNewLine _
    & ", UNSO_CD                                                                        " & vbNewLine _
    & ", UNSO_NM                                                                        " & vbNewLine _
    & ", UNSO_BR_CD                                                                     " & vbNewLine _
    & ", UNSO_BR_NM                                                                     " & vbNewLine _
    & ", DEST_CD                                                                        " & vbNewLine _
    & ", DEST_NM                                                                        " & vbNewLine _
    & ", DEST_AD_1                                                                      " & vbNewLine _
    & ", DEST_AD_2                                                                      " & vbNewLine _
    & ", DEST_AD_3                                                                      " & vbNewLine _
    & ", DEST_TEL                                                                       " & vbNewLine _
    & ", OUTKO_DATE                                                                     " & vbNewLine _
    & ", OUTKA_PLAN_DATE                                                                " & vbNewLine _
    & ", ARR_PLAN_DATE                                                                  " & vbNewLine _
    & ", ARR_PLAN_TIME                                                                  " & vbNewLine _
    & ", REMARK                                                                         " & vbNewLine _
    & ", REMARK_SIJI                                                                    " & vbNewLine _
    & ", CUST_ORD_NO                                                                    " & vbNewLine _
    & ", BUYER_ORD_NO                                                                   " & vbNewLine _
    & ", OUTKA_PKG_NB                                                                   " & vbNewLine _
    & ", OUTKA_L_PKG_NB                                                                 " & vbNewLine _
    & ", OUTKA_TTL_NB                                                                   " & vbNewLine _
    & ", OUTKA_TTL_WT                                                                   " & vbNewLine _
    & ", ATTEND_FLG                                                                     " & vbNewLine _
    & ", URGENT_FLG                                                                     " & vbNewLine _
    & ", NIHUDA_CHK_STATE_KB                                                            " & vbNewLine _
    & ", NIHUDA_CHK_FLG                                                                 " & vbNewLine _
    & ", REMARK_KENPIN_CHK_FLG                                                          " & vbNewLine _
    & ", REMARK_ATTEND_CHK_FLG                                                          " & vbNewLine _
    & ", NIHUDA_FLG                                                                     " & vbNewLine _
    & ", NIHUDA_YN                                                                      " & vbNewLine _
    & ", KENPIN_SAGYO_FILE_STATE_KB                                                     " & vbNewLine _
    & ", ATTEND_SAGYO_FILE_STATE_KB                                                     " & vbNewLine _
    & ", SYS_ENT_DATE                                                                   " & vbNewLine _
    & ", SYS_ENT_TIME                                                                   " & vbNewLine _
    & ", SYS_ENT_PGID                                                                   " & vbNewLine _
    & ", SYS_ENT_USER                                                                   " & vbNewLine _
    & ", SYS_UPD_DATE                                                                   " & vbNewLine _
    & ", SYS_UPD_TIME                                                                   " & vbNewLine _
    & ", SYS_UPD_PGID                                                                   " & vbNewLine _
    & ", SYS_UPD_USER                                                                   " & vbNewLine _
    & ", SYS_DEL_FLG                                                                    " & vbNewLine _
    & ")                                                                                " & vbNewLine _
    & "SELECT                                                                           " & vbNewLine _
    & " OUTL.NRS_BR_CD                                        AS NRS_BR_CD              " & vbNewLine _
    & ",OUTL.OUTKA_NO_L                                       AS OUTKA_NO_L             " & vbNewLine _
    & ",ISNULL((SELECT MAX(KENPIN_ATTEND_SEQ)                                           " & vbNewLine _
    & "         FROM $LM_TRN$..TC_KENPIN_HEAD                                           " & vbNewLine _
    & "         WHERE TC_KENPIN_HEAD.NRS_BR_CD  = OUTL.NRS_BR_CD                        " & vbNewLine _
    & "           AND TC_KENPIN_HEAD.OUTKA_NO_L = OUTL.OUTKA_NO_L                       " & vbNewLine _
    & "         GROUP BY TC_KENPIN_HEAD.NRS_BR_CD,TC_KENPIN_HEAD.OUTKA_NO_L             " & vbNewLine _
    & "         ),0) + 1                                      AS KENPIN_ATTEND_SEQ      " & vbNewLine _
    & ",'00'                                                  AS KENPIN_ATTEND_STATE_KB " & vbNewLine _
    & ",CASE WHEN                                                                       " & vbNewLine _
    & "  (SELECT COUNT(*)                                                               " & vbNewLine _
    & "   FROM  $LM_TRN$..TC_SAGYO                                                      " & vbNewLine _
    & "   WHERE TC_SAGYO.NRS_BR_CD  = @NRS_BR_CD                                        " & vbNewLine _
    & "     AND TC_SAGYO.OUTKA_NO_L = @OUTKA_NO_L                                       " & vbNewLine _
    & "     AND TC_SAGYO.WORK_SEQ   =ISNULL(                                            " & vbNewLine _
    & " 	     (SELECT MAX(KENPIN_ATTEND_SEQ)                                         " & vbNewLine _
    & "           FROM $LM_TRN$..TC_KENPIN_HEAD                                         " & vbNewLine _
    & "           WHERE TC_KENPIN_HEAD.NRS_BR_CD  = OUTL.NRS_BR_CD                      " & vbNewLine _
    & "             AND TC_KENPIN_HEAD.OUTKA_NO_L = OUTL.OUTKA_NO_L                     " & vbNewLine _
    & "           GROUP BY TC_KENPIN_HEAD.NRS_BR_CD,TC_KENPIN_HEAD.OUTKA_NO_L           " & vbNewLine _
    & "        ),0) + 1                                                                 " & vbNewLine _
    & "  ) > 0                                                                          " & vbNewLine _
    & "  THEN '01'                                                                      " & vbNewLine _
    & "  ELSE '00' END                                        AS WORK_STATE_KB          " & vbNewLine _
    & ",'00'                                                  AS CANCEL_FLG             " & vbNewLine _
    & ",'00'                                                  AS CANCEL_TYPE            " & vbNewLine _
    & ",OUTL.WH_CD                                            AS WH_CD                  " & vbNewLine _
    & ",OUTL.CUST_CD_L                                        AS CUST_CD_L              " & vbNewLine _
    & ",OUTL.CUST_CD_M                                        AS CUST_CD_M              " & vbNewLine _
    & ",ISNULL(CUST.CUST_NM_L,'')                             AS CUST_NM_L              " & vbNewLine _
    & ",ISNULL(CUST.CUST_NM_M,'')                             AS CUST_NM_M              " & vbNewLine _
    & ",UNSO.UNSO_CD                                          AS UNSO_CD                " & vbNewLine _
    & ",ISNULL(UNSC.UNSOCO_NM,'')                             AS UNSO_NM                " & vbNewLine _
    & ",UNSO.UNSO_BR_CD                                       AS UNSO_BR_CD             " & vbNewLine _
    & ",ISNULL(UNSC.UNSOCO_BR_NM,'')                          AS UNSO_BR_NM             " & vbNewLine _
    & ",CASE WHEN OUTL.DEST_KB ='02' THEN ISNULL(EDIL.DEST_CD,'')                       " & vbNewLine _
    & "      ELSE OUTL.DEST_CD                                                          " & vbNewLine _
    & " END                                                   AS DEST_CD                " & vbNewLine _
    & ",CASE WHEN OUTL.DEST_KB ='02' THEN ISNULL(EDIL.DEST_NM,'')                       " & vbNewLine _
    & "      ELSE OUTL.DEST_NM                                                          " & vbNewLine _
    & " END                                                   AS DEST_NM                " & vbNewLine _
    & ",CASE WHEN OUTL.DEST_KB ='01' THEN OUTL.DEST_AD_1                                " & vbNewLine _
    & "      WHEN OUTL.DEST_KB ='02' THEN ISNULL(EDIL.DEST_AD_1,'')                     " & vbNewLine _
    & "      ELSE ISNULL(DEST.AD_1,'')                                                  " & vbNewLine _
    & " END                                                   AS DEST_AD_1              " & vbNewLine _
    & ",CASE WHEN OUTL.DEST_KB ='01' THEN OUTL.DEST_AD_2                                " & vbNewLine _
    & "      WHEN OUTL.DEST_KB ='02' THEN ISNULL(EDIL.DEST_AD_2,'')                     " & vbNewLine _
    & "      ELSE ISNULL(DEST.AD_2,'')                                                  " & vbNewLine _
    & " END                                                   AS DEST_AD_2              " & vbNewLine _
    & ",OUTL.DEST_AD_3                                        AS DEST_AD_3              " & vbNewLine _
    & ",OUTL.DEST_TEL                                         AS DEST_TEL               " & vbNewLine _
    & ",OUTL.OUTKO_DATE                                       AS OUTKO_DATE             " & vbNewLine _
    & ",OUTL.OUTKA_PLAN_DATE                                  AS OUTKA_PLAN_DATE        " & vbNewLine _
    & ",OUTL.ARR_PLAN_DATE                                    AS ARR_PLAN_DATE          " & vbNewLine _
    & ",OUTL.ARR_PLAN_TIME                                    AS ARR_PLAN_TIME          " & vbNewLine _
    & ",OUTL.REMARK                                           AS REMARK                 " & vbNewLine _
    & ",OUTL.WH_SIJI_REMARK                                   AS REMARK_SIJI            " & vbNewLine _
    & ",OUTL.CUST_ORD_NO                                      AS CUST_ORD_NO            " & vbNewLine _
    & ",OUTL.BUYER_ORD_NO                                     AS BUYER_ORD_NO           " & vbNewLine _
    & ",OUTL.OUTKA_PKG_NB                                     AS OUTKA_PKG_NB           " & vbNewLine _
    & ",CASE WHEN CUSTDTL2.SET_NAIYO IS NULL                THEN OUTM.OUTKA_L_PKG_NB    " & vbNewLine _
    & "      WHEN CUSTDTL2.SET_NAIYO >= OUTM.OUTKA_L_PKG_NB THEN OUTM.OUTKA_L_PKG_NB    " & vbNewLine _
    & "      ELSE CUSTDTL2.SET_NAIYO                                                    " & vbNewLine _
    & " END                                                   AS OUTKA_L_PKG_NB         " & vbNewLine _
    & ",OUTM.OUTKA_TTL_NB                                     AS OUTKA_TTL_NB           " & vbNewLine _
    & ",OUTM.OUTKA_TTL_QT                                     AS OUTKA_TTL_WT           " & vbNewLine _
    & ",CASE WHEN CUSTDTL.SET_NAIYO = '1' THEN '01'                                     " & vbNewLine _
    & "      ELSE '00'                                                                  " & vbNewLine _
    & " END                                                   AS ATTEND_FLG             " & vbNewLine _
    & ",OUTL.URGENT_YN                                        AS URGENT_FLG             " & vbNewLine _
    & ",'00'                                                  AS NIHUDA_CHK_STATE_KB    " & vbNewLine _
    & ",ISNULL(UNSC.WH_NIFUDA_SCAN_YN,'')                     AS NIHUDA_CHK_FLG         " & vbNewLine _
    & ",CASE WHEN LEN(OUTL.REMARK + OUTL.WH_SIJI_REMARK) > 0 THEN '00' ELSE '01' END AS REMARK_KENPIN_CHK_FLG " & vbNewLine _
    & ",CASE WHEN LEN(OUTL.REMARK + OUTL.WH_SIJI_REMARK) > 0 THEN '00' ELSE '01' END AS REMARK_ATTEND_CHK_FLG " & vbNewLine _
    & ",OUTL.NIHUDA_FLAG                                      AS NIHUDA_FLG             " & vbNewLine _
    & ",ISNULL(UNSC.NIHUDA_YN,'')                             AS NIHUDA_YN              " & vbNewLine _
    & ",'00'                                                  AS KENPIN_SAGYO_FILE_STATE_KB " & vbNewLine _
    & ",'00'                                                  AS ATTEND_SAGYO_FILE_STATE_KB " & vbNewLine _
    & ",@SYS_ENT_DATE                                                                   " & vbNewLine _
    & ",@SYS_ENT_TIME                                                                   " & vbNewLine _
    & ",@SYS_ENT_PGID                                                                   " & vbNewLine _
    & ",@SYS_ENT_USER                                                                   " & vbNewLine _
    & ",@SYS_UPD_DATE                                                                   " & vbNewLine _
    & ",@SYS_UPD_TIME                                                                   " & vbNewLine _
    & ",@SYS_UPD_PGID                                                                   " & vbNewLine _
    & ",@SYS_UPD_USER                                                                   " & vbNewLine _
    & ",'0'                                                   AS SYS_DEL_FLG            " & vbNewLine _
    & "FROM                                                                             " & vbNewLine _
    & "    $LM_TRN$..C_OUTKA_L OUTL                                                     " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_CUST CUST                                                  " & vbNewLine _
    & "ON  CUST.NRS_BR_CD  = OUTL.NRS_BR_CD                                             " & vbNewLine _
    & "AND CUST.CUST_CD_L  = OUTL.CUST_CD_L                                             " & vbNewLine _
    & "AND CUST.CUST_CD_M  = OUTL.CUST_CD_M                                             " & vbNewLine _
    & "AND CUST.CUST_CD_S  = '00'                                                       " & vbNewLine _
    & "AND CUST.CUST_CD_SS = '00'                                                       " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_CUST_DETAILS CUSTDTL                                       " & vbNewLine _
    & "ON  CUSTDTL.NRS_BR_CD   = OUTL.NRS_BR_CD                                         " & vbNewLine _
    & "AND CUSTDTL.CUST_CD     = OUTL.CUST_CD_L                                         " & vbNewLine _
    & "AND CUSTDTL.SUB_KB      = '1M'                                                   " & vbNewLine _
    & "AND CUSTDTL.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_CUST_DETAILS CUSTDTL2                                      " & vbNewLine _
    & "ON  CUSTDTL2.NRS_BR_CD   = OUTL.NRS_BR_CD                                        " & vbNewLine _
    & "AND CUSTDTL2.CUST_CD     = OUTL.CUST_CD_L                                        " & vbNewLine _
    & "AND CUSTDTL2.SUB_KB      = '21'                                                  " & vbNewLine _
    & "AND CUSTDTL2.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
    & "INNER JOIN                                                                       " & vbNewLine _
    & "    (                                                                            " & vbNewLine _
    & "     SELECT                                                                      " & vbNewLine _
    & "		 C_OUTKA_M.NRS_BR_CD                                                        " & vbNewLine _
    & "		,C_OUTKA_M.OUTKA_NO_L                                                       " & vbNewLine _
    & "     ,MIN(C_OUTKA_M.OUTKA_NO_M)   AS OUTKA_NO_M                                  " & vbNewLine _
    & "		,SUM(C_OUTKA_M.OUTKA_TTL_NB) AS OUTKA_TTL_NB                                " & vbNewLine _
    & "		,SUM(C_OUTKA_M.OUTKA_TTL_QT) AS OUTKA_TTL_QT                                " & vbNewLine _
    & "     ,SUM(OUTKA_M_PKG_NB)         AS OUTKA_L_PKG_NB                              " & vbNewLine _
    & "     FROM                                                                        " & vbNewLine _
    & "     $LM_TRN$..C_OUTKA_M     C_OUTKA_M                                           " & vbNewLine _
    & "     WHERE                                                                       " & vbNewLine _
    & "         C_OUTKA_M.NRS_BR_CD   = @NRS_BR_CD                                      " & vbNewLine _
    & "     AND C_OUTKA_M.OUTKA_NO_L  = @OUTKA_NO_L                                     " & vbNewLine _
    & "     AND C_OUTKA_M.SYS_DEL_FLG = '0'                                             " & vbNewLine _
    & "     GROUP BY                                                                    " & vbNewLine _
    & "      C_OUTKA_M.NRS_BR_CD                                                        " & vbNewLine _
    & "     ,C_OUTKA_M.OUTKA_NO_L                                                       " & vbNewLine _
    & "    ) OUTM                                                                       " & vbNewLine _
    & "ON                                                                               " & vbNewLine _
    & "    OUTM.NRS_BR_CD  = OUTL.NRS_BR_CD                                             " & vbNewLine _
    & "AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                            " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_DEST DEST                                                  " & vbNewLine _
    & "ON  OUTL.NRS_BR_CD = DEST.NRS_BR_CD                                              " & vbNewLine _
    & "AND OUTL.CUST_CD_L = DEST.CUST_CD_L                                              " & vbNewLine _
    & "AND OUTL.DEST_CD   = DEST.DEST_CD                                                " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..F_UNSO_L UNSO                                                " & vbNewLine _
    & "ON  UNSO.NRS_BR_CD    = OUTL.NRS_BR_CD                                           " & vbNewLine _
    & "AND UNSO.INOUTKA_NO_L = OUTL.OUTKA_NO_L                                          " & vbNewLine _
    & "AND UNSO.MOTO_DATA_KB = '20'                                                     " & vbNewLine _
    & "AND UNSO.SYS_DEL_FLG  = '0'                                                      " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_UNSOCO UNSC                                                " & vbNewLine _
    & "ON  UNSC.NRS_BR_CD    = OUTL.NRS_BR_CD                                           " & vbNewLine _
    & "AND UNSC.UNSOCO_CD    = UNSO.UNSO_CD                                             " & vbNewLine _
    & "AND UNSC.UNSOCO_BR_CD = UNSO.UNSO_BR_CD                                          " & vbNewLine _
    & "LEFT JOIN                                                                        " & vbNewLine _
    & "(                                                                                " & vbNewLine _
    & "     SELECT                                                                      " & vbNewLine _
    & "            NRS_BR_CD                                                            " & vbNewLine _
    & "          , EDI_CTL_NO                                                           " & vbNewLine _
    & "          , OUTKA_CTL_NO                                                         " & vbNewLine _
    & "      FROM (                                                                     " & vbNewLine _
    & "             SELECT                                                              " & vbNewLine _
    & "                    EDIOUTL.NRS_BR_CD                                            " & vbNewLine _
    & "                  , EDIOUTL.EDI_CTL_NO                                           " & vbNewLine _
    & "                  , EDIOUTL.OUTKA_CTL_NO                                         " & vbNewLine _
    & "                  , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                   " & vbNewLine _
    & "                    ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD       " & vbNewLine _
    & "                                                       , EDIOUTL.OUTKA_CTL_NO    " & vbNewLine _
    & "                                                ORDER BY EDIOUTL.NRS_BR_CD       " & vbNewLine _
    & "                                                       , EDIOUTL.EDI_CTL_NO      " & vbNewLine _
    & "                                           )                                     " & vbNewLine _
    & "                    END AS IDX                                                   " & vbNewLine _
    & "              FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                                " & vbNewLine _
    & "             WHERE EDIOUTL.SYS_DEL_FLG    = '0'                                  " & vbNewLine _
    & "               AND EDIOUTL.NRS_BR_CD      = @NRS_BR_CD                           " & vbNewLine _
    & "               AND EDIOUTL.OUTKA_CTL_NO   = @OUTKA_NO_L                          " & vbNewLine _
    & "           ) EBASE                                                               " & vbNewLine _
    & "     WHERE EBASE.IDX = 1                                                         " & vbNewLine _
    & ") TOPEDI                                                                         " & vbNewLine _
    & "ON  TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                         " & vbNewLine _
    & "AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                        " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                            " & vbNewLine _
    & "ON  EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                           " & vbNewLine _
    & "AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                          " & vbNewLine _
    & "WHERE                                                                            " & vbNewLine _
    & "    OUTL.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
    & "AND OUTL.NRS_BR_CD  = @NRS_BR_CD                                                 " & vbNewLine _
    & "AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                                " & vbNewLine

#End Region

#Region "出荷検品明細登録"

    ''' <summary>
    ''' 出荷検品明細登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TC_KENPIN_DTL As String _
    = "INSERT INTO $LM_TRN$..TC_KENPIN_DTL (                                            " & vbNewLine _
    & "  NRS_BR_CD                                                                      " & vbNewLine _
    & ", OUTKA_NO_L                                                                     " & vbNewLine _
    & ", KENPIN_ATTEND_SEQ                                                              " & vbNewLine _
    & ", OUTKA_NO_M                                                                     " & vbNewLine _
    & ", OUTKA_NO_S                                                                     " & vbNewLine _
    & ", KENPIN_DTL_SEQ                                                                 " & vbNewLine _
    & ", KENPIN_STATE_KB                                                                " & vbNewLine _
    & ", ATTEND_STATE_KB                                                                " & vbNewLine _
    & ", GOODS_CD_NRS                                                                   " & vbNewLine _
    & ", GOODS_CD_CUST                                                                  " & vbNewLine _
    & ", GOODS_NM_NRS                                                                   " & vbNewLine _
    & ", LOT_NO                                                                         " & vbNewLine _
    & ", IRIME                                                                          " & vbNewLine _
    & ", IRIME_UT                                                                       " & vbNewLine _
    & ", NB_UT                                                                          " & vbNewLine _
    & ", PKG_NB                                                                         " & vbNewLine _
    & ", PKG_UT                                                                         " & vbNewLine _
    & ", TOU_NO                                                                         " & vbNewLine _
    & ", SITU_NO                                                                        " & vbNewLine _
    & ", ZONE_CD                                                                        " & vbNewLine _
    & ", LOCA                                                                           " & vbNewLine _
    & ", OUT_NB                                                                         " & vbNewLine _
    & ", OUT_KONSU                                                                      " & vbNewLine _
    & ", OUT_HASU                                                                       " & vbNewLine _
    & ", OUT_ZAN_NB                                                                     " & vbNewLine _
    & ", OUT_ZAN_KONSU                                                                  " & vbNewLine _
    & ", OUT_ZAN_HASU                                                                   " & vbNewLine _
    & ", OUT_QT                                                                         " & vbNewLine _
    & ", OUT_ZAN_QT                                                                     " & vbNewLine _
    & ", GOODS_COND_KB1                                                                 " & vbNewLine _
    & ", GOODS_COND_KB2                                                                 " & vbNewLine _
    & ", GOODS_COND_KB3                                                                 " & vbNewLine _
    & ", LT_DATE                                                                        " & vbNewLine _
    & ", GOODS_CRT_DATE                                                                 " & vbNewLine _
    & ", SPD_KB                                                                         " & vbNewLine _
    & ", OFB_KB                                                                         " & vbNewLine _
    & ", RSV_NO                                                                         " & vbNewLine _
    & ", SERIAL_NO                                                                      " & vbNewLine _
    & ", INKA_NO_L                                                                      " & vbNewLine _
    & ", INKA_NO_M                                                                      " & vbNewLine _
    & ", INKA_NO_S                                                                      " & vbNewLine _
    & ", ZAI_REC_NO                                                                     " & vbNewLine _
    & ", INKA_DATE                                                                      " & vbNewLine _
    & ", REMARK                                                                         " & vbNewLine _
    & ", REMARK_OUT                                                                     " & vbNewLine _
    & ", JISYATASYA_KB                                                                  " & vbNewLine _
    & ", SYS_ENT_DATE                                                                   " & vbNewLine _
    & ", SYS_ENT_TIME                                                                   " & vbNewLine _
    & ", SYS_ENT_PGID                                                                   " & vbNewLine _
    & ", SYS_ENT_USER                                                                   " & vbNewLine _
    & ", SYS_UPD_DATE                                                                   " & vbNewLine _
    & ", SYS_UPD_TIME                                                                   " & vbNewLine _
    & ", SYS_UPD_PGID                                                                   " & vbNewLine _
    & ", SYS_UPD_USER                                                                   " & vbNewLine _
    & ", SYS_DEL_FLG                                                                    " & vbNewLine _
    & ")                                                                                " & vbNewLine _
    & "SELECT                                                                           " & vbNewLine _
    & " OUTS.NRS_BR_CD                                             AS NRS_BR_CD         " & vbNewLine _
    & ",OUTS.OUTKA_NO_L                                            AS OUTKA_NO_L        " & vbNewLine _
    & ",(SELECT MAX(KENPIN_ATTEND_SEQ)                                                  " & vbNewLine _
    & "         FROM $LM_TRN$..TC_KENPIN_HEAD                                           " & vbNewLine _
    & "         WHERE TC_KENPIN_HEAD.NRS_BR_CD  = OUTL.NRS_BR_CD                        " & vbNewLine _
    & "           AND TC_KENPIN_HEAD.OUTKA_NO_L = OUTL.OUTKA_NO_L                       " & vbNewLine _
    & "         GROUP BY TC_KENPIN_HEAD.NRS_BR_CD,TC_KENPIN_HEAD.OUTKA_NO_L             " & vbNewLine _
    & "         )                                                 AS KENPIN_ATTEND_SEQ  " & vbNewLine _
    & ",OUTS.OUTKA_NO_M                                           AS OUTKA_NO_M         " & vbNewLine _
    & ",OUTS.OUTKA_NO_S                                           AS OUTKA_NO_S         " & vbNewLine _
    & ",1                                                         AS KENPIN_DTL_SEQ     " & vbNewLine _
    & ",CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                " & vbNewLine _
    & "      WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                " & vbNewLine _
    & "      ELSE '00'                                                                  " & vbNewLine _
    & " END                                                       AS KENPIN_STATE_KB    " & vbNewLine _
    & ",CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                " & vbNewLine _
    & "      WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                " & vbNewLine _
    & "      ELSE '00'                                                                  " & vbNewLine _
    & " END                                                       AS ATTEND_STATE_KB    " & vbNewLine _
    & ",GDS.GOODS_CD_NRS                                          AS GOODS_CD_NRS       " & vbNewLine _
    & ",GDS.GOODS_CD_CUST                                         AS GOODS_CD_CUST      " & vbNewLine _
    & ",$GOODS_NM$                                                AS GOODS_NM_NRS       " & vbNewLine _
    & ",OUTS.LOT_NO                                               AS LOT_NO             " & vbNewLine _
    & ",OUTM.IRIME                                                AS IRIME              " & vbNewLine _
    & ",OUTM.IRIME_UT                                             AS IRIME_UT           " & vbNewLine _
    & ",GDS.NB_UT                                                 AS NB_UT              " & vbNewLine _
    & ",GDS.PKG_NB                                                AS PKG_NB             " & vbNewLine _
    & ",GDS.PKG_UT                                                AS PKG_UT             " & vbNewLine _
    & ",OUTS.TOU_NO                                               AS TOU_NO             " & vbNewLine _
    & ",OUTS.SITU_NO                                              AS SITU_NO            " & vbNewLine _
    & ",OUTS.ZONE_CD                                              AS ZONE_CD            " & vbNewLine _
    & ",OUTS.LOCA                                                 AS LOCA               " & vbNewLine _
    & ",OUTS.ALCTD_NB                                             AS OUT_NB             " & vbNewLine _
    & ",CONVERT(NUMERIC(10,0),CONVERT(int,OUTS.ALCTD_NB) / CONVERT(int,GDS.PKG_NB)) AS OUT_KONSU " & vbNewLine _
    & ",OUTS.ALCTD_NB % GDS.PKG_NB                                AS OUT_HASU           " & vbNewLine _
    & ",OUTS.ALCTD_CAN_NB                                         AS OUT_ZAN_NB         " & vbNewLine _
    & ",CONVERT(NUMERIC(10,0),CONVERT(int,OUTS.ALCTD_CAN_NB) / CONVERT(int,GDS.PKG_NB)) AS OUT_ZAN_KONSU " & vbNewLine _
    & ",OUTS.ALCTD_CAN_NB % GDS.PKG_NB                            AS OUT_ZAN_HASU       " & vbNewLine _
    & ",OUTS.ALCTD_QT                                             AS OUT_QT             " & vbNewLine _
    & ",ISNULL(ZAI.ALLOC_CAN_QT ,0)                               AS OUT_ZAN_QT         " & vbNewLine _
    & ",ISNULL(ZAI.GOODS_COND_KB_1,'')                            AS GOODS_COND_KB1     " & vbNewLine _
    & ",ISNULL(ZAI.GOODS_COND_KB_2 ,'')                           AS GOODS_COND_KB2     " & vbNewLine _
    & ",ISNULL(ZAI.GOODS_COND_KB_3 ,'')                           AS GOODS_COND_KB3     " & vbNewLine _
    & ",ISNULL(ZAI.LT_DATE ,'')                                   AS LT_DATE            " & vbNewLine _
    & ",ISNULL(ZAI.GOODS_CRT_DATE,'')                             AS GOODS_CRT_DATE     " & vbNewLine _
    & ",ISNULL(ZAI.SPD_KB,'')                                     AS SPD_KB             " & vbNewLine _
    & ",ISNULL(ZAI.OFB_KB,'')                                     AS OFB_KB             " & vbNewLine _
    & ",ISNULL(ZAI.RSV_NO,'')                                     AS RSV_NO             " & vbNewLine _
    & ",ISNULL(ZAI.SERIAL_NO,'')                                  AS SERIAL_NO          " & vbNewLine _
    & ",OUTS.INKA_NO_L                                            AS INKA_NO_L          " & vbNewLine _
    & ",OUTS.INKA_NO_M                                            AS INKA_NO_M          " & vbNewLine _
    & ",OUTS.INKA_NO_S                                            AS INKA_NO_S          " & vbNewLine _
    & ",OUTS.ZAI_REC_NO                                           AS ZAI_REC_NO         " & vbNewLine _
    & ",CASE WHEN BINL.INKA_STATE_KB < '50' THEN ISNULL(BINL.INKA_DATE, '')             " & vbNewLine _
    & "      ELSE ISNULL(ZAI.INKO_DATE, '')                                             " & vbNewLine _
    & " END                                                       AS INKA_DATE          " & vbNewLine _
    & ",ISNULL(ZAI.REMARK,'')                                     AS REMARK             " & vbNewLine _
    & ",ISNULL(ZAI.REMARK_OUT,'')                                 AS REMARK_OUT         " & vbNewLine _
    & ",ISNULL(TOUSITU.JISYATASYA_KB,'')                          AS JISYATASYA_KB      " & vbNewLine _
    & ",@SYS_ENT_DATE                                                                   " & vbNewLine _
    & ",@SYS_ENT_TIME                                                                   " & vbNewLine _
    & ",@SYS_ENT_PGID                                                                   " & vbNewLine _
    & ",@SYS_ENT_USER                                                                   " & vbNewLine _
    & ",@SYS_UPD_DATE                                                                   " & vbNewLine _
    & ",@SYS_UPD_TIME                                                                   " & vbNewLine _
    & ",@SYS_UPD_PGID                                                                   " & vbNewLine _
    & ",@SYS_UPD_USER                                                                   " & vbNewLine _
    & ",'0'                                                       AS SYS_DEL_FLG        " & vbNewLine _
    & "FROM $LM_TRN$..C_OUTKA_L OUTL                                                    " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                               " & vbNewLine _
    & "ON  OUTM.NRS_BR_CD  = OUTL.NRS_BR_CD                                             " & vbNewLine _
    & "AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                            " & vbNewLine _
    & "AND OUTM.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                               " & vbNewLine _
    & "ON  OUTS.NRS_BR_CD  = OUTM.NRS_BR_CD                                             " & vbNewLine _
    & "AND OUTS.OUTKA_NO_L = OUTM.OUTKA_NO_L                                            " & vbNewLine _
    & "AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                            " & vbNewLine _
    & "AND OUTS.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_GOODS GDS                                                  " & vbNewLine _
    & "ON  GDS.NRS_BR_CD    = OUTM.NRS_BR_CD                                            " & vbNewLine _
    & "AND GDS.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                         " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GDSDTL74                                     " & vbNewLine _
    & "ON  GDSDTL74.NRS_BR_CD    = GDS.NRS_BR_CD                                        " & vbNewLine _
    & "AND GDSDTL74.GOODS_CD_NRS = GDS.GOODS_CD_NRS                                     " & vbNewLine _
    & "AND GDSDTL74.SUB_KB = '74'                                                       " & vbNewLine _
    & "AND GDSDTL74.SET_NAIYO <> ''                                                     " & vbNewLine _
    & "AND GDSDTL74.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                                " & vbNewLine _
    & "ON  ZAI.NRS_BR_CD  = OUTS.NRS_BR_CD                                              " & vbNewLine _
    & "AND ZAI.ZAI_REC_NO = OUTS.ZAI_REC_NO                                             " & vbNewLine _
    & "LEFT JOIN $LM_TRN$..B_INKA_L BINL                                                " & vbNewLine _
    & "ON   BINL.NRS_BR_CD = OUTS.NRS_BR_CD                                             " & vbNewLine _
    & "AND  BINL.INKA_NO_L = OUTS.INKA_NO_L                                             " & vbNewLine _
    & "AND  BINL.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
    & "LEFT JOIN $LM_MST$..M_TOU_SITU TOUSITU                                           " & vbNewLine _
    & "ON  TOUSITU.WH_CD   = OUTL.WH_CD                                                 " & vbNewLine _
    & "AND TOUSITU.TOU_NO  = OUTS.TOU_NO                                                " & vbNewLine _
    & "AND TOUSITU.SITU_NO = OUTS.SITU_NO                                               " & vbNewLine _
    & "$JOIN_EDI_M$" _
    & "WHERE                                                                            " & vbNewLine _
    & "    OUTL.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
    & "AND OUTL.NRS_BR_CD  = @NRS_BR_CD                                                 " & vbNewLine _
    & "AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                                " & vbNewLine

    Private Const SQL_INSERT_TC_KENPIN_DTL_GOODS_NM_1 As String _
    = "ISNULL(GDSDTL74.SET_NAIYO, GDS.GOODS_NM_1)"

    Private Const SQL_INSERT_TC_KENPIN_DTL_GOODS_NM_2 As String _
    = " CASE WHEN ISNULL(EDIM.FREE_C04, '') = '' THEN ISNULL(GDSDTL74.SET_NAIYO, GDS.GOODS_NM_1) " & vbNewLine _
    & "      ELSE EDIM.FREE_C04                                      " & vbNewLine _
    & " END       "

    'ADD Start 2022/12/28 アフトン別名出荷対応
    Private Const SQL_INSERT_TC_KENPIN_DTL_GOODS_NM_3 As String _
    = " CASE WHEN ISNULL(EDIM.FREE_C02, '') = '' THEN ISNULL(GDSDTL74.SET_NAIYO, GDS.GOODS_NM_1) " & vbNewLine _
    & "      ELSE EDIM.FREE_C02                                      " & vbNewLine _
    & " END       "
    'ADD End   2022/12/28 アフトン別名出荷対応

    Private Const SQL_INSERT_TC_KENPIN_DTL_JOIN_EDI_M As String _
    = "LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                                        " & vbNewLine _
    & "ON  EDIM.NRS_BR_CD = OUTM.NRS_BR_CD                                          " & vbNewLine _
    & "AND EDIM.OUTKA_CTL_NO = OUTM.OUTKA_NO_L                                      " & vbNewLine _
    & "AND EDIM.OUTKA_CTL_NO_CHU = OUTM.OUTKA_NO_M                                  " & vbNewLine _
    & "AND EDIM.SYS_DEL_FLG = '0'                                                   " & vbNewLine


    ''' <summary>
    ''' 出荷検品明細登録(全項目プレースホルダ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TC_KENPIN_DTL_PLACEHOLDER As String _
    = "INSERT INTO $LM_TRN$..TC_KENPIN_DTL (                                            " & vbNewLine _
    & "  NRS_BR_CD                                                                      " & vbNewLine _
    & ", OUTKA_NO_L                                                                     " & vbNewLine _
    & ", KENPIN_ATTEND_SEQ                                                              " & vbNewLine _
    & ", OUTKA_NO_M                                                                     " & vbNewLine _
    & ", OUTKA_NO_S                                                                     " & vbNewLine _
    & ", KENPIN_DTL_SEQ                                                                 " & vbNewLine _
    & ", KENPIN_STATE_KB                                                                " & vbNewLine _
    & ", ATTEND_STATE_KB                                                                " & vbNewLine _
    & ", GOODS_CD_NRS                                                                   " & vbNewLine _
    & ", GOODS_CD_CUST                                                                  " & vbNewLine _
    & ", GOODS_NM_NRS                                                                   " & vbNewLine _
    & ", LOT_NO                                                                         " & vbNewLine _
    & ", IRIME                                                                          " & vbNewLine _
    & ", IRIME_UT                                                                       " & vbNewLine _
    & ", NB_UT                                                                          " & vbNewLine _
    & ", PKG_NB                                                                         " & vbNewLine _
    & ", PKG_UT                                                                         " & vbNewLine _
    & ", TOU_NO                                                                         " & vbNewLine _
    & ", SITU_NO                                                                        " & vbNewLine _
    & ", ZONE_CD                                                                        " & vbNewLine _
    & ", LOCA                                                                           " & vbNewLine _
    & ", OUT_NB                                                                         " & vbNewLine _
    & ", OUT_KONSU                                                                      " & vbNewLine _
    & ", OUT_HASU                                                                       " & vbNewLine _
    & ", OUT_ZAN_NB                                                                     " & vbNewLine _
    & ", OUT_ZAN_KONSU                                                                  " & vbNewLine _
    & ", OUT_ZAN_HASU                                                                   " & vbNewLine _
    & ", OUT_QT                                                                         " & vbNewLine _
    & ", OUT_ZAN_QT                                                                     " & vbNewLine _
    & ", GOODS_COND_KB1                                                                 " & vbNewLine _
    & ", GOODS_COND_KB2                                                                 " & vbNewLine _
    & ", GOODS_COND_KB3                                                                 " & vbNewLine _
    & ", LT_DATE                                                                        " & vbNewLine _
    & ", GOODS_CRT_DATE                                                                 " & vbNewLine _
    & ", SPD_KB                                                                         " & vbNewLine _
    & ", OFB_KB                                                                         " & vbNewLine _
    & ", RSV_NO                                                                         " & vbNewLine _
    & ", SERIAL_NO                                                                      " & vbNewLine _
    & ", INKA_NO_L                                                                      " & vbNewLine _
    & ", INKA_NO_M                                                                      " & vbNewLine _
    & ", INKA_NO_S                                                                      " & vbNewLine _
    & ", ZAI_REC_NO                                                                     " & vbNewLine _
    & ", INKA_DATE                                                                      " & vbNewLine _
    & ", REMARK                                                                         " & vbNewLine _
    & ", REMARK_OUT                                                                     " & vbNewLine _
    & ", JISYATASYA_KB                                                                  " & vbNewLine _
    & ", SYS_ENT_DATE                                                                   " & vbNewLine _
    & ", SYS_ENT_TIME                                                                   " & vbNewLine _
    & ", SYS_ENT_PGID                                                                   " & vbNewLine _
    & ", SYS_ENT_USER                                                                   " & vbNewLine _
    & ", SYS_UPD_DATE                                                                   " & vbNewLine _
    & ", SYS_UPD_TIME                                                                   " & vbNewLine _
    & ", SYS_UPD_PGID                                                                   " & vbNewLine _
    & ", SYS_UPD_USER                                                                   " & vbNewLine _
    & ", SYS_DEL_FLG                                                                    " & vbNewLine _
    & ") VALUES (                                                                       " & vbNewLine _
    & "  @NRS_BR_CD                                                                     " & vbNewLine _
    & ", @OUTKA_NO_L                                                                    " & vbNewLine _
    & ", @KENPIN_ATTEND_SEQ                                                             " & vbNewLine _
    & ", @OUTKA_NO_M                                                                    " & vbNewLine _
    & ", @OUTKA_NO_S                                                                    " & vbNewLine _
    & ", @KENPIN_DTL_SEQ                                                                " & vbNewLine _
    & ", @KENPIN_STATE_KB                                                               " & vbNewLine _
    & ", @ATTEND_STATE_KB                                                               " & vbNewLine _
    & ", @GOODS_CD_NRS                                                                  " & vbNewLine _
    & ", @GOODS_CD_CUST                                                                 " & vbNewLine _
    & ", @GOODS_NM_NRS                                                                  " & vbNewLine _
    & ", @LOT_NO                                                                        " & vbNewLine _
    & ", @IRIME                                                                         " & vbNewLine _
    & ", @IRIME_UT                                                                      " & vbNewLine _
    & ", @NB_UT                                                                         " & vbNewLine _
    & ", @PKG_NB                                                                        " & vbNewLine _
    & ", @PKG_UT                                                                        " & vbNewLine _
    & ", @TOU_NO                                                                        " & vbNewLine _
    & ", @SITU_NO                                                                       " & vbNewLine _
    & ", @ZONE_CD                                                                       " & vbNewLine _
    & ", @LOCA                                                                          " & vbNewLine _
    & ", @OUT_NB                                                                        " & vbNewLine _
    & ", @OUT_KONSU                                                                     " & vbNewLine _
    & ", @OUT_HASU                                                                      " & vbNewLine _
    & ", @OUT_ZAN_NB                                                                    " & vbNewLine _
    & ", @OUT_ZAN_KONSU                                                                 " & vbNewLine _
    & ", @OUT_ZAN_HASU                                                                  " & vbNewLine _
    & ", @OUT_QT                                                                        " & vbNewLine _
    & ", @OUT_ZAN_QT                                                                    " & vbNewLine _
    & ", @GOODS_COND_KB1                                                                " & vbNewLine _
    & ", @GOODS_COND_KB2                                                                " & vbNewLine _
    & ", @GOODS_COND_KB3                                                                " & vbNewLine _
    & ", @LT_DATE                                                                       " & vbNewLine _
    & ", @GOODS_CRT_DATE                                                                " & vbNewLine _
    & ", @SPD_KB                                                                        " & vbNewLine _
    & ", @OFB_KB                                                                        " & vbNewLine _
    & ", @RSV_NO                                                                        " & vbNewLine _
    & ", @SERIAL_NO                                                                     " & vbNewLine _
    & ", @INKA_NO_L                                                                     " & vbNewLine _
    & ", @INKA_NO_M                                                                     " & vbNewLine _
    & ", @INKA_NO_S                                                                     " & vbNewLine _
    & ", @ZAI_REC_NO                                                                    " & vbNewLine _
    & ", @INKA_DATE                                                                     " & vbNewLine _
    & ", @REMARK                                                                        " & vbNewLine _
    & ", @REMARK_OUT                                                                    " & vbNewLine _
    & ", @JISYATASYA_KB                                                                 " & vbNewLine _
    & ", @SYS_ENT_DATE                                                                  " & vbNewLine _
    & ", @SYS_ENT_TIME                                                                  " & vbNewLine _
    & ", @SYS_ENT_PGID                                                                  " & vbNewLine _
    & ", @SYS_ENT_USER                                                                  " & vbNewLine _
    & ", @SYS_UPD_DATE                                                                  " & vbNewLine _
    & ", @SYS_UPD_TIME                                                                  " & vbNewLine _
    & ", @SYS_UPD_PGID                                                                  " & vbNewLine _
    & ", @SYS_UPD_USER                                                                  " & vbNewLine _
    & ", @SYS_DEL_FLG                                                                   " & vbNewLine _
    & ")                                                                                " & vbNewLine

#End Region

#Region "出荷作業登録"
    Private Const SQL_INSERT_TC_SAGYO As String _
    = " INSERT INTO $LM_TRN$..TC_SAGYO                                                                " & vbNewLine _
    & " (                                                                                             " & vbNewLine _
    & "   NRS_BR_CD                                                                                   " & vbNewLine _
    & "  ,SAGYO_REC_NO                                                                                " & vbNewLine _
    & "  ,OUTKA_NO_L                                                                                  " & vbNewLine _
    & "  ,OUTKA_NO_M                                                                                  " & vbNewLine _
    & "  ,OUTKA_NO_S                                                                                  " & vbNewLine _
    & "  ,WORK_SEQ                                                                                    " & vbNewLine _
    & "  ,SAGYO_STATE1_KB                                                                             " & vbNewLine _
    & "  ,SAGYO_STATE2_KB                                                                             " & vbNewLine _
    & "  ,SAGYO_STATE3_KB                                                                             " & vbNewLine _
    & "  ,WH_CD                                                                                       " & vbNewLine _
    & "  ,GOODS_CD_NRS                                                                                " & vbNewLine _
    & "  ,GOODS_NM_NRS                                                                                " & vbNewLine _
    & "  ,IRIME                                                                                       " & vbNewLine _
    & "  ,IRIME_UT                                                                                    " & vbNewLine _
    & "  ,PKG_NB                                                                                      " & vbNewLine _
    & "  ,PKG_UT                                                                                      " & vbNewLine _
    & "  ,LOT_NO                                                                                      " & vbNewLine _
    & "  ,TOU_NO                                                                                      " & vbNewLine _
    & "  ,SITU_NO                                                                                     " & vbNewLine _
    & "  ,ZONE_CD                                                                                     " & vbNewLine _
    & "  ,LOCA                                                                                        " & vbNewLine _
    & "  ,SAGYO_CD                                                                                    " & vbNewLine _
    & "  ,SAGYO_NM                                                                                    " & vbNewLine _
    & "  ,INV_TANI                                                                                    " & vbNewLine _
    & "  ,KOSU_BAI                                                                                    " & vbNewLine _
    & "  ,SAGYO_NB                                                                                    " & vbNewLine _
    & "  ,REMARK                                                                                      " & vbNewLine _
    & "  ,JISYATASYA_KB                                                                               " & vbNewLine _
    & "  ,IOZS_KB                                                                                     " & vbNewLine _
    & "  ,SYS_ENT_DATE                                                                                " & vbNewLine _
    & "  ,SYS_ENT_TIME                                                                                " & vbNewLine _
    & "  ,SYS_ENT_PGID                                                                                " & vbNewLine _
    & "  ,SYS_ENT_USER                                                                                " & vbNewLine _
    & "  ,SYS_UPD_DATE                                                                                " & vbNewLine _
    & "  ,SYS_UPD_TIME                                                                                " & vbNewLine _
    & "  ,SYS_UPD_PGID                                                                                " & vbNewLine _
    & "  ,SYS_UPD_USER                                                                                " & vbNewLine _
    & "  ,SYS_DEL_FLG                                                                                 " & vbNewLine _
    & " )                                                                                             " & vbNewLine _
    & " VALUES                                                                                        " & vbNewLine _
    & " (                                                                                             " & vbNewLine _
    & "   @NRS_BR_CD                                                                                  " & vbNewLine _
    & "  ,@SAGYO_REC_NO                                                                               " & vbNewLine _
    & "  ,@OUTKA_NO_L                                                                                 " & vbNewLine _
    & "  ,@OUTKA_NO_M                                                                                 " & vbNewLine _
    & "  ,@OUTKA_NO_S                                                                                 " & vbNewLine _
    & "  ,@WORK_SEQ                                                                                   " & vbNewLine _
    & "  ,@SAGYO_STATE1_KB                                                                            " & vbNewLine _
    & "  ,@SAGYO_STATE2_KB                                                                            " & vbNewLine _
    & "  ,@SAGYO_STATE3_KB                                                                            " & vbNewLine _
    & "  ,@WH_CD                                                                                      " & vbNewLine _
    & "  ,@GOODS_CD_NRS                                                                               " & vbNewLine _
    & "  ,@GOODS_NM_NRS                                                                               " & vbNewLine _
    & "  ,@IRIME                                                                                      " & vbNewLine _
    & "  ,@IRIME_UT                                                                                   " & vbNewLine _
    & "  ,@PKG_NB                                                                                     " & vbNewLine _
    & "  ,@PKG_UT                                                                                     " & vbNewLine _
    & "  ,@LOT_NO                                                                                     " & vbNewLine _
    & "  ,@TOU_NO                                                                                     " & vbNewLine _
    & "  ,@SITU_NO                                                                                    " & vbNewLine _
    & "  ,@ZONE_CD                                                                                    " & vbNewLine _
    & "  ,@LOCA                                                                                       " & vbNewLine _
    & "  ,@SAGYO_CD                                                                                   " & vbNewLine _
    & "  ,@SAGYO_NM                                                                                   " & vbNewLine _
    & "  ,@INV_TANI                                                                                   " & vbNewLine _
    & "  ,@KOSU_BAI                                                                                   " & vbNewLine _
    & "  ,@SAGYO_NB                                                                                   " & vbNewLine _
    & "  ,@REMARK                                                                                     " & vbNewLine _
    & "  ,@JISYATASYA_KB                                                                              " & vbNewLine _
    & "  ,@IOZS_KB                                                                                    " & vbNewLine _
    & "  ,@SYS_ENT_DATE                                                                               " & vbNewLine _
    & "  ,@SYS_ENT_TIME                                                                               " & vbNewLine _
    & "  ,@SYS_ENT_PGID                                                                               " & vbNewLine _
    & "  ,@SYS_ENT_USER                                                                               " & vbNewLine _
    & "  ,@SYS_UPD_DATE                                                                               " & vbNewLine _
    & "  ,@SYS_UPD_TIME                                                                               " & vbNewLine _
    & "  ,@SYS_UPD_PGID                                                                               " & vbNewLine _
    & "  ,@SYS_UPD_USER                                                                               " & vbNewLine _
    & "  ,@SYS_DEL_FLG                                                                                " & vbNewLine _
    & " )                                                                                             " & vbNewLine

#End Region

#Region "出荷差分比較登録"

    ''' <summary>
    ''' 出荷差分比較登録
    ''' </summary>
    Private Const SQL_INSERT_TC_DIFF_COMPARISON As String _
    = "INSERT INTO $LM_TRN$..TC_DIFF_COMPARISON (    " & vbNewLine _
    & "     NRS_BR_CD                                " & vbNewLine _
    & "    ,OUTKA_NO_L                               " & vbNewLine _
    & "    ,OUTKA_SEQ                                " & vbNewLine _
    & "    ,DETAILS_SEQ                              " & vbNewLine _
    & "    ,ITEM_NAME                                " & vbNewLine _
    & "    ,BEFORE_VALUE                             " & vbNewLine _
    & "    ,AFTER_VALUE                              " & vbNewLine _
    & "    ,GOODS_CHANGE_FLG                         " & vbNewLine _
    & "    ,GOODS_NM                                 " & vbNewLine _
    & "    ,SYS_ENT_DATE                             " & vbNewLine _
    & "    ,SYS_ENT_TIME                             " & vbNewLine _
    & "    ,SYS_ENT_PGID                             " & vbNewLine _
    & "    ,SYS_ENT_USER                             " & vbNewLine _
    & "    ,SYS_UPD_DATE                             " & vbNewLine _
    & "    ,SYS_UPD_TIME                             " & vbNewLine _
    & "    ,SYS_UPD_PGID                             " & vbNewLine _
    & "    ,SYS_UPD_USER                             " & vbNewLine _
    & "    ,SYS_DEL_FLG                              " & vbNewLine _
    & ") VALUES (                                    " & vbNewLine _
    & "     @NRS_BR_CD                               " & vbNewLine _
    & "    ,@OUTKA_NO_L                              " & vbNewLine _
    & "    ,@OUTKA_SEQ                               " & vbNewLine _
    & "    ,@DETAILS_SEQ                             " & vbNewLine _
    & "    ,@ITEM_NAME                               " & vbNewLine _
    & "    ,@BEFORE_VALUE                            " & vbNewLine _
    & "    ,@AFTER_VALUE                             " & vbNewLine _
    & "    ,@GOODS_CHANGE_FLG                        " & vbNewLine _
    & "    ,@GOODS_NM                                " & vbNewLine _
    & "    ,@SYS_ENT_DATE                            " & vbNewLine _
    & "    ,@SYS_ENT_TIME                            " & vbNewLine _
    & "    ,@SYS_ENT_PGID                            " & vbNewLine _
    & "    ,@SYS_ENT_USER                            " & vbNewLine _
    & "    ,@SYS_UPD_DATE                            " & vbNewLine _
    & "    ,@SYS_UPD_TIME                            " & vbNewLine _
    & "    ,@SYS_UPD_PGID                            " & vbNewLine _
    & "    ,@SYS_UPD_USER                            " & vbNewLine _
    & "    ,@SYS_DEL_FLG                             " & vbNewLine _
    & ")                                             " & vbNewLine

#End Region

#End Region

#Region "Update"

#Region "出荷ピック キャンセル"
    Public Const SQL_UPDATE_TC_PICK_HEAD_CANCEL As String _
    = "UPDATE $LM_TRN$..TC_PICK_HEAD              " & vbNewLine _
    & "SET                                        " & vbNewLine _
    & " CANCEL_FLG   = '01'                       " & vbNewLine _
    & ",CANCEL_TYPE  = @CANCEL_TYPE               " & vbNewLine _
    & ",SYS_UPD_DATE = @SYS_UPD_DATE              " & vbNewLine _
    & ",SYS_UPD_TIME = @SYS_UPD_TIME              " & vbNewLine _
    & ",SYS_UPD_PGID = @SYS_UPD_PGID              " & vbNewLine _
    & ",SYS_UPD_USER = @SYS_UPD_USER              " & vbNewLine _
    & "WHERE                                      " & vbNewLine _
    & "    NRS_BR_CD  = @NRS_BR_CD                " & vbNewLine _
    & "AND OUTKA_NO_L = @OUTKA_NO_L               " & vbNewLine _
    & "AND TOU_NO     = @TOU_NO                   " & vbNewLine _
    & "AND SITU_NO    = @SITU_NO                  " & vbNewLine _
    & "AND PICK_SEQ   = @PICK_SEQ                 " & vbNewLine
#End Region

#Region "出荷ピック 明細キャンセル"
    Public Const SQL_UPDATE_TC_PICK_HEAD_MEISAI_CANCEL As String _
    = "UPDATE $LM_TRN$..TC_PICK_HEAD              " & vbNewLine _
    & "SET                                        " & vbNewLine _
    & " PICK_STATE_KB = @PICK_STATE_KB            " & vbNewLine _
    & ",SYS_UPD_DATE  = @SYS_UPD_DATE             " & vbNewLine _
    & ",SYS_UPD_TIME  = @SYS_UPD_TIME             " & vbNewLine _
    & ",SYS_UPD_PGID  = @SYS_UPD_PGID             " & vbNewLine _
    & ",SYS_UPD_USER  = @SYS_UPD_USER             " & vbNewLine _
    & "WHERE                                      " & vbNewLine _
    & "    NRS_BR_CD     = @NRS_BR_CD             " & vbNewLine _
    & "AND OUTKA_NO_L    = @OUTKA_NO_L            " & vbNewLine _
    & "AND TOU_NO        = @TOU_NO                " & vbNewLine _
    & "AND SITU_NO       = @SITU_NO               " & vbNewLine _
    & "AND PICK_SEQ      = @PICK_SEQ              " & vbNewLine _
    & "AND SYS_UPD_DATE  = @SYS_UPD_DATE_BEFORE   " & vbNewLine _
    & "AND SYS_UPD_TIME  = @SYS_UPD_TIME_BEFORE   " & vbNewLine

    Public Const SQL_UPDATE_TC_PICK_DTL_CANCEL As String _
    = "UPDATE $LM_TRN$..TC_PICK_DTL               " & vbNewLine _
    & "SET                                        " & vbNewLine _
    & " CANCEL_DTL_FLG = @CANCEL_DTL_FLG          " & vbNewLine _
    & ",SYS_DEL_FLG    = @SYS_DEL_FLG             " & vbNewLine _
    & ",SYS_UPD_DATE   = @SYS_UPD_DATE            " & vbNewLine _
    & ",SYS_UPD_TIME   = @SYS_UPD_TIME            " & vbNewLine _
    & ",SYS_UPD_PGID   = @SYS_UPD_PGID            " & vbNewLine _
    & ",SYS_UPD_USER   = @SYS_UPD_USER            " & vbNewLine _
    & "WHERE                                      " & vbNewLine _
    & "    NRS_BR_CD    = @NRS_BR_CD              " & vbNewLine _
    & "AND OUTKA_NO_L   = @OUTKA_NO_L             " & vbNewLine _
    & "AND TOU_NO       = @TOU_NO                 " & vbNewLine _
    & "AND SITU_NO      = @SITU_NO                " & vbNewLine _
    & "AND PICK_SEQ     = @PICK_SEQ               " & vbNewLine _
    & "AND OUTKA_NO_M   = @OUTKA_NO_M             " & vbNewLine _
    & "AND OUTKA_NO_S   = @OUTKA_NO_S             " & vbNewLine _
    & "AND SYS_UPD_DATE = @SYS_UPD_DATE_BEFORE    " & vbNewLine _
    & "AND SYS_UPD_TIME = @SYS_UPD_TIME_BEFORE    " & vbNewLine
#End Region

#Region "出荷検品 キャンセル"
    Public Const SQL_UPDATE_TC_KNEPIN_HEAD_CANCEL As String _
    = "UPDATE $LM_TRN$..TC_KENPIN_HEAD            " & vbNewLine _
    & "SET                                        " & vbNewLine _
    & " CANCEL_FLG   = '01'                       " & vbNewLine _
    & ",CANCEL_TYPE  = @CANCEL_TYPE               " & vbNewLine _
    & ",SYS_UPD_DATE = @SYS_UPD_DATE              " & vbNewLine _
    & ",SYS_UPD_TIME = @SYS_UPD_TIME              " & vbNewLine _
    & ",SYS_UPD_PGID = @SYS_UPD_PGID              " & vbNewLine _
    & ",SYS_UPD_USER = @SYS_UPD_USER              " & vbNewLine _
    & "WHERE                                      " & vbNewLine _
    & "    NRS_BR_CD         = @NRS_BR_CD         " & vbNewLine _
    & "AND OUTKA_NO_L        = @OUTKA_NO_L        " & vbNewLine _
    & "AND KENPIN_ATTEND_SEQ = @KENPIN_ATTEND_SEQ " & vbNewLine

#End Region

#Region "出荷検品 明細キャンセル"
    Public Const SQL_UPDATE_TC_KENPIN_HEAD_MEISAI_CANCEL As String _
    = "UPDATE $LM_TRN$..TC_KENPIN_HEAD                    " & vbNewLine _
    & "SET                                                " & vbNewLine _
    & " KENPIN_ATTEND_STATE_KB = @KENPIN_ATTEND_STATE_KB  " & vbNewLine _
    & ",SYS_UPD_DATE           = @SYS_UPD_DATE            " & vbNewLine _
    & ",SYS_UPD_TIME           = @SYS_UPD_TIME            " & vbNewLine _
    & ",SYS_UPD_PGID           = @SYS_UPD_PGID            " & vbNewLine _
    & ",SYS_UPD_USER           = @SYS_UPD_USER            " & vbNewLine _
    & "WHERE                                              " & vbNewLine _
    & "    NRS_BR_CD         = @NRS_BR_CD                 " & vbNewLine _
    & "AND OUTKA_NO_L        = @OUTKA_NO_L                " & vbNewLine _
    & "AND KENPIN_ATTEND_SEQ = @KENPIN_ATTEND_SEQ         " & vbNewLine _
    & "AND SYS_UPD_DATE      = @SYS_UPD_DATE_BEFORE       " & vbNewLine _
    & "AND SYS_UPD_TIME      = @SYS_UPD_TIME_BEFORE       " & vbNewLine

    Public Const SQL_UPDATE_TC_KENPIN_DTL_CANCEL As String _
    = "UPDATE $LM_TRN$..TC_KENPIN_DTL               " & vbNewLine _
    & "SET                                           " & vbNewLine _
    & " CANCEL_DTL_FLG = @CANCEL_DTL_FLG             " & vbNewLine _
    & ",SYS_DEL_FLG    = @SYS_DEL_FLG                " & vbNewLine _
    & ",SYS_UPD_DATE   = @SYS_UPD_DATE               " & vbNewLine _
    & ",SYS_UPD_TIME   = @SYS_UPD_TIME               " & vbNewLine _
    & ",SYS_UPD_PGID   = @SYS_UPD_PGID               " & vbNewLine _
    & ",SYS_UPD_USER   = @SYS_UPD_USER               " & vbNewLine _
    & "WHERE                                         " & vbNewLine _
    & "    NRS_BR_CD         = @NRS_BR_CD            " & vbNewLine _
    & "AND OUTKA_NO_L        = @OUTKA_NO_L           " & vbNewLine _
    & "AND KENPIN_ATTEND_SEQ = @KENPIN_ATTEND_SEQ    " & vbNewLine _
    & "AND OUTKA_NO_M        = @OUTKA_NO_M           " & vbNewLine _
    & "AND OUTKA_NO_S        = @OUTKA_NO_S           " & vbNewLine _
    & "AND KENPIN_DTL_SEQ    = @KENPIN_DTL_SEQ       " & vbNewLine _
    & "AND SYS_UPD_DATE      = @SYS_UPD_DATE_BEFORE  " & vbNewLine _
    & "AND SYS_UPD_TIME      = @SYS_UPD_TIME_BEFORE  " & vbNewLine
#End Region

#Region "出荷ピック 削除"
    Public Const SQL_UPDATE_TC_PICK_HEAD_DEL As String _
    = "UPDATE $LM_TRN$..TC_PICK_HEAD              " & vbNewLine _
    & "SET                                        " & vbNewLine _
    & " PICK_STATE_KB = '99'                      " & vbNewLine _
    & ",SYS_UPD_DATE  = @SYS_UPD_DATE             " & vbNewLine _
    & ",SYS_UPD_TIME  = @SYS_UPD_TIME             " & vbNewLine _
    & ",SYS_UPD_PGID  = @SYS_UPD_PGID             " & vbNewLine _
    & ",SYS_UPD_USER  = @SYS_UPD_USER             " & vbNewLine _
    & "WHERE                                      " & vbNewLine _
    & "    NRS_BR_CD  = @NRS_BR_CD                " & vbNewLine _
    & "AND OUTKA_NO_L = @OUTKA_NO_L               " & vbNewLine _
    & "AND TOU_NO     = @TOU_NO                   " & vbNewLine _
    & "AND SITU_NO    = @SITU_NO                  " & vbNewLine _
    & "AND PICK_SEQ   = @PICK_SEQ                 " & vbNewLine
#End Region

#Region "出荷検品 削除"
    Public Const SQL_UPDATE_TC_KNEPIN_HEAD_DEL As String _
    = "UPDATE $LM_TRN$..TC_KENPIN_HEAD            " & vbNewLine _
    & "SET                                        " & vbNewLine _
    & " KENPIN_ATTEND_STATE_KB = '99'             " & vbNewLine _
    & ",SYS_UPD_DATE           = @SYS_UPD_DATE    " & vbNewLine _
    & ",SYS_UPD_TIME           = @SYS_UPD_TIME    " & vbNewLine _
    & ",SYS_UPD_PGID           = @SYS_UPD_PGID    " & vbNewLine _
    & ",SYS_UPD_USER           = @SYS_UPD_USER    " & vbNewLine _
    & "WHERE                                      " & vbNewLine _
    & "    NRS_BR_CD         = @NRS_BR_CD         " & vbNewLine _
    & "AND OUTKA_NO_L        = @OUTKA_NO_L        " & vbNewLine _
    & "AND KENPIN_ATTEND_SEQ = @KENPIN_ATTEND_SEQ " & vbNewLine

#End Region

#Region "出荷作業 削除"
    Public Const SQL_UPDATE_TC_SAGYO_DEL As String _
    = "UPDATE $LM_TRN$..TC_SAGYO                     " & vbNewLine _
    & "SET                                           " & vbNewLine _
    & " SYS_DEL_FLG    = @SYS_DEL_FLG                " & vbNewLine _
    & ",SYS_UPD_DATE   = @SYS_UPD_DATE               " & vbNewLine _
    & ",SYS_UPD_TIME   = @SYS_UPD_TIME               " & vbNewLine _
    & ",SYS_UPD_PGID   = @SYS_UPD_PGID               " & vbNewLine _
    & ",SYS_UPD_USER   = @SYS_UPD_USER               " & vbNewLine _
    & "WHERE                                         " & vbNewLine _
    & "    NRS_BR_CD         = @NRS_BR_CD            " & vbNewLine _
    & "AND SAGYO_REC_NO      = @SAGYO_REC_NO         " & vbNewLine _
    & "AND OUTKA_NO_L        = @OUTKA_NO_L           " & vbNewLine _
    & "AND OUTKA_NO_M        = @OUTKA_NO_M           " & vbNewLine _
    & "AND OUTKA_NO_S        = @OUTKA_NO_S           " & vbNewLine _
    & "AND WORK_SEQ          = @WORK_SEQ             " & vbNewLine _
    & "AND SYS_UPD_DATE      = @SYS_UPD_DATE_BEFORE  " & vbNewLine _
    & "AND SYS_UPD_TIME      = @SYS_UPD_TIME_BEFORE  " & vbNewLine
#End Region

#Region "運送更新"

#Region "ピッキングヘッダ更新"
    Public Const SQL_UPDATE_TC_PICK_HEAD_UNSO As String _
    = "UPDATE $LM_TRN$..TC_PICK_HEAD              " & vbNewLine _
    & "SET                                        " & vbNewLine _
    & " UNSO_CD      = @UNSO_CD                   " & vbNewLine _
    & ",UNSO_NM      = @UNSO_NM                   " & vbNewLine _
    & ",UNSO_BR_CD   = @UNSO_BR_CD                " & vbNewLine _
    & ",UNSO_BR_NM   = @UNSO_BR_NM                " & vbNewLine _
    & ",UNSO_CHG_FLG = @UNSO_CHG_FLG              " & vbNewLine _
    & ",SYS_UPD_DATE = @SYS_UPD_DATE              " & vbNewLine _
    & ",SYS_UPD_TIME = @SYS_UPD_TIME              " & vbNewLine _
    & ",SYS_UPD_PGID = @SYS_UPD_PGID              " & vbNewLine _
    & ",SYS_UPD_USER = @SYS_UPD_USER              " & vbNewLine _
    & "WHERE                                      " & vbNewLine _
    & "    NRS_BR_CD  = @NRS_BR_CD                " & vbNewLine _
    & "AND OUTKA_NO_L = @OUTKA_NO_L               " & vbNewLine _
    & "AND PICK_SEQ   = @SEQ                      " & vbNewLine
#End Region

#Region "検品ヘッダ更新"
    Public Const SQL_UPDATE_TC_KNEPIN_HEAD_UNSO As String _
    = "UPDATE $LM_TRN$..TC_KENPIN_HEAD            " & vbNewLine _
    & "SET                                        " & vbNewLine _
    & " KENPIN_ATTEND_STATE_KB = @KENPIN_ATTEND_STATE_KB " & vbNewLine _
    & ",UNSO_CD      = @UNSO_CD                   " & vbNewLine _
    & ",UNSO_NM      = @UNSO_NM                   " & vbNewLine _
    & ",UNSO_BR_CD   = @UNSO_BR_CD                " & vbNewLine _
    & ",UNSO_BR_NM   = @UNSO_BR_NM                " & vbNewLine _
    & ",UNSO_CHG_FLG = @UNSO_CHG_FLG              " & vbNewLine _
    & ",NIHUDA_CHK_FLG  = @NIHUDA_CHK_FLG         " & vbNewLine _
    & ",NIHUDA_CHK_STATE_KB = '00'                " & vbNewLine _
    & ",SYS_UPD_DATE = @SYS_UPD_DATE              " & vbNewLine _
    & ",SYS_UPD_TIME = @SYS_UPD_TIME              " & vbNewLine _
    & ",SYS_UPD_PGID = @SYS_UPD_PGID              " & vbNewLine _
    & ",SYS_UPD_USER = @SYS_UPD_USER              " & vbNewLine _
    & "WHERE                                      " & vbNewLine _
    & "    NRS_BR_CD         = @NRS_BR_CD         " & vbNewLine _
    & "AND OUTKA_NO_L        = @OUTKA_NO_L        " & vbNewLine _
    & "AND KENPIN_ATTEND_SEQ = @SEQ               " & vbNewLine

#End Region

#Region "検品明細更新"
    Public Const SQL_UPDATE_TC_KNEPIN_DTL_UNSO As String _
    = "UPDATE $LM_TRN$..TC_KENPIN_DTL             " & vbNewLine _
    & "SET                                        " & vbNewLine _
    & " KENPIN_STATE_KB = @KENPIN_STATE_KB        " & vbNewLine _
    & ",ATTEND_STATE_KB = @ATTEND_STATE_KB        " & vbNewLine _
    & ",SYS_UPD_DATE = @SYS_UPD_DATE              " & vbNewLine _
    & ",SYS_UPD_TIME = @SYS_UPD_TIME              " & vbNewLine _
    & ",SYS_UPD_PGID = @SYS_UPD_PGID              " & vbNewLine _
    & ",SYS_UPD_USER = @SYS_UPD_USER              " & vbNewLine _
    & "WHERE                                      " & vbNewLine _
    & "    NRS_BR_CD         = @NRS_BR_CD         " & vbNewLine _
    & "AND OUTKA_NO_L        = @OUTKA_NO_L        " & vbNewLine _
    & "AND KENPIN_ATTEND_SEQ = @SEQ               " & vbNewLine _
    & "AND OUTKA_NO_M        = @OUTKA_NO_M        " & vbNewLine _
    & "AND OUTKA_NO_S        = @OUTKA_NO_S        " & vbNewLine _
    & "AND KENPIN_DTL_SEQ    = @KENPIN_DTL_SEQ    " & vbNewLine _
    & "AND JISYATASYA_KB     = '01'               " & vbNewLine

#End Region

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

#Region "取得"

#Region "出荷ピックヘッダ取得"

    ''' <summary>
    ''' 出荷ピックヘッダ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectPickHead(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectOutkaData()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_SELECT_TC_PICK_HEAD, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("PICK_SEQ", "PICK_SEQ")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("CANCEL_TYPE", "CANCEL_TYPE")
        map.Add("PICK_USER_CD", "PICK_USER_CD")
        map.Add("PICK_USER_NM", "PICK_USER_NM")
        map.Add("PICK_STATE_KB", "PICK_STATE_KB")
        map.Add("WORK_STATE_KB", "WORK_STATE_KB")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_SIJI", "REMARK_SIJI")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("OUTKA_TTL_WT", "OUTKA_TTL_WT")
        map.Add("URGENT_FLG", "URGENT_FLG")
        map.Add("REMARK_CHK_FLG", "REMARK_CHK_FLG")
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930OUT_PICK_HEAD))


        Return ds

    End Function

#End Region

#Region "出荷ピック明細取得"

    ''' <summary>
    ''' 出荷ピック明細登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectPickDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectOutkaData()

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMC930DAC.SQL_SELECT_TC_PICK_DTL)
        Me._StrSql.Append(LMC930DAC.SQL_ORDER_TC_PICK_DTL)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(_StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("PICK_SEQ", "PICK_SEQ")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("PICK_STATE_KB", "PICK_STATE_KB")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_NRS", "GOODS_NM_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("NB_UT", "NB_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("OUT_NB", "OUT_NB")
        map.Add("OUT_KONSU", "OUT_KONSU")
        map.Add("OUT_HASU", "OUT_HASU")
        map.Add("OUT_ZAN_NB", "OUT_ZAN_NB")
        map.Add("OUT_ZAN_KONSU", "OUT_ZAN_KONSU")
        map.Add("OUT_ZAN_HASU", "OUT_ZAN_HASU")
        map.Add("OUT_QT", "OUT_QT")
        map.Add("OUT_ZAN_QT", "OUT_ZAN_QT")
        map.Add("GOODS_COND_KB1", "GOODS_COND_KB1")
        map.Add("GOODS_COND_KB2", "GOODS_COND_KB2")
        map.Add("GOODS_COND_KB3", "GOODS_COND_KB3")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")
        map.Add("CANCEL_DTL_FLG", "CANCEL_DTL_FLG")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930OUT_PICK_DTL))

    End Function

#End Region

#Region "出荷検品ヘッダ取得"

    ''' <summary>
    ''' 出荷検品ヘッダ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectKenpinHead(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectOutkaData()


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_SELECT_TC_KENPIN_HEAD, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("KENPIN_ATTEND_SEQ", "KENPIN_ATTEND_SEQ")
        map.Add("KENPIN_ATTEND_STATE_KB", "KENPIN_ATTEND_STATE_KB")
        map.Add("WORK_STATE_KB", "WORK_STATE_KB")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("CANCEL_TYPE", "CANCEL_TYPE")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_SIJI", "REMARK_SIJI")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKA_L_PKG_NB", "OUTKA_L_PKG_NB")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("OUTKA_TTL_WT", "OUTKA_TTL_WT")
        map.Add("ATTEND_FLG", "ATTEND_FLG")
        map.Add("URGENT_FLG", "URGENT_FLG")
        map.Add("NIHUDA_CHK_STATE_KB", "NIHUDA_CHK_STATE_KB")
        map.Add("NIHUDA_CHK_FLG", "NIHUDA_CHK_FLG")
        map.Add("REMARK_KENPIN_CHK_FLG", "REMARK_KENPIN_CHK_FLG")
        map.Add("REMARK_ATTEND_CHK_FLG", "REMARK_ATTEND_CHK_FLG")
        map.Add("NIHUDA_FLG", "NIHUDA_FLG")
        map.Add("NIHUDA_YN", "NIHUDA_YN")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD))

    End Function

#End Region

#Region "出荷ピックヘッダ登録データ取得"

    ''' <summary>
    ''' 出荷ピックヘッダ登録データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLMSPickData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectOutkaData()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_SELECT_PICK_HEAD_DATA, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("PICK_SEQ", "PICK_SEQ")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("CANCEL_TYPE", "CANCEL_TYPE")
        map.Add("PICK_USER_CD", "PICK_USER_CD")
        map.Add("PICK_USER_NM", "PICK_USER_NM")
        map.Add("PICK_USER_CD_SUB", "PICK_USER_CD_SUB")
        map.Add("PICK_USER_NM_SUB", "PICK_USER_NM_SUB")
        map.Add("PICK_STATE_KB", "PICK_STATE_KB")
        map.Add("WORK_STATE_KB", "WORK_STATE_KB")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_SIJI", "REMARK_SIJI")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("OUTKA_TTL_WT", "OUTKA_TTL_WT")
        map.Add("URGENT_FLG", "URGENT_FLG")
        map.Add("REMARK_CHK_FLG", "REMARK_CHK_FLG")
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")
        map.Add("HOKOKU_FLG", "HOKOKU_FLG")
        map.Add("HOKOKU_STATE_KB", "HOKOKU_STATE_KB")
        map.Add("SAGYO_FILE_STATE_KB", "SAGYO_FILE_STATE_KB")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930IN_PICK_HEAD))

        Return ds

    End Function

#End Region

#Region "出荷ピック明細登録データ取得"

    ''' <summary>
    ''' 出荷ピック明細登録データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLMSPickDtlData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectOutkaData()

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMC930DAC.SQL_SELECT_LMS_PICK_DTL_DATA)
        Me._StrSql.Append(LMC930DAC.SQL_ORDER_LMS_PICK_DTL_DATA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("PICK_SEQ", "PICK_SEQ")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("PICK_STATE_KB", "PICK_STATE_KB")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_NRS", "GOODS_NM_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("NB_UT", "NB_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("OUT_NB", "OUT_NB")
        map.Add("OUT_KONSU", "OUT_KONSU")
        map.Add("OUT_HASU", "OUT_HASU")
        map.Add("OUT_ZAN_NB", "OUT_ZAN_NB")
        map.Add("OUT_ZAN_KONSU", "OUT_ZAN_KONSU")
        map.Add("OUT_ZAN_HASU", "OUT_ZAN_HASU")
        map.Add("OUT_QT", "OUT_QT")
        map.Add("OUT_ZAN_QT", "OUT_ZAN_QT")
        map.Add("GOODS_COND_KB1", "GOODS_COND_KB1")
        map.Add("GOODS_COND_KB2", "GOODS_COND_KB2")
        map.Add("GOODS_COND_KB3", "GOODS_COND_KB3")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930IN_PICK_DTL))

    End Function

#End Region

#Region "出荷検品明細登録データ取得"

    ''' <summary>
    ''' 出荷検品明細登録データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLMSKenpinDtlData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectOutkaData()

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMC930DAC.SQL_SELECT_LMS_KENPIN_DTL_DATA)
        Me._StrSql.Append(LMC930DAC.SQL_ORDER_LMS_KENPIN_DTL_DATA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("KENPIN_ATTEND_SEQ", "KENPIN_ATTEND_SEQ")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("KENPIN_DTL_SEQ", "KENPIN_DTL_SEQ")
        map.Add("KENPIN_STATE_KB", "KENPIN_STATE_KB")
        map.Add("ATTEND_STATE_KB", "ATTEND_STATE_KB")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_NRS", "GOODS_NM_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("NB_UT", "NB_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("OUT_NB", "OUT_NB")
        map.Add("OUT_KONSU", "OUT_KONSU")
        map.Add("OUT_HASU", "OUT_HASU")
        map.Add("OUT_ZAN_NB", "OUT_ZAN_NB")
        map.Add("OUT_ZAN_KONSU", "OUT_ZAN_KONSU")
        map.Add("OUT_ZAN_HASU", "OUT_ZAN_HASU")
        map.Add("OUT_QT", "OUT_QT")
        map.Add("OUT_ZAN_QT", "OUT_ZAN_QT")
        map.Add("GOODS_COND_KB1", "GOODS_COND_KB1")
        map.Add("GOODS_COND_KB2", "GOODS_COND_KB2")
        map.Add("GOODS_COND_KB3", "GOODS_COND_KB3")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930IN_KENPIN_DTL))

    End Function

#End Region

#Region "作業取得"

    ''' <summary>
    ''' 作業取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectTcSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectOutkaData()

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMC930DAC.SQL_SELECT_TC_SAGYO)
        Me._StrSql.Append(LMC930DAC.SQL_ORDER_TC_SAGYO)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_REC_NO", "SAGYO_REC_NO")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("WORK_SEQ", "WORK_SEQ")
        map.Add("SAGYO_STATE1_KB", "SAGYO_STATE1_KB")
        map.Add("SAGYO_STATE2_KB", "SAGYO_STATE2_KB")
        map.Add("SAGYO_STATE3_KB", "SAGYO_STATE3_KB")
        map.Add("WH_CD", "WH_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM_NRS", "GOODS_NM_NRS")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("SAGYO_CD", "SAGYO_CD")
        map.Add("SAGYO_NM", "SAGYO_NM")
        map.Add("INV_TANI", "INV_TANI")
        map.Add("KOSU_BAI", "KOSU_BAI")
        map.Add("SAGYO_NB", "SAGYO_NB")
        map.Add("REMARK", "REMARK")
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")
        map.Add("IOZS_KB", "IOZS_KB")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930OUT_SAGYO))

    End Function

#End Region

#Region "出荷作業登録データ取得"

    ''' <summary>
    ''' 出荷作業登録データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLMSSagyoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectOutkaData()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_SELECT_SAGYO_DATA, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_REC_NO", "SAGYO_REC_NO")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("WORK_SEQ", "WORK_SEQ")
        map.Add("SAGYO_STATE1_KB", "SAGYO_STATE1_KB")
        map.Add("SAGYO_STATE2_KB", "SAGYO_STATE2_KB")
        map.Add("SAGYO_STATE3_KB", "SAGYO_STATE3_KB")
        map.Add("WH_CD", "WH_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM_NRS", "GOODS_NM_NRS")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("SAGYO_CD", "SAGYO_CD")
        map.Add("SAGYO_NM", "SAGYO_NM")
        map.Add("INV_TANI", "INV_TANI")
        map.Add("KOSU_BAI", "KOSU_BAI")
        map.Add("SAGYO_NB", "SAGYO_NB")
        map.Add("REMARK", "REMARK")
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")
        map.Add("IOZS_KB", "IOZS_KB")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930IN_SAGYO))

        Return ds

    End Function

#End Region

#Region "チェックデータ取得"

    ''' <summary>
    ''' チェックデータ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectOutkaCheckData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectOutkaData()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_SELECT_CHECK_DATA, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("OUTKA_STATE_KB", "OUTKA_STATE_KB")
        map.Add("WH_TAB_YN", "WH_TAB_YN")
        map.Add("WH_TAB_STATUS", "WH_TAB_STATUS")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("BACKLOG_NB", "BACKLOG_NB")
        map.Add("USER_CD", "USER_CD")
        map.Add("USER_NM", "USER_NM")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930CHECK))

        Return ds

    End Function

#End Region

#Region "倉庫チェック"
    ''' <summary>
    ''' 倉庫チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectSokoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append("SELECT                         " & vbNewLine)
        Me._StrSql.Append("  NRS_BR_CD                    " & vbNewLine)
        Me._StrSql.Append(" ,WH_CD                        " & vbNewLine)
        Me._StrSql.Append(" ,WH_UNSO_CHG_YN               " & vbNewLine)
        Me._StrSql.Append("FROM $LM_MST$..M_SOKO          " & vbNewLine)
        Me._StrSql.Append("WHERE                          " & vbNewLine)
        Me._StrSql.Append("    NRS_BR_CD = @NRS_BR_CD     " & vbNewLine)
        Me._StrSql.Append("AND WH_CD     = @WH_CD         " & vbNewLine)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row("WH_CD").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_UNSO_CHG_YN", "WH_UNSO_CHG_YN")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930_M_SOKO))


    End Function
#End Region

#Region "運送会社情報取得"

    ''' <summary>
    ''' 運送会社情報取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectUnsocoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionSelectUnso()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_SELECT_UNSOCO_DATA, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("NIHUDA_CHK_FLG", "NIHUDA_CHK_FLG")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930_M_UNSOCO))

    End Function

#End Region

#Region "運送情報取得"

    ''' <summary>
    ''' 運送情報取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectUnsoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'パラメータ設定
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append("SELECT                         " & vbNewLine)
        Me._StrSql.Append("  NRS_BR_CD                    " & vbNewLine)
        Me._StrSql.Append(" ,UNSO_NO_L                    " & vbNewLine)
        Me._StrSql.Append(" ,INOUTKA_NO_L AS OUTKA_NO_L   " & vbNewLine)
        Me._StrSql.Append("FROM $LM_TRN$..F_UNSO_L        " & vbNewLine)
        Me._StrSql.Append("WHERE                          " & vbNewLine)
        Me._StrSql.Append("    NRS_BR_CD = @NRS_BR_CD     " & vbNewLine)
        Me._StrSql.Append("AND UNSO_NO_L = @UNSO_NO_L     " & vbNewLine)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row("UNSO_NO_L").ToString(), DBDataType.CHAR))
        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930_F_UNSO_L))

    End Function

#End Region

#Region "出荷検品明細取得"

    ''' <summary>
    ''' 出荷検品明細取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectKenpinDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectOutkaData()


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_SELECT_TC_KENPIN_DTL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("KENPIN_ATTEND_SEQ", "KENPIN_ATTEND_SEQ")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("KENPIN_DTL_SEQ", "KENPIN_DTL_SEQ")
        map.Add("KENPIN_STATE_KB", "KENPIN_STATE_KB")
        map.Add("ATTEND_STATE_KB", "ATTEND_STATE_KB")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_NRS", "GOODS_NM_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("NB_UT", "NB_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("OUT_NB", "OUT_NB")
        map.Add("OUT_KONSU", "OUT_KONSU")
        map.Add("OUT_HASU", "OUT_HASU")
        map.Add("OUT_ZAN_NB", "OUT_ZAN_NB")
        map.Add("OUT_ZAN_KONSU", "OUT_ZAN_KONSU")
        map.Add("OUT_ZAN_HASU", "OUT_ZAN_HASU")
        map.Add("OUT_QT", "OUT_QT")
        map.Add("OUT_ZAN_QT", "OUT_ZAN_QT")
        map.Add("GOODS_COND_KB1", "GOODS_COND_KB1")
        map.Add("GOODS_COND_KB2", "GOODS_COND_KB2")
        map.Add("GOODS_COND_KB3", "GOODS_COND_KB3")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")
        map.Add("CANCEL_DTL_FLG", "CANCEL_DTL_FLG")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_DTL))

    End Function

#End Region

#Region "出荷差分比較用出荷ピックヘッダ取得"

    ''' <summary>
    ''' 出荷差分比較用出荷ピックヘッダ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectPickHeadToCompare(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_PICK_HEAD).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectPickHeadToCompare()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_SELECT_TC_PICK_HEAD_TO_COMPARE, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("PICK_SEQ", "PICK_SEQ")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_SIJI", "REMARK_SIJI")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("OUTKA_TTL_WT", "OUTKA_TTL_WT")
        map.Add("URGENT_FLG", "URGENT_FLG")

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930COMPARE_TC_PICK_HEAD)

    End Function

#End Region

#Region "出荷差分比較用出荷ピック明細取得"

    ''' <summary>
    ''' 出荷差分比較用出荷ピック明細取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectPickDtlToCompare(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930COMPARE_TC_PICK_HEAD).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectPickDtlToCompare(ds)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_SELECT_TC_PICK_DTL_TO_COMPARE, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("GOODS_NM_NRS", "GOODS_NM_NRS")
        map.Add("OUTKA_NO_S_BEFORE", "OUTKA_NO_S_BEFORE")
        map.Add("OUTKA_NO_S_AFTER", "OUTKA_NO_S_AFTER")

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930COMPARE_TC_PICK_DTL)

    End Function

#End Region

#Region "荷主明細マスタ取得"

    ''' <summary>
    ''' 荷主明細マスタ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectMCustDetails(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_M_CUST_DETAILS).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectMCustDetails(ds)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_SELECT_M_CUST_DETAILS, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_NAIYO", "SET_NAIYO")

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930OUT_M_CUST_DETAILS)

    End Function

#End Region

#End Region

#Region "登録"

#Region "出荷ピックヘッダ登録"

    ''' <summary>
    ''' 出荷ピックヘッダ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertPickHead(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_PICK_HEAD).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamInsertPickHead()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_INSERT_TC_PICK_HEAD, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "出荷ピック明細登録"

    ''' <summary>
    ''' 出荷ピック明細登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertPickDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'SQL生成
        Me._StrSql = New StringBuilder
        Me._StrSql.Append(LMC930DAC.SQL_INSERT_TC_PICK_DTL)

        If Me._Row("CUST_DTL_0L").ToString = "1" Then
            'フィルメ特殊処理フラグOnの場合
            Me._StrSql.Replace("$GOODS_NM$", LMC930DAC.SQL_INSERT_TC_PICK_DTL_GOODS_NM_2)
            Me._StrSql.Replace("$JOIN_EDI_M$", LMC930DAC.SQL_INSERT_TC_PICK_DTL_JOIN_EDI_M)
        Else
            Me._StrSql.Replace("$GOODS_NM$", LMC930DAC.SQL_INSERT_TC_PICK_DTL_GOODS_NM_1)
            Me._StrSql.Replace("$JOIN_EDI_M$", "")
        End If

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectOutkaData()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function


    ''' <summary>
    ''' 出荷ピック明細登録(全項目プレースホルダ)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertPickDtlPlaceHolder(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_PICK_DTL).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamInsertPickDtlPlaceHolder()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_INSERT_TC_PICK_DTL_PLACEHOLDER, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "出荷検品ヘッダ登録"

    ''' <summary>
    ''' 出荷検品ヘッダ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertKenpinHead(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectOutkaData()
        Call Me.SetParamCommonSystemUp()


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_INSERT_TC_KENPIN_HEAD, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "出荷検品明細登録"

    ''' <summary>
    ''' 出荷検品明細登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertKenpinDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        'SQL生成
        Me._StrSql = New StringBuilder
        Me._StrSql.Append(LMC930DAC.SQL_INSERT_TC_KENPIN_DTL)

        If Me._Row("CUST_DTL_0L").ToString = "1" Then
            'フィルメ特殊処理フラグOnの場合
            Me._StrSql.Replace("$GOODS_NM$", LMC930DAC.SQL_INSERT_TC_KENPIN_DTL_GOODS_NM_2)
            Me._StrSql.Replace("$JOIN_EDI_M$", LMC930DAC.SQL_INSERT_TC_KENPIN_DTL_JOIN_EDI_M)
            'ADD Start 2022/12/28 アフトン別名出荷対応
        ElseIf Me._Row("CUST_DTL_B2").ToString = "1" Then
            'セミEDI出荷指示取込データの商品名表示フラグOnの場合
            Me._StrSql.Replace("$GOODS_NM$", LMC930DAC.SQL_INSERT_TC_KENPIN_DTL_GOODS_NM_3)
            Me._StrSql.Replace("$JOIN_EDI_M$", LMC930DAC.SQL_INSERT_TC_KENPIN_DTL_JOIN_EDI_M)
            'ADD End   2022/12/28 アフトン別名出荷対応
        Else
            Me._StrSql.Replace("$GOODS_NM$", LMC930DAC.SQL_INSERT_TC_KENPIN_DTL_GOODS_NM_1)
            Me._StrSql.Replace("$JOIN_EDI_M$", "")
        End If

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectOutkaData()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷検品明細登録(全項目プレースホルダ)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertKenpinDtlPlaceHolder(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_KENPIN_DTL).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamInsertKenpinDtl()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_INSERT_TC_KENPIN_DTL_PLACEHOLDER, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "出荷作業登録"

    ''' <summary>
    ''' 出荷作業登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_SAGYO).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamInsertSagyo()
        Call Me.SetParamCommonSystemUp()


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_INSERT_TC_SAGYO, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "出荷差分比較登録"

    ''' <summary>
    ''' 出荷差分比較登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertDiffComparison(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_TC_DIFF_COMPARISON).Rows(0)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_INSERT_TC_DIFF_COMPARISON, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'DataSetのIN情報を取得
        For Each Me._Row In ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_TC_DIFF_COMPARISON).Rows

            'パラメータ設定
            Me._SqlPrmList = New ArrayList()
            Call Me.SetParamInsertDiffComparison()
            Call Me.SetParamCommonSystemUp()

            'パラメータの反映
            cmd.Parameters.Clear()
            cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

            MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region

#End Region

#Region "更新"

#Region "キャンセル"

#Region "出荷ピックキャンセル"
    ''' <summary>
    ''' 出荷ピックキャンセル
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdatePickCancel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_HEAD).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionUpdatePick()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_UPDATE_TC_PICK_HEAD_CANCEL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "出荷ピック明細キャンセル"

    ''' <summary>
    ''' 明細キャンセル時出荷ピックヘッダ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdatePickHeadMeisaiCancel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_HEAD).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamUpdatePickHeadMeisaiCancel()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_UPDATE_TC_PICK_HEAD_MEISAI_CANCEL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷ピック明細キャンセル
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdatePickDtlMeisaiCancel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_DTL).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamUpdatePickDtlMeisaiCancel()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_UPDATE_TC_PICK_DTL_CANCEL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "出荷検品キャンセル"
    ''' <summary>
    ''' 出荷検品キャンセル
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateKenpinCancel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionUpdateKenpin()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_UPDATE_TC_KNEPIN_HEAD_CANCEL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "出荷検品明細キャンセル"

    ''' <summary>
    ''' 明細キャンセル時出荷検品ヘッダ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateKenpinHeadMeisaiCancel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamUpdateKenpinHeadMeisaiCancel()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_UPDATE_TC_KENPIN_HEAD_MEISAI_CANCEL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷検品明細キャンセル
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateKenpinDtlMeisaiCancel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_DTL).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamUpdateKenpinDtlMeisaiCancel()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_UPDATE_TC_KENPIN_DTL_CANCEL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "削除"

#Region "出荷ピック削除"
    ''' <summary>
    ''' 出荷ピックキャンセル
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdatePickDelete(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_HEAD).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionUpdatePick()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_UPDATE_TC_PICK_HEAD_DEL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "出荷検品削除"
    ''' <summary>
    ''' 出荷検品削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateKenpinDelete(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionUpdateKenpin()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_UPDATE_TC_KNEPIN_HEAD_DEL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "出荷作業削除"
    ''' <summary>
    ''' 出荷作業削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateSagyoDelete(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_SAGYO).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamUpdateSagyoDelete()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_UPDATE_TC_SAGYO_DEL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "運送更新"

#Region "ピッキングヘッダ更新"
    ''' <summary>
    ''' ピッキングヘッダ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdatePickUnsoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_UNSO).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionUpdateUnso()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_UPDATE_TC_PICK_HEAD_UNSO, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function
#End Region

#Region "検品ヘッダ更新"
    ''' <summary>
    ''' 検品ヘッダ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateKenpinHeadUnsoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_UNSO).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionUpdateUnso()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_UPDATE_TC_KNEPIN_HEAD_UNSO, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function
#End Region

#Region "検品明細更新"
    ''' <summary>
    ''' 検品明細更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateKenpinDtlUnsoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        For i As Integer = 0 To ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_UNSO).Rows.Count - 1

            Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_UNSO).Rows(i)

            'パラメータ設定
            Me._SqlPrmList = New ArrayList()
            Call Me.SetSelectConditionUpdateUnso()

            'スキーマ名設定
            Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_UPDATE_TC_KNEPIN_DTL_UNSO, Me._Row.Item("NRS_BR_CD").ToString())

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Next

        Return ds

    End Function
#End Region

#End Region

#End Region

#Region "営業所チェック"
    ''' <summary>
    ''' 出荷ピックヘッダ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckTabletUse(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append("SELECT count(*) AS CNT FROM $LM_MST$..Z_KBN  " & vbNewLine)
        Me._StrSql.Append("WHERE                          " & vbNewLine)
        Me._StrSql.Append("    KBN_GROUP_CD = 'B007'      " & vbNewLine)
        Me._StrSql.Append("AND VALUE1 = 1.000             " & vbNewLine)
        Me._StrSql.Append("AND KBN_CD = @NRS_BR_CD        " & vbNewLine)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("CNT")))
        reader.Close()

        Return ds

    End Function
#End Region

#Region "現場作業指示ステータス更新"
    ''' <summary>
    ''' 現場作業指示ステータス更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateWHSagyoShijiStatus(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append("UPDATE $LM_TRN$..C_OUTKA_L           " & vbNewLine)
        Me._StrSql.Append("SET                                  " & vbNewLine)
        Me._StrSql.Append("     WH_TAB_STATUS = @WH_TAB_STATUS  " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_DATE  = @SYS_UPD_DATE   " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_TIME  = @SYS_UPD_TIME   " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_PGID  = @SYS_UPD_PGID   " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_USER  = @SYS_UPD_USER   " & vbNewLine)
        Me._StrSql.Append("WHERE                                " & vbNewLine)
        Me._StrSql.Append("    NRS_BR_CD  = @NRS_BR_CD          " & vbNewLine)
        Me._StrSql.Append("AND OUTKA_NO_L = @OUTKA_NO_L         " & vbNewLine)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", Me._Row("WH_TAB_STATUS_KB").ToString(), DBDataType.CHAR))
        Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        MyBase.SetResultCount(rtn)

        Return ds

    End Function
#End Region

#Region "排他チェック"
    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckHaita(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append("SELECT count(*) AS CNT FROM $LM_TRN$..C_OUTKA_L  " & vbNewLine)
        Me._StrSql.Append("WHERE                                            " & vbNewLine)
        Me._StrSql.Append("    NRS_BR_CD    = @NRS_BR_CD                    " & vbNewLine)
        Me._StrSql.Append("AND OUTKA_NO_L   = @OUTKA_NO_L                   " & vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE                 " & vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME                 " & vbNewLine)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        map.Add("CNT", "CNT")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930CNT))

        Return ds
    End Function
#End Region

#Region "自社他社件数の取得"

    ''' <summary>
    ''' 自社他社件数の取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectJisyaTasyaCount(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_JISYATASYA).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row("OUTKA_NO_M").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC930DAC.SQL_SELECT_JISYATASYA_COUNT, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("JISYA_CNT", "JISYA_CNT")
        map.Add("TASYA_CNT", "TASYA_CNT")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMC930DAC.TABLE_NM.LMC930OUT_JISYATASYA))

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
    '''  パラメータ設定 出荷データ取得用
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub SetParamSelectOutkaData()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_TYPE", Me._Row("PROC_TYPE").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))


    End Sub

    ''' <summary>
    '''  パラメータ設定 出荷差分比較用出荷ピック取得用
    ''' </summary>
    ''' <remarks>出荷差分比較用出荷ピック取得SQLの構築</remarks>
    Private Sub SetParamSelectPickHeadToCompare()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_SEQ_AFT", Me._Row("PICK_SEQ"), DBDataType.NUMERIC))

    End Sub

    ''' <summary>
    '''  パラメータ設定 出荷差分比較用出荷ピック取得用
    ''' </summary>
    ''' <remarks>出荷差分比較用出荷ピック取得SQLの構築</remarks>
    Private Sub SetParamSelectPickDtlToCompare(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930COMPARE_TC_PICK_HEAD)
        Dim pickSeqBef As Integer = CInt(dt.Rows(0).Item("PICK_SEQ"))
        Dim pickSeqAft As Integer = CInt(dt.Rows(1).Item("PICK_SEQ"))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_SEQ_BEF", pickSeqBef, DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_SEQ_AFT", pickSeqAft, DBDataType.NUMERIC))

    End Sub

    ''' <summary>
    '''  パラメータ設定 荷主明細マスタ取得用
    ''' </summary>
    ''' <remarks>荷主明細マスタ取得SQLの構築</remarks>
    Private Sub SetParamSelectMCustDetails(ByVal ds As DataSet)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", Me._Row("CUST_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SUB_KB", Me._Row("SUB_KB").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定 ピック更新用
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub SetSelectConditionUpdatePick()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row("TOU_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_SEQ", Me._Row("PICK_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANCEL_TYPE", Me._Row("CANCEL_TYPE").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定 明細キャンセル時出荷ピックヘッダ更新用
    ''' </summary>
    ''' <remarks>明細キャンセル時出荷ピックヘッダ更新用SQLの構築</remarks>
    Private Sub SetParamUpdatePickHeadMeisaiCancel()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_STATE_KB", Me._Row("PICK_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row("TOU_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_SEQ", Me._Row("PICK_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_BEFORE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_BEFORE", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定 出荷ピック明細キャンセル
    ''' </summary>
    ''' <remarks>出荷ピック明細キャンセル用SQLの構築</remarks>
    Private Sub SetParamUpdatePickDtlMeisaiCancel()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANCEL_DTL_FLG", Me._Row("CANCEL_DTL_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row("TOU_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_SEQ", Me._Row("PICK_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row("OUTKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", Me._Row("OUTKA_NO_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_BEFORE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_BEFORE", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub


    ''' <summary>
    '''  パラメータ設定 明細キャンセル時出荷検品ヘッダ更新用
    ''' </summary>
    ''' <remarks>明細キャンセル時出荷検品ヘッダ更新用SQLの構築</remarks>
    Private Sub SetParamUpdateKenpinHeadMeisaiCancel()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENPIN_ATTEND_STATE_KB", Me._Row("KENPIN_ATTEND_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENPIN_ATTEND_SEQ", Me._Row("KENPIN_ATTEND_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_BEFORE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_BEFORE", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定 出荷検品明細キャンセル
    ''' </summary>
    ''' <remarks>出荷検品明細キャンセル用SQLの構築</remarks>
    Private Sub SetParamUpdateKenpinDtlMeisaiCancel()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANCEL_DTL_FLG", Me._Row("CANCEL_DTL_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENPIN_ATTEND_SEQ", Me._Row("KENPIN_ATTEND_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row("OUTKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", Me._Row("OUTKA_NO_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENPIN_DTL_SEQ", Me._Row("KENPIN_DTL_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_BEFORE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_BEFORE", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定 検品更新用
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub SetSelectConditionUpdateKenpin()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENPIN_ATTEND_SEQ", Me._Row("KENPIN_ATTEND_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANCEL_TYPE", Me._Row("CANCEL_TYPE").ToString(), DBDataType.CHAR))
    End Sub

    ''' <summary>
    '''  パラメータ設定 出荷作業削除用
    ''' </summary>
    ''' <remarks>出荷作業削除用SQLの構築</remarks>
    Private Sub SetParamUpdateSagyoDelete()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", Me._Row("SAGYO_REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row("OUTKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", Me._Row("OUTKA_NO_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WORK_SEQ", Me._Row("WORK_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_BEFORE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_BEFORE", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定 作業インサート用
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub SetParamInsertSagyo()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", Me._Row("SAGYO_REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row("OUTKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", Me._Row("OUTKA_NO_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WORK_SEQ", Me._Row("WORK_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_STATE1_KB", Me._Row("SAGYO_STATE1_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_STATE2_KB", Me._Row("SAGYO_STATE2_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_STATE3_KB", Me._Row("SAGYO_STATE3_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me._Row("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", Me._Row("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me._Row("IRIME").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me._Row("IRIME_UT").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me._Row("PKG_NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_UT", Me._Row("PKG_UT").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me._Row("LOT_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row("TOU_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", Me._Row("ZONE_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", Me._Row("LOCA").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", Me._Row("SAGYO_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", Me._Row("SAGYO_NM").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_TANI", Me._Row("INV_TANI").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOSU_BAI", Me._Row("KOSU_BAI").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", Me._Row("SAGYO_NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me._Row("REMARK").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISYATASYA_KB", Me._Row("JISYATASYA_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", Me._Row("IOZS_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", "0", DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))


    End Sub

    ''' <summary>
    '''  パラメータ設定 ピックインサート用
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub SetParamInsertPickHead()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row("TOU_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_SEQ", Me._Row("PICK_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANCEL_FLG", Me._Row("CANCEL_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANCEL_TYPE", Me._Row("CANCEL_TYPE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_USER_CD", Me._Row("PICK_USER_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_USER_NM", Me._Row("PICK_USER_NM").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_USER_CD_SUB", Me._Row("PICK_USER_CD_SUB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_USER_NM_SUB", Me._Row("PICK_USER_NM_SUB").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_STATE_KB", Me._Row("PICK_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WORK_STATE_KB", Me._Row("WORK_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", Me._Row("CUST_NM_L").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", Me._Row("CUST_NM_M").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me._Row("UNSO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NM", Me._Row("UNSO_NM").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me._Row("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_NM", Me._Row("UNSO_BR_NM").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me._Row("DEST_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me._Row("DEST_NM").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", Me._Row("DEST_AD_1").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", Me._Row("DEST_AD_2").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", Me._Row("DEST_AD_3").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_TEL", Me._Row("DEST_TEL").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", Me._Row("OUTKO_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me._Row("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", Me._Row("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", Me._Row("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me._Row("REMARK").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_SIJI", Me._Row("REMARK_SIJI").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", Me._Row("CUST_ORD_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me._Row("BUYER_ORD_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me._Row("OUTKA_PKG_NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me._Row("OUTKA_TTL_NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_WT", Me._Row("OUTKA_TTL_WT").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@URGENT_FLG", Me._Row("URGENT_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_CHK_FLG", Me._Row("REMARK_CHK_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISYATASYA_KB", Me._Row("JISYATASYA_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKOKU_FLG", Me._Row("HOKOKU_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKOKU_STATE_KB", Me._Row("HOKOKU_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_FILE_STATE_KB", Me._Row("SAGYO_FILE_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", "0", DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))


    End Sub

    ''' <summary>
    '''  パラメータ設定 出荷ピック明細登録(全項目プレースホルダ)用
    ''' </summary>
    ''' <remarks>出荷ピック明細登録用SQLの構築</remarks>
    Private Sub SetParamInsertPickDtlPlaceHolder()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row("TOU_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_SEQ", Me._Row("PICK_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row("OUTKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", Me._Row("OUTKA_NO_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_STATE_KB", Me._Row("PICK_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me._Row("GOODS_CD_NRS").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", Me._Row("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", Me._Row("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me._Row("LOT_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me._Row("IRIME").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me._Row("IRIME_UT").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", Me._Row("NB_UT").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me._Row("PKG_NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_UT", Me._Row("PKG_UT").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", Me._Row("ZONE_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", Me._Row("LOCA").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_NB", Me._Row("OUT_NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_KONSU", Me._Row("OUT_KONSU").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_HASU", Me._Row("OUT_HASU").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_ZAN_NB", Me._Row("OUT_ZAN_NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_ZAN_KONSU", Me._Row("OUT_ZAN_KONSU").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_ZAN_HASU", Me._Row("OUT_ZAN_HASU").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_QT", Me._Row("OUT_QT").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_ZAN_QT", Me._Row("OUT_ZAN_QT").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB1", Me._Row("GOODS_COND_KB1").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB2", Me._Row("GOODS_COND_KB2").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB3", Me._Row("GOODS_COND_KB3").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE", Me._Row("LT_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", Me._Row("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPD_KB", Me._Row("SPD_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OFB_KB", Me._Row("OFB_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RSV_NO", Me._Row("RSV_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", Me._Row("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", Me._Row("INKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", Me._Row("INKA_NO_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me._Row("ZAI_REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", Me._Row("INKA_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me._Row("REMARK").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", Me._Row("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISYATASYA_KB", Me._Row("JISYATASYA_KB").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", "0", DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定 出荷検品明細登録(全項目プレースホルダ)用
    ''' </summary>
    ''' <remarks>出荷検品明細登録用SQLの構築</remarks>
    Private Sub SetParamInsertKenpinDtl()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENPIN_ATTEND_SEQ", Me._Row("KENPIN_ATTEND_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row("OUTKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", Me._Row("OUTKA_NO_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENPIN_DTL_SEQ", Me._Row("KENPIN_DTL_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENPIN_STATE_KB", Me._Row("KENPIN_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ATTEND_STATE_KB", Me._Row("ATTEND_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me._Row("GOODS_CD_NRS").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", Me._Row("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", Me._Row("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me._Row("LOT_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me._Row("IRIME").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me._Row("IRIME_UT").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", Me._Row("NB_UT").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me._Row("PKG_NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_UT", Me._Row("PKG_UT").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row("TOU_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", Me._Row("ZONE_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", Me._Row("LOCA").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_NB", Me._Row("OUT_NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_KONSU", Me._Row("OUT_KONSU").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_HASU", Me._Row("OUT_HASU").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_ZAN_NB", Me._Row("OUT_ZAN_NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_ZAN_KONSU", Me._Row("OUT_ZAN_KONSU").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_ZAN_HASU", Me._Row("OUT_ZAN_HASU").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_QT", Me._Row("OUT_QT").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_ZAN_QT", Me._Row("OUT_ZAN_QT").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB1", Me._Row("GOODS_COND_KB1").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB2", Me._Row("GOODS_COND_KB2").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB3", Me._Row("GOODS_COND_KB3").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE", Me._Row("LT_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", Me._Row("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPD_KB", Me._Row("SPD_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OFB_KB", Me._Row("OFB_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RSV_NO", Me._Row("RSV_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", Me._Row("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", Me._Row("INKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", Me._Row("INKA_NO_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me._Row("ZAI_REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", Me._Row("INKA_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me._Row("REMARK").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", Me._Row("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISYATASYA_KB", Me._Row("JISYATASYA_KB").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", "0", DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定 出荷差分比較登録用
    ''' </summary>
    ''' <remarks>出荷差分比較登録用SQLの構築</remarks>
    Private Sub SetParamInsertDiffComparison()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_SEQ", Me._Row("OUTKA_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DETAILS_SEQ", Me._Row("DETAILS_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ITEM_NAME", Me._Row("ITEM_NAME").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BEFORE_VALUE", Me._Row("BEFORE_VALUE").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AFTER_VALUE", Me._Row("AFTER_VALUE").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CHANGE_FLG", Me._Row("GOODS_CHANGE_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", Me._Row("GOODS_NM").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", "0", DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定 運送更新用
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub SetSelectConditionSelectUnso()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me._Row("UNSO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me._Row("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定 運送更新用
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub SetSelectConditionUpdateUnso()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEQ", Me._Row("SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me._Row("UNSO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NM", Me._Row("UNSO_NM").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me._Row("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_NM", Me._Row("UNSO_BR_NM").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CHG_FLG", Me._Row("UNSO_CHG_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIHUDA_CHK_FLG", Me._Row("NIHUDA_CHK_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENPIN_ATTEND_STATE_KB", Me._Row("KENPIN_ATTEND_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENPIN_STATE_KB", Me._Row("KENPIN_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ATTEND_STATE_KB", Me._Row("ATTEND_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row("OUTKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", Me._Row("OUTKA_NO_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENPIN_DTL_SEQ", Me._Row("KENPIN_DTL_SEQ").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetNowDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Now.ToString("HHmmssfff"), DBDataType.CHAR))
        'KENPIN_STATE_KB
    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUp()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetNowDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetNowTime(), DBDataType.CHAR))
        
    End Sub

    ''' <summary>
    ''' 現在日付取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetNowDate() As String
        Return String.Concat(Right("0000" & Now.Year.ToString(), 4), Right("00" & Now.Month.ToString(), 2), Right("00" & Now.Day.ToString(), 2))
    End Function

    ''' <summary>
    ''' 現在時刻取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetNowTime() As String
        Return String.Concat(Right("00" & Now.Hour.ToString(), 2), Right("00" & Now.Minute.ToString(), 2), Right("00" & Now.Second.ToString(), 2), Right("000" & Now.Millisecond.ToString(), 3))
    End Function

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
