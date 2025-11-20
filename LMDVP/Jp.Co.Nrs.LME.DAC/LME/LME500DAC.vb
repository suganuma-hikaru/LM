' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME       : 作業
'  プログラムID     :  LME500    : 作業料明細書
'  作  成  者       :  [KIM]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LME500DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LME500DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' 検索パターン
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum SelectCondition As Integer
        PTN1  '出力対象帳票パターン取得
        PTN2  'データ検索
    End Enum

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LME500DAC"

    ''' <summary>
    ''' 帳票パターン取得テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_M_RPT As String = "M_RPT"

    ''' <summary>
    ''' INテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LME500IN"

    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LME500OUT"

    ''' <summary>
    ''' 帳票ID
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PTN_ID As String = "39"

    ''' <summary>
    ''' 作業料請求明細書フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SEIKYU_ON As String = "01"
    Private Const SEIKYU_OFF As String = "02"

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
                                            & "	      ELSE MR2.RPT_ID END                            AS RPT_ID      " & vbNewLine

#End Region '帳票ID

#Region "共通SQL"

    ''' <summary>
    ''' 共通FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ALL_FROM As String = "  FROM                                                  " & vbNewLine _
                                         & "    $LM_TRN$..E_SAGYO   E_SAGYO                         " & vbNewLine _
                                         & "  --商品M                                               " & vbNewLine _
                                         & "  LEFT JOIN                                             " & vbNewLine _
                                         & "    $LM_MST$..M_GOODS   M_GOODS                         " & vbNewLine _
                                         & "  ON                                                    " & vbNewLine _
                                         & "    M_GOODS.NRS_BR_CD     = @NRS_BR_CD                  " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    E_SAGYO.GOODS_CD_NRS  = M_GOODS.GOODS_CD_NRS        " & vbNewLine _
                                         & "  --荷主M(Mレベル)                                      " & vbNewLine _
                                         & "  LEFT JOIN                                             " & vbNewLine _
                                         & "    $LM_MST$..M_CUST M_CUST_M                           " & vbNewLine _
                                         & "  ON                                                    " & vbNewLine _
                                         & "    M_CUST_M.NRS_BR_CD   = @NRS_BR_CD                   " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    E_SAGYO.CUST_CD_L  = M_CUST_M.CUST_CD_L             " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    E_SAGYO.CUST_CD_M  = M_CUST_M.CUST_CD_M             " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    M_GOODS.CUST_CD_S  = M_CUST_M.CUST_CD_S             " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    M_GOODS.CUST_CD_SS = M_CUST_M.CUST_CD_SS            " & vbNewLine _
                                         & "  --荷主M(Lレベル)                                      " & vbNewLine _
                                         & "  LEFT JOIN                                             " & vbNewLine _
                                         & "    $LM_MST$..M_CUST M_CUST_L                           " & vbNewLine _
                                         & "  ON                                                    " & vbNewLine _
                                         & "    M_CUST_L.NRS_BR_CD   = @NRS_BR_CD                   " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    E_SAGYO.CUST_CD_L  = M_CUST_L.CUST_CD_L             " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    E_SAGYO.CUST_CD_M  = M_CUST_L.CUST_CD_M             " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    M_CUST_L.CUST_CD_S  = '00'                          " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    M_CUST_L.CUST_CD_SS = '00'                          " & vbNewLine _
                                         & "  --商品コードでの荷主帳票パターン取得                  " & vbNewLine _
                                         & "  LEFT JOIN                                             " & vbNewLine _
                                         & "    $LM_MST$..M_CUST_RPT MCR1                           " & vbNewLine _
                                         & "  ON                                                    " & vbNewLine _
                                         & "    MCR1.NRS_BR_CD = @NRS_BR_CD                         " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    MCR1.CUST_CD_L = M_CUST_L.CUST_CD_L                   " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    MCR1.CUST_CD_M = M_CUST_L.CUST_CD_M                   " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    MCR1.CUST_CD_S = ISNULL(M_GOODS.CUST_CD_S,'00')     " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    MCR1.PTN_ID    = '39'                               " & vbNewLine _
                                         & "  --帳票パターン取得                                    " & vbNewLine _
                                         & "  LEFT JOIN                                             " & vbNewLine _
                                         & "    $LM_MST$..M_RPT MR1                                 " & vbNewLine _
                                         & "  ON                                                    " & vbNewLine _
                                         & "    MR1.NRS_BR_CD = @NRS_BR_CD                          " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    MR1.PTN_ID    = MCR1.PTN_ID                         " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    MR1.PTN_CD    = MCR1.PTN_CD                         " & vbNewLine _
                                         & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                         & "  --存在しない場合の帳票パターン取得                    " & vbNewLine _
                                         & "  LEFT JOIN                                             " & vbNewLine _
                                         & "    $LM_MST$..M_RPT MR2                                 " & vbNewLine _
                                         & "  ON                                                    " & vbNewLine _
                                         & "    MR2.NRS_BR_CD     = @NRS_BR_CD                      " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    MR2.PTN_ID        = @PTN_ID                         " & vbNewLine _
                                         & "  AND                                                   " & vbNewLine _
                                         & "    MR2.STANDARD_FLAG = '01'                            " & vbNewLine _
                                         & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine

#End Region '共通SQL

#Region "印刷データ取得"

    ''' <summary>
    ''' 印刷データ取得SQL（ヘッダ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FIRST As String = " SELECT                                                     " & vbNewLine _
                                             & "     CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID       " & vbNewLine _
                                             & "          ELSE MR2.RPT_ID END           AS RPT_ID           " & vbNewLine _
                                             & "   , @NRS_BR_CD                         AS NRS_BR_CD        " & vbNewLine _
                                             & "   , E_SAGYO.SEIQTO_CD                  AS SEIQTO_CD        " & vbNewLine _
                                             & "   , M_SEIQTO.SEIQTO_NM                 AS SEIQTO_NM        " & vbNewLine _
                                             & "   , E_SAGYO.CUST_CD_L                  AS CUST_CD_L        " & vbNewLine _
                                             & "   , E_SAGYO.CUST_CD_M                  AS CUST_CD_M        " & vbNewLine _
                                             & "   , CASE WHEN M_GOODS.CUST_CD_S IS NOT NULL                " & vbNewLine _
                                             & "          THEN M_GOODS.CUST_CD_S                            " & vbNewLine _
                                             & "          ELSE '00'                                         " & vbNewLine _
                                             & "          END                           AS CUST_CD_S        " & vbNewLine _
                                             & "   , CASE WHEN M_GOODS.CUST_CD_SS IS NOT NULL               " & vbNewLine _
                                             & "          THEN M_GOODS.CUST_CD_SS                           " & vbNewLine _
                                             & "          ELSE '00'                                         " & vbNewLine _
                                             & "          END                           AS CUST_CD_SS       " & vbNewLine _
                                             & "   , CASE WHEN M_CUST_M.CUST_NM_L IS NOT NULL               " & vbNewLine _
                                             & "          THEN M_CUST_M.CUST_NM_L                           " & vbNewLine _
                                             & "          ELSE M_CUST_L.CUST_NM_L                           " & vbNewLine _
                                             & "          END                           AS CUST_NM_L        " & vbNewLine _
                                             & "   , CASE WHEN M_CUST_M.CUST_NM_M IS NOT NULL               " & vbNewLine _
                                             & "          THEN M_CUST_M.CUST_NM_M                           " & vbNewLine _
                                             & "          ELSE M_CUST_L.CUST_NM_M                           " & vbNewLine _
                                             & "          END                           AS CUST_NM_M        " & vbNewLine _
                                             & "   , CASE WHEN M_CUST_M.CUST_NM_S IS NOT NULL               " & vbNewLine _
                                             & "          THEN M_CUST_M.CUST_NM_S                           " & vbNewLine _
                                             & "          ELSE M_CUST_L.CUST_NM_S                           " & vbNewLine _
                                             & "          END                           AS CUST_NM_S        " & vbNewLine _
                                             & "   , CASE WHEN M_CUST_M.CUST_NM_SS IS NOT NULL              " & vbNewLine _
                                             & "          THEN M_CUST_M.CUST_NM_SS                          " & vbNewLine _
                                             & "          ELSE M_CUST_L.CUST_NM_SS                          " & vbNewLine _
                                             & "          END                           AS CUST_NM_SS       " & vbNewLine _
                                             & "   ,CASE WHEN SUBSTRING(@T_DATE, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                             & "         THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, M_NRS_BR.NRS_BR_NM) " & vbNewLine _
                                             & "         ELSE M_NRS_BR.NRS_BR_NM                                " & vbNewLine _
                                             & "         END  AS NRS_BR_NM                                      " & vbNewLine _
                                             & "   , CASE WHEN E_SAGYO.NRS_BR_CD = '95' THEN ''             " & vbNewLine _
                                             & "          WHEN M_SOKO.WH_KB = '01' THEN M_SOKO.AD_1 + M_SOKO.AD_2                  " & vbNewLine _
                                             & "          ELSE M_NRS_BR.AD_1 + M_NRS_BR.AD_2 END       AS NRS_BR_AD                " & vbNewLine _
                                             & "   , CASE WHEN M_SOKO.WH_KB = '01' THEN M_SOKO.TEL          " & vbNewLine _
                                             & "          ELSE M_NRS_BR.TEL END         AS NRS_BR_TEL       " & vbNewLine _
                                             & "--DEL 2022/07/07 030845   , M_NRS_BR.AD_1 + M_NRS_BR.AD_2      AS NRS_BR_AD        " & vbNewLine _
                                             & "--DEL 2022/07/07 030845   , M_NRS_BR.TEL                       AS NRS_BR_TEL       " & vbNewLine _
                                             & "   , E_SAGYO.SAGYO_COMP_DATE            AS SAGYO_COMP_DATE  " & vbNewLine _
                                             & "   , E_SAGYO.GOODS_CD_NRS               AS GOODS_CD_NRS     " & vbNewLine _
                                             & "   , E_SAGYO.GOODS_NM_NRS               AS GOODS_NM_NRS     " & vbNewLine _
                                             & "   , E_SAGYO.LOT_NO                     AS LOT_NO           " & vbNewLine _
                                             & "   , E_SAGYO.SAGYO_CD                   AS SAGYO_CD         " & vbNewLine _
                                             & "   , E_SAGYO.SAGYO_NM                   AS SAGYO_NM         " & vbNewLine _
                                             & "   , E_SAGYO.REMARK_SKYU                AS REMARK_SKYU      " & vbNewLine _
                                             & "   , E_SAGYO.DEST_CD                    AS DEST_CD          " & vbNewLine _
                                             & "   , E_SAGYO.DEST_NM                    AS DEST_NM          " & vbNewLine _
                                             & "   , E_SAGYO.IOZS_KB                    AS IOZS_KB          " & vbNewLine _
                                             & "   ,CASE WHEN @PRT_SHUBETU ='01' THEN ''                    " & vbNewLine _
                                             & "	  ELSE Z_KBN1.KBN_NM1 END           AS IOZS_NM          " & vbNewLine _
                                             & "   , E_SAGYO.SAGYO_NB                   AS SAGYO_NB         " & vbNewLine _
                                             & "   , E_SAGYO.SAGYO_UP                   AS SAGYO_UP         " & vbNewLine _
                                             & "   , E_SAGYO.SAGYO_GK                   AS SAGYO_GK         " & vbNewLine _
                                             & "   , @F_DATE                            AS F_DATE           " & vbNewLine _
                                             & "   , @T_DATE                            AS T_DATE           " & vbNewLine _
                                             & "   , Z_KBN2.KBN_NM2                     AS SAGYO_TITLE      " & vbNewLine _
                                             & "   , CASE WHEN MCD.SET_NAIYO = '01'                         " & vbNewLine _
                                             & "     THEN MCD.SET_NAIYO                                     " & vbNewLine _
                                             & "     ELSE '00'                                              " & vbNewLine _
                                             & "     END                                AS OFB_NM           " & vbNewLine _
                                             & " -- LME510作業料明細書(ローム用)対応 -- START --            " & vbNewLine _
                                             & "   , CASE WHEN Z_KBN3.KBN_CD <> '' THEN '0'                 " & vbNewLine _
                                             & "     ELSE '1'                                               " & vbNewLine _
                                             & "     END                                AS BUNSEKI_UMU_FLG  " & vbNewLine _
                                             & " -- LME510作業料明細書(ローム用)対応 --  END  --            " & vbNewLine

    ''' <summary>
    ''' 印刷データ取得時、追加FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ADD_FROM As String = " --営業所M                                          " & vbNewLine _
                                         & " LEFT JOIN                                          " & vbNewLine _
                                         & "   $LM_MST$..M_NRS_BR   M_NRS_BR                    " & vbNewLine _
                                         & " ON                                                 " & vbNewLine _
                                         & "   M_NRS_BR.NRS_BR_CD   = @NRS_BR_CD                " & vbNewLine _
                                         & " --倉庫M                                            " & vbNewLine _
                                         & " LEFT JOIN                                          " & vbNewLine _
                                         & "   $LM_MST$..M_SOKO    M_SOKO                       " & vbNewLine _
                                         & " ON                                                 " & vbNewLine _
                                         & "   E_SAGYO.WH_CD   =  M_SOKO.WH_CD                  " & vbNewLine _
                                         & " --作業項目M                                        " & vbNewLine _
                                         & " LEFT JOIN                                          " & vbNewLine _
                                         & "   $LM_MST$..M_SAGYO   M_SAGYO                      " & vbNewLine _
                                         & " ON                                                 " & vbNewLine _
                                         & "   M_SAGYO.NRS_BR_CD   = @NRS_BR_CD                 " & vbNewLine _
                                         & " AND                                                " & vbNewLine _
                                         & "   E_SAGYO.SAGYO_CD    = M_SAGYO.SAGYO_CD           " & vbNewLine _
                                         & " --請求先M                                          " & vbNewLine _
                                         & " LEFT JOIN                                          " & vbNewLine _
                                         & "   $LM_MST$..M_SEIQTO  M_SEIQTO                     " & vbNewLine _
                                         & " ON                                                 " & vbNewLine _
                                         & "   M_SEIQTO.NRS_BR_CD   = @NRS_BR_CD                " & vbNewLine _
                                         & " AND                                                " & vbNewLine _
                                         & "   E_SAGYO.SEIQTO_CD    = M_SEIQTO.SEIQTO_CD        " & vbNewLine _
                                         & " --区分M                                            " & vbNewLine _
                                         & " LEFT JOIN                                          " & vbNewLine _
                                         & "   $LM_MST$..Z_KBN   Z_KBN1                         " & vbNewLine _
                                         & " ON                                                 " & vbNewLine _
                                         & "   Z_KBN1.KBN_GROUP_CD = 'M010'                     " & vbNewLine _
                                         & " AND                                                " & vbNewLine _
                                         & "   E_SAGYO.IOZS_KB     = Z_KBN1.KBN_CD              " & vbNewLine _
                                         & " --区分M                                            " & vbNewLine _
                                         & " LEFT JOIN                                          " & vbNewLine _
                                         & "   $LM_MST$..Z_KBN   Z_KBN2                         " & vbNewLine _
                                         & " ON                                                 " & vbNewLine _
                                         & "   Z_KBN2.KBN_GROUP_CD = 'S061'                     " & vbNewLine _
                                         & " AND                                                " & vbNewLine _
                                         & "   Z_KBN2.KBN_CD = @PRT_SHUBETU                     " & vbNewLine _
                                         & " -- LME510作業料明細書(ローム用)対応 -- START --    " & vbNewLine _
                                         & " --区分M                                            " & vbNewLine _
                                         & " LEFT JOIN $LM_MST$..Z_KBN Z_KBN3                   " & vbNewLine _
                                         & "        ON Z_KBN3.KBN_GROUP_CD = 'T020'             " & vbNewLine _
                                         & "       AND Z_KBN3.KBN_NM1      = M_SAGYO.NRS_BR_CD  " & vbNewLine _
                                         & "       AND Z_KBN3.KBN_NM2      = E_SAGYO.SAGYO_CD   " & vbNewLine _
                                         & "       AND Z_KBN3.KBN_NM3      = M_SAGYO.CUST_CD_L  " & vbNewLine _
                                         & " -- LME510作業料明細書(ローム用)対応 --  END  --    " & vbNewLine _
                                         & "--荷主明細マスタ                                    " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                " & vbNewLine _
                                         & "ON  MCD.NRS_BR_CD = E_SAGYO.NRS_BR_CD               " & vbNewLine _
                                         & "AND MCD.CUST_CD = E_SAGYO.CUST_CD_L + E_SAGYO.CUST_CD_M + ISNULL(M_GOODS.CUST_CD_S,'00') +  ISNULL(M_GOODS.CUST_CD_SS,'00')  " & vbNewLine _
                                         & "AND MCD.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                                         & "AND MCD.SUB_KB = '14'                               " & vbNewLine _
                                         & "AND MCD.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                         & "   LEFT JOIN                                                   " & vbNewLine _
                                         & "       LM_MST..Z_KBN RPT_CHG_START_YM                          " & vbNewLine _
                                         & "           ON  RPT_CHG_START_YM.KBN_GROUP_CD = 'B043'          " & vbNewLine _
                                         & "           AND RPT_CHG_START_YM.KBN_CD       = '01'            " & vbNewLine _
                                         & "           AND RPT_CHG_START_YM.SYS_DEL_FLG  = '0'             " & vbNewLine _
                                         & "   LEFT JOIN                                                   " & vbNewLine _
                                         & "       LM_MST..Z_KBN OLD_NRS_BR_NM                             " & vbNewLine _
                                         & "           ON  OLD_NRS_BR_NM.KBN_GROUP_CD = 'B044'             " & vbNewLine _
                                         & "           AND OLD_NRS_BR_NM.KBN_CD       =  E_SAGYO.NRS_BR_CD " & vbNewLine _
                                         & "           AND OLD_NRS_BR_NM.SYS_DEL_FLG =  '0'                " & vbNewLine

#If True Then   'ADD 2019/09/13 007325【LMS】作業項目マスタ請求の有無に✕が付いてる作業項目は作業料明細検索画面に表示→チェックリスト印字、請求書には出さないようにする
    ''' <summary>
    ''' 印刷データ取得時、追加2FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ADD2_FROM As String = " --作業項目M                                        " & vbNewLine _
                                         & " LEFT JOIN                                          " & vbNewLine _
                                         & "   $LM_MST$..M_SAGYO   M_SAGYO                      " & vbNewLine _
                                         & " ON                                                 " & vbNewLine _
                                         & "   M_SAGYO.NRS_BR_CD   = @NRS_BR_CD                 " & vbNewLine _
                                         & " AND                                                " & vbNewLine _
                                         & "   E_SAGYO.SAGYO_CD    = M_SAGYO.SAGYO_CD           " & vbNewLine
#End If

    ''' <summary>
    ''' WHERE句 作業料明細書(請求確定)（必須）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE As String = " WHERE                                                      " & vbNewLine _
                                             & " -- 必須                                                    " & vbNewLine _
                                             & "   E_SAGYO.NRS_BR_CD   = @NRS_BR_CD                         " & vbNewLine _
                                             & " AND                                                        " & vbNewLine _
                                             & "   E_SAGYO.SAGYO_COMP  = '01'                               " & vbNewLine _
                                             & " AND                                                        " & vbNewLine _
                                             & "   E_SAGYO.SKYU_CHK    = '01'                               " & vbNewLine _
                                             & " AND                                                        " & vbNewLine _
                                             & "   E_SAGYO.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                             & " AND                                                        " & vbNewLine _
                                             & "   @F_DATE        <= E_SAGYO.SAGYO_COMP_DATE       " & vbNewLine _
                                             & " AND                                                        " & vbNewLine _
                                             & "   E_SAGYO.SAGYO_COMP_DATE <= @T_DATE                " & vbNewLine _
                                             & " AND                                                        " & vbNewLine _
                                             & "   E_SAGYO.SAGYO_GK <> 0                                    " & vbNewLine _
                                             & " AND M_SAGYO.INV_YN = '01'  --請求有無 01:有  --ADD 2019/09/13 007325 " & vbNewLine


    '2次対応 作業料明細書・チェックリストの切替 2012.01.18 START
    ''' <summary>
    ''' WHERE句 作業料明細書チェックリスト(請求未確定)（必須）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE1 As String = " WHERE                                                      " & vbNewLine _
                                             & " -- 必須                                                    " & vbNewLine _
                                             & "   E_SAGYO.NRS_BR_CD   = @NRS_BR_CD                         " & vbNewLine _
                                             & " AND                                                        " & vbNewLine _
                                             & "   E_SAGYO.SAGYO_COMP  = '01'                               " & vbNewLine _
                                             & " AND                                                        " & vbNewLine _
                                             & "   E_SAGYO.SKYU_CHK    = '00'                               " & vbNewLine _
                                             & " AND                                                        " & vbNewLine _
                                             & "   E_SAGYO.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                             & " AND                                                        " & vbNewLine _
                                             & "   @F_DATE        <= E_SAGYO.SAGYO_COMP_DATE       " & vbNewLine _
                                             & " AND                                                        " & vbNewLine _
                                             & "   E_SAGYO.SAGYO_COMP_DATE <= @T_DATE                " & vbNewLine _
                                             & " --AND                                                        " & vbNewLine _
                                             & " --  E_SAGYO.SAGYO_GK <> 0                                    " & vbNewLine

    '2次対応 作業料明細書・チェックリストの切替 2012.01.18 END


    ''' <summary>
    ''' ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY              " & vbNewLine _
                                         & "       OFB_NM          " & vbNewLine _
                                         & "     , SEIQTO_CD       " & vbNewLine _
                                         & "     , CUST_CD_L       " & vbNewLine _
                                         & "     , CUST_CD_M       " & vbNewLine _
                                         & "     , CUST_CD_S       " & vbNewLine _
                                         & "     , CUST_CD_SS      " & vbNewLine _
                                         & "     , SAGYO_COMP_DATE " & vbNewLine _
                                         & "     , SAGYO_CD        " & vbNewLine _
                                         & "     , IOZS_KB         " & vbNewLine

    Private Const SQL_ORDER_BY_510 As String = " ORDER BY               " & vbNewLine _
                                             & "       OFB_NM           " & vbNewLine _
                                             & "     , SEIQTO_CD        " & vbNewLine _
                                             & "     , BUNSEKI_UMU_FLG  " & vbNewLine _
                                             & "     , SAGYO_COMP_DATE  " & vbNewLine _
                                             & "     , SAGYO_CD         " & vbNewLine _
                                             & "     , IOZS_KB          " & vbNewLine

    '(2012.08.08) LME501：作業料明細書・作業コード順 --- START ---
    Private Const SQL_ORDER_BY_501 As String = " ORDER BY              " & vbNewLine _
                                             & "       OFB_NM          " & vbNewLine _
                                             & "     , SEIQTO_CD       " & vbNewLine _
                                             & "     , CUST_CD_L       " & vbNewLine _
                                             & "     , CUST_CD_M       " & vbNewLine _
                                             & "     , CUST_CD_S       " & vbNewLine _
                                             & "     , CUST_CD_SS      " & vbNewLine _
                                             & "     , SAGYO_CD        " & vbNewLine _
                                             & "     , SAGYO_COMP_DATE " & vbNewLine _
                                             & "     , IOZS_KB         " & vbNewLine
    '(2012.08.08) LME501：作業料明細書・作業コード順 ---  END  ---

    '(2012.09.04) LME502：作業料明細書・荷主(小)･(極小)改頁なし版 --- START ---
    Private Const SQL_ORDER_BY_502 As String = " ORDER BY              " & vbNewLine _
                                             & "       OFB_NM          " & vbNewLine _
                                             & "     , SEIQTO_CD       " & vbNewLine _
                                             & "     , CUST_CD_L       " & vbNewLine _
                                             & "     , CUST_CD_M       " & vbNewLine _
                                             & "     , SAGYO_COMP_DATE " & vbNewLine _
                                             & "     , SAGYO_CD        " & vbNewLine _
                                             & "     , IOZS_KB         " & vbNewLine
    '(2012.09.04) LME502：作業料明細書・荷主(小)･(極小)改頁なし版 ---  END  ---


    '2012/12/06 緊急対応 本明修正 START
    ''(2012.12.02) LME504：作業料明細書・作業レコード番号順 --- START ---
    'Private Const SQL_ORDER_BY_504 As String = " ORDER BY              " & vbNewLine _
    '                                         & "       OFB_NM          " & vbNewLine _
    '                                         & "     , SEIQTO_CD       " & vbNewLine _
    '                                         & "     , CUST_CD_L       " & vbNewLine _
    '                                         & "     , CUST_CD_M       " & vbNewLine _
    '                                         & "     , CUST_CD_S       " & vbNewLine _
    '                                         & "     , CUST_CD_SS      " & vbNewLine _
    '                                         & "     , SAGYO_COMP_DATE " & vbNewLine _
    '                                         & "     , GOODS_NM_NRS    " & vbNewLine _
    '                                         & "     , LOT_NO          " & vbNewLine _
    '                                         & "     , SAGYO_REC_NO    " & vbNewLine _
    '                                         & "     , IOZS_KB         " & vbNewLine
    ''(2012.12.02) LME504：作業料明細書・作業レコード番号順 ---  END  ---

    '(2012.12.02) LME504：作業料明細書・作業レコード番号順 --- START ---
    Private Const SQL_ORDER_BY_504 As String = " ORDER BY              " & vbNewLine _
                                             & "       OFB_NM          " & vbNewLine _
                                             & "     , SEIQTO_CD       " & vbNewLine _
                                             & "     , CUST_CD_L       " & vbNewLine _
                                             & "     , CUST_CD_M       " & vbNewLine _
                                             & "     , CUST_CD_S       " & vbNewLine _
                                             & "     , CUST_CD_SS      " & vbNewLine _
                                             & "     , SAGYO_COMP_DATE " & vbNewLine _
                                             & "     , E_SAGYO.INOUTKA_NO_LM " & vbNewLine _
                                             & "     , LOT_NO          " & vbNewLine _
                                             & "     , SAGYO_REC_NO    " & vbNewLine _
                                             & "     , IOZS_KB         " & vbNewLine
    '(2012.12.02) LME504：作業料明細書・作業レコード番号順 ---  END  ---
    '2012/12/06 緊急対応 本明修正 END

    Private Const SQL_ORDER_BY_505 As String = " ORDER BY              " & vbNewLine _
                                             & "       OFB_NM          " & vbNewLine _
                                             & "     , SEIQTO_CD       " & vbNewLine _
                                             & "     , CUST_CD_L       " & vbNewLine _
                                             & "     , CUST_CD_M       " & vbNewLine _
                                             & "     , CUST_CD_S       " & vbNewLine _
                                             & "     , CUST_CD_SS      " & vbNewLine _
                                             & "     , SAGYO_CD        " & vbNewLine _
                                             & "     , SAGYO_COMP_DATE " & vbNewLine _
                                             & "     , IOZS_KB         " & vbNewLine

#End Region '印刷データ取得

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
                                            , "RPT_ID"
                                            }
        Dim ptn As LME500DAC.SelectCondition = SelectCondition.PTN1
        Return Me.SelectListData(ds, LME500DAC.TABLE_NM_M_RPT, Me.CreateSql(ds, ptn), ptn, str)

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
                                          , "SEIQTO_CD" _
                                          , "SEIQTO_NM" _
                                          , "CUST_CD_L" _
                                          , "CUST_CD_M" _
                                          , "CUST_CD_S" _
                                          , "CUST_CD_SS" _
                                          , "CUST_NM_L" _
                                          , "CUST_NM_M" _
                                          , "CUST_NM_S" _
                                          , "CUST_NM_SS" _
                                          , "NRS_BR_NM" _
                                          , "NRS_BR_AD" _
                                          , "NRS_BR_TEL" _
                                          , "SAGYO_COMP_DATE" _
                                          , "GOODS_CD_NRS" _
                                          , "GOODS_NM_NRS" _
                                          , "LOT_NO" _
                                          , "SAGYO_CD" _
                                          , "SAGYO_NM" _
                                          , "REMARK_SKYU" _
                                          , "DEST_CD" _
                                          , "DEST_NM" _
                                          , "IOZS_KB" _
                                          , "IOZS_NM" _
                                          , "SAGYO_NB" _
                                          , "SAGYO_UP" _
                                          , "SAGYO_GK" _
                                          , "F_DATE" _
                                          , "T_DATE" _
                                          , "SAGYO_TITLE" _
                                          , "OFB_NM" _
                                          , "BUNSEKI_UMU_FLG"
                                           }

        Dim ptn As LME500DAC.SelectCondition = SelectCondition.PTN2
        Return Me.SelectListData(ds, LME500DAC.TABLE_NM_OUT, Me.CreateSql(ds, ptn), ptn, str)

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
    Private Function SelectListData(ByVal ds As DataSet, ByVal tblNm As String, ByVal sql As String, ByVal ptn As LME500DAC.SelectCondition, ByVal str As String()) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LME500DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, ptn)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LME500DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
    ''' SQL作成
    ''' </summary>
    ''' <param name="ptn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSql(ByVal ds As DataSet, ByVal ptn As LME500DAC.SelectCondition) As String

        Dim sql As StringBuilder = New StringBuilder()
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        '2次対応 作業料明細書・チェックリストの切替 2012.01.18 START
        Dim prtKb As String = ds.Tables("LME500IN").Rows(0).Item("PRT_SHUBETU").ToString()
        '2次対応 作業料明細書・チェックリストの切替 2012.01.18 END

        Select Case ptn

            Case SelectCondition.PTN1

                sql.Append(LME500DAC.SQL_SELECT_MPrt)
                sql.Append(LME500DAC.SQL_ALL_FROM)
                '2次対応 作業料明細書・チェックリストの切替 2012.01.18 START
                Select Case prtKb
                    Case SEIKYU_ON
#If True Then   'ADD 2019/09/13 007325【LMS】作業項目マスタ請求の有無に✕が付いてる作業項目は作業料明細検索画面に表示→チェックリスト印字、請求書には出さないよ
                        sql.Append(LME500DAC.SQL_ADD2_FROM)
#End If
                        sql.Append(LME500DAC.SQL_SELECT_WHERE)

                    Case SEIKYU_OFF
                        sql.Append(LME500DAC.SQL_SELECT_WHERE1)
                    Case Else

                End Select
                '2次対応 作業料明細書・チェックリストの切替 2012.01.18 END
                sql.Append(Me.CreateWhereSql(ds))

            Case SelectCondition.PTN2
                sql.Append(LME500DAC.SQL_SELECT_FIRST)
                sql.Append(LME500DAC.SQL_ALL_FROM)
                sql.Append(LME500DAC.SQL_ADD_FROM)
                '2次対応 作業料明細書・チェックリストの切替 2012.01.18 START
                Select Case prtKb
                    Case SEIKYU_ON
                        sql.Append(LME500DAC.SQL_SELECT_WHERE)

                    Case SEIKYU_OFF
                        sql.Append(LME500DAC.SQL_SELECT_WHERE1)
                    Case Else

                End Select
                '2次対応 作業料明細書・チェックリストの切替 2012.01.18 END
                sql.Append(Me.CreateWhereSql(ds))

                '(2012.08.08) LME501：作業料明細書・作業コード順 --- START ---
                Select Case rptTbl.Rows(0).Item("RPT_ID").ToString()
                    Case "LME510"
                        sql.Append(LME500DAC.SQL_ORDER_BY_510)

                    Case "LME501"   '作業コード優先
                        sql.Append(LME500DAC.SQL_ORDER_BY_501)

                        '(2012.09.04) LME502：作業料明細書・荷主(小)･(極小)改頁なし版 --- START ---
                    Case "LME502"   '作業コード優先
                        sql.Append(LME500DAC.SQL_ORDER_BY_502)
                        '(2012.09.04) LME502：作業料明細書・荷主(小)･(極小)改頁なし版 ---  END ---

                        '(2012.12.02) LME502：作業料明細書・作業レコード番号優先 --- START ---
                    Case "LME504"   '作業レコード番号優先
                        sql.Append(LME500DAC.SQL_ORDER_BY_504)
                        '(2012.12.02) LME502：作業料明細書・作業レコード番号優先 ---  END ---

                    Case "LME505"   '作業コード優先・作業毎合計あり
                        sql.Append(LME500DAC.SQL_ORDER_BY_505)

                    Case Else
                        sql.Append(LME500DAC.SQL_ORDER_BY)

                End Select

                'LME510作業料明細書(ローム用)対応 -- START --
                'sql.Append(LME500DAC.SQL_ORDER_BY)
                'If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LME510" Then
                '    sql.Append(LME500DAC.SQL_ORDER_BY_510)
                'Else
                '    sql.Append(LME500DAC.SQL_ORDER_BY)
                'End If
                'LME510作業料明細書(ローム用)対応 --  END  --
                '(2012.08.08) LME501：作業料明細書・作業コード順 ---  END  ---

        End Select

        Return sql.ToString()

    End Function

    ''' <summary>
    ''' WHERE句設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateWhereSql(ByVal ds As DataSet) As String

        Dim whereStr As String = String.Empty
        Dim sql As StringBuilder = New StringBuilder()
        Dim dr As DataRow = ds.Tables(LME500DAC.TABLE_NM_IN).Rows(0)

        With dr

            '倉庫コード
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND E_SAGYO.WH_CD = @WH_CD")
                sql.Append(vbNewLine)
            End If

            '荷主コード大
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND E_SAGYO.CUST_CD_L = @CUST_CD_L")
                sql.Append(vbNewLine)
            End If

            '荷主コード中
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND E_SAGYO.CUST_CD_M = @CUST_CD_M")
                sql.Append(vbNewLine)
            End If

            '請求先コード
            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND E_SAGYO.SEIQTO_CD LIKE @SEIQTO_CD")
                sql.Append(vbNewLine)
            End If

            '作業コード
            whereStr = .Item("SAGYO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND E_SAGYO.SAGYO_CD LIKE @SAGYO_CD")
                sql.Append(vbNewLine)
            End If

            '作業指示№
            whereStr = .Item("SAGYO_SIJI_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND E_SAGYO.SAGYO_SIJI_NO LIKE @SAGYO_SIJI_NO")
                sql.Append(vbNewLine)
            End If

        End With

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
    Private Sub SetSelectParam(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal ptn As LME500DAC.SelectCondition)

        With dr

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@PTN_ID", LME500DAC.PTN_ID, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            If String.IsNullOrEmpty(.Item("WH_CD").ToString()) = False Then
                prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            End If
            If String.IsNullOrEmpty(.Item("CUST_CD_L").ToString()) = False Then
                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            End If
            If String.IsNullOrEmpty(.Item("CUST_CD_M").ToString()) = False Then
                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            End If
            prmList.Add(MyBase.GetSqlParameter("@F_DATE", .Item("F_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@T_DATE", .Item("T_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", String.Concat(.Item("SAGYO_SIJI_NO").ToString(), "%"), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Concat(.Item("SEIQTO_CD").ToString(), "%"), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", String.Concat(.Item("SAGYO_CD").ToString(), "%"), DBDataType.CHAR))
            '2次対応 作業料明細書・チェックリストの切替 2012.01.18 START
            prmList.Add(MyBase.GetSqlParameter("@PRT_SHUBETU", .Item("PRT_SHUBETU").ToString(), DBDataType.CHAR))
            '2次対応 作業料明細書・チェックリストの切替 2012.01.18 END

            Select Case ptn

                Case LME500DAC.SelectCondition.PTN1 '帳票パターン取得

                Case LME500DAC.SelectCondition.PTN2  '印刷データ取得

            End Select

        End With

    End Sub

#End Region

#End Region

End Class
