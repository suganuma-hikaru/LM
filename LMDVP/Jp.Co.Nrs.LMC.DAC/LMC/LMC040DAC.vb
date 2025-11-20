' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC040    : 在庫引当
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMC040DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC040DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

    ''' <summary>
    ''' カウント用（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(ZAI.ZAI_REC_NO)		            AS SELECT_CNT       " & vbNewLine


    ''' <summary>
    ''' ORDER BY（検索時　手動引当）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY As String = "ORDER BY                                          " & vbNewLine _
                                         & " ZAI.DEST_CD_P DESC                                      " & vbNewLine _
                                         & ",ZAI.ALLOC_PRIORITY                                      " & vbNewLine _
                                         & ",ZAI.LT_DATE                                             " & vbNewLine _
                                         & ",ZAI.GOODS_CRT_DATE                                      " & vbNewLine _
                                         & ",INKA_DATE2                                              " & vbNewLine _
                                         & ",ZAI.LOT_NO                                              " & vbNewLine _
                                         & ",ZAI.SERIAL_NO                                           " & vbNewLine _
                                         & ",ZAI.ALLOC_CAN_NB                                        " & vbNewLine


    ''' <summary>
    ''' ORDER BY（検索時　自動引当）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_AUTO As String = "ORDER BY                                     " & vbNewLine _
                                         & " YOJITU                                                  " & vbNewLine _
                                         & ",ZAI.DEST_CD_P DESC                                      " & vbNewLine _
                                         & ",ZAI.ALLOC_PRIORITY                                      " & vbNewLine _
                                         & ",ZAI.LT_DATE                                             " & vbNewLine _
                                         & ",ZAI.GOODS_CRT_DATE                                      " & vbNewLine _
                                         & ",INKA_DATE2                                              " & vbNewLine _
                                         & ",ZAI.LOT_NO                                              " & vbNewLine _
                                         & ",ZAI.SERIAL_NO                                           " & vbNewLine _
                                         & ",ZAI.ALLOC_CAN_NB                                        " & vbNewLine _
                                         & ",ZAI.TOU_NO                                              " & vbNewLine _
                                         & ",ZAI.SITU_NO                                             " & vbNewLine _
                                         & ",ZAI.ZONE_CD                                             " & vbNewLine _
                                         & ",ZAI.LOCA                                                " & vbNewLine

    ''' <summary>
    ''' ORDER BY（検索時　自動引当の特定荷主の場合）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_AUTO2 As String = "ORDER BY                                    " & vbNewLine _
                                         & " YOJITU                                                  " & vbNewLine _
                                         & ",ZAI.DEST_CD_P DESC                                      " & vbNewLine _
                                         & ",ZAI.ALLOC_PRIORITY                                      " & vbNewLine _
                                         & ",ZAI.LT_DATE                                             " & vbNewLine _
                                         & ",ZAI.GOODS_CRT_DATE                                      " & vbNewLine _
                                         & ",INKA_L.INKA_STATE_KB DESC                               " & vbNewLine _
                                         & ",ZAI.LOT_NO                                              " & vbNewLine _
                                         & ",ZAI.SERIAL_NO                                           " & vbNewLine _
                                         & ",ZAI.ALLOC_CAN_NB                                        " & vbNewLine _
                                         & ",ZAI.TOU_NO                                              " & vbNewLine _
                                         & ",ZAI.SITU_NO                                             " & vbNewLine _
                                         & ",ZAI.ZONE_CD                                             " & vbNewLine _
                                         & ",ZAI.LOCA                                                " & vbNewLine
    'END YANAI 要望番号945

    'START S.Koba 要望番号1107
    ''' <summary>
    ''' ORDER BY（検索時　自動引当）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_AUTO3 As String = "ORDER BY                                     " & vbNewLine _
                                         & " YOJITU                                                  " & vbNewLine _
                                         & ",ZAI.DEST_CD_P DESC                                      " & vbNewLine _
                                         & ",ZAI.ALLOC_PRIORITY                                      " & vbNewLine _
                                         & ",ZAI.LT_DATE                                             " & vbNewLine _
                                         & ",ZAI.GOODS_CRT_DATE                                      " & vbNewLine _
                                         & ",INKA_DATE2                                              " & vbNewLine _
                                         & ",ZAI.LOT_NO                                              " & vbNewLine _
                                         & ",ZAI.SERIAL_NO                                           " & vbNewLine _
                                         & ",ISNULL((SELECT MCD.SET_NAIYO_2 FROM $LM_MST$..M_CUST_DETAILS MCD                                         " & vbNewLine _
                                         & "         WHERE  MCD.NRS_BR_CD = ZAI.NRS_BR_CD AND MCD.CUST_CD = ZAI.CUST_CD_L AND MCD.SUB_KB = '30' AND MCD.SET_NAIYO = ZAI.TOU_NO ),99)" & vbNewLine _
                                         & ",ZAI.ALLOC_CAN_NB                                        " & vbNewLine _
                                         & ",ZAI.TOU_NO                                              " & vbNewLine _
                                         & ",ZAI.SITU_NO                                             " & vbNewLine _
                                         & ",ZAI.ZONE_CD                                             " & vbNewLine _
                                         & ",ZAI.LOCA                                                " & vbNewLine
    'END S.Koba 要望番号1107

    '要望番号:1592 terakawa 2012.11.15 Start
    ''' <summary>
    ''' ORDER BY（検索時　群馬DIC(00076)）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_DIC As String = "ORDER BY                                       " & vbNewLine _
                                         & " ZAI.DEST_CD_P DESC                                      " & vbNewLine _
                                         & ",ZAI.ALLOC_PRIORITY                                      " & vbNewLine _
                                         & ",ZAI.LT_DATE                                             " & vbNewLine _
                                         & ",ZAI.GOODS_CRT_DATE                                      " & vbNewLine _
                                         & ",INKA_DATE2                                              " & vbNewLine _
                                         & ",ZAI.LOT_NO                                              " & vbNewLine _
                                         & ",ZAI.SERIAL_NO                                           " & vbNewLine _
                                         & ",ZAI.TOU_NO                                              " & vbNewLine _
                                         & ",ZAI.SITU_NO                                             " & vbNewLine _
                                         & ",ZAI.ZONE_CD                                             " & vbNewLine _
                                         & ",ZAI.LOCA                                                " & vbNewLine _
                                         & ",ZAI.ALLOC_CAN_NB                                        " & vbNewLine


    ''' <summary>
    ''' ORDER BY（検索時　ジェイティ物流）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_JT As String = "ORDER BY                                       " & vbNewLine _
                                         & " ZAI.DEST_CD_P DESC                                      " & vbNewLine _
                                         & ",ZAI.ALLOC_PRIORITY                                      " & vbNewLine _
                                         & ",ZAI.LOT_NO                                              " & vbNewLine _
                                         & ",ZAI.LT_DATE                                             " & vbNewLine _
                                         & ",ZAI.GOODS_CRT_DATE                                      " & vbNewLine _
                                         & ",INKA_DATE2                                              " & vbNewLine _
                                         & ",ZAI.SERIAL_NO                                           " & vbNewLine _
                                         & ",ZAI.ALLOC_CAN_NB                                        " & vbNewLine _
                                         & ",ZAI.TOU_NO                                              " & vbNewLine _
                                         & ",ZAI.SITU_NO                                             " & vbNewLine _
                                         & ",ZAI.ZONE_CD                                             " & vbNewLine _
                                         & ",ZAI.LOCA                                                " & vbNewLine
    '要望番号:1592 terakawa 2012.11.15 End

#If True Then ' フィルメニッヒ セミEDI対応  20161003 added inoue
    ''' <summary>
    ''' ORDER BY（商品状態設定有優先）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_FIR As String _
        = "ORDER BY                                                 " & vbNewLine _
        & " YOJITU                                                  " & vbNewLine _
        & ",ZAI.DEST_CD_P DESC                                      " & vbNewLine _
        & ",ZAI.ALLOC_PRIORITY                                      " & vbNewLine _
        & ",EXISTS_GOODS_COND DESC                                  " & vbNewLine _
        & ",INKA_DATE2                                              " & vbNewLine _
        & ",ZAI.LT_DATE                                             " & vbNewLine _
        & ",ZAI.GOODS_CRT_DATE                                      " & vbNewLine _
        & ",INKA_L.INKA_STATE_KB DESC                               " & vbNewLine _
        & ",ZAI.LOT_NO                                              " & vbNewLine _
        & ",ZAI.SERIAL_NO                                           " & vbNewLine _
        & ",ZAI.ALLOC_CAN_NB                                        " & vbNewLine _
        & ",ZAI.TOU_NO                                              " & vbNewLine _
        & ",ZAI.SITU_NO                                             " & vbNewLine _
        & ",ZAI.ZONE_CD                                             " & vbNewLine _
        & ",ZAI.LOCA                                                " & vbNewLine
#End If



    '要望番号:1971 kobayashi 2013.03.27 Start
    ''' <summary>
    ''' ORDER BY（検索時　ロンザ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_LONZA As String = "ORDER BY                                    " & vbNewLine _
                                         & " CASE WHEN ZAI.ALLOC_PRIORITY = '20' THEN '00' ELSE ZAI.ALLOC_PRIORITY END " & vbNewLine _
                                         & ",YOJITU                                                  " & vbNewLine _
                                         & ",ZAI.DEST_CD_P DESC                                      " & vbNewLine _
                                         & ",ZAI.LT_DATE                                             " & vbNewLine _
                                         & ",ZAI.GOODS_CRT_DATE                                      " & vbNewLine _
                                         & ",INKA_DATE2                                              " & vbNewLine _
                                         & ",ZAI.LOT_NO                                              " & vbNewLine _
                                         & ",ZAI.SERIAL_NO                                           " & vbNewLine _
                                         & ",ZAI.ALLOC_CAN_NB                                        " & vbNewLine _
                                         & ",ZAI.TOU_NO                                              " & vbNewLine _
                                         & ",ZAI.SITU_NO                                             " & vbNewLine _
                                         & ",ZAI.ZONE_CD                                             " & vbNewLine _
                                         & ",ZAI.LOCA                                                " & vbNewLine
    '要望番号:1971 kobayashi 2013.03.27 End

    '2017.09.13 アクサルタ特殊在庫ソート対応START
    Private Const SQL_SELECT_ORDER_BY_AXALTA As String = "ORDER BY                                    " & vbNewLine _
                                         & " CASE WHEN ZAI.ALLOC_PRIORITY = '20' THEN '00' ELSE ZAI.ALLOC_PRIORITY END " & vbNewLine _
                                         & ",YOJITU                                                  " & vbNewLine _
                                         & ",ZAI.DEST_CD_P DESC                                      " & vbNewLine _
                                         & ",CASE WHEN CUSTCOND.JOTAI_NM IS NULL                     " & vbNewLine _
                                         & "      THEN '0'                                           " & vbNewLine _
                                         & "      ELSE '1'                                           " & vbNewLine _
                                         & "      END                                                " & vbNewLine _
                                         & ",ZAI.LT_DATE                                             " & vbNewLine _
                                         & ",ZAI.GOODS_CRT_DATE                                      " & vbNewLine _
                                         & ",CASE WHEN ZAI.LOCA = '' THEN '1'                        " & vbNewLine _
                                         & "      ELSE '0'                                           " & vbNewLine _
                                         & "      END                                                " & vbNewLine _
                                         & ",INKA_DATE2                                              " & vbNewLine _
                                         & "--,CASE WHEN TOU_NO IN ('01') THEN '1'                     " & vbNewLine _
                                         & "--      WHEN TOU_NO IN ('08','81','82','83') THEN '2'      " & vbNewLine _
                                         & "--      ELSE '0'                                           " & vbNewLine _
                                         & "--      END                                                " & vbNewLine _
                                         & ",CASE WHEN ZAI.ALLOC_CAN_NB = @BACKLOG_NB                " & vbNewLine _
                                         & "      THEN '0'                                           " & vbNewLine _
                                         & "      WHEN ZAI.ALLOC_CAN_NB > @BACKLOG_NB                " & vbNewLine _
                                         & "      THEN '1'                                           " & vbNewLine _
                                         & "      ELSE '2'                                           " & vbNewLine _
                                         & "      END                                                " & vbNewLine _
                                         & ",ZAI.LOT_NO                                              " & vbNewLine _
                                         & ",ZAI.SERIAL_NO                                           " & vbNewLine _
                                         & ",ZAI.ALLOC_CAN_NB                                        " & vbNewLine _
                                         & ",ZAI.TOU_NO                                              " & vbNewLine _
                                         & ",ZAI.SITU_NO                                             " & vbNewLine _
                                         & ",ZAI.ZONE_CD                                             " & vbNewLine _
                                         & ",ZAI.LOCA                                                " & vbNewLine

    ''' <summary>
    ''' 在庫データ データ抽出用（他荷主検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_TANINUSI As String = " SELECT                                                                     " & vbNewLine _
                                            & " ZAI.NRS_BR_CD                                       AS NRS_BR_CD           " & vbNewLine _
                                            & ",ZAI.ZAI_REC_NO                                      AS ZAI_REC_NO          " & vbNewLine _
                                            & ",ZAI.WH_CD                                           AS WH_CD     	       " & vbNewLine _
                                            & ",ZAI.TOU_NO                                          AS TOU_NO              " & vbNewLine _
                                            & ",ZAI.SITU_NO                                         AS SITU_NO	           " & vbNewLine _
                                            & ",ZAI.ZONE_CD                                         AS ZONE_CD             " & vbNewLine _
                                            & ",ZAI.LOCA                                            AS LOCA                " & vbNewLine _
                                            & ",ZAI.LOT_NO                                          AS LOT_NO              " & vbNewLine _
                                            & ",ZAI.CUST_CD_L                                       AS CUST_CD_L           " & vbNewLine _
                                            & ",ZAI.CUST_CD_M                                       AS CUST_CD_M           " & vbNewLine _
                                            & ",ZAI.GOODS_CD_NRS                                    AS GOODS_CD_NRS        " & vbNewLine _
                                            & ",ZAI.GOODS_KANRI_NO                                  AS GOODS_KANRI_NO      " & vbNewLine _
                                            & ",ZAI.INKA_NO_L                                       AS INKA_NO_L           " & vbNewLine _
                                            & ",ZAI.INKA_NO_M                                       AS INKA_NO_M           " & vbNewLine _
                                            & ",ZAI.INKA_NO_S                                       AS INKA_NO_S           " & vbNewLine _
                                            & ",ZAI.ALLOC_PRIORITY                                  AS ALLOC_PRIORITY      " & vbNewLine _
                                            & ",ZAI.RSV_NO                                          AS RSV_NO              " & vbNewLine _
                                            & ",ZAI.SERIAL_NO                                       AS SERIAL_NO           " & vbNewLine _
                                            & ",ZAI.HOKAN_YN                                        AS HOKAN_YN            " & vbNewLine _
                                            & ",ZAI.TAX_KB                                          AS TAX_KB              " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_1                                 AS GOODS_COND_KB_1     " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_2                                 AS GOODS_COND_KB_2     " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_3                                 AS GOODS_COND_KB_3     " & vbNewLine _
                                            & ",ZAI.OFB_KB                                          AS OFB_KB              " & vbNewLine _
                                            & ",ZAI.SPD_KB                                          AS SPD_KB              " & vbNewLine _
                                            & ",ZAI.REMARK_OUT                                      AS REMARK_OUT          " & vbNewLine _
                                            & ",ZAI.PORA_ZAI_NB                                     AS PORA_ZAI_NB         " & vbNewLine _
                                            & ",ZAI.ALCTD_NB                                        AS ALCTD_NB            " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_NB                                    AS ALLOC_CAN_NB        " & vbNewLine _
                                            & ",ZAI.IRIME                                           AS IRIME               " & vbNewLine _
                                            & ",ZAI.PORA_ZAI_QT                                     AS PORA_ZAI_QT         " & vbNewLine _
                                            & ",ZAI.ALCTD_QT                                        AS ALCTD_QT            " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT        " & vbNewLine _
                                            & ",ZAI.INKO_DATE                                       AS INKO_DATE           " & vbNewLine _
                                            & ",ZAI.INKO_PLAN_DATE                                  AS INKO_PLAN_DATE      " & vbNewLine _
                                            & ",ZAI.ZERO_FLAG                                       AS ZERO_FLAG           " & vbNewLine _
                                            & ",ZAI.LT_DATE                                         AS LT_DATE             " & vbNewLine _
                                            & ",ZAI.GOODS_CRT_DATE                                  AS GOODS_CRT_DATE      " & vbNewLine _
                                            & ",ZAI.DEST_CD_P                                       AS DEST_CD_P           " & vbNewLine _
                                            & ",ZAI.REMARK                                          AS REMARK              " & vbNewLine _
                                            & ",ZAI.SMPL_FLAG                                       AS SMPL_FLAG           " & vbNewLine _
                                            & ",Z1.KBN_NM1                                          AS GOODS_COND_NM_1     " & vbNewLine _
                                            & ",Z2.KBN_NM1                                          AS GOODS_COND_NM_2     " & vbNewLine _
                                            & ",CUSTCOND.JOTAI_NM                                   AS GOODS_COND_NM_3     " & vbNewLine _
                                            & ",Z3.KBN_NM1                                          AS ALLOC_PRIORITY_NM   " & vbNewLine _
                                            & ",Z4.KBN_NM1                                          AS OFB_KB_NM           " & vbNewLine _
                                            & ",Z5.KBN_NM1                                          AS SPD_KB_NM           " & vbNewLine _
                                            & ",Z6.KBN_NM1                                          AS HIKIATE_ALERT_NM    " & vbNewLine _
                                            & ",Z7.KBN_NM1                                          AS TAX_KB_NM           " & vbNewLine _
                                            & ",Z8.KBN_NM1                                          AS NB_UT_NM            " & vbNewLine _
                                            & ",Z9.KBN_NM1                                          AS IRIME_UT_NM         " & vbNewLine _
                                            & ",GOODS.CONSUME_PERIOD_DATE                           AS CONSUME_PERIOD_DATE " & vbNewLine _
                                            & ",''                                                  AS BUYER_ORD_NO_DTL    " & vbNewLine _
                                            & ",DEST.DEST_NM                                        AS DEST_NM             " & vbNewLine _
                                            & ",GOODS.GOODS_CD_CUST                                 AS GOODS_CD_CUST       " & vbNewLine _
                                            & ",GOODS.GOODS_NM_1                                    AS GOODS_NM_1          " & vbNewLine _
                                            & ",GOODS.OUTKA_ATT                                     AS OUTKA_ATT           " & vbNewLine _
                                            & ",GOODS.SEARCH_KEY_1                                  AS SEARCH_KEY_1        " & vbNewLine _
                                            & ",GOODS.UNSO_ONDO_KB                                  AS UNSO_ONDO_KB        " & vbNewLine _
                                            & ",GOODS.PKG_UT                                        AS PKG_UT              " & vbNewLine _
                                            & ",GOODS.STD_IRIME_NB                                  AS STD_IRIME_NB        " & vbNewLine _
                                            & ",GOODS.STD_WT_KGS                                    AS STD_WT_KGS          " & vbNewLine _
                                            & ",GOODS.TARE_YN                                       AS TARE_YN             " & vbNewLine _
                                            & ",GOODS.HIKIATE_ALERT_YN                              AS HIKIATE_ALERT_YN    " & vbNewLine _
                                            & ",GOODS.PKG_NB                                        AS PKG_NB              " & vbNewLine _
                                            & ",GOODS.STD_IRIME_UT                                  AS STD_IRIME_UT        " & vbNewLine _
                                            & ",GOODS.NB_UT                                         AS NB_UT               " & vbNewLine _
                                            & ",INKA_L.INKA_DATE                                    AS INKA_DATE           " & vbNewLine _
                                            & ",GOODS.CUST_CD_S                                     AS CUST_CD_S           " & vbNewLine _
                                            & ",GOODS.CUST_CD_SS                                    AS CUST_CD_SS          " & vbNewLine _
                                            & ",ZAI.SYS_UPD_DATE                                    AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",ZAI.SYS_UPD_TIME                                    AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",FURIGOODS.CD_NRS                                    AS GOODS_CD_NRS_FROM   " & vbNewLine _
                                            & ",IDO.IDO_DATE                                        AS IDO_DATE            " & vbNewLine _
                                            & ",INKA_L.HOKAN_STR_DATE                               AS HOKAN_STR_DATE      " & vbNewLine _
                                            & ",GOODS.COA_YN                                        AS COA_YN              " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_1                         AS OUTKA_KAKO_SAGYO_KB_1 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_2                         AS OUTKA_KAKO_SAGYO_KB_2 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_3                         AS OUTKA_KAKO_SAGYO_KB_3 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_4                         AS OUTKA_KAKO_SAGYO_KB_4 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_5                         AS OUTKA_KAKO_SAGYO_KB_5 " & vbNewLine _
                                            & ",INKA_L.INKA_STATE_KB                                AS INKA_STATE_KB       " & vbNewLine _
                                            & ",CASE WHEN ZAI.INKO_DATE IS NULL OR ZAI.INKO_DATE = ''                      " & vbNewLine _
                                            & "      THEN '1'                                                              " & vbNewLine _
                                            & "      ELSE '0'                                                              " & vbNewLine _
                                            & " END                                                 AS YOJITU              " & vbNewLine _
                                            & ",GOODS.SIZE_KB                                       AS SIZE_KB             " & vbNewLine _
                                            & ",GOODS.CUST_CD_L                                     AS CUST_CD_L_GOODS     " & vbNewLine _
                                            & ",GOODS.CUST_CD_M                                     AS CUST_CD_M_GOODS     " & vbNewLine _
                                            & ",CASE WHEN INKA_L.INKA_STATE_KB = '50' AND INKA_L.FURI_NO <> ''             " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                 " & vbNewLine _
                                            & "      WHEN INKA_L.INKA_STATE_KB = '50' AND INKA_L.FURI_NO = ''              " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                    " & vbNewLine _
                                            & "      WHEN INKA_L.INKA_STATE_KB = '90' AND INKA_L.FURI_NO <> ''             " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                 " & vbNewLine _
                                            & "      WHEN INKA_L.INKA_STATE_KB = '90' AND INKA_L.FURI_NO = ''              " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                    " & vbNewLine _
                                            & "      ELSE INKA_L.INKA_DATE                                                 " & vbNewLine _
                                            & " END                                                 AS INKA_DATE2          " & vbNewLine _
                                            & ",GOODSDETAILS.SET_NAIYO                              AS INKA_DATE_KANRI_KB  " & vbNewLine _
                                            & ",Z5.VALUE1                                           AS SPD_KB_FLG          " & vbNewLine _
                                            & ",GOODS.STD_IRIME_UT                                  AS IRIME_UT            " & vbNewLine _
                                            & ",GOODS.SHOBO_CD                                      AS SHOBO_CD            " & vbNewLine _
                                            & ",Z10.KBN_NM1 + ' ' + SBO.HINMEI                      AS SHOBO_NM            " & vbNewLine _
                                            & ",ZAI.BYK_KEEP_GOODS_CD                               AS BYK_KEEP_GOODS_CD   " & vbNewLine _
                                            & ",KEEP_GOODS.KBN_NM1                                  AS KEEP_GOODS_NM       " & vbNewLine _
                                            & ",ISNULL(CUST_DETAILS.SET_NAIYO, '0')                 AS IS_BYK_KEEP_GOODS_CD" & vbNewLine


    ''' <summary>
    ''' FROM（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_TANINUSI As String = "FROM                                                   " & vbNewLine _
                                         & "$LM_TRN$..D_ZAI_TRS  ZAI                                  " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_CUSTCOND    CUSTCOND                " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = CUSTCOND.NRS_BR_CD                 " & vbNewLine _
                                         & "AND    ZAI.CUST_CD_L = CUSTCOND.CUST_CD_L                 " & vbNewLine _
                                         & "AND    ZAI.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD            " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_1 = Z1.KBN_CD                    " & vbNewLine _
                                         & "AND    Z1.KBN_GROUP_CD = 'S005'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z2                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_2 = Z2.KBN_CD                    " & vbNewLine _
                                         & "AND    Z2.KBN_GROUP_CD = 'S006'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z3                           " & vbNewLine _
                                         & "ON     ZAI.ALLOC_PRIORITY = Z3.KBN_CD                     " & vbNewLine _
                                         & "AND    Z3.KBN_GROUP_CD = 'W001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z4                           " & vbNewLine _
                                         & "ON     ZAI.OFB_KB = Z4.KBN_CD                             " & vbNewLine _
                                         & "AND    Z4.KBN_GROUP_CD = 'B002'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z5                           " & vbNewLine _
                                         & "ON     ZAI.SPD_KB = Z5.KBN_CD                             " & vbNewLine _
                                         & "AND    Z5.KBN_GROUP_CD = 'H003'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_FURI_GOODS    FURIGOODS             " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = FURIGOODS.NRS_BR_CD                " & vbNewLine _
                                         & "AND    ZAI.GOODS_CD_NRS = FURIGOODS.CD_NRS_TO             " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS  GOODS                        " & vbNewLine _
                                         & "ON     FURIGOODS.NRS_BR_CD = GOODS.NRS_BR_CD              " & vbNewLine _
                                         & "AND    FURIGOODS.CD_NRS = GOODS.GOODS_CD_NRS              " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z6                           " & vbNewLine _
                                         & "ON     GOODS.HIKIATE_ALERT_YN = Z6.KBN_CD                 " & vbNewLine _
                                         & "AND    Z6.KBN_GROUP_CD = 'U009'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN AS Z7                           " & vbNewLine _
                                         & "ON     ZAI.TAX_KB  = Z7.KBN_CD                            " & vbNewLine _
                                         & "AND    Z7.KBN_GROUP_CD = 'Z001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z8                           " & vbNewLine _
                                         & "ON     GOODS.NB_UT = Z8.KBN_CD                            " & vbNewLine _
                                         & "AND    Z8.KBN_GROUP_CD = 'K002'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z9                           " & vbNewLine _
                                         & "ON     GOODS.STD_IRIME_UT  = Z9.KBN_CD                    " & vbNewLine _
                                         & "AND    Z9.KBN_GROUP_CD = 'I001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_DEST    DEST                        " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = DEST.NRS_BR_CD                     " & vbNewLine _
                                         & "AND    ZAI.CUST_CD_L = DEST.CUST_CD_L                     " & vbNewLine _
                                         & "AND    ZAI.DEST_CD_P = DEST.DEST_CD                       " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..B_INKA_L AS INKA_L                    " & vbNewLine _
                                         & "ON  INKA_L.NRS_BR_CD = ZAI.NRS_BR_CD                      " & vbNewLine _
                                         & "AND INKA_L.INKA_NO_L = ZAI.INKA_NO_L                      " & vbNewLine _
                                         & "AND INKA_L.SYS_DEL_FLG = 0                                " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..D_IDO_TRS IDO                         " & vbNewLine _
                                         & "ON  IDO.NRS_BR_CD = ZAI.NRS_BR_CD                         " & vbNewLine _
                                         & "AND IDO.N_ZAI_REC_NO = ZAI.ZAI_REC_NO                     " & vbNewLine _
                                         & "AND IDO.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                         & "AND IDO.REC_NO <> ''                                      " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GOODSDETAILS          " & vbNewLine _
                                         & "ON  GOODSDETAILS.NRS_BR_CD = ZAI.NRS_BR_CD                " & vbNewLine _
                                         & "AND GOODSDETAILS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS          " & vbNewLine _
                                         & "AND GOODSDETAILS.SUB_KB = '09'                            " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_SHOBO SBO                           " & vbNewLine _
                                         & "ON  SBO.SHOBO_CD    = GOODS.SHOBO_CD                      " & vbNewLine _
                                         & "AND SBO.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN Z10                             " & vbNewLine _
                                         & "ON  Z10.KBN_GROUP_CD = 'S004'                             " & vbNewLine _
                                         & "AND Z10.KBN_CD       = SBO.RUI                            " & vbNewLine _
                                         & "AND Z10.SYS_DEL_FLG  = '0'                                " & vbNewLine _
                                         & "LEFT JOIN                                                 " & vbNewLine _
                                         & "    $LM_MST$..Z_KBN AS KEEP_GOODS                         " & vbNewLine _
                                         & "        ON  KEEP_GOODS.KBN_CD = ZAI.BYK_KEEP_GOODS_CD     " & vbNewLine _
                                         & "        AND KEEP_GOODS.KBN_GROUP_CD = 'B039'              " & vbNewLine _
                                         & "LEFT JOIN                                                        " & vbNewLine _
                                         & "   (SELECT                                                       " & vbNewLine _
                                         & "          NRS_BR_CD                                              " & vbNewLine _
                                         & "        , CUST_CD                                                " & vbNewLine _
                                         & "        , CUST_CD_EDA                                            " & vbNewLine _
                                         & "        , SET_NAIYO                                              " & vbNewLine _
                                         & "    FROM                                                         " & vbNewLine _
                                         & "        $LM_MST$..M_CUST_DETAILS                                 " & vbNewLine _
                                         & "    ) AS CUST_DETAILS                                            " & vbNewLine _
                                         & "        ON (        CUST_DETAILS.NRS_BR_CD                       " & vbNewLine _
                                         & "            + ',' + CUST_DETAILS.CUST_CD                         " & vbNewLine _
                                         & "            + ',' + CUST_DETAILS.CUST_CD_EDA) =                  " & vbNewLine _
                                         & "   (SELECT                                                       " & vbNewLine _
                                         & "        ISNULL(MIN(         CUST_DETAILS.NRS_BR_CD               " & vbNewLine _
                                         & "                    + ',' + CUST_DETAILS.CUST_CD                 " & vbNewLine _
                                         & "                    + ',' + CUST_DETAILS.CUST_CD_EDA), '') AS PK " & vbNewLine _
                                         & "    FROM                                                         " & vbNewLine _
                                         & "        $LM_MST$..M_CUST_DETAILS AS CUST_DETAILS                 " & vbNewLine _
                                         & "    WHERE                                                        " & vbNewLine _
                                         & "        CUST_DETAILS.NRS_BR_CD = ZAI.NRS_BR_CD                   " & vbNewLine _
                                         & "    AND CUST_DETAILS.CUST_CD = ZAI.CUST_CD_L + ZAI.CUST_CD_M     " & vbNewLine _
                                         & "    AND CUST_DETAILS.CUST_CLASS = '01'                           " & vbNewLine _
                                         & "    AND CUST_DETAILS.SUB_KB = '1Z'                               " & vbNewLine _
                                         & "    AND CUST_DETAILS.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "    GROUP BY                                                     " & vbNewLine _
                                         & "          CUST_DETAILS.NRS_BR_CD                                 " & vbNewLine _
                                         & "        , CUST_DETAILS.CUST_CD                                   " & vbNewLine _
                                         & "    )                                                            " & vbNewLine

    ''' <summary>
    ''' GROUP BY（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_BY_TANINUSI As String = "GROUP BY                                          " & vbNewLine _
                                     & " ZAI.NRS_BR_CD                                                  " & vbNewLine _
                                     & ",ZAI.ZAI_REC_NO                                                 " & vbNewLine _
                                     & ",ZAI.WH_CD                                                      " & vbNewLine _
                                     & ",ZAI.TOU_NO                                                     " & vbNewLine _
                                     & ",ZAI.SITU_NO                                                    " & vbNewLine _
                                     & ",ZAI.ZONE_CD                                                    " & vbNewLine _
                                     & ",ZAI.LOCA                                                       " & vbNewLine _
                                     & ",ZAI.LOT_NO                                                     " & vbNewLine _
                                     & ",ZAI.CUST_CD_L                                                  " & vbNewLine _
                                     & ",ZAI.CUST_CD_M                                                  " & vbNewLine _
                                     & ",ZAI.GOODS_CD_NRS                                               " & vbNewLine _
                                     & ",ZAI.GOODS_KANRI_NO                                             " & vbNewLine _
                                     & ",ZAI.INKA_NO_L                                                  " & vbNewLine _
                                     & ",ZAI.INKA_NO_M                                                  " & vbNewLine _
                                     & ",ZAI.INKA_NO_S                                                  " & vbNewLine _
                                     & ",ZAI.ALLOC_PRIORITY                                             " & vbNewLine _
                                     & ",ZAI.RSV_NO                                                     " & vbNewLine _
                                     & ",ZAI.SERIAL_NO                                                  " & vbNewLine _
                                     & ",ZAI.HOKAN_YN                                                   " & vbNewLine _
                                     & ",ZAI.TAX_KB                                                     " & vbNewLine _
                                     & ",ZAI.GOODS_COND_KB_1                                            " & vbNewLine _
                                     & ",ZAI.GOODS_COND_KB_2                                            " & vbNewLine _
                                     & ",ZAI.GOODS_COND_KB_3                                            " & vbNewLine _
                                     & ",ZAI.OFB_KB                                                     " & vbNewLine _
                                     & ",ZAI.SPD_KB                                                     " & vbNewLine _
                                     & ",ZAI.REMARK_OUT                                                 " & vbNewLine _
                                     & ",ZAI.PORA_ZAI_NB                                                " & vbNewLine _
                                     & ",ZAI.ALCTD_NB                                                   " & vbNewLine _
                                     & ",ZAI.ALLOC_CAN_NB                                               " & vbNewLine _
                                     & ",ZAI.IRIME                                                      " & vbNewLine _
                                     & ",ZAI.PORA_ZAI_QT                                                " & vbNewLine _
                                     & ",ZAI.ALCTD_QT                                                   " & vbNewLine _
                                     & ",ZAI.ALLOC_CAN_QT                                               " & vbNewLine _
                                     & ",ZAI.INKO_DATE                                                  " & vbNewLine _
                                     & ",ZAI.INKO_PLAN_DATE                                             " & vbNewLine _
                                     & ",ZAI.ZERO_FLAG                                                  " & vbNewLine _
                                     & ",ZAI.LT_DATE                                                    " & vbNewLine _
                                     & ",ZAI.GOODS_CRT_DATE                                             " & vbNewLine _
                                     & ",ZAI.DEST_CD_P                                                  " & vbNewLine _
                                     & ",ZAI.REMARK                                                     " & vbNewLine _
                                     & ",ZAI.SMPL_FLAG                                                  " & vbNewLine _
                                     & ",Z1.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z2.KBN_NM1                                                     " & vbNewLine _
                                     & ",CUSTCOND.JOTAI_NM                                              " & vbNewLine _
                                     & ",Z3.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z4.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z5.KBN_NM1                                                     " & vbNewLine _
                                     & ",GOODS.CONSUME_PERIOD_DATE                                      " & vbNewLine _
                                     & ",Z6.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z7.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z8.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z9.KBN_NM1                                                     " & vbNewLine _
                                     & ",DEST.DEST_NM                                                   " & vbNewLine _
                                     & ",GOODS.GOODS_CD_CUST                                            " & vbNewLine _
                                     & ",GOODS.GOODS_NM_1                                               " & vbNewLine _
                                     & ",GOODS.OUTKA_ATT                                                " & vbNewLine _
                                     & ",GOODS.SEARCH_KEY_1                                             " & vbNewLine _
                                     & ",GOODS.UNSO_ONDO_KB                                             " & vbNewLine _
                                     & ",GOODS.PKG_UT                                                   " & vbNewLine _
                                     & ",GOODS.STD_IRIME_NB                                             " & vbNewLine _
                                     & ",GOODS.STD_WT_KGS                                               " & vbNewLine _
                                     & ",GOODS.TARE_YN                                                  " & vbNewLine _
                                     & ",GOODS.HIKIATE_ALERT_YN                                         " & vbNewLine _
                                     & ",GOODS.PKG_NB                                                   " & vbNewLine _
                                     & ",GOODS.STD_IRIME_UT                                             " & vbNewLine _
                                     & ",GOODS.NB_UT                                                    " & vbNewLine _
                                     & ",INKA_L.INKA_DATE                                               " & vbNewLine _
                                     & ",GOODS.CUST_CD_S                                                " & vbNewLine _
                                     & ",GOODS.CUST_CD_SS                                               " & vbNewLine _
                                     & ",ZAI.SYS_UPD_DATE                                               " & vbNewLine _
                                     & ",ZAI.SYS_UPD_TIME                                               " & vbNewLine _
                                     & ",FURIGOODS.CD_NRS                                               " & vbNewLine _
                                     & ",IDO.IDO_DATE                                                   " & vbNewLine _
                                     & ",INKA_L.INKO_DATE                                               " & vbNewLine _
                                     & ",INKA_L.HOKAN_STR_DATE                                          " & vbNewLine _
                                     & ",GOODS.COA_YN                                                   " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_1                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_2                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_3                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_4                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_5                                    " & vbNewLine _
                                     & ",INKA_L.INKA_STATE_KB                                           " & vbNewLine _
                                     & ",GOODS.SIZE_KB                                                  " & vbNewLine _
                                     & ",GOODS.CUST_CD_L                                                " & vbNewLine _
                                     & ",GOODS.CUST_CD_M                                                " & vbNewLine _
                                     & ",INKA_L.FURI_NO                                                 " & vbNewLine _
                                     & ",GOODSDETAILS.SET_NAIYO                                         " & vbNewLine _
                                     & ",Z5.VALUE1                                                      " & vbNewLine _
                                     & ",GOODS.SHOBO_CD                                                 " & vbNewLine _
                                     & ",Z10.KBN_NM1 + ' ' + SBO.HINMEI                                 " & vbNewLine _
                                     & ",ZAI.BYK_KEEP_GOODS_CD                                          " & vbNewLine _
                                     & ",KEEP_GOODS.KBN_NM1                                             " & vbNewLine _
                                     & ",CUST_DETAILS.SET_NAIYO                                         " & vbNewLine

    ''' <summary>
    ''' ORDER BY（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_TANINUSI As String = "ORDER BY                                 " & vbNewLine _
                                         & " ZAI.DEST_CD_P DESC                                      " & vbNewLine _
                                         & ",ZAI.ALLOC_PRIORITY                                      " & vbNewLine _
                                         & ",ZAI.LT_DATE                                             " & vbNewLine _
                                         & ",ZAI.GOODS_CRT_DATE                                      " & vbNewLine _
                                         & ",INKA_DATE2                                              " & vbNewLine _
                                         & ",ZAI.LOT_NO                                              " & vbNewLine _
                                         & ",ZAI.SERIAL_NO                                           " & vbNewLine _
                                         & ",ZAI.ALLOC_CAN_NB                                        " & vbNewLine

    ''' <summary>
    ''' 在庫データ データ抽出用（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_LMC010 As String = " SELECT                                                                                             " & vbNewLine _
                                    & " ZAI.NRS_BR_CD                                       AS NRS_BR_CD                                   " & vbNewLine _
                                    & ",ZAI.ZAI_REC_NO                                      AS ZAI_REC_NO                                  " & vbNewLine _
                                    & ",ZAI.WH_CD                                           AS WH_CD                                       " & vbNewLine _
                                    & ",ZAI.TOU_NO                                          AS TOU_NO                                      " & vbNewLine _
                                    & ",ZAI.SITU_NO                                         AS SITU_NO                                     " & vbNewLine _
                                    & ",ZAI.ZONE_CD                                         AS ZONE_CD                                     " & vbNewLine _
                                    & ",ZAI.LOCA                                            AS LOCA                                        " & vbNewLine _
                                    & ",ZAI.LOT_NO                                          AS LOT_NO                                      " & vbNewLine _
                                    & ",ZAI.CUST_CD_L                                       AS CUST_CD_L                                   " & vbNewLine _
                                    & ",ZAI.CUST_CD_M                                       AS CUST_CD_M                                   " & vbNewLine _
                                    & ",ZAI.GOODS_CD_NRS                                    AS GOODS_CD_NRS                                " & vbNewLine _
                                    & ",ZAI.GOODS_KANRI_NO                                  AS GOODS_KANRI_NO                              " & vbNewLine _
                                    & ",ZAI.INKA_NO_L                                       AS INKA_NO_L                                   " & vbNewLine _
                                    & ",ZAI.INKA_NO_M                                       AS INKA_NO_M                                   " & vbNewLine _
                                    & ",ZAI.INKA_NO_S                                       AS INKA_NO_S                                   " & vbNewLine _
                                    & ",ZAI.ALLOC_PRIORITY                                  AS ALLOC_PRIORITY                              " & vbNewLine _
                                    & ",ZAI.RSV_NO                                          AS RSV_NO                                      " & vbNewLine _
                                    & ",ZAI.SERIAL_NO                                       AS SERIAL_NO                                   " & vbNewLine _
                                    & ",ZAI.HOKAN_YN                                        AS HOKAN_YN                                    " & vbNewLine _
                                    & ",ZAI.TAX_KB                                          AS TAX_KB                                      " & vbNewLine _
                                    & ",ZAI.GOODS_COND_KB_1                                 AS GOODS_COND_KB_1                             " & vbNewLine _
                                    & ",ZAI.GOODS_COND_KB_2                                 AS GOODS_COND_KB_2                             " & vbNewLine _
                                    & ",ZAI.GOODS_COND_KB_3                                 AS GOODS_COND_KB_3                             " & vbNewLine _
                                    & ",ZAI.OFB_KB                                          AS OFB_KB                                      " & vbNewLine _
                                    & ",ZAI.SPD_KB                                          AS SPD_KB                                      " & vbNewLine _
                                    & ",ZAI.REMARK_OUT                                      AS REMARK_OUT                                  " & vbNewLine _
                                    & ",ZAI.PORA_ZAI_NB                                     AS PORA_ZAI_NB                                 " & vbNewLine _
                                    & ",ZAI.ALCTD_NB                                        AS ALCTD_NB                                    " & vbNewLine _
                                    & ",ZAI.ALLOC_CAN_NB                                    AS ALLOC_CAN_NB                                " & vbNewLine _
                                    & ",ZAI.IRIME                                           AS IRIME                                       " & vbNewLine _
                                    & ",ZAI.PORA_ZAI_QT                                     AS PORA_ZAI_QT                                 " & vbNewLine _
                                    & ",ZAI.ALCTD_QT                                        AS ALCTD_QT                                    " & vbNewLine _
                                    & ",ZAI.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT                                " & vbNewLine _
                                    & ",ZAI.INKO_DATE                                       AS INKO_DATE                                   " & vbNewLine _
                                    & ",ZAI.INKO_PLAN_DATE                                  AS INKO_PLAN_DATE                              " & vbNewLine _
                                    & ",ZAI.ZERO_FLAG                                       AS ZERO_FLAG                                   " & vbNewLine _
                                    & ",ZAI.LT_DATE                                         AS LT_DATE                                     " & vbNewLine _
                                    & ",ZAI.GOODS_CRT_DATE                                  AS GOODS_CRT_DATE                              " & vbNewLine _
                                    & ",ZAI.DEST_CD_P                                       AS DEST_CD_P                                   " & vbNewLine _
                                    & ",ZAI.REMARK                                          AS REMARK                                      " & vbNewLine _
                                    & ",ZAI.SMPL_FLAG                                       AS SMPL_FLAG                                   " & vbNewLine _
                                    & ",''                                          AS GOODS_COND_NM_1                                     " & vbNewLine _
                                    & ",''                                          AS GOODS_COND_NM_2                                     " & vbNewLine _
                                    & ",''                                   AS GOODS_COND_NM_3                                            " & vbNewLine _
                                    & ",''                                          AS ALLOC_PRIORITY_NM                                   " & vbNewLine _
                                    & ",''                                          AS OFB_KB_NM                                           " & vbNewLine _
                                    & ",''                                          AS SPD_KB_NM                                           " & vbNewLine _
                                    & ",''                                          AS HIKIATE_ALERT_NM                                    " & vbNewLine _
                                    & ",''                                          AS TAX_KB_NM                                           " & vbNewLine _
                                    & ",''                                          AS NB_UT_NM                                            " & vbNewLine _
                                    & ",''                                          AS IRIME_UT_NM                                         " & vbNewLine _
                                    & ",GOODS.CONSUME_PERIOD_DATE                           AS CONSUME_PERIOD_DATE                         " & vbNewLine _
                                    & ",''                            AS BUYER_ORD_NO_DTL                                                  " & vbNewLine _
                                    & ",''                                        AS DEST_NM                                               " & vbNewLine _
                                    & ",''                                 AS GOODS_CD_CUST                                                " & vbNewLine _
                                    & ",''                                    AS GOODS_NM_1                                                " & vbNewLine _
                                    & ",''                                     AS OUTKA_ATT                                                " & vbNewLine _
                                    & ",''                                  AS SEARCH_KEY_1                                                " & vbNewLine _
                                    & ",''                                  AS UNSO_ONDO_KB                                                " & vbNewLine _
                                    & ",GOODS.PKG_UT                              AS PKG_UT                                                " & vbNewLine _
                                    & ",GOODS.STD_IRIME_NB                                  AS STD_IRIME_NB                                " & vbNewLine _
                                    & ",GOODS.STD_WT_KGS                                    AS STD_WT_KGS                                  " & vbNewLine _
                                    & ",GOODS.TARE_YN                            AS TARE_YN                                                " & vbNewLine _
                                    & ",''                              AS HIKIATE_ALERT_YN                                                " & vbNewLine _
                                    & ",''                                        AS PKG_NB                                                " & vbNewLine _
                                    & ",''                                  AS STD_IRIME_UT                                                " & vbNewLine _
                                    & ",''                                         AS NB_UT                                                " & vbNewLine _
                                    & ",INKA_L.INKA_DATE                                    AS INKA_DATE                                   " & vbNewLine _
                                    & ",''                                     AS CUST_CD_S                                                " & vbNewLine _
                                    & ",''                                    AS CUST_CD_SS                                                " & vbNewLine _
                                    & ",ZAI.SYS_UPD_DATE                                    AS SYS_UPD_DATE                                " & vbNewLine _
                                    & ",ZAI.SYS_UPD_TIME                                    AS SYS_UPD_TIME                                " & vbNewLine _
                                    & ",''                                                  AS GOODS_CD_NRS_FROM                           " & vbNewLine _
                                    & ",''                                        AS IDO_DATE                                              " & vbNewLine _
                                    & ",INKA_L.HOKAN_STR_DATE                               AS HOKAN_STR_DATE                              " & vbNewLine _
                                    & ",''                                        AS COA_YN                                                " & vbNewLine _
                                    & ",''                         AS OUTKA_KAKO_SAGYO_KB_1                                                " & vbNewLine _
                                    & ",''                         AS OUTKA_KAKO_SAGYO_KB_2                                                " & vbNewLine _
                                    & ",''                         AS OUTKA_KAKO_SAGYO_KB_3                                                " & vbNewLine _
                                    & ",''                         AS OUTKA_KAKO_SAGYO_KB_4                                                " & vbNewLine _
                                    & ",''                         AS OUTKA_KAKO_SAGYO_KB_5                                                " & vbNewLine _
                                    & ",INKA_L.INKA_STATE_KB                                AS INKA_STATE_KB                               " & vbNewLine _
                                    & ",''                                                 AS YOJITU                                       " & vbNewLine _
                                    & ",''                                       AS SIZE_KB                                                " & vbNewLine _
                                    & ",''                                       AS CUST_CD_L_GOODS                                        " & vbNewLine _
                                    & ",''                                       AS CUST_CD_M_GOODS                                        " & vbNewLine _
                                    & ",CASE WHEN INKA_L.INKA_STATE_KB = '50' AND INKA_L.FURI_NO <> ''                                     " & vbNewLine _
                                    & "      THEN ZAI.INKO_DATE                                                                         " & vbNewLine _
                                    & "      WHEN INKA_L.INKA_STATE_KB = '50' AND INKA_L.FURI_NO = ''                                      " & vbNewLine _
                                    & "      THEN ZAI.INKO_DATE                                                                            " & vbNewLine _
                                    & "      WHEN INKA_L.INKA_STATE_KB = '90' AND INKA_L.FURI_NO <> ''                                     " & vbNewLine _
                                    & "      THEN ZAI.INKO_DATE                                                                         " & vbNewLine _
                                    & "      WHEN INKA_L.INKA_STATE_KB = '90' AND INKA_L.FURI_NO = ''                                      " & vbNewLine _
                                    & "      THEN ZAI.INKO_DATE                                                                            " & vbNewLine _
                                    & "      ELSE INKA_L.INKA_DATE                                                                         " & vbNewLine _
                                    & " END                                                 AS INKA_DATE2                                  " & vbNewLine _
                                    & ",GOODSDETAILS.SET_NAIYO                              AS INKA_DATE_KANRI_KB                          " & vbNewLine _
                                    & ",Z1.VALUE1                                           AS SPD_KB_FLG                                  " & vbNewLine _
                                    & ",''                                                  AS SHOBO_CD                                    " & vbNewLine _
                                    & ",''                                                  AS SHOBO_NM                                    " & vbNewLine _
                                    & ",ZAI.BYK_KEEP_GOODS_CD                               AS BYK_KEEP_GOODS_CD                           " & vbNewLine _
                                    & ",KEEP_GOODS.KBN_NM1                                  AS KEEP_GOODS_NM                               " & vbNewLine _
                                    & ",ISNULL(CUST_DETAILS.SET_NAIYO, '0')                 AS IS_BYK_KEEP_GOODS_CD                        " & vbNewLine

    ''' <summary>
    ''' FROM（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_LMC010 As String = "FROM                                                                                     " & vbNewLine _
                                        & "$LM_TRN$..D_ZAI_TRS  ZAI                                                                            " & vbNewLine _
                                        & "LEFT JOIN LM_MST..M_CUSTCOND    CUSTCOND                                                            " & vbNewLine _
                                        & "ON     ZAI.NRS_BR_CD = CUSTCOND.NRS_BR_CD                                                           " & vbNewLine _
                                        & "AND    ZAI.CUST_CD_L = CUSTCOND.CUST_CD_L                                                           " & vbNewLine _
                                        & "AND    ZAI.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD                                                      " & vbNewLine _
                                        & "LEFT JOIN $LM_TRN$..B_INKA_L AS INKA_L                                                              " & vbNewLine _
                                        & "ON  INKA_L.NRS_BR_CD = ZAI.NRS_BR_CD                                                                " & vbNewLine _
                                        & "AND INKA_L.INKA_NO_L = ZAI.INKA_NO_L                                                                " & vbNewLine _
                                        & "AND INKA_L.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                                        & "AND ZAI.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_GOODS  GOODS                                                                  " & vbNewLine _
                                        & "ON     ZAI.NRS_BR_CD = GOODS.NRS_BR_CD                                                              " & vbNewLine _
                                        & "AND    ZAI.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                                        " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GOODSDETAILS                                                    " & vbNewLine _
                                        & "ON  GOODSDETAILS.NRS_BR_CD = ZAI.NRS_BR_CD                                                          " & vbNewLine _
                                        & "AND GOODSDETAILS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS                                                    " & vbNewLine _
                                        & "AND GOODSDETAILS.SUB_KB = '09'                                                                      " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..Z_KBN  Z1                                                                       " & vbNewLine _
                                        & "ON     Z1.KBN_GROUP_CD = 'H003'                                                                     " & vbNewLine _
                                        & "AND    Z1.KBN_CD = ZAI.SPD_KB                                                                       " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..Z_KBN AS DO_KENPIN_CHK                                                          " & vbNewLine _
                                        & "  ON DO_KENPIN_CHK.KBN_GROUP_CD = 'N027'                                                       " & vbNewLine _
                                        & " AND DO_KENPIN_CHK.SYS_DEL_FLG  = '0'                                                          " & vbNewLine _
                                        & " AND DO_KENPIN_CHK.KBN_NM1      = ZAI.NRS_BR_CD                                                " & vbNewLine _
                                        & " AND DO_KENPIN_CHK.KBN_NM2      = ZAI.WH_CD                                                    " & vbNewLine _
                                        & " AND DO_KENPIN_CHK.KBN_NM3      = ZAI.TOU_NO                                                   " & vbNewLine _
                                        & "LEFT JOIN                                                                                           " & vbNewLine _
                                        & "    $LM_MST$..Z_KBN AS KEEP_GOODS                                                                   " & vbNewLine _
                                        & "        ON  KEEP_GOODS.KBN_CD = ZAI.BYK_KEEP_GOODS_CD                                               " & vbNewLine _
                                        & "        AND KEEP_GOODS.KBN_GROUP_CD = 'B039'                                                        " & vbNewLine _
                                        & "LEFT JOIN                                                                                           " & vbNewLine _
                                        & "   (SELECT                                                                                          " & vbNewLine _
                                        & "          NRS_BR_CD                                                                                 " & vbNewLine _
                                        & "        , CUST_CD                                                                                   " & vbNewLine _
                                        & "        , CUST_CD_EDA                                                                               " & vbNewLine _
                                        & "        , SET_NAIYO                                                                                 " & vbNewLine _
                                        & "    FROM                                                                                            " & vbNewLine _
                                        & "        $LM_MST$..M_CUST_DETAILS                                                                    " & vbNewLine _
                                        & "    ) AS CUST_DETAILS                                                                               " & vbNewLine _
                                        & "        ON (        CUST_DETAILS.NRS_BR_CD                                                          " & vbNewLine _
                                        & "            + ',' + CUST_DETAILS.CUST_CD                                                            " & vbNewLine _
                                        & "            + ',' + CUST_DETAILS.CUST_CD_EDA) =                                                     " & vbNewLine _
                                        & "   (SELECT                                                                                          " & vbNewLine _
                                        & "        ISNULL(MIN(         CUST_DETAILS.NRS_BR_CD                                                  " & vbNewLine _
                                        & "                    + ',' + CUST_DETAILS.CUST_CD                                                    " & vbNewLine _
                                        & "                    + ',' + CUST_DETAILS.CUST_CD_EDA), '') AS PK                                    " & vbNewLine _
                                        & "    FROM                                                                                            " & vbNewLine _
                                        & "        $LM_MST$..M_CUST_DETAILS AS CUST_DETAILS                                                    " & vbNewLine _
                                        & "    WHERE                                                                                           " & vbNewLine _
                                        & "        CUST_DETAILS.NRS_BR_CD = ZAI.NRS_BR_CD                                                      " & vbNewLine _
                                        & "    AND CUST_DETAILS.CUST_CD = ZAI.CUST_CD_L + ZAI.CUST_CD_M                                        " & vbNewLine _
                                        & "    AND CUST_DETAILS.CUST_CLASS = '01'                                                              " & vbNewLine _
                                        & "    AND CUST_DETAILS.SUB_KB = '1Z'                                                                  " & vbNewLine _
                                        & "    AND CUST_DETAILS.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                                        & "    GROUP BY                                                                                        " & vbNewLine _
                                        & "          CUST_DETAILS.NRS_BR_CD                                                                    " & vbNewLine _
                                        & "        , CUST_DETAILS.CUST_CD                                                                      " & vbNewLine _
                                        & "    )                                                                                               " & vbNewLine

    ''' <summary>
    ''' GROUP BY（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUP_BY_LMC010 As String = "GROUP BY                                                                                            " & vbNewLine _
                                    & " ZAI.NRS_BR_CD                                                                                      " & vbNewLine _
                                    & ",ZAI.ZAI_REC_NO                                                                                     " & vbNewLine _
                                    & ",ZAI.WH_CD                                                                                          " & vbNewLine _
                                    & ",ZAI.TOU_NO                                                                                         " & vbNewLine _
                                    & ",ZAI.SITU_NO                                                                                        " & vbNewLine _
                                    & ",ZAI.ZONE_CD                                                                                        " & vbNewLine _
                                    & ",ZAI.LOCA                                                                                           " & vbNewLine _
                                    & ",ZAI.LOT_NO                                                                                         " & vbNewLine _
                                    & ",ZAI.CUST_CD_L                                                                                      " & vbNewLine _
                                    & ",ZAI.CUST_CD_M                                                                                      " & vbNewLine _
                                    & ",ZAI.GOODS_CD_NRS                                                                                   " & vbNewLine _
                                    & ",ZAI.GOODS_KANRI_NO                                                                                 " & vbNewLine _
                                    & ",ZAI.INKA_NO_L                                                                                      " & vbNewLine _
                                    & ",ZAI.INKA_NO_M                                                                                      " & vbNewLine _
                                    & ",ZAI.INKA_NO_S                                                                                      " & vbNewLine _
                                    & ",ZAI.ALLOC_PRIORITY                                                                                 " & vbNewLine _
                                    & ",ZAI.RSV_NO                                                                                         " & vbNewLine _
                                    & ",ZAI.SERIAL_NO                                                                                      " & vbNewLine _
                                    & ",ZAI.HOKAN_YN                                                                                       " & vbNewLine _
                                    & ",ZAI.TAX_KB                                                                                         " & vbNewLine _
                                    & ",ZAI.GOODS_COND_KB_1                                                                                " & vbNewLine _
                                    & ",ZAI.GOODS_COND_KB_2                                                                                " & vbNewLine _
                                    & ",ZAI.GOODS_COND_KB_3                                                                                " & vbNewLine _
                                    & ",ZAI.OFB_KB                                                                                         " & vbNewLine _
                                    & ",ZAI.SPD_KB                                                                                         " & vbNewLine _
                                    & ",ZAI.REMARK_OUT                                                                                     " & vbNewLine _
                                    & ",ZAI.PORA_ZAI_NB                                                                                    " & vbNewLine _
                                    & ",ZAI.ALCTD_NB                                                                                       " & vbNewLine _
                                    & ",ZAI.ALLOC_CAN_NB                                                                                   " & vbNewLine _
                                    & ",ZAI.IRIME                                                                                          " & vbNewLine _
                                    & ",ZAI.PORA_ZAI_QT                                                                                    " & vbNewLine _
                                    & ",ZAI.ALCTD_QT                                                                                       " & vbNewLine _
                                    & ",ZAI.ALLOC_CAN_QT                                                                                   " & vbNewLine _
                                    & ",ZAI.INKO_DATE                                                                                      " & vbNewLine _
                                    & ",ZAI.INKO_PLAN_DATE                                                                                 " & vbNewLine _
                                    & ",ZAI.ZERO_FLAG                                                                                      " & vbNewLine _
                                    & ",ZAI.LT_DATE                                                                                        " & vbNewLine _
                                    & ",ZAI.GOODS_CRT_DATE                                                                                 " & vbNewLine _
                                    & ",ZAI.DEST_CD_P                                                                                      " & vbNewLine _
                                    & ",ZAI.REMARK                                                                                         " & vbNewLine _
                                    & ",ZAI.SMPL_FLAG                                                                                      " & vbNewLine _
                                    & ",CUSTCOND.JOTAI_NM                                                                                  " & vbNewLine _
                                    & ",GOODS.CONSUME_PERIOD_DATE                                                                          " & vbNewLine _
                                    & ",INKA_L.INKA_DATE                                                                                   " & vbNewLine _
                                    & ",ZAI.SYS_UPD_DATE                                                                                   " & vbNewLine _
                                    & ",ZAI.SYS_UPD_TIME                                                                                   " & vbNewLine _
                                    & ",INKA_L.INKO_DATE                                                                                   " & vbNewLine _
                                    & ",INKA_L.HOKAN_STR_DATE                                                                              " & vbNewLine _
                                    & ",INKA_L.INKA_STATE_KB                                                                               " & vbNewLine _
                                    & ",GOODS.STD_IRIME_NB                                                                                 " & vbNewLine _
                                    & ",GOODS.STD_WT_KGS                                                                                   " & vbNewLine _
                                    & ",GOODS.TARE_YN                                                                                      " & vbNewLine _
                                    & ",GOODS.PKG_UT                                                                                       " & vbNewLine _
                                    & ",INKA_L.FURI_NO                                                                                     " & vbNewLine _
                                    & ",GOODSDETAILS.SET_NAIYO                                                                             " & vbNewLine _
                                    & ",Z1.VALUE1                                                                                          " & vbNewLine _
                                    & ",ZAI.BYK_KEEP_GOODS_CD                                                                              " & vbNewLine _
                                    & ",KEEP_GOODS.KBN_NM1                                                                                 " & vbNewLine _
                                    & ",CUST_DETAILS.SET_NAIYO                                                                             " & vbNewLine

    ''' <summary>
    ''' ORDER BY（検索時　手動引当）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_BY_LMC010 As String = "ORDER BY                                   " & vbNewLine _
                                         & " ZAI.DEST_CD_P DESC                                      " & vbNewLine _
                                         & ",ZAI.ALLOC_PRIORITY                                      " & vbNewLine _
                                         & ",ZAI.LT_DATE                                             " & vbNewLine _
                                         & ",ZAI.GOODS_CRT_DATE                                      " & vbNewLine _
                                         & ",INKA_DATE2                                              " & vbNewLine _
                                         & ",ZAI.LOT_NO                                              " & vbNewLine _
                                         & ",ZAI.SERIAL_NO                                           " & vbNewLine _
                                         & ",ZAI.ALLOC_CAN_NB                                        " & vbNewLine
    'END YANAI 要望番号945
    'START S.Koba 要望番号1107
    ''' <summary>
    ''' ORDER BY（検索時　棟優先順序制御）
    ''' </summary>
    ''' <remarks>変更履歴
    '''    2013/06/05 要望番号2059対応　引当優先が最優先の場合、Zoneをソート順に含むように変更
    ''' </remarks>
    Private Const SQL_SELECT_ORDER_BY_LMC010_TOU As String = "ORDER BY                                   " & vbNewLine _
                                         & " ZAI.DEST_CD_P DESC                                      " & vbNewLine _
                                         & ",ZAI.ALLOC_PRIORITY                                      " & vbNewLine _
                                         & ",ZAI.LT_DATE                                             " & vbNewLine _
                                         & ",ZAI.GOODS_CRT_DATE                                      " & vbNewLine _
                                         & ",INKA_DATE2                                              " & vbNewLine _
                                         & ",ZAI.LOT_NO                                              " & vbNewLine _
                                         & ",ZAI.SERIAL_NO                                           " & vbNewLine _
                                         & ",ISNULL((SELECT MCD.SET_NAIYO_2 FROM $LM_MST$..M_CUST_DETAILS MCD                                         " & vbNewLine _
                                         & "         WHERE  MCD.NRS_BR_CD = ZAI.NRS_BR_CD AND MCD.CUST_CD = ZAI.CUST_CD_L AND MCD.SUB_KB = '30' AND MCD.SET_NAIYO = ZAI.TOU_NO ),99)" & vbNewLine _
                                         & ",CASE ZAI.ALLOC_PRIORITY WHEN '01' THEN  ZAI.SITU_NO ELSE 'ZZ' END   " & vbNewLine _
                                         & ",ZAI.ALLOC_CAN_NB                                        " & vbNewLine


    ''' <summary>
    ''' 在庫データ データ抽出用（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_LMC020 As String = " SELECT                                                              " & vbNewLine _
                                            & " ZAI.NRS_BR_CD                                       AS NRS_BR_CD           " & vbNewLine _
                                            & ",ZAI.ZAI_REC_NO                                      AS ZAI_REC_NO          " & vbNewLine _
                                            & ",ZAI.WH_CD                                           AS WH_CD     	       " & vbNewLine _
                                            & ",ZAI.TOU_NO                                          AS TOU_NO              " & vbNewLine _
                                            & ",ZAI.SITU_NO                                         AS SITU_NO	           " & vbNewLine _
                                            & ",ZAI.ZONE_CD                                         AS ZONE_CD             " & vbNewLine _
                                            & ",ZAI.LOCA                                            AS LOCA                " & vbNewLine _
                                            & ",ZAI.LOT_NO                                          AS LOT_NO              " & vbNewLine _
                                            & ",ZAI.CUST_CD_L                                       AS CUST_CD_L           " & vbNewLine _
                                            & ",ZAI.CUST_CD_M                                       AS CUST_CD_M           " & vbNewLine _
                                            & ",ZAI.GOODS_CD_NRS                                    AS GOODS_CD_NRS        " & vbNewLine _
                                            & ",ZAI.GOODS_KANRI_NO                                  AS GOODS_KANRI_NO      " & vbNewLine _
                                            & ",ZAI.INKA_NO_L                                       AS INKA_NO_L           " & vbNewLine _
                                            & ",ZAI.INKA_NO_M                                       AS INKA_NO_M           " & vbNewLine _
                                            & ",ZAI.INKA_NO_S                                       AS INKA_NO_S           " & vbNewLine _
                                            & ",ZAI.ALLOC_PRIORITY                                  AS ALLOC_PRIORITY      " & vbNewLine _
                                            & ",ZAI.RSV_NO                                          AS RSV_NO              " & vbNewLine _
                                            & ",ZAI.SERIAL_NO                                       AS SERIAL_NO           " & vbNewLine _
                                            & ",ZAI.HOKAN_YN                                        AS HOKAN_YN            " & vbNewLine _
                                            & ",ZAI.TAX_KB                                          AS TAX_KB              " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_1                                 AS GOODS_COND_KB_1     " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_2                                 AS GOODS_COND_KB_2     " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_3                                 AS GOODS_COND_KB_3     " & vbNewLine _
                                            & ",ZAI.OFB_KB                                          AS OFB_KB              " & vbNewLine _
                                            & ",ZAI.SPD_KB                                          AS SPD_KB              " & vbNewLine _
                                            & ",ZAI.REMARK_OUT                                      AS REMARK_OUT          " & vbNewLine _
                                            & ",ZAI.PORA_ZAI_NB                                     AS PORA_ZAI_NB         " & vbNewLine _
                                            & ",ZAI.ALCTD_NB                                        AS ALCTD_NB            " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_NB                                    AS ALLOC_CAN_NB        " & vbNewLine _
                                            & ",ZAI.IRIME                                           AS IRIME               " & vbNewLine _
                                            & ",ZAI.PORA_ZAI_QT                                     AS PORA_ZAI_QT         " & vbNewLine _
                                            & ",ZAI.ALCTD_QT                                        AS ALCTD_QT            " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT        " & vbNewLine _
                                            & ",ZAI.INKO_DATE                                       AS INKO_DATE           " & vbNewLine _
                                            & ",ZAI.INKO_PLAN_DATE                                  AS INKO_PLAN_DATE      " & vbNewLine _
                                            & ",ZAI.ZERO_FLAG                                       AS ZERO_FLAG           " & vbNewLine _
                                            & ",ZAI.LT_DATE                                         AS LT_DATE             " & vbNewLine _
                                            & ",ZAI.GOODS_CRT_DATE                                  AS GOODS_CRT_DATE      " & vbNewLine _
                                            & ",ZAI.DEST_CD_P                                       AS DEST_CD_P           " & vbNewLine _
                                            & ",ZAI.REMARK                                          AS REMARK              " & vbNewLine _
                                            & ",ZAI.SMPL_FLAG                                       AS SMPL_FLAG           " & vbNewLine _
                                            & ",Z1.KBN_NM1                                          AS GOODS_COND_NM_1     " & vbNewLine _
                                            & ",Z2.KBN_NM1                                          AS GOODS_COND_NM_2     " & vbNewLine _
                                            & ",CUSTCOND.JOTAI_NM                                   AS GOODS_COND_NM_3     " & vbNewLine _
                                            & ",Z3.KBN_NM1                                          AS ALLOC_PRIORITY_NM   " & vbNewLine _
                                            & ",Z4.KBN_NM1                                          AS OFB_KB_NM           " & vbNewLine _
                                            & ",Z5.KBN_NM1                                          AS SPD_KB_NM           " & vbNewLine _
                                            & ",Z6.KBN_NM1                                          AS HIKIATE_ALERT_NM    " & vbNewLine _
                                            & ",Z7.KBN_NM1                                          AS TAX_KB_NM           " & vbNewLine _
                                            & ",Z8.KBN_NM1                                          AS NB_UT_NM            " & vbNewLine _
                                            & ",Z9.KBN_NM1                                          AS IRIME_UT_NM         " & vbNewLine _
                                            & ",GOODS.CONSUME_PERIOD_DATE                           AS CONSUME_PERIOD_DATE " & vbNewLine _
                                            & ",''                                                  AS BUYER_ORD_NO_DTL    " & vbNewLine _
                                            & ",DEST.DEST_NM                                        AS DEST_NM             " & vbNewLine _
                                            & ",GOODS.GOODS_CD_CUST                                 AS GOODS_CD_CUST       " & vbNewLine _
                                            & ",GOODS.GOODS_NM_1                                    AS GOODS_NM_1          " & vbNewLine _
                                            & ",GOODS.OUTKA_ATT                                     AS OUTKA_ATT           " & vbNewLine _
                                            & ",GOODS.SEARCH_KEY_1                                  AS SEARCH_KEY_1        " & vbNewLine _
                                            & ",GOODS.UNSO_ONDO_KB                                  AS UNSO_ONDO_KB        " & vbNewLine _
                                            & ",GOODS.PKG_UT                                        AS PKG_UT              " & vbNewLine _
                                            & ",GOODS.STD_IRIME_NB                                  AS STD_IRIME_NB        " & vbNewLine _
                                            & ",GOODS.STD_WT_KGS                                    AS STD_WT_KGS          " & vbNewLine _
                                            & ",GOODS.TARE_YN                                       AS TARE_YN             " & vbNewLine _
                                            & ",GOODS.HIKIATE_ALERT_YN                              AS HIKIATE_ALERT_YN    " & vbNewLine _
                                            & ",GOODS.PKG_NB                                        AS PKG_NB              " & vbNewLine _
                                            & ",GOODS.STD_IRIME_UT                                  AS STD_IRIME_UT        " & vbNewLine _
                                            & ",GOODS.NB_UT                                         AS NB_UT               " & vbNewLine _
                                            & ",INKA_L.INKA_DATE                                    AS INKA_DATE           " & vbNewLine _
                                            & ",GOODS.CUST_CD_S                                     AS CUST_CD_S           " & vbNewLine _
                                            & ",GOODS.CUST_CD_SS                                    AS CUST_CD_SS          " & vbNewLine _
                                            & ",ZAI.SYS_UPD_DATE                                    AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",ZAI.SYS_UPD_TIME                                    AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",''                                                  AS GOODS_CD_NRS_FROM   " & vbNewLine _
                                            & ",IDO.IDO_DATE                                        AS IDO_DATE            " & vbNewLine _
                                            & ",INKA_L.HOKAN_STR_DATE                               AS HOKAN_STR_DATE      " & vbNewLine _
                                            & ",GOODS.COA_YN                                        AS COA_YN              " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_1                         AS OUTKA_KAKO_SAGYO_KB_1 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_2                         AS OUTKA_KAKO_SAGYO_KB_2 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_3                         AS OUTKA_KAKO_SAGYO_KB_3 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_4                         AS OUTKA_KAKO_SAGYO_KB_4 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_5                         AS OUTKA_KAKO_SAGYO_KB_5 " & vbNewLine _
                                            & ",GOODS.OUTKA_HASU_SAGYO_KB_1                         AS OUTKA_HASU_SAGYO_KB_1 " & vbNewLine _
                                            & ",GOODS.OUTKA_HASU_SAGYO_KB_2                         AS OUTKA_HASU_SAGYO_KB_2 " & vbNewLine _
                                            & ",GOODS.OUTKA_HASU_SAGYO_KB_3                         AS OUTKA_HASU_SAGYO_KB_3 " & vbNewLine _
                                            & ",INKA_L.INKA_STATE_KB                                AS INKA_STATE_KB       " & vbNewLine _
                                            & ",CASE WHEN ZAI.INKO_DATE IS NULL OR ZAI.INKO_DATE = ''                      " & vbNewLine _
                                            & "      THEN '1'                                                              " & vbNewLine _
                                            & "      ELSE '0'                                                              " & vbNewLine _
                                            & " END                                                 AS YOJITU              " & vbNewLine _
                                            & ",GOODS.SIZE_KB                                       AS SIZE_KB             " & vbNewLine _
                                            & ",GOODS.CUST_CD_L                                     AS CUST_CD_L_GOODS     " & vbNewLine _
                                            & ",GOODS.CUST_CD_M                                     AS CUST_CD_M_GOODS     " & vbNewLine _
                                            & ",CASE WHEN INKA_L.INKA_STATE_KB = '50' AND INKA_L.FURI_NO <> ''             " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                    " & vbNewLine _
                                            & "      WHEN INKA_L.INKA_STATE_KB = '50' AND INKA_L.FURI_NO = ''              " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                    " & vbNewLine _
                                            & "      WHEN INKA_L.INKA_STATE_KB = '90' AND INKA_L.FURI_NO <> ''             " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                    " & vbNewLine _
                                            & "      WHEN INKA_L.INKA_STATE_KB = '90' AND INKA_L.FURI_NO = ''              " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                    " & vbNewLine _
                                            & "      ELSE INKA_L.INKA_DATE                                                 " & vbNewLine _
                                            & " END                                                 AS INKA_DATE2          " & vbNewLine _
                                            & ",GOODSDETAILS.SET_NAIYO                              AS INKA_DATE_KANRI_KB  " & vbNewLine _
                                            & ",Z5.VALUE1                                           AS SPD_KB_FLG          " & vbNewLine _
                                            & ",GOODS.SHOBO_CD                                      AS SHOBO_CD            " & vbNewLine _
                                            & ",Z10.KBN_NM1 + ' ' + SBO.HINMEI                      AS SHOBO_NM            " & vbNewLine _
                                            & ",ZAI.BYK_KEEP_GOODS_CD                               AS BYK_KEEP_GOODS_CD   " & vbNewLine _
                                            & ",KEEP_GOODS.KBN_NM1                                  AS KEEP_GOODS_NM       " & vbNewLine _
                                            & ",ISNULL(CUST_DETAILS.SET_NAIYO, '0')                 AS IS_BYK_KEEP_GOODS_CD" & vbNewLine

#If True Then ' フィルメニッヒ セミEDI対応  20161003 added inoue
    ''' <summary>
    ''' フィルメ用追加検索項目
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_LMC020_ADD_FIR As String _
        = ", CASE WHEN ZAI.GOODS_COND_KB_1 <> ''  " & vbNewLine _
        & "         OR ZAI.GOODS_COND_KB_2 <> ''  " & vbNewLine _
        & "       THEN 1                          " & vbNewLine _
        & "       ELSE 0                          " & vbNewLine _
        & "  END  AS EXISTS_GOODS_COND            " & vbNewLine
#End If


    ''' <summary>
    ''' FROM（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_LMC020 As String = "FROM                                            " & vbNewLine _
                                         & "$LM_TRN$..D_ZAI_TRS  ZAI                                  " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_CUSTCOND    CUSTCOND                " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = CUSTCOND.NRS_BR_CD                 " & vbNewLine _
                                         & "AND    ZAI.CUST_CD_L = CUSTCOND.CUST_CD_L                 " & vbNewLine _
                                         & "AND    ZAI.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD            " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_1 = Z1.KBN_CD                    " & vbNewLine _
                                         & "AND    Z1.KBN_GROUP_CD = 'S005'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z2                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_2 = Z2.KBN_CD                    " & vbNewLine _
                                         & "AND    Z2.KBN_GROUP_CD = 'S006'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z3                           " & vbNewLine _
                                         & "ON     ZAI.ALLOC_PRIORITY = Z3.KBN_CD                     " & vbNewLine _
                                         & "AND    Z3.KBN_GROUP_CD = 'W001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z4                           " & vbNewLine _
                                         & "ON     ZAI.OFB_KB = Z4.KBN_CD                             " & vbNewLine _
                                         & "AND    Z4.KBN_GROUP_CD = 'B002'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z5                           " & vbNewLine _
                                         & "ON     ZAI.SPD_KB = Z5.KBN_CD                             " & vbNewLine _
                                         & "AND    Z5.KBN_GROUP_CD = 'H003'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS  GOODS                        " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = GOODS.NRS_BR_CD                    " & vbNewLine _
                                         & "AND    ZAI.GOODS_CD_NRS = GOODS.GOODS_CD_NRS              " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z6                           " & vbNewLine _
                                         & "ON     GOODS.HIKIATE_ALERT_YN = Z6.KBN_CD                 " & vbNewLine _
                                         & "AND    Z6.KBN_GROUP_CD = 'U009'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN AS Z7                           " & vbNewLine _
                                         & "ON     ZAI.TAX_KB  = Z7.KBN_CD                            " & vbNewLine _
                                         & "AND    Z7.KBN_GROUP_CD = 'Z001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z8                           " & vbNewLine _
                                         & "ON     GOODS.NB_UT = Z8.KBN_CD                            " & vbNewLine _
                                         & "AND    Z8.KBN_GROUP_CD = 'K002'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z9                           " & vbNewLine _
                                         & "ON     GOODS.STD_IRIME_UT  = Z9.KBN_CD                    " & vbNewLine _
                                         & "AND    Z9.KBN_GROUP_CD = 'I001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_DEST    DEST                        " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = DEST.NRS_BR_CD                     " & vbNewLine _
                                         & "AND    ZAI.CUST_CD_L = DEST.CUST_CD_L                     " & vbNewLine _
                                         & "AND    ZAI.DEST_CD_P = DEST.DEST_CD                       " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..B_INKA_L AS INKA_L                    " & vbNewLine _
                                         & "ON  INKA_L.NRS_BR_CD = ZAI.NRS_BR_CD                      " & vbNewLine _
                                         & "AND INKA_L.INKA_NO_L = ZAI.INKA_NO_L                      " & vbNewLine _
                                         & "AND INKA_L.SYS_DEL_FLG = 0                                " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..D_IDO_TRS IDO                         " & vbNewLine _
                                         & "ON  IDO.NRS_BR_CD = ZAI.NRS_BR_CD                         " & vbNewLine _
                                         & "AND IDO.N_ZAI_REC_NO = ZAI.ZAI_REC_NO                     " & vbNewLine _
                                         & "AND IDO.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                         & "AND IDO.REC_NO <> ''                                      " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GOODSDETAILS          " & vbNewLine _
                                         & "ON  GOODSDETAILS.NRS_BR_CD = ZAI.NRS_BR_CD                " & vbNewLine _
                                         & "AND GOODSDETAILS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS          " & vbNewLine _
                                         & "AND GOODSDETAILS.SUB_KB = '09'                            " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN AS DO_KENPIN_CHK                " & vbNewLine _
                                         & "  ON DO_KENPIN_CHK.KBN_GROUP_CD = 'N027'                  " & vbNewLine _
                                         & " AND DO_KENPIN_CHK.SYS_DEL_FLG  = '0'                     " & vbNewLine _
                                         & " AND DO_KENPIN_CHK.KBN_NM1      = ZAI.NRS_BR_CD           " & vbNewLine _
                                         & " AND DO_KENPIN_CHK.KBN_NM2      = ZAI.WH_CD               " & vbNewLine _
                                         & " AND DO_KENPIN_CHK.KBN_NM3      = ZAI.TOU_NO              " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_SHOBO SBO                           " & vbNewLine _
                                         & "ON  SBO.SHOBO_CD    = GOODS.SHOBO_CD                      " & vbNewLine _
                                         & "AND SBO.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN Z10                             " & vbNewLine _
                                         & "ON  Z10.KBN_GROUP_CD = 'S004'                             " & vbNewLine _
                                         & "AND Z10.KBN_CD       = SBO.RUI                            " & vbNewLine _
                                         & "AND Z10.SYS_DEL_FLG  = '0'                                " & vbNewLine _
                                         & "LEFT JOIN                                                 " & vbNewLine _
                                         & "    $LM_MST$..Z_KBN AS KEEP_GOODS                         " & vbNewLine _
                                         & "        ON  KEEP_GOODS.KBN_CD = ZAI.BYK_KEEP_GOODS_CD     " & vbNewLine _
                                         & "        AND KEEP_GOODS.KBN_GROUP_CD = 'B039'              " & vbNewLine _
                                         & "LEFT JOIN                                                        " & vbNewLine _
                                         & "   (SELECT                                                       " & vbNewLine _
                                         & "          NRS_BR_CD                                              " & vbNewLine _
                                         & "        , CUST_CD                                                " & vbNewLine _
                                         & "        , CUST_CD_EDA                                            " & vbNewLine _
                                         & "        , SET_NAIYO                                              " & vbNewLine _
                                         & "    FROM                                                         " & vbNewLine _
                                         & "        $LM_MST$..M_CUST_DETAILS                                 " & vbNewLine _
                                         & "    ) AS CUST_DETAILS                                            " & vbNewLine _
                                         & "        ON (        CUST_DETAILS.NRS_BR_CD                       " & vbNewLine _
                                         & "            + ',' + CUST_DETAILS.CUST_CD                         " & vbNewLine _
                                         & "            + ',' + CUST_DETAILS.CUST_CD_EDA) =                  " & vbNewLine _
                                         & "   (SELECT                                                       " & vbNewLine _
                                         & "        ISNULL(MIN(         CUST_DETAILS.NRS_BR_CD               " & vbNewLine _
                                         & "                    + ',' + CUST_DETAILS.CUST_CD                 " & vbNewLine _
                                         & "                    + ',' + CUST_DETAILS.CUST_CD_EDA), '') AS PK " & vbNewLine _
                                         & "    FROM                                                         " & vbNewLine _
                                         & "        $LM_MST$..M_CUST_DETAILS AS CUST_DETAILS                 " & vbNewLine _
                                         & "    WHERE                                                        " & vbNewLine _
                                         & "        CUST_DETAILS.NRS_BR_CD = ZAI.NRS_BR_CD                   " & vbNewLine _
                                         & "    AND CUST_DETAILS.CUST_CD = ZAI.CUST_CD_L + ZAI.CUST_CD_M     " & vbNewLine _
                                         & "    AND CUST_DETAILS.CUST_CLASS = '01'                           " & vbNewLine _
                                         & "    AND CUST_DETAILS.SUB_KB = '1Z'                               " & vbNewLine _
                                         & "    AND CUST_DETAILS.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "    GROUP BY                                                     " & vbNewLine _
                                         & "          CUST_DETAILS.NRS_BR_CD                                 " & vbNewLine _
                                         & "        , CUST_DETAILS.CUST_CD                                   " & vbNewLine _
                                         & "    )                                                            " & vbNewLine

    Private Const SQL_SELECT_GROUP_BY_LMC020 As String = "GROUP BY                                          " & vbNewLine _
                                     & " ZAI.NRS_BR_CD                                                  " & vbNewLine _
                                     & ",ZAI.ZAI_REC_NO                                                 " & vbNewLine _
                                     & ",ZAI.WH_CD                                                      " & vbNewLine _
                                     & ",ZAI.TOU_NO                                                     " & vbNewLine _
                                     & ",ZAI.SITU_NO                                                    " & vbNewLine _
                                     & ",ZAI.ZONE_CD                                                    " & vbNewLine _
                                     & ",ZAI.LOCA                                                       " & vbNewLine _
                                     & ",ZAI.LOT_NO                                                     " & vbNewLine _
                                     & ",ZAI.CUST_CD_L                                                  " & vbNewLine _
                                     & ",ZAI.CUST_CD_M                                                  " & vbNewLine _
                                     & ",ZAI.GOODS_CD_NRS                                               " & vbNewLine _
                                     & ",ZAI.GOODS_KANRI_NO                                             " & vbNewLine _
                                     & ",ZAI.INKA_NO_L                                                  " & vbNewLine _
                                     & ",ZAI.INKA_NO_M                                                  " & vbNewLine _
                                     & ",ZAI.INKA_NO_S                                                  " & vbNewLine _
                                     & ",ZAI.ALLOC_PRIORITY                                             " & vbNewLine _
                                     & ",ZAI.RSV_NO                                                     " & vbNewLine _
                                     & ",ZAI.SERIAL_NO                                                  " & vbNewLine _
                                     & ",ZAI.HOKAN_YN                                                   " & vbNewLine _
                                     & ",ZAI.TAX_KB                                                     " & vbNewLine _
                                     & ",ZAI.GOODS_COND_KB_1                                            " & vbNewLine _
                                     & ",ZAI.GOODS_COND_KB_2                                            " & vbNewLine _
                                     & ",ZAI.GOODS_COND_KB_3                                            " & vbNewLine _
                                     & ",ZAI.OFB_KB                                                     " & vbNewLine _
                                     & ",ZAI.SPD_KB                                                     " & vbNewLine _
                                     & ",ZAI.REMARK_OUT                                                 " & vbNewLine _
                                     & ",ZAI.PORA_ZAI_NB                                                " & vbNewLine _
                                     & ",ZAI.ALCTD_NB                                                   " & vbNewLine _
                                     & ",ZAI.ALLOC_CAN_NB                                               " & vbNewLine _
                                     & ",ZAI.IRIME                                                      " & vbNewLine _
                                     & ",ZAI.PORA_ZAI_QT                                                " & vbNewLine _
                                     & ",ZAI.ALCTD_QT                                                   " & vbNewLine _
                                     & ",ZAI.ALLOC_CAN_QT                                               " & vbNewLine _
                                     & ",ZAI.INKO_DATE                                                  " & vbNewLine _
                                     & ",ZAI.INKO_PLAN_DATE                                             " & vbNewLine _
                                     & ",ZAI.ZERO_FLAG                                                  " & vbNewLine _
                                     & ",ZAI.LT_DATE                                                    " & vbNewLine _
                                     & ",ZAI.GOODS_CRT_DATE                                             " & vbNewLine _
                                     & ",ZAI.DEST_CD_P                                                  " & vbNewLine _
                                     & ",ZAI.REMARK                                                     " & vbNewLine _
                                     & ",ZAI.SMPL_FLAG                                                  " & vbNewLine _
                                     & ",Z1.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z2.KBN_NM1                                                     " & vbNewLine _
                                     & ",CUSTCOND.JOTAI_NM                                              " & vbNewLine _
                                     & ",Z3.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z4.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z5.KBN_NM1                                                     " & vbNewLine _
                                     & ",GOODS.CONSUME_PERIOD_DATE                                      " & vbNewLine _
                                     & ",Z6.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z7.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z8.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z9.KBN_NM1                                                     " & vbNewLine _
                                     & ",DEST.DEST_NM                                                   " & vbNewLine _
                                     & ",GOODS.GOODS_CD_CUST                                            " & vbNewLine _
                                     & ",GOODS.GOODS_NM_1                                               " & vbNewLine _
                                     & ",GOODS.OUTKA_ATT                                                " & vbNewLine _
                                     & ",GOODS.SEARCH_KEY_1                                             " & vbNewLine _
                                     & ",GOODS.UNSO_ONDO_KB                                             " & vbNewLine _
                                     & ",GOODS.PKG_UT                                                   " & vbNewLine _
                                     & ",GOODS.STD_IRIME_NB                                             " & vbNewLine _
                                     & ",GOODS.STD_WT_KGS                                               " & vbNewLine _
                                     & ",GOODS.TARE_YN                                                  " & vbNewLine _
                                     & ",GOODS.HIKIATE_ALERT_YN                                         " & vbNewLine _
                                     & ",GOODS.PKG_NB                                                   " & vbNewLine _
                                     & ",GOODS.STD_IRIME_UT                                             " & vbNewLine _
                                     & ",GOODS.NB_UT                                                    " & vbNewLine _
                                     & ",INKA_L.INKA_DATE                                               " & vbNewLine _
                                     & ",GOODS.CUST_CD_S                                                " & vbNewLine _
                                     & ",GOODS.CUST_CD_SS                                               " & vbNewLine _
                                     & ",ZAI.SYS_UPD_DATE                                               " & vbNewLine _
                                     & ",ZAI.SYS_UPD_TIME                                               " & vbNewLine _
                                     & ",IDO.IDO_DATE                                                   " & vbNewLine _
                                     & ",INKA_L.INKO_DATE                                               " & vbNewLine _
                                     & ",INKA_L.HOKAN_STR_DATE                                          " & vbNewLine _
                                     & ",GOODS.COA_YN                                                   " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_1                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_2                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_3                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_4                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_5                                    " & vbNewLine _
                                     & ",INKA_L.INKA_STATE_KB                                           " & vbNewLine _
                                     & ",GOODS.SIZE_KB                                                  " & vbNewLine _
                                     & ",GOODS.CUST_CD_L                                                " & vbNewLine _
                                     & ",GOODS.CUST_CD_M                                                " & vbNewLine _
                                     & ",INKA_L.FURI_NO                                                 " & vbNewLine _
                                     & ",GOODSDETAILS.SET_NAIYO                                         " & vbNewLine _
                                     & ",Z5.VALUE1                                                      " & vbNewLine _
                                     & ",GOODS.SHOBO_CD                                                 " & vbNewLine _
                                     & ",Z10.KBN_NM1 + ' ' + SBO.HINMEI                                 " & vbNewLine _
                                     & ",GOODS.OUTKA_HASU_SAGYO_KB_1                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_HASU_SAGYO_KB_2                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_HASU_SAGYO_KB_3                                    " & vbNewLine _
                                     & ",ZAI.BYK_KEEP_GOODS_CD                                          " & vbNewLine _
                                     & ",KEEP_GOODS.KBN_NM1                                             " & vbNewLine _
                                     & ",CUST_DETAILS.SET_NAIYO                                         " & vbNewLine

    ''' <summary>
    ''' 在庫データ データ抽出用（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_LMD010 As String = " SELECT                                                              " & vbNewLine _
                                            & " ZAI.NRS_BR_CD                                       AS NRS_BR_CD           " & vbNewLine _
                                            & ",ZAI.ZAI_REC_NO                                      AS ZAI_REC_NO          " & vbNewLine _
                                            & ",ZAI.WH_CD                                           AS WH_CD     	       " & vbNewLine _
                                            & ",ZAI.TOU_NO                                          AS TOU_NO              " & vbNewLine _
                                            & ",ZAI.SITU_NO                                         AS SITU_NO	           " & vbNewLine _
                                            & ",ZAI.ZONE_CD                                         AS ZONE_CD             " & vbNewLine _
                                            & ",ZAI.LOCA                                            AS LOCA                " & vbNewLine _
                                            & ",ZAI.LOT_NO                                          AS LOT_NO              " & vbNewLine _
                                            & ",ZAI.CUST_CD_L                                       AS CUST_CD_L           " & vbNewLine _
                                            & ",ZAI.CUST_CD_M                                       AS CUST_CD_M           " & vbNewLine _
                                            & ",ZAI.GOODS_CD_NRS                                    AS GOODS_CD_NRS        " & vbNewLine _
                                            & ",ZAI.GOODS_KANRI_NO                                  AS GOODS_KANRI_NO      " & vbNewLine _
                                            & ",ZAI.INKA_NO_L                                       AS INKA_NO_L           " & vbNewLine _
                                            & ",ZAI.INKA_NO_M                                       AS INKA_NO_M           " & vbNewLine _
                                            & ",ZAI.INKA_NO_S                                       AS INKA_NO_S           " & vbNewLine _
                                            & ",ZAI.ALLOC_PRIORITY                                  AS ALLOC_PRIORITY      " & vbNewLine _
                                            & ",ZAI.RSV_NO                                          AS RSV_NO              " & vbNewLine _
                                            & ",ZAI.SERIAL_NO                                       AS SERIAL_NO           " & vbNewLine _
                                            & ",ZAI.HOKAN_YN                                        AS HOKAN_YN            " & vbNewLine _
                                            & ",ZAI.TAX_KB                                          AS TAX_KB              " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_1                                 AS GOODS_COND_KB_1     " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_2                                 AS GOODS_COND_KB_2     " & vbNewLine _
                                            & ",ZAI.GOODS_COND_KB_3                                 AS GOODS_COND_KB_3     " & vbNewLine _
                                            & ",ZAI.OFB_KB                                          AS OFB_KB              " & vbNewLine _
                                            & ",ZAI.SPD_KB                                          AS SPD_KB              " & vbNewLine _
                                            & ",ZAI.REMARK_OUT                                      AS REMARK_OUT          " & vbNewLine _
                                            & ",ZAI.PORA_ZAI_NB                                     AS PORA_ZAI_NB         " & vbNewLine _
                                            & ",ZAI.ALCTD_NB                                        AS ALCTD_NB            " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_NB                                    AS ALLOC_CAN_NB        " & vbNewLine _
                                            & ",ZAI.IRIME                                           AS IRIME               " & vbNewLine _
                                            & ",ZAI.PORA_ZAI_QT                                     AS PORA_ZAI_QT         " & vbNewLine _
                                            & ",ZAI.ALCTD_QT                                        AS ALCTD_QT            " & vbNewLine _
                                            & ",ZAI.ALLOC_CAN_QT                                    AS ALLOC_CAN_QT        " & vbNewLine _
                                            & ",ZAI.INKO_DATE                                       AS INKO_DATE           " & vbNewLine _
                                            & ",ZAI.INKO_PLAN_DATE                                  AS INKO_PLAN_DATE      " & vbNewLine _
                                            & ",ZAI.ZERO_FLAG                                       AS ZERO_FLAG           " & vbNewLine _
                                            & ",ZAI.LT_DATE                                         AS LT_DATE             " & vbNewLine _
                                            & ",ZAI.GOODS_CRT_DATE                                  AS GOODS_CRT_DATE      " & vbNewLine _
                                            & ",ZAI.DEST_CD_P                                       AS DEST_CD_P           " & vbNewLine _
                                            & ",ZAI.REMARK                                          AS REMARK              " & vbNewLine _
                                            & ",ZAI.SMPL_FLAG                                       AS SMPL_FLAG           " & vbNewLine _
                                            & ",Z1.KBN_NM1                                          AS GOODS_COND_NM_1     " & vbNewLine _
                                            & ",Z2.KBN_NM1                                          AS GOODS_COND_NM_2     " & vbNewLine _
                                            & ",CUSTCOND.JOTAI_NM                                   AS GOODS_COND_NM_3     " & vbNewLine _
                                            & ",Z3.KBN_NM1                                          AS ALLOC_PRIORITY_NM   " & vbNewLine _
                                            & ",Z4.KBN_NM1                                          AS OFB_KB_NM           " & vbNewLine _
                                            & ",Z5.KBN_NM1                                          AS SPD_KB_NM           " & vbNewLine _
                                            & ",Z6.KBN_NM1                                          AS HIKIATE_ALERT_NM    " & vbNewLine _
                                            & ",Z7.KBN_NM1                                          AS TAX_KB_NM           " & vbNewLine _
                                            & ",Z8.KBN_NM1                                          AS NB_UT_NM            " & vbNewLine _
                                            & ",''                                                  AS IRIME_UT_NM         " & vbNewLine _
                                            & ",GOODS.CONSUME_PERIOD_DATE                           AS CONSUME_PERIOD_DATE " & vbNewLine _
                                            & ",''                                                  AS BUYER_ORD_NO_DTL    " & vbNewLine _
                                            & ",DEST.DEST_NM                                        AS DEST_NM             " & vbNewLine _
                                            & ",GOODS.GOODS_CD_CUST                                 AS GOODS_CD_CUST       " & vbNewLine _
                                            & ",GOODS.GOODS_NM_1                                    AS GOODS_NM_1          " & vbNewLine _
                                            & ",GOODS.OUTKA_ATT                                     AS OUTKA_ATT           " & vbNewLine _
                                            & ",GOODS.SEARCH_KEY_1                                  AS SEARCH_KEY_1        " & vbNewLine _
                                            & ",GOODS.UNSO_ONDO_KB                                  AS UNSO_ONDO_KB        " & vbNewLine _
                                            & ",GOODS.PKG_UT                                        AS PKG_UT              " & vbNewLine _
                                            & ",GOODS.STD_IRIME_NB                                  AS STD_IRIME_NB        " & vbNewLine _
                                            & ",GOODS.STD_WT_KGS                                    AS STD_WT_KGS          " & vbNewLine _
                                            & ",GOODS.TARE_YN                                       AS TARE_YN             " & vbNewLine _
                                            & ",GOODS.HIKIATE_ALERT_YN                              AS HIKIATE_ALERT_YN    " & vbNewLine _
                                            & ",GOODS.PKG_NB                                        AS PKG_NB              " & vbNewLine _
                                            & ",GOODS.STD_IRIME_UT                                  AS STD_IRIME_UT        " & vbNewLine _
                                            & ",GOODS.NB_UT                                         AS NB_UT               " & vbNewLine _
                                            & ",INKA_L.INKA_DATE                                    AS INKA_DATE           " & vbNewLine _
                                            & ",GOODS.CUST_CD_S                                     AS CUST_CD_S           " & vbNewLine _
                                            & ",GOODS.CUST_CD_SS                                    AS CUST_CD_SS          " & vbNewLine _
                                            & ",ZAI.SYS_UPD_DATE                                    AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",ZAI.SYS_UPD_TIME                                    AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",''                                                  AS GOODS_CD_NRS_FROM   " & vbNewLine _
                                            & ",IDO.IDO_DATE                                        AS IDO_DATE            " & vbNewLine _
                                            & ",INKA_L.HOKAN_STR_DATE                               AS HOKAN_STR_DATE      " & vbNewLine _
                                            & ",GOODS.COA_YN                                        AS COA_YN              " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_1                         AS OUTKA_KAKO_SAGYO_KB_1 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_2                         AS OUTKA_KAKO_SAGYO_KB_2 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_3                         AS OUTKA_KAKO_SAGYO_KB_3 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_4                         AS OUTKA_KAKO_SAGYO_KB_4 " & vbNewLine _
                                            & ",GOODS.OUTKA_KAKO_SAGYO_KB_5                         AS OUTKA_KAKO_SAGYO_KB_5 " & vbNewLine _
                                            & ",INKA_L.INKA_STATE_KB                                AS INKA_STATE_KB       " & vbNewLine _
                                            & ",CASE WHEN ZAI.INKO_DATE IS NULL OR ZAI.INKO_DATE = ''                      " & vbNewLine _
                                            & "      THEN '1'                                                              " & vbNewLine _
                                            & "      ELSE '0'                                                              " & vbNewLine _
                                            & " END                                                 AS YOJITU              " & vbNewLine _
                                            & ",GOODS.SIZE_KB                                       AS SIZE_KB             " & vbNewLine _
                                            & ",GOODS.CUST_CD_L                                     AS CUST_CD_L_GOODS     " & vbNewLine _
                                            & ",GOODS.CUST_CD_M                                     AS CUST_CD_M_GOODS     " & vbNewLine _
                                            & ",CASE WHEN INKA_L.INKA_STATE_KB = '50' AND INKA_L.FURI_NO <> ''             " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                    " & vbNewLine _
                                            & "      WHEN INKA_L.INKA_STATE_KB = '50' AND INKA_L.FURI_NO = ''              " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                    " & vbNewLine _
                                            & "      WHEN INKA_L.INKA_STATE_KB = '90' AND INKA_L.FURI_NO <> ''             " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                    " & vbNewLine _
                                            & "      WHEN INKA_L.INKA_STATE_KB = '90' AND INKA_L.FURI_NO = ''              " & vbNewLine _
                                            & "      THEN ZAI.INKO_DATE                                                    " & vbNewLine _
                                            & "      ELSE INKA_L.INKA_DATE                                                 " & vbNewLine _
                                            & " END                                                 AS INKA_DATE2          " & vbNewLine _
                                            & ",GOODSDETAILS.SET_NAIYO                              AS INKA_DATE_KANRI_KB  " & vbNewLine _
                                            & ",Z5.VALUE1                                           AS SPD_KB_FLG          " & vbNewLine _
                                            & ",''                                                  AS SHOBO_CD            " & vbNewLine _
                                            & ",''                                                  AS SHOBO_NM            " & vbNewLine _
                                            & ",ZAI.BYK_KEEP_GOODS_CD                               AS BYK_KEEP_GOODS_CD   " & vbNewLine _
                                            & ",KEEP_GOODS.KBN_NM1                                  AS KEEP_GOODS_NM       " & vbNewLine _
                                            & ",ISNULL(CUST_DETAILS.SET_NAIYO, '0')                 AS IS_BYK_KEEP_GOODS_CD" & vbNewLine

    ''' <summary>
    ''' FROM（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_LMD010 As String = "FROM                                            " & vbNewLine _
                                         & "$LM_TRN$..D_ZAI_TRS  ZAI                                  " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_CUSTCOND    CUSTCOND                " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = CUSTCOND.NRS_BR_CD                 " & vbNewLine _
                                         & "AND    ZAI.CUST_CD_L = CUSTCOND.CUST_CD_L                 " & vbNewLine _
                                         & "AND    ZAI.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD            " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_1 = Z1.KBN_CD                    " & vbNewLine _
                                         & "AND    Z1.KBN_GROUP_CD = 'S005'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z2                           " & vbNewLine _
                                         & "ON     ZAI.GOODS_COND_KB_2 = Z2.KBN_CD                    " & vbNewLine _
                                         & "AND    Z2.KBN_GROUP_CD = 'S006'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z3                           " & vbNewLine _
                                         & "ON     ZAI.ALLOC_PRIORITY = Z3.KBN_CD                     " & vbNewLine _
                                         & "AND    Z3.KBN_GROUP_CD = 'W001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z4                           " & vbNewLine _
                                         & "ON     ZAI.OFB_KB = Z4.KBN_CD                             " & vbNewLine _
                                         & "AND    Z4.KBN_GROUP_CD = 'B002'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z5                           " & vbNewLine _
                                         & "ON     ZAI.SPD_KB = Z5.KBN_CD                             " & vbNewLine _
                                         & "AND    Z5.KBN_GROUP_CD = 'H003'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS  GOODS                        " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = GOODS.NRS_BR_CD                    " & vbNewLine _
                                         & "AND    ZAI.GOODS_CD_NRS = GOODS.GOODS_CD_NRS              " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z6                           " & vbNewLine _
                                         & "ON     GOODS.HIKIATE_ALERT_YN = Z6.KBN_CD                 " & vbNewLine _
                                         & "AND    Z6.KBN_GROUP_CD = 'U009'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN AS Z7                           " & vbNewLine _
                                         & "ON     ZAI.TAX_KB  = Z7.KBN_CD                            " & vbNewLine _
                                         & "AND    Z7.KBN_GROUP_CD = 'Z001'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN    Z8                           " & vbNewLine _
                                         & "ON     GOODS.NB_UT = Z8.KBN_CD                            " & vbNewLine _
                                         & "AND    Z8.KBN_GROUP_CD = 'K002'                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_DEST    DEST                        " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = DEST.NRS_BR_CD                     " & vbNewLine _
                                         & "AND    ZAI.CUST_CD_L = DEST.CUST_CD_L                     " & vbNewLine _
                                         & "AND    ZAI.DEST_CD_P = DEST.DEST_CD                       " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..B_INKA_L AS INKA_L                    " & vbNewLine _
                                         & "ON  INKA_L.NRS_BR_CD = ZAI.NRS_BR_CD                      " & vbNewLine _
                                         & "AND INKA_L.INKA_NO_L = ZAI.INKA_NO_L                      " & vbNewLine _
                                         & "AND INKA_L.SYS_DEL_FLG = 0                                " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..D_IDO_TRS IDO                         " & vbNewLine _
                                         & "ON  IDO.NRS_BR_CD = ZAI.NRS_BR_CD                         " & vbNewLine _
                                         & "AND IDO.N_ZAI_REC_NO = ZAI.ZAI_REC_NO                     " & vbNewLine _
                                         & "AND IDO.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                         & "AND IDO.REC_NO <> ''                                      " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS GOODSDETAILS          " & vbNewLine _
                                         & "ON  GOODSDETAILS.NRS_BR_CD = ZAI.NRS_BR_CD                " & vbNewLine _
                                         & "AND GOODSDETAILS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS          " & vbNewLine _
                                         & "AND GOODSDETAILS.SUB_KB = '09'                            " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN AS DO_KENPIN_CHK                " & vbNewLine _
                                         & "  ON DO_KENPIN_CHK.KBN_GROUP_CD = 'N027'                  " & vbNewLine _
                                         & " AND DO_KENPIN_CHK.SYS_DEL_FLG  = '0'                     " & vbNewLine _
                                         & " AND DO_KENPIN_CHK.KBN_NM1      = ZAI.NRS_BR_CD           " & vbNewLine _
                                         & " AND DO_KENPIN_CHK.KBN_NM2      = ZAI.WH_CD               " & vbNewLine _
                                         & " AND DO_KENPIN_CHK.KBN_NM3      = ZAI.TOU_NO              " & vbNewLine _
                                         & "LEFT JOIN                                                 " & vbNewLine _
                                         & "    $LM_MST$..Z_KBN AS KEEP_GOODS                         " & vbNewLine _
                                         & "        ON  KEEP_GOODS.KBN_CD = ZAI.BYK_KEEP_GOODS_CD     " & vbNewLine _
                                         & "        AND KEEP_GOODS.KBN_GROUP_CD = 'B039'              " & vbNewLine _
                                         & "LEFT JOIN                                                        " & vbNewLine _
                                         & "   (SELECT                                                       " & vbNewLine _
                                         & "          NRS_BR_CD                                              " & vbNewLine _
                                         & "        , CUST_CD                                                " & vbNewLine _
                                         & "        , CUST_CD_EDA                                            " & vbNewLine _
                                         & "        , SET_NAIYO                                              " & vbNewLine _
                                         & "    FROM                                                         " & vbNewLine _
                                         & "        $LM_MST$..M_CUST_DETAILS                                 " & vbNewLine _
                                         & "    ) AS CUST_DETAILS                                            " & vbNewLine _
                                         & "        ON (        CUST_DETAILS.NRS_BR_CD                       " & vbNewLine _
                                         & "            + ',' + CUST_DETAILS.CUST_CD                         " & vbNewLine _
                                         & "            + ',' + CUST_DETAILS.CUST_CD_EDA) =                  " & vbNewLine _
                                         & "   (SELECT                                                       " & vbNewLine _
                                         & "        ISNULL(MIN(         CUST_DETAILS.NRS_BR_CD               " & vbNewLine _
                                         & "                    + ',' + CUST_DETAILS.CUST_CD                 " & vbNewLine _
                                         & "                    + ',' + CUST_DETAILS.CUST_CD_EDA), '') AS PK " & vbNewLine _
                                         & "    FROM                                                         " & vbNewLine _
                                         & "        $LM_MST$..M_CUST_DETAILS AS CUST_DETAILS                 " & vbNewLine _
                                         & "    WHERE                                                        " & vbNewLine _
                                         & "        CUST_DETAILS.NRS_BR_CD = ZAI.NRS_BR_CD                   " & vbNewLine _
                                         & "    AND CUST_DETAILS.CUST_CD = ZAI.CUST_CD_L + ZAI.CUST_CD_M     " & vbNewLine _
                                         & "    AND CUST_DETAILS.CUST_CLASS = '01'                           " & vbNewLine _
                                         & "    AND CUST_DETAILS.SUB_KB = '1Z'                               " & vbNewLine _
                                         & "    AND CUST_DETAILS.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "    GROUP BY                                                     " & vbNewLine _
                                         & "          CUST_DETAILS.NRS_BR_CD                                 " & vbNewLine _
                                         & "        , CUST_DETAILS.CUST_CD                                   " & vbNewLine _
                                         & "    )                                                            " & vbNewLine
    'END YANAI 要望番号780
    ''' <summary>
    ''' FROM（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_LMD010_CNT As String = "FROM                                            " & vbNewLine _
                                         & "$LM_TRN$..D_ZAI_TRS  ZAI                                  " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_CUSTCOND    CUSTCOND                " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = CUSTCOND.NRS_BR_CD                 " & vbNewLine _
                                         & "AND    ZAI.CUST_CD_L = CUSTCOND.CUST_CD_L                 " & vbNewLine _
                                         & "AND    ZAI.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD            " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS  GOODS                        " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = GOODS.NRS_BR_CD                    " & vbNewLine _
                                         & "AND    ZAI.GOODS_CD_NRS = GOODS.GOODS_CD_NRS              " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_DEST    DEST                        " & vbNewLine _
                                         & "ON     ZAI.NRS_BR_CD = DEST.NRS_BR_CD                     " & vbNewLine _
                                         & "AND    ZAI.CUST_CD_L = DEST.CUST_CD_L                     " & vbNewLine _
                                         & "AND    ZAI.DEST_CD_P = DEST.DEST_CD                       " & vbNewLine _
                                         & "LEFT JOIN $LM_TRN$..B_INKA_L AS INKA_L                    " & vbNewLine _
                                         & "  ON INKA_L.NRS_BR_CD = ZAI.NRS_BR_CD                     " & vbNewLine _
                                         & " AND INKA_L.INKA_NO_L = ZAI.INKA_NO_L                     " & vbNewLine _
                                         & " AND INKA_L.SYS_DEL_FLG = 0                               " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..Z_KBN AS DO_KENPIN_CHK                " & vbNewLine _
                                         & "  ON DO_KENPIN_CHK.KBN_GROUP_CD = 'N027'                  " & vbNewLine _
                                         & " AND DO_KENPIN_CHK.SYS_DEL_FLG  = '0'                     " & vbNewLine _
                                         & " AND DO_KENPIN_CHK.KBN_NM1      = ZAI.NRS_BR_CD           " & vbNewLine _
                                         & " AND DO_KENPIN_CHK.KBN_NM2      = ZAI.WH_CD               " & vbNewLine _
                                         & " AND DO_KENPIN_CHK.KBN_NM3      = ZAI.TOU_NO              " & vbNewLine

    Private Const SQL_SELECT_GROUP_BY_LMD010 As String = "GROUP BY                                          " & vbNewLine _
                                     & " ZAI.NRS_BR_CD                                                  " & vbNewLine _
                                     & ",ZAI.ZAI_REC_NO                                                 " & vbNewLine _
                                     & ",ZAI.WH_CD                                                      " & vbNewLine _
                                     & ",ZAI.TOU_NO                                                     " & vbNewLine _
                                     & ",ZAI.SITU_NO                                                    " & vbNewLine _
                                     & ",ZAI.ZONE_CD                                                    " & vbNewLine _
                                     & ",ZAI.LOCA                                                       " & vbNewLine _
                                     & ",ZAI.LOT_NO                                                     " & vbNewLine _
                                     & ",ZAI.CUST_CD_L                                                  " & vbNewLine _
                                     & ",ZAI.CUST_CD_M                                                  " & vbNewLine _
                                     & ",ZAI.GOODS_CD_NRS                                               " & vbNewLine _
                                     & ",ZAI.GOODS_KANRI_NO                                             " & vbNewLine _
                                     & ",ZAI.INKA_NO_L                                                  " & vbNewLine _
                                     & ",ZAI.INKA_NO_M                                                  " & vbNewLine _
                                     & ",ZAI.INKA_NO_S                                                  " & vbNewLine _
                                     & ",ZAI.ALLOC_PRIORITY                                             " & vbNewLine _
                                     & ",ZAI.RSV_NO                                                     " & vbNewLine _
                                     & ",ZAI.SERIAL_NO                                                  " & vbNewLine _
                                     & ",ZAI.HOKAN_YN                                                   " & vbNewLine _
                                     & ",ZAI.TAX_KB                                                     " & vbNewLine _
                                     & ",ZAI.GOODS_COND_KB_1                                            " & vbNewLine _
                                     & ",ZAI.GOODS_COND_KB_2                                            " & vbNewLine _
                                     & ",ZAI.GOODS_COND_KB_3                                            " & vbNewLine _
                                     & ",ZAI.OFB_KB                                                     " & vbNewLine _
                                     & ",ZAI.SPD_KB                                                     " & vbNewLine _
                                     & ",ZAI.REMARK_OUT                                                 " & vbNewLine _
                                     & ",ZAI.PORA_ZAI_NB                                                " & vbNewLine _
                                     & ",ZAI.ALCTD_NB                                                   " & vbNewLine _
                                     & ",ZAI.ALLOC_CAN_NB                                               " & vbNewLine _
                                     & ",ZAI.IRIME                                                      " & vbNewLine _
                                     & ",ZAI.PORA_ZAI_QT                                                " & vbNewLine _
                                     & ",ZAI.ALCTD_QT                                                   " & vbNewLine _
                                     & ",ZAI.ALLOC_CAN_QT                                               " & vbNewLine _
                                     & ",ZAI.INKO_DATE                                                  " & vbNewLine _
                                     & ",ZAI.INKO_PLAN_DATE                                             " & vbNewLine _
                                     & ",ZAI.ZERO_FLAG                                                  " & vbNewLine _
                                     & ",ZAI.LT_DATE                                                    " & vbNewLine _
                                     & ",ZAI.GOODS_CRT_DATE                                             " & vbNewLine _
                                     & ",ZAI.DEST_CD_P                                                  " & vbNewLine _
                                     & ",ZAI.REMARK                                                     " & vbNewLine _
                                     & ",ZAI.SMPL_FLAG                                                  " & vbNewLine _
                                     & ",Z1.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z2.KBN_NM1                                                     " & vbNewLine _
                                     & ",CUSTCOND.JOTAI_NM                                              " & vbNewLine _
                                     & ",Z3.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z4.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z5.KBN_NM1                                                     " & vbNewLine _
                                     & ",GOODS.CONSUME_PERIOD_DATE                                      " & vbNewLine _
                                     & ",Z6.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z7.KBN_NM1                                                     " & vbNewLine _
                                     & ",Z8.KBN_NM1                                                     " & vbNewLine _
                                     & ",DEST.DEST_NM                                                   " & vbNewLine _
                                     & ",GOODS.GOODS_CD_CUST                                            " & vbNewLine _
                                     & ",GOODS.GOODS_NM_1                                               " & vbNewLine _
                                     & ",GOODS.OUTKA_ATT                                                " & vbNewLine _
                                     & ",GOODS.SEARCH_KEY_1                                             " & vbNewLine _
                                     & ",GOODS.UNSO_ONDO_KB                                             " & vbNewLine _
                                     & ",GOODS.PKG_UT                                                   " & vbNewLine _
                                     & ",GOODS.STD_IRIME_NB                                             " & vbNewLine _
                                     & ",GOODS.STD_WT_KGS                                               " & vbNewLine _
                                     & ",GOODS.TARE_YN                                                  " & vbNewLine _
                                     & ",GOODS.HIKIATE_ALERT_YN                                         " & vbNewLine _
                                     & ",GOODS.PKG_NB                                                   " & vbNewLine _
                                     & ",GOODS.STD_IRIME_UT                                             " & vbNewLine _
                                     & ",GOODS.NB_UT                                                    " & vbNewLine _
                                     & ",INKA_L.INKA_DATE                                               " & vbNewLine _
                                     & ",GOODS.CUST_CD_S                                                " & vbNewLine _
                                     & ",GOODS.CUST_CD_SS                                               " & vbNewLine _
                                     & ",ZAI.SYS_UPD_DATE                                               " & vbNewLine _
                                     & ",ZAI.SYS_UPD_TIME                                               " & vbNewLine _
                                     & ",IDO.IDO_DATE                                                   " & vbNewLine _
                                     & ",INKA_L.INKO_DATE                                               " & vbNewLine _
                                     & ",INKA_L.HOKAN_STR_DATE                                          " & vbNewLine _
                                     & ",GOODS.COA_YN                                                   " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_1                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_2                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_3                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_4                                    " & vbNewLine _
                                     & ",GOODS.OUTKA_KAKO_SAGYO_KB_5                                    " & vbNewLine _
                                     & ",INKA_L.INKA_STATE_KB                                           " & vbNewLine _
                                     & ",GOODS.SIZE_KB                                                  " & vbNewLine _
                                     & ",GOODS.CUST_CD_L                                                " & vbNewLine _
                                     & ",GOODS.CUST_CD_M                                                " & vbNewLine _
                                     & ",INKA_L.FURI_NO                                                 " & vbNewLine _
                                     & ",GOODSDETAILS.SET_NAIYO                                         " & vbNewLine _
                                     & ",Z5.VALUE1                                                      " & vbNewLine _
                                     & ",ZAI.BYK_KEEP_GOODS_CD                                          " & vbNewLine _
                                     & ",KEEP_GOODS.KBN_NM1                                             " & vbNewLine _
                                     & ",CUST_DETAILS.SET_NAIYO                                         " & vbNewLine
    'END YANAI 要望番号1200 自動引当・一括引当変更
    'END YANAI 要望番号780
    'END YANAI 要望番号926
    'END YANAI 要望番号547

    ''' <summary>
    ''' 在庫データ データ抽出用（パレット用抽出・先頭／標準）
    ''' </summary>
    ''' <remarks>出荷検索／一括引当</remarks>
    Private Const SQL_SELECT_PALETTE_LMC010_BEFORE As String = "SELECT                               " & vbNewLine _
                                         & " TOU_NO                                                  " & vbNewLine _
                                         & ",SITU_NO                                                 " & vbNewLine _
                                         & ",ZONE_CD                                                 " & vbNewLine _
                                         & ",LOCA                                                    " & vbNewLine _
                                         & "FROM (                                                   " & vbNewLine

    ''' <summary>
    ''' 在庫データ データ抽出用（パレット用抽出・末尾／標準）
    ''' </summary>
    ''' <remarks>出荷検索／一括引当</remarks>
    Private Const SQL_SELECT_PALETTE_LMC010_AFTER As String = ") A                                   " & vbNewLine _
                                         & "GROUP BY                                                 " & vbNewLine _
                                         & " TOU_NO                                                  " & vbNewLine _
                                         & ",SITU_NO                                                 " & vbNewLine _
                                         & ",ZONE_CD                                                 " & vbNewLine _
                                         & ",LOCA                                                    " & vbNewLine _
                                         & "ORDER BY                                                 " & vbNewLine _
                                         & " MAX(DEST_CD_P) DESC                                     " & vbNewLine _
                                         & ",MIN(ALLOC_PRIORITY)                                     " & vbNewLine _
                                         & ",MIN(LT_DATE)                                            " & vbNewLine _
                                         & ",MIN(GOODS_CRT_DATE)                                     " & vbNewLine _
                                         & ",MIN(INKA_DATE2)                                         " & vbNewLine _
                                         & ",MIN(LOT_NO)                                             " & vbNewLine _
                                         & ",MIN(SERIAL_NO)                                          " & vbNewLine _
                                         & ",SUM(ALLOC_CAN_NB)                                       " & vbNewLine _
                                         & ",MIN(TOU_NO)                                             " & vbNewLine _
                                         & ",MIN(SITU_NO)                                            " & vbNewLine _
                                         & ",MIN(ZONE_CD)                                            " & vbNewLine _
                                         & ",MIN(LOCA)                                               " & vbNewLine _

    ''' <summary>
    ''' 在庫データ データ抽出用（パレット用抽出・先頭／標準）
    ''' </summary>
    ''' <remarks>出荷編集／自動引当</remarks>
    Private Const SQL_SELECT_PALETTE_LMC020_BEFORE As String = "SELECT                               " & vbNewLine _
                                         & " TOU_NO                                                  " & vbNewLine _
                                         & ",SITU_NO                                                 " & vbNewLine _
                                         & ",ZONE_CD                                                 " & vbNewLine _
                                         & ",LOCA                                                    " & vbNewLine _
                                         & "FROM (                                                   " & vbNewLine

    ''' <summary>
    ''' 在庫データ データ抽出用（パレット用抽出・末尾／標準）
    ''' </summary>
    ''' <remarks>出荷編集／自動引当</remarks>
    Private Const SQL_SELECT_PALETTE_LMC020_AFTER As String = ") A                                   " & vbNewLine _
                                         & "GROUP BY                                                 " & vbNewLine _
                                         & " TOU_NO                                                  " & vbNewLine _
                                         & ",SITU_NO                                                 " & vbNewLine _
                                         & ",ZONE_CD                                                 " & vbNewLine _
                                         & ",LOCA                                                    " & vbNewLine _
                                         & "ORDER BY                                                 " & vbNewLine _
                                         & " MIN(YOJITU)                                             " & vbNewLine _
                                         & ",MAX(DEST_CD_P) DESC                                     " & vbNewLine _
                                         & ",MIN(ALLOC_PRIORITY)                                     " & vbNewLine _
                                         & ",MIN(LT_DATE)                                            " & vbNewLine _
                                         & ",MIN(GOODS_CRT_DATE)                                     " & vbNewLine _
                                         & ",MIN(INKA_DATE2)                                         " & vbNewLine _
                                         & ",MIN(LOT_NO)                                             " & vbNewLine _
                                         & ",MIN(SERIAL_NO)                                          " & vbNewLine _
                                         & ",SUM(ALLOC_CAN_NB)                                       " & vbNewLine _
                                         & ",MIN(TOU_NO)                                             " & vbNewLine _
                                         & ",MIN(SITU_NO)                                            " & vbNewLine _
                                         & ",MIN(ZONE_CD)                                            " & vbNewLine _
                                         & ",MIN(LOCA)                                               " & vbNewLine _

    ''' <summary>
    ''' ORDER BY（検索時　棟優先順序制御）（パレット用抽出・先頭／日医工）
    ''' </summary>
    ''' <remarks>出荷検索／一括引当</remarks>
    Private Const SQL_SELECT_PALETTE_LMC010_TOU_BEFORE As String = "SELECT                           " & vbNewLine _
                                         & " TOU_NO                                                  " & vbNewLine _
                                         & ",SITU_NO                                                 " & vbNewLine _
                                         & ",ZONE_CD                                                 " & vbNewLine _
                                         & ",LOCA                                                    " & vbNewLine _
                                         & "FROM (                                                   " & vbNewLine _
                                         & "SELECT                                                   " & vbNewLine _
                                         & " *                                                       " & vbNewLine _
                                         & ",ISNULL((SELECT MCD.SET_NAIYO_2 FROM LM_MST..M_CUST_DETAILS MCD " & vbNewLine _
                                         & "         WHERE  MCD.NRS_BR_CD = NRS_BR_CD AND MCD.CUST_CD = CUST_CD_L AND MCD.SUB_KB = '30' AND MCD.SET_NAIYO = TOU_NO ),99) AS SORT_KEY_1 " & vbNewLine _
                                         & ",CASE ALLOC_PRIORITY WHEN '01' THEN SITU_NO ELSE 'ZZ' END AS SORT_KEY_2 " & vbNewLine _
                                         & "FROM (                                                   " & vbNewLine

    ''' <summary>
    ''' ORDER BY（検索時　棟優先順序制御）（パレット用抽出・末尾／日医工）
    ''' </summary>
    ''' <remarks>出荷検索／一括引当</remarks>
    Private Const SQL_SELECT_PALETTE_LMC010_TOU_AFTER As String = ") A                               " & vbNewLine _
                                         & ") B                                                      " & vbNewLine _
                                         & "GROUP BY                                                 " & vbNewLine _
                                         & " TOU_NO                                                  " & vbNewLine _
                                         & ",SITU_NO                                                 " & vbNewLine _
                                         & ",ZONE_CD                                                 " & vbNewLine _
                                         & ",LOCA                                                    " & vbNewLine _
                                         & "ORDER BY                                                 " & vbNewLine _
                                         & " MAX(DEST_CD_P) DESC                                     " & vbNewLine _
                                         & ",MIN(ALLOC_PRIORITY)                                     " & vbNewLine _
                                         & ",MIN(LT_DATE)                                            " & vbNewLine _
                                         & ",MIN(GOODS_CRT_DATE)                                     " & vbNewLine _
                                         & ",MIN(INKA_DATE2)                                         " & vbNewLine _
                                         & ",MIN(LOT_NO)                                             " & vbNewLine _
                                         & ",MIN(SERIAL_NO)                                          " & vbNewLine _
                                         & ",MIN(SORT_KEY_1)                                         " & vbNewLine _
                                         & ",MIN(SORT_KEY_2)                                         " & vbNewLine _
                                         & ",SUM(ALLOC_CAN_NB)                                       " & vbNewLine _
                                         & ",MIN(TOU_NO)                                             " & vbNewLine _
                                         & ",MIN(SITU_NO)                                            " & vbNewLine _
                                         & ",MIN(ZONE_CD)                                            " & vbNewLine _
                                         & ",MIN(LOCA)                                               " & vbNewLine

    ''' <summary>
    ''' ORDER BY（検索時　棟優先順序制御）（パレット用抽出・先頭／日医工）
    ''' </summary>
    ''' <remarks>出荷編集／自動引当</remarks>
    Private Const SQL_SELECT_PALETTE_LMC020_TOU_BEFORE As String = "SELECT                           " & vbNewLine _
                                         & " TOU_NO                                                  " & vbNewLine _
                                         & ",SITU_NO                                                 " & vbNewLine _
                                         & ",ZONE_CD                                                 " & vbNewLine _
                                         & ",LOCA                                                    " & vbNewLine _
                                         & "FROM (                                                   " & vbNewLine _
                                         & "SELECT                                                   " & vbNewLine _
                                         & " *                                                       " & vbNewLine _
                                         & ",ISNULL((SELECT MCD.SET_NAIYO_2 FROM LM_MST..M_CUST_DETAILS MCD " & vbNewLine _
                                         & "         WHERE  MCD.NRS_BR_CD = NRS_BR_CD AND MCD.CUST_CD = CUST_CD_L AND MCD.SUB_KB = '30' AND MCD.SET_NAIYO = TOU_NO ),99) AS SORT_KEY_1 " & vbNewLine _
                                         & "FROM (                                                   " & vbNewLine

    ''' <summary>
    ''' ORDER BY（検索時　棟優先順序制御）（パレット用抽出・末尾／日医工）
    ''' </summary>
    ''' <remarks>出荷編集／自動引当</remarks>
    Private Const SQL_SELECT_PALETTE_LMC020_TOU_AFTER As String = ") A                               " & vbNewLine _
                                         & ") B                                                      " & vbNewLine _
                                         & "GROUP BY                                                 " & vbNewLine _
                                         & " TOU_NO                                                  " & vbNewLine _
                                         & ",SITU_NO                                                 " & vbNewLine _
                                         & ",ZONE_CD                                                 " & vbNewLine _
                                         & ",LOCA                                                    " & vbNewLine _
                                         & "ORDER BY                                                 " & vbNewLine _
                                         & " MIN(YOJITU)                                             " & vbNewLine _
                                         & ",MAX(DEST_CD_P) DESC                                     " & vbNewLine _
                                         & ",MIN(ALLOC_PRIORITY)                                     " & vbNewLine _
                                         & ",MIN(LT_DATE)                                            " & vbNewLine _
                                         & ",MIN(GOODS_CRT_DATE)                                     " & vbNewLine _
                                         & ",MIN(INKA_DATE2)                                         " & vbNewLine _
                                         & ",MIN(LOT_NO)                                             " & vbNewLine _
                                         & ",MIN(SERIAL_NO)                                          " & vbNewLine _
                                         & ",MIN(SORT_KEY_1)                                         " & vbNewLine _
                                         & ",SUM(ALLOC_CAN_NB)                                       " & vbNewLine _
                                         & ",MIN(TOU_NO)                                             " & vbNewLine _
                                         & ",MIN(SITU_NO)                                            " & vbNewLine _
                                         & ",MIN(ZONE_CD)                                            " & vbNewLine _
                                         & ",MIN(LOCA)                                               " & vbNewLine

#End Region

    '2013.02.13 要望番号1824 START

#Region "SELECT_M_GOODS_DETAILS"

    Private Const SQL_M_GOODS_DETAILS As String = " SELECT                         " & vbNewLine _
                                     & " NRS_BR_CD			        AS NRS_BR_CD          " & vbNewLine _
                                     & ",GOODS_CD_NRS		        AS GOODS_CD_NRS       " & vbNewLine _
                                     & ",GOODS_CD_NRS_EDA		    AS GOODS_CD_NRS_EDA   " & vbNewLine _
                                     & ",SUB_KB		                AS SUB_KB             " & vbNewLine _
                                     & ",SET_NAIYO		            AS SET_NAIYO          " & vbNewLine _
                                     & ",REMARK		                AS REMARK             " & vbNewLine _
                                     & ",@OUTKA_PLAN_DATE		    AS OUTKA_PLAN_DATE    " & vbNewLine _
                                     & " FROM                                             " & vbNewLine _
                                     & " $LM_MST$..M_GOODS_DETAILS  M_GOODS_DETAILS       " & vbNewLine _
                                     & " WHERE                                            " & vbNewLine _
                                     & " M_GOODS_DETAILS.NRS_BR_CD   = @NRS_BR_CD         " & vbNewLine _
                                     & " AND                                              " & vbNewLine _
                                     & " M_GOODS_DETAILS.GOODS_CD_NRS   = @GOODS_CD_NRS   " & vbNewLine _
                                     & " AND                                              " & vbNewLine _
                                     & " M_GOODS_DETAILS.SUB_KB   = '34'                  " & vbNewLine

#End Region

    '2013.02.13 要望番号1824 END
#If True Then ' フィルメニッヒ セミEDI対応  20161003 added inoue

    ''' <summary>
    ''' 引当画面特殊並替え用フラグ(荷主明細Ｍ.SUB_KB=02)
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Class SORT_FLG

        ''' <summary>
        ''' 東レ
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TORAY As String = "01"

        ''' <summary>
        ''' 棟優先(日医工)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NICHIIKO As String = "02"

        ''' <summary>
        ''' 引当可能個数より置場優先(群馬DIC)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DIC As String = "03"

        ''' <summary>
        ''' LOT番号優先(ジェイティ物流)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const JT As String = "04"

        ''' <summary>
        ''' 割り当て優先(ロンザ)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const LONZA As String = "05"


        ''' <summary>
        ''' フィルメニッヒ用(商品状態指定商品優先)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const FIRMENICH As String = "06"


    End Class

#End If

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
    ''' マスタスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

    ''' <summary>
    ''' トランザクションスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnSchemaNm As String

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC040IN")

        'INTableの条件rowの格納
        _Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        _StrSql = New StringBuilder()

        'SQL作成
        _StrSql.Append(LMC040DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        'START YANAI 要望番号341
        '_StrSql.Append(LMC040DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        'Me.SQLSelectWhere()                            'SQL構築(データ抽出用Where句)

        'START YANAI 要望番号547
        'If ("02").Equals(_Row.Item("HIKIATE_FLG").ToString()) = True Then
        '    _StrSql.Append(LMC040DAC.SQL_SELECT_FROM_LMC010)      'SQL構築(データ抽出用From句)
        '    Me.SQLSelectWhere_LMC010()                            'SQL構築(データ抽出用Where句)
        'Else
        '    _StrSql.Append(LMC040DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        '    Me.SQLSelectWhere()                            'SQL構築(データ抽出用Where句)
        'End If
        If ("LMC010").Equals(_Row.Item("PGID").ToString()) = True Then
            _StrSql.Append(LMC040DAC.SQL_SELECT_FROM_LMC010)      'SQL構築(データ抽出用From句)
            Me.SQLSelectWhere_LMC010()                            'SQL構築(データ抽出用Where句)
        ElseIf ("LMC020").Equals(_Row.Item("PGID").ToString()) = True Then
            '出荷編集画面で引当選択時
            _StrSql.Append(LMC040DAC.SQL_SELECT_FROM_LMC020)      'SQL構築(データ抽出用From句)
            Me.SQLSelectWhere()                            'SQL構築(データ抽出用Where句)
        ElseIf ("LMD010").Equals(_Row.Item("PGID").ToString()) = True Then
            '在庫振替画面で引当選択時
            _StrSql.Append(LMC040DAC.SQL_SELECT_FROM_LMD010_CNT)      'SQL構築(データ抽出用From句)
            Me.SQLSelectWhere()                            'SQL構築(データ抽出用Where句)
        End If
        'END YANAI 要望番号547
        'END YANAI 要望番号341

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, _Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC040DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' データ抽出SQLの対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ抽出SQL検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC040IN")

        'INTableの条件rowの格納
        _Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        _StrSql = New StringBuilder()

        Dim likeFlg As String = _Row.Item("LIKE_FLG").ToString()

        '自動倉庫パレット用抽出を行うかのフラグ
        Dim swPalette As Boolean = False

        'SQL作成
        'START YANAI 要望番号341
        '_StrSql.Append(LMC040DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        '_StrSql.Append(LMC040DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        'Me.SQLSelectWhere()                            'SQL構築(データ抽出用Where句)
        '_StrSql.Append(LMC040DAC.SQL_SELECT_GROUP_BY)  'SQL構築(データ抽出用GROUP BY句)
        'If ("01").Equals(_Row.Item("HIKIATE_FLG").ToString()) = True Then
        '    '自動引当時
        '    'START YANAI No.4
        '    'If ("10").Equals(_Row.Item("NRS_BR_CD").ToString()) = True AndAlso _
        '    '    ("00041").Equals(_Row.Item("CUST_CD_L").ToString()) = True Then
        '    If ("01").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
        '        'END YANAI No.4
        '        _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_AUTO2)  'SQL構築(データ抽出用ORDER BY句)
        '    Else
        '        _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_AUTO)  'SQL構築(データ抽出用ORDER BY句)
        '    End If

        'Else
        '    '手動引当時
        '    _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY)  'SQL構築(データ抽出用ORDER BY句)
        'End If
        'START YANAI 要望番号547
        'If ("02").Equals(_Row.Item("HIKIATE_FLG").ToString()) = True Then
        If ("LMC010").Equals(_Row.Item("PGID").ToString()) = True Then
            'END YANAI 要望番号547
            '出荷一覧画面で一括引当選択時
            _StrSql.Append(LMC040DAC.SQL_SELECT_DATA_LMC010)      'SQL構築(データ抽出用Select句)
            _StrSql.Append(LMC040DAC.SQL_SELECT_FROM_LMC010)      'SQL構築(データ抽出用From句)
            Me.SQLSelectWhere_LMC010()                            'SQL構築(データ抽出用Where句)
            _StrSql.Append(LMC040DAC.SQL_SELECT_GROUP_BY_LMC010)  'SQL構築(データ抽出用GROUP BY句)
            'START S.Koba 要望番号1107
            If ("02").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                '日医工の場合、棟順序を考慮
                _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_LMC010_TOU)  'SQL構築(データ抽出用ORDER BY句)
                '要望番号:1592 terakawa 2012.11.15 Start
            ElseIf ("03").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                '群馬DIC(00076)の場合、引当可能個数より置場優先
                _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_DIC)  'SQL構築(データ抽出用ORDER BY句)
            ElseIf ("04").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                'ジェイティ物流の場合、LOT番号優先
                _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_JT)  'SQL構築(データ抽出用ORDER BY句)
                '要望番号:1592 terakawa 2012.11.15 End
            ElseIf ("05").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                'ロンザの場合、割り当て優先の降順
                _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_LONZA)  'SQL構築(データ抽出用ORDER BY句)
                '要望番号:1971 s.kobayashi End
                '2017.09.13 アクサルタ特殊在庫ソート対応START
            ElseIf ("08").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_AXALTA)  'SQL構築(データ抽出用ORDER BY句)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@BACKLOG_NB", String.Concat(_Row.Item("BACKLOG_NB").ToString()), DBDataType.CHAR))
                '2017.09.13 アクサルタ特殊在庫ソート対応END
            ElseIf ("11").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                '自動倉庫引当（汎用）
                _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_LMC010)        'SQL構築(データ抽出用ORDER BY句)
                swPalette = True
            ElseIf ("12").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                '日医工（自動倉庫）
                _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_LMC010_TOU)    'SQL構築(データ抽出用ORDER BY句)
                swPalette = True
            Else
                _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_LMC010)  'SQL構築(データ抽出用ORDER BY句)
            End If
            'END S.Koba 要望番号1107
        Else

            'START YANAI 要望番号547
            If ("LMC020").Equals(_Row.Item("PGID").ToString()) = True Then
                '出荷編集画面で引当選択時
                _StrSql.Append(LMC040DAC.SQL_SELECT_DATA_LMC020)      'SQL構築(データ抽出用Select句)


#If True Then ' フィルメニッヒ セミEDI対応  20161003 added inoue
                If (SORT_FLG.FIRMENICH.Equals(_Row.Item("SORT_FLG"))) Then
                    _StrSql.Append(LMC040DAC.SQL_SELECT_DATA_LMC020_ADD_FIR)
                End If
#End If
                _StrSql.Append(LMC040DAC.SQL_SELECT_FROM_LMC020)      'SQL構築(データ抽出用From句)

                Me.SQLSelectWhere(likeFlg)                            'SQL構築(データ抽出用Where句)
                'Me.SQLSelectWhere()                            'SQL構築(データ抽出用Where句)
                _StrSql.Append(LMC040DAC.SQL_SELECT_GROUP_BY_LMC020)  'SQL構築(データ抽出用GROUP BY句)

            ElseIf ("LMD010").Equals(_Row.Item("PGID").ToString()) = True Then
                '在庫振替画面で引当選択時
                _StrSql.Append(LMC040DAC.SQL_SELECT_DATA_LMD010)      'SQL構築(データ抽出用Select句)
                _StrSql.Append(LMC040DAC.SQL_SELECT_FROM_LMD010)      'SQL構築(データ抽出用From句)
                Me.SQLSelectWhere()                            'SQL構築(データ抽出用Where句)
                _StrSql.Append(LMC040DAC.SQL_SELECT_GROUP_BY_LMD010)  'SQL構築(データ抽出用GROUP BY句)
            End If
            'END YANAI 要望番号547
            If ("01").Equals(_Row.Item("HIKIATE_FLG").ToString()) = True Then
                '自動引当時
                'START YANAI No.4
                'If ("10").Equals(_Row.Item("NRS_BR_CD").ToString()) = True AndAlso _
                '    ("00041").Equals(_Row.Item("CUST_CD_L").ToString()) = True Then
                If ("01").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                    'END YANAI No.4
                    _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_AUTO2)  'SQL構築(データ抽出用ORDER BY句)

                    'START S.Koba 要望番号1107
                ElseIf ("02").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                    _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_AUTO3)  'SQL構築(データ抽出用ORDER BY句)
                    'END S.Koba 要望番号1107
                    '要望番号:1592 terakawa 2012.11.15 Start
                ElseIf ("03").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                    '群馬DIC(00076)の場合、引当可能個数より置場優先
                    _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_DIC)  'SQL構築(データ抽出用ORDER BY句)
                ElseIf ("04").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                    'ジェイティ物流の場合、LOT番号優先
                    _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_JT)  'SQL構築(データ抽出用ORDER BY句)
                    '要望番号:1592 terakawa 2012.11.15 End

#If True Then ' フィルメニッヒ セミEDI対応  20161003 added inoue
                ElseIf SORT_FLG.FIRMENICH.Equals(_Row.Item("SORT_FLG")) Then
                    _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_FIR)  'SQL構築(データ抽出用ORDER BY句)
#End If
                    '2017.09.13 アクサルタ特殊在庫ソート対応START
                ElseIf ("08").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                    _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_AXALTA)  'SQL構築(ｱｸｻﾙﾀデータ抽出用ORDER BY句)
                    _SqlPrmList.Add(MyBase.GetSqlParameter("@BACKLOG_NB", String.Concat(_Row.Item("BACKLOG_NB").ToString()), DBDataType.CHAR))
                    '2017.09.13 アクサルタ特殊在庫ソート対応END
                ElseIf ("11").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                    '自動倉庫引当（汎用）
                    _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_AUTO)  'SQL構築(データ抽出用ORDER BY句)
                    swPalette = True
                ElseIf ("12").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                    '日医工（自動倉庫）
                    _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_AUTO3) 'SQL構築(データ抽出用ORDER BY句)
                    swPalette = True
                Else
                    _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_AUTO)  'SQL構築(データ抽出用ORDER BY句)
                End If
            Else
                '手動引当時
                '要望番号:1592 terakawa 2012.11.15 Start
                If ("04").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                    'ジェイティ物流の場合、LOT番号優先
                    _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_JT)  'SQL構築(データ抽出用ORDER BY句)
                Else
                    _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY)  'SQL構築(データ抽出用ORDER BY句)
                End If
                '要望番号:1592 terakawa 2012.11.15 End
            End If
        End If
        'END YANAI 要望番号341

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, _Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)
        '20111001----------------------------
        cmd.CommandTimeout = 6000
        '------------------------------------
        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC040DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_KANRI_NO", "GOODS_KANRI_NO")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("ALLOC_PRIORITY", "ALLOC_PRIORITY")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("HOKAN_YN", "HOKAN_YN")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("ALLOC_CAN_NB", "ALLOC_CAN_NB")
        map.Add("IRIME", "IRIME")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("ALLOC_CAN_QT", "ALLOC_CAN_QT")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("INKO_PLAN_DATE", "INKO_PLAN_DATE")
        map.Add("ZERO_FLAG", "ZERO_FLAG")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("DEST_CD_P", "DEST_CD_P")
        map.Add("REMARK", "REMARK")
        map.Add("SMPL_FLAG", "SMPL_FLAG")
        map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
        map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
        map.Add("GOODS_COND_NM_3", "GOODS_COND_NM_3")
        map.Add("ALLOC_PRIORITY_NM", "ALLOC_PRIORITY_NM")
        map.Add("OFB_KB_NM", "OFB_KB_NM")
        map.Add("SPD_KB_NM", "SPD_KB_NM")
        map.Add("HIKIATE_ALERT_NM", "HIKIATE_ALERT_NM")
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        map.Add("NB_UT_NM", "NB_UT_NM")
        map.Add("IRIME_UT_NM", "IRIME_UT_NM")
        map.Add("CONSUME_PERIOD_DATE", "CONSUME_PERIOD_DATE")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("OUTKA_ATT", "OUTKA_ATT")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("TARE_YN", "TARE_YN")
        map.Add("HIKIATE_ALERT_YN", "HIKIATE_ALERT_YN")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("NB_UT", "NB_UT")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("IDO_DATE", "IDO_DATE")
        map.Add("HOKAN_STR_DATE", "HOKAN_STR_DATE")
        map.Add("COA_YN", "COA_YN")
        map.Add("OUTKA_KAKO_SAGYO_KB_1", "OUTKA_KAKO_SAGYO_KB_1")
        map.Add("OUTKA_KAKO_SAGYO_KB_2", "OUTKA_KAKO_SAGYO_KB_2")
        map.Add("OUTKA_KAKO_SAGYO_KB_3", "OUTKA_KAKO_SAGYO_KB_3")
        map.Add("OUTKA_KAKO_SAGYO_KB_4", "OUTKA_KAKO_SAGYO_KB_4")
        map.Add("OUTKA_KAKO_SAGYO_KB_5", "OUTKA_KAKO_SAGYO_KB_5")
        map.Add("SIZE_KB", "SIZE_KB")
        'START YANAI 要望番号499
        map.Add("CUST_CD_L_GOODS", "CUST_CD_L_GOODS")
        map.Add("CUST_CD_M_GOODS", "CUST_CD_M_GOODS")
        'END YANAI 要望番号499
        'START YANAI 要望番号926
        map.Add("INKA_DATE2", "INKA_DATE2")
        'END YANAI 要望番号926
        'START YANAI 要望番号780
        map.Add("INKA_DATE_KANRI_KB", "INKA_DATE_KANRI_KB")
        'END YANAI 要望番号780
        'START YANAI 要望番号1200 自動引当・一括引当変更
        map.Add("SPD_KB_FLG", "SPD_KB_FLG")
        'END YANAI 要望番号1200 自動引当・一括引当変更
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        '(2013.03.11)要望番号1229 小分け、サンプル時は入荷完了された商品にみ引当可 -- START --
        map.Add("INKA_STATE_KB", "INKA_STATE_KB")
        '(2013.03.11)要望番号1229 小分け、サンプル時は入荷完了された商品にみ引当可 --  END  --
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("SHOBO_NM", "SHOBO_NM")
        map.Add("BYK_KEEP_GOODS_CD", "BYK_KEEP_GOODS_CD")
        map.Add("KEEP_GOODS_NM", "KEEP_GOODS_NM")
        map.Add("IS_BYK_KEEP_GOODS_CD", "IS_BYK_KEEP_GOODS_CD")

        If ("LMC020").Equals(_Row.Item("PGID").ToString()) = True Then
            map.Add("OUTKA_HASU_SAGYO_KB_1", "OUTKA_HASU_SAGYO_KB_1")
            map.Add("OUTKA_HASU_SAGYO_KB_2", "OUTKA_HASU_SAGYO_KB_2")
            map.Add("OUTKA_HASU_SAGYO_KB_3", "OUTKA_HASU_SAGYO_KB_3")
        End If
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC040OUT_ZAI")

        '自動倉庫パレット用抽出
        If swPalette Then
            ds = Me.SelectListDataPalette(ds)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' データ抽出SQLの対象データ検索（自動倉庫パレット用抽出）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ抽出SQL検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListDataPalette(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC040IN")

        'INTableの条件rowの格納
        _Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        _StrSql = New StringBuilder()

        Dim likeFlg As String = _Row.Item("LIKE_FLG").ToString()

        'SQL作成
        If ("LMC010").Equals(_Row.Item("PGID").ToString()) = True Then
            '出荷一覧画面で一括引当選択時
            If ("11").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                '自動倉庫引当（汎用）
                _StrSql.Append(LMC040DAC.SQL_SELECT_PALETTE_LMC010_BEFORE)      'SQL構築(パレット用抽出・先頭／標準)
            ElseIf ("12").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                '日医工（自動倉庫）
                _StrSql.Append(LMC040DAC.SQL_SELECT_PALETTE_LMC010_TOU_BEFORE)  'SQL構築(パレット用抽出・先頭／日医工)
            End If

            _StrSql.Append(LMC040DAC.SQL_SELECT_DATA_LMC010)                    'SQL構築(データ抽出用Select句)
            _StrSql.Append(LMC040DAC.SQL_SELECT_FROM_LMC010)                    'SQL構築(データ抽出用From句)
            Me.SQLSelectWhere_LMC010()                                          'SQL構築(データ抽出用Where句)
            _StrSql.Append(LMC040DAC.SQL_SELECT_GROUP_BY_LMC010)                'SQL構築(データ抽出用GROUP BY句)

            If ("11").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                '自動倉庫引当（汎用）
                _StrSql.Append(LMC040DAC.SQL_SELECT_PALETTE_LMC010_AFTER)       'SQL構築(パレット用抽出・末尾／標準)
            ElseIf ("12").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                '日医工（自動倉庫）
                _StrSql.Append(LMC040DAC.SQL_SELECT_PALETTE_LMC010_TOU_AFTER)   'SQL構築(パレット用抽出・末尾／日医工)
            End If
        Else
            If ("LMC020").Equals(_Row.Item("PGID").ToString()) = True Then
                '出荷編集画面で引当選択時
                If ("01").Equals(_Row.Item("HIKIATE_FLG").ToString()) = True Then
                    '自動引当時
                    If ("11").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                        '自動倉庫引当（汎用）
                        _StrSql.Append(LMC040DAC.SQL_SELECT_PALETTE_LMC020_BEFORE)      'SQL構築(パレット用抽出・先頭)
                    ElseIf ("12").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                        '日医工（自動倉庫）
                        _StrSql.Append(LMC040DAC.SQL_SELECT_PALETTE_LMC020_TOU_BEFORE)  'SQL構築(パレット用抽出・先頭)
                    End If
                End If

                _StrSql.Append(LMC040DAC.SQL_SELECT_DATA_LMC020)                'SQL構築(データ抽出用Select句)
                _StrSql.Append(LMC040DAC.SQL_SELECT_FROM_LMC020)                'SQL構築(データ抽出用From句)
                Me.SQLSelectWhere(likeFlg)                                      'SQL構築(データ抽出用Where句)
                _StrSql.Append(LMC040DAC.SQL_SELECT_GROUP_BY_LMC020)            'SQL構築(データ抽出用GROUP BY句)
            End If

            If ("01").Equals(_Row.Item("HIKIATE_FLG").ToString()) = True Then
                '自動引当時
                If ("11").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                    '自動倉庫引当（汎用）
                    _StrSql.Append(LMC040DAC.SQL_SELECT_PALETTE_LMC020_AFTER)           'SQL構築(パレット用抽出・末尾／標準)
                ElseIf ("12").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                    '日医工（自動倉庫）
                    _StrSql.Append(LMC040DAC.SQL_SELECT_PALETTE_LMC020_TOU_AFTER)   'SQL構築(パレット用抽出・先頭)
                End If
            End If
        End If

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, _Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)
        cmd.CommandTimeout = 6000
        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC040DAC", "SelectListDataPalette", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC040OUT_ZAI_PALETTE")

        Return ds

    End Function

    ''' <summary>
    ''' SQLデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectDataTANINUSI(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC040IN")

        'INTableの条件rowの格納
        _Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        _StrSql = New StringBuilder()

        'SQL作成
        _StrSql.Append(LMC040DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        _StrSql.Append(LMC040DAC.SQL_SELECT_FROM_TANINUSI)      'SQL構築(データ抽出用From句)
        Me.SQLSelectWhereTANINUSI()                            'SQL構築(データ抽出用Where句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, _Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC040DAC", "SelectDataTANINUSI", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' データ抽出SQLの対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ抽出SQL検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListDataTANINUSI(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC040IN")

        'INTableの条件rowの格納
        _Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        _StrSql = New StringBuilder()

        'SQL作成
        _StrSql.Append(LMC040DAC.SQL_SELECT_DATA_TANINUSI)      'SQL構築(データ抽出用Select句)
        _StrSql.Append(LMC040DAC.SQL_SELECT_FROM_TANINUSI)      'SQL構築(データ抽出用From句)
        Me.SQLSelectWhereTANINUSI()                            'SQL構築(データ抽出用Where句)
        _StrSql.Append(LMC040DAC.SQL_SELECT_GROUP_BY_TANINUSI)  'SQL構築(データ抽出用GROUP BY句)
        _StrSql.Append(LMC040DAC.SQL_SELECT_ORDER_BY_TANINUSI)  'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, _Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC040DAC", "SelectListDataTANINUSI", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_KANRI_NO", "GOODS_KANRI_NO")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("ALLOC_PRIORITY", "ALLOC_PRIORITY")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("HOKAN_YN", "HOKAN_YN")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("ALLOC_CAN_NB", "ALLOC_CAN_NB")
        map.Add("IRIME", "IRIME")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("ALLOC_CAN_QT", "ALLOC_CAN_QT")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("INKO_PLAN_DATE", "INKO_PLAN_DATE")
        map.Add("ZERO_FLAG", "ZERO_FLAG")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("DEST_CD_P", "DEST_CD_P")
        map.Add("REMARK", "REMARK")
        map.Add("SMPL_FLAG", "SMPL_FLAG")
        map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
        map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
        map.Add("GOODS_COND_NM_3", "GOODS_COND_NM_3")
        map.Add("ALLOC_PRIORITY_NM", "ALLOC_PRIORITY_NM")
        map.Add("OFB_KB_NM", "OFB_KB_NM")
        map.Add("SPD_KB_NM", "SPD_KB_NM")
        map.Add("HIKIATE_ALERT_NM", "HIKIATE_ALERT_NM")
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        map.Add("NB_UT_NM", "NB_UT_NM")
        map.Add("IRIME_UT_NM", "IRIME_UT_NM")
        map.Add("CONSUME_PERIOD_DATE", "CONSUME_PERIOD_DATE")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("OUTKA_ATT", "OUTKA_ATT")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("TARE_YN", "TARE_YN")
        map.Add("HIKIATE_ALERT_YN", "HIKIATE_ALERT_YN")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("NB_UT", "NB_UT")
        map.Add("GOODS_CD_NRS_FROM", "GOODS_CD_NRS_FROM")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("IDO_DATE", "IDO_DATE")
        map.Add("HOKAN_STR_DATE", "HOKAN_STR_DATE")
        map.Add("COA_YN", "COA_YN")
        map.Add("OUTKA_KAKO_SAGYO_KB_1", "OUTKA_KAKO_SAGYO_KB_1")
        map.Add("OUTKA_KAKO_SAGYO_KB_2", "OUTKA_KAKO_SAGYO_KB_2")
        map.Add("OUTKA_KAKO_SAGYO_KB_3", "OUTKA_KAKO_SAGYO_KB_3")
        map.Add("OUTKA_KAKO_SAGYO_KB_4", "OUTKA_KAKO_SAGYO_KB_4")
        map.Add("OUTKA_KAKO_SAGYO_KB_5", "OUTKA_KAKO_SAGYO_KB_5")
        map.Add("SIZE_KB", "SIZE_KB")
        'START YANAI 要望番号499
        map.Add("CUST_CD_L_GOODS", "CUST_CD_L_GOODS")
        map.Add("CUST_CD_M_GOODS", "CUST_CD_M_GOODS")
        'END YANAI 要望番号499
        'START YANAI 要望番号926
        map.Add("INKA_DATE2", "INKA_DATE2")
        'END YANAI 要望番号926
        'START YANAI 要望番号780
        map.Add("INKA_DATE_KANRI_KB", "INKA_DATE_KANRI_KB")
        'END YANAI 要望番号780
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        '(2013.03.11)要望番号1229 小分け、サンプル時は入荷完了された商品にみ引当可 -- START --
        map.Add("INKA_STATE_KB", "INKA_STATE_KB")
        '(2013.03.11)要望番号1229 小分け、サンプル時は入荷完了された商品にみ引当可 --  END  --
        '(2015.11.27)要望番号2407　他荷主対応
        map.Add("IRIME_UT", "IRIME_UT")
        '(2015.11.27)要望番号2407　他荷主対応
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("SHOBO_NM", "SHOBO_NM")
        map.Add("BYK_KEEP_GOODS_CD", "BYK_KEEP_GOODS_CD")
        map.Add("KEEP_GOODS_NM", "KEEP_GOODS_NM")
        map.Add("IS_BYK_KEEP_GOODS_CD", "IS_BYK_KEEP_GOODS_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC040OUT_ZAI")

        Return ds

    End Function

    ''' <summary>
    ''' 検索用SQL WHERE句作成
    ''' </summary>
    ''' <remarks>SQL検索用SQLの構築</remarks>
    Private Sub SQLSelectWhere(Optional ByVal likeFlg As String = "")

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        'SQLパラメータ初期化
        _SqlPrmList = New ArrayList()

        _StrSql.Append("WHERE                                                        ")
        _StrSql.Append(vbNewLine)
        _StrSql.Append("ZAI.SYS_DEL_FLG = '0'                                        ")
        _StrSql.Append(vbNewLine)
        _StrSql.Append(" AND ZAI.ALLOC_CAN_NB > 0                                    ")
        _StrSql.Append(vbNewLine)
        'START YANAI 要望番号341
        _StrSql.Append(" AND ZAI.ZAI_REC_NO <> ''                                    ")
        _StrSql.Append(vbNewLine)
        'END YANAI 要望番号341

        ' 検品チェック対象の商品で未検品(庫内作業ステータス検品済未満)は、引当から除外する。
        _StrSql.Append(" AND (DO_KENPIN_CHK.KBN_CD IS NULL OR INKA_L.WH_KENPIN_WK_STATUS >= '03')")
        _StrSql.Append(vbNewLine)

        'Del Start 2019/10/10 要望管理007373
        ''Add Start 2019/08/01 要望管理005237
        '_StrSql.Append(" AND (INKA_L.STOP_ALLOC IS NULL OR INKA_L.STOP_ALLOC <> '1')  -- ADD 2019/08/01 要望管理005237")
        '_StrSql.Append(vbNewLine)
        ''Add End   2019/08/01 要望管理005237
        'Del End   2019/10/10 要望管理007373

        With _Row
            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.NRS_BR_CD = @NRS_BR_CD                              ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '倉庫コード
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.WH_CD = @WH_CD                                      ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '荷主コード（大）
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.CUST_CD_L = @CUST_CD_L                              ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '荷主コード（中）
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.CUST_CD_M = @CUST_CD_M                              ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr), DBDataType.CHAR))
            End If

            If likeFlg.Equals("1") = False Then

                '商品コード（商品キー）
                whereStr = .Item("GOODS_CD_NRS").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    _StrSql.Append(" AND ZAI.GOODS_CD_NRS = @GOODS_CD_NRS                        ")
                    _StrSql.Append(vbNewLine)
                    _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", String.Concat(whereStr), DBDataType.CHAR))
                End If

            End If

            '商品コード（商品コード）
            whereStr = .Item("GOODS_CD_CUST").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                If likeFlg.Equals("1") = False Then
                    _StrSql.Append(" AND GOODS.GOODS_CD_CUST LIKE @GOODS_CD_CUST                        ")
                    _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                Else
                    _StrSql.Append(" AND GOODS.GOODS_CD_CUST = @GOODS_CD_CUST                        ")
                    _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat(whereStr), DBDataType.NVARCHAR))
                End If

                _StrSql.Append(vbNewLine)
                'START YANAI 要望番号886
                '_SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat(whereStr), DBDataType.CHAR))
                '_SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat(whereStr), DBDataType.NVARCHAR))
                'END YANAI 要望番号886
            End If

            If likeFlg.Equals("1") = False Then

                'START YANAI 要望番号412
                '商品名（商品名）
                whereStr = .Item("GOODS_NM").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    _StrSql.Append(" AND GOODS.GOODS_NM_1 = @GOODS_NM                        ")
                    _StrSql.Append(vbNewLine)
                    _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat(whereStr), DBDataType.NVARCHAR))
                End If
                'END YANAI 要望番号412

            End If

            'シリアル№
            whereStr = .Item("SERIAL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.SERIAL_NO LIKE @SERIAL_NO                           ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '予約番号
            whereStr = .Item("RSV_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.RSV_NO = @RSV_NO                                    ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@RSV_NO", String.Concat(whereStr), DBDataType.NVARCHAR))
            End If

            'ロット番号
            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'START YANAI 要望番号981
                '_StrSql.Append(" AND ZAI.LOT_NO LIKE @LOT_NO                                 ")
                '_StrSql.Append(vbNewLine)
                '_SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                If ("01").Equals(.Item("HIKIATE_FLG").ToString()) = True Then
                    '自動引当時は条件がイコール
                    _StrSql.Append(" AND ZAI.LOT_NO = @LOT_NO                                 ")
                    _StrSql.Append(vbNewLine)
                    _SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", whereStr, DBDataType.NVARCHAR))
                Else
                    '手動引当時は条件がLIKE
                    _StrSql.Append(" AND ZAI.LOT_NO LIKE @LOT_NO                                 ")
                    _StrSql.Append(vbNewLine)
                    _SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                End If
                'END YANAI 要望番号981
            End If

            '入目
            whereStr = .Item("IRIME").ToString()
            If String.IsNullOrEmpty(whereStr) = False AndAlso 0 < Convert.ToDouble(whereStr) Then
                _StrSql.Append(" AND ZAI.IRIME = @IRIME                                      ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", String.Concat(whereStr), DBDataType.NUMERIC))
            End If

            '棟
            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.TOU_NO LIKE @TOU_NO                                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '室
            whereStr = .Item("SITU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.SITU_NO LIKE @SITU_NO                               ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'ゾーン
            whereStr = .Item("ZONE_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.ZONE_CD LIKE @ZONE_CD                                ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'ロケーション
            whereStr = .Item("LOCA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.LOCA LIKE @LOCA                                      ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '商品状態区分1
            whereStr = .Item("GOODS_COND_KB_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.GOODS_COND_KB_1 = @GOODS_COND_KB_1                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '商品状態区分2
            whereStr = .Item("GOODS_COND_KB_2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.GOODS_COND_KB_2 = @GOODS_COND_KB_2                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '商品状態区分3
            whereStr = .Item("GOODS_COND_KB_3").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.GOODS_COND_KB_3 = @GOODS_COND_KB_3                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", String.Concat(whereStr), DBDataType.CHAR))
            End If

            'REMARK
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.REMARK LIKE @REMARK                                ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '簿外品区分
            whereStr = .Item("OFB_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.OFB_KB = @OFB_KB                                   ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@OFB_KB", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '保留品区分
            whereStr = .Item("SPD_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.SPD_KB = @SPD_KB                                   ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@SPD_KB", String.Concat(whereStr), DBDataType.CHAR))
            End If

            ' BYKキープ品コード
            If ("00").Equals(_Row.Item("HIKIATE_FLG").ToString()) Then
                ' 手動引当の場合
                whereStr = .Item("BYK_KEEP_GOODS_CD").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    _StrSql.Append(" AND ZAI.BYK_KEEP_GOODS_CD = @BYK_KEEP_GOODS_CD                                   ")
                    _StrSql.Append(vbNewLine)
                    _SqlPrmList.Add(MyBase.GetSqlParameter("@BYK_KEEP_GOODS_CD", String.Concat(whereStr), DBDataType.NVARCHAR))
                End If
            Else
                ' 手動引当以外(自動引当または一括引当) の場合
                ' キープ品の設定されている在庫は引当対象外とする
                ' (キープ品の設定されていない在庫のみ引当対象とする)
                _StrSql.Append(" AND RTRIM(ZAI.BYK_KEEP_GOODS_CD) = ''                       ")
                _StrSql.Append(vbNewLine)
            End If

            'REMARK_OUT
            whereStr = .Item("REMARK_OUT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.REMARK_OUT LIKE @REMARK_OUT                                ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '在庫レコード番号
            whereStr = .Item("ZAI_REC_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.ZAI_REC_NO LIKE @ZAI_REC_NO                        ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '課税区分
            whereStr = .Item("TAX_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.TAX_KB = @TAX_KB                                   ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '引当注意品
            whereStr = .Item("HIKIATE_ALERT_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND GOODS.HIKIATE_ALERT_YN = @HIKIATE_ALERT_YN                                   ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@HIKIATE_ALERT_YN", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND (DEST.DEST_NM LIKE @DEST_NM                                   ")
                _StrSql.Append(" OR ZAI.DEST_CD_P = '')                                      ")
                _StrSql.Append(vbNewLine)
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            Else
                _StrSql.Append(" AND ZAI.DEST_CD_P = ''                                      ")
                _StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

    'START YANAI 要望番号341
    ''' <summary>
    ''' 検索用SQL WHERE句作成
    ''' </summary>
    ''' <remarks>SQL検索用SQLの構築</remarks>
    Private Sub SQLSelectWhere_LMC010()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        'SQLパラメータ初期化
        _SqlPrmList = New ArrayList()

        _StrSql.Append("WHERE                                                        ")
        _StrSql.Append(vbNewLine)
        _StrSql.Append("ZAI.SYS_DEL_FLG = '0'                                        ")
        _StrSql.Append(vbNewLine)
        _StrSql.Append(" AND ZAI.ALLOC_CAN_NB > 0                                    ")
        _StrSql.Append(vbNewLine)
        _StrSql.Append(" AND ZAI.ZAI_REC_NO <> ''                                    ")
        _StrSql.Append(vbNewLine)

        ' 検品チェック対象の商品で未検品(庫内作業ステータス検品済未満)は、引当から除外する。
        _StrSql.Append(" AND (DO_KENPIN_CHK.KBN_CD IS NULL OR INKA_L.WH_KENPIN_WK_STATUS >= '03')")
        _StrSql.Append(vbNewLine)

        'Del Start 2019/10/10 要望管理007373
        ''Add Start 2019/08/01 要望管理005237
        '_StrSql.Append(" AND (INKA_L.STOP_ALLOC IS NULL OR INKA_L.STOP_ALLOC <> '1')  -- ADD 2019/08/01 要望管理005237")
        '_StrSql.Append(vbNewLine)
        ''Add End   2019/08/01 要望管理005237
        'Del End   2019/10/10 要望管理007373

        With _Row
            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.NRS_BR_CD = @NRS_BR_CD                              ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '倉庫コード
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.WH_CD = @WH_CD                                      ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '荷主コード（大）
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.CUST_CD_L = @CUST_CD_L                              ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '荷主コード（中）
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.CUST_CD_M = @CUST_CD_M                              ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '商品コード（商品キー）
            whereStr = .Item("GOODS_CD_NRS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.GOODS_CD_NRS = @GOODS_CD_NRS                        ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '2017.10.18 アクサルタ特殊在庫ソート対応START
            If ("08").Equals(_Row.Item("SORT_FLG").ToString()) = True Then
                _StrSql.Append(" AND ZAI.GOODS_COND_KB_3 = ''                                    ")
            End If
            '2017.10.18 アクサルタ特殊在庫ソート対応END

            'シリアル№
            whereStr = .Item("SERIAL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'START KIM 倉庫システムVer2.0 特定荷主対応 2012/9/20
                '_StrSql.Append(" AND ZAI.SERIAL_NO LIKE @SERIAL_NO                           ")
                '_StrSql.Append(vbNewLine)
                '_SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                If "03".Equals(.Item("HIKIATE_FLG").ToString()) Then
                    'ハネウェルCSV引当の場合は【=】条件でデータを抽出する
                    _StrSql.Append(" AND ZAI.SERIAL_NO = @SERIAL_NO                           ")
                    _StrSql.Append(vbNewLine)
                    _SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat(whereStr), DBDataType.NVARCHAR))
                Else
                    _StrSql.Append(" AND ZAI.SERIAL_NO LIKE @SERIAL_NO                           ")
                    _StrSql.Append(vbNewLine)
                    _SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                End If
                'END KIM 倉庫システムVer2.0 特定荷主対応 2012/9/20
            End If

            '予約番号
            whereStr = .Item("RSV_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.RSV_NO = @RSV_NO                                    ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@RSV_NO", String.Concat(whereStr), DBDataType.NVARCHAR))
            End If

            'ロット番号
            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'START YANAI 要望番号981
                '_StrSql.Append(" AND ZAI.LOT_NO LIKE @LOT_NO                                 ")
                '_StrSql.Append(vbNewLine)
                '_SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                _StrSql.Append(" AND ZAI.LOT_NO = @LOT_NO                                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", whereStr, DBDataType.NVARCHAR))
                'END YANAI 要望番号981
            End If

            '入目
            whereStr = .Item("IRIME").ToString()
            If String.IsNullOrEmpty(whereStr) = False AndAlso 0 < Convert.ToDouble(whereStr) Then
                _StrSql.Append(" AND ZAI.IRIME = @IRIME                                      ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", String.Concat(whereStr), DBDataType.NUMERIC))
            End If

            '棟
            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.TOU_NO LIKE @TOU_NO                                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '室
            whereStr = .Item("SITU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.SITU_NO LIKE @SITU_NO                               ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'ゾーン
            whereStr = .Item("ZONE_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.ZONE_CD LIKE @ZONE_CD                                ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'ロケーション
            whereStr = .Item("LOCA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.LOCA LIKE @LOCA                                      ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '商品状態区分1
            whereStr = .Item("GOODS_COND_KB_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.GOODS_COND_KB_1 = @GOODS_COND_KB_1                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '商品状態区分2
            whereStr = .Item("GOODS_COND_KB_2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.GOODS_COND_KB_2 = @GOODS_COND_KB_2                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '商品状態区分3
            whereStr = .Item("GOODS_COND_KB_3").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.GOODS_COND_KB_3 = @GOODS_COND_KB_3                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", String.Concat(whereStr), DBDataType.CHAR))
            End If

            'REMARK
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.REMARK LIKE @REMARK                                ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '簿外品区分
            whereStr = .Item("OFB_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.OFB_KB = @OFB_KB                                   ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@OFB_KB", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '保留品区分
            whereStr = .Item("SPD_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.SPD_KB = @SPD_KB                                   ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@SPD_KB", String.Concat(whereStr), DBDataType.CHAR))
            End If

            ' BYKキープ品コード
            ' (呼出元画面が出荷検索の場合は常に一括引当であることを前提に)
            ' 一括引当の場合
            ' キープ品の設定されている在庫は引当対象外とする
            ' (キープ品の設定されていない在庫のみ引当対象とする)
            _StrSql.Append(" AND RTRIM(ZAI.BYK_KEEP_GOODS_CD) = ''                       ")
            _StrSql.Append(vbNewLine)

            'REMARK_OUT
            whereStr = .Item("REMARK_OUT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.REMARK_OUT LIKE @REMARK_OUT                                ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '在庫レコード番号
            whereStr = .Item("ZAI_REC_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.ZAI_REC_NO LIKE @ZAI_REC_NO                        ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '課税区分
            whereStr = .Item("TAX_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.TAX_KB = @TAX_KB                                   ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '入荷日より後の出荷
            whereStr = .Item("OUTKA_PLAN_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.INKO_DATE <= @OUTKA_PLAN_DATE                                   ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND (DEST.DEST_NM LIKE @DEST_NM                                   ")
                _StrSql.Append(" OR ZAI.DEST_CD_P = '')                                      ")
                _StrSql.Append(vbNewLine)
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            Else
                _StrSql.Append(" AND ZAI.DEST_CD_P = ''                                      ")
                _StrSql.Append(vbNewLine)
            End If

        End With

    End Sub
    'END YANAI 要望番号341

    ''' <summary>
    ''' 検索用SQL WHERE句作成
    ''' </summary>
    ''' <remarks>SQL検索用SQLの構築</remarks>
    Private Sub SQLSelectWhereTANINUSI()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        'SQLパラメータ初期化
        _SqlPrmList = New ArrayList()

        _StrSql.Append("WHERE                                                        ")
        _StrSql.Append(vbNewLine)
        _StrSql.Append("ZAI.SYS_DEL_FLG = '0'                                        ")
        _StrSql.Append(vbNewLine)
        _StrSql.Append(" AND ZAI.ALLOC_CAN_NB > 0                                    ")
        _StrSql.Append(vbNewLine)

        'Mod Start 2019/10/10 要望管理007373
        ''Add Start 2019/08/01 要望管理005237
        '_StrSql.Append(" AND (INKA_L.STOP_ALLOC IS NULL OR INKA_L.STOP_ALLOC <> '1')  -- ADD 2019/08/01 要望管理005237")
        '_StrSql.Append(vbNewLine)
        ''Add End   2019/08/01 要望管理005237
        'Mod End   2019/10/10 要望管理007373

        With _Row
            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.NRS_BR_CD = @NRS_BR_CD                              ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", String.Concat(whereStr), DBDataType.CHAR))
            End If

            '商品コード（商品キー）
            whereStr = .Item("GOODS_CD_NRS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND FURIGOODS.CD_NRS = @GOODS_CD_NRS                        ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", String.Concat(whereStr), DBDataType.CHAR))
            End If

            'START YANAI 要望番号554
            'ロット番号
            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                _StrSql.Append(" AND ZAI.LOT_NO LIKE @LOT_NO                                 ")
                _StrSql.Append(vbNewLine)
                _SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If
            'END YANAI 要望番号554

            ' BYKキープ品コード
            If Not ("00").Equals(_Row.Item("HIKIATE_FLG").ToString()) Then
                ' 手動引当以外(自動引当または一括引当) の場合
                ' キープ品の設定されている在庫は引当対象外とする
                ' (キープ品の設定されていない在庫のみ引当対象とする)
                _StrSql.Append(" AND RTRIM(ZAI.BYK_KEEP_GOODS_CD) = ''                       ")
                _StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

    '2013.02.13 要望番号1824 START

#Region "商品明細マスタパラメータ設定"
    ''' <summary>
    ''' 商品明細マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMGoodstDetailsParameter(ByVal dtIn As DataTable, ByVal dtZai As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dtZai.Rows(0).Item("NRS_BR_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", dtZai.Rows(0).Item("GOODS_CD_NRS"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", dtIn.Rows(0).Item("OUTKA_PLAN_DATE"), DBDataType.NVARCHAR))

    End Sub

#End Region

    ''' <summary>
    ''' データ抽出SQLの対象データ(商品明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ抽出SQL検索結果取得SQLの構築・発行</remarks>
    Private Function SelectMGoodsDetailList(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC040IN")
        Dim zaiTbl As DataTable = ds.Tables("LMC040OUT_ZAI")

        'INTableの条件rowの格納
        _Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        _StrSql = New StringBuilder()

        Me._SqlPrmList = New ArrayList()

        'SQL作成
        _StrSql.Append(LMC040DAC.SQL_M_GOODS_DETAILS)      'SQL構築(データ抽出用Select句)

        'スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(_StrSql.ToString, _Row.Item("NRS_BR_CD").ToString())

        Call Me.SetMGoodstDetailsParameter(inTbl, zaiTbl)
        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC040DAC", "SelectMGoodsDetailList", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC040_M_GOODS_DETAILS")

        Return ds

    End Function

    '2013.02.13 要望番号1824 END

#End Region

#Region "変更処理"


#End Region

#Region "設定処理"

#Region "SQL"

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系のスキーマ名を設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系のスキーマ名を設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#End Region

#End Region

End Class
