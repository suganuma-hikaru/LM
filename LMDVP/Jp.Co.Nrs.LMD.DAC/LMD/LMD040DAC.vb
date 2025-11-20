' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD040    : 在庫履歴照会
'  作  成  者       :  [高道]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD040DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD040DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "現在庫"

#Region "GROUP BY"

#Region "商品"

    Private Const SQL_SELECT_GOODS_GROUP As String = "SELECT                               " & vbNewLine _
                                                   & " MAIN.YOJITU                         " & vbNewLine _
                                                   & ",MAIN.GOODS_CD_CUST                  " & vbNewLine _
                                                   & ",MAIN.GOODS_NM                       " & vbNewLine _
                                                   & ",MAIN.SEARCH_KEY_1                   " & vbNewLine _
                                                   & ",SUM(MAIN.ZANKOSU)     AS ZANKOSU    " & vbNewLine _
                                                   & ",MAIN.NB_UT                          " & vbNewLine _
                                                   & ",SUM(MAIN.ZANSURYO)    AS ZANSURYO   " & vbNewLine _
                                                   & ",SUM(MAIN.ALCTD_NB)    AS ALCTD_NB   " & vbNewLine _
                                                   & ",SUM(MAIN.ALCTD_QT)    AS ALCTD_QT   " & vbNewLine _
                                                   & ",SUM(MAIN.PORA_ZAI_QT) AS PORA_ZAI_QT" & vbNewLine _
                                                   & ",MAIN.STD_IRIME_UT                   " & vbNewLine _
                                                   & ",MAIN.PKG_NB                         " & vbNewLine _
                                                   & ",MAIN.PKG_UT                         " & vbNewLine _
                                                   & ",MAIN.CUST_COST_CD1                  " & vbNewLine _
                                                   & ",MAIN.CUST_COST_CD2                  " & vbNewLine _
                                                   & ",MAIN.SEARCH_KEY_2                   " & vbNewLine _
                                                   & ",MAIN.CUST_NM                        " & vbNewLine _
                                                   & ",MAIN.CUST_CD                        " & vbNewLine _
                                                   & ",MAIN.SHOBO_CD                       " & vbNewLine _
                                                   & ",MAIN.DOKU_KB                        " & vbNewLine _
                                                   & ",MAIN.DOKU_NM                        " & vbNewLine _
                                                   & ",MAIN.ONDO_KB                        " & vbNewLine _
                                                   & ",MAIN.ONDO_NM                        " & vbNewLine _
                                                   & ",MAIN.WH_NM                          " & vbNewLine _
                                                   & ",MAIN.WH_CD                          " & vbNewLine _
                                                   & ",MAIN.NRS_BR_CD                      " & vbNewLine _
                                                   & ",MAIN.GOODS_CD_NRS                   " & vbNewLine _
                                                   & ",MAIN.SHOBO_NM                       " & vbNewLine _
                                                   & "FROM (                               " & vbNewLine

    Private Const SQL_SELECT_GOODS_END As String = ") MAIN                         " & vbNewLine _
                                                 & "GROUP BY MAIN.YOJITU           " & vbNewLine _
                                                 & "        ,MAIN.GOODS_CD_CUST    " & vbNewLine _
                                                 & "        ,MAIN.GOODS_NM         " & vbNewLine _
                                                 & "        ,MAIN.SEARCH_KEY_1     " & vbNewLine _
                                                 & "        ,MAIN.NB_UT            " & vbNewLine _
                                                 & "        ,MAIN.STD_IRIME_UT     " & vbNewLine _
                                                 & "        ,MAIN.PKG_NB           " & vbNewLine _
                                                 & "        ,MAIN.PKG_UT           " & vbNewLine _
                                                 & "        ,MAIN.CUST_COST_CD1    " & vbNewLine _
                                                 & "        ,MAIN.CUST_COST_CD2    " & vbNewLine _
                                                 & "        ,MAIN.SEARCH_KEY_2     " & vbNewLine _
                                                 & "        ,MAIN.CUST_NM          " & vbNewLine _
                                                 & "        ,MAIN.CUST_CD          " & vbNewLine _
                                                 & "        ,MAIN.SHOBO_CD         " & vbNewLine _
                                                 & "        ,MAIN.DOKU_KB          " & vbNewLine _
                                                 & "        ,MAIN.DOKU_NM          " & vbNewLine _
                                                 & "        ,MAIN.ONDO_KB          " & vbNewLine _
                                                 & "        ,MAIN.ONDO_NM          " & vbNewLine _
                                                 & "        ,MAIN.WH_NM            " & vbNewLine _
                                                 & "        ,MAIN.WH_CD            " & vbNewLine _
                                                 & "        ,MAIN.NRS_BR_CD        " & vbNewLine _
                                                 & "        ,MAIN.GOODS_CD_NRS     " & vbNewLine _
                                                 & "        ,MAIN.SHOBO_NM         " & vbNewLine

    'END YANAI 要望番号647

#End Region

#Region "商品・ロット・入目"

    Private Const SQL_SELECT_GOODS_LOT_GROUP As String = "SELECT                               " & vbNewLine _
                                                       & " MAIN.YOJITU                         " & vbNewLine _
                                                       & ",MAIN.GOODS_CD_CUST                  " & vbNewLine _
                                                       & ",MAIN.GOODS_NM                       " & vbNewLine _
                                                       & ",MAIN.SEARCH_KEY_1                   " & vbNewLine _
                                                       & ",MAIN.LOT_NO                         " & vbNewLine _
                                                       & ",MAIN.IRIME                          " & vbNewLine _
                                                       & ",MAIN.IRIME_UT                       " & vbNewLine _
                                                       & ",SUM(MAIN.ZANKOSU)     AS ZANKOSU    " & vbNewLine _
                                                       & ",MAIN.NB_UT                          " & vbNewLine _
                                                       & ",SUM(MAIN.ZANSURYO)    AS ZANSURYO   " & vbNewLine _
                                                       & ",SUM(MAIN.ALCTD_NB)    AS ALCTD_NB   " & vbNewLine _
                                                       & ",SUM(MAIN.ALCTD_QT)    AS ALCTD_QT   " & vbNewLine _
                                                       & ",SUM(MAIN.PORA_ZAI_QT) AS PORA_ZAI_QT" & vbNewLine _
                                                       & ",MAIN.STD_IRIME_UT                   " & vbNewLine _
                                                       & ",MAIN.PKG_NB                         " & vbNewLine _
                                                       & ",MAIN.PKG_UT                         " & vbNewLine _
                                                       & ",MAIN.CUST_COST_CD1                  " & vbNewLine _
                                                       & ",MAIN.CUST_COST_CD2                  " & vbNewLine _
                                                       & ",MAIN.SEARCH_KEY_2                   " & vbNewLine _
                                                       & ",MAIN.CUST_NM                        " & vbNewLine _
                                                       & ",MAIN.CUST_CD                        " & vbNewLine _
                                                       & ",MAIN.SHOBO_CD                       " & vbNewLine _
                                                       & ",MAIN.DOKU_KB                        " & vbNewLine _
                                                       & ",MAIN.DOKU_NM                        " & vbNewLine _
                                                       & ",MAIN.ONDO_KB                        " & vbNewLine _
                                                       & ",MAIN.ONDO_NM                        " & vbNewLine _
                                                       & ",MAIN.WH_NM                          " & vbNewLine _
                                                       & ",MAIN.WH_CD                          " & vbNewLine _
                                                       & ",MAIN.NRS_BR_CD                      " & vbNewLine _
                                                       & ",MAIN.GOODS_CD_NRS                   " & vbNewLine _
                                                       & ",MAIN.SHOBO_NM                       " & vbNewLine _
                                                       & "FROM (                               " & vbNewLine

    Private Const SQL_SELECT_GOODS_LOT_END As String = ") MAIN                                 " & vbNewLine _
                                                     & "GROUP BY MAIN.YOJITU                   " & vbNewLine _
                                                     & "        ,MAIN.GOODS_CD_CUST            " & vbNewLine _
                                                     & "        ,MAIN.GOODS_NM                 " & vbNewLine _
                                                     & "        ,MAIN.SEARCH_KEY_1             " & vbNewLine _
                                                     & "        ,MAIN.LOT_NO                   " & vbNewLine _
                                                     & "        ,MAIN.IRIME                    " & vbNewLine _
                                                     & "        ,MAIN.IRIME_UT                 " & vbNewLine _
                                                     & "        ,MAIN.NB_UT                    " & vbNewLine _
                                                     & "        ,MAIN.STD_IRIME_UT             " & vbNewLine _
                                                     & "        ,MAIN.PKG_NB                   " & vbNewLine _
                                                     & "        ,MAIN.PKG_UT                   " & vbNewLine _
                                                     & "        ,MAIN.CUST_COST_CD1            " & vbNewLine _
                                                     & "        ,MAIN.CUST_COST_CD2            " & vbNewLine _
                                                     & "        ,MAIN.SEARCH_KEY_2             " & vbNewLine _
                                                     & "        ,MAIN.CUST_NM                  " & vbNewLine _
                                                     & "        ,MAIN.CUST_CD                  " & vbNewLine _
                                                     & "        ,MAIN.SHOBO_CD                 " & vbNewLine _
                                                     & "        ,MAIN.DOKU_KB                  " & vbNewLine _
                                                     & "        ,MAIN.DOKU_NM                  " & vbNewLine _
                                                     & "        ,MAIN.ONDO_KB                  " & vbNewLine _
                                                     & "        ,MAIN.ONDO_NM                  " & vbNewLine _
                                                     & "        ,MAIN.WH_NM                    " & vbNewLine _
                                                     & "        ,MAIN.WH_CD                    " & vbNewLine _
                                                     & "        ,MAIN.NRS_BR_CD                " & vbNewLine _
                                                     & "        ,MAIN.GOODS_CD_NRS             " & vbNewLine _
                                                     & "        ,MAIN.SHOBO_NM                 " & vbNewLine

#End Region

#Region "商品・ロット・置場"

    Private Const SQL_SELECT_GOODS_OKIBA_GROUP As String = "SELECT                               " & vbNewLine _
                                                         & " MAIN.YOJITU                         " & vbNewLine _
                                                         & ",MAIN.OKIBA                          " & vbNewLine _
                                                         & ",MAIN.GOODS_CD_CUST                  " & vbNewLine _
                                                         & ",MAIN.GOODS_NM                       " & vbNewLine _
                                                         & ",MAIN.SEARCH_KEY_1                   " & vbNewLine _
                                                         & ",MAIN.IRIME_UT                       " & vbNewLine _
                                                         & ",MAIN.LOT_NO                         " & vbNewLine _
                                                         & ",SUM(MAIN.ZANKOSU)     AS ZANKOSU    " & vbNewLine _
                                                         & ",MAIN.NB_UT                          " & vbNewLine _
                                                         & ",SUM(MAIN.ZANSURYO)    AS ZANSURYO   " & vbNewLine _
                                                         & ",SUM(MAIN.ALCTD_NB)    AS ALCTD_NB   " & vbNewLine _
                                                         & ",SUM(MAIN.ALCTD_QT)    AS ALCTD_QT   " & vbNewLine _
                                                         & ",SUM(MAIN.PORA_ZAI_QT) AS PORA_ZAI_QT" & vbNewLine _
                                                         & ",MAIN.STD_IRIME_UT                   " & vbNewLine _
                                                         & ",MAIN.PKG_NB                         " & vbNewLine _
                                                         & ",MAIN.PKG_UT                         " & vbNewLine _
                                                         & ",MAIN.CUST_COST_CD1                  " & vbNewLine _
                                                         & ",MAIN.CUST_COST_CD2                  " & vbNewLine _
                                                         & ",MAIN.SEARCH_KEY_2                   " & vbNewLine _
                                                         & ",MAIN.CUST_NM                        " & vbNewLine _
                                                         & ",MAIN.CUST_CD                        " & vbNewLine _
                                                         & ",MAIN.SHOBO_CD                       " & vbNewLine _
                                                         & ",MAIN.DOKU_KB                        " & vbNewLine _
                                                         & ",MAIN.DOKU_NM                        " & vbNewLine _
                                                         & ",MAIN.ONDO_KB                        " & vbNewLine _
                                                         & ",MAIN.ONDO_NM                        " & vbNewLine _
                                                         & ",MAIN.WH_NM                          " & vbNewLine _
                                                         & ",MAIN.WH_CD                          " & vbNewLine _
                                                         & ",MAIN.NRS_BR_CD                      " & vbNewLine _
                                                         & ",MAIN.GOODS_CD_NRS                   " & vbNewLine _
                                                         & ",MAIN.SHOBO_NM                       " & vbNewLine _
                                                         & "FROM (                               " & vbNewLine

    Private Const SQL_SELECT_GOODS_OKIBA_END As String = ") MAIN                                 " & vbNewLine _
                                                       & "GROUP BY MAIN.YOJITU                   " & vbNewLine _
                                                       & "        ,MAIN.OKIBA                    " & vbNewLine _
                                                       & "        ,MAIN.GOODS_CD_CUST            " & vbNewLine _
                                                       & "        ,MAIN.GOODS_NM                 " & vbNewLine _
                                                       & "        ,MAIN.IRIME_UT                 " & vbNewLine _
                                                       & "        ,MAIN.SEARCH_KEY_1             " & vbNewLine _
                                                       & "        ,MAIN.LOT_NO                   " & vbNewLine _
                                                       & "        ,MAIN.NB_UT                    " & vbNewLine _
                                                       & "        ,MAIN.STD_IRIME_UT             " & vbNewLine _
                                                       & "        ,MAIN.PKG_NB                   " & vbNewLine _
                                                       & "        ,MAIN.PKG_UT                   " & vbNewLine _
                                                       & "        ,MAIN.CUST_COST_CD1            " & vbNewLine _
                                                       & "        ,MAIN.CUST_COST_CD2            " & vbNewLine _
                                                       & "        ,MAIN.SEARCH_KEY_2             " & vbNewLine _
                                                       & "        ,MAIN.CUST_NM                  " & vbNewLine _
                                                       & "        ,MAIN.CUST_CD                  " & vbNewLine _
                                                       & "        ,MAIN.SHOBO_CD                 " & vbNewLine _
                                                       & "        ,MAIN.DOKU_KB                  " & vbNewLine _
                                                       & "        ,MAIN.DOKU_NM                  " & vbNewLine _
                                                       & "        ,MAIN.ONDO_KB                  " & vbNewLine _
                                                       & "        ,MAIN.ONDO_NM                  " & vbNewLine _
                                                       & "        ,MAIN.WH_NM                    " & vbNewLine _
                                                       & "        ,MAIN.WH_CD                    " & vbNewLine _
                                                       & "        ,MAIN.NRS_BR_CD                " & vbNewLine _
                                                       & "        ,MAIN.GOODS_CD_NRS             " & vbNewLine _
                                                       & "        ,MAIN.SHOBO_NM                 " & vbNewLine

    'START YANAI 要望番号508
    '''' <summary>
    '''' GROUP BY句（履歴表示区分・商品・ロット・置場）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const OKIBA_LOT_SQL_GROUP_BY As String = "GROUP BY                                          " & vbNewLine _
    '                                   & "  GOODS.GOODS_CD_CUST                                         " & vbNewLine _
    '                                   & "  ,GOODS.GOODS_NM_1                                           " & vbNewLine _
    '                                   & "  ,ZAITRS.PORA_ZAI_NB                                         " & vbNewLine _
    '                                   & "  ,ZAITRS.PORA_ZAI_QT                                         " & vbNewLine _
    '                                   & "  ,ZAITRS.ALLOC_CAN_NB                                    " & vbNewLine _
    '                                   & "  ,ZAITRS.ALLOC_CAN_QT                                    " & vbNewLine _
    '                                   & "  ,ZAITRS.LOT_NO                                              " & vbNewLine _
    '                                   & "  ,ZAITRS.TOU_NO                                              " & vbNewLine _
    '                                   & "  ,ZAITRS.SITU_NO                                             " & vbNewLine _
    '                                   & "  ,ZAITRS.ZONE_CD                                             " & vbNewLine _
    '                                   & "  ,ZAITRS.LOCA                                                " & vbNewLine _
    '                                   & "  ,GOODS.NB_UT                                                " & vbNewLine _
    '                                   & "  ,GOODS.STD_IRIME_UT                                         " & vbNewLine _
    '                                   & "  ,GOODS.PKG_NB                                               " & vbNewLine _
    '                                   & "  ,GOODS.PKG_UT                                               " & vbNewLine _
    '                                   & "  ,ZAITRS.CUST_CD_L                                           " & vbNewLine _
    '                                   & "  ,ZAITRS.CUST_CD_M                                           " & vbNewLine _
    '                                   & "  ,GOODS.CUST_CD_S                                            " & vbNewLine _
    '                                   & "  ,GOODS.CUST_CD_SS                                           " & vbNewLine _
    '                                   & "  ,CUST.CUST_NM_L                                             " & vbNewLine _
    '                                   & "  ,CUST.CUST_NM_M                                             " & vbNewLine _
    '                                   & "  ,ZAITRS.GOODS_CD_NRS                                        " & vbNewLine _
    '                                   & "  ,ZAITRS.WH_CD                                               " & vbNewLine _
    '                                   & "  ,SOKO.WH_NM                                                 " & vbNewLine _
    '                                   & "  ,ZAITRS.INKO_DATE                                           " & vbNewLine _
    '                                   & "  ,GOODS.SEARCH_KEY_1                                         " & vbNewLine _
    '                                   & "  ,ZAITRS.OFB_KB                                              " & vbNewLine _
    '                                   & "  ,ZAITRS.SPD_KB                                              " & vbNewLine _
    '                                   & "  ,KBN4.KBN_NM1                                               " & vbNewLine _
    '                                   & "  ,KBN3.KBN_NM1                                               " & vbNewLine _
    '                                   & "  ,KBN5.KBN_NM1                                               " & vbNewLine _
    '                                   & "  ,KBN6.KBN_NM1                                               " & vbNewLine _
    '                                   & "  ,KBN7.KBN_NM1                                               " & vbNewLine _
    '                                   & "  ,KBN8.KBN_NM1                                               " & vbNewLine _
    '                                   & "  ,GOODS.CUST_COST_CD1                                        " & vbNewLine _
    '                                   & "  ,GOODS.CUST_COST_CD2                                        " & vbNewLine _
    '                                   & "  ,GOODS.SEARCH_KEY_2                                         " & vbNewLine _
    '                                   & "  ,ZAITRS.ALLOC_PRIORITY                                      " & vbNewLine _
    '                                   & "  ,ZAITRS.DEST_CD_P                                           " & vbNewLine _
    '                                   & "  ,DEST.DEST_NM                                               " & vbNewLine _
    '                                   & "  ,GOODS.SHOBO_CD                                             " & vbNewLine _
    '                                   & "  ,ZAITRS.TAX_KB                                              " & vbNewLine _
    '                                   & "  ,GOODS.DOKU_KB                                              " & vbNewLine _
    '                                   & "  ,GOODS.ONDO_KB                                              " & vbNewLine _
    '                                   & "  ,ZAITRS.NRS_BR_CD                                           " & vbNewLine _
    '                                   & "  ,NRSBR.NRS_BR_NM                                            " & vbNewLine _
    '                                   & "  ,FURIGOODS.CD_NRS_TO                                        " & vbNewLine _
    '                                   & "  ,ZAITRS.IRIME                                               " & vbNewLine _
    '                                   & "  ,ZAITRS.CUST_CD_L                                           " & vbNewLine _
    '                                   & "  ,ZAITRS.CUST_CD_M                                           " & vbNewLine _
    '                                   & "  ,ZAITRS.INKA_NO_L                                       " & vbNewLine _
    '                                   & "  ,ZAITRS.INKA_NO_M                                       " & vbNewLine _
    '                                   & "  ,ZAITRS.INKA_NO_S                                       " & vbNewLine _
    '                                   & "  ,ZAITRS.ZAI_REC_NO                                      " & vbNewLine
    'START YANAI 要望番号647
    '''' <summary>
    '''' GROUP BY句（履歴表示区分・商品・ロット・置場）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const OKIBA_LOT_SQL_GROUP_BY As String = "GROUP BY                                          " & vbNewLine _
    '                                       & "  GOODS.GOODS_CD_CUST                                         " & vbNewLine _
    '                                       & "  ,GOODS.GOODS_NM_1                                           " & vbNewLine _
    '                                       & "  ,ZAITRS.PORA_ZAI_NB                                         " & vbNewLine _
    '                                       & "  ,ZAITRS.PORA_ZAI_QT                                         " & vbNewLine _
    '                                       & "  ,ZAITRS.ALLOC_CAN_NB                                    " & vbNewLine _
    '                                       & "  ,ZAITRS.ALLOC_CAN_QT                                    " & vbNewLine _
    '                                       & "  ,ZAITRS.LOT_NO                                              " & vbNewLine _
    '                                       & "  ,ZAITRS.TOU_NO                                              " & vbNewLine _
    '                                       & "  ,ZAITRS.SITU_NO                                             " & vbNewLine _
    '                                       & "  ,ZAITRS.ZONE_CD                                             " & vbNewLine _
    '                                       & "  ,ZAITRS.LOCA                                                " & vbNewLine _
    '                                       & "  ,GOODS.NB_UT                                                " & vbNewLine _
    '                                       & "  ,GOODS.STD_IRIME_UT                                         " & vbNewLine _
    '                                       & "  ,GOODS.PKG_NB                                               " & vbNewLine _
    '                                       & "  ,GOODS.PKG_UT                                               " & vbNewLine _
    '                                       & "  ,ZAITRS.CUST_CD_L                                           " & vbNewLine _
    '                                       & "  ,ZAITRS.CUST_CD_M                                           " & vbNewLine _
    '                                       & "  ,GOODS.CUST_CD_S                                            " & vbNewLine _
    '                                       & "  ,GOODS.CUST_CD_SS                                           " & vbNewLine _
    '                                       & "  ,CUST.CUST_NM_L                                             " & vbNewLine _
    '                                       & "  ,CUST.CUST_NM_M                                             " & vbNewLine _
    '                                       & "  ,ZAITRS.GOODS_CD_NRS                                        " & vbNewLine _
    '                                       & "  ,ZAITRS.WH_CD                                               " & vbNewLine _
    '                                       & "  ,SOKO.WH_NM                                                 " & vbNewLine _
    '                                       & "  ,ZAITRS.INKO_DATE                                           " & vbNewLine _
    '                                       & "  ,GOODS.SEARCH_KEY_1                                         " & vbNewLine _
    '                                       & "  ,ZAITRS.OFB_KB                                              " & vbNewLine _
    '                                       & "  ,ZAITRS.SPD_KB                                              " & vbNewLine _
    '                                       & "  ,KBN4.KBN_NM1                                               " & vbNewLine _
    '                                       & "  ,KBN3.KBN_NM1                                               " & vbNewLine _
    '                                       & "  ,KBN5.KBN_NM1                                               " & vbNewLine _
    '                                       & "  ,KBN6.KBN_NM1                                               " & vbNewLine _
    '                                       & "  ,KBN7.KBN_NM1                                               " & vbNewLine _
    '                                       & "  ,KBN8.KBN_NM1                                               " & vbNewLine _
    '                                       & "  ,GOODS.CUST_COST_CD1                                        " & vbNewLine _
    '                                       & "  ,GOODS.CUST_COST_CD2                                        " & vbNewLine _
    '                                       & "  ,GOODS.SEARCH_KEY_2                                         " & vbNewLine _
    '                                       & "  ,ZAITRS.ALLOC_PRIORITY                                      " & vbNewLine _
    '                                       & "  ,ZAITRS.DEST_CD_P                                           " & vbNewLine _
    '                                       & "  ,DEST.DEST_NM                                               " & vbNewLine _
    '                                       & "  ,GOODS.SHOBO_CD                                             " & vbNewLine _
    '                                       & "  ,ZAITRS.TAX_KB                                              " & vbNewLine _
    '                                       & "  ,GOODS.DOKU_KB                                              " & vbNewLine _
    '                                       & "  ,GOODS.ONDO_KB                                              " & vbNewLine _
    '                                       & "  ,ZAITRS.NRS_BR_CD                                           " & vbNewLine _
    '                                       & "  ,NRSBR.NRS_BR_NM                                            " & vbNewLine _
    '                                       & "  ,ZAITRS.IRIME                                               " & vbNewLine _
    '                                       & "  ,ZAITRS.CUST_CD_L                                           " & vbNewLine _
    '                                       & "  ,ZAITRS.CUST_CD_M                                           " & vbNewLine _
    '                                       & "  ,ZAITRS.INKA_NO_L                                       " & vbNewLine _
    '                                       & "  ,ZAITRS.INKA_NO_M                                       " & vbNewLine _
    '                                       & "  ,ZAITRS.INKA_NO_S                                       " & vbNewLine _
    '                                       & "  ,ZAITRS.ZAI_REC_NO                                      " & vbNewLine
    'END YANAI 要望番号647
    'END YANAI 要望番号508

#End Region

#Region "商品・ロット・置場・入目"

    Private Const SQL_SELECT_OKIBA_GROUP As String = "SELECT                               " & vbNewLine _
                                                   & " MAIN.YOJITU                         " & vbNewLine _
                                                   & ",MAIN.OKIBA                          " & vbNewLine _
                                                   & ",MAIN.GOODS_CD_CUST                  " & vbNewLine _
                                                   & ",MAIN.GOODS_NM                       " & vbNewLine _
                                                   & ",MAIN.SEARCH_KEY_1                   " & vbNewLine _
                                                   & ",MAIN.LOT_NO                         " & vbNewLine _
                                                   & ",MAIN.IRIME                          " & vbNewLine _
                                                   & ",MAIN.IRIME_UT                       " & vbNewLine _
                                                   & ",SUM(MAIN.ZANKOSU) AS ZANKOSU        " & vbNewLine _
                                                   & ",MAIN.NB_UT                          " & vbNewLine _
                                                   & ",SUM(MAIN.ZANSURYO) AS ZANSURYO      " & vbNewLine _
                                                   & ",SUM(MAIN.ALCTD_NB) AS ALCTD_NB      " & vbNewLine _
                                                   & ",SUM(MAIN.ALCTD_QT) AS ALCTD_QT      " & vbNewLine _
                                                   & ",SUM(MAIN.PORA_ZAI_QT) AS PORA_ZAI_QT" & vbNewLine _
                                                   & ",MAIN.STD_IRIME_UT                   " & vbNewLine _
                                                   & ",MAIN.PKG_NB                         " & vbNewLine _
                                                   & ",MAIN.PKG_UT                         " & vbNewLine _
                                                   & ",MAIN.CUST_COST_CD1                  " & vbNewLine _
                                                   & ",MAIN.CUST_COST_CD2                  " & vbNewLine _
                                                   & ",MAIN.SEARCH_KEY_2                   " & vbNewLine _
                                                   & ",MAIN.CUST_NM                        " & vbNewLine _
                                                   & ",MAIN.CUST_CD                        " & vbNewLine _
                                                   & ",MAIN.SHOBO_CD                       " & vbNewLine _
                                                   & ",MAIN.DOKU_KB                        " & vbNewLine _
                                                   & ",MAIN.DOKU_NM                        " & vbNewLine _
                                                   & ",MAIN.ONDO_KB                        " & vbNewLine _
                                                   & ",MAIN.ONDO_NM                        " & vbNewLine _
                                                   & ",MAIN.WH_NM                          " & vbNewLine _
                                                   & ",MAIN.WH_CD                          " & vbNewLine _
                                                   & ",MAIN.NRS_BR_CD                      " & vbNewLine _
                                                   & ",MAIN.GOODS_CD_NRS                   " & vbNewLine _
                                                   & ",MAIN.SHOBO_NM                       " & vbNewLine _
                                                   & "FROM (                               " & vbNewLine

    Private Const SQL_SELECT_OKIBA_END As String = ") MAIN                                 " & vbNewLine _
                                                 & "GROUP BY MAIN.YOJITU                   " & vbNewLine _
                                                 & "        ,MAIN.OKIBA                    " & vbNewLine _
                                                 & "        ,MAIN.GOODS_CD_CUST            " & vbNewLine _
                                                 & "        ,MAIN.GOODS_NM                 " & vbNewLine _
                                                 & "        ,MAIN.SEARCH_KEY_1             " & vbNewLine _
                                                 & "        ,MAIN.LOT_NO                   " & vbNewLine _
                                                 & "        ,MAIN.IRIME                    " & vbNewLine _
                                                 & "        ,MAIN.IRIME_UT                 " & vbNewLine _
                                                 & "        ,MAIN.NB_UT                    " & vbNewLine _
                                                 & "        ,MAIN.STD_IRIME_UT             " & vbNewLine _
                                                 & "        ,MAIN.PKG_NB                   " & vbNewLine _
                                                 & "        ,MAIN.PKG_UT                   " & vbNewLine _
                                                 & "        ,MAIN.CUST_COST_CD1            " & vbNewLine _
                                                 & "        ,MAIN.CUST_COST_CD2            " & vbNewLine _
                                                 & "        ,MAIN.SEARCH_KEY_2             " & vbNewLine _
                                                 & "        ,MAIN.CUST_NM                  " & vbNewLine _
                                                 & "        ,MAIN.CUST_CD                  " & vbNewLine _
                                                 & "        ,MAIN.SHOBO_CD                 " & vbNewLine _
                                                 & "        ,MAIN.DOKU_KB                  " & vbNewLine _
                                                 & "        ,MAIN.DOKU_NM                  " & vbNewLine _
                                                 & "        ,MAIN.ONDO_KB                  " & vbNewLine _
                                                 & "        ,MAIN.ONDO_NM                  " & vbNewLine _
                                                 & "        ,MAIN.WH_NM                    " & vbNewLine _
                                                 & "        ,MAIN.WH_CD                    " & vbNewLine _
                                                 & "        ,MAIN.NRS_BR_CD                " & vbNewLine _
                                                 & "        ,MAIN.GOODS_CD_NRS             " & vbNewLine _
                                                 & "        ,MAIN.SHOBO_NM                 " & vbNewLine

    'START YANAI 要望番号508
    '''' <summary>
    '''' GROUP BY句（履歴表示区分・置場）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const OKIBA_SQL_GROUP_BY As String = "GROUP BY                                         " & vbNewLine _
    '                                   & " GOODS.GOODS_CD_CUST                                     " & vbNewLine _
    '                                   & " ,GOODS.GOODS_NM_1                                       " & vbNewLine _
    '                                   & " ,ZAITRS.PORA_ZAI_NB                                     " & vbNewLine _
    '                                   & " ,ZAITRS.PORA_ZAI_QT                                     " & vbNewLine _
    '                                   & " ,ZAITRS.ALLOC_CAN_NB                                    " & vbNewLine _
    '                                   & " ,ZAITRS.ALLOC_CAN_QT                                    " & vbNewLine _
    '                                   & " ,ZAITRS.TOU_NO                                          " & vbNewLine _
    '                                   & " ,ZAITRS.SITU_NO                                         " & vbNewLine _
    '                                   & " ,ZAITRS.ZONE_CD                                         " & vbNewLine _
    '                                   & " ,ZAITRS.LOCA                                            " & vbNewLine _
    '                                   & " ,ZAITRS.LOT_NO                                          " & vbNewLine _
    '                                   & " ,ZAITRS.IRIME                                           " & vbNewLine _
    '                                   & " ,GOODS.NB_UT                                            " & vbNewLine _
    '                                   & " ,GOODS.STD_IRIME_UT                                     " & vbNewLine _
    '                                   & " ,GOODS.PKG_NB                                           " & vbNewLine _
    '                                   & " ,GOODS.PKG_UT                                           " & vbNewLine _
    '                                   & " ,ZAITRS.CUST_CD_L                                       " & vbNewLine _
    '                                   & " ,ZAITRS.CUST_CD_M                                       " & vbNewLine _
    '                                   & " ,GOODS.CUST_CD_S                                        " & vbNewLine _
    '                                   & " ,GOODS.CUST_CD_SS                                       " & vbNewLine _
    '                                   & " ,CUST.CUST_NM_L                                         " & vbNewLine _
    '                                   & " ,CUST.CUST_NM_M                                         " & vbNewLine _
    '                                   & " ,ZAITRS.GOODS_CD_NRS                                    " & vbNewLine _
    '                                   & " ,ZAITRS.WH_CD                                           " & vbNewLine _
    '                                   & " ,SOKO.WH_NM                                             " & vbNewLine _
    '                                   & " ,ZAITRS.INKO_DATE                                       " & vbNewLine _
    '                                   & " ,GOODS.SEARCH_KEY_1                                     " & vbNewLine _
    '                                   & " ,ZAITRS.OFB_KB                                          " & vbNewLine _
    '                                   & " ,ZAITRS.SPD_KB                                          " & vbNewLine _
    '                                   & " ,KBN4.KBN_NM1                                           " & vbNewLine _
    '                                   & " ,KBN3.KBN_NM1                                           " & vbNewLine _
    '                                   & " ,KBN5.KBN_NM1                                           " & vbNewLine _
    '                                   & " ,KBN6.KBN_NM1                                           " & vbNewLine _
    '                                   & " ,KBN7.KBN_NM1                                           " & vbNewLine _
    '                                   & " ,KBN8.KBN_NM1                                           " & vbNewLine _
    '                                   & " ,GOODS.CUST_COST_CD1                                    " & vbNewLine _
    '                                   & " ,GOODS.CUST_COST_CD2                                    " & vbNewLine _
    '                                   & " ,GOODS.SEARCH_KEY_2                                     " & vbNewLine _
    '                                   & " ,ZAITRS.ALLOC_PRIORITY                                  " & vbNewLine _
    '                                   & " ,ZAITRS.DEST_CD_P                                       " & vbNewLine _
    '                                   & " ,DEST.DEST_NM                                           " & vbNewLine _
    '                                   & " ,GOODS.SHOBO_CD                                         " & vbNewLine _
    '                                   & " ,ZAITRS.TAX_KB                                          " & vbNewLine _
    '                                   & " ,GOODS.DOKU_KB                                          " & vbNewLine _
    '                                   & " ,GOODS.ONDO_KB                                          " & vbNewLine _
    '                                   & " ,ZAITRS.NRS_BR_CD                                       " & vbNewLine _
    '                                   & " ,NRSBR.NRS_BR_NM                                        " & vbNewLine _
    '                                   & " ,FURIGOODS.CD_NRS_TO                                    " & vbNewLine _
    '                                   & " ,ZAITRS.IRIME                                           " & vbNewLine _
    '                                   & " ,ZAITRS.CUST_CD_L                                       " & vbNewLine _
    '                                   & " ,ZAITRS.CUST_CD_M                                       " & vbNewLine _
    '                                   & " ,ZAITRS.INKA_NO_L                                       " & vbNewLine _
    '                                   & " ,ZAITRS.INKA_NO_M                                       " & vbNewLine _
    '                                   & " ,ZAITRS.INKA_NO_S                                       " & vbNewLine _
    '                                   & " ,ZAITRS.ZAI_REC_NO                                      " & vbNewLine
    'START YANAI 要望番号647
    '''' <summary>
    '''' GROUP BY句（履歴表示区分・置場）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const OKIBA_SQL_GROUP_BY As String = "GROUP BY                                         " & vbNewLine _
    '                                   & " GOODS.GOODS_CD_CUST                                     " & vbNewLine _
    '                                   & " ,GOODS.GOODS_NM_1                                       " & vbNewLine _
    '                                   & " ,ZAITRS.PORA_ZAI_NB                                     " & vbNewLine _
    '                                   & " ,ZAITRS.PORA_ZAI_QT                                     " & vbNewLine _
    '                                   & " ,ZAITRS.ALLOC_CAN_NB                                    " & vbNewLine _
    '                                   & " ,ZAITRS.ALLOC_CAN_QT                                    " & vbNewLine _
    '                                   & " ,ZAITRS.TOU_NO                                          " & vbNewLine _
    '                                   & " ,ZAITRS.SITU_NO                                         " & vbNewLine _
    '                                   & " ,ZAITRS.ZONE_CD                                         " & vbNewLine _
    '                                   & " ,ZAITRS.LOCA                                            " & vbNewLine _
    '                                   & " ,ZAITRS.LOT_NO                                          " & vbNewLine _
    '                                   & " ,ZAITRS.IRIME                                           " & vbNewLine _
    '                                   & " ,GOODS.NB_UT                                            " & vbNewLine _
    '                                   & " ,GOODS.STD_IRIME_UT                                     " & vbNewLine _
    '                                   & " ,GOODS.PKG_NB                                           " & vbNewLine _
    '                                   & " ,GOODS.PKG_UT                                           " & vbNewLine _
    '                                   & " ,ZAITRS.CUST_CD_L                                       " & vbNewLine _
    '                                   & " ,ZAITRS.CUST_CD_M                                       " & vbNewLine _
    '                                   & " ,GOODS.CUST_CD_S                                        " & vbNewLine _
    '                                   & " ,GOODS.CUST_CD_SS                                       " & vbNewLine _
    '                                   & " ,CUST.CUST_NM_L                                         " & vbNewLine _
    '                                   & " ,CUST.CUST_NM_M                                         " & vbNewLine _
    '                                   & " ,ZAITRS.GOODS_CD_NRS                                    " & vbNewLine _
    '                                   & " ,ZAITRS.WH_CD                                           " & vbNewLine _
    '                                   & " ,SOKO.WH_NM                                             " & vbNewLine _
    '                                   & " ,ZAITRS.INKO_DATE                                       " & vbNewLine _
    '                                   & " ,GOODS.SEARCH_KEY_1                                     " & vbNewLine _
    '                                   & " ,ZAITRS.OFB_KB                                          " & vbNewLine _
    '                                   & " ,ZAITRS.SPD_KB                                          " & vbNewLine _
    '                                   & " ,KBN4.KBN_NM1                                           " & vbNewLine _
    '                                   & " ,KBN3.KBN_NM1                                           " & vbNewLine _
    '                                   & " ,KBN5.KBN_NM1                                           " & vbNewLine _
    '                                   & " ,KBN6.KBN_NM1                                           " & vbNewLine _
    '                                   & " ,KBN7.KBN_NM1                                           " & vbNewLine _
    '                                   & " ,KBN8.KBN_NM1                                           " & vbNewLine _
    '                                   & " ,GOODS.CUST_COST_CD1                                    " & vbNewLine _
    '                                   & " ,GOODS.CUST_COST_CD2                                    " & vbNewLine _
    '                                   & " ,GOODS.SEARCH_KEY_2                                     " & vbNewLine _
    '                                   & " ,ZAITRS.ALLOC_PRIORITY                                  " & vbNewLine _
    '                                   & " ,ZAITRS.DEST_CD_P                                       " & vbNewLine _
    '                                   & " ,DEST.DEST_NM                                           " & vbNewLine _
    '                                   & " ,GOODS.SHOBO_CD                                         " & vbNewLine _
    '                                   & " ,ZAITRS.TAX_KB                                          " & vbNewLine _
    '                                   & " ,GOODS.DOKU_KB                                          " & vbNewLine _
    '                                   & " ,GOODS.ONDO_KB                                          " & vbNewLine _
    '                                   & " ,ZAITRS.NRS_BR_CD                                       " & vbNewLine _
    '                                   & " ,NRSBR.NRS_BR_NM                                        " & vbNewLine _
    '                                   & " ,ZAITRS.IRIME                                           " & vbNewLine _
    '                                   & " ,ZAITRS.CUST_CD_L                                       " & vbNewLine _
    '                                   & " ,ZAITRS.CUST_CD_M                                       " & vbNewLine _
    '                                   & " ,ZAITRS.INKA_NO_L                                       " & vbNewLine _
    '                                   & " ,ZAITRS.INKA_NO_M                                       " & vbNewLine _
    '                                   & " ,ZAITRS.INKA_NO_S                                       " & vbNewLine _
    '                                   & " ,ZAITRS.ZAI_REC_NO                                      " & vbNewLine
    'END YANAI 要望番号647
    'END YANAI 要望番号508

#End Region

#End Region

#Region "SELECT"

#Region "在庫タブ"

#Region "COUNT"

#Region "SELECTStart"

    ''' <summary>
    ''' カウント用Start
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(ZAITRS.NRS_BR_CD)	AS SELECT_CNT            " & vbNewLine

    ''' <summary>
    ''' カウント用Start
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_GROUP As String = " SELECT COUNT(GOODS_CD_CUST)	AS SELECT_CNT FROM(      " & vbNewLine

#End Region

#Region "SELECTEnd"

    ''' <summary>
    ''' カウント用End
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_END As String = " ) AS SELECT_COUNT"

#End Region

#End Region

#Region "商品"

    'START YANAI 要望番号508
    '''' <summary>
    '''' 在庫履歴照会トランザクションデータ抽出用（商品）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const GOODS_SQL_SELECT_DATA As String = " SELECT                                                                                              " & vbNewLine _
    '                                          & " ''  AS  YOJITU,                                                                                         " & vbNewLine _
    '                                          & " GOODS.GOODS_CD_CUST          AS GOODS_CD_CUST,                                                                   " & vbNewLine _
    '                                          & " GOODS.GOODS_NM_1             AS GOODS_NM,                                                                           " & vbNewLine _
    '                                          & " GOODS.SEARCH_KEY_1           AS SEARCH_KEY_1,                                                                     " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_NB AS ZANKOSU,                                                                         " & vbNewLine _
    '                                          & " GOODS.NB_UT                  AS NB_UT,                                                                                   " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_QT AS ZANSURYO,                                                                        " & vbNewLine _
    '                                          & " SUM(ZAITRS.ALCTD_QT) AS ALCTD_QT,                                                                       " & vbNewLine _
    '                                          & " SUM(ZAITRS.PORA_ZAI_QT) AS PORA_ZAI_QT,                                                                 " & vbNewLine _
    '                                          & " GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                     " & vbNewLine _
    '                                          & " SUM(ZAITRS.ALCTD_NB) AS ALCTD_NB,                                                                       " & vbNewLine _
    '                                          & " GOODS.PKG_NB AS PKG_NB,                                                                                 " & vbNewLine _
    '                                          & " GOODS.PKG_UT AS PKG_UT,                                                                                 " & vbNewLine _
    '                                          & " ZAITRS.OFB_KB AS OFB_KB,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.SPD_KB AS SPD_KB,                                                                                " & vbNewLine _
    '                                          & " KBN3.KBN_NM1 AS SPD_KB_NM,                                                                              " & vbNewLine _
    '                                          & " KBN4.KBN_NM1 AS OFB_KB_NM,                                                                              " & vbNewLine _
    '                                          & " GOODS.CUST_COST_CD1 AS CUST_COST_CD1,                                                                   " & vbNewLine _
    '                                          & " GOODS.CUST_COST_CD2 AS CUST_COST_CD2,                                                                   " & vbNewLine _
    '                                          & " GOODS.SEARCH_KEY_2 AS SEARCH_KEY_2,                                                                     " & vbNewLine _
    '                                          & " CUST.CUST_NM_L + '-' + CUST.CUST_NM_M AS CUST_NM,                                                       " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_L + '-' + ZAITRS.CUST_CD_M + '-' + GOODS.CUST_CD_S + '-' + GOODS.CUST_CD_SS AS CUST_CD,  " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_PRIORITY AS ALLOC_PRIORITY,                                                                " & vbNewLine _
    '                                          & " KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                                                      " & vbNewLine _
    '                                          & " ZAITRS.DEST_CD_P AS DEST_CD_P,                                                                          " & vbNewLine _
    '                                          & " GOODS.SHOBO_CD AS SHOBO_CD,                                                                             " & vbNewLine _
    '                                          & " ZAITRS.TAX_KB AS TAX_KB,                                                                                " & vbNewLine _
    '                                          & " KBN6.KBN_NM1 AS TAX_NM,                                                                                 " & vbNewLine _
    '                                          & " GOODS.DOKU_KB AS DOKU_KB,                                                                               " & vbNewLine _
    '                                          & " KBN7.KBN_NM1 AS DOKU_NM,                                                                                " & vbNewLine _
    '                                          & " GOODS.ONDO_KB AS ONDO_KB,                                                                               " & vbNewLine _
    '                                          & " KBN8.KBN_NM1 AS ONDO_NM,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.NRS_BR_CD AS NRS_BR_CD,                                                                          " & vbNewLine _
    '                                          & " NRSBR.NRS_BR_NM AS NRS_BR_NM,                                                                           " & vbNewLine _
    '                                          & " ZAITRS.WH_CD AS WH_CD,                                                                                  " & vbNewLine _
    '                                          & " SOKO.WH_NM AS WH_NM,                                                                                    " & vbNewLine _
    '                                          & " (CASE WHEN FURIGOODS.CD_NRS_TO IS NULL THEN '' ELSE FURIGOODS.CD_NRS_TO END) AS CD_NRS_TO,              " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_L AS CUST_CD_L,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_M AS CUST_CD_M,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_L + '-' + ZAITRS.INKA_NO_M + '-' + ZAITRS.INKA_NO_S AS INKA_NO,                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_L AS INKA_NO_L,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_M AS INKA_NO_M,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_S AS INKA_NO_S,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.ZAI_REC_NO AS ZAI_REC_NO,                                                                        " & vbNewLine _
    '                                          & " ZAITRS.GOODS_CD_NRS AS GOODS_CD_NRS                                                                     " & vbNewLine
    'START YANAI 要望番号647
    '''' <summary>
    '''' 在庫履歴照会トランザクションデータ抽出用（商品）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const GOODS_SQL_SELECT_DATA As String = " SELECT                                                                                              " & vbNewLine _
    '                                              & " ''  AS  YOJITU,                                                                                         " & vbNewLine _
    '                                              & " GOODS.GOODS_CD_CUST          AS GOODS_CD_CUST,                                                                   " & vbNewLine _
    '                                              & " GOODS.GOODS_NM_1             AS GOODS_NM,                                                                           " & vbNewLine _
    '                                              & " GOODS.SEARCH_KEY_1           AS SEARCH_KEY_1,                                                                     " & vbNewLine _
    '                                              & " ZAITRS.ALLOC_CAN_NB AS ZANKOSU,                                                                         " & vbNewLine _
    '                                              & " GOODS.NB_UT                  AS NB_UT,                                                                                   " & vbNewLine _
    '                                              & " ZAITRS.ALLOC_CAN_QT AS ZANSURYO,                                                                        " & vbNewLine _
    '                                              & " SUM(ZAITRS.ALCTD_QT) AS ALCTD_QT,                                                                       " & vbNewLine _
    '                                              & " SUM(ZAITRS.PORA_ZAI_QT) AS PORA_ZAI_QT,                                                                 " & vbNewLine _
    '                                              & " GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                     " & vbNewLine _
    '                                              & " SUM(ZAITRS.ALCTD_NB) AS ALCTD_NB,                                                                       " & vbNewLine _
    '                                              & " GOODS.PKG_NB AS PKG_NB,                                                                                 " & vbNewLine _
    '                                              & " GOODS.PKG_UT AS PKG_UT,                                                                                 " & vbNewLine _
    '                                              & " ZAITRS.OFB_KB AS OFB_KB,                                                                                " & vbNewLine _
    '                                              & " ZAITRS.SPD_KB AS SPD_KB,                                                                                " & vbNewLine _
    '                                              & " KBN3.KBN_NM1 AS SPD_KB_NM,                                                                              " & vbNewLine _
    '                                              & " KBN4.KBN_NM1 AS OFB_KB_NM,                                                                              " & vbNewLine _
    '                                              & " GOODS.CUST_COST_CD1 AS CUST_COST_CD1,                                                                   " & vbNewLine _
    '                                              & " GOODS.CUST_COST_CD2 AS CUST_COST_CD2,                                                                   " & vbNewLine _
    '                                              & " GOODS.SEARCH_KEY_2 AS SEARCH_KEY_2,                                                                     " & vbNewLine _
    '                                              & " CUST.CUST_NM_L + '-' + CUST.CUST_NM_M AS CUST_NM,                                                       " & vbNewLine _
    '                                              & " ZAITRS.CUST_CD_L + '-' + ZAITRS.CUST_CD_M + '-' + GOODS.CUST_CD_S + '-' + GOODS.CUST_CD_SS AS CUST_CD,  " & vbNewLine _
    '                                              & " ZAITRS.ALLOC_PRIORITY AS ALLOC_PRIORITY,                                                                " & vbNewLine _
    '                                              & " KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                                                      " & vbNewLine _
    '                                              & " ZAITRS.DEST_CD_P AS DEST_CD_P,                                                                          " & vbNewLine _
    '                                              & " GOODS.SHOBO_CD AS SHOBO_CD,                                                                             " & vbNewLine _
    '                                              & " ZAITRS.TAX_KB AS TAX_KB,                                                                                " & vbNewLine _
    '                                              & " KBN6.KBN_NM1 AS TAX_NM,                                                                                 " & vbNewLine _
    '                                              & " GOODS.DOKU_KB AS DOKU_KB,                                                                               " & vbNewLine _
    '                                              & " KBN7.KBN_NM1 AS DOKU_NM,                                                                                " & vbNewLine _
    '                                              & " GOODS.ONDO_KB AS ONDO_KB,                                                                               " & vbNewLine _
    '                                              & " KBN8.KBN_NM1 AS ONDO_NM,                                                                                " & vbNewLine _
    '                                              & " ZAITRS.NRS_BR_CD AS NRS_BR_CD,                                                                          " & vbNewLine _
    '                                              & " NRSBR.NRS_BR_NM AS NRS_BR_NM,                                                                           " & vbNewLine _
    '                                              & " ZAITRS.WH_CD AS WH_CD,                                                                                  " & vbNewLine _
    '                                              & " SOKO.WH_NM AS WH_NM,                                                                                    " & vbNewLine _
    '                                              & " '' AS CD_NRS_TO,                                                                                        " & vbNewLine _
    '                                              & " ZAITRS.CUST_CD_L AS CUST_CD_L,                                                                          " & vbNewLine _
    '                                              & " ZAITRS.CUST_CD_M AS CUST_CD_M,                                                                          " & vbNewLine _
    '                                              & " ZAITRS.INKA_NO_L + '-' + ZAITRS.INKA_NO_M + '-' + ZAITRS.INKA_NO_S AS INKA_NO,                          " & vbNewLine _
    '                                              & " ZAITRS.INKA_NO_L AS INKA_NO_L,                                                                          " & vbNewLine _
    '                                              & " ZAITRS.INKA_NO_M AS INKA_NO_M,                                                                          " & vbNewLine _
    '                                              & " ZAITRS.INKA_NO_S AS INKA_NO_S,                                                                          " & vbNewLine _
    '                                              & " ZAITRS.ZAI_REC_NO AS ZAI_REC_NO,                                                                        " & vbNewLine _
    '                                              & " ZAITRS.GOODS_CD_NRS AS GOODS_CD_NRS                                                                     " & vbNewLine
    'END YANAI 要望番号647
    'END YANAI 要望番号508

#End Region

#Region "商品・ロット・入目"

    'START YANAI 要望番号508
    '''' <summary>
    '''' 在庫履歴照会トランザクションデータ抽出用（商品・ロット・入目）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const GOODS_LOT_SQL_SELECT_DATA As String = "SELECT                                                                                           " & vbNewLine _
    '                                          & " ''  AS  YOJITU,                                                                                         " & vbNewLine _
    '                                          & " GOODS.GOODS_CD_CUST AS GOODS_CD_CUST,                                                                   " & vbNewLine _
    '                                          & " GOODS.GOODS_NM_1 AS GOODS_NM,                                                                           " & vbNewLine _
    '                                          & " GOODS.SEARCH_KEY_1 AS SEARCH_KEY_1,                                                                     " & vbNewLine _
    '                                          & " ZAITRS.LOT_NO AS LOT_NO,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.IRIME AS IRIME,                                                                                  " & vbNewLine _
    '                                          & " GOODS.STD_IRIME_UT AS IRIME_UT,                                                                         " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_NB AS ZANKOSU,                                                                         " & vbNewLine _
    '                                          & " GOODS.NB_UT AS NB_UT,                                                                                   " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_QT AS ZANSURYO,                                                                        " & vbNewLine _
    '                                          & " SUM(ZAITRS.ALCTD_QT) AS ALCTD_QT,                                                                       " & vbNewLine _
    '                                          & " SUM(ZAITRS.PORA_ZAI_QT) AS PORA_ZAI_QT,                                                                 " & vbNewLine _
    '                                          & " GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                     " & vbNewLine _
    '                                          & " SUM(ZAITRS.ALCTD_NB) AS ALCTD_NB,                                                                       " & vbNewLine _
    '                                          & " GOODS.PKG_NB AS PKG_NB,                                                                                 " & vbNewLine _
    '                                          & " GOODS.PKG_UT AS PKG_UT,                                                                                 " & vbNewLine _
    '                                          & " ZAITRS.OFB_KB AS OFB_KB,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.SPD_KB AS SPD_KB,                                                                                " & vbNewLine _
    '                                          & " KBN3.KBN_NM1 AS SPD_KB_NM,                                                                              " & vbNewLine _
    '                                          & " KBN4.KBN_NM1 AS OFB_KB_NM,                                                                              " & vbNewLine _
    '                                          & " GOODS.CUST_COST_CD1 AS CUST_COST_CD1,                                                                   " & vbNewLine _
    '                                          & " GOODS.CUST_COST_CD2 AS CUST_COST_CD2,                                                                   " & vbNewLine _
    '                                          & " GOODS.SEARCH_KEY_2 AS SEARCH_KEY_2,                                                                     " & vbNewLine _
    '                                          & " CUST.CUST_NM_L + '-' + CUST.CUST_NM_M AS CUST_NM,                                                       " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_L + '-' + ZAITRS.CUST_CD_M + '-' + GOODS.CUST_CD_S + '-' + GOODS.CUST_CD_SS AS CUST_CD,  " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_PRIORITY AS ALLOC_PRIORITY,                                                                " & vbNewLine _
    '                                          & " KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                                                      " & vbNewLine _
    '                                          & " ZAITRS.DEST_CD_P AS DEST_CD_P,                                                                          " & vbNewLine _
    '                                          & " GOODS.SHOBO_CD AS SHOBO_CD,                                                                             " & vbNewLine _
    '                                          & " ZAITRS.TAX_KB AS TAX_KB,                                                                                " & vbNewLine _
    '                                          & " KBN6.KBN_NM1 AS TAX_NM,                                                                                 " & vbNewLine _
    '                                          & " GOODS.DOKU_KB AS DOKU_KB,                                                                               " & vbNewLine _
    '                                          & " KBN7.KBN_NM1 AS DOKU_NM,                                                                                " & vbNewLine _
    '                                          & " GOODS.ONDO_KB AS ONDO_KB,                                                                               " & vbNewLine _
    '                                          & " KBN8.KBN_NM1 AS ONDO_NM,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.NRS_BR_CD AS NRS_BR_CD,                                                                          " & vbNewLine _
    '                                          & " NRSBR.NRS_BR_NM AS NRS_BR_NM,                                                                           " & vbNewLine _
    '                                          & " ZAITRS.WH_CD AS WH_CD,                                                                                  " & vbNewLine _
    '                                          & " SOKO.WH_NM AS WH_NM,                                                                                    " & vbNewLine _
    '                                          & " (CASE WHEN FURIGOODS.CD_NRS_TO IS NULL THEN '' ELSE FURIGOODS.CD_NRS_TO END) AS CD_NRS_TO,              " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_L AS CUST_CD_L,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_M AS CUST_CD_M,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_L + '-' + ZAITRS.INKA_NO_M + '-' + ZAITRS.INKA_NO_S AS INKA_NO,                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_L AS INKA_NO_L,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_M AS INKA_NO_M,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_S AS INKA_NO_S,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.ZAI_REC_NO AS ZAI_REC_NO,                                                                        " & vbNewLine _
    '                                          & " ZAITRS.GOODS_CD_NRS AS GOODS_CD_NRS                                                                     " & vbNewLine
    'START YANAI 要望番号647
    '''' <summary>
    '''' 在庫履歴照会トランザクションデータ抽出用（商品・ロット・入目）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const GOODS_LOT_SQL_SELECT_DATA As String = "SELECT                                                                                           " & vbNewLine _
    '                                          & " ''  AS  YOJITU,                                                                                         " & vbNewLine _
    '                                          & " GOODS.GOODS_CD_CUST AS GOODS_CD_CUST,                                                                   " & vbNewLine _
    '                                          & " GOODS.GOODS_NM_1 AS GOODS_NM,                                                                           " & vbNewLine _
    '                                          & " GOODS.SEARCH_KEY_1 AS SEARCH_KEY_1,                                                                     " & vbNewLine _
    '                                          & " ZAITRS.LOT_NO AS LOT_NO,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.IRIME AS IRIME,                                                                                  " & vbNewLine _
    '                                          & " GOODS.STD_IRIME_UT AS IRIME_UT,                                                                         " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_NB AS ZANKOSU,                                                                         " & vbNewLine _
    '                                          & " GOODS.NB_UT AS NB_UT,                                                                                   " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_QT AS ZANSURYO,                                                                        " & vbNewLine _
    '                                          & " SUM(ZAITRS.ALCTD_QT) AS ALCTD_QT,                                                                       " & vbNewLine _
    '                                          & " SUM(ZAITRS.PORA_ZAI_QT) AS PORA_ZAI_QT,                                                                 " & vbNewLine _
    '                                          & " GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                     " & vbNewLine _
    '                                          & " SUM(ZAITRS.ALCTD_NB) AS ALCTD_NB,                                                                       " & vbNewLine _
    '                                          & " GOODS.PKG_NB AS PKG_NB,                                                                                 " & vbNewLine _
    '                                          & " GOODS.PKG_UT AS PKG_UT,                                                                                 " & vbNewLine _
    '                                          & " ZAITRS.OFB_KB AS OFB_KB,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.SPD_KB AS SPD_KB,                                                                                " & vbNewLine _
    '                                          & " KBN3.KBN_NM1 AS SPD_KB_NM,                                                                              " & vbNewLine _
    '                                          & " KBN4.KBN_NM1 AS OFB_KB_NM,                                                                              " & vbNewLine _
    '                                          & " GOODS.CUST_COST_CD1 AS CUST_COST_CD1,                                                                   " & vbNewLine _
    '                                          & " GOODS.CUST_COST_CD2 AS CUST_COST_CD2,                                                                   " & vbNewLine _
    '                                          & " GOODS.SEARCH_KEY_2 AS SEARCH_KEY_2,                                                                     " & vbNewLine _
    '                                          & " CUST.CUST_NM_L + '-' + CUST.CUST_NM_M AS CUST_NM,                                                       " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_L + '-' + ZAITRS.CUST_CD_M + '-' + GOODS.CUST_CD_S + '-' + GOODS.CUST_CD_SS AS CUST_CD,  " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_PRIORITY AS ALLOC_PRIORITY,                                                                " & vbNewLine _
    '                                          & " KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                                                      " & vbNewLine _
    '                                          & " ZAITRS.DEST_CD_P AS DEST_CD_P,                                                                          " & vbNewLine _
    '                                          & " GOODS.SHOBO_CD AS SHOBO_CD,                                                                             " & vbNewLine _
    '                                          & " ZAITRS.TAX_KB AS TAX_KB,                                                                                " & vbNewLine _
    '                                          & " KBN6.KBN_NM1 AS TAX_NM,                                                                                 " & vbNewLine _
    '                                          & " GOODS.DOKU_KB AS DOKU_KB,                                                                               " & vbNewLine _
    '                                          & " KBN7.KBN_NM1 AS DOKU_NM,                                                                                " & vbNewLine _
    '                                          & " GOODS.ONDO_KB AS ONDO_KB,                                                                               " & vbNewLine _
    '                                          & " KBN8.KBN_NM1 AS ONDO_NM,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.NRS_BR_CD AS NRS_BR_CD,                                                                          " & vbNewLine _
    '                                          & " NRSBR.NRS_BR_NM AS NRS_BR_NM,                                                                           " & vbNewLine _
    '                                          & " ZAITRS.WH_CD AS WH_CD,                                                                                  " & vbNewLine _
    '                                          & " SOKO.WH_NM AS WH_NM,                                                                                    " & vbNewLine _
    '                                          & " '' AS CD_NRS_TO,                                                                                        " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_L AS CUST_CD_L,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_M AS CUST_CD_M,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_L + '-' + ZAITRS.INKA_NO_M + '-' + ZAITRS.INKA_NO_S AS INKA_NO,                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_L AS INKA_NO_L,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_M AS INKA_NO_M,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_S AS INKA_NO_S,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.ZAI_REC_NO AS ZAI_REC_NO,                                                                        " & vbNewLine _
    '                                          & " ZAITRS.GOODS_CD_NRS AS GOODS_CD_NRS                                                                     " & vbNewLine
    'END YANAI 要望番号647
    'END YANAI 要望番号508

#End Region

#Region "商品・ロット・置場"

    'START YANAI 要望番号508
    '''' <summary>
    '''' 在庫履歴照会トランザクションデータ抽出用（商品・ロット・置場）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const OKIBA_SQL_SELECT_DATA As String = "SELECT                                                                                               " & vbNewLine _
    '                                          & " ''  AS  YOJITU,                                 " & vbNewLine _
    '                                          & "   ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD + (CASE WHEN ZAITRS.LOCA = '' THEN ''" _
    '                                          & " ELSE '-' +  ZAITRS.LOCA END) AS OKIBA,                                                                          " & vbNewLine _
    '                                          & " GOODS.GOODS_CD_CUST AS GOODS_CD_CUST,                                                                   " & vbNewLine _
    '                                          & " GOODS.GOODS_NM_1 AS GOODS_NM,                                                                           " & vbNewLine _
    '                                          & " GOODS.SEARCH_KEY_1 AS SEARCH_KEY_1,                                                                     " & vbNewLine _
    '                                          & " ZAITRS.LOT_NO AS LOT_NO,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.IRIME AS IRIME,                                                                                  " & vbNewLine _
    '                                          & " GOODS.STD_IRIME_UT AS IRIME_UT,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_NB AS ZANKOSU,                                                                         " & vbNewLine _
    '                                          & " GOODS.NB_UT AS NB_UT,                                                                                   " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_QT AS ZANSURYO,                                                                        " & vbNewLine _
    '                                          & " SUM(ZAITRS.ALCTD_QT) AS ALCTD_QT,                                                                       " & vbNewLine _
    '                                          & " SUM(ZAITRS.PORA_ZAI_QT) AS PORA_ZAI_QT,                                                                 " & vbNewLine _
    '                                          & " GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                     " & vbNewLine _
    '                                          & " SUM(ZAITRS.ALCTD_NB) AS ALCTD_NB,                                                                       " & vbNewLine _
    '                                          & " GOODS.PKG_NB AS PKG_NB,                                                                                 " & vbNewLine _
    '                                          & " GOODS.PKG_UT AS PKG_UT,                                                                                 " & vbNewLine _
    '                                          & " ZAITRS.OFB_KB AS OFB_KB,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.SPD_KB AS SPD_KB,                                                                                " & vbNewLine _
    '                                          & " KBN3.KBN_NM1 AS SPD_KB_NM,                                                                              " & vbNewLine _
    '                                          & " KBN4.KBN_NM1 AS OFB_KB_NM,                                                                              " & vbNewLine _
    '                                          & " GOODS.CUST_COST_CD1 AS CUST_COST_CD1,                                                                   " & vbNewLine _
    '                                          & " GOODS.CUST_COST_CD2 AS CUST_COST_CD2,                                                                   " & vbNewLine _
    '                                          & " GOODS.SEARCH_KEY_2 AS SEARCH_KEY_2,                                                                     " & vbNewLine _
    '                                          & " CUST.CUST_NM_L + '-' + CUST.CUST_NM_M AS CUST_NM,                                                       " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_L + '-' + ZAITRS.CUST_CD_M + '-' + GOODS.CUST_CD_S + '-' + GOODS.CUST_CD_SS AS CUST_CD,  " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_PRIORITY AS ALLOC_PRIORITY,                                                                " & vbNewLine _
    '                                          & " KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                                                      " & vbNewLine _
    '                                          & " ZAITRS.DEST_CD_P AS DEST_CD_P,                                                                          " & vbNewLine _
    '                                          & " GOODS.SHOBO_CD AS SHOBO_CD,                                                                             " & vbNewLine _
    '                                          & " ZAITRS.TAX_KB AS TAX_KB,                                                                                " & vbNewLine _
    '                                          & " KBN6.KBN_NM1 AS TAX_NM,                                                                                 " & vbNewLine _
    '                                          & " GOODS.DOKU_KB AS DOKU_KB,                                                                               " & vbNewLine _
    '                                          & " KBN7.KBN_NM1 AS DOKU_NM,                                                                                " & vbNewLine _
    '                                          & " GOODS.ONDO_KB AS ONDO_KB,                                                                               " & vbNewLine _
    '                                          & " KBN8.KBN_NM1 AS ONDO_NM,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.NRS_BR_CD AS NRS_BR_CD,                                                                          " & vbNewLine _
    '                                          & " NRSBR.NRS_BR_NM AS NRS_BR_NM,                                                                           " & vbNewLine _
    '                                          & " ZAITRS.WH_CD AS WH_CD,                                                                                  " & vbNewLine _
    '                                          & " SOKO.WH_NM AS WH_NM,                                                                                    " & vbNewLine _
    '                                          & " (CASE WHEN FURIGOODS.CD_NRS_TO IS NULL THEN '' ELSE FURIGOODS.CD_NRS_TO END) AS CD_NRS_TO,              " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_L AS CUST_CD_L,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_M AS CUST_CD_M,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_L + '-' + ZAITRS.INKA_NO_M + '-' + ZAITRS.INKA_NO_S AS INKA_NO,                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_L AS INKA_NO_L,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_M AS INKA_NO_M,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_S AS INKA_NO_S,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.ZAI_REC_NO AS ZAI_REC_NO,                                                                        " & vbNewLine _
    '                                          & " ZAITRS.GOODS_CD_NRS AS GOODS_CD_NRS                                                                     " & vbNewLine
    'START YANAI 要望番号647
    '''' <summary>
    '''' 在庫履歴照会トランザクションデータ抽出用（商品・ロット・置場）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const OKIBA_SQL_SELECT_DATA As String = "SELECT                                                                                               " & vbNewLine _
    '                                          & " ''  AS  YOJITU,                                 " & vbNewLine _
    '                                          & "   ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD + (CASE WHEN ZAITRS.LOCA = '' THEN ''" _
    '                                          & " ELSE '-' +  ZAITRS.LOCA END) AS OKIBA,                                                                          " & vbNewLine _
    '                                          & " GOODS.GOODS_CD_CUST AS GOODS_CD_CUST,                                                                   " & vbNewLine _
    '                                          & " GOODS.GOODS_NM_1 AS GOODS_NM,                                                                           " & vbNewLine _
    '                                          & " GOODS.SEARCH_KEY_1 AS SEARCH_KEY_1,                                                                     " & vbNewLine _
    '                                          & " ZAITRS.LOT_NO AS LOT_NO,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.IRIME AS IRIME,                                                                                  " & vbNewLine _
    '                                          & " GOODS.STD_IRIME_UT AS IRIME_UT,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_NB AS ZANKOSU,                                                                         " & vbNewLine _
    '                                          & " GOODS.NB_UT AS NB_UT,                                                                                   " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_CAN_QT AS ZANSURYO,                                                                        " & vbNewLine _
    '                                          & " SUM(ZAITRS.ALCTD_QT) AS ALCTD_QT,                                                                       " & vbNewLine _
    '                                          & " SUM(ZAITRS.PORA_ZAI_QT) AS PORA_ZAI_QT,                                                                 " & vbNewLine _
    '                                          & " GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                     " & vbNewLine _
    '                                          & " SUM(ZAITRS.ALCTD_NB) AS ALCTD_NB,                                                                       " & vbNewLine _
    '                                          & " GOODS.PKG_NB AS PKG_NB,                                                                                 " & vbNewLine _
    '                                          & " GOODS.PKG_UT AS PKG_UT,                                                                                 " & vbNewLine _
    '                                          & " ZAITRS.OFB_KB AS OFB_KB,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.SPD_KB AS SPD_KB,                                                                                " & vbNewLine _
    '                                          & " KBN3.KBN_NM1 AS SPD_KB_NM,                                                                              " & vbNewLine _
    '                                          & " KBN4.KBN_NM1 AS OFB_KB_NM,                                                                              " & vbNewLine _
    '                                          & " GOODS.CUST_COST_CD1 AS CUST_COST_CD1,                                                                   " & vbNewLine _
    '                                          & " GOODS.CUST_COST_CD2 AS CUST_COST_CD2,                                                                   " & vbNewLine _
    '                                          & " GOODS.SEARCH_KEY_2 AS SEARCH_KEY_2,                                                                     " & vbNewLine _
    '                                          & " CUST.CUST_NM_L + '-' + CUST.CUST_NM_M AS CUST_NM,                                                       " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_L + '-' + ZAITRS.CUST_CD_M + '-' + GOODS.CUST_CD_S + '-' + GOODS.CUST_CD_SS AS CUST_CD,  " & vbNewLine _
    '                                          & " ZAITRS.ALLOC_PRIORITY AS ALLOC_PRIORITY,                                                                " & vbNewLine _
    '                                          & " KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                                                      " & vbNewLine _
    '                                          & " ZAITRS.DEST_CD_P AS DEST_CD_P,                                                                          " & vbNewLine _
    '                                          & " GOODS.SHOBO_CD AS SHOBO_CD,                                                                             " & vbNewLine _
    '                                          & " ZAITRS.TAX_KB AS TAX_KB,                                                                                " & vbNewLine _
    '                                          & " KBN6.KBN_NM1 AS TAX_NM,                                                                                 " & vbNewLine _
    '                                          & " GOODS.DOKU_KB AS DOKU_KB,                                                                               " & vbNewLine _
    '                                          & " KBN7.KBN_NM1 AS DOKU_NM,                                                                                " & vbNewLine _
    '                                          & " GOODS.ONDO_KB AS ONDO_KB,                                                                               " & vbNewLine _
    '                                          & " KBN8.KBN_NM1 AS ONDO_NM,                                                                                " & vbNewLine _
    '                                          & " ZAITRS.NRS_BR_CD AS NRS_BR_CD,                                                                          " & vbNewLine _
    '                                          & " NRSBR.NRS_BR_NM AS NRS_BR_NM,                                                                           " & vbNewLine _
    '                                          & " ZAITRS.WH_CD AS WH_CD,                                                                                  " & vbNewLine _
    '                                          & " SOKO.WH_NM AS WH_NM,                                                                                    " & vbNewLine _
    '                                          & " '' AS CD_NRS_TO,                                                                                        " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_L AS CUST_CD_L,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.CUST_CD_M AS CUST_CD_M,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_L + '-' + ZAITRS.INKA_NO_M + '-' + ZAITRS.INKA_NO_S AS INKA_NO,                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_L AS INKA_NO_L,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_M AS INKA_NO_M,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.INKA_NO_S AS INKA_NO_S,                                                                          " & vbNewLine _
    '                                          & " ZAITRS.ZAI_REC_NO AS ZAI_REC_NO,                                                                        " & vbNewLine _
    '                                          & " ZAITRS.GOODS_CD_NRS AS GOODS_CD_NRS                                                                     " & vbNewLine
    'END YANAI 要望番号647
    'END YANAI 要望番号508

#End Region

#Region "詳細"

    'START YANAI 要望番号508
    '''' <summary>
    '''' 在庫履歴照会トランザクションデータ抽出用（詳細）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const ALL_SQL_SELECT_DATA As String = " SELECT                                                                                                " & vbNewLine _
    '                                          & " (CASE WHEN RTRIM(LTRIM(ZAITRS.INKO_DATE)) = '' THEN '予' ELSE '' END)  AS  YOJITU,                                 " & vbNewLine _
    '                                      & "   ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD + (CASE WHEN ZAITRS.LOCA = '' THEN ''" _
    '                                      & " ELSE '-' +  ZAITRS.LOCA END) AS OKIBA,                                                                          " & vbNewLine _
    '                                      & " GOODS.GOODS_CD_CUST AS GOODS_CD_CUST,                                                                       " & vbNewLine _
    '                                      & " GOODS.GOODS_NM_1 AS GOODS_NM,                                                                               " & vbNewLine _
    '                                      & " GOODS.SEARCH_KEY_1 AS SEARCH_KEY_1,                                                                         " & vbNewLine _
    '                                      & " ZAITRS.INKO_DATE AS INKO_DATE,                                                                              " & vbNewLine _
    '                                      & " ZAITRS.LOT_NO AS LOT_NO,                                                                                    " & vbNewLine _
    '                                      & " ZAITRS.IRIME AS IRIME,                                                                                      " & vbNewLine _
    '                                      & " GOODS.STD_IRIME_UT AS IRIME_UT,                                                                             " & vbNewLine _
    '                                      & " ZAITRS.ALLOC_CAN_NB AS ZANKOSU,                                                                             " & vbNewLine _
    '                                      & " GOODS.NB_UT AS NB_UT,                                                                                       " & vbNewLine _
    '                                      & " ZAITRS.ALLOC_CAN_QT AS ZANSURYO,                                                                            " & vbNewLine _
    '                                      & " ZAITRS.ALCTD_QT AS ALCTD_QT,                                                                                " & vbNewLine _
    '                                      & " ZAITRS.PORA_ZAI_QT AS PORA_ZAI_QT,                                                                          " & vbNewLine _
    '                                      & " GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                         " & vbNewLine _
    '                                      & " ZAITRS.ALCTD_NB AS ALCTD_NB,                                                                                " & vbNewLine _
    '                                      & " ZAITRS.REMARK AS REMARK,                                                                                    " & vbNewLine _
    '                                      & " ZAITRS.REMARK_OUT AS REMARK_OUT,                                                                            " & vbNewLine _
    '                                      & " ZAITRS.LT_DATE AS LT_DATE,                                                                                  " & vbNewLine _
    '                                      & " ZAITRS.SERIAL_NO AS SERIAL_NO,                                                                              " & vbNewLine _
    '                                      & " ZAITRS.GOODS_COND_KB_1 AS GOODS_COND_KB_1,                                                                  " & vbNewLine _
    '                                      & " ZAITRS.GOODS_COND_KB_2 AS GOODS_COND_KB_2,                                                                  " & vbNewLine _
    '                                      & " ZAITRS.GOODS_COND_KB_3 AS GOODS_COND_KB_3,                                                                  " & vbNewLine _
    '                                      & " KBN1.KBN_NM1 AS GOODS_COND_NM_1,                                                                            " & vbNewLine _
    '                                      & " KBN2.KBN_NM1 AS GOODS_COND_NM_2,                                                                            " & vbNewLine _
    '                                      & " CUSTCOND.JOTAI_NM AS GOODS_COND_NM_3,                                                                       " & vbNewLine _
    '                                      & " GOODS.PKG_NB AS PKG_NB,                                                                                     " & vbNewLine _
    '                                      & " GOODS.PKG_UT AS PKG_UT,                                                                                     " & vbNewLine _
    '                                      & " ZAITRS.OFB_KB AS OFB_KB,                                                                                    " & vbNewLine _
    '                                      & " ZAITRS.SPD_KB AS SPD_KB,                                                                                    " & vbNewLine _
    '                                      & " KBN3.KBN_NM1 AS SPD_KB_NM,                                                                                  " & vbNewLine _
    '                                      & " KBN4.KBN_NM1 AS OFB_KB_NM,                                                                                  " & vbNewLine _
    '                                      & " GOODS.CUST_COST_CD1 AS CUST_COST_CD1,                                                                       " & vbNewLine _
    '                                      & " GOODS.CUST_COST_CD2 AS CUST_COST_CD2,                                                                       " & vbNewLine _
    '                                      & " GOODS.SEARCH_KEY_2 AS SEARCH_KEY_2,                                                                         " & vbNewLine _
    '                                      & " CUST.CUST_NM_L + '-' + CUST.CUST_NM_M AS CUST_NM,                                                           " & vbNewLine _
    '                                      & " ZAITRS.CUST_CD_L + '-' + ZAITRS.CUST_CD_M + '-' + GOODS.CUST_CD_S + '-' + GOODS.CUST_CD_SS AS CUST_CD,      " & vbNewLine _
    '                                      & " ZAITRS.INKA_NO_L + '-' + ZAITRS.INKA_NO_M + '-' + ZAITRS.INKA_NO_S AS INKA_NO,                              " & vbNewLine _
    '                                      & " ZAITRS.INKA_NO_L AS INKA_NO_L,                                                                              " & vbNewLine _
    '                                      & " ZAITRS.INKA_NO_M AS INKA_NO_M,                                                                              " & vbNewLine _
    '                                      & " ZAITRS.INKA_NO_S AS INKA_NO_S,                                                                              " & vbNewLine _
    '                                      & " ZAITRS.GOODS_CD_NRS AS GOODS_CD_NRS,                                                                        " & vbNewLine _
    '                                      & " ZAITRS.ZAI_REC_NO AS ZAI_REC_NO,                                                                            " & vbNewLine _
    '                                      & " ZAITRS.ALLOC_PRIORITY AS ALLOC_PRIORITY,                                                                    " & vbNewLine _
    '                                      & " KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                                                          " & vbNewLine _
    '                                      & " ZAITRS.DEST_CD_P AS DEST_CD_P,                                                                              " & vbNewLine _
    '                                      & " DEST.DEST_NM AS DEST_CD_NM,                                                                                 " & vbNewLine _
    '                                      & " GOODS.SHOBO_CD AS SHOBO_CD,                                                                                 " & vbNewLine _
    '                                      & " ZAITRS.TAX_KB AS TAX_KB,                                                                                    " & vbNewLine _
    '                                      & " KBN6.KBN_NM1 AS TAX_NM,                                                                                     " & vbNewLine _
    '                                      & " GOODS.DOKU_KB AS DOKU_KB,                                                                                   " & vbNewLine _
    '                                      & " KBN7.KBN_NM1 AS DOKU_NM,                                                                                    " & vbNewLine _
    '                                      & " GOODS.ONDO_KB AS ONDO_KB,                                                                                   " & vbNewLine _
    '                                      & " KBN8.KBN_NM1 AS ONDO_NM,                                                                                    " & vbNewLine _
    '                                      & " (CASE WHEN ZAITRS.ALLOC_CAN_NB = '1' AND ZAITRS.SMPL_FLAG = '01' AND                                        " & vbNewLine _
    '                                      & " SIGN(ZAITRS.ALLOC_CAN_QT) = 1 THEN ZAITRS.ALLOC_CAN_QT ELSE ZAITRS.IRIME END) AS INKA_IRIME,                " & vbNewLine _
    '                                      & " ZAITRS.NRS_BR_CD AS NRS_BR_CD,                                                                              " & vbNewLine _
    '                                      & " NRSBR.NRS_BR_NM AS NRS_BR_NM,                                                                               " & vbNewLine _
    '                                      & " ZAITRS.WH_CD AS WH_CD,                                                                                      " & vbNewLine _
    '                                      & " SOKO.WH_NM AS WH_NM,                                                                                        " & vbNewLine _
    '                                      & " (CASE WHEN FURIGOODS.CD_NRS_TO IS NULL THEN '' ELSE FURIGOODS.CD_NRS_TO END) AS CD_NRS_TO,                  " & vbNewLine _
    '                                      & " ZAITRS.CUST_CD_L AS CUST_CD_L,                                                                          " & vbNewLine _
    '                                      & " ZAITRS.CUST_CD_M AS CUST_CD_M                                                                           " & vbNewLine
    ''' <summary>
    ''' 在庫履歴照会トランザクションデータ抽出用（詳細）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ALL_SQL_SELECT_DATA As String = " SELECT                                                                                                " & vbNewLine _
                                              & "-- (CASE WHEN RTRIM(LTRIM(ZAITRS.INKO_DATE)) = '' THEN '予' ELSE '' END)  AS  YOJITU,                                 " & vbNewLine _
                                              & " (CASE WHEN RTRIM(LTRIM(ZAITRS.INKO_DATE)) = '' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K026' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') ELSE '' END)  AS  YOJITU,                                 " & vbNewLine _
                                          & "   ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD + (CASE WHEN ZAITRS.LOCA = '' THEN ''" _
                                          & " ELSE '-' +  ZAITRS.LOCA END) AS OKIBA,                                                                          " & vbNewLine _
                                          & " GOODS.GOODS_CD_CUST AS GOODS_CD_CUST,                                                                       " & vbNewLine _
                                          & " GOODS.GOODS_NM_1 AS GOODS_NM,                                                                               " & vbNewLine _
                                          & " GOODS.SEARCH_KEY_1 AS SEARCH_KEY_1,                                                                         " & vbNewLine _
                                          & " ZAITRS.INKO_DATE AS INKO_DATE,                                                                              " & vbNewLine _
                                          & " ZAITRS.LOT_NO AS LOT_NO,                                                                                    " & vbNewLine _
                                          & " ZAITRS.IRIME AS IRIME,                                                                                      " & vbNewLine _
                                          & " GOODS.STD_IRIME_UT AS IRIME_UT,                                                                             " & vbNewLine _
                                          & " ZAITRS.ALLOC_CAN_NB AS ZANKOSU,                                                                             " & vbNewLine _
                                          & " GOODS.NB_UT AS NB_UT,                                                                                       " & vbNewLine _
                                          & " ZAITRS.ALLOC_CAN_QT AS ZANSURYO,                                                                            " & vbNewLine _
                                          & " ZAITRS.ALCTD_QT AS ALCTD_QT,                                                                                " & vbNewLine _
                                          & " ZAITRS.PORA_ZAI_QT AS PORA_ZAI_QT,                                                                          " & vbNewLine _
                                          & " GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                         " & vbNewLine _
                                          & " ZAITRS.ALCTD_NB AS ALCTD_NB,                                                                                " & vbNewLine _
                                          & " ZAITRS.REMARK AS REMARK,                                                                                    " & vbNewLine _
                                          & " ZAITRS.REMARK_OUT AS REMARK_OUT,                                                                            " & vbNewLine _
                                          & " ZAITRS.LT_DATE AS LT_DATE,                                                                                  " & vbNewLine _
                                          & " ZAITRS.SERIAL_NO AS SERIAL_NO,                                                                              " & vbNewLine _
                                          & " ZAITRS.GOODS_COND_KB_1 AS GOODS_COND_KB_1,                                                                  " & vbNewLine _
                                          & " ZAITRS.GOODS_COND_KB_2 AS GOODS_COND_KB_2,                                                                  " & vbNewLine _
                                          & " ZAITRS.GOODS_COND_KB_3 AS GOODS_COND_KB_3,                                                                  " & vbNewLine _
                                          & " KBN1.KBN_NM1 AS GOODS_COND_NM_1,                                                                            " & vbNewLine _
                                          & " KBN2.KBN_NM1 AS GOODS_COND_NM_2,                                                                            " & vbNewLine _
                                          & " CUSTCOND.JOTAI_NM AS GOODS_COND_NM_3,                                                                       " & vbNewLine _
                                          & " ISNULL(GOODS.PKG_NB,0) AS PKG_NB,                                                                           " & vbNewLine _
                                          & " GOODS.PKG_UT AS PKG_UT,                                                                                     " & vbNewLine _
                                          & " ZAITRS.OFB_KB AS OFB_KB,                                                                                    " & vbNewLine _
                                          & " ZAITRS.SPD_KB AS SPD_KB,                                                                                    " & vbNewLine _
                                          & " KBN3.KBN_NM1 AS SPD_KB_NM,                                                                                  " & vbNewLine _
                                          & " KBN4.KBN_NM1 AS OFB_KB_NM,                                                                                  " & vbNewLine _
                                          & " GOODS.CUST_COST_CD1 AS CUST_COST_CD1,                                                                       " & vbNewLine _
                                          & " GOODS.CUST_COST_CD2 AS CUST_COST_CD2,                                                                       " & vbNewLine _
                                          & " GOODS.SEARCH_KEY_2 AS SEARCH_KEY_2,                                                                         " & vbNewLine _
                                          & " CUST.CUST_NM_L + '-' + CUST.CUST_NM_M AS CUST_NM,                                                           " & vbNewLine _
                                          & " ZAITRS.CUST_CD_L + '-' + ZAITRS.CUST_CD_M + '-' + GOODS.CUST_CD_S + '-' + GOODS.CUST_CD_SS AS CUST_CD,      " & vbNewLine _
                                          & " ZAITRS.INKA_NO_L + '-' + ZAITRS.INKA_NO_M + '-' + ZAITRS.INKA_NO_S AS INKA_NO,                              " & vbNewLine _
                                          & " ZAITRS.INKA_NO_L AS INKA_NO_L,                                                                              " & vbNewLine _
                                          & " ZAITRS.INKA_NO_M AS INKA_NO_M,                                                                              " & vbNewLine _
                                          & " ZAITRS.INKA_NO_S AS INKA_NO_S,                                                                              " & vbNewLine _
                                          & " ZAITRS.GOODS_CD_NRS AS GOODS_CD_NRS,                                                                        " & vbNewLine _
                                          & " ZAITRS.ZAI_REC_NO AS ZAI_REC_NO,                                                                            " & vbNewLine _
                                          & " ZAITRS.ALLOC_PRIORITY AS ALLOC_PRIORITY,                                                                    " & vbNewLine _
                                          & " KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                                                          " & vbNewLine _
                                          & " ZAITRS.DEST_CD_P AS DEST_CD_P,                                                                              " & vbNewLine _
                                          & " DEST.DEST_NM AS DEST_CD_NM,                                                                                 " & vbNewLine _
                                          & " GOODS.SHOBO_CD AS SHOBO_CD,                                                                                 " & vbNewLine _
                                          & " ZAITRS.TAX_KB AS TAX_KB,                                                                                    " & vbNewLine _
                                          & " KBN6.KBN_NM1 AS TAX_NM,                                                                                     " & vbNewLine _
                                          & " GOODS.DOKU_KB AS DOKU_KB,                                                                                   " & vbNewLine _
                                          & " KBN7.KBN_NM1 AS DOKU_NM,                                                                                    " & vbNewLine _
                                          & " GOODS.ONDO_KB AS ONDO_KB,                                                                                   " & vbNewLine _
                                          & " KBN8.KBN_NM1 AS ONDO_NM,                                                                                    " & vbNewLine _
                                          & " (CASE WHEN ZAITRS.ALLOC_CAN_NB = '1' AND ZAITRS.SMPL_FLAG = '01' AND                                        " & vbNewLine _
                                          & " SIGN(ZAITRS.ALLOC_CAN_QT) = 1 THEN ZAITRS.ALLOC_CAN_QT ELSE ZAITRS.IRIME END) AS INKA_IRIME,                " & vbNewLine _
                                          & " ZAITRS.NRS_BR_CD AS NRS_BR_CD,                                                                              " & vbNewLine _
                                          & " NRSBR.NRS_BR_NM AS NRS_BR_NM,                                                                               " & vbNewLine _
                                          & " ZAITRS.WH_CD AS WH_CD,                                                                                      " & vbNewLine _
                                          & " SOKO.WH_NM AS WH_NM,                                                                                        " & vbNewLine _
                                          & " '' AS CD_NRS_TO,                                                                                            " & vbNewLine _
                                          & " ZAITRS.CUST_CD_L AS CUST_CD_L,                                                                              " & vbNewLine _
                                          & " ZAITRS.CUST_CD_M AS CUST_CD_M,                                                                              " & vbNewLine _
                                          & " KBN9.KBN_NM1 + ' ' + ISNULL(SHOBO.HINMEI,'') AS SHOBO_NM                                                    " & vbNewLine

#End Region

    'ADD START 2019/8/27 依頼番号:007116,007119
#Region "空棚"

    Private Const SQL_SELECT_EMPTY_RACK As String = _
          "SELECT                                             " & vbNewLine _
        & "    M_ZONE.TOU_NO,                                 " & vbNewLine _
        & "    M_ZONE.SITU_NO,                                " & vbNewLine _
        & "    M_ZONE.ZONE_CD,                                " & vbNewLine _
        & "    M_TOU_SITU.DOKU_KB,                            " & vbNewLine _
        & "    SUM(                                           " & vbNewLine _
        & "        CASE WHEN    D_ZAI_TRS.PORA_ZAI_NB IS NULL " & vbNewLine _
        & "                  OR D_ZAI_TRS.SYS_DEL_FLG = '1'   " & vbNewLine _
        & "            THEN 0                                 " & vbNewLine _
        & "            ELSE D_ZAI_TRS.PORA_ZAI_NB             " & vbNewLine _
        & "        END                                        " & vbNewLine _
        & "    ) AS ZAI_NB                                    " & vbNewLine _
        & "FROM                                               " & vbNewLine _
        & "    $LM_MST$..M_ZONE                               " & vbNewLine _
        & "INNER JOIN                                         " & vbNewLine _
        & "    $LM_MST$..M_TOU_SITU                           " & vbNewLine _
        & "    ON  M_ZONE.NRS_BR_CD = M_TOU_SITU.NRS_BR_CD    " & vbNewLine _
        & "    AND M_ZONE.WH_CD     = M_TOU_SITU.WH_CD        " & vbNewLine _
        & "    AND M_ZONE.TOU_NO    = M_TOU_SITU.TOU_NO       " & vbNewLine _
        & "    AND M_ZONE.SITU_NO   = M_TOU_SITU.SITU_NO      " & vbNewLine _
        & "LEFT JOIN                                          " & vbNewLine _
        & "    $LM_TRN$..D_ZAI_TRS                            " & vbNewLine _
        & "    ON  M_ZONE.NRS_BR_CD = D_ZAI_TRS.NRS_BR_CD     " & vbNewLine _
        & "    AND M_ZONE.WH_CD     = D_ZAI_TRS.WH_CD         " & vbNewLine _
        & "    AND M_ZONE.TOU_NO    = D_ZAI_TRS.TOU_NO        " & vbNewLine _
        & "    AND M_ZONE.SITU_NO   = D_ZAI_TRS.SITU_NO       " & vbNewLine _
        & "    AND M_ZONE.ZONE_CD   = D_ZAI_TRS.ZONE_CD       " & vbNewLine _
        & "WHERE                                              " & vbNewLine _
        & "        M_ZONE.NRS_BR_CD      = @NRS_BR_CD         " & vbNewLine _
        & "    AND M_ZONE.WH_CD          = @WH_CD             " & vbNewLine _
        & "    AND M_ZONE.SYS_DEL_FLG    = '0'                " & vbNewLine _
        & "GROUP BY                                           " & vbNewLine _
        & "    M_ZONE.TOU_NO,                                 " & vbNewLine _
        & "    M_ZONE.SITU_NO,                                " & vbNewLine _
        & "    M_ZONE.ZONE_CD,                                " & vbNewLine _
        & "    M_TOU_SITU.DOKU_KB                             " & vbNewLine _
        & "ORDER BY                                           " & vbNewLine _
        & "    M_ZONE.TOU_NO,                                 " & vbNewLine _
        & "    M_ZONE.SITU_NO,                                " & vbNewLine _
        & "    M_ZONE.ZONE_CD                                 " & vbNewLine

#End Region

#Region "在庫差異"

    Private Const SQL_SELECT_ZAIKO_DIFF As String = _
          "SELECT                                                                 " & vbNewLine _
        & "    IsNull(H_ZAIKO_FJF.MATNR, ZAI.GOODS_CD_CUST)    AS MATNR,          " & vbNewLine _
        & "    IsNull(H_ZAIKO_FJF.CHARG, ZAI.LOT_NO)           AS CHARG,          " & vbNewLine _
        & "    IsNull(H_ZAIKO_FJF.HINMOKU_TXT, ZAI.GOODS_NM_1) AS HINMOKU_TXT,    " & vbNewLine _
        & "    IsNull(H_ZAIKO_FJF.PRODUCT_NB, 0)               AS SAP_PRODUCT_NB, " & vbNewLine _
        & "    IsNull(H_ZAIKO_FJF.INSPECT_NB, 0)               AS SAP_INSPECT_NB, " & vbNewLine _
        & "    IsNull(H_ZAIKO_FJF.DEFECT_NB, 0)                AS SAP_DEFECT_NB,  " & vbNewLine _
        & "    IsNull(ZAI.LMS_PRODUCT_QT, 0)                   AS LMS_PRODUCT_QT, " & vbNewLine _
        & "    IsNull(ZAI.LMS_INSPECT_QT, 0)                   AS LMS_INSPECT_QT, " & vbNewLine _
        & "    IsNull(ZAI.LMS_DEFECT_QT, 0)                    AS LMS_DEFECT_QT   " & vbNewLine _
        & "FROM                                                                   " & vbNewLine _
        & "    $LM_TRN$..H_ZAIKO_FJF                                              " & vbNewLine _
        & "FULL JOIN                                                              " & vbNewLine _
        & "    (SELECT                                                            " & vbNewLine _
        & "         M_GOODS.GOODS_CD_CUST,                                        " & vbNewLine _
        & "         M_GOODS.GOODS_NM_1,                                           " & vbNewLine _
        & "         D_ZAI_TRS.LOT_NO,                                             " & vbNewLine _
        & "         SUM(                                                          " & vbNewLine _
        & "             CASE WHEN D_ZAI_TRS.GOODS_COND_KB_3 = '00'                         " & vbNewLine _
        & "                 THEN D_ZAI_TRS.PORA_ZAI_QT                            " & vbNewLine _
        & "                 ELSE 0                                                " & vbNewLine _
        & "             END                                                       " & vbNewLine _
        & "         ) AS LMS_PRODUCT_QT,                                          " & vbNewLine _
        & "         SUM(                                                          " & vbNewLine _
        & "             CASE WHEN D_ZAI_TRS.GOODS_COND_KB_3 = '01'                         " & vbNewLine _
        & "                 THEN D_ZAI_TRS.PORA_ZAI_QT                            " & vbNewLine _
        & "                 ELSE 0                                                " & vbNewLine _
        & "             END                                                       " & vbNewLine _
        & "         ) AS LMS_INSPECT_QT,                                          " & vbNewLine _
        & "         SUM(                                                          " & vbNewLine _
        & "             CASE WHEN D_ZAI_TRS.GOODS_COND_KB_3 = '02'                         " & vbNewLine _
        & "                 THEN D_ZAI_TRS.PORA_ZAI_QT                            " & vbNewLine _
        & "                 ELSE 0                                                " & vbNewLine _
        & "             END                                                       " & vbNewLine _
        & "         ) AS LMS_DEFECT_QT                                            " & vbNewLine _
        & "     FROM                                                              " & vbNewLine _
        & "         $LM_TRN$..D_ZAI_TRS                                           " & vbNewLine _
        & "     INNER JOIN                                                        " & vbNewLine _
        & "         $LM_MST$..M_GOODS                                             " & vbNewLine _
        & "         ON D_ZAI_TRS.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS              " & vbNewLine _
        & "     WHERE                                                             " & vbNewLine _
        & "             M_GOODS.NRS_BR_CD     = @NRS_BR_CD                        " & vbNewLine _
        & "         AND M_GOODS.CUST_CD_L     = @CUST_CD_L                        " & vbNewLine _
        & "         AND M_GOODS.CUST_CD_M     = @CUST_CD_M                        " & vbNewLine _
        & "         AND M_GOODS.CUST_CD_S     = @CUST_CD_S                        " & vbNewLine _
        & "         AND M_GOODS.CUST_CD_SS    = @CUST_CD_SS                       " & vbNewLine _
        & "         AND D_ZAI_TRS.SYS_DEL_FLG = '0'                               " & vbNewLine _
        & "     GROUP BY                                                          " & vbNewLine _
        & "         M_GOODS.GOODS_CD_CUST,                                        " & vbNewLine _
        & "         M_GOODS.GOODS_NM_1,                                           " & vbNewLine _
        & "         D_ZAI_TRS.LOT_NO                                              " & vbNewLine _
        & "    ) AS ZAI                                                           " & vbNewLine _
        & "    ON  H_ZAIKO_FJF.MATNR = ZAI.GOODS_CD_CUST                          " & vbNewLine _
        & "    AND H_ZAIKO_FJF.CHARG = ZAI.LOT_NO                                 " & vbNewLine _
        & "ORDER BY                                                               " & vbNewLine _
        & "    IsNull(H_ZAIKO_FJF.MATNR, ZAI.GOODS_CD_CUST)                       " & vbNewLine _
        & "    ,IsNull(H_ZAIKO_FJF.CHARG, ZAI.LOT_NO)                             " & vbNewLine

#End Region
    'ADD END 2019/8/27 依頼番号:007116,007119

#End Region

#End Region

#Region "WHERE句"


    'START YANAI 要望番号508
    '''' <summary>
    '''' 検索用SQLFROM句(在庫履歴照会トランザクション）
    '''' GOODS WHERE句用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM1 As String = "FROM                                                 " & vbNewLine _
    '                                   & "$LM_TRN$..D_ZAI_TRS AS ZAITRS                       " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..M_GOODS AS GOODS          " & vbNewLine _
    '                                   & "     ON ZAITRS.NRS_BR_CD = GOODS.NRS_BR_CD          " & vbNewLine _
    '                                   & "     AND ZAITRS.GOODS_CD_NRS = GOODS.GOODS_CD_NRS   " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..M_CUST AS CUST            " & vbNewLine _
    '                                   & "     ON ZAITRS.NRS_BR_CD = CUST.NRS_BR_CD           " & vbNewLine _
    '                                   & "     AND ZAITRS.CUST_CD_L = CUST.CUST_CD_L          " & vbNewLine _
    '                                   & "     AND ZAITRS.CUST_CD_M = CUST.CUST_CD_M          " & vbNewLine _
    '                                   & "     AND CUST.CUST_CD_S = '00'                      " & vbNewLine _
    '                                   & "     AND CUST.CUST_CD_SS = '00'                     " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..M_CUSTCOND AS CUSTCOND    " & vbNewLine _
    '                                   & "     ON ZAITRS.NRS_BR_CD = CUSTCOND.NRS_BR_CD       " & vbNewLine _
    '                                   & "     AND ZAITRS.CUST_CD_L = CUSTCOND.CUST_CD_L      " & vbNewLine _
    '                                   & "     AND ZAITRS.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST            " & vbNewLine _
    '                                   & "     ON ZAITRS.NRS_BR_CD = DEST.NRS_BR_CD           " & vbNewLine _
    '                                   & "     AND ZAITRS.CUST_CD_L = DEST.CUST_CD_L          " & vbNewLine _
    '                                   & "     AND ZAITRS.DEST_CD_P = DEST.DEST_CD            " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..M_SOKO AS SOKO            " & vbNewLine _
    '                                   & "     ON ZAITRS.WH_CD = SOKO.WH_CD                   " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..M_NRS_BR AS NRSBR         " & vbNewLine _
    '                                   & "     ON ZAITRS.NRS_BR_CD = NRSBR.NRS_BR_CD          " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..M_FURI_GOODS AS FURIGOODS " & vbNewLine _
    '                                   & "     ON ZAITRS.NRS_BR_CD = FURIGOODS.NRS_BR_CD      " & vbNewLine _
    '                                   & "     AND ZAITRS.GOODS_CD_NRS = FURIGOODS.CD_NRS     " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN1             " & vbNewLine _
    '                                   & "     ON ZAITRS.GOODS_COND_KB_1  = KBN1.KBN_CD       " & vbNewLine _
    '                                   & "     AND KBN1.KBN_GROUP_CD = 'S005'                 " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN2             " & vbNewLine _
    '                                   & "     ON ZAITRS.GOODS_COND_KB_2  = KBN2.KBN_CD       " & vbNewLine _
    '                                   & "     AND KBN2.KBN_GROUP_CD = 'S006'                 " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN3             " & vbNewLine _
    '                                   & "     ON ZAITRS.SPD_KB  = KBN3.KBN_CD                " & vbNewLine _
    '                                   & "     AND KBN3.KBN_GROUP_CD = 'H003'                 " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN4             " & vbNewLine _
    '                                   & "     ON ZAITRS.OFB_KB  = KBN4.KBN_CD                " & vbNewLine _
    '                                   & "     AND KBN4.KBN_GROUP_CD = 'B002'                 " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN5             " & vbNewLine _
    '                                   & "     ON ZAITRS.ALLOC_PRIORITY  = KBN5.KBN_CD        " & vbNewLine _
    '                                   & "     AND KBN5.KBN_GROUP_CD = 'W001'                 " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN6             " & vbNewLine _
    '                                   & "     ON ZAITRS.TAX_KB  = KBN6.KBN_CD                " & vbNewLine _
    '                                   & "     AND KBN6.KBN_GROUP_CD = 'Z001'                 " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN7             " & vbNewLine _
    '                                   & "     ON GOODS.DOKU_KB  = KBN7.KBN_CD                " & vbNewLine _
    '                                   & "     AND KBN7.KBN_GROUP_CD = 'G001'                 " & vbNewLine _
    '                                   & "LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN8             " & vbNewLine _
    '                                   & "     ON GOODS.ONDO_KB  = KBN8.KBN_CD                " & vbNewLine _
    '                                   & "     AND KBN8.KBN_GROUP_CD = 'O003'                 " & vbNewLine _
    '                                   & "LEFT OUTER JOIN                                     " & vbNewLine _
    '                                   & "     (SELECT                                        " & vbNewLine _
    '                                   & "         INKAL.NRS_BR_CD,                           " & vbNewLine _
    '                                   & "         INKAL.INKA_NO_L,                           " & vbNewLine _
    '                                   & "         INKAL.INKA_STATE_KB,                       " & vbNewLine _
    '                                   & "         INKAL.INKA_DATE                            " & vbNewLine _
    '                                   & "     FROM                                           " & vbNewLine _
    '                                   & "         $LM_TRN$..B_INKA_L AS INKAL                " & vbNewLine _
    '                                   & "     WHERE INKAL.SYS_DEL_FLG     = '0') INKAL       " & vbNewLine _
    '                                   & "     ON  ZAITRS.NRS_BR_CD = INKAL.NRS_BR_CD         " & vbNewLine _
    '                                   & "     AND ZAITRS.INKA_NO_L = INKAL.INKA_NO_L         " & vbNewLine _
    '                                   & "LEFT OUTER JOIN                                     " & vbNewLine _
    '                                   & "     (SELECT                                        " & vbNewLine _
    '                                   & "         INKAS.NRS_BR_CD,                           " & vbNewLine _
    '                                   & "         INKAS.INKA_NO_L,                           " & vbNewLine _
    '                                   & "         INKAS.INKA_NO_M,                           " & vbNewLine _
    '                                   & "         INKAS.INKA_NO_S,                           " & vbNewLine _
    '                                   & "         INKAS.KONSU,                               " & vbNewLine _
    '                                   & "         INKAS.ZAI_REC_NO,                          " & vbNewLine _
    '                                   & "         INKAS.HASU                                 " & vbNewLine _
    '                                   & "     FROM                                           " & vbNewLine _
    '                                   & "         $LM_TRN$..B_INKA_S AS INKAS                " & vbNewLine _
    '                                   & "     WHERE INKAS.SYS_DEL_FLG     = '0'              " & vbNewLine _
    '                                   & "     ) INKAS2                                       " & vbNewLine _
    '                                   & "     ON ZAITRS.NRS_BR_CD = INKAS2.NRS_BR_CD         " & vbNewLine _
    '                                   & "     AND ZAITRS.INKA_NO_L = INKAS2.INKA_NO_L        " & vbNewLine _
    '                                   & "     AND ZAITRS.INKA_NO_M = INKAS2.INKA_NO_M        " & vbNewLine _
    '                                   & "     AND ZAITRS.INKA_NO_S = INKAS2.INKA_NO_S        " & vbNewLine
    ''' <summary>
    ''' 検索用SQLFROM句(在庫履歴照会トランザクション）
    ''' GOODS WHERE句用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM1 As String = "FROM                                                 " & vbNewLine _
                                       & "$LM_TRN$..D_ZAI_TRS AS ZAITRS                       " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..M_GOODS AS GOODS          " & vbNewLine _
                                       & "     ON ZAITRS.NRS_BR_CD = GOODS.NRS_BR_CD          " & vbNewLine _
                                       & "     AND ZAITRS.GOODS_CD_NRS = GOODS.GOODS_CD_NRS   " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..M_CUST AS CUST            " & vbNewLine _
                                       & "     ON ZAITRS.NRS_BR_CD = CUST.NRS_BR_CD           " & vbNewLine _
                                       & "     AND ZAITRS.CUST_CD_L = CUST.CUST_CD_L          " & vbNewLine _
                                       & "     AND ZAITRS.CUST_CD_M = CUST.CUST_CD_M          " & vbNewLine _
                                       & "     AND CUST.CUST_CD_S = '00'                      " & vbNewLine _
                                       & "     AND CUST.CUST_CD_SS = '00'                     " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..M_CUSTCOND AS CUSTCOND    " & vbNewLine _
                                       & "     ON ZAITRS.NRS_BR_CD = CUSTCOND.NRS_BR_CD       " & vbNewLine _
                                       & "     AND ZAITRS.CUST_CD_L = CUSTCOND.CUST_CD_L      " & vbNewLine _
                                       & "     AND ZAITRS.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST            " & vbNewLine _
                                       & "     ON ZAITRS.NRS_BR_CD = DEST.NRS_BR_CD           " & vbNewLine _
                                       & "     AND ZAITRS.CUST_CD_L = DEST.CUST_CD_L          " & vbNewLine _
                                       & "     AND ZAITRS.DEST_CD_P = DEST.DEST_CD            " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..M_SOKO AS SOKO            " & vbNewLine _
                                       & "     ON ZAITRS.WH_CD = SOKO.WH_CD                   " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..M_NRS_BR AS NRSBR         " & vbNewLine _
                                       & "     ON ZAITRS.NRS_BR_CD = NRSBR.NRS_BR_CD          " & vbNewLine _
                                       & "LEFT OUTER JOIN $LM_MST$..M_SHOBO AS SHOBO          " & vbNewLine _
                                       & "     ON SHOBO.SHOBO_CD = GOODS.SHOBO_CD             " & vbNewLine _
                                       & "--LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN1             " & vbNewLine _
                                       & "--     ON ZAITRS.GOODS_COND_KB_1  = KBN1.KBN_CD       " & vbNewLine _
                                       & "--     AND KBN1.KBN_GROUP_CD = 'S005'                 " & vbNewLine _
                                       & "--LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN2             " & vbNewLine _
                                       & "--     ON ZAITRS.GOODS_COND_KB_2  = KBN2.KBN_CD       " & vbNewLine _
                                       & "--     AND KBN2.KBN_GROUP_CD = 'S006'                 " & vbNewLine _
                                       & "--LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN3             " & vbNewLine _
                                       & "--     ON ZAITRS.SPD_KB  = KBN3.KBN_CD                " & vbNewLine _
                                       & "--     AND KBN3.KBN_GROUP_CD = 'H003'                 " & vbNewLine _
                                       & "--LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN4             " & vbNewLine _
                                       & "--     ON ZAITRS.OFB_KB  = KBN4.KBN_CD                " & vbNewLine _
                                       & "--     AND KBN4.KBN_GROUP_CD = 'B002'                 " & vbNewLine _
                                       & "--LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN5             " & vbNewLine _
                                       & "--     ON ZAITRS.ALLOC_PRIORITY  = KBN5.KBN_CD        " & vbNewLine _
                                       & "--     AND KBN5.KBN_GROUP_CD = 'W001'                 " & vbNewLine _
                                       & "--LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN6             " & vbNewLine _
                                       & "--     ON ZAITRS.TAX_KB  = KBN6.KBN_CD                " & vbNewLine _
                                       & "--     AND KBN6.KBN_GROUP_CD = 'Z001'                 " & vbNewLine _
                                       & "--LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN7             " & vbNewLine _
                                       & "--     ON GOODS.DOKU_KB  = KBN7.KBN_CD                " & vbNewLine _
                                       & "--     AND KBN7.KBN_GROUP_CD = 'G001'                 " & vbNewLine _
                                       & "--LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN8             " & vbNewLine _
                                       & "--     ON GOODS.ONDO_KB  = KBN8.KBN_CD                " & vbNewLine _
                                       & "----(2012.06.19)要望番号1173 --- START ---            " & vbNewLine _
                                       & "--   --AND KBN8.KBN_GROUP_CD = 'O003'                 " & vbNewLine _
                                       & "--     AND KBN8.KBN_GROUP_CD = 'O002'                 " & vbNewLine _
                                       & "----(2012.06.19)要望番号1173 ---  END  ---            " & vbNewLine _
                                       & "----(2016.02.05)修正START                             " & vbNewLine _
                                       & "LEFT OUTER JOIN                                     " & vbNewLine _
                                       & "   (SELECT                                          " & vbNewLine _
                                       & "      " & " #KBN# " & "     AS KBN_NM1              " & vbNewLine _
                                       & "      ,KBN1.KBN_CD                                  " & vbNewLine _
                                       & "   FROM $LM_MST$..Z_KBN AS KBN1                     " & vbNewLine _
                                       & "   WHERE KBN1.KBN_GROUP_CD = 'S005') KBN1           " & vbNewLine _
                                       & "  ON ZAITRS.GOODS_COND_KB_1 = KBN1.KBN_CD           " & vbNewLine _
                                       & "LEFT OUTER JOIN                                     " & vbNewLine _
                                       & "   (SELECT                                          " & vbNewLine _
                                       & "      " & " #KBN# " & "     AS KBN_NM1              " & vbNewLine _
                                       & "      ,KBN1.KBN_CD                                  " & vbNewLine _
                                       & "   FROM $LM_MST$..Z_KBN AS KBN1                     " & vbNewLine _
                                       & "   WHERE KBN1.KBN_GROUP_CD = 'S006') KBN2           " & vbNewLine _
                                       & "  ON ZAITRS.GOODS_COND_KB_2 = KBN2.KBN_CD           " & vbNewLine _
                                       & "LEFT OUTER JOIN                                     " & vbNewLine _
                                       & "   (SELECT                                          " & vbNewLine _
                                       & "      " & " #KBN# " & "     AS KBN_NM1              " & vbNewLine _
                                       & "      ,KBN1.KBN_CD                                  " & vbNewLine _
                                       & "   FROM $LM_MST$..Z_KBN AS KBN1                     " & vbNewLine _
                                       & "   WHERE KBN1.KBN_GROUP_CD = 'H003') KBN3           " & vbNewLine _
                                       & "  ON ZAITRS.SPD_KB = KBN3.KBN_CD                    " & vbNewLine _
                                       & "LEFT OUTER JOIN                                     " & vbNewLine _
                                       & "   (SELECT                                          " & vbNewLine _
                                       & "      " & " #KBN# " & "     AS KBN_NM1              " & vbNewLine _
                                       & "      ,KBN1.KBN_CD                                  " & vbNewLine _
                                       & "   FROM $LM_MST$..Z_KBN AS KBN1                     " & vbNewLine _
                                       & "   WHERE KBN1.KBN_GROUP_CD = 'B002') KBN4           " & vbNewLine _
                                       & "  ON ZAITRS.OFB_KB = KBN4.KBN_CD                    " & vbNewLine _
                                       & "LEFT OUTER JOIN                                     " & vbNewLine _
                                       & "   (SELECT                                          " & vbNewLine _
                                       & "      " & " #KBN# " & "     AS KBN_NM1              " & vbNewLine _
                                       & "      ,KBN1.KBN_CD                                  " & vbNewLine _
                                       & "   FROM $LM_MST$..Z_KBN AS KBN1                     " & vbNewLine _
                                       & "   WHERE KBN1.KBN_GROUP_CD = 'W001') KBN5           " & vbNewLine _
                                       & "  ON ZAITRS.ALLOC_PRIORITY = KBN5.KBN_CD            " & vbNewLine _
                                       & "LEFT OUTER JOIN                                     " & vbNewLine _
                                       & "   (SELECT                                          " & vbNewLine _
                                       & "      " & " #KBN# " & "     AS KBN_NM1              " & vbNewLine _
                                       & "      ,KBN1.KBN_CD                                  " & vbNewLine _
                                       & "   FROM $LM_MST$..Z_KBN AS KBN1                     " & vbNewLine _
                                       & "   WHERE KBN1.KBN_GROUP_CD = 'Z001') KBN6           " & vbNewLine _
                                       & "  ON ZAITRS.TAX_KB = KBN6.KBN_CD                    " & vbNewLine _
                                       & "LEFT OUTER JOIN                                     " & vbNewLine _
                                       & "   (SELECT                                          " & vbNewLine _
                                       & "      " & " #KBN# " & "     AS KBN_NM1              " & vbNewLine _
                                       & "      ,KBN1.KBN_CD                                  " & vbNewLine _
                                       & "   FROM $LM_MST$..Z_KBN AS KBN1                     " & vbNewLine _
                                       & "   WHERE KBN1.KBN_GROUP_CD = 'G001') KBN7           " & vbNewLine _
                                       & "  ON GOODS.DOKU_KB = KBN7.KBN_CD                    " & vbNewLine _
                                       & "LEFT OUTER JOIN                                     " & vbNewLine _
                                       & "   (SELECT                                          " & vbNewLine _
                                       & "      " & " #KBN# " & "     AS KBN_NM1              " & vbNewLine _
                                       & "      ,KBN1.KBN_CD                                  " & vbNewLine _
                                       & "   FROM $LM_MST$..Z_KBN AS KBN1                     " & vbNewLine _
                                       & "   WHERE KBN1.KBN_GROUP_CD = 'O002') KBN8           " & vbNewLine _
                                       & "  ON GOODS.ONDO_KB = KBN8.KBN_CD                    " & vbNewLine _
                                       & "----(2016.02.05)修正END                             " & vbNewLine _
                                       & "LEFT OUTER JOIN                                     " & vbNewLine _
                                       & "   (SELECT                                          " & vbNewLine _
                                       & "      " & " #KBN# " & "     AS KBN_NM1              " & vbNewLine _
                                       & "      ,KBN1.KBN_CD                                  " & vbNewLine _
                                       & "   FROM $LM_MST$..Z_KBN AS KBN1                     " & vbNewLine _
                                       & "   WHERE KBN1.KBN_GROUP_CD = 'S004') KBN9           " & vbNewLine _
                                       & "  ON SHOBO.RUI = KBN9.KBN_CD                        " & vbNewLine _
                                       & "LEFT OUTER JOIN                                     " & vbNewLine _
                                       & "     (SELECT                                        " & vbNewLine _
                                       & "         INKAL.NRS_BR_CD,                           " & vbNewLine _
                                       & "         INKAL.INKA_NO_L,                           " & vbNewLine _
                                       & "         INKAL.INKA_STATE_KB,                       " & vbNewLine _
                                       & "         INKAL.INKA_DATE                            " & vbNewLine _
                                       & "     FROM                                           " & vbNewLine _
                                       & "         $LM_TRN$..B_INKA_L AS INKAL                " & vbNewLine _
                                       & "     WHERE INKAL.SYS_DEL_FLG     = '0') INKAL       " & vbNewLine _
                                       & "     ON  ZAITRS.NRS_BR_CD = INKAL.NRS_BR_CD         " & vbNewLine _
                                       & "     AND ZAITRS.INKA_NO_L = INKAL.INKA_NO_L         " & vbNewLine _
                                       & "LEFT OUTER JOIN                                     " & vbNewLine _
                                       & "     (SELECT                                        " & vbNewLine _
                                       & "         INKAS.NRS_BR_CD,                           " & vbNewLine _
                                       & "         INKAS.INKA_NO_L,                           " & vbNewLine _
                                       & "         INKAS.INKA_NO_M,                           " & vbNewLine _
                                       & "         INKAS.INKA_NO_S,                           " & vbNewLine _
                                       & "         INKAS.KONSU,                               " & vbNewLine _
                                       & "         INKAS.ZAI_REC_NO,                          " & vbNewLine _
                                       & "         INKAS.HASU                                 " & vbNewLine _
                                       & "     FROM                                           " & vbNewLine _
                                       & "         $LM_TRN$..B_INKA_S AS INKAS                " & vbNewLine _
                                       & "     WHERE INKAS.SYS_DEL_FLG     = '0'              " & vbNewLine _
                                       & "     ) INKAS2                                       " & vbNewLine _
                                       & "     ON ZAITRS.NRS_BR_CD = INKAS2.NRS_BR_CD         " & vbNewLine _
                                       & "     AND ZAITRS.INKA_NO_L = INKAS2.INKA_NO_L        " & vbNewLine _
                                       & "     AND ZAITRS.INKA_NO_M = INKAS2.INKA_NO_M        " & vbNewLine _
                                       & "     AND ZAITRS.INKA_NO_S = INKAS2.INKA_NO_S        " & vbNewLine
    'END YANAI 要望番号508
    '& "LEFT OUTER JOIN                                     " & vbNewLine _
    '& "     (SELECT                                        " & vbNewLine _
    '& "         OUTKAS.NRS_BR_CD,                          " & vbNewLine _
    '& "         OUTKAS.INKA_NO_L,                          " & vbNewLine _
    '& "         OUTKAS.INKA_NO_M,                          " & vbNewLine _
    '& "         OUTKAS.INKA_NO_S,                          " & vbNewLine _
    '& "         OUTKAS.ZAI_REC_NO,                         " & vbNewLine _
    '& "         SUM(OUTKAS.ALCTD_NB) AS ALCTD_NB,          " & vbNewLine _
    '& "         SUM(OUTKAS.ALCTD_QT) AS ALCTD_QT           " & vbNewLine _
    '& "     FROM                                           " & vbNewLine _
    '& "         $LM_TRN$..C_OUTKA_S AS OUTKAS              " & vbNewLine _
    '& "     WHERE OUTKAS.SYS_DEL_FLG     = '0'             " & vbNewLine _
    '& "     GROUP BY OUTKAS.NRS_BR_CD,                     " & vbNewLine _
    '& "     OUTKAS.INKA_NO_L,                              " & vbNewLine _
    '& "     OUTKAS.INKA_NO_M,                              " & vbNewLine _
    '& "     OUTKAS.INKA_NO_S,                              " & vbNewLine _
    '& "     OUTKAS.ZAI_REC_NO                              " & vbNewLine _
    '& "     ) OUTKAS2                                      " & vbNewLine _
    '& "     ON ZAITRS.NRS_BR_CD = OUTKAS2.NRS_BR_CD        " & vbNewLine _
    '& "     AND ZAITRS.INKA_NO_L = OUTKAS2.INKA_NO_L       " & vbNewLine _
    '& "     AND ZAITRS.INKA_NO_M = OUTKAS2.INKA_NO_M       " & vbNewLine _
    '& "     AND ZAITRS.INKA_NO_S = OUTKAS2.INKA_NO_S       " & vbNewLine _
    '& "     AND ZAITRS.ZAI_REC_NO = OUTKAS2.ZAI_REC_NO     " & vbNewLine

#End Region

#Region "ORDERBY句"

    ''' <summary>
    ''' ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY GOODS.GOODS_NM_1, ZAITRS.LOT_NO, ZAITRS.IRIME "

    ''' <summary>
    ''' ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GOODS_SQL_ORDER_BY As String = " ORDER BY GOODS.GOODS_NM_1 "

    ''' <summary>
    ''' ORDER BY句(商品名のみ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_PTN1 As String = " ORDER BY MAIN.GOODS_NM "

    ''' <summary>
    ''' ORDER BY句(商品名 + ロット)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_PTN2 As String = " ORDER BY MAIN.GOODS_NM , MAIN.LOT_NO "

    ''' <summary>
    ''' ORDER BY句(商品名 + 入目)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_PTN3 As String = " ORDER BY MAIN.GOODS_NM , MAIN.IRIME "

    ''' <summary>
    ''' ORDER BY句(商品名 + ロット + 入目)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_PTN4 As String = " ORDER BY MAIN.GOODS_NM , MAIN.LOT_NO , MAIN.IRIME "

#End Region

#End Region

#Region "入出荷履歴（入荷）"


    ''' <summary>
    ''' 入出荷履歴（入荷ごと）タブ選択時セレクト文１
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_RIREKI1 As String = "SELECT                                                                                                          " & vbNewLine _
                                                        & " CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                " & vbNewLine _
                                                        & "      ELSE MR2.RPT_ID END                                                                    AS RPT_ID           " & vbNewLine _
                                                        & "   ,@NRS_BR_CD                                                                               AS NRS_BR_CD        " & vbNewLine _
                                                        & "   ,NRS_BR.NRS_BR_NM                                                                         AS NRS_BR_NM        " & vbNewLine _
                                                        & "   ,CUST.CUST_CD_L                                                                           AS CUST_CD_L        " & vbNewLine _
                                                        & "   ,CUST.CUST_NM_L                                                                           AS CUST_NM_L        " & vbNewLine _
                                                        & "   ,@WH_CD                                                                                   AS WH_CD            " & vbNewLine _
                                                        & "   ,SOKO.WH_NM                                                                               AS WH_NM            " & vbNewLine _
                                                        & "   ,GOODS.GOODS_CD_CUST                                                                      AS GOODS_CD_CUST    " & vbNewLine _
                                                        & "   ,GOODS.GOODS_NM_1                                                                         AS GOODS_NM         " & vbNewLine _
                                                        & "   ,GOODS.STD_IRIME_NB                                                                       AS IRIME            " & vbNewLine _
                                                        & "   ,@KAZU_KB                                                                                 AS KAZU_KB          " & vbNewLine _
                                                        & "   ,@GUI_IRIME                                                                               AS GUI_IRIME        " & vbNewLine _
                                                        & "   ,''                                                                                       AS YOJITU           " & vbNewLine _
                                                        & "--   ,'前残'                                                                                   AS SYUBETU          " & vbNewLine _
                                                        & "   ,(SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K027' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0')  AS SYUBETU          " & vbNewLine _
                                                        & "   ,CASE WHEN @MAEZAN_INKO_PLAN = '00000000' THEN ''                                                             " & vbNewLine _
                                                        & "         ELSE CONVERT(VARCHAR(8),DATEADD(DAY, - 1,CONVERT(DATETIME ,@MAEZAN_INKO_PLAN,101)),112)                 " & vbNewLine _
                                                        & "     END                                                                                     AS PLAN_DATE        " & vbNewLine _
                                                        & "   ,0                                                                                        AS INKA_KOSU        " & vbNewLine _
                                                        & "   ,0                                                                                        AS INKA_SURYO       " & vbNewLine _
                                                        & "   ,0                                                                                        AS OUTKA_KOSU       " & vbNewLine _
                                                        & "   ,0                                                                                        AS OUTKA_SURYO      " & vbNewLine _
                                                        & "   , INKAS.KONSU * ISNULL(GOODS.PKG_NB,0) + INKAS.HASU - ISNULL(OUTKAS.ALCTD_NB,0) AS ZAN_KOSU         " & vbNewLine _
                                                        & "   ,GOODS.NB_UT AS NB_UT                                                                                         " & vbNewLine _
                                                        & "   ,(INKAS.KONSU * ISNULL(GOODS.PKG_NB,0) + INKAS.HASU) * INKAS.IRIME - ISNULL(OUTKAS.ALCTD_QT,0) AS ZAN_SURYO        " & vbNewLine _
                                                        & "   ,GOODS.STD_IRIME_UT                                                                       AS STD_IRIME_UT     " & vbNewLine _
                                                        & "   ,''                                                                                       AS OKIBA            " & vbNewLine _
                                                        & "   ,''                                                                                       AS KANRI_NO         " & vbNewLine _
                                                        & "   ,''                                                                                       AS KANRI_NO_L       " & vbNewLine _
                                                        & "   ,''                                                                                       AS KANRI_NO_M       " & vbNewLine _
                                                        & "   ,''                                                                                       AS KANRI_NO_S       " & vbNewLine _
                                                        & "   ,''                                                                                       AS ZAI_REC_NO       " & vbNewLine _
                                                        & "   ,''                                                                                       AS DEST_NM          " & vbNewLine _
                                                        & "   ,''                                                                                       AS ORD_NO           " & vbNewLine _
                                                        & "   ,''                                                                                       AS BUYER_ORD_NO     " & vbNewLine _
                                                        & "   ,''                                                                                       AS UNSOCO_NM        " & vbNewLine _
                                                        & "   ,''                                                                                       AS REMARK           " & vbNewLine _
                                                        & "   ,''                                                                                       AS REMARK_OUT       " & vbNewLine _
                                                        & "   ,''                                                                                       AS GOODS_COND_NM_1  " & vbNewLine _
                                                        & "   ,''                                                                                       AS GOODS_COND_NM_2  " & vbNewLine _
                                                        & "   ,''                                                                                       AS GOODS_COND_NM_3  " & vbNewLine _
                                                        & "   ,''                                                                                       AS OFB_KB_NM        " & vbNewLine _
                                                        & "   ,''                                                                                       AS SPD_KB_NM        " & vbNewLine _
                                                        & "   ,''                                                                                       AS ALLOC_PRIORITY_NM" & vbNewLine _
                                                        & "   ,''                                                                                       AS DEST_CD_NM       " & vbNewLine _
                                                        & "   ,''                                                                                       AS RSV_NO           " & vbNewLine _
                                                        & "   ,'0'                                                                                      AS SORT_KEY         " & vbNewLine _
                                                        & "   ,GOODS.GOODS_NM_1                                                                         AS GOODS_NM         " & vbNewLine _
                                                        & "   ,INKAS.LOT_NO                                                                             AS LOT_NO           " & vbNewLine _
                                                        & "   ,GOODS.STD_IRIME_NB                                                                       AS IRIME            " & vbNewLine _
                                                        & " -- 1888_WIT_ロケーション変更強化対応                                                                            " & vbNewLine _
                                                        & "   ,''                                                                                    AS ZAI_TRS_UPD_USER_NM " & vbNewLine _
                                                        & " FROM      $LM_TRN$..B_INKA_L AS INKAL                        " & vbNewLine _
                                                        & "INNER JOIN $LM_TRN$..B_INKA_M AS INKAM                        " & vbNewLine _
                                                        & "   ON INKAL.NRS_BR_CD          = INKAM.NRS_BR_CD              " & vbNewLine _
                                                        & "  AND INKAL.INKA_NO_L          = INKAM.INKA_NO_L              " & vbNewLine _
                                                        & "INNER JOIN $LM_TRN$..B_INKA_S AS INKAS                        " & vbNewLine _
                                                        & "   ON INKAM.NRS_BR_CD          = INKAS.NRS_BR_CD              " & vbNewLine _
                                                        & "  AND INKAM.INKA_NO_L          = INKAS.INKA_NO_L              " & vbNewLine _
                                                        & "  AND INKAM.INKA_NO_M          = INKAS.INKA_NO_M              " & vbNewLine _
                                                        & "  AND INKAS.SYS_DEL_FLG        = '0'                          " & vbNewLine _
                                                        & "LEFT OUTER JOIN                                               " & vbNewLine _
                                                        & "   (SELECT                                                    " & vbNewLine _
                                                        & "      ISNULL(GOODS.PKG_NB,0) AS PKG_NB,                       " & vbNewLine _
                                                        & "      GOODS.STD_IRIME_NB,                                     " & vbNewLine _
                                                        & "      GOODS.STD_IRIME_UT,                                     " & vbNewLine _
                                                        & "      GOODS.NB_UT,                                            " & vbNewLine _
                                                        & "      GOODS.GOODS_NM_1,                                       " & vbNewLine _
                                                        & "      GOODS.NRS_BR_CD,                                        " & vbNewLine _
                                                        & "      GOODS.GOODS_CD_CUST,                                    " & vbNewLine _
                                                        & "      GOODS.CUST_CD_L,                                        " & vbNewLine _
                                                        & "      GOODS.CUST_CD_M,                                        " & vbNewLine _
                                                        & "      GOODS.CUST_CD_S,                                        " & vbNewLine _
                                                        & "      GOODS.CUST_CD_SS                                        " & vbNewLine _
                                                        & "   FROM $LM_MST$..M_GOODS AS GOODS                            " & vbNewLine _
                                                        & "   WHERE GOODS.GOODS_CD_NRS = @GOODS_CD_NRS                   " & vbNewLine _
                                                        & "     AND GOODS.NRS_BR_CD = @NRS_BR_CD)GOODS                   " & vbNewLine _
                                                        & "      ON INKAS.NRS_BR_CD = GOODS.NRS_BR_CD                    " & vbNewLine _
                                                        & "   --荷主M                                                    " & vbNewLine _
                                                        & "   LEFT JOIN                                                  " & vbNewLine _
                                                        & "     (SELECT                                                  " & vbNewLine _
                                                        & "        CUST.CUST_CD_L,                                       " & vbNewLine _
                                                        & "        CUST.CUST_CD_M,                                       " & vbNewLine _
                                                        & "        CUST.CUST_CD_S,                                       " & vbNewLine _
                                                        & "        CUST.CUST_CD_SS,                                      " & vbNewLine _
                                                        & "        CUST.CUST_NM_L,                                       " & vbNewLine _
                                                        & "        CUST.NRS_BR_CD                                        " & vbNewLine _
                                                        & "     FROM $LM_MST$..M_CUST AS CUST                            " & vbNewLine _
                                                        & "     WHERE CUST.NRS_BR_CD    = @NRS_BR_CD   ) CUST            " & vbNewLine _
                                                        & "   ON                                                         " & vbNewLine _
                                                        & "      CUST.NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
                                                        & "   AND                                                        " & vbNewLine _
                                                        & "       GOODS.CUST_CD_L  = CUST.CUST_CD_L                      " & vbNewLine _
                                                        & "   AND                                                        " & vbNewLine _
                                                        & "       GOODS.CUST_CD_M  = CUST.CUST_CD_M                      " & vbNewLine _
                                                        & "   AND                                                        " & vbNewLine _
                                                        & "       GOODS.CUST_CD_S  = CUST.CUST_CD_S                      " & vbNewLine _
                                                        & "   AND                                                        " & vbNewLine _
                                                        & "       GOODS.CUST_CD_SS = CUST.CUST_CD_SS                     " & vbNewLine _
                                                        & "   --営業所M                                                  " & vbNewLine _
                                                        & "   LEFT JOIN                                                  " & vbNewLine _
                                                        & "      $LM_MST$..M_NRS_BR AS NRS_BR                            " & vbNewLine _
                                                        & "   ON                                                         " & vbNewLine _
                                                        & "      NRS_BR.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                                        & "   --倉庫M                                                    " & vbNewLine _
                                                        & "   LEFT JOIN                                                  " & vbNewLine _
                                                        & "      $LM_MST$..M_SOKO AS SOKO                                  " & vbNewLine _
                                                        & "   ON                                                         " & vbNewLine _
                                                        & "      SOKO.NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
                                                        & "   AND                                                        " & vbNewLine _
                                                        & "      SOKO.WH_CD     = @WH_CD                                 " & vbNewLine _
                                                        & "   --商品コードでの荷主帳票パターン取得                       " & vbNewLine _
                                                        & "    LEFT JOIN                                                 " & vbNewLine _
                                                        & "     $LM_MST$..M_CUST_RPT MCR1                                  " & vbNewLine _
                                                        & "    ON                                                        " & vbNewLine _
                                                        & "     MCR1.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                                        & "    AND                                                       " & vbNewLine _
                                                        & "     MCR1.CUST_CD_L  = GOODS.CUST_CD_L                        " & vbNewLine _
                                                        & "    AND                                                       " & vbNewLine _
                                                        & "     MCR1.CUST_CD_M  = GOODS.CUST_CD_M                        " & vbNewLine _
                                                        & "    AND                                                       " & vbNewLine _
                                                        & "     MCR1.CUST_CD_S = GOODS.CUST_CD_S                         " & vbNewLine _
                                                        & "    AND                                                       " & vbNewLine _
                                                        & "     MCR1.PTN_ID    = @PTN_ID                                 " & vbNewLine _
                                                        & "    --帳票パターン取得                                        " & vbNewLine _
                                                        & "    LEFT JOIN                                                 " & vbNewLine _
                                                        & "     $LM_MST$..M_RPT MR1                                        " & vbNewLine _
                                                        & "    ON                                                        " & vbNewLine _
                                                        & "     MR1.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
                                                        & "    AND                                                       " & vbNewLine _
                                                        & "     MR1.PTN_ID    = MCR1.PTN_ID                              " & vbNewLine _
                                                        & "    AND                                                       " & vbNewLine _
                                                        & "     MR1.PTN_CD    = MCR1.PTN_CD                              " & vbNewLine _
                                                        & "    --存在しない場合の帳票パターン取得                        " & vbNewLine _
                                                        & "    LEFT JOIN                                                 " & vbNewLine _
                                                        & "     $LM_MST$..M_RPT MR2                                        " & vbNewLine _
                                                        & "    ON                                                        " & vbNewLine _
                                                        & "     MR2.NRS_BR_CD     = @NRS_BR_CD                           " & vbNewLine _
                                                        & "    AND                                                       " & vbNewLine _
                                                        & "     MR2.PTN_ID        = @PTN_ID                              " & vbNewLine _
                                                        & "    AND                                                       " & vbNewLine _
                                                        & "     MR2.STANDARD_FLAG = '01'                                 " & vbNewLine _
                                                        & "LEFT OUTER JOIN                                               " & vbNewLine _
                                                        & "(                                                             " & vbNewLine _
                                                        & "       SELECT                                                 " & vbNewLine _
                                                        & "            OUTKAS.NRS_BR_CD         AS NRS_BR_CDS            " & vbNewLine _
                                                        & "           ,SUM(OUTKAS.ALCTD_NB)     AS ALCTD_NB              " & vbNewLine _
                                                        & "           ,SUM(OUTKAS.ALCTD_QT)     AS ALCTD_QT              " & vbNewLine _
                                                        & "           ,OUTKAS.ZAI_REC_NO        AS ZAI_REC_NO            " & vbNewLine _
                                                        & "         FROM      $LM_TRN$..C_OUTKA_L AS OUTKAL              " & vbNewLine _
                                                        & "        INNER JOIN $LM_TRN$..C_OUTKA_M AS OUTKAM              " & vbNewLine _
                                                        & "           ON OUTKAL.NRS_BR_CD          = OUTKAM.NRS_BR_CD    " & vbNewLine _
                                                        & "          AND OUTKAL.OUTKA_NO_L         = OUTKAM.OUTKA_NO_L   " & vbNewLine _
                                                        & "          AND OUTKAM.SYS_DEL_FLG        = '0'                 " & vbNewLine _
                                                        & "        INNER JOIN $LM_TRN$..C_OUTKA_S AS OUTKAS              " & vbNewLine _
                                                        & "           ON OUTKAM.NRS_BR_CD          = OUTKAS.NRS_BR_CD    " & vbNewLine _
                                                        & "          AND OUTKAM.OUTKA_NO_L         = OUTKAS.OUTKA_NO_L   " & vbNewLine _
                                                        & "          AND OUTKAM.OUTKA_NO_M         = OUTKAS.OUTKA_NO_M   " & vbNewLine _
                                                        & "          AND OUTKAS.SYS_DEL_FLG        = '0'                 " & vbNewLine _
                                                        & "        WHERE OUTKAL.NRS_BR_CD          = @NRS_BR_CD          " & vbNewLine _
                                                        & "          AND OUTKAL.SYS_DEL_FLG        = '0'                 " & vbNewLine _
                                                        & "          AND OUTKAM.GOODS_CD_NRS       = @GOODS_CD_NRS       " & vbNewLine _
                                                        & "          AND OUTKAM.ALCTD_KB          <> '04'                " & vbNewLine _
                                                        & "          AND OUTKAL.OUTKA_PLAN_DATE    < @MAEZAN_INKO_PLAN   " & vbNewLine

    Private Const SQL_SELECT_DATA_RIREKI1_2 As String = "    ) OUTKAS                                                  " & vbNewLine _
                                                        & "   ON OUTKAS.NRS_BR_CDS    = INKAS.NRS_BR_CD                  " & vbNewLine _
                                                        & "  AND OUTKAS.ZAI_REC_NO    = INKAS.ZAI_REC_NO                 " & vbNewLine _
                                                        & "WHERE INKAS.NRS_BR_CD      = @NRS_BR_CD                       " & vbNewLine _
                                                        & "  AND INKAM.GOODS_CD_NRS   = @GOODS_CD_NRS                    " & vbNewLine

    Private Const SQL_SELECT_DATA_RIREKI1_3 As String = "  AND CASE WHEN RTRIM(INKAL.INKA_DATE) = '' THEN '999999999'  " & vbNewLine _
                                                        & "           ELSE RTRIM(INKAL.INKA_DATE) END < @MAEZAN_INKO_PLAN" & vbNewLine
    'END YANAI 要望番号508

    ''' <summary>
    ''' 入出荷履歴（入荷ごと）タブ選択時セレクト文３
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_RIREKI3 As String = "      AND ZAITRS.ZAI_REC_NO = INKAS.ZAI_REC_NO                                                     " & vbNewLine _
                                      & "      AND ZAITRS.NRS_BR_CD = INKAS.NRS_BR_CD                                                                     " & vbNewLine _
                                      & "      AND INKAS.KONSU <> 0                                                                                       " & vbNewLine _
                                      & "      AND INKAS.HASU <> 0                                                                                        " & vbNewLine _
                                      & "      AND INKAS.SYS_DEL_FLG = '0'                                                                                " & vbNewLine _
                                      & "      AND ZAITRS.SYS_DEL_FLG = '0'                                                                               " & vbNewLine



    'START YANAI 要望番号414
    '''' <summary>
    '''' 入出荷履歴（入荷ごと）タブ選択時セレクト文４
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_RIREKI4 As String = "UNION                                                                                              " & vbNewLine _
    '                                  & "SELECT                                                                                                           " & vbNewLine _
    '                                  & " CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                " & vbNewLine _
    '                                  & "      ELSE MR2.RPT_ID END                        AS RPT_ID,                                                      " & vbNewLine _
    '                                  & "   @NRS_BR_CD                                  AS NRS_BR_CD,                                                     " & vbNewLine _
    '                                  & "   NRS_BR.NRS_BR_NM                            AS NRS_BR_NM,                                                     " & vbNewLine _
    '                                  & "   CUST.CUST_CD_L                              AS CUST_CD_L,                                                     " & vbNewLine _
    '                                  & "   CUST.CUST_NM_L                              AS CUST_NM_L,                                                     " & vbNewLine _
    '                                  & "   @WH_CD                                      AS WH_CD,                                                         " & vbNewLine _
    '                                  & "   SOKO.WH_NM                                  AS WH_NM,                                                         " & vbNewLine _
    '                                  & "   GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST,                                                 " & vbNewLine _
    '                                  & "   GOODS.GOODS_NM_1                            AS GOODS_NM,                                                      " & vbNewLine _
    '                                  & "   GOODS.STD_IRIME_NB                          AS IRIME,                                                         " & vbNewLine _
    '                                  & "   @KAZU_KB                                    AS KAZU_KB,                                                       " & vbNewLine _
    '                                  & "   @GUI_IRIME                                  AS GUI_IRIME,                                                     " & vbNewLine _
    '                                  & "   (CASE WHEN INKAL.INKA_STATE_KB < '50' THEN '予' ELSE '' END) AS YOJITU,                                       " & vbNewLine _
    '                                  & "   CASE WHEN RTRIM(INKAL.FURI_NO) = '' THEN '入荷' ELSE '振入' END AS SYUBETU,                                                                                            " & vbNewLine _
    '                                  & "   ZAITRS.INKO_PLAN_DATE AS PLAN_DATE,                                                                           " & vbNewLine _
    '                                  & "   INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU AS INKA_KOSU,                                                         " & vbNewLine _
    '                                  & "   (INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU) * INKAS.IRIME AS INKA_SURYO,                                        " & vbNewLine _
    '                                  & "   0 AS OUTKA_KOSU,                                                                                              " & vbNewLine _
    '                                  & "   0 AS OUTKA_SURYO,                                " & vbNewLine _
    '                                  & "   0 AS ZAN_KOSU,                                                                                                " & vbNewLine _
    '                                  & "   GOODS.NB_UT AS NB_UT,                                                                                             " & vbNewLine _
    '                                  & "   0 AS ZAN_SURYO,                                                                                               " & vbNewLine _
    '                                  & "   GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                           " & vbNewLine _
    '                                  & "   ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD + (CASE WHEN ZAITRS.LOCA = '' THEN ''" _
    '                                  & " ELSE '-' +  ZAITRS.LOCA END) AS OKIBA,                                                                          " & vbNewLine _
    '                                  & "   ZAITRS.INKA_NO_L + '-' + ZAITRS.INKA_NO_M + '-' + ZAITRS.INKA_NO_S AS KANRI_NO,                               " & vbNewLine _
    '                                  & "   ZAITRS.INKA_NO_L  AS KANRI_NO_L,                                                                              " & vbNewLine _
    '                                  & "   ZAITRS.INKA_NO_M  AS KANRI_NO_M,                                                                              " & vbNewLine _
    '                                  & "   ZAITRS.INKA_NO_S  AS KANRI_NO_S,                                                                              " & vbNewLine _
    '                                  & "   INKAS.ZAI_REC_NO AS ZAI_REC_NO,                                                                               " & vbNewLine _
    '                                  & "   '' AS DEST_NM,                                                                                                " & vbNewLine _
    '                                  & "   CASE WHEN ISNULL(RTRIM(INKAM.OUTKA_FROM_ORD_NO_M), '') = '' THEN INKAL.OUTKA_FROM_ORD_NO_L ELSE INKAM.OUTKA_FROM_ORD_NO_M END  AS ORD_NO,                                        " & vbNewLine _
    '                                  & "   CASE WHEN ISNULL(RTRIM(INKAM.BUYER_ORD_NO_M), '')      = '' THEN INKAL.BUYER_ORD_NO_L      ELSE INKAM.BUYER_ORD_NO_M      END  AS BUYER_ORD_NO,                                            " & vbNewLine _
    '                                  & "   UNSOCO.UNSOCO_NM AS UNSOCO_NM,                                                                                " & vbNewLine _
    '                                  & "   INKAS.REMARK AS REMARK,                                                                                       " & vbNewLine _
    '                                  & "   INKAS.REMARK_OUT AS REMARK_OUT,                                                                               " & vbNewLine _
    '                                  & "   KBN1.KBN_NM1 AS GOODS_COND_NM_1,                                                                              " & vbNewLine _
    '                                  & "   KBN2.KBN_NM1 AS GOODS_COND_NM_2,                                                                              " & vbNewLine _
    '                                  & "   CUSTCOND.JOTAI_NM AS GOODS_COND_NM_3,                                                                         " & vbNewLine _
    '                                  & "   KBN3.KBN_NM1 AS OFB_KB_NM,                                                                                    " & vbNewLine _
    '                                  & "   KBN4.KBN_NM1 AS SPD_KB_NM,                                                                                    " & vbNewLine _
    '                                  & "   KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                                                            " & vbNewLine _
    '                                  & "   DEST.DEST_NM AS DEST_CD_NM,                                                                                   " & vbNewLine _
    '                                  & "   '' AS RSV_NO,                                                                                                 " & vbNewLine _
    '                                  & "   '1' AS SORT_KEY,                                                                                              " & vbNewLine _
    '                                  & "   GOODS.GOODS_NM_1   AS GOODS_NM,                                                                               " & vbNewLine _
    '                                  & "   ZAITRS.LOT_NO      AS LOT_NO,                                                                                 " & vbNewLine _
    '                                  & "   GOODS.STD_IRIME_NB AS IRIME                                                                                   " & vbNewLine _
    '                                  & "FROM  $LM_TRN$..B_INKA_S AS INKAS                                                                                " & vbNewLine _
    '                                  & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
    '                                  & "   (SELECT                                                                                                       " & vbNewLine _
    '                                  & "      ZAITRS.ZAI_REC_NO,                                                                                         " & vbNewLine _
    '                                  & "      ZAITRS.NRS_BR_CD,                                                                                          " & vbNewLine _
    '                                  & "      ZAITRS.INKO_PLAN_DATE,                                                                                     " & vbNewLine _
    '                                  & "      ZAITRS.TOU_NO,                                                                                             " & vbNewLine _
    '                                  & "      ZAITRS.SITU_NO,                                                                                            " & vbNewLine _
    '                                  & "      ZAITRS.ZONE_CD,                                                                                            " & vbNewLine _
    '                                  & "      ZAITRS.LOCA,                                                                                               " & vbNewLine _
    '                                  & "      ZAITRS.INKA_NO_L,                                                                                          " & vbNewLine _
    '                                  & "      ZAITRS.INKA_NO_M,                                                                                          " & vbNewLine _
    '                                  & "      ZAITRS.INKA_NO_S,                                                                                          " & vbNewLine _
    '                                  & "      ZAITRS.GOODS_COND_KB_1,                                                                                    " & vbNewLine _
    '                                  & "      ZAITRS.GOODS_COND_KB_2,                                                                                    " & vbNewLine _
    '                                  & "      ZAITRS.GOODS_COND_KB_3,                                                                                    " & vbNewLine _
    '                                  & "      ZAITRS.CUST_CD_L,                                                                                          " & vbNewLine _
    '                                  & "      ZAITRS.OFB_KB,                                                                                             " & vbNewLine _
    '                                  & "      ZAITRS.SPD_KB,                                                                                             " & vbNewLine _
    '                                  & "      ZAITRS.ALLOC_PRIORITY,                                                                                     " & vbNewLine _
    '                                  & "      ZAITRS.DEST_CD_P,                                                                                           " & vbNewLine _
    '                                  & "      ZAITRS.LOT_NO                                                                                           " & vbNewLine _
    '                                  & "   FROM $LM_TRN$..D_ZAI_TRS AS ZAITRS                                                                            " & vbNewLine _
    '                                  & "   WHERE ZAITRS.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
    '                                  & "   AND ZAITRS.INKA_NO_L  = @INKA_NO_L                                                                              " & vbNewLine _
    '                                  & "   AND ZAITRS.INKA_NO_M  = @INKA_NO_M                                                                              " & vbNewLine _
    '                                  & "   AND ZAITRS.INKA_NO_S  = @INKA_NO_S                                                                              " & vbNewLine _
    '                                  & "   AND ZAITRS.SYS_DEL_FLG = '0'                                                                                    " & vbNewLine
    'START YANAI 要望番号469
    '''' <summary>
    '''' 入出荷履歴（入荷ごと）タブ選択時セレクト文４
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_RIREKI4 As String = "UNION                                                                                              " & vbNewLine _
    '                                  & "SELECT                                                                                                           " & vbNewLine _
    '                                  & " CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                " & vbNewLine _
    '                                  & "      ELSE MR2.RPT_ID END                        AS RPT_ID,                                                      " & vbNewLine _
    '                                  & "   @NRS_BR_CD                                  AS NRS_BR_CD,                                                     " & vbNewLine _
    '                                  & "   NRS_BR.NRS_BR_NM                            AS NRS_BR_NM,                                                     " & vbNewLine _
    '                                  & "   CUST.CUST_CD_L                              AS CUST_CD_L,                                                     " & vbNewLine _
    '                                  & "   CUST.CUST_NM_L                              AS CUST_NM_L,                                                     " & vbNewLine _
    '                                  & "   @WH_CD                                      AS WH_CD,                                                         " & vbNewLine _
    '                                  & "   SOKO.WH_NM                                  AS WH_NM,                                                         " & vbNewLine _
    '                                  & "   GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST,                                                 " & vbNewLine _
    '                                  & "   GOODS.GOODS_NM_1                            AS GOODS_NM,                                                      " & vbNewLine _
    '                                  & "   GOODS.STD_IRIME_NB                          AS IRIME,                                                         " & vbNewLine _
    '                                  & "   @KAZU_KB                                    AS KAZU_KB,                                                       " & vbNewLine _
    '                                  & "   @GUI_IRIME                                  AS GUI_IRIME,                                                     " & vbNewLine _
    '                                  & "   (CASE WHEN INKAL.INKA_STATE_KB < '50' THEN '予' ELSE '' END) AS YOJITU,                                       " & vbNewLine _
    '                                  & "   CASE WHEN RTRIM(INKAL.FURI_NO) = '' THEN '入荷' ELSE '振入' END AS SYUBETU,                                                                                            " & vbNewLine _
    '                                  & "   ZAITRS.INKO_PLAN_DATE AS PLAN_DATE,                                                                           " & vbNewLine _
    '                                  & "   INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU AS INKA_KOSU,                                                         " & vbNewLine _
    '                                  & "   ZAITRS.ALLOC_CAN_QT - ISNULL(OUTKAS.ALCTD_QT,0) AS INKA_SURYO,                                                " & vbNewLine _
    '                                  & "   0 AS OUTKA_KOSU,                                                                                              " & vbNewLine _
    '                                  & "   0 AS OUTKA_SURYO,                                " & vbNewLine _
    '                                  & "   0 AS ZAN_KOSU,                                                                                                " & vbNewLine _
    '                                  & "   GOODS.NB_UT AS NB_UT,                                                                                             " & vbNewLine _
    '                                  & "   0 AS ZAN_SURYO,                                                                                               " & vbNewLine _
    '                                  & "   GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                           " & vbNewLine _
    '                                  & "   ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD + (CASE WHEN ZAITRS.LOCA = '' THEN ''" _
    '                                  & " ELSE '-' +  ZAITRS.LOCA END) AS OKIBA,                                                                          " & vbNewLine _
    '                                  & "   ZAITRS.INKA_NO_L + '-' + ZAITRS.INKA_NO_M + '-' + ZAITRS.INKA_NO_S AS KANRI_NO,                               " & vbNewLine _
    '                                  & "   ZAITRS.INKA_NO_L  AS KANRI_NO_L,                                                                              " & vbNewLine _
    '                                  & "   ZAITRS.INKA_NO_M  AS KANRI_NO_M,                                                                              " & vbNewLine _
    '                                  & "   ZAITRS.INKA_NO_S  AS KANRI_NO_S,                                                                              " & vbNewLine _
    '                                  & "   INKAS.ZAI_REC_NO AS ZAI_REC_NO,                                                                               " & vbNewLine _
    '                                  & "   '' AS DEST_NM,                                                                                                " & vbNewLine _
    '                                  & "   CASE WHEN ISNULL(RTRIM(INKAM.OUTKA_FROM_ORD_NO_M), '') = '' THEN INKAL.OUTKA_FROM_ORD_NO_L ELSE INKAM.OUTKA_FROM_ORD_NO_M END  AS ORD_NO,                                        " & vbNewLine _
    '                                  & "   CASE WHEN ISNULL(RTRIM(INKAM.BUYER_ORD_NO_M), '')      = '' THEN INKAL.BUYER_ORD_NO_L      ELSE INKAM.BUYER_ORD_NO_M      END  AS BUYER_ORD_NO,                                            " & vbNewLine _
    '                                  & "   UNSOCO.UNSOCO_NM AS UNSOCO_NM,                                                                                " & vbNewLine _
    '                                  & "   INKAS.REMARK AS REMARK,                                                                                       " & vbNewLine _
    '                                  & "   INKAS.REMARK_OUT AS REMARK_OUT,                                                                               " & vbNewLine _
    '                                  & "   KBN1.KBN_NM1 AS GOODS_COND_NM_1,                                                                              " & vbNewLine _
    '                                  & "   KBN2.KBN_NM1 AS GOODS_COND_NM_2,                                                                              " & vbNewLine _
    '                                  & "   CUSTCOND.JOTAI_NM AS GOODS_COND_NM_3,                                                                         " & vbNewLine _
    '                                  & "   KBN3.KBN_NM1 AS OFB_KB_NM,                                                                                    " & vbNewLine _
    '                                  & "   KBN4.KBN_NM1 AS SPD_KB_NM,                                                                                    " & vbNewLine _
    '                                  & "   KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                                                            " & vbNewLine _
    '                                  & "   DEST.DEST_NM AS DEST_CD_NM,                                                                                   " & vbNewLine _
    '                                  & "   '' AS RSV_NO,                                                                                                 " & vbNewLine _
    '                                  & "   '1' AS SORT_KEY,                                                                                              " & vbNewLine _
    '                                  & "   GOODS.GOODS_NM_1   AS GOODS_NM,                                                                               " & vbNewLine _
    '                                  & "   ZAITRS.LOT_NO      AS LOT_NO,                                                                                 " & vbNewLine _
    '                                  & "   GOODS.STD_IRIME_NB AS IRIME                                                                                   " & vbNewLine _
    '                                  & "FROM  $LM_TRN$..B_INKA_S AS INKAS                                                                                " & vbNewLine _
    '                                  & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
    '                                  & "   (SELECT                                                                                                       " & vbNewLine _
    '                                  & "      ZAITRS.ZAI_REC_NO,                                                                                         " & vbNewLine _
    '                                  & "      ZAITRS.NRS_BR_CD,                                                                                          " & vbNewLine _
    '                                  & "      ZAITRS.INKO_PLAN_DATE,                                                                                     " & vbNewLine _
    '                                  & "      ZAITRS.TOU_NO,                                                                                             " & vbNewLine _
    '                                  & "      ZAITRS.SITU_NO,                                                                                            " & vbNewLine _
    '                                  & "      ZAITRS.ZONE_CD,                                                                                            " & vbNewLine _
    '                                  & "      ZAITRS.LOCA,                                                                                               " & vbNewLine _
    '                                  & "      ZAITRS.INKA_NO_L,                                                                                          " & vbNewLine _
    '                                  & "      ZAITRS.INKA_NO_M,                                                                                          " & vbNewLine _
    '                                  & "      ZAITRS.INKA_NO_S,                                                                                          " & vbNewLine _
    '                                  & "      ZAITRS.GOODS_COND_KB_1,                                                                                    " & vbNewLine _
    '                                  & "      ZAITRS.GOODS_COND_KB_2,                                                                                    " & vbNewLine _
    '                                  & "      ZAITRS.GOODS_COND_KB_3,                                                                                    " & vbNewLine _
    '                                  & "      ZAITRS.CUST_CD_L,                                                                                          " & vbNewLine _
    '                                  & "      ZAITRS.OFB_KB,                                                                                             " & vbNewLine _
    '                                  & "      ZAITRS.SPD_KB,                                                                                             " & vbNewLine _
    '                                  & "      ZAITRS.ALLOC_PRIORITY,                                                                                     " & vbNewLine _
    '                                  & "      ZAITRS.DEST_CD_P,                                                                                          " & vbNewLine _
    '                                  & "      ZAITRS.LOT_NO,                                                                                             " & vbNewLine _
    '                                  & "      ZAITRS.ALLOC_CAN_QT                                                                                        " & vbNewLine _
    '                                  & "   FROM $LM_TRN$..D_ZAI_TRS AS ZAITRS                                                                            " & vbNewLine _
    '                                  & "   WHERE ZAITRS.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
    '                                  & "   AND ZAITRS.INKA_NO_L  = @INKA_NO_L                                                                              " & vbNewLine _
    '                                  & "   AND ZAITRS.INKA_NO_M  = @INKA_NO_M                                                                              " & vbNewLine _
    '                                  & "   AND ZAITRS.INKA_NO_S  = @INKA_NO_S                                                                              " & vbNewLine _
    '                                  & "   AND ZAITRS.SYS_DEL_FLG = '0'                                                                                    " & vbNewLine
    ''' <summary>
    ''' 入出荷履歴（入荷ごと）タブ選択時セレクト文４
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_RIREKI4 As String = "UNION                                                                                              " & vbNewLine _
                                          & "SELECT                                                                                                           " & vbNewLine _
                                          & " CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                " & vbNewLine _
                                          & "      ELSE MR2.RPT_ID END                        AS RPT_ID,                                                      " & vbNewLine _
                                          & "   @NRS_BR_CD                                  AS NRS_BR_CD,                                                     " & vbNewLine _
                                          & "   NRS_BR.NRS_BR_NM                            AS NRS_BR_NM,                                                     " & vbNewLine _
                                          & "   CUST.CUST_CD_L                              AS CUST_CD_L,                                                     " & vbNewLine _
                                          & "   CUST.CUST_NM_L                              AS CUST_NM_L,                                                     " & vbNewLine _
                                          & "   @WH_CD                                      AS WH_CD,                                                         " & vbNewLine _
                                          & "   SOKO.WH_NM                                  AS WH_NM,                                                         " & vbNewLine _
                                          & "   GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST,                                                 " & vbNewLine _
                                          & "   GOODS.GOODS_NM_1                            AS GOODS_NM,                                                      " & vbNewLine _
                                          & "   GOODS.STD_IRIME_NB                          AS IRIME,                                                         " & vbNewLine _
                                          & "   @KAZU_KB                                    AS KAZU_KB,                                                       " & vbNewLine _
                                          & "   @GUI_IRIME                                  AS GUI_IRIME,                                                     " & vbNewLine _
                                          & "--   (CASE WHEN INKAL.INKA_STATE_KB < '50' THEN '予' ELSE '' END) AS YOJITU,                                       " & vbNewLine _
                                          & "--   CASE WHEN RTRIM(INKAL.FURI_NO) = '' THEN '入荷' ELSE '振入' END AS SYUBETU,                                                                                            " & vbNewLine _
                                          & "   (CASE WHEN INKAL.INKA_STATE_KB < '50' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K026' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') ELSE '' END) AS YOJITU,                                       " & vbNewLine _
                                          & "   CASE WHEN RTRIM(INKAL.FURI_NO) = '' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K028' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') ELSE (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K029' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') END AS SYUBETU,                                                                                            " & vbNewLine _
                                          & "   ZAITRS.INKO_PLAN_DATE AS PLAN_DATE,                                                                           " & vbNewLine _
                                          & "   INKAS.KONSU * ISNULL(GOODS.PKG_NB,0) + INKAS.HASU AS INKA_KOSU,                                                         " & vbNewLine _
                                          & "   (INKAS.KONSU * ISNULL(GOODS.PKG_NB,0) + INKAS.HASU) * INKAS.IRIME AS INKA_SURYO,                                        " & vbNewLine _
                                          & "   0 AS OUTKA_KOSU,                                                                                              " & vbNewLine _
                                          & "   0 AS OUTKA_SURYO,                                " & vbNewLine _
                                          & "   0 AS ZAN_KOSU,                                                                                                " & vbNewLine _
                                          & "   GOODS.NB_UT AS NB_UT,                                                                                             " & vbNewLine _
                                          & "   0 AS ZAN_SURYO,                                                                                               " & vbNewLine _
                                          & "   GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                           " & vbNewLine _
                                          & "   ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD + (CASE WHEN ZAITRS.LOCA = '' THEN ''" _
                                          & " ELSE '-' +  ZAITRS.LOCA END) AS OKIBA,                                                                          " & vbNewLine _
                                          & "   ZAITRS.INKA_NO_L + '-' + ZAITRS.INKA_NO_M + '-' + ZAITRS.INKA_NO_S AS KANRI_NO,                               " & vbNewLine _
                                          & "   ZAITRS.INKA_NO_L  AS KANRI_NO_L,                                                                              " & vbNewLine _
                                          & "   ZAITRS.INKA_NO_M  AS KANRI_NO_M,                                                                              " & vbNewLine _
                                          & "   ZAITRS.INKA_NO_S  AS KANRI_NO_S,                                                                              " & vbNewLine _
                                          & "   INKAS.ZAI_REC_NO AS ZAI_REC_NO,                                                                               " & vbNewLine _
                                          & "   '' AS DEST_NM,                                                                                                " & vbNewLine _
                                          & "   CASE WHEN ISNULL(RTRIM(INKAM.OUTKA_FROM_ORD_NO_M), '') = '' THEN INKAL.OUTKA_FROM_ORD_NO_L ELSE INKAM.OUTKA_FROM_ORD_NO_M END  AS ORD_NO,                                        " & vbNewLine _
                                          & "   CASE WHEN ISNULL(RTRIM(INKAM.BUYER_ORD_NO_M), '')      = '' THEN INKAL.BUYER_ORD_NO_L      ELSE INKAM.BUYER_ORD_NO_M      END  AS BUYER_ORD_NO,                                            " & vbNewLine _
                                          & "   UNSOCO.UNSOCO_NM AS UNSOCO_NM,                                                                                " & vbNewLine _
                                          & "   INKAS.REMARK AS REMARK,                                                                                       " & vbNewLine _
                                          & "   INKAS.REMARK_OUT AS REMARK_OUT,                                                                               " & vbNewLine _
                                          & "   KBN1.KBN_NM1 AS GOODS_COND_NM_1,                                                                              " & vbNewLine _
                                          & "   KBN2.KBN_NM1 AS GOODS_COND_NM_2,                                                                              " & vbNewLine _
                                          & "   CUSTCOND.JOTAI_NM AS GOODS_COND_NM_3,                                                                         " & vbNewLine _
                                          & "   KBN3.KBN_NM1 AS OFB_KB_NM,                                                                                    " & vbNewLine _
                                          & "   KBN4.KBN_NM1 AS SPD_KB_NM,                                                                                    " & vbNewLine _
                                          & "   KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                                                            " & vbNewLine _
                                          & "   DEST.DEST_NM AS DEST_CD_NM,                                                                                   " & vbNewLine _
                                          & "   '' AS RSV_NO,                                                                                                 " & vbNewLine _
                                          & "   '1' AS SORT_KEY,                                                                                              " & vbNewLine _
                                          & "   GOODS.GOODS_NM_1   AS GOODS_NM,                                                                               " & vbNewLine _
                                          & "   ZAITRS.LOT_NO      AS LOT_NO,                                                                                 " & vbNewLine _
                                          & "   GOODS.STD_IRIME_NB AS IRIME                                                                                   " & vbNewLine _
                                          & " -- 1888_WIT_ロケーション変更強化対応                                                                            " & vbNewLine _
                                          & " , ISNULL(TRS_UPD_USER.USER_NM, '') AS ZAI_TRS_UPD_USER_NM                                                       " & vbNewLine _
                                          & "FROM  $LM_TRN$..B_INKA_S AS INKAS                                                                                " & vbNewLine _
                                          & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
                                          & "   (SELECT                                                                                                       " & vbNewLine _
                                          & "      ZAITRS.ZAI_REC_NO,                                                                                         " & vbNewLine _
                                          & "      ZAITRS.NRS_BR_CD,                                                                                          " & vbNewLine _
                                          & "      ZAITRS.INKO_PLAN_DATE,                                                                                     " & vbNewLine _
                                          & "      ZAITRS.TOU_NO,                                                                                             " & vbNewLine _
                                          & "      ZAITRS.SITU_NO,                                                                                            " & vbNewLine _
                                          & "      ZAITRS.ZONE_CD,                                                                                            " & vbNewLine _
                                          & "      ZAITRS.LOCA,                                                                                               " & vbNewLine _
                                          & "      ZAITRS.INKA_NO_L,                                                                                          " & vbNewLine _
                                          & "      ZAITRS.INKA_NO_M,                                                                                          " & vbNewLine _
                                          & "      ZAITRS.INKA_NO_S,                                                                                          " & vbNewLine _
                                          & "      ZAITRS.GOODS_COND_KB_1,                                                                                    " & vbNewLine _
                                          & "      ZAITRS.GOODS_COND_KB_2,                                                                                    " & vbNewLine _
                                          & "      ZAITRS.GOODS_COND_KB_3,                                                                                    " & vbNewLine _
                                          & "      ZAITRS.CUST_CD_L,                                                                                          " & vbNewLine _
                                          & "      ZAITRS.OFB_KB,                                                                                             " & vbNewLine _
                                          & "      ZAITRS.SPD_KB,                                                                                             " & vbNewLine _
                                          & "      ZAITRS.ALLOC_PRIORITY,                                                                                     " & vbNewLine _
                                          & "      ZAITRS.DEST_CD_P,                                                                                          " & vbNewLine _
                                          & "      ZAITRS.LOT_NO,                                                                                             " & vbNewLine _
                                          & "      ZAITRS.ALLOC_CAN_QT                                                                                        " & vbNewLine _
                                          & " -- 1888_WIT_ロケーション変更強化対応                                                                            " & vbNewLine _
                                          & "    , ZAITRS.SYS_UPD_USER                                                                                        " & vbNewLine _
                                          & "   FROM $LM_TRN$..D_ZAI_TRS AS ZAITRS                                                                            " & vbNewLine _
                                          & "   WHERE ZAITRS.NRS_BR_CD = @NRS_BR_CD                                                                           " & vbNewLine _
                                          & "   AND ZAITRS.GOODS_CD_NRS = @GOODS_CD_NRS                                                                       " & vbNewLine _
                                          & "   AND ZAITRS.SYS_DEL_FLG = '0'                                                                                  " & vbNewLine
    'END YANAI 要望番号469
    'END YANAI 要望番号414

    'START YANAI 要望番号414
    '''' <summary>
    '''' 入出荷履歴（入荷ごと）タブ選択時セレクト文５
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_RIREKI5 As String = "   )ZAITRS                                                                                         " & vbNewLine _
    '                                  & "  ON ZAITRS.ZAI_REC_NO = INKAS.ZAI_REC_NO                                                                        " & vbNewLine _
    '                                  & "  AND ZAITRS.NRS_BR_CD = INKAS.NRS_BR_CD                                                                         " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_TRN$..B_INKA_M AS INKAM                                                                        " & vbNewLine _
    '                                  & "  ON INKAS.INKA_NO_L = INKAM.INKA_NO_L                                                                           " & vbNewLine _
    '                                  & "  AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                                                                          " & vbNewLine _
    '                                  & "  AND INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                                                          " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_TRN$..B_INKA_L AS INKAL                                                                        " & vbNewLine _
    '                                  & "  ON INKAS.INKA_NO_L = INKAL.INKA_NO_L                                                                           " & vbNewLine _
    '                                  & "  AND INKAS.NRS_BR_CD = INKAL.NRS_BR_CD                                                                          " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_TRN$..F_UNSO_L AS UNSOL                                                                        " & vbNewLine _
    '                                  & "  ON INKAS.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                           " & vbNewLine _
    '                                  & "  AND INKAS.INKA_NO_L = UNSOL.INOUTKA_NO_L                                                                          " & vbNewLine _
    '                                  & "  AND UNSOL.MOTO_DATA_KB = '10'                                                                          " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO                                                                       " & vbNewLine _
    '                                  & "  ON UNSOL.NRS_BR_CD = UNSOCO.NRS_BR_CD                                                                          " & vbNewLine _
    '                                  & "  AND UNSOL.UNSO_CD = UNSOCO.UNSOCO_CD                                                                           " & vbNewLine _
    '                                  & "  AND UNSOL.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD                                                                     " & vbNewLine _
    '                                  & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
    '                                  & "   (SELECT                                                                                                       " & vbNewLine _
    '                                  & "      GOODS.PKG_NB,                                                                                              " & vbNewLine _
    '                                  & "      GOODS.STD_IRIME_NB,                                                                                        " & vbNewLine _
    '                                  & "      GOODS.STD_IRIME_UT,                                                                                        " & vbNewLine _
    '                                  & "      GOODS.NB_UT,                                                                                               " & vbNewLine _
    '                                  & "      GOODS.GOODS_NM_1,                                                                                          " & vbNewLine _
    '                                  & "      GOODS.NRS_BR_CD,                                                                                           " & vbNewLine _
    '                                  & "      GOODS.GOODS_CD_CUST,                                                                                       " & vbNewLine _
    '                                  & "      GOODS.CUST_CD_L,                                                                                           " & vbNewLine _
    '                                  & "      GOODS.CUST_CD_M,                                                                                           " & vbNewLine _
    '                                  & "      GOODS.CUST_CD_S,                                                                                           " & vbNewLine _
    '                                  & "      GOODS.CUST_CD_SS                                                                                           " & vbNewLine _
    '                                  & "   FROM $LM_MST$..M_GOODS AS GOODS                                                                               " & vbNewLine _
    '                                  & "   WHERE GOODS.NRS_BR_CD = @NRS_BR_CD                                                                            " & vbNewLine _
    '                                  & "     AND GOODS.GOODS_CD_NRS = @GOODS_CD_NRS)GOODS                                                                " & vbNewLine _
    '                                  & "  ON INKAS.NRS_BR_CD = GOODS.NRS_BR_CD                                                                           " & vbNewLine _
    '                                  & "   --荷主M                                                                                                      " & vbNewLine _
    '                                  & "   LEFT JOIN                                                                                                    " & vbNewLine _
    '                                  & "     (SELECT                                                                                                    " & vbNewLine _
    '                                  & "        CUST.CUST_CD_L,                                                                                         " & vbNewLine _
    '                                  & "        CUST.CUST_CD_M,                                                                                         " & vbNewLine _
    '                                  & "        CUST.CUST_CD_S,                                                                                         " & vbNewLine _
    '                                  & "        CUST.CUST_CD_SS,                                                                                        " & vbNewLine _
    '                                  & "        CUST.CUST_NM_L,                                                                                         " & vbNewLine _
    '                                  & "        CUST.NRS_BR_CD                                                                                          " & vbNewLine _
    '                                  & "     FROM $LM_MST$..M_CUST AS CUST                                                                                " & vbNewLine _
    '                                  & "     WHERE CUST.NRS_BR_CD    = @NRS_BR_CD   ) CUST                                                              " & vbNewLine _
    '                                  & "   ON                                                                                                           " & vbNewLine _
    '                                  & "      CUST.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "       GOODS.CUST_CD_L  = CUST.CUST_CD_L                                                                        " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "       GOODS.CUST_CD_M  = CUST.CUST_CD_M                                                                        " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "       GOODS.CUST_CD_S  = CUST.CUST_CD_S                                                                        " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "       GOODS.CUST_CD_SS = CUST.CUST_CD_SS                                                                       " & vbNewLine _
    '                                  & "   --営業所M                                                                                                    " & vbNewLine _
    '                                  & "   LEFT JOIN                                                                                                    " & vbNewLine _
    '                                  & "      $LM_MST$..M_NRS_BR AS NRS_BR                                                                                " & vbNewLine _
    '                                  & "   ON                                                                                                           " & vbNewLine _
    '                                  & "      NRS_BR.NRS_BR_CD = @NRS_BR_CD                                                                             " & vbNewLine _
    '                                  & "   --倉庫M                                                                                                      " & vbNewLine _
    '                                  & "   LEFT JOIN                                                                                                    " & vbNewLine _
    '                                  & "      $LM_MST$..M_SOKO AS SOKO                                                                                    " & vbNewLine _
    '                                  & "   ON                                                                                                           " & vbNewLine _
    '                                  & "      SOKO.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "      SOKO.WH_CD     = @WH_CD                                                                                   " & vbNewLine _
    '                                  & "   --商品コードでの荷主帳票パターン取得                                                                         " & vbNewLine _
    '                                  & "    LEFT JOIN                                                                                                   " & vbNewLine _
    '                                  & "     $LM_MST$..M_CUST_RPT MCR1                                                                                    " & vbNewLine _
    '                                  & "    ON                                                                                                          " & vbNewLine _
    '                                  & "     MCR1.NRS_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MCR1.CUST_CD_L  = GOODS.CUST_CD_L                                                                          " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MCR1.CUST_CD_M  = GOODS.CUST_CD_M                                                                          " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MCR1.CUST_CD_S = GOODS.CUST_CD_S                                                                           " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MCR1.PTN_ID    = @PTN_ID                                                                                   " & vbNewLine _
    '                                  & "    --帳票パターン取得                                                                                          " & vbNewLine _
    '                                  & "    LEFT JOIN                                                                                                   " & vbNewLine _
    '                                  & "     $LM_MST$..M_RPT MR1                                                                                          " & vbNewLine _
    '                                  & "    ON                                                                                                          " & vbNewLine _
    '                                  & "     MR1.NRS_BR_CD = @NRS_BR_CD                                                                                 " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MR1.PTN_ID    = MCR1.PTN_ID                                                                                " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MR1.PTN_CD    = MCR1.PTN_CD                                                                                " & vbNewLine _
    '                                  & "    --存在しない場合の帳票パターン取得                                                                          " & vbNewLine _
    '                                  & "    LEFT JOIN                                                                                                   " & vbNewLine _
    '                                  & "     $LM_MST$..M_RPT MR2                                                                                          " & vbNewLine _
    '                                  & "    ON                                                                                                          " & vbNewLine _
    '                                  & "     MR2.NRS_BR_CD     = @NRS_BR_CD                                                                             " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MR2.PTN_ID        = @PTN_ID                                                                                " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MR2.STANDARD_FLAG = '01'                                                                                   " & vbNewLine _
    '                                  & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
    '                                  & "   (SELECT                                                                                                       " & vbNewLine _
    '                                  & "      KBN1.KBN_NM1,                                                                                              " & vbNewLine _
    '                                  & "      KBN1.KBN_CD                                                                                                " & vbNewLine _
    '                                  & "   FROM $LM_MST$..Z_KBN AS KBN1                                                                                    " & vbNewLine _
    '                                  & "   WHERE KBN1.KBN_GROUP_CD = 'S005') KBN1                                                                        " & vbNewLine _
    '                                  & "  ON ZAITRS.GOODS_COND_KB_1 = KBN1.KBN_CD                                                                        " & vbNewLine _
    '                                  & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
    '                                  & "   (SELECT                                                                                                       " & vbNewLine _
    '                                  & "      KBN2.KBN_NM1,                                                                                              " & vbNewLine _
    '                                  & "      KBN2.KBN_CD                                                                                                " & vbNewLine _
    '                                  & "   FROM $LM_MST$..Z_KBN AS KBN2                                                                                    " & vbNewLine _
    '                                  & "   WHERE KBN2.KBN_GROUP_CD = 'S006') KBN2                                                                        " & vbNewLine _
    '                                  & "  ON ZAITRS.GOODS_COND_KB_2 = KBN2.KBN_CD                                                                        " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_MST$..M_CUSTCOND AS CUSTCOND                                                                   " & vbNewLine _
    '                                  & "  ON ZAITRS.NRS_BR_CD = CUSTCOND.NRS_BR_CD                                                                       " & vbNewLine _
    '                                  & "  AND ZAITRS.CUST_CD_L = CUSTCOND.CUST_CD_L                                                                      " & vbNewLine _
    '                                  & "  AND ZAITRS.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD                                                                 " & vbNewLine _
    '                                  & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
    '                                  & "   (SELECT                                                                                                       " & vbNewLine _
    '                                  & "      KBN3.KBN_NM1,                                                                                              " & vbNewLine _
    '                                  & "      KBN3.KBN_CD                                                                                                " & vbNewLine _
    '                                  & "   FROM $LM_MST$..Z_KBN AS KBN3                                                                                    " & vbNewLine _
    '                                  & "   WHERE KBN3.KBN_GROUP_CD = 'B002') KBN3                                                                        " & vbNewLine _
    '                                  & "  ON ZAITRS.OFB_KB = KBN3.KBN_CD                                                                                 " & vbNewLine _
    '                                  & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
    '                                  & "   (SELECT                                                                                                       " & vbNewLine _
    '                                  & "      KBN4.KBN_NM1,                                                                                              " & vbNewLine _
    '                                  & "      KBN4.KBN_CD                                                                                                " & vbNewLine _
    '                                  & "   FROM $LM_MST$..Z_KBN AS KBN4                                                                                    " & vbNewLine _
    '                                  & "   WHERE KBN4.KBN_GROUP_CD = 'H003') KBN4                                                                        " & vbNewLine _
    '                                  & "  ON ZAITRS.SPD_KB = KBN4.KBN_CD                                                                                 " & vbNewLine _
    '                                  & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
    '                                  & "   (SELECT                                                                                                       " & vbNewLine _
    '                                  & "      KBN5.KBN_NM1,                                                                                              " & vbNewLine _
    '                                  & "      KBN5.KBN_CD                                                                                                " & vbNewLine _
    '                                  & "   FROM $LM_MST$..Z_KBN AS KBN5                                                                                    " & vbNewLine _
    '                                  & "   WHERE KBN5.KBN_GROUP_CD = 'W001') KBN5                                                                        " & vbNewLine _
    '                                  & "  ON ZAITRS.ALLOC_PRIORITY = KBN5.KBN_CD                                                                         " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST                                                                           " & vbNewLine _
    '                                  & "   ON ZAITRS.NRS_BR_CD = DEST.NRS_BR_CD                                                                          " & vbNewLine _
    '                                  & "   AND ZAITRS.CUST_CD_L = DEST.CUST_CD_L                                                                         " & vbNewLine _
    '                                  & "   AND ZAITRS.DEST_CD_P = DEST.DEST_CD                                                                           " & vbNewLine _
    '                                  & "WHERE INKAS.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
    '                                  & "  AND INKAS.INKA_NO_L  = @INKA_NO_L                                                                              " & vbNewLine _
    '                                  & "  AND INKAS.INKA_NO_M  = @INKA_NO_M                                                                              " & vbNewLine _
    '                                  & "  AND INKAS.INKA_NO_S  = @INKA_NO_S                                                                              " & vbNewLine _
    '                                  & "  AND INKAS.SYS_DEL_FLG = '0'                                                                                    " & vbNewLine
    ''' <summary>
    ''' 入出荷履歴（入荷ごと）タブ選択時セレクト文５
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_RIREKI5 As String = "   )ZAITRS                                                                                             " & vbNewLine _
                                          & "  ON ZAITRS.ZAI_REC_NO = INKAS.ZAI_REC_NO                                                                        " & vbNewLine _
                                          & "  AND ZAITRS.NRS_BR_CD = INKAS.NRS_BR_CD                                                                         " & vbNewLine _
                                          & " -- 1888_WIT_ロケーション変更強化対応                                                                            " & vbNewLine _
                                          & "  LEFT OUTER JOIN $LM_MST$..S_USER AS TRS_UPD_USER                                                               " & vbNewLine _
                                          & "    ON ZAITRS.SYS_UPD_USER = TRS_UPD_USER.USER_CD                                                                " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_TRN$..B_INKA_M AS INKAM                                                                      " & vbNewLine _
                                          & "  ON INKAS.INKA_NO_L = INKAM.INKA_NO_L                                                                           " & vbNewLine _
                                          & "  AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                                                                          " & vbNewLine _
                                          & "  AND INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                                                          " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_TRN$..B_INKA_L AS INKAL                                                                        " & vbNewLine _
                                          & "  ON INKAS.INKA_NO_L = INKAL.INKA_NO_L                                                                           " & vbNewLine _
                                          & "  AND INKAS.NRS_BR_CD = INKAL.NRS_BR_CD                                                                          " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_TRN$..F_UNSO_L AS UNSOL                                                                        " & vbNewLine _
                                          & "  ON INKAS.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                           " & vbNewLine _
                                          & "  AND INKAS.INKA_NO_L = UNSOL.INOUTKA_NO_L                                                                          " & vbNewLine _
                                          & "  AND UNSOL.MOTO_DATA_KB = '10'                                                                          " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO                                                                       " & vbNewLine _
                                          & "  ON UNSOL.NRS_BR_CD = UNSOCO.NRS_BR_CD                                                                          " & vbNewLine _
                                          & "  AND UNSOL.UNSO_CD = UNSOCO.UNSOCO_CD                                                                           " & vbNewLine _
                                          & "  AND UNSOL.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD                                                                     " & vbNewLine _
                                          & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
                                          & "   (SELECT                                                                                                       " & vbNewLine _
                                          & "      ISNULL(GOODS.PKG_NB,0) AS PKG_NB,                                                                          " & vbNewLine _
                                          & "      GOODS.STD_IRIME_NB,                                                                                        " & vbNewLine _
                                          & "      GOODS.STD_IRIME_UT,                                                                                        " & vbNewLine _
                                          & "      GOODS.NB_UT,                                                                                               " & vbNewLine _
                                          & "      GOODS.GOODS_NM_1,                                                                                          " & vbNewLine _
                                          & "      GOODS.NRS_BR_CD,                                                                                           " & vbNewLine _
                                          & "      GOODS.GOODS_CD_CUST,                                                                                       " & vbNewLine _
                                          & "      GOODS.CUST_CD_L,                                                                                           " & vbNewLine _
                                          & "      GOODS.CUST_CD_M,                                                                                           " & vbNewLine _
                                          & "      GOODS.CUST_CD_S,                                                                                           " & vbNewLine _
                                          & "      GOODS.CUST_CD_SS                                                                                           " & vbNewLine _
                                          & "   FROM $LM_MST$..M_GOODS AS GOODS                                                                               " & vbNewLine _
                                          & "   WHERE GOODS.NRS_BR_CD = @NRS_BR_CD                                                                            " & vbNewLine _
                                          & "     AND GOODS.GOODS_CD_NRS = @GOODS_CD_NRS)GOODS                                                                " & vbNewLine _
                                          & "  ON INKAS.NRS_BR_CD = GOODS.NRS_BR_CD                                                                           " & vbNewLine _
                                          & "   --荷主M                                                                                                      " & vbNewLine _
                                          & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                          & "     (SELECT                                                                                                    " & vbNewLine _
                                          & "        CUST.CUST_CD_L,                                                                                         " & vbNewLine _
                                          & "        CUST.CUST_CD_M,                                                                                         " & vbNewLine _
                                          & "        CUST.CUST_CD_S,                                                                                         " & vbNewLine _
                                          & "        CUST.CUST_CD_SS,                                                                                        " & vbNewLine _
                                          & "        CUST.CUST_NM_L,                                                                                         " & vbNewLine _
                                          & "        CUST.NRS_BR_CD                                                                                          " & vbNewLine _
                                          & "     FROM $LM_MST$..M_CUST AS CUST                                                                                " & vbNewLine _
                                          & "     WHERE CUST.NRS_BR_CD    = @NRS_BR_CD   ) CUST                                                              " & vbNewLine _
                                          & "   ON                                                                                                           " & vbNewLine _
                                          & "      CUST.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
                                          & "   AND                                                                                                          " & vbNewLine _
                                          & "       GOODS.CUST_CD_L  = CUST.CUST_CD_L                                                                        " & vbNewLine _
                                          & "   AND                                                                                                          " & vbNewLine _
                                          & "       GOODS.CUST_CD_M  = CUST.CUST_CD_M                                                                        " & vbNewLine _
                                          & "   AND                                                                                                          " & vbNewLine _
                                          & "       GOODS.CUST_CD_S  = CUST.CUST_CD_S                                                                        " & vbNewLine _
                                          & "   AND                                                                                                          " & vbNewLine _
                                          & "       GOODS.CUST_CD_SS = CUST.CUST_CD_SS                                                                       " & vbNewLine _
                                          & "   --営業所M                                                                                                    " & vbNewLine _
                                          & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                          & "      $LM_MST$..M_NRS_BR AS NRS_BR                                                                                " & vbNewLine _
                                          & "   ON                                                                                                           " & vbNewLine _
                                          & "      NRS_BR.NRS_BR_CD = @NRS_BR_CD                                                                             " & vbNewLine _
                                          & "   --倉庫M                                                                                                      " & vbNewLine _
                                          & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                          & "      $LM_MST$..M_SOKO AS SOKO                                                                                    " & vbNewLine _
                                          & "   ON                                                                                                           " & vbNewLine _
                                          & "      SOKO.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
                                          & "   AND                                                                                                          " & vbNewLine _
                                          & "      SOKO.WH_CD     = @WH_CD                                                                                   " & vbNewLine _
                                          & "   --商品コードでの荷主帳票パターン取得                                                                         " & vbNewLine _
                                          & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                          & "     $LM_MST$..M_CUST_RPT MCR1                                                                                    " & vbNewLine _
                                          & "    ON                                                                                                          " & vbNewLine _
                                          & "     MCR1.NRS_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MCR1.CUST_CD_L  = GOODS.CUST_CD_L                                                                          " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MCR1.CUST_CD_M  = GOODS.CUST_CD_M                                                                          " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MCR1.CUST_CD_S = GOODS.CUST_CD_S                                                                           " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MCR1.PTN_ID    = @PTN_ID                                                                                   " & vbNewLine _
                                          & "    --帳票パターン取得                                                                                          " & vbNewLine _
                                          & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                          & "     $LM_MST$..M_RPT MR1                                                                                          " & vbNewLine _
                                          & "    ON                                                                                                          " & vbNewLine _
                                          & "     MR1.NRS_BR_CD = @NRS_BR_CD                                                                                 " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MR1.PTN_ID    = MCR1.PTN_ID                                                                                " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MR1.PTN_CD    = MCR1.PTN_CD                                                                                " & vbNewLine _
                                          & "    --存在しない場合の帳票パターン取得                                                                          " & vbNewLine _
                                          & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                          & "     $LM_MST$..M_RPT MR2                                                                                          " & vbNewLine _
                                          & "    ON                                                                                                          " & vbNewLine _
                                          & "     MR2.NRS_BR_CD     = @NRS_BR_CD                                                                             " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MR2.PTN_ID        = @PTN_ID                                                                                " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MR2.STANDARD_FLAG = '01'                                                                                   " & vbNewLine _
                                          & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
                                          & "   (SELECT                                                                                                       " & vbNewLine _
                                          & "--      KBN1.KBN_NM1,                                                                                              " & vbNewLine _
                                          & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _
                                          & "      ,KBN1.KBN_CD                                                                                                " & vbNewLine _
                                          & "   FROM $LM_MST$..Z_KBN AS KBN1                                                                                    " & vbNewLine _
                                          & "   WHERE KBN1.KBN_GROUP_CD = 'S005') KBN1                                                                        " & vbNewLine _
                                          & "  ON ZAITRS.GOODS_COND_KB_1 = KBN1.KBN_CD                                                                        " & vbNewLine _
                                          & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
                                          & "   (SELECT                                                                                                       " & vbNewLine _
                                          & "--      KBN2.KBN_NM1,                                                                                              " & vbNewLine _
                                          & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _
                                          & "      ,KBN2.KBN_CD                                                                                                " & vbNewLine _
                                          & "   FROM $LM_MST$..Z_KBN AS KBN2                                                                                    " & vbNewLine _
                                          & "   WHERE KBN2.KBN_GROUP_CD = 'S006') KBN2                                                                        " & vbNewLine _
                                          & "  ON ZAITRS.GOODS_COND_KB_2 = KBN2.KBN_CD                                                                        " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..M_CUSTCOND AS CUSTCOND                                                                   " & vbNewLine _
                                          & "  ON ZAITRS.NRS_BR_CD = CUSTCOND.NRS_BR_CD                                                                       " & vbNewLine _
                                          & "  AND ZAITRS.CUST_CD_L = CUSTCOND.CUST_CD_L                                                                      " & vbNewLine _
                                          & "  AND ZAITRS.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD                                                                 " & vbNewLine _
                                          & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
                                          & "   (SELECT                                                                                                       " & vbNewLine _
                                          & "--      KBN3.KBN_NM1,                                                                                              " & vbNewLine _
                                          & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _
                                          & "      ,KBN3.KBN_CD                                                                                                " & vbNewLine _
                                          & "   FROM $LM_MST$..Z_KBN AS KBN3                                                                                    " & vbNewLine _
                                          & "   WHERE KBN3.KBN_GROUP_CD = 'B002') KBN3                                                                        " & vbNewLine _
                                          & "  ON ZAITRS.OFB_KB = KBN3.KBN_CD                                                                                 " & vbNewLine _
                                          & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
                                          & "   (SELECT                                                                                                       " & vbNewLine _
                                          & "--      KBN4.KBN_NM1,                                                                                              " & vbNewLine _
                                          & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _
                                          & "      ,KBN4.KBN_CD                                                                                                " & vbNewLine _
                                          & "   FROM $LM_MST$..Z_KBN AS KBN4                                                                                    " & vbNewLine _
                                          & "   WHERE KBN4.KBN_GROUP_CD = 'H003') KBN4                                                                        " & vbNewLine _
                                          & "  ON ZAITRS.SPD_KB = KBN4.KBN_CD                                                                                 " & vbNewLine _
                                          & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
                                          & "   (SELECT                                                                                                       " & vbNewLine _
                                          & "--      KBN5.KBN_NM1,                                                                                              " & vbNewLine _
                                          & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _
                                          & "      ,KBN5.KBN_CD                                                                                                " & vbNewLine _
                                          & "   FROM $LM_MST$..Z_KBN AS KBN5                                                                                    " & vbNewLine _
                                          & "   WHERE KBN5.KBN_GROUP_CD = 'W001') KBN5                                                                        " & vbNewLine _
                                          & "  ON ZAITRS.ALLOC_PRIORITY = KBN5.KBN_CD                                                                         " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST                                                                           " & vbNewLine _
                                          & "   ON ZAITRS.NRS_BR_CD = DEST.NRS_BR_CD                                                                          " & vbNewLine _
                                          & "   AND ZAITRS.CUST_CD_L = DEST.CUST_CD_L                                                                         " & vbNewLine _
                                          & "   AND ZAITRS.DEST_CD_P = DEST.DEST_CD                                                                           " & vbNewLine _
                                          & "LEFT JOIN                                                                                                            " & vbNewLine _
                                          & "  (SELECT                                                                                                            " & vbNewLine _
                                          & "  SUM(OUTKAS.ALCTD_QT) AS ALCTD_QT                                                                                   " & vbNewLine _
                                          & "  ,OUTKAS.ZAI_REC_NO AS ZAI_REC_NO                                                                                   " & vbNewLine _
                                          & "  FROM $LM_TRN$..C_OUTKA_S OUTKAS                                                                                    " & vbNewLine _
                                          & "  WHERE OUTKAS.NRS_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
                                          & "  AND OUTKAS.SYS_DEL_FLG = '0'                                                                                       " & vbNewLine _
                                          & "  GROUP BY OUTKAS.ZAI_REC_NO                                                                                         " & vbNewLine _
                                          & "  ) OUTKAS                                                                                                           " & vbNewLine _
                                          & "  ON OUTKAS.ZAI_REC_NO = ZAITRS.ZAI_REC_NO                                                                           " & vbNewLine _
                                          & "WHERE INKAS.NRS_BR_CD = @NRS_BR_CD                                                                                   " & vbNewLine _
                                          & "  AND INKAM.GOODS_CD_NRS = @GOODS_CD_NRS                                                                             " & vbNewLine _
                                          & "  AND INKAS.SYS_DEL_FLG = '0'                                                                                        " & vbNewLine
    'END YANAI 要望番号414


    'START YANAI 要望番号508
    'START YANAI 20110913 小分け対応
    '''' <summary>
    '''' 入出荷履歴（入荷ごと）タブ選択時セレクト文６
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_RIREKI6 As String = "UNION                                                                                              " & vbNewLine _
    '                                  & "SELECT                                                                                                               " & vbNewLine _
    '                                  & " CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                " & vbNewLine _
    '                                  & "      ELSE MR2.RPT_ID END                        AS RPT_ID,                                                      " & vbNewLine _
    '                                  & "   @NRS_BR_CD                                  AS NRS_BR_CD,                                                     " & vbNewLine _
    '                                  & "   NRS_BR.NRS_BR_NM                            AS NRS_BR_NM,                                                     " & vbNewLine _
    '                                  & "   CUST.CUST_CD_L                              AS CUST_CD_L,                                                     " & vbNewLine _
    '                                  & "   CUST.CUST_NM_L                              AS CUST_NM_L,                                                     " & vbNewLine _
    '                                  & "   @WH_CD                                      AS WH_CD,                                                         " & vbNewLine _
    '                                  & "   SOKO.WH_NM                                  AS WH_NM,                                                         " & vbNewLine _
    '                                  & "   GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST,                                                 " & vbNewLine _
    '                                  & "   GOODS.GOODS_NM_1                            AS GOODS_NM,                                                      " & vbNewLine _
    '                                  & "   GOODS.STD_IRIME_NB                          AS IRIME,                                                         " & vbNewLine _
    '                                  & "   @KAZU_KB                                    AS KAZU_KB,                                                       " & vbNewLine _
    '                                  & "   @GUI_IRIME                                  AS GUI_IRIME,                                                     " & vbNewLine _
    '                                  & "   (CASE WHEN OUTKAL.SYS_DEL_FLG = '1' THEN '消' WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB < '60' THEN" _
    '                                  & " 'ヨ' WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB >= '60' THEN 'サ' WHEN OUTKAM.ALCTD_KB <> '04 ' AND" _
    '                                  & " OUTKAL.OUTKA_STATE_KB < '60' THEN '予' ELSE '' END)AS YOJITU,                                                       " & vbNewLine _
    '                                  & "   (CASE WHEN RTRIM(OUTKAL.FURI_NO) = '' THEN '出荷' ELSE '振出' END) AS SYUBETU,                                  " & vbNewLine _
    '                                  & "   OUTKAL.OUTKA_PLAN_DATE AS PLAN_DATE,                                                                              " & vbNewLine _
    '                                  & "   0 AS INKA_KOSU,                                                                                                   " & vbNewLine _
    '                                  & "   0 AS INKA_SURYO,                                                                                                  " & vbNewLine _
    '                                  & "   OUTKAS.ALCTD_NB AS OUTKA_KOSU,                                                                                    " & vbNewLine _
    '                                  & "   OUTKAS.ALCTD_QT AS OUTKA_SURYO,                                                                                   " & vbNewLine _
    '                                  & "   0 AS ZAN_KOSU,                                                                                                    " & vbNewLine _
    '                                  & "   GOODS.NB_UT AS NB_UT,                                                                                             " & vbNewLine _
    '                                  & "   0 AS ZAN_SURYO,                                                                                                   " & vbNewLine _
    '                                  & "   GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                               " & vbNewLine _
    '                                  & "   OUTKAS.TOU_NO + '-' + OUTKAS.SITU_NO + '-' + OUTKAS.ZONE_CD + (CASE WHEN OUTKAS.LOCA = '' THEN '' ELSE '-' + " _
    '                                  & "   OUTKAS.LOCA END) AS OKIBA,                                                                                        " & vbNewLine _
    '                                  & "   OUTKAS.OUTKA_NO_L + '-' + OUTKAS.OUTKA_NO_M + '-' + OUTKAS.OUTKA_NO_S AS KANRI_NO,                                " & vbNewLine _
    '                                  & "   OUTKAS.OUTKA_NO_L AS KANRI_NO_L,                                                                                  " & vbNewLine _
    '                                  & "   OUTKAS.OUTKA_NO_M AS KANRI_NO_M,                                                                                  " & vbNewLine _
    '                                  & "   OUTKAS.OUTKA_NO_S AS KANRI_NO_S,                                                                                  " & vbNewLine _
    '                                  & "   OUTKAS.ZAI_REC_NO AS ZAI_REC_NO,                                                                                  " & vbNewLine _
    '                                  & "   CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.DEST_NM                                                                 " & vbNewLine _
    '                                  & "        ELSE OUTKAL.DEST_NM  END  AS DEST_NM,                                                                        " & vbNewLine _
    '                                  & "   CASE WHEN ISNULL(RTRIM(OUTKAM.CUST_ORD_NO_DTL), '')  = '' THEN OUTKAL.CUST_ORD_NO  ELSE OUTKAM.CUST_ORD_NO_DTL  END  AS ORD_NO,                " & vbNewLine _
    '                                  & "   CASE WHEN ISNULL(RTRIM(OUTKAM.BUYER_ORD_NO_DTL), '') = '' THEN OUTKAL.BUYER_ORD_NO ELSE OUTKAM.BUYER_ORD_NO_DTL END  AS BUYER_ORD_NO,          " & vbNewLine _
    '                                  & "   UNSOCO.UNSOCO_NM AS UNSOCO_NM,                                                                                    " & vbNewLine _
    '                                  & "   OUTKAS.REMARK AS REMARK,                                                                                          " & vbNewLine _
    '                                  & "   '' AS REMARK_OUT,                                                                                                 " & vbNewLine _
    '                                  & "   '' AS GOODS_COND_NM_1,                                                                                            " & vbNewLine _
    '                                  & "   '' AS GOODS_COND_NM_2,                                                                                            " & vbNewLine _
    '                                  & "   '' AS GOODS_COND_NM_3,                                                                                            " & vbNewLine _
    '                                  & "   '' AS OFB_KB_NM,                                                                                                  " & vbNewLine _
    '                                  & "   '' AS SPD_KB_NM,                                                                                                  " & vbNewLine _
    '                                  & "   '' AS ALLOC_PRIORITY_NM,                                                                                          " & vbNewLine _
    '                                  & "   '' AS DEST_CD_NM,                                                                                                 " & vbNewLine _
    '                                  & "   '' AS RSV_NO,                                                                                                     " & vbNewLine _
    '                                  & "   '2' AS SORT_KEY,                                                                                                   " & vbNewLine _
    '                                  & "   GOODS.GOODS_NM_1   AS GOODS_NM,                                                                           " & vbNewLine _
    '                                  & "   ZAITRS.LOT_NO      AS LOT_NO,                                                                           " & vbNewLine _
    '                                  & "   GOODS.STD_IRIME_NB AS IRIME                                                                                   " & vbNewLine _
    '                                  & "FROM $LM_TRN$..C_OUTKA_S AS OUTKAS                                                                                     " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_TRN$..C_OUTKA_M AS OUTKAM                                                                          " & vbNewLine _
    '                                  & "  ON OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                                                             " & vbNewLine _
    '                                  & "  AND OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                                                                          " & vbNewLine _
    '                                  & "  AND OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                                                                          " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_TRN$..C_OUTKA_L AS OUTKAL                                                                          " & vbNewLine _
    '                                  & "  ON OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                                             " & vbNewLine _
    '                                  & "  AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                                                          " & vbNewLine _
    '                                  & "LEFT OUTER JOIN                                                                                                      " & vbNewLine _
    '                                  & "   (SELECT                                                                                                           " & vbNewLine _
    '                                  & "      GOODS.PKG_NB,                                                                                                  " & vbNewLine _
    '                                  & "      GOODS.STD_IRIME_NB,                                                                                            " & vbNewLine _
    '                                  & "      GOODS.STD_IRIME_UT,                                                                                            " & vbNewLine _
    '                                  & "      GOODS.NB_UT,                                                                                                   " & vbNewLine _
    '                                  & "      GOODS.GOODS_NM_1,                                                                                              " & vbNewLine _
    '                                  & "      GOODS.NRS_BR_CD,                                                                                           " & vbNewLine _
    '                                  & "      GOODS.GOODS_CD_CUST,                                                                                       " & vbNewLine _
    '                                  & "      GOODS.CUST_CD_L,                                                                                           " & vbNewLine _
    '                                  & "      GOODS.CUST_CD_M,                                                                                           " & vbNewLine _
    '                                  & "      GOODS.CUST_CD_S,                                                                                           " & vbNewLine _
    '                                  & "      GOODS.CUST_CD_SS                                                                                           " & vbNewLine _
    '                                  & "   FROM $LM_MST$..M_GOODS AS GOODS                                                                                     " & vbNewLine _
    '                                  & "   WHERE GOODS.NRS_BR_CD = @NRS_BR_CD                                                                                  " & vbNewLine _
    '                                  & "     AND GOODS.GOODS_CD_NRS = @GOODS_CD_NRS)GOODS                                                                " & vbNewLine _
    '                                  & "  ON OUTKAS.NRS_BR_CD = GOODS.NRS_BR_CD                                                                              " & vbNewLine _
    '                                  & "   --荷主M                                                                                                      " & vbNewLine _
    '                                  & "   LEFT JOIN                                                                                                    " & vbNewLine _
    '                                  & "     (SELECT                                                                                                    " & vbNewLine _
    '                                  & "        CUST.CUST_CD_L,                                                                                         " & vbNewLine _
    '                                  & "        CUST.CUST_CD_M,                                                                                         " & vbNewLine _
    '                                  & "        CUST.CUST_CD_S,                                                                                         " & vbNewLine _
    '                                  & "        CUST.CUST_CD_SS,                                                                                        " & vbNewLine _
    '                                  & "        CUST.CUST_NM_L,                                                                                         " & vbNewLine _
    '                                  & "        CUST.NRS_BR_CD                                                                                          " & vbNewLine _
    '                                  & "     FROM $LM_MST$..M_CUST AS CUST                                                                                " & vbNewLine _
    '                                  & "     WHERE CUST.NRS_BR_CD    = @NRS_BR_CD   ) CUST                                                              " & vbNewLine _
    '                                  & "   ON                                                                                                           " & vbNewLine _
    '                                  & "      CUST.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "       GOODS.CUST_CD_L  = CUST.CUST_CD_L                                                                        " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "       GOODS.CUST_CD_M  = CUST.CUST_CD_M                                                                        " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "       GOODS.CUST_CD_S  = CUST.CUST_CD_S                                                                        " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "       GOODS.CUST_CD_SS = CUST.CUST_CD_SS                                                                       " & vbNewLine _
    '                                  & "   --営業所M                                                                                                    " & vbNewLine _
    '                                  & "   LEFT JOIN                                                                                                    " & vbNewLine _
    '                                  & "      $LM_MST$..M_NRS_BR AS NRS_BR                                                                                " & vbNewLine _
    '                                  & "   ON                                                                                                           " & vbNewLine _
    '                                  & "      NRS_BR.NRS_BR_CD = @NRS_BR_CD                                                                             " & vbNewLine _
    '                                  & "   --倉庫M                                                                                                      " & vbNewLine _
    '                                  & "   LEFT JOIN                                                                                                    " & vbNewLine _
    '                                  & "      $LM_MST$..M_SOKO AS SOKO                                                                                    " & vbNewLine _
    '                                  & "   ON                                                                                                           " & vbNewLine _
    '                                  & "      SOKO.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "      SOKO.WH_CD     = @WH_CD                                                                                   " & vbNewLine _
    '                                  & "   --商品コードでの荷主帳票パターン取得                                                                         " & vbNewLine _
    '                                  & "    LEFT JOIN                                                                                                   " & vbNewLine _
    '                                  & "     $LM_MST$..M_CUST_RPT MCR1                                                                                    " & vbNewLine _
    '                                  & "    ON                                                                                                          " & vbNewLine _
    '                                  & "     MCR1.NRS_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MCR1.CUST_CD_L  = GOODS.CUST_CD_L                                                                          " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MCR1.CUST_CD_M  = GOODS.CUST_CD_M                                                                          " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MCR1.CUST_CD_S = GOODS.CUST_CD_S                                                                           " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MCR1.PTN_ID    = @PTN_ID                                                                                   " & vbNewLine _
    '                                  & "    --帳票パターン取得                                                                                          " & vbNewLine _
    '                                  & "    LEFT JOIN                                                                                                   " & vbNewLine _
    '                                  & "     $LM_MST$..M_RPT MR1                                                                                          " & vbNewLine _
    '                                  & "    ON                                                                                                          " & vbNewLine _
    '                                  & "     MR1.NRS_BR_CD = @NRS_BR_CD                                                                                 " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MR1.PTN_ID    = MCR1.PTN_ID                                                                                " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MR1.PTN_CD    = MCR1.PTN_CD                                                                                " & vbNewLine _
    '                                  & "    --存在しない場合の帳票パターン取得                                                                          " & vbNewLine _
    '                                  & "    LEFT JOIN                                                                                                   " & vbNewLine _
    '                                  & "     $LM_MST$..M_RPT MR2                                                                                          " & vbNewLine _
    '                                  & "    ON                                                                                                          " & vbNewLine _
    '                                  & "     MR2.NRS_BR_CD     = @NRS_BR_CD                                                                             " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MR2.PTN_ID        = @PTN_ID                                                                                " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MR2.STANDARD_FLAG = '01'                                                                                   " & vbNewLine _
    '                                  & "LEFT OUTER JOIN                                                                                                      " & vbNewLine _
    '                                  & "   (SELECT                                                                                                           " & vbNewLine _
    '                                  & "      ZAITRS.ZAI_REC_NO,                                                                                             " & vbNewLine _
    '                                  & "      ZAITRS.NRS_BR_CD,                                                                                              " & vbNewLine _
    '                                  & "      ZAITRS.TOU_NO,                                                                                                 " & vbNewLine _
    '                                  & "      ZAITRS.SITU_NO,                                                                                                " & vbNewLine _
    '                                  & "      ZAITRS.ZONE_CD,                                                                                                " & vbNewLine _
    '                                  & "      ZAITRS.LOCA,                                                                                                   " & vbNewLine _
    '                                  & "      ZAITRS.CUST_CD_L,                                                                                              " & vbNewLine _
    '                                  & "      ZAITRS.DEST_CD_P,                                                                                               " & vbNewLine _
    '                                  & "      ZAITRS.LOT_NO                                                                                               " & vbNewLine _
    '                                  & "   FROM $LM_TRN$..D_ZAI_TRS AS ZAITRS                                                                                  " & vbNewLine _
    '                                  & "   WHERE ZAITRS.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
    '                                  & "   AND ZAITRS.INKA_NO_L  = @INKA_NO_L                                                                              " & vbNewLine _
    '                                  & "   AND ZAITRS.INKA_NO_M  = @INKA_NO_M                                                                              " & vbNewLine _
    '                                  & "   AND ZAITRS.INKA_NO_S  = @INKA_NO_S                                                                              " & vbNewLine _
    '                                  & "   AND ZAITRS.SYS_DEL_FLG = '0'                                                                                    " & vbNewLine _
    '                                  & "   ) ZAITRS                                                                              " & vbNewLine _
    '                                  & "  ON ZAITRS.NRS_BR_CD = OUTKAS.NRS_BR_CD                                                                             " & vbNewLine _
    '                                  & " AND ZAITRS.ZAI_REC_NO = OUTKAS.ZAI_REC_NO                                                                           " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST                                                                               " & vbNewLine _
    '                                  & "  ON OUTKAL.NRS_BR_CD = DEST.NRS_BR_CD                                                                               " & vbNewLine _
    '                                  & "  AND OUTKAL.CUST_CD_L = DEST.CUST_CD_L                                                                              " & vbNewLine _
    '                                  & "  AND OUTKAL.DEST_CD = DEST.DEST_CD                                                                                  " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_TRN$..F_UNSO_L AS UNSOL                                                                            " & vbNewLine _
    '                                  & "  ON OUTKAS.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                              " & vbNewLine _
    '                                  & "  AND OUTKAS.OUTKA_NO_L = UNSOL.INOUTKA_NO_L                                                                          " & vbNewLine _
    '                                  & "  AND UNSOL.MOTO_DATA_KB = '20'                                                                          " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO                                                                           " & vbNewLine _
    '                                  & "  ON UNSOL.NRS_BR_CD = UNSOCO.NRS_BR_CD                                                                              " & vbNewLine _
    '                                  & "  AND UNSOL.UNSO_CD = UNSOCO.UNSOCO_CD                                                                               " & vbNewLine _
    '                                  & "  AND UNSOL.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD                                                                         " & vbNewLine _
    '                                  & "WHERE OUTKAM.NRS_BR_CD = @NRS_BR_CD                                                                                  " & vbNewLine
    '''' <summary>
    '''' 入出荷履歴（入荷ごと）タブ選択時セレクト文６
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_RIREKI6 As String = "UNION                                                                                              " & vbNewLine _
    '                                  & "SELECT                                                                                                               " & vbNewLine _
    '                                  & " CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                " & vbNewLine _
    '                                  & "      ELSE MR2.RPT_ID END                        AS RPT_ID,                                                      " & vbNewLine _
    '                                  & "   @NRS_BR_CD                                  AS NRS_BR_CD,                                                     " & vbNewLine _
    '                                  & "   NRS_BR.NRS_BR_NM                            AS NRS_BR_NM,                                                     " & vbNewLine _
    '                                  & "   CUST.CUST_CD_L                              AS CUST_CD_L,                                                     " & vbNewLine _
    '                                  & "   CUST.CUST_NM_L                              AS CUST_NM_L,                                                     " & vbNewLine _
    '                                  & "   @WH_CD                                      AS WH_CD,                                                         " & vbNewLine _
    '                                  & "   SOKO.WH_NM                                  AS WH_NM,                                                         " & vbNewLine _
    '                                  & "   GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST,                                                 " & vbNewLine _
    '                                  & "   GOODS.GOODS_NM_1                            AS GOODS_NM,                                                      " & vbNewLine _
    '                                  & "   GOODS.STD_IRIME_NB                          AS IRIME,                                                         " & vbNewLine _
    '                                  & "   @KAZU_KB                                    AS KAZU_KB,                                                       " & vbNewLine _
    '                                  & "   @GUI_IRIME                                  AS GUI_IRIME,                                                     " & vbNewLine _
    '                                  & "   (CASE WHEN OUTKAL.SYS_DEL_FLG = '1' THEN '消' WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB < '60' THEN" _
    '                                  & " 'ヨ' WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB >= '60' THEN 'サ' WHEN OUTKAM.ALCTD_KB <> '04 ' AND" _
    '                                  & " OUTKAL.OUTKA_STATE_KB < '60' THEN '予' ELSE '' END)AS YOJITU,                                                       " & vbNewLine _
    '                                  & "   (CASE WHEN RTRIM(OUTKAL.FURI_NO) = '' THEN '出荷' ELSE '振出' END) AS SYUBETU,                                  " & vbNewLine _
    '                                  & "   OUTKAL.OUTKA_PLAN_DATE AS PLAN_DATE,                                                                              " & vbNewLine _
    '                                  & "   0 AS INKA_KOSU,                                                                                                   " & vbNewLine _
    '                                  & "   0 AS INKA_SURYO,                                                                                                  " & vbNewLine _
    '                                  & "   OUTKAS.ALCTD_NB AS OUTKA_KOSU,                                                                                    " & vbNewLine _
    '                                  & "   OUTKAS.ALCTD_QT AS OUTKA_SURYO,                                                                                   " & vbNewLine _
    '                                  & "   0 AS ZAN_KOSU,                                                                                                    " & vbNewLine _
    '                                  & "   GOODS.NB_UT AS NB_UT,                                                                                             " & vbNewLine _
    '                                  & "   0 AS ZAN_SURYO,                                                                                                   " & vbNewLine _
    '                                  & "   GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                               " & vbNewLine _
    '                                  & "   OUTKAS.TOU_NO + '-' + OUTKAS.SITU_NO + '-' + OUTKAS.ZONE_CD + (CASE WHEN OUTKAS.LOCA = '' THEN '' ELSE '-' + " _
    '                                  & "   OUTKAS.LOCA END) AS OKIBA,                                                                                        " & vbNewLine _
    '                                  & "   OUTKAS.OUTKA_NO_L + '-' + OUTKAS.OUTKA_NO_M + '-' + OUTKAS.OUTKA_NO_S AS KANRI_NO,                                " & vbNewLine _
    '                                  & "   OUTKAS.OUTKA_NO_L AS KANRI_NO_L,                                                                                  " & vbNewLine _
    '                                  & "   OUTKAS.OUTKA_NO_M AS KANRI_NO_M,                                                                                  " & vbNewLine _
    '                                  & "   OUTKAS.OUTKA_NO_S AS KANRI_NO_S,                                                                                  " & vbNewLine _
    '                                  & "   OUTKAS.ZAI_REC_NO AS ZAI_REC_NO,                                                                                  " & vbNewLine _
    '                                  & "   CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.DEST_NM                                                                 " & vbNewLine _
    '                                  & "        ELSE OUTKAL.DEST_NM  END  AS DEST_NM,                                                                        " & vbNewLine _
    '                                  & "   CASE WHEN ISNULL(RTRIM(OUTKAM.CUST_ORD_NO_DTL), '')  = '' THEN OUTKAL.CUST_ORD_NO  ELSE OUTKAM.CUST_ORD_NO_DTL  END  AS ORD_NO,                " & vbNewLine _
    '                                  & "   CASE WHEN ISNULL(RTRIM(OUTKAM.BUYER_ORD_NO_DTL), '') = '' THEN OUTKAL.BUYER_ORD_NO ELSE OUTKAM.BUYER_ORD_NO_DTL END  AS BUYER_ORD_NO,          " & vbNewLine _
    '                                  & "   UNSOCO.UNSOCO_NM AS UNSOCO_NM,                                                                                    " & vbNewLine _
    '                                  & "   OUTKAS.REMARK AS REMARK,                                                                                          " & vbNewLine _
    '                                  & "   '' AS REMARK_OUT,                                                                                                 " & vbNewLine _
    '                                  & "   '' AS GOODS_COND_NM_1,                                                                                            " & vbNewLine _
    '                                  & "   '' AS GOODS_COND_NM_2,                                                                                            " & vbNewLine _
    '                                  & "   '' AS GOODS_COND_NM_3,                                                                                            " & vbNewLine _
    '                                  & "   '' AS OFB_KB_NM,                                                                                                  " & vbNewLine _
    '                                  & "   '' AS SPD_KB_NM,                                                                                                  " & vbNewLine _
    '                                  & "   '' AS ALLOC_PRIORITY_NM,                                                                                          " & vbNewLine _
    '                                  & "   '' AS DEST_CD_NM,                                                                                                 " & vbNewLine _
    '                                  & "   '' AS RSV_NO,                                                                                                     " & vbNewLine _
    '                                  & "   '2' AS SORT_KEY,                                                                                                   " & vbNewLine _
    '                                  & "   GOODS.GOODS_NM_1   AS GOODS_NM,                                                                           " & vbNewLine _
    '                                  & "   ZAITRS.LOT_NO      AS LOT_NO,                                                                           " & vbNewLine _
    '                                  & "   GOODS.STD_IRIME_NB AS IRIME                                                                                   " & vbNewLine _
    '                                  & "FROM $LM_TRN$..C_OUTKA_S AS OUTKAS                                                                                     " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_TRN$..C_OUTKA_M AS OUTKAM                                                                          " & vbNewLine _
    '                                  & "  ON OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                                                             " & vbNewLine _
    '                                  & "  AND OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                                                                          " & vbNewLine _
    '                                  & "  AND OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                                                                          " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_TRN$..C_OUTKA_L AS OUTKAL                                                                          " & vbNewLine _
    '                                  & "  ON OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                                             " & vbNewLine _
    '                                  & "  AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                                                          " & vbNewLine _
    '                                  & "LEFT OUTER JOIN                                                                                                      " & vbNewLine _
    '                                  & "   (SELECT                                                                                                           " & vbNewLine _
    '                                  & "      GOODS.PKG_NB,                                                                                                  " & vbNewLine _
    '                                  & "      GOODS.STD_IRIME_NB,                                                                                            " & vbNewLine _
    '                                  & "      GOODS.STD_IRIME_UT,                                                                                            " & vbNewLine _
    '                                  & "      GOODS.NB_UT,                                                                                                   " & vbNewLine _
    '                                  & "      GOODS.GOODS_NM_1,                                                                                              " & vbNewLine _
    '                                  & "      GOODS.NRS_BR_CD,                                                                                           " & vbNewLine _
    '                                  & "      GOODS.GOODS_CD_CUST,                                                                                       " & vbNewLine _
    '                                  & "      GOODS.CUST_CD_L,                                                                                           " & vbNewLine _
    '                                  & "      GOODS.CUST_CD_M,                                                                                           " & vbNewLine _
    '                                  & "      GOODS.CUST_CD_S,                                                                                           " & vbNewLine _
    '                                  & "      GOODS.CUST_CD_SS                                                                                           " & vbNewLine _
    '                                  & "   FROM $LM_MST$..M_GOODS AS GOODS                                                                                     " & vbNewLine _
    '                                  & "   WHERE GOODS.NRS_BR_CD = @NRS_BR_CD                                                                                  " & vbNewLine _
    '                                  & "     AND GOODS.GOODS_CD_NRS = @GOODS_CD_NRS)GOODS                                                                " & vbNewLine _
    '                                  & "  ON OUTKAS.NRS_BR_CD = GOODS.NRS_BR_CD                                                                              " & vbNewLine _
    '                                  & "   --荷主M                                                                                                      " & vbNewLine _
    '                                  & "   LEFT JOIN                                                                                                    " & vbNewLine _
    '                                  & "     (SELECT                                                                                                    " & vbNewLine _
    '                                  & "        CUST.CUST_CD_L,                                                                                         " & vbNewLine _
    '                                  & "        CUST.CUST_CD_M,                                                                                         " & vbNewLine _
    '                                  & "        CUST.CUST_CD_S,                                                                                         " & vbNewLine _
    '                                  & "        CUST.CUST_CD_SS,                                                                                        " & vbNewLine _
    '                                  & "        CUST.CUST_NM_L,                                                                                         " & vbNewLine _
    '                                  & "        CUST.NRS_BR_CD                                                                                          " & vbNewLine _
    '                                  & "     FROM $LM_MST$..M_CUST AS CUST                                                                                " & vbNewLine _
    '                                  & "     WHERE CUST.NRS_BR_CD    = @NRS_BR_CD   ) CUST                                                              " & vbNewLine _
    '                                  & "   ON                                                                                                           " & vbNewLine _
    '                                  & "      CUST.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "       GOODS.CUST_CD_L  = CUST.CUST_CD_L                                                                        " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "       GOODS.CUST_CD_M  = CUST.CUST_CD_M                                                                        " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "       GOODS.CUST_CD_S  = CUST.CUST_CD_S                                                                        " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "       GOODS.CUST_CD_SS = CUST.CUST_CD_SS                                                                       " & vbNewLine _
    '                                  & "   --営業所M                                                                                                    " & vbNewLine _
    '                                  & "   LEFT JOIN                                                                                                    " & vbNewLine _
    '                                  & "      $LM_MST$..M_NRS_BR AS NRS_BR                                                                                " & vbNewLine _
    '                                  & "   ON                                                                                                           " & vbNewLine _
    '                                  & "      NRS_BR.NRS_BR_CD = @NRS_BR_CD                                                                             " & vbNewLine _
    '                                  & "   --倉庫M                                                                                                      " & vbNewLine _
    '                                  & "   LEFT JOIN                                                                                                    " & vbNewLine _
    '                                  & "      $LM_MST$..M_SOKO AS SOKO                                                                                    " & vbNewLine _
    '                                  & "   ON                                                                                                           " & vbNewLine _
    '                                  & "      SOKO.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
    '                                  & "   AND                                                                                                          " & vbNewLine _
    '                                  & "      SOKO.WH_CD     = @WH_CD                                                                                   " & vbNewLine _
    '                                  & "   --商品コードでの荷主帳票パターン取得                                                                         " & vbNewLine _
    '                                  & "    LEFT JOIN                                                                                                   " & vbNewLine _
    '                                  & "     $LM_MST$..M_CUST_RPT MCR1                                                                                    " & vbNewLine _
    '                                  & "    ON                                                                                                          " & vbNewLine _
    '                                  & "     MCR1.NRS_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MCR1.CUST_CD_L  = GOODS.CUST_CD_L                                                                          " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MCR1.CUST_CD_M  = GOODS.CUST_CD_M                                                                          " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MCR1.CUST_CD_S = GOODS.CUST_CD_S                                                                           " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MCR1.PTN_ID    = @PTN_ID                                                                                   " & vbNewLine _
    '                                  & "    --帳票パターン取得                                                                                          " & vbNewLine _
    '                                  & "    LEFT JOIN                                                                                                   " & vbNewLine _
    '                                  & "     $LM_MST$..M_RPT MR1                                                                                          " & vbNewLine _
    '                                  & "    ON                                                                                                          " & vbNewLine _
    '                                  & "     MR1.NRS_BR_CD = @NRS_BR_CD                                                                                 " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MR1.PTN_ID    = MCR1.PTN_ID                                                                                " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MR1.PTN_CD    = MCR1.PTN_CD                                                                                " & vbNewLine _
    '                                  & "    --存在しない場合の帳票パターン取得                                                                          " & vbNewLine _
    '                                  & "    LEFT JOIN                                                                                                   " & vbNewLine _
    '                                  & "     $LM_MST$..M_RPT MR2                                                                                          " & vbNewLine _
    '                                  & "    ON                                                                                                          " & vbNewLine _
    '                                  & "     MR2.NRS_BR_CD     = @NRS_BR_CD                                                                             " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MR2.PTN_ID        = @PTN_ID                                                                                " & vbNewLine _
    '                                  & "    AND                                                                                                         " & vbNewLine _
    '                                  & "     MR2.STANDARD_FLAG = '01'                                                                                   " & vbNewLine _
    '                                  & "LEFT OUTER JOIN                                                                                                      " & vbNewLine _
    '                                  & "   (SELECT                                                                                                           " & vbNewLine _
    '                                  & "      ZAITRS.ZAI_REC_NO,                                                                                             " & vbNewLine _
    '                                  & "      ZAITRS.NRS_BR_CD,                                                                                              " & vbNewLine _
    '                                  & "      ZAITRS.TOU_NO,                                                                                                 " & vbNewLine _
    '                                  & "      ZAITRS.SITU_NO,                                                                                                " & vbNewLine _
    '                                  & "      ZAITRS.ZONE_CD,                                                                                                " & vbNewLine _
    '                                  & "      ZAITRS.LOCA,                                                                                                   " & vbNewLine _
    '                                  & "      ZAITRS.CUST_CD_L,                                                                                              " & vbNewLine _
    '                                  & "      ZAITRS.DEST_CD_P,                                                                                               " & vbNewLine _
    '                                  & "      ZAITRS.LOT_NO,                                                                                              " & vbNewLine _
    '                                  & "      ZAITRS.GOODS_CD_NRS                                                                                         " & vbNewLine _
    '                                  & "   FROM $LM_TRN$..D_ZAI_TRS AS ZAITRS                                                                                  " & vbNewLine _
    '                                  & "   WHERE ZAITRS.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
    '                                  & "   AND ZAITRS.INKA_NO_L  = @INKA_NO_L                                                                              " & vbNewLine _
    '                                  & "   AND ZAITRS.INKA_NO_M  = @INKA_NO_M                                                                              " & vbNewLine _
    '                                  & "   AND ZAITRS.INKA_NO_S  = @INKA_NO_S                                                                              " & vbNewLine _
    '                                  & "   AND ZAITRS.SYS_DEL_FLG = '0'                                                                                    " & vbNewLine _
    '                                  & "   ) ZAITRS                                                                              " & vbNewLine _
    '                                  & "  ON ZAITRS.NRS_BR_CD = OUTKAS.NRS_BR_CD                                                                             " & vbNewLine _
    '                                  & " AND ZAITRS.ZAI_REC_NO = OUTKAS.ZAI_REC_NO                                                                           " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST                                                                               " & vbNewLine _
    '                                  & "  ON OUTKAL.NRS_BR_CD = DEST.NRS_BR_CD                                                                               " & vbNewLine _
    '                                  & "  AND OUTKAL.CUST_CD_L = DEST.CUST_CD_L                                                                              " & vbNewLine _
    '                                  & "  AND OUTKAL.DEST_CD = DEST.DEST_CD                                                                                  " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_TRN$..F_UNSO_L AS UNSOL                                                                            " & vbNewLine _
    '                                  & "  ON OUTKAS.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                              " & vbNewLine _
    '                                  & "  AND OUTKAS.OUTKA_NO_L = UNSOL.INOUTKA_NO_L                                                                          " & vbNewLine _
    '                                  & "  AND UNSOL.MOTO_DATA_KB = '20'                                                                          " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO                                                                           " & vbNewLine _
    '                                  & "  ON UNSOL.NRS_BR_CD = UNSOCO.NRS_BR_CD                                                                              " & vbNewLine _
    '                                  & "  AND UNSOL.UNSO_CD = UNSOCO.UNSOCO_CD                                                                               " & vbNewLine _
    '                                  & "  AND UNSOL.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD                                                                         " & vbNewLine _
    '                                  & "LEFT OUTER JOIN $LM_MST$..M_FURI_GOODS AS FURI_GOODS                                                                 " & vbNewLine _
    '                                  & "  ON FURI_GOODS.NRS_BR_CD = ZAITRS.NRS_BR_CD                                                                         " & vbNewLine _
    '                                  & "  AND FURI_GOODS.CD_NRS = ZAITRS.GOODS_CD_NRS                                                                        " & vbNewLine _
    '                                  & "WHERE OUTKAM.NRS_BR_CD = @NRS_BR_CD                                                                                  " & vbNewLine
    'END YANAI 要望番号508
    ''' <summary>
    ''' 入出荷履歴（入荷ごと）タブ選択時セレクト文６
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_RIREKI6 As String = "UNION                                                                                              " & vbNewLine _
                                          & "SELECT                                                                                                               " & vbNewLine _
                                          & " CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                " & vbNewLine _
                                          & "      ELSE MR2.RPT_ID END                        AS RPT_ID,                                                      " & vbNewLine _
                                          & "   @NRS_BR_CD                                  AS NRS_BR_CD,                                                     " & vbNewLine _
                                          & "   NRS_BR.NRS_BR_NM                            AS NRS_BR_NM,                                                     " & vbNewLine _
                                          & "   CUST.CUST_CD_L                              AS CUST_CD_L,                                                     " & vbNewLine _
                                          & "   CUST.CUST_NM_L                              AS CUST_NM_L,                                                     " & vbNewLine _
                                          & "   @WH_CD                                      AS WH_CD,                                                         " & vbNewLine _
                                          & "   SOKO.WH_NM                                  AS WH_NM,                                                         " & vbNewLine _
                                          & "   GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST,                                                 " & vbNewLine _
                                          & "   GOODS.GOODS_NM_1                            AS GOODS_NM,                                                      " & vbNewLine _
                                          & "   GOODS.STD_IRIME_NB                          AS IRIME,                                                         " & vbNewLine _
                                          & "   @KAZU_KB                                    AS KAZU_KB,                                                       " & vbNewLine _
                                          & "   @GUI_IRIME                                  AS GUI_IRIME,                                                     " & vbNewLine _
                                          & "--   (CASE WHEN OUTKAL.SYS_DEL_FLG = '1' THEN '消' WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB < '60' THEN" _
                                          & "-- 'ヨ' WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB >= '60' THEN 'サ' WHEN OUTKAM.ALCTD_KB <> '04 ' AND" _
                                          & "-- OUTKAL.OUTKA_STATE_KB < '60' THEN '予' ELSE '' END)AS YOJITU,                                                       " & vbNewLine _
                                          & "--   (CASE WHEN RTRIM(OUTKAL.FURI_NO) = '' THEN '出荷' ELSE '振出' END) AS SYUBETU,                                  " & vbNewLine _
                                          & "   (CASE WHEN OUTKAL.SYS_DEL_FLG = '1' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K032' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0')                                  " & vbNewLine _
                                          & "         WHEN OUTKAM.SYS_DEL_FLG = '1' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K032' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0')                                  " & vbNewLine _
                                          & "         WHEN OUTKAS.SYS_DEL_FLG = '1' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K032' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0')                                  " & vbNewLine _
                                          & "         WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB < '60' THEN 'ヨ'                                                                                                                  " & vbNewLine _
                                          & "         WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB >= '60' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K033' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0')  " & vbNewLine _
                                          & "         WHEN OUTKAM.ALCTD_KB <> '04 ' AND OUTKAL.OUTKA_STATE_KB < '60' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K026' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') " & vbNewLine _
                                          & "         ELSE '' END)AS YOJITU,                                                                                                                                                                  " & vbNewLine _
                                          & "   (CASE WHEN RTRIM(OUTKAL.FURI_NO) = '' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K034' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') ELSE (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K035' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') END) AS SYUBETU,                                  " & vbNewLine _
                                          & "   OUTKAL.OUTKA_PLAN_DATE AS PLAN_DATE,                                                                              " & vbNewLine _
                                          & "   0 AS INKA_KOSU,                                                                                                   " & vbNewLine _
                                          & "   0 AS INKA_SURYO,                                                                                                  " & vbNewLine _
                                          & "   OUTKAS.ALCTD_NB AS OUTKA_KOSU,                                                                                    " & vbNewLine _
                                          & "   OUTKAS.ALCTD_QT AS OUTKA_SURYO,                                                                                   " & vbNewLine _
                                          & "   0 AS ZAN_KOSU,                                                                                                    " & vbNewLine _
                                          & "   GOODS.NB_UT AS NB_UT,                                                                                             " & vbNewLine _
                                          & "   0 AS ZAN_SURYO,                                                                                                   " & vbNewLine _
                                          & "   GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                               " & vbNewLine _
                                          & "   OUTKAS.TOU_NO + '-' + OUTKAS.SITU_NO + '-' + OUTKAS.ZONE_CD + (CASE WHEN OUTKAS.LOCA = '' THEN '' ELSE '-' + " _
                                          & "   OUTKAS.LOCA END) AS OKIBA,                                                                                        " & vbNewLine _
                                          & "   OUTKAS.OUTKA_NO_L + '-' + OUTKAS.OUTKA_NO_M + '-' + OUTKAS.OUTKA_NO_S AS KANRI_NO,                                " & vbNewLine _
                                          & "   OUTKAS.OUTKA_NO_L AS KANRI_NO_L,                                                                                  " & vbNewLine _
                                          & "   OUTKAS.OUTKA_NO_M AS KANRI_NO_M,                                                                                  " & vbNewLine _
                                          & "   OUTKAS.OUTKA_NO_S AS KANRI_NO_S,                                                                                  " & vbNewLine _
                                          & "   OUTKAS.ZAI_REC_NO AS ZAI_REC_NO,                                                                                  " & vbNewLine _
                                          & "   CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.DEST_NM                                                                 " & vbNewLine _
                                          & "        ELSE OUTKAL.DEST_NM  END  AS DEST_NM,                                                                        " & vbNewLine _
                                          & "   CASE WHEN ISNULL(RTRIM(OUTKAM.CUST_ORD_NO_DTL), '')  = '' THEN OUTKAL.CUST_ORD_NO  ELSE OUTKAM.CUST_ORD_NO_DTL  END  AS ORD_NO,                " & vbNewLine _
                                          & "   CASE WHEN ISNULL(RTRIM(OUTKAM.BUYER_ORD_NO_DTL), '') = '' THEN OUTKAL.BUYER_ORD_NO ELSE OUTKAM.BUYER_ORD_NO_DTL END  AS BUYER_ORD_NO,          " & vbNewLine _
                                          & "   UNSOCO.UNSOCO_NM AS UNSOCO_NM,                                                                                    " & vbNewLine _
                                          & "   OUTKAS.REMARK AS REMARK,                                                                                          " & vbNewLine _
                                          & "   '' AS REMARK_OUT,                                                                                                 " & vbNewLine _
                                          & "   '' AS GOODS_COND_NM_1,                                                                                            " & vbNewLine _
                                          & "   '' AS GOODS_COND_NM_2,                                                                                            " & vbNewLine _
                                          & "   '' AS GOODS_COND_NM_3,                                                                                            " & vbNewLine _
                                          & "   '' AS OFB_KB_NM,                                                                                                  " & vbNewLine _
                                          & "   '' AS SPD_KB_NM,                                                                                                  " & vbNewLine _
                                          & "   '' AS ALLOC_PRIORITY_NM,                                                                                          " & vbNewLine _
                                          & "   '' AS DEST_CD_NM,                                                                                                 " & vbNewLine _
                                          & "   '' AS RSV_NO,                                                                                                     " & vbNewLine _
                                          & "   '2' AS SORT_KEY,                                                                                                  " & vbNewLine _
                                          & "   GOODS.GOODS_NM_1   AS GOODS_NM,                                                                                   " & vbNewLine _
                                          & "   ZAITRS.LOT_NO      AS LOT_NO,                                                                                     " & vbNewLine _
                                          & "   GOODS.STD_IRIME_NB AS IRIME                                                                                       " & vbNewLine _
                                          & " -- 1888_WIT_ロケーション変更強化対応                                                                                " & vbNewLine _
                                          & "  , ISNULL(TRS_UPD_USER.USER_NM, '') AS ZAI_TRS_UPD_USER_NM                                                          " & vbNewLine _
                                          & "FROM $LM_TRN$..C_OUTKA_S AS OUTKAS                                                                                   " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_TRN$..C_OUTKA_M AS OUTKAM                                                                        " & vbNewLine _
                                          & "  ON OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                                                             " & vbNewLine _
                                          & "  AND OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                                                                          " & vbNewLine _
                                          & "  AND OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                                                                          " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_TRN$..C_OUTKA_L AS OUTKAL                                                                          " & vbNewLine _
                                          & "  ON OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                                             " & vbNewLine _
                                          & "  AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                                                          " & vbNewLine _
                                          & "LEFT OUTER JOIN                                                                                                      " & vbNewLine _
                                          & "   (SELECT                                                                                                           " & vbNewLine _
                                          & "      ISNULL(GOODS.PKG_NB,0) AS PKG_NB,                                                                              " & vbNewLine _
                                          & "      GOODS.STD_IRIME_NB,                                                                                            " & vbNewLine _
                                          & "      GOODS.STD_IRIME_UT,                                                                                            " & vbNewLine _
                                          & "      GOODS.NB_UT,                                                                                                   " & vbNewLine _
                                          & "      GOODS.GOODS_NM_1,                                                                                              " & vbNewLine _
                                          & "      GOODS.NRS_BR_CD,                                                                                           " & vbNewLine _
                                          & "      GOODS.GOODS_CD_CUST,                                                                                       " & vbNewLine _
                                          & "      GOODS.CUST_CD_L,                                                                                           " & vbNewLine _
                                          & "      GOODS.CUST_CD_M,                                                                                           " & vbNewLine _
                                          & "      GOODS.CUST_CD_S,                                                                                           " & vbNewLine _
                                          & "      GOODS.CUST_CD_SS                                                                                           " & vbNewLine _
                                          & "   FROM $LM_MST$..M_GOODS AS GOODS                                                                                     " & vbNewLine _
                                          & "   WHERE GOODS.NRS_BR_CD = @NRS_BR_CD                                                                                  " & vbNewLine _
                                          & "     AND GOODS.GOODS_CD_NRS = @GOODS_CD_NRS)GOODS                                                                " & vbNewLine _
                                          & "  ON OUTKAS.NRS_BR_CD = GOODS.NRS_BR_CD                                                                              " & vbNewLine _
                                          & "   --荷主M                                                                                                      " & vbNewLine _
                                          & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                          & "     (SELECT                                                                                                    " & vbNewLine _
                                          & "        CUST.CUST_CD_L,                                                                                         " & vbNewLine _
                                          & "        CUST.CUST_CD_M,                                                                                         " & vbNewLine _
                                          & "        CUST.CUST_CD_S,                                                                                         " & vbNewLine _
                                          & "        CUST.CUST_CD_SS,                                                                                        " & vbNewLine _
                                          & "        CUST.CUST_NM_L,                                                                                         " & vbNewLine _
                                          & "        CUST.NRS_BR_CD                                                                                          " & vbNewLine _
                                          & "     FROM $LM_MST$..M_CUST AS CUST                                                                                " & vbNewLine _
                                          & "     WHERE CUST.NRS_BR_CD    = @NRS_BR_CD   ) CUST                                                              " & vbNewLine _
                                          & "   ON                                                                                                           " & vbNewLine _
                                          & "      CUST.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
                                          & "   AND                                                                                                          " & vbNewLine _
                                          & "       GOODS.CUST_CD_L  = CUST.CUST_CD_L                                                                        " & vbNewLine _
                                          & "   AND                                                                                                          " & vbNewLine _
                                          & "       GOODS.CUST_CD_M  = CUST.CUST_CD_M                                                                        " & vbNewLine _
                                          & "   AND                                                                                                          " & vbNewLine _
                                          & "       GOODS.CUST_CD_S  = CUST.CUST_CD_S                                                                        " & vbNewLine _
                                          & "   AND                                                                                                          " & vbNewLine _
                                          & "       GOODS.CUST_CD_SS = CUST.CUST_CD_SS                                                                       " & vbNewLine _
                                          & "   --営業所M                                                                                                    " & vbNewLine _
                                          & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                          & "      $LM_MST$..M_NRS_BR AS NRS_BR                                                                                " & vbNewLine _
                                          & "   ON                                                                                                           " & vbNewLine _
                                          & "      NRS_BR.NRS_BR_CD = @NRS_BR_CD                                                                             " & vbNewLine _
                                          & "   --倉庫M                                                                                                      " & vbNewLine _
                                          & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                          & "      $LM_MST$..M_SOKO AS SOKO                                                                                    " & vbNewLine _
                                          & "   ON                                                                                                           " & vbNewLine _
                                          & "      SOKO.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
                                          & "   AND                                                                                                          " & vbNewLine _
                                          & "      SOKO.WH_CD     = @WH_CD                                                                                   " & vbNewLine _
                                          & "   --商品コードでの荷主帳票パターン取得                                                                         " & vbNewLine _
                                          & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                          & "     $LM_MST$..M_CUST_RPT MCR1                                                                                    " & vbNewLine _
                                          & "    ON                                                                                                          " & vbNewLine _
                                          & "     MCR1.NRS_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MCR1.CUST_CD_L  = GOODS.CUST_CD_L                                                                          " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MCR1.CUST_CD_M  = GOODS.CUST_CD_M                                                                          " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MCR1.CUST_CD_S = GOODS.CUST_CD_S                                                                           " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MCR1.PTN_ID    = @PTN_ID                                                                                   " & vbNewLine _
                                          & "    --帳票パターン取得                                                                                          " & vbNewLine _
                                          & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                          & "     $LM_MST$..M_RPT MR1                                                                                          " & vbNewLine _
                                          & "    ON                                                                                                          " & vbNewLine _
                                          & "     MR1.NRS_BR_CD = @NRS_BR_CD                                                                                 " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MR1.PTN_ID    = MCR1.PTN_ID                                                                                " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MR1.PTN_CD    = MCR1.PTN_CD                                                                                " & vbNewLine _
                                          & "    --存在しない場合の帳票パターン取得                                                                          " & vbNewLine _
                                          & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                          & "     $LM_MST$..M_RPT MR2                                                                                          " & vbNewLine _
                                          & "    ON                                                                                                          " & vbNewLine _
                                          & "     MR2.NRS_BR_CD     = @NRS_BR_CD                                                                             " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MR2.PTN_ID        = @PTN_ID                                                                                " & vbNewLine _
                                          & "    AND                                                                                                         " & vbNewLine _
                                          & "     MR2.STANDARD_FLAG = '01'                                                                                   " & vbNewLine _
                                          & "LEFT OUTER JOIN                                                                                                      " & vbNewLine _
                                          & "   (SELECT                                                                                                           " & vbNewLine _
                                          & "      ZAITRS.ZAI_REC_NO,                                                                                             " & vbNewLine _
                                          & "      ZAITRS.NRS_BR_CD,                                                                                              " & vbNewLine _
                                          & "      ZAITRS.TOU_NO,                                                                                                 " & vbNewLine _
                                          & "      ZAITRS.SITU_NO,                                                                                                " & vbNewLine _
                                          & "      ZAITRS.ZONE_CD,                                                                                                " & vbNewLine _
                                          & "      ZAITRS.LOCA,                                                                                                   " & vbNewLine _
                                          & "      ZAITRS.CUST_CD_L,                                                                                              " & vbNewLine _
                                          & "      ZAITRS.DEST_CD_P,                                                                                          " & vbNewLine _
                                          & "      ZAITRS.LOT_NO,                                                                                             " & vbNewLine _
                                          & "      ZAITRS.GOODS_CD_NRS,                                                                                       " & vbNewLine _
                                          & "      ZAITRS.SMPL_FLAG                                                                                           " & vbNewLine _
                                          & " -- 1888_WIT_ロケーション変更強化対応                                                                            " & vbNewLine _
                                          & "    , ZAITRS.SYS_UPD_USER                                                                                        " & vbNewLine _
                                          & "   FROM $LM_TRN$..D_ZAI_TRS AS ZAITRS                                                                                  " & vbNewLine _
                                          & "   WHERE ZAITRS.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
                                          & "   AND ZAITRS.GOODS_CD_NRS = @GOODS_CD_NRS                                                                               " & vbNewLine _
                                          & "   AND ZAITRS.SYS_DEL_FLG = '0'                                                                                    " & vbNewLine

    Private Const SQL_SELECT_DATA_RIREKI6_2 As String = "   ) ZAITRS                                                                                              " & vbNewLine _
                                          & "  ON ZAITRS.NRS_BR_CD = OUTKAS.NRS_BR_CD                                                                             " & vbNewLine _
                                          & " AND ZAITRS.ZAI_REC_NO = OUTKAS.ZAI_REC_NO                                                                           " & vbNewLine _
                                          & " -- 1888_WIT_ロケーション変更強化対応                                                                                " & vbNewLine _
                                          & " LEFT OUTER JOIN $LM_MST$..S_USER AS TRS_UPD_USER                                                                    " & vbNewLine _
                                          & "   ON ZAITRS.SYS_UPD_USER = TRS_UPD_USER.USER_CD                                                                     " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST                                                                             " & vbNewLine _
                                          & "  ON OUTKAL.NRS_BR_CD = DEST.NRS_BR_CD                                                                               " & vbNewLine _
                                          & "  AND OUTKAL.CUST_CD_L = DEST.CUST_CD_L                                                                              " & vbNewLine _
                                          & "  AND OUTKAL.DEST_CD = DEST.DEST_CD                                                                                  " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_TRN$..F_UNSO_L AS UNSOL                                                                          " & vbNewLine _
                                          & "  ON OUTKAS.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                              " & vbNewLine _
                                          & "  AND OUTKAS.OUTKA_NO_L = UNSOL.INOUTKA_NO_L                                                                         " & vbNewLine _
                                          & "  AND UNSOL.MOTO_DATA_KB = '20'                                                                          " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO                                                                           " & vbNewLine _
                                          & "  ON UNSOL.NRS_BR_CD = UNSOCO.NRS_BR_CD                                                                              " & vbNewLine _
                                          & "  AND UNSOL.UNSO_CD = UNSOCO.UNSOCO_CD                                                                               " & vbNewLine _
                                          & "  AND UNSOL.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD                                                                         " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..M_FURI_GOODS AS FURI_GOODS                                                                 " & vbNewLine _
                                          & "  ON FURI_GOODS.NRS_BR_CD = ZAITRS.NRS_BR_CD                                                                         " & vbNewLine _
                                          & "  AND FURI_GOODS.CD_NRS = ZAITRS.GOODS_CD_NRS                                                                        " & vbNewLine _
                                          & "WHERE OUTKAM.NRS_BR_CD = @NRS_BR_CD                                                                                  " & vbNewLine
    'END YANAI 20110913 小分け対応

    ''' <summary>
    ''' 入出荷履歴（入荷ごと）タブ選択時セレクト文７
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_RIREKI7 As String = "  AND OUTKAS.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
                                      & "  AND OUTKAM.SYS_DEL_FLG = '0'                                                                                     " & vbNewLine


    ''' <summary>
    ''' 入出荷履歴（入荷ごと）タブ選択時ORDERBY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDERBY_DATA_RIREKI As String = "ORDER BY PLAN_DATE,SORT_KEY,KANRI_NO,ZAI_REC_NO"

#End Region

#Region "入出荷履歴（在庫）"

    ''' <summary>
    ''' 入出荷タブ（在庫ごと）選択時SQL
    ''' </summary>
    ''' <remarks></remarks>
Private Const SQL_DATA_RIREKI_ZAI As String = "SELECT                                                                                                 " & vbNewLine _
                                      & " CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                " & vbNewLine _
                                      & "      ELSE MR2.RPT_ID END                        AS RPT_ID,                                                      " & vbNewLine _
                                      & "   @NRS_BR_CD                                  AS NRS_BR_CD,                                                     " & vbNewLine _
                                      & "   NRS_BR.NRS_BR_NM                            AS NRS_BR_NM,                                                     " & vbNewLine _
                                      & "   CUST.CUST_CD_L                              AS CUST_CD_L,                                                     " & vbNewLine _
                                      & "   CUST.CUST_NM_L                              AS CUST_NM_L,                                                     " & vbNewLine _
                                      & "   @WH_CD                                      AS WH_CD,                                                         " & vbNewLine _
                                      & "   SOKO.WH_NM                                  AS WH_NM,                                                         " & vbNewLine _
                                      & "   GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST,                                                 " & vbNewLine _
                                      & "   GOODS.GOODS_NM_1                            AS GOODS_NM,                                                      " & vbNewLine _
                                      & "   GOODS.STD_IRIME_NB                          AS IRIME,                                                         " & vbNewLine _
                                      & "   @KAZU_KB                                    AS KAZU_KB,                                                       " & vbNewLine _
                                      & "   @GUI_IRIME                                  AS GUI_IRIME,                                                     " & vbNewLine _
                                      & "   '' AS YOJITU,                                                                                                 " & vbNewLine _
                                      & "--   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN '移出' WHEN IDOTRS.N_ZAI_REC_NO = @ZAI_REC_NO THEN '移入'" _
                                      & "-- ELSE '' END) AS SYUBETU,                                                                                        " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K030' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') WHEN IDOTRS.N_ZAI_REC_NO = @ZAI_REC_NO THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K031' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0')" _
                                      & " ELSE '' END) AS SYUBETU,                                                                                        " & vbNewLine _                                      
                                      & "   IDOTRS.IDO_DATE AS PLAN_DATE,                                                                                 " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN 0 WHEN IDOTRS.N_ZAI_REC_NO = @ZAI_REC_NO THEN" _
                                      & " IDOTRS.N_PORA_ZAI_NB ELSE 0 END) AS INKA_KOSU,                                                                  " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN 0 WHEN IDOTRS.N_ZAI_REC_NO = @ZAI_REC_NO THEN (CASE WHEN ZAITRS_N.SMPL_FLAG = '01' THEN OUTKA_S_O.ALCTD_NB * OUTKA_S_O.IRIME ELSE IDOTRS.N_PORA_ZAI_NB * GOODS.STD_IRIME_NB END)ELSE 0 END) AS INKA_SURYO, " _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN (CASE WHEN ZAITRS_N.SMPL_FLAG = '01' THEN 1 ELSE IDOTRS.N_PORA_ZAI_NB END) WHEN IDOTRS.N_ZAI_REC_NO =" _
                                      & " @ZAI_REC_NO THEN  0 ELSE 0 END) AS OUTKA_KOSU,                                                                  " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN (CASE WHEN ZAITRS_N.SMPL_FLAG = '01' THEN ZAITRS_O.IRIME ELSE IDOTRS.N_PORA_ZAI_NB * GOODS.STD_IRIME_NB END) WHEN" _
                                      & " IDOTRS.N_ZAI_REC_NO = @ZAI_REC_NO THEN 0 ELSE 0 END) AS OUTKA_SURYO,                                            " & vbNewLine _
                                      & "   0 AS ZAN_KOSU,                                                                                                " & vbNewLine _
                                      & "   GOODS.NB_UT AS NB_UT,                                                                                         " & vbNewLine _
                                      & "   0 AS ZAN_SURYO,                                                                                               " & vbNewLine _
                                      & "   GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                           " & vbNewLine _
                                      & "   ZAITRS_N.TOU_NO + '-' + ZAITRS_N.SITU_NO + '-' + ZAITRS_N.ZONE_CD + (CASE WHEN ZAITRS_N.LOCA = '' THEN '' ELSE " _
                                      & "'-' + ZAITRS_N.LOCA END) AS OKIBA,                                                                                 " & vbNewLine _
                                      & "   ZAITRS_N.INKA_NO_L + '-' + ZAITRS_N.INKA_NO_M + '-' + ZAITRS_N.INKA_NO_S AS KANRI_NO,                               " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN IDOTRS.N_ZAI_REC_NO WHEN IDOTRS.N_ZAI_REC_NO =" _
                                      & " @ZAI_REC_NO THEN IDOTRS.O_ZAI_REC_NO ELSE '' END) AS ZAI_REC_NO,                                                " & vbNewLine _
                                      & "   KBN1.KBN_NM1 AS DEST_NM,                                                                                      " & vbNewLine _
                                      & "   '' AS ORD_NO,                                                                                                 " & vbNewLine _
                                      & "   '' AS BUYER_ORD_NO,                                                                                           " & vbNewLine _
                                      & "   '' AS UNSOCO_NM,                                                                                              " & vbNewLine _
                                      & "   IDOTRS.REMARK AS REMARK,                                                                                      " & vbNewLine _
                                      & "   '' AS REMARK_OUT,                                                                                             " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN '' WHEN IDOTRS.N_ZAI_REC_NO =" _
                                      & " @ZAI_REC_NO THEN KBN2.KBN_NM1 ELSE '' END) AS GOODS_COND_NM_1,                                                  " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN '' WHEN IDOTRS.N_ZAI_REC_NO =" _
                                      & " @ZAI_REC_NO THEN KBN3.KBN_NM1 ELSE '' END) AS GOODS_COND_NM_2,                                                  " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN '' WHEN IDOTRS.N_ZAI_REC_NO =" _
                                      & " @ZAI_REC_NO THEN CUSTCOND.JOTAI_NM ELSE '' END) AS GOODS_COND_NM_3,                                             " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN '' WHEN IDOTRS.N_ZAI_REC_NO =" _
                                      & " @ZAI_REC_NO THEN KBN4.KBN_NM1 ELSE '' END) AS OFB_KB_NM,                                                        " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN '' WHEN IDOTRS.N_ZAI_REC_NO =" _
                                      & " @ZAI_REC_NO THEN KBN5.KBN_NM1 ELSE '' END) AS SPD_KB_NM,                                                        " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN '' WHEN IDOTRS.N_ZAI_REC_NO =" _
                                      & " @ZAI_REC_NO THEN KBN6.KBN_NM1 ELSE '' END) AS ALLOC_PRIORITY_NM,                                                " & vbNewLine _
                                      & "   DEST.DEST_NM AS DEST_CD_NM,                                                                                   " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN '' WHEN IDOTRS.N_ZAI_REC_NO =" _
                                      & " @ZAI_REC_NO THEN ZAITRS_N.RSV_NO ELSE '' END) AS RSV_NO,                                                          " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN '3' WHEN IDOTRS.N_ZAI_REC_NO =" _
                                      & " @ZAI_REC_NO THEN '2' ELSE '' END) AS SORT_KEY,                                                                  " & vbNewLine _
                                      & "   ZAITRS_N.INKA_NO_L AS KANRI_NO_L,                                                                                " & vbNewLine _
                                      & "   ZAITRS_N.INKA_NO_M AS KANRI_NO_M,                                                                                " & vbNewLine _
                                      & "   ZAITRS_N.INKA_NO_S AS KANRI_NO_S,                                                                                 " & vbNewLine _
                                      & "   GOODS.GOODS_NM_1    AS GOODS_NM,                                                                              " & vbNewLine _
                                      & "   (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN '' WHEN IDOTRS.N_ZAI_REC_NO =" _
                                      & " @ZAI_REC_NO THEN ZAITRS_N.LOT_NO ELSE '' END) AS LOT_NO,                                                          " & vbNewLine _
                                      & "   GOODS.STD_IRIME_NB  AS IRIME                                                                          " & vbNewLine _
                                      & " -- 1888_WIT_ロケーション変更強化対応 20160822 " & vbNewLine _
                                      & " , ISNULL(TRS_UPD_USER.USER_NM, '') AS ZAI_TRS_UPD_USER_NM                                                       " & vbNewLine _
                                      & "FROM $LM_TRN$..D_IDO_TRS AS IDOTRS                                                                               " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "   (SELECT                                                                                                       " & vbNewLine _
                                      & "       GOODS.NRS_BR_CD,                                                                                          " & vbNewLine _
                                      & "       GOODS.STD_IRIME_NB,                                                                                       " & vbNewLine _
                                      & "       GOODS.STD_IRIME_UT,                                                                                       " & vbNewLine _
                                      & "       GOODS.NB_UT,                                                                                               " & vbNewLine _
                                      & "       GOODS.GOODS_NM_1,                                                                                          " & vbNewLine _
                                      & "       GOODS.GOODS_CD_CUST,                                                                                       " & vbNewLine _
                                      & "       GOODS.CUST_CD_L,                                                                                           " & vbNewLine _
                                      & "       GOODS.CUST_CD_M,                                                                                           " & vbNewLine _
                                      & "       GOODS.CUST_CD_S,                                                                                           " & vbNewLine _
                                      & "       GOODS.CUST_CD_SS                                                                                           " & vbNewLine _
                                      & "    FROM $LM_MST$..M_GOODS AS GOODS                                                                              " & vbNewLine _
                                      & "    WHERE GOODS.GOODS_CD_NRS = @GOODS_CD_NRS) GOODS                                                              " & vbNewLine _
                                      & "  ON IDOTRS.NRS_BR_CD = GOODS.NRS_BR_CD                                                                          " & vbNewLine _
                                      & "   --荷主M                                                                                                      " & vbNewLine _
                                      & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                      & "     (SELECT                                                                                                    " & vbNewLine _
                                      & "        CUST.CUST_CD_L,                                                                                         " & vbNewLine _
                                      & "        CUST.CUST_CD_M,                                                                                         " & vbNewLine _
                                      & "        CUST.CUST_CD_S,                                                                                         " & vbNewLine _
                                      & "        CUST.CUST_CD_SS,                                                                                        " & vbNewLine _
                                      & "        CUST.CUST_NM_L,                                                                                         " & vbNewLine _
                                      & "        CUST.NRS_BR_CD                                                                                          " & vbNewLine _
                                      & "     FROM $LM_MST$..M_CUST AS CUST                                                                                " & vbNewLine _
                                      & "     WHERE CUST.NRS_BR_CD    = @NRS_BR_CD   ) CUST                                                              " & vbNewLine _
                                      & "   ON                                                                                                           " & vbNewLine _
                                      & "      CUST.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "       GOODS.CUST_CD_L  = CUST.CUST_CD_L                                                                        " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "       GOODS.CUST_CD_M  = CUST.CUST_CD_M                                                                        " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "       GOODS.CUST_CD_S  = CUST.CUST_CD_S                                                                        " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "       GOODS.CUST_CD_SS = CUST.CUST_CD_SS                                                                       " & vbNewLine _
                                      & "   --営業所M                                                                                                    " & vbNewLine _
                                      & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                      & "      $LM_MST$..M_NRS_BR AS NRS_BR                                                                                " & vbNewLine _
                                      & "   ON                                                                                                           " & vbNewLine _
                                      & "      NRS_BR.NRS_BR_CD = @NRS_BR_CD                                                                             " & vbNewLine _
                                      & "   --倉庫M                                                                                                      " & vbNewLine _
                                      & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                      & "      $LM_MST$..M_SOKO AS SOKO                                                                                    " & vbNewLine _
                                      & "   ON                                                                                                           " & vbNewLine _
                                      & "      SOKO.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "      SOKO.WH_CD     = @WH_CD                                                                                   " & vbNewLine _
                                      & "   --商品コードでの荷主帳票パターン取得                                                                         " & vbNewLine _
                                      & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                      & "     $LM_MST$..M_CUST_RPT MCR1                                                                                    " & vbNewLine _
                                      & "    ON                                                                                                          " & vbNewLine _
                                      & "     MCR1.NRS_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MCR1.CUST_CD_L  = GOODS.CUST_CD_L                                                                          " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MCR1.CUST_CD_M  = GOODS.CUST_CD_M                                                                          " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MCR1.CUST_CD_S = GOODS.CUST_CD_S                                                                           " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MCR1.PTN_ID    = @PTN_ID                                                                                   " & vbNewLine _
                                      & "    --帳票パターン取得                                                                                          " & vbNewLine _
                                      & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                      & "     $LM_MST$..M_RPT MR1                                                                                          " & vbNewLine _
                                      & "    ON                                                                                                          " & vbNewLine _
                                      & "     MR1.NRS_BR_CD = @NRS_BR_CD                                                                                 " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MR1.PTN_ID    = MCR1.PTN_ID                                                                                " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MR1.PTN_CD    = MCR1.PTN_CD                                                                                " & vbNewLine _
                                      & "    --存在しない場合の帳票パターン取得                                                                          " & vbNewLine _
                                      & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                      & "     $LM_MST$..M_RPT MR2                                                                                          " & vbNewLine _
                                      & "    ON                                                                                                          " & vbNewLine _
                                      & "     MR2.NRS_BR_CD     = @NRS_BR_CD                                                                             " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MR2.PTN_ID        = @PTN_ID                                                                                " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MR2.STANDARD_FLAG = '01'                                                                                   " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "     $LM_TRN$..D_ZAI_TRS AS ZAITRS_O                                                                          " & vbNewLine _
                                      & "  ON IDOTRS.NRS_BR_CD = ZAITRS_O.NRS_BR_CD                                                                         " & vbNewLine _
                                      & "  AND IDOTRS.O_ZAI_REC_NO = ZAITRS_O.ZAI_REC_NO                                                                         " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "     $LM_TRN$..D_ZAI_TRS AS ZAITRS_N                                                                          " & vbNewLine _
                                      & "  ON IDOTRS.NRS_BR_CD = ZAITRS_N.NRS_BR_CD                                                                         " & vbNewLine _
                                      & "  AND IDOTRS.N_ZAI_REC_NO = ZAITRS_N.ZAI_REC_NO                                                               " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_MST$..S_USER AS TRS_UPD_USER                                                             " & vbNewLine _
                                      & "   ON IDOTRS.SYS_UPD_USER = TRS_UPD_USER.USER_CD                                                              " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "     $LM_TRN$..C_OUTKA_S AS OUTKA_S                                                                          " & vbNewLine _
                                      & "  ON OUTKA_S.NRS_BR_CD = ZAITRS_N.NRS_BR_CD                                                                         " & vbNewLine _
                                      & "  AND OUTKA_S.ZAI_REC_NO = ZAITRS_N.ZAI_REC_NO                                                               " & vbNewLine _
                                      & "  AND OUTKA_S.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "     $LM_TRN$..C_OUTKA_S AS OUTKA_S_O                                                                        " & vbNewLine _
                                      & "  ON OUTKA_S_O.NRS_BR_CD = ZAITRS_O.NRS_BR_CD                                                                         " & vbNewLine _
                                      & "  AND OUTKA_S_O.ZAI_REC_NO = ZAITRS_O.ZAI_REC_NO                                                               " & vbNewLine _
                                      & "  AND OUTKA_S_O.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN1.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN1.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                      
                                      & "     FROM $LM_MST$..Z_KBN AS KBN1                                                                                " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN1.KBN_GROUP_CD = 'I002')KBN1                                                                          " & vbNewLine _
                                      & "  ON IDOTRS.REMARK_KBN = KBN1.KBN_CD                                                                             " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN2.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN2.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                            
                                      & "     FROM $LM_MST$..Z_KBN AS KBN2                                                                                " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN2.KBN_GROUP_CD = 'S005')KBN2                                                                          " & vbNewLine _
                                      & "  ON ZAITRS_N.GOODS_COND_KB_1 = KBN2.KBN_CD                                                                        " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN3.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN3.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                            
                                      & "     FROM $LM_MST$..Z_KBN AS KBN3                                                                                " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN3.KBN_GROUP_CD = 'S006')KBN3                                                                          " & vbNewLine _
                                      & "  ON ZAITRS_N.GOODS_COND_KB_2 = KBN3.KBN_CD                                                                        " & vbNewLine _
                                      & "LEFT OUTER JOIN $LM_MST$..M_CUSTCOND AS CUSTCOND                                                                 " & vbNewLine _
                                      & "   ON ZAITRS_N.NRS_BR_CD = CUSTCOND.NRS_BR_CD                                                                      " & vbNewLine _
                                      & "   AND ZAITRS_N.CUST_CD_L = CUSTCOND.CUST_CD_L                                                                     " & vbNewLine _
                                      & "   AND ZAITRS_N.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD                                                                " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN4.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN4.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                            
                                      & "     FROM $LM_MST$..Z_KBN AS KBN4                                                                                  " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN4.KBN_GROUP_CD = 'B002')KBN4                                                                          " & vbNewLine _
                                      & "  ON ZAITRS_N.OFB_KB = KBN4.KBN_CD                                                                                 " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN5.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN5.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                            
                                      & "     FROM $LM_MST$..Z_KBN AS KBN5                                                                                  " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN5.KBN_GROUP_CD = 'H003')KBN5                                                                          " & vbNewLine _
                                      & "  ON ZAITRS_N.SPD_KB = KBN5.KBN_CD                                                                                 " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN6.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN6.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                                                                  
                                      & "     FROM $LM_MST$..Z_KBN AS KBN6                                                                                  " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN6.KBN_GROUP_CD = 'W001')KBN6                                                                          " & vbNewLine _
                                      & "  ON ZAITRS_N.ALLOC_PRIORITY = KBN6.KBN_CD                                                                         " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST                                                                          " & vbNewLine _
                                      & "  ON ZAITRS_N.NRS_BR_CD = DEST.NRS_BR_CD                                                                           " & vbNewLine _
                                      & "  AND ZAITRS_O.CUST_CD_L = DEST.CUST_CD_L                                                                          " & vbNewLine _
                                      & "  AND ZAITRS_N.DEST_CD_P = DEST.DEST_CD                                                                            " & vbNewLine _
                                      & "WHERE                                                                                                            " & vbNewLine _
                                      & "  IDOTRS.NRS_BR_CD = @NRS_BR_CD                                                                                  " & vbNewLine _
                                      & "  AND (IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO                                                                         " & vbNewLine _
                                      & "       OR IDOTRS.N_ZAI_REC_NO = @ZAI_REC_NO)                                                                     " & vbNewLine _
                                      & "  AND IDOTRS.SYS_DEL_FLG = '0'                                                                                   " & vbNewLine _
                                      & "UNION                                                                                                            " & vbNewLine _
                                      & "SELECT                                                                                                           " & vbNewLine _
                                      & " CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                " & vbNewLine _
                                      & "      ELSE MR2.RPT_ID END                        AS RPT_ID,                                                      " & vbNewLine _
                                      & "   @NRS_BR_CD                                  AS NRS_BR_CD,                                                     " & vbNewLine _
                                      & "   NRS_BR.NRS_BR_NM                            AS NRS_BR_NM,                                                     " & vbNewLine _
                                      & "   CUST.CUST_CD_L                              AS CUST_CD_L,                                                     " & vbNewLine _
                                      & "   CUST.CUST_NM_L                              AS CUST_NM_L,                                                     " & vbNewLine _
                                      & "   @WH_CD                                      AS WH_CD,                                                         " & vbNewLine _
                                      & "   SOKO.WH_NM                                  AS WH_NM,                                                         " & vbNewLine _
                                      & "   GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST,                                                 " & vbNewLine _
                                      & "   GOODS.GOODS_NM_1                            AS GOODS_NM,                                                      " & vbNewLine _
                                      & "   GOODS.STD_IRIME_NB                          AS IRIME,                                                         " & vbNewLine _
                                      & "   @KAZU_KB                                    AS KAZU_KB,                                                       " & vbNewLine _
                                      & "   @GUI_IRIME                                  AS GUI_IRIME,                                                     " & vbNewLine _
                                      & "   '' AS YOJITU,                                                                                                 " & vbNewLine _
                                      & "--   CASE WHEN RTRIM(INKAL.FURI_NO) = '' THEN '入荷' ELSE '振入' END AS SYUBETU,                                                                                            " & vbNewLine _
                                      & "   CASE WHEN RTRIM(INKAL.FURI_NO) = '' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K028' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') ELSE (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K029' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') END AS SYUBETU,                                                                                            " & vbNewLine _                                      
                                      & "   ZAITRS.INKO_PLAN_DATE AS PLAN_DATE,                                                                           " & vbNewLine _
                                      & "   INKAS.KONSU * ISNULL(GOODS.PKG_NB,0) + INKAS.HASU AS INKA_KOSU,                                               " & vbNewLine _
                                      & "   (INKAS.KONSU * ISNULL(GOODS.PKG_NB,0) + INKAS.HASU) * INKAS.IRIME AS INKA_SURYO,                              " & vbNewLine _
                                      & "   0 AS OUTKA_KOSU,                                                                                              " & vbNewLine _
                                      & "   0 AS OUTKA_SURYO,                                                                                             " & vbNewLine _
                                      & "   0 AS ZAN_KOSU,                                                                                                " & vbNewLine _
                                      & "   GOODS.NB_UT AS NB_UT,                                                                                         " & vbNewLine _
                                      & "   0 AS ZAN_SURYO,                                                                                               " & vbNewLine _
                                      & "   GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                           " & vbNewLine _
                                      & "   ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD + (CASE WHEN ZAITRS.LOCA = '' THEN '' ELSE" _
                                      & " '-' + ZAITRS.LOCA END) AS OKIBA,                                                                                " & vbNewLine _
                                      & "   ZAITRS.INKA_NO_L + '-' + ZAITRS.INKA_NO_M + '-' + ZAITRS.INKA_NO_S AS KANRI_NO,                               " & vbNewLine _
                                      & "   INKAS.ZAI_REC_NO AS ZAI_REC_NO,                                                                               " & vbNewLine _
                                      & "   '' AS DEST_NM,                                                                                                " & vbNewLine _
                                      & "   CASE WHEN ISNULL(RTRIM(INKAM.OUTKA_FROM_ORD_NO_M), '') = '' THEN INKAL.OUTKA_FROM_ORD_NO_L ELSE INKAM.OUTKA_FROM_ORD_NO_M END AS ORD_NO,                                        " & vbNewLine _
                                      & "   CASE WHEN ISNULL(RTRIM(INKAM.BUYER_ORD_NO_M), '')      = '' THEN INKAL.BUYER_ORD_NO_L      ELSE INKAM.BUYER_ORD_NO_M      END AS BUYER_ORD_NO,                                            " & vbNewLine _
                                      & "   UNSOCO.UNSOCO_NM AS UNSOCO_NM,                                                                                " & vbNewLine _
                                      & "   INKAS.REMARK AS REMARK,                                                                                       " & vbNewLine _
                                      & "   INKAS.REMARK_OUT AS REMARK_OUT,                                                                               " & vbNewLine _
                                      & "   KBN1.KBN_NM1 AS GOODS_COND_NM_1,                                                                              " & vbNewLine _
                                      & "   KBN2.KBN_NM1 AS GOODS_COND_NM_2,                                                                              " & vbNewLine _
                                      & "   CUSTCOND.JOTAI_NM AS GOODS_COND_NM_3,                                                                         " & vbNewLine _
                                      & "   KBN3.KBN_NM1 AS OFB_KB_NM,                                                                                    " & vbNewLine _
                                      & "   KBN4.KBN_NM1 AS SPD_KB_NM,                                                                                    " & vbNewLine _
                                      & "   KBN5.KBN_NM1 AS ALLOC_PRIORITY_NM,                                                                            " & vbNewLine _
                                      & "   DEST.DEST_NM AS DEST_CD_NM,                                                                                   " & vbNewLine _
                                      & "   '' AS RSV_NO,                                                                                                 " & vbNewLine _
                                      & "   '1' AS SORT_KEY,                                                                                              " & vbNewLine _
                                      & "   ZAITRS.INKA_NO_L AS KANRI_NO_L,                                                                                " & vbNewLine _
                                      & "   ZAITRS.INKA_NO_M AS KANRI_NO_M,                                                                                " & vbNewLine _
                                      & "   ZAITRS.INKA_NO_S AS KANRI_NO_S,                                                                                 " & vbNewLine _
                                      & "   GOODS.GOODS_NM_1    AS GOODS_NM,                                                                              " & vbNewLine _
                                      & "   ZAITRS.LOT_NO       AS LOT_NO,                                                                                " & vbNewLine _
                                      & "   GOODS.STD_IRIME_NB  AS IRIME                                                                          " & vbNewLine _
                                      & " -- 1888_WIT_ロケーション変更強化対応                                                                            " & vbNewLine _
                                      & " , ISNULL(TRS_UPD_USER.USER_NM, '')        AS ZAI_TRS_UPD_USER_NM                                                " & vbNewLine _
                                      & "FROM $LM_TRN$..B_INKA_S AS INKAS                                                                                 " & vbNewLine _
                                      & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
                                      & "   (SELECT                                                                                                       " & vbNewLine _
                                      & "       GOODS.NRS_BR_CD,                                                                                          " & vbNewLine _
                                      & "       GOODS.STD_IRIME_NB,                                                                                       " & vbNewLine _
                                      & "       GOODS.STD_IRIME_UT,                                                                                       " & vbNewLine _
                                      & "       GOODS.NB_UT,                                                                                              " & vbNewLine _
                                      & "       GOODS.GOODS_NM_1,                                                                                         " & vbNewLine _
                                      & "       ISNULL(GOODS.PKG_NB,0) AS PKG_NB,                                                                         " & vbNewLine _
                                      & "       GOODS.GOODS_CD_CUST,                                                                                      " & vbNewLine _
                                      & "       GOODS.CUST_CD_L,                                                                                           " & vbNewLine _
                                      & "       GOODS.CUST_CD_M,                                                                                           " & vbNewLine _
                                      & "       GOODS.CUST_CD_S,                                                                                           " & vbNewLine _
                                      & "       GOODS.CUST_CD_SS                                                                                           " & vbNewLine _
                                      & "    FROM $LM_MST$..M_GOODS AS GOODS                                                                                " & vbNewLine _
                                      & "    WHERE GOODS.GOODS_CD_NRS = @GOODS_CD_NRS) GOODS                                                              " & vbNewLine _
                                      & "  ON INKAS.NRS_BR_CD = GOODS.NRS_BR_CD                                                                           " & vbNewLine _
                                      & "   --荷主M                                                                                                      " & vbNewLine _
                                      & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                      & "     (SELECT                                                                                                    " & vbNewLine _
                                      & "        CUST.CUST_CD_L,                                                                                         " & vbNewLine _
                                      & "        CUST.CUST_CD_M,                                                                                         " & vbNewLine _
                                      & "        CUST.CUST_CD_S,                                                                                         " & vbNewLine _
                                      & "        CUST.CUST_CD_SS,                                                                                        " & vbNewLine _
                                      & "        CUST.CUST_NM_L,                                                                                         " & vbNewLine _
                                      & "        CUST.NRS_BR_CD                                                                                          " & vbNewLine _
                                      & "     FROM $LM_MST$..M_CUST AS CUST                                                                                " & vbNewLine _
                                      & "     WHERE CUST.NRS_BR_CD    = @NRS_BR_CD   ) CUST                                                              " & vbNewLine _
                                      & "   ON                                                                                                           " & vbNewLine _
                                      & "      CUST.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "       GOODS.CUST_CD_L  = CUST.CUST_CD_L                                                                        " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "       GOODS.CUST_CD_M  = CUST.CUST_CD_M                                                                        " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "       GOODS.CUST_CD_S  = CUST.CUST_CD_S                                                                        " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "       GOODS.CUST_CD_SS = CUST.CUST_CD_SS                                                                       " & vbNewLine _
                                      & "   --営業所M                                                                                                    " & vbNewLine _
                                      & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                      & "      $LM_MST$..M_NRS_BR AS NRS_BR                                                                                " & vbNewLine _
                                      & "   ON                                                                                                           " & vbNewLine _
                                      & "      NRS_BR.NRS_BR_CD = @NRS_BR_CD                                                                             " & vbNewLine _
                                      & "   --倉庫M                                                                                                      " & vbNewLine _
                                      & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                      & "      $LM_MST$..M_SOKO AS SOKO                                                                                    " & vbNewLine _
                                      & "   ON                                                                                                           " & vbNewLine _
                                      & "      SOKO.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "      SOKO.WH_CD     = @WH_CD                                                                                   " & vbNewLine _
                                      & "   --商品コードでの荷主帳票パターン取得                                                                         " & vbNewLine _
                                      & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                      & "     $LM_MST$..M_CUST_RPT MCR1                                                                                    " & vbNewLine _
                                      & "    ON                                                                                                          " & vbNewLine _
                                      & "     MCR1.NRS_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MCR1.CUST_CD_L  = GOODS.CUST_CD_L                                                                          " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MCR1.CUST_CD_M  = GOODS.CUST_CD_M                                                                          " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MCR1.CUST_CD_S = GOODS.CUST_CD_S                                                                           " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MCR1.PTN_ID    = @PTN_ID                                                                                   " & vbNewLine _
                                      & "    --帳票パターン取得                                                                                          " & vbNewLine _
                                      & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                      & "     $LM_MST$..M_RPT MR1                                                                                          " & vbNewLine _
                                      & "    ON                                                                                                          " & vbNewLine _
                                      & "     MR1.NRS_BR_CD = @NRS_BR_CD                                                                                 " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MR1.PTN_ID    = MCR1.PTN_ID                                                                                " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MR1.PTN_CD    = MCR1.PTN_CD                                                                                " & vbNewLine _
                                      & "    --存在しない場合の帳票パターン取得                                                                          " & vbNewLine _
                                      & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                      & "     $LM_MST$..M_RPT MR2                                                                                          " & vbNewLine _
                                      & "    ON                                                                                                          " & vbNewLine _
                                      & "     MR2.NRS_BR_CD     = @NRS_BR_CD                                                                             " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MR2.PTN_ID        = @PTN_ID                                                                                " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MR2.STANDARD_FLAG = '01'                                                                                   " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        ZAITRS.NRS_BR_CD,                                                                                        " & vbNewLine _
                                      & "        ZAITRS.ZAI_REC_NO,                                                                                        " & vbNewLine _
                                      & "        ZAITRS.GOODS_COND_KB_1,                                                                                  " & vbNewLine _
                                      & "        ZAITRS.GOODS_COND_KB_2,                                                                                  " & vbNewLine _
                                      & "        ZAITRS.GOODS_COND_KB_3,                                                                                  " & vbNewLine _
                                      & "        ZAITRS.CUST_CD_L,                                                                                        " & vbNewLine _
                                      & "        ZAITRS.DEST_CD_P,                                                                                        " & vbNewLine _
                                      & "        ZAITRS.OFB_KB,                                                                                           " & vbNewLine _
                                      & "        ZAITRS.SPD_KB,                                                                                           " & vbNewLine _
                                      & "        ZAITRS.TOU_NO,                                                                                           " & vbNewLine _
                                      & "        ZAITRS.SITU_NO,                                                                                          " & vbNewLine _
                                      & "        ZAITRS.ZONE_CD,                                                                                          " & vbNewLine _
                                      & "        ZAITRS.LOCA,                                                                                             " & vbNewLine _
                                      & "        ZAITRS.ALLOC_PRIORITY,                                                                                   " & vbNewLine _
                                      & "        ZAITRS.INKA_NO_L,                                                                                        " & vbNewLine _
                                      & "        ZAITRS.INKA_NO_M,                                                                                        " & vbNewLine _
                                      & "        ZAITRS.INKA_NO_S,                                                                                        " & vbNewLine _
                                      & "        ZAITRS.INKO_PLAN_DATE,                                                                                   " & vbNewLine _
                                      & "        ZAITRS.LOT_NO,                                                                                           " & vbNewLine _
                                      & "        ZAITRS.SYS_DEL_FLG                                                                                       " & vbNewLine _
                                      & " -- 1888_WIT_ロケーション変更強化対応                                                                            " & vbNewLine _
                                      & "      , ZAITRS.SYS_UPD_USER                                                                                      " & vbNewLine _
                                      & "     FROM $LM_TRN$..D_ZAI_TRS AS ZAITRS                                                                          " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "           ZAITRS.NRS_BR_CD = @NRS_BR_CD                                                                         " & vbNewLine _
                                      & "       AND ZAITRS.SYS_DEL_FLG = '0') AS ZAITRS                                                                   " & vbNewLine _
                                      & "  ON INKAS.NRS_BR_CD = ZAITRS.NRS_BR_CD                                                                          " & vbNewLine _
                                      & "  AND INKAS.ZAI_REC_NO = ZAITRS.ZAI_REC_NO                                                                       " & vbNewLine _
                                      & " -- 1888_WIT_ロケーション変更強化対応                                                                            " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_MST$..S_USER AS TRS_UPD_USER                                                                " & vbNewLine _
                                      & "   ON ZAITRS.SYS_UPD_USER = TRS_UPD_USER.USER_CD                                                                 " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_TRN$..B_INKA_M AS INKAM                                                                       " & vbNewLine _
                                      & "  ON INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                                                           " & vbNewLine _
                                      & "  AND INKAS.INKA_NO_L = INKAM.INKA_NO_L                                                                          " & vbNewLine _
                                      & "  AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                                                                          " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_TRN$..B_INKA_L AS INKAL                                                                       " & vbNewLine _
                                      & "  ON INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                                                           " & vbNewLine _
                                      & "  AND INKAM.INKA_NO_L = INKAL.INKA_NO_L                                                                          " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_TRN$..F_UNSO_L AS UNSOL                                                                       " & vbNewLine _
                                      & "  ON INKAS.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                           " & vbNewLine _
                                      & "  AND INKAS.INKA_NO_L = UNSOL.INOUTKA_NO_L                                                                          " & vbNewLine _
                                      & "  AND UNSOL.MOTO_DATA_KB = '10'                                                                          " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO                                                                      " & vbNewLine _
                                      & "  ON UNSOL.NRS_BR_CD = UNSOCO.NRS_BR_CD                                                                          " & vbNewLine _
                                      & "  AND UNSOL.UNSO_CD = UNSOCO.UNSOCO_CD                                                                           " & vbNewLine _
                                      & "  AND UNSOL.UNSO_BR_CD = UNSOCO_BR_CD                                                                            " & vbNewLine _
                                      & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN1.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN1.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                            
                                      & "     FROM $LM_MST$..Z_KBN AS KBN1                                                                                  " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN1.KBN_GROUP_CD = 'S005')KBN1                                                                          " & vbNewLine _
                                      & "  ON ZAITRS.GOODS_COND_KB_1 = KBN1.KBN_CD                                                                        " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN2.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN2.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                                                                  
                                      & "     FROM $LM_MST$..Z_KBN AS KBN2                                                                                  " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN2.KBN_GROUP_CD = 'S006')KBN2                                                                          " & vbNewLine _
                                      & "  ON ZAITRS.GOODS_COND_KB_2 = KBN2.KBN_CD                                                                        " & vbNewLine _
                                      & "LEFT OUTER JOIN $LM_MST$..M_CUSTCOND AS CUSTCOND                                                                   " & vbNewLine _
                                      & "   ON ZAITRS.NRS_BR_CD = CUSTCOND.NRS_BR_CD                                                                      " & vbNewLine _
                                      & "   AND ZAITRS.CUST_CD_L = CUSTCOND.CUST_CD_L                                                                     " & vbNewLine _
                                      & "   AND ZAITRS.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD                                                                " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN3.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN3.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                                                                  
                                      & "     FROM $LM_MST$..Z_KBN AS KBN3                                                                                  " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN3.KBN_GROUP_CD = 'B002')KBN3                                                                          " & vbNewLine _
                                      & "  ON ZAITRS.OFB_KB = KBN3.KBN_CD                                                                                 " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN4.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN4.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                                                                  
                                      & "     FROM $LM_MST$..Z_KBN AS KBN4                                                                                  " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN4.KBN_GROUP_CD = 'H003')KBN4                                                                          " & vbNewLine _
                                      & "  ON ZAITRS.SPD_KB = KBN4.KBN_CD                                                                                 " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN5.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN5.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                                                                  
                                      & "     FROM $LM_MST$..Z_KBN AS KBN5                                                                                  " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN5.KBN_GROUP_CD = 'W001')KBN5                                                                          " & vbNewLine _
                                      & "  ON ZAITRS.ALLOC_PRIORITY = KBN5.KBN_CD                                                                         " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST                                                                          " & vbNewLine _
                                      & "  ON ZAITRS.NRS_BR_CD = DEST.NRS_BR_CD                                                                           " & vbNewLine _
                                      & "  AND ZAITRS.CUST_CD_L = DEST.CUST_CD_L                                                                          " & vbNewLine _
                                      & "  AND ZAITRS.DEST_CD_P = DEST.DEST_CD                                                                            " & vbNewLine _
                                      & "WHERE                                                                                                            " & vbNewLine _
                                      & "  INKAS.NRS_BR_CD = @NRS_BR_CD                                                                                   " & vbNewLine _
                                      & "  AND INKAS.INKA_NO_L = @INKA_NO_L                                                                               " & vbNewLine _
                                      & "  AND INKAS.INKA_NO_M = @INKA_NO_M                                                                               " & vbNewLine _
                                      & "  AND INKAS.INKA_NO_S = @INKA_NO_S                                                                               " & vbNewLine _
                                      & "  AND INKAS.SYS_DEL_FLG = '0'                                                                                      " & vbNewLine _
                                      & "  AND ZAITRS.SYS_DEL_FLG = '0'                                                                                     " & vbNewLine _
                                      & "UNION                                                                                                            " & vbNewLine _
                                      & "SELECT                                                                                                           " & vbNewLine _
                                      & " CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                " & vbNewLine _
                                      & "      ELSE MR2.RPT_ID END                        AS RPT_ID,                                                      " & vbNewLine _
                                      & "   @NRS_BR_CD                                  AS NRS_BR_CD,                                                     " & vbNewLine _
                                      & "   NRS_BR.NRS_BR_NM                            AS NRS_BR_NM,                                                     " & vbNewLine _
                                      & "   CUST.CUST_CD_L                              AS CUST_CD_L,                                                     " & vbNewLine _
                                      & "   CUST.CUST_NM_L                              AS CUST_NM_L,                                                     " & vbNewLine _
                                      & "   @WH_CD                                      AS WH_CD,                                                         " & vbNewLine _
                                      & "   SOKO.WH_NM                                  AS WH_NM,                                                         " & vbNewLine _
                                      & "   GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST,                                                 " & vbNewLine _
                                      & "   GOODS.GOODS_NM_1                            AS GOODS_NM,                                                      " & vbNewLine _
                                      & "   GOODS.STD_IRIME_NB                          AS IRIME,                                                         " & vbNewLine _
                                      & "   @KAZU_KB                                    AS KAZU_KB,                                                       " & vbNewLine _
                                      & "   @GUI_IRIME                                  AS GUI_IRIME,                                                     " & vbNewLine _
                                      & "--   (CASE WHEN OUTKAL.SYS_DEL_FLG = '1' THEN '消' WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB < '60'" _
                                      & "-- THEN 'ヨ' WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB >= '60' THEN 'サ' WHEN" _
                                      & "-- OUTKAM.ALCTD_KB <> '04' AND OUTKAL.OUTKA_STATE_KB < '60' THEN '予' ELSE '' END) AS YOJITU,                      " & vbNewLine _
                                      & "--   (CASE WHEN RTRIM(OUTKAL.FURI_NO) = '' THEN '出荷' ELSE '振出' END) AS SYUBETU,                                    " & vbNewLine _
                                      & "   (CASE WHEN OUTKAL.SYS_DEL_FLG = '1' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K032' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB < '60'" _
                                      & " THEN 'ヨ' WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB >= '60' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K033' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') WHEN" _
                                      & " OUTKAM.ALCTD_KB <> '04' AND OUTKAL.OUTKA_STATE_KB < '60' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K026' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') ELSE '' END) AS YOJITU,                      " & vbNewLine _
                                      & "   (CASE WHEN RTRIM(OUTKAL.FURI_NO) = '' THEN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K034' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') ELSE (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K035' AND RIGHT(KBN_CD,1) = @LANG AND SYS_DEL_FLG = '0') END) AS SYUBETU,                                    " & vbNewLine _                                      
                                      & "   OUTKAL.OUTKA_PLAN_DATE AS PLAN_DATE,                                                                          " & vbNewLine _
                                      & "   0 AS INKA_KOSU,                                                                                               " & vbNewLine _
                                      & "   0 AS INKA_SURYO,                                                                                              " & vbNewLine _
                                      & "   OUTKAS.ALCTD_NB AS OUTKA_KOSU,                                                                                " & vbNewLine _
                                      & "   OUTKAS.ALCTD_QT AS OUTKA_SURYO,                                                                               " & vbNewLine _
                                      & "   0 AS ZAN_KOSU,                                                                                                " & vbNewLine _
                                      & "   GOODS.NB_UT AS NB_UT,                                                                                         " & vbNewLine _
                                      & "   0 AS ZAN_SURYO,                                                                                               " & vbNewLine _
                                      & "   GOODS.STD_IRIME_UT AS STD_IRIME_UT,                                                                           " & vbNewLine _
                                      & "   OUTKAS.TOU_NO + '-' + OUTKAS.SITU_NO + '-' + OUTKAS.ZONE_CD + (CASE WHEN OUTKAS.LOCA = '' THEN '' ELSE" _
                                      & " '-' + OUTKAS.LOCA END) AS OKIBA,                                                                                " & vbNewLine _
                                      & "   OUTKAS.OUTKA_NO_L + '-' + OUTKAS.OUTKA_NO_M + '-' + OUTKAS.OUTKA_NO_S AS KANRI_NO,                            " & vbNewLine _
                                      & "   '' AS ZAI_REC_NO,                                                                                             " & vbNewLine _
                                      & "   CASE WHEN OUTKAL.DEST_KB = '00' THEN DEST.DEST_NM                                                             " & vbNewLine _
                                      & "        ELSE OUTKAL.DEST_NM          END  AS DEST_NM,                                                            " & vbNewLine _
                                      & "   CASE WHEN ISNULL(RTRIM(OUTKAM.CUST_ORD_NO_DTL), '')  = '' THEN OUTKAL.CUST_ORD_NO  ELSE OUTKAM.CUST_ORD_NO_DTL  END AS ORD_NO,                                                  " & vbNewLine _
                                      & "   CASE WHEN ISNULL(RTRIM(OUTKAM.BUYER_ORD_NO_DTL), '') = '' THEN OUTKAL.BUYER_ORD_NO ELSE OUTKAM.BUYER_ORD_NO_DTL END AS BUYER_ORD_NO,                                          " & vbNewLine _
                                      & "   UNSOCO.UNSOCO_NM AS UNSOCO_NM,                                                                                " & vbNewLine _
                                      & "   OUTKAS.REMARK AS REMARK,                                                                                      " & vbNewLine _
                                      & "   '' AS REMARK_OUT,                                                                                             " & vbNewLine _
                                      & "   '' AS GOODS_COND_NM_1,                                                                                        " & vbNewLine _
                                      & "   '' AS GOODS_COND_NM_2,                                                                                        " & vbNewLine _
                                      & "   '' AS GOODS_COND_NM_3,                                                                                        " & vbNewLine _
                                      & "   '' AS OFB_KB_NM,                                                                                              " & vbNewLine _
                                      & "   '' AS SPD_KB_NM,                                                                                              " & vbNewLine _
                                      & "   '' AS ALLOC_PRIORITY_NM,                                                                                      " & vbNewLine _
                                      & "   '' AS DEST_CD_NM,                                                                                             " & vbNewLine _
                                      & "   '' AS RSV_NO,                                                                                                 " & vbNewLine _
                                      & "   '3' AS SORT_KEY,                                                                                              " & vbNewLine _
                                      & "   OUTKAS.OUTKA_NO_L AS KANRI_NO_L,                                                                                " & vbNewLine _
                                      & "   OUTKAS.OUTKA_NO_M AS KANRI_NO_M,                                                                                " & vbNewLine _
                                      & "   OUTKAS.OUTKA_NO_S AS KANRI_NO_S,                                                                                " & vbNewLine _
                                      & "   GOODS.GOODS_NM_1    AS GOODS_NM,                                                                              " & vbNewLine _
                                      & "   ZAITRS.LOT_NO       AS LOT_NO,                                                                                " & vbNewLine _
                                      & "   GOODS.STD_IRIME_NB  AS IRIME                                                                           " & vbNewLine _
                                      & " -- 1888_WIT_ロケーション変更強化対応                                                                            " & vbNewLine _
                                      & " , ISNULL(TRS_UPD_USER.USER_NM, '') AS ZAI_TRS_UPD_USER_NM                                                       " & vbNewLine _
                                      & "FROM $LM_TRN$..C_OUTKA_S AS OUTKAS                                                                               " & vbNewLine _
                                      & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
                                      & "   (SELECT                                                                                                       " & vbNewLine _
                                      & "       GOODS.NRS_BR_CD,                                                                                          " & vbNewLine _
                                      & "       GOODS.STD_IRIME_NB,                                                                                       " & vbNewLine _
                                      & "       GOODS.STD_IRIME_UT,                                                                                       " & vbNewLine _
                                      & "       GOODS.NB_UT,                                                                                              " & vbNewLine _
                                      & "       GOODS.GOODS_NM_1,                                                                                         " & vbNewLine _
                                      & "       ISNULL(GOODS.PKG_NB,0) AS PKG_NB,                                                                          " & vbNewLine _
                                      & "       GOODS.GOODS_CD_CUST,                                                                                       " & vbNewLine _
                                      & "       GOODS.CUST_CD_L,                                                                                           " & vbNewLine _
                                      & "       GOODS.CUST_CD_M,                                                                                           " & vbNewLine _
                                      & "       GOODS.CUST_CD_S,                                                                                           " & vbNewLine _
                                      & "       GOODS.CUST_CD_SS                                                                                           " & vbNewLine _
                                      & "    FROM $LM_MST$..M_GOODS AS GOODS                                                                              " & vbNewLine _
                                      & "    WHERE GOODS.GOODS_CD_NRS = @GOODS_CD_NRS) GOODS                                                              " & vbNewLine _
                                      & "  ON OUTKAS.NRS_BR_CD = GOODS.NRS_BR_CD                                                                          " & vbNewLine _
                                                                            & "   --荷主M                                                                                                      " & vbNewLine _
                                      & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                      & "     (SELECT                                                                                                    " & vbNewLine _
                                      & "        CUST.CUST_CD_L,                                                                                         " & vbNewLine _
                                      & "        CUST.CUST_CD_M,                                                                                         " & vbNewLine _
                                      & "        CUST.CUST_CD_S,                                                                                         " & vbNewLine _
                                      & "        CUST.CUST_CD_SS,                                                                                        " & vbNewLine _
                                      & "        CUST.CUST_NM_L,                                                                                         " & vbNewLine _
                                      & "        CUST.NRS_BR_CD                                                                                          " & vbNewLine _
                                      & "     FROM $LM_MST$..M_CUST AS CUST                                                                                " & vbNewLine _
                                      & "     WHERE CUST.NRS_BR_CD    = @NRS_BR_CD   ) CUST                                                              " & vbNewLine _
                                      & "   ON                                                                                                           " & vbNewLine _
                                      & "      CUST.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "       GOODS.CUST_CD_L  = CUST.CUST_CD_L                                                                        " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "       GOODS.CUST_CD_M  = CUST.CUST_CD_M                                                                        " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "       GOODS.CUST_CD_S  = CUST.CUST_CD_S                                                                        " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "       GOODS.CUST_CD_SS = CUST.CUST_CD_SS                                                                       " & vbNewLine _
                                      & "   --営業所M                                                                                                    " & vbNewLine _
                                      & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                      & "      $LM_MST$..M_NRS_BR AS NRS_BR                                                                                " & vbNewLine _
                                      & "   ON                                                                                                           " & vbNewLine _
                                      & "      NRS_BR.NRS_BR_CD = @NRS_BR_CD                                                                             " & vbNewLine _
                                      & "   --倉庫M                                                                                                      " & vbNewLine _
                                      & "   LEFT JOIN                                                                                                    " & vbNewLine _
                                      & "      $LM_MST$..M_SOKO AS SOKO                                                                                    " & vbNewLine _
                                      & "   ON                                                                                                           " & vbNewLine _
                                      & "      SOKO.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
                                      & "   AND                                                                                                          " & vbNewLine _
                                      & "      SOKO.WH_CD     = @WH_CD                                                                                   " & vbNewLine _
                                      & "   --商品コードでの荷主帳票パターン取得                                                                         " & vbNewLine _
                                      & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                      & "     $LM_MST$..M_CUST_RPT MCR1                                                                                    " & vbNewLine _
                                      & "    ON                                                                                                          " & vbNewLine _
                                      & "     MCR1.NRS_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MCR1.CUST_CD_L  = GOODS.CUST_CD_L                                                                          " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MCR1.CUST_CD_M  = GOODS.CUST_CD_M                                                                          " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MCR1.CUST_CD_S = GOODS.CUST_CD_S                                                                           " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MCR1.PTN_ID    = @PTN_ID                                                                                   " & vbNewLine _
                                      & "    --帳票パターン取得                                                                                          " & vbNewLine _
                                      & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                      & "     $LM_MST$..M_RPT MR1                                                                                          " & vbNewLine _
                                      & "    ON                                                                                                          " & vbNewLine _
                                      & "     MR1.NRS_BR_CD = @NRS_BR_CD                                                                                 " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MR1.PTN_ID    = MCR1.PTN_ID                                                                                " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MR1.PTN_CD    = MCR1.PTN_CD                                                                                " & vbNewLine _
                                      & "    --存在しない場合の帳票パターン取得                                                                          " & vbNewLine _
                                      & "    LEFT JOIN                                                                                                   " & vbNewLine _
                                      & "     $LM_MST$..M_RPT MR2                                                                                          " & vbNewLine _
                                      & "    ON                                                                                                          " & vbNewLine _
                                      & "     MR2.NRS_BR_CD     = @NRS_BR_CD                                                                             " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MR2.PTN_ID        = @PTN_ID                                                                                " & vbNewLine _
                                      & "    AND                                                                                                         " & vbNewLine _
                                      & "     MR2.STANDARD_FLAG = '01'                                                                                   " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        ZAITRS.NRS_BR_CD,                                                                                        " & vbNewLine _
                                      & "        ZAITRS.GOODS_COND_KB_1,                                                                                  " & vbNewLine _
                                      & "        ZAITRS.GOODS_COND_KB_2,                                                                                  " & vbNewLine _
                                      & "        ZAITRS.GOODS_COND_KB_3,                                                                                  " & vbNewLine _
                                      & "        ZAITRS.CUST_CD_L,                                                                                        " & vbNewLine _
                                      & "        ZAITRS.DEST_CD_P,                                                                                        " & vbNewLine _
                                      & "        ZAITRS.OFB_KB,                                                                                           " & vbNewLine _
                                      & "        ZAITRS.SPD_KB,                                                                                           " & vbNewLine _
                                      & "        ZAITRS.TOU_NO,                                                                                           " & vbNewLine _
                                      & "        ZAITRS.SITU_NO,                                                                                          " & vbNewLine _
                                      & "        ZAITRS.ZONE_CD,                                                                                          " & vbNewLine _
                                      & "        ZAITRS.LOCA,                                                                                             " & vbNewLine _
                                      & "        ZAITRS.ALLOC_PRIORITY,                                                                                   " & vbNewLine _
                                      & "        ZAITRS.INKA_NO_L,                                                                                        " & vbNewLine _
                                      & "        ZAITRS.INKA_NO_M,                                                                                        " & vbNewLine _
                                      & "        ZAITRS.INKA_NO_S,                                                                                        " & vbNewLine _
                                      & "        ZAITRS.LOT_NO,                                                                                           " & vbNewLine _
                                      & "        ZAITRS.INKO_PLAN_DATE,                                                                                   " & vbNewLine _
                                      & "        ZAITRS.GOODS_CD_NRS                                                                                      " & vbNewLine _
                                      & " -- 1888_WIT_ロケーション変更強化対応                                                                            " & vbNewLine _
                                      & "      , ZAITRS.SYS_UPD_USER                                                                                      " & vbNewLine _
                                      & "     FROM $LM_TRN$..D_ZAI_TRS AS ZAITRS                                                                          " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "       ZAITRS.ZAI_REC_NO = @ZAI_REC_NO                                                                           " & vbNewLine _
                                      & "       AND ZAITRS.SYS_DEL_FLG = '0' ) AS ZAITRS                                                                  " & vbNewLine _
                                      & "  ON OUTKAS.NRS_BR_CD = ZAITRS.NRS_BR_CD                                                                         " & vbNewLine _
                                      & " -- 1888_WIT_ロケーション変更強化対応                                                                            " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_MST$..S_USER AS TRS_UPD_USER                                                                " & vbNewLine _
                                      & "   ON ZAITRS.SYS_UPD_USER = TRS_UPD_USER.USER_CD                                                                 " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_TRN$..C_OUTKA_M AS OUTKAM                                                                   " & vbNewLine _
                                      & "  ON OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                                                         " & vbNewLine _
                                      & "  AND OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                                                                      " & vbNewLine _
                                      & "  AND OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                                                                      " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_TRN$..C_OUTKA_L AS OUTKAL                                                                     " & vbNewLine _
                                      & "  ON OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                                         " & vbNewLine _
                                      & "  AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                                                      " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_TRN$..F_UNSO_L AS UNSOL                                                                       " & vbNewLine _
                                      & "  ON OUTKAS.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                          " & vbNewLine _
                                      & "  AND OUTKAS.OUTKA_NO_L = UNSOL.INOUTKA_NO_L                                                                          " & vbNewLine _
                                      & "  AND UNSOL.MOTO_DATA_KB = '20'                                                                          " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_MST$..M_UNSOCO AS UNSOCO                                                                      " & vbNewLine _
                                      & "  ON UNSOL.NRS_BR_CD = UNSOCO.NRS_BR_CD                                                                          " & vbNewLine _
                                      & "  AND UNSOL.UNSO_CD = UNSOCO.UNSOCO_CD                                                                           " & vbNewLine _
                                      & "  AND UNSOL.UNSO_BR_CD = UNSOCO_BR_CD                                                                            " & vbNewLine _
                                      & "LEFT OUTER JOIN                                                                                                  " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN1.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN1.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                                                                  
                                      & "     FROM $LM_MST$..Z_KBN AS KBN1                                                                                  " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN1.KBN_GROUP_CD = 'S005')KBN1                                                                          " & vbNewLine _
                                      & "  ON ZAITRS.GOODS_COND_KB_1 = KBN1.KBN_CD                                                                        " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN2.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN2.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                                                                  
                                      & "     FROM $LM_MST$..Z_KBN AS KBN2                                                                                  " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN2.KBN_GROUP_CD = 'S006')KBN2                                                                          " & vbNewLine _
                                      & "  ON ZAITRS.GOODS_COND_KB_2 = KBN2.KBN_CD                                                                        " & vbNewLine _
                                      & "LEFT OUTER JOIN $LM_MST$..M_CUSTCOND AS CUSTCOND                                                                   " & vbNewLine _
                                      & "   ON ZAITRS.NRS_BR_CD = CUSTCOND.NRS_BR_CD                                                                      " & vbNewLine _
                                      & "   AND ZAITRS.CUST_CD_L = CUSTCOND.CUST_CD_L                                                                     " & vbNewLine _
                                      & "   AND ZAITRS.GOODS_COND_KB_3 = CUSTCOND.JOTAI_CD                                                                " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN3.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN3.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                                                                  
                                      & "     FROM $LM_MST$..Z_KBN AS KBN3                                                                                  " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN3.KBN_GROUP_CD = 'B002')KBN3                                                                          " & vbNewLine _
                                      & "  ON ZAITRS.OFB_KB = KBN3.KBN_CD                                                                                 " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN4.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN4.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                                                                  
                                      & "     FROM $LM_MST$..Z_KBN AS KBN4                                                                                  " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN4.KBN_GROUP_CD = 'H003')KBN4                                                                          " & vbNewLine _
                                      & "  ON ZAITRS.SPD_KB = KBN4.KBN_CD                                                                                 " & vbNewLine _
                                      & " LEFT OUTER JOIN                                                                                                 " & vbNewLine _
                                      & "    (SELECT                                                                                                      " & vbNewLine _
                                      & "        KBN5.KBN_CD,                                                                                             " & vbNewLine _
                                      & "--        KBN5.KBN_NM1                                                                                             " & vbNewLine _
                                      & "        " & " #KBN# " & "     AS KBN_NM1                                                                                            " & vbNewLine _                                                                                                                  
                                      & "     FROM $LM_MST$..Z_KBN AS KBN5                                                                                  " & vbNewLine _
                                      & "     WHERE                                                                                                       " & vbNewLine _
                                      & "        KBN5.KBN_GROUP_CD = 'W001')KBN5                                                                          " & vbNewLine _
                                      & "  ON ZAITRS.ALLOC_PRIORITY = KBN5.KBN_CD                                                                         " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST                                                                          " & vbNewLine _
                                      & "  ON OUTKAL.NRS_BR_CD = DEST.NRS_BR_CD                                                                           " & vbNewLine _
                                      & "  AND OUTKAL.CUST_CD_L = DEST.CUST_CD_L                                                                          " & vbNewLine _
                                      & "  AND OUTKAL.DEST_CD = DEST.DEST_CD                                                                              " & vbNewLine _
                                      & " LEFT OUTER JOIN $LM_MST$..M_FURI_GOODS AS FURI_GOODS                                                            " & vbNewLine _
                                      & "  ON FURI_GOODS.NRS_BR_CD = ZAITRS.NRS_BR_CD                                                                     " & vbNewLine _
                                      & "  AND FURI_GOODS.CD_NRS = ZAITRS.GOODS_CD_NRS                                                                    " & vbNewLine _
                                      & "WHERE OUTKAM.NRS_BR_CD = @NRS_BR_CD                                                                              " & vbNewLine _
    'END YANAI 要望番号725
    'END YANAI 20110913 小分け対応
    '& "  AND (OUTKAM.GOODS_CD_NRS = @GOODS_CD_NRS                                                                       " & vbNewLine _
    '& "      OR (OUTKAM.GOODS_CD_NRS = @CD_NRS_TO                                                                       " & vbNewLine _
    '& "          AND OUTKAL.OUTKA_STATE_KB < '60'))                                                                       " & vbNewLine _
    '& "          AND OUTKAS.ZAI_REC_NO IN(SELECT ZAI_REC_NO FROM $LM_TRN$..D_ZAI_TRS                                     " & vbNewLine _
    '& "          WHERE D_ZAI_TRS.NRS_BR_CD = @NRS_BR_CD                                                                          " & vbNewLine _
    '& "          AND D_ZAI_TRS.INKA_NO_L = @INKA_NO_L                                                                          " & vbNewLine _
    '& "          AND D_ZAI_TRS.INKA_NO_M = @INKA_NO_M                                                                          " & vbNewLine _
    '& "          AND D_ZAI_TRS.INKA_NO_S = @INKA_NO_S                                                                        " & vbNewLine _
    '& "          AND D_ZAI_TRS.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
    '& "          AND D_ZAI_TRS.NRS_BR_CD = OUTKAS.NRS_BR_CD                                                                        " & vbNewLine _
    '& "          AND D_ZAI_TRS.INKA_NO_L = OUTKAS.INKA_NO_L                                                                        " & vbNewLine _
    '& "          AND D_ZAI_TRS.INKA_NO_M = OUTKAS.INKA_NO_M                                                                        " & vbNewLine _
    '& "          AND D_ZAI_TRS.INKA_NO_S = OUTKAS.INKA_NO_S)                                                                        " & vbNewLine _
    '& "  AND OUTKAM.SYS_DEL_FLG = 0                                                                                     " & vbNewLine _
    '& "  AND OUTKAS.SYS_DEL_FLG = 0                                                                                     " & vbNewLine

    ''' <summary>
    ''' 入出荷タブ（在庫ごと）選択時SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DATA_RIREKI_ZAI2 As String = " AND OUTKAS.ZAI_REC_NO = @ZAI_REC_NO                                                                      " & vbNewLine _
                                        & "  AND OUTKAM.SYS_DEL_FLG = '0'                                                                                     " & vbNewLine _
                                        & "  AND OUTKAS.SYS_DEL_FLG = '0'                                                                                     " & vbNewLine


    ''' <summary>
    ''' 入出荷履歴（入荷ごと）タブ選択時ORDERBY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDERBY_DATA_RIREKIZAI As String = " ORDER BY PLAN_DATE,SORT_KEY,KANRI_NO,ZAI_REC_NO"

#End Region

#Region "パラメータ"

    ''' <summary>
    ''' ゼロフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ZERO_FLG As String = "0"

    ''' <summary>
    ''' データ種別
    ''' </summary>
    ''' <remarks></remarks>
    Private Const INKA_STATE_KBN_YO As String = "01"       '予
    Private Const INKA_STATE_KBN_JITSU As String = "02"    '実
    Private Const INKA_STATE_KBN_ZEN As String = "03"      '全

    ''' <summary>
    ''' 帳票ID
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PTN_ID As String = "25"

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

#Region "商品"

    ''' <summary>
    ''' データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectDataGoods(ByVal ds As DataSet) As DataSet

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'GROUP BYなしのSQL
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_COUNT_GROUP)
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_GOODS_GROUP)
        Me.AllSelectSQL()
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_GOODS_END)
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_COUNT_END)

        'SQL文のコンパイル
        '2016.02.05 修正START
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))
        '2016.02.05 修正END

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        Return SelectCount(cmd, ds)

    End Function

    ''' <summary>
    ''' データ検索（商品）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListDataGoods(ByVal ds As DataSet) As DataSet

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        '商品用SQL
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_GOODS_GROUP)
        Me.AllSelectSQL()
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_GOODS_END)
        Me._StrSql.Append(LMD040DAC.SQL_ORDER_BY_PTN1)

        'SQL文のコンパイル
        '2016.02.05 修正START
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))
        '2016.02.05 修正END

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD040DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("YOJITU", "YOJITU")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("ZANKOSU", "ZANKOSU")
        map.Add("NB_UT", "NB_UT")
        map.Add("ZANSURYO", "ZANSURYO")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("CUST_COST_CD1", "CUST_COST_CD1")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("DOKU_KB", "DOKU_KB")
        map.Add("DOKU_NM", "DOKU_NM")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_NM", "ONDO_NM")
        map.Add("WH_NM", "WH_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("SHOBO_NM", "SHOBO_NM")

        '取得結果をデータセットへ設定
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD040OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD040OUT").Rows.Count())

        Return ds

    End Function

#End Region

#Region "商品・ロット・入目"

    ''' <summary>
    ''' データ件数検索（商品・ロット・入目）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectDataGoodsLot(ByVal ds As DataSet) As DataSet

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_COUNT_GROUP)
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_GOODS_LOT_GROUP)
        Me.AllSelectSQL()
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_GOODS_LOT_END)
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_COUNT_END)

        'SQL文のコンパイル
        '2016.02.05 修正START
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))
        '2016.02.05 修正END

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        Return SelectCount(cmd, ds)

    End Function

    ''' <summary>
    ''' データ検索（商品・ロット・入目）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListDataGoodsLot(ByVal ds As DataSet) As DataSet

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        'GROUP BYなしのSQL
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_GOODS_LOT_GROUP)
        Me.AllSelectSQL()
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_GOODS_LOT_END)
        Me._StrSql.Append(LMD040DAC.SQL_ORDER_BY_PTN4)

        'SQL文のコンパイル
        '2016.02.05 修正START
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))
        '2016.02.05 修正END

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD040DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("YOJITU", "YOJITU")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("ZANKOSU", "ZANKOSU")
        map.Add("NB_UT", "NB_UT")
        map.Add("ZANSURYO", "ZANSURYO")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("CUST_COST_CD1", "CUST_COST_CD1")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("DOKU_KB", "DOKU_KB")
        map.Add("DOKU_NM", "DOKU_NM")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_NM", "ONDO_NM")
        map.Add("WH_NM", "WH_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("SHOBO_NM", "SHOBO_NM")

        '取得結果をデータセットへ設定
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD040OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD040OUT").Rows.Count())

        Return ds

    End Function

#End Region

#Region "商品・ロット・置場"

    ''' <summary>
    ''' データ件数検索（商品・ロット・置場）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectDataOkibaLot(ByVal ds As DataSet) As DataSet

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'GROUP BYなしのSQL
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_COUNT_GROUP)
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_GOODS_OKIBA_GROUP)
        Me.AllSelectSQL()
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_GOODS_OKIBA_END)
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_COUNT_END)

        'SQL文のコンパイル
        '2016.02.05 修正START
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))
        '2016.02.05 修正END

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        Return SelectCount(cmd, ds)

    End Function

    ''' <summary>
    ''' データ検索（商品・ロット・置場）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListDataOkibaLot(ByVal ds As DataSet) As DataSet

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        'GROUP BYなしのSQL
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_GOODS_OKIBA_GROUP)
        Me.AllSelectSQL()
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_GOODS_OKIBA_END)
        Me._StrSql.Append(LMD040DAC.SQL_ORDER_BY_PTN2)

        'SQL文のコンパイル
        '2016.02.05 修正START
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))
        '2016.02.05 修正START

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD040DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("YOJITU", "YOJITU")
        map.Add("OKIBA", "OKIBA")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("ZANKOSU", "ZANKOSU")
        map.Add("NB_UT", "NB_UT")
        map.Add("ZANSURYO", "ZANSURYO")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("CUST_COST_CD1", "CUST_COST_CD1")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("DOKU_KB", "DOKU_KB")
        map.Add("DOKU_NM", "DOKU_NM")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_NM", "ONDO_NM")
        map.Add("WH_NM", "WH_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("SHOBO_NM", "SHOBO_NM")

        '取得結果をデータセットへ設定
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD040OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD040OUT").Rows.Count())

        Return ds

    End Function

#End Region

#Region "商品・ロット・入目・置場"

    ''' <summary>
    ''' データ件数検索（置場）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectDataOkiba(ByVal ds As DataSet) As DataSet

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'GROUP BYなしのSQL
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_COUNT_GROUP)
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_OKIBA_GROUP)
        Me.AllSelectSQL()
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_OKIBA_END)
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_COUNT_END)

        'SQL文のコンパイル
        '2016.02.05 修正START
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))
        '2016.02.05 修正END

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        Return SelectCount(cmd, ds)

    End Function

    ''' <summary>
    ''' データ検索（置場）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListDataOkiba(ByVal ds As DataSet) As DataSet

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        'GROUP BYなしのSQL
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_OKIBA_GROUP)
        Me.AllSelectSQL()
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_OKIBA_END)
        Me._StrSql.Append(LMD040DAC.SQL_ORDER_BY_PTN4)

        'SQL文のコンパイル
        '2016.02.05 修正START
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))
        '2016.02.05 修正END

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD040DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("YOJITU", "YOJITU")
        map.Add("OKIBA", "OKIBA")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("ZANKOSU", "ZANKOSU")
        map.Add("NB_UT", "NB_UT")
        map.Add("ZANSURYO", "ZANSURYO")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("CUST_COST_CD1", "CUST_COST_CD1")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("DOKU_KB", "DOKU_KB")
        map.Add("DOKU_NM", "DOKU_NM")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_NM", "ONDO_NM")
        map.Add("WH_NM", "WH_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("SHOBO_NM", "SHOBO_NM")

        '取得結果をデータセットへ設定
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD040OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD040OUT").Rows.Count())

        Return ds

    End Function

#End Region

#Region "詳細"

    ''' <summary>
    ''' データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectDataAll(ByVal ds As DataSet) As DataSet

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'GROUP BYなしのSQL
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_COUNT)
        Me._StrSql.Append(LMD040DAC.SQL_FROM1)                          'SQL構築(データ抽出用From句)
        Call Me.SetConditionSQL()                                       'SQL構築(条件設定）

        'SQL文のコンパイル
        '2016.02.05 修正START
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))
        '2016.02.05 修正END

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        Return SelectCount(cmd, ds)

    End Function

    ''' <summary>
    ''' データ検索（詳細）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListDataAll(ByVal ds As DataSet) As DataSet

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        'GROUP BYなしのSQL
        Me.AllSelectSQL()
        Me._StrSql.Append(LMD040DAC.SQL_ORDER_BY)                       'SQL構築(データ抽出用OrderBy句)

        'SQL文のコンパイル
        '2016.02.05 修正START
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))
        '2016.02.05 修正END

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD040DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得結果をデータセットへ設定
        ds = MyBase.SetSelectResultToDataSet(Me.SetLMD040OUTData(), ds, reader, "LMD040OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD040OUT").Rows.Count())

        Return ds

    End Function

#End Region

#Region "履歴"

#Region "帳票"

    ''' <summary>
    ''' 帳票用のメソッド(ロット別)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks>DataSet</remarks>
    Private Function SelectListRptDataLot(ByVal ds As DataSet) As DataSet
        Return Me.SelectListRptData(ds, "LMD540OUT")
    End Function

    ''' <summary>
    ''' 帳票用のメソッド(商品別)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks>DataSet</remarks>
    Private Function SelectListRptDataOkiba(ByVal ds As DataSet) As DataSet
        Return Me.SelectListRptData(ds, "LMD541OUT")
    End Function

    ''' <summary>
    ''' 帳票用のメソッド
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">DataTable名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListRptData(ByVal ds As DataSet, ByVal tblNm As String) As DataSet

        Dim str As String() = New String() {"RPT_ID" _
                                            , "NRS_BR_CD" _
                                            , "NRS_BR_NM" _
                                            , "CUST_CD_L" _
                                            , "CUST_NM_L" _
                                            , "WH_CD" _
                                            , "WH_NM" _
                                            , "GOODS_CD_CUST" _
                                            , "GOODS_NM" _
                                            , "LOT_NO" _
                                            , "IRIME" _
                                            , "YOJITU" _
                                            , "SYUBETU" _
                                            , "PLAN_DATE" _
                                            , "INKA_KOSU" _
                                            , "INKA_SURYO" _
                                            , "OUTKA_KOSU" _
                                            , "OUTKA_SURYO" _
                                            , "ZAN_KOSU" _
                                            , "NB_UT" _
                                            , "ZAN_SURYO" _
                                            , "STD_IRIME_UT" _
                                            , "OKIBA" _
                                            , "ZAI_REC_NO" _
                                            , "DEST_NM" _
                                            , "ORD_NO" _
                                            , "BUYER_ORD_NO" _
                                            , "UNSOCO_NM" _
                                            , "DEST_CD_NM" _
                                            , "KANRI_NO_L" _
                                            , "KANRI_NO_M" _
                                            , "KAZU_KB" _
                                            , "GUI_IRIME" _
                                            }
        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        '入荷ごと
        If "1".Equals(ds.Tables("GENZAIKO").Rows(0).Item("TAB_FLG").ToString()) = True Then
            '2016.02.05 修正START
            'Return Me.SelectListDataInkaSet(ds, tblNm, str)
            Return Me.SelectListDataInkaSet(ds, tblNm, str, kbnNm)
            '2016.02.05 修正END
        End If

        '2016.02.05 修正START
        'Return Me.SelectListDataZaikoSet(ds, tblNm, str)
        Return Me.SelectListDataZaikoSet(ds, tblNm, str, kbnNm)
        '2016.02.05 修正END

    End Function

#End Region

#Region "入荷"

    ''' <summary>
    ''' 入出荷履歴（在庫ごと(画面)）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectListDataInka(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"YOJITU" _
                                            , "SYUBETU" _
                                            , "PLAN_DATE" _
                                            , "INKA_KOSU" _
                                            , "INKA_SURYO" _
                                            , "OUTKA_KOSU" _
                                            , "OUTKA_SURYO" _
                                            , "ZAN_KOSU" _
                                            , "NB_UT" _
                                            , "ZAN_SURYO" _
                                            , "STD_IRIME_UT" _
                                            , "OKIBA" _
                                            , "KANRI_NO" _
                                            , "KANRI_NO_L" _
                                            , "KANRI_NO_M" _
                                            , "KANRI_NO_S" _
                                            , "ZAI_REC_NO" _
                                            , "DEST_NM" _
                                            , "ORD_NO" _
                                            , "BUYER_ORD_NO" _
                                            , "UNSOCO_NM" _
                                            , "REMARK" _
                                            , "REMARK_OUT" _
                                            , "GOODS_COND_NM_1" _
                                            , "GOODS_COND_NM_2" _
                                            , "GOODS_COND_NM_3" _
                                            , "OFB_KB_NM" _
                                            , "SPD_KB_NM" _
                                            , "ALLOC_PRIORITY_NM" _
                                            , "DEST_CD_NM" _
                                            , "RSV_NO" _
                                            , "SORT_KEY" _
                                            , "GOODS_NM" _
                                            , "LOT_NO" _
                                            , "IRIME" _
                                            , "ZAI_TRS_UPD_USER_NM" _
                                            }

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        '2016.02.05 修正START
        'Return Me.SelectListDataInkaSet(ds, "LMD040_RIREKI", str)
        Return Me.SelectListDataInkaSet(ds, "LMD040_RIREKI", str, kbnNm)
        '2016.02.05 修正END

    End Function

    '2016.02.05 追加START
    ''' <summary>
    ''' 言語の取得(区分マスタの区分項目)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLangSet(ByVal ds As DataSet) As String

        'DataSetのIN情報を取得
        Dim inTbl As DataTable

        If ds.Tables("GENZAIKO").Rows.Count() = 0 Then
            inTbl = ds.Tables("LMD040IN")
        Else
            inTbl = ds.Tables("GENZAIKO")
        End If

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'SQL作成

        'SQL構築
        Me._StrSql.AppendLine("SELECT                                    ")
        Me._StrSql.AppendLine(" CASE WHEN KBN_NM1 = ''    THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      WHEN KBN_NM1 IS NULL THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      ELSE KBN_NM1 END      AS KBN_NM     ")
        Me._StrSql.AppendLine("FROM $LM_MST$..Z_KBN                      ")
        Me._StrSql.AppendLine("WHERE KBN_GROUP_CD = 'K025'               ")
        Me._StrSql.AppendLine("  AND RIGHT(KBN_CD,1 ) = @LANG            ")
        Me._StrSql.AppendLine("  AND SYS_DEL_FLG  = '0'                  ")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD040DAC", "SelectLangset", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim str As String = "KBN_NM1"

        If reader.Read() = True Then
            str = Convert.ToString(reader("KBN_NM"))
        End If
        reader.Close()

        Return str

    End Function
    '2016.02.05 追加END

    ''' <summary>
    ''' 入出荷履歴（入荷ごと）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">設定先DataTable名</param>
    ''' <param name="str">マッピング文字配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectListDataInkaSet(ByVal ds As DataSet, ByVal tblNm As String, ByVal str As String(), ByVal kbnNm As String) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("GENZAIKO")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'SQL作成
        'GROUP BYなしのSQL
        Dim whereStr As String = String.Empty
        'Dim andstr As StringBuilder = New StringBuilder()
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_DATA_RIREKI1)
        Call Me.SetConditionSQLInka7(Me._Row.Item("OPT_TP").ToString())
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_DATA_RIREKI1_2)
        Call Me.SetConditionSQLInka8(Me._Row.Item("OPT_TP").ToString())
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_DATA_RIREKI1_3)

        Me._StrSql.Append(LMD040DAC.SQL_SELECT_DATA_RIREKI4)
        Call Me.SetConditionSQLInka4(Me._Row.Item("OPT_TP").ToString())
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_DATA_RIREKI5)
        Call Me.SetConditionSQLInka5(Me._Row.Item("OPT_TP").ToString())                                       'SQL構築(条件設定）
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_DATA_RIREKI6)
        Call Me.SetConditionSQLInka9(Me._Row.Item("OPT_TP").ToString())                                       'SQL構築(条件設定）
        Me._StrSql.Append(LMD040DAC.SQL_SELECT_DATA_RIREKI6_2)
        Call Me.SetConditionSQLInka6(Me._Row.Item("OPT_TP").ToString())                                       'SQL構築(条件設定）

        With Me._Row
            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            whereStr = .Item("WH_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            whereStr = .Item("PTN_ID").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", whereStr, DBDataType.CHAR))
            whereStr = .Item("KAZU_KB").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KAZU_KB", whereStr, DBDataType.CHAR))
            whereStr = .Item("GUI_IRIME").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_IRIME", whereStr, DBDataType.CHAR))
            'whereStr = .Item("INKA_NO_L").ToString()
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", whereStr, DBDataType.CHAR))
            'whereStr = .Item("INKA_NO_M").ToString()
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", whereStr, DBDataType.CHAR))
            'whereStr = .Item("INKA_NO_S").ToString()
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", whereStr, DBDataType.CHAR))
            whereStr = .Item("GOODS_CD_NRS").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", whereStr, DBDataType.CHAR))
            'START YANAI 要望番号508
            'whereStr = .Item("CD_NRS_TO").ToString()
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_NRS_TO", whereStr, DBDataType.CHAR))
            'END YANAI 要望番号508
            'whereStr = .Item("ZAI_REC_NO").ToString()
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", whereStr, DBDataType.CHAR))
            whereStr = .Item("INKO_PLAN_DATE_FROM").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE_FROM", whereStr, DBDataType.CHAR))
            If String.IsNullOrEmpty(whereStr) = True Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MAEZAN_INKO_PLAN", "00000000", DBDataType.CHAR))
            Else
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MAEZAN_INKO_PLAN", whereStr, DBDataType.CHAR))
            End If
            whereStr = .Item("INKO_PLAN_DATE_TO").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE_TO", whereStr, DBDataType.CHAR))
            whereStr = .Item("OUTKA_DEL_FLG").ToString()
            If LMConst.FLG.OFF.Equals(whereStr) Then
                Me._StrSql.Append(LMD040DAC.SQL_SELECT_DATA_RIREKI7)
            End If
            '20130214
            Select Case Me._Row.Item("OPT_TP").ToString()
                Case "0"

                Case "1"
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.CHAR))
                Case "2"
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.CHAR))
                    'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OKIBA", .Item("OKIBA").ToString(), DBDataType.CHAR))
                Case "3"
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.CHAR))
                    'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OKIBA", .Item("OKIBA").ToString(), DBDataType.CHAR))
                Case "4"
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
                    whereStr = .Item("ZAI_REC_NO").ToString()
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", whereStr, DBDataType.CHAR))

            End Select

            '2016.02.05 追加START
            Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString(), DBDataType.CHAR))
            '2016.02.05 追加END

        End With
        Me._StrSql.Append(LMD040DAC.SQL_ORDERBY_DATA_RIREKI)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD040DAC", "SelectListDataInka", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        Dim max As Integer = str.Length - 1
        For i As Integer = 0 To max
            map.Add(str(i), str(i))
        Next

        '取得結果をデータセットへ設定
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, tblNm)

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables(tblNm).Rows.Count())

        Return ds

    End Function

#End Region

#Region "在庫"

    ''' <summary>
    ''' 入出荷履歴（在庫ごと(画面)）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectListDataZaiko(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"YOJITU" _
                                            , "SYUBETU" _
                                            , "PLAN_DATE" _
                                            , "INKA_KOSU" _
                                            , "INKA_SURYO" _
                                            , "OUTKA_KOSU" _
                                            , "OUTKA_SURYO" _
                                            , "ZAN_KOSU" _
                                            , "NB_UT" _
                                            , "ZAN_SURYO" _
                                            , "STD_IRIME_UT" _
                                            , "OKIBA" _
                                            , "KANRI_NO" _
                                            , "ZAI_REC_NO" _
                                            , "DEST_NM" _
                                            , "ORD_NO" _
                                            , "BUYER_ORD_NO" _
                                            , "UNSOCO_NM" _
                                            , "REMARK" _
                                            , "REMARK_OUT" _
                                            , "GOODS_COND_NM_1" _
                                            , "GOODS_COND_NM_2" _
                                            , "GOODS_COND_NM_3" _
                                            , "OFB_KB_NM" _
                                            , "SPD_KB_NM" _
                                            , "ALLOC_PRIORITY_NM" _
                                            , "DEST_CD_NM" _
                                            , "RSV_NO" _
                                            , "SORT_KEY" _
                                            , "KANRI_NO_L" _
                                            , "KANRI_NO_M" _
                                            , "KANRI_NO_S" _
                                            , "GOODS_NM" _
                                            , "LOT_NO" _
                                            , "IRIME" _
                                            , "ZAI_TRS_UPD_USER_NM" _
                                            }

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        '取得結果をデータセットへ設定
        Return Me.SelectListDataZaikoSet(ds, "LMD040_RIREKIZAI", str, kbnNm)

    End Function

    ''' <summary>
    ''' 入出荷履歴（在庫ごと(共通データセット作成)）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">設定先DataTable名</param>
    ''' <param name="str">マッピング文字配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectListDataZaikoSet(ByVal ds As DataSet, ByVal tblNm As String, ByVal str As String(), ByVal kbnNm As String) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("GENZAIKO")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'SQL作成
        'GROUP BYなしのSQL
        Dim whereStr As String = String.Empty
        'Dim andstr As StringBuilder = New StringBuilder()
        Me._StrSql.Append(LMD040DAC.SQL_DATA_RIREKI_ZAI)
        Call Me.SetConditionSQLZai1()                                       'SQL構築(条件設定）
        Me._StrSql.Append(LMD040DAC.SQL_DATA_RIREKI_ZAI2)
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            whereStr = .Item("WH_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            whereStr = .Item("PTN_ID").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", whereStr, DBDataType.CHAR))
            whereStr = .Item("KAZU_KB").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KAZU_KB", whereStr, DBDataType.CHAR))
            whereStr = .Item("GUI_IRIME").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_IRIME", whereStr, DBDataType.CHAR))
            whereStr = .Item("INKA_NO_L").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", whereStr, DBDataType.CHAR))
            whereStr = .Item("INKA_NO_M").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", whereStr, DBDataType.CHAR))
            whereStr = .Item("INKA_NO_S").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", whereStr, DBDataType.CHAR))
            whereStr = .Item("GOODS_CD_NRS").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", whereStr, DBDataType.CHAR))
            'START YANAI 要望番号508
            'whereStr = .Item("CD_NRS_TO").ToString()
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_NRS_TO", whereStr, DBDataType.CHAR))
            'END YANAI 要望番号508
            whereStr = .Item("ZAI_REC_NO").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", whereStr, DBDataType.CHAR))

            '2016.02.05 追加START
            Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString(), DBDataType.CHAR))
            '2016.02.05 追加END

        End With
        Me._StrSql.Append(LMD040DAC.SQL_ORDERBY_DATA_RIREKIZAI)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD040DAC", "SelectListDataZaikoSet", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        Dim max As Integer = str.Length - 1
        For i As Integer = 0 To max
            map.Add(str(i), str(i))
        Next

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, tblNm)

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables(tblNm).Rows.Count())

        Return ds

    End Function

#End Region

#End Region

#Region "条件文"

#Region "在庫"

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

            '荷主コード（極小）
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.CUST_CD_SS LIKE @CUST_CD_U")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_U", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '入荷日From
            whereStr = .Item("INKO_PLAN_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" INKAL.INKA_DATE >= @INKO_PLAN_DATE_FROM ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '入荷日To
            whereStr = .Item("INKO_PLAN_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" INKAL.INKA_DATE <= @INKO_PLAN_DATE_TO ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            'ゼロフラグ
            whereStr = .Item("ZERO_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'ゼロフラグ=0の場合　'2011.09.16 コメントのみ修正
                If whereStr.Equals(LMD040DAC.ZERO_FLG) Then
                    If andstr.Length <> 0 Then
                        andstr.Append("AND")
                    End If
                    andstr.Append(" ZAITRS.PORA_ZAI_NB <> 0 ")
                    andstr.Append(vbNewLine)
                End If
            End If

            'データ種別
            whereStr = .Item("INKA_STATE_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If whereStr.Equals(LMD040DAC.INKA_STATE_KBN_ZEN) = False Then
                    If andstr.Length <> 0 Then
                        andstr.Append("AND")
                    End If
                    If whereStr.Equals(LMD040DAC.INKA_STATE_KBN_YO) Then
                        '"予"が選択されている時
                        andstr.Append(" INKAL.INKA_STATE_KB < '50' ")
                        andstr.Append(vbNewLine)
                    ElseIf whereStr.Equals(LMD040DAC.INKA_STATE_KBN_JITSU) Then
                        '"実"が選択されている時
                        andstr.Append(" INKAL.INKA_STATE_KB = '50' ")
                        andstr.Append(vbNewLine)
                    End If
                End If
            End If

            '荷主商品コード
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

            '商品名
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.GOODS_NM_1 LIKE @GOODS_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主カテゴリ1
            whereStr = .Item("SEARCH_KEY_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.SEARCH_KEY_1 LIKE @SEARCH_KEY_1")
                andstr.Append(vbNewLine)
                'START YANAI 要望番号1028
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_1", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_1", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                'END YANAI 要望番号1028
            End If

            '毒劇区分
            whereStr = .Item("DOKU_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.DOKU_KB = @DOKU_KB ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOKU_KB", whereStr, DBDataType.CHAR))
            End If

            '荷主勘定科目コード1
            whereStr = .Item("CUST_COST_CD1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.CUST_COST_CD1 LIKE @CUST_COST_CD1")
                andstr.Append(vbNewLine)
                '(2012.12.05)要望番号1614対応の一環として CHAR→NVARCHAR
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_COST_CD1", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_COST_CD1", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主勘定科目コード2
            whereStr = .Item("CUST_COST_CD2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.CUST_COST_CD2 LIKE @CUST_COST_CD2")
                andstr.Append(vbNewLine)
                '(2012.12.05)要望番号1614対応の一環として CHAR→NVARCHAR
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_COST_CD2", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_COST_CD2", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '消防コード
            whereStr = .Item("SHOBO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.SHOBO_CD LIKE @SHOBO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHOBO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '消防情報
            whereStr = .Item("SHOBO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (KBN9.KBN_NM1 + ' ' + ISNULL(SHOBO.HINMEI,'')) LIKE @SHOBO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHOBO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '(2013.02.14)要望番号1843 置き場条件の変更 -- START --
            '置場
            'whereStr = .Item("OKIBA").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    If andstr.Length <> 0 Then
            '        andstr.Append("AND")
            '    End If
            '    andstr.Append(" ZAITRS.TOU_NO + ZAITRS.SITU_NO + ZAITRS.ZONE_CD + ZAITRS.LOCA LIKE  @OKIBA")
            '    andstr.Append(vbNewLine)
            '    'START YANAI 要望番号579
            '    'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OKIBA", String.Concat(whereStr, "%"), DBDataType.CHAR))
            '    'START YANAI 要望番号672
            '    'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OKIBA", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            '    '(2012.12.05)要望番号1614対応の一環として VARCHAR→NVARCHAR
            '    'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OKIBA", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OKIBA", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            '    'END YANAI 要望番号672
            '    'END YANAI 要望番号579
            'End If

            '棟
            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.TOU_NO = @TOU_NO ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", whereStr, DBDataType.CHAR))
            End If

            '室
            whereStr = .Item("SITU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.SITU_NO = @SITU_NO ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", whereStr, DBDataType.CHAR))
            End If

            'ZONE
            whereStr = .Item("ZONE_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.ZONE_CD = @ZONE_CD ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", whereStr, DBDataType.CHAR))
            End If

            'ロケーション
            whereStr = .Item("LOCA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.LOCA LIKE @LOCA")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If
            '(2013.02.14)要望番号1843 置き場条件の変更 --  END  --


            'ロット
            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.LOT_NO LIKE @LOT_NO")
                andstr.Append(vbNewLine)
                '(2012.12.05)要望番号1614対応の一環として VARCHAR→NVARCHAR
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '備考小（社内）
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.REMARK LIKE @REMARK")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '要望番号:1702 terakawa 2012.12.19 Start
            '備考小（社外）
            whereStr = .Item("REMARK_OUT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.REMARK_OUT LIKE @REMARK_OUT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If
            '要望番号:1702 terakawa 2012.12.19 End

            'シリアル№
            whereStr = .Item("SERIAL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.SERIAL_NO LIKE @SERIAL_NO")
                andstr.Append(vbNewLine)
                '(2012.12.05)要望番号1614対応の一環として CHAR→NVARCHAR
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
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
                andstr.Append(" ZAITRS.GOODS_COND_KB_3 = @GOODS_COND_KB_3 ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", whereStr, DBDataType.CHAR))
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

            '荷主カテゴリ２
            whereStr = .Item("SEARCH_KEY_2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.SEARCH_KEY_2 LIKE @SEARCH_KEY_2 ")
                andstr.Append(vbNewLine)
                'START YANAI 要望番号1028
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_2", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_2", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                'END YANAI 要望番号1028
            End If

            '入荷管理番号
            whereStr = .Item("INKA_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.INKA_NO_L + ZAITRS.INKA_NO_M + ZAITRS.INKA_NO_S LIKE @INKA_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '商品KEY
            whereStr = .Item("GOODS_CD_NRS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.GOODS_CD_NRS LIKE @GOODS_CD_NRS")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '在庫レコード番号
            whereStr = .Item("ZAI_REC_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.ZAI_REC_NO LIKE @ZAI_REC_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
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

            '予約届先コード
            whereStr = .Item("DEST_CD_P").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.DEST_CD_P LIKE @DEST_CD_P")
                andstr.Append(vbNewLine)
                '(2012.12.05)要望番号1614対応の一環として CHAR→NVARCHAR
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD_P", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD_P", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" CUST.CUST_NM_L + '-' + CUST.CUST_NM_M LIKE @CUST_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '税区分
            whereStr = .Item("TAX_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.TAX_KB = @TAX_KB ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", whereStr, DBDataType.CHAR))
            End If

            '温度
            whereStr = .Item("ONDO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.ONDO_KB = @ONDO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_NM", whereStr, DBDataType.CHAR))
            End If

            'START YANAI 要望番号647
            '入目単位
            whereStr = .Item("IRIME_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.STD_IRIME_UT = @IRIME_UT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", whereStr, DBDataType.CHAR))
            End If

            '残数単位
            whereStr = .Item("NB_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.NB_UT = @NB_UT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", whereStr, DBDataType.CHAR))
            End If

            '実数量単位
            whereStr = .Item("STD_IRIME_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.STD_IRIME_UT = @STD_IRIME_UT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME_UT", whereStr, DBDataType.CHAR))
            End If

            '入数単位
            whereStr = .Item("PKG_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.PKG_UT = @PKG_UT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_UT", whereStr, DBDataType.CHAR))
            End If
            'END YANAI 要望番号647

#If True Then       'ADD 2018/10/30 依頼番号 : 002779   【LMS】在庫履歴照会_入り目検索できるように変更
            '入目
            whereStr = .Item("IRIME").ToString()
            If String.IsNullOrEmpty(whereStr) = False _
                AndAlso ("0").Equals(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.IRIME = @IRIME")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", whereStr, DBDataType.NUMERIC))
            End If
#End If
            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If
            andstr.Append(" ZAITRS.SYS_DEL_FLG = '0' ")
            andstr.Append(vbNewLine)

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If

            '2016.02.05 追加START
            Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString()))
            '2016.02.05 追加END

        End With

    End Sub

#End Region

#Region "入荷ごと"

    '''' <summary>
    '''' 入荷用在庫データ取得
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub SetConditionSQLInka1()
    '    'SQLパラメータ初期化
    '    Me._SqlPrmList = New ArrayList()

    '    '検索条件部に入力された条件とパラメータ設定
    '    Dim whereStr As String = String.Empty
    '    Dim whereStrto As String = String.Empty
    '    Dim andstr As StringBuilder = New StringBuilder()
    '    With Me._Row
    '        '入荷の"前残"のデータを取得する場合
    '        andstr.Append("   AND (OUTKAM.GOODS_CD_NRS = @GOODS_CD_NRS ")
    '        andstr.Append(vbNewLine)
    '        whereStr = .Item("CD_NRS_TO").ToString()
    '        If String.IsNullOrEmpty(whereStr) = False Then
    '            andstr.Append(" OR (OUTKAM.GOODS_CD_NRS = @CD_NRS_TO ")
    '            andstr.Append(vbNewLine)
    '            andstr.Append(" AND OUTKAL.OUTKA_STATE_KB < '60')) ")
    '            andstr.Append(vbNewLine)
    '        Else
    '            andstr.Append(" ) ")
    '            andstr.Append(vbNewLine)
    '        End If
    '        andstr.Append(" AND OUTKAM.ALCTD_KB <> '04' ")
    '        andstr.Append(vbNewLine)
    '        andstr.Append(" AND OUTKAM.OUTKA_TTL_NB <> 0 ")
    '        andstr.Append(vbNewLine)
    '        andstr.Append(" AND OUTKAM.OUTKA_TTL_QT <> 0 ")
    '        andstr.Append(vbNewLine)

    '        whereStr = .Item("INKO_PLAN_DATE_FROM").ToString()
    '        If String.IsNullOrEmpty(whereStr) = False Then
    '            andstr.Append(" AND OUTKAL.OUTKA_PLAN_DATE < @INKO_PLAN_DATE_FROM ")
    '            andstr.Append(vbNewLine)
    '        End If

    '    End With

    '    If andstr.Length <> 0 Then
    '        Me._StrSql.Append(andstr)
    '    End If

    'End Sub
    '''' <summary>
    '''' 入荷用在庫データ取得
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub SetConditionSQLInka2()
    '    'SQLパラメータ初期化
    '    Me._SqlPrmList = New ArrayList()

    '    '検索条件部に入力された条件とパラメータ設定
    '    Dim whereStr As String = String.Empty
    '    Dim whereStrto As String = String.Empty
    '    Dim andstr As StringBuilder = New StringBuilder()
    '    With Me._Row
    '        '入荷の"前残"のデータを取得する場合
    '        whereStr = .Item("INKO_PLAN_DATE_FROM").ToString()
    '        If String.IsNullOrEmpty(whereStr) = False Then
    '            andstr.Append(" AND ZAITRS.INKO_PLAN_DATE < @INKO_PLAN_DATE_FROM ")
    '            andstr.Append(vbNewLine)
    '        End If

    '    End With

    '    If andstr.Length <> 0 Then
    '        Me._StrSql.Append(andstr)
    '    End If


    'End Sub

    '''' <summary>
    '''' 入荷用在庫データ取得
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub SetConditionSQLInka3()
    '    'SQLパラメータ初期化
    '    Me._SqlPrmList = New ArrayList()

    '    '検索条件部に入力された条件とパラメータ設定
    '    Dim whereStr As String = String.Empty
    '    Dim whereStrto As String = String.Empty
    '    Dim andstr As StringBuilder = New StringBuilder()
    '    With Me._Row
    '        '入荷の"前残"のデータを取得する場合
    '        whereStr = .Item("INKO_PLAN_DATE_FROM").ToString()
    '        If String.IsNullOrEmpty(whereStr) = False Then
    '            andstr.Append("AND ZAITRS.INKO_PLAN_DATE < @INKO_PLAN_DATE_FROM ")
    '            andstr.Append(vbNewLine)
    '        End If

    '    End With

    '    If andstr.Length <> 0 Then
    '        Me._StrSql.Append(andstr)
    '    End If

    'End Sub
    ''' <summary>
    ''' 入荷用在庫データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionSQLInka4(ByVal optTp As String)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim whereStrto As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row
            Select Case optTp
                Case "0"

                Case "1"
                    andstr.Append(" AND ZAITRS.LOT_NO          = @LOT_NO ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND ZAITRS.IRIME          = @GUI_IRIME ")
                    andstr.Append(vbNewLine)
                Case "2"
                    andstr.Append(" AND ZAITRS.LOT_NO          = @LOT_NO ")
                    andstr.Append(vbNewLine)
                    'andstr.Append(" AND ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD + (CASE WHEN ZAITRS.LOCA = '' THEN '' ELSE '-' +  ZAITRS.LOCA END) = @OKIBA ")
                    andstr.Append(vbNewLine)

                Case "3"
                    andstr.Append(" AND ZAITRS.LOT_NO          = @LOT_NO ")
                    'andstr.Append(vbNewLine)
                    'andstr.Append(" AND ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD + (CASE WHEN ZAITRS.LOCA = '' THEN '' ELSE '-' +  ZAITRS.LOCA END) = @OKIBA ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND ZAITRS.IRIME          = @GUI_IRIME ")
                    andstr.Append(vbNewLine)
                Case "4"
                    andstr.Append(" AND ZAITRS.INKA_NO_L          = @INKA_NO_L ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND ZAITRS.INKA_NO_M          = @INKA_NO_M ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND ZAITRS.INKA_NO_S          = @INKA_NO_S ")
                    andstr.Append(vbNewLine)

            End Select

            '入荷の"前残"のデータを取得する場合
            whereStr = .Item("INKO_PLAN_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append("AND @INKO_PLAN_DATE_FROM <= ZAITRS.INKO_PLAN_DATE ")
                andstr.Append(vbNewLine)
            End If

            '入荷の"前残"のデータを取得する場合
            whereStr = .Item("INKO_PLAN_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append("AND ZAITRS.INKO_PLAN_DATE <= @INKO_PLAN_DATE_TO ")
                andstr.Append(vbNewLine)
            End If

        End With

        If andstr.Length <> 0 Then
            Me._StrSql.Append(andstr)
        End If

    End Sub
    ''' <summary>
    ''' 入荷用在庫データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionSQLInka5(ByVal optTp As String)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim whereStrto As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row
            Select Case optTp
                Case "0"

                Case "1"
                    andstr.Append(" AND INKAS.LOT_NO          = @LOT_NO ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND INKAS.IRIME          = @GUI_IRIME ")
                    andstr.Append(vbNewLine)
                Case "2"
                    andstr.Append(" AND INKAS.LOT_NO          = @LOT_NO ")
                    'andstr.Append(vbNewLine)
                    'andstr.Append(" AND INKAS.TOU_NO + '-' + INKAS.SITU_NO + '-' + INKAS.ZONE_CD + (CASE WHEN INKAS.LOCA = '' THEN '' ELSE '-' +  INKAS.LOCA END) = @OKIBA ")
                    andstr.Append(vbNewLine)

                Case "3"
                    andstr.Append(" AND INKAS.LOT_NO          = @LOT_NO ")
                    'andstr.Append(vbNewLine)
                    'andstr.Append(" AND INKAS.TOU_NO + '-' + INKAS.SITU_NO + '-' + INKAS.ZONE_CD + (CASE WHEN INKAS.LOCA = '' THEN '' ELSE '-' +  INKAS.LOCA END) = @OKIBA ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND INKAS.IRIME          = @GUI_IRIME ")
                    andstr.Append(vbNewLine)
                Case "4"
                    andstr.Append(" AND INKAS.INKA_NO_L          = @INKA_NO_L ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND INKAS.INKA_NO_M          = @INKA_NO_M ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND INKAS.INKA_NO_S          = @INKA_NO_S ")
                    andstr.Append(vbNewLine)

            End Select

            '入荷の"入荷"のデータを取得する場合
            whereStr = .Item("INKO_PLAN_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append("AND @INKO_PLAN_DATE_FROM <= ZAITRS.INKO_PLAN_DATE")
                andstr.Append(vbNewLine)
            End If

            '出荷の"前残"のデータを取得する場合
            whereStrto = .Item("INKO_PLAN_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStrto) = False Then
                andstr.Append("AND ZAITRS.INKO_PLAN_DATE <=  @INKO_PLAN_DATE_TO")
                andstr.Append(vbNewLine)
            End If

        End With

        If andstr.Length <> 0 Then
            Me._StrSql.Append(andstr)
        End If

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール（RIREKI用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionSQLInka6(ByVal optTp As String)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim whereStrto As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            andstr.Append("   AND (OUTKAM.GOODS_CD_NRS = @GOODS_CD_NRS ")
            andstr.Append(vbNewLine)
            'START YANAI 要望番号508
            'whereStr = .Item("CD_NRS_TO").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    andstr.Append(" OR (OUTKAM.GOODS_CD_NRS = @CD_NRS_TO ")
            '    andstr.Append(vbNewLine)
            '    andstr.Append(" AND OUTKAL.OUTKA_STATE_KB < '60')) ")
            '    andstr.Append(vbNewLine)
            'Else
            '    andstr.Append(" ) ")
            '    andstr.Append(vbNewLine)
            'End If
            andstr.Append(" OR (OUTKAM.GOODS_CD_NRS = FURI_GOODS.CD_NRS ")
            andstr.Append(vbNewLine)
            andstr.Append(" AND OUTKAL.OUTKA_STATE_KB < '60')) ")
            andstr.Append(vbNewLine)
            'END YANAI 要望番号508
            andstr.Append(" AND OUTKAS.ZAI_REC_NO IN(SELECT ZAI_REC_NO FROM $LM_TRN$..D_ZAI_TRS ")
            andstr.Append(vbNewLine)
            andstr.Append(" WHERE D_ZAI_TRS.NRS_BR_CD = @NRS_BR_CD ")
            andstr.Append(vbNewLine)
            andstr.Append(" AND D_ZAI_TRS.GOODS_CD_NRS          = @GOODS_CD_NRS ")
            andstr.Append(vbNewLine)
            Select Case optTp
                Case "0"

                Case "1"
                    andstr.Append(" AND D_ZAI_TRS.LOT_NO          = @LOT_NO ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND D_ZAI_TRS.IRIME          = @GUI_IRIME ")
                    andstr.Append(vbNewLine)
                Case "2"
                    andstr.Append(" AND D_ZAI_TRS.LOT_NO          = @LOT_NO ")
                    'andstr.Append(vbNewLine)
                    'andstr.Append(" AND D_ZAI_TRS.TOU_NO + '-' + D_ZAI_TRS.SITU_NO + '-' + D_ZAI_TRS.ZONE_CD + (CASE WHEN D_ZAI_TRS.LOCA = '' THEN '' ELSE '-' +  D_ZAI_TRS.LOCA END) = @OKIBA ")
                    andstr.Append(vbNewLine)

                Case "3"
                    andstr.Append(" AND D_ZAI_TRS.LOT_NO          = @LOT_NO ")
                    'andstr.Append(vbNewLine)
                    'andstr.Append(" AND D_ZAI_TRS.TOU_NO + '-' + D_ZAI_TRS.SITU_NO + '-' + D_ZAI_TRS.ZONE_CD + (CASE WHEN D_ZAI_TRS.LOCA = '' THEN '' ELSE '-' +  D_ZAI_TRS.LOCA END) = @OKIBA ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND D_ZAI_TRS.IRIME          = @GUI_IRIME ")
                    andstr.Append(vbNewLine)
                Case "4"
                    andstr.Append(" AND D_ZAI_TRS.INKA_NO_L          = @INKA_NO_L ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND D_ZAI_TRS.INKA_NO_M          = @INKA_NO_M ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND D_ZAI_TRS.INKA_NO_S          = @INKA_NO_S ")
                    andstr.Append(vbNewLine)

            End Select
            'andstr.Append(" AND D_ZAI_TRS.INKA_NO_L = @INKA_NO_L ")
            'andstr.Append(vbNewLine)
            'andstr.Append(" AND D_ZAI_TRS.INKA_NO_M = @INKA_NO_M ")
            'andstr.Append(vbNewLine)
            'andstr.Append(" AND D_ZAI_TRS.INKA_NO_S = @INKA_NO_S ")
            'andstr.Append(vbNewLine)
            andstr.Append(" AND D_ZAI_TRS.SYS_DEL_FLG = '0') ")
            andstr.Append(vbNewLine)
            'andstr.Append(" AND D_ZAI_TRS.NRS_BR_CD = OUTKAS.NRS_BR_CD ")
            'andstr.Append(vbNewLine)
            'andstr.Append(" AND D_ZAI_TRS.INKA_NO_L = OUTKAS.OUTKA_NO_L ")
            'andstr.Append(vbNewLine)
            'andstr.Append(" AND D_ZAI_TRS.INKA_NO_M = OUTKAS.OUTKA_NO_M ")
            'andstr.Append(vbNewLine)
            'andstr.Append(" AND D_ZAI_TRS.INKA_NO_S = OUTKAS.OUTKA_NO_S) ")
            'andstr.Append(vbNewLine)

            '出荷の"出荷"、"振替"のデータを取得する場合
            whereStr = .Item("INKO_PLAN_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append("AND @INKO_PLAN_DATE_FROM <= OUTKAL.OUTKA_PLAN_DATE")
                andstr.Append(vbNewLine)
            End If

            whereStrto = .Item("INKO_PLAN_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStrto) = False Then
                andstr.Append("AND OUTKAL.OUTKA_PLAN_DATE <=  @INKO_PLAN_DATE_TO")
                andstr.Append(vbNewLine)
            End If
        End With
        If andstr.Length <> 0 Then
            Me._StrSql.Append(andstr)
        End If

    End Sub

    ''' <summary>
    ''' 入荷用在庫データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionSQLInka7(ByVal optTp As String)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim whereStrto As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            Select Case optTp
                Case "0"

                Case "1"
                    andstr.Append(" AND OUTKAS.LOT_NO          = @LOT_NO ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND OUTKAS.IRIME          = @GUI_IRIME ")
                    andstr.Append(vbNewLine)
                Case "2"
                    andstr.Append(" AND OUTKAS.LOT_NO          = @LOT_NO ")
                    'andstr.Append(vbNewLine)
                    'andstr.Append(" AND OUTKAS.TOU_NO + '-' + OUTKAS.SITU_NO + '-' + OUTKAS.ZONE_CD + (CASE WHEN OUTKAS.LOCA = '' THEN '' ELSE '-' +  OUTKAS.LOCA END) = @OKIBA ")
                    andstr.Append(vbNewLine)

                Case "3"
                    andstr.Append(" AND OUTKAS.LOT_NO          = @LOT_NO ")
                    'andstr.Append(vbNewLine)
                    'andstr.Append(" AND OUTKAS.TOU_NO + '-' + OUTKAS.SITU_NO + '-' + OUTKAS.ZONE_CD + (CASE WHEN OUTKAS.LOCA = '' THEN '' ELSE '-' +  OUTKAS.LOCA END) = @OKIBA ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND OUTKAS.IRIME          = @GUI_IRIME ")
                    andstr.Append(vbNewLine)
                Case "4"
                    andstr.Append(" AND OUTKAS.INKA_NO_L          = @INKA_NO_L ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND OUTKAS.INKA_NO_M          = @INKA_NO_M ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND OUTKAS.INKA_NO_S          = @INKA_NO_S ")
                    andstr.Append(vbNewLine)

            End Select

            andstr.Append(" GROUP BY ")
            andstr.Append(vbNewLine)
            andstr.Append(" OUTKAS.NRS_BR_CD ")
            andstr.Append(vbNewLine)
            andstr.Append(" ,OUTKAS.ZAI_REC_NO ")

        End With
        If andstr.Length <> 0 Then
            Me._StrSql.Append(andstr)
        End If
    End Sub

    ''' <summary>
    ''' 入荷用在庫データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionSQLInka8(ByVal optTp As String)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim whereStrto As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            Select Case optTp
                Case "0"

                Case "1"
                    andstr.Append(" AND INKAS.LOT_NO          = @LOT_NO ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND INKAS.IRIME          = @GUI_IRIME ")
                    andstr.Append(vbNewLine)
                Case "2"
                    andstr.Append(" AND INKAS.LOT_NO          = @LOT_NO ")
                    'andstr.Append(vbNewLine)
                    'andstr.Append(" AND INKAS.TOU_NO + '-' + INKAS.SITU_NO + '-' + INKAS.ZONE_CD + (CASE WHEN INKAS.LOCA = '' THEN '' ELSE '-' +  INKAS.LOCA END) = @OKIBA ")
                    andstr.Append(vbNewLine)

                Case "3"
                    andstr.Append(" AND INKAS.LOT_NO          = @LOT_NO ")
                    'andstr.Append(vbNewLine)
                    'andstr.Append(" AND INKAS.TOU_NO + '-' + INKAS.SITU_NO + '-' + INKAS.ZONE_CD + (CASE WHEN INKAS.LOCA = '' THEN '' ELSE '-' +  INKAS.LOCA END) = @OKIBA ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND INKAS.IRIME          = @GUI_IRIME ")
                    andstr.Append(vbNewLine)
                Case "4"
                    andstr.Append(" AND INKAS.INKA_NO_L          = @INKA_NO_L ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND INKAS.INKA_NO_M          = @INKA_NO_M ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND INKAS.INKA_NO_S          = @INKA_NO_S ")
                    andstr.Append(vbNewLine)

            End Select

        End With
        If andstr.Length <> 0 Then
            Me._StrSql.Append(andstr)
        End If
    End Sub

    ''' <summary>
    ''' 入荷用在庫データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionSQLInka9(ByVal optTp As String)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim whereStrto As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            Select Case optTp
                Case "0"

                Case "1"
                    andstr.Append(" AND ZAITRS.LOT_NO          = @LOT_NO ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND ZAITRS.IRIME          = @GUI_IRIME ")
                    andstr.Append(vbNewLine)
                Case "2"
                    andstr.Append(" AND ZAITRS.LOT_NO          = @LOT_NO ")
                    'andstr.Append(vbNewLine)
                    'andstr.Append(" AND ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD + (CASE WHEN ZAITRS.LOCA = '' THEN '' ELSE '-' +  ZAITRS.LOCA END) = @OKIBA ")
                    andstr.Append(vbNewLine)

                Case "3"
                    andstr.Append(" AND ZAITRS.LOT_NO          = @LOT_NO ")
                    'andstr.Append(vbNewLine)
                    'andstr.Append(" AND ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD + (CASE WHEN ZAITRS.LOCA = '' THEN '' ELSE '-' +  ZAITRS.LOCA END) = @OKIBA ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND ZAITRS.IRIME          = @GUI_IRIME ")
                    andstr.Append(vbNewLine)
                Case "4"
                    andstr.Append(" AND ZAITRS.INKA_NO_L          = @INKA_NO_L ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND ZAITRS.INKA_NO_M          = @INKA_NO_M ")
                    andstr.Append(vbNewLine)
                    andstr.Append(" AND ZAITRS.INKA_NO_S          = @INKA_NO_S ")
                    andstr.Append(vbNewLine)

            End Select

        End With
        If andstr.Length <> 0 Then
            Me._StrSql.Append(andstr)
        End If
    End Sub

    ''' <summary>
    ''' 在庫タブデータ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionSQLZai1()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim whereStrto As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            andstr.Append("   AND (OUTKAM.GOODS_CD_NRS = @GOODS_CD_NRS ")
            andstr.Append(vbNewLine)
            'START YANAI 要望番号508
            'whereStr = .Item("CD_NRS_TO").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    andstr.Append(" OR (OUTKAM.GOODS_CD_NRS = @CD_NRS_TO ")
            '    andstr.Append(vbNewLine)
            '    andstr.Append(" AND OUTKAL.OUTKA_STATE_KB < '60')) ")
            '    andstr.Append(vbNewLine)
            'Else
            '    andstr.Append(" ) ")
            '    andstr.Append(vbNewLine)
            'End If
            andstr.Append(" OR (OUTKAM.GOODS_CD_NRS = FURI_GOODS.CD_NRS ")
            andstr.Append(vbNewLine)
            andstr.Append(" AND OUTKAL.OUTKA_STATE_KB < '60')) ")
            andstr.Append(vbNewLine)
            'END YANAI 要望番号508

        End With

        If andstr.Length <> 0 Then
            Me._StrSql.Append(andstr)
        End If

    End Sub

#End Region

#End Region

    'ADD START 2019/8/27 依頼番号:007116,007119
#Region "空棚"

    ''' <summary>
    ''' 空棚検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectEmptyRack(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMD040IN").Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(SQL_SELECT_EMPTY_RACK, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        cmd.Parameters.Add(GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD"), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD"), DBDataType.CHAR))

        MyBase.Logger.WriteSQLLog("LMD040DAC", "SelectEmptyRack", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("DOKU_KB", "DOKU_KB")
        map.Add("ZAI_NB", "ZAI_NB")

        '取得結果をデータセットへ設定
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "EMPTY_RACK")

        Return ds

    End Function

#End Region

#Region "在庫差異"

    ''' <summary>
    ''' 在庫差異検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectZaikoDiff(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMD040IN").Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(SQL_SELECT_ZAIKO_DIFF, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        cmd.Parameters.Add(GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD"), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@CUST_CD_L", "00135", DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@CUST_CD_M", "00", DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@CUST_CD_S", "00", DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@CUST_CD_SS", "00", DBDataType.CHAR))

        MyBase.Logger.WriteSQLLog("LMD040DAC", "SelectZaikoDiff", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable
        map.Add("MATNR", "MATNR")
        map.Add("CHARG", "CHARG")
        map.Add("HINMOKU_TXT", "HINMOKU_TXT")
        map.Add("SAP_PRODUCT_NB", "SAP_PRODUCT_NB")
        map.Add("SAP_INSPECT_NB", "SAP_INSPECT_NB")
        map.Add("SAP_DEFECT_NB", "SAP_DEFECT_NB")
        map.Add("LMS_PRODUCT_QT", "LMS_PRODUCT_QT")
        map.Add("LMS_INSPECT_QT", "LMS_INSPECT_QT")
        map.Add("LMS_DEFECT_QT", "LMS_DEFECT_QT")

        '取得結果をデータセットへ設定
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "ZAIKO_DIFF")

        Return ds

    End Function

#End Region

    'ADD END 2019/8/27 依頼番号:007116,007119

#End Region '検索処理

#Region "内部メソッド"

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

    '2016.02.05 追加START
    ''' <summary>
    ''' 区分項目設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetKbnNm(ByVal sql As String, ByVal kbnNm As String) As String

        '区分項目変換設定
        sql = sql.Replace("#KBN#", kbnNm)

        Return sql

    End Function
    '2016.02.05 追加END

    ''' <summary>
    ''' 詳細データ設定処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetLMD040OUTData() As Hashtable

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("YOJITU", "YOJITU")
        map.Add("OKIBA", "OKIBA")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("ZANKOSU", "ZANKOSU")
        map.Add("NB_UT", "NB_UT")
        map.Add("ZANSURYO", "ZANSURYO")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
        map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
        map.Add("GOODS_COND_NM_3", "GOODS_COND_NM_3")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("SPD_KB_NM", "SPD_KB_NM")
        map.Add("OFB_KB_NM", "OFB_KB_NM")
        map.Add("CUST_COST_CD1", "CUST_COST_CD1")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("INKA_NO", "INKA_NO")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("ALLOC_PRIORITY", "ALLOC_PRIORITY")
        map.Add("ALLOC_PRIORITY_NM", "ALLOC_PRIORITY_NM")
        map.Add("DEST_CD_P", "DEST_CD_P")
        map.Add("DEST_CD_NM", "DEST_CD_NM")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("SHOBO_NM", "SHOBO_NM")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_NM", "TAX_NM")
        map.Add("DOKU_KB", "DOKU_KB")
        map.Add("DOKU_NM", "DOKU_NM")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_NM", "ONDO_NM")
        map.Add("INKA_IRIME", "INKA_IRIME")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("CD_NRS_TO", "CD_NRS_TO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")

        Return map

    End Function

    ''' <summary>
    ''' 件数取得処理
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectCount(ByVal cmd As SqlCommand, ByVal ds As DataSet) As DataSet
        MyBase.Logger.WriteSQLLog("LMD040DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    '''  詳細取得時SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AllSelectSQL()

        'SQL作成
        Me._StrSql.Append(LMD040DAC.ALL_SQL_SELECT_DATA)                'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMD040DAC.SQL_FROM1)                          'SQL構築(データ抽出用From句)
        Call Me.SetConditionSQL()                                       'SQL構築(条件設定）

    End Sub

#End Region '内部メソッド

#End Region 'Method

End Class
