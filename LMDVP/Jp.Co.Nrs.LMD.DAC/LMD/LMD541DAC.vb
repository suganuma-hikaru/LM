' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫
'  プログラムID     :  LMD541    : 在庫履歴帳票（商品別）
'  作  成  者       :  [KIM]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD541DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD541DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' 検索パターン
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum SelectCondition As Integer
        PTN1  '出力対象帳票パターン取得
        PTN2  'データ検索（入荷ごとTab）
        PTN3  'データ検索（在庫ごとTab）
    End Enum

    Private Enum SelectWhereCondition As Integer
        WHERE1  '在庫ごとTabの出荷データ抽出時
        WHERE2  '入荷ごとTabの前残データ抽出時
        WHERE3  '入荷ごとTabの入荷データ抽出時
        WHERE4  '入荷ごとTabの出荷・振替データ抽出時
    End Enum

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMD541DAC"

    ''' <summary>
    ''' 帳票パターン取得テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_M_RPT As String = "M_RPT"

    ''' <summary>
    ''' INテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "GENZAIKO"

    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMD541OUT"

    ''' <summary>
    ''' 帳票ID
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PTN_ID As String = "26"

    ''' <summary>
    ''' Tabフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TAB_FLG_INOUT As String = "1"        '入出荷ごと
    Private Const TAB_FLG_ZAIKO As String = "2"        '在庫ごと

#End Region '制御用

#Region "SQL"

#Region "帳票ID"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = " SELECT DISTINCT                                                     " & vbNewLine _
                                            & "	 @NRS_BR_CD                                          AS NRS_BR_CD   " & vbNewLine _
                                            & "	,@PTN_ID                                             AS PTN_ID      " & vbNewLine _
                                            & "	,CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                   " & vbNewLine _
                                            & "	      ELSE MR2.PTN_CD END                            AS PTN_CD      " & vbNewLine _
                                            & "	,CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                   " & vbNewLine _
                                            & "	      ELSE MR2.RPT_ID END                            AS RPT_ID      " & vbNewLine _
                                            & "	FROM                                                                " & vbNewLine _
                                            & " --商品M                                                             " & vbNewLine _
                                            & "   $LM_MST$..M_GOODS AS GOODS                                        " & vbNewLine _
                                            & " --商品コードでの荷主帳票パターン取得                                " & vbNewLine _
                                            & "  LEFT JOIN                                                          " & vbNewLine _
                                            & "   $LM_MST$..M_CUST_RPT MCR1                                         " & vbNewLine _
                                            & "  ON                                                                 " & vbNewLine _
                                            & "   MCR1.NRS_BR_CD = @NRS_BR_CD                                       " & vbNewLine _
                                            & "  AND                                                                " & vbNewLine _
                                            & "   MCR1.CUST_CD_L  = GOODS.CUST_CD_L                                 " & vbNewLine _
                                            & "  AND                                                                " & vbNewLine _
                                            & "   MCR1.CUST_CD_M  = GOODS.CUST_CD_M                                 " & vbNewLine _
                                            & "  AND                                                                " & vbNewLine _
                                            & "   MCR1.CUST_CD_S = GOODS.CUST_CD_S                                  " & vbNewLine _
                                            & "  AND                                                                " & vbNewLine _
                                            & "   MCR1.PTN_ID    = @PTN_ID                                          " & vbNewLine _
                                            & "  --帳票パターン取得                                                 " & vbNewLine _
                                            & "  LEFT JOIN                                                          " & vbNewLine _
                                            & "   $LM_MST$..M_RPT MR1                                               " & vbNewLine _
                                            & "  ON                                                                 " & vbNewLine _
                                            & "   MR1.NRS_BR_CD = @NRS_BR_CD                                        " & vbNewLine _
                                            & "  AND                                                                " & vbNewLine _
                                            & "   MR1.PTN_ID    = MCR1.PTN_ID                                       " & vbNewLine _
                                            & "  AND                                                                " & vbNewLine _
                                            & "   MR1.PTN_CD    = MCR1.PTN_CD                                       " & vbNewLine _
                                            & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                            & "  --存在しない場合の帳票パターン取得                                 " & vbNewLine _
                                            & "  LEFT JOIN                                                          " & vbNewLine _
                                            & "   $LM_MST$..M_RPT MR2                                               " & vbNewLine _
                                            & "  ON                                                                 " & vbNewLine _
                                            & "   MR2.NRS_BR_CD     = @NRS_BR_CD                                    " & vbNewLine _
                                            & "  AND                                                                " & vbNewLine _
                                            & "   MR2.PTN_ID        = @PTN_ID                                       " & vbNewLine _
                                            & "  AND                                                                " & vbNewLine _
                                            & "   MR2.STANDARD_FLAG = '01'                                          " & vbNewLine _
                                            & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                            & " WHERE                                                               " & vbNewLine _
                                            & "   GOODS.NRS_BR_CD    = @NRS_BR_CD                                   " & vbNewLine _
                                            & " AND                                                                 " & vbNewLine _
                                            & "   GOODS.GOODS_CD_NRS = @GOODS_CD_NRS                                " & vbNewLine _
                                            & " AND                                                                 " & vbNewLine _
                                            & "   GOODS.SYS_DEL_FLG  = '0'                                          " & vbNewLine

#End Region '帳票ID

#Region "共通SQL"

    ''' <summary>
    ''' 印刷データ取得SQL（共通ヘッダ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FIRST As String = " SELECT                                                                 " & vbNewLine _
                                                   & " CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                                   & "      ELSE MR2.RPT_ID END                        AS RPT_ID        " & vbNewLine _
                                                   & "  ,  @NRS_BR_CD                                  AS NRS_BR_CD     " & vbNewLine _
                                                   & "  ,  NRS_BR.NRS_BR_NM                            AS NRS_BR_NM     " & vbNewLine _
                                                   & "  ,  CUST.CUST_CD_L                              AS CUST_CD_L     " & vbNewLine _
                                                   & "  ,  CUST.CUST_NM_L                              AS CUST_NM_L     " & vbNewLine _
                                                   & "  ,  @WH_CD                                      AS WH_CD         " & vbNewLine _
                                                   & "  ,  SOKO.WH_NM                                  AS WH_NM         " & vbNewLine _
                                                   & "  ,  GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST " & vbNewLine _
                                                   & "  ,  GOODS.GOODS_NM_1                            AS GOODS_NM      " & vbNewLine _
                                                   & "  ,  @LOT_NO                                     AS LOT_NO        " & vbNewLine _
                                                   & "  ,  GOODS.STD_IRIME_NB                          AS IRIME         " & vbNewLine _
                                                   & "  ,  @KAZU_KB                                    AS KAZU_KB       " & vbNewLine _
                                                   & "  ,  @SHUKEI_KB                                  AS SHUKEI_KB     " & vbNewLine

    ''' <summary>
    ''' 印刷データ取得SQL（共通JOIN部）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAME_JOIN As String = " --商品M                                " & vbNewLine _
                                                       & " LEFT OUTER JOIN                        " & vbNewLine _
                                                       & "   $LM_MST$..M_GOODS AS GOODS           " & vbNewLine _
                                                       & " ON                                     " & vbNewLine _
                                                       & "   GOODS.NRS_BR_CD    = @NRS_BR_CD      " & vbNewLine _
                                                       & " AND                                    " & vbNewLine _
                                                       & "   GOODS.GOODS_CD_NRS = @GOODS_CD_NRS   " & vbNewLine _
                                                       & " --荷主M                                " & vbNewLine _
                                                       & " LEFT JOIN                              " & vbNewLine _
                                                       & "    $LM_MST$..M_CUST AS CUST            " & vbNewLine _
                                                       & " ON                                     " & vbNewLine _
                                                       & "    CUST.NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
                                                       & " AND                                    " & vbNewLine _
                                                       & "     GOODS.CUST_CD_L  = CUST.CUST_CD_L  " & vbNewLine _
                                                       & " AND                                    " & vbNewLine _
                                                       & "     GOODS.CUST_CD_M  = CUST.CUST_CD_M  " & vbNewLine _
                                                       & " AND                                    " & vbNewLine _
                                                       & "     GOODS.CUST_CD_S  = CUST.CUST_CD_S  " & vbNewLine _
                                                       & " AND                                    " & vbNewLine _
                                                       & "     GOODS.CUST_CD_SS = CUST.CUST_CD_SS " & vbNewLine _
                                                       & " --営業所M                              " & vbNewLine _
                                                       & " LEFT JOIN                              " & vbNewLine _
                                                       & "    $LM_MST$..M_NRS_BR AS NRS_BR        " & vbNewLine _
                                                       & " ON                                     " & vbNewLine _
                                                       & "    NRS_BR.NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
                                                       & " --倉庫M                                " & vbNewLine _
                                                       & " LEFT JOIN                              " & vbNewLine _
                                                       & "    $LM_MST$..M_SOKO AS SOKO            " & vbNewLine _
                                                       & " ON                                     " & vbNewLine _
                                                       & "    SOKO.NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
                                                       & " AND                                    " & vbNewLine _
                                                       & "    SOKO.WH_CD     = @WH_CD             " & vbNewLine _
                                                       & " --商品コードでの荷主帳票パターン取得   " & vbNewLine _
                                                       & "  LEFT JOIN                             " & vbNewLine _
                                                       & "   $LM_MST$..M_CUST_RPT MCR1            " & vbNewLine _
                                                       & "  ON                                    " & vbNewLine _
                                                       & "   MCR1.NRS_BR_CD = @NRS_BR_CD          " & vbNewLine _
                                                       & "  AND                                   " & vbNewLine _
                                                       & "   MCR1.CUST_CD_L  = GOODS.CUST_CD_L    " & vbNewLine _
                                                       & "  AND                                   " & vbNewLine _
                                                       & "   MCR1.CUST_CD_M  = GOODS.CUST_CD_M    " & vbNewLine _
                                                       & "  AND                                   " & vbNewLine _
                                                       & "   MCR1.CUST_CD_S = GOODS.CUST_CD_S     " & vbNewLine _
                                                       & "  AND                                   " & vbNewLine _
                                                       & "   MCR1.PTN_ID    = @PTN_ID             " & vbNewLine _
                                                       & "  --帳票パターン取得                    " & vbNewLine _
                                                       & "  LEFT JOIN                             " & vbNewLine _
                                                       & "   $LM_MST$..M_RPT MR1                  " & vbNewLine _
                                                       & "  ON                                    " & vbNewLine _
                                                       & "   MR1.NRS_BR_CD = @NRS_BR_CD           " & vbNewLine _
                                                       & "  AND                                   " & vbNewLine _
                                                       & "   MR1.PTN_ID    = MCR1.PTN_ID          " & vbNewLine _
                                                       & "  AND                                   " & vbNewLine _
                                                       & "   MR1.PTN_CD    = MCR1.PTN_CD          " & vbNewLine _
                                                       & "  --存在しない場合の帳票パターン取得    " & vbNewLine _
                                                       & "  LEFT JOIN                             " & vbNewLine _
                                                       & "   $LM_MST$..M_RPT MR2                  " & vbNewLine _
                                                       & "  ON                                    " & vbNewLine _
                                                       & "   MR2.NRS_BR_CD     = @NRS_BR_CD       " & vbNewLine _
                                                       & "  AND                                   " & vbNewLine _
                                                       & "   MR2.PTN_ID        = @PTN_ID          " & vbNewLine _
                                                       & "  AND                                   " & vbNewLine _
                                                       & "   MR2.STANDARD_FLAG = '01'             " & vbNewLine

    '在庫移動トランザクションに場合のJOIN差分
    Private Const SQL_SELECT_JOIN_ADD_ZAIKO As String = " --在庫TRS_O                                    " & vbNewLine _
                                                       & " LEFT OUTER JOIN                                " & vbNewLine _
                                                       & "   $LM_TRN$..D_ZAI_TRS AS ZAITRS_O              " & vbNewLine _
                                                       & " ON                                             " & vbNewLine _
                                                       & "   ZAITRS_O.NRS_BR_CD   = @NRS_BR_CD            " & vbNewLine _
                                                       & " AND                                            " & vbNewLine _
                                                       & "   ZAITRS_O.ZAI_REC_NO  = IDOTRS.O_ZAI_REC_NO   " & vbNewLine _
                                                       & " AND                                            " & vbNewLine _
                                                       & "   ZAITRS_O.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                                       & " --在庫TRS_N                                    " & vbNewLine _
                                                       & " LEFT OUTER JOIN                                " & vbNewLine _
                                                       & "   $LM_TRN$..D_ZAI_TRS AS ZAITRS_N              " & vbNewLine _
                                                       & " ON                                             " & vbNewLine _
                                                       & "   ZAITRS_N.NRS_BR_CD   = @NRS_BR_CD            " & vbNewLine _
                                                       & " AND                                            " & vbNewLine _
                                                       & "   ZAITRS_N.ZAI_REC_NO  = IDOTRS.N_ZAI_REC_NO   " & vbNewLine _
                                                       & " AND                                            " & vbNewLine _
                                                       & "   ZAITRS_N.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                                       & " --届先M                                        " & vbNewLine _
                                                       & " LEFT OUTER JOIN                                " & vbNewLine _
                                                       & "   $LM_MST$..M_DEST AS DEST                     " & vbNewLine _
                                                       & " ON                                             " & vbNewLine _
                                                       & "  DEST.NRS_BR_CD   = @NRS_BR_CD                 " & vbNewLine _
                                                       & " AND                                            " & vbNewLine _
                                                       & "  ZAITRS_O.CUST_CD_L = DEST.CUST_CD_L           " & vbNewLine _
                                                       & " AND                                            " & vbNewLine _
                                                       & "  ZAITRS_N.DEST_CD_P = DEST.DEST_CD             " & vbNewLine
                                                     


    '入出荷場合のJOIN差分
    Private Const SQL_SELECT_JOIN_ADD As String = " --在庫TRS                                      " & vbNewLine _
                                                       & " LEFT OUTER JOIN                                " & vbNewLine _
                                                       & "   $LM_TRN$..D_ZAI_TRS AS ZAITRS                " & vbNewLine _
                                                       & " ON                                             " & vbNewLine _
                                                       & "   ZAITRS.NRS_BR_CD      = @NRS_BR_CD           " & vbNewLine _
                                                       & " AND                                            " & vbNewLine _
                                                       & "   ZAITRS.ZAI_REC_NO     = @ZAI_REC_NO          " & vbNewLine _
                                                       & " AND ZAITRS.INKA_NO_L    = @INKA_NO_L           " & vbNewLine _
                                                       & " AND ZAITRS.INKA_NO_M    = @INKA_NO_M           " & vbNewLine _
                                                       & " AND ZAITRS.INKA_NO_S    = @INKA_NO_S           " & vbNewLine _
                                                       & " AND ZAITRS.GOODS_CD_NRS = @GOODS_CD_NRS        " & vbNewLine _
                                                       & " AND                                            " & vbNewLine _
                                                       & "   ZAITRS.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                                       & " --届先M                                        " & vbNewLine _
                                                       & " LEFT OUTER JOIN                                " & vbNewLine _
                                                       & "   $LM_MST$..M_DEST AS DEST                     " & vbNewLine _
                                                       & " ON                                             " & vbNewLine _
                                                       & "  DEST.NRS_BR_CD   = @NRS_BR_CD                 " & vbNewLine _
                                                       & " AND                                            " & vbNewLine _
                                                       & "  ZAITRS.CUST_CD_L = DEST.CUST_CD_L             " & vbNewLine _
                                                       & " AND                                            " & vbNewLine _
                                                       & "  ZAITRS.DEST_CD_P = DEST.DEST_CD               " & vbNewLine

    ''' <summary>
    ''' 印刷データ取得SQL（UNION）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNION As String = "  UNION   " & vbNewLine

    ''' <summary>
    ''' ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY " & vbNewLine _
                                         & "    NRS_BR_CD, CUST_CD_L, WH_CD, GOODS_CD_CUST, PLAN_DATE, SYUBETU, YOJITU, LOT_NO, IRIME " & vbNewLine

#End Region '共通SQL

#Region "在庫ごとTab"

#Region "在庫移動トランザクション"

    ''' <summary>
    ''' 検索項目（在庫移動トランザクション）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ZAIKO_MAIN1 As String = " ,  ''                                                                                           AS YOJITU        " & vbNewLine _
                                                    & " , (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN '移出'                                                      " & vbNewLine _
                                                    & "         WHEN IDOTRS.N_ZAI_REC_NO = @ZAI_REC_NO THEN '移入'                                                      " & vbNewLine _
                                                    & "         ELSE '' END)                                                                           AS SYUBETU       " & vbNewLine _
                                                    & " , IDOTRS.IDO_DATE                                                                              AS PLAN_DATE     " & vbNewLine _
                                                    & " , (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN 0                                                           " & vbNewLine _
                                                    & "         WHEN IDOTRS.N_ZAI_REC_NO = @ZAI_REC_NO THEN IDOTRS.N_PORA_ZAI_NB                                        " & vbNewLine _
                                                    & "         ELSE 0 END)                                                                            AS INKA_KOSU     " & vbNewLine _
                                                    & " , (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN 0                                                           " & vbNewLine _
                                                    & "         WHEN IDOTRS.N_ZAI_REC_NO = @ZAI_REC_NO THEN IDOTRS.N_PORA_ZAI_NB * GOODS.STD_IRIME_NB                   " & vbNewLine _
                                                    & "         ELSE 0 END)                                                                            AS INKA_SURYO    " & vbNewLine _
                                                    & " , (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN IDOTRS.O_PORA_ZAI_NB                                        " & vbNewLine _
                                                    & "         WHEN IDOTRS.N_ZAI_REC_NO = @ZAI_REC_NO THEN  0 ELSE 0 END)                             AS OUTKA_KOSU    " & vbNewLine _
                                                    & " , (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN IDOTRS.O_PORA_ZAI_NB * GOODS.STD_IRIME_NB                   " & vbNewLine _
                                                    & "         WHEN IDOTRS.N_ZAI_REC_NO = @ZAI_REC_NO THEN 0 ELSE 0 END)                              AS OUTKA_SURYO   " & vbNewLine _
                                                    & " ,  0                                                                                           AS ZAN_KOSU      " & vbNewLine _
                                                    & " ,  GOODS.NB_UT                                                                                 AS NB_UT         " & vbNewLine _
                                                    & " ,  0                                                                                           AS ZAN_SURYO     " & vbNewLine _
                                                    & " ,  GOODS.STD_IRIME_UT                                                                          AS STD_IRIME_UT  " & vbNewLine _
                                                    & " ,  ZAITRS_N.TOU_NO + '-' + ZAITRS_N.SITU_NO + '-' + ZAITRS_N.ZONE_CD +                                          " & vbNewLine _
                                                    & "        (CASE WHEN ZAITRS_N.LOCA = '' THEN '' ELSE '-' + ZAITRS_N.LOCA END)                     AS OKIBA         " & vbNewLine _
                                                    & "  , (CASE WHEN IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO THEN IDOTRS.N_ZAI_REC_NO                                        " & vbNewLine _
                                                    & "          WHEN IDOTRS.N_ZAI_REC_NO = @ZAI_REC_NO THEN IDOTRS.O_ZAI_REC_NO                                        " & vbNewLine _
                                                    & "          ELSE '' END)                                                                          AS ZAI_REC_NO    " & vbNewLine _
                                                    & "  ,  KBN1.KBN_NM1                                                                               AS DEST_NM       " & vbNewLine _
                                                    & "  ,  ''                                                                                         AS ORD_NO        " & vbNewLine _
                                                    & "  ,  ''                                                                                         AS BUYER_ORD_NO  " & vbNewLine _
                                                    & "  ,  ''                                                                                         AS UNSOCO_NM     " & vbNewLine _
                                                    & "  ,  DEST.DEST_NM                                                                               AS DEST_CD_NM    " & vbNewLine _
                                                    & "  ,  ZAITRS_N.INKA_NO_L                                                                         AS KANRI_NO_L    " & vbNewLine _
                                                    & "  ,  ZAITRS_N.INKA_NO_M                                                                         AS KANRI_NO_M    " & vbNewLine _
                                                    & " FROM                                                                                                            " & vbNewLine _
                                                    & "    $LM_TRN$..D_IDO_TRS AS IDOTRS                                                                                " & vbNewLine

    ''' <summary>
    ''' 個別JOIN部（在庫移動トランザクション）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ZAIKO_END1 As String = " --区分M                                          " & vbNewLine _
                                                  & " LEFT OUTER JOIN                                  " & vbNewLine _
                                                  & "   (                                              " & vbNewLine _
                                                  & "      SELECT                                      " & vbNewLine _
                                                  & "         KBN1.KBN_CD                              " & vbNewLine _
                                                  & "       , KBN1.KBN_NM1                             " & vbNewLine _
                                                  & "      FROM                                        " & vbNewLine _
                                                  & "       $LM_MST$..Z_KBN AS KBN1                    " & vbNewLine _
                                                  & "      WHERE                                       " & vbNewLine _
                                                  & "       KBN1.KBN_GROUP_CD = 'I002'                 " & vbNewLine _
                                                  & "   ) KBN1                                         " & vbNewLine _
                                                  & " ON                                               " & vbNewLine _
                                                  & "   IDOTRS.REMARK_KBN = KBN1.KBN_CD                " & vbNewLine _
                                                  & " --抽出条件                                       " & vbNewLine _
                                                  & " WHERE                                            " & vbNewLine _
                                                  & "   IDOTRS.NRS_BR_CD = @NRS_BR_CD                  " & vbNewLine _
                                                  & " AND                                              " & vbNewLine _
                                                  & "   (   IDOTRS.O_ZAI_REC_NO = @ZAI_REC_NO          " & vbNewLine _
                                                  & "    OR IDOTRS.N_ZAI_REC_NO = @ZAI_REC_NO  )       " & vbNewLine _
                                                  & " AND                                              " & vbNewLine _
                                                  & "   IDOTRS.SYS_DEL_FLG = '0'                       " & vbNewLine

#End Region '在庫移動トランザクション

#Region "入荷データ"

    ''' <summary>
    ''' 検索項目（入荷）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ZAIKO_MAIN2 As String = " , ''                                                                    AS YOJITU         " & vbNewLine _
                                                   & " , '入荷'                                                                AS SYUBETU        " & vbNewLine _
                                                   & " , ZAITRS.INKO_PLAN_DATE                                                 AS PLAN_DATE      " & vbNewLine _
                                                   & " , INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU                               AS INKA_KOSU      " & vbNewLine _
                                                   & " , (INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU) * GOODS.STD_IRIME_NB        AS INKA_SURYO     " & vbNewLine _
                                                   & " , 0                                                                     AS OUTKA_KOSU     " & vbNewLine _
                                                   & " , 0                                                                     AS OUTKA_SURYO    " & vbNewLine _
                                                   & " , 0                                                                     AS ZAN_KOSU       " & vbNewLine _
                                                   & " , GOODS.NB_UT                                                           AS NB_UT          " & vbNewLine _
                                                   & " , 0                                                                     AS ZAN_SURYO      " & vbNewLine _
                                                   & " , GOODS.STD_IRIME_UT                                                    AS STD_IRIME_UT   " & vbNewLine _
                                                   & " , ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD                             " & vbNewLine _
                                                   & "      + ( CASE WHEN ZAITRS.LOCA = '' THEN ''                                               " & vbNewLine _
                                                   & "               ELSE '-' + ZAITRS.LOCA END )                              AS OKIBA          " & vbNewLine _
                                                   & " , INKAS.ZAI_REC_NO                                                      AS ZAI_REC_NO     " & vbNewLine _
                                                   & " , ''                                                                    AS DEST_NM        " & vbNewLine _
                                                   & " , CASE WHEN ISNULL(RTRIM(INKAM.OUTKA_FROM_ORD_NO_M), '') = '' THEN INKAL.OUTKA_FROM_ORD_NO_L ELSE INKAM.OUTKA_FROM_ORD_NO_M END  AS ORD_NO         " & vbNewLine _
                                                   & " , CASE WHEN ISNULL(RTRIM(INKAM.BUYER_ORD_NO_M)     , '') = '' THEN INKAL.BUYER_ORD_NO_L      ELSE INKAM.BUYER_ORD_NO_M      END  AS BUYER_ORD_NO   " & vbNewLine _
                                                   & " , UNSOCO.UNSOCO_NM                                                      AS UNSOCO_NM      " & vbNewLine _
                                                   & " , DEST.DEST_NM                                                          AS DEST_CD_NM     " & vbNewLine _
                                                   & " , ZAITRS.INKA_NO_L                                                      AS KANRI_NO_L       " & vbNewLine _
                                                   & " , ZAITRS.INKA_NO_M                                                      AS KANRI_NO_M      " & vbNewLine _
                                                   & " --入荷S                                              " & vbNewLine _
                                                   & " FROM                                                 " & vbNewLine _
                                                   & "   $LM_TRN$..B_INKA_S AS INKAS                        " & vbNewLine

    ''' <summary>
    ''' 入荷FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_INKA As String = " --入荷M                                              " & vbNewLine _
                                                  & " LEFT OUTER JOIN                                     " & vbNewLine _
                                                  & "   $LM_TRN$..B_INKA_M AS INKAM                       " & vbNewLine _
                                                  & " ON                                                  " & vbNewLine _
                                                  & "   INKAM.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                                                  & " AND                                                 " & vbNewLine _
                                                  & "   INKAM.INKA_NO_L = @INKA_NO_L                      " & vbNewLine _
                                                  & " AND                                                 " & vbNewLine _
                                                  & "   INKAM.INKA_NO_M = @INKA_NO_M                      " & vbNewLine _
                                                  & " --入荷L                                             " & vbNewLine _
                                                  & " LEFT OUTER JOIN                                     " & vbNewLine _
                                                  & "   $LM_TRN$..B_INKA_L AS INKAL                       " & vbNewLine _
                                                  & " ON                                                  " & vbNewLine _
                                                  & "   INKAL.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                                                  & " AND                                                 " & vbNewLine _
                                                  & "   INKAL.INKA_NO_L = @INKA_NO_L                      " & vbNewLine _
                                                  & " --運送L                                             " & vbNewLine _
                                                  & " LEFT OUTER JOIN                                     " & vbNewLine _
                                                  & "   $LM_TRN$..F_UNSO_L AS UNSOL                       " & vbNewLine _
                                                  & " ON                                                  " & vbNewLine _
                                                  & "   UNSOL.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                                                  & " AND                                                 " & vbNewLine _
                                                  & "   UNSOL.MOTO_DATA_KB = '10'                         " & vbNewLine _
                                                  & " AND                                                 " & vbNewLine _
                                                  & "   UNSOL.INOUTKA_NO_L = @INKA_NO_L                   " & vbNewLine _
                                                  & " AND                                                 " & vbNewLine _
                                                  & "   UNSOL.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                                  & " --運送会社M                                         " & vbNewLine _
                                                  & " LEFT OUTER JOIN                                     " & vbNewLine _
                                                  & "   $LM_MST$..M_UNSOCO AS UNSOCO                      " & vbNewLine _
                                                  & " ON                                                  " & vbNewLine _
                                                  & "   UNSOL.NRS_BR_CD  = UNSOCO.NRS_BR_CD               " & vbNewLine _
                                                  & " AND                                                 " & vbNewLine _
                                                  & "   UNSOL.UNSO_CD    = UNSOCO.UNSOCO_CD               " & vbNewLine _
                                                  & " AND                                                 " & vbNewLine _
                                                  & "   UNSOL.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD            " & vbNewLine _
                                                  & " --抽出条件                                          " & vbNewLine _
                                                  & " WHERE                                               " & vbNewLine _
                                                  & "   INKAS.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                                                  & " AND                                                 " & vbNewLine _
                                                  & "   INKAS.INKA_NO_L = @INKA_NO_L                      " & vbNewLine _
                                                  & " AND                                                 " & vbNewLine _
                                                  & "   INKAS.INKA_NO_M = @INKA_NO_M                      " & vbNewLine _
                                                  & " AND                                                 " & vbNewLine _
                                                  & "   INKAS.INKA_NO_S = @INKA_NO_S                      " & vbNewLine _
                                                  & " AND                                                 " & vbNewLine _
                                                  & "   INKAS.ZAI_REC_NO = @ZAI_REC_NO                    " & vbNewLine _
                                                  & " AND                                                 " & vbNewLine _
                                                  & "   INKAS.SYS_DEL_FLG = '0'                           " & vbNewLine

#End Region '入荷データ

#Region "出荷データ"

    ''' <summary>
    ''' 検索項目（出荷）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ZAIKO_MAIN3 As String = " , (CASE WHEN OUTKAL.SYS_DEL_FLG =  '1' THEN '消'                                              " & vbNewLine _
                                                   & "         WHEN OUTKAM.ALCTD_KB    =  '04' AND OUTKAL.OUTKA_STATE_KB <  '60' THEN 'ヨ'           " & vbNewLine _
                                                   & "         WHEN OUTKAM.ALCTD_KB    =  '04' AND OUTKAL.OUTKA_STATE_KB >= '60' THEN 'サ'           " & vbNewLine _
                                                   & "         WHEN OUTKAM.ALCTD_KB    <> '04' AND OUTKAL.OUTKA_STATE_KB <  '60' THEN '予'           " & vbNewLine _
                                                   & "         ELSE '' END)                                                    AS YOJITU             " & vbNewLine _
                                                   & " , (CASE WHEN OUTKAL.FURI_NO IS NULL THEN '振替'                                               " & vbNewLine _
                                                   & "         ELSE '出荷' END)                                                AS SYUBETU            " & vbNewLine _
                                                   & " , OUTKAL.OUTKA_PLAN_DATE                                                AS PLAN_DATE          " & vbNewLine _
                                                   & " , 0                                                                     AS INKA_KOSU          " & vbNewLine _
                                                   & " , 0                                                                     AS INKA_SURYO         " & vbNewLine _
                                                   & " , OUTKAS.ALCTD_NB                                                       AS OUTKA_KOSU         " & vbNewLine _
                                                   & " , OUTKAS.ALCTD_QT                                                       AS OUTKA_SURYO        " & vbNewLine _
                                                   & " , 0                                                                     AS ZAN_KOSU           " & vbNewLine _
                                                   & " , GOODS.NB_UT                                                           AS NB_UT              " & vbNewLine _
                                                   & " , 0                                                                     AS ZAN_SURYO          " & vbNewLine _
                                                   & " , GOODS.STD_IRIME_UT                                                    AS STD_IRIME_UT       " & vbNewLine _
                                                   & " , OUTKAS.TOU_NO + '-' + OUTKAS.SITU_NO + '-' + OUTKAS.ZONE_CD                                 " & vbNewLine _
                                                   & "    + ( CASE WHEN OUTKAS.LOCA = '' THEN ''                                                     " & vbNewLine _
                                                   & "             ELSE '-' + OUTKAS.LOCA END )                                AS OKIBA              " & vbNewLine _
                                                   & " , ''                                                                    AS ZAI_REC_NO         " & vbNewLine _
                                                   & " , DEST.DEST_NM                                                          AS DEST_NM            " & vbNewLine _
                                                   & " , CASE WHEN ISNULL(RTRIM(OUTKAM.CUST_ORD_NO_DTL), '')  = '' THEN OUTKAL.CUST_ORD_NO  ELSE OUTKAM.CUST_ORD_NO_DTL  END  AS ORD_NO             " & vbNewLine _
                                                   & " , CASE WHEN ISNULL(RTRIM(OUTKAM.BUYER_ORD_NO_DTL), '') = '' THEN OUTKAL.BUYER_ORD_NO ELSE OUTKAM.BUYER_ORD_NO_DTL END  AS BUYER_ORD_NO       " & vbNewLine _
                                                   & " , UNSOCO.UNSOCO_NM                                                      AS UNSOCO_NM          " & vbNewLine _
                                                   & " , ''                                                                    AS DEST_CD_NM         " & vbNewLine _
                                                   & " , OUTKAS.OUTKA_NO_L                                                     AS KANRI_NO_L          " & vbNewLine _
                                                   & " , OUTKAS.OUTKA_NO_M                                                     AS KANRI_NO_M          " & vbNewLine _
                                                   & " FROM                                                                      " & vbNewLine _
                                                   & "   (                                                                      " & vbNewLine _
                                                   & "      SELECT                                                              " & vbNewLine _
                                                   & "          S.NRS_BR_CD      AS NRS_BR_CD                                   " & vbNewLine _
                                                   & "        , S.OUTKA_NO_L     AS OUTKA_NO_L                                  " & vbNewLine _
                                                   & "        , S.OUTKA_NO_M     AS OUTKA_NO_M                                  " & vbNewLine _
                                                   & "        , S.OUTKA_NO_S     AS OUTKA_NO_S                                  " & vbNewLine _
                                                   & "        , S.ALCTD_NB       AS ALCTD_NB                                    " & vbNewLine _
                                                   & "        , S.ALCTD_QT       AS ALCTD_QT                                    " & vbNewLine _
                                                   & "        , S.TOU_NO         AS TOU_NO                                      " & vbNewLine _
                                                   & "        , S.SITU_NO        AS SITU_NO                                     " & vbNewLine _
                                                   & "        , S.ZONE_CD        AS ZONE_CD                                     " & vbNewLine _
                                                   & "        , S.LOCA           AS LOCA                                        " & vbNewLine _
                                                   & "        , S.ZAI_REC_NO     AS ZAI_REC_NO                                  " & vbNewLine _
                                                   & "        , S.SYS_DEL_FLG    AS SYS_DEL_FLG                                 " & vbNewLine _
                                                   & "      FROM                                                                " & vbNewLine _
                                                   & "        $LM_TRN$..C_OUTKA_S  AS S                                         " & vbNewLine _
                                                   & "      WHERE                                                               " & vbNewLine _
                                                   & "          S.NRS_BR_CD   = @NRS_BR_CD                                      " & vbNewLine _
                                                   & "      AND S.INKA_NO_L   = @INKA_NO_L                                      " & vbNewLine _
                                                   & "      AND S.INKA_NO_M   = @INKA_NO_M                                      " & vbNewLine _
                                                   & "      AND S.INKA_NO_S   = @INKA_NO_S                                      " & vbNewLine _
                                                   & "      AND S.ZAI_REC_NO IN                                                 " & vbNewLine _
                                                   & "         (                                                                " & vbNewLine _
                                                   & "           SELECT                                                         " & vbNewLine _
                                                   & "             ZAI_REC_NO                                                   " & vbNewLine _
                                                   & "           FROM                                                           " & vbNewLine _
                                                   & "             $LM_TRN$..D_ZAI_TRS  D_ZAI_TRS                               " & vbNewLine _
                                                   & "           WHERE                                                          " & vbNewLine _
                                                   & "                  D_ZAI_TRS.NRS_BR_CD   = @NRS_BR_CD                      " & vbNewLine _
                                                   & "              AND D_ZAI_TRS.INKA_NO_L   = @INKA_NO_L                      " & vbNewLine _
                                                   & "              AND D_ZAI_TRS.INKA_NO_M   = @INKA_NO_M                      " & vbNewLine _
                                                   & "              AND D_ZAI_TRS.INKA_NO_S   = @INKA_NO_S                      " & vbNewLine _
                                                   & "              AND D_ZAI_TRS.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                                   & "          )                                                               " & vbNewLine _
                                                   & "      AND S.ZAI_REC_NO = @ZAI_REC_NO                                      " & vbNewLine _
                                                   & "   ) OUTKAS                                                               " & vbNewLine
    ''' <summary>
    ''' 出荷FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_OUTKA As String = " --出荷M                                                                  " & vbNewLine _
                                                   & " LEFT OUTER JOIN                                                          " & vbNewLine _
                                                   & "   $LM_TRN$..C_OUTKA_M AS OUTKAM                                          " & vbNewLine _
                                                   & " ON                                                                       " & vbNewLine _
                                                   & "   OUTKAM.NRS_BR_CD = @NRS_BR_CD                                          " & vbNewLine _
                                                   & " AND                                                                      " & vbNewLine _
                                                   & "   OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                                  " & vbNewLine _
                                                   & " AND                                                                      " & vbNewLine _
                                                   & "   OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                                  " & vbNewLine _
                                                   & " --出荷L                                                                  " & vbNewLine _
                                                   & " LEFT OUTER JOIN                                                          " & vbNewLine _
                                                   & "   $LM_TRN$..C_OUTKA_L AS OUTKAL                                          " & vbNewLine _
                                                   & " ON                                                                       " & vbNewLine _
                                                   & "   OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                    " & vbNewLine _
                                                   & " AND                                                                      " & vbNewLine _
                                                   & "   OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                  " & vbNewLine _
                                                   & " --運送L                                                                  " & vbNewLine _
                                                   & " LEFT OUTER JOIN                                                          " & vbNewLine _
                                                   & "   $LM_TRN$..F_UNSO_L AS UNSOL                                            " & vbNewLine _
                                                   & " ON                                                                       " & vbNewLine _
                                                   & "   UNSOL.NRS_BR_CD = @NRS_BR_CD                                           " & vbNewLine _
                                                   & " AND                                                                      " & vbNewLine _
                                                   & "   UNSOL.MOTO_DATA_KB = '20'                                              " & vbNewLine _
                                                   & " AND                                                                      " & vbNewLine _
                                                   & "   UNSOL.INOUTKA_NO_L = OUTKAL.OUTKA_NO_L                                 " & vbNewLine _
                                                   & " AND                                                                      " & vbNewLine _
                                                   & "   UNSOL.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                                   & " --運送会社M                                                              " & vbNewLine _
                                                   & " LEFT OUTER JOIN                                                          " & vbNewLine _
                                                   & "   $LM_MST$..M_UNSOCO AS UNSOCO                                           " & vbNewLine _
                                                   & " ON                                                                       " & vbNewLine _
                                                   & "   UNSOL.NRS_BR_CD  = UNSOCO.NRS_BR_CD                                    " & vbNewLine _
                                                   & " AND                                                                      " & vbNewLine _
                                                   & "   UNSOL.UNSO_CD    = UNSOCO.UNSOCO_CD                                    " & vbNewLine _
                                                   & " AND                                                                      " & vbNewLine _
                                                   & "   UNSOL.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD                                 " & vbNewLine _
                                                   & " --抽出条件                                                               " & vbNewLine _
                                                   & " WHERE                                                                    " & vbNewLine _
                                                   & "   OUTKAS.NRS_BR_CD = @NRS_BR_CD                                          " & vbNewLine

#End Region '出荷データ

#End Region '在庫ごとTab

#Region "入出荷ごとTab"

    ''' <summary>
    ''' 入出荷ごと履歴SQL_1
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INOUT1 As String = "   SELECT                                                                " & vbNewLine _
                                              & "   CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                      " & vbNewLine _
                                              & "        ELSE MR2.RPT_ID END                        AS RPT_ID             " & vbNewLine _
                                              & "    ,  @NRS_BR_CD                                  AS NRS_BR_CD          " & vbNewLine _
                                              & "    ,  NRS_BR.NRS_BR_NM                            AS NRS_BR_NM          " & vbNewLine _
                                              & "    ,  CUST.CUST_CD_L                              AS CUST_CD_L          " & vbNewLine _
                                              & "    ,  CUST.CUST_NM_L                              AS CUST_NM_L          " & vbNewLine _
                                              & "    ,  @WH_CD                                      AS WH_CD              " & vbNewLine _
                                              & "    ,  SOKO.WH_NM                                  AS WH_NM              " & vbNewLine _
                                              & "    ,  GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST      " & vbNewLine _
                                              & "    ,  GOODS.GOODS_NM_1                            AS GOODS_NM           " & vbNewLine _
                                              & "    ,  @LOT_NO                                     AS LOT_NO             " & vbNewLine _
                                              & "    ,  GOODS.STD_IRIME_NB                          AS IRIME              " & vbNewLine _
                                              & "    ,  @KAZU_KB                                    AS KAZU_KB            " & vbNewLine _
                                              & "    ,  @SHUKEI_KB                                  AS SHUKEI_KB          " & vbNewLine _
                                              & "   , ''  AS YOJITU                           " & vbNewLine _
                                              & "   , '前残'  AS SYUBETU                      " & vbNewLine _
                                              & "   , @INKO_PLAN_DATE_FROM  AS PLAN_DATE      " & vbNewLine _
                                              & "   , 0   AS INKA_KOSU                        " & vbNewLine _
                                              & "   , 0   AS INKA_SURYO                       " & vbNewLine _
                                              & "   , 0   AS OUTKA_KOSU                       " & vbNewLine _
                                              & "   , 0   AS OUTKA_SURYO                      " & vbNewLine _
                                              & "   , ZAITRS.ALLOC_CAN_NB AS ZAN_KOSU         " & vbNewLine _
                                              & "   , GOODS.NB_UT AS NB_UT                    " & vbNewLine _
                                              & "   , ZAITRS.ALLOC_CAN_QT AS ZAN_SURYO        " & vbNewLine _
                                              & "   , GOODS.STD_IRIME_UT  AS STD_IRIME_UT     " & vbNewLine _
                                              & "   , ''  AS OKIBA                            " & vbNewLine _
                                              & "   , ''  AS ZAI_REC_NO                       " & vbNewLine _
                                              & "   , ''  AS DEST_NM                          " & vbNewLine _
                                              & "   , ''  AS ORD_NO                           " & vbNewLine _
                                              & "   , ''  AS BUYER_ORD_NO                     " & vbNewLine _
                                              & "   , ''  AS UNSOCO_NM                        " & vbNewLine _
                                              & "   , ''  AS DEST_CD_NM                       " & vbNewLine _
                                              & "   , ''  AS KANRI_NO_L                       " & vbNewLine _
                                              & "   , ''  AS KANRI_NO_M                       " & vbNewLine _
                                              & "   FROM                                      " & vbNewLine _
                                              & "   (                                         " & vbNewLine _
                                              & "   SELECT                                    " & vbNewLine _
                                              & "      S.NRS_BR_CD  AS NRS_BR_CD              " & vbNewLine _
                                              & "    , S.KONSU  AS KONSU                      " & vbNewLine _
                                              & "    , S.HASU AS HASU                         " & vbNewLine _
                                              & "    , S.ZAI_REC_NO AS ZAI_REC_NO             " & vbNewLine _
                                              & "   FROM                                      " & vbNewLine _
                                              & "   $LM_TRN$..B_INKA_S  AS S                  " & vbNewLine _
                                              & "   WHERE                                     " & vbNewLine _
                                              & "   S.NRS_BR_CD  = @NRS_BR_CD                 " & vbNewLine _
                                              & "   AND S.INKA_NO_L  = @INKA_NO_L             " & vbNewLine _
                                              & "   AND S.INKA_NO_M  = @INKA_NO_M             " & vbNewLine _
                                              & "   AND S.INKA_NO_S  = @INKA_NO_S             " & vbNewLine _
                                              & "   AND S.SYS_DEL_FLG = 0                     " & vbNewLine _
                                              & "   AND S.KONSU <> 0                          " & vbNewLine _
                                              & "   AND S.HASU  <> 0                          " & vbNewLine _
                                              & "   ) INKAS                                   " & vbNewLine _
                                              & "   --商品M                                   " & vbNewLine _
                                              & "   LEFT OUTER JOIN                           " & vbNewLine _
                                              & "     (SELECT                                 " & vbNewLine _
                                              & "        GOODS.PKG_NB,                        " & vbNewLine _
                                              & "        GOODS.STD_IRIME_NB,                  " & vbNewLine _
                                              & "        GOODS.STD_IRIME_UT,                  " & vbNewLine _
                                              & "        GOODS.NB_UT,                         " & vbNewLine _
                                              & "        GOODS.GOODS_NM_1,                    " & vbNewLine _
                                              & "        GOODS.NRS_BR_CD ,                    " & vbNewLine _
                                              & "        GOODS.GOODS_CD_NRS,                  " & vbNewLine _
                                              & "        GOODS.GOODS_CD_CUST,                 " & vbNewLine _
                                              & "        GOODS.CUST_CD_L,                     " & vbNewLine _
                                              & "        GOODS.CUST_CD_M,                     " & vbNewLine _
                                              & "        GOODS.CUST_CD_S,                     " & vbNewLine _
                                              & "        GOODS.CUST_CD_SS                     " & vbNewLine _
                                              & "     FROM $LM_MST$..M_GOODS AS GOODS                            " & vbNewLine _
                                              & "     WHERE GOODS.NRS_BR_CD    = @NRS_BR_CD    )GOODS            " & vbNewLine _
                                              & "   ON                                                           " & vbNewLine _
                                              & "      GOODS.GOODS_CD_NRS = @GOODS_CD_NRS                        " & vbNewLine _
                                              & "   --荷主M                        " & vbNewLine _
                                              & "   LEFT JOIN                      " & vbNewLine _
                                              & "     (SELECT                      " & vbNewLine _
                                              & "        CUST.CUST_CD_L,           " & vbNewLine _
                                              & "        CUST.CUST_CD_M,           " & vbNewLine _
                                              & "        CUST.CUST_CD_S,           " & vbNewLine _
                                              & "        CUST.CUST_CD_SS,          " & vbNewLine _
                                              & "        CUST.CUST_NM_L,           " & vbNewLine _
                                              & "        CUST.NRS_BR_CD            " & vbNewLine _
                                              & "     FROM $LM_MST$..M_CUST AS CUST                        " & vbNewLine _
                                              & "     WHERE CUST.NRS_BR_CD    = @NRS_BR_CD   ) CUST        " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "      CUST.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "       GOODS.CUST_CD_L  = CUST.CUST_CD_L       " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "       GOODS.CUST_CD_M  = CUST.CUST_CD_M       " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "       GOODS.CUST_CD_S  = CUST.CUST_CD_S       " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "       GOODS.CUST_CD_SS = CUST.CUST_CD_SS      " & vbNewLine _
                                              & "   --営業所M                                   " & vbNewLine _
                                              & "   LEFT JOIN                                   " & vbNewLine _
                                              & "      $LM_MST$..M_NRS_BR AS NRS_BR             " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "      NRS_BR.NRS_BR_CD = @NRS_BR_CD            " & vbNewLine _
                                              & "   --倉庫M                                     " & vbNewLine _
                                              & "   LEFT JOIN                                   " & vbNewLine _
                                              & "      $LM_MST$..M_SOKO AS SOKO                 " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "      SOKO.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "      SOKO.WH_CD     = @WH_CD                  " & vbNewLine _
                                              & "   --商品コードでの荷主帳票パターン取得        " & vbNewLine _
                                              & "    LEFT JOIN                                  " & vbNewLine _
                                              & "     $LM_MST$..M_CUST_RPT MCR1                 " & vbNewLine _
                                              & "    ON                                         " & vbNewLine _
                                              & "     MCR1.NRS_BR_CD = @NRS_BR_CD               " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MCR1.CUST_CD_L  = GOODS.CUST_CD_L         " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MCR1.CUST_CD_M  = GOODS.CUST_CD_M         " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MCR1.CUST_CD_S = GOODS.CUST_CD_S          " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MCR1.PTN_ID    = @PTN_ID                  " & vbNewLine _
                                              & "    --帳票パターン取得                         " & vbNewLine _
                                              & "    LEFT JOIN                                  " & vbNewLine _
                                              & "     $LM_MST$..M_RPT MR1                       " & vbNewLine _
                                              & "    ON                                         " & vbNewLine _
                                              & "     MR1.NRS_BR_CD = @NRS_BR_CD                " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MR1.PTN_ID    = MCR1.PTN_ID               " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MR1.PTN_CD    = MCR1.PTN_CD               " & vbNewLine _
                                              & "    AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                              & "    --存在しない場合の帳票パターン取得         " & vbNewLine _
                                              & "    LEFT JOIN                                  " & vbNewLine _
                                              & "     $LM_MST$..M_RPT MR2                       " & vbNewLine _
                                              & "    ON                                         " & vbNewLine _
                                              & "     MR2.NRS_BR_CD     = @NRS_BR_CD            " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MR2.PTN_ID        = @PTN_ID               " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MR2.STANDARD_FLAG = '01'                  " & vbNewLine _
                                              & "    AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                              & "   --出荷S                                     " & vbNewLine _
                                              & "   LEFT OUTER JOIN                             " & vbNewLine _
                                              & "   $LM_TRN$..C_OUTKA_S AS OUTKAS               " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "   OUTKAS.NRS_BR_CD = @NRS_BR_CD               " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "   OUTKAS.INKA_NO_S = @INKA_NO_S               " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "   OUTKAS.INKA_NO_M = @INKA_NO_M               " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "   OUTKAS.INKA_NO_L = @INKA_NO_L               " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "   INKAS.ZAI_REC_NO = OUTKAS.ZAI_REC_NO        " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "   OUTKAS.SYS_DEL_FLG = '0'                    " & vbNewLine _
                                              & "   --出荷L                                     " & vbNewLine _
                                              & "   LEFT OUTER JOIN                             " & vbNewLine _
                                              & "   $LM_TRN$..C_OUTKA_L AS OUTKAL               " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "   OUTKAL.NRS_BR_CD = @NRS_BR_CD               " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "   OUTKAL.OUTKA_NO_L = OUTKAS.OUTKA_NO_L       " & vbNewLine _
                                              & "   --出荷M                                     " & vbNewLine _
                                              & "   LEFT OUTER JOIN                             " & vbNewLine _
                                              & "   $LM_TRN$..C_OUTKA_M AS OUTKAM               " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "   OUTKAM.NRS_BR_CD = @NRS_BR_CD               " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "   OUTKAM.OUTKA_NO_L = OUTKAS.OUTKA_NO_L       " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "   OUTKAM.OUTKA_NO_M = OUTKAS.OUTKA_NO_M       " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "   OUTKAM.ALCTD_KB <> '04'                     " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "   OUTKAM.OUTKA_TTL_NB <> 0                    " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "   OUTKAM.OUTKA_TTL_QT <> 0                    " & vbNewLine _
                                              & "   --在庫TRS                                   " & vbNewLine _
                                              & "   LEFT OUTER JOIN                             " & vbNewLine _
                                              & "     $LM_TRN$..D_ZAI_TRS AS ZAITRS             " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "         ZAITRS.NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
                                              & "     AND ZAITRS.INKA_NO_L  = @INKA_NO_L        " & vbNewLine _
                                              & "     AND ZAITRS.INKA_NO_M  = @INKA_NO_M        " & vbNewLine _
                                              & "     AND ZAITRS.INKA_NO_S  = @INKA_NO_S        " & vbNewLine _
                                              & "     AND ZAITRS.ZAI_REC_NO = INKAS.ZAI_REC_NO  " & vbNewLine _
                                              & "     AND ZAITRS.SYS_DEL_FLG = '0'              " & vbNewLine _
                                              & "     AND ZAITRS.GOODS_CD_NRS = @GOODS_CD_NRS   " & vbNewLine _
                                              & "   --届先M                                     " & vbNewLine _
                                              & "   LEFT OUTER JOIN                             " & vbNewLine _
                                              & "     $LM_MST$..M_DEST AS DEST                  " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "    DEST.NRS_BR_CD   = @NRS_BR_CD              " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "    ZAITRS.CUST_CD_L = DEST.CUST_CD_L          " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "    ZAITRS.DEST_CD_P = DEST.DEST_CD            " & vbNewLine _
                                              & "   --抽出条件                                  " & vbNewLine _
                                              & "   WHERE                                       " & vbNewLine _
                                              & "   INKAS.NRS_BR_CD = @NRS_BR_CD                " & vbNewLine

    ''' <summary>
    ''' 入出荷ごと履歴SQL_2
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INOUT2 As String = "    UNION        " & vbNewLine _
                                              & "   SELECT                                                                " & vbNewLine _
                                              & "   CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                      " & vbNewLine _
                                              & "        ELSE MR2.RPT_ID END                        AS RPT_ID             " & vbNewLine _
                                              & "    ,  @NRS_BR_CD                                  AS NRS_BR_CD          " & vbNewLine _
                                              & "    ,  NRS_BR.NRS_BR_NM                            AS NRS_BR_NM          " & vbNewLine _
                                              & "    ,  CUST.CUST_CD_L                              AS CUST_CD_L          " & vbNewLine _
                                              & "    ,  CUST.CUST_NM_L                              AS CUST_NM_L          " & vbNewLine _
                                              & "    ,  @WH_CD                                      AS WH_CD              " & vbNewLine _
                                              & "    ,  SOKO.WH_NM                                  AS WH_NM              " & vbNewLine _
                                              & "    ,  GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST      " & vbNewLine _
                                              & "    ,  GOODS.GOODS_NM_1                            AS GOODS_NM           " & vbNewLine _
                                              & "    ,  @LOT_NO                                     AS LOT_NO             " & vbNewLine _
                                              & "    ,  GOODS.STD_IRIME_NB                          AS IRIME              " & vbNewLine _
                                              & "    ,  @KAZU_KB                                    AS KAZU_KB            " & vbNewLine _
                                              & "    ,  @SHUKEI_KB                                  AS SHUKEI_KB          " & vbNewLine _
                                              & "   , (CASE WHEN INKAL.INKA_STATE_KB < '50' THEN '予'                     " & vbNewLine _
                                              & "   ELSE '' END)    AS YOJITU                                             " & vbNewLine _
                                              & "   , '入荷'     AS SYUBETU                                               " & vbNewLine _
                                              & "   , ZAITRS.INKO_PLAN_DATE   AS PLAN_DATE                                " & vbNewLine _
                                              & "   , INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU  AS INKA_KOSU               " & vbNewLine _
                                              & "   , (INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU) * GOODS.STD_IRIME_NB AS INKA_SURYO      " & vbNewLine _
                                              & "   , 0    AS OUTKA_KOSU                                                                " & vbNewLine _
                                              & "   , 0    AS OUTKA_SURYO                                                               " & vbNewLine _
                                              & "   , 0    AS ZAN_KOSU                                                                  " & vbNewLine _
                                              & "   , GOODS.NB_UT    AS NB_UT                                                           " & vbNewLine _
                                              & "   , 0    AS ZAN_SURYO                                                                 " & vbNewLine _
                                              & "   , GOODS.STD_IRIME_UT    AS STD_IRIME_UT                                             " & vbNewLine _
                                              & "   , ZAITRS.TOU_NO + '-' + ZAITRS.SITU_NO + '-' + ZAITRS.ZONE_CD                       " & vbNewLine _
                                              & "   + ( CASE WHEN ZAITRS.LOCA = '' THEN ''                                              " & vbNewLine _
                                              & "    ELSE '-' + ZAITRS.LOCA END )  AS OKIBA                                             " & vbNewLine _
                                              & "   , INKAS.ZAI_REC_NO    AS ZAI_REC_NO                                                 " & vbNewLine _
                                              & "   , ''     AS DEST_NM                                                                 " & vbNewLine _
                                              & "   , CASE WHEN ISNULL(RTRIM(INKAM.OUTKA_FROM_ORD_NO_M), '') = ''                       " & vbNewLine _
                                              & "   THEN INKAL.OUTKA_FROM_ORD_NO_L ELSE INKAM.OUTKA_FROM_ORD_NO_M END AS ORD_NO         " & vbNewLine _
                                              & "   , CASE WHEN ISNULL(RTRIM(INKAM.BUYER_ORD_NO_M), '') = ''                            " & vbNewLine _
                                              & "   THEN INKAL.BUYER_ORD_NO_L ELSE INKAM.BUYER_ORD_NO_M END AS BUYER_ORD_NO             " & vbNewLine _
                                              & "   , UNSOCO.UNSOCO_NM    AS UNSOCO_NM                                                  " & vbNewLine _
                                              & "   , DEST.DEST_NM    AS DEST_CD_NM                                                     " & vbNewLine _
                                              & "   , ZAITRS.INKA_NO_L    AS KANRI_NO_L                                                 " & vbNewLine _
                                              & "   , ZAITRS.INKA_NO_M    AS KANRI_NO_M                                                 " & vbNewLine _
                                              & "   FROM                                                                                " & vbNewLine _
                                              & "   $LM_TRN$..B_INKA_S AS INKAS                                                         " & vbNewLine _
                                              & "   --商品M                                                                             " & vbNewLine _
                                              & "   LEFT OUTER JOIN                                                                     " & vbNewLine _
                                              & "     (SELECT                                                                           " & vbNewLine _
                                              & "        GOODS.PKG_NB,                                                                  " & vbNewLine _
                                              & "        GOODS.STD_IRIME_NB,                                                            " & vbNewLine _
                                              & "        GOODS.STD_IRIME_UT,                                                            " & vbNewLine _
                                              & "        GOODS.NB_UT,                                                                   " & vbNewLine _
                                              & "        GOODS.GOODS_NM_1,                                                              " & vbNewLine _
                                              & "        GOODS.NRS_BR_CD ,                                                              " & vbNewLine _
                                              & "        GOODS.GOODS_CD_NRS,                                                            " & vbNewLine _
                                              & "        GOODS.GOODS_CD_CUST,                                                           " & vbNewLine _
                                              & "        GOODS.CUST_CD_L,                                                               " & vbNewLine _
                                              & "        GOODS.CUST_CD_M,                                                               " & vbNewLine _
                                              & "        GOODS.CUST_CD_S,                                                               " & vbNewLine _
                                              & "        GOODS.CUST_CD_SS                                                               " & vbNewLine _
                                              & "     FROM $LM_MST$..M_GOODS AS GOODS                            " & vbNewLine _
                                              & "     WHERE GOODS.NRS_BR_CD    = @NRS_BR_CD    )GOODS            " & vbNewLine _
                                              & "   ON                                                           " & vbNewLine _
                                              & "      GOODS.GOODS_CD_NRS = @GOODS_CD_NRS                        " & vbNewLine _
                                              & "   --荷主M                                                      " & vbNewLine _
                                              & "   LEFT JOIN                                                    " & vbNewLine _
                                              & "     (SELECT                                                    " & vbNewLine _
                                              & "        CUST.CUST_CD_L,                                         " & vbNewLine _
                                              & "        CUST.CUST_CD_M,                                         " & vbNewLine _
                                              & "        CUST.CUST_CD_S,                                         " & vbNewLine _
                                              & "        CUST.CUST_CD_SS,                                        " & vbNewLine _
                                              & "        CUST.CUST_NM_L,                                         " & vbNewLine _
                                              & "        CUST.NRS_BR_CD                                          " & vbNewLine _
                                              & "     FROM $LM_MST$..M_CUST AS CUST                              " & vbNewLine _
                                              & "     WHERE CUST.NRS_BR_CD    = @NRS_BR_CD   ) CUST              " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "      CUST.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "       GOODS.CUST_CD_L  = CUST.CUST_CD_L       " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "       GOODS.CUST_CD_M  = CUST.CUST_CD_M       " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "       GOODS.CUST_CD_S  = CUST.CUST_CD_S       " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "       GOODS.CUST_CD_SS = CUST.CUST_CD_SS      " & vbNewLine _
                                              & "   --営業所M                                   " & vbNewLine _
                                              & "   LEFT JOIN                                   " & vbNewLine _
                                              & "      $LM_MST$..M_NRS_BR AS NRS_BR             " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "      NRS_BR.NRS_BR_CD = @NRS_BR_CD            " & vbNewLine _
                                              & "   --倉庫M                                     " & vbNewLine _
                                              & "   LEFT JOIN                                   " & vbNewLine _
                                              & "      $LM_MST$..M_SOKO AS SOKO                 " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "      SOKO.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "      SOKO.WH_CD     = @WH_CD                  " & vbNewLine _
                                              & "   --商品コードでの荷主帳票パターン取得        " & vbNewLine _
                                              & "    LEFT JOIN                                  " & vbNewLine _
                                              & "     $LM_MST$..M_CUST_RPT MCR1                 " & vbNewLine _
                                              & "    ON                                         " & vbNewLine _
                                              & "     MCR1.NRS_BR_CD = @NRS_BR_CD               " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MCR1.CUST_CD_L  = GOODS.CUST_CD_L         " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MCR1.CUST_CD_M  = GOODS.CUST_CD_M         " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MCR1.CUST_CD_S = GOODS.CUST_CD_S          " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MCR1.PTN_ID    = @PTN_ID                  " & vbNewLine _
                                              & "    --帳票パターン取得                         " & vbNewLine _
                                              & "    LEFT JOIN                                  " & vbNewLine _
                                              & "     $LM_MST$..M_RPT MR1                       " & vbNewLine _
                                              & "    ON                                         " & vbNewLine _
                                              & "     MR1.NRS_BR_CD = @NRS_BR_CD                " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MR1.PTN_ID    = MCR1.PTN_ID               " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MR1.PTN_CD    = MCR1.PTN_CD               " & vbNewLine _
                                              & "    AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                              & "    --存在しない場合の帳票パターン取得         " & vbNewLine _
                                              & "    LEFT JOIN                                  " & vbNewLine _
                                              & "     $LM_MST$..M_RPT MR2                       " & vbNewLine _
                                              & "    ON                                         " & vbNewLine _
                                              & "     MR2.NRS_BR_CD     = @NRS_BR_CD            " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MR2.PTN_ID        = @PTN_ID               " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MR2.STANDARD_FLAG = '01'                  " & vbNewLine _
                                              & "    AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                              & "   --在庫TRS                                   " & vbNewLine _
                                              & "   LEFT OUTER JOIN                             " & vbNewLine _
                                              & "     $LM_TRN$..D_ZAI_TRS AS ZAITRS             " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "         ZAITRS.NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
                                              & "     AND ZAITRS.INKA_NO_L  = @INKA_NO_L        " & vbNewLine _
                                              & "     AND ZAITRS.INKA_NO_M  = @INKA_NO_M        " & vbNewLine _
                                              & "     AND ZAITRS.INKA_NO_S  = @INKA_NO_S        " & vbNewLine _
                                              & "     AND INKAS.ZAI_REC_NO = ZAITRS.ZAI_REC_NO  " & vbNewLine _
                                              & "     AND ZAITRS.SYS_DEL_FLG = '0'              " & vbNewLine _
                                              & "   --届先M                                     " & vbNewLine _
                                              & "   LEFT OUTER JOIN                             " & vbNewLine _
                                              & "     $LM_MST$..M_DEST AS DEST                  " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "    DEST.NRS_BR_CD   = @NRS_BR_CD              " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "    ZAITRS.CUST_CD_L = DEST.CUST_CD_L          " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "    ZAITRS.DEST_CD_P = DEST.DEST_CD            " & vbNewLine _
                                              & "   --入荷M                                                  " & vbNewLine _
                                              & "   LEFT OUTER JOIN                                          " & vbNewLine _
                                              & "     $LM_TRN$..B_INKA_M AS INKAM                            " & vbNewLine _
                                              & "   ON                                                       " & vbNewLine _
                                              & "     INKAM.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                              & "   AND                                                      " & vbNewLine _
                                              & "     INKAM.INKA_NO_L = @INKA_NO_L                           " & vbNewLine _
                                              & "   AND                                                      " & vbNewLine _
                                              & "     INKAM.INKA_NO_M = @INKA_NO_M                           " & vbNewLine _
                                              & "   --入荷L                                                  " & vbNewLine _
                                              & "   LEFT OUTER JOIN                                          " & vbNewLine _
                                              & "     $LM_TRN$..B_INKA_L AS INKAL                            " & vbNewLine _
                                              & "   ON                                                       " & vbNewLine _
                                              & "     INKAL.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                              & "   AND                                                      " & vbNewLine _
                                              & "     INKAL.INKA_NO_L = @INKA_NO_L                           " & vbNewLine _
                                              & "   --運送L                                                  " & vbNewLine _
                                              & "   LEFT OUTER JOIN                                          " & vbNewLine _
                                              & "     $LM_TRN$..F_UNSO_L AS UNSOL                            " & vbNewLine _
                                              & "   ON                                                       " & vbNewLine _
                                              & "     UNSOL.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                              & "   AND                                                      " & vbNewLine _
                                              & "     UNSOL.MOTO_DATA_KB = '10'                              " & vbNewLine _
                                              & "   AND                                                      " & vbNewLine _
                                              & "     UNSOL.INOUTKA_NO_L = @INKA_NO_L                        " & vbNewLine _
                                              & "   AND                                                      " & vbNewLine _
                                              & "     UNSOL.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                              & "   --運送会社M                                              " & vbNewLine _
                                              & "   LEFT OUTER JOIN                                          " & vbNewLine _
                                              & "     $LM_MST$..M_UNSOCO AS UNSOCO                           " & vbNewLine _
                                              & "   ON                                                       " & vbNewLine _
                                              & "     UNSOL.NRS_BR_CD  = UNSOCO.NRS_BR_CD                    " & vbNewLine _
                                              & "   AND                                                      " & vbNewLine _
                                              & "     UNSOL.UNSO_CD    = UNSOCO.UNSOCO_CD                    " & vbNewLine _
                                              & "   AND                                                      " & vbNewLine _
                                              & "     UNSOL.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD                 " & vbNewLine _
                                              & "   --抽出条件                                               " & vbNewLine _
                                              & "   WHERE                                                    " & vbNewLine _
                                              & "     INKAS.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                              & "   AND                                                      " & vbNewLine _
                                              & "     INKAS.INKA_NO_L = @INKA_NO_L                           " & vbNewLine _
                                              & "   AND                                                      " & vbNewLine _
                                              & "     INKAS.INKA_NO_M = @INKA_NO_M                           " & vbNewLine _
                                              & "   AND                                                      " & vbNewLine _
                                              & "     INKAS.INKA_NO_S = @INKA_NO_S                           " & vbNewLine _
                                              & "   AND                                                      " & vbNewLine _
                                              & "     INKAS.SYS_DEL_FLG = '0'                                " & vbNewLine
    ''' <summary>
    ''' 入出荷ごと履歴SQL_3
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INOUT3 As String = "    UNION        " & vbNewLine _
                                              & "   SELECT                                                                " & vbNewLine _
                                              & "   CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                      " & vbNewLine _
                                              & "        ELSE MR2.RPT_ID END                        AS RPT_ID             " & vbNewLine _
                                              & "    ,  @NRS_BR_CD                                  AS NRS_BR_CD          " & vbNewLine _
                                              & "    ,  NRS_BR.NRS_BR_NM                            AS NRS_BR_NM          " & vbNewLine _
                                              & "    ,  CUST.CUST_CD_L                              AS CUST_CD_L          " & vbNewLine _
                                              & "    ,  CUST.CUST_NM_L                              AS CUST_NM_L          " & vbNewLine _
                                              & "    ,  @WH_CD                                      AS WH_CD              " & vbNewLine _
                                              & "    ,  SOKO.WH_NM                                  AS WH_NM              " & vbNewLine _
                                              & "    ,  GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST      " & vbNewLine _
                                              & "    ,  GOODS.GOODS_NM_1                            AS GOODS_NM           " & vbNewLine _
                                              & "    ,  @LOT_NO                                     AS LOT_NO             " & vbNewLine _
                                              & "    ,  GOODS.STD_IRIME_NB                          AS IRIME              " & vbNewLine _
                                              & "    ,  @KAZU_KB                                    AS KAZU_KB            " & vbNewLine _
                                              & "    ,  @SHUKEI_KB                                  AS SHUKEI_KB          " & vbNewLine _
                                              & "   , ( CASE WHEN OUTKAL.SYS_DEL_FLG = '1' THEN '消'                      " & vbNewLine _
                                              & "    WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB < '60' THEN 'ﾖ'       " & vbNewLine _
                                              & "    WHEN OUTKAM.ALCTD_KB = '04' AND OUTKAL.OUTKA_STATE_KB >= '60' THEN 'ｻ'      " & vbNewLine _
                                              & "    WHEN OUTKAM.ALCTD_KB <> '04' AND OUTKAL.OUTKA_STATE_KB < '60' THEN '予'     " & vbNewLine _
                                              & "    ELSE '' END )       AS YOJITU                                               " & vbNewLine _
                                              & "   , ( CASE WHEN OUTKAL.FURI_NO <> '' THEN '振替'                               " & vbNewLine _
                                              & "    ELSE '出荷' END)      AS SYUBETU                                            " & vbNewLine _
                                              & "   , OUTKAL.OUTKA_PLAN_DATE       AS PLAN_DATE                                  " & vbNewLine _
                                              & "   , 0         AS INKA_KOSU                                                     " & vbNewLine _
                                              & "   , 0         AS INKA_SURYO                                                    " & vbNewLine _
                                              & "   , OUTKAS.ALCTD_NB       AS OUTKA_KOSU                                        " & vbNewLine _
                                              & "   , OUTKAS.ALCTD_QT       AS OUTKA_SURYO                                       " & vbNewLine _
                                              & "   , 0         AS ZAN_KOSU                                                      " & vbNewLine _
                                              & "   , GOODS.NB_UT        AS NB_UT                                                " & vbNewLine _
                                              & "   , 0         AS ZAN_SURYO                                                     " & vbNewLine _
                                              & "   , GOODS.STD_IRIME_UT       AS STD_IRIME_UT                                   " & vbNewLine _
                                              & "   , OUTKAS.TOU_NO + '-' + OUTKAS.SITU_NO + '-' + OUTKAS.ZONE_CD                " & vbNewLine _
                                              & "     + ( CASE WHEN OUTKAS.LOCA = '' THEN ''                                     " & vbNewLine _
                                              & "      ELSE '-' + OUTKAS.LOCA END )   AS OKIBA                                   " & vbNewLine _
                                              & "   , OUTKAS.ZAI_REC_NO       AS ZAI_REC_NO                                      " & vbNewLine _
                                              & "   , DEST.DEST_NM        AS DEST_NM                                             " & vbNewLine _
                                              & "   , CASE WHEN ISNULL(RTRIM(OUTKAM.CUST_ORD_NO_DTL), '') = ''                   " & vbNewLine _
                                              & "   THEN OUTKAL.CUST_ORD_NO ELSE OUTKAM.CUST_ORD_NO_DTL END  AS ORD_NO           " & vbNewLine _
                                              & "   , CASE WHEN ISNULL(RTRIM(OUTKAM.BUYER_ORD_NO_DTL), '') = ''                  " & vbNewLine _
                                              & "   THEN OUTKAL.BUYER_ORD_NO ELSE OUTKAM.BUYER_ORD_NO_DTL END  AS BUYER_ORD_NO   " & vbNewLine _
                                              & "   , UNSOCO.UNSOCO_NM       AS UNSOCO_NM                                        " & vbNewLine _
                                              & "   , ''         AS DEST_CD_NM                                                   " & vbNewLine _
                                              & "   , OUTKAS.OUTKA_NO_L       AS KANRI_NO_L                                      " & vbNewLine _
                                              & "   , OUTKAS.OUTKA_NO_M       AS KANRI_NO_M                                      " & vbNewLine _
                                              & "   FROM                                                                         " & vbNewLine _
                                              & "   (                                                                            " & vbNewLine _
                                              & "   SELECT                                                                       " & vbNewLine _
                                              & "     S.NRS_BR_CD AS NRS_BR_CD                                                   " & vbNewLine _
                                              & "   , S.OUTKA_NO_L AS OUTKA_NO_L                                                 " & vbNewLine _
                                              & "   , S.OUTKA_NO_M AS OUTKA_NO_M                                                 " & vbNewLine _
                                              & "   , S.OUTKA_NO_S AS OUTKA_NO_S                                                 " & vbNewLine _
                                              & "   , S.ALCTD_NB AS ALCTD_NB                                                     " & vbNewLine _
                                              & "   , S.ALCTD_QT AS ALCTD_QT                                                     " & vbNewLine _
                                              & "   , S.TOU_NO  AS TOU_NO                                                        " & vbNewLine _
                                              & "   , S.SITU_NO AS SITU_NO                                                       " & vbNewLine _
                                              & "   , S.ZONE_CD AS ZONE_CD                                                       " & vbNewLine _
                                              & "   , S.LOCA  AS LOCA                                                            " & vbNewLine _
                                              & "   , S.ZAI_REC_NO AS ZAI_REC_NO                                                 " & vbNewLine _
                                              & "   , S.SYS_DEL_FLG AS SYS_DEL_FLG                                               " & vbNewLine _
                                              & "   FROM                                                                         " & vbNewLine _
                                              & "   $LM_TRN$..C_OUTKA_S AS S                                                     " & vbNewLine _
                                              & "   WHERE                                                                        " & vbNewLine _
                                              & "    S.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                                              & "   AND S.INKA_NO_L = @INKA_NO_L                                                 " & vbNewLine _
                                              & "   AND S.INKA_NO_M = @INKA_NO_M                                                 " & vbNewLine _
                                              & "   AND S.INKA_NO_S = @INKA_NO_S          " & vbNewLine _
                                              & "   AND S.ZAI_REC_NO IN                   " & vbNewLine _
                                              & "    (                                    " & vbNewLine _
                                              & "    SELECT                               " & vbNewLine _
                                              & "    ZAI_REC_NO                           " & vbNewLine _
                                              & "    FROM                                 " & vbNewLine _
                                              & "    $LM_TRN$..D_ZAI_TRS D_ZAI_TRS        " & vbNewLine _
                                              & "    WHERE                                " & vbNewLine _
                                              & "     D_ZAI_TRS.NRS_BR_CD = @NRS_BR_CD                  " & vbNewLine _
                                              & "    AND D_ZAI_TRS.INKA_NO_L = @INKA_NO_L               " & vbNewLine _
                                              & "    AND D_ZAI_TRS.INKA_NO_M = @INKA_NO_M               " & vbNewLine _
                                              & "    AND D_ZAI_TRS.INKA_NO_S = @INKA_NO_S               " & vbNewLine _
                                              & "    AND D_ZAI_TRS.SYS_DEL_FLG = '0'                    " & vbNewLine _
                                              & "    )                                                  " & vbNewLine _
                                              & "   ) OUTKAS                                            " & vbNewLine _
                                              & "   --商品M                                             " & vbNewLine _
                                              & "   LEFT OUTER JOIN                                     " & vbNewLine _
                                              & "     (SELECT                                           " & vbNewLine _
                                              & "        GOODS.PKG_NB,                                  " & vbNewLine _
                                              & "        GOODS.STD_IRIME_NB,                            " & vbNewLine _
                                              & "        GOODS.STD_IRIME_UT,                            " & vbNewLine _
                                              & "        GOODS.NB_UT,                                   " & vbNewLine _
                                              & "        GOODS.GOODS_NM_1,                              " & vbNewLine _
                                              & "        GOODS.NRS_BR_CD ,                              " & vbNewLine _
                                              & "        GOODS.GOODS_CD_NRS,                            " & vbNewLine _
                                              & "        GOODS.GOODS_CD_CUST,                           " & vbNewLine _
                                              & "        GOODS.CUST_CD_L,                               " & vbNewLine _
                                              & "        GOODS.CUST_CD_M,                               " & vbNewLine _
                                              & "        GOODS.CUST_CD_S,                               " & vbNewLine _
                                              & "        GOODS.CUST_CD_SS                               " & vbNewLine _
                                              & "     FROM $LM_MST$..M_GOODS AS GOODS                   " & vbNewLine _
                                              & "     WHERE GOODS.NRS_BR_CD    = @NRS_BR_CD    )GOODS   " & vbNewLine _
                                              & "   ON                                                  " & vbNewLine _
                                              & "      GOODS.GOODS_CD_NRS = @GOODS_CD_NRS               " & vbNewLine _
                                              & "   --荷主M                        " & vbNewLine _
                                              & "   LEFT JOIN                      " & vbNewLine _
                                              & "     (SELECT                      " & vbNewLine _
                                              & "        CUST.CUST_CD_L,           " & vbNewLine _
                                              & "        CUST.CUST_CD_M,           " & vbNewLine _
                                              & "        CUST.CUST_CD_S,           " & vbNewLine _
                                              & "        CUST.CUST_CD_SS,          " & vbNewLine _
                                              & "        CUST.CUST_NM_L,           " & vbNewLine _
                                              & "        CUST.NRS_BR_CD            " & vbNewLine _
                                              & "     FROM $LM_MST$..M_CUST AS CUST                        " & vbNewLine _
                                              & "     WHERE CUST.NRS_BR_CD    = @NRS_BR_CD   ) CUST        " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "      CUST.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "       GOODS.CUST_CD_L  = CUST.CUST_CD_L       " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "       GOODS.CUST_CD_M  = CUST.CUST_CD_M       " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "       GOODS.CUST_CD_S  = CUST.CUST_CD_S       " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "       GOODS.CUST_CD_SS = CUST.CUST_CD_SS      " & vbNewLine _
                                              & "   --営業所M                                   " & vbNewLine _
                                              & "   LEFT JOIN                                   " & vbNewLine _
                                              & "      $LM_MST$..M_NRS_BR AS NRS_BR             " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "      NRS_BR.NRS_BR_CD = @NRS_BR_CD            " & vbNewLine _
                                              & "   --倉庫M                                     " & vbNewLine _
                                              & "   LEFT JOIN                                   " & vbNewLine _
                                              & "      $LM_MST$..M_SOKO AS SOKO                 " & vbNewLine _
                                              & "   ON                                          " & vbNewLine _
                                              & "      SOKO.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                              & "   AND                                         " & vbNewLine _
                                              & "      SOKO.WH_CD     = @WH_CD                  " & vbNewLine _
                                              & "   --商品コードでの荷主帳票パターン取得        " & vbNewLine _
                                              & "    LEFT JOIN                                  " & vbNewLine _
                                              & "     $LM_MST$..M_CUST_RPT MCR1                 " & vbNewLine _
                                              & "    ON                                         " & vbNewLine _
                                              & "     MCR1.NRS_BR_CD = @NRS_BR_CD               " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MCR1.CUST_CD_L  = GOODS.CUST_CD_L         " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MCR1.CUST_CD_M  = GOODS.CUST_CD_M         " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MCR1.CUST_CD_S = GOODS.CUST_CD_S          " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MCR1.PTN_ID    = @PTN_ID                  " & vbNewLine _
                                              & "    --帳票パターン取得                         " & vbNewLine _
                                              & "    LEFT JOIN                                  " & vbNewLine _
                                              & "     $LM_MST$..M_RPT MR1                       " & vbNewLine _
                                              & "    ON                                         " & vbNewLine _
                                              & "     MR1.NRS_BR_CD = @NRS_BR_CD                " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MR1.PTN_ID    = MCR1.PTN_ID               " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MR1.PTN_CD    = MCR1.PTN_CD               " & vbNewLine _
                                              & "    AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                              & "    --存在しない場合の帳票パターン取得         " & vbNewLine _
                                              & "    LEFT JOIN                                  " & vbNewLine _
                                              & "     $LM_MST$..M_RPT MR2                       " & vbNewLine _
                                              & "    ON                                         " & vbNewLine _
                                              & "     MR2.NRS_BR_CD     = @NRS_BR_CD            " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MR2.PTN_ID        = @PTN_ID               " & vbNewLine _
                                              & "    AND                                        " & vbNewLine _
                                              & "     MR2.STANDARD_FLAG = '01'                  " & vbNewLine _
                                              & "    AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                              & "   --在庫TRS                                    " & vbNewLine _
                                              & "   LEFT OUTER JOIN                              " & vbNewLine _
                                              & "     $LM_TRN$..D_ZAI_TRS AS ZAITRS              " & vbNewLine _
                                              & "   ON                                           " & vbNewLine _
                                              & "         ZAITRS.NRS_BR_CD = @NRS_BR_CD          " & vbNewLine _
                                              & "     AND ZAITRS.INKA_NO_L  = @INKA_NO_L         " & vbNewLine _
                                              & "     AND ZAITRS.INKA_NO_M  = @INKA_NO_M         " & vbNewLine _
                                              & "     AND ZAITRS.INKA_NO_S  = @INKA_NO_S         " & vbNewLine _
                                              & "     AND ZAITRS.ZAI_REC_NO = OUTKAS.ZAI_REC_NO  " & vbNewLine _
                                              & "     AND ZAITRS.SYS_DEL_FLG = '0'               " & vbNewLine _
                                              & "   --届先M                                      " & vbNewLine _
                                              & "   LEFT OUTER JOIN                              " & vbNewLine _
                                              & "     $LM_MST$..M_DEST AS DEST                   " & vbNewLine _
                                              & "   ON                                           " & vbNewLine _
                                              & "    DEST.NRS_BR_CD   = @NRS_BR_CD               " & vbNewLine _
                                              & "   AND                                          " & vbNewLine _
                                              & "    ZAITRS.CUST_CD_L = DEST.CUST_CD_L           " & vbNewLine _
                                              & "   AND                                          " & vbNewLine _
                                              & "    ZAITRS.DEST_CD_P = DEST.DEST_CD             " & vbNewLine _
                                              & "   --出荷M                                                                       " & vbNewLine _
                                              & "   LEFT OUTER JOIN                                                               " & vbNewLine _
                                              & "     $LM_TRN$..C_OUTKA_M AS OUTKAM                                               " & vbNewLine _
                                              & "   ON                                                                            " & vbNewLine _
                                              & "     OUTKAM.NRS_BR_CD = @NRS_BR_CD                                               " & vbNewLine _
                                              & "   AND                                                                           " & vbNewLine _
                                              & "     OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                                       " & vbNewLine _
                                              & "   AND                                                                           " & vbNewLine _
                                              & "     OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                                       " & vbNewLine _
                                              & "   --出荷L                                                                       " & vbNewLine _
                                              & "   LEFT OUTER JOIN                                                               " & vbNewLine _
                                              & "     $LM_TRN$..C_OUTKA_L AS OUTKAL                                               " & vbNewLine _
                                              & "   ON                                                                            " & vbNewLine _
                                              & "     OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                         " & vbNewLine _
                                              & "   AND                                                                           " & vbNewLine _
                                              & "     OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                       " & vbNewLine _
                                              & "   --運送L                                                                       " & vbNewLine _
                                              & "   LEFT OUTER JOIN                                                               " & vbNewLine _
                                              & "     $LM_TRN$..F_UNSO_L AS UNSOL                                                 " & vbNewLine _
                                              & "   ON                                                                            " & vbNewLine _
                                              & "     UNSOL.NRS_BR_CD = @NRS_BR_CD                                                " & vbNewLine _
                                              & "   AND                                                                           " & vbNewLine _
                                              & "     UNSOL.MOTO_DATA_KB = '20'                                                   " & vbNewLine _
                                              & "   AND                                                                           " & vbNewLine _
                                              & "     UNSOL.INOUTKA_NO_L = OUTKAL.OUTKA_NO_L                                      " & vbNewLine _
                                              & "   AND                                                                           " & vbNewLine _
                                              & "     UNSOL.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                              & "   --運送会社M                                                                   " & vbNewLine _
                                              & "   LEFT OUTER JOIN                                                               " & vbNewLine _
                                              & "     $LM_MST$..M_UNSOCO AS UNSOCO                                                " & vbNewLine _
                                              & "   ON                                                                            " & vbNewLine _
                                              & "     UNSOL.NRS_BR_CD  = UNSOCO.NRS_BR_CD                                         " & vbNewLine _
                                              & "   AND                                                                           " & vbNewLine _
                                              & "     UNSOL.UNSO_CD    = UNSOCO.UNSOCO_CD                                         " & vbNewLine _
                                              & "   AND                                                                           " & vbNewLine _
                                              & "     UNSOL.UNSO_BR_CD = UNSOCO.UNSOCO_BR_CD                                      " & vbNewLine _
                                              & "   --抽出条件                                                                    " & vbNewLine _
                                              & "   WHERE                                                                         " & vbNewLine _
                                              & "     OUTKAS.NRS_BR_CD = @NRS_BR_CD                                               " & vbNewLine




#End Region '入出荷ごとTab

#End Region 'SQL

#End Region 'Const

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 帳票パターン取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "PTN_ID" _
                                            , "PTN_CD" _
                                            , "RPT_ID" _
                                            }

        Return Me.SelectListData(ds, LMD541DAC.TABLE_NM_M_RPT, LMD541DAC.SQL_SELECT_MPrt, LMD541DAC.SelectCondition.PTN1, str)

    End Function

    ''' <summary>
    ''' 印刷データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectPrtData(ByVal ds As DataSet) As DataSet

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
                                          , "SHUKEI_KB" _
                                            }

        Dim ptn As LMD541DAC.SelectCondition = Nothing
        Dim tabFlg As String = ds.Tables(LMD541DAC.TABLE_NM_IN).Rows(0).Item("TAB_FLG").ToString

        Select Case tabFlg
            Case LMD541DAC.TAB_FLG_INOUT
                ptn = SelectCondition.PTN2
            Case LMD541DAC.TAB_FLG_ZAIKO
                ptn = SelectCondition.PTN3
        End Select

        '印刷データ検索SQL作成
        Dim sql As String = Me.CreateSql(ds, ptn)

        ds = Me.SelectListData(ds, LMD541DAC.TABLE_NM_OUT, sql, ptn, str)
        Return Me.SetZanData(ds)

    End Function

#End Region

#Region "設定処理"

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
    ''' データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet, ByVal tblNm As String, ByVal sql As String, ByVal ptn As LMD541DAC.SelectCondition, ByVal str As String()) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMD541DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSelectParam(cmd, Me._Row, ptn)

        '2011.08.19 ：ArrayListを利用してパラメータを設定する場合起きるエラーを回避するため修正（"最小行サイズが8060byteを超えています"）
        'パラメータの反映
        'For Each obj As Object In Me._SqlPrmList
        '    cmd.Parameters.Add(obj)
        'Next

        MyBase.Logger.WriteSQLLog(LMD541DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        Dim max As Integer = str.Length - 1
        For i As Integer = 0 To max
            map.Add(str(i), str(i))
        Next

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, tblNm)

    End Function

    ''' <summary>
    ''' 印刷データ検索SQL作成
    ''' </summary>
    ''' <param name="ptn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSql(ByVal ds As DataSet, ByVal ptn As LMD541DAC.SelectCondition) As String

        Dim sql As StringBuilder = New StringBuilder()
        Dim dr As DataRow = ds.Tables(LMD541DAC.TABLE_NM_IN).Rows(0)
        Dim sysdel As String = dr.Item("OUTKA_DEL_FLG").ToString()

        Select Case ptn

            Case SelectCondition.PTN2 '入出荷ごと

                sql.Append(LMD541DAC.SQL_SELECT_INOUT1)
                sql.Append(Me.CreateWhereSql(ds, SelectWhereCondition.WHERE1))
                sql.Append(Me.CreateWhereSql(ds, SelectWhereCondition.WHERE2))
                '出荷取消区分
                sql.Append(Me.CreateOutkaSysDel(sysdel))
                sql.Append(vbNewLine)

                sql.Append(LMD541DAC.SQL_SELECT_INOUT2)
                sql.Append(Me.CreateWhereSql(ds, SelectWhereCondition.WHERE3))

                sql.Append(LMD541DAC.SQL_SELECT_INOUT3)
                sql.Append(Me.CreateWhereSql(ds, SelectWhereCondition.WHERE1))
                sql.Append(Me.CreateWhereSql(ds, SelectWhereCondition.WHERE4))
                '出荷取消区分
                sql.Append(Me.CreateOutkaSysDel(sysdel))
                sql.Append(vbNewLine)

            Case SelectCondition.PTN3 '在庫ごと

                sql.Append(LMD541DAC.SQL_SELECT_FIRST)
                sql.Append(LMD541DAC.SQL_SELECT_ZAIKO_MAIN1)
                sql.Append(LMD541DAC.SQL_SELECT_SAME_JOIN)
                sql.Append(LMD541DAC.SQL_SELECT_JOIN_ADD_ZAIKO)
                sql.Append(LMD541DAC.SQL_SELECT_ZAIKO_END1)

                sql.Append(LMD541DAC.SQL_SELECT_UNION)
                sql.Append(LMD541DAC.SQL_SELECT_FIRST)
                sql.Append(LMD541DAC.SQL_SELECT_ZAIKO_MAIN2)
                sql.Append(LMD541DAC.SQL_SELECT_SAME_JOIN)
                sql.Append(LMD541DAC.SQL_SELECT_JOIN_ADD)
                sql.Append(LMD541DAC.SQL_SELECT_FROM_INKA)

                sql.Append(LMD541DAC.SQL_SELECT_UNION)
                sql.Append(LMD541DAC.SQL_SELECT_FIRST)
                sql.Append(LMD541DAC.SQL_SELECT_ZAIKO_MAIN3)
                sql.Append(LMD541DAC.SQL_SELECT_SAME_JOIN)
                sql.Append(LMD541DAC.SQL_SELECT_JOIN_ADD)
                sql.Append(LMD541DAC.SQL_SELECT_FROM_OUTKA)
                sql.Append(Me.CreateWhereSql(ds, SelectWhereCondition.WHERE1))
                '出荷取消区分
                sql.Append(Me.CreateOutkaSysDel(sysdel))
                sql.Append(vbNewLine)

        End Select

        sql.Append(LMD541DAC.SQL_ORDER_BY)
        Return sql.ToString()

    End Function

    ''' <summary>
    ''' WHERE句設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="wherePtn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateWhereSql(ByVal ds As DataSet, ByVal wherePtn As LMD541DAC.SelectWhereCondition) As String

        Dim sql As StringBuilder = New StringBuilder()
        Dim dr As DataRow = ds.Tables(LMD541DAC.TABLE_NM_IN).Rows(0)

        With dr

            Dim cdNrsTo As String = .Item("CD_NRS_TO").ToString()
            Dim dateFrom As String = .Item("INKO_PLAN_DATE_FROM").ToString()
            Dim dateTo As String = .Item("INKO_PLAN_DATE_TO").ToString()
            Dim sysdel As String = .Item("OUTKA_DEL_FLG").ToString()

            Select Case wherePtn

                Case LMD541DAC.SelectWhereCondition.WHERE1

                    '出荷データ取得条件
                    sql.Append("   AND (OUTKAM.GOODS_CD_NRS = @GOODS_CD_NRS ")
                    sql.Append(vbNewLine)
                    If String.IsNullOrEmpty(cdNrsTo) = False Then
                        sql.Append(" OR (OUTKAM.GOODS_CD_NRS = @CD_NRS_TO ")
                        sql.Append(vbNewLine)
                        sql.Append(" AND OUTKAL.OUTKA_STATE_KB < '60')) ")
                        sql.Append(vbNewLine)
                    Else
                        sql.Append(" ) ")
                        sql.Append(vbNewLine)
                    End If

                Case LMD541DAC.SelectWhereCondition.WHERE2

                    '入出荷の"前残"のデータを取得する場合	
                    If String.IsNullOrEmpty(dateFrom) = False Then
                        sql.Append(" AND ZAITRS.INKO_PLAN_DATE < @INKO_PLAN_DATE_FROM ")
                        sql.Append(vbNewLine)
                        sql.Append(" AND OUTKAL.OUTKA_PLAN_DATE < @INKO_PLAN_DATE_FROM")
                        sql.Append(vbNewLine)
                    End If

                Case LMD541DAC.SelectWhereCondition.WHERE3

                    '入荷の"入荷"のデータを取得する場合は	
                    If String.IsNullOrEmpty(dateFrom) = False Then
                        sql.Append("AND @INKO_PLAN_DATE_FROM <= ZAITRS.INKO_PLAN_DATE")
                        sql.Append(vbNewLine)
                    End If
                    If String.IsNullOrEmpty(dateTo) = False Then
                        sql.Append("AND ZAITRS.INKO_PLAN_DATE <= @INKO_PLAN_DATE_TO")
                        sql.Append(vbNewLine)
                    End If

                Case LMD541DAC.SelectWhereCondition.WHERE4

                    '出荷の"出荷"、"振替"のデータを取得する場合は	
                    If String.IsNullOrEmpty(dateFrom) = False Then
                        sql.Append("AND @INKO_PLAN_DATE_FROM <= OUTKAL.OUTKA_PLAN_DATE")
                        sql.Append(vbNewLine)
                    End If
                    If String.IsNullOrEmpty(dateTo) = False Then
                        sql.Append("AND OUTKAL.OUTKA_PLAN_DATE <=  @INKO_PLAN_DATE_TO")
                        sql.Append(vbNewLine)
                    End If

            End Select

        End With

        Return sql.ToString()

    End Function

    ''' <summary>
    ''' 出荷取消区分適用
    ''' </summary>
    ''' <param name="flg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateOutkaSysDel(ByVal flg As String) As String

        Dim sql As StringBuilder = New StringBuilder()
        sql.Append(String.Empty)

        If flg.Equals("0") = True Then
            sql.Append(" AND OUTKAL.SYS_DEL_FLG = '0' ")
            sql.Append(vbNewLine)
            sql.Append(" AND OUTKAM.SYS_DEL_FLG = '0' ")
            sql.Append(vbNewLine)
            sql.Append(" AND OUTKAS.SYS_DEL_FLG = '0' ")
            sql.Append(vbNewLine)

        End If

        Return sql.ToString()

    End Function

#End Region 'ユーティリティ

    ''' <summary>
    ''' パラメータ設定モジュール
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="ptn">取得条件の切り替え</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParam(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal ptn As LMD541DAC.SelectCondition)

        With dr

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PTN_ID", LMD541DAC.PTN_ID, DBDataType.CHAR))
            
            Select Case ptn

                Case LMD541DAC.SelectCondition.PTN1 '帳票パターン取得

                    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))

                Case LMD541DAC.SelectCondition.PTN2, LMD541DAC.SelectCondition.PTN3 '印刷データ取得

                    prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CD_NRS_TO", .Item("CD_NRS_TO").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE_FROM", .Item("INKO_PLAN_DATE_FROM").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE_TO", .Item("INKO_PLAN_DATE_TO").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_DEL_FLG", .Item("OUTKA_DEL_FLG").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@KAZU_KB", .Item("KAZU_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SHUKEI_KB", .Item("SHUKEI_KB").ToString(), DBDataType.CHAR))

            End Select

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(SqlCommand)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="ptn">取得条件の切り替え</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParam(ByVal cmd As SqlCommand, ByVal dr As DataRow, ByVal ptn As LMD541DAC.SelectCondition)

        With dr

            'パラメータ設定

            Me.GetSqlParameterData(cmd, "@NRS_BR_CD", .Item("NRS_BR_CD").ToString())
            Me.GetSqlParameterData(cmd, "@PTN_ID", LMD541DAC.PTN_ID)

            Select Case ptn

                Case LMD541DAC.SelectCondition.PTN1 '帳票パターン取得

                    Me.GetSqlParameterData(cmd, "@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString())

                Case LMD541DAC.SelectCondition.PTN2, LMD541DAC.SelectCondition.PTN3 '印刷データ取得

                    Me.GetSqlParameterData(cmd, "@WH_CD", .Item("WH_CD").ToString())
                    Me.GetSqlParameterData(cmd, "@LOT_NO", .Item("LOT_NO").ToString())
                    Me.GetSqlParameterData(cmd, "@INKA_NO_L", .Item("INKA_NO_L").ToString())
                    Me.GetSqlParameterData(cmd, "@INKA_NO_M", .Item("INKA_NO_M").ToString())
                    Me.GetSqlParameterData(cmd, "@INKA_NO_S", .Item("INKA_NO_S").ToString())
                    Me.GetSqlParameterData(cmd, "@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString())
                    Me.GetSqlParameterData(cmd, "@CD_NRS_TO", .Item("CD_NRS_TO").ToString())
                    Me.GetSqlParameterData(cmd, "@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString())
                    Me.GetSqlParameterData(cmd, "@INKO_PLAN_DATE_FROM", .Item("INKO_PLAN_DATE_FROM").ToString())
                    Me.GetSqlParameterData(cmd, "@INKO_PLAN_DATE_TO", .Item("INKO_PLAN_DATE_TO").ToString())
                    Me.GetSqlParameterData(cmd, "@OUTKA_DEL_FLG", .Item("OUTKA_DEL_FLG").ToString())
                    Me.GetSqlParameterData(cmd, "@KAZU_KB", .Item("KAZU_KB").ToString())
                    Me.GetSqlParameterData(cmd, "@SHUKEI_KB", .Item("SHUKEI_KB").ToString())

            End Select

        End With

    End Sub

    ''' <summary>
    ''' Sqlパラメータデータ取得
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <param name="prmNm"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSqlParameterData(ByVal cmd As SqlCommand, ByVal prmNm As String, ByVal value As String) As SqlParameter
        GetSqlParameterData = cmd.Parameters.AddWithValue(prmNm, value)
        Return GetSqlParameterData
    End Function

    ''' <summary>
    ''' 残数・残数量設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetZanData(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMD541DAC.TABLE_NM_OUT)
        Dim dr As DataRow = Nothing

        Dim yojitu As String = String.Empty
        Dim sumKosu As Decimal = 0
        Dim sumSuryo As Decimal = 0

        For i As Integer = 0 To dt.Rows.Count - 1

            dr = dt.Rows(i)
            yojitu = dr.Item("YOJITU").ToString()

            Select Case yojitu

                Case "消", "ヨ", "サ"

                Case Else
                    sumKosu = sumKosu + Convert.ToDecimal(dr.Item("INKA_KOSU").ToString()) - Convert.ToDecimal(dr.Item("OUTKA_KOSU").ToString())
                    sumSuryo = sumKosu + Convert.ToDecimal(dr.Item("INKA_SURYO").ToString()) - Convert.ToDecimal(dr.Item("OUTKA_SURYO").ToString())

            End Select

            dt.Rows(i).Item("ZAN_KOSU") = sumKosu
            dt.Rows(i).Item("ZAN_SURYO") = sumSuryo

        Next

        Return ds

    End Function

#End Region

#End Region

End Class
