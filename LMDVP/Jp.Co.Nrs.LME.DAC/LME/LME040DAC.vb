' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME040  : 作業指示書検索
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LME040DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LME040DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "変数"

#End Region

#Region "作業指示書データの検索"

#Region "作業指示書データの検索 SQL SELECT句"

    ''' <summary>
    ''' 作業指示書データの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGYOSIJI As String = " SELECT                                                                  " & vbNewLine _
                                                 & " SAGYOSIJI.NRS_BR_CD                   AS NRS_BR_CD                      " & vbNewLine _
                                                 & ",SAGYOSIJI.SAGYO_SIJI_NO               AS SAGYO_SIJI_NO                  " & vbNewLine _
                                                 & ",SAGYOSIJI.WH_CD                       AS WH_CD                          " & vbNewLine _
                                                 & ",SAGYOSIJI.REMARK_1                    AS REMARK_1                       " & vbNewLine _
                                                 & ",SAGYOSIJI.REMARK_2                    AS REMARK_2                       " & vbNewLine _
                                                 & ",SAGYOSIJI.REMARK_3                    AS REMARK_3                       " & vbNewLine _
                                                 & ",SAGYOSIJI.WH_TAB_STATUS               AS WH_TAB_STATUS                  " & vbNewLine _
                                                 & ",SAGYOSIJI.SAGYO_SIJI_STATUS           AS SAGYO_SIJI_STATUS              " & vbNewLine _
                                                 & ",SAGYOSIJI.SAGYO_SIJI_DATE             AS SAGYO_SIJI_DATE                " & vbNewLine _
                                                 & ",SAGYOSIJI.SYS_UPD_DATE                AS SYS_UPD_DATE                   " & vbNewLine _
                                                 & ",SAGYOSIJI.SYS_UPD_TIME                AS SYS_UPD_TIME                   " & vbNewLine

#End Region

#Region "作業指示書データの検索 SQL FROM句"

    ''' <summary>
    ''' 作業指示書データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SAGYOSIJI As String = "FROM                                                               " & vbNewLine _
                                                      & "$LM_TRN$..E_SAGYO_SIJI SAGYOSIJI                                   " & vbNewLine

#End Region

#End Region

#Region "作業データの検索"

#Region "作業データの検索 SQL SELECT句"

    ''' <summary>
    ''' 作業データの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGYO As String = " SELECT                                                                  " & vbNewLine _
                                             & " SAGYO.NRS_BR_CD                       AS NRS_BR_CD                      " & vbNewLine _
                                             & ",SAGYO.SAGYO_REC_NO                    AS SAGYO_REC_NO                   " & vbNewLine _
                                             & ",SAGYO.SAGYO_COMP                      AS SAGYO_COMP                     " & vbNewLine _
                                             & ",SAGYO.SKYU_CHK                        AS SKYU_CHK                       " & vbNewLine _
                                             & ",SAGYO.SAGYO_SIJI_NO                   AS SAGYO_SIJI_NO                  " & vbNewLine _
                                             & ",SAGYO.INOUTKA_NO_LM                   AS INOUTKA_NO_LM                  " & vbNewLine _
                                             & ",SAGYO.WH_CD                           AS WH_CD                          " & vbNewLine _
                                             & ",SAGYO.IOZS_KB                         AS IOZS_KB                        " & vbNewLine _
                                             & ",SAGYO.SAGYO_CD                        AS SAGYO_CD                       " & vbNewLine _
                                             & ",SAGYO.SAGYO_NM                        AS SAGYO_NM                       " & vbNewLine _
                                             & ",SAGYO.CUST_CD_L                       AS CUST_CD_L                      " & vbNewLine _
                                             & ",SAGYO.CUST_CD_M                       AS CUST_CD_M                      " & vbNewLine _
                                             & ",SAGYO.DEST_CD                         AS DEST_CD                        " & vbNewLine _
                                             & ",SAGYO.DEST_NM                         AS DEST_NM                        " & vbNewLine _
                                             & ",SAGYO.GOODS_CD_NRS                    AS GOODS_CD_NRS                   " & vbNewLine _
                                             & ",SAGYO.GOODS_NM_NRS                    AS GOODS_NM_NRS                   " & vbNewLine _
                                             & ",SAGYO.LOT_NO                          AS LOT_NO                         " & vbNewLine _
                                             & ",SAGYO.INV_TANI                        AS INV_TANI                       " & vbNewLine _
                                             & ",SAGYO.SAGYO_NB                        AS SAGYO_NB                       " & vbNewLine _
                                             & ",SAGYO.SAGYO_UP                        AS SAGYO_UP                       " & vbNewLine _
                                             & ",SAGYO.SAGYO_GK                        AS SAGYO_GK                       " & vbNewLine _
                                             & ",SAGYO.TAX_KB                          AS TAX_KB                         " & vbNewLine _
                                             & ",SAGYO.SEIQTO_CD                       AS SEIQTO_CD                      " & vbNewLine _
                                             & ",SAGYO.REMARK_ZAI                      AS REMARK_ZAI                     " & vbNewLine _
                                             & ",SAGYO.REMARK_SKYU                     AS REMARK_SKYU                    " & vbNewLine _
                                             & ",SAGYO.SAGYO_COMP_CD                   AS SAGYO_COMP_CD                  " & vbNewLine _
                                             & ",SAGYO.SAGYO_COMP_DATE                 AS SAGYO_COMP_DATE                " & vbNewLine _
                                             & ",SAGYO.DEST_SAGYO_FLG                  AS DEST_SAGYO_FLG                 " & vbNewLine _
                                             & ",SAGYO.ZAI_REC_NO                      AS ZAI_REC_NO                     " & vbNewLine _
                                             & ",SAGYO.PORA_ZAI_NB                     AS PORA_ZAI_NB                    " & vbNewLine _
                                             & ",SAGYO.PORA_ZAI_QT                     AS PORA_ZAI_QT                    " & vbNewLine _
                                             & ",GOODS.GOODS_CD_CUST                   AS GOODS_CD_CUST                  " & vbNewLine _
                                             & ",ZAI.TOU_NO                            AS TOU_NO                         " & vbNewLine _
                                             & ",ZAI.SITU_NO                           AS SITU_NO                        " & vbNewLine _
                                             & ",ZAI.ZONE_CD                           AS ZONE_CD                        " & vbNewLine _
                                             & ",ZAI.LOCA                              AS LOCA                           " & vbNewLine _
                                             & ",ZAI.IRIME                             AS IRIME                          " & vbNewLine _
                                             & ",ZAI.LT_DATE                           AS LT_DATE                        " & vbNewLine _
                                             & ",Z1.KBN_NM1                            AS GOODS_COND_KB_1_NM             " & vbNewLine _
                                             & ",Z2.KBN_NM1                            AS GOODS_COND_KB_2_NM             " & vbNewLine _
                                             & ",CUSTCOND.JOTAI_NM                     AS GOODS_COND_KB_3_NM             " & vbNewLine _
                                             & ",Z3.KBN_NM1                            AS OFB_KB_NM                      " & vbNewLine _
                                             & ",Z4.KBN_NM1                            AS SPD_KB_NM                      " & vbNewLine _
                                             & ",ZAI.SERIAL_NO                         AS SERIAL_NO                      " & vbNewLine _
                                             & ",ZAI.GOODS_CRT_DATE                    AS GOODS_CRT_DATE                 " & vbNewLine _
                                             & ",ZAI.DEST_CD_P                         AS DEST_CD_P                      " & vbNewLine _
                                             & ",Z5.KBN_NM1                            AS ALLOC_PRIORITY_NM              " & vbNewLine _
                                             & ",ZAI.INKO_DATE                         AS INKO_DATE                      " & vbNewLine _
                                             & ",ZAI.INKO_PLAN_DATE                    AS INKO_PLAN_DATE                 " & vbNewLine _
                                             & ",ZAI.ALLOC_CAN_NB                      AS PORA_ZAI_NB_ZAI                " & vbNewLine _
                                             & ",ZAI.ALLOC_CAN_QT                      AS PORA_ZAI_QT_ZAI                " & vbNewLine _
                                             & ",'0'                                   AS UPD_FLG                        " & vbNewLine _
                                             & ",SAGYO.ZAI_REC_NO                      AS KEY_NO                         " & vbNewLine _
                                             & ",SAGYO.REMARK_SIJI                     AS REMARK_SIJI                    " & vbNewLine

#End Region

#Region "作業データの検索 SQL FROM句"

    ''' <summary>
    ''' 作業データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SAGYO As String = "FROM                                                               " & vbNewLine _
                                                  & "$LM_TRN$..E_SAGYO SAGYO                                            " & vbNewLine _
                                                  & "LEFT JOIN                                                          " & vbNewLine _
                                                  & "$LM_TRN$..D_ZAI_TRS ZAI                                            " & vbNewLine _
                                                  & "ON                                                                 " & vbNewLine _
                                                  & "ZAI.NRS_BR_CD = SAGYO.NRS_BR_CD                                    " & vbNewLine _
                                                  & "AND                                                                " & vbNewLine _
                                                  & "ZAI.ZAI_REC_NO = SAGYO.ZAI_REC_NO                                  " & vbNewLine _
                                                  & "AND                                                                " & vbNewLine _
                                                  & "ZAI.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                                  & "LEFT JOIN                                                          " & vbNewLine _
                                                  & "$LM_MST$..M_GOODS GOODS                                            " & vbNewLine _
                                                  & "ON                                                                 " & vbNewLine _
                                                  & "GOODS.NRS_BR_CD = SAGYO.NRS_BR_CD                                  " & vbNewLine _
                                                  & "AND                                                                " & vbNewLine _
                                                  & "GOODS.GOODS_CD_NRS = SAGYO.GOODS_CD_NRS                            " & vbNewLine _
                                                  & "LEFT JOIN                                                          " & vbNewLine _
                                                  & "$LM_MST$..M_CUSTCOND CUSTCOND                                      " & vbNewLine _
                                                  & "ON                                                                 " & vbNewLine _
                                                  & "ZAI.NRS_BR_CD = CUSTCOND.NRS_BR_CD                                 " & vbNewLine _
                                                  & "AND                                                                " & vbNewLine _
                                                  & "ZAI.CUST_CD_L = CUSTCOND.CUST_CD_L                                 " & vbNewLine _
                                                  & "AND                                                                " & vbNewLine _
                                                  & "ZAI.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD                            " & vbNewLine _
                                                  & "LEFT JOIN                                                          " & vbNewLine _
                                                  & "$LM_MST$..Z_KBN    Z1                                              " & vbNewLine _
                                                  & "ON                                                                 " & vbNewLine _
                                                  & "ZAI.GOODS_COND_KB_1 = Z1.KBN_CD                                    " & vbNewLine _
                                                  & "AND                                                                " & vbNewLine _
                                                  & "Z1.KBN_GROUP_CD = 'S005'                                           " & vbNewLine _
                                                  & "LEFT JOIN                                                          " & vbNewLine _
                                                  & "$LM_MST$..Z_KBN    Z2                                              " & vbNewLine _
                                                  & "ON                                                                 " & vbNewLine _
                                                  & "ZAI.GOODS_COND_KB_2 = Z2.KBN_CD                                    " & vbNewLine _
                                                  & "AND                                                                " & vbNewLine _
                                                  & "Z2.KBN_GROUP_CD = 'S006'                                           " & vbNewLine _
                                                  & "LEFT JOIN                                                          " & vbNewLine _
                                                  & "$LM_MST$..Z_KBN    Z3                                              " & vbNewLine _
                                                  & "ON                                                                 " & vbNewLine _
                                                  & "ZAI.OFB_KB = Z3.KBN_CD                                             " & vbNewLine _
                                                  & "AND                                                                " & vbNewLine _
                                                  & "Z3.KBN_GROUP_CD = 'B002'                                           " & vbNewLine _
                                                  & "LEFT JOIN                                                          " & vbNewLine _
                                                  & "$LM_MST$..Z_KBN    Z4                                              " & vbNewLine _
                                                  & "ON                                                                 " & vbNewLine _
                                                  & "ZAI.SPD_KB = Z4.KBN_CD                                             " & vbNewLine _
                                                  & "AND                                                                " & vbNewLine _
                                                  & "Z4.KBN_GROUP_CD = 'H003'                                           " & vbNewLine _
                                                  & "LEFT JOIN                                                          " & vbNewLine _
                                                  & "$LM_MST$..Z_KBN    Z5                                              " & vbNewLine _
                                                  & "ON                                                                 " & vbNewLine _
                                                  & "ZAI.ALLOC_PRIORITY = Z5.KBN_CD                                     " & vbNewLine _
                                                  & "AND                                                                " & vbNewLine _
                                                  & "Z5.KBN_GROUP_CD = 'W001'                                           " & vbNewLine

#End Region

#Region "作業データの検索 SQL ORDER BY句"

    ''' <summary>
    ''' 作業データの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_SAGYO As String = "ORDER BY                                                           " & vbNewLine _
                                                   & " SAGYO.SAGYO_REC_NO                                                " & vbNewLine

#End Region

#End Region


#Region "作業指示書の追加"

    ''' <summary>
    ''' 作業指示書テーブル新規作成 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SAGYOSIJI As String = "INSERT INTO $LM_TRN$..E_SAGYO_SIJI                 " & vbNewLine _
                                                 & " ( 		                                           " & vbNewLine _
                                                 & " SAGYO_SIJI_NO,                                    " & vbNewLine _
                                                 & " NRS_BR_CD,                                        " & vbNewLine _
                                                 & " WH_CD,                                            " & vbNewLine _
                                                 & " REMARK_1,                                         " & vbNewLine _
                                                 & " REMARK_2,                                         " & vbNewLine _
                                                 & " REMARK_3,                                         " & vbNewLine _
                                                 & " WH_TAB_STATUS,                                    " & vbNewLine _
                                                 & " SAGYO_SIJI_STATUS,                                " & vbNewLine _
                                                 & " SAGYO_SIJI_DATE,                                  " & vbNewLine _
                                                 & " SYS_ENT_DATE,                                     " & vbNewLine _
                                                 & " SYS_ENT_TIME,                                     " & vbNewLine _
                                                 & " SYS_ENT_PGID,                                     " & vbNewLine _
                                                 & " SYS_ENT_USER,                                     " & vbNewLine _
                                                 & " SYS_UPD_DATE,                                     " & vbNewLine _
                                                 & " SYS_UPD_TIME,                                     " & vbNewLine _
                                                 & " SYS_UPD_PGID,                                     " & vbNewLine _
                                                 & " SYS_UPD_USER,                                     " & vbNewLine _
                                                 & " SYS_DEL_FLG                                       " & vbNewLine _
                                                 & " ) VALUES (                                        " & vbNewLine _
                                                 & " @SAGYO_SIJI_NO,                                   " & vbNewLine _
                                                 & " @NRS_BR_CD,                                       " & vbNewLine _
                                                 & " @WH_CD,                                           " & vbNewLine _
                                                 & " @REMARK_1,                                        " & vbNewLine _
                                                 & " @REMARK_2,                                        " & vbNewLine _
                                                 & " @REMARK_3,                                        " & vbNewLine _
                                                 & " @WH_TAB_STATUS,                                   " & vbNewLine _
                                                 & " @SAGYO_SIJI_STATUS,                               " & vbNewLine _
                                                 & " @SAGYO_SIJI_DATE,                                 " & vbNewLine _
                                                 & " @SYS_ENT_DATE,                                    " & vbNewLine _
                                                 & " @SYS_ENT_TIME,                                    " & vbNewLine _
                                                 & " @SYS_ENT_PGID,                                    " & vbNewLine _
                                                 & " @SYS_ENT_USER,                                    " & vbNewLine _
                                                 & " @SYS_UPD_DATE,                                    " & vbNewLine _
                                                 & " @SYS_UPD_TIME,                                    " & vbNewLine _
                                                 & " @SYS_UPD_PGID,                                    " & vbNewLine _
                                                 & " @SYS_UPD_USER,                                    " & vbNewLine _
                                                 & " @SYS_DEL_FLG                                      " & vbNewLine _
                                                 & " )                                                 " & vbNewLine

#End Region

#Region "作業指示書の更新"

    ''' <summary>
    ''' 作業指示書の更新 SQL UPDATE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SAGYOSIJI As String = "UPDATE $LM_TRN$..E_SAGYO_SIJI SET                              " & vbNewLine _
                                                 & "                  REMARK_1             = @REMARK_1             " & vbNewLine _
                                                 & "                 ,REMARK_2             = @REMARK_2             " & vbNewLine _
                                                 & "                 ,REMARK_3             = @REMARK_3             " & vbNewLine _
                                                 & "                 ,WH_TAB_STATUS        = @WH_TAB_STATUS        " & vbNewLine _
                                                 & "                 ,SAGYO_SIJI_STATUS    = @SAGYO_SIJI_STATUS    " & vbNewLine _
                                                 & "                 ,SAGYO_SIJI_DATE      = @SAGYO_SIJI_DATE      " & vbNewLine _
                                                 & "                 ,SYS_UPD_DATE         = @SYS_UPD_DATE         " & vbNewLine _
                                                 & "                 ,SYS_UPD_TIME         = @SYS_UPD_TIME         " & vbNewLine _
                                                 & "                 ,SYS_UPD_PGID         = @SYS_UPD_PGID         " & vbNewLine _
                                                 & "                 ,SYS_UPD_USER         = @SYS_UPD_USER         " & vbNewLine _
                                                 & "                 WHERE                                         " & vbNewLine _
                                                 & "                       NRS_BR_CD       = @NRS_BR_CD            " & vbNewLine _
                                                 & "                   AND SAGYO_SIJI_NO   = @SAGYO_SIJI_NO        " & vbNewLine

#End Region

#Region "作業指示書の削除"

    ''' <summary>
    ''' 作業指示書の削除 SQL UPDATE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_SAGYOSIJI As String = "UPDATE $LM_TRN$..E_SAGYO_SIJI SET                              " & vbNewLine _
                                                 & "                  SYS_UPD_DATE         = @SYS_UPD_DATE         " & vbNewLine _
                                                 & "                 ,SYS_UPD_TIME         = @SYS_UPD_TIME         " & vbNewLine _
                                                 & "                 ,SYS_UPD_PGID         = @SYS_UPD_PGID         " & vbNewLine _
                                                 & "                 ,SYS_UPD_USER         = @SYS_UPD_USER         " & vbNewLine _
                                                 & "                 ,SYS_DEL_FLG          = '1'                   " & vbNewLine _
                                                 & "                 WHERE                                         " & vbNewLine _
                                                 & "                       NRS_BR_CD       = @NRS_BR_CD            " & vbNewLine _
                                                 & "                   AND SAGYO_SIJI_NO   = @SAGYO_SIJI_NO        " & vbNewLine

#End Region

#Region "作業レコードの追加"

    ''' <summary>
    ''' 作業指示書テーブル新規作成 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SAGYOREC As String = "INSERT INTO $LM_TRN$..E_SAGYO                      " & vbNewLine _
                                                & " ( 		                                          " & vbNewLine _
                                                & " NRS_BR_CD,                                        " & vbNewLine _
                                                & " SAGYO_REC_NO,                                     " & vbNewLine _
                                                & " SAGYO_COMP,                                       " & vbNewLine _
                                                & " SKYU_CHK,                                         " & vbNewLine _
                                                & " SAGYO_SIJI_NO,                                    " & vbNewLine _
                                                & " INOUTKA_NO_LM,                                    " & vbNewLine _
                                                & " WH_CD,                                            " & vbNewLine _
                                                & " IOZS_KB,                                          " & vbNewLine _
                                                & " SAGYO_CD,                                         " & vbNewLine _
                                                & " SAGYO_NM,                                         " & vbNewLine _
                                                & " CUST_CD_L,                                        " & vbNewLine _
                                                & " CUST_CD_M,                                        " & vbNewLine _
                                                & " DEST_CD,                                          " & vbNewLine _
                                                & " DEST_NM,                                          " & vbNewLine _
                                                & " GOODS_CD_NRS,                                     " & vbNewLine _
                                                & " GOODS_NM_NRS,                                     " & vbNewLine _
                                                & " LOT_NO,                                           " & vbNewLine _
                                                & " INV_TANI,                                         " & vbNewLine _
                                                & " SAGYO_NB,                                         " & vbNewLine _
                                                & " SAGYO_UP,                                         " & vbNewLine _
                                                & " SAGYO_GK,                                         " & vbNewLine _
                                                & " TAX_KB,                                           " & vbNewLine _
                                                & " SEIQTO_CD,                                        " & vbNewLine _
                                                & " REMARK_ZAI,                                       " & vbNewLine _
                                                & " REMARK_SKYU,                                      " & vbNewLine _
                                                & " REMARK_SIJI,                                      " & vbNewLine _
                                                & " SAGYO_COMP_CD,                                    " & vbNewLine _
                                                & " SAGYO_COMP_DATE,                                  " & vbNewLine _
                                                & " DEST_SAGYO_FLG,                                   " & vbNewLine _
                                                & " ZAI_REC_NO,                                       " & vbNewLine _
                                                & " PORA_ZAI_NB,                                      " & vbNewLine _
                                                & " PORA_ZAI_QT,                                      " & vbNewLine _
                                                & " SYS_ENT_DATE,                                     " & vbNewLine _
                                                & " SYS_ENT_TIME,                                     " & vbNewLine _
                                                & " SYS_ENT_PGID,                                     " & vbNewLine _
                                                & " SYS_ENT_USER,                                     " & vbNewLine _
                                                & " SYS_UPD_DATE,                                     " & vbNewLine _
                                                & " SYS_UPD_TIME,                                     " & vbNewLine _
                                                & " SYS_UPD_PGID,                                     " & vbNewLine _
                                                & " SYS_UPD_USER,                                     " & vbNewLine _
                                                & " SYS_DEL_FLG                                       " & vbNewLine _
                                                & " ) VALUES (                                        " & vbNewLine _
                                                & " @NRS_BR_CD,                                       " & vbNewLine _
                                                & " @SAGYO_REC_NO,                                    " & vbNewLine _
                                                & " @SAGYO_COMP,                                      " & vbNewLine _
                                                & " @SKYU_CHK,                                        " & vbNewLine _
                                                & " @SAGYO_SIJI_NO,                                   " & vbNewLine _
                                                & " @INOUTKA_NO_LM,                                   " & vbNewLine _
                                                & " @WH_CD,                                           " & vbNewLine _
                                                & " @IOZS_KB,                                         " & vbNewLine _
                                                & " @SAGYO_CD,                                        " & vbNewLine _
                                                & " @SAGYO_NM,                                        " & vbNewLine _
                                                & " @CUST_CD_L,                                       " & vbNewLine _
                                                & " @CUST_CD_M,                                       " & vbNewLine _
                                                & " @DEST_CD,                                         " & vbNewLine _
                                                & " @DEST_NM,                                         " & vbNewLine _
                                                & " @GOODS_CD_NRS,                                    " & vbNewLine _
                                                & " @GOODS_NM_NRS,                                    " & vbNewLine _
                                                & " @LOT_NO,                                          " & vbNewLine _
                                                & " @INV_TANI,                                        " & vbNewLine _
                                                & " @SAGYO_NB,                                        " & vbNewLine _
                                                & " @SAGYO_UP,                                        " & vbNewLine _
                                                & " @SAGYO_GK,                                        " & vbNewLine _
                                                & " @TAX_KB,                                          " & vbNewLine _
                                                & " @SEIQTO_CD,                                       " & vbNewLine _
                                                & " @REMARK_ZAI,                                      " & vbNewLine _
                                                & " @REMARK_SKYU,                                     " & vbNewLine _
                                                & " @REMARK_SIJI,                                     " & vbNewLine _
                                                & " @SAGYO_COMP_CD,                                   " & vbNewLine _
                                                & " @SAGYO_COMP_DATE,                                 " & vbNewLine _
                                                & " @DEST_SAGYO_FLG,                                  " & vbNewLine _
                                                & " @ZAI_REC_NO,                                      " & vbNewLine _
                                                & " @PORA_ZAI_NB,                                     " & vbNewLine _
                                                & " @PORA_ZAI_QT,                                     " & vbNewLine _
                                                & " @SYS_ENT_DATE,                                    " & vbNewLine _
                                                & " @SYS_ENT_TIME,                                    " & vbNewLine _
                                                & " @SYS_ENT_PGID,                                    " & vbNewLine _
                                                & " @SYS_ENT_USER,                                    " & vbNewLine _
                                                & " @SYS_UPD_DATE,                                    " & vbNewLine _
                                                & " @SYS_UPD_TIME,                                    " & vbNewLine _
                                                & " @SYS_UPD_PGID,                                    " & vbNewLine _
                                                & " @SYS_UPD_USER,                                    " & vbNewLine _
                                                & " @SYS_DEL_FLG                                      " & vbNewLine _
                                                & " )                                                 " & vbNewLine

#End Region

#Region "作業レコードの削除"

    ''' <summary>
    ''' 作業レコードの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_UKIHOKOKU As String = " DELETE FROM $LM_TRN$..E_SAGYO                       " & vbNewLine _
                                                 & " WHERE                                               " & vbNewLine _
                                                 & "       E_SAGYO.NRS_BR_CD = @NRS_BR_CD                " & vbNewLine _
                                                 & "   AND E_SAGYO.SAGYO_REC_NO = @SAGYO_REC_NO          " & vbNewLine

#End Region

#End Region

#Region "Field"

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

#Region "SQLメイン処理"

#Region "作業指示書データの検索"

    ''' <summary>
    ''' 作業指示書データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataSagyoSiji(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LME040DAC.SQL_SELECT_SAGYOSIJI)       'SQL構築 SELECT句
        Me._StrSql.Append(LME040DAC.SQL_SELECT_FROM_SAGYOSIJI)  'SQL構築 FROM句
        Call SetSQLWhereDataSagyoSiji(inTbl.Rows(0))            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME040DAC", "SelectDataSagyoSiji", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_SIJI_NO", "SAGYO_SIJI_NO")
        map.Add("WH_CD", "WH_CD")
        map.Add("REMARK_1", "REMARK_1")
        map.Add("REMARK_2", "REMARK_2")
        map.Add("REMARK_3", "REMARK_3")
        map.Add("WH_TAB_STATUS", "WH_TAB_STATUS")
        map.Add("SAGYO_SIJI_STATUS", "SAGYO_SIJI_STATUS")
        map.Add("SAGYO_SIJI_DATE", "SAGYO_SIJI_DATE")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LME040INOUT_SAGYO_SIJI")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "作業データの検索"

    ''' <summary>
    ''' 作業データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME040IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LME040DAC.SQL_SELECT_SAGYO)       'SQL構築 SELECT句
        Me._StrSql.Append(LME040DAC.SQL_SELECT_FROM_SAGYO)  'SQL構築 FROM句
        Call SetSQLWhereDataSagyo(inTbl.Rows(0))            '条件設定
        Me._StrSql.Append(LME040DAC.SQL_SELECT_ORDER_SAGYO) 'SQL構築 FROM句

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME040DAC", "SelectDataSagyo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_REC_NO", "SAGYO_REC_NO")
        map.Add("SAGYO_COMP", "SAGYO_COMP")
        map.Add("SKYU_CHK", "SKYU_CHK")
        map.Add("SAGYO_SIJI_NO", "SAGYO_SIJI_NO")
        map.Add("INOUTKA_NO_LM", "INOUTKA_NO_LM")
        map.Add("WH_CD", "WH_CD")
        map.Add("IOZS_KB", "IOZS_KB")
        map.Add("SAGYO_CD", "SAGYO_CD")
        map.Add("SAGYO_NM", "SAGYO_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM_NRS", "GOODS_NM_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("INV_TANI", "INV_TANI")
        map.Add("SAGYO_NB", "SAGYO_NB")
        map.Add("SAGYO_UP", "SAGYO_UP")
        map.Add("SAGYO_GK", "SAGYO_GK")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("REMARK_ZAI", "REMARK_ZAI")
        map.Add("REMARK_SKYU", "REMARK_SKYU")
        map.Add("SAGYO_COMP_CD", "SAGYO_COMP_CD")
        map.Add("SAGYO_COMP_DATE", "SAGYO_COMP_DATE")
        map.Add("DEST_SAGYO_FLG", "DEST_SAGYO_FLG")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("IRIME", "IRIME")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("GOODS_COND_KB_1_NM", "GOODS_COND_KB_1_NM")
        map.Add("GOODS_COND_KB_2_NM", "GOODS_COND_KB_2_NM")
        map.Add("GOODS_COND_KB_3_NM", "GOODS_COND_KB_3_NM")
        map.Add("OFB_KB_NM", "OFB_KB_NM")
        map.Add("SPD_KB_NM", "SPD_KB_NM")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("DEST_CD_P", "DEST_CD_P")
        map.Add("ALLOC_PRIORITY_NM", "ALLOC_PRIORITY_NM")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("INKO_PLAN_DATE", "INKO_PLAN_DATE")
        map.Add("PORA_ZAI_NB_ZAI", "PORA_ZAI_NB_ZAI")
        map.Add("PORA_ZAI_QT_ZAI", "PORA_ZAI_QT_ZAI")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("KEY_NO", "KEY_NO")
        map.Add("REMARK_SIJI", "REMARK_SIJI")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LME040INOUT_SAGYO")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "作業指示書の追加"

    ''' <summary>
    ''' 作業指示書の追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSagyoSiji(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME040INOUT_SAGYO_SIJI")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LME040DAC.SQL_INSERT_SAGYOSIJI)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（個別項目）設定
            Call Me.SetInsSagyoSijiParameter(inTbl.Rows(i), Me._SqlPrmList)

            'SQLパラメータ（システム項目）設定
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LME040DAC", "InsertSagyoSiji", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region

#Region "作業指示書の更新"

    ''' <summary>
    ''' 作業指示書の更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSagyosiji(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME040INOUT_SAGYO_SIJI")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LME040DAC.SQL_UPDATE_SAGYOSIJI)         'SQL構築(UPDATE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの初期化
        cmd.Parameters.Clear()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ（個別項目）設定
        Call Me.SetUpdSagyoSijiParameter(inTbl.Rows(0), Me._SqlPrmList)

        'SQLパラメータ（システム項目）設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME040DAC", "UpdateSagyosiji", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, False)

        Return ds

    End Function

#End Region

#Region "作業指示書の削除"

    ''' <summary>
    ''' 作業指示書の更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSagyosiji(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME040INOUT_SAGYO_SIJI")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LME040DAC.SQL_DELETE_SAGYOSIJI)         'SQL構築(UPDATE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの初期化
        cmd.Parameters.Clear()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ（個別項目）設定
        Call Me.SetUpdSagyoSijiParameter(inTbl.Rows(0), Me._SqlPrmList)

        'SQLパラメータ（システム項目）設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME040DAC", "DeleteSagyosiji", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, False)

        Return ds

    End Function

#End Region

#Region "作業レコードの追加"

    ''' <summary>
    ''' 作業レコードの追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSagyoRec(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME040INOUT_SAGYO")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LME040DAC.SQL_INSERT_SAGYOREC)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（個別項目）設定
            Call Me.SetInsSagyoRecParameter(inTbl.Rows(i), Me._SqlPrmList)

            'SQLパラメータ（システム項目）設定
            Call Me.SetParamCommonSystemIns()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LME040DAC", "InsertSagyoRec", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region

#Region "作業レコードの削除"

    ''' <summary>
    ''' 作業レコードの削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSagyoRec(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME040INOUT_SAGYO")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LME040DAC.SQL_DELETE_UKIHOKOKU)      'SQL構築(Delete句)
        Call SetDelSagyoRecParameter(inTbl.Rows(0), Me._SqlPrmList)            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME040DAC", "DeleteSagyoRec", cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 作業指示書データの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereDataSagyoSiji(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("SAGYOSIJI.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYOSIJI.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            End If

            '作業指示書番号
            whereStr = .Item("SAGYO_SIJI_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYOSIJI.SAGYO_SIJI_NO = @SAGYO_SIJI_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Item("SAGYO_SIJI_NO").ToString(), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 作業データの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereDataSagyo(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("SAGYO.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYO.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            End If

            '作業指示書番号
            whereStr = .Item("SAGYO_SIJI_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYO.SAGYO_SIJI_NO = @SAGYO_SIJI_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Item("SAGYO_SIJI_NO").ToString(), DBDataType.NVARCHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYO.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SAGYO.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 作業指示書の追加"

    ''' <summary>
    ''' 作業指示書の追加
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInsSagyoSijiParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Item("SAGYO_SIJI_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_1", .Item("REMARK_1").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_2", .Item("REMARK_2").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_3", .Item("REMARK_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", .Item("WH_TAB_STATUS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_STATUS", .Item("SAGYO_SIJI_STATUS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_DATE", .Item("SAGYO_SIJI_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 作業指示書の更新"

    ''' <summary>
    ''' 作業指示書の更新
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdSagyoSijiParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Item("SAGYO_SIJI_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_1", .Item("REMARK_1").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_2", .Item("REMARK_2").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_3", .Item("REMARK_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", .Item("WH_TAB_STATUS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_STATUS", .Item("SAGYO_SIJI_STATUS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_DATE", .Item("SAGYO_SIJI_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 作業レコードの追加"

    ''' <summary>
    ''' 作業レコードの追加
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInsSagyoRecParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

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
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", .Item("SAGYO_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", .Item("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INV_TANI", .Item("INV_TANI").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", .Item("SAGYO_NB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_UP", .Item("SAGYO_UP").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", .Item("SAGYO_GK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_ZAI", .Item("REMARK_ZAI").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", .Item("REMARK_SKYU").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_SIJI", .Item("REMARK_SIJI").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", .Item("SAGYO_COMP_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", .Item("SAGYO_COMP_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_SAGYO_FLG", .Item("DEST_SAGYO_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", .Item("PORA_ZAI_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", .Item("PORA_ZAI_QT").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "SQL条件設定 作業レコードの削除"

    ''' <summary>
    ''' 作業レコードの削除
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDelSagyoRecParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Item("SAGYO_REC_NO").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

    End Sub

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

#Region "ユーティリティ"

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="setFlg">セットフラグ　False:通常のメッセージセット　True:一括更新のメッセージセット</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand, Optional ByVal setFlg As Boolean = False) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd), setFlg)

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="setFlg">セットフラグ　False:通常のメッセージセット　True:一括更新のメッセージセット</param>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer, Optional ByVal setFlg As Boolean = False) As Boolean

        '判定
        If cnt < 1 Then
            If setFlg = False Then
                MyBase.SetMessage("E011")
            Else
                MyBase.SetMessage("E011")
                'MyBase.SetMessageStore("00", "E011", , Me._Row.Item("ROW_NO").ToString())
            End If
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

End Class
