' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC601    : 出荷取消連絡票(中削除)印刷
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC601DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC601DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                           " & vbNewLine _
                                            & "	OUTL.NRS_BR_CD                                           AS NRS_BR_CD    " & vbNewLine _
                                            & ",'17'                                                     AS PTN_ID       " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                         " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                        " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD       " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                         " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                    " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID       " & vbNewLine


    '''' <summary>
    '''' 印刷データ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'START YANAI 要望番号650
    'Private Const SQL_SELECT_DATA As String = "SELECT                                                                                                               " & vbNewLine _
    '                                        & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                    " & vbNewLine _
    '                                        & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                    " & vbNewLine _
    '                                        & "      ELSE MR3.RPT_ID                                                                                                " & vbNewLine _
    '                                        & " END              AS RPT_ID                                                                                          " & vbNewLine _
    '                                        & ",OUTL.NRS_BR_CD   AS NRS_BR_CD                                                                                       " & vbNewLine _
    '                                        & ",OUTM.PRINT_SORT  AS PRINT_SORT                                                                                      " & vbNewLine _
    '                                        & ",OUTL.OUTKA_NO_L  AS OUTKA_NO_L                                                                                      " & vbNewLine _
    '                                        & ",OUTL.DEST_CD     AS DEST_CD                                                                                         " & vbNewLine _
    '                                        & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                                                               " & vbNewLine _
    '                                        & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                                                     " & vbNewLine _
    '                                        & "      ELSE MDOUT.DEST_NM                                                                                                       " & vbNewLine _
    '                                        & " END              AS DEST_NM                                                                                                   " & vbNewLine _
    '                                        & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                                                             " & vbNewLine _
    '                                        & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                                                   " & vbNewLine _
    '                                        & "      ELSE MDOUT.AD_1                                                                                                          " & vbNewLine _
    '                                        & " END              AS DEST_AD_1                                                                                                 " & vbNewLine _
    '                                        & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                                                             " & vbNewLine _
    '                                        & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                                                   " & vbNewLine _
    '                                        & "      ELSE MDOUT.AD_2                                                                                                          " & vbNewLine _
    '                                        & " END              AS DEST_AD_2                                                                                                 " & vbNewLine _
    '                                        & ",OUTL.DEST_AD_3   AS DEST_AD_3                                                                                       " & vbNewLine _
    '                                        & ",OUTL.DEST_TEL    AS DEST_TEL                                                                                        " & vbNewLine _
    '                                        & ",OUTL.CUST_CD_L     AS CUST_CD_L                                                                                     " & vbNewLine _
    '                                        & ",MC.CUST_NM_L     AS CUST_NM_L                                                                                       " & vbNewLine _
    '                                        & ",MC.CUST_NM_M     AS CUST_NM_M                                                                                       " & vbNewLine _
    '                                        & ",OUTL.CUST_ORD_NO    AS CUST_ORD_NO                                                                                  " & vbNewLine _
    '                                        & ",OUTL.BUYER_ORD_NO   AS BUYER_ORD_NO                                                                                 " & vbNewLine _
    '                                        & ",OUTL.OUTKO_DATE     AS OUTKO_DATE                                                                                   " & vbNewLine _
    '                                        & ",OUTL.ARR_PLAN_DATE     AS ARR_PLAN_DATE                                                                             " & vbNewLine _
    '                                        & ",KBN1.KBN_NM1    AS ARR_PLAN_TIME                                                                                    " & vbNewLine _
    '                                        & ",MUCO.UNSOCO_NM  AS UNSOCO_NM                                                                                        " & vbNewLine _
    '                                        & ",KBN2.KBN_NM1    AS PC_KB                                                                                            " & vbNewLine _
    '                                        & ",MDOUTU.DEST_NM  AS URIG_NM                                                                                          " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 1),'') AS SAGYO_REC_NO_1                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 1),'') AS SAGYO_CD_1                                                                               " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 1),'') AS SAGYO_NM_1                                                                               " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 2),'') AS SAGYO_REC_NO_2                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 2),'') AS SAGYO_CD_2                                                                               " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 2),'') AS SAGYO_NM_2                                                                               " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 3),'') AS SAGYO_REC_NO_3                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 3),'') AS SAGYO_CD_3                                                                               " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 3),'') AS SAGYO_NM_3                                                                               " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 4),'') AS SAGYO_REC_NO_4                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 4),'') AS SAGYO_CD_4                                                                               " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 4),'') AS SAGYO_NM_4                                                                               " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 5),'') AS SAGYO_REC_NO_5                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 5),'') AS SAGYO_CD_5                                                                               " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + '000') AND E.IOZS_KB = '20'             " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 5),'') AS SAGYO_NM_5                                                                               " & vbNewLine _
    '                                        & ",MUSER.USER_NM     AS CRT_USER                                                                                       " & vbNewLine _
    '                                        & ",OUTM.OUTKA_NO_M   AS OUTKA_NO_M                                                                                     " & vbNewLine _
    '                                        & ",MG.GOODS_NM_1     AS GOODS_NM                                                                                       " & vbNewLine _
    '                                        & ",MG.GOODS_CD_CUST     AS GOODS_CD_CUST                                                                               " & vbNewLine _
    '                                        & ",OUTS.IRIME        AS IRIME                                                                                          " & vbNewLine _
    '                                        & ",MG.STD_IRIME_UT   AS IRIME_UT                                                                                       " & vbNewLine _
    '                                        & ",OUTS.ALCTD_NB / MG.PKG_NB     AS KONSU                                                                              " & vbNewLine _
    '                                        & ",OUTS.ALCTD_NB % MG.PKG_NB     AS HASU                                                                               " & vbNewLine _
    '                                        & ",OUTS.ALCTD_NB     AS ALCTD_NB                                                                                       " & vbNewLine _
    '                                        & ",OUTS.ALCTD_QT     AS ALCTD_QT                                                                                       " & vbNewLine _
    '                                        & ",OUTS.ALCTD_CAN_NB / MG.PKG_NB AS ZAN_KONSU                                                     " & vbNewLine _
    '                                        & ",OUTS.ALCTD_CAN_NB % MG.PKG_NB AS ZAN_HASU                                                      " & vbNewLine _
    '                                        & ",OUTS.SERIAL_NO    AS SERIAL_NO                                                                                      " & vbNewLine _
    '                                        & ",MG.PKG_NB         AS PKG_NB                                                                                         " & vbNewLine _
    '                                        & ",MG.PKG_UT         AS PKG_UT                                                                                         " & vbNewLine _
    '                                        & ",MG.ALCTD_KB       AS ALCTD_KB                                                                                       " & vbNewLine _
    '                                        & ",OUTS.ALCTD_CAN_QT AS ALCTD_CAN_QT                                                                                   " & vbNewLine _
    '                                        & ",OUTS.LOT_NO       AS LOT_NO                                                                                         " & vbNewLine _
    '                                        & ",INS.LT_DATE       AS LT_DATE                                                                                        " & vbNewLine _
    '                                        & ",ZAI.INKO_DATE     AS INKO_DATE                                                                                      " & vbNewLine _
    '                                        & ",OUTS.REMARK       AS REMARK_S                                                                                       " & vbNewLine _
    '                                        & ",KBN3.KBN_NM1      AS GOODS_COND_NM_1                                                                                " & vbNewLine _
    '                                        & ",KBN4.KBN_NM1      AS GOODS_COND_NM_2                                                                                " & vbNewLine _
    '                                        & ",OUTS.TOU_NO       AS TOU_NO                                                                                         " & vbNewLine _
    '                                        & ",OUTS.SITU_NO      AS SITU_NO                                                                                        " & vbNewLine _
    '                                        & ",OUTS.ZONE_CD      AS ZONE_CD                                                                                        " & vbNewLine _
    '                                        & ",OUTS.LOCA         AS LOCA                                                                                           " & vbNewLine _
    '                                        & ",OUTM.REMARK       AS REMARK_MEI                                                                                     " & vbNewLine _
    '                                        & ",OUTM.SYS_DEL_FLG  AS SYS_DEL_FLG                                                                                    " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 1),'') AS SAGYO_MEI_REC_NO_1                                                                       " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 1),'') AS SAGYO_MEI_CD_1                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 1),'') AS SAGYO_MEI_NM_1                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 2),'') AS SAGYO_MEI_REC_NO_2                                                                       " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 2),'') AS SAGYO_MEI_CD_2                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 2),'') AS SAGYO_MEI_NM_2                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 3),'') AS SAGYO_MEI_REC_NO_3                                                                       " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 3),'') AS SAGYO_MEI_CD_3                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 3),'') AS SAGYO_MEI_NM_3                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 4),'') AS SAGYO_MEI_REC_NO_4                                                                       " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 4),'') AS SAGYO_MEI_CD_4                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 4),'') AS SAGYO_MEI_NM_4                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_REC_NO                                                                                    " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_REC_NO AS SAGYO_REC_NO                                                                                         " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 5),'') AS SAGYO_MEI_REC_NO_5                                                                       " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_CD                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_CD AS SAGYO_CD                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 5),'') AS SAGYO_MEI_CD_5                                                                           " & vbNewLine _
    '                                        & ",ISNULL((SELECT BASE.SAGYO_NM                                                                                        " & vbNewLine _
    '                                        & " FROM                                                                                                                " & vbNewLine _
    '                                        & "(SELECT                                                                                                              " & vbNewLine _
    '                                        & "SAGYO_NM AS SAGYO_NM                                                                                                 " & vbNewLine _
    '                                        & "               ,ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                         " & vbNewLine _
    '                                        & " FROM $LM_TRN$..E_SAGYO E                                                                                              " & vbNewLine _
    '                                        & " WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'   " & vbNewLine _
    '                                        & " ) AS BASE                                                                                                           " & vbNewLine _
    '                                        & " WHERE BASE.NUM = 5),'') AS SAGYO_MEI_NM_5                                                                           " & vbNewLine
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                                                               " & vbNewLine _
                                        & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                    " & vbNewLine _
                                        & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                    " & vbNewLine _
                                        & "      ELSE MR3.RPT_ID                                                                                                " & vbNewLine _
                                        & " END              AS RPT_ID                                                                                          " & vbNewLine _
                                        & ",OUTL.NRS_BR_CD   AS NRS_BR_CD                                                                                       " & vbNewLine _
                                        & ",OUTM.PRINT_SORT  AS PRINT_SORT                                                                                      " & vbNewLine _
                                        & ",OUTL.OUTKA_NO_L  AS OUTKA_NO_L                                                                                      " & vbNewLine _
                                        & ",OUTL.DEST_CD     AS DEST_CD                                                                                         " & vbNewLine _
                                        & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                                                               " & vbNewLine _
                                        & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                                                     " & vbNewLine _
                                        & "      ELSE MDOUT.DEST_NM                                                                                                       " & vbNewLine _
                                        & " END              AS DEST_NM                                                                                                   " & vbNewLine _
                                        & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                                                             " & vbNewLine _
                                        & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                                                   " & vbNewLine _
                                        & "      ELSE MDOUT.AD_1                                                                                                          " & vbNewLine _
                                        & " END              AS DEST_AD_1                                                                                                 " & vbNewLine _
                                        & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                                                             " & vbNewLine _
                                        & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                                                   " & vbNewLine _
                                        & "      ELSE MDOUT.AD_2                                                                                                          " & vbNewLine _
                                        & " END              AS DEST_AD_2                                                                                                 " & vbNewLine _
                                        & ",OUTL.DEST_AD_3   AS DEST_AD_3                                                                                       " & vbNewLine _
                                        & ",OUTL.DEST_TEL    AS DEST_TEL                                                                                        " & vbNewLine _
                                        & ",OUTL.CUST_CD_L     AS CUST_CD_L                                                                                     " & vbNewLine _
                                        & ",MC.CUST_NM_L     AS CUST_NM_L                                                                                       " & vbNewLine _
                                        & ",MC.CUST_NM_M     AS CUST_NM_M                                                                                       " & vbNewLine _
                                        & ",OUTL.CUST_ORD_NO    AS CUST_ORD_NO                                                                                  " & vbNewLine _
                                        & ",OUTL.BUYER_ORD_NO   AS BUYER_ORD_NO                                                                                 " & vbNewLine _
                                        & ",OUTL.OUTKO_DATE     AS OUTKO_DATE                                                                                   " & vbNewLine _
                                        & ",OUTL.ARR_PLAN_DATE     AS ARR_PLAN_DATE                                                                             " & vbNewLine _
                                        & ",KBN1.KBN_NM1    AS ARR_PLAN_TIME                                                                                    " & vbNewLine _
                                        & ",MUCO.UNSOCO_NM  AS UNSOCO_NM                                                                                        " & vbNewLine _
                                        & ",KBN2.KBN_NM1    AS PC_KB                                                                                            " & vbNewLine _
                                        & ",MDOUTU.DEST_NM  AS URIG_NM                                                                                          " & vbNewLine _
                                        & ",MUSER.USER_NM     AS CRT_USER                                                                                       " & vbNewLine _
                                        & ",OUTM.OUTKA_NO_M   AS OUTKA_NO_M                                                                                     " & vbNewLine _
                                        & ",MG.GOODS_NM_1     AS GOODS_NM                                                                                       " & vbNewLine _
                                        & ",MG.GOODS_CD_CUST     AS GOODS_CD_CUST                                                                               " & vbNewLine _
                                        & ",OUTS.IRIME        AS IRIME                                                                                          " & vbNewLine _
                                        & ",MG.STD_IRIME_UT   AS IRIME_UT                                                                                       " & vbNewLine _
                                        & ",OUTS.ALCTD_NB / MG.PKG_NB     AS KONSU                                                                              " & vbNewLine _
                                        & ",OUTS.ALCTD_NB % MG.PKG_NB     AS HASU                                                                               " & vbNewLine _
                                        & ",OUTS.ALCTD_NB     AS ALCTD_NB                                                                                       " & vbNewLine _
                                        & ",OUTS.ALCTD_QT     AS ALCTD_QT                                                                                       " & vbNewLine _
                                        & ",OUTS.ALCTD_CAN_NB / MG.PKG_NB AS ZAN_KONSU                                                     " & vbNewLine _
                                        & ",OUTS.ALCTD_CAN_NB % MG.PKG_NB AS ZAN_HASU                                                      " & vbNewLine _
                                        & ",OUTS.SERIAL_NO    AS SERIAL_NO                                                                                      " & vbNewLine _
                                        & ",MG.PKG_NB         AS PKG_NB                                                                                         " & vbNewLine _
                                        & ",MG.PKG_UT         AS PKG_UT                                                                                         " & vbNewLine _
                                        & ",MG.ALCTD_KB       AS ALCTD_KB                                                                                       " & vbNewLine _
                                        & ",OUTS.ALCTD_CAN_QT AS ALCTD_CAN_QT                                                                                   " & vbNewLine _
                                        & ",OUTS.LOT_NO       AS LOT_NO                                                                                         " & vbNewLine _
                                        & ",INS.LT_DATE       AS LT_DATE                                                                                        " & vbNewLine _
                                        & ",ZAI.INKO_DATE     AS INKO_DATE                                                                                      " & vbNewLine _
                                        & ",OUTS.REMARK       AS REMARK_S                                                                                       " & vbNewLine _
                                        & ",KBN3.KBN_NM1      AS GOODS_COND_NM_1                                                                                " & vbNewLine _
                                        & ",KBN4.KBN_NM1      AS GOODS_COND_NM_2                                                                                " & vbNewLine _
                                        & ",OUTS.TOU_NO       AS TOU_NO                                                                                         " & vbNewLine _
                                        & ",OUTS.SITU_NO      AS SITU_NO                                                                                        " & vbNewLine _
                                        & ",RTRIM(OUTS.ZONE_CD)     AS ZONE_CD                                                                                        " & vbNewLine _
                                        & ",OUTS.LOCA         AS LOCA                                                                                           " & vbNewLine _
                                        & ",OUTM.REMARK       AS REMARK_MEI                                                                                     " & vbNewLine _
                                        & ",OUTM.SYS_DEL_FLG  AS SYS_DEL_FLG                                                                                    " & vbNewLine
    'END YANAI 要望番号650


    'START YANAI 要望番号650
    '''' <summary>
    '''' データ抽出用FROM句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM As String = "--出荷L                                                                      " & vbNewLine _
    '                                 & "FROM                                                                         " & vbNewLine _
    '                                 & "$LM_TRN$..C_OUTKA_L OUTL                                                       " & vbNewLine _
    '                                 & "--トランザクションテーブル                                                   " & vbNewLine _
    '                                 & "--出荷M                                                                      " & vbNewLine _
    '                                 & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                             " & vbNewLine _
    '                                 & "ON  OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
    '                                 & "AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                        " & vbNewLine _
    '                                 & "--出荷S                                                                      " & vbNewLine _
    '                                 & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                             " & vbNewLine _
    '                                 & "ON  OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
    '                                 & "AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                        " & vbNewLine _
    '                                 & "AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                        " & vbNewLine _
    '                                 & "--出荷EDIL                                                                   " & vbNewLine _
    '                                 & "LEFT JOIN                                                                    " & vbNewLine _
    '                                 & "(                                                                            " & vbNewLine _
    '                                 & "SELECT DISTINCT                                                              " & vbNewLine _
    '                                 & " NRS_BR_CD                                                                   " & vbNewLine _
    '                                 & ",OUTKA_CTL_NO                                                                " & vbNewLine _
    '                                 & ",CUST_CD_L                                                                   " & vbNewLine _
    '                                 & ",SHIP_NM_L                                                                   " & vbNewLine _
    '                                 & ",MIN(DEST_CD)     AS DEST_CD                                                                     " & vbNewLine _
    '                                 & ",MIN(DEST_NM)     AS DEST_NM                                                                     " & vbNewLine _
    '                                 & ",MIN(DEST_AD_1)   AS DEST_AD_1                                                                   " & vbNewLine _
    '                                 & ",MIN(DEST_AD_2)   AS DEST_AD_2                                                                   " & vbNewLine _
    '                                 & ",DEST_AD_3                                                                   " & vbNewLine _
    '                                 & ",DEST_JIS_CD                                                                 " & vbNewLine _
    '                                 & ",DEST_TEL                                                                    " & vbNewLine _
    '                                 & ",FREE_C03                                                                    " & vbNewLine _
    '                                 & ",SYS_DEL_FLG                                                                 " & vbNewLine _
    '                                 & "FROM                                                                         " & vbNewLine _
    '                                 & "$LM_TRN$..H_OUTKAEDI_L                                                         " & vbNewLine _
    '                                 & "GROUP BY                                                                     " & vbNewLine _
    '                                 & " NRS_BR_CD                                                                   " & vbNewLine _
    '                                 & ",OUTKA_CTL_NO                                                                " & vbNewLine _
    '                                 & ",CUST_CD_L                                                                   " & vbNewLine _
    '                                 & ",SHIP_NM_L                                                                   " & vbNewLine _
    '                                 & ",DEST_AD_3                                                                   " & vbNewLine _
    '                                 & ",DEST_JIS_CD                                                                 " & vbNewLine _
    '                                 & ",DEST_TEL                                                                    " & vbNewLine _
    '                                 & ",FREE_C03                                                                    " & vbNewLine _
    '                                 & ",SYS_DEL_FLG                                                                 " & vbNewLine _
    '                                 & ") EDIL                                                                       " & vbNewLine _
    '                                 & "ON  EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
    '                                 & "AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                      " & vbNewLine _
    '                                 & "--入荷L                                                                      " & vbNewLine _
    '                                 & "LEFT JOIN $LM_TRN$..B_INKA_L INL                                               " & vbNewLine _
    '                                 & "ON  INL.NRS_BR_CD = OUTS.NRS_BR_CD                                           " & vbNewLine _
    '                                 & "AND INL.INKA_NO_L = OUTS.INKA_NO_L                                           " & vbNewLine _
    '                                 & "--入荷S                                                                      " & vbNewLine _
    '                                 & "LEFT JOIN $LM_TRN$..B_INKA_S INS                                               " & vbNewLine _
    '                                 & "ON  INS.NRS_BR_CD = OUTS.NRS_BR_CD                                           " & vbNewLine _
    '                                 & "AND INS.INKA_NO_L = OUTS.INKA_NO_L                                           " & vbNewLine _
    '                                 & "AND INS.INKA_NO_M = OUTS.INKA_NO_M                                           " & vbNewLine _
    '                                 & "AND INS.INKA_NO_S = OUTS.INKA_NO_S                                           " & vbNewLine _
    '                                 & "--運送L                                                                      " & vbNewLine _
    '                                 & "LEFT JOIN $LM_TRN$..F_UNSO_L UL                                                " & vbNewLine _
    '                                 & "ON  UL.NRS_BR_CD = OUTL.NRS_BR_CD                                            " & vbNewLine _
    '                                 & "AND UL.INOUTKA_NO_L = OUTL.OUTKA_NO_L                                        " & vbNewLine _
    '                                 & "AND UL.MOTO_DATA_KB = '20'                                                   " & vbNewLine _
    '                                 & "--マスタテーブル                                                             " & vbNewLine _
    '                                 & "--商品M                                                                      " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_GOODS MG                                                 " & vbNewLine _
    '                                 & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                            " & vbNewLine _
    '                                 & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                      " & vbNewLine _
    '                                 & "AND MG.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
    '                                 & "--荷主M(商品M経由)                                                           " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_CUST MC                                                  " & vbNewLine _
    '                                 & "ON  MC.NRS_BR_CD = MG.NRS_BR_CD                                              " & vbNewLine _
    '                                 & "AND MC.CUST_CD_L = MG.CUST_CD_L                                              " & vbNewLine _
    '                                 & "AND MC.CUST_CD_M = MG.CUST_CD_M                                              " & vbNewLine _
    '                                 & "AND MC.CUST_CD_S = MG.CUST_CD_S                                              " & vbNewLine _
    '                                 & "AND MC.CUST_CD_SS = MG.CUST_CD_SS                                            " & vbNewLine _
    '                                 & "AND MC.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
    '                                 & "--届先M(届先取得)(出荷L参照)                                                 " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_DEST MDOUT                                               " & vbNewLine _
    '                                 & "ON  MDOUT.NRS_BR_CD = OUTL.NRS_BR_CD                                         " & vbNewLine _
    '                                 & "AND MDOUT.CUST_CD_L = OUTL.CUST_CD_L                                         " & vbNewLine _
    '                                 & "AND MDOUT.DEST_CD = OUTL.DEST_CD                                             " & vbNewLine _
    '                                 & "AND MDOUT.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
    '                                 & "--届先M(売上先取得)(出荷L参照)                                               " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_DEST MDOUTU                                              " & vbNewLine _
    '                                 & "ON  MDOUTU.NRS_BR_CD = OUTL.NRS_BR_CD                                        " & vbNewLine _
    '                                 & "AND MDOUTU.CUST_CD_L = OUTL.CUST_CD_L                                        " & vbNewLine _
    '                                 & "AND MDOUTU.DEST_CD = OUTL.SHIP_CD_L                                          " & vbNewLine _
    '                                 & "AND MDOUTU.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
    '                                 & "--運送会社M                                                                  " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_UNSOCO MUCO                                              " & vbNewLine _
    '                                 & "ON  MUCO.NRS_BR_CD = UL.NRS_BR_CD                                            " & vbNewLine _
    '                                 & "AND MUCO.UNSOCO_CD = UL.UNSO_CD                                              " & vbNewLine _
    '                                 & "AND MUCO.UNSOCO_BR_CD = UL.UNSO_BR_CD                                        " & vbNewLine _
    '                                 & "AND MUCO.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
    '                                 & "--倉庫M                                                                      " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_SOKO MSO                                                 " & vbNewLine _
    '                                 & "ON  MSO.WH_CD = OUTL.WH_CD                                                   " & vbNewLine _
    '                                 & "AND MSO.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
    '                                 & "--ユーザM                                                                    " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..S_USER MUSER                                               " & vbNewLine _
    '                                 & "ON MUSER.USER_CD = OUTM.SYS_UPD_USER                                         " & vbNewLine _
    '                                 & "AND MUSER.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
    '                                 & "--区分M(納入予定区分)                                                        " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..Z_KBN KBN1                                                 " & vbNewLine _
    '                                 & "ON  KBN1.KBN_GROUP_CD = 'N010'                                               " & vbNewLine _
    '                                 & "AND KBN1.KBN_CD = OUTL.ARR_PLAN_TIME                                         " & vbNewLine _
    '                                 & "AND KBN1.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
    '                                 & "--区分M(元着払区分)                                                          " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..Z_KBN KBN2                                                 " & vbNewLine _
    '                                 & "ON  KBN2.KBN_GROUP_CD = 'M001'                                               " & vbNewLine _
    '                                 & "AND KBN2.KBN_CD = OUTL.PC_KB                                                 " & vbNewLine _
    '                                 & "AND KBN2.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
    '                                 & "--区分M(商品状態区分(中身))                                                  " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..Z_KBN KBN3                                                 " & vbNewLine _
    '                                 & "ON  KBN3.KBN_GROUP_CD = 'S005'                                               " & vbNewLine _
    '                                 & "AND KBN3.KBN_CD = INS.GOODS_COND_KB_1                                        " & vbNewLine _
    '                                 & "AND KBN3.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
    '                                 & "--区分M(商品状態区分(外観))                                                  " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..Z_KBN KBN4                                                 " & vbNewLine _
    '                                 & "ON  KBN4.KBN_GROUP_CD = 'S006'                                               " & vbNewLine _
    '                                 & "AND KBN4.KBN_CD = INS.GOODS_COND_KB_2                                        " & vbNewLine _
    '                                 & "AND KBN4.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
    '                                 & "--出荷Lでの荷主帳票パターン取得                                              " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                            " & vbNewLine _
    '                                 & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                          " & vbNewLine _
    '                                 & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                          " & vbNewLine _
    '                                 & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                          " & vbNewLine _
    '                                 & "AND '00' = MCR1.CUST_CD_S                                                    " & vbNewLine _
    '                                 & "AND MCR1.PTN_ID = '17'                                                       " & vbNewLine _
    '                                 & "--帳票パターン取得                                                           " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_RPT MR1                                                  " & vbNewLine _
    '                                 & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                           " & vbNewLine _
    '                                 & "AND MR1.PTN_ID = MCR1.PTN_ID                                                 " & vbNewLine _
    '                                 & "AND MR1.PTN_CD = MCR1.PTN_CD                                                 " & vbNewLine _
    '                                 & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
    '                                 & "--商品Mの荷主での荷主帳票パターン取得                                        " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                            " & vbNewLine _
    '                                 & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                            " & vbNewLine _
    '                                 & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                            " & vbNewLine _
    '                                 & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                            " & vbNewLine _
    '                                 & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                            " & vbNewLine _
    '                                 & "AND MCR2.PTN_ID = '17'                                                       " & vbNewLine _
    '                                 & "--帳票パターン取得                                                           " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_RPT MR2                                                  " & vbNewLine _
    '                                 & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                           " & vbNewLine _
    '                                 & "AND MR2.PTN_ID = MCR2.PTN_ID                                                 " & vbNewLine _
    '                                 & "AND MR2.PTN_CD = MCR2.PTN_CD                                                 " & vbNewLine _
    '                                 & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
    '                                 & "--在庫データ(D_ZAI_TRS)取得                                                  " & vbNewLine _
    '                                 & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                            " & vbNewLine _
    '                                 & "ON  ZAI.NRS_BR_CD  = OUTS.NRS_BR_CD                                          " & vbNewLine _
    '                                 & "AND ZAI.ZAI_REC_NO = OUTS.ZAI_REC_NO                                         " & vbNewLine _
    '                                 & "AND ZAI.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
    '                                 & "--存在しない場合の帳票パターン取得                                           " & vbNewLine _
    '                                 & "LEFT JOIN $LM_MST$..M_RPT MR3                                                " & vbNewLine _
    '                                 & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                           " & vbNewLine _
    '                                 & "AND MR3.PTN_ID = '17'                                                        " & vbNewLine _
    '                                 & "AND MR3.STANDARD_FLAG = '01'                                                 " & vbNewLine _
    '                                 & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
    '                                 & "WHERE                                                                        " & vbNewLine _
    '                                 & "1 = 1                                                                        " & vbNewLine
    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "--出荷L                                                                        " & vbNewLine _
                                     & "FROM                                                                           " & vbNewLine _
                                     & "$LM_TRN$..C_OUTKA_L OUTL                                                       " & vbNewLine _
                                     & "--トランザクションテーブル                                                     " & vbNewLine _
                                     & "--出荷M                                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                             " & vbNewLine _
                                     & "ON  OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                          " & vbNewLine _
                                     & "--出荷S                                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                             " & vbNewLine _
                                     & "ON  OUTS.NRS_BR_CD = OUTM.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND OUTS.OUTKA_NO_L = OUTM.OUTKA_NO_L                                          " & vbNewLine _
                                     & "AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                          " & vbNewLine _
                                     & "--要望管理2035対応                                                                     " & vbNewLine _
                                     & "AND ((OUTM.SYS_DEL_FLG = '1')                                                  " & vbNewLine _
                                     & "     OR (OUTM.SYS_DEL_FLG = '0' AND OUTS.SYS_DEL_FLG = '0'))                   " & vbNewLine _
                                     & "--出荷EDIL                                                                     " & vbNewLine _
                                     & "--LEFT JOIN                                                                    " & vbNewLine _
                                     & "--(                                                                            " & vbNewLine _
                                     & "--SELECT DISTINCT                                                              " & vbNewLine _
                                     & "-- NRS_BR_CD                                                                   " & vbNewLine _
                                     & "--,OUTKA_CTL_NO                                                                " & vbNewLine _
                                     & "--,CUST_CD_L                                                                   " & vbNewLine _
                                     & "--,SHIP_NM_L                                                                   " & vbNewLine _
                                     & "--,MIN(DEST_CD)     AS DEST_CD                                                 " & vbNewLine _
                                     & "--,MIN(DEST_NM)     AS DEST_NM                                                 " & vbNewLine _
                                     & "--,MIN(DEST_AD_1)   AS DEST_AD_1                                               " & vbNewLine _
                                     & "--,MIN(DEST_AD_2)   AS DEST_AD_2                                               " & vbNewLine _
                                     & "--,DEST_AD_3                                                                   " & vbNewLine _
                                     & "--,DEST_JIS_CD                                                                 " & vbNewLine _
                                     & "--,DEST_TEL                                                                    " & vbNewLine _
                                     & "--,FREE_C03                                                                    " & vbNewLine _
                                     & "--,SYS_DEL_FLG                                                                 " & vbNewLine _
                                     & "--FROM                                                                         " & vbNewLine _
                                     & "--$LM_TRN$..H_OUTKAEDI_L                                                         " & vbNewLine _
                                     & "--WHERE                                                                        " & vbNewLine _
                                     & "--NRS_BR_CD = @NRS_BR_CD                                                       " & vbNewLine _
                                     & "--AND OUTKA_CTL_NO = @OUTKA_NO_L                                               " & vbNewLine _
                                     & "--GROUP BY                                                                     " & vbNewLine _
                                     & "-- NRS_BR_CD                                                                   " & vbNewLine _
                                     & "--,OUTKA_CTL_NO                                                                " & vbNewLine _
                                     & "--,CUST_CD_L                                                                   " & vbNewLine _
                                     & "--,SHIP_NM_L                                                                   " & vbNewLine _
                                     & "--,DEST_AD_3                                                                   " & vbNewLine _
                                     & "--,DEST_JIS_CD                                                                 " & vbNewLine _
                                     & "--,DEST_TEL                                                                    " & vbNewLine _
                                     & "--,FREE_C03                                                                    " & vbNewLine _
                                     & "--,SYS_DEL_FLG                                                                 " & vbNewLine _
                                     & "--) EDIL                                                                       " & vbNewLine _
                                     & "--ON  EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                          " & vbNewLine _
                                     & "--AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                      " & vbNewLine _
                                     & "--下記の内容に変更                                                             " & vbNewLine _
                                     & " LEFT JOIN (                                                                    " & vbNewLine _
                                     & "            SELECT                                                              " & vbNewLine _
                                     & "                   NRS_BR_CD                                                    " & vbNewLine _
                                     & "                 , EDI_CTL_NO                                                   " & vbNewLine _
                                     & "                 , OUTKA_CTL_NO                                                 " & vbNewLine _
                                     & "             FROM (                                                             " & vbNewLine _
                                     & "                    SELECT                                                      " & vbNewLine _
                                     & "                           EDIOUTL.NRS_BR_CD                                    " & vbNewLine _
                                     & "                         , EDIOUTL.EDI_CTL_NO                                   " & vbNewLine _
                                     & "                         , EDIOUTL.OUTKA_CTL_NO                                 " & vbNewLine _
                                     & "                         , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1            " & vbNewLine _
                                     & "                           ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD  " & vbNewLine _
                                     & "                                                              , EDIOUTL.OUTKA_CTL_NO  " & vbNewLine _
                                     & "                                                       ORDER BY EDIOUTL.NRS_BR_CD     " & vbNewLine _
                                     & "                                                              , EDIOUTL.EDI_CTL_NO    " & vbNewLine _
                                     & "                                                  )                                   " & vbNewLine _
                                     & "                           END AS IDX                                                 " & vbNewLine _
                                     & "                     FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                              " & vbNewLine _
                                     & "                    WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                  " & vbNewLine _
                                     & "                      AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                           " & vbNewLine _
                                     & "                      AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L                          " & vbNewLine _
                                     & "                  ) EBASE                                                             " & vbNewLine _
                                     & "            WHERE EBASE.IDX = 1                                                       " & vbNewLine _
                                     & "            ) TOPEDI                                                                  " & vbNewLine _
                                     & "        ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                       " & vbNewLine _
                                     & "       AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                      " & vbNewLine _
                                     & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                " & vbNewLine _
                                     & "        ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                         " & vbNewLine _
                                     & "       AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                        " & vbNewLine _
                                     & "--入荷L                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..B_INKA_L INL                                               " & vbNewLine _
                                     & "ON  INL.NRS_BR_CD = OUTS.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND INL.INKA_NO_L = OUTS.INKA_NO_L                                           " & vbNewLine _
                                     & "--入荷S                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..B_INKA_S INS                                               " & vbNewLine _
                                     & "ON  INS.NRS_BR_CD = OUTS.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND INS.INKA_NO_L = OUTS.INKA_NO_L                                           " & vbNewLine _
                                     & "AND INS.INKA_NO_M = OUTS.INKA_NO_M                                           " & vbNewLine _
                                     & "AND INS.INKA_NO_S = OUTS.INKA_NO_S                                           " & vbNewLine _
                                     & "--運送L                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..F_UNSO_L UL                                                " & vbNewLine _
                                     & "ON  UL.NRS_BR_CD = OUTL.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND UL.INOUTKA_NO_L = OUTL.OUTKA_NO_L                                        " & vbNewLine _
                                     & "AND UL.MOTO_DATA_KB = '20'                                                   " & vbNewLine _
                                     & "--マスタテーブル                                                             " & vbNewLine _
                                     & "--商品M                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_GOODS MG                                                 " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                      " & vbNewLine _
                                     & "AND MG.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                     & "--荷主M(商品M経由)                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST MC                                                  " & vbNewLine _
                                     & "ON  MC.NRS_BR_CD = MG.NRS_BR_CD                                              " & vbNewLine _
                                     & "AND MC.CUST_CD_L = MG.CUST_CD_L                                              " & vbNewLine _
                                     & "AND MC.CUST_CD_M = MG.CUST_CD_M                                              " & vbNewLine _
                                     & "AND MC.CUST_CD_S = MG.CUST_CD_S                                              " & vbNewLine _
                                     & "AND MC.CUST_CD_SS = MG.CUST_CD_SS                                            " & vbNewLine _
                                     & "AND MC.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                     & "--届先M(届先取得)(出荷L参照)                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST MDOUT                                               " & vbNewLine _
                                     & "ON  MDOUT.NRS_BR_CD = OUTL.NRS_BR_CD                                         " & vbNewLine _
                                     & "AND MDOUT.CUST_CD_L = OUTL.CUST_CD_L                                         " & vbNewLine _
                                     & "AND MDOUT.DEST_CD = OUTL.DEST_CD                                             " & vbNewLine _
                                     & "AND MDOUT.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                     & "--届先M(売上先取得)(出荷L参照)                                               " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_DEST MDOUTU                                              " & vbNewLine _
                                     & "ON  MDOUTU.NRS_BR_CD = OUTL.NRS_BR_CD                                        " & vbNewLine _
                                     & "AND MDOUTU.CUST_CD_L = OUTL.CUST_CD_L                                        " & vbNewLine _
                                     & "AND MDOUTU.DEST_CD = OUTL.SHIP_CD_L                                          " & vbNewLine _
                                     & "AND MDOUTU.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                     & "--運送会社M                                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_UNSOCO MUCO                                              " & vbNewLine _
                                     & "ON  MUCO.NRS_BR_CD = UL.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND MUCO.UNSOCO_CD = UL.UNSO_CD                                              " & vbNewLine _
                                     & "AND MUCO.UNSOCO_BR_CD = UL.UNSO_BR_CD                                        " & vbNewLine _
                                     & "AND MUCO.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--倉庫M                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_SOKO MSO                                                 " & vbNewLine _
                                     & "ON  MSO.WH_CD = OUTL.WH_CD                                                   " & vbNewLine _
                                     & "AND MSO.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--ユーザM                                                                    " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..S_USER MUSER                                               " & vbNewLine _
                                     & "ON MUSER.USER_CD = OUTM.SYS_UPD_USER                                         " & vbNewLine _
                                     & "AND MUSER.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                     & "--区分M(納入予定区分)                                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN1                                                 " & vbNewLine _
                                     & "ON  KBN1.KBN_GROUP_CD = 'N010'                                               " & vbNewLine _
                                     & "AND KBN1.KBN_CD = OUTL.ARR_PLAN_TIME                                         " & vbNewLine _
                                     & "AND KBN1.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--区分M(元着払区分)                                                          " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN2                                                 " & vbNewLine _
                                     & "ON  KBN2.KBN_GROUP_CD = 'M001'                                               " & vbNewLine _
                                     & "AND KBN2.KBN_CD = OUTL.PC_KB                                                 " & vbNewLine _
                                     & "AND KBN2.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--区分M(商品状態区分(中身))                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN3                                                 " & vbNewLine _
                                     & "ON  KBN3.KBN_GROUP_CD = 'S005'                                               " & vbNewLine _
                                     & "AND KBN3.KBN_CD = INS.GOODS_COND_KB_1                                        " & vbNewLine _
                                     & "AND KBN3.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--区分M(商品状態区分(外観))                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..Z_KBN KBN4                                                 " & vbNewLine _
                                     & "ON  KBN4.KBN_GROUP_CD = 'S006'                                               " & vbNewLine _
                                     & "AND KBN4.KBN_CD = INS.GOODS_COND_KB_2                                        " & vbNewLine _
                                     & "AND KBN4.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "--出荷Lでの荷主帳票パターン取得                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                            " & vbNewLine _
                                     & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                          " & vbNewLine _
                                     & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                          " & vbNewLine _
                                     & "AND '00' = MCR1.CUST_CD_S                                                    " & vbNewLine _
                                     & "AND MCR1.PTN_ID = '17'                                                       " & vbNewLine _
                                     & "--帳票パターン取得                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                                                  " & vbNewLine _
                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                                                 " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                                                 " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                            " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                            " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                            " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                            " & vbNewLine _
                                     & "AND MCR2.PTN_ID = '17'                                                       " & vbNewLine _
                                     & "--帳票パターン取得                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                                                  " & vbNewLine _
                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                                                 " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                                                 " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "--在庫データ(D_ZAI_TRS)取得                                                  " & vbNewLine _
                                     & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                            " & vbNewLine _
                                     & "ON  ZAI.NRS_BR_CD  = OUTS.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND ZAI.ZAI_REC_NO = OUTS.ZAI_REC_NO                                         " & vbNewLine _
                                     & "AND ZAI.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR3                                                " & vbNewLine _
                                     & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MR3.PTN_ID = '17'                                                        " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                                                 " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine 
    'END YANAI 要望番号650

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE As String _
        = " WHERE                                                                                                       " & vbNewLine _
        & "      1 = 1                                                                                                  " & vbNewLine


    ''' <summary>
    ''' ORDER BY（①営業所コード、②管理番号L、③印刷順番、④管理番号M、⑤管理番号S）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                    " & vbNewLine _
                                         & "     OUTL.NRS_BR_CD                         " & vbNewLine _
                                         & "    ,OUTL.OUTKA_NO_L                        " & vbNewLine _
                                         & "    ,OUTM.PRINT_SORT                        " & vbNewLine _
                                         & "    ,OUTM.OUTKA_NO_M                        " & vbNewLine _
                                         & "    ,OUTS.OUTKA_NO_S                        " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ADD_SAGYO As String _
        = " , ISNULL(SAGYO_L_1.SAGYO_REC_NO, '') AS SAGYO_REC_NO_1                   " & vbNewLine _
        & " , ISNULL(SAGYO_L_1.SAGYO_CD, '')     AS SAGYO_CD_1                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_1.SAGYO_NM, '')	 AS SAGYO_NM_1                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_2.SAGYO_REC_NO, '') AS SAGYO_REC_NO_2                   " & vbNewLine _
        & " , ISNULL(SAGYO_L_2.SAGYO_CD, '')     AS SAGYO_CD_2                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_2.SAGYO_NM, '')	 AS SAGYO_NM_2                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_3.SAGYO_REC_NO, '') AS SAGYO_REC_NO_3                   " & vbNewLine _
        & " , ISNULL(SAGYO_L_3.SAGYO_CD, '')     AS SAGYO_CD_3                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_3.SAGYO_NM, '')	 AS SAGYO_NM_3                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_4.SAGYO_REC_NO, '') AS SAGYO_REC_NO_4                   " & vbNewLine _
        & " , ISNULL(SAGYO_L_4.SAGYO_CD, '')     AS SAGYO_CD_4                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_4.SAGYO_NM, '')	 AS SAGYO_NM_4                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_5.SAGYO_REC_NO, '') AS SAGYO_REC_NO_5                   " & vbNewLine _
        & " , ISNULL(SAGYO_L_5.SAGYO_CD, '')     AS SAGYO_CD_5                       " & vbNewLine _
        & " , ISNULL(SAGYO_L_5.SAGYO_NM, '')	 AS SAGYO_NM_5                       " & vbNewLine _
        & " , ISNULL(SAGYO_M_1.SAGYO_REC_NO, '') AS SAGYO_MEI_REC_NO_1               " & vbNewLine _
        & " , ISNULL(SAGYO_M_1.SAGYO_CD, '')     AS SAGYO_MEI_CD_1                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_1.SAGYO_NM, '')	 AS SAGYO_MEI_NM_1                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_2.SAGYO_REC_NO, '') AS SAGYO_MEI_REC_NO_2               " & vbNewLine _
        & " , ISNULL(SAGYO_M_2.SAGYO_CD, '')     AS SAGYO_MEI_CD_2                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_2.SAGYO_NM, '')	 AS SAGYO_MEI_NM_2                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_3.SAGYO_REC_NO, '') AS SAGYO_MEI_REC_NO_3               " & vbNewLine _
        & " , ISNULL(SAGYO_M_3.SAGYO_CD, '')     AS SAGYO_MEI_CD_3                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_3.SAGYO_NM, '')	 AS SAGYO_MEI_NM_3                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_4.SAGYO_REC_NO, '') AS SAGYO_MEI_REC_NO_4               " & vbNewLine _
        & " , ISNULL(SAGYO_M_4.SAGYO_CD, '')     AS SAGYO_MEI_CD_4                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_4.SAGYO_NM, '')	 AS SAGYO_MEI_NM_4                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_5.SAGYO_REC_NO, '') AS SAGYO_MEI_REC_NO_5               " & vbNewLine _
        & " , ISNULL(SAGYO_M_5.SAGYO_CD, '')     AS SAGYO_MEI_CD_5                   " & vbNewLine _
        & " , ISNULL(SAGYO_M_5.SAGYO_NM, '')	 AS SAGYO_MEI_NM_5                   " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_ADD_SAGYO As String _
        = "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CL.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CL.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '20'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = CL.SYS_DEL_FLG                                                   " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CL.OUTKA_NO_L, '000')                                     " & vbNewLine _
        & "                    WHERE CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 1) AS SAGYO_L_1                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_L_1.NRS_BR_CD  = OUTL.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_L_1.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CL.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CL.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '20'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = CL.SYS_DEL_FLG                                                   " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CL.OUTKA_NO_L, '000')                                     " & vbNewLine _
        & "                    WHERE CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 2) AS SAGYO_L_2                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_L_2.NRS_BR_CD  = OUTL.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_L_2.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CL.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CL.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '20'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = CL.SYS_DEL_FLG                                                   " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CL.OUTKA_NO_L, '000')                                     " & vbNewLine _
        & "                    WHERE CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 3) AS SAGYO_L_3                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_L_3.NRS_BR_CD  = OUTL.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_L_3.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CL.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CL.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '20'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = CL.SYS_DEL_FLG                                                   " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CL.OUTKA_NO_L, '000')                                     " & vbNewLine _
        & "                    WHERE CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 4) AS SAGYO_L_4                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_L_4.NRS_BR_CD  = OUTL.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_L_4.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CL.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CL.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CL.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '20'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = '0'                                                              " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CL.OUTKA_NO_L, '000')                                     " & vbNewLine _
        & "                    WHERE CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 5) AS SAGYO_L_5                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_L_5.NRS_BR_CD  = OUTL.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_L_5.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "              , OUTKA_NO_M                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CM.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CM.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , CM.OUTKA_NO_M                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_M AS  CM                                                         " & vbNewLine _
        & "                       ON CM.NRS_BR_CD   = CL.NRS_BR_CD                                                      " & vbNewLine _
        & "                      AND CM.OUTKA_NO_L  = CL.OUTKA_NO_L                                                     " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CM.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '21'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = CM.SYS_DEL_FLG                                                   " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CM.OUTKA_NO_L, CM.OUTKA_NO_M)                             " & vbNewLine _
        & "                    WHERE CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 1) AS SAGYO_M_1                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_M_1.NRS_BR_CD  = OUTM.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_M_1.OUTKA_NO_L = OUTM.OUTKA_NO_L                                                               " & vbNewLine _
        & "    AND SAGYO_M_1.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "              , OUTKA_NO_M                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CM.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CM.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , CM.OUTKA_NO_M                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_M AS  CM                                                         " & vbNewLine _
        & "                       ON CM.NRS_BR_CD   = CL.NRS_BR_CD                                                      " & vbNewLine _
        & "                      AND CM.OUTKA_NO_L  = CL.OUTKA_NO_L                                                     " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CM.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '21'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = CM.SYS_DEL_FLG                                                   " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CM.OUTKA_NO_L, CM.OUTKA_NO_M)                             " & vbNewLine _
        & "                    WHERE CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 2) AS SAGYO_M_2                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_M_2.NRS_BR_CD  = OUTM.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_M_2.OUTKA_NO_L = OUTM.OUTKA_NO_L                                                               " & vbNewLine _
        & "    AND SAGYO_M_2.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "              , OUTKA_NO_M                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CM.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CM.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , CM.OUTKA_NO_M                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_M AS  CM                                                         " & vbNewLine _
        & "                       ON CM.NRS_BR_CD   = CL.NRS_BR_CD                                                      " & vbNewLine _
        & "                      AND CM.OUTKA_NO_L  = CL.OUTKA_NO_L                                                     " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CM.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '21'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = CM.SYS_DEL_FLG                                                   " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CM.OUTKA_NO_L, CM.OUTKA_NO_M)                             " & vbNewLine _
        & "                    WHERE CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 3) AS SAGYO_M_3                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_M_3.NRS_BR_CD  = OUTM.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_M_3.OUTKA_NO_L = OUTM.OUTKA_NO_L                                                               " & vbNewLine _
        & "    AND SAGYO_M_3.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "              , OUTKA_NO_M                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CM.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CM.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , CM.OUTKA_NO_M                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_M AS  CM                                                         " & vbNewLine _
        & "                       ON CM.NRS_BR_CD   = CL.NRS_BR_CD                                                      " & vbNewLine _
        & "                      AND CM.OUTKA_NO_L  = CL.OUTKA_NO_L                                                     " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CM.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '21'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = CM.SYS_DEL_FLG                                                   " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CM.OUTKA_NO_L, CM.OUTKA_NO_M)                             " & vbNewLine _
        & "                    WHERE CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 4) AS SAGYO_M_4                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_M_4.NRS_BR_CD  = OUTM.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_M_4.OUTKA_NO_L = OUTM.OUTKA_NO_L                                                               " & vbNewLine _
        & "    AND SAGYO_M_4.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine _
        & "   LEFT JOIN                                                                                                 " & vbNewLine _
        & "        (SELECT SAGYO_REC_NO                                                                                 " & vbNewLine _
        & "              , SAGYO_CD                                                                                     " & vbNewLine _
        & "              , SAGYO_NM                                                                                     " & vbNewLine _
        & "              , NRS_BR_CD                                                                                    " & vbNewLine _
        & "              , OUTKA_NO_L                                                                                   " & vbNewLine _
        & "              , OUTKA_NO_M                                                                                   " & vbNewLine _
        & "           FROM                                                                                              " & vbNewLine _
        & "                 ( SELECT                                                                                    " & vbNewLine _
        & "                          CM.NRS_BR_CD                                                                       " & vbNewLine _
        & "                        , CM.OUTKA_NO_L                                                                      " & vbNewLine _
        & "                        , CM.OUTKA_NO_M                                                                      " & vbNewLine _
        & "                        , SAGYO_REC_NO AS SAGYO_REC_NO                                                       " & vbNewLine _
        & "                        , SAGYO_CD                                                                           " & vbNewLine _
        & "                        , SAGYO_NM                                                                           " & vbNewLine _
        & "                        , ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM       " & vbNewLine _
        & "                     FROM                                                                                    " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_L AS  CL                                                         " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..C_OUTKA_M AS  CM                                                         " & vbNewLine _
        & "                       ON CM.NRS_BR_CD   = CL.NRS_BR_CD                                                      " & vbNewLine _
        & "                      AND CM.OUTKA_NO_L  = CL.OUTKA_NO_L                                                     " & vbNewLine _
        & "                     LEFT JOIN                                                                               " & vbNewLine _
        & "                          $LM_TRN$..E_SAGYO E                                                                " & vbNewLine _
        & "                       ON E.NRS_BR_CD     = CM.NRS_BR_CD                                                     " & vbNewLine _
        & "                      AND E.IOZS_KB       = '21'                                                             " & vbNewLine _
        & "                      AND E.SYS_DEL_FLG   = CM.SYS_DEL_FLG                                                   " & vbNewLine _
        & "                      AND E.INOUTKA_NO_LM = CONCAT(CM.OUTKA_NO_L, CM.OUTKA_NO_M)                             " & vbNewLine _
        & "                    WHERE CL.OUTKA_NO_L   = @OUTKA_NO_L                                                      " & vbNewLine _
        & "                      AND CL.NRS_BR_CD    = @NRS_BR_CD                                                       " & vbNewLine _
        & "                  ) AS BASE                                                                                  " & vbNewLine _
        & "           WHERE BASE.NUM = 5) AS SAGYO_M_5                                                                  " & vbNewLine _
        & "     ON                                                                                                      " & vbNewLine _
        & "        SAGYO_M_5.NRS_BR_CD  = OUTM.NRS_BR_CD                                                                " & vbNewLine _
        & "    AND SAGYO_M_5.OUTKA_NO_L = OUTM.OUTKA_NO_L                                                               " & vbNewLine _
        & "    AND SAGYO_M_5.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                               " & vbNewLine _
        & "                                                                                                             " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMC601IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC601DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMC601DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMC601DAC.SQL_WHERE)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC601DAC", "SelectMPrt", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMC601IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC601DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMC601DAC.SQL_SELECT_ADD_SAGYO)
        Me._StrSql.Append(LMC601DAC.SQL_FROM)             'SQL構築(データ抽出用From句
        Me._StrSql.Append(LMC601DAC.SQL_FROM_ADD_SAGYO)
        Me._StrSql.Append(LMC601DAC.SQL_WHERE)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMC601DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC601DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PRINT_SORT", "PRINT_SORT")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("PC_KB", "PC_KB")
        map.Add("URIG_NM", "URIG_NM")
        map.Add("SAGYO_REC_NO_1", "SAGYO_REC_NO_1")
        map.Add("SAGYO_CD_1", "SAGYO_CD_1")
        map.Add("SAGYO_NM_1", "SAGYO_NM_1")
        map.Add("SAGYO_REC_NO_2", "SAGYO_REC_NO_2")
        map.Add("SAGYO_CD_2", "SAGYO_CD_2")
        map.Add("SAGYO_NM_2", "SAGYO_NM_2")
        map.Add("SAGYO_REC_NO_3", "SAGYO_REC_NO_3")
        map.Add("SAGYO_CD_3", "SAGYO_CD_3")
        map.Add("SAGYO_NM_3", "SAGYO_NM_3")
        map.Add("SAGYO_REC_NO_4", "SAGYO_REC_NO_4")
        map.Add("SAGYO_CD_4", "SAGYO_CD_4")
        map.Add("SAGYO_NM_4", "SAGYO_NM_4")
        map.Add("SAGYO_REC_NO_5", "SAGYO_REC_NO_5")
        map.Add("SAGYO_CD_5", "SAGYO_CD_5")
        map.Add("SAGYO_NM_5", "SAGYO_NM_5")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("KONSU", "KONSU")
        map.Add("HASU", "HASU")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("ZAN_KONSU", "ZAN_KONSU")
        map.Add("ZAN_HASU", "ZAN_HASU")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("ALCTD_CAN_QT", "ALCTD_CAN_QT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("REMARK_S", "REMARK_S")
        map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
        map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("REMARK_MEI", "REMARK_MEI")
        map.Add("SAGYO_MEI_REC_NO_1", "SAGYO_MEI_REC_NO_1")
        map.Add("SAGYO_MEI_CD_1", "SAGYO_MEI_CD_1")
        map.Add("SAGYO_MEI_NM_1", "SAGYO_MEI_NM_1")
        map.Add("SAGYO_MEI_REC_NO_2", "SAGYO_MEI_REC_NO_2")
        map.Add("SAGYO_MEI_CD_2", "SAGYO_MEI_CD_2")
        map.Add("SAGYO_MEI_NM_2", "SAGYO_MEI_NM_2")
        map.Add("SAGYO_MEI_REC_NO_3", "SAGYO_MEI_REC_NO_3")
        map.Add("SAGYO_MEI_CD_3", "SAGYO_MEI_CD_3")
        map.Add("SAGYO_MEI_NM_3", "SAGYO_MEI_NM_3")
        map.Add("SAGYO_MEI_REC_NO_4", "SAGYO_MEI_REC_NO_4")
        map.Add("SAGYO_MEI_CD_4", "SAGYO_MEI_CD_4")
        map.Add("SAGYO_MEI_NM_4", "SAGYO_MEI_NM_4")
        map.Add("SAGYO_MEI_REC_NO_5", "SAGYO_MEI_REC_NO_5")
        map.Add("SAGYO_MEI_CD_5", "SAGYO_MEI_CD_5")
        map.Add("SAGYO_MEI_NM_5", "SAGYO_MEI_NM_5")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC601OUT")

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
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append(" AND OUTL.NRS_BR_CD = @NRS_BR_CD")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '入荷管理番号
            whereStr = .Item("OUTKA_NO_L").ToString()
            Me._StrSql.Append(" AND OUTL.OUTKA_NO_L = @OUTKA_NO_L")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", whereStr, DBDataType.CHAR))

            'ユーザID
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
