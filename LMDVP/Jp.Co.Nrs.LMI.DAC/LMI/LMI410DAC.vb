' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特殊荷主機能
'  プログラムID     :  LMI410DAC : ビックケミー取込データ確認／報告
'  作  成  者       :  [Umano]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI410DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI410DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' EXCEL用COLUMタイトル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_COLTITLE As String = ""


#Region "検索処理 SQL"

#Region "倉庫間転送データ検索"

    ''' <summary>
    ''' 倉庫間転送データ検索処理(件数取得(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_SELECT As String = " SELECT COUNT(HIDB.NRS_BR_CD)	   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' 倉庫間転送データ検索処理(データ取得(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_SELECT As String = " SELECT                                                                                           " & vbNewLine _
                                           & " HIDB.NRS_BR_CD                                       AS NRS_BR_CD                                        " & vbNewLine _
                                           & ",HIDB.CUST_CD_L                                       AS CUST_CD_L                                        " & vbNewLine _
                                           & ",HIDB.CUST_CD_M                                       AS CUST_CD_M                                        " & vbNewLine _
                                           & ",ISNULL(KBN_B028.KBN_NM1,'')                          AS SAGYO_STATE_NM                                   " & vbNewLine _
                                           & "--,ISNULL(KBN_B023.KBN_NM1,'')                          AS SYORI_SUB                                      " & vbNewLine _
                                           & ",CASE WHEN KBN_B023_CU.KBN_CD <> '' AND KBN_B023_CU.KBN_CD IS NOT NULL THEN KBN_B023_CU.KBN_NM1           " & vbNewLine _
                                           & "      WHEN KBN_B023_DE.KBN_CD <> '' AND KBN_B023_DE.KBN_CD IS NOT NULL THEN KBN_B023_DE.KBN_NM1           " & vbNewLine _
                                           & "      ELSE KBN_B023.KBN_NM1  END                       AS SYORI_SUB                                       " & vbNewLine _
                                           & ",HIDB.POSTING_DATE                                    AS INOUTKA_DATE                                     " & vbNewLine _
                                           & ",HIDB.FILE_NAME                                       AS FILE_NAME                                        " & vbNewLine _
                                           & ",HIDB.CRT_DATE                                        AS CRT_DATE                                         " & vbNewLine _
                                           & ",KBN_K013.KBN_NM1                                     AS PRINT_KBN_NM                                     " & vbNewLine _
                                           & ",CASE WHEN KBN_B023_CU.KBN_CD <> '' AND KBN_B023_CU.KBN_CD IS NOT NULL THEN KBN_B023_CU.KBN_NM4           " & vbNewLine _
                                           & "      WHEN KBN_B023_DE.KBN_CD <> '' AND KBN_B023_DE.KBN_CD IS NOT NULL THEN KBN_B023_DE.KBN_NM4           " & vbNewLine _
                                           & "      ELSE KBN_B023.KBN_NM4  END                      AS SAGYO_NAIYO                                      " & vbNewLine _
                                           & ",HIDB.TEXT                                            AS TEXT_NM                                          " & vbNewLine _
                                           & ",HIDB.CURRENT_MATERIAL                                AS CURRENT_MATERIAL                                 " & vbNewLine _
                                           & ",HIDB.CURRENT_DESCRIPTION                             AS CURRENT_DESCRIPTION                              " & vbNewLine _
                                           & ",ISNULL(MCC_CURRENT.JOTAI_NM,'')                      AS CURRENT_GOODS_JOTAI                              " & vbNewLine _
                                           & ",HIDB.CURRENT_BATCH                                   AS CURRENT_BATCH                                    " & vbNewLine _
                                           & ",HIDB.CURRENT_QUANTITY                                AS CURRENT_QUANTITY                                 " & vbNewLine _
                                           & ",HIDB.CURRENT_STORAGE_LOCATION                        AS CURRENT_STORAGE_LOCATION                         " & vbNewLine _
                                           & ",MD.DEST_NM                                           AS DEST_NM                                          " & vbNewLine _
                                           & ",HIDB.DESTINATION_MATERIAL                            AS DESTINATION_MATERIAL                             " & vbNewLine _
                                           & ",HIDB.DESTINATION_DESCRIPTION                         AS DESTINATION_DESCRIPTION                          " & vbNewLine _
                                           & ",ISNULL(MCC_DESTINATION.JOTAI_NM,'')                  AS DESTINATION_GOODS_JOTAI                          " & vbNewLine _
                                           & ",HIDB.DESTINATION_BATCH                               AS DESTINATION_BATCH                                " & vbNewLine _
                                           & ",HIDB.DESTINATION_QUANTITY                            AS DESTINATION_QUANTITY                             " & vbNewLine _
                                           & ",HIDB.DESTINATION_STORAGE_LOCATION                    AS DESTINATION_STORAGE_LOCATION                     " & vbNewLine _
                                           & ",HIDB.REC_NO                                          AS REC_NO                                           " & vbNewLine _
                                           & ",UPD_USER.USER_NM                                     AS SYS_UPD_USER                                     " & vbNewLine _
                                           & ",HIDB.SYS_UPD_DATE                                    AS SYS_UPD_DATE                                     " & vbNewLine _
                                           & ",HIDB.SYS_UPD_TIME                                    AS SYS_UPD_TIME                                     " & vbNewLine _
                                           & ",HIDB.JISSEKI_SHORI_FLG                               AS JISSEKI_SHORI_FLG                                " & vbNewLine _
                                           & ",CASE WHEN KBN_B023_CU.KBN_CD <> '' AND KBN_B023_CU.KBN_CD IS NOT NULL THEN KBN_B023_CU.KBN_CD            " & vbNewLine _
                                           & "      WHEN KBN_B023_DE.KBN_CD <> '' AND KBN_B023_DE.KBN_CD IS NOT NULL THEN KBN_B023_DE.KBN_CD            " & vbNewLine _
                                           & "      ELSE KBN_B023.KBN_CD  END                       AS SYORI_KBN                                        " & vbNewLine _
                                           & ",HIDB.PRINT_FLG                                       AS PRINT_FLG                                        " & vbNewLine _
                                           & ",ISNULL(KBN_B028.KBN_CD,'')                           AS SAGYO_STATE_KBN                                  " & vbNewLine

    ''' <summary>
    ''' 倉庫間転送データ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_FROM As String = " FROM                                                                                                " & vbNewLine _
                                            & " $LM_TRN$..H_IDOEDI_DTL_BYK HIDB                                  " & vbNewLine _
                                            & "LEFT JOIN                                                         " & vbNewLine _
                                            & "  $LM_MST$..M_DEST MD                                             " & vbNewLine _
                                            & "ON                                                                " & vbNewLine _
                                            & " HIDB.NRS_BR_CD = MD.NRS_BR_CD                                    " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " CASE WHEN DESTINATION_STORAGE_LOCATION = '2000' THEN DESTINATION_STORAGE_LOCATION  " & vbNewLine _
                                            & "      WHEN CURRENT_STORAGE_LOCATION = '4000' THEN CURRENT_STORAGE_LOCATION          " & vbNewLine _
                                            & "      WHEN DESTINATION_STORAGE_LOCATION = '3000' THEN DESTINATION_STORAGE_LOCATION  " & vbNewLine _
                                            & "      ELSE '' END  = MD.DEST_CD                                   " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " HIDB.CUST_CD_L = MD.CUST_CD_L                                    " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " MD.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                            & " LEFT JOIN                                                        " & vbNewLine _
                                            & " $LM_MST$..Z_KBN KBN_K013                                         " & vbNewLine _
                                            & " ON                                                               " & vbNewLine _
                                            & " HIDB.PRINT_FLG = RIGHT(KBN_K013.KBN_CD,1)                        " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " KBN_K013.KBN_GROUP_CD = 'K013'                                   " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " KBN_K013.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                            & " LEFT JOIN                                                        " & vbNewLine _
                                            & " $LM_MST$..Z_KBN KBN_B023_CU                                      " & vbNewLine _
                                            & " ON                                                               " & vbNewLine _
                                            & " HIDB.CURRENT_STORAGE_LOCATION = KBN_B023_CU.KBN_NM2              " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " KBN_B023_CU.KBN_GROUP_CD = 'B023'                                " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " KBN_B023_CU.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                            & " LEFT JOIN                                                        " & vbNewLine _
                                            & " $LM_MST$..Z_KBN KBN_B023_DE                                      " & vbNewLine _
                                            & " ON                                                               " & vbNewLine _
                                            & " HIDB.DESTINATION_STORAGE_LOCATION = KBN_B023_DE.KBN_NM3          " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " KBN_B023_DE.KBN_GROUP_CD = 'B023'                                " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " KBN_B023_DE.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                            & " LEFT JOIN                                                        " & vbNewLine _
                                            & " $LM_MST$..Z_KBN KBN_B023                                         " & vbNewLine _
                                            & " ON                                                               " & vbNewLine _
                                            & " KBN_B023.KBN_CD = '01'                                           " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " KBN_B023.KBN_GROUP_CD = 'B023'                                   " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " KBN_B023.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                            & " LEFT JOIN                                                        " & vbNewLine _
                                            & " $LM_MST$..Z_KBN KBN_B028                                         " & vbNewLine _
                                            & " ON                                                               " & vbNewLine _
                                            & " KBN_B028.KBN_CD = CASE WHEN HIDB.JISSEKI_SHORI_FLG = '1' THEN '01' " & vbNewLine _
                                            & "                        WHEN HIDB.JISSEKI_SHORI_FLG = '2' THEN '03' " & vbNewLine _
                                            & "                        WHEN HIDB.JISSEKI_SHORI_FLG = '3' THEN '04' " & vbNewLine _
                                            & "                        WHEN HIDB.PRINT_FLG = '1'         THEN '02' " & vbNewLine _
                                            & "                        ELSE '' END                               " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " KBN_B028.KBN_GROUP_CD = 'B028'                                   " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " KBN_B028.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                            & " LEFT JOIN                                                        " & vbNewLine _
                                            & " $LM_MST$..M_CUSTCOND MCC_CURRENT                                 " & vbNewLine _
                                            & " ON                                                               " & vbNewLine _
                                            & " HIDB.NRS_BR_CD = MCC_CURRENT.NRS_BR_CD                           " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " HIDB.CUST_CD_L = MCC_CURRENT.CUST_CD_L                           " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " HIDB.CURRENT_STORAGE_LOCATION = RIGHT(MCC_CURRENT.REMARK,4)      " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " MCC_CURRENT.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                            & " LEFT JOIN                                                        " & vbNewLine _
                                            & " $LM_MST$..M_CUSTCOND MCC_DESTINATION                             " & vbNewLine _
                                            & " ON                                                               " & vbNewLine _
                                            & " HIDB.NRS_BR_CD = MCC_DESTINATION.NRS_BR_CD                       " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " HIDB.CUST_CD_L = MCC_DESTINATION.CUST_CD_L                       " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " HIDB.DESTINATION_STORAGE_LOCATION = RIGHT(MCC_DESTINATION.REMARK,4)  " & vbNewLine _
                                            & " AND                                                              " & vbNewLine _
                                            & " MCC_DESTINATION.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                            & " LEFT JOIN                                                        " & vbNewLine _
                                            & " $LM_MST$..S_USER               UPD_USER                          " & vbNewLine _
                                            & " ON                                                               " & vbNewLine _
                                            & " HIDB.SYS_UPD_USER = UPD_USER.USER_CD                             " & vbNewLine
    ''' <summary>
    ''' 並び順
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY               " & vbNewLine _
                                         & "    HIDB.CRT_DATE      " & vbNewLine _
                                         & "   ,HIDB.FILE_NAME     " & vbNewLine _
                                         & "   ,HIDB.REC_NO        " & vbNewLine

#End Region

    ''' <summary>
    ''' 登録したSENDOUTEDIの数取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_SENDOUTEDI_DATA As String = "   SELECT                                                                   " & vbNewLine _
                                                              & "  count(*)　AS CNT                                                         " & vbNewLine _
                                                              & "  FROM $LM_TRN$..C_OUTKA_L AS CL                                           " & vbNewLine _
                                                              & "  LEFT JOIN $LM_TRN$..C_OUTKA_M AS CM ON                                   " & vbNewLine _
                                                              & "  CL.NRS_BR_CD = CM.NRS_BR_CD                                              " & vbNewLine _
                                                              & "  AND CL.OUTKA_NO_L = CM.OUTKA_NO_L                                        " & vbNewLine _
                                                              & "  AND CM.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                                              & "                                                                           " & vbNewLine _
                                                              & "  LEFT JOIN $LM_TRN$..H_SENDOUTEDI_BYKAGT AS SEND_AGT ON                   " & vbNewLine _
                                                              & "  CM.NRS_BR_CD = SEND_AGT.NRS_BR_CD                                        " & vbNewLine _
                                                              & "  AND CM.OUTKA_NO_L = SEND_AGT.OUTKA_CTL_NO                                " & vbNewLine _
                                                              & "  AND CM.OUTKA_NO_M = SEND_AGT.OUTKA_CTL_NO_CHU                            " & vbNewLine _
                                                              & "  AND SEND_AGT.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                                                              & "                                                                           " & vbNewLine _
                                                              & "  LEFT JOIN $LM_TRN$..H_SENDOUTEDI_BYKEKT AS SEND_EKT ON                   " & vbNewLine _
                                                              & "  CM.NRS_BR_CD = SEND_EKT.NRS_BR_CD                                        " & vbNewLine _
                                                              & "  AND CM.OUTKA_NO_L = SEND_EKT.OUTKA_CTL_NO                                " & vbNewLine _
                                                              & "  AND CM.OUTKA_NO_M = SEND_EKT.OUTKA_CTL_NO_CHU                            " & vbNewLine _
                                                              & "  AND SEND_EKT.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                                                              & "                                                                           " & vbNewLine _
                                                              & "  WHERE                                                                    " & vbNewLine _
                                                              & "                                                                           " & vbNewLine _
                                                              & "  CL.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                                              & "  AND CL.CUST_CD_L IN ('00266','00759')                                    " & vbNewLine _
                                                              & "  AND CL.OUTKAHOKOKU_YN <> '00'                                            " & vbNewLine _
                                                              & "  AND CL.OUTKA_STATE_KB >= '60'                                            " & vbNewLine _
                                                              & "  AND CL.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                                                              & "  AND CL.OUTKA_PLAN_DATE = @SEARCH_FROM                                    " & vbNewLine _
                                                              & "  AND CL.CUST_CD_L = @CUST_CD_L                                            " & vbNewLine _
                                                              & "  AND CL.CUST_CD_M = @CUST_CD_M                                            " & vbNewLine _
                                                              & "  AND ( (SEND_AGT.NRS_BR_CD IS NULL) AND (SEND_EKT.NRS_BR_CD IS NULL))     " & vbNewLine

#End Region

#Region "取込処理 SQL"

#Region "D_ZAI_TRS,M_GOODS(商品+LOTが在庫にありその荷主コードを抽出)"

    ''' <summary>
    '''  BYK商品コード⇒荷主コード割当(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GOODS_CUST_DATA_ZAI As String = " SELECT                                                                                           " & vbNewLine _
                                                       & " DZT.NRS_BR_CD                                       AS NRS_BR_CD                                 " & vbNewLine _
                                                       & ",DZT.CUST_CD_L                                       AS CUST_CD_L                                 " & vbNewLine _
                                                       & ",DZT.CUST_CD_M                                       AS CUST_CD_M                                 " & vbNewLine _
                                                       & " FROM $LM_TRN$..D_ZAI_TRS DZT                                                                     " & vbNewLine _
                                                       & " LEFT JOIN                                                                                        " & vbNewLine _
                                                       & " $LM_MST$..M_GOODS MG                                                                             " & vbNewLine _
                                                       & " ON                                                                                               " & vbNewLine _
                                                       & " DZT.NRS_BR_CD = MG.NRS_BR_CD                                                                     " & vbNewLine _
                                                       & " AND                                                                                              " & vbNewLine _
                                                       & " DZT.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                               " & vbNewLine _
                                                       & " WHERE                                                                                            " & vbNewLine _
                                                       & "     MG.NRS_BR_CD      = @NRS_BR_CD                                                               " & vbNewLine _
                                                       & " AND DZT.NRS_BR_CD     = @NRS_BR_CD                                                               " & vbNewLine _
                                                       & " AND MG.GOODS_CD_CUST  = @DESTINATION_MATERIAL                                                    " & vbNewLine _
                                                       & " AND DZT.LOT_NO        = @DESTINATION_BATCH                                                       " & vbNewLine _
                                                       & " AND DZT.SYS_DEL_FLG   = '0'                                                                      " & vbNewLine _
                                                       & " AND MG.SYS_DEL_FLG   = '0'                                                                       " & vbNewLine _
                                                       & " GROUP BY                                                                                         " & vbNewLine _
                                                       & " DZT.NRS_BR_CD                                                                                    " & vbNewLine _
                                                       & ",DZT.CUST_CD_L                                                                                    " & vbNewLine _
                                                       & ",DZT.CUST_CD_M                                                                                    " & vbNewLine

#End Region

#Region "M_GOODS(商品が商品マスタにありその荷主コードを抽出)"

    ''' <summary>
    '''  BYK商品コード⇒荷主コード割当(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GOODS_CUST_DATA As String = " SELECT                                                                                          " & vbNewLine _
                                                       & " MG.NRS_BR_CD                                       AS NRS_BR_CD                                 " & vbNewLine _
                                                       & ",MG.CUST_CD_L                                       AS CUST_CD_L                                 " & vbNewLine _
                                                       & ",MG.CUST_CD_M                                       AS CUST_CD_M                                 " & vbNewLine _
                                                       & " FROM $LM_MST$..M_GOODS MG                                                                       " & vbNewLine _
                                                       & " WHERE                                                                                           " & vbNewLine _
                                                       & "     MG.NRS_BR_CD      = @NRS_BR_CD                                                              " & vbNewLine _
                                                       & " AND MG.GOODS_CD_CUST  = @DESTINATION_MATERIAL                                                   " & vbNewLine _
                                                       & " AND MG.SYS_DEL_FLG   = '0'                                                                      " & vbNewLine _
                                                       & " GROUP BY                                                                                        " & vbNewLine _
                                                       & " MG.NRS_BR_CD                                                                                    " & vbNewLine _
                                                       & ",MG.CUST_CD_L                                                                                    " & vbNewLine _
                                                       & ",MG.CUST_CD_M                                                                                    " & vbNewLine

#End Region

#Region "H_IDOEDI_DTL_BYK(INSERT)"

    ''' <summary>
    ''' INSERT（H_IDOEDI_DTL_BYK）
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_INSERT_IDOEDI_DTL As String = "INSERT INTO           " & vbNewLine _
                                        & "$LM_TRN$..H_IDOEDI_DTL_BYK       " & vbNewLine _
                                        & "(                                " & vbNewLine _
                                        & "  DEL_KB	                        " & vbNewLine _
                                        & " ,CRT_DATE	                    " & vbNewLine _
                                        & " ,FILE_NAME	                    " & vbNewLine _
                                        & " ,REC_NO	                        " & vbNewLine _
                                        & " ,GYO	                        " & vbNewLine _
                                        & " ,NRS_BR_CD	                    " & vbNewLine _
                                        & " ,INKA_NO_L	                    " & vbNewLine _
                                        & " ,INKA_NO_M	                    " & vbNewLine _
                                        & " ,INKA_NO_S	                    " & vbNewLine _
                                        & " ,OUTKA_NO_L	                    " & vbNewLine _
                                        & " ,OUTKA_NO_M	                    " & vbNewLine _
                                        & " ,OUTKA_NO_S	                    " & vbNewLine _
                                        & " ,CUST_CD_L	                    " & vbNewLine _
                                        & " ,CUST_CD_M	                    " & vbNewLine _
                                        & " ,MOVE_TYPE	                    " & vbNewLine _
                                        & " ,ITEM_NO	                    " & vbNewLine _
                                        & " ,POSTING_DATE	                " & vbNewLine _
                                        & " ,TEXT	                        " & vbNewLine _
                                        & " ,CURRENT_MATERIAL	            " & vbNewLine _
                                        & " ,CURRENT_DESCRIPTION	        " & vbNewLine _
                                        & " ,CURRENT_PLANT	                " & vbNewLine _
                                        & " ,CURRENT_STORAGE_LOCATION	    " & vbNewLine _
                                        & " ,CURRENT_BATCH	                " & vbNewLine _
                                        & " ,CURRENT_QUANTITY	            " & vbNewLine _
                                        & " ,CURRENT_UOM    	            " & vbNewLine _
                                        & " ,DESTINATION_MATERIAL	        " & vbNewLine _
                                        & " ,DESTINATION_DESCRIPTION	    " & vbNewLine _
                                        & " ,DESTINATION_PLANT	            " & vbNewLine _
                                        & " ,DESTINATION_STORAGE_LOCATION   " & vbNewLine _
                                        & " ,DESTINATION_BATCH	            " & vbNewLine _
                                        & " ,DESTINATION_QUANTITY	        " & vbNewLine _
                                        & " ,DESTINATION_UOM    	        " & vbNewLine _
                                        & " ,JISSEKI_SHORI_FLG	            " & vbNewLine _
                                        & " ,JISSEKI_USER	                " & vbNewLine _
                                        & " ,JISSEKI_DATE	                " & vbNewLine _
                                        & " ,JISSEKI_TIME	                " & vbNewLine _
                                        & " ,SEND_USER	                    " & vbNewLine _
                                        & " ,SEND_DATE	                    " & vbNewLine _
                                        & " ,SEND_TIME	                    " & vbNewLine _
                                        & " ,PRINT_FLG	                    " & vbNewLine _
                                        & " ,PRINT_USER	                    " & vbNewLine _
                                        & " ,PRINT_DATE	                    " & vbNewLine _
                                        & " ,PRINT_TIME	                    " & vbNewLine _
                                        & " ,SYS_ENT_DATE                   " & vbNewLine _
                                        & " ,SYS_ENT_TIME                   " & vbNewLine _
                                        & " ,SYS_ENT_PGID                   " & vbNewLine _
                                        & " ,SYS_ENT_USER                   " & vbNewLine _
                                        & " ,SYS_UPD_DATE                   " & vbNewLine _
                                        & " ,SYS_UPD_TIME                   " & vbNewLine _
                                        & " ,SYS_UPD_PGID                   " & vbNewLine _
                                        & " ,SYS_UPD_USER                   " & vbNewLine _
                                        & " ,SYS_DEL_FLG                    " & vbNewLine _
                                        & ")VALUES(                         " & vbNewLine _
                                        & "  @DEL_KB	                    " & vbNewLine _
                                        & " ,@CRT_DATE	                    " & vbNewLine _
                                        & " ,@FILE_NAME	                    " & vbNewLine _
                                        & " ,@REC_NO	                    " & vbNewLine _
                                        & " ,@GYO	                        " & vbNewLine _
                                        & " ,@NRS_BR_CD	                    " & vbNewLine _
                                        & " ,@INKA_NO_L	                    " & vbNewLine _
                                        & " ,@INKA_NO_M	                    " & vbNewLine _
                                        & " ,@INKA_NO_S	                    " & vbNewLine _
                                        & " ,@OUTKA_NO_L	                " & vbNewLine _
                                        & " ,@OUTKA_NO_M	                " & vbNewLine _
                                        & " ,@OUTKA_NO_S	                " & vbNewLine _
                                        & " ,@CUST_CD_L	                    " & vbNewLine _
                                        & " ,@CUST_CD_M	                    " & vbNewLine _
                                        & " ,@MOVE_TYPE	                    " & vbNewLine _
                                        & " ,@ITEM_NO	                    " & vbNewLine _
                                        & " ,@POSTING_DATE	                " & vbNewLine _
                                        & " ,@TEXT	                        " & vbNewLine _
                                        & " ,@CURRENT_MATERIAL	            " & vbNewLine _
                                        & " ,@CURRENT_DESCRIPTION	        " & vbNewLine _
                                        & " ,@CURRENT_PLANT	                " & vbNewLine _
                                        & " ,@CURRENT_STORAGE_LOCATION	    " & vbNewLine _
                                        & " ,@CURRENT_BATCH	                " & vbNewLine _
                                        & " ,@CURRENT_QUANTITY	            " & vbNewLine _
                                        & " ,@CURRENT_UOM   	            " & vbNewLine _
                                        & " ,@DESTINATION_MATERIAL	        " & vbNewLine _
                                        & " ,@DESTINATION_DESCRIPTION	    " & vbNewLine _
                                        & " ,@DESTINATION_PLANT	            " & vbNewLine _
                                        & " ,@DESTINATION_STORAGE_LOCATION  " & vbNewLine _
                                        & " ,@DESTINATION_BATCH	            " & vbNewLine _
                                        & " ,@DESTINATION_QUANTITY	        " & vbNewLine _
                                        & " ,@DESTINATION_UOM   	        " & vbNewLine _
                                        & " ,@JISSEKI_SHORI_FLG	            " & vbNewLine _
                                        & " ,@JISSEKI_USER	                " & vbNewLine _
                                        & " ,@JISSEKI_DATE	                " & vbNewLine _
                                        & " ,@JISSEKI_TIME	                " & vbNewLine _
                                        & " ,@SEND_USER	                    " & vbNewLine _
                                        & " ,@SEND_DATE	                    " & vbNewLine _
                                        & " ,@SEND_TIME	                    " & vbNewLine _
                                        & " ,@PRINT_FLG	                    " & vbNewLine _
                                        & " ,@PRINT_USER	                " & vbNewLine _
                                        & " ,@PRINT_DATE	                " & vbNewLine _
                                        & " ,@PRINT_TIME	                " & vbNewLine _
                                        & ",@SYS_ENT_DATE                   " & vbNewLine _
                                        & ",@SYS_ENT_TIME                   " & vbNewLine _
                                        & ",@SYS_ENT_PGID                   " & vbNewLine _
                                        & ",@SYS_ENT_USER                   " & vbNewLine _
                                        & ",@SYS_UPD_DATE                   " & vbNewLine _
                                        & ",@SYS_UPD_TIME                   " & vbNewLine _
                                        & ",@SYS_UPD_PGID                   " & vbNewLine _
                                        & ",@SYS_UPD_USER                   " & vbNewLine _
                                        & ",@SYS_DEL_FLG                    " & vbNewLine _
                                        & ")                                " & vbNewLine

#End Region

#End Region

#Region "入荷報告作成処理 データ抽出用SQL"

#Region "H_INKAEDI_DTL_BYK SELECT句"

    Private Const SQL_SELECT_BYK_SENDIN_DATA As String = "SELECT                                                                            " & vbNewLine _
                                            & " '0' 						                AS DEL_KB					                    " & vbNewLine _
                                            & " ,INBYK.CRT_DATE				                AS CRT_DATE  					                " & vbNewLine _
                                            & " ,INBYK.FILE_NAME				            AS FILE_NAME					                " & vbNewLine _
                                            & " ,INBYK.REC_NO				                AS REC_NO					                    " & vbNewLine _
                                            & " ,RIGHT('000' + CONVERT(VARCHAR,ROW_NUMBER() OVER (PARTITION BY REC_NO ORDER BY INL.INKA_NO_L,INM.INKA_NO_M,INS.INKA_NO_S)),3) AS GYO " & vbNewLine _
                                            & " ,INBYK.NRS_BR_CD				            AS NRS_BR_CD					                " & vbNewLine _
                                            & " ,INBYK.EDI_CTL_NO				            AS EDI_CTL_NO					                " & vbNewLine _
                                            & " ,INBYK.EDI_CTL_NO_CHU			            AS EDI_CTL_NO_CHU				                " & vbNewLine _
                                            & " ,INL.INKA_NO_L				                AS INKA_CTL_NO_L				                " & vbNewLine _
                                            & " ,INM.INKA_NO_M				                AS INKA_CTL_NO_M				                " & vbNewLine _
                                            & " ,INS.INKA_NO_S				                AS INKA_CTL_NO_S				                " & vbNewLine _
                                            & " ,'' 					                    AS OUTKA_CTL_NO_L				                " & vbNewLine _
                                            & " ,'' 					                    AS OUTKA_CTL_NO_M				                " & vbNewLine _
                                            & " ,'' 					                    AS OUTKA_CTL_NO_S				                " & vbNewLine _
                                            & " ,INL.CUST_CD_L				                AS CUST_CD_L					                " & vbNewLine _
                                            & " ,INL.CUST_CD_M				                AS CUST_CD_M					                " & vbNewLine _
                                            & " ,INL.INKA_DATE				                AS E1BP2017_GM_HEAD_01_PSTNG_DATE		        " & vbNewLine _
                                            & " ,INL.INKA_DATE				                AS E1BP2017_GM_HEAD_01_DOC_DATE			        " & vbNewLine _
                                            & " ,'' 					                    AS E1BP2017_GM_HEAD_01_HEADER_TXT		        " & vbNewLine _
                                            & " ,'01'					                    AS E1BP2017_GM_CODE_GM_CODE			            " & vbNewLine _
                                            & " ,INBYK.MATERIAL 				            AS E1BP2017_GM_ITEM_CREATE_MATERIAL		        " & vbNewLine _
                                            & " --,MG.GOODS_CD_CUST				            AS E1BP2017_GM_ITEM_CREATE_MATERIAL		        " & vbNewLine _
                                            & " ,'3420'					                    AS E1BP2017_GM_ITEM_CREATE_PLANT		        " & vbNewLine _
                                            & " ,CASE		                                                                                " & vbNewLine _
                                            & "     WHEN MCC.REMARK IS NULL AND INL.CUST_CD_L = '00759' AND INL.CUST_CD_M = '01' THEN '5100'" & vbNewLine _
                                            & "     WHEN MCC.REMARK IS NULL AND INL.CUST_CD_L = '00759' AND INL.CUST_CD_M = '02' THEN '5200'" & vbNewLine _
                                            & "     WHEN MCC.REMARK IS NULL THEN '1000'							                            " & vbNewLine _
                                            & "  ELSE RIGHT(MCC.REMARK, 4) END		        AS E1BP2017_GM_ITEM_CREATE_STGE_LOC		        " & vbNewLine _
                                            & " ,INBYK.BATCH					            AS E1BP2017_GM_ITEM_CREATE_BATCH		        " & vbNewLine _
                                            & " --,INS.LOT_NO					            AS E1BP2017_GM_ITEM_CREATE_BATCH		        " & vbNewLine _
                                            & " ,'101'					                    AS E1BP2017_GM_ITEM_CREATE_MOVE_TYPE		    " & vbNewLine _
                                            & " --,INBYK.DERIVERY_QUANTITY	                AS E1BP2017_GM_ITEM_CREATE_ENTRY_QNT		    " & vbNewLine _
                                            & " ,SUM(INS.KONSU * MG.PKG_NB + INS.HASU)	    AS E1BP2017_GM_ITEM_CREATE_ENTRY_QNT		    " & vbNewLine _
                                            & " --,INBYK.PURCHASING_DOCUMENT			    AS E1BP2017_GM_ITEM_CREATE_PO_NUMBER		    " & vbNewLine _
                                            & " ,''                     			        AS E1BP2017_GM_ITEM_CREATE_PO_NUMBER		    " & vbNewLine _
                                            & " ,INBYK.REF_ITEM				                AS E1BP2017_GM_ITEM_CREATE_PO_ITEM		        " & vbNewLine _
                                            & " ,'B'                                        AS E1BP2017_GM_ITEM_CREATE_MVT_IND				" & vbNewLine _
                                            & " ,INBYK.DELIVERY				                AS E1BP2017_GM_ITEM_CREATE_DELIV_NUMB_TO_SEARCH " & vbNewLine _
                                            & " ,INBYK.ITEM_NO				                AS E1BP2017_GM_ITEM_CREATE_DELIV_ITEM_TO_SEARCH	" & vbNewLine _
                                            & " ,'' 					                    AS E1BP2017_GM_ITEM_CREATE_ENTRY_UOM_ISO		" & vbNewLine _
                                            & " ,'' 					                    AS E1BP2017_GM_ITEM_CREATE_MOVE_MAT		        " & vbNewLine _
                                            & " ,'' 					                    AS E1BP2017_GM_ITEM_CREATE_MOVE_PLANT		    " & vbNewLine _
                                            & " ,'' 					                    AS E1BP2017_GM_ITEM_CREATE_MOVE_STLOC		    " & vbNewLine _
                                            & " ,'' 					                    AS E1BP2017_GM_ITEM_CREATE_MOVE_BATCH		    " & vbNewLine _
                                            & " ,'0'					                    AS SAMPLE_HOUKOKU_FLG				            " & vbNewLine _
                                            & " ,'' 					                    AS RECORD_STATUS				                " & vbNewLine _
                                            & " ,'2'					                    AS JISSEKI_SHORI_FLG				            " & vbNewLine

#End Region

#Region "H_INKAEDI_DTL_BYK FROM句"

    Private Const SQL_FROM_BYK_SENDIN_DATA As String = "FROM                                                       　" & vbNewLine _
                                             & "$LM_TRN$..B_INKA_L INL                                             　" & vbNewLine _
                                             & " LEFT OUTER JOIN                                                  　 " & vbNewLine _
                                             & " 	$LM_TRN$..B_INKA_M INM                                           " & vbNewLine _
                                             & " 	ON INL.NRS_BR_CD = INM.NRS_BR_CD                                 " & vbNewLine _
                                             & " 	AND INL.INKA_NO_L = INM.INKA_NO_L                                " & vbNewLine _
                                             & " LEFT OUTER JOIN                                                     " & vbNewLine _
                                             & " $LM_TRN$..H_INKAEDI_DTL_BYK INBYK                                   " & vbNewLine _
                                             & " ON INM.NRS_BR_CD = INBYK.NRS_BR_CD                                  " & vbNewLine _
                                             & " AND INM.INKA_NO_L = INBYK.INKA_CTL_NO_L                             " & vbNewLine _
                                             & " AND INM.INKA_NO_M = INBYK.INKA_CTL_NO_M                             " & vbNewLine _
                                             & " AND INBYK.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                             & " 	LEFT OUTER JOIN                                                  " & vbNewLine _
                                             & " 		$LM_MST$..M_GOODS MG                                         " & vbNewLine _
                                             & " 		ON INM.NRS_BR_CD = MG.NRS_BR_CD                              " & vbNewLine _
                                             & " 		AND INM.GOODS_CD_NRS = MG.GOODS_CD_NRS                       " & vbNewLine _
                                             & " 		LEFT OUTER JOIN                                              " & vbNewLine _
                                             & " 		(                                                            " & vbNewLine _
                                             & " 		SELECT                                                       " & vbNewLine _
                                             & " 			NRS_BR_CD                                                " & vbNewLine _
                                             & " 			,INKA_NO_L                                               " & vbNewLine _
                                             & " 			,INKA_NO_M                                               " & vbNewLine _
                                             & " 			,INKA_NO_S                                               " & vbNewLine _
                                             & " 			,KONSU                                                   " & vbNewLine _
                                             & " 			,HASU                                                    " & vbNewLine _
                                             & " 			,LOT_NO                                                  " & vbNewLine _
                                             & " 			,GOODS_COND_KB_3                                         " & vbNewLine _
                                             & " 			,SYS_DEL_FLG                                             " & vbNewLine _
                                             & " 		FROM $LM_TRN$..B_INKA_S                                      " & vbNewLine _
                                             & " 		WHERE NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                             & " 		GROUP BY                                                     " & vbNewLine _
                                             & " 			NRS_BR_CD                                                " & vbNewLine _
                                             & " 			,INKA_NO_L                                               " & vbNewLine _
                                             & " 			,INKA_NO_M                                               " & vbNewLine _
                                             & " 			,INKA_NO_S                                               " & vbNewLine _
                                             & " 			,KONSU                                                   " & vbNewLine _
                                             & " 			,HASU                                                    " & vbNewLine _
                                             & " 			,LOT_NO                                                  " & vbNewLine _
                                             & " 			,GOODS_COND_KB_3                                         " & vbNewLine _
                                             & " 			,SYS_DEL_FLG                                             " & vbNewLine _
                                             & " 		) INS                                                        " & vbNewLine _
                                             & " 			ON INM.NRS_BR_CD = INS.NRS_BR_CD                         " & vbNewLine _
                                             & " 			AND INM.INKA_NO_L = INS.INKA_NO_L                        " & vbNewLine _
                                             & " 			AND INM.INKA_NO_M = INS.INKA_NO_M                        " & vbNewLine _
                                             & " 			LEFT OUTER JOIN                                          " & vbNewLine _
                                             & " 				$LM_MST$..M_CUSTCOND MCC                             " & vbNewLine _
                                             & " 				ON INL.NRS_BR_CD = MCC.NRS_BR_CD                     " & vbNewLine _
                                             & " 				AND INL.CUST_CD_L = MCC.CUST_CD_L                    " & vbNewLine _
                                             & " 				AND INS.GOODS_COND_KB_3 = MCC.JOTAI_CD               " & vbNewLine _
                                             & " 				AND MCC.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                             & " 			WHERE                                                    " & vbNewLine _
                                             & " 				INBYK.NRS_BR_CD = @NRS_BR_CD                         " & vbNewLine _
                                             & " 				AND INL.CUST_CD_L = @CUST_CD_L                       " & vbNewLine _
                                             & " 				AND INL.CUST_CD_M = @CUST_CD_M                       " & vbNewLine _
                                             & "                AND INL.INKA_DATE = @SEARCH_FROM                     " & vbNewLine _
                                             & " 				AND INL.INKA_STATE_KB = '50'                         " & vbNewLine _
                                             & " 				AND INL.SYS_DEL_FLG   = '0'                          " & vbNewLine _
                                             & " 				AND INBYK.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                             & " 				AND INM.SYS_DEL_FLG   = '0'                          " & vbNewLine _
                                             & " 				AND INS.SYS_DEL_FLG   = '0'                          " & vbNewLine _
                                             & " 				AND INBYK.JISSEKI_SHORI_FLG = '1'                    " & vbNewLine _
                                             & " 			GROUP BY                                                 " & vbNewLine _
                                             & " 				INBYK.DEL_KB                                         " & vbNewLine _
                                             & " 				,INBYK.CRT_DATE                                      " & vbNewLine _
                                             & " 				,INBYK.FILE_NAME                                     " & vbNewLine _
                                             & " 				,INBYK.REC_NO                                        " & vbNewLine _
                                             & " 				,INBYK.GYO                                           " & vbNewLine _
                                             & " 				,INBYK.NRS_BR_CD                                     " & vbNewLine _
                                             & " 				,INBYK.EDI_CTL_NO                                    " & vbNewLine _
                                             & " 			    ,INBYK.EDI_CTL_NO_CHU                                " & vbNewLine _
                                             & " 				,INL.INKA_NO_L                                       " & vbNewLine _
                                             & " 				,INM.INKA_NO_M                                       " & vbNewLine _
                                             & " 				,INS.INKA_NO_S                                       " & vbNewLine _
                                             & " 				,INL.CUST_CD_L                                       " & vbNewLine _
                                             & " 				,INL.CUST_CD_M                                       " & vbNewLine _
                                             & " 				,INL.INKA_DATE                                       " & vbNewLine _
                                             & " 			    ,INL.INKA_DATE                                       " & vbNewLine _
                                             & "                ,INBYK.MATERIAL 				                     " & vbNewLine _
                                             & "-- 				,MG.GOODS_CD_CUST                                    " & vbNewLine _
                                             & " 				,MCC.REMARK                                          " & vbNewLine _
                                             & "                ,INBYK.BATCH					                     " & vbNewLine _
                                             & "-- 				,INS.LOT_NO                                          " & vbNewLine _
                                             & "-- 				,INBYK.PURCHASING_DOCUMENT                           " & vbNewLine _
                                             & "                ,INBYK.DERIVERY_QUANTITY	                         " & vbNewLine _
                                             & " 				,INBYK.REF_ITEM                                      " & vbNewLine _
                                             & " 				,INBYK.DELIVERY                                      " & vbNewLine _
                                             & " 				,INBYK.ITEM_NO                                       " & vbNewLine _
                                             & "            ORDER BY    	                                         " & vbNewLine _
                                             & " 				 INBYK.EDI_CTL_NO                                    " & vbNewLine _
                                             & " 				,INBYK.EDI_CTL_NO_CHU                                " & vbNewLine _
                                             & " 				,INS.INKA_NO_S                                       " & vbNewLine







#End Region

#End Region

#Region "実績作成処理 データ抽出用SQL"

#Region "H_SENDOUTEDI_BYKAGT SELECT句"

    Private Const SQL_SELECT_BYKAGT_SEND_DATA As String = "SELECT                                                                                                                                     " & vbNewLine _
                                      & " BASE.DEL_KB													                                                 AS DEL_KB					                  " & vbNewLine _
                                      & ",BASE.CRT_DATE                                                                                                  AS CRT_DATE                                  " & vbNewLine _
                                      & ",BASE.FILE_NAME                                                                                                 AS FILE_NAME                                 " & vbNewLine _
                                      & ",BASE.REC_NO                                                                                                    AS REC_NO                                    " & vbNewLine _
                                      & ",BASE.GYO                                                                                                       AS GYO                                       " & vbNewLine _
                                      & ",BASE.NRS_BR_CD                                                                                                 AS NRS_BR_CD                                 " & vbNewLine _
                                      & ",BASE.EDI_CTL_NO                                                                                                AS EDI_CTL_NO                                " & vbNewLine _
                                      & ",BASE.EDI_CTL_NO_CHU                                                                                            AS EDI_CTL_NO_CHU                            " & vbNewLine _
                                      & ",BASE.OUTKA_CTL_NO                                                                                              AS OUTKA_CTL_NO                              " & vbNewLine _
                                      & ",BASE.OUTKA_CTL_NO_CHU			                                                                                 AS OUTKA_CTL_NO_CHU                          " & vbNewLine _
                                      & ",BASE.CUST_CD_L                                                                                                 AS CUST_CD_L                                 " & vbNewLine _
                                      & ",BASE.CUST_CD_M                                                                                                 AS CUST_CD_M                                 " & vbNewLine _
                                      & ",BASE.E1EDK01_ACTION                                                                                            AS E1EDK01_ACTION                            " & vbNewLine _
                                      & ",BASE.E1EDK01_CURCY                                                                                             AS E1EDK01_CURCY                             " & vbNewLine _
                                      & ",BASE.E1EDK01_LIFSK                                                                                             AS E1EDK01_LIFSK                             " & vbNewLine _
                                      & ",BASE.E1EDK02_QUALF_BELNR_002                                                                                   AS E1EDK02_QUALF_BELNR_002                   " & vbNewLine _
                                      & ",BASE.E1EDK14_QUALF_ORGID_012                                                                                   AS E1EDK14_QUALF_ORGID_012                   " & vbNewLine _
                                      & ",BASE.E1EDK14_QUALF_ORGID_008                                                                                   AS E1EDK14_QUALF_ORGID_008                   " & vbNewLine _
                                      & ",BASE.E1EDK14_QUALF_ORGID_007                                                                                   AS E1EDK14_QUALF_ORGID_007                   " & vbNewLine _
                                      & ",BASE.E1EDK14_QUALF_ORGID_006                                                                                   AS E1EDK14_QUALF_ORGID_006                   " & vbNewLine _
                                      & ",BASE.E1EDKA1_PARVW_LIFNR_AG                                                                                    AS E1EDKA1_PARVW_LIFNR_AG                    " & vbNewLine _
                                      & ",CASE WHEN BASE.CUST_CD_L = '00266' THEN                                                                                                                     " & vbNewLine _
                                      & "    CASE WHEN BASE.SAMPLE_HOUKOKU_FLG = '0' THEN                                                                                                             " & vbNewLine _
                                      & "        CASE WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '250556' THEN                                                                                                " & vbNewLine _
                                      & "            CASE WHEN (BASE.E1EDKA1_PARVW_LIFNR_WE = '' AND BASE.E1EDKA1_PARVW_PARTN_ZZ = '') THEN                                                           " & vbNewLine _
                                      & "                '99999999999'                                                                                                                                " & vbNewLine _
                                      & "            ELSE   BASE.E1EDKA1_PARVW_PARTN_ZZ + BASE.E1EDKA1_PARVW_LIFNR_WE                                                                                 " & vbNewLine _
                                      & "            END                                                                                                                                              " & vbNewLine _
                                      & "        WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '250570' THEN                                                                                                     " & vbNewLine _
                                      & "            CASE WHEN (BASE.E1EDKA1_PARVW_LIFNR_WE = '' OR LEN(BASE.E1EDKA1_PARVW_LIFNR_WE) <= 11) THEN                                                      " & vbNewLine _
                                      & "                '9999999999'                                                                                                                                 " & vbNewLine _
                                      & "            ELSE LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,12)                                                                                                        " & vbNewLine _
                                      & "            END                                                                                                                                              " & vbNewLine _
                                      & "        ELSE                                                                                                                                                 " & vbNewLine _
                                      & "            CASE WHEN BASE.E1EDKA1_PARVW_LIFNR_WE = '' THEN '9999999999'                                                                                     " & vbNewLine _
                                      & "            ELSE BASE.E1EDKA1_PARVW_LIFNR_WE                                                                                                                 " & vbNewLine _
                                      & "            END                                                                                                                                              " & vbNewLine _
                                      & "        END                                                                                                                                                  " & vbNewLine _
                                      & "    ELSE '100000'                                                                                                                                            " & vbNewLine _
                                      & "    END                                                                                                                                                      " & vbNewLine _
                                      & "WHEN BASE.CUST_CD_L = '00759' THEN                                                                                                                           " & vbNewLine _
                                      & "    CASE WHEN BASE.SAMPLE_HOUKOKU_FLG = '0' THEN                                                                                                             " & vbNewLine _
                                      & "        CASE WHEN (BASE.E1EDKA1_PARVW_LIFNR_WE = '' AND BASE.E1EDKA1_PARVW_PARTN_ZZ = '') THEN                                                               " & vbNewLine _
                                      & "            '99999999999'                                                                                                                                    " & vbNewLine _
                                      & "        ELSE   BASE.E1EDKA1_PARVW_PARTN_ZZ + BASE.E1EDKA1_PARVW_LIFNR_WE                                                                                     " & vbNewLine _
                                      & "        END                                                                                                                                                  " & vbNewLine _
                                      & "    ELSE '100000'                                                                                                                                            " & vbNewLine _
                                      & "    END                                                                                                                                                      " & vbNewLine _
                                      & "ELSE                                                                                                                                                         " & vbNewLine _
                                      & "    BASE.E1EDKA1_PARVW_LIFNR_WE                                                                                                                              " & vbNewLine _
                                      & "END 														AS E1EDKA1_PARVW_LIFNR_WE                                                                         " & vbNewLine _
                                      & "--,BASE.E1EDKA1_PARVW_LIFNR_WE                                                                                                                               " & vbNewLine _
                                      & ",CASE WHEN BASE.CUST_CD_L = '00266' THEN                                                                                                                     " & vbNewLine _
                                      & "    CASE WHEN BASE.SAMPLE_HOUKOKU_FLG = '0' THEN                                                                                                             " & vbNewLine _
                                      & "        CASE WHEN BASE.E1EDKA1_PARVW_PARTN_ZZ = '' THEN                                                                                                      " & vbNewLine _
                                      & "            CASE WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '250556' THEN                                                                                            " & vbNewLine _
                                      & "                 LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,6)                                                                                                         " & vbNewLine _
                                      & "            WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '250563' THEN                                                                                                 " & vbNewLine _
                                      & "                 LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,5)                                                                                                         " & vbNewLine _
                                      & "            WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '250564' THEN                                                                                                 " & vbNewLine _
                                      & "                 LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,5)                                                                                                         " & vbNewLine _
                                      & "            WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '250570' THEN                                                                                                 " & vbNewLine _
                                      & "                 LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,6)                                                                                                         " & vbNewLine _
                                      & "            WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '252370' THEN                                                                                                 " & vbNewLine _
                                      & "                 LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,5)                                                                                                         " & vbNewLine _
                                      & "            ELSE '99999'                                                                                                                                     " & vbNewLine _
                                      & "            END                                                                                                                                              " & vbNewLine _
                                      & "        ELSE BASE.E1EDKA1_PARVW_PARTN_ZZ                                                                                                                     " & vbNewLine _
                                      & "        END                                                                                                                                                  " & vbNewLine _
                                      & "    ELSE ''                                                                                                                                                  " & vbNewLine _
                                      & "    END                                                                                                                                                      " & vbNewLine _
                                      & "WHEN BASE.CUST_CD_L = '00759' THEN                                                                                                                           " & vbNewLine _
                                      & "    CASE WHEN BASE.SAMPLE_HOUKOKU_FLG = '0' THEN                                                                                                             " & vbNewLine _
                                      & "        CASE WHEN BASE.E1EDKA1_PARVW_PARTN_ZZ = '' THEN                                                                                                      " & vbNewLine _
                                      & "            CASE WHEN BASE.E1EDKA1_PARVW_LIFNR_WE = '' THEN                                                                                                  " & vbNewLine _
                                      & "                '999999'                                                                                                                                     " & vbNewLine _
                                      & "            ELSE LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,6)                                                                                                         " & vbNewLine _
                                      & "            END                                                                                                                                              " & vbNewLine _
                                      & "        ELSE BASE.E1EDKA1_PARVW_PARTN_ZZ                                                                                                                     " & vbNewLine _
                                      & "        END                                                                                                                                                  " & vbNewLine _
                                      & "    ELSE ''                                                                                                                                                  " & vbNewLine _
                                      & "    END                                                                                                                                                      " & vbNewLine _
                                      & "ELSE                                                                                                                                                         " & vbNewLine _
                                      & "    BASE.E1EDKA1_PARVW_PARTN_ZZ                                                                                                                              " & vbNewLine _
                                      & "END 														AS E1EDKA1_PARVW_PARTN_ZZ                                                                         " & vbNewLine _
                                      & "--,BASE.E1EDKA1_PARVW_PARTN_ZZ                                                                                                                               " & vbNewLine _
                                      & ",BASE.E1EDK03_IDDAT_DATUM_001											                                         AS E1EDK03_IDDAT_DATUM_001                   " & vbNewLine _
                                      & ",BASE.E1EDKT1_TDID                                                                                              AS E1EDKT1_TDID                              " & vbNewLine _
                                      & ",BASE.E1EDKT1_TSSPRAS                                                                                           AS E1EDKT1_TSSPRAS                           " & vbNewLine _
                                      & ",BASE.E1EDKT1_TDOBJECT                                                                                          AS E1EDKT1_TDOBJECT                          " & vbNewLine _
                                      & ",BASE.E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1                                                                         AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1         " & vbNewLine _
                                      & ",BASE.E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2                                                                         AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2         " & vbNewLine _
                                      & ",BASE.E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3                                                                         AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3         " & vbNewLine _
                                      & ",BASE.E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4                                                                         AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4         " & vbNewLine _
                                      & ",BASE.E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5                                                                         AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5         " & vbNewLine _
                                      & ",BASE.E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6                                                                         AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6         " & vbNewLine _
                                      & ",BASE.E1EDK02_QUALF_BELNR_001                                                                                   AS E1EDK02_QUALF_BELNR_001                   " & vbNewLine _
                                      & ",BASE.E1EDK02_QUALF_BELNR_043                                                                                   AS E1EDK02_QUALF_BELNR_043                   " & vbNewLine _
                                      & ",BASE.E1EDP01_POSEX                                                                                             AS E1EDP01_POSEX                             " & vbNewLine _
                                      & ",BASE.E1EDP01_MATNR                                                                                             AS E1EDP01_MATNR                             " & vbNewLine _
                                      & ",BASE.E1EDP01_MENGE                                                                                             AS E1EDP01_MENGE                             " & vbNewLine _
                                      & ",BASE.E1EDP01_MENEE                                                                                             AS E1EDP01_MENEE                             " & vbNewLine _
                                      & ",BASE.E1EDP01_WERKS                                                                                             AS E1EDP01_WERKS                             " & vbNewLine _
                                      & ",BASE.E1EDP01_VSTEL                                                                                             AS E1EDP01_VSTEL                             " & vbNewLine _
                                      & ",BASE.E1EDP01_LGORT                                                                                             AS E1EDP01_LGORT                             " & vbNewLine _
                                      & ",BASE.E1EDP02_QUALF_BELNR_043                                                                                   AS E1EDP02_QUALF_BELNR_043                   " & vbNewLine _
                                      & ",BASE.E1EDP02_QUALF_ZEILE_043                                                                                   AS E1EDP02_QUALF_ZEILE_043                   " & vbNewLine _
                                      & ",BASE.E1EDP02_QUALF_BELNR_044                                                                                   AS E1EDP02_QUALF_BELNR_044                   " & vbNewLine _
                                      & ",BASE.E1EDP03_QUALF_DATE_010                                                                                    AS E1EDP03_QUALF_DATE_010                    " & vbNewLine _
                                      & ",BASE.E1EDP03_QUALF_DATE_024                                                                                    AS E1EDP03_QUALF_DATE_024                    " & vbNewLine _
                                      & ",BASE.E1EDP20_WMENG_EDATU                                                                                       AS E1EDP20_WMENG_EDATU                       " & vbNewLine _
                                      & ",BASE.E1EDP19_QUALF_IDTNR_002                                                                                   AS E1EDP19_QUALF_IDTNR_002                   " & vbNewLine _
                                      & ",BASE.E1EDP19_QUALF_IDTNR_010                                                                                   AS E1EDP19_QUALF_IDTNR_010                   " & vbNewLine _
                                      & ",BASE.E1EDKA1_PARVW_PARTN_DUMMY                                                                                 AS E1EDKA1_PARVW_PARTN_DUMMY                 " & vbNewLine _
                                      & ",BASE.E1EDKA1_NAME1                                                                                             AS E1EDKA1_NAME1                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_NAME2                                                                                             AS E1EDKA1_NAME2                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_NAME3                                                                                             AS E1EDKA1_NAME3                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_NAME4                                                                                             AS E1EDKA1_NAME4                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_STRAS                                                                                             AS E1EDKA1_STRAS                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_STRS2                                                                                             AS E1EDKA1_STRS2                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_ORT01                                                                                             AS E1EDKA1_ORT01                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_PSTLZ                                                                                             AS E1EDKA1_PSTLZ                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_LAND1                                                                                             AS E1EDKA1_LAND1                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_TELF1                                                                                             AS E1EDKA1_TELF1                             " & vbNewLine _
                                      & ",BASE.SAMPLE_HOUKOKU_FLG                                                                                        AS SAMPLE_HOUKOKU_FLG                        " & vbNewLine _
                                      & ",BASE.RECORD_STATUS                                                                                             AS RECORD_STATUS                             " & vbNewLine _
                                      & ",BASE.JISSEKI_SHORI_FLG                                                                                         AS JISSEKI_SHORI_FLG                         " & vbNewLine _
                                      & ",BASE.SAMPLEHOUKOKU_SHORI_FLG                                                                                   AS SAMPLEHOUKOKU_SHORI_FLG                   " & vbNewLine _
                                      & "FROM (                                                                                                                                                       " & vbNewLine _
                                      & " SELECT	                               							" & vbNewLine _
                                      & " '0'                                          AS DEL_KB	                               " & vbNewLine _
                                      & ",@SYS_UPD_DATE                                AS CRT_DATE                                 " & vbNewLine _
                                      & ",OUT_L.OUTKA_NO_L + '_' + OUT_M.OUTKA_NO_M + '_' + OUT_S.OUTKA_NO_S + '_' + @SYS_UPD_DATE + '_' + @SYS_UPD_TIME   AS FILE_NAME                                " & vbNewLine _
                                      & ",'00001'                                      AS REC_NO                                   " & vbNewLine _
                                      & ",'001'                                        AS GYO                                      " & vbNewLine _
                                      & ",OUT_L.NRS_BR_CD                              AS NRS_BR_CD                                " & vbNewLine _
                                      & ",''                                           AS EDI_CTL_NO                               " & vbNewLine _
                                      & ",''                                           AS EDI_CTL_NO_CHU                           " & vbNewLine _
                                      & ",OUT_M.OUTKA_NO_L                             AS OUTKA_CTL_NO                             " & vbNewLine _
                                      & ",OUT_M.OUTKA_NO_M                             AS OUTKA_CTL_NO_CHU                         " & vbNewLine _
                                      & ",OUT_L.CUST_CD_L                              AS CUST_CD_L                                " & vbNewLine _
                                      & ",OUT_L.CUST_CD_M                              AS CUST_CD_M                                " & vbNewLine _
                                      & ",'004'                                        AS E1EDK01_ACTION                           " & vbNewLine _
                                      & ",'JPY'                                        AS E1EDK01_CURCY                            " & vbNewLine _
                                      & ",'ZJ'                                         AS E1EDK01_LIFSK                            " & vbNewLine _
                                      & ",''                                           AS E1EDK02_QUALF_BELNR_002                  " & vbNewLine _
                                      & ",'ZSO'                                        AS E1EDK14_QUALF_ORGID_012                  " & vbNewLine _
                                      & ",'3420'                                       AS E1EDK14_QUALF_ORGID_008                  " & vbNewLine _
                                      & ",'01'                                         AS E1EDK14_QUALF_ORGID_007                  " & vbNewLine _
                                      & ",'01'                                         AS E1EDK14_QUALF_ORGID_006                  " & vbNewLine _
                                      & "  ,CASE WHEN MIN_GOODS.CUST_CD_S <> '01' THEN --修正開始 2014.12.22                                                                                      " & vbNewLine _
                                      & "   CASE WHEN OUT_L.CUST_CD_L='00266' THEN                                                                                                                " & vbNewLine _
                                      & "    CASE WHEN   OUT_L.CUST_CD_M = '02' THEN                                                                                                              " & vbNewLine _
                                      & "  		CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2) = 'NE' THEN Z_B018_02.KBN_NM5                                                                          " & vbNewLine _
                                      & "  	    　　 WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2) = 'NC' THEN Z_B018_06.KBN_NM1                                                                          " & vbNewLine _
                                      & "      ELSE Z_B018_02.KBN_NM1 END                                                                                                                         " & vbNewLine _
                                      & "      WHEN  OUT_L.CUST_CD_M = '03' THEN Z_B018_03.KBN_NM1                                                                                                " & vbNewLine _
                                      & "      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') THEN Z_B018_01.KBN_NM1                                                                     " & vbNewLine _
                                      & "      ELSE '' END                                                                                                                                        " & vbNewLine _
                                      & "    ELSE                                                                                                                                                 " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2)='TT' OR  SUBSTRING(OUT_L.CUST_ORD_NO,1,2)='T-' THEN Z_B018_01.KBN_NM1 ELSE                                " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2)='DN' THEN Z_B018_02.KBN_NM1 ELSE                                                                          " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2)='NE' THEN Z_B018_02.KBN_NM5 ELSE                                                                          " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2)='C-' THEN Z_B018_03.KBN_NM1 ELSE                                                                          " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2)='NC' THEN Z_B018_06.KBN_NM1 ELSE '' END END END END END END                                               " & vbNewLine _
                                      & "   WHEN MIN_GOODS.CUST_CD_S =  '01' THEN                                                                                                                 " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,1)='T' THEN Z_B018_01.KBN_NM1 ELSE                                                                           " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,1)='B' THEN Z_B018_05.KBN_NM1 ELSE                                                                           " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,1)='N' THEN Z_B018_02.KBN_NM1 ELSE                                                                           " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,1)='C' THEN Z_B018_03.KBN_NM1 ELSE '' END END END END END AS E1EDKA1_PARVW_LIFNR_AG  --修正開始 2014.12.22   " & vbNewLine _
                                      & ",CASE WHEN OUT_L.CUST_CD_M = '02' THEN OUT_L.DENP_NO                                      " & vbNewLine _
                                      & "      WHEN OUT_L.CUST_CD_M = '03' THEN OUT_L.DENP_NO                                      " & vbNewLine _
                                      & "      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND (OUT_L.SYS_ENT_PGID = 'LMC020' OR OUT_L.SYS_ENT_PGID = 'LBC100') AND M_GOODS.CUST_CD_S <> '01' THEN OUT_L.DENP_NO  " & vbNewLine _
                                      & "      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND (OUT_L.SYS_ENT_PGID <> 'LMC020' AND OUT_L.SYS_ENT_PGID <> 'LBC100') THEN CONVERT(nvarchar(5),DTL_BYKTST.NONYU_CD)    " & vbNewLine _
                                      & "      ELSE OUT_L.DEST_CD END E1EDKA1_PARVW_LIFNR_WE                                       " & vbNewLine _
                                      & ",CASE WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND (OUT_L.SYS_ENT_PGID = 'LMC020' OR OUT_L.SYS_ENT_PGID = 'LBC100') THEN OUT_L.SHIP_CD_L  " & vbNewLine _
                                      & "      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND (OUT_L.SYS_ENT_PGID <> 'LMC020' AND OUT_L.SYS_ENT_PGID <> 'LBC100') THEN CONVERT(nvarchar(6),DTL_BYKTST.TOKUI_CD)    " & vbNewLine _
                                      & "      ELSE OUT_L.SHIP_CD_L END E1EDKA1_PARVW_PARTN_ZZ                                     " & vbNewLine _
                                      & ",OUT_L.ARR_PLAN_DATE                          AS E1EDK03_IDDAT_DATUM_001                  " & vbNewLine _
                                      & ",'Z015'                                       AS E1EDKT1_TDID                             " & vbNewLine _
                                      & ",'JA'                                         AS E1EDKT1_TSSPRAS                          " & vbNewLine _
                                      & ",'VBBK'                                       AS E1EDKT1_TDOBJECT                         " & vbNewLine _
                                      & ",CASE WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND (OUT_L.SYS_ENT_PGID = 'LMC020' OR OUT_L.SYS_ENT_PGID = 'LBC100') THEN M_DEST.DEST_NM   " & vbNewLine _
                                      & "      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND (OUT_L.SYS_ENT_PGID <> 'LMC020' AND OUT_L.SYS_ENT_PGID <> 'LBC100') THEN DTL_BYKTST.NONYU_NM1    " & vbNewLine _
                                      & "      ELSE M_DEST.DEST_NM END E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1                           " & vbNewLine _
                                      & "--,CASE WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID = 'LMC020' THEN ''               " & vbNewLine _
                                      & "--      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID <> 'LMC020' THEN ISNULL(RTRIM(DTL_BYKTST.NONYU_NM),'') " & vbNewLine _
                                      & "--      ELSE ''             END E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2                           " & vbNewLine _
                                      & ",''                                           AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2        " & vbNewLine _
                                      & "--,M_DEST.AD_1                                  AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3        " & vbNewLine _
                                      & "--,M_DEST.AD_2                                  AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4        " & vbNewLine _
                                      & "--,M_DEST.AD_3                                  AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5        " & vbNewLine _
                                      & ",OUT_L.DEST_AD_1                              AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3        " & vbNewLine _
                                      & ",OUT_L.DEST_AD_2                              AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4        " & vbNewLine _
                                      & ",OUT_L.DEST_AD_3                              AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5        " & vbNewLine _
                                      & ",M_DEST.TEL                                   AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6        " & vbNewLine _
                                      & "--,CASE WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID = 'LMC020' THEN OUT_L.CUST_ORD_NO        " & vbNewLine _
                                      & "--      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID <> 'LMC020' THEN OUT_M.BUYER_ORD_NO_DTL  " & vbNewLine _
                                      & "--      ELSE OUT_L.CUST_ORD_NO END E1EDK02_QUALF_BELNR_001                                  " & vbNewLine _
                                      & ",CASE WHEN OUT_L.CUST_CD_L = '00759' THEN                                                  " & vbNewLine _
                                      & "      CASE WHEN RTrim(LTrim(OUT_M.BUYER_ORD_NO_DTL)) <> '' THEN OUT_M.BUYER_ORD_NO_DTL     " & vbNewLine _
                                      & "	   ELSE OUT_L.CUST_ORD_NO END                                                           " & vbNewLine _
                                      & " ELSE                                                                                      " & vbNewLine _
                                      & "	   CASE WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND (OUT_L.SYS_ENT_PGID = 'LMC020' OR OUT_L.SYS_ENT_PGID = 'LBC100') THEN OUT_L.CUST_ORD_NO " & vbNewLine _
                                      & "      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND (OUT_L.SYS_ENT_PGID <> 'LMC020' AND OUT_L.SYS_ENT_PGID <> 'LBC100') THEN OUT_M.BUYER_ORD_NO_DTL " & vbNewLine _
                                      & "      ELSE OUT_L.CUST_ORD_NO END                                                           " & vbNewLine _
                                      & " END                                           AS E1EDK02_QUALF_BELNR_001                  " & vbNewLine _
                                      & "--,OUT_M.RSV_NO                                 AS E1EDK02_QUALF_BELNR_043                  " & vbNewLine _
                                      & ",REPLACE(LEFT(ISNULL(OUT_S.SERIAL_NO,''),CHARINDEX('-',ISNULL(OUT_S.SERIAL_NO,''))),'-','')  AS E1EDK02_QUALF_BELNR_043                  " & vbNewLine _
                                      & "--,SUBSTRING(ISNULL(OUT_S.SERIAL_NO,''),CHARINDEX('-',ISNULL(OUT_S.SERIAL_NO,'')) +1,40) AS E1EDP01_POSEX            " & vbNewLine _
                                      & ",''                                           AS E1EDP01_POSEX                            " & vbNewLine _
                                      & "--要望管理番号2387                                                                        " & vbNewLine _
                                      & "--,M_GOODS.GOODS_CD_CUST                        AS E1EDP01_MATNR                            " & vbNewLine _
                                      & "--,Replace(M_GOODS.GOODS_CD_CUST,'-NP','')      AS E1EDP01_MATNR                            " & vbNewLine _
                                      & ",Replace(M_GOODS.GOODS_CD_CUST,M_GOODS.CLASS_3,'')      AS E1EDP01_MATNR                  " & vbNewLine _
                                      & ",ISNULL(OUT_S.ALCTD_NB,0)                     AS E1EDP01_MENGE                            " & vbNewLine _
                                      & ",'PCE'                                        AS E1EDP01_MENEE                            " & vbNewLine _
                                      & ",'3420'                                       AS E1EDP01_WERKS                            " & vbNewLine _
                                      & ",''                                           AS E1EDP01_VSTEL                            " & vbNewLine _
                                      & "--,ISNULL(M_CUSTC.REMARK,'1000')                AS E1EDP01_LGORT                            " & vbNewLine _
                                      & ",CASE WHEN M_CUSTC.REMARK IS NULL THEN '1000'                                             " & vbNewLine _
                                      & "      ELSE REVERSE(CONVERT(VARCHAR(4), REVERSE(M_CUSTC.REMARK))) END  E1EDP01_LGORT       " & vbNewLine _
                                      & ",REPLACE(LEFT(ISNULL(OUT_S.SERIAL_NO,''),CHARINDEX('-',ISNULL(OUT_S.SERIAL_NO,''))),'-','')  AS E1EDP02_QUALF_BELNR_043                  " & vbNewLine _
                                      & "--,OUT_M.RSV_NO                                 AS E1EDP02_QUALF_BELNR_043                  " & vbNewLine _
                                      & "--,ISNULL(OUT_S.REMARK,'')                      AS E1EDP02_QUALF_ZEILE_043                  " & vbNewLine _
                                      & ",SUBSTRING(ISNULL(OUT_S.SERIAL_NO,''),CHARINDEX('-',ISNULL(OUT_S.SERIAL_NO,'')) +1,40) AS E1EDP02_QUALF_ZEILE_043            " & vbNewLine _
                                      & "--,CASE WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID = 'LMC020' THEN OUT_L.BUYER_ORD_NO       " & vbNewLine _
                                      & "--      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID <> 'LMC020' THEN OUT_M.CUST_ORD_NO_DTL   " & vbNewLine _
                                      & "--      ELSE OUT_L.BUYER_ORD_NO END E1EDP02_QUALF_BELNR_044                  " & vbNewLine _
                                      & ",CASE WHEN OUT_L.CUST_CD_L = '00759' THEN RTrim(LTrim(OUT_M.BUYER_ORD_NO_DTL))                                             " & vbNewLine _
                                      & " ELSE                                                                                                                      " & vbNewLine _
                                      & " 	CASE WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND (OUT_L.SYS_ENT_PGID = 'LMC020' OR OUT_L.SYS_ENT_PGID = 'LBC100') THEN OUT_L.BUYER_ORD_NO  " & vbNewLine _
                                      & "   WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND (OUT_L.SYS_ENT_PGID <> 'LMC020' AND OUT_L.SYS_ENT_PGID <> 'LBC100') THEN OUT_M.BUYER_ORD_NO_DTL  " & vbNewLine _
                                      & "   ELSE OUT_L.BUYER_ORD_NO END                                                                                             " & vbNewLine _
                                      & " END AS E1EDP02_QUALF_BELNR_044                                                                                            " & vbNewLine _
                                      & ",OUT_L.OUTKA_PLAN_DATE                        AS E1EDP03_QUALF_DATE_010                   " & vbNewLine _
                                      & ",OUT_L.OUTKA_PLAN_DATE                        AS E1EDP03_QUALF_DATE_024                   " & vbNewLine _
                                      & ",OUT_L.OUTKA_PLAN_DATE                        AS E1EDP20_WMENG_EDATU                      " & vbNewLine _
                                      & "--要望管理番号2387                                                                        " & vbNewLine _
                                      & "--,M_GOODS.GOODS_CD_CUST                      AS E1EDP19_QUALF_IDTNR_002                  " & vbNewLine _
                                      & "--,Replace(M_GOODS.GOODS_CD_CUST,'-NP','')      AS E1EDP19_QUALF_IDTNR_002                  " & vbNewLine _
                                      & ",Replace(M_GOODS.GOODS_CD_CUST,M_GOODS.CLASS_3,'')      AS E1EDP19_QUALF_IDTNR_002        " & vbNewLine _
                                      & ",ISNULL(OUT_S.LOT_NO,'')                      AS E1EDP19_QUALF_IDTNR_010                  " & vbNewLine _
                                      & "--要望番号2091(2013.10.24) 追加START                                                      " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN '100000'                                         " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_PARVW_PARTN_DUMMY               " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN OUT_L.DEST_NM                                    " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_NAME1                           " & vbNewLine _
                                      & ",''                                           AS E1EDKA1_NAME2                            " & vbNewLine _
                                      & ",''                                           AS E1EDKA1_NAME3                            " & vbNewLine _
                                      & ",''                                           AS E1EDKA1_NAME4                            " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  OUT_L.DEST_AD_1                                 " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_STRAS                           " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  OUT_L.DEST_AD_2                                 " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_STRS2                           " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  OUT_L.DEST_AD_3                                 " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_ORT01                           " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  M_DEST.ZIP                                      " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_PSTLZ                           " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  'JP'                                            " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_LAND1                           " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  M_DEST.TEL                                      " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_TELF1                           " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN '1'                                              " & vbNewLine _
                                      & "      ELSE '0'                                END SAMPLE_HOUKOKU_FLG                      " & vbNewLine _
                                      & "--要望番号2091(2013.10.24) 追加END                                                        " & vbNewLine _
                                      & ",''                                           AS RECORD_STATUS                            " & vbNewLine _
                                      & ",'2'                                          AS JISSEKI_SHORI_FLG                        " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN '2'                                              " & vbNewLine _
                                      & "      ELSE '0'                                END SAMPLEHOUKOKU_SHORI_FLG                 " & vbNewLine

#End Region

#Region "H_SENDOUTEDI_BYKAGT FROM句"

    Private Const SQL_FROM_BYKAGT_SEND_DATA As String = "FROM                                                       " & vbNewLine _
                                          & "$LM_TRN$..C_OUTKA_M OUT_M                                              " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..C_OUTKA_L OUT_L                                   " & vbNewLine _
                                          & "  ON  OUT_L.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                          & "  AND OUT_L.NRS_BR_CD = OUT_M.NRS_BR_CD                                " & vbNewLine _
                                          & "  AND OUT_L.OUTKA_NO_L = OUT_M.OUTKA_NO_L                              " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDI_L                                " & vbNewLine _
                                          & "  ON  EDI_L.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                          & "  AND EDI_L.NRS_BR_CD = OUT_L.NRS_BR_CD                                " & vbNewLine _
                                          & "  AND EDI_L.OUTKA_CTL_NO = OUT_L.OUTKA_NO_L                            " & vbNewLine _
                                          & "LEFT JOIN                                                              " & vbNewLine _
                                          & "(SELECT                                                                " & vbNewLine _
                                          & "      TST.DEL_KB                                                       " & vbNewLine _
                                          & "     ,TST.NRS_BR_CD                                                    " & vbNewLine _
                                          & "--   ,TST.CRT_DATE                                                     " & vbNewLine _
                                          & "--   ,TST.FILE_NAME                                                    " & vbNewLine _
                                          & "--   ,TST.REC_NO                                                       " & vbNewLine _
                                          & "     ,TST.EDI_CTL_NO                                                   " & vbNewLine _
                                          & "     ,TST.NONYU_CD                                                     " & vbNewLine _
                                          & "     ,TST.TOKUI_CD                                                     " & vbNewLine _
                                          & "     ,TST.NONYU_NM1                                                    " & vbNewLine _
                                          & "--   ,MIN(TST.NONYU_NM2 + TST.NONYU_NM3) AS NONYU_NM                   " & vbNewLine _
                                          & "--   ,TST.NONYU_NM2                                                    " & vbNewLine _
                                          & "--   ,TST.NONYU_NM3                                                    " & vbNewLine _
                                          & "--   ,TST.JUTYU_NO                                                     " & vbNewLine _
                                          & "--   ,TST.ORDER_NO                                                     " & vbNewLine _
                                          & "     FROM                                                              " & vbNewLine _
                                          & "     $LM_TRN$..H_OUTKAEDI_DTL_BYKTST TST                               " & vbNewLine _
                                          & "     WHERE                                                             " & vbNewLine _
                                          & "         TST.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                          & "     AND TST.CUST_CD_L = @CUST_CD_L                                    " & vbNewLine _
                                          & "     AND TST.CUST_CD_M = @CUST_CD_M                                    " & vbNewLine _
                                          & "     GROUP BY                                                          " & vbNewLine _
                                          & "      TST.DEL_KB                                                       " & vbNewLine _
                                          & "     ,TST.NRS_BR_CD                                                    " & vbNewLine _
                                          & "--   ,TST.CRT_DATE                                                     " & vbNewLine _
                                          & "--   ,TST.FILE_NAME                                                    " & vbNewLine _
                                          & "--   ,TST.REC_NO                                                       " & vbNewLine _
                                          & "     ,TST.EDI_CTL_NO                                                   " & vbNewLine _
                                          & "     ,TST.NONYU_CD                                                     " & vbNewLine _
                                          & "     ,TST.TOKUI_CD                                                     " & vbNewLine _
                                          & "     ,TST.NONYU_NM1                                                    " & vbNewLine _
                                          & "--   ,TST.NONYU_NM2                                                    " & vbNewLine _
                                          & "--   ,TST.NONYU_NM3                                                    " & vbNewLine _
                                          & "--   ,TST.JUTYU_NO                                                     " & vbNewLine _
                                          & "--   ,TST.ORDER_NO                                                     " & vbNewLine _
                                          & ") DTL_BYKTST                                                           " & vbNewLine _
                                          & "  ON  DTL_BYKTST.DEL_KB <> '1'                                         " & vbNewLine _
                                          & "  AND DTL_BYKTST.NRS_BR_CD = EDI_L.NRS_BR_CD                           " & vbNewLine _
                                          & "  AND DTL_BYKTST.EDI_CTL_NO = EDI_L.EDI_CTL_NO                         " & vbNewLine _
                                          & "-- AND DTL_BYKTST.EDI_CTL_NO_CHU = EDI_M.EDI_CTL_NO_CHU                " & vbNewLine _
                                          & "-- AND DTL_BYKTST.JISSEKI_SHORI_FLG = '1'                              " & vbNewLine _
                                          & " LEFT JOIN                                                             " & vbNewLine _
                                          & " (SELECT                                                               " & vbNewLine _
                                          & "      S2.NRS_BR_CD                                                     " & vbNewLine _
                                          & "     ,S2.OUTKA_NO_L                                                    " & vbNewLine _
                                          & "     ,S2.OUTKA_NO_M                                                    " & vbNewLine _
                                          & "     ,MAX(S2.OUTKA_NO_S)  AS OUTKA_NO_S                                " & vbNewLine _
                                          & "     ,S2.LOT_NO                                                        " & vbNewLine _
                                          & "     ,S2.ZAI_REC_NO                                                    " & vbNewLine _
                                          & "     ,S2.SERIAL_NO                                                        " & vbNewLine _
                                          & "     ,SUM(S2.ALCTD_NB) AS ALCTD_NB                                     " & vbNewLine _
                                          & "     FROM                                                              " & vbNewLine _
                                          & "     $LM_TRN$..C_OUTKA_S S2                                            " & vbNewLine _
                                          & "     WHERE                                                             " & vbNewLine _
                                          & "     S2.NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                                          & "--     AND                                                               " & vbNewLine _
                                          & "--     S2.OUTKA_NO_L = @OUTKA_CTL_NO                                     " & vbNewLine _
                                          & "     AND                                                               " & vbNewLine _
                                          & "     S2.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                          & "     GROUP BY                                                          " & vbNewLine _
                                          & "      S2.NRS_BR_CD                                                     " & vbNewLine _
                                          & "     ,S2.OUTKA_NO_L                                                    " & vbNewLine _
                                          & "     ,S2.OUTKA_NO_M                                                    " & vbNewLine _
                                          & "     ,S2.LOT_NO                                                        " & vbNewLine _
                                          & "     ,S2.ZAI_REC_NO                                                    " & vbNewLine _
                                          & "     ,S2.SERIAL_NO                                                        " & vbNewLine _
                                          & " ) OUT_S                                                               " & vbNewLine _
                                          & "  ON                                                                   " & vbNewLine _
                                          & "      OUT_S.NRS_BR_CD = OUT_M.NRS_BR_CD                                " & vbNewLine _
                                          & "  AND OUT_S.OUTKA_NO_L = OUT_M.OUTKA_NO_L                              " & vbNewLine _
                                          & "  AND OUT_S.OUTKA_NO_M = OUT_M.OUTKA_NO_M                              " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI_TRS                                 " & vbNewLine _
                                          & "  ON  ZAI_TRS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                          & "  AND ZAI_TRS.NRS_BR_CD = OUT_S.NRS_BR_CD                              " & vbNewLine _
                                          & "  AND ZAI_TRS.ZAI_REC_NO = OUT_S.ZAI_REC_NO                            " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..H_SENDOUTEDI_BYKAGT SND_BYKAGT                    " & vbNewLine _
                                          & " ON  SND_BYKAGT.DEL_KB <> '1'                                          " & vbNewLine _
                                          & " AND SND_BYKAGT.NRS_BR_CD = OUT_M.NRS_BR_CD                            " & vbNewLine _
                                          & " AND SND_BYKAGT.OUTKA_CTL_NO = OUT_M.OUTKA_NO_L                        " & vbNewLine _
                                          & " AND SND_BYKAGT.OUTKA_CTL_NO_CHU = OUT_M.OUTKA_NO_M                    " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_GOODS  M_GOODS                                  " & vbNewLine _
                                          & "  ON  M_GOODS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                          & "  AND M_GOODS.NRS_BR_CD = OUT_L.NRS_BR_CD                              " & vbNewLine _
                                          & "  AND M_GOODS.CUST_CD_L = OUT_L.CUST_CD_L                              " & vbNewLine _
                                          & "  AND M_GOODS.CUST_CD_M = OUT_L.CUST_CD_M                              " & vbNewLine _
                                          & "  AND M_GOODS.GOODS_CD_NRS = OUT_M.GOODS_CD_NRS                        " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_DEST  M_DEST                                    " & vbNewLine _
                                          & "  ON  M_DEST.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                          & "  AND M_DEST.NRS_BR_CD = OUT_L.NRS_BR_CD                               " & vbNewLine _
                                          & "  AND M_DEST.CUST_CD_L = OUT_L.CUST_CD_L                               " & vbNewLine _
                                          & "  AND M_DEST.DEST_CD = OUT_L.DEST_CD                                   " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_CUSTCOND  M_CUSTC                               " & vbNewLine _
                                          & "  ON  M_CUSTC.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                          & "  AND M_CUSTC.NRS_BR_CD = ZAI_TRS.NRS_BR_CD                            " & vbNewLine _
                                          & "  AND M_CUSTC.CUST_CD_L = ZAI_TRS.CUST_CD_L                            " & vbNewLine _
                                          & "  AND M_CUSTC.JOTAI_CD = ZAI_TRS.GOODS_COND_KB_3                       " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..Z_KBN  Z_B018_01                                  " & vbNewLine _
                                          & "  ON  Z_B018_01.KBN_GROUP_CD = 'B018'                                  " & vbNewLine _
                                          & "  AND Z_B018_01.KBN_CD = '01'                                          " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..Z_KBN  Z_B018_02                                  " & vbNewLine _
                                          & "  ON  Z_B018_02.KBN_GROUP_CD = 'B018'                                  " & vbNewLine _
                                          & "  AND Z_B018_02.KBN_CD = '02'                                          " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..Z_KBN  Z_B018_03                                  " & vbNewLine _
                                          & "  ON  Z_B018_03.KBN_GROUP_CD = 'B018'                                  " & vbNewLine _
                                          & "  AND Z_B018_03.KBN_CD = '03'                                          " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..Z_KBN  Z_B018_05          --追加開始 2014.12.22   " & vbNewLine _
                                          & "  ON  Z_B018_05.KBN_GROUP_CD = 'B018'                                  " & vbNewLine _
                                          & "  AND Z_B018_05.KBN_CD = '05'                                          " & vbNewLine _
                                          & " LEFT JOIN LM_MST..Z_KBN  Z_B018_06                                    " & vbNewLine _
                                          & "  ON  Z_B018_06.KBN_GROUP_CD = 'B018'                                  " & vbNewLine _
                                          & "  AND Z_B018_06.KBN_CD = '06'                                          " & vbNewLine _
                                          & " LEFT JOIN                                                             " & vbNewLine _
                                          & " (SELECT                                                               " & vbNewLine _
                                          & "	 SUBCOM.NRS_BR_CD       AS NRS_BR_CD                                " & vbNewLine _
                                          & "	,SUBCOM.OUTKA_NO_L      AS OUTKA_NO_L                               " & vbNewLine _
                                          & "	,MIN(SUBCOM.OUTKA_NO_M) AS OUTKA_NO_M                               " & vbNewLine _
                                          & "  FROM                                                                 " & vbNewLine _
                                          & "	$LM_TRN$..C_OUTKA_M SUBCOM                                          " & vbNewLine _
                                          & "  LEFT JOIN                                                            " & vbNewLine _
                                          & "	$LM_TRN$..C_OUTKA_L SUBCOL                                          " & vbNewLine _
                                          & "	ON  SUBCOL.NRS_BR_CD  = SUBCOM.NRS_BR_CD                            " & vbNewLine _
                                          & "	AND SUBCOL.OUTKA_NO_L = SUBCOM.OUTKA_NO_L                           " & vbNewLine _
                                          & "  WHERE                                                                " & vbNewLine _
                                          & "	    SUBCOM.NRS_BR_CD   = @NRS_BR_CD                                 " & vbNewLine _
                                          & "	AND SUBCOL.CUST_CD_L   = @CUST_CD_L                                 " & vbNewLine _
                                          & "	AND SUBCOL.CUST_CD_M   = @CUST_CD_M                                 " & vbNewLine _
                                          & "	AND SUBCOM.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                          & "	AND SUBCOL.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                          & "  GROUP BY                                                             " & vbNewLine _
                                          & "	 SUBCOM.NRS_BR_CD                                                   " & vbNewLine _
                                          & "	,SUBCOM.OUTKA_NO_L                                                  " & vbNewLine _
                                          & ") MIN_COM                                                              " & vbNewLine _
                                          & "  ON  MIN_COM.NRS_BR_CD   = OUT_L.NRS_BR_CD                            " & vbNewLine _
                                          & "  AND MIN_COM.OUTKA_NO_L  = OUT_L.OUTKA_NO_L                           " & vbNewLine _
                                          & " LEFT JOIN                                                             " & vbNewLine _
                                          & " (SELECT                                                               " & vbNewLine _
                                          & "	 SUBCOM2.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
                                          & "	,SUBCOM2.OUTKA_NO_L   AS OUTKA_NO_L                                 " & vbNewLine _
                                          & "	,SUBCOM2.OUTKA_NO_M   AS OUTKA_NO_M                                 " & vbNewLine _
                                          & "	,SUBCOM2.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
                                          & "  FROM                                                                 " & vbNewLine _
                                          & "	$LM_TRN$..C_OUTKA_M SUBCOM2                                         " & vbNewLine _
                                          & "  LEFT JOIN                                                            " & vbNewLine _
                                          & "	$LM_TRN$..C_OUTKA_L SUBCOL2                                         " & vbNewLine _
                                          & "	ON  SUBCOL2.NRS_BR_CD  = SUBCOM2.NRS_BR_CD                          " & vbNewLine _
                                          & "	AND SUBCOL2.OUTKA_NO_L = SUBCOM2.OUTKA_NO_L                         " & vbNewLine _
                                          & "  WHERE                                                                " & vbNewLine _
                                          & "	    SUBCOM2.NRS_BR_CD   = @NRS_BR_CD                                " & vbNewLine _
                                          & "	AND SUBCOL2.CUST_CD_L   = @CUST_CD_L                                " & vbNewLine _
                                          & "	AND SUBCOL2.CUST_CD_M   = @CUST_CD_M                                " & vbNewLine _
                                          & "	AND SUBCOM2.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                          & "	AND SUBCOL2.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                          & ") MIN_CD                                                               " & vbNewLine _
                                          & "  ON  MIN_CD.NRS_BR_CD   = MIN_COM.NRS_BR_CD                           " & vbNewLine _
                                          & "  AND MIN_CD.OUTKA_NO_L  = MIN_COM.OUTKA_NO_L                          " & vbNewLine _
                                          & "  AND MIN_CD.OUTKA_NO_M  = MIN_COM.OUTKA_NO_M                          " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_GOODS  MIN_GOODS                              " & vbNewLine _
                                          & "  ON  MIN_GOODS.NRS_BR_CD    = MIN_CD.NRS_BR_CD                      " & vbNewLine _
                                          & "  AND MIN_GOODS.GOODS_CD_NRS = MIN_CD.GOODS_CD_NRS                   " & vbNewLine _
                                          & "  AND MIN_GOODS.SYS_DEL_FLG  = '0'             --追加終了 2014.12.22 " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_BYKEKT AS BYKEKT                   " & vbNewLine _
                                          & "  ON  BYKEKT.NRS_BR_CD       = OUT_L.NRS_BR_CD                         " & vbNewLine _
                                          & "  AND BYKEKT.OUTKA_CTL_NO    = OUT_L.OUTKA_NO_L                        " & vbNewLine _
                                          & "  AND BYKEKT.SYS_DEL_FLG = '0'                 --追加終了 2017.07.20   " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_BYKDIR AS BYKDIR                   " & vbNewLine _
                                          & "  ON  BYKDIR.NRS_BR_CD       = OUT_L.NRS_BR_CD                         " & vbNewLine _
                                          & "  AND BYKDIR.OUTKA_CTL_NO    = OUT_L.OUTKA_NO_L                        " & vbNewLine _
                                          & "  AND BYKDIR.SYS_DEL_FLG = '0'                 --追加終了 2017.08.01   " & vbNewLine _
                                          & " WHERE                                                                 " & vbNewLine _
                                          & "       OUT_M.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                          & "  AND  OUT_M.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                          & "  AND  OUT_L.CUST_CD_L = @CUST_CD_L                                    " & vbNewLine _
                                          & "  AND  OUT_L.CUST_CD_M = @CUST_CD_M                                    " & vbNewLine _
                                          & "  AND  OUT_L.OUTKA_PLAN_DATE = @SEARCH_FROM                            " & vbNewLine _
                                          & "  AND  OUT_L.OUTKA_STATE_KB >= '60'                                    " & vbNewLine _
                                          & "  AND  OUT_L.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                          & "--  AND  OUT_L.SYS_ENT_PGID = 'LMC020'                                 " & vbNewLine _
                                          & "  AND  OUT_L.SYUBETU_KB <> '50'                                        " & vbNewLine _
                                          & "--  (2013.11.25) 要望番号2130 追加START                                " & vbNewLine _
                                          & "  AND  OUT_L.OUTKAHOKOKU_YN <> '00'                                    " & vbNewLine _
                                          & "--  (2013.11.25) 要望番号2130 追加END                                  " & vbNewLine _
                                          & "  AND  BYKEKT.OUTKA_CTL_NO IS NULL                                     " & vbNewLine _
                                          & "  AND  BYKDIR.OUTKA_CTL_NO IS NULL                                     " & vbNewLine _
                                          & "  AND  SND_BYKAGT.SYS_DEL_FLG IS NULL                                  " & vbNewLine _
                                          & "  ) BASE                                  				                " & vbNewLine

#End Region


#End Region

#Region "移動(入荷)報告作成処理 データ抽出用SQL"

#Region "H_IDOEDI_DTL_BYK SELECT句"

    Private Const SQL_SELECT_BYK_SENDIDO_DATA As String = "SELECT                                                                            " & vbNewLine _
                                            & " '0' 						                AS DEL_KB					                    " & vbNewLine _
                                            & " ,HIDB.CRT_DATE				                AS CRT_DATE  					                " & vbNewLine _
                                            & " ,HIDB.FILE_NAME				                AS FILE_NAME					                " & vbNewLine _
                                            & " ,HIDB.REC_NO				                AS REC_NO					                    " & vbNewLine _
                                            & " ,HIDB.GYO					                AS GYO						                    " & vbNewLine _
                                            & " ,HIDB.NRS_BR_CD				                AS NRS_BR_CD					                " & vbNewLine _
                                            & " ,''				                            AS EDI_CTL_NO					                " & vbNewLine _
                                            & " ,''                 			            AS EDI_CTL_NO_CHU				                " & vbNewLine _
                                            & " ,HIDB.INKA_NO_L				                AS INKA_CTL_NO_L				                " & vbNewLine _
                                            & " ,HIDB.INKA_NO_M				                AS INKA_CTL_NO_M				                " & vbNewLine _
                                            & " ,HIDB.INKA_NO_S				                AS INKA_CTL_NO_S				                " & vbNewLine _
                                            & " ,HIDB.OUTKA_NO_L 	                        AS OUTKA_CTL_NO_L				                " & vbNewLine _
                                            & " ,HIDB.OUTKA_NO_M                            AS OUTKA_CTL_NO_M				                " & vbNewLine _
                                            & " ,HIDB.OUTKA_NO_S	                        AS OUTKA_CTL_NO_S				                " & vbNewLine _
                                            & " ,HIDB.CUST_CD_L				                AS CUST_CD_L					                " & vbNewLine _
                                            & " ,HIDB.CUST_CD_M				                AS CUST_CD_M					                " & vbNewLine _
                                            & " ,CASE WHEN HIDB.POSTING_DATE = '' THEN @SYS_UPD_DATE                                    	" & vbNewLine _
                                            & "       ELSE HIDB.POSTING_DATE END            AS E1BP2017_GM_HEAD_01_PSTNG_DATE	            " & vbNewLine _
                                            & " ,CASE WHEN HIDB.POSTING_DATE = '' THEN @SYS_UPD_DATE                                    	" & vbNewLine _
                                            & "       ELSE HIDB.POSTING_DATE END            AS E1BP2017_GM_HEAD_01_DOC_DATE	                " & vbNewLine _
                                            & " ,'' 					                    AS E1BP2017_GM_HEAD_01_HEADER_TXT		        " & vbNewLine _
                                            & " ,'04'					                    AS E1BP2017_GM_CODE_GM_CODE			            " & vbNewLine _
                                            & " ,HIDB.CURRENT_MATERIAL  		            AS E1BP2017_GM_ITEM_CREATE_MATERIAL		        " & vbNewLine _
                                            & " ,'3420'					                    AS E1BP2017_GM_ITEM_CREATE_PLANT		        " & vbNewLine _
                                            & " ,HIDB.CURRENT_STORAGE_LOCATION 		        AS E1BP2017_GM_ITEM_CREATE_STGE_LOC		        " & vbNewLine _
                                            & " ,HIDB.CURRENT_BATCH			                AS E1BP2017_GM_ITEM_CREATE_BATCH		        " & vbNewLine _
                                            & " ,HIDB.MOVE_TYPE 		                    AS E1BP2017_GM_ITEM_CREATE_MOVE_TYPE		    " & vbNewLine _
                                            & " ,HIDB.CURRENT_QUANTITY              	    AS E1BP2017_GM_ITEM_CREATE_ENTRY_QNT		    " & vbNewLine _
                                            & " ,''			                                AS E1BP2017_GM_ITEM_CREATE_PO_NUMBER		    " & vbNewLine _
                                            & " ,''				                            AS E1BP2017_GM_ITEM_CREATE_PO_ITEM		        " & vbNewLine _
                                            & " ,''                                         AS E1BP2017_GM_ITEM_CREATE_MVT_IND				" & vbNewLine _
                                            & " ,''				                            AS E1BP2017_GM_ITEM_CREATE_DELIV_NUMB_TO_SEARCH " & vbNewLine _
                                            & " ,''				                            AS E1BP2017_GM_ITEM_CREATE_DELIV_ITEM_TO_SEARCH	" & vbNewLine _
                                            & " ,HIDB.DESTINATION_UOM 					    AS E1BP2017_GM_ITEM_CREATE_ENTRY_UOM_ISO        " & vbNewLine _
                                            & " ,HIDB.DESTINATION_MATERIAL   				AS E1BP2017_GM_ITEM_CREATE_MOVE_MAT		        " & vbNewLine _
                                            & " ,HIDB.DESTINATION_PLANT 					AS E1BP2017_GM_ITEM_CREATE_MOVE_PLANT		    " & vbNewLine _
                                            & " ,HIDB.DESTINATION_STORAGE_LOCATION 			AS E1BP2017_GM_ITEM_CREATE_MOVE_STLOC		    " & vbNewLine _
                                            & " ,HIDB.DESTINATION_BATCH 					AS E1BP2017_GM_ITEM_CREATE_MOVE_BATCH		    " & vbNewLine _
                                            & " ,'0'					                    AS SAMPLE_HOUKOKU_FLG				            " & vbNewLine _
                                            & " ,'' 					                    AS RECORD_STATUS				                " & vbNewLine _
                                            & " ,'2'					                    AS JISSEKI_SHORI_FLG				            " & vbNewLine

#End Region

#Region "H_IDOEDI_DTL_BYK FROM句"

    Private Const SQL_FROM_BYK_SENDIDO_DATA As String = "FROM                                                        " & vbNewLine _
                                             & "$LM_TRN$..H_IDOEDI_DTL_BYK HIDB                                      " & vbNewLine _
                                             & " 			WHERE                                                    " & vbNewLine _
                                             & " 				    HIDB.CRT_DATE  = @CRT_DATE                       " & vbNewLine _
                                             & " 				AND HIDB.FILE_NAME = @FILE_NAME                      " & vbNewLine _
                                             & " 				AND HIDB.REC_NO    = @REC_NO                         " & vbNewLine _
                                             & " 				AND HIDB.JISSEKI_SHORI_FLG = '1'                     " & vbNewLine

#End Region

#End Region

#Region "入荷報告作成 更新用SQL"

#Region "H_SENDINOUTEDI_BYK"

    ''' <summary>
    ''' 実績TBLのINSERT（H_SENDINOUTEDI_BYK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_H_SENDINOUTEDI_BYK As String = "INSERT INTO                    " & vbNewLine _
                                     & "$LM_TRN$..H_SENDINOUTEDI_BYK                        " & vbNewLine _
                                     & "(                                	                " & vbNewLine _
                                     & " DEL_KB                                             " & vbNewLine _
                                     & " ,CRT_DATE                                           " & vbNewLine _
                                     & " ,FILE_NAME                                          " & vbNewLine _
                                     & " ,REC_NO                                             " & vbNewLine _
                                     & " ,GYO                                                " & vbNewLine _
                                     & " ,NRS_BR_CD                                          " & vbNewLine _
                                     & " ,EDI_CTL_NO                                         " & vbNewLine _
                                     & " ,EDI_CTL_NO_CHU                                     " & vbNewLine _
                                     & " ,INKA_CTL_NO_L                                      " & vbNewLine _
                                     & " ,INKA_CTL_NO_M                                      " & vbNewLine _
                                     & " ,INKA_CTL_NO_S                                      " & vbNewLine _
                                     & " ,OUTKA_CTL_NO_L                                     " & vbNewLine _
                                     & " ,OUTKA_CTL_NO_M                                     " & vbNewLine _
                                     & " ,OUTKA_CTL_NO_S                                     " & vbNewLine _
                                     & " ,CUST_CD_L                                          " & vbNewLine _
                                     & " ,CUST_CD_M                                          " & vbNewLine _
                                     & " ,E1BP2017_GM_HEAD_01_PSTNG_DATE                     " & vbNewLine _
                                     & " ,E1BP2017_GM_HEAD_01_DOC_DATE                       " & vbNewLine _
                                     & " ,E1BP2017_GM_HEAD_01_HEADER_TXT                     " & vbNewLine _
                                     & " ,E1BP2017_GM_CODE_GM_CODE                           " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_MATERIAL                   " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_PLANT                      " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_STGE_LOC                   " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_BATCH                      " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_MOVE_TYPE                  " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_ENTRY_QNT                  " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_PO_NUMBER                  " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_PO_ITEM                    " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_MVT_IND                    " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_DELIV_NUMB_TO_SEARCH       " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_DELIV_ITEM_TO_SEARCH       " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_ENTRY_UOM_ISO              " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_MOVE_MAT                   " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_MOVE_PLANT                 " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_MOVE_STLOC                 " & vbNewLine _
                                     & " ,E1BP2017_GM_ITEM_CREATE_MOVE_BATCH                 " & vbNewLine _
                                     & " ,SAMPLE_HOUKOKU_FLG                                 " & vbNewLine _
                                     & " ,RECORD_STATUS                                      " & vbNewLine _
                                     & " ,JISSEKI_SHORI_FLG                                  " & vbNewLine _
                                     & " ,JISSEKI_USER                                       " & vbNewLine _
                                     & " ,JISSEKI_DATE                                       " & vbNewLine _
                                     & " ,JISSEKI_TIME                                       " & vbNewLine _
                                     & " ,SEND_USER                                          " & vbNewLine _
                                     & " ,SEND_DATE                                          " & vbNewLine _
                                     & " ,SEND_TIME                                          " & vbNewLine _
                                     & " ,SAMPLEHOUKOKU_SHORI_FLG                            " & vbNewLine _
                                     & " ,SAMPLEHOUKOKU_USER                                 " & vbNewLine _
                                     & " ,SAMPLEHOUKOKU_DATE                                 " & vbNewLine _
                                     & " ,SAMPLEHOUKOKU_TIME                                 " & vbNewLine _
                                     & " ,DELETE_USER                                        " & vbNewLine _
                                     & " ,DELETE_DATE                                        " & vbNewLine _
                                     & " ,DELETE_TIME                                        " & vbNewLine _
                                     & " ,DELETE_EDI_NO                                      " & vbNewLine _
                                     & " ,DELETE_EDI_NO_CHU                                  " & vbNewLine _
                                     & " ,UPD_USER                                           " & vbNewLine _
                                     & " ,UPD_DATE                                           " & vbNewLine _
                                     & " ,UPD_TIME                                           " & vbNewLine _
                                     & " ,SYS_ENT_DATE                                       " & vbNewLine _
                                     & " ,SYS_ENT_TIME                                       " & vbNewLine _
                                     & " ,SYS_ENT_PGID                                       " & vbNewLine _
                                     & " ,SYS_ENT_USER                                       " & vbNewLine _
                                     & " ,SYS_UPD_DATE                                       " & vbNewLine _
                                     & " ,SYS_UPD_TIME                                       " & vbNewLine _
                                     & " ,SYS_UPD_PGID                                       " & vbNewLine _
                                     & " ,SYS_UPD_USER                                       " & vbNewLine _
                                     & " ,SYS_DEL_FLG                                        " & vbNewLine _
                                     & ")VALUES(                                             " & vbNewLine _
                                     & " @DEL_KB                                             " & vbNewLine _
                                     & " ,@CRT_DATE                                          " & vbNewLine _
                                     & " ,@FILE_NAME                                         " & vbNewLine _
                                     & " ,@REC_NO                                            " & vbNewLine _
                                     & " ,@GYO                                               " & vbNewLine _
                                     & " ,@NRS_BR_CD                                         " & vbNewLine _
                                     & " ,@EDI_CTL_NO                                        " & vbNewLine _
                                     & " ,@EDI_CTL_NO_CHU                                    " & vbNewLine _
                                     & " ,@INKA_CTL_NO_L                                     " & vbNewLine _
                                     & " ,@INKA_CTL_NO_M                                     " & vbNewLine _
                                     & " ,@INKA_CTL_NO_S                                     " & vbNewLine _
                                     & " ,@OUTKA_CTL_NO_L                                    " & vbNewLine _
                                     & " ,@OUTKA_CTL_NO_M                                    " & vbNewLine _
                                     & " ,@OUTKA_CTL_NO_S                                    " & vbNewLine _
                                     & " ,@CUST_CD_L                                         " & vbNewLine _
                                     & " ,@CUST_CD_M                                         " & vbNewLine _
                                     & " ,@E1BP2017_GM_HEAD_01_PSTNG_DATE                    " & vbNewLine _
                                     & " ,@E1BP2017_GM_HEAD_01_DOC_DATE                      " & vbNewLine _
                                     & " ,@E1BP2017_GM_HEAD_01_HEADER_TXT                    " & vbNewLine _
                                     & " ,@E1BP2017_GM_CODE_GM_CODE                          " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_MATERIAL                  " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_PLANT                     " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_STGE_LOC                  " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_BATCH                     " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_MOVE_TYPE                 " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_ENTRY_QNT                 " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_PO_NUMBER                 " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_PO_ITEM                   " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_MVT_IND                   " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_DELIV_NUMB_TO_SEARCH      " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_DELIV_ITEM_TO_SEARCH      " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_ENTRY_UOM_ISO             " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_MOVE_MAT                  " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_MOVE_PLANT                " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_MOVE_STLOC                " & vbNewLine _
                                     & " ,@E1BP2017_GM_ITEM_CREATE_MOVE_BATCH                " & vbNewLine _
                                     & " ,@SAMPLE_HOUKOKU_FLG                                " & vbNewLine _
                                     & " ,@RECORD_STATUS                                     " & vbNewLine _
                                     & " ,@JISSEKI_SHORI_FLG                                 " & vbNewLine _
                                     & " ,@JISSEKI_USER                                      " & vbNewLine _
                                     & " ,@JISSEKI_DATE                                      " & vbNewLine _
                                     & " ,@JISSEKI_TIME                                      " & vbNewLine _
                                     & " ,@SEND_USER                                         " & vbNewLine _
                                     & " ,@SEND_DATE                                         " & vbNewLine _
                                     & " ,@SEND_TIME                                         " & vbNewLine _
                                     & " ,@SAMPLEHOUKOKU_SHORI_FLG                           " & vbNewLine _
                                     & " ,@SAMPLEHOUKOKU_USER                                " & vbNewLine _
                                     & " ,@SAMPLEHOUKOKU_DATE                                " & vbNewLine _
                                     & " ,@SAMPLEHOUKOKU_TIME                                " & vbNewLine _
                                     & " ,@DELETE_USER                                       " & vbNewLine _
                                     & " ,@DELETE_DATE                                       " & vbNewLine _
                                     & " ,@DELETE_TIME                                       " & vbNewLine _
                                     & " ,@DELETE_EDI_NO                                     " & vbNewLine _
                                     & " ,@DELETE_EDI_NO_CHU                                 " & vbNewLine _
                                     & " ,@UPD_USER                                          " & vbNewLine _
                                     & " ,@UPD_DATE                                          " & vbNewLine _
                                     & " ,@UPD_TIME                                          " & vbNewLine _
                                     & " ,@SYS_ENT_DATE                                      " & vbNewLine _
                                     & " ,@SYS_ENT_TIME                                      " & vbNewLine _
                                     & " ,@SYS_ENT_PGID                                      " & vbNewLine _
                                     & " ,@SYS_ENT_USER                                      " & vbNewLine _
                                     & " ,@SYS_UPD_DATE                                      " & vbNewLine _
                                     & " ,@SYS_UPD_TIME                                      " & vbNewLine _
                                     & " ,@SYS_UPD_PGID                                      " & vbNewLine _
                                     & " ,@SYS_UPD_USER                                      " & vbNewLine _
                                     & " ,@SYS_DEL_FLG                                       " & vbNewLine _
                                     & ")                                                    " & vbNewLine



#End Region

#Region "H_INKAEDI_L(BYK入荷報告作成)"
    ''' <summary>
    ''' H_INKAEDI_L(BYK入荷報告作成)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKISAKUSEI_EDI_L As String = "UPDATE                                  " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_L                             " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                 " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
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
                                              & " JISSEKI_FLAG      = '0'                           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine



#End Region

#Region "H_INKAEDI_M(BYK入荷報告作成)"
    ''' <summary>
    ''' H_INKAEDI_M(BYK入荷報告作成)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKISAKUSEI_EDI_M As String = "UPDATE                                  " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_M                             " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                 " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
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
                                              & " OUT_KB            = '0'                           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " JISSEKI_FLAG      = '0'                           " & vbNewLine

#End Region

#Region "H_INKAEDI_DTL_BYK(BYK入荷報告作成)"

    ''' <summary>
    ''' H_INKAEDI_DTL_BYK(BYK入荷報告作成)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_RCV_DTL_BYK As String = "UPDATE $LM_TRN$..H_INKAEDI_DTL_BYK SET        " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
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
                                              & " JISSEKI_SHORI_FLG  = '1'                          " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine

#End Region

#Region "B_INKA_L"
    ''' <summary>
    ''' B_INKA_LのUPDATE文（B_INKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKISAKUSEI_INKA_L As String = "UPDATE $LM_TRN$..B_INKA_L             " & vbNewLine _
                                              & " SET                                                 " & vbNewLine _
                                              & " INKA_STATE_KB     = '90'                            " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                   " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                   " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                   " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                   " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                      " & vbNewLine _
                                              & "AND INKA_NO_L      = @INKA_CTL_NO_L                  " & vbNewLine _
                                              & "AND SYS_DEL_FLG     <> '1'                           " & vbNewLine
#End Region

#End Region

#Region "出荷報告作成処理 更新用SQL"

#Region "H_SENDOUTEDI_BYKDIR"

    ''' <summary>
    ''' 実績TBLのINSERT（H_SENDOUTEDI_BYKAGT）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_H_SENDOUTEDI_BYKAGT As String = "INSERT INTO       " & vbNewLine _
                                     & "$LM_TRN$..H_SENDOUTEDI_BYKAGT       " & vbNewLine _
                                     & "(                                " & vbNewLine _
                                     & " DEL_KB                          " & vbNewLine _
                                     & ",CRT_DATE                        " & vbNewLine _
                                     & ",FILE_NAME                       " & vbNewLine _
                                     & ",REC_NO                          " & vbNewLine _
                                     & ",GYO                             " & vbNewLine _
                                     & ",NRS_BR_CD                       " & vbNewLine _
                                     & ",EDI_CTL_NO                      " & vbNewLine _
                                     & ",EDI_CTL_NO_CHU                  " & vbNewLine _
                                     & ",OUTKA_CTL_NO                    " & vbNewLine _
                                     & ",OUTKA_CTL_NO_CHU                " & vbNewLine _
                                     & ",CUST_CD_L                       " & vbNewLine _
                                     & ",CUST_CD_M                       " & vbNewLine _
                                     & ",E1EDK01_ACTION                       " & vbNewLine _
                                     & ",E1EDK01_CURCY                        " & vbNewLine _
                                     & ",E1EDK01_LIFSK                        " & vbNewLine _
                                     & ",E1EDK02_QUALF_BELNR_002              " & vbNewLine _
                                     & ",E1EDK14_QUALF_ORGID_012              " & vbNewLine _
                                     & ",E1EDK14_QUALF_ORGID_008              " & vbNewLine _
                                     & ",E1EDK14_QUALF_ORGID_007              " & vbNewLine _
                                     & ",E1EDK14_QUALF_ORGID_006              " & vbNewLine _
                                     & ",E1EDKA1_PARVW_LIFNR_AG               " & vbNewLine _
                                     & ",E1EDKA1_PARVW_LIFNR_WE               " & vbNewLine _
                                     & ",E1EDKA1_PARVW_PARTN_ZZ               " & vbNewLine _
                                     & ",E1EDK03_IDDAT_DATUM_001              " & vbNewLine _
                                     & ",E1EDKT1_TDID                         " & vbNewLine _
                                     & ",E1EDKT1_TSSPRAS                      " & vbNewLine _
                                     & ",E1EDKT1_TDOBJECT                     " & vbNewLine _
                                     & ",E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1    " & vbNewLine _
                                     & ",E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2    " & vbNewLine _
                                     & ",E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3    " & vbNewLine _
                                     & ",E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4    " & vbNewLine _
                                     & ",E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5    " & vbNewLine _
                                     & ",E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6    " & vbNewLine _
                                     & ",E1EDK02_QUALF_BELNR_001              " & vbNewLine _
                                     & ",E1EDK02_QUALF_BELNR_043              " & vbNewLine _
                                     & ",E1EDP01_POSEX                        " & vbNewLine _
                                     & ",E1EDP01_MATNR                        " & vbNewLine _
                                     & ",E1EDP01_MENGE                        " & vbNewLine _
                                     & ",E1EDP01_MENEE                        " & vbNewLine _
                                     & ",E1EDP01_WERKS                        " & vbNewLine _
                                     & ",E1EDP01_VSTEL                        " & vbNewLine _
                                     & ",E1EDP01_LGORT                        " & vbNewLine _
                                     & ",E1EDP02_QUALF_BELNR_043              " & vbNewLine _
                                     & ",E1EDP02_QUALF_ZEILE_043              " & vbNewLine _
                                     & ",E1EDP02_QUALF_BELNR_044              " & vbNewLine _
                                     & ",E1EDP03_QUALF_DATE_010               " & vbNewLine _
                                     & ",E1EDP03_QUALF_DATE_024               " & vbNewLine _
                                     & ",E1EDP20_WMENG_EDATU                  " & vbNewLine _
                                     & ",E1EDP19_QUALF_IDTNR_002              " & vbNewLine _
                                     & ",E1EDP19_QUALF_IDTNR_010              " & vbNewLine _
                                     & ",E1EDKA1_PARVW_PARTN_DUMMY            " & vbNewLine _
                                     & ",E1EDKA1_NAME1                        " & vbNewLine _
                                     & ",E1EDKA1_NAME2                        " & vbNewLine _
                                     & ",E1EDKA1_NAME3                        " & vbNewLine _
                                     & ",E1EDKA1_NAME4                        " & vbNewLine _
                                     & ",E1EDKA1_STRAS                        " & vbNewLine _
                                     & ",E1EDKA1_STRS2                        " & vbNewLine _
                                     & ",E1EDKA1_ORT01                        " & vbNewLine _
                                     & ",E1EDKA1_PSTLZ                        " & vbNewLine _
                                     & ",E1EDKA1_LAND1                        " & vbNewLine _
                                     & ",E1EDKA1_TELF1                        " & vbNewLine _
                                     & ",SAMPLE_HOUKOKU_FLG                   " & vbNewLine _
                                     & ",RECORD_STATUS                   " & vbNewLine _
                                     & ",JISSEKI_SHORI_FLG               " & vbNewLine _
                                     & ",JISSEKI_USER                    " & vbNewLine _
                                     & ",JISSEKI_DATE                    " & vbNewLine _
                                     & ",JISSEKI_TIME                    " & vbNewLine _
                                     & ",SEND_USER                       " & vbNewLine _
                                     & ",SEND_DATE                       " & vbNewLine _
                                     & ",SEND_TIME                       " & vbNewLine _
                                     & ",SAMPLEHOUKOKU_SHORI_FLG         " & vbNewLine _
                                     & ",SAMPLEHOUKOKU_USER              " & vbNewLine _
                                     & ",SAMPLEHOUKOKU_DATE              " & vbNewLine _
                                     & ",SAMPLEHOUKOKU_TIME              " & vbNewLine _
                                     & ",DELETE_USER                     " & vbNewLine _
                                     & ",DELETE_DATE                     " & vbNewLine _
                                     & ",DELETE_TIME                     " & vbNewLine _
                                     & ",DELETE_EDI_NO                   " & vbNewLine _
                                     & ",DELETE_EDI_NO_CHU               " & vbNewLine _
                                     & ",UPD_USER                        " & vbNewLine _
                                     & ",UPD_DATE                        " & vbNewLine _
                                     & ",UPD_TIME                        " & vbNewLine _
                                     & ",SYS_ENT_DATE                    " & vbNewLine _
                                     & ",SYS_ENT_TIME                    " & vbNewLine _
                                     & ",SYS_ENT_PGID                    " & vbNewLine _
                                     & ",SYS_ENT_USER                    " & vbNewLine _
                                     & ",SYS_UPD_DATE                    " & vbNewLine _
                                     & ",SYS_UPD_TIME                    " & vbNewLine _
                                     & ",SYS_UPD_PGID                    " & vbNewLine _
                                     & ",SYS_UPD_USER                    " & vbNewLine _
                                     & ",SYS_DEL_FLG                     " & vbNewLine _
                                     & ")VALUES(                         " & vbNewLine _
                                     & " @DEL_KB                         " & vbNewLine _
                                     & ",@CRT_DATE                       " & vbNewLine _
                                     & ",@FILE_NAME                      " & vbNewLine _
                                     & ",@REC_NO                         " & vbNewLine _
                                     & ",@GYO                            " & vbNewLine _
                                     & ",@NRS_BR_CD                      " & vbNewLine _
                                     & ",@EDI_CTL_NO                     " & vbNewLine _
                                     & ",@EDI_CTL_NO_CHU                 " & vbNewLine _
                                     & ",@OUTKA_CTL_NO                   " & vbNewLine _
                                     & ",@OUTKA_CTL_NO_CHU               " & vbNewLine _
                                     & ",@CUST_CD_L                      " & vbNewLine _
                                     & ",@CUST_CD_M                      " & vbNewLine _
                                     & ",@E1EDK01_ACTION                       " & vbNewLine _
                                     & ",@E1EDK01_CURCY                        " & vbNewLine _
                                     & ",@E1EDK01_LIFSK                        " & vbNewLine _
                                     & ",@E1EDK02_QUALF_BELNR_002              " & vbNewLine _
                                     & ",@E1EDK14_QUALF_ORGID_012              " & vbNewLine _
                                     & ",@E1EDK14_QUALF_ORGID_008              " & vbNewLine _
                                     & ",@E1EDK14_QUALF_ORGID_007              " & vbNewLine _
                                     & ",@E1EDK14_QUALF_ORGID_006              " & vbNewLine _
                                     & ",@E1EDKA1_PARVW_LIFNR_AG               " & vbNewLine _
                                     & ",@E1EDKA1_PARVW_LIFNR_WE               " & vbNewLine _
                                     & ",@E1EDKA1_PARVW_PARTN_ZZ               " & vbNewLine _
                                     & ",@E1EDK03_IDDAT_DATUM_001              " & vbNewLine _
                                     & ",@E1EDKT1_TDID                         " & vbNewLine _
                                     & ",@E1EDKT1_TSSPRAS                      " & vbNewLine _
                                     & ",@E1EDKT1_TDOBJECT                     " & vbNewLine _
                                     & ",@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1    " & vbNewLine _
                                     & ",@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2    " & vbNewLine _
                                     & ",@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3    " & vbNewLine _
                                     & ",@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4    " & vbNewLine _
                                     & ",@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5    " & vbNewLine _
                                     & ",@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6    " & vbNewLine _
                                     & ",@E1EDK02_QUALF_BELNR_001              " & vbNewLine _
                                     & ",@E1EDK02_QUALF_BELNR_043              " & vbNewLine _
                                     & ",@E1EDP01_POSEX                        " & vbNewLine _
                                     & ",@E1EDP01_MATNR                        " & vbNewLine _
                                     & ",@E1EDP01_MENGE                        " & vbNewLine _
                                     & ",@E1EDP01_MENEE                        " & vbNewLine _
                                     & ",@E1EDP01_WERKS                        " & vbNewLine _
                                     & ",@E1EDP01_VSTEL                        " & vbNewLine _
                                     & ",@E1EDP01_LGORT                        " & vbNewLine _
                                     & ",@E1EDP02_QUALF_BELNR_043              " & vbNewLine _
                                     & ",@E1EDP02_QUALF_ZEILE_043              " & vbNewLine _
                                     & ",@E1EDP02_QUALF_BELNR_044              " & vbNewLine _
                                     & ",@E1EDP03_QUALF_DATE_010               " & vbNewLine _
                                     & ",@E1EDP03_QUALF_DATE_024               " & vbNewLine _
                                     & ",@E1EDP20_WMENG_EDATU                  " & vbNewLine _
                                     & ",@E1EDP19_QUALF_IDTNR_002              " & vbNewLine _
                                     & ",@E1EDP19_QUALF_IDTNR_010              " & vbNewLine _
                                     & ",@E1EDKA1_PARVW_PARTN_DUMMY            " & vbNewLine _
                                     & ",@E1EDKA1_NAME1                        " & vbNewLine _
                                     & ",@E1EDKA1_NAME2                        " & vbNewLine _
                                     & ",@E1EDKA1_NAME3                        " & vbNewLine _
                                     & ",@E1EDKA1_NAME4                        " & vbNewLine _
                                     & ",@E1EDKA1_STRAS                        " & vbNewLine _
                                     & ",@E1EDKA1_STRS2                        " & vbNewLine _
                                     & ",@E1EDKA1_ORT01                        " & vbNewLine _
                                     & ",@E1EDKA1_PSTLZ                        " & vbNewLine _
                                     & ",@E1EDKA1_LAND1                        " & vbNewLine _
                                     & ",@E1EDKA1_TELF1                        " & vbNewLine _
                                     & ",@SAMPLE_HOUKOKU_FLG                   " & vbNewLine _
                                     & ",@RECORD_STATUS                  " & vbNewLine _
                                     & ",@JISSEKI_SHORI_FLG              " & vbNewLine _
                                     & ",@JISSEKI_USER                   " & vbNewLine _
                                     & ",@JISSEKI_DATE                   " & vbNewLine _
                                     & ",@JISSEKI_TIME                   " & vbNewLine _
                                     & ",@SEND_USER                      " & vbNewLine _
                                     & ",@SEND_DATE                      " & vbNewLine _
                                     & ",@SEND_TIME                      " & vbNewLine _
                                     & ",@SAMPLEHOUKOKU_SHORI_FLG         " & vbNewLine _
                                     & ",@SAMPLEHOUKOKU_USER              " & vbNewLine _
                                     & ",@SAMPLEHOUKOKU_DATE              " & vbNewLine _
                                     & ",@SAMPLEHOUKOKU_TIME              " & vbNewLine _
                                     & ",@DELETE_USER                    " & vbNewLine _
                                     & ",@DELETE_DATE                    " & vbNewLine _
                                     & ",@DELETE_TIME                    " & vbNewLine _
                                     & ",@DELETE_EDI_NO                  " & vbNewLine _
                                     & ",@DELETE_EDI_NO_CHU              " & vbNewLine _
                                     & ",@UPD_USER                       " & vbNewLine _
                                     & ",@UPD_DATE                       " & vbNewLine _
                                     & ",@UPD_TIME                       " & vbNewLine _
                                     & ",@SYS_ENT_DATE                   " & vbNewLine _
                                     & ",@SYS_ENT_TIME                   " & vbNewLine _
                                     & ",@SYS_ENT_PGID                   " & vbNewLine _
                                     & ",@SYS_ENT_USER                   " & vbNewLine _
                                     & ",@SYS_UPD_DATE                   " & vbNewLine _
                                     & ",@SYS_UPD_TIME                   " & vbNewLine _
                                     & ",@SYS_UPD_PGID                   " & vbNewLine _
                                     & ",@SYS_UPD_USER                   " & vbNewLine _
                                     & ",@SYS_DEL_FLG                    " & vbNewLine _
                                     & ")                                " & vbNewLine



#End Region

#Region "C_OUTKA_L"
    ''' <summary>
    ''' C_OUTKA_LのUPDATE文（C_OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKISAKUSEI_OUTKA_L As String = "UPDATE $LM_TRN$..C_OUTKA_L SET       " & vbNewLine _
                                              & " OUTKA_STATE_KB    = '90'                            " & vbNewLine _
                                              & ",HOKOKU_DATE       = @SYS_UPD_DATE                   " & vbNewLine _
                                              & ",HOU_USER          = @SYS_UPD_USER                   " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                   " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                   " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                   " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                   " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                      " & vbNewLine _
                                              & "AND OUTKA_NO_L     = @OUTKA_CTL_NO                   " & vbNewLine _
                                              & "AND SYS_DEL_FLG     <> '1'                           " & vbNewLine
#End Region

#End Region

#Region "移動(入荷)報告作成 更新用SQL"

#Region "H_IDOEDI_DTL_BYK(BYK入荷報告作成)"

    ''' <summary>
    ''' H_IDOEDI_DTL_BYK(BYK入荷報告作成)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_IDO_DTL_BYK As String = "UPDATE $LM_TRN$..H_IDOEDI_DTL_BYK SET        " & vbNewLine _
                                              & " POSTING_DATE      = CASE WHEN RTRIM(POSTING_DATE) = ''  " & vbNewLine _
                                              & "                          THEN @SYS_UPD_DATE       " & vbNewLine _
                                              & "                          ELSE POSTING_DATE END    " & vbNewLine _
                                              & ",JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " CRT_DATE          = @CRT_DATE                     " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " FILE_NAME         = @FILE_NAME                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " REC_NO            = @REC_NO                       " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG  = '1'                          " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine

#End Region

#End Region

#Region "一括変更 更新用SQL"

#Region "H_IDOEDI_DTL_BYK(一括変更)"

    ''' <summary>
    ''' H_IDOEDI_DTL_BYK(一括変更)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_IKKATU_IDO_DTL_BYK As String = "UPDATE $LM_TRN$..H_IDOEDI_DTL_BYK SET  " & vbNewLine _
                                              & " POSTING_DATE      = CASE WHEN @POSTING_DATE <> '' AND @POSTING_DATE IS NOT NULL " & vbNewLine _
                                              & "                          THEN @POSTING_DATE       " & vbNewLine _
                                              & "                          ELSE POSTING_DATE  END   " & vbNewLine _
                                              & ",CUST_CD_L         = CASE WHEN @CUST_CD_L <> '' AND @CUST_CD_L IS NOT NULL " & vbNewLine _
                                              & "                          THEN @CUST_CD_L          " & vbNewLine _
                                              & "                          ELSE CUST_CD_L     END   " & vbNewLine _
                                              & ",CUST_CD_M         = CASE WHEN @CUST_CD_M <> '' AND @CUST_CD_M IS NOT NULL " & vbNewLine _
                                              & "                          THEN @CUST_CD_M          " & vbNewLine _
                                              & "                          ELSE CUST_CD_M     END   " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " CRT_DATE          = @CRT_DATE                     " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " FILE_NAME         = @FILE_NAME                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " REC_NO            = @REC_NO                       " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG  = '1'                          " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine

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

#Region "検索処理"

    ''' <summary>
    ''' BYK取込データ検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>BYK取込データ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI410IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI410DAC.SQL_SELECT_COUNT_SELECT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMI410DAC.SQL_SELECT_DATA_FROM)        'SQL構築(カウント用FROM句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' BYK取込データ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>BYK取込データ検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI410IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI410DAC.SQL_SELECT_DATA_SELECT)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMI410DAC.SQL_SELECT_DATA_FROM)        'SQL構築(データ抽出用FROM句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMI410DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("SAGYO_STATE_NM", "SAGYO_STATE_NM")
        map.Add("SYORI_SUB", "SYORI_SUB")
        map.Add("INOUTKA_DATE", "INOUTKA_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("PRINT_KBN_NM", "PRINT_KBN_NM")
        map.Add("SAGYO_NAIYO", "SAGYO_NAIYO")
        map.Add("TEXT_NM", "TEXT_NM")
        map.Add("CURRENT_MATERIAL", "CURRENT_MATERIAL")
        map.Add("CURRENT_DESCRIPTION", "CURRENT_DESCRIPTION")
        map.Add("CURRENT_GOODS_JOTAI", "CURRENT_GOODS_JOTAI")
        map.Add("CURRENT_BATCH", "CURRENT_BATCH")
        map.Add("CURRENT_QUANTITY", "CURRENT_QUANTITY")
        map.Add("CURRENT_STORAGE_LOCATION", "CURRENT_STORAGE_LOCATION")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DESTINATION_MATERIAL", "DESTINATION_MATERIAL")
        map.Add("DESTINATION_DESCRIPTION", "DESTINATION_DESCRIPTION")
        map.Add("DESTINATION_GOODS_JOTAI", "DESTINATION_GOODS_JOTAI")
        map.Add("DESTINATION_BATCH", "DESTINATION_BATCH")
        map.Add("DESTINATION_QUANTITY", "DESTINATION_QUANTITY")
        map.Add("DESTINATION_STORAGE_LOCATION", "DESTINATION_STORAGE_LOCATION")
        map.Add("REC_NO", "REC_NO")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG")
        map.Add("SYORI_KBN", "SYORI_KBN")
        map.Add("PRINT_FLG", "PRINT_FLG")
        map.Add("SAGYO_STATE_KBN", "SAGYO_STATE_KBN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI410OUT")

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
        Dim andstr As StringBuilder = New StringBuilder()

        With Me._Row

            '【営業所：=】
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HIDB.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '【荷主コード（大）：=】
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (HIDB.CUST_CD_L = @CUST_CD_L OR  RTRIM(HIDB.CUST_CD_L) = '') ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '【荷主コード（中）：=】
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (HIDB.CUST_CD_M = @CUST_CD_M OR  RTRIM(HIDB.CUST_CD_L) = '')")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '【作業コード：=】
            whereStr = .Item("USER_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HIDB.SYS_UPD_USER = @USER_CD ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", whereStr, DBDataType.NVARCHAR))
            End If

            '
            If String.IsNullOrEmpty(.Item("CMB_SEARCH").ToString()) = False Then

                Select Case .Item("CMB_SEARCH").ToString()

                    Case "01"

                        '【取込日(FROM)：<=】
                        whereStr = .Item("SEARCH_FROM").ToString()
                        If String.IsNullOrEmpty(whereStr) = False Then
                            If andstr.Length <> 0 Then
                                andstr.Append("AND")
                            End If
                            andstr.Append("  HIDB.CRT_DATE >= @SEARCH_FROM")
                            andstr.Append(vbNewLine)
                            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_FROM", whereStr, DBDataType.CHAR))
                        End If

                        '【取込日(TO)：>=】
                        whereStr = .Item("SEARCH_TO").ToString()
                        If String.IsNullOrEmpty(whereStr) = False Then
                            If andstr.Length <> 0 Then
                                andstr.Append("AND")
                            End If
                            andstr.Append("  HIDB.CRT_DATE <= @SEARCH_TO")
                            andstr.Append(vbNewLine)
                            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_TO", whereStr, DBDataType.CHAR))
                        End If

                    Case "02"

                        '【入出荷(振替)日(FROM)：<=】
                        whereStr = .Item("SEARCH_FROM").ToString()
                        If String.IsNullOrEmpty(whereStr) = False Then
                            If andstr.Length <> 0 Then
                                andstr.Append("AND")
                            End If
                            andstr.Append("  HIDB.POSTING_DATE >= @SEARCH_FROM")
                            andstr.Append(vbNewLine)
                            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_FROM", whereStr, DBDataType.CHAR))
                        End If

                        '【入出荷(振替)日(TO)：>=】
                        whereStr = .Item("SEARCH_TO").ToString()
                        If String.IsNullOrEmpty(whereStr) = False Then
                            If andstr.Length <> 0 Then
                                andstr.Append("AND")
                            End If
                            andstr.Append("  HIDB.POSTING_DATE <= @SEARCH_TO")
                            andstr.Append(vbNewLine)
                            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_TO", whereStr, DBDataType.CHAR))
                        End If


                End Select

            End If

            '【処理区分：=】
            whereStr = .Item("STATE_KBN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  KBN_B028.KBN_CD = @STATE_KBN")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STATE_KBN", whereStr, DBDataType.CHAR))
            End If

            '【処理区分：=】
            whereStr = .Item("SYORI_KBN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  KBN_B023.KBN_CD = @SYORI_KBN")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYORI_KBN", whereStr, DBDataType.CHAR))
            End If

            '【印刷区分：=】
            whereStr = .Item("PRINT_KBN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HIDB.PRINT_FLG = @PRINT_KBN")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_KBN", Right(whereStr, 1), DBDataType.CHAR))
            End If

            '【取込ファイル名：LIKE %値%】
            whereStr = .Item("FILE_NAME").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HIDB.FILE_NAME LIKE @FILE_NAME")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【備考：LIKE %値%】
            whereStr = .Item("TEXT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HIDB.TEXT LIKE @TEXT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEXT", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【（現在地）荷主商品コード：LIKE %値%】
            whereStr = .Item("CURRENT_MATERIAL").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HIDB.CURRENT_MATERIAL LIKE @CURRENT_MATERIAL")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CURRENT_MATERIAL", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【（現在地）商品名：LIKE %値%】
            whereStr = .Item("CURRENT_DESCRIPTION").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HIDB.CURRENT_DESCRIPTION LIKE @CURRENT_DESCRIPTION")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CURRENT_DESCRIPTION", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【（現在地）商品状態：=】
            whereStr = .Item("CURRENT_GOODS_JOTAI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  MCC_CURRENT.JOTAI_CD = @CURRENT_GOODS_JOTAI")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CURRENT_GOODS_JOTAI", whereStr, DBDataType.CHAR))
            End If

            '【（現在地）LOT№：LIKE 値%】
            whereStr = .Item("CURRENT_BATCH").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HIDB.CURRENT_BATCH LIKE @CURRENT_BATCH")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CURRENT_BATCH", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【（目的地）荷主商品コード：LIKE %値%】
            whereStr = .Item("DESTINATION_MATERIAL").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HIDB.DESTINATION_MATERIAL LIKE @DESTINATION_MATERIAL")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DESTINATION_MATERIAL", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【（目的地）商品名：LIKE %値%】
            whereStr = .Item("DESTINATION_DESCRIPTION").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HIDB.DESTINATION_DESCRIPTION LIKE @DESTINATION_DESCRIPTION")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DESTINATION_DESCRIPTION", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【（目的地）商品状態：=】
            whereStr = .Item("DESTINATION_GOODS_JOTAI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  MCC_DESTINATION.JOTAI_CD = @DESTINATION_GOODS_JOTAI")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DESTINATION_GOODS_JOTAI", whereStr, DBDataType.CHAR))
            End If

            '【（目的地）LOT№：LIKE 値%】
            whereStr = .Item("DESTINATION_BATCH").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HIDB.DESTINATION_BATCH LIKE @DESTINATION_BATCH")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DESTINATION_BATCH", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 出荷報告したレコードと対象のC_OUTKA_Mの数に差異が無いかを検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckSendOutBykAgtData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI410IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI410DAC.SQL_SELECT_COUNT_SENDOUTEDI_DATA)
        Call Me.setSQLSelect()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", "CheckSendOutBykAgtData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CNT", "CNT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI410CNT")

        Return ds
    End Function

#End Region

#Region "取込処理"

#Region "BYK商品コード⇒荷主コード割当(セミEDI)"

    ''' <summary>
    ''' BYK商品コード⇒荷主コード割当処理(LOTあり：在庫基準)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>BYK商品コード⇒荷主コード割当処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectCustGoodsZaiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("H_IDOEDI_DTL_BYK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI410DAC.SQL_SELECT_GOODS_CUST_DATA_ZAI)      'SQL構築(データ抽出用Select句)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetInsertIdoEdiRcvDtlParameter(Me._Row, Me._SqlPrmList)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", "SelectCustGoodsZaiData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "GOODS_CUST")

        Return ds

    End Function

    ''' <summary>
    ''' BYK商品コード⇒荷主コード割当処理(LOTなし：商品基準)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>BYK商品コード⇒荷主コード割当処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectCustGoodsData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("H_IDOEDI_DTL_BYK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI410DAC.SQL_SELECT_GOODS_CUST_DATA)      'SQL構築(データ抽出用Select句)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetInsertIdoEdiRcvDtlParameter(Me._Row, Me._SqlPrmList)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", "SelectCustGoodsData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "GOODS_CUST")

        Return ds

    End Function

#End Region

#Region "H_INKAEDI_DTL_BYK(セミEDI)"

    ''' <summary>
    ''' BYK移動EDI受信(DTL)テーブル新規登録
    ''' </summary>
    ''' <param name="setDs">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>BYK移動EDI受信(DTL)テーブル更新SQLの構築・発行</remarks>
    Private Function InsertIdoEdiData(ByVal setDs As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtIn As DataTable = setDs.Tables("H_IDOEDI_DTL_BYK")

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMI410DAC.SQL_INSERT_IDOEDI_DTL, setDs.Tables("H_IDOEDI_DTL_BYK").Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dtIn.Rows(0)

        'パラメータ設定
        Call Me.SetInsertIdoEdiRcvDtlParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", "InsertIdoEdiData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return setDs

    End Function

#End Region

#End Region

#Region "BYK入荷実績データ抽出処理"

    ''' <summary>
    ''' BYK入荷報告作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>BYK入荷報告作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectInkaHokoku(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI410IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI410DAC.SQL_SELECT_BYK_SENDIN_DATA)
        Me._StrSql.Append(LMI410DAC.SQL_FROM_BYK_SENDIN_DATA)
        Call setSQLSelect()                   '条件設定
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", "SelectInkaHokoku", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("INKA_CTL_NO_M", "INKA_CTL_NO_M")
        map.Add("INKA_CTL_NO_S", "INKA_CTL_NO_S")
        map.Add("OUTKA_CTL_NO_L", "OUTKA_CTL_NO_L")
        map.Add("OUTKA_CTL_NO_M", "OUTKA_CTL_NO_M")
        map.Add("OUTKA_CTL_NO_S", "OUTKA_CTL_NO_S")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("E1BP2017_GM_HEAD_01_PSTNG_DATE", "E1BP2017_GM_HEAD_01_PSTNG_DATE")
        map.Add("E1BP2017_GM_HEAD_01_DOC_DATE", "E1BP2017_GM_HEAD_01_DOC_DATE")
        map.Add("E1BP2017_GM_HEAD_01_HEADER_TXT", "E1BP2017_GM_HEAD_01_HEADER_TXT")
        map.Add("E1BP2017_GM_CODE_GM_CODE", "E1BP2017_GM_CODE_GM_CODE")
        map.Add("E1BP2017_GM_ITEM_CREATE_MATERIAL", "E1BP2017_GM_ITEM_CREATE_MATERIAL")
        map.Add("E1BP2017_GM_ITEM_CREATE_PLANT", "E1BP2017_GM_ITEM_CREATE_PLANT")
        map.Add("E1BP2017_GM_ITEM_CREATE_STGE_LOC", "E1BP2017_GM_ITEM_CREATE_STGE_LOC")
        map.Add("E1BP2017_GM_ITEM_CREATE_BATCH", "E1BP2017_GM_ITEM_CREATE_BATCH")
        map.Add("E1BP2017_GM_ITEM_CREATE_MOVE_TYPE", "E1BP2017_GM_ITEM_CREATE_MOVE_TYPE")
        map.Add("E1BP2017_GM_ITEM_CREATE_ENTRY_QNT", "E1BP2017_GM_ITEM_CREATE_ENTRY_QNT")
        map.Add("E1BP2017_GM_ITEM_CREATE_PO_NUMBER", "E1BP2017_GM_ITEM_CREATE_PO_NUMBER")
        map.Add("E1BP2017_GM_ITEM_CREATE_PO_ITEM", "E1BP2017_GM_ITEM_CREATE_PO_ITEM")
        map.Add("E1BP2017_GM_ITEM_CREATE_MVT_IND", "E1BP2017_GM_ITEM_CREATE_MVT_IND")
        map.Add("E1BP2017_GM_ITEM_CREATE_DELIV_NUMB_TO_SEARCH", "E1BP2017_GM_ITEM_CREATE_DELIV_NUMB_TO_SEARCH")
        map.Add("E1BP2017_GM_ITEM_CREATE_DELIV_ITEM_TO_SEARCH", "E1BP2017_GM_ITEM_CREATE_DELIV_ITEM_TO_SEARCH")
        map.Add("E1BP2017_GM_ITEM_CREATE_ENTRY_UOM_ISO", "E1BP2017_GM_ITEM_CREATE_ENTRY_UOM_ISO")
        map.Add("E1BP2017_GM_ITEM_CREATE_MOVE_MAT", "E1BP2017_GM_ITEM_CREATE_MOVE_MAT")
        map.Add("E1BP2017_GM_ITEM_CREATE_MOVE_PLANT", "E1BP2017_GM_ITEM_CREATE_MOVE_PLANT")
        map.Add("E1BP2017_GM_ITEM_CREATE_MOVE_STLOC", "E1BP2017_GM_ITEM_CREATE_MOVE_STLOC")
        map.Add("E1BP2017_GM_ITEM_CREATE_MOVE_BATCH", "E1BP2017_GM_ITEM_CREATE_MOVE_BATCH")
        map.Add("SAMPLE_HOUKOKU_FLG", "SAMPLE_HOUKOKU_FLG")
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "H_SENDINOUTEDI_BYK")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("H_SENDINOUTEDI_BYK").Rows.Count())
        reader.Close()

        Return ds

    End Function

#Region "H_INKAEDI_L"

    ''' <summary>
    ''' EDI入荷(大)データ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaEdiLData(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("H_SENDINOUTEDI_BYK")
        Dim nrsBrCd As String = dt.Rows(0)("NRS_BR_CD").ToString
        Dim dtRow As DataRow

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Dim setSql As String = LMI410DAC.SQL_UPD_JISSEKISAKUSEI_EDI_L

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, nrsBrCd))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        dtRow = dt.Rows(0)

        'SQLパラメータ設定
        Call Me.SetParamCommonSystemUp()
        Call Me.SetUpdPrmEdi(dtRow)
        Call Me.SetJissekiParameterRcv()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

#Region "H_INKAEDI_M"

    ''' <summary>
    ''' EDI入荷(中)データ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaEdiMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("H_SENDINOUTEDI_BYK")
        Dim rtn As Integer = 0
        Dim nrsBrCd As String = dt.Rows(0)("NRS_BR_CD").ToString

        Dim setSql As String = LMI410DAC.SQL_UPD_JISSEKISAKUSEI_EDI_M


        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, nrsBrCd))
        Dim dtRow As DataRow
        Dim max As Integer = dt.Rows.Count() - 1

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        dtRow = dt.Rows(0)

        'パラメータ設定
        Call Me.SetParamCommonSystemUp()
        Call Me.SetUpdPrmEdi(dtRow)
        Call Me.SetJissekiParameterRcv()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return ds

    End Function

#End Region

#Region "H_INKAEDI_DTL_BYK"

    ''' <summary>
    ''' BYK入荷EDI受信データ(明細)更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaEdiDtlBykData(ByVal ds As DataSet) As DataSet


        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("H_SENDINOUTEDI_BYK")
        Dim rtn As Integer = 0

        Dim setSql As String = LMI410DAC.SQL_UPDATE_RCV_DTL_BYK

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, dt.Rows(0).Item("NRS_BR_CD").ToString()))
        Dim dtRow As DataRow
        Dim max As Integer = dt.Rows.Count() - 1

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        dtRow = dt.Rows(0)

        'パラメータ設定
        Call Me.SetParamCommonSystemUp()
        Call Me.SetUpdPrmEdi(dtRow)
        Call Me.SetJissekiParameterRcv()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return ds

    End Function

#End Region

#Region "B_INKA_L"

    ''' <summary>
    ''' 入荷Lテーブル更新（入荷報告作成時ステージUP）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("H_SENDINOUTEDI_BYK").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me._Row("INKA_CTL_NO_L").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMI410DAC.SQL_UPDATE_JISSEKISAKUSEI_INKA_L, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", "UpdateInkaData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return ds

    End Function

#End Region

#End Region

#Region "BYK出荷実績データ抽出処理"

    ''' <summary>
    ''' 出荷報告作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷報告作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectOutkaHokoku(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI410IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI410DAC.SQL_SELECT_BYKAGT_SEND_DATA)
        Me._StrSql.Append(LMI410DAC.SQL_FROM_BYKAGT_SEND_DATA)
        Call setSQLSelect()                   '条件設定
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", "SelectOutkaHokoku", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_CTL_NO_CHU", "OUTKA_CTL_NO_CHU")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("E1EDK01_ACTION", "E1EDK01_ACTION")
        map.Add("E1EDK01_CURCY", "E1EDK01_CURCY")
        map.Add("E1EDK01_LIFSK", "E1EDK01_LIFSK")
        map.Add("E1EDK02_QUALF_BELNR_002", "E1EDK02_QUALF_BELNR_002")
        map.Add("E1EDK14_QUALF_ORGID_012", "E1EDK14_QUALF_ORGID_012")
        map.Add("E1EDK14_QUALF_ORGID_008", "E1EDK14_QUALF_ORGID_008")
        map.Add("E1EDK14_QUALF_ORGID_007", "E1EDK14_QUALF_ORGID_007")
        map.Add("E1EDK14_QUALF_ORGID_006", "E1EDK14_QUALF_ORGID_006")
        map.Add("E1EDKA1_PARVW_LIFNR_AG", "E1EDKA1_PARVW_LIFNR_AG")
        map.Add("E1EDKA1_PARVW_LIFNR_WE", "E1EDKA1_PARVW_LIFNR_WE")
        map.Add("E1EDKA1_PARVW_PARTN_ZZ", "E1EDKA1_PARVW_PARTN_ZZ")
        map.Add("E1EDK03_IDDAT_DATUM_001", "E1EDK03_IDDAT_DATUM_001")
        map.Add("E1EDKT1_TDID", "E1EDKT1_TDID")
        map.Add("E1EDKT1_TSSPRAS", "E1EDKT1_TSSPRAS")
        map.Add("E1EDKT1_TDOBJECT", "E1EDKT1_TDOBJECT")
        map.Add("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1", "E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1")
        map.Add("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2", "E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2")
        map.Add("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3", "E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3")
        map.Add("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4", "E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4")
        map.Add("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5", "E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5")
        map.Add("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6", "E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6")
        map.Add("E1EDK02_QUALF_BELNR_001", "E1EDK02_QUALF_BELNR_001")
        map.Add("E1EDK02_QUALF_BELNR_043", "E1EDK02_QUALF_BELNR_043")
        map.Add("E1EDP01_POSEX", "E1EDP01_POSEX")
        map.Add("E1EDP01_MATNR", "E1EDP01_MATNR")
        map.Add("E1EDP01_MENGE", "E1EDP01_MENGE")
        map.Add("E1EDP01_MENEE", "E1EDP01_MENEE")
        map.Add("E1EDP01_WERKS", "E1EDP01_WERKS")
        map.Add("E1EDP01_VSTEL", "E1EDP01_VSTEL")
        map.Add("E1EDP01_LGORT", "E1EDP01_LGORT")
        map.Add("E1EDP02_QUALF_BELNR_043", "E1EDP02_QUALF_BELNR_043")
        map.Add("E1EDP02_QUALF_ZEILE_043", "E1EDP02_QUALF_ZEILE_043")
        map.Add("E1EDP02_QUALF_BELNR_044", "E1EDP02_QUALF_BELNR_044")
        map.Add("E1EDP03_QUALF_DATE_010", "E1EDP03_QUALF_DATE_010")
        map.Add("E1EDP03_QUALF_DATE_024", "E1EDP03_QUALF_DATE_024")
        map.Add("E1EDP20_WMENG_EDATU", "E1EDP20_WMENG_EDATU")
        map.Add("E1EDP19_QUALF_IDTNR_002", "E1EDP19_QUALF_IDTNR_002")
        map.Add("E1EDP19_QUALF_IDTNR_010", "E1EDP19_QUALF_IDTNR_010")
        '要望番号2091 追加START 2013.10.24
        map.Add("E1EDKA1_PARVW_PARTN_DUMMY", "E1EDKA1_PARVW_PARTN_DUMMY")
        map.Add("E1EDKA1_NAME1", "E1EDKA1_NAME1")
        map.Add("E1EDKA1_NAME2", "E1EDKA1_NAME2")
        map.Add("E1EDKA1_NAME3", "E1EDKA1_NAME3")
        map.Add("E1EDKA1_NAME4", "E1EDKA1_NAME4")
        map.Add("E1EDKA1_STRAS", "E1EDKA1_STRAS")
        map.Add("E1EDKA1_STRS2", "E1EDKA1_STRS2")
        map.Add("E1EDKA1_ORT01", "E1EDKA1_ORT01")
        map.Add("E1EDKA1_PSTLZ", "E1EDKA1_PSTLZ")
        map.Add("E1EDKA1_LAND1", "E1EDKA1_LAND1")
        map.Add("E1EDKA1_TELF1", "E1EDKA1_TELF1")
        map.Add("SAMPLE_HOUKOKU_FLG", "SAMPLE_HOUKOKU_FLG")
        '要望番号2091 追加END 2013.10.24
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG")

        map.Add("SAMPLEHOUKOKU_SHORI_FLG", "SAMPLEHOUKOKU_SHORI_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "H_SENDOUTEDI_BYK")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("H_SENDOUTEDI_BYK").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷Lテーブル更新（出荷報告作成時ステージUP）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("H_SENDOUTEDI_BYK").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me._Row("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMI410DAC.SQL_UPDATE_JISSEKISAKUSEI_OUTKA_L, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", "UpdateOutkaData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "BYK移動(入荷)実績データ抽出処理"

    ''' <summary>
    ''' BYK移動(入荷)報告作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>BYK移動(入荷)報告作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectIdoHokoku(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI410IN_IDO_HOKOKU")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI410DAC.SQL_SELECT_BYK_SENDIDO_DATA)
        Me._StrSql.Append(LMI410DAC.SQL_FROM_BYK_SENDIDO_DATA)
        Call setSQLIdoSelect()                   '条件設定
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", "SelectIdoHokoku", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("INKA_CTL_NO_M", "INKA_CTL_NO_M")
        map.Add("INKA_CTL_NO_S", "INKA_CTL_NO_S")
        map.Add("OUTKA_CTL_NO_L", "OUTKA_CTL_NO_L")
        map.Add("OUTKA_CTL_NO_M", "OUTKA_CTL_NO_M")
        map.Add("OUTKA_CTL_NO_S", "OUTKA_CTL_NO_S")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("E1BP2017_GM_HEAD_01_PSTNG_DATE", "E1BP2017_GM_HEAD_01_PSTNG_DATE")
        map.Add("E1BP2017_GM_HEAD_01_DOC_DATE", "E1BP2017_GM_HEAD_01_DOC_DATE")
        map.Add("E1BP2017_GM_HEAD_01_HEADER_TXT", "E1BP2017_GM_HEAD_01_HEADER_TXT")
        map.Add("E1BP2017_GM_CODE_GM_CODE", "E1BP2017_GM_CODE_GM_CODE")
        map.Add("E1BP2017_GM_ITEM_CREATE_MATERIAL", "E1BP2017_GM_ITEM_CREATE_MATERIAL")
        map.Add("E1BP2017_GM_ITEM_CREATE_PLANT", "E1BP2017_GM_ITEM_CREATE_PLANT")
        map.Add("E1BP2017_GM_ITEM_CREATE_STGE_LOC", "E1BP2017_GM_ITEM_CREATE_STGE_LOC")
        map.Add("E1BP2017_GM_ITEM_CREATE_BATCH", "E1BP2017_GM_ITEM_CREATE_BATCH")
        map.Add("E1BP2017_GM_ITEM_CREATE_MOVE_TYPE", "E1BP2017_GM_ITEM_CREATE_MOVE_TYPE")
        map.Add("E1BP2017_GM_ITEM_CREATE_ENTRY_QNT", "E1BP2017_GM_ITEM_CREATE_ENTRY_QNT")
        map.Add("E1BP2017_GM_ITEM_CREATE_PO_NUMBER", "E1BP2017_GM_ITEM_CREATE_PO_NUMBER")
        map.Add("E1BP2017_GM_ITEM_CREATE_PO_ITEM", "E1BP2017_GM_ITEM_CREATE_PO_ITEM")
        map.Add("E1BP2017_GM_ITEM_CREATE_MVT_IND", "E1BP2017_GM_ITEM_CREATE_MVT_IND")
        map.Add("E1BP2017_GM_ITEM_CREATE_DELIV_NUMB_TO_SEARCH", "E1BP2017_GM_ITEM_CREATE_DELIV_NUMB_TO_SEARCH")
        map.Add("E1BP2017_GM_ITEM_CREATE_DELIV_ITEM_TO_SEARCH", "E1BP2017_GM_ITEM_CREATE_DELIV_ITEM_TO_SEARCH")
        map.Add("E1BP2017_GM_ITEM_CREATE_ENTRY_UOM_ISO", "E1BP2017_GM_ITEM_CREATE_ENTRY_UOM_ISO")
        map.Add("E1BP2017_GM_ITEM_CREATE_MOVE_MAT", "E1BP2017_GM_ITEM_CREATE_MOVE_MAT")
        map.Add("E1BP2017_GM_ITEM_CREATE_MOVE_PLANT", "E1BP2017_GM_ITEM_CREATE_MOVE_PLANT")
        map.Add("E1BP2017_GM_ITEM_CREATE_MOVE_STLOC", "E1BP2017_GM_ITEM_CREATE_MOVE_STLOC")
        map.Add("E1BP2017_GM_ITEM_CREATE_MOVE_BATCH", "E1BP2017_GM_ITEM_CREATE_MOVE_BATCH")
        map.Add("SAMPLE_HOUKOKU_FLG", "SAMPLE_HOUKOKU_FLG")
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "H_SENDINOUTEDI_BYK")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("H_SENDINOUTEDI_BYK").Rows.Count())
        reader.Close()

        Return ds

    End Function


#Region "H_IDOEDI_DTL_BYK"

    ''' <summary>
    ''' BYK入荷EDI受信データ(明細)更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateIdoEdiDtlBykData(ByVal ds As DataSet) As DataSet


        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("H_SENDINOUTEDI_BYK")
        Dim rtn As Integer = 0

        Dim setSql As String = LMI410DAC.SQL_UPDATE_IDO_DTL_BYK

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, dt.Rows(0).Item("NRS_BR_CD").ToString()))
        'Dim dtRow As DataRow
        Dim max As Integer = dt.Rows.Count() - 1

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ''条件rowの格納
        'dtRow = dt.Rows(0)

        '条件rowの格納
        Me._Row = dt.Rows(0)

        'パラメータ設定
        Call Me.SetParamCommonSystemUp()
        Call Me.setSQLIdoSelect()
        Call Me.SetJissekiParameterRcv()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", "2", DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return ds

    End Function

#End Region

#End Region

#Region "一括変更処理"

#Region "H_IDOEDI_DTL_BYK"

    ''' <summary>
    ''' BYK入荷EDI受信データ(明細)更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateIkkatuIdoData(ByVal ds As DataSet) As DataSet


        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMI410IN_IKKATU_HENKO")
        Dim rtn As Integer = 0

        Dim setSql As String = LMI410DAC.SQL_UPDATE_IKKATU_IDO_DTL_BYK

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, dt.Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = dt.Rows.Count() - 1

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dt.Rows(0)

        'パラメータ設定
        Call Me.SetParamCommonSystemUp()
        Call Me.setSQLIkkatuSelect()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI410DAC", Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return ds

    End Function

#End Region

#End Region

#Region "H_SENDOUTEDI_BYKAGT"

    ''' <summary>
    ''' BYK(代理店用)EDI実績テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>BYK(代理店用)EDI実績テーブル更新SQLの構築・発行</remarks>
    Private Function InsertSendOutBykAgtData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dowSendTbl As DataTable = ds.Tables("H_SENDOUTEDI_BYK")

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI410DAC.SQL_INSERT_H_SENDOUTEDI_BYKAGT _
                                                                       , ds.Tables("H_SENDOUTEDI_BYK").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = dowSendTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = dowSendTbl.Rows(i)

            'パラメータ設定
            Call Me.SetParamCommonSystemIns()
            Call Me.SetEdiSendAgtCreateParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI410DAC", "InsertSendOutBykAgtData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#Region "EDI送信(TBL)新規登録パラメータ設定"

    ''' <summary>
    ''' EDI送信(TBL)の新規登録パラメータ設定(BYK(テツタニ(代理店)))
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetEdiSendAgtCreateParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
            Dim updTimeNormal As String = MyBase.GetSystemTime().Substring(0, 6)

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me.NullConvertString(.Item("CRT_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me.NullConvertString(.Item("FILE_NAME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", Me.NullConvertString(.Item("REC_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", Me.NullConvertString(.Item("GYO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(.Item("EDI_CTL_NO_CHU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_CHU", Me.NullConvertString(.Item("OUTKA_CTL_NO_CHU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK01_ACTION", Me.NullConvertString(.Item("E1EDK01_ACTION")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK01_CURCY", Me.NullConvertString(.Item("E1EDK01_CURCY")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK01_LIFSK", Me.NullConvertString(.Item("E1EDK01_LIFSK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK02_QUALF_BELNR_002", Me.NullConvertString(.Item("E1EDK02_QUALF_BELNR_002")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK14_QUALF_ORGID_012", Me.NullConvertString(.Item("E1EDK14_QUALF_ORGID_012")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK14_QUALF_ORGID_008", Me.NullConvertString(.Item("E1EDK14_QUALF_ORGID_008")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK14_QUALF_ORGID_007", Me.NullConvertString(.Item("E1EDK14_QUALF_ORGID_007")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK14_QUALF_ORGID_006", Me.NullConvertString(.Item("E1EDK14_QUALF_ORGID_006")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_PARVW_LIFNR_AG", Me.NullConvertString(.Item("E1EDKA1_PARVW_LIFNR_AG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_PARVW_LIFNR_WE", Me.NullConvertString(.Item("E1EDKA1_PARVW_LIFNR_WE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_PARVW_PARTN_ZZ", Me.NullConvertString(.Item("E1EDKA1_PARVW_PARTN_ZZ")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK03_IDDAT_DATUM_001", Me.NullConvertString(.Item("E1EDK03_IDDAT_DATUM_001")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT1_TDID", Me.NullConvertString(.Item("E1EDKT1_TDID")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT1_TSSPRAS", Me.NullConvertString(.Item("E1EDKT1_TSSPRAS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT1_TDOBJECT", Me.NullConvertString(.Item("E1EDKT1_TDOBJECT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1", Me.NullConvertString(.Item("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2", Me.NullConvertString(.Item("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3", Me.NullConvertString(.Item("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4", Me.NullConvertString(.Item("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5", Me.NullConvertString(.Item("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6", Me.NullConvertString(.Item("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK02_QUALF_BELNR_001", Me.NullConvertString(.Item("E1EDK02_QUALF_BELNR_001")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK02_QUALF_BELNR_043", Me.NullConvertString(.Item("E1EDK02_QUALF_BELNR_043")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_POSEX", Me.NullConvertString(.Item("E1EDP01_POSEX")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_MATNR", Me.NullConvertString(.Item("E1EDP01_MATNR")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_MENGE", Me.NullConvertString(.Item("E1EDP01_MENGE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_MENEE", Me.NullConvertString(.Item("E1EDP01_MENEE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_WERKS", Me.NullConvertString(.Item("E1EDP01_WERKS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_VSTEL", Me.NullConvertString(.Item("E1EDP01_VSTEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_LGORT", Me.NullConvertString(.Item("E1EDP01_LGORT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP02_QUALF_BELNR_043", Me.NullConvertString(.Item("E1EDP02_QUALF_BELNR_043")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP02_QUALF_ZEILE_043", Me.NullConvertString(.Item("E1EDP02_QUALF_ZEILE_043")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP02_QUALF_BELNR_044", Me.NullConvertString(.Item("E1EDP02_QUALF_BELNR_044")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP03_QUALF_DATE_010", Me.NullConvertString(.Item("E1EDP03_QUALF_DATE_010")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP03_QUALF_DATE_024", Me.NullConvertString(.Item("E1EDP03_QUALF_DATE_024")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP20_WMENG_EDATU", Me.NullConvertString(.Item("E1EDP20_WMENG_EDATU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP19_QUALF_IDTNR_002", Me.NullConvertString(.Item("E1EDP19_QUALF_IDTNR_002")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP19_QUALF_IDTNR_010", Me.NullConvertString(.Item("E1EDP19_QUALF_IDTNR_010")), DBDataType.NVARCHAR))
            '要望番号2091 追加START 2013.10.24
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_PARVW_PARTN_DUMMY", Me.NullConvertString(.Item("E1EDKA1_PARVW_PARTN_DUMMY")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_NAME1", Me.NullConvertString(.Item("E1EDKA1_NAME1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_NAME2", Me.NullConvertString(.Item("E1EDKA1_NAME2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_NAME3", Me.NullConvertString(.Item("E1EDKA1_NAME3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_NAME4", Me.NullConvertString(.Item("E1EDKA1_NAME4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_STRAS", Me.NullConvertString(.Item("E1EDKA1_STRAS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_STRS2", Me.NullConvertString(.Item("E1EDKA1_STRS2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_ORT01", Me.NullConvertString(.Item("E1EDKA1_ORT01")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_PSTLZ", Me.NullConvertString(.Item("E1EDKA1_PSTLZ")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_LAND1", Me.NullConvertString(.Item("E1EDKA1_LAND1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_TELF1", Me.NullConvertString(.Item("E1EDKA1_TELF1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAMPLE_HOUKOKU_FLG", Me.NullConvertString(.Item("SAMPLE_HOUKOKU_FLG")), DBDataType.NVARCHAR))
            '要望番号2091 追加END 2013.10.24
            prmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", Me.NullConvertString(.Item("RECORD_STATUS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(.Item("JISSEKI_SHORI_FLG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_USER", Me.NullConvertString(.Item("SEND_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_DATE", Me.NullConvertString(.Item("SEND_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_TIME", Me.NullConvertString(.Item("SEND_TIME")), DBDataType.NVARCHAR))
            '要望番号2091 追加START 2013.10.24
            prmList.Add(MyBase.GetSqlParameter("@SAMPLEHOUKOKU_SHORI_FLG", Me.NullConvertString(.Item("SAMPLEHOUKOKU_SHORI_FLG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAMPLEHOUKOKU_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAMPLEHOUKOKU_DATE", MyBase.GetSystemDate(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAMPLEHOUKOKU_TIME", updTime, DBDataType.NVARCHAR))
            '要望番号2091 追加END 2013.10.24
            prmList.Add(MyBase.GetSqlParameter("@DELETE_USER", Me.NullConvertString(.Item("DELETE_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", Me.NullConvertString(.Item("DELETE_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", Me.NullConvertString(.Item("DELETE_TIME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", Me.NullConvertString(.Item("DELETE_EDI_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO_CHU", Me.NullConvertString(.Item("DELETE_EDI_NO_CHU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", Me.NullConvertString(.Item("UPD_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", Me.NullConvertString(.Item("UPD_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.NullConvertString(.Item("UPD_TIME")), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#End Region

#Region "H_SENDINOUTEDI_BYK"

    ''' <summary>
    ''' BYK　EDI入荷実績データテーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>BYK入荷EDI実績テーブル更新SQLの構築・発行</remarks>
    Private Function InsertSendInOutBykData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dowSendTbl As DataTable = ds.Tables("H_SENDINOUTEDI_BYK")

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI410DAC.SQL_INSERT_H_SENDINOUTEDI_BYK _
                                                                       , ds.Tables("H_SENDINOUTEDI_BYK").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = dowSendTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = dowSendTbl.Rows(i)

            'パラメータ設定
            Call Me.SetParamCommonSystemIns()
            Call Me.SetEdiSendInCreateParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI410DAC", "InsertSendInOutBykData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#Region "BYK　EDI入荷実績データ新規登録パラメータ設定"

    ''' <summary>
    ''' BYK　EDI入荷実績データの新規登録パラメータ設定(BYK)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetEdiSendInCreateParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
            Dim updTimeNormal As String = MyBase.GetSystemTime().Substring(0, 6)

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me.NullConvertString(.Item("CRT_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me.NullConvertString(.Item("FILE_NAME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", Me.NullConvertString(.Item("REC_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", Me.NullConvertString(.Item("GYO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(.Item("EDI_CTL_NO_CHU")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(.Item("INKA_CTL_NO_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_M", Me.NullConvertString(.Item("INKA_CTL_NO_M")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_S", Me.NullConvertString(.Item("INKA_CTL_NO_S")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_L", Me.NullConvertString(.Item("OUTKA_CTL_NO_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_M", Me.NullConvertString(.Item("OUTKA_CTL_NO_M")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_S", Me.NullConvertString(.Item("OUTKA_CTL_NO_S")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_HEAD_01_PSTNG_DATE", Me.NullConvertString(.Item("E1BP2017_GM_HEAD_01_PSTNG_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_HEAD_01_DOC_DATE", Me.NullConvertString(.Item("E1BP2017_GM_HEAD_01_DOC_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_HEAD_01_HEADER_TXT", Me.NullConvertString(.Item("E1BP2017_GM_HEAD_01_HEADER_TXT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_CODE_GM_CODE", Me.NullConvertString(.Item("E1BP2017_GM_CODE_GM_CODE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_MATERIAL", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_MATERIAL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_PLANT", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_PLANT")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_STGE_LOC", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_STGE_LOC")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_BATCH", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_BATCH")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_MOVE_TYPE", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_MOVE_TYPE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_ENTRY_QNT", Me.NullConvertZero(.Item("E1BP2017_GM_ITEM_CREATE_ENTRY_QNT")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_PO_NUMBER", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_PO_NUMBER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_PO_ITEM", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_PO_ITEM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_MVT_IND", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_MVT_IND")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_DELIV_NUMB_TO_SEARCH", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_DELIV_NUMB_TO_SEARCH")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_DELIV_ITEM_TO_SEARCH", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_DELIV_ITEM_TO_SEARCH")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_ENTRY_UOM_ISO", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_ENTRY_UOM_ISO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_MOVE_MAT", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_MOVE_MAT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_MOVE_PLANT", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_MOVE_PLANT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_MOVE_STLOC", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_MOVE_STLOC")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1BP2017_GM_ITEM_CREATE_MOVE_BATCH", Me.NullConvertString(.Item("E1BP2017_GM_ITEM_CREATE_MOVE_BATCH")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAMPLE_HOUKOKU_FLG", Me.NullConvertString(.Item("SAMPLE_HOUKOKU_FLG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", Me.NullConvertString(.Item("RECORD_STATUS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(.Item("JISSEKI_SHORI_FLG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_USER", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_TIME", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAMPLEHOUKOKU_SHORI_FLG", Me.NullConvertString(.Item("SAMPLEHOUKOKU_SHORI_FLG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAMPLEHOUKOKU_USER", Me.NullConvertString(.Item("SAMPLEHOUKOKU_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAMPLEHOUKOKU_DATE", Me.NullConvertString(.Item("SAMPLEHOUKOKU_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAMPLEHOUKOKU_TIME", Me.NullConvertString(.Item("SAMPLEHOUKOKU_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_USER", Me.NullConvertString(.Item("DELETE_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", Me.NullConvertString(.Item("DELETE_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", Me.NullConvertString(.Item("DELETE_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", Me.NullConvertString(.Item("DELETE_EDI_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO_CHU", Me.NullConvertString(.Item("DELETE_EDI_NO_CHU")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", Me.NullConvertString(.Item("UPD_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", Me.NullConvertString(.Item("UPD_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.NullConvertString(.Item("UPD_TIME")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "更新パラメータ設定(H_INKAEDI_DTL_BYK)"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmEdi(ByVal row As DataRow)

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(row.Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(row.Item("EDI_CTL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(row.Item("EDI_CTL_NO_CHU")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.GetColonEditTime(MyBase.GetSystemTime()), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", "2", DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", LMConst.FLG.ON, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", String.Empty, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", String.Empty, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 更新時のパラメータ実績日時(Rcv)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJissekiParameterRcv()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.GetColonEditTime(MyBase.GetSystemTime()), DBDataType.CHAR))

    End Sub

#End Region

#End Region

#Region "共通項目"

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUp()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(新規時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetParamCommonSystemUp()

    End Sub

    ''' <summary>
    '''  パラメータ設定モジュール（BYK入荷・出荷報告）
    ''' </summary>
    ''' <remarks>BYK実績報告検索用SQLの構築</remarks>
    Private Sub setSQLSelect()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_FROM", Me._Row("SEARCH_FROM"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row("CUST_CD_M"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定モジュール（BYK移動報告作成）
    ''' </summary>
    ''' <remarks>倉庫間転送移動データ検索用SQLの構築</remarks>
    Private Sub setSQLIdoSelect()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me._Row("CRT_DATE"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me._Row("FILE_NAME"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", Me._Row("REC_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_SYS_UPD_DATE", Me._Row("HAITA_SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_SYS_UPD_TIME", Me._Row("HAITA_SYS_UPD_TIME"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定モジュール（一括変更）
    ''' </summary>
    ''' <remarks>倉庫間転送移動データ検索用SQLの構築</remarks>
    Private Sub setSQLIkkatuSelect()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me._Row("CRT_DATE"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me._Row("FILE_NAME"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", Me._Row("REC_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row("CUST_CD_M"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@POSTING_DATE", Me._Row("POSTING_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_SYS_UPD_DATE", Me._Row("HAITA_SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_SYS_UPD_TIME", Me._Row("HAITA_SYS_UPD_TIME"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaita()

        With Me._Row
            '排他共通項目
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub


#Region "EDI受信(DTL)新規追加パラメータ設定"

    ''' <summary>
    ''' BYK移動EDI受信(DTL)新規追加パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInsertIdoEdiRcvDtlParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            'EDI受信（DTL）共通項目
            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me.NullConvertString(.Item("CRT_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me.NullConvertString(.Item("FILE_NAME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", Me.NullConvertString(.Item("REC_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", Me.NullConvertString(.Item("GYO")), DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me.NullConvertString(.Item("INKA_NO_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", Me.NullConvertString(.Item("INKA_NO_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", Me.NullConvertString(.Item("INKA_NO_S")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me.NullConvertString(.Item("OUTKA_NO_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me.NullConvertString(.Item("OUTKA_NO_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", Me.NullConvertString(.Item("OUTKA_NO_S")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.CHAR))

            'EDI受信（DTL）荷主個別項目
            prmList.Add(MyBase.GetSqlParameter("@MOVE_TYPE", Me.NullConvertString(.Item("MOVE_TYPE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ITEM_NO", Me.NullConvertString(.Item("ITEM_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@POSTING_DATE", Me.NullConvertString(.Item("POSTING_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TEXT", Me.NullConvertString(.Item("TEXT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CURRENT_MATERIAL", Me.NullConvertString(.Item("CURRENT_MATERIAL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CURRENT_DESCRIPTION", Me.NullConvertString(.Item("CURRENT_DESCRIPTION")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CURRENT_PLANT", Me.NullConvertString(.Item("CURRENT_PLANT")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CURRENT_STORAGE_LOCATION", Me.NullConvertString(.Item("CURRENT_STORAGE_LOCATION")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CURRENT_BATCH", Me.NullConvertString(.Item("CURRENT_BATCH")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CURRENT_QUANTITY", Me.NullConvertString(.Item("CURRENT_QUANTITY")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@CURRENT_UOM", Me.NullConvertString(.Item("CURRENT_UOM")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DESTINATION_MATERIAL", Me.NullConvertString(.Item("DESTINATION_MATERIAL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DESTINATION_DESCRIPTION", Me.NullConvertString(.Item("DESTINATION_DESCRIPTION")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DESTINATION_PLANT", Me.NullConvertString(.Item("DESTINATION_PLANT")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DESTINATION_STORAGE_LOCATION", Me.NullConvertString(.Item("DESTINATION_STORAGE_LOCATION")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DESTINATION_BATCH", Me.NullConvertString(.Item("DESTINATION_BATCH")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DESTINATION_QUANTITY", Me.NullConvertString(.Item("DESTINATION_QUANTITY")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DESTINATION_UOM", Me.NullConvertString(.Item("DESTINATION_UOM")), DBDataType.CHAR))

            'EDI受信（DTL）共通項目
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", LMConst.FLG.ON, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", "", DBDataType.VARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@SEND_USER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_DATE", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_TIME", "", DBDataType.VARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@PRINT_FLG", LMConst.FLG.OFF, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRINT_USER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRINT_DATE", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRINT_TIME", "", DBDataType.VARCHAR))

            'システム管理用項目
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

#End Region

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所</param>
    ''' <param name="sverFlg">サーバー切り替え有無フラグTrue:有り</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String _
                                 , ByVal brCd As String _
                                 , Optional ByVal sverFlg As Boolean = False) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

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
        ElseIf String.IsNullOrEmpty(value.ToString) Then
            value = 0
        End If

        Return value

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

#End Region

#End Region

End Class
