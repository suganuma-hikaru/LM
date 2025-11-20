' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD020    : 在庫履歴
'  作  成  者       :  [高道]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD020DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD020DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' 事由欄：自動倉庫移動
    ''' </summary>
    ''' <remarks>要望管理009859</remarks>
    Private Const JIYURAN_AUTO_IDO As String = "07"

#Region "検索処理 SQL"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(ZAITRS.ZAI_REC_NO)	AS SELECT_CNT       " & vbNewLine

    'START YANAI 要望番号766
    '''' <summary>
    '''' 在庫トランザクションデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT As String = " SELECT                                                                     " & vbNewLine _
    '                                          & " GOODS.GOODS_NM_1 AS GOODS_NM,                                            " & vbNewLine _
    '                                          & " ZAITRS.LOT_NO AS LOT_NO,                                                 " & vbNewLine _
    '                                          & " (CASE WHEN ZAITRS.IRIME > ZAITRS.PORA_ZAI_QT THEN ZAITRS.PORA_ZAI_QT     " & vbNewLine _
    '                                          & " ELSE ZAITRS.IRIME END) AS IRIME,                                         " & vbNewLine _
    '                                          & " GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                      " & vbNewLine _
    '                                          & " GOODS.PKG_UT AS PKG_UT,                                                  " & vbNewLine _
    '                                          & " ZAITRS.INKO_DATE AS INKO_DATE,                                           " & vbNewLine _
    '                                          & " ZAITRS.SERIAL_NO AS SERIAL_NO,                                           " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_NB AS ALLOC_CAN_NB,                                     " & vbNewLine _
    '                                          & " ZAITRS.ALCTD_NB AS ALCTD_NB,                                             " & vbNewLine _
    '                                          & " ZAITRS.PORA_ZAI_NB AS PORA_ZAI_NB,                                       " & vbNewLine _
    '                                          & " ZAITRS.TOU_NO AS TOU_NO,                                                 " & vbNewLine _
    '                                          & " ZAITRS.SITU_NO AS SITU_NO,                                               " & vbNewLine _
    '                                          & " ZAITRS.ZONE_CD AS ZONE_CD,                                               " & vbNewLine _
    '                                          & " ZAITRS.LOCA AS LOCA,                                                     " & vbNewLine _
    '                                          & " GOODS.GOODS_CD_CUST AS GOODS_CD_CUST,                                    " & vbNewLine _
    '                                          & " KBN1.KBN_NM1 AS GOODS_COND_NM_1,                                         " & vbNewLine _
    '                                          & " KBN2.KBN_NM1 AS GOODS_COND_NM_2,                                         " & vbNewLine _
    '                                          & " CUSTCOND.JOTAI_NM AS GOODS_COND_NM_3,                                    " & vbNewLine _
    '                                          & " KBN3.KBN_NM1 AS SPD_KB_NM,                                               " & vbNewLine _
    '                                          & " KBN4.KBN_NM1 AS OFB_KB_NM,                                               " & vbNewLine _
    '                                          & " ZAITRS.LT_DATE AS LT_DATE,                                               " & vbNewLine _
    '                                          & " ZAITRS.GOODS_CRT_DATE AS GOODS_CRT_DATE,                                 " & vbNewLine _
    '                                          & " GOODS.SEARCH_KEY_2 AS SEARCH_KEY_2,                                      " & vbNewLine _
    '                                          & " KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                       " & vbNewLine _
    '                                          & " DEST.DEST_NM AS DEST_NM,                                                 " & vbNewLine _
    '                                          & " ZAITRS.RSV_NO AS RSV_NO,                                                 " & vbNewLine _
    '                                          & " ZAITRS.REMARK_OUT AS REMARK_OUT,                                         " & vbNewLine _
    '                                          & " ZAITRS.ZAI_REC_NO AS ZAI_REC_NO,                                         " & vbNewLine _
    '                                          & " CUST.CUST_NM_L AS CUST_NM_L,                                             " & vbNewLine _
    '                                          & " CUST.CUST_NM_M AS CUST_NM_M,                                             " & vbNewLine _
    '                                          & " ZAITRS.REMARK AS REMARK,                                                 " & vbNewLine _
    '                                          & " ZAITRS.ALCTD_QT AS ALCTD_QT,                                             " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_QT AS ALLOC_CAN_QT,                                     " & vbNewLine _
    '                                          & " ZAITRS.PORA_ZAI_QT AS PORA_ZAI_QT,                                       " & vbNewLine _
    '                                          & " GOODS.PKG_NB AS PKG_NB,                                                  " & vbNewLine _
    '                                          & " CUST.HOKAN_NIYAKU_CALCULATION AS HOKAN_NIYAKU_CALCULATION,               " & vbNewLine _
    '                                          & " OUTKA.OUTKO_DATE AS OUTKO_DATE,                                          " & vbNewLine _
    '                                          & " ZAITRS.WH_CD AS WH_CD,                                                   " & vbNewLine _
    '                                          & " ZAITRS.NRS_BR_CD AS NRS_BR_CD,                                           " & vbNewLine _
    '                                          & " ZAITRS.SYS_UPD_DATE AS SYS_UPD_DATE,                                     " & vbNewLine _
    '                                          & " ZAITRS.SYS_UPD_TIME AS SYS_UPD_TIME,                                     " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_NB AS IDO_KOSU,                                         " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_PRIORITY AS ALLOC_PRIORITY,                                 " & vbNewLine _
    '                                          & " ZAITRS.GOODS_COND_KB_1 AS GOODS_COND_KB_1,                               " & vbNewLine _
    '                                          & " ZAITRS.GOODS_COND_KB_2 AS GOODS_COND_KB_2,                               " & vbNewLine _
    '                                          & " ZAITRS.GOODS_COND_KB_3 AS GOODS_COND_KB_3,                               " & vbNewLine _
    '                                          & " ZAITRS.SPD_KB AS SPD_KB,                                                 " & vbNewLine _
    '                                          & " ZAITRS.OFB_KB AS OFB_KB,                                                 " & vbNewLine _
    '                                          & " ZAITRS.DEST_CD_P AS DEST_CD,                                             " & vbNewLine _
    '                                          & " GOODS.GOODS_CD_NRS AS GOODS_CD_NRS,                                      " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_L AS CUST_CD_L,                                           " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_M AS CUST_CD_M,                                           " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_L AS INKA_NO_L,                                           " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_M AS INKA_NO_M,                                           " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_S AS INKA_NO_S,                                           " & vbNewLine _
    '                                          & " ZAITRS.HOKAN_YN AS HOKAN_YN,                                             " & vbNewLine _
    '                                          & " ZAITRS.TAX_KB AS TAX_KB,                                                 " & vbNewLine _
    '                                          & " ZAITRS.ZERO_FLAG AS ZERO_FLAG,                                           " & vbNewLine _
    '                                          & " ZAITRS.SMPL_FLAG AS SMPL_FLAG                                            " & vbNewLine
    'START YANAI 要望番号1327 移動した際に、入目は変更しない
    '''' <summary>
    '''' 在庫トランザクションデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT As String = " SELECT                                                                     " & vbNewLine _
    '                                          & " GOODS.GOODS_NM_1 AS GOODS_NM,                                            " & vbNewLine _
    '                                          & " ZAITRS.LOT_NO AS LOT_NO,                                                 " & vbNewLine _
    '                                          & " (CASE WHEN ZAITRS.IRIME > ZAITRS.PORA_ZAI_QT THEN ZAITRS.PORA_ZAI_QT     " & vbNewLine _
    '                                          & " ELSE ZAITRS.IRIME END) AS IRIME,                                         " & vbNewLine _
    '                                          & " GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                      " & vbNewLine _
    '                                          & " GOODS.PKG_UT AS PKG_UT,                                                  " & vbNewLine _
    '                                          & " ZAITRS.INKO_DATE AS INKO_DATE,                                           " & vbNewLine _
    '                                          & " ZAITRS.SERIAL_NO AS SERIAL_NO,                                           " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_NB AS ALLOC_CAN_NB,                                     " & vbNewLine _
    '                                          & " ZAITRS.ALCTD_NB AS ALCTD_NB,                                             " & vbNewLine _
    '                                          & " ZAITRS.PORA_ZAI_NB AS PORA_ZAI_NB,                                       " & vbNewLine _
    '                                          & " ZAITRS.TOU_NO AS TOU_NO,                                                 " & vbNewLine _
    '                                          & " ZAITRS.SITU_NO AS SITU_NO,                                               " & vbNewLine _
    '                                          & " ZAITRS.ZONE_CD AS ZONE_CD,                                               " & vbNewLine _
    '                                          & " ZAITRS.LOCA AS LOCA,                                                     " & vbNewLine _
    '                                          & " GOODS.GOODS_CD_CUST AS GOODS_CD_CUST,                                    " & vbNewLine _
    '                                          & " KBN1.KBN_NM1 AS GOODS_COND_NM_1,                                         " & vbNewLine _
    '                                          & " KBN2.KBN_NM1 AS GOODS_COND_NM_2,                                         " & vbNewLine _
    '                                          & " CUSTCOND.JOTAI_NM AS GOODS_COND_NM_3,                                    " & vbNewLine _
    '                                          & " KBN3.KBN_NM1 AS SPD_KB_NM,                                               " & vbNewLine _
    '                                          & " KBN4.KBN_NM1 AS OFB_KB_NM,                                               " & vbNewLine _
    '                                          & " ZAITRS.LT_DATE AS LT_DATE,                                               " & vbNewLine _
    '                                          & " ZAITRS.GOODS_CRT_DATE AS GOODS_CRT_DATE,                                 " & vbNewLine _
    '                                          & " GOODS.SEARCH_KEY_2 AS SEARCH_KEY_2,                                      " & vbNewLine _
    '                                          & " KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                       " & vbNewLine _
    '                                          & " DEST.DEST_NM AS DEST_NM,                                                 " & vbNewLine _
    '                                          & " ZAITRS.RSV_NO AS RSV_NO,                                                 " & vbNewLine _
    '                                          & " ZAITRS.REMARK_OUT AS REMARK_OUT,                                         " & vbNewLine _
    '                                          & " ZAITRS.ZAI_REC_NO AS ZAI_REC_NO,                                         " & vbNewLine _
    '                                          & " CUST.CUST_NM_L AS CUST_NM_L,                                             " & vbNewLine _
    '                                          & " CUST.CUST_NM_M AS CUST_NM_M,                                             " & vbNewLine _
    '                                          & " ZAITRS.REMARK AS REMARK,                                                 " & vbNewLine _
    '                                          & " ZAITRS.ALCTD_QT AS ALCTD_QT,                                             " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_QT AS ALLOC_CAN_QT,                                     " & vbNewLine _
    '                                          & " ZAITRS.PORA_ZAI_QT AS PORA_ZAI_QT,                                       " & vbNewLine _
    '                                          & " GOODS.PKG_NB AS PKG_NB,                                                  " & vbNewLine _
    '                                          & " CUST.HOKAN_NIYAKU_CALCULATION AS HOKAN_NIYAKU_CALCULATION,               " & vbNewLine _
    '                                          & " '' AS OUTKO_DATE,                                          " & vbNewLine _
    '                                          & " ZAITRS.WH_CD AS WH_CD,                                                   " & vbNewLine _
    '                                          & " ZAITRS.NRS_BR_CD AS NRS_BR_CD,                                           " & vbNewLine _
    '                                          & " ZAITRS.SYS_UPD_DATE AS SYS_UPD_DATE,                                     " & vbNewLine _
    '                                          & " ZAITRS.SYS_UPD_TIME AS SYS_UPD_TIME,                                     " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_NB AS IDO_KOSU,                                         " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_PRIORITY AS ALLOC_PRIORITY,                                 " & vbNewLine _
    '                                          & " ZAITRS.GOODS_COND_KB_1 AS GOODS_COND_KB_1,                               " & vbNewLine _
    '                                          & " ZAITRS.GOODS_COND_KB_2 AS GOODS_COND_KB_2,                               " & vbNewLine _
    '                                          & " ZAITRS.GOODS_COND_KB_3 AS GOODS_COND_KB_3,                               " & vbNewLine _
    '                                          & " ZAITRS.SPD_KB AS SPD_KB,                                                 " & vbNewLine _
    '                                          & " ZAITRS.OFB_KB AS OFB_KB,                                                 " & vbNewLine _
    '                                          & " ZAITRS.DEST_CD_P AS DEST_CD,                                             " & vbNewLine _
    '                                          & " GOODS.GOODS_CD_NRS AS GOODS_CD_NRS,                                      " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_L AS CUST_CD_L,                                           " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_M AS CUST_CD_M,                                           " & vbNewLine _
    '                                          & " GOODS.CUST_CD_S AS CUST_CD_S,                                            " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_L AS INKA_NO_L,                                           " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_M AS INKA_NO_M,                                           " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_S AS INKA_NO_S,                                           " & vbNewLine _
    '                                          & " ZAITRS.HOKAN_YN AS HOKAN_YN,                                             " & vbNewLine _
    '                                          & " ZAITRS.TAX_KB AS TAX_KB,                                                 " & vbNewLine _
    '                                          & " ZAITRS.ZERO_FLAG AS ZERO_FLAG,                                           " & vbNewLine _
    '                                          & " ZAITRS.SMPL_FLAG AS SMPL_FLAG                                            " & vbNewLine
    ''' <summary>
    ''' 在庫トランザクションデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                                          " & vbNewLine _
                                              & " GOODS.GOODS_NM_1 AS GOODS_NM,                                            " & vbNewLine _
                                              & " ZAITRS.LOT_NO AS LOT_NO,                                                 " & vbNewLine _
                                              & " ZAITRS.IRIME AS IRIME,                                                   " & vbNewLine _
                                              & " GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                      " & vbNewLine _
                                              & " GOODS.PKG_UT AS PKG_UT,                                                  " & vbNewLine _
                                              & " ZAITRS.INKO_DATE AS INKO_DATE,                                           " & vbNewLine _
                                              & " ZAITRS.SERIAL_NO AS SERIAL_NO,                                           " & vbNewLine _
                                              & " ZAITRS.ALLOC_CAN_NB AS ALLOC_CAN_NB,                                     " & vbNewLine _
                                              & " ZAITRS.ALCTD_NB AS ALCTD_NB,                                             " & vbNewLine _
                                              & " ZAITRS.PORA_ZAI_NB AS PORA_ZAI_NB,                                       " & vbNewLine _
                                              & " ZAITRS.TOU_NO AS TOU_NO,                                                 " & vbNewLine _
                                              & " ZAITRS.SITU_NO AS SITU_NO,                                               " & vbNewLine _
                                              & " ZAITRS.ZONE_CD AS ZONE_CD,                                               " & vbNewLine _
                                              & " ZAITRS.LOCA AS LOCA,                                                     " & vbNewLine _
                                              & " GOODS.GOODS_CD_CUST AS GOODS_CD_CUST,                                    " & vbNewLine _
                                              & " KBN1.KBN_NM1 AS GOODS_COND_NM_1,                                         " & vbNewLine _
                                              & " KBN2.KBN_NM1 AS GOODS_COND_NM_2,                                         " & vbNewLine _
                                              & " CUSTCOND.JOTAI_NM AS GOODS_COND_NM_3,                                    " & vbNewLine _
                                              & " KBN3.KBN_NM1 AS SPD_KB_NM,                                               " & vbNewLine _
                                              & " KBN4.KBN_NM1 AS OFB_KB_NM,                                               " & vbNewLine _
                                              & " ZAITRS.LT_DATE AS LT_DATE,                                               " & vbNewLine _
                                              & " ZAITRS.GOODS_CRT_DATE AS GOODS_CRT_DATE,                                 " & vbNewLine _
                                              & " GOODS.SEARCH_KEY_2 AS SEARCH_KEY_2,                                      " & vbNewLine _
                                              & " KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                       " & vbNewLine _
                                              & " DEST.DEST_NM AS DEST_NM,                                                 " & vbNewLine _
                                              & " ZAITRS.RSV_NO AS RSV_NO,                                                 " & vbNewLine _
                                              & " ZAITRS.REMARK_OUT AS REMARK_OUT,                                         " & vbNewLine _
                                              & " ZAITRS.ZAI_REC_NO AS ZAI_REC_NO,                                         " & vbNewLine _
                                              & " CUST.CUST_NM_L AS CUST_NM_L,                                             " & vbNewLine _
                                              & " CUST.CUST_NM_M AS CUST_NM_M,                                             " & vbNewLine _
                                              & " ZAITRS.REMARK AS REMARK,                                                 " & vbNewLine _
                                              & " ZAITRS.ALCTD_QT AS ALCTD_QT,                                             " & vbNewLine _
                                              & " ZAITRS.ALLOC_CAN_QT AS ALLOC_CAN_QT,                                     " & vbNewLine _
                                              & " ZAITRS.PORA_ZAI_QT AS PORA_ZAI_QT,                                       " & vbNewLine _
                                              & " GOODS.PKG_NB AS PKG_NB,                                                  " & vbNewLine _
                                              & " CUST.HOKAN_NIYAKU_CALCULATION AS HOKAN_NIYAKU_CALCULATION,               " & vbNewLine _
                                              & " '' AS OUTKO_DATE,                                                        " & vbNewLine _
                                              & " ZAITRS.WH_CD AS WH_CD,                                                   " & vbNewLine _
                                              & " ZAITRS.NRS_BR_CD AS NRS_BR_CD,                                           " & vbNewLine _
                                              & " ZAITRS.SYS_UPD_DATE AS SYS_UPD_DATE,                                     " & vbNewLine _
                                              & " ZAITRS.SYS_UPD_TIME AS SYS_UPD_TIME,                                     " & vbNewLine _
                                              & " ZAITRS.ALLOC_CAN_NB AS IDO_KOSU,                                         " & vbNewLine _
                                              & " ZAITRS.ALLOC_PRIORITY AS ALLOC_PRIORITY,                                 " & vbNewLine _
                                              & " ZAITRS.GOODS_COND_KB_1 AS GOODS_COND_KB_1,                               " & vbNewLine _
                                              & " ZAITRS.GOODS_COND_KB_2 AS GOODS_COND_KB_2,                               " & vbNewLine _
                                              & " ZAITRS.GOODS_COND_KB_3 AS GOODS_COND_KB_3,                               " & vbNewLine _
                                              & " ZAITRS.SPD_KB AS SPD_KB,                                                 " & vbNewLine _
                                              & " ZAITRS.OFB_KB AS OFB_KB,                                                 " & vbNewLine _
                                              & " ZAITRS.DEST_CD_P AS DEST_CD,                                             " & vbNewLine _
                                              & " GOODS.GOODS_CD_NRS AS GOODS_CD_NRS,                                      " & vbNewLine _
                                              & " ZAITRS.GOODS_KANRI_NO AS GOODS_KANRI_NO,                                 " & vbNewLine _
                                              & " ZAITRS.CUST_CD_L AS CUST_CD_L,                                           " & vbNewLine _
                                              & " ZAITRS.CUST_CD_M AS CUST_CD_M,                                           " & vbNewLine _
                                              & " GOODS.CUST_CD_S AS CUST_CD_S,                                            " & vbNewLine _
                                              & " ZAITRS.INKA_NO_L AS INKA_NO_L,                                           " & vbNewLine _
                                              & " ZAITRS.INKA_NO_M AS INKA_NO_M,                                           " & vbNewLine _
                                              & " ZAITRS.INKA_NO_S AS INKA_NO_S,                                           " & vbNewLine _
                                              & " ZAITRS.HOKAN_YN AS HOKAN_YN,                                             " & vbNewLine _
                                              & " ZAITRS.TAX_KB AS TAX_KB,                                                 " & vbNewLine _
                                              & " ZAITRS.ZERO_FLAG AS ZERO_FLAG,                                           " & vbNewLine _
                                              & " ZAITRS.SMPL_FLAG AS SMPL_FLAG,                                           " & vbNewLine _
                                              & " ZAITRS.BYK_KEEP_GOODS_CD AS BYK_KEEP_GOODS_CD,                           " & vbNewLine _
                                              & " KBN6.KBN_NM1 AS KEEP_GOODS_NM,                                           " & vbNewLine _
                                              & " ISNULL(CUST_DETAILS.SET_NAIYO, '0') AS IS_BYK_KEEP_GOODS_CD              " & vbNewLine
    'END YANAI 要望番号1327 移動した際に、入目は変更しない
    'END YANAI 要望番号766


    ''' <summary>
    ''' 検索用SQLFROM句(在庫トランザクション）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM                                                                                                    " & vbNewLine _
                                       & "$LM_TRN$..D_ZAI_TRS AS ZAITRS                                                                         " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..M_GOODS AS GOODS                                                            " & vbNewLine _
                                       & "     ON ZAITRS.NRS_BR_CD = GOODS.NRS_BR_CD                                                            " & vbNewLine _
                                       & "     AND ZAITRS.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                                     " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..M_CUST AS CUST                                                              " & vbNewLine _
                                       & "     ON ZAITRS.NRS_BR_CD = CUST.NRS_BR_CD	                                                            " & vbNewLine _
                                       & "     AND ZAITRS.CUST_CD_L = CUST.CUST_CD_L                                                            " & vbNewLine _
                                       & "     AND ZAITRS.CUST_CD_M = CUST.CUST_CD_M	                                                        " & vbNewLine _
                                       & "     AND CUST.CUST_CD_S = '00'                                                                        " & vbNewLine _
                                       & "     AND CUST.CUST_CD_SS = '00'	                                                                    " & vbNewLine _
                                       & "LEFT OUTER JOIN                                                                                       " & vbNewLine _
                                       & "   (SELECT                                                                                            " & vbNewLine _
                                       & "          NRS_BR_CD                                                                                   " & vbNewLine _
                                       & "        , CUST_CD                                                                                     " & vbNewLine _
                                       & "        , CUST_CD_EDA                                                                                 " & vbNewLine _
                                       & "        , SET_NAIYO                                                                                   " & vbNewLine _
                                       & "    FROM                                                                                              " & vbNewLine _
                                       & "        $LM_MST$..M_CUST_DETAILS                                                                      " & vbNewLine _
                                       & "    ) AS CUST_DETAILS                                                                                 " & vbNewLine _
                                       & "        ON (        CUST_DETAILS.NRS_BR_CD                                                            " & vbNewLine _
                                       & "            + ',' + CUST_DETAILS.CUST_CD                                                              " & vbNewLine _
                                       & "            + ',' + CUST_DETAILS.CUST_CD_EDA) =                                                       " & vbNewLine _
                                       & "   (SELECT                                                                                            " & vbNewLine _
                                       & "        ISNULL(MIN(         CUST_DETAILS.NRS_BR_CD                                                    " & vbNewLine _
                                       & "                    + ',' + CUST_DETAILS.CUST_CD                                                      " & vbNewLine _
                                       & "                    + ',' + CUST_DETAILS.CUST_CD_EDA), '') AS PK                                      " & vbNewLine _
                                       & "    FROM                                                                                              " & vbNewLine _
                                       & "        $LM_MST$..M_CUST_DETAILS AS CUST_DETAILS                                                      " & vbNewLine _
                                       & "    WHERE                                                                                             " & vbNewLine _
                                       & "        CUST_DETAILS.NRS_BR_CD = ZAITRS.NRS_BR_CD                                                     " & vbNewLine _
                                       & "    AND CUST_DETAILS.CUST_CD = ZAITRS.CUST_CD_L + ZAITRS.CUST_CD_M                                    " & vbNewLine _
                                       & "    AND CUST_DETAILS.CUST_CLASS = '01'                                                                " & vbNewLine _
                                       & "    AND CUST_DETAILS.SUB_KB = '1Z'                                                                    " & vbNewLine _
                                       & "    AND CUST_DETAILS.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                       & "    GROUP BY                                                                                          " & vbNewLine _
                                       & "          CUST_DETAILS.NRS_BR_CD                                                                      " & vbNewLine _
                                       & "        , CUST_DETAILS.CUST_CD                                                                        " & vbNewLine _
                                       & "    )                                                                                                 " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..M_CUSTCOND AS CUSTCOND		                                                " & vbNewLine _
                                       & "     ON ZAITRS.NRS_BR_CD = CUSTCOND.NRS_BR_CD		                                                    " & vbNewLine _
                                       & "     AND ZAITRS.CUST_CD_L = CUSTCOND.CUST_CD_L                                                        " & vbNewLine _
                                       & "     AND ZAITRS.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD		                                            " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST                                                              " & vbNewLine _
                                       & "     ON ZAITRS.NRS_BR_CD = DEST.NRS_BR_CD                                                             " & vbNewLine _
                                       & "     AND ZAITRS.CUST_CD_L = DEST.CUST_CD_L                                                            " & vbNewLine _
                                       & "     AND ZAITRS.DEST_CD_P = DEST.DEST_CD                                                              " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN1                                                               " & vbNewLine _
                                       & "     ON ZAITRS.GOODS_COND_KB_1  = KBN1.KBN_CD                                                         " & vbNewLine _
                                       & "     AND KBN1.KBN_GROUP_CD = 'S005'                                                                   " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN2                                                               " & vbNewLine _
                                       & "     ON ZAITRS.GOODS_COND_KB_2  = KBN2.KBN_CD                                                         " & vbNewLine _
                                       & "     AND KBN2.KBN_GROUP_CD = 'S006'                                                                   " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN3                                                               " & vbNewLine _
                                       & "     ON ZAITRS.SPD_KB  = KBN3.KBN_CD                                                                  " & vbNewLine _
                                       & "     AND KBN3.KBN_GROUP_CD = 'H003'                                                                   " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN4                                                               " & vbNewLine _
                                       & "     ON ZAITRS.OFB_KB  = KBN4.KBN_CD                                                                  " & vbNewLine _
                                       & "     AND KBN4.KBN_GROUP_CD = 'B002'                                                                   " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN5                                                               " & vbNewLine _
                                       & "     ON ZAITRS.ALLOC_PRIORITY  = KBN5.KBN_CD                                                          " & vbNewLine _
                                       & "     AND KBN5.KBN_GROUP_CD = 'W001'                                                                   " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN6                                                               " & vbNewLine _
                                       & "     ON ZAITRS.BYK_KEEP_GOODS_CD  = KBN6.KBN_CD                                                       " & vbNewLine _
                                       & "     AND KBN6.KBN_GROUP_CD = 'B039'                                                                   " & vbNewLine _

    '& "LEFT OUTER JOIN                                                                                       " & vbNewLine _
    '& "     (SELECT                                                                                          " & vbNewLine _
    '& "             MAX(OUTKAL.OUTKO_DATE) AS OUTKO_DATE,                                                    " & vbNewLine _
    '& "             OUTKAS.NRS_BR_CD AS NRS_BR_CD,                                                           " & vbNewLine _
    '& "             OUTKAS.ZAI_REC_NO AS ZAI_REC_NO                                                          " & vbNewLine _
    '& "     FROM $LM_TRN$..C_OUTKA_L AS OUTKAL                                                               " & vbNewLine _
    '& "     LEFT JOIN $LM_TRN$..C_OUTKA_S AS OUTKAS                                                          " & vbNewLine _
    '& "     ON  OUTKAL.NRS_BR_CD = OUTKAS.NRS_BR_CD                                                          " & vbNewLine _
    '& "     AND OUTKAL.OUTKA_NO_L = OUTKAS.OUTKA_NO_L                                                        " & vbNewLine _
    '& "     WHERE OUTKAL.NRS_BR_CD = @NRS_BR_CD                                                              " & vbNewLine _
    '& "     AND OUTKAL.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
    '& "     AND OUTKAS.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
    '& "     GROUP BY OUTKAS.NRS_BR_CD,OUTKAS.ZAI_REC_NO) OUTKA                                               " & vbNewLine _
    '& "     ON ZAITRS.NRS_BR_CD = OUTKA.NRS_BR_CD                                                            " & vbNewLine _
    '& "     AND ZAITRS.ZAI_REC_NO = OUTKA.ZAI_REC_NO                                                         " & vbNewLine

    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    Private Const SQL_SELECT_TOU_SITU_EXP As String = "SELECT [NRS_BR_CD] AS NRS_BR_CD                          " & vbNewLine _
                                                 & "    ,[WH_CD] AS WH_CD                                       " & vbNewLine _
                                                 & "    ,[TOU_NO] AS TOU_NO                                     " & vbNewLine _
                                                 & "    ,[SITU_NO] AS SITU_NO                                   " & vbNewLine _
                                                 & "    ,[SERIAL_NO] AS SERIAL_NO                               " & vbNewLine _
                                                 & "    ,[NO_APL_GOODS_STR_RULE_APL_DATE_FROM] AS APL_DATE_FROM " & vbNewLine _
                                                 & "    ,[NO_APL_GOODS_STR_RULE_APL_DATE_TO] AS APL_DATE_TO     " & vbNewLine _
                                                 & "    ,[CUST_CD_L] AS CUST_CD_L                               " & vbNewLine _
                                                 & "FROM $LM_MST$..[M_TOU_SITU_EXP]                             " & vbNewLine _
                                                 & "WHERE [NRS_BR_CD] = @NRS_BR_CD                              " & vbNewLine _
                                                 & " AND [WH_CD] = @WH_CD                                       " & vbNewLine _
                                                 & " AND [CUST_CD_L] = @CUST_CD_L                               " & vbNewLine _
                                                 & " AND [NO_APL_GOODS_STR_RULE_APL_DATE_FROM] <= @IDOU_DATE    " & vbNewLine _
                                                 & " AND [NO_APL_GOODS_STR_RULE_APL_DATE_TO] >= @IDOU_DATE      " & vbNewLine _
                                                 & " AND [SYS_DEL_FLG] = '0'                                    " & vbNewLine

    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    'START YANAI 要望番号766
    '''' <summary>
    '''' GROUP_BY BY句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_GROUP_BY As String = " GROUP BY       " & vbNewLine _
    '                                   & "GOODS.GOODS_NM_1,ZAITRS.LOT_NO,ZAITRS.PORA_ZAI_QT,ZAITRS.IRIME,GOODS.STD_IRIME_UT,GOODS.PKG_UT,ZAITRS.INKO_DATE,ZAITRS.SERIAL_NO,ZAITRS.ALLOC_CAN_NB," & vbNewLine _
    '                                   & "ZAITRS.ALCTD_NB,ZAITRS.PORA_ZAI_NB,ZAITRS.TOU_NO,ZAITRS.SITU_NO,ZAITRS.ZONE_CD,ZAITRS.LOCA,GOODS.GOODS_CD_CUST,KBN1.KBN_NM1,KBN2.KBN_NM1,CUSTCOND.JOTAI_NM," & vbNewLine _
    '                                   & "KBN3.KBN_NM1,KBN4.KBN_NM1,ZAITRS.LT_DATE,ZAITRS.GOODS_CRT_DATE,GOODS.SEARCH_KEY_2,KBN5.KBN_NM1,DEST.DEST_NM,ZAITRS.RSV_NO,ZAITRS.REMARK_OUT," & vbNewLine _
    '                                   & "ZAITRS.ZAI_REC_NO,CUST.CUST_NM_L,CUST.CUST_NM_M,ZAITRS.REMARK,ZAITRS.ALCTD_QT,ZAITRS.ALLOC_CAN_QT,ZAITRS.PORA_ZAI_QT,GOODS.PKG_NB,CUST.HOKAN_NIYAKU_CALCULATION," & vbNewLine _
    '                                   & "OUTKA.OUTKO_DATE,ZAITRS.NRS_BR_CD,ZAITRS.SYS_UPD_DATE,ZAITRS.SYS_UPD_TIME,ZAITRS.ALLOC_CAN_NB,ZAITRS.ALLOC_PRIORITY,ZAITRS.GOODS_COND_KB_1,ZAITRS.GOODS_COND_KB_2," & vbNewLine _
    '                                   & "ZAITRS.GOODS_COND_KB_3,ZAITRS.SPD_KB,ZAITRS.OFB_KB,ZAITRS.DEST_CD_P,GOODS.GOODS_CD_NRS," & vbNewLine _
    '                                   & "ZAITRS.CUST_CD_L,ZAITRS.CUST_CD_M,ZAITRS.GOODS_CD_NRS,ZAITRS.INKA_NO_L,ZAITRS.INKA_NO_M,ZAITRS.INKA_NO_S,ZAITRS.HOKAN_YN,ZAITRS.TAX_KB,ZAITRS.ZERO_FLAG,ZAITRS.SMPL_FLAG,ZAITRS.WH_CD" & vbNewLine
    ''' <summary>
    ''' GROUP_BY BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY       " & vbNewLine _
                                       & "GOODS.GOODS_NM_1,ZAITRS.LOT_NO,ZAITRS.PORA_ZAI_QT,ZAITRS.IRIME,GOODS.STD_IRIME_UT,GOODS.PKG_UT,ZAITRS.INKO_DATE,ZAITRS.SERIAL_NO,ZAITRS.ALLOC_CAN_NB," & vbNewLine _
                                       & "ZAITRS.ALCTD_NB,ZAITRS.PORA_ZAI_NB,ZAITRS.TOU_NO,ZAITRS.SITU_NO,ZAITRS.ZONE_CD,ZAITRS.LOCA,GOODS.GOODS_CD_CUST,KBN1.KBN_NM1,KBN2.KBN_NM1,CUSTCOND.JOTAI_NM," & vbNewLine _
                                       & "KBN3.KBN_NM1,KBN4.KBN_NM1,ZAITRS.LT_DATE,ZAITRS.GOODS_CRT_DATE,GOODS.SEARCH_KEY_2,KBN5.KBN_NM1,DEST.DEST_NM,ZAITRS.RSV_NO,ZAITRS.REMARK_OUT," & vbNewLine _
                                       & "ZAITRS.ZAI_REC_NO,CUST.CUST_NM_L,CUST.CUST_NM_M,ZAITRS.REMARK,ZAITRS.ALCTD_QT,ZAITRS.ALLOC_CAN_QT,ZAITRS.PORA_ZAI_QT,GOODS.PKG_NB,CUST.HOKAN_NIYAKU_CALCULATION," & vbNewLine _
                                       & "ZAITRS.NRS_BR_CD,ZAITRS.SYS_UPD_DATE,ZAITRS.SYS_UPD_TIME,ZAITRS.ALLOC_CAN_NB,ZAITRS.ALLOC_PRIORITY,ZAITRS.GOODS_COND_KB_1,ZAITRS.GOODS_COND_KB_2," & vbNewLine _
                                       & "ZAITRS.GOODS_COND_KB_3,ZAITRS.SPD_KB,ZAITRS.OFB_KB,ZAITRS.DEST_CD_P,GOODS.GOODS_CD_NRS,ZAITRS.GOODS_KANRI_NO," & vbNewLine _
                                       & "ZAITRS.CUST_CD_L,ZAITRS.CUST_CD_M,GOODS.CUST_CD_S,ZAITRS.GOODS_CD_NRS,ZAITRS.INKA_NO_L,ZAITRS.INKA_NO_M,ZAITRS.INKA_NO_S,ZAITRS.HOKAN_YN,ZAITRS.TAX_KB,ZAITRS.ZERO_FLAG,ZAITRS.SMPL_FLAG,ZAITRS.BYK_KEEP_GOODS_CD,KBN6.KBN_NM1,CUST_DETAILS.SET_NAIYO,ZAITRS.WH_CD" & vbNewLine
    'END YANAI 要望番号766
    'OUTKA.OUTKO_DATE escape
    ''' <summary>
    ''' ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY       " & vbNewLine _
                                       & "ZAITRS.TOU_NO, ZAITRS.SITU_NO, ZAITRS.LOCA, ZAITRS.SERIAL_NO, GOODS.GOODS_NM_1, ZAITRS.LOT_NO, ZAITRS.INKO_DATE, ZAITRS.PORA_ZAI_NB, ZAITRS.ZAI_REC_NO" & vbNewLine

#If True Then   'add 2021/03/02 強制出庫時のORDEY BY 018870   【LMS】強制出庫の出庫順を棚卸し一覧表と同じにして欲しい
    ''' <summary>
    ''' ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_KYOSEI_SHUKO_ORDER_BY As String = " ORDER BY       " & vbNewLine _
                                       & "ZAITRS.WH_CD,ZAITRS.TOU_NO,ZAITRS.SITU_NO,ZAITRS.ZONE_CD,ZAITRS.LOCA,GOODS.GOODS_NM_1,ZAITRS.LOT_NO " & vbNewLine
#End If
    ''' <summary>
    ''' 状態区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GOODS_COND_FLG As String = "1"

    ''' <summary>
    ''' 次に使用する出庫(入庫)指示番号抽出
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INOUTKONO_NEXT As String = "" _
            & " SELECT                                                                                                      " & vbNewLine _
            & "   A.OUTKO_NO,                                                                                               " & vbNewLine _
            & "   B.INKO_NO                                                                                                 " & vbNewLine _
            & " FROM                                                                                                        " & vbNewLine _
            & "   (                                                                                                         " & vbNewLine _
            & "     SELECT                                                                                                  " & vbNewLine _
            & "       @PREFIX + FORMAT(CAST(SUBSTRING(ISNULL(MAX(OUTKO_NO),'000000000'),2,8) AS INT) + 1,'D8') AS OUTKO_NO  " & vbNewLine _
            & "     FROM                                                                                                    " & vbNewLine _
            & "       $LM_TRN$..D_IDO_TRS                                                                                   " & vbNewLine _
            & "     WHERE                                                                                                   " & vbNewLine _
            & "       NRS_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
            & "       AND LEFT(OUTKO_NO,1) = @PREFIX                                                                        " & vbNewLine _
            & "   ) A                                                                                                       " & vbNewLine _
            & " CROSS JOIN                                                                                                  " & vbNewLine _
            & "   (                                                                                                         " & vbNewLine _
            & "     SELECT                                                                                                  " & vbNewLine _
            & "       @PREFIX + FORMAT(CAST(SUBSTRING(ISNULL(MAX(INKO_NO),'000000000'),2,8) AS INT) + 1,'D8') AS INKO_NO    " & vbNewLine _
            & "     FROM                                                                                                    " & vbNewLine _
            & "       $LM_TRN$..D_IDO_TRS                                                                                   " & vbNewLine _
            & "     WHERE                                                                                                   " & vbNewLine _
            & "       NRS_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
            & "       AND LEFT(INKO_NO,1) = @PREFIX                                                                         " & vbNewLine _
            & "   ) B                                                                                                       " & vbNewLine

#End Region

#Region "保存処理SQL"

    ''' <summary>
    ''' IDO_TRSのINSERT用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_IDO_TRS As String = "INSERT INTO $LM_TRN$..D_IDO_TRS        " & vbNewLine _
                                               & "(                                      " & vbNewLine _
                                               & "       NRS_BR_CD                       " & vbNewLine _
                                               & "      ,REC_NO                          " & vbNewLine _
                                               & "      ,IDO_DATE                        " & vbNewLine _
                                               & "      ,O_ZAI_REC_NO                    " & vbNewLine _
                                               & "      ,O_PORA_ZAI_NB                   " & vbNewLine _
                                               & "      ,O_ALCTD_NB                      " & vbNewLine _
                                               & "      ,O_ALLOC_CAN_NB                  " & vbNewLine _
                                               & "      ,O_IRIME                         " & vbNewLine _
                                               & "      ,N_ZAI_REC_NO                    " & vbNewLine _
                                               & "      ,N_PORA_ZAI_NB                   " & vbNewLine _
                                               & "      ,N_ALCTD_NB                      " & vbNewLine _
                                               & "      ,N_ALLOC_CAN_NB                  " & vbNewLine _
                                               & "      ,REMARK_KBN                      " & vbNewLine _
                                               & "      ,REMARK                          " & vbNewLine _
                                               & "      ,HOKOKU_DATE                     " & vbNewLine _
                                               & "      ,ZAIK_ZAN_FLG                    " & vbNewLine _
                                               & "      ,ZAIK_IRIME                      " & vbNewLine _
                                               & "      ,OUTKO_NO                        " & vbNewLine _
                                               & "      ,INKO_NO                         " & vbNewLine _
                                               & "      ,SYS_ENT_DATE                    " & vbNewLine _
                                               & "      ,SYS_ENT_TIME                    " & vbNewLine _
                                               & "      ,SYS_ENT_PGID                    " & vbNewLine _
                                               & "      ,SYS_ENT_USER                    " & vbNewLine _
                                               & "      ,SYS_UPD_DATE                    " & vbNewLine _
                                               & "      ,SYS_UPD_TIME                    " & vbNewLine _
                                               & "      ,SYS_UPD_PGID                    " & vbNewLine _
                                               & "      ,SYS_UPD_USER                    " & vbNewLine _
                                               & "      ,SYS_DEL_FLG                     " & vbNewLine _
                                               & "       ) VALUES (                      " & vbNewLine _
                                               & "       @NRS_BR_CD                      " & vbNewLine _
                                               & "      ,@REC_NO                         " & vbNewLine _
                                               & "      ,@IDO_DATE                       " & vbNewLine _
                                               & "      ,@O_ZAI_REC_NO                   " & vbNewLine _
                                               & "      ,@O_PORA_ZAI_NB                  " & vbNewLine _
                                               & "      ,@O_ALCTD_NB                     " & vbNewLine _
                                               & "      ,@O_ALLOC_CAN_NB                 " & vbNewLine _
                                               & "      ,@O_IRIME                        " & vbNewLine _
                                               & "      ,@N_ZAI_REC_NO                   " & vbNewLine _
                                               & "      ,@N_PORA_ZAI_NB                  " & vbNewLine _
                                               & "      ,@N_ALCTD_NB                     " & vbNewLine _
                                               & "      ,@N_ALLOC_CAN_NB                 " & vbNewLine _
                                               & "      ,@REMARK_KBN                     " & vbNewLine _
                                               & "      ,@REMARK                         " & vbNewLine _
                                               & "      ,@HOKOKU_DATE                    " & vbNewLine _
                                               & "      ,@ZAIK_ZAN_FLG                   " & vbNewLine _
                                               & "      ,@ZAIK_IRIME                     " & vbNewLine _
                                               & "      ,@OUTKO_NO                       " & vbNewLine _
                                               & "      ,@INKO_NO                        " & vbNewLine _
                                               & "      ,@SYS_ENT_DATE                   " & vbNewLine _
                                               & "      ,@SYS_ENT_TIME                   " & vbNewLine _
                                               & "      ,@SYS_ENT_PGID                   " & vbNewLine _
                                               & "      ,@SYS_ENT_USER                   " & vbNewLine _
                                               & "      ,@SYS_UPD_DATE                   " & vbNewLine _
                                               & "      ,@SYS_UPD_TIME                   " & vbNewLine _
                                               & "      ,@SYS_UPD_PGID                   " & vbNewLine _
                                               & "      ,@SYS_UPD_USER                   " & vbNewLine _
                                               & "      ,@SYS_DEL_FLG                    " & vbNewLine _
                                               & "      )                                " & vbNewLine



    ''' <summary>
    ''' ZAI_TRSのINSERT用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_ZAITRS As String = "INSERT INTO $LM_TRN$..D_ZAI_TRS         " & vbNewLine _
                               & "(                                                      " & vbNewLine _
                               & "       NRS_BR_CD                                       " & vbNewLine _
                               & "      ,ZAI_REC_NO                                      " & vbNewLine _
                               & "      ,WH_CD                                           " & vbNewLine _
                               & "      ,TOU_NO                                          " & vbNewLine _
                               & "      ,SITU_NO                                         " & vbNewLine _
                               & "      ,ZONE_CD                                         " & vbNewLine _
                               & "      ,LOCA                                            " & vbNewLine _
                               & "      ,LOT_NO                                          " & vbNewLine _
                               & "      ,CUST_CD_L                                       " & vbNewLine _
                               & "      ,CUST_CD_M                                       " & vbNewLine _
                               & "      ,GOODS_CD_NRS                                    " & vbNewLine _
                               & "      ,GOODS_KANRI_NO                                  " & vbNewLine _
                               & "      ,INKA_NO_L                                       " & vbNewLine _
                               & "      ,INKA_NO_M                                       " & vbNewLine _
                               & "      ,INKA_NO_S                                       " & vbNewLine _
                               & "      ,ALLOC_PRIORITY                                  " & vbNewLine _
                               & "      ,RSV_NO                                          " & vbNewLine _
                               & "      ,SERIAL_NO                                       " & vbNewLine _
                               & "      ,HOKAN_YN                                        " & vbNewLine _
                               & "      ,TAX_KB                                          " & vbNewLine _
                               & "      ,GOODS_COND_KB_1                                 " & vbNewLine _
                               & "      ,GOODS_COND_KB_2                                 " & vbNewLine _
                               & "      ,GOODS_COND_KB_3                                 " & vbNewLine _
                               & "      ,OFB_KB                                          " & vbNewLine _
                               & "      ,SPD_KB                                          " & vbNewLine _
                               & "      ,REMARK_OUT                                      " & vbNewLine _
                               & "      ,PORA_ZAI_NB                                     " & vbNewLine _
                               & "      ,ALCTD_NB                                        " & vbNewLine _
                               & "      ,ALLOC_CAN_NB                                    " & vbNewLine _
                               & "      ,IRIME                                           " & vbNewLine _
                               & "      ,PORA_ZAI_QT                                     " & vbNewLine _
                               & "      ,ALCTD_QT                                        " & vbNewLine _
                               & "      ,ALLOC_CAN_QT                                    " & vbNewLine _
                               & "      ,INKO_DATE                                       " & vbNewLine _
                               & "      ,INKO_PLAN_DATE                                  " & vbNewLine _
                               & "      ,ZERO_FLAG                                       " & vbNewLine _
                               & "      ,LT_DATE                                         " & vbNewLine _
                               & "      ,GOODS_CRT_DATE                                  " & vbNewLine _
                               & "      ,DEST_CD_P                                       " & vbNewLine _
                               & "      ,REMARK                                          " & vbNewLine _
                               & "      ,SMPL_FLAG                                       " & vbNewLine _
                               & "      ,BYK_KEEP_GOODS_CD                               " & vbNewLine _
                               & "      ,SYS_ENT_DATE                                    " & vbNewLine _
                               & "      ,SYS_ENT_TIME                                    " & vbNewLine _
                               & "      ,SYS_ENT_PGID                                    " & vbNewLine _
                               & "      ,SYS_ENT_USER                                    " & vbNewLine _
                               & "      ,SYS_UPD_DATE                                    " & vbNewLine _
                               & "      ,SYS_UPD_TIME                                    " & vbNewLine _
                               & "      ,SYS_UPD_PGID                                    " & vbNewLine _
                               & "      ,SYS_UPD_USER                                    " & vbNewLine _
                               & "      ,SYS_DEL_FLG                                     " & vbNewLine _
                               & "       ) VALUES (                                      " & vbNewLine _
                               & "       @NRS_BR_CD                                      " & vbNewLine _
                               & "      ,@ZAI_REC_NO                                     " & vbNewLine _
                               & "      ,@WH_CD                                          " & vbNewLine _
                               & "      ,@TOU_NO                                         " & vbNewLine _
                               & "      ,@SITU_NO                                        " & vbNewLine _
                               & "      ,@ZONE_CD                                        " & vbNewLine _
                               & "      ,@LOCA                                           " & vbNewLine _
                               & "      ,@LOT_NO                                         " & vbNewLine _
                               & "      ,@CUST_CD_L                                      " & vbNewLine _
                               & "      ,@CUST_CD_M                                      " & vbNewLine _
                               & "      ,@GOODS_CD_NRS                                   " & vbNewLine _
                               & "      ,@GOODS_KANRI_NO                                 " & vbNewLine _
                               & "      ,@INKA_NO_L                                      " & vbNewLine _
                               & "      ,@INKA_NO_M                                      " & vbNewLine _
                               & "      ,@INKA_NO_S                                      " & vbNewLine _
                               & "      ,@ALLOC_PRIORITY                                 " & vbNewLine _
                               & "      ,@RSV_NO                                         " & vbNewLine _
                               & "      ,@SERIAL_NO                                      " & vbNewLine _
                               & "      ,@HOKAN_YN                                       " & vbNewLine _
                               & "      ,@TAX_KB                                         " & vbNewLine _
                               & "      ,@GOODS_COND_KB_1                                " & vbNewLine _
                               & "      ,@GOODS_COND_KB_2                                " & vbNewLine _
                               & "      ,@GOODS_COND_KB_3                                " & vbNewLine _
                               & "      ,@OFB_KB                                         " & vbNewLine _
                               & "      ,@SPD_KB                                         " & vbNewLine _
                               & "      ,@REMARK_OUT                                     " & vbNewLine _
                               & "      ,@PORA_ZAI_NB                                    " & vbNewLine _
                               & "      ,@ALCTD_NB                                       " & vbNewLine _
                               & "      ,@ALLOC_CAN_NB                                   " & vbNewLine _
                               & "      ,@IRIME                                          " & vbNewLine _
                               & "      ,@PORA_ZAI_QT                                    " & vbNewLine _
                               & "      ,@ALCTD_QT                                       " & vbNewLine _
                               & "      ,@ALLOC_CAN_QT                                   " & vbNewLine _
                               & "      ,@INKO_DATE                                      " & vbNewLine _
                               & "      ,@INKO_PLAN_DATE                                 " & vbNewLine _
                               & "      ,@ZERO_FLAG                                      " & vbNewLine _
                               & "      ,@LT_DATE                                        " & vbNewLine _
                               & "      ,@GOODS_CRT_DATE                                 " & vbNewLine _
                               & "      ,@DEST_CD_P                                      " & vbNewLine _
                               & "      ,@REMARK                                         " & vbNewLine _
                               & "      ,@SMPL_FLAG                                      " & vbNewLine _
                               & "      ,@BYK_KEEP_GOODS_CD                              " & vbNewLine _
                               & "      ,@SYS_ENT_DATE                                   " & vbNewLine _
                               & "      ,@SYS_ENT_TIME                                   " & vbNewLine _
                               & "      ,@SYS_ENT_PGID                                   " & vbNewLine _
                               & "      ,@SYS_ENT_USER                                   " & vbNewLine _
                               & "      ,@SYS_UPD_DATE                                   " & vbNewLine _
                               & "      ,@SYS_UPD_TIME                                   " & vbNewLine _
                               & "      ,@SYS_UPD_PGID                                   " & vbNewLine _
                               & "      ,@SYS_UPD_USER                                   " & vbNewLine _
                               & "      ,@SYS_DEL_FLG                                    " & vbNewLine _
                               & "      )                                                " & vbNewLine

    ''' <summary>
    ''' ZAI_TRSのUPDATE用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATA_ZAITRS As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET             " & vbNewLine _
                                              & " PORA_ZAI_NB  = @PORA_ZAI_NB,              " & vbNewLine _
                                              & " ALLOC_CAN_NB = @ALLOC_CAN_NB,             " & vbNewLine _
                                              & " PORA_ZAI_QT = @PORA_ZAI_QT,               " & vbNewLine _
                                              & " ALLOC_CAN_QT = @ALLOC_CAN_QT,             " & vbNewLine _
                                              & " ZERO_FLAG = @ZERO_FLAG,                   " & vbNewLine _
                                              & " SYS_UPD_DATE = @SYS_UPD_DATE,             " & vbNewLine _
                                              & " SYS_UPD_TIME = @SYS_UPD_TIME,             " & vbNewLine _
                                              & " SYS_UPD_PGID = @SYS_UPD_PGID,             " & vbNewLine _
                                              & " SYS_UPD_USER = @SYS_UPD_USER              " & vbNewLine _
                                              & " WHERE                                     " & vbNewLine _
                                              & " NRS_BR_CD = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND ZAI_REC_NO = @ZAI_REC_NO              " & vbNewLine _
                                              & " AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE      " & vbNewLine _
                                              & " AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME      " & vbNewLine



#End Region

    '要望番号:1350 terakawa 2012.08.29 Start
#Region "入力チェックSQL"

#Region "ZAI_TRS"
    ''' <summary>
    ''' SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GOODS_LOT_CHK As String = "SELECT                                     " & vbNewLine _
                                             & "COUNT(*) AS ZAI_CNT                         " & vbNewLine _
                                             & "FROM                                        " & vbNewLine _
                                             & "$LM_TRN$..D_ZAI_TRS AS ZAI                  " & vbNewLine _
                                             & "LEFT OUTER JOIN                             " & vbNewLine _
                                             & "$LM_MST$..M_GOODS AS MG                     " & vbNewLine _
                                             & "ON MG.NRS_BR_CD = ZAI.NRS_BR_CD             " & vbNewLine _
                                             & "AND MG.GOODS_CD_NRS = ZAI.GOODS_CD_NRS      " & vbNewLine _
                                             & "LEFT OUTER JOIN                             " & vbNewLine _
                                             & "$LM_TRN$..D_IDO_TRS AS ZTRN                 " & vbNewLine _
                                             & "ON ZTRN.NRS_BR_CD = ZAI.NRS_BR_CD           " & vbNewLine _
                                             & "AND ZTRN.O_ZAI_REC_NO = ZAI.ZAI_REC_NO      " & vbNewLine _
                                             & "AND ZTRN.SYS_DEL_FLG='0'                    " & vbNewLine _
                                             & "WHERE                                       " & vbNewLine


    ''' <summary>
    ''' NRS_BR_CD,CUST_CD
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GOODS_LOT_CHK_CUST_CD_L As String = "ZAI.NRS_BR_CD     = @NRS_BR_CD   " & vbNewLine _
                                                        & "AND ZAI.CUST_CD_L = @CUST_CD_L   " & vbNewLine

    Private Const SQL_GOODS_LOT_CHK_CUST_CD_L_DETAIL_PLUS As String = "ZAI.NRS_BR_CD     =    @DETAILS_NRS_BR_CD              " & vbNewLine _
                                                                    & "AND ZAI.CUST_CD_L IN ( @CUST_CD_L, @DETAILS_CUST_CD_L) " & vbNewLine

    '要望番号:1511 KIM 2012/10/12 START
    '''' <summary>
    '''' WHERE
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_GOODS_LOT_CHK_AFTER As String = "AND ZAI.PORA_ZAI_NB > 0                  " & vbNewLine _
    '                                             & "AND ZAI.TOU_NO = @TOU_NO                    " & vbNewLine _
    '                                             & "AND ZAI.SITU_NO = @SITU_NO                  " & vbNewLine _
    '                                             & "AND ZAI.ZONE_CD = @ZONE_CD                  " & vbNewLine _
    '                                             & "AND ZAI.LOCA = @LOCA                        " & vbNewLine _
    '                                             & "AND ((MG.GOODS_CD_CUST = @GOODS_CD_CUST AND LOT_NO <> @LOT_NO)  " & vbNewLine _
    '                                             & "OR (MG.GOODS_CD_CUST <> @GOODS_CD_CUST AND LOT_NO = @LOT_NO))   " & vbNewLine _
    '                                             & "AND ((ZAI.ZAI_REC_NO <> @ZAI_REC_NO AND ZAI.SYS_DEL_FLG = '0')  " & vbNewLine _
    '                                             & "OR (ZTRN.O_ZAI_REC_NO <> @ZAI_REC_NO))                          " & vbNewLine
    Private Const SQL_GOODS_LOT_CHK_AFTER As String = "AND ZAI.PORA_ZAI_NB > 0                     " & vbNewLine _
                                                    & "AND ZAI.TOU_NO = @TOU_NO                    " & vbNewLine _
                                                    & "AND ZAI.SITU_NO = @SITU_NO                  " & vbNewLine _
                                                    & "AND ZAI.ZONE_CD = @ZONE_CD                  " & vbNewLine _
                                                    & "AND ZAI.LOCA = @LOCA                        " & vbNewLine _
                                                    & "AND (MG.GOODS_CD_CUST = @GOODS_CD_CUST AND LOT_NO <> @LOT_NO)       " & vbNewLine _
                                                    & "AND (    (ZAI.ZAI_REC_NO <> @ZAI_REC_NO AND ZAI.SYS_DEL_FLG = '0')  " & vbNewLine _
                                                    & "      OR (ZTRN.O_ZAI_REC_NO <> @ZAI_REC_NO))                        " & vbNewLine

    '要望番号:1511 KIM 2012/10/12 END

#End Region

#Region "M_CUST_DETAILS"

    Private Const SQL_SELECT_CUST_DETAILS As String = "SELECT                                    " & vbNewLine _
                                            & "  CUST_D.SET_NAIYO  AS SET_NAIYO                  " & vbNewLine _
                                            & " ,CUST_D.SET_NAIYO_2  AS SET_NAIYO_2              " & vbNewLine _
                                            & "FROM       $LM_MST$..M_CUST_DETAILS CUST_D        " & vbNewLine _
                                            & "WHERE  CUST_D.NRS_BR_CD   = @NRS_BR_CD            " & vbNewLine _
                                            & "  AND  CUST_D.CUST_CD   = @CUST_CD_L              " & vbNewLine _
                                            & "  AND  CUST_D.SUB_KB    = '41'                    " & vbNewLine _
                                            & "  AND  CUST_D.SYS_DEL_FLG = '0'                   " & vbNewLine

#End Region

#End Region
    '要望番号:1350 terakawa 2012.08.29 End

#Region "強制出庫"

    ''' <summary>
    ''' ファイル情報/4号機指定ステーション取得
    ''' </summary>
    Private Const SQL_SELECT_SEND_INFO As String _
            = " SELECT @NRS_BR_CD              AS NRS_BR_CD          " & vbNewLine _
            & "      , @WH_CD                  AS WH_CD              " & vbNewLine _
            & "      , ISNULL(C.CUST_CD,'')    AS CUST_CD_L          " & vbNewLine _
            & "      , Z.KBN_NM4               AS FOLDER_PATH        " & vbNewLine _
            & "      , Z.KBN_NM5               AS OUT_FILE_NAME      " & vbNewLine _
            & "      , Z.KBN_NM6               AS COMPLETE_FILE_NAME " & vbNewLine _
            & "      , 'OutboundTemporary.csv' AS TEMP_FILE_NAME     " & vbNewLine _
            & "      , ISNULL(C.SET_NAIYO,'')  AS STA_NO_04          " & vbNewLine _
            & "   FROM $LM_MST$..Z_KBN AS Z                          " & vbNewLine _
            & "   LEFT JOIN $LM_MST$..M_CUST_DETAILS AS C            " & vbNewLine _
            & "     ON C.NRS_BR_CD       = @NRS_BR_CD                " & vbNewLine _
            & "        AND C.SUB_KB      = 'A4'                      " & vbNewLine _
            & "        AND C.SYS_DEL_FLG = '0'                       " & vbNewLine _
            & "  WHERE Z.KBN_GROUP_CD    = 'O012'                    " & vbNewLine _
            & "    AND Z.KBN_NM1         = @NRS_BR_CD                " & vbNewLine _
            & "    AND Z.KBN_NM2         = @WH_CD                    " & vbNewLine _
            & "    AND Z.KBN_NM3         = '01'                      " & vbNewLine _
            & "    AND Z.SYS_DEL_FLG     = '0'                       " & vbNewLine

    ''' <summary>
    ''' 自動倉庫出庫予定登録
    ''' </summary>
    Private Const SQL_INSERT_OUTKO_PLAN_AUTO_WH As String _
            = " INSERT INTO $LM_TRN$..H_OUTKO_PLAN_AUTO_WH 		              " & vbNewLine _
            & "            ( 		                                          " & vbNewLine _
            & "             NRS_BR_CD 		                                  " & vbNewLine _
            & "           , ZAI_REC_NO 		                                  " & vbNewLine _
            & "           , SEQ 		                                      " & vbNewLine _
            & "           , OUTKO_ORD_NO 		                              " & vbNewLine _
            & "           , OUTKO_ORD_NO_DTL 		                          " & vbNewLine _
            & "           , PALLET_NO 		                                  " & vbNewLine _
            & "           , OUTKA_NO_L 		                                  " & vbNewLine _
            & "           , ORDER_DATE 		                                  " & vbNewLine _
            & "           , ORDER_TIME 		                                  " & vbNewLine _
            & "           , SYS_ENT_DATE 		                              " & vbNewLine _
            & "           , SYS_ENT_TIME 		                              " & vbNewLine _
            & "           , SYS_ENT_PGID 		                              " & vbNewLine _
            & "           , SYS_ENT_USER 		                              " & vbNewLine _
            & "           , SYS_UPD_DATE 		                              " & vbNewLine _
            & "           , SYS_UPD_TIME 		                              " & vbNewLine _
            & "           , SYS_UPD_PGID 		                              " & vbNewLine _
            & "           , SYS_UPD_USER 		                              " & vbNewLine _
            & "           , SYS_DEL_FLG) 		                              " & vbNewLine _
            & "      VALUES 		                                          " & vbNewLine _
            & "            ( 		                                          " & vbNewLine _
            & "             @NRS_BR_CD 		                                  " & vbNewLine _
            & "           , @ZAI_REC_NO 		                              " & vbNewLine _
            & "           , ISNULL((                                          " & vbNewLine _
            & "                      SELECT MAX(SEQ) + 1                      " & vbNewLine _
            & "                        FROM $LM_TRN$..H_OUTKO_PLAN_AUTO_WH    " & vbNewLine _
            & "                       WHERE NRS_BR_CD  = @NRS_BR_CD           " & vbNewLine _
            & "                         AND ZAI_REC_NO = @ZAI_REC_NO          " & vbNewLine _
            & "                    ), 1)                                      " & vbNewLine _
            & "           , @OUTKO_ORD_NO 		                              " & vbNewLine _
            & "           , @OUTKO_ORD_NO_DTL 		                          " & vbNewLine _
            & "           , @PALLET_NO 		                                  " & vbNewLine _
            & "           , @OUTKA_NO_L 		                              " & vbNewLine _
            & "           , @ORDER_DATE 		                              " & vbNewLine _
            & "           , @ORDER_TIME 		                              " & vbNewLine _
            & "           , @SYS_ENT_DATE 		                              " & vbNewLine _
            & "           , @SYS_ENT_TIME 		                              " & vbNewLine _
            & "           , @SYS_ENT_PGID 		                              " & vbNewLine _
            & "           , @SYS_ENT_USER 		                              " & vbNewLine _
            & "           , @SYS_UPD_DATE 		                              " & vbNewLine _
            & "           , @SYS_UPD_TIME 		                              " & vbNewLine _
            & "           , @SYS_UPD_PGID 		                              " & vbNewLine _
            & "           , @SYS_UPD_USER 		                              " & vbNewLine _
            & "           , @SYS_DEL_FLG 		                              " & vbNewLine _
            & "             ) 		                                          " & vbNewLine

    ''' <summary>
    ''' 日別連番取得
    ''' </summary>
    Private Const SQL_SELECT_FILESEQ As String _
            = " SELECT OUTPUT_DATE                  " & vbNewLine _
            & "      , LAST_SEQ                     " & vbNewLine _
            & "   FROM $LM_TRN$..H_AUTO_WH_FILESEQ  " & vbNewLine _
            & "  WHERE NRS_BR_CD   = @NRS_BR_CD     " & vbNewLine _
            & "    AND WH_CD       = @WH_CD         " & vbNewLine _
            & "    AND OUTPUT_DATE = @OUTPUT_DATE   " & vbNewLine

    ''' <summary>
    ''' 日別連番登録
    ''' </summary>
    Private Const SQL_INSERT_FILESEQ As String _
            = " INSERT INTO $LM_TRN$..H_AUTO_WH_FILESEQ                       " & vbNewLine _
            & "            (                                                  " & vbNewLine _
            & "             NRS_BR_CD                                         " & vbNewLine _
            & "           , WH_CD                                             " & vbNewLine _
            & "           , OUTPUT_DATE                                       " & vbNewLine _
            & "           , LAST_SEQ                                          " & vbNewLine _
            & "           , SYS_ENT_DATE                                      " & vbNewLine _
            & "           , SYS_ENT_TIME                                      " & vbNewLine _
            & "           , SYS_ENT_PGID                                      " & vbNewLine _
            & "           , SYS_ENT_USER                                      " & vbNewLine _
            & "           , SYS_UPD_DATE                                      " & vbNewLine _
            & "           , SYS_UPD_TIME                                      " & vbNewLine _
            & "           , SYS_UPD_PGID                                      " & vbNewLine _
            & "           , SYS_UPD_USER                                      " & vbNewLine _
            & "           , SYS_DEL_FLG)                                      " & vbNewLine _
            & "      VALUES                                                   " & vbNewLine _
            & "            (                                                  " & vbNewLine _
            & "             @NRS_BR_CD                                        " & vbNewLine _
            & "           , @WH_CD                                            " & vbNewLine _
            & "           , @OUTPUT_DATE                                      " & vbNewLine _
            & "           , @LAST_SEQ                                         " & vbNewLine _
            & "           , @SYS_ENT_DATE                                     " & vbNewLine _
            & "           , @SYS_ENT_TIME                                     " & vbNewLine _
            & "           , @SYS_ENT_PGID                                     " & vbNewLine _
            & "           , @SYS_ENT_USER                                     " & vbNewLine _
            & "           , @SYS_UPD_DATE                                     " & vbNewLine _
            & "           , @SYS_UPD_TIME                                     " & vbNewLine _
            & "           , @SYS_UPD_PGID                                     " & vbNewLine _
            & "           , @SYS_UPD_USER                                     " & vbNewLine _
            & "           , @SYS_DEL_FLG                                      " & vbNewLine _
            & "             )                                                 " & vbNewLine

    ''' <summary>
    ''' 日別連番更新
    ''' </summary>
    Private Const SQL_UPDATE_FILESEQ As String _
            = " UPDATE $LM_TRN$..H_AUTO_WH_FILESEQ                            " & vbNewLine _
            & "            SET                                                " & vbNewLine _
            & "             LAST_SEQ = @LAST_SEQ                              " & vbNewLine _
            & "           , SYS_UPD_DATE = @SYS_UPD_DATE                      " & vbNewLine _
            & "           , SYS_UPD_TIME = @SYS_UPD_TIME                      " & vbNewLine _
            & "           , SYS_UPD_PGID = @SYS_UPD_PGID                      " & vbNewLine _
            & "           , SYS_UPD_USER = @SYS_UPD_USER                      " & vbNewLine _
            & "  WHERE NRS_BR_CD   = @NRS_BR_CD                               " & vbNewLine _
            & "    AND WH_CD       = @WH_CD                                   " & vbNewLine _
            & "    AND OUTPUT_DATE = @OUTPUT_DATE                             " & vbNewLine

#End Region '強制出庫

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
    ''' 発行SQL作成用(O_ZAI)
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql_O As StringBuilder

    ''' <summary>
    ''' 発行SQL作成用(N_ZAI)
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql_N As StringBuilder

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
    ''' データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD020DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMD020DAC.SQL_FROM)            'SQL構築(データ抽出用From・Where句1)
        Call Me.SetConditionSQL()                                              '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Dim a As String = reader("SELECT_CNT").ToString()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD020DAC.SQL_SELECT)         'SQL構築(データ抽出用Select句1)
        Me._StrSql.Append(LMD020DAC.SQL_FROM)            'SQL構築(データ抽出用From・Where句1)
        Call Me.SetConditionSQL()                                              '条件設定
        Me._StrSql.Append(SQL_GROUP_BY)
#If False Then   'ADD 2021/03/02   強制出庫時 018870   【LMS】強制出庫の出庫順を棚卸し一覧表と同じにして欲しい
        Me._StrSql.Append(SQL_ORDER_BY)                      'SQL構築(データ抽出用OrderBy句)
#Else
        If Me._Row.Item("KYOSEI_SHUKO").ToString.Trim.Equals("1") = True Then
            Me._StrSql.Append(SQL_KYOSEI_SHUKO_ORDER_BY)                      'SQL構築(データ抽出用OrderBy句)
        Else
            Me._StrSql.Append(SQL_ORDER_BY)                      'SQL構築(データ抽出用OrderBy句)
        End If
#End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("ALLOC_CAN_NB", "ALLOC_CAN_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        'START ADD 2013/09/10 KOBAYASHI WIT対応
        map.Add("GOODS_KANRI_NO", "GOODS_KANRI_NO")
        'END   ADD 2013/09/10 KOBAYASHI WIT対応
        map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
        map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
        map.Add("GOODS_COND_NM_3", "GOODS_COND_NM_3")
        map.Add("SPD_KB_NM", "SPD_KB_NM")
        map.Add("OFB_KB_NM", "OFB_KB_NM")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("ALLOC_PRIORITY_NM", "ALLOC_PRIORITY_NM")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("REMARK", "REMARK")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("ALLOC_CAN_QT", "ALLOC_CAN_QT")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("HOKAN_NIYAKU_CALCULATION", "HOKAN_NIYAKU_CALCULATION")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("WH_CD", "WH_CD")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("IDO_KOSU", "IDO_KOSU")
        map.Add("ALLOC_PRIORITY", "ALLOC_PRIORITY")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        'START YANAI 要望番号766
        map.Add("CUST_CD_S", "CUST_CD_S")
        'END YANAI 要望番号766
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("HOKAN_YN", "HOKAN_YN")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("ZERO_FLAG", "ZERO_FLAG")
        map.Add("SMPL_FLAG", "SMPL_FLAG")
        map.Add("BYK_KEEP_GOODS_CD", "BYK_KEEP_GOODS_CD")
        map.Add("KEEP_GOODS_NM", "KEEP_GOODS_NM")
        map.Add("IS_BYK_KEEP_GOODS_CD", "IS_BYK_KEEP_GOODS_CD")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD020OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD020OUT").Rows.Count())

        Return ds

    End Function

    ''' <summary>
    ''' 最新の請求日検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function SelectSeiqDate(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020_SEIQ_HED_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG000DAC.SQL_SELECT_HOKAN_CHK_DATE)         'SQL構築(データ抽出用Select句1)
        Call Me.SetComSelectParam()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "SelectSeiqDate", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_DATE", "SKYU_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD020_SEIQ_HED_OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD020_SEIQ_HED_OUT").Rows.Count())

        Return ds

    End Function

    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 申請外の商品保管ルール検索処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getTouSituExp(ByVal ds As DataSet) As DataSet

        Dim inTblL As DataTable = ds.Tables("LMD020IN")
        Dim inTblLRow As DataRow = inTblL.Rows(0)
        '営業所コード取得
        Dim nrsBrCd As String = inTblLRow.Item("NRS_BR_CD").ToString
        '倉庫コード取得
        Dim whCd As String = inTblLRow.Item("WH_CD").ToString
        '荷主コード取得
        Dim custCdL As String = inTblLRow.Item("CUST_CD_L").ToString
        '移動日取得
        Dim idouDate As String = inTblLRow.Item("IDO_DATE").ToString

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        Me._StrSql.Append(SQL_SELECT_TOU_SITU_EXP)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), nrsBrCd))

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", nrsBrCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", custCdL, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IDOU_DATE", idouDate, DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "getTouSituExp", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("APL_DATE_FROM", "APL_DATE_FROM")
        map.Add("APL_DATE_TO", "APL_DATE_TO")
        map.Add("CUST_CD_L", "CUST_CD_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD020_TOU_SITU_EXP")

        Return ds

    End Function
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.NRS_BR_CD = @NRS_BR_CD ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '倉庫コード
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.WH_CD = @WH_CD ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード（大）
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.CUST_CD_L LIKE @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード（中）
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.CUST_CD_M LIKE @CUST_CD_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'START YANAI 要望番号766
            '荷主コード（小）
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.CUST_CD_S LIKE @CUST_CD_S")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If
            'END YANAI 要望番号766

            '入荷日From
            whereStr = .Item("INKO_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.INKO_DATE >= @INKO_DATE_FROM ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '入荷日To
            whereStr = .Item("INKO_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.INKO_DATE <= @INKO_DATE_TO ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '移動日
            whereStr = .Item("IDO_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" CUST.HOKAN_NIYAKU_CALCULATION < '" & whereStr & "' ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IDO_DATE", whereStr, DBDataType.CHAR))
            End If

            '状態区分
            whereStr = .Item("GOODS_COND_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If whereStr.Equals(GOODS_COND_FLG) Then
                    If andstr.Length <> 0 Then
                        andstr.Append("AND")
                    End If
                    andstr.Append(" (ZAITRS.GOODS_COND_KB_1 <> '' OR ZAITRS.GOODS_COND_KB_2 <> '' OR ZAITRS.GOODS_COND_KB_3 <> '')")
                    andstr.Append(vbNewLine)
                End If
            End If

            '商品名
            whereStr = .Item("GOODS_NM_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.GOODS_NM_1 LIKE @GOODS_NM_1")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'ロット
            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.LOT_NO LIKE @LOT_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'シリアル№
            whereStr = .Item("SERIAL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.SERIAL_NO LIKE @SERIAL_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '棟
            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.TOU_NO LIKE @TOU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '室
            whereStr = .Item("SITU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.SITU_NO LIKE @SITU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'ZONE
            whereStr = .Item("ZONE_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.ZONE_CD LIKE @ZONE_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'ロケーション
            whereStr = .Item("LOCA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.LOCA LIKE @LOCA")
                andstr.Append(vbNewLine)
                'START YANAI 要望番号799
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                'END YANAI 要望番号799
            End If

            '商品コード
            whereStr = .Item("GOODS_CD_CUST").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.GOODS_CD_CUST LIKE @GOODS_CD_CUST")
                andstr.Append(vbNewLine)
                'START YANAI 要望番号886
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                'END YANAI 要望番号886
            End If

            '状態 中身
            whereStr = .Item("GOODS_COND_KB_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.GOODS_COND_KB_1 = @GOODS_COND_KB_1 ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", whereStr, DBDataType.CHAR))
            End If

            '状態 外装
            whereStr = .Item("GOODS_COND_KB_2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.GOODS_COND_KB_2 = @GOODS_COND_KB_2 ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", whereStr, DBDataType.CHAR))
            End If

            '状態 荷主
            whereStr = .Item("GOODS_COND_KB_3").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                '2017.09.08 状態荷主を検索条件に含める対応START
                andstr.Append(" CUSTCOND.JOTAI_NM LIKE @JOTAI_NM ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOTAI_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                'andstr.Append(" ZAITRS.GOODS_COND_KB_3 = @GOODS_COND_KB_3 ")
                'andstr.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", whereStr, DBDataType.CHAR))
                '2017.09.08 状態荷主を検索条件に含める対応END
            End If

            '保留品
            whereStr = .Item("SPD_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.SPD_KB = @SPD_KB ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPD_KB", whereStr, DBDataType.CHAR))
            End If

            '簿外品
            whereStr = .Item("OFB_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.OFB_KB = @OFB_KB ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OFB_KB", whereStr, DBDataType.CHAR))
            End If

            'キープ品
            whereStr = .Item("BYK_KEEP_GOODS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.BYK_KEEP_GOODS_CD = @BYK_KEEP_GOODS_CD ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BYK_KEEP_GOODS_CD", whereStr, DBDataType.NVARCHAR))
            End If

            '荷主カテゴリ２
            whereStr = .Item("SEARCH_KEY_2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.SEARCH_KEY_2 = @SEARCH_KEY_2 ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_2", whereStr, DBDataType.NVARCHAR))
            End If

            '割当優先
            whereStr = .Item("ALLOC_PRIORITY").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.ALLOC_PRIORITY = @ALLOC_PRIORITY ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", whereStr, DBDataType.CHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST.DEST_NM LIKE @DEST_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '予約番号
            whereStr = .Item("RSV_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.RSV_NO LIKE @RSV_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RSV_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '備考小（社外)
            whereStr = .Item("REMARK_OUT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.REMARK_OUT LIKE @REMARK_OUT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '備考小（社内)
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.REMARK LIKE @REMARK")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '在庫レコード№
            whereStr = .Item("ZAI_REC_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.ZAI_REC_NO LIKE @ZAI_REC_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '削除フラグ
            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If
            andstr.Append(" ZAITRS.SYS_DEL_FLG = '0' ")
            andstr.Append(vbNewLine)
            andstr.Append("AND ZAITRS.PORA_ZAI_NB <> '0'")
            andstr.Append(vbNewLine)
            andstr.Append("AND ZAITRS.ALLOC_CAN_NB <> '0'")
            andstr.Append(vbNewLine)
            andstr.Append("AND ZAITRS.INKO_DATE <> ''")


            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 次に使用する出庫(入庫)指示番号検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>要望管理009859</remarks>
    Private Function SelectInoutkoNoNext(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020_INOUTKONO_NEXT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD020DAC.SQL_SELECT_INOUTKONO_NEXT)

        Me._SqlPrmList = New ArrayList()
        With Me._SqlPrmList
            .Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@PREFIX", Me._Row.Item("PREFIX").ToString(), DBDataType.CHAR))
        End With

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "SelectInoutkoNoNext", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'データのクリア（条件と結果を同一DataTableで処理するため）
        ds.Tables("LMD020_INOUTKONO_NEXT").Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("OUTKO_NO", "OUTKO_NO")
        map.Add("INKO_NO", "INKO_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD020_INOUTKONO_NEXT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD020_INOUTKONO_NEXT").Rows.Count())

        Return ds

    End Function

#End Region

#Region "強制出庫"

    ''' <summary>
    ''' ファイル出力先/4号機指定ステーション取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function SelectSendInfo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020IN_REG")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(SQL_SELECT_SEND_INFO, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "SelectSendInfo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("FOLDER_PATH", "FOLDER_PATH")
        map.Add("OUT_FILE_NAME", "OUT_FILE_NAME")
        map.Add("COMPLETE_FILE_NAME", "COMPLETE_FILE_NAME")
        map.Add("TEMP_FILE_NAME", "TEMP_FILE_NAME")
        map.Add("STA_NO_04", "STA_NO_04")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD020_SEND_INFO")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD020_SEND_INFO").Rows.Count())

        Return ds

    End Function

    ''' <summary>
    ''' 自動倉庫出庫予定テーブル登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function InsertOutkoPlanAutoWh(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020IN_REG")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMD020DAC.SQL_INSERT_OUTKO_PLAN_AUTO_WH, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me._Row.Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKO_ORD_NO", Me._Row.Item("OUTKO_ORD_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKO_ORD_NO_DTL", Me._Row.Item("OUTKO_ORD_NO_DTL").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PALLET_NO", Me._Row.Item("PALLET_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_DATE ", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_TIME ", Me.GetSystemTime(), DBDataType.CHAR))
        Call Me.SetParamCommonSystemIns()
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "InsertOutkoPlanAutoWh", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 日別連番取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function SelectFileSeq(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020IN_REG")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(SQL_SELECT_FILESEQ, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTPUT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "SelectFileSeq", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("OUTPUT_DATE", "OUTPUT_DATE")
        map.Add("LAST_SEQ", "LAST_SEQ")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD020OUT_FILESEQ")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD020OUT_FILESEQ").Rows.Count())

        Return ds

    End Function

    ''' <summary>
    ''' 日別連番登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function InsertFileSeq(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020IN_FILESEQ")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMD020DAC.SQL_INSERT_FILESEQ, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTPUT_DATE", Me._Row.Item("OUTPUT_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_SEQ", Me._Row.Item("LAST_SEQ").ToString(), DBDataType.CHAR))
        Call Me.SetParamCommonSystemIns()
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "InsertFileSeq", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 日別連番更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function UpdateFileSeq(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020IN_FILESEQ")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMD020DAC.SQL_UPDATE_FILESEQ, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_SEQ", Me._Row.Item("LAST_SEQ").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTPUT_DATE", Me._Row.Item("OUTPUT_DATE").ToString(), DBDataType.CHAR))
        Call Me.SetParamCommonSystemUp()
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "UpdateFileSeq", cmd)

        '更新出来なかった場合エラーメッセージをセットして終了
        Me.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

#End Region

#Region "保存処理"

#Region "在庫移動トランザクション登録"

    ''' <summary>
    ''' 在庫移動トランザクション新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫移動トランザクション新規登録SQLの構築・発行</remarks>
    Private Function InsertIdoTrs(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020_IDO")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD020DAC.SQL_INSERT_IDO_TRS, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetIdoTrsComInsertParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "InsertIdoTrs", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

#End Region

#Region "在庫データ登録"

    ''' <summary>
    ''' 在庫データ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データ新規登録SQLの構築・発行</remarks>
    Private Function InsertZaiTrs(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020_ZAI_NEW")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD020DAC.SQL_INSERT_ZAITRS, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetZaiTrsComInsertParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "InsertZaiTrs", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

#End Region

#Region "在庫データ更新"

    ''' <summary>
    ''' 在庫データ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データ更新SQLの構築・発行</remarks>
    Private Function UpdataZaiTrs(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020_ZAI_OLD")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD020DAC.SQL_UPDATA_ZAITRS, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetZaiTrsComUpdateParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemUp()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "UpdataZaiTrs", cmd)

        '更新出来なかった場合エラーメッセージをセットして終了
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If


        Return ds

    End Function

#End Region

#End Region

    '要望番号:1350 terakawa 2012.08.29 Start
#Region "入力チェック"

#Region "同一置場での商品・ロット重複チェック"
    ''' <summary>
    ''' 同一置場での商品・ロット重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkGoodsLot(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020_ZAI_NEW")
        Dim inTblOld As DataTable = ds.Tables("LMD020_ZAI_OLD")
        Dim nrsBrCdDetails As String = String.Empty
        Dim custCdLDetails As String = String.Empty

        '荷主明細から同一置き場・商品チェック特殊荷主情報を取得
        If ds.Tables("CUST_DETAILS").Rows.Count > 0 Then
            nrsBrCdDetails = ds.Tables("CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()
            custCdLDetails = ds.Tables("CUST_DETAILS").Rows(0).Item("SET_NAIYO_2").ToString()
        End If

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL作成
        Me._StrSql.Append(LMD020DAC.SQL_GOODS_LOT_CHK)
        If String.IsNullOrEmpty(custCdLDetails) Then
            Me._StrSql.Append(LMD020DAC.SQL_GOODS_LOT_CHK_CUST_CD_L)
        Else
            Me._StrSql.Append(LMD020DAC.SQL_GOODS_LOT_CHK_CUST_CD_L_DETAIL_PLUS)
        End If
        Me._StrSql.Append(LMD020DAC.SQL_GOODS_LOT_CHK_AFTER)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DETAILS_NRS_BR_CD", nrsBrCdDetails, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DETAILS_CUST_CD_L", custCdLDetails, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row.Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row.Item("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", Me._Row.Item("ZONE_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", Me._Row.Item("LOCA").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", Me._Row.Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me._Row.Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", inTblOld.Rows(0).Item("ZAI_REC_NO").ToString(), DBDataType.NVARCHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC020DAC", "ChkGoodsLot", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("ZAI_CNT")))
        reader.Close()
        Return ds

    End Function
#End Region

#Region "M_CUST_DETAILS"
    ''' <summary>
    ''' 荷主明細取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function GetCustDetail(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD020_ZAI_NEW")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", inTbl.Rows(0).Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMD020DAC.SQL_SELECT_CUST_DETAILS, inTbl.Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD020DAC", "GetCustDetail", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("SET_NAIYO_2", "SET_NAIYO_2")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, "CUST_DETAILS"))

    End Function

#End Region

#End Region
    '要望番号:1350 terakawa 2012.08.29 End

#End Region

#Region "ユーティリティ"

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
    ''' パラメータ設定モジュール(SEIQ_HED)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComSelectParam()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR)) '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

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
    ''' IDO_TRSの登録パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetIdoTrsComInsertParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IDO_DATE", .Item("IDO_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@O_ZAI_REC_NO", .Item("O_ZAI_REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@O_PORA_ZAI_NB", .Item("O_PORA_ZAI_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@O_ALCTD_NB", .Item("O_ALCTD_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@O_ALLOC_CAN_NB", .Item("O_ALLOC_CAN_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@O_IRIME", .Item("O_IRIME").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@N_ZAI_REC_NO", .Item("N_ZAI_REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@N_PORA_ZAI_NB", .Item("N_PORA_ZAI_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@N_ALCTD_NB", .Item("N_ALCTD_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@N_ALLOC_CAN_NB", .Item("N_ALLOC_CAN_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_KBN", .Item("REMARK_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", .Item("HOKOKU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAIK_ZAN_FLG", .Item("ZAIK_ZAN_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAIK_IRIME", .Item("ZAIK_IRIME").ToString(), DBDataType.CHAR))
            'start 要望管理009859
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKO_NO", .Item("OUTKO_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_NO", .Item("INKO_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PREFIX", .Item("PREFIX").ToString(), DBDataType.CHAR))
            'end 要望管理009859

        End With

    End Sub

    ''' <summary>
    ''' ZAI_TRSの登録パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZaiTrsComInsertParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_KANRI_NO", .Item("GOODS_KANRI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", .Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", .Item("HOKAN_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", .Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", .Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", .Item("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OFB_KB", .Item("OFB_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", Me.FormatNumValue(.Item("PORA_ZAI_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", Me.FormatNumValue(.Item("ALLOC_CAN_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", Me.FormatNumValue(.Item("PORA_ZAI_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", Me.FormatNumValue(.Item("ALLOC_CAN_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_DATE", .Item("INKO_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE", .Item("INKO_PLAN_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZERO_FLAG", .Item("ZERO_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE", .Item("LT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", .Item("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD_P", .Item("DEST_CD_P").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SMPL_FLAG", .Item("SMPL_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BYK_KEEP_GOODS_CD", .Item("BYK_KEEP_GOODS_CD").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' ZAI_TRSの更新パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZaiTrsComUpdateParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", Me.FormatNumValue(.Item("PORA_ZAI_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", Me.FormatNumValue(.Item("ALLOC_CAN_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", Me.FormatNumValue(.Item("PORA_ZAI_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", Me.FormatNumValue(.Item("ALLOC_CAN_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZERO_FLAG", .Item("ZERO_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", .Item("GUI_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", .Item("GUI_SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

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

End Class
