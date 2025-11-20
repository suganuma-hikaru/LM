' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF690    : 運送保健申込書
'  作  成  者       :  daikoku
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF690DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF690DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt_OUTKA As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "   COL.NRS_BR_CD                                    AS NRS_BR_CD  " & vbNewLine _
                                            & "  ,'DD'                                            AS PTN_ID     " & vbNewLine _
                                            & "  ,'00'                                             AS PTN_CD     " & vbNewLine _
                                            & "  ,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID               " & vbNewLine _
                                            & "            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID           " & vbNewLine _
                                            & "            ELSE MR3.RPT_ID                                       " & vbNewLine _
                                            & "   END                                              AS RPT_ID     " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用SELECT区
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_OUTKA As String = " SELECT   @MOTO_DATA_KB   AS MOTO_DATA_KB                                                      " & vbNewLine _
                                            & "  ,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                               " & vbNewLine _
                                            & "            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                           " & vbNewLine _
                                            & "            ELSE MR3.RPT_ID                                                                       " & vbNewLine _
                                            & "   END                                               AS RPT_ID                                    " & vbNewLine _
                                            & "  ,COL.OUTKA_PLAN_DATE                               AS OUTKA_PLAN_DATE                           " & vbNewLine _
                                            & "  ,GOODS.GOODS_NM_1                                  AS GOODS_NM_1                                " & vbNewLine _
                                            & "  ,GOODS.GOODS_CD_NRS                                AS GOODS_CD_NRS                              " & vbNewLine _
                                            & "  ,ISNULL(CUST.CUST_NM_L,'')                         AS CUST_NM_L                                 " & vbNewLine _
                                            & "  --①数量　⇒　（数量/標準数量）×標準重量に変更                                                 " & vbNewLine _
                                            & "--UPD 20200326 011635  ,(COM.OUTKA_TTL_QT / ISNULL(GOODS.STD_IRIME_NB,0)) * ISNULL(GOODS.STD_WT_KGS,0)    AS OUTKA_TTL_QT       --重量                 " & vbNewLine _
                                            & "  ,CASE WHEN  GOODS.STD_IRIME_NB = GOODS.STD_WT_KGS     THEN COM.OUTKA_TTL_QT " & vbNewLine _
                                            & "          ELSE COM.OUTKA_TTL_QT / ISNULL(GOODS.STD_IRIME_NB,0) * ISNULL(GOODS.STD_WT_KGS,0) " & vbNewLine _
                                            & "   END  AS OUTKA_TTL_QT                                                                           " & vbNewLine _
                                            & "  ,GOODS.KITAKU_GOODS_UP                             AS KITAKU_GOODS_UP    --単価                 " & vbNewLine _
                                            & "  ,ISNULL(KBN1.KBN_NM2,0)                            AS HOKENRITSU         --保険料率             " & vbNewLine _
                                            & " -- ,CEILING(FLOOR((COM.OUTKA_TTL_QT * COM.OUTKA_TTL_QT) * KBN1.KBN_NM2) / 1000) * 1000             " & vbNewLine _
                                            & " --UPD 20200326 011635 ,ROUND(((COM.OUTKA_TTL_QT * GOODS.KITAKU_GOODS_UP ) * KBN1.KBN_NM2),0) AS HOKENRYO          --保険料  " & vbNewLine _
                                            & "--  ,ROUND(((CASE WHEN  GOODS.STD_IRIME_NB = GOODS.STD_WT_KGS         THEN COM.OUTKA_TTL_QT   + (CASE WHEN GOODS.TARE_YN  = '01'　" & vbNewLine _
                                            & "--                                                  　                                     Then COM.OUTKA_TTL_NB * KBN3.VALUE1　" & vbNewLine _
                                            & "-- 													                                    ELSE 0 END)                  　　　　" & vbNewLine _
                                            & "--	               Else COM.OUTKA_TTL_QT / ISNULL(GOODS.STD_IRIME_NB,0) * ISNULL(GOODS.STD_WT_KGS,0)   + (CASE WHEN GOODS.TARE_YN  = '01'　          " & vbNewLine _
                                            & "--                                                  　                                                         Then COM.OUTKA_TTL_NB * KBN3.VALUE1　" & vbNewLine _
                                            & "-- 													                                                        ELSE 0 END)                  　　　　" & vbNewLine _
                                            & "--                End * GOODS.KITAKU_GOODS_UP ) * KBN1.KBN_NM2),0) As HOKENRYO          --保険料    " & vbNewLine _
                                            & "  ,ROUND(                                                                                        " & vbNewLine _
                                            & "     ROUND(                                                                                      " & vbNewLine _
                                            & "       GOODS.KITAKU_GOODS_UP * (                                                                 " & vbNewLine _
                                            & "       CASE WHEN GOODS.STD_IRIME_NB = GOODS.STD_WT_KGS THEN COM.OUTKA_TTL_QT                     " & vbNewLine _
                                            & "            ELSE COM.OUTKA_TTL_QT / ISNULL(GOODS.STD_IRIME_NB,0) * ISNULL(GOODS.STD_WT_KGS,0)    " & vbNewLine _
                                            & "            END                                                                                  " & vbNewLine _
                                            & "       ) ,0                                                                                      " & vbNewLine _
                                            & "     ) * KBN1.KBN_NM2 ,0                                                                         " & vbNewLine _
                                            & "   ) AS HOKENRYO --保険料                                                                        " & vbNewLine _
                                            & "--  ,SOKO.WH_NM                                        As WH_NM                                     " & vbNewLine _
                                            & "    ,Case When MTS.JISYATASYA_KB = '02'  --他社倉庫のとき " & vbNewLine _
                                            & "    　　         THEN MTS.TASYA_WH_NM                     " & vbNewLine _
                                            & "                 ELSE SOKO.WH_NM                          " & vbNewLine _
                                            & "     END   AS WH_NM                                       " & vbNewLine _
                                            & "--UPD 2019/6/27 006458  ,BR.AD_1                                           AS BRAD_1                                    " & vbNewLine _
                                            & "--  ,SOKO.AD_1                                         AS BRAD_1                                    " & vbNewLine _
                                            & "      ,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき " & vbNewLine _
                                            & "    　　         THEN MTS.TASYA_AD_1                        " & vbNewLine _
                                            & "                 ELSE SOKO.AD_1                             " & vbNewLine _
                                            & "     END   AS BRAD_1                                        " & vbNewLine _
                                            & "--    ,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき                           " & vbNewLine _
                                            & "--      　　         THEN MTS.NRS_BR_CD + MTS.WH_CD + MTS.TOU_NO + MTS.SITU_NO        " & vbNewLine _
                                            & "--                   ELSE SOKO.NRS_BR_CD + SOKO.WH_CD                                 " & vbNewLine _
                                            & " --     END   AS ORIG_CD                                                              " & vbNewLine _
                                            & "     ,UNSOL.ORIG_CD                                AS ORIG_CD                       " & vbNewLine _
                                            & "  	,Case When MTS.JISYATASYA_KB = '02'  --他社倉庫のとき                          " & vbNewLine _
                                            & "      　　         THEN MTS.TASYA_WH_NM                                             " & vbNewLine _
                                            & "                   ELSE SOKO.WH_NM                                                  " & vbNewLine _
                                            & "       END   AS ORIG_NM                                                             " & vbNewLine _
                                            & "    ,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき                           " & vbNewLine _
                                            & "      　　         THEN ''                                                          " & vbNewLine _
                                            & "                   ELSE SOKO.ZIP                                                    " & vbNewLine _
                                            & "      END   AS ORIG_ZIP                                                             " & vbNewLine _
                                            & "  	,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき                          " & vbNewLine _
                                            & "      　　         THEN MTS.TASYA_AD_1                                              " & vbNewLine _
                                            & "                   ELSE SOKO.AD_1                                                   " & vbNewLine _
                                            & "       END   AS ORIG_AD_1                                                           " & vbNewLine _
                                            & "  	,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき                          " & vbNewLine _
                                            & "      　　         THEN MTS.TASYA_AD_2                                              " & vbNewLine _
                                            & "                   ELSE SOKO.AD_2                                                   " & vbNewLine _
                                            & "       END   AS ORIG_AD_2                                                           " & vbNewLine _
                                            & "  	,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき                          " & vbNewLine _
                                            & "      　　         THEN MTS.TASYA_AD_3                                              " & vbNewLine _
                                            & "                   ELSE SOKO.AD_3                                                   " & vbNewLine _
                                            & "       END   AS ORIG_AD_3                                                           " & vbNewLine _
                                            & "  ,DEST_D.AD_1 + DEST_D.AD_2 + COL.DEST_AD_3         AS DEST_ADD                    " & vbNewLine _
                                            & "  ,DEST_D.DEST_CD                                    As DEST_CD                    " & vbNewLine _
                                            & "  ,DEST_D.DEST_NM                                    As DEST_NM                     " & vbNewLine _
                                            & "　,DEST_D.ZIP                                        As DEST_ZIP                    " & vbNewLine _
                                            & "  ,DEST_D.AD_1                                       As DEST_AD_1                    " & vbNewLine _
                                            & "  ,DEST_D.AD_2                                       As DEST_AD_2                    " & vbNewLine _
                                            & "  ,DEST_D.AD_3                                       As DEST_AD_3                    " & vbNewLine _
                                            & "  ,UNSOL.REMARK                                      As REMARK             --備考                 " & vbNewLine _
                                            & "  ,COM.OUTKA_NO_L                                    As KANRI_NO_L         --出荷管理番号L        " & vbNewLine _
                                            & "  ,COM.OUTKA_NO_M                                    As KANRI_NO_M         --出荷管理番号M        " & vbNewLine _
                                            & "  ,ISNULL(KBN2.KBN_NM1,'')                           AS KITAKU_GOODS_UPNM  --寄託価格単位区分名   " & vbNewLine _
                                            & "  ,@UNSO_NO_L                                        AS UNSO_NO_L                                 " & vbNewLine _
                                            & "  ,COL.NRS_BR_CD                                     AS NRS_BR_CD                                 " & vbNewLine




    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_OUTKA As String = " FROM $LM_TRN$..C_OUTKA_L COL                                " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_CUST  CUST                          " & vbNewLine _
                                            & "     ON CUST.NRS_BR_CD   = COL.NRS_BR_CD                   " & vbNewLine _
                                            & "  AND CUST.CUST_CD_L   = COL.CUST_CD_L                     " & vbNewLine _
                                            & "  AND CUST.CUST_CD_M   = COL.CUST_CD_M                     " & vbNewLine _
                                            & "  AND CUST.CUST_CD_S   = '00'                              " & vbNewLine _
                                            & "  AND CUST.CUST_CD_SS  = '00'                              " & vbNewLine _
                                            & " LEFT JOIN $LM_TRN$..C_OUTKA_M COM                         " & vbNewLine _
                                            & "   ON COM.NRS_BR_CD   = COL.NRS_BR_CD                      " & vbNewLine _
                                            & "  AND COM.OUTKA_NO_L  = COL.OUTKA_NO_L                     " & vbNewLine _
                                            & "  AND COM.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                            & " LEFT JOIN   $LM_TRN$..C_OUTKA_S COS         " & vbNewLine _
                                            & "     ON  COS.NRS_BR_CD   = COM.NRS_BR_CD     " & vbNewLine _
                                            & "     AND COS.OUTKA_NO_L  = COM.OUTKA_NO_L    " & vbNewLine _
                                            & "     AND COS.OUTKA_NO_M  = COM.OUTKA_NO_M    " & vbNewLine _
                                            & "     AND COS.SYS_DEL_FLG = '0'               " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..M_TOU_SITU MTS         " & vbNewLine _
                                            & "    ON MTS.WH_CD   = COL.WH_CD               " & vbNewLine _
                                            & "   AND MTS.TOU_NO  = COS.TOU_NO              " & vbNewLine _
                                            & "   AND MTS.SITU_NO  = COS.SITU_NO            " & vbNewLine _
                                            & "   AND MTS.SYS_DEL_FLG = '0'                 " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_GOODS GOODS                         " & vbNewLine _
                                            & "   ON GOODS.NRS_BR_CD     = COM.NRS_BR_CD                  " & vbNewLine _
                                            & "  AND GOODS.GOODS_CD_NRS  = COM.GOODS_CD_NRS               " & vbNewLine _
                                            & "  AND GOODS.SYS_DEL_FLG   = '0'                            " & vbNewLine _
                                            & "  AND GOODS.UNSO_HOKEN_YN = '01'   --運送保険有り時        " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN  KBN1                           " & vbNewLine _
                                            & "   ON KBN1.KBN_GROUP_CD  = 'H027'                          " & vbNewLine _
                                            & "  AND KBN1.KBN_NM1       = COL.NRS_BR_CD                   " & vbNewLine _
                                            & "  AND KBN1.SYS_DEL_FLG   = '0'                             " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN  KBN2                           " & vbNewLine _
                                            & "   ON KBN2.KBN_GROUP_CD  = 'T003'                          " & vbNewLine _
                                            & "  AND KBN2.KBN_CD        = GOODS.KITAKU_AM_UT_KB           " & vbNewLine _
                                            & "  AND KBN2.SYS_DEL_FLG   = '0'                             " & vbNewLine _
                                            & "    LEFT JOIN LM_MST..Z_KBN  KBN3                          " & vbNewLine _
                                            & "   On KBN3.KBN_GROUP_CD  = 'N001'                          " & vbNewLine _
                                            & "  And KBN3.KBN_CD        = GOODS.PKG_UT                    " & vbNewLine _
                                            & "  And KBN3.SYS_DEL_FLG   = '0'                             " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_SOKO SOKO                           " & vbNewLine _
                                            & "   ON SOKO.NRS_BR_CD   = COL.NRS_BR_CD                     " & vbNewLine _
                                            & "  AND SOKO.WH_CD       = COL.WH_CD                         " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_NRS_BR   BR                         " & vbNewLine _
                                            & "   ON BR.NRS_BR_CD   = COL.NRS_BR_CD                       " & vbNewLine _
                                            & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                        " & vbNewLine _
                                            & "   ON UNSOL.NRS_BR_CD   = COL.NRS_BR_CD                    " & vbNewLine _
                                            & "  AND UNSOL.INOUTKA_NO_L  = COL.OUTKA_NO_L                 " & vbNewLine _
                                            & "  AND UNSOL.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_DEST    DEST_D                      " & vbNewLine _
                                            & "   ON  DEST_D.NRS_BR_CD =  COL.NRS_BR_CD                     " & vbNewLine _
                                            & "  AND  DEST_D.CUST_CD_L =  COL.CUST_CD_L                     " & vbNewLine _
                                            & "  AND  DEST_D.DEST_CD   =  COL.DEST_CD                       " & vbNewLine _
                                            & "  --荷主での荷主帳票パターン取得                           " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                      " & vbNewLine _
                                            & "   ON MCR1.NRS_BR_CD = COL.NRS_BR_CD                       " & vbNewLine _
                                            & "  AND MCR1.CUST_CD_L = COL.CUST_CD_L                       " & vbNewLine _
                                            & "  AND MCR1.CUST_CD_M = COL.CUST_CD_M                       " & vbNewLine _
                                            & "  AND MCR1.CUST_CD_S = '00'                                " & vbNewLine _
                                            & "  AND MCR1.PTN_ID    = 'DD'                                " & vbNewLine _
                                            & "  --帳票パターン取得                                       " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..M_RPT MR1                            " & vbNewLine _
                                            & "   ON MR1.NRS_BR_CD   = MCR1.NRS_BR_CD                     " & vbNewLine _
                                            & "  AND MR1.PTN_ID      = 'DD'          　　　               " & vbNewLine _
                                            & "  AND MR1.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                            & "  --商品Mの荷主での荷主帳票パターン取得                    " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                      " & vbNewLine _
                                            & "    ON MCR2.NRS_BR_CD = GOODS.NRS_BR_CD                    " & vbNewLine _
                                            & "   AND MCR2.CUST_CD_L = GOODS.CUST_CD_L                    " & vbNewLine _
                                            & "   AND MCR2.CUST_CD_M = GOODS.CUST_CD_M                    " & vbNewLine _
                                            & "   AND MCR2.CUST_CD_S = GOODS.CUST_CD_S                    " & vbNewLine _
                                            & "   AND MCR2.PTN_ID    = 'DD'                               " & vbNewLine _
                                            & "  --帳票パターン取得                                       " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..M_RPT MR2                            " & vbNewLine _
                                            & "    ON MR2.NRS_BR_CD     = MCR2.NRS_BR_CD                  " & vbNewLine _
                                            & "   AND MR2.PTN_ID        = MCR2.PTN_ID                     " & vbNewLine _
                                            & "   AND MR2.PTN_CD        = MCR2.PTN_CD                     " & vbNewLine _
                                            & "   AND MR2.SYS_DEL_FLG   = '0'                             " & vbNewLine _
                                            & "  --存在しない場合の帳票パターン取得                       " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..M_RPT MR3                            " & vbNewLine _
                                            & "    ON MR3.NRS_BR_CD     = COL.NRS_BR_CD                   " & vbNewLine _
                                            & "   AND MR3.PTN_ID        = 'DD'                            " & vbNewLine _
                                            & "   AND MR3.STANDARD_FLAG = '01'                            " & vbNewLine _
                                            & "   AND MR3.SYS_DEL_FLG   = '0'                             " & vbNewLine _
                                            & " WHERE COL.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                            & "   AND COL.NRS_BR_CD       =  @NRS_BR_CD                   " & vbNewLine _
                                            & "   AND COL.OUTKA_NO_L      =  @OUTKA_NO_L                  " & vbNewLine _
                                            & "   AND GOODS.UNSO_HOKEN_YN = '01'                          " & vbNewLine _
                                            & "   AND UNSOL.UNSO_TEHAI_KB =  '10'     --日陸手配のみ　    " & vbNewLine _


    ''' <summary>
    ''' ORDER BY（①営業所コード、②管理番号L、、③管理番号M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_OUTKA As String = "ORDER BY                     " & vbNewLine _
                                         & "      COM.NRS_BR_CD          " & vbNewLine _
                                         & "    , COM.OUTKA_NO_L         " & vbNewLine _
                                         & "    , COM.OUTKA_NO_M         " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用SELECT区
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_INKA As String = "	 SELECT   @MOTO_DATA_KB   AS MOTO_DATA_KB                                                      	 " & vbNewLine _
                                        & "	  ,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                	 " & vbNewLine _
                                        & "	            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID           	 " & vbNewLine _
                                        & "	            ELSE MR3.RPT_ID                                       	 " & vbNewLine _
                                        & "	   END                                               AS RPT_ID    	 " & vbNewLine _
                                        & "	  ,INKAL.INKA_DATE                                   AS OUTKA_PLAN_DATE  --入荷だけど出荷と同じにする  " & vbNewLine _
                                        & "--	  ,GOODS.GOODS_NM_1                              AS GOODS_NM_1   " & vbNewLine _
                                        & "	  ,INKAM_MATOME.GOODS_NM_1                           AS GOODS_NM_1   " & vbNewLine _
                                        & "   ,INKAM_MATOME.GOODS_CD_NRS                         AS GOODS_CD_NRS  " & vbNewLine _
                                        & "	  ,ISNULL(CUST.CUST_NM_L,'')                         AS CUST_NM_L    " & vbNewLine _
                                        & "	  --①数量　⇒　（数量/標準数量）×標準重量に変更                    " & vbNewLine _
                                        & "	--    ,((ISNULL(INKAS.KONSU , 0)                                     	 " & vbNewLine _
                                        & "	--      *  ISNULL(GOODS.PKG_NB , 0)                                  	 " & vbNewLine _
                                        & "	--      +  ISNULL(INKAS.HASU   , 0))                                 	 " & vbNewLine _
                                        & "	--      *  ISNULL(INKAS.IRIME  , 0))                                 	 " & vbNewLine _
                                        & "	--      * GOODS.STD_WT_KGS                                           	 " & vbNewLine _
                                        & "	--      / GOODS.STD_IRIME_NB                                        	 " & vbNewLine _
                                        & "	  ,INKAM_MATOME.SUM_JURYO_M AS OUTKA_TTL_QT  --出荷と同じにするSUM_JURYO_M  --重量	     " & vbNewLine _
                                        & "--	  ,GOODS.KITAKU_GOODS_UP               AS KITAKU_GOODS_UP    --単価  " & vbNewLine _
                                        & "	  ,INKAM_MATOME.KITAKU_GOODS_UP        AS KITAKU_GOODS_UP    --単価  " & vbNewLine _
                                        & "	  ,ISNULL(KBN1.KBN_NM2,0)              AS HOKENRITSU         --保険料率    " & vbNewLine _
                                        & "	  ,INKAM_MATOME.GOODS_CD_NRS        AS GOODS_CD_NRS   " & vbNewLine _
                                        & "--	  ,ROUND(((CASE WHEN  GOODS.STD_IRIME_NB = GOODS.STD_WT_KGS         THEN INKAM_MATOME.SUM_JURYO_M        	 " & vbNewLine _
                                        & "--	                ELSE INKAM_MATOME.SUM_JURYO_M / ISNULL(GOODS.STD_IRIME_NB,0) * ISNULL(GOODS.STD_WT_KGS,0) 	 " & vbNewLine _
                                        & "--	                END * GOODS.KITAKU_GOODS_UP ) * KBN1.KBN_NM2),0)      AS HOKENRYO          --保険料    	     " & vbNewLine _
                                        & "--   ,ROUND(((((INKAM_MATOME.SUM_JURYO_M  + (CASE WHEN GOODS.TARE_YN  = '01'                                            " & vbNewLine _
                                        & "--                                        THEN INKAM_MATOME.SUM_KONSU * KBN3.VALUE1         　                         " & vbNewLine _
                                        & "--                                        ELSE 0 END))   * GOODS.KITAKU_GOODS_UP )) * KBN1.KBN_NM2),0)      AS HOKENRYO          --保険料    	     " & vbNewLine _
                                        & "   ,ROUND(ROUND(GOODS.KITAKU_GOODS_UP * (INKAM_MATOME.SUM_JURYO_M), 0) * KBN1.KBN_NM2, 0) AS HOKENRYO --保険料 " & vbNewLine _
                                        & "	  ,DEST_O.DEST_NM                                        AS WH_NM  " & vbNewLine _
                                        & "	  ,DEST_O.AD_1                                           AS BRAD_1 " & vbNewLine _
                                        & "   ,DEST_O.DEST_CD    AS ORIG_CD " & vbNewLine _
                                        & "   ,DEST_O.DEST_NM    AS ORIG_NM " & vbNewLine _
                                        & "   ,DEST_O.ZIP        AS ORIG_ZIP " & vbNewLine _
                                        & "   ,DEST_O.AD_1       AS ORIG_AD_1 " & vbNewLine _
                                        & "   ,DEST_O.AD_2       AS ORIG_AD_2 " & vbNewLine _
                                        & "   ,DEST_O.AD_3       AS ORIG_AD_3 " & vbNewLine _
                                        & "	  ,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき	           " & vbNewLine _
                                        & "	             THEN MTS.TASYA_AD_1	                               " & vbNewLine _
                                        & "	             ELSE SOKO.AD_1 	                                   " & vbNewLine _
                                        & "	     END   AS DEST_ADD 	                                           " & vbNewLine _
                                        & "	 ,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき	           " & vbNewLine _
                                        & "	             THEN MTS.TASYA_WH_NM	                               " & vbNewLine _
                                        & "	             ELSE SOKO.WH_NM 	                                   " & vbNewLine _
                                        & "	   END   AS DEST_NM  	                                           " & vbNewLine _
                                        & "--   ,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき                         " & vbNewLine _
                                        & "--     　　         THEN MTS.NRS_BR_CD + MTS.WH_CD + MTS.TOU_NO + MTS.SITU_NO      " & vbNewLine _
                                        & "-- END   AS DEST_CD                                                            " & vbNewLine _
                                        & "  ,UNSOL.DEST_CD                         AS DEST_CD                             " & vbNewLine _
                                        & "   ,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき                         " & vbNewLine _
                                        & "     　　         THEN ''                                                        " & vbNewLine _
                                        & "                  ELSE SOKO.ZIP                                                  " & vbNewLine _
                                        & "     END   AS DEST_ZIP                                                           " & vbNewLine _
                                        & " 	,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき                       " & vbNewLine _
                                        & "     　　         THEN MTS.TASYA_AD_1                                            " & vbNewLine _
                                        & "                  ELSE SOKO.AD_1                                                 " & vbNewLine _
                                        & "      END   AS DEST_AD_1                                                         " & vbNewLine _
                                        & " 	,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき                       " & vbNewLine _
                                        & "     　　         THEN MTS.TASYA_AD_2                                            " & vbNewLine _
                                        & "                  ELSE SOKO.AD_2                                                 " & vbNewLine _
                                        & "      END   AS DEST_AD_2                                                         " & vbNewLine _
                                        & " 	,CASE WHEN MTS.JISYATASYA_KB = '02'  --他社倉庫のとき                       " & vbNewLine _
                                        & "     　　         THEN MTS.TASYA_AD_3                                            " & vbNewLine _
                                        & "                  ELSE SOKO.AD_3                                                 " & vbNewLine _
                                        & "      END   AS DEST_AD_3                                                         " & vbNewLine _
                                                                                & "	  ,UNSOL.REMARK                        AS REMARK             --備考                  " & vbNewLine _
                                        & "	  ,INKAM_MATOME.INKA_NO_L              AS KANRI_NO_L         --入荷管理番号L        	 " & vbNewLine _
                                        & "	  ,INKAM_MATOME.INKA_NO_M              AS KANRI_NO_M         --入荷管理番号M        	 " & vbNewLine _
                                        & "	  ,ISNULL(KBN2.KBN_NM1,'')             AS KITAKU_GOODS_UPNM  --寄託価格単位区分名  	 " & vbNewLine _
                                        & " 　,@UNSO_NO_L                          AS UNSO_NO_L   　　　　　　" & vbNewLine _
                                        & "   ,INKAL.NRS_BR_CD                     AS NRS_BR_CD                                                                          " & vbNewLine _

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt_INKA As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "   INKAL.NRS_BR_CD                                 AS NRS_BR_CD  " & vbNewLine _
                                            & "  ,'DD'                                            AS PTN_ID     " & vbNewLine _
                                            & "  ,'00'                                             AS PTN_CD     " & vbNewLine _
                                            & "  ,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID               " & vbNewLine _
                                            & "            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID           " & vbNewLine _
                                            & "            ELSE MR3.RPT_ID                                       " & vbNewLine _
                                            & "   END                                              AS RPT_ID     " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_INKA As String = "	 FROM $LM_TRN$..B_INKA_L INKAL                               	                 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..M_CUST  CUST                            	                 " & vbNewLine _
                                        & "	      ON CUST.NRS_BR_CD   = INKAL.NRS_BR_CD                   	                 " & vbNewLine _
                                        & "	      AND CUST.CUST_CD_L   = INKAL.CUST_CD_L                                 	 " & vbNewLine _
                                        & "	      AND CUST.CUST_CD_M   = INKAL.CUST_CD_M                                	 " & vbNewLine _
                                        & "	      AND CUST.CUST_CD_S   = '00'                                           	 " & vbNewLine _
                                        & "	      AND CUST.CUST_CD_SS  = '00'      	                                         " & vbNewLine _
                                        & "	 LEFT JOIN $LM_TRN$..B_INKA_M INKAM                         	                 " & vbNewLine _
                                        & "	     ON INKAM.NRS_BR_CD   = INKAL.NRS_BR_CD                                   	 " & vbNewLine _
                                        & "	     AND INKAM.INKA_NO_L  = INKAL.INKA_NO_L                                   	 " & vbNewLine _
                                        & "	     AND INKAM.SYS_DEL_FLG = '0' 	                                             " & vbNewLine _
                                        & "	 LEFT JOIN (	                                                                 " & vbNewLine _
                                        & "	             SELECT 	                                                         " & vbNewLine _
                                        & "	                   INKAM.INKA_NO_L	                                             " & vbNewLine _
                                        & "                ,INKAM.INKA_NO_M	                                             " & vbNewLine _
                                        & "	--                ,SUM((ISNULL(INKAS.KONSU , 0)                                  	 " & vbNewLine _
                                        & "	--                                           *  ISNULL(GOODS.PKG_NB , 0)         	 " & vbNewLine _
                                        & "	--                  +  ISNULL(INKAS.HASU   , 0))                                 	 " & vbNewLine _
                                        & "	--                 *  ISNULL(INKAS.IRIME  , 0))                                 	 " & vbNewLine _
                                        & "	--                * GOODS.STD_WT_KGS                                           	 " & vbNewLine _
                                        & "	--               / GOODS.STD_IRIME_NB                                        	     " & vbNewLine _
                                        & "	--                                                     AS      SUM_JURYO_M  	     " & vbNewLine _
                                        & "	             ,SUM(FLOOR((INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU)                 " & vbNewLine _
                                        & "	                * INKAS.IRIME                                                     " & vbNewLine _
                                        & "	                * GOODS.STD_WT_KGS                                                " & vbNewLine _
                                        & "	                / GOODS.STD_IRIME_NB * 1000) / 1000)      AS SUM_JURYO_M         " & vbNewLine _
                                        & "             ,SUM(INKAS.KONSU)                             AS SUM_KONSU           " & vbNewLine _
                                        & "	            ,GOODS.GOODS_NM_1                                                    " & vbNewLine _
                                        & "	            ,GOODS.GOODS_CD_NRS                                                  " & vbNewLine _
                                        & "	            ,GOODS.KITAKU_GOODS_UP                                               " & vbNewLine _
                                        & "	           FROM  $LM_TRN$..B_INKA_M INKAM	                                     " & vbNewLine _
                                        & "	           LEFT JOIN $LM_TRN$..B_INKA_S INKAS                         	         " & vbNewLine _
                                        & "	                      ON INKAS.NRS_BR_CD   = INKAM.NRS_BR_CD                   	 " & vbNewLine _
                                        & "	                  AND INKAS.INKA_NO_L = INKAM.INKA_NO_L  	                     " & vbNewLine _
                                        & "	                 AND INKAS.INKA_NO_M  = INKAM.INKA_NO_M  	                     " & vbNewLine _
                                        & "	                 AND INKAS.SYS_DEL_FLG = '0'                                	 " & vbNewLine _
                                        & "	             LEFT JOIN $LM_MST$..M_GOODS GOODS                              	 " & vbNewLine _
                                        & "	                   ON GOODS.NRS_BR_CD     = INKAM.NRS_BR_CD                  	 " & vbNewLine _
                                        & "	                AND GOODS.GOODS_CD_NRS  = INKAM.GOODS_CD_NRS                	 " & vbNewLine _
                                        & "	                AND GOODS.SYS_DEL_FLG   = '0'	                                 " & vbNewLine _
                                        & "	             WHERE	                                                             " & vbNewLine _
                                        & "	                         INKAM.NRS_BR_CD   = @NRS_BR_CD          ----------        	 " & vbNewLine _
                                        & "	                AND INKAM.INKA_NO_L  = INKAM.INKA_NO_L                      	 " & vbNewLine _
                                        & "	               AND INKAM.INKA_NO_M  =INKAM.INKA_NO_M	                         " & vbNewLine _
                                        & "	               AND INKAM.SYS_DEL_FLG = '0'	                                     " & vbNewLine _
                                        & "	               AND GOODS.UNSO_HOKEN_YN = '01'	--運送保険あり　                  " & vbNewLine _
                                        & "	            GROUP BY 	                                                         " & vbNewLine _
                                        & "	                  INKAM.INKA_NO_L	                                             " & vbNewLine _
                                        & "	                 ,INKAM.INKA_NO_M	                                             " & vbNewLine _
                                        & "	                ,GOODS.STD_WT_KGS                                           	 " & vbNewLine _
                                        & "	                ,GOODS.STD_IRIME_NB	                                             " & vbNewLine _
                                        & "	                ,GOODS.GOODS_NM_1                                                " & vbNewLine _
                                        & "	                ,GOODS.KITAKU_GOODS_UP                                           " & vbNewLine _
                                         & "	            ,GOODS.GOODS_CD_NRS                                              " & vbNewLine _
                                        & "	            ) AS INKAM_MATOME            	                                     " & vbNewLine _
                                        & "	        ON   INKAM_MATOME.INKA_NO_L = INKAL.INKA_NO_L	                         " & vbNewLine _
                                        & "	        AND  INKAM_MATOME.INKA_NO_M = INKAM.INKA_NO_M                            " & vbNewLine _
                                        & "	    LEFT JOIN   $LM_TRN$..B_INKA_S INKAS	                                     " & vbNewLine _
                                        & "	         ON	                                                                     " & vbNewLine _
                                        & "	             INKAS.NRS_BR_CD   = @NRS_BR_CD          ----------                  " & vbNewLine _
                                        & "	         AND INKAS.INKA_NO_L  = INKAM.INKA_NO_L                               	 " & vbNewLine _
                                        & "	         AND INKAS.INKA_NO_M  = INKAM.INKA_NO_M	                                 " & vbNewLine _
                                        & "	         AND INKAS.SYS_DEL_FLG = '0' 	                                         " & vbNewLine _
                                        & "	   LEFT JOIN $LM_MST$..M_TOU_SITU MTS                         	 " & vbNewLine _
                                        & "	         ON MTS.WH_CD   = INKAL.WH_CD                        	 " & vbNewLine _
                                        & "	      AND MTS.TOU_NO  = INKAS.TOU_NO    	                     " & vbNewLine _
                                        & "	     AND MTS.SITU_NO  = INKAS.SITU_NO    	                     " & vbNewLine _
                                        & "	     AND MTS.SYS_DEL_FLG = '0' 	                                 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..M_GOODS GOODS                             	 " & vbNewLine _
                                        & "	       ON GOODS.NRS_BR_CD     = INKAM.NRS_BR_CD                  " & vbNewLine _
                                        & "	    AND GOODS.GOODS_CD_NRS  = INKAM.GOODS_CD_NRS               	 " & vbNewLine _
                                        & "	    AND GOODS.SYS_DEL_FLG   = '0'                            	 " & vbNewLine _
                                        & "	    AND GOODS.UNSO_HOKEN_YN = '01'   --運送保険有り時        	 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..Z_KBN  KBN1                           	     " & vbNewLine _
                                        & "	       ON KBN1.KBN_GROUP_CD  = 'H027'                          	 " & vbNewLine _
                                        & "	     AND KBN1.KBN_NM1       = INKAL.NRS_BR_CD                 	 " & vbNewLine _
                                        & "	     AND KBN1.SYS_DEL_FLG   = '0'                             	 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..Z_KBN  KBN2                              	 " & vbNewLine _
                                        & "	   ON KBN2.KBN_GROUP_CD  = 'T003'                            	 " & vbNewLine _
                                        & "	  AND KBN2.KBN_CD        = GOODS.KITAKU_AM_UT_KB             	 " & vbNewLine _
                                        & "	  AND KBN2.SYS_DEL_FLG   = '0'                               	 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..Z_KBN  KBN3                              	 " & vbNewLine _
                                        & "	   ON KBN3.KBN_GROUP_CD  = 'N001'                            	 " & vbNewLine _
                                        & "	  AND KBN3.KBN_CD        = GOODS.PKG_UT                          " & vbNewLine _
                                        & "	  AND KBN3.SYS_DEL_FLG   = '0'                               	 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..M_SOKO SOKO                           	     " & vbNewLine _
                                        & "	   ON SOKO.NRS_BR_CD   = INKAL.NRS_BR_CD                     	 " & vbNewLine _
                                        & "	  AND SOKO.WH_CD       = INKAL.WH_CD                         	 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_MST$..M_NRS_BR   BR                          	 " & vbNewLine _
                                        & "	   ON BR.NRS_BR_CD   = INKAL.NRS_BR_CD                       	 " & vbNewLine _
                                        & "	 LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                          	 " & vbNewLine _
                                        & "	   ON UNSOL.NRS_BR_CD   = INKAL.NRS_BR_CD                    	 " & vbNewLine _
                                        & "	  AND UNSOL.INOUTKA_NO_L  = INKAL.INKA_NO_L  	                 " & vbNewLine _
                                        & "	  AND  UNSOL.MOTO_DATA_KB = '10' 	                             " & vbNewLine _
                                        & "	  AND UNSOL.SYS_DEL_FLG = '0'                                	 " & vbNewLine _
                                        & "	 LEFT JOIN                                                       " & vbNewLine _
                                        & "	   $LM_MST$..M_DEST                       DEST_O               	 " & vbNewLine _
                                        & "	ON                                                               " & vbNewLine _
                                        & "	     INKAL.NRS_BR_CD = DEST_O.NRS_BR_CD                        	 " & vbNewLine _
                                        & "	 AND INKAL.CUST_CD_L = DEST_O.CUST_CD_L                          " & vbNewLine _
                                        & "	 AND UNSOL.ORIG_CD   = DEST_O.DEST_CD   	                     " & vbNewLine _
                                        & "	LEFT JOIN $LM_MST$..M_SOKO               M_SOKO                	 " & vbNewLine _
                                        & "	ON                                                             	 " & vbNewLine _
                                        & "	M_SOKO.NRS_BR_CD = INKAL.NRS_BR_CD                            	 " & vbNewLine _
                                        & "	AND                                                           	 " & vbNewLine _
                                        & "	M_SOKO.WH_CD = INKAL.WH_CD                                     	 " & vbNewLine _
                                        & "	  --荷主での荷主帳票パターン取得                           	     " & vbNewLine _
                                        & "	  LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                       	 " & vbNewLine _
                                        & "	   ON MCR1.NRS_BR_CD = INKAL.NRS_BR_CD                       	 " & vbNewLine _
                                        & "	  AND MCR1.CUST_CD_L = INKAL.CUST_CD_L                       	 " & vbNewLine _
                                        & "	  AND MCR1.CUST_CD_M = INKAL.CUST_CD_M                       	 " & vbNewLine _
                                        & "	  AND MCR1.CUST_CD_S = '00'                                	 " & vbNewLine _
                                        & "	  AND MCR1.PTN_ID    = 'DD'                                	 " & vbNewLine _
                                        & "	  --帳票パターン取得                                       	 " & vbNewLine _
                                        & "	  LEFT JOIN $LM_MST$..M_RPT MR1                            	 " & vbNewLine _
                                        & "   ON MR1.NRS_BR_CD    = INKAL.NRS_BR_CD                      " & vbNewLine _
                                        & "	  AND MR1.PTN_ID      = 'DD'                               	 " & vbNewLine _
                                        & "	  AND MR1.SYS_DEL_FLG = '0'                                	 " & vbNewLine _
                                        & "	  --商品Mの荷主での荷主帳票パターン取得                    	 " & vbNewLine _
                                        & "	  LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                      	 " & vbNewLine _
                                        & "	    ON MCR2.NRS_BR_CD = GOODS.NRS_BR_CD                    	 " & vbNewLine _
                                        & "	   AND MCR2.CUST_CD_L = GOODS.CUST_CD_L                    	 " & vbNewLine _
                                        & "	   AND MCR2.CUST_CD_M = GOODS.CUST_CD_M                    	 " & vbNewLine _
                                        & "	   AND MCR2.CUST_CD_S = GOODS.CUST_CD_S                    	 " & vbNewLine _
                                        & "	   AND MCR2.PTN_ID    = 'DD'                  	             " & vbNewLine _
                                        & "	  --帳票パターン取得                                       	 " & vbNewLine _
                                        & "	  LEFT JOIN $LM_MST$..M_RPT MR2                            	 " & vbNewLine _
                                        & "	    ON MR2.NRS_BR_CD     = MCR2.NRS_BR_CD                  	 " & vbNewLine _
                                        & "	   AND MR2.PTN_ID        = MCR2.PTN_ID                     	 " & vbNewLine _
                                        & "	   AND MR2.PTN_CD        = MCR2.PTN_CD                     	 " & vbNewLine _
                                        & "	   AND MR2.SYS_DEL_FLG   = '0'                             	 " & vbNewLine _
                                        & "	  --存在しない場合の帳票パターン取得                       	 " & vbNewLine _
                                        & "	  LEFT JOIN $LM_MST$..M_RPT MR3                            	 " & vbNewLine _
                                        & "	    ON MR3.NRS_BR_CD     = INKAL.NRS_BR_CD                   " & vbNewLine _
                                        & "	   AND MR3.PTN_ID        = 'DD'                            	 " & vbNewLine _
                                        & "	   AND MR3.STANDARD_FLAG = '01'                            	 " & vbNewLine _
                                        & "	   AND MR3.SYS_DEL_FLG   = '0'                             	 " & vbNewLine _
                                        & "	 WHERE INKAL.SYS_DEL_FLG = '0'                             	 " & vbNewLine _
                                        & "	   AND INKAL.NRS_BR_CD   =  @NRS_BR_CD               	     " & vbNewLine _
                                        & "	   AND INKAL.INKA_NO_L   =  @INKA_NO_L                    	 " & vbNewLine _
                                        & "	   AND GOODS.UNSO_HOKEN_YN = '01'                          	 " & vbNewLine _
                                        & "	   AND UNSOL.UNSO_TEHAI_KB =  '10'      --日陸手配のみ　 	 " & vbNewLine


    ''' <summary>
    ''' ORDER BY（①営業所コード、②管理番号L、、③管理番号M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_INKA As String = "	ORDER BY                     	 " & vbNewLine _
                                        & "	      INKAM.NRS_BR_CD          	 " & vbNewLine _
                                        & "	    , INKAM.INKA_NO_L         	 " & vbNewLine



    ''' <summary>
    ''' 印刷データ抽出用SELECT区
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_UNSO As String = " 	 SELECT     @MOTO_DATA_KB   AS MOTO_DATA_KB                                                	 " & vbNewLine _
                                        & " 	 ,  CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                               	 " & vbNewLine _
                                        & " 	            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                           	 " & vbNewLine _
                                        & " 	            ELSE MR3.RPT_ID                                                                       	 " & vbNewLine _
                                        & " 	   END                                               AS RPT_ID                                    	 " & vbNewLine _
                                        & " 	  ,UNSO_L.OUTKA_PLAN_DATE                            AS OUTKA_PLAN_DATE                           	 " & vbNewLine _
                                        & " 	  --,GOODS.GOODS_NM_1                                AS GOODS_NM_1                              	 " & vbNewLine _
                                        & " 	  ,UNSO_M.GOODS_NM                                   AS GOODS_NM_1 	                                 " & vbNewLine _
                                        & " 	  ,ISNULL(CUST.CUST_NM_L,'')                         AS CUST_NM_L                                 	 " & vbNewLine _
                                        & " 	  --①数量　⇒　（数量/標準数量）×標準重量に変更                                                 	 " & vbNewLine _
                                        & " 	  ,CASE WHEN GOODS.GOODS_CD_NRS IS NULL THEN   UNSO_M.UNSO_TTL_NB * UNSO_M.BETU_WT	                 " & vbNewLine _
                                        & " 	                                        ELSE	                                                     " & vbNewLine _
                                        & " 	      CASE WHEN  GOODS.STD_IRIME_NB = GOODS.STD_WT_KGS     THEN UNSO_M.UNSO_TTL_QT        	         " & vbNewLine _
                                        & " 	          ELSE UNSO_M.UNSO_TTL_QT / ISNULL(GOODS.STD_IRIME_NB,0) * ISNULL(GOODS.STD_WT_KGS,0)        " & vbNewLine _
                                        & " 	   END  END    AS OUTKA_TTL_QT  	                                                              " & vbNewLine _
                                        & " 	  ,CASE WHEN GOODS.GOODS_CD_NRS IS NULL THEN  UNSO_M.KITAKU_GOODS_UP	                          " & vbNewLine _
                                        & " 	                                         ELSE  GOODS.KITAKU_GOODS_UP   END   AS KITAKU_GOODS_UP    --単価                 	 " & vbNewLine _
                                        & " 	  ,ISNULL(KBN1.KBN_NM2,0)                            AS HOKENRITSU         --保険料率       	 " & vbNewLine _
                                        & " 	  	 " & vbNewLine _
                                        & "-- 	  ,CASE WHEN GOODS.GOODS_CD_NRS IS NULL THEN ROUND(UNSO_M.BETU_WT * UNSO_M.KITAKU_GOODS_UP * KBN1.KBN_NM2,0)	                                     " & vbNewLine _
                                        & "-- 	    ELSE ROUND(((CASE WHEN  GOODS.STD_IRIME_NB = GOODS.STD_WT_KGS         THEN UNSO_M.UNSO_TTL_QT   + (CASE WHEN GOODS.TARE_YN  = '01'　	 " & vbNewLine _
                                        & "-- 	                                                  　                                     Then UNSO_M.UNSO_TTL_NB * KBN3.VALUE1　	         " & vbNewLine _
                                        & "-- 	                                                  ELSE 0 END)                  　　　　                                                 	 " & vbNewLine _
                                        & "-- 	                Else UNSO_M.UNSO_TTL_QT / ISNULL(GOODS.STD_IRIME_NB,0) * ISNULL(GOODS.STD_WT_KGS,0)   + (CASE WHEN GOODS.TARE_YN  = '01'　   " & vbNewLine _
                                        & "-- 	                                                  　                                                         THEN UNSO_M.UNSO_TTL_NB * KBN3.VALUE1　	 " & vbNewLine _
                                        & "-- 	                                                                                                             ELSE 0 END) 	                 " & vbNewLine _
                                        & "-- 	              END * GOODS.KITAKU_GOODS_UP ) * KBN1.KBN_NM2),0)  END As HOKENRYO          --保険料    	                                     " & vbNewLine _
                                        & " 	  ,CASE WHEN GOODS.GOODS_CD_NRS IS NULL THEN ROUND(ROUND(UNSO_M.KITAKU_GOODS_UP * UNSO_M.BETU_WT / 1000, 0) * 1000 * KBN1.KBN_NM2, 0) " & vbNewLine _
                                        & " 	        ELSE                                                                                                            " & vbNewLine _
                                        & " 	           ROUND(                                                                                                       " & vbNewLine _
                                        & " 	             ROUND(                                                                                                     " & vbNewLine _
                                        & " 	               GOODS.KITAKU_GOODS_UP * (                                                                                " & vbNewLine _
                                        & " 	               CASE WHEN GOODS.STD_IRIME_NB = GOODS.STD_WT_KGS THEN UNSO_M.UNSO_TTL_QT                                  " & vbNewLine _
                                        & " 	                    ELSE UNSO_M.UNSO_TTL_QT / ISNULL(GOODS.STD_IRIME_NB,0) * ISNULL(GOODS.STD_WT_KGS,0)                 " & vbNewLine _
                                        & " 	                    END                                                                                                 " & vbNewLine _
                                        & " 	                /1000), 0                                                                                                     " & vbNewLine _
                                        & " 	             )  *1000 * KBN1.KBN_NM2, 0                                                                                        " & vbNewLine _
                                        & " 	           )                                                                                                            " & vbNewLine _
                                        & " 	        END AS HOKENRYO --保険料                                                                                        " & vbNewLine _
                                        & " 	  ,DEST_O.DEST_NM                                    As WH_NM                                                                        	    " & vbNewLine _
                                        & " 	  ,DEST_O.AD_1 + DEST_O.AD_2 + DEST_O.AD_3                                       AS BRAD_1                                                                              " & vbNewLine _
                                        & " 	  ,DEST_D.AD_1 + DEST_D.AD_2 + DEST_D.AD_3                                       AS DEST_ADD                                                                            " & vbNewLine _
                                        & " 	  ,DEST_D.DEST_NM                                    AS DEST_NM                                   	                                        " & vbNewLine _
                                        & " 	  ,UNSO_L.REMARK                                     AS REMARK             --備考                 	                                        " & vbNewLine _
                                        & " 	  ,UNSO_M.UNSO_NO_L                                  AS KANRI_NO_L         --出荷管理番号L        	                                        " & vbNewLine _
                                        & " 	  ,UNSO_M.UNSO_NO_M                                  AS KANRI_NO_M         --出荷管理番号M        	                                        " & vbNewLine _
                                        & " 	  ,ISNULL(KBN2.KBN_NM1,'')                           AS KITAKU_GOODS_UPNM  --寄託価格単位区分名   	                                        " & vbNewLine _
                                        & "       ,UNSO_L.UNSO_NO_L                                  AS UNSO_NO_L                                                                           " & vbNewLine _
                                        & "       ,UNSO_L.NRS_BR_CD                                  AS NRS_BR_CD                                                                          " & vbNewLine _
                                        & "       ,isnull(UNSO_M.GOODS_CD_NRS,'')                    AS GOODS_CD_NRS                                                                        " & vbNewLine _
                                        & "       ,UNSO_L.ORIG_CD                                    AS ORIG_CD                                                                            " & vbNewLine _
                                        & "       ,DEST_O.DEST_NM                                    AS ORIG_NM                                                                             " & vbNewLine _
                                        & "       ,DEST_O.ZIP                                        AS ORIG_ZIP                                                                            " & vbNewLine _
                                        & "       ,DEST_O.AD_1                                       AS ORIG_AD_1                                                                           " & vbNewLine _
                                        & "       ,DEST_O.AD_2                                       AS ORIG_AD_2                                                                           " & vbNewLine _
                                        & "       ,DEST_O.AD_3                                       AS ORIG_AD_3                                                                           " & vbNewLine _
                                        & "       ,UNSO_L.DEST_CD                                    AS DEST_CD                                                                             " & vbNewLine _
                                        & "       ,DEST_D.DEST_NM                                    AS DEST_NM                                                                             " & vbNewLine _
                                        & "       ,DEST_D.ZIP                                        AS DEST_ZIP                                                                            " & vbNewLine _
                                        & "       ,DEST_D.AD_1                                       AS DEST_AD_1                                                                           " & vbNewLine _
                                        & "       ,DEST_D.AD_2                                       AS DEST_AD_2                                                                           " & vbNewLine _
                                        & "       ,DEST_D.AD_3                                       AS DEST_AD_3                                                                           " & vbNewLine



    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_UNSO As String = " From $LM_TRN$..F_UNSO_L UNSO_L                                	 " & vbNewLine _
                                        & " 	 LEFT Join LM_MST..M_CUST  CUST                          	 " & vbNewLine _
                                        & " 	     ON CUST.NRS_BR_CD   = UNSO_L.NRS_BR_CD                   	 " & vbNewLine _
                                        & " 	  AND CUST.CUST_CD_L   = UNSO_L.CUST_CD_L                     	 " & vbNewLine _
                                        & " 	  AND CUST.CUST_CD_M   = UNSO_L.CUST_CD_M                     	 " & vbNewLine _
                                        & " 	  AND CUST.CUST_CD_S   = '00'                              	 " & vbNewLine _
                                        & " 	  AND CUST.CUST_CD_SS  = '00'                              	 " & vbNewLine _
                                        & " 	 LEFT JOIN $LM_TRN$..F_UNSO_M UNSO_M                         	 " & vbNewLine _
                                        & " 	   ON UNSO_M.NRS_BR_CD  = UNSO_L.NRS_BR_CD                      	 " & vbNewLine _
                                        & " 	  AND UNSO_M.UNSO_NO_L  = UNSO_L.UNSO_NO_L                     	 " & vbNewLine _
                                        & " 	  AND UNSO_M.SYS_DEL_FLG = '0'                                	 " & vbNewLine _
                                        & " 	 LEFT JOIN LM_MST..M_GOODS GOODS                         	 " & vbNewLine _
                                        & " 	   ON GOODS.NRS_BR_CD     = UNSO_M.NRS_BR_CD                  	 " & vbNewLine _
                                        & " 	  AND GOODS.GOODS_CD_NRS  = UNSO_M.GOODS_CD_NRS               	 " & vbNewLine _
                                        & " 	  AND GOODS.SYS_DEL_FLG   = '0'                            	 " & vbNewLine _
                                        & " 	  AND GOODS.UNSO_HOKEN_YN = '01'   --運送保険有り時        	 " & vbNewLine _
                                        & " 	 LEFT JOIN LM_MST..Z_KBN  KBN1                           	 " & vbNewLine _
                                        & " 	   ON KBN1.KBN_GROUP_CD  = 'H027'                          	 " & vbNewLine _
                                        & " 	  AND KBN1.KBN_NM1       = UNSO_L.NRS_BR_CD                   	 " & vbNewLine _
                                        & " 	  AND KBN1.SYS_DEL_FLG   = '0'                             	 " & vbNewLine _
                                        & " 	 LEFT JOIN LM_MST..Z_KBN  KBN2                           	 " & vbNewLine _
                                        & " 	   ON KBN2.KBN_GROUP_CD  = 'T003'                          	 " & vbNewLine _
                                        & " 	  AND KBN2.KBN_CD        = GOODS.KITAKU_AM_UT_KB           	 " & vbNewLine _
                                        & " 	  AND KBN2.SYS_DEL_FLG   = '0'                             	 " & vbNewLine _
                                        & " 	    LEFT JOIN LM_MST..Z_KBN  KBN3                          	 " & vbNewLine _
                                        & " 	   On KBN3.KBN_GROUP_CD  = 'N001'                          	 " & vbNewLine _
                                        & " 	  And KBN3.KBN_CD        = GOODS.PKG_UT                    	 " & vbNewLine _
                                        & " 	  And KBN3.SYS_DEL_FLG   = '0'                                                         	 " & vbNewLine _
                                        & " 	LEFT  JOIN LM_MST..M_DEST               DEST_O --積込先              	 " & vbNewLine _
                                        & " 	  ON  UNSO_L.NRS_BR_CD                  = DEST_O.NRS_BR_CD     	 " & vbNewLine _
                                        & " 	 AND  UNSO_L.CUST_CD_L                  = DEST_O.CUST_CD_L     	 " & vbNewLine _
                                        & " 	 AND  UNSO_L.ORIG_CD                    = DEST_O.DEST_CD       	 " & vbNewLine _
                                        & " 	 AND  DEST_O.SYS_DEL_FLG                = '0'                  	 " & vbNewLine _
                                        & " 	LEFT  JOIN LM_MST..M_DEST               DEST_D --荷降先             	 " & vbNewLine _
                                        & " 	  ON  UNSO_L.NRS_BR_CD                  = DEST_D.NRS_BR_CD     	 " & vbNewLine _
                                        & " 	 AND  UNSO_L.CUST_CD_L                  = DEST_D.CUST_CD_L     	 " & vbNewLine _
                                        & " 	 AND  UNSO_L.DEST_CD                    = DEST_D.DEST_CD       	 " & vbNewLine _
                                        & " 	 AND  DEST_D.SYS_DEL_FLG               = '0'                     	 " & vbNewLine _
                                        & " 	  --荷主での荷主帳票パターン取得                           	 " & vbNewLine _
                                        & " 	  LEFT JOIN LM_MST..M_CUST_RPT MCR1                      	 " & vbNewLine _
                                        & " 	   ON MCR1.NRS_BR_CD = UNSO_L.NRS_BR_CD                       	 " & vbNewLine _
                                        & " 	  AND MCR1.CUST_CD_L = UNSO_L.CUST_CD_L                       	 " & vbNewLine _
                                        & " 	  AND MCR1.CUST_CD_M = UNSO_L.CUST_CD_M                       	 " & vbNewLine _
                                        & " 	  AND MCR1.CUST_CD_S = '00'                                	 " & vbNewLine _
                                        & " 	  AND MCR1.PTN_ID    = 'DD'                                	 " & vbNewLine _
                                        & " 	  --帳票パターン取得                                       	 " & vbNewLine _
                                        & " 	  LEFT JOIN LM_MST..M_RPT MR1                            	 " & vbNewLine _
                                        & " 	   ON MR1.NRS_BR_CD   = MCR1.NRS_BR_CD                     	 " & vbNewLine _
                                        & " 	  AND MR1.PTN_ID      = 'DD'                               	 " & vbNewLine _
                                        & " 	  AND MR1.SYS_DEL_FLG = '0'                                	 " & vbNewLine _
                                        & " 	  --商品Mの荷主での荷主帳票パターン取得                    	 " & vbNewLine _
                                        & " 	  LEFT JOIN LM_MST..M_CUST_RPT MCR2                      	 " & vbNewLine _
                                        & " 	    ON MCR2.NRS_BR_CD = GOODS.NRS_BR_CD                    	 " & vbNewLine _
                                        & " 	   AND MCR2.CUST_CD_L = GOODS.CUST_CD_L                    	 " & vbNewLine _
                                        & " 	   AND MCR2.CUST_CD_M = GOODS.CUST_CD_M                    	 " & vbNewLine _
                                        & " 	   AND MCR2.CUST_CD_S = GOODS.CUST_CD_S                    	 " & vbNewLine _
                                        & " 	   AND MCR2.PTN_ID    = 'DD'                               	 " & vbNewLine _
                                        & " 	  --帳票パターン取得                                       	 " & vbNewLine _
                                        & " 	  LEFT JOIN LM_MST..M_RPT MR2                            	 " & vbNewLine _
                                        & " 	    ON MR2.NRS_BR_CD     = MCR2.NRS_BR_CD                  	 " & vbNewLine _
                                        & " 	   AND MR2.PTN_ID        = MCR2.PTN_ID                     	 " & vbNewLine _
                                        & " 	   AND MR2.PTN_CD        = MCR2.PTN_CD                     	 " & vbNewLine _
                                        & " 	   AND MR2.SYS_DEL_FLG   = '0'                             	 " & vbNewLine _
                                        & " 	  --存在しない場合の帳票パターン取得                       	 " & vbNewLine _
                                        & " 	  LEFT JOIN LM_MST..M_RPT MR3                            	 " & vbNewLine _
                                        & " 	    ON MR3.NRS_BR_CD     = UNSO_L.NRS_BR_CD                   	 " & vbNewLine _
                                        & " 	   AND MR3.PTN_ID        = 'DD'                            	 " & vbNewLine _
                                        & " 	   AND MR3.STANDARD_FLAG = '01'                            	 " & vbNewLine _
                                        & " 	   AND MR3.SYS_DEL_FLG   = '0'                             	 " & vbNewLine _
                                        & " 	 WHERE UNSO_L.SYS_DEL_FLG = '0'                               	 " & vbNewLine _
                                        & " 	   AND UNSO_L.NRS_BR_CD       =  @NRS_BR_CD          ---------          	 " & vbNewLine _
                                        & " 	   AND UNSO_L.UNSO_NO_L      =   @UNSO_NO_L    ---------------               	 " & vbNewLine _
                                        & " 	   AND ((GOODS.UNSO_HOKEN_YN = '01' AND GOODS.KITAKU_GOODS_UP > 0)                                       " & vbNewLine _
                                        & " 	      OR (GOODS.GOODS_CD_NRS IS NULL AND UNSO_M.KITAKU_GOODS_UP > 0 AND UNSO_M.UNSO_HOKEN_UM = '01'))  	 " & vbNewLine _
                                        & " 	   AND UNSO_L.UNSO_TEHAI_KB =  '10'   --日陸手配のみ　    	 " & vbNewLine

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt_UNSO As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "   UNSO_L.NRS_BR_CD                                 AS NRS_BR_CD  " & vbNewLine _
                                            & "  ,'DD'                                            AS PTN_ID     " & vbNewLine _
                                            & "  ,'00'                                             AS PTN_CD     " & vbNewLine _
                                            & "  ,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID               " & vbNewLine _
                                            & "            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID           " & vbNewLine _
                                            & "            ELSE MR3.RPT_ID                                       " & vbNewLine _
                                            & "   END                                              AS RPT_ID     " & vbNewLine

    ''' <summary>
    ''' <summary>
    ''' ORDER BY（①営業所コード、②管理番号L、、③管理番号M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_UNSO As String = " 	ORDER BY                     	 " & vbNewLine _
                                        & " 	      UNSO_M.NRS_BR_CD          	 " & vbNewLine _
                                        & " 	    , UNSO_M.UNSO_NO_L          	 " & vbNewLine _
                                        & " 	    , UNSO_M.UNSO_NO_M      	     " & vbNewLine _

#End Region

#Region "運送保険"

    Private Const SQL_DELETE_UNSO_HOKEN As String = "DELETE $LM_TRN$..F_UNSO_HOKEN" & vbNewLine _
                                              & "WHERE UNSO_NO_L = @UNSO_NO_L  " & vbNewLine _
                                              & "  AND NRS_BR_CD = @NRS_BR_CD  " & vbNewLine


    Private Const SQL_INSERT_UNSO_HOKEN As String = "	INSERT INTO  $LM_TRN$..F_UNSO_HOKEN	  " & vbNewLine _
                                                & "	           (NRS_BR_CD	                  " & vbNewLine _
                                                & "	           ,UNSO_NO_L	                 " & vbNewLine _
                                                & "	           ,KANRI_NO_L	                 " & vbNewLine _
                                                & "	           ,KANRI_NO_M	                 " & vbNewLine _
                                                & "	           ,MOTO_DATA_KB_NM	             " & vbNewLine _
                                                & "	           ,KIKEN_START_DATE	         " & vbNewLine _
                                                & "	           ,GOODS_CD_NRS	             " & vbNewLine _
                                                & "	           ,KITAKU_GOODS_UP	             " & vbNewLine _
                                                & "	           ,ORIG_CD	                     " & vbNewLine _
                                                & "	           ,ORIG_NM	                     " & vbNewLine _
                                                & "	           ,ORIG_ZIP	                 " & vbNewLine _
                                                & "	           ,ORIG_AD_1	                 " & vbNewLine _
                                                & "	           ,ORIG_AD_2	                 " & vbNewLine _
                                                & "	           ,ORIG_AD_3	                 " & vbNewLine _
                                                & "	           ,DEST_CD	                     " & vbNewLine _
                                                & "	           ,DEST_NM	                     " & vbNewLine _
                                                & "	           ,DEST_ZIP	                 " & vbNewLine _
                                                & "	           ,DEST_AD_1	                 " & vbNewLine _
                                                & "	           ,DEST_AD_2	                 " & vbNewLine _
                                                & "	           ,DEST_AD_3	                 " & vbNewLine _
                                                & "	           ,HOKEN_RYOU	                 " & vbNewLine _
                                                & "	           ,SYS_ENT_DATE	             " & vbNewLine _
                                                & "	           ,SYS_ENT_TIME	             " & vbNewLine _
                                                & "	           ,SYS_ENT_PGID	             " & vbNewLine _
                                                & "	           ,SYS_ENT_USER	             " & vbNewLine _
                                                & "	           ,SYS_UPD_DATE                 " & vbNewLine _
                                                & "	           ,SYS_UPD_TIME	             " & vbNewLine _
                                                & "	           ,SYS_UPD_PGID	             " & vbNewLine _
                                                & "	           ,SYS_UPD_USER	             " & vbNewLine _
                                                & "	           ,SYS_DEL_FLG)	             " & vbNewLine _
                                                & "	     VALUES	                             " & vbNewLine _
                                                & "	           (@NRS_BR_CD	                 " & vbNewLine _
                                                & "	           ,@UNSO_NO_L                   " & vbNewLine _
                                                & "	           ,@KANRI_NO_L	                 " & vbNewLine _
                                                & "	           ,@KANRI_NO_M	                 " & vbNewLine _
                                                & "	           ,@MOTO_DATA_KB_NM	         " & vbNewLine _
                                                & "	           ,@KIKEN_START_DATA            " & vbNewLine _
                                                & "	           ,@GOODS_CD_NRS	             " & vbNewLine _
                                                & "	           ,@KITAKU_GOODS_UP	         " & vbNewLine _
                                                & "	           ,@ORIG_CD	                 " & vbNewLine _
                                                & "	           ,@ORIG_NM	                 " & vbNewLine _
                                                & "	           ,@ORIG_ZIP	                 " & vbNewLine _
                                                & "	           ,@ORIG_AD_1	                 " & vbNewLine _
                                                & "	           ,@ORIG_AD_2	                 " & vbNewLine _
                                                & "	           ,@ORIG_AD_3	                 " & vbNewLine _
                                                & "	           ,@DEST_CD	                 " & vbNewLine _
                                                & "	           ,@DEST_NM	                 " & vbNewLine _
                                                & "	           ,@DEST_ZIP	                 " & vbNewLine _
                                                & "	           ,@DEST_AD_1	                 " & vbNewLine _
                                                & "	           ,@DEST_AD_2	                 " & vbNewLine _
                                                & "	           ,@DEST_AD_3	                 " & vbNewLine _
                                                & "	           ,@HOKEN_RYOU	                 " & vbNewLine _
                                                & "	           ,@SYS_ENT_DATE	             " & vbNewLine _
                                                & "	           ,@SYS_ENT_TIME	             " & vbNewLine _
                                                & "	           ,@SYS_ENT_PGID	             " & vbNewLine _
                                                & "	           ,@SYS_ENT_USER	             " & vbNewLine _
                                                & "	           ,@SYS_UPD_DATE	             " & vbNewLine _
                                                & "	           ,@SYS_UPD_TIME	             " & vbNewLine _
                                                & "	           ,@SYS_UPD_PGID	             " & vbNewLine _
                                                & "	           ,@SYS_UPD_USER	             " & vbNewLine _
                                                & "	           ,@SYS_DEL_FLG)	             " & vbNewLine

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
            Dim inTbl As DataTable = ds.Tables("LMF690IN")

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(0)

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

        'SQL作成
        If Me._Row.Item("MOTO_DATA_KB").ToString = "出荷" Then
            Me._StrSql.Append(LMF690DAC.SQL_SELECT_MPrt_OUTKA)      'SQL構築(帳票種別用Select句)
            Me._StrSql.Append(LMF690DAC.SQL_FROM_OUTKA)             'SQL構築(データ抽出用From句)
            Call Me.setIndataParameterOUTKA(Me._Row)               '条件設定

        ElseIf Me._Row.Item("MOTO_DATA_KB").ToString = "入荷" Then
            Me._StrSql.Append(LMF690DAC.SQL_SELECT_MPrt_INKA)      'SQL構築(帳票種別用Select句)
            Me._StrSql.Append(LMF690DAC.SQL_FROM_INKA)             'SQL構築(データ抽出用From句)
            Call Me.setIndataParameterINKA(Me._Row)               '条件設定


        ElseIf Me._Row.Item("MOTO_DATA_KB").ToString = "運送" Then

            Me._StrSql.Append(LMF690DAC.SQL_SELECT_MPrt_UNSO)      'SQL構築(帳票種別用Select句)
            Me._StrSql.Append(LMF690DAC.SQL_FROM_UNSO)             'SQL構築(データ抽出用From句)
            Call Me.setIndataParameterUNSO(Me._Row)
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF690DAC", "SelectMPrt", cmd)

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
            Dim inTbl As DataTable = ds.Tables("LMF690IN")

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(0)

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQL作成
            If Me._Row.Item("MOTO_DATA_KB").ToString = "出荷" Then
                Me._StrSql.Append(LMF690DAC.SQL_SELECT_DATA_OUTKA)      'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMF690DAC.SQL_FROM_OUTKA)             'SQL構築(データ抽出用From句)
            Call Me.setIndataParameterOUTKA(Me._Row)                         '条件設定
            Me._StrSql.Append(LMF690DAC.SQL_ORDER_BY_OUTKA)               'SQL構築(データ抽出用ORDER BY句)

        ElseIf Me._Row.Item("MOTO_DATA_KB").ToString = "入荷" Then
            Me._StrSql.Append(LMF690DAC.SQL_SELECT_DATA_INKA)      'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMF690DAC.SQL_FROM_INKA)             'SQL構築(データ抽出用From句)
            Call Me.setIndataParameterINKA(Me._Row)                         '条件設定
            Me._StrSql.Append(LMF690DAC.SQL_ORDER_BY_INKA)               'SQL構築(データ抽出用ORDER BY句)


        ElseIf Me._Row.Item("MOTO_DATA_KB").ToString = "運送" Then

            Me._StrSql.Append(LMF690DAC.SQL_SELECT_DATA_UNSO)      'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMF690DAC.SQL_FROM_UNSO)             'SQL構築(データ抽出用From句)
            Call Me.setIndataParameterUNSO(Me._Row)                         '条件設定
            Me._StrSql.Append(LMF690DAC.SQL_ORDER_BY_UNSO)               'SQL構築(データ抽出用ORDER BY句)
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF690DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("OUTKA_TTL_QT", "OUTKA_TTL_QT")
        map.Add("KITAKU_GOODS_UP", "KITAKU_GOODS_UP")
        map.Add("HOKENRITSU", "HOKENRITSU")
        map.Add("HOKENRYO", "HOKENRYO")
        map.Add("WH_NM", "WH_NM")
        map.Add("BRAD_1", "BRAD_1")
        map.Add("DEST_ADD", "DEST_ADD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("REMARK", "REMARK")
        map.Add("KANRI_NO_L", "KANRI_NO_L")
        map.Add("KANRI_NO_M", "KANRI_NO_M")
        map.Add("KITAKU_GOODS_UPNM", "KITAKU_GOODS_UPNM")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("ORIG_CD", "ORIG_CD")
        map.Add("ORIG_NM", "ORIG_NM")
        map.Add("ORIG_ZIP", "ORIG_ZIP")
        map.Add("ORIG_AD_1", "ORIG_AD_1")
        map.Add("ORIG_AD_2", "ORIG_AD_2")
        map.Add("ORIG_AD_3", "ORIG_AD_3")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF690OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setIndataParameterOUTKA(ByVal dr As DataRow)

        With dr

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("KANRI_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.VARCHAR))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_CHK", .Item("OUTKA_NO_LUNSO_TEHAI_CHK").ToString(), DBDataType.CHAR))


        End With
    End Sub

    Private Sub setIndataParameterINKA(ByVal dr As DataRow)

        With dr

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("KANRI_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.VARCHAR))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_CHK", .Item("UNSO_TEHAI_CHK").ToString(), DBDataType.CHAR))

        End With

    End Sub

    Private Sub setIndataParameterUNSO(ByVal dr As DataRow)

        With dr

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@unso_no_l", .Item("KANRI_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.VARCHAR))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_CHK", .Item("UNSO_TEHAI_CHK").ToString(), DBDataType.CHAR))

        End With

    End Sub


    Private Sub setIndataParameterUNSO_HOKEN_DEL(ByVal dr As DataRow)

        With dr

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetDataInsertParameter(ByVal prmList As ArrayList) As String()


        '更新日時
        Dim sysDateTime As String() = Me.SetSysdataParameter(prmList)

        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", sysDateTime(0), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", sysDateTime(1), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        Return sysDateTime

    End Function

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetSysdataParameter(ByVal prmList As ArrayList) As String()

        '更新日時
        Dim sysDateTime As String() = New String() {MyBase.GetSystemDate(), MyBase.GetSystemTime()}

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

        Return sysDateTime


    End Function
    ''' <summary>
    ''' 運送保険の更新パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetUnsohHokanComParameter(ByVal prmList As ArrayList, ByVal conditionRow As DataRow)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_NO_L", .Item("KANRI_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_NO_M", .Item("KANRI_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB_NM", .Item("MOTO_DATA_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KIKEN_START_DATA", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KITAKU_GOODS_UP", .Item("KITAKU_GOODS_UP").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ORIG_CD", .Item("ORIG_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ORIG_NM", .Item("ORIG_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ORIG_ZIP", .Item("ORIG_ZIP").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ORIG_AD_1", .Item("ORIG_AD_1").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ORIG_AD_2", .Item("ORIG_AD_2").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ORIG_AD_3", .Item("ORIG_AD_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", .Item("DEST_ZIP").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", .Item("DEST_AD_1").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", .Item("DEST_AD_2").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", .Item("DEST_AD_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKEN_RYOU", .Item("HOKENRYO").ToString(), DBDataType.NUMERIC))


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

#Region "運送保険"

    ''' <summary>
    ''' 運送保険削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送保険削除QLの構築・発行</remarks>
    Private Function DeleteUnsoHoken(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF690OUT")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF690DAC.SQL_DELETE_UNSO_HOKEN _
                                                                       , inTbl.Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            'Me._SqlPrmList.Clear()
            '条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.setIndataParameterUNSO_HOKEN_DEL(Me._Row)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF690DAC", "DeleteUnsoHoken", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function


    ''' <summary>
    ''' 運送保険テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送保険テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsrtUnsoHoken(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF690OUT")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF690DAC.SQL_INSERT_UNSO_HOKEN _
                                                                       , inTbl.Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList.Clear()
            '条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetUnsohHokanComParameter(Me._SqlPrmList, Me._Row)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF690DAC", "InsrtUnsoHoken", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function
#End Region
#End Region

End Class

