' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI510DAC : 運賃請求明細書(三井化学ポリウレタン用)
'  作  成  者       :  篠原
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI510DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI510DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "印刷種別"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                       " & vbNewLine _
                                            & "	MCPU.NRS_BR_CD                                        AS NRS_BR_CD   " & vbNewLine _
                                            & ",'79'                                                  AS PTN_ID      " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                     " & vbNewLine _
                                            & "		 WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	 	 ELSE MR3.PTN_CD END                              AS PTN_CD      " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                     " & vbNewLine _
                                            & "  	 WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
                                            & "		 ELSE MR3.RPT_ID END                              AS RPT_ID      " & vbNewLine

#End Region

#Region "SELECT句"

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                 " & vbNewLine _
                                        & "(SELECT  DISTINCT                                           " & vbNewLine _
                                        & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID    " & vbNewLine _
                                        & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID    " & vbNewLine _
                                        & "        ELSE MR3.RPT_ID END       AS RPT_ID                 " & vbNewLine _
                                        & "  FROM                                                      " & vbNewLine _
                                        & "       $LM_TRN$..I_MCPU_UNCHIN_CHK MCPU                       " & vbNewLine _
                                        & "                                                            " & vbNewLine _
                                        & "--運送L                                                     " & vbNewLine _
                                        & " LEFT JOIN $LM_TRN$..F_UNSO_L AS UNSO                         " & vbNewLine _
                                        & "   ON MCPU.UNSO_NO_L   = UNSO.UNSO_NO_L                     " & vbNewLine _
                                        & "  AND UNSO.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                        & " --運送Ｍ                                                   " & vbNewLine _
                                        & " LEFT JOIN $LM_TRN$..F_UNSO_M AS UNSOM                        " & vbNewLine _
                                        & "   ON MCPU.UNSO_NO_L     = UNSOM.UNSO_NO_L                  " & vbNewLine _
                                        & "  AND UNSOM.SYS_DEL_FLG  = '0'                              " & vbNewLine _
                                        & " --商品マスタ                                               " & vbNewLine _
                                        & " LEFT JOIN $LM_MST$..M_GOODS AS GOODS                         " & vbNewLine _
                                        & "  ON MCPU.NRS_BR_CD      = GOODS.NRS_BR_CD                  " & vbNewLine _
                                        & " AND UNSOM.GOODS_CD_NRS  = GOODS.GOODS_CD_NRS               " & vbNewLine _
                                        & "--運賃での荷主帳票パターン取得                              " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                           " & vbNewLine _
                                        & "  ON MCPU.NRS_BR_CD = MCR1.NRS_BR_CD                        " & vbNewLine _
                                        & " AND MCPU.CUST_CD_L = MCR1.CUST_CD_L                        " & vbNewLine _
                                        & " AND MCPU.CUST_CD_M = MCR1.CUST_CD_M                        " & vbNewLine _
                                        & " AND MCR1.CUST_CD_S = '00'                                  " & vbNewLine _
                                        & " AND MCR1.PTN_ID    = '79'                                  " & vbNewLine _
                                        & "--帳票パターン取得                                          " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_RPT MR1                                 " & vbNewLine _
                                        & "  ON MR1.NRS_BR_CD   = MCR1.NRS_BR_CD                       " & vbNewLine _
                                        & " AND MR1.PTN_ID      = MCR1.PTN_ID                          " & vbNewLine _
                                        & " AND MR1.PTN_CD      = MCR1.PTN_CD                          " & vbNewLine _
                                        & " AND MR1.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                        & "--商品Mの荷主での荷主帳票パターン取得                       " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                           " & vbNewLine _
                                        & "  ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                       " & vbNewLine _
                                        & " AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                       " & vbNewLine _
                                        & " AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                       " & vbNewLine _
                                        & " AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                       " & vbNewLine _
                                        & " AND MCR2.PTN_ID     = '79'                                 " & vbNewLine _
                                        & "--帳票パターン取得                                          " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_RPT MR2                                 " & vbNewLine _
                                        & "ON  MR2.NRS_BR_CD   = MCR2.NRS_BR_CD                        " & vbNewLine _
                                        & "AND MR2.PTN_ID      = MCR2.PTN_ID                           " & vbNewLine _
                                        & "AND MR2.PTN_CD      = MCR2.PTN_CD                           " & vbNewLine _
                                        & "AND MR2.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                        & "--存在しない場合の帳票パターン取得                          " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_RPT MR3                                 " & vbNewLine _
                                        & "ON  MR3.NRS_BR_CD         = MCPU.NRS_BR_CD                  " & vbNewLine _
                                        & "AND MR3.PTN_ID            = '79'                            " & vbNewLine _
                                        & "AND MR3.STANDARD_FLAG     = '01'                            " & vbNewLine _
                                        & "AND MR3.SYS_DEL_FLG       = '0'                             " & vbNewLine _
                                        & "                                                            " & vbNewLine _
                                        & "  WHERE MCPU.NRS_BR_CD    = @NRS_BR_CD --条件1＠            " & vbNewLine _
                                        & "    AND MCPU.SYS_DEL_FLG  = '0'        --条件2(固定)        " & vbNewLine _
                                        & "    AND MCPU.DECI_UNCHIN <> 0                               " & vbNewLine _
                                        & "                                                            " & vbNewLine _
                                        & ") RPT_ID                                                    " & vbNewLine




#End Region

#Region "FROM句"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_MPrt As String = "FROM                                              " & vbNewLine _
                                          & " $LM_TRN$..I_MCPU_UNCHIN_CHK AS MCPU              " & vbNewLine _
                                          & " --運送L                                          " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..F_UNSO_L AS UNSO             " & vbNewLine _
                                          & "   ON  MCPU.UNSO_NO_L   = UNSO.UNSO_NO_L          " & vbNewLine _
                                          & "  AND UNSO.SYS_DEL_FLG  ='0'                      " & vbNewLine _
                                          & " --運送Ｍ                                         " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..F_UNSO_M AS UNSOM            " & vbNewLine _
                                          & "    ON MCPU.UNSO_NO_L     = UNSOM.UNSO_NO_L       " & vbNewLine _
                                          & "   AND UNSOM.SYS_DEL_FLG  ='0'                    " & vbNewLine _
                                          & " --商品マスタ                                     " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_GOODS AS GOODS             " & vbNewLine _
                                          & "   ON MCPU.NRS_BR_CD     = GOODS.NRS_BR_CD        " & vbNewLine _
                                          & "  AND UNSOM.GOODS_CD_NRS = GOODS.GOODS_CD_NRS     " & vbNewLine _
                                          & "--運賃での荷主帳票パターン取得                    " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR1              " & vbNewLine _
                                          & "   ON  MCPU.NRS_BR_CD = MCR1.NRS_BR_CD            " & vbNewLine _
                                          & "  AND MCPU.CUST_CD_L  = MCR1.CUST_CD_L            " & vbNewLine _
                                          & "  AND MCPU.CUST_CD_M  = MCR1.CUST_CD_M            " & vbNewLine _
                                          & "  AND MCR1.CUST_CD_S  = '00'                      " & vbNewLine _
                                          & "  AND MCR1.PTN_ID     = '79'                      " & vbNewLine _
                                          & "--帳票パターン取得                                " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_RPT MR1                    " & vbNewLine _
                                          & "   ON  MR1.NRS_BR_CD  = MCR1.NRS_BR_CD            " & vbNewLine _
                                          & "  AND MR1.PTN_ID      = MCR1.PTN_ID               " & vbNewLine _
                                          & "  AND MR1.PTN_CD      = MCR1.PTN_CD               " & vbNewLine _
                                          & "  AND MR1.SYS_DEL_FLG = '0'                       " & vbNewLine _
                                          & "--商品Mの荷主での荷主帳票パターン取得             " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR2              " & vbNewLine _
                                          & "   ON  GOODS.NRS_BR_CD = MCR2.NRS_BR_CD           " & vbNewLine _
                                          & "  AND GOODS.CUST_CD_L  = MCR2.CUST_CD_L           " & vbNewLine _
                                          & "  AND GOODS.CUST_CD_M  = MCR2.CUST_CD_M           " & vbNewLine _
                                          & "  AND GOODS.CUST_CD_S  = MCR2.CUST_CD_S           " & vbNewLine _
                                          & "  AND MCR2.PTN_ID      = '79'                     " & vbNewLine _
                                          & "--帳票パターン取得                                " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_RPT MR2                    " & vbNewLine _
                                          & "   ON  MR2.NRS_BR_CD  = MCR2.NRS_BR_CD            " & vbNewLine _
                                          & "  AND MR2.PTN_ID      = MCR2.PTN_ID               " & vbNewLine _
                                          & "  AND MR2.PTN_CD      = MCR2.PTN_CD               " & vbNewLine _
                                          & "  AND MR2.SYS_DEL_FLG = '0'                       " & vbNewLine _
                                          & "--存在しない場合の帳票パターン取得                " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_RPT MR3                    " & vbNewLine _
                                          & "   ON  MR3.NRS_BR_CD    = MCPU.NRS_BR_CD          " & vbNewLine _
                                          & "  AND MR3.PTN_ID        = '79'                    " & vbNewLine _
                                          & "  AND MR3.STANDARD_FLAG = '01'                    " & vbNewLine _
                                          & "  AND MR3.SYS_DEL_FLG   = '0'                     " & vbNewLine _
                                          & "                                                  " & vbNewLine _
                                          & "   WHERE MCPU.NRS_BR_CD   = @NRS_BR_CD            " & vbNewLine _
                                          & "   AND   MCPU.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                          & "   AND   MCPU.DECI_UNCHIN <> 0                    " & vbNewLine

    'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
    'Private Const SQL_FROM As String = "         , MCPU.OUTKA_PLAN_DATE                      --出荷日            " & vbNewLine _
    '                                    & "      , M_GOODS.GOODS_NM_1    AS GOODS_NM         --商品名            " & vbNewLine _
    '                                    & "      , SUM(MCPU.WT)          AS SUM_WT           --重量              " & vbNewLine _
    '                                    & "      , SUM(MCPU.DECI_UNCHIN) AS SUM_DECI_UNCHIN  --金額              " & vbNewLine _
    '                                    & "      , M_DEST.DEST_NM                            --届先名            " & vbNewLine _
    '                                    & "      , M_DEST.AD_1           AS DEST_AD          --届先住所          " & vbNewLine _
    '                                    & "      , MCPU.CUST_ORD_NO      AS CUST_ORD_NO      --備考              " & vbNewLine _
    '                                    & "      , MCPU.MOTO_DATA_KB                         --元データ区分      " & vbNewLine _
    '                                    & "      , MCPU.SEIQ_CD          AS SEIQ_CD          --請求先コード(MCPU)" & vbNewLine _
    '                                    & "      , MCPU.FREE_C01         AS ROSEN_KUIKI_KB   --路線              " & vbNewLine _
    '                                    & "      , M_SEIQTO.SEIQTO_CD                        --請求先コード(M)   " & vbNewLine _
    '                                    & "      , M_SEIQTO.SEIQTO_NM                        --請求先名(M)       " & vbNewLine _
    '                                    & "      , M_NRS_BR.NRS_BR_NM                                            " & vbNewLine _
    '                                    & "      , @T_DATE               AS T_DATE                               " & vbNewLine _
    '                                    & "  FROM                                                                " & vbNewLine _
    '                                    & "       $LM_TRN$..I_MCPU_UNCHIN_CHK MCPU                               " & vbNewLine _
    '                                    & "       INNER JOIN $LM_MST$..M_NRS_BR M_NRS_BR                         " & vbNewLine _
    '                                    & "               ON MCPU.NRS_BR_CD       = M_NRS_BR.NRS_BR_CD           " & vbNewLine _
    '                                    & "              AND M_NRS_BR.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                    & "       INNER JOIN $LM_MST$..M_SEIQTO M_SEIQTO                         " & vbNewLine _
    '                                    & "               ON MCPU.NRS_BR_CD        = M_SEIQTO.NRS_BR_CD          " & vbNewLine _
    '                                    & "              AND MCPU.SEIQ_CD         = M_SEIQTO.SEIQTO_CD           " & vbNewLine _
    '                                    & "              AND M_SEIQTO.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                    & "		  INNER JOIN $LM_MST$..M_DEST M_DEST                             " & vbNewLine _
    '                                    & "               ON MCPU.NRS_BR_CD     = M_DEST.NRS_BR_CD               " & vbNewLine _
    '                                    & "              AND MCPU.CUST_CD_L     = M_DEST.CUST_CD_L               " & vbNewLine _
    '                                    & "              AND MCPU.DEST_CD       = M_DEST.DEST_CD                 " & vbNewLine _
    '                                    & "              AND M_DEST.SYS_DEL_FLG = '0'                            " & vbNewLine _
    '                                    & "        LEFT JOIN $LM_MST$..M_GOODS M_GOODS                           " & vbNewLine _
    '                                    & "               ON MCPU.NRS_BR_CD      = M_GOODS.NRS_BR_CD             " & vbNewLine _
    '                                    & "              AND MCPU.GOODS_CD_NRS   = M_GOODS.GOODS_CD_NRS          " & vbNewLine _
    '                                    & "              AND M_GOODS.SYS_DEL_FLG = '0'                           " & vbNewLine _
    '                                    & "                                                                      " & vbNewLine _
    '                                    & "  WHERE MCPU.NRS_BR_CD    = @NRS_BR_CD                   --条件1＠    " & vbNewLine _
    '                                    & "    AND MCPU.SYS_DEL_FLG  = '0'                          --条件2(固定)" & vbNewLine _
    '                                    & "    AND MCPU.CUST_CD_L    = @CUST_CD_L                   --条件3＠    " & vbNewLine _
    '                                    & "    AND MCPU.CUST_CD_M    = @CUST_CD_M                   --条件4＠    " & vbNewLine _
    '                                    & "    AND MCPU.OUTKA_PLAN_DATE BETWEEN @F_DATE AND @T_DATE --条件5＠    " & vbNewLine
    Private Const SQL_FROM As String = "         , MCPU.OUTKA_PLAN_DATE                      --出荷日            " & vbNewLine _
                                        & "      , M_GOODS.GOODS_NM_1    AS GOODS_NM         --商品名            " & vbNewLine _
                                        & "      , SUM(MCPU.WT)          AS SUM_WT           --重量              " & vbNewLine _
                                        & "      , SUM(MCPU.DECI_UNCHIN) AS SUM_DECI_UNCHIN  --金額              " & vbNewLine _
                                        & "      , M_DEST.DEST_NM                            --届先名            " & vbNewLine _
                                        & "      , M_DEST.AD_1           AS DEST_AD          --届先住所          " & vbNewLine _
                                        & "      , MCPU.CUST_ORD_NO      AS CUST_ORD_NO      --備考              " & vbNewLine _
                                        & "      , MCPU.MOTO_DATA_KB                         --元データ区分      " & vbNewLine _
                                        & "      , MCPU.SEIQ_CD          AS SEIQ_CD          --請求先コード(MCPU)" & vbNewLine _
                                        & "      , MCPU.FREE_C01         AS ROSEN_KUIKI_KB   --路線              " & vbNewLine _
                                        & "      , M_SEIQTO.SEIQTO_CD                        --請求先コード(M)   " & vbNewLine _
                                        & "      , M_SEIQTO.SEIQTO_NM                        --請求先名(M)       " & vbNewLine _
                                        & "      , M_NRS_BR.NRS_BR_NM                                            " & vbNewLine _
                                        & "      , @T_DATE               AS T_DATE                               " & vbNewLine _
                                        & "  FROM                                                                " & vbNewLine _
                                        & "       $LM_TRN$..I_MCPU_UNCHIN_CHK MCPU                               " & vbNewLine _
                                        & "       INNER JOIN $LM_MST$..M_NRS_BR M_NRS_BR                         " & vbNewLine _
                                        & "               ON MCPU.NRS_BR_CD       = M_NRS_BR.NRS_BR_CD           " & vbNewLine _
                                        & "              AND M_NRS_BR.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                        & "       INNER JOIN $LM_MST$..M_SEIQTO M_SEIQTO                         " & vbNewLine _
                                        & "               ON MCPU.NRS_BR_CD        = M_SEIQTO.NRS_BR_CD          " & vbNewLine _
                                        & "              AND MCPU.SEIQ_CD         = M_SEIQTO.SEIQTO_CD           " & vbNewLine _
                                        & "              AND M_SEIQTO.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                        & "		  INNER JOIN $LM_MST$..M_DEST M_DEST                             " & vbNewLine _
                                        & "               ON MCPU.NRS_BR_CD     = M_DEST.NRS_BR_CD               " & vbNewLine _
                                        & "              AND MCPU.CUST_CD_L     = M_DEST.CUST_CD_L               " & vbNewLine _
                                        & "              AND MCPU.DEST_CD       = M_DEST.DEST_CD                 " & vbNewLine _
                                        & "              AND M_DEST.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                        & "        LEFT JOIN $LM_MST$..M_GOODS M_GOODS                           " & vbNewLine _
                                        & "               ON MCPU.NRS_BR_CD      = M_GOODS.NRS_BR_CD             " & vbNewLine _
                                        & "              AND MCPU.GOODS_CD_NRS   = M_GOODS.GOODS_CD_NRS          " & vbNewLine _
                                        & "              AND M_GOODS.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                        & "                                                                      " & vbNewLine _
                                        & "  WHERE MCPU.NRS_BR_CD    = @NRS_BR_CD                   --条件1＠    " & vbNewLine _
                                        & "    AND MCPU.SYS_DEL_FLG  = '0'                          --条件2(固定)" & vbNewLine _
                                        & "    AND MCPU.CUST_CD_L    = @CUST_CD_L                   --条件3＠    " & vbNewLine _
                                        & "    AND MCPU.CUST_CD_M    = @CUST_CD_M                   --条件4＠    " & vbNewLine _
                                        & "  --(2012.09.19)要望番号1449：抽出条件を可変にする --- START ---      " & vbNewLine _
                                        & "  --AND MCPU.CUST_CD_S    = @CUST_CD_S                   --条件5＠    " & vbNewLine _
                                        & "  --AND MCPU.CUST_CD_SS   = @CUST_CD_SS                  --条件6＠    " & vbNewLine _
                                        & "  --(2012.09.19)要望番号1449：抽出条件を可変にする ---  END  ---      " & vbNewLine _
                                        & "    AND MCPU.OUTKA_PLAN_DATE BETWEEN @F_DATE AND @T_DATE --条件7＠    " & vbNewLine
    'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい



#End Region

#Region "GROUP BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = "GROUP BY  MCPU.MOTO_DATA_KB     " & vbNewLine _
                                         & "     , MCPU.SEIQ_CD             " & vbNewLine _
                                         & "     , MCPU.OUTKA_PLAN_DATE     " & vbNewLine _
                                         & "     , MCPU.CUST_ORD_NO         " & vbNewLine _
                                         & "     , M_DEST.DEST_NM           " & vbNewLine _
                                         & "     , M_GOODS.GOODS_NM_1       " & vbNewLine _
                                         & "     , M_DEST.AD_1              " & vbNewLine _
                                         & "     , MCPU.FREE_C01            " & vbNewLine _
                                         & "     , M_SEIQTO.SEIQTO_CD       " & vbNewLine _
                                         & "     , M_SEIQTO.SEIQTO_NM       " & vbNewLine _
                                         & "     , M_NRS_BR.NRS_BR_NM       " & vbNewLine


#End Region

#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                         " & vbNewLine _
                                         & "         M_SEIQTO.SEIQTO_CD   ASC                " & vbNewLine _
                                         & "       , MCPU.FREE_C01        ASC                " & vbNewLine _
                                         & "       , MCPU.OUTKA_PLAN_DATE ASC                " & vbNewLine _
                                         & "       , MCPU.CUST_ORD_NO     ASC                " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMI510IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI510DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMI510DAC.SQL_FROM_MPrt)        'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI510DAC", "SelectMPrt", cmd)

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
    ''' 運賃テーブル対象データ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI510IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI510DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMI510DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMI510DAC.SQL_GROUP_BY)             'SQL構築(データ抽出用GROUP BY)
        Me._StrSql.Append(LMI510DAC.SQL_ORDER_BY)             'SQL構築(データ抽出用ORDER BY)


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI510DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("DECI_WT", "SUM_WT")
        map.Add("DECI_UNCHIN", "SUM_DECI_UNCHIN")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD", "DEST_AD")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("SEIQ_CD", "SEIQ_CD")
        map.Add("ROSEN_KUIKI_KB", "ROSEN_KUIKI_KB")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("T_DATE", "T_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI510OUT")

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
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所コード
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '荷主コード（大）
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))

            '荷主コード（中）
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))

            '日付From
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", Me._Row.Item("F_DATE").ToString(), DBDataType.CHAR))

            '日付To
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", Me._Row.Item("T_DATE").ToString(), DBDataType.CHAR))

            '(2012.09.19)要望番号1449：画面上、入力していない場合は、抽出条件としない --- START ---
            '荷主コード（小）
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MCPU.CUST_CD_S = @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", whereStr, DBDataType.CHAR))
            End If

            '荷主コード（極小）
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MCPU.CUST_CD_SS = @CUST_CD_SS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", whereStr, DBDataType.CHAR))
            End If

            'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
            '荷主コード（極小）
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", Me._Row.Item("CUST_CD_SS").ToString(), DBDataType.CHAR))

            ''荷主コード（小）
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", Me._Row.Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

            '(2012.09.19)要望番号1449：画面上、入力していない場合は、抽出条件としない ---  END  ---
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

#End Region

End Class
