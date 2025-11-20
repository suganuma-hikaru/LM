' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI490  : ローム　棚卸対象商品リスト
'  作  成  者       :  kido
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI490DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI490DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    'ADD START 2018/11/27 要望管理002837
#Region "区分値"
    Private Const STATUS_IMPORTED As String = "1"       '取込直後
    Private Const STATUS_PROCESSED As String = "2"      '処理完了
#End Region
    'ADD END   2018/11/27 要望管理002837

#Region "検索処理"

#Region "検索処理 SELECT句"

    'ADD START 2018/11/27 要望管理002837
    ''' <summary>
    ''' 棚卸対象商品リスト(一番外側のSELECT句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_SELECT As String = _
                   "SELECT                                            " & vbNewLine _
                 & " EXCEL_GOODS.CUST_CD_L                            " & vbNewLine _
                 & ",EXCEL_GOODS.GOODS_CD_CUST                        " & vbNewLine _
                 & ",ISNULL(ZAIKO.GOODS_NM_1,'')   AS GOODS_NM_1      " & vbNewLine _
                 & ",ISNULL(ZAIKO.OKIBA,'')        AS OKIBA           " & vbNewLine _
                 & ",ISNULL(ZAIKO.LOT_NO,'')       AS LOT_NO          " & vbNewLine _
                 & ",ISNULL(ZAIKO.REMARK,'')       AS REMARK          " & vbNewLine _
                 & ",ISNULL(ZAIKO.IRIME,0)         AS IRIME           " & vbNewLine _
                 & ",ISNULL(ZAIKO.PKG_UT,'')       AS PKG_UT          " & vbNewLine _
                 & ",ISNULL(ZAIKO.IRISU,0)         AS IRISU           " & vbNewLine _
                 & ",ISNULL(ZAIKO.PORA_ZAI_NB,0)   AS PORA_ZAI_NB     " & vbNewLine _
                 & ",ISNULL(ZAIKO.ALCTD_NB,0)      AS ALCTD_NB        " & vbNewLine _
                 & ",ISNULL(ZAIKO.ALLOC_CAN_NB,0)  AS ALLOC_CAN_NB    " & vbNewLine _
                 & "FROM                                              " & vbNewLine

    ''' <summary>
    ''' 棚卸対象商品リスト(サブクエリ1:EXCEL_GOODS)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_EXCEL_GOODS As String = _
                   "(                                                               " & vbNewLine _
                 & "    SELECT DISTINCT                                             " & vbNewLine _
                 & "           M_GOODS.CUST_CD_L                                    " & vbNewLine _
                 & "          ,WK_INV.GOODS_CD_CUST                                 " & vbNewLine _
                 & "      FROM $LM_TRN$..I_WK_RHEM_INVENTORY WK_INV                 " & vbNewLine _
                 & "           LEFT JOIN $LM_MST$..M_GOODS                          " & vbNewLine _
                 & "             ON M_GOODS.NRS_BR_CD = WK_INV.NRS_BR_CD            " & vbNewLine _
                 & "            AND M_GOODS.GOODS_CD_CUST = WK_INV.GOODS_CD_CUST    " & vbNewLine _
                 & "      WHERE WK_INV.DEL_KB = '0'                                 " & vbNewLine _
                 & "        AND WK_INV.CRT_DATE = @CRT_DATE                         " & vbNewLine _
                 & "        AND WK_INV.FILE_NAME = @FILE_NAME                       " & vbNewLine _
                 & "        AND WK_INV.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
                 & "        AND WK_INV.STATUS_FLG = '1'                             " & vbNewLine _
                 & "        AND WK_INV.SYS_DEL_FLG = '0'                            " & vbNewLine _
                 & "        AND M_GOODS.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine

    ''' <summary>
    ''' 棚卸対象商品リスト(サブクエリ1:EXCEL_GOODSの閉じ部分とLEFT JOIN句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_EXCEL_GOODS2 As String = _
                   ") EXCEL_GOODS    " & vbNewLine _
                 & "LEFT JOIN        " & vbNewLine _
                 & "(                " & vbNewLine
    'ADD END   2018/11/27 要望管理002837

    ''' <summary>
    ''' 棚卸対象商品リスト(サブクエリ2:ZAIKO)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = _
                   "SELECT                                                                                                             " & vbNewLine _
                 & " GOODS.CUST_CD_L                                                                                                   " & vbNewLine _
                 & ",GOODS.GOODS_CD_CUST                                                                                               " & vbNewLine _
                 & ",GOODS.GOODS_NM_1                                                                                                  " & vbNewLine _
                 & ",ZAITRS.TOU_NO  + '-' + ZAITRS.SITU_NO + '-' + RTRIM(ZAITRS.ZONE_CD) + '-' + ZAITRS.LOCA  AS OKIBA                 " & vbNewLine _
                 & ",ZAITRS.LOT_NO                                       AS LOT_NO                                                     " & vbNewLine _
                 & ",ZAITRS.REMARK_OUT                                   AS REMARK                                                     " & vbNewLine _
                 & ",ZAITRS.IRIME                                                                                                      " & vbNewLine _
                 & ",KBN5.KBN_NM1                                        AS PKG_UT                                                     " & vbNewLine _
                 & ",GOODS.PKG_NB                                        AS IRISU                                                      " & vbNewLine _
                 & ",ZAITRS.PORA_ZAI_NB                                  AS PORA_ZAI_NB                                                " & vbNewLine _
                 & ",ZAITRS.ALCTD_NB                                     AS ALCTD_NB                                                   " & vbNewLine _
                 & ",ZAITRS.ALLOC_CAN_NB                                 AS ALLOC_CAN_NB                                               " & vbNewLine _
                 & "--ADD START 2018/11/27 要望管理002837 最終的なソートに使用するためSELECTに追加                                     " & vbNewLine _
                 & ",ZAITRS.WH_CD                                                                                                      " & vbNewLine _
                 & ",ZAITRS.TOU_NO                                                                                                     " & vbNewLine _
                 & ",ZAITRS.SITU_NO                                                                                                    " & vbNewLine _
                 & ",ZAITRS.ZONE_CD                                                                                                    " & vbNewLine _
                 & ",ZAITRS.LOCA                                                                                                       " & vbNewLine _
                 & "--ADD END   2018/11/27 要望管理002837                                                                              " & vbNewLine _
                 & "FROM                                                                                                               " & vbNewLine _
                 & "$LM_TRN$..D_ZAI_TRS ZAITRS                                                                                         " & vbNewLine _
                 & "LEFT OUTER JOIN                                                                                                    " & vbNewLine _
                 & "$LM_MST$..M_GOODS GOODS ON                                                                                         " & vbNewLine _
                 & "    GOODS.NRS_BR_CD = ZAITRS.NRS_BR_CD                                                                             " & vbNewLine _
                 & "AND GOODS.GOODS_CD_NRS = ZAITRS.GOODS_CD_NRS                                                                       " & vbNewLine _
                 & "AND GOODS.SYS_DEL_FLG = '0'                                                                                        " & vbNewLine _
                 & "LEFT OUTER JOIN                                                                                                    " & vbNewLine _
                 & "$LM_MST$..M_CUST CUST ON                                                                                           " & vbNewLine _
                 & "    CUST.NRS_BR_CD = ZAITRS.NRS_BR_CD                                                                              " & vbNewLine _
                 & "AND CUST.CUST_CD_L = GOODS.CUST_CD_L                                                                               " & vbNewLine _
                 & "AND CUST.CUST_CD_M = GOODS.CUST_CD_M                                                                               " & vbNewLine _
                 & "AND CUST.CUST_CD_S = GOODS.CUST_CD_S                                                                               " & vbNewLine _
                 & "AND CUST.CUST_CD_SS = GOODS.CUST_CD_SS                                                                             " & vbNewLine _
                 & "AND CUST.SYS_DEL_FLG = '0'                                                                                         " & vbNewLine _
                 & "LEFT OUTER JOIN                                                                                                    " & vbNewLine _
                 & "$LM_MST$..M_SHOBO SHOBO ON                                                                                         " & vbNewLine _
                 & "    SHOBO.SHOBO_CD = GOODS.SHOBO_CD                                                                                " & vbNewLine _
                 & "AND SHOBO.SYS_DEL_FLG = '0'                                                                                        " & vbNewLine _
                 & "LEFT OUTER JOIN                                                                                                    " & vbNewLine _
                 & "   (SELECT                                                                                                         " & vbNewLine _
                 & "       KBN_NM1      AS KBN_NM1                                                                                     " & vbNewLine _
                 & "      ,KBN1.KBN_CD                                                                                                 " & vbNewLine _
                 & "    FROM $LM_MST$..Z_KBN AS KBN1                                                                                   " & vbNewLine _
                 & "    WHERE KBN1.KBN_GROUP_CD = 'S005'                                                                               " & vbNewLine _
                 & "     AND KBN1.SYS_DEL_FLG = '0') KBN2                                                                              " & vbNewLine _
                 & "  ON ZAITRS.GOODS_COND_KB_1 = KBN2.KBN_CD                                                                          " & vbNewLine _
                 & "LEFT OUTER JOIN                                                                                                    " & vbNewLine _
                 & "   (SELECT                                                                                                         " & vbNewLine _
                 & "       KBN_NM1      AS KBN_NM1                                                                                     " & vbNewLine _
                 & "      ,KBN1.KBN_CD                                                                                                 " & vbNewLine _
                 & "    FROM $LM_MST$..Z_KBN AS KBN1                                                                                   " & vbNewLine _
                 & "    WHERE KBN1.KBN_GROUP_CD = 'S006'                                                                               " & vbNewLine _
                 & "     AND KBN1.SYS_DEL_FLG = '0') KBN3                                                                              " & vbNewLine _
                 & "  ON ZAITRS.GOODS_COND_KB_2 = KBN3.KBN_CD                                                                          " & vbNewLine _
                 & "LEFT OUTER JOIN                                                                                                    " & vbNewLine _
                 & "   (SELECT                                                                                                         " & vbNewLine _
                 & "       KBN_NM1      AS KBN_NM1                                                                                     " & vbNewLine _
                 & "      ,KBN1.KBN_CD                                                                                                 " & vbNewLine _
                 & "    FROM $LM_MST$..Z_KBN AS KBN1                                                                                   " & vbNewLine _
                 & "    WHERE KBN1.KBN_GROUP_CD = 'K002'                                                                               " & vbNewLine _
                 & "     AND KBN1.SYS_DEL_FLG = '0') KBN5                                                                              " & vbNewLine _
                 & "  ON GOODS.NB_UT = KBN5.KBN_CD                                                                                     " & vbNewLine _
                 & "LEFT OUTER JOIN                                                                                                    " & vbNewLine _
                 & "   (SELECT                                                                                                         " & vbNewLine _
                 & "       KBN_NM1      AS KBN_NM1                                                                                     " & vbNewLine _
                 & "      ,KBN1.KBN_CD                                                                                                 " & vbNewLine _
                 & "    FROM $LM_MST$..Z_KBN AS KBN1                                                                                   " & vbNewLine _
                 & "    WHERE KBN1.KBN_GROUP_CD = 'S004'                                                                               " & vbNewLine _
                 & "     AND KBN1.SYS_DEL_FLG = '0') KBN6                                                                              " & vbNewLine _
                 & "  ON SHOBO.RUI = KBN6.KBN_CD                                                                                       " & vbNewLine _
                 & "LEFT OUTER JOIN                                                                                                    " & vbNewLine _
                 & "   (SELECT                                                                                                         " & vbNewLine _
                 & "       KBN_NM1      AS KBN_NM1                                                                                     " & vbNewLine _
                 & "      ,KBN1.KBN_CD                                                                                                 " & vbNewLine _
                 & "    FROM $LM_MST$..Z_KBN AS KBN1                                                                                   " & vbNewLine _
                 & "    WHERE KBN1.KBN_GROUP_CD = 'S004') KBN9                                                                         " & vbNewLine _
                 & "  ON SHOBO.RUI = KBN9.KBN_CD                                                                                       " & vbNewLine _
                 & "--在庫の荷主での荷主帳票パターン取得                                                                               " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                                " & vbNewLine _
                 & "ON  ZAITRS.NRS_BR_CD = MCR1.NRS_BR_CD                                                                              " & vbNewLine _
                 & "AND ZAITRS.CUST_CD_L = MCR1.CUST_CD_L                                                                              " & vbNewLine _
                 & "AND ZAITRS.CUST_CD_M = MCR1.CUST_CD_M                                                                              " & vbNewLine _
                 & "AND MCR1.PTN_ID = '23'                                                                                             " & vbNewLine _
                 & "AND '00' = MCR1.CUST_CD_S                                                                                          " & vbNewLine _
                 & "--帳票パターン取得                                                                                                 " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                                      " & vbNewLine _
                 & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                 " & vbNewLine _
                 & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                                       " & vbNewLine _
                 & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                                       " & vbNewLine _
                 & "AND MR1.SYS_DEL_FLG = '0'                                                                                          " & vbNewLine _
                 & "--商品Mの荷主での荷主帳票パターン取得                                                                              " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                                " & vbNewLine _
                 & "ON  GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                                                                               " & vbNewLine _
                 & "AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                                                                               " & vbNewLine _
                 & "AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                                                                               " & vbNewLine _
                 & "AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                                                                               " & vbNewLine _
                 & "AND MCR2.PTN_ID = '23'                                                                                             " & vbNewLine _
                 & "--帳票パターン取得                                                                                                 " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                                      " & vbNewLine _
                 & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                 " & vbNewLine _
                 & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                                       " & vbNewLine _
                 & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                                       " & vbNewLine _
                 & "AND MR2.SYS_DEL_FLG = '0'                                                                                          " & vbNewLine _
                 & "--存在しない場合の帳票パターン取得                                                                                 " & vbNewLine _
                 & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                                      " & vbNewLine _
                 & "ON  MR3.NRS_BR_CD = ZAITRS.NRS_BR_CD                                                                               " & vbNewLine _
                 & "AND MR3.PTN_ID = '23'                                                                                              " & vbNewLine _
                 & "AND MR3.STANDARD_FLAG = '01'                                                                                       " & vbNewLine _
                 & "AND MR3.SYS_DEL_FLG = '0'                                                                                          " & vbNewLine _
                 & "WHERE                                                                                                              " & vbNewLine _
                 & "    ZAITRS.NRS_BR_CD = @NRS_BR_CD                                                                                  " & vbNewLine

    ''' <summary>
    ''' 棚卸対象商品リスト(サブクエリ2:ZAIKOのWHERE句内)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA2 As String = _
                   "AND ZAITRS.PORA_ZAI_NB <> 0                                                                                        " & vbNewLine _
                 & "AND ZAITRS.SYS_DEL_FLG = '0'                                                                                       " & vbNewLine _
                 & "AND ZAITRS.PORA_ZAI_NB <> '0'                                                                                      " & vbNewLine

    'ADD START 2018/11/27 要望管理002837
    ''' <summary>
    ''' 棚卸対象商品リスト(サブクエリ2:ZAIKOの閉じ部分とON句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_ON As String = _
                   ") ZAIKO                                                " & vbNewLine _
                 & " ON EXCEL_GOODS.GOODS_CD_CUST = ZAIKO.GOODS_CD_CUST    " & vbNewLine _
                 & "AND EXCEL_GOODS.CUST_CD_L = ZAIKO.CUST_CD_L            " & vbNewLine
    'ADD END   2018/11/27 要望管理002837

    ''' <summary>
    ''' 棚卸対象商品リスト(ORDER BY句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA3 As String = _
                   "ORDER BY                                                                                                           " & vbNewLine _
                 & "--MOD START 2018/11/27 要望管理002837 テーブル名をサブクエリに変更                                                 " & vbNewLine _
                 & "     ZAIKO.WH_CD                                                                                                   " & vbNewLine _
                 & "    ,ZAIKO.TOU_NO                                                                                                  " & vbNewLine _
                 & "    ,ZAIKO.SITU_NO                                                                                                 " & vbNewLine _
                 & "    ,ZAIKO.ZONE_CD                                                                                                 " & vbNewLine _
                 & "    ,ZAIKO.LOCA                                                                                                    " & vbNewLine _
                 & "    ,ZAIKO.CUST_CD_L                                                                                               " & vbNewLine _
                 & "    ,ZAIKO.GOODS_NM_1                                                                                              " & vbNewLine _
                 & "    ,ZAIKO.LOT_NO                                                                                                  " & vbNewLine _
                 & "--MOD END   2018/11/27 要望管理002837                                                                              " & vbNewLine
    'DEL START 2018/11/27 要望管理002837
    ''           & "     ZAITRS.WH_CD                                                                                                  " & vbNewLine _
    ''           & "    ,ZAITRS.TOU_NO                                                                                                 " & vbNewLine _
    ''           & "    ,ZAITRS.SITU_NO                                                                                                " & vbNewLine _
    ''           & "    ,ZAITRS.ZONE_CD                                                                                                " & vbNewLine _
    ''           & "    ,ZAITRS.LOCA                                                                                                   " & vbNewLine _
    ''           & "    ,ZAITRS.CUST_CD_L                                                                                              " & vbNewLine _
    ''           & "    ,GOODS.GOODS_NM_1                                                                                              " & vbNewLine _
    ''           & "    ,ZAITRS.LOT_NO                                                                                                 " & vbNewLine
    'DEL END 2018/11/27 要望管理002837

    'ADD START 2018/11/27 要望管理002837
    ''' <summary>
    ''' I_WK_RHEM_INVENTORY主キー存在チェックSELECT文
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_REC_CNT As String = _
                   "SELECT COUNT(*) AS REC_CNT                     " & vbNewLine _
                 & "  FROM $LM_TRN$..I_WK_RHEM_INVENTORY WK_INV    " & vbNewLine _
                 & " WHERE WK_INV.CRT_DATE = @CRT_DATE             " & vbNewLine _
                 & "   AND WK_INV.FILE_NAME = @FILE_NAME           " & vbNewLine _
    'ADD END   2018/11/27 要望管理002837
#End Region

    'ADD START 2018/11/27 要望管理002837
#Region "検索処理 INSERT句"

    ''' <summary>
    ''' I_WK_RHEM_INVENTORY登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_EXCEL_DATA As String = _
                   "INSERT INTO $LM_TRN$..I_WK_RHEM_INVENTORY" & vbNewLine _
                 & "(               " & vbNewLine _
                 & " DEL_KB         " & vbNewLine _
                 & ",CRT_DATE       " & vbNewLine _
                 & ",FILE_NAME      " & vbNewLine _
                 & ",REC_NO         " & vbNewLine _
                 & ",NRS_BR_CD      " & vbNewLine _
                 & ",GOODS_CD_CUST  " & vbNewLine _
                 & ",STATUS_FLG     " & vbNewLine _
                 & ",SYS_ENT_DATE   " & vbNewLine _
                 & ",SYS_ENT_TIME   " & vbNewLine _
                 & ",SYS_ENT_PGID   " & vbNewLine _
                 & ",SYS_ENT_USER   " & vbNewLine _
                 & ",SYS_UPD_DATE   " & vbNewLine _
                 & ",SYS_UPD_TIME   " & vbNewLine _
                 & ",SYS_UPD_PGID   " & vbNewLine _
                 & ",SYS_UPD_USER   " & vbNewLine _
                 & ",SYS_DEL_FLG    " & vbNewLine _
                 & ") VALUES (      " & vbNewLine _
                 & " @DEL_KB        " & vbNewLine _
                 & ",@CRT_DATE      " & vbNewLine _
                 & ",@FILE_NAME     " & vbNewLine _
                 & ",@REC_NO        " & vbNewLine _
                 & ",@NRS_BR_CD     " & vbNewLine _
                 & ",@GOODS_CD_CUST " & vbNewLine _
                 & ",@STATUS_FLG    " & vbNewLine _
                 & ",@SYS_ENT_DATE  " & vbNewLine _
                 & ",@SYS_ENT_TIME  " & vbNewLine _
                 & ",@SYS_ENT_PGID  " & vbNewLine _
                 & ",@SYS_ENT_USER  " & vbNewLine _
                 & ",@SYS_UPD_DATE  " & vbNewLine _
                 & ",@SYS_UPD_TIME  " & vbNewLine _
                 & ",@SYS_UPD_PGID  " & vbNewLine _
                 & ",@SYS_UPD_USER  " & vbNewLine _
                 & ",@SYS_DEL_FLG   " & vbNewLine _
                 & ")               " & vbNewLine

#End Region

#Region "検索処理 UPDATE句"

    ''' <summary>
    ''' I_WK_RHEM_INVENTORY更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_EXCEL_DATA_STATUS As String = _
                   "UPDATE $LM_TRN$..I_WK_RHEM_INVENTORY" & vbNewLine _
                 & "SET STATUS_FLG = @STATUS_FLG        " & vbNewLine _
                 & "   ,SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
                 & "   ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
                 & "   ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
                 & "   ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
                 & "WHERE CRT_DATE = @CRT_DATE          " & vbNewLine _
                 & "  AND FILE_NAME = @FILE_NAME        " & vbNewLine

#End Region
    'ADD END   2018/11/27 要望管理002837

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

#Region "SQLメイン処理"

    ''' <summary>
    ''' 検索処理(棚卸対象商品リスト)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI490IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        'ADD START 2018/11/27 要望管理002837
        'SELECT句
        Me._StrSql.Append(LMI490DAC.SQL_SELECT_DATA_SELECT)

        'ワークテーブルのSELECT
        Me._StrSql.Append(LMI490DAC.SQL_SELECT_DATA_EXCEL_GOODS)

        'SQL(条件設定・荷主コード)
        Me._StrSql.Append("        AND M_GOODS.CUST_CD_L IN ")
        Me.SetConditionCustSQL(ds)

        Me._StrSql.Append(LMI490DAC.SQL_SELECT_DATA_EXCEL_GOODS2)
        'ADD END   2018/11/27 要望管理002837

        Me._StrSql.Append(LMI490DAC.SQL_SELECT_DATA)

        'SQL(条件設定・荷主コード)
        Me._StrSql.Append("AND ZAITRS.CUST_CD_L IN ")   'ADD 2018/11/27 要望管理002837
        Me.SetConditionCustSQL(ds)

        'SQL(WHERE句内)
        Me._StrSql.Append(LMI490DAC.SQL_SELECT_DATA2)

        'SQL(条件設定・商品コード)
        Me.SetConditionGoodsSQL(ds)

        'EXCEL_GOODS LEFT JOIN ZAIKO のON句
        Me._StrSql.Append(LMI490DAC.SQL_SELECT_DATA_ON) 'ADD 2018/11/27 要望管理002837

        'SQL(ORDER BY句)
        Me._StrSql.Append(LMI490DAC.SQL_SELECT_DATA3)

        'パラメータ設定
        Call Me.SetSelectDataParameter(ds)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI490DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("OKIBA", "OKIBA")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("REMARK", "REMARK")
        map.Add("IRIME", "IRIME")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("IRISU", "IRISU")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("ALLOC_CAN_NB", "ALLOC_CAN_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI490OUT")

        reader.Close()

        Return ds

    End Function

    'ADD START 2018/11/27 要望管理002837
    ''' <summary>
    ''' I_WK_RHEM_INVENTORY主キー存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectExcelDataCnt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI490IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI490DAC.SQL_SELECT_REC_CNT)

        'パラメータ設定
        Call Me.SetSelectDataParameter(ds)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI490DAC", "SelectExcelDataCnt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' I_WK_RHEM_INVENTORYテーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>I_WK_RHEM_INVENTORYテーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertExcelData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inExcelTbl As DataTable = ds.Tables("LMI490IN_EXCEL")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI490DAC.SQL_INSERT_EXCEL_DATA _
                                                                       , ds.Tables("LMI490IN").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim resCnt As Integer = 0

        Dim max As Integer = inExcelTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = inExcelTbl.Rows(i)

            'パラメータ設定
            Call Me.SetParamInsExcelData(ds)
            Call Me.SetParamSysEnt()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI490DAC", "InsertExcelData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            resCnt += 1

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(resCnt)

        Return ds

    End Function

    ''' <summary>
    ''' I_WK_RHEM_INVENTORYテーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>I_WK_RHEM_INVENTORYテーブル更新SQLの構築・発行</remarks>
    Private Function UpdateExcelDataStatus(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI490IN")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI490DAC.SQL_UPDATE_EXCEL_DATA_STATUS _
                                                                       , inTbl.Rows(0).Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = inTbl.Rows(0)

        'パラメータ設定
        Call Me.SetParamUpdExcelDataStatus(ds)
        Call Me.SetParamSysUpd()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI490DAC", "UpdateExcelDataStatus", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return ds

    End Function
    'ADD END   2018/11/27 要望管理002837

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(荷主コード)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionCustSQL(ByVal ds As DataSet)

        '荷主コード設定
        Dim custKey As String = String.Empty
        Dim max As Integer = ds.Tables("LMI490IN_CUST").Rows.Count - 1
        For i As Integer = 0 To max
            With ds.Tables("LMI490IN_CUST").Rows(i)
                If String.IsNullOrEmpty(custKey) Then
                    custKey = String.Concat("'", .Item("CUST_CD_L").ToString, "'")
                Else
                    custKey = String.Concat(custKey, ",", "'", .Item("CUST_CD_L").ToString, "'")
                End If
            End With
        Next
        custKey = String.Concat("(", custKey, ")")
        '' Me._StrSql.Append("AND ZAITRS.CUST_CD_L IN ")   'DEL START 2018/11/27 要望管理002837
        Me._StrSql.Append(custKey)
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(商品コード)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionGoodsSQL(ByVal ds As DataSet)

        '商品コード設定
        Dim goodsKey As String = String.Empty
        Dim max As Integer = ds.Tables("LMI490IN_EXCEL").Rows.Count - 1
        For i As Integer = 0 To max
            With ds.Tables("LMI490IN_EXCEL").Rows(i)
                '正常データのみで作成
                If .Item("ERR_FLG").ToString = "0" Then
                    If String.IsNullOrEmpty(goodsKey) Then
                        goodsKey = String.Concat("'", .Item("GOODS_CD_CUST").ToString, "'")
                    Else
                        goodsKey = String.Concat(goodsKey, ",", "'", .Item("GOODS_CD_CUST").ToString, "'")
                    End If
                End If
            End With
        Next
        goodsKey = String.Concat("(", goodsKey, ")")
        Me._StrSql.Append("AND GOODS.GOODS_CD_CUST IN ")
        Me._StrSql.Append(goodsKey)
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(棚卸対象商品リスト)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectDataParameter(ByVal ds As DataSet)

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            'ADD START 2018/11/27 要望管理002837
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
            'ADD END   2018/11/27 要望管理002837

        End With

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

    'ADD START 2018/11/27 要望管理002837
    ''' <summary>
    ''' パラメータ設定(登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsExcelData(ByVal ds As DataSet)

        Dim inRow As DataRow = ds.Tables("LMI490IN").Rows(0)

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me._Row.Item("ERR_FLG").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", inRow.Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", inRow.Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", Me._Row.Item("REC_NO").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", Me._Row.Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STATUS_FLG", STATUS_IMPORTED, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定(更新用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdExcelDataStatus(ByVal ds As DataSet)

        Dim inRow As DataRow = ds.Tables("LMI490IN").Rows(0)

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STATUS_FLG", STATUS_PROCESSED, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", inRow.Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", inRow.Item("FILE_NAME").ToString(), DBDataType.VARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSysEnt()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetParamSysUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSysUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub
    'ADD END   2018/11/27 要望管理002837

#End Region

    'ADD START 2018/11/27 要望管理002837
#Region "UPDATE文の発行"
    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        Dim resCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resCnt)

        If resCnt < 1 Then
            Return False
        End If

        Return True

    End Function
#End Region
    'ADD END   2018/11/27 要望管理002837

#End Region

End Class
