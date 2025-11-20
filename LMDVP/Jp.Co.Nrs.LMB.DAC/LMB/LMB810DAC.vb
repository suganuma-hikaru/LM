' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷
'  プログラムID     :  LMB810    : 現場作業指示
'  作  成  者       :  [HOJO]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports System.Reflection

''' <summary>
''' LMB810DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB810DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "テーブル名"

    Public Class TABLE_NM
        Public Const LMB810IN As String = "LMB810IN"
        Public Const LMB810IN_SAGYO As String = "LMB810IN_TB_SAGYO"
        Public Const LMB810OUT_KENPIN_HEAD As String = "LMB810OUT_TB_KENPIN_HEAD"
        Public Const LMB810OUT_KENPIN_DTL As String = "LMB810OUT_TB_KENPIN_DTL"
        Public Const LMB810CNT As String = "LMB810CNT"
        Public Const LMB810CHECK As String = "LMB810CHECK"
        Public Const LMB810OUT_UPD_RESULTS As String = "LMB810OUT_UPD_RESULTS"
    End Class

#End Region

#Region "ファンクション名"

    Public Class FUNCTION_NM
        Public Const KENPIN_SELECT_HEAD As String = "SelectKenpinHead"
        Public Const KENPIN_INSERT_HEAD As String = "InsertKenpinHead"
        Public Const KENPIN_INSERT_DTL As String = "InsertKenpinDtl"
        Public Const KENPIN_CANCEL As String = "UpdateKenpinCancel"
        Public Const KENPIN_DELETE As String = "UpdateKenpinDelete"
        Public Const SAGYO_SELECT_LMS_DATA As String = "SelectLMSSagyoData"
        Public Const SAGYO_INSERT As String = "InsertSagyo"
        Public Const UPDATE_WH_STATUS As String = "UpdateWHSagyoShijiStatus"
        Public Const CHECK_TABLET_USE As String = "CheckTabletUse"
        Public Const CHECK_HAITA As String = "CheckHaita"
        Public Const CHECK_INKA_DATA As String = "SelectInkaCheckData"
        Public Const FILE_INSERT As String = "InsertFileData"
        Public Const INKA_SELECT_UPD_RESULTS As String = "SelectInkaLastUpdResults"
    End Class

#End Region

#Region "Select"

#Region "入荷作業登録用データ取得"
    Private Const SQL_SELECT_SAGYO_DATA As String _
    = " --大 KOSU_BAI = 01                                                                            " & vbNewLine _
    & "  SELECT                                                                                       " & vbNewLine _
    & "   SAGYO.NRS_BR_CD                                           AS NRS_BR_CD                      " & vbNewLine _
    & "  ,SAGYO.SAGYO_REC_NO                                        AS SAGYO_REC_NO                   " & vbNewLine _
    & "  ,INKAL.INKA_NO_L                                           AS INKA_NO_L                      " & vbNewLine _
    & "  ,'000'                                                     AS INKA_NO_M                      " & vbNewLine _
    & "  ,'000'                                                     AS INKA_NO_S                      " & vbNewLine _
    & "  ,ISNULL((SELECT MAX(IN_KENPIN_LOC_SEQ) FROM $LM_TRN$..TB_KENPIN_HEAD                         " & vbNewLine _
    & "          WHERE NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine _
    & "          AND   INKA_NO_L = @INKA_NO_L                                                         " & vbNewLine _
    & "          GROUP BY NRS_BR_CD,INKA_NO_L)                                                        " & vbNewLine _
    & "          ,0)+1                                              AS WORK_SEQ                       " & vbNewLine _
    & "  ,'00'                                                      AS SAGYO_STATE_KB                 " & vbNewLine _
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
    & " FROM                                                                                          " & vbNewLine _
    & " $LM_TRN$..E_SAGYO  SAGYO                                                                      " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_SAGYO    MSAGYO                                                         " & vbNewLine _
    & " ON     SAGYO.NRS_BR_CD = MSAGYO.NRS_BR_CD                                                     " & vbNewLine _
    & " AND    SAGYO.SAGYO_CD = MSAGYO.SAGYO_CD                                                       " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..B_INKA_L INKAL                                                            " & vbNewLine _
    & " ON     INKAL.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    INKAL.INKA_NO_L = LEFT(SAGYO.INOUTKA_NO_LM, 9)                                         " & vbNewLine _
    & " AND    INKAL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
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
    & " AND LEFT(SAGYO.INOUTKA_NO_LM, 9) = @INKA_NO_L                                                 " & vbNewLine _
    & " AND MSAGYO.KOSU_BAI              = '01'                                                       " & vbNewLine _
    & " AND MSAGYO.WH_SAGYO_YN           = '01'                                                       " & vbNewLine _
    & " AND SAGYO.SYS_DEL_FLG            = '0'                                                        " & vbNewLine _
    & " AND SAGYO.IOZS_KB                = '10'                                                       " & vbNewLine _
    & "                                                                                               " & vbNewLine _
    & "  --中 KOSU_BAI = 01                                                                           " & vbNewLine _
    & "  UNION ALL                                                                                    " & vbNewLine _
    & "  SELECT                                                                                       " & vbNewLine _
    & "   SAGYO.NRS_BR_CD                                           AS NRS_BR_CD                      " & vbNewLine _
    & "  ,SAGYO.SAGYO_REC_NO                                        AS SAGYO_REC_NO                   " & vbNewLine _
    & "  ,INKAL.INKA_NO_L                                           AS INKA_NO_L                      " & vbNewLine _
    & "  ,INKAM.INKA_NO_M                                           AS INKA_NO_M                      " & vbNewLine _
    & "  ,'000'                                                     AS INKA_NO_S                      " & vbNewLine _
    & "  ,ISNULL((SELECT MAX(IN_KENPIN_LOC_SEQ) FROM $LM_TRN$..TB_KENPIN_HEAD                         " & vbNewLine _
    & "          WHERE NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine _
    & "          AND   INKA_NO_L = @INKA_NO_L                                                         " & vbNewLine _
    & "          GROUP BY NRS_BR_CD,INKA_NO_L)                                                        " & vbNewLine _
    & "          ,0)+1                                              AS WORK_SEQ                       " & vbNewLine _
    & "  ,'00'                                                      AS SAGYO_STATE_KB                 " & vbNewLine _
    & "  ,SAGYO.WH_CD                                               AS WH_CD                          " & vbNewLine _
    & "  ,GOODS.GOODS_CD_NRS                                        AS GOODS_CD_NRS                   " & vbNewLine _
    & "  ,GOODS.GOODS_NM_1                                          AS GOODS_NM_NRS                   " & vbNewLine _
    & "  ,GOODS.STD_IRIME_NB                                        AS IRIME                          " & vbNewLine _
    & "  ,GOODS.STD_IRIME_UT                                             AS IRIME_UT                  " & vbNewLine _
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
    & " FROM                                                                                          " & vbNewLine _
    & " $LM_TRN$..E_SAGYO  SAGYO                                                                      " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_SAGYO    MSAGYO                                                         " & vbNewLine _
    & " ON     SAGYO.NRS_BR_CD = MSAGYO.NRS_BR_CD                                                     " & vbNewLine _
    & " AND    SAGYO.SAGYO_CD = MSAGYO.SAGYO_CD                                                       " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..B_INKA_L INKAL                                                            " & vbNewLine _
    & " ON     INKAL.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    INKAL.INKA_NO_L = LEFT(SAGYO.INOUTKA_NO_LM, 9)                                         " & vbNewLine _
    & " AND    INKAL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                                            " & vbNewLine _
    & " ON     INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    INKAM.INKA_NO_L = INKAL.INKA_NO_L                                                      " & vbNewLine _
    & " AND    INKAM.INKA_NO_M = RIGHT(SAGYO.INOUTKA_NO_LM, 3)                                        " & vbNewLine _
    & " AND    INKAM.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                                             " & vbNewLine _
    & " ON     GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_CUST MCUST                                                              " & vbNewLine _
    & " ON     MCUST.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_L = SAGYO.CUST_CD_L                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_M = SAGYO.CUST_CD_M                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_S = '00'                                                                 " & vbNewLine _
    & " AND    MCUST.CUST_CD_SS = '00'                                                                " & vbNewLine _
    & " WHERE                                                                                         " & vbNewLine _
    & "     SAGYO.NRS_BR_CD              = @NRS_BR_CD                                                 " & vbNewLine _
    & " AND LEFT(SAGYO.INOUTKA_NO_LM, 9) = @INKA_NO_L                                                 " & vbNewLine _
    & " AND MSAGYO.KOSU_BAI              = '01'                                                       " & vbNewLine _
    & " AND MSAGYO.WH_SAGYO_YN           = '01'                                                       " & vbNewLine _
    & " AND SAGYO.SYS_DEL_FLG            = '0'                                                        " & vbNewLine _
    & " AND SAGYO.IOZS_KB                = '11'                                                       " & vbNewLine _
    & "  --大 KOSU_BAI = 02                                                                           " & vbNewLine _
    & " UNION ALL                                                                                     " & vbNewLine _
    & " SELECT                                                                                        " & vbNewLine _
    & "   SAGYO.NRS_BR_CD                                           AS NRS_BR_CD                      " & vbNewLine _
    & "  ,SAGYO.SAGYO_REC_NO                                        AS SAGYO_REC_NO                   " & vbNewLine _
    & "  ,INKAL.INKA_NO_L                                          AS INKA_NO_L                       " & vbNewLine _
    & "  ,INKAM.INKA_NO_M                                          AS INKA_NO_M                       " & vbNewLine _
    & "  ,INKAS.INKA_NO_S                                          AS INKA_NO_S                       " & vbNewLine _
    & "  ,ISNULL((SELECT MAX(IN_KENPIN_LOC_SEQ) FROM $LM_TRN$..TB_KENPIN_HEAD                         " & vbNewLine _
    & "          WHERE NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine _
    & "          AND   INKA_NO_L = @INKA_NO_L                                                         " & vbNewLine _
    & "          GROUP BY NRS_BR_CD,INKA_NO_L)                                                        " & vbNewLine _
    & "          ,0)+1                                              AS WORK_SEQ                       " & vbNewLine _
    & "  ,CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                            " & vbNewLine _
    & "        WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                            " & vbNewLine _
    & "        ELSE                                   '00'                                            " & vbNewLine _
    & "   END                                                       AS SAGYO_STATE_KB                 " & vbNewLine _
    & "  ,SAGYO.WH_CD                                               AS WH_CD                          " & vbNewLine _
    & "  ,GOODS.GOODS_CD_NRS                                        AS GOODS_CD_NRS                   " & vbNewLine _
    & "  ,GOODS.GOODS_NM_1                                          AS GOODS_NM_NRS                   " & vbNewLine _
    & "  ,INKAS.IRIME                                               AS IRIME                          " & vbNewLine _
    & "  ,GOODS.STD_IRIME_UT                                        AS IRIME_UT                       " & vbNewLine _
    & "  ,GOODS.PKG_NB                                              AS PKG_NB                         " & vbNewLine _
    & "  ,GOODS.PKG_UT                                              AS PKG_UT                         " & vbNewLine _
    & "  ,INKAS.LOT_NO                                              AS LOT_NO                         " & vbNewLine _
    & "  ,INKAS.TOU_NO                                              AS TOU_NO                         " & vbNewLine _
    & "  ,INKAS.SITU_NO                                             AS SITU_NO                        " & vbNewLine _
    & "  ,INKAS.ZONE_CD                                             AS ZONE_CD                        " & vbNewLine _
    & "  ,INKAS.LOCA                                                AS LOCA                           " & vbNewLine _
    & "  ,SAGYO.SAGYO_CD                                            AS SAGYO_CD                       " & vbNewLine _
    & "  ,MSAGYO.WH_SAGYO_NM                                        AS SAGYO_NM                       " & vbNewLine _
    & "  ,SAGYO.INV_TANI                                            AS INV_TANI                       " & vbNewLine _
    & "  ,MSAGYO.KOSU_BAI                                           AS KOSU_BAI                       " & vbNewLine _
    & "  ,INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU                   AS SAGYO_NB                       " & vbNewLine _
    & "  ,SAGYO.REMARK_SIJI                                         AS REMARK                         " & vbNewLine _
    & "  ,TOUSITU.JISYATASYA_KB                                     AS JISYATASYA_KB                  " & vbNewLine _
    & " FROM                                                                                          " & vbNewLine _
    & " $LM_TRN$..E_SAGYO  SAGYO                                                                      " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_SAGYO    MSAGYO                                                         " & vbNewLine _
    & " ON     SAGYO.NRS_BR_CD = MSAGYO.NRS_BR_CD                                                     " & vbNewLine _
    & " AND    SAGYO.SAGYO_CD = MSAGYO.SAGYO_CD                                                       " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..B_INKA_L  INKAL                                                           " & vbNewLine _
    & " ON     INKAL.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    INKAL.INKA_NO_L = LEFT(SAGYO.INOUTKA_NO_LM, 9)                                         " & vbNewLine _
    & " AND    INKAL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                                            " & vbNewLine _
    & " ON     INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    INKAM.INKA_NO_L = INKAL.INKA_NO_L                                                      " & vbNewLine _
    & " AND    INKAM.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..B_INKA_S INKAS                                                            " & vbNewLine _
    & " ON     INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    INKAS.INKA_NO_L = INKAM.INKA_NO_L                                                      " & vbNewLine _
    & " AND    INKAS.INKA_NO_M = INKAM.INKA_NO_M                                                      " & vbNewLine _
    & " AND    INKAS.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                                             " & vbNewLine _
    & " ON     GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_CUST MCUST                                                              " & vbNewLine _
    & " ON     MCUST.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND     MCUST.CUST_CD_L = SAGYO.CUST_CD_L                                                     " & vbNewLine _
    & " AND    MCUST.CUST_CD_M = SAGYO.CUST_CD_M                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_S = '00'                                                                 " & vbNewLine _
    & " AND    MCUST.CUST_CD_SS = '00'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_TOU_SITU TOUSITU                                                        " & vbNewLine _
    & " ON  TOUSITU.WH_CD   = INKAL.WH_CD                                                             " & vbNewLine _
    & " AND TOUSITU.TOU_NO  = INKAS.TOU_NO                                                            " & vbNewLine _
    & " AND TOUSITU.SITU_NO = INKAS.SITU_NO                                                           " & vbNewLine _
    & " WHERE                                                                                         " & vbNewLine _
    & "     SAGYO.NRS_BR_CD              = @NRS_BR_CD                                                 " & vbNewLine _
    & " AND LEFT(SAGYO.INOUTKA_NO_LM,9)  = @INKA_NO_L                                                 " & vbNewLine _
    & " AND MSAGYO.KOSU_BAI              = '02'                                                       " & vbNewLine _
    & " AND MSAGYO.WH_SAGYO_YN           = '01'                                                       " & vbNewLine _
    & " AND SAGYO.SYS_DEL_FLG            = '0'                                                        " & vbNewLine _
    & " AND SAGYO.IOZS_KB                = '10'                                                       " & vbNewLine _
    & "  --中 KOSU_BAI = 02                                                                           " & vbNewLine _
    & " UNION ALL                                                                                     " & vbNewLine _
    & " SELECT                                                                                        " & vbNewLine _
    & "   SAGYO.NRS_BR_CD                                           AS NRS_BR_CD                      " & vbNewLine _
    & "  ,SAGYO.SAGYO_REC_NO                                        AS SAGYO_REC_NO                   " & vbNewLine _
    & "  ,INKAL.INKA_NO_L                                          AS INKA_NO_L                       " & vbNewLine _
    & "  ,INKAM.INKA_NO_M                                          AS INKA_NO_M                       " & vbNewLine _
    & "  ,INKAS.INKA_NO_S                                          AS INKA_NO_S                       " & vbNewLine _
    & "  ,ISNULL((SELECT MAX(IN_KENPIN_LOC_SEQ) FROM $LM_TRN$..TB_KENPIN_HEAD                         " & vbNewLine _
    & "          WHERE NRS_BR_CD = @NRS_BR_CD                                                         " & vbNewLine _
    & "          AND   INKA_NO_L = @INKA_NO_L                                                         " & vbNewLine _
    & "          GROUP BY NRS_BR_CD,INKA_NO_L)                                                        " & vbNewLine _
    & "          ,0)+1                                              AS WORK_SEQ                       " & vbNewLine _
    & "  ,CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                            " & vbNewLine _
    & "        WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                            " & vbNewLine _
    & "        ELSE                                   '00'                                            " & vbNewLine _
    & "   END                                                       AS SAGYO_STATE_KB                 " & vbNewLine _
    & "  ,SAGYO.WH_CD                                               AS WH_CD                          " & vbNewLine _
    & "  ,GOODS.GOODS_CD_NRS                                        AS GOODS_CD_NRS                   " & vbNewLine _
    & "  ,GOODS.GOODS_NM_1                                          AS GOODS_NM_NRS                   " & vbNewLine _
    & "  ,INKAS.IRIME                                               AS IRIME                          " & vbNewLine _
    & "  ,GOODS.STD_IRIME_UT                                        AS IRIME_UT                       " & vbNewLine _
    & "  ,GOODS.PKG_NB                                              AS PKG_NB                         " & vbNewLine _
    & "  ,GOODS.PKG_UT                                              AS PKG_UT                         " & vbNewLine _
    & "  ,INKAS.LOT_NO                                              AS LOT_NO                         " & vbNewLine _
    & "  ,INKAS.TOU_NO                                              AS TOU_NO                         " & vbNewLine _
    & "  ,INKAS.SITU_NO                                             AS SITU_NO                        " & vbNewLine _
    & "  ,INKAS.ZONE_CD                                             AS ZONE_CD                        " & vbNewLine _
    & "  ,INKAS.LOCA                                                AS LOCA                           " & vbNewLine _
    & "  ,SAGYO.SAGYO_CD                                            AS SAGYO_CD                       " & vbNewLine _
    & "  ,MSAGYO.WH_SAGYO_NM                                        AS SAGYO_NM                       " & vbNewLine _
    & "  ,SAGYO.INV_TANI                                            AS INV_TANI                       " & vbNewLine _
    & "  ,MSAGYO.KOSU_BAI                                           AS KOSU_BAI                       " & vbNewLine _
    & "  ,INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU                   AS SAGYO_NB                       " & vbNewLine _
    & "  ,SAGYO.REMARK_SIJI                                         AS REMARK                         " & vbNewLine _
    & "  ,TOUSITU.JISYATASYA_KB                                     AS JISYATASYA_KB                  " & vbNewLine _
    & " FROM                                                                                          " & vbNewLine _
    & " $LM_TRN$..E_SAGYO  SAGYO                                                                      " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_SAGYO    MSAGYO                                                         " & vbNewLine _
    & " ON     SAGYO.NRS_BR_CD = MSAGYO.NRS_BR_CD                                                     " & vbNewLine _
    & " AND    SAGYO.SAGYO_CD = MSAGYO.SAGYO_CD                                                       " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..B_INKA_L  INKAL                                                           " & vbNewLine _
    & " ON     INKAL.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    INKAL.INKA_NO_L = LEFT(SAGYO.INOUTKA_NO_LM, 9)                                         " & vbNewLine _
    & " AND    INKAL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                                            " & vbNewLine _
    & " ON     INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    INKAM.INKA_NO_L = INKAL.INKA_NO_L                                                      " & vbNewLine _
    & " AND    INKAM.INKA_NO_M = RIGHT(SAGYO.INOUTKA_NO_LM, 3)                                        " & vbNewLine _
    & " AND    INKAM.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..B_INKA_S INKAS                                                            " & vbNewLine _
    & " ON     INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    INKAS.INKA_NO_L = INKAM.INKA_NO_L                                                      " & vbNewLine _
    & " AND    INKAS.INKA_NO_M = INKAM.INKA_NO_M                                                      " & vbNewLine _
    & " AND    INKAS.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                                             " & vbNewLine _
    & " ON      GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                                     " & vbNewLine _
    & " AND     GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                               " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_CUST MCUST                                                              " & vbNewLine _
    & " ON     MCUST.NRS_BR_CD = SAGYO.NRS_BR_CD                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_L = SAGYO.CUST_CD_L                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_M = SAGYO.CUST_CD_M                                                      " & vbNewLine _
    & " AND    MCUST.CUST_CD_S = '00'                                                                 " & vbNewLine _
    & " AND    MCUST.CUST_CD_SS = '00'                                                                " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_TOU_SITU TOUSITU                                                        " & vbNewLine _
    & " ON     TOUSITU.WH_CD   = INKAL.WH_CD                                                          " & vbNewLine _
    & " AND    TOUSITU.TOU_NO  = INKAS.TOU_NO                                                         " & vbNewLine _
    & " AND    TOUSITU.SITU_NO = INKAS.SITU_NO                                                        " & vbNewLine _
    & " WHERE                                                                                         " & vbNewLine _
    & "     SAGYO.NRS_BR_CD              = @NRS_BR_CD                                                 " & vbNewLine _
    & " AND LEFT(SAGYO.INOUTKA_NO_LM,9)  = @INKA_NO_L                                                 " & vbNewLine _
    & " AND MSAGYO.KOSU_BAI              = '02'                                                       " & vbNewLine _
    & " AND MSAGYO.WH_SAGYO_YN           = '01'                                                       " & vbNewLine _
    & " AND SAGYO.SYS_DEL_FLG            = '0'                                                        " & vbNewLine _
    & " AND SAGYO.IOZS_KB                = '11'                                                       " & vbNewLine

#End Region

#Region "入荷検品ヘッダ取得"
    ''' <summary>
    ''' 入荷検品検品ヘッダ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TB_KENPIN_HEAD As String _
    = " SELECT                                                                  " & vbNewLine _
    & "     MAIN.NRS_BR_CD                                                      " & vbNewLine _
    & "   , MAIN.INKA_NO_L                                                      " & vbNewLine _
    & "   , MAIN.IN_KENPIN_LOC_SEQ                                              " & vbNewLine _
    & "   , MAIN.IN_KENPIN_LOC_STATE_KB                                         " & vbNewLine _
    & "   , MAIN.WORK_STATE_KB                                                  " & vbNewLine _
    & "   , MAIN.CANCEL_FLG                                                     " & vbNewLine _
    & "   , CASE WHEN @PROC_TYPE = '00'  THEN '01'                              " & vbNewLine _
    & "          WHEN @PROC_TYPE = '01'  THEN '02'                              " & vbNewLine _
    & "          WHEN @PROC_TYPE = '02'  THEN '02'                              " & vbNewLine _
    & "          ELSE ''                                                        " & vbNewLine _
    & "     END                               AS  CANCEL_TYPE                   " & vbNewLine _
    & "   , MAIN.WH_CD                                                          " & vbNewLine _
    & "   , MAIN.CUST_CD_L                                                      " & vbNewLine _
    & "   , MAIN.CUST_CD_M                                                      " & vbNewLine _
    & "   , MAIN.CUST_NM_L                                                      " & vbNewLine _
    & "   , MAIN.CUST_NM_M                                                      " & vbNewLine _
    & "   , MAIN.BUYER_ORD_NO_L                                                 " & vbNewLine _
    & "   , MAIN.OUTKA_FROM_ORD_NO_L                                            " & vbNewLine _
    & "   , MAIN.UNSO_CD                                                        " & vbNewLine _
    & "   , MAIN.UNSO_NM                                                        " & vbNewLine _
    & "   , MAIN.UNSO_BR_CD                                                     " & vbNewLine _
    & "   , MAIN.UNSO_BR_NM                                                     " & vbNewLine _
    & "   , MAIN.INKA_DATE                                                      " & vbNewLine _
    & "   , MAIN.REMARK                                                         " & vbNewLine _
    & "   , MAIN.REMARK_OUT                                                     " & vbNewLine _
    & "   , MAIN.REMARK_KENPIN_CHK_FLG                                          " & vbNewLine _
    & "   , MAIN.REMARK_LOCA_CHK_FLG                                            " & vbNewLine _
    & "   , MAIN.SYS_ENT_DATE                                                   " & vbNewLine _
    & "   , MAIN.SYS_ENT_TIME                                                   " & vbNewLine _
    & "   , MAIN.SYS_ENT_PGID                                                   " & vbNewLine _
    & "   , MAIN.SYS_ENT_USER                                                   " & vbNewLine _
    & "   , MAIN.SYS_UPD_DATE                                                   " & vbNewLine _
    & "   , MAIN.SYS_UPD_TIME                                                   " & vbNewLine _
    & "   , MAIN.SYS_UPD_PGID                                                   " & vbNewLine _
    & "   , MAIN.SYS_UPD_USER                                                   " & vbNewLine _
    & "   , MAIN.SYS_DEL_FLG                                                    " & vbNewLine _
    & " FROM $LM_TRN$..TB_KENPIN_HEAD MAIN                                      " & vbNewLine _
    & " INNER JOIN (                                                            " & vbNewLine _
    & "     SELECT                                                              " & vbNewLine _
    & "         NRS_BR_CD                                                       " & vbNewLine _
    & "       , INKA_NO_L                                                       " & vbNewLine _
    & "       , MAX(IN_KENPIN_LOC_SEQ) AS IN_KENPIN_LOC_SEQ                     " & vbNewLine _
    & "     FROM $LM_TRN$..TB_KENPIN_HEAD                                       " & vbNewLine _
    & "     WHERE                                                               " & vbNewLine _
    & "           NRS_BR_CD = @NRS_BR_CD                                        " & vbNewLine _
    & "       AND INKA_NO_L = @INKA_NO_L                                        " & vbNewLine _
    & "       AND SYS_DEL_FLG = '0'                                             " & vbNewLine _
    & "     GROUP BY                                                            " & vbNewLine _
    & "         NRS_BR_CD                                                       " & vbNewLine _
    & "       , INKA_NO_L                                                       " & vbNewLine _
    & " ) SUB                                                                   " & vbNewLine _
    & "   ON  SUB.NRS_BR_CD  = MAIN.NRS_BR_CD                                   " & vbNewLine _
    & "   AND SUB.INKA_NO_L  = MAIN.INKA_NO_L                                   " & vbNewLine _
    & " WHERE                                                                   " & vbNewLine _
    & "       MAIN.NRS_BR_CD  = @NRS_BR_CD                                      " & vbNewLine _
    & "   AND MAIN.INKA_NO_L  = @INKA_NO_L                                      " & vbNewLine _
    & "   AND MAIN.SYS_DEL_FLG = '0'                                            " & vbNewLine

#End Region

#Region "チェック用データ取得"
    ''' <summary>
    ''' チェック用データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CHECK_DATA As String _
    = " SELECT                                                                    " & vbNewLine _
    & "  INL.NRS_BR_CD                          AS NRS_BR_CD                      " & vbNewLine _
    & " ,INL.INKA_NO_L                          AS INKA_NO_L                      " & vbNewLine _
    & " ,INL.INKA_STATE_KB                      AS INKA_STATE_KB                  " & vbNewLine _
    & " ,INL.WH_TAB_YN                          AS WH_TAB_YN                      " & vbNewLine _
    & " ,INL.WH_TAB_STATUS                      AS WH_TAB_STATUS                  " & vbNewLine _
    & " FROM $LM_TRN$..B_INKA_L INL                                               " & vbNewLine _
    & " WHERE                                                                     " & vbNewLine _
    & "     INL.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
    & " AND INL.INKA_NO_L = @INKA_NO_L                                            " & vbNewLine

#End Region

#End Region

#Region "Insert"

#Region "入荷作業登録"
    Private Const SQL_INSERT_TB_SAGYO As String _
    = " INSERT INTO $LM_TRN$..TB_SAGYO                                                               " & vbNewLine _
    & " (                                                                                            " & vbNewLine _
    & "   NRS_BR_CD                                                                                  " & vbNewLine _
    & "   , SAGYO_REC_NO                                                                             " & vbNewLine _
    & "   , INKA_NO_L                                                                                " & vbNewLine _
    & "   , INKA_NO_M                                                                                " & vbNewLine _
    & "   , INKA_NO_S                                                                                " & vbNewLine _
    & "   , WORK_SEQ                                                                                 " & vbNewLine _
    & "   , SAGYO_STATE_KB                                                                           " & vbNewLine _
    & "   , WH_CD                                                                                    " & vbNewLine _
    & "   , GOODS_CD_NRS                                                                             " & vbNewLine _
    & "   , GOODS_NM_NRS                                                                             " & vbNewLine _
    & "   , IRIME                                                                                    " & vbNewLine _
    & "   , IRIME_UT                                                                                 " & vbNewLine _
    & "   , PKG_NB                                                                                   " & vbNewLine _
    & "   , PKG_UT                                                                                   " & vbNewLine _
    & "   , LOT_NO                                                                                   " & vbNewLine _
    & "   , TOU_NO                                                                                   " & vbNewLine _
    & "   , SITU_NO                                                                                  " & vbNewLine _
    & "   , ZONE_CD                                                                                  " & vbNewLine _
    & "   , LOCA                                                                                     " & vbNewLine _
    & "   , SAGYO_CD                                                                                 " & vbNewLine _
    & "   , SAGYO_NM                                                                                 " & vbNewLine _
    & "   , INV_TANI                                                                                 " & vbNewLine _
    & "   , KOSU_BAI                                                                                 " & vbNewLine _
    & "   , SAGYO_NB                                                                                 " & vbNewLine _
    & "   , REMARK                                                                                   " & vbNewLine _
    & "   , JISYATASYA_KB                                                                            " & vbNewLine _
    & "   , SYS_ENT_DATE                                                                             " & vbNewLine _
    & "   , SYS_ENT_TIME                                                                             " & vbNewLine _
    & "   , SYS_ENT_PGID                                                                             " & vbNewLine _
    & "   , SYS_ENT_USER                                                                             " & vbNewLine _
    & "   , SYS_UPD_DATE                                                                             " & vbNewLine _
    & "   , SYS_UPD_TIME                                                                             " & vbNewLine _
    & "   , SYS_UPD_PGID                                                                             " & vbNewLine _
    & "   , SYS_UPD_USER                                                                             " & vbNewLine _
    & "   , SYS_DEL_FLG                                                                              " & vbNewLine _
    & " )                                                                                            " & vbNewLine _
    & " VALUES (                                                                                     " & vbNewLine _
    & "     @NRS_BR_CD                                                                               " & vbNewLine _
    & "   , @SAGYO_REC_NO                                                                            " & vbNewLine _
    & "   , @INKA_NO_L                                                                               " & vbNewLine _
    & "   , @INKA_NO_M                                                                               " & vbNewLine _
    & "   , @INKA_NO_S                                                                               " & vbNewLine _
    & "   , @WORK_SEQ                                                                                " & vbNewLine _
    & "   , @SAGYO_STATE_KB                                                                          " & vbNewLine _
    & "   , @WH_CD                                                                                   " & vbNewLine _
    & "   , @GOODS_CD_NRS                                                                            " & vbNewLine _
    & "   , @GOODS_NM_NRS                                                                            " & vbNewLine _
    & "   , @IRIME                                                                                   " & vbNewLine _
    & "   , @IRIME_UT                                                                                " & vbNewLine _
    & "   , @PKG_NB                                                                                  " & vbNewLine _
    & "   , @PKG_UT                                                                                  " & vbNewLine _
    & "   , @LOT_NO                                                                                  " & vbNewLine _
    & "   , @TOU_NO                                                                                  " & vbNewLine _
    & "   , @SITU_NO                                                                                 " & vbNewLine _
    & "   , @ZONE_CD                                                                                 " & vbNewLine _
    & "   , @LOCA                                                                                    " & vbNewLine _
    & "   , @SAGYO_CD                                                                                " & vbNewLine _
    & "   , @SAGYO_NM                                                                                " & vbNewLine _
    & "   , @INV_TANI                                                                                " & vbNewLine _
    & "   , @KOSU_BAI                                                                                " & vbNewLine _
    & "   , @SAGYO_NB                                                                                " & vbNewLine _
    & "   , @REMARK                                                                                  " & vbNewLine _
    & "   , @JISYATASYA_KB                                                                           " & vbNewLine _
    & "   , @SYS_ENT_DATE                                                                            " & vbNewLine _
    & "   , @SYS_ENT_TIME                                                                            " & vbNewLine _
    & "   , @SYS_ENT_PGID                                                                            " & vbNewLine _
    & "   , @SYS_ENT_USER                                                                            " & vbNewLine _
    & "   , @SYS_UPD_DATE                                                                            " & vbNewLine _
    & "   , @SYS_UPD_TIME                                                                            " & vbNewLine _
    & "   , @SYS_UPD_PGID                                                                            " & vbNewLine _
    & "   , @SYS_UPD_USER                                                                            " & vbNewLine _
    & "   , @SYS_DEL_FLG                                                                             " & vbNewLine _
    & " )                                                                                            " & vbNewLine

#End Region

#Region "入荷検品ヘッダ登録"

    ''' <summary>
    ''' 入荷検品ヘッダ登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TB_KENPIN_HEAD As String _
    = "INSERT INTO $LM_TRN$..TB_KENPIN_HEAD (                                           " & vbNewLine _
    & "  NRS_BR_CD                                                                                                    " & vbNewLine _
    & ", INKA_NO_L                                                                                                    " & vbNewLine _
    & ", IN_KENPIN_LOC_SEQ                                                                                            " & vbNewLine _
    & ", IN_KENPIN_LOC_STATE_KB                                                                                       " & vbNewLine _
    & ", WORK_STATE_KB                                                                                                " & vbNewLine _
    & ", CANCEL_FLG                                                                                                   " & vbNewLine _
    & ", CANCEL_TYPE                                                                                                  " & vbNewLine _
    & ", WH_CD                                                                                                        " & vbNewLine _
    & ", CUST_CD_L                                                                                                    " & vbNewLine _
    & ", CUST_CD_M                                                                                                    " & vbNewLine _
    & ", CUST_NM_L                                                                                                    " & vbNewLine _
    & ", CUST_NM_M                                                                                                    " & vbNewLine _
    & ", BUYER_ORD_NO_L                                                                                               " & vbNewLine _
    & ", OUTKA_FROM_ORD_NO_L                                                                                          " & vbNewLine _
    & ", UNSO_CD                                                                                                      " & vbNewLine _
    & ", UNSO_NM                                                                                                      " & vbNewLine _
    & ", UNSO_BR_CD                                                                                                   " & vbNewLine _
    & ", UNSO_BR_NM                                                                                                   " & vbNewLine _
    & ", INKA_DATE                                                                                                    " & vbNewLine _
    & ", REMARK                                                                                                       " & vbNewLine _
    & ", REMARK_OUT                                                                                                   " & vbNewLine _
    & ", REMARK_KENPIN_CHK_FLG                                                                                        " & vbNewLine _
    & ", REMARK_LOCA_CHK_FLG                                                                                          " & vbNewLine _
    & ", SYS_ENT_DATE                                                                                                 " & vbNewLine _
    & ", SYS_ENT_TIME                                                                                                 " & vbNewLine _
    & ", SYS_ENT_PGID                                                                                                 " & vbNewLine _
    & ", SYS_ENT_USER                                                                                                 " & vbNewLine _
    & ", SYS_UPD_DATE                                                                                                 " & vbNewLine _
    & ", SYS_UPD_TIME                                                                                                 " & vbNewLine _
    & ", SYS_UPD_PGID                                                                                                 " & vbNewLine _
    & ", SYS_UPD_USER                                                                                                 " & vbNewLine _
    & ", SYS_DEL_FLG                                                                                                  " & vbNewLine _
    & ")                                                                                                              " & vbNewLine _
    & " SELECT                                                                                                        " & vbNewLine _
    & "  INKAL.NRS_BR_CD                                                                AS NRS_BR_CD                  " & vbNewLine _
    & " ,INKAL.INKA_NO_L                                                                AS INKA_NO_L                  " & vbNewLine _
    & " ,ISNULL((SELECT MAX(IN_KENPIN_LOC_SEQ)                                                                        " & vbNewLine _
    & "          FROM $LM_TRN$..TB_KENPIN_HEAD                                                                        " & vbNewLine _
    & "          WHERE TB_KENPIN_HEAD.NRS_BR_CD = INKAL.NRS_BR_CD                                                     " & vbNewLine _
    & "            AND TB_KENPIN_HEAD.INKA_NO_L = INKAL.INKA_NO_L                                                     " & vbNewLine _
    & "          GROUP BY TB_KENPIN_HEAD.NRS_BR_CD,TB_KENPIN_HEAD.INKA_NO_L                                           " & vbNewLine _
    & "         ),0) + 1                                                                AS IN_KENPIN_LOC_SEQ          " & vbNewLine _
    & " ,'00'                                                                           AS IN_KENPIN_LOC_STATE_KB     " & vbNewLine _
    & " ,CASE WHEN                                                                                                    " & vbNewLine _
    & "  (SELECT COUNT(*)                                                                                             " & vbNewLine _
    & "   FROM  $LM_TRN$..TB_SAGYO                                                                                    " & vbNewLine _
    & "   WHERE TB_SAGYO.NRS_BR_CD  = @NRS_BR_CD                                                                      " & vbNewLine _
    & "     AND TB_SAGYO.INKA_NO_L = @INKA_NO_L                                                                      " & vbNewLine _
    & "     AND TB_SAGYO.WORK_SEQ   = ISNULL(                                                                         " & vbNewLine _
    & " 	     (SELECT MAX(IN_KENPIN_LOC_SEQ)                                                                       " & vbNewLine _
    & "           FROM $LM_TRN$..TB_KENPIN_HEAD                                                                       " & vbNewLine _
    & "           WHERE TB_KENPIN_HEAD.NRS_BR_CD = INKAL.NRS_BR_CD                                                    " & vbNewLine _
    & "             AND TB_KENPIN_HEAD.INKA_NO_L = INKAL.INKA_NO_L                                                    " & vbNewLine _
    & "           GROUP BY TB_KENPIN_HEAD.NRS_BR_CD,TB_KENPIN_HEAD.INKA_NO_L                                          " & vbNewLine _
    & "        ),0) + 1                                                                                               " & vbNewLine _
    & "  ) > 0                                                                                                        " & vbNewLine _
    & "  THEN '01'                                                                                                    " & vbNewLine _
    & "  ELSE '00' END                                        AS WORK_STATE_KB                                        " & vbNewLine _
    & " ,'00'                                                                           AS CANCEL_FLG                 " & vbNewLine _
    & " ,'00'                                                                           AS CANCEL_TYPE                " & vbNewLine _
    & " ,INKAL.WH_CD                                                                    AS WH_CD                      " & vbNewLine _
    & " ,ISNULL(MCUST.CUST_CD_L,'')                                                     AS CUST_CD_L                  " & vbNewLine _
    & " ,ISNULL(MCUST.CUST_CD_M,'')                                                     AS CUST_CD_M                  " & vbNewLine _
    & " ,ISNULL(MCUST.CUST_NM_L,'')                                                     AS CUST_NM_L                  " & vbNewLine _
    & " ,ISNULL(MCUST.CUST_NM_M,'')                                                     AS CUST_NM_M                  " & vbNewLine _
    & " ,INKAL.BUYER_ORD_NO_L                                                           AS BUYER_ORD_NO_L             " & vbNewLine _
    & " ,INKAL.OUTKA_FROM_ORD_NO_L                                                      AS OUTKA_FROM_ORD_NO_L        " & vbNewLine _
    & " ,ISNULL(UNSO.UNSO_CD,'')                                                        AS UNSO_CD                    " & vbNewLine _
    & " ,ISNULL(MUNSOCO.UNSOCO_NM,'')                                                   AS UNSO_NM                    " & vbNewLine _
    & " ,ISNULL(UNSO.UNSO_BR_CD,'')                                                     AS UNSO_BR_CD                 " & vbNewLine _
    & " ,ISNULL(MUNSOCO.UNSOCO_BR_NM,'')                                                AS UNSO_BR_NM                 " & vbNewLine _
    & " ,INKAL.INKA_DATE                                                                AS INKA_DATE                  " & vbNewLine _
    & " ,INKAL.REMARK                                                                   AS REMARK                     " & vbNewLine _
    & " ,INKAL.REMARK_OUT                                                               AS REMARK_OUT                 " & vbNewLine _
    & " ,CASE WHEN LEN(INKAL.REMARK + INKAL.REMARK_OUT) > 0 THEN '00' ELSE '01' END     AS REMARK_KENPIN_CHK_FLG      " & vbNewLine _
    & " ,CASE WHEN LEN(INKAL.REMARK + INKAL.REMARK_OUT) > 0 THEN '00' ELSE '01' END     AS REMARK_LOCA_CHK_FLG        " & vbNewLine _
    & " ,@SYS_ENT_DATE                                                                  AS SYS_ENT_DATE               " & vbNewLine _
    & " ,@SYS_ENT_TIME                                                                  AS SYS_ENT_TIME               " & vbNewLine _
    & " ,@SYS_ENT_PGID                                                                  AS SYS_ENT_PGID               " & vbNewLine _
    & " ,@SYS_ENT_USER                                                                  AS SYS_ENT_USER               " & vbNewLine _
    & " ,@SYS_UPD_DATE                                                                  AS SYS_UPD_DATE               " & vbNewLine _
    & " ,@SYS_UPD_TIME                                                                  AS SYS_UPD_TIME               " & vbNewLine _
    & " ,@SYS_UPD_PGID                                                                  AS SYS_UPD_PGID               " & vbNewLine _
    & " ,@SYS_UPD_USER                                                                  AS SYS_UPD_USER               " & vbNewLine _
    & " ,'0'                                                                            AS SYS_DEL_FLG                " & vbNewLine _
    & " FROM $LM_TRN$..B_INKA_L INKAL                                                                                 " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_CUST MCUST                                                                              " & vbNewLine _
    & " ON  MCUST.NRS_BR_CD  = INKAL.NRS_BR_CD                                                                        " & vbNewLine _
    & " AND MCUST.CUST_CD_L  = INKAL.CUST_CD_L                                                                        " & vbNewLine _
    & " AND MCUST.CUST_CD_M  = INKAL.CUST_CD_M                                                                        " & vbNewLine _
    & " AND MCUST.CUST_CD_S  = '00'                                                                                   " & vbNewLine _
    & " AND MCUST.CUST_CD_SS = '00'                                                                                   " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSO                                                                             " & vbNewLine _
    & " ON  UNSO.NRS_BR_CD    = INKAL.NRS_BR_CD                                                                       " & vbNewLine _
    & " AND UNSO.INOUTKA_NO_L = INKAL.INKA_NO_L                                                                       " & vbNewLine _
    & " AND UNSO.MOTO_DATA_KB = '10'                                                                                  " & vbNewLine _
    & " AND UNSO.SYS_DEL_FLG  = '0'                                                                                   " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_UNSOCO MUNSOCO                                                                          " & vbNewLine _
    & " ON  MUNSOCO.NRS_BR_CD  = UNSO.NRS_BR_CD                                                                       " & vbNewLine _
    & " AND MUNSOCO.UNSOCO_CD    = UNSO.UNSO_CD                                                                       " & vbNewLine _
    & " AND MUNSOCO.UNSOCO_BR_CD = UNSO.UNSO_BR_CD                                                                    " & vbNewLine _
    & " WHERE                                                                                                         " & vbNewLine _
    & "     INKAL.NRS_BR_CD   = @NRS_BR_CD                                                                            " & vbNewLine _
    & " AND INKAL.INKA_NO_L   = @INKA_NO_L                                                                            " & vbNewLine _
    & " AND INKAL.SYS_DEL_FLG = '0'                                                                                   " & vbNewLine


#End Region

#Region "入荷検品明細登録"
    ''' <summary>
    ''' 入荷検品明細登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TB_KENPIN_DTL As String _
    = " INSERT INTO $LM_TRN$..TB_KENPIN_DTL                                                                           " & vbNewLine _
    & " (                                                                                                             " & vbNewLine _
    & "   NRS_BR_CD                                                                                                   " & vbNewLine _
    & "   , INKA_NO_L                                                                                                 " & vbNewLine _
    & "   , IN_KENPIN_LOC_SEQ                                                                                         " & vbNewLine _
    & "   , INKA_NO_M                                                                                                 " & vbNewLine _
    & "   , INKA_NO_S                                                                                                 " & vbNewLine _
    & "   , KENPIN_STATE_KB                                                                                           " & vbNewLine _
    & "   , LOCATION_STATE_KB                                                                                         " & vbNewLine _
    & "   , GOODS_CD_NRS                                                                                              " & vbNewLine _
    & "   , GOODS_CD_CUST                                                                                             " & vbNewLine _
    & "   , GOODS_NM_NRS                                                                                              " & vbNewLine _
    & "   , STD_IRIME_NB                                                                                              " & vbNewLine _
    & "   , PKG_NB                                                                                                    " & vbNewLine _
    & "   , STD_WT_KGS                                                                                                " & vbNewLine _
    & "   , SHOBO_CD                                                                                                  " & vbNewLine _
    & "   , RUI                                                                                                       " & vbNewLine _
    & "   , HINMEI                                                                                                    " & vbNewLine _
    & "   , NB_UT                                                                                                     " & vbNewLine _
    & "   , PKG_UT                                                                                                    " & vbNewLine _
    & "   , TOU_NO                                                                                                    " & vbNewLine _
    & "   , SITU_NO                                                                                                   " & vbNewLine _
    & "   , ZONE_CD                                                                                                   " & vbNewLine _
    & "   , LOCA                                                                                                      " & vbNewLine _
    & "   , LOT_NO                                                                                                    " & vbNewLine _
    & "   , KONSU                                                                                                     " & vbNewLine _
    & "   , HASU                                                                                                      " & vbNewLine _
    & "   , BETU_WT                                                                                                   " & vbNewLine _
    & "   , IRIME                                                                                                     " & vbNewLine _
    & "   , IRIME_UT                                                                                                  " & vbNewLine _
    & "   , SERIAL_NO                                                                                                 " & vbNewLine _
    & "   , GOODS_COND_KB_1                                                                                           " & vbNewLine _
    & "   , GOODS_COND_KB_2                                                                                           " & vbNewLine _
    & "   , GOODS_COND_KB_3                                                                                           " & vbNewLine _
    & "   , LT_DATE                                                                                                   " & vbNewLine _
    & "   , GOODS_CRT_DATE                                                                                            " & vbNewLine _
    & "   , SPD_KB                                                                                                    " & vbNewLine _
    & "   , OFB_KB                                                                                                    " & vbNewLine _
    & "   , DEST_CD                                                                                                   " & vbNewLine _
    & "   , DEST_NM                                                                                                   " & vbNewLine _
    & "   , ALLOC_PRIORITY                                                                                            " & vbNewLine _
    & "   , BUG_FLG                                                                                                   " & vbNewLine _
    & "   , REMARK                                                                                                    " & vbNewLine _
    & "   , REMARK_OUT                                                                                                " & vbNewLine _
    & "   , JISYATASYA_KB                                                                                             " & vbNewLine _
    & "   , SYS_ENT_DATE                                                                                              " & vbNewLine _
    & "   , SYS_ENT_TIME                                                                                              " & vbNewLine _
    & "   , SYS_ENT_PGID                                                                                              " & vbNewLine _
    & "   , SYS_ENT_USER                                                                                              " & vbNewLine _
    & "   , SYS_UPD_DATE                                                                                              " & vbNewLine _
    & "   , SYS_UPD_TIME                                                                                              " & vbNewLine _
    & "   , SYS_UPD_PGID                                                                                              " & vbNewLine _
    & "   , SYS_UPD_USER                                                                                              " & vbNewLine _
    & "   , SYS_DEL_FLG                                                                                               " & vbNewLine _
    & " )                                                                                                             " & vbNewLine _
    & " SELECT                                                                                                        " & vbNewLine _
    & "  INKAL.NRS_BR_CD                                                       AS NRS_BR_CD                           " & vbNewLine _
    & " ,INKAL.INKA_NO_L                                                       AS INKA_NO_L                           " & vbNewLine _
    & " ,ISNULL((SELECT MAX(IN_KENPIN_LOC_SEQ)                                                                        " & vbNewLine _
    & "          FROM $LM_TRN$..TB_KENPIN_HEAD                                                                        " & vbNewLine _
    & "          WHERE TB_KENPIN_HEAD.NRS_BR_CD = INKAL.NRS_BR_CD                                                     " & vbNewLine _
    & "            AND TB_KENPIN_HEAD.INKA_NO_L = INKAL.INKA_NO_L                                                     " & vbNewLine _
    & "          GROUP BY TB_KENPIN_HEAD.NRS_BR_CD,TB_KENPIN_HEAD.INKA_NO_L                                           " & vbNewLine _
    & "         ),0)                                                           AS IN_KENPIN_LOC_SEQ                   " & vbNewLine _
    & " ,INKAM.INKA_NO_M                                                       AS INKA_NO_M                           " & vbNewLine _
    & " ,INKAS.INKA_NO_S                                                       AS INKA_NO_S                           " & vbNewLine _
    & " ,CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                                             " & vbNewLine _
    & "       WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                                             " & vbNewLine _
    & "       ELSE '00'                                                                                               " & vbNewLine _
    & "  END                                                                   AS KENPIN_STATE_KB                     " & vbNewLine _
    & " ,CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                                             " & vbNewLine _
    & "       WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                                             " & vbNewLine _
    & "       ELSE '00'                                                                                               " & vbNewLine _
    & "  END                                                                   AS LOCATION_STATE_KB                     " & vbNewLine _
    & " ,INKAM.GOODS_CD_NRS                                                    AS GOODS_CD_NRS                        " & vbNewLine _
    & " ,MGOODS.GOODS_CD_CUST                                                  AS GOODS_CD_CUST                       " & vbNewLine _
    & " ,CASE WHEN INKAS.INKA_NO_S IS NOT NULL THEN MGOODS.GOODS_NM_1                                                 " & vbNewLine _
    & "  ELSE EDIM.GOODS_NM                                                                                           " & vbNewLine _
    & "  END                                                                   AS GOODS_NM_NRS                        " & vbNewLine _
    & " ,ISNULL(MGOODS.STD_IRIME_NB,0)                                         AS STD_IRIME_NB                        " & vbNewLine _
    & " ,ISNULL(MGOODS.PKG_NB,0)                                               AS PKG_NB                              " & vbNewLine _
    & " ,ISNULL(MGOODS.STD_WT_KGS,0)                                           AS STD_WT_KGS                          " & vbNewLine _
    & " ,ISNULL(MGOODS.SHOBO_CD,'')                                            AS SHOBO_CD                            " & vbNewLine _
    & " ,ISNULL(MSHOBO.RUI,'')                                                 AS RUI                                 " & vbNewLine _
    & " ,ISNULL(MSHOBO.HINMEI,'')                                              AS HINMEI                              " & vbNewLine _
    & " ,ISNULL(MGOODS.NB_UT,'')                                               AS NB_UT                               " & vbNewLine _
    & " ,ISNULL(MGOODS.PKG_UT,'')                                              AS PKG_UT                              " & vbNewLine _
    & " ,ISNULL(INKAS.TOU_NO,'')                                               AS TOU_NO                              " & vbNewLine _
    & " ,ISNULL(INKAS.SITU_NO,'')                                              AS SITU_NO                             " & vbNewLine _
    & " ,ISNULL(INKAS.ZONE_CD,'')                                              AS ZONE_CD                             " & vbNewLine _
    & " ,ISNULL(INKAS.LOCA,'')                                                 AS LOCA                                " & vbNewLine _
    & " ,ISNULL(INKAS.LOT_NO ,ISNULL(EDIM.LOT_NO , ''))                        AS LOT_NO                              " & vbNewLine _
    & " ,ISNULL(INKAS.KONSU ,ISNULL(EDIM.NB , '0'))                            AS KONSU                               " & vbNewLine _
    & " ,ISNULL(INKAS.HASU ,ISNULL(EDIM.HASU , '0'))                           AS HASU                                " & vbNewLine _
    & " ,ISNULL(INKAS.BETU_WT,0)                                               AS BETU_WT                             " & vbNewLine _
    & " ,ISNULL(INKAS.IRIME ,ISNULL(EDIM.IRIME , '0'))                         AS IRIME                               " & vbNewLine _
    & " ,ISNULL(MGOODS.STD_IRIME_UT,'')                                        AS IRIME_UT                            " & vbNewLine _
    & " ,ISNULL(INKAS.SERIAL_NO ,ISNULL(EDIM.SERIAL_NO , ''))                  AS SERIAL_NO                           " & vbNewLine _
    & " ,ISNULL(INKAS.GOODS_COND_KB_1,'')                                      AS GOODS_COND_KB_1                     " & vbNewLine _
    & " ,ISNULL(INKAS.GOODS_COND_KB_2,'')                                      AS GOODS_COND_KB_2                     " & vbNewLine _
    & " ,ISNULL(INKAS.GOODS_COND_KB_3,'')                                      AS GOODS_COND_KB_3                     " & vbNewLine _
    & " ,ISNULL(INKAS.LT_DATE,'')                                              AS LT_DATE                             " & vbNewLine _
    & " ,ISNULL(INKAS.GOODS_CRT_DATE,'')                                       AS GOODS_CRT_DATE                      " & vbNewLine _
    & " ,ISNULL(INKAS.SPD_KB,'')                                               AS SPD_KB                              " & vbNewLine _
    & " ,ISNULL(INKAS.OFB_KB,'')                                               AS OFB_KB                              " & vbNewLine _
    & " ,ISNULL(INKAS.DEST_CD,'')                                              AS DEST_CD                             " & vbNewLine _
    & " ,ISNULL(MDEST.DEST_NM,'')                                              AS DEST_NM                             " & vbNewLine _
    & " ,ISNULL(INKAS.ALLOC_PRIORITY,'')                                       AS ALLOC_PRIORITY                      " & vbNewLine _
    & " ,ISNULL(INKAS.BUG_YN,'')                                               AS BUG_FLG                             " & vbNewLine _
    & " ,ISNULL(INKAS.REMARK,'')                                               AS REMARK                              " & vbNewLine _
    & " ,ISNULL(INKAS.REMARK_OUT,'')                                           AS REMARK_OUT                          " & vbNewLine _
    & " ,ISNULL(TOUSITU.JISYATASYA_KB,'')                                      AS JISYATASYA_KB                          " & vbNewLine _
    & " ,@SYS_ENT_DATE                                                         AS SYS_ENT_DATE                        " & vbNewLine _
    & " ,@SYS_ENT_TIME                                                         AS SYS_ENT_TIME                        " & vbNewLine _
    & " ,@SYS_ENT_PGID                                                         AS SYS_ENT_PGID                        " & vbNewLine _
    & " ,@SYS_ENT_USER                                                         AS SYS_ENT_USER                        " & vbNewLine _
    & " ,@SYS_UPD_DATE                                                         AS SYS_UPD_DATE                        " & vbNewLine _
    & " ,@SYS_UPD_TIME                                                         AS SYS_UPD_TIME                        " & vbNewLine _
    & " ,@SYS_UPD_PGID                                                         AS SYS_UPD_PGID                        " & vbNewLine _
    & " ,@SYS_UPD_USER                                                         AS SYS_UPD_USER                        " & vbNewLine _
    & " ,'0'                                                                   AS SYS_DEL_FLG                         " & vbNewLine _
    & " FROM $LM_TRN$..B_INKA_L INKAL                                                                                 " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                                                            " & vbNewLine _
    & " ON  INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                                                         " & vbNewLine _
    & " AND INKAM.INKA_NO_L = INKAL.INKA_NO_L                                                                         " & vbNewLine _
    & " AND INKAM.SYS_DEL_FLG = '0'                                                                                   " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..B_INKA_S INKAS                                                                            " & vbNewLine _
    & " ON  INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                                                         " & vbNewLine _
    & " AND INKAS.INKA_NO_L = INKAM.INKA_NO_L                                                                         " & vbNewLine _
    & " AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                                                                         " & vbNewLine _
    & " AND INKAS.SYS_DEL_FLG = '0'                                                                                   " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_GOODS MGOODS                                                                            " & vbNewLine _
    & " ON  MGOODS.NRS_BR_CD    = INKAM.NRS_BR_CD                                                                     " & vbNewLine _
    & " AND MGOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                                                  " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_SHOBO MSHOBO                                                                            " & vbNewLine _
    & " ON  MSHOBO.SHOBO_CD    = MGOODS.SHOBO_CD                                                                      " & vbNewLine _
    & " AND MSHOBO.SYS_DEL_FLG = '0'                                                                                  " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_DEST MDEST                                                                              " & vbNewLine _
    & " ON  MDEST.NRS_BR_CD = INKAS.NRS_BR_CD                                                                         " & vbNewLine _
    & " AND MDEST.DEST_CD   = INKAS.DEST_CD                                                                           " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..H_INKAEDI_M EDIM                                                                          " & vbNewLine _
    & " ON   EDIM.NRS_BR_CD = INKAM.NRS_BR_CD                                                                         " & vbNewLine _
    & " AND  EDIM.INKA_CTL_NO_L = INKAM.INKA_NO_L                                                                     " & vbNewLine _
    & " AND  EDIM.INKA_CTL_NO_M = INKAM.INKA_NO_M                                                                     " & vbNewLine _
    & " AND  EDIM.SYS_DEL_FLG = '0'                                                                                   " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_TOU_SITU TOUSITU                                                        " & vbNewLine _
    & " ON  TOUSITU.WH_CD   = INKAL.WH_CD                                                             " & vbNewLine _
    & " AND TOUSITU.TOU_NO  = INKAS.TOU_NO                                                            " & vbNewLine _
    & " AND TOUSITU.SITU_NO = INKAS.SITU_NO                                                           " & vbNewLine _
    & "  WHERE                                                                                                        " & vbNewLine _
    & "      INKAL.NRS_BR_CD  = @NRS_BR_CD                                                                            " & vbNewLine _
    & " AND INKAL.INKA_NO_L   = @INKA_NO_L                                                                            " & vbNewLine _
    & " AND INKAL.SYS_DEL_FLG = '0'                                                                                   " & vbNewLine

#End Region

#Region "画像登録"
    Private Const SQL_INSERT_TZ_FILE As String _
    = " INSERT INTO $LM_TRN$..TZ_FILE(                                           " & vbNewLine _
    & "  FILE_TYPE                                                               " & vbNewLine _
    & " ,KEY_TYPE                                                                " & vbNewLine _
    & " ,NRS_BR_CD                                                               " & vbNewLine _
    & " ,CONTROL_NO_L                                                            " & vbNewLine _
    & " ,CONTROL_NO_M                                                            " & vbNewLine _
    & " ,CONTROL_NO_S                                                            " & vbNewLine _
    & " ,CONTROL_SEQ                                                             " & vbNewLine _
    & " ,FILE_PATH                                                               " & vbNewLine _
    & " ,FILE_NM                                                                 " & vbNewLine _
    & " ,REMARK                                                                  " & vbNewLine _
    & " ,SYS_ENT_DATE                                                            " & vbNewLine _
    & " ,SYS_ENT_TIME                                                            " & vbNewLine _
    & " ,SYS_ENT_PGID                                                            " & vbNewLine _
    & " ,SYS_ENT_USER                                                            " & vbNewLine _
    & " ,SYS_UPD_DATE                                                            " & vbNewLine _
    & " ,SYS_UPD_TIME                                                            " & vbNewLine _
    & " ,SYS_UPD_PGID                                                            " & vbNewLine _
    & " ,SYS_UPD_USER                                                            " & vbNewLine _
    & " ,SYS_DEL_FLG                                                             " & vbNewLine _
    & " )                                                                        " & vbNewLine _
    & " SELECT                                                                   " & vbNewLine _
    & "  FILE_TYPE_KBN                          AS FILE_TYPE                     " & vbNewLine _
    & " ,KEY_TYPE_KBN                           AS KEY_TYPE                      " & vbNewLine _
    & " ,@NRS_BR_CD                             AS NRS_BR_CD                     " & vbNewLine _
    & " ,SUBSTRING(KEY_NO,1,9)                  AS CONTROL_NO_L                  " & vbNewLine _
    & " ,SUBSTRING(KEY_NO,10,3)                 AS CONTROL_NO_M                  " & vbNewLine _
    & " ,SUBSTRING(KEY_NO,13,3)                 AS CONTROL_NO_S                  " & vbNewLine _
    & " ,ISNULL((SELECT MAX(IN_KENPIN_LOC_SEQ)                                   " & vbNewLine _
    & "          FROM $LM_TRN$..TB_KENPIN_HEAD                                   " & vbNewLine _
    & "          WHERE TB_KENPIN_HEAD.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
    & "            AND TB_KENPIN_HEAD.INKA_NO_L = @INKA_NO_L                     " & vbNewLine _
    & "          GROUP BY TB_KENPIN_HEAD.NRS_BR_CD,TB_KENPIN_HEAD.INKA_NO_L      " & vbNewLine _
    & "         ),0)                            AS CONTROL_SEQ                   " & vbNewLine _
    & " ,FILE_PATH                              AS FILE_PATH                     " & vbNewLine _
    & " ,FILE_NM                                AS FILE_NM                       " & vbNewLine _
    & " ,REMARK                                 AS REMARK                        " & vbNewLine _
    & " ,@SYS_ENT_DATE                          AS SYS_ENT_DATE                  " & vbNewLine _
    & " ,@SYS_ENT_TIME                          AS SYS_ENT_TIME                  " & vbNewLine _
    & " ,@SYS_ENT_PGID                          AS SYS_ENT_PGID                  " & vbNewLine _
    & " ,@SYS_ENT_USER                          AS SYS_ENT_USER                  " & vbNewLine _
    & " ,@SYS_UPD_DATE                          AS SYS_UPD_DATE                  " & vbNewLine _
    & " ,@SYS_UPD_TIME                          AS SYS_UPD_TIME                  " & vbNewLine _
    & " ,@SYS_UPD_PGID                          AS SYS_UPD_PGID                  " & vbNewLine _
    & " ,@SYS_UPD_USER                          AS SYS_UPD_USER                  " & vbNewLine _
    & " ,@SYS_DEL_FLG                           AS SYS_DEL_FLG                   " & vbNewLine _
    & " FROM LM_MST..M_FILE                                                      " & vbNewLine _
    & " WHERE                                                                    " & vbNewLine _
    & "     KEY_NO LIKE @KEY_NO                                                  " & vbNewLine _
    & " AND FILE_TYPE_KBN = 'L3'                                                 " & vbNewLine _
    & " AND KEY_TYPE_KBN  = '44'                                                 " & vbNewLine _
    & " AND SYS_DEL_FLG   = '0'                                                  " & vbNewLine

#End Region

#End Region

#Region "Update"

#Region "入荷検品 キャンセル"
    Public Const SQL_UPDATE_TB_KNEPIN_HEAD_CANCEL As String _
    = "UPDATE $LM_TRN$..TB_KENPIN_HEAD            " & vbNewLine _
    & "SET                                        " & vbNewLine _
    & " CANCEL_FLG   = '01'                       " & vbNewLine _
    & ",CANCEL_TYPE  = @CANCEL_TYPE               " & vbNewLine _
    & ",SYS_UPD_DATE = @SYS_UPD_DATE              " & vbNewLine _
    & ",SYS_UPD_TIME = @SYS_UPD_TIME              " & vbNewLine _
    & ",SYS_UPD_PGID = @SYS_UPD_PGID              " & vbNewLine _
    & ",SYS_UPD_USER = @SYS_UPD_USER              " & vbNewLine _
    & "WHERE                                      " & vbNewLine _
    & "    NRS_BR_CD         = @NRS_BR_CD         " & vbNewLine _
    & "AND INKA_NO_L         = @INKA_NO_L         " & vbNewLine _
    & "AND IN_KENPIN_LOC_SEQ = @IN_KENPIN_LOC_SEQ " & vbNewLine

#End Region

#Region "入荷検品 削除"
    Public Const SQL_UPDATE_TB_KNEPIN_HEAD_DEL As String _
    = "UPDATE $LM_TRN$..TB_KENPIN_HEAD            " & vbNewLine _
    & "SET                                        " & vbNewLine _
    & " IN_KENPIN_LOC_STATE_KB = '99'             " & vbNewLine _
    & ",SYS_UPD_DATE           = @SYS_UPD_DATE    " & vbNewLine _
    & ",SYS_UPD_TIME           = @SYS_UPD_TIME    " & vbNewLine _
    & ",SYS_UPD_PGID           = @SYS_UPD_PGID    " & vbNewLine _
    & ",SYS_UPD_USER           = @SYS_UPD_USER    " & vbNewLine _
    & "WHERE                                      " & vbNewLine _
    & "    NRS_BR_CD         = @NRS_BR_CD         " & vbNewLine _
    & "AND INKA_NO_L         = @INKA_NO_L         " & vbNewLine _
    & "AND IN_KENPIN_LOC_SEQ = @IN_KENPIN_LOC_SEQ " & vbNewLine

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

#Region "登録用入荷作業データ取得"

    ''' <summary>
    ''' 登録用入荷作業データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLMSSagyoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectInkaData()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMB810DAC.SQL_SELECT_SAGYO_DATA, Me._Row.Item("NRS_BR_CD").ToString())

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
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("WORK_SEQ", "WORK_SEQ")
        map.Add("SAGYO_STATE_KB", "SAGYO_STATE_KB")
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

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMB810DAC.TABLE_NM.LMB810IN_SAGYO))

        Return ds

    End Function

#End Region

#Region "入荷検品ヘッダ取得"

    ''' <summary>
    ''' 入荷検品ヘッダ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectKenpinHead(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectInkaData()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMB810DAC.SQL_SELECT_TB_KENPIN_HEAD, Me._Row.Item("NRS_BR_CD").ToString())

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
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("IN_KENPIN_LOC_SEQ", "IN_KENPIN_LOC_SEQ")
        map.Add("IN_KENPIN_LOC_STATE_KB", "IN_KENPIN_LOC_STATE_KB")
        map.Add("WORK_STATE_KB", "WORK_STATE_KB")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("CANCEL_TYPE", "CANCEL_TYPE")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("BUYER_ORD_NO_L", "BUYER_ORD_NO_L")
        map.Add("OUTKA_FROM_ORD_NO_L", "OUTKA_FROM_ORD_NO_L")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("REMARK_KENPIN_CHK_FLG", "REMARK_KENPIN_CHK_FLG")
        map.Add("REMARK_LOCA_CHK_FLG", "REMARK_LOCA_CHK_FLG")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMB810DAC.TABLE_NM.LMB810OUT_KENPIN_HEAD))

    End Function

#End Region

#Region "チェックデータ取得"

    ''' <summary>
    ''' チェックデータ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectInkaCheckData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectInkaData()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMB810DAC.SQL_SELECT_CHECK_DATA, Me._Row.Item("NRS_BR_CD").ToString())

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
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_STATE_KB", "INKA_STATE_KB")
        map.Add("WH_TAB_YN", "WH_TAB_YN")
        map.Add("WH_TAB_STATUS", "WH_TAB_STATUS")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMB810DAC.TABLE_NM.LMB810CHECK))

        Return ds

    End Function

#End Region

#Region "入荷更新結果取得"

    ''' <summary>
    ''' 入荷更新結果取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectInkaLastUpdResults(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append("SELECT          " & vbNewLine)
        Me._StrSql.Append("     NRS_BR_CD                       " & vbNewLine)
        Me._StrSql.Append("    ,INKA_NO_L                       " & vbNewLine)
        Me._StrSql.Append("    ,WH_TAB_STATUS                   " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_DATE                    " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_TIME                    " & vbNewLine)
        Me._StrSql.Append("FROM $LM_TRN$..B_INKA_L              " & vbNewLine)
        Me._StrSql.Append("WHERE                                " & vbNewLine)
        Me._StrSql.Append("    NRS_BR_CD = @NRS_BR_CD           " & vbNewLine)
        Me._StrSql.Append("AND INKA_NO_L = @INKA_NO_L           " & vbNewLine)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row("INKA_NO_L").ToString(), DBDataType.CHAR))

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
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("WH_TAB_STATUS", "WH_TAB_STATUS")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMB810DAC.TABLE_NM.LMB810OUT_UPD_RESULTS))

        Return ds

    End Function

#End Region

#End Region

#Region "登録"

#Region "入荷作業登録"

    ''' <summary>
    ''' 入荷作業登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN_SAGYO).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamInsertSagyo()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMB810DAC.SQL_INSERT_TB_SAGYO, Me._Row.Item("NRS_BR_CD").ToString())

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

#Region "入荷検品ヘッダ登録"

    ''' <summary>
    ''' 入荷検品ヘッダ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertKenpinHead(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectInkaData()
        Call Me.SetParamCommonSystemUp()


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMB810DAC.SQL_INSERT_TB_KENPIN_HEAD, Me._Row.Item("NRS_BR_CD").ToString())

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

#Region "入荷検品明細登録"

    ''' <summary>
    ''' 入荷検品明細登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertKenpinDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamSelectInkaData()
        Call Me.SetParamCommonSystemUp()


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMB810DAC.SQL_INSERT_TB_KENPIN_DTL, Me._Row.Item("NRS_BR_CD").ToString())

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

#Region "入荷検品画像登録"

    ''' <summary>
    ''' 入荷検品画像登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertFileData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamInsertFileData()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMB810DAC.SQL_INSERT_TZ_FILE, Me._Row.Item("NRS_BR_CD").ToString())

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
#End Region

#Region "更新"

#Region "キャンセル"

#Region "入荷検品キャンセル"
    ''' <summary>
    ''' 入荷検品キャンセル
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateKenpinCancel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMB810DAC.TABLE_NM.LMB810OUT_KENPIN_HEAD).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionUpdateKenpin()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMB810DAC.SQL_UPDATE_TB_KNEPIN_HEAD_CANCEL, Me._Row.Item("NRS_BR_CD").ToString())

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

#End Region

#Region "削除"

#Region "入荷検品削除"
    ''' <summary>
    ''' 入荷検品削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateKenpinDelete(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMB810DAC.TABLE_NM.LMB810OUT_KENPIN_HEAD).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionUpdateKenpin()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMB810DAC.SQL_UPDATE_TB_KNEPIN_HEAD_DEL, Me._Row.Item("NRS_BR_CD").ToString())

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

#End Region

#End Region

#Region "営業所チェック"
    ''' <summary>
    ''' 営業所チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckTabletUse(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN).Rows(0)

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

#Region "入荷L更新"
    ''' <summary>
    ''' 入荷L更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateWHSagyoShijiStatus(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN).Rows(0)

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append("UPDATE $LM_TRN$..B_INKA_L            " & vbNewLine)
        Me._StrSql.Append("SET                                  " & vbNewLine)
        Me._StrSql.Append("     WH_TAB_STATUS = @WH_TAB_STATUS  " & vbNewLine)
        Me._StrSql.Append("    ,WH_TAB_IMP_YN = '00'            " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_DATE  = @SYS_UPD_DATE   " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_TIME  = @SYS_UPD_TIME   " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_PGID  = @SYS_UPD_PGID   " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_USER  = @SYS_UPD_USER   " & vbNewLine)
        Me._StrSql.Append("WHERE                                " & vbNewLine)
        Me._StrSql.Append("    NRS_BR_CD = @NRS_BR_CD           " & vbNewLine)
        Me._StrSql.Append("AND INKA_NO_L = @INKA_NO_L           " & vbNewLine)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row("INKA_NO_L").ToString(), DBDataType.CHAR))
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
        Me._Row = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN).Rows(0)

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append("SELECT count(*) AS CNT FROM $LM_TRN$..B_INKA_L  " & vbNewLine)
        Me._StrSql.Append("WHERE                                            " & vbNewLine)
        Me._StrSql.Append("    NRS_BR_CD    = @NRS_BR_CD                    " & vbNewLine)
        Me._StrSql.Append("AND INKA_NO_L    = @INKA_NO_L                    " & vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE                 " & vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME                 " & vbNewLine)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row("INKA_NO_L").ToString(), DBDataType.CHAR))
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

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMB810DAC.TABLE_NM.LMB810CNT))

        Return ds
    End Function
#End Region

#Region "パラメータ設定"

    ''' <summary>
    '''  パラメータ設定 入荷データ取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectInkaData()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_TYPE", Me._Row("PROC_TYPE").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定 検品更新用
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectConditionUpdateKenpin()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IN_KENPIN_LOC_SEQ", Me._Row("IN_KENPIN_LOC_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANCEL_TYPE", Me._Row("CANCEL_TYPE").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定 作業インサート用
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertSagyo()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", Me._Row("SAGYO_REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", Me._Row("INKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", Me._Row("INKA_NO_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WORK_SEQ", Me._Row("WORK_SEQ").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_STATE_KB", Me._Row("SAGYO_STATE_KB").ToString(), DBDataType.CHAR))
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

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", "0", DBDataType.CHAR))

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
    '''  パラメータ設定 入荷データ取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertFileData()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEY_NO", String.Concat(Me._Row("INKA_NO_L").ToString(), "%"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", "0", DBDataType.CHAR))

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


#End Region

#Region "その他"
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

End Class
