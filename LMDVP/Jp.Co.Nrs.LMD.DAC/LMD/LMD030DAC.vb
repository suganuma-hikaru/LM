' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD030    : 在庫履歴
'  作  成  者       :  [金ヘスル]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD030DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD030DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    'START YANAI 要望番号1044
    '''' <summary>
    '''' 在庫移動トランザクションデータ抽出用(移入用)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_IN As String = " SELECT                                                               " & vbNewLine _
    '                                           & "  ''                                             AS STATE_KB          " & vbNewLine _
    '                                           & " ,'移入'                                         AS SYUBETU           " & vbNewLine _
    '                                           & " ,D_IDO_TRS.IDO_DATE                             AS IDO_DATE          " & vbNewLine _
    '                                           & " ,D_IDO_TRS.N_PORA_ZAI_NB                        AS INKA_NB           " & vbNewLine _
    '                                           & " ,D_IDO_TRS.N_PORA_ZAI_NB * M_GOODS.STD_IRIME_NB AS INKA_QT           " & vbNewLine _
    '                                           & " ,0                                              AS OUTKA_NB          " & vbNewLine _
    '                                           & " ,0                                              AS OUTKA_QT          " & vbNewLine _
    '                                           & " ,0                                              AS BACKLOG_NB        " & vbNewLine _
    '                                           & " ,0                                              AS BACKLOG_QT        " & vbNewLine _
    '                                           & " ,M_GOODS.PKG_UT                                 AS PKG_UT            " & vbNewLine _
    '                                           & " ,M_GOODS.STD_IRIME_UT                           AS STD_IRIME_UT      " & vbNewLine _
    '                                           & " ,D_ZAI_TRS.TOU_NO                               AS TOU_NO            " & vbNewLine _
    '                                           & " ,D_ZAI_TRS.SITU_NO                              AS SITU_NO           " & vbNewLine _
    '                                           & " ,D_ZAI_TRS.ZONE_CD                              AS ZONE_CD           " & vbNewLine _
    '                                           & " ,D_ZAI_TRS.LOCA                                 AS LOCA              " & vbNewLine _
    '                                           & " ,D_ZAI_TRS.INKA_NO_L + '_'                                           " & vbNewLine _
    '                                           & "                     + D_ZAI_TRS.INKA_NO_M + '_'                      " & vbNewLine _
    '                                           & "                     + D_ZAI_TRS.INKA_NO_S       AS INOUTKA_NO_L      " & vbNewLine _
    '                                           & " ,D_IDO_TRS.O_ZAI_REC_NO                         AS O_ZAI_REC_NO      " & vbNewLine _
    '                                           & " ,D_IDO_TRS.N_ZAI_REC_NO                         AS N_ZAI_REC_NO      " & vbNewLine _
    '                                           & " ,Z1.KBN_NM1                                     AS REMARK_KBN        " & vbNewLine _
    '                                           & " ,''                                             AS CUST_ORD_NO       " & vbNewLine _
    '                                           & " ,''                                             AS BUYER_ORD_NO      " & vbNewLine _
    '                                           & " ,''                                             AS UNSOCO_NM         " & vbNewLine _
    '                                           & " ,D_IDO_TRS.REMARK                               AS REMARK            " & vbNewLine _
    '                                           & " ,''                                             AS REMARK_OUT        " & vbNewLine _
    '                                           & " ,Z2.KBN_NM1                                     AS GOODS_COND_NM_1   " & vbNewLine _
    '                                           & " ,Z3.KBN_NM1                                     AS GOODS_COND_NM_2   " & vbNewLine _
    '                                           & " ,M_CUSTCOND.JOTAI_NM                            AS GOODS_COND_NM_3   " & vbNewLine _
    '                                           & " ,Z4.KBN_NM1                                     AS SPD_KB_NM         " & vbNewLine _
    '                                           & " ,Z5.KBN_NM1                                     AS OFB_KB_NM         " & vbNewLine _
    '                                           & " ,Z6.KBN_NM1                                     AS ALLOC_PRIORITY_NM " & vbNewLine _
    '                                           & " ,''                                             AS DEST_NM           " & vbNewLine _
    '                                           & " ,D_ZAI_TRS.RSV_NO                               AS RSV_NO            " & vbNewLine _
    '                                           & " ,'2'                                            AS SORT_KEY          " & vbNewLine _
    '                                           & " ,M_GOODS.STD_IRIME_NB                           AS STD_IRIME_NB      " & vbNewLine _
    '                                           & " ,D_ZAI_TRS.PORA_ZAI_NB                          AS PORA_ZAI_NB       " & vbNewLine _
    '                                           & " ,D_ZAI_TRS.ALLOC_CAN_NB                         AS ALLOC_CAN_NB      " & vbNewLine _
    '                                           & " ,D_IDO_TRS.SYS_UPD_DATE                         AS IDO_SYS_UPD_DATE  " & vbNewLine _
    '                                           & " ,D_IDO_TRS.SYS_UPD_TIME                         AS IDO_SYS_UPD_TIME  " & vbNewLine _
    '                                           & " ,D_ZAI_TRS_O.SYS_UPD_DATE                       AS O_ZAI_SYS_UPD_DATE" & vbNewLine _
    '                                           & " ,D_ZAI_TRS_O.SYS_UPD_TIME                       AS O_ZAI_SYS_UPD_TIME" & vbNewLine _
    '                                           & " ,D_ZAI_TRS.SYS_UPD_DATE                         AS N_ZAI_SYS_UPD_DATE" & vbNewLine _
    '                                           & " ,D_ZAI_TRS.SYS_UPD_TIME                         AS N_ZAI_SYS_UPD_TIME" & vbNewLine _
    '                                           & " ,@NRS_BR_CD                                     AS NRS_BR_CD         " & vbNewLine _
    '                                           & " ,CUST.HOKAN_SEIQTO_CD                           AS HOKAN_SEIQTO_CD   " & vbNewLine
    ''' <summary>
    ''' 在庫移動トランザクションデータ抽出用(移入用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_IN As String = " SELECT                                                               " & vbNewLine _
                                               & "  ''                                             AS STATE_KB          " & vbNewLine _
                                               & " ,'移入'                                         AS SYUBETU           " & vbNewLine _
                                               & " ,D_IDO_TRS.IDO_DATE                             AS IDO_DATE          " & vbNewLine _
                                               & " ,D_IDO_TRS.N_PORA_ZAI_NB                        AS INKA_NB           " & vbNewLine _
                                               & " ,D_IDO_TRS.N_PORA_ZAI_NB * D_ZAI_TRS.IRIME      AS INKA_QT           " & vbNewLine _
                                               & " ,0                                              AS OUTKA_NB          " & vbNewLine _
                                               & " ,0                                              AS OUTKA_QT          " & vbNewLine _
                                               & " ,0                                              AS BACKLOG_NB        " & vbNewLine _
                                               & " ,0                                              AS BACKLOG_QT        " & vbNewLine _
                                               & " ,M_GOODS.PKG_UT                                 AS PKG_UT            " & vbNewLine _
                                               & " ,M_GOODS.STD_IRIME_UT                           AS STD_IRIME_UT      " & vbNewLine _
                                               & " ,D_ZAI_TRS.TOU_NO                               AS TOU_NO            " & vbNewLine _
                                               & " ,D_ZAI_TRS.SITU_NO                              AS SITU_NO           " & vbNewLine _
                                               & " ,D_ZAI_TRS.ZONE_CD                              AS ZONE_CD           " & vbNewLine _
                                               & " ,D_ZAI_TRS.LOCA                                 AS LOCA              " & vbNewLine _
                                               & " ,D_ZAI_TRS.INKA_NO_L + '_'                                           " & vbNewLine _
                                               & "                     + D_ZAI_TRS.INKA_NO_M + '_'                      " & vbNewLine _
                                               & "                     + D_ZAI_TRS.INKA_NO_S       AS INOUTKA_NO_L      " & vbNewLine _
                                               & " ,D_IDO_TRS.O_ZAI_REC_NO                         AS O_ZAI_REC_NO      " & vbNewLine _
                                               & " ,D_IDO_TRS.N_ZAI_REC_NO                         AS N_ZAI_REC_NO      " & vbNewLine _
                                               & " ,Z1.KBN_NM1                                     AS REMARK_KBN        " & vbNewLine _
                                               & " ,''                                             AS CUST_ORD_NO       " & vbNewLine _
                                               & " ,''                                             AS BUYER_ORD_NO      " & vbNewLine _
                                               & " ,''                                             AS UNSOCO_NM         " & vbNewLine _
                                               & " ,D_IDO_TRS.REMARK                               AS REMARK            " & vbNewLine _
                                               & " ,''                                             AS REMARK_OUT        " & vbNewLine _
                                               & " ,Z2.KBN_NM1                                     AS GOODS_COND_NM_1   " & vbNewLine _
                                               & " ,Z3.KBN_NM1                                     AS GOODS_COND_NM_2   " & vbNewLine _
                                               & " ,M_CUSTCOND.JOTAI_NM                            AS GOODS_COND_NM_3   " & vbNewLine _
                                               & " ,Z4.KBN_NM1                                     AS SPD_KB_NM         " & vbNewLine _
                                               & " ,Z5.KBN_NM1                                     AS OFB_KB_NM         " & vbNewLine _
                                               & " ,Z6.KBN_NM1                                     AS ALLOC_PRIORITY_NM " & vbNewLine _
                                               & " ,''                                             AS DEST_NM           " & vbNewLine _
                                               & " ,D_ZAI_TRS.RSV_NO                               AS RSV_NO            " & vbNewLine _
                                               & " ,'2'                                            AS SORT_KEY          " & vbNewLine _
                                               & " ,D_IDO_TRS.ZAIK_IRIME                           AS STD_IRIME_NB      " & vbNewLine _
                                               & " ,D_ZAI_TRS.PORA_ZAI_NB                          AS PORA_ZAI_NB       " & vbNewLine _
                                               & " ,D_ZAI_TRS.ALLOC_CAN_NB                         AS ALLOC_CAN_NB      " & vbNewLine _
                                               & " ,D_IDO_TRS.SYS_UPD_DATE                         AS IDO_SYS_UPD_DATE  " & vbNewLine _
                                               & " ,D_IDO_TRS.SYS_UPD_TIME                         AS IDO_SYS_UPD_TIME  " & vbNewLine _
                                               & " ,D_ZAI_TRS_O.SYS_UPD_DATE                       AS O_ZAI_SYS_UPD_DATE" & vbNewLine _
                                               & " ,D_ZAI_TRS_O.SYS_UPD_TIME                       AS O_ZAI_SYS_UPD_TIME" & vbNewLine _
                                               & " ,D_ZAI_TRS.SYS_UPD_DATE                         AS N_ZAI_SYS_UPD_DATE" & vbNewLine _
                                               & " ,D_ZAI_TRS.SYS_UPD_TIME                         AS N_ZAI_SYS_UPD_TIME" & vbNewLine _
                                               & " ,@NRS_BR_CD                                     AS NRS_BR_CD         " & vbNewLine _
                                               & " ,CUST.HOKAN_SEIQTO_CD                           AS HOKAN_SEIQTO_CD   " & vbNewLine
    'END YANAI 要望番号1044

    'START YANAI 要望番号1044
    '''' <summary>
    '''' 在庫移動トランザクションデータ抽出用(移出用)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_OUT As String = " SELECT                                                               " & vbNewLine _
    '                                            & "  ''                                             AS STATE_KB          " & vbNewLine _
    '                                            & " ,'移出'                                         AS SYUBETU           " & vbNewLine _
    '                                            & " ,D_IDO_TRS.IDO_DATE                             AS IDO_DATE          " & vbNewLine _
    '                                            & " ,0                                              AS INKA_NB           " & vbNewLine _
    '                                            & " ,0                                              AS INKA_QT           " & vbNewLine _
    '                                            & " ,D_IDO_TRS.N_PORA_ZAI_NB                        AS OUTKA_NB          " & vbNewLine _
    '                                            & " ,D_IDO_TRS.N_PORA_ZAI_NB * M_GOODS.STD_IRIME_NB AS OUTKA_QT          " & vbNewLine _
    '                                            & " ,0                                              AS BACKLOG_NB        " & vbNewLine _
    '                                            & " ,0                                              AS BACKLOG_QT        " & vbNewLine _
    '                                            & " ,M_GOODS.PKG_UT                                 AS PKG_UT            " & vbNewLine _
    '                                            & " ,M_GOODS.STD_IRIME_UT                           AS STD_IRIME_UT      " & vbNewLine _
    '                                            & " ,D_ZAI_TRS.TOU_NO                               AS TOU_NO            " & vbNewLine _
    '                                            & " ,D_ZAI_TRS.SITU_NO                              AS SITU_NO           " & vbNewLine _
    '                                            & " ,D_ZAI_TRS.ZONE_CD                              AS ZONE_CD           " & vbNewLine _
    '                                            & " ,D_ZAI_TRS.LOCA                                 AS LOCA              " & vbNewLine _
    '                                            & " ,D_ZAI_TRS.INKA_NO_L + '_'                                           " & vbNewLine _
    '                                            & "                     + D_ZAI_TRS.INKA_NO_M + '_'                      " & vbNewLine _
    '                                            & "                     + D_ZAI_TRS.INKA_NO_S       AS INOUTKA_NO_L      " & vbNewLine _
    '                                            & " ,D_IDO_TRS.O_ZAI_REC_NO                         AS O_ZAI_REC_NO      " & vbNewLine _
    '                                            & " ,D_IDO_TRS.N_ZAI_REC_NO                         AS N_ZAI_REC_NO      " & vbNewLine _
    '                                            & " ,Z1.KBN_NM1                                     AS REMARK_KBN        " & vbNewLine _
    '                                            & " ,''                                             AS CUST_ORD_NO       " & vbNewLine _
    '                                            & " ,''                                             AS BUYER_ORD_NO      " & vbNewLine _
    '                                            & " ,''                                             AS UNSOCO_NM         " & vbNewLine _
    '                                            & " ,D_IDO_TRS.REMARK                               AS REMARK            " & vbNewLine _
    '                                            & " ,''                                             AS REMARK_OUT        " & vbNewLine _
    '                                            & " ,Z2.KBN_NM1                                     AS GOODS_COND_NM_1   " & vbNewLine _
    '                                            & " ,Z3.KBN_NM1                                     AS GOODS_COND_NM_2   " & vbNewLine _
    '                                            & " ,M_CUSTCOND.JOTAI_NM                            AS GOODS_COND_NM_3   " & vbNewLine _
    '                                            & " ,Z4.KBN_NM1                                     AS SPD_KB_NM         " & vbNewLine _
    '                                            & " ,Z5.KBN_NM1                                     AS OFB_KB_NM         " & vbNewLine _
    '                                            & " ,Z6.KBN_NM1                                     AS ALLOC_PRIORITY_NM " & vbNewLine _
    '                                            & " ,M_DEST.DEST_NM                                 AS DEST_NM           " & vbNewLine _
    '                                            & " ,D_ZAI_TRS.RSV_NO                               AS RSV_NO            " & vbNewLine _
    '                                            & " ,'3'                                            AS SORT_KEY          " & vbNewLine _
    '                                            & " ,M_GOODS.STD_IRIME_NB                           AS STD_IRIME_NB      " & vbNewLine _
    '                                            & " ,D_ZAI_TRS.PORA_ZAI_NB                          AS PORA_ZAI_NB       " & vbNewLine _
    '                                            & " ,D_ZAI_TRS.ALLOC_CAN_NB                         AS ALLOC_CAN_NB      " & vbNewLine _
    '                                            & " ,D_IDO_TRS.SYS_UPD_DATE                         AS IDO_SYS_UPD_DATE  " & vbNewLine _
    '                                            & " ,D_IDO_TRS.SYS_UPD_TIME                         AS IDO_SYS_UPD_TIME  " & vbNewLine _
    '                                            & " ,''                                             AS O_ZAI_SYS_UPD_DATE" & vbNewLine _
    '                                            & " ,''                                             AS O_ZAI_SYS_UPD_TIME" & vbNewLine _
    '                                            & " ,''                                             AS N_ZAI_SYS_UPD_DATE" & vbNewLine _
    '                                            & " ,''                                             AS N_ZAI_SYS_UPD_TIME" & vbNewLine _
    '                                            & " ,@NRS_BR_CD                                     AS NRS_BR_CD         " & vbNewLine _
    '                                            & " ,CUST.HOKAN_SEIQTO_CD                           AS HOKAN_SEIQTO_CD   " & vbNewLine
    ''' <summary>
    ''' 在庫移動トランザクションデータ抽出用(移出用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_OUT As String = " SELECT                                                               " & vbNewLine _
                                                & "  ''                                             AS STATE_KB          " & vbNewLine _
                                                & " ,'移出'                                         AS SYUBETU           " & vbNewLine _
                                                & " ,D_IDO_TRS.IDO_DATE                             AS IDO_DATE          " & vbNewLine _
                                                & " ,0                                              AS INKA_NB           " & vbNewLine _
                                                & " ,0                                              AS INKA_QT           " & vbNewLine _
                                                & " ,D_IDO_TRS.N_PORA_ZAI_NB                        AS OUTKA_NB          " & vbNewLine _
                                                & " ,D_IDO_TRS.N_PORA_ZAI_NB * D_ZAI_TRS.IRIME      AS OUTKA_QT          " & vbNewLine _
                                                & " ,0                                              AS BACKLOG_NB        " & vbNewLine _
                                                & " ,0                                              AS BACKLOG_QT        " & vbNewLine _
                                                & " ,M_GOODS.PKG_UT                                 AS PKG_UT            " & vbNewLine _
                                                & " ,M_GOODS.STD_IRIME_UT                           AS STD_IRIME_UT      " & vbNewLine _
                                                & " ,D_ZAI_TRS.TOU_NO                               AS TOU_NO            " & vbNewLine _
                                                & " ,D_ZAI_TRS.SITU_NO                              AS SITU_NO           " & vbNewLine _
                                                & " ,D_ZAI_TRS.ZONE_CD                              AS ZONE_CD           " & vbNewLine _
                                                & " ,D_ZAI_TRS.LOCA                                 AS LOCA              " & vbNewLine _
                                                & " ,D_ZAI_TRS.INKA_NO_L + '_'                                           " & vbNewLine _
                                                & "                     + D_ZAI_TRS.INKA_NO_M + '_'                      " & vbNewLine _
                                                & "                     + D_ZAI_TRS.INKA_NO_S       AS INOUTKA_NO_L      " & vbNewLine _
                                                & " ,D_IDO_TRS.O_ZAI_REC_NO                         AS O_ZAI_REC_NO      " & vbNewLine _
                                                & " ,D_IDO_TRS.N_ZAI_REC_NO                         AS N_ZAI_REC_NO      " & vbNewLine _
                                                & " ,Z1.KBN_NM1                                     AS REMARK_KBN        " & vbNewLine _
                                                & " ,''                                             AS CUST_ORD_NO       " & vbNewLine _
                                                & " ,''                                             AS BUYER_ORD_NO      " & vbNewLine _
                                                & " ,''                                             AS UNSOCO_NM         " & vbNewLine _
                                                & " ,D_IDO_TRS.REMARK                               AS REMARK            " & vbNewLine _
                                                & " ,''                                             AS REMARK_OUT        " & vbNewLine _
                                                & " ,Z2.KBN_NM1                                     AS GOODS_COND_NM_1   " & vbNewLine _
                                                & " ,Z3.KBN_NM1                                     AS GOODS_COND_NM_2   " & vbNewLine _
                                                & " ,M_CUSTCOND.JOTAI_NM                            AS GOODS_COND_NM_3   " & vbNewLine _
                                                & " ,Z4.KBN_NM1                                     AS SPD_KB_NM         " & vbNewLine _
                                                & " ,Z5.KBN_NM1                                     AS OFB_KB_NM         " & vbNewLine _
                                                & " ,Z6.KBN_NM1                                     AS ALLOC_PRIORITY_NM " & vbNewLine _
                                                & " ,M_DEST.DEST_NM                                 AS DEST_NM           " & vbNewLine _
                                                & " ,D_ZAI_TRS.RSV_NO                               AS RSV_NO            " & vbNewLine _
                                                & " ,'3'                                            AS SORT_KEY          " & vbNewLine _
                                                & " ,D_IDO_TRS.ZAIK_IRIME                           AS STD_IRIME_NB      " & vbNewLine _
                                                & " ,D_ZAI_TRS.PORA_ZAI_NB                          AS PORA_ZAI_NB       " & vbNewLine _
                                                & " ,D_ZAI_TRS.ALLOC_CAN_NB                         AS ALLOC_CAN_NB      " & vbNewLine _
                                                & " ,D_IDO_TRS.SYS_UPD_DATE                         AS IDO_SYS_UPD_DATE  " & vbNewLine _
                                                & " ,D_IDO_TRS.SYS_UPD_TIME                         AS IDO_SYS_UPD_TIME  " & vbNewLine _
                                                & " ,''                                             AS O_ZAI_SYS_UPD_DATE" & vbNewLine _
                                                & " ,''                                             AS O_ZAI_SYS_UPD_TIME" & vbNewLine _
                                                & " ,''                                             AS N_ZAI_SYS_UPD_DATE" & vbNewLine _
                                                & " ,''                                             AS N_ZAI_SYS_UPD_TIME" & vbNewLine _
                                                & " ,@NRS_BR_CD                                     AS NRS_BR_CD         " & vbNewLine _
                                                & " ,CUST.HOKAN_SEIQTO_CD                           AS HOKAN_SEIQTO_CD   " & vbNewLine
    'END YANAI 要望番号1044

    'START YANAI 要望番号1044
    '''' <summary>
    '''' 入荷データ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_2 As String = " SELECT                                                                        " & vbNewLine _
    '                                          & " ''                                                       AS STATE_KB          " & vbNewLine _
    '                                          & " ,'入荷'                                                  AS SYUBETU           " & vbNewLine _
    '                                          & " ,D_ZAI_TRS.INKO_DATE                                     AS IDO_DATE          " & vbNewLine _
    '                                          & " ,ISNULL(B_INKA_S.KONSU , 0)                                                   " & vbNewLine _
    '                                          & "    * ISNULL(M_GOODS.PKG_NB , 0)                                               " & vbNewLine _
    '                                          & "    + ISNULL(B_INKA_S.HASU  , 0)                          AS INKA_NB           " & vbNewLine _
    '                                          & " ,(ISNULL(B_INKA_S.KONSU , 0)                                                  " & vbNewLine _
    '                                          & "    * ISNULL(M_GOODS.PKG_NB , 0)                                               " & vbNewLine _
    '                                          & "    + ISNULL(B_INKA_S.HASU  , 0)) * M_GOODS.STD_IRIME_NB  AS INKA_QT           " & vbNewLine _
    '                                          & " ,0                                                       AS OUTKA_NB          " & vbNewLine _
    '                                          & " ,0                                                       AS OUTKA_QT          " & vbNewLine _
    '                                          & " ,0                                                       AS BACKLOG_NB        " & vbNewLine _
    '                                          & " ,0                                                       AS BACKLOG_QT        " & vbNewLine _
    '                                          & " ,M_GOODS.PKG_UT                                          AS PKG_UT            " & vbNewLine _
    '                                          & " ,M_GOODS.STD_IRIME_UT                                    AS STD_IRIME_UT      " & vbNewLine _
    '                                          & " ,B_INKA_S.TOU_NO                                         AS TOU_NO            " & vbNewLine _
    '                                          & " ,B_INKA_S.SITU_NO                                        AS SITU_NO           " & vbNewLine _
    '                                          & " ,B_INKA_S.ZONE_CD                                        AS ZONE_CD           " & vbNewLine _
    '                                          & " ,B_INKA_S.LOCA                                           AS LOCA              " & vbNewLine _
    '                                          & " ,  B_INKA_L.INKA_NO_L + '_'                                                   " & vbNewLine _
    '                                          & "  + B_INKA_M.INKA_NO_M + '_'                                                   " & vbNewLine _
    '                                          & "  + B_INKA_S.INKA_NO_S                                    AS INOUTKA_NO_L      " & vbNewLine _
    '                                          & " ,B_INKA_S.ZAI_REC_NO                                     AS O_ZAI_REC_NO      " & vbNewLine _
    '                                          & " ,''                                                      AS N_ZAI_REC_NO      " & vbNewLine _
    '                                          & " ,''                                                      AS REMARK_KBN        " & vbNewLine _
    '                                          & " ,CASE WHEN RTRIM(B_INKA_M.OUTKA_FROM_ORD_NO_M) <> ''                          " _
    '                                          & "          THEN B_INKA_M.OUTKA_FROM_ORD_NO_M                                    " & vbNewLine _
    '                                          & "       WHEN RTRIM(B_INKA_L.OUTKA_FROM_ORD_NO_L) <> ''                          " _
    '                                          & "          THEN B_INKA_L.OUTKA_FROM_ORD_NO_L                                    " & vbNewLine _
    '                                          & "       ELSE ''                                        END AS CUST_ORD_NO       " & vbNewLine _
    '                                          & " ,CASE WHEN RTRIM(B_INKA_M.BUYER_ORD_NO_M) <> ''                               " _
    '                                          & "          THEN B_INKA_M.BUYER_ORD_NO_M                                         " & vbNewLine _
    '                                          & "       WHEN RTRIM(B_INKA_L.BUYER_ORD_NO_L) <> ''                               " _
    '                                          & "          THEN B_INKA_L.BUYER_ORD_NO_L                                         " & vbNewLine _
    '                                          & "       ELSE ''                                        END AS BUYER_ORD_NO      " & vbNewLine _
    '                                          & " ,M_UNSOCO.UNSOCO_NM                                      AS UNSOCO_NM         " & vbNewLine _
    '                                          & " ,B_INKA_S.REMARK                                         AS REMARK            " & vbNewLine _
    '                                          & " ,B_INKA_S.REMARK_OUT                                       AS REMARK_OUT      " & vbNewLine _
    '                                          & " ,Z2.KBN_NM1                                              AS GOODS_COND_NM_1   " & vbNewLine _
    '                                          & " ,Z3.KBN_NM1                                              AS GOODS_COND_NM_2   " & vbNewLine _
    '                                          & " ,M_CUSTCOND.JOTAI_NM                                     AS GOODS_COND_NM_3   " & vbNewLine _
    '                                          & " ,Z4.KBN_NM1                                              AS SPD_KB_NM         " & vbNewLine _
    '                                          & " ,Z5.KBN_NM1                                              AS OFB_KB_NM         " & vbNewLine _
    '                                          & " ,Z6.KBN_NM1                                              AS ALLOC_PRIORITY_NM " & vbNewLine _
    '                                          & " ,M_DEST.DEST_NM                                          AS DEST_NM           " & vbNewLine _
    '                                          & " ,''                                                      AS RSV_NO            " & vbNewLine _
    '                                          & " ,'1'                                                     AS SORT_KEY          " & vbNewLine _
    '                                          & " ,M_GOODS.STD_IRIME_NB                                    AS STD_IRIME_NB      " & vbNewLine _
    '                                          & " ,0                                                       AS PORA_ZAI_NB       " & vbNewLine _
    '                                          & " ,0                                                       AS ALLOC_CAN_NB      " & vbNewLine _
    '                                          & " ,''                                                      AS IDO_SYS_UPD_DATE  " & vbNewLine _
    '                                          & " ,''                                                      AS IDO_SYS_UPD_TIME  " & vbNewLine _
    '                                          & " ,''                                                      AS O_ZAI_SYS_UPD_DATE" & vbNewLine _
    '                                          & " ,''                                                      AS O_ZAI_SYS_UPD_TIME" & vbNewLine _
    '                                          & " ,''                                                      AS N_ZAI_SYS_UPD_DATE" & vbNewLine _
    '                                          & " ,''                                                      AS N_ZAI_SYS_UPD_TIME" & vbNewLine _
    '                                          & " ,@NRS_BR_CD                                              AS NRS_BR_CD         " & vbNewLine _
    '                                          & " ,CUST.HOKAN_SEIQTO_CD                                    AS HOKAN_SEIQTO_CD   " & vbNewLine
    ''' <summary>
    ''' 入荷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_2 As String = " SELECT                                                                        " & vbNewLine _
                                              & " ''                                                       AS STATE_KB          " & vbNewLine _
                                              & " ,'入荷'                                                  AS SYUBETU           " & vbNewLine _
                                              & " ,D_ZAI_TRS.INKO_DATE                                     AS IDO_DATE          " & vbNewLine _
                                              & " ,ISNULL(B_INKA_S.KONSU , 0)                                                   " & vbNewLine _
                                              & "    * ISNULL(M_GOODS.PKG_NB , 0)                                               " & vbNewLine _
                                              & "    + ISNULL(B_INKA_S.HASU  , 0)                          AS INKA_NB           " & vbNewLine _
                                              & " ,(ISNULL(B_INKA_S.KONSU , 0)                                                  " & vbNewLine _
                                              & "    * ISNULL(M_GOODS.PKG_NB , 0)                                               " & vbNewLine _
                                              & "    + ISNULL(B_INKA_S.HASU  , 0)) * D_ZAI_TRS.IRIME       AS INKA_QT           " & vbNewLine _
                                              & " ,0                                                       AS OUTKA_NB          " & vbNewLine _
                                              & " ,0                                                       AS OUTKA_QT          " & vbNewLine _
                                              & " ,0                                                       AS BACKLOG_NB        " & vbNewLine _
                                              & " ,0                                                       AS BACKLOG_QT        " & vbNewLine _
                                              & " ,M_GOODS.PKG_UT                                          AS PKG_UT            " & vbNewLine _
                                              & " ,M_GOODS.STD_IRIME_UT                                    AS STD_IRIME_UT      " & vbNewLine _
                                              & " ,B_INKA_S.TOU_NO                                         AS TOU_NO            " & vbNewLine _
                                              & " ,B_INKA_S.SITU_NO                                        AS SITU_NO           " & vbNewLine _
                                              & " ,B_INKA_S.ZONE_CD                                        AS ZONE_CD           " & vbNewLine _
                                              & " ,B_INKA_S.LOCA                                           AS LOCA              " & vbNewLine _
                                              & " ,  B_INKA_L.INKA_NO_L + '_'                                                   " & vbNewLine _
                                              & "  + B_INKA_M.INKA_NO_M + '_'                                                   " & vbNewLine _
                                              & "  + B_INKA_S.INKA_NO_S                                    AS INOUTKA_NO_L      " & vbNewLine _
                                              & " ,B_INKA_S.ZAI_REC_NO                                     AS O_ZAI_REC_NO      " & vbNewLine _
                                              & " ,''                                                      AS N_ZAI_REC_NO      " & vbNewLine _
                                              & " ,''                                                      AS REMARK_KBN        " & vbNewLine _
                                              & " ,CASE WHEN RTRIM(B_INKA_M.OUTKA_FROM_ORD_NO_M) <> ''                          " _
                                              & "          THEN B_INKA_M.OUTKA_FROM_ORD_NO_M                                    " & vbNewLine _
                                              & "       WHEN RTRIM(B_INKA_L.OUTKA_FROM_ORD_NO_L) <> ''                          " _
                                              & "          THEN B_INKA_L.OUTKA_FROM_ORD_NO_L                                    " & vbNewLine _
                                              & "       ELSE ''                                        END AS CUST_ORD_NO       " & vbNewLine _
                                              & " ,CASE WHEN RTRIM(B_INKA_M.BUYER_ORD_NO_M) <> ''                               " _
                                              & "          THEN B_INKA_M.BUYER_ORD_NO_M                                         " & vbNewLine _
                                              & "       WHEN RTRIM(B_INKA_L.BUYER_ORD_NO_L) <> ''                               " _
                                              & "          THEN B_INKA_L.BUYER_ORD_NO_L                                         " & vbNewLine _
                                              & "       ELSE ''                                        END AS BUYER_ORD_NO      " & vbNewLine _
                                              & " ,M_UNSOCO.UNSOCO_NM                                      AS UNSOCO_NM         " & vbNewLine _
                                              & " ,B_INKA_S.REMARK                                         AS REMARK            " & vbNewLine _
                                              & " ,B_INKA_S.REMARK_OUT                                       AS REMARK_OUT      " & vbNewLine _
                                              & " ,Z2.KBN_NM1                                              AS GOODS_COND_NM_1   " & vbNewLine _
                                              & " ,Z3.KBN_NM1                                              AS GOODS_COND_NM_2   " & vbNewLine _
                                              & " ,M_CUSTCOND.JOTAI_NM                                     AS GOODS_COND_NM_3   " & vbNewLine _
                                              & " ,Z4.KBN_NM1                                              AS SPD_KB_NM         " & vbNewLine _
                                              & " ,Z5.KBN_NM1                                              AS OFB_KB_NM         " & vbNewLine _
                                              & " ,Z6.KBN_NM1                                              AS ALLOC_PRIORITY_NM " & vbNewLine _
                                              & " ,M_DEST.DEST_NM                                          AS DEST_NM           " & vbNewLine _
                                              & " ,''                                                      AS RSV_NO            " & vbNewLine _
                                              & " ,'1'                                                     AS SORT_KEY          " & vbNewLine _
                                              & " ,D_ZAI_TRS.IRIME                                         AS STD_IRIME_NB      " & vbNewLine _
                                              & " ,0                                                       AS PORA_ZAI_NB       " & vbNewLine _
                                              & " ,0                                                       AS ALLOC_CAN_NB      " & vbNewLine _
                                              & " ,''                                                      AS IDO_SYS_UPD_DATE  " & vbNewLine _
                                              & " ,''                                                      AS IDO_SYS_UPD_TIME  " & vbNewLine _
                                              & " ,''                                                      AS O_ZAI_SYS_UPD_DATE" & vbNewLine _
                                              & " ,''                                                      AS O_ZAI_SYS_UPD_TIME" & vbNewLine _
                                              & " ,''                                                      AS N_ZAI_SYS_UPD_DATE" & vbNewLine _
                                              & " ,''                                                      AS N_ZAI_SYS_UPD_TIME" & vbNewLine _
                                              & " ,@NRS_BR_CD                                              AS NRS_BR_CD         " & vbNewLine _
                                              & " ,CUST.HOKAN_SEIQTO_CD                                    AS HOKAN_SEIQTO_CD   " & vbNewLine
    'END YANAI 要望番号1044

    'START YANAI 要望番号1044
    '''' <summary>
    '''' 出荷データ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_3 As String = "  SELECT                                                                         " & vbNewLine _
    '                                          & "  CASE WHEN C_OUTKA_L.SYS_DEL_FLG = '1' THEN '消'                                " & vbNewLine _
    '                                          & "  WHEN C_OUTKA_L.OUTKA_STATE_KB in ('10', '30', '40') THEN '予'                  " & vbNewLine _
    '                                          & "  ELSE ''                                             END AS STATE_KB            " & vbNewLine _
    '                                          & " ,CASE WHEN RTRIM(C_OUTKA_L.FURI_NO) <> '' THEN '振替'                           " & vbNewLine _
    '                                          & "  ELSE '出荷'                                         END AS SYUBETU             " & vbNewLine _
    '                                          & " ,C_OUTKA_L.OUTKA_PLAN_DATE                               AS IDO_DATE            " & vbNewLine _
    '                                          & " ,0                                                       AS INKA_NB             " & vbNewLine _
    '                                          & " ,0                                                       AS INKA_QT             " & vbNewLine _
    '                                          & " ,C_OUTKA_S.ALCTD_NB                                      AS OUTKA_NB            " & vbNewLine _
    '                                          & " ,C_OUTKA_S.ALCTD_QT                                      AS OUTKA_QT            " & vbNewLine _
    '                                          & " ,0                                                       AS BACKLOG_NB          " & vbNewLine _
    '                                          & " ,0                                                       AS BACKLOG_QT          " & vbNewLine _
    '                                          & " ,M_GOODS.PKG_UT                                          AS PKG_UT              " & vbNewLine _
    '                                          & " ,M_GOODS.STD_IRIME_UT                                    AS STD_IRIME_UT        " & vbNewLine _
    '                                          & " ,C_OUTKA_S.TOU_NO                                        AS TOU_NO              " & vbNewLine _
    '                                          & " ,C_OUTKA_S.SITU_NO                                       AS SITU_NO             " & vbNewLine _
    '                                          & " ,C_OUTKA_S.ZONE_CD                                       AS ZONE_CD             " & vbNewLine _
    '                                          & " ,C_OUTKA_S.LOCA                                          AS LOCA                " & vbNewLine _
    '                                          & " ,  C_OUTKA_S.OUTKA_NO_L + '_'                                                   " & vbNewLine _
    '                                          & "  + C_OUTKA_S.OUTKA_NO_M + '_'                                                   " & vbNewLine _
    '                                          & "  + C_OUTKA_S.OUTKA_NO_S                                  AS INOUTKA_NO_L        " & vbNewLine _
    '                                          & " ,C_OUTKA_S.ZAI_REC_NO                                    AS O_ZAI_REC_NO        " & vbNewLine _
    '                                          & " ,''                                                      AS N_ZAI_REC_NO        " & vbNewLine _
    '                                          & ",CASE WHEN C_OUTKA_L.DEST_KB = '00' THEN M_DEST.DEST_NM   " & vbNewLine _
    '                                          & "      ELSE C_OUTKA_L.DEST_NM                              " & vbNewLine _
    '                                          & " END AS DEST_NM                                           " & vbNewLine _
    '                                          & " ,CASE WHEN RTRIM(C_OUTKA_M.CUST_ORD_NO_DTL) <> ''                               " & vbNewLine _
    '                                          & "         THEN C_OUTKA_M.CUST_ORD_NO_DTL                                          " & vbNewLine _
    '                                          & "       WHEN RTRIM(C_OUTKA_L.CUST_ORD_NO) <> ''                                   " & vbNewLine _
    '                                          & "         THEN C_OUTKA_L.CUST_ORD_NO                                              " & vbNewLine _
    '                                          & "  ELSE ''                                             END AS CUST_ORD_NO         " & vbNewLine _
    '                                          & " ,CASE WHEN RTRIM(C_OUTKA_M.BUYER_ORD_NO_DTL) <> ''                              " & vbNewLine _
    '                                          & "         THEN C_OUTKA_M.BUYER_ORD_NO_DTL                                         " & vbNewLine _
    '                                          & "       WHEN RTRIM(C_OUTKA_L.BUYER_ORD_NO) <> ''                                  " & vbNewLine _
    '                                          & "         THEN C_OUTKA_L.BUYER_ORD_NO                                             " & vbNewLine _
    '                                          & "  ELSE ''                                             END AS BUYER_ORD_NO        " & vbNewLine _
    '                                          & " ,M_UNSOCO.UNSOCO_NM                                      AS UNSOCO_NM           " & vbNewLine _
    '                                          & " ,C_OUTKA_S.REMARK                                        AS REMARK              " & vbNewLine _
    '                                          & " ,''                                                      AS REMARK_OUT          " & vbNewLine _
    '                                          & " ,''                                                      AS GOODS_COND_NM_1     " & vbNewLine _
    '                                          & " ,''                                                      AS GOODS_COND_NM_2     " & vbNewLine _
    '                                          & " ,''                                                      AS GOODS_COND_NM_3     " & vbNewLine _
    '                                          & " ,''                                                      AS SPD_KB_NM           " & vbNewLine _
    '                                          & " ,''                                                      AS OFB_KB_NM           " & vbNewLine _
    '                                          & " ,''                                                      AS ALLOC_PRIORITY_NM   " & vbNewLine _
    '                                          & " ,''                                                      AS DEST_NM             " & vbNewLine _
    '                                          & " ,C_OUTKA_M.RSV_NO                                        AS RSV_NO              " & vbNewLine _
    '                                          & " ,'3'                                                     AS SORT_KEY            " & vbNewLine _
    '                                          & " ,M_GOODS.STD_IRIME_NB                                    AS STD_IRIME_NB        " & vbNewLine _
    '                                          & " ,0                                                       AS PORA_ZAI_NB         " & vbNewLine _
    '                                          & " ,0                                                       AS ALLOC_CAN_NB        " & vbNewLine _
    '                                          & " ,''                                                      AS IDO_SYS_UPD_DATE    " & vbNewLine _
    '                                          & " ,''                                                      AS IDO_SYS_UPD_TIME    " & vbNewLine _
    '                                          & " ,''                                                      AS O_ZAI_SYS_UPD_DATE  " & vbNewLine _
    '                                          & " ,''                                                      AS O_ZAI_SYS_UPD_TIME  " & vbNewLine _
    '                                          & " ,''                                                      AS N_ZAI_SYS_UPD_DATE  " & vbNewLine _
    '                                          & " ,''                                                      AS N_ZAI_SYS_UPD_TIME  " & vbNewLine _
    '                                          & " ,@NRS_BR_CD                                              AS NRS_BR_CD           " & vbNewLine _
    '                                          & " ,CUST.HOKAN_SEIQTO_CD                                    AS HOKAN_SEIQTO_CD     " & vbNewLine
    ''' <summary>
    ''' 出荷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_3 As String = "  SELECT                                                                         " & vbNewLine _
                                              & "  CASE WHEN C_OUTKA_L.SYS_DEL_FLG = '1' THEN '消'                                " & vbNewLine _
                                              & "  WHEN C_OUTKA_L.OUTKA_STATE_KB in ('10', '30', '40') THEN '予'                  " & vbNewLine _
                                              & "  ELSE ''                                             END AS STATE_KB            " & vbNewLine _
                                              & " ,CASE WHEN RTRIM(C_OUTKA_L.FURI_NO) <> '' THEN '振替'                           " & vbNewLine _
                                              & "  ELSE '出荷'                                         END AS SYUBETU             " & vbNewLine _
                                              & " ,C_OUTKA_L.OUTKA_PLAN_DATE                               AS IDO_DATE            " & vbNewLine _
                                              & " ,0                                                       AS INKA_NB             " & vbNewLine _
                                              & " ,0                                                       AS INKA_QT             " & vbNewLine _
                                              & " ,C_OUTKA_S.ALCTD_NB                                      AS OUTKA_NB            " & vbNewLine _
                                              & " ,C_OUTKA_S.ALCTD_QT                                      AS OUTKA_QT            " & vbNewLine _
                                              & " ,0                                                       AS BACKLOG_NB          " & vbNewLine _
                                              & " ,0                                                       AS BACKLOG_QT          " & vbNewLine _
                                              & " ,M_GOODS.PKG_UT                                          AS PKG_UT              " & vbNewLine _
                                              & " ,M_GOODS.STD_IRIME_UT                                    AS STD_IRIME_UT        " & vbNewLine _
                                              & " ,C_OUTKA_S.TOU_NO                                        AS TOU_NO              " & vbNewLine _
                                              & " ,C_OUTKA_S.SITU_NO                                       AS SITU_NO             " & vbNewLine _
                                              & " ,C_OUTKA_S.ZONE_CD                                       AS ZONE_CD             " & vbNewLine _
                                              & " ,C_OUTKA_S.LOCA                                          AS LOCA                " & vbNewLine _
                                              & " ,  C_OUTKA_S.OUTKA_NO_L + '_'                                                   " & vbNewLine _
                                              & "  + C_OUTKA_S.OUTKA_NO_M + '_'                                                   " & vbNewLine _
                                              & "  + C_OUTKA_S.OUTKA_NO_S                                  AS INOUTKA_NO_L        " & vbNewLine _
                                              & " ,C_OUTKA_S.ZAI_REC_NO                                    AS O_ZAI_REC_NO        " & vbNewLine _
                                              & " ,''                                                      AS N_ZAI_REC_NO        " & vbNewLine _
                                              & ",CASE WHEN C_OUTKA_L.DEST_KB = '00' THEN M_DEST.DEST_NM   " & vbNewLine _
                                              & "      ELSE C_OUTKA_L.DEST_NM                              " & vbNewLine _
                                              & " END AS DEST_NM                                           " & vbNewLine _
                                              & " ,CASE WHEN RTRIM(C_OUTKA_M.CUST_ORD_NO_DTL) <> ''                               " & vbNewLine _
                                              & "         THEN C_OUTKA_M.CUST_ORD_NO_DTL                                          " & vbNewLine _
                                              & "       WHEN RTRIM(C_OUTKA_L.CUST_ORD_NO) <> ''                                   " & vbNewLine _
                                              & "         THEN C_OUTKA_L.CUST_ORD_NO                                              " & vbNewLine _
                                              & "  ELSE ''                                             END AS CUST_ORD_NO         " & vbNewLine _
                                              & " ,CASE WHEN RTRIM(C_OUTKA_M.BUYER_ORD_NO_DTL) <> ''                              " & vbNewLine _
                                              & "         THEN C_OUTKA_M.BUYER_ORD_NO_DTL                                         " & vbNewLine _
                                              & "       WHEN RTRIM(C_OUTKA_L.BUYER_ORD_NO) <> ''                                  " & vbNewLine _
                                              & "         THEN C_OUTKA_L.BUYER_ORD_NO                                             " & vbNewLine _
                                              & "  ELSE ''                                             END AS BUYER_ORD_NO        " & vbNewLine _
                                              & " ,M_UNSOCO.UNSOCO_NM                                      AS UNSOCO_NM           " & vbNewLine _
                                              & " ,C_OUTKA_S.REMARK                                        AS REMARK              " & vbNewLine _
                                              & " ,''                                                      AS REMARK_OUT          " & vbNewLine _
                                              & " ,''                                                      AS GOODS_COND_NM_1     " & vbNewLine _
                                              & " ,''                                                      AS GOODS_COND_NM_2     " & vbNewLine _
                                              & " ,''                                                      AS GOODS_COND_NM_3     " & vbNewLine _
                                              & " ,''                                                      AS SPD_KB_NM           " & vbNewLine _
                                              & " ,''                                                      AS OFB_KB_NM           " & vbNewLine _
                                              & " ,''                                                      AS ALLOC_PRIORITY_NM   " & vbNewLine _
                                              & " ,''                                                      AS DEST_NM             " & vbNewLine _
                                              & " ,C_OUTKA_M.RSV_NO                                        AS RSV_NO              " & vbNewLine _
                                              & " ,'3'                                                     AS SORT_KEY            " & vbNewLine _
                                              & " ,D_ZAI_TRS.IRIME                                         AS STD_IRIME_NB        " & vbNewLine _
                                              & " ,0                                                       AS PORA_ZAI_NB         " & vbNewLine _
                                              & " ,0                                                       AS ALLOC_CAN_NB        " & vbNewLine _
                                              & " ,''                                                      AS IDO_SYS_UPD_DATE    " & vbNewLine _
                                              & " ,''                                                      AS IDO_SYS_UPD_TIME    " & vbNewLine _
                                              & " ,''                                                      AS O_ZAI_SYS_UPD_DATE  " & vbNewLine _
                                              & " ,''                                                      AS O_ZAI_SYS_UPD_TIME  " & vbNewLine _
                                              & " ,''                                                      AS N_ZAI_SYS_UPD_DATE  " & vbNewLine _
                                              & " ,''                                                      AS N_ZAI_SYS_UPD_TIME  " & vbNewLine _
                                              & " ,@NRS_BR_CD                                              AS NRS_BR_CD           " & vbNewLine _
                                              & " ,CUST.HOKAN_SEIQTO_CD                                    AS HOKAN_SEIQTO_CD     " & vbNewLine
    'END YANAI 要望番号1044

    ''' <summary>
    ''' 検索用SQLFROM句(在庫移動トランザクション）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_IN As String = "FROM                                                   " & vbNewLine _
                                  & " $LM_TRN$..D_IDO_TRS  D_IDO_TRS                              " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_TRN$..D_ZAI_TRS  D_ZAI_TRS                              " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " D_ZAI_TRS.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                  & " AND                                                         " & vbNewLine _
                                  & "    D_IDO_TRS.N_ZAI_REC_NO = D_ZAI_TRS.ZAI_REC_NO            " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.CUST_CD_L = @CUST_CD_L                            " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.CUST_CD_M = @CUST_CD_M                            " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.GOODS_CD_NRS  = @GOODS_CD_NRS                     " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_TRN$..D_ZAI_TRS  D_ZAI_TRS_O                            " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " D_ZAI_TRS_O.NRS_BR_CD = @NRS_BR_CD                          " & vbNewLine _
                                  & " AND                                                         " & vbNewLine _
                                  & " D_IDO_TRS.O_ZAI_REC_NO = D_ZAI_TRS_O.ZAI_REC_NO             " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS_O.CUST_CD_L = @CUST_CD_L                          " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS_O.CUST_CD_M = @CUST_CD_M                          " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS_O.GOODS_CD_NRS  = @GOODS_CD_NRS                   " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..M_CUSTCOND  M_CUSTCOND                            " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " M_CUSTCOND.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.CUST_CD_L  = @CUST_CD_L                           " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.GOODS_COND_KB_3 = M_CUSTCOND.JOTAI_CD             " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..M_DEST  M_DEST                                    " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " M_DEST.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " M_DEST.CUST_CD_L = @CUST_CD_L                               " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.DEST_CD_P = M_DEST.DEST_CD                        " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..M_GOODS  M_GOODS                                  " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " M_GOODS.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " M_GOODS.GOODS_CD_NRS  = @GOODS_CD_NRS                       " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..M_CUST   CUST                                     " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " CUST.NRS_BR_CD     = @NRS_BR_CD                             " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " M_GOODS.CUST_CD_L  = CUST.CUST_CD_L                         " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " M_GOODS.CUST_CD_M  = CUST.CUST_CD_M                         " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " M_GOODS.CUST_CD_S  = CUST.CUST_CD_S                         " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " M_GOODS.CUST_CD_SS = CUST.CUST_CD_SS                        " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " CUST.SYS_DEL_FLG   = '0'                                    " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..Z_KBN Z1                                          " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " Z1.KBN_GROUP_CD = 'I002'                                    " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_IDO_TRS.REMARK_KBN = Z1.KBN_CD                            " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..Z_KBN Z2                                          " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " Z2.KBN_GROUP_CD = 'S005'                                    " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.GOODS_COND_KB_1 = Z2.KBN_CD                       " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..Z_KBN Z3                                          " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " Z3.KBN_GROUP_CD = 'S006'                                    " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.GOODS_COND_KB_2=Z3.KBN_CD                         " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..Z_KBN Z4                                          " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " Z4.KBN_GROUP_CD =  'H003'                                   " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.SPD_KB=Z4.KBN_CD                                  " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..Z_KBN Z5                                          " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " Z5.KBN_GROUP_CD  = 'B002'                                   " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.OFB_KB = Z5.KBN_CD                                " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..Z_KBN Z6                                          " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " Z6.KBN_GROUP_CD = 'W001'                                    " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.ALLOC_PRIORITY = Z6.KBN_CD                        " & vbNewLine _
                                  & "WHERE                                                        " & vbNewLine _
                                  & " D_IDO_TRS.NRS_BR_CD  = @NRS_BR_CD                           " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " (                                                           " & vbNewLine _
                                  & "    D_IDO_TRS.N_ZAI_REC_NO = @ZAI_REC_NO                     " & vbNewLine _
                                  & " )                                                           " & vbNewLine _
                                  & "AND D_IDO_TRS.SYS_DEL_FLG = '0'                              " & vbNewLine

    ''' <summary>
    ''' 検索用SQLFROM句(在庫移動トランザクション）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_OUT As String = "FROM                                                  " & vbNewLine _
                                  & " $LM_TRN$..D_IDO_TRS  D_IDO_TRS                              " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_TRN$..D_ZAI_TRS  D_ZAI_TRS                              " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " D_ZAI_TRS.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                  & " AND                                                         " & vbNewLine _
                                  & " (                                                           " & vbNewLine _
                                  & "    D_IDO_TRS.O_ZAI_REC_NO = D_ZAI_TRS.ZAI_REC_NO            " & vbNewLine _
                                  & " )                                                           " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.CUST_CD_L = @CUST_CD_L                            " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.CUST_CD_M = @CUST_CD_M                            " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.GOODS_CD_NRS  = @GOODS_CD_NRS                     " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..M_CUSTCOND  M_CUSTCOND                            " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " M_CUSTCOND.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.CUST_CD_L  = @CUST_CD_L                           " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.GOODS_COND_KB_3 = M_CUSTCOND.JOTAI_CD             " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..M_DEST  M_DEST                                    " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " M_DEST.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " M_DEST.CUST_CD_L = @CUST_CD_L                               " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.DEST_CD_P = M_DEST.DEST_CD                        " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..M_GOODS  M_GOODS                                  " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " M_GOODS.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " M_GOODS.GOODS_CD_NRS  = @GOODS_CD_NRS                       " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..M_CUST   CUST                                     " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " CUST.NRS_BR_CD     = @NRS_BR_CD                             " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " M_GOODS.CUST_CD_L  = CUST.CUST_CD_L                         " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " M_GOODS.CUST_CD_M  = CUST.CUST_CD_M                         " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " M_GOODS.CUST_CD_S  = CUST.CUST_CD_S                         " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " M_GOODS.CUST_CD_SS = CUST.CUST_CD_SS                        " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " CUST.SYS_DEL_FLG   = '0'                                    " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..Z_KBN Z1                                          " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " Z1.KBN_GROUP_CD = 'I002'                                    " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_IDO_TRS.REMARK_KBN = Z1.KBN_CD                            " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..Z_KBN Z2                                          " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " Z2.KBN_GROUP_CD = 'S005'                                    " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.GOODS_COND_KB_1 = Z2.KBN_CD                       " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..Z_KBN Z3                                          " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " Z3.KBN_GROUP_CD = 'S006'                                    " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.GOODS_COND_KB_2=Z3.KBN_CD                         " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..Z_KBN Z4                                          " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " Z4.KBN_GROUP_CD =  'H003'                                   " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.SPD_KB=Z4.KBN_CD                                  " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..Z_KBN Z5                                          " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " Z5.KBN_GROUP_CD  = 'B002'                                   " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.OFB_KB = Z5.KBN_CD                                " & vbNewLine _
                                  & "LEFT JOIN                                                    " & vbNewLine _
                                  & " $LM_MST$..Z_KBN Z6                                          " & vbNewLine _
                                  & "ON                                                           " & vbNewLine _
                                  & " Z6.KBN_GROUP_CD = 'W001'                                    " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " D_ZAI_TRS.ALLOC_PRIORITY = Z6.KBN_CD                        " & vbNewLine _
                                  & "WHERE                                                        " & vbNewLine _
                                  & " D_IDO_TRS.NRS_BR_CD  = @NRS_BR_CD                           " & vbNewLine _
                                  & "AND                                                          " & vbNewLine _
                                  & " (                                                           " & vbNewLine _
                                  & "    D_IDO_TRS.O_ZAI_REC_NO = @ZAI_REC_NO                     " & vbNewLine _
                                  & " )                                                           " & vbNewLine _
                                  & "AND D_IDO_TRS.SYS_DEL_FLG = '0'                              " & vbNewLine


 


    ''' <summary>
    ''' 検索用SQLFROM句(入荷データ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_2 As String = "FROM                                                       " & vbNewLine _
                                    & " $LM_TRN$..B_INKA_S B_INKA_S                                  " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_TRN$..D_ZAI_TRS      D_ZAI_TRS                           " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " D_ZAI_TRS.NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.INKA_NO_L = D_ZAI_TRS.INKA_NO_L                     " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.INKA_NO_M = D_ZAI_TRS.INKA_NO_M                     " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.INKA_NO_S = D_ZAI_TRS.INKA_NO_S                     " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " D_ZAI_TRS.CUST_CD_L = @CUST_CD_L                             " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " D_ZAI_TRS.CUST_CD_M = @CUST_CD_M                             " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " D_ZAI_TRS.GOODS_CD_NRS  = @GOODS_CD_NRS                      " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_TRN$..B_INKA_M B_INKA_M                                  " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " B_INKA_M.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.INKA_NO_L = B_INKA_M.INKA_NO_L                      " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.INKA_NO_M = B_INKA_M.INKA_NO_M                      " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_TRN$..B_INKA_L B_INKA_L                                  " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " B_INKA_L.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.INKA_NO_L = B_INKA_L.INKA_NO_L                      " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_L.CUST_CD_L = @CUST_CD_L                              " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_L.CUST_CD_M  = @CUST_CD_M                             " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_MST$..M_CUSTCOND M_CUSTCOND                              " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " M_CUSTCOND.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " M_CUSTCOND.CUST_CD_L = @CUST_CD_L                            " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.GOODS_COND_KB_3 = M_CUSTCOND.JOTAI_CD               " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_MST$..M_DEST M_DEST                                      " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " M_DEST.NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " M_DEST.CUST_CD_L = @CUST_CD_L                                " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.DEST_CD = M_DEST.DEST_CD                            " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_MST$..M_GOODS M_GOODS                                    " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " M_GOODS.NRS_BR_CD    = @NRS_BR_CD                            " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " M_GOODS.GOODS_CD_NRS = @GOODS_CD_NRS                         " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_MST$..M_CUST   CUST                                      " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " CUST.NRS_BR_CD     = @NRS_BR_CD                              " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " M_GOODS.CUST_CD_L  = CUST.CUST_CD_L                          " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " M_GOODS.CUST_CD_M  = CUST.CUST_CD_M                          " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " M_GOODS.CUST_CD_S  = CUST.CUST_CD_S                          " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " M_GOODS.CUST_CD_SS = CUST.CUST_CD_SS                         " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " CUST.SYS_DEL_FLG   = '0'                                     " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_L                                           " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " F_UNSO_L.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.INKA_NO_L = F_UNSO_L.INOUTKA_NO_L                   " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_MST$..M_UNSOCO                                           " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " M_UNSOCO.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " F_UNSO_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                        " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " F_UNSO_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                  " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_MST$..Z_KBN Z2                                           " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " Z2.KBN_GROUP_CD = 'S005'                                     " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.GOODS_COND_KB_1=Z2.KBN_CD                           " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_MST$..Z_KBN Z3                                           " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " Z3.KBN_GROUP_CD = 'S006'                                     " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.GOODS_COND_KB_2 = Z3.KBN_CD                         " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_MST$..Z_KBN Z4                                           " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " Z4.KBN_GROUP_CD = 'H003'                                     " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.SPD_KB = Z4.KBN_CD                                  " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_MST$..Z_KBN Z5                                           " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " Z5.KBN_GROUP_CD = 'B002'                                     " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.OFB_KB = Z5.KBN_CD                                  " & vbNewLine _
                                    & "LEFT JOIN                                                     " & vbNewLine _
                                    & " $LM_MST$..Z_KBN Z6                                           " & vbNewLine _
                                    & "ON                                                            " & vbNewLine _
                                    & " Z6.KBN_GROUP_CD = 'W001'                                     " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.ALLOC_PRIORITY = Z6.KBN_CD                          " & vbNewLine _
                                    & "WHERE                                                         " & vbNewLine _
                                    & " B_INKA_S.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " (                                                            " & vbNewLine _
                                    & "    B_INKA_S.ZAI_REC_NO = @ZAI_REC_NO                      " & vbNewLine _
                                    & " )                                                            " & vbNewLine _
                                    & "AND                                                           " & vbNewLine _
                                    & " B_INKA_S.SYS_DEL_FLG = '0'                                   " & vbNewLine


    ''' <summary>
    ''' 検索用SQLFROM句(出荷データ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_3 As String = "FROM                                                              " & vbNewLine _
                                       & " $LM_TRN$..C_OUTKA_S C_OUTKA_S                                    " & vbNewLine _
                                       & "LEFT JOIN                                                         " & vbNewLine _
                                       & " $LM_TRN$..D_ZAI_TRS      D_ZAI_TRS                               " & vbNewLine _
                                       & "ON                                                                " & vbNewLine _
                                       & " D_ZAI_TRS.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " D_ZAI_TRS.GOODS_CD_NRS  = @GOODS_CD_NRS                          " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " D_ZAI_TRS.CUST_CD_L = @CUST_CD_L                                 " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " D_ZAI_TRS.CUST_CD_M = @CUST_CD_M                                 " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & "C_OUTKA_S.ZAI_REC_NO = D_ZAI_TRS.ZAI_REC_NO                       " & vbNewLine _
                                       & "LEFT JOIN                                                         " & vbNewLine _
                                       & " $LM_TRN$..C_OUTKA_M C_OUTKA_M                                    " & vbNewLine _
                                       & "ON                                                                " & vbNewLine _
                                       & " C_OUTKA_M.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " C_OUTKA_S.OUTKA_NO_L = C_OUTKA_M.OUTKA_NO_L                      " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " C_OUTKA_S.OUTKA_NO_M = C_OUTKA_M.OUTKA_NO_M                      " & vbNewLine _
                                       & "LEFT JOIN                                                         " & vbNewLine _
                                       & " $LM_TRN$..C_OUTKA_L C_OUTKA_L                                    " & vbNewLine _
                                       & "ON                                                                " & vbNewLine _
                                       & " C_OUTKA_L.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " C_OUTKA_S.OUTKA_NO_L = C_OUTKA_L.OUTKA_NO_L                      " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " C_OUTKA_L.CUST_CD_L = @CUST_CD_L                                 " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " C_OUTKA_L.CUST_CD_M  = @CUST_CD_M                                " & vbNewLine _
                                       & "LEFT JOIN                                                         " & vbNewLine _
                                       & " $LM_MST$..M_DEST M_DEST                                          " & vbNewLine _
                                       & "ON                                                                " & vbNewLine _
                                       & " M_DEST.NRS_BR_CD= @NRS_BR_CD                                     " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " M_DEST.CUST_CD_L = @CUST_CD_L                                    " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " C_OUTKA_L.DEST_CD= M_DEST.DEST_CD                                " & vbNewLine _
                                       & "LEFT JOIN                                                         " & vbNewLine _
                                       & " $LM_MST$..M_GOODS M_GOODS                                        " & vbNewLine _
                                       & "ON                                                                " & vbNewLine _
                                       & " M_GOODS.NRS_BR_CD= @NRS_BR_CD                                    " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " M_GOODS.GOODS_CD_NRS    = @GOODS_CD_NRS                          " & vbNewLine _
                                       & "LEFT JOIN                                                         " & vbNewLine _
                                       & " $LM_MST$..M_CUST   CUST                                          " & vbNewLine _
                                       & "ON                                                                " & vbNewLine _
                                       & " CUST.NRS_BR_CD     = @NRS_BR_CD                                  " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " M_GOODS.CUST_CD_L  = CUST.CUST_CD_L                              " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " M_GOODS.CUST_CD_M  = CUST.CUST_CD_M                              " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " M_GOODS.CUST_CD_S  = CUST.CUST_CD_S                              " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " M_GOODS.CUST_CD_SS = CUST.CUST_CD_SS                             " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " CUST.SYS_DEL_FLG   = '0'                                         " & vbNewLine _
                                       & "LEFT JOIN                                                         " & vbNewLine _
                                       & " $LM_TRN$..F_UNSO_L                                               " & vbNewLine _
                                       & "ON                                                                " & vbNewLine _
                                       & " F_UNSO_L.NRS_BR_CD = @NRS_BR_CD                                  " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " C_OUTKA_S.OUTKA_NO_L =F_UNSO_L.INOUTKA_NO_L                      " & vbNewLine _
                                       & "LEFT JOIN                                                         " & vbNewLine _
                                       & " $LM_MST$..M_UNSOCO M_UNSOCO                                      " & vbNewLine _
                                       & "ON                                                                " & vbNewLine _
                                       & " M_UNSOCO.NRS_BR_CD= @NRS_BR_CD                                   " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " F_UNSO_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                            " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " F_UNSO_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                      " & vbNewLine _
                                       & "WHERE                                                             " & vbNewLine _
                                       & " C_OUTKA_S.NRS_BR_CD  = @NRS_BR_CD                                " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " (                                                                " & vbNewLine _
                                       & "    C_OUTKA_S.ZAI_REC_NO = @ZAI_REC_NO                          " & vbNewLine _
                                       & " )                                                                " & vbNewLine _
                                       & "AND                                                               " & vbNewLine _
                                       & " C_OUTKA_S.SYS_DEL_FLG = '0'                                      " & vbNewLine


    ''' <summary>
    ''' ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY SORT_KEY , IDO_DATE, SYUBETU DESC, INOUTKA_NO_L "

#End Region

#Region "削除処理 SQL"

    ''' <summary>
    ''' 削除用SQL(在庫トランザクション)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_ZAIKO_TRN As String = "UPDATE  $LM_TRN$..D_IDO_TRS  SET         " & vbNewLine _
                                                 & "      SYS_UPD_DATE = @SYS_UPD_DATE       " & vbNewLine _
                                                 & "    , SYS_UPD_TIME = @SYS_UPD_TIME       " & vbNewLine _
                                                 & "    , SYS_UPD_PGID = @SYS_UPD_PGID       " & vbNewLine _
                                                 & "    , SYS_UPD_USER = @SYS_UPD_USER       " & vbNewLine _
                                                 & "    , SYS_DEL_FLG  = '1'                 " & vbNewLine _
                                                 & "WHERE NRS_BR_CD    = @NRS_BR_CD          " & vbNewLine _
                                                 & "AND   O_ZAI_REC_NO = @O_ZAI_REC_NO       " & vbNewLine _
                                                 & "AND   N_ZAI_REC_NO = @N_ZAI_REC_NO       " & vbNewLine _
                                                 & "AND   SYS_DEL_FLG  = '0'                 " & vbNewLine _
                                                 & "AND   SYS_UPD_DATE = @HAITA_DATE         " & vbNewLine _
                                                 & "AND   SYS_UPD_TIME = @HAITA_TIME         " & vbNewLine

    ''' <summary>
    ''' 削除用SQL(在庫データ（元))
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_ZAIKO_DATA_OLD As String = "UPDATE  $LM_TRN$..D_ZAI_TRS  SET                       " & vbNewLine _
                                                      & "     PORA_ZAI_NB   = PORA_ZAI_NB  + @PORA_ZAI_NB       " & vbNewLine _
                                                      & "    ,ALLOC_CAN_NB  = ALLOC_CAN_NB + @ALLOC_CAN_NB      " & vbNewLine _
                                                      & "    ,PORA_ZAI_QT   = PORA_ZAI_QT  + @PORA_ZAI_QT       " & vbNewLine _
                                                      & "    ,ALLOC_CAN_QT  = ALLOC_CAN_QT + @ALLOC_CAN_QT      " & vbNewLine _
                                                      & "    ,SYS_UPD_DATE  = @SYS_UPD_DATE                     " & vbNewLine _
                                                      & "    ,SYS_UPD_TIME  = @SYS_UPD_TIME                     " & vbNewLine _
                                                      & "    ,SYS_UPD_PGID  = @SYS_UPD_PGID                     " & vbNewLine _
                                                      & "    ,SYS_UPD_USER  = @SYS_UPD_USER                     " & vbNewLine _
                                                      & "    ,SYS_DEL_FLG   = '0'                               " & vbNewLine _
                                                      & "WHERE NRS_BR_CD    = @NRS_BR_CD                        " & vbNewLine _
                                                      & "AND   ZAI_REC_NO   = @ZAI_REC_NO                       " & vbNewLine _
                                                      & "AND   SYS_UPD_DATE = @HAITA_DATE                       " & vbNewLine _
                                                      & "AND   SYS_UPD_TIME = @HAITA_TIME                       " & vbNewLine

    ''' <summary>
    ''' 削除用SQL(在庫データ（先))
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_ZAIKO_DATA_NEW As String = "UPDATE  $LM_TRN$..D_ZAI_TRS  SET        " & vbNewLine _
                                                      & "     PORA_ZAI_NB   = @PORA_ZAI_NB       " & vbNewLine _
                                                      & "    ,ALLOC_CAN_NB  = @ALLOC_CAN_NB      " & vbNewLine _
                                                      & "    ,PORA_ZAI_QT   = @PORA_ZAI_QT       " & vbNewLine _
                                                      & "    ,ALLOC_CAN_QT  = @ALLOC_CAN_QT      " & vbNewLine _
                                                      & "    ,SYS_UPD_DATE  = @SYS_UPD_DATE      " & vbNewLine _
                                                      & "    ,SYS_UPD_TIME  = @SYS_UPD_TIME      " & vbNewLine _
                                                      & "    ,SYS_UPD_PGID  = @SYS_UPD_PGID      " & vbNewLine _
                                                      & "    ,SYS_UPD_USER  = @SYS_UPD_USER      " & vbNewLine _
                                                      & "    ,SYS_DEL_FLG   = $SYS_DEL_FLG$      " & vbNewLine _
                                                      & "WHERE NRS_BR_CD    = @NRS_BR_CD         " & vbNewLine _
                                                      & "AND   ZAI_REC_NO   = @ZAI_REC_NO        " & vbNewLine _
                                                      & "AND   SYS_UPD_DATE = @HAITA_DATE        " & vbNewLine _
                                                      & "AND   SYS_UPD_TIME = @HAITA_TIME        " & vbNewLine _
                                                      & "AND   SYS_DEL_FLG   = '0'               " & vbNewLine





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
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD030DAC.SQL_SELECT_DATA_IN)      'SQL構築(データ抽出用Select句移入用)
        Me._StrSql.Append(LMD030DAC.SQL_FROM_IN)             'SQL構築(データ抽出用From・Where句移入用)
        Me._StrSql.Append(" UNION ")
        Me._StrSql.Append(LMD030DAC.SQL_SELECT_DATA_OUT)     'SQL構築(データ抽出用Select句移出用)
        Me._StrSql.Append(LMD030DAC.SQL_FROM_OUT)            'SQL構築(データ抽出用From・Where句移出用)
        Me._StrSql.Append(" UNION ")
        Me._StrSql.Append(LMD030DAC.SQL_SELECT_DATA_2)       'SQL構築(データ抽出用Select句2)
        Me._StrSql.Append(LMD030DAC.SQL_FROM_2)              'SQL構築(データ抽出用From・Where句2)
        Me._StrSql.Append(" UNION ")
        Me._StrSql.Append(LMD030DAC.SQL_SELECT_DATA_3)       'SQL構築(データ抽出用Select句3)
        Me._StrSql.Append(LMD030DAC.SQL_FROM_3)              'SQL構築(データ抽出用From・Where句3)
        If Me._Row("DEL_VIEW_FLG").ToString().Equals("00") Then
            Me._StrSql.Append(" AND C_OUTKA_L.SYS_DEL_FLG = '0' ")
        End If
        Me._StrSql.Append(LMD030DAC.SQL_ORDER_BY)            'SQL構築(データ抽出用OrderBy句)

        '条件設定
        Call Me.SetConditionMasterSQL()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD030DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("STATE_KB", "STATE_KB")
        map.Add("SYUBETU", "SYUBETU")
        map.Add("IDO_DATE", "IDO_DATE")
        map.Add("INKA_NB", "INKA_NB")
        map.Add("INKA_QT", "INKA_QT")
        map.Add("OUTKA_NB", "OUTKA_NB")
        map.Add("OUTKA_QT", "OUTKA_QT")
        map.Add("BACKLOG_NB", "BACKLOG_NB")
        map.Add("BACKLOG_QT", "BACKLOG_QT")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("O_ZAI_REC_NO", "O_ZAI_REC_NO")
        map.Add("N_ZAI_REC_NO", "N_ZAI_REC_NO")
        map.Add("REMARK_KBN", "REMARK_KBN")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
        map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
        map.Add("GOODS_COND_NM_3", "GOODS_COND_NM_3")
        map.Add("SPD_KB_NM", "SPD_KB_NM")
        map.Add("OFB_KB_NM", "OFB_KB_NM")
        map.Add("ALLOC_PRIORITY_NM", "ALLOC_PRIORITY_NM")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("SORT_KEY", "SORT_KEY")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("ALLOC_CAN_NB", "ALLOC_CAN_NB")
        map.Add("IDO_SYS_UPD_DATE", "IDO_SYS_UPD_DATE")
        map.Add("IDO_SYS_UPD_TIME", "IDO_SYS_UPD_TIME")
        map.Add("O_ZAI_SYS_UPD_DATE", "O_ZAI_SYS_UPD_DATE")
        map.Add("O_ZAI_SYS_UPD_TIME", "O_ZAI_SYS_UPD_TIME")
        map.Add("N_ZAI_SYS_UPD_DATE", "N_ZAI_SYS_UPD_DATE")
        map.Add("N_ZAI_SYS_UPD_TIME", "N_ZAI_SYS_UPD_TIME")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("HOKAN_SEIQTO_CD", "HOKAN_SEIQTO_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD030OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD030OUT").Rows.Count())

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
        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS"), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 請求ヘッダ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求ヘッダ取得SQLの構築・発行</remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD030IN_ZAI_DEL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("HOKAN_SEIQTO_CD").ToString(), DBDataType.NVARCHAR))	'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMG000DAC.SQL_SELECT_HOKAN_CHK_DATE, brCd))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD030DAC", "SelectGheaderData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_DATE", "SKYU_DATE")

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, "G_HED")

    End Function

#End Region '検索処理

#Region "削除処理"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteDataControl(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD030IN_ZAI_DEL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '削除処理（在庫トランザクション）
        ds = Me.DeleteZaikoTrn(ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        '削除処理（在庫データ（元））
        ds = Me.DeleteZaikoDataOld(ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        '削除処理(在庫データ(先))
        ds = Me.DeleteZaikoDataNew(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理(在庫トランザクション）
    ''' </summary>
    ''' <param name="ds"></param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteZaikoTrn(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(LMD030DAC.SQL_DELETE_ZAIKO_TRN)  '更新用SQL

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ設定
        Call Me.setParaUpdateDeleteZaikoTrn()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD030DAC", "DeleteZaikoTrn", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理(在庫データ（元））
    ''' </summary>
    ''' <param name="ds"></param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteZaikoDataOld(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(LMD030DAC.SQL_DELETE_ZAIKO_DATA_OLD)  '更新用SQL

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ設定
        Call Me.setParaUpdateDeleteZaikoDataOld()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD030DAC", "DeleteZaikoDataOld", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理(在庫データ（先））
    ''' </summary>
    ''' <param name="ds"></param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteZaikoDataNew(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(LMD030DAC.SQL_DELETE_ZAIKO_DATA_NEW)  '更新用SQL
        If Convert.ToDouble(Me._Row.Item("N_PORA_ZAI_NB")) <> 0 Then
            Me._StrSql.Replace("$SYS_DEL_FLG$", "'0'")
        Else
            '残数がなくなったデータなら削除する
            Me._StrSql.Replace("$SYS_DEL_FLG$", "'1'")
        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ設定
        Call Me.setParaUpdateDeleteZaikoDataNew()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD030DAC", "DeleteZaikoDataNew", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(在庫トランザクション)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setParaUpdateDeleteZaikoTrn()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@O_ZAI_REC_NO", .Item("O_ZAI_REC_NO"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@N_ZAI_REC_NO", .Item("N_ZAI_REC_NO"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("IDO_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("IDO_SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

        '更新時共通項目
        Call Me.SetParamCommonSystemUp()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(在庫データ（元））
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setParaUpdateDeleteZaikoDataOld()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("O_ZAI_REC_NO"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", .Item("O_PORA_ZAI_NB"), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", .Item("O_ALLOC_CAN_NB"), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", .Item("O_PORA_ZAI_QT"), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", .Item("O_ALLOC_CAN_QT"), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("O_ZAI_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("O_ZAI_SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

        '更新時共通項目
        Call Me.SetParamCommonSystemUp()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(在庫データ（先））
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setParaUpdateDeleteZaikoDataNew()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("N_ZAI_REC_NO"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", .Item("N_PORA_ZAI_NB"), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", .Item("N_ALLOC_CAN_NB"), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", .Item("N_PORA_ZAI_QT"), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", .Item("N_ALLOC_CAN_QT"), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("N_ZAI_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("N_ZAI_SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

        '更新時共通項目
        Call Me.SetParamCommonSystemUp()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUp()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

#End Region

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
    ''' SQLパラメータ設定モジュール（排他チェック）
    ''' </summary>
    ''' <param name="haitaSyubetu"></param>
    ''' <remarks></remarks>
    Private Sub setParaHaitaSQL(ByVal haitaSyubetu As String)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '排他対象による分岐
        Select Case haitaSyubetu

            Case "IDO"
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@O_ZAI_REC_NO", Me._Row("O_ZAI_REC_NO").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@N_ZAI_REC_NO", Me._Row("N_ZAI_REC_NO").ToString(), DBDataType.CHAR))

            Case "O_ZAI", "N_ZAI"
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me._Row(haitaSyubetu + "_REC_NO").ToString(), DBDataType.CHAR))
        End Select

        'パラメータ設定(共通）
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row(haitaSyubetu & "_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row(haitaSyubetu & "_SYS_UPD_TIME").ToString(), DBDataType.CHAR))



    End Sub

#End Region 'SQL

#End Region 'Method

End Class
