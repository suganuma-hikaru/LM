' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷データ検索
'  EDI荷主ID　　　　:  118　　　 : サクラファインテック(千葉⇒土気)
'  作  成  者       :  daikoku 
'  備  考           :  401（横浜)を丸ごとコピー
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH030DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030DAC118
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 カウント用SQL"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(H_OUTKAEDI_L.EDI_CTL_NO)		       AS SELECT_CNT            " & vbNewLine

#End Region

#Region "検索処理 抽出用SQL"

#Region "H_OUTKAEDI_L SELECT句"

    ''' <summary>
    ''' H_OUTKAEDI_Lデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                      " & vbNewLine _
                                                & " Z4.KBN_NM1                                          AS JYOTAI	           " & vbNewLine _
                                                & ",Z5.KBN_NM1                                          AS HORYU	           " & vbNewLine _
                                                & ",CASE WHEN H_OUTKAEDI_L.CUST_ORD_NO IS NOT NULL                              " & vbNewLine _
                                                & " THEN H_OUTKAEDI_L.CUST_ORD_NO                                               " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_M_FST.CUST_ORD_NO_DTL                                       " & vbNewLine _
                                                & " END                                                 AS CUST_ORD_NO          " & vbNewLine _
                                                & ",Z1.KBN_NM1                                          AS OUTKA_STATE_KB_NM    " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.OUTKO_DATE IS NOT NULL                                  " & vbNewLine _
                                                & " THEN C_OUTKA_L.OUTKO_DATE                                                   " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.OUTKO_DATE                                                " & vbNewLine _
                                                & " END                                                 AS OUTKO_DATE           " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.OUTKA_PLAN_DATE IS NOT NULL                             " & vbNewLine _
                                                & " THEN C_OUTKA_L.OUTKA_PLAN_DATE                                              " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.OUTKA_PLAN_DATE                                           " & vbNewLine _
                                                & " END                                                 AS OUTKA_PLAN_DATE      " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.ARR_PLAN_DATE IS NOT NULL                               " & vbNewLine _
                                                & " THEN C_OUTKA_L.ARR_PLAN_DATE                                                " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.ARR_PLAN_DATE                                             " & vbNewLine _
                                                & " END                                                 AS ARR_PLAN_DATE        " & vbNewLine _
                                                & ",CASE WHEN H_OUTKAEDI_L.CUST_NM_L + '　' + H_OUTKAEDI_L.CUST_NM_M IS NOT NULL       " & vbNewLine _
                                                & " THEN H_OUTKAEDI_L.CUST_NM_L + '　' + H_OUTKAEDI_L.CUST_NM_M                        " & vbNewLine _
                                                & " ELSE M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M                                    " & vbNewLine _
                                                & " END                                                 AS CUST_NM              " & vbNewLine _
                                                & ",CASE WHEN H_OUTKAEDI_L.DEST_NM IS NOT NULL                                  " & vbNewLine _
                                                & " THEN H_OUTKAEDI_L.DEST_NM                                                   " & vbNewLine _
                                                & " ELSE M_DEST.DEST_NM                                                         " & vbNewLine _
                                                & " END                                                 AS DEST_NM              " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.REMARK IS NOT NULL                                      " & vbNewLine _
                                                & " THEN C_OUTKA_L.REMARK                                                       " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.REMARK                                                    " & vbNewLine _
                                                & " END                                                 AS REMARK               " & vbNewLine _
                                                & ",CASE WHEN F_UNSO_L.REMARK IS NOT NULL                                       " & vbNewLine _
                                                & " THEN F_UNSO_L.REMARK                                                        " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.UNSO_ATT                                                  " & vbNewLine _
                                                & " END                                                 AS UNSO_ATT             " & vbNewLine _
                                                & ",H_OUTKAEDI_M_FST.GOODS_NM                           AS GOODS_NM	            " & vbNewLine _
                                                & ",H_OUTKAEDI_M_SUM.OUTKA_TTL_NB                           AS OUTKA_TTL_NB         " & vbNewLine _
                                                & ",CASE WHEN M_DEST.AD_1 + '　' + M_DEST.AD_2 + '　' + M_DEST.AD_3 IS NOT NULL               " & vbNewLine _
                                                & " THEN M_DEST.AD_1 + '　' + M_DEST.AD_2 + '　' + M_DEST.AD_3                                " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.DEST_AD_1 + '　' + H_OUTKAEDI_L.DEST_AD_2 + '　' + C_OUTKA_L.DEST_AD_3  " & vbNewLine _
                                                & " END                                                 AS DEST_AD              " & vbNewLine _
                                                & ",M_UNSOCO.UNSOCO_NM                                  AS UNSO_NM	            " & vbNewLine _
                                                & ",Z2.KBN_NM1                                          AS BIN_KB_NM            " & vbNewLine _
                                                & ",CASE WHEN H_OUTKAEDI_L.SYS_DEL_FLG = '1'                                    " & vbNewLine _
                                                & " THEN ISNULL(MCNT.M_COUNT,0)                                        	                " & vbNewLine _
                                                & " ELSE ISNULL(MCNTSYS.M_COUNT,0)                                        	            " & vbNewLine _
                                                & " END                                                 AS M_COUNT	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.EDI_CTL_NO                             AS EDI_CTL_NO	        " & vbNewLine _
                                                & ",CASE WHEN SUBSTRING(H_OUTKAEDI_L.FREE_C30,5,8) <> '00000000'                 " & vbNewLine _
                                                & " THEN SUBSTRING(H_OUTKAEDI_L.FREE_C30,4,9)                                   " & vbNewLine _
                                                & " ELSE ''                                                                     " & vbNewLine _
                                                & " END                                                 AS MATOME_NO            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.OUTKA_CTL_NO                           AS OUTKA_CTL_NO       " & vbNewLine _
                                                & ",H_OUTKAEDI_L.BUYER_ORD_NO                           AS BUYER_ORD_NO	        " & vbNewLine _
                                                & ",Z3.KBN_NM1                                          AS SYUBETU_KB_NM        " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.OUTKA_PKG_NB IS NOT NULL                                " & vbNewLine _
                                                & " THEN C_OUTKA_L.OUTKA_PKG_NB                                                 " & vbNewLine _
                                                & " ELSE '0'                                                                    " & vbNewLine _
                                                & " END                                                 AS OUTKA_PKG_NB         " & vbNewLine _
                                                & ",Z6.KBN_NM1                                          AS UNSO_MOTO_KB_NM      " & vbNewLine _
                                                & ",H_OUTKAEDI_L.CRT_DATE                               AS CRT_DATE	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.CRT_TIME                               AS CRT_TIME	            " & vbNewLine _
                                                & ",SENDTBL.SEND_DATE                                   AS SEND_DATE	        " & vbNewLine _
                                                & ",SENDTBL.SEND_TIME                                   AS SEND_TIME	        " & vbNewLine _
                                                & ",M_NRS_BR.NRS_BR_NM                                  AS NRS_BR_NM	        " & vbNewLine _
                                                & ",M_SOKO.WH_NM                                        AS WH_NM	            " & vbNewLine _
                                                & ",TANTO_USER.USER_NM                                  AS TANTO_USER	        " & vbNewLine _
                                                & ",ENT_USER.USER_NM                                    AS SYS_ENT_USER         " & vbNewLine _
                                                & ",UPD_USER.USER_NM                                    AS SYS_UPD_USER         " & vbNewLine _
                                                & ",H_OUTKAEDI_L.SYS_UPD_DATE                           AS SYS_UPD_DATE	        " & vbNewLine _
                                                & ",H_OUTKAEDI_L.SYS_UPD_TIME                           AS SYS_UPD_TIME	        " & vbNewLine _
                                                & ",H_OUTKAEDI_L.NRS_BR_CD                              AS NRS_BR_CD	        " & vbNewLine _
                                                & ",H_OUTKAEDI_L.WH_CD                                  AS WH_CD	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.CUST_CD_L                              AS CUST_CD_L            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.CUST_CD_M                              AS CUST_CD_M            " & vbNewLine _
                                                & ",H_OUTKAEDI_M_MIN.NRS_GOODS_CD                       AS GOODS_CD_NRS         " & vbNewLine _
                                                & ",H_OUTKAEDI_L.DEST_CD                                AS DEST_CD              " & vbNewLine _
                                                & ",C_OUTKA_L.OUTKA_STATE_KB                         AS OUTKA_STATE_KB       " & vbNewLine _
                                                & ",F_UNSO_L.UNSO_CD                                    AS UNSO_CD              " & vbNewLine _
                                                & ",F_UNSO_L.UNSO_BR_CD                                 AS UNSO_BR_CD           " & vbNewLine _
                                                & ",F_UNSO_L.UNSO_NO_L                                  AS UNSO_NO_L            " & vbNewLine _
                                                & ",F_UNSO_L.SYS_UPD_DATE                               AS UNSO_SYS_UPD_DATE    " & vbNewLine _
                                                & ",F_UNSO_L.SYS_UPD_TIME                               AS UNSO_SYS_UPD_TIME    " & vbNewLine _
                                                & ",H_OUTKAEDI_M_SUM.MIN_NB                                 AS MIN_NB               " & vbNewLine _
                                                & ",H_OUTKAEDI_L.DEL_KB                                 AS EDI_DEL_KB           " & vbNewLine _
                                                & ",C_OUTKA_L.SYS_DEL_FLG                               AS OUTKA_DEL_KB         " & vbNewLine _
                                                & ",F_UNSO_L.SYS_DEL_FLG                                AS UNSO_DEL_KB          " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C01                               AS FREE_C01	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C02                               AS FREE_C02	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C03                               AS FREE_C03	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C04                               AS FREE_C04	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C30                               AS FREE_C30	            " & vbNewLine _
                                                & ",H_OUTKAEDI_M_SUM.AKAKURO_FLG                            AS AKAKURO_FLG	        " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_01                                  AS EDI_CUST_JISSEKI     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_07                                  AS EDI_CUST_MATOMEF     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_08                                  AS EDI_CUST_DELDISP     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_12                                  AS EDI_CUST_SPECIAL     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_13                                  AS EDI_CUST_HOLDOUT     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_14                                  AS EDI_CUST_UNSOFLG     " & vbNewLine _
                                                & ",M_EDI_CUST.EDI_CUST_INDEX                           AS EDI_CUST_INDEX       " & vbNewLine _
                                                & ",M_EDI_CUST.RCV_NM_HED                               AS RCV_NM_HED           " & vbNewLine _
                                                & ",M_EDI_CUST.SND_NM                                   AS SND_NM               " & vbNewLine _
                                                & ",SENDTBL.SYS_UPD_DATE                                AS SND_SYS_UPD_DATE     " & vbNewLine _
                                                & ",SENDTBL.SYS_UPD_TIME                                AS SND_SYS_UPD_TIME     " & vbNewLine _
                                                & ",H_OUTKAEDI_HED_SFJ.SYS_UPD_DATE                     AS RCV_SYS_UPD_DATE     " & vbNewLine _
                                                & ",H_OUTKAEDI_HED_SFJ.SYS_UPD_TIME                     AS RCV_SYS_UPD_TIME     " & vbNewLine _
                                                & ",C_OUTKA_L.SYS_UPD_DATE                              AS OUTKA_SYS_UPD_DATE   " & vbNewLine _
                                                & ",C_OUTKA_L.SYS_UPD_TIME                              AS OUTKA_SYS_UPD_TIME   " & vbNewLine _
                                                & ",H_OUTKAEDI_L.JISSEKI_FLAG                           AS JISSEKI_FLAG	        " & vbNewLine _
                                                & ",H_OUTKAEDI_L.OUT_FLAG                               AS OUT_FLAG	            " & vbNewLine _
                                                & ",M_EDI_CUST.AUTO_MATOME_FLG                          AS AUTO_MATOME_FLG	    " & vbNewLine _
                                                & ",H_OUTKAEDI_L.SYS_DEL_FLG                            AS SYS_DEL_FLG	        " & vbNewLine _
                                                & ",M_EDI_CUST.ORDER_CHECK_FLG                          AS ORDER_CHECK_FLG      " & vbNewLine

#End Region

    'Private Const SQL_GROUP_BY As String = "GROUP BY   

#Region "H_OUTKAEDI_L COUNT FROM句"

    ''' <summary>
    ''' 検索用SQLCOUNTFROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COUNT_FROM As String = "FROM                                                              " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                                         " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..C_OUTKA_L                                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =C_OUTKA_L.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.OUTKA_CTL_NO =C_OUTKA_L.OUTKA_NO_L                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CUST_ORD_NO_DTL                                  " & vbNewLine _
                                        & ",H_OUTKAEDI_M.GOODS_NM                                         " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M     H_OUTKAEDI_M                        " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO_CHU = '001'                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & ")                   H_OUTKAEDI_M_FST                           " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_M_FST.NRS_BR_CD             " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_M_FST.EDI_CTL_NO           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_DEST                       M_DEST                  " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_DEST.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_DEST.CUST_CD_L                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.DEST_CD = M_DEST.DEST_CD                          " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..F_UNSO_L                     F_UNSO_L                " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.OUTKA_CTL_NO = F_UNSO_L.INOUTKA_NO_L              " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.OUTKA_CTL_NO <> ''                                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_UNSOCO                 M_UNSOCO                    " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "M_TCUST.CUST_CD_L AS CUST_CD_L                                 " & vbNewLine _
                                        & ",min(USER_CD) AS USER_CD                                       " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_MST$..M_TCUST              M_TCUST                         " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "M_TCUST.CUST_CD_L                                              " & vbNewLine _
                                        & ") M_TCUST                                                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_TCUST.CUST_CD_L                     " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               TANTO_USER                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "M_TCUST.USER_CD = TANTO_USER.USER_CD                           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               ENT_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_ENT_USER = ENT_USER.USER_CD                   " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               UPD_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_UPD_USER = UPD_USER.USER_CD                   " & vbNewLine _
                                        & "INNER JOIN                                                     " & vbNewLine _
                                        & "$LM_MST$..M_EDI_CUST                       M_EDI_CUST          " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_EDI_CUST.NRS_BR_CD                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.WH_CD = M_EDI_CUST.WH_CD                          " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_EDI_CUST.CUST_CD_L                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_EDI_CUST.CUST_CD_M                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_EDI_CUST.INOUT_KB = '0'                                      " & vbNewLine

#End Region

#Region "H_OUTKAEDI_L FROM句"

    ''' <summary>
    ''' 検索用SQLFROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM                                                              " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                                         " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..C_OUTKA_L                                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =C_OUTKA_L.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.OUTKA_CTL_NO =C_OUTKA_L.OUTKA_NO_L                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                        Z1                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "C_OUTKA_L.OUTKA_STATE_KB = Z1.KBN_CD                           " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z1.KBN_GROUP_CD = 'S010'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CUST_ORD_NO_DTL                                  " & vbNewLine _
                                        & ",H_OUTKAEDI_M.GOODS_NM                                         " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M     H_OUTKAEDI_M                        " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO_CHU = '001'                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & ")                   H_OUTKAEDI_M_FST                           " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_M_FST.NRS_BR_CD             " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_M_FST.EDI_CTL_NO           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ",MIN(H_OUTKAEDI_M.EDI_CTL_NO_CHU) AS EDI_CTL_NO_CHU            " & vbNewLine _
                                        & ",SUM(H_OUTKAEDI_M.OUTKA_TTL_NB) AS OUTKA_TTL_NB                " & vbNewLine _
                                        & ",MIN(H_OUTKAEDI_M.OUTKA_TTL_NB) AS MIN_NB                      " & vbNewLine _
                                        & ",MAX(H_OUTKAEDI_M.AKAKURO_KB) AS AKAKURO_FLG                   " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M     H_OUTKAEDI_M                        " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                 H_OUTKAEDI_L            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD  = H_OUTKAEDI_M.NRS_BR_CD               " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = H_OUTKAEDI_M.EDI_CTL_NO              " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "AND    (H_OUTKAEDI_L.DEL_KB = '1'                              " & vbNewLine _
                                        & "OR     (H_OUTKAEDI_L.DEL_KB <> '1'                             " & vbNewLine _
                                        & "AND    H_OUTKAEDI_M.DEL_KB <> '1'))                            " & vbNewLine _
                                        & "AND    H_OUTKAEDI_M.OUT_KB <> '1'                              " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ")                   H_OUTKAEDI_M_SUM                           " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_M_SUM.NRS_BR_CD             " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_M_SUM.EDI_CTL_NO           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M             H_OUTKAEDI_M_MIN            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_M_SUM.NRS_BR_CD =H_OUTKAEDI_M_MIN.NRS_BR_CD         " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M_SUM.EDI_CTL_NO =H_OUTKAEDI_M_MIN.EDI_CTL_NO       " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M_SUM.EDI_CTL_NO_CHU =H_OUTKAEDI_M_MIN.EDI_CTL_NO_CHU   " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_DEST                       M_DEST                  " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_DEST.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_DEST.CUST_CD_L                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.DEST_CD = M_DEST.DEST_CD                          " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_CUST                       M_CUST                  " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_CUST.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_CUST.CUST_CD_L                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_CUST.CUST_CD_M                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_CUST.CUST_CD_S = '00'                                        " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_CUST.CUST_CD_SS = '00'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_GOODS              M_GOODS                         " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_M_MIN.NRS_BR_CD =M_GOODS.NRS_BR_CD                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M_MIN.NRS_GOODS_CD = M_GOODS.GOODS_CD_NRS           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..F_UNSO_L                     F_UNSO_L                " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.OUTKA_CTL_NO = F_UNSO_L.INOUTKA_NO_L              " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.OUTKA_CTL_NO <> ''                                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_UNSOCO                 M_UNSOCO                    " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",COUNT(H_OUTKAEDI_M.EDI_CTL_NO_CHU)   AS  M_COUNT              " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M   H_OUTKAEDI_M                          " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ")                           MCNT                               " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = MCNT.EDI_CTL_NO                      " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",COUNT(H_OUTKAEDI_M.EDI_CTL_NO_CHU)   AS  M_COUNT              " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M   H_OUTKAEDI_M                          " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ")                           MCNTSYS                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = MCNTSYS.EDI_CTL_NO                   " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z2                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.BIN_KB = Z2.KBN_CD                                " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z2.KBN_GROUP_CD = 'U001'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z3                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYUBETU_KB = Z3.KBN_CD                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z3.KBN_GROUP_CD = 'S020'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z6                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_TEHAI_KB = Z6.KBN_CD                         " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z6.KBN_GROUP_CD = 'T015'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_SOKO               M_SOKO                          " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "M_SOKO.NRS_BR_CD = H_OUTKAEDI_L.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_SOKO.WH_CD = H_OUTKAEDI_L.WH_CD                              " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_NRS_BR                                             " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_NRS_BR.NRS_BR_CD                    " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "M_TCUST.CUST_CD_L AS CUST_CD_L                                 " & vbNewLine _
                                        & ",min(USER_CD) AS USER_CD                                       " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_MST$..M_TCUST              M_TCUST                         " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "M_TCUST.CUST_CD_L                                              " & vbNewLine _
                                        & ") M_TCUST                                                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_TCUST.CUST_CD_L                     " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               TANTO_USER                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "M_TCUST.USER_CD = TANTO_USER.USER_CD                           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               ENT_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_ENT_USER = ENT_USER.USER_CD                   " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               UPD_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_UPD_USER = UPD_USER.USER_CD                   " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                        Z4                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_DEL_FLG = Z4.KBN_CD                           " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z4.KBN_GROUP_CD = 'S051'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                        Z5                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.DEL_KB = Z5.KBN_CD                                " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z5.KBN_GROUP_CD = 'E011'                                       " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--Z5.KBN_CD = '3'                                                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_HED_SFJ                                   " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_HED_SFJ.NRS_BR_CD           " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_HED_SFJ.EDI_CTL_NO         " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "H_SENDOUTEDI_SFJ.EDI_CTL_NO                                    " & vbNewLine _
                                        & ",MAX(H_SENDOUTEDI_SFJ.SEND_DATE) AS SEND_DATE                  " & vbNewLine _
                                        & ",MAX(H_SENDOUTEDI_SFJ.SEND_TIME) AS SEND_TIME                  " & vbNewLine _
                                        & ",MAX(H_SENDOUTEDI_SFJ.SYS_UPD_DATE) AS SYS_UPD_DATE            " & vbNewLine _
                                        & ",MAX(H_SENDOUTEDI_SFJ.SYS_UPD_TIME) AS SYS_UPD_TIME            " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_SENDOUTEDI_SFJ   H_SENDOUTEDI_SFJ                  " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_SENDOUTEDI_SFJ.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "H_SENDOUTEDI_SFJ.EDI_CTL_NO                                    " & vbNewLine _
                                        & ")                           SENDTBL                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = SENDTBL.EDI_CTL_NO                   " & vbNewLine _
                                        & "INNER JOIN                                                     " & vbNewLine _
                                        & "$LM_MST$..M_EDI_CUST                       M_EDI_CUST          " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_EDI_CUST.NRS_BR_CD                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.WH_CD = M_EDI_CUST.WH_CD                          " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_EDI_CUST.CUST_CD_L                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_EDI_CUST.CUST_CD_M                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_EDI_CUST.INOUT_KB = '0'                                      " & vbNewLine

#End Region

#End Region

#Region "出荷登録処理 データ抽出用SQL"

#Region "H_OUTKAEDI_L SELECT句"
    ''' <summary>
    ''' H_OUTKAEDI_Lデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const L_DEF_SQL_SELECT_DATA As String = " SELECT                                                                                " & vbNewLine _
                                            & " '0'                                                 AS DEL_KB                               " & vbNewLine _
                                            & ",H_OUTKAEDI_L.NRS_BR_CD                                           AS NRS_BR_CD                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.EDI_CTL_NO                                          AS EDI_CTL_NO                           " & vbNewLine _
                                            & ",H_OUTKAEDI_L.OUTKA_CTL_NO                                          AS OUTKA_CTL_NO                           " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.OUTKA_KB <> ''                                                 " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.OUTKA_KB                                                                  " & vbNewLine _
                                            & " ELSE '10'                                                                                   " & vbNewLine _
                                            & " END                                                 AS OUTKA_KB                             " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.SYUBETU_KB <> ''                                               " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.SYUBETU_KB                                                                " & vbNewLine _
                                            & " ELSE '10'                                                                                   " & vbNewLine _
                                            & " END                                                 AS SYUBETU_KB                           " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.NAIGAI_KB <> ''                                                " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.NAIGAI_KB                                                                 " & vbNewLine _
                                            & " ELSE '01'                                                                                   " & vbNewLine _
                                            & " END                                                 AS NAIGAI_KB                            " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.OUTKA_STATE_KB <> ''                                           " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.OUTKA_STATE_KB                                                            " & vbNewLine _
                                            & " ELSE '10'                                                                                   " & vbNewLine _
                                            & " END                                                 AS OUTKA_STATE_KB                       " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.OUTKAHOKOKU_YN <> ''                                           " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.OUTKAHOKOKU_YN                                                            " & vbNewLine _
                                            & " WHEN M_CUST.OUTKA_RPT_YN <> '' OR M_CUST.OUTKA_RPT_YN IS NULL                                                 " & vbNewLine _
                                            & " THEN RIGHT(M_CUST.OUTKA_RPT_YN,1)                                                                  " & vbNewLine _
                                            & " ELSE '0'                                                                                    " & vbNewLine _
                                            & " END                                                 AS OUTKAHOKOKU_YN                       " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.PICK_KB <> ''                                                  " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.PICK_KB                                                                   " & vbNewLine _
                                            & " WHEN D_M_DEST.PICK_KB <> '' OR M_CUST.PICK_LIST_KB IS NULL                                                        " & vbNewLine _
                                            & " THEN D_M_DEST.PICK_KB                                                                         " & vbNewLine _
                                            & " WHEN E_M_DEST.PICK_KB <> '' OR M_CUST.PICK_LIST_KB IS NULL                                                        " & vbNewLine _
                                            & " THEN E_M_DEST.PICK_KB                                                                         " & vbNewLine _
                                            & " ELSE '01'                                                                                   " & vbNewLine _
                                            & " END                                                 AS PICK_KB                              " & vbNewLine _
                                            & ",H_OUTKAEDI_L.NRS_BR_NM                              AS NRS_BR_NM	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.WH_CD                                  AS WH_CD	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.WH_NM                                  AS WH_NM	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.OUTKA_PLAN_DATE                        AS OUTKA_PLAN_DATE	                    " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.OUTKO_DATE <> ''                                               " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.OUTKO_DATE                                                                " & vbNewLine _
                                            & " ELSE H_OUTKAEDI_L.OUTKA_PLAN_DATE                                                           " & vbNewLine _
                                            & " END                                                 AS OUTKO_DATE                           " & vbNewLine _
                                            & ",H_OUTKAEDI_L.ARR_PLAN_DATE                          AS ARR_PLAN_DATE	                    " & vbNewLine _
                                            & ",H_OUTKAEDI_L.ARR_PLAN_TIME                          AS ARR_PLAN_TIME	                    " & vbNewLine _
                                            & ",H_OUTKAEDI_L.HOKOKU_DATE                            AS HOKOKU_DATE	                        " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.TOUKI_HOKAN_YN <> ''                                           " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.TOUKI_HOKAN_YN                                                            " & vbNewLine _
                                            & " ELSE '1'                                                                                    " & vbNewLine _
                                            & " END                                                 AS TOUKI_HOKAN_YN                       " & vbNewLine _
                                            & ",H_OUTKAEDI_L.CUST_CD_L                              AS CUST_CD_L	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.CUST_CD_M                              AS CUST_CD_M	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.CUST_NM_L                              AS CUST_NM_L	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.CUST_NM_M                              AS CUST_NM_M	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SHIP_CD_L                              AS SHIP_CD_L	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SHIP_CD_M                              AS SHIP_CD_M	                        " & vbNewLine _
                                            & "-- 2011.10.05修正START 荷送人名称不具合                          " & vbNewLine _
                                            & ",CASE WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) = '01'                                                        " & vbNewLine _
                                            & "      THEN ''                                                                                          " & vbNewLine _
                                            & "      WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND RTRIM(H_OUTKAEDI_L.SHIP_CD_L) = ''                " & vbNewLine _
                                            & "      THEN ''                                                                                          " & vbNewLine _
                                            & "      WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND (RTRIM(H_OUTKAEDI_L.SHIP_CD_L) <> RTRIM(H_OUTKAEDI_L.DEST_CD))  " & vbNewLine _
                                            & "		   AND (RTRIM(S_M_DEST.DEST_NM) <> '' AND S_M_DEST.DEST_NM IS NOT NULL)                   " & vbNewLine _
                                            & "      THEN S_M_DEST.DEST_NM                                                                            " & vbNewLine _
                                            & "      WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND (RTRIM(H_OUTKAEDI_L.SHIP_CD_L) = RTRIM(H_OUTKAEDI_L.DEST_CD))   " & vbNewLine _
                                            & "		   AND (RTRIM(H_OUTKAEDI_L.SHIP_CD_L) = RTRIM(H_OUTKAEDI_L.EDI_DEST_CD))                                " & vbNewLine _
                                            & "		   AND (RTRIM(D_M_DEST.DEST_NM) <> '' AND D_M_DEST.DEST_NM IS NOT NULL)                   " & vbNewLine _
                                            & "      THEN D_M_DEST.DEST_NM                                                                            " & vbNewLine _
                                            & "      WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND (RTRIM(H_OUTKAEDI_L.SHIP_CD_L) = RTRIM(H_OUTKAEDI_L.DEST_CD))   " & vbNewLine _
                                            & "		   AND (RTRIM(H_OUTKAEDI_L.SHIP_CD_L) <> RTRIM(H_OUTKAEDI_L.EDI_DEST_CD))                               " & vbNewLine _
                                            & "		   AND (RTRIM(S_M_DEST.DEST_NM) <> '' AND S_M_DEST.DEST_NM IS NOT NULL)                   " & vbNewLine _
                                            & "      THEN S_M_DEST.DEST_NM                                                                            " & vbNewLine _
                                            & "      ELSE ''                                                                                          " & vbNewLine _
                                            & "      END                                                 AS SHIP_NM_L                                 " & vbNewLine _
                                            & "-- 2011.10.05修正END 荷送人名称不具合                          " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SHIP_NM_M                              AS SHIP_NM_M	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.EDI_DEST_CD                            AS EDI_DEST_CD	                        " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.DEST_CD <> ''                                                  " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.DEST_CD                                                                   " & vbNewLine _
                                            & " ELSE E_M_DEST.DEST_CD                                                                              " & vbNewLine _
                                            & " END                                                 AS DEST_CD                              " & vbNewLine _
                                            & ",H_OUTKAEDI_L.DEST_NM                                AS DEST_NM	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.DEST_ZIP                               AS DEST_ZIP	                            " & vbNewLine _
                                            & "--,CASE WHEN H_OUTKAEDI_L.DEST_CD = ''                                                      " & vbNewLine _
                                            & "--      THEN H_OUTKAEDI_L.DEST_AD_1                                                            " & vbNewLine _
                                            & "--      WHEN H_OUTKAEDI_L.DEST_CD <> ''                                                  " & vbNewLine _
                                            & "--           AND H_OUTKAEDI_L.DEST_AD_1 <> ''                                            " & vbNewLine _
                                            & "--      THEN D_M_DEST.AD_1                                                                        " & vbNewLine _
                                            & "--      ELSE E_M_DEST.AD_1                                                                        " & vbNewLine _
                                            & "-- END                                                 AS DEST_AD_1                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.DEST_AD_1                              AS DEST_AD_1                            " & vbNewLine _
                                            & "--,CASE WHEN H_OUTKAEDI_L.DEST_CD = ''                                                      " & vbNewLine _
                                            & "--      THEN H_OUTKAEDI_L.DEST_AD_2                                                            " & vbNewLine _
                                            & "--      WHEN H_OUTKAEDI_L.DEST_CD <> ''                                                  " & vbNewLine _
                                            & "--           AND H_OUTKAEDI_L.DEST_AD_2 <> ''                                             " & vbNewLine _
                                            & "--      THEN D_M_DEST.AD_2                                                                        " & vbNewLine _
                                            & "--      ELSE E_M_DEST.AD_2                                                                        " & vbNewLine _
                                            & "-- END                                                 AS DEST_AD_2                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.DEST_AD_2                              AS DEST_AD_2                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.DEST_AD_3                              AS DEST_AD_3	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.DEST_AD_4                              AS DEST_AD_4	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.DEST_AD_5                              AS DEST_AD_5	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.DEST_TEL                               AS DEST_TEL	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.DEST_FAX                               AS DEST_FAX	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.DEST_MAIL                              AS DEST_MAIL	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.DEST_JIS_CD                            AS DEST_JIS_CD	                        " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.DEST_CD = ''                                                      " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.SP_NHS_KB                                                                 " & vbNewLine _
                                            & " WHEN D_M_DEST.SP_NHS_KB <> '' OR D_M_DEST.SP_NHS_KB IS NOT NULL                                                           " & vbNewLine _
                                            & " THEN D_M_DEST.SP_NHS_KB                                                                       " & vbNewLine _
                                            & " WHEN E_M_DEST.SP_NHS_KB <> '' OR E_M_DEST.SP_NHS_KB IS NOT NULL                                                           " & vbNewLine _
                                            & " THEN E_M_DEST.SP_NHS_KB                                                                       " & vbNewLine _
                                            & " ELSE H_OUTKAEDI_L.SP_NHS_KB                                                                 " & vbNewLine _
                                            & " END                                                 AS SP_NHS_KB                            " & vbNewLine _
                                            & "--'要望番号:483((出荷登録時)EDI出荷(大,中)の更新不具合のCOA_YN) 2012/06/21 本明 Start        " & vbNewLine _
                                            & "--,CASE WHEN H_OUTKAEDI_L.DEST_CD = ''                                                      " & vbNewLine _
                                            & "-- THEN H_OUTKAEDI_L.COA_YN                                                                    " & vbNewLine _
                                            & "-- WHEN D_M_DEST.COA_YN <> '' OR D_M_DEST.COA_YN IS NOT NULL                                                              " & vbNewLine _
                                            & "-- THEN RIGHT(D_M_DEST.COA_YN,1)                                                                          " & vbNewLine _
                                            & "-- WHEN E_M_DEST.COA_YN <> '' OR E_M_DEST.COA_YN IS NOT NULL                                                              " & vbNewLine _
                                            & "-- THEN RIGHT(E_M_DEST.COA_YN,1)                                                                          " & vbNewLine _
                                            & "-- ELSE H_OUTKAEDI_L.COA_YN                                                                    " & vbNewLine _
                                            & "-- END                                                 AS COA_YN	                            " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.COA_YN <> '' THEN                                                    " & vbNewLine _
                                            & "      --H_OUTKAEDI_L.COA_YNが空でない場合はH_OUTKAEDI_L.COA_YNを採用                         " & vbNewLine _
                                            & "      H_OUTKAEDI_L.COA_YN                                                                    " & vbNewLine _
                                            & " ELSE                                                                                        " & vbNewLine _
                                            & "      --H_OUTKAEDI_L.COA_YNが空の場合                                                        " & vbNewLine _
                                            & "      CASE WHEN ISNULL(D_M_DEST.COA_YN,'') <> '' THEN                                        " & vbNewLine _
                                            & "           --D_M_DEST.COA_YNが空でない場合はD_M_DEST.COA_YNを採用                            " & vbNewLine _
                                            & "           RIGHT(D_M_DEST.COA_YN,1)                                                          " & vbNewLine _
                                            & "      ELSE                                                                                   " & vbNewLine _
                                            & "           --D_M_DEST.COA_YNが空の場合                                                       " & vbNewLine _
                                            & "           CASE WHEN   ISNULL(E_M_DEST.COA_YN,'') <> '' THEN                                 " & vbNewLine _
                                            & "                --E_M_DEST.COA_YNが空でない場合はE_M_DEST.COA_YNを採用                       " & vbNewLine _
                                            & "                RIGHT(E_M_DEST.COA_YN,1)                                                     " & vbNewLine _
                                            & "           ELSE                                                                              " & vbNewLine _
                                            & "                --E_M_DEST.COA_YNが空の場合は既定値を採用（サクラの場合は''？）              " & vbNewLine _
                                            & "                ''                                                                           " & vbNewLine _
                                            & "           END                                                                               " & vbNewLine _
                                            & "      END                                                                                    " & vbNewLine _
                                            & " END                                                 AS COA_YN                               " & vbNewLine _
                                            & "--'要望番号:483((出荷登録時)EDI出荷(大,中)の更新不具合のCOA_YN) 2012/06/21 本明 End          " & vbNewLine _
                                            & ",H_OUTKAEDI_L.CUST_ORD_NO                            AS CUST_ORD_NO	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.BUYER_ORD_NO                           AS BUYER_ORD_NO	                        " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.UNSO_MOTO_KB <> ''                                             " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.UNSO_MOTO_KB                                                              " & vbNewLine _
                                            & " WHEN RTRIM(M_CUST.UNSO_TEHAI_KB) <> '' OR M_CUST.UNSO_TEHAI_KB IS NOT NULL                  " & vbNewLine _
                                            & " THEN M_CUST.UNSO_TEHAI_KB                                                                   " & vbNewLine _
                                            & " ELSE '90'                                                                                   " & vbNewLine _
                                            & " END                                                 AS UNSO_MOTO_KB                         " & vbNewLine _
                                            & "-- 2011.10.05修正START EDI(メモ)№60                          " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.UNSO_TEHAI_KB <> ''                                                                              " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.UNSO_TEHAI_KB                                                                                         " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) = '01'                                                                               " & vbNewLine _
                                            & "      AND RTRIM(C_M_UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB) <>'' AND C_M_UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB IS NOT NULL      " & vbNewLine _
                                            & " THEN C_M_UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB                                                                             " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND RTRIM(H_OUTKAEDI_L.FREE_C29) = ''                                        " & vbNewLine _
                                            & "      AND RTRIM(D_M_UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB) <>'' AND D_M_UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB IS NOT NULL      " & vbNewLine _
                                            & " THEN D_M_UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB                                                                             " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND LEFT(H_OUTKAEDI_L.FREE_C29,1) = '0'                                      " & vbNewLine _
                                            & "      AND RTRIM(C_M_UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB) <>'' AND C_M_UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB IS NOT NULL      " & vbNewLine _
                                            & " THEN C_M_UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB                                                                             " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND LEFT(H_OUTKAEDI_L.FREE_C29,1) = '1'                                      " & vbNewLine _
                                            & "      AND RTRIM(D_M_UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB) <>'' AND D_M_UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB IS NOT NULL      " & vbNewLine _
                                            & " THEN D_M_UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB                                                                             " & vbNewLine _
                                            & " ELSE '10'                                                                                                               " & vbNewLine _
                                            & " END                                                 AS UNSO_TEHAI_KB                                                    " & vbNewLine _
                                            & "-- 2011.10.05修正END EDI(メモ)№60                          " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYARYO_KB                              AS SYARYO_KB	                        " & vbNewLine _
                                            & ",CASE WHEN RTRIM(H_OUTKAEDI_L.BIN_KB) <> ''                                                   " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.BIN_KB                                                                    " & vbNewLine _
                                            & " WHEN RTRIM(D_M_DEST.BIN_KB) <> '' AND D_M_DEST.BIN_KB IS NOT NULL                            " & vbNewLine _
                                            & " THEN D_M_DEST.BIN_KB                                                                        " & vbNewLine _
                                            & " WHEN RTRIM(E_M_DEST.BIN_KB) <> '' AND E_M_DEST.BIN_KB IS NOT NULL                            " & vbNewLine _
                                            & " THEN E_M_DEST.BIN_KB                                                                        " & vbNewLine _
                                            & " ELSE '01'                                                                                   " & vbNewLine _
                                            & " END                                                 AS BIN_KB                               " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.UNSO_CD <> ''                                                  " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.UNSO_CD                                                                   " & vbNewLine _
                                            & "      WHEN H_OUTKAEDI_L.UNSO_CD = '' AND (D_M_DEST.SP_UNSO_CD <> '' OR D_M_DEST.SP_UNSO_CD IS NOT NULL)                  " & vbNewLine _
                                            & " THEN D_M_DEST.SP_UNSO_CD                                                                    " & vbNewLine _
                                            & "      WHEN H_OUTKAEDI_L.UNSO_CD = '' AND (E_M_DEST.SP_UNSO_CD <> '' OR E_M_DEST.SP_UNSO_CD IS NOT NULL)                  " & vbNewLine _
                                            & " THEN E_M_DEST.SP_UNSO_CD                                                                    " & vbNewLine _
                                            & " ELSE M_CUST.SP_UNSO_CD                                                                      " & vbNewLine _
                                            & " END                                                 AS UNSO_CD                              " & vbNewLine _
                                            & ",H_OUTKAEDI_L.UNSO_NM                                AS UNSO_NM	                            " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.UNSO_BR_CD <> ''                                               " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.UNSO_BR_CD                                                                " & vbNewLine _
                                            & "      WHEN H_OUTKAEDI_L.UNSO_BR_CD = '' AND (D_M_DEST.SP_UNSO_BR_CD <> '' OR D_M_DEST.SP_UNSO_BR_CD IS NOT NULL)            " & vbNewLine _
                                            & " THEN D_M_DEST.SP_UNSO_BR_CD                                                                    " & vbNewLine _
                                            & "      WHEN H_OUTKAEDI_L.UNSO_BR_CD = '' AND (E_M_DEST.SP_UNSO_BR_CD <> '' OR E_M_DEST.SP_UNSO_BR_CD IS NOT NULL)            " & vbNewLine _
                                            & " THEN E_M_DEST.SP_UNSO_BR_CD                                                                    " & vbNewLine _
                                            & " ELSE M_CUST.SP_UNSO_BR_CD                                                                   " & vbNewLine _
                                            & " END                                                 AS UNSO_BR_CD                           " & vbNewLine _
                                            & ",H_OUTKAEDI_L.UNSO_BR_NM                             AS UNSO_BR_NM	                        " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.UNCHIN_TARIFF_CD <> ''                                         " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.UNCHIN_TARIFF_CD                                                          " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) = '01' AND H_OUTKAEDI_L.UNSO_TEHAI_KB = '10'               " & vbNewLine _
                                            & " THEN C_M_UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD1                                                  " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) = '01' AND H_OUTKAEDI_L.UNSO_TEHAI_KB = '20'               " & vbNewLine _
                                            & " THEN C_M_UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD2                                                  " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) = '01' AND H_OUTKAEDI_L.UNSO_TEHAI_KB = '40'               " & vbNewLine _
                                            & " THEN C_M_UNCHIN_TARIFF_SET.YOKO_TARIFF_CD                                                     " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND H_OUTKAEDI_L.UNCHIN_TARIFF_CD = ''          " & vbNewLine _
                                            & "      AND H_OUTKAEDI_L.FREE_C29 = '' AND H_OUTKAEDI_L.UNSO_TEHAI_KB = '10'                " & vbNewLine _
                                            & "      THEN D_M_UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD1                                             " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND H_OUTKAEDI_L.UNCHIN_TARIFF_CD = ''          " & vbNewLine _
                                            & "      AND H_OUTKAEDI_L.FREE_C29 = '' AND H_OUTKAEDI_L.UNSO_TEHAI_KB = '20'                " & vbNewLine _
                                            & " THEN D_M_UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD2                                                  " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND H_OUTKAEDI_L.UNCHIN_TARIFF_CD = ''          " & vbNewLine _
                                            & "      AND H_OUTKAEDI_L.FREE_C29 = '' AND H_OUTKAEDI_L.UNSO_TEHAI_KB = '40'                " & vbNewLine _
                                            & "      THEN D_M_UNCHIN_TARIFF_SET.YOKO_TARIFF_CD                                                " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND H_OUTKAEDI_L.UNCHIN_TARIFF_CD = ''          " & vbNewLine _
                                            & "      AND LEFT(H_OUTKAEDI_L.FREE_C29,1) = '0' AND H_OUTKAEDI_L.UNSO_TEHAI_KB = '10'        " & vbNewLine _
                                            & " THEN C_M_UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD1                                                  " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND H_OUTKAEDI_L.UNCHIN_TARIFF_CD = ''          " & vbNewLine _
                                            & "      AND LEFT(H_OUTKAEDI_L.FREE_C29,1) = '0' AND H_OUTKAEDI_L.UNSO_TEHAI_KB = '20'        " & vbNewLine _
                                            & " THEN C_M_UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD2                                                  " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND H_OUTKAEDI_L.UNCHIN_TARIFF_CD = ''          " & vbNewLine _
                                            & "      AND LEFT(H_OUTKAEDI_L.FREE_C29,1) = '0' AND H_OUTKAEDI_L.UNSO_TEHAI_KB = '40'        " & vbNewLine _
                                            & " THEN C_M_UNCHIN_TARIFF_SET.YOKO_TARIFF_CD                                                     " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND H_OUTKAEDI_L.UNCHIN_TARIFF_CD = ''          " & vbNewLine _
                                            & "      AND LEFT(H_OUTKAEDI_L.FREE_C29,1) = '1' AND H_OUTKAEDI_L.UNSO_TEHAI_KB = '10'        " & vbNewLine _
                                            & " THEN D_M_UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD1                                                  " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND H_OUTKAEDI_L.UNCHIN_TARIFF_CD = ''          " & vbNewLine _
                                            & "      AND LEFT(H_OUTKAEDI_L.FREE_C29,1) = '1' AND H_OUTKAEDI_L.UNSO_TEHAI_KB = '20'        " & vbNewLine _
                                            & " THEN D_M_UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD2                                                  " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND H_OUTKAEDI_L.UNCHIN_TARIFF_CD = ''          " & vbNewLine _
                                            & "      AND LEFT(H_OUTKAEDI_L.FREE_C29,1) = '1' AND H_OUTKAEDI_L.UNSO_TEHAI_KB = '40'        " & vbNewLine _
                                            & " THEN D_M_UNCHIN_TARIFF_SET.YOKO_TARIFF_CD                                                     " & vbNewLine _
                                            & " ELSE NULL                                                                                   " & vbNewLine _
                                            & " END                                                 AS UNCHIN_TARIFF_CD                     " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.EXTC_TARIFF_CD <> ''                                           " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.EXTC_TARIFF_CD                                                            " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) = '01'                                                     " & vbNewLine _
                                            & " THEN C_M_UNCHIN_TARIFF_SET.EXTC_TARIFF_CD                                                  " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND H_OUTKAEDI_L.EXTC_TARIFF_CD = ''            " & vbNewLine _
                                            & "      AND LEFT(H_OUTKAEDI_L.FREE_C29,1) = ''                                            " & vbNewLine _
                                            & " THEN D_M_UNCHIN_TARIFF_SET.EXTC_TARIFF_CD                                                     " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND H_OUTKAEDI_L.EXTC_TARIFF_CD = ''            " & vbNewLine _
                                            & "      AND LEFT(H_OUTKAEDI_L.FREE_C29,1) = '0'                                              " & vbNewLine _
                                            & " THEN D_M_UNCHIN_TARIFF_SET.EXTC_TARIFF_CD                                                     " & vbNewLine _
                                            & " WHEN LEFT(H_OUTKAEDI_L.FREE_C30,2) <> '01' AND H_OUTKAEDI_L.EXTC_TARIFF_CD = ''            " & vbNewLine _
                                            & "      AND LEFT(H_OUTKAEDI_L.FREE_C29,1) = '1'                                              " & vbNewLine _
                                            & " THEN D_M_UNCHIN_TARIFF_SET.EXTC_TARIFF_CD                                                  " & vbNewLine _
                                            & " ELSE NULL                                                                                   " & vbNewLine _
                                            & " END                                                 AS EXTC_TARIFF_CD                       " & vbNewLine _
                                            & ",H_OUTKAEDI_L.REMARK                                 AS REMARK	                            " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.DEST_CD = ''                                                      " & vbNewLine _
                                            & "      THEN H_OUTKAEDI_L.UNSO_ATT                                                             " & vbNewLine _
                                            & "      WHEN H_OUTKAEDI_L.DEST_CD <> '' AND H_OUTKAEDI_L.UNSO_ATT = '' AND (D_M_DEST.DELI_ATT <> '' OR D_M_DEST.DELI_ATT IS NOT NULL)              " & vbNewLine _
                                            & "      THEN D_M_DEST.DELI_ATT                                                                   " & vbNewLine _
                                            & "      WHEN H_OUTKAEDI_L.DEST_CD <> '' AND H_OUTKAEDI_L.UNSO_ATT = '' AND (E_M_DEST.DELI_ATT <> '' OR E_M_DEST.DELI_ATT IS NOT NULL)              " & vbNewLine _
                                            & "      THEN E_M_DEST.DELI_ATT                                                                   " & vbNewLine _
                                            & "--'要望番号:601(出荷登録時の配送時注意事項の設定不具合) 2012/06/22 本明 Start        				" & vbNewLine _
                                            & "--    WHEN H_OUTKAEDI_L.DEST_CD <> '' AND H_OUTKAEDI_L.UNSO_ATT = D_M_DEST.DELI_ATT      			" & vbNewLine _
                                            & "      WHEN H_OUTKAEDI_L.DEST_CD <> '' AND CHARINDEX(D_M_DEST.DELI_ATT, H_OUTKAEDI_L.UNSO_ATT) <> 0  	" & vbNewLine _
                                            & "--'要望番号:601(出荷登録時の配送時注意事項の設定不具合) 2012/06/22 本明 End        					" & vbNewLine _
                                            & "      THEN H_OUTKAEDI_L.UNSO_ATT                                                             " & vbNewLine _
                                            & "--'要望番号:601(出荷登録時の配送時注意事項の設定不具合) 2012/06/22 本明 Start        				" & vbNewLine _
                                            & "--    WHEN H_OUTKAEDI_L.DEST_CD <> '' AND H_OUTKAEDI_L.UNSO_ATT = E_M_DEST.DELI_ATT      			" & vbNewLine _
                                            & "      WHEN H_OUTKAEDI_L.DEST_CD <> '' AND CHARINDEX(E_M_DEST.DELI_ATT, H_OUTKAEDI_L.UNSO_ATT) <> 0  	" & vbNewLine _
                                            & "--'要望番号:601(出荷登録時の配送時注意事項の設定不具合) 2012/06/22 本明 End        					" & vbNewLine _
                                            & "      THEN H_OUTKAEDI_L.UNSO_ATT                                                             " & vbNewLine _
                                            & " ELSE LEFT(H_OUTKAEDI_L.UNSO_ATT + Space(2) + D_M_DEST.DELI_ATT,100)                         " & vbNewLine _
                                            & " END                                                 AS UNSO_ATT                             " & vbNewLine _
                                            & "--'要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start    " & vbNewLine _
                                            & "--,CASE WHEN H_OUTKAEDI_L.DENP_YN <> ''                                                              " & vbNewLine _
                                            & "-- THEN H_OUTKAEDI_L.DENP_YN                                                                         " & vbNewLine _
                                            & "-- ELSE '1'                                                                                          " & vbNewLine _
                                            & "-- END                                                 AS DENP_YN                                    " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.DENP_YN <> '' THEN                                                           " & vbNewLine _
                                            & "     H_OUTKAEDI_L.DENP_YN                                                                            " & vbNewLine _
                                            & " ELSE                                                                                                " & vbNewLine _
                                            & "     CASE WHEN ISNULL(M_UNSO_CUST_RPT.UNSOCO_CD,'') = '' THEN                                        " & vbNewLine _
                                            & "         '0'     --M_UNSO_CUST_RPTに存在しない場合                                                   " & vbNewLine _
                                            & "     ELSE                                                                                            " & vbNewLine _
                                            & "         '1'     --M_UNSO_CUST_RPTに存在した場合                                                     " & vbNewLine _
                                            & "     END                                                                                             " & vbNewLine _
                                            & " END                                                 AS DENP_YN                                      " & vbNewLine _
                                            & "--'要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 End      " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.PC_KB <> ''                                                    " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.PC_KB                                                                     " & vbNewLine _
                                            & " ELSE '01'                                                                                   " & vbNewLine _
                                            & " END                                                 AS PC_KB                                " & vbNewLine _
                                            & ",H_OUTKAEDI_L.UNCHIN_YN                              AS UNCHIN_YN	                        " & vbNewLine _
                                            & ",CASE WHEN H_OUTKAEDI_L.NIYAKU_YN <> ''                                                " & vbNewLine _
                                            & " THEN H_OUTKAEDI_L.NIYAKU_YN                                                                 " & vbNewLine _
                                            & " ELSE '1'                                                                                    " & vbNewLine _
                                            & " END                                                 AS NIYAKU_YN                            " & vbNewLine _
                                            & ",'1'                           AS OUT_FLAG	                                                " & vbNewLine _
                                            & ",H_OUTKAEDI_L.AKAKURO_KB                             AS AKAKURO_KB	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.JISSEKI_FLAG                           AS JISSEKI_FLAG	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.JISSEKI_USER                           AS JISSEKI_USER	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.JISSEKI_DATE                           AS JISSEKI_DATE	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.JISSEKI_TIME                           AS JISSEKI_TIME	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_N01                               AS FREE_N01	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_N02                               AS FREE_N02	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_N03                               AS FREE_N03	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_N04                               AS FREE_N04	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_N05                               AS FREE_N05	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_N06                               AS FREE_N06	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_N07                               AS FREE_N07	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_N08                               AS FREE_N08	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_N09                               AS FREE_N09	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_N10                               AS FREE_N10	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C01                               AS FREE_C01	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C02                               AS FREE_C02	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C03                               AS FREE_C03	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C04                               AS FREE_C04	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C05                               AS FREE_C05	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C06                               AS FREE_C06	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C07                               AS FREE_C07	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C08                               AS FREE_C08	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C09                               AS FREE_C09	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C10                               AS FREE_C10	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C11                               AS FREE_C11	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C12                               AS FREE_C12	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C13                               AS FREE_C13	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C14                               AS FREE_C14	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C15                               AS FREE_C15	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C16                               AS FREE_C16	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C17                               AS FREE_C17	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C18                               AS FREE_C18	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C19                               AS FREE_C19	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C20                               AS FREE_C20	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C21                               AS FREE_C21	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C22                               AS FREE_C22	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C23                               AS FREE_C23	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C24                               AS FREE_C24	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C25                               AS FREE_C25	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C26                               AS FREE_C26	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C27                               AS FREE_C27	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C28                               AS FREE_C28	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C29                               AS FREE_C29	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.FREE_C30                               AS FREE_C30	                            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.CRT_USER                               AS CRT_USER    	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.CRT_DATE                               AS CRT_DATE    	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.CRT_TIME                               AS CRT_TIME    	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.UPD_USER                               AS UPD_USER    	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.UPD_DATE                               AS UPD_DATE    	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.UPD_TIME                               AS UPD_TIME    	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SCM_CTL_NO_L                           AS SCM_CTL_NO_L	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.EDIT_FLAG                              AS EDIT_FLAG	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.MATCHING_FLAG                          AS MATCHING_FLAG	                    " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_ENT_DATE                           AS SYS_ENT_DATE	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_ENT_TIME                           AS SYS_ENT_TIME	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_ENT_PGID                           AS SYS_ENT_PGID	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_ENT_USER                           AS SYS_ENT_USER	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_UPD_DATE                           AS SYS_UPD_DATE	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_UPD_TIME                           AS SYS_UPD_TIME	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_UPD_PGID                           AS SYS_UPD_PGID	                        " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_UPD_USER                           AS SYS_UPD_USER	                        " & vbNewLine _
                                            & ",'0'                                                 AS SYS_DEL_FLG	                        " & vbNewLine

#End Region

#Region "H_OUTKAEDI_L FROM句"
    ''' <summary>
    ''' 出荷登録用SQLFROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const L_DEF_SQL_FROM As String = "FROM                                                          " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                                           " & vbNewLine _
                                        & "LEFT JOIN                                                        " & vbNewLine _
                                        & "$LM_MST$..M_DEST    D_M_DEST                                     " & vbNewLine _
                                        & "ON                                                               " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = D_M_DEST.NRS_BR_CD                        " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = D_M_DEST.CUST_CD_L                        " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.DEST_CD = D_M_DEST.DEST_CD                            " & vbNewLine _
                                        & "LEFT JOIN                                                        " & vbNewLine _
                                        & "$LM_MST$..M_DEST    E_M_DEST                                     " & vbNewLine _
                                        & "ON                                                               " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = E_M_DEST.NRS_BR_CD                        " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = E_M_DEST.CUST_CD_L                        " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_DEST_CD = E_M_DEST.EDI_CD                         " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "E_M_DEST.EDI_CD <> ''                        " & vbNewLine _
                                        & "-- 2011.10.05追加START 荷送人名称不具合                          " & vbNewLine _
                                        & "LEFT JOIN                                                        " & vbNewLine _
                                        & "$LM_MST$..M_DEST    S_M_DEST                                     " & vbNewLine _
                                        & "ON                                                               " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = S_M_DEST.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = S_M_DEST.CUST_CD_L                      " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.SHIP_CD_L = S_M_DEST.DEST_CD                        " & vbNewLine _
                                        & "-- 2011.10.05追加END                                             " & vbNewLine _
                                        & "LEFT JOIN                                                        " & vbNewLine _
                                        & "$LM_MST$..M_CUST                                                 " & vbNewLine _
                                        & "ON                                                               " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_CUST.NRS_BR_CD                        " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_CUST.CUST_CD_L                        " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_CUST.CUST_CD_M                        " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "M_CUST.CUST_CD_S = '00'                        " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "M_CUST.CUST_CD_SS = '00'                        " & vbNewLine _
                                        & "LEFT JOIN                                                        " & vbNewLine _
                                        & "$LM_MST$..M_UNSOCO   M_UNSOCO                                    " & vbNewLine _
                                        & "ON                                                               " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                        " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                          " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                    " & vbNewLine _
                                        & "--'要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start    " & vbNewLine _
                                        & "LEFT JOIN                                                                                            " & vbNewLine _
                                        & "--'要望番号:1152(通常JOINだと複数件取得する為サブクエリを使用) 2012/07/06 馬野 Start    " & vbNewLine _
                                        & "(SELECT                                                                                 " & vbNewLine _
                                        & " NRS_BR_CD                                                                              " & vbNewLine _
                                        & ",UNSOCO_CD                                                                              " & vbNewLine _
                                        & ",UNSOCO_BR_CD                                                                           " & vbNewLine _
                                        & ",MOTO_TYAKU_KB                                                                          " & vbNewLine _
                                        & ",SYS_DEL_FLG                                                                            " & vbNewLine _
                                        & " FROM $LM_MST$..M_UNSO_CUST_RPT                                                         " & vbNewLine _
                                        & " WHERE                                                                                  " & vbNewLine _
                                        & " NRS_BR_CD = @NRS_BR_CD                                                                 " & vbNewLine _
                                        & " GROUP BY                                                                               " & vbNewLine _
                                        & " NRS_BR_CD                                                                              " & vbNewLine _
                                        & ",UNSOCO_CD                                                                              " & vbNewLine _
                                        & ",UNSOCO_BR_CD                                                                           " & vbNewLine _
                                        & ",MOTO_TYAKU_KB                                                                          " & vbNewLine _
                                        & ",SYS_DEL_FLG                                                                            " & vbNewLine _
                                        & ") M_UNSO_CUST_RPT                                                                       " & vbNewLine _
                                        & "--'要望番号:1152(通常JOINだと複数件取得する為サブクエリを使用) 2012/07/06 馬野 End    " & vbNewLine _
                                        & "--$LM_MST$..M_UNSO_CUST_RPT   M_UNSO_CUST_RPT                                                          " & vbNewLine _
                                        & "ON                                                                                                   " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_UNSO_CUST_RPT.NRS_BR_CD                                                   " & vbNewLine _
                                        & "AND                                                                                                  " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_CD = M_UNSO_CUST_RPT.UNSOCO_CD                                                     " & vbNewLine _
                                        & "AND                                                                                                  " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_BR_CD = M_UNSO_CUST_RPT.UNSOCO_BR_CD                                               " & vbNewLine _
                                        & "AND                                                                                                  " & vbNewLine _
                                        & "M_UNSO_CUST_RPT.SYS_DEL_FLG  = '0'                                                                   " & vbNewLine _
                                        & "AND                                                                                                  " & vbNewLine _
                                        & "M_UNSO_CUST_RPT.MOTO_TYAKU_KB  = '01'                                                                " & vbNewLine _
                                        & "--'要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 End      " & vbNewLine _
                                        & "LEFT JOIN                                                        " & vbNewLine _
                                        & "$LM_MST$..M_UNCHIN_TARIFF_SET  C_M_UNCHIN_TARIFF_SET             " & vbNewLine _
                                        & "ON                                                               " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = C_M_UNCHIN_TARIFF_SET.NRS_BR_CD           " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = C_M_UNCHIN_TARIFF_SET.CUST_CD_L           " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = C_M_UNCHIN_TARIFF_SET.CUST_CD_M           " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "C_M_UNCHIN_TARIFF_SET.SET_KB = '00'                              " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "C_M_UNCHIN_TARIFF_SET.DEST_CD = ''                              " & vbNewLine _
                                        & "LEFT JOIN                                                        " & vbNewLine _
                                        & "$LM_MST$..M_UNCHIN_TARIFF_SET   D_M_UNCHIN_TARIFF_SET            " & vbNewLine _
                                        & "ON                                                               " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = D_M_UNCHIN_TARIFF_SET.NRS_BR_CD           " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = D_M_UNCHIN_TARIFF_SET.CUST_CD_L           " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = D_M_UNCHIN_TARIFF_SET.CUST_CD_M           " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "H_OUTKAEDI_L.DEST_CD = D_M_UNCHIN_TARIFF_SET.DEST_CD               " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "D_M_UNCHIN_TARIFF_SET.SET_KB = '01'                              " & vbNewLine _
                                        & "WHERE                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_DEL_FLG  = '0'                                  " & vbNewLine _
                                        & " AND H_OUTKAEDI_L.NRS_BR_CD         = @NRS_BR_CD                              " & vbNewLine _
                                        & " AND H_OUTKAEDI_L.EDI_CTL_NO         = @EDI_CTL_NO                        " & vbNewLine _
                                        & " AND H_OUTKAEDI_L.SYS_UPD_DATE  = @SYS_UPD_DATE                               " & vbNewLine _
                                        & " AND H_OUTKAEDI_L.SYS_UPD_TIME  = @SYS_UPD_TIME                               " & vbNewLine

#End Region
    '2011.11.07 要望番号391 CHUUI_NARRの追加
#Region "H_OUTKAEDI_M SELECT句"
    ''' <summary>
    ''' H_OUTKAEDI_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>

    Private Const M_DEF_SQL_SELECT_DATA As String = " SELECT                                                                            " & vbNewLine _
                                        & " '0'                                                 AS DEL_KB                               " & vbNewLine _
                                        & ",H_OUTKAEDI_M.NRS_BR_CD                              AS  NRS_BR_CD                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                             AS  EDI_CTL_NO                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO_CHU                         AS  EDI_CTL_NO_CHU                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_CTL_NO                              AS  OUTKA_CTL_NO                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_CTL_NO_CHU                              AS  OUTKA_CTL_NO_CHU                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.COA_YN                              AS  COA_YN                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CUST_ORD_NO_DTL                              AS  CUST_ORD_NO_DTL                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.BUYER_ORD_NO_DTL                              AS  BUYER_ORD_NO_DTL                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CUST_GOODS_CD                              AS  CUST_GOODS_CD                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.NRS_GOODS_CD                              AS  NRS_GOODS_CD                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.GOODS_NM                              AS  GOODS_NM                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.RSV_NO                              AS  RSV_NO                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.LOT_NO                              AS  LOT_NO                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SERIAL_NO                              AS  SERIAL_NO                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.ALCTD_KB                              AS  ALCTD_KB                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_PKG_NB                              AS  OUTKA_PKG_NB                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_HASU                              AS  OUTKA_HASU                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_QT                              AS  OUTKA_QT                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_TTL_NB                              AS  OUTKA_TTL_NB                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_TTL_QT                              AS  OUTKA_TTL_QT                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.KB_UT                              AS  KB_UT                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.QT_UT                              AS  QT_UT                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.PKG_NB                              AS  PKG_NB                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.PKG_UT                              AS  PKG_UT                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.ONDO_KB                              AS  ONDO_KB                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.UNSO_ONDO_KB                              AS  UNSO_ONDO_KB                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.IRIME                              AS  IRIME                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.IRIME_UT                              AS  IRIME_UT                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.BETU_WT                              AS  BETU_WT                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.REMARK                              AS  REMARK                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUT_KB                              AS  OUT_KB                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.AKAKURO_KB                              AS  AKAKURO_KB                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.JISSEKI_FLAG                              AS  JISSEKI_FLAG                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.JISSEKI_USER                              AS  JISSEKI_USER                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.JISSEKI_DATE                              AS  JISSEKI_DATE                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.JISSEKI_TIME                              AS  JISSEKI_TIME                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SET_KB                              AS  SET_KB                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N01                              AS  FREE_N01                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N02                              AS  FREE_N02                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N03                              AS  FREE_N03                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N04                              AS  FREE_N04                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N05                              AS  FREE_N05                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N06                              AS  FREE_N06                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N07                              AS  FREE_N07                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N08                              AS  FREE_N08                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N09                              AS  FREE_N09                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N10                              AS  FREE_N10                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C01                              AS  FREE_C01                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C02                              AS  FREE_C02                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C03                              AS  FREE_C03                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C04                              AS  FREE_C04                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C05                              AS  FREE_C05                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C06                              AS  FREE_C06                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C07                              AS  FREE_C07                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C08                              AS  FREE_C08                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C09                              AS  FREE_C09                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C10                              AS  FREE_C10                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C11                              AS  FREE_C11                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C12                              AS  FREE_C12                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C13                              AS  FREE_C13                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C14                              AS  FREE_C14                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C15                              AS  FREE_C15                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C16                              AS  FREE_C16                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C17                              AS  FREE_C17                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C18                              AS  FREE_C18                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C19                              AS  FREE_C19                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C20                              AS  FREE_C20                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C21                              AS  FREE_C21                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C22                              AS  FREE_C22                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C23                              AS  FREE_C23                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C24                              AS  FREE_C24                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C25                              AS  FREE_C25                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C26                              AS  FREE_C26                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C27                              AS  FREE_C27                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C28                              AS  FREE_C28                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C29                              AS  FREE_C29                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C30                              AS  FREE_C30                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CRT_USER                              AS  CRT_USER                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CRT_DATE                              AS  CRT_DATE                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CRT_TIME                              AS  CRT_TIME                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.UPD_USER                              AS  UPD_USER                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.UPD_DATE                              AS  UPD_DATE                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.UPD_TIME                              AS  UPD_TIME                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SCM_CTL_NO_L                              AS  SCM_CTL_NO_L                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SCM_CTL_NO_M                              AS  SCM_CTL_NO_M                           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_ENT_DATE                           AS SYS_ENT_DATE	                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_ENT_TIME                           AS SYS_ENT_TIME	                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_ENT_PGID                           AS SYS_ENT_PGID	                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_ENT_USER                           AS SYS_ENT_USER	                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_UPD_DATE                           AS SYS_UPD_DATE	                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_UPD_TIME                           AS SYS_UPD_TIME	                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_UPD_PGID                           AS SYS_UPD_PGID	                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_UPD_USER                           AS SYS_UPD_USER	                        " & vbNewLine _
                                        & ",'0'                                                 AS SYS_DEL_FLG	                        " & vbNewLine _
                                        & ",M_DESTGOODS.SAGYO_KB_1                              AS SAGYO_KB_1	                        " & vbNewLine _
                                        & ",M_DESTGOODS.SAGYO_KB_2                              AS SAGYO_KB_2	                        " & vbNewLine _
                                        & ",H_OUTKAEDI_DTL_SFJ.CHUUI_NARR                       AS CHUUI_NARR	                        " & vbNewLine

#End Region
    '2011.11.07 要望番号391 CHUUI_NARRの追加END

    '2011.11.07 要望番号391 H_OUTKAEDI_DTL_SFJの追加START
#Region "H_OUTKAEDI_M FROM句"

    Private Const M_DEF_SQL_FROM As String = "FROM                                                      " & vbNewLine _
                                    & "$LM_TRN$..H_OUTKAEDI_M                                           " & vbNewLine _
                                    & "INNER JOIN                                                       " & vbNewLine _
                                    & "$LM_TRN$..H_OUTKAEDI_L    H_OUTKAEDI_L                           " & vbNewLine _
                                    & "ON                                                               " & vbNewLine _
                                    & "H_OUTKAEDI_M.NRS_BR_CD = H_OUTKAEDI_L.NRS_BR_CD                  " & vbNewLine _
                                    & "AND                                                              " & vbNewLine _
                                    & "H_OUTKAEDI_M.EDI_CTL_NO = H_OUTKAEDI_L.EDI_CTL_NO                " & vbNewLine _
                                    & "INNER JOIN                                                       " & vbNewLine _
                                    & "$LM_TRN$..H_OUTKAEDI_DTL_SFJ    H_OUTKAEDI_DTL_SFJ               " & vbNewLine _
                                    & "ON                                                               " & vbNewLine _
                                    & "H_OUTKAEDI_M.NRS_BR_CD = H_OUTKAEDI_DTL_SFJ.NRS_BR_CD            " & vbNewLine _
                                    & "AND                                                              " & vbNewLine _
                                    & "H_OUTKAEDI_M.EDI_CTL_NO = H_OUTKAEDI_DTL_SFJ.EDI_CTL_NO          " & vbNewLine _
                                    & "AND                                                              " & vbNewLine _
                                    & "H_OUTKAEDI_M.EDI_CTL_NO_CHU = H_OUTKAEDI_DTL_SFJ.EDI_CTL_NO_CHU  " & vbNewLine _
                                    & "LEFT JOIN                                                        " & vbNewLine _
                                    & "$LM_MST$..M_DESTGOODS    M_DESTGOODS                             " & vbNewLine _
                                    & "ON                                                               " & vbNewLine _
                                    & "H_OUTKAEDI_M.NRS_BR_CD = M_DESTGOODS.NRS_BR_CD                   " & vbNewLine _
                                    & "AND                                                              " & vbNewLine _
                                    & "H_OUTKAEDI_M.NRS_GOODS_CD = M_DESTGOODS.GOODS_CD_NRS             " & vbNewLine _
                                    & "AND                                                              " & vbNewLine _
                                    & "H_OUTKAEDI_L.CUST_CD_L = M_DESTGOODS.CUST_CD_L                   " & vbNewLine _
                                    & "AND                                                              " & vbNewLine _
                                    & "H_OUTKAEDI_L.CUST_CD_M = M_DESTGOODS.CUST_CD_M                   " & vbNewLine _
                                    & "AND                                                              " & vbNewLine _
                                    & "H_OUTKAEDI_L.DEST_CD = M_DESTGOODS.CD                            " & vbNewLine _
                                    & "WHERE                                                            " & vbNewLine _
                                    & "H_OUTKAEDI_L.SYS_DEL_FLG  = '0'                                  " & vbNewLine _
                                    & " AND H_OUTKAEDI_M.SYS_DEL_FLG   = '0'                            " & vbNewLine _
                                    & " AND H_OUTKAEDI_M.OUT_KB    = '0'                            " & vbNewLine _
                                    & " AND H_OUTKAEDI_DTL_SFJ.SYS_DEL_FLG   = '0'                      " & vbNewLine _
                                    & " AND H_OUTKAEDI_L.NRS_BR_CD     = @NRS_BR_CD                     " & vbNewLine _
                                    & " AND H_OUTKAEDI_L.EDI_CTL_NO    = @EDI_CTL_NO                    " & vbNewLine _
                                    & " AND H_OUTKAEDI_L.SYS_UPD_DATE  = @SYS_UPD_DATE                  " & vbNewLine _
                                    & " AND H_OUTKAEDI_L.SYS_UPD_TIME  = @SYS_UPD_TIME                  " & vbNewLine
#End Region
    '2011.11.07 要望番号391 H_OUTKAEDI_DTL_SFJの追加END
#Region "制御用"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#End Region

#Region "出荷登録処理　存在チェックSQL"

#Region "SELECT_M_DEST"

    Private Const SQL_SELECT_M_DEST As String = " SELECT                                       " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_DEST                       M_DEST         " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_DEST.NRS_BR_CD   = @NRS_BR_CD                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_DEST.CUST_CD_L   = @CUST_CD_L                       " & vbNewLine

#End Region

#End Region

#Region "出荷登録処理 サクラまとめ用SQL"

    ''' <summary>
    ''' 選択データがサクラまとめ対象かのチェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MATOME_TARGET_CHECK As String = "FROM                                                 " & vbNewLine _
                                                 & "$LM_TRN$..H_OUTKAEDI_L                                  " & vbNewLine _
                                                 & "INNER JOIN                                              " & vbNewLine _
                                                 & "$LM_TRN$..H_OUTKAEDI_M                                  " & vbNewLine _
                                                 & "ON                                                      " & vbNewLine _
                                                 & "H_OUTKAEDI_L.NRS_BR_CD    = H_OUTKAEDI_M.NRS_BR_CD      " & vbNewLine _
                                                 & "AND                                                     " & vbNewLine _
                                                 & "H_OUTKAEDI_L.EDI_CTL_NO    = H_OUTKAEDI_M.EDI_CTL_NO    " & vbNewLine _
                                                 & "WHERE                                                   " & vbNewLine _
                                                 & "    H_OUTKAEDI_L.SYS_DEL_FLG  = '0'                     " & vbNewLine _
                                                 & "AND H_OUTKAEDI_L.OUT_FLAG    = '0'                      " & vbNewLine _
                                                 & "AND H_OUTKAEDI_L.FREE_C03    <> '16'                    " & vbNewLine _
                                                 & "AND H_OUTKAEDI_L.FREE_C02    <> ''                      " & vbNewLine _
                                                 & "AND H_OUTKAEDI_L.FREE_C02    <> '02000'                 " & vbNewLine _
                                                 & "AND H_OUTKAEDI_L.NRS_BR_CD    = @NRS_BR_CD              " & vbNewLine _
                                                 & "AND H_OUTKAEDI_L.EDI_CTL_NO    = @EDI_CTL_NO            " & vbNewLine

    ''' <summary>
    ''' サクラまとめデータ取得用SQL(SELECT句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SAKURA_MATOME_SELECT As String = "SELECT                                                        " & vbNewLine _
                                                 & " COUNT(C_OUTKA_L.OUTKA_NO_L)             AS MATOME_CNT            " & vbNewLine _
                                                 & ",MAX(C_OUTKA_M.OUTKA_NO_M)               AS OUTKA_CTL_NO_CHU      " & vbNewLine _
                                                 & ",MIN(H_OUTKAEDI_L.EDI_CTL_NO)            AS EDI_CTL_NO            " & vbNewLine _
                                                 & ",MAX(C_OUTKA_L.SYS_UPD_DATE)             AS SYS_UPD_DATE          " & vbNewLine _
                                                 & ",MAX(C_OUTKA_L.SYS_UPD_TIME)             AS SYS_UPD_TIME          " & vbNewLine _
                                                 & ",MAX(F_UNSO_M.UNSO_NO_M)                 AS UNSO_NO_M             " & vbNewLine _
                                                 & ",MAX(F_UNSO_L.SYS_UPD_DATE)              AS SYS_UNSO_UPD_DATE     " & vbNewLine _
                                                 & ",MAX(F_UNSO_L.SYS_UPD_TIME)              AS SYS_UNSO_UPD_TIME     " & vbNewLine _
                                                 & ",F_UNSO_L.UNSO_NO_L                      AS UNSO_NO_L             " & vbNewLine _
                                                 & ",C_OUTKA_L.OUTKA_NO_L                    AS OUTKA_CTL_NO          " & vbNewLine _
                                                 & ",H_OUTKAEDI_L.NRS_BR_CD                  AS NRS_BR_CD             " & vbNewLine _
                                                 & ",H_OUTKAEDI_L.OUTKA_PLAN_DATE                                     " & vbNewLine _
                                                 & ",H_OUTKAEDI_L.FREE_C03                                            " & vbNewLine _
                                                 & ",H_OUTKAEDI_L.FREE_C02                                            " & vbNewLine _
                                                 & ",H_OUTKAEDI_L.FREE_C01                                            " & vbNewLine

    ''' <summary>
    ''' サクラまとめデータ取得用SQL(FROM句)
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_SAKURA_MATOME_FROM As String = "FROM                                                  " & vbNewLine _
                                                 & "$LM_TRN$..C_OUTKA_L                                     " & vbNewLine _
                                                 & "INNER JOIN                                              " & vbNewLine _
                                                 & "$LM_TRN$..C_OUTKA_M                                     " & vbNewLine _
                                                 & "ON                                                      " & vbNewLine _
                                                 & "C_OUTKA_L.NRS_BR_CD      = C_OUTKA_M.NRS_BR_CD          " & vbNewLine _
                                                 & "AND                                                     " & vbNewLine _
                                                 & "C_OUTKA_L.OUTKA_NO_L     = C_OUTKA_M.OUTKA_NO_L         " & vbNewLine _
                                                 & "INNER JOIN                                              " & vbNewLine _
                                                 & "$LM_TRN$..H_OUTKAEDI_L                                  " & vbNewLine _
                                                 & "ON                                                      " & vbNewLine _
                                                 & "C_OUTKA_L.NRS_BR_CD      = H_OUTKAEDI_L.NRS_BR_CD       " & vbNewLine _
                                                 & "AND                                                     " & vbNewLine _
                                                 & "C_OUTKA_L.OUTKA_NO_L     = H_OUTKAEDI_L.OUTKA_CTL_NO    " & vbNewLine _
                                                 & "INNER JOIN                                              " & vbNewLine _
                                                 & "$LM_TRN$..F_UNSO_L                                      " & vbNewLine _
                                                 & "ON                                                      " & vbNewLine _
                                                 & "C_OUTKA_L.NRS_BR_CD      = F_UNSO_L.NRS_BR_CD           " & vbNewLine _
                                                 & "AND                                                     " & vbNewLine _
                                                 & "C_OUTKA_L.OUTKA_NO_L     = F_UNSO_L.INOUTKA_NO_L        " & vbNewLine _
                                                 & "INNER JOIN                                              " & vbNewLine _
                                                 & "$LM_TRN$..F_UNSO_M                                      " & vbNewLine _
                                                 & "ON                                                      " & vbNewLine _
                                                 & "F_UNSO_L.NRS_BR_CD      = F_UNSO_M.NRS_BR_CD            " & vbNewLine _
                                                 & "AND                                                     " & vbNewLine _
                                                 & "F_UNSO_L.UNSO_NO_L     = F_UNSO_M.UNSO_NO_L             " & vbNewLine _
                                                 & "WHERE                                                   " & vbNewLine _
                                                 & "    H_OUTKAEDI_L.NRS_BR_CD  = @NRS_BR_CD                " & vbNewLine _
                                                 & "AND H_OUTKAEDI_L.OUTKA_PLAN_DATE    = @OUTKA_PLAN_DATE  " & vbNewLine _
                                                 & "AND H_OUTKAEDI_L.FREE_C03    = @FREE_C03                " & vbNewLine _
                                                 & "AND H_OUTKAEDI_L.FREE_C02    = @FREE_C02                " & vbNewLine _
                                                 & "AND H_OUTKAEDI_L.FREE_C01    = @FREE_C01                " & vbNewLine _
                                                 & "AND C_OUTKA_L.OUTKA_STATE_KB = '10'                     " & vbNewLine _
                                                 & "AND C_OUTKA_L.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                                 & "AND C_OUTKA_L.OUTKA_NO_L    <> ''                       " & vbNewLine
    ''' <summary>
    ''' サクラまとめデータ取得用SQL(GROUP BY句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SAKURA_MATOME_GROUPBY As String = "GROUP BY                                           " & vbNewLine _
                                                 & " C_OUTKA_L.OUTKA_NO_L                                   " & vbNewLine _
                                                 & ",F_UNSO_L.UNSO_NO_L                                     " & vbNewLine _
                                                 & ",H_OUTKAEDI_L.NRS_BR_CD                                 " & vbNewLine _
                                                 & ",H_OUTKAEDI_L.OUTKA_PLAN_DATE                           " & vbNewLine _
                                                 & ",H_OUTKAEDI_L.FREE_C03                                  " & vbNewLine _
                                                 & ",H_OUTKAEDI_L.FREE_C02                                  " & vbNewLine _
                                                 & ",H_OUTKAEDI_L.FREE_C01                                  " & vbNewLine

    ''' <summary>
    ''' サクラまとめ元データ運送M取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MATOMEMOTO_DATA_UNSO_M As String = " SELECT                                     " & vbNewLine _
                                            & " F_UNSO_M.NRS_BR_CD                       AS NRS_BR_CD " & vbNewLine _
                                            & ",F_UNSO_M.UNSO_NO_L                       AS UNSO_NO_L " & vbNewLine _
                                            & ",F_UNSO_M.UNSO_NO_M                       AS UNSO_NO_M " & vbNewLine _
                                            & ",F_UNSO_M.BETU_WT                         AS BETU_WT   " & vbNewLine _
                                            & ",F_UNSO_M.UNSO_TTL_NB                     AS UNSO_TTL_NB " & vbNewLine _
                                            & ",F_UNSO_M.HASU                            AS HASU      " & vbNewLine _
                                            & ",F_UNSO_M.PKG_NB                          AS PKG_NB    " & vbNewLine _
                                            & " FROM                                                  " & vbNewLine _
                                            & " $LM_TRN$..F_UNSO_M                       F_UNSO_M     " & vbNewLine _
                                            & " WHERE                                                 " & vbNewLine _
                                            & " F_UNSO_M.NRS_BR_CD   = @NRS_BR_CD                     " & vbNewLine _
                                            & " AND                                                   " & vbNewLine _
                                            & " F_UNSO_M.UNSO_NO_L   = @UNSO_NO_L                     " & vbNewLine


#End Region

#Region "出荷登録処理 更新用SQL"

#Region "H_OUTKAEDI_L(通常出荷登録)"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKASAVEEDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET       " & vbNewLine _
                                              & " DEL_KB            = @DEL_KB             " & vbNewLine _
                                              & ",OUTKA_CTL_NO      = @OUTKA_CTL_NO       " & vbNewLine _
                                              & ",OUTKA_KB          = @OUTKA_KB           " & vbNewLine _
                                              & ",SYUBETU_KB        = @SYUBETU_KB         " & vbNewLine _
                                              & ",NAIGAI_KB         = @NAIGAI_KB          " & vbNewLine _
                                              & ",OUTKA_STATE_KB    = @OUTKA_STATE_KB     " & vbNewLine _
                                              & ",OUTKAHOKOKU_YN    = @OUTKAHOKOKU_YN     " & vbNewLine _
                                              & ",PICK_KB           = @PICK_KB            " & vbNewLine _
                                              & ",NRS_BR_NM         = @NRS_BR_NM          " & vbNewLine _
                                              & ",WH_CD             = @WH_CD              " & vbNewLine _
                                              & ",WH_NM             = @WH_NM              " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE   = @OUTKA_PLAN_DATE    " & vbNewLine _
                                              & ",OUTKO_DATE        = @OUTKO_DATE         " & vbNewLine _
                                              & ",ARR_PLAN_DATE     = @ARR_PLAN_DATE      " & vbNewLine _
                                              & ",ARR_PLAN_TIME     = @ARR_PLAN_TIME      " & vbNewLine _
                                              & ",HOKOKU_DATE       = @HOKOKU_DATE        " & vbNewLine _
                                              & ",TOUKI_HOKAN_YN    = @TOUKI_HOKAN_YN     " & vbNewLine _
                                              & ",CUST_CD_L         = @CUST_CD_L          " & vbNewLine _
                                              & ",CUST_CD_M         = @CUST_CD_M          " & vbNewLine _
                                              & ",CUST_NM_L         = @CUST_NM_L          " & vbNewLine _
                                              & ",CUST_NM_M         = @CUST_NM_M          " & vbNewLine _
                                              & ",SHIP_CD_L         = @SHIP_CD_L          " & vbNewLine _
                                              & ",SHIP_CD_M         = @SHIP_CD_M          " & vbNewLine _
                                              & ",SHIP_NM_L         = @SHIP_NM_L          " & vbNewLine _
                                              & ",SHIP_NM_M         = @SHIP_NM_M          " & vbNewLine _
                                              & ",EDI_DEST_CD       = @EDI_DEST_CD        " & vbNewLine _
                                              & ",DEST_CD           = @DEST_CD            " & vbNewLine _
                                              & ",DEST_NM           = @DEST_NM            " & vbNewLine _
                                              & ",DEST_ZIP          = @DEST_ZIP           " & vbNewLine _
                                              & ",DEST_AD_1         = @DEST_AD_1          " & vbNewLine _
                                              & ",DEST_AD_2         = @DEST_AD_2          " & vbNewLine _
                                              & ",DEST_AD_3         = @DEST_AD_3          " & vbNewLine _
                                              & ",DEST_AD_4         = @DEST_AD_4          " & vbNewLine _
                                              & ",DEST_AD_5         = @DEST_AD_5          " & vbNewLine _
                                              & ",DEST_TEL          = @DEST_TEL           " & vbNewLine _
                                              & ",DEST_FAX          = @DEST_FAX           " & vbNewLine _
                                              & ",DEST_MAIL         = @DEST_MAIL          " & vbNewLine _
                                              & ",DEST_JIS_CD       = @DEST_JIS_CD        " & vbNewLine _
                                              & ",SP_NHS_KB         = @SP_NHS_KB          " & vbNewLine _
                                              & ",COA_YN            = @COA_YN             " & vbNewLine _
                                              & ",CUST_ORD_NO       = @CUST_ORD_NO        " & vbNewLine _
                                              & ",BUYER_ORD_NO      = @BUYER_ORD_NO       " & vbNewLine _
                                              & ",UNSO_MOTO_KB      = @UNSO_MOTO_KB       " & vbNewLine _
                                              & ",UNSO_TEHAI_KB     = @UNSO_TEHAI_KB      " & vbNewLine _
                                              & ",SYARYO_KB         = @SYARYO_KB          " & vbNewLine _
                                              & ",BIN_KB            = @BIN_KB             " & vbNewLine _
                                              & ",UNSO_CD           = @UNSO_CD            " & vbNewLine _
                                              & ",UNSO_NM           = @UNSO_NM            " & vbNewLine _
                                              & ",UNSO_BR_CD        = @UNSO_BR_CD         " & vbNewLine _
                                              & ",UNSO_BR_NM        = @UNSO_BR_NM         " & vbNewLine _
                                              & ",UNCHIN_TARIFF_CD  = @UNCHIN_TARIFF_CD   " & vbNewLine _
                                              & ",EXTC_TARIFF_CD    = @EXTC_TARIFF_CD     " & vbNewLine _
                                              & ",REMARK            = @REMARK             " & vbNewLine _
                                              & ",UNSO_ATT          = @UNSO_ATT           " & vbNewLine _
                                              & ",DENP_YN           = @DENP_YN            " & vbNewLine _
                                              & ",PC_KB             = @PC_KB           	  " & vbNewLine _
                                              & ",UNCHIN_YN         = @UNCHIN_YN          " & vbNewLine _
                                              & ",NIYAKU_YN         = @NIYAKU_YN          " & vbNewLine _
                                              & ",OUT_FLAG          = @OUT_FLAG           " & vbNewLine _
                                              & ",AKAKURO_KB        = @AKAKURO_KB         " & vbNewLine _
                                              & ",JISSEKI_FLAG      = @JISSEKI_FLAG       " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER       " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE       " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME       " & vbNewLine _
                                              & ",FREE_N01          = @FREE_N01           " & vbNewLine _
                                              & ",FREE_N02          = @FREE_N02           " & vbNewLine _
                                              & ",FREE_N03          = @FREE_N03           " & vbNewLine _
                                              & ",FREE_N04          = @FREE_N04           " & vbNewLine _
                                              & ",FREE_N05          = @FREE_N05           " & vbNewLine _
                                              & ",FREE_N06          = @FREE_N06           " & vbNewLine _
                                              & ",FREE_N07          = @FREE_N07           " & vbNewLine _
                                              & ",FREE_N08          = @FREE_N08           " & vbNewLine _
                                              & ",FREE_N09          = @FREE_N09           " & vbNewLine _
                                              & ",FREE_N10          = @FREE_N10           " & vbNewLine _
                                              & ",FREE_C01          = @FREE_C01           " & vbNewLine _
                                              & ",FREE_C02          = @FREE_C02           " & vbNewLine _
                                              & ",FREE_C03          = @FREE_C03           " & vbNewLine _
                                              & ",FREE_C04          = @FREE_C04           " & vbNewLine _
                                              & ",FREE_C05          = @FREE_C05           " & vbNewLine _
                                              & ",FREE_C06          = @FREE_C06           " & vbNewLine _
                                              & ",FREE_C07          = @FREE_C07           " & vbNewLine _
                                              & ",FREE_C08          = @FREE_C08           " & vbNewLine _
                                              & ",FREE_C09          = @FREE_C09           " & vbNewLine _
                                              & ",FREE_C10          = @FREE_C10           " & vbNewLine _
                                              & ",FREE_C11          = @FREE_C11           " & vbNewLine _
                                              & ",FREE_C12          = @FREE_C12           " & vbNewLine _
                                              & ",FREE_C13          = @FREE_C13           " & vbNewLine _
                                              & ",FREE_C14          = @FREE_C14           " & vbNewLine _
                                              & ",FREE_C15          = @FREE_C15           " & vbNewLine _
                                              & ",FREE_C16          = @FREE_C16           " & vbNewLine _
                                              & ",FREE_C17          = @FREE_C17           " & vbNewLine _
                                              & ",FREE_C18          = @FREE_C18           " & vbNewLine _
                                              & ",FREE_C19          = @FREE_C19           " & vbNewLine _
                                              & ",FREE_C20          = @FREE_C20           " & vbNewLine _
                                              & ",FREE_C21          = @FREE_C21           " & vbNewLine _
                                              & ",FREE_C22          = @FREE_C22           " & vbNewLine _
                                              & ",FREE_C23          = @FREE_C23           " & vbNewLine _
                                              & ",FREE_C24          = @FREE_C24           " & vbNewLine _
                                              & ",FREE_C25          = @FREE_C25           " & vbNewLine _
                                              & ",FREE_C26          = @FREE_C26           " & vbNewLine _
                                              & ",FREE_C27          = @FREE_C27           " & vbNewLine _
                                              & ",FREE_C28          = @FREE_C28           " & vbNewLine _
                                              & ",FREE_C29          = @FREE_C29           " & vbNewLine _
                                              & ",FREE_C30          = @FREE_C30           " & vbNewLine _
                                              & ",CRT_USER          = @CRT_USER           " & vbNewLine _
                                              & ",CRT_DATE          = @CRT_DATE           " & vbNewLine _
                                              & ",CRT_TIME          = @CRT_TIME           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER           " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE           " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME           " & vbNewLine _
                                              & ",SCM_CTL_NO_L      = @SCM_CTL_NO_L       " & vbNewLine _
                                              & ",EDIT_FLAG         = @EDIT_FLAG          " & vbNewLine _
                                              & ",MATCHING_FLAG     = @MATCHING_FLAG      " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE       " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME       " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID       " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER       " & vbNewLine _
                                              & ",SYS_DEL_FLG       = @SYS_DEL_FLG        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD          " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO         " & vbNewLine
#End Region

#Region "H_OUTKAEDI_L(まとめ先の更新用)"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_MATOMESAKI_OUTKAEDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET       " & vbNewLine _
                                              & " FREE_C30          = @FREE_C30            " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD       " & vbNewLine _
                                              & "AND OUTKA_CTL_NO   = @OUTKA_CTL_NO      " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO      " & vbNewLine _
                                              & "AND SYS_DEL_FLG     <> '1'      " & vbNewLine
#End Region

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' H_OUTKAEDI_MのUPDATE文（H_OUTKAEDI_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKAEDI_M As String = "UPDATE $LM_TRN$..H_OUTKAEDI_M SET      " & vbNewLine _
                                          & " DEL_KB            =  @DEL_KB            	    " & vbNewLine _
                                          & ",OUTKA_CTL_NO      =  @OUTKA_CTL_NO            " & vbNewLine _
                                          & ",OUTKA_CTL_NO_CHU  =  @OUTKA_CTL_NO_CHU        " & vbNewLine _
                                          & ",COA_YN            =  @COA_YN                  " & vbNewLine _
                                          & ",CUST_ORD_NO_DTL   =  @CUST_ORD_NO_DTL         " & vbNewLine _
                                          & ",BUYER_ORD_NO_DTL  =  @BUYER_ORD_NO_DTL        " & vbNewLine _
                                          & ",CUST_GOODS_CD     =  @CUST_GOODS_CD           " & vbNewLine _
                                          & ",NRS_GOODS_CD      =  @NRS_GOODS_CD            " & vbNewLine _
                                          & ",GOODS_NM          =  @GOODS_NM                " & vbNewLine _
                                          & ",RSV_NO            =  @RSV_NO                  " & vbNewLine _
                                          & ",LOT_NO            =  @LOT_NO                  " & vbNewLine _
                                          & ",SERIAL_NO         =  @SERIAL_NO               " & vbNewLine _
                                          & ",ALCTD_KB          =  @ALCTD_KB                " & vbNewLine _
                                          & ",OUTKA_PKG_NB      =  @OUTKA_PKG_NB            " & vbNewLine _
                                          & ",OUTKA_HASU        =  @OUTKA_HASU              " & vbNewLine _
                                          & ",OUTKA_QT          =  @OUTKA_QT                " & vbNewLine _
                                          & ",OUTKA_TTL_NB      =  @OUTKA_TTL_NB            " & vbNewLine _
                                          & ",OUTKA_TTL_QT      =  @OUTKA_TTL_QT            " & vbNewLine _
                                          & ",KB_UT             =  @KB_UT                   " & vbNewLine _
                                          & ",QT_UT             =  @QT_UT                   " & vbNewLine _
                                          & ",PKG_NB            =  @PKG_NB                  " & vbNewLine _
                                          & ",PKG_UT            =  @PKG_UT                  " & vbNewLine _
                                          & ",ONDO_KB           =  @ONDO_KB                 " & vbNewLine _
                                          & ",UNSO_ONDO_KB      =  @UNSO_ONDO_KB            " & vbNewLine _
                                          & ",IRIME             =  @IRIME                   " & vbNewLine _
                                          & ",IRIME_UT          =  @IRIME_UT                " & vbNewLine _
                                          & ",BETU_WT           =  @BETU_WT                 " & vbNewLine _
                                          & ",REMARK            =  @REMARK                  " & vbNewLine _
                                          & ",OUT_KB            =  @OUT_KB                  " & vbNewLine _
                                          & ",AKAKURO_KB        =  @AKAKURO_KB              " & vbNewLine _
                                          & ",JISSEKI_FLAG      =  @JISSEKI_FLAG            " & vbNewLine _
                                          & ",JISSEKI_USER      =  @JISSEKI_USER            " & vbNewLine _
                                          & ",JISSEKI_DATE      =  @JISSEKI_DATE            " & vbNewLine _
                                          & ",JISSEKI_TIME      =  @JISSEKI_TIME            " & vbNewLine _
                                          & ",SET_KB            =  @SET_KB                  " & vbNewLine _
                                          & ",FREE_N01          =  @FREE_N01                " & vbNewLine _
                                          & ",FREE_N02          =  @FREE_N02                " & vbNewLine _
                                          & ",FREE_N03          =  @FREE_N03                " & vbNewLine _
                                          & ",FREE_N04          =  @FREE_N04                " & vbNewLine _
                                          & ",FREE_N05          =  @FREE_N05                " & vbNewLine _
                                          & ",FREE_N06          =  @FREE_N06                " & vbNewLine _
                                          & ",FREE_N07          =  @FREE_N07                " & vbNewLine _
                                          & ",FREE_N08          =  @FREE_N08                " & vbNewLine _
                                          & ",FREE_N09          =  @FREE_N09                " & vbNewLine _
                                          & ",FREE_N10          =  @FREE_N10                " & vbNewLine _
                                          & ",FREE_C01          =  @FREE_C01                " & vbNewLine _
                                          & ",FREE_C02          =  @FREE_C02                " & vbNewLine _
                                          & ",FREE_C03          =  @FREE_C03                " & vbNewLine _
                                          & ",FREE_C04          =  @FREE_C04                " & vbNewLine _
                                          & ",FREE_C05          =  @FREE_C05                " & vbNewLine _
                                          & ",FREE_C06          =  @FREE_C06                " & vbNewLine _
                                          & ",FREE_C07          =  @FREE_C07                " & vbNewLine _
                                          & ",FREE_C08          =  @FREE_C08                " & vbNewLine _
                                          & ",FREE_C09          =  @FREE_C09                " & vbNewLine _
                                          & ",FREE_C10          =  @FREE_C10                " & vbNewLine _
                                          & ",FREE_C11          =  @FREE_C11                " & vbNewLine _
                                          & ",FREE_C12          =  @FREE_C12                " & vbNewLine _
                                          & ",FREE_C13          =  @FREE_C13                " & vbNewLine _
                                          & ",FREE_C14          =  @FREE_C14                " & vbNewLine _
                                          & ",FREE_C15          =  @FREE_C15                " & vbNewLine _
                                          & ",FREE_C16          =  @FREE_C16                " & vbNewLine _
                                          & ",FREE_C17          =  @FREE_C17                " & vbNewLine _
                                          & ",FREE_C18          =  @FREE_C18                " & vbNewLine _
                                          & ",FREE_C19          =  @FREE_C19                " & vbNewLine _
                                          & ",FREE_C20          =  @FREE_C20                " & vbNewLine _
                                          & ",FREE_C21          =  @FREE_C21                " & vbNewLine _
                                          & ",FREE_C22          =  @FREE_C22                " & vbNewLine _
                                          & ",FREE_C23          =  @FREE_C23                " & vbNewLine _
                                          & ",FREE_C24          =  @FREE_C24                " & vbNewLine _
                                          & ",FREE_C25          =  @FREE_C25                " & vbNewLine _
                                          & ",FREE_C26          =  @FREE_C26                " & vbNewLine _
                                          & ",FREE_C27          =  @FREE_C27                " & vbNewLine _
                                          & ",FREE_C28          =  @FREE_C28                " & vbNewLine _
                                          & ",FREE_C29          =  @FREE_C29                " & vbNewLine _
                                          & ",FREE_C30          =  @FREE_C30                " & vbNewLine _
                                          & ",CRT_USER          =  @CRT_USER                " & vbNewLine _
                                          & ",CRT_DATE          =  @CRT_DATE                " & vbNewLine _
                                          & ",CRT_TIME          =  @CRT_TIME                " & vbNewLine _
                                          & ",UPD_USER          =  @UPD_USER                " & vbNewLine _
                                          & ",UPD_DATE          =  @UPD_DATE                " & vbNewLine _
                                          & ",UPD_TIME          =  @UPD_TIME                " & vbNewLine _
                                          & ",SCM_CTL_NO_L      =  @SCM_CTL_NO_L            " & vbNewLine _
                                          & ",SCM_CTL_NO_M      =  @SCM_CTL_NO_M            " & vbNewLine _
                                          & ",SYS_UPD_DATE      =  @SYS_UPD_DATE            " & vbNewLine _
                                          & ",SYS_UPD_TIME      =  @SYS_UPD_TIME            " & vbNewLine _
                                          & ",SYS_UPD_PGID      =  @SYS_UPD_PGID            " & vbNewLine _
                                          & ",SYS_UPD_USER      =  @SYS_UPD_USER            " & vbNewLine _
                                          & ",SYS_DEL_FLG       =  @SYS_DEL_FLG             " & vbNewLine _
                                          & "WHERE   NRS_BR_CD  =  @NRS_BR_CD               " & vbNewLine _
                                          & "AND EDI_CTL_NO     =  @EDI_CTL_NO              " & vbNewLine _
                                          & "AND EDI_CTL_NO_CHU =  @EDI_CTL_NO_CHU          " & vbNewLine
#End Region

#Region "H_OUTKAEDI_HED_SFJ"
    ''' <summary>
    ''' サクラEDI受信HEDのUPDATE文（H_OUTKAEDI_HED_SFJ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKASAVE_EDI_RCV_HED As String = "UPDATE $LM_TRN$..H_OUTKAEDI_HED_SFJ SET       " & vbNewLine _
                                              & " OUTKA_CTL_NO      = @OUTKA_CTL_NO       	        " & vbNewLine _
                                              & ",OUTKA_USER        = @UPD_USER                     " & vbNewLine _
                                              & ",OUTKA_DATE        = @UPD_DATE                     " & vbNewLine _
                                              & ",OUTKA_TIME        = @UPD_TIME                     " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                   " & vbNewLine _
                                              & "AND SYS_DEL_FLG    = '0'                           " & vbNewLine

#End Region

#Region "H_OUTKAEDI_DTL_SFJ"
    ''' <summary>
    ''' サクラEDI受信DTLのUPDATE文（H_OUTKAEDI_DTL_SFJ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKASAVE_EDI_RCV_DTL As String = "UPDATE $LM_TRN$..H_OUTKAEDI_DTL_SFJ SET       " & vbNewLine _
                                              & " OUTKA_CTL_NO      = @OUTKA_CTL_NO       	        " & vbNewLine _
                                              & ",OUTKA_CTL_NO_CHU  = @OUTKA_CTL_NO_CHU       	    " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                   " & vbNewLine _
                                              & "AND EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU               " & vbNewLine _
                                              & "AND SYS_DEL_FLG    = '0'                           " & vbNewLine

#End Region

#Region "C_OUTKA_L(INSERT)"

    ''' <summary>
    ''' INSERT（OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTKA_L As String = "INSERT INTO                                        " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_L                                      " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",OUTKA_NO_L                                              " & vbNewLine _
                                         & ",FURI_NO                                                 " & vbNewLine _
                                         & ",OUTKA_KB                                                " & vbNewLine _
                                         & ",SYUBETU_KB                                              " & vbNewLine _
                                         & ",OUTKA_STATE_KB                                          " & vbNewLine _
                                         & ",OUTKAHOKOKU_YN                                          " & vbNewLine _
                                         & ",PICK_KB                                                 " & vbNewLine _
                                         & ",DENP_NO                                                 " & vbNewLine _
                                         & ",ARR_KANRYO_INFO                                         " & vbNewLine _
                                         & ",WH_CD                                                   " & vbNewLine _
                                         & ",OUTKA_PLAN_DATE                                         " & vbNewLine _
                                         & ",OUTKO_DATE                                              " & vbNewLine _
                                         & ",ARR_PLAN_DATE                                           " & vbNewLine _
                                         & ",ARR_PLAN_TIME                                           " & vbNewLine _
                                         & ",HOKOKU_DATE                                             " & vbNewLine _
                                         & ",TOUKI_HOKAN_YN                                          " & vbNewLine _
                                         & ",END_DATE                                                " & vbNewLine _
                                         & ",CUST_CD_L                                               " & vbNewLine _
                                         & ",CUST_CD_M                                               " & vbNewLine _
                                         & ",SHIP_CD_L                                               " & vbNewLine _
                                         & ",SHIP_CD_M                                               " & vbNewLine _
                                         & ",DEST_CD                                                 " & vbNewLine _
                                         & ",DEST_AD_3                                               " & vbNewLine _
                                         & ",DEST_TEL                                                " & vbNewLine _
                                         & ",NHS_REMARK                                              " & vbNewLine _
                                         & ",SP_NHS_KB                                               " & vbNewLine _
                                         & ",COA_YN                                                  " & vbNewLine _
                                         & ",CUST_ORD_NO                                             " & vbNewLine _
                                         & ",BUYER_ORD_NO                                            " & vbNewLine _
                                         & ",REMARK                                                  " & vbNewLine _
                                         & ",OUTKA_PKG_NB                                            " & vbNewLine _
                                         & ",DENP_YN                                                 " & vbNewLine _
                                         & ",PC_KB                                                   " & vbNewLine _
                                         & ",NIYAKU_YN                                               " & vbNewLine _
                                         & ",ALL_PRINT_FLAG                                          " & vbNewLine _
                                         & ",NIHUDA_FLAG                                             " & vbNewLine _
                                         & ",NHS_FLAG                                                " & vbNewLine _
                                         & ",DENP_FLAG                                               " & vbNewLine _
                                         & ",COA_FLAG                                                " & vbNewLine _
                                         & ",HOKOKU_FLAG                                             " & vbNewLine _
                                         & ",MATOME_PICK_FLAG                                        " & vbNewLine _
                                         & ",LAST_PRINT_DATE                                         " & vbNewLine _
                                         & ",LAST_PRINT_TIME                                         " & vbNewLine _
                                         & ",SASZ_USER                                               " & vbNewLine _
                                         & ",OUTKO_USER                                              " & vbNewLine _
                                         & ",KEN_USER                                                " & vbNewLine _
                                         & ",OUTKA_USER                                              " & vbNewLine _
                                         & ",HOU_USER                                                " & vbNewLine _
                                         & ",ORDER_TYPE                                              " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ",DEST_KB                                                 " & vbNewLine _
                                         & ",DEST_NM                                                 " & vbNewLine _
                                         & ",DEST_AD_1                                               " & vbNewLine _
                                         & ",DEST_AD_2                                               " & vbNewLine _
                                         & ",WH_TAB_STATUS                                           " & vbNewLine _
                                         & ",WH_TAB_YN                                               " & vbNewLine _
                                         & ")VALUES(                                                 " & vbNewLine _
                                         & " @NRS_BR_CD                                              " & vbNewLine _
                                         & ",@OUTKA_NO_L                                             " & vbNewLine _
                                         & ",@FURI_NO                                                " & vbNewLine _
                                         & ",@OUTKA_KB                                               " & vbNewLine _
                                         & ",@SYUBETU_KB                                             " & vbNewLine _
                                         & ",@OUTKA_STATE_KB                                         " & vbNewLine _
                                         & ",@OUTKAHOKOKU_YN                                         " & vbNewLine _
                                         & ",@PICK_KB                                                " & vbNewLine _
                                         & ",@DENP_NO                                                " & vbNewLine _
                                         & ",@ARR_KANRYO_INFO                                        " & vbNewLine _
                                         & ",@WH_CD                                                  " & vbNewLine _
                                         & ",@OUTKA_PLAN_DATE                                        " & vbNewLine _
                                         & ",@OUTKO_DATE                                             " & vbNewLine _
                                         & ",@ARR_PLAN_DATE                                          " & vbNewLine _
                                         & ",@ARR_PLAN_TIME                                          " & vbNewLine _
                                         & ",@HOKOKU_DATE                                            " & vbNewLine _
                                         & ",@TOUKI_HOKAN_YN                                         " & vbNewLine _
                                         & ",@END_DATE                                               " & vbNewLine _
                                         & ",@CUST_CD_L                                              " & vbNewLine _
                                         & ",@CUST_CD_M                                              " & vbNewLine _
                                         & ",@SHIP_CD_L                                              " & vbNewLine _
                                         & ",@SHIP_CD_M                                              " & vbNewLine _
                                         & ",@DEST_CD                                                " & vbNewLine _
                                         & ",@DEST_AD_3                                              " & vbNewLine _
                                         & ",@DEST_TEL                                               " & vbNewLine _
                                         & ",@NHS_REMARK                                             " & vbNewLine _
                                         & ",@SP_NHS_KB                                              " & vbNewLine _
                                         & ",@COA_YN                                                 " & vbNewLine _
                                         & ",@CUST_ORD_NO                                            " & vbNewLine _
                                         & ",@BUYER_ORD_NO                                           " & vbNewLine _
                                         & ",@REMARK                                                 " & vbNewLine _
                                         & ",@OUTKA_PKG_NB                                           " & vbNewLine _
                                         & ",@DENP_YN                                                " & vbNewLine _
                                         & ",@PC_KB                                                  " & vbNewLine _
                                         & ",@NIYAKU_YN                                              " & vbNewLine _
                                         & ",@ALL_PRINT_FLAG                                         " & vbNewLine _
                                         & ",@NIHUDA_FLAG                                            " & vbNewLine _
                                         & ",@NHS_FLAG                                               " & vbNewLine _
                                         & ",@DENP_FLAG                                              " & vbNewLine _
                                         & ",@COA_FLAG                                               " & vbNewLine _
                                         & ",@HOKOKU_FLAG                                            " & vbNewLine _
                                         & ",@MATOME_PICK_FLAG                                       " & vbNewLine _
                                         & ",@LAST_PRINT_DATE                                        " & vbNewLine _
                                         & ",@LAST_PRINT_TIME                                        " & vbNewLine _
                                         & ",@SASZ_USER                                              " & vbNewLine _
                                         & ",@OUTKO_USER                                             " & vbNewLine _
                                         & ",@KEN_USER                                               " & vbNewLine _
                                         & ",@OUTKA_USER                                             " & vbNewLine _
                                         & ",@HOU_USER                                               " & vbNewLine _
                                         & ",@ORDER_TYPE                                             " & vbNewLine _
                                         & ",@SYS_ENT_DATE                                           " & vbNewLine _
                                         & ",@SYS_ENT_TIME                                           " & vbNewLine _
                                         & ",@SYS_ENT_PGID                                           " & vbNewLine _
                                         & ",@SYS_ENT_USER                                           " & vbNewLine _
                                         & ",@SYS_UPD_DATE                                           " & vbNewLine _
                                         & ",@SYS_UPD_TIME                                           " & vbNewLine _
                                         & ",@SYS_UPD_PGID                                           " & vbNewLine _
                                         & ",@SYS_UPD_USER                                           " & vbNewLine _
                                         & ",@SYS_DEL_FLG                                            " & vbNewLine _
                                         & ",@DEST_KB                                                " & vbNewLine _
                                         & ",@DEST_NM                                                " & vbNewLine _
                                         & ",@DEST_AD_1                                              " & vbNewLine _
                                         & ",@DEST_AD_2                                              " & vbNewLine _
                                         & ",@WH_TAB_STATUS                                          " & vbNewLine _
                                         & ",@WH_TAB_YN                                              " & vbNewLine _
                                         & ")                                                        " & vbNewLine
#End Region

#Region "C_OUTKA_L(UPDATE:サクラまとめ用)"
    ''' <summary>
    ''' C_OUTKA_LのUPDATE文（C_OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_C_OUTKASAVE_L As String = "UPDATE $LM_TRN$..C_OUTKA_L SET       " & vbNewLine _
                                              & " SYS_UPD_DATE      = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD       " & vbNewLine _
                                              & "AND OUTKA_NO_L     = @OUTKA_NO_L      " & vbNewLine _
                                              & "AND SYS_DEL_FLG     <> '1'      " & vbNewLine
#End Region

#Region "OUTKA_M"

    ''' <summary>
    ''' INSERT（OUTKA_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTKA_M As String = "INSERT INTO                                        " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_M                                      " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",OUTKA_NO_L                                              " & vbNewLine _
                                         & ",OUTKA_NO_M                                              " & vbNewLine _
                                         & ",EDI_SET_NO                                              " & vbNewLine _
                                         & ",COA_YN                                                  " & vbNewLine _
                                         & ",CUST_ORD_NO_DTL                                         " & vbNewLine _
                                         & ",BUYER_ORD_NO_DTL                                        " & vbNewLine _
                                         & ",GOODS_CD_NRS                                            " & vbNewLine _
                                         & ",RSV_NO                                                  " & vbNewLine _
                                         & ",LOT_NO                                                  " & vbNewLine _
                                         & ",SERIAL_NO                                               " & vbNewLine _
                                         & ",ALCTD_KB                                                " & vbNewLine _
                                         & ",OUTKA_PKG_NB                                            " & vbNewLine _
                                         & ",OUTKA_HASU                                              " & vbNewLine _
                                         & ",OUTKA_QT                                                " & vbNewLine _
                                         & ",OUTKA_TTL_NB                                            " & vbNewLine _
                                         & ",OUTKA_TTL_QT                                            " & vbNewLine _
                                         & ",ALCTD_NB                                                " & vbNewLine _
                                         & ",ALCTD_QT                                                " & vbNewLine _
                                         & ",BACKLOG_NB                                              " & vbNewLine _
                                         & ",BACKLOG_QT                                              " & vbNewLine _
                                         & ",UNSO_ONDO_KB                                            " & vbNewLine _
                                         & ",IRIME                                                   " & vbNewLine _
                                         & ",IRIME_UT                                                " & vbNewLine _
                                         & ",OUTKA_M_PKG_NB                                          " & vbNewLine _
                                         & ",REMARK                                                  " & vbNewLine _
                                         & ",SIZE_KB                                                 " & vbNewLine _
                                         & ",ZAIKO_KB                                                " & vbNewLine _
                                         & ",SOURCE_CD                                               " & vbNewLine _
                                         & ",YELLOW_CARD                                             " & vbNewLine _
                                         & ",PRINT_SORT                                              " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ")VALUES(                                                 " & vbNewLine _
                                         & " @NRS_BR_CD                                              " & vbNewLine _
                                         & ",@OUTKA_NO_L                                             " & vbNewLine _
                                         & ",@OUTKA_NO_M                                             " & vbNewLine _
                                         & ",@EDI_SET_NO                                             " & vbNewLine _
                                         & ",@COA_YN                                                 " & vbNewLine _
                                         & ",@CUST_ORD_NO_DTL                                        " & vbNewLine _
                                         & ",@BUYER_ORD_NO_DTL                                       " & vbNewLine _
                                         & ",@GOODS_CD_NRS                                           " & vbNewLine _
                                         & ",@RSV_NO                                                 " & vbNewLine _
                                         & ",@LOT_NO                                                 " & vbNewLine _
                                         & ",@SERIAL_NO                                              " & vbNewLine _
                                         & ",@ALCTD_KB                                               " & vbNewLine _
                                         & ",@OUTKA_PKG_NB                                           " & vbNewLine _
                                         & ",@OUTKA_HASU                                             " & vbNewLine _
                                         & ",@OUTKA_QT                                               " & vbNewLine _
                                         & ",@OUTKA_TTL_NB                                           " & vbNewLine _
                                         & ",@OUTKA_TTL_QT                                           " & vbNewLine _
                                         & ",@ALCTD_NB                                               " & vbNewLine _
                                         & ",@ALCTD_QT                                               " & vbNewLine _
                                         & ",@BACKLOG_NB                                             " & vbNewLine _
                                         & ",@BACKLOG_QT                                             " & vbNewLine _
                                         & ",@UNSO_ONDO_KB                                           " & vbNewLine _
                                         & ",@IRIME                                                  " & vbNewLine _
                                         & ",@IRIME_UT                                               " & vbNewLine _
                                         & ",@OUTKA_M_PKG_NB                                         " & vbNewLine _
                                         & ",@REMARK                                                 " & vbNewLine _
                                         & ",@SIZE_KB                                                " & vbNewLine _
                                         & ",@ZAIKO_KB                                               " & vbNewLine _
                                         & ",@SOURCE_CD                                              " & vbNewLine _
                                         & ",@YELLOW_CARD                                            " & vbNewLine _
                                         & ",@PRINT_SORT                                             " & vbNewLine _
                                         & ",@SYS_ENT_DATE                                           " & vbNewLine _
                                         & ",@SYS_ENT_TIME                                           " & vbNewLine _
                                         & ",@SYS_ENT_PGID                                           " & vbNewLine _
                                         & ",@SYS_ENT_USER                                           " & vbNewLine _
                                         & ",@SYS_UPD_DATE                                           " & vbNewLine _
                                         & ",@SYS_UPD_TIME                                           " & vbNewLine _
                                         & ",@SYS_UPD_PGID                                           " & vbNewLine _
                                         & ",@SYS_UPD_USER                                           " & vbNewLine _
                                         & ",@SYS_DEL_FLG                                            " & vbNewLine _
                                         & ")                                                        " & vbNewLine

#End Region

#Region "SAGYO"

    ''' <summary>
    ''' INSERT（SAGYO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SAGYO As String = "INSERT INTO                                          " & vbNewLine _
                                         & "$LM_TRN$..E_SAGYO                                        " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",SAGYO_REC_NO                                            " & vbNewLine _
                                         & ",SAGYO_COMP                                              " & vbNewLine _
                                         & ",SKYU_CHK                                                " & vbNewLine _
                                         & ",SAGYO_SIJI_NO                                           " & vbNewLine _
                                         & ",INOUTKA_NO_LM                                           " & vbNewLine _
                                         & ",WH_CD                                                   " & vbNewLine _
                                         & ",IOZS_KB                                                 " & vbNewLine _
                                         & ",SAGYO_CD                                                " & vbNewLine _
                                         & ",SAGYO_NM                                                " & vbNewLine _
                                         & ",CUST_CD_L                                               " & vbNewLine _
                                         & ",CUST_CD_M                                               " & vbNewLine _
                                         & ",DEST_CD                                                 " & vbNewLine _
                                         & ",DEST_NM                                                 " & vbNewLine _
                                         & ",GOODS_CD_NRS                                            " & vbNewLine _
                                         & ",GOODS_NM_NRS                                            " & vbNewLine _
                                         & ",LOT_NO                                                  " & vbNewLine _
                                         & ",INV_TANI                                                " & vbNewLine _
                                         & ",SAGYO_NB                                                " & vbNewLine _
                                         & ",SAGYO_UP                                                " & vbNewLine _
                                         & ",SAGYO_GK                                                " & vbNewLine _
                                         & ",TAX_KB                                                  " & vbNewLine _
                                         & ",SEIQTO_CD                                               " & vbNewLine _
                                         & ",REMARK_ZAI                                              " & vbNewLine _
                                         & ",REMARK_SKYU                                             " & vbNewLine _
                                         & ",REMARK_SIJI                                             " & vbNewLine _
                                         & ",SAGYO_COMP_CD                                           " & vbNewLine _
                                         & ",SAGYO_COMP_DATE                                         " & vbNewLine _
                                         & ",DEST_SAGYO_FLG                                          " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ")SELECT                                                  " & vbNewLine _
                                         & " @NRS_BR_CD                                              " & vbNewLine _
                                         & ",@SAGYO_REC_NO                                           " & vbNewLine _
                                         & ",@SAGYO_COMP                                             " & vbNewLine _
                                         & ",@SKYU_CHK                                               " & vbNewLine _
                                         & ",@SAGYO_SIJI_NO                                          " & vbNewLine _
                                         & ",@INOUTKA_NO_LM                                          " & vbNewLine _
                                         & ",@WH_CD                                                  " & vbNewLine _
                                         & ",@IOZS_KB                                                " & vbNewLine _
                                         & ",@SAGYO_CD                                               " & vbNewLine _
                                         & ",M_SAGYO.SAGYO_NM                                        " & vbNewLine _
                                         & ",@CUST_CD_L                                              " & vbNewLine _
                                         & ",@CUST_CD_M                                              " & vbNewLine _
                                         & ",@DEST_CD                                                " & vbNewLine _
                                         & ",@DEST_NM                                                " & vbNewLine _
                                         & ",@GOODS_CD_NRS                                           " & vbNewLine _
                                         & ",@GOODS_NM_NRS                                           " & vbNewLine _
                                         & ",@LOT_NO                                                 " & vbNewLine _
                                         & ",M_SAGYO.INV_TANI                                        " & vbNewLine _
                                         & ",@SAGYO_NB                                               " & vbNewLine _
                                         & ",M_SAGYO.SAGYO_UP                                        " & vbNewLine _
                                         & ",@SAGYO_GK                                               " & vbNewLine _
                                         & ",M_SAGYO.ZEI_KBN                                         " & vbNewLine _
                                         & ",M_CUST.SAGYO_SEIQTO_CD                                  " & vbNewLine _
                                         & ",M_SAGYO.SAGYO_REMARK                                    " & vbNewLine _
                                         & ",@REMARK_SKYU                                            " & vbNewLine _
                                         & ",M_SAGYO.WH_SAGYO_REMARK                                 " & vbNewLine _
                                         & ",@SAGYO_COMP_CD                                          " & vbNewLine _
                                         & ",@SAGYO_COMP_DATE                                        " & vbNewLine _
                                         & ",@DEST_SAGYO_FLG                                         " & vbNewLine _
                                         & ",@SYS_ENT_DATE                                           " & vbNewLine _
                                         & ",@SYS_ENT_TIME                                           " & vbNewLine _
                                         & ",@SYS_ENT_PGID                                           " & vbNewLine _
                                         & ",@SYS_ENT_USER                                           " & vbNewLine _
                                         & ",@SYS_UPD_DATE                                           " & vbNewLine _
                                         & ",@SYS_UPD_TIME                                           " & vbNewLine _
                                         & ",@SYS_UPD_PGID                                           " & vbNewLine _
                                         & ",@SYS_UPD_USER                                           " & vbNewLine _
                                         & ",@SYS_DEL_FLG                                            " & vbNewLine _
                                         & "FROM                                                     " & vbNewLine _
                                         & "$LM_MST$..M_SAGYO   M_SAGYO                              " & vbNewLine _
                                         & ",$LM_MST$..M_GOODS  M_GOODS                              " & vbNewLine _
                                         & "LEFT JOIN                                                " & vbNewLine _
                                         & "$LM_MST$..M_CUST  M_CUST                                 " & vbNewLine _
                                         & "ON                                                       " & vbNewLine _
                                         & "M_GOODS.NRS_BR_CD  = M_CUST.NRS_BR_CD                    " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.CUST_CD_L  = M_CUST.CUST_CD_L                    " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.CUST_CD_M  = M_CUST.CUST_CD_M                    " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.CUST_CD_S  = M_CUST.CUST_CD_S                    " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.CUST_CD_SS  = M_CUST.CUST_CD_SS                  " & vbNewLine _
                                         & "WHERE                                                    " & vbNewLine _
                                         & "M_SAGYO.SAGYO_CD  = @SAGYO_CD                            " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.NRS_BR_CD  = @NRS_BR_CD                          " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.GOODS_CD_NRS  = @GOODS_CD_NRS                    " & vbNewLine

#End Region

#Region "UNSO_L"

    Private Const SQL_INSERT_UNSO_L As String = "INSERT INTO $LM_TRN$..F_UNSO_L" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",UNSO_NO_L                    " & vbNewLine _
                                              & ",YUSO_BR_CD                   " & vbNewLine _
                                              & ",INOUTKA_NO_L                 " & vbNewLine _
                                              & ",TRIP_NO                      " & vbNewLine _
                                              & ",UNSO_CD                      " & vbNewLine _
                                              & ",UNSO_BR_CD                   " & vbNewLine _
                                              & ",BIN_KB                       " & vbNewLine _
                                              & ",JIYU_KB                      " & vbNewLine _
                                              & ",DENP_NO                      " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE              " & vbNewLine _
                                              & ",OUTKA_PLAN_TIME              " & vbNewLine _
                                              & ",ARR_PLAN_DATE                " & vbNewLine _
                                              & ",ARR_PLAN_TIME                " & vbNewLine _
                                              & ",ARR_ACT_TIME                 " & vbNewLine _
                                              & ",CUST_CD_L                    " & vbNewLine _
                                              & ",CUST_CD_M                    " & vbNewLine _
                                              & ",CUST_REF_NO                  " & vbNewLine _
                                              & ",SHIP_CD                      " & vbNewLine _
                                              & ",ORIG_CD                      " & vbNewLine _
                                              & ",DEST_CD                      " & vbNewLine _
                                              & ",UNSO_PKG_NB                  " & vbNewLine _
                                              & ",NB_UT                        " & vbNewLine _
                                              & ",UNSO_WT                      " & vbNewLine _
                                              & ",UNSO_ONDO_KB                 " & vbNewLine _
                                              & ",PC_KB                        " & vbNewLine _
                                              & ",TARIFF_BUNRUI_KB             " & vbNewLine _
                                              & ",VCLE_KB                      " & vbNewLine _
                                              & ",MOTO_DATA_KB                 " & vbNewLine _
                                              & ",TAX_KB                       " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",SEIQ_TARIFF_CD               " & vbNewLine _
                                              & ",SEIQ_ETARIFF_CD              " & vbNewLine _
                                              & ",AD_3                         " & vbNewLine _
                                              & ",UNSO_TEHAI_KB                " & vbNewLine _
                                              & ",BUY_CHU_NO                   " & vbNewLine _
                                              & ",AREA_CD                      " & vbNewLine _
                                              & ",TYUKEI_HAISO_FLG             " & vbNewLine _
                                              & ",SYUKA_TYUKEI_CD              " & vbNewLine _
                                              & ",HAIKA_TYUKEI_CD              " & vbNewLine _
                                              & ",TRIP_NO_SYUKA                " & vbNewLine _
                                              & ",TRIP_NO_TYUKEI               " & vbNewLine _
                                              & ",TRIP_NO_HAIKA                " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & "--'START UMANO 要望番号1302 支払運賃に伴う修正" & vbNewLine _
                                              & ",SHIHARAI_TARIFF_CD           " & vbNewLine _
                                              & ",SHIHARAI_ETARIFF_CD          " & vbNewLine _
                                              & "--'END   UMANO 要望番号1302 支払運賃に伴う修正" & vbNewLine _
                                              & " )SELECT                      " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@UNSO_NO_L                   " & vbNewLine _
                                              & ",@YUSO_BR_CD                  " & vbNewLine _
                                              & ",@INOUTKA_NO_L                " & vbNewLine _
                                              & ",@TRIP_NO                     " & vbNewLine _
                                              & ",@UNSO_CD                     " & vbNewLine _
                                              & ",@UNSO_BR_CD                  " & vbNewLine _
                                              & ",@BIN_KB                      " & vbNewLine _
                                              & ",@JIYU_KB                     " & vbNewLine _
                                              & ",@DENP_NO                     " & vbNewLine _
                                              & ",@OUTKA_PLAN_DATE             " & vbNewLine _
                                              & ",@OUTKA_PLAN_TIME             " & vbNewLine _
                                              & ",@ARR_PLAN_DATE               " & vbNewLine _
                                              & ",@ARR_PLAN_TIME               " & vbNewLine _
                                              & ",@ARR_ACT_TIME                " & vbNewLine _
                                              & ",@CUST_CD_L                   " & vbNewLine _
                                              & ",@CUST_CD_M                   " & vbNewLine _
                                              & ",@CUST_REF_NO                 " & vbNewLine _
                                              & ",@SHIP_CD                     " & vbNewLine _
                                              & ",M_SOKO.SOKO_DEST_CD          " & vbNewLine _
                                              & ",@DEST_CD                     " & vbNewLine _
                                              & ",@UNSO_PKG_NB                 " & vbNewLine _
                                              & ",@NB_UT                       " & vbNewLine _
                                              & ",@UNSO_WT                     " & vbNewLine _
                                              & ",@UNSO_ONDO_KB                " & vbNewLine _
                                              & ",@PC_KB                       " & vbNewLine _
                                              & ",@TARIFF_BUNRUI_KB            " & vbNewLine _
                                              & ",@VCLE_KB                     " & vbNewLine _
                                              & ",@MOTO_DATA_KB                " & vbNewLine _
                                              & ",@TAX_KB                      " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@SEIQ_TARIFF_CD              " & vbNewLine _
                                              & ",@SEIQ_ETARIFF_CD             " & vbNewLine _
                                              & ",@AD_3                        " & vbNewLine _
                                              & ",@UNSO_TEHAI_KB               " & vbNewLine _
                                              & ",@BUY_CHU_NO                  " & vbNewLine _
                                              & ",@AREA_CD                     " & vbNewLine _
                                              & ",@TYUKEI_HAISO_FLG            " & vbNewLine _
                                              & ",@SYUKA_TYUKEI_CD             " & vbNewLine _
                                              & ",@HAIKA_TYUKEI_CD             " & vbNewLine _
                                              & ",@TRIP_NO_SYUKA               " & vbNewLine _
                                              & ",@TRIP_NO_TYUKEI              " & vbNewLine _
                                              & ",@TRIP_NO_HAIKA               " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & "--'START UMANO 要望番号1302 支払運賃に伴う修正" & vbNewLine _
                                              & ",@SHIHARAI_TARIFF_CD          " & vbNewLine _
                                              & ",@SHIHARAI_ETARIFF_CD         " & vbNewLine _
                                              & "--'END   UMANO 要望番号1302 支払運賃に伴う修正" & vbNewLine _
                                              & "FROM                          " & vbNewLine _
                                              & "$LM_MST$..M_SOKO   M_SOKO     " & vbNewLine _
                                              & ",$LM_MST$..M_CUST  M_CUST     " & vbNewLine _
                                              & "WHERE                                     " & vbNewLine _
                                              & "M_SOKO.NRS_BR_CD  = @NRS_BR_CD            " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_SOKO.WH_CD  = @WH_CD                    " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.NRS_BR_CD  = @NRS_BR_CD            " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.CUST_CD_L  = @CUST_CD_L            " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.CUST_CD_M  = @CUST_CD_M            " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.CUST_CD_S  = '00'                  " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.CUST_CD_SS  = '00'                 " & vbNewLine

#End Region

#Region "F_UNSO_L(まとめ先の更新用)"
    ''' <summary>
    ''' F_UNSO_LのUPDATE文（F_UNSOI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_MATOMESAKI_UNSO_L As String = "UPDATE $LM_TRN$..F_UNSO_L SET       " & vbNewLine _
                                              & " UNSO_WT          = @UNSO_WT            " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD       " & vbNewLine _
                                              & "AND UNSO_NO_L   = @UNSO_NO_L      " & vbNewLine _
                                              & "AND SYS_DEL_FLG     <> '1'      " & vbNewLine
#End Region

#Region "UNSO_M"

    Private Const SQL_INSERT_UNSO_M As String = "INSERT INTO $LM_TRN$..F_UNSO_M" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",UNSO_NO_L                    " & vbNewLine _
                                              & ",UNSO_NO_M                    " & vbNewLine _
                                              & ",GOODS_CD_NRS                 " & vbNewLine _
                                              & ",GOODS_NM                     " & vbNewLine _
                                              & ",UNSO_TTL_NB                  " & vbNewLine _
                                              & ",NB_UT                        " & vbNewLine _
                                              & ",UNSO_TTL_QT                  " & vbNewLine _
                                              & ",QT_UT                        " & vbNewLine _
                                              & ",HASU                         " & vbNewLine _
                                              & ",ZAI_REC_NO                   " & vbNewLine _
                                              & ",UNSO_ONDO_KB                 " & vbNewLine _
                                              & ",IRIME                        " & vbNewLine _
                                              & ",IRIME_UT                     " & vbNewLine _
                                              & ",BETU_WT                      " & vbNewLine _
                                              & ",SIZE_KB                      " & vbNewLine _
                                              & ",ZBUKA_CD                     " & vbNewLine _
                                              & ",ABUKA_CD                     " & vbNewLine _
                                              & ",PKG_NB                       " & vbNewLine _
                                              & ",LOT_NO                       " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@UNSO_NO_L                   " & vbNewLine _
                                              & ",@UNSO_NO_M                   " & vbNewLine _
                                              & ",@GOODS_CD_NRS                " & vbNewLine _
                                              & ",@GOODS_NM                    " & vbNewLine _
                                              & ",@UNSO_TTL_NB                 " & vbNewLine _
                                              & ",@NB_UT                       " & vbNewLine _
                                              & ",@UNSO_TTL_QT                 " & vbNewLine _
                                              & ",@QT_UT                       " & vbNewLine _
                                              & ",@HASU                        " & vbNewLine _
                                              & ",@ZAI_REC_NO                  " & vbNewLine _
                                              & ",@UNSO_ONDO_KB                " & vbNewLine _
                                              & ",@IRIME                       " & vbNewLine _
                                              & ",@IRIME_UT                    " & vbNewLine _
                                              & ",@BETU_WT                     " & vbNewLine _
                                              & ",@SIZE_KB                     " & vbNewLine _
                                              & ",@ZBUKA_CD                    " & vbNewLine _
                                              & ",@ABUKA_CD                    " & vbNewLine _
                                              & ",@PKG_NB                      " & vbNewLine _
                                              & ",@LOT_NO                      " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "UNCHIN"

    Private Const SQL_INSERT_UNCHIN As String = "INSERT INTO $LM_TRN$..F_UNCHIN_TRS" & vbNewLine _
                                              & "(                                 " & vbNewLine _
                                              & " YUSO_BR_CD                       " & vbNewLine _
                                              & ",NRS_BR_CD                        " & vbNewLine _
                                              & ",UNSO_NO_L                        " & vbNewLine _
                                              & ",UNSO_NO_M                        " & vbNewLine _
                                              & ",CUST_CD_L                        " & vbNewLine _
                                              & ",CUST_CD_M                        " & vbNewLine _
                                              & ",CUST_CD_S                        " & vbNewLine _
                                              & ",CUST_CD_SS                       " & vbNewLine _
                                              & ",SEIQ_GROUP_NO                    " & vbNewLine _
                                              & ",SEIQ_GROUP_NO_M                  " & vbNewLine _
                                              & ",SEIQTO_CD                        " & vbNewLine _
                                              & ",UNTIN_CALCULATION_KB             " & vbNewLine _
                                              & ",SEIQ_SYARYO_KB                   " & vbNewLine _
                                              & ",SEIQ_PKG_UT                      " & vbNewLine _
                                              & ",SEIQ_NG_NB                       " & vbNewLine _
                                              & ",SEIQ_DANGER_KB                   " & vbNewLine _
                                              & ",SEIQ_TARIFF_BUNRUI_KB            " & vbNewLine _
                                              & ",SEIQ_TARIFF_CD                   " & vbNewLine _
                                              & ",SEIQ_ETARIFF_CD                  " & vbNewLine _
                                              & ",SEIQ_KYORI                       " & vbNewLine _
                                              & ",SEIQ_WT                          " & vbNewLine _
                                              & ",SEIQ_UNCHIN                      " & vbNewLine _
                                              & ",SEIQ_CITY_EXTC                   " & vbNewLine _
                                              & ",SEIQ_WINT_EXTC                   " & vbNewLine _
                                              & ",SEIQ_RELY_EXTC                   " & vbNewLine _
                                              & ",SEIQ_TOLL                        " & vbNewLine _
                                              & ",SEIQ_INSU                        " & vbNewLine _
                                              & ",SEIQ_FIXED_FLAG                  " & vbNewLine _
                                              & ",DECI_NG_NB                       " & vbNewLine _
                                              & ",DECI_KYORI                       " & vbNewLine _
                                              & ",DECI_WT                          " & vbNewLine _
                                              & ",DECI_UNCHIN                      " & vbNewLine _
                                              & ",DECI_CITY_EXTC                   " & vbNewLine _
                                              & ",DECI_WINT_EXTC                   " & vbNewLine _
                                              & ",DECI_RELY_EXTC                   " & vbNewLine _
                                              & ",DECI_TOLL                        " & vbNewLine _
                                              & ",DECI_INSU                        " & vbNewLine _
                                              & ",KANRI_UNCHIN                     " & vbNewLine _
                                              & ",KANRI_CITY_EXTC                  " & vbNewLine _
                                              & ",KANRI_WINT_EXTC                  " & vbNewLine _
                                              & ",KANRI_RELY_EXTC                  " & vbNewLine _
                                              & ",KANRI_TOLL                       " & vbNewLine _
                                              & ",KANRI_INSU                       " & vbNewLine _
                                              & ",REMARK                           " & vbNewLine _
                                              & ",SIZE_KB                          " & vbNewLine _
                                              & ",TAX_KB                           " & vbNewLine _
                                              & ",SAGYO_KANRI                      " & vbNewLine _
                                              & ",SYS_ENT_DATE                     " & vbNewLine _
                                              & ",SYS_ENT_TIME                     " & vbNewLine _
                                              & ",SYS_ENT_PGID                     " & vbNewLine _
                                              & ",SYS_ENT_USER                     " & vbNewLine _
                                              & ",SYS_UPD_DATE                     " & vbNewLine _
                                              & ",SYS_UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_PGID                     " & vbNewLine _
                                              & ",SYS_UPD_USER                     " & vbNewLine _
                                              & ",SYS_DEL_FLG                      " & vbNewLine _
                                              & " )VALUES(                         " & vbNewLine _
                                              & " @YUSO_BR_CD                      " & vbNewLine _
                                              & ",@NRS_BR_CD                       " & vbNewLine _
                                              & ",@UNSO_NO_L                       " & vbNewLine _
                                              & ",@UNSO_NO_M                       " & vbNewLine _
                                              & ",@CUST_CD_L                       " & vbNewLine _
                                              & ",@CUST_CD_M                       " & vbNewLine _
                                              & ",@CUST_CD_S                       " & vbNewLine _
                                              & ",@CUST_CD_SS                      " & vbNewLine _
                                              & ",@SEIQ_GROUP_NO                   " & vbNewLine _
                                              & ",@SEIQ_GROUP_NO_M                 " & vbNewLine _
                                              & ",@SEIQTO_CD                       " & vbNewLine _
                                              & ",@UNTIN_CALCULATION_KB            " & vbNewLine _
                                              & ",@SEIQ_SYARYO_KB                  " & vbNewLine _
                                              & ",@SEIQ_PKG_UT                     " & vbNewLine _
                                              & ",@SEIQ_NG_NB                      " & vbNewLine _
                                              & ",@SEIQ_DANGER_KB                  " & vbNewLine _
                                              & ",@SEIQ_TARIFF_BUNRUI_KB           " & vbNewLine _
                                              & ",@SEIQ_TARIFF_CD                  " & vbNewLine _
                                              & ",@SEIQ_ETARIFF_CD                 " & vbNewLine _
                                              & ",@SEIQ_KYORI                      " & vbNewLine _
                                              & ",@SEIQ_WT                         " & vbNewLine _
                                              & ",@SEIQ_UNCHIN                     " & vbNewLine _
                                              & ",@SEIQ_CITY_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_WINT_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_RELY_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_TOLL                       " & vbNewLine _
                                              & ",@SEIQ_INSU                       " & vbNewLine _
                                              & ",@SEIQ_FIXED_FLAG                 " & vbNewLine _
                                              & ",@DECI_NG_NB                      " & vbNewLine _
                                              & ",@DECI_KYORI                      " & vbNewLine _
                                              & ",@DECI_WT                         " & vbNewLine _
                                              & ",@DECI_UNCHIN                     " & vbNewLine _
                                              & ",@DECI_CITY_EXTC                  " & vbNewLine _
                                              & ",@DECI_WINT_EXTC                  " & vbNewLine _
                                              & ",@DECI_RELY_EXTC                  " & vbNewLine _
                                              & ",@DECI_TOLL                       " & vbNewLine _
                                              & ",@DECI_INSU                       " & vbNewLine _
                                              & ",@KANRI_UNCHIN                    " & vbNewLine _
                                              & ",@KANRI_CITY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_WINT_EXTC                 " & vbNewLine _
                                              & ",@KANRI_RELY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_TOLL                      " & vbNewLine _
                                              & ",@KANRI_INSU                      " & vbNewLine _
                                              & ",@REMARK                          " & vbNewLine _
                                              & ",@SIZE_KB                         " & vbNewLine _
                                              & ",@TAX_KB                          " & vbNewLine _
                                              & ",@SAGYO_KANRI                     " & vbNewLine _
                                              & ",@SYS_ENT_DATE                    " & vbNewLine _
                                              & ",@SYS_ENT_TIME                    " & vbNewLine _
                                              & ",@SYS_ENT_PGID                    " & vbNewLine _
                                              & ",@SYS_ENT_USER                    " & vbNewLine _
                                              & ",@SYS_UPD_DATE                    " & vbNewLine _
                                              & ",@SYS_UPD_TIME                    " & vbNewLine _
                                              & ",@SYS_UPD_PGID                    " & vbNewLine _
                                              & ",@SYS_UPD_USER                    " & vbNewLine _
                                              & ",@SYS_DEL_FLG                     " & vbNewLine _
                                              & ")                                 " & vbNewLine

#End Region

#End Region

#Region "一括変更処理 マスタチェック用SQL"

    ''' <summary>
    ''' 運送会社マスタﾁｪｯｸ用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UNSOCO_MST_CHECK As String = "SELECT                              " & vbNewLine _
                                                 & "   COUNT(UNSOCO_CD)  AS REC_CNT     " & vbNewLine _
                                                 & "FROM                                " & vbNewLine _
                                                 & "$LM_MST$..M_UNSOCO                  " & vbNewLine _
                                                 & "WHERE                               " & vbNewLine _
                                                 & "    NRS_BR_CD      = @NRS_BR_CD     " & vbNewLine _
                                                 & "AND UNSOCO_CD      = @UNSOCO_CD     " & vbNewLine _
                                                 & "AND UNSOCO_BR_CD   = @UNSOCO_BR_CD  " & vbNewLine

    ''' <summary>
    ''' 運送会社名取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UNSONM_GET As String = "SELECT                                          " & vbNewLine _
                                                 & "   COUNT(UNSOCO_CD)  AS REC_CNT           " & vbNewLine _
                                                 & ",UNSOCO_CD        AS EDIT_ITEM_VALUE1     " & vbNewLine _
                                                 & ",UNSOCO_BR_CD     AS EDIT_ITEM_VALUE2     " & vbNewLine _
                                                 & ",UNSOCO_NM        AS EDIT_ITEM_VALUE3     " & vbNewLine _
                                                 & ",UNSOCO_BR_NM     AS EDIT_ITEM_VALUE4     " & vbNewLine _
                                                 & "FROM                                      " & vbNewLine _
                                                 & "$LM_MST$..M_UNSOCO                        " & vbNewLine _
                                                 & "WHERE                                     " & vbNewLine _
                                                 & "    NRS_BR_CD      = @NRS_BR_CD           " & vbNewLine _
                                                 & "AND UNSOCO_CD      = @UNSOCO_CD           " & vbNewLine _
                                                 & "AND UNSOCO_BR_CD   = @UNSOCO_BR_CD        " & vbNewLine _
                                                 & "GROUP BY                                  " & vbNewLine _
                                                 & " UNSOCO_CD                                " & vbNewLine _
                                                 & ",UNSOCO_BR_CD                             " & vbNewLine _
                                                 & ",UNSOCO_NM                                " & vbNewLine _
                                                 & ",UNSOCO_BR_NM                             " & vbNewLine


    ''' <summary>
    ''' 排他チェック用(WHERE句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_HAITA As String = " WHERE                                " & vbNewLine _
                                            & " NRS_BR_CD         = @NRS_BR_CD       " & vbNewLine _
                                            & " AND SYS_UPD_DATE  = @SYS_UPD_DATE    " & vbNewLine _
                                            & " AND SYS_UPD_TIME  = @SYS_UPD_TIME    " & vbNewLine


#End Region

#Region "実績作成処理 データ抽出用SQL"

#Region "H_SENDOUTEDI_SFJ SELECT句"

    Private Const SQL_SELECT_SAKURA_SEND_DATA As String = " SELECT                                                                                 " & vbNewLine _
                                                        & " '0'                                                                AS DEL_KB           " & vbNewLine _
                                                        & ",EM.NRS_BR_CD                                                       AS NRS_BR_CD        " & vbNewLine _
                                                        & ",EM.EDI_CTL_NO                                                      AS EDI_CTL_NO       " & vbNewLine _
                                                        & ",EM.EDI_CTL_NO_CHU                                                  AS EDI_CTL_NO_CHU   " & vbNewLine _
                                                        & ",'000'                                                              AS EDI_CTL_NO_SHO   " & vbNewLine _
                                                        & ",ISNULL(OL.WH_CD,EL.WH_CD)                                          AS WH_CD            " & vbNewLine _
                                                        & ",'0000'                                                             AS RECORD_NO        " & vbNewLine _
                                                        & ",LEFT(EM.FREE_C01,12)                                               AS SIJI_NO          " & vbNewLine _
                                                        & ",RIGHT(RTRIM('000'+CONVERT(CHAR,CONVERT(INT,EM.FREE_N01))),3)       AS GYO_NO           " & vbNewLine _
                                                        & ",LEFT(EM.CUST_GOODS_CD,13)                                          AS CUST_GOODS_CD    " & vbNewLine _
                                                        & ",ISNULL(OS.SERIAL_NO,'')                                            AS SERIAL_NO        " & vbNewLine _
                                                        & ",ISNULL(OS.ALCTD_NB,DSFJ.KOSU)                                      AS JISSEKI_QT       " & vbNewLine _
                                                        & ",EM.FREE_C09                                                        AS JISSEKI_NISUGATA " & vbNewLine _
                                                        & ",1                                                                  AS JISSEKI_IRISU    " & vbNewLine _
                                                        & ",ISNULL(OS.ALCTD_NB,DSFJ.SURYO)                                     AS JISSEKI_TOTAL_NB " & vbNewLine _
                                                        & ",'N' + EM.OUTKA_CTL_NO                                              AS JISSEKI_NO       " & vbNewLine _
                                                        & ",LEFT(ISNULL(OL2.DENP_NO,''),15)                                    AS DENP_NO          " & vbNewLine _
                                                        & ",LEFT(ISNULL(FL.UNSO_CD,''),3)                                      AS UNSO_CD          " & vbNewLine _
                                                        & ",ISNULL(OL.OUTKA_PLAN_DATE,EL.OUTKA_PLAN_DATE)                      AS OUTKA_PLAN_DATE  " & vbNewLine _
                                                        & ",'2'   		                                                       AS JISSEKI_SHORI_FLG" & vbNewLine

#End Region

    '★★★2011.10.21 要望番号421 修正START

#Region "H_SENDOUTEDI_SFJ FROM句"

    Private Const SQL_FROM_SAKURA_SEND_DATA As String = "FROM                                                               " & vbNewLine _
                                                        & " $LM_TRN$..H_OUTKAEDI_M  EM                                      " & vbNewLine _
                                                        & " INNER JOIN                                                      " & vbNewLine _
                                                        & " $LM_TRN$..H_OUTKAEDI_L  EL                                      " & vbNewLine _
                                                        & " ON                                                              " & vbNewLine _
                                                        & " EM.NRS_BR_CD = EL.NRS_BR_CD                                     " & vbNewLine _
                                                        & " AND                                                             " & vbNewLine _
                                                        & " EM.EDI_CTL_NO = EL.EDI_CTL_NO                                   " & vbNewLine _
                                                        & " LEFT JOIN 						                                " & vbNewLine _
                                                        & " $LM_TRN$..H_OUTKAEDI_DTL_SFJ DSFJ 			                    " & vbNewLine _
                                                        & " ON 							                                    " & vbNewLine _
                                                        & " DSFJ.EDI_CTL_NO = EM.EDI_CTL_NO			                        " & vbNewLine _
                                                        & " AND DSFJ.EDI_CTL_NO_CHU = EM.EDI_CTL_NO_CHU		                " & vbNewLine _
                                                        & " LEFT JOIN                                                       " & vbNewLine _
                                                        & " $LM_TRN$..F_UNSO_L  FL                                          " & vbNewLine _
                                                        & " ON                                                              " & vbNewLine _
                                                        & " EL.NRS_BR_CD = FL.NRS_BR_CD                                     " & vbNewLine _
                                                        & " AND                                                             " & vbNewLine _
                                                        & " EL.OUTKA_CTL_NO = FL.INOUTKA_NO_L                               " & vbNewLine _
                                                        & " AND                                                             " & vbNewLine _
                                                        & " EL.SYS_DEL_FLG = '0'                                            " & vbNewLine _
                                                        & " LEFT JOIN                                                       " & vbNewLine _
                                                        & " $LM_TRN$..C_OUTKA_L  OL                                         " & vbNewLine _
                                                        & " ON                                                              " & vbNewLine _
                                                        & " EM.NRS_BR_CD = OL.NRS_BR_CD                                     " & vbNewLine _
                                                        & " AND                                                             " & vbNewLine _
                                                        & " EM.OUTKA_CTL_NO = OL.OUTKA_NO_L                                 " & vbNewLine _
                                                        & " LEFT JOIN                                                       " & vbNewLine _
                                                        & " $LM_TRN$..C_OUTKA_L  OL2                                        " & vbNewLine _
                                                        & " ON                                                              " & vbNewLine _
                                                        & " EM.NRS_BR_CD = OL2.NRS_BR_CD                                    " & vbNewLine _
                                                        & " AND                                                             " & vbNewLine _
                                                        & " EM.OUTKA_CTL_NO = OL2.OUTKA_NO_L                                " & vbNewLine _
                                                        & " AND                                                             " & vbNewLine _
                                                        & " OL2.SYS_DEL_FLG <> '1'                                          " & vbNewLine _
                                                        & " LEFT JOIN                                                       " & vbNewLine _
                                                        & " $LM_TRN$..C_OUTKA_M  OM                                         " & vbNewLine _
                                                        & " ON                                                              " & vbNewLine _
                                                        & " EM.NRS_BR_CD = OM.NRS_BR_CD                                     " & vbNewLine _
                                                        & " AND                                                             " & vbNewLine _
                                                        & " EM.OUTKA_CTL_NO = OM.OUTKA_NO_L                                 " & vbNewLine _
                                                        & " AND                                                             " & vbNewLine _
                                                        & " EM.OUTKA_CTL_NO_CHU = OM.OUTKA_NO_M                             " & vbNewLine _
                                                        & " AND                                                             " & vbNewLine _
                                                        & " OM.SYS_DEL_FLG <> '1'                                           " & vbNewLine _
                                                        & " LEFT JOIN                                                       " & vbNewLine _
                                                        & "  (SELECT                                                        " & vbNewLine _
                                                        & "    S1.OUTKA_NO_L                                                " & vbNewLine _
                                                        & "    ,S1.OUTKA_NO_M                                               " & vbNewLine _
                                                        & "    ,'1' AS SERIAL_KB                                            " & vbNewLine _
                                                        & "    ,RIGHT(RTRIM(S1.SERIAL_NO),15) AS SERIAL_NO                  " & vbNewLine _
                                                        & "    ,SUM(S1.ALCTD_NB) AS ALCTD_NB                                " & vbNewLine _
                                                        & "    FROM                                                         " & vbNewLine _
                                                        & "    $LM_TRN$..C_OUTKA_S S1                                       " & vbNewLine _
                                                        & "    WHERE                                                        " & vbNewLine _
                                                        & "    S1.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                                        & "    AND                                                          " & vbNewLine _
                                                        & "    S1.OUTKA_NO_L = @OUTKA_CTL_NO                                " & vbNewLine _
                                                        & "    AND                                                          " & vbNewLine _
                                                        & "    S1.SYS_DEL_FLG <> '1'                                        " & vbNewLine _
                                                        & "    GROUP BY                                                     " & vbNewLine _
                                                        & "     S1.OUTKA_NO_L                                               " & vbNewLine _
                                                        & "    ,S1.OUTKA_NO_M                                               " & vbNewLine _
                                                        & "    ,S1.SERIAL_NO                                                " & vbNewLine _
                                                        & "   UNION                                                         " & vbNewLine _
                                                        & "   SELECT                                                        " & vbNewLine _
                                                        & "    S2.OUTKA_NO_L                                                " & vbNewLine _
                                                        & "    ,S2.OUTKA_NO_M                                               " & vbNewLine _
                                                        & "    ,'2' AS SERIAL_KB                                            " & vbNewLine _
                                                        & "    ,RIGHT(RTRIM(S2.LOT_NO),15) AS SERIAL_NO                     " & vbNewLine _
                                                        & "    ,SUM(S2.ALCTD_NB) AS ALCTD_NB                                " & vbNewLine _
                                                        & "    FROM                                                         " & vbNewLine _
                                                        & "    $LM_TRN$..C_OUTKA_S S2                                       " & vbNewLine _
                                                        & "    WHERE                                                        " & vbNewLine _
                                                        & "    S2.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                                        & "    AND                                                          " & vbNewLine _
                                                        & "    S2.OUTKA_NO_L = @OUTKA_CTL_NO                                " & vbNewLine _
                                                        & "    AND                                                          " & vbNewLine _
                                                        & "    S2.SYS_DEL_FLG <> '1'                                        " & vbNewLine _
                                                        & "    GROUP BY                                                     " & vbNewLine _
                                                        & "     S2.OUTKA_NO_L,                                              " & vbNewLine _
                                                        & "     S2.OUTKA_NO_M                                               " & vbNewLine _
                                                        & "    ,S2.LOT_NO                                                   " & vbNewLine _
                                                        & "   UNION                                                         " & vbNewLine _
                                                        & "   SELECT                                                        " & vbNewLine _
                                                        & "    S3.OUTKA_NO_L                                                " & vbNewLine _
                                                        & "    ,S3.OUTKA_NO_M                                               " & vbNewLine _
                                                        & "    ,'0' AS SERIAL_KB                                            " & vbNewLine _
                                                        & "    ,'' AS SERIAL_NO                                             " & vbNewLine _
                                                        & "    ,SUM(S3.ALCTD_NB) AS ALCTD_NB                                " & vbNewLine _
                                                        & "    FROM                                                         " & vbNewLine _
                                                        & "    $LM_TRN$..C_OUTKA_S S3                                       " & vbNewLine _
                                                        & "    WHERE                                                        " & vbNewLine _
                                                        & "    S3.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                                        & "    AND                                                          " & vbNewLine _
                                                        & "    S3.OUTKA_NO_L = @OUTKA_CTL_NO                                " & vbNewLine _
                                                        & "    AND                                                          " & vbNewLine _
                                                        & "    S3.SYS_DEL_FLG <> '1'                                        " & vbNewLine _
                                                        & "    GROUP BY                                                     " & vbNewLine _
                                                        & "     S3.OUTKA_NO_L,                                              " & vbNewLine _
                                                        & "     S3.OUTKA_NO_M                                               " & vbNewLine _
                                                        & "  ) OS                                                           " & vbNewLine _
                                                        & "  ON                                                             " & vbNewLine _
                                                        & "  OM.OUTKA_NO_L = OS.OUTKA_NO_L                                  " & vbNewLine _
                                                        & "  AND                                                            " & vbNewLine _
                                                        & "  OM.OUTKA_NO_M = OS.OUTKA_NO_M                                  " & vbNewLine _
                                                        & "  AND                                                            " & vbNewLine _
                                                        & "  ((EM.FREE_C06 = '1' AND OS.SERIAL_KB = '1')                    " & vbNewLine _
                                                        & "  OR                                                             " & vbNewLine _
                                                        & "   (EM.FREE_C06 = '2' AND OS.SERIAL_KB = '2')                    " & vbNewLine _
                                                        & "  OR                                                             " & vbNewLine _
                                                        & "   (EM.FREE_C06 = '0' AND OS.SERIAL_KB = '0'))                   " & vbNewLine _
                                                        & " WHERE                                                           " & vbNewLine _
                                                        & " EM.NRS_BR_CD = @NRS_BR_CD                                       " & vbNewLine _
                                                        & "  AND                                                            " & vbNewLine _
                                                        & "  EM.EDI_CTL_NO = @EDI_CTL_NO                                    " & vbNewLine _
                                                        & "  AND                                                            " & vbNewLine _
                                                        & "  EM.OUT_KB = '0'                                                " & vbNewLine _
                                                        & "  AND                                                            " & vbNewLine _
                                                        & "  EM.JISSEKI_FLAG = '0'                                          " & vbNewLine _
                                                        & " AND EL.SYS_UPD_DATE  = @SYS_UPD_DATE                            " & vbNewLine _
                                                        & " AND EL.SYS_UPD_TIME  = @SYS_UPD_TIME                            " & vbNewLine _
                                                        & " ORDER BY EM.EDI_CTL_NO_CHU                                      " & vbNewLine

#End Region

    '★★★2011.10.21 要望番号421 修正END

#End Region

    '▼▼▼20011.09.21
#Region "実績作成処理 同一まとめ番号データ取得用SQL"

    ''' <summary>
    ''' 同一まとめ番号データ取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_MATOME As String = " SELECT                                                              " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD                             AS NRS_BR_CD            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.EDI_CTL_NO                            AS EDI_CTL_NO           " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_UPD_DATE                          AS SYS_UPD_DATE         " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_UPD_TIME                          AS SYS_UPD_TIME         " & vbNewLine _
                                            & ",H_OUTKAEDI_L.OUTKA_CTL_NO                          AS OUTKA_CTL_NO         " & vbNewLine _
                                            & ",C_OUTKA_L.SYS_UPD_DATE                             AS OUTKA_SYS_UPD_DATE   " & vbNewLine _
                                            & ",C_OUTKA_L.SYS_UPD_TIME                             AS OUTKA_SYS_UPD_TIME   " & vbNewLine _
                                            & ",H_OUTKAEDI_HED_SFJ.SYS_UPD_DATE                    AS RCV_SYS_UPD_DATE     " & vbNewLine _
                                            & ",H_OUTKAEDI_HED_SFJ.SYS_UPD_TIME                    AS RCV_SYS_UPD_TIME     " & vbNewLine _
                                            & ",C_OUTKA_L.SYS_DEL_FLG                              AS OUTKA_DEL_KB         " & vbNewLine _
                                            & " FROM                                                                       " & vbNewLine _
                                            & " $LM_TRN$..H_OUTKAEDI_L                    H_OUTKAEDI_L                     " & vbNewLine _
                                            & " LEFT JOIN                                                                  " & vbNewLine _
                                            & " $LM_TRN$..C_OUTKA_L                       C_OUTKA_L                        " & vbNewLine _
                                            & " ON                                                                         " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD =C_OUTKA_L.NRS_BR_CD                                " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.OUTKA_CTL_NO =C_OUTKA_L.OUTKA_NO_L                            " & vbNewLine _
                                            & " LEFT JOIN                                                                  " & vbNewLine _
                                            & " $LM_TRN$..H_OUTKAEDI_HED_SFJ                                               " & vbNewLine _
                                            & " ON                                                                         " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_HED_SFJ.NRS_BR_CD                       " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_HED_SFJ.EDI_CTL_NO                     " & vbNewLine _
                                            & " INNER JOIN                                                                 " & vbNewLine _
                                            & " $LM_MST$..M_EDI_CUST                       M_EDI_CUST                      " & vbNewLine _
                                            & " ON                                                                         " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD = M_EDI_CUST.NRS_BR_CD                              " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.WH_CD = M_EDI_CUST.WH_CD                                      " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.CUST_CD_L = M_EDI_CUST.CUST_CD_L                              " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.CUST_CD_M = M_EDI_CUST.CUST_CD_M                              " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " M_EDI_CUST.INOUT_KB = '0'                                                  " & vbNewLine _
                                            & " WHERE                                                                      " & vbNewLine _
                                            & " SUBSTRING(H_OUTKAEDI_L.FREE_C30,4,9) = @MATOME_NO                          " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " (((M_EDI_CUST.FLAG_01 IN ('1','2')                                         " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " C_OUTKA_L.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " C_OUTKA_L.OUTKA_STATE_KB >= '60')                                          " & vbNewLine _
                                            & " OR                                                                         " & vbNewLine _
                                            & " (M_EDI_CUST.FLAG_01 = '2'                                                  " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " (H_OUTKAEDI_L.SYS_DEL_FLG = '1'                                            " & vbNewLine _
                                            & " OR                                                                         " & vbNewLine _
                                            & " C_OUTKA_L.SYS_DEL_FLG = '1'))                                              " & vbNewLine _
                                            & " OR                                                                         " & vbNewLine _
                                            & " (M_EDI_CUST.FLAG_01 = '4'                                                  " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " C_OUTKA_L.SYS_DEL_FLG = '0'))                                              " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " (H_OUTKAEDI_L.JISSEKI_FLAG = '0'))                                         " & vbNewLine


#End Region
    '▲▲▲20011.09.21

    '2001.09.27 START 追加
#Region "出荷取消⇒未登録処理 同一まとめ番号データ取得用SQL"

    ''' <summary>
    ''' 同一まとめ番号データ取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_MATOMETORIKESI As String = " SELECT                                                      " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD                             AS NRS_BR_CD            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.EDI_CTL_NO                            AS EDI_CTL_NO           " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_UPD_DATE                          AS SYS_UPD_DATE         " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_UPD_TIME                          AS SYS_UPD_TIME         " & vbNewLine _
                                            & ",H_OUTKAEDI_L.OUTKA_CTL_NO                          AS OUTKA_CTL_NO         " & vbNewLine _
                                            & ",H_OUTKAEDI_HED_SFJ.SYS_UPD_DATE                    AS RCV_SYS_UPD_DATE     " & vbNewLine _
                                            & ",H_OUTKAEDI_HED_SFJ.SYS_UPD_TIME                    AS RCV_SYS_UPD_TIME     " & vbNewLine _
                                            & " FROM                                                                       " & vbNewLine _
                                            & " $LM_TRN$..H_OUTKAEDI_L                    H_OUTKAEDI_L                     " & vbNewLine _
                                            & " INNER JOIN                                                                 " & vbNewLine _
                                            & " $LM_TRN$..C_OUTKA_L                       C_OUTKA_L                        " & vbNewLine _
                                            & " ON                                                                         " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD =C_OUTKA_L.NRS_BR_CD                                " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.OUTKA_CTL_NO =C_OUTKA_L.OUTKA_NO_L                            " & vbNewLine _
                                            & " INNER JOIN                                                                 " & vbNewLine _
                                            & " $LM_TRN$..H_OUTKAEDI_HED_SFJ                                               " & vbNewLine _
                                            & " ON                                                                         " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_HED_SFJ.NRS_BR_CD                       " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_HED_SFJ.EDI_CTL_NO                     " & vbNewLine _
                                            & " WHERE                                                                      " & vbNewLine _
                                            & " SUBSTRING(H_OUTKAEDI_L.FREE_C30,4,9) = @MATOME_NO                          " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " C_OUTKA_L.OUTKA_NO_L = @OUTKA_CTL_NO                                       " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " C_OUTKA_L.SYS_DEL_FLG = '1'                                                " & vbNewLine _
                                            & " AND C_OUTKA_L.SYS_UPD_DATE = @OUTKA_SYS_UPD_DATE                           " & vbNewLine _
                                            & " AND C_OUTKA_L.SYS_UPD_TIME = @OUTKA_SYS_UPD_TIME                           " & vbNewLine


#End Region
    '2001.09.27 END 追加

#Region "実績作成処理 更新用SQL"

#Region "H_SENDOUTEDI_SFJ"

    ''' <summary>
    ''' INSERT（OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_H_SENDOUTEDI_SFJ As String = "INSERT INTO                  " & vbNewLine _
                                         & "$LM_TRN$..H_SENDOUTEDI_SFJ                  " & vbNewLine _
                                         & "(                                           " & vbNewLine _
                                         & " DEL_KB                                     " & vbNewLine _
                                         & ",NRS_BR_CD                                  " & vbNewLine _
                                         & ",EDI_CTL_NO                                 " & vbNewLine _
                                         & ",EDI_CTL_NO_CHU                             " & vbNewLine _
                                         & ",EDI_CTL_NO_SHO                             " & vbNewLine _
                                         & ",WH_CD                                      " & vbNewLine _
                                         & ",RECORD_NO                                  " & vbNewLine _
                                         & ",SIJI_NO                                    " & vbNewLine _
                                         & ",GYO_NO                                     " & vbNewLine _
                                         & ",CUST_GOODS_CD                              " & vbNewLine _
                                         & ",SERIAL_NO                                  " & vbNewLine _
                                         & ",JISSEKI_QT                                 " & vbNewLine _
                                         & ",JISSEKI_NISUGATA                           " & vbNewLine _
                                         & ",JISSEKI_IRISU                              " & vbNewLine _
                                         & ",JISSEKI_TOTAL_NB                           " & vbNewLine _
                                         & ",JISSEKI_NO                                 " & vbNewLine _
                                         & ",DENP_NO                                    " & vbNewLine _
                                         & ",UNSO_CD                                    " & vbNewLine _
                                         & ",OUTKA_PLAN_DATE                            " & vbNewLine _
                                         & ",JUSHIN_DATE                                " & vbNewLine _
                                         & ",JUSHIN_TIME                                " & vbNewLine _
                                         & ",RECORD_STATUS                              " & vbNewLine _
                                         & ",JISSEKI_SHORI_FLG                          " & vbNewLine _
                                         & ",JISSEKI_USER                               " & vbNewLine _
                                         & ",JISSEKI_DATE                               " & vbNewLine _
                                         & ",JISSEKI_TIME                               " & vbNewLine _
                                         & ",SEND_USER                                  " & vbNewLine _
                                         & ",SEND_DATE                                  " & vbNewLine _
                                         & ",SEND_TIME                                  " & vbNewLine _
                                         & ",CRT_USER                                   " & vbNewLine _
                                         & ",CRT_DATE                                   " & vbNewLine _
                                         & ",CRT_TIME                                   " & vbNewLine _
                                         & ",UPD_USER                                   " & vbNewLine _
                                         & ",UPD_DATE                                   " & vbNewLine _
                                         & ",UPD_TIME                                   " & vbNewLine _
                                         & ",SYS_ENT_DATE                               " & vbNewLine _
                                         & ",SYS_ENT_TIME                               " & vbNewLine _
                                         & ",SYS_ENT_PGID                               " & vbNewLine _
                                         & ",SYS_ENT_USER                               " & vbNewLine _
                                         & ",SYS_UPD_DATE                               " & vbNewLine _
                                         & ",SYS_UPD_TIME                               " & vbNewLine _
                                         & ",SYS_UPD_PGID                               " & vbNewLine _
                                         & ",SYS_UPD_USER                               " & vbNewLine _
                                         & ",SYS_DEL_FLG                                " & vbNewLine _
                                         & ")VALUES(                                    " & vbNewLine _
                                         & " @DEL_KB                                    " & vbNewLine _
                                         & ",@NRS_BR_CD                                 " & vbNewLine _
                                         & ",@EDI_CTL_NO                                " & vbNewLine _
                                         & ",@EDI_CTL_NO_CHU                            " & vbNewLine _
                                         & ",@EDI_CTL_NO_SHO                            " & vbNewLine _
                                         & ",@WH_CD                                     " & vbNewLine _
                                         & ",@RECORD_NO                                 " & vbNewLine _
                                         & ",@SIJI_NO                                   " & vbNewLine _
                                         & ",@GYO_NO                                    " & vbNewLine _
                                         & ",@CUST_GOODS_CD                             " & vbNewLine _
                                         & ",@SERIAL_NO                                 " & vbNewLine _
                                         & ",@JISSEKI_QT                                " & vbNewLine _
                                         & ",@JISSEKI_NISUGATA                          " & vbNewLine _
                                         & ",@JISSEKI_IRISU                             " & vbNewLine _
                                         & ",@JISSEKI_TOTAL_NB                          " & vbNewLine _
                                         & ",@JISSEKI_NO                                " & vbNewLine _
                                         & ",@DENP_NO                                   " & vbNewLine _
                                         & ",@UNSO_CD                                   " & vbNewLine _
                                         & ",@OUTKA_PLAN_DATE                           " & vbNewLine _
                                         & ",@JUSHIN_DATE                               " & vbNewLine _
                                         & ",@JUSHIN_TIME                               " & vbNewLine _
                                         & ",@RECORD_STATUS                             " & vbNewLine _
                                         & ",@JISSEKI_SHORI_FLG                         " & vbNewLine _
                                         & ",@JISSEKI_USER                              " & vbNewLine _
                                         & ",@JISSEKI_DATE                              " & vbNewLine _
                                         & ",@JISSEKI_TIME                              " & vbNewLine _
                                         & ",@SEND_USER                                 " & vbNewLine _
                                         & ",@SEND_DATE                                 " & vbNewLine _
                                         & ",@SEND_TIME                                 " & vbNewLine _
                                         & ",@CRT_USER                                  " & vbNewLine _
                                         & ",@CRT_DATE                                  " & vbNewLine _
                                         & ",@CRT_TIME                                  " & vbNewLine _
                                         & ",@UPD_USER                                  " & vbNewLine _
                                         & ",@UPD_DATE                                  " & vbNewLine _
                                         & ",@UPD_TIME                                  " & vbNewLine _
                                         & ",@SYS_ENT_DATE                              " & vbNewLine _
                                         & ",@SYS_ENT_TIME                              " & vbNewLine _
                                         & ",@SYS_ENT_PGID                              " & vbNewLine _
                                         & ",@SYS_ENT_USER                              " & vbNewLine _
                                         & ",@SYS_UPD_DATE                              " & vbNewLine _
                                         & ",@SYS_UPD_TIME                              " & vbNewLine _
                                         & ",@SYS_UPD_PGID                              " & vbNewLine _
                                         & ",@SYS_UPD_USER                              " & vbNewLine _
                                         & ",@SYS_DEL_FLG                               " & vbNewLine _
                                         & ")                                           " & vbNewLine
#End Region

#Region "H_OUTKAEDI_L"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKISAKUSEI_EDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                           " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                           " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                             " & vbNewLine
#End Region

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKISAKUSEI_EDI_M As String = "UPDATE $LM_TRN$..H_OUTKAEDI_M SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                           " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                           " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                             " & vbNewLine _
                                              & "AND JISSEKI_FLAG   = '0'                                    " & vbNewLine _
                                              & "AND OUT_KB         = '0'                                     " & vbNewLine

#End Region

#Region "H_OUTKAEDI_HED_SFJ"
    ''' <summary>
    ''' サクラEDI受信HEDのUPDATE文（H_OUTKAEDI_HED_SFJ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKISAKUSEI_EDI_RCV_HED As String = "UPDATE $LM_TRN$..H_OUTKAEDI_HED_SFJ SET       " & vbNewLine _
                                              & " UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                   " & vbNewLine _
                                              & "--2011.09.30削除 サクラの場合は取消データも実績作成するのでSYS_DEL_FLGの条件は見ない" & vbNewLine _
                                              & "--AND SYS_DEL_FLG    <> '1'                          " & vbNewLine

#End Region

#Region "H_OUTKAEDI_DTL_SFJ"

    ''' <summary>
    ''' サクラEDI受信DTLのUPDATE文（H_OUTKAEDI_DTL_SFJ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKISAKUSEI_EDI_RCV_DTL As String = "UPDATE $LM_TRN$..H_OUTKAEDI_DTL_SFJ SET       " & vbNewLine _
                                                  & " JISSEKI_SHORI_FLG      = @JISSEKI_SHORI_FLG       	              " & vbNewLine _
                                                  & ",JISSEKI_USER           = @JISSEKI_USER                              " & vbNewLine _
                                                  & ",JISSEKI_DATE           = @JISSEKI_DATE                              " & vbNewLine _
                                                  & ",JISSEKI_TIME           = @JISSEKI_TIME                              " & vbNewLine _
                                                  & ",UPD_USER               = @UPD_USER                                  " & vbNewLine _
                                                  & ",UPD_DATE               = @UPD_DATE                                  " & vbNewLine _
                                                  & ",UPD_TIME               = @UPD_TIME                                  " & vbNewLine _
                                                  & ",SYS_UPD_DATE           = @SYS_UPD_DATE                              " & vbNewLine _
                                                  & ",SYS_UPD_TIME           = @SYS_UPD_TIME                              " & vbNewLine _
                                                  & ",SYS_UPD_PGID           = @SYS_UPD_PGID                              " & vbNewLine _
                                                  & ",SYS_UPD_USER           = @SYS_UPD_USER                              " & vbNewLine _
                                                  & "WHERE   NRS_BR_CD       = @NRS_BR_CD                                 " & vbNewLine _
                                                  & "AND EDI_CTL_NO          = @EDI_CTL_NO                                " & vbNewLine _
                                                  & "AND JISSEKI_SHORI_FLG   = '1'      				                  " & vbNewLine _
                                                  & "--2011.09.30削除 サクラの場合は取消データも実績作成するのでSYS_DEL_FLGの条件は見ない" & vbNewLine _
                                                  & "--AND SYS_DEL_FLG         <> '1'      				                  " & vbNewLine
#End Region

#Region "C_OUTKA_L"
    ''' <summary>
    ''' C_OUTKA_LのUPDATE文（C_OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKISAKUSEI_OUTKA_L As String = "UPDATE $LM_TRN$..C_OUTKA_L SET       " & vbNewLine _
                                              & " OUTKA_STATE_KB          = @OUTKA_STATE_KB           " & vbNewLine _
                                              & ",HOKOKU_DATE          = @HOKOKU_DATE           " & vbNewLine _
                                              & ",HOU_USER          = @HOU_USER           " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD       " & vbNewLine _
                                              & "AND OUTKA_NO_L     = @OUTKA_NO_L      " & vbNewLine _
                                              & "--2011.09.30削除 サクラの場合は取消データも実績作成するのでSYS_DEL_FLGの条件は見ない" & vbNewLine _
                                              & "--AND SYS_DEL_FLG     <> '1'      " & vbNewLine
#End Region


#End Region

#Region "EDI取消,EDI取消⇒未登録処理 更新用SQL"

#Region "OUTKAEDI_L(EDI取消、EDI取消⇒未登録)"
    ''' <summary>
    ''' EDI出荷(大)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_EDITORIKESI_EDI_L As String = " UPDATE                                   " & vbNewLine _
                                         & " $LM_TRN$..H_OUTKAEDI_L                                 " & vbNewLine _
                                         & " SET                                                   " & vbNewLine _
                                         & " DEL_KB = @DEL_KB                                      " & vbNewLine _
                                         & ",UPD_USER = @UPD_USER                                  " & vbNewLine _
                                         & ",UPD_DATE = @UPD_DATE                                  " & vbNewLine _
                                         & ",UPD_TIME = @UPD_TIME                                  " & vbNewLine _
                                         & ",SYS_UPD_PGID = @SYS_UPD_PGID                          " & vbNewLine _
                                         & ",SYS_UPD_USER = @SYS_UPD_USER                          " & vbNewLine _
                                         & ",SYS_UPD_DATE = @SYS_UPD_DATE                          " & vbNewLine _
                                         & ",SYS_UPD_TIME = @SYS_UPD_TIME                          " & vbNewLine _
                                         & ",SYS_DEL_FLG  = @SYS_DEL_FLG                           " & vbNewLine _
                                         & " WHERE                                                 " & vbNewLine _
                                         & " NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                         & " AND                                                   " & vbNewLine _
                                         & " EDI_CTL_NO = @EDI_CTL_NO                              " & vbNewLine


#End Region

#Region "OUTKAEDI_M(EDI取消、EDI取消⇒未登録)"
    ''' <summary>
    ''' EDI出荷(中)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_EDITORIKESI_EDI_M As String = " UPDATE                                   " & vbNewLine _
                                         & " $LM_TRN$..H_OUTKAEDI_M                                 " & vbNewLine _
                                         & " SET                                                   " & vbNewLine _
                                         & " DEL_KB = @DEL_KB                                      " & vbNewLine _
                                         & ",UPD_USER = @UPD_USER                                  " & vbNewLine _
                                         & ",UPD_DATE = @UPD_DATE                                  " & vbNewLine _
                                         & ",UPD_TIME = @UPD_TIME                                  " & vbNewLine _
                                         & ",SYS_UPD_PGID = @SYS_UPD_PGID                          " & vbNewLine _
                                         & ",SYS_UPD_USER = @SYS_UPD_USER                          " & vbNewLine _
                                         & ",SYS_UPD_DATE = @SYS_UPD_DATE                          " & vbNewLine _
                                         & ",SYS_UPD_TIME = @SYS_UPD_TIME                          " & vbNewLine _
                                         & ",SYS_DEL_FLG  = @SYS_DEL_FLG                           " & vbNewLine _
                                         & " WHERE                                                 " & vbNewLine _
                                         & " NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                         & " AND                                                   " & vbNewLine _
                                         & " EDI_CTL_NO = @EDI_CTL_NO                              " & vbNewLine

#End Region

#Region "RCV_HED(EDI取消、EDI取消⇒未登録)"
    ''' <summary>
    ''' EDI受信(HED)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_EDITORIKESI_RCV_HED As String = "UPDATE                                " & vbNewLine _
                                              & " $LM_TRN$..H_OUTKAEDI_HED_SFJ                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " DEL_KB            = @DEL_KB                       " & vbNewLine _
                                              & ",DELETE_USER       = @DELETE_USER                  " & vbNewLine _
                                              & ",DELETE_DATE       = @DELETE_DATE                  " & vbNewLine _
                                              & ",DELETE_TIME       = @DELETE_TIME                  " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG       = @SYS_DEL_FLG                  " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine


#End Region

#Region "RCV_DTL(EDI取消、EDI取消⇒未登録)"
    ''' <summary>
    ''' EDI受信(DTL)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_EDITORIKESI_RCV_DTL As String = "UPDATE                                " & vbNewLine _
                                              & " $LM_TRN$..H_OUTKAEDI_DTL_SFJ                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " DEL_KB            = @DEL_KB                       " & vbNewLine _
                                              & ",DELETE_USER       = @DELETE_USER                  " & vbNewLine _
                                              & ",DELETE_DATE       = @DELETE_DATE                  " & vbNewLine _
                                              & ",DELETE_TIME       = @DELETE_TIME                  " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG       = @SYS_DEL_FLG                  " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine


#End Region

#End Region

#Region "実績取消処理 更新用SQL"

#Region "H_OUTKAEDI_L"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIDELEDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET        " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                    " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                    " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                    " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                   " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                  " & vbNewLine
#End Region

#Region "H_OUTKAEDI_HED_SFJ"
    ''' <summary>
    ''' サクラEDI受信HEDのUPDATE文（H_OUTKAEDI_HED_SFJ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKICANSEL_EDI_RCV_HED As String = "UPDATE $LM_TRN$..H_OUTKAEDI_HED_SFJ SET       " & vbNewLine _
                                              & " UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                   " & vbNewLine

#End Region

#Region "H_OUTKAEDI_DTL_SFJ"
    ''' <summary>
    ''' サクラEDI受信DTLのUPDATE文（H_OUTKAEDI_DTL_SFJ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKICANSEL_EDI_RCV_DTL As String = "UPDATE $LM_TRN$..H_OUTKAEDI_DTL_SFJ SET       " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG       	    " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                   " & vbNewLine



#End Region

#End Region

#Region "実績作成済⇒実績未,実績送信済⇒実績未処理 更新用SQL"

#Region "H_OUTKAEDI_L"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                           " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                           " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                             " & vbNewLine
#End Region

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_M As String = "UPDATE $LM_TRN$..H_OUTKAEDI_M SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                           " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                           " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                             " & vbNewLine _
                                              & "AND JISSEKI_FLAG   <> '9'                                    " & vbNewLine _
                                              & "AND OUT_KB         = '0'                                     " & vbNewLine

#End Region

#Region "H_OUTKAEDI_HED_SFJ"
    ''' <summary>
    ''' サクラEDI受信HEDのUPDATE文（H_OUTKAEDI_HED_SFJ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_RCV_HED As String = "UPDATE $LM_TRN$..H_OUTKAEDI_HED_SFJ SET       " & vbNewLine _
                                              & " UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                   " & vbNewLine _
                                              & "--2011.09.30削除 サクラの場合は取消データも実績作成するのでSYS_DEL_FLGの条件は見ない" & vbNewLine _
                                              & "--AND SYS_DEL_FLG    <> '1'                          " & vbNewLine

#End Region

#Region "H_OUTKAEDI_DTL_SFJ"
    ''' <summary>
    ''' サクラEDI受信DTLのUPDATE文（H_OUTKAEDI_DTL_SFJ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_RCV_DTL As String = "UPDATE $LM_TRN$..H_OUTKAEDI_DTL_SFJ SET       " & vbNewLine _
                                                  & " JISSEKI_SHORI_FLG      = @JISSEKI_SHORI_FLG       	              " & vbNewLine _
                                                  & ",JISSEKI_USER           = @JISSEKI_USER                              " & vbNewLine _
                                                  & ",JISSEKI_DATE           = @JISSEKI_DATE                              " & vbNewLine _
                                                  & ",JISSEKI_TIME           = @JISSEKI_TIME                              " & vbNewLine _
                                                  & ",UPD_USER               = @UPD_USER                                  " & vbNewLine _
                                                  & ",UPD_DATE               = @UPD_DATE                                  " & vbNewLine _
                                                  & ",UPD_TIME               = @UPD_TIME                                  " & vbNewLine _
                                                  & ",SYS_UPD_DATE           = @SYS_UPD_DATE                              " & vbNewLine _
                                                  & ",SYS_UPD_TIME           = @SYS_UPD_TIME                              " & vbNewLine _
                                                  & ",SYS_UPD_PGID           = @SYS_UPD_PGID                              " & vbNewLine _
                                                  & ",SYS_UPD_USER           = @SYS_UPD_USER                              " & vbNewLine _
                                                  & "WHERE   NRS_BR_CD       = @NRS_BR_CD                                 " & vbNewLine _
                                                  & "AND EDI_CTL_NO          = @EDI_CTL_NO                                " & vbNewLine _
                                                  & "AND JISSEKI_SHORI_FLG   = '2'      				                  " & vbNewLine _
                                                  & "--2011.09.30削除 サクラの場合は取消データも実績作成するのでSYS_DEL_FLGの条件は見ない" & vbNewLine _
                                                  & "--AND SYS_DEL_FLG         <> '1'      				                  " & vbNewLine

    Private Const SQL_UPDATE_JISSEKISOUSINZUMI_JISSEKIMI_EDI_RCV_DTL As String = "UPDATE $LM_TRN$..H_OUTKAEDI_DTL_SFJ SET           " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG          = @JISSEKI_SHORI_FLG       	                        " & vbNewLine _
                                              & ",JISSEKI_USER               = @JISSEKI_USER                                        " & vbNewLine _
                                              & ",JISSEKI_DATE               = @JISSEKI_DATE                                        " & vbNewLine _
                                              & ",JISSEKI_TIME               = @JISSEKI_TIME                                        " & vbNewLine _
                                              & ",SEND_USER                  = @SEND_USER                                           " & vbNewLine _
                                              & ",SEND_DATE                  = @SEND_DATE                                           " & vbNewLine _
                                              & ",SEND_TIME                  = @SEND_TIME                                           " & vbNewLine _
                                              & ",UPD_USER                   = @UPD_USER                                            " & vbNewLine _
                                              & ",UPD_DATE                   = @UPD_DATE                                            " & vbNewLine _
                                              & ",UPD_TIME                   = @UPD_TIME                                            " & vbNewLine _
                                              & ",SYS_UPD_DATE               = @SYS_UPD_DATE                                        " & vbNewLine _
                                              & ",SYS_UPD_TIME               = @SYS_UPD_TIME                                        " & vbNewLine _
                                              & ",SYS_UPD_PGID               = @SYS_UPD_PGID                                        " & vbNewLine _
                                              & ",SYS_UPD_USER               = @SYS_UPD_USER                                        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD           = @NRS_BR_CD                                           " & vbNewLine _
                                              & "AND EDI_CTL_NO              = @EDI_CTL_NO                                          " & vbNewLine _
                                              & "AND JISSEKI_SHORI_FLG       = '3'                                                  " & vbNewLine _
                                              & "--2011.09.30削除 サクラの場合は取消データも実績作成するのでSYS_DEL_FLGの条件は見ない              " & vbNewLine _
                                              & "--AND SYS_DEL_FLG             <> '1'                                                 " & vbNewLine


#End Region

#Region "H_SENDOUTEDI_SFJ"
    ''' <summary>
    ''' サクラEDI送信TBLのDELETE文（H_SENDOUTEDI_SFJ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_JISSEKIZUMI_JISSEKIMI_EDI_SEND As String = "DELETE $LM_TRN$..H_SENDOUTEDI_SFJ       " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                   " & vbNewLine _
                                              & "AND SYS_DEL_FLG     <> '1'      " & vbNewLine

#End Region

#Region "C_OUTKA_L"
    ''' <summary>
    ''' C_OUTKA_LのUPDATE文（C_OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_OUTKA_L As String = "UPDATE $LM_TRN$..C_OUTKA_L SET       " & vbNewLine _
                                              & " OUTKA_STATE_KB          = @OUTKA_STATE_KB           " & vbNewLine _
                                              & ",HOKOKU_DATE          = @HOKOKU_DATE           " & vbNewLine _
                                              & ",HOU_USER          = @HOU_USER           " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD       " & vbNewLine _
                                              & "AND OUTKA_NO_L     = @OUTKA_NO_L      " & vbNewLine _
                                              & "--2011.09.30削除 サクラの場合は取消データも実績作成するのでSYS_DEL_FLGの条件は見ない" & vbNewLine _
                                              & "--AND SYS_DEL_FLG     <> '1'      " & vbNewLine
#End Region

#End Region

#Region "実績送信済⇒送信待処理 更新用SQL"

#Region "H_OUTKAEDI_L"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_SOUSINMACHI_EDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                             " & vbNewLine
#End Region

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' H_OUTKAEDI_MのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_SOUSINMACHI_EDI_M As String = "UPDATE $LM_TRN$..H_OUTKAEDI_M SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                             " & vbNewLine


#End Region

#Region "H_OUTKAEDI_HED_SFJ"
    ''' <summary>
    ''' H_OUTKAEDI_HED_SFJ(実績送信済⇒送信未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_HED As String = "UPDATE                                 " & vbNewLine _
                                              & " $LM_TRN$..H_OUTKAEDI_HED_SFJ                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & "--2011.09.30削除 サクラの場合は取消データも実績作成するのでSYS_DEL_FLGの条件は見ない" & vbNewLine _
                                              & "-- AND                                               " & vbNewLine _
                                              & "-- SYS_DEL_FLG     <> '1'                            " & vbNewLine


#End Region

#Region "H_OUTKAEDI_DTL_SFJ"
    ''' <summary>
    ''' H_OUTKAEDI_DTL_SFJ(実績送信済⇒送信待)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_DTL As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..H_OUTKAEDI_DTL_SFJ                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",SEND_USER         = @SEND_USER                    " & vbNewLine _
                                              & ",SEND_DATE         = @SEND_DATE                    " & vbNewLine _
                                              & ",SEND_TIME         = @SEND_TIME                    " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG  = '3'                          " & vbNewLine _
                                              & "--2011.09.30削除 サクラの場合は取消データも実績作成するのでSYS_DEL_FLGの条件は見ない" & vbNewLine _
                                              & "-- AND                                               " & vbNewLine _
                                              & "-- SYS_DEL_FLG     <> '1'                            " & vbNewLine


#End Region

#Region "H_SENDOUTEDI_SFJ"
    ''' <summary>
    ''' H_SENDOUTEDI_SFJ(実績送信済⇒送信待)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIMODOSI_SEND As String = "UPDATE                                    " & vbNewLine _
                                              & "$LM_TRN$..H_SENDOUTEDI_SFJ                         " & vbNewLine _
                                              & "SET                                                " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",SEND_USER         = @SEND_USER                    " & vbNewLine _
                                              & ",SEND_DATE         = @SEND_DATE                    " & vbNewLine _
                                              & ",SEND_TIME         = @SEND_TIME                    " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine


#End Region

#End Region

#Region "出荷取消⇒未登録"

#Region "H_OUTKAEDI_L"

    ''' <summary>
    ''' OUTKAEDI_L(出荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_TOUROKUMI_EDI_L As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..H_OUTKAEDI_L                             " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " OUTKA_CTL_NO      = ''                            " & vbNewLine _
                                              & ",FREE_C30          = ''                            " & vbNewLine _
                                              & ",OUT_FLAG          = @OUT_FLAG                     " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & "-- OUTKA_CTL_NO      = @OUTKA_CTL_NO                 " & vbNewLine _
                                              & " EDI_CTL_NO      = @EDI_CTL_NO                 " & vbNewLine


#End Region

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' OUTKAEDI_M(出荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_TOUROKUMI_EDI_M As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..H_OUTKAEDI_M                            " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " OUTKA_CTL_NO      = ''                            " & vbNewLine _
                                              & ",OUTKA_CTL_NO_CHU  = ''                            " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & "-- OUTKA_CTL_NO      = @OUTKA_CTL_NO                 " & vbNewLine _
                                              & " EDI_CTL_NO      = @EDI_CTL_NO                     " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " OUT_KB            = '0'                           " & vbNewLine

#End Region

#Region "H_OUTKAEDI_HED_SFJ"
    ''' <summary>
    ''' H_OUTKAEDI_HED_SFJ(出荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_TOUROKUMI_RCV_HED As String = "UPDATE                                     " & vbNewLine _
                                              & " $LM_TRN$..H_OUTKAEDI_HED_SFJ                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " OUTKA_CTL_NO      = 'Y00000000'                " & vbNewLine _
                                              & ",OUTKA_USER        = @OUTKA_USER                    " & vbNewLine _
                                              & ",OUTKA_DATE        = @OUTKA_DATE                    " & vbNewLine _
                                              & ",OUTKA_TIME        = @OUTKA_TIME                    " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & "-- OUTKA_CTL_NO      = @OUTKA_CTL_NO                   " & vbNewLine _
                                              & " EDI_CTL_NO      = @EDI_CTL_NO                 " & vbNewLine


#End Region

#Region "H_OUTKAEDI_DTL_SFJ"
    ''' <summary>
    ''' H_OUTKAEDI_DTL_SFJ(出荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_TOUROKUMI_RCV_DTL As String = "UPDATE                                     " & vbNewLine _
                                              & " $LM_TRN$..H_OUTKAEDI_DTL_SFJ                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " OUTKA_CTL_NO      = 'Y00000000'                   " & vbNewLine _
                                              & ",OUTKA_CTL_NO_CHU  = '000'                         " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & "-- OUTKA_CTL_NO      = @OUTKA_CTL_NO                   " & vbNewLine _
                                              & " EDI_CTL_NO      = @EDI_CTL_NO                 " & vbNewLine


#End Region

#End Region

#Region "更新共通"

    Private Const SQL_UPDATE As String = ",SYS_UPD_DATE      = @SYS_UPD_DATE" & vbNewLine _
                                       & ",SYS_UPD_TIME      = @SYS_UPD_TIME" & vbNewLine _
                                       & ",SYS_UPD_PGID      = @SYS_UPD_PGID" & vbNewLine _
                                       & ",SYS_UPD_USER      = @SYS_UPD_USER" & vbNewLine _
                                       & "WHERE NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                       & "  AND EDI_CTL_NO   = @EDI_CTL_NO   " & vbNewLine _
                                       & "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                       & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "届先マスタ自動追加/更新"

    ''' <summary>
    ''' 届先マスタINSERT文（M_DEST）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_M_DEST As String = "INSERT INTO $LM_MST$..M_DEST        " & vbNewLine _
                                       & "(                                   " & vbNewLine _
                                       & "      NRS_BR_CD                     " & vbNewLine _
                                       & "      ,CUST_CD_L                    " & vbNewLine _
                                       & "      ,DEST_CD                      " & vbNewLine _
                                       & "      ,EDI_CD                       " & vbNewLine _
                                       & "      ,DEST_NM                      " & vbNewLine _
                                       & "      ,ZIP                          " & vbNewLine _
                                       & "      ,AD_1                         " & vbNewLine _
                                       & "      ,AD_2                         " & vbNewLine _
                                       & "      ,AD_3                         " & vbNewLine _
                                       & "      ,CUST_DEST_CD                 " & vbNewLine _
                                       & "      ,SALES_CD                     " & vbNewLine _
                                       & "      ,SP_NHS_KB                    " & vbNewLine _
                                       & "      ,COA_YN                       " & vbNewLine _
                                       & "      ,SP_UNSO_CD                   " & vbNewLine _
                                       & "      ,SP_UNSO_BR_CD                " & vbNewLine _
                                       & "      ,DELI_ATT                     " & vbNewLine _
                                       & "      ,CARGO_TIME_LIMIT             " & vbNewLine _
                                       & "      ,LARGE_CAR_YN                 " & vbNewLine _
                                       & "      ,TEL                          " & vbNewLine _
                                       & "      ,FAX                          " & vbNewLine _
                                       & "      ,UNCHIN_SEIQTO_CD             " & vbNewLine _
                                       & "      ,JIS                          " & vbNewLine _
                                       & "      ,KYORI                        " & vbNewLine _
                                       & "      ,PICK_KB                      " & vbNewLine _
                                       & "      ,BIN_KB                       " & vbNewLine _
                                       & "      ,MOTO_CHAKU_KB                " & vbNewLine _
                                       & "      ,URIAGE_CD                    " & vbNewLine _
                                       & "      ,SHIHARAI_AD                  " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                 " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                 " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                 " & vbNewLine _
                                       & "      ,SYS_ENT_USER                 " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                 " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                 " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                 " & vbNewLine _
                                       & "      ,SYS_UPD_USER                 " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                  " & vbNewLine _
                                       & "      ) VALUES (                    " & vbNewLine _
                                       & "      @NRS_BR_CD                    " & vbNewLine _
                                       & "      ,@CUST_CD_L                   " & vbNewLine _
                                       & "      ,@DEST_CD                     " & vbNewLine _
                                       & "      ,@EDI_CD                      " & vbNewLine _
                                       & "      ,@DEST_NM                     " & vbNewLine _
                                       & "      ,@ZIP                         " & vbNewLine _
                                       & "      ,@AD_1                        " & vbNewLine _
                                       & "      ,@AD_2                        " & vbNewLine _
                                       & "      ,@AD_3                        " & vbNewLine _
                                       & "      ,@CUST_DEST_CD                " & vbNewLine _
                                       & "      ,@SALES_CD                    " & vbNewLine _
                                       & "      ,@SP_NHS_KB                   " & vbNewLine _
                                       & "      ,@COA_YN                      " & vbNewLine _
                                       & "      ,@SP_UNSO_CD                  " & vbNewLine _
                                       & "      ,@SP_UNSO_BR_CD               " & vbNewLine _
                                       & "      ,@DELI_ATT                    " & vbNewLine _
                                       & "      ,@CARGO_TIME_LIMIT            " & vbNewLine _
                                       & "      ,@LARGE_CAR_YN                " & vbNewLine _
                                       & "      ,@TEL                         " & vbNewLine _
                                       & "      ,@FAX                         " & vbNewLine _
                                       & "      ,@UNCHIN_SEIQTO_CD            " & vbNewLine _
                                       & "      ,@JIS                         " & vbNewLine _
                                       & "      ,@KYORI                       " & vbNewLine _
                                       & "      ,@PICK_KB                     " & vbNewLine _
                                       & "      ,@BIN_KB                      " & vbNewLine _
                                       & "      ,@MOTO_CHAKU_KB               " & vbNewLine _
                                       & "      ,@URIAGE_CD                   " & vbNewLine _
                                       & "      ,@AD_1 + @AD_2                " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                 " & vbNewLine _
                                       & ")                                   " & vbNewLine

    ''' <summary>
    ''' 届先マスタUPDATE文（M_DEST）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_M_DEST_CJC As String = "UPDATE $LM_MST$..M_DEST SET       " & vbNewLine _
                                              & " DEST_NM           = @DEST_NM       	" & vbNewLine _
                                              & ",ZIP               = @ZIP              " & vbNewLine _
                                              & ",AD_1              = @AD_1             " & vbNewLine _
                                              & ",AD_2              = @AD_2             " & vbNewLine _
                                              & ",AD_3              = @AD_3             " & vbNewLine _
                                              & ",TEL               = @TEL              " & vbNewLine _
                                              & ",JIS               = @JIS              " & vbNewLine _
                                              & ",SHIHARAI_AD       = @AD_1 + @AD_2     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE     " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME     " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID     " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER     " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD        " & vbNewLine _
                                              & "AND CUST_CD_L      = @CUST_CD_L        " & vbNewLine _
                                              & "AND DEST_CD        = @DEST_CD          " & vbNewLine

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
    ''' ORDER BY句作成
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSqlOrderBy As StringBuilder

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 出荷データL検索対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷データLテーブル更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC118.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        '2011.09.21 修正START
        Me._StrSql.Append(LMH030DAC118.SQL_COUNT_FROM)             'SQL構築(データ抽出用From句)
        'Me._StrSql.Append(LMH030DAC118.SQL_FROM)             'SQL構築(データ抽出用From句)
        '2011.09.21 修正END
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウトの設定
        cmd.CommandTimeout = 1200

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' EDI出荷データL検索データ抽出
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC118.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMH030DAC118.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                      '条件設定
        '2011.09.26 追加START
        Call Me.SQLOrderBy()                                 'SQL構築(データ抽出用Order By句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "SelectSakuraListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("JYOTAI", "JYOTAI")
        map.Add("HORYU", "HORYU")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("OUTKA_STATE_KB_NM", "OUTKA_STATE_KB_NM")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("REMARK", "REMARK")
        map.Add("UNSO_ATT", "UNSO_ATT")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("DEST_AD", "DEST_AD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("BIN_KB_NM", "BIN_KB_NM")
        map.Add("M_COUNT", "M_COUNT")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("MATOME_NO", "MATOME_NO")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("SYUBETU_KB_NM", "SYUBETU_KB_NM")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("UNSO_MOTO_KB_NM", "UNSO_MOTO_KB_NM")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("SEND_DATE", "SEND_DATE")
        map.Add("SEND_TIME", "SEND_TIME")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_NM", "WH_NM")
        map.Add("TANTO_USER", "TANTO_USER")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("OUTKA_STATE_KB", "OUTKA_STATE_KB")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_SYS_UPD_DATE", "UNSO_SYS_UPD_DATE")
        map.Add("UNSO_SYS_UPD_TIME", "UNSO_SYS_UPD_TIME")
        map.Add("MIN_NB", "MIN_NB")
        map.Add("EDI_DEL_KB", "EDI_DEL_KB")
        map.Add("OUTKA_DEL_KB", "OUTKA_DEL_KB")
        map.Add("UNSO_DEL_KB", "UNSO_DEL_KB")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C30", "FREE_C30")
        map.Add("AKAKURO_FLG", "AKAKURO_FLG")
        map.Add("EDI_CUST_JISSEKI", "EDI_CUST_JISSEKI")
        map.Add("EDI_CUST_MATOMEF", "EDI_CUST_MATOMEF")
        map.Add("EDI_CUST_DELDISP", "EDI_CUST_DELDISP")
        map.Add("EDI_CUST_SPECIAL", "EDI_CUST_SPECIAL")
        map.Add("EDI_CUST_HOLDOUT", "EDI_CUST_HOLDOUT")
        map.Add("EDI_CUST_UNSOFLG", "EDI_CUST_UNSOFLG")
        map.Add("EDI_CUST_INDEX", "EDI_CUST_INDEX")
        map.Add("RCV_NM_HED", "RCV_NM_HED")
        map.Add("SND_NM", "SND_NM")
        map.Add("SND_SYS_UPD_DATE", "SND_SYS_UPD_DATE")
        map.Add("SND_SYS_UPD_TIME", "SND_SYS_UPD_TIME")
        map.Add("RCV_SYS_UPD_DATE", "RCV_SYS_UPD_DATE")
        map.Add("RCV_SYS_UPD_TIME", "RCV_SYS_UPD_TIME")
        map.Add("OUTKA_SYS_UPD_DATE", "OUTKA_SYS_UPD_DATE")
        map.Add("OUTKA_SYS_UPD_TIME", "OUTKA_SYS_UPD_TIME")
        map.Add("JISSEKI_FLAG", "JISSEKI_FLAG")
        map.Add("OUT_FLAG", "OUT_FLAG")
        map.Add("AUTO_MATOME_FLG", "AUTO_MATOME_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("ORDER_CHECK_FLG", "ORDER_CHECK_FLG")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '進捗区分
            Dim arr As ArrayList = New ArrayList()

            Dim connectFlg As Boolean = False
            Dim checkFlg As Boolean = False

            Me._StrSql.Append(" ( ")

            '未登録にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB1").ToString()) = False Then

                Me._StrSql.Append(" ((H_OUTKAEDI_L.DEL_KB IN ('0','3','2')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND H_OUTKAEDI_L.OUT_FLAG IN ('0','2')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND H_OUTKAEDI_L.JISSEKI_FLAG IN ('0','9'))")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (H_OUTKAEDI_L.DEL_KB = '1'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND M_EDI_CUST.FLAG_08 = '1'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND H_OUTKAEDI_L.OUT_FLAG IN ('0','2')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND H_OUTKAEDI_L.JISSEKI_FLAG IN ('0','9')))")
                Me._StrSql.Append(vbNewLine)
                connectFlg = True
                checkFlg = True
            End If

            '出荷登録済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB2").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" ((M_EDI_CUST.FLAG_01 IN ('1','2','4')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.OUTKA_STATE_KB < '60')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 IN ('1','4')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.SYS_DEL_FLG = '1')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 IN ('0','9')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.OUTKA_STATE_KB IS NOT NULL)")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (LEFT(H_OUTKAEDI_L.FREE_C30,2) = '01'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND SUBSTRING(H_OUTKAEDI_L.FREE_C30,5,8) NOT IN ('','00000000')))")
                Me._StrSql.Append(vbNewLine)
                connectFlg = True
                checkFlg = True
            End If

            '実績未にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB3").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (((M_EDI_CUST.FLAG_01 IN ('1','2')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND H_OUTKAEDI_L.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.OUTKA_STATE_KB >= '60')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 = '2'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND (H_OUTKAEDI_L.SYS_DEL_FLG = '1'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR C_OUTKA_L.SYS_DEL_FLG = '1'))")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 = '4'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND H_OUTKAEDI_L.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.SYS_DEL_FLG = '0'))")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND (H_OUTKAEDI_L.JISSEKI_FLAG = '0'))")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '実績作成済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB4").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (H_OUTKAEDI_L.JISSEKI_FLAG = '1')")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '実績送信済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB5").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (H_OUTKAEDI_L.JISSEKI_FLAG = '2')")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '赤データにチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB6").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (H_OUTKAEDI_L.DEL_KB = '2')")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '取消のみにチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB8").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (H_OUTKAEDI_L.DEL_KB = '1')")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '進捗区分チェックなしは全件検索
            If checkFlg = False Then

                Me._StrSql.Append(" ((H_OUTKAEDI_L.DEL_KB IN ('0','3','2'))")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 = '2'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR M_EDI_CUST.FLAG_08 = '1'))")
                Me._StrSql.Append(vbNewLine)

            End If

            Me._StrSql.Append(" ) ")

            '====== ヘッダ項目 ======'

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '倉庫
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.WH_CD = @WH_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND H_OUTKAEDI_L.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(" AND H_OUTKAEDI_L.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND H_OUTKAEDI_L.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(" AND H_OUTKAEDI_L.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '担当者コード
            whereStr = .Item("TANTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TANTO_USER.USER_CD LIKE @TANTO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先コード
            whereStr = .Item("DEST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.DEST_CD LIKE @DEST_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'EDI取込日(FROM)
            whereStr = .Item("EDI_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.CRT_DATE >= @EDI_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'EDI取込日(TO)
            whereStr = .Item("EDI_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.CRT_DATE <= @EDI_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DATE_TO", whereStr, DBDataType.CHAR))
            End If


            '区分格納変数
            Dim kbn As String

            '可変比較対象項目名格納変数
            Dim colNM As String = String.Empty

            'EDI検索日付区分
            kbn = .Item("SEARCH_DATE_KBN").ToString()

            'EDI検索日付区分によって以下分岐
            Select Case kbn

                'Case "01"
                '    colNM = "CRT_DATE"
                Case "01"
                    colNM = "OUTKA_PLAN_DATE"
                Case "02"
                    colNM = "ARR_PLAN_DATE"
                Case Else
                    colNM = String.Empty

            End Select

            If String.IsNullOrEmpty(colNM) = False Then

                'EDI検索日(FROM)
                whereStr = .Item("SEARCH_DATE_FROM").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    '2011.09.25 修正START
                    Select Case kbn
                        'Case "01"
                        '    Me._StrSql.Append(" AND @SEARCH_DATE = H_OUTKAEDI_L.")
                        '    Me._StrSql.Append(colNM)
                        '    Me._StrSql.Append(vbNewLine)
                        '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE", whereStr, DBDataType.CHAR))
                        Case "01", "02"
                            Me._StrSql.Append(" AND @SEARCH_DATE_FROM <= ISNULL(C_OUTKA_L.")
                            Me._StrSql.Append(colNM)
                            Me._StrSql.Append(" ,H_OUTKAEDI_L.")
                            Me._StrSql.Append(colNM)
                            Me._StrSql.Append(" )")
                            Me._StrSql.Append(vbNewLine)
                            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.CHAR))
                        Case Else

                    End Select
                    '2011.09.25 修正END
                End If

                'EDI検索日(TO)
                whereStr = .Item("SEARCH_DATE_TO").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then

                    Select Case kbn

                        Case "01", "02"
                            Me._StrSql.Append(" AND @SEARCH_DATE_TO >= ISNULL(C_OUTKA_L.")
                            Me._StrSql.Append(colNM)
                            Me._StrSql.Append(" ,H_OUTKAEDI_L.")
                            Me._StrSql.Append(colNM)
                            Me._StrSql.Append(" )")
                            Me._StrSql.Append(vbNewLine)
                            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.CHAR))
                        Case Else

                    End Select

                End If

            End If

            '====== スプレッド項目 ======'

            '★★★
            '状態
            whereStr = .Item("JYOTAI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.SYS_DEL_FLG = @JYOTAI_KB ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JYOTAI_KB", whereStr, DBDataType.CHAR))
            End If

            '保留
            whereStr = .Item("HORYU_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If whereStr.Equals("3") Then
                    Me._StrSql.Append(" AND H_OUTKAEDI_L.DEL_KB = '3' ")
                Else
                    Me._StrSql.Append(" AND H_OUTKAEDI_L.DEL_KB <> '3' ")
                End If
            End If
            '★★★

            'オーダー番号
            whereStr = .Item("CUST_ORD_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.CUST_ORD_NO LIKE @CUST_ORD_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (H_OUTKAEDI_L.CUST_NM_L + H_OUTKAEDI_L.CUST_NM_M) LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (H_OUTKAEDI_L.DEST_NM LIKE @DEST_NM")
                Me._StrSql.Append(" OR M_DEST.DEST_NM LIKE @DEST_NM)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '出荷時注意事項
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (H_OUTKAEDI_L.REMARK LIKE @REMARK")
                Me._StrSql.Append(" OR C_OUTKA_L.REMARK LIKE @REMARK)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '配送時注意事項
            whereStr = .Item("UNSO_ATT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (H_OUTKAEDI_L.UNSO_ATT LIKE @UNSO_ATT")
                Me._StrSql.Append(" OR F_UNSO_L.REMARK LIKE @UNSO_ATT)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ATT", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '商品名（中1）
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_M_FST.GOODS_NM LIKE @GOODS_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先住所
            whereStr = .Item("DEST_AD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (M_DEST.AD_1 + M_DEST.AD_2 + M_DEST.AD_3 LIKE @DEST_AD")
                Me._StrSql.Append(" OR H_OUTKAEDI_L.DEST_AD_1 + H_OUTKAEDI_L.DEST_AD_2 + C_OUTKA_L.DEST_AD_3 LIKE @DEST_AD)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '運送会社名
            whereStr = .Item("UNSO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_UNSOCO.UNSOCO_NM LIKE @UNSOCO_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '便区分
            whereStr = .Item("BIN_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.BIN_KB = @BIN_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", whereStr, DBDataType.CHAR))
            End If

            'EDI管理番号(大)
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.EDI_CTL_NO LIKE @EDI_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '出荷管理番号(大)
            whereStr = .Item("OUTKA_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.OUTKA_CTL_NO LIKE @KANRI_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'まとめ番号(大)
            whereStr = .Item("MATOME_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUBSTRING(H_OUTKAEDI_L.FREE_C30,4,9) LIKE @MATOME_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '注文番号
            whereStr = .Item("BUYER_ORD_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.BUYER_ORD_NO LIKE @BUYER_ORD_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '出荷種別
            whereStr = .Item("SYUBETU_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.SYUBETU_KB = @SYUBETU_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", whereStr, DBDataType.CHAR))
            End If

            'タリフ分類区分
            whereStr = .Item("UNSO_MOTO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.UNSO_TEHAI_KB = @UNSO_MOTO_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_MOTO_KB", whereStr, DBDataType.CHAR))
            End If

            '担当者
            whereStr = .Item("TANTO_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TANTO_USER.USER_NM LIKE @TANTO_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '作成者
            whereStr = .Item("SYS_ENT_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ENT_USER.USER_NM LIKE @SYS_ENT_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '最終更新者
            whereStr = .Item("SYS_UPD_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UPD_USER.USER_NM LIKE @SYS_UPD_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 検索用OrderBy句作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLOrderBy()

        Me._StrSqlOrderBy.Append(" ORDER BY ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(" H_OUTKAEDI_L.OUTKA_PLAN_DATE ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.DEST_NM ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.FREE_C30 ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.EDI_CTL_NO ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.OUTKA_CTL_NO ")


        'SQL文にOrderBy追加
        Me._StrSql.Append(Me._StrSqlOrderBy.ToString())

    End Sub

#End Region

#Region "サクラ出荷登録処理"

    ''' <summary>
    ''' EDI出荷データLの初期値設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectEdiL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC118.L_DEF_SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMH030DAC118.L_DEF_SQL_FROM)        'SQL構築(データ抽出用From句)
        Call Me.setSQLSelectDataExists()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "SelectEdiL", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_KB", "OUTKA_KB")
        map.Add("SYUBETU_KB", "SYUBETU_KB")
        map.Add("NAIGAI_KB", "NAIGAI_KB")
        map.Add("OUTKA_STATE_KB", "OUTKA_STATE_KB")
        map.Add("OUTKAHOKOKU_YN", "OUTKAHOKOKU_YN")
        map.Add("PICK_KB", "PICK_KB")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("HOKOKU_DATE", "HOKOKU_DATE")
        map.Add("TOUKI_HOKAN_YN", "TOUKI_HOKAN_YN")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("SHIP_CD_M", "SHIP_CD_M")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("SHIP_NM_M", "SHIP_NM_M")
        map.Add("EDI_DEST_CD", "EDI_DEST_CD")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_AD_4", "DEST_AD_4")
        map.Add("DEST_AD_5", "DEST_AD_5")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_FAX", "DEST_FAX")
        map.Add("DEST_MAIL", "DEST_MAIL")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("COA_YN", "COA_YN")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("UNSO_MOTO_KB", "UNSO_MOTO_KB")
        map.Add("UNSO_TEHAI_KB", "UNSO_TEHAI_KB")
        map.Add("SYARYO_KB", "SYARYO_KB")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("REMARK", "REMARK")
        map.Add("UNSO_ATT", "UNSO_ATT")
        map.Add("DENP_YN", "DENP_YN")
        map.Add("PC_KB", "PC_KB")
        map.Add("UNCHIN_YN", "UNCHIN_YN")
        map.Add("NIYAKU_YN", "NIYAKU_YN")
        map.Add("OUT_FLAG", "OUT_FLAG")
        map.Add("AKAKURO_KB", "AKAKURO_KB")
        map.Add("JISSEKI_FLAG", "JISSEKI_FLAG")
        map.Add("JISSEKI_USER", "JISSEKI_USER")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("JISSEKI_TIME", "JISSEKI_TIME")
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
        map.Add("FREE_C11", "FREE_C11")
        map.Add("FREE_C12", "FREE_C12")
        map.Add("FREE_C13", "FREE_C13")
        map.Add("FREE_C14", "FREE_C14")
        map.Add("FREE_C15", "FREE_C15")
        map.Add("FREE_C16", "FREE_C16")
        map.Add("FREE_C17", "FREE_C17")
        map.Add("FREE_C18", "FREE_C18")
        map.Add("FREE_C19", "FREE_C19")
        map.Add("FREE_C20", "FREE_C20")
        map.Add("FREE_C21", "FREE_C21")
        map.Add("FREE_C22", "FREE_C22")
        map.Add("FREE_C23", "FREE_C23")
        map.Add("FREE_C24", "FREE_C24")
        map.Add("FREE_C25", "FREE_C25")
        map.Add("FREE_C26", "FREE_C26")
        map.Add("FREE_C27", "FREE_C27")
        map.Add("FREE_C28", "FREE_C28")
        map.Add("FREE_C29", "FREE_C29")
        map.Add("FREE_C30", "FREE_C30")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
        map.Add("EDIT_FLAG", "EDIT_FLAG")
        map.Add("MATCHING_FLAG", "MATCHING_FLAG")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_OUTKAEDI_L")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030_OUTKAEDI_L").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' EDI出荷データMの初期値設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷データMテーブル更新対象データ結果取得SQLの構築・発行</remarks>
    Private Function SelectEdiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC118.M_DEF_SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMH030DAC118.M_DEF_SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.setSQLSelectDataExists()                           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "SelectEdiM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_CTL_NO_CHU", "OUTKA_CTL_NO_CHU")
        map.Add("COA_YN", "COA_YN")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("NRS_GOODS_CD", "NRS_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKA_HASU", "OUTKA_HASU")
        map.Add("OUTKA_QT", "OUTKA_QT")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("OUTKA_TTL_QT", "OUTKA_TTL_QT")
        map.Add("KB_UT", "KB_UT")
        map.Add("QT_UT", "QT_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("REMARK", "REMARK")
        map.Add("OUT_KB", "OUT_KB")
        map.Add("AKAKURO_KB", "AKAKURO_KB")
        map.Add("JISSEKI_FLAG", "JISSEKI_FLAG")
        map.Add("JISSEKI_USER", "JISSEKI_USER")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("JISSEKI_TIME", "JISSEKI_TIME")
        map.Add("SET_KB", "SET_KB")
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
        map.Add("FREE_C11", "FREE_C11")
        map.Add("FREE_C12", "FREE_C12")
        map.Add("FREE_C13", "FREE_C13")
        map.Add("FREE_C14", "FREE_C14")
        map.Add("FREE_C15", "FREE_C15")
        map.Add("FREE_C16", "FREE_C16")
        map.Add("FREE_C17", "FREE_C17")
        map.Add("FREE_C18", "FREE_C18")
        map.Add("FREE_C19", "FREE_C19")
        map.Add("FREE_C20", "FREE_C20")
        map.Add("FREE_C21", "FREE_C21")
        map.Add("FREE_C22", "FREE_C22")
        map.Add("FREE_C23", "FREE_C23")
        map.Add("FREE_C24", "FREE_C24")
        map.Add("FREE_C25", "FREE_C25")
        map.Add("FREE_C26", "FREE_C26")
        map.Add("FREE_C27", "FREE_C27")
        map.Add("FREE_C28", "FREE_C28")
        map.Add("FREE_C29", "FREE_C29")
        map.Add("FREE_C30", "FREE_C30")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
        map.Add("SCM_CTL_NO_M", "SCM_CTL_NO_M")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SAGYO_KB_1", "SAGYO_KB_1")
        map.Add("SAGYO_KB_2", "SAGYO_KB_2")
        '2011.10.28  要望番号391 追加START
        map.Add("CHUUI_NARR", "CHUUI_NARR")
        '2011.10.28  要望番号391 追加END
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_OUTKAEDI_M")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030_OUTKAEDI_M").Rows.Count())
        reader.Close()

        Return ds

    End Function

#End Region

#Region "サクラまとめチェック"

    ''' <summary>
    ''' 選択データがまとめデータかのチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷データLテーブル更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectMatomeCheck(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC118.SQL_SELECT_COUNT)                    'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMH030DAC118.SQL_MATOME_TARGET_CHECK)             'SQL構築(データ抽出用From句)
        Call Me.setSQLSelectDataExists()                                    '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "SelectMatomeCheck", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

#End Region

#Region "サクラまとめデータ(出荷)取得処理"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)のまとめデータ(出荷管理番号)の取得SQLの構築・発行</remarks>
    Private Function SelectMatomeTarget(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC118.SQL_SAKURA_MATOME_SELECT)      'SQL構築(データ抽出用Select句)

        If inTbl.Rows(0)("FREE_C03").ToString().Equals("15") = True Then
            Me._StrSql.Append(",H_OUTKAEDI_L.FREE_C04")
            Me._StrSql.Append(vbNewLine)
        End If

        Me._StrSql.Append(LMH030DAC118.SQL_SAKURA_MATOME_FROM)        'SQL構築(データ抽出用From句)

        If inTbl.Rows(0)("FREE_C03").ToString().Equals("15") = True Then
            Me._StrSql.Append("AND H_OUTKAEDI_L.FREE_C04    = @FREE_C04")
            Me._StrSql.Append(vbNewLine)
        End If

        Me._StrSql.Append(LMH030DAC118.SQL_SAKURA_MATOME_GROUPBY)     'SQL構築(データ抽出用Group By句)

        If inTbl.Rows(0)("FREE_C03").ToString().Equals("15") = True Then
            Me._StrSql.Append(",H_OUTKAEDI_L.FREE_C04")
            Me._StrSql.Append(vbNewLine)
        End If
        Call Me.setSQLMatomeDataExists()                              '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "SelectMatomeTarget", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_CTL_NO_CHU", "OUTKA_CTL_NO_CHU")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("SYS_UNSO_UPD_DATE", "SYS_UNSO_UPD_DATE")
        map.Add("SYS_UNSO_UPD_TIME", "SYS_UNSO_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_MATOMESAKI_EDIL")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030_MATOMESAKI_EDIL").Rows.Count())
        reader.Close()
        Return ds

    End Function

#End Region

#Region "サクラまとめデータ(運送M)取得処理"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(中)のまとめデータ(個別重量)の取得SQLの構築・発行</remarks>
    Private Function SelectUnsoMatomeTarget(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC118.SQL_MATOMEMOTO_DATA_UNSO_M)      'SQL構築

        Call Me.setSQLMatomeUnsoMData()                              '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "SelectUnsoMatomeTarget", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("UNSO_TTL_NB", "UNSO_TTL_NB")
        map.Add("HASU", "HASU")
        map.Add("PKG_NB", "PKG_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_MATOME_UNSO_M")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030_MATOME_UNSO_M").Rows.Count())
        reader.Close()
        Return ds

    End Function

#End Region

    '2011.09.27 追加START
#Region "同一まとめレコード取得処理(出荷取消⇒未登録)"

    Private Function SelectMatomeTorikesi(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC118.SQL_SELECT_DATA_MATOMETORIKESI)      'SQL構築(データ抽出用Select句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMatomeTorikesiSelectParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "SelectMatomeTorikesi", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("RCV_SYS_UPD_DATE", "RCV_SYS_UPD_DATE")
        map.Add("RCV_SYS_UPD_TIME", "RCV_SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030OUT")

        Return ds

    End Function

#End Region
    '2011.09.27 追加END

#Region "サクラ実績作成処理"

#Region "データ取得処理"

    ''' <summary>
    ''' サクラEDI実績データの値設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>サクラEDI実績データ結果取得SQLの構築・発行</remarks>
    Private Function SelectSakuraEdiSend(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC118.SQL_SELECT_SAKURA_SEND_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMH030DAC118.SQL_FROM_SAKURA_SEND_DATA)             'SQL構築(データ抽出用From句)
        Call Me.setSQLSelectDataExists()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "SelectSakuraEdiSend", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()
        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("EDI_CTL_NO_SHO", "EDI_CTL_NO_SHO")
        map.Add("WH_CD", "WH_CD")
        map.Add("RECORD_NO", "RECORD_NO")
        map.Add("SIJI_NO", "SIJI_NO")
        map.Add("GYO_NO", "GYO_NO")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("JISSEKI_QT", "JISSEKI_QT")
        map.Add("JISSEKI_NISUGATA", "JISSEKI_NISUGATA")
        map.Add("JISSEKI_IRISU", "JISSEKI_IRISU")
        map.Add("JISSEKI_TOTAL_NB", "JISSEKI_TOTAL_NB")
        map.Add("JISSEKI_NO", "JISSEKI_NO")
        map.Add("DENP_NO", "DENP_NO")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG")



        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_H_SENDOUTEDI_SFJ")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030_H_SENDOUTEDI_SFJ").Rows.Count())
        reader.Close()

        Return ds

    End Function

#End Region

    '▼▼▼20011.09.21
#Region "同一まとめレコード取得処理"

    Private Function SelectMatome(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC118.SQL_SELECT_DATA_MATOME)      'SQL構築(データ抽出用Select句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMatomeSelectParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "SelectMatome", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("RCV_SYS_UPD_DATE", "RCV_SYS_UPD_DATE")
        map.Add("RCV_SYS_UPD_TIME", "RCV_SYS_UPD_TIME")
        map.Add("OUTKA_SYS_UPD_DATE", "OUTKA_SYS_UPD_DATE")
        map.Add("OUTKA_SYS_UPD_TIME", "OUTKA_SYS_UPD_TIME")
        map.Add("OUTKA_DEL_KB", "OUTKA_DEL_KB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030OUT")

        Return ds

    End Function

#End Region
    '▲▲▲20011.09.21

#End Region

#Region "存在チェック(出荷登録用)"

#Region "届先マスタ(サクラ用)"

    ''' <summary>
    ''' 件数取得処理(届先マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataSakuraMdest(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMH030DAC118.SQL_SELECT_M_DEST)

        If dt.Rows(0)("DEST_CD").ToString() = String.Empty Then
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND M_DEST.EDI_CD = @EDI_DEST_CD")
        Else
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND M_DEST.DEST_CD = @DEST_CD")
        End If

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        '★★★2012.01.12 要望番号596 START
        Call Me.SetMdestParameter(dt, 0)
        '★★★2012.01.12 要望番号596 END

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "SelectDataMdest", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

    '#Region "商品マスタ(個数単位,包装個数,包装単位,標準入目,入目単位)取得"

    '    ''' <summary>
    '    ''' 項目値取得処理(商品マスタ)
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks></remarks>
    '    Private Function SelectDataSakuraMgoods(ByVal ds As DataSet) As DataSet

    '        ''SQL格納変数の初期化
    '        'Me._StrSql = New StringBuilder()

    '        ''SQL作成
    '        'Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_GOODS)

    '        'Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

    '        ''INTableの条件rowの格納
    '        'Me._Row = dt.Rows(0)

    '        'Me._SqlPrmList = New ArrayList()

    '        ''パラメータ設定
    '        'Call Me.SetMgoodsParameter(dt)

    '        ''スキーマ設定
    '        'Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

    '        ''SQL文のコンパイル
    '        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '        ''パラメータの反映
    '        'For Each obj As Object In Me._SqlPrmList
    '        '    cmd.Parameters.Add(obj)
    '        'Next

    '        'MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataSakuraMgoods", cmd)

    '        ''SQLの発行
    '        'Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

    '        ''処理件数の設定
    '        'reader.Read()

    '        ''マスタ値との整合性チェック(サクラ専用)
    '        'Call Me.SakuraCompareCheck(reader, dt)

    '        'reader.Close()

    '        'Return ds

    '    End Function

    '#End Region

#End Region

#Region "一括変更処理"

#Region "運送会社存在チェック＋名称取得処理"

    ''' <summary>
    ''' 運送会社名設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社マスタの結果取得SQLの構築・発行</remarks>
    Private Function SelectUnsoNM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtVal As DataTable = ds.Tables("LMH030OUT_UPDATE_VALUE")
        Dim dtKey As DataTable = ds.Tables("LMH030OUT_UPDATE_KEY")
        Dim drIn As DataRow = ds.Tables("LMH030INOUT").Rows(0)

        'INTableの条件rowの格納
        Me._Row = dtKey.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC118.SQL_UNSONM_GET)      'SQL構築(データ抽出用Select句)
        Call Me.setSQLSelectExists(dtVal, dtKey)            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "SelectUnsoNM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)


        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("EDIT_ITEM_VALUE3", "EDIT_ITEM_VALUE3")
        map.Add("EDIT_ITEM_VALUE4", "EDIT_ITEM_VALUE4")
        Dim ds2 As DataSet = ds.Clone
        'ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030OUT_UPDATE_VALUE")

        If reader.HasRows() = True Then
            ds2 = MyBase.SetSelectResultToDataSet(map, ds2, reader, "LMH030OUT_UPDATE_VALUE")

            ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_VALUE3") = ds2.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_VALUE3")
            ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_VALUE4") = ds2.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_VALUE4")

        End If
        reader.Close()

        'SQLの発行
        Dim readerCnt As SqlDataReader = MyBase.GetSelectResult(cmd)
        '処理件数の設定
        readerCnt.Read()
        If String.IsNullOrEmpty(ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_VALUE3").ToString()) = True Then
            '▼▼▼
            readerCnt.Close()
            '▲▲▲
            'MyBase.SetMessageStore(LMH030DAC.GUIDANCE_KBN, "E079", New String() {"運送会社マスタ", "運送会社コード"}, drIn.Item("ROW_NO").ToString(), LMH030DAC.EXCEL_COLTITLE, drIn.Item("EDI_CTL_NO").ToString())
            MyBase.SetMessage("E079", New String() {"運送会社マスタ", "運送会社コード"})
            Return ds
        End If
        readerCnt.Close()

        Return ds

    End Function

#End Region

#End Region

#Region "Update"

#Region "一括変更時のEDI出荷(大)"

    ''' <summary>
    ''' EDI出荷(大)テーブル更新（一括変更）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>EDI出荷(大)テーブル更新（一括変更）</remarks>
    Private Function UpdateHenko(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0
        Dim strSql As String = String.Empty

        'DataSetのIN情報を取得
        Dim dtKey As DataTable = ds.Tables("LMH030OUT_UPDATE_KEY")
        Dim dtValue As DataTable = ds.Tables("LMH030OUT_UPDATE_VALUE")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '更新項目
        Call Me.SetsqlEdit(dtValue)

        '共通項目
        Me._StrSql.Append(LMH030DAC118.SQL_UPDATE)

        'SQL作成
        Me._StrSql.Append(strSql)


        'SQLパラメータ設定
        Call Me.SetUpdHenkoPrm(dtKey, dtValue)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), dtKey.Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "UpdateHenko", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "サクラまとめ時のまとめ先EDI出荷(大)"

    ''' <summary>
    ''' EDI出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateMatomesakiEdiLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(1)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録SQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA
                setSql = LMH030DAC118.SQL_UPDATE_MATOMESAKI_OUTKAEDI_L

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetOutkaEdiLComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "UpdateMatomesakiEdiLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "サクラまとめ時のまとめ先運送(大)"

    ''' <summary>
    ''' 運送(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateMatomesakiUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_UNSO_L")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録SQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA
                setSql = LMH030DAC118.SQL_UPDATE_MATOMESAKI_UNSO_L

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUnsoLMatomeParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "UpdateMatomesakiUnsoLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_L"

    ''' <summary>
    ''' EDI出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaEdiLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録、紐付けSQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA, LMH030DAC.EventShubetsu.HIMODUKE
                setSql = LMH030DAC118.SQL_UPDATE_OUTKASAVEEDI_L

                '実績作成SQL CONST名
            Case LMH030DAC.EventShubetsu.CREATEJISSEKI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKISAKUSEI_EDI_L

                '実績取消SQL CONST名
            Case LMH030DAC.EventShubetsu.TORIKESIJISSEKI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKIDELEDI_L

                'EDI取消、EDI取消⇒未登録SQL CONST名
            Case LMH030DAC.EventShubetsu.EDITORIKESI _
                , LMH030DAC.EventShubetsu.TORIKESI_MITOUROKU

                setSql = LMH030DAC118.SQL_UPD_EDITORIKESI_EDI_L

                '実行(実績作成済⇒実績未,実績送信済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI _
                , LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI

                setSql = LMH030DAC118.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_L

                '実行(実績送信済⇒送信待)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_SOUSINMACHI

                setSql = LMH030DAC118.SQL_UPDATE_JISSEKIZUMI_SOUSINMACHI_EDI_L

                '実行(出荷取消⇒未登録)SQL CONST名
            Case LMH030DAC.EventShubetsu.TOUROKUZUMI_MITOUROKU

                setSql = LMH030DAC118.SQL_UPDATE_TOUROKUMI_EDI_L


            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータ設定
        Call Me.SetOutkaEdiLComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(inTbl.Rows(0), Me._SqlPrmList)
        Call Me.SetJissekiParameterEdiLM(inTbl.Rows(0), dtEventShubetsu)
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))


        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "UpdateOutkaEdiLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_M"

    ''' <summary>
    ''' EDI出荷(中)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaEdiMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediMTbl As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        Dim loopflg As Integer = 0

        Dim rtn As Integer = 0

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録、紐付けSQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA, LMH030DAC.EventShubetsu.HIMODUKE
                setSql = LMH030DAC118.SQL_UPDATE_OUTKAEDI_M
                loopflg = 1

                '実績作成SQL CONST名
            Case LMH030DAC.EventShubetsu.CREATEJISSEKI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKISAKUSEI_EDI_M

                'EDI取消、EDI取消⇒未登録SQL CONST名
            Case LMH030DAC.EventShubetsu.EDITORIKESI _
                , LMH030DAC.EventShubetsu.TORIKESI_MITOUROKU

                setSql = LMH030DAC118.SQL_UPD_EDITORIKESI_EDI_M

                '実行(実績作成済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_M

                '実行(実績送信済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_M

                '実行(実績送信済⇒送信待)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_SOUSINMACHI

                setSql = LMH030DAC118.SQL_UPDATE_JISSEKIZUMI_SOUSINMACHI_EDI_M

            Case LMH030DAC.EventShubetsu.TOUROKUZUMI_MITOUROKU

                setSql = LMH030DAC118.SQL_UPDATE_TOUROKUMI_EDI_M

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = ediMTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = ediMTbl.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter(Me._SqlPrmList)
            Call Me.SetJissekiParameterEdiLM(ediMTbl.Rows(i), dtEventShubetsu)
            Call Me.SetOutkaEdiMComParameter(Me._Row, Me._SqlPrmList)
            '↓↓↓EDI出荷(中)は排他をしない為不要↓↓↓
            'Call Me.SetSysDateTime(dtIn.Rows(0), Me._SqlPrmList)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))


            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC118", "UpdateOutkaEdiMData", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

            '処理回数の判定
            If loopflg = 0 Then
                Exit For
            End If

        Next

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_HED_SFJ"

    ''' <summary>
    ''' EDI受信(HED)サクラ用テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信(HED)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateEdiRcvHedData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediRcvHedTbl As DataTable = ds.Tables("LMH030_EDI_RCV_HED")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = ediRcvHedTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録、紐付けSQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA, LMH030DAC.EventShubetsu.HIMODUKE
                setSql = LMH030DAC118.SQL_UPDATE_OUTKASAVE_EDI_RCV_HED

                '実績作成SQL CONST名
            Case LMH030DAC.EventShubetsu.CREATEJISSEKI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKISAKUSEI_EDI_RCV_HED

                '実績取消SQL CONST名
            Case LMH030DAC.EventShubetsu.TORIKESIJISSEKI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKICANSEL_EDI_RCV_HED

                'EDI取消、EDI取消⇒未登録
            Case LMH030DAC.EventShubetsu.EDITORIKESI _
                , LMH030DAC.EventShubetsu.TORIKESI_MITOUROKU

                setSql = LMH030DAC118.SQL_UPDATE_EDITORIKESI_RCV_HED

                '実行(実績作成済⇒実績未,実績送信済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI _
                , LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_RCV_HED

                '実行(実績送信済⇒送信待)
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_SOUSINMACHI

                setSql = LMH030DAC118.SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_HED

            Case LMH030DAC.EventShubetsu.TOUROKUZUMI_MITOUROKU

                setSql = LMH030DAC118.SQL_UPD_TOUROKUMI_RCV_HED

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetOutkaEdiRcvHedComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
        Call Me.SetUpdPrmDelDateRcv(dtEventShubetsu)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "UpdateEdiRcvHedData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_DTL_SFJ"

    ''' <summary>
    ''' EDI受信(DTL)サクラ用テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信(DTL)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateEdiRcvDtlData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediRcvDtlTbl As DataTable = ds.Tables("LMH030_EDI_RCV_DTL")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        Dim loopflg As Integer = 0

        Dim rtn As Integer = 0

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録、紐付けSQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA, LMH030DAC.EventShubetsu.HIMODUKE
                setSql = LMH030DAC118.SQL_UPDATE_OUTKASAVE_EDI_RCV_DTL
                loopflg = 1

                '実績作成SQL CONST名
            Case LMH030DAC.EventShubetsu.CREATEJISSEKI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKISAKUSEI_EDI_RCV_DTL

                '実績取消SQL CONST名
            Case LMH030DAC.EventShubetsu.TORIKESIJISSEKI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKICANSEL_EDI_RCV_DTL

                'EDI取消、EDI取消⇒未登録、報告用EDI取消
            Case LMH030DAC.EventShubetsu.EDITORIKESI _
                , LMH030DAC.EventShubetsu.TORIKESI_MITOUROKU

                setSql = LMH030DAC118.SQL_UPDATE_EDITORIKESI_RCV_DTL

                '実行(実績作成済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_RCV_DTL

                '実行(実績作成済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKISOUSINZUMI_JISSEKIMI_EDI_RCV_DTL

                '実行(実績送信済⇒送信待)
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_SOUSINMACHI

                setSql = LMH030DAC118.SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_DTL

            Case LMH030DAC.EventShubetsu.TOUROKUZUMI_MITOUROKU

                setSql = LMH030DAC118.SQL_UPD_TOUROKUMI_RCV_DTL

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = ediRcvDtlTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = ediRcvDtlTbl.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter(Me._SqlPrmList)
            Call Me.SetOutkaEdiRcvDtlComParameter(Me._Row, Me._SqlPrmList)
            '↓↓↓EDI受信(DTL)は排他をしない為不要↓↓↓
            'Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)
            Call Me.SetUpdPrmDelDateRcv(dtEventShubetsu)
            Call Me.SetJissekiParameterRcv(ediRcvDtlTbl.Rows(i), dtEventShubetsu)
            Call Me.SetUpdPrmSndDateRcvDtl(dtEventShubetsu)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC118", "UpdateEdiRcvDtlData", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

            '処理回数の判定
            If loopflg = 0 Then
                Exit For
            End If

        Next

        Return ds

    End Function

#End Region

#Region "C_OUTKA_L"

    ''' <summary>
    ''' 出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_C_OUTKA_L")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録SQL CONST名(サクラまとめ処理のみ更新処理を行う)
            Case LMH030DAC.EventShubetsu.SAVEOUTKA
                setSql = LMH030DAC118.SQL_UPDATE_C_OUTKASAVE_L

                ''実績取消SQL CONST名
                'Case LMH030DAC.EventShubetsu.TORIKESIJISSEKI
                '    setSql = LMH030DAC118.SQL_UPDATE_JISSEKIDELEDI_L

                '実績作成SQL CONST名
            Case LMH030DAC.EventShubetsu.CREATEJISSEKI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKISAKUSEI_OUTKA_L

                '実行(実績作成済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_OUTKA_L

                '実行(実績送信済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI
                setSql = LMH030DAC118.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_OUTKA_L

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetOutkaLComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(inTbl.Rows(0), Me._SqlPrmList)
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))


        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "UpdateOutkaLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_SENDOUTEDI_SFJ"

    ''' <summary>
    ''' EDI送信テーブル(サクラファインテック)更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI送信テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateEdiSendLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_EDI_SND")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '実行(実績送信済⇒送信待)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_SOUSINMACHI
                setSql = LMH030DAC118.SQL_UPD_JISSEKIMODOSI_SEND

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータ設定
        Call Me.SetEdiSendComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(inTbl.Rows(0), Me._SqlPrmList)
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))


        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "UpdateEdiSendLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)


        Return ds

    End Function

#End Region

#End Region

#Region "Insert"

#Region "C_OUTKA_L"

    ''' <summary>
    ''' 出荷(大)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertOutkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim outkaLTbl As DataTable = ds.Tables("LMH030_C_OUTKA_L")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        'INTableの条件rowの格納
        Me._Row = outkaLTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC118.SQL_INSERT_OUTKA_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetOutkaLComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "InsertOutkaLData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "C_OUTKA_M"

    ''' <summary>
    ''' 出荷(中)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function InsertOutkaMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim outkaMTbl As DataTable = ds.Tables("LMH030_C_OUTKA_M")

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC118.SQL_INSERT_OUTKA_M _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = outkaMTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = outkaMTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetOutkaMComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC118", "UpdateOutkaMData", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "E_SAGYO_REC"

    ''' <summary>
    ''' 作業レコードの新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業レコードの新規作成SQLの構築・発行</remarks>
    Private Function InsertSagyoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim sagyoTbl As DataTable = ds.Tables("LMH030_E_SAGYO")

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC118.SQL_INSERT_SAGYO _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = sagyoTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = sagyoTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetSagyoParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC118", "InsertSagyoData", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoLTbl As DataTable = ds.Tables("LMH030_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = unsoLTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC118.SQL_INSERT_UNSO_L _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetUnsoLComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "InsertUnsoLData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "UNSO_M"

    ''' <summary>
    ''' 運送（中）テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送（中）テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsoMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoMTbl As DataTable = ds.Tables("LMH030_UNSO_M")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC118.SQL_INSERT_UNSO_M _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = unsoMTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()
            '条件rowの格納
            Me._Row = unsoMTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetUnsoMComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC118", "InsertUnsoMData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "UNCHIN"

    ''' <summary>
    ''' 運賃テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃新規登録SQLの構築・発行</remarks>
    Private Function InsertUnchinData(ByVal ds As DataSet) As DataSet

        If ds.Tables("F_UNCHIN_TRS").Rows.Count = 0 Then
            'F_UNCHIN_TRSが0件ということは本来無いが、一応念のために0件の時はINSERT処理が行われないようにする
            Return ds
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("F_UNCHIN_TRS")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC118.SQL_INSERT_UNCHIN _
                                                                       , ds.Tables("F_UNCHIN_TRS").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetUnchinComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC118", "InsertUnchinData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "H_SENDOUTEDI_SFJ"

    ''' <summary>
    ''' EDIサクラ実績テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDIサクラ実績テーブル更新SQLの構築・発行</remarks>
    Private Function InsertSakuraEdiSendData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim SakuraSendTbl As DataTable = ds.Tables("LMH030_H_SENDOUTEDI_SFJ")

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC118.SQL_INSERT_H_SENDOUTEDI_SFJ _
                                                                       , ds.Tables("LMH030_H_SENDOUTEDI_SFJ").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = SakuraSendTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = SakuraSendTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetEdiSendCreateParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC118", "InsertSakuraEdiSendData", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#End Region

#Region "Delete"

#Region "H_SENDOUTEDI_SFJ"

    ''' <summary>
    ''' EDI送信テーブル(サクラ用)更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI送信テーブル(サクラ用)削除SQLの構築・発行</remarks>
    Private Function DeleteEdiSendLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_EDI_SND")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '実行(実績作成済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI
                setSql = LMH030DAC118.SQL_DELETE_JISSEKIZUMI_JISSEKIMI_EDI_SEND

                '実行(実績送信済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI
                setSql = LMH030DAC118.SQL_DELETE_JISSEKIZUMI_JISSEKIMI_EDI_SEND

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータ設定
        Call Me.SetEdiSendComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)
        'Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(inTbl.Rows(0), Me._SqlPrmList)
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))


        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "DeleteEdiSendData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#End Region


#Region "SQL"

#Region "スキーマ名称設定"
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

#Region "一括変更SQL構築"

    ''' <summary>
    ''' 一括変更SQL構築
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetsqlEdit(ByVal dtVal As DataTable)

        Dim colNm1 As String = dtVal.Rows(0).Item("EDIT_ITEM_NM1").ToString
        Dim colNm2 As String = dtVal.Rows(0).Item("EDIT_ITEM_NM2").ToString
        'SQL構築
        Me._StrSql.Append("UPDATE $LM_TRN$..H_OUTKAEDI_L SET")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(String.Concat(colNm1, " = @EDIT_ITEM_VALUE1"))
        Me._StrSql.Append(vbNewLine)
        If colNm2 = String.Empty Then

        Else
            Me._StrSql.Append(String.Concat(",", colNm2, " = @EDIT_ITEM_VALUE2"))
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(",UNSO_NM = @EDIT_ITEM_VALUE3")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(",UNSO_BR_NM = @EDIT_ITEM_VALUE4")
            Me._StrSql.Append(vbNewLine)
        End If
        Me._StrSql.Append(",EDIT_FLAG = '01'")
        Me._StrSql.Append(vbNewLine)

    End Sub

#End Region

#Region "運送会社マスタ抽出パラメータ設定"

    ''' <summary>
    '''  運送会社マスタパラメータ設定
    ''' </summary>
    ''' <remarks>運送会社マスタ存在チェック用SQLの構築</remarks>
    Private Sub setSQLSelectExists(ByVal dtVal As DataTable, ByVal dtKey As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dtKey.Rows(0)("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", dtVal.Rows(0)("EDIT_ITEM_VALUE1"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", dtVal.Rows(0)("EDIT_ITEM_VALUE2"), DBDataType.NVARCHAR))

    End Sub

#End Region

#Region "EDI出荷(大,中),EDI受信TBL抽出パラメータ設定"

    ''' <summary>
    '''  パラメータ設定（EDI出荷(大・中),EDI受信テーブル・存在チェック）
    ''' </summary>
    ''' <remarks>出荷登録時出荷マスタ検索用SQLの構築</remarks>
    Private Sub setSQLSelectDataExists()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定(共通）
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row("EDI_CTL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me._Row("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "サクラまとめ対象専用 EDI出荷(大)パラメータ設定"

    ''' <summary>
    '''  パラメータ設定（EDI出荷(大),まとめ対象チェック）
    ''' </summary>
    ''' <remarks>出荷登録時サクラまとめデータ検索用SQLの構築</remarks>
    Private Sub setSQLMatomeDataExists()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定(サクラまとめ専用）
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(Me._Row("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(Me._Row("OUTKA_PLAN_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C01", Me.NullConvertString(Me._Row.Item("FREE_C01")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C02", Me.NullConvertString(Me._Row.Item("FREE_C02")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C03", Me.NullConvertString(Me._Row.Item("FREE_C03")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C04", Me.NullConvertString(Me._Row.Item("FREE_C04")), DBDataType.NVARCHAR))

    End Sub

#End Region

#Region "サクラまとめ先取得専用 運送(中)パラメータ設定"

    ''' <summary>
    '''  パラメータ設定（運送(中)）
    ''' </summary>
    ''' <remarks>出荷登録時サクラまとめデータ検索用SQLの構築</remarks>
    Private Sub setSQLMatomeUnsoMData()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定(サクラまとめ専用）
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(Me._Row("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me.NullConvertString(Me._Row("UNSO_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", Me.NullConvertString(Me._Row.Item("UNSO_NO_M")), DBDataType.CHAR))

    End Sub

#End Region

#Region "共通パラメータ設定"

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetSysdataParameter(prmList)

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        Call Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList)

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 抽出条件(日時:EDI(大))
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime(ByVal dr As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", dr.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", dr.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "EDI出荷(大)更新パラメータ設定"

    ''' <summary>
    ''' EDI出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", Me.NullConvertString(.Item("OUTKA_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", Me.NullConvertString(.Item("SYUBETU_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NAIGAI_KB", Me.NullConvertString(.Item("NAIGAI_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", Me.NullConvertString(.Item("OUTKA_STATE_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", Me.NullConvertString(.Item("OUTKAHOKOKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(.Item("PICK_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_NM", Me.NullConvertString(.Item("NRS_BR_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(.Item("WH_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_NM", Me.NullConvertString(.Item("WH_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(.Item("OUTKA_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", Me.NullConvertString(.Item("OUTKO_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", Me.NullConvertString(.Item("ARR_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", Me.NullConvertString(.Item("ARR_PLAN_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", Me.NullConvertString(.Item("HOKOKU_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(.Item("TOUKI_HOKAN_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", Me.NullConvertString(.Item("CUST_NM_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", Me.NullConvertString(.Item("CUST_NM_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", Me.NullConvertString(.Item("SHIP_CD_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", Me.NullConvertString(.Item("SHIP_CD_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NM_L", Me.NullConvertString(.Item("SHIP_NM_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NM_M", Me.NullConvertString(.Item("SHIP_NM_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", Me.NullConvertString(.Item("EDI_DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", Me.NullConvertString(.Item("DEST_ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", Me.NullConvertString(.Item("DEST_AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", Me.NullConvertString(.Item("DEST_AD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", Me.NullConvertString(.Item("DEST_AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_4", Me.NullConvertString(.Item("DEST_AD_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_5", Me.NullConvertString(.Item("DEST_AD_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", Me.NullConvertString(.Item("DEST_TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_FAX", Me.NullConvertString(.Item("DEST_FAX")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_MAIL", Me.NullConvertString(.Item("DEST_MAIL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", Me.NullConvertString(.Item("DEST_JIS_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(.Item("SP_NHS_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", Me.NullConvertString(.Item("CUST_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(.Item("BUYER_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_MOTO_KB", Me.NullConvertString(.Item("UNSO_MOTO_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", Me.NullConvertString(.Item("UNSO_TEHAI_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYARYO_KB", Me.NullConvertString(.Item("SYARYO_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", Me.NullConvertString(.Item("BIN_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.NullConvertString(.Item("UNSO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NM", Me.NullConvertString(.Item("UNSO_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me.NullConvertString(.Item("UNSO_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_NM", Me.NullConvertString(.Item("UNSO_BR_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", Me.NullConvertString(.Item("UNCHIN_TARIFF_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", Me.NullConvertString(.Item("EXTC_TARIFF_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ATT", Me.NullConvertString(.Item("UNSO_ATT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_YN", Me.NullConvertString(.Item("DENP_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", Me.NullConvertString(.Item("PC_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_YN", Me.NullConvertString(.Item("UNCHIN_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(.Item("NIYAKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_FLAG", Me.NullConvertString(.Item("OUT_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", Me.NullConvertString(.Item("AKAKURO_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", Me.NullConvertString(.Item("JISSEKI_FLAG")), DBDataType.CHAR))
            'Functionへ移動の為、コメント
            'prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.NullConvertString(.Item("JISSEKI_USER")), DBDataType.NVARCHAR))
            'prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.NullConvertString(.Item("JISSEKI_DATE")), DBDataType.NVARCHAR))
            'prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.NullConvertString(.Item("JISSEKI_TIME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.NullConvertZero(.Item("FREE_N01")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.NullConvertZero(.Item("FREE_N02")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.NullConvertZero(.Item("FREE_N03")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.NullConvertZero(.Item("FREE_N04")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.NullConvertZero(.Item("FREE_N05")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.NullConvertZero(.Item("FREE_N06")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.NullConvertZero(.Item("FREE_N07")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.NullConvertZero(.Item("FREE_N08")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.NullConvertZero(.Item("FREE_N09")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.NullConvertZero(.Item("FREE_N10")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", Me.NullConvertString(.Item("FREE_C01")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", Me.NullConvertString(.Item("FREE_C02")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", Me.NullConvertString(.Item("FREE_C03")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C04", Me.NullConvertString(.Item("FREE_C04")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C05", Me.NullConvertString(.Item("FREE_C05")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C06", Me.NullConvertString(.Item("FREE_C06")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C07", Me.NullConvertString(.Item("FREE_C07")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C08", Me.NullConvertString(.Item("FREE_C08")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C09", Me.NullConvertString(.Item("FREE_C09")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C10", Me.NullConvertString(.Item("FREE_C10")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C11", Me.NullConvertString(.Item("FREE_C11")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C12", Me.NullConvertString(.Item("FREE_C12")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C13", Me.NullConvertString(.Item("FREE_C13")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C14", Me.NullConvertString(.Item("FREE_C14")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C15", Me.NullConvertString(.Item("FREE_C15")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C16", Me.NullConvertString(.Item("FREE_C16")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C17", Me.NullConvertString(.Item("FREE_C17")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C18", Me.NullConvertString(.Item("FREE_C18")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C19", Me.NullConvertString(.Item("FREE_C19")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C20", Me.NullConvertString(.Item("FREE_C20")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C21", Me.NullConvertString(.Item("FREE_C21")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C22", Me.NullConvertString(.Item("FREE_C22")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C23", Me.NullConvertString(.Item("FREE_C23")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C24", Me.NullConvertString(.Item("FREE_C24")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C25", Me.NullConvertString(.Item("FREE_C25")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C26", Me.NullConvertString(.Item("FREE_C26")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C27", Me.NullConvertString(.Item("FREE_C27")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C28", Me.NullConvertString(.Item("FREE_C28")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C29", Me.NullConvertString(.Item("FREE_C29")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", Me.NullConvertString(.Item("FREE_C30")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", Me.NullConvertString(.Item("CRT_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me.NullConvertString(.Item("CRT_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", Me.NullConvertString(.Item("CRT_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", Me.NullConvertString(.Item("SCM_CTL_NO_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDIT_FLAG", Me.NullConvertString(.Item("EDIT_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATCHING_FLAG", Me.NullConvertString(.Item("MATCHING_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "EDI出荷(大)更新パラメータ設定(実績日時用)"

    ''' <summary>
    ''' 更新時のパラメータ実績日時(EDI出荷(大,中)用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJissekiParameterEdiLM(ByVal row As DataRow, ByVal eventShubetsu As Integer)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)

            Case LMH030DAC.EventShubetsu.SAVEOUTKA, LMH030DAC.EventShubetsu.HIMODUKE

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.NullConvertString(row.Item("JISSEKI_USER")), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.NullConvertString(row.Item("JISSEKI_DATE")), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.NullConvertString(row.Item("JISSEKI_TIME")), DBDataType.CHAR))

            Case LMH030DAC.EventShubetsu.CREATEJISSEKI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime))

            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI _
                 , LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", String.Empty, DBDataType.CHAR))

            Case Else

        End Select

    End Sub

#End Region

#Region "EDI受信(HED,DTL)更新パラメータ設定(実績日時用)"

    ''' <summary>
    ''' 更新時のパラメータ実績日時(EDI受信(HED,DTL)用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJissekiParameterRcv(ByVal row As DataRow, ByVal eventShubetsu As Integer)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)

            Case LMH030DAC.EventShubetsu.CREATEJISSEKI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime))

            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI _
                 , LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", String.Empty, DBDataType.CHAR))

            Case Else

        End Select

    End Sub

#End Region

#Region "EDI出荷(中)更新パラメータ設定"

    ''' <summary>
    ''' EDI出荷(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", .Item("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_CHU", .Item("OUTKA_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", .Item("CUST_GOODS_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", .Item("NRS_GOODS_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", Me.FormatNumValue(.Item("OUTKA_HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", Me.FormatNumValue(.Item("OUTKA_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me.FormatNumValue(.Item("OUTKA_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", Me.FormatNumValue(.Item("OUTKA_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KB_UT", .Item("KB_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@QT_UT", .Item("QT_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.FormatNumValue(.Item("PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PKG_UT", .Item("PKG_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_KB", .Item("ONDO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_KB", .Item("OUT_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", .Item("AKAKURO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", .Item("JISSEKI_FLAG").ToString(), DBDataType.CHAR))
            'Functionへ移動の為、コメント
            'prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", .Item("JISSEKI_USER").ToString(), DBDataType.NVARCHAR))
            'prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", .Item("JISSEKI_DATE").ToString(), DBDataType.NVARCHAR))
            'prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", .Item("JISSEKI_TIME").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SET_KB", .Item("SET_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.FormatNumValue(.Item("FREE_N01").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.FormatNumValue(.Item("FREE_N02").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.FormatNumValue(.Item("FREE_N03").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.FormatNumValue(.Item("FREE_N04").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.FormatNumValue(.Item("FREE_N05").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.FormatNumValue(.Item("FREE_N06").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.FormatNumValue(.Item("FREE_N07").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.FormatNumValue(.Item("FREE_N08").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.FormatNumValue(.Item("FREE_N09").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.FormatNumValue(.Item("FREE_N10").ToString()), DBDataType.NUMERIC))
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
            prmList.Add(MyBase.GetSqlParameter("@FREE_C11", .Item("FREE_C11").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C12", .Item("FREE_C12").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C13", .Item("FREE_C13").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C14", .Item("FREE_C14").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C15", .Item("FREE_C15").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C16", .Item("FREE_C16").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C17", .Item("FREE_C17").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C18", .Item("FREE_C18").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C19", .Item("FREE_C19").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C20", .Item("FREE_C20").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C21", .Item("FREE_C21").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C22", .Item("FREE_C22").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C23", .Item("FREE_C23").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C24", .Item("FREE_C24").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C25", .Item("FREE_C25").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C26", .Item("FREE_C26").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C27", .Item("FREE_C27").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C28", .Item("FREE_C28").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C29", .Item("FREE_C29").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", .Item("FREE_C30").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", .Item("CRT_USER").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", .Item("CRT_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))
        End With

    End Sub

#End Region

#Region "EDI受信(HED)更新パラメータ設定"

    ''' <summary>
    ''' EDI受信(HED)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiRcvHedComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "EDI受信(DTL)更新パラメータ設定"

    ''' <summary>
    ''' EDI受信(DTL)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiRcvDtlComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(.Item("EDI_CTL_NO_CHU")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_CHU", Me.NullConvertString(.Item("OUTKA_CTL_NO_CHU")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(.Item("JISSEKI_SHORI_FLG")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "更新パラメータ削除日時設定(RCV_TBL)"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="eventShubetsu"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmDelDateRcv(ByVal eventShubetsu As Integer)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定

        Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)
            'EDI取消
            Case LMH030DAC.EventShubetsu.EDITORIKESI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", updTime, DBDataType.CHAR))

                'EDI取消⇒未登録
            Case LMH030DAC.EventShubetsu.TORIKESI_MITOUROKU

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", String.Empty, DBDataType.CHAR))

                '出荷取消⇒未登録
            Case LMH030DAC.EventShubetsu.TOUROKUZUMI_MITOUROKU

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TIME", String.Empty, DBDataType.CHAR))

            Case Else

        End Select
    End Sub

#End Region

#Region "更新パラメータ送信日時設定(RCV_DTL)"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="eventShubetsu"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmSndDateRcvDtl(ByVal eventShubetsu As Integer)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定

        Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)

            '実績送信済⇒実績未
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI _
                , LMH030DAC.EventShubetsu.SOUSINZUMI_SOUSINMACHI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", String.Empty, DBDataType.CHAR))

            Case Else

        End Select
    End Sub

#End Region

#Region "EDI送信(TBL)更新パラメータ設定"

    ''' <summary>
    ''' EDI送信(TBL)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetEdiSendComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))

            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))

            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(.Item("JISSEKI_SHORI_FLG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_USER", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_TIME", String.Empty, DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "EDI送信(TBL)新規登録パラメータ設定"

    ''' <summary>
    ''' EDI送信(TBL)の新規登録パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetEdiSendCreateParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
            'Dim updTimeNormal As String = DateTime.Now.ToString("HHmmss")
            Dim updTimeNormal As String = MyBase.GetSystemTime().Substring(0, 6)

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(.Item("EDI_CTL_NO_CHU")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_SHO", Me.NullConvertString(.Item("EDI_CTL_NO_SHO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(.Item("WH_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RECORD_NO", Me.NullConvertString(.Item("RECORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIJI_NO", Me.NullConvertString(.Item("SIJI_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO_NO", Me.NullConvertString(.Item("GYO_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", Me.NullConvertString(.Item("CUST_GOODS_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", Me.NullConvertString(.Item("SERIAL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_QT", Me.NullConvertZero(.Item("JISSEKI_QT")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_NISUGATA", Me.NullConvertString(.Item("JISSEKI_NISUGATA")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_IRISU", Me.NullConvertZero(.Item("JISSEKI_IRISU")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TOTAL_NB", Me.NullConvertZero(.Item("JISSEKI_TOTAL_NB")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_NO", Me.NullConvertString(.Item("JISSEKI_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", Me.NullConvertString(.Item("DENP_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.NullConvertString(.Item("UNSO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(.Item("OUTKA_PLAN_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JUSHIN_DATE", MyBase.GetSystemDate(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JUSHIN_TIME", updTimeNormal, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", Me.NullConvertString(.Item("RECORD_STATUS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(.Item("JISSEKI_SHORI_FLG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_USER", Me.NullConvertString(.Item("SEND_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_DATE", Me.NullConvertString(.Item("SEND_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_TIME", Me.NullConvertString(.Item("SEND_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", updTime, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", Me.NullConvertString(.Item("UPD_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", Me.NullConvertString(.Item("UPD_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.NullConvertString(.Item("UPD_TIME")), DBDataType.CHAR))


        End With

    End Sub

#End Region

#Region "出荷(大)更新パラメータ設定"

    ''' <summary>
    ''' 出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me.NullConvertString(.Item("OUTKA_NO_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FURI_NO", Me.NullConvertString(.Item("FURI_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", Me.NullConvertString(.Item("OUTKA_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", Me.NullConvertString(.Item("SYUBETU_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", Me.NullConvertString(.Item("OUTKA_STATE_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", Me.NullConvertString(.Item("OUTKAHOKOKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(.Item("PICK_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", Me.NullConvertString(.Item("DENP_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_KANRYO_INFO", Me.NullConvertString(.Item("ARR_KANRYO_INFO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(.Item("WH_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(.Item("OUTKA_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", Me.NullConvertString(.Item("OUTKO_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", Me.NullConvertString(.Item("ARR_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", Me.NullConvertString(.Item("ARR_PLAN_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(.Item("TOUKI_HOKAN_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@END_DATE", Me.NullConvertString(.Item("END_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", Me.NullConvertString(.Item("SHIP_CD_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", Me.NullConvertString(.Item("SHIP_CD_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", Me.NullConvertString(.Item("DEST_AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", Me.NullConvertString(.Item("DEST_TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", Me.NullConvertString(.Item("NHS_REMARK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(.Item("SP_NHS_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", Me.NullConvertString(.Item("CUST_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(.Item("BUYER_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.NullConvertZero(.Item("OUTKA_PKG_NB")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DENP_YN", Me.NullConvertString(.Item("DENP_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", Me.NullConvertString(.Item("PC_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(.Item("NIYAKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALL_PRINT_FLAG", Me.NullConvertString(.Item("ALL_PRINT_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIHUDA_FLAG", Me.NullConvertString(.Item("NIHUDA_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_FLAG", Me.NullConvertString(.Item("NHS_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_FLAG", Me.NullConvertString(.Item("DENP_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_FLAG", Me.NullConvertString(.Item("COA_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_FLAG", Me.NullConvertString(.Item("HOKOKU_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATOME_PICK_FLAG", Me.NullConvertString(.Item("MATOME_PICK_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_DATE", Me.NullConvertString(.Item("LAST_PRINT_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_TIME", Me.NullConvertString(.Item("LAST_PRINT_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SASZ_USER", Me.NullConvertString(.Item("SASZ_USER")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_USER", Me.NullConvertString(.Item("OUTKO_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEN_USER", Me.NullConvertString(.Item("KEN_USER")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", Me.NullConvertString(.Item("OUTKA_USER")), DBDataType.NVARCHAR))

            'イベント種別の判断
            Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)

                '出荷登録処理
                '出荷登録SQL CONST名
                Case LMH030DAC.EventShubetsu.SAVEOUTKA
                    prmList.Add(MyBase.GetSqlParameter("@HOU_USER", Me.NullConvertString(.Item("HOU_USER")), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", Me.NullConvertString(.Item("HOKOKU_DATE")), DBDataType.CHAR))

                    '実績作成SQL CONST名
                Case LMH030DAC.EventShubetsu.CREATEJISSEKI
                    prmList.Add(MyBase.GetSqlParameter("@HOU_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))

                    '実績作成済⇒実績未,実績送信済⇒実績未処理
                Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI, LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI
                    prmList.Add(MyBase.GetSqlParameter("@HOU_USER", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", String.Empty, DBDataType.CHAR))

            End Select
            prmList.Add(MyBase.GetSqlParameter("@ORDER_TYPE", Me.NullConvertString(.Item("ORDER_TYPE")), DBDataType.NVARCHAR))
            '2011.09.16 追加START
            prmList.Add(MyBase.GetSqlParameter("@DEST_KB", Me.NullConvertString(.Item("DEST_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", Me.NullConvertString(.Item("DEST_AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", Me.NullConvertString(.Item("DEST_AD_2")), DBDataType.NVARCHAR))
            '2011.09.16 追加END

            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", Me.NullConvertString(.Item("WH_TAB_STATUS")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_YN", Me.NullConvertString(.Item("WH_TAB_YN")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "出荷(中)更新パラメータ設定"

    ''' <summary>
    ''' 出荷(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_SET_NO", .Item("EDI_SET_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", Me.FormatNumValue(.Item("OUTKA_HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", Me.FormatNumValue(.Item("OUTKA_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me.FormatNumValue(.Item("OUTKA_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", Me.FormatNumValue(.Item("OUTKA_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BACKLOG_NB", Me.FormatNumValue(.Item("BACKLOG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BACKLOG_QT", Me.FormatNumValue(.Item("BACKLOG_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_M_PKG_NB", Me.FormatNumValue(.Item("OUTKA_M_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAIKO_KB", .Item("ZAIKO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SOURCE_CD", .Item("SOURCE_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YELLOW_CARD", .Item("YELLOW_CARD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", Me.FormatNumValue(.Item("PRINT_SORT").ToString()), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "作業更新パラメータ設定"

    ''' <summary>
    ''' 作業の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Item("SAGYO_REC_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", .Item("SAGYO_COMP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SKYU_CHK", .Item("SKYU_CHK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Item("SAGYO_SIJI_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", .Item("INOUTKA_NO_LM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", .Item("SAGYO_CD").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", .Item("SAGYO_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", .Item("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            'prmList.Add(MyBase.GetSqlParameter("@INV_TANI", .Item("INV_TANI").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", .Item("SAGYO_NB").ToString(), DBDataType.NUMERIC))
            'prmList.Add(MyBase.GetSqlParameter("@SAGYO_UP", .Item("SAGYO_UP").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", .Item("SAGYO_GK").ToString(), DBDataType.NUMERIC))
            'prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))	'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            'prmList.Add(MyBase.GetSqlParameter("@REMARK_ZAI", .Item("REMARK_ZAI").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", .Item("REMARK_SKYU").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", .Item("SAGYO_COMP_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", .Item("SAGYO_COMP_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_SAGYO_FLG", .Item("DEST_SAGYO_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "運送(大)パラメータ設定"

    ''' <summary>
    ''' 運送(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIYU_KB", .Item("JIYU_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", .Item("DENP_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_TIME", .Item("OUTKA_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_ACT_TIME", .Item("ARR_ACT_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", .Item("CUST_REF_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD", .Item("SHIP_CD").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", .Item("SHIP_CD_M").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@ORIG_CD", .Item("ORIG_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_PKG_NB", Me.FormatNumValue(.Item("UNSO_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_WT", Me.FormatNumValue(.Item("UNSO_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", .Item("PC_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", .Item("TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@VCLE_KB", .Item("VCLE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
            '要望番号602 2011.12.08 修正START
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            '要望番号602 2011.12.08 修正END
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_3", .Item("AD_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", .Item("UNSO_TEHAI_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUY_CHU_NO", .Item("BUY_CHU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AREA_CD", .Item("AREA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TYUKEI_HAISO_FLG", .Item("TYUKEI_HAISO_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_CD", .Item("SYUKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_CD", .Item("HAIKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", .Item("TRIP_NO_SYUKA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", .Item("TRIP_NO_TYUKEI").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", .Item("TRIP_NO_HAIKA").ToString(), DBDataType.CHAR))

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            'END UMANO 要望番号1302 支払運賃に伴う修正。

        End With

    End Sub

#End Region

#Region "サクラまとめ先専用　運送(大)パラメータ設定"

    ''' <summary>
    ''' 運送(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLMatomeParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_WT", Me.FormatNumValue(.Item("UNSO_WT").ToString()), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "運送(中)パラメータ設定"

    ''' <summary>
    ''' 運送(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_NB", Me.FormatNumValue(.Item("UNSO_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_QT", Me.FormatNumValue(.Item("UNSO_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@QT_UT", .Item("QT_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HASU", Me.FormatNumValue(.Item("HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZBUKA_CD", .Item("ZBUKA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ABUKA_CD", .Item("ABUKA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.FormatNumValue(.Item("PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "運賃パラメータ設定"

    ''' <summary>
    ''' 運賃の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnchinComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", .Item("SEIQ_GROUP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", .Item("SEIQ_GROUP_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))   '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            prmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_SYARYO_KB", .Item("SEIQ_SYARYO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_PKG_UT", .Item("SEIQ_PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_NG_NB", Me.FormatNumValue(.Item("SEIQ_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_DANGER_KB", .Item("SEIQ_DANGER_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_BUNRUI_KB", .Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_KYORI", Me.FormatNumValue(.Item("SEIQ_KYORI").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WT", Me.FormatNumValue(.Item("SEIQ_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_UNCHIN", Me.FormatNumValue(.Item("SEIQ_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_CITY_EXTC", Me.FormatNumValue(.Item("SEIQ_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WINT_EXTC", Me.FormatNumValue(.Item("SEIQ_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_RELY_EXTC", Me.FormatNumValue(.Item("SEIQ_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TOLL", Me.FormatNumValue(.Item("SEIQ_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_INSU", Me.FormatNumValue(.Item("SEIQ_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_FIXED_FLAG", .Item("SEIQ_FIXED_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DECI_NG_NB", Me.FormatNumValue(.Item("DECI_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_KYORI", Me.FormatNumValue(.Item("DECI_KYORI").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WT", Me.FormatNumValue(.Item("DECI_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", Me.FormatNumValue(.Item("DECI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", Me.FormatNumValue(.Item("DECI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", Me.FormatNumValue(.Item("DECI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", Me.FormatNumValue(.Item("DECI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", Me.FormatNumValue(.Item("DECI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_INSU", Me.FormatNumValue(.Item("DECI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_UNCHIN", Me.FormatNumValue(.Item("KANRI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_CITY_EXTC", Me.FormatNumValue(.Item("KANRI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_WINT_EXTC", Me.FormatNumValue(.Item("KANRI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_RELY_EXTC", Me.FormatNumValue(.Item("KANRI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_TOLL", Me.FormatNumValue(.Item("KANRI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_INSU", Me.FormatNumValue(.Item("KANRI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_KANRI", .Item("SAGYO_KANRI").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "一括変更(EDI出荷(大))パラメータ設定"

    ''' <summary>
    ''' 一括変更(EDI出荷(大))パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdHenkoPrm(ByVal dtKey As DataTable, ByVal dtVal As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        'Dim editType As DBDataType = DirectCast(dtVal.Rows(0).Item("EDIT_ITEM_TYPE1"), DBDataType)
        'Dim editType2 As DBDataType = DirectCast(dtVal.Rows(0).Item("EDIT_ITEM_TYPE2"), DBDataType)

        'SQLパラメータ設定
        Dim editType1 As Integer = Convert.ToInt32(Me.NullConvertZero(dtVal.Rows(0).Item("EDIT_ITEM_TYPE1")))
        Dim editType2 As Integer = Convert.ToInt32(Me.NullConvertZero(dtVal.Rows(0).Item("EDIT_ITEM_TYPE2")))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE1", dtVal.Rows(0).Item("EDIT_ITEM_VALUE1"), editType))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE2", dtVal.Rows(0).Item("EDIT_ITEM_VALUE2"), editType))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_NM", dtVal.Rows(0).Item("EDIT_ITEM_NM"), DBDataType.NUMERIC))
        'Debug.Print(_SqlPrmList(0).ToString)

        'If editType1.ToString <> String.Empty Then
        If String.IsNullOrEmpty(editType1.ToString()) = False Then
            Select Case editType1
                Case 3
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE1", dtVal.Rows(0).Item("EDIT_ITEM_VALUE1"), DBDataType.CHAR))
                Case 5
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE1", dtVal.Rows(0).Item("EDIT_ITEM_VALUE1"), DBDataType.NUMERIC))
                Case 12
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE1", dtVal.Rows(0).Item("EDIT_ITEM_VALUE1"), DBDataType.NVARCHAR))
                Case Else
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE1", Me.NullConvertString(dtVal.Rows(0).Item("EDIT_ITEM_VALUE1")), DBDataType.CHAR))
            End Select
        End If

        If String.IsNullOrEmpty(editType2.ToString()) = False Then
            'If editType2.ToString <> String.Empty Then
            Select Case editType2
                Case 3
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE2", dtVal.Rows(0).Item("EDIT_ITEM_VALUE2"), DBDataType.CHAR))
                Case 5
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE2", dtVal.Rows(0).Item("EDIT_ITEM_VALUE2"), DBDataType.NUMERIC))
                Case 12
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE2", dtVal.Rows(0).Item("EDIT_ITEM_VALUE2"), DBDataType.NVARCHAR))
                Case Else
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE2", Me.NullConvertString(dtVal.Rows(0).Item("EDIT_ITEM_VALUE2")), DBDataType.CHAR))
            End Select
        End If

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE3", Me.NullConvertString(dtVal.Rows(0).Item("EDIT_ITEM_VALUE3")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE4", Me.NullConvertString(dtVal.Rows(0).Item("EDIT_ITEM_VALUE4")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dtKey.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", dtKey.Rows(0).Item("EDI_CTL_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", dtKey.Rows(0).Item("SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", dtKey.Rows(0).Item("SYS_UPD_TIME"), DBDataType.CHAR))


    End Sub

#End Region

#Region "届先マスタパラメータ設定"
    ''' <summary>
    ''' 届先マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMdestParameter(ByVal dt As DataTable, ByVal prmUpdFlg As Integer)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        '★★★2012.01.12 要望番号596 START
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("DEST_CD"), DBDataType.NVARCHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", dt.Rows(0).Item("EDI_DEST_CD"), DBDataType.NVARCHAR))
        If prmUpdFlg = 1 Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("DEST_CD"), DBDataType.NVARCHAR))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", dt.Rows(0).Item("EDI_DEST_CD"), DBDataType.NVARCHAR))
        Else
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("SHIP_CD_L"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", dt.Rows(0).Item("SHIP_CD_L"), DBDataType.NVARCHAR))
        End If
        '★★★2012.01.12 要望番号596 END

    End Sub


#End Region

    '▼▼▼20011.09.21
#Region "同一まとめデータ取得パラメータ設定"
    ''' <summary>
    ''' 同一まとめデータ取得パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetMatomeSelectParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_NO", conditionRow.Item("MATOME_NO"), DBDataType.NVARCHAR))

    End Sub

#End Region
    '▲▲▲20011.09.21

    '▼▼▼20011.09.21
#Region "同一まとめデータ取得パラメータ設定(出荷取消⇒未登録)"
    ''' <summary>
    ''' 同一まとめデータ取得パラメータ設定(出荷取消⇒未登録)
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetMatomeTorikesiSelectParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_NO", conditionRow.Item("MATOME_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", conditionRow.Item("OUTKA_CTL_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_SYS_UPD_DATE", conditionRow.Item("SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_SYS_UPD_TIME", conditionRow.Item("SYS_UPD_TIME"), DBDataType.CHAR))

    End Sub

#End Region
    '▲▲▲20011.09.21

#Region "Null変換"
    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertString(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = String.Empty
        End If

        Return value

    End Function

    ''' <summary>
    ''' Null変換（数値）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertZero(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = 0
        End If

        Return value

    End Function
#End Region



    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <remarks></remarks>
    Private Sub UpdateResultChk(ByVal cmd As SqlCommand)

        Dim updateCnt As Integer = 0

        updateCnt = MyBase.GetUpdateResult(cmd)
        'SQLの発行
        If updateCnt < 1 Then
            MyBase.SetMessage("E011")
        End If

        MyBase.SetResultCount(updateCnt)

    End Sub


#End Region

#Region "届先マスタ自動追加/更新"

    ''' <summary>
    ''' 届先マスタ新規登録(荷送人コードまたは届先コードを元に新規登録:日興産業専用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertMDestData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_M_DEST_SHIP_CD_L")
        Dim mDestShipCnt As Integer = ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows.Count
        Dim mDestCnt As Integer = ds.Tables("LMH030_M_DEST").Rows.Count

        'DataSetのIN情報を取得
        If mDestShipCnt > 0 AndAlso ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("INSERT_TARGET_FLG").Equals("1") = True Then
            inTbl = ds.Tables("LMH030_M_DEST_SHIP_CD_L")
        ElseIf mDestCnt > 0 AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG").Equals("1") = True Then
            inTbl = ds.Tables("LMH030_M_DEST")
        End If

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC118.SQL_INSERT_M_DEST, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetMdestInsertParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "InsertMDestData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 届先マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateMDestData(ByVal ds As DataSet) As DataSet

        Dim inTbl As DataTable = ds.Tables("LMH030_M_DEST")
        Dim setSql As String = String.Empty
        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'SQL文のコンパイル
        setSql = SQL_UPDATE_M_DEST_CJC

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMdestParameter(inTbl, 1)
        Call Me.SetMdestUpdateParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC118", "UpdateMDestData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 届先マスタの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetMdestInsertParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CD", Me.NullConvertString(.Item("EDI_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP", Me.NullConvertString(.Item("ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_1", Me.NullConvertString(.Item("AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_2", Me.NullConvertString(.Item("AD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_3", Me.NullConvertString(.Item("AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_DEST_CD", Me.NullConvertString(.Item("CUST_DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SALES_CD", Me.NullConvertString(.Item("SALES_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(.Item("SP_NHS_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_UNSO_CD", Me.NullConvertString(.Item("SP_UNSO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_UNSO_BR_CD", Me.NullConvertString(.Item("SP_UNSO_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELI_ATT", Me.NullConvertString(.Item("DELI_ATT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CARGO_TIME_LIMIT", Me.NullConvertString(.Item("CARGO_TIME_LIMIT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LARGE_CAR_YN", Me.NullConvertString(.Item("LARGE_CAR_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TEL", Me.NullConvertString(.Item("TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FAX", Me.NullConvertString(.Item("FAX")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_SEIQTO_CD", Me.NullConvertString(.Item("UNCHIN_SEIQTO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIS", Me.NullConvertString(.Item("JIS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KYORI", Me.NullConvertZero(.Item("KYORI")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(.Item("PICK_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", Me.NullConvertString(.Item("BIN_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTO_CHAKU_KB", Me.NullConvertString(.Item("MOTO_CHAKU_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@URIAGE_CD", Me.NullConvertString(.Item("URIAGE_CD")), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 届先マスタの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetMdestUpdateParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")

            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP", Me.NullConvertString(.Item("ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_1", Me.NullConvertString(.Item("AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_2", Me.NullConvertString(.Item("AD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_3", Me.NullConvertString(.Item("AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TEL", Me.NullConvertString(.Item("TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIS", Me.NullConvertString(.Item("JIS")), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#End Region

End Class
