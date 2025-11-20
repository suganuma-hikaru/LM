' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD621    : 消防分類別在庫重量表
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD621DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD621DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "検索処理 SQL(SELECT句)"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                         " & vbNewLine _
                                            & "	 ZAIKO.NRS_BR_CD                                          AS NRS_BR_CD " & vbNewLine _
                                            & ", 'BF'                                                     AS PTN_ID    " & vbNewLine _
                                            & ", CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
                                            & "	      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                      " & vbNewLine _
                                            & "  ELSE      MR3.PTN_CD END                                 AS PTN_CD    " & vbNewLine _
                                            & ", CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "       WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                      " & vbNewLine _
                                            & "  ELSE      MR3.RPT_ID END                                 AS RPT_ID    " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                                             " & vbNewLine _
                                            & "  CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                 " & vbNewLine _
                                            & "  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                      " & vbNewLine _
                                            & "  ELSE                                  MR3.RPT_ID                                                 " & vbNewLine _
                                            & "  END                                                            AS RPT_ID       --帳票ID          " & vbNewLine _
                                            & ", ZAIKO.NRS_BR_CD                                                AS NRS_BR_CD    --営業所コード    " & vbNewLine _
                                            & ", NRS_BR.NRS_BR_NM                                               AS NRS_BR_NM    --営業所名        " & vbNewLine _
                                            & ", SOKO.WH_NM                                                     AS WH_NM        --倉庫名          " & vbNewLine _
                                            & ", ZAIKO.CUST_CD_L                                                AS CUST_CD_L    --荷主コード(大)  " & vbNewLine _
                                            & ", ZAIKO.CUST_CD_M                                                AS CUST_CD_M    --荷主コード(中)  " & vbNewLine _
                                            & ", ZAI_TRS.TOU_NO                                                 AS TOU_NO       --棟              " & vbNewLine _
                                            & ", ZAI_TRS.SITU_NO                                                AS SITU_NO      --室              " & vbNewLine _
                                            & ", GOODS.SHOBO_CD                                                 AS SHOBO_CD     --消防コード      " & vbNewLine _
                                            & ", CASE WHEN ISNULL(SHOBO.HINMEI,'') = '' THEN '一般品'                                             " & vbNewLine _
                                            & "  ELSE                                        SHOBO.HINMEI                                         " & vbNewLine _
                                            & "  END                                                            AS SHOBO_NM     --消防品名        " & vbNewLine _
                                            & ", ISNULL(SHOBO.RUI,'')                                           AS SHOBO_RUI_CD --消防類コード    " & vbNewLine _
                                            & ", CASE WHEN ISNULL(KBN.KBN_NM1,'') = '' THEN '一般品'                                              " & vbNewLine _
                                            & "  ELSE                                       KBN.KBN_NM1                                           " & vbNewLine _
                                            & "  END                                                            AS SHOBO_RUI_NM --消防類品名      " & vbNewLine _
                                            & ", CASE WHEN ISNULL(GOODS.SHOBO_CD,'') = ''          THEN 0                                         " & vbNewLine _
                                            & "       WHEN ISNULL(TOU_SITU_SHOBO.SHOBO_CD,'') = '' THEN 1                                         " & vbNewLine _
                                            & "  ELSE                                                   0                                         " & vbNewLine _
                                            & "  END                                                            AS ZEN_NB       --前月重量        " & vbNewLine _
                                            & ", SUM(ZAIKO.PORA_ZAI_QT * GOODS.STD_WT_KGS / GOODS.STD_IRIME_NB) AS TOU_WT       --重量            " & vbNewLine _
                                            & ", SUM(ZAIKO.PORA_ZAI_NB)                                         AS NB           --個数            " & vbNewLine _
                                            & ", SUM(ZAIKO.PORA_ZAI_QT)                                         AS QT           --数量            " & vbNewLine _
                                            & ", @PRINT_FROM                                                    AS PRINT_FROM   --印刷日          " & vbNewLine

#End Region

#Region "検索処理 SQL(FROM句)"

    ''' <summary>
    ''' データ抽出用FROM句(帳票種別取得用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_MPrt As String = "FROM                                         " & vbNewLine _
                                          & "    $LM_TRN$..D_ZAI_TRS ZAIKO                " & vbNewLine _
                                          & "LEFT JOIN                                    " & vbNewLine _
                                          & "    $LM_MST$..M_GOODS GOODS                  " & vbNewLine _
                                          & " ON ZAIKO.NRS_BR_CD = GOODS.NRS_BR_CD        " & vbNewLine _
                                          & "AND ZAIKO.GOODS_CD_NRS = GOODS.GOODS_CD_NRS  " & vbNewLine _
                                          & " --在庫の荷主での荷主帳票パターン取得        " & vbNewLine _
                                          & " LEFT JOIN                                   " & vbNewLine _
                                          & "     $LM_MST$..M_CUST_RPT MCR1               " & vbNewLine _
                                          & "  ON ZAIKO.NRS_BR_CD = MCR1.NRS_BR_CD        " & vbNewLine _
                                          & " AND ZAIKO.CUST_CD_L = MCR1.CUST_CD_L        " & vbNewLine _
                                          & " AND ZAIKO.CUST_CD_M = MCR1.CUST_CD_M        " & vbNewLine _
                                          & " AND MCR1.CUST_CD_S = '00'                   " & vbNewLine _
                                          & " AND MCR1.PTN_ID = 'BF'                      " & vbNewLine _
                                          & " AND MCR1.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                          & " --帳票パターン取得                          " & vbNewLine _
                                          & " LEFT JOIN                                   " & vbNewLine _
                                          & "     $LM_MST$..M_RPT MR1                     " & vbNewLine _
                                          & "  ON MCR1.NRS_BR_CD = MR1.NRS_BR_CD          " & vbNewLine _
                                          & " AND MCR1.PTN_ID = MR1.PTN_ID                " & vbNewLine _
                                          & " AND MCR1.PTN_CD = MR1.PTN_CD                " & vbNewLine _
                                          & " AND MR1.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                          & " --商品Mの荷主での荷主帳票パターン取得       " & vbNewLine _
                                          & " LEFT JOIN                                   " & vbNewLine _
                                          & "     $LM_MST$..M_CUST_RPT MCR2               " & vbNewLine _
                                          & "  ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD        " & vbNewLine _
                                          & " AND GOODS.CUST_CD_L = MCR2.CUST_CD_L        " & vbNewLine _
                                          & " AND GOODS.CUST_CD_M = MCR2.CUST_CD_M        " & vbNewLine _
                                          & " AND GOODS.CUST_CD_S = MCR2.CUST_CD_S        " & vbNewLine _
                                          & " AND MCR2.PTN_ID = 'BF'                      " & vbNewLine _
                                          & " AND MCR2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                          & " --帳票パターン取得                          " & vbNewLine _
                                          & " LEFT JOIN                                   " & vbNewLine _
                                          & "     $LM_MST$..M_RPT MR2                     " & vbNewLine _
                                          & "  ON MCR2.NRS_BR_CD = MR2.NRS_BR_CD          " & vbNewLine _
                                          & " AND MCR2.PTN_ID = MR2.PTN_ID                " & vbNewLine _
                                          & " AND MCR2.PTN_CD = MR2.PTN_CD                " & vbNewLine _
                                          & " AND MR2.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                          & " --存在しない場合の帳票パターン取得          " & vbNewLine _
                                          & " LEFT JOIN                                   " & vbNewLine _
                                          & "     $LM_MST$..M_RPT MR3                     " & vbNewLine _
                                          & "  ON ZAIKO.NRS_BR_CD = MR3.NRS_BR_CD         " & vbNewLine _
                                          & " AND MR3.PTN_ID = 'BF'                       " & vbNewLine _
                                          & " AND MR3.STANDARD_FLAG = '01'                " & vbNewLine _
                                          & " AND MR3.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                          & "WHERE                                        " & vbNewLine _
                                          & "    ZAIKO.NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
                                          & "AND ZAIKO.CUST_CD_L = @CUST_CD_L             " & vbNewLine _
                                          & "AND ZAIKO.CUST_CD_M = @CUST_CD_M             " & vbNewLine

    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM(                                              " & vbNewLine _
                                     & "     SELECT                                        " & vbNewLine _
                                     & "       NRS_BR_CD        AS NRS_BR_CD               " & vbNewLine _
                                     & "     , CUST_CD_L        AS CUST_CD_L               " & vbNewLine _
                                     & "     , CUST_CD_M        AS CUST_CD_M               " & vbNewLine _
                                     & "     , ZAI_REC_NO       AS ZAI_REC_NO              " & vbNewLine _
                                     & "     , GOODS_CD_NRS     AS GOODS_CD_NRS            " & vbNewLine _
                                     & "     , SUM(PORA_ZAI_NB) AS PORA_ZAI_NB             " & vbNewLine _
                                     & "     , SUM(PORA_ZAI_QT) AS PORA_ZAI_QT             " & vbNewLine _
                                     & "     FROM                                          " & vbNewLine _
                                     & "         $LM_TRN$..D_WK_SHOBO_PRT                  " & vbNewLine _
                                     & "     WHERE                                         " & vbNewLine _
                                     & "         NRS_BR_CD   = @NRS_BR_CD                  " & vbNewLine _
                                     & "     AND CUST_CD_L   = @CUST_CD_L                  " & vbNewLine _
                                     & "     AND CUST_CD_M   = @CUST_CD_M                  " & vbNewLine _
                                     & "     AND PRINT_FROM  = @PRINT_FROM                 " & vbNewLine _
                                     & "     AND ZAI_GETU    = ''                          " & vbNewLine _
                                     & "     AND GETU_FLG    = @GETU_FLG                   " & vbNewLine _
                                     & "     GROUP BY                                      " & vbNewLine _
                                     & "       NRS_BR_CD                                   " & vbNewLine _
                                     & "     , CUST_CD_L                                   " & vbNewLine _
                                     & "     , CUST_CD_M                                   " & vbNewLine _
                                     & "     , ZAI_REC_NO                                  " & vbNewLine _
                                     & "     , GOODS_CD_NRS                                " & vbNewLine _
                                     & "     ) ZAIKO                                       " & vbNewLine _
                                     & "LEFT JOIN                                          " & vbNewLine _
                                     & "    $LM_TRN$..D_ZAI_TRS ZAI_TRS                    " & vbNewLine _
                                     & " ON ZAIKO.NRS_BR_CD   = ZAI_TRS.NRS_BR_CD          " & vbNewLine _
                                     & "AND ZAIKO.ZAI_REC_NO  = ZAI_TRS.ZAI_REC_NO         " & vbNewLine _
                                     & "AND ZAI_TRS.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                     & "LEFT JOIN                                          " & vbNewLine _
                                     & "    $LM_MST$..M_NRS_BR NRS_BR                      " & vbNewLine _
                                     & " ON ZAIKO.NRS_BR_CD    = NRS_BR.NRS_BR_CD          " & vbNewLine _
                                     & "AND NRS_BR.SYS_DEL_FLG = '0'                       " & vbNewLine _
                                     & "LEFT JOIN                                          " & vbNewLine _
                                     & "    $LM_MST$..M_SOKO SOKO                          " & vbNewLine _
                                     & " ON ZAI_TRS.NRS_BR_CD = SOKO.NRS_BR_CD             " & vbNewLine _
                                     & "AND ZAI_TRS.WH_CD = SOKO.WH_CD                     " & vbNewLine _
                                     & "AND SOKO.SYS_DEL_FLG = '0'                         " & vbNewLine _
                                     & "LEFT JOIN                                          " & vbNewLine _
                                     & "    $LM_MST$..M_GOODS GOODS                        " & vbNewLine _
                                     & " ON ZAIKO.NRS_BR_CD    = GOODS.NRS_BR_CD           " & vbNewLine _
                                     & "AND ZAIKO.GOODS_CD_NRS = GOODS.GOODS_CD_NRS        " & vbNewLine _
                                     & "LEFT JOIN                                          " & vbNewLine _
                                     & "    $LM_MST$..M_SHOBO SHOBO                        " & vbNewLine _
                                     & " ON GOODS.SHOBO_CD = SHOBO.SHOBO_CD                " & vbNewLine _
                                     & "AND SHOBO.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                     & "LEFT JOIN                                          " & vbNewLine _
                                     & "    $LM_MST$..M_TOU_SITU_SHOBO TOU_SITU_SHOBO      " & vbNewLine _
                                     & " ON ZAI_TRS.NRS_BR_CD = TOU_SITU_SHOBO.NRS_BR_CD   " & vbNewLine _
                                     & "AND ZAI_TRS.WH_CD = TOU_SITU_SHOBO.WH_CD           " & vbNewLine _
                                     & "AND ZAI_TRS.TOU_NO = TOU_SITU_SHOBO.TOU_NO         " & vbNewLine _
                                     & "AND ZAI_TRS.SITU_NO = TOU_SITU_SHOBO.SITU_NO       " & vbNewLine _
                                     & "AND SHOBO.SHOBO_CD = TOU_SITU_SHOBO.SHOBO_CD       " & vbNewLine _
                                     & "AND TOU_SITU_SHOBO.SYS_DEL_FLG = '0'               " & vbNewLine _
                                     & "LEFT JOIN                                          " & vbNewLine _
                                     & "    $LM_MST$..Z_KBN KBN                            " & vbNewLine _
                                     & " ON KBN.KBN_GROUP_CD = 'S004'                      " & vbNewLine _
                                     & "AND SHOBO.RUI = KBN.KBN_CD                         " & vbNewLine _
                                     & "AND KBN.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                     & "--在庫の荷主での荷主帳票パターン取得               " & vbNewLine _
                                     & "LEFT JOIN                                          " & vbNewLine _
                                     & "    $LM_MST$..M_CUST_RPT MCR1                      " & vbNewLine _
                                     & " ON ZAIKO.NRS_BR_CD = MCR1.NRS_BR_CD               " & vbNewLine _
                                     & "AND ZAIKO.CUST_CD_L = MCR1.CUST_CD_L               " & vbNewLine _
                                     & "AND ZAIKO.CUST_CD_M = MCR1.CUST_CD_M               " & vbNewLine _
                                     & "AND MCR1.CUST_CD_S = '00'                          " & vbNewLine _
                                     & "AND MCR1.PTN_ID = 'BF'                             " & vbNewLine _
                                     & "AND MCR1.SYS_DEL_FLG = '0'                         " & vbNewLine _
                                     & "--帳票パターン取得                                 " & vbNewLine _
                                     & "LEFT JOIN                                          " & vbNewLine _
                                     & "    $LM_MST$..M_RPT MR1                            " & vbNewLine _
                                     & " ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                 " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                       " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                       " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得              " & vbNewLine _
                                     & "LEFT JOIN                                          " & vbNewLine _
                                     & "    $LM_MST$..M_CUST_RPT MCR2                      " & vbNewLine _
                                     & " ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD               " & vbNewLine _
                                     & "AND GOODS.CUST_CD_L = MCR2.CUST_CD_L               " & vbNewLine _
                                     & "AND GOODS.CUST_CD_M = MCR2.CUST_CD_M               " & vbNewLine _
                                     & "AND GOODS.CUST_CD_S = MCR2.CUST_CD_S               " & vbNewLine _
                                     & "AND MCR2.PTN_ID = 'BF'                             " & vbNewLine _
                                     & "AND MCR2.SYS_DEL_FLG = '0'                         " & vbNewLine _
                                     & "--帳票パターン取得                                 " & vbNewLine _
                                     & "LEFT JOIN                                          " & vbNewLine _
                                     & "    $LM_MST$..M_RPT MR2                            " & vbNewLine _
                                     & " ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                 " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                       " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                       " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                 " & vbNewLine _
                                     & "LEFT JOIN                                          " & vbNewLine _
                                     & "    $LM_MST$..M_RPT MR3                            " & vbNewLine _
                                     & " ON MR3.NRS_BR_CD = ZAIKO.NRS_BR_CD                " & vbNewLine _
                                     & "AND MR3.PTN_ID = 'BF'                              " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                       " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                          " & vbNewLine

#End Region

#Region "検索処理 SQL(WHERE句)"

    ''' <summary>
    ''' データ抽出用WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE As String = "WHERE                       " & vbNewLine _
                                      & "    ZAIKO.PORA_ZAI_NB <> 0  " & vbNewLine

#End Region

#Region "検索処理 SQL(GROUP BY句)"

    ''' <summary>
    ''' データ抽出用GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = "GROUP BY                                            " & vbNewLine _
                                         & "  CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID  " & vbNewLine _
                                         & "       WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID  " & vbNewLine _
                                         & "  ELSE                                  MR3.RPT_ID  " & vbNewLine _
                                         & "  END                                               " & vbNewLine _
                                         & ", ZAIKO.NRS_BR_CD                                   " & vbNewLine _
                                         & ", NRS_BR.NRS_BR_NM                                  " & vbNewLine _
                                         & ", SOKO.WH_NM                                        " & vbNewLine _
                                         & ", ZAIKO.CUST_CD_L                                   " & vbNewLine _
                                         & ", ZAIKO.CUST_CD_M                                   " & vbNewLine _
                                         & ", ZAI_TRS.TOU_NO                                    " & vbNewLine _
                                         & ", ZAI_TRS.SITU_NO                                   " & vbNewLine _
                                         & ", GOODS.SHOBO_CD                                    " & vbNewLine _
                                         & ", SHOBO.HINMEI                                      " & vbNewLine _
                                         & ", SHOBO.RUI                                         " & vbNewLine _
                                         & ", KBN.KBN_NM1                                       " & vbNewLine _
                                         & ", GOODS.SHOBO_CD                                    " & vbNewLine _
                                         & ", TOU_SITU_SHOBO.SHOBO_CD                           " & vbNewLine

#End Region

#Region "検索処理 SQL(ORDER BY句)"

    ''' <summary>
    ''' データ抽出用ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY           " & vbNewLine _
                                         & "  ZAI_TRS.TOU_NO   " & vbNewLine _
                                         & ", ZAI_TRS.SITU_NO  " & vbNewLine

#End Region

#End Region

#Region "ワークテーブル関連 SQL"

#Region "削除用SQL"

    ''' <summary>
    ''' ワークテーブルデータ削除
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_WK As String = " DELETE                         " & vbNewLine _
                                          & " FROM                           " & vbNewLine _
                                          & "      $LM_TRN$..D_WK_SHOBO_PRT  " & vbNewLine _
                                          & " WHERE                          " & vbNewLine _
                                          & "     NRS_BR_CD   = @NRS_BR_CD   " & vbNewLine _
                                          & " AND CUST_CD_L   = @CUST_CD_L   " & vbNewLine _
                                          & " AND CUST_CD_M   = @CUST_CD_M   " & vbNewLine _
                                          & " AND PRINT_FROM  = @PRINT_FROM  " & vbNewLine _
                                          & " AND ZAI_GETU    = ''           " & vbNewLine _
                                          & " AND GETU_FLG    = @GETU_FLG    " & vbNewLine

#End Region

#Region "SQL_INSERT_HED"

    ''' <summary>
    ''' ワークテーブルのINSERT文（共通）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_HED As String = " INSERT INTO $LM_TRN$..D_WK_SHOBO_PRT " & vbNewLine _
                                           & " (                                    " & vbNewLine _
                                           & "     NRS_BR_CD                        " & vbNewLine _
                                           & "    ,CUST_CD_L                        " & vbNewLine _
                                           & "    ,CUST_CD_M                        " & vbNewLine _
                                           & "    ,PRINT_FROM                       " & vbNewLine _
                                           & "    ,ZAI_GETU                         " & vbNewLine _
                                           & "    ,GETU_FLG                         " & vbNewLine _
                                           & "    ,OUTKA_NO_L                       " & vbNewLine _
                                           & "    ,OUTKA_NO_M                       " & vbNewLine _
                                           & "    ,OUTKA_NO_S                       " & vbNewLine _
                                           & "    ,CUST_NM_L                        " & vbNewLine _
                                           & "    ,ZAI_REC_NO                       " & vbNewLine _
                                           & "    ,GOODS_CD_NRS                     " & vbNewLine _
                                           & "    ,GOODS_CD_CUST                    " & vbNewLine _
                                           & "    ,LOT_NO                           " & vbNewLine _
                                           & "    ,SERIAL_NO                        " & vbNewLine _
                                           & "    ,PORA_ZAI_NB                      " & vbNewLine _
                                           & "    ,PORA_ZAI_QT                      " & vbNewLine _
                                           & "    ,UNION_NO                         " & vbNewLine _
                                           & "    ,SYS_ENT_DATE                     " & vbNewLine _
                                           & "    ,SYS_ENT_TIME                     " & vbNewLine _
                                           & "    ,SYS_ENT_PGID                     " & vbNewLine _
                                           & "    ,SYS_ENT_USER                     " & vbNewLine _
                                           & "    ,SYS_UPD_DATE                     " & vbNewLine _
                                           & "    ,SYS_UPD_TIME                     " & vbNewLine _
                                           & "    ,SYS_UPD_PGID                     " & vbNewLine _
                                           & "    ,SYS_UPD_USER                     " & vbNewLine _
                                           & "    ,SYS_DEL_FLG                      " & vbNewLine _
                                           & " )                                    " & vbNewLine

#End Region

#Region "SQL_INSERT_UNION_2"

    Private Const SQL_INSERT_UNION_2 As String = "SELECT                                                                          " & vbNewLine _
                                               & "  @NRS_BR_CD                                                  AS NRS_BR_CD      " & vbNewLine _
                                               & ", @CUST_CD_L                                                  AS CUST_CD_L      " & vbNewLine _
                                               & ", @CUST_CD_M                                                  AS CUST_CD_M      " & vbNewLine _
                                               & ", @PRINT_FROM                                                 AS PRINT_FROM     " & vbNewLine _
                                               & ", ''                                                          AS ZAI_GETU       " & vbNewLine _
                                               & ", @GETU_FLG                                                   AS GETU_FLG       " & vbNewLine _
                                               & ", ''                                                          AS OUTKA_NO_L     " & vbNewLine _
                                               & ", ''                                                          AS OUTKA_NO_M     " & vbNewLine _
                                               & ", ''                                                          AS OUTKA_NO_S     " & vbNewLine _
                                               & ", ''                                                          AS CUST_NM_L      " & vbNewLine _
                                               & ", MAIN.ZAI_REC_NO                                             AS ZAI_REC_NO     " & vbNewLine _
                                               & ", MAIN.GOODS_CD_NRS                                           AS GOODS_CD_NRS   " & vbNewLine _
                                               & ", ''                                                          AS GOODS_CD_CUST  " & vbNewLine _
                                               & ", ''                                                          AS LOT_NO         " & vbNewLine _
                                               & ", ''                                                          AS SERIAL_NO      " & vbNewLine _
                                               & ", SUM((MAIN.KONSU * GOODS.PKG_NB) + MAIN.HASU)                AS PORA_ZAI_NB    " & vbNewLine _
                                               & ", SUM(((MAIN.KONSU * GOODS.PKG_NB) + MAIN.HASU) * MAIN.IRIME) AS PORA_ZAI_QT    " & vbNewLine _
                                               & ", '2'                                                         AS UNION_NO       " & vbNewLine _
                                               & ", @SYS_ENT_DATE                                               AS SYS_ENT_DATE   " & vbNewLine _
                                               & ", @SYS_ENT_TIME                                               AS SYS_ENT_TIME   " & vbNewLine _
                                               & ", @SYS_ENT_PGID                                               AS SYS_ENT_PGID   " & vbNewLine _
                                               & ", @SYS_ENT_USER                                               AS SYS_ENT_USER   " & vbNewLine _
                                               & ", @SYS_UPD_DATE                                               AS SYS_UPD_DATE   " & vbNewLine _
                                               & ", @SYS_UPD_TIME                                               AS SYS_UPD_TIME   " & vbNewLine _
                                               & ", @SYS_UPD_PGID                                               AS SYS_UPD_PGID   " & vbNewLine _
                                               & ", @SYS_UPD_USER                                               AS SYS_UPD_USER   " & vbNewLine _
                                               & ", '0'                                                         AS SYS_DEL_FLG    " & vbNewLine _
                                               & "FROM(                                                                           " & vbNewLine _
                                               & "     SELECT                       --入荷データ                                  " & vbNewLine _
                                               & "       INKA_L.NRS_BR_CD             AS NRS_BR_CD                                " & vbNewLine _
                                               & "     , INKA_S.ZAI_REC_NO            AS ZAI_REC_NO                               " & vbNewLine _
                                               & "     , INKA_M.GOODS_CD_NRS          AS GOODS_CD_NRS                             " & vbNewLine _
                                               & "     , INKA_S.KONSU                 AS KONSU                                    " & vbNewLine _
                                               & "     , INKA_S.HASU                  AS HASU                                     " & vbNewLine _
                                               & "     , INKA_S.IRIME                 AS IRIME                                    " & vbNewLine _
                                               & "     FROM                                                                       " & vbNewLine _
                                               & "         $LM_TRN$..B_INKA_L INKA_L                                              " & vbNewLine _
                                               & "     LEFT JOIN                                                                  " & vbNewLine _
                                               & "         $LM_TRN$..B_INKA_M INKA_M                                              " & vbNewLine _
                                               & "      ON INKA_L.NRS_BR_CD = INKA_M.NRS_BR_CD                                    " & vbNewLine _
                                               & "     AND INKA_L.INKA_NO_L = INKA_M.INKA_NO_L                                    " & vbNewLine _
                                               & "     AND INKA_M.SYS_DEL_FLG = '0'	                                              " & vbNewLine _
                                               & "     LEFT JOIN                                                                  " & vbNewLine _
                                               & "         $LM_TRN$..B_INKA_S INKA_S                                              " & vbNewLine _
                                               & "      ON INKA_M.NRS_BR_CD = INKA_S.NRS_BR_CD                                    " & vbNewLine _
                                               & "     AND INKA_M.INKA_NO_L = INKA_S.INKA_NO_L                                    " & vbNewLine _
                                               & "     AND INKA_M.INKA_NO_M = INKA_S.INKA_NO_M                                    " & vbNewLine _
                                               & "     AND INKA_S.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                               & "     WHERE                                                                      " & vbNewLine _
                                               & "         INKA_L.NRS_BR_CD = @NRS_BR_CD                                          " & vbNewLine _
                                               & "     AND INKA_L.CUST_CD_L = @CUST_CD_L                                          " & vbNewLine _
                                               & "     AND INKA_L.CUST_CD_M = @CUST_CD_M                                          " & vbNewLine _
                                               & "     AND INKA_L.INKA_DATE <= @PRINT_FROM                                        " & vbNewLine _
                                               & "     AND(INKA_L.INKA_DATE > '00000000'                                          " & vbNewLine _
                                               & "      OR INKA_L.INKA_STATE_KB < '50')                                           " & vbNewLine _
                                               & "     AND(INKA_L.INKA_STATE_KB > '10'                                            " & vbNewLine _
                                               & "      OR INKA_S.ZAI_REC_NO <> '')                                               " & vbNewLine _
                                               & "     AND INKA_L.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                               & "     )MAIN                                                                      " & vbNewLine _
                                               & "LEFT JOIN                                                                       " & vbNewLine _
                                               & "    $LM_MST$..M_GOODS GOODS                                                     " & vbNewLine _
                                               & " ON MAIN.NRS_BR_CD = GOODS.NRS_BR_CD                                            " & vbNewLine _
                                               & "AND MAIN.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                      " & vbNewLine _
                                               & "GROUP BY                                                                        " & vbNewLine _
                                               & "  MAIN.ZAI_REC_NO                                                               " & vbNewLine _
                                               & ", MAIN.GOODS_CD_NRS                                                             " & vbNewLine




#End Region

#Region "SQL_INSERT_UNION_3"

    Private Const SQL_INSERT_UNION_3 As String = "SELECT                                                                          " & vbNewLine _
                                               & "  @NRS_BR_CD                                                  AS NRS_BR_CD      " & vbNewLine _
                                               & ", @CUST_CD_L                                                  AS CUST_CD_L      " & vbNewLine _
                                               & ", @CUST_CD_M                                                  AS CUST_CD_M      " & vbNewLine _
                                               & ", @PRINT_FROM                                                 AS PRINT_FROM     " & vbNewLine _
                                               & ", ''                                                          AS ZAI_GETU       " & vbNewLine _
                                               & ", @GETU_FLG                                                   AS GETU_FLG       " & vbNewLine _
                                               & ", ''                                                          AS OUTKA_NO_L     " & vbNewLine _
                                               & ", ''                                                          AS OUTKA_NO_M     " & vbNewLine _
                                               & ", ''                                                          AS OUTKA_NO_S     " & vbNewLine _
                                               & ", ''                                                          AS CUST_NM_L      " & vbNewLine _
                                               & ", MAIN.ZAI_REC_NO                                             AS ZAI_REC_NO     " & vbNewLine _
                                               & ", MAIN.GOODS_CD_NRS                                           AS GOODS_CD_NRS   " & vbNewLine _
                                               & ", ''                                                          AS GOODS_CD_CUST  " & vbNewLine _
                                               & ", ''                                                          AS LOT_NO         " & vbNewLine _
                                               & ", ''                                                          AS SERIAL_NO      " & vbNewLine _
                                               & ", SUM(MAIN.PORA_ZAI_NB)                                       AS PORA_ZAI_NB    " & vbNewLine _
                                               & ", SUM(MAIN.PORA_ZAI_NB * MAIN.IRIME)                          AS PORA_ZAI_QT    " & vbNewLine _
                                               & ", '3'                                                         AS UNION_NO       " & vbNewLine _
                                               & ", @SYS_ENT_DATE                                               AS SYS_ENT_DATE   " & vbNewLine _
                                               & ", @SYS_ENT_TIME                                               AS SYS_ENT_TIME   " & vbNewLine _
                                               & ", @SYS_ENT_PGID                                               AS SYS_ENT_PGID   " & vbNewLine _
                                               & ", @SYS_ENT_USER                                               AS SYS_ENT_USER   " & vbNewLine _
                                               & ", @SYS_UPD_DATE                                               AS SYS_UPD_DATE   " & vbNewLine _
                                               & ", @SYS_UPD_TIME                                               AS SYS_UPD_TIME   " & vbNewLine _
                                               & ", @SYS_UPD_PGID                                               AS SYS_UPD_PGID   " & vbNewLine _
                                               & ", @SYS_UPD_USER                                               AS SYS_UPD_USER   " & vbNewLine _
                                               & ", '0'                                                         AS SYS_DEL_FLG    " & vbNewLine _
                                               & "FROM(                                                                           " & vbNewLine _
                                               & "     SELECT                       --移動後                                      " & vbNewLine _
                                               & "       IDO1.NRS_BR_CD            AS NRS_BR_CD                                   " & vbNewLine _
                                               & "     , IDO1.N_ZAI_REC_NO         AS ZAI_REC_NO                                  " & vbNewLine _
                                               & "     , ZAI1.GOODS_CD_NRS         AS GOODS_CD_NRS                                " & vbNewLine _
                                               & "     , IDO1.N_PORA_ZAI_NB        AS PORA_ZAI_NB                                 " & vbNewLine _
                                               & "     , IDO1.O_IRIME              AS IRIME                                       " & vbNewLine _
                                               & "     FROM                                                                       " & vbNewLine _
                                               & "         $LM_TRN$..D_IDO_TRS IDO1                                               " & vbNewLine _
                                               & "     LEFT JOIN                                                                  " & vbNewLine _
                                               & "         $LM_TRN$..D_ZAI_TRS ZAI1                                               " & vbNewLine _
                                               & "      ON IDO1.NRS_BR_CD = ZAI1.NRS_BR_CD                                        " & vbNewLine _
                                               & "     AND IDO1.N_ZAI_REC_NO = ZAI1.ZAI_REC_NO                                    " & vbNewLine _
                                               & "     AND ZAI1.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                               & "     WHERE                                                                      " & vbNewLine _
                                               & "         IDO1.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                                               & "     AND ZAI1.CUST_CD_L = @CUST_CD_L                                            " & vbNewLine _
                                               & "     AND ZAI1.CUST_CD_M = @CUST_CD_M                                            " & vbNewLine _
                                               & "     AND IDO1.IDO_DATE <= @PRINT_FROM                                           " & vbNewLine _
                                               & "     AND IDO1.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                               & "     )MAIN                                                                      " & vbNewLine _
                                               & "GROUP BY                                                                        " & vbNewLine _
                                               & "  MAIN.ZAI_REC_NO                                                               " & vbNewLine _
                                               & ", MAIN.GOODS_CD_NRS                                                             " & vbNewLine


#End Region

#Region "SQL_INSERT_UNION_4"

    Private Const SQL_INSERT_UNION_4 As String = "SELECT                                                                          " & vbNewLine _
                                               & "  @NRS_BR_CD                                                  AS NRS_BR_CD      " & vbNewLine _
                                               & ", @CUST_CD_L                                                  AS CUST_CD_L      " & vbNewLine _
                                               & ", @CUST_CD_M                                                  AS CUST_CD_M      " & vbNewLine _
                                               & ", @PRINT_FROM                                                 AS PRINT_FROM     " & vbNewLine _
                                               & ", ''                                                          AS ZAI_GETU       " & vbNewLine _
                                               & ", @GETU_FLG                                                   AS GETU_FLG       " & vbNewLine _
                                               & ", ''                                                          AS OUTKA_NO_L     " & vbNewLine _
                                               & ", ''                                                          AS OUTKA_NO_M     " & vbNewLine _
                                               & ", ''                                                          AS OUTKA_NO_S     " & vbNewLine _
                                               & ", ''                                                          AS CUST_NM_L      " & vbNewLine _
                                               & ", MAIN.ZAI_REC_NO                                             AS ZAI_REC_NO     " & vbNewLine _
                                               & ", MAIN.GOODS_CD_NRS                                           AS GOODS_CD_NRS   " & vbNewLine _
                                               & ", ''                                                          AS GOODS_CD_CUST  " & vbNewLine _
                                               & ", ''                                                          AS LOT_NO         " & vbNewLine _
                                               & ", ''                                                          AS SERIAL_NO      " & vbNewLine _
                                               & ", SUM(MAIN.PORA_ZAI_NB) * -1                                  AS PORA_ZAI_NB    " & vbNewLine _
                                               & ", SUM(MAIN.PORA_ZAI_NB * MAIN.IRIME) * -1                     AS PORA_ZAI_QT    " & vbNewLine _
                                               & ", '4'                                                         AS UNION_NO       " & vbNewLine _
                                               & ", @SYS_ENT_DATE                                               AS SYS_ENT_DATE   " & vbNewLine _
                                               & ", @SYS_ENT_TIME                                               AS SYS_ENT_TIME   " & vbNewLine _
                                               & ", @SYS_ENT_PGID                                               AS SYS_ENT_PGID   " & vbNewLine _
                                               & ", @SYS_ENT_USER                                               AS SYS_ENT_USER   " & vbNewLine _
                                               & ", @SYS_UPD_DATE                                               AS SYS_UPD_DATE   " & vbNewLine _
                                               & ", @SYS_UPD_TIME                                               AS SYS_UPD_TIME   " & vbNewLine _
                                               & ", @SYS_UPD_PGID                                               AS SYS_UPD_PGID   " & vbNewLine _
                                               & ", @SYS_UPD_USER                                               AS SYS_UPD_USER   " & vbNewLine _
                                               & ", '0'                                                         AS SYS_DEL_FLG    " & vbNewLine _
                                               & "FROM(                                                                           " & vbNewLine _
                                               & "     SELECT                       --移動前                                      " & vbNewLine _
                                               & "       IDO2.NRS_BR_CD            AS NRS_BR_CD                                   " & vbNewLine _
                                               & "     , IDO2.O_ZAI_REC_NO         AS ZAI_REC_NO                                  " & vbNewLine _
                                               & "     , ZAI2.GOODS_CD_NRS         AS GOODS_CD_NRS                                " & vbNewLine _
                                               & "     , IDO2.N_PORA_ZAI_NB        AS PORA_ZAI_NB                                 " & vbNewLine _
                                               & "     , IDO2.O_IRIME              AS IRIME                                       " & vbNewLine _
                                               & "     FROM                                                                       " & vbNewLine _
                                               & "         $LM_TRN$..D_IDO_TRS IDO2                                               " & vbNewLine _
                                               & "     LEFT JOIN                                                                  " & vbNewLine _
                                               & "         $LM_TRN$..D_ZAI_TRS ZAI2                                               " & vbNewLine _
                                               & "      ON IDO2.NRS_BR_CD = ZAI2.NRS_BR_CD                                        " & vbNewLine _
                                               & "     AND IDO2.O_ZAI_REC_NO = ZAI2.ZAI_REC_NO                                    " & vbNewLine _
                                               & "     AND ZAI2.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                               & "     WHERE                                                                      " & vbNewLine _
                                               & "         IDO2.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                                               & "     AND ZAI2.CUST_CD_L = @CUST_CD_L                                            " & vbNewLine _
                                               & "     AND ZAI2.CUST_CD_M = @CUST_CD_M                                            " & vbNewLine _
                                               & "     AND IDO2.IDO_DATE <= @PRINT_FROM                                           " & vbNewLine _
                                               & "     AND IDO2.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                               & "     )MAIN                                                                      " & vbNewLine _
                                               & "GROUP BY                                                                        " & vbNewLine _
                                               & "  MAIN.ZAI_REC_NO                                                               " & vbNewLine _
                                               & ", MAIN.GOODS_CD_NRS                                                             " & vbNewLine



#End Region

#Region "SQL_INSERT_UNION_5"

    Private Const SQL_INSERT_UNION_5 As String = "SELECT                                                                          " & vbNewLine _
                                               & "  @NRS_BR_CD                                                  AS NRS_BR_CD      " & vbNewLine _
                                               & ", @CUST_CD_L                                                  AS CUST_CD_L      " & vbNewLine _
                                               & ", @CUST_CD_M                                                  AS CUST_CD_M      " & vbNewLine _
                                               & ", @PRINT_FROM                                                 AS PRINT_FROM     " & vbNewLine _
                                               & ", ''                                                          AS ZAI_GETU       " & vbNewLine _
                                               & ", @GETU_FLG                                                   AS GETU_FLG       " & vbNewLine _
                                               & ", ''                                                          AS OUTKA_NO_L     " & vbNewLine _
                                               & ", ''                                                          AS OUTKA_NO_M     " & vbNewLine _
                                               & ", ''                                                          AS OUTKA_NO_S     " & vbNewLine _
                                               & ", ''                                                          AS CUST_NM_L      " & vbNewLine _
                                               & ", MAIN.ZAI_REC_NO                                             AS ZAI_REC_NO     " & vbNewLine _
                                               & ", MAIN.GOODS_CD_NRS                                           AS GOODS_CD_NRS   " & vbNewLine _
                                               & ", ''                                                          AS GOODS_CD_CUST  " & vbNewLine _
                                               & ", ''                                                          AS LOT_NO         " & vbNewLine _
                                               & ", ''                                                          AS SERIAL_NO      " & vbNewLine _
                                               & ", SUM(MAIN.ALCTD_NB) * -1                                     AS PORA_ZAI_NB    " & vbNewLine _
                                               & ", SUM(MAIN.ALCTD_QT) * -1                                     AS PORA_ZAI_QT    " & vbNewLine _
                                               & ", '5'                                                         AS UNION_NO       " & vbNewLine _
                                               & ", @SYS_ENT_DATE                                               AS SYS_ENT_DATE   " & vbNewLine _
                                               & ", @SYS_ENT_TIME                                               AS SYS_ENT_TIME   " & vbNewLine _
                                               & ", @SYS_ENT_PGID                                               AS SYS_ENT_PGID   " & vbNewLine _
                                               & ", @SYS_ENT_USER                                               AS SYS_ENT_USER   " & vbNewLine _
                                               & ", @SYS_UPD_DATE                                               AS SYS_UPD_DATE   " & vbNewLine _
                                               & ", @SYS_UPD_TIME                                               AS SYS_UPD_TIME   " & vbNewLine _
                                               & ", @SYS_UPD_PGID                                               AS SYS_UPD_PGID   " & vbNewLine _
                                               & ", @SYS_UPD_USER                                               AS SYS_UPD_USER   " & vbNewLine _
                                               & ", '0'                                                         AS SYS_DEL_FLG    " & vbNewLine _
                                               & "FROM(                                                                           " & vbNewLine _
                                               & "     SELECT                       --出荷データ                                  " & vbNewLine _
                                               & "       OUTL.NRS_BR_CD            AS NRS_BR_CD                                   " & vbNewLine _
                                               & "     , OUTS.ZAI_REC_NO           AS ZAI_REC_NO                                  " & vbNewLine _
                                               & "     , OUTM.GOODS_CD_NRS         AS GOODS_CD_NRS                                " & vbNewLine _
                                               & "     , OUTS.ALCTD_NB             AS ALCTD_NB                                    " & vbNewLine _
                                               & "     , OUTS.ALCTD_QT             AS ALCTD_QT                                    " & vbNewLine _
                                               & "     FROM                                                                       " & vbNewLine _
                                               & "         $LM_TRN$..C_OUTKA_L OUTL                                               " & vbNewLine _
                                               & "     LEFT JOIN                                                                  " & vbNewLine _
                                               & "         $LM_TRN$..C_OUTKA_M OUTM                                               " & vbNewLine _
                                               & "      ON OUTL.NRS_BR_CD = OUTM.NRS_BR_CD                                        " & vbNewLine _
                                               & "     AND OUTL.OUTKA_NO_L = OUTM.OUTKA_NO_L                                      " & vbNewLine _
                                               & "     AND OUTM.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                               & "     LEFT JOIN                                                                  " & vbNewLine _
                                               & "         $LM_TRN$..C_OUTKA_S OUTS                                               " & vbNewLine _
                                               & "      ON OUTM.NRS_BR_CD = OUTS.NRS_BR_CD                                        " & vbNewLine _
                                               & "     AND OUTM.OUTKA_NO_L = OUTS.OUTKA_NO_L                                      " & vbNewLine _
                                               & "     AND OUTM.OUTKA_NO_M = OUTS.OUTKA_NO_M                                      " & vbNewLine _
                                               & "     AND OUTS.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                               & "     WHERE                                                                      " & vbNewLine _
                                               & "         OUTL.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                                               & "     AND OUTL.CUST_CD_L = @CUST_CD_L                                            " & vbNewLine _
                                               & "     AND OUTL.CUST_CD_M = @CUST_CD_M                                            " & vbNewLine _
                                               & "     AND OUTL.OUTKA_PLAN_DATE <= @PRINT_FROM                                    " & vbNewLine _
                                               & "     AND OUTM.ALCTD_KB <> '04'                                                  " & vbNewLine _
                                               & "     AND OUTL.OUTKA_STATE_KB >= '60'                                            " & vbNewLine _
                                               & "     AND OUTL.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                               & "     )MAIN                                                                      " & vbNewLine _
                                               & "GROUP BY                                                                        " & vbNewLine _
                                               & "  MAIN.ZAI_REC_NO                                                               " & vbNewLine _
                                               & ", MAIN.GOODS_CD_NRS                                                             " & vbNewLine

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
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD620IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD621DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMD621DAC.SQL_FROM_MPrt)        'SQL構築(データ抽出用From句)
        Call Me.SetSQLWhereData()                         '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD621DAC", "SelectMPrt", cmd)

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
    ''' 在庫テーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫テーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD620IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'ワークテーブルにデータを登録
        Call Me.InsertPrintDataContorol(ds)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD621DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMD621DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMD621DAC.SQL_WHERE)            'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMD621DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用Group By句)
        Me._StrSql.Append(LMD621DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用Order By句)
        Call Me.SetSQLWhereData()                         'SQL構築(条件設定)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD621DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_NM", "WH_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("SHOBO_NM", "SHOBO_NM")
        map.Add("SHOBO_RUI_CD", "SHOBO_RUI_CD")
        map.Add("SHOBO_RUI_NM", "SHOBO_RUI_NM")
        map.Add("ZEN_NB", "ZEN_NB")
        map.Add("TOU_WT", "TOU_WT")
        map.Add("NB", "NB")
        map.Add("QT", "QT")
        map.Add("PRINT_FROM", "PRINT_FROM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD620OUT")

        Return ds

    End Function


    ''' <summary>
    ''' ワークテーブル保存処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub InsertPrintDataContorol(ByVal ds As DataSet)

        'データ削除
        Call Me.DeletePrintData(ds)

        'データ登録
        Call Me.InsertPrintData(ds, String.Concat(SQL_INSERT_HED, SQL_INSERT_UNION_2))
        Call Me.InsertPrintData(ds, String.Concat(SQL_INSERT_HED, SQL_INSERT_UNION_3))
        Call Me.InsertPrintData(ds, String.Concat(SQL_INSERT_HED, SQL_INSERT_UNION_4))
        Call Me.InsertPrintData(ds, String.Concat(SQL_INSERT_HED, SQL_INSERT_UNION_5))

    End Sub

    ''' <summary>
    ''' ワークテーブルのデータを削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub DeletePrintData(ByVal ds As DataSet)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD620IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(SQL_DELETE_WK)

        'パラメータ設定
        Call Me.SetSQLWhereData()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD621DAC", "DeletePrintData", cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

    End Sub

    ''' <summary>
    ''' ワークテーブルにデータ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="strSql"></param>
    ''' <remarks></remarks>
    Private Sub InsertPrintData(ByVal ds As DataSet, ByVal strSql As String)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD620IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(strSql)

        'パラメータ設定
        Call Me.SetSQLWhereData()
        Call Me.SetDataInsertParameter()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD621DAC", "InsertPrintData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

    End Sub

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereData()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row("CUST_CD_M"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_FROM", Me._Row("PRINT_FROM"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GETU_FLG", Me._Row.Item("GETU_FLG").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter()

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", systemDate, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", systemTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", systemPGID, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", systemUserID, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

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

End Class
