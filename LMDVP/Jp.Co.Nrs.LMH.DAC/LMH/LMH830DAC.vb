' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH
'  プログラムID     :  LMH830DAC : 未着・早着ファイル作成(UTI)
'  作  成  者       :  umano
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH830DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH830DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"


#End Region

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(COA.NRS_BR_CD)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                      " & vbNewLine

    ''' <summary>
    ''' Z_KBNデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SENDFOLDERPASS As String = " SELECT                                                         " & vbNewLine _
                                            & "	      KBN.KBN_NM2                        AS SEND_MI_OUTPUT_DIR           " & vbNewLine _
                                            & "	     ,KBN.KBN_NM3                        AS SEND_SOU_OUTPUT_DIR          " & vbNewLine _
                                            & "	     ,KBN.KBN_NM4                        AS BACKUP_OUTPUT_DIR            " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_SENDFOLDERPASS As String = "FROM                                                                         " & vbNewLine _
                                          & "             $LM_MST$..Z_KBN AS KBN                             " & vbNewLine

#End Region

#Region "WHERE句"

    Private Const SQL_WHERE_SENDFOLDERPASS As String = "WHERE                                               " & vbNewLine _
                                         & "        KBN.KBN_GROUP_CD = @KBN_GROUP_CD                    " & vbNewLine _
                                         & "AND     KBN.KBN_CD = @KBN_CD                                " & vbNewLine


#End Region

#Region "検索処理SQL"

    ''' <summary>
    ''' 合致データ(不整合なし)取得SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_AGREE As String = "SELECT                                                                                " & vbNewLine _
                                                      & "   COUNT(CRT_DATE)  AS MEISAI_CNT                                            " & vbNewLine _
                                                      & "  ,DATA_CONF.CRT_DATE                                                        " & vbNewLine _
                                                      & "  ,DATA_CONF.FILE_NAME                                                       " & vbNewLine _
                                                      & "  ,DATA_CONF.REC_NO                                                          " & vbNewLine _
                                                      & "  ,DATA_CONF.NRS_BR_CD                                                       " & vbNewLine _
                                                      & "  ,DATA_CONF.INKA_NO_L                                                       " & vbNewLine _
                                                      & "  ,DATA_CONF.INKA_NO_M                                                       " & vbNewLine _
                                                      & "  ,DATA_CONF.INKA_DATE                                                       " & vbNewLine _
                                                      & "  ,MAX(DATA_CONF.SYS_UPD_DATE) AS SYS_UPD_DATE                               " & vbNewLine _
                                                      & "  ,MAX(DATA_CONF.SYS_UPD_TIME) AS SYS_UPD_TIME                               " & vbNewLine _
                                                      & "  ,DATA_CONF.CNT_INKA_NO_LMS                                                 " & vbNewLine _
                                                      & "  FROM                                                                       " & vbNewLine _
                                                      & "  (SELECT                                                                    " & vbNewLine _
                                                      & "  DISTINCT                                                                   " & vbNewLine _
                                                      & "     H_UTI.CRT_DATE        AS CRT_DATE                                       " & vbNewLine _
                                                      & "    ,H_UTI.FILE_NAME       AS FILE_NAME                                      " & vbNewLine _
                                                      & "    ,H_UTI.REC_NO          AS REC_NO                                         " & vbNewLine _
                                                      & "    ,INKAS.NRS_BR_CD       AS NRS_BR_CD                                      " & vbNewLine _
                                                      & "    ,INKAL.INKA_DATE       AS INKA_DATE                                      " & vbNewLine _
                                                      & "    ,INKAS.INKA_NO_L       AS INKA_NO_L                                      " & vbNewLine _
                                                      & "    ,INKAS.INKA_NO_M       AS INKA_NO_M                                      " & vbNewLine _
                                                      & "--    ,INKAS.SYS_UPD_DATE    AS SYS_UPD_DATE                                   " & vbNewLine _
                                                      & "--    ,INKAS.SYS_UPD_TIME    AS SYS_UPD_TIME                                   " & vbNewLine _
                                                      & "    ,INKAL.SYS_UPD_DATE    AS SYS_UPD_DATE                                   " & vbNewLine _
                                                      & "    ,INKAL.SYS_UPD_TIME    AS SYS_UPD_TIME                                   " & vbNewLine _
                                                      & "    ,H_UTI.H4_DELIVERY_NO  AS DELIVERY_NO                                    " & vbNewLine _
                                                      & "    ,INKAS.SERIAL_NO       AS SERIAL_NO                                      " & vbNewLine _
                                                      & "-- upd 20180216    ,SUBSTRING(H_UTI.H3_CODE,4,7)    AS DCT_CONSIGNEE                        " & vbNewLine _
                                                      & "    ,H_UTI.H3_CODE         AS DCT_CONSIGNEE                                  " & vbNewLine _
                                                      & "    ,REPLACE(UNSOL.ORIG_CD,'*','')   AS NRS_CONSIGNEE                        " & vbNewLine _
                                                      & "-- 20180216    ,SUBSTRING(D_UTI.L2_ITEM_CODE,4,7)         AS DCT_GOODS_CD                 " & vbNewLine _
                                                      & "    ,D_UTI.L2_ITEM_CODE    AS DCT_GOODS_CD                                   " & vbNewLine _
                                                      & "--    ,REPLACE(D_UTI.L2_ITEM_CODE,'000','')    AS DCT_GOODS_CD                 " & vbNewLine _
                                                      & "    ,INKAS.GOODS_CD_CUST      AS NRS_GOODS_CD                                " & vbNewLine _
                                                      & "    ,D_UTI.L2_BATCH_NO     AS DCT_LOT_NO                                     " & vbNewLine _
                                                      & "    ,INKAS.LOT_NO          AS NRS_LOT_NO                                     " & vbNewLine _
                                                      & "--    ,D_UTI.L2_QUANTITY     AS DCT_KOSU                                       " & vbNewLine _
                                                      & "--    ,INKAS.KOSU          AS NRS_KOSU                                         " & vbNewLine _
                                                      & "    ,CNT_INKAS.INKA_NO_L  AS CNT_INKA_NO_LMS                                 " & vbNewLine _
                                                      & "  FROM $LM_TRN$..H_INKAEDI_HED_UTI H_UTI                                     " & vbNewLine _
                                                      & "  LEFT JOIN                                                                  " & vbNewLine _
                                                      & "  (SELECT                                                                    " & vbNewLine _
                                                      & "   GRP_INKAS.NRS_BR_CD                                                       " & vbNewLine _
                                                      & "  ,GRP_INKAS.INKA_NO_L                                                       " & vbNewLine _
                                                      & "  ,GRP_INKAS.INKA_NO_M                                                       " & vbNewLine _
                                                      & "  ,GRP_INKAS.INKA_NO_S                                                       " & vbNewLine _
                                                      & "  ,GRP_INKAS.SERIAL_NO                                                       " & vbNewLine _
                                                      & "  FROM                                                                       " & vbNewLine _
                                                      & "  $LM_TRN$..B_INKA_S GRP_INKAS                                               " & vbNewLine _
                                                      & "  WHERE                                                                      " & vbNewLine _
                                                      & "      GRP_INKAS.NRS_BR_CD = @NRS_BR_CD                                     " & vbNewLine _
                                                      & "  AND GRP_INKAS.SYS_DEL_FLG = '0'                                            " & vbNewLine _
                                                      & "  AND GRP_INKAS.SERIAL_NO <> ''                                              " & vbNewLine _
                                                      & "  GROUP BY                                                                   " & vbNewLine _
                                                      & "   GRP_INKAS.NRS_BR_CD                                                       " & vbNewLine _
                                                      & "  ,GRP_INKAS.INKA_NO_L                                                       " & vbNewLine _
                                                      & "  ,GRP_INKAS.INKA_NO_M                                                       " & vbNewLine _
                                                      & "  ,GRP_INKAS.INKA_NO_S                                                       " & vbNewLine _
                                                      & "  ,GRP_INKAS.SERIAL_NO                                                       " & vbNewLine _
                                                      & "  )CNT_INKAS                                                                 " & vbNewLine _
                                                      & "  ON H_UTI.H4_DELIVERY_NO = CNT_INKAS.SERIAL_NO                              " & vbNewLine _
                                                      & "  AND H_UTI.NRS_BR_CD = CNT_INKAS.NRS_BR_CD                                  " & vbNewLine _
                                                      & "  AND H_UTI.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                      & "  AND NOT H_UTI.H4_DELIVERY_NO IS NULL                                       " & vbNewLine _
                                                      & "  LEFT JOIN                                                                  " & vbNewLine _
                                                      & "  (SELECT                                                                    " & vbNewLine _
                                                      & "   CRT_DATE                                                                  " & vbNewLine _
                                                      & "  ,FILE_NAME                                                                 " & vbNewLine _
                                                      & "  ,REC_NO                                                                    " & vbNewLine _
                                                      & "  ,L2_ITEM_CODE                                                              " & vbNewLine _
                                                      & "  ,L2_BATCH_NO                                                               " & vbNewLine _
                                                      & "--  ,SUM(L2_QUANTITY) AS L2_QUANTITY                                           " & vbNewLine _
                                                      & "--  ,SUM(L2_WEIGHT_NET) AS L2_QUANTITY                                           " & vbNewLine _
                                                      & "  FROM                                                                       " & vbNewLine _
                                                      & "  $LM_TRN$..H_INKAEDI_DTL_UTI                                                " & vbNewLine _
                                                      & "  WHERE                                                                      " & vbNewLine _
                                                      & "   DEL_KB = '0'                                                              " & vbNewLine _
                                                      & "  GROUP BY                                                                   " & vbNewLine _
                                                      & "   CRT_DATE                                                                  " & vbNewLine _
                                                      & "  ,FILE_NAME                                                                 " & vbNewLine _
                                                      & "  ,REC_NO                                                                    " & vbNewLine _
                                                      & "  ,L2_ITEM_CODE                                                              " & vbNewLine _
                                                      & "  ,L2_BATCH_NO                                                               " & vbNewLine _
                                                      & "  ) D_UTI                                                                    " & vbNewLine _
                                                      & "  ON H_UTI.CRT_DATE = D_UTI.CRT_DATE                                         " & vbNewLine _
                                                      & "  AND H_UTI.FILE_NAME = D_UTI.FILE_NAME                                      " & vbNewLine _
                                                      & "  AND H_UTI.REC_NO = D_UTI.REC_NO                                            " & vbNewLine _
                                                      & "  LEFT JOIN                                                                  " & vbNewLine _
                                                      & "  (SELECT                                                                    " & vbNewLine _
                                                      & "   SB_INKAS.NRS_BR_CD                                                        " & vbNewLine _
                                                      & "  ,SB_INKAS.INKA_NO_L                                                        " & vbNewLine _
                                                      & "  ,SB_INKAS.INKA_NO_M                                                        " & vbNewLine _
                                                      & "  ,SB_INKAS.LOT_NO                                                           " & vbNewLine _
                                                      & "  ,SB_INKAS.SERIAL_NO                                                        " & vbNewLine _
                                                      & "  ,SB_INKAS.SYS_UPD_DATE                                                     " & vbNewLine _
                                                      & "  ,SB_INKAS.SYS_UPD_TIME                                                     " & vbNewLine _
                                                      & "  ,MG.GOODS_CD_CUST                                                          " & vbNewLine _
                                                      & "--  ,SUM(SB_INKAS.KONSU * MG.PKG_NB + SB_INKAS.HASU)       AS KOSU             " & vbNewLine _
                                                      & "--  ,SUM((SB_INKAS.KONSU * MG.PKG_NB + SB_INKAS.HASU) * SB_INKAS.IRIME)  AS KOSU  " & vbNewLine _
                                                      & "  FROM                                                                       " & vbNewLine _
                                                      & "  $LM_TRN$..B_INKA_S SB_INKAS                                                " & vbNewLine _
                                                      & "  LEFT JOIN $LM_TRN$..B_INKA_M SB_INKAM                                      " & vbNewLine _
                                                      & "  ON SB_INKAS.NRS_BR_CD = SB_INKAM.NRS_BR_CD                                 " & vbNewLine _
                                                      & "  AND SB_INKAS.INKA_NO_L = SB_INKAM.INKA_NO_L                                " & vbNewLine _
                                                      & "  AND SB_INKAS.INKA_NO_M = SB_INKAM.INKA_NO_M                                " & vbNewLine _
                                                      & "  LEFT JOIN $LM_MST$..M_GOODS MG                                             " & vbNewLine _
                                                      & "  ON MG.NRS_BR_CD = SB_INKAM.NRS_BR_CD                                       " & vbNewLine _
                                                      & "  AND MG.GOODS_CD_NRS = SB_INKAM.GOODS_CD_NRS                                " & vbNewLine _
                                                      & "  WHERE                                                                      " & vbNewLine _
                                                      & "      SB_INKAS.NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
                                                      & "  AND SB_INKAS.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                                      & "  AND MG.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                                                      & "  AND MG.CUST_CD_L = @CUST_CD_L                                            " & vbNewLine _
                                                      & "  AND MG.CUST_CD_M = @CUST_CD_M                                            " & vbNewLine _
                                                      & "  AND SB_INKAM.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                                      & "  AND MG.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                                      & "  GROUP BY                                                                   " & vbNewLine _
                                                      & "   SB_INKAS.NRS_BR_CD                                                        " & vbNewLine _
                                                      & "  ,SB_INKAS.INKA_NO_L                                                        " & vbNewLine _
                                                      & "  ,SB_INKAS.INKA_NO_M                                                        " & vbNewLine _
                                                      & "  ,SB_INKAS.LOT_NO                                                           " & vbNewLine _
                                                      & "  ,SB_INKAS.SERIAL_NO                                                        " & vbNewLine _
                                                      & "  ,SB_INKAS.SYS_UPD_DATE                                                     " & vbNewLine _
                                                      & "  ,SB_INKAS.SYS_UPD_TIME                                                     " & vbNewLine _
                                                      & "  ,MG.GOODS_CD_CUST                                                          " & vbNewLine _
                                                      & "  )INKAS                                                                     " & vbNewLine _
                                                      & "  --LEFT JOIN                                                                " & vbNewLine _
                                                      & "  --LEFT JOIN $LM_TRN$..B_INKA_S INKAS                                       " & vbNewLine _
                                                      & "  ON H_UTI.H4_DELIVERY_NO = INKAS.SERIAL_NO                                  " & vbNewLine _
                                                      & "  AND H_UTI.NRS_BR_CD = INKAS.NRS_BR_CD                                      " & vbNewLine _
                                                      & "  LEFT JOIN $LM_TRN$..B_INKA_L INKAL                                         " & vbNewLine _
                                                      & "  ON INKAS.NRS_BR_CD = INKAL.NRS_BR_CD                                       " & vbNewLine _
                                                      & "  AND INKAS.INKA_NO_L = INKAL.INKA_NO_L                                      " & vbNewLine _
                                                      & "  LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                         " & vbNewLine _
                                                      & "  ON INKAL.NRS_BR_CD = UNSOL.NRS_BR_CD                                       " & vbNewLine _
                                                      & "  AND INKAL.INKA_NO_L = UNSOL.INOUTKA_NO_L                                   " & vbNewLine _
                                                      & "  AND MOTO_DATA_KB = '10'                                                    " & vbNewLine _
                                                      & "  WHERE H_UTI.NRS_BR_CD       = @NRS_BR_CD                                 " & vbNewLine _
                                                      & "    AND H_UTI.CUST_CD_L       = @CUST_CD_L                                 " & vbNewLine _
                                                      & "    AND H_UTI.CUST_CD_M       = @CUST_CD_M                                 " & vbNewLine _
                                                      & "  --  AND H_UTI.CRT_DATE        = '20121222'                                 " & vbNewLine _
                                                      & "  --  AND H_UTI.MISOUCYAKU_SHORI_FLG  = '0'                                  " & vbNewLine _
                                                      & "    AND NOT H_UTI.CRT_DATE IS NULL                                           " & vbNewLine _
                                                      & "    AND H_UTI.DEL_KB           = '0'                                         " & vbNewLine _
                                                      & "    AND H_UTI.INKA_TAG_FLG     = '0'                                         " & vbNewLine _
                                                      & "  --  AND H_UTI.CRT_DATE >= '20121219'                                       " & vbNewLine _
                                                      & "    AND NOT INKAL.NRS_BR_CD IS NULL                                          " & vbNewLine _
                                                      & "    AND INKAL.INKA_STATE_KB = '40'                                           " & vbNewLine _
                                                      & "    AND INKAL.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                                      & "    AND UNSOL.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                                      & "    AND                                                                      " & vbNewLine _
                                                      & "  --  (H_UTI.H4_DELIVERY_NO = INKAS.SERIAL_NO                                " & vbNewLine _
                                                      & "  --  OR REPLACE(D_UTI.L2_ITEM_CODE,'000','') <> INKAS.GOODS_CD_CUST         " & vbNewLine _
                                                      & "  --  OR D_UTI.L2_BATCH_NO <> INKAS.LOT_NO                                   " & vbNewLine _
                                                      & "  --  OR D_UTI.L2_QUANTITY <> INKAS.KOSU)                                    " & vbNewLine _
                                                      & "    (H_UTI.H4_DELIVERY_NO = INKAS.SERIAL_NO                                  " & vbNewLine _
                                                      & "-- 20180216    AND SUBSTRING(H_UTI.H3_CODE,4,7) = REPLACE(UNSOL.ORIG_CD,'*','')         " & vbNewLine _
                                                      & "    AND H_UTI.H3_CODE    = REPLACE(UNSOL.ORIG_CD,'*','')                     " & vbNewLine _
                                                      & "-- 20180216    AND SUBSTRING(D_UTI.L2_ITEM_CODE,4,7) = INKAS.GOODS_CD_CUST              " & vbNewLine _
                                                      & "    AND D_UTI.L2_ITEM_CODE  = INKAS.GOODS_CD_CUST              " & vbNewLine _
                                                      & "--    AND REPLACE(D_UTI.L2_ITEM_CODE,'000','') = INKAS.GOODS_CD_CUST           " & vbNewLine _
                                                      & "    AND D_UTI.L2_BATCH_NO = INKAS.LOT_NO)                                     " & vbNewLine _
                                                      & "  --  AND D_UTI.L2_QUANTITY = INKAS.KOSU)                                      " & vbNewLine _
                                                      & "  --ORDER BY                                                                 " & vbNewLine _
                                                      & "  --H_UTI.H4_DELIVERY_NO                                                     " & vbNewLine _
                                                      & "  ) DATA_CONF                                                                " & vbNewLine _
                                                      & "  GROUP BY                                                                   " & vbNewLine _
                                                      & "   DATA_CONF.CRT_DATE                                                        " & vbNewLine _
                                                      & "  ,DATA_CONF.FILE_NAME                                                       " & vbNewLine _
                                                      & "  ,DATA_CONF.REC_NO                                                          " & vbNewLine _
                                                      & "  ,DATA_CONF.NRS_BR_CD                                                       " & vbNewLine _
                                                      & "  ,DATA_CONF.INKA_NO_L                                                       " & vbNewLine _
                                                      & "  ,DATA_CONF.INKA_NO_M                                                       " & vbNewLine _
                                                      & "  ,DATA_CONF.INKA_DATE                                                       " & vbNewLine _
                                                      & "  ,DATA_CONF.CNT_INKA_NO_LMS                                                 " & vbNewLine _
                                                      & "  HAVING                                                                     " & vbNewLine _
                                                      & "  COUNT(DATA_CONF.CRT_DATE) = COUNT(DATA_CONF.CNT_INKA_NO_LMS)               " & vbNewLine _
                                                      & "  ORDER BY                                                                   " & vbNewLine _
                                                      & "   DATA_CONF.INKA_NO_L                                                       " & vbNewLine _
                                                      & "  ,DATA_CONF.INKA_NO_M                                                       " & vbNewLine

    ''' <summary>
    ''' 未着データ取得SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_MICYAKU As String = "SELECT                                                                                                        " & vbNewLine _
                                        & "   H_UTI.CRT_DATE        AS CRT_DATE                               " & vbNewLine _
                                        & "  ,H_UTI.FILE_NAME       AS FILE_NAME                              " & vbNewLine _
                                        & "  ,H_UTI.REC_NO          AS REC_NO                                 " & vbNewLine _
                                        & "  ,H_UTI.NRS_BR_CD       AS NRS_BR_CD                              " & vbNewLine _
                                        & "  ,H_UTI.H4_DELIVERY_NO  AS DELIVERY_NO                            " & vbNewLine _
                                        & "-- 20180216  ,SUBSTRING(H_UTI.H3_CODE,4,7)               AS DCT_CONSIGNEE     " & vbNewLine _
                                        & "  ,H_UTI.H3_CODE         AS DCT_CONSIGNEE                          " & vbNewLine _
                                        & "-- 20180216  ,SUBSTRING(D_UTI.L2_ITEM_CODE,4,7)          AS DCT_GOODS_CD      " & vbNewLine _
                                        & "  ,D_UTI.L2_ITEM_CODE    AS DCT_GOODS_CD                           " & vbNewLine _
                                        & "--  ,REPLACE(D_UTI.L2_ITEM_CODE,'000','')       AS DCT_GOODS_CD      " & vbNewLine _
                                        & "  ,D_UTI.L2_NAME_INTERNAL                     AS DCT_GOODS_NM      " & vbNewLine _
                                        & "--  ,D_UTI.L2_QUANTITY                          AS DCT_KOSU          " & vbNewLine _
                                        & "  ,D_UTI.L2_BATCH_NO                          AS DCT_LOT_NO        " & vbNewLine _
                                        & "  ,H_UTI.SYS_UPD_DATE    AS SYS_UPD_DATE                           " & vbNewLine _
                                        & "  ,H_UTI.SYS_UPD_TIME    AS SYS_UPD_TIME                           " & vbNewLine _
                                        & "FROM $LM_TRN$..H_INKAEDI_HED_UTI H_UTI                            " & vbNewLine _
                                        & "LEFT JOIN                                                          " & vbNewLine _
                                        & "  (SELECT                                                          " & vbNewLine _
                                        & "   CRT_DATE                                                        " & vbNewLine _
                                        & "  ,FILE_NAME                                                       " & vbNewLine _
                                        & "  ,REC_NO                                                          " & vbNewLine _
                                        & "  ,L2_ITEM_CODE                                                    " & vbNewLine _
                                        & "  ,L2_NAME_INTERNAL                                                " & vbNewLine _
                                        & "  ,L2_BATCH_NO                                                     " & vbNewLine _
                                        & "  ,SUM(L2_QUANTITY) AS L2_QUANTITY                                 " & vbNewLine _
                                        & "  FROM                                                             " & vbNewLine _
                                        & "  $LM_TRN$..H_INKAEDI_DTL_UTI                                     " & vbNewLine _
                                        & "  WHERE                                                            " & vbNewLine _
                                        & "   DEL_KB = '0'                                                    " & vbNewLine _
                                        & "  GROUP BY                                                         " & vbNewLine _
                                        & "   CRT_DATE                                                        " & vbNewLine _
                                        & "  ,FILE_NAME                                                       " & vbNewLine _
                                        & "  ,REC_NO                                                          " & vbNewLine _
                                        & "  ,L2_ITEM_CODE                                                    " & vbNewLine _
                                        & "  ,L2_NAME_INTERNAL                                                " & vbNewLine _
                                        & "  ,L2_BATCH_NO                                                     " & vbNewLine _
                                        & "  ) D_UTI                                                          " & vbNewLine _
                                        & "  ON H_UTI.CRT_DATE = D_UTI.CRT_DATE                               " & vbNewLine _
                                        & "  AND H_UTI.FILE_NAME = D_UTI.FILE_NAME                            " & vbNewLine _
                                        & "  AND H_UTI.REC_NO = D_UTI.REC_NO                                  " & vbNewLine _
                                        & "LEFT JOIN $LM_TRN$..B_INKA_S INKAS                                " & vbNewLine _
                                        & "ON H_UTI.H4_DELIVERY_NO = INKAS.SERIAL_NO                          " & vbNewLine _
                                        & "AND H_UTI.NRS_BR_CD = INKAS.NRS_BR_CD                              " & vbNewLine _
                                        & "AND INKAS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                        & "LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                " & vbNewLine _
                                        & "ON INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                               " & vbNewLine _
                                        & "AND INKAS.INKA_NO_L = INKAM.INKA_NO_L                              " & vbNewLine _
                                        & "AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                              " & vbNewLine _
                                        & "AND INKAM.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                        & "LEFT JOIN $LM_TRN$..B_INKA_L INKAL                                " & vbNewLine _
                                        & "ON INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                               " & vbNewLine _
                                        & "AND INKAM.INKA_NO_L = INKAL.INKA_NO_L                              " & vbNewLine _
                                        & "AND INKAL.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                        & "WHERE H_UTI.NRS_BR_CD       = @NRS_BR_CD                           " & vbNewLine _
                                        & "  AND H_UTI.CUST_CD_L       = @CUST_CD_L                           " & vbNewLine _
                                        & "  AND H_UTI.CUST_CD_M       = @CUST_CD_M                           " & vbNewLine _
                                        & "--  AND H_UTI.CRT_DATE        = @EDI_CRT_DATE                      " & vbNewLine _
                                        & "--  AND H_UTI.MISOUCYAKU_SHORI_FLG  = '0'                          " & vbNewLine _
                                        & "  AND INKAL.INKA_NO_L IS NULL                                      " & vbNewLine _
                                        & "  AND H_UTI.DEL_KB           = '0'                                 " & vbNewLine _
                                        & "  AND H_UTI.INKA_TAG_FLG     = '0'                                 " & vbNewLine _
                                        & "ORDER BY                                                           " & vbNewLine _
                                        & "H_UTI.H4_DELIVERY_NO                                               " & vbNewLine

    'Private Const SQL_EXIT_MICYAKU As String = "SELECT                                       " & vbNewLine _
    '                                        & "   H_UTI.CRT_DATE        AS CRT_DATE          " & vbNewLine _
    '                                        & "  ,H_UTI.FILE_NAME       AS FILE_NAME         " & vbNewLine _
    '                                        & "  ,H_UTI.REC_NO          AS REC_NO            " & vbNewLine _
    '                                        & "  ,H_UTI.NRS_BR_CD       AS NRS_BR_CD         " & vbNewLine _
    '                                        & "  ,H_UTI.H4_DELIVERY_NO  AS DELIVERY_NO       " & vbNewLine _
    '                                        & "  ,H_UTI.SYS_UPD_DATE    AS SYS_UPD_DATE      " & vbNewLine _
    '                                        & "  ,H_UTI.SYS_UPD_TIME    AS SYS_UPD_TIME      " & vbNewLine _
    '                                        & "FROM $LM_TRN$..H_INKAEDI_HED_UTI H_UTI        " & vbNewLine _
    '                                        & "LEFT JOIN $LM_TRN$..B_INKA_S INKAS            " & vbNewLine _
    '                                        & "ON H_UTI.H4_DELIVERY_NO = INKAS.SERIAL_NO     " & vbNewLine _
    '                                        & "AND H_UTI.NRS_BR_CD = INKAS.NRS_BR_CD         " & vbNewLine _
    '                                        & "LEFT JOIN $LM_TRN$..B_INKA_M INKAM            " & vbNewLine _
    '                                        & "ON INKAS.NRS_BR_CD = INKAM.NRS_BR_CD          " & vbNewLine _
    '                                        & "AND INKAS.INKA_NO_L = INKAM.INKA_NO_L         " & vbNewLine _
    '                                        & "AND INKAS.INKA_NO_M = INKAM.INKA_NO_M         " & vbNewLine _
    '                                        & "LEFT JOIN $LM_TRN$..B_INKA_L INKAL            " & vbNewLine _
    '                                        & "ON INKAM.NRS_BR_CD = INKAL.NRS_BR_CD          " & vbNewLine _
    '                                        & "AND INKAM.INKA_NO_L = INKAL.INKA_NO_L         " & vbNewLine _
    '                                        & "WHERE H_UTI.NRS_BR_CD       = @NRS_BR_CD      " & vbNewLine _
    '                                        & "  AND H_UTI.CUST_CD_L       = @CUST_CD_L      " & vbNewLine _
    '                                        & "  AND H_UTI.CUST_CD_M       = @CUST_CD_M      " & vbNewLine _
    '                                        & "--  AND H_UTI.CRT_DATE        = @EDI_CRT_DATE   " & vbNewLine _
    '                                        & "--  AND H_UTI.MISOUCYAKU_SHORI_FLG  = '0'       " & vbNewLine _
    '                                        & "  AND INKAL.INKA_NO_L IS NULL                 " & vbNewLine _
    '                                        & "  AND H_UTI.DEL_KB           = '0'            " & vbNewLine _
    '                                        & "  AND H_UTI.INKA_TAG_FLG     = '0'            " & vbNewLine _
    '                                        & "ORDER BY                                      " & vbNewLine _
    '                                        & "H_UTI.H4_DELIVERY_NO                          " & vbNewLine

    ''' <summary>
    ''' 早着データ取得SQL(データ不整合も含む)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Const SQL_EXIT_SOUCYAKU As String = "SELECT                                                                                                         " & vbNewLine _
                                        & "  DISTINCT INKAS.SERIAL_NO  AS SERIAL_NO                                     " & vbNewLine _
                                        & "  ,''               AS DCT_CONSIGNEE                                         " & vbNewLine _
                                        & "  ,''               AS DCT_GOODS_CD                                          " & vbNewLine _
                                        & "--  ,0                AS DCT_KOSU                                              " & vbNewLine _
                                        & "  ,''               AS DCT_LOT_NO                                            " & vbNewLine _
                                        & "  FROM $LM_TRN$..B_INKA_S INKAS                                             " & vbNewLine _
                                        & "  LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                        " & vbNewLine _
                                        & "  ON INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                       " & vbNewLine _
                                        & "  AND INKAS.INKA_NO_L = INKAM.INKA_NO_L                                      " & vbNewLine _
                                        & "  AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                                      " & vbNewLine _
                                        & "  AND INKAM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                        & "  LEFT JOIN $LM_TRN$..B_INKA_L INKAL                                        " & vbNewLine _
                                        & "  ON INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                       " & vbNewLine _
                                        & "  AND INKAM.INKA_NO_L = INKAL.INKA_NO_L                                      " & vbNewLine _
                                        & "  LEFT JOIN $LM_TRN$..H_INKAEDI_HED_UTI H_UTI                               " & vbNewLine _
                                        & "  ON INKAS.SERIAL_NO = H_UTI.H4_DELIVERY_NO                                  " & vbNewLine _
                                        & "  AND INKAS.NRS_BR_CD = H_UTI.NRS_BR_CD                                      " & vbNewLine _
                                        & "  WHERE INKAL.NRS_BR_CD       = @NRS_BR_CD                                   " & vbNewLine _
                                        & "  AND INKAL.CUST_CD_L       = @CUST_CD_L                                     " & vbNewLine _
                                        & "  AND INKAL.CUST_CD_M       = @CUST_CD_M                                     " & vbNewLine _
                                        & "  AND INKAL.INKA_STATE_KB   = '40'                                           " & vbNewLine _
                                        & "  AND H_UTI.CRT_DATE IS NULL                                                 " & vbNewLine _
                                        & "  AND INKAS.SERIAL_NO <> ''                                                  " & vbNewLine _
                                        & "  AND INKAL.SYS_DEL_FLG     = '0'                                            " & vbNewLine _
                                        & "  AND INKAS.SYS_DEL_FLG     = '0'                                            " & vbNewLine _
                                        & "  UNION ALL                                                                  " & vbNewLine _
                                        & "  SELECT                                                                     " & vbNewLine _
                                        & "  DISTINCT H_UTI.H4_DELIVERY_NO               AS SERIAL_NO                   " & vbNewLine _
                                        & "-- 20180216  ,SUBSTRING(H_UTI.H3_CODE,4,7)               AS DCT_CONSIGNEE               " & vbNewLine _
                                        & "  ,H_UTI.H3_CODE                              AS DCT_CONSIGNEE               " & vbNewLine _
                                        & "-- 20180216  ,SUBSTRING(D_UTI.L2_ITEM_CODE,4,7)          AS DCT_GOODS_CD                " & vbNewLine _
                                        & "  ,D_UTI.L2_ITEM_CODE                         AS DCT_GOODS_CD                " & vbNewLine _
                                        & "--  ,REPLACE(D_UTI.L2_ITEM_CODE,'000','')       AS DCT_GOODS_CD                " & vbNewLine _
                                        & "--  ,D_UTI.L2_QUANTITY                          AS DCT_KOSU                    " & vbNewLine _
                                        & "  ,D_UTI.L2_BATCH_NO                          AS DCT_LOT_NO                  " & vbNewLine _
                                        & "  FROM $LM_TRN$..B_INKA_S INKAS                                             " & vbNewLine _
                                        & "  LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                        " & vbNewLine _
                                        & "  ON INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                       " & vbNewLine _
                                        & "  AND INKAS.INKA_NO_L = INKAM.INKA_NO_L                                      " & vbNewLine _
                                        & "  AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                                      " & vbNewLine _
                                        & "  AND INKAM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                        & "  LEFT JOIN $LM_TRN$..B_INKA_L INKAL                                        " & vbNewLine _
                                        & "  ON INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                       " & vbNewLine _
                                        & "  AND INKAM.INKA_NO_L = INKAL.INKA_NO_L                                      " & vbNewLine _
                                        & "  LEFT JOIN $LM_TRN$..H_INKAEDI_HED_UTI H_UTI                               " & vbNewLine _
                                        & "  ON INKAS.SERIAL_NO = H_UTI.H4_DELIVERY_NO                                  " & vbNewLine _
                                        & "  AND INKAS.NRS_BR_CD = H_UTI.NRS_BR_CD                                      " & vbNewLine _
                                        & "  AND H_UTI.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                        & "  LEFT JOIN                                                                  " & vbNewLine _
                                        & "  (SELECT                                                                  " & vbNewLine _
                                        & "  CRT_DATE                                                                " & vbNewLine _
                                        & "  ,FILE_NAME                                                               " & vbNewLine _
                                        & "  ,REC_NO                                                                  " & vbNewLine _
                                        & "  ,L2_ITEM_CODE                                                            " & vbNewLine _
                                        & "  ,L2_BATCH_NO                                                             " & vbNewLine _
                                        & "  ,SUM(L2_QUANTITY) AS L2_QUANTITY                                         " & vbNewLine _
                                        & "  FROM                                                                     " & vbNewLine _
                                        & "  $LM_TRN$..H_INKAEDI_DTL_UTI                                             " & vbNewLine _
                                        & "  WHERE                                                                   " & vbNewLine _
                                        & "   DEL_KB = '0'                                                           " & vbNewLine _
                                        & "  GROUP BY                                                                 " & vbNewLine _
                                        & "  CRT_DATE                                                                " & vbNewLine _
                                        & "  ,FILE_NAME                                                               " & vbNewLine _
                                        & "  ,REC_NO                                                                  " & vbNewLine _
                                        & "  ,L2_ITEM_CODE                                                            " & vbNewLine _
                                        & "  ,L2_BATCH_NO                                                             " & vbNewLine _
                                        & "  ) D_UTI                                                                  " & vbNewLine _
                                        & "  ON H_UTI.CRT_DATE = D_UTI.CRT_DATE                                       " & vbNewLine _
                                        & "  AND H_UTI.FILE_NAME = D_UTI.FILE_NAME                                    " & vbNewLine _
                                        & "  AND H_UTI.REC_NO = D_UTI.REC_NO                                          " & vbNewLine _
                                        & "  WHERE INKAL.NRS_BR_CD       = @NRS_BR_CD                                   " & vbNewLine _
                                        & "  AND INKAL.CUST_CD_L       = @CUST_CD_L                                   " & vbNewLine _
                                        & "  AND INKAL.CUST_CD_M       = @CUST_CD_M                                   " & vbNewLine _
                                        & "  AND INKAL.INKA_STATE_KB   = '40'                                         " & vbNewLine _
                                        & "  AND NOT H_UTI.CRT_DATE IS NULL                                           " & vbNewLine _
                                        & "  AND INKAS.SERIAL_NO <> ''                                                " & vbNewLine _
                                        & "  AND INKAL.SYS_DEL_FLG     = '0'                                          " & vbNewLine _
                                        & "  AND INKAS.SYS_DEL_FLG     = '0'                                          " & vbNewLine _
                                        & "  ORDER BY                                                                   " & vbNewLine _
                                        & "  INKAS.SERIAL_NO                                                            " & vbNewLine

    'Private Const SQL_EXIT_SOUCYAKU As String = "SELECT                                  " & vbNewLine _
    '                                    & "   DISTINCT INKAS.SERIAL_NO  AS SERIAL_NO     " & vbNewLine _
    '                                    & "FROM $LM_TRN$..B_INKA_S INKAS                 " & vbNewLine _
    '                                    & "LEFT JOIN $LM_TRN$..B_INKA_M INKAM            " & vbNewLine _
    '                                    & "ON INKAS.NRS_BR_CD = INKAM.NRS_BR_CD          " & vbNewLine _
    '                                    & "AND INKAS.INKA_NO_L = INKAM.INKA_NO_L         " & vbNewLine _
    '                                    & "AND INKAS.INKA_NO_M = INKAM.INKA_NO_M         " & vbNewLine _
    '                                    & "LEFT JOIN $LM_TRN$..B_INKA_L INKAL            " & vbNewLine _
    '                                    & "ON INKAM.NRS_BR_CD = INKAL.NRS_BR_CD          " & vbNewLine _
    '                                    & "AND INKAM.INKA_NO_L = INKAL.INKA_NO_L         " & vbNewLine _
    '                                    & "LEFT JOIN $LM_TRN$..H_INKAEDI_HED_UTI H_UTI   " & vbNewLine _
    '                                    & "ON INKAS.SERIAL_NO = H_UTI.H4_DELIVERY_NO     " & vbNewLine _
    '                                    & "AND INKAS.NRS_BR_CD = H_UTI.NRS_BR_CD         " & vbNewLine _
    '                                    & "WHERE INKAL.NRS_BR_CD       = @NRS_BR_CD      " & vbNewLine _
    '                                    & "  AND INKAL.CUST_CD_L       = @CUST_CD_L      " & vbNewLine _
    '                                    & "  AND INKAL.CUST_CD_M       = @CUST_CD_M      " & vbNewLine _
    '                                    & "  AND INKAL.INKA_STATE_KB   = '40'            " & vbNewLine _
    '                                    & "  AND H_UTI.CRT_DATE IS NULL                  " & vbNewLine _
    '                                    & "  AND INKAS.SERIAL_NO <> ''                   " & vbNewLine _
    '                                    & "  AND INKAL.SYS_DEL_FLG     = '0'             " & vbNewLine _
    '                                    & "ORDER BY                                      " & vbNewLine _
    '                                    & "INKAS.SERIAL_NO                               " & vbNewLine

    'Private Const SQL_EXIT_SOUCYAKU As String = "SELECT                                    " & vbNewLine _
    '                                & "INKAS.SERIAL_NO  AS SERIAL_NO                       " & vbNewLine _
    '                                & "FROM $LM_TRN$..B_INKA_S INKAS                      " & vbNewLine _
    '                                & "LEFT JOIN $LM_TRN$..B_INKA_M INKAM                 " & vbNewLine _
    '                                & "ON INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                " & vbNewLine _
    '                                & "AND INKAS.INKA_NO_L = INKAM.INKA_NO_L               " & vbNewLine _
    '                                & "AND INKAS.INKA_NO_M = INKAM.INKA_NO_M               " & vbNewLine _
    '                                & "--在庫レコード                                      " & vbNewLine _
    '                                & "LEFT JOIN                                           " & vbNewLine _
    '                                & "--$LM_TRN$..D_ZAI_TRS DZAI                         " & vbNewLine _
    '                                & "(SELECT                                             " & vbNewLine _
    '                                & "NRS_BR_CD                                           " & vbNewLine _
    '                                & ",INKA_NO_L                                          " & vbNewLine _
    '                                & ",INKA_NO_M                                          " & vbNewLine _
    '                                & ",INKA_NO_S                                          " & vbNewLine _
    '                                & ",SUM(PORA_ZAI_NB) AS PORA_ZAI_NB                    " & vbNewLine _
    '                                & ",SYS_DEL_FLG                                        " & vbNewLine _
    '                                & "FROM                                                " & vbNewLine _
    '                                & "$LM_TRN$..D_ZAI_TRS                                " & vbNewLine _
    '                                & "WHERE NRS_BR_CD       = @NRS_BR_CD                  " & vbNewLine _
    '                                & "AND CUST_CD_L       = @CUST_CD_L                    " & vbNewLine _
    '                                & "AND CUST_CD_M       = @CUST_CD_M                    " & vbNewLine _
    '                                & "AND SYS_DEL_FLG = '0'                               " & vbNewLine _
    '                                & "GROUP BY                                            " & vbNewLine _
    '                                & "NRS_BR_CD                                           " & vbNewLine _
    '                                & ",INKA_NO_L                                          " & vbNewLine _
    '                                & ",INKA_NO_M                                          " & vbNewLine _
    '                                & ",INKA_NO_S                                          " & vbNewLine _
    '                                & ",SYS_DEL_FLG                                        " & vbNewLine _
    '                                & ") DZAI                                              " & vbNewLine _
    '                                & "--入荷(小)                                          " & vbNewLine _
    '                                & "ON INKAS.NRS_BR_CD = DZAI.NRS_BR_CD                 " & vbNewLine _
    '                                & "AND INKAS.INKA_NO_L = DZAI.INKA_NO_L                " & vbNewLine _
    '                                & "AND INKAS.INKA_NO_M = DZAI.INKA_NO_M                " & vbNewLine _
    '                                & "AND INKAS.INKA_NO_S = DZAI.INKA_NO_S                " & vbNewLine _
    '                                & "LEFT JOIN                                           " & vbNewLine _
    '                                & "--$LM_TRN$..C_OUTKA_S OUTKAS                       " & vbNewLine _
    '                                & "--出荷(小)                                          " & vbNewLine _
    '                                & "(SELECT                                             " & vbNewLine _
    '                                & "NRS_BR_CD                                           " & vbNewLine _
    '                                & ",INKA_NO_L                                          " & vbNewLine _
    '                                & ",INKA_NO_M                                          " & vbNewLine _
    '                                & ",INKA_NO_S                                          " & vbNewLine _
    '                                & ",SYS_DEL_FLG                                        " & vbNewLine _
    '                                & "FROM                                                " & vbNewLine _
    '                                & "$LM_TRN$..C_OUTKA_S                                " & vbNewLine _
    '                                & "WHERE NRS_BR_CD       = @NRS_BR_CD                  " & vbNewLine _
    '                                & "AND SYS_DEL_FLG = '0'                               " & vbNewLine _
    '                                & "GROUP BY                                            " & vbNewLine _
    '                                & "NRS_BR_CD                                           " & vbNewLine _
    '                                & ",INKA_NO_L                                          " & vbNewLine _
    '                                & ",INKA_NO_M                                          " & vbNewLine _
    '                                & ",INKA_NO_S                                          " & vbNewLine _
    '                                & ",SYS_DEL_FLG) OUTKAS                                " & vbNewLine _
    '                                & "ON OUTKAS.NRS_BR_CD = DZAI.NRS_BR_CD                " & vbNewLine _
    '                                & "AND OUTKAS.INKA_NO_L = DZAI.INKA_NO_L               " & vbNewLine _
    '                                & "AND OUTKAS.INKA_NO_M = DZAI.INKA_NO_M               " & vbNewLine _
    '                                & "AND OUTKAS.INKA_NO_S = DZAI.INKA_NO_S               " & vbNewLine _
    '                                & "--出荷(中)                                          " & vbNewLine _
    '                                & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM               " & vbNewLine _
    '                                & "ON OUTKAM.NRS_BR_CD = OUTKAS.NRS_BR_CD              " & vbNewLine _
    '                                & "AND OUTKAM.OUTKA_NO_L = OUTKAS.INKA_NO_L            " & vbNewLine _
    '                                & "AND OUTKAM.OUTKA_NO_M = OUTKAS.INKA_NO_M            " & vbNewLine _
    '                                & "--出荷(大)                                          " & vbNewLine _
    '                                & "LEFT JOIN $LM_TRN$..C_OUTKA_L OUTKAL               " & vbNewLine _
    '                                & "ON OUTKAL.NRS_BR_CD = OUTKAM.NRS_BR_CD              " & vbNewLine _
    '                                & "AND OUTKAL.OUTKA_NO_L = OUTKAM.OUTKA_NO_L           " & vbNewLine _
    '                                & "--入荷(大)                                          " & vbNewLine _
    '                                & "LEFT JOIN $LM_TRN$..B_INKA_L INKAL                 " & vbNewLine _
    '                                & "ON INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                " & vbNewLine _
    '                                & "AND INKAM.INKA_NO_L = INKAL.INKA_NO_L               " & vbNewLine _
    '                                & "--UTI入荷確認EDIデータ(HED)                         " & vbNewLine _
    '                                & "LEFT JOIN $LM_TRN$..H_INKAEDI_HED_UTI H_UTI        " & vbNewLine _
    '                                & "ON INKAS.SERIAL_NO = H_UTI.H4_DELIVERY_NO           " & vbNewLine _
    '                                & "AND INKAS.NRS_BR_CD = H_UTI.NRS_BR_CD               " & vbNewLine _
    '                                & "WHERE INKAL.NRS_BR_CD       = @NRS_BR_CD            " & vbNewLine _
    '                                & "AND INKAL.CUST_CD_L       = @CUST_CD_L              " & vbNewLine _
    '                                & "AND INKAL.CUST_CD_M       = @CUST_CD_M              " & vbNewLine _
    '                                & "--  AND INKAL.INKA_STATE_KB   = '40'                " & vbNewLine _
    '                                & "AND H_UTI.CRT_DATE IS NULL                          " & vbNewLine _
    '                                & "AND OUTKAL.OUTKA_NO_L IS NULL                       " & vbNewLine _
    '                                & "AND DZAI.PORA_ZAI_NB > 0                            " & vbNewLine _
    '                                & "AND INKAS.SERIAL_NO <> ''                           " & vbNewLine _
    '                                & "AND INKAL.SYS_DEL_FLG     = '0'                     " & vbNewLine _
    '                                & "GROUP BY                                            " & vbNewLine _
    '                                & "INKAS.SERIAL_NO                                     " & vbNewLine _
    '                                & "ORDER BY                                            " & vbNewLine _
    '                                & "INKAS.SERIAL_NO                                     " & vbNewLine

#End Region

#Region "更新処理 SQL"

    ''' <summary>
    ''' B_INKA_L
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_B_INKA_L As String = "UPDATE $LM_TRN$..B_INKA_L               " & vbNewLine _
                                                            & " SET                                             " & vbNewLine _
                                                            & "       INKA_STATE_KB         = '50'              " & vbNewLine _
                                                            & "      ,HOKAN_STR_DATE        = @INKA_DATE        " & vbNewLine _
                                                            & "      ,SYS_UPD_DATE          = @SYS_UPD_DATE     " & vbNewLine _
                                                            & "      ,SYS_UPD_TIME          = @SYS_UPD_TIME     " & vbNewLine _
                                                            & "      ,SYS_UPD_PGID          = @SYS_UPD_PGID     " & vbNewLine _
                                                            & "      ,SYS_UPD_USER          = @SYS_UPD_USER     " & vbNewLine _
                                                            & " WHERE                                           " & vbNewLine _
                                                            & "       NRS_BR_CD             = @NRS_BR_CD        " & vbNewLine _
                                                            & " AND   INKA_NO_L             = @INKA_NO_L        " & vbNewLine _
                                                            & " AND   SYS_UPD_DATE          = @HAITA_SYS_UPD_DATE  " & vbNewLine _
                                                            & " AND   SYS_UPD_TIME          = @HAITA_SYS_UPD_TIME  " & vbNewLine _
                                                            & " AND   SYS_DEL_FLG           = '0'               " & vbNewLine _
                                                            & " AND   INKA_STATE_KB         = '40'              " & vbNewLine

    ''' <summary>
    ''' D_ZAI_TRS
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_D_ZAI_TRS As String = "UPDATE $LM_TRN$..D_ZAI_TRS               " & vbNewLine _
                                                            & " SET                                             " & vbNewLine _
                                                            & "       INKO_DATE             = @INKA_DATE        " & vbNewLine _
                                                            & "      ,SYS_UPD_DATE          = @SYS_UPD_DATE     " & vbNewLine _
                                                            & "      ,SYS_UPD_TIME          = @SYS_UPD_TIME     " & vbNewLine _
                                                            & "      ,SYS_UPD_PGID          = @SYS_UPD_PGID     " & vbNewLine _
                                                            & "      ,SYS_UPD_USER          = @SYS_UPD_USER     " & vbNewLine _
                                                            & " WHERE                                           " & vbNewLine _
                                                            & "       NRS_BR_CD             = @NRS_BR_CD        " & vbNewLine _
                                                            & " AND   INKA_NO_L             = @INKA_NO_L        " & vbNewLine _
                                                            & "-- AND   SYS_UPD_DATE          = @HAITA_SYS_UPD_DATE  " & vbNewLine _
                                                            & "-- AND   SYS_UPD_TIME          = @HAITA_SYS_UPD_TIME  " & vbNewLine _
                                                            & " AND   SYS_DEL_FLG           = '0'               " & vbNewLine

    ''' <summary>
    ''' H_INKAEDI_HED_UTI
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_H_INKAEDI_HED_UTI As String = "UPDATE $LM_TRN$..H_INKAEDI_HED_UTI               " & vbNewLine _
                                                            & " SET                                             " & vbNewLine _
                                                            & "       MISOUCYAKU_SHORI_FLG  = '1'               " & vbNewLine _
                                                            & "      ,MISOUCYAKU_USER       = @MISOUCYAKU_USER  " & vbNewLine _
                                                            & "      ,MISOUCYAKU_DATE       = @MISOUCYAKU_DATE  " & vbNewLine _
                                                            & "      ,MISOUCYAKU_TIME       = @MISOUCYAKU_TIME  " & vbNewLine _
                                                            & "      ,UPD_USER              = @UPD_USER         " & vbNewLine _
                                                            & "      ,UPD_DATE              = @UPD_DATE         " & vbNewLine _
                                                            & "      ,UPD_TIME              = @UPD_TIME         " & vbNewLine _
                                                            & "      ,SYS_UPD_DATE          = @SYS_UPD_DATE     " & vbNewLine _
                                                            & "      ,SYS_UPD_TIME          = @SYS_UPD_TIME     " & vbNewLine _
                                                            & "      ,SYS_UPD_PGID          = @SYS_UPD_PGID     " & vbNewLine _
                                                            & "      ,SYS_UPD_USER          = @SYS_UPD_USER     " & vbNewLine _
                                                            & " WHERE                                           " & vbNewLine _
                                                            & "       CRT_DATE              = @CRT_DATE         " & vbNewLine _
                                                            & " AND   FILE_NAME             = @FILE_NAME        " & vbNewLine _
                                                            & " AND   REC_NO                = @REC_NO           " & vbNewLine _
                                                            & "-- AND   SYS_UPD_DATE          = @HAITA_SYS_UPD_DATE  " & vbNewLine _
                                                            & "-- AND   SYS_UPD_TIME          = @HAITA_SYS_UPD_TIME     " & vbNewLine



    ''' <summary>                                                                                                       
    ''' H_INKAEDI_DTL_UTI                                                                                               
    ''' </summary>                                                                                                      
    ''' <remarks></remarks>                                                                                             
    Private Const SQL_UPDATE_H_INKAEDI_DTL_UTI As String = "UPDATE $LM_TRN$..H_INKAEDI_DTL_UTI                  " & vbNewLine _
                                                            & " SET                                             " & vbNewLine _
                                                            & "       UPD_USER              = @UPD_USER         " & vbNewLine _
                                                            & "      ,UPD_DATE              = @UPD_DATE         " & vbNewLine _
                                                            & "      ,UPD_TIME              = @UPD_TIME         " & vbNewLine _
                                                            & "      ,SYS_UPD_DATE          = @SYS_UPD_DATE     " & vbNewLine _
                                                            & "      ,SYS_UPD_TIME          = @SYS_UPD_TIME     " & vbNewLine _
                                                            & "      ,SYS_UPD_PGID          = @SYS_UPD_PGID     " & vbNewLine _
                                                            & "      ,SYS_UPD_USER          = @SYS_UPD_USER     " & vbNewLine _
                                                            & " WHERE                                           " & vbNewLine _
                                                            & "       CRT_DATE              = @CRT_DATE         " & vbNewLine _
                                                            & " AND   FILE_NAME             = @FILE_NAME        " & vbNewLine _
                                                            & " AND   REC_NO                = @REC_NO           " & vbNewLine

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
    ''' フォルダパス取得(区分M)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>区分マスタ取得SQLの構築・発行</remarks>
    Private Function GetSendFolderPass(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH830IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH830DAC.SQL_SELECT_SENDFOLDERPASS)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMH830DAC.SQL_FROM_SENDFOLDERPASS)        'SQL構築(データ抽出用from句)
        Me._StrSql.Append(LMH830DAC.SQL_WHERE_SENDFOLDERPASS)       'SQL構築(データ抽出用Where句)
        Call Me.SetinParameter()                                '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH830DAC", "GetSendFolderPass", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SEND_MI_OUTPUT_DIR", "SEND_MI_OUTPUT_DIR")
        map.Add("SEND_SOU_OUTPUT_DIR", "SEND_SOU_OUTPUT_DIR")
        map.Add("BACKUP_OUTPUT_DIR", "BACKUP_OUTPUT_DIR")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH830FILE_PATH")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 入荷・受信合致(不整合なし)データの取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>未着対象データ取得SQLの構築・発行</remarks>
    Private Function GetAgreeDataSql(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH830IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH830DAC.SQL_EXIT_AGREE, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChkChar()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH830DAC", "GetAgreeDataSql", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH830_AGREE_OUT")

        Return ds

    End Function


    ''' <summary>
    ''' 未着対象データの取得(UTI入荷確認EDIデータ：有、入荷データ：無)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>未着対象データ取得SQLの構築・発行</remarks>
    Private Function GetMicyakuDataFile(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH830IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH830DAC.SQL_EXIT_MICYAKU, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH830DAC", "GetMicyakuDataFile", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DELIVERY_NO", "DELIVERY_NO")
        map.Add("DCT_CONSIGNEE", "DCT_CONSIGNEE")
        map.Add("DCT_GOODS_CD", "DCT_GOODS_CD")
        map.Add("DCT_GOODS_NM", "DCT_GOODS_NM")
        'map.Add("DCT_KOSU", "DCT_KOSU")
        map.Add("DCT_LOT_NO", "DCT_LOT_NO")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH830_MICYAKU_OUT")

        Return ds

        ''処理件数の設定
        'reader.Read()
        'MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        'reader.Close()

        'Return ds

    End Function

    ''' <summary>
    ''' 早着対象データの取得(UTI入荷確認EDIデータ：無、入荷データ：有)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>早着対象データ取得SQLの構築・発行</remarks>
    Private Function GetSoucyakuDataFile(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH830IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH830DAC.SQL_EXIT_SOUCYAKU, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChkChar()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH830DAC", "GetSoucyakuDataFile", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("DCT_CONSIGNEE", "DCT_CONSIGNEE")
        map.Add("DCT_GOODS_CD", "DCT_GOODS_CD")
        'map.Add("DCT_KOSU", "DCT_KOSU")
        map.Add("DCT_LOT_NO", "DCT_LOT_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH830_SOUCYAKU_OUT")

        Return ds

        ''処理件数の設定
        'reader.Read()
        'MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        'reader.Close()

        'Return ds

    End Function

    ''' <summary>
    ''' 入荷データ(大)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データ(大)更新処理SQLの構築・発行</remarks>
    Private Function UpdateBInkaL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim agrTbl As DataTable = ds.Tables("LMH830_AGREE_OUT")
        Dim preInkaNoL As String = String.Empty

        Dim max As Integer = agrTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = agrTbl.Rows(i)

            If Me._Row.Item("INKA_NO_L").Equals(preInkaNoL) = True Then
                Continue For
            End If

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH830DAC.SQL_UPDATE_B_INKA_L, Me._Row.Item("NRS_BR_CD").ToString()))

            'SQLパラメータ初期化/設定
            Call Me.SetParamAgreeUpdate()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH830DAC", "UpdateBInkaL", cmd)

            If Me.UpdateResultChk(cmd) = False Then

                ds.Tables("LMH830Result").Rows(0)("IsErrorResult") = "1"
                Return ds
            End If

            preInkaNoL = Me._Row.Item("INKA_NO_L").ToString()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 在庫データ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データ(大)更新処理SQLの構築・発行</remarks>
    Private Function UpdateZaiTrs(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim agrTbl As DataTable = ds.Tables("LMH830_AGREE_OUT")

        Dim max As Integer = agrTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = agrTbl.Rows(i)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH830DAC.SQL_UPDATE_D_ZAI_TRS, Me._Row.Item("NRS_BR_CD").ToString()))

            'SQLパラメータ初期化/設定
            Call Me.SetParamAgreeUpdate()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH830DAC", "UpdateZaiTrs", cmd)

            If Me.UpdateResultChk(cmd) = False Then

                ds.Tables("LMH830Result").Rows(0)("IsErrorResult") = "1"
                Return ds
            End If

        Next

        Return ds

    End Function


    ''' <summary>
    ''' UTI入荷確認EDIデータ(HED)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>UTI入荷確認EDIデータ(DTL)更新処理SQLの構築・発行</remarks>
    Private Function UpdateHInkaEdiHedUti(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim mckTbl As DataTable = ds.Tables("LMH830_MICYAKU_OUT")

        Dim max As Integer = mckTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = mckTbl.Rows(i)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH830DAC.SQL_UPDATE_H_INKAEDI_HED_UTI, Me._Row.Item("NRS_BR_CD").ToString()))

            'SQLパラメータ初期化/設定
            Call Me.SetParamUpdate()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH830DAC", "UpdateHInkaEdiHedUti", cmd)

            If Me.UpdateResultChk(cmd) = False Then

                ds.Tables("LMH830Result").Rows(0)("IsErrorResult") = "1"
                Return ds
            End If


        Next

        Return ds

    End Function

    ''' <summary>
    ''' UTI入荷確認EDIデータ(DTL)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>UTI入荷確認EDIデータ(DTL)更新処理SQLの構築・発行</remarks>
    Private Function UpdateHInkaEdiDtlUti(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim mckTbl As DataTable = ds.Tables("LMH830_MICYAKU_OUT")

        Dim max As Integer = mckTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = mckTbl.Rows(i)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH830DAC.SQL_UPDATE_H_INKAEDI_DTL_UTI, Me._Row.Item("NRS_BR_CD").ToString()))

            'SQLパラメータ初期化/設定
            Call Me.SetParamUpdate()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH830DAC", "UpdateHInkaEdiDtlUti", cmd)

            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 更新時排他チェック
    ''' </summary>
    ''' <param name="cmd">更新SQL</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

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

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定(区分マスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetinParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", .Item("KBN_GROUP_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_CD", .Item("KBN_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(UTI入荷確認EDIデータ(EDI)存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CRT_DATE", .Item("EDI_CRT_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(UTI入荷確認EDIデータ(EDI)存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChkChar()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CRT_DATE", .Item("EDI_CRT_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetMicyakuParam()

        '更新項目
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamAgreeUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetAgreeParam()

        '更新項目
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetAgreeParam()

        With Me._Row
            'パラメータ設定

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item("INKA_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMicyakuParam()

        With Me._Row
            'パラメータ設定

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MISOUCYAKU_USER", MyBase.GetUserID(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MISOUCYAKU_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MISOUCYAKU_TIME", updTime.ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.VARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me._Row.Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me._Row.Item("LOT_NO").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me._Row.Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

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

#End Region

#End Region

End Class
