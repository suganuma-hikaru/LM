' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH560    : EDI入出荷受信帳票(ダウ・ケミカル)
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH560DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH560DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       HED.NRS_BR_CD                                    AS NRS_BR_CD " & vbNewLine _
                                            & "      , '80'                                             AS PTN_ID    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD              " & vbNewLine _
                                            & "        ELSE MR3.PTN_CD END                              AS PTN_CD    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID END                              AS RPT_ID    " & vbNewLine

    ''' <summary>
    ''' 帳票種別取得用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' ダウケミEDI受信データHEAD - ダウケミEDI受信データDETAIL,商品Ｍ,区分Ｍ
    ''' </remarks>
    Private Const SQL_MPrt_FROM As String = "  FROM $LM_TRN$..H_INOUTKAEDI_HED_DOW  HED                          " & vbNewLine _
                                          & " --【Notes】№1007/1008対応 --- START ---                           " & vbNewLine _
                                          & "      -- EDI印刷種別テーブル                                        " & vbNewLine _
                                          & "      LEFT JOIN (                                                   " & vbNewLine _
                                          & "                  SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT           " & vbNewLine _
                                          & "                       , H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                          & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                          & "                    FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT          " & vbNewLine _
                                          & "                   WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD       " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L       " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M       " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.PRINT_TP    = '02'             " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB        " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'              " & vbNewLine _
                                          & "                   GROUP BY                                         " & vbNewLine _
                                          & "                         H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                          & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                          & "                ) HEDIPRINT                                         " & vbNewLine _
                                          & "             ON HEDIPRINT.NRS_BR_CD  = HED.NRS_BR_CD                " & vbNewLine _
                                          & "            AND HEDIPRINT.EDI_CTL_NO = HED.EDI_CTL_NO               " & vbNewLine _
                                          & " --【Notes】№1007/1008対応 ---  END  ---                           " & vbNewLine _
                                          & "      -- ダウケミEDI受信データ                                      " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_TRN$..H_INOUTKAEDI_DTL_DOW  DTL           " & vbNewLine _
                                          & "                   ON DTL.CRT_DATE  = HED.CRT_DATE                  " & vbNewLine _
                                          & "                  AND DTL.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
                                          & "                  AND DTL.REC_NO    = HED.REC_NO                    " & vbNewLine _
                                          & "      -- 商品マスタ                                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                          & "                   ON M_GOODS.NRS_BR_CD      = HED.NRS_BR_CD        " & vbNewLine _
                                          & "                  AND M_GOODS.GOODS_CD_NRS   = DTL.M1_IMATERIAL_CD  " & vbNewLine _
                                          & "      -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_DOWの荷主より取得)   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                          & "                   ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_L   = HED.CUST_CD_L        " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_M   = HED.CUST_CD_M        " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.PTN_ID      = '80'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                          " & vbNewLine _
                                          & "                   ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD " & vbNewLine _
                                          & "                  AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID    " & vbNewLine _
                                          & "                  AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD    " & vbNewLine _
                                          & "                  AND MR1.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                          & "      -- 帳票パターンマスタ②(商品マスタより)                       " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2               " & vbNewLine _
                                          & "                   ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD    " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L    " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M    " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.CUST_CD_S   = '00'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.PTN_ID      = '80'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                          & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                          & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                          & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                          & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                          & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                          & "                   ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       " & vbNewLine _
                                          & "                  AND MR3.PTN_ID             = '80'                 " & vbNewLine _
                                          & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                          & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine

    ''' <summary>
    ''' 帳票種別取得用 WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_WHERE As String = " WHERE                                     " & vbNewLine _
                                           & "       HED.NRS_BR_CD     =  @NRS_BR_CD     " & vbNewLine _
                                           & "   AND HED.CUST_CD_L     =  @CUST_CD_L     " & vbNewLine _
                                           & "   AND HED.CUST_CD_M     =  @CUST_CD_M     " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                    " & vbNewLine _
                                       & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID   " & vbNewLine _
                                       & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID   " & vbNewLine _
                                       & "        ELSE MR3.RPT_ID END        AS RPT_ID               " & vbNewLine _
                                       & "      , HED.DEL_KB                 AS H_DEL_KB             " & vbNewLine _
                                       & "      , HED.CRT_DATE               AS CRT_DATE             " & vbNewLine _
                                       & "      , HED.FILE_NAME              AS FILE_NAME            " & vbNewLine _
                                       & "      , HED.REC_NO                 AS REC_NO               " & vbNewLine _
                                       & "      , HED.NRS_BR_CD              AS NRS_BR_CD            " & vbNewLine _
                                       & "      , HED.NRS_WH_CD              AS NRS_WH_CD            " & vbNewLine _
                                       & "      , HED.INOUT_KB               AS INOUT_KB             " & vbNewLine _
                                       & "      , ''                         AS INOUT_KB_NM          " & vbNewLine _
                                       & "      , HED.EDI_CTL_NO             AS EDI_CTL_NO           " & vbNewLine _
                                       & "      , HED.INKA_CTL_NO_L          AS INKA_CTL_NO_L        " & vbNewLine _
                                       & "      , HED.OUTKA_CTL_NO           AS OUTKA_CTL_NO         " & vbNewLine _
                                       & "      , HED.CUST_CD_L              AS CUST_CD_L            " & vbNewLine _
                                       & "      , HED.CUST_CD_M              AS CUST_CD_M            " & vbNewLine _
                                       & "      , HED.PRTFLG                 AS PRTFLG               " & vbNewLine _
                                       & "      , HED.CANCEL_FLG             AS CANCEL_FLG           " & vbNewLine _
                                       & "      , HED.H1_HREC_TYP1           AS H1_HREC_TYP1         " & vbNewLine _
                                       & "      , HED.H2_HREC_TYP2           AS H2_HREC_TYP2         " & vbNewLine _
                                       & "      , HED.H3_HREC_TYP3           AS H3_HREC_TYP3         " & vbNewLine _
                                       & "      , HED.H4_HREC_TYP4           AS H4_HREC_TYP4         " & vbNewLine _
                                       & "      , HED.H5_HREC_TYP5           AS H5_HREC_TYP5         " & vbNewLine _
                                       & "      , HED.H6_HREC_TYP6           AS H6_HREC_TYP6         " & vbNewLine _
                                       & "      , HED.H7_HREC_TYP7           AS H7_HREC_TYP7         " & vbNewLine _
                                       & "      , HED.H1_HCOM_CODE1          AS H1_HCOM_CODE1        " & vbNewLine _
                                       & "      , HED.H1_HDEL_NO1            AS H1_HDEL_NO1          " & vbNewLine _
                                       & "      , HED.H1_HSHIP_NO1           AS H1_HSHIP_NO1         " & vbNewLine _
                                       & "      , HED.H1_HORD_STATUS         AS H1_HORD_STATUS       " & vbNewLine _
                                       & "      , ''                         AS H1_HORD_STATUS_NM    " & vbNewLine _
                                       & "      , HED.H1_HCOM_NAME           AS H1_HCOM_NAME         " & vbNewLine _
                                       & "      , HED.H1_HORD_TYP            AS H1_HORD_TYP          " & vbNewLine _
                                       & "      , ''                         AS H1_HORD_TYP_NM       " & vbNewLine _
                                       & "      , HED.H1_HSYS_DATE           AS H1_HSYS_DATE         " & vbNewLine _
                                       & "      , HED.H1_HSYS_TIME           AS H1_HSYS_TIME         " & vbNewLine _
                                       & "      , HED.H1_HDEL_TERM           AS H1_HDEL_TERM         " & vbNewLine _
                                       & "      , HED.H1_HDEL_PLANT_CD       AS H1_HDEL_PLANT_CD     " & vbNewLine _
                                       & "      , HED.H1_HDEL_PLANT_NM       AS H1_HDEL_PLANT_NM     " & vbNewLine _
                                       & "      , HED.H1_HDEL_PLANT_CD1      AS H1_HDEL_PLANT_CD1    " & vbNewLine _
                                       & "      , HED.H1_HSEL_PLANT_CD       AS H1_HSEL_PLANT_CD     " & vbNewLine _
                                       & "      , HED.H1_HSEL_PLANT_NM       AS H1_HSEL_PLANT_NM     " & vbNewLine _
                                       & "      , HED.H1_HREC_WH_CD          AS H1_HREC_WH_CD        " & vbNewLine _
                                       & "      , HED.H1_HREC_WH_NM          AS H1_HREC_WH_NM        " & vbNewLine _
                                       & "      , HED.H1_HCON_CD             AS H1_HCON_CD           " & vbNewLine _
                                       & "      , HED.H1_HCON_NM1            AS H1_HCON_NM1          " & vbNewLine _
                                       & "      , HED.H1_HCON_NM2            AS H1_HCON_NM2          " & vbNewLine _
                                       & "      , HED.H1_HCON_PHONE_NO       AS H1_HCON_PHONE_NO     " & vbNewLine _
                                       & "      , HED.H1_HPAR_NO             AS H1_HPAR_NO           " & vbNewLine _
                                       & "      , HED.H1_HREQ_FLAG           AS H1_HREQ_FLAG         " & vbNewLine _
                                       & "      , ''                         AS H1_HREQ_FLAG_NM      " & vbNewLine _
                                       & "      , HED.H1_HEXPORT_MARK        AS H1_HEXPORT_MARK      " & vbNewLine _
                                       & "      , HED.H1_HORD_NO             AS H1_HORD_NO           " & vbNewLine _
                                       & "      , HED.H2_HDEL_DATE           AS H2_HDEL_DATE         " & vbNewLine _
                                       & "      , HED.H2_HSHIP_DATE          AS H2_HSHIP_DATE        " & vbNewLine _
                                       & "      , HED.H2_HCON_NM3            AS H2_HCON_NM3          " & vbNewLine _
                                       & "      , HED.H2_HCON_NM4            AS H2_HCON_NM4          " & vbNewLine _
                                       & "      , HED.H2_HCON_ADD1           AS H2_HCON_ADD1         " & vbNewLine _
                                       & "      , HED.H2_HCON_ADD2           AS H2_HCON_ADD2         " & vbNewLine _
                                       & "      , HED.H2_HCON_CITY           AS H2_HCON_CITY         " & vbNewLine _
                                       & "      , HED.H2_HCON_ADD3           AS H2_HCON_ADD3         " & vbNewLine _
                                       & "      , HED.H2_HCON_ADD4           AS H2_HCON_ADD4         " & vbNewLine _
                                       & "      , HED.H2_HCON_POST_CD        AS H2_HCON_POST_CD      " & vbNewLine _
                                       & "      , HED.H2_HCON_POST_AREA      AS H2_HCON_POST_AREA    " & vbNewLine _
                                       & "      , HED.H2_HCON_LOCATION       AS H2_HCON_LOCATION     " & vbNewLine _
                                       & "      , HED.H3_HCARR_CD            AS H3_HCARR_CD          " & vbNewLine _
                                       & "      , HED.H3_HCARR_NM            AS H3_HCARR_NM          " & vbNewLine _
                                       & "      , HED.H3_HDAIKAN             AS H3_HDAIKAN           " & vbNewLine _
                                       & "      , HED.H3_HTEXT1              AS H3_HTEXT1            " & vbNewLine _
                                       & "      , HED.H3_HTEXT2              AS H3_HTEXT2            " & vbNewLine _
                                       & "      , HED.H3_HOLD_CARR_CD        AS H3_HOLD_CARR_CD      " & vbNewLine _
                                       & "      , HED.H3_HFILE_VERSION       AS H3_HFILE_VERSION     " & vbNewLine _
                                       & "      , HED.H3_HSHIP_NM_KAN        AS H3_HSHIP_NM_KAN      " & vbNewLine _
                                       & "      , HED.H3_HCARR_NM_KAN        AS H3_HCARR_NM_KAN      " & vbNewLine _
                                       & "      , HED.H4_HCON_NM_KAN         AS H4_HCON_NM_KAN       " & vbNewLine _
                                       & "      , HED.H4_HCON_ADD_KAN        AS H4_HCON_ADD_KAN      " & vbNewLine _
                                       & "      , HED.H4_HCON_STR1_KAN       AS H4_HCON_STR1_KAN     " & vbNewLine _
                                       & "      , HED.H4_HCON_STR2_KAN       AS H4_HCON_STR2_KAN     " & vbNewLine _
                                       & "      , HED.H4_HCON_PHONE_NO1      AS H4_HCON_PHONE_NO1    " & vbNewLine _
                                       & "      , HED.H4_HCON_ADDRESS_KAN    AS H4_HCON_ADDRESS_KAN  " & vbNewLine _
                                       & "      , HED.H5_HCUST_NO            AS H5_HCUST_NO          " & vbNewLine _
                                       & "      , HED.H5_HSOLD_CD            AS H5_HSOLD_CD          " & vbNewLine _
                                       & "      , HED.H5_HSOLD_NM            AS H5_HSOLD_NM          " & vbNewLine _
                                       & "      , HED.H5_HCSR_NO             AS H5_HCSR_NO           " & vbNewLine _
                                       & "      , HED.H5_HCSR_NM             AS H5_HCSR_NM           " & vbNewLine _
                                       & "      , HED.H5_HSOLD_NM_KAN        AS H5_HSOLD_NM_KAN      " & vbNewLine _
                                       & "      , HED.H5_HOTHER_NM1_KAN      AS H5_HOTHER_NM1_KAN    " & vbNewLine _
                                       & "      , HED.H5_HOTEHR_NM2_KAN      AS H5_HOTEHR_NM2_KAN    " & vbNewLine _
                                       & "      , HED.H6_HCARR_TEXT1         AS H6_HCARR_TEXT1       " & vbNewLine _
                                       & "      , HED.H6_HCARR_TEXT2         AS H6_HCARR_TEXT2       " & vbNewLine _
                                       & "      , HED.H6_HCARR_TEXT3         AS H6_HCARR_TEXT3       " & vbNewLine _
                                       & "      , HED.H6_HCARR_TEXT4         AS H6_HCARR_TEXT4       " & vbNewLine _
                                       & "      , HED.H6_HCARR_TEXT5         AS H6_HCARR_TEXT5       " & vbNewLine _
                                       & "      , HED.H7_HDELV_TEXT1         AS H7_HDELV_TEXT1       " & vbNewLine _
                                       & "      , HED.H7_HDELV_TEXT2         AS H7_HDELV_TEXT2       " & vbNewLine _
                                       & "      , HED.H7_HDELV_TEXT3         AS H7_HDELV_TEXT3       " & vbNewLine _
                                       & "      , HED.H7_HDELV_TEXT4         AS H7_HDELV_TEXT4       " & vbNewLine _
                                       & "      , HED.H7_HDELV_TEXT5         AS H7_HDELV_TEXT5       " & vbNewLine _
                                       & "      , HED.RECORD_STATUS          AS H_RECORD_STATUS      " & vbNewLine _
                                       & "      , DTL.DEL_KB                 AS M_DEL_KB             " & vbNewLine _
                                       & "      , DTL.GYO                    AS GYO                  " & vbNewLine _
                                       & "      , DTL.EDI_CTL_NO_CHU         AS EDI_CTL_NO_CHU       " & vbNewLine _
                                       & "      , DTL.INKA_CTL_NO_M          AS INKA_CTL_NO_M        " & vbNewLine _
                                       & "      , DTL.OUTKA_CTL_NO_CHU       AS OUTKA_CTL_NO_CHU     " & vbNewLine _
                                       & "      , DTL.M1_IREC_TYP1           AS M1_IREC_TYP1         " & vbNewLine _
                                       & "      , DTL.M2_IREC_TYP2           AS M2_IREC_TYP2         " & vbNewLine _
                                       & "      , DTL.M3_IREC_TYP3           AS M3_IREC_TYP3         " & vbNewLine _
                                       & "      , DTL.M1_ICOM_CODE1          AS M1_ICOM_CODE1        " & vbNewLine _
                                       & "      , DTL.M1_IDEL_NO1            AS M1_IDEL_NO1          " & vbNewLine _
                                       & "      , DTL.M1_ISHIP_NO1           AS M1_ISHIP_NO1         " & vbNewLine _
                                       & "      , DTL.M1_IDEL_ITEM_NO        AS M1_IDEL_ITEM_NO      " & vbNewLine _
                                       & "      , DTL.M1_IPO_NO              AS M1_IPO_NO            " & vbNewLine _
                                       & "      , DTL.M1_IPO_ITEM_NO         AS M1_IPO_ITEM_NO       " & vbNewLine _
                                       & "      , DTL.M1_ILOT_NO             AS M1_ILOT_NO           " & vbNewLine _
                                       & "      , DTL.M1_ISTO_LOC            AS M1_ISTO_LOC          " & vbNewLine _
                                       & "      , DTL.M1_IMATERIAL_CD        AS M1_IMATERIAL_CD      " & vbNewLine _
                                       & "      , DTL.M1_IMATERIAL_NM1       AS M1_IMATERIAL_NM1     " & vbNewLine _
                                       & "      , DTL.M1_IMATERIAL_NM2       AS M1_IMATERIAL_NM2     " & vbNewLine _
                                       & "      , DTL.M1_IMATERIAL_NM3       AS M1_IMATERIAL_NM3     " & vbNewLine _
                                       & "      , DTL.M1_IQTY                AS M1_IQTY              " & vbNewLine _
                                       & "      , DTL.M1_IQTY_UNIT           AS M1_IQTY_UNIT         " & vbNewLine _
                                       & "      , DTL.M1_ISKU_QTY            AS M1_ISKU_QTY          " & vbNewLine _
                                       & "      , DTL.M1_ISKU_QTY_UNIT       AS M1_ISKU_QTY_UNIT     " & vbNewLine _
                                       & "      , DTL.M1_IPACKAGE1           AS M1_IPACKAGE1         " & vbNewLine _
                                       & "      , DTL.M1_IBASE_UNIT          AS M1_IBASE_UNIT        " & vbNewLine _
                                       & "      , DTL.M1_IPACKAGE2           AS M1_IPACKAGE2         " & vbNewLine _
                                       & "      , DTL.M2_ISHORT_TEXT         AS M2_ISHORT_TEXT       " & vbNewLine _
                                       & "      , DTL.M2_ICUSTOMER_NM        AS M2_ICUSTOMER_NM      " & vbNewLine _
                                       & "      , DTL.M2_IFINI_CD            AS M2_IFINI_CD          " & vbNewLine _
                                       & "      , DTL.M2_IFINI_NM            AS M2_IFINI_NM          " & vbNewLine _
                                       & "      , DTL.M2_IPRICE_PLAN_PRO     AS M2_IPRICE_PLAN_PRO   " & vbNewLine _
                                       & "      , DTL.M2_IPRICE_PRO          AS M2_IPRICE_PRO        " & vbNewLine _
                                       & "      , DTL.M3_ILOA_ITEM1          AS M3_ILOA_ITEM1        " & vbNewLine _
                                       & "      , DTL.M3_ILOA_ITEM2          AS M3_ILOA_ITEM2        " & vbNewLine _
                                       & "      , DTL.M3_ILOA_ITEM3          AS M3_ILOA_ITEM3        " & vbNewLine _
                                       & "      , DTL.M3_ILOA_ITEM4          AS M3_ILOA_ITEM4        " & vbNewLine _
                                       & "      , DTL.M3_ILOA_ITEM5          AS M3_ILOA_ITEM5        " & vbNewLine _
                                       & "      , DTL.RECORD_STATUS          AS M_RECORD_STATUS      " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' ダウケミEDI受信データHEAD - ダウケミEDI受信データDETAIL,商品Ｍ,区分Ｍ
    ''' </remarks>
    Private Const SQL_FROM As String = "  FROM $LM_TRN$..H_INOUTKAEDI_HED_DOW  HED                          " & vbNewLine _
                                     & " --【Notes】№1007/1008対応 --- START ---                           " & vbNewLine _
                                     & "      -- EDI印刷種別テーブル                                        " & vbNewLine _
                                     & "      LEFT JOIN (                                                   " & vbNewLine _
                                     & "                  SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT           " & vbNewLine _
                                     & "                       , H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                     & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                     & "                    FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT          " & vbNewLine _
                                     & "                   WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD       " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L       " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M       " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.PRINT_TP    = '02'             " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB        " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'              " & vbNewLine _
                                     & "                   GROUP BY                                         " & vbNewLine _
                                     & "                         H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                     & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                     & "                ) HEDIPRINT                                         " & vbNewLine _
                                     & "             ON HEDIPRINT.NRS_BR_CD  = HED.NRS_BR_CD                " & vbNewLine _
                                     & "            AND HEDIPRINT.EDI_CTL_NO = HED.EDI_CTL_NO               " & vbNewLine _
                                     & " --【Notes】№1007/1008対応 ---  END  ---                           " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_TRN$..H_INOUTKAEDI_DTL_DOW  DTL           " & vbNewLine _
                                     & "                   ON DTL.CRT_DATE  = HED.CRT_DATE                  " & vbNewLine _
                                     & "                  AND DTL.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
                                     & "                  AND DTL.REC_NO    = HED.REC_NO                    " & vbNewLine _
                                     & "      -- 荷主マスタ                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                       " & vbNewLine _
                                     & "                   ON M_CUST.NRS_BR_CD       = HED.NRS_BR_CD        " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_L       = HED.CUST_CD_L        " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_M       = HED.CUST_CD_M        " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_S       = '00'                 " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_SS      = '00'                 " & vbNewLine _
                                     & "                  AND M_CUST.SYS_DEL_FLG     = '0'                  " & vbNewLine _
                                     & "      -- 商品マスタ                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                     & "                   ON M_GOODS.NRS_BR_CD      = HED.NRS_BR_CD        " & vbNewLine _
                                     & "                  AND M_GOODS.GOODS_CD_NRS   = DTL.M1_IMATERIAL_CD  " & vbNewLine _
                                     & "      -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_DOWの荷主より取得)   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                     & "                   ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_L   = HED.CUST_CD_L        " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_M   = HED.CUST_CD_M        " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.PTN_ID      = '80'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                          " & vbNewLine _
                                     & "                   ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD " & vbNewLine _
                                     & "                  AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID    " & vbNewLine _
                                     & "                  AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD    " & vbNewLine _
                                     & "                  AND MR1.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                     & "      -- 帳票パターンマスタ②(商品マスタより)                       " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2               " & vbNewLine _
                                     & "                   ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD    " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L    " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M    " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.CUST_CD_S   = '00'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.PTN_ID      = '80'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                     & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                     & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                     & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                     & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                     & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                     & "                   ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND MR3.PTN_ID             = '80'                 " & vbNewLine _
                                     & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                     & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine


    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY            " & vbNewLine _
                                         & "       HED.CRT_DATE  " & vbNewLine _
                                         & "     , HED.FILE_NAME " & vbNewLine _
                                         & "     , HED.REC_NO    " & vbNewLine _
                                         & "     , DTL.GYO       " & vbNewLine


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
    ''' ゼロフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ZERO_FLG As String = "0"


#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''帳票パターンマスタ データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタデータ取得 SQLの構築・発行</remarks>
    Private Function SelectMPrintPattern(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH560IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH560DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMH560DAC.SQL_MPrt_FROM)      'SQL構築(帳票種別用FROM句)
        '(2012.03.19) WHERE条件を帳票取得時と同じにする -- START --
        'Me._StrSql.Append(LMH560DAC.SQL_MPrt_WHERE)     'SQL構築(帳票種別用WHERE句)
        'Call Me.SetConditionPrintPatternMSQL()          '条件設定
        If Me._Row.Item("PRTFLG").ToString = "1" Then    'Notes 1061 2012/05/15　開始
            Call Me.SetConditionMasterSQL_OUT()          '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()                 'SQL構築(印刷データ抽出条件設定) '未出力・両方(出力済、未出力併せて)
        End If                                          'Notes 1061 2012/05/15　終了
        'Call Me.SetConditionPrintPatternMSQL()          '条件設定 Notes1061
        '(2012.03.19) WHERE条件を帳票取得時と同じにする --  END  --

        ''追加(Notes_1007 2012/05/09)
        'Call Me.SetConditionPrintPatternMSQL()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH560DAC", "SelectMPrt", cmd)

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
    ''' ダウケミEDI受信データ(HEAD)・ダウケミEDI受信データ(DETAIL)対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ダウケミEDI受信データ(HEAD)・(DETAIL)対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH560IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH560DAC.SQL_SELECT)      'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMH560DAC.SQL_FROM)        'SQL構築(印刷データ抽出用 FROM句)
        If Me._Row.Item("PRTFLG").ToString = "1" Then 'Notes 1061 2012/05/15　開始
            Call Me.SetConditionMasterSQL_OUT()          '出力済の場合
            'Call Me.SetConditionPrintPatternMSQL()          '条件設定
        Else
            Call Me.SetConditionMasterSQL()              'SQL構築(印刷データ抽出条件設定) '未出力・両方(出力済、未出力併せて)
        End If                                       'Notes 1061 2012/05/15　終了
        Me._StrSql.Append(LMH560DAC.SQL_ORDER_BY)    'SQL構築(印刷データ抽出用 ORDER BY句)

        'Call Me.SetConditionPrintPatternMSQL()          '条件設定 Notes1061

        ''追加(Notes_1007 2012/05/09)
        'Call Me.SetConditionPrintPatternMSQL()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH560DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("H_DEL_KB", "H_DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_WH_CD", "NRS_WH_CD")
        map.Add("INOUT_KB", "INOUT_KB")
        map.Add("INOUT_KB_NM", "INOUT_KB_NM")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("PRTFLG", "PRTFLG")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("H1_HREC_TYP1", "H1_HREC_TYP1")
        map.Add("H2_HREC_TYP2", "H2_HREC_TYP2")
        map.Add("H3_HREC_TYP3", "H3_HREC_TYP3")
        map.Add("H4_HREC_TYP4", "H4_HREC_TYP4")
        map.Add("H5_HREC_TYP5", "H5_HREC_TYP5")
        map.Add("H6_HREC_TYP6", "H6_HREC_TYP6")
        map.Add("H7_HREC_TYP7", "H7_HREC_TYP7")
        map.Add("H1_HCOM_CODE1", "H1_HCOM_CODE1")
        map.Add("H1_HDEL_NO1", "H1_HDEL_NO1")
        map.Add("H1_HSHIP_NO1", "H1_HSHIP_NO1")
        map.Add("H1_HORD_STATUS", "H1_HORD_STATUS")
        map.Add("H1_HORD_STATUS_NM", "H1_HORD_STATUS_NM")
        map.Add("H1_HCOM_NAME", "H1_HCOM_NAME")
        map.Add("H1_HORD_TYP", "H1_HORD_TYP")
        map.Add("H1_HORD_TYP_NM", "H1_HORD_TYP_NM")
        map.Add("H1_HSYS_DATE", "H1_HSYS_DATE")
        map.Add("H1_HSYS_TIME", "H1_HSYS_TIME")
        map.Add("H1_HDEL_TERM", "H1_HDEL_TERM")
        map.Add("H1_HDEL_PLANT_CD", "H1_HDEL_PLANT_CD")
        map.Add("H1_HDEL_PLANT_NM", "H1_HDEL_PLANT_NM")
        map.Add("H1_HDEL_PLANT_CD1", "H1_HDEL_PLANT_CD1")
        map.Add("H1_HSEL_PLANT_CD", "H1_HSEL_PLANT_CD")
        map.Add("H1_HSEL_PLANT_NM", "H1_HSEL_PLANT_NM")
        map.Add("H1_HREC_WH_CD", "H1_HREC_WH_CD")
        map.Add("H1_HREC_WH_NM", "H1_HREC_WH_NM")
        map.Add("H1_HCON_CD", "H1_HCON_CD")
        map.Add("H1_HCON_NM1", "H1_HCON_NM1")
        map.Add("H1_HCON_NM2", "H1_HCON_NM2")
        map.Add("H1_HCON_PHONE_NO", "H1_HCON_PHONE_NO")
        map.Add("H1_HPAR_NO", "H1_HPAR_NO")
        map.Add("H1_HREQ_FLAG", "H1_HREQ_FLAG")
        map.Add("H1_HREQ_FLAG_NM", "H1_HREQ_FLAG_NM")
        map.Add("H1_HEXPORT_MARK", "H1_HEXPORT_MARK")
        map.Add("H1_HORD_NO", "H1_HORD_NO")
        map.Add("H2_HDEL_DATE", "H2_HDEL_DATE")
        map.Add("H2_HSHIP_DATE", "H2_HSHIP_DATE")
        map.Add("H2_HCON_NM3", "H2_HCON_NM3")
        map.Add("H2_HCON_NM4", "H2_HCON_NM4")
        map.Add("H2_HCON_ADD1", "H2_HCON_ADD1")
        map.Add("H2_HCON_ADD2", "H2_HCON_ADD2")
        map.Add("H2_HCON_CITY", "H2_HCON_CITY")
        map.Add("H2_HCON_ADD3", "H2_HCON_ADD3")
        map.Add("H2_HCON_ADD4", "H2_HCON_ADD4")
        map.Add("H2_HCON_POST_CD", "H2_HCON_POST_CD")
        map.Add("H2_HCON_POST_AREA", "H2_HCON_POST_AREA")
        map.Add("H2_HCON_LOCATION", "H2_HCON_LOCATION")
        map.Add("H3_HCARR_CD", "H3_HCARR_CD")
        map.Add("H3_HCARR_NM", "H3_HCARR_NM")
        map.Add("H3_HDAIKAN", "H3_HDAIKAN")
        map.Add("H3_HTEXT1", "H3_HTEXT1")
        map.Add("H3_HTEXT2", "H3_HTEXT2")
        map.Add("H3_HOLD_CARR_CD", "H3_HOLD_CARR_CD")
        map.Add("H3_HFILE_VERSION", "H3_HFILE_VERSION")
        map.Add("H3_HSHIP_NM_KAN", "H3_HSHIP_NM_KAN")
        map.Add("H3_HCARR_NM_KAN", "H3_HCARR_NM_KAN")
        map.Add("H4_HCON_NM_KAN", "H4_HCON_NM_KAN")
        map.Add("H4_HCON_ADD_KAN", "H4_HCON_ADD_KAN")
        map.Add("H4_HCON_STR1_KAN", "H4_HCON_STR1_KAN")
        map.Add("H4_HCON_STR2_KAN", "H4_HCON_STR2_KAN")
        map.Add("H4_HCON_PHONE_NO1", "H4_HCON_PHONE_NO1")
        map.Add("H4_HCON_ADDRESS_KAN", "H4_HCON_ADDRESS_KAN")
        map.Add("H5_HCUST_NO", "H5_HCUST_NO")
        map.Add("H5_HSOLD_CD", "H5_HSOLD_CD")
        map.Add("H5_HSOLD_NM", "H5_HSOLD_NM")
        map.Add("H5_HCSR_NO", "H5_HCSR_NO")
        map.Add("H5_HCSR_NM", "H5_HCSR_NM")
        map.Add("H5_HSOLD_NM_KAN", "H5_HSOLD_NM_KAN")
        map.Add("H5_HOTHER_NM1_KAN", "H5_HOTHER_NM1_KAN")
        map.Add("H5_HOTEHR_NM2_KAN", "H5_HOTEHR_NM2_KAN")
        map.Add("H6_HCARR_TEXT1", "H6_HCARR_TEXT1")
        map.Add("H6_HCARR_TEXT2", "H6_HCARR_TEXT2")
        map.Add("H6_HCARR_TEXT3", "H6_HCARR_TEXT3")
        map.Add("H6_HCARR_TEXT4", "H6_HCARR_TEXT4")
        map.Add("H6_HCARR_TEXT5", "H6_HCARR_TEXT5")
        map.Add("H7_HDELV_TEXT1", "H7_HDELV_TEXT1")
        map.Add("H7_HDELV_TEXT2", "H7_HDELV_TEXT2")
        map.Add("H7_HDELV_TEXT3", "H7_HDELV_TEXT3")
        map.Add("H7_HDELV_TEXT4", "H7_HDELV_TEXT4")
        map.Add("H7_HDELV_TEXT5", "H7_HDELV_TEXT5")
        map.Add("H_RECORD_STATUS", "H_RECORD_STATUS")
        map.Add("M_DEL_KB", "M_DEL_KB")
        map.Add("GYO", "GYO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("INKA_CTL_NO_M", "INKA_CTL_NO_M")
        map.Add("OUTKA_CTL_NO_CHU", "OUTKA_CTL_NO_CHU")
        map.Add("M1_IREC_TYP1", "M1_IREC_TYP1")
        map.Add("M2_IREC_TYP2", "M2_IREC_TYP2")
        map.Add("M3_IREC_TYP3", "M3_IREC_TYP3")
        map.Add("M1_ICOM_CODE1", "M1_ICOM_CODE1")
        map.Add("M1_IDEL_NO1", "M1_IDEL_NO1")
        map.Add("M1_ISHIP_NO1", "M1_ISHIP_NO1")
        map.Add("M1_IDEL_ITEM_NO", "M1_IDEL_ITEM_NO")
        map.Add("M1_IPO_NO", "M1_IPO_NO")
        map.Add("M1_IPO_ITEM_NO", "M1_IPO_ITEM_NO")
        map.Add("M1_ILOT_NO", "M1_ILOT_NO")
        map.Add("M1_ISTO_LOC", "M1_ISTO_LOC")
        map.Add("M1_IMATERIAL_CD", "M1_IMATERIAL_CD")
        map.Add("M1_IMATERIAL_NM1", "M1_IMATERIAL_NM1")
        map.Add("M1_IMATERIAL_NM2", "M1_IMATERIAL_NM2")
        map.Add("M1_IMATERIAL_NM3", "M1_IMATERIAL_NM3")
        map.Add("M1_IQTY", "M1_IQTY")
        map.Add("M1_IQTY_UNIT", "M1_IQTY_UNIT")
        map.Add("M1_ISKU_QTY", "M1_ISKU_QTY")
        map.Add("M1_ISKU_QTY_UNIT", "M1_ISKU_QTY_UNIT")
        map.Add("M1_IPACKAGE1", "M1_IPACKAGE1")
        map.Add("M1_IBASE_UNIT", "M1_IBASE_UNIT")
        map.Add("M1_IPACKAGE2", "M1_IPACKAGE2")
        map.Add("M2_ISHORT_TEXT", "M2_ISHORT_TEXT")
        map.Add("M2_ICUSTOMER_NM", "M2_ICUSTOMER_NM")
        map.Add("M2_IFINI_CD", "M2_IFINI_CD")
        map.Add("M2_IFINI_NM", "M2_IFINI_NM")
        map.Add("M2_IPRICE_PLAN_PRO", "M2_IPRICE_PLAN_PRO")
        map.Add("M2_IPRICE_PRO", "M2_IPRICE_PRO")
        map.Add("M3_ILOA_ITEM1", "M3_ILOA_ITEM1")
        map.Add("M3_ILOA_ITEM2", "M3_ILOA_ITEM2")
        map.Add("M3_ILOA_ITEM3", "M3_ILOA_ITEM3")
        map.Add("M3_ILOA_ITEM4", "M3_ILOA_ITEM4")
        map.Add("M3_ILOA_ITEM5", "M3_ILOA_ITEM5")
        map.Add("M_RECORD_STATUS", "M_RECORD_STATUS")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH560OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンＭ取得 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrintPatternMSQL()

        'SQLパラメータ初期化
        'Me._SqlPrmList = New ArrayList() 'notes1061

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        'パラメータ設定
        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))

            '入出荷区分(Notes1007 2012/05/09)
            whereStr = .Item("INOUT_KB").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" HED.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '倉庫コード
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.NRS_WH_CD = @WH_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine) 'Notes 1061
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR)) 'Notes 1061
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine) 'Notes 1061
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR)) 'Notes 1061
            End If

            'EDI取込日(FROM)
            whereStr = .Item("CRT_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.CRT_DATE >= @CRT_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'EDI取込日(TO)
            whereStr = .Item("CRT_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.CRT_DATE <= @CRT_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '入出荷区分
            whereStr = .Item("INOUT_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.INOUT_KB = @INOUT_KB")
                Me._StrSql.Append(vbNewLine) 'Notes1007 2012/05/09(スカラ変数の定義がぶつかるためコメントアウト)'Notes 1061
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR)) 'Notes1007 2012/05/09(スカラ変数の定義がぶつかるためコメントアウト)'Notes 1061
            End If

            '(2012.05.09) Notes№1007/1008 未出力/出力済の判断をHEDIPRINTのレコード有無で行う --- START ---
            'プリントフラグ
            'whereStr = .Item("PRTFLG").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.PRTFLG = @PRTFLG")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))
            'End If

            whereStr = .Item("PRTFLG").ToString()
            Select Case whereStr
                Case "0"
                    '未出力
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT  = 0 OR HEDIPRINT.PRT_COUNT IS NULL) ")
                Case "1"
                    '出力済
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT >= 1 ) ")
            End Select
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))
            '(2012.05.09) Notes№1007/1008 未出力/出力済の判断をHEDIPRINTのレコード有無で行う ---  END  ---

        End With

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール(出力済 Notes 1061 2012/05/15 新設)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_OUT()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" HED.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.CUST_CD_L = @CUST_CD_L")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            'End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.CUST_CD_M = @CUST_CD_M")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            'End If

            '入出荷区分
            whereStr = .Item("INOUT_KB").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.INOUT_KB = @INOUT_KB")
            Me._StrSql.Append(vbNewLine) 'Notes1007 2012/05/09(スカラ変数の定義がぶつかるためコメントアウト)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR)) 'Notes1007 2012/05/09(スカラ変数の定義がぶつかるためコメントアウト)
            'End If

            'EDI出荷管理番号
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.EDI_CTL_NO = @EDI_CTL_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", whereStr, DBDataType.CHAR))
            End If



            '未出力/出力済の判断をHEDIPRINTのレコード有無で行う --- START ---
            'プリントフラグ
            whereStr = .Item("PRTFLG").ToString()
            Select Case whereStr
                Case "0"
                    '未出力
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT  = 0 OR HEDIPRINT.PRT_COUNT IS NULL) ")
                Case "1"
                    '出力済
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT >= 1 ) ")
            End Select
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))
            '未出力/出力済の判断をHEDIPRINTのレコード有無で行う ---  END  ---

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
